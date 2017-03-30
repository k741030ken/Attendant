'****************************************************
'功能說明：Title及功能按鈕頁
'建立人員：Chung
'建立日期：2011.05.13
'****************************************************
Partial Class SC_SC0060
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            If Bsp.Utility.IsStringNull(Request("FunID")).Trim() <> "" Then
                ViewState.Item("FunID") = Request("FunID").ToString()
                UserProfile.SelectFunID = Request("FunID").ToString()
                ViewState.Item("CompRoleID") = UserProfile.SelectCompRoleID 'Request("CompRoleID").ToString()
                ViewState.Item("SysID") = UserProfile.LoginSysID
            Else
                If Me.StateTransfer IsNot Nothing Then
                    Dim ti As TransferInfo = Me.StateTransfer

                    ViewState("FunID") = ti.CalleePageID
                    ViewState.Item("CompRoleID") = UserProfile.SelectCompRoleID
                    ViewState.Item("SysID") = UserProfile.LoginSysID

                    '由CommandList來判斷是否回主頁
                    If ti.CommandList IsNot Nothing AndAlso ti.CommandList.Length > 0 Then
                        ucButtonPermission.CheckRight = False
                        ucButtonPermission.ButtonList = ti.CommandList
                        ucTitle.CaptionID = ti.CallerPageID
                    Else
                        ucButtonPermission.CheckRight = ti.CheckRight
                    End If
                End If
            End If
        End If

        ucTitle.FunID = ViewState.Item("FunID")
        ucTitle.SysID = ViewState.Item("SysID")
        ucButtonPermission.FunID = ViewState.Item("FunID")
        ucButtonPermission.SysID = ViewState.Item("SysID")
        ucButtonPermission.CompRoleID = ViewState.Item("CompRoleID")

        ucButtonPermission.DoLoad()

        If ucButtonPermission.ButtonCount <= 0 Then
            tblBody.Height = "22px"
            trFun.Style.Item("display") = "none"
            Bsp.Utility.RunClientScript(Me, "window.parent.fraSubmain.rows = '22,*';")
        End If
    End Sub

End Class
