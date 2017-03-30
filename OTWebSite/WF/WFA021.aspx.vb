'****************************************************
'功能說明：流程訊息顯示頁
'建立人員：Chung
'建立日期：2010.10.15
'參數說明：InforPath=>訊息來源
'          MessageType=>訊息種類
'          NextWebPage=>下一導向頁面
'****************************************************
Partial Class WF_WFA021
    Inherits PageBase

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If ti.Args.Length > 0 Then
            Object2ViewState(ti.Args)
        End If
        Dim strPath As String = Request.ApplicationPath
        Session.Remove("CurrentFlow")

        If strPath = "/" Then strPath = ""

        If Not ViewState.Item("InforPath") Is Nothing Then
            lblInforPath.Text = "訊息來源：" & ViewState.Item("InforPath")
        End If

        '判斷訊息種類
        If ViewState.Item("MessageType") IsNot Nothing Then
            Select Case ViewState.Item("MessageType").ToString()
                Case "MESSAGE"
                    imgInfo.ImageUrl = strPath & "/images/infor.gif"
                Case "ERROR"
                    imgInfo.ImageUrl = strPath & "/images/error.gif"
                Case Else
                    imgInfo.ImageUrl = strPath & "/images/infor.gif"
            End Select
        Else
            imgInfo.ImageUrl = strPath & "/images/infor.gif"
        End If
        lblMessage.Text = Server.UrlDecode(ViewState.Item("Message"))
        '按鈕Caption
        If ViewState.Item("ButtonTitle") Is Nothing Then
            btnGo.Text = "返 回"
        Else
            If ViewState.Item("ButtonTitle").ToString().Trim() = "" Then
                btnGo.Text = "返 回"
            Else
                btnGo.Text = ViewState.Item("ButtonTitle")
            End If
        End If
        'page.
        If ViewState.Item("NextWebPage") Is Nothing Then
            NextWebPage.Text = "~/WF/WFA000.aspx"
        Else
            NextWebPage.Text = ViewState.Item("NextWebPage")
        End If


        Page.SetFocus(btnGo)
    End Sub

    Protected Sub btnGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGo.Click
        Me.TransferFramePage(NextWebPage.Text, Nothing, "")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            If StateTransfer Is Nothing Then
                Dim strPath As String = Request.ApplicationPath

                If Not Request("InforPath") Is Nothing Then
                    lblInforPath.Text = "訊息來源：" & Request("InforPath").ToString()
                End If

                If strPath.Length > 0 AndAlso strPath.Substring(strPath.Length - 1, 1) = "/" Then
                    strPath = strPath.Substring(0, strPath.Length - 1)
                End If

                '判斷訊息種類
                If Request("MessageType") IsNot Nothing Then
                    Select Case Request("MessageType").ToString()
                        Case "MESSAGE"
                            imgInfo.ImageUrl = strPath & "/images/infor.gif"
                        Case "ERROR"
                            imgInfo.ImageUrl = strPath & "/images/error.gif"
                        Case Else
                            imgInfo.ImageUrl = strPath & "/images/infor.gif"
                    End Select
                Else
                    imgInfo.ImageUrl = strPath & "/images/infor.gif"
                End If
                lblMessage.Text = Server.UrlDecode(Request("Message").ToString())
                '按鈕Caption
                If Request("ButtonTitle") Is Nothing Then
                    btnGo.Text = "返 回"
                Else
                    If Request("ButtonTitle").ToString().Trim() = "" Then
                        btnGo.Text = "返 回"
                    Else
                        btnGo.Text = Request("ButtonTitle").ToString()
                    End If
                End If
                'page.
                If Request("NextWebPage") Is Nothing OrElse Request("NextWebPage").ToString().Trim() = "" Then
                    NextWebPage.Text = ResolveUrl("~/WF/WFA000.aspx")
                Else
                    NextWebPage.Text = ResolveUrl(Request("NextWebPage").ToString())
                End If


                Page.SetFocus(btnGo)
            End If
        End If
    End Sub
End Class
