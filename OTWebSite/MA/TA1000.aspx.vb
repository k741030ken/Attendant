'****************************************************
'功能說明：
'建立人員：
'建立日期：
'****************************************************
Imports System.Data

Partial Class TA_TA1000
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txtDeptID.QuerySQL = "Select Distinct OrganID, OrganName From Organization Where CompID='" + UserProfile.SelectCompRoleID + "' And OrganID=DeptID And InValidFlag='0' Order By OrganID"
            txtDeptID.LoadData()
            txtOrganID.QuerySQL = "Select Distinct OrganID, OrganName From Organization Where CompID='" + UserProfile.SelectCompRoleID + "' And OrganID<>DeptID And InValidFlag='0' Order By OrganID"
            txtOrganID.LoadData()

            ddlFlowOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))

            ucSelectOrgan.SelectCompID = UserProfile.SelectCompRoleID
            ucSelectOrgan.InValidFlag = "Y"
            ucSelectOrgan.Fields = New FieldState() { _
                New FieldState("Organization", "行政組織", True, True), _
                New FieldState("OrganizationFlow", "功能組織", True, True), _
                New FieldState("OrganizationWait", "待異動組織", True, True)}

            DoOrgQuery()
        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnQuery"
                DoQuery()
            Case "btnActionX"
                ClearData()
        End Select
    End Sub

    Private Sub DoQuery()
        lblDeptID.Text = txtDeptID.DataText
        lblOrganID.Text = txtOrganID.DataText
    End Sub

    Private Sub DoOrgQuery()
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine("SELECT TOP 5 C.CompID")
        strSQL.AppendLine(", C.CompName")
        strSQL.AppendLine(", '行政組織' OrganType")
        strSQL.AppendLine(", O.OrganID")
        strSQL.AppendLine(", O.OrganName")
        strSQL.AppendLine("FROM Organization O")
        strSQL.AppendLine("LEFT JOIN Company C ON C.CompID = O.CompID")
        strSQL.AppendLine("WHERE 1=1")
        strSQL.AppendLine("AND O.CompID = " & Bsp.Utility.Quote(UserProfile.SelectCompRoleID))
        strSQL.AppendLine("AND O.InValidFlag = '0' AND O.VirtualFlag = '0'")

        strSQL.AppendLine("UNION")

        strSQL.AppendLine("SELECT TOP 5 C.CompID")
        strSQL.AppendLine(", C.CompName")
        strSQL.AppendLine(", '功能組織' OrganType")
        strSQL.AppendLine(", O.OrganID")
        strSQL.AppendLine(", O.OrganName")
        strSQL.AppendLine("FROM OrganizationFlow O")
        strSQL.AppendLine("LEFT JOIN Company C ON C.CompID = O.CompID")
        strSQL.AppendLine("WHERE 1=1")
        strSQL.AppendLine("AND O.CompID = " & Bsp.Utility.Quote(UserProfile.SelectCompRoleID))
        strSQL.AppendLine("AND O.InValidFlag = '0' AND O.VirtualFlag = '0'")

        strSQL.AppendLine("Order By OrganType")
        Using dt = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "eHRMSDB").Tables(0)
            gvMain.DataSource = dt
            gvMain.DataBind()
        End Using
    End Sub

    Protected Sub gvMain_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMain.RowCommand
        Dim objMA1 As New MA1()

        If e.CommandName = "Detail" Then
            Dim a As New FlowBackInfo()
            a.MenuNodeTitle = "回清單"
            a.URL = "~/MA/TA1000.aspx"

            If gvMain.DataKeys(Me.selectedRow(gvMain))("OrganType").ToString() = "行政組織" Then
                TransferFramePage(Bsp.MySettings.FlowRedirectPage, Nothing, "FlowID=ORGINFO", a, _
                    "SelectedCompID=" & gvMain.DataKeys(Me.selectedRow(gvMain))("CompID").ToString(), _
                    "SelectedCompName=" & gvMain.DataKeys(Me.selectedRow(gvMain))("CompName").ToString, _
                    "SelectedOrganID=" & gvMain.DataKeys(Me.selectedRow(gvMain))("OrganID").ToString(), _
                    "SelectedOrganName=" & gvMain.DataKeys(Me.selectedRow(gvMain))("OrganName").ToString(), _
                    "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
            Else
                TransferFramePage(Bsp.MySettings.FlowRedirectPage, Nothing, "FlowID=ORGFLOWINFO", a, _
                "SelectedCompID=" & gvMain.DataKeys(Me.selectedRow(gvMain))("CompID").ToString(), _
                "SelectedCompName=" & gvMain.DataKeys(Me.selectedRow(gvMain))("CompName").ToString, _
                "SelectedOrganID=" & gvMain.DataKeys(Me.selectedRow(gvMain))("OrganID").ToString(), _
                "SelectedOrganName=" & gvMain.DataKeys(Me.selectedRow(gvMain))("OrganName").ToString(), _
                "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
            End If
        End If
    End Sub

    Private Sub ClearData()
        txtDeptID.DataText = ""
        lblDeptID.Text = ""
        txtOrganID.DataText = ""
        lblOrganID.Text = ""
    End Sub

    Protected Sub txtDeptID_TextChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDeptID.ucSelectTextChangedHandler_TextChange
        lblDeptID.Text = txtDeptID.DataText
    End Sub

    Protected Sub txtOrganID_TextChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtOrganID.ucSelectTextChangedHandler_TextChange
        lblOrganID.Text = txtOrganID.DataText
    End Sub

    '載入按鈕-比對簽核單位畫面
    Protected Sub ucFlowOrgan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ucFlowOrgan.Load
        ucFlowOrgan.QueryCompID = ""
        ucFlowOrgan.DefaultFlowOrgan = hldFlowOrgan.Value
        ucFlowOrgan.Fields = New FieldState() { _
            New FieldState("OrganID", "單位代碼", True, True), _
            New FieldState("OrganName", "單位名稱", True, True)}
    End Sub

    Public Overrides Sub DoModalReturn(ByVal returnValue As String)
        If returnValue <> "" Then
            Dim aryData() As String = returnValue.Split(":")

            Select Case aryData(0)
                Case "ucFlowOrgan" '比對簽核單位
                    If aryData(1).Replace("'", "").Length = 0 Then
                        hldFlowOrgan.Value = ""
                        ddlFlowOrganID.Items.Clear()
                        ddlFlowOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
                    Else
                        hldFlowOrgan.Value = aryData(1)
                        Bsp.Utility.FillDDL(ddlFlowOrganID, "eHRMSDB", "OrganizationFlow", "RTrim(OrganID)", "OrganName", Bsp.Utility.DisplayType.Full, "", "And OrganID In (" & aryData(1) & ")", "Order by OrganID")
                    End If

                Case "ucSelectOrgan" '單位代碼快速查詢
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    lblSelectCompID.Text = aryValue(0)
                    lblSelectCompName.Text = aryValue(1)
                    txtSelectOrganID.Text = aryValue(2)
                    lblSelectOrganName.Text = aryValue(3)
                    lblSelectOrganType.Text = aryValue(4)
            End Select
        End If
    End Sub
End Class

