'****************************************************
'功能說明：SC_BatchRule維護
'建立人員：Chung
'建立日期：2013/05/09
'****************************************************
Imports System.Data

Partial Class SC_SC0330
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtDeptID.Attributes.Add("onkeypress", "EntertoSubmit();")
        txtOrganID.Attributes.Add("onkeypress", "EntertoSubmit();")
        txtWorkTypeID.Attributes.Add("onkeypress", "EntertoSubmit();")

        If StateMain IsNot Nothing Then
            sdsMain.SelectCommand = CType(StateMain, String)
        End If

        If Not IsPostBack() Then
            Page.SetFocus(txtDeptID)
        End If
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

        For Each strKey As String In ht.Keys
            Dim ctl As Control = Me.Form.FindControl(strKey)

            If ctl IsNot Nothing Then
                If TypeOf ctl Is TextBox Then
                    CType(ctl, TextBox).Text = ht(strKey)
                End If
            End If
        Next
        DoQuery()
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnAdd"       '新增
                DoAdd()
            Case "btnUpdate"    '修改
                DoUpdate()
            Case "btnQuery"     '查詢
                DoQuery()
            Case "btnDelete"    '刪除
                DoDelete()
            Case Else
                DoOtherAction()   '其他功能動作
        End Select
    End Sub

    Private Sub DoAdd()
        Dim btnA As New ButtonState(ButtonState.emButtonType.Add)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)

        btnA.Caption = "存檔返回"
        btnX.Caption = "返回"

        Me.TransferFramePage("~/SC/SC0331.aspx", New ButtonState() {btnA, btnX}, _
            txtDeptID.ID & "=" & txtDeptID.Text, _
            txtOrganID.ID & "=" & txtOrganID.Text, _
            txtWorkTypeID.ID & "=" & txtWorkTypeID.Text)
    End Sub

    Private Sub DoUpdate()
        Dim btnU As New ButtonState(ButtonState.emButtonType.Update)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
        Dim intRow As Integer = selectedRow(gvMain)

        btnU.Caption = "存檔返回"
        btnX.Caption = "返回"

        Me.TransferFramePage("~/SC/SC0332.aspx", New ButtonState() {btnU, btnX}, _
            txtDeptID.ID & "=" & txtDeptID.Text, _
            txtOrganID.ID & "=" & txtOrganID.Text, _
            txtWorkTypeID.ID & "=" & txtWorkTypeID.Text, _
            "DeptID=" & gvMain.DataKeys(intRow)("DeptID").ToString(), _
            "OrganID=" & gvMain.DataKeys(intRow)("OrganID").ToString(), _
            "WorkTypeID=" & gvMain.DataKeys(intRow)("WorkTypeID").ToString(), _
            "UpdateSeq=" & gvMain.DataKeys(intRow)("UpdateSeq").ToString())
    End Sub

    Private Sub DoQuery()
        Dim objSC As New SC

        StateMain = objSC.GetBatchRuleQueryString("DeptID=" & txtDeptID.Text, _
                        "OrganID=" & txtOrganID.Text, _
                        "WorkTypeID=" & txtWorkTypeID.Text)

        sdsMain.SelectCommand = StateMain
        sdsMain.DataBind()
    End Sub

    Private Sub DoDelete()
        Dim beBatchRule As New beSC_BatchRule.Row()
        Dim intRow As Integer = selectedRow(gvMain)
        Dim objSC As New SC

        beBatchRule.DeptID.Value = gvMain.DataKeys(intRow)("DeptID").ToString()
        beBatchRule.OrganID.Value = gvMain.DataKeys(intRow)("OrganID").ToString()
        beBatchRule.WorkTypeID.Value = gvMain.DataKeys(intRow)("WorkTypeID").ToString()
        beBatchRule.UpdateSeq.Value = gvMain.DataKeys(intRow)("UpdateSeq")

        Try
            objSC.DeleteBatchRule(beBatchRule)
            gvMain.DataBind()
            Bsp.Utility.ShowMessage(Me, "删除成功！", True)
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoDelete", ex)
        End Try
    End Sub

    Private Sub DoOtherAction()

    End Sub

End Class
