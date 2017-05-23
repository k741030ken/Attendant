Imports System.Data
Imports System.Data.Common
Partial Class ATWF_ATWF12
    Inherits PageBase
    Private _BaseSQL As String = "SELECT FE.FlowCode,FE.FlowSN,C.CompID+'-'+C.CompName as CompID,EmpID,FE.OrganID,FE.BusinessType,FE.RankID,FE.PositionID,WorkTypeID" &
                                 " FROM Flow_Emp_Define FE " &
                                 "LEFT JOIN Company C ON C.CompID=FE.CompID"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then

        End If
    End Sub
    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            'LoadData()
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)
            Dim aryValue() As String = ht("SelectedCompID").ToString().Split("-")
            ViewState.Item("SystemCode") = ht("SelectedSystemCode").ToString().Trim
            ViewState.Item("FlowCode") = ht("SelectedFlowCode").ToString().Trim
            ViewState.Item("FlowSN") = ht("SelectedFlowSN").ToString().Trim
            ViewState.Item("CompID") = aryValue(0)
            ViewState.Item("EmpID") = ht("SelectedEmpID").ToString().Trim
            ViewState.Item("DeptID") = ht("SelectedDeptID").ToString().Trim
            ViewState.Item("OrganID") = ht("SelectedOrganID").ToString().Trim
            ViewState.Item("BusinessType") = ht("SelectedBusinessType").ToString().Trim
            ViewState.Item("RankID") = ht("SelectedRankID").ToString().Trim
            ViewState.Item("TitleIDB") = ht("SelectedTitleIDB").ToString().Trim
            ViewState.Item("TitleIDE") = ht("SelectedTitleIDE").ToString().Trim
            ViewState.Item("PositionID") = ht("SelectedPositionID").ToString().Trim
            ViewState.Item("EmpFlowRemark") = ht("SelectedEmpFlowRemark").ToString().Trim
            ViewState.Item("WorkTypeID") = ht("SelectedWorkTypeID").ToString().Trim
            ViewState.Item("PrincipalFlag") = ht("SelectedPrincipalFlag").ToString().Trim
            ViewState.Item("LastChgComp") = ht("SelectedLastChgComp").ToString().Trim
            ViewState.Item("LastChgID") = ht("SelectedLastChgID").ToString().Trim
            ViewState.Item("LastChgDate") = ht("SelectedLastChgDate").ToString().Trim
            LoadData()
            'DoClear()
            GetEmpName()

            ucQueryEmpID.ShowCompRole = "True"
            ucQueryEmpID.InValidFlag = "N"
            ucQueryEmpID.SelectCompID = ddlFlowCompID.SelectedValue

            initScreen()
        End If
    End Sub

    Public Sub initScreen()
        Dim RankID As String = ViewState.Item("RankID").ToString.Trim
        Dim RanlIDB As String = ""
        Dim RanlIDE As String = ""
        If RankID.Length > 3 Then
            RanlIDB = RankID.Substring(0, 2)
            RanlIDE = RankID.Substring(3)
        End If
        ddlSystemCode.SelectedValue = ViewState.Item("SystemCode").ToString.Trim
        ddlFlowCode.SelectedValue = ViewState.Item("FlowCode").ToString.Trim
        ddlFlowSN.SelectedValue = ViewState.Item("FlowSN").ToString.Trim
        txtEmpID.Text = ViewState.Item("EmpID").ToString.Trim
        ddlDeptID.SelectedValue = ViewState.Item("DeptID").ToString.Trim
        If ViewState.Item("DeptID").ToString.Trim <> "" Then
            ddlDeptID.SelectedValue = ViewState.Item("DeptID").ToString.Trim
            deptIDchang()
        Else
            ddlOrganID.Items.Clear()
            FillDDL(ddlOrganID, "eHRMSDB", "Organization", "RTrim(OrganID)", "OrganName + case when InValidFlag = '1' then '(無效)' else '' end", DisplayType.Full, "", "AND CompID='" + UserProfile.SelectCompRoleID + "' and OrganID <> DeptID AND InValidFlag='0' ", "Order By InValidFlag, OrganID")
            ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
        End If
        ddlOrganID.SelectedValue = ViewState.Item("OrganID").ToString.Trim
        ddlRankB.SelectedValue = RanlIDB
        ddlRank_Changed(Me.FindControl("ddlRankB"), EventArgs.Empty)
        ddlRankE.SelectedValue = RanlIDE
        ddlRank_Changed(Me.FindControl("ddlRankE"), EventArgs.Empty)
        ddlTitleB.SelectedValue = ViewState.Item("TitleIDB").ToString.Trim
        ddlTitleE.SelectedValue = ViewState.Item("TitleIDE").ToString.Trim
        ddlPositionID.SelectedValue = ViewState.Item("PositionID").ToString.Trim
        ddlWorkTypeID.SelectedValue = ViewState.Item("WorkTypeID").ToString.Trim
        ddlBusinessType.SelectedValue = ViewState.Item("BusinessType").ToString.Trim
        ddlBusinessType_Changed()
        ddlEmpFlowRemark.SelectedValue = ViewState.Item("EmpFlowRemark").ToString.Trim
        If ViewState.Item("PrincipalFlag") = "1" Then
            chkInValidFlag.Checked = True
        Else
            chkInValidFlag.Checked = False
        End If
        lblLastChgComp.Text = ViewState.Item("LastChgComp").ToString & "-" & getCompany(ViewState.Item("LastChgComp").ToString)
        lblLastChgID.Text = ViewState.Item("LastChgID").ToString & "-" & getName(ViewState.Item("LastChgComp").ToString, ViewState.Item("LastChgID").ToString)
        lblLastChgDate.Text = ViewState.Item("LastChgDate").ToString
        '欄位鎖定
        ddlFlowCode.Enabled = False
        ddlFlowSN.Enabled = False
        ddlSystemCode.Enabled = False
        '永豐銀行登入有業務類別
        If UserProfile.SelectCompRoleID = "SPHBK1" Then
            BusinessType.Visible = True
            lblBusinessType.Visible = True
            ddlBusinessType.Visible = True
            lblEmpFlowRemark.Visible = True
            ddlEmpFlowRemark.Visible = True
        End If

    End Sub
    Public Function getCompany(ByVal compID As String) As String
        Dim result As String = ""
        Dim strSQL As New StringBuilder
        Dim dataTable As New DataTable
        strSQL.Append("select CompName FROM Company ")
        strSQL.Append("where CompID = " & Bsp.Utility.Quote(compID))
        dataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)

        result = dataTable.Rows(0).Item("CompName").ToString
        Return result
    End Function

    Public Function getName(ByVal compID As String, ByVal empID As String) As String
        Dim result As String = ""
        Dim strSQL As New StringBuilder
        Dim dataTable As New DataTable
        strSQL.Append("select NameN FROM Personal ")
        strSQL.Append("where  CompID= " & Bsp.Utility.Quote(compID))
        strSQL.Append(" And EmpID = " & Bsp.Utility.Quote(empID))
        dataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)

        result = dataTable.Rows(0).Item("NameN").ToString
        Return result
    End Function
    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnUpdate"       '修改
                'Bsp.Utility.RunClientScript(Me.Page, "BtnClick();")
                If CheckData() Then
                    beforeCheck()
                End If
            Case "btnCancel"    '返回
                GoBack()
            Case "btnActionX"   '清除
                initScreen()

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
    Private Function CheckData() As Boolean
        If ddlSystemCode.SelectedValue = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00035", "系統代碼")
            Return False
        End If
        If ddlFlowCode.SelectedValue = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00035", "流程代碼")
            Return False
        End If
        If ddlFlowSN.SelectedValue = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00035", "流程識別碼")
            Return False
        End If
        Try
            If ddlRankB.SelectedValue <> "" And ddlRankE.SelectedValue <> "" Then
                '職等
                Dim Comp_Rank As String = UserProfile.SelectCompRoleID
                Dim sRank As String = ""
                Dim eRank As String = ""
                sRank = OVBusinessCommon.GetRankID(Comp_Rank, ddlRankB.SelectedValue)
                eRank = OVBusinessCommon.GetRankID(Comp_Rank, ddlRankE.SelectedValue)
                If sRank > eRank Then
                    Bsp.Utility.ShowMessage(Me, "適用職等選擇有誤")
                    ddlRankE.SelectedValue = ""
                    ddlRank_Changed(Me.FindControl("ddlRankE"), EventArgs.Empty)
                    Return False
                End If
            End If
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, ex.Message)
        End Try



        '檢核資料是否重複
        Dim strSQL As String = ""
        strSQL += "CREATE TABLE #tmpHRFlowEmpDefine "
        strSQL += " ( "
        strSQL += " CompID char(6),  "
        strSQL += " SystemID char(3),  "
        strSQL += " FlowCode varchar(12), "
        strSQL += " FlowSN varchar(12), "
        strSQL += " FlowCompID char(6), "
        strSQL += " EmpID char(6), "
        strSQL += " DeptID varchar(12), "
        strSQL += " OrganID varchar(12), "
        strSQL += " RankIDTop char(2), "
        strSQL += " RankIDBottom char(2), "
        strSQL += " TitleIDTop char(2), "
        strSQL += " TitleIDBottom char(2), "
        strSQL += " PositionID varchar(12), "
        strSQL += " WorkTypeID varchar(12), "
        strSQL += " BusinessType char(2), "
        strSQL += " EmpFlowRemark char(2), "
        strSQL += " PrincipalFlag char(1), "
        strSQL += " Seq tinyint, "
        strSQL += " InValidFlag char(1), "
        strSQL += " LastChgComp varchar(6), "
        strSQL += " LastChgID char(6), "
        strSQL += " LastChgDate datetime ) "
        strSQL += " Begin"
        strSQL += " INSERT INTO #tmpHRFlowEmpDefine "
        strSQL += " SELECT * FROM HRFlowEmpDefine WHERE NOT (CompID=" & Bsp.Utility.Quote(UserProfile.SelectCompRoleID) & _
                                            " AND SystemID =" & Bsp.Utility.Quote("OT") & _
                                            " AND FlowCode =" & Bsp.Utility.Quote(ViewState.Item("FlowCode")) & _
                                            " AND FlowSN =" & Bsp.Utility.Quote(ViewState.Item("FlowSN")) & _
                                            " AND FlowCompID =" & Bsp.Utility.Quote(UserProfile.SelectCompRoleID) & _
                                            " AND DeptID =" & Bsp.Utility.Quote(ViewState.Item("DeptID")) & _
                                            " AND OrganID = " & Bsp.Utility.Quote(ViewState.Item("OrganID")) & _
                                            " AND EmpID = " & Bsp.Utility.Quote(ViewState.Item("EmpID")) & ")"
        strSQL += " End"
        strSQL += " SELECT * FROM #tmpHRFlowEmpDefine WHERE CompID=" & Bsp.Utility.Quote(UserProfile.SelectCompRoleID) & _
                                            " AND SystemID =" & Bsp.Utility.Quote("OT") & _
                                            " AND FlowCode =" & Bsp.Utility.Quote(ddlFlowCode.SelectedValue) & _
                                            " AND FlowSN =" & Bsp.Utility.Quote(ddlFlowSN.SelectedValue) & _
                                            " AND FlowCompID =" & Bsp.Utility.Quote(UserProfile.SelectCompRoleID) & _
                                            " AND DeptID =" & Bsp.Utility.Quote(ddlDeptID.SelectedValue) & _
                                            " AND OrganID = " & Bsp.Utility.Quote(ddlOrganID.SelectedValue) & _
                                            " AND EmpID = " & Bsp.Utility.Quote(txtEmpID.Text)
        Dim dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "AattendantDB").Tables(0)
        If dt.Rows.Count = 1 Then
            Bsp.Utility.ShowMessage(Me, "您欲修改的資料已存在")
            strSQL += " DROP TABLE #tmpHRFlowEmpDefine "
            Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "AattendantDB")
            Return False
        End If
        Return True
    End Function

    Private Sub GoBack()
        Dim ti As TransferInfo = Me.StateTransfer
        Me.TransferFramePage(ti.CallerUrl, Nothing, ti.Args)
    End Sub

    Private Sub LoadData()
        Dim strCompID As String = ViewState.Item("CompID")
        lblCompID.Text = UserProfile.SelectCompRoleName
        '流程代碼
        FillDDLATWF10(ddlFlowCode, "AattendantDB", "AT_CodeMap", "RTrim(Code)", "CodeCName", "", DisplayType.Full, "", " AND TabName='HRFlowEngine' AND FldName='FlowCode' AND NotShowFlag='0'", "ORDER BY Code, CodeCName")
        ddlFlowCode.Items.Insert(0, New ListItem("---請選擇---", ""))
        '適用公司代碼
        FillDDL(ddlFlowCompID, "eHRMSDB", "Company", "RTrim(CompID)", "CompName + Case When InValidFlag = '1' Then '(無效)' Else '' End", DisplayType.Full, "", "", " Order By InValidFlag, CompID ")
        ddlFlowCompID.Items.Insert(0, New ListItem("" + UserProfile.SelectCompRoleName + "", ""))
        '適用識別碼來源
        FillDDLATWF10(ddlFlowSN, "AattendantDB", " HRFlowEngine ", "RTrim(FlowSN)", "", "", DisplayType.OnlyID, "", "", "")
        ddlFlowSN.Items.Insert(0, New ListItem("---請選擇---", ""))
        'Using dtFlowSN As DataTable = Query("AattendantDB", "RTrim(FlowSN)", "HRFlowEngine")
        '    ddlFlowSN.DataSource = dtFlowSN
        '    ddlFlowSN.DataValueField = "FlowSN"
        '    ddlFlowSN.DataTextField = "FlowSN"
        '    ddlFlowSN.DataBind()
        '    ddlFlowSN.Items.Insert(0, New ListItem("---請選擇---", ""))
        'End Using

        '部門代碼
        FillDDL(ddlDeptID, "eHRMSDB", "Organization", "RTrim(OrganID)", "OrganName", DisplayType.Full, "", "AND CompID='" + UserProfile.SelectCompRoleID + "' and OrganID = DeptID AND InValidFlag='0' and VirtualFlag='0'", "Order By InValidFlag, OrganID")
        ddlDeptID.Items.Insert(0, New ListItem("---請選擇---", ""))

        '單位代碼
        FillDDL(ddlOrganID, "eHRMSDB", "Organization", "RTrim(OrganID)", "OrganName + case when InValidFlag = '1' then '(無效)' else '' end", DisplayType.Full, "", "AND CompID='" + UserProfile.SelectCompRoleID + "' and OrganID <> DeptID AND InValidFlag='0' ", "Order By InValidFlag, OrganID")
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
            FillDDL(ddlBusinessType, "eHRMSDB", "HRCodeMap", "RTRIM(Code)", "CodeCName", DisplayType.Full, "", " AND TabName='Business'  and FldName= 'BusinessType' and NotShowFlag='0' ", " order by Code ")
            ddlBusinessType.Items.Insert(0, New ListItem("---請選擇---", ""))
            lblBusinessType.Visible = True
            ddlBusinessType.Visible = True
        End If
    End Sub

    Protected Sub DoUpdate(ByVal mainchk As Boolean)

        Dim strSQL As New StringBuilder()
        Dim successFlag As Boolean = False

        Dim strSQL_Main As New StringBuilder
        If mainchk Then
            Dim temptable As DataTable = ViewState.Item("mainData")
            For i As Integer = 0 To temptable.Rows.Count - 1 Step 1
                Dim chkCompID As String = temptable.Rows(i).Item("CompID").ToString
                Dim chkSystemID As String = temptable.Rows(i).Item("SystemID").ToString
                Dim chkFlowCode As String = temptable.Rows(i).Item("FlowCode").ToString
                Dim chkFlowSN As String = temptable.Rows(i).Item("FlowSN").ToString
                Dim chkFlowCompID As String = temptable.Rows(i).Item("FlowCompID").ToString
                Dim chkDeptID As String = temptable.Rows(i).Item("DeptID").ToString
                Dim chkOrganID As String = temptable.Rows(i).Item("OrganID").ToString
                Dim chkEmpID As String = temptable.Rows(i).Item("EmpID").ToString

                strSQL_Main.AppendLine("Update HRFlowEmpDefine ")
                strSQL_Main.AppendLine("Set PrincipalFlag = '0' ")
                strSQL_Main.AppendLine("Where CompID = " & Bsp.Utility.Quote(chkCompID))
                strSQL_Main.AppendLine(" And SystemID = " & Bsp.Utility.Quote(chkSystemID))
                strSQL_Main.AppendLine(" And FlowCode = " & Bsp.Utility.Quote(chkFlowCode))
                strSQL_Main.AppendLine(" And FlowSN = " & Bsp.Utility.Quote(chkFlowSN))
                strSQL_Main.AppendLine(" And FlowCompID = " & Bsp.Utility.Quote(chkFlowCompID))
                strSQL_Main.AppendLine(" And DeptID = " & Bsp.Utility.Quote(chkDeptID))
                strSQL_Main.AppendLine(" And OrganID = " & Bsp.Utility.Quote(chkOrganID))
                strSQL_Main.AppendLine(" And EmpID = " & Bsp.Utility.Quote(chkEmpID))
                strSQL_Main.AppendLine(";")
            Next
        End If

        strSQL.Append("UPDATE HRFlowEmpDefine")
        strSQL.Append(" SET")
        strSQL.Append(" EmpID=" & Bsp.Utility.Quote(txtEmpID.Text))
        strSQL.Append(" ,DeptID=" & Bsp.Utility.Quote(ddlDeptID.SelectedValue))
        strSQL.Append(" ,OrganID=" & Bsp.Utility.Quote(ddlOrganID.SelectedValue))
        strSQL.Append(" ,RankIDTop=" & Bsp.Utility.Quote(ddlRankB.SelectedValue))
        strSQL.Append(" ,RankIDBottom=" & Bsp.Utility.Quote(ddlRankE.SelectedValue))
        strSQL.Append(" ,TitleIDTop=" & Bsp.Utility.Quote(ddlTitleB.SelectedValue))
        strSQL.Append(" ,TitleIDBottom=" & Bsp.Utility.Quote(ddlTitleE.SelectedValue))
        strSQL.Append(" ,PositionID=" & Bsp.Utility.Quote(ddlPositionID.SelectedValue))
        strSQL.Append(" ,WorkTypeID=" & Bsp.Utility.Quote(ddlWorkTypeID.SelectedValue))
        strSQL.Append(" ,BusinessType=" & Bsp.Utility.Quote(ddlBusinessType.SelectedValue))
        strSQL.Append(" ,EmpFlowRemark=" & Bsp.Utility.Quote(ddlEmpFlowRemark.SelectedValue))
        strSQL.Append(" ,PrincipalFlag=" & Bsp.Utility.Quote(IIf(chkInValidFlag.Checked, "1", "0")))
        'Dim InValidFlag As String
        'If chkInValidFlag.Checked Then
        '    InValidFlag = 1
        'Else
        '    InValidFlag = 0
        'End If
        'strSQL.Append(" ,InValidFlag=" & Bsp.Utility.Quote(InValidFlag))
        strSQL.Append(" ,LastChgComp=" & Bsp.Utility.Quote(UserProfile.ActCompID))
        strSQL.Append(" ,LastChgID=" & Bsp.Utility.Quote(UserProfile.ActUserID))
        strSQL.Append(" ,LastChgDate=" & Bsp.Utility.Quote(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")))
        strSQL.Append(" WHERE 1 = 1")
        strSQL.Append(" AND CompID=" & Bsp.Utility.Quote(UserProfile.SelectCompRoleID))
        strSQL.Append(" AND SystemID=" & Bsp.Utility.Quote("OT"))
        strSQL.Append(" AND FlowCode=" & Bsp.Utility.Quote(ViewState.Item("FlowCode")))
        strSQL.Append(" AND FlowSN=" & Bsp.Utility.Quote(ViewState.Item("FlowSN")))
        strSQL.Append(" AND FlowCompID=" & Bsp.Utility.Quote(UserProfile.SelectCompRoleID))
        strSQL.Append(" AND DeptID=" & Bsp.Utility.Quote(ViewState.Item("DeptID")))
        strSQL.Append(" AND OrganID=" & Bsp.Utility.Quote(ViewState.Item("OrganID")))
        strSQL.Append(" AND EmpID=" & Bsp.Utility.Quote(ViewState.Item("EmpID")))
        Using cn As DbConnection = Bsp.DB.getConnection("AattendantDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Try
                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL_Main.ToString & strSQL.ToString(), tran, "AattendantDB")
                tran.Commit()
                successFlag = True
            Catch ex As Exception
                tran.Rollback()
                Bsp.Utility.ShowMessage(Me, "修改失敗")
                Throw
            Finally
                If tran IsNot Nothing Then tran.Dispose()
            End Try
        End Using
        If successFlag Then
            Bsp.Utility.ShowMessage(Me, "修改成功")
            'MsgBox("修改成功", MsgBoxStyle.OkOnly, "提示訊息")
            Message()
        End If
    End Sub
    Public Sub Message()
        Dim isSuccess As Boolean = False
        Dim sCompID As String = "SPHBK1"
        Dim sEmpID As String = IIf(txtEmpID.Text <> "", txtEmpID.Text, "")
        Dim sOrganID As String = IIf(ddlOrganID.SelectedValue <> "", ddlOrganID.SelectedValue, "")
        Dim sDeptID As String = IIf(ddlDeptID.SelectedValue <> "", ddlDeptID.SelectedValue, "")
        Dim sSystemID As String = "OT"
        Dim sFlowCode As String = IIf(ddlFlowCode.SelectedValue <> "", ddlFlowCode.SelectedValue, "")
        Dim sFlowSN As String = IIf(ddlFlowSN.SelectedValue <> "", ddlFlowSN.SelectedValue, "")
        Dim sRankIDTop As String = IIf(ddlRankB.SelectedValue <> "", ddlRankB.SelectedValue, "")
        Dim sRankIDBottom As String = IIf(ddlRankE.SelectedValue <> "", ddlRankE.SelectedValue, "")
        Dim sTitleIDTop As String = IIf(ddlTitleB.SelectedValue <> "", ddlTitleB.SelectedValue, "")
        Dim sTitleIDBottom As String = IIf(ddlTitleE.SelectedValue <> "", ddlTitleE.SelectedValue, "")
        Dim sPositionID As String = IIf(ddlPositionID.SelectedValue <> "", ddlPositionID.SelectedValue, "")
        Dim sWorkTypeID As String = IIf(ddlWorkTypeID.SelectedValue <> "", ddlWorkTypeID.SelectedValue, "")
        Dim sEmpFlowRemark As String = IIf(ddlEmpFlowRemark.SelectedValue <> "", ddlEmpFlowRemark.SelectedValue, "")
        Dim sBusinessType As String = IIf(ddlBusinessType.SelectedValue <> "", ddlBusinessType.SelectedValue, "")
        Dim schkInValidFlag As String = IIf(chkInValidFlag.Checked, "是", "否")

        Dim showMsg As String = ""
        Dim message As String = ""
        Dim addOrEditCount As Long = 0

        isSuccess = OV9Business.ComparEmpFlowSNData(sCompID, sEmpID, sOrganID, sDeptID, sSystemID, sFlowCode, sFlowSN, sRankIDTop, sRankIDBottom, _
                                     sTitleIDTop, sTitleIDBottom, sPositionID, sWorkTypeID, sEmpFlowRemark, sBusinessType, _
                                     addOrEditCount, showMsg, message)

        'isSuccess = True
        If isSuccess And String.IsNullOrEmpty(showMsg) Then

            Dim returnCount As Long = 0
            isSuccess = OV9Business.AddOrEditEmpFlowSNData(returnCount, message)
            '回壓筆數
            'Page.ClientScript.RegisterStartupScript(Page.GetType, "Startup", "<script language ='javascript'>alert(" & returnCount & ") " & "</script>")

        ElseIf isSuccess And showMsg <> "" Then

            Dim TipMsg As New StringBuilder
            'ViewState.Item("message") = message
            TipMsg.AppendLine("以下人員是否更新主要流程?") '& sCompID & ","
            TipMsg.AppendLine("名單 :" & showMsg)
            msg.Text = TipMsg.ToString
            Bsp.Utility.RunClientScript(Me.Page, "BossCheck();")

            'Dim result As Integer = MsgBox(TipMsg.ToString, MsgBoxStyle.OkCancel, "提示訊息")
            'If result = 1 Then '是
            '    Dim returnCount As Long = 0
            '    isSuccess = OV9Business.AddOrEditEmpFlowSNData(returnCount, message)
            'ElseIf result <> 1 Then
            '    GoBack()
            'End If
        ElseIf message <> "" Then
            'Dim result As Integer = MsgBox(message.ToString, MsgBoxStyle.OkOnly, "錯誤訊息")
            Page.ClientScript.RegisterStartupScript(Page.GetType, "Startup", "<script language ='javascript'>alert(" & message & ") " &
                                      "</script>")
        End If

        GoBack()
    End Sub
    Protected Sub btnBoss_Click(sender As Object, e As System.EventArgs) Handles btnBoss.Click
        'If ViewState("Param") <> "" Then
        Dim isSuccess As Boolean
        Dim message As String = ViewState.Item("message")
        Dim returnCount As Long = 0
        isSuccess = OV9Business.AddOrEditEmpFlowSNData(returnCount, message)
        'End If
        GoBack()
    End Sub

    'Protected Sub DoClear()
    '    ddlFlowCode.SelectedValue = ViewState.Item("FlowCode").ToString()
    '    ddlFlowSN.SelectedValue = ViewState.Item("FlowSN").ToString()
    '    ddlFlowCompID.SelectedValue = ViewState.Item("CompID").ToString().Substring(0, 6)
    '    txtEmpID.Text = ViewState.Item("EmpID").ToString().Substring(0, 6)
    '    lblEmpID.Text = ""
    '    ddlOrganID.SelectedValue = ViewState.Item("OrganID").ToString().Substring(0, 6)
    '    ddlBusinessType.SelectedValue = ViewState.Item("BusinessType").ToString().Substring(0, 2)
    '    'ddlRankID.SelectedValue = ViewState.Item("RankID").ToString()
    '    ddlPositionID.SelectedValue = ViewState.Item("PositionID").ToString().Substring(0, 6)
    '    ddlWorkTypeID.SelectedValue = ViewState.Item("WorkTypeID").ToString().Substring(0, 6)
    'End Sub

    Protected Sub txtEmpID_TextChanged(sender As Object, e As System.EventArgs) Handles txtEmpID.TextChanged
        If txtEmpID.Text <> "" And txtEmpID.Text.Length = 6 Then
            Dim objHR As New HR
            Dim rtnTable As DataTable = objHR.GetHREmpName(UserProfile.SelectCompRoleID, txtEmpID.Text)
            If rtnTable.Rows.Count <= 0 Then
                lblEmpID.Text = ""
                txtEmpID.Text = ""
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", "人事資料尚未建檔")
            Else
                lblEmpID.Text = rtnTable.Rows(0).Item(0)
            End If
        Else
            lblEmpID.Text = ""
        End If


    End Sub
    Private Sub GetEmpName()
        lblEmpID.Text = GetEmpName(UserProfile.ActCompID, txtEmpID.Text)
    End Sub

#Region "下拉選單-DisplayType"
    Enum DisplayType
        OnlyName    '只顯示名字
        OnlyID      '顯示ID  
        Full        '顯示ID + 名字
    End Enum
    Public Shared Sub FillDDLATWF10(ByVal objDDL As DropDownList, ByVal DBName As String, ByVal strTabName As String, ByVal strValue As String, ByVal strText As String, ByVal str3rdOrder As String, Optional ByVal Type As DisplayType = DisplayType.Full, Optional ByVal JoinStr As String = "", Optional ByVal WhereStr As String = "", Optional ByVal OrderByStr As String = "")
        Try
            Using dt As DataTable = GetDDLInfoATWF10(DBName, strTabName, strValue, strText, str3rdOrder, JoinStr, WhereStr, OrderByStr)
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
    Public Shared Function GetDDLInfoATWF10(ByVal DBName As String, ByVal strTabName As String, ByVal strValue As String, ByVal strText As String, ByVal str3rdOrder As String, Optional ByVal JoinStr As String = "", Optional ByVal WhereStr As String = "", Optional ByVal OrderByStr As String = "") As DataTable
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
    Public Shared Function GetEmpName(ByVal CompID As String, ByVal EmpID As String) As String
        Dim strSQL As String
        strSQL = "Select NameN From Personal"
        strSQL += " Where CompID =  " & Bsp.Utility.Quote(CompID)
        strSQL += " And EmpID =  " & Bsp.Utility.Quote(EmpID)

        Return Bsp.DB.ExecuteScalar(strSQL, "eHRMSDB")
    End Function

    Public Shared Function Query(ByVal DBName As String, ByVal strValue As String, ByVal strTab As String) As DataTable
        Dim strSQL As New StringBuilder
        strSQL.AppendLine("Select distinct " & strValue & "")
        strSQL.AppendLine(" from " & strTab & "")
        strSQL.AppendLine(" order by " & strValue & " asc")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "" + DBName + "").Tables(0)
    End Function

    '職等
    Protected Sub ddlRank_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRankB.SelectedIndexChanged, ddlRankE.SelectedIndexChanged
        Dim compID() As String = lblCompID.Text.Split("-")
        Dim strCompID As String = compID(0)
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

    Protected Sub ddlBusinessType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlBusinessType.SelectedIndexChanged
        ddlBusinessType_Changed()
    End Sub

    Public Sub ddlBusinessType_Changed()
        If ddlBusinessType.SelectedValue = "" Then
            ddlEmpFlowRemark.Items.Clear()
            ddlEmpFlowRemark.Items.Insert(0, New ListItem("---請先選擇業務類別---", ""))
        Else
            Bsp.Utility.FillDDL(ddlEmpFlowRemark, "eHRMSDB", "HRCodeMap", "Code", "CodeCName", Bsp.Utility.DisplayType.Full, "", " and TabName ='EmpFlowRemark' and FldName= " & Bsp.Utility.Quote(ddlBusinessType.SelectedValue))
            ddlEmpFlowRemark.Items.Insert(0, New ListItem("---請選擇---", ""))
        End If
    End Sub

#Region "適用部門代碼、適用單位代碼"
    Protected Sub ddlDeptID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDeptID.SelectedIndexChanged
        deptIDchang()
    End Sub
    Public Sub deptIDchang()
        ddlOrganID.Items.Clear()
        FillDDL(ddlOrganID, "eHRMSDB", "Organization", "RTrim(OrganID)", "OrganName", DisplayType.Full, "", "AND CompID='" + UserProfile.SelectCompRoleID + "' and OrganID <> DeptID AND InValidFlag='0' AND VirtualFlag='0' and DeptID= " & Bsp.Utility.Quote(ddlDeptID.SelectedValue), "Order By InValidFlag, OrganID")
        ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
    End Sub
#End Region

    Public Function getProcess(ByVal compID As String, ByVal systemID As String, ByVal flowCode As String, ByVal flowSN As String, ByVal chkmainFlag As Boolean, ParamArray strArg() As String) As DataTable
        Dim result As New DataTable
        Dim strSQL_Main As New StringBuilder
        strSQL_Main.AppendLine("Select * FROM HRFlowEmpDefine")
        strSQL_Main.AppendLine("Where 1=1 ")
        If chkmainFlag Then
            strSQL_Main.AppendLine(" And PrincipalFlag = '1' ")
            strSQL_Main.AppendLine(" And FlowSN <> " & Bsp.Utility.Quote(flowSN))
        ElseIf strArg.Length > 0 Then
            strSQL_Main.AppendLine(" And FlowCompID = " & Bsp.Utility.Quote(strArg(0)))
            strSQL_Main.AppendLine(" And EmpID = " & Bsp.Utility.Quote(strArg(1)))
            strSQL_Main.AppendLine(" And DeptID = " & Bsp.Utility.Quote(strArg(2)))
            strSQL_Main.AppendLine(" And OrganID = " & Bsp.Utility.Quote(strArg(3)))
            strSQL_Main.AppendLine(" And RankIDTop = " & Bsp.Utility.Quote(strArg(4)))
            strSQL_Main.AppendLine(" And RankIDBottom = " & Bsp.Utility.Quote(strArg(5)))
            strSQL_Main.AppendLine(" And TitleIDTop = " & Bsp.Utility.Quote(strArg(6)))
            strSQL_Main.AppendLine(" And TitleIDBottom = " & Bsp.Utility.Quote(strArg(7)))
            strSQL_Main.AppendLine(" And PositionID = " & Bsp.Utility.Quote(strArg(8)))
            strSQL_Main.AppendLine(" And WorkTypeID = " & Bsp.Utility.Quote(strArg(9)))
            strSQL_Main.AppendLine(" And BusinessType = " & Bsp.Utility.Quote(strArg(10)))
            strSQL_Main.AppendLine(" And EmpFlowRemark = " & Bsp.Utility.Quote(strArg(11)))
        End If
        strSQL_Main.AppendLine(" And CompID = " & Bsp.Utility.Quote(compID))
        strSQL_Main.AppendLine(" And SystemID = " & Bsp.Utility.Quote(systemID))
        strSQL_Main.AppendLine(" And FlowCode = " & Bsp.Utility.Quote(flowCode))
        result = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL_Main.ToString(), "AattendantDB").Tables(0)
        Return result
    End Function
    Protected Sub beforeCheck()
        Dim isFix As Boolean = False
        Dim compID As String = lblCompID.Text.Substring(0, 6)
        Dim systemID As String = ddlSystemCode.SelectedValue
        Dim flowCode As String = ddlFlowCode.SelectedValue
        Dim flowSN As String = ddlFlowSN.SelectedValue
        '----------------------是用流程----------------------
        Dim sameProcess As DataTable
        Dim isNotSameProcess As Boolean = False
        Dim flowCompID As String = compID                      'str(0)
        Dim empID As String = txtEmpID.Text                    'str(1)
        Dim deptID As String = ddlDeptID.SelectedValue         'str(2)
        Dim organID As String = ddlOrganID.SelectedValue       'str(3)
        Dim rankTop As String = ddlRankB.SelectedValue         'str(4)
        Dim rankBut As String = ddlRankE.SelectedValue         'str(5)
        Dim titleTop As String = ddlTitleB.SelectedValue       'str(6)
        Dim titleBut As String = ddlTitleE.SelectedValue       'str(7)
        Dim positionID As String = ddlPositionID.SelectedValue 'str(8)
        Dim workType As String = ddlWorkTypeID.SelectedValue   'str(9)
        Dim busType As String = ddlBusinessType.SelectedValue  'str(10)
        Dim remark As String = ddlEmpFlowRemark.SelectedValue  'str(11)
        '-----------------------------------------------------
        Dim mainFlag As Boolean = chkInValidFlag.Checked
        Dim mainData As DataTable
        Dim TipMsg As New StringBuilder

        isFix = isFixData(flowCompID, empID, deptID, organID, rankTop, rankBut, titleTop, titleBut, positionID, workType, busType, remark)
        If isFix Then
            sameProcess = getProcess(compID, systemID, flowCode, flowSN, False, flowCompID, empID, deptID, organID, rankTop, rankBut, titleTop, titleBut, positionID, workType, busType, remark)
            If sameProcess.Rows.Count > 0 Then
                TipMsg.AppendLine("欲新增的流程與以下流程相同 :")
                For i As Integer = 0 To sameProcess.Rows.Count - 1 Step 1
                    Dim sameCompID As String = sameProcess.Rows(i).Item("CompID").ToString
                    Dim sameSystemID As String = sameProcess.Rows(i).Item("SystemID").ToString
                    Dim sameFlowCode As String = sameProcess.Rows(i).Item("FlowCode").ToString
                    Dim sameFlowSN As String = sameProcess.Rows(i).Item("FlowSN").ToString
                    TipMsg.AppendLine("公司代碼 : " & sameCompID)
                    TipMsg.AppendLine("系統代碼 : " & sameSystemID)
                    TipMsg.AppendLine("流程代碼 : " & sameFlowCode)
                    TipMsg.AppendLine("流程識別碼 : " & sameFlowSN)
                Next
                Bsp.Utility.ShowMessage(Me, TipMsg.ToString)
                Return
            Else
                isNotSameProcess = True
            End If
        Else
            isNotSameProcess = True
        End If

        If mainFlag <> IIf(ViewState.Item("PrincipalFlag").Equals("1"), True, False) And isNotSameProcess Then
            mainData = getProcess(compID, systemID, flowCode, flowSN, True)
            If mainData.Rows.Count > 0 Then
                ViewState.Item("mainData") = mainData
                TipMsg.AppendLine("流程代碼 : " & flowCode & ",已有設定主要流程,")
                TipMsg.AppendLine("請確定是否更換主要流程?")
                checkMsg.Text = TipMsg.ToString
                Bsp.Utility.RunClientScript(Me.Page, "mainFlagCheck();")
            Else
                TipMsg.AppendLine("此為唯一主要流程，不可移除，如欲調整主要流程，請至其他流程修改設定")
                Bsp.Utility.ShowMessage(Me, TipMsg.ToString)
                Return
            End If
        Else
            DoUpdate(False)
        End If

    End Sub
    Public Function isFixData(ParamArray strArg() As String) As Boolean
        Dim result As Boolean = False
        Dim RankID As String = ViewState.Item("RankID").ToString.Trim
        Dim RanlIDB As String = ""
        Dim RanlIDE As String = ""
        If RankID.Length > 3 Then
            RanlIDB = RankID.Substring(0, 2)
            RanlIDE = RankID.Substring(3)
        End If

        If ViewState.Item("CompID") <> strArg(0) Then
            result = True
        End If
        If ViewState.Item("EmpID") <> strArg(1) Then
            result = True
        End If
        If ViewState.Item("DeptID") <> strArg(2) Then
            result = True
        End If
        If ViewState.Item("OrganID") <> strArg(3) Then
            result = True
        End If
        If RanlIDB <> strArg(4) Then
            result = True
        End If
        If RanlIDE <> strArg(5) Then
            result = True
        End If
        If ViewState.Item("TitleIDB") <> strArg(6) Then
            result = True
        End If
        If ViewState.Item("TitleIDE") <> strArg(7) Then
            result = True
        End If
        If ViewState.Item("PositionID") <> strArg(8) Then
            result = True
        End If
        If ViewState.Item("WorkTypeID") <> strArg(9) Then
            result = True
        End If
        If ViewState.Item("BusinessType") <> strArg(10) Then
            result = True
        End If
        If ViewState.Item("EmpFlowRemark") <> strArg(10) Then
            result = True
        End If
        Return result
    End Function
    Protected Sub btnMessage_Click(sender As Object, e As System.EventArgs) Handles btnMessage.Click
        DoUpdate(True)
    End Sub
End Class
