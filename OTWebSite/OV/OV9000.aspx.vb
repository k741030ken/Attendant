Imports System.Data
Imports System.Data.Common
Partial Class OV_OV9000
    Inherits PageBase
    Private Property eHRMSDB_ITRD As String
        Get
            Dim result As String = ConfigurationManager.AppSettings("eHRMSDB")
            If String.IsNullOrEmpty(result) Then
                result = "eHRMSDB_ITRD"
            End If
            Return result
        End Get
        Set(ByVal value As String)

        End Set
    End Property

    Private Property AattendantDB As String
        Get
            Dim result As String = ConfigurationManager.AppSettings("AattendantDB")
            If String.IsNullOrEmpty(result) Then
                result = "AattendantDB"
            End If
            Return result
        End Get
        Set(ByVal value As String)

        End Set
    End Property
    'Private _BaseSQL As String = "SELECT DISTINCT FE.FlowCode,FE.SystemID,FE.FlowSN,C.CompID + '-' + ISNULL(C.CompName,'') AS CompID,FE.EmpID + '-' + ISNULL(P.NameN,'') AS EmpID,FE.OrganID+ '-' + ISNULL(O.OrganName,'') AS OrganID,FE.BusinessType + '-' + ISNULL(HR.CodeCName,'') AS BusinessType,P.RankID,FE.PositionID + '-' + ISNULL(Po.Remark,'') AS PositionID,FE.WorkTypeID + '-' + ISNULL(Wo.Remark,'') AS WorkTypeID, FE.InValidFlag, FE.LastChgComp + '-' + ISNULL(C2.CompName,'') AS LastChgComp, FE.LastChgID + '-' + ISNULL(P2.NameN,'') AS LastChgID, LastChgDate = Case When Convert(Char(10), FE.LastChgDate, 111) = '1900/01/01' Then '' ELSE Convert(Char(10), FE.LastChgDate, 111) End" &
    '                             " FROM HRFlowEmpDefine FE " &
    '                             " LEFT JOIN [eHRMSDB_ITRD].[dbo].[Company] C ON C.CompID=FE.CompID" &
    '                             " LEFT JOIN [eHRMSDB_ITRD].[dbo].[Personal] P ON P.CompID=FE.CompID AND P.EmpID=FE.EmpID" &
    '                             " LEFT JOIN [eHRMSDB_ITRD].[dbo].[Organization] O ON FE.CompID=O.CompID AND FE.OrganID=O.OrganID" &
    '                             " LEFT JOIN [eHRMSDB_ITRD].[dbo].[Company] C2 ON C2.CompID=FE.LastChgComp " &
    '                             " LEFT JOIN [eHRMSDB_ITRD].[dbo].[HRCodeMap] HR ON HR.TabName='Business' AND HR.FldName = 'BusinessType' AND HR.NotShowFlag='0' AND HR.Code = FE.BusinessType" &
    '                             " LEFT JOIN [eHRMSDB_ITRD].[dbo].[Position] Po ON Po.CompID=FE.CompID AND Po.PositionID=FE.PositionID " &
    '                             " LEFT JOIN [eHRMSDB_ITRD].[dbo].[WorkType] Wo ON Wo.CompID=FE.CompID AND Wo.WorkTypeID=FE.WorkTypeID " &
    '                             " LEFT JOIN [eHRMSDB_ITRD].[dbo].[Personal] P2 ON P2.CompID=FE.CompID AND P2.EmpID=FE.LastChgID"
    Private _BaseSQL As String = "SELECT HE.CompID,HE.FlowCode,HE.SystemID,HE.FlowSN,HE.FlowCompID,C.CompName,HE.EmpID,HE.EmpID+ISNULL(P.NameN,'') AS NameN,HE.DeptID,ISNULL(O.OrganName,'') AS DeptName,HE.OrganID,ISNULL(O2.OrganName,'') AS OrganName," &
                                 " HE.RankIDTop,HE.RankIDBottom,ISNULL(HE.RankIDTop,'') AS RankID,HE.TitleIDTop,HE.TitleIDBottom,ISNULL(HE.TitleIDTop,'') AS TitleID,ISNULL(T.TitleName,'') AS TitleName,HE.PositionID,ISNULL(PO.Remark,'') AS PositionName,HE.WorkTypeID," &
                                 " ISNULL(WO.Remark,'') AS WorkTypeName,HE.BusinessType,ISNULL(HR.CodeCName,'') AS BusinessTypeName,ISNULL(HR1.CodeCName,'') as EmpFlowRemarkName,HE.EmpFlowRemark,HE.PrincipalFlag,HE.InValidFlag," &
                                 " HE.InValidFlag,HE.LastChgComp,HE.LastChgID,REPLACE(CONVERT(NVARCHAR (19),HE.LastChgDate,120),'-','/') AS LastChgDate " &
                                 " FROM HRFlowEmpDefine HE " &
                                 " LEFT JOIN " & eHRMSDB_ITRD & ".[dbo].[Company] C ON C.CompID=HE.CompID " &
                                 " LEFT JOIN " & eHRMSDB_ITRD & ".[dbo].[Personal] P ON P.CompID=HE.CompID AND P.EmpID=HE.EmpID " &
                                 " LEFT JOIN " & eHRMSDB_ITRD & ".[dbo].[Title] T ON P.CompID=T.CompID AND HE.RankIDTop=T.RankID AND HE.TitleIDTop=T.TitleID " &
                                 " LEFT JOIN " & eHRMSDB_ITRD & ".[dbo].[Organization] O ON HE.CompID=O.CompID AND HE.DeptID=O.OrganID AND HE.DeptID=O.DeptID " &
                                 " LEFT JOIN " & eHRMSDB_ITRD & ".[dbo].[Organization] O2 ON HE.CompID=O2.CompID AND HE.OrganID=O2.OrganID " &
                                 " LEFT JOIN " & eHRMSDB_ITRD & ".[dbo].[Position] PO ON PO.CompID=HE.CompID AND PO.PositionID=HE.PositionID " &
                                 " LEFT JOIN " & eHRMSDB_ITRD & ".[dbo].[WorkType] WO ON WO.CompID=HE.CompID AND WO.WorkTypeID=HE.WorkTypeID " &
                                 " LEFT JOIN " & eHRMSDB_ITRD & ".[dbo].[HRCodeMap] HR ON HR.TabName='Business' AND HR.FldName = 'BusinessType' AND HR.NotShowFlag='0' AND HR.Code = HE.BusinessType" &
                                 " LEFT JOIN " & eHRMSDB_ITRD & ".[dbo].[HRCodeMap] HR1 ON HR1.TabName='EmpFlowRemark' AND HR1.FldName = HR.Code AND HR1.NotShowFlag='0' AND HR1.Code = HE.EmpFlowRemark" &
                                 " LEFT JOIN " & eHRMSDB_ITRD & ".[dbo].[RankMapping] RMT ON HE.CompID = RMT.CompID AND HE.RankIDTop = RMT.RankID" &
                                 " LEFT JOIN " & eHRMSDB_ITRD & ".[dbo].[RankMapping] RMB ON HE.CompID = RMB.CompID AND HE.RankIDBottom = RMB.RankID"





    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            If UserProfile.SelectCompRoleID = "ALL" Then 'SelectCompRoleID：授權公司代碼
                lblCompID.Visible = False
                Bsp.Utility.FillHRCompany(ddlCompID) 'FillHRCompany：填寫HR公司資料(公司代碼)
                Page.SetFocus(ddlCompID) '焦點停留在公司代碼下拉選項
            Else
                lblCompID.Text = UserProfile.SelectCompRoleName '如果授權公司代碼不是選擇ALL，
                ddlCompID.Visible = False '則看不到下拉選單，顯示該公司(label)
            End If
            LoadData()
        End If
    End Sub
    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        Dim bus As String = ""
        If Not IsPostBack() Then
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

            For Each strKey As String In ht.Keys
                Dim ctl As Control = Me.FindControl(strKey)
                If TypeOf ctl Is TextBox Then
                    CType(ctl, TextBox).Text = ht(strKey).ToString()
                ElseIf TypeOf ctl Is DropDownList Then
                    If ht(strKey).ToString <> "" Then
                        CType(ctl, DropDownList).SelectedValue = ht(strKey).ToString
                        If "ddlEmpFlowRemark" = strKey Then
                            Dim a As String = ht(strKey).ToString
                            bus = a
                        End If
                    End If
                ElseIf TypeOf ctl Is CheckBox Then
                    CType(ctl, CheckBox).Checked = ht(strKey)
                End If
            Next

            If ht.ContainsKey("PageNo") Then pcMain.PageNo = Convert.ToInt32(ht("PageNo"))
            If ht.ContainsKey("DoQuery") Then
                If ht("DoQuery").ToString() = "Y" Then
                    ViewState.Item("DoQuery") = "Y"
                    DoQuery()
                End If
            End If
            ddlRank_Changed(Me.FindControl("ddlRankB"), EventArgs.Empty)
            ddlRank_Changed(Me.FindControl("ddlRankE"), EventArgs.Empty)
            BusinessType()
            ddlEmpFlowRemark.SelectedValue = bus
        End If
    End Sub
    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnAdd"       '新增
                DoAdd()
            Case "btnUpdate"    '修改
                DoUpdate()
            Case "btnQuery"     '查詢
                DoQuery()
            Case "btnDelete"    '刪除
                DoDelete()
                resetGrid()
            Case "btnActionX"   '清除
                DoClear()
        End Select
    End Sub
    Public Overrides Sub DoModalReturn(ByVal returnValue As String)
        If returnValue <> "" Then
            Dim aryData() As String = returnValue.Split(":")
            Select Case aryData(0)
                Case "ucQueryEmpID" '員編uc
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    txtEmpID.Text = aryValue(1)
                    lblEmpID.Text = aryValue(2)
                    ddlFlowCompID.SelectedValue = aryValue(3)
            End Select
        End If
    End Sub
    Public Sub resetGrid()
        'Table顯示
        For i As Integer = 0 To gvMain.Rows.Count - 1 Step 1
            Dim objChk As CheckBox = gvMain.Rows(i).FindControl("chk_gvMain")
            If objChk.Checked Then
                objChk.Checked = False
            End If
        Next
        ScriptManager.RegisterStartupScript(Me, Page.GetType, "script", "gridClear();", True)
    End Sub
    Private Function getOverTimeMain() As String
        Dim result As String = ""

        'Check
        Dim checkCount As Integer = 0
        For c As Integer = 0 To gvMain.Rows.Count - 1 Step 1
            If gvMain.Rows.Count >= 1 Then
                Dim objChk As CheckBox = gvMain.Rows(c).FindControl("chk_gvMain")
                If objChk.Checked Then
                    checkCount = checkCount + 1
                End If
            End If
        Next
        If checkCount > 1 Then
            result = "刪除請選擇一筆資料刪除"
        ElseIf checkCount = 1 Then

            Dim strSQL As New StringBuilder
            Dim dataTable As New DataTable
            Dim compID As String = ""
            Dim flowCode As String = ""
            Dim flowSN As String = ""

            For intRow As Integer = 0 To gvMain.Rows.Count - 1
                If gvMain.Rows.Count >= 1 Then
                    Dim objChk As CheckBox = gvMain.Rows(intRow).FindControl("chk_gvMain")
                    If objChk.Checked Then
                        compID = UserProfile.SelectCompRoleID
                        flowCode = gvMain.DataKeys(intRow).Item("FlowCode").ToString
                        flowSN = gvMain.DataKeys(intRow).Item("FlowSN").ToString
                    End If
                End If
            Next

            strSQL.AppendLine("select FlowFlag from HROverTimeMain")
            strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(compID))
            strSQL.AppendLine(" And FlowCode = " & Bsp.Utility.Quote(flowCode))
            strSQL.AppendLine(" And FlowSN = " & Bsp.Utility.Quote(flowSN))
            strSQL.AppendLine(" And FlowFlag = '1' ")

            dataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "AattendantDB").Tables(0)

            If dataTable.Rows.Count <> 0 Then
                result = "簽核表單目前還在使用中，無法刪除此筆"
            End If

        End If

        

        Return result
    End Function
    Public Function getProcess(ByVal compID As String, ByVal systemID As String, ByVal flowCode As String, ByVal flowSN As String) As DataTable
        Dim result As New DataTable
        Dim strSQL_Main As New StringBuilder
        strSQL_Main.AppendLine("Select * FROM HRFlowEmpDefine")
        strSQL_Main.AppendLine("Where 1=1 ")
        strSQL_Main.AppendLine(" And PrincipalFlag = '1' ")
        strSQL_Main.AppendLine(" And FlowSN <> " & Bsp.Utility.Quote(flowSN))
        strSQL_Main.AppendLine(" And CompID = " & Bsp.Utility.Quote(compID))
        strSQL_Main.AppendLine(" And SystemID = " & Bsp.Utility.Quote(systemID))
        strSQL_Main.AppendLine(" And FlowCode = " & Bsp.Utility.Quote(flowCode))
        result = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL_Main.ToString(), "AattendantDB").Tables(0)
        Return result
    End Function
    Public Function beforeCheck() As String
        Dim result As String = ""
        Dim datatable As New DataTable
        Dim compID As String = ""
        Dim systemID As String = ""
        Dim flowCode As String = ""
        Dim flowSN As String = ""


        For intRow As Integer = 0 To gvMain.Rows.Count - 1
            If gvMain.Rows.Count >= 1 Then
                Dim objChk As CheckBox = gvMain.Rows(intRow).FindControl("chk_gvMain")
                If objChk.Checked Then
                    compID = gvMain.DataKeys(intRow)("CompID").ToString().Substring(0, 6)
                    systemID = gvMain.DataKeys(intRow)("SystemID").ToString()
                    flowCode = gvMain.DataKeys(intRow)("FlowCode").ToString()
                    flowSN = gvMain.DataKeys(intRow)("FlowSN").ToString()
                End If
            End If
        Next
        If compID <> "" And systemID <> "" And flowCode <> "" And flowSN <> "" Then
            datatable = getProcess(compID, systemID, flowCode, flowSN)
        End If

        If datatable.Rows.Count = 0 Then
            result = "此為唯一主要流程，不可移除，如欲調整主要流程，請至其他流程修改設定"
        End If
        Return result

    End Function
    Private Sub DoDelete()
        Dim strSQL As New StringBuilder()
        Dim chkCount As Integer = 0
        If ViewState.Item("DoQuery") <> "Y" Then
            Bsp.Utility.ShowMessage(Me, "請先查詢資料")
            Return
        End If
        If selectedRows(gvMain) = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00000")
            Return
        End If
        If getOverTimeMain() <> "" Then
            Bsp.Utility.ShowFormatMessage(Me, getOverTimeMain())
            Return
        End If
        If beforeCheck() <> "" Then
            Bsp.Utility.ShowMessage(Me, beforeCheck())
            Return
        End If
        For intRow As Integer = 0 To gvMain.Rows.Count - 1
            If gvMain.Rows.Count >= 1 Then
                Dim objChk As CheckBox = gvMain.Rows(intRow).FindControl("chk_gvMain")
                If objChk.Checked Then
                    chkCount = chkCount + 1
                    strSQL.AppendLine("Delete FROM HRFlowEmpDefine Where 1=1 ")
                    strSQL.AppendLine(" and FlowCode=" & Bsp.Utility.Quote(gvMain.DataKeys(intRow)("FlowCode").ToString()))
                    strSQL.AppendLine(" and FlowSN=" & Bsp.Utility.Quote(gvMain.DataKeys(intRow)("FlowSN").ToString()))
                    strSQL.AppendLine(" and CompID=" & Bsp.Utility.Quote(gvMain.DataKeys(intRow)("CompID").ToString().Substring(0, 6)))
                    strSQL.AppendLine(" and SystemID=" & Bsp.Utility.Quote(gvMain.DataKeys(intRow)("SystemID").ToString()))
                    strSQL.AppendLine(" and FlowCompID=" & Bsp.Utility.Quote(gvMain.DataKeys(intRow)("CompID").ToString()))
                    strSQL.AppendLine(" and DeptID=" & Bsp.Utility.Quote(gvMain.DataKeys(intRow)("DeptID").ToString()))
                    strSQL.AppendLine(" and OrganID=" & Bsp.Utility.Quote(gvMain.DataKeys(intRow)("OrganID").ToString()))
                    strSQL.AppendLine(" and EmpID=" & Bsp.Utility.Quote(gvMain.DataKeys(intRow)("EmpID").ToString()))
                    strSQL.AppendLine(" ;")
                End If
            End If
        Next
        If chkCount = 1 Then
            Using cn As DbConnection = Bsp.DB.getConnection("AattendantDB")
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction
                Try
                    Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran, "AattendantDB")
                    tran.Commit()
                Catch ex As Exception
                    tran.Rollback()
                    Bsp.Utility.ShowMessage(Me, "刪除失敗")
                    Throw
                Finally
                    If tran IsNot Nothing Then tran.Dispose()
                End Try
            End Using
        Else
            Bsp.Utility.ShowMessage(Me, "刪除請選定一筆資料")
            Return
        End If
        Bsp.Utility.ShowMessage(Me, "刪除成功")
        DoQuery()
    End Sub
    Private Sub DoQuery()

        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(_BaseSQL & " Where 0=0 ")
        If (lblCompID.Text <> "") Then
            strSQL.AppendLine(" AND HE.CompID = '" + UserProfile.SelectCompRoleID + "'")
        End If
        If (ddlFlowCode.SelectedValue <> "") Then
            strSQL.AppendLine(" AND HE.FlowCode = '" + ddlFlowCode.SelectedValue + "'")
        End If
        If (ddlFlowSN.SelectedValue <> "") Then
            strSQL.AppendLine(" AND HE.FlowSN = '" + ddlFlowSN.SelectedValue + "'")
        End If
        If (ddlFlowCompID.SelectedValue <> "") Then
            strSQL.AppendLine(" AND HE.CompID = '" + UserProfile.SelectCompRoleID + "'")
        End If
        If (txtEmpID.Text <> "") Then
            strSQL.AppendLine(" And HE.EmpID = '" + txtEmpID.Text + "'")
        End If
        If (ddlDeptID.SelectedValue <> "") Then
            strSQL.AppendLine(" AND HE.DeptID = '" + ddlDeptID.SelectedValue + "'")
        End If
        If (ddlOrganID.SelectedValue <> "") Then
            strSQL.AppendLine(" AND HE.OrganID = '" + ddlOrganID.SelectedValue + "'")
        End If
        If (ddlWorkTypeID.SelectedValue <> "") Then
            strSQL.AppendLine(" AND HE.WorkTypeID = '" + ddlWorkTypeID.SelectedValue + "'")
        End If
        'If (ddlRankID.SelectedValue <> "") Then
        '    strSQL.AppendLine(" AND FE.RankID = '" + ddlRankID.SelectedValue + "'")
        'End If
        '職等
        Dim Comp_Rank As String = UserProfile.SelectCompRoleID
        Dim sRank As String = ""
        Dim eRank As String = ""

        If (ddlRankB.SelectedValue <> "" And ddlRankE.SelectedValue <> "") Then
            sRank = OVBusinessCommon.GetRankID(Comp_Rank, ddlRankB.SelectedValue)
            eRank = OVBusinessCommon.GetRankID(Comp_Rank, ddlRankE.SelectedValue)
            If sRank > eRank Then
                Bsp.Utility.ShowMessage(Me, "職等(迄)不可小於職等(起) !!")
                Return
            Else
                strSQL.AppendLine(" AND RMT.RankIDMap >= '" + sRank + "' and RMB.RankIDMap <= '" + eRank + "'")
            End If

        ElseIf (ddlRankB.SelectedValue <> "") Then
            sRank = OVBusinessCommon.GetRankID(Comp_Rank, ddlRankB.SelectedValue)
            strSQL.AppendLine(" AND RMT.RankIDMap >= '" + sRank + "'")
        ElseIf (ddlRankE.SelectedValue <> "") Then
            eRank = OVBusinessCommon.GetRankID(Comp_Rank, ddlRankE.SelectedValue)
            strSQL.AppendLine(" AND RMB.RankIDMap <= '" + eRank + "'")
        End If


        If (ddlPositionID.SelectedValue <> "") Then
            strSQL.AppendLine(" AND HE.PositionID = '" + ddlPositionID.SelectedValue + "'")
        End If
        If chkInValidFlag.Checked Then
            strSQL.AppendLine(" AND HE.PrincipalFlag = '1'")
        Else
            'strSQL.AppendLine(" AND HE.PrincipalFlag = '0'")
        End If
        '永豐銀行登入才有業務類別
        If UserProfile.SelectCompRoleID = "SPHBK1" Then
            If (ddlBusinessType.SelectedValue <> "") Then
                strSQL.AppendLine(" AND HE.BusinessType = '" + ddlBusinessType.SelectedValue + "'")
            End If
        End If
        strSQL.AppendLine("Order By HE.FlowCode,HE.FlowSN")
        Dim dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "AattendantDB").Tables(0)
        For i As Integer = 0 To dt.Rows.Count - 1 Step 1
            Dim Top As String = dt.Rows(i).Item("RankIDTop").ToString.Trim
            Dim Buttom As String = dt.Rows(i).Item("RankIDBottom").ToString.Trim
            dt.Rows(i).Item("RankID") = Top & IIf(Buttom.Equals(""), "", "~" & Buttom)
        Next
        'ShowTable.Visible = True
        pcMain.DataTable = dt
        gvMain.DataBind()
        gvMain.Visible = True
        ViewState.Item("DoQuery") = "Y"
    End Sub
    Private Sub LoadData()
        Dim strCompID As String = ddlCompID.SelectedValue
        If strCompID = "" Then
            strCompID = UserProfile.SelectCompRoleID
        End If
        lblCompID.Text = UserProfile.SelectCompRoleName
        '流程代碼
        FillDDLOV9000(ddlFlowCode, "AattendantDB", "AT_CodeMap", "RTrim(Code)", "CodeCName", "", DisplayType.Full, "", " AND TabName='HRFlowEngine' AND FldName='FlowCode' AND NotShowFlag='0'", "ORDER BY Code, CodeCName")
        ddlFlowCode.Items.Insert(0, New ListItem("---請選擇---", ""))
        '適用公司代碼
        FillDDL(ddlFlowCompID, "eHRMSDB", "Company", "RTrim(CompID)", "CompName + Case When InValidFlag = '1' Then '(無效)' Else '' End", DisplayType.Full, "", "", " Order By InValidFlag, CompID ")
        ddlFlowCompID.Items.Insert(0, New ListItem("" + UserProfile.SelectCompRoleName + "", ""))
        '適用識別碼來源
        FillDDLOV9000(ddlFlowSN, "AattendantDB", " HRFlowEngine ", "RTrim(FlowSN)", "", "", DisplayType.OnlyID, "", " and CompID =" & Bsp.Utility.Quote(UserProfile.SelectCompRoleID))
        ddlFlowSN.Items.Insert(0, New ListItem("　- -請選擇- -", ""))

        '部門代碼
        FillDDL(ddlDeptID, "eHRMSDB", "Organization", "RTrim(OrganID)", "OrganName", DisplayType.Full, "", "AND CompID='" + UserProfile.SelectCompRoleID + "' and OrganID = DeptID AND InValidFlag='0' and VirtualFlag='0'", "Order By InValidFlag, OrganID")
        ddlDeptID.Items.Insert(0, New ListItem("---請選擇---", ""))

        '單位代碼
        FillDDL(ddlOrganID, "eHRMSDB", "Organization", "RTrim(OrganID)", "OrganName", DisplayType.Full, "", "AND CompID='" + UserProfile.SelectCompRoleID + "' and OrganID <> DeptID AND InValidFlag='0' and VirtualFlag='0'", "Order By InValidFlag, OrganID")
        ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
        '職等
        Bsp.Utility.FillDDL(ddlRankB, "eHRMSDB", "Rank", "distinct RankID", "RankID", Bsp.Utility.DisplayType.OnlyID, "", IIf(strCompID = "0", "", "and CompID = " & Bsp.Utility.Quote(strCompID)))
        ddlRankB.Items.Insert(0, New ListItem("---請選擇---", ""))
        Bsp.Utility.FillDDL(ddlRankE, "eHRMSDB", "Rank", "distinct RankID", "RankID", Bsp.Utility.DisplayType.OnlyID, "", IIf(strCompID = "0", "", "and CompID = " & Bsp.Utility.Quote(strCompID)))
        ddlRankE.Items.Insert(0, New ListItem("---請選擇---", ""))

        '職稱
        ddlTitleB.Items.Insert(0, New ListItem("---請先選擇職等---", ""))
        ddlTitleE.Items.Insert(0, New ListItem("---請先選擇職等---", ""))

        '工作性質
        FillDDL(ddlWorkTypeID, "eHRMSDB", " WorkType ", "WorkTypeID", "Remark", DisplayType.Full, "", "AND CompID='" + UserProfile.SelectCompRoleID + "'", "")
        ddlWorkTypeID.Items.Insert(0, New ListItem("---請選擇---", ""))
        '職位
        FillDDL(ddlPositionID, "eHRMSDB", "Position", "PositionID", "Remark", Bsp.Utility.DisplayType.Full, "", "AND CompID='" + UserProfile.SelectCompRoleID + "'", "")
        ddlPositionID.Items.Insert(0, New ListItem("---請選擇---", ""))

        ddlEmpFlowRemark.Items.Insert(0, New ListItem("---請先選擇業務類別---", ""))
        '永豐銀行登入有業務類別
        If UserProfile.SelectCompRoleID = "SPHBK1" Then
            FillDDL(ddlBusinessType, "eHRMSDB", "HRCodeMap", "RTrim(Code)", "CodeCName", DisplayType.Full, "", "And TabName = 'Business' And FldName = 'BusinessType'  and NotShowFlag='0'", "")
            ddlBusinessType.Items.Insert(0, New ListItem("---請選擇---", ""))
            lblBusinessType.Visible = True
            ddlBusinessType.Visible = True
            lblEmpFlowRemark.Visible = True
            ddlEmpFlowRemark.Visible = True
        End If
    End Sub

    Protected Sub DoUpdate()
        Dim i = ddlOrganID.SelectedValue
        If ViewState.Item("DoQuery") <> "Y" Then
            Bsp.Utility.ShowMessage(Me, "請先查詢並選取資料")
            Return
        End If
        If selectedRows(gvMain) = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00000")
            Return
        End If
        If selectedRows(gvMain) <> "" Then
            Dim intSelectRow As Integer
            Dim intSelectCount As Integer = 0
            For intRow As Integer = 0 To gvMain.Rows.Count - 1
                Dim objChk As CheckBox = gvMain.Rows(intRow).FindControl("chk_gvMain")
                If objChk.Checked Then
                    intSelectRow = intRow
                    intSelectCount = intSelectCount + 1
                End If
            Next
            If intSelectCount = 1 Then
                Dim btnU As New ButtonState(ButtonState.emButtonType.Update)
                Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
                Dim btnC As New ButtonState(ButtonState.emButtonType.Cancel)

                btnU.Caption = "存檔返回"
                btnX.Caption = "清除"
                btnC.Caption = "返回"

                Me.TransferFramePage("~/OV/OV9002.aspx", New ButtonState() {btnU, btnX, btnC}, _
            ddlSystemCode.ID & "=" & ddlSystemCode.SelectedValue, _
            ddlFlowCode.ID & "=" & ddlFlowCode.SelectedValue, _
            ddlFlowSN.ID & "=" & ddlFlowSN.SelectedValue, _
            ddlFlowCompID.ID & "=" & ddlFlowCompID.SelectedValue, _
            txtEmpID.ID & "=" & txtEmpID.Text, _
            ddlDeptID.ID & "=" & ddlDeptID.SelectedValue, _
            ddlOrganID.ID & "=" & ddlOrganID.SelectedValue, _
            ddlRankB.ID & "=" & ddlRankB.SelectedValue, _
            ddlRankE.ID & "=" & ddlRankE.SelectedValue, _
            ddlTitleB.ID & "=" & ddlTitleB.SelectedValue, _
            ddlTitleE.ID & "=" & ddlTitleE.SelectedValue, _
            ddlPositionID.ID & "=" & ddlPositionID.SelectedValue, _
            ddlWorkTypeID.ID & "=" & ddlWorkTypeID.SelectedValue, _
            ddlBusinessType.ID & "=" & ddlBusinessType.SelectedValue, _
            ddlEmpFlowRemark.ID & "=" & ddlEmpFlowRemark.SelectedValue, _
            chkInValidFlag.ID & "=" & chkInValidFlag.Checked, _
            "PageNo=" & pcMain.PageNo.ToString(), _
            "SelectedSystemCode=" & gvMain.DataKeys(intSelectRow)("SystemID").ToString(), _
            "SelectedFlowCode=" & gvMain.DataKeys(intSelectRow)("FlowCode").ToString(), _
            "SelectedFlowSN=" & gvMain.DataKeys(intSelectRow)("FlowSN").ToString(), _
            "SelectedCompID=" & gvMain.DataKeys(intSelectRow)("CompID").ToString(), _
            "SelectedEmpID=" & gvMain.DataKeys(intSelectRow)("EmpID").ToString(), _
            "SelectedDeptID=" & gvMain.DataKeys(intSelectRow)("DeptID").ToString(), _
            "SelectedOrganID=" & gvMain.DataKeys(intSelectRow)("OrganID").ToString(), _
            "SelectedBusinessType=" & gvMain.DataKeys(intSelectRow)("BusinessType").ToString(), _
            "SelectedRankID=" & gvMain.DataKeys(intSelectRow)("RankID").ToString(), _
            "SelectedTitleIDB=" & gvMain.DataKeys(intSelectRow)("TitleIDTop").ToString(), _
            "SelectedTitleIDE=" & gvMain.DataKeys(intSelectRow)("TitleIDBottom").ToString(), _
            "SelectedPositionID=" & gvMain.DataKeys(intSelectRow)("PositionID").ToString(), _
            "SelectedWorkTypeID=" & gvMain.DataKeys(intSelectRow)("WorkTypeID").ToString(), _
            "SelectedEmpFlowRemark=" & gvMain.DataKeys(intSelectRow)("EmpFlowRemark").ToString(), _
            "SelectedPrincipalFlag=" & gvMain.DataKeys(intSelectRow)("PrincipalFlag").ToString(), _
            "SelectedLastChgComp=" & gvMain.DataKeys(intSelectRow)("LastChgComp").ToString(), _
            "SelectedLastChgID=" & gvMain.DataKeys(intSelectRow)("LastChgID").ToString(), _
            "SelectedLastChgDate=" & gvMain.DataKeys(intSelectRow)("LastChgDate").ToString(), _
            "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
            Else
                Bsp.Utility.ShowMessage(Me, "修改只能選擇一筆資料")
            End If
        End If
    End Sub
    Protected Sub DoAdd()
        Dim btnA As New ButtonState(ButtonState.emButtonType.Add)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
        Dim btnC As New ButtonState(ButtonState.emButtonType.Cancel)
        btnA.Caption = "存檔"
        btnX.Caption = "清除"
        btnC.Caption = "返回"
        'ddlRankID.ID & "=" & ddlRankID.SelectedValue, _
        Me.TransferFramePage("~/OV/OV9001.aspx", New ButtonState() {btnA, btnX, btnC}, _
            ddlSystemCode.ID & "=" & ddlSystemCode.SelectedValue, _
            ddlFlowCode.ID & "=" & ddlFlowCode.SelectedValue, _
            ddlFlowSN.ID & "=" & ddlFlowSN.SelectedValue, _
            ddlFlowCompID.ID & "=" & ddlFlowCompID.SelectedValue, _
            txtEmpID.ID & "=" & txtEmpID.Text, _
            ddlDeptID.ID & "=" & ddlDeptID.SelectedValue, _
            ddlOrganID.ID & "=" & ddlOrganID.SelectedValue, _
            ddlRankB.ID & "=" & ddlRankB.SelectedValue, _
            ddlRankE.ID & "=" & ddlRankE.SelectedValue, _
            ddlTitleB.ID & "=" & ddlTitleB.SelectedValue, _
            ddlTitleE.ID & "=" & ddlTitleE.SelectedValue, _
            ddlPositionID.ID & "=" & ddlPositionID.SelectedValue, _
            ddlWorkTypeID.ID & "=" & ddlWorkTypeID.SelectedValue, _
            ddlBusinessType.ID & "=" & ddlBusinessType.SelectedValue, _
            ddlEmpFlowRemark.ID & "=" & ddlEmpFlowRemark.SelectedValue, _
            chkInValidFlag.ID & "=" & chkInValidFlag.Checked, _
            "PageNo=" & pcMain.PageNo.ToString(), _
            "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
    End Sub

    Protected Sub DoClear()

        ddlOrganID.Items.Clear()
        FillDDL(ddlOrganID, "eHRMSDB", "Organization", "RTrim(OrganID)", "OrganName", DisplayType.Full, "", "AND CompID='" + UserProfile.SelectCompRoleID + "' and OrganID <> DeptID AND InValidFlag='0' and VirtualFlag='0'", "Order By InValidFlag, OrganID")
        ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))

        ddlSystemCode.SelectedValue = ""
        ddlFlowCode.SelectedValue = ""
        ddlFlowSN.SelectedValue = ""
        ddlFlowCompID.SelectedValue = ""
        txtEmpID.Text = ""
        ddlDeptID.SelectedValue = ""
        ddlOrganID.SelectedValue = ""
        ddlRankB.SelectedValue = ""
        ddlRank_Changed(Me.FindControl("ddlRankB"), EventArgs.Empty)
        ddlRankE.SelectedValue = ""
        ddlRank_Changed(Me.FindControl("ddlRankE"), EventArgs.Empty)
        ddlTitleB.SelectedValue = ""
        ddlTitleE.SelectedValue = ""
        ddlPositionID.SelectedValue = ""
        ddlWorkTypeID.SelectedValue = ""
        ddlBusinessType.SelectedValue = ""
        ddlBusinessType_SelectedIndexChanged(Me.FindControl("ddlBusinessType"), EventArgs.Empty)
        ddlEmpFlowRemark.SelectedValue = ""
        lblEmpID.Text = ""
        chkInValidFlag.Checked = False
        'ShowTable.Visible = False
        'gvMain.Visible = False
        If Not pcMain.DataTable Is Nothing Then 'GridView
            pcMain.DataTable.Clear()
            pcMain.BindGridView()
        End If
        ViewState.Item("DoQuery") = ""
    End Sub

    Protected Sub txtEmpID_TextChanged(sender As Object, e As System.EventArgs) Handles txtEmpID.TextChanged
        If txtEmpID.Text <> "" And txtEmpID.Text.Length = 6 Then
            Dim objHR As New HR
            Dim rtnTable As DataTable = objHR.GetHREmpName(UserProfile.SelectCompRoleID, txtEmpID.Text)
            If rtnTable.Rows.Count <= 0 Then
                lblEmpID.Text = ""
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", "人事資料尚未建檔")
            Else
                'lblEmpID.Text = rtnTable.Rows(0).Item(0)
            End If
        Else
            lblEmpID.Text = ""
        End If
    End Sub
#Region "公司代碼"
    Protected Sub ddlFlowCompID_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlFlowCompID.SelectedIndexChanged
        'Bsp.Utility.Rank(ddlRankID, ddlFlowCompID.SelectedValue)
        ucQueryEmpID.SelectCompID = ddlFlowCompID.SelectedValue
        txtEmpID.Text = ""
        lblEmpID.Text = ""
    End Sub
#End Region

    Protected Sub gvMain_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMain.RowCommand
        If ViewState.Item("DoQuery") <> "Y" Then
            Bsp.Utility.ShowMessage(Me, "請先查詢並選取資料")
            Return
        End If
        Dim btnC As New ButtonState(ButtonState.emButtonType.Cancel)
        btnC.Caption = "返回"
        If e.CommandName = "Detail" Then
            Dim a As New FlowBackInfo()
            a.MenuNodeTitle = "回清單"
            a.URL = "~/OV/OV9000.aspx"
            If selectedRows(gvMain) <> "" Then
                Dim intSelectRow As Integer
                Dim intSelectCount As Integer = 0
                For intRow As Integer = 0 To gvMain.Rows.Count - 1
                    Dim objChk As CheckBox = gvMain.Rows(intRow).FindControl("chk_gvMain")
                    If objChk.Checked Then
                        intSelectRow = intRow
                        intSelectCount = intSelectCount + 1
                    End If
                Next
                If intSelectCount = 1 Then
                    'ddlRankID.ID & "=" & ddlRankID.SelectedValue, _
                    TransferFramePage("~/OV/OV9003.aspx", New ButtonState() {btnC}, a, _
                        lblCompID.ID & "&" & lblCompID.Text, _
                        ddlFlowCode.ID & "=" & ddlFlowCode.SelectedValue, _
                        ddlFlowSN.ID & "=" & ddlFlowSN.SelectedValue, _
                        ddlFlowCompID.ID & "=" & ddlFlowCompID.SelectedValue, _
                        txtEmpID.ID & "=" & txtEmpID.Text, _
                        ddlOrganID.ID & "=" & ddlOrganID.SelectedValue, _
                        ddlWorkTypeID.ID & "=" & ddlWorkTypeID.SelectedValue, _
                        ddlPositionID.ID & "=" & ddlPositionID.SelectedValue, _
                        ddlBusinessType.ID & "=" & ddlBusinessType.SelectedValue, _
                        "PageNo=" & pcMain.PageNo.ToString(), _
                        "SelectedFlowCode=" & gvMain.DataKeys(intSelectRow)("FlowCode").ToString(), _
                        "SelectedFlowSN=" & gvMain.DataKeys(intSelectRow)("FlowSN").ToString(), _
                        "SelectedCompID=" & UserProfile.SelectCompRoleID, _
                        "SelectedEmpID=" & gvMain.DataKeys(intSelectRow)("EmpID").ToString(), _
                        "SelectedOrganID=" & gvMain.DataKeys(intSelectRow)("OrganID").ToString(), _
                        "SelectedWorkTypeID=" & gvMain.DataKeys(intSelectRow)("WorkTypeID").ToString(), _
                        "SelectedRankID=" & gvMain.DataKeys(intSelectRow)("RankID").ToString, _
                        "SelectedPositionID=" & gvMain.DataKeys(intSelectRow)("PositionID").ToString(), _
                        "SelectedBusinessType=" & gvMain.DataKeys(intSelectRow)("BusinessType").ToString(), _
                        "SelectedInValidFlag=" & gvMain.DataKeys(intSelectRow)("InValidFlag").ToString(), _
                        "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
                End If
            End If
        End If
    End Sub

#Region "下拉選單-DisplayType"
    Enum DisplayType
        OnlyName    '只顯示名字
        OnlyID      '顯示ID  
        Full        '顯示ID + 名字
    End Enum
    Public Shared Sub FillDDLOV9000(ByVal objDDL As DropDownList, ByVal DBName As String, ByVal strTabName As String, ByVal strValue As String, ByVal strText As String, ByVal str3rdOrder As String, Optional ByVal Type As DisplayType = DisplayType.Full, Optional ByVal JoinStr As String = "", Optional ByVal WhereStr As String = "", Optional ByVal OrderByStr As String = "")
        Try
            Using dt As DataTable = GetDDLInfoOV9000(DBName, strTabName, strValue, strText, str3rdOrder, JoinStr, WhereStr, OrderByStr)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    Select Case Type
                        Case DisplayType.OnlyID
                            .DataTextField = "Code"
                        Case DisplayType.OnlyName
                            .DataTextField = "CodeName"
                        Case Else
                            .DataTextField = "FullName"
                    End Select
                    .DataValueField = "Code"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Shared Function GetDDLInfoOV9000(ByVal DBName As String, ByVal strTabName As String, ByVal strValue As String, ByVal strText As String, ByVal str3rdOrder As String, Optional ByVal JoinStr As String = "", Optional ByVal WhereStr As String = "", Optional ByVal OrderByStr As String = "") As DataTable
        Dim strSQL As New StringBuilder
        strSQL.AppendLine("Select distinct " & strValue & " AS Code")
        If strText <> "" Then strSQL.AppendLine(", " & strText & " AS CodeName, " & strValue & " + '-' + " & strText & " AS FullName ")
        If str3rdOrder <> "" Then strSQL.AppendLine(", " & str3rdOrder)
        strSQL.AppendLine("FROM " & strTabName)
        If JoinStr <> "" Then strSQL.AppendLine(JoinStr)
        strSQL.AppendLine("Where 1=1")
        If WhereStr <> "" Then strSQL.AppendLine(WhereStr)
        If OrderByStr <> "" Then
            strSQL.AppendLine(OrderByStr)
        Else
            strSQL.AppendLine("Order By " & strValue.Replace("distinct", ""))
        End If
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "" + DBName + "").Tables(0)
    End Function
#End Region
#Region "普通的FillDDL"
    Public Shared Sub FillDDL(ByVal objDDL As DropDownList, ByVal DBName As String, ByVal strTabName As String, ByVal strValue As String, ByVal strText As String, Optional ByVal Type As DisplayType = DisplayType.Full, Optional ByVal JoinStr As String = "", Optional ByVal WhereStr As String = "", Optional ByVal OrderByStr As String = "")
        Try
            Using dt As DataTable = GetDDLInfo(DBName, strTabName, strValue, strText, JoinStr, WhereStr, OrderByStr)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    Select Case Type
                        Case DisplayType.OnlyID
                            .DataTextField = "Code"
                        Case DisplayType.OnlyName
                            .DataTextField = "CodeName"
                        Case Else
                            .DataTextField = "FullName"
                    End Select
                    .DataValueField = "Code"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Shared Function GetDDLInfo(ByVal DBName As String, ByVal strTabName As String, ByVal strValue As String, ByVal strText As String, Optional ByVal JoinStr As String = "", Optional ByVal WhereStr As String = "", Optional ByVal OrderByStr As String = "") As DataTable
        Dim strSQL As New StringBuilder
        strSQL.AppendLine("Select " & strValue & " AS Code")
        If strText <> "" Then strSQL.AppendLine(", " & strText & " AS CodeName, " & strValue & " + '-' + " & strText & " AS FullName ")
        strSQL.AppendLine("FROM " & strTabName)
        If JoinStr <> "" Then strSQL.AppendLine(JoinStr)
        strSQL.AppendLine("Where 1=1")
        If WhereStr <> "" Then strSQL.AppendLine(WhereStr)
        If OrderByStr <> "" Then
            strSQL.AppendLine(OrderByStr)
        Else
            strSQL.AppendLine("Order By " & strValue.Replace("distinct", ""))
        End If
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "" + DBName + "").Tables(0)
    End Function
#End Region

    Public Shared Function Query(ByVal DBName As String, ByVal strValue As String, ByVal strTab As String) As DataTable
        Dim strSQL As New StringBuilder
        strSQL.AppendLine("Select distinct " & strValue & "")
        strSQL.AppendLine(" from " & strTab & "")
        strSQL.AppendLine(" order by " & strValue & " asc")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "" + DBName + "").Tables(0)
    End Function

#Region "適用部門代碼、適用單位代碼"
    Protected Sub ddlDeptID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDeptID.SelectedIndexChanged
        ddlOrganID.Items.Clear()
        FillDDL(ddlOrganID, "eHRMSDB", "Organization", "RTrim(OrganID)", "OrganName", DisplayType.Full, "", "AND CompID='" + UserProfile.SelectCompRoleID + "' and OrganID <> DeptID AND InValidFlag='0' AND VirtualFlag='0' and DeptID= " & Bsp.Utility.Quote(ddlDeptID.SelectedValue), "Order By InValidFlag, OrganID")
        ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
    End Sub
#End Region

#Region "適用職等、適用職稱"
    Protected Sub ddlRank_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRankB.SelectedIndexChanged, ddlRankE.SelectedIndexChanged
        Dim strCompID As String = ddlCompID.SelectedValue
        If strCompID = "" Then
            strCompID = UserProfile.SelectCompRoleID
        End If

        Dim ddlRank As DropDownList = CType(sender, DropDownList)
        Dim ddlTitle As DropDownList = Me.FindControl(ddlRank.ID.Replace("Rank", "Title"))

        If ddlRank.SelectedValue = "" Then
            ddlTitle.Items.Clear()
            ddlTitle.Items.Insert(0, New ListItem("---請先選擇職等---", ""))
        Else
            Bsp.Utility.FillDDL(ddlTitle, "eHRMSDB", "Title", "distinct TitleID", "TitleName", Bsp.Utility.DisplayType.Full, "", IIf(strCompID = "0", "", "and CompID = " & Bsp.Utility.Quote(strCompID)) & " and RankID = " & Bsp.Utility.Quote(ddlRank.SelectedValue))
            ddlTitle.Items.Insert(0, New ListItem("---請選擇---", ""))
        End If
    End Sub
#End Region
    
#Region "適用業務類別、適用功能備註"
    Protected Sub ddlBusinessType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlBusinessType.SelectedIndexChanged
        BusinessType()
    End Sub
    Public Sub BusinessType()
        If ddlBusinessType.SelectedValue = "" Then
            ddlEmpFlowRemark.Items.Clear()
            ddlEmpFlowRemark.Items.Insert(0, New ListItem("---請先選擇業務類別---", ""))
        Else
        FillDDL(ddlEmpFlowRemark, "eHRMSDB", "HRCodeMap", "Code", "CodeCName", Bsp.Utility.DisplayType.Full, "", " and TabName ='EmpFlowRemark' and FldName= " & Bsp.Utility.Quote(ddlBusinessType.SelectedValue))
        ddlEmpFlowRemark.Items.Insert(0, New ListItem("---請選擇---", ""))
        End If
    End Sub
#End Region

    Protected Sub ddlFlowCode_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlFlowCode.SelectedIndexChanged
        Dim flowCode As String = ddlFlowCode.SelectedValue
        Dim compID As String = UserProfile.SelectCompRoleID
        Dim sysID As String = "OT"
        If flowCode <> "" Then
            ddlFlowSN.Items.Clear()
            FillDDLOV9000(ddlFlowSN, "AattendantDB", " HRFlowEngine ", "RTrim(FlowSN)", "", "", DisplayType.OnlyID, "", " and CompID =" & Bsp.Utility.Quote(compID) & " and SystemID= " & Bsp.Utility.Quote(sysID) & " and FlowCode = " & Bsp.Utility.Quote(flowCode))
            ddlFlowSN.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        Else
            ddlFlowSN.Items.Clear()
            FillDDLOV9000(ddlFlowSN, "AattendantDB", " HRFlowEngine ", "RTrim(FlowSN)", "", "", DisplayType.OnlyID, "", " and CompID =" & Bsp.Utility.Quote(compID))
            ddlFlowSN.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        End If
    End Sub
End Class