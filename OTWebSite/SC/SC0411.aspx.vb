'****************************************************
'功能說明：流程關卡設定檔-新增
'建立人員：Chung
'建立日期：2013/01/29
'****************************************************
Imports System.Data

Partial Class SC_SC0411
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            subInit()
        End If
    End Sub

    Private Sub subInit()
        Bsp.Utility.FillCommon(ddlFlowID, "003", Bsp.Enums.SelectCommonType.Valid)

        txtFlowVer.Text = "1"

        Dim dtBtn As New DataTable()

        dtBtn.Columns.Add(New DataColumn("SeqNo", System.Type.GetType("System.Int32")))
        dtBtn.Columns.Add(New DataColumn("ButtonName", System.Type.GetType("System.String")))
        dtBtn.Columns.Add(New DataColumn("RequireOpinion", System.Type.GetType("System.String")))
        dtBtn.Columns.Add(New DataColumn("NextStepID", System.Type.GetType("System.String")))
        dtBtn.Columns.Add(New DataColumn("SendMail", System.Type.GetType("System.String")))
        dtBtn.Columns.Add(New DataColumn("DefaultUserGroup", System.Type.GetType("System.String")))
        dtBtn.Columns.Add(New DataColumn("DefaultUserGroupNm", System.Type.GetType("System.String")))
        dtBtn.Columns.Add(New DataColumn("DefaultUserGroupEx", System.Type.GetType("System.String")))
        dtBtn.Columns.Add(New DataColumn("CloseFlag", System.Type.GetType("System.String")))
        dtBtn.Columns.Add(New DataColumn("AgreeFlag", System.Type.GetType("System.String")))
        dtBtn.PrimaryKey = New DataColumn() {dtBtn.Columns("SeqNo")}

        ViewState.Item("dtBtn") = dtBtn
        ViewState.Item("MaxSeq") = "0"

    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnAdd"
                If funCheckData() Then
                    If SaveData() Then
                        GoBack()
                    End If
                End If
            Case "btnActionX"
                GoBack()
        End Select
    End Sub

    Private Sub GoBack()
        Dim ti As TransferInfo = Me.StateTransfer
        Me.TransferFramePage(ti.CallerUrl, Nothing, ti.Args)
    End Sub

    Private Function SaveData() As Boolean
        Dim beFlowStepM As New beWF_FlowStepM.Row()
        Dim bsFlowStepM As New beWF_FlowStepM.Service()
        Dim beFlowStepD() As beWF_FlowStepD.Row = Nothing
        Dim objSC As New SC()
        Dim intCount As Integer = 0

        beFlowStepM.FlowID.Value = ddlFlowID.SelectedValue
        beFlowStepM.FlowVer.Value = Convert.ToInt32(txtFlowVer.Text)
        beFlowStepM.FlowStepID.Value = txtFlowStepID.Text
        beFlowStepM.Description.Value = txtDescription.Text
        beFlowStepM.MenuTitle.Value = txtMenuTitle.Text
        beFlowStepM.ShowModeMenuTitle.Value = txtShowModeMenuTitle.Text
        beFlowStepM.DefaultPage.Value = txtDefaultPage.Text
        beFlowStepM.ShowModePage.Value = txtShowModePage.Text
        beFlowStepM.ProcDay.Value = Convert.ToInt32(txtProcDay.Text)
        beFlowStepM.Intimation.Value = txtIntimation.Text
        beFlowStepM.AgreeRate.Value = Convert.ToDecimal(txtAgreeRate.Text)
        beFlowStepM.LastChgID.Value = UserProfile.ActUserID
        beFlowStepM.LastChgDate.Value = Now

        Using dt As DataTable = CType(ViewState.Item("dtBtn"), DataTable)
            ReDim beFlowStepD(dt.Rows.Count - 1)
            For Each dr As DataRow In dt.Rows
                Dim beFSD As New beWF_FlowStepD.Row()

                With beFSD
                    .FlowID.Value = ddlFlowID.SelectedValue
                    .FlowVer.Value = Bsp.Utility.IsDecimalNull(txtFlowVer.Text, 1)
                    .FlowStepID.Value = txtFlowStepID.Text

                    For Each dc As DataColumn In dt.Columns
                        If dc.ColumnName <> "DefaultUserGroupNm" Then
                            .Row(dc.ColumnName) = dr.Item(dc.ColumnName)
                        End If
                    Next
                End With
                beFlowStepD(intCount) = beFSD
                intCount += 1
            Next
        End Using

        If bsFlowStepM.IsDataExists(beFlowStepM) Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00010", "")
            Return False
        End If

        Try
            objSC.AddFlowStep(beFlowStepM, beFlowStepD)
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".SaveData", ex)
            Return False
        End Try

        Return True
    End Function

    Private Function funCheckData() As Boolean
        Dim strValue As String

        strValue = txtFlowVer.Text.ToString().Trim()
        If strValue = "" Then strValue = "1"
        If Not IsNumeric(strValue) Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00040", lblFlowVer.Text, txtFlowVer.MaxLength.ToString())
            txtFlowVer.Focus()
            Return False
        End If
        txtFlowVer.Text = strValue

        strValue = txtFlowStepID.Text.ToString().ToUpper().Trim()
        If strValue = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblFlowStepID.Text)
            txtFlowStepID.Focus()
            Return False
        Else
            If strValue.Length > txtFlowStepID.MaxLength Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00040", lblFlowStepID.Text, txtFlowStepID.MaxLength.ToString())
                txtFlowStepID.Focus()
                Return False
            End If
            txtFlowStepID.Text = strValue
        End If

        strValue = txtDescription.Text.ToString().Trim()
        If strValue = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblDescription.Text)
            txtDescription.Focus()
            Return False
        Else
            If strValue.Length > txtDescription.MaxLength Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00040", lblDescription.Text, txtDescription.MaxLength.ToString())
                txtDescription.Focus()
                Return False
            End If
            txtDescription.Text = strValue
        End If

        strValue = txtMenuTitle.Text.ToString().Trim()
        If strValue = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblMenuTitle.Text)
            txtMenuTitle.Focus()
            Return False
        Else
            If strValue.Length > txtMenuTitle.MaxLength Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00040", lblMenuTitle.Text, txtMenuTitle.MaxLength.ToString())
                txtMenuTitle.Focus()
                Return False
            End If
            txtMenuTitle.Text = strValue
        End If

        strValue = txtShowModeMenuTitle.Text.ToString().Trim()
        'If strValue = "" Then
        '    Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblShowModeMenuTitle.Text)
        '    txtShowModeMenuTitle.Focus()
        '    Return False
        'Else
        If strValue.Length > txtShowModeMenuTitle.MaxLength Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00040", lblShowModeMenuTitle.Text, txtShowModeMenuTitle.MaxLength.ToString())
            txtShowModeMenuTitle.Focus()
            Return False
        End If
        txtShowModeMenuTitle.Text = strValue
        'End If

        strValue = txtDefaultPage.Text.ToString().Trim()
        'If strValue = "" Then
        '    Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblDefaultPage.Text)
        '    txtDefaultPage.Focus()
        '    Return False
        'Else
        If strValue.Length > txtDefaultPage.MaxLength Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00040", lblDefaultPage.Text, txtDefaultPage.MaxLength.ToString())
            txtDefaultPage.Focus()
            Return False
        End If
        txtDefaultPage.Text = strValue
        'End If

        strValue = txtShowModePage.Text.ToString().Trim()
        'If strValue = "" Then
        '    Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblShowModePage.Text)
        '    txtShowModePage.Focus()
        '    Return False
        'Else
        If strValue.Length > txtShowModePage.MaxLength Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00040", lblShowModePage.Text, txtShowModePage.MaxLength.ToString())
            txtShowModePage.Focus()
            Return False
        End If
        txtShowModePage.Text = strValue
        'End If

        strValue = txtProcDay.Text.ToString().Trim()
        If strValue = "" Then strValue = "0"
        If Not IsNumeric(strValue) Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00050", lblProcDay.Text)
            txtProcDay.Focus()
            Return False
        End If
        txtProcDay.Text = strValue

        strValue = txtIntimation.Text.ToString().Trim()
        If Bsp.Utility.getStringLength(strValue) > txtIntimation.MaxLength Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00040", lblIntimation.Text, txtIntimation.MaxLength.ToString())
            txtIntimation.Focus()
            Return False
        End If
        txtIntimation.Text = strValue

        strValue = txtAgreeRate.Text.Trim()
        If strValue = "" Then strValue = "0"
        If Not IsNumeric(strValue) Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00050", lblAgreeRate.Text)
            txtAgreeRate.Focus()
            Return False
        Else
            If Convert.ToDecimal(strValue) > 1 OrElse Convert.ToDecimal(strValue) < 0 Then
                Bsp.Utility.ShowFormatMessage(Me, "W_A3100")
                txtAgreeRate.Focus()
                Return False
            End If
        End If
        txtAgreeRate.Text = strValue

        Return True
    End Function

    Protected Sub lbAddButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAddButton.Click
        Dim dt As DataTable = CType(ViewState.Item("dtBtn"), DataTable)

        'dt.Rows.Add(dt.NewRow())
        Dim dr As DataRow = dt.NewRow()
        dr.Item("SeqNo") = Convert.ToInt32(ViewState.Item("MaxSeq")) + 1
        dr.Item("RequireOpinion") = ""
        dr.Item("SendMail") = ""
        dr.Item("CloseFlag") = ""
        dr.Item("AgreeFlag") = ""
        dt.Rows.Add(dr)

        gvMain.EditIndex = dt.Rows.Count - 1
        bindgvMain(dt)

        ViewState.Item("dtBtn") = dt
        ViewState.Item("Action") = "New"
        lbAddButton.Enabled = False
    End Sub

    Protected Sub gvMain_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvMain.RowCancelingEdit
        Dim strKey As String = gvMain.DataKeys(e.RowIndex)("SeqNo")
        Dim dt As DataTable = CType(ViewState.Item("dtBtn"), DataTable)

        If ViewState.Item("Action") = "New" Then
            Dim drs() As DataRow = dt.Select("SeqNo=" & strKey)

            For Each dr As DataRow In drs
                dt.Rows.Remove(dr)
            Next
        End If
        gvMain.EditIndex = -1
        bindgvMain(dt)
        ViewState.Item("dtBtn") = dt
        ViewState.Item("Action") = ""
        lbAddButton.Enabled = True
    End Sub

    Private Sub MoveDataRow(ByVal MoveDirection As String, ByVal rowIndex As Integer)
        Dim intMoveTo As Integer = 0

        If MoveDirection = "Up" Then
            If rowIndex = 0 Then Return
            intMoveTo = rowIndex - 1
        Else
            If gvMain.DataKeys(rowIndex)("SeqNo") = ViewState.Item("MaxSeq") Then Return
            intMoveTo = rowIndex + 1
        End If
        Dim dt As DataTable = CType(ViewState.Item("dtBtn"), DataTable)
        Dim drCurrent As DataRow = dt.Select("SeqNo=" & gvMain.DataKeys(rowIndex)("SeqNo"))(0)
        Dim drMoveTo As DataRow = dt.Select("SeqNo=" & gvMain.DataKeys(intMoveTo)("SeqNo"))(0)

        Dim intCurrentSeq As Integer = drCurrent.Item("SeqNo")
        Dim intMoveToSeq As Integer = drMoveTo.Item("SeqNo")

        '先default給定一個Seq
        drMoveTo.Item("SeqNo") = 999
        drCurrent.Item("SeqNo") = intMoveToSeq
        drMoveTo.Item("SeqNo") = intCurrentSeq

        bindgvMain(dt)
    End Sub

    Protected Sub gvMain_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMain.RowCommand
        Select Case e.CommandName
            Case "Up", "Down"  '上移
                MoveDataRow(e.CommandName, e.CommandArgument)
        End Select
    End Sub

    Protected Sub gvMain_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvMain.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim objUp As LinkButton = e.Row.FindControl("lbUp")
            Dim objDown As LinkButton = e.Row.FindControl("lbDown")

            If objUp IsNot Nothing Then objUp.CommandArgument = e.Row.RowIndex
            If objDown IsNot Nothing Then objDown.CommandArgument = e.Row.RowIndex
        End If
    End Sub

    Protected Sub gvMain_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvMain.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If gvMain.EditIndex >= 0 Then
                If gvMain.EditIndex <> e.Row.RowIndex Then
                    Dim objUpdate As LinkButton = e.Row.FindControl("lbUpdate")
                    Dim objDelete As LinkButton = e.Row.FindControl("lbDelete")

                    If objUpdate IsNot Nothing Then objUpdate.Enabled = False
                    If objDelete IsNot Nothing Then objDelete.Enabled = False
                End If
                Dim objUp As LinkButton = e.Row.FindControl("lbUp")
                Dim objDown As LinkButton = e.Row.FindControl("lbDown")

                If objUp IsNot Nothing Then objUp.Visible = False
                If objDown IsNot Nothing Then objDown.Visible = False
                If gvMain.EditIndex = e.Row.RowIndex Then
                    Dim objDefaultUserGroup As DropDownList = e.Row.FindControl("ddlDefaultUserGroup")

                    If objDefaultUserGroup IsNot Nothing Then
                        Bsp.Utility.FillCommon(objDefaultUserGroup, "004", Bsp.Enums.SelectCommonType.Valid)
                        If Bsp.Utility.IsStringNull(DataBinder.Eval(e.Row.DataItem, "DefaultUserGroup")) <> "" Then
                            Bsp.Utility.IniListWithValue(objDefaultUserGroup, DataBinder.Eval(e.Row.DataItem, "DefaultUserGroup"))
                        Else
                            objDefaultUserGroup.SelectedIndex = 0
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Protected Sub gvMain_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvMain.RowDeleting
        Dim strSeq As String = gvMain.DataKeys(e.RowIndex)("SeqNo")
        Dim dt As DataTable = CType(ViewState.Item("dtBtn"), DataTable)

        Dim drs() As DataRow = dt.Select("SeqNo=" & strSeq)
        If drs.Length > 0 Then
            dt.Rows.Remove(drs(0))
            bindgvMain(dt)
            ViewState.Item("dtBtn") = dt
        End If
    End Sub

    Protected Sub gvMain_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvMain.RowEditing
        gvMain.EditIndex = e.NewEditIndex
        bindgvMain(CType(ViewState.Item("dtBtn"), DataTable))
        ViewState.Item("Action") = "Edit"
    End Sub

    Protected Sub gvMain_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvMain.RowUpdating
        Dim strValue As String
        Dim objButtonName As TextBox = gvMain.Rows(e.RowIndex).FindControl("txtButtonName")
        Dim objRequireOpinion As CheckBox = gvMain.Rows(e.RowIndex).FindControl("chkRequireOpinion")
        Dim objNextStepID As TextBox = gvMain.Rows(e.RowIndex).FindControl("txtNextStepID")
        Dim objSendMail As CheckBox = gvMain.Rows(e.RowIndex).FindControl("chkSendMail")
        Dim objDefaultUserGroup As DropDownList = gvMain.Rows(e.RowIndex).FindControl("ddlDefaultUserGroup")
        Dim objDefaultUserGroupEx As TextBox = gvMain.Rows(e.RowIndex).FindControl("txtDefaultUserGroupEx")
        Dim objCloseFlag As CheckBox = gvMain.Rows(e.RowIndex).FindControl("chkCloseFlag")
        Dim objAgreeFlag As CheckBox = gvMain.Rows(e.RowIndex).FindControl("chkAgreeFlag")

        strValue = objButtonName.Text.Trim()
        If strValue = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "功能按鍵")
            e.Cancel = True
            Return
        Else
            If strValue.Length > objButtonName.MaxLength Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00040", "功能按鍵", objButtonName.MaxLength)
                e.Cancel = True
                Return
            End If
        End If
        objButtonName.Text = strValue

        strValue = objNextStepID.Text.Trim()
        If strValue = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "指向關卡")
            e.Cancel = True
            Return
        Else
            If Bsp.Utility.getStringLength(strValue) > objNextStepID.MaxLength Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00040", "指向關卡", objNextStepID.MaxLength)
                e.Cancel = True
                Return
            End If
        End If
        objNextStepID.Text = strValue

        Dim dt As DataTable = CType(ViewState.Item("dtBtn"), DataTable)
        Dim strKey As String = gvMain.DataKeys(e.RowIndex)("SeqNo").ToString()
        Dim drs() As DataRow = dt.Select("SeqNo=" & strKey)

        If drs.Length > 0 Then
            dt.Rows.Remove(drs(0))

            drs(0).Item("SeqNo") = strKey
            drs(0).Item("ButtonName") = objButtonName.Text
            drs(0).Item("RequireOpinion") = IIf(objRequireOpinion.Checked, "Y", "N")
            drs(0).Item("NextStepID") = objNextStepID.Text
            drs(0).Item("SendMail") = IIf(objSendMail.Checked, "Y", "N")
            If objDefaultUserGroup.Items.Count > 0 Then drs(0).Item("DefaultUserGroup") = objDefaultUserGroup.SelectedValue
            drs(0).Item("DefaultUserGroupNm") = objDefaultUserGroup.SelectedItem.Text
            drs(0).Item("DefaultUserGroupEx") = objDefaultUserGroupEx.Text
            drs(0).Item("CloseFlag") = IIf(objCloseFlag.Checked, "Y", "N")
            drs(0).Item("AgreeFlag") = IIf(objAgreeFlag.Checked, "Y", "N")

            dt.Rows.Add(drs(0))
        End If

        If ViewState.Item("Action") = "New" Then
            ViewState.Item("MaxSeq") = strKey
        End If
        gvMain.EditIndex = -1
        bindgvMain(dt)
        ViewState.Item("dtBtn") = dt
        ViewState.Item("Action") = ""

        lbAddButton.Enabled = True
    End Sub

    Private Sub bindgvMain(ByVal dt As DataTable)
        Dim dv As DataView = dt.DefaultView
        dv.Sort = "SeqNo"
        gvMain.DataSource = dv
        gvMain.DataBind()
    End Sub
End Class
