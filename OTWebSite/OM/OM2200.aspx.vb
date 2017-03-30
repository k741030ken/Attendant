'****************************************************
'功能說明：組織異動紀錄查詢
'建立人員：Rebecca Yan
'建立日期：2016.09.24
'****************************************************
Imports System.Collections.Generic
Imports System.Data
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.TableRow
Imports System.Web.UI.WebControls.TableCell
Imports System.Web.UI.WebControls.TableCellCollection

Partial Class OM_OM2200
    Inherits PageBase
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)
        If ht.ContainsKey("SelectedCompID") Then
            ViewState.Item("CompID") = ht("SelectedCompID").ToString() '公司代碼
            ViewState.Item("CompName") = ht("SelectedCompName").ToString() '公司名稱

            Dim aryValue() As String = Split(ht("SelectedOrganType").ToString(), "-") '組織類型
            ViewState.Item("OrganType") = ht("SelectedOrganType").ToString()
            hidOrganType.Value = aryValue(0)
            txtOrganType.Text = ht("SelectedOrganType").ToString()
            ViewState.Item("OrganID") = ht("SelectedOrganID").ToString() '組織代碼
            ViewState.Item("OrganName") = ht("SelectedOrganName").ToString() '組織名稱
            'ViewState.Item("OrganReason") = ht("SelectedOrganReason").ToString()
            txtCompID.Text = UserProfile.SelectCompRoleName
            txtOrganID.Text = ViewState.Item("OrganID").ToString()
            txtOrganName.Text = ViewState.Item("OrganName").ToString()
            'hidOrganReason.Value = ViewState.Item("OrganReason")
            DoQuery()
        Else
            Return
        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String) '按鈕
        Select Case Param
            Case "btnAdd"       '新增
                DoAdd()
            Case "btnUpdate"    '修改
                DoUpdate()
            Case "btnDelete"    '刪除
                'Release("btnDelete") '放行
                DoDelete()
        End Select
    End Sub

    Private Sub DoAdd() '新增
        Dim btnU As New ButtonState(ButtonState.emButtonType.Update)
        Dim btnF As New ButtonState(ButtonState.emButtonType.Confirm)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
        Dim btnC As New ButtonState(ButtonState.emButtonType.Cancel)
        Dim aryValue() As String = Split(txtCompID.Text, "-")
        btnU.Caption = "存檔返回"
        btnF.Caption = "存檔"
        btnX.Caption = "返回"
        btnC.Caption = "清除"

        Me.TransferFramePage("~/OM/OM2201.aspx", New ButtonState() {btnU, btnF, btnX, btnC}, _
            txtCompID.ID & "=" & txtCompID.Text, _
            txtOrganID.ID & "=" & txtOrganID.Text, _
            txtOrganName.ID & "=" & txtOrganName.Text, _
            "SelectedCompID=" & aryValue(0), _
            "SelectedCompName=" & aryValue(1), _
            "SelectedOrganName=" & txtOrganName.Text, _
            "SelectedOrganType=" & ViewState.Item("OrganType"), _
            "SelectedOrganID=" & txtOrganID.Text, _
            "PageNo=" & pcMain.PageNo.ToString(), _
             "PageID=" & "NOTupdate", _
            "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
    End Sub

    Private Sub DoUpdate() '(20161101-leo modify)
        Dim btnU As New ButtonState(ButtonState.emButtonType.Update)
        Dim btnF As New ButtonState(ButtonState.emButtonType.Confirm)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
        Dim btnC As New ButtonState(ButtonState.emButtonType.Cancel)
        Dim aryValue() As String = Split(txtCompID.Text, "-")
        btnU.Caption = "存檔返回"
        btnF.Caption = "存檔"
        btnX.Caption = "返回"
        btnC.Caption = "清除"
        Me.TransferFramePage("~/OM/OM2202.aspx", New ButtonState() {btnU, btnF, btnX, btnC}, _
            txtCompID.ID & "=" & txtCompID.Text, _
            txtOrganID.ID & "=" & txtOrganID.Text, _
            txtOrganName.ID & "=" & txtOrganName.Text, _
            "SelectedCompID=" & aryValue(0), _
            "SelectedCompName=" & aryValue(1), _
            "SelectedOrganName=" & txtOrganName.Text, _
            "SelectedOrganType=" & ViewState.Item("OrganType"), _
            "SelectedOrganReason=" & gvMain.DataKeys(selectedRow(gvMain))("OrganReason").ToString(), _
            "SelectedOrganID=" & txtOrganID.Text, _
            "SelectedValidDateB=" & gvMain.DataKeys(selectedRow(gvMain))("ValidDateB").ToString(), _
            "SelectedValidDateE=" & gvMain.DataKeys(selectedRow(gvMain))("ValidDateE").ToString(), _
            "SelectedSeq=" & gvMain.DataKeys(selectedRow(gvMain))("Seq").ToString(), _
            "PageNo=" & pcMain.PageNo.ToString(), _
             "PageID=" & "ISupdate", _
            "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
    End Sub

    Private Sub DoQuery() '查詢
        Dim objOM As New OM1()
        gvMain.Visible = True
        ViewState.Item("DoQuery") = "Y"
        Try
            pcMain.DataTable = objOM.OM2000OrganizationLog(ViewState.Item("CompID"), "", hidOrganType.Value, "", ViewState.Item("OrganID"))
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
        End Try
    End Sub

    Private Sub DoDelete()
        If selectedRow(gvMain) < 0 Then
            '尚未選取資料列！
            Bsp.Utility.ShowFormatMessage(Me, "W_00000")
        Else
            Dim beOrganizationLog As New beOrganizationLog.Row()
            Dim bsOrganizationLog As New beOrganizationLog.Service
            'Dim objOM As New OM1

            'Dim strCompID As String
            'If UserProfile.SelectCompRoleID = "ALL" Then
            '    strCompID = ddlCompID.SelectedValue
            'Else
            '    strCompID = UserProfile.SelectCompRoleID
            'End If
            Dim aryValue() As String = Split(gvMain.DataKeys(Me.selectedRow(gvMain))("OrganReason").ToString(), "-")
            beOrganizationLog.CompID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("CompID").ToString()
            beOrganizationLog.OrganID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("OrganID").ToString()
            beOrganizationLog.OrganReason.Value = aryValue(0)
            aryValue = Split(gvMain.DataKeys(Me.selectedRow(gvMain))("OrganType").ToString(), "-")
            beOrganizationLog.OrganType.Value = aryValue(0)
            beOrganizationLog.ValidDateB.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("ValidDateB").ToString()
            beOrganizationLog.Seq.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("Seq").ToString()
            Try
                'objOM.DeleteOrganization(beOrganization)
                'objOM.OM2000DeleteOrganizationLog(beOrganizationLog)
                If bsOrganizationLog.DeleteRowByPrimaryKey(beOrganizationLog) <= 0 Then
                    Bsp.Utility.ShowMessage(Me, "錯誤")
                End If
            Catch ex As Exception
                Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoDelete", ex)
            End Try
            gvMain.DataBind()

            DoQuery()
            Bsp.Utility.ShowMessage(Me, "刪除成功")
        End If
    End Sub

    Private Sub DoExecutes()

    End Sub
    '(20161101-leo modify)
    Protected Sub gvMain_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMain.RowCommand
        Dim objOM2 As New OM2()
        Dim aryValue() As String = Split(txtCompID.Text, "-")
        Dim strCompID As String
        strCompID = UserProfile.SelectCompRoleID

        If e.CommandName = "Detail" Then
            Dim a As New FlowBackInfo()
            a.MenuNodeTitle = "回清單"
            a.URL = "~/OM/OM2201.aspx"
            Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)

            btnX.Caption = "返回"
            Me.TransferFramePage("~/OM/OM2202.aspx", New ButtonState() {btnX}, _
                txtCompID.ID & "=" & txtCompID.Text, _
            txtOrganID.ID & "=" & txtOrganID.Text, _
            txtOrganName.ID & "=" & txtOrganName.Text, _
            "SelectedCompID=" & aryValue(0), _
            "SelectedCompName=" & aryValue(1), _
            "SelectedOrganName=" & txtOrganName.Text, _
            "SelectedOrganType=" & ViewState.Item("OrganType"), _
            "SelectedOrganReason=" & gvMain.DataKeys(selectedRow(gvMain))("OrganReason").ToString(), _
            "SelectedOrganID=" & txtOrganID.Text, _
            "SelectedValidDateB=" & gvMain.DataKeys(selectedRow(gvMain))("ValidDateB").ToString(), _
            "SelectedValidDateE=" & gvMain.DataKeys(selectedRow(gvMain))("ValidDateE").ToString(), _
            "SelectedSeq=" & gvMain.DataKeys(selectedRow(gvMain))("Seq").ToString(), _
            "PageNo=" & pcMain.PageNo.ToString(), _
            "PageID=" & "NOTupdate", _
            "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
         

        End If
    End Sub
    '清除
    Private Sub DoClear()
        ViewState.Item("DoQuery") = ""
        gvMain.Visible = False

        'ddlCompID.SelectedValue = ""
        'ddlOrganID.SelectedValue = ""
        'txtOrganNameA.Text = ""
    End Sub

    'Protected Sub gvMain_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
    '    If e.Row.RowType = DataControlRowType.Header Then
    '        e.Row.Cells(0).Text = "<tr><td rowspan='2'>XXX</td><td colspan='2' style='text-align: center'>YYYY</td><td style='text-align: center'>ZZZ</td></tr>"
    '        'e.Row.Cells[0].Text = "<tr><td rowspan='2'>XXX</td><td colspan='2' style='text-align: center'>YYYY</td><td style='text-align: center'>ZZZ</td></tr>"
    '    End If
    'End Sub

    Friend Function GridviewDoubleHeader(ByVal _gd As GridViewRowEventArgs, ByVal _od As Dictionary(Of String, Integer)) As GridViewRow
        Dim gvd = New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal)

        Dim definehd = New TableCell()
        For Each oK As KeyValuePair(Of String, Integer) In _od
            definehd = New TableCell()
            definehd.Text = oK.Key
            definehd.ColumnSpan = oK.Value
            'definehd.Attributes.Add("bgcolor", "#99CCCC")
            definehd.Attributes.Add("align", "center") '標頭致中
            gvd.Cells.Add(definehd)
        Next
        gvd.BackColor = System.Drawing.Color.LightGray
        gvd.ForeColor = System.Drawing.Color.Black

        gvd.Visible = True

        Return gvd
    End Function

    Protected Sub gvMain_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.Header Then
            Dim od = New Dictionary(Of String, Integer)
            od.Add("", 5)
            od.Add("異動前", 4)
            od.Add("異動後", 4)

            gvMain.Controls(0).Controls.AddAt(0, GridviewDoubleHeader(e, od))
        End If
    End Sub
End Class
