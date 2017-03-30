'****************************************************************
' Table:Organization
' Created Date: 2016.10.24
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beOrganization
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "OrganID", "OrganName", "OrganEngName", "VirtualFlag", "UpOrganID", "DeptID", "GroupType", "GroupID", "Boss" _
                                    , "BossCompID", "BossType", "BossTemporary", "SecBoss", "SecBossCompID", "PersonPart", "CheckPart", "WorkSiteID", "PositionID", "WorkTypeID", "CostDeptID" _
                                    , "CostType", "AccountBranch", "InValidFlag", "ReportingEnd", "Depth", "BranchFlag", "RoleCode", "InvoiceNo", "SortOrder", "OrgType", "ValidDateB" _
                                    , "ValidDateE", "Remark", "LastChgComp", "LastChgID", "LastChgDate", "SecPersonPart" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date) _
                                    , GetType(Date), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(String) }
        Private m_PrimaryFields As String() = { "CompID", "OrganID" }

        Public ReadOnly Property Rows() As beOrganization.Rows 
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
        Public Sub Transfer2Row(OrganizationTable As DataTable)
            For Each dr As DataRow In OrganizationTable.Rows
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
                dr(m_Rows(i).OrganID.FieldName) = m_Rows(i).OrganID.Value
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
                dr(m_Rows(i).PersonPart.FieldName) = m_Rows(i).PersonPart.Value
                dr(m_Rows(i).CheckPart.FieldName) = m_Rows(i).CheckPart.Value
                dr(m_Rows(i).WorkSiteID.FieldName) = m_Rows(i).WorkSiteID.Value
                dr(m_Rows(i).PositionID.FieldName) = m_Rows(i).PositionID.Value
                dr(m_Rows(i).WorkTypeID.FieldName) = m_Rows(i).WorkTypeID.Value
                dr(m_Rows(i).CostDeptID.FieldName) = m_Rows(i).CostDeptID.Value
                dr(m_Rows(i).CostType.FieldName) = m_Rows(i).CostType.Value
                dr(m_Rows(i).AccountBranch.FieldName) = m_Rows(i).AccountBranch.Value
                dr(m_Rows(i).InValidFlag.FieldName) = m_Rows(i).InValidFlag.Value
                dr(m_Rows(i).ReportingEnd.FieldName) = m_Rows(i).ReportingEnd.Value
                dr(m_Rows(i).Depth.FieldName) = m_Rows(i).Depth.Value
                dr(m_Rows(i).BranchFlag.FieldName) = m_Rows(i).BranchFlag.Value
                dr(m_Rows(i).RoleCode.FieldName) = m_Rows(i).RoleCode.Value
                dr(m_Rows(i).InvoiceNo.FieldName) = m_Rows(i).InvoiceNo.Value
                dr(m_Rows(i).SortOrder.FieldName) = m_Rows(i).SortOrder.Value
                dr(m_Rows(i).OrgType.FieldName) = m_Rows(i).OrgType.Value
                dr(m_Rows(i).ValidDateB.FieldName) = m_Rows(i).ValidDateB.Value
                dr(m_Rows(i).ValidDateE.FieldName) = m_Rows(i).ValidDateE.Value
                dr(m_Rows(i).Remark.FieldName) = m_Rows(i).Remark.Value
                dr(m_Rows(i).LastChgComp.FieldName) = m_Rows(i).LastChgComp.Value
                dr(m_Rows(i).LastChgID.FieldName) = m_Rows(i).LastChgID.Value
                dr(m_Rows(i).LastChgDate.FieldName) = m_Rows(i).LastChgDate.Value
                dr(m_Rows(i).SecPersonPart.FieldName) = m_Rows(i).SecPersonPart.Value

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

        Public Sub Add(OrganizationRow As Row)
            m_Rows.Add(OrganizationRow)
        End Sub

        Public Sub Remove(OrganizationRow As Row)
            If m_Rows.IndexOf(OrganizationRow) >= 0 Then
                m_Rows.Remove(OrganizationRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_OrganID As Field(Of String) = new Field(Of String)("OrganID", true)
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
        Private FI_PersonPart As Field(Of String) = new Field(Of String)("PersonPart", true)
        Private FI_CheckPart As Field(Of String) = new Field(Of String)("CheckPart", true)
        Private FI_WorkSiteID As Field(Of String) = new Field(Of String)("WorkSiteID", true)
        Private FI_PositionID As Field(Of String) = new Field(Of String)("PositionID", true)
        Private FI_WorkTypeID As Field(Of String) = new Field(Of String)("WorkTypeID", true)
        Private FI_CostDeptID As Field(Of String) = new Field(Of String)("CostDeptID", true)
        Private FI_CostType As Field(Of String) = new Field(Of String)("CostType", true)
        Private FI_AccountBranch As Field(Of String) = new Field(Of String)("AccountBranch", true)
        Private FI_InValidFlag As Field(Of String) = new Field(Of String)("InValidFlag", true)
        Private FI_ReportingEnd As Field(Of String) = new Field(Of String)("ReportingEnd", true)
        Private FI_Depth As Field(Of String) = new Field(Of String)("Depth", true)
        Private FI_BranchFlag As Field(Of String) = new Field(Of String)("BranchFlag", true)
        Private FI_RoleCode As Field(Of String) = new Field(Of String)("RoleCode", true)
        Private FI_InvoiceNo As Field(Of String) = new Field(Of String)("InvoiceNo", true)
        Private FI_SortOrder As Field(Of String) = new Field(Of String)("SortOrder", true)
        Private FI_OrgType As Field(Of String) = new Field(Of String)("OrgType", true)
        Private FI_ValidDateB As Field(Of Date) = new Field(Of Date)("ValidDateB", true)
        Private FI_ValidDateE As Field(Of Date) = new Field(Of Date)("ValidDateE", true)
        Private FI_Remark As Field(Of String) = new Field(Of String)("Remark", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private FI_SecPersonPart As Field(Of String) = new Field(Of String)("SecPersonPart", true)
        Private m_FieldNames As String() = { "CompID", "OrganID", "OrganName", "OrganEngName", "VirtualFlag", "UpOrganID", "DeptID", "GroupType", "GroupID", "Boss" _
                                    , "BossCompID", "BossType", "BossTemporary", "SecBoss", "SecBossCompID", "PersonPart", "CheckPart", "WorkSiteID", "PositionID", "WorkTypeID", "CostDeptID" _
                                    , "CostType", "AccountBranch", "InValidFlag", "ReportingEnd", "Depth", "BranchFlag", "RoleCode", "InvoiceNo", "SortOrder", "OrgType", "ValidDateB" _
                                    , "ValidDateE", "Remark", "LastChgComp", "LastChgID", "LastChgDate", "SecPersonPart" }
        Private m_PrimaryFields As String() = { "CompID", "OrganID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CompID"
                    Return FI_CompID.Value
                Case "OrganID"
                    Return FI_OrganID.Value
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
                Case "PersonPart"
                    Return FI_PersonPart.Value
                Case "CheckPart"
                    Return FI_CheckPart.Value
                Case "WorkSiteID"
                    Return FI_WorkSiteID.Value
                Case "PositionID"
                    Return FI_PositionID.Value
                Case "WorkTypeID"
                    Return FI_WorkTypeID.Value
                Case "CostDeptID"
                    Return FI_CostDeptID.Value
                Case "CostType"
                    Return FI_CostType.Value
                Case "AccountBranch"
                    Return FI_AccountBranch.Value
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
                Case "InvoiceNo"
                    Return FI_InvoiceNo.Value
                Case "SortOrder"
                    Return FI_SortOrder.Value
                Case "OrgType"
                    Return FI_OrgType.Value
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
                Case "SecPersonPart"
                    Return FI_SecPersonPart.Value
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Sub SetFieldValue(ByVal fieldName As String, ByVal value As Object)
            Select Case fieldName
                Case "CompID"
                    FI_CompID.SetValue(value)
                Case "OrganID"
                    FI_OrganID.SetValue(value)
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
                Case "PersonPart"
                    FI_PersonPart.SetValue(value)
                Case "CheckPart"
                    FI_CheckPart.SetValue(value)
                Case "WorkSiteID"
                    FI_WorkSiteID.SetValue(value)
                Case "PositionID"
                    FI_PositionID.SetValue(value)
                Case "WorkTypeID"
                    FI_WorkTypeID.SetValue(value)
                Case "CostDeptID"
                    FI_CostDeptID.SetValue(value)
                Case "CostType"
                    FI_CostType.SetValue(value)
                Case "AccountBranch"
                    FI_AccountBranch.SetValue(value)
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
                Case "InvoiceNo"
                    FI_InvoiceNo.SetValue(value)
                Case "SortOrder"
                    FI_SortOrder.SetValue(value)
                Case "OrgType"
                    FI_OrgType.SetValue(value)
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
                Case "SecPersonPart"
                    FI_SecPersonPart.SetValue(value)
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
                Case "OrganID"
                    return FI_OrganID.Updated
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
                Case "PersonPart"
                    return FI_PersonPart.Updated
                Case "CheckPart"
                    return FI_CheckPart.Updated
                Case "WorkSiteID"
                    return FI_WorkSiteID.Updated
                Case "PositionID"
                    return FI_PositionID.Updated
                Case "WorkTypeID"
                    return FI_WorkTypeID.Updated
                Case "CostDeptID"
                    return FI_CostDeptID.Updated
                Case "CostType"
                    return FI_CostType.Updated
                Case "AccountBranch"
                    return FI_AccountBranch.Updated
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
                Case "InvoiceNo"
                    return FI_InvoiceNo.Updated
                Case "SortOrder"
                    return FI_SortOrder.Updated
                Case "OrgType"
                    return FI_OrgType.Updated
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
                Case "SecPersonPart"
                    return FI_SecPersonPart.Updated
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public Function CreateUpdateSQL(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "CompID"
                    return FI_CompID.CreateUpdateSQL
                Case "OrganID"
                    return FI_OrganID.CreateUpdateSQL
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
                Case "PersonPart"
                    return FI_PersonPart.CreateUpdateSQL
                Case "CheckPart"
                    return FI_CheckPart.CreateUpdateSQL
                Case "WorkSiteID"
                    return FI_WorkSiteID.CreateUpdateSQL
                Case "PositionID"
                    return FI_PositionID.CreateUpdateSQL
                Case "WorkTypeID"
                    return FI_WorkTypeID.CreateUpdateSQL
                Case "CostDeptID"
                    return FI_CostDeptID.CreateUpdateSQL
                Case "CostType"
                    return FI_CostType.CreateUpdateSQL
                Case "AccountBranch"
                    return FI_AccountBranch.CreateUpdateSQL
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
                Case "InvoiceNo"
                    return FI_InvoiceNo.CreateUpdateSQL
                Case "SortOrder"
                    return FI_SortOrder.CreateUpdateSQL
                Case "OrgType"
                    return FI_OrgType.CreateUpdateSQL
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
                Case "SecPersonPart"
                    return FI_SecPersonPart.CreateUpdateSQL
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
            FI_OrganID.SetInitValue("")
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
            FI_PersonPart.SetInitValue("")
            FI_CheckPart.SetInitValue("")
            FI_WorkSiteID.SetInitValue("")
            FI_PositionID.SetInitValue("")
            FI_WorkTypeID.SetInitValue("")
            FI_CostDeptID.SetInitValue("")
            FI_CostType.SetInitValue("")
            FI_AccountBranch.SetInitValue("")
            FI_InValidFlag.SetInitValue("0")
            FI_ReportingEnd.SetInitValue("0")
            FI_Depth.SetInitValue("")
            FI_BranchFlag.SetInitValue("")
            FI_RoleCode.SetInitValue("")
            FI_InvoiceNo.SetInitValue("")
            FI_SortOrder.SetInitValue("")
            FI_OrgType.SetInitValue("")
            FI_ValidDateB.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_ValidDateE.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_Remark.SetInitValue("")
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_SecPersonPart.SetInitValue("")
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CompID.SetInitValue(dr("CompID"))
            FI_OrganID.SetInitValue(dr("OrganID"))
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
            FI_PersonPart.SetInitValue(dr("PersonPart"))
            FI_CheckPart.SetInitValue(dr("CheckPart"))
            FI_WorkSiteID.SetInitValue(dr("WorkSiteID"))
            FI_PositionID.SetInitValue(dr("PositionID"))
            FI_WorkTypeID.SetInitValue(dr("WorkTypeID"))
            FI_CostDeptID.SetInitValue(dr("CostDeptID"))
            FI_CostType.SetInitValue(dr("CostType"))
            FI_AccountBranch.SetInitValue(dr("AccountBranch"))
            FI_InValidFlag.SetInitValue(dr("InValidFlag"))
            FI_ReportingEnd.SetInitValue(dr("ReportingEnd"))
            FI_Depth.SetInitValue(dr("Depth"))
            FI_BranchFlag.SetInitValue(dr("BranchFlag"))
            FI_RoleCode.SetInitValue(dr("RoleCode"))
            FI_InvoiceNo.SetInitValue(dr("InvoiceNo"))
            FI_SortOrder.SetInitValue(dr("SortOrder"))
            FI_OrgType.SetInitValue(dr("OrgType"))
            FI_ValidDateB.SetInitValue(dr("ValidDateB"))
            FI_ValidDateE.SetInitValue(dr("ValidDateE"))
            FI_Remark.SetInitValue(dr("Remark"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))
            FI_SecPersonPart.SetInitValue(dr("SecPersonPart"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CompID.Updated = False
            FI_OrganID.Updated = False
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
            FI_PersonPart.Updated = False
            FI_CheckPart.Updated = False
            FI_WorkSiteID.Updated = False
            FI_PositionID.Updated = False
            FI_WorkTypeID.Updated = False
            FI_CostDeptID.Updated = False
            FI_CostType.Updated = False
            FI_AccountBranch.Updated = False
            FI_InValidFlag.Updated = False
            FI_ReportingEnd.Updated = False
            FI_Depth.Updated = False
            FI_BranchFlag.Updated = False
            FI_RoleCode.Updated = False
            FI_InvoiceNo.Updated = False
            FI_SortOrder.Updated = False
            FI_OrgType.Updated = False
            FI_ValidDateB.Updated = False
            FI_ValidDateE.Updated = False
            FI_Remark.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
            FI_SecPersonPart.Updated = False
        End Sub

        Public ReadOnly Property CompID As Field(Of String) 
            Get
                Return FI_CompID
            End Get
        End Property

        Public ReadOnly Property OrganID As Field(Of String) 
            Get
                Return FI_OrganID
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

        Public ReadOnly Property PersonPart As Field(Of String) 
            Get
                Return FI_PersonPart
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

        Public ReadOnly Property SecPersonPart As Field(Of String) 
            Get
                Return FI_SecPersonPart
            End Get
        End Property

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal OrganizationRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From Organization")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And OrganID = @OrganID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, OrganizationRow.CompID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationRow.OrganID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal OrganizationRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From Organization")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And OrganID = @OrganID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, OrganizationRow.CompID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationRow.OrganID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal OrganizationRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From Organization")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And OrganID = @OrganID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In OrganizationRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal OrganizationRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From Organization")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And OrganID = @OrganID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In OrganizationRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal OrganizationRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Organization")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And OrganID = @OrganID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, OrganizationRow.CompID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationRow.OrganID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(OrganizationRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Organization")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And OrganID = @OrganID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, OrganizationRow.CompID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationRow.OrganID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal OrganizationRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update Organization Set")
            For i As Integer = 0 To OrganizationRow.FieldNames.Length - 1
                If Not OrganizationRow.IsIdentityField(OrganizationRow.FieldNames(i)) AndAlso OrganizationRow.IsUpdated(OrganizationRow.FieldNames(i)) AndAlso OrganizationRow.CreateUpdateSQL(OrganizationRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, OrganizationRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And OrganID = @PKOrganID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If OrganizationRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, OrganizationRow.CompID.Value)
            If OrganizationRow.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationRow.OrganID.Value)
            If OrganizationRow.OrganName.Updated Then db.AddInParameter(dbcmd, "@OrganName", DbType.String, OrganizationRow.OrganName.Value)
            If OrganizationRow.OrganEngName.Updated Then db.AddInParameter(dbcmd, "@OrganEngName", DbType.String, OrganizationRow.OrganEngName.Value)
            If OrganizationRow.VirtualFlag.Updated Then db.AddInParameter(dbcmd, "@VirtualFlag", DbType.String, OrganizationRow.VirtualFlag.Value)
            If OrganizationRow.UpOrganID.Updated Then db.AddInParameter(dbcmd, "@UpOrganID", DbType.String, OrganizationRow.UpOrganID.Value)
            If OrganizationRow.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, OrganizationRow.DeptID.Value)
            If OrganizationRow.GroupType.Updated Then db.AddInParameter(dbcmd, "@GroupType", DbType.String, OrganizationRow.GroupType.Value)
            If OrganizationRow.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, OrganizationRow.GroupID.Value)
            If OrganizationRow.Boss.Updated Then db.AddInParameter(dbcmd, "@Boss", DbType.String, OrganizationRow.Boss.Value)
            If OrganizationRow.BossCompID.Updated Then db.AddInParameter(dbcmd, "@BossCompID", DbType.String, OrganizationRow.BossCompID.Value)
            If OrganizationRow.BossType.Updated Then db.AddInParameter(dbcmd, "@BossType", DbType.String, OrganizationRow.BossType.Value)
            If OrganizationRow.BossTemporary.Updated Then db.AddInParameter(dbcmd, "@BossTemporary", DbType.String, OrganizationRow.BossTemporary.Value)
            If OrganizationRow.SecBoss.Updated Then db.AddInParameter(dbcmd, "@SecBoss", DbType.String, OrganizationRow.SecBoss.Value)
            If OrganizationRow.SecBossCompID.Updated Then db.AddInParameter(dbcmd, "@SecBossCompID", DbType.String, OrganizationRow.SecBossCompID.Value)
            If OrganizationRow.PersonPart.Updated Then db.AddInParameter(dbcmd, "@PersonPart", DbType.String, OrganizationRow.PersonPart.Value)
            If OrganizationRow.CheckPart.Updated Then db.AddInParameter(dbcmd, "@CheckPart", DbType.String, OrganizationRow.CheckPart.Value)
            If OrganizationRow.WorkSiteID.Updated Then db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, OrganizationRow.WorkSiteID.Value)
            If OrganizationRow.PositionID.Updated Then db.AddInParameter(dbcmd, "@PositionID", DbType.String, OrganizationRow.PositionID.Value)
            If OrganizationRow.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, OrganizationRow.WorkTypeID.Value)
            If OrganizationRow.CostDeptID.Updated Then db.AddInParameter(dbcmd, "@CostDeptID", DbType.String, OrganizationRow.CostDeptID.Value)
            If OrganizationRow.CostType.Updated Then db.AddInParameter(dbcmd, "@CostType", DbType.String, OrganizationRow.CostType.Value)
            If OrganizationRow.AccountBranch.Updated Then db.AddInParameter(dbcmd, "@AccountBranch", DbType.String, OrganizationRow.AccountBranch.Value)
            If OrganizationRow.InValidFlag.Updated Then db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, OrganizationRow.InValidFlag.Value)
            If OrganizationRow.ReportingEnd.Updated Then db.AddInParameter(dbcmd, "@ReportingEnd", DbType.String, OrganizationRow.ReportingEnd.Value)
            If OrganizationRow.Depth.Updated Then db.AddInParameter(dbcmd, "@Depth", DbType.String, OrganizationRow.Depth.Value)
            If OrganizationRow.BranchFlag.Updated Then db.AddInParameter(dbcmd, "@BranchFlag", DbType.String, OrganizationRow.BranchFlag.Value)
            If OrganizationRow.RoleCode.Updated Then db.AddInParameter(dbcmd, "@RoleCode", DbType.String, OrganizationRow.RoleCode.Value)
            If OrganizationRow.InvoiceNo.Updated Then db.AddInParameter(dbcmd, "@InvoiceNo", DbType.String, OrganizationRow.InvoiceNo.Value)
            If OrganizationRow.SortOrder.Updated Then db.AddInParameter(dbcmd, "@SortOrder", DbType.String, OrganizationRow.SortOrder.Value)
            If OrganizationRow.OrgType.Updated Then db.AddInParameter(dbcmd, "@OrgType", DbType.String, OrganizationRow.OrgType.Value)
            If OrganizationRow.ValidDateB.Updated Then db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(OrganizationRow.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), OrganizationRow.ValidDateB.Value))
            If OrganizationRow.ValidDateE.Updated Then db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(OrganizationRow.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), OrganizationRow.ValidDateE.Value))
            If OrganizationRow.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, OrganizationRow.Remark.Value)
            If OrganizationRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, OrganizationRow.LastChgComp.Value)
            If OrganizationRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, OrganizationRow.LastChgID.Value)
            If OrganizationRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(OrganizationRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), OrganizationRow.LastChgDate.Value))
            If OrganizationRow.SecPersonPart.Updated Then db.AddInParameter(dbcmd, "@SecPersonPart", DbType.String, OrganizationRow.SecPersonPart.Value)
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(OrganizationRow.LoadFromDataRow, OrganizationRow.CompID.OldValue, OrganizationRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKOrganID", DbType.String, IIf(OrganizationRow.LoadFromDataRow, OrganizationRow.OrganID.OldValue, OrganizationRow.OrganID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal OrganizationRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update Organization Set")
            For i As Integer = 0 To OrganizationRow.FieldNames.Length - 1
                If Not OrganizationRow.IsIdentityField(OrganizationRow.FieldNames(i)) AndAlso OrganizationRow.IsUpdated(OrganizationRow.FieldNames(i)) AndAlso OrganizationRow.CreateUpdateSQL(OrganizationRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, OrganizationRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And OrganID = @PKOrganID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If OrganizationRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, OrganizationRow.CompID.Value)
            If OrganizationRow.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationRow.OrganID.Value)
            If OrganizationRow.OrganName.Updated Then db.AddInParameter(dbcmd, "@OrganName", DbType.String, OrganizationRow.OrganName.Value)
            If OrganizationRow.OrganEngName.Updated Then db.AddInParameter(dbcmd, "@OrganEngName", DbType.String, OrganizationRow.OrganEngName.Value)
            If OrganizationRow.VirtualFlag.Updated Then db.AddInParameter(dbcmd, "@VirtualFlag", DbType.String, OrganizationRow.VirtualFlag.Value)
            If OrganizationRow.UpOrganID.Updated Then db.AddInParameter(dbcmd, "@UpOrganID", DbType.String, OrganizationRow.UpOrganID.Value)
            If OrganizationRow.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, OrganizationRow.DeptID.Value)
            If OrganizationRow.GroupType.Updated Then db.AddInParameter(dbcmd, "@GroupType", DbType.String, OrganizationRow.GroupType.Value)
            If OrganizationRow.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, OrganizationRow.GroupID.Value)
            If OrganizationRow.Boss.Updated Then db.AddInParameter(dbcmd, "@Boss", DbType.String, OrganizationRow.Boss.Value)
            If OrganizationRow.BossCompID.Updated Then db.AddInParameter(dbcmd, "@BossCompID", DbType.String, OrganizationRow.BossCompID.Value)
            If OrganizationRow.BossType.Updated Then db.AddInParameter(dbcmd, "@BossType", DbType.String, OrganizationRow.BossType.Value)
            If OrganizationRow.BossTemporary.Updated Then db.AddInParameter(dbcmd, "@BossTemporary", DbType.String, OrganizationRow.BossTemporary.Value)
            If OrganizationRow.SecBoss.Updated Then db.AddInParameter(dbcmd, "@SecBoss", DbType.String, OrganizationRow.SecBoss.Value)
            If OrganizationRow.SecBossCompID.Updated Then db.AddInParameter(dbcmd, "@SecBossCompID", DbType.String, OrganizationRow.SecBossCompID.Value)
            If OrganizationRow.PersonPart.Updated Then db.AddInParameter(dbcmd, "@PersonPart", DbType.String, OrganizationRow.PersonPart.Value)
            If OrganizationRow.CheckPart.Updated Then db.AddInParameter(dbcmd, "@CheckPart", DbType.String, OrganizationRow.CheckPart.Value)
            If OrganizationRow.WorkSiteID.Updated Then db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, OrganizationRow.WorkSiteID.Value)
            If OrganizationRow.PositionID.Updated Then db.AddInParameter(dbcmd, "@PositionID", DbType.String, OrganizationRow.PositionID.Value)
            If OrganizationRow.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, OrganizationRow.WorkTypeID.Value)
            If OrganizationRow.CostDeptID.Updated Then db.AddInParameter(dbcmd, "@CostDeptID", DbType.String, OrganizationRow.CostDeptID.Value)
            If OrganizationRow.CostType.Updated Then db.AddInParameter(dbcmd, "@CostType", DbType.String, OrganizationRow.CostType.Value)
            If OrganizationRow.AccountBranch.Updated Then db.AddInParameter(dbcmd, "@AccountBranch", DbType.String, OrganizationRow.AccountBranch.Value)
            If OrganizationRow.InValidFlag.Updated Then db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, OrganizationRow.InValidFlag.Value)
            If OrganizationRow.ReportingEnd.Updated Then db.AddInParameter(dbcmd, "@ReportingEnd", DbType.String, OrganizationRow.ReportingEnd.Value)
            If OrganizationRow.Depth.Updated Then db.AddInParameter(dbcmd, "@Depth", DbType.String, OrganizationRow.Depth.Value)
            If OrganizationRow.BranchFlag.Updated Then db.AddInParameter(dbcmd, "@BranchFlag", DbType.String, OrganizationRow.BranchFlag.Value)
            If OrganizationRow.RoleCode.Updated Then db.AddInParameter(dbcmd, "@RoleCode", DbType.String, OrganizationRow.RoleCode.Value)
            If OrganizationRow.InvoiceNo.Updated Then db.AddInParameter(dbcmd, "@InvoiceNo", DbType.String, OrganizationRow.InvoiceNo.Value)
            If OrganizationRow.SortOrder.Updated Then db.AddInParameter(dbcmd, "@SortOrder", DbType.String, OrganizationRow.SortOrder.Value)
            If OrganizationRow.OrgType.Updated Then db.AddInParameter(dbcmd, "@OrgType", DbType.String, OrganizationRow.OrgType.Value)
            If OrganizationRow.ValidDateB.Updated Then db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(OrganizationRow.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), OrganizationRow.ValidDateB.Value))
            If OrganizationRow.ValidDateE.Updated Then db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(OrganizationRow.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), OrganizationRow.ValidDateE.Value))
            If OrganizationRow.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, OrganizationRow.Remark.Value)
            If OrganizationRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, OrganizationRow.LastChgComp.Value)
            If OrganizationRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, OrganizationRow.LastChgID.Value)
            If OrganizationRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(OrganizationRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), OrganizationRow.LastChgDate.Value))
            If OrganizationRow.SecPersonPart.Updated Then db.AddInParameter(dbcmd, "@SecPersonPart", DbType.String, OrganizationRow.SecPersonPart.Value)
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(OrganizationRow.LoadFromDataRow, OrganizationRow.CompID.OldValue, OrganizationRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKOrganID", DbType.String, IIf(OrganizationRow.LoadFromDataRow, OrganizationRow.OrganID.OldValue, OrganizationRow.OrganID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal OrganizationRow As Row()) As Integer
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
                    For Each r As Row In OrganizationRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update Organization Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where CompID = @PKCompID")
                        strSQL.AppendLine("And OrganID = @PKOrganID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
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
                        If r.PersonPart.Updated Then db.AddInParameter(dbcmd, "@PersonPart", DbType.String, r.PersonPart.Value)
                        If r.CheckPart.Updated Then db.AddInParameter(dbcmd, "@CheckPart", DbType.String, r.CheckPart.Value)
                        If r.WorkSiteID.Updated Then db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, r.WorkSiteID.Value)
                        If r.PositionID.Updated Then db.AddInParameter(dbcmd, "@PositionID", DbType.String, r.PositionID.Value)
                        If r.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                        If r.CostDeptID.Updated Then db.AddInParameter(dbcmd, "@CostDeptID", DbType.String, r.CostDeptID.Value)
                        If r.CostType.Updated Then db.AddInParameter(dbcmd, "@CostType", DbType.String, r.CostType.Value)
                        If r.AccountBranch.Updated Then db.AddInParameter(dbcmd, "@AccountBranch", DbType.String, r.AccountBranch.Value)
                        If r.InValidFlag.Updated Then db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, r.InValidFlag.Value)
                        If r.ReportingEnd.Updated Then db.AddInParameter(dbcmd, "@ReportingEnd", DbType.String, r.ReportingEnd.Value)
                        If r.Depth.Updated Then db.AddInParameter(dbcmd, "@Depth", DbType.String, r.Depth.Value)
                        If r.BranchFlag.Updated Then db.AddInParameter(dbcmd, "@BranchFlag", DbType.String, r.BranchFlag.Value)
                        If r.RoleCode.Updated Then db.AddInParameter(dbcmd, "@RoleCode", DbType.String, r.RoleCode.Value)
                        If r.InvoiceNo.Updated Then db.AddInParameter(dbcmd, "@InvoiceNo", DbType.String, r.InvoiceNo.Value)
                        If r.SortOrder.Updated Then db.AddInParameter(dbcmd, "@SortOrder", DbType.String, r.SortOrder.Value)
                        If r.OrgType.Updated Then db.AddInParameter(dbcmd, "@OrgType", DbType.String, r.OrgType.Value)
                        If r.ValidDateB.Updated Then db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(r.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateB.Value))
                        If r.ValidDateE.Updated Then db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(r.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateE.Value))
                        If r.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        If r.SecPersonPart.Updated Then db.AddInParameter(dbcmd, "@SecPersonPart", DbType.String, r.SecPersonPart.Value)
                        db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                        db.AddInParameter(dbcmd, "@PKOrganID", DbType.String, IIf(r.LoadFromDataRow, r.OrganID.OldValue, r.OrganID.Value))

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

        Public Function Update(ByVal OrganizationRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In OrganizationRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update Organization Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where CompID = @PKCompID")
                strSQL.AppendLine("And OrganID = @PKOrganID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
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
                If r.PersonPart.Updated Then db.AddInParameter(dbcmd, "@PersonPart", DbType.String, r.PersonPart.Value)
                If r.CheckPart.Updated Then db.AddInParameter(dbcmd, "@CheckPart", DbType.String, r.CheckPart.Value)
                If r.WorkSiteID.Updated Then db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, r.WorkSiteID.Value)
                If r.PositionID.Updated Then db.AddInParameter(dbcmd, "@PositionID", DbType.String, r.PositionID.Value)
                If r.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                If r.CostDeptID.Updated Then db.AddInParameter(dbcmd, "@CostDeptID", DbType.String, r.CostDeptID.Value)
                If r.CostType.Updated Then db.AddInParameter(dbcmd, "@CostType", DbType.String, r.CostType.Value)
                If r.AccountBranch.Updated Then db.AddInParameter(dbcmd, "@AccountBranch", DbType.String, r.AccountBranch.Value)
                If r.InValidFlag.Updated Then db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, r.InValidFlag.Value)
                If r.ReportingEnd.Updated Then db.AddInParameter(dbcmd, "@ReportingEnd", DbType.String, r.ReportingEnd.Value)
                If r.Depth.Updated Then db.AddInParameter(dbcmd, "@Depth", DbType.String, r.Depth.Value)
                If r.BranchFlag.Updated Then db.AddInParameter(dbcmd, "@BranchFlag", DbType.String, r.BranchFlag.Value)
                If r.RoleCode.Updated Then db.AddInParameter(dbcmd, "@RoleCode", DbType.String, r.RoleCode.Value)
                If r.InvoiceNo.Updated Then db.AddInParameter(dbcmd, "@InvoiceNo", DbType.String, r.InvoiceNo.Value)
                If r.SortOrder.Updated Then db.AddInParameter(dbcmd, "@SortOrder", DbType.String, r.SortOrder.Value)
                If r.OrgType.Updated Then db.AddInParameter(dbcmd, "@OrgType", DbType.String, r.OrgType.Value)
                If r.ValidDateB.Updated Then db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(r.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateB.Value))
                If r.ValidDateE.Updated Then db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(r.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateE.Value))
                If r.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                If r.SecPersonPart.Updated Then db.AddInParameter(dbcmd, "@SecPersonPart", DbType.String, r.SecPersonPart.Value)
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                db.AddInParameter(dbcmd, "@PKOrganID", DbType.String, IIf(r.LoadFromDataRow, r.OrganID.OldValue, r.OrganID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal OrganizationRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From Organization")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And OrganID = @OrganID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, OrganizationRow.CompID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationRow.OrganID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal OrganizationRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From Organization")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And OrganID = @OrganID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, OrganizationRow.CompID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationRow.OrganID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Organization")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal OrganizationRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into Organization")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, OrganID, OrganName, OrganEngName, VirtualFlag, UpOrganID, DeptID, GroupType,")
            strSQL.AppendLine("    GroupID, Boss, BossCompID, BossType, BossTemporary, SecBoss, SecBossCompID, PersonPart,")
            strSQL.AppendLine("    CheckPart, WorkSiteID, PositionID, WorkTypeID, CostDeptID, CostType, AccountBranch,")
            strSQL.AppendLine("    InValidFlag, ReportingEnd, Depth, BranchFlag, RoleCode, InvoiceNo, SortOrder, OrgType,")
            strSQL.AppendLine("    ValidDateB, ValidDateE, Remark, LastChgComp, LastChgID, LastChgDate, SecPersonPart")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @OrganID, @OrganName, @OrganEngName, @VirtualFlag, @UpOrganID, @DeptID, @GroupType,")
            strSQL.AppendLine("    @GroupID, @Boss, @BossCompID, @BossType, @BossTemporary, @SecBoss, @SecBossCompID, @PersonPart,")
            strSQL.AppendLine("    @CheckPart, @WorkSiteID, @PositionID, @WorkTypeID, @CostDeptID, @CostType, @AccountBranch,")
            strSQL.AppendLine("    @InValidFlag, @ReportingEnd, @Depth, @BranchFlag, @RoleCode, @InvoiceNo, @SortOrder, @OrgType,")
            strSQL.AppendLine("    @ValidDateB, @ValidDateE, @Remark, @LastChgComp, @LastChgID, @LastChgDate, @SecPersonPart")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, OrganizationRow.CompID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@OrganName", DbType.String, OrganizationRow.OrganName.Value)
            db.AddInParameter(dbcmd, "@OrganEngName", DbType.String, OrganizationRow.OrganEngName.Value)
            db.AddInParameter(dbcmd, "@VirtualFlag", DbType.String, OrganizationRow.VirtualFlag.Value)
            db.AddInParameter(dbcmd, "@UpOrganID", DbType.String, OrganizationRow.UpOrganID.Value)
            db.AddInParameter(dbcmd, "@DeptID", DbType.String, OrganizationRow.DeptID.Value)
            db.AddInParameter(dbcmd, "@GroupType", DbType.String, OrganizationRow.GroupType.Value)
            db.AddInParameter(dbcmd, "@GroupID", DbType.String, OrganizationRow.GroupID.Value)
            db.AddInParameter(dbcmd, "@Boss", DbType.String, OrganizationRow.Boss.Value)
            db.AddInParameter(dbcmd, "@BossCompID", DbType.String, OrganizationRow.BossCompID.Value)
            db.AddInParameter(dbcmd, "@BossType", DbType.String, OrganizationRow.BossType.Value)
            db.AddInParameter(dbcmd, "@BossTemporary", DbType.String, OrganizationRow.BossTemporary.Value)
            db.AddInParameter(dbcmd, "@SecBoss", DbType.String, OrganizationRow.SecBoss.Value)
            db.AddInParameter(dbcmd, "@SecBossCompID", DbType.String, OrganizationRow.SecBossCompID.Value)
            db.AddInParameter(dbcmd, "@PersonPart", DbType.String, OrganizationRow.PersonPart.Value)
            db.AddInParameter(dbcmd, "@CheckPart", DbType.String, OrganizationRow.CheckPart.Value)
            db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, OrganizationRow.WorkSiteID.Value)
            db.AddInParameter(dbcmd, "@PositionID", DbType.String, OrganizationRow.PositionID.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, OrganizationRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@CostDeptID", DbType.String, OrganizationRow.CostDeptID.Value)
            db.AddInParameter(dbcmd, "@CostType", DbType.String, OrganizationRow.CostType.Value)
            db.AddInParameter(dbcmd, "@AccountBranch", DbType.String, OrganizationRow.AccountBranch.Value)
            db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, OrganizationRow.InValidFlag.Value)
            db.AddInParameter(dbcmd, "@ReportingEnd", DbType.String, OrganizationRow.ReportingEnd.Value)
            db.AddInParameter(dbcmd, "@Depth", DbType.String, OrganizationRow.Depth.Value)
            db.AddInParameter(dbcmd, "@BranchFlag", DbType.String, OrganizationRow.BranchFlag.Value)
            db.AddInParameter(dbcmd, "@RoleCode", DbType.String, OrganizationRow.RoleCode.Value)
            db.AddInParameter(dbcmd, "@InvoiceNo", DbType.String, OrganizationRow.InvoiceNo.Value)
            db.AddInParameter(dbcmd, "@SortOrder", DbType.String, OrganizationRow.SortOrder.Value)
            db.AddInParameter(dbcmd, "@OrgType", DbType.String, OrganizationRow.OrgType.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(OrganizationRow.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), OrganizationRow.ValidDateB.Value))
            db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(OrganizationRow.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), OrganizationRow.ValidDateE.Value))
            db.AddInParameter(dbcmd, "@Remark", DbType.String, OrganizationRow.Remark.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, OrganizationRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, OrganizationRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(OrganizationRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), OrganizationRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@SecPersonPart", DbType.String, OrganizationRow.SecPersonPart.Value)

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal OrganizationRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into Organization")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, OrganID, OrganName, OrganEngName, VirtualFlag, UpOrganID, DeptID, GroupType,")
            strSQL.AppendLine("    GroupID, Boss, BossCompID, BossType, BossTemporary, SecBoss, SecBossCompID, PersonPart,")
            strSQL.AppendLine("    CheckPart, WorkSiteID, PositionID, WorkTypeID, CostDeptID, CostType, AccountBranch,")
            strSQL.AppendLine("    InValidFlag, ReportingEnd, Depth, BranchFlag, RoleCode, InvoiceNo, SortOrder, OrgType,")
            strSQL.AppendLine("    ValidDateB, ValidDateE, Remark, LastChgComp, LastChgID, LastChgDate, SecPersonPart")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @OrganID, @OrganName, @OrganEngName, @VirtualFlag, @UpOrganID, @DeptID, @GroupType,")
            strSQL.AppendLine("    @GroupID, @Boss, @BossCompID, @BossType, @BossTemporary, @SecBoss, @SecBossCompID, @PersonPart,")
            strSQL.AppendLine("    @CheckPart, @WorkSiteID, @PositionID, @WorkTypeID, @CostDeptID, @CostType, @AccountBranch,")
            strSQL.AppendLine("    @InValidFlag, @ReportingEnd, @Depth, @BranchFlag, @RoleCode, @InvoiceNo, @SortOrder, @OrgType,")
            strSQL.AppendLine("    @ValidDateB, @ValidDateE, @Remark, @LastChgComp, @LastChgID, @LastChgDate, @SecPersonPart")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, OrganizationRow.CompID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@OrganName", DbType.String, OrganizationRow.OrganName.Value)
            db.AddInParameter(dbcmd, "@OrganEngName", DbType.String, OrganizationRow.OrganEngName.Value)
            db.AddInParameter(dbcmd, "@VirtualFlag", DbType.String, OrganizationRow.VirtualFlag.Value)
            db.AddInParameter(dbcmd, "@UpOrganID", DbType.String, OrganizationRow.UpOrganID.Value)
            db.AddInParameter(dbcmd, "@DeptID", DbType.String, OrganizationRow.DeptID.Value)
            db.AddInParameter(dbcmd, "@GroupType", DbType.String, OrganizationRow.GroupType.Value)
            db.AddInParameter(dbcmd, "@GroupID", DbType.String, OrganizationRow.GroupID.Value)
            db.AddInParameter(dbcmd, "@Boss", DbType.String, OrganizationRow.Boss.Value)
            db.AddInParameter(dbcmd, "@BossCompID", DbType.String, OrganizationRow.BossCompID.Value)
            db.AddInParameter(dbcmd, "@BossType", DbType.String, OrganizationRow.BossType.Value)
            db.AddInParameter(dbcmd, "@BossTemporary", DbType.String, OrganizationRow.BossTemporary.Value)
            db.AddInParameter(dbcmd, "@SecBoss", DbType.String, OrganizationRow.SecBoss.Value)
            db.AddInParameter(dbcmd, "@SecBossCompID", DbType.String, OrganizationRow.SecBossCompID.Value)
            db.AddInParameter(dbcmd, "@PersonPart", DbType.String, OrganizationRow.PersonPart.Value)
            db.AddInParameter(dbcmd, "@CheckPart", DbType.String, OrganizationRow.CheckPart.Value)
            db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, OrganizationRow.WorkSiteID.Value)
            db.AddInParameter(dbcmd, "@PositionID", DbType.String, OrganizationRow.PositionID.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, OrganizationRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@CostDeptID", DbType.String, OrganizationRow.CostDeptID.Value)
            db.AddInParameter(dbcmd, "@CostType", DbType.String, OrganizationRow.CostType.Value)
            db.AddInParameter(dbcmd, "@AccountBranch", DbType.String, OrganizationRow.AccountBranch.Value)
            db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, OrganizationRow.InValidFlag.Value)
            db.AddInParameter(dbcmd, "@ReportingEnd", DbType.String, OrganizationRow.ReportingEnd.Value)
            db.AddInParameter(dbcmd, "@Depth", DbType.String, OrganizationRow.Depth.Value)
            db.AddInParameter(dbcmd, "@BranchFlag", DbType.String, OrganizationRow.BranchFlag.Value)
            db.AddInParameter(dbcmd, "@RoleCode", DbType.String, OrganizationRow.RoleCode.Value)
            db.AddInParameter(dbcmd, "@InvoiceNo", DbType.String, OrganizationRow.InvoiceNo.Value)
            db.AddInParameter(dbcmd, "@SortOrder", DbType.String, OrganizationRow.SortOrder.Value)
            db.AddInParameter(dbcmd, "@OrgType", DbType.String, OrganizationRow.OrgType.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(OrganizationRow.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), OrganizationRow.ValidDateB.Value))
            db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(OrganizationRow.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), OrganizationRow.ValidDateE.Value))
            db.AddInParameter(dbcmd, "@Remark", DbType.String, OrganizationRow.Remark.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, OrganizationRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, OrganizationRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(OrganizationRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), OrganizationRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@SecPersonPart", DbType.String, OrganizationRow.SecPersonPart.Value)

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal OrganizationRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into Organization")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, OrganID, OrganName, OrganEngName, VirtualFlag, UpOrganID, DeptID, GroupType,")
            strSQL.AppendLine("    GroupID, Boss, BossCompID, BossType, BossTemporary, SecBoss, SecBossCompID, PersonPart,")
            strSQL.AppendLine("    CheckPart, WorkSiteID, PositionID, WorkTypeID, CostDeptID, CostType, AccountBranch,")
            strSQL.AppendLine("    InValidFlag, ReportingEnd, Depth, BranchFlag, RoleCode, InvoiceNo, SortOrder, OrgType,")
            strSQL.AppendLine("    ValidDateB, ValidDateE, Remark, LastChgComp, LastChgID, LastChgDate, SecPersonPart")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @OrganID, @OrganName, @OrganEngName, @VirtualFlag, @UpOrganID, @DeptID, @GroupType,")
            strSQL.AppendLine("    @GroupID, @Boss, @BossCompID, @BossType, @BossTemporary, @SecBoss, @SecBossCompID, @PersonPart,")
            strSQL.AppendLine("    @CheckPart, @WorkSiteID, @PositionID, @WorkTypeID, @CostDeptID, @CostType, @AccountBranch,")
            strSQL.AppendLine("    @InValidFlag, @ReportingEnd, @Depth, @BranchFlag, @RoleCode, @InvoiceNo, @SortOrder, @OrgType,")
            strSQL.AppendLine("    @ValidDateB, @ValidDateE, @Remark, @LastChgComp, @LastChgID, @LastChgDate, @SecPersonPart")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In OrganizationRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
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
                        db.AddInParameter(dbcmd, "@PersonPart", DbType.String, r.PersonPart.Value)
                        db.AddInParameter(dbcmd, "@CheckPart", DbType.String, r.CheckPart.Value)
                        db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, r.WorkSiteID.Value)
                        db.AddInParameter(dbcmd, "@PositionID", DbType.String, r.PositionID.Value)
                        db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                        db.AddInParameter(dbcmd, "@CostDeptID", DbType.String, r.CostDeptID.Value)
                        db.AddInParameter(dbcmd, "@CostType", DbType.String, r.CostType.Value)
                        db.AddInParameter(dbcmd, "@AccountBranch", DbType.String, r.AccountBranch.Value)
                        db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, r.InValidFlag.Value)
                        db.AddInParameter(dbcmd, "@ReportingEnd", DbType.String, r.ReportingEnd.Value)
                        db.AddInParameter(dbcmd, "@Depth", DbType.String, r.Depth.Value)
                        db.AddInParameter(dbcmd, "@BranchFlag", DbType.String, r.BranchFlag.Value)
                        db.AddInParameter(dbcmd, "@RoleCode", DbType.String, r.RoleCode.Value)
                        db.AddInParameter(dbcmd, "@InvoiceNo", DbType.String, r.InvoiceNo.Value)
                        db.AddInParameter(dbcmd, "@SortOrder", DbType.String, r.SortOrder.Value)
                        db.AddInParameter(dbcmd, "@OrgType", DbType.String, r.OrgType.Value)
                        db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(r.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateB.Value))
                        db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(r.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateE.Value))
                        db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                        db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@SecPersonPart", DbType.String, r.SecPersonPart.Value)

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

        Public Function Insert(ByVal OrganizationRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into Organization")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, OrganID, OrganName, OrganEngName, VirtualFlag, UpOrganID, DeptID, GroupType,")
            strSQL.AppendLine("    GroupID, Boss, BossCompID, BossType, BossTemporary, SecBoss, SecBossCompID, PersonPart,")
            strSQL.AppendLine("    CheckPart, WorkSiteID, PositionID, WorkTypeID, CostDeptID, CostType, AccountBranch,")
            strSQL.AppendLine("    InValidFlag, ReportingEnd, Depth, BranchFlag, RoleCode, InvoiceNo, SortOrder, OrgType,")
            strSQL.AppendLine("    ValidDateB, ValidDateE, Remark, LastChgComp, LastChgID, LastChgDate, SecPersonPart")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @OrganID, @OrganName, @OrganEngName, @VirtualFlag, @UpOrganID, @DeptID, @GroupType,")
            strSQL.AppendLine("    @GroupID, @Boss, @BossCompID, @BossType, @BossTemporary, @SecBoss, @SecBossCompID, @PersonPart,")
            strSQL.AppendLine("    @CheckPart, @WorkSiteID, @PositionID, @WorkTypeID, @CostDeptID, @CostType, @AccountBranch,")
            strSQL.AppendLine("    @InValidFlag, @ReportingEnd, @Depth, @BranchFlag, @RoleCode, @InvoiceNo, @SortOrder, @OrgType,")
            strSQL.AppendLine("    @ValidDateB, @ValidDateE, @Remark, @LastChgComp, @LastChgID, @LastChgDate, @SecPersonPart")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In OrganizationRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
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
                db.AddInParameter(dbcmd, "@PersonPart", DbType.String, r.PersonPart.Value)
                db.AddInParameter(dbcmd, "@CheckPart", DbType.String, r.CheckPart.Value)
                db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, r.WorkSiteID.Value)
                db.AddInParameter(dbcmd, "@PositionID", DbType.String, r.PositionID.Value)
                db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                db.AddInParameter(dbcmd, "@CostDeptID", DbType.String, r.CostDeptID.Value)
                db.AddInParameter(dbcmd, "@CostType", DbType.String, r.CostType.Value)
                db.AddInParameter(dbcmd, "@AccountBranch", DbType.String, r.AccountBranch.Value)
                db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, r.InValidFlag.Value)
                db.AddInParameter(dbcmd, "@ReportingEnd", DbType.String, r.ReportingEnd.Value)
                db.AddInParameter(dbcmd, "@Depth", DbType.String, r.Depth.Value)
                db.AddInParameter(dbcmd, "@BranchFlag", DbType.String, r.BranchFlag.Value)
                db.AddInParameter(dbcmd, "@RoleCode", DbType.String, r.RoleCode.Value)
                db.AddInParameter(dbcmd, "@InvoiceNo", DbType.String, r.InvoiceNo.Value)
                db.AddInParameter(dbcmd, "@SortOrder", DbType.String, r.SortOrder.Value)
                db.AddInParameter(dbcmd, "@OrgType", DbType.String, r.OrgType.Value)
                db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(r.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateB.Value))
                db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(r.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateE.Value))
                db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@SecPersonPart", DbType.String, r.SecPersonPart.Value)

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

