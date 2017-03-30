'************************************************************
'功能說明：登入畫面
'建立人員：Chung
'建立日期：2011.05.12
'************************************************************
Imports System.Data
Imports System.Security.Cryptography

Partial Class _Default
    Inherits System.Web.UI.Page
    Dim strLoginSysID As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        FormsAuthentication.SignOut()
        Session.Clear()
        txtPwd.Attributes.Add("onkeypress", "EntertoSubmit();")
        txtConfirmPwd.Attributes.Add("onkeypress", "EntertoSubmit();")

        Dim objSC As New SC
        Dim strPage As String
        If Request("Action") Is Nothing OrElse (Request("Action") IsNot Nothing AndAlso Request("Action").ToString().ToUpper() <> "OPENWEB") Then
            If Bsp.MySettings.IsSystemClose = "Y" Then
                strPage = Bsp.Utility.getMessagePageURL("Login_Load_1", Bsp.Enums.MessageType.Information, "目前網站關閉中，請稍候再試！", "關閉視窗", "")

                FormsAuthentication.SetAuthCookie("ClosePage", False)

                Server.Transfer(Me.ResolveUrl(strPage))
            End If
        End If

        strLoginSysID = "wHRMS"

        If Not IsPostBack() Then
            '檢查IE版本及Cookies
            'If Not funCheckBrower() Then Return

            '企業平台登入檢查
            If Request.Form.Count > 0 Then
                Dim ht As New Hashtable
                For Each strKey As String In Request.Form.AllKeys
                    ht.Add(strKey, Request.Form(strKey))
                Next
                ViewState.Item("SSOMode") = ""
                'Update by Chung 2010.07.08 配合企業平台登入方式修改
                If ht.ContainsKey("SSOTime") AndAlso ht.ContainsKey("UserID") AndAlso ht.ContainsKey("MD5") AndAlso ht("SSOTime").ToString() <> "" Then
                    'UserID;3;4 + aBc99+ SSOTime;4;5 + UserID;1;4
                    Dim strBeforeHash As String = String.Format("{0}aBc99{1}{2}", _
                                                                ht("UserID").ToString().Substring(2, 4), _
                                                                ht("SSOTime").ToString().Substring(3, 5), _
                                                                ht("UserID").ToString().Substring(0, 4))

                    Dim MD5hasher As MD5 = MD5.Create()
                    Dim myMD5Data As Byte() = MD5hasher.ComputeHash(Encoding.Default.GetBytes(strBeforeHash))
                    Dim strAfterHash As String = ""

                    For i As Integer = 0 To myMD5Data.Length - 1
                        strAfterHash &= myMD5Data(i).ToString("x2")
                    Next

                    If ht("MD5").ToString() = strAfterHash Then
                        txtUserID.Text = ht("UserID").ToString()

                        If ht("PageFlag") IsNot Nothing Then
                            Select Case ht("PageFlag").ToString()
                                Case "ToDoList"
                                    Session.Add("PageFlag", "ToDoList")
                                    Session.Add("sys_LoginFrom", "Portal")
                                    Login(txtUserID.Text)
                            End Select
                        End If
                        If ht.Contains("SSOMode") Then ViewState.Item("SSOMode") = ht("SSOMode").ToString()
                        Session.Add("sys_LoginFrom", "Portal")
                        Login(txtUserID.Text)
                    Else
                        strPage = Bsp.Utility.getMessagePageURL("Login_Load_2", Bsp.Enums.MessageType.Information, "不正確登入此系統，請由入口網站重新登入！", "關閉視窗", "")
                        FormsAuthentication.SetAuthCookie("ClosePage", False)
                        Server.Transfer(Me.ResolveUrl(strPage))
                    End If
                End If
            Else
                ''檢查郵件登入
                'If Request("ArrivalDt") IsNot Nothing AndAlso _
                '    Request("ArrivalDt").ToString().Trim() <> "" Then

                '    Dim strURL As String = PubFun.stringDecoding(Request("Content"))
                '    If strURL.Trim() = "" Then
                '        strPage = Bsp.Utility.getMessagePageURL("Default.Page_Load", enumClass.MessageType.Information, "不正確登入此系統，請由入口網站重新登入！", "關閉視窗", "")
                '        FormsAuthentication.SetAuthCookie("ClosePage", False)
                '        Server.Transfer(strPage)
                '    End If

                '    subCheckMailControl(Request("ArrivalDt"), Request("MailSeqNo"), strURL)
                'End If
            End If

            Me.SetFocus(txtUserID)
        End If
    End Sub

    'Mark by Chung 2011.05.12 目前不允許郵件登入
    'Private Sub subCheckMailControl(ByVal strArrivalDt As String, ByVal strMailSeqNo As String, ByVal strURL As String)
    '    Dim beControl As New SC_Entities.beSC_SendMailControl
    '    Dim bsControl As New SC_Service.bsSC_SendMailControl()
    '    Dim strPage As String

    '    With beControl
    '        .ArrivalDt = strArrivalDt
    '        .MailSeqNo = strMailSeqNo
    '    End With
    '    Using dt As DataTable = bsControl.QueryByKey(beControl).Tables(0)
    '        If dt.Rows.Count = 0 Then
    '            strPage = Bsp.Utility.getMessagePageURL("Default.subCheckMailControl", enumClass.MessageType.Information, "不正確登入此系統，請由入口網站重新登入！", "關閉視窗", "")
    '            FormsAuthentication.SetAuthCookie("ClosePage", False)
    '            Server.Transfer(strPage)
    '            Return
    '        End If

    '        beControl = New SC_Entities.beSC_SendMailControl(dt.Rows(0))
    '        '郵件已到期
    '        If beControl.MailInValidDt.ToString("yyyyMMddHHmmss") < Now.ToString("yyyyMMddHHmmss") Then
    '            strPage = Bsp.Utility.getMessagePageURL("Default.subCheckMailControl", enumClass.MessageType.Information, "此郵件已過有效期限，請由入口網站重新登入！", "關閉視窗", "")
    '            FormsAuthentication.SetAuthCookie("ClosePage", False)
    '            Server.Transfer(strPage)
    '            Return
    '        End If
    '        '檢查首次登入IP和此次是否相同
    '        If beControl.ReceiverIP Is Nothing OrElse beControl.ReceiverIP.Trim() = "" Then
    '            With beControl
    '                .ReceiverIP = Request.UserHostAddress
    '                .ReceiverTime = Now
    '                .LastChgDate = Now
    '            End With
    '        Else
    '            If beControl.ReceiverIP <> Request.UserHostAddress Then
    '                strPage = Bsp.Utility.getMessagePageURL("Default.subCheckMailControl", enumClass.MessageType.Information, "登入IP錯誤(IP需與第一次登入IP位置相同)，請由入口網站重新登入！", "關閉視窗", "")
    '                FormsAuthentication.SetAuthCookie("ClosePage", False)
    '                Server.Transfer(strPage)
    '                Return
    '            End If
    '            With beControl
    '                .LastChgDate = Now
    '            End With
    '        End If
    '        Try
    '            bsControl.Update(beControl)
    '        Catch ex As Exception

    '        End Try
    '        Session("PageFlag") = "ToDoList"
    '        Session.Add("LoginFrom", "Mail")
    '        subLogin(beControl.ReceiverID, Bsp.Utility.getAppSetting("StartPage"))
    '        'subLogin(beControl.ReceiverID, strURL)
    '    End Using
    'End Sub

    Private Sub Login(ByVal UserID As String)
        Login(UserID, Bsp.Utility.getAppSetting("StartPage"))
    End Sub

    Private Sub Login(ByVal UserID As String, ByVal TargetPage As String)
        Dim colSysIDs As New System.Collections.Generic.List(Of String) '20140807 wei add   系統別代碼
        Dim colCompRoles As New System.Collections.Generic.List(Of String) '20140807 wei add   授權公司代碼
        Dim colGroups As New System.Collections.Generic.List(Of String)
        Dim objSC As New SC()

        Try
            '取得SC_User資料
            Using dtUser As DataTable = objSC.GetUserInfo(UserID, "*, dbo.funGetAOrgDefine('4', DeptID) DeptName, dbo.funGetAOrgDefine('4', OrganID) OrganName,dbo.funGetACompDefine('4', CompID) CompName")
                If dtUser.Rows.Count = 0 Then
                    Throw New Exception("您沒有此系統的權限(無使用者資料)！")
                End If

                Dim beUser As New beSC_User.Row(dtUser.Rows(0))
                If beUser.BanMark.Value = "1" Then
                    Throw New Exception("您沒有此系統的權限(用户禁用中)！")
                End If

                Using dtUserGroup As DataTable = objSC.GetUserGroupInfo(UserID, "", "SysID,CompRoleID,GroupID")
                    If dtUserGroup.Rows.Count = 0 Then
                        Throw New Exception("您没有權限使用此系统！")
                    End If
                    For Each dr As DataRow In dtUserGroup.Rows
                        colSysIDs.Add(dr.Item("SysID").ToString())
                        colCompRoles.Add(dr.Item("CompRoleID").ToString())
                        colGroups.Add(dr.Item("GroupID").ToString())
                    Next
                End Using

                SC.CreateUserInfoSession(strLoginSysID, beUser.UserID.Value, beUser.UserName.Value, beUser.CompID.Value, dtUser.Rows(0).Item("CompName").ToString(), beUser.DeptID.Value, dtUser.Rows(0).Item("DeptName").ToString(), _
                                         beUser.OrganID.Value, dtUser.Rows(0).Item("OrganName").ToString(), colGroups, colSysIDs, colCompRoles)

                If Session("sys_LoginFrom") <> "LoginPage" AndAlso ViewState.Item("SSOMode") <> "SSO-SIM" Then
                    objSC.WriteLoginLog(beUser.CompID.Value, beUser.UserID.Value, beUser.UserName.Value, Request.ServerVariables("REMOTE_ADDR"), Session("sys_LoginFrom"))  '20150717 wei modify
                End If
            End Using

            FormsAuthentication.SetAuthCookie(UserID, False)

        Catch ex As Exception
            FormsAuthentication.SetAuthCookie("Temp", False)
            Dim strPage As String = ""
            If Session("sys_LoginFrom") IsNot Nothing AndAlso Session("sys_LoginFrom") = "Portal" Then
                strPage = Bsp.Utility.getMessagePageURL("Login", Bsp.Enums.MessageType.Errors, Bsp.Utility.getInnerException("Default_1", ex), "回登入页", "http://eportal.bsp")
            Else
                strPage = Bsp.Utility.getMessagePageURL("Login", Bsp.Enums.MessageType.Errors, Bsp.Utility.getInnerException("Default_1", ex), "回登入页", "~/Default.aspx")
            End If

            Server.Transfer(Me.ResolveUrl(strPage))
            Return
        End Try

        Response.Redirect(TargetPage)

        ''已為轉址後的Port, 不再轉址
        'If Request("Action") IsNot Nothing AndAlso Request("Action").ToString().ToUpper() = "OPENWEB" Then
        '    Response.Redirect(TargetPage)
        'Else
        '    If Bsp.Utility.InStr(Request.Url.Port.ToString(), "8090", "8091", "8092", "8093", "8080") Then
        '        Response.Redirect(TargetPage)
        '    Else
        '        If Bsp.Utility.InStr(Request.Url.Host.ToUpper(), "COCREDIT-1.BSP", "COCREDIT-2.BSP", "COCREDIT-3.BSP", "COCREDIT-4.BSP") Then
        '            Response.Redirect(TargetPage)
        '        Else
        '            TransferPage(TargetPage)
        '        End If
        '    End If
        'End If
    End Sub

    Private Function funCheckBrower() As Boolean
        Dim blIsWrongBrowser As Boolean = False
        Dim strMsg As String = ""
        Dim strInfoPath As String = ""
        Dim strBrowser As String = Request.Browser.Browser.ToString()

        '檢查瀏覽器是否為IE
        If strBrowser.ToUpper() <> "IE" Then
            blIsWrongBrowser = True
            strInfoPath = "Login_funCheckBrower_1"
            strMsg = "本系統須使用Microsoft Internet Explorer瀏覽！"
        End If

        If Not IsNumeric(Request.Browser.Version.ToString()) Then
            blIsWrongBrowser = True
            strInfoPath = "Login_funCheckBrower_2"
            strMsg = "無法得知Client端版本，請使用IE 5.5以上版本瀏覽！"
        Else
            If (strBrowser.ToUpper() = "IE" And CSng(Request.Browser.Version.ToString()) < 5.5) Then
                blIsWrongBrowser = True
                strInfoPath = "Login_funCheckBrower_3"
                strMsg = "本系統需用IE5.5以上之版本瀏覽！<br>請更新您的IE版本，謝謝。"
            End If
        End If
        '檢查是否支援Cookies
        If Not Request.Browser.Cookies() Then
            blIsWrongBrowser = True
            strInfoPath = "Login_funCheckBrower_1"
            strMsg = "請開啟Cookie功能，謝謝。"
        End If

        If blIsWrongBrowser = True Then
            Dim strPage As String

            strPage = Bsp.Utility.getMessagePageURL(strInfoPath, Bsp.Enums.MessageType.Errors, strMsg, "關閉視窗", "")
            FormsAuthentication.SetAuthCookie("Default", False)
            Server.Transfer(Me.ResolveUrl(strPage))
            Return False
        End If
        Return True
    End Function

    Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRunLogin.Click
        Dim objSC As New SC
        Dim intResult As Integer
        Dim strMsg As String = ""

        If txtNewPwd.Text.Trim() = "" Then
            Try
                intResult = objSC.LoginCheck(txtUserID.Text.ToString().ToUpper().Trim(), txtPwd.Text, Request.ServerVariables("REMOTE_ADDR"))
                Select Case intResult
                    Case 0  '成功
                        Session.Add("sys_LoginFrom", "LoginPage")
                        Login(txtUserID.Text.Trim().ToUpper())
                    Case 1  '禁用BanMark = '1'
                        strMsg = "此帳號禁用中"
                    Case 2  '使用期限到期
                        strMsg = "帳號使用期限已到期"
                    Case 3, 4  '查無此帳號或密碼錯誤
                        strMsg = "查無此帳號或密碼錯誤"
                    Case 5  '錯誤次數已達3次
                        strMsg = "錯誤次數已達3次"
                    Case 6  ''此帳號目前禁用中
                        strMsg = "此帳號目前禁用中"
                    Case Else
                        strMsg = "無法指出的錯誤...請通知系統人員"
                End Select

                Bsp.Utility.ShowMessage(Me, strMsg)
            Catch ex As System.Data.SqlClient.SqlException
                Bsp.Utility.ShowMessage(Me, Bsp.Utility.getInnerException("Default_2", ex))
            Catch ex As Exception
                Bsp.Utility.ShowMessage(Me, ex.Message)
            End Try
        Else
            Try
                intResult = objSC.ChangePassword(txtUserID.Text.Trim.ToUpper(), txtPwd.Text, txtNewPwd.Text)

                Select Case intResult
                    Case 0
                        strMsg = "修改密碼成功"
                    Case 1
                        strMsg = "查無此帳號或密碼錯誤"
                    Case 2
                        strMsg = "密碼錯誤超過三次"
                    Case 3
                        strMsg = "帳號已禁用"
                    Case 4
                        strMsg = "無法指出的錯誤...請通知系統人員"
                End Select

                Bsp.Utility.ShowMessage(Me, strMsg)

                If intResult <> 0 Then
                    txtPwd.Text = ""
                    txtNewPwd.Text = ""
                    txtConfirmPwd.Text = ""
                    Bsp.Utility.RunClientScript(Me, "funAction('changepwd');")
                End If
            Catch ex As Exception
                Bsp.Utility.ShowMessage(Me, Bsp.Utility.getInnerException("Default_3", ex))
            End Try
        End If
    End Sub

End Class
