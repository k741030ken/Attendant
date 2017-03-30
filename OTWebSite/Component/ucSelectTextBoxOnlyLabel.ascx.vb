Imports System.Data

'****************************************************
'功能說明：
'建立人員：
'建立日期：2016.07.04
'****************************************************
Partial Class Component_ucSelectTextBoxOnlyLabel
    Inherits System.Web.UI.UserControl

    Public Property LoadjQuery() As Boolean
        Get
            If ViewState.Item("LoadjQuery") Is Nothing Then
                ViewState.Item("LoadjQuery") = False
            End If
            Return ViewState.Item("LoadjQuery")
        End Get
        Set(ByVal value As Boolean)
            If value = True Then
                Dim script As New HtmlGenericControl("script")
                script.Attributes.Add("type", "text/javascript")
                script.Attributes.Add("src", "../ClientFun/jquery-1.8.3.min.js")
                Page.Header.Controls.Add(script)
            End If
        End Set
    End Property

    Public Property LoadjQueryUI() As Boolean
        Get
            If ViewState.Item("LoadjQueryUI") Is Nothing Then
                ViewState.Item("LoadjQueryUI") = False
            End If
            Return ViewState.Item("LoadjQueryUI")
        End Get
        Set(ByVal value As Boolean)
            If value = True Then
                Dim script As New HtmlGenericControl("script")
                script.Attributes.Add("type", "text/javascript")
                script.Attributes.Add("src", "../ClientFun/jquery-ui-1.8.24.custom.js")
                Page.Header.Controls.Add(script)

                Dim link As New HtmlGenericControl("link")
                link.Attributes.Add("rel", "stylesheet")
                link.Attributes.Add("type", "text/css")
                link.Attributes.Add("href", "../css/smoothness/jquery-ui-1.8.24.custom.css")
                Page.Header.Controls.Add(link)
            End If
        End Set
    End Property

    '查詢SQL
    Public Property QuerySQL() As String
        Get
            If ViewState.Item("QuerySQL") Is Nothing Then
                ViewState.Item("QuerySQL") = ""
            End If
            Return ViewState.Item("QuerySQL")
        End Get
        Set(ByVal value As String)
            ViewState.Item("QuerySQL") = value
        End Set
    End Property

    '查詢值
    Public Property DataText() As String
        Get
            Return txtSelectText.Text
        End Get
        Set(ByVal value As String)
            txtSelectText.Text = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

        End If
    End Sub

    Public Sub LoadData()
        GetData()
    End Sub

    Public Sub GetData()
        If ViewState.Item("QuerySQL") <> "" Then
            Using dt = Bsp.DB.ExecuteDataSet(CommandType.Text, ViewState.Item("QuerySQL"), "eHRMSDB").Tables(0)
                ddlQueryValue.Items.Clear()
                For i As Integer = 0 To dt.Rows.Count - 1
                    ddlQueryValue.Items.Insert(0, New ListItem(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
                Next
            End Using
        End If
    End Sub

    Public Delegate Sub SelectTextChangedHandler(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event ucSelectTextChangedHandler_TextChange As SelectTextChangedHandler
    Protected Overridable Sub SelectTextChanged(ByVal e As System.EventArgs)
        RaiseEvent ucSelectTextChangedHandler_TextChange(Me, e)
    End Sub

    Protected Sub txtSelectText_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTextChange.Click
        SelectTextChanged(e)
    End Sub
End Class
