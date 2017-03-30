'****************************************************
'功能說明：組織數據分析
'建立人員：BeatriceCheng
'建立日期：2016.11.15
'****************************************************
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data

Public Class OM9

#Region "查詢使用者按鈕權限"
    Public Function funCheckRight() As String()
        Dim strSQL As New StringBuilder()
        Dim arrFunRight As New List(Of String)
        Dim CheckRight As Object = Bsp.DB.ExecuteScalar("Select CheckRight From SC_Fun Where FunID = " & Bsp.Utility.Quote(UserProfile.SelectFunID))

        If Not String.IsNullOrEmpty(CheckRight) Then
            If CheckRight.ToString = "0" Then
                strSQL.AppendLine("Select F.RightID From SC_FunRight F")
                If UserProfile.SelectCompRoleID = "ALL" Then
                    strSQL.AppendLine("Join SC_Right R On F.RightID = R.RightID")
                    strSQL.AppendLine("Where R.IsCompAll = '1'")
                End If
            Else
                strSQL.AppendLine("Select Distinct F.RightID From SC_UserGroup U")
                strSQL.AppendLine("Join SC_GroupFun G On U.GroupID = G.GroupID And U.SysID = G.SysID And U.CompRoleID = G.CompRoleID")
                strSQL.AppendLine("Join SC_FunRight F On G.FunID = F.FunID And G.RightID = F.RightID And G.SysID = F.SysID")
                strSQL.AppendLine("Join SC_Right R On F.RightID = R.RightID")
                strSQL.AppendLine("Where U.UserID = " & Bsp.Utility.Quote(UserProfile.UserID))
                strSQL.AppendLine("And G.FunID = " & Bsp.Utility.Quote(UserProfile.SelectFunID))
                strSQL.AppendLine("And U.SysID = " & Bsp.Utility.Quote(UserProfile.LoginSysID))
                strSQL.AppendLine("And U.CompRoleID = " & Bsp.Utility.Quote(UserProfile.SelectCompRoleID))
                If UserProfile.SelectCompRoleID = "ALL" Then
                    strSQL.AppendLine("And R.IsCompAll = '1'")
                End If
            End If

            Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        arrFunRight.Add(dr.Item(0).ToString)
                    Next
                End If
            End Using
        End If

        Return arrFunRight.ToArray
    End Function
#End Region

#Region "查詢使用者薪資權限"
    Public Function funCheckSalary() As Boolean
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select Count(*) From SC_GroupMutiQry G")
        strSQL.AppendLine("Join SC_UserGroup U On G.SysID = U.SysID And G.CompRoleID = U.CompRoleID And G.GroupID = U.GroupID")
        strSQL.AppendLine("Where G.QueryID = '1'")
        strSQL.AppendLine("And G.SysID = " & Bsp.Utility.Quote(UserProfile.LoginSysID))
        strSQL.AppendLine("And G.CompRoleID = " & Bsp.Utility.Quote(UserProfile.SelectCompRoleID))
        strSQL.AppendLine("And U.CompID = " & Bsp.Utility.Quote(UserProfile.CompID))
        strSQL.AppendLine("And U.UserID = " & Bsp.Utility.Quote(UserProfile.UserID))

        Return IIf(Bsp.DB.ExecuteScalar(strSQL.ToString) > 0, True, False)
    End Function
#End Region

#Region "可查詢薪資最低人數"
    Public Function SalaryLimitNumber() As Integer
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine("Select CodeCName From HRCodeMap")
        strSQL.AppendLine("Where TabName = 'OrganizationParameter' And FldName = 'AvgSalary' And Code = 'LimitNumber'")

        If Bsp.DB.ExecuteScalar(strSQL.ToString, "eHRMSDB") Is Nothing Then
            Return 0
        Else
            Return Integer.Parse(Bsp.DB.ExecuteScalar(strSQL.ToString, "eHRMSDB"))
        End If
    End Function
#End Region

#Region "部門選單"
    Public Function GetOrgMenu(ByVal CompID As String, ByVal QryDate As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select OrgType, DeptID, OrganID")
        strSQL.AppendLine(", OrganID + '-' + OrganName As OrganName")
        strSQL.AppendLine(", SortNode = Case When DeptID <> OrganID Then '3' When OrgType = DeptID Then '1' When OrganID = DeptID Then '2' End")
        strSQL.AppendLine(", IsNull(W.Color, '#FFFFFF') Color")
        strSQL.AppendLine(", O.InValidFlag, O.SortOrder, Right(O.GroupType, 1), O.GroupID")
        strSQL.AppendLine("From Organization O")
        strSQL.AppendLine("Left Join OrganColor_Web W On O.CompID = W.CompID and O.SortOrder = W.SortOrder")
        strSQL.AppendLine("Where O.CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine("And VirtualFlag = '0'")
        strSQL.AppendLine("And InValidFlag = '0'")

        If QryDate > Now.Date Then
            strSQL.AppendLine("UNION")
            strSQL.AppendLine("Select OrgType, DeptID, OrganID")
            strSQL.AppendLine(", OrganID + '-' + OrganName + '(未生效)' As OrganName")
            strSQL.AppendLine(", SortNode = Case When DeptID <> OrganID Then '3' When OrgType = DeptID Then '1' When OrganID = DeptID Then '2' End")
            strSQL.AppendLine(", IsNull(W.Color, '#FFFFFF') Color")
            strSQL.AppendLine(", O.InValidFlag, O.SortOrder, Right(O.GroupType, 1), O.GroupID")
            strSQL.AppendLine("From OrganizationWait O")
            strSQL.AppendLine("Left Join OrganColor_Web W On O.CompID = W.CompID and O.SortOrder = W.SortOrder")
            strSQL.AppendLine("Where O.CompID = " & Bsp.Utility.Quote(CompID))
            strSQL.AppendLine("And VirtualFlag = '0'")
            strSQL.AppendLine("And InValidFlag = '0'")
            strSQL.AppendLine("And OrganReason = '1'")
            strSQL.AppendLine("And WaitStatus = '0'")
            strSQL.AppendLine("And OrganType IN ('1', '3')")
            strSQL.AppendLine("And ValidDate <= " & Bsp.Utility.Quote(QryDate))
        End If
        strSQL.AppendLine("Order By O.InValidFlag, O.SortOrder, Right(O.GroupType, 1), O.GroupID, O.OrgType, O.OrganID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "部門資料"
    Public Function GetOrganData(ByVal CompID As String, ByVal QryDate As String, ByVal strWhere As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select Case When O.UpOrganID = O.OrganID Then NULL Else O.UpOrganID End UpOrganID")
        strSQL.AppendLine(", O.OrganID")
        strSQL.AppendLine(", O.OrganName")
        strSQL.AppendLine(", O.RoleCode")
        strSQL.AppendLine(", W.Color")
        strSQL.AppendLine(", O.Boss")
        strSQL.AppendLine(", P.NameN")
        strSQL.AppendLine(", T.TitleName")
        strSQL.AppendLine(", IsNull(U.OrgCnt, 0) OrgCnt")
        If QryDate = Now.Date Then '現在 Personal
            strSQL.AppendLine(", EmpCnt = (Select Count(*) From Personal Where CompID = O.CompID And DeptID = O.DeptID And OrganID = O.OrganID And WorkStatus = '1' And EmpType = '1')")
        ElseIf QryDate < Now.Date Then '過去 Staff_History_All
            strSQL.AppendLine(", EmpCnt = (Select Count(*) From Staff_History_All Where CompID = O.CompID And DeptID = O.DeptID And OrganID = O.OrganID And WorkStatus = '1' And LogDate = " & Bsp.Utility.Quote(QryDate) & ")")
        ElseIf QryDate > Now.Date Then '未來 Personal + EmployeeWait + Recruit
            strSQL.AppendLine(", EmpCnt = (Select Count(*) From (")
            strSQL.AppendLine("Select P.CompID, P.EmpID")
            strSQL.AppendLine("From Personal P")
            strSQL.AppendLine("Where P.CompID = O.CompID And P.OrganID = O.OrganID And P.WorkStatus = '1' And P.EmpType = '1'")
            strSQL.AppendLine("And P.EmpID NOT IN (")
            strSQL.AppendLine("Select TOP 1 EmpID From EmployeeWait ")
            strSQL.AppendLine("Where CompID = P.CompID And EmpID = P.EmpID And ValidDate <= " & Bsp.Utility.Quote(QryDate) & " And ValidMark = '0' And OrganID <> P.OrganID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("UNION")
            strSQL.AppendLine("Select CompID, EmpID")
            strSQL.AppendLine("From EmployeeWait")
            strSQL.AppendLine("Where NewCompID = O.CompID And OrganID = O.OrganID And ValidDate <= " & Bsp.Utility.Quote(QryDate) & " And ValidMark = '0'")
            strSQL.AppendLine("UNION")
            strSQL.AppendLine("Select C.CompID, R.RecID As EmpID")
            strSQL.AppendLine("From " + Bsp.Utility.getAppSetting("RecruitDB") + "..RE_Recruit R")
            strSQL.AppendLine("Join " + Bsp.Utility.getAppSetting("RecruitDB") + "..RE_ContractData C On C.RecID = R.RecID")
            strSQL.AppendLine("Join " + Bsp.Utility.getAppSetting("RecruitDB") + "..RE_CheckInData D On C.CompID = D.CompID And C.RecID = D.RecID And C.CheckInDate = D.CheckInDate")
            strSQL.AppendLine("Where C.CompID = O.CompID And D.OrganID = O.OrganID")
            strSQL.AppendLine("And Convert(VarChar, C.ContractDate, 111) >= " & Bsp.Utility.Quote(Now.Date))
            strSQL.AppendLine("And Convert(VarChar, C.ContractDate, 111) <= " & Bsp.Utility.Quote(QryDate))
            strSQL.AppendLine("And C.FinalFlag = '' And C.CheckInFlag = ''")
            strSQL.AppendLine("And Convert(VarChar, C.EmpDate, 111) = '1900/01/01'")
            strSQL.AppendLine(") E)")
        End If
        strSQL.AppendLine("From Organization O")
        strSQL.AppendLine("Left Join (")
        strSQL.AppendLine("	Select CompID, UpOrganID, COUNT(UpOrganID) OrgCnt From (")
        strSQL.AppendLine(" Select CompID, OrganID, UpOrganID From Organization")
        strSQL.AppendLine("	Where InValidFlag = '0' And VirtualFlag = '0'")
        strSQL.AppendLine(" And CompID = " & Bsp.Utility.Quote(CompID))
        If QryDate > Now.Date Then
            strSQL.AppendLine("UNION")
            strSQL.AppendLine(" Select CompID, OrganID, UpOrganID From OrganizationWait")
            strSQL.AppendLine("	Where InValidFlag = '0' And  VirtualFlag = '0'")
            strSQL.AppendLine("	And OrganReason = '1' And WaitStatus = '0' And OrganType IN ('1', '3')")
            strSQL.AppendLine("	And ValidDate <= " & Bsp.Utility.Quote(QryDate))
            strSQL.AppendLine(" And CompID = " & Bsp.Utility.Quote(CompID))
        End If
        strSQL.AppendLine("	) O Where UpOrganID <> OrganID")
        strSQL.AppendLine("	Group By CompID, UpOrganID")
        strSQL.AppendLine(") U On U.CompID = O.CompID And U.UpOrganID = O.OrganID")
        strSQL.AppendLine("Left Join OrganColor_Web W On O.CompID = W.CompID and O.SortOrder = W.SortOrder")
        strSQL.AppendLine("Left Join Personal P On O.BossCompID = P.CompID And O.Boss = P.EmpID")
        strSQL.AppendLine("Left Join Title T On T.CompID = P.CompID And T.RankID = P.RankID And T.TitleID = P.TitleID")
        strSQL.AppendLine("Where O.InValidFlag = '0' And O.VirtualFlag = '0'")
        strSQL.AppendLine("And O.CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine(strWhere)
        strSQL.AppendLine("Order By O.RoleCode, O.UpOrganID, O.OrganID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "職位下拉選單"
    Public Function GetPosition(ByVal CompID As String, ByVal OrganID As String, ByVal QryDate As String) As DataTable
        Dim strSQL As New StringBuilder()

        If QryDate = Now.Date Then '現在 Personal
            strSQL.AppendLine("Select Distinct EP.PositionID, EP.PositionID + '-' + PO.Remark As PositionName")
            strSQL.AppendLine("From Personal P")
            strSQL.AppendLine("Join EmpPosition EP On EP.CompID = P.CompID And EP.EmpID = P.EmpID And EP.PrincipalFlag = '1'")
            strSQL.AppendLine("Left Join Position PO On PO.CompID = EP.CompID And PO.PositionID = EP.PositionID")
            strSQL.AppendLine("Where P.CompID = " & Bsp.Utility.Quote(CompID))
            strSQL.AppendLine("And P.OrganID In (Select OrganID From funGetUnderOrgan(" & Bsp.Utility.Quote(CompID) & ", " & Bsp.Utility.Quote(OrganID) & ", ''))")
            strSQL.AppendLine("And P.WorkStatus = '1' And P.EmpType = '1'")
            strSQL.AppendLine("Order By EP.PositionID")
        ElseIf QryDate < Now.Date Then '過去 Staff_History_All
            strSQL.AppendLine("Select Distinct PositionID, PositionID + '-' + Position As PositionName")
            strSQL.AppendLine("From Staff_History_All")
            strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(CompID))
            strSQL.AppendLine("And OrganID In (Select OrganID From funGetUnderOrgan(" & Bsp.Utility.Quote(CompID) & ", " & Bsp.Utility.Quote(OrganID) & ", ''))")
            strSQL.AppendLine("And WorkStatus = '1'")
            strSQL.AppendLine("Order By PositionID")
        ElseIf QryDate > Now.Date Then '未來 Personal + EmployeeWait + Recruit
            strSQL.AppendLine("Select Distinct EP.PositionID, EP.PositionID + '-' + PO.Remark As PositionName")
            strSQL.AppendLine("From Personal P")
            strSQL.AppendLine("Join EmpPosition EP On EP.CompID = P.CompID And EP.EmpID = P.EmpID And EP.PrincipalFlag = '1'")
            strSQL.AppendLine("Left Join Position PO On PO.CompID = EP.CompID And PO.PositionID = EP.PositionID")
            strSQL.AppendLine("Where P.CompID = " & Bsp.Utility.Quote(CompID))
            strSQL.AppendLine("And P.OrganID In (Select OrganID From funGetUnderOrgan(" & Bsp.Utility.Quote(CompID) & ", " & Bsp.Utility.Quote(OrganID) & ", ''))")
            strSQL.AppendLine("And P.WorkStatus = '1' And P.EmpType = '1'")
            strSQL.AppendLine("And P.EmpID Not In (")
            strSQL.AppendLine("Select TOP 1 EmpID From EmployeeWait")
            strSQL.AppendLine("Where CompID = P.CompID And EmpID = P.EmpID And ValidDate <= " & Bsp.Utility.Quote(QryDate) & " And ValidMark = '0' And OrganID <> P.OrganID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("UNION")
            strSQL.AppendLine("Select Right(Left(E.PositionID, 8), 6) As PositionID, Right(Left(E.PositionID, 8), 6) + '-' + PO.Remark As PositionName")
            strSQL.AppendLine("From EmployeeWait E")
            strSQL.AppendLine("Left Join Position PO On PO.CompID = E.CompID And PO.PositionID = Right(Left(E.PositionID, 8), 6)")
            strSQL.AppendLine("Where E.NewCompID = " & Bsp.Utility.Quote(CompID))
            strSQL.AppendLine("And E.OrganID In (Select OrganID From funGetUnderOrgan(" & Bsp.Utility.Quote(CompID) & ", " & Bsp.Utility.Quote(OrganID) & ", ''))")
            strSQL.AppendLine("And E.ValidDate <= " & Bsp.Utility.Quote(QryDate))
            strSQL.AppendLine("And E.ValidMark = '0'")
            strSQL.AppendLine("UNION")
            strSQL.AppendLine("Select P.PositionID, P.PositionID + '-' + PO.Remark As PositionName")
            strSQL.AppendLine("From " + Bsp.Utility.getAppSetting("RecruitDB") + "..RE_ContractData C")
            strSQL.AppendLine("Join " + Bsp.Utility.getAppSetting("RecruitDB") + "..RE_CheckInData D On C.CompID = D.CompID And C.RecID = D.RecID And C.CheckInDate = D.CheckInDate")
            strSQL.AppendLine("Left Join " + Bsp.Utility.getAppSetting("RecruitDB") + "..RE_EmpPosition P On P.CompID = D.CompID And P.RecID = D.RecID And P.CheckInDate = D.CheckInDate And P.PrincipalFlag = '1' And P.REType = 'F'")
            strSQL.AppendLine("Left Join Position PO On PO.CompID = P.CompID And PO.PositionID = P.PositionID")
            strSQL.AppendLine("Where C.CompID = " & Bsp.Utility.Quote(CompID))
            strSQL.AppendLine("And D.OrganID In (Select OrganID From funGetUnderOrgan(" & Bsp.Utility.Quote(CompID) & ", " & Bsp.Utility.Quote(OrganID) & ", ''))")
            strSQL.AppendLine("And Convert(VarChar, C.ContractDate, 111) >= " & Bsp.Utility.Quote(Now.Date))
            strSQL.AppendLine("And Convert(VarChar, C.ContractDate, 111) <= " & Bsp.Utility.Quote(QryDate))
            strSQL.AppendLine("And C.FinalFlag = '' And C.CheckInFlag = ''")
            strSQL.AppendLine("And Convert(VarChar, C.EmpDate, 111) = '1900/01/01'")
            strSQL.AppendLine("Order By PositionID")
        End If

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "工作性質下拉選單"
    Public Function GetWorkType(ByVal CompID As String, ByVal OrganID As String, ByVal QryDate As String) As DataTable
        Dim strSQL As New StringBuilder()
        If QryDate = Now.Date Then '現在 Personal
            strSQL.AppendLine("Select Distinct EW.WorkTypeID, EW.WorkTypeID + '-' + W.Remark As WorkTypeName")
            strSQL.AppendLine("From Personal P")
            strSQL.AppendLine("Join EmpWorkType EW On EW.CompID = P.CompID And EW.EmpID = P.EmpID And EW.PrincipalFlag = '1'")
            strSQL.AppendLine("Left Join WorkType W On W.CompID = EW.CompID And W.WorkTypeID = EW.WorkTypeID")
            strSQL.AppendLine("Where P.CompID = " & Bsp.Utility.Quote(CompID))
            strSQL.AppendLine("And P.OrganID In (Select OrganID From funGetUnderOrgan(" & Bsp.Utility.Quote(CompID) & ", " & Bsp.Utility.Quote(OrganID) & ", ''))")
            strSQL.AppendLine("And P.WorkStatus = '1' And P.EmpType = '1'")
            strSQL.AppendLine("Order By EW.WorkTypeID")
        ElseIf QryDate < Now.Date Then '過去 Staff_History_All
            strSQL.AppendLine("Select Distinct WorkTypeID, WorkType As WorkTypeName")
            strSQL.AppendLine("From Staff_History_All")
            strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(CompID))
            strSQL.AppendLine("And OrganID In (Select OrganID From funGetUnderOrgan(" & Bsp.Utility.Quote(CompID) & ", " & Bsp.Utility.Quote(OrganID) & ", ''))")
            strSQL.AppendLine("And WorkStatus = '1'")
            strSQL.AppendLine("Order By WorkTypeID")
        ElseIf QryDate > Now.Date Then '未來 Personal + EmployeeWait + Recruit
            strSQL.AppendLine("Select Distinct EW.WorkTypeID, EW.WorkTypeID + '-' + W.Remark As WorkTypeName")
            strSQL.AppendLine("From Personal P")
            strSQL.AppendLine("Join EmpWorkType EW On EW.CompID = P.CompID And EW.EmpID = P.EmpID And EW.PrincipalFlag = '1'")
            strSQL.AppendLine("Left Join WorkType W On W.CompID = EW.CompID And W.WorkTypeID = EW.WorkTypeID")
            strSQL.AppendLine("Where P.CompID = " & Bsp.Utility.Quote(CompID))
            strSQL.AppendLine("And P.OrganID In (Select OrganID From funGetUnderOrgan(" & Bsp.Utility.Quote(CompID) & ", " & Bsp.Utility.Quote(OrganID) & ", ''))")
            strSQL.AppendLine("And P.WorkStatus = '1' And P.EmpType = '1'")
            strSQL.AppendLine("And P.EmpID Not In (")
            strSQL.AppendLine("Select TOP 1 EmpID From EmployeeWait ")
            strSQL.AppendLine("Where CompID = P.CompID And EmpID = P.EmpID And ValidDate <= " & Bsp.Utility.Quote(QryDate) & " And ValidMark = '0' And OrganID <> P.OrganID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("UNION")
            strSQL.AppendLine("Select Right(Left(E.WorkTypeID, 8), 6) As WorkTypeID, Right(Left(E.WorkTypeID, 8), 6) + '-' + WT.Remark As WorkTypeName")
            strSQL.AppendLine("From EmployeeWait E")
            strSQL.AppendLine("Left Join WorkType WT On WT.CompID = E.CompID And WT.WorkTypeID = Right(Left(E.WorkTypeID, 8), 6)")
            strSQL.AppendLine("Where E.NewCompID = " & Bsp.Utility.Quote(CompID))
            strSQL.AppendLine("And E.OrganID In (Select OrganID From funGetUnderOrgan(" & Bsp.Utility.Quote(CompID) & ", " & Bsp.Utility.Quote(OrganID) & ", ''))")
            strSQL.AppendLine("And E.ValidDate <= " & Bsp.Utility.Quote(QryDate))
            strSQL.AppendLine("And E.ValidMark = '0'")
            strSQL.AppendLine("UNION")
            strSQL.AppendLine("Select W.WorkTypeID, W.WorkTypeID + '-' + WT.Remark As WorkTypeName")
            strSQL.AppendLine("From " + Bsp.Utility.getAppSetting("RecruitDB") + "..RE_ContractData C")
            strSQL.AppendLine("Join " + Bsp.Utility.getAppSetting("RecruitDB") + "..RE_CheckInData D On C.CompID = D.CompID And C.RecID = D.RecID And C.CheckInDate = D.CheckInDate")
            strSQL.AppendLine("Left Join " + Bsp.Utility.getAppSetting("RecruitDB") + "..RE_EmpWorkType W On W.CompID = D.CompID And W.RecID = D.RecID And W.CheckInDate = D.CheckInDate And W.PrincipalFlag = '1' And W.REType = '1'")
            strSQL.AppendLine("Left Join WorkType WT On WT.CompID = W.CompID And WT.WorkTypeID = W.WorkTypeID")
            strSQL.AppendLine("Where C.CompID = " & Bsp.Utility.Quote(CompID))
            strSQL.AppendLine("And D.OrganID In (Select OrganID From funGetUnderOrgan(" & Bsp.Utility.Quote(CompID) & ", " & Bsp.Utility.Quote(OrganID) & ", ''))")
            strSQL.AppendLine("And Convert(VarChar, C.ContractDate, 111) >= " & Bsp.Utility.Quote(Now.Date))
            strSQL.AppendLine("And Convert(VarChar, C.ContractDate, 111) <= " & Bsp.Utility.Quote(QryDate))
            strSQL.AppendLine("And C.FinalFlag = '' And C.CheckInFlag = ''")
            strSQL.AppendLine("And Convert(VarChar, C.EmpDate, 111) = '1900/01/01'")
            strSQL.AppendLine("Order By WorkTypeID")
        End If

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PayrollDetail薪資"
    Public Function GetPayrollDetail(ByVal CompID As String, ByVal EmpID As String, ByVal QryDate As String) As String
        Dim strSQL As New StringBuilder()
        Dim ASalary As Integer = 0
        Dim BSalary As Integer = 0

        strSQL.Clear()
        strSQL.AppendLine("SELECT Amount FROM PayrollDetail")
        strSQL.AppendLine("WHERE SalaryID = 'A010'")
        strSQL.AppendLine("AND CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine("AND EmpID = " & Bsp.Utility.Quote(EmpID))
        strSQL.AppendLine("AND PayDate = LEFT(REPLACE(" & Bsp.Utility.Quote(QryDate) & ", '/', ''), 6)")
        strSQL.AppendLine("ORDER BY PayDateSeq DESC")

        If Bsp.DB.ExecuteScalar(strSQL.ToString, "HRDB_Org") Is Nothing Then
            ASalary = 0
        Else
            Integer.TryParse(HRDBDecrypt(Bsp.DB.ExecuteScalar(strSQL.ToString, "HRDB_Org")), ASalary)
        End If

        strSQL.Clear()
        strSQL.AppendLine("SELECT Amount FROM PayrollDetail")
        strSQL.AppendLine("WHERE SalaryID = 'B000'")
        strSQL.AppendLine("AND CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine("AND EmpID = " & Bsp.Utility.Quote(EmpID))
        strSQL.AppendLine("AND PayDate = LEFT(REPLACE(" & Bsp.Utility.Quote(QryDate) & ", '/', ''), 6)")
        strSQL.AppendLine("ORDER BY PayDateSeq DESC")

        If Bsp.DB.ExecuteScalar(strSQL.ToString, "HRDB_Org") Is Nothing Then
            BSalary = 0
        Else
            Integer.TryParse(HRDBDecrypt(Bsp.DB.ExecuteScalar(strSQL.ToString, "HRDB_Org")), BSalary)
        End If

        Return (ASalary + BSalary).ToString
    End Function
#End Region

#Region "數字解密"
    Public Function HRDBDecrypt(ByVal Amount As Byte()) As String
        Dim db As Database = DatabaseFactory.CreateDatabase("HRDB_Org")
        Dim dbCommand As DbCommand = db.GetStoredProcCommand("SP_HRDBDecrypt")

        db.AddInParameter(dbCommand, "@strData", DbType.Binary, Amount)
        db.AddOutParameter(dbCommand, "@strResult", DbType.String, 10)
        db.ExecuteNonQuery(dbCommand)

        Return db.GetParameterValue(dbCommand, "@strResult")
    End Function

    Public Function RecruitDecrypt(ByVal Amount As Byte()) As String
        Dim db As Database = DatabaseFactory.CreateDatabase("Recruit")
        Dim dbCommand As DbCommand = db.GetStoredProcCommand("SP_RecruitDecrypt")

        db.AddInParameter(dbCommand, "@strData", DbType.Binary, Amount)
        db.AddOutParameter(dbCommand, "@strResult", DbType.String, 10)
        db.ExecuteNonQuery(dbCommand)

        Return db.GetParameterValue(dbCommand, "@strResult")
    End Function
#End Region

End Class
