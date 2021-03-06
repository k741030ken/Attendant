'****************************************************
'功能說明：片語維護
'建立人員：Chung
'建立日期：2013/01/29
'****************************************************
Imports System.Data.SqlClient
Imports System.Data
Imports System.Data.Common

Partial Class WF_WFA040
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                '若為系統管理人員，則視同維護通用片語
                If UserProfile.GroupID.Contains("S1") Then
                    ViewState.Item("PhraseUser") = ""
                    ViewState.Item("lblTitleM") = "通用片語維護"
                Else
                    ViewState.Item("PhraseUser") = UserProfile.UserID
                    ViewState.Item("lblTitleM") = "片語維護"
                End If
                'End If
                Dim objWF As New WF()

                Bsp.Utility.FillCommon(ddlFlowName, "003", Bsp.Enums.SelectCommonType.All)
                ddlFlowName.Items.Insert(0, New ListItem("----All----", ""))

                GetFlowStep()
                Using dt As DataTable = objWF.GetFlowPhrasebyUser(ViewState.Item("PhraseUser").ToString(), ddlFlowName.SelectedValue, ddlFlowStep.SelectedValue)
                    gvMain.DataSource = dt
                    gvMain.DataBind()
                End Using
            End If
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".PageLoad", ex)
        End Try
    End Sub

    Protected Overrides Sub BaseOnPageCall(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            If ti.Args.Length > 0 Then
                For intLoop As Integer = 0 To ti.Args.Length - 1
                    ViewState.Item(ti.Args(intLoop).ToString().Split("=")(0)) = ti.Args(intLoop).ToString().Split("=")(1)
                Next
            End If
            If ViewState.Item("PhraseFlag") = "Y" Then
                ViewState.Item("PhraseUser") = UserProfile.UserID
                ViewState.Item("lblTitleM") = "片語維護"
            Else
                ViewState.Item("PhraseUser") = ""
                ViewState.Item("lblTitleM") = "通用片語維護"
            End If
            Dim objWF As New WF()
            Bsp.Utility.FillCommon(ddlFlowName, "003", Bsp.Enums.SelectCommonType.All)
            ddlFlowName.Items.Insert(0, New ListItem("----All----", ""))

            Bsp.Utility.IniListWithValue(ddlFlowName, ViewState.Item("FlowID"))
            GetFlowStep()
            Bsp.Utility.IniListWithValue(ddlFlowStep, ViewState.Item("FlowStepID"))
            Using dt As DataTable = objWF.GetFlowPhrasebyUser(ViewState.Item("PhraseUser").ToString(), ddlFlowName.SelectedValue, ddlFlowStep.SelectedValue)
                gvMain.DataSource = dt
                gvMain.DataBind()
            End Using
        End If
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            If ti.Args IsNot Nothing Then
                If ti.Args.Length > 0 Then
                    For intLoop As Integer = 0 To ti.Args.Length - 1
                        ViewState.Item(ti.Args(intLoop).ToString().Split("=")(0)) = ti.Args(intLoop).ToString().Split("=")(1)
                    Next
                End If
                If ViewState.Item("PhraseFlag") = "Y" Then
                    ViewState.Item("PhraseUser") = UserProfile.UserID
                    ViewState.Item("lblTitleM") = "片語維護"
                Else
                    ViewState.Item("PhraseUser") = ""
                    ViewState.Item("lblTitleM") = "通用片語維護"
                End If
                Dim objWF As New WF()
                Bsp.Utility.FillCommon(ddlFlowName, "003", Bsp.Enums.SelectCommonType.All)
                ddlFlowName.Items.Insert(0, New ListItem("----All----", ""))

                Bsp.Utility.IniListWithValue(ddlFlowName, ViewState.Item("FlowID"))
                GetFlowStep()
                Bsp.Utility.IniListWithValue(ddlFlowStep, ViewState.Item("FlowStepID"))
                Using dt As DataTable = objWF.GetFlowPhrasebyUser(ViewState.Item("PhraseUser").ToString(), ddlFlowName.SelectedValue, ddlFlowStep.SelectedValue)
                    gvMain.DataSource = dt
                    gvMain.DataBind()
                End Using
            End If
        End If
    End Sub


    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnAdd"       '新增
                Dim btnA As New ButtonState(ButtonState.emButtonType.Add)
                Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
                btnA.Caption = "存檔返回"
                btnX.Caption = "返回"
                Me.TransferFramePage("~/WF/WFA041.aspx", New ButtonState() {btnA, btnX}, "PhraseUser=" & ViewState.Item("PhraseUser"), "EMode=INS", "PhraseFlag=" & ViewState.Item("PhraseFlag"), "FlowID=" & ddlFlowName.SelectedValue, "FlowStepID=" & ddlFlowStep.SelectedValue)
            Case "btnUpdate"    '修改
                DoUpdate()
            Case "btnQuery"     '查詢
                DoQuery()
            Case "btnActionC"    '
                DoComf()
            Case "btnActionX"    '離開

            Case Else
                DoOtherAction(Param)   '其他功能動作
        End Select
    End Sub

    Private Sub DoComf()

    End Sub

    Private Sub DoUpdate()

    End Sub

    Private Sub DoQuery()

    End Sub

    Private Sub DoDelete()

    End Sub


    Private Sub DoOtherAction(ByVal Param As String)
    End Sub

    Protected Sub gvMain_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvMain.RowDeleting
        Dim objWF As New WF

        Try
            objWF.Delete_FlowPhrase(gvMain.DataKeys(e.RowIndex)("FlowID").ToString, gvMain.DataKeys(e.RowIndex)("FlowStepID").ToString, gvMain.DataKeys(e.RowIndex)("SeqNo").ToString(), gvMain.DataKeys(e.RowIndex)("UserID").ToString())
            Using dt As DataTable = objWF.GetFlowPhrasebyUser(ViewState.Item("PhraseUser").ToString(), ddlFlowName.SelectedValue, ddlFlowStep.SelectedValue)
                gvMain.DataSource = dt
                gvMain.DataBind()
            End Using
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".RowDeleting", ex)
        End Try
    End Sub

    Protected Sub gvMain_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvMain.RowEditing
        Try
            Dim btnA As New ButtonState(ButtonState.emButtonType.Update)
            Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)

            btnA.Caption = "存檔返回"
            btnX.Caption = "返回"
            Me.TransferFramePage("~/WF/WFA041.aspx", New ButtonState() {btnA, btnX}, "FlowID=" & gvMain.DataKeys(e.NewEditIndex)("FlowID").ToString(), "FlowStepID=" & gvMain.DataKeys(e.NewEditIndex)("FlowStepID").ToString(), "PhraseUser=" & gvMain.DataKeys(e.NewEditIndex)("UserID").ToString(), "SeqNo=" & gvMain.DataKeys(e.NewEditIndex)("SeqNo").ToString(), "EMode=UPD", "PhraseFlag=" & ViewState.Item("PhraseFlag"))
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".RowEditing", ex)
        End Try
    End Sub

    Protected Sub ddlFlowName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFlowName.SelectedIndexChanged
        Dim objWF As New WF
        Using dt As DataTable = objWF.GetFlowPhrasebyUser(ViewState.Item("PhraseUser").ToString(), ddlFlowName.SelectedValue, ddlFlowStep.SelectedValue)
            gvMain.DataSource = dt
            gvMain.DataBind()
        End Using
        GetFlowStep()
    End Sub

    Protected Sub ddlFlowStep_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFlowStep.SelectedIndexChanged
        Dim objWF As New WF
        Using dt As DataTable = objWF.GetFlowPhrasebyUser(ViewState.Item("PhraseUser").ToString(), ddlFlowName.SelectedValue, ddlFlowStep.SelectedValue)
            gvMain.DataSource = dt
            gvMain.DataBind()
        End Using
    End Sub

    Private Sub GetFlowStep()
        If ddlFlowName.SelectedIndex <> 0 Then
            Dim objWF As New WF()
            Using dt As DataTable = objWF.GetFlowStepbyFlowID(ddlFlowName.SelectedValue)
                ddlFlowStep.Items.Clear()
                ddlFlowStep.DataTextField = "FlowStepDesc"
                ddlFlowStep.DataValueField = "FlowStepID"
                ddlFlowStep.DataSource = dt
                ddlFlowStep.DataBind()
                ddlFlowStep.Items.Insert(0, New ListItem("----All----", ""))
            End Using
        Else
            ddlFlowStep.Items.Clear()
        End If
    End Sub
End Class
