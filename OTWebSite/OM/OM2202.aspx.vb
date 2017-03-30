'Imports System.Collections.Generic
'Imports System.Web.UI.WebControls
'Imports System.Web.UI.WebControls.TableRow
Imports System.Data

Partial Class OM_OM2202
    'Inherits System.Web.UI.Page
    Inherits PageBase
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
        End If
    End Sub
    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo) '返回查詢頁的動作(為了撈回查詢的資料，再做一次查詢 => 利用暫存)
        If Not IsPostBack() Then
            switchEnabled(False, 3)
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)
            Dim aryValue() As String = Split(ht(txtCompID.ID).ToString(), "-")
            '公司代碼
            hidCompID.Value = aryValue(0)
            txtCompID.Text = ht(txtCompID.ID).ToString()
            '部門代碼
            txtOrganID.Text = ht(txtOrganID.ID).ToString() & "-" & ht(txtOrganName.ID).ToString()
            hidOrganID.Value = ht(txtOrganID.ID).ToString().Trim
            '組織類型
            aryValue = Split(ht("SelectedOrganType").ToString(), "-")
            hidOrganType.Value = aryValue(0)
            lblOrganType.Text = aryValue(1)
            '異動原因
            aryValue = Split(ht("SelectedOrganReason").ToString(), "-")
            ddlOrganReason.SelectedValue = aryValue(0)
            hidOrganReason.Value = aryValue(0)
            '生效日期
            ucValidDateB.DateText = ht("SelectedValidDateB").ToString()
            hidValidDateB.Value = ht("SelectedValidDateB").ToString()

            ucValidDateE.DateText = ht("SelectedValidDateE").ToString()
            'Seq暫存
            hidSeq.Value = ht("SelectedSeq").ToString()

            ucQueryBossOld.ShowCompRole = "False"
            ucQueryBossOld.InValidFlag = "N"
            ucQueryBossOld.SelectCompID = hidCompID.Value
            ucSecQueryBossOld.ShowCompRole = "False"
            ucSecQueryBossOld.InValidFlag = "N"
            ucSecQueryBossOld.SelectCompID = hidCompID.Value
            ucSecPersonPartOld.ShowCompRole = "False"
            ucSecPersonPartOld.InValidFlag = "N"
            ucSecPersonPartOld.SelectCompID = hidCompID.Value
            ucPersonPartOld.ShowCompRole = "False"
            ucPersonPartOld.InValidFlag = "N"
            ucPersonPartOld.SelectCompID = hidCompID.Value
            ucCheckPartOld.ShowCompRole = "False"
            ucCheckPartOld.InValidFlag = "N"
            ucCheckPartOld.SelectCompID = hidCompID.Value
            '/**/
            ucQueryBoss.ShowCompRole = "False"
            ucQueryBoss.InValidFlag = "N"
            ucQueryBoss.SelectCompID = hidCompID.Value
            ucSecQueryBoss.ShowCompRole = "False"
            ucSecQueryBoss.InValidFlag = "N"
            ucSecQueryBoss.SelectCompID = hidCompID.Value
            ucSecPersonPart.ShowCompRole = "False"
            ucSecPersonPart.InValidFlag = "N"
            ucSecPersonPart.SelectCompID = hidCompID.Value
            ucPersonPart.ShowCompRole = "False"
            ucPersonPart.InValidFlag = "N"
            ucPersonPart.SelectCompID = hidCompID.Value
            ucCheckPart.ShowCompRole = "False"
            ucCheckPart.InValidFlag = "N"
            ucCheckPart.SelectCompID = hidCompID.Value

            subGetData()
            GetSelectData(True)
            If ht("PageID").ToString() = "NOTupdate" Then
                switchEnabled(False, 3)
                ddlOrganReason.Enabled = False
                ucValidDateB.Enabled = False
                ucValidDateE.Enabled = False
            Else
                switchEnabled(True, ddlOrganReason.SelectedValue)
            End If

        End If
    End Sub
    Private Sub switchEnabled(enb As Boolean, ORS As String)
        Select Case ORS
            Case ""
                switchEnabled(False, 3)
            Case 1 '(5)~(7)+(9)~(34)
                switchEnabled(False, 3)
                '/*===========New===========*/
                chkInValidFlag.Enabled = False '(8)特別處理false 
                chkInValidFlag.Checked = False
                'txtOrganID.Enabled = enb '新增可以輸入，只有修改頁不行
                'ucSelectOrgan.Visible = enb '新增可以輸入，只有修改頁不行
                txtOrganName.Enabled = enb
                txtOrganEngName.Enabled = enb
                chkVirtualFlag.Enabled = enb
                chkBranchFlag.Enabled = enb
                ddlOrgType.Enabled = enb
                ddlGroupID.Enabled = enb
                ddlUpOrganID.Enabled = enb
                ddlGroupType.Enabled = enb
                ddlDeptID.Enabled = enb
                ddlRoleCode.Enabled = enb
                ddlBossCompID.Enabled = enb
                txtBoss.Enabled = enb
                lblBossName.Enabled = enb
                ucQueryBoss.Visible = enb ''''
                ddlBossType.Enabled = enb
                ddlSecBossCompID.Enabled = enb
                txtSecBoss.Enabled = enb
                lblSecBossName.Enabled = enb
                ucSecQueryBoss.Visible = enb ''''
                chkBossTemporary.Enabled = enb
                '/*實體*/
                If hidOrganType.Value = "1" Then
                    txtPersonPart.Enabled = enb
                    ucPersonPart.Visible = enb
                    txtSecPersonPart.Enabled = enb
                    ucSecPersonPart.Visible = enb
                    ddlWorkSiteID.Enabled = enb
                    txtCheckPart.Enabled = enb
                    ucCheckPart.Visible = enb
                    'ddlPositionID.Enabled = enb '永久開啟
                    ucSelectPosition.Visible = enb ''uc''
                    ddlCostDeptID.Enabled = enb
                    'ddlWorkTypeID.Enabled = enb '永久開啟
                    ucSelectWorkType.Visible = enb ''uc''
                    txtAccountBranch.Enabled = enb
                    ddlCostType.Enabled = enb
                End If
                '/*簽核*/
                If hidOrganType.Value = "2" Then
                    'ddlFlowOrganID.Enabled = enb  '/*⑨*/ '永久開啟
                    ucFlowOrgan.Visible = enb
                    chkCompareFlag.Enabled = enb
                    chkDelegateFlag.Enabled = enb
                    chkOrganNo.Enabled = enb
                    ddlBusinessType.Enabled = enb
                End If
            Case 2 '(8)
                switchEnabled(False, 3)
                'chkInValidFlagOld.Enabled = enb
                '/*OldNew*/
                chkInValidFlag.Enabled = enb
            Case 3 '(6)~(34)
                chkInValidFlagOld.Enabled = False '(8)特別處理false 
                chkInValidFlagOld.Checked = False
                'txtOrganIDOld.Enabled = enb '新增可以輸入，只有修改頁不行
                'ucSelectOrganOld.Visible = enb '新增可以輸入，只有修改頁不行
                txtOrganNameOld.Enabled = enb
                txtOrganEngNameOld.Enabled = enb
                chkVirtualFlagOld.Enabled = enb
                chkBranchFlagOld.Enabled = enb
                ddlOrgTypeOld.Enabled = enb
                ddlGroupIDOld.Enabled = enb
                ddlUpOrganIDOld.Enabled = enb
                ddlGroupTypeOld.Enabled = enb
                ddlDeptIDOld.Enabled = enb
                ddlRoleCodeOld.Enabled = enb
                ddlBossCompIDOld.Enabled = enb
                txtBossOld.Enabled = enb
                lblBossNameOld.Enabled = enb
                ucQueryBossOld.Visible = enb ''''
                ddlBossTypeOld.Enabled = enb
                ddlSecBossCompIDOld.Enabled = enb
                txtSecBossOld.Enabled = enb
                lblSecBossNameOld.Enabled = enb
                ucSecQueryBossOld.Visible = enb ''''
                chkBossTemporaryOld.Enabled = enb
                '/*實體*/
                If hidOrganType.Value = "1" Or enb = False Then
                    txtPersonPartOld.Enabled = enb
                    ucPersonPartOld.Visible = enb
                    txtSecPersonPartOld.Enabled = enb
                    ucSecPersonPartOld.Visible = enb
                    ddlWorkSiteIDOld.Enabled = enb
                    txtCheckPartOld.Enabled = enb
                    ucCheckPartOld.Visible = enb
                    'ddlPositionIDOld.Enabled = enb '永久開啟
                    ucSelectPositionOld.Visible = enb ''uc''
                    ddlCostDeptIDOld.Enabled = enb
                    'ddlWorkTypeIDOld.Enabled = enb '永久開啟
                    ucSelectWorkTypeOld.Visible = enb ''uc''
                    txtAccountBranchOld.Enabled = enb
                    ddlCostTypeOld.Enabled = enb
                End If

                '/*簽核*/
                If hidOrganType.Value = "2" Or enb = False Then
                    'ddlFlowOrganIDOld.Enabled = enb  '/*⑨*/ '永久開啟
                    ucFlowOrganOld.Visible = enb
                    chkCompareFlagOld.Enabled = enb
                    chkDelegateFlagOld.Enabled = enb
                    chkOrganNoOld.Enabled = enb
                    ddlBusinessTypeOld.Enabled = enb
                End If
                '/*Old===========New===========*/
                chkInValidFlag.Enabled = False '(8)特別處理false 
                chkInValidFlag.Checked = False
                'txtOrganID.Enabled = enb '新增可以輸入，只有修改頁不行
                'ucSelectOrgan.Visible = enb '新增可以輸入，只有修改頁不行
                txtOrganName.Enabled = enb
                txtOrganEngName.Enabled = enb
                chkVirtualFlag.Enabled = enb
                chkBranchFlag.Enabled = enb
                ddlOrgType.Enabled = enb
                ddlGroupID.Enabled = enb
                ddlUpOrganID.Enabled = enb
                ddlGroupType.Enabled = enb
                ddlDeptID.Enabled = enb
                ddlRoleCode.Enabled = enb
                ddlBossCompID.Enabled = enb
                txtBoss.Enabled = enb
                lblBossName.Enabled = enb
                ucQueryBoss.Visible = enb ''''
                ddlBossType.Enabled = enb
                ddlSecBossCompID.Enabled = enb
                txtSecBoss.Enabled = enb
                lblSecBossName.Enabled = enb
                ucSecQueryBoss.Visible = enb ''''
                chkBossTemporary.Enabled = enb
                '/*實體*/
                If hidOrganType.Value = "1" Or enb = False Then
                    txtPersonPart.Enabled = enb
                    ucPersonPart.Visible = enb
                    txtSecPersonPart.Enabled = enb
                    ucSecPersonPart.Visible = enb
                    ddlWorkSiteID.Enabled = enb
                    txtCheckPart.Enabled = enb
                    ucCheckPart.Visible = enb
                    'ddlPositionID.Enabled = enb '永久開啟
                    ucSelectPosition.Visible = enb ''uc''
                    ddlCostDeptID.Enabled = enb
                    'ddlWorkTypeID.Enabled = enb '永久開啟
                    ucSelectWorkType.Visible = enb ''uc''
                    txtAccountBranch.Enabled = enb
                    ddlCostType.Enabled = enb
                End If
                '/*簽核*/
                If hidOrganType.Value = "2" Or enb = False Then
                    'ddlFlowOrganID.Enabled = enb  '/*⑨*/ '永久開啟
                    ucFlowOrgan.Visible = enb
                    chkCompareFlag.Enabled = enb
                    chkDelegateFlag.Enabled = enb
                    chkOrganNo.Enabled = enb
                    ddlBusinessType.Enabled = enb
                End If
        End Select
    End Sub
#Region "UC"
    Public Overrides Sub DoModalReturn(ByVal returnValue As String)
        Dim strSql As String = ""
        Dim strRstName1 As String = ""

        If returnValue <> "" Then
            Dim aryData() As String = returnValue.Split(":")
            Select Case aryData(0)
                Case "ucQueryBossOld" '部門主管
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    ddlBossCompIDOld.SelectedValue = aryValue(3)
                    txtBossOld.Text = aryValue(1)
                    lblBossNameOld.Text = aryValue(2)
                Case "ucSecQueryBossOld" '副主管
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    ddlSecBossCompIDOld.SelectedValue = aryValue(3)
                    txtSecBossOld.Text = aryValue(1)
                    lblSecBossNameOld.Text = aryValue(2)
                Case "ucPersonPartOld" '人事管理員
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    txtPersonPartOld.Text = aryValue(1)
                    lblPersonPartNameOld.Text = aryValue(2)
                Case "ucSecPersonPartOld" '第二人事
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    txtSecPersonPartOld.Text = aryValue(1)
                    lblSecPersonPartNameOld.Text = aryValue(2)
                Case "ucCheckPartOld" '自行查核主管
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    txtCheckPartOld.Text = aryValue(1)
                    lblCheckPartNameOld.Text = aryValue(2)
                    '/*----------------------------------*/
                Case "ucFlowOrganOld" '比對簽核單位
                    If aryData(1).Replace("'", "").Length = 0 Then
                        hidFlowOrganOld.Value = ""
                        ddlFlowOrganIDOld.Items.Clear()
                        ddlFlowOrganIDOld.Items.Insert(0, New ListItem("---請選擇---", ""))
                    Else
                        hidFlowOrganOld.Value = aryData(1)
                        Bsp.Utility.FillDDL(ddlFlowOrganIDOld, "eHRMSDB", "OrganizationFlow", "RTrim(OrganID)", "OrganName", Bsp.Utility.DisplayType.Full, "", "And OrganID In (" & aryData(1) & ")", "Order by OrganID")
                    End If
                    '/*=========================*/
                Case "ucSelectWorkTypeOld"     '工作性質
                    lblSelectWorkTypeIDOld.Text = aryData(1)

                    If lblSelectWorkTypeIDOld.Text <> "''" Then  '非必填時，回傳空值
                        '載入 工作性質 下拉式選單
                        strSql = " and WorkTypeID in (" + lblSelectWorkTypeIDOld.Text + ") and CompID = '" + hidCompID.Value + "'"
                        Bsp.Utility.WorkType(ddlWorkTypeIDOld, "WorkTypeID", , strSql)

                        '第一筆為主要工作性質
                        Dim strDefaultValue() As String = lblSelectWorkTypeIDOld.Text.Replace("'", "").Split(",")
                        Dim strWorkType As String = ""
                        Bsp.Utility.SetSelectedIndex(ddlWorkTypeIDOld, strDefaultValue(0))
                        For intLoop As Integer = 0 To strDefaultValue.GetUpperBound(0)
                            If intLoop = 0 Then
                                strWorkType = "1|" + strDefaultValue(intLoop)
                            Else
                                strWorkType = strWorkType + "|0|" + strDefaultValue(intLoop)
                            End If
                        Next
                        hidWorkTypeIDOld.Value = strWorkType
                    Else
                        ddlWorkTypeIDOld.Items.Clear()
                        ddlWorkTypeIDOld.Items.Insert(0, New ListItem("---請選擇---", ""))
                    End If

                    For i As Integer = 0 To ddlWorkTypeIDOld.Items.Count - 1
                        If ddlWorkTypeIDOld.Items(i).Selected Then
                            strRstName1 = ddlWorkTypeIDOld.Items(i).Text.Trim.Split("-")(1).ToString
                        End If
                    Next

                Case "ucSelectPositionOld"     '職位
                    lblSelectPositionIDOld.Text = aryData(1)

                    If lblSelectPositionIDOld.Text <> "''" Then  '非必填時，回傳空值
                        '載入 職位 下拉式選單
                        strSql = " and PositionID in (" + lblSelectPositionIDOld.Text + ") and CompID = '" + hidCompID.Value + "'"
                        Bsp.Utility.Position(ddlPositionIDOld, "PositionID", , strSql)
                        '第一筆為主要職位
                        Dim strDefaultValue() As String = lblSelectPositionIDOld.Text.Replace("'", "").Split(",")
                        Dim strPosition As String = ""
                        Bsp.Utility.SetSelectedIndex(ddlPositionIDOld, strDefaultValue(0))
                        For intLoop As Integer = 0 To strDefaultValue.GetUpperBound(0)
                            If intLoop = 0 Then
                                strPosition = "1|" + strDefaultValue(intLoop)
                            Else
                                strPosition = strPosition + "|0|" + strDefaultValue(intLoop)
                            End If
                        Next
                        hidPositionIDOld.Value = strPosition
                    Else
                        ddlPositionIDOld.Items.Clear()
                        ddlPositionIDOld.Items.Insert(0, New ListItem("---請選擇---", ""))
                    End If

                    For i As Integer = 0 To ddlPositionIDOld.Items.Count - 1
                        If ddlPositionIDOld.Items(i).Selected Then
                            strRstName1 = ddlPositionIDOld.Items(i).Text.Trim.Split("-")(1).ToString
                        End If
                    Next
                    '//以上Old以下New=====================================
                Case "ucQueryBoss" '部門主管
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    ddlBossCompID.SelectedValue = aryValue(3)
                    txtBoss.Text = aryValue(1)
                    lblBossName.Text = aryValue(2)
                Case "ucSecQueryBoss" '副主管
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    ddlSecBossCompID.SelectedValue = aryValue(3)
                    txtSecBoss.Text = aryValue(1)
                    lblSecBossName.Text = aryValue(2)
                Case "ucPersonPart" '人事管理員
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    txtPersonPart.Text = aryValue(1)
                    lblPersonPartName.Text = aryValue(2)
                Case "ucSecPersonPart" '第二人事
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    txtSecPersonPart.Text = aryValue(1)
                    lblsecPersonPartName.Text = aryValue(2)
                Case "ucCheckPart" '自行查核主管
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    txtCheckPart.Text = aryValue(1)
                    lblCheckPartName.Text = aryValue(2)
                    '/*----------------------------------*/
                Case "ucFlowOrgan" '比對簽核單位
                    If aryData(1).Replace("'", "").Length = 0 Then
                        hidFlowOrgan.Value = ""
                        ddlFlowOrganID.Items.Clear()
                        ddlFlowOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
                    Else
                        hidFlowOrgan.Value = aryData(1)
                        Bsp.Utility.FillDDL(ddlFlowOrganID, "eHRMSDB", "OrganizationFlow", "RTrim(OrganID)", "OrganName", Bsp.Utility.DisplayType.Full, "", "And OrganID In (" & aryData(1) & ")", "Order by OrganID")
                    End If
                    '/*=========================*/
                Case "ucSelectWorkType"     '工作性質
                    lblSelectWorkTypeID.Text = aryData(1)

                    If lblSelectWorkTypeID.Text <> "''" Then  '非必填時，回傳空值
                        '載入 工作性質 下拉式選單
                        strSql = " and WorkTypeID in (" + lblSelectWorkTypeID.Text + ") and CompID = '" + hidCompID.Value + "'"
                        Bsp.Utility.WorkType(ddlWorkTypeID, "WorkTypeID", , strSql)

                        '第一筆為主要工作性質
                        Dim strDefaultValue() As String = lblSelectWorkTypeID.Text.Replace("'", "").Split(",")
                        Dim strWorkType As String = ""
                        Bsp.Utility.SetSelectedIndex(ddlWorkTypeID, strDefaultValue(0))
                        For intLoop As Integer = 0 To strDefaultValue.GetUpperBound(0)
                            If intLoop = 0 Then
                                strWorkType = "1|" + strDefaultValue(intLoop)
                            Else
                                strWorkType = strWorkType + "|0|" + strDefaultValue(intLoop)
                            End If
                        Next
                        hidWorkTypeID.Value = strWorkType
                    Else
                        ddlWorkTypeID.Items.Clear()
                        ddlWorkTypeID.Items.Insert(0, New ListItem("---請選擇---", ""))
                    End If

                    For i As Integer = 0 To ddlWorkTypeID.Items.Count - 1
                        If ddlWorkTypeID.Items(i).Selected Then
                            strRstName1 = ddlWorkTypeID.Items(i).Text.Trim.Split("-")(1).ToString
                        End If
                    Next

                Case "ucSelectPosition"     '職位
                    lblSelectPositionID.Text = aryData(1)

                    If lblSelectPositionID.Text <> "''" Then  '非必填時，回傳空值
                        '載入 職位 下拉式選單
                        strSql = " and PositionID in (" + lblSelectPositionID.Text + ") and CompID = '" + hidCompID.Value + "'"
                        Bsp.Utility.Position(ddlPositionID, "PositionID", , strSql)
                        '第一筆為主要職位
                        Dim strDefaultValue() As String = lblSelectPositionID.Text.Replace("'", "").Split(",")
                        Dim strPosition As String = ""
                        Bsp.Utility.SetSelectedIndex(ddlPositionID, strDefaultValue(0))
                        For intLoop As Integer = 0 To strDefaultValue.GetUpperBound(0)
                            If intLoop = 0 Then
                                strPosition = "1|" + strDefaultValue(intLoop)
                            Else
                                strPosition = strPosition + "|0|" + strDefaultValue(intLoop)
                            End If
                        Next
                        hidPositionID.Value = strPosition
                    Else
                        ddlPositionID.Items.Clear()
                        ddlPositionID.Items.Insert(0, New ListItem("---請選擇---", ""))
                    End If

                    For i As Integer = 0 To ddlPositionID.Items.Count - 1
                        If ddlPositionID.Items(i).Selected Then
                            strRstName1 = ddlPositionID.Items(i).Text.Trim.Split("-")(1).ToString
                        End If
                    Next
            End Select

        End If
    End Sub
#Region "人名相關的CompID-DDL與TXT連動"
    '/*DDL*/
#Region "NameNew"
    Protected Sub ddlBossCompID_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlBossCompID.SelectedIndexChanged
        txtBoss.Text = ""
        lblBossName.Text = ""
    End Sub
    Protected Sub ddlSecBossCompID_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlSecBossCompID.SelectedIndexChanged
        txtSecBoss.Text = ""
        lblSecBossName.Text = ""
    End Sub

    Protected Sub txtBoss_TextChanged(sender As Object, e As System.EventArgs) Handles txtBoss.TextChanged
        If ddlBossCompID.SelectedValue <> "" And txtBoss.Text <> "" Then
            Dim objHR As New HR
            Dim rtnTable As DataTable = objHR.GetHREmpName(ddlBossCompID.SelectedValue, txtBoss.Text)
            If rtnTable.Rows.Count <= 0 Then
                lblBossName.Text = ""
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", "人事資料尚未建檔")
            Else
                lblBossName.Text = rtnTable.Rows(0).Item(0).Trim
            End If
        Else
            lblBossName.Text = ""
        End If
    End Sub

    Protected Sub txtSecBoss_TextChanged(sender As Object, e As System.EventArgs) Handles txtSecBoss.TextChanged
        If ddlSecBossCompID.SelectedValue <> "" And txtSecBoss.Text <> "" Then
            Dim objHR As New HR
            Dim rtnTable As DataTable = objHR.GetHREmpName(ddlSecBossCompID.SelectedValue, txtSecBoss.Text)
            If rtnTable.Rows.Count <= 0 Then
                lblSecBossName.Text = ""
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", "人事資料尚未建檔")
            Else
                lblSecBossName.Text = rtnTable.Rows(0).Item(0).Trim
            End If
        Else
            lblSecBossName.Text = ""
        End If
    End Sub

    Protected Sub txtPersonPart_TextChanged(sender As Object, e As System.EventArgs) Handles txtPersonPart.TextChanged
        If txtPersonPart.Text <> "" Then
            Dim objHR As New HR
            Dim rtnTable As DataTable = objHR.GetHREmpName(hidCompID.Value, txtPersonPart.Text)
            If rtnTable.Rows.Count <= 0 Then
                lblPersonPartName.Text = ""
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", "人事資料尚未建檔")
            Else
                lblPersonPartName.Text = rtnTable.Rows(0).Item(0).Trim
            End If
        Else
            lblPersonPartName.Text = ""
        End If
    End Sub

    Protected Sub txtSecPersonPart_TextChanged(sender As Object, e As System.EventArgs) Handles txtSecPersonPart.TextChanged
        If txtSecPersonPart.Text <> "" Then
            Dim objHR As New HR
            Dim rtnTable As DataTable = objHR.GetHREmpName(hidCompID.Value, txtSecPersonPart.Text)
            If rtnTable.Rows.Count <= 0 Then
                lblsecPersonPartName.Text = ""
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", "人事資料尚未建檔")
            Else
                lblsecPersonPartName.Text = rtnTable.Rows(0).Item(0).Trim
            End If
        Else
            lblsecPersonPartName.Text = ""
        End If
    End Sub

    Protected Sub txtCheckPart_TextChanged(sender As Object, e As System.EventArgs) Handles txtCheckPart.TextChanged
        If txtCheckPart.Text <> "" Then
            Dim objHR As New HR
            Dim rtnTable As DataTable = objHR.GetHREmpName(hidCompID.Value, txtCheckPart.Text)
            If rtnTable.Rows.Count <= 0 Then
                lblCheckPartName.Text = ""
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", "人事資料尚未建檔")
            Else
                lblCheckPartName.Text = rtnTable.Rows(0).Item(0).Trim
            End If
        Else
            lblCheckPartName.Text = ""
        End If
    End Sub
#End Region
#Region "NameOld"
    Protected Sub ddlBossCompIDOld_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlBossCompIDOld.SelectedIndexChanged
        txtBossOld.Text = ""
        lblBossNameOld.Text = ""
    End Sub
    Protected Sub ddlSecBossCompIDOld_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlSecBossCompIDOld.SelectedIndexChanged
        txtSecBossOld.Text = ""
        lblSecBossNameOld.Text = ""
    End Sub

    Protected Sub txtBossOld_TextChanged(sender As Object, e As System.EventArgs) Handles txtBossOld.TextChanged
        If ddlBossCompIDOld.SelectedValue <> "" And txtBossOld.Text <> "" Then
            Dim objHR As New HR
            Dim rtnTable As DataTable = objHR.GetHREmpName(ddlBossCompIDOld.SelectedValue, txtBossOld.Text)
            If rtnTable.Rows.Count <= 0 Then
                lblBossName.Text = ""
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", "人事資料尚未建檔")
            Else
                lblBossNameOld.Text = rtnTable.Rows(0).Item(0).Trim
            End If
        Else
            lblBossNameOld.Text = ""
        End If
    End Sub

    Protected Sub txtSecBossOld_TextChanged(sender As Object, e As System.EventArgs) Handles txtSecBossOld.TextChanged
        If ddlSecBossCompIDOld.SelectedValue <> "" And txtSecBossOld.Text <> "" Then
            Dim objHR As New HR
            Dim rtnTable As DataTable = objHR.GetHREmpName(ddlSecBossCompIDOld.SelectedValue, txtSecBossOld.Text)
            If rtnTable.Rows.Count <= 0 Then
                lblSecBossNameOld.Text = ""
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", "人事資料尚未建檔")
            Else
                lblSecBossNameOld.Text = rtnTable.Rows(0).Item(0).Trim
            End If
        Else
            lblSecBossNameOld.Text = ""
        End If
    End Sub

    Protected Sub txtPersonPartOld_TextChanged(sender As Object, e As System.EventArgs) Handles txtPersonPartOld.TextChanged
        If txtPersonPartOld.Text <> "" Then
            Dim objHR As New HR
            Dim rtnTable As DataTable = objHR.GetHREmpName(hidCompID.Value, txtPersonPartOld.Text)
            If rtnTable.Rows.Count <= 0 Then
                lblPersonPartNameOld.Text = ""
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", "人事資料尚未建檔")
            Else
                lblPersonPartNameOld.Text = rtnTable.Rows(0).Item(0).Trim
            End If
        Else
            lblPersonPartNameOld.Text = ""
        End If
    End Sub

    Protected Sub txtSecPersonPartOld_TextChanged(sender As Object, e As System.EventArgs) Handles txtSecPersonPartOld.TextChanged
        If txtSecPersonPart.Text <> "" Then
            Dim objHR As New HR
            Dim rtnTable As DataTable = objHR.GetHREmpName(hidCompID.Value, txtSecPersonPartOld.Text)
            If rtnTable.Rows.Count <= 0 Then
                lblSecPersonPartNameOld.Text = ""
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", "人事資料尚未建檔")
            Else
                lblSecPersonPartNameOld.Text = rtnTable.Rows(0).Item(0).Trim
            End If
        Else
            lblSecPersonPartNameOld.Text = ""
        End If
    End Sub

    Protected Sub txtCheckPartOld_TextChanged(sender As Object, e As System.EventArgs) Handles txtCheckPartOld.TextChanged
        If txtCheckPartOld.Text <> "" Then
            Dim objHR As New HR
            Dim rtnTable As DataTable = objHR.GetHREmpName(hidCompID.Value, txtCheckPartOld.Text)
            If rtnTable.Rows.Count <= 0 Then
                lblCheckPartNameOld.Text = ""
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", "人事資料尚未建檔")
            Else
                lblCheckPartNameOld.Text = rtnTable.Rows(0).Item(0).Trim
            End If
        Else
            lblCheckPartNameOld.Text = ""
        End If
    End Sub
#End Region
#End Region
#Region "Old"
    Protected Sub ucQueryBossOld_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ucQueryBossOld.Load
        '載入按鈕-部門主管選單畫面
        ucQueryBossOld.SelectCompID = ddlBossCompIDOld.SelectedValue
    End Sub
    Protected Sub ucSecQueryBossOld_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ucSecQueryBossOld.Load
        ucSecQueryBossOld.SelectCompID = ddlSecBossCompIDOld.SelectedValue
    End Sub

    Protected Sub ucPersonPartOld_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ucPersonPartOld.Load
        '載入按鈕-人事管理選單畫面
        ucPersonPartOld.SelectCompID = hidCompID.Value
    End Sub
    Protected Sub ucSecPersonPartOld_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ucSecPersonPartOld.Load
        '載入按鈕-第二人事管理選單畫面
        ucSecPersonPartOld.SelectCompID = hidCompID.Value
    End Sub
    '/*========職業、工作性質=================*/
    Protected Sub ucSelectPositionOld_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ucSelectPositionOld.Load
        '載入按鈕-職位選單畫面
        ucSelectPositionOld.QueryCompID = hidCompID.Value
        ucSelectPositionOld.QueryEmpID = ""
        ucSelectPositionOld.DefaultPosition = lblSelectPositionIDOld.Text
        ucSelectPositionOld.QueryOrganID = ""

        ucSelectPositionOld.Fields = New FieldState() { _
            New FieldState("PositionID", "職位代碼", True, True), _
            New FieldState("Remark", "職位名稱", True, True)}
    End Sub

    Protected Sub ucSelectWorkTypeOld_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ucSelectWorkTypeOld.Load
        '載入按鈕-工作性質選單畫面
        ucSelectWorkTypeOld.QueryCompID = hidCompID.Value
        ucSelectWorkTypeOld.QueryEmpID = ""
        ucSelectWorkTypeOld.DefaultWorkType = lblSelectWorkTypeIDOld.Text
        ucSelectWorkTypeOld.QueryOrganID = ""

        ucSelectWorkTypeOld.Fields = New FieldState() { _
            New FieldState("WorkTypeID", "工作性質代碼", True, True), _
            New FieldState("Remark", "工作性質名稱", True, True)}
    End Sub
    '載入按鈕-比對簽核單位畫面
    Protected Sub ucFlowOrganOld_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ucFlowOrganOld.Load

        ucFlowOrganOld.QueryCompID = ""
        ucFlowOrganOld.DefaultFlowOrgan = hidFlowOrganOld.Value
        ucFlowOrganOld.Fields = New FieldState() { _
            New FieldState("OrganID", "單位代碼", True, True), _
            New FieldState("OrganName", "單位名稱", True, True)}
    End Sub
#End Region

    '/*============*/
#Region "New"



    Protected Sub ucQueryBoss_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ucQueryBoss.Load
        '載入按鈕-部門主管選單畫面
        ucQueryBoss.SelectCompID = ddlBossCompID.SelectedValue
    End Sub
    Protected Sub ucSecQueryBoss_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ucSecQueryBoss.Load
        ucSecQueryBoss.SelectCompID = ddlSecBossCompID.SelectedValue
    End Sub

    Protected Sub ucPersonPart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ucPersonPart.Load
        '載入按鈕-人事管理選單畫面
        ucPersonPart.SelectCompID = hidCompID.Value
    End Sub
    Protected Sub ucSecPersonPart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ucSecPersonPart.Load
        '載入按鈕-第二人事管理選單畫面
        ucSecPersonPart.SelectCompID = hidCompID.Value
    End Sub
    '/*========職業、工作性質=================*/
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

    Public Overrides Sub DoAction(ByVal Param As String) '按鈕
        Select Case Param
            Case "btnUpdate"       '存檔返回
                If funCheckData() Then
                    If SaveData() Then
                        Bsp.Utility.ShowMessage(Me, "存檔成功，即將返回")
                        GoBack()
                    End If
                End If
            Case "btnActionC"    '存檔
                If funCheckData() Then
                    If SaveData() Then
                        Bsp.Utility.RunClientScript(Me.Page, "ActionVisable();")
                    End If
                End If
            Case "btnActionX"     '返回
                GoBack()
            Case "btnCancel"   '清除
                'ClearData()
                subGetData() 'ClearData只清除(6)以下，(2)~(5)額外清除
                GetSelectData(True)
        End Select
    End Sub
    Private Sub ClearData() 'ClearData只清除(6)以下，(2)~(5)需額外清除

        ddlCostType.SelectedValue = "" '寫死'
        ddlBossType.SelectedValue = "" '寫死'

        txtOrganName.Text = ""
        txtOrganEngName.Text = ""
        txtBoss.Text = ""
        txtPersonPart.Text = ""
        txtSecPersonPart.Text = ""
        txtCheckPart.Text = ""
        txtAccountBranch.Text = ""

        chkInValidFlag.Checked = False
        chkVirtualFlag.Checked = False
        chkBossTemporary.Checked = False
        chkCompareFlag.Checked = False
        chkDelegateFlag.Checked = False
        chkOrganNo.Checked = False

        chkBranchFlag.Checked = False
        lblBossName.Text = ""
        lblSecBossName.Text = ""
        lblPersonPartName.Text = ""
        lblsecPersonPartName.Text = ""

        '特殊uc與ddl刪除選項內容
        lblSelectPositionID.Text = ""
        lblSelectWorkTypeID.Text = ""
        hidFlowOrgan.Value = ""
        ddlPositionID.Items.Clear()
        ddlWorkTypeID.Items.Clear()
        ddlFlowOrganID.Items.Clear()
        '/*====================*/
        ddlCostTypeOld.SelectedValue = "" '寫死'
        ddlBossTypeOld.SelectedValue = "" '寫死'


        txtOrganNameOld.Text = ""
        txtOrganEngNameOld.Text = ""
        txtBossOld.Text = ""
        txtPersonPartOld.Text = ""
        txtSecPersonPartOld.Text = ""
        txtCheckPartOld.Text = ""
        txtAccountBranchOld.Text = ""

        chkInValidFlagOld.Checked = False
        chkVirtualFlagOld.Checked = False
        chkBossTemporaryOld.Checked = False
        chkCompareFlagOld.Checked = False
        chkDelegateFlagOld.Checked = False
        chkOrganNoOld.Checked = False

        chkBranchFlagOld.Checked = False
        lblBossNameOld.Text = ""
        lblSecBossNameOld.Text = ""
        lblPersonPartNameOld.Text = ""
        lblSecPersonPartNameOld.Text = ""


        '特殊uc與ddl刪除選項內容
        lblSelectPositionIDOld.Text = ""
        lblSelectWorkTypeIDOld.Text = ""
        hidFlowOrganOld.Value = ""
        ddlPositionIDOld.Items.Clear()
        ddlWorkTypeIDOld.Items.Clear()
        ddlFlowOrganIDOld.Items.Clear()
    End Sub
    Private Sub GoBack()
        Dim ti As TransferInfo = Me.StateTransfer
        Me.TransferFramePage(ti.CallerUrl, Nothing, ti.Args)
    End Sub

#Region "subGetData"
    Private Sub subGetClear()
        ddlOrgType.Items.Clear()
        ddlGroupID.Items.Clear()
        ddlUpOrganID.Items.Clear()
        ddlGroupType.Items.Clear()
        ddlDeptID.Items.Clear()
        ddlRoleCode.Items.Clear()
        ddlBossCompID.Items.Clear()
        ddlSecBossCompID.Items.Clear()
        ''/*----------------------行政組織資料----------------------*/
        ddlWorkSiteID.Items.Clear()
        ddlPositionID.Items.Clear()
        ddlCostDeptID.Items.Clear()
        ddlWorkTypeID.Items.Clear()
        ''/*----------------------功能組織資料----------------------*/
        ddlFlowOrganID.Items.Clear()
        ddlBusinessType.Items.Clear()
    End Sub
    Private Sub subGetData()
        Dim objOM1 As New OM1()
        'subGetClear()
        '單位類別
        If hidOrganType.Value = "2" Then
            OM1.FillDDL(ddlOrgTypeOld, " HRCodeMap ", " RTRIM(Code) ", " CodeCName ", OM1.DisplayType.Full, "", " AND TabName='OrganizationFlow_OrgType'  and FldName= '" + hidCompID.Value + "'  and NotShowFlag='0'  ", " order by SortFld ")
        Else
            OM1.FillDDL(ddlOrgTypeOld, " HRCodeMap ", " RTRIM(Code) ", " CodeCName ", OM1.DisplayType.Full, "", " AND TabName='Organization_OrgType'  and FldName= '" + hidCompID.Value + "'  and NotShowFlag='0'  ", " order by SortFld ")
        End If
        ddlOrgTypeOld.Items.Insert(0, New ListItem("---請選擇---", ""))
        '所屬事業群
        OM1.FillDDL(ddlGroupIDOld, " OrganizationFlow ", " RTRIM(GroupID) ", " OrganName ", OM1.DisplayType.Full, "", " AND OrganID = GroupID  ", " Order by GroupID ")
        ddlGroupIDOld.Items.Insert(0, New ListItem("---請選擇---", ""))

        '⑨上階部門-OrganID
        If hidOrganType.Value = "2" Then
            OM1.FillDDLOM1000(ddlUpOrganIDOld, " OrganizationFlow ", " RTRIM(OrganID) ", " Case when InValidFlag='0' then RTRIM(OrganName) else RTRIM(OrganName)+'(無效)' end ", " InValidFlag, VirtualFlag ", OM1.DisplayType.Full, "", "", " order by InValidFlag, RTRIM(OrganID) ")
        Else
            OM1.FillDDLOM1000(ddlUpOrganIDOld, " Organization ", " RTRIM(OrganID) ", " Case when InValidFlag='0' then RTRIM(OrganName) else RTRIM(OrganName)+'(無效)' end ", " InValidFlag, VirtualFlag ", OM1.DisplayType.Full, "", " and CompID = " + Bsp.Utility.Quote(hidCompID.Value) + " ", " order by InValidFlag, RTRIM(OrganID) ")
        End If
        ddlUpOrganIDOld.Items.Insert(0, New ListItem("---請選擇---", ""))

        '事業群類別
        OM1.FillDDL(ddlGroupTypeOld, " HRCodeMap ", " RTRIM(Code) ", " CodeCName ", OM1.DisplayType.Full, "", " AND TabName='Organization' and FldName='GroupType' and NotShowFlag='0'  ", " order by SortFld ")
        ddlGroupTypeOld.Items.Insert(0, New ListItem("---請選擇---", ""))

        '⑨所屬一級部門-OrganID
        If hidOrganType.Value = "2" Then
            OM1.FillDDLOM1000(ddlDeptIDOld, " OrganizationFlow ", " RTRIM(OrganID) ", " Case when InValidFlag='0' then RTRIM(OrganName) else RTRIM(OrganName)+'(無效)' end ", " InValidFlag , VirtualFlag", OM1.DisplayType.Full, "", " and OrganID=DeptID  ", " order by InValidFlag, VirtualFlag, RTRIM(OrganID) ")
        Else
            OM1.FillDDLOM1000(ddlDeptIDOld, " Organization ", " RTRIM(OrganID) ", " Case when InValidFlag='0' then RTRIM(OrganName) else RTRIM(OrganName)+'(無效)' end ", " InValidFlag , VirtualFlag", OM1.DisplayType.Full, "", " and CompID = " + Bsp.Utility.Quote(hidCompID.Value) + " and OrganID=DeptID ", " order by InValidFlag, VirtualFlag, RTRIM(OrganID) ")
        End If
        ddlDeptIDOld.Items.Insert(0, New ListItem("---請選擇---", ""))

        '部門主管角色
        If hidOrganType.Value = "2" Then
            OM1.FillDDL(ddlRoleCodeOld, " HRCodeMap ", " Code ", " CodeCName ", OM1.DisplayType.Full, "", " AND  TabName='OrganizationFlow'  and FldName = 'RoleCode' and NotShowFlag='0'  ", " order by SortFld ")
        Else
            OM1.FillDDL(ddlRoleCodeOld, " HRCodeMap ", " Code ", " CodeCName ", OM1.DisplayType.Full, "", " AND  TabName='Organization'  and FldName = 'RoleCode' and NotShowFlag='0'  ", " order by SortFld ")
        End If
        ddlRoleCodeOld.Items.Insert(0, New ListItem("---請選擇---", ""))
        '部門主管-公司代碼ddlBossCompID
        OM1.FillDDL(ddlBossCompIDOld, " Company ", " RTRIM(CompID)  ", " CompName ", OM1.DisplayType.Full, "", "", " order by RTRIM(CompID) ")
        ddlBossCompIDOld.Items.Insert(0, New ListItem("---請選擇---", ""))

        '副主管-公司代碼ddlSecBossCompID
        OM1.FillDDL(ddlSecBossCompIDOld, " Company ", " RTRIM(CompID)  ", " CompName ", OM1.DisplayType.Full, "", "", " order by RTRIM(CompID) ")
        ddlSecBossCompIDOld.Items.Insert(0, New ListItem("---請選擇---", ""))

        ''/*----------------------行政組織資料----------------------*/

        '工作地點 
        OM1.FillDDL(ddlWorkSiteIDOld, " WorkSite ", " RTRIM(WorkSiteID)  ", " Remark  ", OM1.DisplayType.Full, "", " and CompID=" + Bsp.Utility.Quote(hidCompID.Value) + "", " order by WorkSiteID ")
        ddlWorkSiteIDOld.Items.Insert(0, New ListItem("---請選擇---", ""))

        ''職位 DDL+UC
        ddlPositionIDOld.Items.Insert(0, New ListItem("---請選擇---", ""))

        '費用分攤部門
        OM1.FillDDLOM1000(ddlCostDeptIDOld, _
                            " (select OrganID,OrganName,InValidFlag,VirtualFlag from Organization where CompID = " + Bsp.Utility.Quote(hidCompID.Value) + "	and VirtualFlag <> '1') as T ", " OrganID ", " OrganName ", " '1' as number,InValidFlag,VirtualFlag ", OM1.DisplayType.Full, " union select CompID AS Code, CompName AS CodeName,CompID+'-'+CompName AS FullName , '2' as number, ' ' as InValidFlag, ' ' as VirtualFlag from(select CompID,CompName from Company where FeeShareFlag ='1') as S ", "", " order by number,InValidFlag,VirtualFlag,Code ")
        ddlCostDeptIDOld.Items.Insert(0, New ListItem("---請選擇---", ""))

        ''工作性質 DDL+UC
        ddlWorkTypeIDOld.Items.Insert(0, New ListItem("---請選擇---", ""))

        ''/*----------------------功能組織資料----------------------*/
        '比對簽核單位DDL+UC
        ddlFlowOrganIDOld.Items.Insert(0, New ListItem("---請選擇---", ""))

        '業務類別ddl
        OM1.FillDDL(ddlBusinessTypeOld, " HRCodeMap ", " RTRIM(Code) ", " CodeCName ", OM1.DisplayType.Full, "", " AND TabName='Business'  and FldName= 'BusinessType' ", " order by Code ")
        ddlBusinessTypeOld.Items.Insert(0, New ListItem("---請選擇---", ""))

        '/*New==================================================*/

        '⌳單位類別
        If hidOrganType.Value = "2" Then
            OM1.FillDDL(ddlOrgType, " HRCodeMap ", " RTRIM(Code) ", " CodeCName ", OM1.DisplayType.Full, "", " AND TabName='OrganizationFlow_OrgType'  and FldName= '" + hidCompID.Value + "'  and NotShowFlag='0'  ", " order by SortFld ")
        Else
            OM1.FillDDL(ddlOrgType, " HRCodeMap ", " RTRIM(Code) ", " CodeCName ", OM1.DisplayType.Full, "", " AND TabName='Organization_OrgType'  and FldName= '" + hidCompID.Value + "'  and NotShowFlag='0'  ", " order by SortFld ")
        End If
        ddlOrgType.Items.Insert(0, New ListItem("---請選擇---", ""))

        '⌳所屬事業群
        OM1.FillDDL(ddlGroupID, " OrganizationFlow ", " RTRIM(GroupID) ", " OrganName ", OM1.DisplayType.Full, "", " AND OrganID = GroupID  ", " Order by GroupID ")
        ddlGroupID.Items.Insert(0, New ListItem("---請選擇---", ""))

        '⑨上階部門-OrganID
        If hidOrganType.Value = "2" Then
            OM1.FillDDLOM1000(ddlUpOrganID, " OrganizationFlow ", " RTRIM(OrganID) ", " Case when InValidFlag='0' then RTRIM(OrganName) else RTRIM(OrganName)+'(無效)' end ", " InValidFlag, VirtualFlag ", OM1.DisplayType.Full, "", "", " order by InValidFlag, RTRIM(OrganID) ")
        Else
            OM1.FillDDLOM1000(ddlUpOrganID, " Organization ", " RTRIM(OrganID) ", " Case when InValidFlag='0' then RTRIM(OrganName) else RTRIM(OrganName)+'(無效)' end ", " InValidFlag, VirtualFlag ", OM1.DisplayType.Full, "", " and CompID = " + Bsp.Utility.Quote(hidCompID.Value) + " ", " order by InValidFlag, RTRIM(OrganID) ")
        End If
        ddlUpOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
        '事業群類別
        OM1.FillDDL(ddlGroupType, " HRCodeMap ", " RTRIM(Code) ", " CodeCName ", OM1.DisplayType.Full, "", " AND TabName='Organization' and FldName='GroupType' and NotShowFlag='0'  ", " order by SortFld ")
        ddlGroupType.Items.Insert(0, New ListItem("---請選擇---", ""))

        '⑨所屬一級部門-OrganID
        If hidOrganType.Value = "2" Then
            OM1.FillDDLOM1000(ddlDeptID, " OrganizationFlow ", " RTRIM(OrganID) ", " Case when InValidFlag='0' then RTRIM(OrganName) else RTRIM(OrganName)+'(無效)' end ", " InValidFlag , VirtualFlag", OM1.DisplayType.Full, "", " and OrganID=DeptID  ", " order by InValidFlag, VirtualFlag, RTRIM(OrganID) ")
        Else
            OM1.FillDDLOM1000(ddlDeptID, " Organization ", " RTRIM(OrganID) ", " Case when InValidFlag='0' then RTRIM(OrganName) else RTRIM(OrganName)+'(無效)' end ", " InValidFlag , VirtualFlag", OM1.DisplayType.Full, "", " and CompID = " + Bsp.Utility.Quote(hidCompID.Value) + " and OrganID=DeptID ", " order by InValidFlag, VirtualFlag, RTRIM(OrganID) ")
        End If
        ddlDeptID.Items.Insert(0, New ListItem("---請選擇---", ""))

        '部門主管角色
        If hidOrganType.Value = "2" Then
            OM1.FillDDL(ddlRoleCode, " HRCodeMap ", " Code ", " CodeCName ", OM1.DisplayType.Full, "", " AND  TabName='OrganizationFlow'  and FldName = 'RoleCode' and NotShowFlag='0'  ", " order by SortFld ")
        Else
            OM1.FillDDL(ddlRoleCode, " HRCodeMap ", " Code ", " CodeCName ", OM1.DisplayType.Full, "", " AND  TabName='Organization'  and FldName = 'RoleCode' and NotShowFlag='0'  ", " order by SortFld ")
        End If
        ddlRoleCode.Items.Insert(0, New ListItem("---請選擇---", ""))
        '部門主管-公司代碼ddlBossCompID
        OM1.FillDDL(ddlBossCompID, " Company ", " RTRIM(CompID)  ", " CompName ", OM1.DisplayType.Full, "", "", " order by RTRIM(CompID) ")
        ddlBossCompID.Items.Insert(0, New ListItem("---請選擇---", ""))

        '副主管-公司代碼ddlSecBossCompID
        OM1.FillDDL(ddlSecBossCompID, " Company ", " RTRIM(CompID)  ", " CompName ", OM1.DisplayType.Full, "", "", " order by RTRIM(CompID) ")
        ddlSecBossCompID.Items.Insert(0, New ListItem("---請選擇---", ""))

        ''/*----------------------行政組織資料----------------------*/

        '⌳工作地點 
        OM1.FillDDL(ddlWorkSiteID, " WorkSite ", " RTRIM(WorkSiteID)  ", " Remark  ", OM1.DisplayType.Full, "", " and CompID=" + Bsp.Utility.Quote(hidCompID.Value) + "", " order by WorkSiteID ")
        ddlWorkSiteID.Items.Insert(0, New ListItem("---請選擇---", ""))

        ''職位 DDL+UC
        ddlPositionID.Items.Insert(0, New ListItem("---請選擇---", ""))

        '⌳費用分攤部門
        OM1.FillDDLOM1000(ddlCostDeptID, _
                            " (select OrganID,OrganName,InValidFlag,VirtualFlag from Organization where CompID = " + Bsp.Utility.Quote(hidCompID.Value) + "	and VirtualFlag <> '1') as T ", " OrganID ", " OrganName ", " '1' as number,InValidFlag,VirtualFlag ", OM1.DisplayType.Full, " union select CompID AS Code, CompName AS CodeName,CompID+'-'+CompName AS FullName , '2' as number, ' ' as InValidFlag, ' ' as VirtualFlag from(select CompID,CompName from Company where FeeShareFlag ='1') as S ", "", " order by number,InValidFlag,VirtualFlag,Code ")
        ddlCostDeptID.Items.Insert(0, New ListItem("---請選擇---", ""))

        ''工作性質 DDL+UC
        ddlWorkTypeID.Items.Insert(0, New ListItem("---請選擇---", ""))

        ''/*----------------------功能組織資料----------------------*/
        '比對簽核單位DDL+UC
        ddlFlowOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))

        '業務類別ddl
        OM1.FillDDL(ddlBusinessType, " HRCodeMap ", " RTRIM(Code) ", " CodeCName ", OM1.DisplayType.Full, "", " AND TabName='Business'  and FldName= 'BusinessType' ", " order by Code ")
        ddlBusinessType.Items.Insert(0, New ListItem("---請選擇---", ""))
    End Sub
#End Region
    Private Sub GetSelectData(Optional ByVal Update As Boolean = False) '//True修改&明細畫面：正常讀取資料。False新增畫面：依照異動原因帶入前後筆資料
        Dim objOM As New OM1()
        Dim aryData() As String
        Dim strSQL As New StringBuilder()
        Dim Old As String = ""
        Dim Seq As String = ""
        Dim OrganReason As String = ""
        Try
            If Update Then
                Old = "Old"
                Seq = hidSeq.Value
                OrganReason = ddlOrganReason.SelectedValue
            Else
                Select Case ddlOrganReason.SelectedValue
                    Case "1"
                        Old = ""
                    Case "2"
                        Old = "Old"
                    Case "3"
                        Old = ""
                End Select
            End If
            If ddlOrganReason.SelectedValue <> "1" Then

                Using dt As DataTable = objOM.OM2200OrganizationLog(hidCompID.Value, OrganReason, hidOrganType.Value, ucValidDateB.DateText, IIf(Update, "2202", Old), hidOrganID.Value, Seq)
                    If dt.Rows().Count > 0 Then
                        If dt.Rows(0).Item("OrganName" & Old).ToString.Trim <> "" Then
                            '部門名稱
                            txtOrganNameOld.Text = dt.Rows(0).Item("OrganName" & Old).ToString.Trim
                            '部門英文名稱
                            txtOrganEngNameOld.Text = dt.Rows(0).Item("OrganEngName" & Old).ToString.Trim
                            '無效註記
                            If dt.Rows(0).Item("InValidFlag" & Old).ToString.Trim = "1" Then
                                chkInValidFlagOld.Checked = True
                            End If
                            '是否為虛擬部門
                            If dt.Rows(0).Item("VirtualFlag" & Old).ToString.Trim = "1" Then
                                chkVirtualFlagOld.Checked = True
                            End If
                            '分行註記rdo 0是/1否
                            If dt.Rows(0).Item("BranchFlag" & Old).ToString.Trim = "1" Then
                                chkBranchFlagOld.Checked = True
                            End If
                            '單位類別
                            Bsp.Utility.SetSelectedIndex(ddlOrgTypeOld, dt.Rows(0).Item("OrgType" & Old).ToString.Trim)

                            '所屬事業群
                            ddlGroupIDOld.SelectedValue = dt.Rows(0).Item("GroupID" & Old).ToString.Trim
                            '上階部門
                            Bsp.Utility.SetSelectedIndex(ddlUpOrganIDOld, dt.Rows(0).Item("UpOrganID" & Old).ToString.Trim)
                            '事業群類別
                            ddlGroupTypeOld.SelectedValue = dt.Rows(0).Item("GroupType" & Old).ToString.Trim
                            '所屬一部門
                            Bsp.Utility.SetSelectedIndex(ddlDeptIDOld, dt.Rows(0).Item("DeptID" & Old).ToString.Trim)
                            '部門主管角色
                            Bsp.Utility.SetSelectedIndex(ddlRoleCodeOld, dt.Rows(0).Item("RoleCode" & Old).ToString.Trim)
                            '部門主管公司代碼
                            ddlBossCompIDOld.SelectedValue = dt.Rows(0).Item("BossCompID" & Old).ToString.Trim
                            '部門主管
                            txtBossOld.Text = dt.Rows(0).Item("Boss" & Old).ToString.Trim
                            '部門主管姓名
                            lblBossNameOld.Text = dt.Rows(0).Item("PNameN" & Old).ToString.Trim
                            '主管任用方式
                            ddlBossTypeOld.SelectedValue = dt.Rows(0).Item("BossType" & Old).ToString.Trim
                            '副主管公司代碼
                            ddlSecBossCompIDOld.SelectedValue = dt.Rows(0).Item("SecBossCompID" & Old).ToString.Trim
                            '副主管
                            txtSecBossOld.Text = dt.Rows(0).Item("SecBoss" & Old).ToString.Trim
                            '副主管姓名
                            lblSecBossNameOld.Text = dt.Rows(0).Item("P2NameN" & Old).ToString.Trim
                            '主管暫代
                            If dt.Rows(0).Item("BossTemporary" & Old).ToString.Trim = "1" Then
                                chkBossTemporaryOld.Checked = True
                            End If
                            '/*----------------------實體組織資料----------------------*/
                            '人事管理員
                            txtPersonPartOld.Text = dt.Rows(0).Item("PersonPart" & Old).ToString.Trim
                            '人事管理員姓名
                            lblPersonPartNameOld.Text = dt.Rows(0).Item("P3NameN" & Old).ToString.Trim
                            '第二人事管理員
                            txtSecPersonPartOld.Text = dt.Rows(0).Item("SecPersonPart" & Old).ToString.Trim
                            '第二人事管理員姓名
                            lblSecPersonPartNameOld.Text = dt.Rows(0).Item("P4NameN" & Old).ToString.Trim
                            '工作地點
                            ddlWorkSiteIDOld.SelectedValue = dt.Rows(0).Item("WorkSiteID" & Old).ToString.Trim
                            '自行查核主管
                            txtCheckPartOld.Text = dt.Rows(0).Item("CheckPart" & Old).ToString.Trim
                            '自行查核主管姓名
                            lblCheckPartNameOld.Text = dt.Rows(0).Item("P5NameN" & Old).ToString.Trim
                            '職位
                            hidPositionIDOld.Value = dt.Rows(0).Item("PositionID" & Old).ToString.Trim()
                            aryData = Split(dt.Rows(0).Item("PositionID" & Old).ToString.Trim(), "|")
                            For ii = 0 To aryData.Length - 2 Step 2
                                If aryData(ii) = "0" Then
                                    lblSelectPositionIDOld.Text = lblSelectPositionIDOld.Text & ",'" & aryData(ii + 1) & "'"
                                ElseIf aryData(ii) = "1" Then
                                    lblSelectPositionIDOld.Text = "'" & aryData(ii + 1) & "'" & lblSelectPositionIDOld.Text
                                End If
                            Next
                            If lblSelectPositionIDOld.Text <> "" Then OM1.FillDDLOM1000(ddlPositionIDOld, " Position  ", " PositionID ", " Remark ", "", OM1.DisplayType.Full, _
                         "", " AND PositionID in (" & lblSelectPositionIDOld.Text & ")", "  ")

                            If ddlPositionIDOld.Items.Count = 0 Then
                                ddlPositionIDOld.Items.Insert(0, New ListItem("---請選擇---", ""))
                            End If
                            '費用分攤部門
                            ddlCostDeptIDOld.SelectedValue = dt.Rows(0).Item("CostDeptID" & Old).ToString.Trim

                            '工作性質
                            aryData = Nothing
                            hidWorkTypeIDOld.Value = dt.Rows(0).Item("WorkTypeID" & Old).ToString.Trim()
                            aryData = Split(dt.Rows(0).Item("WorkTypeID" & Old).ToString.Trim(), "|")
                            For ii = 0 To aryData.Length - 2 Step 2
                                If aryData(ii) = "0" Then
                                    lblSelectWorkTypeIDOld.Text = lblSelectWorkTypeIDOld.Text & ",'" & aryData(ii + 1) & "'"
                                ElseIf aryData(ii) = "1" Then
                                    lblSelectWorkTypeIDOld.Text = "'" & aryData(ii + 1) & "'" & lblSelectWorkTypeIDOld.Text
                                End If
                            Next
                            If lblSelectWorkTypeIDOld.Text <> "" Then OM1.FillDDLOM1000(ddlWorkTypeIDOld, " WorkType  ", " WorkTypeID ", " Remark ", "", OM1.DisplayType.Full, _
                         "", " AND WorkTypeID in (" & lblSelectWorkTypeIDOld.Text & ")", "  ")

                            If ddlWorkTypeIDOld.Items.Count = 0 Then
                                ddlWorkTypeIDOld.Items.Insert(0, New ListItem("---請選擇---", ""))
                            End If

                            '會計分行別
                            txtAccountBranchOld.Text = dt.Rows(0).Item("AccountBranch" & Old).ToString.Trim
                            '費用分攤科目別
                            ddlCostTypeOld.SelectedValue = dt.Rows(0).Item("CostType" & Old).ToString.Trim

                            '/*----------------------簽核組織資料----------------------*/
                            '比對簽核單位UC
                            If dt.Rows(0).Item("FlowOrganID" & Old).ToString.Trim = "" Then
                                ddlFlowOrganIDOld.Items.Clear()
                                ddlFlowOrganIDOld.Items.Insert(0, New ListItem("---請選擇---", ""))
                            Else
                                hidFlowOrganOld.Value = "'" & dt.Rows(0).Item("FlowOrganID" & Old).Replace("|", "','").ToString.Trim & "'"
                                If hidFlowOrganOld.Value <> "" Then Bsp.Utility.FillDDL(ddlFlowOrganIDOld, "eHRMSDB", "OrganizationFlow", "RTrim(OrganID)", "OrganName", Bsp.Utility.DisplayType.Full, "", "And OrganID In (" & hidFlowOrganOld.Value & ")", "Order by OrganID")
                            End If

                            'HR內部比對單位註記
                            If dt.Rows(0).Item("CompareFlag" & Old).ToString.Trim = "1" Then
                                chkCompareFlagOld.Checked = True
                            End If
                            '授權單位
                            If dt.Rows(0).Item("DelegateFlag" & Old).ToString.Trim = "1" Then
                                chkDelegateFlagOld.Checked = True
                            End If
                            '處級單位註記
                            If dt.Rows(0).Item("OrganNo" & Old).ToString.Trim = "1" Then
                                chkOrganNoOld.Checked = True
                            End If
                            '業務類別
                            ddlBusinessTypeOld.SelectedValue = dt.Rows(0).Item("BusinessType" & Old).ToString.Trim
                        End If
                    End If
                End Using
            End If
            '/*NEW=======================================================*/
            If Update Then
                Old = ""
            Else
                Select Case ddlOrganReason.SelectedValue
                    Case "1"
                        Old = "Old"
                    Case "2"
                        Old = ""
                    Case "3"
                        Old = "Old"
                End Select
            End If
            Using dt As DataTable = objOM.OM2200OrganizationLog(hidCompID.Value, OrganReason, hidOrganType.Value, ucValidDateB.DateText, IIf(Update, "2202", Old), hidOrganID.Value, Seq)
                If dt.Rows().Count > 0 Then
                    If dt.Rows(0).Item("OrganName" & Old).ToString.Trim <> "" Then
                        '部門名稱
                        txtOrganName.Text = dt.Rows(0).Item("OrganName" & Old).ToString.Trim
                        '部門英文名稱
                        txtOrganEngName.Text = dt.Rows(0).Item("OrganEngName" & Old).ToString.Trim
                        '無效註記
                        If dt.Rows(0).Item("InValidFlag" & Old).ToString.Trim = "1" Then
                            chkInValidFlag.Checked = True
                        End If
                        '是否為虛擬部門
                        If dt.Rows(0).Item("VirtualFlag" & Old).ToString.Trim = "1" Then
                            chkVirtualFlag.Checked = True
                        End If
                        '分行註記rdo 0是/1否
                        If dt.Rows(0).Item("BranchFlag" & Old).ToString.Trim = "1" Then
                            chkBranchFlag.Checked = True
                        End If
                        '單位類別
                        Bsp.Utility.SetSelectedIndex(ddlOrgType, dt.Rows(0).Item("OrgType" & Old).ToString.Trim)
                        '所屬事業群
                        ddlGroupID.SelectedValue = dt.Rows(0).Item("GroupID" & Old).ToString.Trim
                        '上階部門
                        Bsp.Utility.SetSelectedIndex(ddlUpOrganID, dt.Rows(0).Item("UpOrganID" & Old).ToString.Trim)
                        '事業群類別
                        ddlGroupType.SelectedValue = dt.Rows(0).Item("GroupType" & Old).ToString.Trim
                        '所屬一部門
                        Bsp.Utility.SetSelectedIndex(ddlDeptID, dt.Rows(0).Item("DeptID" & Old).ToString.Trim)
                        '部門主管角色
                        Bsp.Utility.SetSelectedIndex(ddlRoleCode, dt.Rows(0).Item("RoleCode" & Old).ToString.Trim)
                        '部門主管公司代碼
                        ddlBossCompID.SelectedValue = dt.Rows(0).Item("BossCompID" & Old).ToString.Trim
                        '部門主管
                        txtBoss.Text = dt.Rows(0).Item("Boss" & Old).ToString.Trim
                        '部門主管姓名
                        lblBossName.Text = dt.Rows(0).Item("PNameN" & Old).ToString.Trim
                        '主管任用方式
                        ddlBossType.SelectedValue = dt.Rows(0).Item("BossType" & Old).ToString.Trim
                        '副主管公司代碼
                        ddlSecBossCompID.SelectedValue = dt.Rows(0).Item("SecBossCompID" & Old).ToString.Trim
                        '副主管
                        txtSecBoss.Text = dt.Rows(0).Item("SecBoss" & Old).ToString.Trim
                        '副主管姓名
                        lblSecBossName.Text = dt.Rows(0).Item("P2NameN" & Old).ToString.Trim
                        '主管暫代
                        If dt.Rows(0).Item("BossTemporary" & Old).ToString.Trim = "1" Then
                            chkBossTemporary.Checked = True
                        End If
                        '/*----------------------實體組織資料----------------------*/
                        '人事管理員
                        txtPersonPart.Text = dt.Rows(0).Item("PersonPart" & Old).ToString.Trim
                        '人事管理員姓名
                        lblPersonPartName.Text = dt.Rows(0).Item("P3NameN" & Old).ToString.Trim
                        '第二人事管理員
                        txtSecPersonPart.Text = dt.Rows(0).Item("SecPersonPart" & Old).ToString.Trim
                        '第二人事管理員姓名
                        lblsecPersonPartName.Text = dt.Rows(0).Item("P4NameN" & Old).ToString.Trim
                        '工作地點
                        ddlWorkSiteID.SelectedValue = dt.Rows(0).Item("WorkSiteID" & Old).ToString.Trim
                        '自行查核主管
                        txtCheckPart.Text = dt.Rows(0).Item("CheckPart" & Old).ToString.Trim
                        '自行查核主管姓名
                        lblCheckPartName.Text = dt.Rows(0).Item("P5NameN" & Old).ToString.Trim
                        '職位
                        hidPositionID.Value = dt.Rows(0).Item("PositionID" & Old).ToString.Trim()
                        aryData = Split(dt.Rows(0).Item("PositionID" & Old).ToString.Trim(), "|")
                        For ii = 0 To aryData.Length - 2 Step 2
                            If aryData(ii) = "0" Then
                                lblSelectPositionID.Text = lblSelectPositionID.Text & ",'" & aryData(ii + 1) & "'"
                            ElseIf aryData(ii) = "1" Then
                                lblSelectPositionID.Text = "'" & aryData(ii + 1) & "'" & lblSelectPositionID.Text
                            End If
                        Next
                        If lblSelectPositionID.Text <> "" Then OM1.FillDDLOM1000(ddlPositionID, " Position  ", " PositionID ", " Remark ", "", OM1.DisplayType.Full, _
                     "", " AND PositionID in (" & lblSelectPositionID.Text & ")", "  ")

                        If ddlPositionID.Items.Count = 0 Then
                            ddlPositionID.Items.Insert(0, New ListItem("---請選擇---", ""))
                        Else
                            For ii = 0 To ddlPositionID.Items.Count - 1
                                If ii = 0 Then
                                    lblSelectPositionID.Text = "'" & ddlPositionID.Items(ii).Value & "'"
                                Else
                                    lblSelectPositionID.Text = lblSelectPositionID.Text & ",'" & ddlPositionID.Items(ii).Value & "'"
                                End If
                            Next
                        End If
                        '費用分攤部門
                        ddlCostDeptID.SelectedValue = dt.Rows(0).Item("CostDeptID" & Old).ToString.Trim

                        '工作性質
                        aryData = Nothing
                        hidWorkTypeID.Value = dt.Rows(0).Item("WorkTypeID" & Old).ToString.Trim()
                        aryData = Split(dt.Rows(0).Item("WorkTypeID" & Old).ToString.Trim(), "|")
                        For ii = 0 To aryData.Length - 2 Step 2
                            If aryData(ii) = "0" Then
                                lblSelectWorkTypeID.Text = lblSelectWorkTypeID.Text & ",'" & aryData(ii + 1) & "'"
                            ElseIf aryData(ii) = "1" Then
                                lblSelectWorkTypeID.Text = "'" & aryData(ii + 1) & "'" & lblSelectWorkTypeID.Text
                            End If
                        Next
                        If lblSelectWorkTypeID.Text <> "" Then OM1.FillDDLOM1000(ddlWorkTypeID, " WorkType  ", " WorkTypeID ", " Remark ", "", OM1.DisplayType.Full, _
                     "", " AND WorkTypeID in (" & lblSelectWorkTypeID.Text & ")", "  ")

                        If ddlWorkTypeID.Items.Count = 0 Then
                            ddlWorkTypeID.Items.Insert(0, New ListItem("---請選擇---", ""))
                        Else
                            For ii = 0 To ddlWorkTypeID.Items.Count - 1
                                If ii = 0 Then
                                    lblSelectWorkTypeID.Text = "'" & ddlWorkTypeID.Items(ii).Value & "'"
                                Else
                                    lblSelectWorkTypeID.Text = lblSelectWorkTypeID.Text & ",'" & ddlWorkTypeID.Items(ii).Value & "'"
                                End If
                            Next
                        End If

                        '會計分行別
                        txtAccountBranch.Text = dt.Rows(0).Item("AccountBranch" & Old).ToString.Trim
                        '費用分攤科目別
                        ddlCostType.SelectedValue = dt.Rows(0).Item("CostType" & Old).ToString.Trim

                        '/*----------------------簽核組織資料----------------------*/
                        '比對簽核單位UC
                        If dt.Rows(0).Item("FlowOrganID" & Old).ToString.Trim = "" Then
                            ddlFlowOrganID.Items.Clear()
                            ddlFlowOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
                        Else
                            hidFlowOrgan.Value = "'" & dt.Rows(0).Item("FlowOrganID" & Old).Replace("|", "','").ToString.Trim & "'"
                            If hidFlowOrgan.Value <> "" Then Bsp.Utility.FillDDL(ddlFlowOrganID, "eHRMSDB", "OrganizationFlow", "RTrim(OrganID)", "OrganName", Bsp.Utility.DisplayType.Full, "", "And OrganID In (" & hidFlowOrgan.Value & ")", "Order by OrganID")
                        End If

                        'HR內部比對單位註記
                        If dt.Rows(0).Item("CompareFlag" & Old).ToString.Trim = "1" Then
                            chkCompareFlag.Checked = True
                        End If
                        '授權單位
                        If dt.Rows(0).Item("DelegateFlag" & Old).ToString.Trim = "1" Then
                            chkDelegateFlag.Checked = True
                        End If
                        '處級單位註記
                        If dt.Rows(0).Item("OrganNo" & Old).ToString.Trim = "1" Then
                            chkOrganNo.Checked = True
                        End If
                        '業務類別
                        ddlBusinessType.SelectedValue = dt.Rows(0).Item("BusinessType" & Old).ToString.Trim
                    End If
                End If
            End Using
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & "GetSelectData", ex)
        End Try
    End Sub
#Region "DDL Position WorkType"
    Protected Sub ddlPositionID_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlPositionID.SelectedIndexChanged
        Dim strRst1 As String = ""
        Dim strRst2 As String = ""
        Dim strMainPosition As String = ""
        Dim strPosition As String = ""
        Dim strValue() As String
        If ucSelectPosition.Visible = False Then
            strValue = Split(Replace(lblSelectPositionID.Text, "'", ""), ",")
            ddlPositionID.SelectedValue = strValue(0)
        Else
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
        End If
    End Sub
    Protected Sub ddlPositionIDOld_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlPositionIDOld.SelectedIndexChanged
        Dim strRst1 As String = ""
        Dim strRst2 As String = ""
        Dim strMainPosition As String = ""
        Dim strPosition As String = ""
        Dim strValue() As String
        If ucSelectPositionOld.Visible = False Then
            strValue = Split(Replace(lblSelectPositionIDOld.Text, "'", ""), ",")
            ddlPositionIDOld.SelectedValue = strValue(0)
        Else
            For i As Integer = 0 To ddlPositionIDOld.Items.Count - 1
                If ddlPositionIDOld.Items(i).Selected Then
                    strRst1 = "'" + ddlPositionIDOld.Items(i).Value + "'"
                    strMainPosition = "1|" + ddlPositionIDOld.Items(i).Value
                Else
                    If strRst2 <> "" Then strRst2 += ","
                    strRst2 += "'" + ddlPositionIDOld.Items(i).Value + "'"
                    If strPosition <> "" Then strPosition += "|"
                    strPosition += "0|" + ddlPositionIDOld.Items(i).Value
                End If
            Next

            If strRst2 = "" Then
                lblSelectPositionIDOld.Text = strRst1
                hidPositionIDOld.Value = strMainPosition
            Else
                lblSelectPositionIDOld.Text = strRst1 + "," + strRst2
                hidPositionIDOld.Value = strMainPosition + "|" + strPosition
            End If
        End If
    End Sub
    Protected Sub ddlWorkType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlWorkTypeID.SelectedIndexChanged
        Dim strRst1 As String = ""
        Dim strRst2 As String = ""
        Dim strMainWorkType As String = ""
        Dim strWorkType As String = ""
        Dim strValue() As String
        If ucSelectWorkType.Visible = False Then
            strValue = Split(Replace(lblSelectWorkTypeID.Text, "'", ""), ",")
            ddlWorkTypeID.SelectedValue = strValue(0)
        Else
            For i As Integer = 0 To ddlWorkTypeID.Items.Count - 1
                If ddlWorkTypeID.Items(i).Selected Then
                    strRst1 = "'" + ddlWorkTypeID.Items(i).Value + "'"
                    strMainWorkType = "1|" + ddlWorkTypeID.Items(i).Value
                Else
                    If strRst2 <> "" Then strRst2 += ","
                    strRst2 += "'" + ddlWorkTypeID.Items(i).Value + "'"
                    If strWorkType <> "" Then strWorkType += "|"
                    strWorkType += "0|" + ddlWorkTypeID.Items(i).Value
                End If
            Next
            If strRst2 = "" Then
                lblSelectWorkTypeID.Text = strRst1
                hidWorkTypeID.Value = strMainWorkType
            Else
                lblSelectWorkTypeID.Text = strRst1 + "," + strRst2
                hidWorkTypeID.Value = strMainWorkType + "|" + strWorkType
            End If
        End If
    End Sub
    Protected Sub ddlWorkTypeOld_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlWorkTypeIDOld.SelectedIndexChanged
        Dim strRst1 As String = ""
        Dim strRst2 As String = ""
        Dim strMainWorkType As String = ""
        Dim strWorkType As String = ""
        Dim strValue() As String
        If ucSelectWorkTypeOld.Visible = False Then
            strValue = Split(Replace(lblSelectWorkTypeIDOld.Text, "'", ""), ",")
            ddlWorkTypeIDOld.SelectedValue = strValue(0)
        Else
            For i As Integer = 0 To ddlWorkTypeIDOld.Items.Count - 1
                If ddlWorkTypeIDOld.Items(i).Selected Then
                    strRst1 = "'" + ddlWorkTypeIDOld.Items(i).Value + "'"
                    strMainWorkType = "1|" + ddlWorkTypeIDOld.Items(i).Value
                Else
                    If strRst2 <> "" Then strRst2 += ","
                    strRst2 += "'" + ddlWorkTypeIDOld.Items(i).Value + "'"
                    If strWorkType <> "" Then strWorkType += "|"
                    strWorkType += "0|" + ddlWorkTypeIDOld.Items(i).Value
                End If
            Next
            If strRst2 = "" Then
                lblSelectWorkTypeIDOld.Text = strRst1
                hidWorkTypeIDOld.Value = strMainWorkType
            Else
                lblSelectWorkTypeIDOld.Text = strRst1 + "," + strRst2
                hidWorkTypeIDOld.Value = strMainWorkType + "|" + strWorkType
            End If
        End If
    End Sub
#End Region


#Region "funCheckData"
#Region "funCheckData"
    Private Function funCheckData() As Boolean
        Dim objOM1 As New OM1()
        'Dim temp As String = ddlOrganReason.SelectedValue.Trim
        Dim strWhere As String = ""
        Dim strWhere2 As String = ""
        'Dim strNowDate As String = Date.Now


        '異動原因-全套
        '/*-----------------異動原因-全套---------------------*/
        If ddlOrganReason.SelectedValue.Trim = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "異動原因")
            ddlOrganReason.Focus()
            Return False
        End If
        If ddlOrganReason.SelectedValue.Trim = "2" And chkInValidFlag.Checked = False Then
            Bsp.Utility.ShowMessage(Me, "組織無效須勾選無效註記")
            ddlOrganReason.Focus()
            Return False
        End If
        If ucValidDateB.DateText.Trim = "" Or Replace(ucValidDateB.DateText.Trim, "_", "") = "//" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "生效日期")
            ucValidDateB.Focus()
            Return False
        End If
        If ucValidDateE.DateText.Trim <> "" Then
            If ucValidDateB.DateText.Trim > ucValidDateE.DateText.Trim Then
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", "生效迄日必須大於生效日期")
                ucValidDateE.Focus()
                Return False
            End If
        End If
        '/*-----------------異動原因-全套---------------------*/
        'pnlKey
        If ddlOrganReason.SelectedValue = "3" Then

            '異動原因-組織無效：檢查到這
            If ddlOrganReason.SelectedValue = "2" And chkInValidFlag.Checked = True Then Return True
            '/*pnlOld0*/
            '*部門名稱
            If txtOrganNameOld.Text.Trim = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00030", "異動前-部門名稱")
                txtOrganNameOld.Focus()
                Return False
            End If

            '*部門英文名稱
            If Bsp.Utility.getStringLength(txtOrganEngNameOld.Text.Trim) > txtOrganEngNameOld.MaxLength Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00040", txtOrganEngNameOld.Text, txtOrganEngNameOld.MaxLength.ToString)
                txtOrganEngNameOld.Focus()
                Return False
            End If

            '單位類別
            If ddlOrgTypeOld.SelectedValue.Trim = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00035", "異動前-單位類別")
                ddlOrgTypeOld.Focus()
                Return False
            End If

            '所屬事業群
            If ddlGroupIDOld.SelectedValue.Trim = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00035", "異動前-所屬事業群")
                ddlGroupIDOld.Focus()
                Return False
            End If

            '*上階部門
            If ddlUpOrganIDOld.SelectedValue.Trim = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00035", "異動前-上階部門")
                ddlUpOrganIDOld.Focus()
                Return False
            End If

            '*事業群類別
            If ddlGroupTypeOld.SelectedValue.Trim = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00035", "異動前-事業群類別")
                ddlGroupTypeOld.Focus()
                Return False
            End If

            '*所屬一級部門
            If ddlDeptIDOld.SelectedValue.Trim = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00035", "異動前-所屬一級部門")
                ddlDeptIDOld.Focus()
                Return False
            End If

            '*部門主管角色
            If ddlRoleCodeOld.SelectedValue.Trim = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00035", "異動前-部門主管角色")
                ddlRoleCodeOld.Focus()
                Return False
            End If

            '*部門主管-公司代碼
            If ddlBossCompIDOld.SelectedValue.Trim = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00035", "異動前-部門主管公司代碼")
                ddlBossCompIDOld.Focus()
                Return False
            End If
            '*部門主管-員編
            If txtBossOld.Text.Trim = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00030", "異動前-部門主管員編")
                txtBossOld.Focus()
                Return False
            End If
            If txtBossOld.Text <> "" Then
                Dim objHR As New HR
                Dim rtnTable As DataTable = objHR.GetHREmpName(ddlBossCompIDOld.SelectedValue.Trim, txtBossOld.Text)
                If rtnTable.Rows.Count <= 0 Then
                    Bsp.Utility.ShowFormatMessage(Me, "H_00000", "異動前-部門主管員編不存在")
                    Return False
                End If
            End If
            '部門主管-人員名稱(不用)

            '主管任用方式
            If ddlBossTypeOld.SelectedValue = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00035", "異動前-主管任用方式")
                Return False
            End If

            '副主管
            If txtSecBossOld.Text <> "" Then
                If ddlBossCompIDOld.SelectedValue.Trim = "" Then
                    Bsp.Utility.ShowFormatMessage(Me, "W_00035", "異動前-副主管公司代碼")
                    Return False
                Else
                    Dim objHR As New HR
                    Dim rtnTable As DataTable = objHR.GetHREmpName(ddlSecBossCompIDOld.SelectedValue.Trim, txtSecBossOld.Text)
                    If rtnTable.Rows.Count <= 0 Then
                        Bsp.Utility.ShowFormatMessage(Me, "H_00000", "異動前-副主管員編不存在")
                        Return False
                    End If
                End If
            End If

            '/*'pnlOld0----------------------行政組織資料----------------------*/
            If hidOrganType.Value <> "2" Then '組織型態


                '人事管理員
                If txtPersonPartOld.Text <> "" Then
                    Dim objHR As New HR
                    Dim rtnTable As DataTable = objHR.GetHREmpName(UserProfile.SelectCompRoleID, txtPersonPartOld.Text)
                    If rtnTable.Rows.Count <= 0 Then
                        Bsp.Utility.ShowFormatMessage(Me, "H_00000", "異動前-副主管員編不存在")
                        Return False
                    End If
                End If

                '第二人事管理員
                If txtSecPersonPartOld.Text <> "" Then
                    Dim objHR As New HR
                    Dim rtnTable As DataTable = objHR.GetHREmpName(UserProfile.SelectCompRoleID, txtSecPersonPartOld.Text)
                    If rtnTable.Rows.Count <= 0 Then
                        Bsp.Utility.ShowFormatMessage(Me, "H_00000", "異動前-副主管員編不存在")
                        Return False
                    End If
                End If

                '自行查核主管
                If txtCheckPartOld.Text <> "" Then
                    Dim objHR As New HR
                    Dim rtnTable As DataTable = objHR.GetHREmpName(UserProfile.SelectCompRoleID, txtCheckPartOld.Text)
                    If rtnTable.Rows.Count <= 0 Then
                        Bsp.Utility.ShowFormatMessage(Me, "H_00000", "異動前-副主管員編不存在")
                        Return False
                    End If
                End If

                '工作地點ddlWorkSiteID
                If ddlWorkSiteIDOld.SelectedValue.Trim = "" Then
                    Bsp.Utility.ShowFormatMessage(Me, "W_00030", "異動前-工作地點")
                    ddlWorkSiteIDOld.Focus()
                    Return False
                End If

                '工作性質
                If ddlWorkTypeIDOld.SelectedValue.Trim = "" Then
                    Bsp.Utility.ShowFormatMessage(Me, "W_00030", "異動前-工作性質")
                    ddlWorkTypeIDOld.Focus()
                    Return False
                End If
            End If '組織型態
        End If '異動原因
        '/*--------------以上Old以下New----------------------*/
        If ddlOrganReason.SelectedValue <> "2" Then

            'pnlNew0
            '*部門名稱
            If txtOrganName.Text.Trim = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00030", "異動後-部門名稱")
                txtOrganName.Focus()
                Return False
            End If

            '*部門英文名稱
            If Bsp.Utility.getStringLength(txtOrganEngName.Text.Trim) > txtOrganEngName.MaxLength Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00040", txtOrganEngName.Text, txtOrganEngName.MaxLength.ToString)
                txtOrganEngName.Focus()
                Return False
            End If

            '單位類別
            If ddlOrgType.SelectedValue.Trim = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00030", "異動後-單位類別")
                ddlOrgType.Focus()
                Return False
            End If

            '所屬事業群
            If ddlGroupID.SelectedValue.Trim = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00030", "異動後-所屬事業群")
                ddlGroupID.Focus()
                Return False
            End If

            '*上階部門
            If ddlUpOrganID.SelectedValue.Trim = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00030", "異動後-上階部門")
                ddlUpOrganID.Focus()
                Return False
            End If

            '*事業群類別
            If ddlGroupType.SelectedValue.Trim = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00030", "異動後-事業群類別")
                ddlGroupType.Focus()
                Return False
            End If

            '*所屬一級部門
            If ddlDeptID.SelectedValue.Trim = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00030", "異動後-所屬一級部門")
                ddlDeptID.Focus()
                Return False
            End If

            '*部門主管角色
            If ddlRoleCode.SelectedValue.Trim = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00030", "異動後-部門主管角色")
                ddlRoleCode.Focus()
                Return False
            End If

            '*部門主管-公司代碼
            If ddlBossCompID.SelectedValue.Trim = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00035", "異動後-部門主管公司代碼")
                ddlBossCompID.Focus()
                Return False
            End If
            '*部門主管-員編
            If txtBoss.Text.Trim = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00030", "異動後-部門主管員編")
                txtBoss.Focus()
                Return False
            End If
            If txtBoss.Text <> "" Then
                Dim objHR As New HR
                Dim rtnTable As DataTable = objHR.GetHREmpName(ddlBossCompID.SelectedValue.Trim, txtBoss.Text)
                If rtnTable.Rows.Count <= 0 Then
                    Bsp.Utility.ShowFormatMessage(Me, "H_00000", "異動後-部門主管員編不存在")
                    Return False
                End If
            End If
            '部門主管-人員名稱(不用)

            '主管任用方式
            If ddlBossType.SelectedValue = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00035", "異動後-主管任用方式")
                Return False
            End If

            '副主管
            If txtSecBoss.Text <> "" Then
                If ddlBossCompID.SelectedValue.Trim = "" Then
                    Bsp.Utility.ShowFormatMessage(Me, "W_00035", "異動後-副主管公司代碼")
                    Return False
                Else
                    Dim objHR As New HR
                    Dim rtnTable As DataTable = objHR.GetHREmpName(ddlSecBossCompID.SelectedValue.Trim, txtSecBoss.Text)
                    If rtnTable.Rows.Count <= 0 Then
                        Bsp.Utility.ShowFormatMessage(Me, "H_00000", "異動後-副主管員編不存在")
                        Return False
                    End If
                End If
            End If

            '/*'pnlNew1----------------------行政組織資料----------------------*/
            If hidOrganType.Value <> "2" Then '組織型態
                '人事管理員
                If txtPersonPart.Text <> "" Then
                    Dim objHR As New HR
                    Dim rtnTable As DataTable = objHR.GetHREmpName(UserProfile.SelectCompRoleID, txtPersonPart.Text)
                    If rtnTable.Rows.Count <= 0 Then
                        Bsp.Utility.ShowFormatMessage(Me, "H_00000", "異動後-副主管員編不存在")
                        Return False
                    End If
                End If

                '第二人事管理員
                If txtSecPersonPart.Text <> "" Then
                    Dim objHR As New HR
                    Dim rtnTable As DataTable = objHR.GetHREmpName(UserProfile.SelectCompRoleID, txtSecPersonPart.Text)
                    If rtnTable.Rows.Count <= 0 Then
                        Bsp.Utility.ShowFormatMessage(Me, "H_00000", "異動後-副主管員編不存在")
                        Return False
                    End If
                End If

                '自行查核主管
                If txtCheckPart.Text <> "" Then
                    Dim objHR As New HR
                    Dim rtnTable As DataTable = objHR.GetHREmpName(UserProfile.SelectCompRoleID, txtCheckPart.Text)
                    If rtnTable.Rows.Count <= 0 Then
                        Bsp.Utility.ShowFormatMessage(Me, "H_00000", "異動後-副主管員編不存在")
                        Return False
                    End If
                End If

                '工作地點ddlWorkSiteID
                If ddlWorkSiteID.SelectedValue.Trim = "" Then
                    Bsp.Utility.ShowFormatMessage(Me, "W_00030", "異動後-工作地點")
                    ddlWorkSiteID.Focus()
                    Return False
                End If

                '工作性質
                If ddlWorkTypeID.SelectedValue.Trim = "" Then
                    Bsp.Utility.ShowFormatMessage(Me, "W_00030", "異動後-工作性質")
                    ddlWorkTypeID.Focus()
                    Return False
                End If
            End If '組織型態

        End If '異動原因
        Return True
    End Function

#End Region

#End Region

    Private Function SaveData() As Boolean
        Dim objOM1 As New OM1()

        Dim beOrganizationLog As New beOrganizationLog.Row()
        Dim bsOrganizationLog As New beOrganizationLog.Service()

        'beOrganizationLog的gencode取得值
        With beOrganizationLog
            .CompID.Value = hidCompID.Value
            .OrganReason.Value = ddlOrganReason.SelectedValue
            .OrganType.Value = hidOrganType.Value
            .ValidDateB.Value = ucValidDateB.DateText
            .Seq.Value = objOM1.GetSeq2("OrganizationLog", " And CompID = " & Bsp.Utility.Quote(hidCompID.Value.Trim) & " And OrganID = " & Bsp.Utility.Quote(hidOrganID.Value) & " And OrganReason = " & Bsp.Utility.Quote(ddlOrganReason.SelectedValue.Trim) & " And OrganType = " & Bsp.Utility.Quote(hidOrganType.Value.Trim) & " And ValidDateB = " & Bsp.Utility.Quote(ucValidDateB.DateText))
            If ucValidDateE.DateText <> "" Then
                .ValidDateE.Value = ucValidDateE.DateText
            End If
            .OrganID.Value = hidOrganID.Value
            'pnlOld0
            If ddlOrganReason.SelectedValue = "3" Then
                .OrganNameOld.Value = txtOrganNameOld.Text
                .OrganEngNameOld.Value = txtOrganEngNameOld.Text
                .InValidFlagOld.Value = IIf(chkInValidFlagOld.Checked, "1", "0")
                .VirtualFlagOld.Value = IIf(chkVirtualFlagOld.Checked, "1", "0")
                .BranchFlagOld.Value = IIf(chkBranchFlagOld.Checked, "1", "0")
                .OrgTypeOld.Value = ddlOrgTypeOld.SelectedValue
                .GroupIDOld.Value = ddlGroupIDOld.SelectedValue
                .UpOrganIDOld.Value = ddlUpOrganIDOld.SelectedValue
                .GroupTypeOld.Value = ddlGroupTypeOld.SelectedValue
                .DeptIDOld.Value = ddlDeptIDOld.SelectedValue
                .RoleCodeOld.Value = ddlRoleCodeOld.SelectedValue
                .BossCompIDOld.Value = ddlBossCompIDOld.SelectedValue
                .BossOld.Value = txtBossOld.Text
                .BossTypeOld.Value = ddlBossTypeOld.SelectedValue
                .SecBossCompIDOld.Value = ddlSecBossCompIDOld.SelectedValue
                .SecBossOld.Value = txtSecBossOld.Text
                .BossTemporaryOld.Value = IIf(chkBossTemporaryOld.Checked, "1", "0")
            ElseIf ddlOrganReason.SelectedValue = "2" Then
                .InValidFlagOld.Value = IIf(chkInValidFlagOld.Checked, "1", "0")
            End If
                'pnlOld1行政組織資料
                If hidOrganType.Value = "1" Then
                If ddlOrganReason.SelectedValue = "3" Then
                    .PersonPartOld.Value = txtPersonPartOld.Text
                    .SecPersonPartOld.Value = txtSecPersonPartOld.Text
                    .WorkSiteIDOld.Value = ddlWorkSiteIDOld.SelectedValue
                    .CheckPartOld.Value = txtCheckPartOld.Text
                    .PositionIDOld.Value = hidPositionIDOld.Value
                    .CostDeptIDOld.Value = ddlCostDeptIDOld.SelectedValue
                    .WorkTypeIDOld.Value = hidWorkTypeIDOld.Value
                    .AccountBranchOld.Value = txtAccountBranchOld.Text
                    .CostTypeOld.Value = ddlCostTypeOld.SelectedValue
                End If
            End If
            'pnlOld2功能組織資料
            If hidOrganType.Value = "2" Then
                If ddlOrganReason.SelectedValue = "3" Then
                    'uc-
                    Dim FlowOrganID As String = ""
                    For ii = 0 To ddlFlowOrganIDOld.Items.Count - 1
                        If ii = 0 Then
                            FlowOrganID = ddlFlowOrganIDOld.Items(ii).Value
                        Else
                            FlowOrganID = FlowOrganID & "|" & ddlFlowOrganIDOld.Items(ii).Value
                        End If
                    Next
                    .FlowOrganIDOld.Value = FlowOrganID
                    .CompareFlagOld.Value = IIf(chkCompareFlagOld.Checked, "1", "0")
                    .DelegateFlagOld.Value = IIf(chkDelegateFlagOld.Checked, "1", "0")
                    .OrganNoOld.Value = IIf(chkOrganNoOld.Checked, "1", "0")
                    .BusinessTypeOld.Value = ddlBusinessTypeOld.SelectedValue
                End If
            End If
            '/*========================================*/
            'pnlNew0
            If ddlOrganReason.SelectedValue <> "2" Then
                .OrganName.Value = txtOrganName.Text
                .OrganEngName.Value = txtOrganEngName.Text
                .InValidFlag.Value = IIf(chkInValidFlag.Checked, "1", "0")
                .VirtualFlag.Value = IIf(chkVirtualFlag.Checked, "1", "0")
                .BranchFlag.Value = IIf(chkBranchFlag.Checked, "1", "0")
                .OrgType.Value = ddlOrgType.SelectedValue
                .GroupID.Value = ddlGroupID.SelectedValue
                .UpOrganID.Value = ddlUpOrganID.SelectedValue
                .GroupType.Value = ddlGroupType.SelectedValue
                .DeptID.Value = ddlDeptID.SelectedValue
                .RoleCode.Value = ddlRoleCode.SelectedValue
                .BossCompID.Value = ddlBossCompID.SelectedValue
                .Boss.Value = txtBoss.Text
                .BossType.Value = ddlBossType.SelectedValue
                .SecBossCompID.Value = ddlSecBossCompID.SelectedValue
                .SecBoss.Value = txtSecBoss.Text
                .BossTemporary.Value = IIf(chkBossTemporary.Checked, "1", "0")
            Else
                .InValidFlag.Value = IIf(chkInValidFlag.Checked, "1", "0")
            End If
            'pnlNew1行政組織資料
            If hidOrganType.Value = "1" Then
                If ddlOrganReason.SelectedValue <> "2" Then
                    .PersonPart.Value = txtPersonPart.Text
                    .SecPersonPart.Value = txtSecPersonPart.Text
                    .WorkSiteID.Value = ddlWorkSiteID.SelectedValue
                    .CheckPart.Value = txtCheckPart.Text
                    .PositionID.Value = hidPositionID.Value
                    .CostDeptID.Value = ddlCostDeptID.SelectedValue
                    .WorkTypeID.Value = hidWorkTypeID.Value
                    .AccountBranch.Value = txtAccountBranch.Text
                    .CostType.Value = ddlCostType.SelectedValue
                End If
            End If

            'pnlNew2功能組織資料
            If hidOrganType.Value = "2" Then
                If ddlOrganReason.SelectedValue <> "2" Then
                    'uc-
                    Dim FlowOrganID As String = ""
                    For ii = 0 To ddlFlowOrganID.Items.Count - 1
                        If ii = 0 Then
                            FlowOrganID = ddlFlowOrganID.Items(ii).Value
                        Else
                            FlowOrganID = FlowOrganID & "|" & ddlFlowOrganID.Items(ii).Value
                        End If
                    Next
                    .FlowOrganID.Value = FlowOrganID
                    .CompareFlag.Value = IIf(chkCompareFlag.Checked, "1", "0")
                    .DelegateFlag.Value = IIf(chkDelegateFlag.Checked, "1", "0")
                    .OrganNo.Value = IIf(chkOrganNo.Checked, "1", "0")
                    .BusinessType.Value = ddlBusinessType.SelectedValue
                End If
            End If
            '額外輸入
            .LastChgComp.Value = UserProfile.ActCompID
            .LastChgID.Value = UserProfile.ActUserID
            .LastChgDate.Value = Now
        End With 'beOrganizationLog

        ''檢查資料是否存在
        'If bsOrganizationLog.IsDataExists(beOrganizationLog) Then
        '    Bsp.Utility.ShowFormatMessage(Me, "W_00010", "資料已存在")
        '    Return False
        'End If

        Try
            Return objOM1.UpdateOLAddition(beOrganizationLog, hidCompID.Value.ToString, hidOrganReason.Value.ToString, hidOrganType.Value.ToString, hidValidDateB.Value.ToString, hidOrganID.Value, hidSeq.Value.ToString)
        Catch ex As Exception
            Dim errLine As Integer = Convert.ToInt32(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(" ")))
            Bsp.Utility.ShowMessage(Me, "[SaveData]" & errLine & Bsp.Utility.getMessage("E_00000") & Bsp.Utility.getInnerException(Me.FunID, ex))
            Return False
        End Try
    End Function

    Protected Sub ValidDateB_Click(sender As Object, e As System.EventArgs) Handles ValidDateB.Click
       Dim objOM As New OM1
        If objOM.Check(ucValidDateB.DateText) Then
            Bsp.Utility.ShowFormatMessage(Me, "H_00000", objOM.rtError)
            ucValidDateB.DateText = objOM.DateCheck(ucValidDateB.DateText)
        End If
        ClearData()
        subGetData()
        GetSelectData(False)
    End Sub

    Protected Sub ddlOrganReason_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlOrganReason.SelectedIndexChanged
        If Not IsNothing(ddlOrganReason.SelectedValue) Then
            switchEnabled(True, ddlOrganReason.SelectedValue)
        End If
    End Sub
End Class