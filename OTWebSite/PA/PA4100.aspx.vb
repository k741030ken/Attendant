'****************************************************
'功能說明：Web人員資料查詢設定
'建立人員：BeatriceCheng
'建立日期：2015.08.17
'****************************************************
Imports System.Data

Partial Class PA_PA4100
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objSC As New SC

            ucSelectEmpID.ShowCompRole = False
            ddlCompID.Visible = False

            If UserProfile.SelectCompRoleID = "ALL" Then
                ddlCompID.Visible = True
                Bsp.Utility.FillHRCompany(ddlCompID)
                'ddlCompID.Items.Insert(0, New ListItem("全金控", "0"))
                lblCompRoleID.Visible = False
            Else
                ddlCompID.Visible = False
                lblCompRoleID.Text = UserProfile.SelectCompRoleName
                lblCompRoleID.Visible = True

                ucSelectEmpID.SelectCompID = UserProfile.SelectCompRoleID
            End If

            subLoadVIPDropDownList()
        End If
    End Sub

    Private Sub subLoadVIPDropDownList()
        PA4.FillDDL(ddlUseCompID, "Company", "RTRIM(CompID)", "CompName + case when InValidFlag = '1' then '(無效)' else '' end", PA4.DisplayType.Full, "", "", "Order By InValidFlag, CompID")
        ddlUseCompID.Items.Insert(0, New ListItem("---請選擇---", ""))

        PA4.FillDDL(ddlUseGroupID, "OrganizationFlow", "RTRIM(GroupID)", "OrganName", PA4.DisplayType.Full, "", "And GroupID = OrganID")
        ddlUseGroupID.Items.Insert(0, New ListItem("---請選擇---", ""))

        PA4.FillDDL(ddlUseOrganID, "Organization", "RTRIM(OrganID)", "OrganName + case when InValidFlag = '1' then '(無效)' else '' end", PA4.DisplayType.Full, "", "And OrganID = DeptID And VirtualFlag = '0'", "Order By InValidFlag, OrganID")
        ddlUseOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
    End Sub

    Private Sub subLoadVIPFlowDropDownList()
        PA4.FillDDL(ddlUseCompID, "Company", "RTRIM(CompID)", "CompName + case when InValidFlag = '1' then '(無效)' else '' end", PA4.DisplayType.Full, "", "And CompID = 'SPHBK1'", "Order By InValidFlag, CompID")
        ddlUseCompID.Items.Insert(0, New ListItem("---請選擇---", ""))

        PA4.FillDDL(ddlUseGroupID, "HRCodeMap", "Code", "CodeCName", PA4.DisplayType.Full, "", "And TabName = 'Business' And FldName = 'BusinessType'")
        ddlUseGroupID.Items.Insert(0, New ListItem("---請選擇---", ""))

        PA4.FillDDL(ddlUseOrganID, "OrganizationFlow", "RTRIM(OrganID)", "OrganName + case when InValidFlag = '1' then '(無效)' else '' end", PA4.DisplayType.Full, "", "And BusinessType <> ''", "Order By BusinessType, RoleCode Desc")
        ddlUseOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param

            Case "btnAdd"       '新增VIP
                DoAddVIP()
            Case "btnUpdate"  '查詢VIP結果
                ToVIP()

            Case "btnActionC"     '新增VIPFlow
                DoAddVIPFlow()
            Case "btnExecutes"  '查詢VIPFlow結果
                ToVIPFlow()

            Case "btnDownload"     '查詢
                ViewState.Item("DoQuery") = "Y"
                If rbVIP.Checked = True Then
                    DoQueryVIP()
                Else
                    DoQueryVIPFlow()
                End If
            Case "btnCopy"    '刪除
                DoDelete()
            Case "btnActionX"   '清除
                DoClear()
        End Select
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

            If ht("ddlUseCompID").ToString() <> "" Then
                ddlUseCompID.SelectedValue = ht("ddlUseCompID").ToString()
                ddlUseCompID_SelectedIndexChanged(Nothing, Nothing)
            End If

            If ht("ddlUseGroupID").ToString() <> "" Then
                ddlUseGroupID.SelectedValue = ht("ddlUseGroupID").ToString()
                ddlUseGroupID_SelectedIndexChanged(Nothing, Nothing)
            End If

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
                    DoQueryVIP()
                End If
            End If
        End If
    End Sub

    Private Sub DoAddVIP()
        Dim btnA As New ButtonState(ButtonState.emButtonType.Add)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
        Dim btnC As New ButtonState(ButtonState.emButtonType.Cancel)

        btnA.Caption = "存檔返回"
        btnX.Caption = "返回"
        btnC.Caption = "清除"

        Me.TransferFramePage("~/PA/PA4101.aspx", New ButtonState() {btnA, btnX, btnC}, _
            Bsp.Utility.FormatToParam(txtEmpID), _
            Bsp.Utility.FormatToParam(txtName), _
            "ddlAllCompIDFlag=" & ddlAllCompIDFlag.SelectedValue, _
            "ddlAllGroupIDFlag=" & ddlAllGroupIDFlag.SelectedValue, _
            "ddlAllOrganIDFlag=" & ddlAllOrganIDFlag.SelectedValue, _
            "ddlUseCompID=" & ddlUseCompID.SelectedValue, _
            "ddlUseGroupID=" & ddlUseGroupID.SelectedValue, _
            "ddlUseOrganID=" & ddlUseOrganID.SelectedValue, _
            "ddlGrantFlag=" & ddlGrantFlag.SelectedValue, _
            "PageNo=" & pcMain.PageNo.ToString(), _
            "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
    End Sub

    Private Sub DoAddVIPFlow()
        Dim btnA As New ButtonState(ButtonState.emButtonType.Add)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
        Dim btnC As New ButtonState(ButtonState.emButtonType.Cancel)

        btnA.Caption = "存檔返回"
        btnX.Caption = "返回"
        btnC.Caption = "清除"

        Me.TransferFramePage("~/PA/PA4103.aspx", New ButtonState() {btnA, btnX, btnC}, _
            Bsp.Utility.FormatToParam(txtEmpID), _
            Bsp.Utility.FormatToParam(txtName), _
            "ddlAllCompIDFlag=" & ddlAllCompIDFlag.SelectedValue, _
            "ddlAllGroupIDFlag=" & ddlAllGroupIDFlag.SelectedValue, _
            "ddlAllOrganIDFlag=" & ddlAllOrganIDFlag.SelectedValue, _
            "ddlUseCompID=" & ddlUseCompID.SelectedValue, _
            "ddlUseGroupID=" & ddlUseGroupID.SelectedValue, _
            "ddlUseOrganID=" & ddlUseOrganID.SelectedValue, _
            "ddlGrantFlag=" & ddlGrantFlag.SelectedValue, _
            "PageNo=" & pcMain.PageNo.ToString(), _
            "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
    End Sub

    Private Sub DoQueryVIP()
        Dim objPA4 As New PA4()

        Dim strCompID As String = ddlCompID.SelectedValue
        If strCompID = "" Then
            strCompID = UserProfile.SelectCompRoleID
        End If

        Try
            gvMain.Columns(10).HeaderText = "查詢權限事業群"
            gvMain.Columns(13).Visible = True

            pcMain.DataTable = objPA4.VIPPQuery(
                "CompID=" & strCompID, _
                "EmpID=" & txtEmpID.Text.ToUpper, _
                "Name=" & txtName.Text, _
                "AllCompIDFlag=" & ddlAllCompIDFlag.SelectedValue, _
                "AllGroupIDFlag=" & ddlAllGroupIDFlag.SelectedValue, _
                "AllOrganIDFlag=" & ddlAllOrganIDFlag.SelectedValue, _
                "UseCompID=" & ddlUseCompID.SelectedValue, _
                "UseGroupID=" & ddlUseGroupID.SelectedValue, _
                "UseOrganID=" & ddlUseOrganID.SelectedValue, _
                "GrantFlag=" & ddlGrantFlag.SelectedValue)

            gvMain.Visible = True
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
        End Try
    End Sub

    Private Sub DoQueryVIPFlow()
        Dim objPA4 As New PA4()

        Dim strCompID As String = ddlCompID.SelectedValue
        If strCompID = "" Then
            strCompID = UserProfile.SelectCompRoleID
        End If

        Try
            gvMain.Columns(10).HeaderText = "查詢權限業務別"
            gvMain.Columns(13).Visible = False

            pcMain.DataTable = objPA4.VIPPFlowQuery(
                "CompID=" & strCompID, _
                "EmpID=" & txtEmpID.Text.ToUpper, _
                "Name=" & txtName.Text, _
                "AllBusinessType=" & ddlAllGroupIDFlag.SelectedValue, _
                "AllOrganIDFlag=" & ddlAllOrganIDFlag.SelectedValue, _
                "UseCompID=" & ddlUseCompID.SelectedValue, _
                "UseBusinessType=" & ddlUseGroupID.SelectedValue, _
                "UseOrganID=" & ddlUseOrganID.SelectedValue, _
                "GrantFlag=" & ddlGrantFlag.SelectedValue)

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
            Release("btnDelete")
        End If
    End Sub

    Private Sub DoClear()
        ViewState.Item("DoQuery") = ""
        ddlCompID.SelectedValue = ""
        txtEmpID.Text = ""
        txtName.Text = ""
        ddlAllCompIDFlag.SelectedIndex = 0
        ddlAllGroupIDFlag.SelectedIndex = 0
        ddlAllOrganIDFlag.SelectedIndex = 0
        ddlGrantFlag.SelectedIndex = 0
        gvMain.Visible = False

        If rbVIP.Checked = True Then
            subLoadVIPDropDownList()
        Else
            subLoadVIPFlowDropDownList()
        End If
    End Sub

    Private Sub ToVIP()
        Dim btnQ As New ButtonState(ButtonState.emButtonType.Query)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
        Dim btnC As New ButtonState(ButtonState.emButtonType.Cancel)

        btnQ.Caption = "查詢"
        btnX.Caption = "返回"
        btnC.Caption = "清除"

        Me.TransferFramePage("~/PA/PA4102.aspx", New ButtonState() {btnQ, btnX, btnC}, _
            Bsp.Utility.FormatToParam(txtEmpID), _
            Bsp.Utility.FormatToParam(txtName), _
            "ddlAllCompIDFlag=" & ddlAllCompIDFlag.SelectedValue, _
            "ddlAllGroupIDFlag=" & ddlAllGroupIDFlag.SelectedValue, _
            "ddlAllOrganIDFlag=" & ddlAllOrganIDFlag.SelectedValue, _
            "ddlUseCompID=" & ddlUseCompID.SelectedValue, _
            "ddlUseGroupID=" & ddlUseGroupID.SelectedValue, _
            "ddlUseOrganID=" & ddlUseOrganID.SelectedValue, _
            "ddlGrantFlag=" & ddlGrantFlag.SelectedValue, _
            "PageNo=" & pcMain.PageNo.ToString(), _
            "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
    End Sub

    Private Sub ToVIPFlow()
        Dim btnQ As New ButtonState(ButtonState.emButtonType.Query)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
        Dim btnC As New ButtonState(ButtonState.emButtonType.Cancel)

        btnQ.Caption = "查詢"
        btnX.Caption = "返回"
        btnC.Caption = "清除"

        Me.TransferFramePage("~/PA/PA4104.aspx", New ButtonState() {btnQ, btnX, btnC}, _
            Bsp.Utility.FormatToParam(txtEmpID), _
            Bsp.Utility.FormatToParam(txtName), _
            "ddlAllCompIDFlag=" & ddlAllCompIDFlag.SelectedValue, _
            "ddlAllGroupIDFlag=" & ddlAllGroupIDFlag.SelectedValue, _
            "ddlAllOrganIDFlag=" & ddlAllOrganIDFlag.SelectedValue, _
            "ddlUseCompID=" & ddlUseCompID.SelectedValue, _
            "ddlUseGroupID=" & ddlUseGroupID.SelectedValue, _
            "ddlUseOrganID=" & ddlUseOrganID.SelectedValue, _
            "ddlGrantFlag=" & ddlGrantFlag.SelectedValue, _
            "PageNo=" & pcMain.PageNo.ToString(), _
            "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
    End Sub

    Private Sub Release(ByVal LogFunction As String)
        ucRelease.ShowCompRole = "True"
        ucRelease.FunID = "PA4100"
        ucRelease.LogFunction = LogFunction
        ucRelease.OpenSelect()
    End Sub

    Public Overrides Sub DoModalReturn(ByVal returnValue As String)
        Dim strSql As String = ""

        If returnValue <> "" Then
            Dim aryData() As String = returnValue.Split(":")

            Select Case aryData(0)
                Case "ucSelectEmpID"
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    txtEmpID.Text = aryValue(1)
                    'txtName.Text = aryValue(2)
                    If UserProfile.SelectCompRoleID = "ALL" Then
                        ddlCompID.SelectedValue = aryValue(0)
                    End If

                Case "ucRelease"
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    If aryValue(0) = "Y" Then
                        Try
                            If rbVIP.Checked = True Then
                                Dim beVIPParameter As New beVIPParameter.Row()
                                Dim objPA4 As New PA4

                                beVIPParameter.CompID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("CompID").ToString()
                                beVIPParameter.EmpID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("EmpID").ToString()
                                beVIPParameter.GrantFlag.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("GrantFlag").ToString()
                                beVIPParameter.UseCompID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("UseCompID").ToString()
                                beVIPParameter.UseGroupID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("UseGroupID").ToString()
                                beVIPParameter.UseOrganID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("UseOrganID").ToString()

                                objPA4.VIPDelete(beVIPParameter)
                                DoQueryVIP()
                            Else
                                Dim beVIPParameterFlow As New beVIPParameterFlow.Row()
                                Dim objPA4 As New PA4

                                beVIPParameterFlow.CompID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("CompID").ToString()
                                beVIPParameterFlow.EmpID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("EmpID").ToString()
                                beVIPParameterFlow.GrantFlag.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("GrantFlag").ToString()
                                beVIPParameterFlow.UseCompID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("UseCompID").ToString()
                                beVIPParameterFlow.UseBusinessType.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("UseGroupID").ToString()
                                beVIPParameterFlow.UseOrganID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("UseOrganID").ToString()

                                objPA4.VIPFlowDelete(beVIPParameterFlow)
                                DoQueryVIPFlow()
                            End If
                        Catch ex As Exception
                            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoDelete", ex)
                        End Try
                    End If
            End Select
        End If
    End Sub

    '排除授權
    Protected Sub rbVIP_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbVIP.CheckedChanged
        If rbVIP.Checked = True Then
            lblAllGroupIDFlag.Text = "事業群全選註記："
            lblUseGroupID.Text = "授權事業群："
            ddlAllCompIDFlag.Enabled = True
            DoClear()
        End If
    End Sub

    '授權
    Protected Sub rbVIPFlow_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbVIPFlow.CheckedChanged
        If rbVIPFlow.Checked = True Then
            lblAllGroupIDFlag.Text = "業務別全選註記："
            lblUseGroupID.Text = "授權業務別："
            ddlAllCompIDFlag.Enabled = False
            DoClear()
        End If
    End Sub

    Protected Sub gvMain_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvMain.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim UseOurColleagues As String() = DataBinder.Eval(e.Row.DataItem, "UseOurColleagues").ToString.Split("|")

            If UseOurColleagues.Length > 0 Then
                Dim strColleague As String = ""
                For Each item In UseOurColleagues
                    Select Case item
                        Case "01"
                            strColleague = strColleague & "、基本資料"
                        Case "02"
                            strColleague = strColleague & "、進階資料"
                        Case "03"
                            strColleague = strColleague & "、學歷資料"
                        Case "04"
                            strColleague = strColleague & "、前職經歷"
                        Case "05"
                            strColleague = strColleague & "、家庭狀況"
                        Case "06"
                            strColleague = strColleague & "、企業團經歷"
                        Case "07"
                            strColleague = strColleague & "、證照"
                        Case "08"
                            strColleague = strColleague & "、訓練紀錄"
                    End Select
                Next
                If strColleague.Length > 0 Then
                    e.Row.Cells(14).Text = strColleague.Substring(1)
                End If
            End If
        End If
    End Sub

    Protected Sub ddlUseCompID_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlUseCompID.SelectedIndexChanged
        If rbVIP.Checked = True Then
            If ddlUseCompID.SelectedValue <> "" Then
                PA4.FillDDL(ddlUseGroupID, "OrganizationFlow", "RTRIM(GroupID)", "OrganName", PA4.DisplayType.Full, "", "And GroupID = OrganID And GroupID In (Select Distinct GroupID From Organization Where CompID = " & Bsp.Utility.Quote(ddlUseCompID.SelectedValue) & ")")
                ddlUseGroupID.Items.Insert(0, New ListItem("---請選擇---", ""))

                PA4.FillDDL(ddlUseOrganID, "Organization", "RTRIM(OrganID)", "OrganName + case when InValidFlag = '1' then '(無效)' else '' end", PA4.DisplayType.Full, "", "And OrganID = DeptID And VirtualFlag = '0' And CompID = " & Bsp.Utility.Quote(ddlUseCompID.SelectedValue), "Order By InValidFlag, OrganID")
                ddlUseOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
            Else
                PA4.FillDDL(ddlUseGroupID, "OrganizationFlow", "RTRIM(GroupID)", "OrganName", PA4.DisplayType.Full, "", "And GroupID = OrganID")
                ddlUseGroupID.Items.Insert(0, New ListItem("---請選擇---", ""))

                PA4.FillDDL(ddlUseOrganID, "Organization", "RTRIM(OrganID)", "OrganName + case when InValidFlag = '1' then '(無效)' else '' end", PA4.DisplayType.Full, "", "And OrganID = DeptID And VirtualFlag = '0'", "Order By InValidFlag, OrganID")
                ddlUseOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
            End If
        Else
            If ddlUseCompID.SelectedValue <> "" Then
                PA4.FillDDL(ddlUseGroupID, "HRCodeMap", "Code", "CodeCName", PA4.DisplayType.Full, "", "And TabName = 'Business' And FldName = 'BusinessType'")
                ddlUseGroupID.Items.Insert(0, New ListItem("---請選擇---", ""))

                PA4.FillDDL(ddlUseOrganID, "OrganizationFlow", "RTRIM(OrganID)", "OrganName + case when InValidFlag = '1' then '(無效)' else '' end", PA4.DisplayType.Full, "", "And BusinessType <> '' And CompID = " & Bsp.Utility.Quote(ddlUseCompID.SelectedValue), "Order By BusinessType, RoleCode Desc")
                ddlUseOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
            Else
                PA4.FillDDL(ddlUseGroupID, "HRCodeMap", "Code", "CodeCName", PA4.DisplayType.Full, "", "And TabName = 'Business' And FldName = 'BusinessType'")
                ddlUseGroupID.Items.Insert(0, New ListItem("---請選擇---", ""))

                PA4.FillDDL(ddlUseOrganID, "OrganizationFlow", "RTRIM(OrganID)", "OrganName + case when InValidFlag = '1' then '(無效)' else '' end", PA4.DisplayType.Full, "", "And BusinessType <> ''", "Order By BusinessType, RoleCode Desc")
                ddlUseOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
            End If
        End If
    End Sub

    Protected Sub ddlUseGroupID_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlUseGroupID.SelectedIndexChanged
        If rbVIP.Checked = True Then
            If ddlUseCompID.SelectedValue <> "" Then
                If ddlUseGroupID.SelectedValue <> "" Then
                    PA4.FillDDL(ddlUseOrganID, "Organization", "RTRIM(OrganID)", "OrganName + case when InValidFlag = '1' then '(無效)' else '' end", PA4.DisplayType.Full, "", "And OrganID = DeptID And VirtualFlag = '0' And CompID = " & Bsp.Utility.Quote(ddlUseCompID.SelectedValue) & _
                                " And GroupID In (Select GroupID From OrganizationFlow Where OrganID = GroupID And GroupID In ( Select Distinct GroupID From Organization Where CompID = " & Bsp.Utility.Quote(ddlUseCompID.SelectedValue) & " )) And GroupID = " & Bsp.Utility.Quote(ddlUseGroupID.SelectedValue), "Order By InValidFlag, OrganID")
                    ddlUseOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
                Else
                    PA4.FillDDL(ddlUseOrganID, "Organization", "RTRIM(OrganID)", "OrganName + case when InValidFlag = '1' then '(無效)' else '' end", PA4.DisplayType.Full, "", "And OrganID = DeptID And VirtualFlag = '0' And CompID = " & Bsp.Utility.Quote(ddlUseCompID.SelectedValue), "Order By InValidFlag, OrganID")
                    ddlUseOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
                End If
            Else
                PA4.FillDDL(ddlUseOrganID, "Organization", "RTRIM(OrganID)", "OrganName + case when InValidFlag = '1' then '(無效)' else '' end", PA4.DisplayType.Full, "", "And OrganID = DeptID And VirtualFlag = '0'", "Order By InValidFlag, OrganID")
                ddlUseOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
            End If
        Else
            If ddlUseCompID.SelectedValue <> "" Then
                If ddlUseGroupID.SelectedValue <> "" Then
                    PA4.FillDDL(ddlUseOrganID, "OrganizationFlow", "RTRIM(OrganID)", "OrganName + case when InValidFlag = '1' then '(無效)' else '' end", PA4.DisplayType.Full, "", "And CompID = " & Bsp.Utility.Quote(ddlUseCompID.SelectedValue) & " And BusinessType = " & Bsp.Utility.Quote(ddlUseGroupID.SelectedValue), "Order By BusinessType, RoleCode Desc")
                    ddlUseOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
                Else
                    PA4.FillDDL(ddlUseOrganID, "OrganizationFlow", "RTRIM(OrganID)", "OrganName + case when InValidFlag = '1' then '(無效)' else '' end", PA4.DisplayType.Full, "", "And BusinessType <> '' And CompID = " & Bsp.Utility.Quote(ddlUseCompID.SelectedValue), "Order By BusinessType, RoleCode Desc")
                    ddlUseOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
                End If
            Else
                PA4.FillDDL(ddlUseOrganID, "OrganizationFlow", "RTRIM(OrganID)", "OrganName + case when InValidFlag = '1' then '(無效)' else '' end", PA4.DisplayType.Full, "", "And BusinessType <> ''", "Order By BusinessType, RoleCode Desc")
                ddlUseOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
            End If
        End If
    End Sub

    Protected Sub txtEmpID_TextChanged(sender As Object, e As System.EventArgs) Handles txtEmpID.TextChanged

    End Sub
End Class
