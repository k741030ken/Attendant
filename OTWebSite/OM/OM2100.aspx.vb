'****************************************************
'功能說明：組織異動維護查詢功能
'建立人員：Rebecca Yan
'建立日期：2016.09.24
'****************************************************
Imports System.Data

Partial Class OM_OM2100
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            ucSecPersonPart.ShowCompRole = "False"
            ucSecPersonPart.InValidFlag = "N"
            'ucSecPersonPart.SelectCompID = UserProfile.SelectCompRoleID
            ucPersonPart.ShowCompRole = "False"
            ucPersonPart.InValidFlag = "N"
            'ucPersonPart.SelectCompID = UserProfile.SelectCompRoleID
            ucCheckPart.ShowCompRole = "False"
            ucCheckPart.InValidFlag = "N"
            'ucCheckPart.SelectCompID = UserProfile.SelectCompRoleID
            hidOrganReason.Value = "3"
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
            If ht("SelectedValidDateB") Is Nothing Then
                ViewState.Item("ValidDateB") = "1900/01/01" '單位有效起日
            Else
                ViewState.Item("ValidDateB") = ht("SelectedValidDateB").ToString()
            End If
            txtCompID.Text = UserProfile.SelectCompRoleName

            txtOrganType.Text = ViewState.Item("OrganType").ToString()

            txtOrganID.Text = ViewState.Item("OrganID").ToString()
            txtOrganName.Text = ViewState.Item("OrganName").ToString()
            hidCompID.Value = ViewState.Item("CompID").ToString
            hidOrganID.Value = ViewState.Item("OrganID")
            Dim array() As String = ViewState.Item("OrganType").Split("-")
            hidOrganType.Value = array(0)

            GetSelectData(ViewState.Item("CompID").ToString, ViewState.Item("OrganID").ToString)
            '動態隱藏「行政組織或功能組織」次要欄位
            If txtOrganType.Text = "1-行政組織" Then
                Panel2.Visible = False
            ElseIf txtOrganType.Text = "2-功能組織" Then
                Panel1.Visible = False
            End If

            '----------行政組織----------
            '載入時預存職位(存進OrganizationLog)
            'hidPositionID_Old.Value = hidPositionID.Value
            '載入時預存工作性質(存進OrganizationLog)
            'hidWorkTypeID_Old.Value = hidWorkTypeID.Value

            '----------功能組織----------
            'hidFlowOrganID_Old.Value
        Else
            Return
        End If
    End Sub

#Region "取得既有資料"
    Private Sub GetSelectData(ByVal CompID As String, ByVal OrganID As String)
        Dim objOM As New OM2()

        Dim strSQL As New StringBuilder()
        txtOrganType.Text = ViewState.Item("OrganType")
        Try
            If txtOrganType.Text = "1-行政組織" Then
                Using dt As DataTable = objOM.subGetData_OM2010Organization(hidCompID.Value, txtOrganID.Text)
                    If dt.Rows().Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            '部門英文名
                            txtOrganEngName.Text = dr.Item("OrganEngName").ToString.Trim

                            '無效註記
                            hidInValidFlag.Value = dr.Item("InValidFlag").ToString.Trim
                            If hidInValidFlag.Value = "1" Then '1-無效
                                chkInValidFlag.Checked = True
                            ElseIf hidInValidFlag.Value = "0" Then '0-有效
                                chkInValidFlag.Checked = False
                            End If

                            '是否為虛擬部門
                            hidVirtualFlag.Value = dr.Item("VirtualFlag").ToString.Trim
                            If hidVirtualFlag.Value = "1" Then '1-虛擬
                                chkVirtualFlag.Checked = True
                            ElseIf hidVirtualFlag.Value = "0" Then '0-一般
                                chkVirtualFlag.Checked = False
                            End If

                            '分行註記
                            hidBranchFlag.Value = dr.Item("BranchFlag").ToString.Trim
                            If hidBranchFlag.Value = "1" Then '1-是
                                chkBranchFlag.Checked = True
                            ElseIf hidBranchFlag.Value = "0" Then '0-否
                                chkBranchFlag.Checked = False
                            End If

                            '單位類別
                            hidOrgType.Value = dr.Item("OrgType").ToString.Trim
                            If dr.Item("OrgType").ToString.Trim = "" Then
                                txtOrgType.Text = ""
                            Else
                                txtOrgType.Text = dr.Item("OrgType").ToString.Trim + "-" + dr.Item("OrgTypeName").ToString.Trim
                            End If

                            '所屬事業群
                            hidGroupID.Value = dr.Item("GroupID").ToString.Trim
                            If dr.Item("GroupID").ToString.Trim = "" Then
                                txtGroupID.Text = ""
                            Else
                                txtGroupID.Text = dr.Item("GroupID").ToString.Trim + "-" + dr.Item("GroupName").ToString.Trim
                            End If

                            '上階部門
                            hidUpOrganID.Value = dr.Item("UpOrganID").ToString.Trim
                            If dr.Item("UpOrganID").ToString.Trim = "" Then
                                txtUpOrganID.Text = ""
                            Else
                                txtUpOrganID.Text = dr.Item("UpOrganID").ToString.Trim + "-" + dr.Item("UpOrganName").ToString.Trim
                            End If

                            '事業群類別
                            hidGroupType.Value = dr.Item("GroupType").ToString.Trim
                            If dr.Item("GroupType").ToString.Trim = "" Then
                                txtGroupType.Text = ""
                            Else
                                txtGroupType.Text = dr.Item("GroupType").ToString.Trim + "-" + dr.Item("GroupTypeName").ToString.Trim
                            End If

                            '所屬一級部門
                            hidDeptID.Value = dr.Item("DeptID").ToString.Trim
                            If dr.Item("DeptID").ToString.Trim = "" Then
                                txtDeptID.Text = ""
                            Else
                                txtDeptID.Text = dr.Item("DeptID").ToString.Trim + "-" + dr.Item("DeptName").ToString.Trim
                            End If

                            '部門主管角色
                            hidRoleCode.Value = dr.Item("RoleCode").ToString.Trim
                            If dr.Item("RoleCode").ToString.Trim = "" Then
                                txtRoleCode.Text = ""
                            Else
                                txtRoleCode.Text = dr.Item("RoleCode").ToString.Trim + "-" + dr.Item("RoleCodeName").ToString.Trim
                            End If

                            '部門主管
                            hidBossCompID.Value = dr.Item("BossCompID").ToString.Trim
                            hidBoss.Value = dr.Item("Boss").ToString.Trim

                            If dr.Item("Boss").ToString.Trim = "" Then
                                txtBoss.Text = ""
                            Else
                                txtBoss.Text = dr.Item("BossCompID").ToString.Trim + "-" + dr.Item("BossCompName").ToString.Trim + "-" + dr.Item("Boss").ToString.Trim + "-" + dr.Item("BossName").ToString.Trim
                            End If

                            '主管任用方式
                            hidBossType.Value = dr.Item("BossType").ToString.Trim

                            txtBossType.Text = dr.Item("BossType").ToString.Trim
                            If txtBossType.Text = "1" Then
                                txtBossType.Text = "1-主要"
                            ElseIf txtBossType.Text = "2" Then
                                txtBossType.Text = "2-兼任"
                            End If

                            '副主管
                            hidSecBossCompID.Value = dr.Item("SecBossCompID").ToString.Trim
                            hidSecBoss.Value = dr.Item("SecBoss").ToString.Trim

                            If dr.Item("SecBossCompID").ToString.Trim = "" Then
                                txtSecBoss.Text = ""
                            Else
                                txtSecBoss.Text = dr.Item("SecBossCompID").ToString.Trim + "-" + dr.Item("SecBossCompName").ToString.Trim + "-" + dr.Item("SecBoss").ToString.Trim + "-" + dr.Item("SecBossName").ToString.Trim
                            End If

                            '主管暫代
                            hidBossTemporary.Value = dr.Item("BossTemporary").ToString.Trim
                            If hidBossTemporary.Value = "1" Then '1-是
                                chkBossTemporary.Checked = True
                            ElseIf hidBossTemporary.Value = "0" Then '0-否
                                chkBossTemporary.Checked = False
                            End If

                            '------------------------------實體組織START------------------------------'
                            '第一人事管理員(員編+姓名)
                            hidPersonPart_Old.Value = dr.Item("PersonPart").ToString.Trim '異動前
                            txtPersonPart.Text = dr.Item("PersonPart").ToString.Trim
                            lblPersonPartName.Text = dr.Item("PersonPartName").ToString.Trim()

                            '第二人事管理員(員編+姓名)
                            hidSecPersonPart_Old.Value = dr.Item("SecPersonPart").ToString.Trim '異動前
                            txtSecPersonPart.Text = dr.Item("SecPersonPart").ToString.Trim
                            lblSecPersonPartName.Text = dr.Item("SecPersonPartName").ToString.Trim()

                            '工作地點
                            OM2.FillDDL(ddlWorkSiteID, "WorkSite", "RTRIM(WorkSiteID)", "Remark", OM2.DisplayType.Full, "", "And CompID = " + Bsp.Utility.Quote(UserProfile.SelectCompRoleID), "ORDER BY WorkSiteID")
                            ddlWorkSiteID.Items.Insert(0, New ListItem("---請選擇---", ""))
                            ddlWorkSiteID.SelectedValue = dt.Rows(0).Item("WorkSiteID").ToString.Trim
                            hidWorkSiteID_Old.Value = ddlWorkSiteID.SelectedValue '異動前

                            '自行查核主管
                            hidCheckPart_Old.Value = dr.Item("CheckPart").ToString.Trim '異動前
                            txtCheckPart.Text = dr.Item("CheckPart").ToString.Trim
                            lblCheckPartName.Text = dr.Item("CheckPartName").ToString.Trim

                            '職位
                            OM2.FillDDL(ddlPositionID, " OrgPosition OrgP", " OrgP.PositionID ", " P.Remark ", OM2.DisplayType.Full, _
               " INNER JOIN Position P ON OrgP.CompID = P.CompID and OrgP.PositionID = P.PositionID ", _
               " AND OrgP.CompID = '" & hidCompID.Value & "' AND OrgP.OrganID = '" & txtOrganID.Text & "' ", " order by PrincipalFlag desc , P.PositionID ")
                            For i = 0 To ddlPositionID.Items.Count - 1
                                If i = 0 Then
                                    lblSelectPositionID.Text = "'" + ddlPositionID.Items(i).Value + "'"
                                    hidPositionID_Old.Value = "1|" + ddlPositionID.Items(i).Value
                                Else
                                    lblSelectPositionID.Text = lblSelectPositionID.Text + ",'" + ddlPositionID.Items(i).Value + "'"
                                    hidPositionID_Old.Value = hidPositionID_Old.Value + "|0|" + ddlPositionID.Items(i).Value
                                End If
                            Next
                            hidPositionID.Value = hidPositionID_Old.Value '異動前

                            '費用分攤部門
                            OM2.getUnionOrderBy(ddlCostDeptID, _
                            " (select OrganID,OrganName,InValidFlag,VirtualFlag from Organization where CompID = " + Bsp.Utility.Quote(UserProfile.SelectCompRoleID) + "	and VirtualFlag <> '1') as T ", " OrganID ", " OrganName ", " '1' as number,InValidFlag,VirtualFlag ", OM2.DisplayType.Full, " union select CompID AS Code, CompName AS CodeName,CompID+'-'+CompName AS FullName , '2' as number, ' ' as InValidFlag, ' ' as VirtualFlag from(select CompID,CompName from Company where FeeShareFlag ='1') as S ", "", " order by number,InValidFlag,VirtualFlag,Code ")
                            ddlCostDeptID.Items.Insert(0, New ListItem("---請選擇---", ""))
                            ddlCostDeptID.SelectedValue = dt.Rows(0).Item("CostDeptID").ToString.Trim
                            hidCostDeptID_Old.Value = ddlCostDeptID.SelectedValue '異動前

                            '工作性質
                            OM2.FillDDL(ddlWorkTypeID, " OrgWorkType  E", " E.WorkTypeID ", " W.Remark ", OM2.DisplayType.Full, _
              " inner join WorkType W on E.CompID = W.CompID and E.WorkTypeID  = W.WorkTypeID  ", _
              " and E.CompID = '" & hidCompID.Value & "' and E.OrganID = '" & txtOrganID.Text & "' ", " order by PrincipalFlag desc , W.WorkTypeID  ")
                            For i = 0 To ddlWorkTypeID.Items.Count - 1
                                If i = 0 Then
                                    lblSelectWorkTypeID.Text = "'" + ddlWorkTypeID.Items(i).Value + "'"
                                    hidWorkTypeID_Old.Value = "1|" + ddlWorkTypeID.Items(i).Value
                                Else
                                    lblSelectWorkTypeID.Text = lblSelectWorkTypeID.Text + ",'" + ddlWorkTypeID.Items(i).Value + "'"
                                    hidWorkTypeID_Old.Value = hidWorkTypeID_Old.Value + "|0|" + ddlWorkTypeID.Items(i).Value
                                End If
                            Next
                            hidWorkTypeID.Value = hidWorkTypeID_Old.Value '異動前

                            '會計分別行
                            txtAccountBranch.Text = dr.Item("AccountBranch").ToString.Trim
                            hidAccountBranch_Old.Value = dr.Item("AccountBranch").ToString.Trim '異動前

                            '費用分攤科目別
                            Dim strCostType As String = dt.Rows(0).Item("CostType").ToString.Trim
                            If strCostType <> "" Then
                                ddlCostType.SelectedValue = dt.Rows(0).Item("CostType").ToString.Trim
                            ElseIf strCostType = "" Then
                                ddlCostType.SelectedValue = ""
                            End If
                            hidCostType_Old.Value = ddlCostType.SelectedValue '異動前
                            '------------------------------實體組織END------------------------------'

                            '最後異動公司
                            Dim strLastChgComp As String = dr.Item("LastChgCompName").ToString.Trim
                            If strLastChgComp <> "" Then
                                txtLastChgComp.Text = strLastChgComp
                            ElseIf strLastChgComp = "" Then
                                txtLastChgComp.Text = dr.Item("LastChgComp").ToString.Trim
                            End If

                            '最後異動人員
                            Dim strLastChgID As String = dr.Item("LastChgName").ToString.Trim
                            If strLastChgID <> "" Then
                                txtLastChgID.Text = strLastChgID
                            ElseIf strLastChgID = "" Then
                                txtLastChgID.Text = dr.Item("LastChgID").ToString.Trim
                            End If

                            '最後異動日期
                            txtLastChgDate.Text = dr.Item("LastChgDate").ToString.Trim
                        Next
                    End If
                End Using
            ElseIf txtOrganType.Text = "2-功能組織" Then
                Using dt As DataTable = objOM.subGetData_OM2010OrganizationFlow(hidCompID.Value, txtOrganID.Text)
                    If dt.Rows().Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            '部門英文名
                            txtOrganEngName.Text = dr.Item("OrganEngName").ToString.Trim

                            '無效註記
                            hidInValidFlag.Value = dr.Item("InValidFlag").ToString.Trim
                            If hidInValidFlag.Value = "1" Then '1-無效
                                chkInValidFlag.Checked = True
                            ElseIf hidInValidFlag.Value = "0" Then '0-有效
                                chkInValidFlag.Checked = False
                            End If

                            '是否為虛擬部門
                            hidVirtualFlag.Value = dr.Item("VirtualFlag").ToString.Trim
                            If hidVirtualFlag.Value = "1" Then '1-虛擬
                                chkVirtualFlag.Checked = True
                            ElseIf hidVirtualFlag.Value = "0" Then '0-一般
                                chkVirtualFlag.Checked = False
                            End If

                            '分行註記
                            hidBranchFlag.Value = dr.Item("BranchFlag").ToString.Trim
                            If hidBranchFlag.Value = "1" Then '1-是
                                chkBranchFlag.Checked = True
                            ElseIf hidBranchFlag.Value = "0" Then '0-否
                                chkBranchFlag.Checked = False
                            End If

                            '單位類別
                            hidOrgType.Value = dr.Item("OrgType").ToString.Trim
                            If dr.Item("OrgType").ToString.Trim = "" Then
                                txtOrgType.Text = ""
                            Else
                                txtOrgType.Text = dr.Item("OrgType").ToString.Trim + "-" + dr.Item("OrgTypeName").ToString.Trim
                            End If

                            '所屬事業群
                            hidGroupID.Value = dr.Item("GroupID").ToString.Trim
                            If dr.Item("GroupID").ToString.Trim = "" Then
                                txtGroupID.Text = ""
                            Else
                                txtGroupID.Text = dr.Item("GroupID").ToString.Trim + "-" + dr.Item("GroupName").ToString.Trim
                            End If

                            '上階部門
                            hidUpOrganID.Value = dr.Item("UpOrganID").ToString.Trim
                            If dr.Item("UpOrganID").ToString.Trim = "" Then
                                txtUpOrganID.Text = ""
                            Else
                                txtUpOrganID.Text = dr.Item("UpOrganID").ToString.Trim + "-" + dr.Item("UpOrganName").ToString.Trim
                            End If

                            '事業群類別
                            hidGroupType.Value = dr.Item("GroupType").ToString.Trim
                            If dr.Item("GroupType").ToString.Trim = "" Then
                                txtGroupType.Text = ""
                            Else
                                txtGroupType.Text = dr.Item("GroupType").ToString.Trim + "-" + dr.Item("GroupTypeName").ToString.Trim
                            End If

                            '所屬一級部門
                            hidDeptID.Value = dr.Item("DeptID").ToString.Trim
                            If dr.Item("DeptID").ToString.Trim = "" Then
                                txtDeptID.Text = ""
                            Else
                                txtDeptID.Text = dr.Item("DeptID").ToString.Trim + "-" + dr.Item("DeptName").ToString.Trim
                            End If

                            '部門主管角色
                            hidRoleCode.Value = dr.Item("RoleCode").ToString.Trim
                            If dr.Item("RoleCode").ToString.Trim = "" Then
                                txtRoleCode.Text = ""
                            Else
                                txtRoleCode.Text = dr.Item("RoleCode").ToString.Trim + "-" + dr.Item("RoleCodeName").ToString.Trim
                            End If

                            '部門主管
                            hidBossCompID.Value = dr.Item("BossCompID").ToString.Trim
                            hidBoss.Value = dr.Item("Boss").ToString.Trim

                            If dr.Item("BossCompID").ToString.Trim = "" Then
                                txtBoss.Text = ""
                            Else
                                txtBoss.Text = dr.Item("BossCompID").ToString.Trim + "-" + dr.Item("BossCompName").ToString.Trim + "-" + dr.Item("Boss").ToString.Trim + "-" + dr.Item("BossName").ToString.Trim
                            End If

                            '主管任用方式
                            hidBossType.Value = dr.Item("BossType").ToString.Trim
                            txtBossType.Text = dr.Item("BossType").ToString.Trim
                            If txtBossType.Text = "1" Then
                                txtBossType.Text = "1-主要"
                            ElseIf txtBossType.Text = "2" Then
                                txtBossType.Text = "2-兼任"
                            End If

                            '副主管
                            hidSecBossCompID.Value = dr.Item("SecBossCompID").ToString.Trim
                            hidSecBoss.Value = dr.Item("SecBoss").ToString.Trim

                            If dr.Item("SecBossCompID").ToString.Trim = "" Then
                                txtSecBoss.Text = ""
                            Else
                                txtSecBoss.Text = dr.Item("SecBossCompID").ToString.Trim + "-" + dr.Item("SecBossCompName").ToString.Trim + "-" + dr.Item("SecBoss").ToString.Trim + "-" + dr.Item("SecBossName").ToString.Trim
                            End If

                            '主管暫代
                            hidBossTemporary.Value = dr.Item("BossTemporary").ToString.Trim
                            If hidBossTemporary.Value = "1" Then '1-是
                                chkBossTemporary.Checked = True
                            ElseIf hidBossTemporary.Value = "0" Then '0-否
                                chkBossTemporary.Checked = False
                            End If
                            '------------------------------功能單位START------------------------------'
                            '比對簽核單位
                            hidFlowOrganID_Old.Value = dr.Item("FlowOrganID").ToString.Trim '異動前
                            If dt.Rows(0).Item("FlowOrganID").ToString = "" Then
                                ddlFlowOrganID.Items.Clear()
                                ddlFlowOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
                            Else
                                hidFlowOrgan.Value = "'" & dt.Rows(0).Item("FlowOrganID").Replace("|", "','").ToString & "'"
                                Bsp.Utility.FillDDL(ddlFlowOrganID, "eHRMSDB", "OrganizationFlow", "RTrim(OrganID)", "OrganName", Bsp.Utility.DisplayType.Full, "", "And OrganID In (" & hidFlowOrgan.Value & ")", "Order by OrganID")
                            End If

                            'HR內部比對單位註記
                            hidCompareFlag_Old.Value = dr.Item("CompareFlag").ToString.Trim '異動前
                            hidCompareFlag.Value = dr.Item("CompareFlag").ToString.Trim
                            If hidCompareFlag.Value = "1" Then '1-是
                                chkCompareFlag.Checked = True
                            ElseIf hidCompareFlag.Value = "0" Then '0-否
                                chkCompareFlag.Checked = False
                            End If

                            '授權單位
                            hidDelegateFlag_Old.Value = dr.Item("DelegateFlag").ToString.Trim '異動前
                            hidDelegateFlag.Value = dr.Item("DelegateFlag").ToString.Trim
                            If hidDelegateFlag.Value = "1" Then '1-是
                                chkDelegateFlag.Checked = True
                            ElseIf hidDelegateFlag.Value = "0" Then '0-否
                                chkDelegateFlag.Checked = False
                            End If

                            '處級單位註記
                            hidOrganNo_Old.Value = dr.Item("OrganNo").ToString.Trim '異動前
                            hidOrganNo.Value = dr.Item("OrganNo").ToString.Trim
                            If hidOrganNo.Value = "1" Then '1-是
                                chkOrganNo.Checked = True
                            ElseIf hidOrganNo.Value = "0" Then '0-否
                                chkOrganNo.Checked = False
                            End If

                            '業務類別
                            OM2.FillDDL(ddlBusinessType, "HRCodeMap", "RTRIM(Code)", "CodeCName", OM2.DisplayType.Full, "", "And TabName = 'Business' And FldName='BusinessType' ", "")
                            ddlBusinessType.Items.Insert(0, New ListItem("---請選擇---", ""))
                            ddlBusinessType.SelectedValue = dt.Rows(0).Item("BusinessType").ToString.Trim
                            hidBusinessType_Old.Value = ddlBusinessType.SelectedValue '異動前

                            '------------------------------功能單位END------------------------------'

                            '最後異動公司
                            Dim strLastChgComp As String = dr.Item("LastChgCompName").ToString.Trim
                            If strLastChgComp <> "" Then
                                txtLastChgComp.Text = strLastChgComp
                            ElseIf strLastChgComp = "" Then
                                txtLastChgComp.Text = dr.Item("LastChgComp").ToString.Trim
                            End If

                            '最後異動人員
                            Dim strLastChgID As String = dr.Item("LastChgName").ToString.Trim
                            If strLastChgID <> "" Then
                                txtLastChgID.Text = strLastChgID
                            ElseIf strLastChgID = "" Then
                                txtLastChgID.Text = dr.Item("LastChgID").ToString.Trim
                            End If

                            '最後異動日期
                            txtLastChgDate.Text = dr.Item("LastChgDate").ToString.Trim
                        Next
                    End If
                End Using
            End If

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & "subGetData", ex)
        End Try
    End Sub
#End Region

#Region "uc-DoModelReturn"
    Public Overrides Sub DoModalReturn(ByVal returnValue As String)
        Dim objOM2 As New OM2
        Dim strSql As String = ""
        Dim strRstName1 As String = ""
        Dim strRstName2 As String = ""

        If returnValue <> "" Then
            Dim aryData() As String = returnValue.Split(":")
            Select Case aryData(0)
                Case "ucPersonPart" '第一人事管理員
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    txtPersonPart.Text = aryValue(1)
                    lblPersonPartName.Text = aryValue(2)
                Case "ucSecPersonPart" '第二人事管理員
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    txtSecPersonPart.Text = aryValue(1)
                    lblSecPersonPartName.Text = aryValue(2)
                Case "ucCheckPart" '自行查核主管
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    txtCheckPart.Text = aryValue(1)
                    lblCheckPartName.Text = aryValue(2)
                Case "ucFlowOrgan" '比對簽核單位
                    If aryData(1).Replace("'", "").Length = 0 Then
                        hidFlowOrgan.Value = ""
                        ddlFlowOrganID.Items.Clear()
                        ddlFlowOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
                    Else
                        hidFlowOrgan.Value = aryData(1)
                        Bsp.Utility.FillDDL(ddlFlowOrganID, "eHRMSDB", "OrganizationFlow", "RTrim(OrganID)", "OrganName", Bsp.Utility.DisplayType.Full, "", "And OrganID In (" & aryData(1) & ")", "Order by OrganID")
                    End If

                    '/*=========================================*/
                Case "ucSelectPosition"
                    lblSelectPositionID.Text = aryData(1)
                    If lblSelectPositionID.Text <> "''" Then  '非必填時，回傳空值
                        '載入 職位 下拉式選單
                        'strSql = " and PositionID in (" + lblSelectPositionID.Text + ") "
                        strSql = "and PositionID in (" + lblSelectPositionID.Text + ") and CompID = '" + UserProfile.SelectCompRoleID + "'"
                        Bsp.Utility.Position(ddlPositionID, "PositionID", , strSql)
                        'Bsp.Utility.RE_PositionU(ddlPosition, "PositionID")

                    Else
                        ddlPositionID.Items.Clear()
                        ddlPositionID.Items.Insert(0, New ListItem("---請選擇---", ""))
                    End If
                    '/*-----------------------------------------*/
                Case "ucSelectWorkType" '工作性質
                    lblSelectWorkTypeID.Text = aryData(1)
                    If lblSelectWorkTypeID.Text <> "''" Then  '非必填時，回傳空值
                        strSql = " and WorkTypeID in (" + lblSelectWorkTypeID.Text + ") and CompID = '" + UserProfile.SelectCompRoleID + "'"
                        Bsp.Utility.WorkType(ddlWorkTypeID, "WorkTypeID", , strSql)

                        '第一筆為主要工作性質
                        Dim strDefaultValue() As String = lblSelectWorkTypeID.Text.Replace("'", "").Split(",")
                        Dim strWorkType As String = ""
                        Bsp.Utility.SetSelectedIndex(ddlWorkTypeID, strDefaultValue(0))
                    Else
                        ddlWorkTypeID.Items.Clear()
                        ddlWorkTypeID.Items.Insert(0, New ListItem("---請選擇---", ""))
                    End If
                    '/*--------------------------------------------*/

            End Select
        End If
    End Sub
#End Region

#Region "UC-clicked與附屬之DDL、TXT"
#Region "人員欄位變更"
    Protected Sub txtPersonPart_TextChanged(sender As Object, e As System.EventArgs) Handles txtPersonPart.TextChanged
        If txtPersonPart.Text <> "" Then
            Dim objHR As New HR
            Dim rtnTable As DataTable = objHR.GetHREmpName(UserProfile.SelectCompRoleID, txtPersonPart.Text)
            If rtnTable.Rows.Count <= 0 Then
                lblPersonPartName.Text = ""
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", "人事資料尚未建檔")
            Else
                lblPersonPartName.Text = rtnTable.Rows(0).Item(0)
            End If
        Else
            lblPersonPartName.Text = ""
        End If
    End Sub

    Protected Sub txtSecPersonPart_TextChanged(sender As Object, e As System.EventArgs) Handles txtSecPersonPart.TextChanged
        If txtSecPersonPart.Text <> "" Then
            Dim objHR As New HR
            Dim rtnTable As DataTable = objHR.GetHREmpName(UserProfile.SelectCompRoleID, txtSecPersonPart.Text)
            If rtnTable.Rows.Count <= 0 Then
                lblSecPersonPartName.Text = ""
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", "人事資料尚未建檔")
            Else
                lblSecPersonPartName.Text = rtnTable.Rows(0).Item(0)
            End If
        Else
            lblSecPersonPartName.Text = ""
        End If
    End Sub

    Protected Sub txtCheckPart_TextChanged(sender As Object, e As System.EventArgs) Handles txtCheckPart.TextChanged
        If txtCheckPart.Text <> "" Then
            Dim objHR As New HR
            Dim rtnTable As DataTable = objHR.GetHREmpName(UserProfile.SelectCompRoleID, txtCheckPart.Text)
            If rtnTable.Rows.Count <= 0 Then
                lblCheckPartName.Text = ""
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", "人事資料尚未建檔")
            Else
                lblCheckPartName.Text = rtnTable.Rows(0).Item(0)
            End If
        Else
            lblCheckPartName.Text = ""
        End If
    End Sub
#End Region

#Region "點擊uc"
    Protected Sub ucPersonPart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ucPersonPart.Load
        '載入按鈕-第一人事管理員選單畫面
        ucPersonPart.SelectCompID = UserProfile.SelectCompRoleID
    End Sub
    Protected Sub ucSecPersonPart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ucSecPersonPart.Load
        '載入按鈕-第二人事管理員選單畫面
        ucSecPersonPart.SelectCompID = UserProfile.SelectCompRoleID
    End Sub
    '/*========職業、工作性質=================================================*/
    Protected Sub ucSelectPosition_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ucSelectPosition.Load
        '載入按鈕-職位選單畫面
        ucSelectPosition.QueryCompID = hidCompID.Value
        ucSelectPosition.QueryEmpID = ""
        ucSelectPosition.DefaultPosition = lblSelectPositionID.Text
        ucSelectPosition.QueryOrganID = ""

        ucSelectPosition.Fields = New FieldState() { _
            New FieldState("PositionID", "職位代碼", True, True), _
            New FieldState("Remark", "職位名稱", True, True)}
    End Sub

    '異動後資料-職位下拉選單:將選擇那筆 改為 第一筆為主要職位
    Protected Sub ddlPositionID_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlPositionID.SelectedIndexChanged
        Dim strRst1 As String = ""
        Dim strRst2 As String = ""
        Dim strMainPosition As String = ""
        Dim strPosition As String = ""
        For i As Integer = 0 To ddlPositionID.Items.Count - 1

            If ddlPositionID.Items(i).Selected Then
                strRst1 = "'" + ddlPositionID.Items(i).Value + "'"
                strMainPosition = "1|" + ddlPositionID.Items(i).Value
            Else
                If strRst2 <> "" Then strRst2 += ","
                strRst2 += "'" + ddlPositionID.Items(i).Value + "'"
                If strPosition <> "" Then strPosition += "|"
                strPosition += "0|" + ddlPositionID.Items(i).Value
            End If
        Next
        If strRst2 = "" Then
            lblSelectPositionID.Text = strRst1
            hidPositionID.Value = strMainPosition
        Else
            lblSelectPositionID.Text = strRst1 + "," + strRst2
            hidPositionID.Value = strMainPosition + "|" + strPosition
        End If
    End Sub

    Public Function GetPositionID(ByVal strPositionID As String) As String
        Dim strWhere As String
        Dim strWherePosition As String
        Dim aryValue() As String = strPositionID.Split("|")
        Dim intCnt As Integer
        Dim strMainPosition As String '主要職位
        Dim objST As New ST1
        strMainPosition = ""
        Dim CompID As String = hidCompID.Value

        strWhere = "where CompID = " & Bsp.Utility.Quote(CompID)
        strWherePosition = ""
        For intCnt = 0 To UBound(aryValue) Step 2
            If intCnt = 0 Then
                strWherePosition = Bsp.Utility.Quote(aryValue(intCnt + 1).ToString().Trim)
            Else
                strWherePosition = strWherePosition & "," & Bsp.Utility.Quote(aryValue(intCnt + 1).ToString().Trim)
            End If
            If aryValue(intCnt) = "1" Then
                strMainPosition = aryValue(intCnt + 1)
            End If
        Next
        If intCnt > 0 Then
            strWhere = strWhere & "And PositionID In (" & strWherePosition & ")"
        End If

        lblSelectPositionID.Text = strWherePosition

        Try
            Using dt As Data.DataTable = objST.GetPositionID(strWhere).Tables(0)
                With ddlPositionID
                    .DataSource = dt
                    .DataTextField = "FullPositionName"
                    .DataValueField = "PositionID"
                    .DataBind()
                    .Items.Insert(0, New ListItem("---請選擇---", ""))
                End With
            End Using
            Return strMainPosition

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me.Page, Bsp.Utility.getInnerException("ddlPositionID", ex))
            Return strMainPosition
        End Try
    End Function

    Protected Sub ucSelectWorkType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ucSelectWorkType.Load
        '載入按鈕-工作性質選單畫面
        ucSelectWorkType.QueryCompID = hidCompID.Value
        ucSelectWorkType.QueryEmpID = ""
        ucSelectWorkType.DefaultWorkType = lblSelectWorkTypeID.Text
        ucSelectWorkType.QueryOrganID = ""

        ucSelectWorkType.Fields = New FieldState() { _
            New FieldState("WorkTypeID", "工作性質代碼", True, True), _
            New FieldState("Remark", "工作性質名稱", True, True)}
    End Sub
    'hidWorkTypeID.Value=lblSelectWorkTypeID.Text
    '異動後資料-工作性質選單:將選擇那筆 改為 第一筆為主要工作性質
    Protected Sub ddlWorkType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlWorkTypeID.SelectedIndexChanged
        Dim strRst1 As String = ""
        Dim strRst2 As String = ""
        Dim strMainWorkType As String = ""
        Dim strWorkType As String = ""
        Dim strRstName1 As String = ""
        Dim strRstName2 As String = ""
        For i As Integer = 0 To ddlWorkTypeID.Items.Count - 1

            If ddlWorkTypeID.Items(i).Selected Then
                strRst1 = "'" + ddlWorkTypeID.Items(i).Value + "'"
                strMainWorkType = "1|" + ddlWorkTypeID.Items(i).Value
                strRstName1 = ddlWorkTypeID.Items(i).Text.Trim.Split("-")(1).ToString
            Else
                If strRst2 <> "" Then strRst2 += ","
                strRst2 += "'" + ddlWorkTypeID.Items(i).Value + "'"
                If strWorkType <> "" Then strWorkType += "|"
                strWorkType += "0|" + ddlWorkTypeID.Items(i).Value
                If strRstName2 <> "" Then strRstName2 += ","
                strRstName2 += ddlWorkTypeID.Items(i).Text.Trim.Split("-")(1).ToString
            End If
        Next
        If strRst2 = "" Then
            lblSelectWorkTypeID.Text = strRst1
            hidWorkTypeID.Value = strMainWorkType
            lblSelectWorkTypeName.Text = strRstName1
        Else
            lblSelectWorkTypeID.Text = strRst1 + "," + strRst2
            hidWorkTypeID.Value = strMainWorkType + "|" + strWorkType
            lblSelectWorkTypeName.Text = strRstName1 + "," + strRstName2
        End If

    End Sub

    Public Function GetWorkTypeID(ByVal strWorkTypeID As String) As String
        Dim strWhere As String = "where CompID = " & Bsp.Utility.Quote(hidCompID.Value)
        Dim strWhereWorkType As String = ""
        Dim aryValue() As String = strWorkTypeID.Split("|")
        Dim intCnt As Integer
        Dim strMainWorkType As String = "" '主要工作性質
        Dim objST As New ST1

        For intCnt = 0 To UBound(aryValue) Step 2
            If intCnt = 0 Then
                strWhereWorkType = Bsp.Utility.Quote(aryValue(intCnt + 1).ToString().Trim)
            Else
                strWhereWorkType = strWhereWorkType & "," & Bsp.Utility.Quote(aryValue(intCnt + 1).ToString().Trim)
            End If
            If aryValue(intCnt) = "1" Then
                strMainWorkType = aryValue(intCnt + 1)
            End If
        Next

        If intCnt > 0 Then
            strWhere = strWhere & "And WorkTypeID In (" & strWhereWorkType & ")"
        End If

        lblSelectWorkTypeID.Text = strWhereWorkType

        Try
            Using dt As Data.DataTable = objST.GetWorkTypeID(strWhere).Tables(0)
                With ddlWorkTypeID
                    .DataSource = dt
                    .DataTextField = "FullWorkTypeName"
                    .DataValueField = "WorkTypeID"
                    .DataBind()
                    .Items.Insert(0, New ListItem("---請選擇---", ""))
                End With
            End Using

            Return strMainWorkType

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me.Page, Bsp.Utility.getInnerException("ddlWorkTypeID", ex))
            Return strMainWorkType
        End Try
    End Function

    '載入按鈕-比對簽核單位畫面
    Protected Sub ucFlowOrgan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ucFlowOrgan.Load

        ucFlowOrgan.QueryCompID = ""
        ucFlowOrgan.DefaultFlowOrgan = hidFlowOrgan.Value
        ucFlowOrgan.Fields = New FieldState() { _
            New FieldState("OrganID", "單位代碼", True, True), _
            New FieldState("OrganName", "單位名稱", True, True)}
    End Sub
#End Region

#End Region

#Region "視窗按鈕 DoAction"
    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnUpdate"       '修改
                If funCheckData(hidOrganType.Value.ToString()) Then
                    If SaveData() Then
                        InsertOrganizationLog()
                        Bsp.Utility.ShowMessage(Me, "存檔成功！")
                        GetSelectData(UserProfile.SelectCompRoleID, txtOrganID.Text)
                    End If
                End If
        End Select
    End Sub

#Region "funCheckData"
    Private Function funCheckData(ByVal hidOrganType As String) As Boolean
        Dim objOM2 As New OM2()

        '/*********************組織資料-修改*********************/
        If hidOrganType = "1" Then
            '工作地點
            If ddlWorkSiteID.SelectedValue.Trim = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00030", "工作地點")
                ddlWorkSiteID.Focus()
                Return False
            End If

            '工作性質
            If ddlWorkTypeID.Text.Trim = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00030", "工作性質")
                ddlWorkTypeID.Focus()
                Return False
            End If
        End If

        Return True
    End Function

#End Region

#Region "存檔(含uc欄位)"
    '儲存
    Private Function SaveData() As Boolean
        Dim beOrganization As New beOrganization.Row()
        Dim bsOrganization As New beOrganization.Service()

        Dim beOrganizationFlow As New beOrganizationFlow.Row()
        Dim bsOrganizationFlow As New beOrganizationFlow.Service()

        Dim objOM2 As New OM2()

        If ViewState.Item("OrganType") = "1-行政組織" Then
            '-----取得輸入資料-----
            
            '公司代碼
            beOrganization.CompID.Value = hidCompID.Value
            '部門代碼
            beOrganization.OrganID.Value = hidOrganID.Value
            '第一人事管理員
            beOrganization.PersonPart.Value = txtPersonPart.Text
            '第二人事管理員
            beOrganization.SecPersonPart.Value = txtSecPersonPart.Text
            '工作地點
            beOrganization.WorkSiteID.Value = ddlWorkSiteID.SelectedValue
            '自行查核主管
            beOrganization.CheckPart.Value = txtCheckPart.Text

            '職位(多筆)
            'beOrganization.PositionID.Value = ddlPositionID.SelectedValue

            '費用分攤部門
            beOrganization.CostDeptID.Value = ddlCostDeptID.SelectedValue

            '工作性質(多筆)
            'beOrganization.WorkTypeID.Value = ddlWorkTypeID.SelectedValue

            '會計分行別
            beOrganization.AccountBranch.Value = txtAccountBranch.Text
            '費用分攤科別
            beOrganization.CostType.Value = ddlCostType.SelectedValue

            '最後異動人員公司
            beOrganization.LastChgComp.Value = UserProfile.ActCompID
            '最後異動人員
            beOrganization.LastChgID.Value = UserProfile.ActUserID
            '最後異動時間
            beOrganization.LastChgDate.Value = Now

            '儲存資料
            Try
                Dim PositionID() As String = lblSelectPositionID.Text.Replace("'", "").Split(",")
                Dim WorkTypeID() As String = lblSelectWorkTypeID.Text.Replace("'", "").Split(",")
                Return objOM2.UpdateOWAddition(beOrganization, hidCompID.Value.ToString, hidOrganID.Value.ToString, hidOrganType.Value.ToString, PositionID, WorkTypeID)

            Catch ex As Exception
                Dim errLine As Integer = Convert.ToInt32(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(" ")))
                Bsp.Utility.ShowMessage(Me, "[SaveData]" & errLine & Bsp.Utility.getMessage("E_00000") & Bsp.Utility.getInnerException(Me.FunID, ex))
                Return False
            End Try
        ElseIf ViewState.Item("OrganType") = "2-功能組織" Then

            '-----取得輸入資料-----
            '公司代碼
            'beOrganizationFlow.CompID.Value = hidCompID.Value
            '部門代碼
            beOrganizationFlow.OrganID.Value = hidOrganID.Value

            '比對簽核單位(多筆) 
            For i = 0 To ddlFlowOrganID.Items.Count - 1
                If i = 0 Then
                    beOrganizationFlow.FlowOrganID.Value += ddlFlowOrganID.Items(i).Value
                Else
                    beOrganizationFlow.FlowOrganID.Value += "|" + ddlFlowOrganID.Items(i).Value
                End If
            Next


            'HR內部比對簽核
            If chkCompareFlag.Checked Then
                beOrganizationFlow.CompareFlag.Value = 1
            Else
                beOrganizationFlow.CompareFlag.Value = 0
            End If
            '授權單位
            If chkDelegateFlag.Checked Then
                beOrganizationFlow.DelegateFlag.Value = 1
            Else
                beOrganizationFlow.DelegateFlag.Value = 0
            End If
            '處級單位註記
            If chkOrganNo.Checked Then
                beOrganizationFlow.OrganNo.Value = 1
            Else
                beOrganizationFlow.OrganNo.Value = 0
            End If
            '業務類別
            beOrganizationFlow.BusinessType.Value = ddlBusinessType.SelectedValue

            '最後異動人員公司
            beOrganizationFlow.LastChgComp.Value = UserProfile.ActCompID
            '最後異動人員
            beOrganizationFlow.LastChgID.Value = UserProfile.ActUserID
            '最後異動時間
            beOrganizationFlow.LastChgDate.Value = Now

            '儲存資料
            Try
                Return objOM2.OrganizationFlowUpdate(beOrganizationFlow)
            Catch ex As Exception
                Bsp.Utility.ShowMessage(Me, "[CC0201_99]" & Bsp.Utility.getMessage("E_00000") & Bsp.Utility.getInnerException(Me.FunID, ex))
                Return False
            End Try

        End If
        Return True
    End Function

    '新增組織異動記錄檔
    Private Function InsertOrganizationLog() As Boolean
        Dim beOrganizationLog As New beOrganizationLog.Row()
        Dim bsOrganizationLog As New beOrganizationLog.Service()

        Dim objOM2 As New OM2()
        Dim intSeq As String
        If ViewState.Item("OrganType") = "1-行政組織" Then
            beOrganizationLog.CompID.Value = hidCompID.Value '公司代碼
            beOrganizationLog.OrganReason.Value = 3 '異動原因:組織異動
            beOrganizationLog.OrganType.Value = hidOrganType.Value '組織類型:1:行政;2:功能
            beOrganizationLog.ValidDateB.Value = Format(Date.Parse(Now.ToString), "yyyy/MM/dd") '單位有效起日
            'beOrganizationLog.ValidDateB.Value = Format(Date.Parse(beOrganizationLog.ValidDateB.Value.ToString()), "yyyy/MM/dd")
            intSeq = objOM2.GetSeq("OrganizatioLog", hidCompID.Value, 3, hidOrganType.Value, Format(Date.Parse(Now.ToString()), "yyyy/MM/dd"), txtOrganID.Text)
            beOrganizationLog.Seq.Value = intSeq
            beOrganizationLog.OrganID.Value = txtOrganID.Text

            '----------異動前主要欄位START(不得更改)----------
            beOrganizationLog.OrganNameOld.Value = txtOrganName.Text
            beOrganizationLog.OrganEngNameOld.Value = txtOrganEngName.Text
            beOrganizationLog.VirtualFlagOld.Value = hidVirtualFlag.Value
            beOrganizationLog.UpOrganIDOld.Value = hidUpOrganID.Value
            beOrganizationLog.DeptIDOld.Value = hidDeptID.Value
            beOrganizationLog.GroupTypeOld.Value = hidGroupType.Value
            beOrganizationLog.GroupIDOld.Value = hidGroupID.Value
            beOrganizationLog.BossOld.Value = hidBoss.Value
            beOrganizationLog.BossCompIDOld.Value = hidBossCompID.Value
            beOrganizationLog.BossTypeOld.Value = hidBossType.Value
            beOrganizationLog.BossTemporaryOld.Value = hidBossTemporary.Value
            beOrganizationLog.SecBossOld.Value = hidSecBoss.Value
            beOrganizationLog.SecBossCompIDOld.Value = hidSecBossCompID.Value
            beOrganizationLog.InValidFlagOld.Value = hidInValidFlag.Value
            'beOrganizationLog.ReportingEndOld.Value = '異動前簽核頂端註記
            'beOrganizationLog.DepthOld.Value '異動前層級
            beOrganizationLog.BranchFlagOld.Value = hidBranchFlag.Value
            beOrganizationLog.RoleCodeOld.Value = hidRoleCode.Value
            beOrganizationLog.OrgTypeOld.Value = hidOrgType.Value
            '----------異動前主要欄位END(不得更改)----------

            '----------異動後主要欄位START(不得更改)----------
            beOrganizationLog.OrganName.Value = txtOrganName.Text
            beOrganizationLog.OrganEngName.Value = txtOrganEngName.Text
            beOrganizationLog.VirtualFlag.Value = hidVirtualFlag.Value
            beOrganizationLog.UpOrganID.Value = hidUpOrganID.Value
            beOrganizationLog.DeptID.Value = hidDeptID.Value
            beOrganizationLog.GroupType.Value = hidGroupType.Value
            beOrganizationLog.GroupID.Value = hidGroupID.Value
            beOrganizationLog.Boss.Value = hidBoss.Value
            beOrganizationLog.BossCompID.Value = hidBossCompID.Value
            beOrganizationLog.BossType.Value = hidBossType.Value
            beOrganizationLog.BossTemporary.Value = hidBossTemporary.Value
            beOrganizationLog.SecBoss.Value = hidSecBoss.Value
            beOrganizationLog.SecBossCompID.Value = hidSecBossCompID.Value
            beOrganizationLog.InValidFlag.Value = hidInValidFlag.Value
            'beOrganizationLog.ReportingEnd.Value '異動後簽核頂端註記??
            'beOrganizationLog.Depth.Value '異動後層級??
            beOrganizationLog.BranchFlag.Value = hidBranchFlag.Value '異動後分行註記
            beOrganizationLog.RoleCode.Value = hidRoleCode.Value '異動後單位角色
            beOrganizationLog.OrgType.Value = hidOrgType.Value '異動後單位類別
            '----------異動後主要欄位END(不得更改)----------

            '----------異動前行政START----------
            beOrganizationLog.PersonPartOld.Value = hidPersonPart_Old.Value
            beOrganizationLog.SecPersonPartOld.Value = hidSecPersonPart_Old.Value
            beOrganizationLog.CheckPartOld.Value = hidCheckPart_Old.Value
            beOrganizationLog.WorkSiteIDOld.Value = hidWorkSiteID_Old.Value '異動前工作地點
            beOrganizationLog.WorkTypeIDOld.Value = hidWorkTypeID_Old.Value '異動前工作性質
            beOrganizationLog.CostDeptIDOld.Value = hidCostDeptID_Old.Value '異動前費用分攤部門
            beOrganizationLog.CostTypeOld.Value = hidCostType_Old.Value '異動前費用分攤科目別
            beOrganizationLog.AccountBranchOld.Value = hidAccountBranch_Old.Value '異動前會計分行別
            beOrganizationLog.PositionIDOld.Value = hidPositionID_Old.Value '異動前職位
            '----------異動前行政END----------

            '----------異動後行政START----------
            beOrganizationLog.PersonPart.Value = txtPersonPart.Text
            beOrganizationLog.SecPersonPart.Value = txtSecPersonPart.Text
            beOrganizationLog.CheckPart.Value = txtCheckPart.Text
            beOrganizationLog.WorkSiteID.Value = ddlWorkSiteID.SelectedValue '異動後工作地點
            beOrganizationLog.WorkTypeID.Value = hidWorkTypeID.Value '異動後工作性質
            beOrganizationLog.CostDeptID.Value = ddlCostDeptID.SelectedValue '異動後費用分攤部門
            beOrganizationLog.CostType.Value = ddlCostType.SelectedValue '異動後費用分攤科目別
            beOrganizationLog.AccountBranch.Value = txtAccountBranch.Text '異動後會計分行別
            beOrganizationLog.PositionID.Value = hidPositionID.Value '異動後職位
            '----------異動後行政END----------

            '最後異動人員公司
            beOrganizationLog.LastChgComp.Value = UserProfile.ActCompID
            '最後異動人員
            beOrganizationLog.LastChgID.Value = UserProfile.ActUserID
            '最後異動時間
            beOrganizationLog.LastChgDate.Value = Now
        ElseIf ViewState.Item("OrganType") = "2-功能組織" Then
            beOrganizationLog.CompID.Value = hidCompID.Value '公司代碼
            beOrganizationLog.OrganReason.Value = 3 '異動原因:組織異動
            beOrganizationLog.OrganType.Value = hidOrganType.Value '組織類型:1:行政;2:功能
            beOrganizationLog.ValidDateB.Value = Format(Date.Parse(Now.ToString), "yyyy/MM/dd") '單位有效起日
            intSeq = objOM2.GetSeq("OrganizatioLog", hidCompID.Value, 3, hidOrganType.Value, Format(Date.Parse(Now.ToString), "yyyy/MM/dd"), txtOrganID.Text)
            beOrganizationLog.Seq.Value = intSeq
            beOrganizationLog.OrganID.Value = txtOrganID.Text

            '----------異動前主要欄位START(不得更改)----------
            beOrganizationLog.OrganNameOld.Value = txtOrganName.Text
            beOrganizationLog.OrganEngNameOld.Value = txtOrganEngName.Text
            beOrganizationLog.VirtualFlagOld.Value = hidVirtualFlag.Value
            beOrganizationLog.UpOrganIDOld.Value = hidUpOrganID.Value '異動前上階部門
            beOrganizationLog.DeptIDOld.Value = hidDeptID.Value
            beOrganizationLog.GroupTypeOld.Value = hidGroupType.Value
            beOrganizationLog.GroupIDOld.Value = hidGroupID.Value
            beOrganizationLog.BossOld.Value = hidBoss.Value
            beOrganizationLog.BossCompIDOld.Value = hidBossCompID.Value
            beOrganizationLog.BossTypeOld.Value = hidBossType.Value
            beOrganizationLog.BossTemporaryOld.Value = hidBossTemporary.Value
            beOrganizationLog.SecBossOld.Value = hidSecBoss.Value
            beOrganizationLog.SecBossCompIDOld.Value = hidSecBossCompID.Value
            beOrganizationLog.InValidFlagOld.Value = hidInValidFlag.Value
            'beOrganizationLog.ReportingEndOld.Value = '異動前簽核頂端註記
            'beOrganizationLog.DepthOld.Value '異動前層級
            beOrganizationLog.BranchFlagOld.Value = hidBranchFlag.Value
            beOrganizationLog.RoleCodeOld.Value = hidRoleCode.Value
            beOrganizationLog.OrgTypeOld.Value = hidOrgType.Value
            '----------異動前主要欄位END(不得更改)----------

            '----------異動後主要欄位START(不得更改)----------
            beOrganizationLog.OrganName.Value = txtOrganName.Text
            beOrganizationLog.OrganEngName.Value = txtOrganEngName.Text
            beOrganizationLog.VirtualFlag.Value = hidVirtualFlag.Value
            beOrganizationLog.UpOrganID.Value = hidUpOrganID.Value '異動後上階部門
            beOrganizationLog.DeptID.Value = hidDeptID.Value
            beOrganizationLog.GroupType.Value = hidGroupType.Value
            beOrganizationLog.GroupID.Value = hidGroupID.Value
            beOrganizationLog.Boss.Value = hidBoss.Value
            beOrganizationLog.BossCompID.Value = hidBossCompID.Value
            beOrganizationLog.BossType.Value = hidBossType.Value
            beOrganizationLog.BossTemporary.Value = hidBossTemporary.Value
            beOrganizationLog.SecBoss.Value = hidSecBoss.Value
            beOrganizationLog.SecBossCompID.Value = hidSecBossCompID.Value
            beOrganizationLog.InValidFlag.Value = hidInValidFlag.Value
            'beOrganizationLog.ReportingEnd.Value '異動後簽核頂端註記??
            'beOrganizationLog.Depth.Value '異動後層級??
            beOrganizationLog.BranchFlag.Value = hidBranchFlag.Value '異動後分行註記
            beOrganizationLog.RoleCode.Value = hidRoleCode.Value '異動後單位角色
            beOrganizationLog.OrgType.Value = hidOrgType.Value '異動後單位類別
            '----------異動後主要欄位END(不得更改)----------

            '----------異動前功能START----------
            beOrganizationLog.CompareFlagOld.Value = hidCompareFlag_Old.Value
            beOrganizationLog.FlowOrganIDOld.Value = hidFlowOrganID_Old.Value
            beOrganizationLog.DelegateFlagOld.Value = hidDelegateFlag_Old.Value
            beOrganizationLog.OrganNoOld.Value = hidOrganNo_Old.Value
            'beOrganizationLog.InvoiceNoOld.Value = 
            'beOrganizationLog.SortOrderOld.Value = 
            beOrganizationLog.BusinessTypeOld.Value = hidBusinessType_Old.Value '異動前業務類別
            '----------異動前功能END----------

            '----------異動後功能START----------
            beOrganizationLog.CompareFlag.Value = hidCompareFlag.Value '異動後HR內部比對單位註記

            '比對簽核迴圈
            hidFlowOrganID.Value = ""
            For i = 0 To ddlFlowOrganID.Items.Count - 1
                If i = 0 Then
                    hidFlowOrganID.Value += ddlFlowOrganID.Items(i).Value
                Else
                    hidFlowOrganID.Value += "|" + ddlFlowOrganID.Items(i).Value
                End If
            Next
            beOrganizationLog.FlowOrganID.Value = hidFlowOrganID.Value '異動後比對簽核

            beOrganizationLog.DelegateFlag.Value = hidDelegateFlag.Value '異動後授權註記
            beOrganizationLog.OrganNo.Value = hidOrganNo.Value
            'beOrganizationLog.InvoiceNo.Value '異動後扣繳單位-統一編號
            'beOrganizationLog.SortOrder.Value '異動後單位順序
            beOrganizationLog.BusinessType.Value = ddlBusinessType.SelectedValue '異動後業務類別

            'beOrganizationLog.ValidDateE.Value = ViewState.Item("ValidDateE").ToString()
            'beOrganizationLog.Remark.Value '備註??
            '----------異動後功能END----------

            '最後異動人員公司
            beOrganizationLog.LastChgComp.Value = UserProfile.ActCompID
            '最後異動人員
            beOrganizationLog.LastChgID.Value = UserProfile.ActUserID
            '最後異動時間
            beOrganizationLog.LastChgDate.Value = Now
        End If

        '儲存資料
        Try
            Return objOM2.OrganizationLogInsert(beOrganizationLog)
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, "[CC0201_99]" & Bsp.Utility.getMessage("E_00000") & Bsp.Utility.getInnerException(Me.FunID, ex))
            Return False
        End Try

        Return True
    End Function
#End Region
#End Region
End Class
