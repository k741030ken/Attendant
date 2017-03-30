
Partial Class SC_SC0002
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        tdMsg.InnerText = Request("strMsg")
    End Sub
End Class
