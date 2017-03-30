'****************************************************
'功能說明：時間選取
'建立人員：
'建立日期：2016.12.02
'****************************************************

Partial Class Component_ucTime
    Inherits System.Web.UI.UserControl

    '是否要選[秒] (預設false)
    Public Property ucIsSSEnabled() As Boolean
        Get
            If ViewState.Item("_IsSSEnabled") Is Nothing Then
                ViewState.Item("_IsSSEnabled") = False
            End If
            Return ViewState.Item("_IsSSEnabled")
        End Get
        Set(ByVal value As Boolean)
            ViewState.Item("_IsSSEnabled") = value
        End Set
    End Property

    '預設選取HH
    Public Property ucDefaultSelectedHH() As String
        Get
            Return CType(ddlHH.SelectedValue, String)
        End Get
        Set(ByVal value As String)
            ddlHH.SelectedValue = value
        End Set
    End Property

    '預設選取MM
    Public Property ucDefaultSelectedMM() As String
        Get
            Return CType(ddlMM.SelectedValue, String)
        End Get
        Set(ByVal value As String)
            ddlMM.SelectedValue = value
        End Set
    End Property

    '預設選取SS
    Public Property ucDefaultSelectedSS() As String
        Get
            Return CType(ddlSS.SelectedValue, String)
        End Get
        Set(ByVal value As String)
            ddlSS.SelectedValue = value
        End Set
    End Property

    '選取結果HH
    Public ReadOnly Property ucSelectedTime() As String
        Get
            Dim sResult As String = String.Format("{0}:{1}", ddlHH.SelectedValue, ddlMM.SelectedValue)
            If ucIsSSEnabled Then
                sResult += String.Format(":{0}", ddlSS.SelectedValue)
            End If
            Return CType(sResult, String)
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
       Refresh()
    End Sub
    Public Sub Refresh()
        Dim tmpValue As String
        Dim _HHList() As String = "00,01,02,03,04,05,06,07,08,09,10,11,12,13,14,15,16,17,18,19,20,21,22,23".Split(",")
        Dim _MMList() As String = "00,05,10,15,20,25,30,35,40,45,50,55".Split(",")
        Dim _SSList() As String = "00,30".Split(",")

        tmpValue = ddlHH.SelectedValue
        ddlHH.DataSource = _HHList
        ddlHH.DataBind()
        ddlHH.SelectedValue = tmpValue

        If String.IsNullOrEmpty(tmpValue) Then
            ddlHH.SelectedValue = ucDefaultSelectedHH
        End If

        tmpValue = ddlMM.SelectedValue
        ddlMM.DataSource = _MMList
        ddlMM.DataBind()
        ddlMM.SelectedValue = tmpValue

        If String.IsNullOrEmpty(tmpValue) Then
            ddlMM.SelectedValue = ucDefaultSelectedMM
        End If

        litSS.Visible = ucIsSSEnabled
        ddlSS.Visible = ucIsSSEnabled

        If ddlSS.Visible Then
            tmpValue = ddlSS.SelectedValue
            ddlSS.DataSource = _SSList
            ddlSS.DataBind()
            ddlSS.SelectedValue = tmpValue

            If String.IsNullOrEmpty(tmpValue) Then
                ddlSS.SelectedValue = ucDefaultSelectedMM
            End If
        End If

    End Sub
End Class
