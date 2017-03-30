'****************************************************
'功能說明：快速查詢
'建立人員：Ann
'建立日期：2014.08.20
'****************************************************

Partial Class Component_ucRelease
    Inherits System.Web.UI.UserControl
    Public Property DefaultQueryField() As String
        Get
            If ViewState.Item("DefaultQueryField") Is Nothing Then
                ViewState.Item("DefaultQueryField") = ""
            End If
            Return ViewState.Item("DefaultQueryField")
        End Get
        Set(ByVal value As String)
            ViewState.Item("DefaultQueryField") = value
        End Set
    End Property

    Public Property DefaultQueryValue() As String
        Get
            If ViewState.Item("DefaultQueryValue") Is Nothing Then
                ViewState.Item("DefaultQueryValue") = ""
            End If
            Return ViewState.Item("DefaultQueryValue")
        End Get
        Set(ByVal value As String)
            ViewState.Item("DefaultQueryValue") = value
        End Set
    End Property

    '顯示有有授權的公司
    Public Property ShowCompRole() As String
        Get
            If ViewState.Item("ShowCompRole") Is Nothing Then
                ViewState.Item("ShowCompRole") = ""
            End If
            Return ViewState.Item("ShowCompRole")
        End Get
        Set(ByVal value As String)
            ViewState.Item("ShowCompRole") = value
        End Set
    End Property
    'FunID
    Public Property FunID() As String
        Get
            If ViewState.Item("FunID") Is Nothing Then
                ViewState.Item("FunID") = ""
            End If
            Return ViewState.Item("FunID")
        End Get
        Set(ByVal value As String)
            ViewState.Item("FunID") = value
        End Set
    End Property
    'LogFunction
    Public Property LogFunction() As String
        Get
            If ViewState.Item("LogFunction") Is Nothing Then
                ViewState.Item("LogFunction") = ""
            End If
            Return ViewState.Item("LogFunction")
        End Get
        Set(ByVal value As String)
            ViewState.Item("LogFunction") = value
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

    '要傳回的欄位index從0開始(,隔開)
    Public Property ReturnColumnIndex() As String
        Get
            If ViewState.Item("ReturnColumnIndex") Is Nothing Then
                ViewState.Item("ReturnColumnIndex") = ""
            End If
            Return ViewState.Item("ReturnColumnIndex")
        End Get
        Set(ByVal value As String)
            ViewState.Item("ReturnColumnIndex") = value
        End Set
    End Property



    Public Property SelectStyle() As Bsp.Enums.SelectStyle
        Get
            If ViewState.Item("SelectStyle") Is Nothing Then
                ViewState.Item("SelectStyle") = Bsp.Enums.SelectStyle.RadioButton
            End If
            Return ViewState.Item("SelectStyle")
        End Get
        Set(ByVal value As Bsp.Enums.SelectStyle)
            ViewState.Item("SelectStyle") = value
        End Set
    End Property

    '傳回的Value(|$|隔開)
    Public ReadOnly Property ReturnValue() As String
        Get
            Return txtValueResult.Text.Trim
        End Get
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

    Public Property Fields() As FieldState()
        Get
            Return ViewState.Item("Fields")
        End Get
        Set(ByVal value As FieldState())
            ViewState.Item("Fields") = value
        End Set
    End Property

    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreRender

        Dim strUrl As String

        strUrl = ResolveUrl("~/HR0/HR0200.aspx")
        strUrl = strUrl & "?ControlSessionID=" & ViewState.Item("ControlSessionID")
        txtParam.Text = "?FunID=Release&Path=" & strUrl

    End Sub

    Public Sub OpenSelect()
        If ShowCompRole Is Nothing Then
            Bsp.Utility.ShowFormatMessage(Me.Page, "W_00100", "ShowCompRole")
        ElseIf ShowCompRole.ToString.Length = 0 Then
            Bsp.Utility.ShowFormatMessage(Me.Page, "W_00100", "ShowCompRole")
        Else
            Dim strControlSessionID As String = "Release_" & Now.ToString("HHmmss") & Now.Millisecond.ToString("000")
            ViewState.Item("ControlSessionID") = strControlSessionID

            Dim aryParam(6) As Object
            aryParam(0) = ShowCompRole
            aryParam(1) = ReturnColumnIndex
            aryParam(2) = Fields
            aryParam(3) = DefaultQueryField
            aryParam(4) = DefaultQueryValue
            aryParam(5) = FunID
            aryParam(6) = LogFunction

            Session(strControlSessionID) = aryParam
            Const CS_FUN As String = "{0}(document.forms[0].{1},document.forms[0].{2}, '{3}', '{4}', '{5}', '{6}');"
            Dim strText As String = String.Format(CS_FUN, "PageQuerySelectReturnValue", txtParam.ClientID, txtValueResult.ClientID, DataControlID, WindowWidth, WindowHeight, Me.ClientID)

            Bsp.Utility.RunClientScript(Me.Page, strText)
        End If
    End Sub


End Class
