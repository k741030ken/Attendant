'****************************************************
'功能說明：快速查詢
'建立人員：Ann
'建立日期：2013.11.27
'****************************************************

Partial Class Component_ucButtonWorkType
    Inherits System.Web.UI.UserControl
    Public Property Enabled() As Boolean
        Get
            Return btnSelect.Enabled
        End Get
        Set(ByVal value As Boolean)
            btnSelect.Enabled = value
        End Set
    End Property

    '20161108 Beatrice Add
    '是否撈取新增待異動組織(OrganizationWait)
    Public Property IsWait() As Boolean
        Get
            If ViewState.Item("_IsWait") Is Nothing Then
                ViewState.Item("_IsWait") = False
            End If
            Return ViewState.Item("_IsWait")
        End Get
        Set(ByVal value As Boolean)
            ViewState.Item("_IsWait") = value
        End Set
    End Property

    '20161108 Beatrice Add
    '待異動組織(OrganizationWait)生效日期
    Public Property ValidDate() As String
        Get
            If ViewState.Item("_ValidDate") Is Nothing Then
                ViewState.Item("_ValidDate") = ""
            End If
            Return ViewState.Item("_ValidDate")
        End Get
        Set(ByVal value As String)
            ViewState.Item("_ValidDate") = value
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

    Public Property QueryCompID() As String
        Get
            If ViewState.Item("QueryCompID") Is Nothing Then
                ViewState.Item("QueryCompID") = ""
            End If
            Return ViewState.Item("QueryCompID")
        End Get
        Set(ByVal value As String)
            ViewState.Item("QueryCompID") = value
        End Set
    End Property

    Public Property QueryEmpID() As String
        Get
            If ViewState.Item("QueryEmpID") Is Nothing Then
                ViewState.Item("QueryEmpID") = ""
            End If
            Return ViewState.Item("QueryEmpID")
        End Get
        Set(ByVal value As String)
            ViewState.Item("QueryEmpID") = value
        End Set
    End Property
    '20141224 wei add 只顯示該單位的工作性質
    Public Property QueryOrganID() As String
        Get
            If ViewState.Item("QueryOrganID") Is Nothing Then
                ViewState.Item("QueryOrganID") = ""
            End If
            Return ViewState.Item("QueryOrganID")
        End Get
        Set(ByVal value As String)
            ViewState.Item("QueryOrganID") = value
        End Set
    End Property
    Public Property DefaultWorkType() As String
        Get
            If ViewState.Item("DefaultWorkType") Is Nothing Then
                ViewState.Item("DefaultWorkType") = ""
            End If
            Return ViewState.Item("DefaultWorkType")
        End Get
        Set(ByVal value As String)
            ViewState.Item("DefaultWorkType") = value
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

        strUrl = ResolveUrl("PageWorkType.aspx")
        strUrl = strUrl & "?ControlSessionID=" & ViewState.Item("ControlSessionID")
        txtParam.Text = "?FunID=WQuery&Path=" & strUrl

    End Sub

    Public Sub OpenSelect()

        Dim strControlSessionID As String = "ButtonWorkType_" & Now.ToString("HHmmss") & Now.Millisecond.ToString("000")
        ViewState.Item("ControlSessionID") = strControlSessionID

        Dim aryParam(6) As Object
        aryParam(0) = QueryCompID
        aryParam(1) = QueryEmpID
        aryParam(2) = DefaultWorkType
        aryParam(3) = Fields
        aryParam(4) = QueryOrganID
        aryParam(5) = IsWait
        aryParam(6) = ValidDate

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
