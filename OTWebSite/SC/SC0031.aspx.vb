'****************************************************
'功能說明：友站連結轉址網頁
'建立人員：Chung
'建立日期：2011.05.13
'****************************************************
Partial Class SC_SC0031
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Bsp.Utility.IsStringNull(Request("Web")) <> "" Then
            Dim objSC As New SC

            Select Case Request("Web").ToString().ToUpper()
                Case "ELOAN", "CAAUT", "SILK", "ICREDIT2", "OVERSEAS"
                    'Response.Redirect("/System/CC6110.aspx?Site=" & Request("Web").ToString().ToUpper())
            End Select
        End If
    End Sub
End Class
