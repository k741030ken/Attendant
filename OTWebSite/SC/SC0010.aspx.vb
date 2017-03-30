'**************************************************************
'功能說明：Banner頁
'建立人員：Chung
'建立日期：2011.05.13
'**************************************************************
Imports System.Data

Partial Class SC_SC0010
    Inherits CommonBase

    Protected Sub btnBackHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBackHome.Click
        If Session(Bsp.MySettings.UserProfileSessionName) Is Nothing Then
            Dim strLoginPage As String = ResolveUrl(Bsp.MySettings.LoginPage)

            Bsp.Utility.RunClientScript(Me, "window.top.location = '" & strLoginPage & "';")
        Else
            Dim strHomePage As String = ResolveUrl(Bsp.MySettings.StartPage)

            Bsp.Utility.RunClientScript(Me, "window.top.location = '" & strHomePage & "';")
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnLogout.Attributes.Add("onmouseover", "ChangeColor(this, '#fce031');")
        btnClose.Attributes.Add("onmouseover", "ChangeColor(this, '#fce031');")
        btnLogout.Attributes.Add("onmouseout", "ChangeColor(this, '#fff799');")
        btnClose.Attributes.Add("onmouseout", "ChangeColor(this, '#fff799');")
        If Not IsPostBack() Then
            'Dim UserProfile As New UserProfile
            If Session.Item("sys_LoginFrom") IsNot Nothing AndAlso Session.Item("sys_LoginFrom") = "Portal" Then
                btnLogout.Visible = False
                btnClose.Visible = True
            Else
                btnLogout.Visible = True
                btnClose.Visible = False
            End If

            '顯示測試網站字樣
            If Bsp.MySettings.ProductionFlag = "1" Then
                lblProductionFlag.Visible = False
            End If


            lblUserID.Text = UserProfile.ActUserID
            lblUserName.Text = UserProfile.ActUserName & "-" & UserProfile.ActCompName

            If UserProfile.UserID = UserProfile.ActUserID Then
                lblAgent.Visible = False
                btCloseAgency.Visible = False
                lb_3.Visible = False
            Else
                '判斷代理種類 -- 以後要再加判斷

                Dim objSC As New SC

                Using dt As DataTable = objSC.GetAgency(UserProfile.UserID, UserProfile.ActUserID, _
                                                        "AgencyType, dbo.funGetAOrgDefine('3', AgentUserID) AgentUserName, dbo.funGetAOrgDefine('3', UserID) UserName")
                    If dt.Rows.Count > 0 Then
                        txtAgencyType.Text = dt.Rows(0).Item("AgencyType").ToString()
                        If txtAgencyType.Text = "0" Then
                            lb_1.Visible = False
                            lb_2.Visible = False
                            lb_3.Visible = True
                        Else
                            lb_3.Visible = False
                        End If
                        lblAgent.Visible = True
                        lblAgent.Text = "目前代理：" & UserProfile.UserID & "　" & UserProfile.UserName
                        btCloseAgency.Visible = True
                        txtStatus.Text = "1" '代理中

                    Else
                        If UserProfile.ActGroupID.Contains("22") Then
                            txtAgencyType.Text = "1"
                            lb_3.Visible = False
                            lblAgent.Visible = True
                            lblAgent.Text = "目前代理：" & UserProfile.UserID & "　" & UserProfile.UserName
                            btCloseAgency.Visible = True
                            txtStatus.Text = "1" '代理中
                        Else
                            SC.CancelAgent()

                            lblAgent.Visible = False
                            btCloseAgency.Visible = False
                            lb_3.Visible = False

                            Bsp.DB.ExecuteNonQuery(CommandType.StoredProcedure, "SP_SC0001_1", New Data.Common.DbParameter() { _
                            Bsp.DB.getDbParameter("@argIP", Request.ServerVariables("REMOTE_ADDR")), _
                            Bsp.DB.getDbParameter("@argSite", ""), _
                            Bsp.DB.getDbParameter("@argUserID", UserProfile.ActUserID), _
                            Bsp.DB.getDbParameter("@argActAs", UserProfile.UserID)})
                        End If
                    End If
                End Using
            End If
        End If
    End Sub

    Protected Sub btCloseAgency_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btCloseAgency.Click
        SC.CancelAgent()

        lblAgent.Visible = False
        btCloseAgency.Visible = False
        lb_1.Visible = True
        lb_2.Visible = True
        lb_3.Visible = False

        txtStatus.Text = "0" '結束代理
        txtAgencyType.Text = ""

        Bsp.DB.ExecuteNonQuery(CommandType.StoredProcedure, "SP_SC0001_1", New Data.Common.DbParameter() { _
                                    Bsp.DB.getDbParameter("@argIP", Request.ServerVariables("REMOTE_ADDR")), _
                                    Bsp.DB.getDbParameter("@argSite", ""), _
                                    Bsp.DB.getDbParameter("@argUserID", UserProfile.ActUserID), _
                                    Bsp.DB.getDbParameter("@argActAs", UserProfile.UserID)})

        Bsp.Utility.RunClientScript(Me, "window.top.location='" & ResolveUrl(Bsp.MySettings.StartPage) & "';")
    End Sub

    Protected Sub btnLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogout.Click
        Bsp.Utility.RunClientScript(Me, "window.top.location = '" & ResolveUrl("~/Default.aspx") & "';")
    End Sub

    Protected Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Bsp.Utility.RunClientScript(Me, "window.top.close();")
    End Sub
End Class
