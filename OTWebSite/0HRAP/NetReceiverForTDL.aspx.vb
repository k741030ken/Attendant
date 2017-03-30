'************************************************************
'功能說明：登入畫面For人資待辦
'建立人員：Weicheng
'建立日期：2015.11.04
'************************************************************
Imports System.Data
Imports System.Security.Cryptography
Imports System.IO
Imports System.Net
Imports System.Net.Sockets 'HttpWebRequest、HttpWebResponse類別

Partial Class NetReceiverForTDL
    Inherits System.Web.UI.Page
    Dim strLoginSysID As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        FormsAuthentication.SignOut()
        Session.Clear()

        Dim objSC As New SC
        Dim strPage As String
        Dim strUserID As String

        If Request("Action") Is Nothing OrElse (Request("Action") IsNot Nothing AndAlso Request("Action").ToString().ToUpper() <> "OPENWEB") Then
            If Bsp.MySettings.IsSystemClose = "Y" Then
                strPage = Bsp.Utility.getMessagePageURL("Login_Load_1", Bsp.Enums.MessageType.Information, "目前網站關閉中，請稍候再試！", "關閉視窗", "")

                FormsAuthentication.SetAuthCookie("ClosePage", False)

                Server.Transfer(Me.ResolveUrl(strPage))
            End If
        End If

        Response.Headers.Add("P3P", "CP=\""NOI ADM DEV COM NAV OUR\""") '20151015 wei add

        strLoginSysID = "wHRMS"
        If Not IsPostBack() Then
            '企業平台登入檢查
            'If Request.Form.Count > 0 Then
            Dim ht As New Hashtable
            'For Each strKey As String In Request.Form.AllKeys
            '    ht.Add(strKey, Request.Form(strKey))
            'Next
            ht.Add("SSOTime", Request("SSOTime"))
            ht.Add("UserID", Request("UserID"))
            ht.Add("MD5", Request("MD5"))
            ht.Add("TargetPage", Request("TargetPage"))
            ht.Add("CompID", Request("CompID"))
            ht.Add("ApplyID", Request("ApplyID"))
            ht.Add("ApplyTime", Request("ApplyTime"))
            ht.Add("Seq", Request("Seq"))
            ht.Add("Status", Request("Status"))
            ht.Add("MainFlag", Request("MainFlag"))

            ViewState.Item("SSOMode") = ""

            If ht.ContainsKey("SSOTime") AndAlso ht.ContainsKey("UserID") AndAlso ht.ContainsKey("MD5") AndAlso ht("SSOTime").ToString() <> "" Then

                ' MD5 Policy	CHRMS + UserID;2;4 + SSOTime;1;5 + UserID;1;4  
                '=>順序是MID的邏輯，故若USER=200001，SSOTime="15:20:20" =>CHRMS + "0000" + "15:20" + "2000"
                '=>要注意的是Mid函數的索引值是由1開始，NET Substrig是由0開始
                Dim strBeforeHash As String = "wHRMS" & ht("UserID").ToString().Substring(1, 4) & ht("SSOTime").ToString().Substring(0, 5) & ht("UserID").ToString().Substring(0, 4)

                Dim MD5hasher As MD5 = MD5.Create()
                Dim myMD5Data As Byte() = MD5hasher.ComputeHash(Encoding.Default.GetBytes(strBeforeHash))
                Dim strAfterHash As String = ""

                For i As Integer = 0 To myMD5Data.Length - 1
                    strAfterHash &= myMD5Data(i).ToString("x2")
                    'ToString("X2")中的"X2"是什么意思
                    '如果两个数10和26，正常情况十六进制显示0xA、0x1A，这样看起来不整齐，为了好看，我们可以指定X2，这样显示出来就是：0x0A、0x1A
                    'msdn里面的string(Format)
                Next

                If ht("MD5").ToString() = strAfterHash Then

                    strUserID = ht("UserID").ToString()

                    If ht("TargetPage") IsNot Nothing Then
                        Session.Add("TargetPage", ht("TargetPage").ToString())
                        Session.Add("sys_LoginFrom", "Portal")
                        Session.Add("PageSource", "GS1000")
                        Session.Add("hidCompID", ht("CompID").ToString())
                        Session.Add("ApplyID", ht("ApplyID").ToString())
                        Session.Add("ApplyTime", ht("ApplyTime").ToString())
                        Session.Add("Seq", ht("Seq").ToString())
                        Session.Add("Status", ht("Status").ToString())
                        Session.Add("MainFlag", ht("MainFlag").ToString())
                        Login(strUserID, ht("TargetPage").ToString())
                    End If
                    If ht.Contains("SSOMode") Then ViewState.Item("SSOMode") = ht("SSOMode").ToString()
                    Session.Add("sys_LoginFrom", "Portal")
                    Login(strUserID)

                Else
                    'strPage = Bsp.Utility.getMessagePageURL("Login_Load_2", Bsp.Enums.MessageType.Information, "不正确登入此系统，請由入口网站重新登入1！", "关闭窗口", "")
                    strPage = Bsp.Utility.getMessagePageURL("Login_Load_2", Bsp.Enums.MessageType.Information, "不正確登入此系统，請由入口網站重新登入1！MD5=" & ht("MD5").ToString() & ",strAfterHash=" & strAfterHash & ",strBeforeHash=" & strBeforeHash & ",", "關閉視窗", "")
                    FormsAuthentication.SetAuthCookie("ClosePage", False)
                    Server.Transfer(Me.ResolveUrl(strPage))
                End If
            Else
                strPage = Bsp.Utility.getMessagePageURL("Login_Load_2", Bsp.Enums.MessageType.Information, "不正確登入此系统，請由入口網站重新登入2！", "關閉視窗", "")
                FormsAuthentication.SetAuthCookie("ClosePage", False)
                Server.Transfer(Me.ResolveUrl(strPage))
            End If
        Else
            strPage = Bsp.Utility.getMessagePageURL("Login_Load_2", Bsp.Enums.MessageType.Information, "不正確登入此系统，請由入口網站重新登入4！", "關閉視窗", "")
            FormsAuthentication.SetAuthCookie("ClosePage", False)
            Server.Transfer(Me.ResolveUrl(strPage))
        End If
    End Sub

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
End Class
