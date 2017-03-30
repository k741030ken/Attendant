'****************************************************
'功能說明：考核補充說明
'建立人員：Micky Sung
'建立日期：2015.11.04
'****************************************************
Imports System.Data

Partial Class GS_GS1170
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then

        End If
    End Sub

    Protected Overrides Sub BaseOnPageCall(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim objSC As New SC()
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

            If ht.ContainsKey("CompID") Then
                'lblSignName.Text = ht("SignName").ToString()
                lblComment.Text = ht("Comment").ToString()
                'lblComment_Adjust.Text = ht("Comment_Adjust").ToString()   '20160720 wei del
            Else
                Return
            End If
        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnActionX"    '返回
                Bsp.Utility.RunClientScript(Me, "window.top.close();")
        End Select
    End Sub

End Class
