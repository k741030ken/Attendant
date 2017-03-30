'****************************************************
'功能說明：歷任主管記錄-查詢
'建立人員：Rebecca Yan
'建立日期：2015.10
'****************************************************

Imports System.Data

Partial Class OM_OM2300
    Inherits PageBase
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            
        End If
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)
        If ht.ContainsKey("SelectedCompID") Then
            ViewState.Item("CompID") = ht("SelectedCompID").ToString() '公司代碼
            ViewState.Item("CompName") = ht("SelectedCompName").ToString() '公司名稱
            ViewState.Item("OrganType") = ht("SelectedOrganType").ToString() '組織類型
            ViewState.Item("OrganID") = ht("SelectedOrganID").ToString() '組織代碼
            ViewState.Item("OrganName") = ht("SelectedOrganName").ToString() '組織名稱

            txtCompID.Text = UserProfile.SelectCompRoleName
            txtOrganType.Text = ViewState.Item("OrganType").ToString()
            txtOrganID.Text = ViewState.Item("OrganID").ToString()
            txtOrganName.Text = ViewState.Item("OrganName").ToString()
            hidCompID.Value = UserProfile.SelectCompRoleID '隱藏欄位(公司代碼)
            hidOrganType.Value = ViewState.Item("OrganType").ToString() '隱藏欄位(組織類型)

            GetSelectData(hidCompID.Value, txtOrganID.Text)
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
        Dim btnA As New ButtonState(ButtonState.emButtonType.Add)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
        Dim btnC As New ButtonState(ButtonState.emButtonType.Cancel)

        btnA.Caption = "存檔返回"
        btnX.Caption = "返回"
        btnC.Caption = "清除"

        Me.TransferFramePage("~/OM/OM2301.aspx", New ButtonState() {btnA, btnX, btnC}, _
            txtCompID.ID & "=" & txtCompID.Text, _
            hidOrganType.ID & "=" & hidOrganType.Value, _
            "txtOrganName=" & txtOrganID.Text & "-" & txtOrganName.Text, _
            "hidOrganID=" & txtOrganID.Text, _
            "SelectedCompID=" & ViewState.Item("CompID"), _
            "SelectedCompName=" & ViewState.Item("CompName"), _
            "SelectedOrganID=" & ViewState.Item("OrganID"), _
            "SelectedOrganName=" & ViewState.Item("OrganName"), _
            "SelectedOrganType=" & ViewState.Item("OrganType"), _
            "PageNo=" & pcMain.PageNo.ToString(), _
            "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
    End Sub

    Private Sub DoUpdate()
        If selectedRow(gvMain) < 0 Then
            '尚未選取資料列！
            Bsp.Utility.ShowFormatMessage(Me, "W_00000")
        Else
            Dim btnU As New ButtonState(ButtonState.emButtonType.Update)
            Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
            Dim btnC As New ButtonState(ButtonState.emButtonType.Cancel)

            btnU.Caption = "存檔返回"
            btnX.Caption = "返回"
            btnC.Caption = "清除"

            Me.TransferFramePage("~/OM/OM2302.aspx", New ButtonState() {btnU, btnX, btnC}, _
                txtCompID.ID & "=" & txtCompID.Text, _
                hidOrganType.ID & "=" & hidOrganType.Value, _
                "txtOrganName=" & txtOrganID.Text & "-" & txtOrganName.Text, _
                "hidOrganID=" & txtOrganID.Text, _
                "SelectedCompID=" & ViewState.Item("CompID"), _
                "SelectedCompName=" & ViewState.Item("CompName"), _
                "SelectedOrganID=" & ViewState.Item("OrganID"), _
                "SelectedOrganName=" & ViewState.Item("OrganName"), _
                "SelectedOrganType=" & ViewState.Item("OrganType"), _
                "SelectedBoss=" & ViewState.Item("Boss"), _
                "SelectedBossName=" & ViewState.Item("BossName"), _
                "BossCompID=" & gvMain.DataKeys(Me.selectedRow(gvMain))("BossCompID").ToString(), _
                "Boss=" & gvMain.DataKeys(Me.selectedRow(gvMain))("Boss").ToString(), _
                "BossName=" & gvMain.DataKeys(Me.selectedRow(gvMain))("BossName").ToString(), _
                "BossType=" & gvMain.DataKeys(Me.selectedRow(gvMain))("BossType").ToString(), _
                "ValidDateBH=" & gvMain.DataKeys(Me.selectedRow(gvMain))("ValidDateBH").ToString(), _
                "ValidDateEH=" & gvMain.DataKeys(Me.selectedRow(gvMain))("ValidDateEH").ToString(), _
                "PageNo=" & pcMain.PageNo.ToString(), _
                "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
        End If

    End Sub

    Private Sub GetSelectData(ByVal CompID As String, ByVal OrganID As String)
        Dim objOM As New OM2()

        Dim strSQL As New StringBuilder()
        'Dim strOrganType As String = ViewState.Item("OrganType")
        Try
            If hidOrganType.Value = "1-行政組織" Then
                Using dt As DataTable = objOM.OM2000QueryOrganizationBossLogNow(hidCompID.Value, txtOrganID.Text)
                    If dt.Rows().Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            txtBossName.Text = dr.Item("Boss").ToString.Trim + "-" + dr.Item("BossName").ToString.Trim
                            txtValidDateBH.Text = dr.Item("ValidDateBH").ToString.Trim
                            txtBossType.Text = dr.Item("BossType").ToString.Trim() '任用方式
                            If txtBossType.Text = "1" Then
                                txtBossType.Text = "主要"
                            ElseIf txtBossType.Text = "0" Then
                                txtBossType.Text = "兼任"
                            End If
                        Next
                    End If
                End Using
            ElseIf hidOrganType.Value = "2-功能組織" Then
                Using dt As DataTable = objOM.OM2000QueryOrganizationFlowBossLogNow(hidCompID.Value, txtOrganID.Text)
                    If dt.Rows().Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            txtBossName.Text = dr.Item("Boss").ToString.Trim + "-" + dr.Item("BossName").ToString.Trim
                            txtValidDateBH.Text = dr.Item("ValidDateBH").ToString.Trim
                            txtBossType.Text = dr.Item("BossType").ToString.Trim() '任用方式
                            If txtBossType.Text = "1" Then
                                txtBossType.Text = "1-主要"
                            ElseIf txtBossType.Text = "0" Then
                                txtBossType.Text = "2-兼任"
                            End If
                        Next
                    End If
                End Using
            End If

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & "GetSelectData", ex)
        End Try
    End Sub

    Private Sub DoQuery()
        Dim objOM As New OM2()
        gvMain.Visible = True
        ViewState.Item("DoQuery") = "Y"

        'Dim strOrganType As String = ViewState.Item("OrganType")

        Try
            If hidOrganType.Value = "1-行政組織" Then
                pcMain.DataTable = objOM.OM2000QueryOrganizationBossLogPast(hidCompID.Value, txtOrganID.Text)
            ElseIf hidOrganType.Value = "2-功能組織" Then
                pcMain.DataTable = objOM.OM2000QueryOrganizationFlowBossPast(hidCompID.Value, txtOrganID.Text)
            End If
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
        End Try
    End Sub

    Private Sub DoDelete()
        If selectedRow(gvMain) < 0 Then
            '尚未選取資料列！
            Bsp.Utility.ShowFormatMessage(Me, "W_00000")
        Else
            Dim beOrganizationBossLog As New beOrganizationBossLog.Row()
            Dim beOrganizationFlowBossLog As New beOrganizationFlowBossLog.Row()
            Dim objOM As New OM2

            '刪除歷來主管紀錄
            If hidOrganType.Value = "1-行政組織" Then
                beOrganizationBossLog.CompID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("CompID").ToString()
                beOrganizationBossLog.OrganID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("OrganID").ToString()
                beOrganizationBossLog.ValidDateBH.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("ValidDateBH").ToString()

                Try
                    objOM.OM2000DeleteOrganizationBossLog(beOrganizationBossLog)
                Catch ex As Exception
                    Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoDelete", ex)
                End Try
            ElseIf hidOrganType.Value = "2-功能組織" Then
                '刪除歷來主管紀錄(簽核)
                beOrganizationFlowBossLog.CompID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("CompID").ToString()
                beOrganizationFlowBossLog.OrganID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("OrganID").ToString()
                beOrganizationFlowBossLog.ValidDateBH.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("ValidDateBH").ToString()

                Try
                    objOM.OM2000DeleteOrganizationFlowBossLog(beOrganizationFlowBossLog)
                Catch ex As Exception
                    Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoDelete", ex)
                End Try
            End If
            gvMain.DataBind()

            DoQuery()
        End If
    End Sub

    Private Sub DoExecutes()

    End Sub

    Protected Sub gvMain_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMain.RowCommand
        If e.CommandName = "Detail" Then
            Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)

            btnX.Caption = "返回"

            'Dim strCompID As String
            'If UserProfile.SelectCompRoleID = "ALL" Then
            '    strCompID = ddlCompID.SelectedValue
            'Else
            '    strCompID = UserProfile.SelectCompRoleID
            'End If

            'Me.TransferFramePage("~/OM/OM2000.aspx", New ButtonState() {btnX}, _
            '    ddlCompID.ID & "=" & strCompID, _
            '    txtOrganID.ID & "=" & txtOrganID.Text, _
            '    txtOrganName.ID & "=" & txtOrganName.Text, _
            '    txtNameN.ID & "=" & txtNameN.Text, _
            '    txtValidDateBH.ID & txtValidDateBH.Text, _
            '    txtValidDateEH.ID & txtValidDateEH.Text, _
            '    txtBossType.ID & "=" & txtBossType.Text, _
            '    "SelectedCompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
            '    "SelectedOrganID=" & gvMain.DataKeys(selectedRow(gvMain))("OrganID").ToString(), _
            '    "SelectedOrganName=" & gvMain.DataKeys(selectedRow(gvMain))("OrganName").ToString(), _
            '    "SelectedNameN=" & gvMain.DataKeys(selectedRow(gvMain))("NameN").ToString(), _
            '    "SelectedValidDateBH=" & gvMain.DataKeys(selectedRow(gvMain))("ValidDateBH").ToString(), _
            '    "SelectedValidDateEH=" & gvMain.DataKeys(selectedRow(gvMain))("ValidDateEH").ToString(), _
            '    "SelectedBossType=" & gvMain.DataKeys(selectedRow(gvMain))("BossType").ToString(), _
            '    "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))

        End If
    End Sub
    '清除
    Private Sub DoClear()
        ViewState.Item("DoQuery") = ""
        gvMain.Visible = False

        'ddlCompID.SelectedValue = ""
        'txtOrganID.Text = ""
        'txtOrganName.Text = ""
        'txtNameN.Text = ""
        'txtValidDateBH.Text = ""
        'txtValidDateEH.Text = ""
        'txtBossType.Text = ""
    End Sub
End Class