'****************************************************
'功能說明：快速查詢網頁(QFind)
'建立人員：Ann
'建立日期：2014.08.20
'****************************************************
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports System.Data.Common
Imports System.Data

Partial Class HR_HR0200
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim aryParam() As Object = Session(Request("ControlSessionID"))

            ViewState.Item("bolCompRole") = aryParam(0)
            ViewState.Item("FunID") = aryParam(5)
            ViewState.Item("LogFunction") = aryParam(6)
            LoadField()
        End If
    End Sub

    'Private Sub LoadField(ByVal SQL As String)
    Private Sub LoadField()

        '公司
        UC.UC_Company(ddlCompID, "", CBool(ViewState.Item("bolCompRole")), "N")
        ddlCompID.Items.Insert(0, "---請選擇---")
        ddlCompID.SelectedValue = UserProfile.CompID

    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnActionC"
                txtReturnValue.Text = ""
                Dim aryColumn() As String = Nothing

                If ddlCompID.SelectedValue <> "---請選擇---" Then
                    
                    If checkRelease() Then
                        txtReturnValue.Text = "Y|$|" & ddlCompID.SelectedValue & "|$|" & txtEmpID.Text  '20150513 wei modify 增加回傳放行人員的公司代碼及員編
                    Else
                        txtReturnValue.Text = "N" & ddlCompID.SelectedValue & "|$|" & txtEmpID.Text '20150513 wei modify 增加回傳放行人員的公司代碼及員編
                    End If
                    'txtReturnValue.Text = ((ddlCompID.SelectedItem.Text.Substring(7)) & "|$|" & (ddlUserID.SelectedValue) & "|$|" & (ddlUserID.SelectedItem.Text.Substring(7)) & "|$|" & ddlCompID.SelectedItem.Value)
                Else
                    Bsp.Utility.ShowFormatMessage(Me, "W_00030", "公司代碼")
                End If

                Bsp.Utility.RunClientScript(Me, "window.top.returnValue='" & Replace(txtReturnValue.Text, "'", "\'") & "';window.top.close();")
        End Select
    End Sub

    Private Function checkRelease() As Boolean
        Dim intResult As Integer
        Dim strMsg As String = ""
        Dim strSQLWhere As String = ""

        strSQLWhere = "And U.CompID=" & Bsp.Utility.Quote(ddlCompID.SelectedValue)
        strSQLWhere = strSQLWhere & " and G.SysID=" & Bsp.Utility.Quote(UserProfile.LoginSysID)
        strSQLWhere = strSQLWhere & " and G.CompRoleID=" & Bsp.Utility.Quote(UserProfile.SelectCompRoleID)
        strSQLWhere = strSQLWhere & " and F.FunID=" & Bsp.Utility.Quote(ViewState.Item("FunID"))

        Dim objHR As New HR()
        Dim objSC As New SC()
        Dim objUC As New UC()
        Try
            If ddlCompID.SelectedValue = "" Then
                strMsg = "公司代碼未選擇"
                Throw New Exception(strMsg)
                Return False
            End If

            If txtEmpID.Text = "" Then
                strMsg = "放行主管未輸入"
                Throw New Exception(strMsg)
                Return False
            End If

            If txtPassword.Text = "" Then
                strMsg = "放行密碼未輸入"
                Throw New Exception(strMsg)
                Return False
            End If

            If txtEmpID.Text.ToString() = UserProfile.UserID And Not UserProfile.IsSysAdmin Then
                strMsg = "放行主管不可與執行人員相同"
                Throw New Exception(strMsg)
                Return False
            End If
            intResult = objSC.ReleaseCheck(txtEmpID.Text.ToString().ToUpper().Trim(), txtPassword.Text, Request.ServerVariables("REMOTE_ADDR"))
            Select Case intResult
                Case 0  '成功
                    Using dtUser As DataTable = objUC.GetReleaseUserInfo(txtEmpID.Text, "*", strSQLWhere)
                        If dtUser.Rows.Count = 0 Then
                            subAddReleaseLog(ddlCompID.SelectedValue, txtEmpID.Text.ToString(), "2")
                            Throw New Exception("您沒有此系統的放行權限(無使用者資料)！")
                            Return False
                        End If

                        Dim beUser As New beSC_User.Row(dtUser.Rows(0))
                        If beUser.BanMark.Value = "1" Then
                            subAddReleaseLog(ddlCompID.SelectedValue, txtEmpID.Text.ToString(), "2")
                            Throw New Exception("您沒有此系統的放行權限(用户禁用中)！")
                            Return False
                        End If
                    End Using
                    subAddReleaseLog(ddlCompID.SelectedValue, txtEmpID.Text.ToString(), "0")
                    '20151116 Beatrice Add
                    subResetPwdErrCount(ddlCompID.SelectedValue, txtEmpID.Text.ToString())
                    Return True
                Case 1  '禁用BanMark = '1'
                    subAddReleaseLog(ddlCompID.SelectedValue, txtEmpID.Text.ToString(), "3")
                    strMsg = "此帳號禁用中"
                    Throw New Exception(strMsg)
                    Return False
                Case 2  '使用期限到期
                    subAddReleaseLog(ddlCompID.SelectedValue, txtEmpID.Text.ToString(), "3")
                    'strMsg = "帳號使用期限已到期"   '20151001 Ann modify
                    strMsg = "密碼使用期限已到期，請至安控管理「SC0800放行密碼維護」變更密碼"    '20151001 Ann modify
                    Throw New Exception(strMsg)
                    Return False
                Case 3, 4  '查無此帳號或密碼錯誤
                    subAddReleaseLog(ddlCompID.SelectedValue, txtEmpID.Text.ToString(), "3")
                    strMsg = "查無此帳號或密碼錯誤" & GetPwdErrCount(ddlCompID.SelectedValue, txtEmpID.Text.ToString()) '20151117 Beatrice modify
                    Throw New Exception(strMsg)
                    Return False
                Case 5  '錯誤次數已達3次
                    subAddReleaseLog(ddlCompID.SelectedValue, txtEmpID.Text.ToString(), "3")
                    'strMsg = "錯誤次數已達3次"    '20151113 Ann modify
                    strMsg = "錯誤次數已達3次，請聯繫系統管理者至「SC0100使用者維護」重設密碼" '20151113 Ann modify
                    Throw New Exception(strMsg)
                    Return False
                Case 6  ''此帳號目前禁用中
                    subAddReleaseLog(ddlCompID.SelectedValue, txtEmpID.Text.ToString(), "3")
                    strMsg = "此帳號目前禁用中"
                    Throw New Exception(strMsg)
                    Return False
                Case Else
                    subAddReleaseLog(ddlCompID.SelectedValue, txtEmpID.Text.ToString(), "3")
                    strMsg = "無法指出的錯誤...請通知系統人員"
                    Throw New Exception(strMsg)
                    Return False
            End Select

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Bsp.Utility.getInnerException("checkRelease", ex))
            Return False
        End Try
    End Function

    '20151117 Beatrice Add
    Private Function GetPwdErrCount(ByVal strCompID As String, ByVal strUserID As String) As String
        Dim strResult As String = ""
        Dim strSQL As String = "Select PasswordErrorCount From SC_User Where CompID = " & Bsp.Utility.Quote(strCompID) & " And UserID = " & Bsp.Utility.Quote(strUserID)

        Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL).Tables(0)
            If dt.Rows.Count > 0 Then
                strResult = vbCrLf & "密碼錯誤次數：" & dt.Rows(0).Item(0).ToString() & vbCrLf & "請注意，密碼錯誤3次將無法放行，請聯繫系統管理者！"
            End If
        End Using

        Return strResult
    End Function

    '20151116 Beatrice Add
    Private Sub subResetPwdErrCount(ByVal strCompID As String, ByVal strUserID As String)
        Dim strSQL As String = ""

        strSQL = "UPDATE SC_User Set PasswordErrorCount = 0 Where CompID = " & Bsp.Utility.Quote(strCompID) & " And UserID = " & Bsp.Utility.Quote(strUserID)

        Using cn As DbConnection = Bsp.DB.getConnection()
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)

                tran.Commit()
            Catch ex As Exception
                If inTrans Then tran.Rollback()
                Throw
            Finally
                tran.Dispose()
                If cn.State = ConnectionState.Open Then cn.Close()
            End Try
        End Using
    End Sub

    Private Sub subAddReleaseLog(ByVal strReleaseComp As String, ByVal strReleaseID As String, ByVal strResultCode As String)
        Dim objSC As New SC()
        Dim strSQL As String = ""

        strSQL = "Insert into FunctionLog (" & vbCrLf
        strSQL = strSQL & "  LogDate     " & vbCrLf
        strSQL = strSQL & " ,LogFormID   " & vbCrLf
        strSQL = strSQL & " ,LogFunction " & vbCrLf
        strSQL = strSQL & " ,LogUserComp " & vbCrLf
        strSQL = strSQL & " ,LogUserID   " & vbCrLf

        strSQL = strSQL & " ,LogUserName " & vbCrLf
        strSQL = strSQL & " ,EffCount    " & vbCrLf
        strSQL = strSQL & " ,LogType     " & vbCrLf
        strSQL = strSQL & " ,ResultCode  " & vbCrLf
        strSQL = strSQL & " ,ReleaseComp " & vbCrLf

        strSQL = strSQL & " ,ReleaseID   " & vbCrLf
        strSQL = strSQL & " ,ReleaseName " & vbCrLf
        strSQL = strSQL & " ,RegIP       " & vbCrLf

        strSQL = strSQL & " ) values (   " & vbCrLf
        strSQL = strSQL & "  " & Bsp.Utility.Quote(Format(Now, "yyyy/MM/dd HH:mm:ss")) & vbCrLf
        strSQL = strSQL & " ," & Bsp.Utility.Quote(ViewState.Item("FunID")) & vbCrLf
        strSQL = strSQL & " ," & Bsp.Utility.Quote(ViewState.Item("LogFunction")) & vbCrLf
        strSQL = strSQL & " ," & Bsp.Utility.Quote(UserProfile.ActCompID) & vbCrLf
        strSQL = strSQL & " ," & Bsp.Utility.Quote(UserProfile.ActUserID) & vbCrLf
        strSQL = strSQL & " ," & Bsp.Utility.Quote(UserProfile.ActUserName) & vbCrLf
        strSQL = strSQL & " ,0   " & vbCrLf
        strSQL = strSQL & " ,'1' " & vbCrLf     'Log類別-放行
        strSQL = strSQL & " ,'" & strResultCode & "' " & vbCrLf
        strSQL = strSQL & " ,'" & strReleaseComp & "' " & vbCrLf

        strSQL = strSQL & " ,'" & strReleaseID & "' " & vbCrLf
        strSQL = strSQL & " ," & Bsp.Utility.Quote(objSC.GetSC_UserName(strReleaseComp, strReleaseID).ToString) & vbCrLf
        strSQL = strSQL & " ," & Bsp.Utility.Quote(Request.ServerVariables("REMOTE_ADDR")) & vbCrLf
        strSQL = strSQL & " )"

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran, "eHRMSDB")

                tran.Commit()
            Catch ex As Exception
                If inTrans Then tran.Rollback()
                Throw
            Finally
                tran.Dispose()
                If cn.State = ConnectionState.Open Then cn.Close()
            End Try
        End Using
    End Sub

End Class
