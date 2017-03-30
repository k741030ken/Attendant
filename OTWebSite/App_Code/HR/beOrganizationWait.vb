'****************************************************************
' Table:OrganizationWait
' Created Date: 2016.10.18
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beOrganizationWait
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "OrganReason", "OrganType", "ValidDate", "Seq", "ExecuteDate", "WaitStatus", "OrganID", "OrganNameOld", "OrganName" _
                                    , "OrganEngName", "VirtualFlag", "UpOrganID", "DeptID", "GroupType", "GroupID", "Boss", "BossCompID", "BossType", "BossTemporary", "SecBoss" _
                                    , "SecBossCompID", "InValidFlag", "ReportingEnd", "Depth", "BranchFlag", "RoleCode", "PersonPart", "SecPersonPart", "CheckPart", "WorkSiteID", "WorkTypeID" _
                                    , "CostDeptID", "CostType", "AccountBranch", "PositionID", "CompareFlag", "FlowOrganID", "DelegateFlag", "OrganNo", "InvoiceNo", "SortOrder", "OrgType" _
                                    , "BusinessType", "ValidDateB", "ValidDateE", "Remark", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(Date), GetType(Integer), GetType(Date), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(Date), GetType(Date), GetType(String), GetType(String), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "CompID", "OrganID", "OrganReason", "OrganType", "ValidDate", "Seq" }

        Public ReadOnly Property Rows() As beOrganizationWait.Rows 
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
        Public Sub Transfer2Row(OrganizationWaitTable As DataTable)
            For Each dr As DataRow In OrganizationWaitTable.Rows
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
                dr(m_Rows(i).OrganReason.FieldName) = m_Rows(i).OrganReason.Value
                dr(m_Rows(i).OrganType.FieldName) = m_Rows(i).OrganType.Value
                dr(m_Rows(i).ValidDate.FieldName) = m_Rows(i).ValidDate.Value
                dr(m_Rows(i).Seq.FieldName) = m_Rows(i).Seq.Value
                dr(m_Rows(i).ExecuteDate.FieldName) = m_Rows(i).ExecuteDate.Value
                dr(m_Rows(i).WaitStatus.FieldName) = m_Rows(i).WaitStatus.Value
                dr(m_Rows(i).OrganID.FieldName) = m_Rows(i).OrganID.Value
                dr(m_Rows(i).OrganNameOld.FieldName) = m_Rows(i).OrganNameOld.Value
                dr(m_Rows(i).OrganName.FieldName) = m_Rows(i).OrganName.Value
                dr(m_Rows(i).OrganEngName.FieldName) = m_Rows(i).OrganEngName.Value
                dr(m_Rows(i).VirtualFlag.FieldName) = m_Rows(i).VirtualFlag.Value
                dr(m_Rows(i).UpOrganID.FieldName) = m_Rows(i).UpOrganID.Value
                dr(m_Rows(i).DeptID.FieldName) = m_Rows(i).DeptID.Value
                dr(m_Rows(i).GroupType.FieldName) = m_Rows(i).GroupType.Value
                dr(m_Rows(i).GroupID.FieldName) = m_Rows(i).GroupID.Value
                dr(m_Rows(i).Boss.FieldName) = m_Rows(i).Boss.Value
                dr(m_Rows(i).BossCompID.FieldName) = m_Rows(i).BossCompID.Value
                dr(m_Rows(i).BossType.FieldName) = m_Rows(i).BossType.Value
                dr(m_Rows(i).BossTemporary.FieldName) = m_Rows(i).BossTemporary.Value
                dr(m_Rows(i).SecBoss.FieldName) = m_Rows(i).SecBoss.Value
                dr(m_Rows(i).SecBossCompID.FieldName) = m_Rows(i).SecBossCompID.Value
                dr(m_Rows(i).InValidFlag.FieldName) = m_Rows(i).InValidFlag.Value
                dr(m_Rows(i).ReportingEnd.FieldName) = m_Rows(i).ReportingEnd.Value
                dr(m_Rows(i).Depth.FieldName) = m_Rows(i).Depth.Value
                dr(m_Rows(i).BranchFlag.FieldName) = m_Rows(i).BranchFlag.Value
                dr(m_Rows(i).RoleCode.FieldName) = m_Rows(i).RoleCode.Value
                dr(m_Rows(i).PersonPart.FieldName) = m_Rows(i).PersonPart.Value
                dr(m_Rows(i).SecPersonPart.FieldName) = m_Rows(i).SecPersonPart.Value
                dr(m_Rows(i).CheckPart.FieldName) = m_Rows(i).CheckPart.Value
                dr(m_Rows(i).WorkSiteID.FieldName) = m_Rows(i).WorkSiteID.Value
                dr(m_Rows(i).WorkTypeID.FieldName) = m_Rows(i).WorkTypeID.Value
                dr(m_Rows(i).CostDeptID.FieldName) = m_Rows(i).CostDeptID.Value
                dr(m_Rows(i).CostType.FieldName) = m_Rows(i).CostType.Value
                dr(m_Rows(i).AccountBranch.FieldName) = m_Rows(i).AccountBranch.Value
                dr(m_Rows(i).PositionID.FieldName) = m_Rows(i).PositionID.Value
                dr(m_Rows(i).CompareFlag.FieldName) = m_Rows(i).CompareFlag.Value
                dr(m_Rows(i).FlowOrganID.FieldName) = m_Rows(i).FlowOrganID.Value
                dr(m_Rows(i).DelegateFlag.FieldName) = m_Rows(i).DelegateFlag.Value
                dr(m_Rows(i).OrganNo.FieldName) = m_Rows(i).OrganNo.Value
                dr(m_Rows(i).InvoiceNo.FieldName) = m_Rows(i).InvoiceNo.Value
                dr(m_Rows(i).SortOrder.FieldName) = m_Rows(i).SortOrder.Value
                dr(m_Rows(i).OrgType.FieldName) = m_Rows(i).OrgType.Value
                dr(m_Rows(i).BusinessType.FieldName) = m_Rows(i).BusinessType.Value
                dr(m_Rows(i).ValidDateB.FieldName) = m_Rows(i).ValidDateB.Value
                dr(m_Rows(i).ValidDateE.FieldName) = m_Rows(i).ValidDateE.Value
                dr(m_Rows(i).Remark.FieldName) = m_Rows(i).Remark.Value
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

        Public Sub Add(OrganizationWaitRow As Row)
            m_Rows.Add(OrganizationWaitRow)
        End Sub

        Public Sub Remove(OrganizationWaitRow As Row)
            If m_Rows.IndexOf(OrganizationWaitRow) >= 0 Then
                m_Rows.Remove(OrganizationWaitRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_OrganReason As Field(Of String) = new Field(Of String)("OrganReason", true)
        Private FI_OrganType As Field(Of String) = new Field(Of String)("OrganType", true)
        Private FI_ValidDate As Field(Of Date) = new Field(Of Date)("ValidDate", true)
        Private FI_Seq As Field(Of Integer) = new Field(Of Integer)("Seq", true)
        Private FI_ExecuteDate As Field(Of Date) = new Field(Of Date)("ExecuteDate", true)
        Private FI_WaitStatus As Field(Of String) = new Field(Of String)("WaitStatus", true)
        Private FI_OrganID As Field(Of String) = new Field(Of String)("OrganID", true)
        Private FI_OrganNameOld As Field(Of String) = new Field(Of String)("OrganNameOld", true)
        Private FI_OrganName As Field(Of String) = new Field(Of String)("OrganName", true)
        Private FI_OrganEngName As Field(Of String) = new Field(Of String)("OrganEngName", true)
        Private FI_VirtualFlag As Field(Of String) = new Field(Of String)("VirtualFlag", true)
        Private FI_UpOrganID As Field(Of String) = new Field(Of String)("UpOrganID", true)
        Private FI_DeptID As Field(Of String) = new Field(Of String)("DeptID", true)
        Private FI_GroupType As Field(Of String) = new Field(Of String)("GroupType", true)
        Private FI_GroupID As Field(Of String) = new Field(Of String)("GroupID", true)
        Private FI_Boss As Field(Of String) = new Field(Of String)("Boss", true)
        Private FI_BossCompID As Field(Of String) = new Field(Of String)("BossCompID", true)
        Private FI_BossType As Field(Of String) = new Field(Of String)("BossType", true)
        Private FI_BossTemporary As Field(Of String) = new Field(Of String)("BossTemporary", true)
        Private FI_SecBoss As Field(Of String) = new Field(Of String)("SecBoss", true)
        Private FI_SecBossCompID As Field(Of String) = new Field(Of String)("SecBossCompID", true)
        Private FI_InValidFlag As Field(Of String) = new Field(Of String)("InValidFlag", true)
        Private FI_ReportingEnd As Field(Of String) = new Field(Of String)("ReportingEnd", true)
        Private FI_Depth As Field(Of String) = new Field(Of String)("Depth", true)
        Private FI_BranchFlag As Field(Of String) = new Field(Of String)("BranchFlag", true)
        Private FI_RoleCode As Field(Of String) = new Field(Of String)("RoleCode", true)
        Private FI_PersonPart As Field(Of String) = new Field(Of String)("PersonPart", true)
        Private FI_SecPersonPart As Field(Of String) = new Field(Of String)("SecPersonPart", true)
        Private FI_CheckPart As Field(Of String) = new Field(Of String)("CheckPart", true)
        Private FI_WorkSiteID As Field(Of String) = new Field(Of String)("WorkSiteID", true)
        Private FI_WorkTypeID As Field(Of String) = new Field(Of String)("WorkTypeID", true)
        Private FI_CostDeptID As Field(Of String) = new Field(Of String)("CostDeptID", true)
        Private FI_CostType As Field(Of String) = new Field(Of String)("CostType", true)
        Private FI_AccountBranch As Field(Of String) = new Field(Of String)("AccountBranch", true)
        Private FI_PositionID As Field(Of String) = new Field(Of String)("PositionID", true)
        Private FI_CompareFlag As Field(Of String) = new Field(Of String)("CompareFlag", true)
        Private FI_FlowOrganID As Field(Of String) = new Field(Of String)("FlowOrganID", true)
        Private FI_DelegateFlag As Field(Of String) = new Field(Of String)("DelegateFlag", true)
        Private FI_OrganNo As Field(Of String) = new Field(Of String)("OrganNo", true)
        Private FI_InvoiceNo As Field(Of String) = new Field(Of String)("InvoiceNo", true)
        Private FI_SortOrder As Field(Of String) = new Field(Of String)("SortOrder", true)
        Private FI_OrgType As Field(Of String) = new Field(Of String)("OrgType", true)
        Private FI_BusinessType As Field(Of String) = new Field(Of String)("BusinessType", true)
        Private FI_ValidDateB As Field(Of Date) = new Field(Of Date)("ValidDateB", true)
        Private FI_ValidDateE As Field(Of Date) = new Field(Of Date)("ValidDateE", true)
        Private FI_Remark As Field(Of String) = new Field(Of String)("Remark", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "CompID", "OrganReason", "OrganType", "ValidDate", "Seq", "ExecuteDate", "WaitStatus", "OrganID", "OrganNameOld", "OrganName" _
                                    , "OrganEngName", "VirtualFlag", "UpOrganID", "DeptID", "GroupType", "GroupID", "Boss", "BossCompID", "BossType", "BossTemporary", "SecBoss" _
                                    , "SecBossCompID", "InValidFlag", "ReportingEnd", "Depth", "BranchFlag", "RoleCode", "PersonPart", "SecPersonPart", "CheckPart", "WorkSiteID", "WorkTypeID" _
                                    , "CostDeptID", "CostType", "AccountBranch", "PositionID", "CompareFlag", "FlowOrganID", "DelegateFlag", "OrganNo", "InvoiceNo", "SortOrder", "OrgType" _
                                    , "BusinessType", "ValidDateB", "ValidDateE", "Remark", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "CompID", "OrganID", "OrganReason", "OrganType", "ValidDate", "Seq" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CompID"
                    Return FI_CompID.Value
                Case "OrganReason"
                    Return FI_OrganReason.Value
                Case "OrganType"
                    Return FI_OrganType.Value
                Case "ValidDate"
                    Return FI_ValidDate.Value
                Case "Seq"
                    Return FI_Seq.Value
                Case "ExecuteDate"
                    Return FI_ExecuteDate.Value
                Case "WaitStatus"
                    Return FI_WaitStatus.Value
                Case "OrganID"
                    Return FI_OrganID.Value
                Case "OrganNameOld"
                    Return FI_OrganNameOld.Value
                Case "OrganName"
                    Return FI_OrganName.Value
                Case "OrganEngName"
                    Return FI_OrganEngName.Value
                Case "VirtualFlag"
                    Return FI_VirtualFlag.Value
                Case "UpOrganID"
                    Return FI_UpOrganID.Value
                Case "DeptID"
                    Return FI_DeptID.Value
                Case "GroupType"
                    Return FI_GroupType.Value
                Case "GroupID"
                    Return FI_GroupID.Value
                Case "Boss"
                    Return FI_Boss.Value
                Case "BossCompID"
                    Return FI_BossCompID.Value
                Case "BossType"
                    Return FI_BossType.Value
                Case "BossTemporary"
                    Return FI_BossTemporary.Value
                Case "SecBoss"
                    Return FI_SecBoss.Value
                Case "SecBossCompID"
                    Return FI_SecBossCompID.Value
                Case "InValidFlag"
                    Return FI_InValidFlag.Value
                Case "ReportingEnd"
                    Return FI_ReportingEnd.Value
                Case "Depth"
                    Return FI_Depth.Value
                Case "BranchFlag"
                    Return FI_BranchFlag.Value
                Case "RoleCode"
                    Return FI_RoleCode.Value
                Case "PersonPart"
                    Return FI_PersonPart.Value
                Case "SecPersonPart"
                    Return FI_SecPersonPart.Value
                Case "CheckPart"
                    Return FI_CheckPart.Value
                Case "WorkSiteID"
                    Return FI_WorkSiteID.Value
                Case "WorkTypeID"
                    Return FI_WorkTypeID.Value
                Case "CostDeptID"
                    Return FI_CostDeptID.Value
                Case "CostType"
                    Return FI_CostType.Value
                Case "AccountBranch"
                    Return FI_AccountBranch.Value
                Case "PositionID"
                    Return FI_PositionID.Value
                Case "CompareFlag"
                    Return FI_CompareFlag.Value
                Case "FlowOrganID"
                    Return FI_FlowOrganID.Value
                Case "DelegateFlag"
                    Return FI_DelegateFlag.Value
                Case "OrganNo"
                    Return FI_OrganNo.Value
                Case "InvoiceNo"
                    Return FI_InvoiceNo.Value
                Case "SortOrder"
                    Return FI_SortOrder.Value
                Case "OrgType"
                    Return FI_OrgType.Value
                Case "BusinessType"
                    Return FI_BusinessType.Value
                Case "ValidDateB"
                    Return FI_ValidDateB.Value
                Case "ValidDateE"
                    Return FI_ValidDateE.Value
                Case "Remark"
                    Return FI_Remark.Value
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
                Case "OrganReason"
                    FI_OrganReason.SetValue(value)
                Case "OrganType"
                    FI_OrganType.SetValue(value)
                Case "ValidDate"
                    FI_ValidDate.SetValue(value)
                Case "Seq"
                    FI_Seq.SetValue(value)
                Case "ExecuteDate"
                    FI_ExecuteDate.SetValue(value)
                Case "WaitStatus"
                    FI_WaitStatus.SetValue(value)
                Case "OrganID"
                    FI_OrganID.SetValue(value)
                Case "OrganNameOld"
                    FI_OrganNameOld.SetValue(value)
                Case "OrganName"
                    FI_OrganName.SetValue(value)
                Case "OrganEngName"
                    FI_OrganEngName.SetValue(value)
                Case "VirtualFlag"
                    FI_VirtualFlag.SetValue(value)
                Case "UpOrganID"
                    FI_UpOrganID.SetValue(value)
                Case "DeptID"
                    FI_DeptID.SetValue(value)
                Case "GroupType"
                    FI_GroupType.SetValue(value)
                Case "GroupID"
                    FI_GroupID.SetValue(value)
                Case "Boss"
                    FI_Boss.SetValue(value)
                Case "BossCompID"
                    FI_BossCompID.SetValue(value)
                Case "BossType"
                    FI_BossType.SetValue(value)
                Case "BossTemporary"
                    FI_BossTemporary.SetValue(value)
                Case "SecBoss"
                    FI_SecBoss.SetValue(value)
                Case "SecBossCompID"
                    FI_SecBossCompID.SetValue(value)
                Case "InValidFlag"
                    FI_InValidFlag.SetValue(value)
                Case "ReportingEnd"
                    FI_ReportingEnd.SetValue(value)
                Case "Depth"
                    FI_Depth.SetValue(value)
                Case "BranchFlag"
                    FI_BranchFlag.SetValue(value)
                Case "RoleCode"
                    FI_RoleCode.SetValue(value)
                Case "PersonPart"
                    FI_PersonPart.SetValue(value)
                Case "SecPersonPart"
                    FI_SecPersonPart.SetValue(value)
                Case "CheckPart"
                    FI_CheckPart.SetValue(value)
                Case "WorkSiteID"
                    FI_WorkSiteID.SetValue(value)
                Case "WorkTypeID"
                    FI_WorkTypeID.SetValue(value)
                Case "CostDeptID"
                    FI_CostDeptID.SetValue(value)
                Case "CostType"
                    FI_CostType.SetValue(value)
                Case "AccountBranch"
                    FI_AccountBranch.SetValue(value)
                Case "PositionID"
                    FI_PositionID.SetValue(value)
                Case "CompareFlag"
                    FI_CompareFlag.SetValue(value)
                Case "FlowOrganID"
                    FI_FlowOrganID.SetValue(value)
                Case "DelegateFlag"
                    FI_DelegateFlag.SetValue(value)
                Case "OrganNo"
                    FI_OrganNo.SetValue(value)
                Case "InvoiceNo"
                    FI_InvoiceNo.SetValue(value)
                Case "SortOrder"
                    FI_SortOrder.SetValue(value)
                Case "OrgType"
                    FI_OrgType.SetValue(value)
                Case "BusinessType"
                    FI_BusinessType.SetValue(value)
                Case "ValidDateB"
                    FI_ValidDateB.SetValue(value)
                Case "ValidDateE"
                    FI_ValidDateE.SetValue(value)
                Case "Remark"
                    FI_Remark.SetValue(value)
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
                Case "OrganReason"
                    return FI_OrganReason.Updated
                Case "OrganType"
                    return FI_OrganType.Updated
                Case "ValidDate"
                    return FI_ValidDate.Updated
                Case "Seq"
                    return FI_Seq.Updated
                Case "ExecuteDate"
                    return FI_ExecuteDate.Updated
                Case "WaitStatus"
                    return FI_WaitStatus.Updated
                Case "OrganID"
                    return FI_OrganID.Updated
                Case "OrganNameOld"
                    return FI_OrganNameOld.Updated
                Case "OrganName"
                    return FI_OrganName.Updated
                Case "OrganEngName"
                    return FI_OrganEngName.Updated
                Case "VirtualFlag"
                    return FI_VirtualFlag.Updated
                Case "UpOrganID"
                    return FI_UpOrganID.Updated
                Case "DeptID"
                    return FI_DeptID.Updated
                Case "GroupType"
                    return FI_GroupType.Updated
                Case "GroupID"
                    return FI_GroupID.Updated
                Case "Boss"
                    return FI_Boss.Updated
                Case "BossCompID"
                    return FI_BossCompID.Updated
                Case "BossType"
                    return FI_BossType.Updated
                Case "BossTemporary"
                    return FI_BossTemporary.Updated
                Case "SecBoss"
                    return FI_SecBoss.Updated
                Case "SecBossCompID"
                    return FI_SecBossCompID.Updated
                Case "InValidFlag"
                    return FI_InValidFlag.Updated
                Case "ReportingEnd"
                    return FI_ReportingEnd.Updated
                Case "Depth"
                    return FI_Depth.Updated
                Case "BranchFlag"
                    return FI_BranchFlag.Updated
                Case "RoleCode"
                    return FI_RoleCode.Updated
                Case "PersonPart"
                    return FI_PersonPart.Updated
                Case "SecPersonPart"
                    return FI_SecPersonPart.Updated
                Case "CheckPart"
                    return FI_CheckPart.Updated
                Case "WorkSiteID"
                    return FI_WorkSiteID.Updated
                Case "WorkTypeID"
                    return FI_WorkTypeID.Updated
                Case "CostDeptID"
                    return FI_CostDeptID.Updated
                Case "CostType"
                    return FI_CostType.Updated
                Case "AccountBranch"
                    return FI_AccountBranch.Updated
                Case "PositionID"
                    return FI_PositionID.Updated
                Case "CompareFlag"
                    return FI_CompareFlag.Updated
                Case "FlowOrganID"
                    return FI_FlowOrganID.Updated
                Case "DelegateFlag"
                    return FI_DelegateFlag.Updated
                Case "OrganNo"
                    return FI_OrganNo.Updated
                Case "InvoiceNo"
                    return FI_InvoiceNo.Updated
                Case "SortOrder"
                    return FI_SortOrder.Updated
                Case "OrgType"
                    return FI_OrgType.Updated
                Case "BusinessType"
                    return FI_BusinessType.Updated
                Case "ValidDateB"
                    return FI_ValidDateB.Updated
                Case "ValidDateE"
                    return FI_ValidDateE.Updated
                Case "Remark"
                    return FI_Remark.Updated
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
                Case "OrganReason"
                    return FI_OrganReason.CreateUpdateSQL
                Case "OrganType"
                    return FI_OrganType.CreateUpdateSQL
                Case "ValidDate"
                    return FI_ValidDate.CreateUpdateSQL
                Case "Seq"
                    return FI_Seq.CreateUpdateSQL
                Case "ExecuteDate"
                    return FI_ExecuteDate.CreateUpdateSQL
                Case "WaitStatus"
                    return FI_WaitStatus.CreateUpdateSQL
                Case "OrganID"
                    return FI_OrganID.CreateUpdateSQL
                Case "OrganNameOld"
                    return FI_OrganNameOld.CreateUpdateSQL
                Case "OrganName"
                    return FI_OrganName.CreateUpdateSQL
                Case "OrganEngName"
                    return FI_OrganEngName.CreateUpdateSQL
                Case "VirtualFlag"
                    return FI_VirtualFlag.CreateUpdateSQL
                Case "UpOrganID"
                    return FI_UpOrganID.CreateUpdateSQL
                Case "DeptID"
                    return FI_DeptID.CreateUpdateSQL
                Case "GroupType"
                    return FI_GroupType.CreateUpdateSQL
                Case "GroupID"
                    return FI_GroupID.CreateUpdateSQL
                Case "Boss"
                    return FI_Boss.CreateUpdateSQL
                Case "BossCompID"
                    return FI_BossCompID.CreateUpdateSQL
                Case "BossType"
                    return FI_BossType.CreateUpdateSQL
                Case "BossTemporary"
                    return FI_BossTemporary.CreateUpdateSQL
                Case "SecBoss"
                    return FI_SecBoss.CreateUpdateSQL
                Case "SecBossCompID"
                    return FI_SecBossCompID.CreateUpdateSQL
                Case "InValidFlag"
                    return FI_InValidFlag.CreateUpdateSQL
                Case "ReportingEnd"
                    return FI_ReportingEnd.CreateUpdateSQL
                Case "Depth"
                    return FI_Depth.CreateUpdateSQL
                Case "BranchFlag"
                    return FI_BranchFlag.CreateUpdateSQL
                Case "RoleCode"
                    return FI_RoleCode.CreateUpdateSQL
                Case "PersonPart"
                    return FI_PersonPart.CreateUpdateSQL
                Case "SecPersonPart"
                    return FI_SecPersonPart.CreateUpdateSQL
                Case "CheckPart"
                    return FI_CheckPart.CreateUpdateSQL
                Case "WorkSiteID"
                    return FI_WorkSiteID.CreateUpdateSQL
                Case "WorkTypeID"
                    return FI_WorkTypeID.CreateUpdateSQL
                Case "CostDeptID"
                    return FI_CostDeptID.CreateUpdateSQL
                Case "CostType"
                    return FI_CostType.CreateUpdateSQL
                Case "AccountBranch"
                    return FI_AccountBranch.CreateUpdateSQL
                Case "PositionID"
                    return FI_PositionID.CreateUpdateSQL
                Case "CompareFlag"
                    return FI_CompareFlag.CreateUpdateSQL
                Case "FlowOrganID"
                    return FI_FlowOrganID.CreateUpdateSQL
                Case "DelegateFlag"
                    return FI_DelegateFlag.CreateUpdateSQL
                Case "OrganNo"
                    return FI_OrganNo.CreateUpdateSQL
                Case "InvoiceNo"
                    return FI_InvoiceNo.CreateUpdateSQL
                Case "SortOrder"
                    return FI_SortOrder.CreateUpdateSQL
                Case "OrgType"
                    return FI_OrgType.CreateUpdateSQL
                Case "BusinessType"
                    return FI_BusinessType.CreateUpdateSQL
                Case "ValidDateB"
                    return FI_ValidDateB.CreateUpdateSQL
                Case "ValidDateE"
                    return FI_ValidDateE.CreateUpdateSQL
                Case "Remark"
                    return FI_Remark.CreateUpdateSQL
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
            FI_OrganReason.SetInitValue("")
            FI_OrganType.SetInitValue("")
            FI_ValidDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_Seq.SetInitValue(0)
            FI_ExecuteDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_WaitStatus.SetInitValue("0")
            FI_OrganID.SetInitValue("")
            FI_OrganNameOld.SetInitValue("")
            FI_OrganName.SetInitValue("")
            FI_OrganEngName.SetInitValue("")
            FI_VirtualFlag.SetInitValue("0")
            FI_UpOrganID.SetInitValue("")
            FI_DeptID.SetInitValue("")
            FI_GroupType.SetInitValue("")
            FI_GroupID.SetInitValue("")
            FI_Boss.SetInitValue("")
            FI_BossCompID.SetInitValue("")
            FI_BossType.SetInitValue("")
            FI_BossTemporary.SetInitValue("0")
            FI_SecBoss.SetInitValue("")
            FI_SecBossCompID.SetInitValue("")
            FI_InValidFlag.SetInitValue("0")
            FI_ReportingEnd.SetInitValue("0")
            FI_Depth.SetInitValue("")
            FI_BranchFlag.SetInitValue("")
            FI_RoleCode.SetInitValue("")
            FI_PersonPart.SetInitValue("")
            FI_SecPersonPart.SetInitValue("")
            FI_CheckPart.SetInitValue("")
            FI_WorkSiteID.SetInitValue("")
            FI_WorkTypeID.SetInitValue("")
            FI_CostDeptID.SetInitValue("")
            FI_CostType.SetInitValue("")
            FI_AccountBranch.SetInitValue("")
            FI_PositionID.SetInitValue("")
            FI_CompareFlag.SetInitValue("0")
            FI_FlowOrganID.SetInitValue("")
            FI_DelegateFlag.SetInitValue("")
            FI_OrganNo.SetInitValue("0")
            FI_InvoiceNo.SetInitValue("")
            FI_SortOrder.SetInitValue("")
            FI_OrgType.SetInitValue("")
            FI_BusinessType.SetInitValue("")
            FI_ValidDateB.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_ValidDateE.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_Remark.SetInitValue("")
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CompID.SetInitValue(dr("CompID"))
            FI_OrganReason.SetInitValue(dr("OrganReason"))
            FI_OrganType.SetInitValue(dr("OrganType"))
            FI_ValidDate.SetInitValue(dr("ValidDate"))
            FI_Seq.SetInitValue(dr("Seq"))
            FI_ExecuteDate.SetInitValue(dr("ExecuteDate"))
            FI_WaitStatus.SetInitValue(dr("WaitStatus"))
            FI_OrganID.SetInitValue(dr("OrganID"))
            FI_OrganNameOld.SetInitValue(dr("OrganNameOld"))
            FI_OrganName.SetInitValue(dr("OrganName"))
            FI_OrganEngName.SetInitValue(dr("OrganEngName"))
            FI_VirtualFlag.SetInitValue(dr("VirtualFlag"))
            FI_UpOrganID.SetInitValue(dr("UpOrganID"))
            FI_DeptID.SetInitValue(dr("DeptID"))
            FI_GroupType.SetInitValue(dr("GroupType"))
            FI_GroupID.SetInitValue(dr("GroupID"))
            FI_Boss.SetInitValue(dr("Boss"))
            FI_BossCompID.SetInitValue(dr("BossCompID"))
            FI_BossType.SetInitValue(dr("BossType"))
            FI_BossTemporary.SetInitValue(dr("BossTemporary"))
            FI_SecBoss.SetInitValue(dr("SecBoss"))
            FI_SecBossCompID.SetInitValue(dr("SecBossCompID"))
            FI_InValidFlag.SetInitValue(dr("InValidFlag"))
            FI_ReportingEnd.SetInitValue(dr("ReportingEnd"))
            FI_Depth.SetInitValue(dr("Depth"))
            FI_BranchFlag.SetInitValue(dr("BranchFlag"))
            FI_RoleCode.SetInitValue(dr("RoleCode"))
            FI_PersonPart.SetInitValue(dr("PersonPart"))
            FI_SecPersonPart.SetInitValue(dr("SecPersonPart"))
            FI_CheckPart.SetInitValue(dr("CheckPart"))
            FI_WorkSiteID.SetInitValue(dr("WorkSiteID"))
            FI_WorkTypeID.SetInitValue(dr("WorkTypeID"))
            FI_CostDeptID.SetInitValue(dr("CostDeptID"))
            FI_CostType.SetInitValue(dr("CostType"))
            FI_AccountBranch.SetInitValue(dr("AccountBranch"))
            FI_PositionID.SetInitValue(dr("PositionID"))
            FI_CompareFlag.SetInitValue(dr("CompareFlag"))
            FI_FlowOrganID.SetInitValue(dr("FlowOrganID"))
            FI_DelegateFlag.SetInitValue(dr("DelegateFlag"))
            FI_OrganNo.SetInitValue(dr("OrganNo"))
            FI_InvoiceNo.SetInitValue(dr("InvoiceNo"))
            FI_SortOrder.SetInitValue(dr("SortOrder"))
            FI_OrgType.SetInitValue(dr("OrgType"))
            FI_BusinessType.SetInitValue(dr("BusinessType"))
            FI_ValidDateB.SetInitValue(dr("ValidDateB"))
            FI_ValidDateE.SetInitValue(dr("ValidDateE"))
            FI_Remark.SetInitValue(dr("Remark"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CompID.Updated = False
            FI_OrganReason.Updated = False
            FI_OrganType.Updated = False
            FI_ValidDate.Updated = False
            FI_Seq.Updated = False
            FI_ExecuteDate.Updated = False
            FI_WaitStatus.Updated = False
            FI_OrganID.Updated = False
            FI_OrganNameOld.Updated = False
            FI_OrganName.Updated = False
            FI_OrganEngName.Updated = False
            FI_VirtualFlag.Updated = False
            FI_UpOrganID.Updated = False
            FI_DeptID.Updated = False
            FI_GroupType.Updated = False
            FI_GroupID.Updated = False
            FI_Boss.Updated = False
            FI_BossCompID.Updated = False
            FI_BossType.Updated = False
            FI_BossTemporary.Updated = False
            FI_SecBoss.Updated = False
            FI_SecBossCompID.Updated = False
            FI_InValidFlag.Updated = False
            FI_ReportingEnd.Updated = False
            FI_Depth.Updated = False
            FI_BranchFlag.Updated = False
            FI_RoleCode.Updated = False
            FI_PersonPart.Updated = False
            FI_SecPersonPart.Updated = False
            FI_CheckPart.Updated = False
            FI_WorkSiteID.Updated = False
            FI_WorkTypeID.Updated = False
            FI_CostDeptID.Updated = False
            FI_CostType.Updated = False
            FI_AccountBranch.Updated = False
            FI_PositionID.Updated = False
            FI_CompareFlag.Updated = False
            FI_FlowOrganID.Updated = False
            FI_DelegateFlag.Updated = False
            FI_OrganNo.Updated = False
            FI_InvoiceNo.Updated = False
            FI_SortOrder.Updated = False
            FI_OrgType.Updated = False
            FI_BusinessType.Updated = False
            FI_ValidDateB.Updated = False
            FI_ValidDateE.Updated = False
            FI_Remark.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
        End Sub

        Public ReadOnly Property CompID As Field(Of String) 
            Get
                Return FI_CompID
            End Get
        End Property

        Public ReadOnly Property OrganReason As Field(Of String) 
            Get
                Return FI_OrganReason
            End Get
        End Property

        Public ReadOnly Property OrganType As Field(Of String) 
            Get
                Return FI_OrganType
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

        Public ReadOnly Property ExecuteDate As Field(Of Date) 
            Get
                Return FI_ExecuteDate
            End Get
        End Property

        Public ReadOnly Property WaitStatus As Field(Of String) 
            Get
                Return FI_WaitStatus
            End Get
        End Property

        Public ReadOnly Property OrganID As Field(Of String) 
            Get
                Return FI_OrganID
            End Get
        End Property

        Public ReadOnly Property OrganNameOld As Field(Of String) 
            Get
                Return FI_OrganNameOld
            End Get
        End Property

        Public ReadOnly Property OrganName As Field(Of String) 
            Get
                Return FI_OrganName
            End Get
        End Property

        Public ReadOnly Property OrganEngName As Field(Of String) 
            Get
                Return FI_OrganEngName
            End Get
        End Property

        Public ReadOnly Property VirtualFlag As Field(Of String) 
            Get
                Return FI_VirtualFlag
            End Get
        End Property

        Public ReadOnly Property UpOrganID As Field(Of String) 
            Get
                Return FI_UpOrganID
            End Get
        End Property

        Public ReadOnly Property DeptID As Field(Of String) 
            Get
                Return FI_DeptID
            End Get
        End Property

        Public ReadOnly Property GroupType As Field(Of String) 
            Get
                Return FI_GroupType
            End Get
        End Property

        Public ReadOnly Property GroupID As Field(Of String) 
            Get
                Return FI_GroupID
            End Get
        End Property

        Public ReadOnly Property Boss As Field(Of String) 
            Get
                Return FI_Boss
            End Get
        End Property

        Public ReadOnly Property BossCompID As Field(Of String) 
            Get
                Return FI_BossCompID
            End Get
        End Property

        Public ReadOnly Property BossType As Field(Of String) 
            Get
                Return FI_BossType
            End Get
        End Property

        Public ReadOnly Property BossTemporary As Field(Of String) 
            Get
                Return FI_BossTemporary
            End Get
        End Property

        Public ReadOnly Property SecBoss As Field(Of String) 
            Get
                Return FI_SecBoss
            End Get
        End Property

        Public ReadOnly Property SecBossCompID As Field(Of String) 
            Get
                Return FI_SecBossCompID
            End Get
        End Property

        Public ReadOnly Property InValidFlag As Field(Of String) 
            Get
                Return FI_InValidFlag
            End Get
        End Property

        Public ReadOnly Property ReportingEnd As Field(Of String) 
            Get
                Return FI_ReportingEnd
            End Get
        End Property

        Public ReadOnly Property Depth As Field(Of String) 
            Get
                Return FI_Depth
            End Get
        End Property

        Public ReadOnly Property BranchFlag As Field(Of String) 
            Get
                Return FI_BranchFlag
            End Get
        End Property

        Public ReadOnly Property RoleCode As Field(Of String) 
            Get
                Return FI_RoleCode
            End Get
        End Property

        Public ReadOnly Property PersonPart As Field(Of String) 
            Get
                Return FI_PersonPart
            End Get
        End Property

        Public ReadOnly Property SecPersonPart As Field(Of String) 
            Get
                Return FI_SecPersonPart
            End Get
        End Property

        Public ReadOnly Property CheckPart As Field(Of String) 
            Get
                Return FI_CheckPart
            End Get
        End Property

        Public ReadOnly Property WorkSiteID As Field(Of String) 
            Get
                Return FI_WorkSiteID
            End Get
        End Property

        Public ReadOnly Property WorkTypeID As Field(Of String) 
            Get
                Return FI_WorkTypeID
            End Get
        End Property

        Public ReadOnly Property CostDeptID As Field(Of String) 
            Get
                Return FI_CostDeptID
            End Get
        End Property

        Public ReadOnly Property CostType As Field(Of String) 
            Get
                Return FI_CostType
            End Get
        End Property

        Public ReadOnly Property AccountBranch As Field(Of String) 
            Get
                Return FI_AccountBranch
            End Get
        End Property

        Public ReadOnly Property PositionID As Field(Of String) 
            Get
                Return FI_PositionID
            End Get
        End Property

        Public ReadOnly Property CompareFlag As Field(Of String) 
            Get
                Return FI_CompareFlag
            End Get
        End Property

        Public ReadOnly Property FlowOrganID As Field(Of String) 
            Get
                Return FI_FlowOrganID
            End Get
        End Property

        Public ReadOnly Property DelegateFlag As Field(Of String) 
            Get
                Return FI_DelegateFlag
            End Get
        End Property

        Public ReadOnly Property OrganNo As Field(Of String) 
            Get
                Return FI_OrganNo
            End Get
        End Property

        Public ReadOnly Property InvoiceNo As Field(Of String) 
            Get
                Return FI_InvoiceNo
            End Get
        End Property

        Public ReadOnly Property SortOrder As Field(Of String) 
            Get
                Return FI_SortOrder
            End Get
        End Property

        Public ReadOnly Property OrgType As Field(Of String) 
            Get
                Return FI_OrgType
            End Get
        End Property

        Public ReadOnly Property BusinessType As Field(Of String) 
            Get
                Return FI_BusinessType
            End Get
        End Property

        Public ReadOnly Property ValidDateB As Field(Of Date) 
            Get
                Return FI_ValidDateB
            End Get
        End Property

        Public ReadOnly Property ValidDateE As Field(Of Date) 
            Get
                Return FI_ValidDateE
            End Get
        End Property

        Public ReadOnly Property Remark As Field(Of String) 
            Get
                Return FI_Remark
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

        Function FlowRoleCode() As Object
            Throw New NotImplementedException
        End Function

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal OrganizationWaitRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From OrganizationWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And OrganID = @OrganID")
            strSQL.AppendLine("And OrganReason = @OrganReason")
            strSQL.AppendLine("And OrganType = @OrganType")
            strSQL.AppendLine("And ValidDate = @ValidDate")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, OrganizationWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationWaitRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@OrganReason", DbType.String, OrganizationWaitRow.OrganReason.Value)
            db.AddInParameter(dbcmd, "@OrganType", DbType.String, OrganizationWaitRow.OrganType.Value)
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, OrganizationWaitRow.ValidDate.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, OrganizationWaitRow.Seq.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal OrganizationWaitRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From OrganizationWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And OrganID = @OrganID")
            strSQL.AppendLine("And OrganReason = @OrganReason")
            strSQL.AppendLine("And OrganType = @OrganType")
            strSQL.AppendLine("And ValidDate = @ValidDate")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, OrganizationWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationWaitRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@OrganReason", DbType.String, OrganizationWaitRow.OrganReason.Value)
            db.AddInParameter(dbcmd, "@OrganType", DbType.String, OrganizationWaitRow.OrganType.Value)
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, OrganizationWaitRow.ValidDate.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, OrganizationWaitRow.Seq.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal OrganizationWaitRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From OrganizationWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And OrganID = @OrganID")
            strSQL.AppendLine("And OrganReason = @OrganReason")
            strSQL.AppendLine("And OrganType = @OrganType")
            strSQL.AppendLine("And ValidDate = @ValidDate")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In OrganizationWaitRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                        db.AddInParameter(dbcmd, "@OrganReason", DbType.String, r.OrganReason.Value)
                        db.AddInParameter(dbcmd, "@OrganType", DbType.String, r.OrganType.Value)
                        db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, r.ValidDate.Value)
                        db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal OrganizationWaitRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From OrganizationWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And OrganID = @OrganID")
            strSQL.AppendLine("And OrganReason = @OrganReason")
            strSQL.AppendLine("And OrganType = @OrganType")
            strSQL.AppendLine("And ValidDate = @ValidDate")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In OrganizationWaitRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                db.AddInParameter(dbcmd, "@OrganReason", DbType.String, r.OrganReason.Value)
                db.AddInParameter(dbcmd, "@OrganType", DbType.String, r.OrganType.Value)
                db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, r.ValidDate.Value)
                db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal OrganizationWaitRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From OrganizationWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And OrganID = @OrganID")
            strSQL.AppendLine("And OrganReason = @OrganReason")
            strSQL.AppendLine("And OrganType = @OrganType")
            strSQL.AppendLine("And ValidDate = @ValidDate")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, OrganizationWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationWaitRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@OrganReason", DbType.String, OrganizationWaitRow.OrganReason.Value)
            db.AddInParameter(dbcmd, "@OrganType", DbType.String, OrganizationWaitRow.OrganType.Value)
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, OrganizationWaitRow.ValidDate.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, OrganizationWaitRow.Seq.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(OrganizationWaitRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From OrganizationWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And OrganID = @OrganID")
            strSQL.AppendLine("And OrganReason = @OrganReason")
            strSQL.AppendLine("And OrganType = @OrganType")
            strSQL.AppendLine("And ValidDate = @ValidDate")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, OrganizationWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationWaitRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@OrganReason", DbType.String, OrganizationWaitRow.OrganReason.Value)
            db.AddInParameter(dbcmd, "@OrganType", DbType.String, OrganizationWaitRow.OrganType.Value)
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, OrganizationWaitRow.ValidDate.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, OrganizationWaitRow.Seq.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal OrganizationWaitRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update OrganizationWait Set")
            For i As Integer = 0 To OrganizationWaitRow.FieldNames.Length - 1
                If Not OrganizationWaitRow.IsIdentityField(OrganizationWaitRow.FieldNames(i)) AndAlso OrganizationWaitRow.IsUpdated(OrganizationWaitRow.FieldNames(i)) AndAlso OrganizationWaitRow.CreateUpdateSQL(OrganizationWaitRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, OrganizationWaitRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And OrganID = @PKOrganID")
            strSQL.AppendLine("And OrganReason = @PKOrganReason")
            strSQL.AppendLine("And OrganType = @PKOrganType")
            strSQL.AppendLine("And ValidDate = @PKValidDate")
            strSQL.AppendLine("And Seq = @PKSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If OrganizationWaitRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, OrganizationWaitRow.CompID.Value)
            If OrganizationWaitRow.OrganReason.Updated Then db.AddInParameter(dbcmd, "@OrganReason", DbType.String, OrganizationWaitRow.OrganReason.Value)
            If OrganizationWaitRow.OrganType.Updated Then db.AddInParameter(dbcmd, "@OrganType", DbType.String, OrganizationWaitRow.OrganType.Value)
            If OrganizationWaitRow.ValidDate.Updated Then db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(OrganizationWaitRow.ValidDate.Value), Convert.ToDateTime("1900/1/1"), OrganizationWaitRow.ValidDate.Value))
            If OrganizationWaitRow.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, OrganizationWaitRow.Seq.Value)
            If OrganizationWaitRow.ExecuteDate.Updated Then db.AddInParameter(dbcmd, "@ExecuteDate", DbType.Date, IIf(IsDateTimeNull(OrganizationWaitRow.ExecuteDate.Value), Convert.ToDateTime("1900/1/1"), OrganizationWaitRow.ExecuteDate.Value))
            If OrganizationWaitRow.WaitStatus.Updated Then db.AddInParameter(dbcmd, "@WaitStatus", DbType.String, OrganizationWaitRow.WaitStatus.Value)
            If OrganizationWaitRow.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationWaitRow.OrganID.Value)
            If OrganizationWaitRow.OrganNameOld.Updated Then db.AddInParameter(dbcmd, "@OrganNameOld", DbType.String, OrganizationWaitRow.OrganNameOld.Value)
            If OrganizationWaitRow.OrganName.Updated Then db.AddInParameter(dbcmd, "@OrganName", DbType.String, OrganizationWaitRow.OrganName.Value)
            If OrganizationWaitRow.OrganEngName.Updated Then db.AddInParameter(dbcmd, "@OrganEngName", DbType.String, OrganizationWaitRow.OrganEngName.Value)
            If OrganizationWaitRow.VirtualFlag.Updated Then db.AddInParameter(dbcmd, "@VirtualFlag", DbType.String, OrganizationWaitRow.VirtualFlag.Value)
            If OrganizationWaitRow.UpOrganID.Updated Then db.AddInParameter(dbcmd, "@UpOrganID", DbType.String, OrganizationWaitRow.UpOrganID.Value)
            If OrganizationWaitRow.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, OrganizationWaitRow.DeptID.Value)
            If OrganizationWaitRow.GroupType.Updated Then db.AddInParameter(dbcmd, "@GroupType", DbType.String, OrganizationWaitRow.GroupType.Value)
            If OrganizationWaitRow.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, OrganizationWaitRow.GroupID.Value)
            If OrganizationWaitRow.Boss.Updated Then db.AddInParameter(dbcmd, "@Boss", DbType.String, OrganizationWaitRow.Boss.Value)
            If OrganizationWaitRow.BossCompID.Updated Then db.AddInParameter(dbcmd, "@BossCompID", DbType.String, OrganizationWaitRow.BossCompID.Value)
            If OrganizationWaitRow.BossType.Updated Then db.AddInParameter(dbcmd, "@BossType", DbType.String, OrganizationWaitRow.BossType.Value)
            If OrganizationWaitRow.BossTemporary.Updated Then db.AddInParameter(dbcmd, "@BossTemporary", DbType.String, OrganizationWaitRow.BossTemporary.Value)
            If OrganizationWaitRow.SecBoss.Updated Then db.AddInParameter(dbcmd, "@SecBoss", DbType.String, OrganizationWaitRow.SecBoss.Value)
            If OrganizationWaitRow.SecBossCompID.Updated Then db.AddInParameter(dbcmd, "@SecBossCompID", DbType.String, OrganizationWaitRow.SecBossCompID.Value)
            If OrganizationWaitRow.InValidFlag.Updated Then db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, OrganizationWaitRow.InValidFlag.Value)
            If OrganizationWaitRow.ReportingEnd.Updated Then db.AddInParameter(dbcmd, "@ReportingEnd", DbType.String, OrganizationWaitRow.ReportingEnd.Value)
            If OrganizationWaitRow.Depth.Updated Then db.AddInParameter(dbcmd, "@Depth", DbType.String, OrganizationWaitRow.Depth.Value)
            If OrganizationWaitRow.BranchFlag.Updated Then db.AddInParameter(dbcmd, "@BranchFlag", DbType.String, OrganizationWaitRow.BranchFlag.Value)
            If OrganizationWaitRow.RoleCode.Updated Then db.AddInParameter(dbcmd, "@RoleCode", DbType.String, OrganizationWaitRow.RoleCode.Value)
            If OrganizationWaitRow.PersonPart.Updated Then db.AddInParameter(dbcmd, "@PersonPart", DbType.String, OrganizationWaitRow.PersonPart.Value)
            If OrganizationWaitRow.SecPersonPart.Updated Then db.AddInParameter(dbcmd, "@SecPersonPart", DbType.String, OrganizationWaitRow.SecPersonPart.Value)
            If OrganizationWaitRow.CheckPart.Updated Then db.AddInParameter(dbcmd, "@CheckPart", DbType.String, OrganizationWaitRow.CheckPart.Value)
            If OrganizationWaitRow.WorkSiteID.Updated Then db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, OrganizationWaitRow.WorkSiteID.Value)
            If OrganizationWaitRow.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, OrganizationWaitRow.WorkTypeID.Value)
            If OrganizationWaitRow.CostDeptID.Updated Then db.AddInParameter(dbcmd, "@CostDeptID", DbType.String, OrganizationWaitRow.CostDeptID.Value)
            If OrganizationWaitRow.CostType.Updated Then db.AddInParameter(dbcmd, "@CostType", DbType.String, OrganizationWaitRow.CostType.Value)
            If OrganizationWaitRow.AccountBranch.Updated Then db.AddInParameter(dbcmd, "@AccountBranch", DbType.String, OrganizationWaitRow.AccountBranch.Value)
            If OrganizationWaitRow.PositionID.Updated Then db.AddInParameter(dbcmd, "@PositionID", DbType.String, OrganizationWaitRow.PositionID.Value)
            If OrganizationWaitRow.CompareFlag.Updated Then db.AddInParameter(dbcmd, "@CompareFlag", DbType.String, OrganizationWaitRow.CompareFlag.Value)
            If OrganizationWaitRow.FlowOrganID.Updated Then db.AddInParameter(dbcmd, "@FlowOrganID", DbType.String, OrganizationWaitRow.FlowOrganID.Value)
            If OrganizationWaitRow.DelegateFlag.Updated Then db.AddInParameter(dbcmd, "@DelegateFlag", DbType.String, OrganizationWaitRow.DelegateFlag.Value)
            If OrganizationWaitRow.OrganNo.Updated Then db.AddInParameter(dbcmd, "@OrganNo", DbType.String, OrganizationWaitRow.OrganNo.Value)
            If OrganizationWaitRow.InvoiceNo.Updated Then db.AddInParameter(dbcmd, "@InvoiceNo", DbType.String, OrganizationWaitRow.InvoiceNo.Value)
            If OrganizationWaitRow.SortOrder.Updated Then db.AddInParameter(dbcmd, "@SortOrder", DbType.String, OrganizationWaitRow.SortOrder.Value)
            If OrganizationWaitRow.OrgType.Updated Then db.AddInParameter(dbcmd, "@OrgType", DbType.String, OrganizationWaitRow.OrgType.Value)
            If OrganizationWaitRow.BusinessType.Updated Then db.AddInParameter(dbcmd, "@BusinessType", DbType.String, OrganizationWaitRow.BusinessType.Value)
            If OrganizationWaitRow.ValidDateB.Updated Then db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(OrganizationWaitRow.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), OrganizationWaitRow.ValidDateB.Value))
            If OrganizationWaitRow.ValidDateE.Updated Then db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(OrganizationWaitRow.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), OrganizationWaitRow.ValidDateE.Value))
            If OrganizationWaitRow.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, OrganizationWaitRow.Remark.Value)
            If OrganizationWaitRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, OrganizationWaitRow.LastChgComp.Value)
            If OrganizationWaitRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, OrganizationWaitRow.LastChgID.Value)
            If OrganizationWaitRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(OrganizationWaitRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), OrganizationWaitRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(OrganizationWaitRow.LoadFromDataRow, OrganizationWaitRow.CompID.OldValue, OrganizationWaitRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKOrganID", DbType.String, IIf(OrganizationWaitRow.LoadFromDataRow, OrganizationWaitRow.OrganID.OldValue, OrganizationWaitRow.OrganID.Value))
            db.AddInParameter(dbcmd, "@PKOrganReason", DbType.String, IIf(OrganizationWaitRow.LoadFromDataRow, OrganizationWaitRow.OrganReason.OldValue, OrganizationWaitRow.OrganReason.Value))
            db.AddInParameter(dbcmd, "@PKOrganType", DbType.String, IIf(OrganizationWaitRow.LoadFromDataRow, OrganizationWaitRow.OrganType.OldValue, OrganizationWaitRow.OrganType.Value))
            db.AddInParameter(dbcmd, "@PKValidDate", DbType.Date, IIf(OrganizationWaitRow.LoadFromDataRow, OrganizationWaitRow.ValidDate.OldValue, OrganizationWaitRow.ValidDate.Value))
            db.AddInParameter(dbcmd, "@PKSeq", DbType.Int32, IIf(OrganizationWaitRow.LoadFromDataRow, OrganizationWaitRow.Seq.OldValue, OrganizationWaitRow.Seq.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal OrganizationWaitRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update OrganizationWait Set")
            For i As Integer = 0 To OrganizationWaitRow.FieldNames.Length - 1
                If Not OrganizationWaitRow.IsIdentityField(OrganizationWaitRow.FieldNames(i)) AndAlso OrganizationWaitRow.IsUpdated(OrganizationWaitRow.FieldNames(i)) AndAlso OrganizationWaitRow.CreateUpdateSQL(OrganizationWaitRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, OrganizationWaitRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And OrganID = @PKOrganID")
            strSQL.AppendLine("And OrganReason = @PKOrganReason")
            strSQL.AppendLine("And OrganType = @PKOrganType")
            strSQL.AppendLine("And ValidDate = @PKValidDate")
            strSQL.AppendLine("And Seq = @PKSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If OrganizationWaitRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, OrganizationWaitRow.CompID.Value)
            If OrganizationWaitRow.OrganReason.Updated Then db.AddInParameter(dbcmd, "@OrganReason", DbType.String, OrganizationWaitRow.OrganReason.Value)
            If OrganizationWaitRow.OrganType.Updated Then db.AddInParameter(dbcmd, "@OrganType", DbType.String, OrganizationWaitRow.OrganType.Value)
            If OrganizationWaitRow.ValidDate.Updated Then db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(OrganizationWaitRow.ValidDate.Value), Convert.ToDateTime("1900/1/1"), OrganizationWaitRow.ValidDate.Value))
            If OrganizationWaitRow.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, OrganizationWaitRow.Seq.Value)
            If OrganizationWaitRow.ExecuteDate.Updated Then db.AddInParameter(dbcmd, "@ExecuteDate", DbType.Date, IIf(IsDateTimeNull(OrganizationWaitRow.ExecuteDate.Value), Convert.ToDateTime("1900/1/1"), OrganizationWaitRow.ExecuteDate.Value))
            If OrganizationWaitRow.WaitStatus.Updated Then db.AddInParameter(dbcmd, "@WaitStatus", DbType.String, OrganizationWaitRow.WaitStatus.Value)
            If OrganizationWaitRow.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationWaitRow.OrganID.Value)
            If OrganizationWaitRow.OrganNameOld.Updated Then db.AddInParameter(dbcmd, "@OrganNameOld", DbType.String, OrganizationWaitRow.OrganNameOld.Value)
            If OrganizationWaitRow.OrganName.Updated Then db.AddInParameter(dbcmd, "@OrganName", DbType.String, OrganizationWaitRow.OrganName.Value)
            If OrganizationWaitRow.OrganEngName.Updated Then db.AddInParameter(dbcmd, "@OrganEngName", DbType.String, OrganizationWaitRow.OrganEngName.Value)
            If OrganizationWaitRow.VirtualFlag.Updated Then db.AddInParameter(dbcmd, "@VirtualFlag", DbType.String, OrganizationWaitRow.VirtualFlag.Value)
            If OrganizationWaitRow.UpOrganID.Updated Then db.AddInParameter(dbcmd, "@UpOrganID", DbType.String, OrganizationWaitRow.UpOrganID.Value)
            If OrganizationWaitRow.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, OrganizationWaitRow.DeptID.Value)
            If OrganizationWaitRow.GroupType.Updated Then db.AddInParameter(dbcmd, "@GroupType", DbType.String, OrganizationWaitRow.GroupType.Value)
            If OrganizationWaitRow.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, OrganizationWaitRow.GroupID.Value)
            If OrganizationWaitRow.Boss.Updated Then db.AddInParameter(dbcmd, "@Boss", DbType.String, OrganizationWaitRow.Boss.Value)
            If OrganizationWaitRow.BossCompID.Updated Then db.AddInParameter(dbcmd, "@BossCompID", DbType.String, OrganizationWaitRow.BossCompID.Value)
            If OrganizationWaitRow.BossType.Updated Then db.AddInParameter(dbcmd, "@BossType", DbType.String, OrganizationWaitRow.BossType.Value)
            If OrganizationWaitRow.BossTemporary.Updated Then db.AddInParameter(dbcmd, "@BossTemporary", DbType.String, OrganizationWaitRow.BossTemporary.Value)
            If OrganizationWaitRow.SecBoss.Updated Then db.AddInParameter(dbcmd, "@SecBoss", DbType.String, OrganizationWaitRow.SecBoss.Value)
            If OrganizationWaitRow.SecBossCompID.Updated Then db.AddInParameter(dbcmd, "@SecBossCompID", DbType.String, OrganizationWaitRow.SecBossCompID.Value)
            If OrganizationWaitRow.InValidFlag.Updated Then db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, OrganizationWaitRow.InValidFlag.Value)
            If OrganizationWaitRow.ReportingEnd.Updated Then db.AddInParameter(dbcmd, "@ReportingEnd", DbType.String, OrganizationWaitRow.ReportingEnd.Value)
            If OrganizationWaitRow.Depth.Updated Then db.AddInParameter(dbcmd, "@Depth", DbType.String, OrganizationWaitRow.Depth.Value)
            If OrganizationWaitRow.BranchFlag.Updated Then db.AddInParameter(dbcmd, "@BranchFlag", DbType.String, OrganizationWaitRow.BranchFlag.Value)
            If OrganizationWaitRow.RoleCode.Updated Then db.AddInParameter(dbcmd, "@RoleCode", DbType.String, OrganizationWaitRow.RoleCode.Value)
            If OrganizationWaitRow.PersonPart.Updated Then db.AddInParameter(dbcmd, "@PersonPart", DbType.String, OrganizationWaitRow.PersonPart.Value)
            If OrganizationWaitRow.SecPersonPart.Updated Then db.AddInParameter(dbcmd, "@SecPersonPart", DbType.String, OrganizationWaitRow.SecPersonPart.Value)
            If OrganizationWaitRow.CheckPart.Updated Then db.AddInParameter(dbcmd, "@CheckPart", DbType.String, OrganizationWaitRow.CheckPart.Value)
            If OrganizationWaitRow.WorkSiteID.Updated Then db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, OrganizationWaitRow.WorkSiteID.Value)
            If OrganizationWaitRow.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, OrganizationWaitRow.WorkTypeID.Value)
            If OrganizationWaitRow.CostDeptID.Updated Then db.AddInParameter(dbcmd, "@CostDeptID", DbType.String, OrganizationWaitRow.CostDeptID.Value)
            If OrganizationWaitRow.CostType.Updated Then db.AddInParameter(dbcmd, "@CostType", DbType.String, OrganizationWaitRow.CostType.Value)
            If OrganizationWaitRow.AccountBranch.Updated Then db.AddInParameter(dbcmd, "@AccountBranch", DbType.String, OrganizationWaitRow.AccountBranch.Value)
            If OrganizationWaitRow.PositionID.Updated Then db.AddInParameter(dbcmd, "@PositionID", DbType.String, OrganizationWaitRow.PositionID.Value)
            If OrganizationWaitRow.CompareFlag.Updated Then db.AddInParameter(dbcmd, "@CompareFlag", DbType.String, OrganizationWaitRow.CompareFlag.Value)
            If OrganizationWaitRow.FlowOrganID.Updated Then db.AddInParameter(dbcmd, "@FlowOrganID", DbType.String, OrganizationWaitRow.FlowOrganID.Value)
            If OrganizationWaitRow.DelegateFlag.Updated Then db.AddInParameter(dbcmd, "@DelegateFlag", DbType.String, OrganizationWaitRow.DelegateFlag.Value)
            If OrganizationWaitRow.OrganNo.Updated Then db.AddInParameter(dbcmd, "@OrganNo", DbType.String, OrganizationWaitRow.OrganNo.Value)
            If OrganizationWaitRow.InvoiceNo.Updated Then db.AddInParameter(dbcmd, "@InvoiceNo", DbType.String, OrganizationWaitRow.InvoiceNo.Value)
            If OrganizationWaitRow.SortOrder.Updated Then db.AddInParameter(dbcmd, "@SortOrder", DbType.String, OrganizationWaitRow.SortOrder.Value)
            If OrganizationWaitRow.OrgType.Updated Then db.AddInParameter(dbcmd, "@OrgType", DbType.String, OrganizationWaitRow.OrgType.Value)
            If OrganizationWaitRow.BusinessType.Updated Then db.AddInParameter(dbcmd, "@BusinessType", DbType.String, OrganizationWaitRow.BusinessType.Value)
            If OrganizationWaitRow.ValidDateB.Updated Then db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(OrganizationWaitRow.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), OrganizationWaitRow.ValidDateB.Value))
            If OrganizationWaitRow.ValidDateE.Updated Then db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(OrganizationWaitRow.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), OrganizationWaitRow.ValidDateE.Value))
            If OrganizationWaitRow.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, OrganizationWaitRow.Remark.Value)
            If OrganizationWaitRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, OrganizationWaitRow.LastChgComp.Value)
            If OrganizationWaitRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, OrganizationWaitRow.LastChgID.Value)
            If OrganizationWaitRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(OrganizationWaitRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), OrganizationWaitRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(OrganizationWaitRow.LoadFromDataRow, OrganizationWaitRow.CompID.OldValue, OrganizationWaitRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKOrganID", DbType.String, IIf(OrganizationWaitRow.LoadFromDataRow, OrganizationWaitRow.OrganID.OldValue, OrganizationWaitRow.OrganID.Value))
            db.AddInParameter(dbcmd, "@PKOrganReason", DbType.String, IIf(OrganizationWaitRow.LoadFromDataRow, OrganizationWaitRow.OrganReason.OldValue, OrganizationWaitRow.OrganReason.Value))
            db.AddInParameter(dbcmd, "@PKOrganType", DbType.String, IIf(OrganizationWaitRow.LoadFromDataRow, OrganizationWaitRow.OrganType.OldValue, OrganizationWaitRow.OrganType.Value))
            db.AddInParameter(dbcmd, "@PKValidDate", DbType.Date, IIf(OrganizationWaitRow.LoadFromDataRow, OrganizationWaitRow.ValidDate.OldValue, OrganizationWaitRow.ValidDate.Value))
            db.AddInParameter(dbcmd, "@PKSeq", DbType.Int32, IIf(OrganizationWaitRow.LoadFromDataRow, OrganizationWaitRow.Seq.OldValue, OrganizationWaitRow.Seq.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal OrganizationWaitRow As Row()) As Integer
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
                    For Each r As Row In OrganizationWaitRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update OrganizationWait Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where CompID = @PKCompID")
                        strSQL.AppendLine("And OrganID = @PKOrganID")
                        strSQL.AppendLine("And OrganReason = @PKOrganReason")
                        strSQL.AppendLine("And OrganType = @PKOrganType")
                        strSQL.AppendLine("And ValidDate = @PKValidDate")
                        strSQL.AppendLine("And Seq = @PKSeq")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.OrganReason.Updated Then db.AddInParameter(dbcmd, "@OrganReason", DbType.String, r.OrganReason.Value)
                        If r.OrganType.Updated Then db.AddInParameter(dbcmd, "@OrganType", DbType.String, r.OrganType.Value)
                        If r.ValidDate.Updated Then db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(r.ValidDate.Value), Convert.ToDateTime("1900/1/1"), r.ValidDate.Value))
                        If r.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                        If r.ExecuteDate.Updated Then db.AddInParameter(dbcmd, "@ExecuteDate", DbType.Date, IIf(IsDateTimeNull(r.ExecuteDate.Value), Convert.ToDateTime("1900/1/1"), r.ExecuteDate.Value))
                        If r.WaitStatus.Updated Then db.AddInParameter(dbcmd, "@WaitStatus", DbType.String, r.WaitStatus.Value)
                        If r.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                        If r.OrganNameOld.Updated Then db.AddInParameter(dbcmd, "@OrganNameOld", DbType.String, r.OrganNameOld.Value)
                        If r.OrganName.Updated Then db.AddInParameter(dbcmd, "@OrganName", DbType.String, r.OrganName.Value)
                        If r.OrganEngName.Updated Then db.AddInParameter(dbcmd, "@OrganEngName", DbType.String, r.OrganEngName.Value)
                        If r.VirtualFlag.Updated Then db.AddInParameter(dbcmd, "@VirtualFlag", DbType.String, r.VirtualFlag.Value)
                        If r.UpOrganID.Updated Then db.AddInParameter(dbcmd, "@UpOrganID", DbType.String, r.UpOrganID.Value)
                        If r.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                        If r.GroupType.Updated Then db.AddInParameter(dbcmd, "@GroupType", DbType.String, r.GroupType.Value)
                        If r.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                        If r.Boss.Updated Then db.AddInParameter(dbcmd, "@Boss", DbType.String, r.Boss.Value)
                        If r.BossCompID.Updated Then db.AddInParameter(dbcmd, "@BossCompID", DbType.String, r.BossCompID.Value)
                        If r.BossType.Updated Then db.AddInParameter(dbcmd, "@BossType", DbType.String, r.BossType.Value)
                        If r.BossTemporary.Updated Then db.AddInParameter(dbcmd, "@BossTemporary", DbType.String, r.BossTemporary.Value)
                        If r.SecBoss.Updated Then db.AddInParameter(dbcmd, "@SecBoss", DbType.String, r.SecBoss.Value)
                        If r.SecBossCompID.Updated Then db.AddInParameter(dbcmd, "@SecBossCompID", DbType.String, r.SecBossCompID.Value)
                        If r.InValidFlag.Updated Then db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, r.InValidFlag.Value)
                        If r.ReportingEnd.Updated Then db.AddInParameter(dbcmd, "@ReportingEnd", DbType.String, r.ReportingEnd.Value)
                        If r.Depth.Updated Then db.AddInParameter(dbcmd, "@Depth", DbType.String, r.Depth.Value)
                        If r.BranchFlag.Updated Then db.AddInParameter(dbcmd, "@BranchFlag", DbType.String, r.BranchFlag.Value)
                        If r.RoleCode.Updated Then db.AddInParameter(dbcmd, "@RoleCode", DbType.String, r.RoleCode.Value)
                        If r.PersonPart.Updated Then db.AddInParameter(dbcmd, "@PersonPart", DbType.String, r.PersonPart.Value)
                        If r.SecPersonPart.Updated Then db.AddInParameter(dbcmd, "@SecPersonPart", DbType.String, r.SecPersonPart.Value)
                        If r.CheckPart.Updated Then db.AddInParameter(dbcmd, "@CheckPart", DbType.String, r.CheckPart.Value)
                        If r.WorkSiteID.Updated Then db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, r.WorkSiteID.Value)
                        If r.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                        If r.CostDeptID.Updated Then db.AddInParameter(dbcmd, "@CostDeptID", DbType.String, r.CostDeptID.Value)
                        If r.CostType.Updated Then db.AddInParameter(dbcmd, "@CostType", DbType.String, r.CostType.Value)
                        If r.AccountBranch.Updated Then db.AddInParameter(dbcmd, "@AccountBranch", DbType.String, r.AccountBranch.Value)
                        If r.PositionID.Updated Then db.AddInParameter(dbcmd, "@PositionID", DbType.String, r.PositionID.Value)
                        If r.CompareFlag.Updated Then db.AddInParameter(dbcmd, "@CompareFlag", DbType.String, r.CompareFlag.Value)
                        If r.FlowOrganID.Updated Then db.AddInParameter(dbcmd, "@FlowOrganID", DbType.String, r.FlowOrganID.Value)
                        If r.DelegateFlag.Updated Then db.AddInParameter(dbcmd, "@DelegateFlag", DbType.String, r.DelegateFlag.Value)
                        If r.OrganNo.Updated Then db.AddInParameter(dbcmd, "@OrganNo", DbType.String, r.OrganNo.Value)
                        If r.InvoiceNo.Updated Then db.AddInParameter(dbcmd, "@InvoiceNo", DbType.String, r.InvoiceNo.Value)
                        If r.SortOrder.Updated Then db.AddInParameter(dbcmd, "@SortOrder", DbType.String, r.SortOrder.Value)
                        If r.OrgType.Updated Then db.AddInParameter(dbcmd, "@OrgType", DbType.String, r.OrgType.Value)
                        If r.BusinessType.Updated Then db.AddInParameter(dbcmd, "@BusinessType", DbType.String, r.BusinessType.Value)
                        If r.ValidDateB.Updated Then db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(r.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateB.Value))
                        If r.ValidDateE.Updated Then db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(r.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateE.Value))
                        If r.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                        db.AddInParameter(dbcmd, "@PKOrganID", DbType.String, IIf(r.LoadFromDataRow, r.OrganID.OldValue, r.OrganID.Value))
                        db.AddInParameter(dbcmd, "@PKOrganReason", DbType.String, IIf(r.LoadFromDataRow, r.OrganReason.OldValue, r.OrganReason.Value))
                        db.AddInParameter(dbcmd, "@PKOrganType", DbType.String, IIf(r.LoadFromDataRow, r.OrganType.OldValue, r.OrganType.Value))
                        db.AddInParameter(dbcmd, "@PKValidDate", DbType.Date, IIf(r.LoadFromDataRow, r.ValidDate.OldValue, r.ValidDate.Value))
                        db.AddInParameter(dbcmd, "@PKSeq", DbType.Int32, IIf(r.LoadFromDataRow, r.Seq.OldValue, r.Seq.Value))

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

        Public Function Update(ByVal OrganizationWaitRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In OrganizationWaitRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update OrganizationWait Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where CompID = @PKCompID")
                strSQL.AppendLine("And OrganID = @PKOrganID")
                strSQL.AppendLine("And OrganReason = @PKOrganReason")
                strSQL.AppendLine("And OrganType = @PKOrganType")
                strSQL.AppendLine("And ValidDate = @PKValidDate")
                strSQL.AppendLine("And Seq = @PKSeq")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.OrganReason.Updated Then db.AddInParameter(dbcmd, "@OrganReason", DbType.String, r.OrganReason.Value)
                If r.OrganType.Updated Then db.AddInParameter(dbcmd, "@OrganType", DbType.String, r.OrganType.Value)
                If r.ValidDate.Updated Then db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(r.ValidDate.Value), Convert.ToDateTime("1900/1/1"), r.ValidDate.Value))
                If r.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                If r.ExecuteDate.Updated Then db.AddInParameter(dbcmd, "@ExecuteDate", DbType.Date, IIf(IsDateTimeNull(r.ExecuteDate.Value), Convert.ToDateTime("1900/1/1"), r.ExecuteDate.Value))
                If r.WaitStatus.Updated Then db.AddInParameter(dbcmd, "@WaitStatus", DbType.String, r.WaitStatus.Value)
                If r.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                If r.OrganNameOld.Updated Then db.AddInParameter(dbcmd, "@OrganNameOld", DbType.String, r.OrganNameOld.Value)
                If r.OrganName.Updated Then db.AddInParameter(dbcmd, "@OrganName", DbType.String, r.OrganName.Value)
                If r.OrganEngName.Updated Then db.AddInParameter(dbcmd, "@OrganEngName", DbType.String, r.OrganEngName.Value)
                If r.VirtualFlag.Updated Then db.AddInParameter(dbcmd, "@VirtualFlag", DbType.String, r.VirtualFlag.Value)
                If r.UpOrganID.Updated Then db.AddInParameter(dbcmd, "@UpOrganID", DbType.String, r.UpOrganID.Value)
                If r.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                If r.GroupType.Updated Then db.AddInParameter(dbcmd, "@GroupType", DbType.String, r.GroupType.Value)
                If r.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                If r.Boss.Updated Then db.AddInParameter(dbcmd, "@Boss", DbType.String, r.Boss.Value)
                If r.BossCompID.Updated Then db.AddInParameter(dbcmd, "@BossCompID", DbType.String, r.BossCompID.Value)
                If r.BossType.Updated Then db.AddInParameter(dbcmd, "@BossType", DbType.String, r.BossType.Value)
                If r.BossTemporary.Updated Then db.AddInParameter(dbcmd, "@BossTemporary", DbType.String, r.BossTemporary.Value)
                If r.SecBoss.Updated Then db.AddInParameter(dbcmd, "@SecBoss", DbType.String, r.SecBoss.Value)
                If r.SecBossCompID.Updated Then db.AddInParameter(dbcmd, "@SecBossCompID", DbType.String, r.SecBossCompID.Value)
                If r.InValidFlag.Updated Then db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, r.InValidFlag.Value)
                If r.ReportingEnd.Updated Then db.AddInParameter(dbcmd, "@ReportingEnd", DbType.String, r.ReportingEnd.Value)
                If r.Depth.Updated Then db.AddInParameter(dbcmd, "@Depth", DbType.String, r.Depth.Value)
                If r.BranchFlag.Updated Then db.AddInParameter(dbcmd, "@BranchFlag", DbType.String, r.BranchFlag.Value)
                If r.RoleCode.Updated Then db.AddInParameter(dbcmd, "@RoleCode", DbType.String, r.RoleCode.Value)
                If r.PersonPart.Updated Then db.AddInParameter(dbcmd, "@PersonPart", DbType.String, r.PersonPart.Value)
                If r.SecPersonPart.Updated Then db.AddInParameter(dbcmd, "@SecPersonPart", DbType.String, r.SecPersonPart.Value)
                If r.CheckPart.Updated Then db.AddInParameter(dbcmd, "@CheckPart", DbType.String, r.CheckPart.Value)
                If r.WorkSiteID.Updated Then db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, r.WorkSiteID.Value)
                If r.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                If r.CostDeptID.Updated Then db.AddInParameter(dbcmd, "@CostDeptID", DbType.String, r.CostDeptID.Value)
                If r.CostType.Updated Then db.AddInParameter(dbcmd, "@CostType", DbType.String, r.CostType.Value)
                If r.AccountBranch.Updated Then db.AddInParameter(dbcmd, "@AccountBranch", DbType.String, r.AccountBranch.Value)
                If r.PositionID.Updated Then db.AddInParameter(dbcmd, "@PositionID", DbType.String, r.PositionID.Value)
                If r.CompareFlag.Updated Then db.AddInParameter(dbcmd, "@CompareFlag", DbType.String, r.CompareFlag.Value)
                If r.FlowOrganID.Updated Then db.AddInParameter(dbcmd, "@FlowOrganID", DbType.String, r.FlowOrganID.Value)
                If r.DelegateFlag.Updated Then db.AddInParameter(dbcmd, "@DelegateFlag", DbType.String, r.DelegateFlag.Value)
                If r.OrganNo.Updated Then db.AddInParameter(dbcmd, "@OrganNo", DbType.String, r.OrganNo.Value)
                If r.InvoiceNo.Updated Then db.AddInParameter(dbcmd, "@InvoiceNo", DbType.String, r.InvoiceNo.Value)
                If r.SortOrder.Updated Then db.AddInParameter(dbcmd, "@SortOrder", DbType.String, r.SortOrder.Value)
                If r.OrgType.Updated Then db.AddInParameter(dbcmd, "@OrgType", DbType.String, r.OrgType.Value)
                If r.BusinessType.Updated Then db.AddInParameter(dbcmd, "@BusinessType", DbType.String, r.BusinessType.Value)
                If r.ValidDateB.Updated Then db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(r.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateB.Value))
                If r.ValidDateE.Updated Then db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(r.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateE.Value))
                If r.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                db.AddInParameter(dbcmd, "@PKOrganID", DbType.String, IIf(r.LoadFromDataRow, r.OrganID.OldValue, r.OrganID.Value))
                db.AddInParameter(dbcmd, "@PKOrganReason", DbType.String, IIf(r.LoadFromDataRow, r.OrganReason.OldValue, r.OrganReason.Value))
                db.AddInParameter(dbcmd, "@PKOrganType", DbType.String, IIf(r.LoadFromDataRow, r.OrganType.OldValue, r.OrganType.Value))
                db.AddInParameter(dbcmd, "@PKValidDate", DbType.Date, IIf(r.LoadFromDataRow, r.ValidDate.OldValue, r.ValidDate.Value))
                db.AddInParameter(dbcmd, "@PKSeq", DbType.Int32, IIf(r.LoadFromDataRow, r.Seq.OldValue, r.Seq.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal OrganizationWaitRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From OrganizationWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And OrganID = @OrganID")
            strSQL.AppendLine("And OrganReason = @OrganReason")
            strSQL.AppendLine("And OrganType = @OrganType")
            strSQL.AppendLine("And ValidDate = @ValidDate")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, OrganizationWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationWaitRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@OrganReason", DbType.String, OrganizationWaitRow.OrganReason.Value)
            db.AddInParameter(dbcmd, "@OrganType", DbType.String, OrganizationWaitRow.OrganType.Value)
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, OrganizationWaitRow.ValidDate.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, OrganizationWaitRow.Seq.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal OrganizationWaitRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From OrganizationWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And OrganID = @OrganID")
            strSQL.AppendLine("And OrganReason = @OrganReason")
            strSQL.AppendLine("And OrganType = @OrganType")
            strSQL.AppendLine("And ValidDate = @ValidDate")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, OrganizationWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationWaitRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@OrganReason", DbType.String, OrganizationWaitRow.OrganReason.Value)
            db.AddInParameter(dbcmd, "@OrganType", DbType.String, OrganizationWaitRow.OrganType.Value)
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, OrganizationWaitRow.ValidDate.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, OrganizationWaitRow.Seq.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From OrganizationWait")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal OrganizationWaitRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into OrganizationWait")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, OrganReason, OrganType, ValidDate, Seq, ExecuteDate, WaitStatus, OrganID, OrganNameOld,")
            strSQL.AppendLine("    OrganName, OrganEngName, VirtualFlag, UpOrganID, DeptID, GroupType, GroupID, Boss,")
            strSQL.AppendLine("    BossCompID, BossType, BossTemporary, SecBoss, SecBossCompID, InValidFlag, ReportingEnd,")
            strSQL.AppendLine("    Depth, BranchFlag, RoleCode, PersonPart, SecPersonPart, CheckPart, WorkSiteID, WorkTypeID,")
            strSQL.AppendLine("    CostDeptID, CostType, AccountBranch, PositionID, CompareFlag, FlowOrganID, DelegateFlag,")
            strSQL.AppendLine("    OrganNo, InvoiceNo, SortOrder, OrgType, BusinessType, ValidDateB, ValidDateE, Remark,")
            strSQL.AppendLine("    LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @OrganReason, @OrganType, @ValidDate, @Seq, @ExecuteDate, @WaitStatus, @OrganID, @OrganNameOld,")
            strSQL.AppendLine("    @OrganName, @OrganEngName, @VirtualFlag, @UpOrganID, @DeptID, @GroupType, @GroupID, @Boss,")
            strSQL.AppendLine("    @BossCompID, @BossType, @BossTemporary, @SecBoss, @SecBossCompID, @InValidFlag, @ReportingEnd,")
            strSQL.AppendLine("    @Depth, @BranchFlag, @RoleCode, @PersonPart, @SecPersonPart, @CheckPart, @WorkSiteID, @WorkTypeID,")
            strSQL.AppendLine("    @CostDeptID, @CostType, @AccountBranch, @PositionID, @CompareFlag, @FlowOrganID, @DelegateFlag,")
            strSQL.AppendLine("    @OrganNo, @InvoiceNo, @SortOrder, @OrgType, @BusinessType, @ValidDateB, @ValidDateE, @Remark,")
            strSQL.AppendLine("    @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, OrganizationWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@OrganReason", DbType.String, OrganizationWaitRow.OrganReason.Value)
            db.AddInParameter(dbcmd, "@OrganType", DbType.String, OrganizationWaitRow.OrganType.Value)
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(OrganizationWaitRow.ValidDate.Value), Convert.ToDateTime("1900/1/1"), OrganizationWaitRow.ValidDate.Value))
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, OrganizationWaitRow.Seq.Value)
            db.AddInParameter(dbcmd, "@ExecuteDate", DbType.Date, IIf(IsDateTimeNull(OrganizationWaitRow.ExecuteDate.Value), Convert.ToDateTime("1900/1/1"), OrganizationWaitRow.ExecuteDate.Value))
            db.AddInParameter(dbcmd, "@WaitStatus", DbType.String, OrganizationWaitRow.WaitStatus.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationWaitRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@OrganNameOld", DbType.String, OrganizationWaitRow.OrganNameOld.Value)
            db.AddInParameter(dbcmd, "@OrganName", DbType.String, OrganizationWaitRow.OrganName.Value)
            db.AddInParameter(dbcmd, "@OrganEngName", DbType.String, OrganizationWaitRow.OrganEngName.Value)
            db.AddInParameter(dbcmd, "@VirtualFlag", DbType.String, OrganizationWaitRow.VirtualFlag.Value)
            db.AddInParameter(dbcmd, "@UpOrganID", DbType.String, OrganizationWaitRow.UpOrganID.Value)
            db.AddInParameter(dbcmd, "@DeptID", DbType.String, OrganizationWaitRow.DeptID.Value)
            db.AddInParameter(dbcmd, "@GroupType", DbType.String, OrganizationWaitRow.GroupType.Value)
            db.AddInParameter(dbcmd, "@GroupID", DbType.String, OrganizationWaitRow.GroupID.Value)
            db.AddInParameter(dbcmd, "@Boss", DbType.String, OrganizationWaitRow.Boss.Value)
            db.AddInParameter(dbcmd, "@BossCompID", DbType.String, OrganizationWaitRow.BossCompID.Value)
            db.AddInParameter(dbcmd, "@BossType", DbType.String, OrganizationWaitRow.BossType.Value)
            db.AddInParameter(dbcmd, "@BossTemporary", DbType.String, OrganizationWaitRow.BossTemporary.Value)
            db.AddInParameter(dbcmd, "@SecBoss", DbType.String, OrganizationWaitRow.SecBoss.Value)
            db.AddInParameter(dbcmd, "@SecBossCompID", DbType.String, OrganizationWaitRow.SecBossCompID.Value)
            db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, OrganizationWaitRow.InValidFlag.Value)
            db.AddInParameter(dbcmd, "@ReportingEnd", DbType.String, OrganizationWaitRow.ReportingEnd.Value)
            db.AddInParameter(dbcmd, "@Depth", DbType.String, OrganizationWaitRow.Depth.Value)
            db.AddInParameter(dbcmd, "@BranchFlag", DbType.String, OrganizationWaitRow.BranchFlag.Value)
            db.AddInParameter(dbcmd, "@RoleCode", DbType.String, OrganizationWaitRow.RoleCode.Value)
            db.AddInParameter(dbcmd, "@PersonPart", DbType.String, OrganizationWaitRow.PersonPart.Value)
            db.AddInParameter(dbcmd, "@SecPersonPart", DbType.String, OrganizationWaitRow.SecPersonPart.Value)
            db.AddInParameter(dbcmd, "@CheckPart", DbType.String, OrganizationWaitRow.CheckPart.Value)
            db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, OrganizationWaitRow.WorkSiteID.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, OrganizationWaitRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@CostDeptID", DbType.String, OrganizationWaitRow.CostDeptID.Value)
            db.AddInParameter(dbcmd, "@CostType", DbType.String, OrganizationWaitRow.CostType.Value)
            db.AddInParameter(dbcmd, "@AccountBranch", DbType.String, OrganizationWaitRow.AccountBranch.Value)
            db.AddInParameter(dbcmd, "@PositionID", DbType.String, OrganizationWaitRow.PositionID.Value)
            db.AddInParameter(dbcmd, "@CompareFlag", DbType.String, OrganizationWaitRow.CompareFlag.Value)
            db.AddInParameter(dbcmd, "@FlowOrganID", DbType.String, OrganizationWaitRow.FlowOrganID.Value)
            db.AddInParameter(dbcmd, "@DelegateFlag", DbType.String, OrganizationWaitRow.DelegateFlag.Value)
            db.AddInParameter(dbcmd, "@OrganNo", DbType.String, OrganizationWaitRow.OrganNo.Value)
            db.AddInParameter(dbcmd, "@InvoiceNo", DbType.String, OrganizationWaitRow.InvoiceNo.Value)
            db.AddInParameter(dbcmd, "@SortOrder", DbType.String, OrganizationWaitRow.SortOrder.Value)
            db.AddInParameter(dbcmd, "@OrgType", DbType.String, OrganizationWaitRow.OrgType.Value)
            db.AddInParameter(dbcmd, "@BusinessType", DbType.String, OrganizationWaitRow.BusinessType.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(OrganizationWaitRow.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), OrganizationWaitRow.ValidDateB.Value))
            db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(OrganizationWaitRow.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), OrganizationWaitRow.ValidDateE.Value))
            db.AddInParameter(dbcmd, "@Remark", DbType.String, OrganizationWaitRow.Remark.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, OrganizationWaitRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, OrganizationWaitRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(OrganizationWaitRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), OrganizationWaitRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal OrganizationWaitRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into OrganizationWait")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, OrganReason, OrganType, ValidDate, Seq, ExecuteDate, WaitStatus, OrganID, OrganNameOld,")
            strSQL.AppendLine("    OrganName, OrganEngName, VirtualFlag, UpOrganID, DeptID, GroupType, GroupID, Boss,")
            strSQL.AppendLine("    BossCompID, BossType, BossTemporary, SecBoss, SecBossCompID, InValidFlag, ReportingEnd,")
            strSQL.AppendLine("    Depth, BranchFlag, RoleCode, PersonPart, SecPersonPart, CheckPart, WorkSiteID, WorkTypeID,")
            strSQL.AppendLine("    CostDeptID, CostType, AccountBranch, PositionID, CompareFlag, FlowOrganID, DelegateFlag,")
            strSQL.AppendLine("    OrganNo, InvoiceNo, SortOrder, OrgType, BusinessType, ValidDateB, ValidDateE, Remark,")
            strSQL.AppendLine("    LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @OrganReason, @OrganType, @ValidDate, @Seq, @ExecuteDate, @WaitStatus, @OrganID, @OrganNameOld,")
            strSQL.AppendLine("    @OrganName, @OrganEngName, @VirtualFlag, @UpOrganID, @DeptID, @GroupType, @GroupID, @Boss,")
            strSQL.AppendLine("    @BossCompID, @BossType, @BossTemporary, @SecBoss, @SecBossCompID, @InValidFlag, @ReportingEnd,")
            strSQL.AppendLine("    @Depth, @BranchFlag, @RoleCode, @PersonPart, @SecPersonPart, @CheckPart, @WorkSiteID, @WorkTypeID,")
            strSQL.AppendLine("    @CostDeptID, @CostType, @AccountBranch, @PositionID, @CompareFlag, @FlowOrganID, @DelegateFlag,")
            strSQL.AppendLine("    @OrganNo, @InvoiceNo, @SortOrder, @OrgType, @BusinessType, @ValidDateB, @ValidDateE, @Remark,")
            strSQL.AppendLine("    @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, OrganizationWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@OrganReason", DbType.String, OrganizationWaitRow.OrganReason.Value)
            db.AddInParameter(dbcmd, "@OrganType", DbType.String, OrganizationWaitRow.OrganType.Value)
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(OrganizationWaitRow.ValidDate.Value), Convert.ToDateTime("1900/1/1"), OrganizationWaitRow.ValidDate.Value))
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, OrganizationWaitRow.Seq.Value)
            db.AddInParameter(dbcmd, "@ExecuteDate", DbType.Date, IIf(IsDateTimeNull(OrganizationWaitRow.ExecuteDate.Value), Convert.ToDateTime("1900/1/1"), OrganizationWaitRow.ExecuteDate.Value))
            db.AddInParameter(dbcmd, "@WaitStatus", DbType.String, OrganizationWaitRow.WaitStatus.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationWaitRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@OrganNameOld", DbType.String, OrganizationWaitRow.OrganNameOld.Value)
            db.AddInParameter(dbcmd, "@OrganName", DbType.String, OrganizationWaitRow.OrganName.Value)
            db.AddInParameter(dbcmd, "@OrganEngName", DbType.String, OrganizationWaitRow.OrganEngName.Value)
            db.AddInParameter(dbcmd, "@VirtualFlag", DbType.String, OrganizationWaitRow.VirtualFlag.Value)
            db.AddInParameter(dbcmd, "@UpOrganID", DbType.String, OrganizationWaitRow.UpOrganID.Value)
            db.AddInParameter(dbcmd, "@DeptID", DbType.String, OrganizationWaitRow.DeptID.Value)
            db.AddInParameter(dbcmd, "@GroupType", DbType.String, OrganizationWaitRow.GroupType.Value)
            db.AddInParameter(dbcmd, "@GroupID", DbType.String, OrganizationWaitRow.GroupID.Value)
            db.AddInParameter(dbcmd, "@Boss", DbType.String, OrganizationWaitRow.Boss.Value)
            db.AddInParameter(dbcmd, "@BossCompID", DbType.String, OrganizationWaitRow.BossCompID.Value)
            db.AddInParameter(dbcmd, "@BossType", DbType.String, OrganizationWaitRow.BossType.Value)
            db.AddInParameter(dbcmd, "@BossTemporary", DbType.String, OrganizationWaitRow.BossTemporary.Value)
            db.AddInParameter(dbcmd, "@SecBoss", DbType.String, OrganizationWaitRow.SecBoss.Value)
            db.AddInParameter(dbcmd, "@SecBossCompID", DbType.String, OrganizationWaitRow.SecBossCompID.Value)
            db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, OrganizationWaitRow.InValidFlag.Value)
            db.AddInParameter(dbcmd, "@ReportingEnd", DbType.String, OrganizationWaitRow.ReportingEnd.Value)
            db.AddInParameter(dbcmd, "@Depth", DbType.String, OrganizationWaitRow.Depth.Value)
            db.AddInParameter(dbcmd, "@BranchFlag", DbType.String, OrganizationWaitRow.BranchFlag.Value)
            db.AddInParameter(dbcmd, "@RoleCode", DbType.String, OrganizationWaitRow.RoleCode.Value)
            db.AddInParameter(dbcmd, "@PersonPart", DbType.String, OrganizationWaitRow.PersonPart.Value)
            db.AddInParameter(dbcmd, "@SecPersonPart", DbType.String, OrganizationWaitRow.SecPersonPart.Value)
            db.AddInParameter(dbcmd, "@CheckPart", DbType.String, OrganizationWaitRow.CheckPart.Value)
            db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, OrganizationWaitRow.WorkSiteID.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, OrganizationWaitRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@CostDeptID", DbType.String, OrganizationWaitRow.CostDeptID.Value)
            db.AddInParameter(dbcmd, "@CostType", DbType.String, OrganizationWaitRow.CostType.Value)
            db.AddInParameter(dbcmd, "@AccountBranch", DbType.String, OrganizationWaitRow.AccountBranch.Value)
            db.AddInParameter(dbcmd, "@PositionID", DbType.String, OrganizationWaitRow.PositionID.Value)
            db.AddInParameter(dbcmd, "@CompareFlag", DbType.String, OrganizationWaitRow.CompareFlag.Value)
            db.AddInParameter(dbcmd, "@FlowOrganID", DbType.String, OrganizationWaitRow.FlowOrganID.Value)
            db.AddInParameter(dbcmd, "@DelegateFlag", DbType.String, OrganizationWaitRow.DelegateFlag.Value)
            db.AddInParameter(dbcmd, "@OrganNo", DbType.String, OrganizationWaitRow.OrganNo.Value)
            db.AddInParameter(dbcmd, "@InvoiceNo", DbType.String, OrganizationWaitRow.InvoiceNo.Value)
            db.AddInParameter(dbcmd, "@SortOrder", DbType.String, OrganizationWaitRow.SortOrder.Value)
            db.AddInParameter(dbcmd, "@OrgType", DbType.String, OrganizationWaitRow.OrgType.Value)
            db.AddInParameter(dbcmd, "@BusinessType", DbType.String, OrganizationWaitRow.BusinessType.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(OrganizationWaitRow.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), OrganizationWaitRow.ValidDateB.Value))
            db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(OrganizationWaitRow.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), OrganizationWaitRow.ValidDateE.Value))
            db.AddInParameter(dbcmd, "@Remark", DbType.String, OrganizationWaitRow.Remark.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, OrganizationWaitRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, OrganizationWaitRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(OrganizationWaitRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), OrganizationWaitRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal OrganizationWaitRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into OrganizationWait")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, OrganReason, OrganType, ValidDate, Seq, ExecuteDate, WaitStatus, OrganID, OrganNameOld,")
            strSQL.AppendLine("    OrganName, OrganEngName, VirtualFlag, UpOrganID, DeptID, GroupType, GroupID, Boss,")
            strSQL.AppendLine("    BossCompID, BossType, BossTemporary, SecBoss, SecBossCompID, InValidFlag, ReportingEnd,")
            strSQL.AppendLine("    Depth, BranchFlag, RoleCode, PersonPart, SecPersonPart, CheckPart, WorkSiteID, WorkTypeID,")
            strSQL.AppendLine("    CostDeptID, CostType, AccountBranch, PositionID, CompareFlag, FlowOrganID, DelegateFlag,")
            strSQL.AppendLine("    OrganNo, InvoiceNo, SortOrder, OrgType, BusinessType, ValidDateB, ValidDateE, Remark,")
            strSQL.AppendLine("    LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @OrganReason, @OrganType, @ValidDate, @Seq, @ExecuteDate, @WaitStatus, @OrganID, @OrganNameOld,")
            strSQL.AppendLine("    @OrganName, @OrganEngName, @VirtualFlag, @UpOrganID, @DeptID, @GroupType, @GroupID, @Boss,")
            strSQL.AppendLine("    @BossCompID, @BossType, @BossTemporary, @SecBoss, @SecBossCompID, @InValidFlag, @ReportingEnd,")
            strSQL.AppendLine("    @Depth, @BranchFlag, @RoleCode, @PersonPart, @SecPersonPart, @CheckPart, @WorkSiteID, @WorkTypeID,")
            strSQL.AppendLine("    @CostDeptID, @CostType, @AccountBranch, @PositionID, @CompareFlag, @FlowOrganID, @DelegateFlag,")
            strSQL.AppendLine("    @OrganNo, @InvoiceNo, @SortOrder, @OrgType, @BusinessType, @ValidDateB, @ValidDateE, @Remark,")
            strSQL.AppendLine("    @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In OrganizationWaitRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@OrganReason", DbType.String, r.OrganReason.Value)
                        db.AddInParameter(dbcmd, "@OrganType", DbType.String, r.OrganType.Value)
                        db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(r.ValidDate.Value), Convert.ToDateTime("1900/1/1"), r.ValidDate.Value))
                        db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                        db.AddInParameter(dbcmd, "@ExecuteDate", DbType.Date, IIf(IsDateTimeNull(r.ExecuteDate.Value), Convert.ToDateTime("1900/1/1"), r.ExecuteDate.Value))
                        db.AddInParameter(dbcmd, "@WaitStatus", DbType.String, r.WaitStatus.Value)
                        db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                        db.AddInParameter(dbcmd, "@OrganNameOld", DbType.String, r.OrganNameOld.Value)
                        db.AddInParameter(dbcmd, "@OrganName", DbType.String, r.OrganName.Value)
                        db.AddInParameter(dbcmd, "@OrganEngName", DbType.String, r.OrganEngName.Value)
                        db.AddInParameter(dbcmd, "@VirtualFlag", DbType.String, r.VirtualFlag.Value)
                        db.AddInParameter(dbcmd, "@UpOrganID", DbType.String, r.UpOrganID.Value)
                        db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                        db.AddInParameter(dbcmd, "@GroupType", DbType.String, r.GroupType.Value)
                        db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                        db.AddInParameter(dbcmd, "@Boss", DbType.String, r.Boss.Value)
                        db.AddInParameter(dbcmd, "@BossCompID", DbType.String, r.BossCompID.Value)
                        db.AddInParameter(dbcmd, "@BossType", DbType.String, r.BossType.Value)
                        db.AddInParameter(dbcmd, "@BossTemporary", DbType.String, r.BossTemporary.Value)
                        db.AddInParameter(dbcmd, "@SecBoss", DbType.String, r.SecBoss.Value)
                        db.AddInParameter(dbcmd, "@SecBossCompID", DbType.String, r.SecBossCompID.Value)
                        db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, r.InValidFlag.Value)
                        db.AddInParameter(dbcmd, "@ReportingEnd", DbType.String, r.ReportingEnd.Value)
                        db.AddInParameter(dbcmd, "@Depth", DbType.String, r.Depth.Value)
                        db.AddInParameter(dbcmd, "@BranchFlag", DbType.String, r.BranchFlag.Value)
                        db.AddInParameter(dbcmd, "@RoleCode", DbType.String, r.RoleCode.Value)
                        db.AddInParameter(dbcmd, "@PersonPart", DbType.String, r.PersonPart.Value)
                        db.AddInParameter(dbcmd, "@SecPersonPart", DbType.String, r.SecPersonPart.Value)
                        db.AddInParameter(dbcmd, "@CheckPart", DbType.String, r.CheckPart.Value)
                        db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, r.WorkSiteID.Value)
                        db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                        db.AddInParameter(dbcmd, "@CostDeptID", DbType.String, r.CostDeptID.Value)
                        db.AddInParameter(dbcmd, "@CostType", DbType.String, r.CostType.Value)
                        db.AddInParameter(dbcmd, "@AccountBranch", DbType.String, r.AccountBranch.Value)
                        db.AddInParameter(dbcmd, "@PositionID", DbType.String, r.PositionID.Value)
                        db.AddInParameter(dbcmd, "@CompareFlag", DbType.String, r.CompareFlag.Value)
                        db.AddInParameter(dbcmd, "@FlowOrganID", DbType.String, r.FlowOrganID.Value)
                        db.AddInParameter(dbcmd, "@DelegateFlag", DbType.String, r.DelegateFlag.Value)
                        db.AddInParameter(dbcmd, "@OrganNo", DbType.String, r.OrganNo.Value)
                        db.AddInParameter(dbcmd, "@InvoiceNo", DbType.String, r.InvoiceNo.Value)
                        db.AddInParameter(dbcmd, "@SortOrder", DbType.String, r.SortOrder.Value)
                        db.AddInParameter(dbcmd, "@OrgType", DbType.String, r.OrgType.Value)
                        db.AddInParameter(dbcmd, "@BusinessType", DbType.String, r.BusinessType.Value)
                        db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(r.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateB.Value))
                        db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(r.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateE.Value))
                        db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
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

        Public Function Insert(ByVal OrganizationWaitRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into OrganizationWait")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, OrganReason, OrganType, ValidDate, Seq, ExecuteDate, WaitStatus, OrganID, OrganNameOld,")
            strSQL.AppendLine("    OrganName, OrganEngName, VirtualFlag, UpOrganID, DeptID, GroupType, GroupID, Boss,")
            strSQL.AppendLine("    BossCompID, BossType, BossTemporary, SecBoss, SecBossCompID, InValidFlag, ReportingEnd,")
            strSQL.AppendLine("    Depth, BranchFlag, RoleCode, PersonPart, SecPersonPart, CheckPart, WorkSiteID, WorkTypeID,")
            strSQL.AppendLine("    CostDeptID, CostType, AccountBranch, PositionID, CompareFlag, FlowOrganID, DelegateFlag,")
            strSQL.AppendLine("    OrganNo, InvoiceNo, SortOrder, OrgType, BusinessType, ValidDateB, ValidDateE, Remark,")
            strSQL.AppendLine("    LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @OrganReason, @OrganType, @ValidDate, @Seq, @ExecuteDate, @WaitStatus, @OrganID, @OrganNameOld,")
            strSQL.AppendLine("    @OrganName, @OrganEngName, @VirtualFlag, @UpOrganID, @DeptID, @GroupType, @GroupID, @Boss,")
            strSQL.AppendLine("    @BossCompID, @BossType, @BossTemporary, @SecBoss, @SecBossCompID, @InValidFlag, @ReportingEnd,")
            strSQL.AppendLine("    @Depth, @BranchFlag, @RoleCode, @PersonPart, @SecPersonPart, @CheckPart, @WorkSiteID, @WorkTypeID,")
            strSQL.AppendLine("    @CostDeptID, @CostType, @AccountBranch, @PositionID, @CompareFlag, @FlowOrganID, @DelegateFlag,")
            strSQL.AppendLine("    @OrganNo, @InvoiceNo, @SortOrder, @OrgType, @BusinessType, @ValidDateB, @ValidDateE, @Remark,")
            strSQL.AppendLine("    @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In OrganizationWaitRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@OrganReason", DbType.String, r.OrganReason.Value)
                db.AddInParameter(dbcmd, "@OrganType", DbType.String, r.OrganType.Value)
                db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(r.ValidDate.Value), Convert.ToDateTime("1900/1/1"), r.ValidDate.Value))
                db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                db.AddInParameter(dbcmd, "@ExecuteDate", DbType.Date, IIf(IsDateTimeNull(r.ExecuteDate.Value), Convert.ToDateTime("1900/1/1"), r.ExecuteDate.Value))
                db.AddInParameter(dbcmd, "@WaitStatus", DbType.String, r.WaitStatus.Value)
                db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                db.AddInParameter(dbcmd, "@OrganNameOld", DbType.String, r.OrganNameOld.Value)
                db.AddInParameter(dbcmd, "@OrganName", DbType.String, r.OrganName.Value)
                db.AddInParameter(dbcmd, "@OrganEngName", DbType.String, r.OrganEngName.Value)
                db.AddInParameter(dbcmd, "@VirtualFlag", DbType.String, r.VirtualFlag.Value)
                db.AddInParameter(dbcmd, "@UpOrganID", DbType.String, r.UpOrganID.Value)
                db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                db.AddInParameter(dbcmd, "@GroupType", DbType.String, r.GroupType.Value)
                db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                db.AddInParameter(dbcmd, "@Boss", DbType.String, r.Boss.Value)
                db.AddInParameter(dbcmd, "@BossCompID", DbType.String, r.BossCompID.Value)
                db.AddInParameter(dbcmd, "@BossType", DbType.String, r.BossType.Value)
                db.AddInParameter(dbcmd, "@BossTemporary", DbType.String, r.BossTemporary.Value)
                db.AddInParameter(dbcmd, "@SecBoss", DbType.String, r.SecBoss.Value)
                db.AddInParameter(dbcmd, "@SecBossCompID", DbType.String, r.SecBossCompID.Value)
                db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, r.InValidFlag.Value)
                db.AddInParameter(dbcmd, "@ReportingEnd", DbType.String, r.ReportingEnd.Value)
                db.AddInParameter(dbcmd, "@Depth", DbType.String, r.Depth.Value)
                db.AddInParameter(dbcmd, "@BranchFlag", DbType.String, r.BranchFlag.Value)
                db.AddInParameter(dbcmd, "@RoleCode", DbType.String, r.RoleCode.Value)
                db.AddInParameter(dbcmd, "@PersonPart", DbType.String, r.PersonPart.Value)
                db.AddInParameter(dbcmd, "@SecPersonPart", DbType.String, r.SecPersonPart.Value)
                db.AddInParameter(dbcmd, "@CheckPart", DbType.String, r.CheckPart.Value)
                db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, r.WorkSiteID.Value)
                db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                db.AddInParameter(dbcmd, "@CostDeptID", DbType.String, r.CostDeptID.Value)
                db.AddInParameter(dbcmd, "@CostType", DbType.String, r.CostType.Value)
                db.AddInParameter(dbcmd, "@AccountBranch", DbType.String, r.AccountBranch.Value)
                db.AddInParameter(dbcmd, "@PositionID", DbType.String, r.PositionID.Value)
                db.AddInParameter(dbcmd, "@CompareFlag", DbType.String, r.CompareFlag.Value)
                db.AddInParameter(dbcmd, "@FlowOrganID", DbType.String, r.FlowOrganID.Value)
                db.AddInParameter(dbcmd, "@DelegateFlag", DbType.String, r.DelegateFlag.Value)
                db.AddInParameter(dbcmd, "@OrganNo", DbType.String, r.OrganNo.Value)
                db.AddInParameter(dbcmd, "@InvoiceNo", DbType.String, r.InvoiceNo.Value)
                db.AddInParameter(dbcmd, "@SortOrder", DbType.String, r.SortOrder.Value)
                db.AddInParameter(dbcmd, "@OrgType", DbType.String, r.OrgType.Value)
                db.AddInParameter(dbcmd, "@BusinessType", DbType.String, r.BusinessType.Value)
                db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(r.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateB.Value))
                db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(r.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateE.Value))
                db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
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

