'****************************************************
'功能說明：單位班別設定-檔案上傳
'建立人員：MickySung
'建立日期：2015/05/20
'****************************************************
Imports System.Data
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.IO
Imports System.IO.Compression
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Class PA_PA2203

    Inherits PageBase

    Public Shared intSuccessCount As Integer = 0
    Public Shared intErrorCount As Integer = 0
    Public Shared intTotalCount As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param

            Case "btnActionC"   '上傳
                If funCheckData() Then

                    '畫面輸入的條件

                    'cs:btnActionC = > Server 判斷DB有資料(先儲存上傳檔案&路徑) => 再回cs:IsTOConfirm('') => cs:btnUpd.click() => 再回Server讀取檔案內容寫入DB
                    'cs:btnActionC = > Server 判斷DB無資料(先儲存上傳檔案&路徑) =>讀取檔案內容寫入DB
                    '要先儲存檔案~不然CS<-->Server來回後~FileUpload資料會遺失
                    Dim strFileName As String = Bsp.Utility.GetNewFileName("PA2200")

                    Dim strFilePath As String = Server.MapPath(Bsp.Utility.getAppSetting("TempPath")) & "\" & strFileName & ".txt"
                    '先儲存上傳檔案
                    FileUpload.PostedFile.SaveAs(strFilePath)
                    'Server上 上傳檔案SaveAs路徑
                    hidFileUploadFileName.Value = strFileName & ".txt"

                    '要從IIS Server上直接產生USER LOCAL電腦的檔案是不行的，有安全問題，沒有權限，在LOCAL開發OK，但上SERVER不行
                    '將在Server先產生ERR檔案，再傳過去USER LOCAL CS端

                    'hidLogFileName.Value = strFileName & ".err"
                    'ERR LOG檔案的檔名同上傳檔
                    Dim strLogFile As String = FileUpload.PostedFile.FileName
                    strLogFile = strLogFile.Substring(strLogFile.LastIndexOf("\") + 1)
                    strLogFile = strLogFile.Substring(0, strLogFile.LastIndexOf("."))
                    hidLogFileName.Value = strLogFile & strFileName & ".err"

                    If IsDataExist.Value = "Y" Then
                        '若DB有資料，再回到CS，請USER由畫面確認 "相同资料已经存在，确定全部删除，重新上传?!?"
                        Bsp.Utility.RunClientScript(Me.Page, "IsTOConfirm('');")
                    Else
                        '若DB無資料，直接上傳
                        DoUpload()
                    End If
                End If
            Case "btnActionX"   '關閉離開
                Bsp.Utility.RunClientScript(Me, "window.top.close();")
            Case Else
                'DoOtherAction(Param)   '其他功能動作                
        End Select
    End Sub

    '隱藏的按鈕--上傳檔案
    Protected Sub btnUpd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpd.Click
        DoUpload()
    End Sub

    '隱藏的按鈕--下傳檔案
    Protected Sub btnUpd1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpd1.Click
        subDownloadLogFile()
    End Sub

    Private Sub GoBack()
        Dim ti As TransferInfo = Me.StateTransfer
        Me.TransferFramePage(ti.CallerUrl, Nothing, ti.Args)
    End Sub

    Private Function funCheckData() As Boolean
        Dim strValue As String = ""

        '上傳檔案路徑
        If FileUpload.PostedFile Is Nothing Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00120", "上傳檔案路徑")
            FileUpload.Focus()
            Return False
        Else
            If FileUpload.PostedFile.FileName = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00030", "上傳檔案路徑")
                FileUpload.Focus()
                Return False
            End If
        End If

        Return True
    End Function
    Private Sub DoUpload()
        'Dim objSC As New SC
        Dim strMessage As String = ""
        Dim strUpload_TableName As String = ""
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
        btnX.Caption = "關閉離開"

        Try
            Dim strFileName As String = Server.MapPath(Bsp.Utility.getAppSetting("TempPath")) & "\" & hidFileUploadFileName.Value

            Dim objHR As New HR


            strUpload_TableName = objHR.funCheck_UploadTableName(strFileName).Replace("'", "").ToString
            If strUpload_TableName <> "PA2200$" Then
                File.Delete(strFileName)
                strMessage = "上傳檔案的工作表(WorkSheet)-" & Mid(strUpload_TableName, 1, Len(strUpload_TableName) - 1) & " 與選擇上傳類型-PA2200不一致!"
                Bsp.Utility.RunClientScript(Me, "alert('" & String.Format(Bsp.Utility.getMessage("E_00050"), strMessage) & "');" & vbCrLf & _
                                "window.top.returnValue = 'OK';")
            End If


            If objHR.funCheck_UploadCount(strFileName, "[PA2200$]") > CInt(ConfigurationManager.AppSettings("Upload_MaxCount").ToString) Then
                File.Delete(strFileName)
                strMessage = "上傳資料筆數過大，系統允許最大上傳筆數：" & ConfigurationManager.AppSettings("Upload_MaxCount").ToString
                Bsp.Utility.RunClientScript(Me, "alert('" & String.Format(Bsp.Utility.getMessage("E_00050"), strMessage) & "');" & vbCrLf & _
                                "window.top.returnValue = 'OK';")
            End If

            If funCheckData1(strFileName) Then
                File.Delete(strFileName)
                strMessage = "PA2200資料上傳總筆數：" & intTotalCount & " 成功筆數：" & intSuccessCount & " 失敗筆數：" & intErrorCount
                Bsp.Utility.RunClientScript(Me, "alert('" & String.Format(Bsp.Utility.getMessage("I_00020"), strMessage) & "');" & vbCrLf & _
                            "window.top.returnValue = 'OK';" & vbCrLf & "window.top.close();")
            Else
                '刪除上傳檔案
                File.Delete(strFileName)
                strMessage = "『上傳檔案失敗！總筆數：" & intTotalCount & " ，成功筆數：" & intSuccessCount & "， 失敗筆數：" & intErrorCount & "，請下載錯誤紀錄log檔案!"
                '顯示上傳失敗錯誤訊息，並下載ERR LOG檔案
                Bsp.Utility.RunClientScript(Me.Page, "IsTODownload('" & strMessage & "');")

            End If


        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoUpload", ex)
        End Try

    End Sub

    'Check
    Private Function funCheckData1(ByVal FileName As String) As Boolean
        Dim CompID As String = UserProfile.SelectCompRoleID
        Dim objPA As New PA2
        Dim bolInputStatus As Boolean = True
        Dim strErrMsg As String = ""
        Dim strSqlWhere As String = ""

        Dim intLoop As Integer = 0

        Dim bolCheckData As Boolean = False  '檢核資料

        '補充說明：欄位非必填且放空白	
        Dim nintCompID As Integer = 0
        Dim nintWTID As Integer = 1
        Dim nintDeptID As Integer = 2
        Dim nintOrganID As Integer = 3

        intTotalCount = 0
        intSuccessCount = 0
        intErrorCount = 0

        Dim strExcelConn As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & FileName & ";Extended Properties=EXCEL 8.0"

        Dim strExcelWorkSheet As String = "[PA2200$]"
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

                    'Dim strIDNo As String = ""
                    'If Trim(myDataRead(nintCompID).ToString) <> "" And Trim(myDataRead(nintEmpID).ToString) <> "" Then
                    '    Using dt As DataTable = objHR3000.GetEmpDataByHR3000(Trim(myDataRead(nintCompID).ToString), Trim(myDataRead(nintEmpID).ToString))
                    '        If dt.Rows.Count > 0 Then
                    '            strIDNo = dt.Rows.Item(0)("IDNo").ToString()
                    '        End If
                    '    End Using
                    'End If

                    '先判斷是否上傳欄位個數正確
                    If myDataRead.FieldCount <> 4 Then
                        strErrMsg = strErrMsg & "==> 上傳資料欄位個數不正確！"
                        intErrorCount = intErrorCount + 1
                    Else
                        If CompID <> myDataRead(nintCompID).ToString Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCompID).ToString) & " 公司代碼與授權公司代碼不同!"
                            bolCheckData = False
                        End If

                        '公司代碼
                        strSqlWhere = ""
                        strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                        If Not objPA.IsDataExists("Company", strSqlWhere) Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintCompID).ToString) & " 查無公司代碼!"
                            bolCheckData = False
                        End If

                        '班別代碼
                        strSqlWhere = ""
                        strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                        strSqlWhere = strSqlWhere & " and WTID =" & Bsp.Utility.Quote(Trim(myDataRead(nintWTID).ToString))
                        If Not objPA.IsDataExists("WorkTime", strSqlWhere) Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintWTID).ToString) & " 班別代碼不存在!"
                            bolCheckData = False
                        End If

                        '部門代碼
                        strSqlWhere = ""
                        strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                        strSqlWhere = strSqlWhere & " and OrganID =" & Bsp.Utility.Quote(Trim(myDataRead(nintDeptID).ToString)) '2015/07/06 midify
                        If Not objPA.IsDataExists("Organization", strSqlWhere) Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintWTID).ToString) & " 部門代碼不存在!"
                            bolCheckData = False
                        End If

                        '科組課代碼
                        strSqlWhere = ""
                        strSqlWhere = strSqlWhere & " and CompID =" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                        strSqlWhere = strSqlWhere & " and OrganID =" & Bsp.Utility.Quote(Trim(myDataRead(nintOrganID).ToString))
                        If Not objPA.IsDataExists("Organization", strSqlWhere) Then
                            strErrMsg = strErrMsg & "==> " & Trim(myDataRead(nintWTID).ToString) & " 科組課代碼不存在!"
                            bolCheckData = False
                        End If

                        If Trim(myDataRead(nintCompID).ToString) <> "" And Trim(myDataRead(nintWTID).ToString) <> "" And Trim(myDataRead(nintDeptID).ToString) <> "" And Trim(myDataRead(nintOrganID).ToString) <> "" Then
                            strSqlWhere = ""
                            strSqlWhere = strSqlWhere & " and CompID=" & Bsp.Utility.Quote(Trim(myDataRead(nintCompID).ToString))
                            strSqlWhere = strSqlWhere & " and WTID=" & Bsp.Utility.Quote(Trim(myDataRead(nintWTID).ToString))
                            strSqlWhere = strSqlWhere & " and DeptID=" & Bsp.Utility.Quote(Trim(myDataRead(nintDeptID).ToString))
                            strSqlWhere = strSqlWhere & " and OrganID=" & Bsp.Utility.Quote(Trim(myDataRead(nintOrganID).ToString))
                            If objPA.IsDataExists("OrgWorkTime", strSqlWhere) Then
                                strErrMsg = strErrMsg & "==> 資料重複！"
                                bolCheckData = False
                            End If
                        End If

                        If bolCheckData Then
                            If funUploadDataSingle(myDataRead) Then
                                intSuccessCount = intSuccessCount + 1
                            Else
                                intErrorCount = intErrorCount + 1
                            End If
                        Else
                            intErrorCount = intErrorCount + 1
                        End If

                    End If

                    If strErrMsg <> "" Then
                        subWriteLog("資料列:" & intTotalCount + 1 & " 公司代碼：" & Trim(myDataRead(nintCompID).ToString) & " 班別編號：" & Trim(myDataRead(nintWTID).ToString) & " 部門代碼：" & Trim(myDataRead(nintDeptID).ToString) & " 科組課代碼：" & Trim(myDataRead(nintOrganID).ToString) & strErrMsg)
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
    Private Function funUploadDataSingle(ByVal myDataRead As OleDbDataReader) As Boolean
        Dim bsOrgWorkTime As New beOrgWorkTime.Service()
        Dim beOrgWorkTime As New beOrgWorkTime.Row
        Dim dtNow = DateTime.Now

        '畫面輸入的條件

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()

            Dim tran As DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Dim nintCompID As Integer = 0
            Dim nintWTID As Integer = 1
            Dim nintDeptID As Integer = 2
            Dim nintOrganID As Integer = 3

            Dim intLoop As Integer = 0

            Try

                '公司代碼
                beOrgWorkTime.CompID.Value = myDataRead(nintCompID).ToString
                '班別代碼
                beOrgWorkTime.WTID.Value = myDataRead(nintWTID).ToString
                '部門代碼
                beOrgWorkTime.DeptID.Value = myDataRead(nintDeptID).ToString
                '科組課代碼
                beOrgWorkTime.OrganID.Value = myDataRead(nintOrganID).ToString

                beOrgWorkTime.LastChgComp.Value = UserProfile.ActCompID
                beOrgWorkTime.LastChgID.Value = UserProfile.ActUserID
                beOrgWorkTime.LastChgDate.Value = Now

                bsOrgWorkTime.Insert(beOrgWorkTime, tran)

                tran.Commit()
                inTrans = False
                Return True

            Catch ex As Exception

                If inTrans Then tran.Rollback()
                Return False

                Bsp.Utility.ShowMessage(Me, Me.FunID & ".funUploadDataSingle", ex)
            End Try
        End Using
    End Function

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
            Response.Write("無檔案")
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
    '如同VB 6.0的LenB函數，傳回字串aStr的位元組長度    
    Public Shared Function StrLenB(ByVal vstrValue As String) As Integer
        Dim i, k As Integer
        For i = 1 To Len(vstrValue)
            k += CharByte(Mid(vstrValue, i, 1))
        Next
        Return k

    End Function
    Public Shared Function CharByte(ByVal vstrWord As String) As Integer
        If Len(vstrWord) = 0 Then
            Return 0
        Else
            Select Case Asc(vstrWord)
                Case 0 To 255
                    Return 1
                Case Else
                    Return 2
            End Select
        End If
    End Function
    Private Sub subDownloadSampleFile(ByVal strFile As String)

        '若有多個ShowFormatMessage時，只會跳最後一個
        'Bsp.Utility.ShowFormatMessage(Me, "W_00031", "subDownloadLogFile!")

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
            Response.Write("無檔案")
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
    Protected Sub btnDownload_Click(sender As Object, e As System.EventArgs) Handles btnDownload.Click
        subDownloadSampleFile("PA2200UploadFileSample.xls")
    End Sub
End Class




