'****************************************************
'功能說明：整批新員資料上傳作業
'建立人員：Micky
'建立日期：2015.08.05
'****************************************************
Imports System.Data
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.IO
Imports System.IO.Compression
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Class RG_RG1200
    Inherits PageBase
    Public Shared intSuccessCount As Integer = 0
    Public Shared intErrorCount As Integer = 0
    Public Shared intTotalCount As Integer = 0
    Public Shared intTotalWarning As Integer = 0
    Public Const cnAnnualPay = "A000" '(年薪)
    Public Const cnMealPay = "B000" '(伙食津貼)
    Public Shared strFileName As String = ""
    Public Shared strMessageTitle As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnUpload"  '上傳
                If funCheckData() Then
                    Dim strFileName As String = Bsp.Utility.GetNewFileName("RG1200")
                    Dim strFilePath As String = Server.MapPath(Bsp.Utility.getAppSetting("TempPath")) & "\" & strFileName & ".txt"
                    FileUpload.PostedFile.SaveAs(strFilePath)
                    hidFileUploadFileName.Value = strFileName & ".txt"
                    Dim strLogFile As String = FileUpload.PostedFile.FileName
                    strLogFile = strLogFile.Substring(strLogFile.LastIndexOf("\") + 1)
                    strLogFile = strLogFile.Substring(0, strLogFile.LastIndexOf("."))
                    hidLogFileName.Value = strLogFile & strFileName & ".err"

                    If IsDataExist.Value = "Y" Then
                        Bsp.Utility.RunClientScript(Me.Page, "IsTOConfirm('');")
                    Else
                        Select Case UploadSelect.SelectedValue
                            Case "1"    '1.員工基本資料
                                DoUpload()
                            Case "3"    '3.員工教育資料
                                DoUpload()
                            Case "4"    '4.員工前職經歷資料
                                DoUpload()
                            Case "6"    '6.員工簽核資料
                                DoUpload()
                            Case "2"    '2.員工年薪主檔資料
                                Release("Release")
                            Case "5"    '5.員工勞退自提比率資料
                                Release("Release")
                        End Select
                    End If
                End If
        End Select
    End Sub

    Private Sub DoUpload()
        Dim objHR As New HR
        Dim strMessage As String = ""
        Dim strUpload_TableName As String = ""
        Dim strWorkSheet As String = ""

        Try
            strFileName = Server.MapPath(Bsp.Utility.getAppSetting("TempPath")) & "\" & hidFileUploadFileName.Value

            strUpload_TableName = objHR.funCheck_UploadTableName(strFileName).Replace("'", "").ToString
            Select Case UploadSelect.SelectedValue
                Case "1"    '1.員工基本資料
                    strMessageTitle = "員工基本資料"
                    strWorkSheet = "RG1200_Personal"
                Case "2"    '2.員工年薪主檔資料
                    strMessageTitle = "員工年薪主檔資料"
                    strWorkSheet = "RG1200_Salary"
                Case "3"    '3.員工教育資料
                    strMessageTitle = "員工教育資料"
                    strWorkSheet = "RG1200_Education"
                Case "4"    '4.員工前職經歷資料
                    strMessageTitle = "員工前職經歷資料"
                    strWorkSheet = "RG1200_Experience"
                Case "5"    '5.員工勞退自提比率資料
                    strMessageTitle = "員工勞退自提比率資料"
                    strWorkSheet = "RG1200_EmpRatio"
                Case "6"    '6.員工簽核資料
                    strMessageTitle = "員工簽核資料"
                    strWorkSheet = "RG1200_EmpFlow"
            End Select

            If strUpload_TableName <> strWorkSheet + "$" Then
                File.Delete(strFileName)
                strMessage = "上傳檔案的工作表(WorkSheet)-" & Mid(strUpload_TableName, 1, Len(strUpload_TableName) - 1) & " 與選擇上傳類型-" + strWorkSheet + "不一致!"
                Bsp.Utility.RunClientScript(Me, "alert('" & String.Format(Bsp.Utility.getMessage("E_00050"), strMessage) & "');" & vbCrLf & "window.top.returnValue = 'OK';")
                Return  '2015/12/08 Add
            End If

            If objHR.funCheck_UploadCount(strFileName, "[" + strWorkSheet + "$]") > CInt(ConfigurationManager.AppSettings("Upload_MaxCount").ToString) Then
                File.Delete(strFileName)
                strMessage = "上傳資料筆數過大，系統允許最大上傳筆數：" & ConfigurationManager.AppSettings("Upload_MaxCount").ToString
                Bsp.Utility.RunClientScript(Me, "alert('" & String.Format(Bsp.Utility.getMessage("E_00050"), strMessage) & "');" & vbCrLf & "window.top.returnValue = 'OK';")
                Return  '2015/12/08 Add
            End If

            If funCheckData1(strFileName) Then
                File.Delete(strFileName)
                If UploadSelect.SelectedValue = "1" Then
                    'strMessage = "RG1200資料上傳總筆數：" & intTotalCount & " 成功筆數：" & intSuccessCount & " 失敗筆數：" & intErrorCount & " 警告筆數：" & intTotalWarning
                    strMessage = "RG1200" & strMessageTitle & "上傳成功！\n上傳總筆數：" & intTotalCount & "\n成功筆數：" & intSuccessCount & "\n失敗筆數：" & intErrorCount & "\n警告筆數：" & intTotalWarning    '2015/11/18 Modify 修改提示訊息

                    If intErrorCount <> 0 Or intTotalWarning <> 0 Then
                        Bsp.Utility.RunClientScript(Me.Page, "IsTODownload('" & strMessage & "');")
                    Else
                        Bsp.Utility.RunClientScript(Me, "alert('" & strMessage & "');")
                    End If
                Else
                    'strMessage = "RG1200資料上傳總筆數：" & intTotalCount & " 成功筆數：" & intSuccessCount & " 失敗筆數：" & intErrorCount
                    strMessage = "RG1200" & strMessageTitle & "上傳成功！\n上傳總筆數：" & intTotalCount & "\n成功筆數：" & intSuccessCount & "\n失敗筆數：" & intErrorCount      '2015/11/18 Modify 修改提示訊息
                    Bsp.Utility.RunClientScript(Me, "alert('" & strMessage & "');")
                End If
            Else
                '刪除上傳檔案
                File.Delete(strFileName)

                If UploadSelect.SelectedValue = "1" Then
                    'strMessage = "『上傳檔案失敗！總筆數：" & intTotalCount & " ，成功筆數：" & intSuccessCount & "， 失敗筆數：" & intErrorCount & "， 警告筆數：" & intTotalWarning & "，請下載錯誤紀錄log檔案!"
                    strMessage = "RG1200" & strMessageTitle & "上傳失敗，請下載錯誤記錄Log檔案！\n上傳總筆數：" & intTotalCount & "\n成功筆數：" & intSuccessCount & "\n失敗筆數：" & intErrorCount & "\n警告筆數：" & intTotalWarning   '2015/11/18 Modify 修改提示訊息
                Else
                    'strMessage = "『上傳檔案失敗！總筆數：" & intTotalCount & " ，成功筆數：" & intSuccessCount & "， 失敗筆數：" & intErrorCount & "，請下載錯誤紀錄log檔案!"
                    strMessage = "RG1200" & strMessageTitle & "上傳失敗，請下載錯誤記錄Log檔案！\n上傳總筆數：" & intTotalCount & "\n成功筆數：" & intSuccessCount & "\n失敗筆數：" & intErrorCount     '2015/11/18 Modify 修改提示訊息
                End If

                '顯示上傳失敗錯誤訊息，並下載ERR LOG檔案
                Bsp.Utility.RunClientScript(Me.Page, "IsTODownload('" & strMessage & "');")
            End If

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoUpload", ex)
        End Try
    End Sub

    Private Function funCheckData() As Boolean
        '上傳類型
        If UploadSelect.SelectedValue <> "1" And UploadSelect.SelectedValue <> "2" And UploadSelect.SelectedValue <> "3" And UploadSelect.SelectedValue <> "4" And UploadSelect.SelectedValue <> "5" And UploadSelect.SelectedValue <> "6" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblUploadType.Text)
            UploadSelect.Focus()
            Return False
        End If

        '檔案路徑
        If FileUpload.PostedFile Is Nothing Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00120", lblUploadPath.Text)
            FileUpload.Focus()
            Return False
        Else
            If FileUpload.PostedFile.FileName = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblUploadPath.Text)
                FileUpload.Focus()
                Return False
            End If
        End If

        Return True
    End Function

    Private Function funCheckData1(ByVal FileName As String) As Boolean
        Dim flag As Boolean = False
        Select Case UploadSelect.SelectedValue
            Case "1"    '1.員工基本資料
                flag = funCheckData_Personal(FileName)
            Case "2"    '2.員工年薪主檔資料
                flag = funCheckData_Salary(FileName)
            Case "3"    '3.員工教育資料
                flag = funCheckData_Education(FileName)
            Case "4"    '4.員工前職經歷資料
                flag = funCheckData_Experience(FileName)
            Case "5"    '5.員工勞退自提比率資料
                flag = funCheckData_EmpRatio(FileName)
            Case "6"    '6.員工簽核資料
                flag = funCheckData_EmpFlow(FileName)
        End Select
        Return flag
    End Function

#Region "ERR LOG檔案"
    '隱藏的按鈕--下傳檔案
    Protected Sub btnUpd1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpd1.Click
        subDownloadLogFile()
    End Sub

    Private Sub subWriteLog(ByVal strLogString As String, Optional ByVal strFileformat As String = "Unicode")
        '產生檔案寫法一
        'Dim FileNum As Integer
        'FileNum = FreeFile()
        'FileOpen(FileNum, strLogFileName, OpenMode.Append)
        'PrintLine(FileNum, strLogString)
        'FileClose(FileNum)

        '產生檔案寫法二---若要存成Unicode TXT檔，需改這種寫法，指定要輸出別的編碼方式
        Dim strFileName As String = Server.MapPath(Bsp.Utility.getAppSetting("TempPath")) & "\" & hidLogFileName.Value
        Using Writer As System.IO.StreamWriter = New System.IO.StreamWriter(strFileName, True, System.Text.Encoding.Unicode)
            Writer.WriteLine(strLogString)
        End Using
    End Sub

    '將ERR LOG檔案立即提供USER端下載，完成後刪除檔案
    '若發生ERR LOG檔案在SERVER有產生，但無法提供下載(USER端無跳出檔案下載視窗畫面)~~WHY??=>IE\工具\網際網路選項\安全性\加入信任的網站 即可!!!!!
    Private Sub subDownloadLogFile()

        '若有多個ShowFormatMessage時，只會跳最後一個
        'Bsp.Utility.ShowFormatMessage(Me, "W_00031", "subDownloadLogFile!")

        Dim strFileName As String = Server.MapPath(Bsp.Utility.getAppSetting("TempPath")) & "\" & hidLogFileName.Value
        If System.IO.File.Exists(strFileName) Then
            Response.ClearContent()
            Response.BufferOutput = True
            Response.Charset = "utf-8"
            '設定MIME類型  
            'Response.ContentType = "application/ms-excel"       '只寫ms-excel不OK，會變成程式碼下載@@
            'Response.ContentType = "application/vnd.ms-excel"
            'Response.ContentType = "application/save-as"
            'Response.ContentType = "Application/unknown"
            Response.ContentType = "application/octet-stream"

            Response.AddHeader("Content-Transfer-Encoding", "binary")
            Response.ContentEncoding = System.Text.Encoding.Default
            '跳出視窗，讓用戶端選擇要儲存的地方     '=>使用Server.UrlPathEncode()編碼中文字才不會下載時，檔名為亂碼
            Response.AddHeader("Content-Disposition", "attachment; filename=" & Server.UrlPathEncode(hidLogFileName.Value))
            '=>使用Server.UrlEncode()還是亂碼
            'http://blog.miniasp.com/post/2008/04/20/ASPNET-Force-Download-File-and-deal-with-Chinese-Filename-correctly.aspx
            '「網址的路徑(Path)」與「網址的參數(QueryString)」編碼方式不一樣! 
            '路徑包括目錄名稱與檔案名稱的部分，要用 HttpUtility.UrlPathEncode 編碼。 
            '參數的部分才用 HttpUtility.UrlEncode 編碼。 
            '例如：空白字元( )用 HttpUtility.UrlPathEncode 會變成(%20)，但用 HttpUtility.UrlEncode 卻會變成加號(+)，而檔名中空白的部分用 %20 才是對的，否則存檔後檔名空白的部分會變成加號(+)那檔名就不對了

            '檔案有各式各樣，所以用BinaryWrite 
            Response.BinaryWrite(File.ReadAllBytes(strFileName))
            'Response.WriteFile(strFileName)                     '這寫法也可

            '刪除Server上log檔
            File.Delete(strFileName)

            Response.End()      '加這行後續的程式執行程式碼才不會寫入檔案，但後續處理就都不會進行了
        Else
            Response.Write("無下傳欄位格式說明檔")    '2015/11/24 Modify 改文字說明
        End If

        '----------------------------------------------------
        '這寫法也可
        'Dim default_file_root As String = Server.MapPath(Bsp.Utility.getAppSetting("TempPath")) & "\"
        'Dim user_request As String = "test.txt"

        'Dim filep As String = default_file_root & user_request
        'If System.IO.File.Exists(filep) Then
        '    With Response
        '        .ContentType = "application/save-as"
        '        .AddHeader("content-disposition", "attachment; filename=" & user_request)
        '        .WriteFile(filep)
        '        .End()
        '    End With
        'Else
        '    Response.Write("無檔案")
        'End If
    End Sub
#End Region

#Region "欄位格式說明檔下載"
    Protected Sub btnDownload_Click(sender As Object, e As System.EventArgs) Handles btnDownload.Click
        Select Case UploadSelect.SelectedValue
            Case "1"    '1.員工基本資料
                strMessageTitle = "員工基本資料"
                subDownloadSampleFile("員工基本資料.xls")
            Case "2"    '2.員工年薪主檔資料
                strMessageTitle = "員工年薪主檔資料"
                subDownloadSampleFile("員工年薪主檔資料.xls")
            Case "3"    '3.員工教育資料
                strMessageTitle = "員工教育資料"
                subDownloadSampleFile("員工教育資料.xls")
            Case "4"    '4.員工前職經歷資料
                strMessageTitle = "員工前職經歷資料"
                subDownloadSampleFile("員工前職經歷資料.xls")
            Case "5"    '5.員工勞退自提比率資料
                strMessageTitle = "員工勞退自提比率資料"
                subDownloadSampleFile("員工勞退自提比率資料.xls")
            Case "6"    '6.員工簽核資料
                strMessageTitle = "員工簽核資料"
                subDownloadSampleFile("員工簽核資料.xls")
            Case ""
                '2015/12/08 Add 都沒選時顯示題是訊息
                Bsp.Utility.ShowMessage(Me, "「請先選擇欄位格式說明檔對應的上傳類型」")
        End Select
    End Sub

    Private Sub subDownloadSampleFile(ByVal strFile As String)
        Dim strFileName As String = Server.MapPath(Bsp.Utility.getAppSetting("DownloadPath")) & "\" & strFile
        If System.IO.File.Exists(strFileName) Then
            Response.ClearContent()
            Response.BufferOutput = True
            Response.Charset = "utf-8"
            '設定MIME類型  
            'Response.ContentType = "application/ms-excel"       '只寫ms-excel不OK，會變成程式碼下載@@
            'Response.ContentType = "application/vnd.ms-excel"
            'Response.ContentType = "application/save-as"
            'Response.ContentType = "Application/unknown"
            Response.ContentType = "application/octet-stream"

            Response.AddHeader("Content-Transfer-Encoding", "binary")
            Response.ContentEncoding = System.Text.Encoding.Default
            '跳出視窗，讓用戶端選擇要儲存的地方     '=>使用Server.UrlPathEncode()編碼中文字才不會下載時，檔名為亂碼
            Response.AddHeader("Content-Disposition", "attachment; filename=" & Server.UrlPathEncode(strFile))
            '=>使用Server.UrlEncode()還是亂碼
            'http://blog.miniasp.com/post/2008/04/20/ASPNET-Force-Download-File-and-deal-with-Chinese-Filename-correctly.aspx
            '「網址的路徑(Path)」與「網址的參數(QueryString)」編碼方式不一樣! 
            '路徑包括目錄名稱與檔案名稱的部分，要用 HttpUtility.UrlPathEncode 編碼。 
            '參數的部分才用 HttpUtility.UrlEncode 編碼。 
            '例如：空白字元( )用 HttpUtility.UrlPathEncode 會變成(%20)，但用 HttpUtility.UrlEncode 卻會變成加號(+)，而檔名中空白的部分用 %20 才是對的，否則存檔後檔名空白的部分會變成加號(+)那檔名就不對了

            '檔案有各式各樣，所以用BinaryWrite 
            Response.BinaryWrite(File.ReadAllBytes(strFileName))
            'Response.WriteFile(strFileName)                     '這寫法也可

            Response.End()      '加這行後續的程式執行程式碼才不會寫入檔案，但後續處理就都不會進行了
        Else
            '2015/12/08 Modify 修改錯誤訊息顯示方式
            'Response.Write("無檔案")
            Bsp.Utility.ShowMessage(Me, "「無" & strMessageTitle & "下傳欄位格式說明檔」")
        End If
    End Sub
#End Region

#Region "1.員工基本資料 Personal"
    Private Function funCheckData_Personal(ByVal FileName As String) As Boolean
        Dim CompID As String = UserProfile.SelectCompRoleID
        Dim objRG As New RG1
        Dim objHR As New HR
        Dim bolInputStatus As Boolean = True
        Dim strErrMsg As String = ""
        Dim strSqlWhere As String = ""
        Dim bolCheckData As Boolean = False  '檢核資料
        Dim bolCheckWarning As Boolean = False  '警告check

        '補充說明：欄位非必填且放空白	
        Dim nintCompID As Integer = 0           '公司代碼
        Dim nintEmpID As Integer = 1            '員工編號
        Dim nintIDNo As Integer = 2             '身份證字號
        Dim nintNameN As Integer = 3            '中文姓名難字
        Dim nintName As Integer = 4             '中文姓名
        Dim nintEngName As Integer = 5          '英文姓名
        Dim nintPassportName As Integer = 6     '護照英文姓名
        Dim nintBirthDate As Integer = 7        '出生日期
        Dim nintSex As Integer = 8              '性別
        Dim nintNationID As Integer = 9         '員工身分別  '2015/12/18 Modify 變更欄位名稱
        Dim nintIDType As Integer = 10          '證件類型    '2015/12/18 Add 新增欄位
        Dim nintIDExpireDate As Integer = 11    '工作證期限
        Dim nintEduID As Integer = 12           '學歷代碼
        Dim nintMarriage As Integer = 13        '婚姻狀況
        Dim nintEmpType As Integer = 14         '僱用類別
        Dim nintWorkTypeID As Integer = 15      '工作性質代碼
        'Dim nintGroupID As Integer = 16         '事業群單位代號
        Dim nintDeptID As Integer = 16          '部門代碼
        Dim nintOrganID As Integer = 17         '科/組/課代碼
        Dim nintRankID As Integer = 18          '職等代碼
        Dim nintHoldingRankID As Integer = 19   '金控職等
        Dim nintTitleID As Integer = 20         '職稱代碼
        Dim nintPublicTitleID As Integer = 21   '對外職稱代碼
        Dim nintEmpDate As Integer = 22         '公司到職日
        Dim nintProbMonth As Integer = 23       '試用考核月份
        Dim nintRegCityCode As Integer = 24     '戶籍地址縣市別
        Dim nintRegAddrCode As Integer = 25     '戶籍地址區別
        Dim nintRegAddr As Integer = 26         '戶籍地址
        Dim nintCommCityCode As Integer = 27    '通訊地址縣市別
        Dim nintCommAddrCode As Integer = 28    '通訊地址區別
        Dim nintCommAddr As Integer = 29        '通訊地址
        Dim nintCommTelCode1 As Integer = 30    '聯絡電話一國碼
        Dim nintCommTel1 As Integer = 31        '聯絡電話一
        Dim nintCommTelCode2 As Integer = 32    '聯絡電話二國碼
        Dim nintCommTel2 As Integer = 33        '聯絡電話二
        Dim nintCommTelCode3 As Integer = 34    '聯絡電話三國碼
        Dim nintCommTel3 As Integer = 35        '聯絡電話三
        Dim nintCommTelCode4 As Integer = 36    '聯絡電話四國碼
        Dim nintCommTel4 As Integer = 37        '聯絡電話四
        Dim nintRelName As Integer = 38         '緊急聯絡人
        Dim nintRelTel As Integer = 39          '緊急聯絡人電話
        Dim nintRelRelation As Integer = 40     '緊急聯絡人關係
        Dim nintPassExamFlag As Integer = 41    '新員招考註記
        Dim nintWTID As Integer = 42            '班別
        Dim nintEmpType1 As Integer = 43        '類別
        Dim nintPositionID As Integer = 44      '職位代碼
        Dim nintRecID As Integer = 45           '應試者編號
        Dim nintCheckInDate As Integer = 46     '預計報到日
        Dim nintEmail2 As Integer = 47          '外部信箱

        intTotalCount = 0
        intSuccessCount = 0
        intErrorCount = 0
        intTotalWarning = 0

        Dim strExcelConn As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & FileName & ";Extended Properties=EXCEL 8.0"

        Dim strExcelWorkSheet As String = "[RG1200_Personal$]"
        Dim strExcelSelect As String = "SELECT * FROM " & strExcelWorkSheet

        Dim myExcelConn As OleDbConnection = Nothing
        myExcelConn = New OleDbConnection(strExcelConn)
        Dim myExcelCommand As OleDbCommand = New OleDbCommand(strExcelSelect, myExcelConn)
        Try
            myExcelConn.Open()

            Using myDataRead As OleDbDataReader = myExcelCommand.ExecuteReader
                While myDataRead.Read

                    '檢核上傳資料
                    strErrMsg = ""
                    bolCheckData = True
                    bolCheckWarning = True

                    '先判斷是否上傳欄位個數正確
                    If myDataRead.FieldCount <> 48 Then
                        strErrMsg = strErrMsg & "==> 上傳資料欄位個數不正確！"
                        intErrorCount = intErrorCount + 1
                        bolCheckData = False
                    Else
                        '公司代碼與授權公司代碼不同
                        If CompID <> myDataRead(nintCompID).ToString Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCompID).ToString) & " 公司代碼與授權公司代碼不同!"
                            bolCheckData = False
                        End If

                        '公司代碼
                        If Trim(myDataRead(nintCompID).ToString) = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCompID).ToString) & " 公司代碼未輸入!"
                            bolCheckData = False
                        Else
                            If Trim(myDataRead(nintCompID).ToString).Length > 6 Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCompID).ToString) & " 公司代碼最多6個字!"
                                bolCheckData = False
                            Else
                                strSqlWhere = ""
                                strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                                If Not objRG.IsDataExists("Company", strSqlWhere) Then
                                    strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCompID).ToString) & " 查無公司代碼!"
                                    bolCheckData = False
                                End If
                            End If
                        End If

                        '員工編號
                        If myDataRead(nintEmpID).ToString().Trim() = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintEmpID).ToString) & " 員工編號未輸入!"
                            bolCheckData = False
                        Else
                            If Trim(myDataRead(nintEmpID).ToString).Length > 6 Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintEmpID).ToString) & " 員工編號最多6個字!"
                                bolCheckData = False
                            Else
                                strSqlWhere = ""
                                strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                                strSqlWhere = strSqlWhere & " and EmpID =" & Bsp.Utility.Quote(Trim(myDataRead(nintEmpID).ToString))
                                strSqlWhere = strSqlWhere & " and WorkStatus = '1'" '2015/11/18 Modify 修改工作狀態的條件
                                If objRG.IsDataExists("Personal", strSqlWhere) Then
                                    strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintEmpID).ToString) & " 相同員編已經存在，且為在職狀態，請勿重複新增"
                                    bolCheckData = False
                                End If
                            End If
                        End If

                        '身份證字號
                        If Trim(myDataRead(nintIDNo).ToString) = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintIDNo).ToString) & " 身份證字號未輸入!"
                            bolCheckData = False
                        Else
                            If Trim(myDataRead(nintIDNo).ToString).Length > 10 Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintIDNo).ToString) & " 身份證字號最多10個字!"
                                bolCheckData = False
                            Else
                                strSqlWhere = ""
                                strSqlWhere = strSqlWhere & " and IDNo =" & Bsp.Utility.Quote(Trim(myDataRead(nintIDNo).ToString))
                                strSqlWhere = strSqlWhere & " and WorkStatus = '1'"
                                If objRG.IsDataExists("Personal", strSqlWhere) Then
                                    strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintIDNo).ToString) & " 員工身分證字號/居留證號已經存在，且為在職狀態，不可重複輸入"
                                    bolCheckData = False
                                End If
                            End If
                        End If

                        '中文姓名難字
                        If Trim(myDataRead(nintNameN).ToString) <> "" Then
                            If myDataRead(nintNameN).ToString.Trim.Length > 6 Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintNameN).ToString) & " 中文姓名難字最多6個中文字!"
                                bolCheckData = False
                            End If
                        End If

                        '中文姓名
                        If Trim(myDataRead(nintName).ToString) <> "" Then
                            If myDataRead(nintName).ToString.Trim.Length > 6 Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintName).ToString) & " 中文姓名最多6個中文字!"
                                bolCheckData = False
                            End If
                        End If

                        '英文姓名
                        If Trim(myDataRead(nintEngName).ToString) <> "" Then
                            If myDataRead(nintEngName).ToString.Trim.Length > 20 Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintEngName).ToString) & " 英文姓名最多20個字!"
                                bolCheckData = False
                            End If
                        End If

                        '護照英文姓名
                        If Trim(myDataRead(nintPassportName).ToString) <> "" Then
                            If myDataRead(nintPassportName).ToString.Trim.Length > 30 Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintPassportName).ToString) & " 護照英文姓名最多30個字!"
                                bolCheckData = False
                            End If
                        End If

                        '出生日期
                        If Trim(myDataRead(nintBirthDate).ToString) = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintBirthDate).ToString) & " 出生日期未輸入!"
                            bolCheckData = False
                        Else
                            If Not IsDate(Trim(myDataRead(nintBirthDate).ToString)) Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintBirthDate).ToString) & " [出生日期]請輸入西曆YYYY/MM/DD"
                                bolCheckData = False
                            End If
                        End If

                        '性別
                        If Trim(myDataRead(nintSex).ToString) = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintSex).ToString) & " 性別未輸入!"
                            bolCheckData = False
                        Else
                            If myDataRead(nintSex).ToString.Trim <> "1" And myDataRead(nintSex).ToString.Trim <> "2" Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintSex).ToString) & " 性別1：男，2：女"
                                bolCheckData = False
                            End If
                        End If

                        '員工身分別   '2015/12/18 Modify 變更欄位名稱
                        If Trim(myDataRead(nintNationID).ToString) = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintNationID).ToString) & " 員工身分別未輸入!"
                            bolCheckData = False
                        Else
                            If Trim(myDataRead(nintNationID).ToString) <> "1" And Trim(myDataRead(nintNationID).ToString) <> "2" And Trim(myDataRead(nintNationID).ToString) <> "3" And Trim(myDataRead(nintNationID).ToString) <> "4" Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintNationID).ToString) & " 員工身分別1：本國人，2：外國人，3：大陸人，4：香港人"
                                bolCheckData = False
                            End If
                            'If Trim(myDataRead(nintIDType).ToString) = "1" Then
                            '    If Not objHR.funCheckIDNO(Trim(myDataRead(nintIDNo).ToString)) Then
                            '        strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintIDNo).ToString) & " 員工身分別為本國人，員工身份證字號邏輯有誤(警告，仍可上傳)"  '2015/11/18 Modify 修改錯誤訊息
                            '        bolCheckWarning = False
                            '    End If
                            'ElseIf Trim(myDataRead(nintIDType).ToString) = "2" Then
                            '    If Not objHR.funCheckResidentIDNo(Trim(myDataRead(nintIDNo).ToString)) Then
                            '        strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintIDNo).ToString) & " 員工身分別為外國人，員工居留證號邏輯有誤(警告，仍可上傳)"   '2015/11/18 Modify 修改錯誤訊息
                            '        bolCheckWarning = False
                            '    End If
                            'Else
                            '    strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintNationID).ToString) & " 員工身分別1：本國人，2：外國人"
                            '    bolCheckData = False
                            'End If
                        End If

                        '2015/12/18 Add 新增欄位:證件類型
                        If Trim(myDataRead(nintIDType).ToString) = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintIDType).ToString) & " 證件類型未輸入!"
                            bolCheckData = False
                        Else
                            strSqlWhere = ""
                            strSqlWhere = strSqlWhere & " and Code =" & Bsp.Utility.Quote(Trim(myDataRead(nintIDType).ToString))
                            strSqlWhere = strSqlWhere & " and TabName='Personal' and FldName='IDType' and NotShowFlag='0'"
                            If Not objRG.IsDataExists("HRCodeMap", strSqlWhere) Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintIDType).ToString) & " 證件類型代碼不存在"
                                bolCheckData = False
                            Else
                                If Trim(myDataRead(nintIDType).ToString) = "1" Then
                                    If Not objHR.funCheckIDNO(Trim(myDataRead(nintIDNo).ToString)) Then
                                        strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintIDNo).ToString) & " 證件類型為1-台灣身分證字號，員工身份證字號邏輯有誤(警告，仍可上傳)"  '2015/11/18 Modify 修改錯誤訊息
                                        bolCheckWarning = False
                                    End If
                                ElseIf Trim(myDataRead(nintIDType).ToString) = "2" Then
                                    If Not objHR.funCheckResidentIDNo(Trim(myDataRead(nintIDNo).ToString)) Then
                                        strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintIDNo).ToString) & " 證件類型為2-居留證號，員工居留證號邏輯有誤(警告，仍可上傳)"   '2015/11/18 Modify 修改錯誤訊息
                                        bolCheckWarning = False
                                    End If
                                End If
                            End If
                        End If

                        '工作證期限
                        If Trim(myDataRead(nintIDExpireDate).ToString) <> "" And Not IsDate(Trim(myDataRead(nintIDExpireDate).ToString)) Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintIDExpireDate).ToString) & " [工作證期限]請輸入西曆YYYY/MM/DD"
                            bolCheckData = False
                        End If
                        'If Trim(myDataRead(nintNationID).ToString) = "2" Then
                        '    If Trim(myDataRead(nintIDExpireDate).ToString) = "" Then
                        '        strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintIDExpireDate).ToString) & " 外國人需輸入[工作證期限]" '2015/12/18 Modify 變更欄位名稱
                        '        bolCheckData = False
                        '    Else
                        '        If Not IsDate(Trim(myDataRead(nintIDExpireDate).ToString)) Then
                        '            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintIDExpireDate).ToString) & " [工作證期限]請輸入西曆YYYY/MM/DD"
                        '            bolCheckData = False
                        '        End If
                        '    End If
                        'End If

                        '學歷代碼
                        If Trim(myDataRead(nintEduID).ToString) = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintEduID).ToString) & " 學歷代碼未輸入!"
                            bolCheckData = False
                        Else
                            strSqlWhere = ""
                            strSqlWhere = strSqlWhere & " and EduID =" & Bsp.Utility.Quote(Trim(myDataRead(nintEduID).ToString))
                            If Not objRG.IsDataExists("EduDegree ", strSqlWhere) Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintIDNo).ToString) & " 學歷代碼不存在"
                                bolCheckData = False
                            End If
                        End If

                        '婚姻狀況
                        If Trim(myDataRead(nintMarriage).ToString) = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintMarriage).ToString) & " 婚姻狀況未輸入!"
                            bolCheckData = False
                        Else
                            If Trim(myDataRead(nintMarriage).ToString) <> "1" And Trim(myDataRead(nintMarriage).ToString) <> "2" Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintMarriage).ToString) & " 婚姻狀況1未婚，2已婚"
                                bolCheckData = False
                            End If
                        End If

                        '僱用類別
                        If Trim(myDataRead(nintEmpType).ToString) = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintEmpType).ToString) & " 僱用類別未輸入!"
                            bolCheckData = False
                        Else
                            If myDataRead(nintEmpType).ToString.Trim <> "1" And myDataRead(nintEmpType).ToString.Trim <> "2" Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintEmpType).ToString) & " 雇用類別1正式員工，3臨時人員"
                                bolCheckData = False
                            End If
                        End If

                        '工作性質代碼
                        If Trim(myDataRead(nintWorkTypeID).ToString) = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintWorkTypeID).ToString) & " 工作性質代碼未輸入!"
                            bolCheckData = False
                        Else
                            strSqlWhere = ""
                            strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                            strSqlWhere = strSqlWhere & " and WorkTypeID =" & Bsp.Utility.Quote(Trim(myDataRead(nintWorkTypeID).ToString))
                            If Not objRG.IsDataExists("WorkType", strSqlWhere) Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintWorkTypeID).ToString) & " 員工工作性質代碼不存在"
                                bolCheckData = False
                            End If
                        End If

                        '檢查是否已有員工工作性質檔資料
                        strSqlWhere = ""
                        strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                        strSqlWhere = strSqlWhere & " and EmpID =" & Bsp.Utility.Quote(Trim(myDataRead(nintEmpID).ToString))
                        If objRG.IsDataExists("EmpWorkType", strSqlWhere) Then
                            strErrMsg = strErrMsg & "==> " & " [員工工作性質檔]資料已存在，請勿重複新增"
                            bolCheckData = False
                        End If

                        '檢查是否已有員工職位檔資料
                        strSqlWhere = ""
                        strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                        strSqlWhere = strSqlWhere & " and EmpID =" & Bsp.Utility.Quote(Trim(myDataRead(nintEmpID).ToString))
                        If objRG.IsDataExists("EmpPosition", strSqlWhere) Then
                            strErrMsg = strErrMsg & "==> " & " [員工職位檔]資料已存在，請勿重複新增"
                            bolCheckData = False
                        End If

                        '職位代碼
                        If objHR.IsRankIDMapFlag(Trim(myDataRead(nintCompID).ToString)) Then
                            If Trim(myDataRead(nintPositionID).ToString) = "" Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintPositionID).ToString) & " 員工職位代碼不可空白"
                                bolCheckData = False
                            End If
                        End If
                        If Trim(myDataRead(nintPositionID).ToString) <> "" Then
                            strSqlWhere = ""
                            strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                            strSqlWhere = strSqlWhere & " and PositionID =" & Bsp.Utility.Quote(Trim(myDataRead(nintPositionID).ToString))
                            If Not objRG.IsDataExists("Position", strSqlWhere) Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintPositionID).ToString) & " 員工職位代碼不存在"
                                bolCheckData = False
                            End If
                        End If

                        '事業群單位代號
                        'If Trim(myDataRead(nintGroupID).ToString) = "" Then
                        '    strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintGroupID).ToString) & " 事業群單位代號未輸入!"
                        '    bolCheckData = False
                        'Else
                        '    strSqlWhere = ""
                        '    strSqlWhere = strSqlWhere & " and OrganID = GroupID "
                        '    strSqlWhere = strSqlWhere & " and OrganID =" & Bsp.Utility.Quote(Trim(myDataRead(nintGroupID).ToString))
                        '    If Not objRG.IsDataExists("OrganizationFlow", strSqlWhere) Then
                        '        strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintGroupID).ToString) & " 事業群單位代碼不存在"
                        '        bolCheckData = False
                        '    End If
                        'End If

                        '部門代碼
                        If Trim(myDataRead(nintDeptID).ToString) = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintDeptID).ToString) & " 部門代碼未輸入!"
                            bolCheckData = False
                        Else
                            strSqlWhere = ""
                            strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                            strSqlWhere = strSqlWhere & " and OrganID =" & Bsp.Utility.Quote(Trim(myDataRead(nintDeptID).ToString))
                            If Not objRG.IsDataExists("Organization", strSqlWhere) Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintDeptID).ToString) & " 部門代碼不存在"
                                bolCheckData = False
                            End If
                        End If

                        '科/組/課代碼
                        If Trim(myDataRead(nintOrganID).ToString) = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintOrganID).ToString) & " 科/組/課代碼未輸入!"
                            bolCheckData = False
                        Else
                            strSqlWhere = ""
                            strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                            strSqlWhere = strSqlWhere & " and OrganID =" & Bsp.Utility.Quote(Trim(myDataRead(nintOrganID).ToString))
                            strSqlWhere = strSqlWhere & " and DeptID =" & Bsp.Utility.Quote(Trim(myDataRead(nintDeptID).ToString))
                            If Not objRG.IsDataExists("Organization", strSqlWhere) Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintOrganID).ToString) & " 科/組/課代碼不存在"
                                bolCheckData = False
                            End If
                        End If

                        '職等職稱代碼
                        Dim RankIDTitleIDFlag As Boolean = True
                        If Trim(myDataRead(nintRankID).ToString) = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintRankID).ToString) & " 職等未輸入!"
                            bolCheckData = False
                            RankIDTitleIDFlag = False
                        End If
                        If Trim(myDataRead(nintTitleID).ToString) = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintTitleID).ToString) & " 職稱未輸入!"
                            bolCheckData = False
                            RankIDTitleIDFlag = False
                        End If
                        If RankIDTitleIDFlag Then
                            strSqlWhere = ""
                            strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                            strSqlWhere = strSqlWhere & " and RankID =" & Bsp.Utility.Quote(Trim(myDataRead(nintRankID).ToString))
                            strSqlWhere = strSqlWhere & " and TitleID =" & Bsp.Utility.Quote(Trim(myDataRead(nintTitleID).ToString))
                            If Not objRG.IsDataExists("Title", strSqlWhere) Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintRankID).ToString) & " " & Trim(myDataRead(nintTitleID).ToString) & " 職等職稱代碼不存在"
                                bolCheckData = False
                            End If
                        End If

                        '金控職等
                        If Trim(myDataRead(nintHoldingRankID).ToString) = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintHoldingRankID).ToString) & " 金控職等代碼未輸入!"
                            bolCheckData = False
                        Else
                            strSqlWhere = ""
                            strSqlWhere = strSqlWhere & " and HoldingRankID =" & Bsp.Utility.Quote(Trim(myDataRead(nintHoldingRankID).ToString))
                            If Not objRG.IsDataExists("CompareRank", strSqlWhere) Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintHoldingRankID).ToString) & " 金控職等代碼不存在"
                                bolCheckData = False
                            End If
                        End If

                        '對外職稱代碼
                        If Trim(myDataRead(nintPublicTitleID).ToString) <> "" Then
                            strSqlWhere = ""
                            strSqlWhere = strSqlWhere & " and PublicTitleID =" & Bsp.Utility.Quote(Trim(myDataRead(nintPublicTitleID).ToString))
                            If Not objRG.IsDataExists("PublicTitle", strSqlWhere) Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintPublicTitleID).ToString) & " 對外職稱代碼不存在"
                                bolCheckData = False
                            End If
                        End If

                        '檢查是否已有員工退休金主檔資料
                        strSqlWhere = ""
                        strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                        strSqlWhere = strSqlWhere & " and EmpID =" & Bsp.Utility.Quote(Trim(myDataRead(nintEmpID).ToString))
                        If objRG.IsDataExists("EmpRetire", strSqlWhere) Then
                            strErrMsg = strErrMsg & "==> " & " [員工退休金主檔]資料已存在，請勿重複新增"    '2015/11/18 Modify 修改錯誤訊息
                            bolCheckData = False
                        End If

                        '公司到職日nintEmpDate
                        If Trim(myDataRead(nintEmpDate).ToString) <> "" Then
                            If Not IsDate(Trim(myDataRead(nintEmpDate).ToString)) Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintEmpDate).ToString) & " [公司到職日]請輸入西曆YYYY/MM/DD"
                                bolCheckData = False
                            End If
                        End If

                        '試用考核月份
                        If Trim(myDataRead(nintProbMonth).ToString) = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintProbMonth).ToString) & " 試用考核月份未輸入!"
                            bolCheckData = False
                        Else
                            If Trim(myDataRead(nintProbMonth).ToString) < 0 Or Trim(myDataRead(nintProbMonth).ToString) > 6 Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintProbMonth).ToString) & " [試用考核月份]請輸入數字0~6"
                                bolCheckData = False
                            End If
                        End If

                        '新員招考註記
                        If Trim(myDataRead(nintPassExamFlag).ToString) = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintPassExamFlag).ToString) & " 新員招考註記未輸入!"
                            bolCheckData = False
                        Else
                            If Trim(myDataRead(nintPassExamFlag).ToString) <> "1" And Trim(myDataRead(nintPassExamFlag).ToString) <> "0" Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintPassExamFlag).ToString) & " 新員招考註記1新員招考(銀行自行招考)，0 非新員招考(透過人力公司)"
                                bolCheckData = False
                            End If
                        End If

                        '檢查信用卡員工類別
                        If Trim(myDataRead(nintCompID).ToString) = "SPHCR1" Then
                            If objHR.funGetCheckInFileCompID(Trim(myDataRead(nintCompID).ToString)) = "0" Then
                                strErrMsg = strErrMsg & "==> " & " 信用卡員工類別(分A類D類人員，用途For繳交文件)不可空白"
                                bolCheckData = False
                            End If
                        End If

                        '戶籍地址縣市別nintRegCityCode
                        If Trim(myDataRead(nintRegCityCode).ToString) = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintRegCityCode).ToString) & " 戶籍地址縣市別不可空白"
                            bolCheckData = False
                        End If
                        If Trim(myDataRead(nintRegCityCode).ToString).Length > 10 Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintRegCityCode).ToString) & " 戶籍地址縣市別最多10個字!"
                            bolCheckData = False
                        End If

                        '戶籍地址區別nintRegAddrCode
                        If Trim(myDataRead(nintRegAddrCode).ToString) = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintRegAddrCode).ToString) & " 戶籍地址區別不可空白"
                            bolCheckData = False
                        End If
                        If Trim(myDataRead(nintRegAddrCode).ToString).Length > 5 Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintRegAddrCode).ToString) & " 戶籍地址區別最多5個數字!"
                            bolCheckData = False
                        End If

                        '戶籍地址nintRegAddr
                        If Trim(myDataRead(nintRegAddr).ToString) = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintRegAddr).ToString) & " 戶籍地址不可空白"
                            bolCheckData = False
                        End If
                        If Trim(myDataRead(nintRegAddr).ToString).Length > 100 Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintRegAddr).ToString) & " 戶籍地址最多100個字!"
                            bolCheckData = False
                        End If

                        '通訊地址縣市別nintCommCityCode
                        If Trim(myDataRead(nintCommCityCode).ToString) = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCommCityCode).ToString) & " 通訊地址縣市別不可空白"
                            bolCheckData = False
                        End If
                        If Trim(myDataRead(nintCommCityCode).ToString).Length > 10 Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCommCityCode).ToString) & " 通訊地址縣市別最多10個字!"
                            bolCheckData = False
                        End If

                        '通訊地址區別nintCommAddrCode
                        If Trim(myDataRead(nintCommAddrCode).ToString) = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCommAddrCode).ToString) & " 通訊地址區別不可空白"
                            bolCheckData = False
                        End If
                        If Trim(myDataRead(nintCommAddrCode).ToString).Length > 5 Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCommAddrCode).ToString) & " 通訊地址區別最多5個數字!"
                            bolCheckData = False
                        End If

                        '通訊地址nintCommAddr
                        If Trim(myDataRead(nintCommAddr).ToString) = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCommAddr).ToString) & " 通訊地址不可空白"
                            bolCheckData = False
                        End If
                        If Trim(myDataRead(nintCommAddr).ToString).Length > 100 Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCommAddr).ToString) & " 通訊地址最多100個字!"
                            bolCheckData = False
                        End If

                        '聯絡電話一國碼nintCommTelCode1,聯絡電話一nintCommTel1
                        If (Trim(myDataRead(nintCommTelCode1).ToString) = "" And Trim(myDataRead(nintCommTel1).ToString) <> "") Or (Trim(myDataRead(nintCommTelCode1).ToString) <> "" And Trim(myDataRead(nintCommTel1).ToString) = "") Then
                            strErrMsg = strErrMsg & "==> " & " 聯絡電話1(戶籍電話)：戶籍電話國碼或戶籍電話不得為空白(警告，仍可上傳)"
                            bolCheckWarning = False
                        End If
                        If Trim(myDataRead(nintCommTelCode1).ToString).Length > 6 Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCommTel1).ToString) & " 聯絡電話1(戶籍電話)：戶籍電話國碼最多6個字!"
                            bolCheckData = False
                        End If
                        If Trim(myDataRead(nintCommTel1).ToString).Length > 20 Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCommTel1).ToString) & " 聯絡電話1(戶籍電話)：戶籍電話最多20個字!"
                            bolCheckData = False
                        End If
                        If Trim(myDataRead(nintCommTelCode1).ToString) <> "" And Not objHR.funCheckStr(Trim(myDataRead(nintCommTelCode1).ToString)) Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCommTelCode1).ToString) & " 聯絡電話1(戶籍電話)：戶籍電話國碼只能為數字(警告，上傳值改為空白)"
                            bolCheckWarning = False
                        End If
                        If Trim(myDataRead(nintCommTel1).ToString) <> "" And Not objHR.funCheckStr(Trim(myDataRead(nintCommTel1).ToString).Replace("-", "")) Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCommTel1).ToString) & " 聯絡電話1(戶籍電話)：戶籍電話只能為數字(警告，上傳值改為空白)"
                            bolCheckWarning = False
                        End If
                        If Trim(myDataRead(nintCommTelCode1).ToString) <> "" And Trim(myDataRead(nintCommTel1).ToString) = "866" Then
                            If Trim(myDataRead(nintCommTel1).ToString).Length > 10 Or Trim(myDataRead(nintCommTel1).ToString).Length < 9 Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCommTel1).ToString) & " 聯絡電話1(戶籍電話)：戶籍電話含區碼長度需為9~10碼(警告，仍可上傳)"
                                bolCheckWarning = False
                            End If
                        ElseIf Left(Trim(myDataRead(nintCommTel1).ToString), 1) <> "0" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCommTel1).ToString) & " 聯絡電話1(戶籍電話)：戶籍電話請加區域碼為開頭(警告，仍可上傳)"
                            bolCheckWarning = False
                        End If



                        '聯絡電話二國碼nintCommTelCode2,聯絡電話二nintCommTel2
                        If Trim(myDataRead(nintCommTelCode2).ToString) = "" Or Trim(myDataRead(nintCommTel2).ToString) = "" Then
                            strErrMsg = strErrMsg & "==> " & " 聯絡電話2(通訊電話)：通訊電話國碼及通訊電話不得為空白(警告，仍可上傳)"
                            bolCheckWarning = False
                        End If
                        If Trim(myDataRead(nintCommTelCode2).ToString).Length > 6 Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCommTelCode2).ToString) & " 聯絡電話2(通訊電話)：通訊電話國碼最多6個字!"
                            bolCheckData = False
                        End If
                        If Trim(myDataRead(nintCommTel2).ToString).Length > 20 Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCommTel2).ToString) & " 聯絡電話2(通訊電話)：通訊電話最多20個字!"
                            bolCheckData = False
                        End If
                        If Trim(myDataRead(nintCommTelCode2).ToString) <> "" And Not objHR.funCheckStr(Trim(myDataRead(nintCommTelCode2).ToString)) Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCommTelCode2).ToString) & " 聯絡電話2(通訊電話)：通訊電話國碼只能為數字(警告，上傳值改為空白)"
                            bolCheckWarning = False
                        End If
                        If Trim(myDataRead(nintCommTel2).ToString) <> "" And Not objHR.funCheckStr(Trim(myDataRead(nintCommTel2).ToString).Replace("-", "")) Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCommTel2).ToString) & " 聯絡電話2(通訊電話)：通訊電話只能為數字(警告，上傳值改為空白)"
                            bolCheckWarning = False
                        End If
                        If Trim(myDataRead(nintCommTelCode2).ToString) <> "" And Trim(myDataRead(nintCommTel2).ToString) = "866" Then
                            If Trim(myDataRead(nintCommTel2).ToString).Length > 10 Or Trim(myDataRead(nintCommTel2).ToString).Length < 9 Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCommTel2).ToString) & " 聯絡電話2(通訊電話)：通訊電話含區碼長度需為9~10碼(警告，仍可上傳)"
                                bolCheckWarning = False
                            End If
                        ElseIf Left(Trim(myDataRead(nintCommTel2).ToString), 1) <> "0" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCommTel2).ToString) & " 聯絡電話2(通訊電話)：通訊電話請加區域碼為開頭(警告，仍可上傳)"
                            bolCheckWarning = False
                        End If

                        '聯絡電話三國碼nintCommTelCode3,聯絡電話三nintCommTel3
                        If Trim(myDataRead(nintCommTelCode3).ToString) = "" Or Trim(myDataRead(nintCommTel3).ToString) = "" Then
                            strErrMsg = strErrMsg & "==> " & " 聯絡電話3(行動電話1)：行動電話國碼及行動電話不得為空白(警告，仍可上傳)"
                            bolCheckWarning = False
                        End If
                        If Trim(myDataRead(nintCommTelCode3).ToString).Length > 6 Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCommTelCode3).ToString) & " 聯絡電話3(行動電話1)：行動電話國碼最多6個字!"
                            bolCheckData = False
                        End If
                        If Trim(myDataRead(nintCommTel3).ToString).Length > 20 Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCommTel3).ToString) & " 聯絡電話3(行動電話1)：行動電話最多20個字!"
                            bolCheckData = False
                        End If
                        If Trim(myDataRead(nintCommTelCode3).ToString) <> "" And Not objHR.funCheckStr(Trim(myDataRead(nintCommTelCode3).ToString)) Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCommTelCode3).ToString) & " 聯絡電話3(行動電話1)：行動電話國碼只能為數字(警告，上傳值改為空白)"
                            bolCheckWarning = False
                        End If
                        If Trim(myDataRead(nintCommTel3).ToString) <> "" And Not objHR.funCheckStr(Trim(myDataRead(nintCommTel3).ToString)) Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCommTel3).ToString) & " 聯絡電話3(行動電話1)：行動電話只能為數字(警告，上傳值改為空白)"
                            bolCheckWarning = False
                        End If
                        If Trim(myDataRead(nintCommTelCode3).ToString) <> "" And Trim(myDataRead(nintCommTel3).ToString) = "866" Then
                            If Trim(myDataRead(nintCommTel3).ToString).Length <> 10 Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCommTel3).ToString) & " 聯絡電話3(行動電話1)：行動電話含區碼不等於10碼(警告，仍然可以上傳)"
                                bolCheckWarning = False
                            End If
                        ElseIf Left(Trim(myDataRead(nintCommTel3).ToString), 2) <> "09" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCommTel3).ToString) & " 聯絡電話3(行動電話1)：行動電話前二碼需為09(警告，仍然可以上傳)"
                            bolCheckWarning = False
                        End If

                        '聯絡電話四國碼nintCommTelCode4,聯絡電話四nintCommTel4
                        If (Trim(myDataRead(nintCommTelCode4).ToString) = "" And Trim(myDataRead(nintCommTel4).ToString) <> "") Or (Trim(myDataRead(nintCommTelCode4).ToString) <> "" And Trim(myDataRead(nintCommTel4).ToString) = "") Then
                            strErrMsg = strErrMsg & "==> " & " 聯絡電話4(行動電話2)：行動電話國碼或行動電話不得為空白(警告，仍可上傳)"
                            bolCheckWarning = False
                        End If
                        If Trim(myDataRead(nintCommTelCode4).ToString).Length > 6 Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCommTelCode4).ToString) & " 聯絡電話4(行動電話2)：行動電話國碼最多6個字!"
                            bolCheckData = False
                        End If
                        If Trim(myDataRead(nintCommTel4).ToString).Length > 20 Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCommTel4).ToString) & " 聯絡電話4(行動電話2)：行動電話最多20個字!"
                            bolCheckData = False
                        End If
                        If Trim(myDataRead(nintCommTelCode4).ToString) <> "" And Not objHR.funCheckStr(Trim(myDataRead(nintCommTelCode4).ToString)) Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCommTelCode4).ToString) & " 聯絡電話4(行動電話2)：行動電話國碼或行動電話不得為空白(警告，仍可上傳)"
                            bolCheckWarning = False
                        End If
                        If Trim(myDataRead(nintCommTel4).ToString) <> "" And Not objHR.funCheckStr(Trim(myDataRead(nintCommTel4).ToString).Replace("-", "")) Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCommTel4).ToString) & " 聯絡電話4(行動電話2)：行動電話只能為數字(警告，上傳值改為空白)"
                            bolCheckWarning = False
                        End If
                        If Trim(myDataRead(nintCommTelCode4).ToString) <> "" And Trim(myDataRead(nintCommTel4).ToString) = "866" Then
                            If Trim(myDataRead(nintCommTel4).ToString).Length <> 10 Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCommTel4).ToString) & " 聯絡電話4(行動電話2)：行動電話含區碼不等於10碼(警告，仍然可以上傳)"
                                bolCheckWarning = False
                            End If
                        ElseIf Left(Trim(myDataRead(nintCommTel4).ToString), 2) <> "09" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCommTel4).ToString) & " 聯絡電話4(行動電話2)：行動電話前二碼需為09(警告，仍然可以上傳)"
                            bolCheckWarning = False
                        End If

                        '緊急聯絡人關係
                        If Trim(myDataRead(nintRelRelation).ToString).Length > 2 Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintRelRelation).ToString) & " 緊急聯絡人關係最多2個字!"
                            bolCheckData = False
                        End If

                        '班別
                        If Trim(myDataRead(nintWTID).ToString).Length > 2 Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintWTID).ToString) & " 班別最多2個字!"
                            bolCheckData = False
                        End If

                        '類別
                        If Trim(myDataRead(nintEmpType1).ToString).Length > 1 Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintEmpType1).ToString) & " 類別最多1個字!"
                            bolCheckData = False
                        End If

                        '應試者編號,預計報到日
                        If Trim(myDataRead(nintRecID).ToString) <> "" Or Trim(myDataRead(nintCheckInDate).ToString) <> "" Then
                            If Trim(myDataRead(nintCheckInDate).ToString) <> "" Then
                                If Not IsDate(Trim(myDataRead(nintCheckInDate).ToString)) Then
                                    strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCheckInDate).ToString) & " [預計報到日]請輸入西曆YYYY/MM/DD"
                                    bolCheckData = False
                                End If
                            End If

                            If Trim(myDataRead(nintRecID).ToString).Length > 20 Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintRecID).ToString) & " 應試者編號最多20個字!"
                                bolCheckData = False
                            End If

                            strSqlWhere = ""
                            strSqlWhere = strSqlWhere & " and CompID = " & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                            strSqlWhere = strSqlWhere & " and RecID = " & Bsp.Utility.Quote(Trim(myDataRead(nintRecID).ToString))
                            strSqlWhere = strSqlWhere & " and ContractDate = " & Bsp.Utility.Quote(Trim(myDataRead(nintCheckInDate).ToString))
                            Dim count As String = objRG.IsDataExistsRecruit("RE_ContractData", strSqlWhere)
                            If count = 0 Then
                                strErrMsg = strErrMsg & "==> " & " 資料不存在招募系統"
                                bolCheckData = False
                            ElseIf count > 1 Then
                                strErrMsg = strErrMsg & "==> " & " 同一應試者編號及預計報到日，存在招募系統超過一筆以上，導致系統無法判讀取得登記日期來上傳系統"
                                bolCheckData = False
                            ElseIf count = 1 Then
                                strErrMsg = strErrMsg & "==> " & " 上傳的應試者編號已有對應的員工編號" & Bsp.Utility.Quote(Trim(myDataRead(nintEmpID).ToString))
                                bolCheckData = False
                            End If
                        End If

                        '外部信箱
                        If Trim(myDataRead(nintEmail2).ToString).Length > 60 Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintEmail2).ToString) & " 外部信箱最多60個字!"
                            bolCheckData = False
                        End If

                        ''資料重複
                        'Dim IDNo As String = objHR.GetIDNo(myDataRead(nintCompID).ToString, myDataRead(nintEmpID).ToString)
                        'If IDNo <> "" And Trim(myDataRead(nintEduID).ToString) <> "" Then
                        '    strSqlWhere = ""
                        '    strSqlWhere = strSqlWhere & " and IDNo=" & Bsp.Utility.Quote(IDNo)
                        '    strSqlWhere = strSqlWhere & " and EduID=" & Bsp.Utility.Quote(Trim(myDataRead(nintEduID).ToString))
                        '    If objRG.IsDataExists("Education", strSqlWhere) Then
                        '        strErrMsg = strErrMsg & "==> 資料重複！"
                        '        bolCheckData = False
                        '    End If
                        'End If

                        '2015/11/19 Add 新增檢核：是否已有[人事主檔]資料
                        strSqlWhere = ""
                        strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                        strSqlWhere = strSqlWhere & " and EmpID =" & Bsp.Utility.Quote(Trim(myDataRead(nintEmpID).ToString))
                        If objRG.IsDataExists("Personal", strSqlWhere) Then
                            strErrMsg = strErrMsg & "==> " & " [人事主檔]資料已存在，請勿重複新增"
                            bolCheckData = False
                        End If

                        '2015/11/19 Add 新增檢核：是否已有[員工薪資資料檔]資料
                        strSqlWhere = ""
                        strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                        strSqlWhere = strSqlWhere & " and EmpID =" & Bsp.Utility.Quote(Trim(myDataRead(nintEmpID).ToString))
                        If objRG.IsDataExists("SalaryData", strSqlWhere) Then
                            strErrMsg = strErrMsg & "==> " & " [員工薪資資料檔]資料已存在，請勿重複新增"
                            bolCheckData = False
                        End If

                        '2015/11/19 Add 新增檢核：是否已有[通訊資料檔]資料
                        strSqlWhere = ""
                        strSqlWhere = strSqlWhere & " and IDNo =" & Bsp.Utility.Quote(Trim(myDataRead(nintIDNo).ToString))
                        If objRG.IsDataExists("Communication", strSqlWhere) Then
                            strErrMsg = strErrMsg & "==> " & " [通訊資料檔]資料已存在，請勿重複新增"
                            bolCheckData = False
                        End If

                        '2015/11/19 Add 新增檢核：是否已有[員工特殊設定資料檔]資料
                        strSqlWhere = ""
                        strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                        strSqlWhere = strSqlWhere & " and EmpID =" & Bsp.Utility.Quote(Trim(myDataRead(nintEmpID).ToString))
                        If objRG.IsDataExists("PersonalOther", strSqlWhere) Then
                            strErrMsg = strErrMsg & "==> " & " [員工特殊設定資料檔]資料已存在，請勿重複新增"
                            bolCheckData = False
                        End If

                        '2015/11/19 Add 新增檢核：是否已有[銀行員工報到文件檔],[證券員工報到文件檔]資料
                        If Trim(myDataRead(nintCompID).ToString) = "SPHBK1" Then
                            strSqlWhere = ""
                            strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                            strSqlWhere = strSqlWhere & " and EmpID =" & Bsp.Utility.Quote(Trim(myDataRead(nintEmpID).ToString))
                            If objRG.IsDataExists("CheckInFile_SPHBK1", strSqlWhere) Then
                                strErrMsg = strErrMsg & "==> " & " [銀行員工報到文件檔]資料已存在，請勿重複新增"
                                bolCheckData = False
                            End If
                        ElseIf Trim(myDataRead(nintCompID).ToString) = "SPHSC1" Then
                            strSqlWhere = ""
                            strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                            strSqlWhere = strSqlWhere & " and EmpID =" & Bsp.Utility.Quote(Trim(myDataRead(nintEmpID).ToString))
                            If objRG.IsDataExists("CheckInFile_SPHSC1", strSqlWhere) Then
                                strErrMsg = strErrMsg & "==> " & " [證券員工報到文件檔]資料已存在，請勿重複新增"
                                bolCheckData = False
                            End If
                        End If

                        '2015/11/19 Add 新增檢核：是否已有[員工異動記錄檔]資料
                        strSqlWhere = ""
                        strSqlWhere = strSqlWhere & " and IDNo =" & Bsp.Utility.Quote(Trim(myDataRead(nintIDNo).ToString))
                        strSqlWhere = strSqlWhere & " and ModifyDate =" & Bsp.Utility.Quote(Trim(myDataRead(nintEmpDate).ToString))
                        strSqlWhere = strSqlWhere & " and Reason = '01'"
                        If objRG.IsDataExists("EmployeeLog", strSqlWhere) Then
                            strErrMsg = strErrMsg & "==> " & " [員工異動記錄檔]資料已存在，請勿重複新增"
                            bolCheckData = False
                        End If

                        '2015/11/19 Add 新增檢核：是否已有[員工職等年資紀錄檔]資料
                        strSqlWhere = ""
                        strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                        strSqlWhere = strSqlWhere & " and EmpID =" & Bsp.Utility.Quote(Trim(myDataRead(nintEmpID).ToString))
                        strSqlWhere = strSqlWhere & " and RankID =" & Bsp.Utility.Quote(Trim(myDataRead(nintRankID).ToString))
                        strSqlWhere = strSqlWhere & " and ValidDateB =" & Bsp.Utility.Quote(Trim(myDataRead(nintEmpDate).ToString))
                        If objRG.IsDataExists("EmpSenRank", strSqlWhere) Then
                            strErrMsg = strErrMsg & "==> " & " [員工職等年資紀錄檔]資料已存在，請勿重複新增"
                            bolCheckData = False
                        End If

                        '2015/11/19 Add 新增檢核：是否已有[員工單位年資紀錄檔]資料
                        Dim OrganizationTable As DataTable
                        Dim strOrgType As String = ""
                        OrganizationTable = objRG.QueryOrganization(myDataRead(nintOrganID).ToString, myDataRead(nintDeptID).ToString, Trim(myDataRead(nintCompID).ToString))
                        If OrganizationTable.Rows().Count <> 0 Then
                            strOrgType = OrganizationTable.Rows(0).Item(0).ToString()
                        End If

                        strSqlWhere = ""
                        strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                        strSqlWhere = strSqlWhere & " and EmpID =" & Bsp.Utility.Quote(Trim(myDataRead(nintEmpID).ToString))
                        strSqlWhere = strSqlWhere & " and OrgType =" & Bsp.Utility.Quote(strOrgType)
                        strSqlWhere = strSqlWhere & " and ValidDateB =" & Bsp.Utility.Quote(Trim(myDataRead(nintEmpDate).ToString))
                        strSqlWhere = strSqlWhere & " and Reason = '01'"
                        If objRG.IsDataExists("EmpSenOrgType", strSqlWhere) Then
                            strErrMsg = strErrMsg & "==> " & " [員工單位年資紀錄檔]資料已存在，請勿重複新增"
                            bolCheckData = False
                        End If

                        '2015/11/19 Add 新增檢核：是否已有[員工工作性質年資紀錄檔]資料
                        strSqlWhere = ""
                        strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                        strSqlWhere = strSqlWhere & " and EmpID =" & Bsp.Utility.Quote(Trim(myDataRead(nintEmpID).ToString))
                        strSqlWhere = strSqlWhere & " and WorkTypeID =" & Bsp.Utility.Quote(Trim(myDataRead(nintWorkTypeID).ToString))
                        strSqlWhere = strSqlWhere & " and ValidDateB =" & Bsp.Utility.Quote(Trim(myDataRead(nintEmpDate).ToString))
                        strSqlWhere = strSqlWhere & " and Reason = '01'"
                        If objRG.IsDataExists("EmpSenWorkType", strSqlWhere) Then
                            strErrMsg = strErrMsg & "==> " & " [員工工作性質年資紀錄檔]資料已存在，請勿重複新增"
                            bolCheckData = False
                        End If

                        '2015/11/19 Add 新增檢核：是否已有[員工簽核單位年資紀錄檔]資料
                        strSqlWhere = ""
                        strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                        strSqlWhere = strSqlWhere & " and EmpID =" & Bsp.Utility.Quote(Trim(myDataRead(nintEmpID).ToString))
                        strSqlWhere = strSqlWhere & " and OrgType =" & Bsp.Utility.Quote(Trim(myDataRead(nintDeptID).ToString))
                        strSqlWhere = strSqlWhere & " and ValidDateB =" & Bsp.Utility.Quote(Trim(myDataRead(nintEmpDate).ToString))
                        If objRG.IsDataExists("EmpSenOrgTypeFlow", strSqlWhere) Then
                            strErrMsg = strErrMsg & "==> " & " [員工簽核單位年資紀錄檔]資料已存在，請勿重複新增"
                            bolCheckData = False
                        End If

                        '2015/11/19 Add 新增檢核：是否已有[職位年資檔]資料
                        If objHR.IsRankIDMapFlag(Trim(myDataRead(nintCompID).ToString)) Then
                            strSqlWhere = ""
                            strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                            strSqlWhere = strSqlWhere & " and EmpID =" & Bsp.Utility.Quote(Trim(myDataRead(nintEmpID).ToString))
                            strSqlWhere = strSqlWhere & " and PositionID =" & Bsp.Utility.Quote(Trim(myDataRead(nintPositionID).ToString))
                            strSqlWhere = strSqlWhere & " and ValidDateB =" & Bsp.Utility.Quote(Trim(myDataRead(nintEmpDate).ToString))
                            If objRG.IsDataExists("EmpSenPosition", strSqlWhere) Then
                                strErrMsg = strErrMsg & "==> " & " [職位年資檔]資料已存在，請勿重複新增"
                                bolCheckData = False
                            End If
                        End If

                        '2016/01/28 Add 新增檢核：是否已有[員工公司年資記錄檔]資料
                        strSqlWhere = ""
                        strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                        strSqlWhere = strSqlWhere & " and EmpID =" & Bsp.Utility.Quote(Trim(myDataRead(nintEmpID).ToString))
                        strSqlWhere = strSqlWhere & " and IDNo =" & Bsp.Utility.Quote(Trim(myDataRead(nintIDNo).ToString))
                        strSqlWhere = strSqlWhere & " and ValidDateB =" & Bsp.Utility.Quote(Trim(myDataRead(nintEmpDate).ToString))
                        If objRG.IsDataExists("EmpSenComp", strSqlWhere) Then
                            strErrMsg = strErrMsg & "==> " & " [員工公司年資記錄檔]資料已存在，請勿重複新增"
                            bolCheckData = False
                        End If

                        If bolCheckData Then
                            Dim ErrMsg As String = ""
                            If funUploadDataSingle_Personal(myDataRead, ErrMsg) Then
                                intSuccessCount = intSuccessCount + 1
                            Else
                                intErrorCount = intErrorCount + 1
                            End If

                            If ErrMsg <> "" Then
                                strErrMsg = strErrMsg & "==> " & ErrMsg
                            End If
                        Else
                            intErrorCount = intErrorCount + 1
                        End If

                        '警告筆數
                        If Not bolCheckWarning Then
                            intTotalWarning = intTotalWarning + 1
                        End If

                    End If

                    If strErrMsg <> "" Then
                        subWriteLog("資料列:" & intTotalCount + 1 _
                                    & " 公司代碼：" & Trim(myDataRead(nintCompID).ToString) _
                                    & " 員工編號：" & Trim(myDataRead(nintEmpID).ToString) _
                                    & " 身份證字號：" & Trim(myDataRead(nintIDNo).ToString) _
                                    & " 中文姓名難字：" & Trim(myDataRead(nintNameN).ToString) _
                                    & " 中文姓名：" & Trim(myDataRead(nintName).ToString) _
                                    & " 英文姓名：" & Trim(myDataRead(nintEngName).ToString) _
                                    & " 護照英文姓名：" & Trim(myDataRead(nintPassportName).ToString) _
                                    & " 出生日期：" & Trim(myDataRead(nintBirthDate).ToString) _
                                    & " 性別：" & Trim(myDataRead(nintSex).ToString) _
                                    & " 員工身分別：" & Trim(myDataRead(nintNationID).ToString) _
                                    & " 證件類型：" & Trim(myDataRead(nintIDType).ToString) _
                                    & " 工作證期限：" & Trim(myDataRead(nintIDExpireDate).ToString) _
                                    & " 學歷代碼：" & Trim(myDataRead(nintEduID).ToString) _
                                    & " 婚姻狀況：" & Trim(myDataRead(nintMarriage).ToString) _
                                    & " 僱用類別：" & Trim(myDataRead(nintEmpType).ToString) _
                                    & " 工作性質代碼：" & Trim(myDataRead(nintWorkTypeID).ToString) _
                                    & " 部門代碼：" & Trim(myDataRead(nintDeptID).ToString) _
                                    & " 科/組/課代碼：" & Trim(myDataRead(nintOrganID).ToString) _
                                    & " 職等代碼：" & Trim(myDataRead(nintRankID).ToString) _
                                    & " 金控職等：" & Trim(myDataRead(nintHoldingRankID).ToString) _
                                    & " 職稱代碼：" & Trim(myDataRead(nintTitleID).ToString) _
                                    & " 對外職稱代碼：" & Trim(myDataRead(nintPublicTitleID).ToString) _
                                    & " 公司到職日：" & Trim(myDataRead(nintEmpDate).ToString) _
                                    & " 試用考核月份：" & Trim(myDataRead(nintProbMonth).ToString) _
                                    & " 戶籍地址縣市別：" & Trim(myDataRead(nintRegCityCode).ToString) _
                                    & " 戶籍地址區別：" & Trim(myDataRead(nintRegAddrCode).ToString) _
                                    & " 戶籍地址：" & Trim(myDataRead(nintRegAddr).ToString) _
                                    & " 通訊地址縣市別：" & Trim(myDataRead(nintCommCityCode).ToString) _
                                    & " 通訊地址區別：" & Trim(myDataRead(nintCommAddrCode).ToString) _
                                    & " 通訊地址：" & Trim(myDataRead(nintCommAddr).ToString) _
                                    & " 聯絡電話一國碼：" & Trim(myDataRead(nintCommTelCode1).ToString) _
                                    & " 聯絡電話一：" & Trim(myDataRead(nintCommTel1).ToString) _
                                    & " 聯絡電話二國碼：" & Trim(myDataRead(nintCommTelCode2).ToString) _
                                    & " 聯絡電話二：" & Trim(myDataRead(nintCommTel2).ToString) _
                                    & " 聯絡電話三國碼：" & Trim(myDataRead(nintCommTelCode3).ToString) _
                                    & " 聯絡電話三：" & Trim(myDataRead(nintCommTel3).ToString) _
                                    & " 聯絡電話四國碼：" & Trim(myDataRead(nintCommTelCode4).ToString) _
                                    & " 聯絡電話四：" & Trim(myDataRead(nintCommTel4).ToString) _
                                    & " 緊急聯絡人：" & Trim(myDataRead(nintRelName).ToString) _
                                    & " 緊急聯絡人電話：" & Trim(myDataRead(nintRelTel).ToString) _
                                    & " 緊急聯絡人關係：" & Trim(myDataRead(nintRelRelation).ToString) _
                                    & " 新員招考註記：" & Trim(myDataRead(nintPassExamFlag).ToString) _
                                    & " 班別：" & Trim(myDataRead(nintWTID).ToString) _
                                    & " 類別：" & Trim(myDataRead(nintEmpType1).ToString) _
                                    & " 職位代碼：" & Trim(myDataRead(nintPositionID).ToString) _
                                    & " 應試者編號：" & Trim(myDataRead(nintRecID).ToString) _
                                    & " 預計報到日：" & Trim(myDataRead(nintCheckInDate).ToString) _
                                    & " Email2：" & Trim(myDataRead(nintEmail2).ToString) _
                                    & strErrMsg)

                        If bolCheckData = False Then
                            bolInputStatus = False
                        End If
                    End If

                    intTotalCount = intTotalCount + 1

                End While
            End Using

            myExcelConn.Close()

            If bolInputStatus = False Then
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            myExcelConn.Close()
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".funCheckData1", ex)
            Return False
        End Try
    End Function

    '資料上傳 單筆
    Private Function funUploadDataSingle_Personal(ByVal myDataRead As OleDbDataReader, ByRef ErrMsg As String) As Boolean
        Dim bsPersonal As New bePersonal.Service()
        Dim bePersonal As New bePersonal.Row
        Dim bsSalaryData As New beSalaryData.Service()
        Dim beSalaryData As New beSalaryData.Row
        Dim bsCommunication As New beCommunication.Service()
        Dim beCommunication As New beCommunication.Row
        Dim bsPersonalOther_1 As New bePersonalOther.Service()
        Dim bePersonalOther_1 As New bePersonalOther.Row
        Dim bsPersonalOther_2 As New bePersonalOther.Service()
        Dim bePersonalOther_2 As New bePersonalOther.Row
        Dim bsEmpWorkType As New beEmpWorkType.Service()
        Dim beEmpWorkType As New beEmpWorkType.Row
        Dim bsEmpPosition As New beEmpPosition.Service()
        Dim beEmpPosition As New beEmpPosition.Row
        Dim bsCheckInFile_SPHBK1 As New beCheckInFile_SPHBK1.Service()
        Dim beCheckInFile_SPHBK1 As New beCheckInFile_SPHBK1.Row
        Dim bsCheckInFile_SPHSC1 As New beCheckInFile_SPHSC1.Service()
        Dim beCheckInFile_SPHSC1 As New beCheckInFile_SPHSC1.Row
        Dim bsEmployeeLog As New beEmployeeLog.Service()
        Dim beEmployeeLog As New beEmployeeLog.Row
        Dim bsProbation As New beProbation.Service()
        Dim beProbation As New beProbation.Row
        Dim bsProbationSPHSC1 As New beProbationSPHSC1.Service()
        Dim beProbationSPHSC1 As New beProbationSPHSC1.Row
        Dim bsEmpRetire As New beEmpRetire.Service()
        Dim beEmpRetire As New beEmpRetire.Row
        Dim bsEmpSenRank As New beEmpSenRank.Service()
        Dim beEmpSenRank As New beEmpSenRank.Row
        Dim bsEmpSenOrgType As New beEmpSenOrgType.Service()
        Dim beEmpSenOrgType As New beEmpSenOrgType.Row
        Dim bsEmpSenWorkType As New beEmpSenWorkType.Service()
        Dim beEmpSenWorkType As New beEmpSenWorkType.Row
        Dim bsEmpSenOrgTypeFlow As New beEmpSenOrgTypeFlow.Service()
        Dim beEmpSenOrgTypeFlow As New beEmpSenOrgTypeFlow.Row
        Dim bsEmpSenPosition As New beEmpSenPosition.Service()
        Dim beEmpSenPosition As New beEmpSenPosition.Row
        Dim beEmpSenComp As New beEmpSenComp.Row
        Dim bsEmpSenComp As New beEmpSenComp.Service()
        Dim bsEmpRetireLog As New beEmpRetireLog.Service()
        Dim beEmpRetireLog_1 As New beEmpRetireLog.Row
        Dim beEmpRetireLog_2 As New beEmpRetireLog.Row
        Dim beEmpRetireLog_3 As New beEmpRetireLog.Row
        Dim beEmpRetireLog_4 As New beEmpRetireLog.Row

        Dim dtNow = DateTime.Now
        Dim objHR As New HR
        Dim objRG As New RG1
        Dim objRegistData As New RegistData

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()

            Dim tran As DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Dim nintCompID As Integer = 0           '公司代碼
            Dim nintEmpID As Integer = 1            '員工編號
            Dim nintIDNo As Integer = 2             '身份證字號
            Dim nintNameN As Integer = 3            '中文姓名難字
            Dim nintName As Integer = 4             '中文姓名
            Dim nintEngName As Integer = 5          '英文姓名
            Dim nintPassportName As Integer = 6     '護照英文姓名
            Dim nintBirthDate As Integer = 7        '出生日期
            Dim nintSex As Integer = 8              '性別
            Dim nintNationID As Integer = 9         '員工身分別  '2015/12/18 Modify 變更欄位名稱
            Dim nintIDType As Integer = 10          '證件類型    '2015/12/18 Add 新增欄位
            Dim nintIDExpireDate As Integer = 11    '工作證期限
            Dim nintEduID As Integer = 12           '學歷代碼
            Dim nintMarriage As Integer = 13        '婚姻狀況
            Dim nintEmpType As Integer = 14         '僱用類別
            Dim nintWorkTypeID As Integer = 15      '工作性質代碼
            'Dim nintGroupID As Integer = 16         '事業群單位代號
            Dim nintDeptID As Integer = 16          '部門代碼
            Dim nintOrganID As Integer = 17         '科/組/課代碼
            Dim nintRankID As Integer = 18          '職等代碼
            Dim nintHoldingRankID As Integer = 19   '金控職等
            Dim nintTitleID As Integer = 20         '職稱代碼
            Dim nintPublicTitleID As Integer = 21   '對外職稱代碼
            Dim nintEmpDate As Integer = 22         '公司到職日
            Dim nintProbMonth As Integer = 23       '試用考核月份
            Dim nintRegCityCode As Integer = 24     '戶籍地址縣市別
            Dim nintRegAddrCode As Integer = 25     '戶籍地址區別
            Dim nintRegAddr As Integer = 26         '戶籍地址
            Dim nintCommCityCode As Integer = 27    '通訊地址縣市別
            Dim nintCommAddrCode As Integer = 28    '通訊地址區別
            Dim nintCommAddr As Integer = 29        '通訊地址
            Dim nintCommTelCode1 As Integer = 30    '聯絡電話一國碼
            Dim nintCommTel1 As Integer = 31        '聯絡電話一
            Dim nintCommTelCode2 As Integer = 32    '聯絡電話二國碼
            Dim nintCommTel2 As Integer = 33        '聯絡電話二
            Dim nintCommTelCode3 As Integer = 34    '聯絡電話三國碼
            Dim nintCommTel3 As Integer = 35        '聯絡電話三
            Dim nintCommTelCode4 As Integer = 36    '聯絡電話四國碼
            Dim nintCommTel4 As Integer = 37        '聯絡電話四
            Dim nintRelName As Integer = 38         '緊急聯絡人
            Dim nintRelTel As Integer = 39          '緊急聯絡人電話
            Dim nintRelRelation As Integer = 40     '緊急聯絡人關係
            Dim nintPassExamFlag As Integer = 41    '新員招考註記
            Dim nintWTID As Integer = 42            '班別
            Dim nintEmpType1 As Integer = 43        '類別
            Dim nintPositionID As Integer = 44      '職位代碼
            Dim nintRecID As Integer = 45           '應試者編號
            Dim nintCheckInDate As Integer = 46     '預計報到日
            Dim nintEmail2 As Integer = 47          '外部信箱

            Dim intLoop As Integer = 0

            Dim strCompID As String = myDataRead(nintCompID).ToString
            Dim strEmpID As String = myDataRead(nintEmpID).ToString
            Dim strRankID As String = myDataRead(nintRankID).ToString
            'Dim strIDNo As String = objHR.GetIDNo(strCompID, strEmpID)
            Dim strIDNo As String = myDataRead(nintIDNo).ToString.ToUpper()
            Dim strEmpDate As String = myDataRead(nintEmpDate).ToString
            Dim strEmpType = myDataRead(nintEmpType).ToString
            Dim strCheckInFileCompID As String = objHR.funGetCheckInFileCompID(Trim(myDataRead(nintCompID).ToString))

            Try
                '1.寫入人事主檔Personal
                bePersonal.CompID.Value = strCompID
                bePersonal.EmpID.Value = strEmpID
                bePersonal.IDNo.Value = strIDNo
                bePersonal.NameN.Value = myDataRead(nintNameN).ToString
                bePersonal.Name.Value = myDataRead(nintName).ToString
                bePersonal.EngName.Value = myDataRead(nintEngName).ToString
                bePersonal.PassportName.Value = myDataRead(nintPassportName).ToString
                bePersonal.BirthDate.Value = myDataRead(nintBirthDate).ToString
                bePersonal.Sex.Value = myDataRead(nintSex).ToString
                bePersonal.NationID.Value = myDataRead(nintNationID).ToString
                bePersonal.IDType.Value = myDataRead(nintIDType).ToString   '2015/12/18 Add
                bePersonal.IDExpireDate.Value = IIf(myDataRead(nintIDExpireDate).ToString = "", "1900/01/01", myDataRead(nintIDExpireDate).ToString)
                bePersonal.EduID.Value = myDataRead(nintEduID).ToString
                bePersonal.Marriage.Value = myDataRead(nintMarriage).ToString
                bePersonal.WorkStatus.Value = "1"
                bePersonal.EmpType.Value = myDataRead(nintEmpType).ToString
                bePersonal.GroupID.Value = objRG.QueryData("Organization", " AND CompID = " & Bsp.Utility.Quote(Trim(strCompID)) & " AND OrganID = " & Bsp.Utility.Quote(Trim(myDataRead(nintOrganID).ToString)), "GroupID")
                bePersonal.DeptID.Value = myDataRead(nintDeptID).ToString
                bePersonal.OrganID.Value = myDataRead(nintOrganID).ToString
                bePersonal.WorkSiteID.Value = objRG.QueryData("Organization", " AND CompID = " & Bsp.Utility.Quote(Trim(strCompID)) & " AND OrganID = " & Bsp.Utility.Quote(Trim(myDataRead(nintOrganID).ToString)), "isnull(WorkSiteID, '')")
                bePersonal.RankBeginDate.Value = strEmpDate
                bePersonal.RankID.Value = strRankID
                bePersonal.RankIDMap.Value = objHR.FunGetRankIDMap(strCompID, strRankID)
                bePersonal.HoldingRankID.Value = myDataRead(nintHoldingRankID).ToString
                bePersonal.TitleID.Value = myDataRead(nintTitleID).ToString
                bePersonal.PublicTitleID.Value = myDataRead(nintPublicTitleID).ToString
                bePersonal.EmpDate.Value = strEmpDate
                bePersonal.SinopacEmpDate.Value = strEmpDate
                '試用考核試滿日
                Dim strProbMonth As String = ""
                Dim strProbDate As String = ""
                If myDataRead(nintProbMonth).ToString <> "0" And myDataRead(nintProbMonth).ToString <> "" Then
                    strProbDate = "1900/01/01"
                    strProbMonth = myDataRead(nintProbMonth).ToString
                Else
                    strProbDate = strEmpDate
                    strProbMonth = "0"
                End If
                bePersonal.ProbDate.Value = strProbDate
                bePersonal.ProbMonth.Value = strProbMonth   '2015/11/23 Add
                bePersonal.CheckInFlag.Value = "1"
                bePersonal.PassExamFlag.Value = myDataRead(nintPassExamFlag).ToString
                'bePersonal.LastChgComp.Value = UserProfile.ActCompID
                'bePersonal.LastChgID.Value = UserProfile.ActUserID
                bePersonal.LastChgComp.Value = "RG1200"
                bePersonal.LastChgID.Value = "RG1200"
                bePersonal.LastChgDate.Value = Now

                '2.寫入員工薪資資料檔SalaryData
                beSalaryData.CompID.Value = strCompID
                beSalaryData.EmpID.Value = strEmpID
                beSalaryData.SalaryDate.Value = strEmpDate
                '福利金註記
                If strCompID = "SPHMCC" Or strCompID = "SPHOLD" Or strCompID = "SPHPIA" Or strCompID = "SPHPLA" Then
                    beSalaryData.WelfareFlag.Value = "0"
                Else
                    beSalaryData.WelfareFlag.Value = "1"
                End If
                beSalaryData.CreateUserComp.Value = "RG1200"
                beSalaryData.CreateUserID.Value = "RG1200"
                beSalaryData.CreateDate.Value = Now
                'beSalaryData.LastChgComp.Value = UserProfile.ActCompID
                'beSalaryData.LastChgID.Value = UserProfile.ActUserID
                beSalaryData.LastChgComp.Value = "RG1200"
                beSalaryData.LastChgID.Value = "RG1200"
                beSalaryData.LastChgDate.Value = Now

                '3.寫入通訊資料檔Communication
                beCommunication.IDNo.Value = strIDNo
                beCommunication.RegCityCode.Value = objRG.QueryData("PostalCode", " AND AreaCode<>'' AND AddrCode = " & Bsp.Utility.Quote(Trim(myDataRead(nintRegCityCode).ToString)), "CityCode")
                beCommunication.RegAddrCode.Value = myDataRead(nintRegAddrCode).ToString
                'beCommunication.RegAddrCode.Value = objRG.QueryData("PostalCode", " AND AddrCode = " & Bsp.Utility.Quote(Trim(myDataRead(nintRegCityCode).ToString)), "AreaCode")
                beCommunication.RegAddr.Value = myDataRead(nintRegAddr).ToString
                beCommunication.CommCityCode.Value = objRG.QueryData("PostalCode", " AND AreaCode<>'' AND AddrCode = " & Bsp.Utility.Quote(Trim(myDataRead(nintCommCityCode).ToString)), "CityCode")
                beCommunication.CommAddrCode.Value = myDataRead(nintCommAddrCode).ToString
                'beCommunication.CommAddrCode.Value = objRG.QueryData("PostalCode", " AND AddrCode = " & Bsp.Utility.Quote(Trim(myDataRead(nintCommAddrCode).ToString)), "AreaCode")
                beCommunication.CommAddr.Value = myDataRead(nintCommAddr).ToString
                beCommunication.CommTelCode1.Value = myDataRead(nintCommTelCode1).ToString
                beCommunication.CommTel1.Value = myDataRead(nintCommTel1).ToString
                beCommunication.CommTelCode2.Value = myDataRead(nintCommTelCode2).ToString
                beCommunication.CommTel2.Value = myDataRead(nintCommTel2).ToString
                beCommunication.CommTelCode3.Value = myDataRead(nintCommTelCode3).ToString
                beCommunication.CommTel3.Value = myDataRead(nintCommTel3).ToString
                beCommunication.CommTelCode4.Value = myDataRead(nintCommTelCode4).ToString
                beCommunication.CommTel4.Value = myDataRead(nintCommTel4).ToString
                beCommunication.RelName.Value = myDataRead(nintRelName).ToString
                beCommunication.RelTel.Value = myDataRead(nintRelTel).ToString
                beCommunication.RelRelation.Value = myDataRead(nintRelRelation).ToString
                beCommunication.Email2.Value = myDataRead(nintEmail2).ToString
                'beCommunication.LastChgComp.Value = UserProfile.ActCompID
                'beCommunication.LastChgID.Value = UserProfile.ActUserID
                beCommunication.LastChgComp.Value = "RG1200"
                beCommunication.LastChgID.Value = "RG1200"
                beCommunication.LastChgDate.Value = Now

                '4.若從招募而來的資料則新增員工特殊設定資料檔[PersonalOther]
                Dim strPayrollCompID As String = objHR.funGetPayrollCompID(strCompID)
                Dim strCheckInDate As String = objRG.QueryDataRecruit("RE_ContractData", " AND CompID = " & Bsp.Utility.Quote(Trim(strCompID)) & " AND RecID = " & Bsp.Utility.Quote(Trim(myDataRead(nintRecID).ToString)), " isnull(Convert(char(10), CheckInDate, 111), '1900/01/01') as CheckInDate ")   '預計報到日
                '若是證券體系，新增員工特殊設定資料檔[PersonalOther]
                If strPayrollCompID = "SPHSC1" Then
                    bePersonalOther_1.CompID.Value = strCompID
                    bePersonalOther_1.EmpID.Value = strEmpID
                    bePersonalOther_1.WTID.Value = myDataRead(nintWTID).ToString
                    bePersonalOther_1.OfficeLoginFlag.Value = "1"
                    bePersonalOther_1.RecID.Value = myDataRead(nintRecID).ToString
                    bePersonalOther_1.CheckInDate.Value = IIf(strCheckInDate = "", "1900/01/01", strCheckInDate)
                    'bePersonalOther_1.LastChgComp.Value = UserProfile.ActCompID
                    'bePersonalOther_1.LastChgID.Value = UserProfile.ActUserID
                    bePersonalOther_1.LastChgComp.Value = "RG1200"
                    bePersonalOther_1.LastChgID.Value = "RG1200"
                    bePersonalOther_1.LastChgDate.Value = Now
                Else
                    '2015/11/24 Modify 更改if條件
                    If myDataRead(nintRecID).ToString <> "" Or strCheckInDate <> "" And strCheckInFileCompID <> "SPHCR1" Then
                        bePersonalOther_1.CompID.Value = strCompID
                        bePersonalOther_1.EmpID.Value = strEmpID
                        bePersonalOther_1.RecID.Value = myDataRead(nintRecID).ToString
                        bePersonalOther_1.CheckInDate.Value = IIf(strCheckInDate = "", "1900/01/01", strCheckInDate)
                        'bePersonalOther_1.LastChgComp.Value = UserProfile.ActCompID
                        'bePersonalOther_1.LastChgID.Value = UserProfile.ActUserID
                        bePersonalOther_1.LastChgComp.Value = "RG1200"
                        bePersonalOther_1.LastChgID.Value = "RG1200"
                        bePersonalOther_1.LastChgDate.Value = Now
                    End If
                End If

                '5.若是繳交文件歸屬信用卡，新增員工特殊設定資料檔[PersonalOther]
                If strCheckInFileCompID = "SPHCR1" Then
                    bePersonalOther_2.CompID.Value = strCompID
                    bePersonalOther_2.EmpID.Value = strEmpID
                    bePersonalOther_2.EmpType1.Value = myDataRead(nintEmpType1).ToString
                    'bePersonalOther_2.LastChgComp.Value = UserProfile.ActCompID
                    'bePersonalOther_2.LastChgID.Value = UserProfile.ActUserID
                    bePersonalOther_2.LastChgComp.Value = "RG1200"
                    bePersonalOther_2.LastChgID.Value = "RG1200"
                    bePersonalOther_2.LastChgDate.Value = Now
                    'bsPersonalOther_2.Insert(bePersonalOther_2, tran)
                End If

                '6.寫入員工工作性質檔EmpWorkType
                beEmpWorkType.CompID.Value = strCompID
                beEmpWorkType.EmpID.Value = strEmpID
                beEmpWorkType.WorkTypeID.Value = myDataRead(nintWorkTypeID).ToString
                beEmpWorkType.PrincipalFlag.Value = "1"
                'beEmpWorkType.LastChgComp.Value = UserProfile.ActCompID
                'beEmpWorkType.LastChgID.Value = UserProfile.ActUserID
                beEmpWorkType.LastChgComp.Value = "RG1200"
                beEmpWorkType.LastChgID.Value = "RG1200"
                beEmpWorkType.LastChgDate.Value = Now

                '7.寫入員工職位檔EmpPosition
                beEmpPosition.CompID.Value = strCompID
                beEmpPosition.EmpID.Value = strEmpID
                beEmpPosition.PositionID.Value = myDataRead(nintPositionID).ToString
                beEmpPosition.PrincipalFlag.Value = "1"
                'beEmpPosition.LastChgComp.Value = UserProfile.ActCompID
                'beEmpPosition.LastChgID.Value = UserProfile.ActUserID
                beEmpPosition.LastChgComp.Value = "RG1200"
                beEmpPosition.LastChgID.Value = "RG1200"
                beEmpPosition.LastChgDate.Value = Now

                '8.寫入員工報到文件檔[CheckInFile_SPHBK1, CheckInFile_SPHSC1]
                If strCompID = "SPHBK1" Then
                    beCheckInFile_SPHBK1.CompID.Value = strCompID
                    beCheckInFile_SPHBK1.EmpID.Value = strEmpID
                    '健康告知書
                    If objHR.FunGetRankIDMap(strCompID, strRankID) < "07" Then
                        beCheckInFile_SPHBK1.File16.Value = "1"
                    Else
                        beCheckInFile_SPHBK1.File16.Value = "0"
                    End If
                    '退伍令
                    If myDataRead(nintSex).ToString = "2" Or myDataRead(nintNationID).ToString = "2" Then
                        beCheckInFile_SPHBK1.File9.Value = "1"
                    Else
                        beCheckInFile_SPHBK1.File9.Value = "0"
                    End If
                    beCheckInFile_SPHBK1.File14.Value = "1"
                    beCheckInFile_SPHBK1.File15.Value = "1"
                    '刑事紀錄證明
                    If Not objHR.funIsCredit(strCompID, strEmpID) Then
                        beCheckInFile_SPHBK1.File18.Value = "1"
                    Else
                        beCheckInFile_SPHBK1.File18.Value = "01"
                    End If
                    '2015/11/30 Add
                    beCheckInFile_SPHBK1.LastChgComp.Value = "RG1200"
                    beCheckInFile_SPHBK1.LastChgID.Value = "RG1200"
                    beCheckInFile_SPHBK1.LastChgDate.Value = Now

                    'bsCheckInFile_SPHBK1.Insert(beCheckInFile_SPHBK1, tran)
                ElseIf strCompID = "SPHSC1" Then
                    beCheckInFile_SPHSC1.CompID.Value = strCompID
                    beCheckInFile_SPHSC1.EmpID.Value = strEmpID
                    '退伍令影本或兵役資料影本
                    If myDataRead(nintSex).ToString = "2" Or myDataRead(nintNationID).ToString = "2" Then
                        beCheckInFile_SPHSC1.File11.Value = "1"
                    Else
                        beCheckInFile_SPHSC1.File11.Value = "0"
                    End If
                    '2015/11/30 Add
                    beCheckInFile_SPHSC1.LastChgComp.Value = "RG1200"
                    beCheckInFile_SPHSC1.LastChgID.Value = "RG1200"
                    beCheckInFile_SPHSC1.LastChgDate.Value = Now

                    'bsCheckInFile_SPHSC1.Insert(beCheckInFile_SPHSC1, tran)
                End If

                '9.寫入新增員工異動記錄檔[EmployeeLog]
                beEmployeeLog.IDNo.Value = strIDNo
                beEmployeeLog.ModifyDate.Value = myDataRead(nintEmpDate).ToString
                beEmployeeLog.Seq.Value = "1"
                beEmployeeLog.Reason.Value = "01"
                beEmployeeLog.CompID.Value = strCompID
                beEmployeeLog.EmpID.Value = strEmpID
                beEmployeeLog.Company.Value = objRG.QueryData("Company", " AND CompID = " & Bsp.Utility.Quote(Trim(strCompID)), " isnull(CompName, '') as Name ")
                beEmployeeLog.DeptID.Value = myDataRead(nintDeptID).ToString
                beEmployeeLog.DeptName.Value = objRG.QueryData("Organization", " AND CompID = " & Bsp.Utility.Quote(Trim(strCompID)) & " AND OrganID = " & Bsp.Utility.Quote(Trim(myDataRead(nintDeptID).ToString)), " isnull(OrganName, '') as Name ")
                beEmployeeLog.OrganID.Value = myDataRead(nintOrganID).ToString
                beEmployeeLog.OrganName.Value = objRG.QueryData("Organization", " AND CompID = " & Bsp.Utility.Quote(Trim(strCompID)) & " AND OrganID = " & Bsp.Utility.Quote(Trim(myDataRead(nintOrganID).ToString)), " isnull(OrganName, '') as Name ")
                beEmployeeLog.GroupID.Value = objRG.QueryData("Organization", " AND CompID = " & Bsp.Utility.Quote(Trim(strCompID)) & " AND OrganID = " & Bsp.Utility.Quote(Trim(myDataRead(nintOrganID).ToString)), "GroupID")
                beEmployeeLog.GroupName.Value = objRG.QueryData("OrganizationFlow", " AND OrganID = " & Bsp.Utility.Quote(beEmployeeLog.GroupID.Value), " isnull(OrganName, '') as Name ")   '2015/11/23 Modify 原用科組課代碼改成事業群代碼
                beEmployeeLog.RankID.Value = strRankID
                beEmployeeLog.TitleID.Value = myDataRead(nintTitleID).ToString
                beEmployeeLog.TitleName.Value = objRG.QueryData("Title", " AND CompID = " & Bsp.Utility.Quote(Trim(strCompID)) & " AND RankID = " & Bsp.Utility.Quote(Trim(strRankID)) & " AND TitleID = " & Bsp.Utility.Quote(Trim(myDataRead(nintTitleID).ToString)), " isnull(TitleName, '') AS TitleName ")
                '異動後職位代碼
                If objHR.IsRankIDMapFlag(strCompID) Then
                    beEmployeeLog.PositionID.Value = "1|" + myDataRead(nintPositionID).ToString
                End If
                beEmployeeLog.Position.Value = objRG.QueryData("Position", " AND CompID = " & Bsp.Utility.Quote(Trim(strCompID)) & " AND PositionID = " & Bsp.Utility.Quote(Trim(myDataRead(nintPositionID).ToString)), " isnull(Remark, '') as Name ")
                beEmployeeLog.WorkTypeID.Value = "1|" + myDataRead(nintWorkTypeID).ToString
                beEmployeeLog.WorkType.Value = objRG.QueryData("WorkType", " AND CompID = " & Bsp.Utility.Quote(Trim(strCompID)) & " AND WorkTypeID = " & Bsp.Utility.Quote(Trim(myDataRead(nintWorkTypeID).ToString)), " isnull(Remark, '') as Name ")
                beEmployeeLog.WorkStatus.Value = "1"
                beEmployeeLog.WorkStatusName.Value = "在職"
                'beEmployeeLog.LastChgComp.Value = UserProfile.ActCompID
                'beEmployeeLog.LastChgID.Value = UserProfile.ActUserID
                beEmployeeLog.LastChgComp.Value = "RG1200"
                beEmployeeLog.LastChgID.Value = "RG1200"
                beEmployeeLog.LastChgDate.Value = Now

                '10.寫入適用考核檔Probation
                If strPayrollCompID <> "SPHSC1" Then
                    '需試用考核
                    If myDataRead(nintProbMonth).ToString <> "0" And myDataRead(nintProbMonth).ToString <> "" And myDataRead(nintEmpType).ToString = "1" Then
                        beProbation.CompID.Value = strCompID
                        beProbation.ApplyTime.Value = Now.ToString("yyyy/MM/dd hh:mm:ss")
                        beProbation.ApplyID.Value = strEmpID
                        beProbation.ProbSeq.Value = "1"
                        beProbation.ProbDate.Value = strEmpDate
                        beProbation.ProbDate2.Value = objHR.funGetProbationEndDate(strEmpDate, myDataRead(nintProbMonth).ToString)
                        'beProbation.LastChgComp.Value = UserProfile.ActCompID
                        'beProbation.LastChgID.Value = UserProfile.ActUserID
                        beProbation.LastChgComp.Value = "RG1200"
                        beProbation.LastChgID.Value = "RG1200"
                        beProbation.LastChgDate.Value = Now
                        'bsProbation.Insert(beProbation, tran)
                    End If
                Else
                    '若歸屬證券體系ProbationSPHSC1
                    beProbationSPHSC1.CompID.Value = strCompID
                    beProbationSPHSC1.ApplyTime.Value = Now.ToString("yyyy/MM/dd hh:mm:ss")
                    beProbationSPHSC1.ApplyID.Value = strEmpID
                    beProbationSPHSC1.ProbSeq.Value = "1"
                    beProbationSPHSC1.ProbDate.Value = myDataRead(nintEmpDate).ToString
                    beProbationSPHSC1.ProbDate2.Value = objHR.funGetProbationEndDate(Bsp.Utility.CheckDate(myDataRead(nintEmpDate).ToString), myDataRead(nintProbMonth).ToString)
                    'beProbationSPHSC1.LastChgComp.Value = UserProfile.ActCompID
                    'beProbationSPHSC1.LastChgID.Value = UserProfile.ActUserID
                    beProbationSPHSC1.LastChgComp.Value = "RG1200"
                    beProbationSPHSC1.LastChgID.Value = "RG1200"
                    beProbationSPHSC1.LastChgDate.Value = Now
                    'bsProbationSPHSC1.Insert(beProbationSPHSC1, tran)
                End If

                '11.寫入員工退休金主檔 [EmpRetire]
                beEmpRetire.CompID.Value = strCompID
                beEmpRetire.EmpID.Value = strEmpID
                beEmpRetire.Kind.Value = "1"    '種類 1=勞退新制
                beEmpRetire.EmpRatio.Value = "0"    '員工自提比例
                beEmpRetire.CompRatio.Value = "6"    '公司提撥比例

                beEmpRetire.Amount.Value = objRegistData.funEncryptNumber(strEmpID, "0")    '三個月平均工資,default 0

                beEmpRetire.ManagerFlag.Value = "0"    '委任經理人註記 0=非委任經理人
                beEmpRetire.BossFlag.Value = "0"    '僱主註記 0=非僱主人
                beEmpRetire.ChangeCount.Value = "0"    '修改自提比率次數
                'beEmpRetire.LastChgComp.Value = UserProfile.ActCompID
                'beEmpRetire.LastChgID.Value = UserProfile.ActUserID
                beEmpRetire.LastChgComp.Value = "RG1200"
                beEmpRetire.LastChgID.Value = "RG1200"
                beEmpRetire.LastChgDate.Value = Now

                '12.寫退休金主檔異動紀錄
                Dim strOldData As String
                Dim strNewData As String
                '12-1 TransferType = 1 種類異動
                beEmpRetireLog_1.CompID.Value = strCompID
                beEmpRetireLog_1.EmpID.Value = strEmpID
                beEmpRetireLog_1.TransferType.Value = "1"
                beEmpRetireLog_1.LastChgDate.Value = Now
                beEmpRetireLog_1.LastChgComp.Value = "RG1200"
                beEmpRetireLog_1.LastChgID.Value = "RG1200"
                beEmpRetireLog_1.ApplyDate.Value = Now
                beEmpRetireLog_1.TransferDate.Value = strEmpDate
                beEmpRetireLog_1.DeclareDate.Value = strEmpDate
                beEmpRetireLog_1.DownDate.Value = strEmpDate
                If strEmpType = "4" Or strEmpType = "7" Then
                    strOldData = objRegistData.funEncryptNumber(strEmpID, "-")
                    strNewData = objRegistData.funEncryptNumber(strEmpID, "1")
                Else
                    strOldData = "-"
                    strNewData = "1"
                End If
                beEmpRetireLog_1.OldData.Value = strOldData
                beEmpRetireLog_1.NewData.Value = strNewData

                '12-2 TransferType = 3 公司提撥比例異動
                beEmpRetireLog_2.CompID.Value = strCompID
                beEmpRetireLog_2.EmpID.Value = strEmpID
                beEmpRetireLog_2.TransferType.Value = "3"
                beEmpRetireLog_2.LastChgDate.Value = Now
                beEmpRetireLog_2.LastChgComp.Value = "RG1200"
                beEmpRetireLog_2.LastChgID.Value = "RG1200"
                beEmpRetireLog_2.ApplyDate.Value = Now
                beEmpRetireLog_2.TransferDate.Value = strEmpDate
                beEmpRetireLog_2.DeclareDate.Value = strEmpDate
                beEmpRetireLog_2.DownDate.Value = strEmpDate
                If strEmpType = "4" Or strEmpType = "7" Then
                    strOldData = objRegistData.funEncryptNumber(strEmpID, "-")
                    strNewData = objRegistData.funEncryptNumber(strEmpID, "6")
                Else
                    strOldData = "-"
                    strNewData = "6"
                End If
                beEmpRetireLog_2.OldData.Value = strOldData
                beEmpRetireLog_2.NewData.Value = strNewData

                '12-3 TransferType = 5 委任經理人註記異動
                beEmpRetireLog_3.CompID.Value = strCompID
                beEmpRetireLog_3.EmpID.Value = strEmpID
                beEmpRetireLog_3.TransferType.Value = "5"
                beEmpRetireLog_3.LastChgDate.Value = Now
                beEmpRetireLog_3.LastChgComp.Value = "RG1200"
                beEmpRetireLog_3.LastChgID.Value = "RG1200"
                beEmpRetireLog_3.ApplyDate.Value = Now
                beEmpRetireLog_3.TransferDate.Value = strEmpDate
                beEmpRetireLog_3.DeclareDate.Value = strEmpDate
                beEmpRetireLog_3.DownDate.Value = strEmpDate
                If strEmpType = "4" Or strEmpType = "7" Then
                    strOldData = objRegistData.funEncryptNumber(strEmpID, "-")
                    strNewData = objRegistData.funEncryptNumber(strEmpID, "0")
                Else
                    strOldData = "-"
                    strNewData = "0"
                End If
                beEmpRetireLog_3.OldData.Value = strOldData
                beEmpRetireLog_3.NewData.Value = strNewData

                '12-4 TransferType = 6 僱主註記異動
                beEmpRetireLog_4.CompID.Value = strCompID
                beEmpRetireLog_4.EmpID.Value = strEmpID
                beEmpRetireLog_4.TransferType.Value = "6"
                beEmpRetireLog_4.LastChgDate.Value = Now
                beEmpRetireLog_4.LastChgComp.Value = "RG1200"
                beEmpRetireLog_4.LastChgID.Value = "RG1200"
                beEmpRetireLog_4.ApplyDate.Value = Now
                beEmpRetireLog_4.TransferDate.Value = strEmpDate
                beEmpRetireLog_4.DeclareDate.Value = strEmpDate
                beEmpRetireLog_4.DownDate.Value = strEmpDate
                If strEmpType = "4" Or strEmpType = "7" Then
                    strOldData = objRegistData.funEncryptNumber(strEmpID, "-")
                    strNewData = objRegistData.funEncryptNumber(strEmpID, "0")
                Else
                    strOldData = "-"
                    strNewData = "0"
                End If
                beEmpRetireLog_4.OldData.Value = strOldData
                beEmpRetireLog_4.NewData.Value = strNewData

                '13.職等年資檔[EmpSenRank]
                beEmpSenRank.CompID.Value = strCompID
                beEmpSenRank.EmpID.Value = strEmpID
                beEmpSenRank.RankID.Value = strRankID
                beEmpSenRank.ValidDateB.Value = myDataRead(nintEmpDate).ToString
                'beEmpSenRank.LastChgComp.Value = UserProfile.ActCompID
                'beEmpSenRank.LastChgID.Value = UserProfile.ActUserID
                beEmpSenRank.LastChgComp.Value = "RG1200"
                beEmpSenRank.LastChgID.Value = "RG1200"
                beEmpSenRank.LastChgDate.Value = Now

                '14.單位年資檔[EmpSenOrgType]
                beEmpSenOrgType.CompID.Value = strCompID
                beEmpSenOrgType.PreCompID.Value = strCompID
                beEmpSenOrgType.Reason.Value = "01"
                beEmpSenOrgType.EmpID.Value = strEmpID
                beEmpSenOrgType.IDNo.Value = strIDNo
                beEmpSenOrgType.OrgCompID.Value = strCompID
                Dim OrganizationTable As DataTable
                OrganizationTable = objRG.QueryOrganization(myDataRead(nintOrganID).ToString, myDataRead(nintDeptID).ToString, strCompID)
                If OrganizationTable.Rows().Count <> 0 Then
                    beEmpSenOrgType.OrgType.Value = OrganizationTable.Rows(0).Item(0).ToString()
                    beEmpSenOrgType.OrgTypeName.Value = OrganizationTable.Rows(0).Item(1).ToString()
                    beEmpSenOrgType.OrganID.Value = OrganizationTable.Rows(0).Item(2).ToString()
                    beEmpSenOrgType.OrgName.Value = OrganizationTable.Rows(0).Item(3).ToString()
                End If
                beEmpSenOrgType.ValidDateB.Value = myDataRead(nintEmpDate).ToString
                beEmpSenOrgType.LastChgComp.Value = "RG1200"
                beEmpSenOrgType.LastChgID.Value = "RG1200"
                'beEmpSenOrgType.LastChgComp.Value = UserProfile.ActCompID
                'beEmpSenOrgType.LastChgID.Value = UserProfile.ActUserID
                beEmpSenOrgType.LastChgDate.Value = Now

                '15.工作性質年資檔[EmpSenWorkType]
                beEmpSenWorkType.CompID.Value = strCompID
                beEmpSenWorkType.PreCompID.Value = strCompID
                beEmpSenWorkType.Reason.Value = "01"
                beEmpSenWorkType.EmpID.Value = strEmpID
                beEmpSenWorkType.IDNo.Value = strIDNo
                beEmpSenWorkType.WorkTypeID.Value = myDataRead(nintWorkTypeID).ToString
                beEmpSenWorkType.WorkType.Value = objRG.QueryData("WorkType", " AND WorkTypeID = " & Bsp.Utility.Quote(Trim(myDataRead(nintWorkTypeID).ToString)), " isnull(Remark, '') as WorkTypeName ")
                beEmpSenWorkType.ValidDateB.Value = myDataRead(nintEmpDate).ToString
                beEmpSenWorkType.CategoryI.Value = objRG.QueryData("WorkType", " AND CompID = " & Bsp.Utility.Quote(Trim(strCompID)) & " AND WorkTypeID = " & Bsp.Utility.Quote(Trim(myDataRead(nintWorkTypeID).ToString)), " CategoryI ")
                beEmpSenWorkType.CategoryII.Value = objRG.QueryData("WorkType", " AND CompID = " & Bsp.Utility.Quote(Trim(strCompID)) & " AND WorkTypeID = " & Bsp.Utility.Quote(Trim(myDataRead(nintWorkTypeID).ToString)), " CategoryII ")
                beEmpSenWorkType.CategoryIII.Value = objRG.QueryData("WorkType", " AND CompID = " & Bsp.Utility.Quote(Trim(strCompID)) & " AND WorkTypeID = " & Bsp.Utility.Quote(Trim(myDataRead(nintWorkTypeID).ToString)), " CategoryIII ")
                beEmpSenWorkType.LastChgComp.Value = "RG1200"
                beEmpSenWorkType.LastChgID.Value = "RG1200"
                'beEmpSenWorkType.LastChgComp.Value = UserProfile.ActCompID
                'beEmpSenWorkType.LastChgID.Value = UserProfile.ActUserID
                beEmpSenWorkType.LastChgDate.Value = Now

                '16.簽核單位年資檔[EmpSenOrgTypeFlow]
                beEmpSenOrgTypeFlow.CompID.Value = strCompID
                beEmpSenOrgTypeFlow.EmpID.Value = strEmpID
                beEmpSenOrgTypeFlow.OrgType.Value = myDataRead(nintDeptID).ToString
                beEmpSenOrgTypeFlow.ValidDateB.Value = myDataRead(nintEmpDate).ToString
                beEmpSenOrgTypeFlow.LastChgComp.Value = "RG1200"
                beEmpSenOrgTypeFlow.LastChgID.Value = "RG1200"
                'beEmpSenOrgTypeFlow.LastChgComp.Value = UserProfile.ActCompID
                'beEmpSenOrgTypeFlow.LastChgID.Value = UserProfile.ActUserID
                beEmpSenOrgTypeFlow.LastChgDate.Value = Now

                '17.職位年資檔[EmpSenPosition]
                If objHR.IsRankIDMapFlag(strCompID) Then
                    beEmpSenPosition.CompID.Value = strCompID
                    beEmpSenPosition.PreCompID.Value = strCompID
                    beEmpSenPosition.Reason.Value = "01"
                    beEmpSenPosition.EmpID.Value = strEmpID
                    beEmpSenPosition.IDNo.Value = strIDNo
                    beEmpSenPosition.PositionID.Value = myDataRead(nintPositionID).ToString
                    beEmpSenPosition.Position.Value = objRG.QueryData("Position", " AND PositionID = " & Bsp.Utility.Quote(Trim(myDataRead(nintPositionID).ToString)), " isnull(Remark, '') as PositionName ")
                    beEmpSenPosition.ValidDateB.Value = myDataRead(nintEmpDate).ToString
                    beEmpSenPosition.CategoryI.Value = objRG.QueryData("Position", " AND CompID = " & Bsp.Utility.Quote(Trim(strCompID)) & " AND PositionID = " & Bsp.Utility.Quote(Trim(myDataRead(nintPositionID).ToString)), " CategoryI ")
                    beEmpSenPosition.CategoryII.Value = objRG.QueryData("Position", " AND CompID = " & Bsp.Utility.Quote(Trim(strCompID)) & " AND PositionID = " & Bsp.Utility.Quote(Trim(myDataRead(nintPositionID).ToString)), " CategoryII ")
                    beEmpSenPosition.CategoryIII.Value = objRG.QueryData("Position", " AND CompID = " & Bsp.Utility.Quote(Trim(strCompID)) & " AND PositionID = " & Bsp.Utility.Quote(Trim(myDataRead(nintPositionID).ToString)), " CategoryIII ")
                    beEmpSenPosition.LastChgComp.Value = "RG1200"
                    beEmpSenPosition.LastChgID.Value = "RG1200"
                    'beEmpSenPosition.LastChgComp.Value = UserProfile.ActCompID
                    'beEmpSenPosition.LastChgID.Value = UserProfile.ActUserID
                    beEmpSenPosition.LastChgDate.Value = Now
                End If

                '18.公司年資檔[EmpSenComp]
                beEmpSenComp.IDNo.Value = strIDNo
                beEmpSenComp.CompID.Value = strCompID
                beEmpSenComp.EmpID.Value = strEmpID
                beEmpSenComp.ValidDateB.Value = strEmpDate
                beEmpSenComp.ValidDateB_Sinopac.Value = strEmpDate
                beEmpSenComp.LastChgComp.Value = "RG1200"
                beEmpSenComp.LastChgID.Value = "RG1200"
                beEmpSenComp.LastChgDate.Value = Now

                bsPersonal.Insert(bePersonal, tran)                         '1.寫入人事主檔Personal
                bsSalaryData.Insert(beSalaryData, tran)                     '2.寫入員工薪資資料檔SalaryData
                bsCommunication.Insert(beCommunication, tran)               '3.寫入通訊資料檔Communication

                '4.若從招募而來的資料則新增員工特殊設定資料檔[PersonalOther]
                '2015/11/24 Modify 更改if條件
                If strPayrollCompID = "SPHSC1" Then
                    bsPersonalOther_1.Insert(bePersonalOther_1, tran)
                Else
                    If myDataRead(nintRecID).ToString <> "" Or strCheckInDate <> "" And strCheckInFileCompID <> "SPHCR1" Then
                        bsPersonalOther_1.Insert(bePersonalOther_1, tran)
                    End If
                End If

                If strCheckInFileCompID = "SPHCR1" Then
                    bsPersonalOther_2.Insert(bePersonalOther_2, tran)       '5.若是繳交文件歸屬信用卡，新增員工特殊設定資料檔[PersonalOther]
                End If
                bsEmpWorkType.Insert(beEmpWorkType, tran)                   '6.寫入員工工作性質檔EmpWorkType
                bsEmpPosition.Insert(beEmpPosition, tran)                   '7.寫入員工職位檔EmpPosition
                If strCompID = "SPHBK1" Then
                    bsCheckInFile_SPHBK1.Insert(beCheckInFile_SPHBK1, tran) '8.寫入員工報到文件檔[CheckInFile_SPHBK1, CheckInFile_SPHSC1]
                ElseIf strCompID = "SPHSC1" Then
                    bsCheckInFile_SPHSC1.Insert(beCheckInFile_SPHSC1, tran)
                End If
                bsEmployeeLog.Insert(beEmployeeLog, tran)                   '9.寫入新增員工異動記錄檔[EmployeeLog]
                If strPayrollCompID <> "SPHSC1" Then
                    bsProbation.Insert(beProbation, tran)                   '10.寫入適用考核檔Probation
                Else
                    bsProbationSPHSC1.Insert(beProbationSPHSC1, tran)
                End If
                bsEmpRetire.Insert(beEmpRetire, tran)                       '11.寫入員工退休金主檔 [EmpRetire]
                bsEmpRetireLog.Insert(beEmpRetireLog_1, tran)               '12-1.寫退休金主檔異動紀錄 [EmpRetireLog]
                bsEmpRetireLog.Insert(beEmpRetireLog_2, tran)               '12-2.寫退休金主檔異動紀錄 [EmpRetireLog]
                bsEmpRetireLog.Insert(beEmpRetireLog_3, tran)               '12-3.寫退休金主檔異動紀錄 [EmpRetireLog]
                bsEmpRetireLog.Insert(beEmpRetireLog_4, tran)               '12-4.寫退休金主檔異動紀錄 [EmpRetireLog]
                bsEmpSenRank.Insert(beEmpSenRank, tran)                     '13.職等年資檔[EmpSenRank]
                bsEmpSenOrgType.Insert(beEmpSenOrgType, tran)               '14.單位年資檔[EmpSenOrgType]
                bsEmpSenWorkType.Insert(beEmpSenWorkType, tran)             '15.工作性質年資檔[EmpSenWorkType]
                bsEmpSenOrgTypeFlow.Insert(beEmpSenOrgTypeFlow, tran)       '16.簽核單位年資檔[EmpSenOrgTypeFlow]
                If objHR.IsRankIDMapFlag(strCompID) Then
                    bsEmpSenPosition.Insert(beEmpSenPosition, tran)         '17.職位年資檔[EmpSenPosition]
                End If
                bsEmpSenComp.Insert(beEmpSenComp, tran)                     '18.公司年資檔[EmpSenComp]

                '19.回寫招募系統-H00017功能-報到註記-Y報到及實際報到日欄位 DB欄位
                If myDataRead(nintRecID).ToString <> "" Or Trim(myDataRead(nintCheckInDate).ToString) <> "" Then
                    objRG.UpdateRE_ContractData(myDataRead(nintEmpDate).ToString, strEmpID, strCompID, myDataRead(nintRecID).ToString, myDataRead(nintCheckInDate).ToString)
                End If

                tran.Commit()
                inTrans = False
                Return True

            Catch ex As Exception
                If inTrans Then tran.Rollback()
                ErrMsg = ex.Message
                'Bsp.Utility.ShowMessage(Me, Me.FunID & ".funUploadDataSingle_Personal", ex)
                Return False
            End Try
        End Using
    End Function
#End Region

#Region "2.員工年薪主檔資料 Salary"
    Private Function funCheckData_Salary(ByVal FileName As String) As Boolean
        Dim CompID As String = UserProfile.SelectCompRoleID
        Dim objRG As New RG1
        Dim objHR As New HR
        Dim bolInputStatus As Boolean = True
        Dim strErrMsg As String = ""
        Dim strSqlWhere As String = ""
        Dim bolCheckData As Boolean = False  '檢核資料

        '補充說明：欄位非必填且放空白	
        Dim nintCompID As Integer = 0                   '公司代碼
        Dim nintEmpID As Integer = 1                    '員工編號
        Dim nintBasePay As Integer = 2                  '年薪
        Dim nintIsHLBFlag As Integer = 3                '高階主管註記
        Dim nintPrincipalBankID As Integer = 4          '主要薪資存入銀行
        Dim nintPrincipalAccount As Integer = 5         '主要入帳銀行帳號
        Dim nintPrincipalAccountRatio As Integer = 6    '主要入帳比例
        Dim nintSecondBankID As Integer = 7             '次要入帳銀行代碼
        Dim nintSecondAccount As Integer = 8            '次要入帳銀行帳號
        Dim nintSecondAccountRatio As Integer = 9       '次要入帳比例
        Dim nintTaxRearNbr As Integer = 10              '計稅撫養人數
        Dim nintFeeShareComp As Integer = 11            '費用分攤公司代碼
        Dim nintFeeShareDept As Integer = 12            '費用分攤單位
        Dim nintRetireFlag As Integer = 13              '退休金註記
        Dim nintWelfareFlag As Integer = 14             '福利金註記
        Dim nintUnityAccountFlag As Integer = 15        '單一銀行入帳註記
        Dim nintTaxOption As Integer = 16               '所得稅扣繳方式
        Dim nintMonthOfAnnualPay As Integer = 18        '年薪計薪月份
        Dim nintLabFlag As Integer = 19                 '加勞保
        Dim nintHeaFlag As Integer = 20                 '加健保
        Dim nintGrpFlag As Integer = 21                 '加團保
        Dim nintGrpUnitID As Integer = 22               '證券團保公司代碼

        intTotalCount = 0
        intSuccessCount = 0
        intErrorCount = 0

        Dim strExcelConn As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & FileName & ";Extended Properties=EXCEL 8.0"

        Dim strExcelWorkSheet As String = "[RG1200_Salary$]"
        Dim strExcelSelect As String = "SELECT * FROM " & strExcelWorkSheet

        Dim myExcelConn As OleDbConnection = Nothing
        myExcelConn = New OleDbConnection(strExcelConn)
        Dim myExcelCommand As OleDbCommand = New OleDbCommand(strExcelSelect, myExcelConn)
        Try
            myExcelConn.Open()

            Using myDataRead As OleDbDataReader = myExcelCommand.ExecuteReader
                While myDataRead.Read

                    '檢核上傳資料
                    strErrMsg = ""
                    bolCheckData = True

                    '先判斷是否上傳欄位個數正確
                    If myDataRead.FieldCount <> 23 Then
                        strErrMsg = strErrMsg & "==> 上傳資料欄位個數不正確！"
                        intErrorCount = intErrorCount + 1
                        bolCheckData = False
                    Else
                        '公司代碼與授權公司代碼不同
                        If CompID <> myDataRead(nintCompID).ToString Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCompID).ToString) & " 公司代碼與授權公司代碼不同!"
                            bolCheckData = False
                        End If

                        '公司代碼
                        If Trim(myDataRead(nintCompID).ToString) = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCompID).ToString) & " 公司代碼未輸入!"
                            bolCheckData = False
                        Else
                            strSqlWhere = ""
                            strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                            If Not objRG.IsDataExists("Company", strSqlWhere) Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCompID).ToString) & " 查無公司代碼!"
                                bolCheckData = False
                            End If
                        End If

                        '員工編號
                        If myDataRead(nintEmpID).ToString().Trim() = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintEmpID).ToString) & " 員工編號未輸入!"
                            bolCheckData = False
                        Else
                            strSqlWhere = ""
                            strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                            strSqlWhere = strSqlWhere & " and EmpID =" & Bsp.Utility.Quote(Trim(myDataRead(nintEmpID).ToString))
                            If Not objRG.IsDataExists("Personal", strSqlWhere) Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintEmpID).ToString) & " 員工編號不存在"
                                bolCheckData = False
                            Else
                                strSqlWhere = ""
                                strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                                strSqlWhere = strSqlWhere & " and EmpID =" & Bsp.Utility.Quote(Trim(myDataRead(nintEmpID).ToString))
                                strSqlWhere = strSqlWhere & " and WorkStatus <> '1'"
                                If objRG.IsDataExists("Personal", strSqlWhere) Then
                                    strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintEmpID).ToString) & " 員工非在職狀態"
                                    bolCheckData = False
                                End If
                            End If
                        End If

                        '檢查員工最後異動是否由RG1200而來
                        strSqlWhere = ""
                        strSqlWhere = strSqlWhere & " and LastChgComp = 'RG1200' "
                        strSqlWhere = strSqlWhere & " and LastChgID = 'RG1200' "
                        If Not objRG.IsDataExists("Personal", strSqlWhere) Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintEmpID).ToString) & " 員工基本資料最後異動記錄非為2320整批作業"
                            bolCheckData = False
                        End If

                        '年薪(BasePay)、年薪計薪月份(MonthOfAnnualPay)
                        If myDataRead(nintBasePay).ToString().Trim() = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintBasePay).ToString) & " 年薪未輸入!"
                            bolCheckData = False
                        End If
                        If myDataRead(nintMonthOfAnnualPay).ToString().Trim() = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintMonthOfAnnualPay).ToString) & " 年薪計薪月份未輸入!"
                            bolCheckData = False
                        End If
                        If myDataRead(nintBasePay).ToString().Trim() = "" Or myDataRead(nintMonthOfAnnualPay).ToString().Trim() = "" Or myDataRead(nintMonthOfAnnualPay).ToString().Trim() = "0" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintMonthOfAnnualPay).ToString) & " 年薪不能空白、年薪計薪月份不能空白或等於0"
                            bolCheckData = False
                        End If

                        '高階主管註記
                        If myDataRead(nintIsHLBFlag).ToString().Trim() = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintIsHLBFlag).ToString) & " 高階主管註記未輸入!"
                            bolCheckData = False
                        Else
                            If Len(myDataRead(nintIsHLBFlag).ToString().Trim()) = 0 Or (myDataRead(nintIsHLBFlag).ToString().Trim() <> 0 And myDataRead(nintIsHLBFlag).ToString().Trim() <> 1) Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintIsHLBFlag).ToString) & " 高階主管註記 0=否/1=是"
                                bolCheckData = False
                            End If
                        End If

                        '主要入帳銀行代碼(PrincipalBankID)、主要入帳銀行帳號(PrincipalAccount)
                        strSqlWhere = ""
                        strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                        strSqlWhere = strSqlWhere & " and EmpID =" & Bsp.Utility.Quote(Trim(myDataRead(nintEmpID).ToString))
                        strSqlWhere = strSqlWhere & " and LastChgComp = 'RG1200'"
                        strSqlWhere = strSqlWhere & " and LastChgID = 'RG1200'"
                        If Not objRG.IsDataExists("Personal", strSqlWhere) Then
                            Dim returnCheckAccount As Integer = objHR.funCheckAccount(Trim(myDataRead(nintPrincipalBankID).ToString), Trim(myDataRead(nintPrincipalAccount).ToString))
                            Select Case returnCheckAccount
                                Case -1
                                    strErrMsg = strErrMsg & "==> 帳號邏輯錯誤"
                                    bolCheckData = False
                                Case 1
                                    strErrMsg = strErrMsg & "==> 帳號應為12或13碼"
                                    bolCheckData = False
                                Case -2
                                    strErrMsg = strErrMsg & "==> 員工主要入帳銀行帳號，帳號檢核邏輯不存在"
                                    bolCheckData = False
                                Case Is > 10
                                    strErrMsg = strErrMsg & "==> 入帳銀行帳號應為" & returnCheckAccount & "碼"
                                    bolCheckData = False
                            End Select
                        End If

                        '主要入帳比例
                        If Trim(myDataRead(nintPrincipalAccountRatio).ToString) <> "" Then
                            If Not objHR.funCheckStr(Trim(myDataRead(nintPrincipalAccountRatio).ToString)) Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintPrincipalAccountRatio).ToString) & " 主要入帳比例只能為數字"
                                bolCheckData = False
                            End If
                            If CInt(Trim(myDataRead(nintPrincipalAccountRatio).ToString)) < 0 Or CInt(Trim(myDataRead(nintPrincipalAccountRatio).ToString)) > 255 Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintPrincipalAccountRatio).ToString) & " 主要入帳比例請輸入0~255之數字"
                                bolCheckData = False
                            End If
                        End If

                        '次要入帳銀行代碼(SecondBankID)、次要入帳銀行帳號(SecondAccount)
                        strSqlWhere = ""
                        strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                        strSqlWhere = strSqlWhere & " and EmpID =" & Bsp.Utility.Quote(Trim(myDataRead(nintEmpID).ToString))
                        strSqlWhere = strSqlWhere & " and LastChgComp = 'RG1200'"
                        strSqlWhere = strSqlWhere & " and LastChgID = 'RG1200'"
                        If Not objRG.IsDataExists("Personal", strSqlWhere) Then
                            Dim returnCheckAccount As Integer = objHR.funCheckAccount(Trim(myDataRead(nintSecondBankID).ToString), Trim(myDataRead(nintSecondAccount).ToString))
                            Select Case returnCheckAccount
                                Case -1
                                    strErrMsg = strErrMsg & "==> 帳號邏輯錯誤"
                                    bolCheckData = False
                                Case 1
                                    strErrMsg = strErrMsg & "==> 帳號應為12或13碼"
                                    bolCheckData = False
                                Case -2
                                    strErrMsg = strErrMsg & "==> 員工次要入帳銀行帳號，帳號檢核邏輯不存在"
                                    bolCheckData = False
                                Case Is > 10
                                    strErrMsg = strErrMsg & "==> 入帳銀行帳號應為" & returnCheckAccount & "碼"
                                    bolCheckData = False
                            End Select
                        End If

                        '次要入帳比例
                        If Trim(myDataRead(nintSecondAccountRatio).ToString) <> "" Then
                            If Not objHR.funCheckStr(Trim(myDataRead(nintSecondAccountRatio).ToString)) Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintSecondAccountRatio).ToString) & " 次要入帳比例只能為數字"
                                bolCheckData = False
                            End If
                            If CInt(Trim(myDataRead(nintSecondAccountRatio).ToString)) < 0 Or CInt(Trim(myDataRead(nintSecondAccountRatio).ToString)) > 255 Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintSecondAccountRatio).ToString) & " 次要入帳比例請輸入0~255之數字"
                                bolCheckData = False
                            End If
                        End If

                        '檢核員工入帳帳號是否存在
                        strSqlWhere = ""
                        strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                        strSqlWhere = strSqlWhere & " and EmpID =" & Bsp.Utility.Quote(Trim(myDataRead(nintEmpID).ToString))
                        If objRG.IsDataExists("EmpAccount", strSqlWhere) Then
                            strErrMsg = strErrMsg & "==> 員工入帳帳號檔已存在"
                            bolCheckData = False
                        End If

                        '檢核薪資主檔是否存在
                        strSqlWhere = ""
                        strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                        strSqlWhere = strSqlWhere & " and EmpID =" & Bsp.Utility.Quote(Trim(myDataRead(nintEmpID).ToString))
                        If objRG.IsDataExists("Salary", strSqlWhere) Then
                            strErrMsg = strErrMsg & "==> 薪資主檔已存在"
                            bolCheckData = False
                        End If

                        '檢核員工保險待加退保檔是否存在
                        If myDataRead(nintLabFlag).ToString().Trim() = "1" Or myDataRead(nintHeaFlag).ToString().Trim() = "1" Then
                            strSqlWhere = ""
                            strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                            strSqlWhere = strSqlWhere & " and EmpID =" & Bsp.Utility.Quote(Trim(myDataRead(nintEmpID).ToString))
                            If objRG.IsDataExists("InsureWait", strSqlWhere) Then
                                strErrMsg = strErrMsg & "==> 員工保險待加退保檔已存在"
                                bolCheckData = False
                            End If
                        End If

                        '檢核員工團保待加退保檔是否存在
                        If myDataRead(nintGrpFlag).ToString().Trim() = "1" Then
                            strSqlWhere = ""
                            strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                            strSqlWhere = strSqlWhere & " and EmpID =" & Bsp.Utility.Quote(Trim(myDataRead(nintEmpID).ToString))
                            If objRG.IsDataExists("GroupWait", strSqlWhere) Then
                                strErrMsg = strErrMsg & "==> 員工團保待加退保檔已存在"
                                bolCheckData = False
                            End If
                        End If

                        '檢核員工退休金資料是否存在
                        strSqlWhere = ""
                        strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                        strSqlWhere = strSqlWhere & " and EmpID =" & Bsp.Utility.Quote(Trim(myDataRead(nintEmpID).ToString))
                        If Not objRG.IsDataExists("EmpRetire", strSqlWhere) Then
                            strErrMsg = strErrMsg & "==> 員工退休金資料不存在"
                            bolCheckData = False
                        End If

                        '檢核費用分攤公司代碼FeeShareComp、費用分攤單位FeeShareDept
                        If Trim(myDataRead(nintFeeShareComp).ToString) <> "" Or Trim(myDataRead(nintFeeShareDept).ToString) <> "" Then
                            strSqlWhere = ""
                            strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintFeeShareComp).ToString))
                            strSqlWhere = strSqlWhere & " and OrganID =" & Bsp.Utility.Quote(Trim(myDataRead(nintFeeShareDept).ToString))
                            If Not objRG.IsDataExists("Organization", strSqlWhere) Then
                                strErrMsg = strErrMsg & "==> 費用分攤公司,部門代碼錯誤"
                                bolCheckData = False
                            End If
                        End If

                        '計稅撫養人數
                        If Trim(myDataRead(nintTaxRearNbr).ToString) <> "" Then
                            If Not objHR.funCheckStr(Trim(myDataRead(nintTaxRearNbr).ToString)) Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintTaxRearNbr).ToString) & " 計稅撫養人數只能為數字"
                                bolCheckData = False
                            End If
                            If CInt(Trim(myDataRead(nintTaxRearNbr).ToString)) < 0 Or CInt(Trim(myDataRead(nintTaxRearNbr).ToString)) > 255 Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintTaxRearNbr).ToString) & " 計稅撫養人數請輸入0~255之數字"
                                bolCheckData = False
                            End If
                        End If

                        '退休金註記
                        If myDataRead(nintRetireFlag).ToString().Trim() = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintRetireFlag).ToString) & " 退休金註記未輸入!"
                            bolCheckData = False
                        Else
                            If (Len(myDataRead(nintRetireFlag).ToString().Trim()) = 0 Or (myDataRead(nintRetireFlag).ToString().Trim() <> 0 And myDataRead(nintRetireFlag).ToString().Trim() <> 1)) Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintRetireFlag).ToString) & " 退休金註記 0=不計退休金/1=計算"
                                bolCheckData = False
                            End If
                        End If

                        '福利金註記 WelfareFlag
                        If myDataRead(nintWelfareFlag).ToString().Trim() = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintWelfareFlag).ToString) & " 福利金註記未輸入!"
                            bolCheckData = False
                        Else
                            If Len(myDataRead(nintWelfareFlag).ToString().Trim()) = 0 Or (myDataRead(nintWelfareFlag).ToString().Trim() <> 0 And myDataRead(nintWelfareFlag).ToString().Trim() <> 1) Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintWelfareFlag).ToString) & " 福利金註記 0=不計退休金/1=計算"
                                bolCheckData = False
                            End If
                        End If

                        '單一銀行入帳註記 UnityAccountFlag
                        If myDataRead(nintUnityAccountFlag).ToString().Trim() = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintUnityAccountFlag).ToString) & " 單一銀行入帳註記未輸入!"
                            bolCheckData = False
                        Else
                            If Len(myDataRead(nintUnityAccountFlag).ToString().Trim()) = 0 Or (myDataRead(nintUnityAccountFlag).ToString().Trim() <> 0 And myDataRead(nintUnityAccountFlag).ToString().Trim() <> 1) Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintUnityAccountFlag).ToString) & " 單一銀行入帳註記 0=非單一/1=單一"
                                bolCheckData = False
                            End If
                        End If

                        '所得稅扣繳方式 TaxOption
                        If myDataRead(nintTaxOption).ToString().Trim() = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintTaxOption).ToString) & " 所得稅扣繳方式未輸入!"
                            bolCheckData = False
                        Else
                            strSqlWhere = ""
                            strSqlWhere = strSqlWhere & " and TabName = 'SalaryData' "
                            strSqlWhere = strSqlWhere & " and FldName = 'TaxOption' "
                            strSqlWhere = strSqlWhere & " and NotShowFlag = '0' "
                            strSqlWhere = strSqlWhere & " and Code = " & Bsp.Utility.Quote(Trim(myDataRead(nintTaxOption).ToString))
                            If Not objRG.IsDataExists("HRCodeMap", strSqlWhere) Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintTaxOption).ToString) & " 所得稅扣繳方式錯誤"
                                bolCheckData = False
                            End If
                        End If

                        '加勞保 LabFlag
                        If myDataRead(nintLabFlag).ToString().Trim() = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintLabFlag).ToString) & " 加勞保未輸入!"
                            bolCheckData = False
                        Else
                            If myDataRead(nintLabFlag).ToString().Trim() = 0 Or (myDataRead(nintLabFlag).ToString().Trim() <> 0 And myDataRead(nintLabFlag).ToString().Trim() <> 1) Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintLabFlag).ToString) & " 加勞保 0=未加保/退保,1=加保"
                                bolCheckData = False
                            End If
                        End If

                        '加健保 HeaFlag
                        If myDataRead(nintHeaFlag).ToString().Trim() = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintHeaFlag).ToString) & " 加健保未輸入!"
                            bolCheckData = False
                        Else
                            If myDataRead(nintHeaFlag).ToString().Trim() = 0 Or (myDataRead(nintHeaFlag).ToString().Trim() <> 0 And myDataRead(nintHeaFlag).ToString().Trim() <> 1) Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintHeaFlag).ToString) & " 加健保 0=未加保/退保,1=加保"
                                bolCheckData = False
                            End If
                        End If

                        '加團保 GrpFlag
                        If myDataRead(nintGrpFlag).ToString().Trim() = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintGrpFlag).ToString) & " 加團保未輸入!"
                            bolCheckData = False
                        Else
                            If myDataRead(nintGrpFlag).ToString().Trim() = 0 Or (myDataRead(nintGrpFlag).ToString().Trim() <> 0 And myDataRead(nintGrpFlag).ToString().Trim() <> 1) Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintGrpFlag).ToString) & " 加團保 0=未加保/退保,1=加保"
                                bolCheckData = False
                            End If
                        End If

                        '證券團保公司代碼 GrpUnitID
                        '2015/11/24 Modify 更改錯誤訊息寫法
                        'If myDataRead(nintGrpUnitID).ToString().Trim() = "" Then
                        '    strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintGrpUnitID).ToString) & " 證券團保公司代碼未輸入!"
                        '    bolCheckData = False
                        'Else
                        '    If objHR.funGetSPHSC1Grp(myDataRead(nintCompID).ToString().Trim()) Then
                        '        strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintGrpUnitID).ToString) & " 證券體系需上傳團保公司代碼"
                        '        bolCheckData = False
                        '    End If
                        'End If
                        If objHR.funGetSPHSC1Grp(myDataRead(nintCompID).ToString().Trim()) And Trim(myDataRead(nintGrpUnitID).ToString).Length = 0 Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintGrpUnitID).ToString) & " 證券體系需上傳團保公司代碼"
                            bolCheckData = False
                        End If
                        If Trim(myDataRead(nintGrpUnitID).ToString) <> "" Then
                            If Not objHR.funCheckStr(Trim(myDataRead(nintGrpUnitID).ToString)) Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintGrpUnitID).ToString) & " 證券團保公司代碼只能為數字"
                                bolCheckData = False
                            Else
                                If CInt(Trim(myDataRead(nintGrpUnitID).ToString)) < 0 Or CInt(Trim(myDataRead(nintGrpUnitID).ToString)) > 255 Then
                                    strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintGrpUnitID).ToString) & " 證券團保公司代碼請輸入0~255之數字"
                                    bolCheckData = False
                                End If
                            End If
                        End If


                        If bolCheckData Then
                            Dim ErrMsg As String = ""
                            If funUploadDataSingle_Salary(myDataRead, ErrMsg) Then
                                intSuccessCount = intSuccessCount + 1
                            Else
                                intErrorCount = intErrorCount + 1
                            End If

                            If ErrMsg <> "" Then
                                strErrMsg = strErrMsg & "==> " & ErrMsg
                            End If
                        Else
                            intErrorCount = intErrorCount + 1
                        End If

                    End If

                    If strErrMsg <> "" Then
                        subWriteLog("資料列:" & intTotalCount + 1 _
                                    & " 公司代碼：" & Trim(myDataRead(nintCompID).ToString) _
                                    & " 員工編號：" & Trim(myDataRead(nintEmpID).ToString) _
                                    & " 年薪：" & Trim(myDataRead(nintBasePay).ToString) _
                                    & " 高階主管註記：" & Trim(myDataRead(nintIsHLBFlag).ToString) _
                                    & " 主要薪資存入銀行：" & Trim(myDataRead(nintPrincipalBankID).ToString) _
                                    & " 主要入帳銀行帳號：" & Trim(myDataRead(nintPrincipalAccount).ToString) _
                                    & " 主要入帳比例：" & Trim(myDataRead(nintPrincipalAccountRatio).ToString) _
                                    & " 次要入帳銀行代碼：" & Trim(myDataRead(nintSecondBankID).ToString) _
                                    & " 次要入帳銀行帳號：" & Trim(myDataRead(nintSecondAccount).ToString) _
                                    & " 次要入帳比例：" & Trim(myDataRead(nintSecondAccountRatio).ToString) _
                                    & " 計稅撫養人數：" & Trim(myDataRead(nintTaxRearNbr).ToString) _
                                    & " 費用分攤公司代碼：" & Trim(myDataRead(nintFeeShareComp).ToString) _
                                    & " 費用分攤單位：" & Trim(myDataRead(nintFeeShareDept).ToString) _
                                    & " 退休金註記：" & Trim(myDataRead(nintRetireFlag).ToString) _
                                    & " 福利金註記：" & Trim(myDataRead(nintWelfareFlag).ToString) _
                                    & " 單一銀行入帳註記：" & Trim(myDataRead(nintUnityAccountFlag).ToString) _
                                    & " 所得稅扣繳方式：" & Trim(myDataRead(nintTaxOption).ToString) _
                                    & " 年薪計薪月份：" & Trim(myDataRead(nintMonthOfAnnualPay).ToString) _
                                    & " 加勞保：" & Trim(myDataRead(nintLabFlag).ToString) _
                                    & " 加健保：" & Trim(myDataRead(nintHeaFlag).ToString) _
                                    & " 加團保：" & Trim(myDataRead(nintGrpFlag).ToString) _
                                    & " 證券團保公司代碼：" & Trim(myDataRead(nintGrpUnitID).ToString) _
                                    & strErrMsg)
                        bolInputStatus = False
                    End If

                    intTotalCount = intTotalCount + 1

                End While
            End Using

            myExcelConn.Close()

            If bolInputStatus = False Then
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            myExcelConn.Close()
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".funCheckData1", ex)
            Return False
        End Try
    End Function

    '資料上傳 單筆
    Private Function funUploadDataSingle_Salary(ByVal myDataRead As OleDbDataReader, ByRef ErrMsg As String) As Boolean
        Dim bsPersonal As New bePersonal.Service()
        Dim bePersonal As New bePersonal.Row
        Dim bsSalaryData As New beSalaryData.Service()
        Dim beSalaryData As New beSalaryData.Row
        Dim bsEmpAccount As New beEmpAccount.Service()
        Dim beEmpAccount As New beEmpAccount.Row
        Dim bsSalary_cnAnnualPay As New beSalary.Service()  'Salary(年薪)
        Dim beSalary_cnAnnualPay As New beSalary.Row
        Dim bsSalary_cnMealPay As New beSalary.Service()    'Salary(伙食)
        Dim beSalary_cnMealPay As New beSalary.Row
        Dim bsInsureWait As New beInsureWait.Service()
        Dim beInsureWait As New beInsureWait.Row
        Dim bsGroupWait As New beGroupWait.Service()
        Dim beGroupWait As New beGroupWait.Row
        Dim bsEmpRetire As New beEmpRetire.Service()
        Dim beEmpRetire As New beEmpRetire.Row
        Dim bsEmpRetireLog As New beEmpRetireLog.Service()
        Dim beEmpRetireLog_1 As New beEmpRetireLog.Row
        Dim beEmpRetireLog_2 As New beEmpRetireLog.Row

        Dim dtNow = DateTime.Now
        Dim objHR As New HR
        Dim objRG As New RG1
        Dim objRegistData As New RegistData

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()

            Dim tran As DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Dim nintCompID As Integer = 0                   '公司代碼
            Dim nintEmpID As Integer = 1                    '員工編號
            Dim nintBasePay As Integer = 2                  '年薪
            Dim nintIsHLBFlag As Integer = 3                '高階主管註記
            Dim nintPrincipalBankID As Integer = 4          '主要薪資存入銀行
            Dim nintPrincipalAccount As Integer = 5         '主要入帳銀行帳號
            Dim nintPrincipalAccountRatio As Integer = 6    '主要入帳比例
            Dim nintSecondBankID As Integer = 7             '次要入帳銀行代碼
            Dim nintSecondAccount As Integer = 8            '次要入帳銀行帳號
            Dim nintSecondAccountRatio As Integer = 9       '次要入帳比例
            Dim nintTaxRearNbr As Integer = 10              '計稅撫養人數
            Dim nintFeeShareComp As Integer = 11            '費用分攤公司代碼
            Dim nintFeeShareDept As Integer = 12            '費用分攤單位
            Dim nintRetireFlag As Integer = 13              '退休金註記
            Dim nintWelfareFlag As Integer = 14             '福利金註記
            Dim nintUnityAccountFlag As Integer = 15        '單一銀行入帳註記
            Dim nintTaxOption As Integer = 16               '所得稅扣繳方式
            Dim nintMonthOfAnnualPay As Integer = 18        '年薪計薪月份
            Dim nintLabFlag As Integer = 19                 '加勞保
            Dim nintHeaFlag As Integer = 20                 '加健保
            Dim nintGrpFlag As Integer = 21                 '加團保
            Dim nintGrpUnitID As Integer = 22               '證券團保公司代碼

            Dim intLoop As Integer = 0

            Try
                Dim strCompID As String = Trim(myDataRead(nintCompID).ToString)
                Dim strEmpID As String = Trim(myDataRead(nintEmpID).ToString)
                Dim strIDNo As String = objHR.GetIDNo(strCompID, strEmpID)
                Dim strEmpDate As String = objRG.QueryData("Personal", " AND CompID = " & Bsp.Utility.Quote(strCompID) & " AND EmpID = " & Bsp.Utility.Quote(strEmpID), "Convert(Char(10), EmpDate, 111)")

                '1. 更新人事主檔Personal
                bePersonal.CompID.Value = strCompID
                bePersonal.EmpID.Value = strEmpID
                bePersonal.IsHLBFlag.Value = Trim(myDataRead(nintIsHLBFlag).ToString)
                bePersonal.LastChgComp.Value = UserProfile.ActCompID
                bePersonal.LastChgID.Value = UserProfile.ActUserID
                'bePersonal.LastChgComp.Value = "RG1200"
                'bePersonal.LastChgID.Value = "RG1200"
                bePersonal.LastChgDate.Value = Now

                '2. 寫入員工薪資資料檔SalaryData
                beSalaryData.CompID.Value = strCompID
                beSalaryData.EmpID.Value = strEmpID
                beSalaryData.TaxRearNo.Value = Trim(myDataRead(nintTaxRearNbr).ToString)
                beSalaryData.FeeShareComp.Value = Trim(myDataRead(nintFeeShareComp).ToString)
                beSalaryData.FeeShareDept.Value = Trim(myDataRead(nintFeeShareDept).ToString)
                beSalaryData.RetireFlag.Value = Trim(myDataRead(nintRetireFlag).ToString)
                beSalaryData.WelfareFlag.Value = Trim(myDataRead(nintWelfareFlag).ToString)
                beSalaryData.UnityAccountFlag.Value = Trim(myDataRead(nintUnityAccountFlag).ToString)
                beSalaryData.TaxOption.Value = Trim(myDataRead(nintTaxOption).ToString)
                beSalaryData.MonthOfAnnualPay.Value = Trim(myDataRead(nintMonthOfAnnualPay).ToString)
                'beSalaryData.LastChgComp.Value = UserProfile.ActCompID
                'beSalaryData.LastChgID.Value = UserProfile.ActUserID
                beSalaryData.LastChgComp.Value = "RG1200"
                beSalaryData.LastChgID.Value = "RG1200"
                beSalaryData.LastChgDate.Value = Now

                '3. 寫入員工入帳帳號檔案EmpAccount
                If Trim(myDataRead(nintPrincipalBankID).ToString) > "0" And Trim(myDataRead(nintPrincipalAccount).ToString) > "0" Then
                    beEmpAccount.CompID.Value = strCompID
                    beEmpAccount.EmpID.Value = strEmpID
                    beEmpAccount.BankID.Value = Trim(myDataRead(nintPrincipalBankID).ToString)
                    beEmpAccount.Account.Value = Trim(myDataRead(nintPrincipalAccount).ToString)
                    beEmpAccount.PrincipalFlag.Value = "1"
                    beEmpAccount.AccountRatio.Value = Trim(myDataRead(nintPrincipalAccountRatio).ToString)
                    beEmpAccount.CreateComp.Value = "RG1200"
                    beEmpAccount.CreateID.Value = "RG1200"
                    beEmpAccount.CreateDate.Value = Now
                    'beEmpAccount.LastChgComp.Value = UserProfile.ActCompID
                    'beEmpAccount.LastChgID.Value = UserProfile.ActUserID
                    beEmpAccount.LastChgComp.Value = "RG1200"
                    beEmpAccount.LastChgID.Value = "RG1200"
                    beEmpAccount.LastChgDate.Value = Now
                End If

                '4. 寫入薪資主檔Salary(伙食)
                beSalary_cnMealPay.CompID.Value = strCompID
                beSalary_cnMealPay.EmpID.Value = strEmpID
                beSalary_cnMealPay.SalaryID.Value = cnMealPay
                beSalary_cnMealPay.PayMethod.Value = "1"
                '薪資金額
                Dim strMealPay As String = 0   '伙食津貼
                strMealPay = objHR.FunGetMealPay(strCompID)

                beSalary_cnMealPay.Amount.Value = objRegistData.funEncryptNumber(strEmpID, strMealPay)
                'beSalary_cnMealPay.Amount.Value = 0 '********************************************************************************

                beSalary_cnMealPay.PeriodFlag.Value = "1"
                beSalary_cnMealPay.LastChgComp.Value = "RG1200"
                beSalary_cnMealPay.LastChgID.Value = "RG1200"
                'beSalary_cnMealPay.LastChgComp.Value = UserProfile.ActCompID
                'beSalary_cnMealPay.LastChgID.Value = UserProfile.ActUserID
                beSalary_cnMealPay.LastChgDate.Value = Now

                '5. 寫入薪資主檔Salary(年薪)
                beSalary_cnAnnualPay.CompID.Value = strCompID
                beSalary_cnAnnualPay.EmpID.Value = strEmpID
                beSalary_cnAnnualPay.SalaryID.Value = cnAnnualPay
                beSalary_cnAnnualPay.PayMethod.Value = "1"
                beSalary_cnAnnualPay.Amount.Value = objRegistData.funEncryptNumber(strEmpID, Trim(myDataRead(nintBasePay).ToString))
                beSalary_cnAnnualPay.PeriodFlag.Value = "1"
                beSalary_cnAnnualPay.LastChgComp.Value = "RG1200"
                beSalary_cnAnnualPay.LastChgID.Value = "RG1200"
                'beSalary_cnAnnualPay.LastChgComp.Value = UserProfile.ActCompID
                'beSalary_cnAnnualPay.LastChgID.Value = UserProfile.ActUserID
                beSalary_cnAnnualPay.LastChgDate.Value = Now

                '6. 寫入保險待加退保檔InsureWait
                '月實際工資
                Dim lngSalary As Integer = 0
                If Trim(myDataRead(nintLabFlag).ToString) = 1 Or Trim(myDataRead(nintHeaFlag).ToString) = 1 Then
                    lngSalary = 0
                Else
                    lngSalary = Trim(myDataRead(nintBasePay).ToString) / Trim(myDataRead(nintMonthOfAnnualPay).ToString)
                End If
                beInsureWait.WaitType.Value = "1"
                beInsureWait.WaitDate.Value = strEmpDate
                beInsureWait.CompID.Value = strCompID
                beInsureWait.EmpID.Value = strEmpID
                beInsureWait.Source.Value = "1"
                beInsureWait.Amount.Value = objRegistData.funEncryptNumber(strEmpID, objHR.funGetLabHeaRetireLevel(lngSalary, "Amount", "Hea"))
                beInsureWait.LabAmount.Value = objRegistData.funEncryptNumber(strEmpID, objHR.funGetLabHeaRetireLevel(lngSalary, "Amount", "Lab"))
                beInsureWait.RetireAmount.Value = objRegistData.funEncryptNumber(strEmpID, objHR.funGetLabHeaRetireLevel(lngSalary, "Amount", "Retire"))
                beInsureWait.MthRealWages.Value = objRegistData.funEncryptNumber(strEmpID, lngSalary)
                beInsureWait.IdentityID.Value = "01"
                beInsureWait.RelativeID.Value = "00"
                beInsureWait.Reason.Value = "01"
                '勞保加退保日期
                If Trim(myDataRead(nintLabFlag).ToString) = 1 Then
                    beInsureWait.LabDate.Value = strEmpDate
                Else
                    beInsureWait.LabDate.Value = "1900/01/01"
                End If
                '健保加退保日期
                If Trim(myDataRead(nintHeaFlag).ToString) = 1 Then
                    beInsureWait.HeaDate.Value = strEmpDate
                Else
                    beInsureWait.HeaDate.Value = "1900/01/01"
                End If
                beInsureWait.InsureState.Value = "0"
                '2015/11/30 Add
                beInsureWait.LastChgComp.Value = "RG1200"
                beInsureWait.LastChgID.Value = "RG1200"
                beInsureWait.LastChgDate.Value = Now

                '7. 寫入團保待加退保檔GroupWait
                beGroupWait.WaitType.Value = "1"
                beGroupWait.WaitDate.Value = strEmpDate
                beGroupWait.CompID.Value = strCompID
                beGroupWait.EmpID.Value = strEmpID
                beGroupWait.Source.Value = "1"
                beGroupWait.GrpLvl.Value = objRG.QueryData("Personal", " AND CompID = " & Bsp.Utility.Quote(strCompID) & " AND EmpID = " & Bsp.Utility.Quote(strEmpID), " isnull(RankID, '') AS RankID ")
                beGroupWait.RelativeID.Value = "00"
                beGroupWait.GrpDate.Value = strEmpDate
                '健康聲明書註記
                Dim strNotifyFlag As String = ""
                If objHR.FunGetRankIDMap(strCompID, objHR.funGetRankID(strCompID, strEmpID)) < "07" Then
                    strNotifyFlag = "0"
                Else
                    strNotifyFlag = "1"
                End If
                beGroupWait.NotifyFlag.Value = strNotifyFlag
                beGroupWait.GrpUnitID.Value = IIf(Trim(myDataRead(nintGrpUnitID).ToString) = "", 0, Trim(myDataRead(nintGrpUnitID).ToString))
                '2015/11/30 Add
                beGroupWait.LastChgComp.Value = "RG1200"
                beGroupWait.LastChgID.Value = "RG1200"
                beGroupWait.LastChgDate.Value = Now

                '8. 更新勞退主檔EmpRetire
                Dim mOldAmount As Integer = 0           '調整前平均月薪
                Dim mNewAmount As Integer = 0           '調整後平均月薪
                Dim mOldReleaseLevel As Integer = 0     '調整前投保級距(臨時計算)
                Dim mNewReleaseLevel As Integer = 0     '調整後投保級距(臨時計算)
                Dim strAmount As String = objRG.QueryData("EmpRetire", " AND CompID = " & Bsp.Utility.Quote(strCompID) & " AND EmpID = " & Bsp.Utility.Quote(strEmpID), " Amount ").Trim
                strAmount = objRegistData.funDecryptNumber(strEmpID, strAmount)
                Long.TryParse(strAmount, mOldAmount)

                'mOldAmount = "0"    '******************************************************
                mNewAmount = Trim(myDataRead(nintBasePay).ToString) / Trim(myDataRead(nintMonthOfAnnualPay).ToString)
                'mOldReleaseLevel = objHR.funGetLabHeaRetireLevel("0", "Retire")    '******************************************************
                mOldReleaseLevel = objHR.funGetLabHeaRetireLevel(mOldAmount, "Amount", "Retire")
                mNewReleaseLevel = objHR.funGetLabHeaRetireLevel(mNewAmount, "Amount", "Retire")

                '9. 寫入員工退休金主檔異動紀錄 EmpRetireLog
                beEmpRetireLog_1.CompID.Value = strCompID
                beEmpRetireLog_1.EmpID.Value = strEmpID
                beEmpRetireLog_1.TransferType.Value = "4"
                beEmpRetireLog_1.LastChgDate.Value = Now
                beEmpRetireLog_1.LastChgComp.Value = "RG1200"
                beEmpRetireLog_1.LastChgID.Value = "RG1200"
                beEmpRetireLog_1.ApplyDate.Value = Now
                beEmpRetireLog_1.TransferDate.Value = strEmpDate
                beEmpRetireLog_1.DeclareDate.Value = strEmpDate
                beEmpRetireLog_1.DownDate.Value = strEmpDate
                beEmpRetireLog_1.OldData.Value = mOldAmount
                beEmpRetireLog_1.NewData.Value = mNewAmount

                '10. 勞退級距有異動需加 EmpRetireLog
                Dim strOldData As String
                Dim strNewData As String
                beEmpRetireLog_2.CompID.Value = strCompID
                beEmpRetireLog_2.EmpID.Value = strEmpID
                beEmpRetireLog_2.TransferType.Value = "7"
                beEmpRetireLog_2.LastChgDate.Value = Now
                beEmpRetireLog_2.LastChgComp.Value = "RG1200"
                beEmpRetireLog_2.LastChgID.Value = "RG1200"
                beEmpRetireLog_2.ApplyDate.Value = Now
                beEmpRetireLog_2.TransferDate.Value = strEmpDate
                beEmpRetireLog_2.DeclareDate.Value = strEmpDate
                beEmpRetireLog_2.DownDate.Value = strEmpDate
                strOldData = objRegistData.funEncryptNumber(strEmpID, "-")
                strNewData = objRegistData.funEncryptNumber(strEmpID, "1")
                beEmpRetireLog_2.OldData.Value = strOldData
                beEmpRetireLog_2.NewData.Value = strNewData

                bsPersonal.Update(bePersonal, tran)                     '1. 更新人事主檔Personal
                bsSalaryData.Update(beSalaryData, tran)                 '2. 寫入員工薪資資料檔SalaryData
                '3. 寫入員工入帳帳號檔案EmpAccount
                If Trim(myDataRead(nintPrincipalBankID).ToString) > "0" And Trim(myDataRead(nintPrincipalAccount).ToString) > "0" Then
                    bsEmpAccount.Insert(beEmpAccount, tran)
                End If
                bsSalary_cnMealPay.Insert(beSalary_cnMealPay, tran)     '4. 寫入薪資主檔Salary(伙食)
                bsSalary_cnAnnualPay.Insert(beSalary_cnAnnualPay, tran) '5. 寫入薪資主檔Salary(年薪)
                bsInsureWait.Insert(beInsureWait, tran)                 '6. 寫入保險待加退保檔InsureWait
                bsGroupWait.Insert(beGroupWait, tran)                   '7. 寫入團保待加退保檔GroupWait
                '8. 更新勞退主檔EmpRetire    '******************************************************
                If mOldAmount <> mNewAmount Then '三個月平均工資有異動
                    objRG.UpdateEmpRetire(strCompID, strEmpID, objRegistData.funEncryptNumber(strEmpID, mNewAmount))
                End If
                bsEmpRetireLog.Insert(beEmpRetireLog_1, tran)           '9. 寫入員工退休金主檔異動紀錄 EmpRetireLog
                bsEmpRetireLog.Insert(beEmpRetireLog_2, tran)           '10. 勞退級距有異動需加 EmpRetireLog

                tran.Commit()
                inTrans = False
                Return True
            Catch ex As Exception
                If inTrans Then tran.Rollback()
                ErrMsg = ex.Message
                'Bsp.Utility.ShowMessage(Me, Me.FunID & ".funUploadDataSingle_Salary", ex)
                Return False
            End Try
        End Using
    End Function
#End Region

#Region "3.員工教育資料 Education"
    Private Function funCheckData_Education(ByVal FileName As String) As Boolean
        Dim CompID As String = UserProfile.SelectCompRoleID
        Dim objRG As New RG1
        Dim objHR As New HR
        Dim bolInputStatus As Boolean = True
        Dim strErrMsg As String = ""
        Dim strSqlWhere As String = ""
        Dim bolCheckData As Boolean = False  '檢核資料

        '補充說明：欄位非必填且放空白	
        Dim nintCompID As Integer = 0
        Dim nintEmpID As Integer = 1
        Dim nintEduID As Integer = 2
        Dim nintEduStatus As Integer = 3
        Dim nintGraduateYear As Integer = 4
        Dim nintSchoolType As Integer = 5
        Dim nintSchoolID As Integer = 6
        Dim nintDepartID As Integer = 7
        Dim nintSecDepartID As Integer = 8
        Dim nintSchool As Integer = 9
        Dim nintDepart As Integer = 10
        Dim nintSecDepart As Integer = 11

        intTotalCount = 0
        intSuccessCount = 0
        intErrorCount = 0

        Dim strExcelConn As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & FileName & ";Extended Properties=EXCEL 8.0"

        Dim strExcelWorkSheet As String = "[RG1200_Education$]"
        Dim strExcelSelect As String = "SELECT * FROM " & strExcelWorkSheet

        Dim myExcelConn As OleDbConnection = Nothing
        myExcelConn = New OleDbConnection(strExcelConn)
        Dim myExcelCommand As OleDbCommand = New OleDbCommand(strExcelSelect, myExcelConn)
        Try
            myExcelConn.Open()

            Using myDataRead As OleDbDataReader = myExcelCommand.ExecuteReader
                While myDataRead.Read

                    '檢核上傳資料
                    strErrMsg = ""
                    bolCheckData = True

                    '先判斷是否上傳欄位個數正確
                    If myDataRead.FieldCount <> 12 Then
                        strErrMsg = strErrMsg & "==> 上傳資料欄位個數不正確！"
                        intErrorCount = intErrorCount + 1
                        bolCheckData = False
                    Else
                        '公司代碼與授權公司代碼不同
                        If CompID <> myDataRead(nintCompID).ToString Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCompID).ToString) & " 公司代碼與授權公司代碼不同!"
                            bolCheckData = False
                        End If

                        '公司代碼
                        If Trim(myDataRead(nintCompID).ToString) = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCompID).ToString) & " 公司代碼未輸入!"
                            bolCheckData = False
                        Else
                            strSqlWhere = ""
                            strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                            If Not objRG.IsDataExists("Company", strSqlWhere) Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCompID).ToString) & " 查無公司代碼!"
                                bolCheckData = False
                            End If
                        End If

                        '員工編號
                        If myDataRead(nintEmpID).ToString().Trim() = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintEmpID).ToString) & " 員工編號未輸入!"
                            bolCheckData = False
                        Else
                            strSqlWhere = ""
                            strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                            strSqlWhere = strSqlWhere & " and EmpID =" & Bsp.Utility.Quote(Trim(myDataRead(nintEmpID).ToString))
                            If Not objRG.IsDataExists("Personal", strSqlWhere) Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintEmpID).ToString) & " 員工編號不存在"
                                bolCheckData = False
                            Else
                                strSqlWhere = ""
                                strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                                strSqlWhere = strSqlWhere & " and EmpID =" & Bsp.Utility.Quote(Trim(myDataRead(nintEmpID).ToString))
                                strSqlWhere = strSqlWhere & " and WorkStatus <> '1'"
                                If objRG.IsDataExists("Personal", strSqlWhere) Then
                                    strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintEmpID).ToString) & " 員工非在職狀態"
                                    bolCheckData = False
                                End If
                            End If
                        End If

                        '學歷代碼
                        If Trim(myDataRead(nintEduID).ToString) <> "" Then
                            strSqlWhere = ""
                            strSqlWhere = strSqlWhere & " and EduID =" & Bsp.Utility.Quote(Trim(myDataRead(nintEduID).ToString))
                            If Not objRG.IsDataExists("EduDegree", strSqlWhere) Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintEduID).ToString) & " 學歷代碼錯誤"
                                bolCheckData = False
                            End If
                        End If

                        '學歷狀態
                        If myDataRead(nintEduStatus).ToString().Trim() = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintEduStatus).ToString) & " 學歷狀態未輸入!"
                            bolCheckData = False
                        Else
                            Dim EduStatus As String = Trim(myDataRead(nintEduStatus).ToString)
                            If EduStatus <> "1" And EduStatus <> "2" And EduStatus <> "9" Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintEduStatus).ToString) & " 學歷狀態1畢業，2就學中，9肆業"
                                bolCheckData = False
                            End If
                        End If

                        '畢業年度
                        If Trim(myDataRead(nintGraduateYear).ToString) <> "" Then
                            If Not IsNumeric(myDataRead(nintGraduateYear)) Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintGraduateYear).ToString) & " 畢業年度請輸入數字"
                                bolCheckData = False
                            End If
                        End If

                        '學校類別代碼
                        If Trim(myDataRead(nintSchoolType).ToString) <> "" Then
                            strSqlWhere = ""
                            strSqlWhere = strSqlWhere & " and TabName='Education' and FldName='SchoolType' and NotShowFlag='0' and Code = " & Bsp.Utility.Quote(Trim(myDataRead(nintSchoolType).ToString))
                            If Not objRG.IsDataExists("HRCodeMap", strSqlWhere) Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintSchoolType).ToString) & " 學校類別不存在"
                                bolCheckData = False
                            End If
                        End If

                        '學校代碼
                        If Trim(myDataRead(nintSchoolID).ToString) <> "" Then
                            strSqlWhere = ""
                            strSqlWhere = strSqlWhere & " and SchoolID =" & Bsp.Utility.Quote(Trim(myDataRead(nintSchoolID).ToString))
                            If Not objRG.IsDataExists("School", strSqlWhere) Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintSchoolID).ToString) & " 學校代碼不存在"
                                bolCheckData = False
                            End If
                        End If

                        '科系名稱代碼
                        If Trim(myDataRead(nintDepartID).ToString) <> "" Then
                            strSqlWhere = ""
                            strSqlWhere = strSqlWhere & " and DepartID =" & Bsp.Utility.Quote(Trim(myDataRead(nintDepartID).ToString))
                            If Not objRG.IsDataExists("Depart ", strSqlWhere) Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintDepartID).ToString) & " 科系代碼不存在"
                                bolCheckData = False
                            End If
                        End If

                        '第二科系名稱代碼
                        If Trim(myDataRead(nintSecDepartID).ToString) <> "" Then
                            strSqlWhere = ""
                            strSqlWhere = strSqlWhere & " and DepartID =" & Bsp.Utility.Quote(Trim(myDataRead(nintSecDepartID).ToString))
                            If Not objRG.IsDataExists("Depart ", strSqlWhere) Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintSecDepartID).ToString) & " 輔科系代碼不存在"
                                bolCheckData = False
                            End If
                        End If

                        '學校名稱
                        If Bsp.Utility.getStringLength(Trim(myDataRead(nintSchool).ToString)) > 50 Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintSchool).ToString) & " 學校名稱最多25個中文字!"
                            bolCheckData = False
                        End If

                        '科系名稱
                        If Bsp.Utility.getStringLength(Trim(myDataRead(nintDepart).ToString)) > 50 Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintDepart).ToString) & " 科系名稱最多25個中文字!"
                            bolCheckData = False
                        End If

                        '第二科系名稱
                        If Bsp.Utility.getStringLength(Trim(myDataRead(nintSecDepart).ToString)) > 50 Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintSecDepart).ToString) & " 第二科系名稱最多25個中文字!"
                            bolCheckData = False
                        End If

                        If bolCheckData Then
                            Dim ErrMsg As String = ""
                            If funUploadDataSingle_Education(myDataRead, ErrMsg) Then
                                intSuccessCount = intSuccessCount + 1
                            Else
                                intErrorCount = intErrorCount + 1
                            End If

                            If ErrMsg <> "" Then
                                strErrMsg = strErrMsg & "==> " & ErrMsg
                            End If
                        Else
                            intErrorCount = intErrorCount + 1
                        End If

                    End If

                    If strErrMsg <> "" Then
                        subWriteLog("資料列:" & intTotalCount + 1 & " 公司代碼：" & Trim(myDataRead(nintCompID).ToString) _
                                    & " 員工編號：" & Trim(myDataRead(nintEmpID).ToString) _
                                    & " 學歷代碼：" & Trim(myDataRead(nintEduID).ToString) _
                                    & " 學歷狀態：" & Trim(myDataRead(nintEduStatus).ToString) _
                                    & " 畢業年度：" & Trim(myDataRead(nintGraduateYear).ToString) _
                                    & " 學校類別代碼：" & Trim(myDataRead(nintSchoolType).ToString) _
                                    & " 學校代碼：" & Trim(myDataRead(nintSchoolID).ToString) _
                                    & " 科系名稱代碼：" & Trim(myDataRead(nintDepartID).ToString) _
                                    & " 輔科系代碼：" & Trim(myDataRead(nintSecDepartID).ToString) _
                                    & strErrMsg)
                        bolInputStatus = False
                    End If

                    intTotalCount = intTotalCount + 1

                End While
            End Using

            myExcelConn.Close()

            If bolInputStatus = False Then
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            myExcelConn.Close()
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".funCheckData1", ex)
            Return False
        End Try
    End Function

    '資料上傳 單筆
    Private Function funUploadDataSingle_Education(ByVal myDataRead As OleDbDataReader, ByRef ErrMsg As String) As Boolean
        Dim bsEducation As New beEducation.Service()
        Dim beEducation As New beEducation.Row
        Dim bsPersonal As New bePersonal.Service()
        Dim bePersonal As New bePersonal.Row
        Dim dtNow = DateTime.Now
        Dim objHR As New HR
        Dim objRG As New RG1

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()

            Dim tran As DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Dim nintCompID As Integer = 0
            Dim nintEmpID As Integer = 1
            Dim nintEduID As Integer = 2
            Dim nintEduStatus As Integer = 3
            Dim nintGraduateYear As Integer = 4
            Dim nintSchoolType As Integer = 5
            Dim nintSchoolID As Integer = 6
            Dim nintDepartID As Integer = 7
            Dim nintSecDepartID As Integer = 8
            Dim nintSchool As Integer = 9
            Dim nintDepart As Integer = 10
            Dim nintSecDepart As Integer = 11

            Dim intLoop As Integer = 0

            Try
                '員工身分證字號
                beEducation.IDNo.Value = objHR.GetIDNo(myDataRead(nintCompID).ToString, myDataRead(nintEmpID).ToString)

                '學歷代碼
                beEducation.EduID.Value = myDataRead(nintEduID).ToString

                '學歷狀態
                beEducation.EduStatus.Value = myDataRead(nintEduStatus).ToString

                '序號
                Dim strSeq As String = objRG.GetSeq("Education", " AND IDNo = " & Bsp.Utility.Quote(Trim(objHR.GetIDNo(myDataRead(nintCompID).ToString, myDataRead(nintEmpID).ToString))) & " AND EduID = " & Bsp.Utility.Quote(Trim(myDataRead(nintEduID).ToString)))    '2015/12/08 Modify
                beEducation.Seq.Value = IIf(strSeq = "", "1", strSeq)   '2015/12/11 Modify 序號預設為1

                '畢業年度
                beEducation.GraduateYear.Value = IIf(myDataRead(nintGraduateYear).ToString = "", 0, myDataRead(nintGraduateYear))

                '學校類別代碼
                beEducation.SchoolType.Value = IIf(myDataRead(nintSchoolType).ToString = "", 0, myDataRead(nintSchoolType))

                '校名代碼
                beEducation.SchoolID.Value = myDataRead(nintSchoolID).ToString

                '科系代碼
                beEducation.DepartID.Value = myDataRead(nintDepartID).ToString

                '輔系代碼
                beEducation.SecDepartID.Value = myDataRead(nintSecDepartID).ToString

                '校名
                Dim School As String
                If myDataRead(nintSchoolID).ToString <> "" Then
                    School = objRG.GetName("School", "SchoolID = " & Bsp.Utility.Quote(Trim(myDataRead(nintSchoolID).ToString)))
                Else
                    School = myDataRead(nintSchool).ToString()
                End If
                beEducation.School.Value = School

                '科系
                Dim Depart As String
                If myDataRead(nintDepartID).ToString <> "" Then
                    Depart = objRG.GetName("Depart", "DepartID = " & Bsp.Utility.Quote(Trim(myDataRead(nintDepartID).ToString)))
                Else
                    Depart = myDataRead(nintDepart).ToString()
                End If
                beEducation.Depart.Value = Depart

                '輔系
                Dim SecDepart As String
                If myDataRead(nintSecDepart).ToString <> "" Then
                    SecDepart = objRG.GetName("Depart", "DepartID = " & Bsp.Utility.Quote(Trim(myDataRead(nintSecDepartID).ToString)))
                Else
                    SecDepart = myDataRead(nintSecDepart).ToString()
                End If
                beEducation.SecDepart.Value = SecDepart

                '最後異動
                beEducation.LastChgComp.Value = "RG1200"
                beEducation.LastChgID.Value = "RG1200"
                'beEducation.LastChgComp.Value = UserProfile.ActCompID
                'beEducation.LastChgID.Value = UserProfile.ActUserID
                beEducation.LastChgDate.Value = Now

                'Personal
                bePersonal.CompID.Value = Trim(myDataRead(nintCompID).ToString)
                bePersonal.EmpID.Value = Trim(myDataRead(nintEmpID).ToString)
                bePersonal.EduID.Value = Trim(myDataRead(nintEduID).ToString)
                'bePersonal.LastChgComp.Value = UserProfile.ActCompID
                'bePersonal.LastChgID.Value = UserProfile.ActUserID
                bePersonal.LastChgComp.Value = "RG1200"
                bePersonal.LastChgID.Value = "RG1200"
                bePersonal.LastChgDate.Value = Now


                bsEducation.Insert(beEducation, tran)
                bsPersonal.Update(bePersonal, tran)

                tran.Commit()
                inTrans = False
                Return True

            Catch ex As Exception
                If inTrans Then tran.Rollback()
                ErrMsg = ex.Message
                Return False
                'Bsp.Utility.ShowMessage(Me, Me.FunID & ".funUploadDataSingle_Education", ex)
            End Try
        End Using
    End Function
#End Region

#Region "4.員工前職經歷資料 Experience"
    Private Function funCheckData_Experience(ByVal FileName As String) As Boolean
        Dim CompID As String = UserProfile.SelectCompRoleID
        Dim objRG As New RG1
        Dim objHR As New HR
        Dim bolInputStatus As Boolean = True
        Dim strErrMsg As String = ""
        Dim strSqlWhere As String = ""
        Dim bolCheckData As Boolean = False  '檢核資料

        '補充說明：欄位非必填且放空白	
        Dim nintCompID As Integer = 0       '公司代碼
        Dim nintEmpID As Integer = 1        '員工編號
        Dim nintBeginDate As Integer = 2    '起日
        Dim nintEndDate As Integer = 3      '迄日
        Dim nintIndustryType As Integer = 4 '產業類別
        Dim nintCompany As Integer = 5      '公司名稱
        Dim nintDepartment As Integer = 6   '部門
        Dim nintTitle As Integer = 7        '職稱
        Dim nintWorkType As Integer = 8     '工作性質
        Dim nintWorkYear As Integer = 9     '年資
        Dim nintProfession As Integer = 10  '專業註記

        intTotalCount = 0
        intSuccessCount = 0
        intErrorCount = 0

        Dim strExcelConn As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & FileName & ";Extended Properties=EXCEL 8.0"

        Dim strExcelWorkSheet As String = "[RG1200_Experience$]"
        Dim strExcelSelect As String = "SELECT * FROM " & strExcelWorkSheet

        Dim myExcelConn As OleDbConnection = Nothing
        myExcelConn = New OleDbConnection(strExcelConn)
        Dim myExcelCommand As OleDbCommand = New OleDbCommand(strExcelSelect, myExcelConn)
        Try
            myExcelConn.Open()

            Using myDataRead As OleDbDataReader = myExcelCommand.ExecuteReader
                While myDataRead.Read

                    '檢核上傳資料
                    strErrMsg = ""
                    bolCheckData = True

                    '先判斷是否上傳欄位個數正確
                    If myDataRead.FieldCount <> 11 Then
                        strErrMsg = strErrMsg & "==> 上傳資料欄位個數不正確！"
                        intErrorCount = intErrorCount + 1
                        bolCheckData = False
                    Else
                        '公司代碼與授權公司代碼不同
                        If CompID <> myDataRead(nintCompID).ToString Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCompID).ToString) & " 公司代碼與授權公司代碼不同!"
                            bolCheckData = False
                        End If

                        '公司代碼
                        If Trim(myDataRead(nintCompID).ToString) = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCompID).ToString) & " 公司代碼未輸入!"
                            bolCheckData = False
                        Else
                            strSqlWhere = ""
                            strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                            If Not objRG.IsDataExists("Company", strSqlWhere) Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCompID).ToString) & " 查無公司代碼!"
                                bolCheckData = False
                            End If
                        End If

                        '員工編號
                        If myDataRead(nintEmpID).ToString().Trim() = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintEmpID).ToString) & " 員工編號未輸入!"
                            bolCheckData = False
                        Else
                            strSqlWhere = ""
                            strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                            strSqlWhere = strSqlWhere & " and EmpID =" & Bsp.Utility.Quote(Trim(myDataRead(nintEmpID).ToString))
                            If Not objRG.IsDataExists("Personal", strSqlWhere) Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintEmpID).ToString) & " 員工編號不存在"
                                bolCheckData = False
                            Else
                                strSqlWhere = ""
                                strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                                strSqlWhere = strSqlWhere & " and EmpID =" & Bsp.Utility.Quote(Trim(myDataRead(nintEmpID).ToString))
                                strSqlWhere = strSqlWhere & " and WorkStatus <> '1'"
                                If objRG.IsDataExists("Personal", strSqlWhere) Then
                                    strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintEmpID).ToString) & " 員工非在職狀態"
                                    bolCheckData = False
                                End If
                            End If
                        End If

                        '起日
                        If Trim(myDataRead(nintBeginDate).ToString) <> "" Then
                            If Not IsDate(Trim(myDataRead(nintBeginDate).ToString)) Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintBeginDate).ToString) & " [起日]請輸入西曆YYYY/MM/DD"
                                bolCheckData = False
                            End If
                        End If

                        '迄日
                        If Trim(myDataRead(nintEndDate).ToString) <> "" Then
                            If Not IsDate(Trim(myDataRead(nintEndDate).ToString)) Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintEndDate).ToString) & " [迄日]請輸入西曆YYYY/MM/DD"
                                bolCheckData = False
                            End If
                        End If

                        '產業類別
                        If Trim(myDataRead(nintIndustryType).ToString) <> "" Then
                            If Bsp.Utility.getStringLength(Trim(myDataRead(nintIndustryType).ToString)) > 2 Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintEndDate).ToString) & " 產業類別最多2個字"
                                bolCheckData = False
                            End If
                        End If

                        '公司名稱
                        If Trim(myDataRead(nintCompany).ToString) <> "" Then
                            If Bsp.Utility.getStringLength(Trim(myDataRead(nintCompany).ToString)) > 50 Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCompany).ToString) & " 公司名稱最多25個中文字"
                                bolCheckData = False
                            End If
                        End If

                        '部門
                        If Trim(myDataRead(nintDepartment).ToString) <> "" Then
                            If Bsp.Utility.getStringLength(Trim(myDataRead(nintDepartment).ToString)) > 50 Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintDepartment).ToString) & " 部門最多25個中文字"
                                bolCheckData = False
                            End If
                        End If

                        '職稱
                        If Trim(myDataRead(nintTitle).ToString) <> "" Then
                            If Bsp.Utility.getStringLength(Trim(myDataRead(nintTitle).ToString)) > 20 Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintTitle).ToString) & " 職稱最多10個中文字"
                                bolCheckData = False
                            End If
                        End If

                        '工作性質
                        If Trim(myDataRead(nintWorkType).ToString) <> "" Then
                            If Trim(myDataRead(nintWorkType).ToString).Length > 410 Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintWorkType).ToString) & " 工作性質最多410個中文字"
                                bolCheckData = False
                            End If
                        End If

                        '年資 '2015/12/11 Modify
                        If Trim(myDataRead(nintWorkYear).ToString) = "" Then
                            If Trim(myDataRead(nintBeginDate).ToString) <> "" And Trim(myDataRead(nintEndDate).ToString) <> "" Then
                                Dim WorkYear As Integer = DateDiff("d", Trim(myDataRead(nintBeginDate).ToString), Trim(myDataRead(nintEndDate).ToString)) / 365
                                If WorkYear < 0 Then
                                    strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintWorkYear).ToString) & " 年資不可小於0"
                                    bolCheckData = False
                                End If
                            End If
                        End If

                        '專業註記
                        If Trim(myDataRead(nintProfession).ToString) <> "" Then
                            If Trim(myDataRead(nintProfession).ToString) <> "0" And Trim(myDataRead(nintProfession).ToString) <> "1" Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintProfession).ToString) & " 專業註記0 : 非專業，1 : 專業"
                                bolCheckData = False
                            End If
                        End If

                        If bolCheckData Then
                            Dim ErrMsg As String = ""
                            If funUploadDataSingle_Experience(myDataRead, ErrMsg) Then
                                intSuccessCount = intSuccessCount + 1
                            Else
                                intErrorCount = intErrorCount + 1
                            End If

                            If ErrMsg <> "" Then
                                strErrMsg = strErrMsg & "==> " & ErrMsg
                            End If
                        Else
                            intErrorCount = intErrorCount + 1
                        End If

                    End If

                    If strErrMsg <> "" Then
                        subWriteLog("資料列:" & intTotalCount + 1 & " 公司代碼：" & Trim(myDataRead(nintCompID).ToString) _
                                    & " 員工編號：" & Trim(myDataRead(nintEmpID).ToString) _
                                    & " 起日：" & Trim(myDataRead(nintBeginDate).ToString) _
                                    & " 迄日：" & Trim(myDataRead(nintEndDate).ToString) _
                                    & " 產業類別：" & Trim(myDataRead(nintIndustryType).ToString) _
                                    & " 公司名稱：" & Trim(myDataRead(nintCompany).ToString) _
                                    & " 部門：" & Trim(myDataRead(nintDepartment).ToString) _
                                    & " 職稱：" & Trim(myDataRead(nintTitle).ToString) _
                                    & " 工作性質：" & Trim(myDataRead(nintWorkType).ToString) _
                                    & " 年資：" & Trim(myDataRead(nintWorkYear).ToString) _
                                    & " 專業註記：" & Trim(myDataRead(nintProfession).ToString) _
                                    & strErrMsg)
                        bolInputStatus = False
                    End If

                    intTotalCount = intTotalCount + 1

                End While
            End Using

            myExcelConn.Close()

            If bolInputStatus = False Then
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            myExcelConn.Close()
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".funCheckData1", ex)
            Return False
        End Try
    End Function

    '資料上傳 單筆
    Private Function funUploadDataSingle_Experience(ByVal myDataRead As OleDbDataReader, ByRef ErrMsg As String) As Boolean
        Dim bsExperience As New beExperience.Service()
        Dim beExperience As New beExperience.Row
        Dim dtNow = DateTime.Now
        Dim objHR As New HR
        Dim objRG As New RG1

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()

            Dim tran As DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Dim nintCompID As Integer = 0       '公司代碼
            Dim nintEmpID As Integer = 1        '員工編號
            Dim nintBeginDate As Integer = 2    '起日
            Dim nintEndDate As Integer = 3      '迄日
            Dim nintIndustryType As Integer = 4 '產業類別
            Dim nintCompany As Integer = 5      '公司名稱
            Dim nintDepartment As Integer = 6   '部門
            Dim nintTitle As Integer = 7        '職稱
            Dim nintWorkType As Integer = 8     '工作性質
            Dim nintWorkYear As Integer = 9     '年資
            Dim nintProfession As Integer = 10  '專業註記

            Dim intLoop As Integer = 0

            Try
                beExperience.IDNo.Value = objHR.GetIDNo(myDataRead(nintCompID).ToString, myDataRead(nintEmpID).ToString)
                Dim strSeq As String = objRG.GetSeq("Experience", " AND IDNo = " & Bsp.Utility.Quote(Trim(objHR.GetIDNo(myDataRead(nintCompID).ToString, myDataRead(nintEmpID).ToString))))    '2015/12/08 Modify
                beExperience.Seq.Value = IIf(strSeq = "", "1", strSeq)   '2015/12/11 Modify 序號預設為1
                beExperience.BeginDate.Value = IIf(myDataRead(nintBeginDate).ToString = "", "1900/01/01", Bsp.Utility.CheckDate(Trim(myDataRead(nintBeginDate).ToString)))
                beExperience.EndDate.Value = IIf(myDataRead(nintEndDate).ToString = "", "1900/01/01", Bsp.Utility.CheckDate(Trim(myDataRead(nintEndDate).ToString)))
                beExperience.IndustryType.Value = myDataRead(nintIndustryType).ToString
                beExperience.Company.Value = myDataRead(nintCompany).ToString
                beExperience.Department.Value = myDataRead(nintDepartment).ToString
                beExperience.Title.Value = myDataRead(nintTitle).ToString
                beExperience.WorkType.Value = myDataRead(nintWorkType).ToString

                '2015/12/11 Modify 年資
                Dim WorkYear As Integer = 0
                If Trim(myDataRead(nintBeginDate).ToString) <> "" And Trim(myDataRead(nintEndDate).ToString) <> "" Then
                    WorkYear = DateDiff("d", Trim(myDataRead(nintBeginDate).ToString), Trim(myDataRead(nintEndDate).ToString)) / 365
                End If
                beExperience.WorkYear.Value = IIf(myDataRead(nintWorkYear).ToString = "", WorkYear, myDataRead(nintWorkYear).ToString)
                beExperience.Profession.Value = myDataRead(nintProfession).ToString

                '最後異動
                beExperience.LastChgComp.Value = "RG1200"
                beExperience.LastChgID.Value = "RG1200"
                'beExperience.LastChgComp.Value = UserProfile.ActCompID
                'beExperience.LastChgID.Value = UserProfile.ActUserID
                beExperience.LastChgDate.Value = Now

                bsExperience.Insert(beExperience, tran)

                tran.Commit()
                inTrans = False
                Return True

            Catch ex As Exception
                If inTrans Then tran.Rollback()
                'Bsp.Utility.ShowMessage(Me, Me.FunID & ".funUploadDataSingle_Experience", ex)
                ErrMsg = ex.Message
                Return False
            End Try
        End Using
    End Function
#End Region

#Region "5.員工勞退自提比率資料 EmpRatio"
    Private Function funCheckData_EmpRatio(ByVal FileName As String) As Boolean
        Dim CompID As String = UserProfile.SelectCompRoleID
        Dim objRG As New RG1
        Dim objHR As New HR
        Dim bolInputStatus As Boolean = True
        Dim strErrMsg As String = ""
        Dim strSqlWhere As String = ""
        Dim bolCheckData As Boolean = False  '檢核資料

        '補充說明：欄位非必填且放空白	
        Dim nintCompID As Integer = 0       '公司代碼
        Dim nintEmpID As Integer = 1        '員工編號
        Dim nintEmpRatio As Integer = 2     '勞退自提比率

        intTotalCount = 0
        intSuccessCount = 0
        intErrorCount = 0

        Dim strExcelConn As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & FileName & ";Extended Properties=EXCEL 8.0"

        Dim strExcelWorkSheet As String = "[RG1200_EmpRatio$]"
        Dim strExcelSelect As String = "SELECT * FROM " & strExcelWorkSheet

        Dim myExcelConn As OleDbConnection = Nothing
        myExcelConn = New OleDbConnection(strExcelConn)
        Dim myExcelCommand As OleDbCommand = New OleDbCommand(strExcelSelect, myExcelConn)
        Try
            myExcelConn.Open()

            Using myDataRead As OleDbDataReader = myExcelCommand.ExecuteReader
                While myDataRead.Read

                    '檢核上傳資料
                    strErrMsg = ""
                    bolCheckData = True

                    '先判斷是否上傳欄位個數正確
                    If myDataRead.FieldCount <> 3 Then
                        strErrMsg = strErrMsg & "==> 上傳資料欄位個數不正確！"
                        intErrorCount = intErrorCount + 1
                        bolCheckData = False
                    Else
                        '公司代碼與授權公司代碼不同
                        If CompID <> myDataRead(nintCompID).ToString Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCompID).ToString) & " 公司代碼與授權公司代碼不同!"
                            bolCheckData = False
                        End If

                        '公司代碼
                        If Trim(myDataRead(nintCompID).ToString) = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCompID).ToString) & " 公司代碼未輸入!"
                            bolCheckData = False
                        Else
                            strSqlWhere = ""
                            strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                            If Not objRG.IsDataExists("Company", strSqlWhere) Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCompID).ToString) & " 查無公司代碼!"
                                bolCheckData = False
                            End If
                        End If

                        '員工編號
                        If myDataRead(nintEmpID).ToString().Trim() = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintEmpID).ToString) & " 員工編號未輸入!"
                            bolCheckData = False
                        Else
                            strSqlWhere = ""
                            strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                            strSqlWhere = strSqlWhere & " and EmpID =" & Bsp.Utility.Quote(Trim(myDataRead(nintEmpID).ToString))
                            If Not objRG.IsDataExists("Personal", strSqlWhere) Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintEmpID).ToString) & " 員工編號不存在"
                                bolCheckData = False
                            Else
                                strSqlWhere = ""
                                strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                                strSqlWhere = strSqlWhere & " and EmpID =" & Bsp.Utility.Quote(Trim(myDataRead(nintEmpID).ToString))
                                strSqlWhere = strSqlWhere & " and WorkStatus <> '1'"
                                If objRG.IsDataExists("Personal", strSqlWhere) Then
                                    strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintEmpID).ToString) & " 員工非在職狀態"
                                    bolCheckData = False
                                End If
                            End If
                        End If

                        '勞退自提比率
                        If myDataRead(nintEmpRatio).ToString().Trim() <> "" Then
                            If myDataRead(nintEmpRatio).ToString().Trim() = "" Or IsNumeric(myDataRead(nintEmpRatio).ToString().Trim()) = False Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintEmpRatio).ToString) & " 新制退休金自提比率，請輸入在0.0% ~ 6.0%範圍內"
                                bolCheckData = False
                            End If
                            If myDataRead(nintEmpRatio).ToString().Trim() < 0.0 Or myDataRead(nintEmpRatio).ToString().Trim() > 6 Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintEmpRatio).ToString) & " 新制退休金自提比率，請輸入在0.0% ~ 6.0%範圍內"
                                bolCheckData = False
                            End If
                        End If

                        '檢查員工最後異動是否由RG1200而來
                        Dim LastChgComp As String = objRG.QueryData("Personal", " AND CompID = " & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString)) & " AND EmpID = " & Bsp.Utility.Quote(Trim(myDataRead(nintEmpID).ToString)), "LastChgComp")
                        Dim LastChgID As String = objRG.QueryData("Personal", " AND CompID = " & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString)) & " AND EmpID = " & Bsp.Utility.Quote(Trim(myDataRead(nintEmpID).ToString)), "LastChgID")
                        If LastChgComp <> "RG1200" And LastChgID <> "RG1200" Then
                            strErrMsg = strErrMsg & "==> 員工基本資料最後異動記錄非為2320整批作業"
                            bolCheckData = False
                        End If
                        '檢查員工退休金主檔是否有已有資料存在
                        strSqlWhere = ""
                        strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                        strSqlWhere = strSqlWhere & " and EmpID =" & Bsp.Utility.Quote(Trim(myDataRead(nintEmpID).ToString))
                        If Not objRG.IsDataExists("EmpRetire", strSqlWhere) Then
                            strErrMsg = strErrMsg & "==> 員工退休金資料不存在"
                            bolCheckData = False
                        End If

                        If bolCheckData Then
                            Dim ErrMsg As String = ""
                            If funUploadDataSingle_EmpRatio(myDataRead, ErrMsg) Then
                                intSuccessCount = intSuccessCount + 1
                            Else
                                intErrorCount = intErrorCount + 1
                            End If

                            If ErrMsg <> "" Then
                                strErrMsg = strErrMsg & "==> " & ErrMsg
                            End If
                        Else
                            intErrorCount = intErrorCount + 1
                        End If

                    End If

                    If strErrMsg <> "" Then
                        subWriteLog("資料列:" & intTotalCount + 1 _
                                    & " 公司代碼：" & Trim(myDataRead(nintCompID).ToString) _
                                    & " 員工編號：" & Trim(myDataRead(nintEmpID).ToString) _
                                    & " 勞退自提比率：" & Trim(myDataRead(nintEmpRatio).ToString) _
                                    & strErrMsg)
                        bolInputStatus = False
                    End If

                    intTotalCount = intTotalCount + 1

                End While
            End Using

            myExcelConn.Close()

            If bolInputStatus = False Then
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            myExcelConn.Close()
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".funCheckData1", ex)
            Return False
        End Try
    End Function

    '資料上傳 單筆
    Private Function funUploadDataSingle_EmpRatio(ByVal myDataRead As OleDbDataReader, ByRef ErrMsg As String) As Boolean
        Dim bsEmpRetire As New beEmpRetire.Service()
        Dim beEmpRetire As New beEmpRetire.Row
        Dim bsEmpRetireLog As New beEmpRetireLog.Service()
        Dim beEmpRetireLog As New beEmpRetireLog.Row

        Dim dtNow = DateTime.Now
        Dim objHR As New HR
        Dim objRG As New RG1
        Dim mEmpRatio As String = ""

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()

            Dim tran As DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Dim nintCompID As Integer = 0       '公司代碼
            Dim nintEmpID As Integer = 1        '員工編號
            Dim nintEmpRatio As Integer = 2     '勞退自提比率

            Dim intLoop As Integer = 0

            Try
                Dim strCompID As String = myDataRead(nintCompID).ToString
                Dim strEmpID As String = myDataRead(nintEmpID).ToString
                Dim strEmpDate As String = objRG.QueryData("Personal", " AND CompID = " & Bsp.Utility.Quote(strCompID) & " AND EmpID = " & Bsp.Utility.Quote(strEmpID), "Convert(Char(10), EmpDate, 111)")

                mEmpRatio = objRG.QueryData("EmpRetire", " AND CompID = " & Bsp.Utility.Quote(myDataRead(nintCompID).ToString.Trim) & " AND EmpID = " & Bsp.Utility.Quote(myDataRead(nintEmpID).ToString.Trim), "EmpRatio")

                beEmpRetire.CompID.Value = strCompID
                beEmpRetire.EmpID.Value = strEmpID
                beEmpRetire.Kind.Value = "1"
                beEmpRetire.EmpRatio.Value = myDataRead(nintEmpRatio).ToString
                Dim intEmpRatio As Integer = Int(myDataRead(nintEmpRatio).ToString())
                beEmpRetire.ChangeCount.Value = intEmpRatio + 1
                '最後異動
                beEmpRetire.LastChgComp.Value = "RG1200"
                beEmpRetire.LastChgID.Value = "RG1200"
                beEmpRetire.LastChgDate.Value = Now
                bsEmpRetire.Update(beEmpRetire, tran)

                '寫入員工退休金主檔異動記錄
                Dim strOldData As String
                Dim strNewData As String
                beEmpRetireLog.CompID.Value = strCompID
                beEmpRetireLog.EmpID.Value = strEmpID
                beEmpRetireLog.TransferType.Value = "2"
                beEmpRetireLog.LastChgDate.Value = Now
                beEmpRetireLog.LastChgComp.Value = "RG1200"
                beEmpRetireLog.LastChgID.Value = "RG1200"
                beEmpRetireLog.ApplyDate.Value = Now
                beEmpRetireLog.TransferDate.Value = strEmpDate
                beEmpRetireLog.DeclareDate.Value = strEmpDate
                beEmpRetireLog.DownDate.Value = strEmpDate
                strOldData = mEmpRatio
                strNewData = myDataRead(nintEmpRatio).ToString
                beEmpRetireLog.OldData.Value = strOldData
                beEmpRetireLog.NewData.Value = strNewData
                bsEmpRetireLog.Insert(beEmpRetireLog, tran)

                tran.Commit()
                inTrans = False
                Return True
            Catch ex As Exception
                If inTrans Then tran.Rollback()
                ErrMsg = ex.Message
                'Bsp.Utility.ShowMessage(Me, Me.FunID & ".funUploadDataSingle_EmpRatio", ex)
                Return False
            End Try
        End Using
    End Function
#End Region

#Region "6.員工簽核資料 EmpFlow"
    Private Function funCheckData_EmpFlow(ByVal FileName As String) As Boolean
        Dim CompID As String = UserProfile.SelectCompRoleID
        Dim objRG As New RG1
        Dim objHR As New HR
        Dim bolInputStatus As Boolean = True
        Dim strErrMsg As String = ""
        Dim strSqlWhere As String = ""
        Dim bolCheckData As Boolean = False  '檢核資料

        '補充說明：欄位非必填且放空白	
        Dim nintCompID As Integer = 0       '公司代碼
        Dim nintEmpID As Integer = 1        '員工編號
        Dim nintActionID As Integer = 2     '簽核類別
        Dim nintOrganID As Integer = 3      '最小單位代碼
        'Dim nintGroupType As Integer = 4    '事業單位類別
        'Dim nintGroupID As Integer = 5      '事業單位代碼

        intTotalCount = 0
        intSuccessCount = 0
        intErrorCount = 0

        Dim strExcelConn As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & FileName & ";Extended Properties=EXCEL 8.0"
        Dim strExcelWorkSheet As String = "[RG1200_EmpFlow$]"
        Dim strExcelSelect As String = "SELECT * FROM " & strExcelWorkSheet

        Dim myExcelConn As OleDbConnection = Nothing
        myExcelConn = New OleDbConnection(strExcelConn)
        Dim myExcelCommand As OleDbCommand = New OleDbCommand(strExcelSelect, myExcelConn)
        Try
            myExcelConn.Open()

            Using myDataRead As OleDbDataReader = myExcelCommand.ExecuteReader
                While myDataRead.Read

                    '檢核上傳資料
                    strErrMsg = ""
                    bolCheckData = True

                    '先判斷是否上傳欄位個數正確
                    If myDataRead.FieldCount <> 4 Then
                        strErrMsg = strErrMsg & "==> 上傳資料欄位個數不正確！"
                        intErrorCount = intErrorCount + 1
                        bolCheckData = False
                    Else
                        '公司代碼與授權公司代碼不同
                        If CompID <> myDataRead(nintCompID).ToString Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCompID).ToString) & " 上傳資料與選擇公司代碼不符!"
                            bolCheckData = False
                        End If

                        '公司代碼
                        If myDataRead(nintCompID).ToString.Trim = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCompID).ToString) & " 公司代碼不能為空白!"
                            bolCheckData = False
                        Else
                            strSqlWhere = ""
                            strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                            If Not objRG.IsDataExists("Company", strSqlWhere) Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCompID).ToString) & " 查無公司資料!"
                                bolCheckData = False
                            End If
                        End If

                        '員工編號
                        If myDataRead(nintEmpID).ToString().Trim() = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintEmpID).ToString) & " 員工編號未輸入!"
                            bolCheckData = False
                        Else
                            strSqlWhere = ""
                            strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                            strSqlWhere = strSqlWhere & " and EmpID =" & Bsp.Utility.Quote(Trim(myDataRead(nintEmpID).ToString))
                            If Not objRG.IsDataExists("Personal", strSqlWhere) Then
                                strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintEmpID).ToString) & " 員工編號不存在"
                                bolCheckData = False
                            Else
                                strSqlWhere = ""
                                strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                                strSqlWhere = strSqlWhere & " and EmpID =" & Bsp.Utility.Quote(Trim(myDataRead(nintEmpID).ToString))
                                strSqlWhere = strSqlWhere & " and WorkStatus <> '1'"
                                If objRG.IsDataExists("Personal", strSqlWhere) Then
                                    strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintEmpID).ToString) & " 員工非在職狀態"
                                    bolCheckData = False
                                End If
                            End If
                        End If

                        '簽核類別
                        strSqlWhere = ""
                        strSqlWhere = strSqlWhere & " and ActionID =" & Bsp.Utility.Quote(Trim(myDataRead(nintActionID).ToString))
                        If Not objRG.IsDataExists("EmpFlow", strSqlWhere) Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintActionID).ToString) & " 目前只開放上傳請假作業的簽核資料"
                            bolCheckData = False
                        End If

                        '最小單位代碼
                        If myDataRead(nintOrganID).ToString.Trim = "" Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintOrganID).ToString) & " 最小單位代碼不能為空白!"
                            bolCheckData = False
                        Else
                            Dim EmpOrgID As String = objRG.QueryData("Personal", "And CompID = " & Bsp.Utility.Quote(myDataRead(nintCompID).ToString) & " And EmpID = " & Bsp.Utility.Quote(myDataRead(nintEmpID).ToString), "OrganID").Trim
                            Dim FlowOrganIDs As String = objRG.QueryData("OrganizationFlow", "And OrganID = " & Bsp.Utility.Quote(EmpOrgID), "FlowOrganID")
                            Dim IsFlowOrgID As Boolean = False

                            If FlowOrganIDs <> "" Then
                                For Each FlowOrgID In FlowOrganIDs.Split("|")
                                    If FlowOrgID = Trim(myDataRead(nintOrganID).ToString) Then
                                        IsFlowOrgID = True
                                        Exit For
                                    End If
                                Next

                                If Not IsFlowOrgID Then
                                    strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintOrganID).ToString) & " OrganizationFlow查無最小單位資料!"
                                    bolCheckData = False
                                End If
                            Else
                                If EmpOrgID <> Trim(myDataRead(nintOrganID).ToString) Then
                                    strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintOrganID).ToString) & " OrganizationFlow查無最小單位資料!"
                                    bolCheckData = False
                                End If
                            End If

                            'strSqlWhere = ""
                            'strSqlWhere = strSqlWhere & "AND OrganID in (select DeptID from OrganizationFlow where OrganID = " & Bsp.Utility.Quote(Trim(myDataRead(nintOrganID).ToString)) & ")"
                            'If Not objRG.IsDataExists("OrganizationFlow", strSqlWhere) Then
                            '    strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintOrganID).ToString) & " 查無公司資料!"
                            '    bolCheckData = False
                            'End If
                        End If

                        ''事業單位類別
                        'If myDataRead(nintGroupType).ToString.Trim = "" Then
                        '    strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintGroupType).ToString) & " 事業單位類別不能為空白!"
                        '    bolCheckData = False
                        'Else
                        '    strSqlWhere = ""
                        '    strSqlWhere = strSqlWhere & " and TabName='Organization' and FldName='GroupType' and NotShowFlag='0' and Code = " & Bsp.Utility.Quote(Trim(myDataRead(nintGroupType).ToString))
                        '    If Not objRG.IsDataExists("HRCodeMap", strSqlWhere) Then
                        '        strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintGroupType).ToString) & " 事業單位類別錯誤!"
                        '        bolCheckData = False
                        '    End If
                        'End If

                        ''事業單位代碼
                        'If myDataRead(nintGroupID).ToString.Trim = "" Then
                        '    strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintGroupID).ToString) & " 事業單位代碼不能為空白"
                        '    bolCheckData = False
                        'Else
                        '    strSqlWhere = ""
                        '    strSqlWhere = strSqlWhere & " and GroupType = " & Bsp.Utility.Quote(Trim(myDataRead(nintGroupType).ToString))
                        '    strSqlWhere = strSqlWhere & " and OrganID = " & Bsp.Utility.Quote(Trim(myDataRead(nintGroupID).ToString))
                        '    strSqlWhere = strSqlWhere & " and OrganID = GroupID"
                        '    If Not objRG.IsDataExists("OrganizationFlow", strSqlWhere) Then
                        '        strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintGroupID).ToString) & " 無此類別+單位代碼資料"
                        '        bolCheckData = False
                        '    End If
                        'End If

                        If bolCheckData Then
                            Dim ErrMsg As String = ""
                            If funUploadDataSingle_EmpFlow(myDataRead, ErrMsg) Then
                                intSuccessCount = intSuccessCount + 1
                            Else
                                intErrorCount = intErrorCount + 1
                            End If

                            If ErrMsg <> "" Then
                                strErrMsg = strErrMsg & "==> " & ErrMsg
                            End If
                        Else
                            intErrorCount = intErrorCount + 1
                        End If

                    End If

                    If strErrMsg <> "" Then
                        subWriteLog("資料列:" & intTotalCount + 1 & " 公司代碼：" & Trim(myDataRead(nintCompID).ToString) _
                                    & " 員工編號：" & Trim(myDataRead(nintEmpID).ToString) _
                                    & " 簽核類別：" & Trim(myDataRead(nintActionID).ToString) _
                                    & " 最小單位代碼：" & Trim(myDataRead(nintOrganID).ToString) _
                                    & strErrMsg)
                        '& " 事業單位類別：" & Trim(myDataRead(nintGroupType).ToString) _
                        '& " 事業單位代碼：" & Trim(myDataRead(nintGroupID).ToString) _
                        bolInputStatus = False
                    End If

                    intTotalCount = intTotalCount + 1

                End While
            End Using

            myExcelConn.Close()

            If bolInputStatus = False Then
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            myExcelConn.Close()
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".funCheckData1", ex)
            Return False
        End Try
    End Function

    '資料上傳 單筆
    Private Function funUploadDataSingle_EmpFlow(ByVal myDataRead As OleDbDataReader, ByRef ErrMsg As String) As Boolean
        Dim bsEmpFlow As New beEmpFlow.Service()
        Dim beEmpFlow As New beEmpFlow.Row
        Dim bsEmployeeLog As New beEmployeeLog.Service()
        Dim beEmployeeLog As New beEmployeeLog.Row
        Dim bsEmpSenOrgTypeFlow As New beEmpSenOrgTypeFlow.Service()
        Dim beEmpSenOrgTypeFlow As New beEmpSenOrgTypeFlow.Row
        Dim dtNow = DateTime.Now
        Dim objHR As New HR
        Dim objRG As New RG1
        Dim strSqlWhere As String = ""

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()

            Dim tran As DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Dim nintCompID As Integer = 0       '公司代碼
            Dim nintEmpID As Integer = 1        '員工編號
            Dim nintActionID As Integer = 2     '簽核類別
            Dim nintOrganID As Integer = 3      '最小單位代碼
            'Dim nintGroupType As Integer = 4    '事業單位類別
            'Dim nintGroupID As Integer = 5      '事業單位代碼
            Dim intLoop As Integer = 0

            Try
                '公司代碼
                beEmpFlow.CompID.Value = myDataRead(nintCompID).ToString

                '員工編號
                beEmpFlow.EmpID.Value = myDataRead(nintEmpID).ToString

                '簽核類別
                beEmpFlow.ActionID.Value = myDataRead(nintActionID).ToString

                '最小單位代碼
                beEmpFlow.OrganID.Value = myDataRead(nintOrganID).ToString

                '事業單位類別
                beEmpFlow.GroupType.Value = objRG.QueryData("OrganizationFlow", "And OrganID = " & Bsp.Utility.Quote(myDataRead(nintOrganID).ToString), "GroupType") 'myDataRead(nintGroupType).ToString

                '事業單位代碼
                beEmpFlow.GroupID.Value = objRG.QueryData("OrganizationFlow", "And OrganID = " & Bsp.Utility.Quote(myDataRead(nintOrganID).ToString), "GroupID") 'myDataRead(nintGroupID).ToString

                '資料是否重複
                If Trim(myDataRead(nintCompID).ToString) <> "" And Trim(myDataRead(nintEmpID).ToString) <> "" And Trim(myDataRead(nintActionID).ToString) <> "" Then
                    strSqlWhere = ""
                    strSqlWhere = strSqlWhere & " and CompID=" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                    strSqlWhere = strSqlWhere & " and EmpID=" & Bsp.Utility.Quote(Trim(myDataRead(nintEmpID).ToString))
                    strSqlWhere = strSqlWhere & " and ActionID=" & Bsp.Utility.Quote(Trim(myDataRead(nintActionID).ToString))

                    If objRG.IsDataExists("EmpFlow", strSqlWhere) Then
                        '最後異動
                        'beEmpFlow.LastChgComp.Value = UserProfile.ActCompID
                        'beEmpFlow.LastChgID.Value = UserProfile.ActUserID
                        beEmpFlow.LastChgComp.Value = "12006E"  '2015/12/09 Modify
                        beEmpFlow.LastChgID.Value = "12006E"  '2015/12/09 Modify
                        beEmpFlow.LastChgDate.Value = Now

                        bsEmpFlow.Update(beEmpFlow, tran)
                    Else
                        '最後異動
                        'beEmpFlow.LastChgComp.Value = UserProfile.ActCompID
                        'beEmpFlow.LastChgID.Value = UserProfile.ActUserID
                        beEmpFlow.LastChgComp.Value = "12006A"  '2015/12/09 Modify
                        beEmpFlow.LastChgID.Value = "12006A"  '2015/12/09 Modify
                        beEmpFlow.LastChgDate.Value = Now

                        bsEmpFlow.Insert(beEmpFlow, tran)
                    End If
                End If

                '更新員工異動記錄檔EmployeeLog
                Dim IDNo As String = objHR.GetIDNo(myDataRead(nintCompID).ToString, myDataRead(nintEmpID).ToString)
                beEmployeeLog.IDNo.Value = IDNo
                beEmployeeLog.FlowOrganID.Value = Trim(myDataRead(nintOrganID).ToString)
                beEmployeeLog.FlowOrganName.Value = objRG.funGetEmpLogName(Trim(myDataRead(nintOrganID).ToString))
                Dim ModifyDate As String = objRG.GetEmpDate(myDataRead(nintCompID).ToString, myDataRead(nintEmpID).ToString)
                beEmployeeLog.ModifyDate.Value = ModifyDate
                beEmployeeLog.Reason.Value = "01"
                beEmployeeLog.LastChgComp.Value = "12006E"    '2015/12/09 Modify
                beEmployeeLog.LastChgID.Value = "12006E"      '2015/12/09 Modify
                'beEmployeeLog.LastChgComp.Value = UserProfile.ActCompID
                'beEmployeeLog.LastChgID.Value = UserProfile.ActUserID
                beEmployeeLog.LastChgDate.Value = Now
                bsEmployeeLog.Update(beEmployeeLog, tran)

                '寫入簽核單位年資檔EmpSenOrgTypeFlow
                If Trim(myDataRead(nintCompID).ToString) <> "" And Trim(myDataRead(nintEmpID).ToString) <> "" And ModifyDate <> "" Then
                    strSqlWhere = ""
                    strSqlWhere = strSqlWhere & " and CompID=" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                    strSqlWhere = strSqlWhere & " and EmpID=" & Bsp.Utility.Quote(Trim(myDataRead(nintEmpID).ToString))
                    strSqlWhere = strSqlWhere & " and convert(char(10), ValidDateB, 111)=" & Bsp.Utility.Quote(ModifyDate)

                    If objRG.IsDataExists("EmpSenOrgTypeFlow", strSqlWhere) Then
                        beEmpSenOrgTypeFlow.CompID.Value = myDataRead(nintCompID).ToString
                        beEmpSenOrgTypeFlow.EmpID.Value = myDataRead(nintEmpID).ToString
                        beEmpSenOrgTypeFlow.OrgType.Value = objRG.QueryData("OrganizationFlow", "AND OrganID in (select DeptID from OrganizationFlow where OrganID = " & Bsp.Utility.Quote(myDataRead(nintOrganID).ToString) & ")", "OrgType")
                        beEmpSenOrgTypeFlow.ValidDateB.Value = ModifyDate
                        beEmpSenOrgTypeFlow.LastChgComp.Value = "RG1200"
                        beEmpSenOrgTypeFlow.LastChgID.Value = "RG1200"
                        'beEmpSenOrgTypeFlow.LastChgComp.Value = UserProfile.ActCompID
                        'beEmpSenOrgTypeFlow.LastChgID.Value = UserProfile.ActUserID
                        beEmpSenOrgTypeFlow.LastChgDate.Value = Now

                        bsEmpSenOrgTypeFlow.Update(beEmpSenOrgTypeFlow, tran)
                    Else
                        beEmpSenOrgTypeFlow.CompID.Value = myDataRead(nintCompID).ToString
                        beEmpSenOrgTypeFlow.EmpID.Value = myDataRead(nintEmpID).ToString
                        beEmpSenOrgTypeFlow.OrgType.Value = objRG.QueryData("OrganizationFlow", "And OrganID = " & Bsp.Utility.Quote(myDataRead(nintOrganID).ToString), "GroupType") 'myDataRead(nintGroupType).ToString
                        beEmpSenOrgTypeFlow.ValidDateB.Value = ModifyDate
                        beEmpSenOrgTypeFlow.LastChgComp.Value = "RG1200"
                        beEmpSenOrgTypeFlow.LastChgID.Value = "RG1200"
                        'beEmpSenOrgTypeFlow.LastChgComp.Value = UserProfile.ActCompID
                        'beEmpSenOrgTypeFlow.LastChgID.Value = UserProfile.ActUserID
                        beEmpSenOrgTypeFlow.LastChgDate.Value = Now

                        bsEmpSenOrgTypeFlow.Insert(beEmpSenOrgTypeFlow, tran)
                    End If
                End If

                tran.Commit()
                inTrans = False
                Return True

            Catch ex As Exception
                If inTrans Then tran.Rollback()
                ErrMsg = ex.Message
                Return False
                'Bsp.Utility.ShowMessage(Me, Me.FunID & ".funUploadDataSingle_EmpFlow", ex)
            End Try
        End Using
    End Function
#End Region

#Region "放行"
    Private Sub Release(ByVal LogFunction As String)
        ucRelease.ShowCompRole = "True"
        ucRelease.FunID = "RG1200"
        ucRelease.LogFunction = LogFunction
        ucRelease.OpenSelect()
    End Sub

    Public Overrides Sub DoModalReturn(ByVal returnValue As String)
        If returnValue <> "" Then
            Dim aryData() As String = returnValue.Split(":")

            Select Case aryData(0)
                Case "ucRelease"
                    lblReleaseResult.Text = ""
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    lblReleaseResult.Text = aryValue(0)
                    If lblReleaseResult.Text = "Y" Then
                        DoUpload()
                    End If
            End Select

        End If
    End Sub
#End Region

End Class
