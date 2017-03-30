'****************************************************
'功能說明：快速查詢
'建立人員：Ann
'建立日期：2014.08.20
'****************************************************

Partial Class Component_ucButtonQuerySelectOrgan
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

    '顯示有有授權的公司(True-有授權 False-所有公司)
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

    '是否顯示無效的資料(Y-顯示 or N-不顯示)
    Public Property InValidFlag() As String
        Get
            If ViewState.Item("InValidFlag") Is Nothing Then
                ViewState.Item("InValidFlag") = "N"
            End If
            Return ViewState.Item("InValidFlag")
        End Get
        Set(ByVal value As String)
            ViewState.Item("InValidFlag") = value
        End Set
    End Property

    '20150512 wei add 是否有指定選擇公司
    Public Property SelectCompID() As String
        Get
            If ViewState.Item("SelectCompID") Is Nothing Then
                ViewState.Item("SelectCompID") = ""
            End If
            Return ViewState.Item("SelectCompID")
        End Get
        Set(ByVal value As String)
            ViewState.Item("SelectCompID") = value
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

    Public Property Enabled() As Boolean
        Get
            Return btnSelect.Enabled
        End Get
        Set(ByVal value As Boolean)
            btnSelect.Enabled = value
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
        btnSelect.ToolTip = ButtonHint

        Dim strUrl As String = ResolveUrl("PageQuerySelectOrgan.aspx")
        strUrl = strUrl & "?ControlSessionID=" & ViewState.Item("ControlSessionID")
        txtParam.Text = "?FunID=QFind&Path=" & strUrl
    End Sub

    Public Sub OpenSelect()
        If Fields Is Nothing Then
            Bsp.Utility.ShowFormatMessage(Me.Page, "W_00100", "組織類型")
        ElseIf Fields.ToString.Length = 0 Then
            Bsp.Utility.ShowFormatMessage(Me.Page, "W_00100", "組織類型")
        Else
            Dim strControlSessionID As String = "ButtonQuerySelectOrgan_" & Now.ToString("HHmmss") & Now.Millisecond.ToString("000")
            ViewState.Item("ControlSessionID") = strControlSessionID

            Dim aryParam(3) As Object
            aryParam(0) = ShowCompRole
            aryParam(1) = InValidFlag
            aryParam(2) = SelectCompID
            aryParam(3) = Fields

            Session(strControlSessionID) = aryParam
            Const CS_FUN As String = "{0}(document.forms[0].{1},document.forms[0].{2}, '{3}', '{4}', '{5}', '{6}');"
            Dim strText As String = String.Format(CS_FUN, "PageQuerySelectReturnValue", txtParam.ClientID, txtValueResult.ClientID, DataControlID, WindowWidth, WindowHeight, Me.ClientID)

            Bsp.Utility.RunClientScript(Me.Page, strText)
        End If
    End Sub

    Protected Sub btnSelect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        OpenSelect()
    End Sub
End Class
