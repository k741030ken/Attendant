'****************************************************
'功能說明：待異動維護作業-新增
'建立人員：leo
'建立日期：2016.10
'****************************************************
Imports System.Data

Partial Class OM_OM1001
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            hidCompID.Value = UserProfile.SelectCompRoleID
            ucSelectOrgan.SelectCompID = UserProfile.SelectCompRoleID
            ucSelectOrgan.InValidFlag = "Y"
            ucSelectOrgan.Fields = New FieldState() { _
                New FieldState("Organization", "行政組織", True, True), _
                New FieldState("OrganizationFlow", "功能組織", True, True), _
                New FieldState("OrganizationWait", "待異動組織", True, True)}

            ucQueryBoss.ShowCompRole = "True"
            ucQueryBoss.InValidFlag = "N"
            ucQueryBoss.SelectCompID = ddlBossCompID.SelectedValue

            ucSecQueryBoss.ShowCompRole = "True"
            ucSecQueryBoss.InValidFlag = "N"
            ucSecQueryBoss.SelectCompID = ddlSecBossCompID.SelectedValue

            ucSecPersonPart.ShowCompRole = "False"
            ucSecPersonPart.InValidFlag = "N"
            ucSecPersonPart.SelectCompID = UserProfile.SelectCompRoleID

            ucPersonPart.ShowCompRole = "False"
            ucPersonPart.InValidFlag = "N"
            ucPersonPart.SelectCompID = UserProfile.SelectCompRoleID

            ucCheckPart.ShowCompRole = "False"
            ucCheckPart.InValidFlag = "N"
            ucCheckPart.SelectCompID = UserProfile.SelectCompRoleID

            ucSelectPosition.DefaultPosition = lblSelectPositionID.Text
            ucSelectPosition.QueryCompID = UserProfile.SelectCompRoleID
            ucSelectPosition.QueryEmpID = ""
            ucSelectPosition.QueryOrganID = ""
            ucSelectPosition.Fields = New FieldState() { _
            New FieldState("PositionID", "職位代碼", True, True), _
            New FieldState("Remark", "職位名稱", True, True)}

            ucSelectWorkType.DefaultWorkType = lblSelectWorkTypeID.Text
            ucSelectWorkType.QueryCompID = UserProfile.SelectCompRoleID
            ucSelectWorkType.QueryEmpID = ""
            ucSelectWorkType.QueryOrganID = ""
            ucSelectWorkType.Fields = New FieldState() { _
           New FieldState("WorkTypeID", "工作性質代碼", True, True), _
           New FieldState("Remark", "工作性質名稱", True, True)}

            ucFlowOrgan.DefaultFlowOrgan = hidFlowOrgan.Value
            ucFlowOrgan.QueryCompID = ""
            ucFlowOrgan.Fields = New FieldState() { _
           New FieldState("OrganID", "單位代碼", True, True), _
           New FieldState("OrganName", "單位名稱", True, True)}

        End If


    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Panel1.Visible = False
            Panel2.Visible = False
            Panel4.Visible = False
            ucSelectOrgan.Visible = False
            switchEnabled("")
            'Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)
            txtCompID.Text = UserProfile.SelectCompRoleName
            If Not hidCompID.Value = "SPHBK1" Then
                tdBusinessType1.Visible = False
                tdBusinessType2.Visible = False
            End If
            subGetData()
        End If
    End Sub
#Region "照片"
    '/*txtBoss_TextChanged沒有PhotoSelect()了*/
    Private Sub PhotoSelect()
        Panel4.Visible = True
        Dim objOM As New OM1
        Dim UpOrganID As String = IIf(ddlUpOrganID.SelectedItem.Text = "---<Self>---", txtOrganID.Text, ddlUpOrganID.SelectedValue.Trim)
        '[讀取資料] 
        '/*因為：萬一組織新增+上階部門選擇Self=Organiztion中找不到該部門與主管，這時直接套用目前部門主管Photo的內容，所以先做部門主管*/
        '//部門主管
        If ddlBossCompID.SelectedValue <> "" And txtBoss.Text <> "" Then
            OrganName.Text = txtOrganName.Text '1
            BossID.Text = txtBoss.Text '4
            BossName.Text = lblBossName.Text '5
            Rank.Text = "職等：" & objOM.GetPersonal("RankID", ddlBossCompID.SelectedValue, txtBoss.Text) '3
            Title.Text = "職稱：" & objOM.GetPersonal("TitleName", ddlBossCompID.SelectedValue, txtBoss.Text) '2
        End If

        '//上階部門主管ddlOrgType.SelectedItem.Text = "---<Self>---"
        If ddlUpOrganID.SelectedValue <> "(self)" And ddlUpOrganID.SelectedValue.Trim <> txtOrganID.Text Then
            UpOrganName.Text = objOM.GetOrganName(ddlOrganType.SelectedValue, hidCompID.Value, UpOrganID) '1
            UpBossID.Text = objOM.UpOrganEmpID(ddlOrganType.SelectedValue, hidCompID.Value, UpOrganID) '4
            UpBossName.Text = objOM.GetEmpName(hidCompID.Value, UpBossID.Text) '5
            UpRank.Text = "職等：" & objOM.GetPersonal("RankID", hidCompID.Value, UpBossID.Text) '3
            UpTitle.Text = "職稱：" & objOM.GetPersonal("TitleName", hidCompID.Value, UpBossID.Text) '2
        Else
            UpOrganName.Text = OrganName.Text '1
            UpBossID.Text = BossID.Text '4
            UpBossName.Text = BossName.Text '5
            UpRank.Text = Rank.Text '3
            UpTitle.Text = Title.Text '2
        End If

        '[讀取照片]()
        '因為沒有照片檔案, 偵錯時會卡很久故暫時註解
        '//上階部門主管
        Try
            imgPhoto.ImageUrl = New ST2().EmpPhotoQuery(IIf(ddlUpOrganID.SelectedItem.Text = "---<Self>---", hidCompID.Value, ddlBossCompID.SelectedValue.Trim), UpBossID.Text.Trim)
            imgPhoto.Visible = True
            lblPhoto_NoPic.Visible = False
        Catch ex As Exception
            imgPhoto.ImageUrl = ""
            imgPhoto.Visible = False
            lblPhoto_NoPic.Visible = True
            'Bsp.Utility.ShowMessage(Me, "上階部門主管沒有照片")
        End Try
        '//部門主管
        Try
            imgPhoto2.ImageUrl = New ST2().EmpPhotoQuery(ddlBossCompID.SelectedValue.Trim, txtBoss.Text.Trim)
            imgPhoto2.Visible = True
            lblPhoto_NoPic2.Visible = False
        Catch ex As Exception
            imgPhoto2.ImageUrl = ""
            imgPhoto2.Visible = False
            lblPhoto_NoPic2.Visible = True
            'Bsp.Utility.ShowMessage(Me, "部門主管沒有照片")
        End Try
    End Sub
#End Region

#Region "uc-uc相關物件"

#Region "uc-Case DoModalReturn"
    Public Overrides Sub DoModalReturn(ByVal returnValue As String)
        Dim objOM1 As New OM1
        Dim strSql As String = ""
        Dim strRstName1 As String = ""
        Dim strRstName2 As String = ""

        If returnValue <> "" Then
            Dim aryData() As String = returnValue.Split(":")
            Select Case aryData(0)

                Case "ucSelectOrgan" '單位代碼快速查詢
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    txtOrganID.Text = aryValue(2)
                    txtOrganName.Text = aryValue(3)
                    'ClearData()
                    'subGetData()
                    OrganSelectData(aryValue(4), hidCompID.Value, ddlOrganReason.SelectedValue, "", objOM1.DateCheck(ucValidDate.DateText), aryValue(2))
                    
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
                    lblSecPersonPartName.Text = aryValue(2)
                Case "ucCheckPart" '自行查核主管
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    txtCheckPart.Text = aryValue(1)
                    lblCheckPart.Text = aryValue(2)

                Case "ucFlowOrgan" '比對簽核單位
                    If aryData(1).Replace("'", "").Length = 0 Then
                        hidFlowOrgan.Value = ""
                        ddlFlowOrganID.Items.Clear()
                        ddlFlowOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
                    Else
                        hidFlowOrgan.Value = aryData(1)
                        Bsp.Utility.FillDDL(ddlFlowOrganID, "eHRMSDB", "OrganizationFlow", "RTrim(OrganID)", "OrganName", Bsp.Utility.DisplayType.Full, "", "And OrganID In (" & aryData(1) & ")", "Order by OrganID")
                    End If
                    ucFlowOrgan.DefaultFlowOrgan = hidFlowOrgan.Value

                    '/*=========================================*/

                Case "ucSelectPosition"
                    lblSelectPositionID.Text = aryData(1)
                    If lblSelectPositionID.Text <> "''" Then  '非必填時，回傳空值
                        '載入 職位 下拉式選單
                        strSql = "and PositionID in (" + lblSelectPositionID.Text + ") and CompID = '" + UserProfile.SelectCompRoleID + "'"
                        Bsp.Utility.Position(ddlPositionID, "PositionID", , strSql)
                        '第一筆為主要職位
                        Dim strDefaultValue() As String = lblSelectPositionID.Text.Replace("'", "").Split(",")
                        Dim strPosition As String = ""
                        Bsp.Utility.SetSelectedIndex(ddlPositionID, strDefaultValue(0))
                    Else
                        ddlPositionID.Items.Clear()
                        ddlPositionID.Items.Insert(0, New ListItem("---請選擇---", ""))
                    End If
                    ucSelectPosition.DefaultPosition = lblSelectPositionID.Text

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
                    ucSelectWorkType.DefaultWorkType = lblSelectWorkTypeID.Text
            End Select
        End If
    End Sub
#End Region

#Region "人名相關的CompID-DDL與TXT連動"
    Protected Sub ddlBossCompID_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlBossCompID.SelectedIndexChanged
        txtBoss.Text = ""
        lblBossName.Text = ""
        ucQueryBoss.SelectCompID = ddlBossCompID.SelectedValue
    End Sub
    Protected Sub ddlSecBossCompID_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlSecBossCompID.SelectedIndexChanged
        txtSecBoss.Text = ""
        lblSecBossName.Text = ""
        ucSecQueryBoss.SelectCompID = ddlSecBossCompID.SelectedValue
    End Sub
    Protected Sub txtBoss_TextChanged(sender As Object, e As System.EventArgs) Handles txtBoss.TextChanged
        If ddlBossCompID.SelectedValue <> "" And txtBoss.Text <> "" Then
            Dim objHR As New HR

            Dim rtnTable As DataTable = objHR.GetHREmpName(ddlBossCompID.SelectedValue, txtBoss.Text)
            If rtnTable.Rows.Count <= 0 Then
                lblBossName.Text = ""
                Bsp.Utility.ShowFormatMessage(Me, "W_00020", "部門主管")
            Else
                lblBossName.Text = rtnTable.Rows(0).Item(0)
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
                Bsp.Utility.ShowFormatMessage(Me, "W_00020", "副主管")
            Else
                lblSecBossName.Text = rtnTable.Rows(0).Item(0)
            End If
        Else
            lblSecBossName.Text = ""
        End If
    End Sub
    Protected Sub txtPersonPart_TextChanged(sender As Object, e As System.EventArgs) Handles txtPersonPart.TextChanged
        If txtPersonPart.Text <> "" Then
            Dim objHR As New HR
            Dim rtnTable As DataTable = objHR.GetHREmpName(UserProfile.SelectCompRoleID, txtPersonPart.Text)
            If rtnTable.Rows.Count <= 0 Then
                lblPersonPartName.Text = ""
                Bsp.Utility.ShowFormatMessage(Me, "W_00020", "人事管理員")
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
            Dim rtnTable As DataTable = objHR.GetHREmpName(UserProfile.SelectCompRoleID, txtSecPersonPart.Text)
            If rtnTable.Rows.Count <= 0 Then
                lblSecPersonPartName.Text = ""
                Bsp.Utility.ShowFormatMessage(Me, "W_00020", "第二人事管理員")
            Else
                lblSecPersonPartName.Text = rtnTable.Rows(0).Item(0).Trim
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
                lblCheckPart.Text = ""
                Bsp.Utility.ShowFormatMessage(Me, "W_00020", "自行查核主管員編")
            Else
                lblCheckPart.Text = rtnTable.Rows(0).Item(0).Trim
            End If
        Else
            lblCheckPart.Text = ""
        End If
    End Sub
#End Region

#Region "異動後資料，將下拉式選擇那筆 改為 第一筆為主要"
    '異動後資料-工作性質選單:將選擇那筆 改為 第一筆為主要工作性質
    Protected Sub ddlWorkType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlWorkTypeID.SelectedIndexChanged
        Dim strRst1 As String = ""
        Dim strRst2 As String = ""
        Dim strValue() As String
        If ucSelectWorkType.Visible = False Then
            strValue = Split(Replace(lblSelectWorkTypeID.Text, "'", ""), ",")
            ddlWorkTypeID.SelectedValue = strValue(0)
        Else
            For i As Integer = 0 To ddlWorkTypeID.Items.Count - 1
                If ddlWorkTypeID.Items(i).Selected Then
                    strRst1 = "'" + ddlWorkTypeID.Items(i).Value + "'"
                Else
                    If strRst2 <> "" Then strRst2 += ","
                    strRst2 += "'" + ddlWorkTypeID.Items(i).Value + "'"
                End If
            Next
            If strRst2 = "" Then
                lblSelectWorkTypeID.Text = strRst1
            Else
                lblSelectWorkTypeID.Text = strRst1 + "," + strRst2
            End If
        End If
        ucSelectWorkType.DefaultWorkType = lblSelectWorkTypeID.Text
    End Sub
    '異動後資料-職位下拉選單:將選擇那筆 改為 第一筆為主要職位
    Protected Sub ddlPositionID_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlPositionID.SelectedIndexChanged
        Dim strRst1 As String = ""
        Dim strRst2 As String = ""
        Dim strValue() As String
        If ucSelectPosition.Visible = False Then
            strValue = Split(Replace(lblSelectPositionID.Text, "'", ""), ",")
            ddlPositionID.SelectedValue = strValue(0)
        Else
            For i As Integer = 0 To ddlPositionID.Items.Count - 1
                If ddlPositionID.Items(i).Selected Then
                    strRst1 = "'" + ddlPositionID.Items(i).Value + "'"
                Else
                    If strRst2 <> "" Then strRst2 += ","
                    strRst2 += "'" + ddlPositionID.Items(i).Value + "'"
                End If
            Next
            If strRst2 = "" Then
                lblSelectPositionID.Text = strRst1
            Else
                lblSelectPositionID.Text = strRst1 + "," + strRst2
            End If
        End If
        ucSelectPosition.DefaultPosition = lblSelectPositionID.Text
    End Sub
#End Region

#End Region

#Region "GetSelectData"
    '方便OrganType與SQL-From判別
    Private Function TableFrom(ByVal strFrom As String) As String
        Select Case strFrom
            Case "1"
                strFrom = "Organization"
            Case "2"
                strFrom = "OrganizationFlow"
            Case "3"
                strFrom = "Organization"
            Case ""
                strFrom = "OrganizationWait"
        End Select
        Return strFrom
    End Function
    Private Sub OrganSelectData(ByVal strFrom As String, ByVal CompID As String, ByVal OrganReason As String, ByVal OrganType As String, ByVal ValidDate As String, ByVal OrganID As String)
        'ViewState("testtest") = ViewState("testtest") + 1
        'TextBox1.Text = ViewState("testtest")
        Dim objOM1 As New OM1()
        OrganReason = objOM1.OrganTypeOrganReasonToNo("", OrganReason)
        Dim arySQL() As String = {TableFrom(strFrom), CompID, OrganReason, OrganType, ValidDate, OrganID}

        '多次修改後的不智慧做法
        Select Case TableFrom(strFrom)
            Case "Organization"
                arySQL(1) = ""
                arySQL(2) = ""
                arySQL(3) = ""
                arySQL(4) = ""
            Case "OrganizationFlow"
                arySQL(1) = CompID
                arySQL(2) = ""
                arySQL(3) = ""
                arySQL(4) = ""
        End Select

        Try
            Using dt As DataTable = objOM1.QueryOrganizationByDetail(arySQL(0), arySQL(1), arySQL(2), arySQL(3), arySQL(4), arySQL(5))
                If dt.Rows.Count <= 0 Then
                    Exit Sub
                End If
                'If (strFrom = "1" Or strFrom = "2" Or strFrom = "3") And (OrganReason = " 3") Then
                If Not ((strFrom = "1" Or strFrom = "2" Or strFrom = "3") And (OrganReason = "3")) Then
                    '部門名稱
                    txtOrganName.Text = dt.Rows(0).Item("OrganName").ToString
                    '部門名稱Old
                    hidOrganNameOld.Value = dt.Rows(0).Item("OrganName").ToString
                    '部門英文名稱
                    txtOrganEngName.Text = dt.Rows(0).Item("OrganEngName").ToString
                    '無效註記
                    If ddlOrganReason.SelectedValue = "1" Or ddlOrganReason.SelectedValue = "3" Then
                        chkInValidFlag.Checked = False
                    Else
                        If dt.Rows(0).Item("InValidFlag").ToString = "1" Then
                            chkInValidFlag.Checked = True
                        End If
                    End If
                    '是否為虛擬部門
                    If dt.Rows(0).Item("VirtualFlag").ToString = "1" Then
                        chkVirtualFlag.Checked = True
                    End If
                    '分行註記rdo 0是/1否
                    If dt.Rows(0).Item("BranchFlag").ToString = "1" Then
                        chkBranchFlag.Checked = True
                    End If
                    '單位類別
                    If dt.Rows(0).Item("OrganID").ToString.Trim = dt.Rows(0).Item("OrgType").ToString.Trim Then
                        Bsp.Utility.SetSelectedIndex(ddlOrgType, "(self)")
                    Else
                        Bsp.Utility.SetSelectedIndex(ddlOrgType, dt.Rows(0).Item("OrgType").ToString.Trim)
                    End If
                    '所屬事業群
                    Bsp.Utility.SetSelectedIndex(ddlGroupID, dt.Rows(0).Item("GroupID").ToString.Trim)
                    '上階部門
                    If dt.Rows(0).Item("OrganID").ToString.Trim = dt.Rows(0).Item("UpOrganID").ToString.Trim Then
                        Bsp.Utility.SetSelectedIndex(ddlUpOrganID, "(self)")
                    Else
                        Bsp.Utility.SetSelectedIndex(ddlUpOrganID, dt.Rows(0).Item("UpOrganID").ToString.Trim)
                    End If
                    '事業群類別
                    Bsp.Utility.SetSelectedIndex(ddlGroupType, dt.Rows(0).Item("GroupType").ToString.Trim)
                    '所屬一部門
                    If dt.Rows(0).Item("OrganID").ToString.Trim = dt.Rows(0).Item("DeptID").ToString.Trim Then
                        Bsp.Utility.SetSelectedIndex(ddlDeptID, "(self)")
                    Else
                        Bsp.Utility.SetSelectedIndex(ddlDeptID, dt.Rows(0).Item("DeptID").ToString.Trim)
                    End If
                    ''部門主管角色
                    'Bsp.Utility.SetSelectedIndex(ddlRoleCode, dt.Rows(0).Item("RoleCode").ToString.Trim)
                    'ddlRoleCode.SelectedValue = dt.Rows(0).Item("RoleCode").ToString.Trim
                    '部門主管公司代碼
                    Bsp.Utility.SetSelectedIndex(ddlBossCompID, dt.Rows(0).Item("BossCompID").ToString.Trim)
                    '部門主管
                    txtBoss.Text = dt.Rows(0).Item("Boss").ToString.Trim
                    '部門主管姓名
                    lblBossName.Text = dt.Rows(0).Item("PNameN").ToString.Trim
                    '主管任用方式
                    Bsp.Utility.SetSelectedIndex(ddlBossType, dt.Rows(0).Item("BossType").ToString.Trim)
                    '副主管公司代碼
                    Bsp.Utility.SetSelectedIndex(ddlSecBossCompID, dt.Rows(0).Item("SecBossCompID").ToString.Trim)
                    '副主管
                    txtSecBoss.Text = dt.Rows(0).Item("SecBoss").ToString.Trim
                    '副主管姓名
                    lblSecBossName.Text = dt.Rows(0).Item("P2NameN").ToString.Trim
                    '主管暫代
                    If dt.Rows(0).Item("BossTemporary").ToString.Trim = "1" Then
                        chkBossTemporary.Checked = True
                    End If
                End If 'strFrom與OrganReason
                '/*----------------------行政組織資料----------------------*/
                If ddlOrganType.SelectedValue <> "2" And arySQL(0) <> "OrganizationFlow" Then
                    '人事管理員
                    txtPersonPart.Text = dt.Rows(0).Item("PersonPart").ToString.Trim
                    '人事管理員姓名
                    lblPersonPartName.Text = dt.Rows(0).Item("P3NameN").ToString.Trim
                    '第二人事管理員
                    txtSecPersonPart.Text = dt.Rows(0).Item("SecPersonPart").ToString.Trim
                    '第二人事管理員姓名
                    lblSecPersonPartName.Text = dt.Rows(0).Item("P4NameN").ToString.Trim
                    '工作地點
                    Bsp.Utility.SetSelectedIndex(ddlWorkSiteID, dt.Rows(0).Item("WorkSiteID").ToString.Trim)
                    '自行查核主管
                    txtCheckPart.Text = dt.Rows(0).Item("CheckPart").ToString.Trim
                    '自行查核主管姓名
                    lblCheckPart.Text = dt.Rows(0).Item("P5NameN").ToString.Trim
                    '職位
                    If txtOrganID.Text <> "" Then
                        OM1.FillDDLOM1000(ddlPositionID, arySQL(0).Replace("Organization", "OrgPosition") & " E", " E.PositionID ", " P.Remark ", "E.PrincipalFlag", OM1.DisplayType.Full, _
                       " inner join Position P on E.CompID = P.CompID and E.PositionID = P.PositionID ", _
                       " and E.CompID = '" & hidCompID.Value & "' and E.OrganID = '" & txtOrganID.Text & "' ", " order by E.PrincipalFlag desc , E.PositionID ")
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
                    End If
                    ucSelectPosition.DefaultPosition = lblSelectPositionID.Text

                    '費用分攤部門
                    If dt.Rows(0).Item("OrganID").ToString.Trim = dt.Rows(0).Item("CostDeptID").ToString.Trim Then
                        Bsp.Utility.SetSelectedIndex(ddlCostDeptID, "(self)")
                    Else
                        Bsp.Utility.SetSelectedIndex(ddlCostDeptID, dt.Rows(0).Item("CostDeptID").ToString.Trim)
                    End If
                    'ddlCostDeptID.SelectedValue = dt.Rows(0).Item("CostDeptID").ToString.Trim
                    '工作性質
                    If txtOrganID.Text <> "" Then
                        OM1.FillDDLOM1000(ddlWorkTypeID, arySQL(0).Replace("Organization", "OrgWorkType") & "  E", " E.WorkTypeID ", " W.Remark ", "E.PrincipalFlag", OM1.DisplayType.Full, _
                      " inner join WorkType W on E.CompID = W.CompID and E.WorkTypeID  = W.WorkTypeID  ", _
                      " and E.CompID = '" & hidCompID.Value & "' and E.OrganID = '" & txtOrganID.Text & "' ", " order by E.PrincipalFlag desc , E.WorkTypeID  ")
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
                    End If
                    ucSelectWorkType.DefaultWorkType = lblSelectWorkTypeID.Text

                    '會計分行別
                    txtAccountBranch.Text = dt.Rows(0).Item("AccountBranch").ToString.Trim
                    '費用分攤科目別
                    ddlCostType.SelectedValue = dt.Rows(0).Item("CostType").ToString.Trim
                    '部門主管角色
                    Bsp.Utility.SetSelectedIndex(ddlRoleCode, dt.Rows(0).Item("RoleCode").ToString.Trim)
                End If
                ''/*----------------------功能組織資料(待異動)----------------------*/
                If ddlOrganType.SelectedValue <> "1" Then
                    If (TableFrom(strFrom) = "OrganizationWait") Then
                        If dt.Rows(0).Item("FlowOrganID").ToString.Trim = "" Then
                            ddlFlowOrganID.Items.Clear()
                            ddlFlowOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
                        Else
                            hidFlowOrgan.Value = "'" & dt.Rows(0).Item("FlowOrganID").Replace("|", "','").ToString.Trim & "'"
                            Bsp.Utility.FillDDL(ddlFlowOrganID, "eHRMSDB", "OrganizationFlow", "RTrim(OrganID)", "OrganName", Bsp.Utility.DisplayType.Full, "", "And OrganID In (" & hidFlowOrgan.Value & ")", "Order by OrganID")
                        End If
                        ucFlowOrgan.DefaultFlowOrgan = hidFlowOrgan.Value
                        'HR內部比對單位註記
                        If dt.Rows(0).Item("CompareFlag").ToString.Trim = "1" Then
                            chkCompareFlag.Checked = True
                        End If
                        '授權單位
                        If dt.Rows(0).Item("DelegateFlag").ToString.Trim = "1" Then
                            chkDelegateFlag.Checked = True
                        End If
                        '處級單位註記
                        If dt.Rows(0).Item("OrganNo").ToString.Trim = "1" Then
                            chkOrganNo.Checked = True
                        End If
                        '業務類別

                        'ddlBusinessType.SelectedValue = dt.Rows(0).Item("BusinessType").ToString.Trim
                        Bsp.Utility.SetSelectedIndex(ddlBusinessType, dt.Rows(0).Item("BusinessType").ToString.Trim)
                        '部門主管角色
                        Bsp.Utility.SetSelectedIndex(ddlFlowRoleCode, dt.Rows(0).Item("RoleCode").ToString.Trim)
                    End If
                End If
            End Using
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".GetData", ex)
        End Try

        ''/*----------------------功能組織資料(非待異動)----------------------*/
        If ddlOrganType.SelectedValue <> "1" And Not TableFrom(strFrom) = "OrganizationWait" Then
            Try
                Using dt As DataTable = objOM1.QueryOrganizationByDetail("OrganizationFlow", arySQL(1), "", "", "", arySQL(5))
                    If dt.Rows.Count <= 0 Then Exit Sub
                    '比對簽核單位UC
                    If dt.Rows(0).Item("FlowOrganID").ToString.Trim = "" Then
                        ddlFlowOrganID.Items.Clear()
                        ddlFlowOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
                    Else
                        hidFlowOrgan.Value = "'" & dt.Rows(0).Item("FlowOrganID").Replace("|", "','").ToString.Trim & "'"
                        Bsp.Utility.FillDDL(ddlFlowOrganID, "eHRMSDB", "OrganizationFlow", "RTrim(OrganID)", "OrganName", Bsp.Utility.DisplayType.Full, "", "And OrganID In (" & hidFlowOrgan.Value & ")", "Order by OrganID")
                    End If
                    ucFlowOrgan.DefaultFlowOrgan = hidFlowOrgan.Value
                    'HR內部比對單位註記
                    If dt.Rows(0).Item("CompareFlag").ToString.Trim = "1" Then
                        chkCompareFlag.Checked = True
                    End If
                    '授權單位
                    If dt.Rows(0).Item("DelegateFlag").ToString.Trim = "1" Then
                        chkDelegateFlag.Checked = True
                    End If
                    '處級單位註記
                    If dt.Rows(0).Item("OrganNo").ToString.Trim = "1" Then
                        chkOrganNo.Checked = True
                    End If
                    '業務類別

                    Bsp.Utility.SetSelectedIndex(ddlBusinessType, dt.Rows(0).Item("BusinessType").ToString.Trim)

                    '部門主管角色
                    Bsp.Utility.SetSelectedIndex(ddlFlowRoleCode, dt.Rows(0).Item("RoleCode").ToString.Trim)
                    '/*-----------------------主管照片--------------------------*/
                End Using

            Catch ex As Exception
                Bsp.Utility.ShowMessage(Me, Me.FunID & ".GetData", ex)
            End Try
        End If
    End Sub

#End Region

#Region "subGetData"
    '上階與所屬一級 因為日期連動單獨做get
    Private Sub subGet_UpOrgan_DeptID()
        '上階部門-OrganID
        Dim objOM1 As New OM1()
        Dim strValidDate As String = IIf(objOM1.Check_SP(ucValidDate.DateText), "", " and ValidDate<=" + Bsp.Utility.Quote(ucValidDate.DateText) + " ")
        If ddlOrganType.SelectedValue = "2" Then
            '上階部門-OrganID
            OM1.FillDDLOM1000(ddlUpOrganID, " OrganizationFlow ", " RTRIM(OrganID) ", " Case when InValidFlag='0' then RTRIM(OrganName) else RTRIM(OrganName)+'(無效)' end ", " InValidFlag, VirtualFlag ,'B' as TableFrom ", OM1.DisplayType.Full, "  union select distinct RTRIM(OrganID)  AS Code  ,   RTRIM(OrganName)+'(未生效)'   AS CodeName,  RTRIM(OrganID)  + '-' +   RTRIM(OrganName)+'(未生效)'  AS FullName, InValidFlag, VirtualFlag,'A' as TableFrom from OrganizationWait ", " and CompID = " + Bsp.Utility.Quote(hidCompID.Value) + " and OrganReason='1' and OrganType in ('2','3') and WaitStatus='0' " + strValidDate, " order by TableFrom,InValidFlag, RTRIM(OrganID) ")
        Else
            OM1.FillDDLOM1000(ddlUpOrganID, " Organization ", " RTRIM(OrganID) ", " Case when InValidFlag='0' then RTRIM(OrganName) else RTRIM(OrganName)+'(無效)' end ", " InValidFlag, VirtualFlag ,'B' as TableFrom ", OM1.DisplayType.Full, "Where 1=1   and CompID = " + Bsp.Utility.Quote(hidCompID.Value) + "  union select distinct RTRIM(OrganID)  AS Code  ,   RTRIM(OrganName)+'(未生效)'   AS CodeName,  RTRIM(OrganID)  + '-' +   RTRIM(OrganName)+'(未生效)'  AS FullName, InValidFlag, VirtualFlag,'A' as TableFrom from OrganizationWait ", " and CompID = " + Bsp.Utility.Quote(hidCompID.Value) + " and OrganReason='1' and OrganType in ('1','3') and WaitStatus='0' " + strValidDate, " order by TableFrom,InValidFlag, RTRIM(OrganID) ")
        End If
        ddlUpOrganID.Items.Insert(0, New ListItem("---<Self>---", "(self)"))
        ddlUpOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))

        '所屬一級部門-OrganID
        If ddlOrganType.SelectedValue = "2" Then
            OM1.FillDDLOM1000(ddlDeptID, " OrganizationFlow ", " RTRIM(OrganID) ", " Case when InValidFlag='0' then RTRIM(OrganName) else RTRIM(OrganName)+'(無效)' end ", " InValidFlag , VirtualFlag,'B' as TableFrom ", OM1.DisplayType.Full, " where OrganID=DeptID union select distinct RTRIM(OrganID)  AS Code  ,   RTRIM(OrganName)+'(未生效)'   AS CodeName,  RTRIM(OrganID)  + '-' +   RTRIM(OrganName)+'(未生效)'  AS FullName, InValidFlag, VirtualFlag,'A' as TableFrom from OrganizationWait ", " and OrganID=DeptID and CompID = " + Bsp.Utility.Quote(hidCompID.Value) + " and OrganReason='1' and OrganType in ('2','3') and WaitStatus='0'" + strValidDate, " order by TableFrom,InValidFlag, VirtualFlag, RTRIM(OrganID) ")
        Else
            OM1.FillDDLOM1000(ddlDeptID, " Organization ", " RTRIM(OrganID) ", " Case when InValidFlag='0' then RTRIM(OrganName) else RTRIM(OrganName)+'(無效)' end ", " InValidFlag , VirtualFlag,'B' as TableFrom ", OM1.DisplayType.Full, " where CompID = " + Bsp.Utility.Quote(hidCompID.Value) + " and OrganID=DeptID union select distinct RTRIM(OrganID)  AS Code  ,   RTRIM(OrganName)+'(未生效)'   AS CodeName,  RTRIM(OrganID)  + '-' +   RTRIM(OrganName)+'(未生效)'  AS FullName, InValidFlag, VirtualFlag,'A' as TableFrom from OrganizationWait ", " and OrganID=DeptID and CompID = " + Bsp.Utility.Quote(hidCompID.Value) + " and OrganReason='1' and OrganType in ('1','3') and WaitStatus='0'" + strValidDate, " order by TableFrom,InValidFlag, VirtualFlag, RTRIM(OrganID) ")
        End If
        ddlDeptID.Items.Insert(0, New ListItem("---<Self>---", "(self)"))
        ddlDeptID.Items.Insert(0, New ListItem("---請選擇---", ""))
    End Sub

    Private Sub subGetData()
        '⌳單位類別
        OM1.FillDDL(ddlOrgType, " HRCodeMap ", " RTRIM(Code) ", " CodeCName ", OM1.DisplayType.Full, "", " AND TabName='Organization_OrgType'  and FldName= '" + hidCompID.Value + "'  and NotShowFlag='0'  ", " order by SortFld ")
        ddlOrgType.Items.Insert(0, New ListItem("---<Self>---", "(self)"))
        ddlOrgType.Items.Insert(0, New ListItem("---請選擇---", ""))
        '⌳所屬事業群
        OM1.FillDDL(ddlGroupID, " OrganizationFlow ", " RTRIM(GroupID) ", " OrganName ", OM1.DisplayType.Full, "", " AND OrganID = GroupID  ", " Order by GroupID ")
        ddlGroupID.Items.Insert(0, New ListItem("---請選擇---", ""))

        '事業群類別
        OM1.FillDDL(ddlGroupType, " HRCodeMap ", " RTRIM(Code) ", " CodeCName ", OM1.DisplayType.Full, "", " AND TabName='Organization' and FldName='GroupType' and NotShowFlag='0'  ", " order by SortFld ")
        ddlGroupType.Items.Insert(0, New ListItem("---請選擇---", ""))

        '上階與所屬一級 因為日期連動單獨做get
        subGet_UpOrgan_DeptID()

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

        '職位 DDL+UC
        ddlPositionID.Items.Clear()
        ddlPositionID.Items.Insert(0, New ListItem("---請選擇---", ""))

        '⌳費用分攤部門
        OM1.FillDDLOM1000(ddlCostDeptID, _
                            " (select OrganID,OrganName,InValidFlag,VirtualFlag from Organization where CompID = " + Bsp.Utility.Quote(hidCompID.Value) + "	and VirtualFlag <> '1') as T ", " OrganID ", " OrganName ", " '1' as number,InValidFlag,VirtualFlag ", OM1.DisplayType.Full, " union select CompID AS Code, CompName AS CodeName,CompID+'-'+CompName AS FullName , '2' as number, ' ' as InValidFlag, ' ' as VirtualFlag from(select CompID,CompName from Company where FeeShareFlag ='1') as S ", "", " order by number,InValidFlag,VirtualFlag,Code ")

        ddlCostDeptID.Items.Insert(0, New ListItem("---<Self>---", "(self)"))
        ddlCostDeptID.Items.Insert(0, New ListItem("---請選擇---", ""))

        ''工作性質 DDL+UC
        ddlWorkTypeID.Items.Clear()
        ddlWorkTypeID.Items.Insert(0, New ListItem("---請選擇---", ""))
        '部門主管角色
        OM1.FillDDL(ddlRoleCode, " HRCodeMap ", " Code ", " CodeCName ", OM1.DisplayType.Full, "", " AND  TabName='Organization'  and FldName = 'RoleCode' and NotShowFlag='0'  ", " order by SortFld ")

        ddlRoleCode.Items.Insert(0, New ListItem("---請選擇---", ""))
        ''/*----------------------功能組織資料----------------------*/
        '比對簽核單位DDL+UC
        ddlFlowOrganID.Items.Clear()
        ddlFlowOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))

        '業務類別ddl
        OM1.FillDDL(ddlBusinessType, " HRCodeMap ", " RTRIM(Code) ", " CodeCName ", OM1.DisplayType.Full, "", " AND TabName='Business'  and FldName= 'BusinessType' ", " order by Code ")
        ddlBusinessType.Items.Insert(0, New ListItem("---請選擇---", ""))

        '部門主管角色Flow
        OM1.FillDDL(ddlFlowRoleCode, " HRCodeMap ", " Code ", " CodeCName ", OM1.DisplayType.Full, "", " AND  TabName='OrganizationFlow'  and FldName = 'RoleCode' and NotShowFlag='0'  ", " order by SortFld ")
        ddlFlowRoleCode.Items.Insert(0, New ListItem("---請選擇---", ""))

        '/*======Clear動作===============*/
        txtOrganName.Text = ""
        txtOrganEngName.Text = ""
        txtBoss.Text = ""
        txtSecBoss.Text = ""
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
        lblSecPersonPartName.Text = ""

        ddlBossType.SelectedValue = ""
        ddlCostType.SelectedValue = ""

    End Sub
    Private Sub subGetDataFlow()
        Dim objOM1 As New OM1()
        Dim strValidDate As String = IIf(objOM1.Check_SP(ucValidDate.DateText), "", " and ValidDate<=" + Bsp.Utility.Quote(ucValidDate.DateText) + " ")
        OM1.FillDDL(ddlOrgType, " HRCodeMap ", " RTRIM(Code) ", " CodeCName ", OM1.DisplayType.Full, "", " AND TabName='Organization_OrgType'  and FldName= '" + hidCompID.Value + "'  and NotShowFlag='0'  ", " order by SortFld ")
        'subGetClear()
        '⌳單位類別
        OM1.FillDDL(ddlOrgType, " HRCodeMap ", " RTRIM(Code) ", " CodeCName ", OM1.DisplayType.Full, "", " AND TabName='OrganizationFlow_OrgType'  and FldName= '" + hidCompID.Value + "'  and NotShowFlag='0'  ", " order by SortFld ")

        ddlOrgType.Items.Insert(0, New ListItem("---<Self>---", "(self)"))
        ddlOrgType.Items.Insert(0, New ListItem("---請選擇---", ""))
        '⌳所屬事業群
        OM1.FillDDL(ddlGroupID, " OrganizationFlow ", " RTRIM(GroupID) ", " OrganName ", OM1.DisplayType.Full, "", " AND OrganID = GroupID  ", " Order by GroupID ")
        ddlGroupID.Items.Insert(0, New ListItem("---請選擇---", ""))
        '上階與所屬一級 因為日期連動單獨做get
        subGet_UpOrgan_DeptID()

        '事業群類別
        OM1.FillDDL(ddlGroupType, " HRCodeMap ", " RTRIM(Code) ", " CodeCName ", OM1.DisplayType.Full, "", " AND TabName='Organization' and FldName='GroupType' and NotShowFlag='0'  ", " order by SortFld ")
        ddlGroupType.Items.Insert(0, New ListItem("---請選擇---", ""))


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
        ddlPositionID.Items.Clear()
        ddlPositionID.Items.Insert(0, New ListItem("---請選擇---", ""))

        '⌳費用分攤部門
        OM1.FillDDLOM1000(ddlCostDeptID, _
                            " (select OrganID,OrganName,InValidFlag,VirtualFlag from Organization where CompID = " + Bsp.Utility.Quote(hidCompID.Value) + "	and VirtualFlag <> '1') as T ", " OrganID ", " OrganName ", " '1' as number,InValidFlag,VirtualFlag ", OM1.DisplayType.Full, " union select CompID AS Code, CompName AS CodeName,CompID+'-'+CompName AS FullName , '2' as number, ' ' as InValidFlag, ' ' as VirtualFlag from(select CompID,CompName from Company where FeeShareFlag ='1') as S ", "", " order by number,InValidFlag,VirtualFlag,Code ")
        ddlCostDeptID.Items.Insert(0, New ListItem("---<Self>---", "(self)"))
        ddlCostDeptID.Items.Insert(0, New ListItem("---請選擇---", ""))

        ''工作性質 DDL+UC
        ddlWorkTypeID.Items.Clear()
        ddlWorkTypeID.Items.Insert(0, New ListItem("---請選擇---", ""))
        '部門主管角色
        OM1.FillDDL(ddlRoleCode, " HRCodeMap ", " Code ", " CodeCName ", OM1.DisplayType.Full, "", " AND  TabName='Organization'  and FldName = 'RoleCode' and NotShowFlag='0'  ", " order by SortFld ")
        ddlRoleCode.Items.Insert(0, New ListItem("---請選擇---", ""))
        ''/*----------------------功能組織資料----------------------*/
        '比對簽核單位DDL+UC
        ddlFlowOrganID.Items.Clear()
        ddlFlowOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))

        '業務類別ddl
        OM1.FillDDL(ddlBusinessType, " HRCodeMap ", " RTRIM(Code) ", " CodeCName ", OM1.DisplayType.Full, "", " AND TabName='Business'  and FldName= 'BusinessType' and Code in('01','02','03') ", " union select'09','其他','09-其他'order by Code")
        'OM1.FillDDL(ddlBusinessType, " HRCodeMap ", " RTRIM(Code) ", " CodeCName ", OM1.DisplayType.Full, "", " AND TabName='Business'  and FldName= 'BusinessType' ", " order by Code ")
        ddlBusinessType.Items.Insert(0, New ListItem("---請選擇---", ""))
        '部門主管角色Flow

        OM1.FillDDL(ddlFlowRoleCode, " HRCodeMap ", " Code ", " CodeCName ", OM1.DisplayType.Full, "", " AND  TabName='OrganizationFlow'  and FldName = 'RoleCode' and NotShowFlag='0'  ", " order by SortFld ")
        ddlFlowRoleCode.Items.Insert(0, New ListItem("---請選擇---", ""))
        '/*======Clear動作===============*/
        txtOrganName.Text = ""
        txtOrganEngName.Text = ""
        txtBoss.Text = ""
        txtSecBoss.Text = ""
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
        lblSecPersonPartName.Text = ""
    End Sub
#End Region

#Region "funCheckData"
    Private Function funBossCheckData() As Boolean
        If ddlOrganReason.SelectedValue = "3" Then
            If ddlBossCompID.SelectedValue <> "" And txtBoss.Text <> "" Then
                Dim objHR As New HR
                If Not objHR.IsDataExists("OrganizationBossLog", " and CompID=" & Bsp.Utility.Quote(hidCompID.Value) & " and OrganID=" & Bsp.Utility.Quote(txtOrganID.Text) & " and ValidDateBH=" & Bsp.Utility.Quote(ucValidDate.DateText) & " and not(BossCompID=" & Bsp.Utility.Quote(ddlBossCompID.SelectedValue) & " and Boss=" & Bsp.Utility.Quote(txtBoss.Text) & ") ") Then
                    Return True
                Else
                    Bsp.Utility.RunClientScript(Me.Page, "BossCheck();")
                    Return False
                End If
            Else
                Bsp.Utility.ShowMessage(Me, "部門主管資料錯誤")
                Return False
            End If
        Else
            Return True
        End If
    End Function

    Private Function funCheckData() As Boolean
        Dim objOM1 As New OM1()
        'Organization用
        Dim strWhere As String = " And CompID = " & Bsp.Utility.Quote(hidCompID.Value.Trim) & " And OrganID = " & Bsp.Utility.Quote(txtOrganID.Text.Trim)
        'OrganizationFlow用
        Dim strWhere2 As String = " And OrganID = " & Bsp.Utility.Quote(txtOrganID.Text.Trim)
        Dim strNowDate As String = Date.Now
        Dim strSQL As New StringBuilder()
        Dim Temp As String = ""
        '異動原因-空值
        If ddlOrganReason.SelectedValue.Trim = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00035", "異動原因")
            ddlOrganReason.Focus()
            Return False
        End If
        '待異動組織類型-空值
        If ddlOrganType.SelectedValue.Trim = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00035", "待異動組織類型")
            ddlOrganType.Focus()
            Return False
        End If

        If txtOrganID.Text.Trim = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00035", "部門代碼")
            txtOrganID.Focus()
            Return False
        End If
        '日期檢核
        If objOM1.Check_SP(ucValidDate.DateText) Then
            Bsp.Utility.ShowFormatMessage(Me, "H_00000", objOM1.rtError)
            Return False
        End If
        '/*-----------------異動原因-全套---------------------*/
        If ddlOrganReason.SelectedValue.Trim = "2" And chkInValidFlag.Checked = False Then
            Bsp.Utility.ShowMessage(Me, "組織無效須勾選無效註記")
            ddlOrganReason.Focus()
            Return False
        End If
        Select Case ddlOrganReason.SelectedValue.Trim
            Case 1  '異動原因-新增時 
                If ddlOrganType.SelectedValue <> "3" Then
                    If objOM1.IsDataExists(TableFrom(ddlOrganType.SelectedValue), strWhere) Then
                        Bsp.Utility.ShowMessage(Me, "異動原因-組織新增：該筆組織已存在於" & TableFrom(ddlOrganType.SelectedValue))
                        Return False
                    End If
                    'OrganizationWait重複判斷
                    If objOM1.IsDataExists("OrganizationWait", strWhere & " And  OrganType in(" & Bsp.Utility.Quote(ddlOrganType.SelectedValue) & ",'3') and WaitStatus= '0' ") Then
                        Bsp.Utility.ShowMessage(Me, "異動原因-組織新增：該筆組織已有待異動資料")
                        ddlOrganType.Focus()
                        Return False
                    End If
                Else 'ddOrganType=3
                    If objOM1.IsDataExists("Organization", strWhere) Then
                        Bsp.Utility.ShowMessage(Me, "異動原因-組織新增：該筆組織已存在於Organization")
                        Return False
                    End If
                    If objOM1.IsDataExists("OrganizationFlow", strWhere2) Then
                        Bsp.Utility.ShowMessage(Me, "異動原因-組織新增：該筆組織已存在於OrganizationFlow")
                        Return False
                    End If
                    'OrganizationWait重複判斷
                    If objOM1.IsDataExists("OrganizationWait", strWhere & " and WaitStatus= '0' ") Then
                        Bsp.Utility.ShowMessage(Me, "異動原因-組織新增：該筆組織已有待異動資料")
                        ddlOrganType.Focus()
                        Return False
                    End If
                End If

            Case 2  '異動原因-無效時 
                'OrganizationWait存在未生效資料
                If objOM1.IsDataExists("OrganizationWait", strWhere & " And OrganType = " & Bsp.Utility.Quote(ddlOrganType.SelectedValue.Trim) & " and WaitStatus= '0' ") Then
                    Bsp.Utility.ShowMessage(Me, "異動原因-組織無效：該筆組織已存在於OrganizationWait且未生效！")
                    Return False
                End If

                'Organization和OrganizationFlow是否存在
                If ddlOrganType.SelectedValue.Trim = "1" Or ddlOrganType.SelectedValue.Trim = "3" Then
                    If Not objOM1.IsDataExists("Organization", strWhere) Then
                        Bsp.Utility.ShowMessage(Me, "異動原因-組織無效：該組織Organization不存在！")
                        Return False
                    End If
                End If

                If ddlOrganType.SelectedValue.Trim = "2" Or ddlOrganType.SelectedValue.Trim = "3" Then
                    If Not objOM1.IsDataExists("OrganizationFlow", strWhere2) Then
                        Bsp.Utility.ShowMessage(Me, "異動原因-組織無效：該組織OrganizationFlow不存在！")
                        Return False
                    End If
                End If
                '/*******************************/(檢核下階組織)
                If ddlOrganType.SelectedValue.Trim = "1" Or ddlOrganType.SelectedValue.Trim = "3" Then
                    If objOM1.IsDataExists("Organization ", " and UpOrganID=" & Bsp.Utility.Quote(txtOrganID.Text.Trim) & " and OrganID!=UpOrganID and InValidFlag='0' ") Then
                        Bsp.Utility.ShowMessage(Me, "異動原因-組織無效：行政組織下存在有效組織")
                        Return False
                    End If
                End If

                If ddlOrganType.SelectedValue.Trim = "2" Or ddlOrganType.SelectedValue.Trim = "3" Then
                    If objOM1.IsDataExists("OrganizationFlow ", " and UpOrganID=" & Bsp.Utility.Quote(txtOrganID.Text.Trim) & " and OrganID!=UpOrganID and InValidFlag='0' ") Then
                        Bsp.Utility.ShowMessage(Me, "異動原因-組織無效： 功能組織下存在有效組織")
                        Return False
                    End If
                End If
                '/*******************************/(檢核人員)  
                If ddlOrganType.SelectedValue = "1" Or ddlOrganType.SelectedValue = "3" Then
                    If objOM1.IsDataExists(" Personal ", strWhere & " and WorkStatus ='1' ") _
                   And objOM1.IsDataExists(" Personal as Q left join(select E.EmpID from Personal as P,EmployeeWait as E where  P.CompID= " & Bsp.Utility.Quote(hidCompID.Value) & " and P.OrganID= " & Bsp.Utility.Quote(txtOrganID.Text.Trim) & " and P.EmpID=E.EmpID and E.ValidDate<= " & Bsp.Utility.Quote(ucValidDate.DateText) & " and P.WorkStatus='1' and P.OrganID<>E.OrganID)as T on  Q.EmpID=T.EmpID  ", " and Q.CompID= " & Bsp.Utility.Quote(hidCompID.Value) & " and Q.OrganID= " & Bsp.Utility.Quote(txtOrganID.Text.Trim) & " and Q.WorkStatus='1' and  T.EmpID is null") Then
                        Bsp.Utility.ShowMessage(Me, "異動原因-組織無效：組織下有在職人員且無待異動！")
                        Return False
                    End If
                End If

                If ddlOrganType.SelectedValue = "2" Or ddlOrganType.SelectedValue = "3" Then
                    If objOM1.IsDataExists("EmpFlow E ", " and OrganID=" & Bsp.Utility.Quote(txtOrganID.Text.Trim)) Then
                        Bsp.Utility.ShowMessage(Me, "異動原因-組織無效：功能組織下有在職人員")
                        Return False
                    End If
                End If

                '//-----------------------------------

            Case 3 '異動原因-異動時
                If ddlOrganType.SelectedValue <> "3" Then
                    If objOM1.IsDataExists(TableFrom(ddlOrganType.SelectedValue), strWhere) Then
                        'OrganizationWait重複判斷
                        If objOM1.IsDataExists("OrganizationWait", strWhere & " And  OrganType in(" & Bsp.Utility.Quote(ddlOrganType.SelectedValue.Trim) & ",'3') and WaitStatus= '0' ") Then
                            Bsp.Utility.ShowMessage(Me, "異動原因-組織異動：該筆組織已有待異動資料")
                            ddlOrganType.Focus()
                            Return False
                        End If
                    Else
                        Bsp.Utility.ShowMessage(Me, "異動原因-組織異動：該筆組織不存在")
                        Return False
                    End If
                Else
                    If Not objOM1.IsDataExists("Organization", strWhere) Then
                        Bsp.Utility.ShowMessage(Me, "異動原因-組織異動：該筆組織Organization不存在")
                        Return False
                    End If
                    If Not objOM1.IsDataExists("OrganizationFlow", strWhere2) Then
                        Bsp.Utility.ShowMessage(Me, "異動原因-組織異動：該筆組織OrganizationFlow不存在")
                        Return False
                    End If
                    'OrganizationWait重複判斷
                    If objOM1.IsDataExists("OrganizationWait", strWhere & " and WaitStatus= '0' ") Then
                        Bsp.Utility.ShowMessage(Me, "異動原因-組織異動：該筆組織已有待異動資料")
                        ddlOrganType.Focus()
                        Return False
                    End If
                End If
        End Select

        '/*-----------------異動原因-全套---------------------*/
        '/*------------------組織類型-判斷--------------------*/
        If ddlOrganType.SelectedValue.Trim = "3" Then
            If objOM1.IsDataExists("OrganizationWait", strWhere & " and WaitStatus= '0' ") Then
                Bsp.Utility.ShowMessage(Me, "組織類型：該筆組織已有待異動資料")
                ddlOrganType.Focus()
                Return False
            End If
        Else
            If objOM1.IsDataExists("OrganizationWait", strWhere & " And  OrganType in (" & Bsp.Utility.Quote(ddlOrganType.SelectedValue.Trim) & ",'3') " & " and WaitStatus= '0' ") Then
                Bsp.Utility.ShowMessage(Me, "組織類型：該筆組織已有待異動資料")
                ddlOrganType.Focus()
                Return False
            End If
        End If
        '/*------------------組織類型-判斷--------------------*/

        '*部門代號
        If txtOrganID.Text.Trim = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "部門代號")
            txtOrganID.Focus()
            Return False
        End If

        '異動原因-組織無效：檢查到這
        If ddlOrganReason.SelectedValue = "2" And chkInValidFlag.Checked = True Then Return True

        '*部門名稱
        If txtOrganName.Text.Trim = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "部門名稱")
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
            Bsp.Utility.ShowFormatMessage(Me, "W_00035", "單位類別")
            ddlOrgType.Focus()
            Return False
        End If

        '所屬事業群
        If ddlGroupID.SelectedValue.Trim = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00035", "所屬事業群")
            ddlGroupID.Focus()
            Return False
        End If

        '*上階部門
        If ddlUpOrganID.SelectedValue.Trim = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00035", "上階部門")
            ddlUpOrganID.Focus()
            Return False
        End If

        '*事業群類別
        If ddlGroupType.SelectedValue.Trim = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00035", "事業群類別")
            ddlGroupType.Focus()
            Return False
        End If

        '*所屬一級部門
        If ddlDeptID.SelectedValue.Trim = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00035", "所屬一級部門")
            ddlDeptID.Focus()
            Return False
        End If

        '*部門主管-公司代碼
        If ddlBossCompID.SelectedValue.Trim = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00035", "部門主管公司代碼")
            ddlBossCompID.Focus()
            Return False
        End If
        '*部門主管-員編
        If txtBoss.Text.Trim = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "部門主管")
            txtBoss.Focus()
            Return False
        End If
        If txtBoss.Text <> "" Then
            Dim objHR As New HR
            Dim rtnTable As DataTable = objHR.GetHREmpName(ddlBossCompID.SelectedValue.Trim, txtBoss.Text)
            If rtnTable.Rows.Count <= 0 Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00020", "部門主管")
                txtBoss.Focus()
                Return False
            End If
        End If


        '部門主管-人員名稱(不用)

        '主管任用方式
        If ddlBossType.SelectedValue = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00035", "主管任用方式")
            ddlBossType.Focus()
            Return False
        End If

        '部門主管-主要重複檢核
        strSQL.Clear()
        If ddlBossType.SelectedValue = "1" And (ddlOrganType.SelectedValue = "1" Or ddlOrganType.SelectedValue = "3") Then
            strSQL.AppendLine("select TOP 1 ISNULL(OrganName,'') from (")
            strSQL.AppendLine("SELECT OrganName from dbo.Organization WHERE Boss=" + Bsp.Utility.Quote(txtBoss.Text) + " AND BossType='1'")
            strSQL.AppendLine("UNION")
            strSQL.AppendLine("SELECT OrganName from dbo.OrganizationWait where  OrganType in ('1','3') AND OrganReason IN ('1','3') AND Boss=" + Bsp.Utility.Quote(txtBoss.Text) + " AND BossType='1' and WaitStatus='0' ")
            strSQL.AppendLine(") as O")
            Temp = Bsp.DB.ExecuteScalar(strSQL.ToString, "eHRMSDB")
            If Temp <> Nothing Then
                Bsp.Utility.ShowMessage(Me, txtBoss.Text + "-" + lblBossName.Text + "，已任" + Temp + "主管，本單位建檔請改兼任")
                ddlBossType.Focus()
                Return False
            End If
        End If
        Temp = ""
        strSQL.Clear()
        If ddlBossType.SelectedValue = "1" And (ddlOrganType.SelectedValue = "2" Or ddlOrganType.SelectedValue = "3") Then
            strSQL.AppendLine("select TOP 1 ISNULL(OrganName,'') from (")
            strSQL.AppendLine("SELECT OrganName from dbo.OrganizationFlow WHERE Boss=" + Bsp.Utility.Quote(txtBoss.Text) + " AND BossType='1'")
            strSQL.AppendLine("UNION")
            strSQL.AppendLine("SELECT OrganName from dbo.OrganizationWait where OrganType in ('2','3') AND OrganReason IN ('1','3') AND Boss=" + Bsp.Utility.Quote(txtBoss.Text) + " AND BossType='1' and WaitStatus='0' ")
            strSQL.AppendLine(") as O")
            Temp = Bsp.DB.ExecuteScalar(strSQL.ToString, "eHRMSDB")
            If Temp <> Nothing Then
                Bsp.Utility.ShowMessage(Me, +txtBoss.Text + "-" + lblBossName.Text + "，已任" + Temp + "主管，本單位建檔請改兼任")
                ddlBossType.Focus()
                Return False
            End If
        End If
        Temp = ""
        strSQL.Clear()

        '副主管
        If txtSecBoss.Text <> "" Then
            If ddlBossCompID.SelectedValue.Trim = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00035", "副主管公司代碼")
                ddlBossCompID.Focus()
                Return False
            Else
                Dim objHR As New HR
                Dim rtnTable As DataTable = objHR.GetHREmpName(ddlSecBossCompID.SelectedValue.Trim, txtSecBoss.Text)
                If rtnTable.Rows.Count <= 0 Then
                    Bsp.Utility.ShowFormatMessage(Me, "W_00020", "副主管")
                    txtSecBoss.Focus()
                    Return False
                End If
            End If
        End If

        '/*----------------------行政組織資料----------------------*/
        If ddlOrganType.SelectedValue <> "2" And ddlOrganReason.SelectedValue = "1" Then '組織型態


            '人事管理員
            If txtPersonPart.Text <> "" Then
                Dim objHR As New HR
                Dim rtnTable As DataTable = objHR.GetHREmpName(UserProfile.SelectCompRoleID, txtPersonPart.Text)
                If rtnTable.Rows.Count <= 0 Then
                    Bsp.Utility.ShowFormatMessage(Me, "W_00020", "人事管理員")
                    txtPersonPart.Focus()
                    Return False
                End If
            End If

            '第二人事管理員
            If txtSecPersonPart.Text <> "" Then
                Dim objHR As New HR
                Dim rtnTable As DataTable = objHR.GetHREmpName(UserProfile.SelectCompRoleID, txtSecPersonPart.Text)
                If rtnTable.Rows.Count <= 0 Then
                    Bsp.Utility.ShowFormatMessage(Me, "W_00020", "第二人事管理員")
                    txtSecPersonPart.Focus()
                    Return False
                End If
            End If

            '自行查核主管
            If txtCheckPart.Text <> "" Then
                Dim objHR As New HR
                Dim rtnTable As DataTable = objHR.GetHREmpName(UserProfile.SelectCompRoleID, txtCheckPart.Text)
                If rtnTable.Rows.Count <= 0 Then
                    Bsp.Utility.ShowFormatMessage(Me, "W_00020", "自行查核主管")
                    txtCheckPart.Focus()
                    Return False
                End If
            End If

            '工作地點ddlWorkSiteID
            If ddlWorkSiteID.SelectedValue.Trim = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00035", "工作地點")
                ddlWorkSiteID.Focus()
                Return False
            End If

            '工作性質
            If ddlWorkTypeID.SelectedValue.Trim = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00035", "工作性質")
                ddlWorkTypeID.Focus()
                Return False
            End If

            '*部門主管角色(行政組織)
            If ddlRoleCode.SelectedValue.Trim = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00035", "部門主管角色(行政組織)")
                ddlRoleCode.Focus()
                Return False
            End If

        End If '組織型態

        '/*--------------功能組織---------------------------------*/
        If ddlOrganType.SelectedValue <> "1" Then
            If tdBusinessType2.Visible Then
                If ddlBusinessType.SelectedValue.Trim = "" Then
                    Bsp.Utility.ShowFormatMessage(Me, "W_00035", "業務類別")
                    ddlBusinessType.Focus()
                    Return False
                End If
            End If

            If ddlFlowRoleCode.SelectedValue.Trim = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00035", "部門主管角色(功能組織)")
                ddlFlowRoleCode.Focus()
                Return False
            End If
        End If
        Return True
    End Function

#End Region

#Region "視窗按鈕 DoAction"
    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnAdd"       '存檔返回
                ViewState("Param") = Param
                If funCheckData() Then
                    If funBossCheckData() Then
                        If SaveData() Then
                            Bsp.Utility.ShowMessage(Me, "存檔成功，即將返回")
                            GoBack()
                            Return
                        End If
                        Bsp.Utility.ShowFormatMessage(Me, "E_00000")
                    End If
                End If
            Case "btnActionC"    '存檔
                ViewState("Param") = Param
                If funCheckData() Then
                    If funBossCheckData() Then
                        'Bsp.Utility.ShowMessage(Me, "我存檔了")
                        If SaveData() Then
                            Bsp.Utility.RunClientScript(Me.Page, "ActionVisable();")
                            PhotoSelect()
                            Return
                        End If
                        Bsp.Utility.ShowFormatMessage(Me, "E_00000")
                    End If
                End If

            Case "btnActionX"     '返回
                    GoBack()
            Case "btnCancel"   '清除
                subGetData()  'ClearData只清除(6)以下，(2)~(5)額外清除
                    ucSelectOrgan.Visible = False
                    switchEnabled("")
                    txtOrganID.Text = ""
                    ucValidDate.DateText = ""
                    ddlOrganReason.SelectedValue = "" '寫死'
                ddlOrganType.SelectedValue = "" '寫死'
                ddlOrganType_change()
        End Select
    End Sub
    Private Sub GoBack()
        Dim ti As TransferInfo = Me.StateTransfer
        Me.TransferFramePage(ti.CallerUrl, Nothing, ti.Args)
    End Sub

#End Region

#Region "SaveData"
    Private Function SaveData() As Boolean
        Dim objOM1 As New OM1()
        Dim SeqTemp As String = objOM1.GetSeq("OrganizationWait", hidCompID.Value, ddlOrganReason.SelectedValue, ddlOrganType.SelectedValue, ucValidDate.DateText, txtOrganID.Text)
        Dim beOrganizationWait As New beOrganizationWait.Row()
        Dim bsOrganizationWait As New beOrganizationWait.Service()

        '如果沒用uc速查OrganID，在存檔時帶入手打的OrganID相關資料  20161220 不懂+加上異動原因"2"
        If hidOrganNameOld.Value = "" And ddlOrganReason.SelectedValue = "2" Then
            OrganSelectData(ddlOrganType.SelectedValue, hidCompID.Value, ddlOrganReason.SelectedValue, "", "", txtOrganID.Text)
        End If

        'beOrganizationWait的gencode取得值
        With beOrganizationWait
            .CompID.Value = UserProfile.SelectCompRoleID
            .OrganReason.Value = ddlOrganReason.SelectedValue
            .OrganType.Value = ddlOrganType.SelectedValue
            .ValidDate.Value = ucValidDate.DateText
            .OrganID.Value = txtOrganID.Text.ToUpper
            .OrganName.Value = txtOrganName.Text
            .OrganNameOld.Value = IIf(ddlOrganReason.SelectedValue = "1", txtOrganName.Text, hidOrganNameOld.Value)
            .OrganEngName.Value = txtOrganEngName.Text
            .InValidFlag.Value = IIf(chkInValidFlag.Checked, "1", "0")
            .VirtualFlag.Value = IIf(chkVirtualFlag.Checked, "1", "0")
            .BranchFlag.Value = IIf(chkBranchFlag.Checked, "1", "0")
            .OrgType.Value = IIf(ddlOrgType.SelectedItem.Text = "---<Self>---", txtOrganID.Text, ddlOrgType.SelectedValue)
            .GroupID.Value = ddlGroupID.SelectedValue
            .UpOrganID.Value = IIf(ddlUpOrganID.SelectedItem.Text = "---<Self>---", txtOrganID.Text, ddlUpOrganID.SelectedValue)
            .GroupType.Value = ddlGroupType.SelectedValue
            .DeptID.Value = IIf(ddlDeptID.SelectedItem.Text = "---<Self>---", txtOrganID.Text, ddlDeptID.SelectedValue)
            '.RoleCode.Value = ddlRoleCode.SelectedValue
            .BossCompID.Value = ddlBossCompID.SelectedValue
            .Boss.Value = txtBoss.Text
            .BossType.Value = ddlBossType.SelectedValue
            .SecBossCompID.Value = ddlSecBossCompID.SelectedValue
            .SecBoss.Value = txtSecBoss.Text
            .BossTemporary.Value = IIf(chkBossTemporary.Checked, "1", "0")
            '行政組織資料
            'If Panel1.Visible And ddlOrganReason.SelectedValue = "1" Then
                .PersonPart.Value = txtPersonPart.Text
                .SecPersonPart.Value = txtSecPersonPart.Text
                .WorkSiteID.Value = ddlWorkSiteID.SelectedValue
                .CheckPart.Value = txtCheckPart.Text
                .PositionID.Value = ddlPositionID.SelectedValue
            .CostDeptID.Value = IIf(ddlCostDeptID.SelectedItem.Text = "---<Self>---", txtOrganID.Text, ddlCostDeptID.SelectedValue)
                .WorkTypeID.Value = ddlWorkTypeID.SelectedValue
                .AccountBranch.Value = txtAccountBranch.Text
            .CostType.Value = ddlCostType.SelectedValue
            .RoleCode.Value = ddlRoleCode.SelectedValue
            'End If
            '功能組織資料
            'If Panel2.Visible And ddlOrganReason.SelectedValue = "1" Then
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
            .FlowRoleCode.Value = ddlFlowRoleCode.SelectedValue
            'End If
            '額外輸入
            .Seq.Value = SeqTemp
            .WaitStatus.Value = "0"
            .LastChgComp.Value = UserProfile.ActCompID
            .LastChgID.Value = UserProfile.ActUserID
            .LastChgDate.Value = Now
        End With 'beOrganizationWait

        '檢查資料是否存在
        If bsOrganizationWait.IsDataExists(beOrganizationWait) Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00010")
            Return False
        End If

        Try
            'Dim temp As Boolean = False
            Dim PositionID() As String = lblSelectPositionID.Text.Replace("'", "").Split(",")
            Dim WorkTypeID() As String = lblSelectWorkTypeID.Text.Replace("'", "").Split(",")

            Return objOM1.InsertOWAddition(beOrganizationWait, hidCompID.Value, ddlOrganReason.SelectedValue, ddlOrganType.SelectedValue, ucValidDate.DateText.ToString, txtOrganID.Text, txtOrganName.Text, SeqTemp, PositionID, WorkTypeID)

        Catch ex As Exception
            Dim errLine As Integer = Convert.ToInt32(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(" ")))
            Bsp.Utility.ShowMessage(Me, "[SaveData]" & errLine & Bsp.Utility.getMessage("E_00000") & Bsp.Utility.getInnerException(Me.FunID, ex))
            Return False
        End Try
    End Function
#End Region

#Region "異動原因開放欄位"

#Region "行政功能層與組織新增異動"
    Private Sub ddlOrganType_change() '//Panel1~2隱藏
        Select Case ddlOrganType.SelectedValue
            Case "1"
                    Panel1.Visible = True
                    Panel2.Visible = False
                txtOrganID.MaxLength = 8
                ucSelectOrgan.Fields = New FieldState() { _
           New FieldState("Organization", "行政組織", True, True), _
           New FieldState("OrganizationWait", "待異動組織", True, True)}
                ucSelectOrgan.Visible = True
                subGetData()
            Case "2"
                    Panel1.Visible = False
                Panel2.Visible = True
                ucSelectOrgan.Fields = New FieldState() { _
          New FieldState("OrganizationFlow", "功能組織", True, True), _
          New FieldState("OrganizationWait", "待異動組織", True, True)}
                    ucSelectOrgan.Visible = True
                txtOrganID.MaxLength = 12
                subGetDataFlow()
            Case "3"
                    Panel1.Visible = True
                Panel2.Visible = True
                ucSelectOrgan.Fields = New FieldState() { _
           New FieldState("Organization", "行政組織", True, True), _
           New FieldState("OrganizationFlow", "功能組織", True, True), _
           New FieldState("OrganizationWait", "待異動組織", True, True)}
                ucSelectOrgan.Visible = True
                txtOrganID.MaxLength = 8
                subGetData()
            Case ""
                    Panel1.Visible = False
                Panel2.Visible = False
                ucSelectOrgan.Fields = New FieldState() { _
          New FieldState("Organization", "行政組織", True, True), _
          New FieldState("OrganizationFlow", "功能組織", True, True), _
          New FieldState("OrganizationWait", "待異動組織", True, True)}
                ucSelectOrgan.Visible = False
                txtOrganID.MaxLength = 8
                subGetData()
        End Select
    End Sub
#End Region
    '#Region "異動原因與畫面鎖定"
    '    '新增-修改有差異
    '    Private Sub switchEnabled(enb As Boolean, ORS As String)
    '        Select Case ORS
    '            Case 1 '(5)~(7)+(9)~(34)
    '                chkInValidFlag.Enabled = False '(8)特別處理false 
    '                If ddlOrganReason.SelectedValue <> "2" Then
    '                    chkInValidFlag.Checked = False
    '                End If
    '                If ViewState("enb") <> enb Then
    '                    ViewState("enb") = enb
    '                    'txtOrganID.Enabled = enb '新增可以輸入，只有修改頁不行
    '                    'ucSelectOrgan.Visible = enb '新增可以輸入，只有修改頁不行
    '                    txtOrganName.Enabled = enb
    '                    txtOrganEngName.Enabled = enb
    '                    chkVirtualFlag.Enabled = enb
    '                    chkBranchFlag.Enabled = enb
    '                    ddlOrgType.Enabled = enb
    '                    ddlGroupID.Enabled = enb
    '                    ddlUpOrganID.Enabled = enb
    '                    ddlGroupType.Enabled = enb
    '                    ddlDeptID.Enabled = enb
    '                    ddlRoleCode.Enabled = enb
    '                    ddlBossCompID.Enabled = enb
    '                    txtBoss.Enabled = enb
    '                    lblBossName.Enabled = enb
    '                    ucQueryBoss.Visible = enb ''''
    '                    ddlBossType.Enabled = enb
    '                    ddlSecBossCompID.Enabled = enb
    '                    txtSecBoss.Enabled = enb
    '                    lblSecBossName.Enabled = enb
    '                    ucSecQueryBoss.Visible = enb ''''
    '                    chkBossTemporary.Enabled = enb
    '                    '/*行政*/
    '                    txtPersonPart.Enabled = enb
    '                    ucPersonPart.Visible = enb
    '                    txtSecPersonPart.Enabled = enb
    '                    ucSecPersonPart.Visible = enb
    '                    ddlWorkSiteID.Enabled = enb
    '                    txtCheckPart.Enabled = enb
    '                    ucCheckPart.Visible = enb
    '                    'ddlPositionID.Enabled = enb '永久開啟
    '                    ucSelectPosition.Visible = enb ''uc''
    '                    ddlCostDeptID.Enabled = enb
    '                    'ddlWorkTypeID.Enabled = enb '永久開啟
    '                    ucSelectWorkType.Visible = enb ''uc''
    '                    txtAccountBranch.Enabled = enb
    '                    ddlCostType.Enabled = enb
    '                    '/*功能*/
    '                    'ddlFlowOrganID.Enabled = enb  '/*⑨*/ '永久開啟
    '                    ucFlowOrgan.Visible = enb
    '                    chkCompareFlag.Enabled = enb
    '                    chkDelegateFlag.Enabled = enb
    '                    chkOrganNo.Enabled = enb
    '                    ddlBusinessType.Enabled = enb
    '                End If
    '            Case 2 '(8)
    '                switchEnabled(False, 1)
    '                chkInValidFlag.Enabled = enb
    '            Case 3 '(6)~(34)
    '                switchEnabled(False, 1)
    '                If ViewState("enb") <> enb Then
    '                    'txtOrganID.Enabled = enb '與修改頁不同，可以輸入
    '                    txtOrganName.Enabled = enb
    '                    txtOrganEngName.Enabled = enb
    '                    chkVirtualFlag.Enabled = enb
    '                    chkBranchFlag.Enabled = enb
    '                    ddlOrgType.Enabled = enb
    '                    ddlGroupID.Enabled = enb
    '                    ddlUpOrganID.Enabled = enb
    '                    ddlGroupType.Enabled = enb
    '                    ddlDeptID.Enabled = enb
    '                    ddlRoleCode.Enabled = enb
    '                    ddlBossCompID.Enabled = enb
    '                    txtBoss.Enabled = enb
    '                    lblBossName.Enabled = enb
    '                    ucQueryBoss.Visible = enb ''uc''
    '                    ddlBossType.Enabled = enb
    '                    ddlSecBossCompID.Enabled = enb
    '                    txtSecBoss.Enabled = enb
    '                    lblSecBossName.Enabled = enb
    '                    ucSecQueryBoss.Visible = enb ''uc''
    '                    chkBossTemporary.Enabled = enb
    '                End If
    '        End Select
    '    End Sub
    '#End Region

#Region "new異動原因與畫面鎖定"
    '新增-修改有差異
    Private Sub switchEnabled(ORS As String)
        Select Case ORS
            Case 1 '(5)~(7)+(9)~(34)
                chkInValidFlag.Enabled = False '(8)特別處理false 
                chkInValidFlag.Checked = False
                txtOrganID.Enabled = True ''異動原因鎖txtOrganID，只給用uc速查
                'ucSelectOrgan.Visible = True '新增可以輸入，只有修改頁不行
                txtOrganName.Enabled = True
                txtOrganEngName.Enabled = True
                chkVirtualFlag.Enabled = True
                chkBranchFlag.Enabled = True
                ddlOrgType.Enabled = True
                ddlGroupID.Enabled = True
                ddlUpOrganID.Enabled = True
                ddlGroupType.Enabled = True
                ddlDeptID.Enabled = True

                ddlBossCompID.Enabled = True
                txtBoss.Enabled = True
                lblBossName.Enabled = True
                ucQueryBoss.Visible = True ''''
                ddlBossType.Enabled = True
                ddlSecBossCompID.Enabled = True
                txtSecBoss.Enabled = True
                lblSecBossName.Enabled = True
                ucSecQueryBoss.Visible = True ''''
                chkBossTemporary.Enabled = True
                '/*行政*/
                txtPersonPart.Enabled = True
                ucPersonPart.Visible = True
                txtSecPersonPart.Enabled = True
                ucSecPersonPart.Visible = True
                ddlWorkSiteID.Enabled = True
                txtCheckPart.Enabled = True
                ucCheckPart.Visible = True
                'ddlPositionID.Enabled = true '永久開啟
                ucSelectPosition.Visible = True ''uc''
                ddlCostDeptID.Enabled = True
                'ddlWorkTypeID.Enabled = true '永久開啟
                ucSelectWorkType.Visible = True ''uc''
                txtAccountBranch.Enabled = True
                ddlCostType.Enabled = True
                ddlRoleCode.Enabled = True
                '/*簽核*/
                'ddlFlowOrganID.Enabled = true  '永久開啟
                ucFlowOrgan.Visible = True
                chkCompareFlag.Enabled = True
                chkDelegateFlag.Enabled = True
                chkOrganNo.Enabled = True
                ddlBusinessType.Enabled = True
                ddlFlowRoleCode.Enabled = True
            Case 2 '(8)
                txtOrganID.Enabled = False '異動原因鎖txtOrganID，只給用uc速查
                chkInValidFlag.Enabled = True
                txtOrganName.Enabled = False
                txtOrganEngName.Enabled = False
                chkVirtualFlag.Enabled = False
                chkBranchFlag.Enabled = False
                ddlOrgType.Enabled = False
                ddlGroupID.Enabled = False
                ddlUpOrganID.Enabled = False
                ddlGroupType.Enabled = False
                ddlDeptID.Enabled = False

                ddlBossCompID.Enabled = False
                txtBoss.Enabled = False
                lblBossName.Enabled = False
                ucQueryBoss.Visible = False ''''
                ddlBossType.Enabled = False
                ddlSecBossCompID.Enabled = False
                txtSecBoss.Enabled = False
                lblSecBossName.Enabled = False
                ucSecQueryBoss.Visible = False ''''
                chkBossTemporary.Enabled = False
                '/*行政*/
                txtPersonPart.Enabled = False
                ucPersonPart.Visible = False
                txtSecPersonPart.Enabled = False
                ucSecPersonPart.Visible = False
                ddlWorkSiteID.Enabled = False
                txtCheckPart.Enabled = False
                ucCheckPart.Visible = False
                'ddlPositionID.Enabled = false '永久開啟
                ucSelectPosition.Visible = False ''uc''
                ddlCostDeptID.Enabled = False
                'ddlWorkTypeID.Enabled = false '永久開啟
                ucSelectWorkType.Visible = False ''uc''
                txtAccountBranch.Enabled = False
                ddlCostType.Enabled = False
                ddlRoleCode.Enabled = False
                '/*功能*/
                'ddlFlowOrganID.Enabled = false  '/*⑨*/ '永久開啟
                ucFlowOrgan.Visible = False
                chkCompareFlag.Enabled = False
                chkDelegateFlag.Enabled = False
                chkOrganNo.Enabled = False
                ddlBusinessType.Enabled = False
                ddlFlowRoleCode.Enabled = False
            Case 3 '(6)~(34)
                chkInValidFlag.Enabled = False
                chkInValidFlag.Checked = False
                txtOrganID.Enabled = False '異動原因鎖txtOrganID，只給用uc速查
                txtOrganName.Enabled = True
                txtOrganEngName.Enabled = True
                chkVirtualFlag.Enabled = True
                chkBranchFlag.Enabled = True
                ddlOrgType.Enabled = True
                ddlGroupID.Enabled = True
                ddlUpOrganID.Enabled = True
                ddlGroupType.Enabled = True
                ddlDeptID.Enabled = True

                ddlBossCompID.Enabled = True
                txtBoss.Enabled = True
                lblBossName.Enabled = True
                ucQueryBoss.Visible = True ''''
                ddlBossType.Enabled = True
                ddlSecBossCompID.Enabled = True
                txtSecBoss.Enabled = True
                lblSecBossName.Enabled = True
                ucSecQueryBoss.Visible = True ''''
                chkBossTemporary.Enabled = True
                '/*行政*/
                txtPersonPart.Enabled = False
                ucPersonPart.Visible = False
                txtSecPersonPart.Enabled = False
                ucSecPersonPart.Visible = False
                ddlWorkSiteID.Enabled = False
                txtCheckPart.Enabled = False
                ucCheckPart.Visible = False
                'ddlPositionID.Enabled = false '永久開啟
                ucSelectPosition.Visible = False ''uc''
                ddlCostDeptID.Enabled = False
                'ddlWorkTypeID.Enabled = false '永久開啟
                ucSelectWorkType.Visible = False ''uc''
                txtAccountBranch.Enabled = False
                ddlCostType.Enabled = False
                ddlRoleCode.Enabled = False
                '/*功能*/
                'ddlFlowOrganID.Enabled = false  '/*⑨*/ '永久開啟
                ucFlowOrgan.Visible = False
                chkCompareFlag.Enabled = False
                chkDelegateFlag.Enabled = False
                chkOrganNo.Enabled = False
                ddlBusinessType.Enabled = False
                ddlFlowRoleCode.Enabled = False
            Case ""
                txtOrganID.Enabled = False  '異動原因鎖txtOrganID，只給用uc速查
                chkInValidFlag.Enabled = False
                txtOrganName.Enabled = False
                txtOrganEngName.Enabled = False
                chkVirtualFlag.Enabled = False
                chkBranchFlag.Enabled = False
                ddlOrgType.Enabled = False
                ddlGroupID.Enabled = False
                ddlUpOrganID.Enabled = False
                ddlGroupType.Enabled = False
                ddlDeptID.Enabled = False

                ddlBossCompID.Enabled = False
                txtBoss.Enabled = False
                lblBossName.Enabled = False
                ucQueryBoss.Visible = False ''''
                ddlBossType.Enabled = False
                ddlSecBossCompID.Enabled = False
                txtSecBoss.Enabled = False
                lblSecBossName.Enabled = False
                ucSecQueryBoss.Visible = False ''''
                chkBossTemporary.Enabled = False
                '/*行政*/
                txtPersonPart.Enabled = False
                ucPersonPart.Visible = False
                txtSecPersonPart.Enabled = False
                ucSecPersonPart.Visible = False
                ddlWorkSiteID.Enabled = False
                txtCheckPart.Enabled = False
                ucCheckPart.Visible = False
                'ddlPositionID.Enabled = false '永久開啟
                ucSelectPosition.Visible = False ''uc''
                ddlCostDeptID.Enabled = False
                'ddlWorkTypeID.Enabled = false '永久開啟
                ucSelectWorkType.Visible = False ''uc''
                txtAccountBranch.Enabled = False
                ddlCostType.Enabled = False
                ddlRoleCode.Enabled = False
                '/*功能*/
                'ddlFlowOrganID.Enabled = false  '/*⑨*/ '永久開啟
                ucFlowOrgan.Visible = False
                chkCompareFlag.Enabled = False
                chkDelegateFlag.Enabled = False
                chkOrganNo.Enabled = False
                ddlBusinessType.Enabled = False
                ddlFlowRoleCode.Enabled = False
        End Select
    End Sub
#End Region
#Region "組織類型與異動原因的DDL"
    Protected Sub ddlOrganReason_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlOrganReason.SelectedIndexChanged
        switchEnabled(ddlOrganReason.SelectedValue)
        'ClearData()
        If ddlOrganType.SelectedValue = "2" Then
            subGetDataFlow()
        Else
            subGetData()
        End If
        txtOrganID.Text = ""
    End Sub
    Protected Sub ddlOrganType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlOrganType.SelectedIndexChanged
            ddlOrganType_change()
           
            txtOrganID.Text = ""
    End Sub
#End Region
#End Region
    '#Region "日期內容檢核"
    '    Protected Sub btnValidDate_Click(sender As Object, e As System.EventArgs) Handles btnValidDate.Click
    '        Dim objOM As New OM1
    '        If objOM.Check(ucValidDate.DateText) Then
    '            Bsp.Utility.ShowFormatMessage(Me, "H_00000", objOM.rtError)
    '            ucValidDate.DateText = objOM.DateCheck(ucValidDate.DateText)
    '        End If

    '    End Sub
    '#End Region

    'Protected Sub txtOrganID_TextChanged(sender As Object, e As System.EventArgs) Handles txtOrganID.TextChanged
    '    subGetData()

    'End Sub
#Region "Boss重複檢核用"
    Protected Sub btnBoss_Click(sender As Object, e As System.EventArgs) Handles btnBoss.Click
        If ViewState("Param") <> "" Then
            Bsp.Utility.RunClientScript(Me.Page, "BossSave();")
        End If

    End Sub

    Protected Sub btnBossSave_Click(sender As Object, e As System.EventArgs) Handles btnBossSave.Click
        Select Case ViewState("Param")
            Case "btnAdd"
                If SaveData() Then
                    Bsp.Utility.ShowMessage(Me, "存檔成功，即將返回")
                    GoBack()
                    Return
                End If
                Bsp.Utility.ShowFormatMessage(Me, "E_00000")
                ViewState("Param") = ""
            Case "btnActionC"
                If SaveData() Then
                    Bsp.Utility.RunClientScript(Me.Page, "ActionVisable();")
                    PhotoSelect()
                    Return
                End If
                Bsp.Utility.ShowFormatMessage(Me, "E_00000")
                ViewState("Param") = ""
        End Select
    End Sub
#End Region

    Protected Sub btnValidDate_Click(sender As Object, e As System.EventArgs) Handles btnValidDate.Click
        Dim objOM As New OM1
        
        If objOM.Check(ucValidDate.DateText) Then
            Bsp.Utility.ShowFormatMessage(Me, "H_00000", objOM.rtError)
            ucValidDate.DateText = objOM.DateCheck(ucValidDate.DateText)
        Else
            Dim strUpOrganID As String = ddlUpOrganID.SelectedValue
            Dim strDeptID As String = ddlDeptID.SelectedValue
            subGet_UpOrgan_DeptID()
            Bsp.Utility.SetSelectedIndex(ddlUpOrganID, strUpOrganID)
            Bsp.Utility.SetSelectedIndex(ddlDeptID, strDeptID)
        End If
    End Sub
End Class



