'****************************************************
'功能說明：員工中斷年資維護-搜尋
'建立人員：John Lin
'建立日期：2016.01.20
'****************************************************
Imports System.Data

Partial Class ST_ST1900
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnAdd"       '新增
                DoAdd()
            Case "btnUpdate"    '修改
                DoUpdate()
            Case "btnDelete"    '刪除
                DoDelete()
        End Select
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim objST As New ST1
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

            If ht.ContainsKey("SelectedCompID") Then
                ViewState.Item("CompID") = ht("SelectedCompID").ToString()
                ViewState.Item("CompName") = ht("SelectedCompName").ToString()
                ViewState.Item("EmpID") = ht("SelectedEmpID").ToString()
                ViewState.Item("EmpName") = ht("SelectedEmpName").ToString()
                ViewState.Item("IDNo") = ht("SelectedIDNo").ToString()

                lblCompRoleID.Text = ViewState.Item("CompID").ToString + "-" + ViewState.Item("CompName")
                txtEmpID.Text = ViewState.Item("EmpID").ToString
                txtEmpName.Text = ViewState.Item("EmpName").ToString

                Using dt As DataTable = objST.QueryEmpDate(ViewState.Item("CompID"), ViewState.Item("EmpID"))
                    txtEmpDate.Text = dt.Rows(0).Item("EmpDate").ToString
                    txtQuitDate.Text = dt.Rows(0).Item("QuitDate").ToString
                    txtSinopacEmpDate.Text = dt.Rows(0).Item("SinopacEmpDate").ToString
                    txtSinopacQuitDate.Text = dt.Rows(0).Item("SinopacQuitDate").ToString
                End Using
            Else
                Return
            End If
            DoQuery()
        End If
    End Sub

    Private Sub DoAdd()
        Dim btnA As New ButtonState(ButtonState.emButtonType.Add)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
        Dim btnC As New ButtonState(ButtonState.emButtonType.Cancel)

        btnA.Caption = "存檔返回"
        btnX.Caption = "返回"
        btnC.Caption = "清除"

        Me.TransferFramePage("~/ST/ST1901.aspx", New ButtonState() {btnA, btnX, btnC}, _
            "SelectedCompID=" & ViewState.Item("CompID"), _
            "SelectedCompName=" & ViewState.Item("CompName"), _
            "SelectedEmpID=" & ViewState.Item("EmpID"), _
            "SelectedEmpName=" & ViewState.Item("EmpName"), _
            "SelectedIDNo=" & ViewState.Item("IDNo"), _
            "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
    End Sub

    Private Sub DoUpdate()
        Dim btnU As New ButtonState(ButtonState.emButtonType.Update)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
        Dim btnC As New ButtonState(ButtonState.emButtonType.Cancel)

        btnU.Caption = "存檔返回"
        btnX.Caption = "返回"
        btnC.Caption = "清除"

        Me.TransferFramePage("~/ST/ST1902.aspx", New ButtonState() {btnU, btnX, btnC}, _
            "SelectedCompID=" & ViewState.Item("CompID"), _
            "SelectedCompName=" & ViewState.Item("CompName"), _
            "SelectedEmpID=" & ViewState.Item("EmpID"), _
            "SelectedEmpName=" & ViewState.Item("EmpName"), _
            "SelectedIDNo=" & ViewState.Item("IDNo"), _
            "CompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
            "EmpID=" & gvMain.DataKeys(selectedRow(gvMain))("EmpID").ToString(), _
            "Seq=" & gvMain.DataKeys(selectedRow(gvMain))("Seq").ToString(), _
            "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
    End Sub

    Private Sub DoQuery()
        Dim objST As New ST1

        Try
            If UserProfile.SelectCompRoleID = "ALL" Then
                Using dt As DataTable = objST.QueryNotEmpLogInfo(ViewState.Item("IDNo"))

                    If dt.Rows.Count > 0 Then
                        tbCompEmpSen.Visible = False

                        txtTotSen_SPHOLD.Text = dt.Rows(0).Item("TotSen_SPHOLD").ToString
                        txtDays_SPHOLD.Text = dt.Rows(0).Item("Days_SPHOLD").ToString

                        tbCompSen.Width = "50%"
                    End If
                End Using

                pcMain.DataTable = objST.QueryNotEmpLog(
                    "IDNo=" & ViewState.Item("IDNo"))

            Else
                Using dt As DataTable = objST.QueryNotEmpLogInfo(
                    "CompID=" & ViewState.Item("CompID"), _
                    "EmpID=" & txtEmpID.Text)

                    If dt.Rows.Count > 0 Then
                        txtTotSen.Text = dt.Rows(0).Item("TotSen").ToString
                        txtTotDays.Text = dt.Rows(0).Item("TotDays").ToString

                        txtNotEmpDay.Text = dt.Rows(0).Item("NotEmpDay").ToString
                        txtNotEmpSen.Text = (Double.Parse(txtNotEmpDay.Text) / 365).ToString("f2")

                        txtTotSen_SPHOLD.Text = dt.Rows(0).Item("TotSen_SPHOLD").ToString
                        txtDays_SPHOLD.Text = dt.Rows(0).Item("Days_SPHOLD").ToString
                    End If
                End Using

                pcMain.DataTable = objST.QueryNotEmpLog(
                    "CompID=" & ViewState.Item("CompID"), _
                    "EmpID=" & txtEmpID.Text)
            End If

            Using dt As DataTable = objST.QueryCompEmpSen(ViewState.Item("IDNo"))
                If dt.Rows.Count > 0 Then
                    txtCompName.Text = dt.Rows(0).Item("CompName").ToString
                    txtCompSen.Text = dt.Rows(0).Item("TotSen").ToString
                    txtCompDays.Text = dt.Rows(0).Item("TotDays").ToString

                    For i As Integer = 1 To dt.Rows.Count - 1
                        Dim tr As New HtmlTableRow()
                        tr.Height = "20px"

                        Dim td1 As New HtmlTableCell()
                        td1.InnerText = dt.Rows(i).Item("CompName").ToString + " 年資 "
                        td1.Attributes.Add("class", "td_Edit")
                        td1.Align = "left"

                        tr.Cells.Add(td1)

                        Dim td2 As New HtmlTableCell()
                        td2.InnerText = dt.Rows(i).Item("TotSen").ToString + " 年 / " + dt.Rows(i).Item("TotDays").ToString + " 天"
                        td2.Attributes.Add("class", "td_Edit")
                        td2.Align = "center"

                        tr.Cells.Add(td2)

                        tbCompSen.Rows.Add(tr)
                    Next
                End If
            End Using

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
        End Try
    End Sub

    Private Sub DoDelete()
        If selectedRow(gvMain) < 0 Then
            '尚未選取資料列！
            Bsp.Utility.ShowFormatMessage(Me, "W_00000")
        Else
            Dim beNotEmpLog As New beNotEmpLog.Row()
            Dim objST As New ST1

            beNotEmpLog.CompID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("CompID").ToString()
            beNotEmpLog.EmpID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("EmpID").ToString()
            beNotEmpLog.Seq.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("Seq").ToString()
            beNotEmpLog.IDNo.Value = ViewState.Item("IDNo")

            Try
                objST.DeleteNotEmpLogSetting(beNotEmpLog)
            Catch ex As Exception
                Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoDelete", ex)
            End Try
            gvMain.DataBind()

            DoQuery()
        End If
    End Sub

    Protected Sub gvMain_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMain.RowCommand
        If e.CommandName = "Detail" Then
            Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)

            btnX.Caption = "返回"

            Me.TransferFramePage("~/ST/ST1902.aspx", New ButtonState() {btnX}, _
                "SelectedCompID=" & ViewState.Item("CompID"), _
                "SelectedCompName=" & ViewState.Item("CompName"), _
                "SelectedEmpID=" & ViewState.Item("EmpID"), _
                "SelectedEmpName=" & ViewState.Item("EmpName"), _
                "SelectedIDNo=" & ViewState.Item("IDNo"), _
                "CompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
                "EmpID=" & gvMain.DataKeys(selectedRow(gvMain))("EmpID").ToString(), _
                "Seq=" & gvMain.DataKeys(selectedRow(gvMain))("Seq").ToString(), _
                "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
        End If
    End Sub
End Class
