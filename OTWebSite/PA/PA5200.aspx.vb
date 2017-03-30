'****************************************************
'功能說明：功能負責人維護
'建立人員：MickySung
'建立日期：2015.05.25
'****************************************************
Imports System.Data

Partial Class PA_PA5200
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objSC As New SC()

        If Not IsPostBack Then
            '公司代碼
            If UserProfile.SelectCompRoleID = "ALL" Then
                lblCompRoleID.Visible = False
                Bsp.Utility.FillHRCompany(ddlCompID)
                Page.SetFocus(ddlCompID)

                '功能代碼
                PA5.FillDDL(ddlFunctionID, "Maintain a", "a.FunctionID", "f.CodeCName", PA5.DisplayType.Full, "And a.CompID = " & Bsp.Utility.Quote(ddlCompID.SelectedValue), "Y", "left join HRCodeMap as f on f.TabName='Maintain' and f.FldName='FunctionID' and f.Code = a.FunctionID and f.NotShowFlag='0' ")
                ddlFunctionID.Items.Insert(0, New ListItem("---請選擇---", ""))
            Else
                '2015/05/28 公司代碼-名稱改寫法
                lblCompRoleID.Text = UserProfile.SelectCompRoleName
                'lblCompRoleID.Text = UserProfile.SelectCompRoleID + "-" + objSC.GetCompName(UserProfile.SelectCompRoleID).Rows(0).Item("CompName").ToString
                ddlCompID.Visible = False

                '功能代碼
                PA5.FillDDL(ddlFunctionID, "Maintain a", "a.FunctionID", "f.CodeCName", PA5.DisplayType.Full, "And a.CompID = " & Bsp.Utility.Quote(UserProfile.SelectCompRoleID), "Y", "left join HRCodeMap as f on f.TabName='Maintain' and f.FldName='FunctionID' and f.Code = a.FunctionID and f.NotShowFlag='0' ")
                ddlFunctionID.Items.Insert(0, New ListItem("---請選擇---", ""))
            End If
        End If

        '員工編號、員工姓名
        ucQueryEmp.ShowCompRole = "False"
        ucQueryEmp.InValidFlag = "N"

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
                End If
                If TypeOf ctl Is DropDownList Then
                    If ht(strKey).ToString <> "" Then
                        CType(ctl, DropDownList).SelectedValue = ht(strKey).ToString
                    End If
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

        Dim strCompID As String
        If UserProfile.SelectCompRoleID = "ALL" Then
            strCompID = ddlCompID.SelectedValue
        Else
            strCompID = UserProfile.SelectCompRoleID
        End If

        Me.TransferFramePage("~/PA/PA5201.aspx", New ButtonState() {btnA, btnX, btnC}, _
            ddlCompID.ID & "=" & strCompID, _
            ddlFunctionID.ID & "=" & ddlFunctionID.SelectedValue, _
            txtEmpID.ID & "=" & txtEmpID.Text, _
            txtEmpName.ID & "=" & txtEmpName.Text, _
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

        Dim strSysID As String
        strSysID = Bsp.Utility.subGetSysID(UserProfile.LoginSysID)
        Dim arySysID() As String = Split(strSysID, "-")

        Dim strCompID As String
        If UserProfile.SelectCompRoleID = "ALL" Then
            strCompID = ddlCompID.SelectedValue
        Else
            strCompID = UserProfile.SelectCompRoleID
        End If

        Me.TransferFramePage("~/PA/PA5202.aspx", New ButtonState() {btnU, btnX, btnC}, _
            ddlCompID.ID & "=" & strCompID, _
            ddlFunctionID.ID & "=" & ddlFunctionID.SelectedValue, _
            txtEmpID.ID & "=" & txtEmpID.Text, _
            txtEmpName.ID & "=" & txtEmpName.Text, _
            "PageNo=" & pcMain.PageNo.ToString(), _
            "SysID=" & arySysID(0), _
            "SelectedCompID=" & strCompID, _
            "SelectedFunctionID=" & gvMain.DataKeys(selectedRow(gvMain))("FunctionID").ToString(), _
            "SelectedRole=" & gvMain.DataKeys(selectedRow(gvMain))("Role").ToString(), _
            "SelectedEmpComp=" & gvMain.DataKeys(selectedRow(gvMain))("EmpComp").ToString(), _
            "SelectedEmpID=" & gvMain.DataKeys(selectedRow(gvMain))("EmpID").ToString(), _
            "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
    End Sub

    Private Sub DoQuery()
        Dim objPA As New PA5
        gvMain.Visible = True

        Dim strCompID As String
        If UserProfile.SelectCompRoleID = "ALL" Then
            strCompID = ddlCompID.SelectedValue
        Else
            strCompID = UserProfile.SelectCompRoleID
        End If

        Try
            pcMain.DataTable = objPA.MaintainSettingQuery(
                "CompID=" & strCompID, _
                "FunctionID=" & ddlFunctionID.SelectedValue, _
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
            Dim beMaintain As New beMaintain.Row()
            Dim objPA As New PA5

            Dim strCompID As String
            If UserProfile.SelectCompRoleID = "ALL" Then
                strCompID = ddlCompID.SelectedValue
            Else
                strCompID = UserProfile.SelectCompRoleID
            End If

            beMaintain.CompID.Value = strCompID
            beMaintain.FunctionID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("FunctionID").ToString()
            beMaintain.Role.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("Role").ToString()
            beMaintain.EmpComp.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("EmpComp").ToString()
            beMaintain.EmpID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("EmpID").ToString()

            Try
                objPA.DeleteHRCodeMapSetting(beMaintain)
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

            Dim strCompID As String
            If UserProfile.SelectCompRoleID = "ALL" Then
                strCompID = ddlCompID.SelectedValue
            Else
                strCompID = UserProfile.SelectCompRoleID
            End If

            Me.TransferFramePage("~/PA/PA5202.aspx", New ButtonState() {btnX}, _
                ddlCompID.ID & "=" & strCompID, _
                ddlFunctionID.ID & "=" & ddlFunctionID.SelectedValue, _
                txtEmpID.ID & "=" & txtEmpID.Text, _
                txtEmpName.ID & "=" & txtEmpName.Text, _
                "PageNo=" & pcMain.PageNo.ToString(), _
                "SelectedCompID=" & strCompID, _
                "SelectedFunctionID=" & gvMain.DataKeys(selectedRow(gvMain))("FunctionID").ToString(), _
                "SelectedRole=" & gvMain.DataKeys(selectedRow(gvMain))("Role").ToString(), _
                "SelectedEmpComp=" & gvMain.DataKeys(selectedRow(gvMain))("EmpComp").ToString(), _
                "SelectedEmpID=" & gvMain.DataKeys(selectedRow(gvMain))("EmpID").ToString(), _
                "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))

        End If
    End Sub

    Private Sub DoClear()
        IsDoQuery.Value = ""
        ViewState.Item("DoQuery") = ""
        gvMain.Visible = False

        ddlCompID.SelectedValue = ""
        ddlFunctionID.SelectedValue = ""
        txtEmpID.Text = ""
        txtEmpName.Text = ""

    End Sub

    Protected Sub ddlCompID_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlCompID.SelectedIndexChanged
        '功能代碼
        PA5.FillDDL(ddlFunctionID, "Maintain a", "a.FunctionID", "f.CodeCName", PA5.DisplayType.Full, "And a.CompID = " & Bsp.Utility.Quote(ddlCompID.SelectedValue), "Y", "left join HRCodeMap as f on f.TabName='Maintain' and f.FldName='FunctionID' and f.Code = a.FunctionID and f.NotShowFlag='0' ")
        ddlFunctionID.Items.Insert(0, New ListItem("---請選擇---", ""))
        UpdFunctionID.Update()
    End Sub

    Public Overrides Sub DoModalReturn(ByVal returnValue As String)
        Dim strSql As String = ""

        If returnValue <> "" Then
            Dim aryData() As String = returnValue.Split(":")

            Select Case aryData(0)
                Case "ucQueryEmp"
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    '員工編號
                    txtEmpID.Text = aryValue(1)
                    '員工姓名
                    'txtEmpName.Text = aryValue(2)
            End Select
        End If
    End Sub
End Class
