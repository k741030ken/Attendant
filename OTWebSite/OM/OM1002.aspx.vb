'****************************************************
'功能說明：待異動維護作業-修改-查詢
'建立人員：leo
'建立日期：2016.10
'****************************************************
Imports System.Data
'Imports System.Transactions

Partial Class OM_OM1002
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
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
            hidSeq.Value = 0
        End If
        '不能用，因為還有下拉要開放...
        'Panel1.Enabled = False
        'Panel2.Enabled = False
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Panel1.Visible = False
            Panel2.Visible = False
            Panel4.Visible = False
            txtOrganID.Enabled = False '修改頁固定鎖所以特別處理false
            '20161019更改-修改頁除了OrganID外都可以更動
            switchEnabled("")
            Dim objOM1 As New OM1

            '假設OM1000選取公司與使用者公司不同，需要讀取公司名稱
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)
            Dim CompName As String = objOM1.GetCompName(ht("SelectedCompID").ToString())
            '公司名稱
            If CompName = "" Then
                txtCompID.Text = ht("SelectedCompID").ToString()
                'Bsp.Utility.ShowFormatMessage(Me, "H_00000", "公司名稱讀取錯誤")
            Else
                txtCompID.Text = ht("SelectedCompID").ToString() + "-" + CompName
            End If
            hidCompID.Value = ht("SelectedCompID").ToString()
            '部門代碼
            txtOrganID.Text = ht("SelectedOrganID").ToString()
            '異動原因
            Dim aryValue() As String = Split(ht("SelectedOrganReason").ToString(), "-")
            ddlOrganReason.SelectedValue = aryValue(0)
            hidOrganReason.Value = aryValue(0)
            '組織類型
            aryValue = Split(ht("SelectedOrganType").ToString(), "-")

            ddlOrganType.SelectedValue = aryValue(0).Substring(aryValue(0).Length - 1)
            hidOrganType.Value = aryValue(0).Substring(aryValue(0).Length - 1)
            '生效日期
            ucValidDate.DateText = Date.Parse(ht("SelectedValidDate").ToString())
            hidValidDate.Value = Date.Parse(ht("SelectedValidDate").ToString())
            hidSeq.Value = ht("Seq").ToString()

            '明細資料初始化+獲取資料
            subGetData()
            GetSelectData(hidCompID.Value, hidOrganReason.Value, hidOrganType.Value, Format(Date.Parse(ht("SelectedValidDate").ToString()), "yyyy/MM/dd"), txtOrganID.Text, hidSeq.Value)
            ddlOrganType_change()

            '判斷 OM1000 修改/明細 按鍵，明細封鎖全部欄位
            If ht("PageID").ToString() = "NOTupdate" Then
                hidSeq.Value = -10
                txtCompID.Enabled = False
                ddlOrganReason.Enabled = False
                ddlOrganType.Enabled = False
                ucValidDate.Enabled = False
                'chkInValidFlag.Enabled = False
                switchEnabled("")
                PhotoSelect()
                Panel4.Visible = True
            Else
                switchEnabled(ddlOrganReason.SelectedValue)
            End If
        End If
    End Sub

#Region "照片"
    '/*txtBoss_TextChanged也有PhotoSelect()*/
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
        If ddlUpOrganID.SelectedItem.Text <> "---<Self>---" And ddlUpOrganID.SelectedValue.Trim <> txtOrganID.Text Then
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

        '[讀取照片]
        '因為沒有照片檔案，偵錯時會卡很久故暫時註解
        '//上階部門主管
        Try
            'imgPhoto.ImageUrl = "http://localhost:36314/lastHR/images/Pocky.jpg"
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
            'imgPhoto2.ImageUrl = "http://localhost:36314/lastHR/images/Rimuru.jpg"
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
                    '/*=========================================*/
                Case "ucSelectPosition" '職位
                    lblSelectPositionID.Text = aryData(1)
                    If lblSelectPositionID.Text <> "''" Then
                        '載入 職位 下拉式選單
                        strSql = "and PositionID in (" + lblSelectPositionID.Text + ") and CompID = '" + hidCompID.Value + "'"
                        Bsp.Utility.Position(ddlPositionID, "PositionID", , strSql)

                        '第一筆為主要職位
                        Dim strDefaultValue() As String = lblSelectPositionID.Text.Replace("'", "").Split(",")
                        Dim strPosition As String = ""
                        Bsp.Utility.SetSelectedIndex(ddlPositionID, strDefaultValue(0))
                    Else
                        ddlPositionID.Items.Clear()
                        ddlPositionID.Items.Insert(0, New ListItem("---請選擇---", ""))
                    End If
                    '/*-----------------------------------------*/
                Case "ucSelectWorkType" '工作性質
                    lblSelectWorkTypeID.Text = aryData(1)
                    If lblSelectWorkTypeID.Text <> "''" Then
                        strSql = " and WorkTypeID in (" + lblSelectWorkTypeID.Text + ") and CompID = '" + hidCompID.Value + "'"
                        Bsp.Utility.WorkType(ddlWorkTypeID, "WorkTypeID", , strSql)

                        '第一筆為主要工作性質
                        Dim strDefaultValue() As String = lblSelectWorkTypeID.Text.Replace("'", "").Split(",")
                        Dim strWorkType As String = ""
                        Bsp.Utility.SetSelectedIndex(ddlWorkTypeID, strDefaultValue(0))
                    Else
                        ddlWorkTypeID.Items.Clear()
                        ddlWorkTypeID.Items.Insert(0, New ListItem("---請選擇---", ""))
                    End If
            End Select
        End If
    End Sub
#End Region

#Region "UC-clicked與附屬之DDL、TXT"
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

#Region "人名相關的CompID-DDL與TXT連動"
    '/*DDL*/
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
                lblSecPersonPartName.Text = ""
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", "人事資料尚未建檔")
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
            Dim rtnTable As DataTable = objHR.GetHREmpName(hidCompID.Value, txtCheckPart.Text)
            If rtnTable.Rows.Count <= 0 Then
                lblCheckPart.Text = ""
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", "人事資料尚未建檔")
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
    Private Sub GetSelectData(ByVal CompID As String, ByVal OrganReason As String, ByVal OrganType As String, ByVal ValidDate As Date, ByVal OrganID As String, ByVal Seq As String)
        Dim objOM As New OM1
        OrganType = objOM.OrganTypeOrganReasonToNo(OrganType, "")
        OrganReason = objOM.OrganTypeOrganReasonToNo("", OrganReason)

        Dim bsOrganizationWait As New beOrganizationWait.Service()
        Dim beOrganizationWait As New beOrganizationWait.Row()

        beOrganizationWait.CompID.Value = CompID
        beOrganizationWait.OrganReason.Value = OrganReason
        beOrganizationWait.OrganType.Value = OrganType
        beOrganizationWait.ValidDate.Value = ValidDate
        beOrganizationWait.OrganID.Value = OrganID
        Try
            Using dt As DataTable = objOM.QueryOrganizationWaitByDetail(CompID, OrganReason, OrganType, ValidDate, OrganID, Seq)
                If dt.Rows.Count <= 0 Then Exit Sub
                'Seq次數
                'Seq.Value = dt.Rows(0).Item("Seq").ToString.Trim
                '部門名稱
                txtOrganName.Text = dt.Rows(0).Item("OrganName").ToString.Trim
                '部門名稱Old
                hidOrganNameOld.Value = dt.Rows(0).Item("OrganNameOld").ToString.Trim
                '部門英文名稱
                txtOrganEngName.Text = dt.Rows(0).Item("OrganEngName").ToString.Trim
                '無效註記
                If dt.Rows(0).Item("InValidFlag").ToString.Trim = "1" Then
                    chkInValidFlag.Checked = True
                End If
                '是否為虛擬部門
                If dt.Rows(0).Item("VirtualFlag").ToString.Trim = "1" Then
                    chkVirtualFlag.Checked = True
                End If
                '分行註記rdo 0是/1否
                If dt.Rows(0).Item("BranchFlag").ToString.Trim = "1" Then
                    chkBranchFlag.Checked = True
                End If
                '單位類別
                If dt.Rows(0).Item("OrganID").ToString.Trim = dt.Rows(0).Item("OrgType").ToString.Trim Then
                    ddlOrgType.SelectedValue = "(self)"
                Else
                    Bsp.Utility.SetSelectedIndex(ddlOrgType, dt.Rows(0).Item("OrgType").ToString.Trim)
                End If
                'Bsp.Utility.SetSelectedIndex(ddlOrgType, dt.Rows(0).Item("OrgType").ToString.Trim)  '如果沒找到不會報錯
                '所屬事業群
                ddlGroupID.SelectedValue = dt.Rows(0).Item("GroupID").ToString.Trim
                '上階部門
                If dt.Rows(0).Item("OrganID").ToString.Trim = dt.Rows(0).Item("UpOrganID").ToString.Trim Then
                    ddlUpOrganID.SelectedValue = "(self)"
                Else
                    Bsp.Utility.SetSelectedIndex(ddlUpOrganID, dt.Rows(0).Item("UpOrganID").ToString.Trim)
                End If
                '事業群類別
                ddlGroupType.SelectedValue = dt.Rows(0).Item("GroupType").ToString.Trim
                '所屬一部門
                If dt.Rows(0).Item("OrganID").ToString.Trim = dt.Rows(0).Item("DeptID").ToString.Trim Then
                    ddlDeptID.SelectedValue = "(self)"
                Else
                    Bsp.Utility.SetSelectedIndex(ddlDeptID, dt.Rows(0).Item("DeptID").ToString.Trim)
                End If
                '部門主管角色
                Bsp.Utility.SetSelectedIndex(ddlRoleCode, dt.Rows(0).Item("RoleCode").ToString.Trim)
                'ddlRoleCode.SelectedValue = dt.Rows(0).Item("RoleCode").ToString.Trim
                '部門主管公司代碼
                ddlBossCompID.SelectedValue = dt.Rows(0).Item("BossCompID").ToString.Trim
                '部門主管
                txtBoss.Text = dt.Rows(0).Item("Boss").ToString.Trim
                '部門主管姓名
                lblBossName.Text = dt.Rows(0).Item("PNameN").ToString.Trim
                '主管任用方式
                ddlBossType.SelectedValue = dt.Rows(0).Item("BossType").ToString.Trim
                '副主管公司代碼
                ddlSecBossCompID.SelectedValue = dt.Rows(0).Item("SecBossCompID").ToString.Trim
                '副主管
                txtSecBoss.Text = dt.Rows(0).Item("SecBoss").ToString.Trim
                '副主管姓名
                lblSecBossName.Text = dt.Rows(0).Item("P2NameN").ToString.Trim
                '主管暫代
                If dt.Rows(0).Item("BossTemporary").ToString.Trim = "1" Then
                    chkBossTemporary.Checked = True
                End If
                '/*----------------------行政組織資料----------------------*/
                '人事管理員
                txtPersonPart.Text = dt.Rows(0).Item("PersonPart").ToString.Trim
                '人事管理員姓名
                lblPersonPartName.Text = dt.Rows(0).Item("P3NameN").ToString.Trim
                '第二人事管理員
                txtSecPersonPart.Text = dt.Rows(0).Item("SecPersonPart").ToString.Trim
                '第二人事管理員姓名
                lblSecPersonPartName.Text = dt.Rows(0).Item("P4NameN").ToString.Trim
                '工作地點
                ddlWorkSiteID.SelectedValue = dt.Rows(0).Item("WorkSiteID").ToString.Trim
                '自行查核主管
                txtCheckPart.Text = dt.Rows(0).Item("CheckPart").ToString.Trim
                '自行查核主管姓名
                lblCheckPart.Text = dt.Rows(0).Item("P5NameN").ToString.Trim
                '職位
                OM1.FillDDLOM1000(ddlPositionID, " OrgPositionWait E", " E.PositionID ", " P.Remark ", "E.PrincipalFlag", OM1.DisplayType.Full, _
               " inner join Position P on E.CompID = P.CompID and E.PositionID = P.PositionID ", _
               " and E.CompID = " & Bsp.Utility.Quote(hidCompID.Value) & " and E.OrganID = " & Bsp.Utility.Quote(txtOrganID.Text) & " and E.OrganReason = " & Bsp.Utility.Quote(OrganReason) & " and E.OrganType = " & Bsp.Utility.Quote(OrganType) & " and E.ValidDate = " & Bsp.Utility.Quote(ValidDate) & " and E.Seq = " & Bsp.Utility.Quote(Seq) & " ", " order by E.PrincipalFlag desc , E.PositionID ")

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
                If dt.Rows(0).Item("OrganID").ToString.Trim = dt.Rows(0).Item("CostDeptID").ToString.Trim Then
                    ddlCostDeptID.SelectedValue = "(self)"
                Else
                    Bsp.Utility.SetSelectedIndex(ddlCostDeptID, dt.Rows(0).Item("CostDeptID").ToString.Trim)
                End If

                '工作性質
                OM1.FillDDLOM1000(ddlWorkTypeID, " OrgWorkTypeWait  E", " E.WorkTypeID ", " W.Remark ", "E.PrincipalFlag", OM1.DisplayType.Full, _
              " inner join WorkType W on E.CompID = W.CompID and E.WorkTypeID  = W.WorkTypeID  ", _
              " and E.CompID = " & Bsp.Utility.Quote(hidCompID.Value) & " and E.OrganID = " & Bsp.Utility.Quote(txtOrganID.Text) & " and E.OrganReason = " & Bsp.Utility.Quote(OrganReason) & " and E.OrganType = " & Bsp.Utility.Quote(OrganType) & " and E.ValidDate = " & Bsp.Utility.Quote(ValidDate) & " and E.Seq = " & Bsp.Utility.Quote(Seq) & " ", " order by E.PrincipalFlag desc , E.WorkTypeID  ")
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
                ddlBossType.Items.FindByText("")                '會計分行別
                txtAccountBranch.Text = dt.Rows(0).Item("AccountBranch").ToString.Trim
                '費用分攤科目別
                ddlCostType.SelectedValue = dt.Rows(0).Item("CostType").ToString.Trim

                '/*----------------------功能組織資料----------------------*/
                '比對簽核單位UC
                If dt.Rows(0).Item("FlowOrganID").ToString.Trim = "" Then
                    ddlFlowOrganID.Items.Clear()
                    ddlFlowOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
                Else
                    hidFlowOrgan.Value = "'" & dt.Rows(0).Item("FlowOrganID").Replace("|", "','").ToString.Trim & "'"
                    Bsp.Utility.FillDDL(ddlFlowOrganID, "eHRMSDB", "OrganizationFlow", "RTrim(OrganID)", "OrganName", Bsp.Utility.DisplayType.Full, "", "And OrganID In (" & hidFlowOrgan.Value & ")", "Order by OrganID")
                End If

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
                If dt.Rows(0).Item("BusinessType").ToString.Trim = "" Or dt.Rows(0).Item("BusinessType").ToString.Trim = "01" Or dt.Rows(0).Item("BusinessType").ToString.Trim = "02" Or dt.Rows(0).Item("BusinessType").ToString.Trim = "03" Then
                    ddlBusinessType.SelectedValue = dt.Rows(0).Item("BusinessType").ToString.Trim
                Else
                    ddlBusinessType.SelectedValue = "09"
                End If
                'ddlBusinessType.SelectedValue = dt.Rows(0).Item("BusinessType").ToString.Trim
                '/*-----------------------最後異動---------------------------*/
                txtLastChgComp.Text = dt.Rows(0).Item("LastChgComp").ToString.Trim
                txtLastChgDate.Text = Format(Date.Parse(dt.Rows(0).Item("LastChgDate").ToString.Trim), "yyyy/M/d-hh:mm:ss")
                txtLastChgID.Text = dt.Rows(0).Item("LastChgID").ToString.Trim
                '/*-----------------------主管照片--------------------------*/
            End Using
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".GetData", ex)
        End Try
    End Sub
#End Region

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
        subGetClear()
        '⌳單位類別
        If ddlOrganType.SelectedValue = "2" Then
            OM1.FillDDL(ddlOrgType, " HRCodeMap ", " RTRIM(Code) ", " CodeCName ", OM1.DisplayType.Full, "", " AND TabName='OrganizationFlow_OrgType'  and FldName= '" + hidCompID.Value + "'  and NotShowFlag='0'  ", " order by SortFld ")
        Else
            OM1.FillDDL(ddlOrgType, " HRCodeMap ", " RTRIM(Code) ", " CodeCName ", OM1.DisplayType.Full, "", " AND TabName='Organization_OrgType'  and FldName= '" + hidCompID.Value + "'  and NotShowFlag='0'  ", " order by SortFld ")
        End If

        ddlOrgType.Items.Insert(0, New ListItem("---<Self>---", "(self)"))

        ddlOrgType.Items.Insert(0, New ListItem("---請選擇---", ""))
        '⌳所屬事業群
        OM1.FillDDL(ddlGroupID, " OrganizationFlow ", " RTRIM(GroupID) ", " OrganName ", OM1.DisplayType.Full, "", " AND OrganID = GroupID  ", " Order by GroupID ")
        ddlGroupID.Items.Insert(0, New ListItem("---請選擇---", ""))

        '上階部門-OrganID
        If ddlOrganType.SelectedValue = "2" Then
            OM1.FillDDLOM1000(ddlUpOrganID, " OrganizationFlow ", " RTRIM(OrganID) ", " Case when InValidFlag='0' then RTRIM(OrganName) else RTRIM(OrganName)+'(無效)' end ", " InValidFlag, VirtualFlag ", OM1.DisplayType.Full, "", "", " order by InValidFlag, RTRIM(OrganID) ")
        Else
            OM1.FillDDLOM1000(ddlUpOrganID, " Organization ", " RTRIM(OrganID) ", " Case when InValidFlag='0' then RTRIM(OrganName) else RTRIM(OrganName)+'(無效)' end ", " InValidFlag, VirtualFlag ", OM1.DisplayType.Full, "", " and CompID = " + Bsp.Utility.Quote(hidCompID.Value) + " ", " order by InValidFlag, RTRIM(OrganID) ")
        End If

        ddlUpOrganID.Items.Insert(0, New ListItem("---<Self>---", "(self)"))

        ddlUpOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
        '事業群類別
        OM1.FillDDL(ddlGroupType, " HRCodeMap ", " RTRIM(Code) ", " CodeCName ", OM1.DisplayType.Full, "", " AND TabName='Organization' and FldName='GroupType' and NotShowFlag='0'  ", " order by SortFld ")
        ddlGroupType.Items.Insert(0, New ListItem("---請選擇---", ""))

        '所屬一級部門-OrganID
        If ddlOrganType.SelectedValue = "2" Then
            OM1.FillDDLOM1000(ddlDeptID, " OrganizationFlow ", " RTRIM(OrganID) ", " Case when InValidFlag='0' then RTRIM(OrganName) else RTRIM(OrganName)+'(無效)' end ", " InValidFlag , VirtualFlag", OM1.DisplayType.Full, "", " and OrganID=DeptID  ", " order by InValidFlag, VirtualFlag, RTRIM(OrganID) ")
        Else
            OM1.FillDDLOM1000(ddlDeptID, " Organization ", " RTRIM(OrganID) ", " Case when InValidFlag='0' then RTRIM(OrganName) else RTRIM(OrganName)+'(無效)' end ", " InValidFlag , VirtualFlag", OM1.DisplayType.Full, "", " and CompID = " + Bsp.Utility.Quote(hidCompID.Value) + " and OrganID=DeptID ", " order by InValidFlag, VirtualFlag, RTRIM(OrganID) ")
        End If

        ddlDeptID.Items.Insert(0, New ListItem("---<Self>---", "(self)"))

        ddlDeptID.Items.Insert(0, New ListItem("---請選擇---", ""))

        '部門主管角色
        If ddlOrganType.SelectedValue = "2" Then
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
        ddlCostDeptID.Items.Insert(0, New ListItem("---<Self>---", "(self)"))
        ddlCostDeptID.Items.Insert(0, New ListItem("---請選擇---", ""))

        ''工作性質 DDL+UC
        ddlWorkTypeID.Items.Insert(0, New ListItem("---請選擇---", ""))

        ''/*----------------------功能組織資料----------------------*/
        '比對簽核單位DDL+UC
        ddlFlowOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))

        '業務類別ddl
        OM1.FillDDL(ddlBusinessType, " HRCodeMap ", " RTRIM(Code) ", " CodeCName ", OM1.DisplayType.Full, "", " AND TabName='Business'  and FldName= 'BusinessType' and Code in('01','02','03') ", " union select'09','其他','09-其他'order by Code")
        'OM1.FillDDL(ddlBusinessType, " HRCodeMap ", " RTRIM(Code) ", " CodeCName ", OM1.DisplayType.Full, "", " AND TabName='Business'  and FldName= 'BusinessType' ", " order by Code ")
        ddlBusinessType.Items.Insert(0, New ListItem("---請選擇---", ""))
    End Sub
#End Region

#Region "ClearData"
    Private Sub ClearData() 'ClearData只清除(6)以下，(2)~(5)需額外清除
        'ddl的清理由重新執行subget取代
        'ddlOrgType.Items.Clear()
        'ddlGroupID.Items.Clear()
        'ddlUpOrganID.Items.Clear()
        'ddlGroupType.Items.Clear()
        'ddlDeptID.Items.Clear()
        'ddlRoleCode.Items.Clear()
        'ddlBossCompID.Items.Clear()
        'ddlSecBossCompID.Items.Clear()
        'ddlWorkSiteID.Items.Clear()
        'ddlPositionID.Items.Clear()
        'ddlWorkTypeID.Items.Clear()
        'ddlBusinessType.Items.Clear()
        'ddlCostDeptID.Items.Clear()

        ddlCostType.SelectedValue = "" '寫死'
        ddlBossType.SelectedValue = "" '寫死'

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

        '照片
        Panel4.Visible = False

        '暫存資料用lbl
        lblSelectPositionID.Text = ""
        lblSelectWorkTypeID.Text = ""
        hidFlowOrgan.Value = ""
    End Sub
#End Region

#Region "funCheckData"
    Private Function funCheckData() As Boolean
        Dim objOM1 As New OM1()
        'Organization用
        Dim strWhere As String = " And CompID = " & Bsp.Utility.Quote(hidCompID.Value.Trim) & " And OrganID = " & Bsp.Utility.Quote(txtOrganID.Text.Trim)
        'OrganizationFlow用
        Dim strWhere2 As String = " And OrganID = " & Bsp.Utility.Quote(txtOrganID.Text.Trim)
        'OrganizationWait修改前用
        Dim strWhereMe As String = " and not( CompID =" & Bsp.Utility.Quote(hidCompID.Value) & " and OrganReason=" & Bsp.Utility.Quote(hidOrganReason.Value) & " and OrganType=" & Bsp.Utility.Quote(hidOrganType.Value) & " and ValidDate=" & Bsp.Utility.Quote(hidValidDate.Value) & " and Seq=" & Bsp.Utility.Quote(hidSeq.Value) & " and OrganID=" & Bsp.Utility.Quote(txtOrganID.Text) & ") "
        Dim strNowDate As String = Date.Now

        '異動原因-空值
        If ddlOrganReason.SelectedValue.Trim = "" Then
            ddlOrganReason.Focus()
            Bsp.Utility.ShowFormatMessage(Me, "W_00035", "異動原因")
            Return False
        End If
        '待異動組織類型-空值
        If ddlOrganType.SelectedValue.Trim = "" Then
            ddlOrganType.Focus()
            Bsp.Utility.ShowFormatMessage(Me, "W_00035", "待異動組織類型")
            Return False
        End If
        'If ucValidDate.DateText = "" Or Replace(ucValidDate.DateText, "_", "") = "//" Then
        '    ucValidDate.Focus()
        '    Bsp.Utility.ShowFormatMessage(Me, "W_00030", "生效日期")
        '    Return False
        'End If
        If objOM1.Check(ucValidDate.DateText) Then
            Bsp.Utility.ShowFormatMessage(Me, "H_00000", objOM1.rtError)
            ucValidDate.DateText = objOM1.DateCheck(ucValidDate.DateText)
            Return False
        End If
        '/*-----------------異動原因-全套---------------------*/

        If ddlOrganReason.SelectedValue.Trim = "2" And chkInValidFlag.Checked = False Then
            ddlOrganReason.Focus()
            Bsp.Utility.ShowMessage(Me, "組織無效須勾選無效註記")
            Return False
        End If
        If ddlOrganReason.SelectedValue.Trim <> hidOrganReason.Value Or ddlOrganType.SelectedValue.Trim <> hidOrganType.Value Then
            Select Case ddlOrganReason.SelectedValue.Trim
                Case 1  '異動原因-新增時 
                    If ddlOrganType.SelectedValue <> "3" Then
                        If objOM1.IsDataExists(TableFrom(ddlOrganType.SelectedValue), strWhere) Then
                            Bsp.Utility.ShowMessage(Me, "異動原因-組織新增：該筆組織已存在於" & TableFrom(ddlOrganType.SelectedValue))
                            Return False
                        End If
                        'OrganizationWait重複判斷
                        If objOM1.IsDataExists("OrganizationWait", strWhere & " And  OrganType in(" & Bsp.Utility.Quote(ddlOrganType.SelectedValue) & ",'3') and WaitStatus= '0' " & strWhereMe) Then
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
                        If objOM1.IsDataExists("OrganizationWait", strWhere & " and WaitStatus= '0' " & strWhereMe) Then
                            Bsp.Utility.ShowMessage(Me, "異動原因-組織新增：該筆組織已有待異動資料")
                            ddlOrganType.Focus()
                            Return False
                        End If
                    End If

                Case 2  '異動原因-無效時 
                    'OrganizationWait存在未生效資料
                    If objOM1.IsDataExists("OrganizationWait", strWhere & " And OrganType = " & Bsp.Utility.Quote(ddlOrganType.SelectedValue.Trim) & " and WaitStatus= '0' " & strWhereMe) Then
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

                    '/*******************************/(檢核人員)  
                    If objOM1.IsDataExists(" Personal ", strWhere & " and WorkStatus ='1' ") _
                        And objOM1.IsDataExists(" Personal as Q left join(select E.EmpID from Personal as P,EmployeeWait as E where  P.CompID= " & Bsp.Utility.Quote(hidCompID.Value) & " and P.OrganID= " & Bsp.Utility.Quote(txtOrganID.Text.Trim) & " and P.EmpID=E.EmpID and E.ValidDate<= " & Bsp.Utility.Quote(ucValidDate.DateText) & " and P.WorkStatus='1' and P.OrganID<>E.OrganID)as T on  Q.EmpID=T.EmpID  ", " and Q.CompID= " & Bsp.Utility.Quote(hidCompID.Value) & " and Q.OrganID= " & Bsp.Utility.Quote(txtOrganID.Text.Trim) & " and Q.WorkStatus='1' and  T.EmpID is null") Then

                        Bsp.Utility.ShowMessage(Me, "異動原因-組織無效：組織下有在職人員且無待異動！")
                        Return False
                    End If

                Case 3 '異動原因-異動時
                    If ddlOrganType.SelectedValue <> "3" Then
                        If objOM1.IsDataExists(TableFrom(ddlOrganType.SelectedValue), strWhere) Then
                            'OrganizationWait重複判斷
                            If objOM1.IsDataExists("OrganizationWait", strWhere & " And  OrganType in(" & Bsp.Utility.Quote(ddlOrganType.SelectedValue.Trim) & ",'3') and WaitStatus= '0' " & strWhereMe) Then
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
                        If objOM1.IsDataExists("OrganizationWait", strWhere & " and WaitStatus= '0' " & strWhereMe) Then
                            Bsp.Utility.ShowMessage(Me, "異動原因-組織異動：該筆組織已有待異動資料")
                            ddlOrganType.Focus()
                            Return False
                        End If
                    End If
            End Select
        End If

        '/*-----------------異動原因-全套---------------------*/

        '/*------------------組織類型-判斷--------------------*/
        '排除自己的條件
        If ddlOrganType.SelectedValue.Trim = "3" Then
            If objOM1.IsDataExists("OrganizationWait", strWhere & " and WaitStatus= '0' " & strWhereMe) Then
                Bsp.Utility.ShowMessage(Me, "組織類型：該筆組織已有待異動資料")
                ddlOrganType.Focus()
                Return False
            End If
        Else
            If objOM1.IsDataExists("OrganizationWait", strWhere & " And  OrganType in( '3'," & Bsp.Utility.Quote(ddlOrganType.SelectedValue.Trim) & ")" & " and WaitStatus= '0' " & strWhereMe) Then
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

        '*部門主管角色
        If ddlRoleCode.SelectedValue.Trim = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00035", "部門主管角色")
            ddlRoleCode.Focus()
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
        If ddlOrganType.SelectedValue <> 2 Then '組織型態
            
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
        End If '組織型態
        Return True
    End Function

#End Region

#Region "視窗按鈕 DoAction"
    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnUpdate"       '存檔返回
                If funCheckData() Then
                    If SaveData() Then
                        Bsp.Utility.ShowMessage(Me, "修改成功，即將返回")
                        GoBack()
                        Return
                    End If
                    Bsp.Utility.ShowFormatMessage(Me, "E_00000")
                End If
            Case "btnActionC"    '存檔
                If funCheckData() Then
                    If SaveData() Then
                        GetSelectData(hidCompID.Value, ddlOrganReason.SelectedValue, ddlOrganType.SelectedValue, ucValidDate.DateText, txtOrganID.Text, hidSeqNew.Value)
                        PhotoSelect()
                        Bsp.Utility.RunClientScript(Me.Page, "ActionVisable();")
                        Return
                    End If
                    Bsp.Utility.ShowFormatMessage(Me, "E_00000")
                End If
            Case "btnActionX"     '返回
                    GoBack()
            Case "btnCancel"   '清除
                    ucValidDate.DateText = hidValidDate.Value
                ddlOrganReason.SelectedValue = hidOrganReason.Value
                    ddlOrganType.SelectedValue = hidOrganType.Value
                    ClearData()
                    ddlOrganType_change()
                'switchEnabled("")
                switchEnabled(hidOrganReason.Value)
                    subGetData() 'ClearData只清除(6)以下，(2)~(5)額外清除
                GetSelectData(hidCompID.Value, hidOrganReason.Value, hidOrganType.Value, hidValidDate.Value, txtOrganID.Text, hidSeq.Value)
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
        Dim beOrganizationWait As New beOrganizationWait.Row()
        Dim bsOrganizationWait As New beOrganizationWait.Service()

        If (ddlOrganReason.SelectedValue = hidOrganReason.Value And ddlOrganType.SelectedValue = hidOrganType.Value And ucValidDate.DateText = hidValidDate.Value) Then
            hidSeqNew.Value = hidSeq.Value
        Else
            hidSeqNew.Value = objOM1.GetSeq("OrganizationWait", hidCompID.Value, ddlOrganReason.SelectedValue, ddlOrganType.SelectedValue, ucValidDate.DateText, txtOrganID.Text)
        End If
        'Dim beHRCodeMap As New beHRCodeMap.Row()

        'beOrganizationWait的gencode取得值
        With beOrganizationWait
            .CompID.Value = hidCompID.Value
            .OrganReason.Value = ddlOrganReason.SelectedValue
            .OrganType.Value = ddlOrganType.SelectedValue
            .ValidDate.Value = ucValidDate.DateText
            .OrganID.Value = txtOrganID.Text
            .OrganName.Value = txtOrganName.Text
            .OrganNameOld.Value = hidOrganNameOld.Value
            .OrganEngName.Value = txtOrganEngName.Text
            .InValidFlag.Value = IIf(chkInValidFlag.Checked, "1", "0")
            .VirtualFlag.Value = IIf(chkVirtualFlag.Checked, "1", "0")
            .BranchFlag.Value = IIf(chkBranchFlag.Checked, "1", "0")
            .OrgType.Value = IIf(ddlOrgType.SelectedItem.Text = "---<Self>---", txtOrganID.Text, ddlOrgType.SelectedValue)
            .GroupID.Value = ddlGroupID.SelectedValue
            .UpOrganID.Value = IIf(ddlUpOrganID.SelectedItem.Text = "---<Self>---", txtOrganID.Text, ddlUpOrganID.SelectedValue)
            .GroupType.Value = ddlGroupType.SelectedValue
            .DeptID.Value = IIf(ddlDeptID.SelectedItem.Text = "---<Self>---", txtOrganID.Text, ddlDeptID.SelectedValue)
            .RoleCode.Value = ddlRoleCode.SelectedValue
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
            'End If
            '額外輸入
            'Seq次數
            .Seq.Value = hidSeqNew.Value
            .WaitStatus.Value = "0"
            .LastChgComp.Value = UserProfile.ActCompID
            .LastChgID.Value = UserProfile.ActUserID
            .LastChgDate.Value = Now
        End With 'beOrganizationWait

        Try
            Dim PositionID() As String = lblSelectPositionID.Text.Replace("'", "").Split(",")
            Dim WorkTypeID() As String = lblSelectWorkTypeID.Text.Replace("'", "").Split(",")
            Return objOM1.UpdateOWAddition(beOrganizationWait, hidCompID.Value, txtOrganID.Text, PositionID, WorkTypeID, hidSeqNew.Value, hidSeq.Value, ucValidDate.DateText, hidValidDate.Value, ddlOrganType.SelectedValue, hidOrganType.Value, ddlOrganReason.SelectedValue, hidOrganReason.Value, txtOrganName.Text, hidOrganNameOld.Value)
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
                If Not Panel1.Visible Then
                    Panel1.Visible = True
                End If
                If Panel2.Visible Then
                    Panel2.Visible = False
                End If
            Case "2"
                If Panel1.Visible Then
                    Panel1.Visible = False
                End If
                If Not Panel2.Visible Then
                    Panel2.Visible = True
                End If
            Case "3"
                If Not Panel1.Visible Then
                    Panel1.Visible = True
                End If
                If Not Panel2.Visible Then
                    Panel2.Visible = True
                End If
            Case ""
                If Panel1.Visible Then
                    Panel1.Visible = False
                End If
                If Panel2.Visible Then
                    Panel2.Visible = False
                End If
        End Select
        'Select Case ddlOrganType.SelectedValue
        '    Case 1
        '        Panel1.Visible = True
        '        Panel2.Visible = False
        '    Case 2
        '        Panel1.Visible = False
        '        Panel2.Visible = True
        '    Case 3
        '        Panel1.Visible = True
        '        Panel2.Visible = True
        '    Case ""
        '        Panel1.Visible = False
        '        Panel2.Visible = False
        'End Select
    End Sub
#End Region
#Region "異動原因與畫面鎖定"
    '新增-修改有差異
    Private Sub switchEnabled(ORS As String)
        Select Case ORS
            Case 1 '(5)~(7)+(9)~(34)
                chkInValidFlag.Enabled = False '(8)特別處理false
                If ddlOrganReason.SelectedValue <> "2" Then
                    chkInValidFlag.Checked = False
                End If
                'txtOrganID.Enabled = False '修改頁固定鎖所以特別處理false
                'ucSelectOrgan.Visible = enb ''''
                txtOrganName.Enabled = True
                txtOrganEngName.Enabled = True
                chkVirtualFlag.Enabled = True
                chkBranchFlag.Enabled = True
                ddlOrgType.Enabled = True
                ddlGroupID.Enabled = True
                ddlUpOrganID.Enabled = True
                ddlGroupType.Enabled = True
                ddlDeptID.Enabled = True
                ddlRoleCode.Enabled = True
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
                '/*簽核*/
                'ddlFlowOrganID.Enabled = true  '永久開啟
                ucFlowOrgan.Visible = True
                chkCompareFlag.Enabled = True
                chkDelegateFlag.Enabled = True
                chkOrganNo.Enabled = True
                ddlBusinessType.Enabled = True
            Case 2 '(8)
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
                ddlRoleCode.Enabled = False
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
                '/*簽核*/
                'ddlFlowOrganID.Enabled = false  '永久開啟
                ucFlowOrgan.Visible = False
                chkCompareFlag.Enabled = False
                chkDelegateFlag.Enabled = False
                chkOrganNo.Enabled = False
                ddlBusinessType.Enabled = False
            Case 3 '(6)~(34)
                chkInValidFlag.Enabled = True
                txtOrganName.Enabled = True
                txtOrganEngName.Enabled = True
                chkVirtualFlag.Enabled = True
                chkBranchFlag.Enabled = True
                ddlOrgType.Enabled = True
                ddlGroupID.Enabled = True
                ddlUpOrganID.Enabled = True
                ddlGroupType.Enabled = True
                ddlDeptID.Enabled = True
                ddlRoleCode.Enabled = True
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
                '/*功能*/
                'ddlFlowOrganID.Enabled = false  '/*⑨*/ '永久開啟
                ucFlowOrgan.Visible = False
                chkCompareFlag.Enabled = False
                chkDelegateFlag.Enabled = False
                chkOrganNo.Enabled = False
                ddlBusinessType.Enabled = False
            Case ""
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
                ddlRoleCode.Enabled = False
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
                '/*功能*/
                'ddlFlowOrganID.Enabled = false  '/*⑨*/ '永久開啟
                ucFlowOrgan.Visible = False
                chkCompareFlag.Enabled = False
                chkDelegateFlag.Enabled = False
                chkOrganNo.Enabled = False
                ddlBusinessType.Enabled = False
        End Select
    End Sub
#End Region
#Region "組織類型與異動原因的DDL"
    Protected Sub ddlOrganReason_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlOrganReason.SelectedIndexChanged
        '20161019更改，修改頁除了OrganID都可以修改，所以保留
        If hidSeq.Value >= 0 Then
                switchEnabled(ddlOrganReason.SelectedValue)
            End If
    End Sub

    Protected Sub ddlOrganType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlOrganType.SelectedIndexChanged
        Dim objOM As New OM1
        ddlOrganType_change()
        ClearData()
        subGetData()
        GetSelectData(hidCompID.Value, ddlOrganReason.SelectedValue, hidOrganType.Value, objOM.DateCheck(ucValidDate.DateText), lblOrganID.Text, hidSeq.Value)
    End Sub


#End Region
#End Region

    '#Region "日期內容檢核"
    '    Protected Sub btnValidDate_Click(sender As Object, e As System.EventArgs) Handles btnValidDate.Click
    '        'If ucValidDate.DateText = "" Or ucValidDate.DateText = "____/__/__" Then
    '        '    ucValidDate.DateText = ""
    '        '    Return
    '        'Else
    '        '    If Convert.ToDateTime(ucValidDate.DateText) < "1900/01/01" Then
    '        '        ucValidDate.DateText = ""
    '        '        Bsp.Utility.ShowMessage(Me, "日期須介於1900/01/01~9999/12/31")
    '        '        Return
    '        '    End If
    '        'End If
    '        Dim objOM As New OM1
    '        If objOM.Check(ucValidDate.DateText) Then
    '            Bsp.Utility.ShowFormatMessage(Me, "H_00000", objOM.rtError)
    '            ucValidDate.DateText = objOM.DateCheck(ucValidDate.DateText)
    '        End If
    '    End Sub
    '#End Region
End Class
