'****************************************************
'功能說明：快速查詢
'建立人員：Ann
'建立日期：2013.11.27
'****************************************************

Partial Class Component_ucButtonMultiListBox
    Inherits System.Web.UI.UserControl

    Public Property Enabled() As Boolean
        Get
            Return btnSelect.Enabled
        End Get
        Set(ByVal value As Boolean)
            btnSelect.Enabled = value
        End Set
    End Property

    '顯示的Button文字
    Public Property ButtonText() As String
        Get
            If ViewState.Item("ButtonText") Is Nothing Then
                ViewState.Item("ButtonText") = "..."
            End If
            Return ViewState.Item("ButtonText")
        End Get
        Set(ByVal value As String)
            ViewState.Item("ButtonText") = value
        End Set
    End Property

    Public Property ButtonHint() As String
        Get
            If ViewState.Item("ButtonHint") Is Nothing Then
                ViewState.Item("ButtonHint") = "..."
            End If
            Return ViewState.Item("ButtonHint")
        End Get
        Set(ByVal value As String)
            ViewState.Item("ButtonHint") = value
        End Set
    End Property

    Public Property Fields() As FieldState()
        Get
            Return ViewState.Item("Fields")
        End Get
        Set(ByVal value As FieldState())
            ViewState.Item("Fields") = value
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

    '查詢ConnectionString
    Public Property ConnStr() As String
        Get
            If ViewState.Item("ConnStr") Is Nothing Then
                ViewState.Item("ConnStr") = "eHRMSDB"
            End If
            Return ViewState.Item("ConnStr")
        End Get
        Set(ByVal value As String)
            ViewState.Item("ConnStr") = value
        End Set
    End Property

    '已選欄位名稱
    Public Property DefaultField() As String
        Get
            If ViewState.Item("DefaultField") Is Nothing Then
                ViewState.Item("DefaultField") = ""
            End If
            Return ViewState.Item("DefaultField")
        End Get
        Set(ByVal value As String)
            ViewState.Item("DefaultField") = value
        End Set
    End Property

    '已選欄位值
    Public Property DefaultValue() As String
        Get
            If ViewState.Item("DefaultValue") Is Nothing Then
                ViewState.Item("DefaultValue") = ""
            End If
            Return ViewState.Item("DefaultValue")
        End Get
        Set(ByVal value As String)
            ViewState.Item("DefaultValue") = value
        End Set
    End Property

    '開啟視窗的寬度, Default 800
    Public Property WindowWidth() As String
        Get
            If ViewState.Item("WindowWidth") Is Nothing Then
                ViewState.Item("WindowWidth") = "800"
            End If
            Return ViewState.Item("WindowWidth")
        End Get
        Set(ByVal value As String)
            ViewState.Item("WindowWidth") = value
        End Set
    End Property

    '開啟視窗的高度, Default 600
    Public Property WindowHeight() As String
        Get
            If ViewState.Item("WindowHeight") Is Nothing Then
                ViewState.Item("WindowHeight") = "600"
            End If
            Return ViewState.Item("WindowHeight")
        End Get
        Set(ByVal value As String)
            ViewState.Item("WindowHeight") = value
        End Set
    End Property

    '返回時寫入的控制項ID，一定要為textbox
    Public Property DataControlID() As String
        Get
            If ViewState.Item("DataControlID") Is Nothing Then
                ViewState.Item("DataControlID") = ""
            End If
            Return ViewState.Item("DataControlID")
        End Get
        Set(ByVal value As String)
            ViewState.Item("DataControlID") = value
        End Set
    End Property

    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
        btnSelect.Text = ButtonText
        btnSelect.ToolTip = ButtonHint

        Dim strUrl As String

        strUrl = ResolveUrl("PageMultiListBox.aspx")
        strUrl = strUrl & "?ControlSessionID=" & ViewState.Item("ControlSessionID")
        txtParam.Text = "?FunID=PQuery&Path=" & strUrl

    End Sub

    Public Sub OpenSelect()

        Dim strControlSessionID As String = "ButtonMultiListBox_" & Now.ToString("HHmmss") & Now.Millisecond.ToString("000")
        ViewState.Item("ControlSessionID") = strControlSessionID

        Dim aryParam(4) As Object
        aryParam(0) = QuerySQL
        aryParam(1) = ConnStr
        aryParam(2) = Fields
        aryParam(3) = DefaultField
        aryParam(4) = DefaultValue

        Session(strControlSessionID) = aryParam
        Const CS_FUN As String = "{0}(document.forms[0].{1},document.forms[0].{2}, '{3}', '{4}', '{5}', '{6}');"
        Dim strText As String = String.Format(CS_FUN, "PageQuerySelectReturnValue", txtParam.ClientID, txtValueResult.ClientID, DataControlID, WindowWidth, WindowHeight, Me.ClientID)

        Bsp.Utility.RunClientScript(Me.Page, strText)
        'End If
    End Sub

    Protected Sub btnSelect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        OpenSelect()
    End Sub
End Class
