'****************************************************
'功能說明：流程相關邏輯
'建立人員：Chung
'建立日期：2013/01/28
'****************************************************
Imports System.Data
Imports System.Data.Common
Imports Bsp
Imports Microsoft.VisualBasic
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports System.Data.SqlClient
Imports System.Text

Public Class WF
#Region "Public : 其他共用"

    Public Shared Function GetFlowInstance(ByVal PaperID As String, ByVal AssignTo As String) As FlowInstance
        Dim strSQL As New StringBuilder()
        Dim fi As New FlowInstance()

        strSQL.AppendLine("Select FlowID, FlowCaseID, FlowLogBatNo, FlowLogID, FlowStepID, FlowKeyValue")
        strSQL.AppendLine("From WF_FlowToDoList")
        strSQL.AppendLine("Where AssignTo = " & Bsp.Utility.Quote(AssignTo))
        strSQL.AppendLine("And PaperID = " & Bsp.Utility.Quote(PaperID))

        Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
            If dt.Rows.Count = 0 Then Throw New Exception(String.Format("[GetFlowInstance]：查無案號[{0}]流程紀錄！", PaperID))
            fi.FlowID = dt.Rows(0).Item("FlowID").ToString()
            fi.FlowCaseID = dt.Rows(0).Item("FlowCaseID").ToString()
            fi.FlowLogBatNo = CInt(dt.Rows(0).Item("FlowLogBatNo"))
            fi.FlowLogID = dt.Rows(0).Item("FlowLogID").ToString()
            fi.FlowStepID = dt.Rows(0).Item("FlowStepID").ToString()
        End Using

        Return fi
    End Function

    Public Shared Function GetFlowInstance(ByVal FlowID As String, ByVal FlowCaseID As String, ByVal AssignTo As String) As FlowInstance
        Dim strSQL As New StringBuilder()
        Dim fi As New FlowInstance()

        strSQL.AppendLine("Select FlowID, FlowCaseID, FlowLogBatNo, FlowLogID, FlowStepID, FlowKeyValue")
        strSQL.AppendLine("From WF_FlowToDoList")
        strSQL.AppendLine("Where AssignTo = " & Bsp.Utility.Quote(AssignTo))
        strSQL.AppendLine("And FlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("And FlowCaseID = " & Bsp.Utility.Quote(FlowCaseID))

        Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
            If dt.Rows.Count = 0 Then Throw New Exception("[GetFlowInstance]：無此流程紀錄！")
            fi.FlowID = dt.Rows(0).Item("FlowID").ToString()
            fi.FlowCaseID = dt.Rows(0).Item("FlowCaseID").ToString()
            fi.FlowLogBatNo = CInt(dt.Rows(0).Item("FlowLogBatNo"))
            fi.FlowLogID = dt.Rows(0).Item("FlowLogID").ToString()
            fi.FlowStepID = dt.Rows(0).Item("FlowStepID").ToString()
        End Using

        Return fi
    End Function

    ''' <summary>
    ''' 取得部門主管
    ''' </summary>
    ''' <param name="DeptID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetDeptBoss(ByVal DeptID As String) As String
        Dim strSQL As String

        strSQL = "Select Boss From SC_Organization with (nolock) Where OrganID = " & Bsp.Utility.Quote(DeptID)
        Return Bsp.Utility.IsStringNull(Bsp.DB.ExecuteScalar(strSQL))
    End Function

    ''' <summary>
    ''' 取得業務主管
    ''' </summary>
    ''' <param name="DeptID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetDeptBusinessBoss(ByVal DeptID As String) As String
        Dim strSQL As String

        strSQL = "Select BusinessBoss From SC_Organization with (nolock) Where OrganID = " & Bsp.Utility.Quote(DeptID)
        Return Bsp.Utility.IsStringNull(Bsp.DB.ExecuteScalar(strSQL))
    End Function

    ''' <summary>
    ''' 取得分行的單位主管及業務主管. DeptIDs若為多個分行請以逗號隔開
    ''' </summary>
    ''' <param name="DeptIDs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetDeptBossAndBussBoss(ByVal DeptIDs As String, Optional ByVal CondStr As String = "") As String
        Dim strSQL As New StringBuilder

        If DeptIDs = "" Then Return ""

        strSQL.AppendLine("Declare @strBoss varchar(500)")
        strSQL.AppendLine("Select @strBoss = Isnull(@strBoss + ',', '') + Boss")
        strSQL.AppendLine("From (")
        strSQL.AppendLine(" Select Boss From SC_Organization with (nolock)")
        strSQL.AppendLine(" Where OrganID in ('" & DeptIDs & "')")
        strSQL.AppendLine(" And Isnull(Boss, '') <> ''")
        strSQL.AppendLine(" Union")
        strSQL.AppendLine(" Select BusinessBoss Boss From SC_Organization with (nolock)")
        strSQL.AppendLine(" Where OrganID in ('" & DeptIDs & "')")
        strSQL.AppendLine(" And Isnull(BusinessBoss, '') <> ''")
        strSQL.AppendLine(" ) a")
        strSQL.AppendLine("Where 1 = 1")
        If CondStr <> "" Then strSQL.AppendLine(CondStr)
        strSQL.AppendLine("Select Isnull(@strBoss, '') Boss")

        Return Bsp.DB.ExecuteScalar(strSQL.ToString())
    End Function

    ''' <summary>
    ''' 取得部門人員
    ''' </summary>
    ''' <param name="DeptIDs">多個部門以逗號隔開</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetDeptUser(ByVal DeptIDs As String, Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        DeptIDs = Bsp.Utility.Quote(DeptIDs).Replace(",", "','")

        strSQL.AppendLine("Select UserID, UserName From SC_User with (nolock)")
        strSQL.AppendLine("Where DeptID in (" & DeptIDs & ")")
        strSQL.AppendLine("And BanMark = '0'")
        If CondStr <> "" Then strSQL.AppendLine(CondStr)
        strSQL.AppendLine("Order by UserID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    ''' <summary>
    ''' 取得科組課人員
    ''' </summary>
    ''' <param name="OrganIDs">多個科組課以逗號隔開</param>
    ''' <param name="CondStr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetOrganUser(ByVal OrganIDs As String, Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        OrganIDs = Bsp.Utility.Quote(OrganIDs).Replace(",", "','")

        strSQL.AppendLine("Select UserID, UserName From SC_User with (nolock)")
        strSQL.AppendLine("Where OrganID in (" & OrganIDs & ")")
        strSQL.AppendLine("And BanMark = '0'")
        If CondStr <> "" Then strSQL.AppendLine(CondStr)
        strSQL.AppendLine("Order by UserID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    ''' <summary>
    ''' 取得群組人員
    ''' </summary>
    ''' <param name="GroupIDs">多個群組以逗號隔開</param>
    ''' <param name="CondStr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetGroupUser(ByVal GroupIDs As String, Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        GroupIDs = Bsp.Utility.Quote(GroupIDs).Replace(",", "','")

        strSQL.AppendLine("Select UserID, UserName")
        strSQL.AppendLine("From SC_User")
        strSQL.AppendLine("Where Exists (Select UserID From SC_UserGroup Where UserID = SC_User.UserID And GroupID in (" & GroupIDs & "))")
        strSQL.AppendLine("And BanMark = '0'")
        If CondStr <> "" Then strSQL.AppendLine(CondStr)
        strSQL.AppendLine("Order by UserID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    ''' <summary>
    ''' 取得群組下的所有人員,目前主要供線審使用
    ''' </summary>
    ''' <param name="GroupID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetGroupUserInfo(ByVal GroupID As String) As String
        Dim strResult As String = ""
        Dim strSQL As New StringBuilder

        strSQL.AppendLine("Select a.UserID, a.UserID + '-' + b.UserName FullName, b.UserName")
        strSQL.AppendLine("From SC_UserGroup a with (nolock)")
        strSQL.AppendLine("     inner join SC_User b with (nolock) on a.UserID = b.UserID")
        strSQL.AppendLine("Where a.GroupID = " & Bsp.Utility.Quote(GroupID))
        strSQL.AppendLine("And b.BanMark = '0'")
        strSQL.AppendLine("Order by a.UserID")

        Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
            If dt.Rows.Count > 0 Then
                For intLoop As Integer = 0 To dt.Rows.Count - 1
                    strResult &= ";" & dt.Rows(intLoop).Item("UserID").ToString()
                Next
                strResult = strResult.Substring(1)
            End If
        End Using
        Return strResult
    End Function

    ''' <summary>
    ''' 取得審閱人員
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowCaseID"></param>
    ''' <param name="tran"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllCheckerID(ByVal FlowID As String, ByVal FlowCaseID As String, ByVal tran As DbTransaction) As String
        Dim strSQL As New StringBuilder
        Dim strBID As String = ""

        strSQL.AppendLine("Declare @strChecker varchar(500)")
        strSQL.AppendLine("Select @strChecker = Isnull(@strChecker + ';', '') + SignUser From CM_Verify")
        strSQL.AppendLine("Where CMNO in (Select PaperID From WF_FlowCase where FlowID = " & Bsp.Utility.Quote(FlowID) & " And FlowCaseID = " & Bsp.Utility.Quote(FlowCaseID) & ")")
        strSQL.AppendLine("And SignStatus = 'N'")
        strSQL.AppendLine("And SignType = 'C'")
        strSQL.AppendLine("And (Isnull(EmailFg, '') = '' or EmailFg = 'N')")
        strSQL.AppendLine("Order by SignUser")
        strSQL.AppendLine("Select Isnull(@strChecker, '')")

        Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), tran).Tables(0)
            strBID = dt.Rows(0).Item(0).ToString()
        End Using

        Return strBID
    End Function

    ''' <summary>
    ''' 取得User的資訊
    ''' </summary>
    ''' <param name="UserID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUserInfo(ByVal UserID As String, Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder

        strSQL.AppendLine("Select a.UserID, a.UserName, a.DeptID, Isnull(b.OrganName, '') DeptName, ")
        strSQL.AppendLine(" a.OrganID, a.RankID, a.EMail, c.OrganName, b.OrganType")
        strSQL.AppendLine("From SC_User a with (nolock)")
        strSQL.AppendLine("     left outer join SC_Organization b with (nolock) on a.DeptID = b.OrganID")
        strSQL.AppendLine("     left outer join SC_Organization c with (nolock) on a.OrganID = c.OrganID")
        strSQL.AppendLine("Where 1 = 1")
        If UserID <> "" Then strSQL.AppendLine("And a.UserID = @UserID")
        If CondStr <> "" Then
            strSQL.AppendLine(CondStr)
            strSQL.AppendLine("Order by a.UserID")
        End If

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), _
            Bsp.DB.getDbParameter("@UserID", UserID)).Tables(0)
    End Function

    ''' <summary>
    ''' 取得部門名稱
    ''' </summary>
    ''' <param name="OrganID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetOrganName(ByVal OrganID As String) As String
        Dim strSQL As String = "Select OrganName From SC_Organization with (nolock) Where OrganID = " & Bsp.Utility.Quote(OrganID)

        Return Bsp.Utility.IsStringNull(Bsp.DB.ExecuteScalar(strSQL))
    End Function
#End Region

#Region "WFA000 : 待辦清單"
    Public Shared Function GetToDoList(ByVal AssignTo As String, Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select Row_Number() over (Order by FromDate) As Seq, *")
        strSQL.AppendLine("From (")
        strSQL.AppendLine("	Select 'F' as FlowType, FlowID + ';' + FlowCaseID + ';' + FlowLogID as FlowKeyStr, ")
        strSQL.AppendLine("		FlowDispatchFlag, FlowStepID + '-' + FlowStepDesc As FlowStepDesc, PaperID as KeyID, FlowShowValue, ")
        strSQL.AppendLine("     dbo.funGetAOrgDefine('3', AssignTo) AssignName, FromBrName, FromUserName, FromDate, FlowID, FlowStepID, 'F' MsgKind")
        strSQL.AppendLine("     From WF_FlowToDoList")
        strSQL.AppendLine("	Where AssignTo = @AssignTo")
        strSQL.AppendLine("	And FlowCaseStatus = 'Open'")
        strSQL.AppendLine(" Union")
        strSQL.AppendLine("	Select 'M' as FlowType, MsgCaseID as FlowKeyStr, ")
        strSQL.AppendLine("     'N' as FlowDispatchFlag, MsgCode + '-' + MsgReason as FlowStepDesc, PaperID as KeyID, MsgNote as FlowShowValue,")
        strSQL.AppendLine("		dbo.funGetAOrgDefine('3', AssignTo) AssignName, dbo.funGetAOrgDefine('4', LastChgDept) FromBrName, dbo.funGetAOrgDefine('3', LastChgID) FromUserName, ")
        strSQL.AppendLine("     LastChgDate As FromDate, MsgCode FlowID, MsgCode FlowStepID, (Select MsgKind From WF_MsgDefine Where MsgCode = WF_ToDoMessage.MsgCode) MsgKind")
        strSQL.AppendLine(" From WF_ToDoMessage")
        strSQL.AppendLine("	Where AssignTo = @AssignTo")
        strSQL.AppendLine("	And Status <> 'D') a Where 1 = 1")
        If CondStr <> "" Then
            strSQL.AppendLine(CondStr)
        Else
            strSQL.AppendLine("Order by FromDate")
        End If

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), Bsp.DB.getDbParameter("@AssignTo", AssignTo)).Tables(0)
    End Function

    Public Shared Function GetToDoListCount(ByVal AssignTo As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select b.Code, Isnull(b.RsvCol1, b.Define) as Define, Count(*) Cnt")
        strSQL.AppendLine("From WF_FlowToDoList a inner join SC_Common b on a.FlowID = b.Code and b.Type = '003'")
        strSQL.AppendLine("Where AssignTo = @AssignTo")
        strSQL.AppendLine("And FlowCaseStatus = 'Open'")
        strSQL.AppendLine("Group by b.Code, Isnull(b.RsvCol1, b.Define)")
        strSQL.AppendLine("Union")
        strSQL.AppendLine("Select b.MsgCode Code, b.MsgReason Define, Count(*) Cnt")
        strSQL.AppendLine("From WF_ToDoMessage a inner join WF_MsgDefine b on a.MsgCode = b.MsgCode")
        strSQL.AppendLine("Where a.Status <> 'D'")
        strSQL.AppendLine("And AssignTo = @AssignTo")
        strSQL.AppendLine("Group by b.MsgCode, b.MsgReason")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), Bsp.DB.getDbParameter("@AssignTo", AssignTo)).Tables(0)
    End Function

    Public Shared Function GetTraceList(ByVal TraceUserID As String, Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select Row_Number() over (Order by FromDate) As Seq, *")
        strSQL.AppendLine("From (")
        strSQL.AppendLine("	Select distinct a.FlowID, a.FlowCaseID, a.FlowLogID, a.FlowDispatchFlag, a.FlowStepID + '-' + a.FlowStepDesc As FlowStepDesc, ")
        strSQL.AppendLine("		a.PaperID as KeyID, a.FlowShowValue, a.FromBrName, a.FromUserName, a.FromDate, dbo.funGetAOrgDefine('3', a.AssignTo) AssignName ")
        strSQL.AppendLine("	From WF_FlowToDoList a inner join WF_TraceList b on a.FlowID = b.FlowID And a.FlowCaseID = b.FlowCaseID")
        strSQL.AppendLine("	Where b.TraceUserID = @TraceUserID")
        strSQL.AppendLine("	And FlowCaseStatus = 'Open') a")
        If CondStr <> "" Then
            strSQL.AppendLine(CondStr)
        Else
            strSQL.AppendLine("Order by FromDate")
        End If

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), Bsp.DB.getDbParameter("@TraceUserID", TraceUserID)).Tables(0)
    End Function

    Public Shared Function GetToDoMessage(ByVal MsgCaseID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select * From WF_ToDoMessage")
        strSQL.AppendLine("Where MsgCaseID = " & Bsp.Utility.Quote(MsgCaseID))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    Public Shared Function GetMsgDefine(ByVal MsgCode As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select * From WF_MsgDefine")
        strSQL.AppendLine("Where MsgCode = " & Bsp.Utility.Quote(MsgCode))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    Public Sub UpdateToDoMessage(ByVal MsgCaseID As String, ByVal Status As String)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Update WF_ToDoMessage Set")
        strSQL.AppendLine(" Status = " & Bsp.Utility.Quote(Status) & ",")
        strSQL.AppendLine(" LastChgID = " & Bsp.Utility.Quote(UserProfile.ActUserID) & ",")
        strSQL.AppendLine(" LastChgDate = Getdate(), ")
        strSQL.AppendLine(" LastChgDept = " & Bsp.Utility.Quote(UserProfile.ActDeptID) & ",")
        strSQL.AppendLine(" LastChgDeptName = " & Bsp.Utility.Quote(UserProfile.ActDeptName))
        strSQL.AppendLine("Where MsgCaseID = " & Bsp.Utility.Quote(MsgCaseID))

        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString())
    End Sub

    '刪除追蹤事項
    Public Sub DeleteTraceList(ByVal FlowID As String, ByVal FlowCaseID As String, ByVal TraceUserID As String)
        Dim strSQL As New StringBuilder
        strSQL.AppendLine("DELETE FROM WF_TraceList ")
        strSQL.AppendLine("WHERE FlowID = @FlowID AND FlowCaseID = @FlowCaseID AND TraceUserID = @TraceUserID")

        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), New DbParameter() { _
            Bsp.DB.getDbParameter("@FlowID", FlowID), _
            Bsp.DB.getDbParameter("@FlowCaseID", FlowCaseID), _
            Bsp.DB.getDbParameter("@TraceUserID", TraceUserID)})
    End Sub
#End Region

#Region "WFA010 : FlowStep"

    Public Function GetFlowStepM(ByVal FlowStepID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select * From WF_FlowStepM with (nolock)")
        strSQL.AppendLine("Where FlowStepID = " & Bsp.Utility.Quote(FlowStepID))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    Public Function GetFlowStepM(ByVal FlowID As String, ByVal FlowStepID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select * From WF_FlowStepM with (nolock)")
        strSQL.AppendLine("Where FlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("And FlowStepID = " & Bsp.Utility.Quote(FlowStepID))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    Public Function GetFlowStepM(ByVal FlowID As String, ByVal FlowStepID As String, ByVal FlowVer As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select * From WF_FlowStepM with (nolock)")
        strSQL.AppendLine("Where FlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("And FlowStepID = " & Bsp.Utility.Quote(FlowStepID))
        strSQL.AppendLine("And FlowVer = " & FlowVer)

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    Public Function GetFlowStepD(ByVal FlowID As String, ByVal FlowStepID As String, ByVal FlowVer As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select * From WF_FlowStepD with (nolock)")
        strSQL.AppendLine("Where FlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("And FlowStepID = " & Bsp.Utility.Quote(FlowStepID))
        strSQL.AppendLine("And FlowVer = " & FlowVer)
        strSQL.AppendLine("Order by SeqNo")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    Public Function GetFlowStepD(ByVal FlowID As String, ByVal FlowStepID As String, ByVal FlowVer As String, ByVal ButtonName As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select * From WF_FlowStepD with (nolock)")
        strSQL.AppendLine("Where FlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("And FlowStepID = " & Bsp.Utility.Quote(FlowStepID))
        strSQL.AppendLine("And FlowVer = " & FlowVer)
        strSQL.AppendLine("And ButtonName = " & Bsp.Utility.Quote(ButtonName))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    Public Function GetFlowChildFunction(ByVal ParentMenu As String) As DataTable
        Dim strSQL As New StringBuilder
        strSQL.AppendLine("SELECT  distinct a.ParentMenu, a.OrderSeq, a.MenuName, ")
        strSQL.AppendLine("	case ISNull( b.FunName,'') when '' then  CASE CHARINDEX('*', ISNULL( a.MenuName, '')) WHEN 0 THEN ISNULL( a.MenuName, '') ELSE LEFT(MenuName, CHARINDEX('*', ISNULL( a.MenuName, '')) - 1) END else b.FunName  end  AS FunShowName, ")
        strSQL.AppendLine("	case ISNull(b.Path,'') when '' then  ISNULL(a.LinkPage, '')  else b.Path  end  AS LinkPage, ")
        strSQL.AppendLine("	ISNULL(a.LinkPara, '') AS LinkPara, ISNULL(a.LinkParaExt, 'N') AS LinkParaExt, ISNULL(a.LinkParaSql, '') AS LinkParaSql, isnull(a.FunID,'') as FunID ")
        strSQL.AppendLine("FROM  WF_FlowMenu a with (nolock) left outer join SC_Fun b with (nolock) on a.FunID = b.FunID")
        strSQL.AppendLine("Left join SC_GroupFun c with (nolock) on b.SysID=c.SysID and b.FunID=c.FunID") '20150421 wei add
        strSQL.AppendLine("Left Join SC_UserGroup d with (nolock) on c.GroupID = d.GroupID and c.SysID=d.SysID and c.CompRoleID=d.CompRoleID")  '20150422 wei add
        strSQL.AppendLine("WHERE a.ParentMenu = " & Bsp.Utility.Quote(ParentMenu))
        strSQL.AppendLine("And c.CompRoleID = " & Bsp.Utility.Quote(UserProfile.SelectCompRoleID))  '20150421 wei add
        strSQL.AppendLine("And d.UserID = " & Bsp.Utility.Quote(UserProfile.ActUserID))  '20150422 wei add
        strSQL.AppendLine("Order by a.OrderSeq")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    ''' <summary>
    ''' 取得WF_FlowStepM的指定欄位值
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowVer"></param>
    ''' <param name="FlowStepID"></param>
    ''' <param name="ColumnName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFlowStepMCol(ByVal FlowID As String, ByVal FlowVer As String, ByVal FlowStepID As String, ByVal ColumnName As String) As Object
        Dim strSQL As String

        strSQL = "Select " & ColumnName & " From WF_FlowStepM with (nolock) Where FlowID = " & Bsp.Utility.Quote(FlowID) & " and FlowVer = " & Bsp.Utility.Quote(FlowVer) & " and FlowStepID = " & Bsp.Utility.Quote(FlowStepID)
        Return Bsp.DB.ExecuteScalar(strSQL)
    End Function

    ''' <summary>
    ''' 已FlowID來取得向下的FlowStep
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <returns></returns>
    ''' <remarks>會扣除StepID結尾為00, 97, 98, 99的Step</remarks>
    Public Function GetFlowStepbyFlowID(ByVal FlowID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select FlowStepID, FlowStepID + '-' + Description AS FlowStepDesc")
        strSQL.AppendLine("From WF_FlowStepM")
        strSQL.AppendLine("Where FlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("And RIGHT(FlowStepID, 2) NOT IN ('00','97', '98', '99') ")
        strSQL.AppendLine("Order by FlowStepID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    ''' <summary>
    ''' 取得客戶基本資料
    ''' </summary>
    ''' <param name="CID"></param>
    ''' <param name="FieldNames"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBR_Customer(ByVal CID As String, Optional ByVal FieldNames As String = "*") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames = "" Then FieldNames = "*"
        strSQL.AppendLine("Select " & FieldNames)
        strSQL.AppendLine("From BR_Customer")
        strSQL.AppendLine("Where CID = " & Bsp.Utility.Quote(CID))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    Private Function GetBR_CustomerE(ByVal AppID As String, ByVal CustomerID As String, Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "", Optional ByVal Tran As DbTransaction = Nothing) As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames = "" Then FieldNames = "*"
        strSQL.AppendLine("Select " & FieldNames)
        strSQL.AppendLine("From BR_Customer")
        strSQL.AppendLine("Where AppID = " & Bsp.Utility.Quote(AppID))
        strSQL.AppendLine("And CustomerID = " & Bsp.Utility.Quote(CustomerID))
        If CondStr <> "" Then strSQL.AppendLine(CondStr)

        If Tran IsNot Nothing Then
            Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), Tran).Tables(0)
        Else
            Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
        End If
    End Function
#End Region

#Region "WFA011 : 流程處理主程式"
    ''' <summary>
    ''' 由已知的FlowID/FlowCaseID來回推目前的FlowStepID,取的WF_FlowStepM的資料
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowCaseID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFlowStepMbyFlowCase(ByVal FlowID As String, ByVal FlowCaseID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select b.* From WF_FlowCase a inner join WF_FlowStepM b")
        strSQL.AppendLine("	on a.FlowID = b.FlowID and a.FlowVer = b.FlowVer and a.FlowCurrStepID = b.FlowStepID")
        strSQL.AppendLine("Where a.FlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("And a.FlowCaseID = " & Bsp.Utility.Quote(FlowCaseID))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function
#End Region

#Region "WFA020 : 案件處理前置作業"
    ''' <summary>
    ''' 取得目前關卡所有的按鈕及可Assign之人員
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowVer"></param>
    ''' <param name="FlowStepID"></param>
    ''' <param name="ReturnMessage"></param>
    ''' <param name="Params"></param>
    ''' <returns></returns>
    ''' <remarks>供WFA0020流程處理使用</remarks>
    Public Function GetCurrentStepInfo(ByVal FlowID As String, ByVal FlowVer As String, ByVal FlowStepID As String, ByRef ReturnMessage As String, ByVal ParamArray Params As Object()) As DataTable
        Dim AssignTable As New DataTable

        AssignTable.Columns.Add(New DataColumn("btnActionName", System.Type.GetType("System.String")))
        AssignTable.Columns.Add(New DataColumn("DescText", System.Type.GetType("System.String")))
        AssignTable.Columns.Add(New DataColumn("UserList", System.Type.GetType("System.String")))
        AssignTable.Columns.Add(New DataColumn("FlowStepBtnReqList", System.Type.GetType("System.String")))
        AssignTable.Columns.Add(New DataColumn("FlowStepMail", System.Type.GetType("System.String")))

        Using dt As DataTable = GetFlowStepD(FlowID, FlowStepID, FlowVer)
            If dt.Rows.Count > 0 Then
                Dim ButtonName(dt.Rows.Count - 1) As String
                Dim RequireOpinion(dt.Rows.Count - 1) As String
                Dim NextStepID(dt.Rows.Count - 1) As String
                Dim SendMaiFlag(dt.Rows.Count - 1) As String

                For intLoop As Integer = 0 To dt.Rows.Count - 1
                    Dim beFlowStepD As New beWF_FlowStepD.Row(dt.Rows(intLoop))

                    ButtonName(intLoop) = beFlowStepD.ButtonName.Value
                    RequireOpinion(intLoop) = beFlowStepD.RequireOpinion.Value
                    NextStepID(intLoop) = beFlowStepD.NextStepID.Value
                    SendMaiFlag(intLoop) = beFlowStepD.SendMail.Value
                Next

                Dim AssignList As String = ""
                For intLoop As Integer = 0 To ButtonName.GetUpperBound(0)
                    If CheckButtonDisplay(FlowID, FlowVer, FlowStepID, ButtonName(intLoop), ReturnMessage, Params) Then
                        Dim dr As DataRow = AssignTable.NewRow
                        AssignList = FlowStepAssignList(FlowID, FlowVer, FlowStepID, ButtonName(intLoop), Params)
                        dr.Item("btnActionName") = ButtonName(intLoop)
                        dr.Item("DescText") = NextStepID(intLoop) & "-" & GetFlowStepMCol(FlowID, FlowVer, NextStepID(intLoop), "Description").ToString
                        dr.Item("UserList") = AssignList
                        dr.Item("FlowStepBtnReqList") = RequireOpinion(intLoop)
                        dr.Item("FlowStepMail") = SendMaiFlag(intLoop)
                        AssignTable.Rows.Add(dr)
                    End If
                Next
            End If
        End Using

        Return AssignTable
    End Function

    ''' <summary>
    ''' 取得流程顯示的人員
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowVer"></param>
    ''' <param name="FlowStepID"></param>
    ''' <param name="ButtonName"></param>
    ''' <param name="Params"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function FlowStepAssignList(ByVal FlowID As String, ByVal FlowVer As String, ByVal FlowStepID As String, ByVal ButtonName As String, ByVal ParamArray Params As Object()) As String
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim AssignList As String = ""
        Dim FlowCaseID As String = ""
        Dim AppID As String = ""
        'Dim RLID As String = ""

        If ht.ContainsKey("FlowCaseID") Then FlowCaseID = ht("FlowCaseID").ToString()
        If ht.ContainsKey("AppID") Then AppID = ht("AppID").ToString()
        'If ht.ContainsKey("RLID") Then RLID = ht("RLID").ToString()

        '先抓取預設顯示人員,若無才抓取自訂的人員
        Using dt As DataTable = GetFlowStepD(FlowID, FlowStepID, FlowVer, ButtonName)
            If dt.Rows.Count > 0 Then
                Select Case dt.Rows(0).Item("DefaultUserGroup").ToString()
                    Case "Z01"  '所有合法使用者
                        Using dtUser As DataTable = GetUserInfo("", "And BanMark = '0'")
                            If dtUser.Rows.Count > 0 Then
                                AssignList = ToDisUserName(dt, "UserID")
                            End If
                        End Using
                    Case "001"  '案件負責人..目前僅針對授信案件
                        Select Case FlowID
                            Case "AP"
                                Using dtAP As DataTable = GetAP_Profile(AppID)
                                    If dtAP.Rows.Count > 0 Then
                                        AssignList = ToDisUserName(dtAP.Rows(0).Item("OfficerID").ToString())
                                    End If
                                End Using
                        End Select
                    Case "A00"  '起案人
                        Using dtFlowCase As DataTable = GetFlowCase(FlowID, FlowCaseID)
                            If dtFlowCase.Rows.Count > 0 Then
                                AssignList = ToDisUserName(dtFlowCase.Rows(0).Item("CrUser").ToString())
                            End If
                        End Using
                    Case "A01"  '起案人主管
                        Using dtFlowCase As DataTable = GetFlowCase(FlowID, FlowCaseID)
                            If dtFlowCase.Rows.Count > 0 Then
                                AssignList = ToDisUserName(GetDeptBoss(dtFlowCase.Rows(0).Item("CrBR").ToString()))
                            End If
                        End Using
                    Case "A02"  '起案人部門主管及業主
                        Using dtFlowCase As DataTable = GetFlowCase(FlowID, FlowCaseID)
                            If dtFlowCase.Rows.Count > 0 Then
                                AssignList = ToDisUserName(GetDeptBossAndBussBoss(dtFlowCase.Rows(0).Item("CrBr").ToString(), "And Boss <> " & Bsp.Utility.Quote(UserProfile.UserID)))
                            End If
                        End Using
                    Case "A03" '起案人部門AO
                        Using dtFlowCase As DataTable = GetFlowCase(FlowID, FlowCaseID)
                            If dtFlowCase.Rows.Count > 0 Then
                                AssignList = ToDisUserName(GetDeptUser(dtFlowCase.Rows(0).Item("CrBr").ToString(), "And Exists (Select * From SC_UserGroup Where UserID = SC_User.UserID And GroupID = '01')"), "UserID")
                            End If
                        End Using
                    Case "A04"  '起案人部門人員
                        Using dtFlowCase As DataTable = GetFlowCase(FlowID, FlowCaseID)
                            If dtFlowCase.Rows.Count > 0 Then
                                AssignList = ToDisUserName(GetDeptUser(dtFlowCase.Rows(0).Item("CrBr").ToString()), "UserID")
                            End If
                        End Using
                    Case "A05"  '起案單位審查窗口
                        Using dtFlowCase As DataTable = GetFlowCase(FlowID, FlowCaseID)
                            If dtFlowCase.Rows.Count > 0 Then
                                'Using dtUser As DataTable = GetSC_User(dtFlowCase.Rows(0).Item("CrBr").ToString(), "DeptID")
                                '    If dtUser.Rows.Count > 0 Then
                                Using dtOrgan As DataTable = GetSC_Organization(dtFlowCase.Rows(0).Item("CrBr").ToString(), "AnalyzeWindow")
                                    If dtOrgan.Rows.Count > 0 Then
                                        AssignList = ToDisUserName(dtOrgan.Rows(0).Item(0).ToString())
                                    End If
                                End Using
                                '    End If
                                'End Using
                            End If
                        End Using
                        'Using dtAP As DataTable = GetAP_Profile(AppID, "OfficerID")
                        '    If dtAP.Rows.Count > 0 Then
                        '        Using dtUser As DataTable = GetSC_User(dtAP.Rows(0).Item(0).ToString(), "DeptID")
                        '            If dtUser.Rows.Count > 0 Then
                        '                Using dtOrgan As DataTable = GetSC_Organization(dtUser.Rows(0).Item(0).ToString(), "AnalyzeWindow")
                        '                    If dtOrgan.Rows.Count > 0 Then
                        '                        AssignList = ToDisUserName(dtOrgan.Rows(0).Item(0).ToString())
                        '                    End If
                        '                End Using
                        '            End If
                        '        End Using
                        '    End If
                        'End Using
                    Case "A06"  '起案單位审查科主管
                        Using dtAP As DataTable = GetAP_Profile(AppID, "OfficerID")
                            If dtAP.Rows.Count > 0 Then
                                Using dtUser As DataTable = GetSC_User(dtAP.Rows(0).Item(0).ToString(), "DeptID")
                                    If dtUser.Rows.Count > 0 Then
                                        Using dtOrgan As DataTable = GetSC_Organization(dtUser.Rows(0).Item(0).ToString(), "AnalyzeBoss")
                                            If dtOrgan.Rows.Count > 0 Then
                                                AssignList = ToDisUserName(dtOrgan.Rows(0).Item(0).ToString())
                                            End If
                                        End Using
                                    End If
                                End Using
                            End If
                        End Using
                    Case "B00"  '該關卡設定之簽核主管
                        Using dtLoanChecker As DataTable = getSC_LoanChecker_In(FlowCaseID, Bsp.Utility.IsStringNull(dt.Rows(0).Item("DefaultUserGroupEx")), "", FlowID).Tables(0)
                            If dtLoanChecker.Rows.Count > 0 Then
                                AssignList = ToDisUserName(dtLoanChecker.Rows(0).Item(0).ToString())
                            End If
                        End Using
                    Case "B01"  '前一關卡簽核人員
                        AssignList = ToDisUserName(GetPreviousFlowStepUser(FlowID, FlowCaseID, FlowStepID))
                    Case "B02"  '簽核人員部門主管
                        Using dtFlowCase As DataTable = GetFlowCase(FlowID, FlowCaseID)
                            If dtFlowCase.Rows.Count > 0 Then
                                AssignList = ToDisUserName(GetDeptBoss(UserProfile.DeptID))
                            End If
                        End Using
                    Case "B03"  '簽核人員部門主管及業主
                        AssignList = ToDisUserName(GetDeptBossAndBussBoss(UserProfile.DeptID, "And Boss <> " & Bsp.Utility.Quote(UserProfile.UserID)))
                    Case "B04"  '簽核人員部門人員
                        Using dtDeptUser As DataTable = GetDeptUser(UserProfile.DeptID, "And UserID <> " & Bsp.Utility.Quote(UserProfile.UserID))
                            If dtDeptUser.Rows.Count > 0 Then
                                Dim strUserIDs As String = ""
                                For Each dr As DataRow In dtDeptUser.Rows
                                    strUserIDs &= "," & dr.Item("UserID").ToString()
                                Next
                                AssignList = ToDisUserName(strUserIDs.Substring(1))
                            End If
                        End Using
                    Case "B05"  '簽核人員部門業主
                        AssignList = ToDisUserName(GetDeptBusinessBoss(UserProfile.DeptID))
                    Case "B06" '簽核單位覆審窗口
                        Using dtWindow As DataTable = GetSC_Organization(UserProfile.DeptID, "CRWindow")
                            If dtWindow.Rows.Count > 0 Then
                                AssignList = ToDisUserName(Bsp.Utility.IsStringNull(dtWindow.Rows(0).Item(0)))
                            End If
                        End Using
                    Case "C00"  '指定部門人員
                        Using dtDeptUser As DataTable = GetDeptUser(Bsp.Utility.IsStringNull(dt.Rows(0).Item("DefaultUserGroupEx")), "And UserID <> " & Bsp.Utility.Quote(UserProfile.UserID))
                            If dtDeptUser.Rows.Count > 0 Then
                                Dim strUserIDs As String = ""
                                For Each dr As DataRow In dtDeptUser.Rows
                                    strUserIDs &= "," & dr.Item("UserID").ToString()
                                Next
                                AssignList = ToDisUserName(strUserIDs.Substring(1))
                            End If
                        End Using
                    Case "C01"  '指定科組課人員
                        Using dtDeptUser As DataTable = GetOrganUser(Bsp.Utility.IsStringNull(dt.Rows(0).Item("DefaultUserGroupEx")), "And UserID <> " & Bsp.Utility.Quote(UserProfile.UserID))
                            If dtDeptUser.Rows.Count > 0 Then
                                Dim strUserIDs As String = ""
                                For Each dr As DataRow In dtDeptUser.Rows
                                    strUserIDs &= "," & dr.Item("UserID").ToString()
                                Next
                                AssignList = ToDisUserName(strUserIDs.Substring(1))
                            End If
                        End Using
                    Case "C02"  '指定群組人員
                        Using dtGroupUser As DataTable = GetGroupUser(Bsp.Utility.IsStringNull(dt.Rows(0).Item("DefaultUserGroupEx")), "And UserID <> " & Bsp.Utility.Quote(UserProfile.UserID))
                            If dtGroupUser.Rows.Count > 0 Then
                                Dim strUserIDs As String = ""
                                For Each dr As DataRow In dtGroupUser.Rows
                                    strUserIDs &= "," & dr.Item("UserID").ToString()
                                Next
                                AssignList = ToDisUserName(strUserIDs.Substring(1))
                            End If
                        End Using
                    Case "C03"  '指定關卡簽核人員
                        AssignList = ToDisUserName(GetFlowStepUser(FlowID, FlowCaseID, Bsp.Utility.IsStringNull(dt.Rows(0).Item("DefaultUserGroupEx"))))
                    Case Else
                        Return GetCustomAssignList(FlowID, FlowVer, FlowStepID, ButtonName, ht)
                End Select
                Return AssignList
            End If
        End Using

        Return GetCustomAssignList(FlowID, FlowVer, FlowStepID, ButtonName, ht)
    End Function

    ''' <summary>
    ''' 取得AP_Profile資料
    ''' </summary>
    ''' <param name="AppID"></param>
    ''' <param name="FieldNames"></param>
    ''' <param name="CondStr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAP_Profile(ByVal AppID As String, Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames.Trim() = "" Then FieldNames = "*"

        strSQL.AppendLine("Select " & FieldNames)
        strSQL.AppendLine("From AP_Profile")
        strSQL.AppendLine("Where 1 = 1")
        If AppID <> "" Then strSQL.AppendLine("And AppID = " & Bsp.Utility.Quote(AppID))
        If CondStr <> "" Then strSQL.AppendLine(CondStr)

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    ''' <summary>
    ''' 取得核貸書主檔資料
    ''' </summary>
    ''' <param name="AppID"></param>
    ''' <param name="FieldNames"></param>
    ''' <param name="CondStr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAL_Profile(ByVal AppID As String, Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames.Trim() = "" Then FieldNames = "*"

        strSQL.AppendLine("Select " & FieldNames)
        strSQL.AppendLine("From AL_Profile")
        strSQL.AppendLine("Where 1 = 1")
        If AppID <> "" Then strSQL.AppendLine("And AppID = " & Bsp.Utility.Quote(AppID))
        If CondStr <> "" Then strSQL.AppendLine(CondStr)

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    ''' <summary>
    ''' 取得覆審主檔資料
    ''' </summary>
    ''' <param name="CRID"></param>
    ''' <param name="FieldNames"></param>
    ''' <param name="CondStr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCR_Profile(ByVal CRID As String, Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames.Trim() = "" Then FieldNames = "*"

        strSQL.AppendLine("Select " & FieldNames)
        strSQL.AppendLine("From CR_Profile")
        strSQL.AppendLine("Where 1 = 1")
        If CRID <> "" Then strSQL.AppendLine("And CRID = " & Bsp.Utility.Quote(CRID))
        If CondStr <> "" Then strSQL.AppendLine(CondStr)

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    ''' <summary>
    ''' 取得SC_User資料
    ''' </summary>
    ''' <param name="UserID"></param>
    ''' <param name="FieldNames"></param>
    ''' <param name="CondStr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSC_User(ByVal UserID As String, Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames.Trim() = "" Then FieldNames = "*"

        strSQL.AppendLine("Select " & FieldNames)
        strSQL.AppendLine("From SC_User")
        strSQL.AppendLine("Where 1 = 1")
        If UserID <> "" Then strSQL.AppendLine("And UserID = " & Bsp.Utility.Quote(UserID))
        If CondStr <> "" Then strSQL.AppendLine(CondStr)

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    ''' <summary>
    ''' 取得部門資料
    ''' </summary>
    ''' <param name="OrganID"></param>
    ''' <param name="FieldNames"></param>
    ''' <param name="CondStr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSC_Organization(ByVal OrganID As String, Optional ByVal FieldNames As String = "", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames.Trim() = "" Then FieldNames = "*"

        strSQL.AppendLine("Select " & FieldNames)
        strSQL.AppendLine("From SC_Organization")
        strSQL.AppendLine("Where 1 = 1")
        If OrganID <> "" Then strSQL.AppendLine("And OrganID = " & Bsp.Utility.Quote(OrganID))
        If CondStr <> "" Then strSQL.AppendLine(CondStr)

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    ''' <summary>
    ''' 取得客製化流程顯示的人員
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowVer"></param>
    ''' <param name="FlowStepID"></param>
    ''' <param name="ButtonName"></param>
    ''' <param name="Params">注意這裡傳入的是hashtable</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetCustomAssignList(ByVal FlowID As String, ByVal FlowVer As String, ByVal FlowStepID As String, ByVal ButtonName As String, ByVal Params As Hashtable) As String
        Dim AssignList As String = ""
        Dim FlowCaseID As String = ""
        Dim CRID As String = ""
        Dim CMNO As String = ""
        Dim AppID As String = ""

        For Each s As String In Params.Keys
            Select Case s
                Case "CRID"
                    CRID = Params(s).ToString()
                Case "CMNO"
                    CMNO = Params(s).ToString()
                Case "AppID"
                    AppID = Params(s).ToString()
            End Select
        Next

        Select Case FlowID & "-" & FlowVer & "-" & FlowStepID & "-" & ButtonName
            Case "AP-1-S006-送授信审查委员会"
                Select Case GetDirectType(AppID)
                    Case "1"    '書審
                        AssignList = String.Format("Q9_1-{0}", GetGroupName("Q9_1"))
                    Case "2"    '線審
                        AssignList = String.Format("Q9-{0}", GetGroupName("Q9"))
                    Case Else
                        AssignList = ""
                End Select
            Case "CM-1-CM01-参阅"
                AssignList = "CM02-参阅人员"
            Case "CM-1-CM01-审核"
                AssignList = ToDisUserName(GetCMBoss(CMNO), ",")
            Case "CM-1-CM02-审核"
                AssignList = ToDisUserName(GetCMBoss(CMNO), ",")
            Case "RV-1-RV02-复审送审"
                Using dt As DataTable = GetCR_Profile(CRID, "AOID")
                    If dt.Rows.Count > 0 Then
                        AssignList = ToDisUserName(Bsp.Utility.IsStringNull(dt.Rows(0).Item(0)))
                    End If
                End Using
            Case Else   '其他,未指定時
                AssignList = ToDisUserName(UserProfile.UserID)
        End Select
        Return AssignList
    End Function

    'S010關卡時檢查是否為第一個不同意或有條件同意
    Public Function GetDisagreeCount(ByVal FlowID As String, ByVal FlowCaseID As String, ByVal tran As DbTransaction) As Integer
        Dim strSQL As New StringBuilder

        strSQL.AppendLine("Select Count(*) Cnt")
        strSQL.AppendLine("From WF_FlowFullLog with (nolock)")
        strSQL.AppendLine("Where FlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("And FlowCaseID = " & Bsp.Utility.Quote(FlowCaseID))
        strSQL.AppendLine("And FlowStepID = 'S010'")
        strSQL.AppendLine("And FlowStepAction in ('不同意併送審查維護','有條件同意','同意送書面審議')")

        Return CInt(Bsp.DB.ExecuteScalar(strSQL.ToString(), tran))
    End Function

    Public Function GetDirectType(ByVal AppID As String) As String
        Dim strSQL As String

        strSQL = "Select Isnull(DirectType, '') DirectType From AL_Profile with (nolock) Where AppID = " & Bsp.Utility.Quote(AppID)
        Return Bsp.DB.ExecuteScalar(strSQL)
    End Function

    Public Shared Function GetGroupName(ByVal GroupID As String) As String
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select GroupName From SC_Group with (nolock) Where GroupID = " & Bsp.Utility.Quote(GroupID))

        Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item(0).ToString()
            Else
                Return ""
            End If
        End Using
    End Function


#End Region

#Region "WFA020 : 案件流程處理"
    ''' <summary>
    ''' 起流程作業
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowStepID"></param>
    ''' <param name="FlowVer"></param>
    ''' <param name="FlowDispatchFlag"></param>
    ''' <param name="AssignTo"></param>
    ''' <param name="KeyValue"></param>
    ''' <param name="PaperIDName">放入PaperID欄位的名稱</param>
    ''' <param name="CurFlow">若為子流程,此參數為必要之參數</param>
    ''' <param name="tran"></param>
    ''' <param name="arrShowValue"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CreateFlow(ByVal FlowID As String, ByVal FlowStepID As String, ByVal FlowVer As String, _
            ByVal FlowDispatchFlag As String, ByVal AssignTo As String, ByVal PaperIDName As String, _
            ByVal KeyValue As Hashtable, ByVal CurFlow As FlowInstance, _
            ByVal tran As DbTransaction, ByVal ParamArray arrShowValue As Object()) As ArrayList
        Dim strShowValue As String = ""
        Dim CurrFlowID As String = ""
        Dim CurrFlowCaseID As String = ""
        Dim CurrFlowStepID As String = ""
        Dim CurrFlowLogBatNo As String = ""
        Dim FlowStepAction As String = ""
        Dim FlowDefaultOpinion As String = "無意見"
        Dim TraceFlag As String = "N"
        Dim strKeyValue As String = ""
        Dim strPaperID As String = ""

        For Each strKey As String In KeyValue.Keys
            Select Case strKey
                Case PaperIDName
                    If strKeyValue = "" Then
                        strKeyValue = String.Format("{0}={1}", strKey, KeyValue(strKey))
                    Else
                        strKeyValue = String.Format("{0}={1}&{2}", strKey, KeyValue(strKey), IIf(Left(strKeyValue, 1) = "&", Right(strKeyValue, Len(strKeyValue) - 1), strKeyValue))
                    End If

                    strPaperID = KeyValue(strKey)
                Case "FlowOpinion"
                    FlowDefaultOpinion = KeyValue(strKey)
                Case Else
                    strKeyValue &= String.Format("&{0}={1}", strKey, KeyValue(strKey))
            End Select
        Next

        '檢查若無PaperID,則需去掉第一碼的(&)符號
        If strPaperID = "" Then strKeyValue = strKeyValue.Substring(1)

        If arrShowValue.Length > 0 Then
            For intLoop As Integer = 0 To arrShowValue.Length - 1
                strShowValue &= "|" & arrShowValue(intLoop).ToString()
            Next
            strShowValue = strShowValue.Substring(1)
        End If

        Dim rtnArrList As New ArrayList()

        If CurFlow Is Nothing Then
            CurrFlowLogBatNo = 0
        Else
            CurrFlowID = CurFlow.FlowID
            CurrFlowCaseID = CurFlow.FlowCaseID
            CurrFlowStepID = CurFlow.FlowStepID
            CurrFlowLogBatNo = CurFlow.FlowLogBatNo
        End If

        TraceFlag = "Y"
        FlowStepAction = GetFlowStepMCol(FlowID, FlowVer, FlowStepID, "Description")

        Try
            If FlowStepAction <> "" Then
                '改為呼叫Store Procedure處理
                'Dim rtnFlowCaseID As String = _
                '    Insert_FlowCase(FlowID, FlowVer, FlowStepID, strKeyValue, strShowValue, _
                '    FlowDispatchFlag, strPaperID, AssignTo, CurrFlowID, CurrFlowCaseID, CurrFlowStepID, _
                '    CurrFlowLogBatNo, FlowStepAction, FlowDefaultOpinion, TraceFlag, tran)

                Dim db As Database = DatabaseFactory.CreateDatabase()
                Dim dbCommand As DbCommand
                Dim rtnFlowCaseID As String = ""

                dbCommand = db.GetStoredProcCommand("SP_WF_CreateFlow")

                db.AddInParameter(dbCommand, "@argFlowID", DbType.String, FlowID)
                db.AddInParameter(dbCommand, "@argFlowVer", DbType.Decimal, FlowVer)
                db.AddInParameter(dbCommand, "@argFlowNextStepID", DbType.String, FlowStepID)
                db.AddInParameter(dbCommand, "@argFlowKeyValue", DbType.String, strKeyValue)
                db.AddInParameter(dbCommand, "@argFlowShowValue", DbType.String, strShowValue)
                db.AddInParameter(dbCommand, "@argFlowDispatchFlag", DbType.String, FlowDispatchFlag)
                db.AddInParameter(dbCommand, "@argPaperID", DbType.String, strPaperID)
                db.AddInParameter(dbCommand, "@argAssignTo", DbType.String, AssignTo)
                db.AddInParameter(dbCommand, "@argCurrFlowID", DbType.String, CurrFlowID)
                db.AddInParameter(dbCommand, "@argCurrFlowCaseID", DbType.String, CurrFlowCaseID)
                db.AddInParameter(dbCommand, "@argCurrFlowStepID", DbType.String, CurrFlowStepID)
                db.AddInParameter(dbCommand, "@argCurrFlowLogBatNo", DbType.String, CurrFlowLogBatNo)
                db.AddInParameter(dbCommand, "@argFlowStepAction", DbType.String, FlowStepAction)
                db.AddInParameter(dbCommand, "@argFlowStepOpinion", DbType.String, FlowDefaultOpinion)
                db.AddInParameter(dbCommand, "@argTraceFlag", DbType.String, TraceFlag)
                db.AddInParameter(dbCommand, "@argCrUser", DbType.String, UserProfile.UserID)
                db.AddInParameter(dbCommand, "@argCrBr", DbType.String, UserProfile.DeptID)
                db.AddInParameter(dbCommand, "@argActUser", DbType.String, UserProfile.ActUserID)
                db.AddOutParameter(dbCommand, "@argFlowCaseID", DbType.String, 15)

                db.ExecuteNonQuery(dbCommand)

                rtnFlowCaseID = db.GetParameterValue(dbCommand, "@argFlowCaseID").ToString()

                rtnArrList.Add(rtnFlowCaseID)
                rtnArrList.Add(AssignTo)
            Else
                Throw New Exception("關卡[" & FlowStepID & "] 未在起案定義內!")
            End If
            Return rtnArrList
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' 修改案件的KeyValue和ShowValue
    ''' </summary>
    ''' <param name="PaperID">需確定為惟一的ID</param>
    ''' <param name="PaperIDName"></param>
    ''' <param name="KeyValue"></param>
    ''' <param name="Transaction"></param>
    ''' <param name="ShowValue"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateFlowKey(ByVal PaperID As String, ByVal PaperIDName As String, ByVal KeyValue As Hashtable, ByVal Transaction As DbTransaction, ByVal ParamArray ShowValue() As String) As Boolean
        Dim strSQL As New StringBuilder()
        Dim strKeyValue As String = ""
        Dim strShowValue As String = ""

        For Each strKey As String In KeyValue.Keys
            Select Case strKey
                Case PaperIDName
                    strKeyValue = String.Format("{0}={1}&{2}", strKey, KeyValue(strKey), strKeyValue)
                Case Else
                    strKeyValue &= String.Format("&{0}={1}", strKey, KeyValue(strKey))
            End Select
        Next

        If strKeyValue.Substring(0, 1) = "&" Then strKeyValue = strKeyValue.Substring(1)

        If ShowValue.Length > 0 Then
            For intLoop As Integer = 0 To ShowValue.Length - 1
                strShowValue &= "|" & ShowValue(intLoop).ToString()
            Next
            strShowValue = strShowValue.Substring(1)
        End If

        strSQL.AppendLine("Update WF_FlowCase Set")
        strSQL.AppendLine(" FlowKeyValue = @FlowKeyValue, ")
        strSQL.AppendLine(" FlowShowValue = @FlowShowValue")
        strSQL.AppendLine("Where PaperID = @PaperID;")
        strSQL.AppendLine("Update WF_FlowToDoList Set")
        strSQL.AppendLine(" FlowKeyValue = @FlowKeyValue, ")
        strSQL.AppendLine(" FlowShowValue = @FlowShowValue")
        strSQL.AppendLine("Where PaperID = @PaperID;")

        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), New DbParameter() { _
                              Bsp.DB.getDbParameter("@FlowKeyValue", strKeyValue), _
                              Bsp.DB.getDbParameter("@FlowShowValue", strShowValue), _
                              Bsp.DB.getDbParameter("@PaperID", PaperID)}, Transaction)
    End Function

    ''' <summary>
    ''' 新增流程資料
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowVer"></param>
    ''' <param name="FlowNextStepID"></param>
    ''' <param name="FlowKeyValue"></param>
    ''' <param name="FlowShowValue"></param>
    ''' <param name="FlowDispatchFlag"></param>
    ''' <param name="PaperID"></param>
    ''' <param name="AssignTo"></param>
    ''' <param name="CurrFlowID"></param>
    ''' <param name="CurrFlowCaseID"></param>
    ''' <param name="CurrFlowStepID"></param>
    ''' <param name="CurrFlowLogBatID"></param>
    ''' <param name="FlowStepAction"></param>
    ''' <param name="FlowStepOpinion"></param>
    ''' <param name="TraceFlag"></param>
    ''' <param name="tran"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Insert_FlowCase(ByVal FlowID As String, ByVal FlowVer As String, ByVal FlowNextStepID As String, _
        ByVal FlowKeyValue As String, ByVal FlowShowValue As String, _
        ByVal FlowDispatchFlag As String, ByVal PaperID As String, ByVal AssignTo As String, _
        ByVal CurrFlowID As String, ByVal CurrFlowCaseID As String, ByVal CurrFlowStepID As String, _
        ByVal CurrFlowLogBatID As Integer, ByVal FlowStepAction As String, ByVal FlowStepOpinion As String, _
        ByVal TraceFlag As String, ByVal tran As DbTransaction) As String

        Dim beFlowCase As New beWF_FlowCase.Row()
        Dim bsFlowCase As New beWF_FlowCase.Service()
        Try
            With beFlowCase
                .FlowCaseID.Value = GetFlowCaseID(FlowID, tran)
                .FlowID.Value = FlowID
                .FlowVer.Value = FlowVer
                .FlowKeyValue.Value = FlowKeyValue
                .FlowShowValue.Value = FlowShowValue
                .FlowCaseStatus.Value = "Open"
                .FlowDispatchFlag.Value = FlowDispatchFlag
                .FlowCurrStepID.Value = "0000"
                .FlowCurrStepDesc.Value = GetFlowStepMCol(FlowID, FlowVer, .FlowCurrStepID.Value, "Description").ToString()
                .FlowCurrStepDueDate.Value = Now.AddDays(Convert.ToInt32(GetFlowStepMCol(FlowID, FlowVer, FlowNextStepID, "ProcDay"))).ToString("yyyyMMdd")
                .FlowSubCaseFlag.Value = ""
                .LastLogBatNo.Value = 0
                .LastLogID.Value = 0
                .CrBr.Value = UserProfile.DeptID
                .CrBrName.Value = UserProfile.DeptName
                .CrUser.Value = UserProfile.UserID
                .CrUserName.Value = UserProfile.UserName
                .CrDate.Value = Now
                .PaperID.Value = PaperID
                .UpdDate.Value = Now
            End With
            bsFlowCase.Insert(beFlowCase, tran)

            Dim LastLogID As String = ""
            Dim LogRetrn As String = _
                Insert_FlowFullLog(FlowID, FlowVer, beFlowCase.FlowCaseID.Value, _
                     beFlowCase.FlowCurrStepID.Value, beFlowCase.CrUser.Value, beFlowCase.CrUser.Value, "Open", LastLogID, tran)

            If CurrFlowID <> "" AndAlso CurrFlowCaseID <> "" AndAlso CurrFlowStepID <> "" AndAlso _
                CurrFlowLogBatID > 0 Then
                '更新母流程之關聯狀態
                Update_FlowCaseRelation(CurrFlowID, CurrFlowCaseID, "Y", tran)
                Insert_FlowRelation(beFlowCase.FlowID.Value, beFlowCase.FlowCaseID.Value, CurrFlowID, CurrFlowCaseID, _
                    CurrFlowStepID, CurrFlowLogBatID, "Open", tran)
            End If

            GoToNextStep(FlowID, FlowVer, beFlowCase.FlowCaseID.Value, beFlowCase.FlowCurrStepID.Value, 1, _
                LastLogID, FlowStepAction, FlowStepOpinion, UserProfile.ActUserID, _
                AssignTo, "", FlowNextStepID, TraceFlag, "", FlowDispatchFlag, tran)
        Catch ex As Exception
            Throw
        End Try
        Return beFlowCase.FlowCaseID.Value
    End Function

    ''' <summary>
    ''' 異動母流程之FlowSubcaseFlag
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowCaseID"></param>
    ''' <param name="FlowSubCaseFlag"></param>
    ''' <param name="tran"></param>
    ''' <remarks></remarks>
    Private Sub Update_FlowCaseRelation(ByVal FlowID As String, ByVal FlowCaseID As String, ByVal FlowSubCaseFlag As String, ByVal tran As DbTransaction)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Update WF_FlowCase Set")
        strSQL.AppendLine(" FlowSubCaseFlag = @FlowSubCaseFlag")
        strSQL.AppendLine("Where FlowID = @FlowID and FlowCaseID = @FlowCaseID")

        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), New DbParameter() { _
            Bsp.DB.getDbParameter("@FlowSubCaseFlag", FlowSubCaseFlag), _
            Bsp.DB.getDbParameter("@FlowID", FlowID), _
            Bsp.DB.getDbParameter("@FlowCaseID", FlowCaseID)}, tran)
    End Sub

    ''' <summary>
    ''' 案件流程處理時，檢查目前案件是否在此人身上，並且整個案件無Close
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowCaseID"></param>
    ''' <param name="FlowLogID"></param>
    ''' <param name="CurrUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckFlowLogCurrUser(ByVal FlowID As String, ByVal FlowCaseID As String, ByVal FlowLogID As String, ByVal CurrUser As String) As Boolean
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select count(*) From WF_FlowFullLog with (nolock)")
        strSQL.AppendLine("Where FlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("And FlowCaseID = " & Bsp.Utility.Quote(FlowCaseID))
        strSQL.AppendLine("And FlowLogID = " & Bsp.Utility.Quote(FlowLogID))
        strSQL.AppendLine("And AssignTo = " & Bsp.Utility.Quote(CurrUser))
        strSQL.AppendLine("And FlowLogStatus <> 'Close' ")

        If Convert.ToInt32(Bsp.DB.ExecuteScalar(strSQL.ToString())) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function GetFlowToDoInfo(ByVal FlowID As String, ByVal FlowCaseID As String, ByVal FlowLogID As String) As DataTable
        Dim strSQL As New StringBuilder

        strSQL.AppendLine("Select dbo.funSplitString(FlowShowValue, '|', 1) as KeyID,")
        strSQL.AppendLine(" dbo.funSplitString(FlowShowValue, '|', 2) as CustomerName, ")
        strSQL.AppendLine(" Left(Isnull(dbo.funSplitString(FlowShowValue, '|', 3), '') + Isnull('|' + dbo.funSplitString(FlowShowValue, '|', 4), ''), 20) AS Note,")
        strSQL.AppendLine(" CrUser + '-' + CrUserName AS CrUserName, CrBr + '-' + CrBrName CrBrName, CONVERT(varchar(8), CrDate, 112) AS Crdate")
        strSQL.AppendLine("From WF_FlowToDoList with (nolock)")
        strSQL.AppendLine("Where FlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("And FlowCaseID = " & Bsp.Utility.Quote(FlowCaseID))
        strSQL.AppendLine("And FlowLogID = " & Bsp.Utility.Quote(FlowLogID))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    ''' <summary>
    ''' 將傳入之UserList轉換成UserID+'-'+UserName的字串
    ''' </summary>
    ''' <param name="UserList"></param>
    ''' <param name="SplitKey"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ToDisUserName(ByVal UserList As String, Optional ByVal SplitKey As String = ",") As String
        UserList = UserList.Replace(SplitKey, "','").ToString
        Dim strSQL As New StringBuilder()
        Dim UserStr As String = ""

        strSQL.AppendLine("Select distinct UserID + '-' + UserName as UserName, UserID")
        strSQL.AppendLine("From SC_User")
        strSQL.AppendLine("Where UserID in ('" & UserList & "')")
        strSQL.AppendLine("Order by UserID")

        Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
            If dt.Rows.Count > 0 Then
                For intLoop As Integer = 0 To dt.Rows.Count - 1
                    UserStr &= dt.Rows(intLoop).Item("UserName").ToString() & ","
                Next
                UserStr = Left(UserStr, UserStr.Length - 1)
            End If
        End Using
        Return UserStr
    End Function

    ''' <summary>
    ''' 將傳入之UserTable, 以指定的User Field串出User List
    ''' </summary>
    ''' <param name="UserTable"></param>
    ''' <param name="UserIDField"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ToDisUserName(ByVal UserTable As DataTable, ByVal UserIDField As String) As String
        If UserTable.Rows.Count = 0 Then Return ""
        If Not UserTable.Columns.Contains(UserIDField) Then Return ""

        Dim UserList As String = ""

        For Each dr As DataRow In UserTable.Rows
            UserList &= "," & dr.Item(UserIDField).ToString()
        Next
        UserList = UserList.Substring(1)

        Return ToDisUserName(UserList)
    End Function

    ''' <summary>
    ''' 將傳入的UserID清單,不重複傳出有效的UserID
    ''' </summary>
    ''' <param name="UserList"></param>
    ''' <param name="SplitKey"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ToDisUser(ByVal UserList As String, Optional ByVal SplitKey As String = ",") As String
        UserList = UserList.Replace(SplitKey, "','").ToString
        Dim strSQL As New StringBuilder()
        Dim UserStr As String = ""

        strSQL.AppendLine("Select distinct UserID")
        strSQL.AppendLine("From SC_User with (nolock)")
        strSQL.AppendLine("Where UserID in ('" & UserList & "')")
        strSQL.AppendLine("And BanMark = '0'")
        strSQL.AppendLine("Order by UserID")

        Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
            If dt.Rows.Count > 0 Then
                For intLoop As Integer = 0 To dt.Rows.Count - 1
                    UserStr &= dt.Rows(intLoop).Item("UserID").ToString() & ";"
                Next
                UserStr = Left(UserStr, UserStr.Length - 1)
            End If
        End Using
        Return UserStr
    End Function


    ''' <summary>
    ''' 查詢意見暫存表WF_FlowOpinTmp
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowCaseID"></param>
    ''' <param name="FlowLogID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFlowOpinTemp(ByVal FlowID As String, ByVal FlowCaseID As String, ByVal FlowLogID As String) As String
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select FlowStepOpinion From WF_FlowOpinTmp with (nolock)")
        strSQL.AppendLine("Where FlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("And FlowCaseID = " & Bsp.Utility.Quote(FlowCaseID))
        strSQL.AppendLine("And FlowLogID = " & Bsp.Utility.Quote(FlowLogID))

        Return Bsp.Utility.IsStringNull(Bsp.DB.ExecuteScalar(strSQL.ToString()))
    End Function

    ''' <summary>
    ''' 暫存意見儲存
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowCaseID"></param>
    ''' <param name="FlowLogID"></param>
    ''' <param name="TempOpinion"></param>
    ''' <remarks></remarks>
    Public Sub SaveFlowOpinTemp(ByVal FlowID As String, ByVal FlowCaseID As String, ByVal FlowLogID As String, ByVal TempOpinion As String)
        Dim beFlowOpinTemp As New beWF_FlowOpinTmp.Row()
        Dim bsFlowOpinTemp As New beWF_FlowOpinTmp.Service()

        With beFlowOpinTemp
            .FlowID.Value = FlowID
            .FlowCaseID.Value = FlowCaseID
            .FlowLogID.Value = FlowLogID
            .FlowStepOpinion.Value = TempOpinion
            .LastChgDate.Value = Now
            .LastChgID.Value = UserProfile.ActUserID
        End With

        If bsFlowOpinTemp.IsDataExists(beFlowOpinTemp) Then
            bsFlowOpinTemp.Update(beFlowOpinTemp)
        Else
            bsFlowOpinTemp.Insert(beFlowOpinTemp)
        End If
    End Sub

    ''' <summary>
    ''' 流程送出處理
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowVer"></param>
    ''' <param name="FlowCaseID"></param>
    ''' <param name="FlowCurrStepID"></param>
    ''' <param name="FlowCurrLogBatNo"></param>
    ''' <param name="FlowCurrLogID"></param>
    ''' <param name="FlowStepAction"></param>
    ''' <param name="FlowStepOpinion"></param>
    ''' <param name="ActUser"></param>
    ''' <param name="AssignTo"></param>
    ''' <param name="LogRemark"></param>
    ''' <param name="FlowNextStepID"></param>
    ''' <param name="TraceFlag"></param>
    ''' <param name="MailList">請以逗號(,)隔開; 無另外通知人員則放空字串</param>
    ''' <param name="FlowDispatchFlag"></param>
    ''' <param name="tran"></param>
    ''' <remarks></remarks>
    Public Sub GoToNextStep(ByVal FlowID As String, ByVal FlowVer As String, ByVal FlowCaseID As String, _
        ByVal FlowCurrStepID As String, ByVal FlowCurrLogBatNo As Integer, ByVal FlowCurrLogID As String, _
        ByVal FlowStepAction As String, ByVal FlowStepOpinion As String, ByVal ActUser As String, _
        ByVal AssignTo As String, ByVal LogRemark As String, ByVal FlowNextStepID As String, _
        ByVal TraceFlag As String, ByVal MailList As String, ByVal FlowDispatchFlag As String, ByVal tran As DbTransaction)

        Bsp.DB.ExecuteNonQuery(CommandType.StoredProcedure, "SP_WF_GoNextStep", _
                    New DbParameter() { _
                    Bsp.DB.getDbParameter("@argFlowID", FlowID), _
                    Bsp.DB.getDbParameter("@argFlowCaseID", FlowCaseID), _
                    Bsp.DB.getDbParameter("@argFlowVer", FlowVer), _
                    Bsp.DB.getDbParameter("@argFlowCurrStepID", FlowCurrStepID), _
                    Bsp.DB.getDbParameter("@argFlowCurrLogBatNo", FlowCurrLogBatNo), _
                    Bsp.DB.getDbParameter("@argFlowCurrLogID", FlowCurrLogID), _
                    Bsp.DB.getDbParameter("@argFlowStepAction", FlowStepAction), _
                    Bsp.DB.getDbParameter("@argFlowStepOpinion", FlowStepOpinion), _
                    Bsp.DB.getDbParameter("@argCurrUser", UserProfile.UserID), _
                    Bsp.DB.getDbParameter("@argActUser", UserProfile.ActUserID), _
                    Bsp.DB.getDbParameter("@argAssignTo", AssignTo), _
                    Bsp.DB.getDbParameter("@argLogRemark", LogRemark), _
                    Bsp.DB.getDbParameter("@argFlowNextStepID", FlowNextStepID), _
                    Bsp.DB.getDbParameter("@argPaperID", ""), _
                    Bsp.DB.getDbParameter("@argTraceFlag", TraceFlag), _
                    Bsp.DB.getDbParameter("@argFlowDispatchFlag", FlowDispatchFlag)}, tran)
    End Sub

    ''' <summary>
    ''' 寄送流程信件
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowCase"></param>
    ''' <param name="MailList"></param>
    ''' <param name="AssignTo"></param>
    ''' <param name="FlowStepAction"></param>
    ''' <param name="NextFlowStepID"></param>
    ''' <param name="FlowStepOpinion"></param>
    ''' <param name="tran"></param>
    ''' <remarks></remarks>
    Private Sub SendFlowMail(ByVal FlowID As String, ByVal FlowCase As String, ByVal MailList As String, _
            ByVal AssignTo As String, ByVal FlowStepAction As String, ByVal NextFlowStepID As String, _
            ByVal FlowVer As String, ByVal FlowStepOpinion As String, ByVal tran As DbTransaction)
        Dim DocKey As String = "" '用來寄送mail之key值
        Dim MailHeader As String = ""
        Dim PsDesc As New StringBuilder()
        Dim PsDescEx As String = ""
        Dim ProcessUser As String = ""

        MailHeader = Bsp.MySettings.FlowMailHeader

        Using dt As DataTable = GetFlowCase(FlowID, FlowCase, tran)
            DocKey = "案件編號：" & dt.Rows(0).Item("PaperID").ToString()
        End Using

        If UserProfile.ActUserID = UserProfile.UserID Then
            ProcessUser = UserProfile.ActUserID & "-" & UserProfile.ActUserName
        Else
            ProcessUser = UserProfile.ActUserID & "-" & UserProfile.ActUserName & "(代" & UserProfile.UserID & "-" & UserProfile.UserName & ")"
        End If

        PsDesc.AppendLine("<table>")
        Using dt As DataTable = GetUserInfo(AssignTo)
            If dt.Rows.Count > 0 Then
                PsDesc.AppendLine("<tr><th align=right>待處理人員：</th><td>" & String.Format("{0}-{1}", AssignTo, dt.Rows(0).Item("UserName").ToString()) & "</td></tr>")
            Else
                PsDesc.AppendLine("<tr><th align=right>待處理人員：</th><td>" & AssignTo & "</td></tr>")
            End If
        End Using
        PsDesc.AppendLine(DocKey)
        PsDesc.AppendLine("<tr><th align=right>上一關處理人員：</th><td>" & ProcessUser & "</td></tr>")
        PsDesc.AppendLine("<tr><th align=right>執行動作：</th><td>" & FlowStepAction & "->[" & NextFlowStepID & "-" & GetFlowStepMCol(FlowID, FlowVer, NextFlowStepID, "Description").ToString() & "]" & "</td></tr>")
        PsDesc.AppendLine("<tr><th align=right>上一關處理時間：</th><td>" & Now.ToString("yyyy/MM/dd HH:mm:ss") & "</td></tr>")
        PsDesc.AppendLine("<tr><th align=right>上一關處理意見內容：</th><td>" & FlowStepOpinion & "</td></tr>")
        PsDesc.AppendLine("</table>")

        Bsp.DB.ExecuteNonQuery(CommandType.StoredProcedure, "SP_SendMail", _
                New DbParameter() { _
                Bsp.DB.getDbParameter("@argSenderID", ""), _
                Bsp.DB.getDbParameter("@argMailAddress", MailList), _
                Bsp.DB.getDbParameter("@argMailHeader", MailHeader), _
                Bsp.DB.getDbParameter("@argGreetWord", ""), _
                Bsp.DB.getDbParameter("@argPsDesc", PsDesc.ToString()), _
                Bsp.DB.getDbParameter("@argLinkPath", ""), _
                Bsp.DB.getDbParameter("@argAttachment", "")}, tran)
    End Sub

    Private Sub Update_FlowCase(ByVal FlowID As String, ByVal FlowCaseID As String, ByVal FlowVer As String, _
            ByVal FlowCaseStatus As String, ByVal FlowCurrStepID As String, ByVal LastLogBatNo As Integer, _
            ByVal LastLogID As String, ByVal FlowDispatchFlag As String, ByVal tran As DbTransaction)
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine("Update WF_FlowCase Set")
        strSQL.AppendLine(" FlowCaseStatus = @FlowCaseStatus, ")
        strSQL.AppendLine(" FlowCurrStepID = @FlowCurrStepID, ")
        strSQL.AppendLine(" FlowCurrStepDesc = @FlowCurrStepDesc, ")
        strSQL.AppendLine(" FlowCurrStepDueDate = @FlowCurrStepDueDate, ")
        strSQL.AppendLine(" FlowSubCaseFlag  = @FlowSubCaseFlag, ")
        strSQL.AppendLine(" LastLogBatNo  = @LastLogBatNo, ")
        strSQL.AppendLine(" LastLogID = @LastLogID, ")
        strSQL.AppendLine(" FlowDispatchFlag = @FlowDispatchFlag, ")
        strSQL.AppendLine(" UpdDate  = getdate()")
        strSQL.AppendLine("Where FlowID = @FlowID and FlowCaseID = @FlowCaseID")

        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), New DbParameter() { _
            Bsp.DB.getDbParameter("@FlowCaseStatus", FlowCaseStatus), _
            Bsp.DB.getDbParameter("@FlowCurrStepID", FlowCurrStepID), _
            Bsp.DB.getDbParameter("@FlowCurrStepDesc", GetFlowStepMCol(FlowID, FlowVer, FlowCurrStepID, "Description").ToString()), _
            Bsp.DB.getDbParameter("@FlowCurrStepDueDate", Now.ToString("yyyyMMdd")), _
            Bsp.DB.getDbParameter("@FlowSubCaseFlag", IIf(ExistChildFlow(FlowID, FlowCaseID), "Y", "N")), _
            Bsp.DB.getDbParameter("@LastLogBatNo", LastLogBatNo), _
            Bsp.DB.getDbParameter("@LastLogID", LastLogID), _
            Bsp.DB.getDbParameter("@FlowDispatchFlag", FlowDispatchFlag), _
            Bsp.DB.getDbParameter("@FlowID", FlowID), _
            Bsp.DB.getDbParameter("@FlowCaseID", FlowCaseID)}, tran)
    End Sub

    ''' <summary>
    ''' 新增FlowFullLog
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowVer"></param>
    ''' <param name="FlowCaseID"></param>
    ''' <param name="FlowStepID"></param>
    ''' <param name="FromUser"></param>
    ''' <param name="AssignTo"></param>
    ''' <param name="FlowCaseStatus"></param>
    ''' <param name="NewFlowLogID">傳出新取得之FlowLogID</param>
    ''' <param name="tran"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Insert_FlowFullLog(ByVal FlowID As String, ByVal FlowVer As String, ByVal FlowCaseID As String, _
            ByVal FlowStepID As String, ByVal FromUser As String, ByVal AssignTo As String, ByVal FlowCaseStatus As String, _
            ByRef NewFlowLogID As String, ByVal tran As DbTransaction) As Integer

        Dim intFlowLogBatNo As Integer = GetFlowLogBatNo(FlowID, FlowCaseID, tran)
        Dim aryAssignTo() As String = AssignTo.Split(";")

        For intLoop As Integer = 0 To aryAssignTo.GetUpperBound(0)
            If Trim(aryAssignTo(intLoop).ToString) <> "" Then
                Dim beFlowFullLog As New beWF_FlowFullLog.Row()
                Dim bsFlowFullLog As New beWF_FlowFullLog.Service()

                With beFlowFullLog
                    .FlowID.Value = FlowID
                    .FlowCaseID.Value = FlowCaseID
                    .FlowLogBatNo.Value = intFlowLogBatNo
                    .FlowLogID.Value = GetFlowLogID(FlowID, FlowCaseID, tran)
                    NewFlowLogID = .FlowLogID.Value
                    .FlowStepID.Value = FlowStepID
                    .FlowStepDesc.Value = GetFlowStepMCol(FlowID, FlowVer, FlowStepID, "Description").ToString()
                    .FlowStepAction.Value = ""
                    .FlowStepOpinion.Value = ""
                    .FlowLogStatus.Value = FlowCaseStatus
                    Using dt As DataTable = GetUserInfo(FromUser)
                        .FromBr.Value = dt.Rows(0).Item("DeptID").ToString
                        .FromBrName.Value = dt.Rows(0).Item("DeptName").ToString
                        .FromUser.Value = FromUser
                        .FromUserName.Value = dt.Rows(0).Item("UserName").ToString
                    End Using
                    .AssignTo.Value = aryAssignTo(intLoop)
                    Using dt As DataTable = GetUserInfo(aryAssignTo(intLoop))
                        .AssignToName.Value = dt.Rows(0).Item("UserName").ToString
                    End Using
                    .ToBr.Value = ""
                    .ToBrName.Value = ""
                    .ToUser.Value = ""
                    .ToUserName.Value = ""
                    .IsProxy.Value = ""
                    .CrDate.Value = Now
                    .UpdDate.Value = "1900/1/1"
                    .LogRemark.Value = ""
                End With

                bsFlowFullLog.Insert(beFlowFullLog, tran)
            End If
        Next
        Return intFlowLogBatNo
    End Function

    ''' <summary>
    ''' 流程異動WF_FlowFullLog
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowVer"></param>
    ''' <param name="FlowCaseID"></param>
    ''' <param name="FlowLogBatNo"></param>
    ''' <param name="FlowCurrLogID"></param>
    ''' <param name="FlowStepID"></param>
    ''' <param name="FlowStepAction"></param>
    ''' <param name="FlowStepOpinion"></param>
    ''' <param name="ActUser"></param>
    ''' <param name="LogRemark"></param>
    ''' <param name="UpdateKind"></param>
    ''' <param name="tran"></param>
    ''' <remarks></remarks>
    Private Sub Update_FlowFullLog(ByVal FlowID As String, ByVal FlowVer As String, ByVal FlowCaseID As String, _
            ByVal FlowLogBatNo As Integer, ByVal FlowCurrLogID As String, ByVal FlowStepID As String, ByVal FlowStepAction As String, _
            ByVal FlowStepOpinion As String, ByVal ActUser As String, ByVal LogRemark As String, _
            ByVal UpdateKind As String, ByVal tran As DbTransaction)

        Dim strSQL As New StringBuilder

        If UpdateKind = "" OrElse UpdateKind = "1" Then
            strSQL.AppendLine("UPDATE  WF_FlowFullLog SET  ")
            strSQL.AppendLine("     FlowStepAction = @FlowStepAction, ")
            strSQL.AppendLine("     FlowStepOpinion = @FlowStepopinion, ")
            strSQL.AppendLine("     ToBr = @ToBr, ")
            strSQL.AppendLine("     ToBrName = @ToBrName, ")
            strSQL.AppendLine("     ToUser = @ToUser, ")
            strSQL.AppendLine("     ToUserName = @ToUserName, ")
            strSQL.AppendLine("     IsProxy = @IsProxy, ")
            strSQL.AppendLine("     FlowLogStatus = 'Close', ")
            strSQL.AppendLine("     UpdDate = Getdate(),")
            strSQL.AppendLine("     LogRemark = @LogRemark")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And FlowLogID = @FlowLogID")

            Using dt As DataTable = GetUserInfo(ActUser)
                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), _
                    New DbParameter() { _
                    Bsp.DB.getDbParameter("@FlowStepAction", FlowStepAction), _
                    Bsp.DB.getDbParameter("@FlowStepopinion", FlowStepOpinion), _
                    Bsp.DB.getDbParameter("@ToBr", dt.Rows(0).Item("DeptID").ToString), _
                    Bsp.DB.getDbParameter("@ToBrName", dt.Rows(0).Item("DeptName").ToString), _
                    Bsp.DB.getDbParameter("@ToUser", ActUser), _
                    Bsp.DB.getDbParameter("@ToUserName", dt.Rows(0).Item("UserName").ToString), _
                    Bsp.DB.getDbParameter("@IsProxy", WF_IsProxy(FlowID, FlowCaseID, FlowCurrLogID, ActUser, tran)), _
                    Bsp.DB.getDbParameter("@LogRemark", LogRemark), _
                    Bsp.DB.getDbParameter("@FlowID", FlowID), _
                    Bsp.DB.getDbParameter("@FlowCaseID", FlowCaseID), _
                    Bsp.DB.getDbParameter("@FlowLogID", FlowCurrLogID)}, tran)
            End Using
        End If

        '下面將同批號的close
        If UpdateKind = "" OrElse UpdateKind = "2" Then
            If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)

            strSQL.AppendLine("Update WF_FlowFullLog Set ")
            strSQL.AppendLine("     FlowLogStatus = 'Close' ")
            strSQL.AppendLine("where FlowID = " & Bsp.Utility.Quote(FlowID))
            strSQL.AppendLine("and FlowCaseID = " & Bsp.Utility.Quote(FlowCaseID))
            strSQL.AppendLine("and FlowLogBatNo = " & FlowLogBatNo.ToString())
            strSQL.AppendLine("and FlowStepID = " & Bsp.Utility.Quote(FlowStepID))

            Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)
        End If
    End Sub

    ''' <summary>
    ''' 新增流程關聯檔
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowCaseID"></param>
    ''' <param name="ParentFlowID"></param>
    ''' <param name="ParentFlowCaseID"></param>
    ''' <param name="ParentFlowStepID"></param>
    ''' <param name="ParentFlowLogBatNo"></param>
    ''' <param name="FlowCaseStatus"></param>
    ''' <param name="tran"></param>
    ''' <remarks></remarks>
    Private Sub Insert_FlowRelation(ByVal FlowID As String, ByVal FlowCaseID As String, ByVal ParentFlowID As String, ByVal ParentFlowCaseID As String, ByVal ParentFlowStepID As String, ByVal ParentFlowLogBatNo As Integer, ByVal FlowCaseStatus As String, ByVal tran As DbTransaction)
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine("Insert into WF_FlowRelation")
        strSQL.AppendLine("(FlowID, FlowCaseID, ParentFlowID, ParentFlowCaseID, ParentFlowStepID, ParentFlowLogBatNo, FlowCaseStatus)")
        strSQL.AppendLine("Values")
        strSQL.AppendLine("(@FlowID, @FlowCaseID, @ParentFlowID, @ParentFlowCaseID, @ParentFlowStepID, @ParentFlowLogBatNo, @FlowCaseStatus)")

        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), New DbParameter() { _
            Bsp.DB.getDbParameter("@FlowID", FlowID), _
            Bsp.DB.getDbParameter("@FlowCaseID", FlowCaseID), _
            Bsp.DB.getDbParameter("@ParentFlowID", ParentFlowID), _
            Bsp.DB.getDbParameter("@ParentFlowCaseID", ParentFlowCaseID), _
            Bsp.DB.getDbParameter("@ParentFlowStepID", ParentFlowStepID), _
            Bsp.DB.getDbParameter("@ParentFlowLogBatNo", ParentFlowLogBatNo), _
            Bsp.DB.getDbParameter("@FlowCaseStatus", FlowCaseStatus)}, tran)
    End Sub

    ''' <summary>
    ''' 異動流程關係檔內的狀態
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowCaseID"></param>
    ''' <param name="Status"></param>
    ''' <param name="tran"></param>
    ''' <remarks></remarks>
    Private Sub Update_FlowRelation(ByVal FlowID As String, ByVal FlowCaseID As String, ByVal Status As String, ByVal tran As DbTransaction)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("UPDATE WF_FlowRelation SET")
        strSQL.AppendLine("     FlowCaseStatus = @Status")
        strSQL.AppendLine("WHERE (FlowID = @FlowID) AND (FlowCaseID = @FlowCaseID) ")

        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), New DbParameter() { _
            Bsp.DB.getDbParameter("@Status", Status), _
            Bsp.DB.getDbParameter("@FlowID", FlowID), _
            Bsp.DB.getDbParameter("@FlowCaseID", FlowCaseID)}, tran)
    End Sub

    ''' <summary>
    ''' 檢查是否為代理人
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowCaseID"></param>
    ''' <param name="FlowLogID"></param>
    ''' <param name="ActUser"></param>
    ''' <param name="tran"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function WF_IsProxy(ByVal FlowID As String, ByVal FlowCaseID As String, ByVal FlowLogID As String, ByVal ActUser As String, ByVal tran As DbTransaction) As String
        Dim strSQL As New StringBuilder
        Dim rtnValue As String = "Y"

        strSQL.AppendLine("Select AssignTo From WF_FlowFullLog")
        strSQL.AppendLine("Where FlowID = @FlowID")
        strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
        strSQL.AppendLine("And FlowLogID = @FlowLogID")

        Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), New DbParameter() { _
            Bsp.DB.getDbParameter("@FlowID", FlowID), _
            Bsp.DB.getDbParameter("@FlowCaseID", FlowCaseID), _
            Bsp.DB.getDbParameter("@FlowLogID", FlowLogID)}, tran).Tables(0)
            If dt.Rows.Count > 0 Then
                If ActUser.Trim() = dt.Rows(0).Item("AssignTo").ToString().Trim() Then
                    rtnValue = "N"
                End If
            End If
            Return rtnValue
        End Using
    End Function

    ''' <summary>
    ''' 檢查是否送下關處理
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowCaseID"></param>
    ''' <param name="FlowVer"></param>
    ''' <param name="FlowLogBatNo"></param>
    ''' <param name="FlowStepID"></param>
    ''' <param name="FlowStepAction"></param>
    ''' <param name="tran"></param>
    ''' <returns></returns>
    ''' <remarks>目前沒用到多人同時簽核,故Default回傳True</remarks>
    Public Function CheckGoNextStep(ByVal FlowID As String, ByVal FlowCaseID As String, ByVal FlowVer As String, ByVal FlowLogBatNo As String, ByVal FlowStepID As String, ByVal FlowStepAction As String, ByVal tran As DbTransaction) As Boolean
        Select Case FlowID & "-" & FlowVer & "-" & FlowStepID
            Case "AP-1-S010"
                If checkAllGroupUserResponse(FlowID, FlowCaseID, FlowLogBatNo, tran) Then
                    Return True
                Else
                    Return False
                End If
            Case "CM-1-CM02"
                If FlowStepAction = "退回維護人員" Then
                    Return True
                Else
                    If CheckAllGroupUserResponse(FlowID, FlowCaseID, FlowLogBatNo, tran) Then
                        Return True
                    Else
                        Return False
                    End If
                End If
            Case Else
                Return True
        End Select
        Return True
    End Function

    Private Function CheckAllGroupUserResponse(ByVal FlowID As String, ByVal FlowCaseID As String, ByVal FlowLogBatNo As String, ByVal tran As DbTransaction) As Boolean
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select Count(*) AllCount, Sum(Case When FlowStepAction <> '' Then 1 Else 0 End) ResponseCount")
        strSQL.AppendLine("From WF_FlowFullLog with (nolock)")
        strSQL.AppendLine("Where FlowID = @FlowID")
        strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
        strSQL.AppendLine("And FlowLogBatNo = @FlowLogBatNo")

        Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), _
            New DbParameter() { _
            Bsp.DB.getDbParameter("@FlowID", FlowID), _
            Bsp.DB.getDbParameter("@FlowCaseID", FlowCaseID), _
            Bsp.DB.getDbParameter("@FlowLogBatNo", FlowLogBatNo)}, tran).Tables(0)

            If CInt(dt.Rows(0).Item(1)) > 0 Then
                If dt.Rows(0).Item("AllCount") = dt.Rows(0).Item("ResponseCount") Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        End Using
    End Function

    ''' <summary>
    ''' 取得新的FlowCase ID
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="tran"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetFlowCaseID(ByVal FlowID As String, ByVal tran As DbTransaction) As String
        Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.StoredProcedure, "SP_GetNewFlowCaseID", _
            New DbParameter() { _
            Bsp.DB.getDbParameter("@FlowID", FlowID)}, tran).Tables(0)
            If dt.Rows.Count > 0 Then
                Return Bsp.Utility.IsStringNull(dt.Rows(0).Item(0))
            Else
                Return ""
            End If
        End Using
    End Function

    ''' <summary>
    ''' 取的下一批號
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowCaseID"></param>
    ''' <param name="tran"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetFlowLogBatNo(ByVal FlowID As String, ByVal FlowCaseID As String, ByVal tran As DbTransaction) As String
        Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.StoredProcedure, "SP_GetFlowLogBatNo", _
                    New DbParameter() { _
                    Bsp.DB.getDbParameter("@FlowID", FlowID), Bsp.DB.getDbParameter("@FlowCaseID", FlowCaseID)}, tran).Tables(0)
            If dt.Rows.Count > 0 Then
                Return Bsp.Utility.IsStringNull(dt.Rows(0).Item(0), "1")
            Else
                Return "1"
            End If
        End Using
    End Function

    ''' <summary>
    ''' 取得最新的FlowLogID
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowCaseID"></param>
    ''' <param name="tran"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetFlowLogID(ByVal FlowID As String, ByVal FlowCaseID As String, ByVal tran As DbTransaction) As String
        Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.StoredProcedure, "SP_GetFlowLogID", _
            New DbParameter() { _
            Bsp.DB.getDbParameter("@FlowID", FlowID), Bsp.DB.getDbParameter("@FlowCaseID", FlowCaseID)}, tran).Tables(0)
            If dt.Rows.Count > 0 Then
                Return Bsp.Utility.IsStringNull(dt.Rows(0).Item(0))
            Else
                Return ""
            End If
        End Using
    End Function

    ''' <summary>
    ''' 檢查是否有子流程
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowCaseID"></param>
    ''' <param name="FlowCaseStatus"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ExistChildFlow(ByVal FlowID As String, ByVal FlowCaseID As String, Optional ByVal FlowCaseStatus As String = "") As Boolean
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select Count(*) From WF_FlowRelation with (nolock)")
        strSQL.AppendLine("Where ParentFlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("And ParentFlowCaseID = " & Bsp.Utility.Quote(FlowCaseID))

        If FlowCaseStatus <> "" Then
            strSQL.AppendLine("And FlowCaseStatus = " & Bsp.Utility.Quote(FlowCaseStatus))
        End If

        If CInt(Bsp.DB.ExecuteScalar(strSQL.ToString())) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' 建立ToDoList
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowCaseID"></param>
    ''' <param name="tran"></param>
    ''' <remarks></remarks>
    Public Sub PutFlowToDoList(ByVal FlowID As String, ByVal FlowCaseID As String, ByVal tran As DbTransaction)
        Bsp.DB.ExecuteNonQuery(CommandType.StoredProcedure, "SP_PutFlowToDoList", _
                New DbParameter() { _
                Bsp.DB.getDbParameter("@FlowID", FlowID), _
                Bsp.DB.getDbParameter("@FlowCaseID", FlowCaseID)}, tran)
    End Sub

    ''' <summary>
    ''' 建立追蹤清單
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowCaseID"></param>
    ''' <param name="TraceUserID"></param>
    ''' <param name="tran"></param>
    ''' <remarks></remarks>
    Private Sub Insert_TraceList(ByVal FlowID As String, ByVal FlowCaseID As String, ByVal TraceUserID As String, ByVal tran As DbTransaction)
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine("Insert into  WF_TraceList(FlowID, FlowCaseID, TraceUserID, TraceDate)")
        strSQL.AppendLine("Values (@FlowID, @FlowCaseID, @TraceUserID, GetDate())")

        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), New DbParameter() { _
            Bsp.DB.getDbParameter("@FlowID", FlowID), _
            Bsp.DB.getDbParameter("@FlowCaseID", FlowCaseID), _
            Bsp.DB.getDbParameter("@TraceUserID", TraceUserID)}, tran)
    End Sub

    ''' <summary>
    ''' 刪除追蹤清單
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowCaseID"></param>
    ''' <param name="TraceUserID"></param>
    ''' <param name="tran"></param>
    ''' <remarks></remarks>
    Private Sub Delete_TraceList(ByVal FlowID As String, ByVal FlowCaseID As String, ByVal TraceUserID As String, Optional ByVal tran As DbTransaction = Nothing)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Delete From WF_TraceList ")
        strSQL.AppendLine("Where FlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("And FlowCaseID = " & Bsp.Utility.Quote(FlowCaseID))
        strSQL.AppendLine("And TraceUserID = " & Bsp.Utility.Quote(TraceUserID))

        If tran Is Nothing Then
            Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString())
        Else
            Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)
        End If
    End Sub


    Private Sub Delete_FlowToDoList(ByVal FlowID As String, ByVal FlowCaseID As String, ByVal FlowLogID As String, ByVal tran As DbTransaction)
        Dim strSQL As New StringBuilder

        strSQL.AppendLine("Delete From WF_FlowToDoList")
        strSQL.AppendLine("Where FlowID = @FlowID")
        strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
        strSQL.AppendLine("And FlowLogID = @FlowLogID")

        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), _
            New DbParameter() { _
            Bsp.DB.getDbParameter("@FlowID", FlowID), _
            Bsp.DB.getDbParameter("@FlowCaseID", FlowCaseID), _
            Bsp.DB.getDbParameter("@FlowLogID", FlowLogID)}, tran)
    End Sub
#End Region

#Region "WFA020 : 案件流程處理按扭送出後作業"
    Public Sub FlowStepSubmitAction(ByVal FlowID As String, ByVal FlowVer As String, ByVal FlowStepID As String, _
        ByVal ButtonName As String, ByVal FlowCaseID As String, ByVal tran As DbTransaction, ByVal ParamArray Params As Object())

        'Dim a As Type = Me.GetType()
        'Dim m As System.Reflection.MethodInfo = a.GetMethod(String.Format("After_{0}_{1}_{2}", FlowID, FlowVer, FlowStepID))
        'm.Invoke(a, New Object() {FlowID, FlowVer, FlowStepID, ButtonName, FlowCaseID, tran, Params})

        Select Case FlowID & "-" & FlowVer & "-" & FlowStepID
            Case "AP-1-S001"
                AfterActS001(FlowID, FlowVer, FlowStepID, ButtonName, FlowCaseID, tran, Params)
            Case "AP-1-S002"
                AfterActS002(FlowID, FlowVer, FlowStepID, ButtonName, FlowCaseID, tran, Params)
            Case "AP-1-S003"
                AfterActS003(FlowID, FlowVer, FlowStepID, ButtonName, FlowCaseID, tran, Params)
            Case "AP-1-S004"
                AfterActS004(FlowID, FlowVer, FlowStepID, ButtonName, FlowCaseID, tran, Params)
            Case "AP-1-S005"
                AfterActS005(FlowID, FlowVer, FlowStepID, ButtonName, FlowCaseID, tran, Params)
            Case "AP-1-S006"
                AfterActS006(FlowID, FlowVer, FlowStepID, ButtonName, FlowCaseID, tran, Params)
            Case "AP-1-S007"
                AfterActS007(FlowID, FlowVer, FlowStepID, ButtonName, FlowCaseID, tran, Params)
            Case "AP-1-S010"
                AfterActS010(FlowID, FlowVer, FlowStepID, ButtonName, FlowCaseID, tran, Params)
            Case "AP-1-S011"
                AfterActS011(FlowID, FlowVer, FlowStepID, ButtonName, FlowCaseID, tran, Params)
            Case "AP-1-S012"
                AfterActS012(FlowID, FlowVer, FlowStepID, ButtonName, FlowCaseID, tran, Params)
            Case "CC-1-CC01"
                AfterActCC01(FlowID, FlowVer, FlowStepID, ButtonName, FlowCaseID, tran, Params)
            Case "CC-1-CC04"
                AfterActCC04(FlowID, FlowVer, FlowStepID, ButtonName, FlowCaseID, tran, Params)
            Case "CC-1-CC05"
                AfterActCC05(FlowID, FlowVer, FlowStepID, ButtonName, FlowCaseID, tran, Params)
            Case "DD-1-DD01"
                AfterActDD01(FlowID, FlowVer, FlowStepID, ButtonName, FlowCaseID, tran, Params)
            Case "DD-1-DD02"
                AfterActDD02(FlowID, FlowVer, FlowStepID, ButtonName, FlowCaseID, tran, Params)

            Case "CM-1-CM01"
                AfterActCM01(FlowID, FlowVer, FlowStepID, ButtonName, FlowCaseID, tran, Params)
            Case "CM-1-CM02"
                AfterActCM02(FlowID, FlowVer, FlowStepID, ButtonName, FlowCaseID, tran, Params)
            Case "CM-1-CM03"
                AfterActCM03(FlowID, FlowVer, FlowStepID, ButtonName, FlowCaseID, tran, Params)
        End Select
    End Sub

    
    '更新覆審報告資料狀態
    Public Sub UpCR_ProfileStatus(ByVal CRID As String, ByVal Status As String, ByVal CondStr As String, ByVal tran As DbTransaction)
        Dim strSQL As New StringBuilder

        strSQL.AppendLine("update   CR_Profile set Status = " & Bsp.Utility.Quote(Status) & ",LastChgID = " & Bsp.Utility.Quote(UserProfile.ActUserID) & ",LastChgDate = getdate() ")
        strSQL.AppendLine(" WHERE     (CRID = " & Bsp.Utility.Quote(CRID) & ") " & CondStr & " ")

        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)
    End Sub

    Private Sub CheckCPRMForCR(ByVal CRID As String, ByVal tran As DbTransaction)
        'If QryCPRM(CRID, tran) <> "" Then
        Dim strSQL As New StringBuilder()

        strSQL.Append("Update CR_Profile Set CloseDept = '1' Where CRID = " & Bsp.Utility.Quote(CRID))
        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)
        'End If
    End Sub

    '更新CR_Profile之主覆審AO,覆審人員
    Public Sub UpCR_ProfileAOCR(ByVal CRID As String, ByVal CRUser As String, ByVal AOID As String, ByVal Condstr As String, ByVal tran As DbTransaction)
        Dim strSQL As New StringBuilder
        strSQL.AppendLine("update   CR_Profile set ")
        If CRUser.Trim <> "" Then
            strSQL.AppendLine("CRUser = " & Bsp.Utility.Quote(CRUser) & ",")
        End If
        If AOID.Trim <> "" Then
            strSQL.AppendLine("AOID = " & Bsp.Utility.Quote(AOID) & ",")
            Using dt As DataTable = WFGetUserInfo(AOID).Tables(0)
                If dt.Rows.Count > 0 Then
                    strSQL.AppendLine("AODeptID = " & Bsp.Utility.Quote(dt.Rows(0).Item("DeptID").ToString.Trim) & ",")
                Else
                    strSQL.AppendLine("AODeptID = '',")
                End If
            End Using
        End If
        If CRUser.Trim = "" And AOID.Trim = "" Then
            strSQL.AppendLine(" CRUser = '' , AOID = '', AODeptID='', ")
        End If
        strSQL.AppendLine("LastChgID = '" & UserProfile.ActUserID & "',LastChgDate = getdate() ")
        strSQL.AppendLine(" WHERE (CRID = " & Bsp.Utility.Quote(CRID) & ") " & Condstr & " ")

        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)
    End Sub

    '更新CR_ProfitAO
    Public Sub UpCR_ProfitAO(ByVal CRID As String, ByVal DoingFlag As String, ByVal AOID As String, ByVal AODeptID As String, ByVal DescAO As String, ByVal Condstr As String, ByVal tran As DbTransaction)
        Dim strSQL As New StringBuilder
        strSQL.AppendLine("update  CR_ProfitAO set ")
        strSQL.AppendLine("DoingFlag = " & Bsp.Utility.Quote(DoingFlag) & ",")
        If DoingFlag = "1" Then
            strSQL.AppendLine("DoingDate = getdate(),")
        ElseIf DoingFlag = "2" Then
            strSQL.AppendLine("DoingDate = getdate(),")
        Else
            strSQL.AppendLine("DoingDate = NULL,")
        End If
        If AOID.Trim <> "" Then
            strSQL.AppendLine("AOUserID = " & Bsp.Utility.Quote(AOID) & ",")
        End If
        If AODeptID.Trim <> "" Then
            strSQL.AppendLine("AODeptID = " & Bsp.Utility.Quote(AODeptID) & ",")
        End If
        If DescAO.Trim <> "" Then
            strSQL.AppendLine("DescAO = " & Bsp.Utility.Quote(DescAO) & ",")
        End If
        strSQL.AppendLine("LastChgID = " & Bsp.Utility.Quote(UserProfile.ActUserID) & ",LastChgDate = getdate() ")
        strSQL.AppendLine(" WHERE (CRID = " & Bsp.Utility.Quote(CRID) & ") " & Condstr & " ")

        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)
    End Sub

    '建立CR_ProfitAO
    Public Sub Ins_CR_ProfitAO(ByVal CRID As String, ByVal AOUserID As String, ByVal tran As DbTransaction)
        Dim strSQL As New StringBuilder
        strSQL.AppendLine("insert into CR_ProfitAO (CRID, AOUserID, FlowSeq, AODeptID, DoingFlag, LastChgID, LastChgDate) values(")
        strSQL.AppendLine(Bsp.Utility.Quote(CRID) & ",")
        strSQL.AppendLine(Bsp.Utility.Quote(AOUserID) & ",")
        strSQL.AppendLine("ISNULL((SELECT MAX(FlowSeq) FROM CR_ProfitAO WHERE CRID = " & Bsp.Utility.Quote(CRID) & "),0) + 1,")
        Using dt As DataTable = WFGetUserInfo(AOUserID).Tables(0)
            strSQL.AppendLine("'" & dt.Rows(0).Item("DeptID").ToString & "',")
        End Using
        strSQL.AppendLine("'0',")
        strSQL.AppendLine(Bsp.Utility.Quote(UserProfile.UserID) & ",")
        strSQL.AppendLine(" getdate())")

        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)
    End Sub

    '為了只有一筆AppID,做結案時呼叫，強制銷掉其他同案件及其他同客戶之ID
    Public Sub WF_SP_ImpProfitAO(ByVal CRID As String, ByVal tran As DbTransaction)
        Bsp.DB.ExecuteNonQuery(CommandType.StoredProcedure, "SP_ImpProfitAO", _
                New DbParameter() { _
                Bsp.DB.getDbParameter("@CRID", CRID), _
                Bsp.DB.getDbParameter("@UserID", UserProfile.ActUserID)}, tran)

    End Sub

    '退回重新覆審更新CR_ProfitAO,將所有的資料清空
    Public Sub UpCR_ProfitReAO(ByVal CRID As String, ByVal Condstr As String, ByVal tran As DbTransaction)
        Dim strSQL As New StringBuilder
        strSQL.AppendLine("UPDATE CR_ProfitAO set ")
        strSQL.AppendLine("DoingFlag = '0',")
        strSQL.AppendLine("DoingDate = NULL,")
        strSQL.AppendLine("DescAO = NULL,")
        strSQL.AppendLine("LastChgID = '" & UserProfile.ActUserID & "',LastChgDate = getdate() ")
        strSQL.AppendLine(" WHERE (CRID = '" & CRID & "') " & Condstr & " ")

        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)
    End Sub

    '查詢覆審AO
    Function QryCRAOID(ByVal CRID As String, ByVal Condstr As String, Optional ByVal tran As DbTransaction = Nothing) As String
        If Condstr.Trim = "" Then
            Condstr = " and CRID = '" & CRID & "' "
        Else
            Condstr = " and CRID = '" & CRID & "' " & Condstr
        End If

        Using dt As DataTable = QryCR_Profile(Condstr, tran).Tables(0)
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item("AOID").ToString
            Else
                Return ""
            End If
        End Using
    End Function

    '覆審CR_Profile查詢
    Function QryCR_Profile(ByVal Condstr As String, Optional ByVal tran As DbTransaction = Nothing) As DataSet
        Dim SelectCommandStr As String = " SELECT * FROM CR_Profile with (nolock)  "
        SelectCommandStr &= " WHERE 1=1 " & Condstr & " "

        If tran Is Nothing Then
            Return Bsp.DB.ExecuteDataSet(CommandType.Text, SelectCommandStr)
        Else
            Return Bsp.DB.ExecuteDataSet(CommandType.Text, SelectCommandStr, tran)
        End If
    End Function

    '查詢覆審AODepID
    Function QryCRAODeptID(ByVal CRID As String, ByVal Condstr As String, Optional ByVal tran As DbTransaction = Nothing) As String
        If Condstr.Trim = "" Then
            Condstr = " and CRID = '" & CRID & "' "
        Else
            Condstr = " and CRID = '" & CRID & "' " & Condstr
        End If

        Using dt As DataTable = QryCR_Profile(Condstr, tran).Tables(0)
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item("AODeptID").ToString
            Else
                Return ""
            End If
        End Using
    End Function

    '更新CR_Profile之撤件原因
    Public Sub UpCR_ProfileAOCancel(ByVal CRID As String, ByVal CancelFlag As String, ByVal DescCancel As String, ByVal Condstr As String, ByVal tran As DbTransaction)
        Dim strSQL As New StringBuilder
        strSQL.AppendLine("UPDATE CR_Profile set ")
        strSQL.AppendLine("CancelFlag = " & Bsp.Utility.Quote(CancelFlag) & ",")
        strSQL.AppendLine("CancelDesc = " & Bsp.Utility.Quote(DescCancel) & ",")
        strSQL.AppendLine("LastChgID = " & Bsp.Utility.Quote(UserProfile.ActUserID) & ",LastChgDate = getdate() ")
        strSQL.AppendLine("WHERE (CRID = " & Bsp.Utility.Quote(CRID) & ") " & Condstr & " ")

        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)
    End Sub

    '查詢AO
    Public Function QryCRIDtoAoID(ByVal CRID As String, ByVal tran As DbTransaction) As String
        Dim strSQL As New StringBuilder
        strSQL.AppendLine("SELECT isnull(aoempno,'')  FROM  DB_CDNJ.dbo.db_cdcif_dds WHERE  rtrim(idno) = (SELECT CustomerID AS EXPR1 FROM CR_Profile WHERE CRID = " & Bsp.Utility.Quote(CRID) & ") ")

        Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), tran).Tables(0)
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item(0).ToString
            Else
                Return ""
            End If
        End Using
    End Function

    '案件流程處理CM01關卡送出後作業
    Public Sub AfterActCM01(ByVal FLowID As String, ByVal FLowVer As String, ByVal FlowStepID As String, ByVal FlowStepCnd As String, ByVal FlowCaseID As String, ByVal tran As DbTransaction, ByVal ParamArray ArrValue As Object())
        Dim CMNO As String = ""
        Dim AssignTo As String = ""
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ArrValue)

        For Each strKey As String In ht.Keys
            Select Case strKey
                Case "CMNO"
                    CMNO = ht(strKey)
                Case "AssignTo"
                    AssignTo = ht(strKey)
            End Select
        Next

        Select Case FlowStepCnd
            Case "撤銷"
                UpdCM_ProfileStatus(CMNO, "0", tran)
            Case "參閱"
                UpdCM_ProfileStatus(CMNO, "2", tran)
                'UpdCM_VerifyEmailFg(CMNO, AssignTo, "Y", tran)
            Case "審核"
                CheckCM01(CMNO)
                UpdCM_ProfileStatus(CMNO, "2", tran)
                'UpdCM_VerifyEmailFg(CMNO, AssignTo, "Y", tran)
        End Select

    End Sub

    '案件流程處理CM02關卡送出後作業
    Public Sub AfterActCM02(ByVal FlowID As String, ByVal FlowVer As String, ByVal FlowStepID As String, ByVal FlowStepCnd As String, ByVal FlowCaseID As String, ByVal tran As DbTransaction, ByVal ParamArray ArrValue As Object())
        Dim CMNO As String = ""
        Dim AssignTo As String = ""
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ArrValue)

        For Each strKey As String In ht.Keys
            Select Case strKey
                Case "CMNO"
                    CMNO = ht(strKey)
                Case "AssignTo"
                    AssignTo = ht(strKey)
            End Select
        Next

        Select Case FlowStepCnd
            Case "退回維護人員"
                UpdCM_ProfileStatus(CMNO, "1", tran)
                UpdCM_VerifyForReturn(CMNO, tran)
            Case "審核"
                If GetCMNextCheckerID(CMNO) = "" Then
                    'UpdCM_VerifyEmailFg(CMNO, AssignTo, "Y", tran)
                End If
                UpdCM_VerifyVstatus(CMNO, UserProfile.UserID, "Y", tran)
        End Select

    End Sub

    '案件流程處理CM03關卡送出後作業
    Public Sub AfterActCM03(ByVal FLowID As String, ByVal FLowVer As String, ByVal FlowStepID As String, ByVal FlowStepCnd As String, ByVal FlowCaseID As String, ByVal tran As DbTransaction, ByVal ParamArray ArrValue As Object())
        Dim CMNO As String = ""
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ArrValue)

        For Each strKey As String In ht.Keys
            Select Case strKey
                Case "CMNO"
                    CMNO = ht(strKey)
            End Select
        Next

        Select Case FlowStepCnd
            Case "退回維護人員"
                UpdCM_ProfileStatus(CMNO, "1", tran)
                UpdCM_VerifyForReturn(CMNO, tran)
            Case "核閱完成"
                UpdCM_ProfileStatus(CMNO, "9", tran)
                UpdCM_VerifyVstatus(CMNO, UserProfile.UserID, "Y", tran)
                'Ins_CS_Profile(CMNO, "01", tran)

        End Select
    End Sub

    '更新訪談報告資料狀態
    Public Sub UpdCM_ProfileStatus(ByVal CMNO As String, ByVal Status As String, ByVal tran As DbTransaction, Optional ByVal CondStr As String = "")
        Dim strSQL As New StringBuilder

        strSQL.AppendLine("Update CM_Profile Set")
        strSQL.AppendLine("     Status = " & Bsp.Utility.Quote(Status) & ",")
        strSQL.AppendLine("     UpdateDate = GetDate(), ")
        strSQL.AppendLine("     UpdateUser = " & Bsp.Utility.Quote(UserProfile.ActUserID))
        strSQL.AppendLine("Where CMNO = " & Bsp.Utility.Quote(CMNO))
        strSQL.AppendLine(CondStr)

        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)
    End Sub

    '更新CM_Verify的Vstatus
    Private Sub UpdCM_VerifyForReturn(ByVal CMNO As String, ByVal tran As DbTransaction)
        Dim strSQL As New StringBuilder

        strSQL.AppendLine("Update CM_Verify Set")
        strSQL.AppendLine("     SignStatus = 'N',")
        strSQL.AppendLine("     EmailFg = 'N'")
        strSQL.AppendLine("Where CMNO = " & Bsp.Utility.Quote(CMNO))

        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)
    End Sub

    '更新CM_Verify的Vstatus
    Private Sub UpdCM_VerifyVstatus(ByVal CMNO As String, ByVal VerifyUser As String, ByVal Status As String, ByVal tran As DbTransaction)
        Dim strSQL As New StringBuilder

        strSQL.AppendLine("Update CM_Verify Set")
        strSQL.AppendLine("     SignStatus = " & Bsp.Utility.Quote(Status))
        strSQL.AppendLine("Where CMNO = " & Bsp.Utility.Quote(CMNO))
        strSQL.AppendLine("And SignUser = " & Bsp.Utility.Quote(VerifyUser))

        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)
    End Sub

    'Public Shared Sub After_RL_1_RL01(ByVal FlowID As String, ByVal FlowVer As String, ByVal FlowStepID As String, ByVal ButtonName As String, ByVal FlowCaseID As String, ByVal tran As DbTransaction, ByVal ParamArray Params As Object())
    '    Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
    '    Dim RLID As String = ""

    '    For Each s As String In ht.Keys
    '        Select Case s
    '            Case "RLID"
    '                RLID = Bsp.Utility.IsStringNull(ht(s))
    '        End Select
    '    Next

    '    Select Case ButtonName
    '        Case "撤銷"
    '            Dim objRL As New RL()
    '            objRL.Cancel_RelateFlow(RLID, tran)
    '    End Select
    'End Sub

#Region "AP : 授信案件"
    Private Sub AfterActS001(ByVal FlowID As String, ByVal FlowVer As String, ByVal FlowStepID As String, ByVal ButtonName As String, ByVal FlowCaseID As String, ByVal tran As DbTransaction, ByVal ParamArray Params As Object())
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim AppID As String = ""
        Dim AssignTo As String = ""

        For Each s As String In ht.Keys
            Select Case s
                Case "AssignTo"
                    AssignTo = Bsp.Utility.IsStringNull(ht(s))
                Case "AppID"
                    AppID = Bsp.Utility.IsStringNull(ht(s))
            End Select
        Next

        Select Case ButtonName
            Case "簽核主管審核"
                UpdNextLoanCheckdateBeg(FlowCaseID, AssignTo, " AND SetupStepID = 'S001'", tran)
                Update_AP_Profile_Status(AppID, "4", "", tran)
                Update_AL_Profile_Status(AppID, "3", tran)
                Update_AL_Profile_ExRateDate(AppID, tran)
                Update_BR_Customer_Status(AppID, "4", "And AppStatus not in ('5','7','9')", tran)
                Update_CA_Profile_Status(AppID, "3", "And Status not in ('0','9')", tran)
                'Update_SP_Profile_Status(AppID, "3", "And Status not in ('0','9')", tran)
                'AL.ALSyncCompLimit(AppID, tran)        '寫入/更新公司額度=額度科目流用加總

                '送出後重新計算客戶評級
                Bsp.DB.ExecuteNonQuery(CommandType.StoredProcedure, "SP_GetScore_All", New DbParameter() { _
                                           Bsp.DB.getDbParameter("@argAppID", AppID), _
                                           Bsp.DB.getDbParameter("@argLastChgID", UserProfile.ActUserID)}, tran)

            Case "銷案"
                Update_AP_Profile_Status(AppID, "9", "And AppStatus not in ('0','9') ", tran)       '更新核貸主檔
                Update_AL_Profile_Status(AppID, "0", tran)       '更新核貸書狀態
                'Update_SP_Profile_Status(AppID, "0", " and Status not in ('0','9') ", tran)       '更新期中報告狀態
                Update_BR_Customer_Status(AppID, "9", " And AppStatus not in ('5','9') ", tran) '更新客戶狀態

        End Select
    End Sub

    Private Sub AfterActS002(ByVal FlowID As String, ByVal FlowVer As String, ByVal FlowStepID As String, ByVal ButtonName As String, ByVal FlowCaseID As String, ByVal tran As DbTransaction, ByVal ParamArray Params As Object())
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim AppID As String = ""
        Dim AssignTo As String = ""

        For Each s As String In ht.Keys
            Select Case s
                Case "AssignTo"
                    AssignTo = Bsp.Utility.IsStringNull(ht(s))
                Case "AppID"
                    AppID = Bsp.Utility.IsStringNull(ht(s))
            End Select
        Next

        Select Case ButtonName
            Case "退回AO需重審"
                WF_LoanCheckerLogIns(FlowCaseID, "0", UserProfile.ActUserID, "退回AO-刪除", "", tran)
                UpdBackLoanCheck(FlowCaseID, "  ", tran)

                Update_AP_Profile_Status(AppID, "1", "", tran)
                Update_AL_Profile_Status(AppID, "2", tran)
                Update_BR_Customer_Status(AppID, "1", "And AppStatus = '4'", tran)
                'Update_SP_Profile_Status(AppID, "3", "And Status not in ('0','9')", tran)

            Case "同意"
                UpdNextLoanCheckdateBeg(FlowCaseID, AssignTo, " AND SetupStepID = 'S001'", tran)
            Case "轉呈"
                UpdNextLoanCheckdateBeg(FlowCaseID, AssignTo, " AND SetupStepID = 'S001'", tran)
            Case "同意送審查窗口"

                'Case "核准"

                'Case "不核准或销案"

        End Select
    End Sub

    Private Sub AfterActS003(ByVal FlowID As String, ByVal FlowVer As String, ByVal FlowStepID As String, ByVal ButtonName As String, ByVal FlowCaseID As String, ByVal tran As DbTransaction, ByVal ParamArray Params As Object())
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim AppID As String = ""
        Dim AssignTo As String = ""

        For Each s As String In ht.Keys
            Select Case s
                Case "AssignTo"
                    AssignTo = Bsp.Utility.IsStringNull(ht(s))
                Case "AppID"
                    AppID = Bsp.Utility.IsStringNull(ht(s))
            End Select
        Next

        Select Case ButtonName
            Case "退回AO需重審"
                WF_LoanCheckerLogIns(FlowCaseID, "0", UserProfile.ActUserID, "退回AO-刪除", "", tran)
                UpdBackLoanCheck(FlowCaseID, "  ", tran)

                Update_AP_Profile_Status(AppID, "1", "", tran)
                Update_AL_Profile_Status(AppID, "2", tran)
                Update_BR_Customer_Status(AppID, "1", "And AppStatus = '4'", tran)
                'Update_SP_Profile_Status(AppID, "3", "And Status not in ('0','9')", tran)
            Case "退回AO不需重審"
                Update_AP_Profile_Status(AppID, "1", "", tran)
                Update_AL_Profile_Status(AppID, "2", tran)
                Update_BR_Customer_Status(AppID, "1", "And AppStatus = '4'", tran)
                'Update_SP_Profile_Status(AppID, "1", "And Status not in ('0','9')", tran)
            Case "總行簽核主管"
                UpdNextLoanCheckdateBeg(FlowCaseID, AssignTo, " AND SetupStepID = 'S003'", tran)
                'AL.ALSyncCompLimit(AppID, tran)        '寫入/更新公司額度=額度科目流用加總
            Case "轉呈/分案"
                UpdNextLoanCheckdateBeg(FlowCaseID, AssignTo, " AND SetupStepID = 'S001'", tran)
            Case "核貸書結案"
                'AL.ALSyncCompLimit(AppID, tran)        '寫入/更新公司額度=額度科目流用加總
        End Select
    End Sub

    Private Sub AfterActS004(ByVal FlowID As String, ByVal FlowVer As String, ByVal FlowStepID As String, ByVal ButtonName As String, ByVal FlowCaseID As String, ByVal tran As DbTransaction, ByVal ParamArray Params As Object())
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim AppID As String = ""
        Dim AssignTo As String = ""

        For Each s As String In ht.Keys
            Select Case s
                Case "AssignTo"
                    AssignTo = Bsp.Utility.IsStringNull(ht(s))
                Case "AppID"
                    AppID = Bsp.Utility.IsStringNull(ht(s))
            End Select
        Next

        Select Case ButtonName
            Case "退回AO需重審"
                WF_LoanCheckerLogIns(FlowCaseID, "0", UserProfile.ActUserID, "退回AO-刪除", "", tran)
                UpdBackLoanCheck(FlowCaseID, "  ", tran)

                Update_AP_Profile_Status(AppID, "1", "", tran)
                Update_AL_Profile_Status(AppID, "2", tran)
                Update_BR_Customer_Status(AppID, "1", "And AppStatus = '4'", tran)
                'Update_SP_Profile_Status(AppID, "3", "And Status not in ('0','9')", tran)
            Case "退回審查窗口"
                WF_LoanCheckerLogIns(FlowCaseID, "0", UserProfile.ActUserID, "退回审查窗口-删除", "And SetupStepID = 'S003'", tran)
                UpdBackLoanCheck(FlowCaseID, "And SetupStepID = 'S003' ", tran)
            Case "同意"
                UpdNextLoanCheckdateBeg(FlowCaseID, AssignTo, " AND SetupStepID = 'S003'", tran)
            Case "不同意'"
                UpdNextLoanCheckdateBeg(FlowCaseID, AssignTo, " AND SetupStepID = 'S003'", tran)
            Case "轉呈"
                UpdNextLoanCheckdateBeg(FlowCaseID, AssignTo, " AND SetupStepID = 'S003'", tran)

        End Select
    End Sub

    Private Sub AfterActS005(ByVal FlowID As String, ByVal FlowVer As String, ByVal FlowStepID As String, ByVal ButtonName As String, ByVal FlowCaseID As String, ByVal tran As DbTransaction, ByVal ParamArray Params As Object())
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim AppID As String = ""
        Dim AssignTo As String = ""

        For Each s As String In ht.Keys
            Select Case s
                Case "AssignTo"
                    AssignTo = Bsp.Utility.IsStringNull(ht(s))
                Case "AppID"
                    AppID = Bsp.Utility.IsStringNull(ht(s))
            End Select
        Next

        Select Case ButtonName
            Case "不核准或銷案"
                Update_AP_Profile_Status(AppID, "9", "And AppStatus not in ('0','9') ", tran)       '更新核貸主檔
                Update_AL_Profile_Status(AppID, "0", tran)       '更新核貸書狀態
                'Update_SP_Profile_Status(AppID, "0", " and Status not in ('0','9') ", tran)       '更新期中報告狀態
                Update_BR_Customer_Status(AppID, "9", " And AppStatus not in ('5','9') ", tran) '更新客戶狀態

            Case "核准"
                Update_AP_Profile_Status(AppID, "5", "And AppStatus not in ('0','9') ", tran)       '更新核貸主檔
                Update_AL_Profile_Status(AppID, "9", tran)       '更新核貸書狀態
                'Update_SP_Profile_Status(AppID, "9", " and Status not in ('0','9') ", tran)       '更新期中報告狀態
                Update_BR_Customer_Status(AppID, "5", " And AppStatus not in ('5','9') ", tran) '更新客戶狀態

                Bsp.DB.ExecuteNonQuery(CommandType.StoredProcedure, "SP_ChangeCustTypebyAppID", New DbParameter() { _
                                       Bsp.DB.getDbParameter("@argAppID", AppID), _
                                       Bsp.DB.getDbParameter("@argLastChgID", UserProfile.ActUserID)}, tran)

        End Select
    End Sub

    Private Sub AfterActS006(ByVal FlowID As String, ByVal FlowVer As String, ByVal FlowStepID As String, ByVal ButtonName As String, ByVal FlowCaseID As String, ByVal tran As DbTransaction, ByVal ParamArray Params As Object())
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim AppID As String = ""
        Dim AssignTo As String = ""

        For Each s As String In ht.Keys
            Select Case s
                Case "AssignTo"
                    AssignTo = Bsp.Utility.IsStringNull(ht(s))
                Case "AppID"
                    AppID = Bsp.Utility.IsStringNull(ht(s))
            End Select
        Next

        Select Case ButtonName
            Case "退回AO需重審"
                WF_LoanCheckerLogIns(FlowCaseID, "0", UserProfile.ActUserID, "退回AO-刪除", "", tran)
                UpdBackLoanCheck(FlowCaseID, " ", tran)

                Update_AP_Profile_Status(AppID, "1", "", tran)
                Update_AL_Profile_Status(AppID, "2", tran)
                Update_BR_Customer_Status(AppID, "1", "And AppStatus = '4'", tran)
                'Update_SP_Profile_Status(AppID, "3", "And Status not in ('0','9')", tran)

            Case "退回AO不需重審"
                Update_AP_Profile_Status(AppID, "1", "", tran)
                Update_AL_Profile_Status(AppID, "2", tran)
                Update_BR_Customer_Status(AppID, "1", "And AppStatus = '4'", tran)
                'Update_SP_Profile_Status(AppID, "1", "And Status not in ('0','9')", tran)
            Case "退回審查窗口"
                WF_LoanCheckerLogIns(FlowCaseID, "0", UserProfile.ActUserID, "退回審查窗口-删除", "AND SetupStepID = 'S003'", tran)
                UpdBackLoanCheck(FlowCaseID, "AND SetupStepID = 'S003'", tran)

            Case "送授信審查委員會"
                AssignTo = GetGroupUserInfo(AssignTo)
                UpdNextLoanCheckdateBeg(FlowCaseID, AssignTo.Replace(";", ","), "", tran)
                'AL.ALSyncCompLimit(AppID, tran)
            Case "送簽核主管"
                UpdNextLoanCheckdateBeg(FlowCaseID, AssignTo, "", tran)
                'AL.ALSyncCompLimit(AppID, tran)
            Case "轉呈"

            Case "核貸書結案"
                'AL.ALSyncCompLimit(AppID, tran)
        End Select
    End Sub

    Private Sub AfterActS007(ByVal FlowID As String, ByVal FlowVer As String, ByVal FlowStepID As String, ByVal ButtonName As String, ByVal FlowCaseID As String, ByVal tran As DbTransaction, ByVal ParamArray Params As Object())
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim AppID As String = ""
        Dim AssignTo As String = ""

        For Each s As String In ht.Keys
            Select Case s
                Case "AssignTo"
                    AssignTo = Bsp.Utility.IsStringNull(ht(s))
                Case "AppID"
                    AppID = Bsp.Utility.IsStringNull(ht(s))
            End Select
        Next

        Select Case ButtonName
            Case "審查窗口"
                Update_AP_Profile_Status(AppID, "4", "", tran)
                Update_AL_Profile_Status(AppID, "3", tran)
                Update_AL_Profile_ExRateDate(AppID, tran)
                Update_BR_Customer_Status(AppID, "4", "And AppStatus not in ('5','7','9')", tran)
                Update_CA_Profile_Status(AppID, "3", "And Status not in ('0','9')", tran)
                'Update_SP_Profile_Status(AppID, "3", "And Status not in ('0','9')", tran)

                '送出後重新計算客戶評級
                Bsp.DB.ExecuteNonQuery(CommandType.StoredProcedure, "SP_GetScore_All", New DbParameter() { _
                                           Bsp.DB.getDbParameter("@argAppID", AppID), _
                                           Bsp.DB.getDbParameter("@argLastChgID", UserProfile.ActUserID)})
            Case "審查窗口."
                Update_AP_Profile_Status(AppID, "4", "", tran)
                Update_AL_Profile_Status(AppID, "3", tran)
                Update_AL_Profile_ExRateDate(AppID, tran)
                Update_BR_Customer_Status(AppID, "4", "And AppStatus not in ('5','7','9')", tran)
                Update_CA_Profile_Status(AppID, "3", "And Status not in ('0','9')", tran)
                'Update_SP_Profile_Status(AppID, "3", "And Status not in ('0','9')", tran)

                '送出後重新計算客戶評級
                Bsp.DB.ExecuteNonQuery(CommandType.StoredProcedure, "SP_GetScore_All", New DbParameter() { _
                                           Bsp.DB.getDbParameter("@argAppID", AppID), _
                                           Bsp.DB.getDbParameter("@argLastChgID", UserProfile.ActUserID)})
        End Select
    End Sub

    Private Sub AfterActS010(ByVal FlowID As String, ByVal FlowVer As String, ByVal FlowStepID As String, ByVal ButtonName As String, ByVal FlowCaseID As String, ByVal tran As DbTransaction, ByVal ParamArray Params As Object())
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim AppID As String = ""
        Dim AssignTo As String = ""

        For Each s As String In ht.Keys
            Select Case s
                Case "AssignTo"
                    AssignTo = Bsp.Utility.IsStringNull(ht(s))
                Case "AppID"
                    AppID = Bsp.Utility.IsStringNull(ht(s))
            End Select
        Next

       
    End Sub

    Private Sub AfterActS011(ByVal FlowID As String, ByVal FlowVer As String, ByVal FlowStepID As String, ByVal ButtonName As String, ByVal FlowCaseID As String, ByVal tran As DbTransaction, ByVal ParamArray Params As Object())
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim AppID As String = ""
        Dim AssignTo As String = ""

        For Each s As String In ht.Keys
            Select Case s
                Case "AssignTo"
                    AssignTo = Bsp.Utility.IsStringNull(ht(s))
                Case "AppID"
                    AppID = Bsp.Utility.IsStringNull(ht(s))
            End Select
        Next

        Select Case ButtonName
            Case "簽核主管"
                UpdNextLoanCheckdateBeg(FlowCaseID, AssignTo, "And SetupStepID in ('S006','S011')", tran)
            Case "轉呈"

            Case "核貸書結案"

        End Select
    End Sub

    Private Sub AfterActS012(ByVal FlowID As String, ByVal FlowVer As String, ByVal FlowStepID As String, ByVal ButtonName As String, ByVal FlowCaseID As String, ByVal tran As DbTransaction, ByVal ParamArray Params As Object())
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim AppID As String = ""
        Dim AssignTo As String = ""

        For Each s As String In ht.Keys
            Select Case s
                Case "AssignTo"
                    AssignTo = Bsp.Utility.IsStringNull(ht(s))
                Case "AppID"
                    AppID = Bsp.Utility.IsStringNull(ht(s))
            End Select
        Next

        Select Case ButtonName
            Case "同意送審查編制"

            Case "同意"
                UpdNextLoanCheckdateBeg(FlowCaseID, AssignTo, "And SetupStepID in ('S006','S011') ", tran)
            Case "不同意併送審查編制"

        End Select
    End Sub

#End Region

#Region "CC : 徵信報告"
    Private Sub AfterActCC01(ByVal FlowID As String, ByVal FlowVer As String, ByVal FlowStepID As String, ByVal ButtonName As String, ByVal FlowCaseID As String, ByVal tran As DbTransaction, ByVal ParamArray Params As Object())
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim CCID As String = ""
        Dim AppID As String = ""
        Dim CustomerID As String = ""

        For Each s As String In ht.Keys
            Select Case s
                Case "CCID"
                    CCID = Bsp.Utility.IsStringNull(ht(s))
                Case "AppID"
                    AppID = Bsp.Utility.IsStringNull(ht(s))
                Case "CustomerID"
                    CustomerID = Bsp.Utility.IsStringNull(ht(s))
            End Select
        Next

        Select Case ButtonName
            Case "徵信維護"
                Update_BR_Customer_Status(AppID, "7", "And CustomerID=" & Bsp.Utility.Quote(CustomerID), tran)
            Case "徵信銷案"
                Update_BR_Customer_Status(AppID, "1", "And CustomerID=" & Bsp.Utility.Quote(CustomerID), tran)
                Update_CA_Profile_Status(AppID, "0", "And CustomerID=" & Bsp.Utility.Quote(CustomerID), tran)
                Update_CC_Profile_Status(CCID, "0", tran)
                Update_SP_Profile_Status(AppID, CustomerID, "0", tran)
        End Select
    End Sub

    Private Sub AfterActCC04(ByVal FlowID As String, ByVal FlowVer As String, ByVal FlowStepID As String, ByVal ButtonName As String, ByVal FlowCaseID As String, ByVal tran As DbTransaction, ByVal ParamArray Params As Object())
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim CCID As String = ""
        Dim AppID As String = ""
        Dim CustomerID As String = ""

        For Each s As String In ht.Keys
            Select Case s
                Case "CCID"
                    CCID = Bsp.Utility.IsStringNull(ht(s))
                Case "AppID"
                    AppID = Bsp.Utility.IsStringNull(ht(s))
                Case "CustomerID"
                    CustomerID = Bsp.Utility.IsStringNull(ht(s))
            End Select
        Next

        Select Case ButtonName
            Case "徵信覆核"
                Update_CA_Profile_Status(AppID, "3", "And CustomerID=" & Bsp.Utility.Quote(CustomerID), tran)
                Update_CC_Profile_Status(CCID, "3", tran)
                Update_SP_Profile_Status(AppID, CustomerID, "3", tran)
                '送出後重新計算客戶評級
                Bsp.DB.ExecuteNonQuery(CommandType.StoredProcedure, "SP_GetScore", New DbParameter() { _
                                           Bsp.DB.getDbParameter("@argAppID", AppID), _
                                           Bsp.DB.getDbParameter("@argCustomerID", CustomerID), _
                                           Bsp.DB.getDbParameter("@argLastChgID", UserProfile.ActUserID)})
            Case "徵信撤件"
                'TODO : 徵信撤案,尚有財報須撤銷
                Update_BR_Customer_Status(AppID, "1", "And CustomerID=" & Bsp.Utility.Quote(CustomerID), tran)
                Update_CA_Profile_Status(AppID, "0", "And CustomerID=" & Bsp.Utility.Quote(CustomerID), tran)
                Update_CC_Profile_Status(CCID, "0", tran)
                Update_SP_Profile_Status(AppID, CustomerID, "0", tran)
        End Select
    End Sub

    ''' <summary>
    ''' 將案件的基本資料複製到基本件上
    ''' </summary>
    ''' <param name="AppID"></param>
    ''' <param name="CustomerID"></param>
    ''' <param name="tran"></param>
    ''' <remarks></remarks>
    Private Sub CopyBRTables(ByVal AppID As String, ByVal CustomerID As String, ByVal tran As DbTransaction)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select a.OfficerID, b.CID [SourceCID],")
        strSQL.AppendLine("	(Select CID From BR_Customer Where CustomerID = b.CustomerID And AppID = '' And AO_CODE = b.AO_CODE) [TargetCID]")
        strSQL.AppendLine("From AP_Profile a inner join BR_Customer b on a.AppID = b.AppID")
        strSQL.AppendLine("Where a.AppID = " & Bsp.Utility.Quote(AppID))
        strSQL.AppendLine("And b.CustomerID = " & Bsp.Utility.Quote(CustomerID))

        Using dtAP_Profile As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), tran).Tables(0)
            If Bsp.Utility.IsStringNull(dtAP_Profile.Rows(0).Item("TargetCID")) <> "" Then
                Bsp.DB.ExecuteNonQuery(CommandType.StoredProcedure, "SP_CopyBRTables", New DbParameter() { _
                                                   Bsp.DB.getDbParameter("@SourceCID", dtAP_Profile.Rows(0).Item("SourceCID").ToString()), _
                                                   Bsp.DB.getDbParameter("@TargetCID", dtAP_Profile.Rows(0).Item("TargetCID").ToString()), _
                                                   Bsp.DB.getDbParameter("@UpdateUserID", UserProfile.ActUserID), _
                                                   Bsp.DB.getDbParameter("@CopyTables", "")}, tran)
            End If

        End Using
    End Sub

    Private Sub AfterActCC05(ByVal FlowID As String, ByVal FlowVer As String, ByVal FlowStepID As String, ByVal ButtonName As String, ByVal FlowCaseID As String, ByVal tran As DbTransaction, ByVal ParamArray Params As Object())
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim CCID As String = ""
        Dim AppID As String = ""
        Dim CustomerID As String = ""

        For Each s As String In ht.Keys
            Select Case s
                Case "CCID"
                    CCID = Bsp.Utility.IsStringNull(ht(s))
                Case "AppID"
                    AppID = Bsp.Utility.IsStringNull(ht(s))
                Case "CustomerID"
                    CustomerID = Bsp.Utility.IsStringNull(ht(s))
            End Select
        Next

        Select Case ButtonName
            Case "轉呈"
            Case "退回徵信"
                Update_CA_Profile_Status(AppID, "1", "And CustomerID=" & Bsp.Utility.Quote(CustomerID), tran)
                Update_CC_Profile_Status(CCID, "1", tran)
                Update_SP_Profile_Status(AppID, CustomerID, "1", tran)
            Case "徵信結案"
                Update_CA_Profile_Status(AppID, "9", "And CustomerID=" & Bsp.Utility.Quote(CustomerID), tran)
                Update_CC_Profile_Status(CCID, "9", tran)
                Update_SP_Profile_Status(AppID, CustomerID, "9", tran)

                Using dt As DataTable = GetBR_CustomerE(AppID, CustomerID, "CustType", "", tran)
                    If Bsp.Utility.InStr(Bsp.Utility.IsStringNull(dt.Rows(0).Item("CustType")).Trim(), "A", "E") Then
                        '回寫客戶基本資料到代表件
                        CopyBRTables(AppID, CustomerID, tran)

                        '複製財報資料致客戶基本資料
                        Bsp.DB.ExecuteNonQuery(CommandType.StoredProcedure, "SP_Write2BRFin", New DbParameter() { _
                                               Bsp.DB.getDbParameter("@argCCID", CCID)}, tran)

                        '送出後重新計算客戶評級
                        Bsp.DB.ExecuteNonQuery(CommandType.StoredProcedure, "SP_GetScore", New DbParameter() { _
                                                   Bsp.DB.getDbParameter("@argAppID", AppID), _
                                                   Bsp.DB.getDbParameter("@argCustomerID", CustomerID), _
                                                   Bsp.DB.getDbParameter("@argLastChgID", UserProfile.ActUserID)}, tran)
                    End If
                End Using

            Case "徵信銷案"
                'TODO : 徵信撤案,尚有財報須撤銷
                Update_BR_Customer_Status(AppID, "1", "And CustomerID=" & Bsp.Utility.Quote(CustomerID), tran)
                Update_CA_Profile_Status(AppID, "0", "And CustomerID=" & Bsp.Utility.Quote(CustomerID), tran)
                Update_CC_Profile_Status(CCID, "0", tran)
                Update_SP_Profile_Status(AppID, CustomerID, "0", tran)
        End Select
    End Sub

    ''' <summary>
    ''' 異動CC_Profile的狀態,僅針對徵信案件,參照不適用
    ''' </summary>
    ''' <param name="CCID"></param>
    ''' <param name="Status"></param>
    ''' <param name="Tran"></param>
    ''' <remarks></remarks>
    Private Sub Update_CC_Profile_Status(ByVal CCID As String, ByVal Status As String, ByVal Tran As DbTransaction)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Update CC_Profile Set Status = " & Bsp.Utility.Quote(Status))
        strSQL.AppendLine(" , LastChgDate = Getdate()")
        strSQL.AppendLine(" , LastChgID = " & Bsp.Utility.Quote(UserProfile.ActUserID))
        strSQL.AppendLine("Where CCID = " & Bsp.Utility.Quote(CCID))
        strSQL.AppendLine("And RefFlag = '0'")

        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), Tran)
    End Sub

    ''' <summary>
    ''' 異動SP_Profile的狀態,僅針對徵信案件,參照不適用
    ''' </summary>
    ''' <param name="AppID"></param>
    ''' <param name="CustomerID"></param>
    ''' <param name="Status"></param>
    ''' <param name="Tran"></param>
    ''' <remarks></remarks>
    Private Sub Update_SP_Profile_Status(ByVal AppID As String, ByVal CustomerID As String, ByVal Status As String, ByVal Tran As DbTransaction)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Update SP_Profile Set Status = " & Bsp.Utility.Quote(Status))
        strSQL.AppendLine(" , LastChgDate = Getdate()")
        strSQL.AppendLine(" , LastChgID = " & Bsp.Utility.Quote(UserProfile.ActUserID))
        strSQL.AppendLine("Where AppID = " & Bsp.Utility.Quote(AppID))
        strSQL.AppendLine("And CustomerID = " & Bsp.Utility.Quote(CustomerID))

        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), Tran)
    End Sub

    Private Sub Update_BR_Customer_Status(ByVal AppID As String, ByVal Status As String, ByVal CondStr As String, ByVal Tran As DbTransaction)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Update BR_Customer Set AppStatus = " & Bsp.Utility.Quote(Status))
        strSQL.AppendLine(" , UpdateUser = " & Bsp.Utility.Quote(UserProfile.ActUserID))
        strSQL.AppendLine(" , UpdateDate = Getdate()")
        strSQL.AppendLine("Where AppID = " & Bsp.Utility.Quote(AppID))
        If CondStr <> "" Then strSQL.AppendLine(CondStr)

        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), Tran)
    End Sub

    Private Sub Update_AP_Profile_Status(ByVal AppID As String, ByVal Status As String, ByVal CondStr As String, ByVal Tran As DbTransaction)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Update AP_Profile Set AppStatus = " & Bsp.Utility.Quote(Status))
        strSQL.AppendLine(" , LastChgID = " & Bsp.Utility.Quote(UserProfile.ActUserID))
        strSQL.AppendLine(" , LastChgDate = getdate()")
        strSQL.AppendLine("Where AppID = " & Bsp.Utility.Quote(AppID))
        If CondStr <> "" Then strSQL.AppendLine(CondStr)

        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), Tran)
    End Sub

    Private Sub Update_AL_Profile_Status(ByVal AppID As String, ByVal Status As String, ByVal Tran As DbTransaction)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Update AL_Profile Set Status = " & Bsp.Utility.Quote(Status))
        strSQL.AppendLine(" , LastChgID = " & Bsp.Utility.Quote(UserProfile.ActUserID))
        strSQL.AppendLine(" , LastChgDate = getdate()")
        strSQL.AppendLine("Where AppID = " & Bsp.Utility.Quote(AppID))

        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), Tran)
    End Sub

    Private Sub Update_CA_Profile_Status(ByVal AppID As String, ByVal Status As String, ByVal CondStr As String, ByVal Tran As DbTransaction)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Update CA_Profile Set Status = " & Bsp.Utility.Quote(Status))
        strSQL.AppendLine(" , LastChgID = " & Bsp.Utility.Quote(UserProfile.ActUserID))
        strSQL.AppendLine(" , LastChgDate = getdate()")
        strSQL.AppendLine("Where AppID = " & Bsp.Utility.Quote(AppID))
        If CondStr <> "" Then strSQL.AppendLine(CondStr)

        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), Tran)
    End Sub

    Private Sub Update_AL_Profile_ExRateDate(ByVal AppID As String, ByVal Tran As DbTransaction)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Update AL_Profile Set")
        strSQL.AppendLine(" ExRateDate = Convert(varchar(8),dateadd(dd,-1,convert(datetime,left(convert(varchar(8),getdate(),112),6) + '01')),112)")
        strSQL.AppendLine(" , LastChgDate = Getdate()")
        strSQL.AppendLine(" , LastChgID = " & Bsp.Utility.Quote(UserProfile.ActUserID))
        strSQL.AppendLine("Where AppID = " & Bsp.Utility.Quote(AppID))

        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), Tran)
    End Sub
#End Region

#Region "DD : 查核報告"
    Private Sub AfterActDD01(ByVal FlowID As String, ByVal FlowVer As String, ByVal FlowStepID As String, ByVal ButtonName As String, ByVal FlowCaseID As String, ByVal tran As DbTransaction, ByVal ParamArray Params As Object())
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim DDID As String = ""
        Dim strAssignTo As String = ""

        For Each s As String In ht.Keys
            Select Case s
                Case "DDID"
                    DDID = Bsp.Utility.IsStringNull(ht(s))
                Case "AssignTo"
                    strAssignTo = Bsp.Utility.IsStringNull(ht(s))
            End Select
        Next

        Select Case ButtonName
            Case "改派查核"
                Update_DD_Checker(DDID, strAssignTo, tran)
            Case "查核覆核"
                Update_DD_Profile_Status(DDID, "3", tran)
            Case "銷案"
                Update_DD_Profile_Status(DDID, "0", tran)
        End Select
    End Sub

    Private Sub AfterActDD02(ByVal FlowID As String, ByVal FlowVer As String, ByVal FlowStepID As String, ByVal ButtonName As String, ByVal FlowCaseID As String, ByVal tran As DbTransaction, ByVal ParamArray Params As Object())
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim DDID As String = ""

        For Each s As String In ht.Keys
            Select Case s
                Case "DDID"
                    DDID = Bsp.Utility.IsStringNull(ht(s))
            End Select
        Next

        Select Case ButtonName
            Case "退回維護"
                Update_DD_Profile_Status(DDID, "1", tran)
            Case "結案"
                Update_DD_Profile_Status(DDID, "9", tran)
            Case "銷案"
                Update_DD_Profile_Status(DDID, "0", tran)
        End Select
    End Sub

    Private Sub Update_DD_Profile_Status(ByVal DDID As String, ByVal Status As String, ByVal Tran As DbTransaction)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Update DD_Profile Set Status = " & Bsp.Utility.Quote(Status))
        strSQL.AppendLine("Where DDID = " & Bsp.Utility.Quote(DDID))

        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), Tran)
    End Sub

    Private Sub Update_DD_Checker(ByVal DDID As String, ByVal AssignTo As String, ByVal Tran As DbTransaction)
        Dim strSQL As New StringBuilder()

        If DDID <> "" AndAlso AssignTo <> "" Then
            strSQL.AppendLine("Update DD_Profile set")
            strSQL.AppendLine("    CheckerID = " & Bsp.Utility.Quote(AssignTo))
            strSQL.AppendLine("  , CheckerDeptID = dbo.funShowEmployee(" & Bsp.Utility.Quote(AssignTo) & ", '', '4')")
            strSQL.AppendLine("Where DDID = " & Bsp.Utility.Quote(DDID))

            Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), Tran)
        End If
    End Sub
#End Region

#End Region

#Region "WFA030 : 案件流程明細"
    Public Function GetFlowMaster(ByVal FlowID As String, ByVal FlowCaseID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select dbo.funFindSCCommonItem('04', FlowID) as FlowName, PaperID AS KeyID, ")
        strSQL.AppendLine(" dbo.funSplitString(FlowShowValue, '|', 2) AS CustomerName, ")
        strSQL.AppendLine(" Isnull(dbo.funSplitString(FlowShowValue, '|', 3), '') + Isnull('|' + dbo.funSplitString(FlowShowValue, '|', 4), '') AS FlowNote,")
        strSQL.AppendLine(" FlowID, FlowCaseID, FlowVer, FlowKeyValue, FlowCaseStatus ")
        strSQL.AppendLine("From WF_FlowCase with (nolock)")
        strSQL.AppendLine("Where FlowID + '-' + FlowCaseID in ( ")
        strSQL.AppendLine("	Select FlowID + '-' + FlowCaseID From WF_FlowRelation with (nolock) ")
        strSQL.AppendLine("	Where ParentFlowID + '-' + ParentFlowCaseID in ( ")
        strSQL.AppendLine("		Select @FlowID + '-' + @FlowCaseID")
        strSQL.AppendLine("		Union")
        strSQL.AppendLine("		Select ParentFlowID + '-' + ParentFlowCaseID ")
        strSQL.AppendLine("		From WF_FlowRelation with (nolock) ")
        strSQL.AppendLine("		Where FlowID = @FlowID And FlowCaseID = @FlowCaseID)")
        strSQL.AppendLine("	Union ")
        strSQL.AppendLine("	Select ParentFlowID + '-' + ParentFlowCaseID From WF_FlowRelation with (nolock) ")
        strSQL.AppendLine("	Where ParentFlowID + '-' + ParentFlowCaseID in ( ")
        strSQL.AppendLine("		Select @FlowID + '-' + @FlowCaseID")
        strSQL.AppendLine("		Union")
        strSQL.AppendLine("		Select ParentFlowID + '-' + ParentFlowCaseID ")
        strSQL.AppendLine("		From WF_FlowRelation with (nolock) ")
        strSQL.AppendLine("		Where FlowID = @FlowID And FlowCaseID = @FlowCaseID) ")
        strSQL.AppendLine("	Union ")
        strSQL.AppendLine("	Select @FlowID + '-' + @FlowCaseID) ")
        strSQL.AppendLine("Order by CrDate")


        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), New DbParameter() { _
                    Bsp.DB.getDbParameter("@FlowID", FlowID), Bsp.DB.getDbParameter("@FlowCaseID", FlowCaseID)}).Tables(0)
    End Function

    ''' <summary>
    ''' 流程明細資料, 供流程明細查詢功能使用
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowCaseID"></param>
    ''' <param name="ShowMode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFlowDetail(ByVal FlowID As String, ByVal FlowCaseID As String, Optional ByVal ShowMode As String = "1") As DataTable
        Dim strSQL As New StringBuilder()

        'strSQL.AppendLine("Select FlowStepID + ' - ' + FlowStepDesc + '(' +  CASE FlowLogStatus WHEN 'Open' THEN '在途' WHEN 'Close' THEN CASE Isnull(FlowStepAction, '')  WHEN '' THEN '被完成' ELSE '完成' END END  + ')' AS FlowStepDesc, ")
        'strSQL.AppendLine("	FlowStepAction, FlowStepOpinion, AssignTo + '-' + AssignToName + '/' + ToUser + '-' + ToUserName AS ToUserName, ")
        'strSQL.AppendLine("    CASE IsProxy WHEN 'Y' THEN '(代理:' + AssignTo + '-' + AssignToName + ')' ELSE '' END AS ProxyStr, ")
        'strSQL.AppendLine("	CASE CONVERT(varchar(8), UpdDate, 112) WHEN '19000101' THEN NULL ELSE UpdDate END AS UpdDate,")
        'strSQL.AppendLine("	isnull(LogRemark,'') as LogRemark,")
        'strSQL.AppendLine("	CASE WHEN FlowStepID in ('') Then '" & ShowMode & "' ELSE '1' END AS ShowMode, 'N' as AgreeFlag ")
        'strSQL.AppendLine("From WF_FlowFullLog with (nolock) ")
        'strSQL.AppendLine("Where FlowID = " & Bsp.Utility.Quote(FlowID))
        'strSQL.AppendLine("And FlowCaseID = " & Bsp.Utility.Quote(FlowCaseID))
        'strSQL.AppendLine("Order by FlowLogID")

        strSQL.AppendLine("Select x.FlowStepID + ' - ' + x.FlowStepDesc + '(' +  CASE x.FlowLogStatus WHEN 'Open' THEN '在途' WHEN 'Close' THEN CASE Isnull(x.FlowStepAction, '')  WHEN '' THEN '被完成' ELSE '完成' END END  + ')' AS FlowStepDesc")
        strSQL.AppendLine("	, x.FlowStepAction, x.FlowStepOpinion, x.AssignTo + '-' + x.AssignToName + '/' + x.ToUser + '-' + x.ToUserName AS ToUserName")
        strSQL.AppendLine("	, CASE x.IsProxy WHEN 'Y' THEN '(代理:' + x.AssignTo + '-' + x.AssignToName + ')' ELSE '' END AS ProxyStr")
        strSQL.AppendLine("	, CASE CONVERT(varchar(8), x.UpdDate, 112) WHEN '19000101' THEN NULL ELSE x.UpdDate END AS UpdDate")
        strSQL.AppendLine("	, isnull(x.LogRemark,'') as LogRemark, CASE WHEN x.FlowStepID in ('S003','S004','S010','S012') Then '" & ShowMode & "' ELSE '1' END AS ShowMode")
        strSQL.AppendLine("	, ISNULL(y.AgreeFlag, 'N') as AgreeFlag ")
        strSQL.AppendLine("From WF_FlowFullLog x with (nolock) Left outer join WF_LoanChecker y on x.FlowID = y.FlowID and x.FlowCaseID = y.FlowCaseID And x.FlowLogID = y.FlowLogID ")
        strSQL.AppendLine("Where x.FlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("And x.FlowCaseID = " & Bsp.Utility.Quote(FlowCaseID))
        strSQL.AppendLine("Order by x.FlowLogID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    Public Function CheckWFListReadAuth(ByVal FlowCaseID As String, ByVal UserID As String) As Boolean
        Dim strSQL As New StringBuilder
        strSQL.AppendLine("SELECT  count(*)  FROM ( ")
        strSQL.AppendLine(" SELECT AssignTo AS UserID ")
        strSQL.AppendLine(" FROM   WF_FlowFullLog with (nolock) ")
        strSQL.AppendLine(" WHERE (FlowID = 'AP') AND (FlowCaseID = " & Bsp.Utility.Quote(FlowCaseID) & ") AND (FlowStepID IN ('S004')) ")
        strSQL.AppendLine(" UNION ")
        strSQL.AppendLine(" SELECT ToUser AS UserID ") '因應代理人代職不代權，故，代理人S004之核貸委員也行看
        strSQL.AppendLine(" FROM   WF_FlowFullLog with (nolock) ")
        strSQL.AppendLine(" WHERE (FlowID = 'AP') AND (FlowCaseID = " & Bsp.Utility.Quote(FlowCaseID) & ") AND (FlowStepID IN ('S004')) ")
        strSQL.AppendLine(" UNION ")
        'strSQL.AppendLine(" SELECT AssignTo AS UserID ")
        'strSQL.AppendLine(" FROM   WF_FlowFullLog with (nolock) ")
        'strSQL.AppendLine(" WHERE (FlowID = 'ELoan') AND (FlowCaseID = '" & FlowCaseID & "') AND (FlowStepID IN ('EL06', 'EL11', 'EL13')) ")
        'strSQL.AppendLine(" UNION ")

        strSQL.AppendLine(" SELECT  UserID ")
        strSQL.AppendLine(" FROM   SC_UserGroup with (nolock) ")
        strSQL.AppendLine(" WHERE  (GroupID in ('C5','21','22','Q9','Q9_1','25')) ")
        strSQL.AppendLine(") AS k ")
        strSQL.AppendLine("where UserID = " & Bsp.Utility.Quote(UserID) & " ")

        Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
            If dt.Rows(0).Item(0) > 0 Then
                Return True
            Else
                Return False
            End If
        End Using
    End Function
#End Region

#Region "WFA040 : 片語維護"
    ''' <summary>
    ''' 取得流程片語資料
    ''' </summary>
    ''' <param name="UserID"></param>
    ''' <param name="FlowID">選擇性參數</param>
    ''' <param name="FlowStepID">選擇性參數</param>
    ''' <returns></returns>
    ''' <remarks>供WFA040片語維護使用</remarks>
    Public Function GetFlowPhrasebyUser(ByVal UserID As String, Optional ByVal FlowID As String = "", Optional ByVal FlowStepID As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select a.FlowID, a.FlowID + '-' + dbo.funFindSCCommonItem('04',a.FlowID) AS FlowName,")
        strSQL.AppendLine(" a.FlowStepID, b.Description, a.UserID, a.SeqNo, a.FlowPhrase, ")
        strSQL.AppendLine(" dbo.funGetAOrgDefine('1', a.LastChgID) as LastChgID , a.LastChgDate, b.Description ")
        strSQL.AppendLine("From WF_Phrase AS a INNER JOIN ")
        strSQL.AppendLine("     WF_FlowStepM AS b ON a.FlowID = b.FlowID AND a.FlowStepID = b.FlowStepID ")
        strSQL.AppendLine("where a.UserID = " & Bsp.Utility.Quote(UserID))
        If FlowID.Trim <> "" Then
            strSQL.AppendLine("and a.FlowID = " & Bsp.Utility.Quote(FlowID))
        End If
        If FlowStepID.Trim <> "" Then
            strSQL.AppendLine("and a.FlowStepID = " & Bsp.Utility.Quote(FlowStepID))
        End If
        strSQL.AppendLine("Order by a.FlowID, a.FlowStepID, a.UserID, a.SeqNo")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    ''' <summary>
    ''' 取得單一筆的片語, 供維護使用
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowStepID"></param>
    ''' <param name="UserID"></param>
    ''' <param name="SeqNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFlowPhrase(ByVal FlowID As String, ByVal FlowStepID As String, ByVal UserID As String, ByVal SeqNo As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select * From WF_Phrase")
        strSQL.AppendLine("Where FlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("And FlowStepID = " & Bsp.Utility.Quote(FlowStepID))
        strSQL.AppendLine("And UserID = " & Bsp.Utility.Quote(UserID))
        strSQL.AppendLine("And SeqNo = " & SeqNo)

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    ''' <summary>
    ''' 刪除片語資料
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowStepID"></param>
    ''' <param name="SeqNo"></param>
    ''' <param name="UserID"></param>
    ''' <remarks></remarks>
    Public Sub Delete_FlowPhrase(ByVal FlowID As String, ByVal FlowStepID As String, ByVal SeqNo As String, ByVal UserID As String)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Delete From WF_Phrase ")
        strSQL.AppendLine("Where FlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("And FlowStepID = " & Bsp.Utility.Quote(FlowStepID))
        strSQL.AppendLine("And UserID = " & Bsp.Utility.Quote(UserID))
        strSQL.AppendLine("And SeqNo = " & Bsp.Utility.Quote(SeqNo))

        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString())
    End Sub

    Public Sub Insert_FlowPhrase(ByVal FlowID As String, ByVal FlowStepID As String, ByVal UserID As String, ByVal FlowPhrase As String)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Insert into WF_Phrase")
        strSQL.AppendLine("(FlowID, FlowStepID, UserID, SeqNo, FlowPhrase, LastChgID, LastChgDate)")
        strSQL.AppendLine("values")
        strSQL.AppendLine("(@FlowID, @FlowStepID, @UserID, @SeqNo, @FlowPhrase, @LastChgID, Getdate())")

        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), _
                New DbParameter() { _
                Bsp.DB.getDbParameter("@FlowID", FlowID), _
                Bsp.DB.getDbParameter("@FlowStepID", FlowStepID), _
                Bsp.DB.getDbParameter("@UserID", UserID), _
                Bsp.DB.getDbParameter("@SeqNo", GetPhraseMaxSeqNo(FlowID, FlowStepID, UserID)), _
                Bsp.DB.getDbParameter("@FlowPhrase", FlowPhrase), _
                Bsp.DB.getDbParameter("@LastChgID", UserProfile.ActUserID)})
    End Sub

    Public Sub Update_FlowPhrase(ByVal FlowID As String, ByVal FlowStepID As String, ByVal UserID As String, ByVal SeqNo As String, ByVal FlowPhrase As String)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Update WF_Phrase Set ")
        strSQL.AppendLine(" FlowPhrase   = N" & Bsp.Utility.Quote(FlowPhrase) & ",")
        strSQL.AppendLine(" LastChgID    = " & Bsp.Utility.Quote(UserProfile.ActUserID) & ",")
        strSQL.AppendLine(" LastChgDate  = getdate() ")
        strSQL.AppendLine("Where FlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("And FlowStepID = " & Bsp.Utility.Quote(FlowStepID))
        strSQL.AppendLine("And UserID = " & Bsp.Utility.Quote(UserID))
        strSQL.AppendLine("And SeqNo = " & Bsp.Utility.Quote(SeqNo))

        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString())
    End Sub

    Private Function GetPhraseMaxSeqNo(ByVal FlowID As String, ByVal FlowStepID As String, ByVal UserID As String) As Integer
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select Isnull(Max(SeqNo), 0) + 1")
        strSQL.AppendLine("From WF_Phrase")
        strSQL.AppendLine("Where FlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("And FlowStepID = " & Bsp.Utility.Quote(FlowStepID))
        strSQL.AppendLine("And UserID = " & Bsp.Utility.Quote(UserID))

        Return Convert.ToInt32(Bsp.DB.ExecuteScalar(strSQL.ToString()))
    End Function
#End Region

#Region "WFA050 : 案件改派作業"
    ''' <summary>
    ''' 取得使用者的To do list清單
    ''' </summary>
    ''' <param name="ToDoUser"></param>
    ''' <returns></returns>
    ''' <remarks>供WFA050改派作業使用</remarks>
    Public Function GetUserToDoList(ByVal ToDoUser As String) As String
        Dim strSQL As New StringBuilder

        strSQL.AppendLine("Select FlowID + ';' + FlowCaseID + ';' + CONVERT(varchar(2), FlowLogBatNo) + ';' + FlowLogID AS FlowKeyStr, ")
        strSQL.AppendLine(" ROW_NUMBER() OVER (ORDER BY FromDate) AS seq, FlowDispatchFlag, FlowStepID + FlowStepDesc AS FlowStepDesc,")
        strSQL.AppendLine(" PaperID AS KeyID, dbo.funSplitString(FlowShowValue, '|', 2) AS CustomerName,")
        strSQL.AppendLine(" Left(Isnull(dbo.funSplitString(FlowShowValue, '|', 3), '') + Isnull('|' + dbo.funSplitString(FlowShowValue, '|', 4), ''), 20) AS Note,")
        strSQL.AppendLine(" FromBrName, FromUserName, FromDate, FlowID, FlowCaseID, FlowLogID")
        strSQL.AppendLine("From WF_FlowToDoList")
        strSQL.AppendLine("Where AssignTo = " & Bsp.Utility.Quote(ToDoUser))

        Return strSQL.ToString()
    End Function

    Public Function FlowReAssign(ByVal FlowID As String, ByVal FlowVer As String, ByVal FlowCaseID As String, ByVal FlowLogBatNo As String, ByVal FlowLogID As String, ByVal ReAssignTo As String, ByVal Remark As String, ByVal tran As DbTransaction) As String
        Dim DoChangeUser As String
        Dim RtnReAgnUser As String = ""
        Try
            '查詢該筆
            Using dt As DataTable = GetFlowFullLog(FlowID, FlowCaseID, FlowLogID, "And FlowLogStatus = 'Open'", tran)
                If dt.Rows.Count > 0 Then
                    '新增log
                    ReAssignTo = ToReAssignDisUser(FlowID, FlowCaseID, FlowLogBatNo, ReAssignTo, dt.Rows(0).Item("AssignTo").ToString, ";", tran)
                    RtnReAgnUser = ReAssignTo
                    If ReAssignTo <> "" AndAlso ReAssignTo <> dt.Rows(0).Item("AssignTo").ToString Then
                        Dim LastFlowLogID As String = Insert_ReAssignFlowFullLog(FlowID, FlowVer, FlowCaseID, FlowLogBatNo, dt.Rows(0).Item("FlowStepID").ToString, "", UserProfile.ActUserID, ReAssignTo, "Open", "", tran)
                        '更新原log
                        If UserProfile.ActUserID <> UserProfile.UserID Then
                            DoChangeUser = UserProfile.ActUserID & "-" & UserProfile.ActUserName & "(代" & UserProfile.UserID & "-" & UserProfile.UserName & ")"
                        Else
                            DoChangeUser = UserProfile.ActUserID & "-" & UserProfile.ActUserName
                        End If
                        Remark = DoChangeUser & " 改派原處理人員[" & dt.Rows(0).Item("AssignTo").ToString & "-" & dt.Rows(0).Item("AssignToName").ToString & "]為" & ReAssignTo & "。<br>" & Remark
                        Update_ReAssignFlowFullLog(FlowID, FlowVer, FlowCaseID, FlowLogID, dt.Rows(0).Item("FlowStepID").ToString, "改派", Remark, UserProfile.ActUserID, "Close", "", tran)
                        '執行更新ToDolist
                        PutFlowToDoList(FlowID, FlowCaseID, tran)
                        '更新FlowCase之LastFlowLogID,UpdDate
                        Update_ReAssignFlowCase(FlowID, FlowCaseID, LastFlowLogID, tran)
                    Else
                        Return ""
                    End If
                Else
                    Throw New Exception("查無該筆流程 [" & FlowID & "-" & FlowCaseID & "-" & FlowLogID & "-" & "] 相關資料，無改派動作!")
                End If
            End Using

        Catch ex As Exception
            Throw
        End Try
        Return RtnReAgnUser
    End Function

    ''' <summary>
    ''' ToReAssignDisUser 將傳入之User清單，不重覆傳出且不為目前之關卡之待辦人員(CanAssignUser為可再重覆傳出人員）
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowCaseID"></param>
    ''' <param name="FlowLogBatNo"></param>
    ''' <param name="ToUser"></param>
    ''' <param name="OutUser"></param>
    ''' <param name="SplitKey"></param>
    ''' <param name="tran"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ToReAssignDisUser(ByVal FlowID As String, ByVal FlowCaseID As String, ByVal FlowLogBatNo As String, ByVal ToUser As String, ByVal OutUser As String, ByVal SplitKey As String, ByVal tran As DbTransaction) As String
        ToUser = ToUser.Replace(SplitKey, "','").ToString
        Dim strSQL As New StringBuilder()
        Dim UserStr As String = ""

        strSQL.AppendLine("Select distinct UserID From SC_User")
        strSQL.AppendLine("Where UserID in ('" & ToUser & "')")
        strSQL.AppendLine("And UserID not in (")
        strSQL.AppendLine("     Select AssignTo From WF_FlowToDoList")
        strSQL.AppendLine("     Where FlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("     And FlowCaseID = " & Bsp.Utility.Quote(FlowCaseID))
        strSQL.AppendLine("     And FlowLogBatNo = " & Bsp.Utility.Quote(FlowLogBatNo))
        strSQL.AppendLine("     And FlowCaseStatus = 'Open'")
        strSQL.AppendLine("     And AssignTo <> " & Bsp.Utility.Quote(OutUser) & ")")
        strSQL.AppendLine("Order by UserID")

        Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), tran).Tables(0)
            If dt.Rows.Count > 0 Then
                For intLoop As Integer = 0 To dt.Rows.Count - 1
                    UserStr &= dt.Rows(intLoop).Item("UserID").ToString() & ";"
                Next
                UserStr = Left(UserStr, UserStr.Length - 1)
            End If
        End Using

        Return UserStr
    End Function

    Public Function Insert_ReAssignFlowFullLog(ByVal FlowID As String, ByVal FlowVer As String, ByVal FlowCaseID As String, _
            ByVal FlowLogBatNo As String, ByVal FlowStepID As String, ByVal FlowStepOpinion As String, ByVal FromUser As String, _
            ByVal AssignTo As String, ByVal FlowCaseStatus As String, ByVal LogRemark As String, ByVal tran As DbTransaction) As String

        Dim LastFlowLogID As String = ""
        Dim AssignToArr() As String = AssignTo.Split(";")
        For intLoop As Integer = 0 To AssignToArr.GetUpperBound(0)
            If AssignToArr(intLoop).ToString().Trim() <> "" Then
                Dim beFlowFullLog As New beWF_FlowFullLog.Row()
                Dim bsFlowFullLog As New beWF_FlowFullLog.Service()

                With beFlowFullLog
                    .FlowID.Value = FlowID
                    .FlowCaseID.Value = FlowCaseID
                    .FlowLogBatNo.Value = FlowLogBatNo
                    .FlowLogID.Value = GetFlowLogID(FlowID, FlowCaseID, tran)
                    LastFlowLogID = .FlowLogID.Value
                    .FlowStepID.Value = FlowStepID
                    Try
                        .FlowStepDesc.Value = GetFlowStepMCol(FlowID, FlowVer, FlowStepID, "Description")
                    Catch ex As Exception
                        Throw
                    End Try
                    .FlowStepAction.Value = ""
                    .FlowStepOpinion.Value = FlowStepOpinion
                    .FlowLogStatus.Value = FlowCaseStatus
                    .FromUser.Value = FromUser
                    .AssignTo.Value = AssignToArr(intLoop)
                    Using dt As DataTable = GetUserInfo(FromUser)
                        .FromBr.Value = dt.Rows(0).Item("DeptID").ToString
                        .FromBrName.Value = dt.Rows(0).Item("DeptName").ToString
                        .FromUserName.Value = dt.Rows(0).Item("UserName").ToString()
                    End Using
                    Using dt As DataTable = GetUserInfo(AssignToArr(intLoop))
                        .AssignToName.Value = dt.Rows(0).Item("UserName").ToString()
                    End Using
                    .ToBr.Value = ""
                    .ToBrName.Value = ""
                    .ToUser.Value = ""
                    .ToUserName.Value = ""
                    .IsProxy.Value = ""
                    .CrDate.Value = Now
                    .UpdDate.Value = "1900/1/1"
                    .LogRemark.Value = LogRemark
                End With

                bsFlowFullLog.Insert(beFlowFullLog, tran)
            End If
        Next
        Return LastFlowLogID
    End Function

    Private Sub Update_ReAssignFlowFullLog(ByVal FlowID As String, ByVal FlowVer As String, ByVal FlowCaseID As String, _
            ByVal FlowCurrLogID As String, ByVal FlowStepID As String, _
            ByVal FlowStepAction As String, ByVal FlowStepOpinion As String, ByVal ActUser As String, _
            ByVal FlowLogStatus As String, ByVal LogRemark As String, ByVal tran As DbTransaction)
        Dim strSQL As New StringBuilder()

        Using dt As DataTable = GetUserInfo(ActUser)
            strSQL.AppendLine("Update WF_FlowFullLog Set")
            strSQL.AppendLine(" FlowStepAction = " & Bsp.Utility.Quote(FlowStepAction) & ",")
            strSQL.AppendLine(" FlowStepOpinion = N" & Bsp.Utility.Quote(FlowStepOpinion) & ",")
            strSQL.AppendLine(" ToBr = " & Bsp.Utility.Quote(dt.Rows(0).Item("DeptID").ToString) & ",")
            strSQL.AppendLine(" ToBrName = " & Bsp.Utility.Quote(dt.Rows(0).Item("DeptName").ToString) & ",")
            strSQL.AppendLine(" ToUser = " & Bsp.Utility.Quote(ActUser) & ",")
            strSQL.AppendLine(" ToUserName = " & Bsp.Utility.Quote(dt.Rows(0).Item("UserName").ToString) & ",")
            strSQL.AppendLine(" IsProxy = Case When AssignTo = " & Bsp.Utility.Quote(ActUser) & " Then 'N' Else 'Y' End,")
            strSQL.AppendLine(" FlowLogStatus = " & Bsp.Utility.Quote(FlowLogStatus) & ", ")
            strSQL.AppendLine(" UpdDate = getdate(),")
            strSQL.AppendLine(" LogRemark = N" & Bsp.Utility.Quote(LogRemark) & "   ")
            strSQL.AppendLine("Where FlowID = " & Bsp.Utility.Quote(FlowID))
            strSQL.AppendLine("And FlowCaseID = " & Bsp.Utility.Quote(FlowCaseID))
            strSQL.AppendLine("And FlowLogID = " & Bsp.Utility.Quote(FlowCurrLogID))
        End Using

        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)
    End Sub

    Public Sub Update_ReAssignFlowCase(ByVal FlowID As String, ByVal FlowCaseID As String, ByVal LastLogID As String, ByVal tran As DbTransaction)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Update WF_FlowCase Set ")
        strSQL.AppendLine(" LastLogID = " & Bsp.Utility.Quote(LastLogID) & ", ")
        strSQL.AppendLine(" UpdDate = Getdate() ")
        strSQL.AppendLine("Where FlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("And FlowCaseID = " & Bsp.Utility.Quote(FlowCaseID))

        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)
    End Sub
#End Region

#Region "WFA060 : 附件上傳"
    ''' <summary>
    ''' 取得附件資料的Script
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowCaseID"></param>
    ''' <param name="DocType"></param>
    ''' <returns></returns>
    ''' <remarks>供WFA060附件上傳功能使用</remarks>
    Public Function GetAttachmentQueryString(ByVal FlowID As String, ByVal FlowCaseID As String, Optional ByVal DocType As String = "", Optional ByVal CustomerID As String = "") As String
        Dim strSQL As New StringBuilder

        strSQL.AppendLine("Select *, dbo.funGetAOrgDefine('1', LastChgID) UploadUserNm, ")
        strSQL.AppendLine("     dbo.funFindSCCommonItem('103', DocType) DocTypeNm")
        strSQL.AppendLine(" , Case When FlowID = 'AP' And Isnull(CustomerID,'') <> '' Then (Select Top 1 CName From BR_Customer With (nolock) Where AppID = WF_Attachment.PaperID And CustomerID = WF_Attachment.CustomerID)")
        strSQL.AppendLine("        When FlowID = 'CC' And Isnull(CustomerID,'') <> '' Then (Select Top 1 CName From CC_Profile With (nolock) Where CCID = WF_Attachment.PaperID And CustomerID = WF_Attachment.CustomerID)")
        strSQL.AppendLine("        When FlowID = 'DD' And Isnull(CustomerID,'') <> '' Then (Select Top 1 CName From BR_Customer With (nolock) Where AppID = (Select Top 1 AppID From DD_Profile With (nolock) Where DDID = WF_Attachment.PaperID And CustomerID = WF_Attachment.CustomerID) And CustomerID = WF_Attachment.CustomerID)")
        strSQL.AppendLine("   Else '' End CustomerNm")
        strSQL.AppendLine(" From WF_Attachment With (nolock)")
        strSQL.AppendLine("Where FlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("And FlowCaseID = " & Bsp.Utility.Quote(FlowCaseID))
        If CustomerID <> "" Then
            strSQL.AppendLine("And CustomerID = " & Bsp.Utility.Quote(CustomerID))
        End If
        If DocType <> "" Then
            strSQL.AppendLine("And DocType = " & Bsp.Utility.Quote(DocType))
        End If
        strSQL.AppendLine("Order by SeqNo")

        Return strSQL.ToString()
    End Function

    ''' <summary>
    ''' 取得案件內所有上傳的附件資料
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>授信案使用</remarks>
    Public Function GetAllAttachment(ByVal AppID As String, Optional ByVal DocType As String = "") As String
        Dim strSQL As New StringBuilder

        strSQL.AppendLine("Select *, dbo.funGetAOrgDefine('1', LastChgID) UploadUserNm ")
        strSQL.AppendLine("     , dbo.funFindSCCommonItem('103', DocType) DocTypeNm")
        strSQL.AppendLine("     , Case When Isnull(CustomerID,'') <> '' Then (Select Top 1 CName From BR_Customer With (nolock) Where AppID = WF_Attachment.PaperID And CustomerID = WF_Attachment.CustomerID) else '' End as CustomerNm")
        strSQL.AppendLine(" From WF_Attachment With (nolock)")
        strSQL.AppendLine("Where PaperID = " & Bsp.Utility.Quote(AppID))
        strSQL.AppendLine("And FlowID = 'AP'")
        If DocType <> "" Then
            strSQL.AppendLine("And DocType = " & Bsp.Utility.Quote(DocType))
        End If
        strSQL.AppendLine("Union")
        strSQL.AppendLine("Select *, dbo.funGetAOrgDefine('1', LastChgID) UploadUserNm ")
        strSQL.AppendLine("     , dbo.funFindSCCommonItem('103', DocType) DocTypeNm")
        strSQL.AppendLine("     , Case When Isnull(CustomerID,'') <> '' Then (Select Top 1 CName From CC_Profile With (nolock) Where CCID = WF_Attachment.PaperID And CustomerID = WF_Attachment.CustomerID) else '' End CustomerNm")
        strSQL.AppendLine(" From WF_Attachment With (nolock)")
        strSQL.AppendLine("Where PaperID in (Select CCID From CC_Profile where AppID = " & Bsp.Utility.Quote(AppID) & " And Status <> '0')")
        strSQL.AppendLine("And FlowID = 'CC'")
        If DocType <> "" Then
            strSQL.AppendLine("And DocType = " & Bsp.Utility.Quote(DocType))
        End If
        strSQL.AppendLine("Union")
        strSQL.AppendLine("Select *, dbo.funGetAOrgDefine('1', LastChgID) UploadUserNm ")
        strSQL.AppendLine("     , dbo.funFindSCCommonItem('103', DocType) DocTypeNm")
        strSQL.AppendLine("     , Case When Isnull(CustomerID,'') <> '' Then (Select Top 1 CName From BR_Customer With (nolock) Where AppID = (Select Top 1 AppID From DD_Profile With (nolock) Where DDID = WF_Attachment.PaperID And CustomerID = WF_Attachment.CustomerID) And CustomerID = WF_Attachment.CustomerID) else '' End CustomerNm")
        strSQL.AppendLine(" From WF_Attachment With (nolock)")
        strSQL.AppendLine("Where PaperID in (Select DDID From DD_Profile Where AppID = " & Bsp.Utility.Quote(AppID) & " And Status <> '0')")
        strSQL.AppendLine("And FlowID = 'DD'")
        If DocType <> "" Then
            strSQL.AppendLine("And DocType = " & Bsp.Utility.Quote(DocType))
        End If
        strSQL.AppendLine("Order by FlowID, SeqNo")

        Return strSQL.ToString()
    End Function

    ''' <summary>
    ''' 取得個別公司上傳的附件
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>授信案使用</remarks>
    Public Function GetAttachmentbyID(ByVal AppID As String, ByVal CustomerID As String, Optional ByVal DocType As String = "") As String
        Dim strSQL As New StringBuilder
        Dim strCCID As String = ""

        strSQL.AppendLine("Select CCID From CC_Profile With (nolock) ")
        strSQL.AppendLine("Where RefFlag = '0' And CCID in (")
        strSQL.AppendLine(" Select CCID From CC_Profile With (nolock) ")
        strSQL.AppendLine(" Where AppID = " & Bsp.Utility.Quote(AppID))
        strSQL.AppendLine(" And CustomerID = " & Bsp.Utility.Quote(CustomerID))
        strSQL.AppendLine(" And Status <> '0')")

        strCCID = Bsp.Utility.IsStringNull(Bsp.DB.ExecuteScalar(strSQL.ToString()))

        strSQL.Remove(0, strSQL.Length)
        strSQL.AppendLine("Select *, dbo.funGetAOrgDefine('1', LastChgID) UploadUserNm, ")
        strSQL.AppendLine("     dbo.funFindSCCommonItem('103', DocType) DocTypeNm")
        strSQL.AppendLine("     , (Select Top 1 CName From BR_Customer With (nolock) Where AppID = WF_Attachment.PaperID And CustomerID = WF_Attachment.CustomerID) as CustomerNm")
        strSQL.AppendLine(" From WF_Attachment With (nolock)")
        strSQL.AppendLine("Where PaperID = " & Bsp.Utility.Quote(AppID))
        strSQL.AppendLine("And FlowID = 'AP'")
        strSQL.AppendLine("And CustomerID = " & Bsp.Utility.Quote(CustomerID))
        If DocType <> "" Then
            strSQL.AppendLine("And DocType = " & Bsp.Utility.Quote(DocType))
        End If
        strSQL.AppendLine("Union")
        strSQL.AppendLine("Select *, dbo.funGetAOrgDefine('1', LastChgID) UploadUserNm, ")
        strSQL.AppendLine("     dbo.funFindSCCommonItem('103', DocType) DocTypeNm")
        strSQL.AppendLine("     , (Select Top 1 CName From CC_Profile With (nolock) Where CCID = WF_Attachment.PaperID And CustomerID = WF_Attachment.CustomerID) CustomerNm")
        strSQL.AppendLine(" From WF_Attachment With (nolock)")
        strSQL.AppendLine("Where PaperID = " & Bsp.Utility.Quote(strCCID))
        strSQL.AppendLine("And FlowID = 'CC'")
        If DocType <> "" Then
            strSQL.AppendLine("And DocType = " & Bsp.Utility.Quote(DocType))
        End If
        strSQL.AppendLine("Union")
        strSQL.AppendLine("Select *, dbo.funGetAOrgDefine('1', LastChgID) UploadUserNm, ")
        strSQL.AppendLine("     dbo.funFindSCCommonItem('103', DocType) DocTypeNm")
        strSQL.AppendLine("     , (Select Top 1 CName From BR_Customer With (nolock) Where AppID = (Select Top 1 AppID From DD_Profile With (nolock) Where DDID = WF_Attachment.PaperID And CustomerID = WF_Attachment.CustomerID) And CustomerID = WF_Attachment.CustomerID) CustomerNm")
        strSQL.AppendLine(" From WF_Attachment With (nolock)")
        strSQL.AppendLine("Where PaperID in (Select DDID From DD_Profile With (nolock) Where AppID = " & Bsp.Utility.Quote(AppID) & " And CustomerID = " & Bsp.Utility.Quote(CustomerID) & " And Status <> '0')")
        strSQL.AppendLine("And FlowID = 'DD'")
        If DocType <> "" Then
            strSQL.AppendLine("And DocType = " & Bsp.Utility.Quote(DocType))
        End If
        strSQL.AppendLine("Order by FlowID, SeqNo")

        Return strSQL.ToString()
    End Function

    ''' <summary>
    ''' 取得上傳附件資訊
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowCaseID"></param>
    ''' <param name="SeqNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAttachment(ByVal FlowID As String, ByVal FlowCaseID As String, Optional ByVal SeqNo As Integer = 0) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select * From WF_Attachment with (nolock)")
        strSQL.AppendLine("Where FlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("And FlowCaseID = " & Bsp.Utility.Quote(FlowCaseID))
        If SeqNo <> 0 Then
            strSQL.AppendLine("And SeqNo = " & SeqNo.ToString())
        Else
            strSQL.AppendLine("Order by SeqNo")
        End If


        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    ''' <summary>
    ''' 新增附件
    ''' </summary>
    ''' <param name="beAttachment"></param>
    ''' <param name="tran"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Insert_Attachment(ByVal beAttachment As beWF_Attachment.Row, ByVal tran As DbTransaction) As Integer
        Dim bsAttachment As New beWF_Attachment.Service()

        beAttachment.SeqNo.Value = GetMaxAttachmentSeqNo(beAttachment.FlowID.Value, beAttachment.FlowCaseID.Value, tran)
        Return bsAttachment.Insert(beAttachment, tran)
    End Function

    ''' <summary>
    ''' 刪除上傳之附件
    ''' </summary>
    ''' <param name="beAttachment"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Delete_Attachment(ByVal beAttachment As beWF_Attachment.Row) As Integer
        Dim bsAttachment As New beWF_Attachment.Service()
        Dim strFilePath As String = System.IO.Path.Combine(beAttachment.Path.Value, beAttachment.ActFileName.Value)
        Try
            If System.IO.File.Exists(strFilePath) Then
                System.IO.File.Delete(strFilePath)
            End If
            Return bsAttachment.DeleteRowByPrimaryKey(beAttachment)
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' 取的下一個附件序號
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowCaseID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetMaxAttachmentSeqNo(ByVal FlowID As String, ByVal FlowCaseID As String, ByVal tran As DbTransaction) As String
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select Isnull(Max(SeqNo), 0) + 1")
        strSQL.AppendLine("From WF_Attachment")
        strSQL.AppendLine("Where FlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("And FlowCaseID = " & Bsp.Utility.Quote(FlowCaseID))

        Return Bsp.DB.ExecuteScalar(strSQL.ToString(), tran).ToString()
    End Function
#End Region

#Region "WFA070 : 待辦事項查詢"
    ''' <summary>
    ''' 流程查詢待辦事項
    ''' </summary>
    ''' <param name="args"></param>
    ''' <returns></returns>
    ''' <remarks>For WFA070使用且僅提供未完成待辦查詢</remarks>
    Public Function GetToDoListForWFA070(ByVal args() As Object) As DataTable
        Dim strSQL As New StringBuilder()
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(args)

        strSQL.AppendLine("Select FlowID, FlowCaseID, CONVERT(varchar(2), FlowLogBatNo) as FlowLogBatNo, FlowLogID, AssignTo, ")
        strSQL.AppendLine("     b.UserName UserName, c.OrganID + '-' + c.OrganName AssignOrg,")
        strSQL.AppendLine("     FlowDispatchFlag, FlowStepID + FlowStepDesc AS FlowStepDesc, ")
        strSQL.AppendLine("     PaperID, SUBSTRING(FlowShowValue, CHARINDEX('|', FlowShowValue) + 1, CHARINDEX('|', FlowShowValue, CHARINDEX('|', FlowShowValue) + 1) - CHARINDEX('|', FlowShowValue) - 1) AS CustomerName,")
        strSQL.AppendLine("     FromBrName, FromUserName, FromDate, FlowCaseStatus, CrUser, CrBr, CrDate")
        strSQL.AppendLine("From WF_FlowToDoList a with (nolock) inner join SC_User b with (nolock) on a.AssignTo = b.UserID")
        strSQL.AppendLine("     inner join SC_Organization c with (nolock) on b.DeptID = c.OrganID")
        strSQL.AppendLine("Where a.FlowCaseStatus = 'Open'")

        For Each strKey As String In ht.Keys
            Select Case strKey
                Case "ddlAO"
                    If ht(strKey).ToString().Trim() <> "" Then
                        strSQL.AppendLine("And a.AssignTo = " & Bsp.Utility.Quote(ht(strKey).ToString().Trim()))
                    Else
                        If ht.Contains("ddlOrgan") AndAlso ht("ddlOrgan").ToString().Trim() <> "" Then
                            strSQL.AppendLine("And b.DeptID = " & Bsp.Utility.Quote(ht("ddlOrgan").ToString().Trim()))
                        End If
                    End If
                Case "txtApplyCaseS"
                    If ht(strKey).ToString().Trim() <> "" Then
                        strSQL.AppendLine("And a.CrDate >= " & Bsp.Utility.Quote(ht(strKey).ToString().Trim()))
                    End If
                Case "txtApplyCaseE"
                    If ht(strKey).ToString().Trim() <> "" Then
                        strSQL.AppendLine("And a.CrDate <= " & Bsp.Utility.Quote(ht(strKey).ToString().Trim()))
                    End If
                Case "ddlFlowName"
                    If ht(strKey).ToString().Trim() <> "" Then
                        strSQL.AppendLine("And a.FlowID = " & Bsp.Utility.Quote(ht(strKey).ToString().Trim()))
                    End If
            End Select
        Next

        strSQL.AppendLine("Order by a.AssignTo, a.FromDate")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function
#End Region

#Region "WFA080 : 簽核主管設定"
    '核貸委員WF_LoanCheckerLog清單  
    Function WF_LoanCheckerLogList(ByVal FlowID As String, ByVal FlowCaseID As String, ByVal SetupStepID As String) As DataSet
        Dim strSQL As New StringBuilder

        strSQL.AppendLine("SELECT  ROW_NUMBER() OVER (ORDER BY a.FlowCaseID, a.LogChgDate, a.LastChgDate, a.FlowSeq ) AS RowSeq, ")
        strSQL.AppendLine("     a.FlowCaseID, a.SetupStepID,case a.SetupStepID when 'C50000' then '總行簽核主管' else '簽核主管' end  as LoanCheckKind,")
        strSQL.AppendLine("     a.FlowSeq, a.LoanCheckID + '-' + dbo.funGetAOrgDefine('3', a.LoanCheckID) as LoanCheckIDName, ")
        strSQL.AppendLine("     a.LoanCheckdateBeg, a.LoanCheckdateEnd, ")
        strSQL.AppendLine("     a.LastChgID + '-' + dbo.funGetAOrgDefine('3', a.LastChgID) as LastChgIDName, a.LastChgDate, ")
        strSQL.AppendLine("     a.LogChgID + '-' + dbo.funGetAOrgDefine('3', a.LogChgID) as LogChgIDName, a.LogChgDate, a.LogDesc ")
        strSQL.AppendLine("FROM WF_LoanCheckerLog a with (nolock)")
        strSQL.AppendLine("WHERE FlowID = '" & FlowID & "' AND FlowCaseID = '" & FlowCaseID & "' AND SetupStepID = '" & SetupStepID & "' ")
        strSQL.AppendLine("ORDER BY a.FlowCaseID, a.LogChgDate, a.LastChgDate, a.FlowSeq")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString())
    End Function

    '查詢ToDoList中相關欄位資訊，利用FlowID,FlowBatNo,FlowCaseID,FlowLogID
    Public Function ToDisUserQTable(ByVal FlowID As String, ByVal FlowCaseID As String, ByVal FlowBatNo As String, ByVal FlowLogID As String) As DataTable
        Dim strSQL As New StringBuilder

        strSQL.AppendLine("SELECT * FROM WF_FlowToDoList with (nolock) ")
        strSQL.AppendLine("Where FlowID = @FlowID")
        strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
        strSQL.AppendLine("And FlowLogBatNo  = @FlowBatNo")
        strSQL.AppendLine("And FlowLogID = @FlowLogID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), New DbParameter() { _
            Bsp.DB.getDbParameter("@FlowID", FlowID), _
            Bsp.DB.getDbParameter("@FlowCaseID", FlowCaseID), _
            Bsp.DB.getDbParameter("@FlowBatNo", FlowBatNo), _
            Bsp.DB.getDbParameter("@FlowLogID", FlowLogID)}).Tables(0)
    End Function

    Public Shared Sub FillOrganByValue(ByVal objDDL As DropDownList, ByVal QueryDept As String, ByVal sRegionID As String, ByVal CondStr As String)
        Dim strSQL As New StringBuilder

        strSQL.AppendLine("Select distinct OrganID, OrganID + '-' + OrganName FullName")
        strSQL.AppendLine("From SC_Organization with (nolock)")
        strSQL.AppendLine("Where 1 =1 ")
        If QueryDept <> "" Then
            strSQL.AppendLine(" and OrganID in ('" & QueryDept.Replace(",", "','") & "')")
        End If
        If sRegionID <> "" Then
            strSQL.AppendLine(" And RegionID = " & Bsp.Utility.Quote(sRegionID))
        End If
        If CondStr <> "" Then
            strSQL.AppendLine(CondStr)
        End If
        strSQL.AppendLine("Order by OrganID")

        Try
            Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
                With objDDL
                    .Items.Clear()
                    .DataTextField = "FullName"
                    .DataValueField = "OrganID"
                    .DataSource = dt
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    '特殊，Flow用來判斷單位種類，,另將其他訂義單位歸為何種類，Kind =1:法金，kind = 2 :育成 
    Public Function WFSpecRegKind(ByVal AppID As String, Optional ByVal DefaultKind As String = "k2") As String
        Dim strSQL As String = "Select FlowType From AP_Profile with (nolock) Where AppID = " & Bsp.Utility.Quote(AppID)
        Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL).Tables(0)
            If dt.Rows.Count > 0 Then
                Select Case dt.Rows(0).Item("FlowType").ToString()
                    Case "E"
                        Return "k1"
                    Case "C", "N"
                        Return "k2"
                    Case Else
                        Return DefaultKind
                End Select
            Else
                Return DefaultKind
            End If
        End Using
    End Function

    'ReBuileLoanChecker:取得核貸委員檔
    Public Sub ReBuileLoanChecker(ByVal FlowCaseID As String, ByVal Kind As String, ByVal SetupStepID As String, ByVal LoanChecker As String, Optional ByVal FlowID As String = "AP") 'Kind=0 全部重建，Kind=1簽核過的不重建
        Using cn As DbConnection = Bsp.DB.getConnection()
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Try
                WF_LoanCheckerLogIns(FlowCaseID, Kind, UserProfile.UserID, "簽核主管重訂-刪除", SetupStepID, tran, FlowID)
                WF_LoanCheckerDel(FlowCaseID, Kind, SetupStepID, tran, FlowID)
                WF_LoanCheckerIns(FlowCaseID, UserProfile.UserID, SetupStepID, LoanChecker, tran, FlowID)
                WF_LoanCheckerLogIns(FlowCaseID, Kind, UserProfile.UserID, "簽核主管重訂-新增", SetupStepID, tran, FlowID)
                tran.Commit()
            Catch ex As Exception
                tran.Rollback()
                Throw
            Finally
                If cn.State = ConnectionState.Open Then cn.Close()
            End Try
        End Using

    End Sub

    Public Function WFGetUserInfo(ByVal UserID As String) As DataSet
        Dim strSQL As New StringBuilder

        strSQL.AppendLine("SELECT  a.UserID, a.UserName, a.CompID, a.CompName,a.DeptID, ")
        strSQL.AppendLine(" (select OrganName from SC_Organization with (nolock) where  OrganID = a.DeptID) as DeptName,")
        strSQL.AppendLine(" a.OrganID, a.RankID, a.EMail, b.OrganName, b.OrganType, b.GroupID, b.GroupName")
        strSQL.AppendLine("FROM  SC_User a with (nolock) left outer join SC_Organization b with (nolock) on a.OrganID = b.OrganID")
        strSQL.AppendLine("where a.UserID = @UserID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), _
            Bsp.DB.getDbParameter("@UserID", UserID))
    End Function

    '新增該筆資料核貸委員最大順序
    Public Function WF_LoanCheckerMaxSeq(ByVal FlowCaseID As String, ByVal SetupStepID As String, ByVal tran As DbTransaction, Optional ByVal FlowID As String = "AP") As Integer
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("SELECT ISNULL(MAX(FlowSeq), 0) AS FlowSeq")
        strSQL.AppendLine("FROM WF_LoanChecker")
        strSQL.AppendLine("WHERE FlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("AND FlowCaseID = " & Bsp.Utility.Quote(FlowCaseID))

        If SetupStepID <> "" Then
            strSQL.AppendLine(" AND SetupStepID = '" & SetupStepID & "' ")
        End If

        Return CInt(Bsp.DB.ExecuteScalar(strSQL.ToString(), tran))
    End Function

    '新增核貸委員
    Public Sub WF_LoanCheckerIns(ByVal FlowCaseID As String, ByVal UserID As String, ByVal SetupStepID As String, ByVal LoanChecker As String, ByVal tran As DbTransaction, Optional ByVal FlowID As String = "AP")
        Dim Seq1 As Integer = WF_LoanCheckerMaxSeq(FlowCaseID, SetupStepID, tran)
        LoanChecker &= ","
        Dim Scobj As New SC
        Dim LoanCheckerArr() As String = LoanChecker.Split(",")
        For intLoopB As Integer = 0 To LoanCheckerArr.GetUpperBound(0) - 1
            If Trim(LoanCheckerArr(intLoopB).ToString) <> "" Then
                Seq1 = Seq1 + 1
                Dim strSQL As New StringBuilder
                strSQL.AppendLine("insert into WF_LoanChecker(FlowID, FlowCaseID,SetupStepID,FlowSeq,LoanCheckID,AgreeFlag,FlowStepAction,")
                strSQL.AppendLine("FlowStepOpinion,LastChgID,LastChgDate,LoanCheckDeptID,LoanCheckDeptName) values(")
                strSQL.AppendLine(Bsp.Utility.Quote(FlowID) & ",")
                strSQL.AppendLine(Bsp.Utility.Quote(FlowCaseID) & ",")
                strSQL.AppendLine(Bsp.Utility.Quote(SetupStepID) & ",")
                strSQL.AppendLine(Bsp.Utility.Quote(Seq1) & ",")
                strSQL.AppendLine(Bsp.Utility.Quote(Trim(LoanCheckerArr(intLoopB).ToString)) & ",")
                strSQL.AppendLine("Null,")
                strSQL.AppendLine("Null,")
                strSQL.AppendLine("Null,")
                strSQL.AppendLine(Bsp.Utility.Quote(UserID) & ",")
                strSQL.AppendLine("getdate(),")
                Using dt As DataTable = WFGetUserInfo(LoanCheckerArr(intLoopB).ToString()).Tables(0)
                    If dt.Rows.Count > 0 Then
                        strSQL.AppendLine(Bsp.Utility.Quote(dt.Rows(0).Item("DeptID").ToString()) & ", " & _
                                          Bsp.Utility.Quote(dt.Rows(0).Item("DeptName").ToString()))
                    Else
                        strSQL.AppendLine("'',''")
                    End If
                End Using
                strSQL.AppendLine(")")
                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)
            End If
        Next

    End Sub

    '刪除WF_LoanChecker
    Public Sub WF_LoanCheckerDel(ByVal FlowCaseID As String, ByVal Kind As String, ByVal SetupStepID As String, ByVal tran As DbTransaction, Optional ByVal FlowID As String = "AP")
        Dim strSQL As New StringBuilder
        strSQL.AppendLine("delete FROM  WF_LoanChecker ")
        strSQL.AppendLine("Where FlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("And FlowCaseID = " & Bsp.Utility.Quote(FlowCaseID))

        If Kind <> "0" Then
            strSQL.AppendLine("  and isNull(LoanCheckdateBeg,'') = '' ")
        End If
        If SetupStepID <> "" Then
            strSQL.AppendLine(" and SetupStepID = '" & SetupStepID & "' ")
        End If
        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)
    End Sub

    '寫入WF_LoanCheckerLog
    Public Sub WF_LoanCheckerLogIns(ByVal FlowCaseID As String, ByVal Kind As String, ByVal UserID As String, ByVal LogDesc As String, ByVal SetupStepID As String, ByVal tran As DbTransaction, Optional ByVal FlowID As String = "AP")
        Dim strSQL As New StringBuilder

        strSQL.AppendLine("INSERT INTO WF_LoanCheckerLog (")
        strSQL.AppendLine(" FlowID, FlowCaseID, SetupStepID, FlowSeq, LoanCheckID, LoanCheckdateBeg, LoanCheckdateEnd, AgreeFlag, ")
        strSQL.AppendLine(" FlowStepAction, FlowStepOpinion, LastChgID,  LastChgDate, LogChgID, LogChgDate, LogDesc)")
        strSQL.AppendLine("SELECT FlowID, FlowCaseID,SetupStepID,FlowSeq,LoanCheckID,LoanCheckdateBeg,LoanCheckdateEnd,AgreeFlag, ")
        strSQL.AppendLine(" FlowStepAction,FlowStepOpinion,LastChgID,LastChgDate, '" & UserID & "' AS LogChgID, getdate() AS LogChgDate, '" & LogDesc & "' AS LogDesc")
        strSQL.AppendLine("FROM WF_LoanChecker ")
        strSQL.AppendLine("WHERE FlowCaseID = " & Bsp.Utility.Quote(FlowCaseID))
        strSQL.AppendLine("AND FlowID = " & Bsp.Utility.Quote(FlowID))

        If Kind <> "0" Then
            strSQL.AppendLine("  and isNull(LoanCheckdateBeg,'') = '' ")
        End If
        If SetupStepID <> "" Then
            strSQL.AppendLine("And SetupStepID = " & Bsp.Utility.Quote(SetupStepID))
        End If
        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)
    End Sub

    Public Function getSC_LoanChecker_In(ByVal FlowCaseID As String, ByVal SetupStepID As String, ByVal OrganID As String, Optional ByVal FlowID As String = "AP") As DataSet
        Dim strSQL As New StringBuilder
        strSQL.AppendLine("Select a.LoanCheckID, b.UserID + '-' + b.UserName + '/' + dbo.funGetAOrgDefine('4', b.DeptID) AS OrganName")
        strSQL.AppendLine("From WF_LoanChecker a with (nolock) Left outer join SC_User b with (nolock) on a.LoanCheckID = b.UserID")
        strSQL.AppendLine("Where a.FlowCaseID = " & Bsp.Utility.Quote(FlowCaseID))
        strSQL.AppendLine("And a.FlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("And a.LoanCheckdateBeg is null")
        If SetupStepID.IndexOf(",") >= 0 Then
            strSQL.AppendLine("And a.SetupStepID in ('" & SetupStepID.Replace(",", "','") & "')")
        Else
            strSQL.AppendLine("And a.SetupStepID = " & Bsp.Utility.Quote(SetupStepID))
        End If
        If OrganID <> "" Then
            strSQL.AppendLine("And b.DeptID = " & Bsp.Utility.Quote(OrganID))
        End If
        strSQL.AppendLine("Order by a.FlowSeq")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString())
    End Function

    Public Function getSC_LoanChecker_Out(ByVal FlowCaseID As String, ByVal SetupStepID As String, ByVal OrganID As String, Optional ByVal FlowID As String = "AP") As DataSet
        Dim strSQL As New StringBuilder

        strSQL.AppendLine("Select UserID, UserID + '-' + UserName + '/' + dbo.funGetAOrgDefine('4', DeptID) as OrganName")
        strSQL.AppendLine("From SC_User b with (nolock)")
        strSQL.AppendLine("Where UserID not in (Select LoanCheckID From WF_LoanChecker with (nolock) Where FlowID = " & Bsp.Utility.Quote(FlowID) & " And FlowCaseID = " & Bsp.Utility.Quote(FlowCaseID) & " and SetupStepID = " & Bsp.Utility.Quote(SetupStepID) & " And AgreeFlag is null)")
        strSQL.AppendLine("And BanMark = '0' And WorkTypeID <> 'ZZZZZZ'")
        If OrganID <> "" Then
            strSQL.AppendLine("And b.DeptID = " & Bsp.Utility.Quote(OrganID))
        End If
        strSQL.AppendLine("Order by DeptID, UserID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString())
    End Function

    '更新下關核代委員之初始時間
    Public Sub UpdNextLoanCheckdateBeg(ByVal FlowCaseID As String, ByVal LoanCheckID As String, ByVal CondStr As String, ByVal tran As DbTransaction, Optional ByVal FlowID As String = "AP")
        Dim strSQL As New StringBuilder

        strSQL.AppendLine("Update WF_LoanChecker Set")
        strSQL.AppendLine("     LoanCheckdateBeg = Getdate()")
        strSQL.AppendLine("Where FlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("And FlowCaseID = " & Bsp.Utility.Quote(FlowCaseID))
        strSQL.AppendLine("And LoanCheckID in ('" & LoanCheckID.Replace(",", "','") & "')")
        strSQL.AppendLine("And Isnull(LoanCheckdateBeg, '') = ''")
        strSQL.AppendLine(CondStr)

        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)
    End Sub

    '更新下關核代委員之初始時間
    Public Sub UpdNextLoanCheckdateBeg(ByVal FlowCaseID As String, ByVal LoanCheckID As String, ByVal BeginDate As String, ByVal CondStr As String, ByVal tran As DbTransaction, Optional ByVal FlowID As String = "AP")
        Dim strSQL As New StringBuilder

        strSQL.AppendLine("Update WF_LoanChecker Set")
        strSQL.AppendLine("     LoanCheckdateBeg = " & Bsp.Utility.Quote(BeginDate))
        strSQL.AppendLine("Where FlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("And FlowCaseID = " & Bsp.Utility.Quote(FlowCaseID))
        strSQL.AppendLine("And LoanCheckID in ('" & LoanCheckID.Replace(",", "','") & "')")
        strSQL.AppendLine("And Isnull(LoanCheckdateBeg, '') = ''")
        strSQL.AppendLine(CondStr)

        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)
    End Sub

    '退回AO時，將起始日期，結束日期及意見等
    Private Sub UpdBackLoanCheck(ByVal FlowCaseID As String, ByVal CondStr As String, ByVal tran As DbTransaction, Optional ByVal FlowID As String = "AP")
        Dim strSQL As New StringBuilder
        strSQL.AppendLine("UPDATE WF_LoanChecker SET LoanCheckdateBeg = Null,LoanCheckdateEnd=Null,AgreeFlag=Null,FlowStepAction=Null,FlowStepOpinion=Null ")
        strSQL.AppendLine("WHERE FlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("And FlowCaseID = " & Bsp.Utility.Quote(FlowCaseID))
        strSQL.AppendLine(CondStr)

        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)
    End Sub

    ''' <summary>
    ''' 寫入核貸委員簽核檔
    ''' </summary>
    ''' <param name="FlowCaseID"></param>
    ''' <param name="FlowLogID"></param>
    ''' <param name="LoanCheckID"></param>
    ''' <param name="FlowStepAction"></param>
    ''' <param name="FlowStepOpinion"></param>
    ''' <param name="FlowStepID"></param>
    ''' <param name="tran"></param>
    ''' <param name="FlowID"></param>
    ''' <remarks></remarks>
    Public Sub UpdCurrLoanCheckActOpin(ByVal FlowCaseID As String, ByVal FlowLogID As String, ByVal LoanCheckID As String, ByVal FlowStepAction As String, ByVal FlowStepOpinion As String, ByVal FlowStepID As String, ByVal tran As DbTransaction, Optional ByVal FlowID As String = "AP")
        Dim strSQL As New StringBuilder
        Dim AgreeFlag As String

        strSQL.AppendLine("Select AgreeFlag From WF_FlowStepD")
        strSQL.AppendLine("Where FlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("And FlowStepID = " & Bsp.Utility.Quote(FlowStepID))
        strSQL.AppendLine("And ButtonName = " & Bsp.Utility.Quote(FlowStepAction))

        AgreeFlag = Bsp.Utility.IsStringNull(Bsp.DB.ExecuteScalar(strSQL.ToString(), tran), "N")

        strSQL = New StringBuilder()
        strSQL.AppendLine("UPDATE a SET")
        strSQL.AppendLine(" FlowStepAction = " & Bsp.Utility.Quote(FlowStepAction))
        strSQL.AppendLine(" , FlowStepOpinion = N" & Bsp.Utility.Quote(FlowStepOpinion))
        strSQL.AppendLine(" , AgreeFlag = " & Bsp.Utility.Quote(AgreeFlag))
        strSQL.AppendLine(" , LoanCheckdateEnd = GetDate()")
        strSQL.AppendLine(" , FlowLogID = " & Bsp.Utility.Quote(FlowLogID))

        If LoanCheckID <> UserProfile.ActUserID Then '因代理人員，依代職不代權處理
            strSQL.AppendLine(" , LoanCheckID = " & Bsp.Utility.Quote(UserProfile.ActUserID))
            strSQL.AppendLine(" , LoanCheckDeptID = " & Bsp.Utility.Quote(UserProfile.ActDeptID))
            strSQL.AppendLine(" , LoanCheckDeptName = " & Bsp.Utility.Quote(UserProfile.ActDeptName))
        End If

        strSQL.AppendLine("From WF_LoanChecker a inner join (")
        strSQL.AppendLine("	Select top 1 FlowID, FlowCaseID, SetupStepID, LoanCheckID, FlowSeq")
        strSQL.AppendLine("	From WF_LoanChecker")
        strSQL.AppendLine("	Where FlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("	And FlowCaseID = " & Bsp.Utility.Quote(FlowCaseID))
        strSQL.AppendLine("	And LoancheckID = " & Bsp.Utility.Quote(LoanCheckID))
        strSQL.AppendLine("	And ISNULL(LoanCheckdateBeg, '') <> ''")
        strSQL.AppendLine("	And ISNULL(LoanCheckdateEnd, '') = '' Order by LastChgDate desc, FlowSeq desc) b")
        strSQL.AppendLine("  on a.FlowID = b.FlowID And a.FlowCaseID = b.FlowCaseID And a.SetupStepID = b.SetupStepID And a.FlowSeq = b.FlowSeq")

        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)
    End Sub

    'Public Function getSC_LoanChecker(ByVal FlowCaseID As String, ByVal SetupStepID As String, ByVal OrganID As String, ByVal CondStr As String, Optional ByVal FlowID As String = "AP") As DataSet
    '    Dim strSQL As New StringBuilder

    '    If OrganID <> "" Then
    '        CondStr = " and b.DeptID = '" & OrganID & "' " & CondStr
    '    End If

    '    strSQL.AppendLine("Select a.LoanCheckID, b.UserID + '-' + b.UserName + '/' + dbo.funGetAOrgDefine('4', b.DeptID) AS OrganName")
    '    strSQL.AppendLine("From WF_LoanChecker a with (nolock) Left outer join SC_User b with (nolock) on a.LoanCheckID = b.UserID")
    '    strSQL.AppendLine("Where a.FlowCaseID = " & Bsp.Utility.Quote(FlowCaseID))
    '    strSQL.AppendLine("And a.FlowID = " & Bsp.Utility.Quote(FlowID))
    '    strSQL.AppendLine("And a.LoanCheckdateBeg is null")
    '    strSQL.AppendLine("And a.SetupStepID = " & Bsp.Utility.Quote(SetupStepID))
    '    strSQL.AppendLine(CondStr)
    '    strSQL.AppendLine(" Order by a.FlowSeq")

    '    Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString())
    'End Function
#End Region

#Region "WFA090  CCA080：結案流程退一關"
    Public Function getFinishCase(ByVal ht As Hashtable) As DataTable
        Dim strSQL As New StringBuilder

        '20121123 (U) 201209240086, 配合個資法規範
        strSQL.AppendLine("Select Top 200 a.AppID, a.CustomerID, b.CName, c.Define AppTypeNm, d.Define AppStatusNm, ")
        strSQL.AppendLine(" Convert(varchar(10), e.ApproveDate, 111) ApproveDate, Convert(varchar(10), e.EffectDate, 111) EffectDate,")
        strSQL.AppendLine(" dbo.funGetAOrgDefine('4', a.BusinessID) OrganName, dbo.funGetAOrgDefine('1', a.OfficerID) OfficerNm")
        strSQL.AppendLine("From AP_Profile a with (nolock) ")
        strSQL.AppendLine(" inner join BR_Customer b with (nolock) on a.AppID = b.AppID and a.CustomerID = b.CustomerID")
        strSQL.AppendLine(" inner join SC_Common c with (nolock) on a.AppType = c.Code and c.Type = '101'")
        strSQL.AppendLine(" inner join SC_Common d with (nolock) on a.AppStatus = d.Code and d.Type = '100'")
        strSQL.AppendLine(" inner join AL_Profile e with (nolock) on a.AppID = e.AppID")
        'strSQL.AppendLine(" inner join DB_CD..db_cdline_dds f with (nolock) on e.ALID = f.seq")
        strSQL.AppendLine("Where a.AppStatus = '5'")
        'strSQL.AppendLine("And a.DataSrc = ''")
        strSQL.AppendLine("And a.AppID like 'AP%'")
        'strSQL.AppendLine("And f.actcd = '0'")

        '部門, AO
        If ht.ContainsKey("ddlOrgan") Then
            If ht("ddlAO").ToString() <> "" Then
                strSQL.AppendLine("And a.AppID in (Select AppID From BR_Customer with (nolock) Where AppStatus = '5' and CustomerID in (Select idno From DB_CDNJ..db_cdcif_dds Where aoempno = " & Bsp.Utility.Quote(ht("ddlAO")) & "))")
            Else
                strSQL.AppendLine("And a.AppID in (Select AppID From BR_Customer with (nolock) Where AppStatus = '5' and CustomerID in (Select idno From DB_CDNJ..db_cdcif_dds where bkorgid in ('" & ht("ddlOrgan").Replace(",", "','") & "')))")
            End If
        End If
        'AppID
        If ht.ContainsKey("txtAppID") Then
            If ht("txtAppID").ToString() <> "" Then
                If ht("ddlAppID").ToString = "=" Then
                    strSQL.AppendLine("And a.AppID = " & Bsp.Utility.Quote(ht("txtAppID")))
                Else
                    strSQL.AppendLine("And a.AppID like " & Bsp.Utility.Quote(ht("txtAppID") & "%"))
                End If
            End If
        End If
        'CustomerID
        If ht.ContainsKey("txtCustomerID") Then
            If ht("txtCustomerID").ToString() <> "" Then
                If ht("ddlCustomerID").ToString = "=" Then
                    strSQL.AppendLine("And a.AppID in (Select AppID From BR_Customer with (nolock) Where CustomerID = " & Bsp.Utility.Quote(ht("txtCustomerID")) & " And AppStatus = '5')")
                Else
                    strSQL.AppendLine("And a.AppID in (Select AppID From BR_Customer with (nolock) Where CustomerID like " & Bsp.Utility.Quote(ht("txtCustomerID") & "%") & " And AppStatus = '5')")
                End If
            End If
        End If
        'CName
        If ht.ContainsKey("txtCName") Then
            If ht("txtCName").ToString() <> "" Then
                If ht("ddlCName").ToString = "=" Then
                    strSQL.AppendLine("And a.AppID in (Select AppID From BR_Customer with (nolock) Where CName = " & Bsp.Utility.Quote(ht("txtCName")) & " And AppStatus = '5')")
                Else
                    strSQL.AppendLine("And a.AppID in (Select AppID From BR_Customer with (nolock) Where CName like " & Bsp.Utility.Quote(ht("txtCName") & "%") & " And AppStatus = '5')")
                End If
            End If
        End If
        strSQL.AppendLine("Order by a.AppID desc")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    'CC99-徵信結案
    Public Function getCC99Case(ByVal ht As Hashtable) As DataTable
        Dim strSQL As New StringBuilder
        'Dim strSQL_Boss As New StringBuilder
        'Dim strSQL_Mate As New StringBuilder

        strSQL.AppendLine("Select Top 200 a.CCID, a.AppID, a.CustomerID, c.CName, a.LastChgDate, Case When c.AO_CODE = 'C50000' Then '' Else dbo.funGetAOrgDefine('1', c.AO_CODE) End [OfficerNm]")
        strSQL.AppendLine("From CC_Profile a with (nolock) inner join AP_Profile b with (nolock) on a.AppID = b.AppID")
        strSQL.AppendLine("inner join BR_Customer c with (nolock) on a.AppID = c.AppID and a.CustomerID = c.CustomerID")
        strSQL.AppendLine("Where a.Status = '9'")
        '2014.06.17 (M) Chung 暫時取消判斷授信案件狀態
        'strSQL.AppendLine("And b.AppStatus = '1'") 
        strSQL.AppendLine("And a.RefFlag = '0'")
        strSQL.AppendLine("And a.CCID like 'CC%' ")
        strSQL.AppendLine("And ( Not Exists ( Select d.* From CC_Profile d with (nolock)  Where a.CCID = d.CCID  and   RefFlag = '1' )) ")

        '部門, AO
        If ht.ContainsKey("ddlOrgan") Then
            If ht("ddlAO").ToString() <> "" Then
                strSQL.AppendLine("And c.AO_CODE = " & Bsp.Utility.Quote(ht("ddlAO")))
            Else
                strSQL.AppendLine("And (c.AO_CODE = 'C50000' or c.AO_CODE in (Select UserID From SC_User with (nolock) Where DeptID in (" & Bsp.Utility.Quote(ht("ddlOrgan")).Replace(",", "','") & ")))")
            End If
        End If
        'AppID
        If ht.ContainsKey("txtAppID") Then
            If ht("txtAppID").ToString() <> "" Then
                If ht("ddlAppID").ToString = "=" Then
                    strSQL.AppendLine("And (a.AppID = " & Bsp.Utility.Quote(ht("txtAppID")))
                    strSQL.AppendLine("  or a.CCID = " & Bsp.Utility.Quote(ht("txtAppID")) & ")")
                Else
                    strSQL.AppendLine("And (a.AppID like " & Bsp.Utility.Quote("%" & ht("txtAppID") & "%"))
                    strSQL.AppendLine("  or a.CCID like " & Bsp.Utility.Quote("%" & ht("txtAppID") & "%") & ")")
                End If
            End If
        End If
        'CustomerID
        If ht.ContainsKey("txtCustomerID") Then
            If ht("txtCustomerID").ToString() <> "" Then
                If ht("ddlCustomerID").ToString = "=" Then
                    strSQL.AppendLine("And a.CustomerID = " & Bsp.Utility.Quote(ht("txtCustomerID")))
                Else
                    strSQL.AppendLine("And a.CustomerID like " & Bsp.Utility.Quote("%" & ht("txtCustomerID") & "%"))
                End If
            End If
        End If
        'CName
        If ht.ContainsKey("txtCName") Then
            If ht("txtCName").ToString() <> "" Then
                If ht("ddlCName").ToString = "=" Then
                    strSQL.AppendLine("And c.CName = " & Bsp.Utility.Quote(ht("txtCName")))
                Else
                    strSQL.AppendLine("And c.CName like " & Bsp.Utility.Quote("%" & ht("txtCName") & "%"))
                End If
            End If
        End If
        strSQL.AppendLine("Order by a.CCID desc")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    'CC98-徵信銷案
    Public Function getCC98Case(ByVal ht As Hashtable) As DataTable
        Dim strSQL As New StringBuilder

        strSQL.AppendLine("Select Top 200 * From (")
        strSQL.AppendLine("Select a.CCID, a.AppID, a.CustomerID, c.CName, a.LastChgDate, dbo.funGetAOrgDefine('1', c.AO_CODE) [OfficerNm]")
        strSQL.AppendLine("From CC_Profile a with (nolock) inner join AP_Profile b with (nolock) on a.AppID = b.AppID")
        strSQL.AppendLine("     inner join BR_Customer c with (nolock) on a.AppID = c.AppID and a.CustomerID = c.CustomerID")
        strSQL.AppendLine("     inner join WF_FlowCase d with (nolock) on a.CCID = d.PaperID")
        strSQL.AppendLine("Where a.Status = '0'")
        strSQL.AppendLine("And b.AppStatus = '1'")
        strSQL.AppendLine("And a.RefFlag = '0'")
        strSQL.AppendLine("And a.CCID like 'CC%'")
        strSQL.AppendLine("And Exists (")
        strSQL.AppendLine("     Select * From WF_FlowFullLog with (nolock) where FlowCaseID = d.FlowCaseID And FlowID = d.FlowID")
        strSQL.AppendLine("     And FlowLogBatNo = (Select Max(FlowLogBatNo) - 1 From WF_FlowFullLog with (nolock) Where FlowCaseID = d.FlowCaseID And FlowID = d.FlowID)")
        strSQL.AppendLine("     And FlowStepID in ('CC15','CC14','CC05','CC04')) ")

        '部門, AO
        If ht.ContainsKey("ddlOrgan") Then
            If ht("ddlAO").ToString() <> "" Then
                strSQL.AppendLine("And c.AO_CODE = " & Bsp.Utility.Quote(ht("ddlAO")))
            Else
                strSQL.AppendLine("And c.AO_CODE in (Select UserID From SC_User with (nolock) Where DeptID in (" & Bsp.Utility.Quote(ht("ddlOrgan")).Replace(",", "','") & "))")
            End If
        End If
        'AppID
        If ht.ContainsKey("txtAppID") Then
            If ht("txtAppID").ToString() <> "" Then
                If ht("ddlAppID").ToString = "=" Then
                    strSQL.AppendLine("And (a.AppID = " & Bsp.Utility.Quote(ht("txtAppID")))
                    strSQL.AppendLine("  or a.CCID = " & Bsp.Utility.Quote(ht("txtAppID")) & ")")
                Else
                    strSQL.AppendLine("And (a.AppID like " & Bsp.Utility.Quote("%" & ht("txtAppID") & "%"))
                    strSQL.AppendLine("  or a.CCID like " & Bsp.Utility.Quote("%" & ht("txtAppID") & "%") & ")")
                End If
            End If
        End If
        'CustomerID
        If ht.ContainsKey("txtCustomerID") Then
            If ht("txtCustomerID").ToString() <> "" Then
                If ht("ddlCustomerID").ToString = "=" Then
                    strSQL.AppendLine("And a.CustomerID = " & Bsp.Utility.Quote(ht("txtCustomerID")))
                Else
                    strSQL.AppendLine("And a.CustomerID like " & Bsp.Utility.Quote("%" & ht("txtCustomerID") & "%"))
                End If
            End If
        End If
        'CName
        If ht.ContainsKey("txtCName") Then
            If ht("txtCName").ToString() <> "" Then
                If ht("ddlCName").ToString = "=" Then
                    strSQL.AppendLine("And c.CName = " & Bsp.Utility.Quote(ht("txtCName")))
                Else
                    strSQL.AppendLine("And c.CName like " & Bsp.Utility.Quote("%" & ht("txtCName") & "%"))
                End If
            End If
        End If

        strSQL.AppendLine(") x")
        strSQL.AppendLine("Order by CCID desc")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    Public Function getCRCase(ByVal ht As Hashtable) As DataTable
        Dim sBuilder As New StringBuilder

        '20121126 (U) 201209240086, 配合個資法規範
        sBuilder.AppendLine("select top 200 CRID, CRYYYYMM, AppID, CustomerID, CName, Status, AOID, AODepID")
        sBuilder.AppendLine("	, dbo.funGetAOrgDefine('1', AOID) [OfficerNm]")
        sBuilder.AppendLine("	, LastChgID, LastChgDate")
        sBuilder.AppendLine("  from CR_Profile with (nolock) ")
        sBuilder.AppendLine(" where 1 = 1")
        If ht.ContainsKey("Status") Then
            sBuilder.AppendLine("   and Status = " & Bsp.Utility.Quote(ht("Status")))
        End If

        'AppID
        If ht.ContainsKey("txtAppID") Then
            If ht("txtAppID").ToString() <> "" Then
                If ht("ddlAppID").ToString = "=" Then
                    sBuilder.AppendLine("   and AppID = " & Bsp.Utility.Quote(ht("txtAppID")))
                Else
                    sBuilder.AppendLine("   and AppID like " & Bsp.Utility.Quote(ht("txtAppID") & "%"))
                End If
            End If
        End If

        '部門, AO
        If ht.ContainsKey("ddlOrgan") Then
            If ht("ddlAO").ToString() <> "" Then
                sBuilder.AppendLine("   and AOID = " & Bsp.Utility.Quote(ht("ddlAO")))
            Else
                sBuilder.AppendLine("   and AOID in (Select UserID From SC_User with (nolock) Where DeptID in (" & Bsp.Utility.Quote(ht("ddlOrgan")).Replace(",", "','") & "))")
            End If
        End If

        'CustomerID
        If ht.ContainsKey("txtCustomerID") Then
            If ht("txtCustomerID").ToString() <> "" Then
                If ht("ddlCustomerID").ToString = "=" Then
                    sBuilder.AppendLine("   and CustomerID = " & Bsp.Utility.Quote(ht("txtCustomerID")))
                Else
                    sBuilder.AppendLine("   and CustomerID like " & Bsp.Utility.Quote(ht("txtCustomerID") & "%"))
                End If
            End If
        End If

        'CName
        If ht.ContainsKey("txtCName") Then
            If ht("txtCName").ToString() <> "" Then
                If ht("ddlCName").ToString = "=" Then
                    sBuilder.AppendLine("   and CName = " & Bsp.Utility.Quote(ht("txtCName")))
                Else
                    sBuilder.AppendLine("   and CName like " & Bsp.Utility.Quote(ht("txtCName") & "%"))
                End If
            End If
        End If
        sBuilder.AppendLine(" order by CRID desc")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, sBuilder.ToString()).Tables(0)
    End Function

    ''' <summary>
    ''' 取得單筆利費率優惠申請結案資料
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>2012.12.10 (A) Chung 201211010062, 為業務之需，擬新增企審系統中文版「結案退關卡」類別</remarks>
    Public Function GetRACase(ByVal Params As Hashtable) As DataTable
        Dim sBuilder As New StringBuilder

        sBuilder.AppendLine("Select top 200 RAID, CustomerID, AppID, dbo.funFindSCCommonItem('7Y',RAType) as RATypeNm, ApplyDate ")
        sBuilder.AppendLine("   , dbo.funGetAOrgDefine('1', AO) as OfficerNm, dbo.funGetCustomerName(CustomerID, AppID) as CName")
        sBuilder.AppendLine("From AL_RateOffer")
        sBuilder.AppendLine("Where Status = '9'")

        For Each s As String In Params.Keys
            Select Case s
                Case "ddlOrgan"
                    If Params("ddlAO").ToString() <> "" Then
                        sBuilder.AppendLine("And AO = " & Bsp.Utility.Quote(Params("ddlAO").ToString()))
                    Else
                        sBuilder.AppendLine("And AODeptID in ('" & Params(s).ToString().Replace(",", "','") & "')")
                    End If
                Case "txtAppID"
                    If Params(s).ToString() <> "" Then
                        If Params("ddlAppID").ToString() = "=" Then
                            sBuilder.AppendLine("And (AppID = " & Bsp.Utility.Quote(Params(s).ToString()) & " or RAID = " & Bsp.Utility.Quote(Params(s).ToString()) & ")")
                        Else
                            sBuilder.AppendLine("And (AppID like " & Bsp.Utility.Quote(Params(s).ToString() & "%") & " Or RAID like " & Bsp.Utility.Quote(Params(s).ToString() & "%") & ")")
                        End If
                    End If
                Case "txtCustomerID"
                    If Params(s).ToString() <> "" Then
                        If Params("ddlCustomerID").ToString() = "=" Then
                            sBuilder.AppendLine("And CustomerID = " & Bsp.Utility.Quote(Params(s).ToString()))
                        Else
                            sBuilder.AppendLine("And CustomerID like " & Bsp.Utility.Quote(Params(s).ToString() & "%"))
                        End If
                    End If
                Case "txtCName"
                    If Params(s).ToString() <> "" Then
                        If Params("ddlCName").ToString() = "=" Then
                            sBuilder.AppendLine("And Exists (Select * From BR_Customer Where AppID = AL_RateOffer.AppID And CustomerID = AL_RateOffer.CustomerID And CNameQ = " & Bsp.Utility.Quote(Params(s).ToString()) & ")")
                        Else
                            sBuilder.AppendLine("And Exists (Select * From BR_Customer Where AppID = AL_RateOffer.AppID And CustomerID = AL_RateOffer.CustomerID And CNameQ like " & Bsp.Utility.Quote(Params(s).ToString() & "%") & ")")
                        End If
                    End If
            End Select
        Next

        sBuilder.AppendLine("Order by RAID desc")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, sBuilder.ToString()).Tables(0)
    End Function

    '取的WF_FlowCase資料
    Public Function ExecuteBackStep(ByVal strFlowStepID As String, ByVal strAppID As String, ByVal strRemark As String) As Boolean
        Bsp.DB.ExecuteNonQuery(CommandType.StoredProcedure, "SP_WFA090", _
            New DbParameter() { _
            Bsp.DB.getDbParameter("@argFlowStepID", strFlowStepID), _
            Bsp.DB.getDbParameter("@argID", strAppID), _
            Bsp.DB.getDbParameter("@argUserID", UserProfile.UserID), _
            Bsp.DB.getDbParameter("@argRemark", strRemark)})

        Return True
    End Function

    Public Function DeleteCCPDF(ByVal AppID As String, ByVal CustomerID As String, Optional ByVal ReportNames As String = "") As Boolean
        Dim strPath As String
        Dim strCID As String
        Dim strSQL As String = "Select CID From BR_Customer Where AppID = " & Bsp.Utility.Quote(AppID) & " And CustomerID = " & Bsp.Utility.Quote(CustomerID)

        '刪除客戶基本資料表
        If ReportNames = "" OrElse ReportNames.IndexOf("客戶基本資料表") >= 0 Then
            Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL).Tables(0)
                If dt.Rows.Count > 0 Then
                    strCID = dt.Rows(0).Item("CID").ToString()
                    strPath = System.IO.Path.Combine(MySettings.ALBackupPath, _
                                String.Format("20{0}\{1}\BIR\{2}.pdf", strCID.Substring(2, 2), strCID.Substring(4, 2), strCID))

                    If System.IO.File.Exists(strPath) Then System.IO.File.Delete(strPath)
                End If
            End Using
        End If

        '刪除徵信照會報告
        If ReportNames = "" OrElse ReportNames.IndexOf("徵信照會報告") >= 0 Then
            strPath = System.IO.Path.Combine(MySettings.ALBackupPath, _
                String.Format("20{0}\{1}\CC\{2}_{3}.pdf", AppID.Substring(2, 2), AppID.Substring(4, 2), AppID, CustomerID))
            If System.IO.File.Exists(strPath) Then System.IO.File.Delete(strPath)
        End If

        '刪除個人基本資料表
        If ReportNames = "" OrElse ReportNames.IndexOf("個人基本資料表") >= 0 Then
            strPath = System.IO.Path.Combine(MySettings.ALBackupPath, _
                String.Format("20{0}\{1}\CCB\{2}_{3}.pdf", AppID.Substring(2, 2), AppID.Substring(4, 2), AppID, CustomerID))
            If System.IO.File.Exists(strPath) Then System.IO.File.Delete(strPath)
        End If

        '刪除董監事清單
        If ReportNames = "" OrElse ReportNames.IndexOf("董監事清單") >= 0 Then
            strPath = System.IO.Path.Combine(MySettings.ALBackupPath, _
                String.Format("20{0}\{1}\DL\{2}_{3}.pdf", AppID.Substring(2, 2), AppID.Substring(4, 2), AppID, CustomerID))
            If System.IO.File.Exists(strPath) Then System.IO.File.Delete(strPath)
        End If

        '刪除行庫餘額明細表
        If ReportNames = "" OrElse ReportNames.IndexOf("行庫餘額明細表") >= 0 Then
            strPath = System.IO.Path.Combine(MySettings.ALBackupPath, _
                String.Format("20{0}\{1}\BD\{2}_{3}.pdf", AppID.Substring(2, 2), AppID.Substring(4, 2), AppID, CustomerID))
            If System.IO.File.Exists(strPath) Then System.IO.File.Delete(strPath)
        End If

        '刪除行庫餘額比較表
        If ReportNames = "" OrElse ReportNames.IndexOf("行庫餘額比較表") >= 0 Then
            strPath = System.IO.Path.Combine(MySettings.ALBackupPath, _
                String.Format("20{0}\{1}\BC\{2}_{3}.pdf", AppID.Substring(2, 2), AppID.Substring(4, 2), AppID, CustomerID))
            If System.IO.File.Exists(strPath) Then System.IO.File.Delete(strPath)
        End If

        '刪除徵信調查報告(信用分析)
        If ReportNames = "" OrElse ReportNames.IndexOf("徵信調查報告") >= 0 Then
            '先刪除集團徵信調查報告
            strPath = System.IO.Path.Combine(MySettings.ALBackupPath, _
                String.Format("20{0}\{1}\CA\{2}.pdf", AppID.Substring(2, 2), AppID.Substring(4, 2), AppID))
            If System.IO.File.Exists(strPath) Then System.IO.File.Delete(strPath)

            strSQL = "Select CAID From CA_Profile Where AppID = " & Bsp.Utility.Quote(AppID) & " And CustomerID = " & Bsp.Utility.Quote(CustomerID) & " And Status <> '0'"
            Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL).Tables(0)
                If dt.Rows.Count > 0 Then
                    Dim strCAID As String = dt.Rows(0).Item("CAID").ToString()

                    strPath = System.IO.Path.Combine(MySettings.ALBackupPath, _
                        String.Format("20{0}\{1}\CA\{2}.pdf", strCAID.Substring(2, 2), strCAID.Substring(4, 2), strCAID))
                    If System.IO.File.Exists(strPath) Then System.IO.File.Delete(strPath)
                End If
            End Using
        End If

        '刪除財報報表(含相關財報)
        If ReportNames = "" OrElse ReportNames.IndexOf("財務報表") >= 0 Then
            strSQL = "select SPID from SP_Profile "
            strSQL += " where AppID = " & Bsp.Utility.Quote(AppID) & " and CustomerID = " & Bsp.Utility.Quote(CustomerID) & " and PartyKind = '' "
            strSQL += "union "
            strSQL += "select a.SPID from SP_Profile a "
            strSQL += "  inner join SP_Sponsor b on b.SPID = a.SPID and b.SpCustomerID = " & Bsp.Utility.Quote(CustomerID)
            strSQL += " where a.AppID = " & Bsp.Utility.Quote(AppID) & " and a.PartyKind = '4' "
            Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL).Tables(0)
                If dt.Rows.Count > 0 Then
                    '' 20130325 (U) 104751 201303060038 徵信報告財務報表SPREAD檔欄位修正
                    For Each objRow As DataRow In dt.Rows
                        Dim strSPID As String = objRow("SPID").ToString()

                        strPath = System.IO.Path.Combine(MySettings.ALBackupPath, _
                            String.Format("20{0}\{1}\SP\{2}.pdf", AppID.Substring(2, 2), AppID.Substring(4, 2), strSPID))
                        If System.IO.File.Exists(strPath) Then System.IO.File.Delete(strPath)

                        strPath = System.IO.Path.Combine(MySettings.ALBackupPath, _
                            String.Format("20{0}\{1}\SP\{2}_IFRS.pdf", AppID.Substring(2, 2), AppID.Substring(4, 2), strSPID))
                        If System.IO.File.Exists(strPath) Then System.IO.File.Delete(strPath)
                    Next
                End If
            End Using
        End If

        Return True
    End Function

#End Region


#Region "WF_FlowCase相關"
    ''' <summary>
    ''' 取的流程主檔資料
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowCaseID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFlowCase(ByVal FlowID As String, ByVal FlowCaseID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select * From WF_FlowCase with (nolock)")
        strSQL.AppendLine("Where FlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("And FlowCaseID = " & Bsp.Utility.Quote(FlowCaseID))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    ''' <summary>
    ''' 取的流程主檔資料
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowCaseID"></param>
    ''' <param name="tran"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFlowCase(ByVal FlowID As String, ByVal FlowCaseID As String, ByVal tran As DbTransaction) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select * From WF_FlowCase")
        strSQL.AppendLine("Where FlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("And FlowCaseID = " & Bsp.Utility.Quote(FlowCaseID))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), tran).Tables(0)
    End Function

    ''' <summary>
    ''' 取得流程主檔資料-by PaperID
    ''' </summary>
    ''' <param name="PaperID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFlowCase(ByVal PaperID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select * From WF_FlowCase Where PaperID = " & Bsp.Utility.Quote(PaperID))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    Public Function GetToDoListbyPaperID(ByVal PaperID As String, ByVal UserID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select * From WF_FlowToDoList Where PaperID = " & Bsp.Utility.Quote(PaperID))
        strSQL.AppendLine("And AssignTo = " & Bsp.Utility.Quote(UserID))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    ''' <summary>
    ''' 取得流程參數
    ''' </summary>
    ''' <param name="Params"></param>
    ''' <param name="PaperID"></param>
    ''' <remarks></remarks>
    Public Sub AddFlowKey(ByRef Params() As Object, ByVal PaperID As String)
        Dim intLength As Integer = Params.GetLength(0)
        Array.Resize(Params, intLength + 3)

        Using dtToDoList As DataTable = GetToDoListbyPaperID(PaperID, UserProfile.UserID)
            If dtToDoList.Rows.Count > 0 Then
                Params(intLength) = "FlowID=" & dtToDoList.Rows(0).Item("FlowID").ToString()
                Params(intLength + 1) = "FlowCaseID=" & dtToDoList.Rows(0).Item("FlowCaseID").ToString()
                Params(intLength + 2) = "FlowLogID=" & dtToDoList.Rows(0).Item("FlowLogID").ToString()
            Else
                Using dt As DataTable = GetFlowCase(PaperID)
                    If dt.Rows.Count > 0 Then
                        Params(intLength) = "FlowID=" & dt.Rows(0).Item("FlowID").ToString()
                        Params(intLength + 1) = "FlowCaseID=" & dt.Rows(0).Item("FlowCaseID").ToString()
                        Params(intLength + 2) = "FlowLogID=" & dt.Rows(0).Item("LastLogID").ToString()
                    End If
                End Using
            End If
        End Using
    End Sub

    ''' <summary>
    ''' 取得流程主檔資料-by PaperID
    ''' </summary>
    ''' <param name="PaperID"></param>
    ''' <param name="tran"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFlowCase(ByVal PaperID As String, ByVal tran As DbTransaction) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select * From WF_FlowCase Where PaperID = " & Bsp.Utility.Quote(PaperID))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), tran).Tables(0)
    End Function

    Public Function GetPaperID(ByVal FlowID As String, ByVal FlowCaseID As String) As String
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select PaperID From WF_FlowCase With (nolock)")
        strSQL.AppendLine("Where FlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("And FlowCaseID = " & Bsp.Utility.Quote(FlowCaseID))

        Return Bsp.Utility.IsStringNull(Bsp.DB.ExecuteScalar(strSQL.ToString()))
    End Function
#End Region

#Region "WF_FlowToDoList相關"
    ''' <summary>
    ''' 查詢ToDoList中相關欄位資訊，利用FlowID,FlowCaseID,FlowLogID
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowCaseID"></param>
    ''' <param name="FlowLogID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFlowToDoList(ByVal FlowID As String, ByVal FlowCaseID As String, ByVal FlowLogID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("SELECT * FROM WF_FlowToDoList with (nolock) ")
        strSQL.AppendLine("Where FlowID = @FlowID")
        strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
        strSQL.AppendLine("And FlowLogID = @FlowLogID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), New DbParameter() { _
                Bsp.DB.getDbParameter("@FlowID", FlowID), _
                Bsp.DB.getDbParameter("@FlowCaseID", FlowCaseID), _
                Bsp.DB.getDbParameter("@FlowLogID", FlowLogID)}).Tables(0)
    End Function

    ''' <summary>
    ''' 查詢ToDoList中相關欄位資訊，利用FlowID,FlowCaseID,FlowLogID
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowCaseID"></param>
    ''' <param name="FlowLogID"></param>
    ''' <param name="tran"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFlowToDoList(ByVal FlowID As String, ByVal FlowCaseID As String, ByVal FlowLogID As String, ByVal tran As DbTransaction) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("SELECT * FROM WF_FlowToDoList ")
        strSQL.AppendLine("Where FlowID = @FlowID")
        strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
        strSQL.AppendLine("And FlowLogID = @FlowLogID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), New DbParameter() { _
                Bsp.DB.getDbParameter("@FlowID", FlowID), _
                Bsp.DB.getDbParameter("@FlowCaseID", FlowCaseID), _
                Bsp.DB.getDbParameter("@FlowLogID", FlowLogID)}, tran).Tables(0)
    End Function
#End Region

#Region "WF_FlowFullLog相關"
    ''' <summary>
    ''' 取得FlowFullLog的資料
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowCaseID"></param>
    ''' <param name="FlowBatNo"></param>
    ''' <param name="ActionFlag">是否只取出已處理的流程關卡 True:是 False:否</param>
    ''' <returns></returns>
    ''' <remarks>供WFA020流程處理使用,為取出上一關流程意見</remarks>
    Public Function GetFlowFullLog(ByVal FlowID As String, ByVal FlowCaseID As String, ByVal FlowBatNo As String, Optional ByVal ActionFlag As Boolean = False) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select FlowStepID + ' - ' + FlowStepDesc + '(' +  Case FlowLogStatus When 'Open' Then '在途' When 'Close' Then Case Isnull(FlowStepAction, '') When '' Then '被完成' Else '完成' End End + ')' as FlowStepDesc, ")
        strSQL.AppendLine("	FlowStepAction, FlowStepOpinion, ToUser + '-' + ToUserName AS ToUserName, ")
        strSQL.AppendLine("	Case IsProxy When 'Y' Then '(代理:' + AssignTo + '-' + AssignToName + ')' Else '' End as ProxyStr, ")
        strSQL.AppendLine("	Case Convert(varchar(8), UpdDate, 112) When '19000101' Then NULL Else UpdDate END AS UpdDate,")
        strSQL.AppendLine("	Isnull(LogRemark, '') as LogRemark")
        strSQL.AppendLine("From WF_FlowFullLog ")
        strSQL.AppendLine("Where FlowID = @FlowID")
        strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
        strSQL.AppendLine("And FlowLogBatNo = @FlowBatNo")

        If ActionFlag Then
            strSQL.AppendLine("And Isnull(FlowStepAction, '') <> ''")
        End If
        strSQL.AppendLine("Order by UpdDate desc")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), New DbParameter() { _
            Bsp.DB.getDbParameter("@FlowID", FlowID), _
            Bsp.DB.getDbParameter("@FlowCaseID", FlowCaseID), _
            Bsp.DB.getDbParameter("@FlowBatNo", FlowBatNo)}).Tables(0)
    End Function

    ''' <summary>
    ''' 取得指定的FlowFullLog
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowCaseID"></param>
    ''' <param name="FlowLogID"></param>
    ''' <param name="CondStr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFlowFullLog(ByVal FlowID As String, ByVal FlowCaseID As String, ByVal FlowLogID As String, ByVal CondStr As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select * From WF_FlowFullLog with (nolock)")
        strSQL.AppendLine("Where FlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("And FlowCaseID = " & Bsp.Utility.Quote(FlowCaseID))
        strSQL.AppendLine("And FlowLogID = " & Bsp.Utility.Quote(FlowLogID))

        If CondStr <> "" Then
            strSQL.AppendLine(CondStr)
        End If

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    ''' <summary>
    ''' 取得指定的FlowFullLog
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowCaseID"></param>
    ''' <param name="FlowLogID"></param>
    ''' <param name="CondStr"></param>
    ''' <param name="tran"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFlowFullLog(ByVal FlowID As String, ByVal FlowCaseID As String, ByVal FlowLogID As String, ByVal CondStr As String, ByVal tran As DbTransaction) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select * From WF_FlowFullLog")
        strSQL.AppendLine("Where FlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("And FlowCaseID = " & Bsp.Utility.Quote(FlowCaseID))
        strSQL.AppendLine("And FlowLogID = " & Bsp.Utility.Quote(FlowLogID))

        If CondStr <> "" Then
            strSQL.AppendLine(CondStr)
        End If

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), tran).Tables(0)
    End Function

    Public Function GetFlowFullLog(ByVal FlowID As String, ByVal FlowCaseID As String, ByVal FlowLogBatNo As Integer, ByVal CondStr As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select * From WF_FlowFullLog with (nolock)")
        strSQL.AppendLine("Where FlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("And FlowCaseID = " & Bsp.Utility.Quote(FlowCaseID))
        strSQL.AppendLine("And FlowLogBatNo = " & Bsp.Utility.Quote(FlowLogBatNo.ToString()))

        If CondStr <> "" Then
            strSQL.AppendLine(CondStr)
        End If

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function

    ''' <summary>
    ''' 取得最近一個關卡的人員
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowCaseID"></param>
    ''' <param name="CurrentFlowStep"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPreviousFlowStepUser(ByVal FlowID As String, ByVal FlowCaseID As String, ByVal CurrentFlowStep As String) As String
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select Top 1 ToUser From WF_FlowFullLog")
        strSQL.AppendLine("Where FlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("And FlowCaseID = " & Bsp.Utility.Quote(FlowCaseID))
        strSQL.AppendLine("And FlowStepID <> " & Bsp.Utility.Quote(CurrentFlowStep))
        strSQL.AppendLine("Order by UpdDate Desc")

        Return Bsp.Utility.IsStringNull(Bsp.DB.ExecuteScalar(strSQL.ToString()))
    End Function

    ''' <summary>
    ''' 取得指定某一關卡的最後處理人員
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowCaseID"></param>
    ''' <param name="FlowStepID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFlowStepUser(ByVal FlowID As String, ByVal FlowCaseID As String, ByVal FlowStepID As String) As String
        Dim strSQL As New StringBuilder()
        Dim strFlowStepID As String = FlowStepID

        strFlowStepID = "'" & strFlowStepID.Replace(",", "','") & "'"

        strSQL.AppendLine("Select Top 1 ToUser From WF_FlowFullLog")
        strSQL.AppendLine("Where FlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("And FlowCaseID = " & Bsp.Utility.Quote(FlowCaseID))
        strSQL.AppendLine("And FlowStepID in (" & strFlowStepID & ")")
        strSQL.AppendLine("Order by UpdDate Desc")

        Return Bsp.Utility.IsStringNull(Bsp.DB.ExecuteScalar(strSQL.ToString()))
    End Function
#End Region

#Region "WF_Phrase相關"
    ''' <summary>
    ''' 取得所在關卡片語
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowStepID"></param>
    ''' <param name="PhraseUser"></param>
    ''' <param name="CondStr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetStepPhrase(ByVal FlowID As String, ByVal FlowStepID As String, ByVal PhraseUser As String, Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select FlowID, FlowStepID, UserID, SeqNo, FlowPhrase, ")
        strSQL.AppendLine(" Case When Len(FlowPhrase) > 30 Then Left(FlowPhrase, 30) + '...' Else FlowPhrase End LFlowPhrase")
        strSQL.AppendLine("From WF_Phrase With (nolock)")
        strSQL.AppendLine("Where FlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("And FlowStepID = " & Bsp.Utility.Quote(FlowStepID))
        strSQL.AppendLine("And UserID in ('', " & Bsp.Utility.Quote(PhraseUser) & ")")
        If CondStr <> "" Then strSQL.AppendLine(CondStr)
        strSQL.AppendLine("Order by FlowID, FlowStepID, UserID, SeqNo")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function
#End Region

#Region "流程送出後處理呼叫"
    Private Function UpdateSP_ProfileStatus(ByVal SPID As String, ByVal Status As String, ByVal Transaction As DbTransaction, Optional ByVal ValidFlag As String = "") As Integer
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Update SP_Profile Set")
        strSQL.AppendFormat(" Status = {0}," & vbCrLf, Bsp.Utility.Quote(Status))
        If ValidFlag.Trim.Length = 1 Then strSQL.AppendFormat("   ValidFlag = {0}," & vbCrLf, Bsp.Utility.Quote(ValidFlag))
        strSQL.AppendLine(" LastChgDate = Getdate(),")
        strSQL.AppendFormat("   LastChgID = {0}" & vbCrLf, UserProfile.ActUserID)
        strSQL.AppendLine("Where SPID = " & Bsp.Utility.Quote(SPID))

        Return Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), Transaction)
    End Function
#End Region

#Region "流程退關卡處理"
    ''' <summary>
    ''' 流程退關卡處理
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowCaseID"></param>
    ''' <param name="Remark"></param>
    ''' <param name="Transaction"></param>
    ''' <returns></returns>
    ''' <remarks>之後若有新的流程Case, 需要在此加入該流程退關卡後的主檔狀態</remarks>
    Public Function FlowBackOneStep(ByVal FlowID As String, ByVal FlowCaseID As String, ByVal Remark As String, ByVal Transaction As DbTransaction) As Boolean
        Dim strSQL As New StringBuilder()

        Bsp.DB.ExecuteNonQuery(CommandType.StoredProcedure, "SP_FlowBackOneStep", New DbParameter() { _
                              Bsp.DB.getDbParameter("@FlowID", FlowID), _
                              Bsp.DB.getDbParameter("@FlowCaseID", FlowCaseID), _
                              Bsp.DB.getDbParameter("@FlowLogRemark", "")}, Transaction)

        Using dt As DataTable = GetFlowCase(FlowID, FlowCaseID, Transaction)
            If dt.Rows.Count > 0 Then
                Select Case dt.Rows(0).Item("FlowCurrStepID").ToString()
                    Case "SP02"
                        UpdateSP_ProfileStatus(dt.Rows(0).Item("PaperID").ToString(), "2", Transaction, "0")
                    Case "SP01", "SP03", "SP04", "SP05"
                        UpdateSP_ProfileStatus(dt.Rows(0).Item("PaperID").ToString(), "3", Transaction, "0")
                End Select
            End If
        End Using
    End Function

#End Region

#Region "CheckButtonDisplay : 判斷流程按鈕是否出現的邏輯"
    ''' <summary>
    ''' 檢查流程按鈕是否顯示
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowVer"></param>
    ''' <param name="FlowStepID"></param>
    ''' <param name="ButtonName"></param>
    ''' <param name="ReturnMessage"></param>
    ''' <param name="Params"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckButtonDisplay(ByVal FlowID As String, ByVal FlowVer As String, ByVal FlowStepID As String, ByVal ButtonName As String, ByRef ReturnMessage As String, ByVal ParamArray Params As Object()) As Boolean
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim AppID As String = ""
        Dim CMNO As String = ""
        Dim FlowCaseID As String = ""
        Dim FlowLogBatNo As String = ""
        Dim FlowLogID As String = ""
        Dim CCID As String = ""

        For Each s As String In ht.Keys
            Select Case s
                Case "AppID"
                    AppID = ht(s).ToString()
                Case "CMNO"
                    CMNO = ht(s).ToString()
                Case "FlowCaseID"
                    FlowCaseID = ht(s).ToString()
                Case "FlowLogBatNo"
                    FlowLogBatNo = ht(s).ToString()
                Case "FlowLogID"
                    FlowLogID = ht(s).ToString()
                Case "CCID"
                    CCID = ht(s).ToString()
            End Select
        Next

        Select Case FlowID & "-" & FlowVer & "-" & FlowStepID & "-" & ButtonName
            Case "AP-1-S001-簽核主管审核"
                ReturnMessage = CheckS001Display(AppID, FlowCaseID)
                If ReturnMessage <> "" Then Return False
            Case "AP-1-S001-销案"
                If CheckFlowRelation("AP", FlowCaseID, "Open") Then Return False
            Case "AP-1-S002-同意送审查窗口"
                Using dt As DataTable = getSC_LoanChecker_In(FlowCaseID, "S001", "").Tables(0)
                    If dt.Rows.Count > 0 Then Return False
                End Using
            Case "AP-1-S002-同意"
                Using dt As DataTable = getSC_LoanChecker_In(FlowCaseID, "S001", "").Tables(0)
                    If dt.Rows.Count = 0 Then Return False
                End Using
            Case "AP-1-S002-转呈"
                Using dt As DataTable = getSC_LoanChecker_In(FlowCaseID, "S001", "").Tables(0)
                    If dt.Rows.Count = 0 Then Return False
                End Using
            Case "AP-1-S003-总行簽核主管", "AP-1-S003-转呈/分案", "AP-1-S003-核贷书结案"
                If CheckFlowRelation("AP", FlowCaseID, "Open") Then
                    ReturnMessage = "[提示说明]:目前尚有查核或征信子流程未完成，尚不得送核贷作业或销案動作!请查询流程紀錄!"
                    Return False
                End If
            Case "AP-1-S004-同意"
                Using dt As DataTable = getSC_LoanChecker_In(FlowCaseID, "S003", "").Tables(0)
                    If dt.Rows.Count = 0 Then Return False
                End Using
            Case "AP-1-S004-不同意"
                Using dt As DataTable = getSC_LoanChecker_In(FlowCaseID, "S003", "").Tables(0)
                    If dt.Rows.Count = 0 Then Return False
                End Using
            Case "AP-1-S004-转呈"
                Using dt As DataTable = getSC_LoanChecker_In(FlowCaseID, "S003", "").Tables(0)
                    If dt.Rows.Count = 0 Then Return False
                End Using
            Case "AP-1-S004-同意送审查作业"
                Using dt As DataTable = getSC_LoanChecker_In(FlowCaseID, "S003", "").Tables(0)
                    If dt.Rows.Count > 0 Then Return False
                End Using
            Case "AP-1-S004-不同意并送审查作业"
                Using dt As DataTable = getSC_LoanChecker_In(FlowCaseID, "S003", "").Tables(0)
                    If dt.Rows.Count > 0 Then Return False
                End Using
            Case "AP-1-S005-退回审查作业"
                If GetFlowStepUser(FlowID, FlowCaseID, "S006") = "" Then Return False
            Case "AP-1-S005-不核准或销案", "AP-1-S005-核准"
                If CheckFlowRelation("AP", FlowCaseID, "Open") Then
                    ReturnMessage = "[提示说明]:目前尚有查核或征信子流程未完成，尚不得送核贷作业或销案動作!请查询流程紀錄!"
                    Return False
                End If
            Case "AP-1-S006-送簽核主管", "AP-1-S006-核贷书结案"
                If CheckFlowRelation("AP", FlowCaseID, "Open") Then
                    ReturnMessage = "[提示说明]:目前尚有查核或征信子流程未完成，尚不得送核贷作业或销案動作!请查询流程紀錄!"
                    Return False
                End If
            Case "AP-1-S006-送授信审查委员会"
                If CheckFlowRelation("AP", FlowCaseID, "Open") Then
                    ReturnMessage = "[提示说明]:目前尚有查核或征信子流程未完成，尚不得送核贷作业或销案動作!请查询流程紀錄!"
                    Return False
                End If

                '檢查是否有送線審或書審
                Using dt As DataTable = GetAL_Profile(AppID, "DirectType")
                    If Not Bsp.Utility.InStr(Bsp.Utility.IsStringNull(dt.Rows(0).Item(0)), "1", "2") Then Return False
                End Using
            Case "AP-1-S007-审查窗口"
                Using dt As DataTable = GetFlowFullLog(FlowID, FlowCaseID, CInt((Convert.ToInt32(FlowLogBatNo) - 1).ToString()), "And FlowStepID = 'S006'")
                    If dt.Rows.Count = 0 Then Return False
                End Using
            Case "AP-1-S007-审查窗口."
                Using dt As DataTable = GetFlowFullLog(FlowID, FlowCaseID, CInt((Convert.ToInt32(FlowLogBatNo) - 1).ToString()), "And FlowStepID = 'S003'")
                    If dt.Rows.Count = 0 Then Return False
                End Using
            Case "AP-1-S010-同意送審查維護", "CoCredit-1-S010-不同意并送审查维护", "CoCredit-1-S010-有条件同意"
                Using dt As DataTable = GetAL_Profile(AppID, "DirectType")
                    If Bsp.Utility.IsStringNull(dt.Rows(0).Item(0)) <> "2" Then Return False
                End Using
            Case "AP-1-S010-同意送書面審議"
                Using dt As DataTable = GetAL_Profile(AppID, "DirectType")
                    If Bsp.Utility.IsStringNull(dt.Rows(0).Item(0)) <> "1" Then Return False
                End Using
            Case "AP-1-S011-簽核主管", "AP-1-S011-核贷书结案"
                If CheckFlowRelation("AP", FlowCaseID, "Open") Then
                    ReturnMessage = "[提示说明]:目前尚有查核或征信子流程未完成，尚不得送核贷作业或销案動作!请查询流程紀錄!"
                    Return False
                End If
            Case "AP-1-S012-同意"
                Using dt As DataTable = getSC_LoanChecker_In(FlowCaseID, "S011", "").Tables(0)
                    If dt.Rows.Count = 0 Then Return False
                End Using
            Case "AP-1-S012-同意送审查编制"
                Using dt As DataTable = getSC_LoanChecker_In(FlowCaseID, "S011", "").Tables(0)
                    If dt.Rows.Count > 0 Then Return False
                End Using
            Case "AP-S013-核贷书结案"
                If CheckFlowRelation("AP", FlowCaseID, "Open") Then
                    ReturnMessage = "[提示说明]:目前尚有查核或征信子流程未完成，尚不得送核贷作业或销案動作!请查询流程紀錄!"
                    Return False
                End If
            Case "CC-1-CC04-征信复核", "CC-1-CC05-徵信結案"
                'ReturnMessage = checkSP_FinRepInfo(CCID)

            Case "CM-1-CM01-参阅"
                If GetCMBoss(CMNO) = "" Then
                    ReturnMessage = "【提示说明】：未设定审核人员，请至访谈纪录编辑内指定审核人员！"
                    Return False
                End If
                If GetCMNextCheckerID(CMNO) = "" Then
                    Return False
                End If
            Case "CM-1-CM01-審核"
                If GetCMNextCheckerID(CMNO) = "" Then
                    If GetCMBoss(CMNO) = "" Then
                        ReturnMessage = "【提示说明】：未设定审核人员，请至访谈纪录编辑内指定审核人员！"
                        Return False
                    End If
                    If CheckCM01(CMNO) = False Then
                        ReturnMessage = "【提示说明】：请至访谈纪录维护中勾选【业务机会】，若暂无机会请勾选【客户暂无需求】！"
                        Return False
                    End If
                Else
                    Return False
                End If
            Case "CM-1-CM02-審核"
                If GetCMBoss(CMNO) = "" Then
                    ReturnMessage = "【提示说明】：未设定审核人员，请退回维护人员指定审核人员！"
                    Return False
                End If
        End Select
        Return True
    End Function

    Private Function GetCMBoss(ByVal CMNO As String) As String
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select Top 1 SignUser From CM_Verify")
        strSQL.AppendLine("Where CMNO = " & Bsp.Utility.Quote(CMNO))
        strSQL.AppendLine("And SignStatus in ('N','R')")
        strSQL.AppendLine("And SignType = 'A'")

        Dim strSignUser As String = Bsp.DB.ExecuteScalar(strSQL.ToString())
        If strSignUser Is Nothing Then strSignUser = ""

        Return strSignUser
    End Function

    Private Function GetCMNextCheckerID(ByVal CMNO As String) As String
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select Top 1 SignUser From CM_Verify")
        strSQL.AppendLine("Where CMNO = " & Bsp.Utility.Quote(CMNO))
        strSQL.AppendLine("And SignStatus = 'N'")
        strSQL.AppendLine("And SignType = 'C'")
        strSQL.AppendLine("And (Isnull(EmailFg, '') = '' or EmailFg = 'N')")
        strSQL.AppendLine("Order by SeqNo")

        Dim strSignUser As String = Bsp.DB.ExecuteScalar(strSQL.ToString())
        If strSignUser Is Nothing Then strSignUser = ""

        Return strSignUser
    End Function

    '沒有勾跨售機會也沒有勾客戶暫無需求,要出警示
    Private Function CheckCM01(ByVal CMNO As String) As Boolean
        Dim strSQL As New StringBuilder()
        Dim strCnt As String
        Dim strCnt2 As String

        strSQL.AppendLine("Select count(*) From CM_Profile a inner join CM_CrossSellingM b on a.CMNO=b.CMNO ")
        strSQL.AppendLine("Where a.CMNO=" + Bsp.Utility.Quote(CMNO) + " and a.NoCrossSelling='' ")
        strCnt = Bsp.DB.ExecuteScalar(strSQL.ToString)

        strSQL.Remove(0, strSQL.Length)
        strSQL.AppendLine("Select count(*) From CM_Profile  ")
        strSQL.AppendLine("Where CMNO=" + Bsp.Utility.Quote(CMNO) + " and NoCrossSelling='Y' ")
        strCnt2 = Bsp.DB.ExecuteScalar(strSQL.ToString)

        If strCnt = "0" AndAlso strCnt2 = "0" Then
            Return False
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' 檢查財報數字邏輯是否正確
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckSPRules(ByVal CCID As String) As Boolean
        Dim strSQL As New StringBuilder()
        Dim strSQL_SP As New StringBuilder()

        strSQL.AppendLine("Select Define, Note, RsvCol1")
        strSQL.AppendLine("From SC_Common")
        strSQL.AppendLine("Where Type = '225'")
        strSQL.AppendLine("And ValidFlag = '1'")
        strSQL.AppendLine("And OrderSeq = '1'")

        strSQL_SP.AppendLine("Select distinct a.SPID, b.Report_Kind")
        strSQL_SP.AppendLine("From SP_FinRepInfo a inner join SP_FinRepData b on a.SPID = b.SPID ")
        strSQL_SP.AppendLine("Where Exists (Select * From CC_Profile Where AppID = a.AppID And CustomerID = a.CustomerID and CCID = @CCID)")
        strSQL_SP.AppendLine("And b.Report_Kind = @Report_Kind")

        Using dtCommon As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
            For Each r As DataRow In dtCommon.Rows

                Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL_SP.ToString(), New DbParameter() { _
                                                              Bsp.DB.getDbParameter("@CCID", CCID), _
                                                              Bsp.DB.getDbParameter("@Report_Kind", r.Item("RsvCol1").ToString())}).Tables(0)

                End Using
            Next
        End Using
    End Function

    ''' <summary>
    ''' S001進入前檢查
    ''' </summary>
    ''' <param name="AppID"></param>
    ''' <param name="FlowCaseID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckS001Display(ByVal AppID As String, ByVal FlowCaseID As String) As String
        Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.StoredProcedure, "SP_CheckBeforeS001", New DbParameter() { _
                                                      Bsp.DB.getDbParameter("@argAppID", AppID), _
                                                      Bsp.DB.getDbParameter("@argFlowCaseID", FlowCaseID)}).Tables(0)
            If dt.Rows.Count > 0 Then
                If dt.Rows(0).Item(0).ToString() <> "" Then
                    Return dt.Rows(0).Item(1).ToString()
                End If
            End If
        End Using
        Return ""
    End Function

    ''' <summary>
    ''' CC04進入前檢查
    ''' </summary>
    ''' <param name="CCID"></param>
    ''' <param name="FlowCaseID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckCC04Display(ByVal CCID As String, ByVal FlowCaseID As String) As String
        Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.StoredProcedure, "SP_CheckBeforeCC04", New DbParameter() { _
                                                      Bsp.DB.getDbParameter("@argAppID", CCID)}).Tables(0)
            If dt.Rows.Count > 0 Then
                If dt.Rows(0).Item(0).ToString() <> "" Then
                    Return dt.Rows(0).Item(1).ToString()
                End If
            End If
        End Using
        Return ""
    End Function

    ''' <summary>
    ''' 檢查是否有符合條件的子流程
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowCaseID"></param>
    ''' <param name="FlowCaseStatus">Open/Close</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckFlowRelation(ByVal FlowID As String, ByVal FlowCaseID As String, ByVal FlowCaseStatus As String) As Boolean
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select Count(*) From WF_FlowRelation")
        strSQL.AppendLine("Where ParentFlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("And ParentFlowCaseID = " & Bsp.Utility.Quote(FlowCaseID))
        If FlowCaseStatus <> "" Then
            strSQL.AppendLine("And FlowCaseStatus = " & Bsp.Utility.Quote(FlowCaseStatus))
        End If

        If Convert.ToInt32(Bsp.DB.ExecuteScalar(strSQL.ToString())) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
#End Region

#Region "調整流程FlowShowValue"
    ''' <summary>
    ''' 調整流程FlowShowValue
    ''' </summary>
    ''' <param name="PaperID">主要Key值</param>
    ''' <param name="Tran"></param>
    ''' <param name="aryShowValue"></param>
    ''' <remarks></remarks>
    Public Sub UpdateFlowShowValue(ByVal FlowID As String, ByVal PaperID As String, ByVal Tran As DbTransaction, ByVal ParamArray aryShowValue As String())
        Dim strSQL As New StringBuilder()
        Dim strShowValue As String = PaperID

        If aryShowValue.Length > 0 Then
            For intLoop As Integer = 0 To aryShowValue.Length - 1
                strShowValue &= "|" & aryShowValue(intLoop).ToString()
            Next
        End If
        Try
            strSQL.AppendLine("Update WF_FlowCase Set FlowShowValue = N" & Bsp.Utility.Quote(strShowValue))
            strSQL.AppendLine("WHERE PaperID = " & Bsp.Utility.Quote(PaperID))
            strSQL.AppendLine("And FlowID = " & Bsp.Utility.Quote(FlowID) & ";")
            strSQL.AppendLine("Update WF_FlowToDoList Set FlowShowValue = N" & Bsp.Utility.Quote(strShowValue))
            strSQL.AppendLine("WHERE PaperID = " & Bsp.Utility.Quote(PaperID))
            strSQL.AppendLine("And FlowID = " & Bsp.Utility.Quote(FlowID) & ";")

            Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), Tran)
        Catch ex As Exception
            Throw
        End Try
    End Sub
#End Region

#Region "取消案件權限 - For AP & CM使用"
    Public Sub CancelAuthFlow(ByVal FlowID As String, ByVal PaperID As String, ByVal AuthDelAssignToStr As String, ByVal OpinionStr As String, ByVal Tran As DbTransaction)
        Dim strCondStr As String = ""

        Select Case FlowID
            Case "AP"
                strCondStr = " And FlowLogStatus = 'Open' AND FlowStepID in ('S001','S007') "
            Case "CM"
                strCondStr = " And FlowLogStatus = 'Open' AND FlowStepID = 'CM01' "
        End Select
        Try
            '檢查案件是否為S001
            Using dt As DataTable = FindFlowKey(PaperID, FlowID, strCondStr, Tran)
                If dt.Rows.Count > 0 Then
                    'Dim FlowID As String = dt.Rows(0).Item("FlowID").ToString()
                    Dim FlowCaseID As String = dt.Rows(0).Item("FlowCaseID").ToString()
                    Dim FlowLogBatNo As String = dt.Rows(0).Item("FlowLogBatNo").ToString()
                    '新增log
                    AuthDelAssignToStr = ToDelAuthDisUser(FlowID, FlowCaseID, FlowLogBatNo, AuthDelAssignToStr, ";", Tran)
                    If AuthDelAssignToStr.Trim <> "" Then
                        '將刪除的人員FullLog更新
                        UpdDelAuthFlowFullLog(FlowID, FlowCaseID, FlowLogBatNo, AuthDelAssignToStr, "取消授權", UserProfile.ActUserName & "取消授權。" & OpinionStr, UserProfile.ActUserID, "Close", Tran)
                        '執行更新ToDolist
                        PutFlowToDoList(FlowID, FlowCaseID, Tran)

                        '檢查是否刪除後，尚有User，否則回應錯誤
                        If Not CheckFlowLogCurOpen(FlowID, FlowCaseID, FlowLogBatNo, Tran) Then
                            Throw New Exception("案件取消授權作業錯誤，取消後無待辦人員!")
                        End If
                    End If
                Else
                    Select Case FlowID
                        Case "AP"
                            Throw New Exception("目前案件並非於S001/S007維護關卡，不得授權處理!")
                    End Select
                End If
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' 為了只有一筆PaperID查詢目前處理人員相關Flow資訊，但PaperID必需為唯一值。
    ''' </summary>
    ''' <param name="PaperID"></param>
    ''' <param name="FlowID"></param>
    ''' <param name="CondStr"></param>
    ''' <param name="tran"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FindFlowKey(ByVal PaperID As String, ByVal FlowID As String, ByVal CondStr As String, ByVal tran As DbTransaction) As DataTable
        Dim strSQL As New StringBuilder

        strSQL.AppendLine("Select a.FlowID, a.FlowCaseID, a.FlowLogBatNo, a.FlowLogID, a.FlowStepID")
        strSQL.AppendLine("From WF_FlowFullLog a inner join WF_FlowCase b ")
        strSQL.AppendLine("	    on a.FlowID = b.FlowID and a.FlowCaseID = b.FlowCaseID and a.FlowLogBatNo = b.LastLogBatNo")
        strSQL.AppendLine("Where b.PaperID = " & Bsp.Utility.Quote(PaperID))
        strSQL.AppendLine("And b.FlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine(CondStr)
        strSQL.AppendLine("Order by FlowLogID Desc")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), tran).Tables(0)
    End Function

    ''' <summary>
    ''' 授權取消將傳入之User清單，不重覆傳出且為目前關卡之待辦人員
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowCaseID"></param>
    ''' <param name="FlowLogBatNo"></param>
    ''' <param name="UserList"></param>
    ''' <param name="SplitKey">若傳入空字串,會以逗號(,)來分割</param>
    ''' <param name="tran"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ToDelAuthDisUser(ByVal FlowID As String, ByVal FlowCaseID As String, ByVal FlowLogBatNo As String, ByVal UserList As String, ByVal SplitKey As String, ByVal Tran As DbTransaction) As String
        If SplitKey = "" Then SplitKey = ","
        UserList = UserList.Replace(SplitKey, "','").ToString
        Dim strSQL As New StringBuilder()
        Dim UserStr As String = ""

        strSQL.AppendLine("SELECT distinct UserID FROM SC_User WHERE UserID IN ('" & UserList & "')")
        strSQL.AppendLine("And UserID in (")
        strSQL.AppendLine("     Select AssignTo From WF_FlowToDoList")
        strSQL.AppendLine("     Where FlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("     And FlowCaseID = " & Bsp.Utility.Quote(FlowCaseID))
        strSQL.AppendLine("     And FlowCaseStatus = 'Open'")
        strSQL.AppendLine("     And FlowLogBatNo = " & Bsp.Utility.Quote(FlowLogBatNo) & ")")
        strSQL.AppendLine("Order by UserID")


        Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), Tran).Tables(0)
            If dt.Rows.Count > 0 Then
                For intLoop As Integer = 0 To dt.Rows.Count - 1
                    UserStr &= ";" & dt.Rows(intLoop).Item("UserID").ToString()
                Next
                UserStr = UserStr.Substring(1)
            End If
        End Using
        Return UserStr
    End Function

    ''' <summary>
    ''' 更新UpdDelAuthFlowFullLog,授權取消時使用
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowCaseID"></param>
    ''' <param name="FlowLogBatNo"></param>
    ''' <param name="DelAssignToStr"></param>
    ''' <param name="FlowStepAction"></param>
    ''' <param name="FlowStepOpinion"></param>
    ''' <param name="ActUser"></param>
    ''' <param name="FlowLogStatus"></param>
    ''' <param name="tran"></param>
    ''' <remarks></remarks>
    Public Sub UpdDelAuthFlowFullLog(ByVal FlowID As String, ByVal FlowCaseID As String, ByVal FlowLogBatNo As String, _
                                     ByVal DelAssignToStr As String, ByVal FlowStepAction As String, ByVal FlowStepOpinion As String, _
                                     ByVal ActUser As String, ByVal FlowLogStatus As String, ByVal tran As DbTransaction)
        Dim strSQL As New StringBuilder()
        Dim AssignToArr() As String = DelAssignToStr.Split(";")

        For intLoop As Integer = 0 To AssignToArr.GetUpperBound(0)
            strSQL.AppendLine("Update WF_FlowFullLog SET  ")
            strSQL.AppendLine("     FlowStepAction = " & Bsp.Utility.Quote(FlowStepAction))
            strSQL.AppendLine("     , FlowStepOpinion = N" & Bsp.Utility.Quote(FlowStepOpinion))

            Using dt As DataTable = GetUserInfo(ActUser)
                strSQL.AppendLine("     , ToBr = " & Bsp.Utility.Quote(dt.Rows(0).Item("DeptID").ToString))
                strSQL.AppendLine("     , ToBrName = " & Bsp.Utility.Quote(dt.Rows(0).Item("DeptName").ToString))
                strSQL.AppendLine("     , ToUser = " & Bsp.Utility.Quote(ActUser))
                strSQL.AppendLine("     , ToUserName = " & Bsp.Utility.Quote(dt.Rows(0).Item("UserName").ToString))
            End Using

            strSQL.AppendLine("     , IsProxy = CASE AssignTo WHEN " & Bsp.Utility.Quote(Trim(ActUser)) & " THEN 'N' ELSE 'Y' END")
            strSQL.AppendLine("     , FlowLogStatus = " & Bsp.Utility.Quote(FlowLogStatus))
            strSQL.AppendLine("     , UpdDate = Getdate() ")
            strSQL.AppendLine("Where FlowID = " & Bsp.Utility.Quote(FlowID))
            strSQL.AppendLine("And FlowCaseID = " & Bsp.Utility.Quote(FlowCaseID))
            strSQL.AppendLine("And FlowLogBatNo = " & Bsp.Utility.Quote(FlowLogBatNo))
            strSQL.AppendLine("And AssignTo = " & Bsp.Utility.Quote(AssignToArr(intLoop)))

            Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)
        Next
    End Sub

    ''' <summary>
    ''' 案件流程處理時，檢查是否此批號案件是否目前為Open狀態
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowCaseID"></param>
    ''' <param name="FlowLogBatNo"></param>
    ''' <param name="tran"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckFlowLogCurOpen(ByVal FlowID As String, ByVal FlowCaseID As String, ByVal FlowLogBatNo As String, ByVal tran As DbTransaction) As Boolean
        Dim strSQL As New StringBuilder
        strSQL.AppendLine("SELECT Count(*) FROM WF_FlowFullLog")
        strSQL.AppendLine("Where FlowID  = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("And FlowCaseID = " & Bsp.Utility.Quote(FlowCaseID))
        strSQL.AppendLine("And FlowLogBatNo = " & Bsp.Utility.Quote(FlowLogBatNo))
        strSQL.AppendLine("And FlowLogStatus = 'Open'")

        If Convert.ToInt32(Bsp.DB.ExecuteScalar(strSQL.ToString(), tran)) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub DoAuthFlowLog(ByVal AppID As String, ByVal AutAssignToStr As String, ByVal OpinionStr As String, ByVal tran As DbTransaction)
        Dim strSQL As New StringBuilder()
        Dim LastFlowLogID As String = ""

        Try
            '檢查案件是否為S001或S007且案件有在本人身上
            Using dt As DataTable = FindFlowKey(AppID, "AP", "And FlowLogStatus = 'Open' AND FlowStepID in ('S001','S007') ", tran)
                If dt.Rows.Count > 0 Then
                    Dim FlowID As String = dt.Rows(0).Item("FlowID").ToString()
                    Dim FlowCaseID As String = dt.Rows(0).Item("FlowCaseID").ToString()
                    Dim FlowLogBatNo As String = dt.Rows(0).Item("FlowLogBatNo").ToString()
                    Dim FlowStepID As String = dt.Rows(0).Item("FlowStepID").ToString()
                    '新增log
                    AutAssignToStr = ToReAssignDisUser(FlowID, FlowCaseID, FlowLogBatNo, AutAssignToStr, "", ";", tran)
                    If AutAssignToStr.Trim <> "" Then
                        LastFlowLogID = Insert_ReAssignFlowFullLog(FlowID, "1", FlowCaseID, FlowLogBatNo, FlowStepID, "", UserProfile.ActUserID, AutAssignToStr, "Open", "[" & UserProfile.ActUserID & "授权]" & OpinionStr, tran)
                        '執行更新ToDolist
                        PutFlowToDoList(FlowID, FlowCaseID, tran)
                        '更新FlowCase之LastFlowLogID,UpdDate
                        Update_ReAssignFlowCase(FlowID, FlowCaseID, LastFlowLogID, tran)
                    Else
                        Throw New Exception("未授權予任何人員或已存在之處理人員，流程未做授權處理!")
                    End If
                Else
                    Throw New Exception("目前案件並非於S001/S007維護關卡，不得授權處理!")
                End If
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' 此fun為特例，為了只有一筆AppID,用於授信案件移管
    ''' </summary>
    ''' <param name="AppID"></param>
    ''' <param name="CurrAssignTo"></param>
    ''' <param name="ChgAssignTo"></param>
    ''' <param name="OpinionStr"></param>
    ''' <param name="tran"></param>
    ''' <remarks></remarks>
    Public Sub DoChangeAOFlowLog(ByVal AppID As String, ByVal CurrAssignTo As String, ByVal ChgAssignTo As String, ByVal OpinionStr As String, ByVal tran As DbTransaction)
        Dim strSQL As New StringBuilder
        Dim ActFlowLogID As String

        Try
            strSQL.AppendLine("SELECT IsNull(MAX(a.FlowLogID),'') AS MaxLogID")
            strSQL.AppendLine("FROM  WF_FlowFullLog a inner join WF_FlowCase b on a.FlowID = b.FlowID And a.FlowCaseID = b.FlowCaseID ")
            strSQL.AppendLine("Where b.FlowID = 'AP'")
            'strSQL.AppendLine("And b.FlowVer = 1")
            strSQL.AppendLine("And b.PaperID = " & Bsp.Utility.Quote(AppID))
            strSQL.AppendLine("And a.FlowStepID = 'S001'")
            strSQL.AppendLine("And ISNULL(a.ToUser, '') <> ''")
            strSQL.AppendLine("And a.FlowStepAction <> '改派'")

            ActFlowLogID = Bsp.DB.ExecuteScalar(strSQL.ToString, tran).ToString()
            DoChangeFlowFullLog("AP", ActFlowLogID, ChgAssignTo, "案件移管", tran)
            '檢查案件是否為S001且案件有在本人身上
            Using dt As DataTable = FindStepFlowKey(AppID, "AP", CurrAssignTo, "S001", tran)
                If dt.Rows.Count > 0 Then
                    For intLoop As Integer = 0 To dt.Rows.Count - 1
                        FlowReAssign(dt.Rows(intLoop).Item("FlowID").ToString(), _
                                     dt.Rows(intLoop).Item("FlowVer").ToString(), _
                                     dt.Rows(intLoop).Item("FlowCaseID").ToString(), _
                                     dt.Rows(intLoop).Item("FlowLogBatNo").ToString(), _
                                     dt.Rows(intLoop).Item("FlowLogID").ToString(), _
                                     ChgAssignTo, _
                                     "[案件移管]" & CurrAssignTo & "-->" & ChgAssignTo & "<br>" & OpinionStr, _
                                     tran)
                    Next
                End If
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' 為了只有一筆PaperID查詢目前處理人員相關Flow資訊(並為固定關卡)，但PaperID必需為唯一值。
    ''' </summary>
    ''' <param name="PaperID"></param>
    ''' <param name="FlowID"></param>
    ''' <param name="AssignTo"></param>
    ''' <param name="FlowCurrStepID"></param>
    ''' <param name="tran"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FindStepFlowKey(ByVal PaperID As String, ByVal FlowID As String, ByVal AssignTo As String, ByVal FlowCurrStepID As String, ByVal tran As DbTransaction) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select a.FlowID, a.FlowCaseID, a.FlowLogBatNo, a.FlowLogID, a.FlowStepID, b.FlowVer")
        strSQL.AppendLine("From WF_FlowFullLog a inner join WF_FlowCase b ")
        strSQL.AppendLine("	    on a.FlowID = b.FlowID and a.FlowCaseID = b.FlowCaseID and a.FlowLogBatNo = b.LastLogBatNo")
        strSQL.AppendLine("Where a.FlowLogStatus = 'Open'")
        strSQL.AppendLine("And a.AssignTo = " & Bsp.Utility.Quote(AssignTo))
        strSQL.AppendLine("And b.PaperID = " & Bsp.Utility.Quote(PaperID))
        strSQL.AppendLine("And b.FlowID = " & Bsp.Utility.Quote(FlowID))
        strSQL.AppendLine("And b.FlowCurrStepID = " & Bsp.Utility.Quote(FlowCurrStepID))
        strSQL.AppendLine("Order by b.FlowLogID desc")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), tran).Tables(0)
    End Function

    ''' <summary>
    ''' 處理直接變更FlowLog,例:案件移管時，Flow更新
    ''' </summary>
    ''' <param name="FlowID"></param>
    ''' <param name="FlowLogID"></param>
    ''' <param name="ChgAssignTo"></param>
    ''' <param name="ChgDesc"></param>
    ''' <param name="tran"></param>
    ''' <remarks></remarks>
    Private Sub DoChangeFlowFullLog(ByVal FlowID As String, ByVal FlowLogID As String, ByVal ChgAssignTo As String, ByVal ChgDesc As String, ByVal tran As DbTransaction)
        Dim strSQL As New StringBuilder

        strSQL.AppendLine("UPDATE WF_FlowFullLog SET")
        strSQL.AppendLine("     AssignTo = " & Bsp.Utility.Quote(ChgAssignTo) & ", ")
        strSQL.AppendLine("     AssignToName = (Select UserName From SC_User Where UserID = " & Bsp.Utility.Quote(ChgAssignTo) & "), ")
        strSQL.AppendLine("     LogRemark = N'變更事由:" & ChgDesc.Replace("'", "''") & " 原處理人員(' + AssignTo + N') 變更時間:" & Now.ToString() & "'")
        strSQL.AppendLine("WHERE FlowID = " & Bsp.Utility.Quote(FlowID) & " AND FlowLogID = " & Bsp.Utility.Quote(FlowLogID))

        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)
    End Sub
#End Region

#Region "WFA092"
    Public Function getFlowBackLog(ByVal ht As Hashtable) As DataTable
        Dim strSQL As New StringBuilder
        Dim strSQL_1 As New StringBuilder
        'Dim strSQL_2 As New StringBuilder

        strSQL.AppendLine("Select Top " & Bsp.Utility.QueryLimit() & " * From (")

        strSQL.AppendLine("Select b.Description FlowStepDesc, a.FlowCaseID, a.AppID, ")
        strSQL.AppendLine(" dbo.funGetAOrgDefine('1', a.UserID) UserNm,")
        strSQL.AppendLine(" Convert(varchar, a.LogDate, 20) LogDate, a.Remark, ")
        strSQL.AppendLine(" dbo.funGetAOrgDefine('1', c.OfficerID) AONm,")
        strSQL.AppendLine(" rtrim(c.CustomerID) + '-' + dbo.funGetCustomerName(c.CustomerID, a.AppID) CustomerNm")
        strSQL.AppendLine("From WF_FlowBackLog a with (nolock) inner join WF_FlowStepM b with (nolock) on a.FlowStepID = b.FlowStepID")
        strSQL.AppendLine(" inner join AP_Profile c with (nolock) on a.AppID = c.AppID")
        strSQL.AppendLine("Where a.FlowStepID = 'S999'")

        strSQL_1.AppendLine("Select b.Description FlowStepDesc, a.FlowCaseID, a.AppID, ")
        strSQL_1.AppendLine(" dbo.funGetAOrgDefine('1', a.UserID) UserNm,")
        strSQL_1.AppendLine(" Convert(varchar, a.LogDate, 20) LogDate, a.Remark, ")
        strSQL_1.AppendLine(" dbo.funGetAOrgDefine('1', d.AO_CODE) AONm,")
        strSQL_1.AppendLine(" rtrim(c.CustomerID) + '-' + dbo.funGetCustomerName(c.CustomerID, c.AppID) CustomerNm")
        strSQL_1.AppendLine("From WF_FlowBackLog a with (nolock) inner join WF_FlowStepM b with (nolock) on a.FlowStepID = b.FlowStepID")
        strSQL_1.AppendLine(" inner join CC_Profile c with (nolock) on a.AppID = c.CCID")
        strSQL_1.AppendLine(" inner join BR_Customer d with (nolock) on c.AppID = d.AppID and c.CustomerID = d.CustomerID")
        strSQL_1.AppendLine("Where a.FlowStepID = 'CC99'")

        'strSQL_2.AppendLine("select (select top 1 FlowStepDesc from WF_FlowStep b with (nolock) where b.FlowStepID = a.FlowStepID order by FlowVer desc)")
        'strSQL_2.AppendLine("	, a.FlowCaseID, a.AppID, dbo.funGetAOrgDefine('1', a.UserID) UserNm")
        'strSQL_2.AppendLine("	, Convert(varchar, a.LogDate, 20) LogDate, a.Remark, dbo.funGetAOrgDefine('1', c.AOID) AONm")

        'strSQL_2.AppendLine("	, rtrim(c.CustomerID) + '-' + dbo.funEncryptName(c.CustomerID,dbo.funGetCustomerName(c.CustomerID, c.AppID)) CustomerNm")
        'strSQL_2.AppendLine("  from WF_FlowBackLog a with (nolock) ")
        'strSQL_2.AppendLine(" inner join CR_Profile c with (nolock) on a.AppID = c.CRID")
        'strSQL_2.AppendLine(" where a.FlowStepID in ('RV98', 'RV99')")
        'strSQL_2.AppendLine("   and a.AppID like 'CR%'")

        For Each strKey As String In ht.Keys
            If ht(strKey) <> "" Then
                Select Case strKey
                    Case "txtCustomerID"
                        If ht("ddlCustomerID") = "=" Then
                            strSQL.AppendLine("  And a.AppID in (Select AppID From BR_Customer with (nolock) Where AppID <> ''")
                            strSQL.AppendLine("  And CustomerID = " & Bsp.Utility.Quote(ht(strKey)) & ")")
                            strSQL_1.AppendLine("  And c.CustomerID = " & Bsp.Utility.Quote(ht(strKey)))
                            'strSQL_2.AppendLine("   and c.CustomerID = " & Bsp.Utility.Quote(ht(strKey)))
                        Else
                            strSQL.AppendLine("  And a.AppID in (Select AppID From BR_Customer with (nolock) Where AppID <> ''")
                            strSQL.AppendLine("  And CustomerID like " & Bsp.Utility.Quote("%" & ht(strKey) & "%") & ")")
                            strSQL_1.AppendLine("  And c.CustomerID like " & Bsp.Utility.Quote("%" & ht(strKey) & "%"))
                            'strSQL_2.AppendLine("   and c.CustomerID like " & Bsp.Utility.Quote("%" & ht(strKey) & "%"))
                        End If
                    Case "txtCName"
                        If ht("ddlCName") = "=" Then
                            strSQL.AppendLine("  And a.AppID in (Select AppID From BR_Customer with (nolock) Where AppID <> ''")
                            strSQL.AppendLine("  And CName = " & Bsp.Utility.Quote(ht(strKey)) & ")")
                            strSQL_1.AppendLine("  And d.CName = " & Bsp.Utility.Quote(ht(strKey)))
                            'strSQL_2.AppendLine("   and c.CName = " & Bsp.Utility.Quote(ht(strKey)))
                        Else
                            strSQL.AppendLine("  And a.AppID in (Select AppID From BR_Customer with (nolock) Where AppID <> ''")
                            strSQL.AppendLine("  And CName like " & Bsp.Utility.Quote("%" & ht(strKey) & "%") & ")")
                            strSQL_1.AppendLine("  And d.CName like " & Bsp.Utility.Quote("%" & ht(strKey) & "%"))
                            'strSQL_2.AppendLine("   and c.CName like " & Bsp.Utility.Quote("%" & ht(strKey) & "%"))
                        End If
                    Case "txtAppID"
                        If ht("ddlAppID") = "=" Then
                            strSQL.AppendLine("  And a.AppID = " & Bsp.Utility.Quote(ht(strKey)))
                            strSQL_1.AppendLine("  And a.AppID = " & Bsp.Utility.Quote(ht(strKey)))
                            'strSQL_2.AppendLine("   and a.AppID = " & Bsp.Utility.Quote(ht(strKey)))
                        Else
                            strSQL.AppendLine("  And a.AppID like " & Bsp.Utility.Quote("%" & ht(strKey) & "%"))
                            strSQL_1.AppendLine("  And a.AppID like " & Bsp.Utility.Quote("%" & ht(strKey) & "%"))
                            'strSQL_2.AppendLine("   and a.AppID like " & Bsp.Utility.Quote("%" & ht(strKey) & "%"))
                        End If
                    Case "ddlUserID"
                        strSQL.AppendLine("  And a.UserID = " & Bsp.Utility.Quote(ht(strKey)))
                        strSQL_1.AppendLine("  And a.UserID = " & Bsp.Utility.Quote(ht(strKey)))
                        'strSQL_2.AppendLine("   and a.UserID = " & Bsp.Utility.Quote(ht(strKey)))
                End Select
            End If
        Next

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString() & " Union " & strSQL_1.ToString() & ") a Order by a.LogDate desc").Tables(0)
    End Function

    Public Function getFlowBackExecuteUser() As DataTable
        Dim strSQL As New StringBuilder

        strSQL.AppendLine("Select dbo.funGetAOrgDefine('1', UserID) UserNm, UserID")
        strSQL.AppendLine("From (Select distinct UserID")
        strSQL.AppendLine("      From WF_FlowBackLog with (nolock)) a")
        strSQL.AppendLine("Order by UserID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function
#End Region

End Class
