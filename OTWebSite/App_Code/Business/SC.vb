'******************************************************************************
'功能說明：安控及系統管理相關Function
'建立人員：Chung
'建立日期：2011.05.12
'******************************************************************************
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.IO
Imports System

Public Class SC

#Region "Default.aspx : 登入頁使用"
    ''' <summary>
    ''' 建立UserInfo物件
    ''' </summary>
    ''' <param name="LoginSysID"></param>
    ''' <param name="UserID"></param>
    ''' <param name="UserName"></param>
    ''' <param name="DeptID"></param>
    ''' <param name="CompID"></param>
    ''' <param name="CompName"></param>
    ''' <param name="DeptName"></param>
    ''' <param name="OrganID"></param>
    ''' <param name="OrganName"></param>
    ''' <param name="GroupID"></param>
    ''' <param name="SysID"></param>
    ''' <param name="CompRoleID"></param>
    ''' <remarks></remarks>
    Public Shared Sub CreateUserInfoSession(ByVal LoginSysID As String, ByVal UserID As String, ByVal UserName As String, ByVal CompID As String, ByVal CompName As String, ByVal DeptID As String, ByVal DeptName As String, ByVal OrganID As String, ByVal OrganName As String, ByVal GroupID As System.Collections.Generic.List(Of String), ByVal SysID As System.Collections.Generic.List(Of String), ByVal CompRoleID As System.Collections.Generic.List(Of String))

        If HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName) IsNot Nothing Then
            HttpContext.Current.Session.Remove(Bsp.MySettings.UserProfileSessionName)
        End If
        Dim myUserInfo As UserInfo = New UserInfo()

        myUserInfo.LoginSysID = LoginSysID    '20140808 wei add Login的系統別代碼
        myUserInfo.SelectCompRoleID = CompID  '20140818 wei add   選擇權限公司代碼
        myUserInfo.SysID = SysID    '20140807 wei add   系統別代碼
        myUserInfo.CompRoleID = CompRoleID  '20140807 wei add   授權公司代碼
        myUserInfo.UserID = UserID
        myUserInfo.UserName = UserName
        myUserInfo.CompID = CompID  '20140807 wei add 公司代碼
        myUserInfo.CompName = CompName    '20140808 wei add 公司
        myUserInfo.DeptID = DeptID
        myUserInfo.DeptName = DeptName
        myUserInfo.OrganID = OrganID
        myUserInfo.OrganName = OrganName
        myUserInfo.GroupID = GroupID
        myUserInfo.ActSysID = SysID    '20140807 wei add   系統別代碼
        myUserInfo.ActCompRoleID = CompRoleID  '20140807 wei add   授權公司代碼
        myUserInfo.ActUserID = UserID
        myUserInfo.ActUserName = UserName
        myUserInfo.ActCompID = CompID  '20140807 wei add 公司代碼
        myUserInfo.ActCompName = CompName    '20140808 wei add 公司
        myUserInfo.ActDeptID = DeptID
        myUserInfo.ActDeptName = DeptName
        myUserInfo.ActOrganID = OrganID
        myUserInfo.ActOrganName = OrganName

        myUserInfo.ActGroupID = GroupID

        HttpContext.Current.Session.Add(Bsp.MySettings.UserProfileSessionName, myUserInfo)
    End Sub

    ''' <summary>
    ''' 執行代理
    ''' </summary>
    ''' <param name="UserID"></param>
    ''' <param name="UserName"></param>
    ''' <param name="CompID"></param>
    ''' <param name="DeptID"></param>
    ''' <param name="DeptName"></param>
    ''' <param name="OrganID"></param>
    ''' <param name="OrganName"></param>
    ''' <param name="GroupID"></param>
    ''' <param name="SysID"></param>
    ''' <param name="CompRoleID"></param>
    ''' <remarks></remarks>
    Public Shared Sub ActAsAgent(ByVal UserID As String, ByVal UserName As String, ByVal CompID As String, ByVal DeptID As String, ByVal DeptName As String, ByVal OrganID As String, ByVal OrganName As String, ByVal GroupID As System.Collections.Generic.List(Of String), ByVal SysID As System.Collections.Generic.List(Of String), ByVal CompRoleID As System.Collections.Generic.List(Of String))
        Dim myUserInfo As UserInfo = CType(HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName), UserInfo)

        With myUserInfo
            .SysID = SysID    '20140807 wei add   系統別代碼
            .CompRoleID = CompRoleID  '20140807 wei add   授權公司代碼
            .UserID = UserID
            .UserName = UserName
            .CompID = CompID  '20140807 wei add 公司代碼
            .DeptID = DeptID
            .DeptName = DeptName
            .OrganID = OrganID
            .OrganName = OrganName
            .GroupID = GroupID
        End With

        HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName) = myUserInfo
    End Sub

    ''' <summary>
    ''' 取消代理
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub CancelAgent()
        Dim myUserInfo As UserInfo = CType(HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName), UserInfo)

        With myUserInfo
            .UserID = .ActUserID
            .UserName = .ActUserName
            .DeptID = .ActDeptID
            .DeptName = .ActDeptName
            .OrganID = .ActOrganID
            .OrganName = .ActOrganName
            .GroupID = .ActGroupID
        End With

        HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName) = myUserInfo
    End Sub

    

#End Region

#Region "SC0100：SC_User相關..."
    Public Function GetSCUser(Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" Select S.CompID, S.UserID, S.UserName, W.Remark as WorkStatus, C.CompName, S.DeptID + '-' + O2.OrganName as DeptID, S.OrganID + '-' + O1.OrganName as OrganID, T.TitleName , S.BanMark, S.WorkStatus ")
        strSQL.AppendLine(", BanMarkValidDate = Case When Convert(Char(10), S.BanMarkValidDate, 111) = '1900/01/01' Then '' Else Convert(Varchar, S.BanMarkValidDate, 111) End") '20151116 Beatrice modify
        strSQL.AppendLine(" , C1.CompName as LastChgComp, U.UserName as LastChgID, S.LastChgDate ") '20150311 Beatrice modify
        strSQL.AppendLine(" From SC_User S ")
        strSQL.AppendLine(" left join SC_Company C on C.CompID = S.CompID ")
        strSQL.AppendLine(" left join SC_Company C1 on C1.CompID = S.LastChgComp ") '20150306 Beatrice modify
        strSQL.AppendLine(" left join SC_User U on U.CompID = S.LastChgComp And U.UserID = S.LastChgID ") '20150311 Beatrice modify
        strSQL.AppendLine(" left join SC_Organization O1 on S.CompID = O1.CompID and S.DeptID = O1.DeptID and S.OrganID = O1.OrganID ")
        strSQL.AppendLine(" left join SC_Organization O2 on S.CompID = O2.CompID and S.DeptID = O2.DeptID and S.DeptID = O2.OrganID ")
        strSQL.AppendLine(" left join eHRMSDB_ITRD.dbo.WorkStatus W on S.WorkStatus = W.WorkCode ")
        strSQL.AppendLine(" left join eHRMSDB_ITRD.dbo.Title T on S.CompID = T.CompID and S.TitleID = T.TitleID and S.RankID = T.RankID ")
        strSQL.AppendLine(" Where 1 = 1 ")
        If CondStr <> "" Then strSQL.AppendLine(CondStr)

        strSQL.AppendLine(" order by S.CompID, S.WorkStatus, S.UserID ")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    Public Function GetUserInfo(ByVal UserID As String, Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames = "" Then FieldNames = "*"

        If UserID = "" Then
            strSQL.AppendLine("Select " & FieldNames)
            strSQL.AppendLine("From SC_User Where 1 = 1")
			strSQL.AppendLine("And WorkStatus = '1'")
            If CondStr <> "" Then strSQL.AppendLine(CondStr)
        Else
            strSQL.AppendLine("Select " & FieldNames)
            strSQL.AppendLine("From SC_User")
            strSQL.AppendLine("Where UserID = " & Bsp.Utility.Quote(UserID))
			strSQL.AppendLine("And WorkStatus = '1'")
            If CondStr <> "" Then strSQL.AppendLine(CondStr)
        End If

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    ''' <summary>
    ''' 取得指定部門的使用者資料
    ''' </summary>
    ''' <param name="DeptIDs">若有多個,請用逗號隔開,5中間請勿夾雜空白。如000170,000300,BR010</param>
    ''' <param name="FieldNames"></param>
    ''' <param name="CondStr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUserInfobyDeptID(ByVal DeptIDs As String, Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder

        If FieldNames = "" Then FieldNames = "*"

        strSQL.AppendLine("Select " & FieldNames)
        strSQL.AppendLine("From SC_User")
        strSQL.AppendLine("Where 1 = 1")
        If DeptIDs <> "" Then strSQL.AppendLine("And DeptID in (" & Bsp.Utility.Quote(DeptIDs).Replace(",", "','") & ")")
        If CondStr <> "" Then strSQL.AppendLine(CondStr)
        strSQL.AppendLine("Order by UserID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    Public Function AddUser(ByVal beUser As beSC_User.Row) As Boolean
        Dim bsSC_User As New beSC_User.Service()

        Try
            If bsSC_User.Insert(beUser) = 0 Then Return False
        Catch ex As Exception
            Throw
        End Try
        Return True
    End Function

    Public Function UpdateUser(ByVal beUser As beSC_User.Row) As Boolean
        Dim bsSC_User As New beSC_User.Service()

        Try
            If bsSC_User.Update(beUser) = 0 Then Return False
        Catch ex As Exception
            Throw
        End Try
        Return True
    End Function

    Public Function QueryUser(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "UserID"
                        strSQL.AppendLine("And UserID like " & Bsp.Utility.Quote(ht(strKey).ToString() & "%"))
                    Case "UserName"
                        strSQL.AppendLine("And UserName like N" & Bsp.Utility.Quote(ht(strKey).ToString() & "%"))
                    Case "DeptID"
                        strSQL.AppendLine("And DeptID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next

        Return GetUserInfo("", "", strSQL.ToString())
    End Function

    Public Function QuerySCUser(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine("And S.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "UserID"
                        strSQL.AppendLine("And S.UserID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "UserName"
                        'strSQL.AppendLine("And S.UserName like N" & Bsp.Utility.Quote(ht(strKey).ToString() & "%"))
                        strSQL.AppendLine("And S.UserName like N'%" & ht(strKey).ToString() & "%'") '20150727 Beatrice modify
                End Select
            End If
        Next

        Return GetSCUser(strSQL.ToString())
    End Function

    ''' <summary>
    ''' 刪除使用者
    ''' </summary>
    ''' <param name="beUser"></param>
    ''' <returns></returns>
    ''' <remarks>連同相關群組權限一併刪除</remarks>
    Public Function DeleteUser(ByVal beUser As beSC_User.Row) As Boolean
        Dim bsUser As New beSC_User.Service()
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Delete From SC_UserGroup Where UserID = " & Bsp.Utility.Quote(beUser.UserID.Value))

        Using cn As DbConnection = Bsp.DB.getConnection
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                bsUser.DeleteRowByPrimaryKey(beUser, tran)
                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)

                tran.Commit()
                Return True
            Catch ex As Exception
                If inTrans Then tran.Rollback()
                Throw
            Finally
                tran.Dispose()
                If cn.State = ConnectionState.Open Then cn.Close()
            End Try
        End Using
    End Function

    Public Function ResetPassword(ByVal beUser As beSC_User.Row) As Integer
        Dim strSQL As New StringBuilder

        Try
            strSQL.AppendLine("Update SC_User Set ")
            strSQL.AppendLine("  Password = 'NEWUSER', ")
            strSQL.AppendLine("  PasswordErrorCount = 0, ")
            strSQL.AppendLine("  LastChgDate = GetDate(), ")
            strSQL.AppendLine("  LastChgID = " & Bsp.Utility.Quote(beUser.LastChgID.Value))
            strSQL.AppendLine("Where UserID = " & Bsp.Utility.Quote(beUser.UserID.Value))

            Return Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString())
        Catch ex As Exception
            Throw
        End Try
    End Function
#End Region

#Region "SC0200：SC_Sys相關..."
    Public Function GetSysInfo(ByVal SysID As String, Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames = "" Then FieldNames = "S.*"

        If SysID = "" Then
            strSQL.AppendLine("Select " & FieldNames & ", C.CompName AS LastChgCompName, U.UserName AS LastChgName") '20150311 Beatrice modify
            strSQL.AppendLine("From SC_Sys S")
            strSQL.AppendLine("left join SC_Company C on C.CompID = S.LastChgComp") '20150306 Beatrice modify
            strSQL.AppendLine("left join SC_User U on U.CompID = S.LastChgComp And U.UserID = S.LastChgID ") '20150311 Beatrice modify
            strSQL.AppendLine("Where 1 = 1")
            If CondStr <> "" Then strSQL.AppendLine(CondStr)
        Else
            strSQL.AppendLine("Select " & FieldNames & ", IsNull(C.CompName, S.LastChgComp) AS LastChgCompName, IsNull(U.UserName, S.LastChgID) AS LastChgName") '20150311 Beatrice modify
            strSQL.AppendLine("From SC_Sys S")
            strSQL.AppendLine("left join SC_Company C on C.CompID = S.LastChgComp") '20150306 Beatrice modify
            strSQL.AppendLine("left join SC_User U on U.CompID = S.LastChgComp And U.UserID = S.LastChgID ") '20150311 Beatrice modify
            strSQL.AppendLine("Where SysID = " & Bsp.Utility.Quote(SysID))
            If CondStr <> "" Then strSQL.AppendLine(CondStr)
        End If


        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    Public Function AddSys(ByVal beSys As beSC_Sys.Row) As Boolean
        Dim bsSC_Sys As New beSC_Sys.Service()

        Try
            If bsSC_Sys.Insert(beSys) = 0 Then Return False
        Catch ex As Exception
            Throw
        End Try
        Return True
    End Function

    Public Function UpdateSys(ByVal beSys As beSC_Sys.Row) As Boolean
        Dim bsSC_Sys As New beSC_Sys.Service()

        Try
            If bsSC_Sys.Update(beSys) = 0 Then Return False
        Catch ex As Exception
            Throw
        End Try
        Return True
    End Function

    Public Function QuerySys(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "SysID"
                        strSQL.AppendLine("And S.SysID like " & Bsp.Utility.Quote(ht(strKey).ToString() & "%"))
                    Case "SysName"
                        strSQL.AppendLine("And S.SysName like N" & Bsp.Utility.Quote(ht(strKey).ToString() & "%"))
                End Select
            End If
        Next

        Return GetSysInfo("", "", strSQL.ToString())
    End Function
    
    ''' <summary>
    ''' 刪除系統別
    ''' </summary>
    ''' <param name="beSys"></param>
    ''' <returns></returns>
    ''' <remarks>連同相關群組權限一併刪除</remarks>
    Public Function DeleteSys(ByVal beSys As beSC_Sys.Row) As Boolean
        Dim bsSys As New beSC_Sys.Service()
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Delete From SC_Sys Where SysID = " & Bsp.Utility.Quote(beSys.SysID.Value))

        Using cn As DbConnection = Bsp.DB.getConnection
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                bsSys.DeleteRowByPrimaryKey(beSys, tran)
                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)

                tran.Commit()
                Return True
            Catch ex As Exception
                If inTrans Then tran.Rollback()
                Throw
            Finally
                tran.Dispose()
                If cn.State = ConnectionState.Open Then cn.Close()
            End Try
        End Using
    End Function
#End Region

#Region "SC0210：部門組織維護相關"
    ''' <summary>
    ''' 取得部門相關資訊
    ''' </summary>
    ''' <param name="OrganID">部門代號</param>
    ''' <param name="FieldNames">要撈取的欄位,Default為所有(*)</param>
    ''' <param name="CondStr">額外條件</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetOrganInfo(ByVal OrganID As String, Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder

        If FieldNames = "" Then FieldNames = "*"

        strSQL.AppendLine("Select " & FieldNames)
        strSQL.AppendLine("From SC_Organization")
        strSQL.AppendLine("Where 1=1")
        If OrganID <> "" Then strSQL.AppendLine("And OrganID = " & Bsp.Utility.Quote(OrganID))
        If CondStr <> "" Then strSQL.AppendLine(CondStr)

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    Public Function AddOrganization(ByVal beOrganization As beSC_Organization.Row) As Boolean
        Dim bsOrgan As New beSC_Organization.Service()
        Dim intRowsAffected As Integer = 0

        Try
            intRowsAffected = bsOrgan.Insert(beOrganization)
            If intRowsAffected = 0 Then Return False
        Catch ex As Exception
            Throw
        End Try
        Return True
    End Function

    Public Function UpdateOrganization(ByVal beOrganization As beSC_Organization.Row) As Boolean
        Dim bsOrgan As New beSC_Organization.Service()
        Dim intRowsAffected As Integer = 0

        Try
            intRowsAffected = bsOrgan.Update(beOrganization)
            If intRowsAffected = 0 Then Return False
        Catch ex As Exception
            Throw
        End Try
        Return True
    End Function

    Public Function DeleteOrganization(ByVal beOrganization As beSC_Organization.Row) As Integer
        Dim bsOrgan As New beSC_Organization.Service()

        Return bsOrgan.DeleteRowByPrimaryKey(beOrganization)
    End Function

    Public Function QueryOrganization(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "OrganID"
                        strSQL.AppendLine("And OrganID like " & Bsp.Utility.Quote(ht(strKey).ToString() & "%"))
                    Case "OrganName"
                        strSQL.AppendLine("And OrganName like N" & Bsp.Utility.Quote(ht(strKey).ToString() & "%"))
                    Case "OrganType"
                        Select Case ht(strKey).ToString()
                            Case "Dept"
                                strSQL.AppendLine("And OrganType = '1'")
                            Case "Branch"
                                strSQL.AppendLine("And BranchFlag = '1'")
                        End Select
                End Select
            End If
        Next

        Return GetOrganInfo("", "", strSQL.ToString())
    End Function
#End Region

#Region "SC0220：區域中心相關"
    ''' <summary>
    ''' 取得區域中心資訊
    ''' </summary>
    ''' <param name="RegionID"></param>
    ''' <param name="FieldNames"></param>
    ''' <param name="CondStr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRegionInfo(ByVal RegionID As String, Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder

        If FieldNames = "" Then FieldNames = "*"

        strSQL.AppendLine("Select " & FieldNames)
        strSQL.AppendLine("From SC_Region")
        strSQL.AppendLine("Where 1=1")
        If RegionID <> "" Then strSQL.AppendLine("And RegionID = " & Bsp.Utility.Quote(RegionID))
        If CondStr <> "" Then strSQL.AppendLine(CondStr)

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    Public Function AddRegion(ByVal beRegion As beSC_Region.Row) As Integer
        Dim bsRegion As New beSC_Region.Service()

        Return bsRegion.Insert(beRegion)
    End Function

    Public Function UpdateRegion(ByVal beRegion As beSC_Region.Row) As Integer
        Dim bsRegion As New beSC_Region.Service()

        Return bsRegion.Update(beRegion)
    End Function

    Public Function DeleteRegion(ByVal beRegion As beSC_Region.Row) As Integer
        Dim bsRegion As New beSC_Region.Service()

        Return bsRegion.DeleteRowByPrimaryKey(beRegion)
    End Function

    Public Function QueryRegion(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "RegionID"
                        strSQL.AppendLine("And RegionID like " & Bsp.Utility.Quote(ht(strKey).ToString() & "%"))
                    Case "RegionName"
                        strSQL.AppendLine("And RegionName like N" & Bsp.Utility.Quote(ht(strKey).ToString() & "%"))
                End Select
            End If
        Next

        Return GetRegionInfo("", "*, dbo.funGetAOrgDefine('3', Boss) BossName, dbo.funGetAOrgDefine('4', UpRegionID) UpRegionName", _
                             strSQL.ToString())
    End Function
#End Region

#Region "SC0230：群組維護相關..."
    'Public Function GetGroupInfo_0500(ByVal GroupID As String, Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
    Public Function GetGroupInfo_0500(ByVal GroupID As String, ByVal CompRoleID As String, ByVal SysID As String, Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames = "" Then FieldNames = "*"

        strSQL.AppendLine("Select " & FieldNames)
        strSQL.AppendLine("From SC_Group")
        strSQL.AppendLine("Where 1 = 1")
        If GroupID <> "0" And GroupID <> "" Then
            strSQL.AppendLine("And GroupID = " & Bsp.Utility.Quote(GroupID))
        End If
        If CompRoleID <> "" Then strSQL.AppendLine("And CompRoleID = " & Bsp.Utility.Quote(CompRoleID)) '20140903 Ann add
        If SysID <> "" Then strSQL.AppendLine("And SysID = " & Bsp.Utility.Quote(SysID)) '20140903 Ann add
        If CondStr <> "" Then strSQL.AppendLine(CondStr)

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    Public Function GetGroupInfo_0504(ByVal GroupID As String, Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames.Trim() = "" Then FieldNames = "*"

        strSQL.AppendLine("Select " & FieldNames)
        strSQL.AppendLine("From SC_Group Where 1 = 1")
        If GroupID <> "" Then strSQL.AppendLine("And GroupID = " & Bsp.Utility.Quote(GroupID))
        If CondStr <> "" Then strSQL.AppendLine(CondStr)

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    'Public Function GetGroupInfo(ByVal GroupID As String, ByVal GroupName As String, Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
    Public Function GetGroupInfo(ByVal GroupID As String, ByVal SysID As String, Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()
        Dim bsGroup As New beSC_Group.Service()
        'If FieldNames = "" Then FieldNames = "*"

        strSQL.AppendLine("Select G.SysID,C.CompName as CompRoleName,G.CompRoleID, G.GroupID, G.GroupName, ")
        strSQL.AppendLine("C1.CompName as LastChgComp, U.UserName as LastChgID ,G.LastChgDate") '20150311 Beatrice modify
        strSQL.AppendLine("from SC_Group G")
        strSQL.AppendLine("left join SC_Company C on G.CompRoleID  = C.CompID ")
        strSQL.AppendLine("left join SC_Company C1 on G.LastChgComp  = C1.CompID ")
        strSQL.AppendLine("left join SC_User U on U.CompID = G.LastChgComp And U.UserID = G.LastChgID ") '20150311 Beatrice modify
        strSQL.AppendLine("where 1=1 ")
        If SysID <> "" Then strSQL.AppendLine("And SysID = " & Bsp.Utility.Quote(SysID)) '20140903 Ann add
        'If CompRoleID <> "" Then strSQL.AppendLine("And CompRoleID = " & Bsp.Utility.Quote(CompRoleID)) '20140903 Ann add
        If GroupID <> "" Then strSQL.AppendLine("And GroupID = " & Bsp.Utility.Quote(GroupID))
        'If GroupID <> "" Then strSQL.AppendLine("And GroupID = " & Bsp.Utility.Quote(GroupID))
        'If GroupName <> "" Then strSQL.AppendLine(Bsp.Utility.Quote(GroupName))
        'If FieldNames <> "" Then strSQL.AppendLine(FieldNames)
        If CondStr <> "" Then strSQL.AppendLine(CondStr)

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    Public Function AddGroup(ByVal beGroup As beSC_Group.Row) As Boolean
        Dim bsGroup As New beSC_Group.Service()

        Using cn As DbConnection = Bsp.DB.getConnection()
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                bsGroup.Insert(beGroup, tran)

                'Dim beUserGroup As New beSC_UserGroup.Row()
                'Dim bsUserGroup As New beSC_UserGroup.Service()
                'beUserGroup.CompID.Value = UserProfile.ActCompID
                'beUserGroup.UserID.Value = UserProfile.ActUserID
                'beUserGroup.SysID.Value = beGroup.SysID.Value
                'beUserGroup.CompRoleID.Value = beGroup.CompRoleID.Value
                'beUserGroup.GroupID.Value = beGroup.GroupID.Value
                'beUserGroup.CreateDate.Value = Now
                'beUserGroup.LastChgComp.Value = beGroup.LastChgComp.Value
                'beUserGroup.LastChgID.Value = beGroup.LastChgID.Value
                'beUserGroup.LastChgDate.Value = Format(Now, "yyyy/MM/dd HH:mm:ss")

                'bsUserGroup.Insert(beUserGroup, tran)

                tran.Commit()
            Catch ex As Exception
                tran.Rollback()
                Throw
            Finally
                If tran IsNot Nothing Then tran.Dispose()
            End Try
        End Using

        Return True
    End Function

    '全金控
    'Public Function AddGroupAll(ByVal GroupID As String, ByVal GroupName As String) As DataTable
    Public Function AddGroupAll(ByVal GroupID As String, ByVal GroupName As String) As Boolean
        Dim bsGroup As New beSC_Group.Service()
        Dim beGroup As New beSC_Group.Row()
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" insert into SC_Group (SysID, CompRoleID, GroupID, GroupName, CreateDate, LastChgComp, LastChgID, LastChgDate)")
        strSQL.AppendLine(" select " & Bsp.Utility.Quote(UserProfile.LoginSysID) & ",CompID ," & Bsp.Utility.Quote(GroupID) & "," & Bsp.Utility.Quote(GroupName) & ",getdate()," & Bsp.Utility.Quote(UserProfile.ActCompID) & "," & Bsp.Utility.Quote(UserProfile.ActUserID) & ", getdate() from SC_Company where InValidFlag = '0'")
        strSQL.AppendLine(" union ")
        strSQL.AppendLine(" select " & Bsp.Utility.Quote(UserProfile.LoginSysID) & ",'ALL' ," & Bsp.Utility.Quote(GroupID) & "," & Bsp.Utility.Quote(GroupName) & ",getdate()," & Bsp.Utility.Quote(UserProfile.ActCompID) & "," & Bsp.Utility.Quote(UserProfile.ActUserID) & ", getdate() from SC_Company where InValidFlag = '0'")
        strSQL.AppendLine(" delete SC_Group where SysID = '' ")

        Using cn As DbConnection = Bsp.DB.getConnection()
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                bsGroup.Insert(beGroup, tran)
                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)
                tran.Commit()
            Catch ex As Exception
                tran.Rollback()
                Throw
            Finally
                If tran IsNot Nothing Then tran.Dispose()
            End Try
        End Using

        Return True
    End Function

    Public Function UpdateGroup(ByVal beGroup As beSC_Group.Row) As Boolean
        Dim bsGroup As New beSC_Group.Service()

        Try
            bsGroup.Update(beGroup)
        Catch ex As Exception
            Throw
        End Try

        Return True
    End Function

    ''' <summary>
    ''' 刪除群組資料
    ''' </summary>
    ''' <param name="beGroup"></param>
    ''' <returns></returns>
    ''' <remarks>需連同人員群組資料、群組功能資料一併刪除</remarks>
    Public Function DeleteGroup(ByVal beGroup As beSC_Group.Row) As Boolean
        Dim bsGroup As New beSC_Group.Service()
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Delete From SC_Group Where SysID = " & Bsp.Utility.Quote(beGroup.SysID.Value) & "and CompRoleID = " & Bsp.Utility.Quote(beGroup.CompRoleID.Value) & "and GroupID = " & Bsp.Utility.Quote(beGroup.GroupID.Value))

        Using cn As DbConnection = Bsp.DB.getConnection()
            cn.Open()
            Dim tran As Data.Common.DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                bsGroup.DeleteRowByPrimaryKey(beGroup, tran)
                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)

                tran.Commit()
                inTrans = False
            Catch ex As Exception
                If inTrans Then tran.Rollback()
                Throw
            Finally
                tran.Dispose()
                If cn.State = ConnectionState.Open Then cn.Close()
            End Try
        End Using

        Return True
    End Function

    '20150318 Beatrice Add 刪除群組資料，需連同人員群組資料、群組功能資料一併刪除
    Public Function DeleteUserGroup_0400(ByVal SysID As String, ByVal CompRoleID As String, ByVal GroupID As String) As Integer
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Delete From SC_UserGroup")
        strSQL.AppendLine("Where SysID = " & Bsp.Utility.Quote(SysID))
        strSQL.AppendLine("And CompRoleID = " & Bsp.Utility.Quote(CompRoleID))
        strSQL.AppendLine("And GroupID = " & Bsp.Utility.Quote(GroupID))

        Return Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString())
    End Function

    Public Function QueryGroup(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "SysID"
                        strSQL.AppendLine("And G.SysID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "CompRoleID"
                        strSQL.AppendLine("And G.CompRoleID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "GroupID"
                        strSQL.AppendLine("And G.GroupID like " & Bsp.Utility.Quote("%" & ht(strKey).ToString() & "%")) '20150313 Beatrice modify
                    Case "GroupName"
                        strSQL.AppendLine("And G.GroupName like N" & Bsp.Utility.Quote("%" & ht(strKey).ToString() & "%")) '20150313 Beatrice modify
                End Select
            End If
        Next

        Return GetGroupInfo("", "", "", strSQL.ToString())
    End Function

    Public Function QueryGroup1(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "SysID"
                        strSQL.AppendLine("And G.SysID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "GroupID"
                        strSQL.AppendLine("And GroupID like " & Bsp.Utility.Quote("%" & ht(strKey).ToString() & "%"))
                    Case "GroupName"
                        strSQL.AppendLine("And GroupName like N" & Bsp.Utility.Quote("%" & ht(strKey).ToString() & "%"))
                End Select
            End If
        Next

        Return GetGroupInfo("", "", "", strSQL.ToString())
    End Function
#End Region

#Region "SC0240：功能維護相關..."
    Public Function GetFunInfo_0500(ByVal FunID As String, ByVal SysID As String, Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames.Trim() = "" Then FieldNames = "*"

        strSQL.AppendLine("Select " & FieldNames)
        strSQL.AppendLine("From SC_Fun Where 1 = 1")
        If FunID <> "" Then strSQL.AppendLine("And FunID = " & Bsp.Utility.Quote(FunID))
        If SysID <> "" Then strSQL.AppendLine("And SysID = " & Bsp.Utility.Quote(SysID)) '20140903 Ann add
        If CondStr <> "" Then strSQL.AppendLine(CondStr)

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    Public Function GetFunInfo(ByVal FunID As String, Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames.Trim() = "" Then FieldNames = "S.*"

        strSQL.AppendLine("Select " & FieldNames & ", C.CompName AS LastChgCompName, U.UserName AS LastChgName") '20150312 Beatrice modify
        strSQL.AppendLine("From SC_Fun S")
        strSQL.AppendLine("left join SC_Company C on C.CompID = S.LastChgComp ") '20150312 Beatrice modify
        strSQL.AppendLine("left join SC_User U on U.CompID = S.LastChgComp And U.UserID = S.LastChgID ") '20150311 Beatrice modify
        strSQL.AppendLine("Where 1 = 1")
        If FunID <> "" Then strSQL.AppendLine("And FunID = " & Bsp.Utility.Quote(FunID))
        If CondStr <> "" Then strSQL.AppendLine(CondStr)

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function
    Public Function AddFun(ByVal beFun As beSC_Fun.Row, ByVal beFunRight() As beSC_FunRight.Row) As Boolean
        Dim bsFun As New beSC_Fun.Service()
        Dim bsFunRight As New beSC_FunRight.Service()

        '取得LevelNo
        If beFun.ParentFormID.Value = "" Then
            beFun.LevelNo.Value = 0
        Else
            Using dt As DataTable = GetFunInfo(beFun.ParentFormID.Value, "LevelNo")
                If dt.Rows.Count > 0 Then
                    beFun.LevelNo.Value = Convert.ToInt32(dt.Rows(0).Item("LevelNo")) + 1
                End If
            End Using
        End If

        Using cn As DbConnection = Bsp.DB.getConnection()
            cn.Open()
            Dim tran As Data.Common.DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                bsFun.Insert(beFun, tran)
                If beFunRight IsNot Nothing Then
                    bsFunRight.Insert(beFunRight, tran)
                End If

                tran.Commit()
                inTrans = False
            Catch ex As Exception
                If inTrans Then tran.Rollback()
                Throw
            Finally
                tran.Dispose()
                If cn.State = ConnectionState.Open Then cn.Close()
            End Try
        End Using
        Return True
    End Function

    Public Function UpdateFun(ByVal beFun As beSC_Fun.Row, ByVal beFunRight() As beSC_FunRight.Row) As Boolean
        Dim bsFun As New beSC_Fun.Service()
        Dim bsFunRight As New beSC_FunRight.Service()
        Dim strSQL As New StringBuilder()

        '取得LevelNo
        If beFun.ParentFormID.Value = "" Then
            beFun.LevelNo.Value = 0
        Else
            Using dt As DataTable = GetFunInfo(beFun.ParentFormID.Value)
                If dt.Rows.Count > 0 Then
                    beFun.LevelNo.Value = Convert.ToInt32(dt.Rows(0).Item("LevelNo")) + 1
                End If
            End Using
        End If
        strSQL.AppendLine("Delete From SC_FunRight where FunID = " & Bsp.Utility.Quote(beFun.FunID.Value))

        Using cn As DbConnection = Bsp.DB.getConnection()
            cn.Open()
            Dim tran As Data.Common.DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                bsFun.Update(beFun, tran)
                If beFunRight IsNot Nothing Then
                    Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)
                    bsFunRight.Insert(beFunRight, tran)
                Else
                    Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)
                End If

                tran.Commit()
                inTrans = False
            Catch ex As Exception
                If inTrans Then tran.Rollback()
                Throw
            Finally
                tran.Dispose()
                If cn.State = ConnectionState.Open Then cn.Close()
            End Try
        End Using

        Return True

    End Function

    Public Function DeleteFun(ByVal beFun As beSC_Fun.Row) As Boolean
        Dim bsFun As New beSC_Fun.Service()
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Delete From SC_FunRight Where FunID = " & Bsp.Utility.Quote(beFun.FunID.Value) & " and SysID = " & Bsp.Utility.Quote(beFun.SysID.Value))

        Using cn As DbConnection = Bsp.DB.getConnection()
            cn.Open()
            Dim tran As Data.Common.DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                bsFun.DeleteRowByPrimaryKey(beFun, tran)
                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)

                tran.Commit()
                inTrans = False
            Catch ex As Exception
                If inTrans Then tran.Rollback()
                Throw
            Finally
                tran.Dispose()
                If cn.State = ConnectionState.Open Then cn.Close()
            End Try
        End Using

        Return True
    End Function

    Public Function QueryFun(ByVal ParamArray Params() As String) As DataTable
        Dim strSQL As New StringBuilder()
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)

        For Each strKey As String In ht.Keys
            If strKey = "ParentFunID" Then
                If ht(strKey).ToString() <> "$$" Then
                    strSQL.AppendLine("And ParentFormID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    strSQL.AppendLine("And IsMenu = '1'")
                    If ht(strKey).ToString() = "" Then
                        strSQL.AppendLine("And S.Path = ''")
                    End If
                End If
            Else
                If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                    Select Case strKey
                        Case "FunID"
                            strSQL.AppendLine("And FunID like " & Bsp.Utility.Quote(ht(strKey).ToString() & "%"))
                        Case "FunName"
                            'strSQL.AppendLine("And FunName like N" & Bsp.Utility.Quote(ht(strKey).ToString() & "%"))
                            strSQL.AppendLine("And FunName like N'%" & ht(strKey).ToString() & "%'") '20150727 Beatrice modify
                    End Select
                End If
            End If
        Next

        Return GetFunInfo("", "", strSQL.ToString())
    End Function

    Public Function GetRightFun(ByVal FunID As String) As DataTable
        Dim strSQL As New StringBuilder

        strSQL.AppendLine("Select a.RightID, a.RightName, a.OrderSeq, IsNull(b.IsVisible, '') IsVisible, IsNull(b.Caption, '') Caption, IsNull(b.FunID, '') FunID, Isnull(b.CaptionEng, '') CaptionEng")
        strSQL.AppendLine("from SC_Right a left outer join SC_FunRight b on a.RightID = b.RightID and b.FunID = " & Bsp.Utility.Quote(FunID))
        strSQL.AppendLine("order by a.OrderSeq")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function
#End Region

#Region "SC0250:使用者群組關係"
    ''' <summary>
    ''' 撈取使用者群組關係資料
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUserGroupInfo(ByVal UserID As String, Optional ByVal GroupID As String = "", Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames = "" Then FieldNames = "*"
        strSQL.AppendLine("Select " & FieldNames)
        strSQL.AppendLine("From SC_UserGroup")
        strSQL.AppendLine("Where 1 = 1")
        If UserID <> "" Then strSQL.AppendLine("And UserID = " & Bsp.Utility.Quote(UserID))
        If GroupID <> "" Then strSQL.AppendLine("And GroupID = " & Bsp.Utility.Quote(GroupID))
        If CondStr <> "" Then strSQL.AppendLine(CondStr)

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    Public Function UpdateUserGroup(ByVal beUserGroup_Ins() As beSC_UserGroup.Row, ByVal beUserGroup_Del() As beSC_UserGroup.Row, ByVal beGroup_Del() As beSC_Group.Row) As Integer
        Dim bsUserGroup As New beSC_UserGroup.Service()
        Dim bsGroup As New beSC_Group.Service()
        Dim intAffectedRows As Integer = 0

        Using cn As DbConnection = Bsp.DB.getConnection()
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction()
            Dim inTrans As Boolean = True

            Try
                If beUserGroup_Ins IsNot Nothing Then intAffectedRows += bsUserGroup.Insert(beUserGroup_Ins, tran)
                If beUserGroup_Del IsNot Nothing Then intAffectedRows += bsUserGroup.DeleteRowByPrimaryKey(beUserGroup_Del, tran)
                If beGroup_Del IsNot Nothing Then intAffectedRows += bsGroup.DeleteRowByPrimaryKey(beGroup_Del)
                tran.Commit()
            Catch ex As Exception
                tran.Rollback()
                Throw
            Finally
                If tran IsNot Nothing Then tran.Dispose()
            End Try
        End Using
        Return intAffectedRows
    End Function

    Public Function UpdateUserGroup(ByVal GroupID As String, ByVal UserID As String, ByVal LastChgID As String) As Integer
        Dim bsUserGroup As New beSC_UserGroup.Service()
        Dim beUserGroup() As beSC_UserGroup.Row = Nothing
        Dim strSQL_Del As New StringBuilder()
        Dim aryUserID() As String = UserID.Split(",")
        Dim strSubQuery As String = ""
        Dim intAffectedRows As Integer = 0

        Using dt As DataTable = GetUserInfo("", "UserID, UserName", "And Exists (Select GroupID From SC_UserGroup Where UserID = SC_User.UserID And GroupID = " & Bsp.Utility.Quote(GroupID) & ")")
            For intLoop As Integer = 0 To aryUserID.GetUpperBound(0)
                If strSubQuery <> "" Then strSubQuery &= ","
                strSubQuery &= Bsp.Utility.Quote(aryUserID(intLoop))

                If dt.Select("UserID=" & Bsp.Utility.Quote(aryUserID(intLoop))).Length = 0 Then
                    If beUserGroup Is Nothing Then
                        ReDim beUserGroup(0)
                    Else
                        ReDim Preserve beUserGroup(beUserGroup.GetUpperBound(0) + 1)
                    End If
                    Dim beUG As New beSC_UserGroup.Row()

                    beUG.GroupID.Value = GroupID
                    beUG.UserID.Value = aryUserID(intLoop)
                    beUG.LastChgID.Value = LastChgID
                    beUG.CreateDate.Value = Now
                    beUG.LastChgDate.Value = Now
                    beUserGroup(beUserGroup.GetUpperBound(0)) = beUG
                End If
            Next
        End Using


        strSQL_Del.AppendLine("Delete From SC_UserGroup")
        strSQL_Del.AppendLine("Where GroupID = " & Bsp.Utility.Quote(GroupID))
        strSQL_Del.AppendLine("And UserID not in (" & strSubQuery & ")")

        Using cn As DbConnection = Bsp.DB.getConnection()
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction()
            Dim inTrans As Boolean = True

            Try
                If beUserGroup IsNot Nothing Then intAffectedRows += bsUserGroup.Insert(beUserGroup, tran)
                intAffectedRows += Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL_Del.ToString(), tran)

                tran.Commit()
            Catch ex As Exception
                tran.Rollback()
                Throw
            Finally
                If tran IsNot Nothing Then tran.Dispose()
            End Try
        End Using
        Return intAffectedRows
    End Function

    '功能：取得目前使用者在某一個網頁的權限
    Public Function GetFunRightbyUserID(ByVal UserID As String, ByVal FunID As String) As DataTable
        Dim strSQL As New StringBuilder

        strSQL.AppendLine("Select distinct c.RightID, c.IsVisible, c.Caption, c.CaptionEng, d.RightName, d.RightEngName, d.OrderSeq")
        strSQL.AppendLine("From SC_UserGroup a")
        strSQL.AppendLine("	inner join SC_GroupFun b on a.GroupID = b.GroupID")
        strSQL.AppendLine("	inner join SC_FunRight c on b.FunID = c.FunID And b.RightID = c.RightID")
        strSQL.AppendLine("	inner join SC_Right d on c.RightID = d.RightID")
        strSQL.AppendLine("Where a.UserID = " & Bsp.Utility.Quote(UserID))
        strSQL.AppendLine("and b.FunID = " & Bsp.Utility.Quote(FunID))
        strSQL.AppendLine("Order by d.OrderSeq")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function
#End Region

#Region "SC0500：群組功能關係維護"
    Public Function GetGroupFun(ByVal GroupFunType As Bsp.Enums.GroupFunType, ByVal GroupID As String, ByVal CompRoleID As String) As DataTable
        Dim strSQL As New StringBuilder

        If GroupFunType = Bsp.Enums.GroupFunType.Group Then
            'strSQL.AppendLine("Select a.FunID, a.FunName,")
            strSQL.AppendLine("Select distinct c.SysID, c.CompRoleID, d.CompName , a.FunID, a.FunName,c.GroupID, e.GroupName,") '20140905 Ann modify
            strSQL.AppendLine("	Case When a.RightA > 0 Then Case When b.RightA > 0 Then 'V' Else '' End Else 'X' End RightA, ")
            strSQL.AppendLine("	Case When a.RightU > 0 Then Case When b.RightU > 0 Then 'V' Else '' End Else 'X' End RightU, ")
            strSQL.AppendLine("	Case When a.RightD > 0 Then Case When b.RightD > 0 Then 'V' Else '' End Else 'X' End RightD, ")
            strSQL.AppendLine("	Case When a.RightI > 0 Then Case When b.RightI > 0 Then 'V' Else '' End Else 'X' End RightI, ")
            strSQL.AppendLine("	Case When a.RightC > 0 Then Case When b.RightC > 0 Then 'V' Else '' End Else 'X' End RightC, ")
            strSQL.AppendLine("	Case When a.RightR > 0 Then Case When b.RightR > 0 Then 'V' Else '' End Else 'X' End RightR, ")
            strSQL.AppendLine("	Case When a.RightP > 0 Then Case When b.RightP > 0 Then 'V' Else '' End Else 'X' End RightP, ")
            strSQL.AppendLine("	Case When a.RightX > 0 Then Case When b.RightX > 0 Then 'V' Else '' End Else 'X' End RightX, ")
            strSQL.AppendLine("	Case When a.RightL > 0 Then Case When b.RightL > 0 Then 'V' Else '' End Else 'X' End RightL ")
            strSQL.AppendLine("From (")
            strSQL.AppendLine("	Select a.FunID, a.FunName, ")
            strSQL.AppendLine("		Case When Sum(RightA) > 0 Then 1 Else 0 End RightA, ")
            strSQL.AppendLine("		Case When Sum(RightU) > 0 Then 1 Else 0 End RightU, ")
            strSQL.AppendLine("		Case When Sum(RightD) > 0 Then 1 Else 0 End RightD, ")
            strSQL.AppendLine("		Case When Sum(RightI) > 0 Then 1 Else 0 End RightI, ")
            strSQL.AppendLine("		Case When Sum(RightC) > 0 Then 1 Else 0 End RightC, ")
            strSQL.AppendLine("		Case When Sum(RightR) > 0 Then 1 Else 0 End RightR, ")
            strSQL.AppendLine("		Case When Sum(RightP) > 0 Then 1 Else 0 End RightP, ")
            strSQL.AppendLine("		Case When Sum(RightX) > 0 Then 1 Else 0 End RightX, ")
            strSQL.AppendLine("		Case When Sum(RightL) > 0 Then 1 Else 0 End RightL ")
            strSQL.AppendLine("	From (")
            strSQL.AppendLine("		Select a.FunID, a.FunName, ")
            strSQL.AppendLine("				Case When b.RightID = 'A' Then 1 Else 0 End RightA,")
            strSQL.AppendLine("				Case When b.RightID = 'U' Then 1 Else 0 End RightU,")
            strSQL.AppendLine("				Case When b.RightID = 'D' Then 1 Else 0 End RightD,")
            strSQL.AppendLine("				Case When b.RightID = 'I' Then 1 Else 0 End RightI,")
            strSQL.AppendLine("				Case When b.RightID = 'C' Then 1 Else 0 End RightC,")
            strSQL.AppendLine("				Case When b.RightID = 'R' Then 1 Else 0 End RightR,")
            strSQL.AppendLine("				Case When b.RightID = 'P' Then 1 Else 0 End RightP,")
            strSQL.AppendLine("				Case When b.RightID = 'X' Then 1 Else 0 End RightX,")
            strSQL.AppendLine("				Case When b.RightID = 'L' Then 1 Else 0 End RightL")
            strSQL.AppendLine("		From SC_Fun a with (nolock)")
            'strSQL.AppendLine("				inner join SC_FunRight b with (nolock) on a.FunID = b.FunID")
            strSQL.AppendLine("				inner join SC_FunRight b with (nolock) on a.FunID = b.FunID and a.SysID = b.SysID")   '20140905 Ann modify
            If GroupID <> "0" Then
                strSQL.AppendLine("		Where a.FunID in (Select FunID From SC_GroupFun with (nolock) Where GroupID = " & Bsp.Utility.Quote(GroupID) & ")) a")
            Else
                strSQL.AppendLine("		Where a.FunID in (Select FunID From SC_GroupFun with (nolock) )) a")
            End If
            strSQL.AppendLine("	Group by a.FunID, a.FunName) a")
            strSQL.AppendLine("	inner join ")
            strSQL.AppendLine("	(Select a.FunID, a.FunName, ")
            strSQL.AppendLine("		Case When Sum(RightA) > 0 Then 1 Else 0 End RightA, ")
            strSQL.AppendLine("		Case When Sum(RightU) > 0 Then 1 Else 0 End RightU, ")
            strSQL.AppendLine("		Case When Sum(RightD) > 0 Then 1 Else 0 End RightD, ")
            strSQL.AppendLine("		Case When Sum(RightI) > 0 Then 1 Else 0 End RightI, ")
            strSQL.AppendLine("		Case When Sum(RightC) > 0 Then 1 Else 0 End RightC, ")
            strSQL.AppendLine("		Case When Sum(RightR) > 0 Then 1 Else 0 End RightR, ")
            strSQL.AppendLine("		Case When Sum(RightP) > 0 Then 1 Else 0 End RightP, ")
            strSQL.AppendLine("		Case When Sum(RightX) > 0 Then 1 Else 0 End RightX, ")
            strSQL.AppendLine("		Case When Sum(RightL) > 0 Then 1 Else 0 End RightL")
            strSQL.AppendLine("	From (")
            strSQL.AppendLine("		Select a.FunID, a.FunName, ")
            strSQL.AppendLine("				Case When b.RightID = 'A' Then 1 Else 0 End RightA,")
            strSQL.AppendLine("				Case When b.RightID = 'U' Then 1 Else 0 End RightU,")
            strSQL.AppendLine("				Case When b.RightID = 'D' Then 1 Else 0 End RightD,")
            strSQL.AppendLine("				Case When b.RightID = 'I' Then 1 Else 0 End RightI,")
            strSQL.AppendLine("				Case When b.RightID = 'C' Then 1 Else 0 End RightC,")
            strSQL.AppendLine("				Case When b.RightID = 'R' Then 1 Else 0 End RightR,")
            strSQL.AppendLine("				Case When b.RightID = 'P' Then 1 Else 0 End RightP,")
            strSQL.AppendLine("				Case When b.RightID = 'X' Then 1 Else 0 End RightX,")
            strSQL.AppendLine("				Case When b.RightID = 'L' Then 1 Else 0 End RightL")
            strSQL.AppendLine("		From SC_Fun a with (nolock) ")
            'strSQL.AppendLine("				inner join SC_GroupFun b with (nolock) on a.FunID = b.FunID")
            strSQL.AppendLine("				inner join SC_GroupFun b with (nolock) on a.FunID = b.FunID and a.SysID = b.SysID")   '20140905 Ann modify
            If GroupID <> "0" Then
                strSQL.AppendLine("		Where a.FunID in (Select FunID From SC_GroupFun with (nolock) Where GroupID = " & Bsp.Utility.Quote(GroupID) & "and CompRoleID = " & Bsp.Utility.Quote(CompRoleID) & ")")
            Else
                strSQL.AppendLine("		Where a.FunID in (Select FunID From SC_GroupFun with (nolock) Where CompRoleID = " & Bsp.Utility.Quote(CompRoleID) & ")")
            End If
            If GroupID <> "0" Then
                strSQL.AppendLine("		and b.GroupID = " & Bsp.Utility.Quote(GroupID) & "and b.CompRoleID =" & Bsp.Utility.Quote(CompRoleID) & ") a")
            Else
                strSQL.AppendLine("		and b.CompRoleID =" & Bsp.Utility.Quote(CompRoleID) & ") a")
            End If
            strSQL.AppendLine("	Group by a.FunID, a.FunName) b on a.FunID = b.FunID")
            If GroupID <> "0" Then
                strSQL.AppendLine("	inner join SC_GroupFun c with (nolock) on b.FunID  = c.FunID and a.FunID = c.FunID and c.GroupID =" & Bsp.Utility.Quote(GroupID) & " and c.CompRoleID = " & Bsp.Utility.Quote(CompRoleID))    '20140905 Ann modify
            Else
                strSQL.AppendLine("	inner join SC_GroupFun c with (nolock) on b.FunID  = c.FunID and a.FunID = c.FunID and c.CompRoleID = " & Bsp.Utility.Quote(CompRoleID))    '20140905 Ann modify
            End If
            strSQL.AppendLine("	inner join SC_Company d with (nolock) on c.CompRoleID = d.CompID")  '20140905 Ann modify
            'strSQL.AppendLine("	inner join SC_Group e with (nolock) on c.GroupID = e.GroupID")  '20140905 Ann modify
            strSQL.AppendLine("	inner join SC_Group e with (nolock) on c.GroupID = e.GroupID and e.CompRoleID = " & Bsp.Utility.Quote(CompRoleID))  '20140905 Ann modify
            strSQL.AppendLine("Order by c.GroupID --a.FunID")
        Else
            'strSQL.AppendLine("Select b.GroupID, c.GroupName, a.FunID, a.FunName,")
            If GroupFunType.ToString = "Group" Then
                strSQL.AppendLine("Select c.SysID, d.CompName ,b.GroupID, c.GroupName, a.FunID, a.FunName,") '20140905 Ann modify
            Else
                strSQL.AppendLine("Select c.SysID, d.CompID, d.CompName ,b.GroupID, c.GroupName, a.FunID, a.FunName,") '20140905 Ann modify
            End If
            strSQL.AppendLine("	Case When a.RightA > 0 Then Case When b.RightA > 0 Then 'V' Else '' End Else 'X' End RightA, ")
            strSQL.AppendLine("	Case When a.RightU > 0 Then Case When b.RightU > 0 Then 'V' Else '' End Else 'X' End RightU, ")
            strSQL.AppendLine("	Case When a.RightD > 0 Then Case When b.RightD > 0 Then 'V' Else '' End Else 'X' End RightD, ")
            strSQL.AppendLine("	Case When a.RightI > 0 Then Case When b.RightI > 0 Then 'V' Else '' End Else 'X' End RightI, ")
            strSQL.AppendLine("	Case When a.RightC > 0 Then Case When b.RightC > 0 Then 'V' Else '' End Else 'X' End RightC, ")
            strSQL.AppendLine("	Case When a.RightR > 0 Then Case When b.RightR > 0 Then 'V' Else '' End Else 'X' End RightR, ")
            strSQL.AppendLine("	Case When a.RightP > 0 Then Case When b.RightP > 0 Then 'V' Else '' End Else 'X' End RightP, ")
            strSQL.AppendLine("	Case When a.RightX > 0 Then Case When b.RightX > 0 Then 'V' Else '' End Else 'X' End RightX, ")
            strSQL.AppendLine("	Case When a.RightL > 0 Then Case When b.RightL > 0 Then 'V' Else '' End Else 'X' End RightL")
            strSQL.AppendLine("From (")
            strSQL.AppendLine("	Select a.FunID, a.FunName, ")
            strSQL.AppendLine("		Case When Sum(RightA) > 0 Then 1 Else 0 End RightA, ")
            strSQL.AppendLine("		Case When Sum(RightU) > 0 Then 1 Else 0 End RightU, ")
            strSQL.AppendLine("		Case When Sum(RightD) > 0 Then 1 Else 0 End RightD, ")
            strSQL.AppendLine("		Case When Sum(RightI) > 0 Then 1 Else 0 End RightI, ")
            strSQL.AppendLine("		Case When Sum(RightC) > 0 Then 1 Else 0 End RightC, ")
            strSQL.AppendLine("		Case When Sum(RightR) > 0 Then 1 Else 0 End RightR, ")
            strSQL.AppendLine("		Case When Sum(RightP) > 0 Then 1 Else 0 End RightP, ")
            strSQL.AppendLine("		Case When Sum(RightX) > 0 Then 1 Else 0 End RightX, ")
            strSQL.AppendLine("		Case When Sum(RightL) > 0 Then 1 Else 0 End RightL ")
            strSQL.AppendLine("	From (")
            strSQL.AppendLine("		Select a.FunID, a.FunName, ")
            strSQL.AppendLine("				Case When b.RightID = 'A' Then 1 Else 0 End RightA,")
            strSQL.AppendLine("				Case When b.RightID = 'U' Then 1 Else 0 End RightU,")
            strSQL.AppendLine("				Case When b.RightID = 'D' Then 1 Else 0 End RightD,")
            strSQL.AppendLine("				Case When b.RightID = 'I' Then 1 Else 0 End RightI,")
            strSQL.AppendLine("				Case When b.RightID = 'C' Then 1 Else 0 End RightC,")
            strSQL.AppendLine("				Case When b.RightID = 'R' Then 1 Else 0 End RightR,")
            strSQL.AppendLine("				Case When b.RightID = 'P' Then 1 Else 0 End RightP,")
            strSQL.AppendLine("				Case When b.RightID = 'X' Then 1 Else 0 End RightX,")
            strSQL.AppendLine("				Case When b.RightID = 'L' Then 1 Else 0 End RightL")
            strSQL.AppendLine("		From SC_Fun a with (nolock) ")
            'strSQL.AppendLine("				inner join SC_FunRight b with (nolock) on a.FunID = b.FunID")
            strSQL.AppendLine("				inner join SC_FunRight b with (nolock) on a.FunID = b.FunID and a.SysID = b.SysID") '20140905 Ann modify
			If GroupFunType.ToString = "Group" Then
            strSQL.AppendLine("		Where a.FunID = " & Bsp.Utility.Quote(GroupID) & ") a")
			Else
                strSQL.AppendLine("	 ) a")
            End If
            strSQL.AppendLine("	Group by a.FunID, a.FunName) a")
            strSQL.AppendLine("	inner join ")
            strSQL.AppendLine("	(Select a.GroupID, a.FunID, a.FunName, ")
            strSQL.AppendLine("		Case When Sum(RightA) > 0 Then 1 Else 0 End RightA, ")
            strSQL.AppendLine("		Case When Sum(RightU) > 0 Then 1 Else 0 End RightU, ")
            strSQL.AppendLine("		Case When Sum(RightD) > 0 Then 1 Else 0 End RightD, ")
            strSQL.AppendLine("		Case When Sum(RightI) > 0 Then 1 Else 0 End RightI, ")
            strSQL.AppendLine("		Case When Sum(RightC) > 0 Then 1 Else 0 End RightC, ")
            strSQL.AppendLine("		Case When Sum(RightR) > 0 Then 1 Else 0 End RightR, ")
            strSQL.AppendLine("		Case When Sum(RightP) > 0 Then 1 Else 0 End RightP, ")
            strSQL.AppendLine("		Case When Sum(RightX) > 0 Then 1 Else 0 End RightX, ")
            strSQL.AppendLine("		Case When Sum(RightL) > 0 Then 1 Else 0 End RightL")
            strSQL.AppendLine("	From (")
            strSQL.AppendLine("		Select b.GroupID, a.FunID, a.FunName, ")
            strSQL.AppendLine("				Case When b.RightID = 'A' Then 1 Else 0 End RightA,")
            strSQL.AppendLine("				Case When b.RightID = 'U' Then 1 Else 0 End RightU,")
            strSQL.AppendLine("				Case When b.RightID = 'D' Then 1 Else 0 End RightD,")
            strSQL.AppendLine("				Case When b.RightID = 'I' Then 1 Else 0 End RightI,")
            strSQL.AppendLine("				Case When b.RightID = 'C' Then 1 Else 0 End RightC,")
            strSQL.AppendLine("				Case When b.RightID = 'R' Then 1 Else 0 End RightR,")
            strSQL.AppendLine("				Case When b.RightID = 'P' Then 1 Else 0 End RightP,")
            strSQL.AppendLine("				Case When b.RightID = 'X' Then 1 Else 0 End RightX,")
            strSQL.AppendLine("				Case When b.RightID = 'L' Then 1 Else 0 End RightL")
            strSQL.AppendLine("		From SC_Fun a with (nolock) ")
            'strSQL.AppendLine("				inner join SC_GroupFun b with (nolock) on a.FunID = b.FunID ")
            strSQL.AppendLine("				inner join SC_GroupFun b with (nolock) on a.FunID = b.FunID and a.SysID = b.SysID") '20140905 Ann modify
			If GroupFunType.ToString = "Group" Then
            strSQL.AppendLine("		Where a.FunID = " & Bsp.Utility.Quote(GroupID) & "and CompRoleID =" & Bsp.Utility.Quote(CompRoleID) & ") a")
			Else
                strSQL.AppendLine("		Where CompRoleID =" & Bsp.Utility.Quote(CompRoleID) & ") a")
            End If
            strSQL.AppendLine("	Group by a.GroupID, a.FunID, a.FunName) b on a.FunID = b.FunID ")
            If GroupFunType.ToString = "Group" Then
                strSQL.AppendLine("     inner join SC_Group c with (nolock) on b.GroupID = c.GroupID and c.GroupID =" & Bsp.Utility.Quote(GroupID))  '20140905 Ann add
            Else
                strSQL.AppendLine("     inner join SC_Group c with (nolock) on b.GroupID = c.GroupID ")  '20140905 Ann add
            End If
            strSQL.AppendLine("     inner join SC_Company d with (nolock) on c.CompRoleID = d.CompID ")  '20140905 Ann add
            If GroupFunType.ToString = "Group" Then
                strSQL.AppendLine("Order by b.GroupID")
            Else
                strSQL.AppendLine("where d.CompID = " & Bsp.Utility.Quote(CompRoleID))
                strSQL.AppendLine("Order by d.CompID, b.GroupID")
            End If
        End If
            Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    '20150304 Beatrice Add
    Public Function GetGroupFun_0500(ByVal GroupID As String, ByVal FunID As String, ByVal CompRoleID As String) As DataTable
        Dim strSQL As New StringBuilder

        strSQL.AppendLine("Select c.SysID, c.CompRoleID, d.CompName ,b.GroupID, c.GroupName, a.FunID, a.FunName,")
        strSQL.AppendLine("Case When a.RightA > 0 Then Case When b.RightA > 0 Then 'V' Else '' End Else 'X' End RightA, ")
        strSQL.AppendLine("Case When a.RightU > 0 Then Case When b.RightU > 0 Then 'V' Else '' End Else 'X' End RightU, ")
        strSQL.AppendLine("Case When a.RightD > 0 Then Case When b.RightD > 0 Then 'V' Else '' End Else 'X' End RightD, ")
        strSQL.AppendLine("Case When a.RightI > 0 Then Case When b.RightI > 0 Then 'V' Else '' End Else 'X' End RightI, ")
        '20150720 Beatrice modify
        strSQL.AppendLine("Case When a.RightE > 0 Then Case When b.RightE > 0 Then 'V' Else '' End Else 'X' End RightE, ")
        strSQL.AppendLine("Case When a.RightC > 0 Then Case When b.RightC > 0 Then 'V' Else '' End Else 'X' End RightC, ")
        strSQL.AppendLine("Case When a.RightP > 0 Then Case When b.RightP > 0 Then 'V' Else '' End Else 'X' End RightP, ")
        strSQL.AppendLine("Case When a.RightL > 0 Then Case When b.RightL > 0 Then 'V' Else '' End Else 'X' End RightL, ")
        strSQL.AppendLine("Case When a.RightF > 0 Then Case When b.RightF > 0 Then 'V' Else '' End Else 'X' End RightF, ")
        strSQL.AppendLine("Case When a.RightR > 0 Then Case When b.RightR > 0 Then 'V' Else '' End Else 'X' End RightR, ")
        strSQL.AppendLine("Case When a.RightG > 0 Then Case When b.RightG > 0 Then 'V' Else '' End Else 'X' End RightG, ")
        strSQL.AppendLine("Case When a.RightB > 0 Then Case When b.RightB > 0 Then 'V' Else '' End Else 'X' End RightB, ")
        strSQL.AppendLine("Case When a.RightX > 0 Then Case When b.RightX > 0 Then 'V' Else '' End Else 'X' End RightX, ")
        strSQL.AppendLine("Case When a.RightH > 0 Then Case When b.RightH > 0 Then 'V' Else '' End Else 'X' End RightH, ")
        strSQL.AppendLine("Case When a.RightJ > 0 Then Case When b.RightJ > 0 Then 'V' Else '' End Else 'X' End RightJ, ")
        '20150720 Beatrice modify End
        '20150312 Beatrice modify
        strSQL.AppendLine("LastChgComp = (Select Top 1 C.CompName From SC_GroupFun e Left Join SC_Company C on C.CompID = e.LastChgComp ")
        strSQL.AppendLine("Where c.SysID = e.SysID And c.CompRoleID = e.CompRoleID And b.GroupID = e.GroupID And a.FunID = e.FunID Order By e.LastChgDate DESC), ")
        strSQL.AppendLine("LastChgID = (Select Top 1 U.UserName From SC_GroupFun e Left Join SC_User U on U.CompID = e.LastChgComp And U.UserID = e.LastChgID ")
        strSQL.AppendLine("Where c.SysID = e.SysID And c.CompRoleID = e.CompRoleID And b.GroupID = e.GroupID And a.FunID = e.FunID Order By e.LastChgDate DESC), ")
        strSQL.AppendLine("LastChgDate = (Select Top 1 e.LastChgDate From SC_GroupFun e Where c.SysID = e.SysID And c.CompRoleID = e.CompRoleID ")
        strSQL.AppendLine("And b.GroupID = e.GroupID And a.FunID = e.FunID Order By e.LastChgDate DESC) ")
        '20150312 Beatrice modify End
        strSQL.AppendLine("From (")
        strSQL.AppendLine("Select a.FunID, a.FunName, ")
        strSQL.AppendLine("Case When Sum(RightA) > 0 Then 1 Else 0 End RightA, ")
        strSQL.AppendLine("Case When Sum(RightU) > 0 Then 1 Else 0 End RightU, ")
        strSQL.AppendLine("Case When Sum(RightD) > 0 Then 1 Else 0 End RightD, ")
        strSQL.AppendLine("Case When Sum(RightI) > 0 Then 1 Else 0 End RightI, ")
        '20150720 Beatrice modify
        strSQL.AppendLine("Case When Sum(RightE) > 0 Then 1 Else 0 End RightE, ")
        strSQL.AppendLine("Case When Sum(RightC) > 0 Then 1 Else 0 End RightC, ")
        strSQL.AppendLine("Case When Sum(RightP) > 0 Then 1 Else 0 End RightP, ")
        strSQL.AppendLine("Case When Sum(RightL) > 0 Then 1 Else 0 End RightL, ")
        strSQL.AppendLine("Case When Sum(RightF) > 0 Then 1 Else 0 End RightF, ")
        strSQL.AppendLine("Case When Sum(RightR) > 0 Then 1 Else 0 End RightR, ")
        strSQL.AppendLine("Case When Sum(RightG) > 0 Then 1 Else 0 End RightG, ")
        strSQL.AppendLine("Case When Sum(RightB) > 0 Then 1 Else 0 End RightB, ")
        strSQL.AppendLine("Case When Sum(RightX) > 0 Then 1 Else 0 End RightX, ")
        strSQL.AppendLine("Case When Sum(RightH) > 0 Then 1 Else 0 End RightH, ")
        strSQL.AppendLine("Case When Sum(RightJ) > 0 Then 1 Else 0 End RightJ ")
        '20150720 Beatrice modify End
        strSQL.AppendLine("From (")
        strSQL.AppendLine("Select a.FunID, a.FunName, ")
        strSQL.AppendLine("Case When b.RightID = 'A' Then 1 Else 0 End RightA,")
        strSQL.AppendLine("Case When b.RightID = 'U' Then 1 Else 0 End RightU,")
        strSQL.AppendLine("Case When b.RightID = 'D' Then 1 Else 0 End RightD,")
        strSQL.AppendLine("Case When b.RightID = 'I' Then 1 Else 0 End RightI,")
        '20150720 Beatrice modify
        strSQL.AppendLine("Case When b.RightID = 'E' Then 1 Else 0 End RightE,")
        strSQL.AppendLine("Case When b.RightID = 'C' Then 1 Else 0 End RightC,")
        strSQL.AppendLine("Case When b.RightID = 'P' Then 1 Else 0 End RightP,")
        strSQL.AppendLine("Case When b.RightID = 'L' Then 1 Else 0 End RightL,")
        strSQL.AppendLine("Case When b.RightID = 'F' Then 1 Else 0 End RightF,")
        strSQL.AppendLine("Case When b.RightID = 'R' Then 1 Else 0 End RightR,")
        strSQL.AppendLine("Case When b.RightID = 'G' Then 1 Else 0 End RightG,")
        strSQL.AppendLine("Case When b.RightID = 'B' Then 1 Else 0 End RightB,")
        strSQL.AppendLine("Case When b.RightID = 'X' Then 1 Else 0 End RightX,")
        strSQL.AppendLine("Case When b.RightID = 'H' Then 1 Else 0 End RightH,")
        strSQL.AppendLine("Case When b.RightID = 'J' Then 1 Else 0 End RightJ")
        '20150720 Beatrice modify End
        strSQL.AppendLine("From SC_Fun a with (nolock) ")
        strSQL.AppendLine("inner join SC_FunRight b with (nolock) on a.FunID = b.FunID and a.SysID = b.SysID")
        strSQL.AppendLine(") a")
        strSQL.AppendLine("Group by a.FunID, a.FunName) a")
        strSQL.AppendLine("inner join (")
        strSQL.AppendLine("Select a.GroupID, a.FunID, a.FunName, ")
        strSQL.AppendLine("Case When Sum(RightA) > 0 Then 1 Else 0 End RightA, ")
        strSQL.AppendLine("Case When Sum(RightU) > 0 Then 1 Else 0 End RightU, ")
        strSQL.AppendLine("Case When Sum(RightD) > 0 Then 1 Else 0 End RightD, ")
        strSQL.AppendLine("Case When Sum(RightI) > 0 Then 1 Else 0 End RightI, ")
        '20150720 Beatrice modify
        strSQL.AppendLine("Case When Sum(RightE) > 0 Then 1 Else 0 End RightE, ")
        strSQL.AppendLine("Case When Sum(RightC) > 0 Then 1 Else 0 End RightC, ")
        strSQL.AppendLine("Case When Sum(RightP) > 0 Then 1 Else 0 End RightP, ")
        strSQL.AppendLine("Case When Sum(RightL) > 0 Then 1 Else 0 End RightL, ")
        strSQL.AppendLine("Case When Sum(RightF) > 0 Then 1 Else 0 End RightF, ")
        strSQL.AppendLine("Case When Sum(RightR) > 0 Then 1 Else 0 End RightR, ")
        strSQL.AppendLine("Case When Sum(RightG) > 0 Then 1 Else 0 End RightG, ")
        strSQL.AppendLine("Case When Sum(RightB) > 0 Then 1 Else 0 End RightB, ")
        strSQL.AppendLine("Case When Sum(RightX) > 0 Then 1 Else 0 End RightX, ")
        strSQL.AppendLine("Case When Sum(RightH) > 0 Then 1 Else 0 End RightH, ")
        strSQL.AppendLine("Case When Sum(RightJ) > 0 Then 1 Else 0 End RightJ ")
        '20150720 Beatrice modify End
        strSQL.AppendLine("From (")
        strSQL.AppendLine("Select b.GroupID, a.FunID, a.FunName, ")
        strSQL.AppendLine("Case When b.RightID = 'A' Then 1 Else 0 End RightA,")
        strSQL.AppendLine("Case When b.RightID = 'U' Then 1 Else 0 End RightU,")
        strSQL.AppendLine("Case When b.RightID = 'D' Then 1 Else 0 End RightD,")
        strSQL.AppendLine("Case When b.RightID = 'I' Then 1 Else 0 End RightI,")
        '20150720 Beatrice modify
        strSQL.AppendLine("Case When b.RightID = 'E' Then 1 Else 0 End RightE,")
        strSQL.AppendLine("Case When b.RightID = 'C' Then 1 Else 0 End RightC,")
        strSQL.AppendLine("Case When b.RightID = 'P' Then 1 Else 0 End RightP,")
        strSQL.AppendLine("Case When b.RightID = 'L' Then 1 Else 0 End RightL,")
        strSQL.AppendLine("Case When b.RightID = 'F' Then 1 Else 0 End RightF,")
        strSQL.AppendLine("Case When b.RightID = 'R' Then 1 Else 0 End RightR,")
        strSQL.AppendLine("Case When b.RightID = 'G' Then 1 Else 0 End RightG,")
        strSQL.AppendLine("Case When b.RightID = 'B' Then 1 Else 0 End RightB,")
        strSQL.AppendLine("Case When b.RightID = 'X' Then 1 Else 0 End RightX,")
        strSQL.AppendLine("Case When b.RightID = 'H' Then 1 Else 0 End RightH,")
        strSQL.AppendLine("Case When b.RightID = 'J' Then 1 Else 0 End RightJ")
        '20150720 Beatrice modify End
        strSQL.AppendLine("From SC_Fun a with (nolock) ")
        strSQL.AppendLine("inner join SC_GroupFun b with (nolock) on a.FunID = b.FunID and a.SysID = b.SysID")
        strSQL.AppendLine("Where 1=1")
        If CompRoleID <> "0" Then
            strSQL.AppendLine("And CompRoleID = " & Bsp.Utility.Quote(CompRoleID))
        End If
        strSQL.AppendLine(") a ")
        strSQL.AppendLine("Group by a.GroupID, a.FunID, a.FunName) b on a.FunID = b.FunID ")
        strSQL.AppendLine("inner join SC_Group c with (nolock) on b.GroupID = c.GroupID ")
        strSQL.AppendLine("inner join (")
        strSQL.AppendLine("Select CompID = 'ALL', CompName = '' UNION Select CompID, CompName From SC_Company with (nolock)")
        strSQL.AppendLine(")d on c.CompRoleID = d.CompID ")
        strSQL.AppendLine("Where 1=1 ")
        If CompRoleID <> "0" Then
            strSQL.AppendLine("And d.CompID = " & Bsp.Utility.Quote(CompRoleID))
        End If
        If GroupID <> "" Then
            strSQL.AppendLine("And b.GroupID = " & Bsp.Utility.Quote(GroupID))
        End If
        If FunID <> "" Then
            strSQL.AppendLine("And a.FunID = " & Bsp.Utility.Quote(FunID))
        End If
        strSQL.AppendLine("Order by d.CompID, b.GroupID, a.FunID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function
#End Region

#Region "SC0600：使用者權限維護-查詢"
    'Public Function UserGroup(ByVal ParamArray Params() As String) As DataTable
    '    Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
    '    Dim strSQL As New StringBuilder()

    '    For Each strKey As String In ht.Keys
    '        If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
    '            Select Case strKey
    '                Case "CompRoleID"
    '                    strSQL.AppendLine("And CompRoleID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
    '                Case "GroupID"
    '                    strSQL.AppendLine("And UserID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
    '            End Select
    '        End If
    '    Next

    '    Return GetUserGroup("", strSQL.ToString())
    'End Function

    Public Function GetUserGroup(ByVal CompRoleID As String, ByVal UserID As String, ByVal UserName As String, ByVal GroupID As String) As DataTable '20150727 Beatrice modify
        Dim strSQL As New StringBuilder
        strSQL.AppendLine(" Select distinct a.CompID,a.CompName,a.SysID,a.UserID,a.UserID+' - '+a.UserName as UserName,c.CompRoleID, d.CompName as CompRoleName,c.GroupID, e.GroupName , a.FunID, a.FunName, ")
        strSQL.AppendLine("	Case When a.RightA > 0 Then Case When b.RightA > 0 Then 'V' Else '' End Else 'X' End RightA, ")
        strSQL.AppendLine("	Case When a.RightU > 0 Then Case When b.RightU > 0 Then 'V' Else '' End Else 'X' End RightU, ")
        strSQL.AppendLine("	Case When a.RightD > 0 Then Case When b.RightD > 0 Then 'V' Else '' End Else 'X' End RightD, ")
        strSQL.AppendLine("	Case When a.RightI > 0 Then Case When b.RightI > 0 Then 'V' Else '' End Else 'X' End RightI, ")
        '20150721 Beatrice modify
        strSQL.AppendLine(" Case When a.RightE > 0 Then Case When b.RightE > 0 Then 'V' Else '' End Else 'X' End RightE, ")
        strSQL.AppendLine(" Case When a.RightC > 0 Then Case When b.RightC > 0 Then 'V' Else '' End Else 'X' End RightC, ")
        strSQL.AppendLine(" Case When a.RightP > 0 Then Case When b.RightP > 0 Then 'V' Else '' End Else 'X' End RightP, ")
        strSQL.AppendLine(" Case When a.RightL > 0 Then Case When b.RightL > 0 Then 'V' Else '' End Else 'X' End RightL, ")
        strSQL.AppendLine(" Case When a.RightF > 0 Then Case When b.RightF > 0 Then 'V' Else '' End Else 'X' End RightF, ")
        strSQL.AppendLine(" Case When a.RightR > 0 Then Case When b.RightR > 0 Then 'V' Else '' End Else 'X' End RightR, ")
        strSQL.AppendLine(" Case When a.RightG > 0 Then Case When b.RightG > 0 Then 'V' Else '' End Else 'X' End RightG, ")
        strSQL.AppendLine(" Case When a.RightB > 0 Then Case When b.RightB > 0 Then 'V' Else '' End Else 'X' End RightB, ")
        strSQL.AppendLine(" Case When a.RightX > 0 Then Case When b.RightX > 0 Then 'V' Else '' End Else 'X' End RightX, ")
        strSQL.AppendLine(" Case When a.RightH > 0 Then Case When b.RightH > 0 Then 'V' Else '' End Else 'X' End RightH, ")
        strSQL.AppendLine(" Case When a.RightJ > 0 Then Case When b.RightJ > 0 Then 'V' Else '' End Else 'X' End RightJ ")
        '20150721 Beatrice modify End
        strSQL.AppendLine("From (")
        strSQL.AppendLine("	Select a.FunID, a.FunName,a.CompName,a.CompRoleID,a.CompID,a.SysID, a.UserID,a.UserName,a.GroupID,")
        strSQL.AppendLine("		Case When Sum(RightA) > 0 Then 1 Else 0 End RightA, ")
        strSQL.AppendLine("		Case When Sum(RightU) > 0 Then 1 Else 0 End RightU, ")
        strSQL.AppendLine("		Case When Sum(RightD) > 0 Then 1 Else 0 End RightD, ")
        strSQL.AppendLine("		Case When Sum(RightI) > 0 Then 1 Else 0 End RightI, ")
        '20150721 Beatrice modify
        strSQL.AppendLine("		Case When Sum(RightE) > 0 Then 1 Else 0 End RightE, ")
        strSQL.AppendLine("		Case When Sum(RightC) > 0 Then 1 Else 0 End RightC, ")
        strSQL.AppendLine("		Case When Sum(RightP) > 0 Then 1 Else 0 End RightP, ")
        strSQL.AppendLine("		Case When Sum(RightL) > 0 Then 1 Else 0 End RightL, ")
        strSQL.AppendLine("		Case When Sum(RightF) > 0 Then 1 Else 0 End RightF, ")
        strSQL.AppendLine("		Case When Sum(RightR) > 0 Then 1 Else 0 End RightR, ")
        strSQL.AppendLine("		Case When Sum(RightG) > 0 Then 1 Else 0 End RightG, ")
        strSQL.AppendLine("		Case When Sum(RightB) > 0 Then 1 Else 0 End RightB, ")
        strSQL.AppendLine("		Case When Sum(RightX) > 0 Then 1 Else 0 End RightX, ")
        strSQL.AppendLine("		Case When Sum(RightH) > 0 Then 1 Else 0 End RightH, ")
        strSQL.AppendLine("		Case When Sum(RightJ) > 0 Then 1 Else 0 End RightJ ")
        '20150721 Beatrice modify End
        strSQL.AppendLine("	From (")
        strSQL.AppendLine("		Select a.FunID, a.FunName,h.CompName,f.CompRoleID,f.CompID,f.SysID,f.UserID,g.UserName,f.GroupID,")
        strSQL.AppendLine("				Case When b.RightID = 'A' Then 1 Else 0 End RightA,")
        strSQL.AppendLine("				Case When b.RightID = 'U' Then 1 Else 0 End RightU,")
        strSQL.AppendLine("				Case When b.RightID = 'D' Then 1 Else 0 End RightD,")
        strSQL.AppendLine("				Case When b.RightID = 'I' Then 1 Else 0 End RightI,")
        '20150721 Beatrice modify
        strSQL.AppendLine("				Case When b.RightID = 'E' Then 1 Else 0 End RightE,")
        strSQL.AppendLine("				Case When b.RightID = 'C' Then 1 Else 0 End RightC,")
        strSQL.AppendLine("				Case When b.RightID = 'P' Then 1 Else 0 End RightP,")
        strSQL.AppendLine("				Case When b.RightID = 'L' Then 1 Else 0 End RightL,")
        strSQL.AppendLine("				Case When b.RightID = 'F' Then 1 Else 0 End RightF,")
        strSQL.AppendLine("				Case When b.RightID = 'R' Then 1 Else 0 End RightR,")
        strSQL.AppendLine("				Case When b.RightID = 'G' Then 1 Else 0 End RightG,")
        strSQL.AppendLine("				Case When b.RightID = 'B' Then 1 Else 0 End RightB,")
        strSQL.AppendLine("				Case When b.RightID = 'X' Then 1 Else 0 End RightX,")
        strSQL.AppendLine("				Case When b.RightID = 'H' Then 1 Else 0 End RightH,")
        strSQL.AppendLine("				Case When b.RightID = 'J' Then 1 Else 0 End RightJ")
        '20150721 Beatrice modify End
        strSQL.AppendLine("		From SC_Fun a with (nolock)")
        strSQL.AppendLine("				inner join SC_FunRight b with (nolock) on a.FunID = b.FunID and a.SysID = b.SysID")
        strSQL.AppendLine("				inner join SC_UserGroup f with (nolock) on a.SysID = f.SysID and b.SysID = f.SysID ")
        strSQL.AppendLine("				inner join SC_User g on f.SysID = a.SysID and f.CompID = g.CompID and f.UserID = g.UserID ")
        strSQL.AppendLine("				inner join SC_Company h with (nolock) on g.CompID = h.CompID")
        '20151119 Beatrice modify
        strSQL.AppendLine("		Where a.FunID in (Select distinct FunID From SC_GroupFun with (nolock))")
        If UserID <> "" Then
            strSQL.AppendLine("		And f.UserID = " & Bsp.Utility.Quote(UserID))
        End If
        If CompRoleID <> "0" Then
            strSQL.AppendLine("		And f.CompRoleID = " & Bsp.Utility.Quote(CompRoleID))
        End If
        strSQL.AppendLine("		) a")
        '20151119 Beatrice modify End
        strSQL.AppendLine("	Group by a.FunID, a.FunName,a.CompName,a.CompRoleID,a.CompID,a.SysID,a.UserID,a.UserName,a.GroupID) a")
        strSQL.AppendLine("	inner join ")
        strSQL.AppendLine("	(Select a.FunID, a.FunName,a.CompName,a.CompRoleID,a.CompID,a.SysID,a.UserID,a.UserID+' - '+a.UserName as UseID,")
        strSQL.AppendLine("		Case When Sum(RightA) > 0 Then 1 Else 0 End RightA, ")
        strSQL.AppendLine("		Case When Sum(RightU) > 0 Then 1 Else 0 End RightU, ")
        strSQL.AppendLine("		Case When Sum(RightD) > 0 Then 1 Else 0 End RightD, ")
        strSQL.AppendLine("		Case When Sum(RightI) > 0 Then 1 Else 0 End RightI, ")
        '20150721 Beatrice modify
        strSQL.AppendLine("		Case When Sum(RightE) > 0 Then 1 Else 0 End RightE, ")
        strSQL.AppendLine("		Case When Sum(RightC) > 0 Then 1 Else 0 End RightC, ")
        strSQL.AppendLine("		Case When Sum(RightP) > 0 Then 1 Else 0 End RightP, ")
        strSQL.AppendLine("		Case When Sum(RightL) > 0 Then 1 Else 0 End RightL, ")
        strSQL.AppendLine("		Case When Sum(RightF) > 0 Then 1 Else 0 End RightF, ")
        strSQL.AppendLine("		Case When Sum(RightR) > 0 Then 1 Else 0 End RightR, ")
        strSQL.AppendLine("		Case When Sum(RightG) > 0 Then 1 Else 0 End RightG, ")
        strSQL.AppendLine("		Case When Sum(RightB) > 0 Then 1 Else 0 End RightB, ")
        strSQL.AppendLine("		Case When Sum(RightX) > 0 Then 1 Else 0 End RightX, ")
        strSQL.AppendLine("		Case When Sum(RightH) > 0 Then 1 Else 0 End RightH, ")
        strSQL.AppendLine("		Case When Sum(RightJ) > 0 Then 1 Else 0 End RightJ ")
        '20150721 Beatrice modify End
        strSQL.AppendLine("	From (")
        strSQL.AppendLine("		Select a.FunID, a.FunName,h.CompName,f.CompRoleID,f.CompID,f.SysID,f.UserID,g.UserName,")
        strSQL.AppendLine("				Case When b.RightID = 'A' Then 1 Else 0 End RightA,")
        strSQL.AppendLine("				Case When b.RightID = 'U' Then 1 Else 0 End RightU,")
        strSQL.AppendLine("				Case When b.RightID = 'D' Then 1 Else 0 End RightD,")
        strSQL.AppendLine("				Case When b.RightID = 'I' Then 1 Else 0 End RightI,")
        '20150721 Beatrice modify
        strSQL.AppendLine("				Case When b.RightID = 'E' Then 1 Else 0 End RightE,")
        strSQL.AppendLine("				Case When b.RightID = 'C' Then 1 Else 0 End RightC,")
        strSQL.AppendLine("				Case When b.RightID = 'P' Then 1 Else 0 End RightP,")
        strSQL.AppendLine("				Case When b.RightID = 'L' Then 1 Else 0 End RightL,")
        strSQL.AppendLine("				Case When b.RightID = 'F' Then 1 Else 0 End RightF,")
        strSQL.AppendLine("				Case When b.RightID = 'R' Then 1 Else 0 End RightR,")
        strSQL.AppendLine("				Case When b.RightID = 'G' Then 1 Else 0 End RightG,")
        strSQL.AppendLine("				Case When b.RightID = 'B' Then 1 Else 0 End RightB,")
        strSQL.AppendLine("				Case When b.RightID = 'X' Then 1 Else 0 End RightX,")
        strSQL.AppendLine("				Case When b.RightID = 'H' Then 1 Else 0 End RightH,")
        strSQL.AppendLine("				Case When b.RightID = 'J' Then 1 Else 0 End RightJ")
        '20150721 Beatrice modify End
        strSQL.AppendLine("		From SC_Fun a with (nolock) ")
        strSQL.AppendLine("				inner join SC_GroupFun b with (nolock) on a.FunID = b.FunID and a.SysID = b.SysID")
        strSQL.AppendLine("				inner join SC_UserGroup f with (nolock) on a.SysID = f.SysID and b.CompRoleID = f.CompRoleID and f.GroupID = b.GroupID and b.SysID = f.SysID ")
        strSQL.AppendLine("				inner join SC_User g on f.CompID = g.CompID and f.UserID = g.UserID")
        strSQL.AppendLine("				inner join SC_Company h with (nolock) on g.CompID = h.CompID")
        strSQL.AppendLine("		Where 1 = 1")
        '20150323 Beatrice modify
        If UserID <> "" Then
            strSQL.AppendLine("		And f.UserID = " & Bsp.Utility.Quote(UserID))
        End If
        If CompRoleID <> "0" Then
            strSQL.AppendLine("		And f.CompRoleID = " & Bsp.Utility.Quote(CompRoleID))
        End If
        '20150323 Beatrice modify End
        strSQL.AppendLine(") a	Group by a.FunID, a.FunName,a.CompName,a.CompRoleID,a.CompID,a.SysID, a.UserID,a.UserName) b on a.FunID = b.FunID and a.UserID = b.UserID")
        '20150303 Beatrice modify
        If CompRoleID = "0" Then
            strSQL.AppendLine(" inner join SC_GroupFun c with (nolock) on b.FunID  = c.FunID and a.FunID = c.FunID and c.CompRoleID = b.CompRoleID")
        Else
            strSQL.AppendLine("	inner join SC_GroupFun c with (nolock) on b.FunID  = c.FunID and a.FunID = c.FunID and c.CompRoleID = b.CompRoleID and c.CompRoleID = " & Bsp.Utility.Quote(CompRoleID))
        End If
        '20150303 Beatrice modify End
        strSQL.AppendLine("	inner join SC_Company d with (nolock) on c.CompRoleID = d.CompID")
        strSQL.AppendLine("	inner join SC_Group e with (nolock) on c.GroupID = e.GroupID and c.SysID = e.SysID and c.CompRoleID = e.CompRoleID  and a.GroupID = e.GroupID ")
        strSQL.AppendLine("	inner join SC_UserGroup h with (nolock) on a.CompRoleID = h.CompRoleID and c.CompRoleID = h.CompRoleID ")
        '20151119 Beatrice modify
        If UserID <> "" Then
            strSQL.AppendLine(" And h.UserID = " & Bsp.Utility.Quote(UserID))
        End If
        If CompRoleID <> "0" Then
            strSQL.AppendLine(" And h.CompRoleID = " & Bsp.Utility.Quote(CompRoleID))
        End If
        '20151119 Beatrice modify End
        strSQL.AppendLine("	Where 1 = 1 ")
        '20150313 Beatrice modify
        If GroupID <> "" Then
            strSQL.AppendLine("	And c.GroupID = " & Bsp.Utility.Quote(GroupID))
        End If
        '20150313 Beatrice modify End
        '20150727 Beatrice modify
        If UserName <> "" Then
            strSQL.AppendLine("	And a.UserName like N'%" & UserName & "%'")
        End If
        '20150727 Beatrice modify End
        strSQL.AppendLine(" Order by c.GroupID, a.FunID ")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    Public Function AddUserGroup(ByVal beUserGroup As beSC_UserGroup.Row) As Integer
        Dim bsUserGroup As New beSC_UserGroup.Service()
        Try
            Return bsUserGroup.Insert(beUserGroup)
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Public Function DeleteUserGroup(ByVal CompID As String, ByVal UserID As String, ByVal SysID As String, ByVal CompRoleID As String, ByVal GroupID As String) As Integer
    Public Function DeleteUserGroup(ByVal beUserGroup As beSC_UserGroup.Row) As Boolean
        'If GroupID = "" AndAlso FunID = "" Then
        '    Throw New Exception("[DeleteUserGroup]：群組代碼或功能代碼必要輸入一項！")
        'End If

        'Dim strSQL As New StringBuilder()
        'strSQL.AppendLine("Delete From SC_UserGroup Where 1=1")
        'If CompID <> "" Then strSQL.AppendLine("And CompID = " & Bsp.Utility.Quote(CompID))
        'If UserID <> "" Then strSQL.AppendLine("And UserID = " & Bsp.Utility.Quote(UserID))
        'If SysID <> "" Then strSQL.AppendLine("And SysID = " & Bsp.Utility.Quote(SysID))
        'If CompRoleID <> "" Then strSQL.AppendLine("And CompRoleID = " & Bsp.Utility.Quote(CompRoleID))
        'If GroupID <> "" Then strSQL.AppendLine("And GroupID = " & Bsp.Utility.Quote(GroupID))

        'Return Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString())

        Dim bsUserGroup As New beSC_UserGroup.Service()
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Delete From SC_UserGroup Where CompID = " & Bsp.Utility.Quote(beUserGroup.CompID.Value) & "and UserID =" & Bsp.Utility.Quote(beUserGroup.UserID.Value) & "and SysID = " & Bsp.Utility.Quote(beUserGroup.SysID.Value) & "and CompRoleID = " & Bsp.Utility.Quote(beUserGroup.CompRoleID.Value) & "and GroupID = " & Bsp.Utility.Quote(beUserGroup.GroupID.Value))

        Using cn As DbConnection = Bsp.DB.getConnection()
            cn.Open()
            Dim tran As Data.Common.DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                bsUserGroup.DeleteRowByPrimaryKey(beUserGroup, tran)
                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)

                tran.Commit()
                inTrans = False
            Catch ex As Exception
                If inTrans Then tran.Rollback()
                Throw
            Finally
                tran.Dispose()
                If cn.State = ConnectionState.Open Then cn.Close()
            End Try
        End Using

        Return True
    End Function
    ''' <summary>
    ''' 取得SC_GroupFun...
    ''' </summary>
    ''' <param name="GroupID"></param>
    ''' <param name="FunID"></param>
    ''' <param name="FieldNames"></param>
    ''' <param name="CondStr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetGroupFunInfo(ByVal GroupID As String, ByVal FunID As String, Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames.Trim() = "" Then FieldNames = "*"

        strSQL.AppendLine("Select * ")
        strSQL.AppendLine("From SC_GroupFun Where 1 = 1")
        If GroupID <> "" Then strSQL.AppendLine("And GroupID = " & Bsp.Utility.Quote(GroupID))
        If FunID <> "" Then strSQL.AppendLine("And FunID = " & Bsp.Utility.Quote(FunID))
        If CondStr <> "" Then strSQL.AppendLine(CondStr)

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    ''' <summary>
    ''' 取得SC_FunRight...
    ''' 因有Join SC_Right, 故要取得SC_FunRight的欄位,前置詞請用FR; SC_Right的前置詞請用RT
    ''' </summary>
    ''' <param name="FunID"></param>
    ''' <param name="FieldNames"></param>
    ''' <param name="CondStr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFunRightInfo(ByVal FunID As String, Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames = "" Then FieldNames = "*"

        strSQL.AppendLine("Select distinct " & FieldNames)
        strSQL.AppendLine("From SC_FunRight FR")
        strSQL.AppendLine("	inner join SC_Right RT on FR.RightID = RT.RightID")
        strSQL.AppendLine("Where 1 = 1")
        If FunID <> "" Then strSQL.AppendLine("And FunID = " & Bsp.Utility.Quote(FunID))
        If CondStr <> "" Then strSQL.AppendLine(CondStr)

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    '20150310 Beatrice modify
    Public Function HasGroupFun(ByVal beGroupFun As beSC_GroupFun.Row) As Boolean
        Dim bsGroupFun As New beSC_GroupFun.Service()

        Try
            Return bsGroupFun.QueryByKey(beGroupFun).Tables(0).Rows.Count > 0
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function AddGroupFun(ByVal beGroupFun() As beSC_GroupFun.Row) As Integer
        Dim bsGroupFun As New beSC_GroupFun.Service()

        Try
            Return bsGroupFun.Insert(beGroupFun)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function UpdateGroupFun(ByVal beGroupFun() As beSC_GroupFun.Row) As Integer
        If beGroupFun Is Nothing Then Return 0

        Dim db As Database = DatabaseFactory.CreateDatabase()
        Dim bsGroupFun As New beSC_GroupFun.Service()
        Dim intAffectedRow As Integer = 0

        Using cn As DbConnection = db.CreateConnection
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Try
                intAffectedRow += DeleteGroupFun(beGroupFun(0).GroupID.Value, beGroupFun(0).FunID.Value, beGroupFun(0).SysID.Value, beGroupFun(0).CompRoleID.Value)
                intAffectedRow += bsGroupFun.Insert(beGroupFun, tran)
                tran.Commit()
            Catch ex As Exception
                tran.Rollback()
                Throw
            Finally
                If tran IsNot Nothing Then tran.Dispose()
            End Try
        End Using
    End Function

    ''' <summary>
    ''' 刪除SC_GroupFun
    ''' </summary>
    ''' <param name="GroupID">不指定請輸入空字串</param>
    ''' <param name="FunID">不指定請輸入空字串</param>
    ''' <returns></returns>
    ''' <remarks>兩者都沒指定會刪除所有的SC_GroupFun, 要注意</remarks>
    Public Function DeleteGroupFun(ByVal GroupID As String, ByVal FunID As String, ByVal SysID As String, ByVal CompRoleID As String) As Integer
        If GroupID = "" AndAlso FunID = "" Then
            Throw New Exception("[DeleteGroupFun]：群組代碼或功能代碼必要輸入一項！")
        End If
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Delete From SC_GroupFun Where 1=1")
        If GroupID <> "" Then strSQL.AppendLine("And GroupID = " & Bsp.Utility.Quote(GroupID))
        If FunID <> "" Then strSQL.AppendLine("And FunID = " & Bsp.Utility.Quote(FunID))
        If SysID <> "" Then strSQL.AppendLine("And SysID = " & Bsp.Utility.Quote(SysID))
        If CompRoleID <> "" Then strSQL.AppendLine("And CompRoleID = " & Bsp.Utility.Quote(CompRoleID))

        Return Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString())
    End Function
#End Region

#Region "SC0601：使用者權限維護-新增"
    Public Function GetUserGroupFunadd(ByVal GroupID As String, ByVal CompRoleID As String) As DataTable
        Dim strSQL As New StringBuilder

        'strSQL.AppendLine("Select a.FunID, a.FunName,")
        strSQL.AppendLine("Select distinct d.CompName,c.GroupID, e.GroupName , a.FunID, a.FunName,") '20140905 Ann modify
        strSQL.AppendLine("	Case When a.RightA > 0 Then Case When b.RightA > 0 Then 'V' Else '' End Else 'X' End RightA, ")
        strSQL.AppendLine("	Case When a.RightU > 0 Then Case When b.RightU > 0 Then 'V' Else '' End Else 'X' End RightU, ")
        strSQL.AppendLine("	Case When a.RightD > 0 Then Case When b.RightD > 0 Then 'V' Else '' End Else 'X' End RightD, ")
        strSQL.AppendLine("	Case When a.RightI > 0 Then Case When b.RightI > 0 Then 'V' Else '' End Else 'X' End RightI, ")
        '20150721 Beatrice modify
        strSQL.AppendLine(" Case When a.RightE > 0 Then Case When b.RightE > 0 Then 'V' Else '' End Else 'X' End RightE, ")
        strSQL.AppendLine(" Case When a.RightC > 0 Then Case When b.RightC > 0 Then 'V' Else '' End Else 'X' End RightC, ")
        strSQL.AppendLine(" Case When a.RightP > 0 Then Case When b.RightP > 0 Then 'V' Else '' End Else 'X' End RightP, ")
        strSQL.AppendLine(" Case When a.RightL > 0 Then Case When b.RightL > 0 Then 'V' Else '' End Else 'X' End RightL, ")
        strSQL.AppendLine(" Case When a.RightF > 0 Then Case When b.RightF > 0 Then 'V' Else '' End Else 'X' End RightF, ")
        strSQL.AppendLine(" Case When a.RightR > 0 Then Case When b.RightR > 0 Then 'V' Else '' End Else 'X' End RightR, ")
        strSQL.AppendLine(" Case When a.RightG > 0 Then Case When b.RightG > 0 Then 'V' Else '' End Else 'X' End RightG, ")
        strSQL.AppendLine(" Case When a.RightB > 0 Then Case When b.RightB > 0 Then 'V' Else '' End Else 'X' End RightB, ")
        strSQL.AppendLine(" Case When a.RightX > 0 Then Case When b.RightX > 0 Then 'V' Else '' End Else 'X' End RightX, ")
        strSQL.AppendLine(" Case When a.RightH > 0 Then Case When b.RightH > 0 Then 'V' Else '' End Else 'X' End RightH, ")
        strSQL.AppendLine(" Case When a.RightJ > 0 Then Case When b.RightJ > 0 Then 'V' Else '' End Else 'X' End RightJ ")
        '20150721 Beatrice modify End
        strSQL.AppendLine("From (")
        strSQL.AppendLine("	Select a.FunID, a.FunName, ")
        strSQL.AppendLine("		Case When Sum(RightA) > 0 Then 1 Else 0 End RightA, ")
        strSQL.AppendLine("		Case When Sum(RightU) > 0 Then 1 Else 0 End RightU, ")
        strSQL.AppendLine("		Case When Sum(RightD) > 0 Then 1 Else 0 End RightD, ")
        strSQL.AppendLine("		Case When Sum(RightI) > 0 Then 1 Else 0 End RightI, ")
        '20150721 Beatrice modify
        strSQL.AppendLine("		Case When Sum(RightE) > 0 Then 1 Else 0 End RightE, ")
        strSQL.AppendLine("		Case When Sum(RightC) > 0 Then 1 Else 0 End RightC, ")
        strSQL.AppendLine("		Case When Sum(RightP) > 0 Then 1 Else 0 End RightP, ")
        strSQL.AppendLine("		Case When Sum(RightL) > 0 Then 1 Else 0 End RightL, ")
        strSQL.AppendLine("		Case When Sum(RightF) > 0 Then 1 Else 0 End RightF, ")
        strSQL.AppendLine("		Case When Sum(RightR) > 0 Then 1 Else 0 End RightR, ")
        strSQL.AppendLine("		Case When Sum(RightG) > 0 Then 1 Else 0 End RightG, ")
        strSQL.AppendLine("		Case When Sum(RightB) > 0 Then 1 Else 0 End RightB, ")
        strSQL.AppendLine("		Case When Sum(RightX) > 0 Then 1 Else 0 End RightX, ")
        strSQL.AppendLine("		Case When Sum(RightH) > 0 Then 1 Else 0 End RightH, ")
        strSQL.AppendLine("		Case When Sum(RightJ) > 0 Then 1 Else 0 End RightJ ")
        '20150721 Beatrice modify End
        strSQL.AppendLine("	From (")
        strSQL.AppendLine("		Select a.FunID, a.FunName, ")
        strSQL.AppendLine("				Case When b.RightID = 'A' Then 1 Else 0 End RightA,")
        strSQL.AppendLine("				Case When b.RightID = 'U' Then 1 Else 0 End RightU,")
        strSQL.AppendLine("				Case When b.RightID = 'D' Then 1 Else 0 End RightD,")
        strSQL.AppendLine("				Case When b.RightID = 'I' Then 1 Else 0 End RightI,")
        '20150721 Beatrice modify
        strSQL.AppendLine("				Case When b.RightID = 'E' Then 1 Else 0 End RightE,")
        strSQL.AppendLine("				Case When b.RightID = 'C' Then 1 Else 0 End RightC,")
        strSQL.AppendLine("				Case When b.RightID = 'P' Then 1 Else 0 End RightP,")
        strSQL.AppendLine("				Case When b.RightID = 'L' Then 1 Else 0 End RightL,")
        strSQL.AppendLine("				Case When b.RightID = 'F' Then 1 Else 0 End RightF,")
        strSQL.AppendLine("				Case When b.RightID = 'R' Then 1 Else 0 End RightR,")
        strSQL.AppendLine("				Case When b.RightID = 'G' Then 1 Else 0 End RightG,")
        strSQL.AppendLine("				Case When b.RightID = 'B' Then 1 Else 0 End RightB,")
        strSQL.AppendLine("				Case When b.RightID = 'X' Then 1 Else 0 End RightX,")
        strSQL.AppendLine("				Case When b.RightID = 'H' Then 1 Else 0 End RightH,")
        strSQL.AppendLine("				Case When b.RightID = 'J' Then 1 Else 0 End RightJ")
        '20150721 Beatrice modify End
        strSQL.AppendLine("		From SC_Fun a with (nolock)")
        'strSQL.AppendLine("				inner join SC_FunRight b with (nolock) on a.FunID = b.FunID")
        strSQL.AppendLine("				inner join SC_FunRight b with (nolock) on a.FunID = b.FunID and a.SysID = b.SysID")   '20140905 Ann modify
        strSQL.AppendLine("		Where a.FunID in (Select FunID From SC_GroupFun with (nolock) Where GroupID = " & Bsp.Utility.Quote(GroupID) & ")) a")
        strSQL.AppendLine("	Group by a.FunID, a.FunName) a")
        strSQL.AppendLine("	inner join ")
        strSQL.AppendLine("	(Select a.FunID, a.FunName, a.GroupID, ")
        strSQL.AppendLine("		Case When Sum(RightA) > 0 Then 1 Else 0 End RightA, ")
        strSQL.AppendLine("		Case When Sum(RightU) > 0 Then 1 Else 0 End RightU, ")
        strSQL.AppendLine("		Case When Sum(RightD) > 0 Then 1 Else 0 End RightD, ")
        strSQL.AppendLine("		Case When Sum(RightI) > 0 Then 1 Else 0 End RightI, ")
        '20150721 Beatrice modify
        strSQL.AppendLine("		Case When Sum(RightE) > 0 Then 1 Else 0 End RightE, ")
        strSQL.AppendLine("		Case When Sum(RightC) > 0 Then 1 Else 0 End RightC, ")
        strSQL.AppendLine("		Case When Sum(RightP) > 0 Then 1 Else 0 End RightP, ")
        strSQL.AppendLine("		Case When Sum(RightL) > 0 Then 1 Else 0 End RightL, ")
        strSQL.AppendLine("		Case When Sum(RightF) > 0 Then 1 Else 0 End RightF, ")
        strSQL.AppendLine("		Case When Sum(RightR) > 0 Then 1 Else 0 End RightR, ")
        strSQL.AppendLine("		Case When Sum(RightG) > 0 Then 1 Else 0 End RightG, ")
        strSQL.AppendLine("		Case When Sum(RightB) > 0 Then 1 Else 0 End RightB, ")
        strSQL.AppendLine("		Case When Sum(RightX) > 0 Then 1 Else 0 End RightX, ")
        strSQL.AppendLine("		Case When Sum(RightH) > 0 Then 1 Else 0 End RightH, ")
        strSQL.AppendLine("		Case When Sum(RightJ) > 0 Then 1 Else 0 End RightJ ")
        '20150721 Beatrice modify End
        strSQL.AppendLine("	From (")
        strSQL.AppendLine("		Select a.FunID, a.FunName, b.GroupID, ")
        strSQL.AppendLine("				Case When b.RightID = 'A' Then 1 Else 0 End RightA,")
        strSQL.AppendLine("				Case When b.RightID = 'U' Then 1 Else 0 End RightU,")
        strSQL.AppendLine("				Case When b.RightID = 'D' Then 1 Else 0 End RightD,")
        strSQL.AppendLine("				Case When b.RightID = 'I' Then 1 Else 0 End RightI,")
        '20150721 Beatrice modify
        strSQL.AppendLine("				Case When b.RightID = 'E' Then 1 Else 0 End RightE,")
        strSQL.AppendLine("				Case When b.RightID = 'C' Then 1 Else 0 End RightC,")
        strSQL.AppendLine("				Case When b.RightID = 'P' Then 1 Else 0 End RightP,")
        strSQL.AppendLine("				Case When b.RightID = 'L' Then 1 Else 0 End RightL,")
        strSQL.AppendLine("				Case When b.RightID = 'F' Then 1 Else 0 End RightF,")
        strSQL.AppendLine("				Case When b.RightID = 'R' Then 1 Else 0 End RightR,")
        strSQL.AppendLine("				Case When b.RightID = 'G' Then 1 Else 0 End RightG,")
        strSQL.AppendLine("				Case When b.RightID = 'B' Then 1 Else 0 End RightB,")
        strSQL.AppendLine("				Case When b.RightID = 'X' Then 1 Else 0 End RightX,")
        strSQL.AppendLine("				Case When b.RightID = 'H' Then 1 Else 0 End RightH,")
        strSQL.AppendLine("				Case When b.RightID = 'J' Then 1 Else 0 End RightJ")
        '20150721 Beatrice modify End
        strSQL.AppendLine("		From SC_Fun a with (nolock) ")
        'strSQL.AppendLine("				inner join SC_GroupFun b with (nolock) on a.FunID = b.FunID")
        strSQL.AppendLine("				inner join SC_GroupFun b with (nolock) on a.FunID = b.FunID and a.SysID = b.SysID")   '20140905 Ann modify
        strSQL.AppendLine("		Where a.FunID in (Select FunID From SC_GroupFun with (nolock) Where GroupID = " & Bsp.Utility.Quote(GroupID) & ")")
        strSQL.AppendLine("		and b.GroupID = " & Bsp.Utility.Quote(GroupID) & ") a")
        strSQL.AppendLine("	Group by a.FunID, a.FunName, a.GroupID) b on a.FunID = b.FunID")
        'If CompRoleID = "0" Then
        '    strSQL.AppendLine("	inner join SC_GroupFun c with (nolock) on b.FunID  = c.FunID and a.FunID = c.FunID ")    '20140905 Ann modify
        '    strSQL.AppendLine("	inner join SC_Company d with (nolock) on c.CompRoleID = d.CompID")  '20140905 Ann modify
        '    strSQL.AppendLine("	inner join SC_Group e with (nolock) on c.GroupID = e.GroupID and b.GroupID = e.GroupID ")  '20140905 Ann modify
        'Else
        strSQL.AppendLine("	inner join SC_GroupFun c with (nolock) on b.FunID  = c.FunID and a.FunID = c.FunID and c.CompRoleID = " & Bsp.Utility.Quote(CompRoleID))    '20140905 Ann modify
        strSQL.AppendLine("	inner join SC_Company d with (nolock) on c.CompRoleID = d.CompID")  '20140905 Ann modify
        strSQL.AppendLine("	inner join SC_Group e with (nolock) on c.GroupID = e.GroupID and b.GroupID = e.GroupID and e.CompRoleID = " & Bsp.Utility.Quote(CompRoleID))  '20140905 Ann modify
        'End If
        strSQL.AppendLine("Order by a.FunID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function
#End Region

#Region "SC0505：權限複製"
    Public Function GetGroupFunCopy(ByVal GroupFunType As Bsp.Enums.GroupFunType, ByVal GroupID As String) As DataTable
        Dim strSQL As New StringBuilder

        If GroupFunType = Bsp.Enums.GroupFunType.Group Then
            strSQL.AppendLine("Select a.FunID, a.FunName,")
            'strSQL.AppendLine("Select distinct c.SysID, d.CompName , a.FunID, a.FunName,c.GroupID, e.GroupName,") '20140905 Ann modify
            strSQL.AppendLine("	Case When a.RightA > 0 Then Case When b.RightA > 0 Then 'V' Else '' End Else 'X' End RightA, ")
            strSQL.AppendLine("	Case When a.RightU > 0 Then Case When b.RightU > 0 Then 'V' Else '' End Else 'X' End RightU, ")
            strSQL.AppendLine("	Case When a.RightD > 0 Then Case When b.RightD > 0 Then 'V' Else '' End Else 'X' End RightD, ")
            strSQL.AppendLine("	Case When a.RightI > 0 Then Case When b.RightI > 0 Then 'V' Else '' End Else 'X' End RightI, ")
            strSQL.AppendLine("	Case When a.RightC > 0 Then Case When b.RightC > 0 Then 'V' Else '' End Else 'X' End RightC, ")
            strSQL.AppendLine("	Case When a.RightR > 0 Then Case When b.RightR > 0 Then 'V' Else '' End Else 'X' End RightR, ")
            strSQL.AppendLine("	Case When a.RightP > 0 Then Case When b.RightP > 0 Then 'V' Else '' End Else 'X' End RightP, ")
            strSQL.AppendLine("	Case When a.RightX > 0 Then Case When b.RightX > 0 Then 'V' Else '' End Else 'X' End RightX, ")
            strSQL.AppendLine("	Case When a.RightL > 0 Then Case When b.RightL > 0 Then 'V' Else '' End Else 'X' End RightL ")
            strSQL.AppendLine("From (")
            strSQL.AppendLine("	Select a.FunID, a.FunName, ")
            strSQL.AppendLine("		Case When Sum(RightA) > 0 Then 1 Else 0 End RightA, ")
            strSQL.AppendLine("		Case When Sum(RightU) > 0 Then 1 Else 0 End RightU, ")
            strSQL.AppendLine("		Case When Sum(RightD) > 0 Then 1 Else 0 End RightD, ")
            strSQL.AppendLine("		Case When Sum(RightI) > 0 Then 1 Else 0 End RightI, ")
            strSQL.AppendLine("		Case When Sum(RightC) > 0 Then 1 Else 0 End RightC, ")
            strSQL.AppendLine("		Case When Sum(RightR) > 0 Then 1 Else 0 End RightR, ")
            strSQL.AppendLine("		Case When Sum(RightP) > 0 Then 1 Else 0 End RightP, ")
            strSQL.AppendLine("		Case When Sum(RightX) > 0 Then 1 Else 0 End RightX, ")
            strSQL.AppendLine("		Case When Sum(RightL) > 0 Then 1 Else 0 End RightL ")
            strSQL.AppendLine("	From (")
            strSQL.AppendLine("		Select a.FunID, a.FunName, ")
            strSQL.AppendLine("				Case When b.RightID = 'A' Then 1 Else 0 End RightA,")
            strSQL.AppendLine("				Case When b.RightID = 'U' Then 1 Else 0 End RightU,")
            strSQL.AppendLine("				Case When b.RightID = 'D' Then 1 Else 0 End RightD,")
            strSQL.AppendLine("				Case When b.RightID = 'I' Then 1 Else 0 End RightI,")
            strSQL.AppendLine("				Case When b.RightID = 'C' Then 1 Else 0 End RightC,")
            strSQL.AppendLine("				Case When b.RightID = 'R' Then 1 Else 0 End RightR,")
            strSQL.AppendLine("				Case When b.RightID = 'P' Then 1 Else 0 End RightP,")
            strSQL.AppendLine("				Case When b.RightID = 'X' Then 1 Else 0 End RightX,")
            strSQL.AppendLine("				Case When b.RightID = 'L' Then 1 Else 0 End RightL")
            strSQL.AppendLine("		From SC_Fun a with (nolock)")
            strSQL.AppendLine("				inner join SC_FunRight b with (nolock) on a.FunID = b.FunID")
            'strSQL.AppendLine("				inner join SC_FunRight b with (nolock) on a.FunID = b.FunID and a.SysID = b.SysID")   '20140905 Ann modify
            strSQL.AppendLine("		Where a.FunID in (Select FunID From SC_GroupFun with (nolock) Where GroupID = " & Bsp.Utility.Quote(GroupID) & ")) a")
            strSQL.AppendLine("	Group by a.FunID, a.FunName) a")
            strSQL.AppendLine("	inner join ")
            strSQL.AppendLine("	(Select a.FunID, a.FunName, ")
            strSQL.AppendLine("		Case When Sum(RightA) > 0 Then 1 Else 0 End RightA, ")
            strSQL.AppendLine("		Case When Sum(RightU) > 0 Then 1 Else 0 End RightU, ")
            strSQL.AppendLine("		Case When Sum(RightD) > 0 Then 1 Else 0 End RightD, ")
            strSQL.AppendLine("		Case When Sum(RightI) > 0 Then 1 Else 0 End RightI, ")
            strSQL.AppendLine("		Case When Sum(RightC) > 0 Then 1 Else 0 End RightC, ")
            strSQL.AppendLine("		Case When Sum(RightC) > 0 Then 1 Else 0 End RightR, ")
            strSQL.AppendLine("		Case When Sum(RightP) > 0 Then 1 Else 0 End RightP, ")
            strSQL.AppendLine("		Case When Sum(RightX) > 0 Then 1 Else 0 End RightX, ")
            strSQL.AppendLine("		Case When Sum(RightL) > 0 Then 1 Else 0 End RightL")
            strSQL.AppendLine("	From (")
            strSQL.AppendLine("		Select a.FunID, a.FunName, ")
            strSQL.AppendLine("				Case When b.RightID = 'A' Then 1 Else 0 End RightA,")
            strSQL.AppendLine("				Case When b.RightID = 'U' Then 1 Else 0 End RightU,")
            strSQL.AppendLine("				Case When b.RightID = 'D' Then 1 Else 0 End RightD,")
            strSQL.AppendLine("				Case When b.RightID = 'I' Then 1 Else 0 End RightI,")
            strSQL.AppendLine("				Case When b.RightID = 'C' Then 1 Else 0 End RightC,")
            strSQL.AppendLine("				Case When b.RightID = 'R' Then 1 Else 0 End RightR,")
            strSQL.AppendLine("				Case When b.RightID = 'P' Then 1 Else 0 End RightP,")
            strSQL.AppendLine("				Case When b.RightID = 'X' Then 1 Else 0 End RightX,")
            strSQL.AppendLine("				Case When b.RightID = 'L' Then 1 Else 0 End RightL")
            strSQL.AppendLine("		From SC_Fun a with (nolock) ")
            strSQL.AppendLine("				inner join SC_GroupFun b with (nolock) on a.FunID = b.FunID")
            'strSQL.AppendLine("				inner join SC_GroupFun b with (nolock) on a.FunID = b.FunID and a.SysID = b.SysID")   '20140905 Ann modify
            strSQL.AppendLine("		Where a.FunID in (Select FunID From SC_GroupFun with (nolock) Where GroupID = " & Bsp.Utility.Quote(GroupID) & ")")
            strSQL.AppendLine("		and b.GroupID = " & Bsp.Utility.Quote(GroupID) & ") a")
            strSQL.AppendLine("	Group by a.FunID, a.FunName) b on a.FunID = b.FunID")
            'strSQL.AppendLine("	inner join SC_GroupFun c with (nolock) on b.FunID  = c.FunID and a.FunID = c.FunID")    '20140905 Ann modify
            'strSQL.AppendLine("	inner join SC_Company d with (nolock) on c.CompRoleID = d.CompID")  '20140905 Ann modify
            'strSQL.AppendLine("	inner join SC_Group e with (nolock) on c.GroupID = e.GroupID")  '20140905 Ann modify
            strSQL.AppendLine("Order by a.FunID")
        Else
            'strSQL.AppendLine("Select b.GroupID, c.GroupName, a.FunID, a.FunName,")
            strSQL.AppendLine("Select a.FunID, a.FunName,") '20140905 Ann modify
            strSQL.AppendLine("	Case When a.RightA > 0 Then Case When b.RightA > 0 Then 'V' Else '' End Else 'X' End RightA, ")
            strSQL.AppendLine("	Case When a.RightU > 0 Then Case When b.RightU > 0 Then 'V' Else '' End Else 'X' End RightU, ")
            strSQL.AppendLine("	Case When a.RightD > 0 Then Case When b.RightD > 0 Then 'V' Else '' End Else 'X' End RightD, ")
            strSQL.AppendLine("	Case When a.RightI > 0 Then Case When b.RightI > 0 Then 'V' Else '' End Else 'X' End RightI, ")
            strSQL.AppendLine("	Case When a.RightC > 0 Then Case When b.RightC > 0 Then 'V' Else '' End Else 'X' End RightC, ")
            strSQL.AppendLine("	Case When a.RightR > 0 Then Case When b.RightR > 0 Then 'V' Else '' End Else 'X' End RightR, ")
            strSQL.AppendLine("	Case When a.RightP > 0 Then Case When b.RightP > 0 Then 'V' Else '' End Else 'X' End RightP, ")
            strSQL.AppendLine("	Case When a.RightX > 0 Then Case When b.RightX > 0 Then 'V' Else '' End Else 'X' End RightX, ")
            strSQL.AppendLine("	Case When a.RightL > 0 Then Case When b.RightL > 0 Then 'V' Else '' End Else 'X' End RightL")
            strSQL.AppendLine("From (")
            strSQL.AppendLine("	Select a.FunID, a.FunName, ")
            strSQL.AppendLine("		Case When Sum(RightA) > 0 Then 1 Else 0 End RightA, ")
            strSQL.AppendLine("		Case When Sum(RightU) > 0 Then 1 Else 0 End RightU, ")
            strSQL.AppendLine("		Case When Sum(RightD) > 0 Then 1 Else 0 End RightD, ")
            strSQL.AppendLine("		Case When Sum(RightI) > 0 Then 1 Else 0 End RightI, ")
            strSQL.AppendLine("		Case When Sum(RightC) > 0 Then 1 Else 0 End RightC, ")
            strSQL.AppendLine("		Case When Sum(RightR) > 0 Then 1 Else 0 End RightR, ")
            strSQL.AppendLine("		Case When Sum(RightP) > 0 Then 1 Else 0 End RightP, ")
            strSQL.AppendLine("		Case When Sum(RightX) > 0 Then 1 Else 0 End RightX, ")
            strSQL.AppendLine("		Case When Sum(RightL) > 0 Then 1 Else 0 End RightL ")
            strSQL.AppendLine("	From (")
            strSQL.AppendLine("		Select a.FunID, a.FunName, ")
            strSQL.AppendLine("				Case When b.RightID = 'A' Then 1 Else 0 End RightA,")
            strSQL.AppendLine("				Case When b.RightID = 'U' Then 1 Else 0 End RightU,")
            strSQL.AppendLine("				Case When b.RightID = 'D' Then 1 Else 0 End RightD,")
            strSQL.AppendLine("				Case When b.RightID = 'I' Then 1 Else 0 End RightI,")
            strSQL.AppendLine("				Case When b.RightID = 'C' Then 1 Else 0 End RightC,")
            strSQL.AppendLine("				Case When b.RightID = 'R' Then 1 Else 0 End RightR,")
            strSQL.AppendLine("				Case When b.RightID = 'P' Then 1 Else 0 End RightP,")
            strSQL.AppendLine("				Case When b.RightID = 'X' Then 1 Else 0 End RightX,")
            strSQL.AppendLine("				Case When b.RightID = 'L' Then 1 Else 0 End RightL")
            strSQL.AppendLine("		From SC_Fun a with (nolock) ")
            strSQL.AppendLine("				inner join SC_FunRight b with (nolock) on a.FunID = b.FunID")
            'strSQL.AppendLine("				inner join SC_FunRight b with (nolock) on a.FunID = b.FunID and a.SysID = b.SysID") '20140905 Ann modify
            strSQL.AppendLine("		Where a.FunID = " & Bsp.Utility.Quote(GroupID) & ") a")
            strSQL.AppendLine("	Group by a.FunID, a.FunName) a")
            strSQL.AppendLine("	inner join ")
            strSQL.AppendLine("	(Select a.GroupID, a.FunID, a.FunName, ")
            strSQL.AppendLine("		Case When Sum(RightA) > 0 Then 1 Else 0 End RightA, ")
            strSQL.AppendLine("		Case When Sum(RightU) > 0 Then 1 Else 0 End RightU, ")
            strSQL.AppendLine("		Case When Sum(RightD) > 0 Then 1 Else 0 End RightD, ")
            strSQL.AppendLine("		Case When Sum(RightI) > 0 Then 1 Else 0 End RightI, ")
            strSQL.AppendLine("		Case When Sum(RightC) > 0 Then 1 Else 0 End RightC, ")
            strSQL.AppendLine("		Case When Sum(RightC) > 0 Then 1 Else 0 End RightR, ")
            strSQL.AppendLine("		Case When Sum(RightP) > 0 Then 1 Else 0 End RightP, ")
            strSQL.AppendLine("		Case When Sum(RightX) > 0 Then 1 Else 0 End RightX, ")
            strSQL.AppendLine("		Case When Sum(RightL) > 0 Then 1 Else 0 End RightL")
            strSQL.AppendLine("	From (")
            strSQL.AppendLine("		Select b.GroupID, a.FunID, a.FunName, ")
            strSQL.AppendLine("				Case When b.RightID = 'A' Then 1 Else 0 End RightA,")
            strSQL.AppendLine("				Case When b.RightID = 'U' Then 1 Else 0 End RightU,")
            strSQL.AppendLine("				Case When b.RightID = 'D' Then 1 Else 0 End RightD,")
            strSQL.AppendLine("				Case When b.RightID = 'I' Then 1 Else 0 End RightI,")
            strSQL.AppendLine("				Case When b.RightID = 'C' Then 1 Else 0 End RightC,")
            strSQL.AppendLine("				Case When b.RightID = 'R' Then 1 Else 0 End RightR,")
            strSQL.AppendLine("				Case When b.RightID = 'P' Then 1 Else 0 End RightP,")
            strSQL.AppendLine("				Case When b.RightID = 'X' Then 1 Else 0 End RightX,")
            strSQL.AppendLine("				Case When b.RightID = 'L' Then 1 Else 0 End RightL")
            strSQL.AppendLine("		From SC_Fun a with (nolock) ")
            strSQL.AppendLine("				inner join SC_GroupFun b with (nolock) on a.FunID = b.FunID ")
            'strSQL.AppendLine("				inner join SC_GroupFun b with (nolock) on a.FunID = b.FunID and a.SysID = b.SysID") '20140905 Ann modify
            strSQL.AppendLine("		Where a.FunID = " & Bsp.Utility.Quote(GroupID) & ") a")
            'strSQL.AppendLine("	Group by a.GroupID, a.FunID, a.FunName) b on a.FunID = b.FunID")
            'strSQL.AppendLine("     inner join SC_Group c with (nolock) on b.GroupID = c.GroupID")  '20140905 Ann add
            'strSQL.AppendLine("     inner join SC_Company d with (nolock) on c.CompRoleID = d.CompID ")  '20140905 Ann add
            strSQL.AppendLine("Order by b.GroupID")
        End If

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function
#End Region

#Region "SC02A0-SC_Agency：代理人設定"
    ''' <summary>
    ''' 取得代理人資訊
    ''' </summary>
    ''' <param name="UserID">被代理人</param>
    ''' <param name="AgentUserID">目前登入實際使用者</param>
    ''' <param name="FieldNames">查詢的欄位名稱, 用逗號隔開</param>
    ''' <param name="CondStr">額外條件, 也可加入Order by資訊</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAgency(ByVal UserID As String, ByVal AgentUserID As String, Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder

        strSQL.AppendLine("Select " & FieldNames)
        strSQL.AppendLine("From SC_Agency")
        strSQL.AppendLine("Where UserID = " & Bsp.Utility.Quote(UserID))
        If AgentUserID <> "" Then
            strSQL.AppendLine("And AgentUserID = " & Bsp.Utility.Quote(AgentUserID))
        End If

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    Public Function AddAgency(ByVal beAgency As beSC_Agency.Row) As Boolean
        Dim bsAgency As New beSC_Agency.Service()

        Return IIf(bsAgency.Insert(beAgency) > 0, True, False)
    End Function

    Public Function DeleteAgency(ByVal beAgency As beSC_Agency.Row) As Boolean
        Dim bsAgency As New beSC_Agency.Service()

        Return IIf(bsAgency.DeleteRowByPrimaryKey(beAgency) > 0, True, False)
    End Function

    Public Function IsAgencyExists(ByVal beAgency As beSC_Agency.Row) As Boolean
        Dim bsAgency As New beSC_Agency.Service()

        Return bsAgency.IsDataExists(beAgency)
    End Function

    Public Function UpdateAgency(ByVal beAgency As beSC_Agency.Row) As Boolean
        Dim bsAgency As New beSC_Agency.Service()

        Return IIf(bsAgency.Update(beAgency) > 0, True, False)
    End Function

#End Region

#Region "SC02B0 : 代理人作業"
    Public Function AddAgencyExecuteLog(ByVal beAgencyExecuteLog As beSC_AgencyExecuteLog.Row) As Boolean
        Dim bsAgencyExecuteLog As New beSC_AgencyExecuteLog.Service()

        Return IIf(bsAgencyExecuteLog.Insert(beAgencyExecuteLog) > 0, True, False)
    End Function

    Public Function GetSC02B0QueryString(ByVal AgentUserID As String) As String
        Dim strSQL As New StringBuilder

        strSQL.AppendLine("Select *, dbo.funGetAOrgDefine('3', UserID) UserName")
        strSQL.AppendLine("From SC_Agency Where AgentUserID = " & Bsp.Utility.Quote(AgentUserID))
        strSQL.AppendLine("And Convert(varchar(10), getdate(), 111) between ValidFrom And ValidTo ")
        strSQL.AppendLine("And ValidFlag = '1' ")
        strSQL.AppendLine("Order by UserID")

        Return strSQL.ToString()
    End Function
#End Region

#Region "SC02C0 : 代理人維護"
    Public Function GetSC02C0QueryString(ByVal UserID As String) As String
        Dim strSQL As New StringBuilder

        strSQL.AppendLine("Select *, dbo.funGetAOrgDefine('3', AgentUserID) AgentUserName, dbo.funGetAOrgDefine('3', UserID) UserName")
        strSQL.AppendLine("From SC_Agency Where UserID = " & Bsp.Utility.Quote(UserID))
        strSQL.AppendLine("Order by AgentUserID")

        Return strSQL.ToString()
    End Function
#End Region

#Region "SC0300-SC_Billboard：公佈欄維護"
    Public Function GetBillboardInfo(ByVal Kind As String, ByVal Seq As String, Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder

        If FieldNames.Trim() = "" Then FieldNames = "*"

        strSQL.AppendLine("Select " & FieldNames)
        strSQL.AppendLine("From SC_Billboard Where 1 = 1")
        If Kind.Trim() <> "" Then strSQL.AppendLine("And Kind = " & Bsp.Utility.Quote(Kind.Trim()))
        If Seq.Trim() <> "" Then strSQL.AppendLine("And Seq = " & Bsp.Utility.Quote(Seq))
        If CondStr.Trim() <> "" Then strSQL.AppendLine(CondStr)

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    Public Function AddBillboard(ByVal beBillboard As beSC_Billboard.Row) As Boolean
        Dim bsBillboard As New beSC_Billboard.Service()

        If beBillboard.FileName.Value <> "" Then
            Dim strBillboardPath As String = HttpContext.Current.Server.MapPath(Bsp.MySettings.BillboardPath)
            Dim strTmpPath As String = HttpContext.Current.Server.MapPath(Bsp.MySettings.TempPath)

            If System.IO.File.Exists(strTmpPath & "\" & beBillboard.FileName.Value) Then
                System.IO.File.Move(strTmpPath & "\" & beBillboard.FileName.Value, strBillboardPath & "\" & beBillboard.FileName.Value)
            End If
        End If

        '取得Seq
        Dim intSeq As Integer = CInt(Bsp.DB.ExecuteScalar("Select Isnull(Max(Seq), 0) From SC_Billboard Where Kind = " & Bsp.Utility.Quote(beBillboard.Kind.Value)))

        beBillboard.Seq.Value = intSeq + 1
        Return IIf(bsBillboard.Insert(beBillboard) > 0, True, False)
    End Function

    Public Function UpdateBillboard(ByVal beBillboard As beSC_Billboard.Row) As Boolean
        Dim bsBillboard As New beSC_Billboard.Service()

        If beBillboard.FileName.Value <> "" Then
            Dim strBillboardPath As String = HttpContext.Current.Server.MapPath(Bsp.MySettings.BillboardPath)
            Dim strTmpPath As String = HttpContext.Current.Server.MapPath(Bsp.MySettings.TempPath)

            If System.IO.File.Exists(strTmpPath & "\" & beBillboard.FileName.Value) Then
                System.IO.File.Move(strTmpPath & "\" & beBillboard.FileName.Value, strBillboardPath & "\" & beBillboard.FileName.Value)
            End If
        End If

        Return IIf(bsBillboard.Update(beBillboard) > 0, True, False)
    End Function

    Public Function DeleteBillboard(ByVal beBillboard As beSC_Billboard.Row) As Boolean
        Dim bsBillboard As New beSC_Billboard.Service()

        Using dt As DataTable = GetBillboardInfo(beBillboard.Kind.Value, beBillboard.Seq.Value.ToString())
            If dt.Rows.Count > 0 Then
                beBillboard = New beSC_Billboard.Row(dt.Rows(0))

                If beBillboard.DetailFlag.Value = "1" Then
                    '有深入說明則刪除檔案
                    Try
                        System.IO.File.Delete(HttpContext.Current.Server.MapPath(Bsp.MySettings.BillboardPath & "\" & beBillboard.FileName.Value))
                    Catch ex As Exception
                        Throw
                    End Try
                End If
            End If
        End Using

        Return IIf(bsBillboard.DeleteRowByPrimaryKey(beBillboard) > 0, True, False)
    End Function

    Public Function QueryBillboard(ByVal Kind As String, ByVal Title As String) As DataTable
        Dim strSQL As New StringBuilder()

        If Title.Trim() <> "" Then strSQL.AppendLine("And Title like " & Bsp.Utility.Quote("%" & Title & "%"))
        strSQL.AppendLine("Order by Kind, Seq")

        Return GetBillboardInfo(Kind, "", "*, dbo.funGetAOrgDefine('3', LastChgID) LastChgNm", strSQL.ToString())
    End Function
#End Region

#Region "SC0310-SC_Calendar：年曆檔維護"
    Public Function GetCalendar(ByVal SysDate As DateTime, ByVal AreaID As String) As DataTable
        Dim bsCalendar As New beSC_Calendar.Service()
        Dim strWhere As New StringBuilder()

        strWhere.AppendLine("Where 1 = 1")
        strWhere.AppendLine("And SysDate = " & Bsp.Utility.Quote(SysDate.ToString("yyyy/MM/dd")))
        If AreaID <> "" Then
            strWhere.AppendLine("And AreaID = " & Bsp.Utility.Quote(AreaID))
        End If

        Return bsCalendar.QuerybyWhere(strWhere.ToString()).Tables(0)
    End Function

    Public Function InsertCalendar(ByVal beCalendar As beSC_Calendar.Row, ByVal tran As DbTransaction) As Boolean
        Dim bsCalendar As New beSC_Calendar.Service()

        Return bsCalendar.Insert(beCalendar, tran)
    End Function

    Public Function UpdateCalendar(ByVal SysDate As DateTime, ByVal AreaID As String, ByVal HolidayOrNot As String) As Boolean
        Dim DbParam() As DbParameter = { _
                Bsp.DB.getDbParameter("@argAreaID", AreaID), _
                Bsp.DB.getDbParameter("@argSysDate", SysDate.ToString("yyyy/MM/dd")), _
                Bsp.DB.getDbParameter("@argHolidayOrNot", HolidayOrNot), _
                Bsp.DB.getDbParameter("@argLastChgID", UserProfile.ActUserID)}

        Try
            Bsp.DB.ExecuteNonQuery(CommandType.StoredProcedure, "SP_SC0310", DbParam)
            Return True
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function DeleteCalendarbyYear(ByVal AreaID As String, ByVal Year As String, ByVal tran As DbTransaction) As Boolean
        Dim strSQL As New StringBuilder

        strSQL.AppendLine("Delete From SC_Calendar")
        strSQL.AppendLine("Where AreaID = " & Bsp.Utility.Quote(AreaID))
        strSQL.AppendLine("And Year(SysDate) = " & Year)

        Return Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)
    End Function

    ''' <summary>
    ''' 從Report匯入年曆檔
    ''' </summary>
    ''' <param name="AreaID"></param>
    ''' <param name="Year"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ImportCalendar(ByVal AreaID As String, ByVal Year As String) As Boolean
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select CAST(TBSDY as Datetime) as SysDate, LOCATION as AreaID, HOLIDAY as HolidayOrNot")
        strSQL.AppendLine("	, WEKDAY as [Week], Cast(NBSDY as Datetime) as NextBusDate, Cast(LBSDY as datetime) as LastBusDate")
        strSQL.AppendLine("	, CAST(NNBSDY as datetime) as NeNeBusDate, CAST(LMNDY AS datetime) as LastEndDate")
        strSQL.AppendLine("	, CAST(TMNDY as datetime) as ThisEndDate, CAST(FNBSDY as datetime) as MonEndDate")
        strSQL.AppendLine("	, NDYCNT as NextDateDiff, NNDCNT as NeNeDateDiff, JDAY as JulianDate")
        strSQL.AppendLine("From lnkREPORT.Report.dbo.Z_TBDAY")
        strSQL.AppendLine("Where Left(TBSDY,4) = " & Year)
        strSQL.AppendLine("And LOCATION = " & Bsp.Utility.Quote(AreaID))

        Using cn As DbConnection = Bsp.DB.getConnection()
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Try
                DeleteCalendarbyYear(AreaID, Year, tran)
                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)
            Catch ex As Exception
                Throw
            End Try
        End Using
    End Function
#End Region

#Region "SC0312：年曆檔下載"
    Public Function GetCalendarByYearAndArea(ByVal Year As String, ByVal AreaID As String) As Boolean
        Try
            'Dim bsCalendar As New SC_Entities.beSC_Calendar.Service()
            Dim sb As New StringBuilder()

            sb.AppendLine("SELECT HOLIDAY ")
            sb.AppendLine("     + WEKDAY ")
            sb.AppendLine("     + TBSDY ")
            sb.AppendLine("     + NBSDY ")
            sb.AppendLine("     + NNBSDY ")
            sb.AppendLine("     + LBSDY ")
            sb.AppendLine("     + LMNDY ")
            sb.AppendLine("     + TMNDY ")
            sb.AppendLine("     + FNBSDY ")
            sb.AppendLine("     + RIGHT('00' + NDYCNT,2) ")
            sb.AppendLine("     + RIGHT('00' + NNDCNT,2) ")
            sb.AppendLine("     + RIGHT('00' + LDYCNT,2) ")
            sb.AppendLine("     + RIGHT('000' + JDAY,3) ")
            sb.AppendLine("FROM ODS_SERVER.ODS2_DBU.dbo.Z_TBDAY ")
            sb.AppendLine("WHERE LEFT(TBSDY,4) = '" & Year & "' ")
            sb.AppendLine("AND LOCATION = '" & AreaID & "' ")

            Dim B As DataSet = Bsp.DB.ExecuteDataSet(CommandType.Text, sb.ToString())

            Dim filePath As String = System.Configuration.ConfigurationManager.AppSettings("JCIC_FilePath").ToString()


            If (System.IO.Directory.Exists(filePath)) Then

            Else
                System.IO.Directory.CreateDirectory(filePath)
            End If

            Dim fileName = filePath + AreaID + "_SDATE_" + Year

            If (System.IO.Directory.Exists(fileName)) Then
                System.IO.Directory.Delete(fileName)
            End If

            Dim sw As System.IO.StreamWriter

            sw = File.CreateText(fileName)

            For i = 0 To B.Tables(0).Rows().Count() - 1 Step 1
                sw.WriteLine(B.Tables(0).Rows(i).Item(0).ToString())
            Next

            sw.Close()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region

#Region "SC0320-SC_Common：代碼維護"
    Public Function GetCommonInfo(ByVal Type As String, ByVal Code As String, Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames.Trim() = "" Then FieldNames = "*"

        strSQL.AppendLine("Select " & FieldNames)
        strSQL.AppendLine("From SC_Common Where 1 = 1")
        If Type.Trim() <> "" Then strSQL.AppendLine("And Type = " & Bsp.Utility.Quote(Type))
        If Code.Trim() <> "" Then strSQL.AppendLine("And Code = " & Bsp.Utility.Quote(Code))
        If CondStr.Trim() <> "" Then strSQL.AppendLine(CondStr)

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    Public Function QueryCommon(ByVal Type As String, ByVal Code As String, ByVal Define As String) As DataTable
        Dim CondStr As New StringBuilder()

        If Type.Trim() <> "" Then CondStr.AppendLine("And Type like " & Bsp.Utility.Quote(Type.Trim() & "%"))
        If Code.Trim() <> "" Then CondStr.AppendLine("And Code like " & Bsp.Utility.Quote(Code.Trim() & "%"))
        If Define.Trim() <> "" Then CondStr.AppendLine("And Define like " & Bsp.Utility.Quote(Define.Trim() & "%"))

        Return GetCommonInfo("", "", "*, Type + '-' + dbo.funFindSCCommonItem('000',Type) TypeName", CondStr.ToString())
    End Function

    ''' <summary>
    ''' 針對代碼類別,檢查是否有子代碼存在
    ''' </summary>
    ''' <param name="Type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function HasChildCommon(ByVal Type As String) As Boolean
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select Count(*) From SC_Common Where Type = " & Bsp.Utility.Quote(Type))

        Return IIf(Bsp.DB.ExecuteScalar(strSQL.ToString()) > 0, True, False)
    End Function

    Public Function DeleteCommon(ByVal beCommon As beSC_Common.Row) As Boolean
        Dim bsCommon As New beSC_Common.Service()

        Return IIf(bsCommon.DeleteRowByPrimaryKey(beCommon) > 0, True, False)
    End Function
#End Region

#Region "SC0330 : 批次匯入設定"
    Public Function AddBatchRule(ByVal beBatchRule As beSC_BatchRule.Row) As Integer
        Dim bsBatchRule As New beSC_BatchRule.Service()

        Return bsBatchRule.Insert(beBatchRule)
    End Function

    Public Function UpdateBatchRule(ByVal beBatchRule As beSC_BatchRule.Row) As Integer
        Dim bsBatchRule As New beSC_BatchRule.Service()

        Return bsBatchRule.Update(beBatchRule)
    End Function

    Public Function DeleteBatchRule(ByVal beBatchRule As beSC_BatchRule.Row) As Integer
        Dim bsBatchRule As New beSC_BatchRule.Service()

        Return bsBatchRule.DeleteRowByPrimaryKey(beBatchRule)
    End Function

    Public Function GetBatchRuleQueryString(ByVal ParamArray Args() As String) As String
        Dim strSQL As New StringBuilder()
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Args)

        strSQL.AppendLine("Select *")
        strSQL.AppendLine(" , Case BanMark When '0' Then '有效' When '1' Then '禁用' Else BanMark End BanMarkNm")
        strSQL.AppendLine(" , Case BusinessFlag When '1' Then '業務人員' When '0' Then '非業務人員' Else BusinessFlag End BusinessFlagNm")
        strSQL.AppendLine("From SC_BatchRule")
        strSQL.AppendLine("Where 1=1")

        For Each strKey As String In ht.Keys
            If ht(strKey) <> "" Then
                strSQL.AppendLine("And " & strKey & " like '%" & ht(strKey).ToString.Trim() & "%'")
            End If
        Next

        strSQL.AppendLine("Order by UpdateSeq")
        Return strSQL.ToString()
    End Function

    Public Function GetBatchRule(ByVal ParamArray Args() As Object) As DataTable
        Dim strSQL As New StringBuilder()
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Args)

        strSQL.AppendLine("Select * From SC_BatchRule")
        strSQL.AppendLine("Where 1 = 1")

        For Each strKey As String In ht.Keys
            Select Case strKey
                Case "DeptID", "OrganID", "WorkTypeID"
                    strSQL.AppendLine("And " & strKey & " = " & Bsp.Utility.Quote(ht(strKey)))
                Case "UpdateSeq"
                    strSQL.AppendLine("And " & strKey & " = " & ht(strKey).ToString())
            End Select
        Next

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    Public Function IsBatchRuleExist(ByVal beBatchRule As beSC_BatchRule.Row) As Boolean
        Dim bsBatchRule As New beSC_BatchRule.Service()

        Return bsBatchRule.IsDataExists(beBatchRule)
    End Function
#End Region

#Region "SC0340 : 查詢部門設定"
    ''' <summary>
    ''' 取得查詢部門設定資料
    ''' </summary>
    ''' <param name="Params"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSC0340Data(ByVal Params As Hashtable) As DataTable
        Dim strSQL As New StringBuilder

        strSQL.AppendLine("Select *, dbo.funGetAOrgDefine('3', a.UserID) UserName, dbo.funGetAOrgDefine('4', a.OrganID) OrganName, Isnull(b.GroupName, '') GroupName")
        strSQL.AppendLine("From SC_DeptQuerySetting a left outer join SC_Group b on a.GroupID = b.GroupID Where 1 = 1")

        For Each s As String In Params.Keys
            Select Case s
                Case "OrganID"
                    If Bsp.Utility.IsStringNull(Params(s)) <> "" Then
                        strSQL.AppendLine("And a.OrganID like " & Bsp.Utility.Quote(Params(s).ToString() & "%"))
                    End If
                Case "UserID"
                    If Bsp.Utility.IsStringNull(Params(s)) <> "" Then
                        strSQL.AppendLine("And a.UserID like " & Bsp.Utility.Quote(Params(s).ToString() & "%"))
                    End If
                Case "GroupID"
                    If Bsp.Utility.IsStringNull(Params(s)) <> "" Then
                        strSQL.AppendLine("And a.GroupID like " & Bsp.Utility.Quote(Params(s).ToString() & "%"))
                    End If
            End Select
        Next
        strSQL.AppendLine("Order by a.OrganID, a.UserID, a.GroupID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    Public Function AddDeptQuerySetting(ByVal beDeptQuerySetting As beSC_DeptQuerySetting.Row) As Boolean
        Dim bsDeptQuerySetting As New beSC_DeptQuerySetting.Service()

        Return IIf(bsDeptQuerySetting.Insert(beDeptQuerySetting) > 0, True, False)
    End Function

    Public Function IsDeptQuerySettingExists(ByVal beDeptQuerySetting As beSC_DeptQuerySetting.Row) As Boolean
        Dim bsDeptQuerySetting As New beSC_DeptQuerySetting.Service()

        Return bsDeptQuerySetting.IsDataExists(beDeptQuerySetting)
    End Function

    Public Function DeleteDeptQuerySetting(ByVal beDeptQuerySetting As beSC_DeptQuerySetting.Row) As Boolean
        Dim bsDeptQuerySetting As New beSC_DeptQuerySetting.Service()

        Return IIf(bsDeptQuerySetting.DeleteRowByPrimaryKey(beDeptQuerySetting) > 0, True, False)
    End Function

    Public Function UpdateDeptQuerySetting(ByVal beDeptQuerySetting As beSC_DeptQuerySetting.Row) As Boolean
        Dim bsDeptQuerySetting As New beSC_DeptQuerySetting.Service()

        Return IIf(bsDeptQuerySetting.Update(beDeptQuerySetting) > 0, True, False)
    End Function

    Public Function GetOrganForQuerySetting() As DataTable
        Dim strSQL As New StringBuilder

        strSQL.AppendLine("Select OrganID, OrganName, OrganID + '-' + OrganName OrganFullName")
        strSQL.AppendLine("From SC_Organization")
        strSQL.AppendLine("Where BusinessFlag = '1'")
        strSQL.AppendLine("And InValidFlag = '0'")
        strSQL.AppendLine("Order by OrganID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    Public Function GetDeptQuerySetting(ByVal OrganID As String, ByVal UserID As String, ByVal GroupID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select * From SC_DeptQuerySetting Where 1 = 1")
        strSQL.AppendLine("And OrganID = " & Bsp.Utility.Quote(OrganID))
        strSQL.AppendLine("And UserID = " & Bsp.Utility.Quote(UserID))
        strSQL.AppendLine("And GroupID = " & Bsp.Utility.Quote(GroupID))
        strSQL.AppendLine("Order by OrganID, UserID, GroupID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function
#End Region

#Region "SC0400 : 流程選單設定"
    Public Function GetFlowMenu(ByVal ParentMenu As String, Optional ByVal FlowMenu As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select * From WF_FlowMenu Where 1 = 1")
        If ParentMenu <> "" Then strSQL.AppendLine("And ParentMenu like N" & Bsp.Utility.Quote("%" & ParentMenu & "%"))
        If FlowMenu <> "" Then strSQL.AppendLine("And MenuName like N" & Bsp.Utility.Quote("%" & FlowMenu & "%"))

        strSQL.AppendLine("Order by ParentMenu, OrderSeq")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    Public Function GetParentFlowMenu() As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select distinct MenuName From WF_FlowMenu")
        strSQL.AppendLine("Where (FunID = '' and LinkPage = '')")
        strSQL.AppendLine("Order by MenuName")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    Public Function UpdateFlowMenu(ByVal beFlowMenu As beWF_FlowMenu.Row, ByVal beFlowMenu_Del As beWF_FlowMenu.Row) As Boolean
        Dim bsFlowMenu As New beWF_FlowMenu.Service()
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Update WF_FlowMenu Set ParentMenu = @NewParentMenu Where ParentMenu = @OldParentMenu")

        Using cn As DbConnection = Bsp.DB.getConnection()
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction()
            Try
                bsFlowMenu.DeleteRowByPrimaryKey(beFlowMenu_Del, tran)
                bsFlowMenu.Insert(beFlowMenu, tran)
                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), New DbParameter() { _
                    Bsp.DB.getDbParameter("@NewParentMenu", beFlowMenu.MenuName.Value), _
                    Bsp.DB.getDbParameter("@OldParentMenu", beFlowMenu_Del.MenuName.Value)}, tran)
                tran.Commit()
            Catch ex As Exception
                tran.Rollback()
                Throw
            Finally
                tran.Dispose()
                cn.Close()
            End Try
        End Using
        Return True
    End Function

    Public Shared Sub FillFun(ByVal objDDL As DropDownList, ByVal FunType As Bsp.Enums.SelectFunctionType, Optional ByVal GroupID As String = "")
        Try
            Using dt As DataTable = GetFunBasicData(FunType, GroupID)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "FullName"
                    .DataValueField = "FunID"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Shared Function GetFunBasicData(ByVal FunType As Bsp.Enums.SelectFunctionType, Optional ByVal GroupID As String = "") As DataTable
        Dim strSQL As New StringBuilder

        Select Case FunType
            Case Bsp.Enums.SelectFunctionType.AllFun

                strSQL.AppendLine("Select a.FunID, a.FunName, a.FunID + '-' + a.FunName FullName")
                strSQL.AppendLine("From SC_Fun a")

            Case Bsp.Enums.SelectFunctionType.ParentFun

                strSQL.AppendLine("Select a.FunID, a.FunName, a.FunID + '-' + a.FunName FullName")
                strSQL.AppendLine("From SC_Fun a")
                strSQL.AppendLine("Where a.Path = ''")
                strSQL.AppendLine("And a.IsMenu = '1'")

            Case Bsp.Enums.SelectFunctionType.AssignToGroup

                If GroupID = "" Then Return Nothing
                strSQL.AppendLine("Select distinct a.FunID, a.FunName, a.FunID + '-' + a.FunName FullName")
                strSQL.AppendLine("From SC_Fun a")
                strSQL.AppendLine("	inner join SC_FunRight b on a.FunID = b.FunID")
                strSQL.AppendLine("Where a.FunID in (Select FunID From SC_GroupFun Where GroupID = " & Bsp.Utility.Quote(GroupID) & ")")

            Case Bsp.Enums.SelectFunctionType.FunHasRight

                strSQL.AppendLine("Select distinct a.FunID, a.FunName, a.FunID + '-' + a.FunName FullName")
                strSQL.AppendLine("From SC_Fun a")
                strSQL.AppendLine("	inner join SC_FunRight b on a.FunID = b.FunID")

            Case Bsp.Enums.SelectFunctionType.NotAssignToGroup

                If GroupID = "" Then Return Nothing
                strSQL.AppendLine("Select distinct a.FunID, a.FunName, a.FunID + '-' + a.FunName FullName")
                strSQL.AppendLine("From SC_Fun a")
                strSQL.AppendLine("	inner join SC_FunRight b on a.FunID = b.FunID")
                strSQL.AppendLine("Where a.FunID not in (Select FunID From SC_GroupFun Where GroupID = " & Bsp.Utility.Quote(GroupID) & ")")
                strSQL.AppendLine("And a.CheckRight = '1'")

            Case Else
                Return Nothing
        End Select
        strSQL.AppendLine("Order by a.FunID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function
#End Region

#Region "SC0410 : 流程關卡設定"
    Public Function GetFlowStepM(ByVal Params As Hashtable) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select *, FlowID + '-' + dbo.funFindSCCommonItem('003', FlowID) FlowIDNm From WF_FlowStepM Where 1 = 1 ")

        For Each strKey As String In Params.Keys
            Select Case strKey
                Case "FlowID"
                    If Params(strKey).ToString() <> "" Then strSQL.AppendLine("And FlowID = " & Bsp.Utility.Quote(Params(strKey).ToString()))
                Case "FlowStepID"
                    If Params(strKey).ToString() <> "" Then strSQL.AppendLine("And FlowStepID like " & Bsp.Utility.Quote(Params(strKey).ToString() & "%"))
                Case "Description"
                    If Params(strKey).ToString() <> "" Then strSQL.AppendLine("And Description like " & Bsp.Utility.Quote(Params(strKey).ToString() & "%"))
            End Select
        Next
        strSQL.AppendLine("Order by FlowID")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    Public Function GetFlowStepM(ByVal FlowID As String, ByVal FlowVer As String, ByVal FlowStepID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select *, FlowID + '-' + dbo.funFindSCCommonItem('003', FlowID) FlowIDNm From WF_FlowStepM Where 1 = 1")
        strSQL.AppendLine("And FlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("And FlowVer = " & FlowVer)
        strSQL.AppendLine("And FlowStepID = " & Bsp.Utility.Quote(FlowStepID))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    Public Function GetFlowStepD(ByVal FlowID As String, ByVal FlowVer As String, ByVal FlowStepID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select *, DefaultUserGroup + '-' + dbo.funFindSCCommonItem('004', DefaultUserGroup) DefaultUserGroupNm From WF_FlowStepD Where 1 = 1")
        strSQL.AppendLine("And FlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("And FlowVer = " & FlowVer)
        strSQL.AppendLine("And FlowStepID = " & Bsp.Utility.Quote(FlowStepID))
        strSQL.AppendLine("Order by SeqNo")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    Public Function AddFlowStep(ByVal beFlowStepM As beWF_FlowStepM.Row, ByVal beFlowStepD() As beWF_FlowStepD.Row) As Boolean
        Dim bsFlowStepM As New beWF_FlowStepM.Service()
        Dim bsFlowStepD As New beWF_FlowStepD.Service()
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Delete From WF_FlowStepD Where FlowID = @FlowID and FlowVer = @FlowVer and FlowStepID = @FlowStepID")

        Using cn As DbConnection = Bsp.DB.getConnection()
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction()
            Try
                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), New DbParameter() { _
                    Bsp.DB.getDbParameter("@FlowID", beFlowStepM.FlowID.Value), _
                    Bsp.DB.getDbParameter("@FlowVer", beFlowStepM.FlowVer.Value), _
                    Bsp.DB.getDbParameter("@FlowStepID", beFlowStepM.FlowStepID.Value)}, tran)

                If beFlowStepD IsNot Nothing Then bsFlowStepD.Insert(beFlowStepD, tran)
                bsFlowStepM.Insert(beFlowStepM, tran)
                tran.Commit()
            Catch ex As Exception
                tran.Rollback()
                Throw
            Finally
                tran.Dispose()
                cn.Close()
            End Try
        End Using
    End Function

    Public Function DeleteFlowStep(ByVal beFlowStepM As beWF_FlowStepM.Row) As Boolean
        Dim bsFlowStepM As New beWF_FlowStepM.Service()
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Delete From WF_FlowStepD")
        strSQL.AppendLine("Where FlowID = " & Bsp.Utility.Quote(beFlowStepM.FlowID.Value))
        strSQL.AppendLine("And FlowVer = " & beFlowStepM.FlowVer.Value.ToString())
        strSQL.AppendLine("And FlowStepID = " & Bsp.Utility.Quote(beFlowStepM.FlowStepID.Value))

        Using cn As DbConnection = Bsp.DB.getConnection()
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction()
            Try
                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)
                bsFlowStepM.DeleteRowByPrimaryKey(beFlowStepM)
                tran.Commit()
            Catch ex As Exception
                tran.Rollback()
                Throw
            Finally
                tran.Dispose()
                cn.Close()
            End Try
        End Using
    End Function

    Public Function UpdateFlowStep(ByVal beFlowStepM As beWF_FlowStepM.Row, ByVal beFlowStepD() As beWF_FlowStepD.Row) As Boolean
        Dim bsFlowStepM As New beWF_FlowStepM.Service()
        Dim bsFlowStepD As New beWF_FlowStepD.Service()
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Delete From WF_FlowStepD Where FlowID = @FlowID and FlowVer = @FlowVer and FlowStepID = @FlowStepID")

        Using cn As DbConnection = Bsp.DB.getConnection()
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction()
            Try
                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), New DbParameter() { _
                    Bsp.DB.getDbParameter("@FlowID", beFlowStepM.FlowID.Value), _
                    Bsp.DB.getDbParameter("@FlowVer", beFlowStepM.FlowVer.Value), _
                    Bsp.DB.getDbParameter("@FlowStepID", beFlowStepM.FlowStepID.Value)}, tran)

                If beFlowStepD IsNot Nothing Then bsFlowStepD.Insert(beFlowStepD, tran)
                bsFlowStepM.Update(beFlowStepM, tran)
                tran.Commit()
            Catch ex As Exception
                tran.Rollback()
                Throw
            Finally
                tran.Dispose()
                cn.Close()
            End Try
        End Using
    End Function
#End Region

#Region "SC0420 : 訊息定義檔"
    Public Function GetMsgDefineData(ByVal Params As Hashtable, Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select *, ")
        strSQL.AppendLine(" dbo.funFindSCCommonItem('02', MsgKind) MsgKindNm,")
        strSQL.AppendLine(" dbo.funFindSCCommonItem('03', OpenFlag) OpenFlagNm,")
        strSQL.AppendLine(" Case DelKind When 'Y' Then '開啟時刪除' When 'N' Then '開啟時不刪除' End DelKindNm")
        strSQL.AppendLine("From WF_MsgDefine")
        strSQL.AppendLine("Where 1 = 1")

        For Each strKey As String In Params.Keys
            Select Case strKey
                Case "MsgCode"
                    If Params(strKey).ToString() <> "" Then strSQL.AppendLine("And MsgCode like " & Bsp.Utility.Quote(Params(strKey).ToString()))
                Case "MsgKind"
                    If Params(strKey).ToString() <> "" Then strSQL.AppendLine("And MsgKind = " & Bsp.Utility.Quote(Params(strKey)))
            End Select
        Next
        If CondStr <> "" Then
            strSQL.AppendLine(CondStr)
        End If
        strSQL.AppendLine("Order by MsgCode")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    Public Function GetMsgDefineData(ByVal MsgCode As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select * From WF_MsgDefine")
        strSQL.AppendLine("Where MsgCode = " & Bsp.Utility.Quote(MsgCode))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function
#End Region

    '20140808 wei add 取得授權公司
#Region "GetChildFunction:取得授權公司"
    Public Function GetCompRoleID(ByVal UserID As String, ByVal SysID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select distinct a.CompRoleID, Case When a.CompRoleID='ALL' Then 'ALL-全金控' Else a.CompRoleID + '-' + b.CompName End as CompName")
        strSQL.AppendLine(",Case when a.CompRoleID='ALL' Then '9' + a.CompRoleID  Else '0' + a.CompRoleID End as SortOrder")
        strSQL.AppendLine("From SC_UserGroup a with (nolock) ")
        strSQL.AppendLine("	Left join SC_Company b with (nolock) on a.CompRoleID = b.CompID")
        strSQL.AppendLine("Where a.UserID = " & Bsp.Utility.Quote(UserID))
        strSQL.AppendLine("And a.SysID = " & Bsp.Utility.Quote(SysID))
        strSQL.AppendLine("Order by SortOrder")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
 	End Function
#End Region

#Region "GetFunctionByCompRoleID:取得目前授權公司是不有選擇功能" '20150319 wei add
    Public Function GetFunctionByCompRoleID(ByVal UserID As String, ByVal SysID As String, ByVal CompRoleID As String, ByVal FunID As String) As String
        Dim strSQL As New StringBuilder

        strSQL.AppendLine("Select c.Path")
        strSQL.AppendLine("From SC_UserGroup a with (nolock) ")
        strSQL.AppendLine("	inner join SC_GroupFun b with (nolock) on a.GroupID = b.GroupID and a.SysID=b.SysID and a.CompRoleID=b.CompRoleID")
        strSQL.AppendLine("	inner join SC_Fun c on b.FunID = c.FunID and b.SysID=c.SysID")
        strSQL.AppendLine("Where c.IsMenu = '1'")
        strSQL.AppendLine("And a.UserID = " & Bsp.Utility.Quote(UserID))
        strSQL.AppendLine("And a.SysID = " & Bsp.Utility.Quote(SysID))    '20140808 wei add
        strSQL.AppendLine("And a.CompRoleID = " & Bsp.Utility.Quote(CompRoleID))    '20140808 wei add
        strSQL.AppendLine("And c.FunID = " & Bsp.Utility.Quote(FunID))

        Return Bsp.DB.ExecuteScalar(strSQL.ToString())
    End Function
#End Region

#Region "GetChildFunction:取得子功能項目"
    Public Function GetAllFunction(ByVal UserID As String, ByVal SysID As String, ByVal CompRoleID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select c.FunID, c.FunName, c.Path, c.OrderSeq, c.ParentFormID, c.LevelNo")
        strSQL.AppendLine("From SC_UserGroup a with (nolock) ")
        strSQL.AppendLine("	inner join SC_GroupFun b with (nolock) on a.GroupID = b.GroupID and a.SysID=b.SysID and a.CompRoleID=b.CompRoleID")
        strSQL.AppendLine("	inner join SC_Fun c on b.FunID = c.FunID and b.SysID=c.SysID")
        strSQL.AppendLine("Where c.IsMenu = '1'")
        strSQL.AppendLine("And a.UserID = " & Bsp.Utility.Quote(UserID))
        strSQL.AppendLine("And a.SysID = " & Bsp.Utility.Quote(SysID))    '20140808 wei add
        strSQL.AppendLine("And a.CompRoleID = " & Bsp.Utility.Quote(CompRoleID))    '20140808 wei add
        strSQL.AppendLine("And Not Exists (Select FunID From SC_GroupDisableFun with (nolock) Where GroupID = a.GroupID and FunID = b.FunID)")
        strSQL.AppendLine("Union")
        strSQL.AppendLine("Select distinct a.FunID, a.FunName, a.Path, a.OrderSeq, a.ParentFormID, a.LevelNo")
        strSQL.AppendLine("From SC_Fun a with (nolock)")
        strSQL.AppendLine("Where a.IsMenu = '1'")
        strSQL.AppendLine("And a.CheckRight = '0'")
        strSQL.AppendLine("And a.SysID = " & Bsp.Utility.Quote(SysID))    '20140808 wei add
        strSQL.AppendLine("Order by ParentFormID, OrderSeq")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    Public Function GetChildFunction(ByVal UserID As String, ByVal ParentFunID As String) As DataSet
        Dim strSQL As New StringBuilder

        strSQL.AppendLine("Select distinct c.FunID, c.FunName, c.FunEngName, c.Path, c.OrderSeq")
        strSQL.AppendLine("From SC_UserGroup a ")
        strSQL.AppendLine("     inner join SC_GroupFun b on a.GroupID = b.GroupID")
        strSQL.AppendLine("     inner join SC_Fun c on b.FunID = c.FunID")
        strSQL.AppendLine("Where c.IsMenu = '1'")
        strSQL.AppendLine("And a.UserID = " & Bsp.Utility.Quote(UserID))
        strSQL.AppendLine("And c.ParentFormID = " & Bsp.Utility.Quote(ParentFunID))
        strSQL.AppendLine("And b.FunID not in (")
        strSQL.AppendLine("     Select b.FunID ")
        strSQL.AppendLine("     From SC_UserGroup a inner join SC_GroupDisableFun b on a.GroupID = b.GroupID")
        strSQL.AppendLine("     Where a.UserID = " & Bsp.Utility.Quote(UserID) & ")")
        strSQL.AppendLine("union")
        strSQL.AppendLine("Select distinct a.FunID, a.FunName, a.FunEngName, a.Path, a.OrderSeq")
        strSQL.AppendLine("From SC_Fun a ")
        strSQL.AppendLine("Where a.IsMenu = '1'")
        strSQL.AppendLine("And a.CheckRight = '0'")
        strSQL.AppendLine("And a.ParentFormID = " & Bsp.Utility.Quote(ParentFunID))
        strSQL.AppendLine("Order by OrderSeq")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString())
    End Function

    Public Function HasChildFun(ByVal UserID As String, ByVal ParentFunID As String) As Boolean
        Dim strSQL As New StringBuilder

        strSQL.AppendLine("Select Count(*) From (")
        strSQL.AppendLine("Select distinct c.FunID")
        strSQL.AppendLine("From SC_UserGroup a ")
        strSQL.AppendLine("     inner join SC_GroupFun b on a.GroupID = b.GroupID")
        strSQL.AppendLine("     inner join SC_Fun c on b.FunID = c.FunID")
        strSQL.AppendLine("Where c.IsMenu = '1'")
        strSQL.AppendLine("And a.UserID = " & Bsp.Utility.Quote(UserID))
        strSQL.AppendLine("And c.ParentFormID = " & Bsp.Utility.Quote(ParentFunID))
        strSQL.AppendLine("And b.FunID not in (")
        strSQL.AppendLine("     Select b.FunID ")
        strSQL.AppendLine("     From SC_UserGroup a inner join SC_GroupDisableFun b on a.GroupID = b.GroupID")
        strSQL.AppendLine("     Where a.UserID = " & Bsp.Utility.Quote(UserID) & ")")
        strSQL.AppendLine("union")
        strSQL.AppendLine("Select distinct a.FunID")
        strSQL.AppendLine("From SC_Fun a ")
        strSQL.AppendLine("Where a.IsMenu = '1'")
        strSQL.AppendLine("And a.CheckRight = '0'")
        strSQL.AppendLine("And a.ParentFormID = " & Bsp.Utility.Quote(ParentFunID) & ") a")

        Return IIf(Bsp.DB.ExecuteScalar(strSQL.ToString()) > 0, True, False)
    End Function
#End Region

#Region "changePassword:修改Password"
    Public Function ChangePassword(ByVal UserID As String, ByVal Password As String, ByVal NewPassword As String) As Integer
        Dim db As Database = DatabaseFactory.CreateDatabase()
        Dim dbCommand As DbCommand = db.GetStoredProcCommand("SP_ChangePassword")
        Dim intResult As String

        db.AddInParameter(dbCommand, "@argUserID", DbType.String, UserID)
        db.AddInParameter(dbCommand, "@argOldPwd", DbType.String, IIf(Password = "NEWUSER", "NEWUSER", Bsp.Utility.passwordEncrypt(Password)))
        db.AddInParameter(dbCommand, "@argNewPwd", DbType.String, Bsp.Utility.passwordEncrypt(NewPassword))

        db.AddOutParameter(dbCommand, "@intResult", DbType.Int32, 4)

        db.ExecuteNonQuery(dbCommand)

        intResult = db.GetParameterValue(dbCommand, "@intResult")

        Return intResult
    End Function
#End Region

#Region "changePassword:修改Password"
    Public Function ChangePassword_SC0800(ByVal CompID As String, ByVal UserID As String, ByVal Password As String, ByVal NewPassword As String) As Integer
        Dim db As Database = DatabaseFactory.CreateDatabase()
        Dim dbCommand As DbCommand = db.GetStoredProcCommand("SP_ChangePassword_SC0800")
        Dim intResult As String

        db.AddInParameter(dbCommand, "@argCompID", DbType.String, CompID)   '20150515 Ann modify 增加公司別判斷
        db.AddInParameter(dbCommand, "@argUserID", DbType.String, UserID)
        db.AddInParameter(dbCommand, "@argOldPwd", DbType.String, IIf(Password = "NEWUSER", "NEWUSER", Bsp.Utility.passwordEncrypt(Password)))
        db.AddInParameter(dbCommand, "@argNewPwd", DbType.String, Bsp.Utility.passwordEncrypt(NewPassword))

        db.AddOutParameter(dbCommand, "@intResult", DbType.Int32, 4)

        db.ExecuteNonQuery(dbCommand)

        intResult = db.GetParameterValue(dbCommand, "@intResult")

        Return intResult
    End Function
#End Region

#Region "checkLogin:Login檢查"
    Public Function LoginCheck(ByVal UserID As String, ByVal Password As String, ByVal IP As String) As Integer
        Dim db As Database = DatabaseFactory.CreateDatabase()
        Dim dbCommand As DbCommand = db.GetStoredProcCommand("SP_LoginCheck")
        Dim LogResult As Integer

        db.AddInParameter(dbCommand, "@argUserID", DbType.String, UserID)
        db.AddInParameter(dbCommand, "@argPWD", DbType.String, Bsp.Utility.passwordEncrypt(Password))
        db.AddInParameter(dbCommand, "@argHostName", DbType.String, IP)

        db.AddOutParameter(dbCommand, "@intLoginResult", DbType.Int32, 4)

        db.ExecuteNonQuery(dbCommand)

        LogResult = db.GetParameterValue(dbCommand, "@intLoginResult")

        Return LogResult
    End Function
#End Region

#Region "loginlog：寫入登入Log"
    Public Function WriteLoginLog(ByVal strCompID As String, ByVal strUserID As String, ByVal strUserName As String, ByVal strHost As String, ByVal strSource As String) As Integer '20150717 wei modify 增加CompID
        Dim strSQL As New StringBuilder

        strSQL.AppendLine("Insert into SC_LoginLog")
        strSQL.AppendLine("(CompID,UserID, UserName, LogType, LogDateTime, HostName, Source)")  '20150717 wei modify 增加CompID
        strSQL.AppendLine("Values")
        strSQL.AppendLine("(@CompID,@UserID, @UserName, '0', Getdate(), @HostName, @Source)")   '20150717 wei modify 增加CompID

        Return Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), _
                New DbParameter() { _
                Bsp.DB.getDbParameter("@CompID", strCompID), _
                Bsp.DB.getDbParameter("@UserID", strUserID), _
                Bsp.DB.getDbParameter("@UserName", strUserName), _
                Bsp.DB.getDbParameter("@HostName", strHost), _
                Bsp.DB.getDbParameter("@Source", strSource)})
    End Function
#End Region

#Region "SC0500：群組功能關係維護"
    Public Function GetGroup(ByVal SysID As String, ByVal CompRoleID As String) As DataTable
        Dim strSQL As String

        'If CompRoleID = "0" Then
        '    strSQL = " select distinct GroupID,GroupID + GroupName as GroupName from SC_Group where SysID = " & Bsp.Utility.Quote(SysID)
        '    strSQL = strSQL & " order by GroupName "
        'Else
        strSQL = " select GroupID,GroupID + '-' + GroupName as GroupName from SC_Group where SysID = " & Bsp.Utility.Quote(SysID) & " and CompRoleID = " & Bsp.Utility.Quote(CompRoleID)
        strSQL = strSQL & " order by GroupName "
        'End If

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function
#End Region

    '20140825 Ann add
#Region "SC0700：系統管理者資料維護"
    Public Function QueryAdmin(ByVal ParamArray Params() As String) As DataTable
        Dim strSQL As New StringBuilder()
        Dim strWhere As New StringBuilder()
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)

        'For Each strKey As String In ht.Keys
        '    If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
        '        Select Case strKey
        '            'Case "CompID"
        '            '    strWhere.AppendLine("And R.CompID like " & Bsp.Utility.Quote(ht(strKey).ToString() & "%"))
        '           Case "AdminID"
        '                strSQL.AppendLine("And AdminID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
        '        End Select
        '    End If
        'Next

        strSQL.AppendLine("select S.SysID + ' - ' + S.SysName as SysID from SC_Admin A")
        strSQL.AppendLine("left join SC_Sys S on A.SysID = S.SysID")
        strSQL.AppendLine("Where 1 = 1")
        strSQL.AppendLine("And AdminID = " & UserProfile.ActUserID)
        'If strWhere.ToString() <> "" Then strSQL.AppendLine(strWhere.ToString())
        strSQL.AppendLine("Order by S.SysID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)

    End Function

    Public Function QuerySysByUser(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "UserID"
                        strSQL.AppendLine("And U1.UserID like " & Bsp.Utility.Quote(ht(strKey).ToString() & "%"))
                    Case "UserName"
                        strSQL.AppendLine("And U1.UserName like N" & Bsp.Utility.Quote(ht(strKey).ToString() & "%"))
                End Select
            End If
        Next

        Return Get_SysInforByUser("", "", strSQL.ToString())
    End Function

    Public Function Get_SysInforByUser(ByVal SysID As String, Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames = "" Then FieldNames = "*"

        'strSQL.AppendLine("Select " & FieldNames)
        'strSQL.AppendLine("From SC_Admin")
        'strSQL.AppendLine("Where 1 = 1")

        strSQL.AppendLine("select S.SysID + ' - ' + S.SysName as SysID ,C1.CompName ,U1.UserID + ' - '+ U1.UserName as UserID, C2.CompName as LastChgComp , isnull(U2.UserName,'') as LastChgID  ,A.LastChgDate ,A.SysID ,A.AdminComp ,A.AdminID from SC_Admin A ")
        strSQL.AppendLine("left join SC_Sys S on A.SysID = S.SysID ")
        strSQL.AppendLine("left join SC_User U1 on A.AdminComp = U1.CompID And A.AdminID = U1.UserID") '20150312 Beatrice modify
        strSQL.AppendLine("left join SC_User U2 on A.LastChgComp = U2.CompID And A.LastChgID = U2.UserID") '20150312 Beatrice modify
        strSQL.AppendLine("left join SC_Company C1 on A.AdminComp = C1.CompID and U1.CompID = C1.CompID ")
        strSQL.AppendLine("left join SC_Company C2 on A.LastChgComp = C2.CompID")
        strSQL.AppendLine("where 1=1 and S.SysID = '" & UserProfile.LoginSysID & "'")
        If SysID <> "" Then strSQL.AppendLine("And S.UserID = " & Bsp.Utility.Quote(SysID))
        If CondStr <> "" Then strSQL.AppendLine(CondStr)

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    Public Function AddAdmin(ByVal beAdmin As beSC_Admin.Row) As Boolean
        Dim bsAdmin As New beSC_Admin.Service()

        Using cn As DbConnection = Bsp.DB.getConnection()
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                Dim beSysAdmin As New beSC_Admin.Row()
                Dim bsSysAdmin As New beSC_Admin.Service()

                beSysAdmin.SysID.Value = beAdmin.SysID.Value
                beSysAdmin.AdminComp.Value = beAdmin.AdminComp.Value
                beSysAdmin.AdminID.Value = beAdmin.AdminID.Value
                beSysAdmin.LastChgComp.Value = beAdmin.LastChgComp.Value
                beSysAdmin.LastChgID.Value = beAdmin.LastChgID.Value
                beSysAdmin.LastChgDate.Value = Now
                bsAdmin.Insert(beAdmin, tran)

                tran.Commit()
            Catch ex As Exception
                tran.Rollback()
                Throw
            Finally
                If tran IsNot Nothing Then tran.Dispose()
            End Try
        End Using

        Return True
    End Function
    Public Function DeleteAdmin(ByVal beAdmin As beSC_Admin.Row) As Boolean
        Dim bsAdmin As New beSC_Admin.Service()
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Delete From SC_Admin Where SysID = '" & UserProfile.LoginSysID & "' and AdminComp = " & Bsp.Utility.Quote(beAdmin.AdminComp.Value) & " and AdminID = " & Bsp.Utility.Quote(beAdmin.AdminID.Value))

        Using cn As DbConnection = Bsp.DB.getConnection()
            cn.Open()
            Dim tran As Data.Common.DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                bsAdmin.DeleteRowByPrimaryKey(beAdmin, tran)
                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)

                tran.Commit()
                inTrans = False
            Catch ex As Exception
                If inTrans Then tran.Rollback()
                Throw
            Finally
                tran.Dispose()
                If cn.State = ConnectionState.Open Then cn.Close()
            End Try
        End Using

        Return True
    End Function

#End Region
#Region "SC0800：放行密碼維護"
    Public Function UpdateSC_User(ByVal strCompID As String, ByVal strUserID As String, ByVal strPassword As String _
                                        , ByVal strLastChgCompID As String, ByVal strLastChgID As String _
                                        , ByVal tran As DbTransaction _
                                        , Optional ByVal strSocialInsuranceTotal As String = "") As Boolean

        Dim strSQL As New StringBuilder
        '開啟對稱金鑰
        'strSQL.AppendLine(ConfigurationManager.AppSettings("HRDBDES").ToString())  '20141027 Ann modify
        strSQL.AppendLine(" Update SC_User ")
        strSQL.AppendLine(" set Password = ")
        'strSQL.AppendLine(" EncryptByKey(Key_GUID('HRDBDES'), " & Bsp.Utility.Quote(strPassword) & ")")    '20141027 Ann modify
        strSQL.AppendLine("  '" & Bsp.Utility.passwordEncrypt(strPassword) & "' ")
        strSQL.AppendLine(" ,LastChgComp = ")
        strSQL.AppendLine(" " & Bsp.Utility.Quote(strLastChgCompID))
        strSQL.AppendLine(" ,LastChgID = ")
        strSQL.AppendLine(" " & Bsp.Utility.Quote(strLastChgID))
        strSQL.AppendLine(" ,LastChgDate =")
        strSQL.AppendLine(" getdate()")
        strSQL.AppendLine(" where CompID = ")
        strSQL.AppendLine("  " & Bsp.Utility.Quote(strCompID))
        strSQL.AppendLine(" and UserID = ")
        strSQL.AppendLine(" " & Bsp.Utility.Quote(strUserID))

        Return Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)

    End Function
#End Region

#Region "SC0900：資料授權設定"
#Region "查詢資料授權設定"
    Public Function SC0900_Query(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("SELECT S.TabName")
        strSQL.AppendLine(", Case S.TabName")
        strSQL.AppendLine("WHEN 'HRSupport_Birth' THEN 'HRSupport_Birth-員工生日資料'")
        strSQL.AppendLine("WHEN 'HRSupport_Staff' THEN 'HRSupport_Staff-員工基本資料(STAFF)'")
        strSQL.AppendLine("WHEN 'HRSupport_WorkStatus' THEN 'HRSupport_WorkStatus-員工狀態資料'")
        strSQL.AppendLine("WHEN 'HRSupport_WorkType' THEN 'HRSupport_WorkType-工作性質年資'")
        strSQL.AppendLine("ELSE S.TabName END AS TabNameN")
        strSQL.AppendLine(", S.FldName")
        strSQL.AppendLine(", CASE S.FldName")
        strSQL.AppendLine("WHEN '_SPHOLD' THEN '_SPHOLD-全金控'")
        strSQL.AppendLine("WHEN 'All' THEN 'All-所有公司別'")
        strSQL.AppendLine("WHEN 'Other' THEN 'Other-其他人員'")
        strSQL.AppendLine("WHEN 'SelectOP' THEN 'SelectOP-分行作業類人員'")
        strSQL.AppendLine("ELSE S.FldName + '-' + ISNULL(C.CompName, W.Remark) END AS FldNameN")
        strSQL.AppendLine(", S.Code")
        strSQL.AppendLine(", ISNULL(C1.CompName, '') AS LastChgComp")
        strSQL.AppendLine(", ISNULL(P.NameN, '') AS LastChgID")
        strSQL.AppendLine(", LastChgDate = CASE WHEN CONVERT(CHAR(10), S.LastChgDate, 111) = '1900/01/01' THEN '' ELSE CONVERT(VARCHAR, S.LastChgDate, 120) END")
        strSQL.AppendLine("FROM HRCodeMap S")
        strSQL.AppendLine("LEFT JOIN Company C ON S.FldName = C.CompID")
        strSQL.AppendLine("LEFT JOIN WorkType W ON S.FldName = W.WorkTypeID AND W.CompID = 'SPHBK1' AND W.InValidFlag = '0'")
        strSQL.AppendLine("LEFT JOIN Company C1 ON S.LastChgComp = C1.CompID")
        strSQL.AppendLine("LEFT JOIN Personal P ON S.LastChgID = P.EmpID AND S.LastChgComp = P.CompID")
        strSQL.AppendLine("WHERE TabName LIKE 'HRSupport%'")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "TabName"
                        strSQL.AppendLine("And S.TabName = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "FldName"
                        strSQL.AppendLine("And S.FldName = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "Code"
                        strSQL.AppendLine("And S.Code = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next

        strSQL.AppendLine("ORDER BY S.TabName, S.FldName, S.Code")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "新增資料授權設定"
    Public Function SC0900_Add(ByVal beHRCodeMap() As beHRCodeMap.Row) As Boolean
        Dim bsHRCodeMap As New beHRCodeMap.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try

                If beHRCodeMap.Length = 1 Then
                    If beHRCodeMap(0).FldName.Value = "All" Then
                        Dim strSQL As New StringBuilder()
                        strSQL.AppendLine("DELETE FROM HRCodeMap")
                        strSQL.AppendLine("WHERE TabName = " & Bsp.Utility.Quote(beHRCodeMap(0).TabName.Value))
                        strSQL.AppendLine("AND FldName <> '_SPHOLD'")
                        strSQL.AppendLine("AND Code = " & Bsp.Utility.Quote(beHRCodeMap(0).Code.Value))

                        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran, "eHRMSDB")
                    End If
                End If

                If bsHRCodeMap.Insert(beHRCodeMap, tran) = 0 Then Return False
                tran.Commit()
            Catch ex As Exception
                tran.Rollback()
                Throw
            Finally
                If tran IsNot Nothing Then tran.Dispose()
            End Try
        End Using

        Return True
    End Function
#End Region

#Region "刪除資料授權設定"
    Public Function SC0900_Delete(ByVal beHRCodeMap As beHRCodeMap.Row) As Boolean
        Dim bsHRCodeMap As New beHRCodeMap.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsHRCodeMap.DeleteRowByPrimaryKey(beHRCodeMap, tran) = 0 Then Return False
                tran.Commit()
            Catch ex As Exception
                tran.Rollback()
                Throw
            Finally
                If tran IsNot Nothing Then tran.Dispose()
            End Try
        End Using

        Return True
    End Function
#End Region

#Region "授權功能類別選單"
    Public Function SC0900_FillTabName(ByVal objDDL As DropDownList)
        Try
            Dim strSQL As New StringBuilder()

            strSQL.AppendLine("SELECT DISTINCT TabName, CASE TabName ")
            strSQL.AppendLine("WHEN 'HRSupport_Birth' THEN 'HRSupport_Birth-員工生日資料' ")
            strSQL.AppendLine("WHEN 'HRSupport_Staff' THEN 'HRSupport_Staff-員工基本資料(STAFF)'")
            strSQL.AppendLine("WHEN 'HRSupport_WorkStatus' THEN 'HRSupport_WorkStatus-員工狀態資料'")
            strSQL.AppendLine("WHEN 'HRSupport_WorkType' THEN 'HRSupport_WorkType-工作性質年資'")
            strSQL.AppendLine("ELSE TabName END")
            strSQL.AppendLine("AS Name")
            strSQL.AppendLine("FROM HRCodeMap")
            strSQL.AppendLine("WHERE TabName like 'HRSupport%'")
            strSQL.AppendLine("ORDER BY TabName ")

            Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "eHRMSDB").Tables(0)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "Name"
                    .DataValueField = "TabName"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "授權項目選單"
    Public Function SC0900_FillFldName(ByVal objDDL As DropDownList, ByVal TabName As String)
        Try
            Dim strSQL As New StringBuilder()

            strSQL.AppendLine("SELECT FldName, CASE FldName ")

            If TabName = "HRSupport_WorkType" Then
                strSQL.AppendLine("WHEN 'Other' THEN 'Other-其他人員'")
                strSQL.AppendLine("WHEN 'SelectOP' THEN 'SelectOP-分行作業類人員'")
                strSQL.AppendLine("WHEN '_SPHOLD' THEN '_SPHOLD-全金控'")
                strSQL.AppendLine("WHEN 'SPHBK1' THEN 'SPHBK1-永豐銀行'")
                strSQL.AppendLine("ELSE S.FldName + '-' + ISNULL(W.Remark, '') END AS Name")
            Else
                strSQL.AppendLine("WHEN '_SPHOLD' THEN '_SPHOLD-全金控'")
                strSQL.AppendLine("WHEN 'All' THEN 'All-所有公司別'")
                strSQL.AppendLine("ELSE S.FldName + '-' + ISNULL(C.CompName, '') END AS Name")
            End If
            strSQL.AppendLine("FROM HRCodeMap S")
            strSQL.AppendLine("LEFT JOIN Company C ON S.FldName = C.CompID")
            strSQL.AppendLine("LEFT JOIN WorkType W ON S.FldName = W.WorkTypeID AND W.CompID = 'SPHBK1' AND W.InValidFlag = '0'")
            strSQL.AppendLine("WHERE S.TabName = " & Bsp.Utility.Quote(TabName))
            strSQL.AppendLine("ORDER BY S.FldName ")

            Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "eHRMSDB").Tables(0)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "Name"
                    .DataValueField = "FldName"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
#End Region

#Region "SC1000：綜合查詢授權設定"
#Region "查詢綜合查詢授權設定"
    Public Function SC1000_Query(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select S.CompRoleID")
        strSQL.AppendLine(", CompRoleName = Case When S.CompRoleID = 'ALL' Then '全金控' Else IsNull(C1.CompName, '') End")
        strSQL.AppendLine(", S.GroupID")
        strSQL.AppendLine(", S.GroupName")
        strSQL.AppendLine(", IsNull(G1.Query0, 0) Query0")
        strSQL.AppendLine(", IsNull(G1.Query1, 0) Query1")
        strSQL.AppendLine(", G2.LastChgComp")
        strSQL.AppendLine(", IsNull(C2.CompName, '') AS LastChgCompName")
        strSQL.AppendLine(", G2.LastChgID")
        strSQL.AppendLine(", IsNull(U.UserName, '') AS LastChgName")
        strSQL.AppendLine(", LastChgDate = Case When Convert(Char(10), IsNull(G2.LastChgDate, ''), 111) = '1900/01/01' Then '' Else Convert(Varchar, IsNull(G2.LastChgDate, ''), 120) End")
        strSQL.AppendLine("From SC_Group S")
        strSQL.AppendLine("Join")
        strSQL.AppendLine("(")
        strSQL.AppendLine("  Select B.SysID, B.CompRoleID, B.GroupID")
        strSQL.AppendLine("  , Case When Sum(Query0) > 0 Then 1 Else 0 End Query0")
        strSQL.AppendLine("  , Case When Sum(Query1) > 0 Then 1 Else 0 End Query1")
        strSQL.AppendLine("  From")
        strSQL.AppendLine("  (")
        strSQL.AppendLine("    Select A.SysID, A.CompRoleID, A.GroupID")
        strSQL.AppendLine("    , Case When A.QueryID = '0' Then 1 Else 0 End Query0")
        strSQL.AppendLine("    , Case When A.QueryID = '1' Then 1 Else 0 End Query1")
        strSQL.AppendLine("    From SC_GroupMutiQry A")
        strSQL.AppendLine("  ) B Group by B.SysID, B.CompRoleID, B.GroupID")
        strSQL.AppendLine(") G1 ON G1.SysID = S.SysID And G1.CompRoleID = S.CompRoleID And G1.GroupID = S.GroupID")
        strSQL.AppendLine("Left Join")
        strSQL.AppendLine("(")
        strSQL.AppendLine("  Select SysID, CompRoleID, GroupID, LastChgComp, LastChgID, Max(LastChgDate) LastChgDate")
        strSQL.AppendLine("  From SC_GroupMutiQry ")
        strSQL.AppendLine("  Group By SysID, CompRoleID, GroupID, LastChgComp, LastChgID")
        strSQL.AppendLine(") G2 ON G2.SysID = S.SysID And G2.CompRoleID = S.CompRoleID And G2.GroupID = S.GroupID")
        strSQL.AppendLine("Left Join SC_Company C1 ON C1.CompID = S.CompRoleID")
        strSQL.AppendLine("Left Join SC_Company C2 ON C2.CompID = G2.LastChgComp")
        strSQL.AppendLine("Left Join SC_User U ON U.CompID = G2.LastChgComp And U.UserID = G2.LastChgID")
        strSQL.AppendLine("Where S.SysID = " & Bsp.Utility.Quote(UserProfile.LoginSysID))

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompRoleID"
                        strSQL.AppendLine("And S.CompRoleID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "GroupID"
                        strSQL.AppendLine("And S.GroupID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "QueryFlag"
                        Select Case ht(strKey).ToString()
                            Case "0"
                                strSQL.AppendLine("And G1.Query0 = '1'")
                            Case "1"
                                strSQL.AppendLine("And G1.Query1 = '1'")
                        End Select
                End Select
            End If
        Next

        strSQL.AppendLine("Order By S.CompRoleID, S.GroupID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function
#End Region

#Region "新增綜合查詢授權設定"
    Public Function SC1000_Add(ByVal beSC_GroupMutiQry() As beSC_GroupMutiQry.Row) As Boolean
        Dim bsSC_GroupMutiQry As New beSC_GroupMutiQry.Service()

        Using cn As DbConnection = Bsp.DB.getConnection()
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsSC_GroupMutiQry.Insert(beSC_GroupMutiQry, tran) = 0 Then Return False
                tran.Commit()
            Catch ex As Exception
                tran.Rollback()
                Throw
            Finally
                If tran IsNot Nothing Then tran.Dispose()
            End Try
        End Using

        Return True
    End Function
#End Region

#Region "修改綜合查詢授權設定"
    Public Function SC1000_Update(ByVal beSC_GroupMutiQry() As beSC_GroupMutiQry.Row) As Boolean
        Dim bsSC_GroupMutiQry As New beSC_GroupMutiQry.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection()
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                strSQL.AppendLine("Delete SC_GroupMutiQry")
                strSQL.AppendLine("WHERE SysID = @SysID")
                strSQL.AppendLine("AND CompRoleID = @CompRoleID")
                strSQL.AppendLine("AND GroupID = @GroupID")

                Dim DbParam() As DbParameter = { _
                    Bsp.DB.getDbParameter("@SysID", beSC_GroupMutiQry(0).SysID.Value), _
                    Bsp.DB.getDbParameter("@CompRoleID", beSC_GroupMutiQry(0).CompRoleID.Value), _
                    Bsp.DB.getDbParameter("@GroupID", beSC_GroupMutiQry(0).GroupID.Value)}

                If Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), DbParam, tran) = 0 Then Return False
                If bsSC_GroupMutiQry.Insert(beSC_GroupMutiQry, tran) = 0 Then Return False

                tran.Commit()
            Catch ex As Exception
                tran.Rollback()
                Throw
            Finally
                If tran IsNot Nothing Then tran.Dispose()
            End Try
        End Using

        Return True
    End Function
#End Region

#Region "刪除綜合查詢授權設定"
    Public Function SC1000_Delete(ByVal beSC_GroupMutiQry As beSC_GroupMutiQry.Row) As Boolean
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection()
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                strSQL.AppendLine("Delete SC_GroupMutiQry")
                strSQL.AppendLine("WHERE SysID = @SysID")
                strSQL.AppendLine("AND CompRoleID = @CompRoleID")
                strSQL.AppendLine("AND GroupID = @GroupID")

                Dim DbParam() As DbParameter = { _
                    Bsp.DB.getDbParameter("@SysID", beSC_GroupMutiQry.SysID.Value), _
                    Bsp.DB.getDbParameter("@CompRoleID", beSC_GroupMutiQry.CompRoleID.Value), _
                    Bsp.DB.getDbParameter("@GroupID", beSC_GroupMutiQry.GroupID.Value)}

                If Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), DbParam, tran) = 0 Then Return False
                tran.Commit()
            Catch ex As Exception
                tran.Rollback()
                Throw
            Finally
                If tran IsNot Nothing Then tran.Dispose()
            End Try
        End Using

        Return True
    End Function
#End Region
#End Region

#Region "Company相關"

    Public Function GetCompName(ByVal CompID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select CompName From SC_Company")
        strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(CompID))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    Public Function GetCompanyInfo(ByVal CompID As String, Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder

        If FieldNames = "" Then FieldNames = "*"
        strSQL.AppendLine("Select " & FieldNames)
        strSQL.AppendLine("From SC_Company")
        strSQL.AppendLine("Where 1=1")
        If CompID <> "" Then strSQL.AppendLine("And CompID = " & Bsp.Utility.Quote(CompID))
        If CondStr <> "" Then strSQL.AppendLine(CondStr)
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    '20150312 Beatrice Add
    Public Function GetSC_CompName(ByVal strCompID As String) As String
        Dim strSQL As String
        Dim strCompName As String

        strSQL = "Select CompName from SC_Company where CompID = " & Bsp.Utility.Quote(strCompID)
        If Bsp.DB.ExecuteScalar(strSQL.ToString()) Is Nothing Then
            strCompName = ""
        Else
            strCompName = Bsp.DB.ExecuteScalar(strSQL.ToString())
        End If

        Return strCompName
    End Function
#End Region

    Function GetGroupInfo_0500(p1 As String, list As List(Of String), p3 As String, p4 As String) As DataTable
        Throw New NotImplementedException
    End Function

#Region "Release:放行檢查"
    Public Function ReleaseCheck(ByVal UserID As String, ByVal Password As String, ByVal IP As String) As Integer
        Dim db As Database = DatabaseFactory.CreateDatabase()
        Dim dbCommand As DbCommand = db.GetStoredProcCommand("SP_ReleaseCheck")
        Dim LogResult As Integer

        db.AddInParameter(dbCommand, "@argUserID", DbType.String, UserID)
        db.AddInParameter(dbCommand, "@argPWD", DbType.String, Bsp.Utility.passwordEncrypt(Password))
        db.AddInParameter(dbCommand, "@argHostName", DbType.String, IP)

        db.AddOutParameter(dbCommand, "@intLoginResult", DbType.Int32, 4)

        db.ExecuteNonQuery(dbCommand)

        LogResult = db.GetParameterValue(dbCommand, "@intLoginResult")

        Return LogResult
    End Function
#End Region

#Region "取SC_User的UserName"
    Public Function GetSC_UserName(ByVal CompID As String, ByVal UserID As String) As String
        Dim strSQL As New StringBuilder()
        Dim strUserName As String

        strSQL.AppendLine("Select UserName From SC_User")
        strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine("And UserID = " & Bsp.Utility.Quote(UserID))

        If Bsp.DB.ExecuteScalar(strSQL.ToString()) Is Nothing Then
            strUserName = ""
        Else
            strUserName = Bsp.DB.ExecuteScalar(strSQL.ToString())
        End If

        Return strUserName
    End Function
#End Region

#Region "取SC_User的WorkStatus"
    Public Function GetUserWorkStatus(ByVal CompID As String, ByVal UserID As String) As String
        Dim strSQL As New StringBuilder()
        Dim strWorkStatus As String

        strSQL.AppendLine("Select WorkStatus From SC_User")
        strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine("And UserID = " & Bsp.Utility.Quote(UserID))

        If Bsp.DB.ExecuteScalar(strSQL.ToString()) Is Nothing Then
            strWorkStatus = ""
        Else
            strWorkStatus = Bsp.DB.ExecuteScalar(strSQL.ToString())
        End If

        Return strWorkStatus
    End Function
#End Region

End Class
