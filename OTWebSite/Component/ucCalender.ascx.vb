Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Partial Class Component_ucCalender
    Inherits System.Web.UI.UserControl

    Public Property CssClass() As String
        Get
            Return txtDate.CssClass
        End Get
        Set(ByVal value As String)
            txtDate.CssClass = value
        End Set
    End Property

    Public Property DateText() As String
        Get
            Return txtDate.Text
        End Get
        Set(ByVal value As String)
            If value.Length = 8 Then
                value = value.Substring(0, 4) + "/" + value.Substring(4, 2) + "/" + value.Substring(6, 2)
            Else
                txtDate.Text = value
            End If
        End Set
    End Property

    Public Property DateTextNoSlash() As String
        Get
            Dim strDate As String = String.Empty
            If txtDate.Text.Length <> 0 Then
                strDate = txtDate.Text.Substring(0, 4) + txtDate.Text.Substring(5, 2) + txtDate.Text.Substring(8, 2)
            Else
                strDate = ""
            End If
            Return strDate
        End Get
        Set(ByVal value As String)
            txtDate.Text = value
        End Set
    End Property

    Public Property Enabled() As Boolean
        Get
            Return txtDate.Enabled
        End Get
        Set(ByVal value As Boolean)
            imgDate.Enabled = value
            txtDate.Enabled = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

End Class
