'****************************************************
'功能說明：取得職稱職等
'建立人員：Micky Sung
'建立日期：2015.06.12
'****************************************************

Partial Class Component_ucSelectRankAndTitle
    Inherits System.Web.UI.UserControl

    'DisplayType
    Public Enum emDisplayType
        OnlyName    '只顯示名字
        OnlyID      '顯示ID  
        Full        '顯示ID + 名字
    End Enum

    '顯示內容
    Public Property DisplayType() As emDisplayType
        Get
            If ViewState.Item("_DisplayType") Is Nothing Then
                ViewState.Item("_DisplayType") = emDisplayType.Full
            End If
            Return ViewState.Item("_DisplayType")
        End Get
        Set(ByVal value As emDisplayType)
            ViewState.Item("_DisplayType") = value
        End Set
    End Property

    '選取職等代號
    Public ReadOnly Property SelectedRankID() As String
        Get
            If ddlRankID.Visible = True Then
                Return ddlRankID.SelectedValue
            Else
                Return lblRankID.Text
            End If
            Return ""
        End Get
    End Property

    '由外部來設定職等代號，若傳入為空值，則不處理
    Public Sub setRankID(ByVal strCompID As String, ByVal strRankID As String, ByVal strActionID As String)
        If strRankID <> "" Then
            'subLoadRankIDData(strCompID, strActionID)
            ddlRankID.Items.Clear()
            Bsp.Utility.Rank(ddlRankID, strCompID)
            ddlRankID.Items.Insert(0, New ListItem("---請選擇---", ""))
            Bsp.Utility.SetSelectedIndex(ddlRankID, strRankID)

            '下拉選單內找不到該RankID
            If ddlRankID.SelectedIndex = 0 Then
                ddlRankID.Visible = False
                lblRankID.Visible = True
                lblRankID.Text = strRankID
            Else
                ddlRankID.Visible = True
                lblRankID.Visible = False
            End If
            'subLoadTitleIDData(ViewState.Item("strCompID"), ddlRankID.SelectedValue, ViewState.Item("ActionID")) '20150717
        End If
    End Sub

    '選取職稱ID
    Public ReadOnly Property SelectedTitleID() As String
        Get
            If ddlTitleID.Visible = True Then
                Return ddlTitleID.SelectedValue
            Else
                Return lblTitleName.Text
            End If
            Return ""
        End Get
    End Property

    '選取職稱ID
    Public ReadOnly Property SelectedTitleName() As String
        Get
            If ddlTitleID.Visible = True Then
                If ddlTitleID.SelectedValue <> "" Then
                    Dim count As Integer = ddlTitleID.SelectedValue.Length
                    Dim titleNameLength As Integer = ddlTitleID.SelectedItem.Text.Length
                    Dim titleName As String = Right(ddlTitleID.SelectedItem.Text, titleNameLength - count)
                    Return titleName
                End If
            Else
                Return lblTitleName.Text
            End If
            Return ""
        End Get
    End Property

    '由外部來設定職稱代號，若傳入為空值，則不處理
    Public Sub setTitleID(ByVal strCompID As String, ByVal strRankID As String, ByVal strTitleID As String, ByVal strActionID As String)
        If strTitleID <> "" Then
            If strActionID = "A" Then
                ddlTitleID.Visible = True
                lblTitleName.Visible = False
                'If ddlTitleID.Items.Count = 0 Then
                subLoadTitleIDData(strCompID, strRankID, strActionID)
                'End If
                Bsp.Utility.SetSelectedIndex(ddlTitleID, strTitleID)
            ElseIf strActionID = "U" Then
                If ddlRankID.SelectedIndex = 0 Then
                    ddlTitleID.Visible = False
                    lblTitleName.Visible = True
                    lblTitleName.Text = strTitleID
                Else
                    ddlTitleID.Visible = True
                    lblTitleName.Visible = False
                    'If ddlTitleID.Items.Count = 0 Then
                    subLoadTitleIDData(strCompID, strRankID, strActionID)
                    'End If
                    Bsp.Utility.SetSelectedIndex(ddlTitleID, strTitleID)
                End If
            End If
        End If
    End Sub

    'Private Sub subLoadRankIDData(ByVal strCompID As String, ByVal strActionID As String)
    '    Dim objHR As New HR

    '    Try
    '        Using dt As Data.DataTable = objHR.GetRankIDInfo(strCompID)
    '            With ddlRankID
    '                .DataSource = dt
    '                .DataTextField = "RankID"
    '                .DataValueField = "RankID"
    '                .DataBind()
    '                .Items.Insert(0, New ListItem("---請選擇---", ""))
    '            End With
    '        End Using

    '    Catch ex As Exception
    '        Bsp.Utility.ShowMessage(Me.Parent, "ucSelectRankAndTitle.subLoadDeptData", ex)
    '        Throw
    '        Return
    '    End Try

    'End Sub

    Private Sub subLoadTitleIDData(ByVal strCompID As String, ByVal strRankID As String, ByVal strActionID As String)
        Dim objHR As New HR

        Try
            Using dt As Data.DataTable = objHR.GetTitleInfo(ddlRankID.SelectedValue, "TitleID, TitleName, TitleID + TitleName as FullName", "And CompID=" & Bsp.Utility.Quote(strCompID))
                With ddlTitleID
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "FullName"
                    .DataValueField = "TitleID"
                    .DataBind()
                    .Items.Insert(0, New ListItem("---請選擇---", ""))
                End With
            End Using

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me.Parent, "ucSelectRankAndTitle.subLoadTitleIDData", ex)
            Throw
            Return
        End Try

    End Sub

    Public Sub LoadData(Optional ByVal strCompID As String = "", Optional ByVal strActionID As String = "")
        ViewState.Item("strCompID") = strCompID
        ViewState.Item("strActionID") = strActionID
        'subLoadRankIDData(strCompID, strActionID)

        Bsp.Utility.Rank(ddlRankID, strCompID)
        ddlRankID.Items.Insert(0, New ListItem("---請選擇---", ""))
        If strActionID = "A" Then
            ddlRankID.Visible = True
            ddlTitleID.Visible = True
            lblRankID.Visible = False
            lblTitleName.Visible = False

            ddlTitleID.Items.Clear()
            ddlTitleID.Items.Insert(0, New ListItem("---請先選擇職等---", ""))
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim ActionID As String = ViewState.Item("strActionID")
        'If ActionID = "A" Then
        '    lblRankID.Visible = False
        '    lblTitleName.Visible = False

        '    'If Not IsPostBack() Then
        '    '    ddlTitleID.Items.Insert(0, New ListItem("---請先選擇職等---", ""))
        '    'End If
        'End If
    End Sub

    Protected Sub ddlRankID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRankID.SelectedIndexChanged
        subLoadTitleIDData(ViewState.Item("strCompID"), ddlRankID.SelectedValue, ViewState.Item("ActionID"))
        RankIDSelectedIndexChanged(e)
    End Sub

    Public Delegate Sub TitleIDSelectedIndexChangedHandler(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event ucSelectTitleIDSelectedIndexChangedHandler_SelectChange As TitleIDSelectedIndexChangedHandler

    Protected Overridable Sub TitleIDSelectedIndexChanged(ByVal e As System.EventArgs)
        RaiseEvent ucSelectTitleIDSelectedIndexChangedHandler_SelectChange(Me, e)
    End Sub

    Protected Sub ddlTitleID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTitleID.SelectedIndexChanged
        TitleIDSelectedIndexChanged(e)
    End Sub

    Public Delegate Sub RankIDSelectedIndexChangedHandler(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event ucSelectRankIDSelectedIndexChangedHandler_SelectChange As RankIDSelectedIndexChangedHandler

    Protected Overridable Sub RankIDSelectedIndexChanged(ByVal e As System.EventArgs)
        RaiseEvent ucSelectRankIDSelectedIndexChangedHandler_SelectChange(Me, e)
    End Sub

End Class
