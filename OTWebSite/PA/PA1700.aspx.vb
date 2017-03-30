'****************************************************
'功能說明：工作性質設定
'建立人員：MickySung
'建立日期：2015.05.05
'****************************************************
Imports System.Data

Partial Class PA_PA1700
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objPA As New PA1()
        Dim objSC As New SC()

        If Not IsPostBack Then
            '公司代碼
            If UserProfile.SelectCompRoleID = "ALL" Then
                lblCompRoleID.Visible = False
                Bsp.Utility.FillHRCompany(ddlCompID)
                Page.SetFocus(ddlCompID)

                '工作性質代碼
                '2015/05/18 規格變更:工作性質代碼 須隨公司代碼連動
                PA1.FillWorkTypeID_PA1700(ddlWorkTypeID, ddlCompID.SelectedValue)
                ddlWorkTypeID.Items.Insert(0, New ListItem("---請選擇---", ""))

                '無效註記
                '2015/05/18 規格變更:無效註記 須隨公司代碼連動
                PA1.FillInValidFlag_PA1700(ddlInValidFlag, ddlCompID.SelectedValue)
                ddlInValidFlag.Items.Insert(0, New ListItem("---請選擇---", ""))

                'PB類
                '2015/05/18 規格變更:PB類 須隨公司代碼連動
                PA1.FillPBFlag_PA1700(ddlPBFlag, ddlCompID.SelectedValue)
                ddlPBFlag.Items.Insert(0, New ListItem("---請選擇---", ""))

                'AO/OP
                '2015/05/18 規格變更:AO/OP 須隨公司代碼連動
                PA1.FillAOFlag_PA1700(ddlAOFlag, ddlCompID.SelectedValue)
                ddlAOFlag.Items.Insert(0, New ListItem("---請選擇---", ""))

                '部門列印註記
                '2015/05/18 規格變更:部門列印註記 須隨公司代碼連動
                PA1.FillOrganPrintFlag_PA1700(ddlOrganPrintFlag, ddlCompID.SelectedValue)
                ddlOrganPrintFlag.Items.Insert(0, New ListItem("---請選擇---", ""))

                '工作性質類別
                '2015/05/18 規格變更:工作性質類別 須隨公司代碼連動
                PA1.FillClass_PA1700(ddlClass, ddlCompID.SelectedValue)
                ddlClass.Items.Insert(0, New ListItem("---請選擇---", ""))
            Else
                '2015/05/28 公司代碼-名稱改寫法
                lblCompRoleID.Text = UserProfile.SelectCompRoleName
                'lblCompRoleID.Text = UserProfile.SelectCompRoleID + "-" + objSC.GetCompName(UserProfile.SelectCompRoleID).Rows(0).Item("CompName").ToString
                ddlCompID.Visible = False

                '工作性質代碼
                '2015/05/18 規格變更:工作性質代碼 須隨公司代碼連動
                PA1.FillWorkTypeID_PA1700(ddlWorkTypeID, UserProfile.SelectCompRoleID)
                ddlWorkTypeID.Items.Insert(0, New ListItem("---請選擇---", ""))

                '無效註記
                '2015/05/18 規格變更:無效註記 須隨公司代碼連動
                PA1.FillInValidFlag_PA1700(ddlInValidFlag, UserProfile.SelectCompRoleID)
                ddlInValidFlag.Items.Insert(0, New ListItem("---請選擇---", ""))

                'PB類
                '2015/05/18 規格變更:PB類 須隨公司代碼連動
                PA1.FillPBFlag_PA1700(ddlPBFlag, UserProfile.SelectCompRoleID)
                ddlPBFlag.Items.Insert(0, New ListItem("---請選擇---", ""))

                'AO/OP
                '2015/05/18 規格變更:AO/OP 須隨公司代碼連動
                PA1.FillAOFlag_PA1700(ddlAOFlag, UserProfile.SelectCompRoleID)
                ddlAOFlag.Items.Insert(0, New ListItem("---請選擇---", ""))

                '部門列印註記
                '2015/05/18 規格變更:部門列印註記 須隨公司代碼連動
                PA1.FillOrganPrintFlag_PA1700(ddlOrganPrintFlag, UserProfile.SelectCompRoleID)
                ddlOrganPrintFlag.Items.Insert(0, New ListItem("---請選擇---", ""))

                '工作性質類別
                '2015/05/18 規格變更:工作性質類別 須隨公司代碼連動
                PA1.FillClass_PA1700(ddlClass, UserProfile.SelectCompRoleID)
                ddlClass.Items.Insert(0, New ListItem("---請選擇---", ""))
            End If

            '大類
            PA1.FillCategoryI_PA1700(ddlCategoryI)
            ddlCategoryI.Items.Insert(0, New ListItem("---請選擇---", ""))

            '中類
            PA1.FillCategoryII_PA1720(ddlCategoryII)
            ddlCategoryII.Items.Insert(0, New ListItem("---請選擇---", ""))

            '細類
            PA1.FillCategoryIII_PA1730(ddlCategoryIII)
            ddlCategoryIII.Items.Insert(0, New ListItem("---請選擇---", ""))

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
            
            '中類
            If ddlCategoryI.SelectedValue <> "" Then
                PA1.FillCategoryII_PA1700(ddlCategoryII, ddlCategoryI.SelectedValue)
                ddlCategoryII.Items.Insert(0, New ListItem("---請選擇---", ""))
                UpdCategoryII.Update()
            Else
                PA1.FillCategoryII_PA1720(ddlCategoryII)
                ddlCategoryII.Items.Insert(0, New ListItem("---請選擇---", ""))
                UpdCategoryII.Update()
            End If

            '細類
            If ddlCategoryII.SelectedValue <> "" Then
                PA1.FillCategoryIII_PA1700(ddlCategoryIII, ddlCategoryI.SelectedValue, ddlCategoryII.SelectedValue)
                ddlCategoryIII.Items.Insert(0, New ListItem("---請選擇---", ""))
                UpdCategoryIII.Update()
            Else
                PA1.FillCategoryIII_PA1730(ddlCategoryII)
                ddlCategoryII.Items.Insert(0, New ListItem("---請選擇---", ""))
                UpdCategoryII.Update()
            End If

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

        Me.TransferFramePage("~/PA/PA1701.aspx", New ButtonState() {btnA, btnX, btnC}, _
            ddlCompID.ID & "=" & strCompID, _
            ddlWorkTypeID.ID & "=" & ddlWorkTypeID.SelectedValue, _
            txtRemark.ID & "=" & txtRemark.Text, _
            ddlInValidFlag.ID & "=" & ddlInValidFlag.SelectedValue, _
            ddlPBFlag.ID & "=" & ddlPBFlag.SelectedValue, _
            ddlAOFlag.ID & "=" & ddlAOFlag.SelectedValue, _
            txtSortOrder.ID & "=" & txtSortOrder.Text, _
            ddlOrganPrintFlag.ID & "=" & ddlOrganPrintFlag.SelectedValue, _
            ddlClass.ID & "=" & ddlClass.SelectedValue, _
            ddlCategoryI.ID & "=" & ddlCategoryI.SelectedValue, _
            ddlCategoryII.ID & "=" & ddlCategoryII.SelectedValue, _
            ddlCategoryIII.ID & "=" & ddlCategoryIII.SelectedValue, _
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

        Me.TransferFramePage("~/PA/PA1702.aspx", New ButtonState() {btnU, btnX, btnC}, _
            ddlCompID.ID & "=" & strCompID, _
            ddlWorkTypeID.ID & "=" & ddlWorkTypeID.SelectedValue, _
            txtRemark.ID & "=" & txtRemark.Text, _
            ddlInValidFlag.ID & "=" & ddlInValidFlag.SelectedValue, _
            ddlPBFlag.ID & "=" & ddlPBFlag.SelectedValue, _
            ddlAOFlag.ID & "=" & ddlAOFlag.SelectedValue, _
            txtSortOrder.ID & "=" & txtSortOrder.Text, _
            ddlOrganPrintFlag.ID & "=" & ddlOrganPrintFlag.SelectedValue, _
            ddlClass.ID & "=" & ddlClass.SelectedValue, _
            ddlCategoryI.ID & "=" & ddlCategoryI.SelectedValue, _
            ddlCategoryII.ID & "=" & ddlCategoryII.SelectedValue, _
            ddlCategoryIII.ID & "=" & ddlCategoryIII.SelectedValue, _
            "PageNo=" & pcMain.PageNo.ToString(), _
            "SysID=" & arySysID(0), _
            "SelectedWorkTypeID=" & gvMain.DataKeys(selectedRow(gvMain))("WorkTypeID").ToString(), _
            "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
    End Sub

    Private Sub DoQuery()
        Dim objPA As New PA1()
        gvMain.Visible = True
        IsDoQuery.Value = "Y"

        Dim strCompID As String
        If UserProfile.SelectCompRoleID = "ALL" Then
            strCompID = ddlCompID.SelectedValue
        Else
            strCompID = UserProfile.SelectCompRoleID
        End If

        Try
            pcMain.DataTable = objPA.WorkTypeSettingQuery(
                "CompID=" & strCompID, _
                "WorkTypeID=" & ddlWorkTypeID.SelectedValue, _
                "Remark=" & txtRemark.Text, _
                "InValidFlag=" & ddlInValidFlag.SelectedValue, _
                "PBFlag=" & ddlPBFlag.SelectedValue, _
                "AOFlag=" & ddlAOFlag.SelectedValue, _
                "SortOrder=" & txtSortOrder.Text, _
                "OrganPrintFlag=" & ddlOrganPrintFlag.SelectedValue, _
                "Class=" & ddlClass.SelectedValue, _
                "CategoryI=" & ddlCategoryI.SelectedValue, _
                "CategoryII=" & ddlCategoryII.SelectedValue, _
                "CategoryIII=" & ddlCategoryIII.SelectedValue)

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
        End Try
    End Sub

    Private Sub DoDelete()
        If selectedRow(gvMain) < 0 Then
            '尚未選取資料列！
            Bsp.Utility.ShowFormatMessage(Me, "W_00000")
        Else
            Dim beWorkType As New beWorkType.Row()
            Dim objPA As New PA1()
            Dim strSysID As String

            strSysID = Bsp.Utility.subGetSysID(UserProfile.LoginSysID)
            Dim arySysID() As String = Split(strSysID, "-")

            Dim strCompID As String
            If UserProfile.SelectCompRoleID = "ALL" Then
                strCompID = ddlCompID.SelectedValue
            Else
                strCompID = UserProfile.SelectCompRoleID
            End If

            beWorkType.CompID.Value = strCompID
            beWorkType.WorkTypeID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("WorkTypeID").ToString()

            Try
                objPA.DeleteWorkTypeSetting(beWorkType)
            Catch ex As Exception
                Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoDelete", ex)
            End Try
            gvMain.DataBind()

            DoQuery()

            '2015/05/18 工作性質代碼 重新讀取下拉選單
            PA1.FillWorkTypeID_PA1700(ddlWorkTypeID, UserProfile.SelectCompRoleID)
            ddlWorkTypeID.Items.Insert(0, New ListItem("---請選擇---", ""))
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

            Me.TransferFramePage("~/PA/PA1702.aspx", New ButtonState() {btnX}, _
                ddlCompID.ID & "=" & strCompID, _
                ddlWorkTypeID.ID & "=" & ddlWorkTypeID.SelectedValue, _
                txtRemark.ID & "=" & txtRemark.Text, _
                ddlInValidFlag.ID & "=" & ddlInValidFlag.SelectedValue, _
                ddlPBFlag.ID & "=" & ddlPBFlag.SelectedValue, _
                ddlAOFlag.ID & "=" & ddlAOFlag.SelectedValue, _
                txtSortOrder.ID & "=" & txtSortOrder.Text, _
                ddlOrganPrintFlag.ID & "=" & ddlOrganPrintFlag.SelectedValue, _
                ddlClass.ID & "=" & ddlClass.SelectedValue, _
                ddlCategoryI.ID & "=" & ddlCategoryI.SelectedValue, _
                ddlCategoryII.ID & "=" & ddlCategoryII.SelectedValue, _
                ddlCategoryIII.ID & "=" & ddlCategoryIII.SelectedValue, _
                "PageNo=" & pcMain.PageNo.ToString(), _
                "SelectedWorkTypeID=" & gvMain.DataKeys(selectedRow(gvMain))("WorkTypeID").ToString(), _
                "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))

        End If
    End Sub

    Private Sub DoClear()
        IsDoQuery.Value = ""
        ViewState.Item("DoQuery") = ""
        gvMain.Visible = False

        '公司代碼
        ddlCompID.SelectedValue = ""

        '工作性質代碼
        ddlWorkTypeID.SelectedValue = ""

        '工作性質名稱
        txtRemark.Text = ""

        '無效註記
        ddlInValidFlag.SelectedValue = ""

        'PB類
        ddlPBFlag.SelectedValue = ""

        'AO/OP
        ddlAOFlag.SelectedValue = ""

        '排序
        txtSortOrder.Text = ""

        '部門列印註記
        ddlOrganPrintFlag.SelectedValue = ""

        '工作性質類別
        ddlClass.SelectedValue = ""

        '大類
        PA1.FillCategoryI_PA1700(ddlCategoryI)
        ddlCategoryI.Items.Insert(0, New ListItem("---請選擇---", ""))

        '中類
        PA1.FillCategoryII_PA1720(ddlCategoryII)
        ddlCategoryII.Items.Insert(0, New ListItem("---請選擇---", ""))

        '細類
        PA1.FillCategoryIII_PA1730(ddlCategoryIII)
        ddlCategoryIII.Items.Insert(0, New ListItem("---請選擇---", ""))

    End Sub

    Protected Sub ddlCategoryI_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlCategoryI.SelectedIndexChanged
        If ddlCategoryI.SelectedValue <> "" Then
            PA1.FillCategoryII_PA1700(ddlCategoryII, ddlCategoryI.SelectedItem.Value)
            ddlCategoryII.Items.Insert(0, New ListItem("---請選擇---", ""))
            UpdCategoryII.Update()

            PA1.FillCategoryIII_PA1700(ddlCategoryIII, ddlCategoryI.SelectedItem.Value, ddlCategoryII.SelectedItem.Value)
            ddlCategoryIII.Items.Insert(0, New ListItem("---請選擇---", ""))
            ddlCategoryIII.SelectedIndex = 0
            UpdCategoryIII.Update()
        Else
            PA1.FillCategoryII_PA1720(ddlCategoryII)
            ddlCategoryII.Items.Insert(0, New ListItem("---請選擇---", ""))
            UpdCategoryII.Update()

            PA1.FillCategoryIII_PA1730(ddlCategoryIII)
            ddlCategoryIII.Items.Insert(0, New ListItem("---請選擇---", ""))
            ddlCategoryIII.SelectedIndex = 0
            UpdCategoryIII.Update()
        End If
    End Sub

    Protected Sub ddlCategoryII_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlCategoryII.SelectedIndexChanged
        If ddlCategoryII.SelectedValue <> "" Then
            If ddlCategoryI.SelectedValue <> "" Then
                PA1.FillCategoryIII_PA1700(ddlCategoryIII, ddlCategoryI.SelectedItem.Value, ddlCategoryII.SelectedItem.Value)
                ddlCategoryIII.Items.Insert(0, New ListItem("---請選擇---", ""))
                ddlCategoryIII.SelectedIndex = 0
                UpdCategoryIII.Update()
            Else
                PA1.FillCategoryIII_PA1700(ddlCategoryIII, "", ddlCategoryII.SelectedItem.Value)
                ddlCategoryIII.Items.Insert(0, New ListItem("---請選擇---", ""))
                ddlCategoryIII.SelectedIndex = 0
                UpdCategoryIII.Update()
            End If
        Else
            PA1.FillCategoryIII_PA1730(ddlCategoryIII)
            ddlCategoryIII.Items.Insert(0, New ListItem("---請選擇---", ""))
            ddlCategoryIII.SelectedIndex = 0
            UpdCategoryIII.Update()
        End If
    End Sub

    Protected Sub ddlCompID_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlCompID.SelectedIndexChanged
        '工作性質代碼
        '2015/05/18 規格變更:工作性質代碼 須隨公司代碼連動
        PA1.FillWorkTypeID_PA1700(ddlWorkTypeID, ddlCompID.SelectedValue)
        ddlWorkTypeID.Items.Insert(0, New ListItem("---請選擇---", ""))
        UpdWorkTypeID.Update()

        '無效註記
        '2015/05/18 規格變更:無效註記 須隨公司代碼連動
        PA1.FillInValidFlag_PA1700(ddlInValidFlag, ddlCompID.SelectedValue)
        ddlInValidFlag.Items.Insert(0, New ListItem("---請選擇---", ""))
        UpdInValidFlag.Update()

        'PB類
        '2015/05/18 規格變更:PB類 須隨公司代碼連動
        PA1.FillPBFlag_PA1700(ddlPBFlag, ddlCompID.SelectedValue)
        ddlPBFlag.Items.Insert(0, New ListItem("---請選擇---", ""))
        UpdPBFlag.Update()

        'AO/OP
        '2015/05/18 規格變更:AO/OP 須隨公司代碼連動
        PA1.FillAOFlag_PA1700(ddlAOFlag, ddlCompID.SelectedValue)
        ddlAOFlag.Items.Insert(0, New ListItem("---請選擇---", ""))
        UpdAOFlag.Update()

        '部門列印註記
        '2015/05/18 規格變更:部門列印註記 須隨公司代碼連動
        PA1.FillOrganPrintFlag_PA1700(ddlOrganPrintFlag, ddlCompID.SelectedValue)
        ddlOrganPrintFlag.Items.Insert(0, New ListItem("---請選擇---", ""))
        UpdOrganPrintFlag.Update()

        '工作性質類別
        '2015/05/18 規格變更:工作性質類別 須隨公司代碼連動
        PA1.FillClass_PA1700(ddlClass, ddlCompID.SelectedValue)
        ddlClass.Items.Insert(0, New ListItem("---請選擇---", ""))
        UpdClass.Update()
    End Sub
End Class
