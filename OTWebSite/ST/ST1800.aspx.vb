'****************************************************
'功能說明：員工企業團經歷維護
'建立人員：Micky Sung
'建立日期：2015.06.10
'****************************************************
Imports System.Data

Partial Class ST_ST1800
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
                Release("btnDelete")
                'DoDelete()
        End Select
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
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
            Else
                Return
            End If

            If ht.ContainsKey("PageNo1800") Then pcMain.PageNo = Convert.ToInt32(ht("PageNo1800"))

            DoQuery()
        End If
    End Sub

    Private Sub DoAdd()
        Dim objSC As New SC
        Dim btnA As New ButtonState(ButtonState.emButtonType.Add)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
        Dim btnC As New ButtonState(ButtonState.emButtonType.Cancel)

        btnA.Caption = "存檔返回"
        btnX.Caption = "返回"
        btnC.Caption = "清除"

        Me.TransferFramePage("~/ST/ST1801.aspx", New ButtonState() {btnA, btnX, btnC}, _
            lblCompRoleID.ID & "=" & ViewState.Item("CompID"), _
            txtEmpID.ID & "=" & txtEmpID.Text, _
            txtEmpName.ID & "=" & txtEmpName.Text, _
            "SelectedCompID=" & ViewState.Item("CompID"), _
            "SelectedCompName=" & objSC.GetCompName(ViewState.Item("CompID")).Rows(0).Item("CompName").ToString, _
            "SelectedEmpID=" & txtEmpID.Text, _
            "SelectedEmpName=" & txtEmpName.Text, _
            "SelectedIDNo=" & ViewState.Item("IDNo"), _
            "PageNo1800=" & pcMain.PageNo.ToString())
    End Sub

    Private Sub DoUpdate()
        Dim objSC As New SC
        Dim btnU As New ButtonState(ButtonState.emButtonType.Update)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
        Dim btnC As New ButtonState(ButtonState.emButtonType.Cancel)

        btnU.Caption = "存檔返回"
        btnX.Caption = "返回"
        btnC.Caption = "清除"

        Me.TransferFramePage("~/ST/ST1802.aspx", New ButtonState() {btnU, btnX, btnC}, _
            lblCompRoleID.ID & "=" & ViewState.Item("CompID"), _
            txtEmpID.ID & "=" & txtEmpID.Text, _
            txtEmpName.ID & "=" & txtEmpName.Text, _
            "SelectedCompID=" & ViewState.Item("CompID"), _
            "SelectedCompName=" & objSC.GetCompName(ViewState.Item("CompID")).Rows(0).Item("CompName").ToString, _
            "SelectedEmpID=" & txtEmpID.Text, _
            "SelectedEmpName=" & txtEmpName.Text, _
            "SelectedIDNo=" & gvMain.DataKeys(selectedRow(gvMain))("IDNo").ToString(), _
            "SelectedModifyDate=" & gvMain.DataKeys(selectedRow(gvMain))("ModifyDate").ToString(), _
            "SelectedReason=" & gvMain.DataKeys(selectedRow(gvMain))("Reason").ToString(), _
            "PageNo1800=" & pcMain.PageNo.ToString())
    End Sub

    Private Sub DoQuery()
        Dim objST As New ST1
        gvMain.Visible = True

        Try
            pcMain.DataTable = objST.QueryEmployeeLogSetting(
                "CompID=" & ViewState.Item("CompID"), _
                "EmpID=" & txtEmpID.Text, _
                "EmpName=" & txtEmpName.Text)

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
        End Try
    End Sub

    Private Sub DoDelete()
        If selectedRow(gvMain) < 0 Then
            '尚未選取資料列！
            Bsp.Utility.ShowFormatMessage(Me, "W_00000")
        Else
            Dim beEmployeeLog As New beEmployeeLog.Row()
            Dim objST As New ST1

            beEmployeeLog.IDNo.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("IDNo").ToString()
            beEmployeeLog.ModifyDate.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("ModifyDate").ToString()
            beEmployeeLog.Reason.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("Reason").ToString()
            '2015/08/03 Add 刪除時增加"序號"
            beEmployeeLog.Seq.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("Seq").ToString()

            Try
                objST.DeleteEmployeeLogSetting(beEmployeeLog)
            Catch ex As Exception
                Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoDelete", ex)
            End Try
            gvMain.DataBind()

            DoQuery()
        End If
    End Sub

    Protected Sub gvMain_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMain.RowCommand
        If e.CommandName = "Detail" Then
            Dim objSC As New SC
            Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)

            btnX.Caption = "返回"

            Me.TransferFramePage("~/ST/ST1802.aspx", New ButtonState() {btnX}, _
                lblCompRoleID.ID & "=" & ViewState.Item("CompID"), _
                txtEmpID.ID & "=" & txtEmpID.Text, _
                txtEmpName.ID & "=" & txtEmpName.Text, _
                "SelectedCompID=" & ViewState.Item("CompID"), _
                "SelectedCompName=" & objSC.GetCompName(ViewState.Item("CompID")).Rows(0).Item("CompName").ToString, _
                "SelectedEmpID=" & txtEmpID.Text, _
                "SelectedEmpName=" & txtEmpName.Text, _
                "SelectedIDNo=" & gvMain.DataKeys(selectedRow(gvMain))("IDNo").ToString(), _
                "SelectedModifyDate=" & gvMain.DataKeys(selectedRow(gvMain))("ModifyDate").ToString(), _
                "SelectedReason=" & gvMain.DataKeys(selectedRow(gvMain))("Reason").ToString(), _
                "PageNo1800=" & pcMain.PageNo.ToString())
        End If
    End Sub

    Private Sub Release(ByVal LogFunction As String)
        ucRelease.ShowCompRole = "True"
        ucRelease.FunID = "ST1800"
        ucRelease.LogFunction = LogFunction
        ucRelease.OpenSelect()
    End Sub

    Public Overrides Sub DoModalReturn(ByVal returnValue As String)
        Dim strSql As String = ""

        If returnValue <> "" Then
            Dim aryData() As String = returnValue.Split(":")

            Select Case aryData(0)
                Case "ucRelease"
                    lblReleaseResult.Text = ""
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    lblReleaseResult.Text = aryValue(0)
                    If lblReleaseResult.Text = "Y" Then
                        DoDelete()
                    End If
            End Select

        End If
    End Sub

    '2015/08/10 Add 重組GridViewHeader
    Protected Sub gvMain_RowCreated(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.Header Then
            Dim tcc As TableCellCollection = e.Row.Cells
            Dim intcount As Integer = tcc.Count

            tcc.Add(New TableHeaderCell())
            tcc(22).Text() = "</th>"
            tcc(22).ColumnSpan = "5"
            tcc(22).CssClass = "td_header"

            tcc.Add(New TableHeaderCell())
            tcc(23).Text() = "異動前</th>"
            tcc(23).ColumnSpan = "7"
            tcc(23).CssClass = "td_header"

            tcc.Add(New TableHeaderCell())
            tcc(24).Text() = "異動後</th></tr><tr>"
            tcc(24).ColumnSpan = "7"
            tcc(24).CssClass = "td_header"

            For i As Integer = 0 To intcount - 1
                tcc.Add(tcc(0))
            Next
        End If
    End Sub

End Class
