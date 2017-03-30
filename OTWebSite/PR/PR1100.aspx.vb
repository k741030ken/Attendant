'****************************************************
'功能說明：計薪前上傳作業
'建立人員：Beatrice
'建立日期：2016.05.16
'****************************************************
Imports System.Data
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.IO
Imports System.IO.Compression
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Class PR_PR1100
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ViewState.Item("SuccessCount") = 0
            ViewState.Item("ErrorCount") = 0
            ViewState.Item("TotalCount") = 0
            ViewState.Item("TotalWarning") = 0

            '公司代碼
            If UserProfile.SelectCompRoleID = "ALL" Then
                lblCompRoleID.Visible = False
                Bsp.Utility.FillHRCompany(ddlCompID)
                Page.SetFocus(ddlCompID)
            Else
                lblCompRoleID.Text = UserProfile.SelectCompRoleName
                ddlCompID.Visible = False
            End If
        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnUpload"  '上傳
                If funCheckData() Then
                    Dim strFileName As String = Bsp.Utility.GetNewFileName("PR1100")
                    Dim strFilePath As String = Server.MapPath(Bsp.Utility.getAppSetting("TempPath")) & "\" & strFileName & ".txt"
                    FileUpload.PostedFile.SaveAs(strFilePath)
                    ViewState.Item("FileUploadFileName") = strFileName & ".txt"

                    Dim strLogFile As String = FileUpload.PostedFile.FileName
                    strLogFile = strLogFile.Substring(strLogFile.LastIndexOf("\") + 1)
                    strLogFile = strLogFile.Substring(0, strLogFile.LastIndexOf("."))
                    ViewState.Item("LogFileName") = strLogFile & strFileName & ".err"

                    Select Case UploadSelect.SelectedValue
                        Case "1"    '1.整批津貼資料
                            Release("Release")
                            'DoUpload()
                    End Select
                End If
        End Select
    End Sub

    Private Sub DoUpload()
        Dim objHR As New HR
        Dim strMessage As String = ""
        Dim strMessageTitle As String = ""
        Dim strUpload_TableName As String = ""
        Dim strWorkSheet As String = ""

        Try
            Dim strFileName As String = Server.MapPath(Bsp.Utility.getAppSetting("TempPath")) & "\" & ViewState.Item("FileUploadFileName")

            strUpload_TableName = objHR.funCheck_UploadTableName(strFileName).Replace("'", "").ToString
            Select Case UploadSelect.SelectedValue
                Case "1"    '1.整批津貼資料
                    strMessageTitle = "整批津貼資料"
                    strWorkSheet = "PR1100_Salary_Allowance"
            End Select

            If strUpload_TableName <> strWorkSheet + "$" Then
                File.Delete(strFileName)
                strMessage = "上傳檔案的工作表(WorkSheet)-" & Mid(strUpload_TableName, 1, Len(strUpload_TableName) - 1) & " 與選擇上傳類型-" + strWorkSheet + "不一致!"
                Bsp.Utility.RunClientScript(Me, "alert('" & String.Format(Bsp.Utility.getMessage("E_00050"), strMessage) & "');" & vbCrLf & "window.top.returnValue = 'OK';")
                Return
            End If

            If objHR.funCheck_UploadCount(strFileName, "[" + strWorkSheet + "$]") > CInt(ConfigurationManager.AppSettings("Upload_MaxCount").ToString) Then
                File.Delete(strFileName)
                strMessage = "上傳資料筆數過大，系統允許最大上傳筆數：" & ConfigurationManager.AppSettings("Upload_MaxCount").ToString
                Bsp.Utility.RunClientScript(Me, "alert('" & String.Format(Bsp.Utility.getMessage("E_00050"), strMessage) & "');" & vbCrLf & "window.top.returnValue = 'OK';")
                Return
            End If

            If funCheckData(strFileName) Then
                File.Delete(strFileName)
                'If UploadSelect.SelectedValue = "1" Then
                '    strMessage = "PR1100" & strMessageTitle & "上傳成功！\n上傳總筆數：" & ViewState.Item("TotalCount") & "\n成功筆數：" & ViewState.Item("SuccessCount") & "\n失敗筆數：" & ViewState.Item("ErrorCount") & "\n警告筆數：" & ViewState.Item("TotalWarning")

                '    If ViewState.Item("ErrorCount") <> 0 Or ViewState.Item("TotalWarning") <> 0 Then
                '        Bsp.Utility.RunClientScript(Me.Page, "IsTODownload('" & strMessage & "');")
                '    Else
                '        Bsp.Utility.RunClientScript(Me, "alert('" & strMessage & "');")
                '    End If
                'Else
                strMessage = "PR1100" & strMessageTitle & "上傳成功！\n上傳總筆數：" & ViewState.Item("TotalCount") & "\n成功筆數：" & ViewState.Item("SuccessCount") & "\n失敗筆數：" & ViewState.Item("ErrorCount")
                Bsp.Utility.RunClientScript(Me, "alert('" & strMessage & "');")
                'End If
            Else
                '刪除上傳檔案
                File.Delete(strFileName)

                'If UploadSelect.SelectedValue = "1" Then
                '    strMessage = "PR1100" & strMessageTitle & "上傳失敗，請下載錯誤記錄Log檔案！\n上傳總筆數：" & ViewState.Item("TotalCount") & "\n成功筆數：" & ViewState.Item("SuccessCount") & "\n失敗筆數：" & ViewState.Item("ErrorCount") & "\n警告筆數：" & ViewState.Item("TotalWarning")
                'Else
                strMessage = "PR1100" & strMessageTitle & "上傳失敗，請下載錯誤記錄Log檔案！\n上傳總筆數：" & ViewState.Item("TotalCount") & "\n成功筆數：" & ViewState.Item("SuccessCount") & "\n失敗筆數：" & ViewState.Item("ErrorCount")
                'End If

                '顯示上傳失敗錯誤訊息，並下載ERR LOG檔案
                Bsp.Utility.RunClientScript(Me.Page, "IsTODownload('" & strMessage & "');")
            End If

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoUpload", ex)
        End Try
    End Sub

    Private Function funCheckData() As Boolean
        '上傳類型
        If UploadSelect.SelectedValue = "" Then
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

    Private Function funCheckData(ByVal FileName As String) As Boolean
        Dim flag As Boolean = False
        Select Case UploadSelect.SelectedValue
            Case "1"    '1.整批津貼資料
                flag = funCheckData_Salary(FileName)
        End Select
        Return flag
    End Function

#Region "ERR LOG檔案"
    '隱藏的按鈕--下傳檔案
    Protected Sub btnUpd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpd.Click
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
        Dim strFileName As String = Server.MapPath(Bsp.Utility.getAppSetting("TempPath")) & "\" & ViewState.Item("LogFileName")
        Using Writer As System.IO.StreamWriter = New System.IO.StreamWriter(strFileName, True, System.Text.Encoding.Unicode)
            Writer.WriteLine(strLogString)
        End Using
    End Sub

    '將ERR LOG檔案立即提供USER端下載，完成後刪除檔案
    '若發生ERR LOG檔案在SERVER有產生，但無法提供下載(USER端無跳出檔案下載視窗畫面)~~WHY??=>IE\工具\網際網路選項\安全性\加入信任的網站 即可!!!!!
    Private Sub subDownloadLogFile()

        '若有多個ShowFormatMessage時，只會跳最後一個
        'Bsp.Utility.ShowFormatMessage(Me, "W_00031", "subDownloadLogFile!")

        Dim strFileName As String = Server.MapPath(Bsp.Utility.getAppSetting("TempPath")) & "\" & ViewState.Item("LogFileName")
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
            Response.AddHeader("Content-Disposition", "attachment; filename=" & Server.UrlPathEncode(ViewState.Item("LogFileName")))
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
            Response.Write("無下傳欄位格式說明檔")
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
            Case "1"    '1.整批津貼資料
                subDownloadSampleFile("整批津貼資料.xls", "整批津貼資料")
            Case Else
                Bsp.Utility.ShowMessage(Me, "「請先選擇欄位格式說明檔對應的上傳類型」")
        End Select
    End Sub

    Private Sub subDownloadSampleFile(ByVal strFile As String, ByVal strMessageTitle As String)
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
            Bsp.Utility.ShowMessage(Me, "「無" & strMessageTitle & "下傳欄位格式說明檔」")
        End If
    End Sub
#End Region

#Region "1.整批津貼資料"
    Private Function funCheckData_Salary(ByVal FileName As String) As Boolean
        Dim CompID As String = UserProfile.SelectCompRoleID
        Dim objRG As New RG1
        Dim objHR As New HR
        Dim bolInputStatus As Boolean = True
        Dim strValue As String = ""
        Dim strErrMsg As String = ""
        Dim strSqlWhere As String = ""
        Dim bolCheckData As Boolean = False  '檢核資料

        '補充說明：欄位非必填且放空白	
        Dim nintCompID As Integer = 0       '公司代碼
        Dim nintEmpID As Integer = 1        '員工編號
        Dim nintSalaryID As Integer = 2     '薪資項目代碼
        Dim nintPayMethod As Integer = 3    '計算方式
        Dim nintMethodRatio As Integer = 4  '計算比例
        Dim nintAmount As Integer = 5       '薪資金額
        Dim nintPeriodFlag As Integer = 6   '發放期間
        Dim nintValidFrom As Integer = 7    '發放起日
        Dim nintValidTo As Integer = 8      '發放迄日

        Dim intTotalCount As Integer = 0
        Dim intSuccessCount As Integer = 0
        Dim intErrorCount As Integer = 0

        Dim strExcelConn As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & FileName & ";Extended Properties=EXCEL 8.0"

        Dim strExcelWorkSheet As String = "[PR1100_Salary_Allowance$]"
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
                    If myDataRead.FieldCount <> 9 Then
                        strErrMsg = strErrMsg & "==> 上傳資料欄位個數不正確！"
                        intErrorCount = intErrorCount + 1
                        bolCheckData = False
                    Else

                        '公司代碼
                        strValue = Trim(myDataRead(nintCompID).ToString())
                        If strValue = "" Then
                            strErrMsg = strErrMsg & "==> " & strValue & " 公司代碼未輸入!"
                            bolCheckData = False
                        Else
                            strSqlWhere = ""
                            strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(strValue)
                            If Not objRG.IsDataExists("Company", strSqlWhere) Then
                                strErrMsg = strErrMsg & "==> " & Trim(strValue) & " 查無公司代碼!"
                                bolCheckData = False
                            End If

                            '公司代碼與授權公司代碼不同
                            If CompID <> strValue Then
                                strErrMsg = strErrMsg & "==> " & strValue & " 公司代碼與授權公司代碼不同!"
                                bolCheckData = False
                            End If
                        End If

                        '員工編號
                        strValue = Trim(myDataRead(nintEmpID).ToString())
                        If strValue = "" Then
                            strErrMsg = strErrMsg & "==> " & strValue & " 員工編號未輸入!"
                            bolCheckData = False
                        Else
                            strSqlWhere = ""
                            strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                            strSqlWhere = strSqlWhere & " and EmpID =" & Bsp.Utility.Quote(strValue)
                            If Not objRG.IsDataExists("Personal", strSqlWhere) Then
                                strErrMsg = strErrMsg & "==> " & strValue & " 員工編號不存在"
                                bolCheckData = False
                            Else
                                strSqlWhere = ""
                                strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                                strSqlWhere = strSqlWhere & " and EmpID =" & Bsp.Utility.Quote(strValue)
                                strSqlWhere = strSqlWhere & " and WorkStatus = '1'"
                                If Not objRG.IsDataExists("Personal", strSqlWhere) Then
                                    strErrMsg = strErrMsg & "==> " & strValue & " 員工非在職狀態"
                                    bolCheckData = False
                                End If
                            End If
                        End If

                        '薪資項目代碼
                        strValue = Trim(myDataRead(nintSalaryID).ToString())
                        If strValue = "" Then
                            strErrMsg = strErrMsg & "==> " & strValue & " 薪資項目代碼未輸入!"
                            bolCheckData = False
                        Else
                            strSqlWhere = ""
                            strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                            strSqlWhere = strSqlWhere & " and SalaryID =" & Bsp.Utility.Quote(strValue)
                            If Not objRG.IsDataExists("SalaryItem", strSqlWhere) Then
                                strErrMsg = strErrMsg & "==> " & strValue & " 薪資項目代碼不存在"
                                bolCheckData = False
                            Else
                                strSqlWhere = ""
                                strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                                strSqlWhere = strSqlWhere & " and SalaryID =" & Bsp.Utility.Quote(strValue)
                                strSqlWhere = strSqlWhere & " and SalaryType = '1'"
                                If Not objRG.IsDataExists("SalaryItem", strSqlWhere) Then
                                    strErrMsg = strErrMsg & "==> " & strValue & " 薪資項目代碼非津貼類"
                                    bolCheckData = False
                                End If
                            End If
                        End If

                        '計算方式
                        strValue = Trim(myDataRead(nintPayMethod).ToString())
                        If strValue = "" Then
                            strErrMsg = strErrMsg & "==> " & strValue & " 計算方式未輸入!"
                            bolCheckData = False
                        Else
                            If strValue <> "1" And strValue <> "2" And strValue <> "3" And strValue <> "4" And strValue <> "5" Then
                                strErrMsg = strErrMsg & "==> " & strValue & " 計算方式1：固定金額，2：公式-輪班，3：公式-異地(北)，4：公式-異地(南)，5：費用出帳"
                                bolCheckData = False
                            End If
                        End If

                        '計算比例
                        strValue = Trim(myDataRead(nintMethodRatio).ToString())
                        If Trim(myDataRead(nintPayMethod).ToString()) = "2" Then
                            If strValue = "" Then
                                strErrMsg = strErrMsg & "==> " & strValue & " 若計算方式為2：公式-輪班，計算比例為必填欄位"
                                bolCheckData = False
                            Else
                                If Not IsNumeric(strValue) Then
                                    strErrMsg = strErrMsg & "==> " & strValue & " 計算比例請輸入數字"
                                    bolCheckData = False
                                Else
                                    If Double.Parse(strValue) = 0 Then
                                        strErrMsg = strErrMsg & "==> " & strValue & " 計算比例不得為0"
                                        bolCheckData = False
                                    Else
                                        If Not Regex.IsMatch(strValue, "^([0-9]{1,4})(?:\.[0-9]{1})?$") Then
                                            strErrMsg = strErrMsg & "==> " & strValue & " 計算比例整數最多四位，小數點最多一位"
                                            bolCheckData = False
                                        End If
                                    End If
                                End If
                            End If
                        End If

                        If Trim(myDataRead(nintPayMethod).ToString()) = "1" Or Trim(myDataRead(nintPayMethod).ToString()) = "5" Then
                            If strValue <> "" Then
                                If Not IsNumeric(strValue) Then
                                    strErrMsg = strErrMsg & "==> " & strValue & " 計算比例請輸入數字"
                                    bolCheckData = False
                                Else
                                    If Double.Parse(strValue) <> 0 Then
                                        strErrMsg = strErrMsg & "==> " & strValue & " 若計算方式為1：固定金額或5：費用出帳，計算比例只能上傳0"
                                        bolCheckData = False
                                    End If
                                End If
                            End If
                        End If

                        If Trim(myDataRead(nintPayMethod).ToString()) = "3" Or Trim(myDataRead(nintPayMethod).ToString()) = "4" Then
                            If strValue <> "" Then
                                If Not IsNumeric(strValue) Then
                                    strErrMsg = strErrMsg & "==> " & strValue & " 計算比例請輸入數字"
                                    bolCheckData = False
                                Else
                                    If Double.Parse(strValue) <> 15 Then
                                        strErrMsg = strErrMsg & "==> " & strValue & " 若計算方式為3：公式-異地(北)或4：公式-異地(南)，計算比例只能上傳15"
                                        bolCheckData = False
                                    End If
                                End If
                            End If
                        End If


                        '薪資金額
                        strValue = Trim(myDataRead(nintAmount).ToString())
                        If Trim(myDataRead(nintPayMethod).ToString()) = "1" Or Trim(myDataRead(nintPayMethod).ToString()) = "5" Then
                            If strValue = "" Then
                                strErrMsg = strErrMsg & "==> " & strValue & " 若計算方式為1：固定金額或5：費用出帳，薪資金額為必填欄位"
                                bolCheckData = False
                            Else
                                If Not objHR.funCheckStr(strValue) Then
                                    strErrMsg = strErrMsg & "==> " & strValue & " 薪資金額請輸入數字"
                                    bolCheckData = False
                                Else
                                    If Double.Parse(strValue) = 0 Then
                                        strErrMsg = strErrMsg & "==> " & strValue & " 薪資金額不得為0"
                                        bolCheckData = False
                                    End If
                                End If
                            End If
                        Else
                            If strValue <> "" Then
                                If Not objHR.funCheckStr(strValue) Then
                                    strErrMsg = strErrMsg & "==> " & strValue & " 薪資金額請輸入數字"
                                    bolCheckData = False
                                Else
                                    If Double.Parse(strValue) <> 0 Then
                                        strErrMsg = strErrMsg & "==> " & strValue & " 若計算方式非1：固定金額或5：費用出帳，薪資金額只能上傳0"
                                        bolCheckData = False
                                    End If
                                End If
                            End If
                        End If

                        '發放期間
                        strValue = Trim(myDataRead(nintPeriodFlag).ToString())
                        If strValue = "" Then
                            strErrMsg = strErrMsg & "==> " & strValue & " 發放期間未輸入!"
                            bolCheckData = False
                        Else
                            If strValue <> "1" And strValue <> "2" Then
                                strErrMsg = strErrMsg & "==> " & strValue & " 發放期間1：期間未定，2：固定時間"
                                bolCheckData = False
                            End If
                        End If

                        '發放起日 發放迄日
                        If Trim(myDataRead(nintPeriodFlag).ToString()) = "1" Then

                            If Trim(myDataRead(nintValidFrom).ToString()) <> "" Then
                                strErrMsg = strErrMsg & "==> " & strValue & " 發放期間為1：期間未定，發放起日不需填寫"
                                bolCheckData = False
                            End If

                            If Trim(myDataRead(nintValidTo).ToString()) <> "" Then
                                strErrMsg = strErrMsg & "==> " & strValue & " 發放期間為1：期間未定，發放迄日不需填寫"
                                bolCheckData = False
                            End If

                        ElseIf Trim(myDataRead(nintPeriodFlag).ToString()) = "2" Then

                            If Trim(myDataRead(nintValidFrom).ToString()) = "" Then
                                strErrMsg = strErrMsg & "==> " & strValue & " 發放期間為2：固定時間，發放起日為必填欄位"
                                bolCheckData = False
                            End If

                            If Trim(myDataRead(nintValidTo).ToString()) = "" Then
                                strErrMsg = strErrMsg & "==> " & strValue & " 發放期間為2：固定時間，發放迄日為必填欄位"
                                bolCheckData = False
                            End If

                            If Trim(myDataRead(nintValidFrom).ToString()) <> "" Then
                                If Not IsDate(Trim(myDataRead(nintValidFrom).ToString)) Then
                                    strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintValidFrom).ToString) & " 發放起日請輸入西曆YYYY/MM/DD"
                                    bolCheckData = False
                                End If
                            End If

                            If Trim(myDataRead(nintValidTo).ToString()) <> "" Then
                                If Not IsDate(Trim(myDataRead(nintValidTo).ToString)) Then
                                    strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintValidTo).ToString) & " 發放迄日請輸入西曆YYYY/MM/DD"
                                    bolCheckData = False
                                End If
                            End If

                            If Trim(myDataRead(nintValidFrom).ToString()) <> "" And Trim(myDataRead(nintValidTo).ToString()) <> "" Then
                                If Trim(myDataRead(nintValidFrom).ToString()) > Trim(myDataRead(nintValidTo).ToString()) Then
                                    strErrMsg = strErrMsg & "==> 發放迄日" & Trim(myDataRead(nintValidTo).ToString) & " 需大於 發放起日" & Trim(myDataRead(nintValidFrom).ToString)
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
                                    & " 薪資項目代碼：" & Trim(myDataRead(nintSalaryID).ToString) _
                                    & " 計算方式：" & Trim(myDataRead(nintPayMethod).ToString) _
                                    & " 計算比例：" & Trim(myDataRead(nintMethodRatio).ToString) _
                                    & " 薪資金額：" & Trim(myDataRead(nintAmount).ToString) _
                                    & " 發放期間：" & Trim(myDataRead(nintPeriodFlag).ToString) _
                                    & " 發放起日：" & Trim(myDataRead(nintValidFrom).ToString) _
                                    & " 發放迄日：" & Trim(myDataRead(nintValidTo).ToString) _
                                    & strErrMsg)
                        bolInputStatus = False
                    End If

                    intTotalCount = intTotalCount + 1

                End While
            End Using

            myExcelConn.Close()

            ViewState.Item("SuccessCount") = intSuccessCount
            ViewState.Item("ErrorCount") = intErrorCount
            ViewState.Item("TotalCount") = intTotalCount

            If bolInputStatus = False Then
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            myExcelConn.Close()
            ViewState.Item("SuccessCount") = intSuccessCount
            ViewState.Item("ErrorCount") = intErrorCount
            ViewState.Item("TotalCount") = intTotalCount

            Bsp.Utility.ShowMessage(Me, Me.FunID & ".funCheckData", ex)
            Return False
        End Try
    End Function

    '資料上傳 單筆
    Private Function funUploadDataSingle_Salary(ByVal myDataRead As OleDbDataReader, ByRef ErrMsg As String) As Boolean
        Dim bsSalary As New beSalary.Service()
        Dim beSalary As New beSalary.Row

        Dim dtNow = DateTime.Now
        Dim objHR As New HR
        Dim objRG As New RG1
        Dim objRegistData As New RegistData

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()

            Dim tran As DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Dim nintCompID As Integer = 0       '公司代碼
            Dim nintEmpID As Integer = 1        '員工編號
            Dim nintSalaryID As Integer = 2     '薪資項目代碼
            Dim nintPayMethod As Integer = 3    '計算方式
            Dim nintMethodRatio As Integer = 4  '計算比例
            Dim nintAmount As Integer = 5       '薪資金額
            Dim nintPeriodFlag As Integer = 6   '發放期間
            Dim nintValidFrom As Integer = 7    '發放起日
            Dim nintValidTo As Integer = 8      '發放迄日

            Dim intLoop As Integer = 0

            Try
                Dim strCompID As String = Trim(myDataRead(nintCompID).ToString)
                Dim strEmpID As String = Trim(myDataRead(nintEmpID).ToString)
                Dim strValue As String = ""

                '寫入薪資主檔Salary
                beSalary.CompID.Value = strCompID
                beSalary.EmpID.Value = strEmpID
                beSalary.SalaryID.Value = Trim(myDataRead(nintSalaryID).ToString)
                beSalary.PayMethod.Value = Trim(myDataRead(nintPayMethod).ToString)

                strValue = Trim(myDataRead(nintPayMethod).ToString)
                If strValue = "1" Or strValue = "5" Then
                    beSalary.MethodRatio.Value = "0"
                ElseIf strValue = "3" Or strValue = "4" Then
                    beSalary.MethodRatio.Value = "15"
                Else
                    beSalary.MethodRatio.Value = Trim(myDataRead(nintMethodRatio).ToString)
                End If

                If strValue = "1" Or strValue = "2" Or strValue = "5" Then
                    beSalary.MethodAmt.Value = 0
                ElseIf strValue = "3" Then
                    beSalary.MethodAmt.Value = objRG.QueryData("Parameter", "And CompID = " & Bsp.Utility.Quote(strCompID), "SiteAmountN")
                ElseIf strValue = "4" Then
                    beSalary.MethodAmt.Value = objRG.QueryData("Parameter", "And CompID = " & Bsp.Utility.Quote(strCompID), "SiteAmountS")
                End If

                If strValue = "1" Or strValue = "5" Then
                    beSalary.Amount.Value = objRegistData.funEncryptNumber(strEmpID, Trim(myDataRead(nintAmount).ToString))
                    'beSalary.Amount.Value = Trim(myDataRead(nintAmount).ToString) '********************************************
                Else
                    beSalary.Amount.Value = objRegistData.funEncryptNumber(strEmpID, "0")
                    'beSalary.Amount.Value = 0 '********************************************************************************
                End If

                beSalary.PeriodFlag.Value = Trim(myDataRead(nintPeriodFlag).ToString)
                beSalary.ValidFrom.Value = IIf(myDataRead(nintValidFrom).ToString = "", "1900/01/01", myDataRead(nintValidFrom).ToString)
                beSalary.ValidTo.Value = IIf(myDataRead(nintValidTo).ToString = "", "1900/01/01", myDataRead(nintValidTo).ToString)
                beSalary.LastChgComp.Value = "PR1100"
                beSalary.LastChgID.Value = "PR1100"
                beSalary.LastChgDate.Value = Now

                If bsSalary.IsDataExists(beSalary) Then
                    bsSalary.Update(beSalary, tran)
                Else
                    bsSalary.Insert(beSalary, tran)
                End If

                tran.Commit()
                inTrans = False
                Return True
            Catch ex As Exception
                If inTrans Then tran.Rollback()
                ErrMsg = ex.Message
                Return False
            End Try
        End Using
    End Function
#End Region

#Region "放行"
    Private Sub Release(ByVal LogFunction As String)
        ucRelease.ShowCompRole = "True"
        ucRelease.FunID = "PR1100"
        ucRelease.LogFunction = LogFunction
        ucRelease.OpenSelect()
    End Sub

    Public Overrides Sub DoModalReturn(ByVal returnValue As String)
        If returnValue <> "" Then
            Dim aryData() As String = returnValue.Split(":")

            Select Case aryData(0)
                Case "ucRelease"
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    If aryValue(0) = "Y" Then
                        DoUpload()
                    End If
            End Select

        End If
    End Sub
#End Region

End Class
