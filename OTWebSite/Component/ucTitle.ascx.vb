'****************************************************
'功能說明：功能頁上的程式名稱ascx
'建立人員：Chung
'建立日期：2011.05.13
'****************************************************
Partial Class Component_ucTitle
    Inherits System.Web.UI.UserControl

    Private _SysID As String    '20140808 wei add
    Private _FunID As String
    Private _CaptionID As String

    '20140808 wei add SysID
    Public Property SysID() As String
        Get
            Return _SysID
        End Get
        Set(ByVal value As String)
            _SysID = value
        End Set
    End Property

    Public Property FunID() As String
        Get
            Return _FunID
        End Get
        Set(ByVal value As String)
            _FunID = value
        End Set
    End Property

    Public Property CaptionID() As String
        Get
            Return _CaptionID
        End Get
        Set(ByVal value As String)
            _CaptionID = value
        End Set
    End Property

    Public Property Caption() As String
        Get
            If ViewState.Item("Caption") Is Nothing Then ViewState.Item("Caption") = ""
            Return ViewState.Item("Caption")
        End Get
        Set(ByVal value As String)
            ViewState.Item("Caption") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        buildTitle()
    End Sub

    Private Sub buildTitle()
        Dim objUC As New UC()
        If CaptionID Is Nothing OrElse CaptionID = "" Then
            If Caption <> "" Then
                lblTitle.Text = Caption & "(" & FunID & ")"
                Bsp.Utility.runClientScript(Me.Page, "setTitle('" & lblTitle.Text & "');")
                Return
            End If
            CaptionID = FunID
        End If
        Dim strCaption As String

        strCaption = objUC.T_GetFunName(FunID, SysID)
        If strCaption = "" Then
            strCaption = objUC.T_GetFunName(CaptionID, SysID)
        End If
        lblTitle.Text = strCaption & "(" & FunID & ")"
        Bsp.Utility.runClientScript(Me.Page, "setTitle('" & lblTitle.Text & "');")
    End Sub
End Class
