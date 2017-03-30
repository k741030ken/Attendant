'****************************************************************
' Table:EmployeeWait
' Created Date: 2016.11.04
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beEmployeeWait
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "EmpID", "ValidDate", "Seq", "Reason", "NewCompID", "DeptID", "OrganID", "RankID", "TitleID" _
                                    , "TitleName", "PositionID", "WorkTypeID", "GroupID", "GroupOrganID", "IsBoss", "IsSecBoss", "IsGroupBoss", "IsSecGroupBoss", "BossType", "Remark" _
                                    , "ValidMark", "FlowOrganID", "WorkSiteID", "ApplyID", "ProcessDate", "ApplyDate", "DueDate", "SalaryPaid", "QuitReason", "EvaluationMark", "WTID" _
                                    , "ExistsEmployeeLog", "RecID", "ContractDate", "BindData", "BusinessType", "EmpFlowRemarkID", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(Date), GetType(Integer), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(Date), GetType(Date), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(Date), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "EmpID", "ValidDate", "Seq", "CompID" }

        Public ReadOnly Property Rows() As beEmployeeWait.Rows 
            Get
                Return m_Rows
            End Get
        End Property

        Public ReadOnly Property FieldNames() As String()
            Get
                Return m_Fields
            End Get
        End Property

        Public ReadOnly Property PrimaryFieldNames() As String()
            Get
                Return m_PrimaryFields
            End Get
        End Property

        Public Function IsPrimaryKey(ByVal fieldName As String) As Boolean
            Dim iKeys As IEnumerable(Of String) = From s In m_PrimaryFields Where s.ToString().Equals(fieldName) Select s
            Return IIf(iKeys.Count() > 0, True, False)
        End Function

        Public Sub Dispose()
            m_Rows.Dispose()
        End Sub

        ''' <summary>
        ''' 將DataTable資料轉成entity
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Transfer2Row(EmployeeWaitTable As DataTable)
            For Each dr As DataRow In EmployeeWaitTable.Rows
                m_Rows.Add(New Row(dr))
            Next
        End Sub

        ''' <summary>
        ''' 將Entity的資料轉成DataTable
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Transfer2DataTable() As DataTable
            Dim dt As DataTable = New DataTable()
            Dim dcPrimary As DataColumn() = New DataColumn() {}

            For i As Integer = 0 To m_Fields.Length - 1
                Dim dc As DataColumn = New DataColumn(m_Fields(i), m_Types(i))
                If IsPrimaryKey(m_Fields(i)) Then
                    Array.Resize(Of DataColumn)(dcPrimary, dcPrimary.Length + 1)
                    dcPrimary(dcPrimary.Length - 1) = dc
                End If
            Next

            For i As Integer = 0 To m_Rows.Count - 1
                Dim dr As DataRow = dt.NewRow()

                dr(m_Rows(i).CompID.FieldName) = m_Rows(i).CompID.Value
                dr(m_Rows(i).EmpID.FieldName) = m_Rows(i).EmpID.Value
                dr(m_Rows(i).ValidDate.FieldName) = m_Rows(i).ValidDate.Value
                dr(m_Rows(i).Seq.FieldName) = m_Rows(i).Seq.Value
                dr(m_Rows(i).Reason.FieldName) = m_Rows(i).Reason.Value
                dr(m_Rows(i).NewCompID.FieldName) = m_Rows(i).NewCompID.Value
                dr(m_Rows(i).DeptID.FieldName) = m_Rows(i).DeptID.Value
                dr(m_Rows(i).OrganID.FieldName) = m_Rows(i).OrganID.Value
                dr(m_Rows(i).RankID.FieldName) = m_Rows(i).RankID.Value
                dr(m_Rows(i).TitleID.FieldName) = m_Rows(i).TitleID.Value
                dr(m_Rows(i).TitleName.FieldName) = m_Rows(i).TitleName.Value
                dr(m_Rows(i).PositionID.FieldName) = m_Rows(i).PositionID.Value
                dr(m_Rows(i).WorkTypeID.FieldName) = m_Rows(i).WorkTypeID.Value
                dr(m_Rows(i).GroupID.FieldName) = m_Rows(i).GroupID.Value
                dr(m_Rows(i).GroupOrganID.FieldName) = m_Rows(i).GroupOrganID.Value
                dr(m_Rows(i).IsBoss.FieldName) = m_Rows(i).IsBoss.Value
                dr(m_Rows(i).IsSecBoss.FieldName) = m_Rows(i).IsSecBoss.Value
                dr(m_Rows(i).IsGroupBoss.FieldName) = m_Rows(i).IsGroupBoss.Value
                dr(m_Rows(i).IsSecGroupBoss.FieldName) = m_Rows(i).IsSecGroupBoss.Value
                dr(m_Rows(i).BossType.FieldName) = m_Rows(i).BossType.Value
                dr(m_Rows(i).Remark.FieldName) = m_Rows(i).Remark.Value
                dr(m_Rows(i).ValidMark.FieldName) = m_Rows(i).ValidMark.Value
                dr(m_Rows(i).FlowOrganID.FieldName) = m_Rows(i).FlowOrganID.Value
                dr(m_Rows(i).WorkSiteID.FieldName) = m_Rows(i).WorkSiteID.Value
                dr(m_Rows(i).ApplyID.FieldName) = m_Rows(i).ApplyID.Value
                dr(m_Rows(i).ProcessDate.FieldName) = m_Rows(i).ProcessDate.Value
                dr(m_Rows(i).ApplyDate.FieldName) = m_Rows(i).ApplyDate.Value
                dr(m_Rows(i).DueDate.FieldName) = m_Rows(i).DueDate.Value
                dr(m_Rows(i).SalaryPaid.FieldName) = m_Rows(i).SalaryPaid.Value
                dr(m_Rows(i).QuitReason.FieldName) = m_Rows(i).QuitReason.Value
                dr(m_Rows(i).EvaluationMark.FieldName) = m_Rows(i).EvaluationMark.Value
                dr(m_Rows(i).WTID.FieldName) = m_Rows(i).WTID.Value
                dr(m_Rows(i).ExistsEmployeeLog.FieldName) = m_Rows(i).ExistsEmployeeLog.Value
                dr(m_Rows(i).RecID.FieldName) = m_Rows(i).RecID.Value
                dr(m_Rows(i).ContractDate.FieldName) = m_Rows(i).ContractDate.Value
                dr(m_Rows(i).BindData.FieldName) = m_Rows(i).BindData.Value
                dr(m_Rows(i).BusinessType.FieldName) = m_Rows(i).BusinessType.Value
                dr(m_Rows(i).EmpFlowRemarkID.FieldName) = m_Rows(i).EmpFlowRemarkID.Value
                dr(m_Rows(i).LastChgComp.FieldName) = m_Rows(i).LastChgComp.Value
                dr(m_Rows(i).LastChgID.FieldName) = m_Rows(i).LastChgID.Value
                dr(m_Rows(i).LastChgDate.FieldName) = m_Rows(i).LastChgDate.Value

                dt.Rows.Add(dr)
            Next

            Return dt
        End Function

    End Class

    Public Class Rows
        Private m_Rows As List(Of Row) = New List(Of Row)()

        Default Public ReadOnly Property Rows(ByVal i As Integer) As Row
            Get
                Return m_Rows(i)
            End Get
        End Property

        Public ReadOnly Property Count() As Integer
            Get
                Return m_Rows.Count
            End Get
        End Property

        Public Sub Add(EmployeeWaitRow As Row)
            m_Rows.Add(EmployeeWaitRow)
        End Sub

        Public Sub Remove(EmployeeWaitRow As Row)
            If m_Rows.IndexOf(EmployeeWaitRow) >= 0 Then
                m_Rows.Remove(EmployeeWaitRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_EmpID As Field(Of String) = new Field(Of String)("EmpID", true)
        Private FI_ValidDate As Field(Of Date) = new Field(Of Date)("ValidDate", true)
        Private FI_Seq As Field(Of Integer) = new Field(Of Integer)("Seq", true)
        Private FI_Reason As Field(Of String) = new Field(Of String)("Reason", true)
        Private FI_NewCompID As Field(Of String) = new Field(Of String)("NewCompID", true)
        Private FI_DeptID As Field(Of String) = new Field(Of String)("DeptID", true)
        Private FI_OrganID As Field(Of String) = new Field(Of String)("OrganID", true)
        Private FI_RankID As Field(Of String) = new Field(Of String)("RankID", true)
        Private FI_TitleID As Field(Of String) = new Field(Of String)("TitleID", true)
        Private FI_TitleName As Field(Of String) = new Field(Of String)("TitleName", true)
        Private FI_PositionID As Field(Of String) = new Field(Of String)("PositionID", true)
        Private FI_WorkTypeID As Field(Of String) = new Field(Of String)("WorkTypeID", true)
        Private FI_GroupID As Field(Of String) = new Field(Of String)("GroupID", true)
        Private FI_GroupOrganID As Field(Of String) = new Field(Of String)("GroupOrganID", true)
        Private FI_IsBoss As Field(Of String) = new Field(Of String)("IsBoss", true)
        Private FI_IsSecBoss As Field(Of String) = new Field(Of String)("IsSecBoss", true)
        Private FI_IsGroupBoss As Field(Of String) = new Field(Of String)("IsGroupBoss", true)
        Private FI_IsSecGroupBoss As Field(Of String) = new Field(Of String)("IsSecGroupBoss", true)
        Private FI_BossType As Field(Of String) = new Field(Of String)("BossType", true)
        Private FI_Remark As Field(Of String) = new Field(Of String)("Remark", true)
        Private FI_ValidMark As Field(Of String) = new Field(Of String)("ValidMark", true)
        Private FI_FlowOrganID As Field(Of String) = new Field(Of String)("FlowOrganID", true)
        Private FI_WorkSiteID As Field(Of String) = new Field(Of String)("WorkSiteID", true)
        Private FI_ApplyID As Field(Of String) = new Field(Of String)("ApplyID", true)
        Private FI_ProcessDate As Field(Of Date) = new Field(Of Date)("ProcessDate", true)
        Private FI_ApplyDate As Field(Of Date) = new Field(Of Date)("ApplyDate", true)
        Private FI_DueDate As Field(Of Date) = new Field(Of Date)("DueDate", true)
        Private FI_SalaryPaid As Field(Of String) = new Field(Of String)("SalaryPaid", true)
        Private FI_QuitReason As Field(Of String) = new Field(Of String)("QuitReason", true)
        Private FI_EvaluationMark As Field(Of String) = new Field(Of String)("EvaluationMark", true)
        Private FI_WTID As Field(Of String) = new Field(Of String)("WTID", true)
        Private FI_ExistsEmployeeLog As Field(Of String) = new Field(Of String)("ExistsEmployeeLog", true)
        Private FI_RecID As Field(Of String) = new Field(Of String)("RecID", true)
        Private FI_ContractDate As Field(Of Date) = new Field(Of Date)("ContractDate", true)
        Private FI_BindData As Field(Of String) = new Field(Of String)("BindData", true)
        Private FI_BusinessType As Field(Of String) = new Field(Of String)("BusinessType", true)
        Private FI_EmpFlowRemarkID As Field(Of String) = new Field(Of String)("EmpFlowRemarkID", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "CompID", "EmpID", "ValidDate", "Seq", "Reason", "NewCompID", "DeptID", "OrganID", "RankID", "TitleID" _
                                    , "TitleName", "PositionID", "WorkTypeID", "GroupID", "GroupOrganID", "IsBoss", "IsSecBoss", "IsGroupBoss", "IsSecGroupBoss", "BossType", "Remark" _
                                    , "ValidMark", "FlowOrganID", "WorkSiteID", "ApplyID", "ProcessDate", "ApplyDate", "DueDate", "SalaryPaid", "QuitReason", "EvaluationMark", "WTID" _
                                    , "ExistsEmployeeLog", "RecID", "ContractDate", "BindData", "BusinessType", "EmpFlowRemarkID", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "EmpID", "ValidDate", "Seq", "CompID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CompID"
                    Return FI_CompID.Value
                Case "EmpID"
                    Return FI_EmpID.Value
                Case "ValidDate"
                    Return FI_ValidDate.Value
                Case "Seq"
                    Return FI_Seq.Value
                Case "Reason"
                    Return FI_Reason.Value
                Case "NewCompID"
                    Return FI_NewCompID.Value
                Case "DeptID"
                    Return FI_DeptID.Value
                Case "OrganID"
                    Return FI_OrganID.Value
                Case "RankID"
                    Return FI_RankID.Value
                Case "TitleID"
                    Return FI_TitleID.Value
                Case "TitleName"
                    Return FI_TitleName.Value
                Case "PositionID"
                    Return FI_PositionID.Value
                Case "WorkTypeID"
                    Return FI_WorkTypeID.Value
                Case "GroupID"
                    Return FI_GroupID.Value
                Case "GroupOrganID"
                    Return FI_GroupOrganID.Value
                Case "IsBoss"
                    Return FI_IsBoss.Value
                Case "IsSecBoss"
                    Return FI_IsSecBoss.Value
                Case "IsGroupBoss"
                    Return FI_IsGroupBoss.Value
                Case "IsSecGroupBoss"
                    Return FI_IsSecGroupBoss.Value
                Case "BossType"
                    Return FI_BossType.Value
                Case "Remark"
                    Return FI_Remark.Value
                Case "ValidMark"
                    Return FI_ValidMark.Value
                Case "FlowOrganID"
                    Return FI_FlowOrganID.Value
                Case "WorkSiteID"
                    Return FI_WorkSiteID.Value
                Case "ApplyID"
                    Return FI_ApplyID.Value
                Case "ProcessDate"
                    Return FI_ProcessDate.Value
                Case "ApplyDate"
                    Return FI_ApplyDate.Value
                Case "DueDate"
                    Return FI_DueDate.Value
                Case "SalaryPaid"
                    Return FI_SalaryPaid.Value
                Case "QuitReason"
                    Return FI_QuitReason.Value
                Case "EvaluationMark"
                    Return FI_EvaluationMark.Value
                Case "WTID"
                    Return FI_WTID.Value
                Case "ExistsEmployeeLog"
                    Return FI_ExistsEmployeeLog.Value
                Case "RecID"
                    Return FI_RecID.Value
                Case "ContractDate"
                    Return FI_ContractDate.Value
                Case "BindData"
                    Return FI_BindData.Value
                Case "BusinessType"
                    Return FI_BusinessType.Value
                Case "EmpFlowRemarkID"
                    Return FI_EmpFlowRemarkID.Value
                Case "LastChgComp"
                    Return FI_LastChgComp.Value
                Case "LastChgID"
                    Return FI_LastChgID.Value
                Case "LastChgDate"
                    Return FI_LastChgDate.Value
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Sub SetFieldValue(ByVal fieldName As String, ByVal value As Object)
            Select Case fieldName
                Case "CompID"
                    FI_CompID.SetValue(value)
                Case "EmpID"
                    FI_EmpID.SetValue(value)
                Case "ValidDate"
                    FI_ValidDate.SetValue(value)
                Case "Seq"
                    FI_Seq.SetValue(value)
                Case "Reason"
                    FI_Reason.SetValue(value)
                Case "NewCompID"
                    FI_NewCompID.SetValue(value)
                Case "DeptID"
                    FI_DeptID.SetValue(value)
                Case "OrganID"
                    FI_OrganID.SetValue(value)
                Case "RankID"
                    FI_RankID.SetValue(value)
                Case "TitleID"
                    FI_TitleID.SetValue(value)
                Case "TitleName"
                    FI_TitleName.SetValue(value)
                Case "PositionID"
                    FI_PositionID.SetValue(value)
                Case "WorkTypeID"
                    FI_WorkTypeID.SetValue(value)
                Case "GroupID"
                    FI_GroupID.SetValue(value)
                Case "GroupOrganID"
                    FI_GroupOrganID.SetValue(value)
                Case "IsBoss"
                    FI_IsBoss.SetValue(value)
                Case "IsSecBoss"
                    FI_IsSecBoss.SetValue(value)
                Case "IsGroupBoss"
                    FI_IsGroupBoss.SetValue(value)
                Case "IsSecGroupBoss"
                    FI_IsSecGroupBoss.SetValue(value)
                Case "BossType"
                    FI_BossType.SetValue(value)
                Case "Remark"
                    FI_Remark.SetValue(value)
                Case "ValidMark"
                    FI_ValidMark.SetValue(value)
                Case "FlowOrganID"
                    FI_FlowOrganID.SetValue(value)
                Case "WorkSiteID"
                    FI_WorkSiteID.SetValue(value)
                Case "ApplyID"
                    FI_ApplyID.SetValue(value)
                Case "ProcessDate"
                    FI_ProcessDate.SetValue(value)
                Case "ApplyDate"
                    FI_ApplyDate.SetValue(value)
                Case "DueDate"
                    FI_DueDate.SetValue(value)
                Case "SalaryPaid"
                    FI_SalaryPaid.SetValue(value)
                Case "QuitReason"
                    FI_QuitReason.SetValue(value)
                Case "EvaluationMark"
                    FI_EvaluationMark.SetValue(value)
                Case "WTID"
                    FI_WTID.SetValue(value)
                Case "ExistsEmployeeLog"
                    FI_ExistsEmployeeLog.SetValue(value)
                Case "RecID"
                    FI_RecID.SetValue(value)
                Case "ContractDate"
                    FI_ContractDate.SetValue(value)
                Case "BindData"
                    FI_BindData.SetValue(value)
                Case "BusinessType"
                    FI_BusinessType.SetValue(value)
                Case "EmpFlowRemarkID"
                    FI_EmpFlowRemarkID.SetValue(value)
                Case "LastChgComp"
                    FI_LastChgComp.SetValue(value)
                Case "LastChgID"
                    FI_LastChgID.SetValue(value)
                Case "LastChgDate"
                    FI_LastChgDate.SetValue(value)
            End Select
        End Sub

        Default Public Property Row(ByVal fieldName As String) As Object
            Get
                Return GetFieldValue(fieldName)
            End Get
            Set(ByVal value As Object)
                SetFieldValue(fieldName, value)
            End Set
        End Property

        Default Public Property Row(ByVal idx As Integer) As Object
            Get
                Return GetFieldValue(m_FieldNames(idx))
            End Get
            Set(ByVal value As Object)
                SetFieldValue(m_FieldNames(idx), value)
            End Set
        End Property

        Public ReadOnly Property FieldNames() As String()
            Get
                Return m_FieldNames
            End Get
        End Property

        Public ReadOnly Property FieldCount() As Integer
            Get
                Return m_FieldNames.Length
            End Get
        End Property

        Public Function IsUpdated(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "CompID"
                    return FI_CompID.Updated
                Case "EmpID"
                    return FI_EmpID.Updated
                Case "ValidDate"
                    return FI_ValidDate.Updated
                Case "Seq"
                    return FI_Seq.Updated
                Case "Reason"
                    return FI_Reason.Updated
                Case "NewCompID"
                    return FI_NewCompID.Updated
                Case "DeptID"
                    return FI_DeptID.Updated
                Case "OrganID"
                    return FI_OrganID.Updated
                Case "RankID"
                    return FI_RankID.Updated
                Case "TitleID"
                    return FI_TitleID.Updated
                Case "TitleName"
                    return FI_TitleName.Updated
                Case "PositionID"
                    return FI_PositionID.Updated
                Case "WorkTypeID"
                    return FI_WorkTypeID.Updated
                Case "GroupID"
                    return FI_GroupID.Updated
                Case "GroupOrganID"
                    return FI_GroupOrganID.Updated
                Case "IsBoss"
                    return FI_IsBoss.Updated
                Case "IsSecBoss"
                    return FI_IsSecBoss.Updated
                Case "IsGroupBoss"
                    return FI_IsGroupBoss.Updated
                Case "IsSecGroupBoss"
                    return FI_IsSecGroupBoss.Updated
                Case "BossType"
                    return FI_BossType.Updated
                Case "Remark"
                    return FI_Remark.Updated
                Case "ValidMark"
                    return FI_ValidMark.Updated
                Case "FlowOrganID"
                    return FI_FlowOrganID.Updated
                Case "WorkSiteID"
                    return FI_WorkSiteID.Updated
                Case "ApplyID"
                    return FI_ApplyID.Updated
                Case "ProcessDate"
                    return FI_ProcessDate.Updated
                Case "ApplyDate"
                    return FI_ApplyDate.Updated
                Case "DueDate"
                    return FI_DueDate.Updated
                Case "SalaryPaid"
                    return FI_SalaryPaid.Updated
                Case "QuitReason"
                    return FI_QuitReason.Updated
                Case "EvaluationMark"
                    return FI_EvaluationMark.Updated
                Case "WTID"
                    return FI_WTID.Updated
                Case "ExistsEmployeeLog"
                    return FI_ExistsEmployeeLog.Updated
                Case "RecID"
                    return FI_RecID.Updated
                Case "ContractDate"
                    return FI_ContractDate.Updated
                Case "BindData"
                    return FI_BindData.Updated
                Case "BusinessType"
                    return FI_BusinessType.Updated
                Case "EmpFlowRemarkID"
                    return FI_EmpFlowRemarkID.Updated
                Case "LastChgComp"
                    return FI_LastChgComp.Updated
                Case "LastChgID"
                    return FI_LastChgID.Updated
                Case "LastChgDate"
                    return FI_LastChgDate.Updated
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public Function CreateUpdateSQL(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "CompID"
                    return FI_CompID.CreateUpdateSQL
                Case "EmpID"
                    return FI_EmpID.CreateUpdateSQL
                Case "ValidDate"
                    return FI_ValidDate.CreateUpdateSQL
                Case "Seq"
                    return FI_Seq.CreateUpdateSQL
                Case "Reason"
                    return FI_Reason.CreateUpdateSQL
                Case "NewCompID"
                    return FI_NewCompID.CreateUpdateSQL
                Case "DeptID"
                    return FI_DeptID.CreateUpdateSQL
                Case "OrganID"
                    return FI_OrganID.CreateUpdateSQL
                Case "RankID"
                    return FI_RankID.CreateUpdateSQL
                Case "TitleID"
                    return FI_TitleID.CreateUpdateSQL
                Case "TitleName"
                    return FI_TitleName.CreateUpdateSQL
                Case "PositionID"
                    return FI_PositionID.CreateUpdateSQL
                Case "WorkTypeID"
                    return FI_WorkTypeID.CreateUpdateSQL
                Case "GroupID"
                    return FI_GroupID.CreateUpdateSQL
                Case "GroupOrganID"
                    return FI_GroupOrganID.CreateUpdateSQL
                Case "IsBoss"
                    return FI_IsBoss.CreateUpdateSQL
                Case "IsSecBoss"
                    return FI_IsSecBoss.CreateUpdateSQL
                Case "IsGroupBoss"
                    return FI_IsGroupBoss.CreateUpdateSQL
                Case "IsSecGroupBoss"
                    return FI_IsSecGroupBoss.CreateUpdateSQL
                Case "BossType"
                    return FI_BossType.CreateUpdateSQL
                Case "Remark"
                    return FI_Remark.CreateUpdateSQL
                Case "ValidMark"
                    return FI_ValidMark.CreateUpdateSQL
                Case "FlowOrganID"
                    return FI_FlowOrganID.CreateUpdateSQL
                Case "WorkSiteID"
                    return FI_WorkSiteID.CreateUpdateSQL
                Case "ApplyID"
                    return FI_ApplyID.CreateUpdateSQL
                Case "ProcessDate"
                    return FI_ProcessDate.CreateUpdateSQL
                Case "ApplyDate"
                    return FI_ApplyDate.CreateUpdateSQL
                Case "DueDate"
                    return FI_DueDate.CreateUpdateSQL
                Case "SalaryPaid"
                    return FI_SalaryPaid.CreateUpdateSQL
                Case "QuitReason"
                    return FI_QuitReason.CreateUpdateSQL
                Case "EvaluationMark"
                    return FI_EvaluationMark.CreateUpdateSQL
                Case "WTID"
                    return FI_WTID.CreateUpdateSQL
                Case "ExistsEmployeeLog"
                    return FI_ExistsEmployeeLog.CreateUpdateSQL
                Case "RecID"
                    return FI_RecID.CreateUpdateSQL
                Case "ContractDate"
                    return FI_ContractDate.CreateUpdateSQL
                Case "BindData"
                    return FI_BindData.CreateUpdateSQL
                Case "BusinessType"
                    return FI_BusinessType.CreateUpdateSQL
                Case "EmpFlowRemarkID"
                    return FI_EmpFlowRemarkID.CreateUpdateSQL
                Case "LastChgComp"
                    return FI_LastChgComp.CreateUpdateSQL
                Case "LastChgID"
                    return FI_LastChgID.CreateUpdateSQL
                Case "LastChgDate"
                    return FI_LastChgDate.CreateUpdateSQL
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public ReadOnly Property PrimaryFieldNames() As String()
            Get
                Return m_PrimaryFields
            End Get
        End Property

        Public Function IsPrimaryKey(ByVal fieldName As String) As Boolean
            Dim iKeys As IEnumerable(Of String) = From s In m_PrimaryFields Where s.ToString().Equals(fieldName) Select s
            Return IIf(iKeys.Count() > 0, True, False)
        End Function

        Public ReadOnly Property IdentityFields()
            Get
                Return m_IdentityFields
            End Get
        End Property

        Public Function IsIdentityField(ByVal fieldName As String) As Boolean
            Dim iKeys As IEnumerable(Of String) = From s In m_IdentityFields Where s.ToString().Equals(fieldName) Select s
            Return IIf(iKeys.Count() > 0, True, False)
        End Function

        Public ReadOnly Property LoadFromDataRow() As Boolean
            Get
                Return m_LoadFromDataRow
            End Get
        End Property

        Public Sub New()
            FI_CompID.SetInitValue("")
            FI_EmpID.SetInitValue("")
            FI_ValidDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_Seq.SetInitValue(0)
            FI_Reason.SetInitValue("")
            FI_NewCompID.SetInitValue("")
            FI_DeptID.SetInitValue("")
            FI_OrganID.SetInitValue("")
            FI_RankID.SetInitValue("")
            FI_TitleID.SetInitValue("")
            FI_TitleName.SetInitValue("")
            FI_PositionID.SetInitValue("")
            FI_WorkTypeID.SetInitValue("")
            FI_GroupID.SetInitValue("")
            FI_GroupOrganID.SetInitValue("")
            FI_IsBoss.SetInitValue("0")
            FI_IsSecBoss.SetInitValue("0")
            FI_IsGroupBoss.SetInitValue("0")
            FI_IsSecGroupBoss.SetInitValue("0")
            FI_BossType.SetInitValue("")
            FI_Remark.SetInitValue("")
            FI_ValidMark.SetInitValue("0")
            FI_FlowOrganID.SetInitValue("")
            FI_WorkSiteID.SetInitValue("")
            FI_ApplyID.SetInitValue("")
            FI_ProcessDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_ApplyDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_DueDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_SalaryPaid.SetInitValue("")
            FI_QuitReason.SetInitValue("")
            FI_EvaluationMark.SetInitValue("0")
            FI_WTID.SetInitValue("")
            FI_ExistsEmployeeLog.SetInitValue("0")
            FI_RecID.SetInitValue("")
            FI_ContractDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_BindData.SetInitValue("0")
            FI_BusinessType.SetInitValue("")
            FI_EmpFlowRemarkID.SetInitValue("")
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CompID.SetInitValue(dr("CompID"))
            FI_EmpID.SetInitValue(dr("EmpID"))
            FI_ValidDate.SetInitValue(dr("ValidDate"))
            FI_Seq.SetInitValue(dr("Seq"))
            FI_Reason.SetInitValue(dr("Reason"))
            FI_NewCompID.SetInitValue(dr("NewCompID"))
            FI_DeptID.SetInitValue(dr("DeptID"))
            FI_OrganID.SetInitValue(dr("OrganID"))
            FI_RankID.SetInitValue(dr("RankID"))
            FI_TitleID.SetInitValue(dr("TitleID"))
            FI_TitleName.SetInitValue(dr("TitleName"))
            FI_PositionID.SetInitValue(dr("PositionID"))
            FI_WorkTypeID.SetInitValue(dr("WorkTypeID"))
            FI_GroupID.SetInitValue(dr("GroupID"))
            FI_GroupOrganID.SetInitValue(dr("GroupOrganID"))
            FI_IsBoss.SetInitValue(dr("IsBoss"))
            FI_IsSecBoss.SetInitValue(dr("IsSecBoss"))
            FI_IsGroupBoss.SetInitValue(dr("IsGroupBoss"))
            FI_IsSecGroupBoss.SetInitValue(dr("IsSecGroupBoss"))
            FI_BossType.SetInitValue(dr("BossType"))
            FI_Remark.SetInitValue(dr("Remark"))
            FI_ValidMark.SetInitValue(dr("ValidMark"))
            FI_FlowOrganID.SetInitValue(dr("FlowOrganID"))
            FI_WorkSiteID.SetInitValue(dr("WorkSiteID"))
            FI_ApplyID.SetInitValue(dr("ApplyID"))
            FI_ProcessDate.SetInitValue(dr("ProcessDate"))
            FI_ApplyDate.SetInitValue(dr("ApplyDate"))
            FI_DueDate.SetInitValue(dr("DueDate"))
            FI_SalaryPaid.SetInitValue(dr("SalaryPaid"))
            FI_QuitReason.SetInitValue(dr("QuitReason"))
            FI_EvaluationMark.SetInitValue(dr("EvaluationMark"))
            FI_WTID.SetInitValue(dr("WTID"))
            FI_ExistsEmployeeLog.SetInitValue(dr("ExistsEmployeeLog"))
            FI_RecID.SetInitValue(dr("RecID"))
            FI_ContractDate.SetInitValue(dr("ContractDate"))
            FI_BindData.SetInitValue(dr("BindData"))
            FI_BusinessType.SetInitValue(dr("BusinessType"))
            FI_EmpFlowRemarkID.SetInitValue(dr("EmpFlowRemarkID"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CompID.Updated = False
            FI_EmpID.Updated = False
            FI_ValidDate.Updated = False
            FI_Seq.Updated = False
            FI_Reason.Updated = False
            FI_NewCompID.Updated = False
            FI_DeptID.Updated = False
            FI_OrganID.Updated = False
            FI_RankID.Updated = False
            FI_TitleID.Updated = False
            FI_TitleName.Updated = False
            FI_PositionID.Updated = False
            FI_WorkTypeID.Updated = False
            FI_GroupID.Updated = False
            FI_GroupOrganID.Updated = False
            FI_IsBoss.Updated = False
            FI_IsSecBoss.Updated = False
            FI_IsGroupBoss.Updated = False
            FI_IsSecGroupBoss.Updated = False
            FI_BossType.Updated = False
            FI_Remark.Updated = False
            FI_ValidMark.Updated = False
            FI_FlowOrganID.Updated = False
            FI_WorkSiteID.Updated = False
            FI_ApplyID.Updated = False
            FI_ProcessDate.Updated = False
            FI_ApplyDate.Updated = False
            FI_DueDate.Updated = False
            FI_SalaryPaid.Updated = False
            FI_QuitReason.Updated = False
            FI_EvaluationMark.Updated = False
            FI_WTID.Updated = False
            FI_ExistsEmployeeLog.Updated = False
            FI_RecID.Updated = False
            FI_ContractDate.Updated = False
            FI_BindData.Updated = False
            FI_BusinessType.Updated = False
            FI_EmpFlowRemarkID.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
        End Sub

        Public ReadOnly Property CompID As Field(Of String) 
            Get
                Return FI_CompID
            End Get
        End Property

        Public ReadOnly Property EmpID As Field(Of String) 
            Get
                Return FI_EmpID
            End Get
        End Property

        Public ReadOnly Property ValidDate As Field(Of Date) 
            Get
                Return FI_ValidDate
            End Get
        End Property

        Public ReadOnly Property Seq As Field(Of Integer) 
            Get
                Return FI_Seq
            End Get
        End Property

        Public ReadOnly Property Reason As Field(Of String) 
            Get
                Return FI_Reason
            End Get
        End Property

        Public ReadOnly Property NewCompID As Field(Of String) 
            Get
                Return FI_NewCompID
            End Get
        End Property

        Public ReadOnly Property DeptID As Field(Of String) 
            Get
                Return FI_DeptID
            End Get
        End Property

        Public ReadOnly Property OrganID As Field(Of String) 
            Get
                Return FI_OrganID
            End Get
        End Property

        Public ReadOnly Property RankID As Field(Of String) 
            Get
                Return FI_RankID
            End Get
        End Property

        Public ReadOnly Property TitleID As Field(Of String) 
            Get
                Return FI_TitleID
            End Get
        End Property

        Public ReadOnly Property TitleName As Field(Of String) 
            Get
                Return FI_TitleName
            End Get
        End Property

        Public ReadOnly Property PositionID As Field(Of String) 
            Get
                Return FI_PositionID
            End Get
        End Property

        Public ReadOnly Property WorkTypeID As Field(Of String) 
            Get
                Return FI_WorkTypeID
            End Get
        End Property

        Public ReadOnly Property GroupID As Field(Of String) 
            Get
                Return FI_GroupID
            End Get
        End Property

        Public ReadOnly Property GroupOrganID As Field(Of String) 
            Get
                Return FI_GroupOrganID
            End Get
        End Property

        Public ReadOnly Property IsBoss As Field(Of String) 
            Get
                Return FI_IsBoss
            End Get
        End Property

        Public ReadOnly Property IsSecBoss As Field(Of String) 
            Get
                Return FI_IsSecBoss
            End Get
        End Property

        Public ReadOnly Property IsGroupBoss As Field(Of String) 
            Get
                Return FI_IsGroupBoss
            End Get
        End Property

        Public ReadOnly Property IsSecGroupBoss As Field(Of String) 
            Get
                Return FI_IsSecGroupBoss
            End Get
        End Property

        Public ReadOnly Property BossType As Field(Of String) 
            Get
                Return FI_BossType
            End Get
        End Property

        Public ReadOnly Property Remark As Field(Of String) 
            Get
                Return FI_Remark
            End Get
        End Property

        Public ReadOnly Property ValidMark As Field(Of String) 
            Get
                Return FI_ValidMark
            End Get
        End Property

        Public ReadOnly Property FlowOrganID As Field(Of String) 
            Get
                Return FI_FlowOrganID
            End Get
        End Property

        Public ReadOnly Property WorkSiteID As Field(Of String) 
            Get
                Return FI_WorkSiteID
            End Get
        End Property

        Public ReadOnly Property ApplyID As Field(Of String) 
            Get
                Return FI_ApplyID
            End Get
        End Property

        Public ReadOnly Property ProcessDate As Field(Of Date) 
            Get
                Return FI_ProcessDate
            End Get
        End Property

        Public ReadOnly Property ApplyDate As Field(Of Date) 
            Get
                Return FI_ApplyDate
            End Get
        End Property

        Public ReadOnly Property DueDate As Field(Of Date) 
            Get
                Return FI_DueDate
            End Get
        End Property

        Public ReadOnly Property SalaryPaid As Field(Of String) 
            Get
                Return FI_SalaryPaid
            End Get
        End Property

        Public ReadOnly Property QuitReason As Field(Of String) 
            Get
                Return FI_QuitReason
            End Get
        End Property

        Public ReadOnly Property EvaluationMark As Field(Of String) 
            Get
                Return FI_EvaluationMark
            End Get
        End Property

        Public ReadOnly Property WTID As Field(Of String) 
            Get
                Return FI_WTID
            End Get
        End Property

        Public ReadOnly Property ExistsEmployeeLog As Field(Of String) 
            Get
                Return FI_ExistsEmployeeLog
            End Get
        End Property

        Public ReadOnly Property RecID As Field(Of String) 
            Get
                Return FI_RecID
            End Get
        End Property

        Public ReadOnly Property ContractDate As Field(Of Date) 
            Get
                Return FI_ContractDate
            End Get
        End Property

        Public ReadOnly Property BindData As Field(Of String) 
            Get
                Return FI_BindData
            End Get
        End Property

        Public ReadOnly Property BusinessType As Field(Of String) 
            Get
                Return FI_BusinessType
            End Get
        End Property

        Public ReadOnly Property EmpFlowRemarkID As Field(Of String) 
            Get
                Return FI_EmpFlowRemarkID
            End Get
        End Property

        Public ReadOnly Property LastChgComp As Field(Of String) 
            Get
                Return FI_LastChgComp
            End Get
        End Property

        Public ReadOnly Property LastChgID As Field(Of String) 
            Get
                Return FI_LastChgID
            End Get
        End Property

        Public ReadOnly Property LastChgDate As Field(Of Date) 
            Get
                Return FI_LastChgDate
            End Get
        End Property

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal EmployeeWaitRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From EmployeeWait")
            strSQL.AppendLine("Where EmpID = @EmpID")
            strSQL.AppendLine("And ValidDate = @ValidDate")
            strSQL.AppendLine("And Seq = @Seq")
            strSQL.AppendLine("And CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmployeeWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, EmployeeWaitRow.ValidDate.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EmployeeWaitRow.Seq.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmployeeWaitRow.CompID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal EmployeeWaitRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From EmployeeWait")
            strSQL.AppendLine("Where EmpID = @EmpID")
            strSQL.AppendLine("And ValidDate = @ValidDate")
            strSQL.AppendLine("And Seq = @Seq")
            strSQL.AppendLine("And CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmployeeWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, EmployeeWaitRow.ValidDate.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EmployeeWaitRow.Seq.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmployeeWaitRow.CompID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal EmployeeWaitRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From EmployeeWait")
            strSQL.AppendLine("Where EmpID = @EmpID")
            strSQL.AppendLine("And ValidDate = @ValidDate")
            strSQL.AppendLine("And Seq = @Seq")
            strSQL.AppendLine("And CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In EmployeeWaitRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, r.ValidDate.Value)
                        db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)

                        intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
                    Next
                    tran.Commit()
                    inTrans = false
                Catch ex As Exception
                    If inTrans Then tran.Rollback()
                    Throw
                Finally
                    tran.Dispose()
                    If cn.State = ConnectionState.Open Then cn.Close()
                End Try
            End Using
            Return intRowsAffected
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal EmployeeWaitRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From EmployeeWait")
            strSQL.AppendLine("Where EmpID = @EmpID")
            strSQL.AppendLine("And ValidDate = @ValidDate")
            strSQL.AppendLine("And Seq = @Seq")
            strSQL.AppendLine("And CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In EmployeeWaitRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, r.ValidDate.Value)
                db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal EmployeeWaitRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmployeeWait")
            strSQL.AppendLine("Where EmpID = @EmpID")
            strSQL.AppendLine("And ValidDate = @ValidDate")
            strSQL.AppendLine("And Seq = @Seq")
            strSQL.AppendLine("And CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmployeeWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, EmployeeWaitRow.ValidDate.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EmployeeWaitRow.Seq.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmployeeWaitRow.CompID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(EmployeeWaitRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmployeeWait")
            strSQL.AppendLine("Where EmpID = @EmpID")
            strSQL.AppendLine("And ValidDate = @ValidDate")
            strSQL.AppendLine("And Seq = @Seq")
            strSQL.AppendLine("And CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmployeeWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, EmployeeWaitRow.ValidDate.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EmployeeWaitRow.Seq.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmployeeWaitRow.CompID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal EmployeeWaitRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update EmployeeWait Set")
            For i As Integer = 0 To EmployeeWaitRow.FieldNames.Length - 1
                If Not EmployeeWaitRow.IsIdentityField(EmployeeWaitRow.FieldNames(i)) AndAlso EmployeeWaitRow.IsUpdated(EmployeeWaitRow.FieldNames(i)) AndAlso EmployeeWaitRow.CreateUpdateSQL(EmployeeWaitRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, EmployeeWaitRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where EmpID = @PKEmpID")
            strSQL.AppendLine("And ValidDate = @PKValidDate")
            strSQL.AppendLine("And Seq = @PKSeq")
            strSQL.AppendLine("And CompID = @PKCompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If EmployeeWaitRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, EmployeeWaitRow.CompID.Value)
            If EmployeeWaitRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmployeeWaitRow.EmpID.Value)
            If EmployeeWaitRow.ValidDate.Updated Then db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(EmployeeWaitRow.ValidDate.Value), Convert.ToDateTime("1900/1/1"), EmployeeWaitRow.ValidDate.Value))
            If EmployeeWaitRow.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EmployeeWaitRow.Seq.Value)
            If EmployeeWaitRow.Reason.Updated Then db.AddInParameter(dbcmd, "@Reason", DbType.String, EmployeeWaitRow.Reason.Value)
            If EmployeeWaitRow.NewCompID.Updated Then db.AddInParameter(dbcmd, "@NewCompID", DbType.String, EmployeeWaitRow.NewCompID.Value)
            If EmployeeWaitRow.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, EmployeeWaitRow.DeptID.Value)
            If EmployeeWaitRow.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, EmployeeWaitRow.OrganID.Value)
            If EmployeeWaitRow.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, EmployeeWaitRow.RankID.Value)
            If EmployeeWaitRow.TitleID.Updated Then db.AddInParameter(dbcmd, "@TitleID", DbType.String, EmployeeWaitRow.TitleID.Value)
            If EmployeeWaitRow.TitleName.Updated Then db.AddInParameter(dbcmd, "@TitleName", DbType.String, EmployeeWaitRow.TitleName.Value)
            If EmployeeWaitRow.PositionID.Updated Then db.AddInParameter(dbcmd, "@PositionID", DbType.String, EmployeeWaitRow.PositionID.Value)
            If EmployeeWaitRow.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, EmployeeWaitRow.WorkTypeID.Value)
            If EmployeeWaitRow.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, EmployeeWaitRow.GroupID.Value)
            If EmployeeWaitRow.GroupOrganID.Updated Then db.AddInParameter(dbcmd, "@GroupOrganID", DbType.String, EmployeeWaitRow.GroupOrganID.Value)
            If EmployeeWaitRow.IsBoss.Updated Then db.AddInParameter(dbcmd, "@IsBoss", DbType.String, EmployeeWaitRow.IsBoss.Value)
            If EmployeeWaitRow.IsSecBoss.Updated Then db.AddInParameter(dbcmd, "@IsSecBoss", DbType.String, EmployeeWaitRow.IsSecBoss.Value)
            If EmployeeWaitRow.IsGroupBoss.Updated Then db.AddInParameter(dbcmd, "@IsGroupBoss", DbType.String, EmployeeWaitRow.IsGroupBoss.Value)
            If EmployeeWaitRow.IsSecGroupBoss.Updated Then db.AddInParameter(dbcmd, "@IsSecGroupBoss", DbType.String, EmployeeWaitRow.IsSecGroupBoss.Value)
            If EmployeeWaitRow.BossType.Updated Then db.AddInParameter(dbcmd, "@BossType", DbType.String, EmployeeWaitRow.BossType.Value)
            If EmployeeWaitRow.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, EmployeeWaitRow.Remark.Value)
            If EmployeeWaitRow.ValidMark.Updated Then db.AddInParameter(dbcmd, "@ValidMark", DbType.String, EmployeeWaitRow.ValidMark.Value)
            If EmployeeWaitRow.FlowOrganID.Updated Then db.AddInParameter(dbcmd, "@FlowOrganID", DbType.String, EmployeeWaitRow.FlowOrganID.Value)
            If EmployeeWaitRow.WorkSiteID.Updated Then db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, EmployeeWaitRow.WorkSiteID.Value)
            If EmployeeWaitRow.ApplyID.Updated Then db.AddInParameter(dbcmd, "@ApplyID", DbType.String, EmployeeWaitRow.ApplyID.Value)
            If EmployeeWaitRow.ProcessDate.Updated Then db.AddInParameter(dbcmd, "@ProcessDate", DbType.Date, IIf(IsDateTimeNull(EmployeeWaitRow.ProcessDate.Value), Convert.ToDateTime("1900/1/1"), EmployeeWaitRow.ProcessDate.Value))
            If EmployeeWaitRow.ApplyDate.Updated Then db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, IIf(IsDateTimeNull(EmployeeWaitRow.ApplyDate.Value), Convert.ToDateTime("1900/1/1"), EmployeeWaitRow.ApplyDate.Value))
            If EmployeeWaitRow.DueDate.Updated Then db.AddInParameter(dbcmd, "@DueDate", DbType.Date, IIf(IsDateTimeNull(EmployeeWaitRow.DueDate.Value), Convert.ToDateTime("1900/1/1"), EmployeeWaitRow.DueDate.Value))
            If EmployeeWaitRow.SalaryPaid.Updated Then db.AddInParameter(dbcmd, "@SalaryPaid", DbType.String, EmployeeWaitRow.SalaryPaid.Value)
            If EmployeeWaitRow.QuitReason.Updated Then db.AddInParameter(dbcmd, "@QuitReason", DbType.String, EmployeeWaitRow.QuitReason.Value)
            If EmployeeWaitRow.EvaluationMark.Updated Then db.AddInParameter(dbcmd, "@EvaluationMark", DbType.String, EmployeeWaitRow.EvaluationMark.Value)
            If EmployeeWaitRow.WTID.Updated Then db.AddInParameter(dbcmd, "@WTID", DbType.String, EmployeeWaitRow.WTID.Value)
            If EmployeeWaitRow.ExistsEmployeeLog.Updated Then db.AddInParameter(dbcmd, "@ExistsEmployeeLog", DbType.String, EmployeeWaitRow.ExistsEmployeeLog.Value)
            If EmployeeWaitRow.RecID.Updated Then db.AddInParameter(dbcmd, "@RecID", DbType.String, EmployeeWaitRow.RecID.Value)
            If EmployeeWaitRow.ContractDate.Updated Then db.AddInParameter(dbcmd, "@ContractDate", DbType.Date, IIf(IsDateTimeNull(EmployeeWaitRow.ContractDate.Value), Convert.ToDateTime("1900/1/1"), EmployeeWaitRow.ContractDate.Value))
            If EmployeeWaitRow.BindData.Updated Then db.AddInParameter(dbcmd, "@BindData", DbType.String, EmployeeWaitRow.BindData.Value)
            If EmployeeWaitRow.BusinessType.Updated Then db.AddInParameter(dbcmd, "@BusinessType", DbType.String, EmployeeWaitRow.BusinessType.Value)
            If EmployeeWaitRow.EmpFlowRemarkID.Updated Then db.AddInParameter(dbcmd, "@EmpFlowRemarkID", DbType.String, EmployeeWaitRow.EmpFlowRemarkID.Value)
            If EmployeeWaitRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmployeeWaitRow.LastChgComp.Value)
            If EmployeeWaitRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmployeeWaitRow.LastChgID.Value)
            If EmployeeWaitRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmployeeWaitRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmployeeWaitRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(EmployeeWaitRow.LoadFromDataRow, EmployeeWaitRow.EmpID.OldValue, EmployeeWaitRow.EmpID.Value))
            db.AddInParameter(dbcmd, "@PKValidDate", DbType.Date, IIf(EmployeeWaitRow.LoadFromDataRow, EmployeeWaitRow.ValidDate.OldValue, EmployeeWaitRow.ValidDate.Value))
            db.AddInParameter(dbcmd, "@PKSeq", DbType.Int32, IIf(EmployeeWaitRow.LoadFromDataRow, EmployeeWaitRow.Seq.OldValue, EmployeeWaitRow.Seq.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(EmployeeWaitRow.LoadFromDataRow, EmployeeWaitRow.CompID.OldValue, EmployeeWaitRow.CompID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal EmployeeWaitRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update EmployeeWait Set")
            For i As Integer = 0 To EmployeeWaitRow.FieldNames.Length - 1
                If Not EmployeeWaitRow.IsIdentityField(EmployeeWaitRow.FieldNames(i)) AndAlso EmployeeWaitRow.IsUpdated(EmployeeWaitRow.FieldNames(i)) AndAlso EmployeeWaitRow.CreateUpdateSQL(EmployeeWaitRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, EmployeeWaitRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where EmpID = @PKEmpID")
            strSQL.AppendLine("And ValidDate = @PKValidDate")
            strSQL.AppendLine("And Seq = @PKSeq")
            strSQL.AppendLine("And CompID = @PKCompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If EmployeeWaitRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, EmployeeWaitRow.CompID.Value)
            If EmployeeWaitRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmployeeWaitRow.EmpID.Value)
            If EmployeeWaitRow.ValidDate.Updated Then db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(EmployeeWaitRow.ValidDate.Value), Convert.ToDateTime("1900/1/1"), EmployeeWaitRow.ValidDate.Value))
            If EmployeeWaitRow.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EmployeeWaitRow.Seq.Value)
            If EmployeeWaitRow.Reason.Updated Then db.AddInParameter(dbcmd, "@Reason", DbType.String, EmployeeWaitRow.Reason.Value)
            If EmployeeWaitRow.NewCompID.Updated Then db.AddInParameter(dbcmd, "@NewCompID", DbType.String, EmployeeWaitRow.NewCompID.Value)
            If EmployeeWaitRow.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, EmployeeWaitRow.DeptID.Value)
            If EmployeeWaitRow.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, EmployeeWaitRow.OrganID.Value)
            If EmployeeWaitRow.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, EmployeeWaitRow.RankID.Value)
            If EmployeeWaitRow.TitleID.Updated Then db.AddInParameter(dbcmd, "@TitleID", DbType.String, EmployeeWaitRow.TitleID.Value)
            If EmployeeWaitRow.TitleName.Updated Then db.AddInParameter(dbcmd, "@TitleName", DbType.String, EmployeeWaitRow.TitleName.Value)
            If EmployeeWaitRow.PositionID.Updated Then db.AddInParameter(dbcmd, "@PositionID", DbType.String, EmployeeWaitRow.PositionID.Value)
            If EmployeeWaitRow.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, EmployeeWaitRow.WorkTypeID.Value)
            If EmployeeWaitRow.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, EmployeeWaitRow.GroupID.Value)
            If EmployeeWaitRow.GroupOrganID.Updated Then db.AddInParameter(dbcmd, "@GroupOrganID", DbType.String, EmployeeWaitRow.GroupOrganID.Value)
            If EmployeeWaitRow.IsBoss.Updated Then db.AddInParameter(dbcmd, "@IsBoss", DbType.String, EmployeeWaitRow.IsBoss.Value)
            If EmployeeWaitRow.IsSecBoss.Updated Then db.AddInParameter(dbcmd, "@IsSecBoss", DbType.String, EmployeeWaitRow.IsSecBoss.Value)
            If EmployeeWaitRow.IsGroupBoss.Updated Then db.AddInParameter(dbcmd, "@IsGroupBoss", DbType.String, EmployeeWaitRow.IsGroupBoss.Value)
            If EmployeeWaitRow.IsSecGroupBoss.Updated Then db.AddInParameter(dbcmd, "@IsSecGroupBoss", DbType.String, EmployeeWaitRow.IsSecGroupBoss.Value)
            If EmployeeWaitRow.BossType.Updated Then db.AddInParameter(dbcmd, "@BossType", DbType.String, EmployeeWaitRow.BossType.Value)
            If EmployeeWaitRow.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, EmployeeWaitRow.Remark.Value)
            If EmployeeWaitRow.ValidMark.Updated Then db.AddInParameter(dbcmd, "@ValidMark", DbType.String, EmployeeWaitRow.ValidMark.Value)
            If EmployeeWaitRow.FlowOrganID.Updated Then db.AddInParameter(dbcmd, "@FlowOrganID", DbType.String, EmployeeWaitRow.FlowOrganID.Value)
            If EmployeeWaitRow.WorkSiteID.Updated Then db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, EmployeeWaitRow.WorkSiteID.Value)
            If EmployeeWaitRow.ApplyID.Updated Then db.AddInParameter(dbcmd, "@ApplyID", DbType.String, EmployeeWaitRow.ApplyID.Value)
            If EmployeeWaitRow.ProcessDate.Updated Then db.AddInParameter(dbcmd, "@ProcessDate", DbType.Date, IIf(IsDateTimeNull(EmployeeWaitRow.ProcessDate.Value), Convert.ToDateTime("1900/1/1"), EmployeeWaitRow.ProcessDate.Value))
            If EmployeeWaitRow.ApplyDate.Updated Then db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, IIf(IsDateTimeNull(EmployeeWaitRow.ApplyDate.Value), Convert.ToDateTime("1900/1/1"), EmployeeWaitRow.ApplyDate.Value))
            If EmployeeWaitRow.DueDate.Updated Then db.AddInParameter(dbcmd, "@DueDate", DbType.Date, IIf(IsDateTimeNull(EmployeeWaitRow.DueDate.Value), Convert.ToDateTime("1900/1/1"), EmployeeWaitRow.DueDate.Value))
            If EmployeeWaitRow.SalaryPaid.Updated Then db.AddInParameter(dbcmd, "@SalaryPaid", DbType.String, EmployeeWaitRow.SalaryPaid.Value)
            If EmployeeWaitRow.QuitReason.Updated Then db.AddInParameter(dbcmd, "@QuitReason", DbType.String, EmployeeWaitRow.QuitReason.Value)
            If EmployeeWaitRow.EvaluationMark.Updated Then db.AddInParameter(dbcmd, "@EvaluationMark", DbType.String, EmployeeWaitRow.EvaluationMark.Value)
            If EmployeeWaitRow.WTID.Updated Then db.AddInParameter(dbcmd, "@WTID", DbType.String, EmployeeWaitRow.WTID.Value)
            If EmployeeWaitRow.ExistsEmployeeLog.Updated Then db.AddInParameter(dbcmd, "@ExistsEmployeeLog", DbType.String, EmployeeWaitRow.ExistsEmployeeLog.Value)
            If EmployeeWaitRow.RecID.Updated Then db.AddInParameter(dbcmd, "@RecID", DbType.String, EmployeeWaitRow.RecID.Value)
            If EmployeeWaitRow.ContractDate.Updated Then db.AddInParameter(dbcmd, "@ContractDate", DbType.Date, IIf(IsDateTimeNull(EmployeeWaitRow.ContractDate.Value), Convert.ToDateTime("1900/1/1"), EmployeeWaitRow.ContractDate.Value))
            If EmployeeWaitRow.BindData.Updated Then db.AddInParameter(dbcmd, "@BindData", DbType.String, EmployeeWaitRow.BindData.Value)
            If EmployeeWaitRow.BusinessType.Updated Then db.AddInParameter(dbcmd, "@BusinessType", DbType.String, EmployeeWaitRow.BusinessType.Value)
            If EmployeeWaitRow.EmpFlowRemarkID.Updated Then db.AddInParameter(dbcmd, "@EmpFlowRemarkID", DbType.String, EmployeeWaitRow.EmpFlowRemarkID.Value)
            If EmployeeWaitRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmployeeWaitRow.LastChgComp.Value)
            If EmployeeWaitRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmployeeWaitRow.LastChgID.Value)
            If EmployeeWaitRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmployeeWaitRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmployeeWaitRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(EmployeeWaitRow.LoadFromDataRow, EmployeeWaitRow.EmpID.OldValue, EmployeeWaitRow.EmpID.Value))
            db.AddInParameter(dbcmd, "@PKValidDate", DbType.Date, IIf(EmployeeWaitRow.LoadFromDataRow, EmployeeWaitRow.ValidDate.OldValue, EmployeeWaitRow.ValidDate.Value))
            db.AddInParameter(dbcmd, "@PKSeq", DbType.Int32, IIf(EmployeeWaitRow.LoadFromDataRow, EmployeeWaitRow.Seq.OldValue, EmployeeWaitRow.Seq.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(EmployeeWaitRow.LoadFromDataRow, EmployeeWaitRow.CompID.OldValue, EmployeeWaitRow.CompID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal EmployeeWaitRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In EmployeeWaitRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update EmployeeWait Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where EmpID = @PKEmpID")
                        strSQL.AppendLine("And ValidDate = @PKValidDate")
                        strSQL.AppendLine("And Seq = @PKSeq")
                        strSQL.AppendLine("And CompID = @PKCompID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        If r.ValidDate.Updated Then db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(r.ValidDate.Value), Convert.ToDateTime("1900/1/1"), r.ValidDate.Value))
                        If r.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                        If r.Reason.Updated Then db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                        If r.NewCompID.Updated Then db.AddInParameter(dbcmd, "@NewCompID", DbType.String, r.NewCompID.Value)
                        If r.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                        If r.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                        If r.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                        If r.TitleID.Updated Then db.AddInParameter(dbcmd, "@TitleID", DbType.String, r.TitleID.Value)
                        If r.TitleName.Updated Then db.AddInParameter(dbcmd, "@TitleName", DbType.String, r.TitleName.Value)
                        If r.PositionID.Updated Then db.AddInParameter(dbcmd, "@PositionID", DbType.String, r.PositionID.Value)
                        If r.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                        If r.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                        If r.GroupOrganID.Updated Then db.AddInParameter(dbcmd, "@GroupOrganID", DbType.String, r.GroupOrganID.Value)
                        If r.IsBoss.Updated Then db.AddInParameter(dbcmd, "@IsBoss", DbType.String, r.IsBoss.Value)
                        If r.IsSecBoss.Updated Then db.AddInParameter(dbcmd, "@IsSecBoss", DbType.String, r.IsSecBoss.Value)
                        If r.IsGroupBoss.Updated Then db.AddInParameter(dbcmd, "@IsGroupBoss", DbType.String, r.IsGroupBoss.Value)
                        If r.IsSecGroupBoss.Updated Then db.AddInParameter(dbcmd, "@IsSecGroupBoss", DbType.String, r.IsSecGroupBoss.Value)
                        If r.BossType.Updated Then db.AddInParameter(dbcmd, "@BossType", DbType.String, r.BossType.Value)
                        If r.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                        If r.ValidMark.Updated Then db.AddInParameter(dbcmd, "@ValidMark", DbType.String, r.ValidMark.Value)
                        If r.FlowOrganID.Updated Then db.AddInParameter(dbcmd, "@FlowOrganID", DbType.String, r.FlowOrganID.Value)
                        If r.WorkSiteID.Updated Then db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, r.WorkSiteID.Value)
                        If r.ApplyID.Updated Then db.AddInParameter(dbcmd, "@ApplyID", DbType.String, r.ApplyID.Value)
                        If r.ProcessDate.Updated Then db.AddInParameter(dbcmd, "@ProcessDate", DbType.Date, IIf(IsDateTimeNull(r.ProcessDate.Value), Convert.ToDateTime("1900/1/1"), r.ProcessDate.Value))
                        If r.ApplyDate.Updated Then db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, IIf(IsDateTimeNull(r.ApplyDate.Value), Convert.ToDateTime("1900/1/1"), r.ApplyDate.Value))
                        If r.DueDate.Updated Then db.AddInParameter(dbcmd, "@DueDate", DbType.Date, IIf(IsDateTimeNull(r.DueDate.Value), Convert.ToDateTime("1900/1/1"), r.DueDate.Value))
                        If r.SalaryPaid.Updated Then db.AddInParameter(dbcmd, "@SalaryPaid", DbType.String, r.SalaryPaid.Value)
                        If r.QuitReason.Updated Then db.AddInParameter(dbcmd, "@QuitReason", DbType.String, r.QuitReason.Value)
                        If r.EvaluationMark.Updated Then db.AddInParameter(dbcmd, "@EvaluationMark", DbType.String, r.EvaluationMark.Value)
                        If r.WTID.Updated Then db.AddInParameter(dbcmd, "@WTID", DbType.String, r.WTID.Value)
                        If r.ExistsEmployeeLog.Updated Then db.AddInParameter(dbcmd, "@ExistsEmployeeLog", DbType.String, r.ExistsEmployeeLog.Value)
                        If r.RecID.Updated Then db.AddInParameter(dbcmd, "@RecID", DbType.String, r.RecID.Value)
                        If r.ContractDate.Updated Then db.AddInParameter(dbcmd, "@ContractDate", DbType.Date, IIf(IsDateTimeNull(r.ContractDate.Value), Convert.ToDateTime("1900/1/1"), r.ContractDate.Value))
                        If r.BindData.Updated Then db.AddInParameter(dbcmd, "@BindData", DbType.String, r.BindData.Value)
                        If r.BusinessType.Updated Then db.AddInParameter(dbcmd, "@BusinessType", DbType.String, r.BusinessType.Value)
                        If r.EmpFlowRemarkID.Updated Then db.AddInParameter(dbcmd, "@EmpFlowRemarkID", DbType.String, r.EmpFlowRemarkID.Value)
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))
                        db.AddInParameter(dbcmd, "@PKValidDate", DbType.Date, IIf(r.LoadFromDataRow, r.ValidDate.OldValue, r.ValidDate.Value))
                        db.AddInParameter(dbcmd, "@PKSeq", DbType.Int32, IIf(r.LoadFromDataRow, r.Seq.OldValue, r.Seq.Value))
                        db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))

                        intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
                    Next
                    tran.Commit()
                Catch ex As Exception
                    If inTrans Then tran.Rollback()
                    Throw
                Finally
                    tran.Dispose()
                    If cn.State = ConnectionState.Open Then cn.Close()
                End Try
            End Using
            Return intRowsAffected
        End Function

        Public Function Update(ByVal EmployeeWaitRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In EmployeeWaitRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update EmployeeWait Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where EmpID = @PKEmpID")
                strSQL.AppendLine("And ValidDate = @PKValidDate")
                strSQL.AppendLine("And Seq = @PKSeq")
                strSQL.AppendLine("And CompID = @PKCompID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                If r.ValidDate.Updated Then db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(r.ValidDate.Value), Convert.ToDateTime("1900/1/1"), r.ValidDate.Value))
                If r.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                If r.Reason.Updated Then db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                If r.NewCompID.Updated Then db.AddInParameter(dbcmd, "@NewCompID", DbType.String, r.NewCompID.Value)
                If r.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                If r.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                If r.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                If r.TitleID.Updated Then db.AddInParameter(dbcmd, "@TitleID", DbType.String, r.TitleID.Value)
                If r.TitleName.Updated Then db.AddInParameter(dbcmd, "@TitleName", DbType.String, r.TitleName.Value)
                If r.PositionID.Updated Then db.AddInParameter(dbcmd, "@PositionID", DbType.String, r.PositionID.Value)
                If r.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                If r.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                If r.GroupOrganID.Updated Then db.AddInParameter(dbcmd, "@GroupOrganID", DbType.String, r.GroupOrganID.Value)
                If r.IsBoss.Updated Then db.AddInParameter(dbcmd, "@IsBoss", DbType.String, r.IsBoss.Value)
                If r.IsSecBoss.Updated Then db.AddInParameter(dbcmd, "@IsSecBoss", DbType.String, r.IsSecBoss.Value)
                If r.IsGroupBoss.Updated Then db.AddInParameter(dbcmd, "@IsGroupBoss", DbType.String, r.IsGroupBoss.Value)
                If r.IsSecGroupBoss.Updated Then db.AddInParameter(dbcmd, "@IsSecGroupBoss", DbType.String, r.IsSecGroupBoss.Value)
                If r.BossType.Updated Then db.AddInParameter(dbcmd, "@BossType", DbType.String, r.BossType.Value)
                If r.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                If r.ValidMark.Updated Then db.AddInParameter(dbcmd, "@ValidMark", DbType.String, r.ValidMark.Value)
                If r.FlowOrganID.Updated Then db.AddInParameter(dbcmd, "@FlowOrganID", DbType.String, r.FlowOrganID.Value)
                If r.WorkSiteID.Updated Then db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, r.WorkSiteID.Value)
                If r.ApplyID.Updated Then db.AddInParameter(dbcmd, "@ApplyID", DbType.String, r.ApplyID.Value)
                If r.ProcessDate.Updated Then db.AddInParameter(dbcmd, "@ProcessDate", DbType.Date, IIf(IsDateTimeNull(r.ProcessDate.Value), Convert.ToDateTime("1900/1/1"), r.ProcessDate.Value))
                If r.ApplyDate.Updated Then db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, IIf(IsDateTimeNull(r.ApplyDate.Value), Convert.ToDateTime("1900/1/1"), r.ApplyDate.Value))
                If r.DueDate.Updated Then db.AddInParameter(dbcmd, "@DueDate", DbType.Date, IIf(IsDateTimeNull(r.DueDate.Value), Convert.ToDateTime("1900/1/1"), r.DueDate.Value))
                If r.SalaryPaid.Updated Then db.AddInParameter(dbcmd, "@SalaryPaid", DbType.String, r.SalaryPaid.Value)
                If r.QuitReason.Updated Then db.AddInParameter(dbcmd, "@QuitReason", DbType.String, r.QuitReason.Value)
                If r.EvaluationMark.Updated Then db.AddInParameter(dbcmd, "@EvaluationMark", DbType.String, r.EvaluationMark.Value)
                If r.WTID.Updated Then db.AddInParameter(dbcmd, "@WTID", DbType.String, r.WTID.Value)
                If r.ExistsEmployeeLog.Updated Then db.AddInParameter(dbcmd, "@ExistsEmployeeLog", DbType.String, r.ExistsEmployeeLog.Value)
                If r.RecID.Updated Then db.AddInParameter(dbcmd, "@RecID", DbType.String, r.RecID.Value)
                If r.ContractDate.Updated Then db.AddInParameter(dbcmd, "@ContractDate", DbType.Date, IIf(IsDateTimeNull(r.ContractDate.Value), Convert.ToDateTime("1900/1/1"), r.ContractDate.Value))
                If r.BindData.Updated Then db.AddInParameter(dbcmd, "@BindData", DbType.String, r.BindData.Value)
                If r.BusinessType.Updated Then db.AddInParameter(dbcmd, "@BusinessType", DbType.String, r.BusinessType.Value)
                If r.EmpFlowRemarkID.Updated Then db.AddInParameter(dbcmd, "@EmpFlowRemarkID", DbType.String, r.EmpFlowRemarkID.Value)
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))
                db.AddInParameter(dbcmd, "@PKValidDate", DbType.Date, IIf(r.LoadFromDataRow, r.ValidDate.OldValue, r.ValidDate.Value))
                db.AddInParameter(dbcmd, "@PKSeq", DbType.Int32, IIf(r.LoadFromDataRow, r.Seq.OldValue, r.Seq.Value))
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal EmployeeWaitRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From EmployeeWait")
            strSQL.AppendLine("Where EmpID = @EmpID")
            strSQL.AppendLine("And ValidDate = @ValidDate")
            strSQL.AppendLine("And Seq = @Seq")
            strSQL.AppendLine("And CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmployeeWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, EmployeeWaitRow.ValidDate.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EmployeeWaitRow.Seq.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmployeeWaitRow.CompID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal EmployeeWaitRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From EmployeeWait")
            strSQL.AppendLine("Where EmpID = @EmpID")
            strSQL.AppendLine("And ValidDate = @ValidDate")
            strSQL.AppendLine("And Seq = @Seq")
            strSQL.AppendLine("And CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmployeeWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, EmployeeWaitRow.ValidDate.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EmployeeWaitRow.Seq.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmployeeWaitRow.CompID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmployeeWait")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal EmployeeWaitRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into EmployeeWait")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, ValidDate, Seq, Reason, NewCompID, DeptID, OrganID, RankID, TitleID,")
            strSQL.AppendLine("    TitleName, PositionID, WorkTypeID, GroupID, GroupOrganID, IsBoss, IsSecBoss, IsGroupBoss,")
            strSQL.AppendLine("    IsSecGroupBoss, BossType, Remark, ValidMark, FlowOrganID, WorkSiteID, ApplyID, ProcessDate,")
            strSQL.AppendLine("    ApplyDate, DueDate, SalaryPaid, QuitReason, EvaluationMark, WTID, ExistsEmployeeLog,")
            strSQL.AppendLine("    RecID, ContractDate, BindData, BusinessType, EmpFlowRemarkID, LastChgComp, LastChgID,")
            strSQL.AppendLine("    LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @ValidDate, @Seq, @Reason, @NewCompID, @DeptID, @OrganID, @RankID, @TitleID,")
            strSQL.AppendLine("    @TitleName, @PositionID, @WorkTypeID, @GroupID, @GroupOrganID, @IsBoss, @IsSecBoss, @IsGroupBoss,")
            strSQL.AppendLine("    @IsSecGroupBoss, @BossType, @Remark, @ValidMark, @FlowOrganID, @WorkSiteID, @ApplyID, @ProcessDate,")
            strSQL.AppendLine("    @ApplyDate, @DueDate, @SalaryPaid, @QuitReason, @EvaluationMark, @WTID, @ExistsEmployeeLog,")
            strSQL.AppendLine("    @RecID, @ContractDate, @BindData, @BusinessType, @EmpFlowRemarkID, @LastChgComp, @LastChgID,")
            strSQL.AppendLine("    @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmployeeWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmployeeWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(EmployeeWaitRow.ValidDate.Value), Convert.ToDateTime("1900/1/1"), EmployeeWaitRow.ValidDate.Value))
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EmployeeWaitRow.Seq.Value)
            db.AddInParameter(dbcmd, "@Reason", DbType.String, EmployeeWaitRow.Reason.Value)
            db.AddInParameter(dbcmd, "@NewCompID", DbType.String, EmployeeWaitRow.NewCompID.Value)
            db.AddInParameter(dbcmd, "@DeptID", DbType.String, EmployeeWaitRow.DeptID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, EmployeeWaitRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@RankID", DbType.String, EmployeeWaitRow.RankID.Value)
            db.AddInParameter(dbcmd, "@TitleID", DbType.String, EmployeeWaitRow.TitleID.Value)
            db.AddInParameter(dbcmd, "@TitleName", DbType.String, EmployeeWaitRow.TitleName.Value)
            db.AddInParameter(dbcmd, "@PositionID", DbType.String, EmployeeWaitRow.PositionID.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, EmployeeWaitRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@GroupID", DbType.String, EmployeeWaitRow.GroupID.Value)
            db.AddInParameter(dbcmd, "@GroupOrganID", DbType.String, EmployeeWaitRow.GroupOrganID.Value)
            db.AddInParameter(dbcmd, "@IsBoss", DbType.String, EmployeeWaitRow.IsBoss.Value)
            db.AddInParameter(dbcmd, "@IsSecBoss", DbType.String, EmployeeWaitRow.IsSecBoss.Value)
            db.AddInParameter(dbcmd, "@IsGroupBoss", DbType.String, EmployeeWaitRow.IsGroupBoss.Value)
            db.AddInParameter(dbcmd, "@IsSecGroupBoss", DbType.String, EmployeeWaitRow.IsSecGroupBoss.Value)
            db.AddInParameter(dbcmd, "@BossType", DbType.String, EmployeeWaitRow.BossType.Value)
            db.AddInParameter(dbcmd, "@Remark", DbType.String, EmployeeWaitRow.Remark.Value)
            db.AddInParameter(dbcmd, "@ValidMark", DbType.String, EmployeeWaitRow.ValidMark.Value)
            db.AddInParameter(dbcmd, "@FlowOrganID", DbType.String, EmployeeWaitRow.FlowOrganID.Value)
            db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, EmployeeWaitRow.WorkSiteID.Value)
            db.AddInParameter(dbcmd, "@ApplyID", DbType.String, EmployeeWaitRow.ApplyID.Value)
            db.AddInParameter(dbcmd, "@ProcessDate", DbType.Date, IIf(IsDateTimeNull(EmployeeWaitRow.ProcessDate.Value), Convert.ToDateTime("1900/1/1"), EmployeeWaitRow.ProcessDate.Value))
            db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, IIf(IsDateTimeNull(EmployeeWaitRow.ApplyDate.Value), Convert.ToDateTime("1900/1/1"), EmployeeWaitRow.ApplyDate.Value))
            db.AddInParameter(dbcmd, "@DueDate", DbType.Date, IIf(IsDateTimeNull(EmployeeWaitRow.DueDate.Value), Convert.ToDateTime("1900/1/1"), EmployeeWaitRow.DueDate.Value))
            db.AddInParameter(dbcmd, "@SalaryPaid", DbType.String, EmployeeWaitRow.SalaryPaid.Value)
            db.AddInParameter(dbcmd, "@QuitReason", DbType.String, EmployeeWaitRow.QuitReason.Value)
            db.AddInParameter(dbcmd, "@EvaluationMark", DbType.String, EmployeeWaitRow.EvaluationMark.Value)
            db.AddInParameter(dbcmd, "@WTID", DbType.String, EmployeeWaitRow.WTID.Value)
            db.AddInParameter(dbcmd, "@ExistsEmployeeLog", DbType.String, EmployeeWaitRow.ExistsEmployeeLog.Value)
            db.AddInParameter(dbcmd, "@RecID", DbType.String, EmployeeWaitRow.RecID.Value)
            db.AddInParameter(dbcmd, "@ContractDate", DbType.Date, IIf(IsDateTimeNull(EmployeeWaitRow.ContractDate.Value), Convert.ToDateTime("1900/1/1"), EmployeeWaitRow.ContractDate.Value))
            db.AddInParameter(dbcmd, "@BindData", DbType.String, EmployeeWaitRow.BindData.Value)
            db.AddInParameter(dbcmd, "@BusinessType", DbType.String, EmployeeWaitRow.BusinessType.Value)
            db.AddInParameter(dbcmd, "@EmpFlowRemarkID", DbType.String, EmployeeWaitRow.EmpFlowRemarkID.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmployeeWaitRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmployeeWaitRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmployeeWaitRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmployeeWaitRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal EmployeeWaitRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into EmployeeWait")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, ValidDate, Seq, Reason, NewCompID, DeptID, OrganID, RankID, TitleID,")
            strSQL.AppendLine("    TitleName, PositionID, WorkTypeID, GroupID, GroupOrganID, IsBoss, IsSecBoss, IsGroupBoss,")
            strSQL.AppendLine("    IsSecGroupBoss, BossType, Remark, ValidMark, FlowOrganID, WorkSiteID, ApplyID, ProcessDate,")
            strSQL.AppendLine("    ApplyDate, DueDate, SalaryPaid, QuitReason, EvaluationMark, WTID, ExistsEmployeeLog,")
            strSQL.AppendLine("    RecID, ContractDate, BindData, BusinessType, EmpFlowRemarkID, LastChgComp, LastChgID,")
            strSQL.AppendLine("    LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @ValidDate, @Seq, @Reason, @NewCompID, @DeptID, @OrganID, @RankID, @TitleID,")
            strSQL.AppendLine("    @TitleName, @PositionID, @WorkTypeID, @GroupID, @GroupOrganID, @IsBoss, @IsSecBoss, @IsGroupBoss,")
            strSQL.AppendLine("    @IsSecGroupBoss, @BossType, @Remark, @ValidMark, @FlowOrganID, @WorkSiteID, @ApplyID, @ProcessDate,")
            strSQL.AppendLine("    @ApplyDate, @DueDate, @SalaryPaid, @QuitReason, @EvaluationMark, @WTID, @ExistsEmployeeLog,")
            strSQL.AppendLine("    @RecID, @ContractDate, @BindData, @BusinessType, @EmpFlowRemarkID, @LastChgComp, @LastChgID,")
            strSQL.AppendLine("    @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmployeeWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmployeeWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(EmployeeWaitRow.ValidDate.Value), Convert.ToDateTime("1900/1/1"), EmployeeWaitRow.ValidDate.Value))
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EmployeeWaitRow.Seq.Value)
            db.AddInParameter(dbcmd, "@Reason", DbType.String, EmployeeWaitRow.Reason.Value)
            db.AddInParameter(dbcmd, "@NewCompID", DbType.String, EmployeeWaitRow.NewCompID.Value)
            db.AddInParameter(dbcmd, "@DeptID", DbType.String, EmployeeWaitRow.DeptID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, EmployeeWaitRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@RankID", DbType.String, EmployeeWaitRow.RankID.Value)
            db.AddInParameter(dbcmd, "@TitleID", DbType.String, EmployeeWaitRow.TitleID.Value)
            db.AddInParameter(dbcmd, "@TitleName", DbType.String, EmployeeWaitRow.TitleName.Value)
            db.AddInParameter(dbcmd, "@PositionID", DbType.String, EmployeeWaitRow.PositionID.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, EmployeeWaitRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@GroupID", DbType.String, EmployeeWaitRow.GroupID.Value)
            db.AddInParameter(dbcmd, "@GroupOrganID", DbType.String, EmployeeWaitRow.GroupOrganID.Value)
            db.AddInParameter(dbcmd, "@IsBoss", DbType.String, EmployeeWaitRow.IsBoss.Value)
            db.AddInParameter(dbcmd, "@IsSecBoss", DbType.String, EmployeeWaitRow.IsSecBoss.Value)
            db.AddInParameter(dbcmd, "@IsGroupBoss", DbType.String, EmployeeWaitRow.IsGroupBoss.Value)
            db.AddInParameter(dbcmd, "@IsSecGroupBoss", DbType.String, EmployeeWaitRow.IsSecGroupBoss.Value)
            db.AddInParameter(dbcmd, "@BossType", DbType.String, EmployeeWaitRow.BossType.Value)
            db.AddInParameter(dbcmd, "@Remark", DbType.String, EmployeeWaitRow.Remark.Value)
            db.AddInParameter(dbcmd, "@ValidMark", DbType.String, EmployeeWaitRow.ValidMark.Value)
            db.AddInParameter(dbcmd, "@FlowOrganID", DbType.String, EmployeeWaitRow.FlowOrganID.Value)
            db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, EmployeeWaitRow.WorkSiteID.Value)
            db.AddInParameter(dbcmd, "@ApplyID", DbType.String, EmployeeWaitRow.ApplyID.Value)
            db.AddInParameter(dbcmd, "@ProcessDate", DbType.Date, IIf(IsDateTimeNull(EmployeeWaitRow.ProcessDate.Value), Convert.ToDateTime("1900/1/1"), EmployeeWaitRow.ProcessDate.Value))
            db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, IIf(IsDateTimeNull(EmployeeWaitRow.ApplyDate.Value), Convert.ToDateTime("1900/1/1"), EmployeeWaitRow.ApplyDate.Value))
            db.AddInParameter(dbcmd, "@DueDate", DbType.Date, IIf(IsDateTimeNull(EmployeeWaitRow.DueDate.Value), Convert.ToDateTime("1900/1/1"), EmployeeWaitRow.DueDate.Value))
            db.AddInParameter(dbcmd, "@SalaryPaid", DbType.String, EmployeeWaitRow.SalaryPaid.Value)
            db.AddInParameter(dbcmd, "@QuitReason", DbType.String, EmployeeWaitRow.QuitReason.Value)
            db.AddInParameter(dbcmd, "@EvaluationMark", DbType.String, EmployeeWaitRow.EvaluationMark.Value)
            db.AddInParameter(dbcmd, "@WTID", DbType.String, EmployeeWaitRow.WTID.Value)
            db.AddInParameter(dbcmd, "@ExistsEmployeeLog", DbType.String, EmployeeWaitRow.ExistsEmployeeLog.Value)
            db.AddInParameter(dbcmd, "@RecID", DbType.String, EmployeeWaitRow.RecID.Value)
            db.AddInParameter(dbcmd, "@ContractDate", DbType.Date, IIf(IsDateTimeNull(EmployeeWaitRow.ContractDate.Value), Convert.ToDateTime("1900/1/1"), EmployeeWaitRow.ContractDate.Value))
            db.AddInParameter(dbcmd, "@BindData", DbType.String, EmployeeWaitRow.BindData.Value)
            db.AddInParameter(dbcmd, "@BusinessType", DbType.String, EmployeeWaitRow.BusinessType.Value)
            db.AddInParameter(dbcmd, "@EmpFlowRemarkID", DbType.String, EmployeeWaitRow.EmpFlowRemarkID.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmployeeWaitRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmployeeWaitRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmployeeWaitRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmployeeWaitRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal EmployeeWaitRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into EmployeeWait")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, ValidDate, Seq, Reason, NewCompID, DeptID, OrganID, RankID, TitleID,")
            strSQL.AppendLine("    TitleName, PositionID, WorkTypeID, GroupID, GroupOrganID, IsBoss, IsSecBoss, IsGroupBoss,")
            strSQL.AppendLine("    IsSecGroupBoss, BossType, Remark, ValidMark, FlowOrganID, WorkSiteID, ApplyID, ProcessDate,")
            strSQL.AppendLine("    ApplyDate, DueDate, SalaryPaid, QuitReason, EvaluationMark, WTID, ExistsEmployeeLog,")
            strSQL.AppendLine("    RecID, ContractDate, BindData, BusinessType, EmpFlowRemarkID, LastChgComp, LastChgID,")
            strSQL.AppendLine("    LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @ValidDate, @Seq, @Reason, @NewCompID, @DeptID, @OrganID, @RankID, @TitleID,")
            strSQL.AppendLine("    @TitleName, @PositionID, @WorkTypeID, @GroupID, @GroupOrganID, @IsBoss, @IsSecBoss, @IsGroupBoss,")
            strSQL.AppendLine("    @IsSecGroupBoss, @BossType, @Remark, @ValidMark, @FlowOrganID, @WorkSiteID, @ApplyID, @ProcessDate,")
            strSQL.AppendLine("    @ApplyDate, @DueDate, @SalaryPaid, @QuitReason, @EvaluationMark, @WTID, @ExistsEmployeeLog,")
            strSQL.AppendLine("    @RecID, @ContractDate, @BindData, @BusinessType, @EmpFlowRemarkID, @LastChgComp, @LastChgID,")
            strSQL.AppendLine("    @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In EmployeeWaitRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(r.ValidDate.Value), Convert.ToDateTime("1900/1/1"), r.ValidDate.Value))
                        db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                        db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                        db.AddInParameter(dbcmd, "@NewCompID", DbType.String, r.NewCompID.Value)
                        db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                        db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                        db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                        db.AddInParameter(dbcmd, "@TitleID", DbType.String, r.TitleID.Value)
                        db.AddInParameter(dbcmd, "@TitleName", DbType.String, r.TitleName.Value)
                        db.AddInParameter(dbcmd, "@PositionID", DbType.String, r.PositionID.Value)
                        db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                        db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                        db.AddInParameter(dbcmd, "@GroupOrganID", DbType.String, r.GroupOrganID.Value)
                        db.AddInParameter(dbcmd, "@IsBoss", DbType.String, r.IsBoss.Value)
                        db.AddInParameter(dbcmd, "@IsSecBoss", DbType.String, r.IsSecBoss.Value)
                        db.AddInParameter(dbcmd, "@IsGroupBoss", DbType.String, r.IsGroupBoss.Value)
                        db.AddInParameter(dbcmd, "@IsSecGroupBoss", DbType.String, r.IsSecGroupBoss.Value)
                        db.AddInParameter(dbcmd, "@BossType", DbType.String, r.BossType.Value)
                        db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                        db.AddInParameter(dbcmd, "@ValidMark", DbType.String, r.ValidMark.Value)
                        db.AddInParameter(dbcmd, "@FlowOrganID", DbType.String, r.FlowOrganID.Value)
                        db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, r.WorkSiteID.Value)
                        db.AddInParameter(dbcmd, "@ApplyID", DbType.String, r.ApplyID.Value)
                        db.AddInParameter(dbcmd, "@ProcessDate", DbType.Date, IIf(IsDateTimeNull(r.ProcessDate.Value), Convert.ToDateTime("1900/1/1"), r.ProcessDate.Value))
                        db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, IIf(IsDateTimeNull(r.ApplyDate.Value), Convert.ToDateTime("1900/1/1"), r.ApplyDate.Value))
                        db.AddInParameter(dbcmd, "@DueDate", DbType.Date, IIf(IsDateTimeNull(r.DueDate.Value), Convert.ToDateTime("1900/1/1"), r.DueDate.Value))
                        db.AddInParameter(dbcmd, "@SalaryPaid", DbType.String, r.SalaryPaid.Value)
                        db.AddInParameter(dbcmd, "@QuitReason", DbType.String, r.QuitReason.Value)
                        db.AddInParameter(dbcmd, "@EvaluationMark", DbType.String, r.EvaluationMark.Value)
                        db.AddInParameter(dbcmd, "@WTID", DbType.String, r.WTID.Value)
                        db.AddInParameter(dbcmd, "@ExistsEmployeeLog", DbType.String, r.ExistsEmployeeLog.Value)
                        db.AddInParameter(dbcmd, "@RecID", DbType.String, r.RecID.Value)
                        db.AddInParameter(dbcmd, "@ContractDate", DbType.Date, IIf(IsDateTimeNull(r.ContractDate.Value), Convert.ToDateTime("1900/1/1"), r.ContractDate.Value))
                        db.AddInParameter(dbcmd, "@BindData", DbType.String, r.BindData.Value)
                        db.AddInParameter(dbcmd, "@BusinessType", DbType.String, r.BusinessType.Value)
                        db.AddInParameter(dbcmd, "@EmpFlowRemarkID", DbType.String, r.EmpFlowRemarkID.Value)
                        db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))

                        intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
                    Next
                    tran.Commit()
                Catch ex As Exception
                    If inTrans Then tran.Rollback()
                    Throw
                Finally
                    tran.Dispose()
                    If cn.State = ConnectionState.Open Then cn.Close()
                End Try
            End Using
            Return intRowsAffected
        End Function

        Public Function Insert(ByVal EmployeeWaitRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into EmployeeWait")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, ValidDate, Seq, Reason, NewCompID, DeptID, OrganID, RankID, TitleID,")
            strSQL.AppendLine("    TitleName, PositionID, WorkTypeID, GroupID, GroupOrganID, IsBoss, IsSecBoss, IsGroupBoss,")
            strSQL.AppendLine("    IsSecGroupBoss, BossType, Remark, ValidMark, FlowOrganID, WorkSiteID, ApplyID, ProcessDate,")
            strSQL.AppendLine("    ApplyDate, DueDate, SalaryPaid, QuitReason, EvaluationMark, WTID, ExistsEmployeeLog,")
            strSQL.AppendLine("    RecID, ContractDate, BindData, BusinessType, EmpFlowRemarkID, LastChgComp, LastChgID,")
            strSQL.AppendLine("    LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @ValidDate, @Seq, @Reason, @NewCompID, @DeptID, @OrganID, @RankID, @TitleID,")
            strSQL.AppendLine("    @TitleName, @PositionID, @WorkTypeID, @GroupID, @GroupOrganID, @IsBoss, @IsSecBoss, @IsGroupBoss,")
            strSQL.AppendLine("    @IsSecGroupBoss, @BossType, @Remark, @ValidMark, @FlowOrganID, @WorkSiteID, @ApplyID, @ProcessDate,")
            strSQL.AppendLine("    @ApplyDate, @DueDate, @SalaryPaid, @QuitReason, @EvaluationMark, @WTID, @ExistsEmployeeLog,")
            strSQL.AppendLine("    @RecID, @ContractDate, @BindData, @BusinessType, @EmpFlowRemarkID, @LastChgComp, @LastChgID,")
            strSQL.AppendLine("    @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In EmployeeWaitRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(r.ValidDate.Value), Convert.ToDateTime("1900/1/1"), r.ValidDate.Value))
                db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                db.AddInParameter(dbcmd, "@NewCompID", DbType.String, r.NewCompID.Value)
                db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                db.AddInParameter(dbcmd, "@TitleID", DbType.String, r.TitleID.Value)
                db.AddInParameter(dbcmd, "@TitleName", DbType.String, r.TitleName.Value)
                db.AddInParameter(dbcmd, "@PositionID", DbType.String, r.PositionID.Value)
                db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                db.AddInParameter(dbcmd, "@GroupOrganID", DbType.String, r.GroupOrganID.Value)
                db.AddInParameter(dbcmd, "@IsBoss", DbType.String, r.IsBoss.Value)
                db.AddInParameter(dbcmd, "@IsSecBoss", DbType.String, r.IsSecBoss.Value)
                db.AddInParameter(dbcmd, "@IsGroupBoss", DbType.String, r.IsGroupBoss.Value)
                db.AddInParameter(dbcmd, "@IsSecGroupBoss", DbType.String, r.IsSecGroupBoss.Value)
                db.AddInParameter(dbcmd, "@BossType", DbType.String, r.BossType.Value)
                db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                db.AddInParameter(dbcmd, "@ValidMark", DbType.String, r.ValidMark.Value)
                db.AddInParameter(dbcmd, "@FlowOrganID", DbType.String, r.FlowOrganID.Value)
                db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, r.WorkSiteID.Value)
                db.AddInParameter(dbcmd, "@ApplyID", DbType.String, r.ApplyID.Value)
                db.AddInParameter(dbcmd, "@ProcessDate", DbType.Date, IIf(IsDateTimeNull(r.ProcessDate.Value), Convert.ToDateTime("1900/1/1"), r.ProcessDate.Value))
                db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, IIf(IsDateTimeNull(r.ApplyDate.Value), Convert.ToDateTime("1900/1/1"), r.ApplyDate.Value))
                db.AddInParameter(dbcmd, "@DueDate", DbType.Date, IIf(IsDateTimeNull(r.DueDate.Value), Convert.ToDateTime("1900/1/1"), r.DueDate.Value))
                db.AddInParameter(dbcmd, "@SalaryPaid", DbType.String, r.SalaryPaid.Value)
                db.AddInParameter(dbcmd, "@QuitReason", DbType.String, r.QuitReason.Value)
                db.AddInParameter(dbcmd, "@EvaluationMark", DbType.String, r.EvaluationMark.Value)
                db.AddInParameter(dbcmd, "@WTID", DbType.String, r.WTID.Value)
                db.AddInParameter(dbcmd, "@ExistsEmployeeLog", DbType.String, r.ExistsEmployeeLog.Value)
                db.AddInParameter(dbcmd, "@RecID", DbType.String, r.RecID.Value)
                db.AddInParameter(dbcmd, "@ContractDate", DbType.Date, IIf(IsDateTimeNull(r.ContractDate.Value), Convert.ToDateTime("1900/1/1"), r.ContractDate.Value))
                db.AddInParameter(dbcmd, "@BindData", DbType.String, r.BindData.Value)
                db.AddInParameter(dbcmd, "@BusinessType", DbType.String, r.BusinessType.Value)
                db.AddInParameter(dbcmd, "@EmpFlowRemarkID", DbType.String, r.EmpFlowRemarkID.Value)
                db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Private Function IsDateTimeNull(ByVal Src As DateTime) As Boolean
            If Src = Convert.ToDateTime("1900/1/1") OrElse _
               Src = Convert.ToDateTime("0001/1/1") Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

