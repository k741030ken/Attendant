'****************************************************
'功能說明：快速查詢
'建立人員：Chung
'建立日期：2013.03.26
'****************************************************

Partial Class Component_ucButtonQuerySelect
    Inherits System.Web.UI.UserControl

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

    Public Enum enmButtonStyle
        Button = 1
        LinkButton = 2
    End Enum

    Public Property ButtonStyle() As enmButtonStyle
        Get
            If ViewState.Item("ButtonStyle") Is Nothing Then
                ViewState.Item("ButtonStyle") = "1"
            End If
            Return ViewState.Item("ButtonStyle")
        End Get
        Set(ByVal value As enmButtonStyle)
            ViewState.Item("ButtonStyle") = value
        End Set
    End Property

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

    Public Property Enabled() As Boolean
        Get
            If ButtonStyle = enmButtonStyle.LinkButton Then
                Return lbSelect.Enabled
            Else
                Return btnSelect.Enabled
            End If
        End Get
        Set(ByVal value As Boolean)
            btnSelect.Enabled = value
            lbSelect.Enabled = value
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

    Public Property InAjax() As Boolean
        Get
            If ViewState.Item("InAjax") Is Nothing Then
                ViewState.Item("InAjax") = False
            End If
            Return ViewState.Item("InAjax")
        End Get
        Set(ByVal value As Boolean)
            ViewState.Item("InAjax") = value
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

    Public Property Fields() As FieldState()
        Get
            Return ViewState.Item("Fields")
        End Get
        Set(ByVal value As FieldState())
            ViewState.Item("Fields") = value
        End Set
    End Property

    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
        btnSelect.Text = ButtonText
        lbSelect.Text = ButtonText
        btnSelect.ToolTip = ButtonHint
        lbSelect.ToolTip = ButtonHint

        Dim strUrl As String

        Select Case SelectStyle
            Case Bsp.Enums.SelectStyle.CheckBox

                strUrl = ResolveUrl("PageQueryMultiSelect.aspx")
                strUrl = strUrl & "?ControlSessionID=" & ViewState.Item("ControlSessionID")
                txtParam.Text = "?FunID=MQuery&Path=" & strUrl

            Case Bsp.Enums.SelectStyle.RadioButton

                strUrl = ResolveUrl("PageQuerySelect.aspx")
                strUrl = strUrl & "?ControlSessionID=" & ViewState.Item("ControlSessionID")
                txtParam.Text = "?FunID=QFind&Path=" & strUrl

        End Select

        Select Case ButtonStyle
            Case enmButtonStyle.Button
                btnSelect.Visible = True
                lbSelect.Visible = False
            Case enmButtonStyle.LinkButton
                btnSelect.Visible = False
                lbSelect.Visible = True
        End Select
    End Sub

    Public Sub OpenSelect()
        If QuerySQL Is Nothing Then
            If InAjax Then
                Bsp.Utility.ShowFormatMessageForAjax(Me.Page, "W_00100", "QuerySQL")
            Else
                Bsp.Utility.ShowFormatMessage(Me.Page, "W_00100", "QuerySQL")
            End If
        ElseIf QuerySQL.ToString.Length = 0 Then
            If InAjax Then
                Bsp.Utility.ShowFormatMessageForAjax(Me.Page, "W_00100", "QuerySQL")
            Else
                Bsp.Utility.ShowFormatMessage(Me.Page, "W_00100", "QuerySQL")
            End If
        Else
            Dim strControlSessionID As String = "ButtonQuerySelect_" & Now.ToString("HHmmss") & Now.Millisecond.ToString("000")
            ViewState.Item("ControlSessionID") = strControlSessionID

            Dim aryParam(4) As Object
            aryParam(0) = QuerySQL
            aryParam(1) = ReturnColumnIndex
            aryParam(2) = Fields
            aryParam(3) = DefaultQueryField
            aryParam(4) = DefaultQueryValue

            Session(strControlSessionID) = aryParam
            Const CS_FUN As String = "{0}(document.forms[0].{1},document.forms[0].{2}, '{3}', '{4}', '{5}', '{6}');"
            Dim strText As String = String.Format(CS_FUN, "PageQuerySelectReturnValue", txtParam.ClientID, txtValueResult.ClientID, DataControlID, WindowWidth, WindowHeight, Me.ClientID)

            If InAjax Then
                Bsp.Utility.RunClientScriptForAjax(Me.Page, strText)
            Else
                Bsp.Utility.RunClientScript(Me.Page, strText)
            End If
        End If
    End Sub

    Protected Sub btnSelect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelect.Click, lbSelect.Click
        OpenSelect()
    End Sub
End Class
