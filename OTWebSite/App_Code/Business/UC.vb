'****************************************************
'功能說明：UserControl呼叫的Function
'建立人員：Chung
'建立日期：2011.05.13
'****************************************************
Imports Microsoft.VisualBasic
Imports System.Data

Public Class UC
#Region "ucButtonPermission"
    Public Function BP_GetFun(ByVal FunID As String, ByVal SysID As String) As DataTable   '20140808 wei modify
        Dim strSQL As New StringBuilder

        strSQL.AppendLine("Select FunID, FunName, CheckRight From SC_Fun")
        strSQL.AppendLine("Where 1 = 1 ")
        If FunID <> "" Then
            strSQL.AppendLine("And FunID = " & Bsp.Utility.Quote(FunID))
        End If
        If SysID <> "" Then
            strSQL.AppendLine("And SysID = " & Bsp.Utility.Quote(SysID))
        End If
        strSQL.AppendLine("Order by FunID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    Public Function GetFunRight(ByVal FunID As String, ByVal SysID As String) As DataTable   '20140808 wei modify
        Dim strSQL As New StringBuilder

        strSQL.AppendLine("Select distinct c.RightID, c.IsVisible, c.Caption, c.CaptionEng, d.RightName, d.RightEngName, d.OrderSeq")
        strSQL.AppendLine("From SC_FunRight c")
        strSQL.AppendLine("	inner join SC_Right d on c.RightID = d.RightID")
        strSQL.AppendLine("Where c.FunID = " & Bsp.Utility.Quote(FunID))
        strSQL.AppendLine("And c.SysID = " & Bsp.Utility.Quote(SysID))
        strSQL.AppendLine("Order by d.OrderSeq")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    Public Function GetGroupFunRightbyUserID(ByVal UserID As String, ByVal FunID As String, ByVal SysID As String, ByVal CompRoleID As String) As DataTable   '20140808 wei modify
        Dim strSQL As New StringBuilder

        strSQL.AppendLine("Select distinct c.RightID, c.IsVisible, c.Caption, c.CaptionEng, d.RightName, d.RightEngName, d.OrderSeq")
        strSQL.AppendLine("From SC_UserGroup a")
        strSQL.AppendLine("	inner join SC_GroupFun b on a.GroupID = b.GroupID and a.SysID=b.SysID and a.CompRoleID=b.CompRoleID")
        strSQL.AppendLine("	inner join SC_FunRight c on b.FunID = c.FunID And b.RightID = c.RightID and b.SysID=c.SysID")
        strSQL.AppendLine("	inner join SC_Right d on c.RightID = d.RightID")
        strSQL.AppendLine("Where a.UserID = " & Bsp.Utility.Quote(UserID))
        strSQL.AppendLine("and b.FunID = " & Bsp.Utility.Quote(FunID))
        strSQL.AppendLine("and a.SysID = " & Bsp.Utility.Quote(SysID))
        strSQL.AppendLine("and a.CompRoleID = " & Bsp.Utility.Quote(CompRoleID))
        If CompRoleID = "ALL" Then
            strSQL.AppendLine("and d.IsCompAll = '1'")
        End If
        strSQL.AppendLine("Order by d.OrderSeq")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function
#End Region

#Region "ucTitle"
    Public Function T_GetFunName(ByVal FunID As String, ByVal SysID As String) As String
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select FunName From SC_Fun Where FunID = " & Bsp.Utility.Quote(FunID) & " and SysID = " & Bsp.Utility.Quote(SysID))

        Return Bsp.Utility.IsStringNull(Bsp.DB.ExecuteScalar(strSQL.ToString()))
    End Function
#End Region

#Region "ucButtonQuerySelectUserID"
    'Company
    Public Shared Sub UC_Company(ByVal objDDL As DropDownList, ByVal CompID As String, ByVal bolCompRole As Boolean, ByVal InValidFalg As String)
        Dim objUC As New UC

        Try
            Using dt As DataTable = objUC.GetUC_CompanyInfo(CompID, bolCompRole, InValidFalg)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "CompName"
                    .DataValueField = "CompID"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function GetUC_CompanyInfo(ByVal CompID As String, ByVal bolCompRole As Boolean, ByVal InValidFlag As String) As DataTable
        Dim strSQL As New StringBuilder()

        'strSQL.AppendLine("Select Distinct C.CompID,Case When C.InValidFlag='1' Then C.CompID + '-' + C.CompName + '(無效)' Else C.CompID + '-' + C.CompName  End CompName,C.InValidFlag")
        'strSQL.AppendLine("From SC_Company C with (nolock)")
        'strSQL.AppendLine("Left join SC_UserGroup U with (nolock) on U.CompRoleID = C.CompID")
        'strSQL.AppendLine("Where 1 = 1")
        'If CompID <> "" Then strSQL.AppendLine("And C.CompID = " & Bsp.Utility.Quote(CompID))
        'If bolCompRole Then
        '    strSQL.AppendLine("And U.UserID = " & Bsp.Utility.Quote(UserProfile.ActUserID))
        '    strSQL.AppendLine("And U.SysID = " & Bsp.Utility.Quote(UserProfile.LoginSysID))
        'End If
        'If InValidFlag = "Y" Then
        '    strSQL.AppendLine("And C.InValidFlag = '1'")
        'End If
        'strSQL.AppendLine("Order By C.InValidFlag,C.CompID")

        strSQL.AppendLine("Select CompID")
        strSQL.AppendLine(", Case When InValidFlag = '1' Then CompID + '-' + CompName + '(無效)' Else CompID + '-' + CompName End CompName")
        strSQL.AppendLine("From Company")
        strSQL.AppendLine("Where 1 = 1")
        If CompID <> "" Then strSQL.AppendLine("And CompID = " & Bsp.Utility.Quote(CompID))
        If InValidFlag = "N" Then strSQL.AppendLine("And InValidFlag = '0'")
        strSQL.AppendLine("Order By InValidFlag, CompID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
    'Dpet
    Public Shared Sub UC_DeptID(ByVal objDDL As DropDownList, ByVal CompID As String, ByVal InValidFlag As String)
        Dim objUC As New UC

        Try
            Using dt As DataTable = objUC.GetUC_DeptIDInfo(CompID, InValidFlag)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "OrganName"
                    .DataValueField = "DeptID"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function GetUC_DeptIDInfo(ByVal CompID As String, ByVal InValidFlag As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select DeptID")
        strSQL.AppendLine(", Case When InValidFlag = '1' Then DeptID + '-' + OrganName + '(無效)' Else DeptID + '-' + OrganName End OrganName")
        strSQL.AppendLine("From Organization") 'SC_Organization
        strSQL.AppendLine("Where DeptID = OrganID And CompID = " & Bsp.Utility.Quote(CompID))
        If InValidFlag = "N" Then
            strSQL.AppendLine("And InValidFlag = '0'")
        End If
        strSQL.AppendLine("Order by InValidFlag, DeptID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
    'User
    Public Shared Sub UC_User(ByVal objDDL As DropDownList, ByVal CompID As String, ByVal DeptID As String, ByVal InValidFlag As String)
        Dim objUC As New UC

        Try
            Using dt As DataTable = objUC.GetUC_UserInfo(CompID, DeptID, InValidFlag)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "Name"
                    .DataValueField = "EmpID"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function GetUC_UserInfo(ByVal CompID As String, ByVal DeptID As String, ByVal InValidFlag As String) As DataTable
        Dim strSQL As New StringBuilder()

        'strSQL.AppendLine("Select UserID")
        'strSQL.AppendLine(", Case When WorkStatus <> '1' Then UserID + '-' + UserName + '(非在職)' Else UserID + '-' + UserName End UserName from SC_User")

        strSQL.AppendLine("Select EmpID")
        strSQL.AppendLine(", Case When WorkStatus <> '1' Then EmpID + '-' + NameN + '(非在職)' Else EmpID + '-' + NameN End Name")
        strSQL.AppendLine("From Personal")
        strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(CompID) & " And DeptID = " & Bsp.Utility.Quote(DeptID))
        If InValidFlag = "N" Then
            strSQL.AppendLine("And WorkStatus = '1'")
        End If
        strSQL.AppendLine("Order by WorkStatus, EmpID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
    Public Function GetUC_UserByQuery(ByVal CompID As String, ByVal QueryEmp As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine("SELECT P.CompID")
        strSQL.AppendLine(", P.EmpID ")
        strSQL.AppendLine(", P.DeptID ")
        strSQL.AppendLine("FROM Personal P ")
        strSQL.AppendLine("WHERE P.WorkStatus='1' ")
        'strSQL.AppendLine("AND P.EmpType='1' ")
        If CompID <> "" Then
            strSQL.AppendLine("AND P.CompID=" & Bsp.Utility.Quote(CompID))
        End If
        strSQL.AppendLine("AND (")
        strSQL.AppendLine("P.EmpID LIKE '%" & QueryEmp & "%'")
        strSQL.AppendLine("OR P.Name LIKE '%" & QueryEmp & "%'")
        strSQL.AppendLine("OR P.NameN LIKE N'%" & QueryEmp & "%'")
        strSQL.AppendLine("OR Upper(P.EngName) LIKE Upper('%" & QueryEmp & "%')")
        strSQL.AppendLine(")")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

#End Region

#Region "ucButtonQuerySelectUserID For SC"
    'Company
    Public Shared Sub SC_Company(ByVal objDDL As DropDownList, ByVal CompID As String, ByVal bolCompRole As Boolean, ByVal InValidFalg As String)
        Dim objUC As New UC
        Try
            Using dt As DataTable = objUC.GetSC_CompanyInfo(CompID, bolCompRole, InValidFalg)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "CompName"
                    .DataValueField = "CompID"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetSC_CompanyInfo(ByVal CompID As String, ByVal bolCompRole As Boolean, ByVal InValidFlag As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("SELECT DISTINCT C.CompID")
        strSQL.AppendLine(", CASE WHEN C.InValidFlag = '1' THEN C.CompID + '-' + C.CompName + '(無效)' ELSE C.CompID + '-' + C.CompName END CompName")
        strSQL.AppendLine(", C.InValidFlag")
        strSQL.AppendLine("FROM SC_Company C WITH (NOLOCK)")
        strSQL.AppendLine("LEFT JOIN SC_UserGroup U WITH (NOLOCK) ON U.CompRoleID = C.CompID")
        strSQL.AppendLine("WHERE 1 = 1")
        If CompID <> "" Then strSQL.AppendLine("AND C.CompID = " & Bsp.Utility.Quote(CompID))
        If bolCompRole Then
            strSQL.AppendLine("AND U.UserID = " & Bsp.Utility.Quote(UserProfile.ActUserID))
            strSQL.AppendLine("AND U.SysID = " & Bsp.Utility.Quote(UserProfile.LoginSysID))
        End If
        If InValidFlag = "N" Then
            strSQL.AppendLine("AND C.InValidFlag = '0'")
        End If
        strSQL.AppendLine("ORDER BY C.InValidFlag, C.CompID")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    'Dpet
    Public Shared Sub SC_DeptID(ByVal objDDL As DropDownList, ByVal CompID As String, ByVal InValidFlag As String)
        Dim objUC As New UC
        Try
            Using dt As DataTable = objUC.GetSC_DeptIDInfo(CompID, InValidFlag)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "OrganName"
                    .DataValueField = "DeptID"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetSC_DeptIDInfo(ByVal CompID As String, ByVal InValidFlag As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("SELECT DeptID ")
        strSQL.AppendLine(", CASE WHEN InValidFlag = '1' THEN DeptID + '-' + OrganName + '(無效)' ELSE DeptID + '-' + OrganName END OrganName")
        strSQL.AppendLine("FROM SC_Organization")
        strSQL.AppendLine("WHERE DeptID = OrganID")
        strSQL.AppendLine("AND CompID = " & Bsp.Utility.Quote(CompID))
        If InValidFlag = "N" Then
            strSQL.AppendLine("AND InValidFlag = '0'")
        End If
        strSQL.AppendLine("ORDER BY InValidFlag, DeptID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    'User
    Public Shared Sub SC_User(ByVal objDDL As DropDownList, ByVal CompID As String, ByVal DeptID As String, ByVal InValidFlag As String)
        Dim objUC As New UC
        Try
            Using dt As DataTable = objUC.GetSC_UserInfo(CompID, DeptID, InValidFlag)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "UserName"
                    .DataValueField = "UserID"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetSC_UserInfo(ByVal CompID As String, ByVal DeptID As String, ByVal InValidFlag As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("SELECT UserID")
        strSQL.AppendLine(", CASE WHEN WorkStatus <> '1' THEN UserID + '-' + UserName + '(非在職)' ELSE UserID + '-' + UserName END UserName")
        strSQL.AppendLine("FROM SC_User")
        strSQL.AppendLine("WHERE CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine("AND DeptID = " & Bsp.Utility.Quote(DeptID))
        If InValidFlag = "N" Then
            strSQL.AppendLine("AND WorkStatus = '1'")
        End If
        strSQL.AppendLine("ORDER BY WorkStatus, UserID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    Public Function GetSC_UserByQuery(ByVal CompID As String, ByVal QueryEmp As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("SELECT CompID")
        strSQL.AppendLine(", DeptID ")
        strSQL.AppendLine(", UserID ")
        strSQL.AppendLine("FROM SC_User")
        strSQL.AppendLine("WHERE WorkStatus = '1'")
        If CompID <> "" Then
            strSQL.AppendLine("AND CompID = " & Bsp.Utility.Quote(CompID))
        End If
        strSQL.AppendLine("AND (")
        strSQL.AppendLine("UserID LIKE '%" & QueryEmp & "%'")
        strSQL.AppendLine("OR UserName LIKE '%" & QueryEmp & "%'")
        strSQL.AppendLine("OR Upper(EngName) LIKE Upper('%" & QueryEmp & "%')")
        strSQL.AppendLine(")")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

#End Region

#Region "PageWorkType"
    Public Function GetWorkTypeByUC(ByVal WorkTypeID As String, Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        'Public Function GetWorkTypeU(ByVal WorkTypeID As String, ByVal CompID As String, Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "", Optional ByVal CondStr2 As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames = "" Then FieldNames = "*, WorkTypeID + '-' + Remark as FullName"

        strSQL.AppendLine("Select " & FieldNames)
        strSQL.AppendLine("From WorkType ")
        strSQL.AppendLine("Where 1=1 ")
        If WorkTypeID <> "" Then strSQL.AppendLine("and WorkTypeID = " & Bsp.Utility.Quote(WorkTypeID))
        'If CompID <> "" Then strSQL.AppendLine("and CompID = " & Bsp.Utility.Quote(CompID))
        If CondStr <> "" Then strSQL.AppendLine(CondStr)
        strSQL.AppendLine("Order by WorkTypeID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
    Public Function GetCWorkTypeByUC(ByVal WorkTypeID As String, Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        'Public Function GetWorkTypeU(ByVal WorkTypeID As String, ByVal CompID As String, Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "", Optional ByVal CondStr2 As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames = "" Then FieldNames = "Code as WorkTypeID,Code + '-' + CodeName as FullName"

        strSQL.AppendLine("Select " & FieldNames)
        strSQL.AppendLine("From CHRCodeMap ")
        strSQL.AppendLine("Where 1=1 And TabName='EmpWorkType' and FldName='WorkTypeID' ")
        If WorkTypeID <> "" Then strSQL.AppendLine("and Code = " & Bsp.Utility.Quote(WorkTypeID))
        'If CompID <> "" Then strSQL.AppendLine("and CompID = " & Bsp.Utility.Quote(CompID))
        If CondStr <> "" Then strSQL.AppendLine(CondStr)
        strSQL.AppendLine("Order by Code")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
    
#End Region
#Region "PagePosition"
    Public Function GetPositionByUC(ByVal PositionID As String, Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames = "" Then FieldNames = "*, PositionID + '-' + Remark as FullName"

        strSQL.AppendLine("Select " & FieldNames)
        strSQL.AppendLine("From Position ")
        strSQL.AppendLine("Where 1=1 ")
        If PositionID <> "" Then strSQL.AppendLine("and PositionID = " & Bsp.Utility.Quote(PositionID))
        If CondStr <> "" Then strSQL.AppendLine(CondStr)
        strSQL.AppendLine("Order by PositionID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region
#Region "ucSelectHROrgan"
    Public Function GetHROrganInfo(ByVal TableName As String, ByVal OrganID As String, Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder

        If FieldNames = "" Then FieldNames = "*"


        strSQL.AppendLine("Select " & FieldNames)
        strSQL.AppendLine("From " & TableName)
        strSQL.AppendLine("Where 1=1")
        If OrganID <> "" Then strSQL.AppendLine("And OrganID = " & Bsp.Utility.Quote(OrganID))
        If CondStr <> "" Then strSQL.AppendLine(CondStr)

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "ucRelease"
    Public Function GetReleaseUserInfo(ByVal UserID As String, Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames = "" Then FieldNames = "*"


        If UserID = "" Then
            strSQL.AppendLine("Select " & FieldNames)
            strSQL.AppendLine("From SC_User U")
            strSQL.AppendLine("inner join SC_UserGroup G On U.CompID=G.CompID and U.UserID=G.UserID")
            strSQL.AppendLine("inner join SC_GroupFun F On G.SysID=F.SysID and G.CompRoleID=F.CompRoleID and G.GroupID=F.GroupID")
            strSQL.AppendLine("Where F.RightID='R'")
            If CondStr <> "" Then strSQL.AppendLine(CondStr)
        Else
            strSQL.AppendLine("Select " & FieldNames)
            strSQL.AppendLine("From SC_User U")
            strSQL.AppendLine("inner join SC_UserGroup G On U.CompID=G.CompID and U.UserID=G.UserID")
            strSQL.AppendLine("inner join SC_GroupFun F On G.SysID=F.SysID and G.CompRoleID=F.CompRoleID and G.GroupID=F.GroupID")
            strSQL.AppendLine("Where F.RightID='R'")
            strSQL.AppendLine("And U.UserID = " & Bsp.Utility.Quote(UserID))
            If CondStr <> "" Then strSQL.AppendLine(CondStr)
        End If

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

#End Region

#Region "ucSelectEmp"
    '2015/07/31 Add By Micky
    Public Function GetEmpName(ByVal txtEmpID As String) As DataTable
        Dim strSQL As New StringBuilder
        strSQL.AppendLine(" Select NameN from Personal ")
        strSQL.AppendLine(" Where 1 = 1 AND EmpID = " & Bsp.Utility.Quote(txtEmpID))
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "ucSelectRecruit-下傳"
    Public Function QueryRE_ContractDataByDownload(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()
        Dim strFieldNames As New StringBuilder()
        Dim aryStr() As String

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine("And C.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "RecID"
                        strSQL.AppendLine("And R.RecID LIKE '" & ht(strKey).ToString() + "%' ")
                    Case "NameN"
                        strSQL.AppendLine("And R.NameN LIKE N'" & ht(strKey).ToString() + "%' ")
                End Select
            End If
        Next
        strSQL.AppendLine(" AND Convert(char(10), C.ContractDate, 111) <> '1900/01/01' ")
        strSQL.AppendLine(" AND C.FinalFlag = '' ")
        strSQL.AppendLine(" AND C.CheckInFlag = '' ")
        strSQL.AppendLine(" AND Convert(char(10), C.EmpDate, 111) = '1900/01/01' ")
        strSQL.AppendLine(" AND E.REType = '1' and E.PrincipalFlag = '1' ")

        strFieldNames.AppendLine(" R.RecID as '應試編號',")
        strFieldNames.AppendLine(" isnull(R.NameN, '') as '姓名',")
        strFieldNames.AppendLine(" isnull(R.IDNo,'') as '身分證字號',")
        strFieldNames.AppendLine(" Convert(char(10), C.ContractDate, 111) as '預計報到日',")
        strFieldNames.AppendLine(" isnull(C1.DeptID + '-' + O1.OrganName, '') as '用人單位部門',")
        strFieldNames.AppendLine(" isnull(C1.OrganID + '-' + O2.OrganName, '') as '科組課',")
        strFieldNames.AppendLine(" isnull((Select Code + ' ' + CodeName From RE_CodeMap Where Code = C1.RecIdentityID and TabName = 'RE_CheckInData' and FldName = 'RecIdentityID'), '') as '身分別',")
        strFieldNames.AppendLine(" isnull(C1.RecIdentityRemark,'') as '身分別-註明說明',")
        strFieldNames.AppendLine(" E.PositionID + ISNULL(RP.Remark, '') as '職位',")
        strFieldNames.AppendLine(" Case When Convert(Char(10), R.BirthDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, R.BirthDate, 111) END as '生日' ")

        Return GetRE_ContractDataByDownload(strFieldNames.ToString(), strSQL.ToString())
    End Function
    Public Function GetRE_ContractDataByDownload(Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames = "" Then FieldNames = "*"

        strSQL.AppendLine(" Select " & FieldNames)
        strSQL.AppendLine(" From RE_ContractData C ")
        strSQL.AppendLine(" Inner Join RE_CheckInData C1 On C.CompID = C1.CompID and C.RecID = C1.RecID and C.CheckInDate = C1.CheckInDate ")
        strSQL.AppendLine(" Inner Join RE_Recruit R On C.RecID = R.RecID ")
        strSQL.AppendLine(" Inner Join RE_EmpPosition E On C.CompID = E.CompID and C.RecID = E.RecID and C.CheckInDate = E.CheckInDate")
        strSQL.AppendLine(" Left Join SC_Organization O1 On C1.CompID = O1.CompID and C1.DeptID = O1.OrganID ")
        strSQL.AppendLine(" Left Join SC_Organization O2 On C1.CompID = O2.CompID and C1.OrganID = O2.OrganID ")
        strSQL.AppendLine(" Left Join RE_Position RP on RP.CompID = C1.CompID and RP.PositionID = E.PositionID ")

        strSQL.AppendLine(" Where 1 = 1")
        If CondStr <> "" Then strSQL.AppendLine(CondStr)
        strSQL.AppendLine(" Order By C.ContractDate, R.RecID ")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "Recruit").Tables(0)
    End Function
#End Region
    
End Class
