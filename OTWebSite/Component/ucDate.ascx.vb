
Partial Class Component_ucDate
    Inherits System.Web.UI.UserControl

    Public Property DateControlName() As String
        Get
            If ViewState.Item("_DataControlName") Is Nothing Then
                ViewState.Item("_DataControlName") = ""
            End If
            Return ViewState.Item("_DataControlName")
        End Get
        Set(ByVal value As String)
            ViewState.Item("_DataControlName") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim obj As Object = Me.Page.Form.FindControl(DateControlName)

            If obj IsNot Nothing Then
                btnCalendar.Attributes.Add("onclick", "funGetDate('" & DateControlName & "', '" & CType(obj, TextBox).Text & "');")
            End If
        End If
    End Sub

End Class
