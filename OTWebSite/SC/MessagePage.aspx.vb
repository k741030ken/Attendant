'****************************************************
'功能說明：訊息顯示頁
'建立人員：A2976
'建立日期：2007.02.16
'參數說明：InforPath=>訊息來源
'          MessageType=>訊息種類
'          ButtonTitle=>按鈕的Caption
'          NextWebPage=>下一導向頁面
'****************************************************
Partial Class SC_MessagePage
    Inherits System.Web.UI.Page
    Public buttonDisplay As String = "block"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            Dim strPath As String = Request.ApplicationPath

            If strPath = "/" Then strPath = ""

            If Not Request("InforPath") Is Nothing Then
                lblInforPath.Text = "訊息來源：" & Request("InforPath")
            End If

            '判斷訊息種類
            Select Case Request("MessageType")
                Case Bsp.Enums.MessageType.Information
                    imgInfo.ImageUrl = Page.ResolveUrl("~/images/infor.gif")
                Case Bsp.Enums.MessageType.Errors
                    imgInfo.ImageUrl = Page.ResolveUrl("~/images/error.gif")
                Case Else
                    imgInfo.ImageUrl = Page.ResolveUrl("~/images/infor.gif")
            End Select

            '若要折行,請以$$替代
            lblMessage.Text = Request("Message").ToString().Replace("$$", "<br>")

            '按鈕Caption
            If Request("ButtonTitle") Is Nothing Then
                btnGo.Text = "確 定"
            Else
                If Request("ButtonTitle").ToString().Trim() = "" Then
                    btnGo.Text = "確 定"
                ElseIf Request("ButtonTitle").ToString().Trim() = "--" Then
                    buttonDisplay = "none"
                Else
                    btnGo.Text = Request("ButtonTitle")
                End If
            End If
            'page.
            If Request("NextWebPage") Is Nothing Then
                NextWebPage.Text = ""
            Else
                NextWebPage.Text = Page.ResolveUrl(Request("NextWebPage"))
            End If

            If NextWebPage.Text = "" Then
                btnGo.Text = "關閉視窗"
            End If

            Page.SetFocus(btnGo)
        End If
    End Sub

    Protected Sub btnGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGo.Click
        Response.Redirect(NextWebPage.Text)
    End Sub
End Class
