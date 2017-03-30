'****************************************************
'功能說明：公司名稱設定
'建立人員：MickySung
'建立日期：2015.04.10
'****************************************************
Imports System.Data

Partial Class PA_PA1200
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Dim objPA As New PA1()
            Dim objSC As New SC()
            '公司代碼下拉選單
            '2015/05/18 規格變更:公司代碼不綁定左上角公司
            'If UserProfile.SelectCompRoleID = "ALL" Then
            lblCompRoleID.Visible = False
            Bsp.Utility.FillHRCompany(ddlCompID, Bsp.Enums.FullNameType.CodeDefine)
            ddlCompID.Items.Insert(0, New ListItem("---請選擇---", ""))
            'Page.SetFocus(ddlCompID)
            'Else
            '    lblCompRoleID.Text = UserProfile.SelectCompRoleID + "-" + objSC.GetCompName(UserProfile.SelectCompRoleID).Rows(0).Item("CompName").ToString
            '    ddlCompID.Visible = False
            'End If

            '員工資料來源 下拉選單
            PA1.FillEmpSource(ddlEmpSource)
            ddlEmpSource.Items.Insert(0, New ListItem("---請選擇---", ""))
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

        '2015/05/18 規格變更:公司代碼不綁定左上角公司
        Dim strCompID As String
        'If UserProfile.SelectCompRoleID = "ALL" Then
        strCompID = ddlCompID.SelectedValue
        'Else
        'strCompID = UserProfile.SelectCompRoleID
        'End If

        Me.TransferFramePage("~/PA/PA1201.aspx", New ButtonState() {btnA, btnX, btnC}, _
            ddlCompID.ID & "=" & strCompID, _
            txtCompName.ID & "=" & txtCompName.Text, _
            txtCompEngID.ID & "=" & txtCompEngID.Text, _
            txtCompChID.ID & "=" & txtCompChID.Text, _
            ddlInValidFlag.ID & "=" & ddlInValidFlag.SelectedValue, _
            ddlNotShowFlag.ID & "=" & ddlNotShowFlag.SelectedValue, _
            ddlHRISFlag.ID & "=" & ddlHRISFlag.SelectedValue, _
            ddlRankIDMapFlag.ID & "=" & ddlRankIDMapFlag.SelectedValue, _
            ddlNotShowRankID.ID & "=" & ddlNotShowRankID.SelectedValue, _
            ddlNotShowWorkType.ID & "=" & ddlNotShowWorkType.SelectedValue, _
            ddlFeeShareFlag.ID & "=" & ddlFeeShareFlag.SelectedValue, _
            ddlSPHSC1GrpFlag.ID & "=" & ddlSPHSC1GrpFlag.SelectedValue, _
            ddlEmpSource.ID & "=" & ddlEmpSource.SelectedValue, _
            ddlCNFlag.ID & "=" & ddlCNFlag.SelectedValue, _
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

        '2015/05/18 規格變更:公司代碼不綁定左上角公司
        Dim strCompID As String
        'If UserProfile.SelectCompRoleID = "ALL" Then
        strCompID = ddlCompID.SelectedValue
        'Else
        'strCompID = UserProfile.SelectCompRoleID
        'End If

        Me.TransferFramePage("~/PA/PA1202.aspx", New ButtonState() {btnU, btnX, btnC}, _
            ddlCompID.ID & "=" & strCompID, _
            txtCompName.ID & "=" & txtCompName.Text, _
            txtCompEngID.ID & "=" & txtCompEngID.Text, _
            txtCompChID.ID & "=" & txtCompChID.Text, _
            ddlInValidFlag.ID & "=" & ddlInValidFlag.SelectedValue, _
            ddlNotShowFlag.ID & "=" & ddlNotShowFlag.SelectedValue, _
            ddlHRISFlag.ID & "=" & ddlHRISFlag.SelectedValue, _
            ddlRankIDMapFlag.ID & "=" & ddlRankIDMapFlag.SelectedValue, _
            ddlNotShowRankID.ID & "=" & ddlNotShowRankID.SelectedValue, _
            ddlNotShowWorkType.ID & "=" & ddlNotShowWorkType.SelectedValue, _
            ddlFeeShareFlag.ID & "=" & ddlFeeShareFlag.SelectedValue, _
            ddlSPHSC1GrpFlag.ID & "=" & ddlSPHSC1GrpFlag.SelectedValue, _
            ddlEmpSource.ID & "=" & ddlEmpSource.SelectedValue, _
            ddlCNFlag.ID & "=" & ddlCNFlag.SelectedValue, _
            "PageNo=" & pcMain.PageNo.ToString(), _
            "SysID=" & arySysID(0), _
            "SelectedCompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
            "DoQuery=Y")
    End Sub

    Private Sub DoQuery()
        Dim objPA As New PA1()
        gvMain.Visible = True

        '2015/05/18 規格變更:公司代碼不綁定左上角公司
        Dim strCompID As String
        'If UserProfile.SelectCompRoleID = "ALL" Then
        strCompID = ddlCompID.SelectedValue
        'Else
        'strCompID = UserProfile.SelectCompRoleID
        'End If

        Try
            pcMain.DataTable = objPA.GetCompanyNameSetting(
                "CompID=" & strCompID, _
                "CompName=" & txtCompName.Text.Trim(), _
                "CompEngID=" & txtCompEngID.Text.Trim(), _
                "CompChID=" & txtCompChID.Text.Trim(), _
                "InValidFlag=" & ddlInValidFlag.SelectedValue, _
                "NotShowFlag=" & ddlNotShowFlag.SelectedValue, _
                "HRISFlag=" & ddlHRISFlag.SelectedValue, _
                "RankIDMapFlag=" & ddlRankIDMapFlag.SelectedValue, _
                "NotShowRankID=" & ddlNotShowRankID.SelectedValue, _
                "NotShowWorkType=" & ddlNotShowWorkType.SelectedValue, _
                "FeeShareFlag=" & ddlFeeShareFlag.SelectedValue, _
                "SPHSC1GrpFlag=" & ddlSPHSC1GrpFlag.SelectedValue, _
                "EmpSource=" & ddlEmpSource.SelectedValue, _
                "CNFlag=" & ddlCNFlag.SelectedValue)

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
        End Try
    End Sub

    Private Sub DoDelete()
        If selectedRow(gvMain) < 0 Then
            '尚未選取資料列！
            Bsp.Utility.ShowFormatMessage(Me, "W_00000")
        Else
            Dim beCompany As New beCompany.Row()
            Dim beSC_Company As New beSC_Company.Row()
            Dim objPA As New PA1()
            Dim strSysID As String

            strSysID = Bsp.Utility.subGetSysID(UserProfile.LoginSysID)
            Dim arySysID() As String = Split(strSysID, "-")

            'Company
            beCompany.CompID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("CompID").ToString()

            '2015/05/25 SC_Company
            beSC_Company.CompID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("CompID").ToString()

            Dim OrganizationTable As DataTable
            Dim OrganizationFlowTable As DataTable
            Dim SC_UserGroupTable As DataTable
            Dim SC_GroupFunTable As DataTable
            OrganizationTable = objPA.checkOrganization(gvMain.DataKeys(Me.selectedRow(gvMain))("CompID").ToString())
            OrganizationFlowTable = objPA.checkOrganizationFlow(gvMain.DataKeys(Me.selectedRow(gvMain))("CompID").ToString())
            SC_UserGroupTable = objPA.checkSC_UserGroup(gvMain.DataKeys(Me.selectedRow(gvMain))("CompID").ToString())
            SC_GroupFunTable = objPA.checkSC_GroupFun(gvMain.DataKeys(Me.selectedRow(gvMain))("CompID").ToString())

            If OrganizationTable.Rows(0).Item(0).ToString() > 0 Or OrganizationFlowTable.Rows(0).Item(0).ToString() > 0 Then
                Bsp.Utility.ShowMessage(Me, "【該公司含組織資料，無法刪除，請先刪除組織資料】")
            ElseIf SC_UserGroupTable.Rows(0).Item(0).ToString() > 0 Or SC_GroupFunTable.Rows(0).Item(0).ToString() > 0 Then
                Bsp.Utility.ShowMessage(Me, "【該公司已授權非系統管理者，無法刪除，請先刪除非系統管理者權限】")
            Else
                Try
                    objPA.DeleteCompany(beCompany, beSC_Company)
                Catch ex As Exception
                    Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoDelete", ex)
                End Try
                gvMain.DataBind()

                DoQuery()
            End If
        End If
    End Sub

    Protected Sub gvMain_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMain.RowCommand
        If e.CommandName = "Detail" Then
            Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
            btnX.Caption = "返回"

            '2015/05/18 規格變更:公司代碼不綁定左上角公司
            Dim strCompID As String
            'If UserProfile.SelectCompRoleID = "ALL" Then
            strCompID = ddlCompID.SelectedValue
            'Else
            '    strCompID = UserProfile.SelectCompRoleID
            'End If

            Me.TransferFramePage("~/PA/PA1202.aspx", New ButtonState() {btnX}, _
                ddlCompID.ID & "=" & strCompID, _
                txtCompName.ID & "=" & txtCompName.Text, _
                txtCompEngID.ID & "=" & txtCompEngID.Text, _
                txtCompChID.ID & "=" & txtCompChID.Text, _
                ddlInValidFlag.ID & "=" & ddlInValidFlag.SelectedValue, _
                ddlNotShowFlag.ID & "=" & ddlNotShowFlag.SelectedValue, _
                ddlHRISFlag.ID & "=" & ddlHRISFlag.SelectedValue, _
                ddlRankIDMapFlag.ID & "=" & ddlRankIDMapFlag.SelectedValue, _
                ddlNotShowRankID.ID & "=" & ddlNotShowRankID.SelectedValue, _
                ddlNotShowWorkType.ID & "=" & ddlNotShowWorkType.SelectedValue, _
                ddlFeeShareFlag.ID & "=" & ddlFeeShareFlag.SelectedValue, _
                ddlSPHSC1GrpFlag.ID & "=" & ddlSPHSC1GrpFlag.SelectedValue, _
                ddlEmpSource.ID & "=" & ddlEmpSource.SelectedValue, _
                ddlCNFlag.ID & "=" & ddlCNFlag.SelectedValue, _
                "PageNo=" & pcMain.PageNo.ToString(), _
                "SelectedCompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
                "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))

        End If
    End Sub

    Private Sub DoClear()
        IsDoQuery.Value = ""
        ViewState.Item("DoQuery") = ""
        gvMain.Visible = False

        '公司代碼
        ddlCompID.SelectedValue = ""

        '公司名稱
        txtCompName.Text = ""

        '英文名稱
        txtCompEngID.Text = ""

        '中文名稱
        txtCompChID.Text = ""

        '無效註記
        ddlInValidFlag.SelectedValue = ""

        '不顯示註記
        ddlNotShowFlag.SelectedValue = ""

        '資料轉入HRISDB
        ddlHRISFlag.SelectedValue = ""

        '導入惠悅
        ddlRankIDMapFlag.SelectedValue = ""

        '不顯示職等
        ddlNotShowRankID.SelectedValue = ""

        '不顯示工作性質
        ddlNotShowWorkType.SelectedValue = ""

        '費用分攤註記
        ddlFeeShareFlag.SelectedValue = ""

        '證券團保公司註記
        ddlSPHSC1GrpFlag.SelectedValue = ""

        '員工資料來源
        ddlEmpSource.SelectedValue = ""

        '繁/簡體註記
        ddlCNFlag.SelectedValue = ""
    End Sub

End Class
