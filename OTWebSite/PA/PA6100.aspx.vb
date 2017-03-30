'****************************************************
'功能說明：薪資所得扣繳稅額繳款書參數維護
'建立人員：BeatriceCheng
'建立日期：2016.05.11
'****************************************************
Imports System.Data

Partial Class PA_PA6100
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ddlCompID.Visible = False

            If UserProfile.SelectCompRoleID = "ALL" Then
                ddlCompID.Visible = True
                Bsp.Utility.FillHRCompany(ddlCompID)
                lblCompRoleID.Visible = False
            Else
                ddlCompID.Visible = False
                lblCompRoleID.Text = UserProfile.SelectCompRoleName
                lblCompRoleID.Visible = True
            End If
        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnAdd"       '新增
                DoAdd()
            Case "btnUpdate"    '修改
                DoUpdate()
            Case "btnQuery"     '查詢
                ViewState.Item("DoQuery") = "Y"
                DoQuery()
            Case "btnDelete"    '刪除
                DoDelete()
            Case "btnActionX"   '清除
                DoClear()
        End Select
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

            For Each strKey As String In ht.Keys
                Dim ctl As Control = Me.FindControl(strKey)

                If TypeOf ctl Is TextBox Then
                    CType(ctl, TextBox).Text = ht(strKey).ToString()
                ElseIf TypeOf ctl Is DropDownList Then
                    CType(ctl, DropDownList).SelectedValue = ht(strKey).ToString()
                End If
            Next
            If ht.ContainsKey("PageNo") Then pcMain.PageNo = Convert.ToInt32(ht("PageNo"))
            If ht.ContainsKey("DoQuery") Then
                If ht("DoQuery").ToString() = "Y" Then
                    ViewState.Item("DoQuery") = "Y"
                    DoQuery()
                End If
            End If
        End If
    End Sub

    Private Sub DoAdd()
        Dim btnA As New ButtonState(ButtonState.emButtonType.Add)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
        Dim btnC As New ButtonState(ButtonState.emButtonType.Cancel)

        btnA.Caption = "存檔返回"
        btnX.Caption = "返回"
        btnC.Caption = "清除"

        Me.TransferFramePage("~/PA/PA6101.aspx", New ButtonState() {btnA, btnX, btnC}, _
            Bsp.Utility.FormatToParam(txtInvoiceNo), _
            "PageNo=" & pcMain.PageNo.ToString(), _
            "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
    End Sub

    Private Sub DoUpdate()
        Dim btnU As New ButtonState(ButtonState.emButtonType.Update)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
        Dim btnC As New ButtonState(ButtonState.emButtonType.Cancel)

        btnU.Caption = "存檔返回"
        btnX.Caption = "返回"
        btnC.Caption = "清除"

        Me.TransferFramePage("~/PA/PA6102.aspx", New ButtonState() {btnU, btnX, btnC}, _
            Bsp.Utility.FormatToParam(txtInvoiceNo), _
            "PageNo=" & pcMain.PageNo.ToString(), _
            "SelectedCompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
            "SelectedInvoiceNo=" & gvMain.DataKeys(selectedRow(gvMain))("InvoiceNo").ToString(), _
            "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
    End Sub

    Private Sub DoQuery()
        Dim objPA6 As New PA6()

        Try
            Dim strCompID As String = ddlCompID.SelectedValue
            If strCompID = "" Then
                strCompID = UserProfile.SelectCompRoleID
            End If

            pcMain.DataTable = objPA6.TaxParameterOrganQuery(
                "CompID=" & strCompID, _
                "InvoiceNo=" & txtInvoiceNo.Text)
            gvMain.Visible = True

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
        End Try
    End Sub

    Private Sub DoDelete()
        If selectedRow(gvMain) < 0 Then
            '尚未選取資料列！
            Bsp.Utility.ShowFormatMessage(Me, "W_00000")
        Else
            Dim beTaxParameterOrgan As New beTaxParameterOrgan.Row()
            Dim objPA6 As New PA6

            beTaxParameterOrgan.CompID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("CompID").ToString()
            beTaxParameterOrgan.InvoiceNo.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("InvoiceNo").ToString()

            Try
                objPA6.TaxParameterOrganDelete(beTaxParameterOrgan)
            Catch ex As Exception
                Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoDelete", ex)
            End Try

            DoQuery()
        End If
    End Sub

    Protected Sub gvMain_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMain.RowCommand
        If e.CommandName = "Detail" Then
            Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
            btnX.Caption = "返回"

            Me.TransferFramePage("~/PA/PA6102.aspx", New ButtonState() {btnX}, _
                Bsp.Utility.FormatToParam(ddlCompID), _
                Bsp.Utility.FormatToParam(txtInvoiceNo), _
                "PageNo=" & pcMain.PageNo.ToString(), _
                "SelectedCompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
                "SelectedInvoiceNo=" & gvMain.DataKeys(selectedRow(gvMain))("InvoiceNo").ToString(), _
                "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))

        End If
    End Sub

    Private Sub DoClear()
        ViewState.Item("DoQuery") = ""
        txtInvoiceNo.Text = ""

        If ddlCompID.SelectedValue <> "" Then
            ddlCompID.SelectedIndex = 0
        End If

        If Not pcMain.DataTable Is Nothing Then
            pcMain.DataTable.Clear()
            pcMain.BindGridView()
        End If

        gvMain.Visible = False
    End Sub

End Class
