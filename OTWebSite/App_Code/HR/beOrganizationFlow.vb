'****************************************************************
' Table:OrganizationFlow
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

Namespace beOrganizationFlow
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "OrganID", "OrganName", "OrganEngName", "VirtualFlag", "UpOrganID", "DeptID", "GroupType", "GroupID", "Boss" _
                                    , "BossCompID", "BossType", "BossTemporary", "SecBoss", "SecBossCompID", "InValidFlag", "ReportingEnd", "Depth", "BranchFlag", "DelegateFlag", "DeptID1" _
                                    , "StopDeptID", "CompareFlag", "FlowOrganID", "RoleCode", "BusinessType", "OrganNo", "OrgType", "ValidDateB", "ValidDateE", "Remark", "LastChgComp" _
                                    , "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(Date), GetType(String), GetType(String) _
                                    , GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "OrganID" }

        Public ReadOnly Property Rows() As beOrganizationFlow.Rows 
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
        Public Sub Transfer2Row(OrganizationFlowTable As DataTable)
            For Each dr As DataRow In OrganizationFlowTable.Rows
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
                dr(m_Rows(i).InValidFlag.FieldName) = m_Rows(i).InValidFlag.Value
                dr(m_Rows(i).ReportingEnd.FieldName) = m_Rows(i).ReportingEnd.Value
                dr(m_Rows(i).Depth.FieldName) = m_Rows(i).Depth.Value
                dr(m_Rows(i).BranchFlag.FieldName) = m_Rows(i).BranchFlag.Value
                dr(m_Rows(i).DelegateFlag.FieldName) = m_Rows(i).DelegateFlag.Value
                dr(m_Rows(i).DeptID1.FieldName) = m_Rows(i).DeptID1.Value
                dr(m_Rows(i).StopDeptID.FieldName) = m_Rows(i).StopDeptID.Value
                dr(m_Rows(i).CompareFlag.FieldName) = m_Rows(i).CompareFlag.Value
                dr(m_Rows(i).FlowOrganID.FieldName) = m_Rows(i).FlowOrganID.Value
                dr(m_Rows(i).RoleCode.FieldName) = m_Rows(i).RoleCode.Value
                dr(m_Rows(i).BusinessType.FieldName) = m_Rows(i).BusinessType.Value
                dr(m_Rows(i).OrganNo.FieldName) = m_Rows(i).OrganNo.Value
                dr(m_Rows(i).OrgType.FieldName) = m_Rows(i).OrgType.Value
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

        Public Sub Add(OrganizationFlowRow As Row)
            m_Rows.Add(OrganizationFlowRow)
        End Sub

        Public Sub Remove(OrganizationFlowRow As Row)
            If m_Rows.IndexOf(OrganizationFlowRow) >= 0 Then
                m_Rows.Remove(OrganizationFlowRow)
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
        Private FI_InValidFlag As Field(Of String) = new Field(Of String)("InValidFlag", true)
        Private FI_ReportingEnd As Field(Of String) = new Field(Of String)("ReportingEnd", true)
        Private FI_Depth As Field(Of String) = new Field(Of String)("Depth", true)
        Private FI_BranchFlag As Field(Of String) = new Field(Of String)("BranchFlag", true)
        Private FI_DelegateFlag As Field(Of String) = new Field(Of String)("DelegateFlag", true)
        Private FI_DeptID1 As Field(Of String) = new Field(Of String)("DeptID1", true)
        Private FI_StopDeptID As Field(Of String) = new Field(Of String)("StopDeptID", true)
        Private FI_CompareFlag As Field(Of String) = new Field(Of String)("CompareFlag", true)
        Private FI_FlowOrganID As Field(Of String) = new Field(Of String)("FlowOrganID", true)
        Private FI_RoleCode As Field(Of String) = new Field(Of String)("RoleCode", true)
        Private FI_BusinessType As Field(Of String) = new Field(Of String)("BusinessType", true)
        Private FI_OrganNo As Field(Of String) = new Field(Of String)("OrganNo", true)
        Private FI_OrgType As Field(Of String) = new Field(Of String)("OrgType", true)
        Private FI_ValidDateB As Field(Of Date) = new Field(Of Date)("ValidDateB", true)
        Private FI_ValidDateE As Field(Of Date) = new Field(Of Date)("ValidDateE", true)
        Private FI_Remark As Field(Of String) = new Field(Of String)("Remark", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "CompID", "OrganID", "OrganName", "OrganEngName", "VirtualFlag", "UpOrganID", "DeptID", "GroupType", "GroupID", "Boss" _
                                    , "BossCompID", "BossType", "BossTemporary", "SecBoss", "SecBossCompID", "InValidFlag", "ReportingEnd", "Depth", "BranchFlag", "DelegateFlag", "DeptID1" _
                                    , "StopDeptID", "CompareFlag", "FlowOrganID", "RoleCode", "BusinessType", "OrganNo", "OrgType", "ValidDateB", "ValidDateE", "Remark", "LastChgComp" _
                                    , "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "OrganID" }
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
                Case "InValidFlag"
                    Return FI_InValidFlag.Value
                Case "ReportingEnd"
                    Return FI_ReportingEnd.Value
                Case "Depth"
                    Return FI_Depth.Value
                Case "BranchFlag"
                    Return FI_BranchFlag.Value
                Case "DelegateFlag"
                    Return FI_DelegateFlag.Value
                Case "DeptID1"
                    Return FI_DeptID1.Value
                Case "StopDeptID"
                    Return FI_StopDeptID.Value
                Case "CompareFlag"
                    Return FI_CompareFlag.Value
                Case "FlowOrganID"
                    Return FI_FlowOrganID.Value
                Case "RoleCode"
                    Return FI_RoleCode.Value
                Case "BusinessType"
                    Return FI_BusinessType.Value
                Case "OrganNo"
                    Return FI_OrganNo.Value
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
                Case "InValidFlag"
                    FI_InValidFlag.SetValue(value)
                Case "ReportingEnd"
                    FI_ReportingEnd.SetValue(value)
                Case "Depth"
                    FI_Depth.SetValue(value)
                Case "BranchFlag"
                    FI_BranchFlag.SetValue(value)
                Case "DelegateFlag"
                    FI_DelegateFlag.SetValue(value)
                Case "DeptID1"
                    FI_DeptID1.SetValue(value)
                Case "StopDeptID"
                    FI_StopDeptID.SetValue(value)
                Case "CompareFlag"
                    FI_CompareFlag.SetValue(value)
                Case "FlowOrganID"
                    FI_FlowOrganID.SetValue(value)
                Case "RoleCode"
                    FI_RoleCode.SetValue(value)
                Case "BusinessType"
                    FI_BusinessType.SetValue(value)
                Case "OrganNo"
                    FI_OrganNo.SetValue(value)
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
                Case "InValidFlag"
                    return FI_InValidFlag.Updated
                Case "ReportingEnd"
                    return FI_ReportingEnd.Updated
                Case "Depth"
                    return FI_Depth.Updated
                Case "BranchFlag"
                    return FI_BranchFlag.Updated
                Case "DelegateFlag"
                    return FI_DelegateFlag.Updated
                Case "DeptID1"
                    return FI_DeptID1.Updated
                Case "StopDeptID"
                    return FI_StopDeptID.Updated
                Case "CompareFlag"
                    return FI_CompareFlag.Updated
                Case "FlowOrganID"
                    return FI_FlowOrganID.Updated
                Case "RoleCode"
                    return FI_RoleCode.Updated
                Case "BusinessType"
                    return FI_BusinessType.Updated
                Case "OrganNo"
                    return FI_OrganNo.Updated
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
                Case "InValidFlag"
                    return FI_InValidFlag.CreateUpdateSQL
                Case "ReportingEnd"
                    return FI_ReportingEnd.CreateUpdateSQL
                Case "Depth"
                    return FI_Depth.CreateUpdateSQL
                Case "BranchFlag"
                    return FI_BranchFlag.CreateUpdateSQL
                Case "DelegateFlag"
                    return FI_DelegateFlag.CreateUpdateSQL
                Case "DeptID1"
                    return FI_DeptID1.CreateUpdateSQL
                Case "StopDeptID"
                    return FI_StopDeptID.CreateUpdateSQL
                Case "CompareFlag"
                    return FI_CompareFlag.CreateUpdateSQL
                Case "FlowOrganID"
                    return FI_FlowOrganID.CreateUpdateSQL
                Case "RoleCode"
                    return FI_RoleCode.CreateUpdateSQL
                Case "BusinessType"
                    return FI_BusinessType.CreateUpdateSQL
                Case "OrganNo"
                    return FI_OrganNo.CreateUpdateSQL
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
            FI_VirtualFlag.SetInitValue("")
            FI_UpOrganID.SetInitValue("")
            FI_DeptID.SetInitValue("")
            FI_GroupType.SetInitValue("")
            FI_GroupID.SetInitValue("")
            FI_Boss.SetInitValue("")
            FI_BossCompID.SetInitValue("")
            FI_BossType.SetInitValue("1")
            FI_BossTemporary.SetInitValue("0")
            FI_SecBoss.SetInitValue("")
            FI_SecBossCompID.SetInitValue("")
            FI_InValidFlag.SetInitValue("")
            FI_ReportingEnd.SetInitValue("")
            FI_Depth.SetInitValue("")
            FI_BranchFlag.SetInitValue("")
            FI_DelegateFlag.SetInitValue("")
            FI_DeptID1.SetInitValue("")
            FI_StopDeptID.SetInitValue("")
            FI_CompareFlag.SetInitValue("0")
            FI_FlowOrganID.SetInitValue("")
            FI_RoleCode.SetInitValue("")
            FI_BusinessType.SetInitValue("")
            FI_OrganNo.SetInitValue("0")
            FI_OrgType.SetInitValue("")
            FI_ValidDateB.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_ValidDateE.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_Remark.SetInitValue("")
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
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
            FI_InValidFlag.SetInitValue(dr("InValidFlag"))
            FI_ReportingEnd.SetInitValue(dr("ReportingEnd"))
            FI_Depth.SetInitValue(dr("Depth"))
            FI_BranchFlag.SetInitValue(dr("BranchFlag"))
            FI_DelegateFlag.SetInitValue(dr("DelegateFlag"))
            FI_DeptID1.SetInitValue(dr("DeptID1"))
            FI_StopDeptID.SetInitValue(dr("StopDeptID"))
            FI_CompareFlag.SetInitValue(dr("CompareFlag"))
            FI_FlowOrganID.SetInitValue(dr("FlowOrganID"))
            FI_RoleCode.SetInitValue(dr("RoleCode"))
            FI_BusinessType.SetInitValue(dr("BusinessType"))
            FI_OrganNo.SetInitValue(dr("OrganNo"))
            FI_OrgType.SetInitValue(dr("OrgType"))
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
            FI_InValidFlag.Updated = False
            FI_ReportingEnd.Updated = False
            FI_Depth.Updated = False
            FI_BranchFlag.Updated = False
            FI_DelegateFlag.Updated = False
            FI_DeptID1.Updated = False
            FI_StopDeptID.Updated = False
            FI_CompareFlag.Updated = False
            FI_FlowOrganID.Updated = False
            FI_RoleCode.Updated = False
            FI_BusinessType.Updated = False
            FI_OrganNo.Updated = False
            FI_OrgType.Updated = False
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

        Public ReadOnly Property DelegateFlag As Field(Of String) 
            Get
                Return FI_DelegateFlag
            End Get
        End Property

        Public ReadOnly Property DeptID1 As Field(Of String) 
            Get
                Return FI_DeptID1
            End Get
        End Property

        Public ReadOnly Property StopDeptID As Field(Of String) 
            Get
                Return FI_StopDeptID
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

        Public ReadOnly Property RoleCode As Field(Of String) 
            Get
                Return FI_RoleCode
            End Get
        End Property

        Public ReadOnly Property BusinessType As Field(Of String) 
            Get
                Return FI_BusinessType
            End Get
        End Property

        Public ReadOnly Property OrganNo As Field(Of String) 
            Get
                Return FI_OrganNo
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

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal OrganizationFlowRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From OrganizationFlow")
            strSQL.AppendLine("Where OrganID = @OrganID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationFlowRow.OrganID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal OrganizationFlowRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From OrganizationFlow")
            strSQL.AppendLine("Where OrganID = @OrganID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationFlowRow.OrganID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal OrganizationFlowRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From OrganizationFlow")
            strSQL.AppendLine("Where OrganID = @OrganID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In OrganizationFlowRow
                        dbcmd.Parameters.Clear()
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

        Public Function DeleteRowByPrimaryKey(ByVal OrganizationFlowRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From OrganizationFlow")
            strSQL.AppendLine("Where OrganID = @OrganID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In OrganizationFlowRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal OrganizationFlowRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From OrganizationFlow")
            strSQL.AppendLine("Where OrganID = @OrganID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationFlowRow.OrganID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(OrganizationFlowRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From OrganizationFlow")
            strSQL.AppendLine("Where OrganID = @OrganID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationFlowRow.OrganID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal OrganizationFlowRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update OrganizationFlow Set")
            For i As Integer = 0 To OrganizationFlowRow.FieldNames.Length - 1
                If Not OrganizationFlowRow.IsIdentityField(OrganizationFlowRow.FieldNames(i)) AndAlso OrganizationFlowRow.IsUpdated(OrganizationFlowRow.FieldNames(i)) AndAlso OrganizationFlowRow.CreateUpdateSQL(OrganizationFlowRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, OrganizationFlowRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where OrganID = @PKOrganID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If OrganizationFlowRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, OrganizationFlowRow.CompID.Value)
            If OrganizationFlowRow.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationFlowRow.OrganID.Value)
            If OrganizationFlowRow.OrganName.Updated Then db.AddInParameter(dbcmd, "@OrganName", DbType.String, OrganizationFlowRow.OrganName.Value)
            If OrganizationFlowRow.OrganEngName.Updated Then db.AddInParameter(dbcmd, "@OrganEngName", DbType.String, OrganizationFlowRow.OrganEngName.Value)
            If OrganizationFlowRow.VirtualFlag.Updated Then db.AddInParameter(dbcmd, "@VirtualFlag", DbType.String, OrganizationFlowRow.VirtualFlag.Value)
            If OrganizationFlowRow.UpOrganID.Updated Then db.AddInParameter(dbcmd, "@UpOrganID", DbType.String, OrganizationFlowRow.UpOrganID.Value)
            If OrganizationFlowRow.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, OrganizationFlowRow.DeptID.Value)
            If OrganizationFlowRow.GroupType.Updated Then db.AddInParameter(dbcmd, "@GroupType", DbType.String, OrganizationFlowRow.GroupType.Value)
            If OrganizationFlowRow.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, OrganizationFlowRow.GroupID.Value)
            If OrganizationFlowRow.Boss.Updated Then db.AddInParameter(dbcmd, "@Boss", DbType.String, OrganizationFlowRow.Boss.Value)
            If OrganizationFlowRow.BossCompID.Updated Then db.AddInParameter(dbcmd, "@BossCompID", DbType.String, OrganizationFlowRow.BossCompID.Value)
            If OrganizationFlowRow.BossType.Updated Then db.AddInParameter(dbcmd, "@BossType", DbType.String, OrganizationFlowRow.BossType.Value)
            If OrganizationFlowRow.BossTemporary.Updated Then db.AddInParameter(dbcmd, "@BossTemporary", DbType.String, OrganizationFlowRow.BossTemporary.Value)
            If OrganizationFlowRow.SecBoss.Updated Then db.AddInParameter(dbcmd, "@SecBoss", DbType.String, OrganizationFlowRow.SecBoss.Value)
            If OrganizationFlowRow.SecBossCompID.Updated Then db.AddInParameter(dbcmd, "@SecBossCompID", DbType.String, OrganizationFlowRow.SecBossCompID.Value)
            If OrganizationFlowRow.InValidFlag.Updated Then db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, OrganizationFlowRow.InValidFlag.Value)
            If OrganizationFlowRow.ReportingEnd.Updated Then db.AddInParameter(dbcmd, "@ReportingEnd", DbType.String, OrganizationFlowRow.ReportingEnd.Value)
            If OrganizationFlowRow.Depth.Updated Then db.AddInParameter(dbcmd, "@Depth", DbType.String, OrganizationFlowRow.Depth.Value)
            If OrganizationFlowRow.BranchFlag.Updated Then db.AddInParameter(dbcmd, "@BranchFlag", DbType.String, OrganizationFlowRow.BranchFlag.Value)
            If OrganizationFlowRow.DelegateFlag.Updated Then db.AddInParameter(dbcmd, "@DelegateFlag", DbType.String, OrganizationFlowRow.DelegateFlag.Value)
            If OrganizationFlowRow.DeptID1.Updated Then db.AddInParameter(dbcmd, "@DeptID1", DbType.String, OrganizationFlowRow.DeptID1.Value)
            If OrganizationFlowRow.StopDeptID.Updated Then db.AddInParameter(dbcmd, "@StopDeptID", DbType.String, OrganizationFlowRow.StopDeptID.Value)
            If OrganizationFlowRow.CompareFlag.Updated Then db.AddInParameter(dbcmd, "@CompareFlag", DbType.String, OrganizationFlowRow.CompareFlag.Value)
            If OrganizationFlowRow.FlowOrganID.Updated Then db.AddInParameter(dbcmd, "@FlowOrganID", DbType.String, OrganizationFlowRow.FlowOrganID.Value)
            If OrganizationFlowRow.RoleCode.Updated Then db.AddInParameter(dbcmd, "@RoleCode", DbType.String, OrganizationFlowRow.RoleCode.Value)
            If OrganizationFlowRow.BusinessType.Updated Then db.AddInParameter(dbcmd, "@BusinessType", DbType.String, OrganizationFlowRow.BusinessType.Value)
            If OrganizationFlowRow.OrganNo.Updated Then db.AddInParameter(dbcmd, "@OrganNo", DbType.String, OrganizationFlowRow.OrganNo.Value)
            If OrganizationFlowRow.OrgType.Updated Then db.AddInParameter(dbcmd, "@OrgType", DbType.String, OrganizationFlowRow.OrgType.Value)
            If OrganizationFlowRow.ValidDateB.Updated Then db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(OrganizationFlowRow.ValidDateB.Value), DBNull.Value, OrganizationFlowRow.ValidDateB.Value))
            If OrganizationFlowRow.ValidDateE.Updated Then db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(OrganizationFlowRow.ValidDateE.Value), DBNull.Value, OrganizationFlowRow.ValidDateE.Value))
            If OrganizationFlowRow.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, OrganizationFlowRow.Remark.Value)
            If OrganizationFlowRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, OrganizationFlowRow.LastChgComp.Value)
            If OrganizationFlowRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, OrganizationFlowRow.LastChgID.Value)
            If OrganizationFlowRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(OrganizationFlowRow.LastChgDate.Value), DBNull.Value, OrganizationFlowRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKOrganID", DbType.String, IIf(OrganizationFlowRow.LoadFromDataRow, OrganizationFlowRow.OrganID.OldValue, OrganizationFlowRow.OrganID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal OrganizationFlowRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update OrganizationFlow Set")
            For i As Integer = 0 To OrganizationFlowRow.FieldNames.Length - 1
                If Not OrganizationFlowRow.IsIdentityField(OrganizationFlowRow.FieldNames(i)) AndAlso OrganizationFlowRow.IsUpdated(OrganizationFlowRow.FieldNames(i)) AndAlso OrganizationFlowRow.CreateUpdateSQL(OrganizationFlowRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, OrganizationFlowRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where OrganID = @PKOrganID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If OrganizationFlowRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, OrganizationFlowRow.CompID.Value)
            If OrganizationFlowRow.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationFlowRow.OrganID.Value)
            If OrganizationFlowRow.OrganName.Updated Then db.AddInParameter(dbcmd, "@OrganName", DbType.String, OrganizationFlowRow.OrganName.Value)
            If OrganizationFlowRow.OrganEngName.Updated Then db.AddInParameter(dbcmd, "@OrganEngName", DbType.String, OrganizationFlowRow.OrganEngName.Value)
            If OrganizationFlowRow.VirtualFlag.Updated Then db.AddInParameter(dbcmd, "@VirtualFlag", DbType.String, OrganizationFlowRow.VirtualFlag.Value)
            If OrganizationFlowRow.UpOrganID.Updated Then db.AddInParameter(dbcmd, "@UpOrganID", DbType.String, OrganizationFlowRow.UpOrganID.Value)
            If OrganizationFlowRow.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, OrganizationFlowRow.DeptID.Value)
            If OrganizationFlowRow.GroupType.Updated Then db.AddInParameter(dbcmd, "@GroupType", DbType.String, OrganizationFlowRow.GroupType.Value)
            If OrganizationFlowRow.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, OrganizationFlowRow.GroupID.Value)
            If OrganizationFlowRow.Boss.Updated Then db.AddInParameter(dbcmd, "@Boss", DbType.String, OrganizationFlowRow.Boss.Value)
            If OrganizationFlowRow.BossCompID.Updated Then db.AddInParameter(dbcmd, "@BossCompID", DbType.String, OrganizationFlowRow.BossCompID.Value)
            If OrganizationFlowRow.BossType.Updated Then db.AddInParameter(dbcmd, "@BossType", DbType.String, OrganizationFlowRow.BossType.Value)
            If OrganizationFlowRow.BossTemporary.Updated Then db.AddInParameter(dbcmd, "@BossTemporary", DbType.String, OrganizationFlowRow.BossTemporary.Value)
            If OrganizationFlowRow.SecBoss.Updated Then db.AddInParameter(dbcmd, "@SecBoss", DbType.String, OrganizationFlowRow.SecBoss.Value)
            If OrganizationFlowRow.SecBossCompID.Updated Then db.AddInParameter(dbcmd, "@SecBossCompID", DbType.String, OrganizationFlowRow.SecBossCompID.Value)
            If OrganizationFlowRow.InValidFlag.Updated Then db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, OrganizationFlowRow.InValidFlag.Value)
            If OrganizationFlowRow.ReportingEnd.Updated Then db.AddInParameter(dbcmd, "@ReportingEnd", DbType.String, OrganizationFlowRow.ReportingEnd.Value)
            If OrganizationFlowRow.Depth.Updated Then db.AddInParameter(dbcmd, "@Depth", DbType.String, OrganizationFlowRow.Depth.Value)
            If OrganizationFlowRow.BranchFlag.Updated Then db.AddInParameter(dbcmd, "@BranchFlag", DbType.String, OrganizationFlowRow.BranchFlag.Value)
            If OrganizationFlowRow.DelegateFlag.Updated Then db.AddInParameter(dbcmd, "@DelegateFlag", DbType.String, OrganizationFlowRow.DelegateFlag.Value)
            If OrganizationFlowRow.DeptID1.Updated Then db.AddInParameter(dbcmd, "@DeptID1", DbType.String, OrganizationFlowRow.DeptID1.Value)
            If OrganizationFlowRow.StopDeptID.Updated Then db.AddInParameter(dbcmd, "@StopDeptID", DbType.String, OrganizationFlowRow.StopDeptID.Value)
            If OrganizationFlowRow.CompareFlag.Updated Then db.AddInParameter(dbcmd, "@CompareFlag", DbType.String, OrganizationFlowRow.CompareFlag.Value)
            If OrganizationFlowRow.FlowOrganID.Updated Then db.AddInParameter(dbcmd, "@FlowOrganID", DbType.String, OrganizationFlowRow.FlowOrganID.Value)
            If OrganizationFlowRow.RoleCode.Updated Then db.AddInParameter(dbcmd, "@RoleCode", DbType.String, OrganizationFlowRow.RoleCode.Value)
            If OrganizationFlowRow.BusinessType.Updated Then db.AddInParameter(dbcmd, "@BusinessType", DbType.String, OrganizationFlowRow.BusinessType.Value)
            If OrganizationFlowRow.OrganNo.Updated Then db.AddInParameter(dbcmd, "@OrganNo", DbType.String, OrganizationFlowRow.OrganNo.Value)
            If OrganizationFlowRow.OrgType.Updated Then db.AddInParameter(dbcmd, "@OrgType", DbType.String, OrganizationFlowRow.OrgType.Value)
            If OrganizationFlowRow.ValidDateB.Updated Then db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(OrganizationFlowRow.ValidDateB.Value), DBNull.Value, OrganizationFlowRow.ValidDateB.Value))
            If OrganizationFlowRow.ValidDateE.Updated Then db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(OrganizationFlowRow.ValidDateE.Value), DBNull.Value, OrganizationFlowRow.ValidDateE.Value))
            If OrganizationFlowRow.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, OrganizationFlowRow.Remark.Value)
            If OrganizationFlowRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, OrganizationFlowRow.LastChgComp.Value)
            If OrganizationFlowRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, OrganizationFlowRow.LastChgID.Value)
            If OrganizationFlowRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(OrganizationFlowRow.LastChgDate.Value), DBNull.Value, OrganizationFlowRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKOrganID", DbType.String, IIf(OrganizationFlowRow.LoadFromDataRow, OrganizationFlowRow.OrganID.OldValue, OrganizationFlowRow.OrganID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal OrganizationFlowRow As Row()) As Integer
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
                    For Each r As Row In OrganizationFlowRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update OrganizationFlow Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where OrganID = @PKOrganID")

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
                        If r.InValidFlag.Updated Then db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, r.InValidFlag.Value)
                        If r.ReportingEnd.Updated Then db.AddInParameter(dbcmd, "@ReportingEnd", DbType.String, r.ReportingEnd.Value)
                        If r.Depth.Updated Then db.AddInParameter(dbcmd, "@Depth", DbType.String, r.Depth.Value)
                        If r.BranchFlag.Updated Then db.AddInParameter(dbcmd, "@BranchFlag", DbType.String, r.BranchFlag.Value)
                        If r.DelegateFlag.Updated Then db.AddInParameter(dbcmd, "@DelegateFlag", DbType.String, r.DelegateFlag.Value)
                        If r.DeptID1.Updated Then db.AddInParameter(dbcmd, "@DeptID1", DbType.String, r.DeptID1.Value)
                        If r.StopDeptID.Updated Then db.AddInParameter(dbcmd, "@StopDeptID", DbType.String, r.StopDeptID.Value)
                        If r.CompareFlag.Updated Then db.AddInParameter(dbcmd, "@CompareFlag", DbType.String, r.CompareFlag.Value)
                        If r.FlowOrganID.Updated Then db.AddInParameter(dbcmd, "@FlowOrganID", DbType.String, r.FlowOrganID.Value)
                        If r.RoleCode.Updated Then db.AddInParameter(dbcmd, "@RoleCode", DbType.String, r.RoleCode.Value)
                        If r.BusinessType.Updated Then db.AddInParameter(dbcmd, "@BusinessType", DbType.String, r.BusinessType.Value)
                        If r.OrganNo.Updated Then db.AddInParameter(dbcmd, "@OrganNo", DbType.String, r.OrganNo.Value)
                        If r.OrgType.Updated Then db.AddInParameter(dbcmd, "@OrgType", DbType.String, r.OrgType.Value)
                        If r.ValidDateB.Updated Then db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(r.ValidDateB.Value), DBNull.Value, r.ValidDateB.Value))
                        If r.ValidDateE.Updated Then db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(r.ValidDateE.Value), DBNull.Value, r.ValidDateE.Value))
                        If r.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), DBNull.Value, r.LastChgDate.Value))
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

        Public Function Update(ByVal OrganizationFlowRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In OrganizationFlowRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update OrganizationFlow Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where OrganID = @PKOrganID")

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
                If r.InValidFlag.Updated Then db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, r.InValidFlag.Value)
                If r.ReportingEnd.Updated Then db.AddInParameter(dbcmd, "@ReportingEnd", DbType.String, r.ReportingEnd.Value)
                If r.Depth.Updated Then db.AddInParameter(dbcmd, "@Depth", DbType.String, r.Depth.Value)
                If r.BranchFlag.Updated Then db.AddInParameter(dbcmd, "@BranchFlag", DbType.String, r.BranchFlag.Value)
                If r.DelegateFlag.Updated Then db.AddInParameter(dbcmd, "@DelegateFlag", DbType.String, r.DelegateFlag.Value)
                If r.DeptID1.Updated Then db.AddInParameter(dbcmd, "@DeptID1", DbType.String, r.DeptID1.Value)
                If r.StopDeptID.Updated Then db.AddInParameter(dbcmd, "@StopDeptID", DbType.String, r.StopDeptID.Value)
                If r.CompareFlag.Updated Then db.AddInParameter(dbcmd, "@CompareFlag", DbType.String, r.CompareFlag.Value)
                If r.FlowOrganID.Updated Then db.AddInParameter(dbcmd, "@FlowOrganID", DbType.String, r.FlowOrganID.Value)
                If r.RoleCode.Updated Then db.AddInParameter(dbcmd, "@RoleCode", DbType.String, r.RoleCode.Value)
                If r.BusinessType.Updated Then db.AddInParameter(dbcmd, "@BusinessType", DbType.String, r.BusinessType.Value)
                If r.OrganNo.Updated Then db.AddInParameter(dbcmd, "@OrganNo", DbType.String, r.OrganNo.Value)
                If r.OrgType.Updated Then db.AddInParameter(dbcmd, "@OrgType", DbType.String, r.OrgType.Value)
                If r.ValidDateB.Updated Then db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(r.ValidDateB.Value), DBNull.Value, r.ValidDateB.Value))
                If r.ValidDateE.Updated Then db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(r.ValidDateE.Value), DBNull.Value, r.ValidDateE.Value))
                If r.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), DBNull.Value, r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKOrganID", DbType.String, IIf(r.LoadFromDataRow, r.OrganID.OldValue, r.OrganID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal OrganizationFlowRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From OrganizationFlow")
            strSQL.AppendLine("Where OrganID = @OrganID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationFlowRow.OrganID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal OrganizationFlowRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From OrganizationFlow")
            strSQL.AppendLine("Where OrganID = @OrganID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationFlowRow.OrganID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From OrganizationFlow")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal OrganizationFlowRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into OrganizationFlow")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, OrganID, OrganName, OrganEngName, VirtualFlag, UpOrganID, DeptID, GroupType,")
            strSQL.AppendLine("    GroupID, Boss, BossCompID, BossType, BossTemporary, SecBoss, SecBossCompID, InValidFlag,")
            strSQL.AppendLine("    ReportingEnd, Depth, BranchFlag, DelegateFlag, DeptID1, StopDeptID, CompareFlag, FlowOrganID,")
            strSQL.AppendLine("    RoleCode, BusinessType, OrganNo, OrgType, ValidDateB, ValidDateE, Remark, LastChgComp,")
            strSQL.AppendLine("    LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @OrganID, @OrganName, @OrganEngName, @VirtualFlag, @UpOrganID, @DeptID, @GroupType,")
            strSQL.AppendLine("    @GroupID, @Boss, @BossCompID, @BossType, @BossTemporary, @SecBoss, @SecBossCompID, @InValidFlag,")
            strSQL.AppendLine("    @ReportingEnd, @Depth, @BranchFlag, @DelegateFlag, @DeptID1, @StopDeptID, @CompareFlag, @FlowOrganID,")
            strSQL.AppendLine("    @RoleCode, @BusinessType, @OrganNo, @OrgType, @ValidDateB, @ValidDateE, @Remark, @LastChgComp,")
            strSQL.AppendLine("    @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, OrganizationFlowRow.CompID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationFlowRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@OrganName", DbType.String, OrganizationFlowRow.OrganName.Value)
            db.AddInParameter(dbcmd, "@OrganEngName", DbType.String, OrganizationFlowRow.OrganEngName.Value)
            db.AddInParameter(dbcmd, "@VirtualFlag", DbType.String, OrganizationFlowRow.VirtualFlag.Value)
            db.AddInParameter(dbcmd, "@UpOrganID", DbType.String, OrganizationFlowRow.UpOrganID.Value)
            db.AddInParameter(dbcmd, "@DeptID", DbType.String, OrganizationFlowRow.DeptID.Value)
            db.AddInParameter(dbcmd, "@GroupType", DbType.String, OrganizationFlowRow.GroupType.Value)
            db.AddInParameter(dbcmd, "@GroupID", DbType.String, OrganizationFlowRow.GroupID.Value)
            db.AddInParameter(dbcmd, "@Boss", DbType.String, OrganizationFlowRow.Boss.Value)
            db.AddInParameter(dbcmd, "@BossCompID", DbType.String, OrganizationFlowRow.BossCompID.Value)
            db.AddInParameter(dbcmd, "@BossType", DbType.String, OrganizationFlowRow.BossType.Value)
            db.AddInParameter(dbcmd, "@BossTemporary", DbType.String, OrganizationFlowRow.BossTemporary.Value)
            db.AddInParameter(dbcmd, "@SecBoss", DbType.String, OrganizationFlowRow.SecBoss.Value)
            db.AddInParameter(dbcmd, "@SecBossCompID", DbType.String, OrganizationFlowRow.SecBossCompID.Value)
            db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, OrganizationFlowRow.InValidFlag.Value)
            db.AddInParameter(dbcmd, "@ReportingEnd", DbType.String, OrganizationFlowRow.ReportingEnd.Value)
            db.AddInParameter(dbcmd, "@Depth", DbType.String, OrganizationFlowRow.Depth.Value)
            db.AddInParameter(dbcmd, "@BranchFlag", DbType.String, OrganizationFlowRow.BranchFlag.Value)
            db.AddInParameter(dbcmd, "@DelegateFlag", DbType.String, OrganizationFlowRow.DelegateFlag.Value)
            db.AddInParameter(dbcmd, "@DeptID1", DbType.String, OrganizationFlowRow.DeptID1.Value)
            db.AddInParameter(dbcmd, "@StopDeptID", DbType.String, OrganizationFlowRow.StopDeptID.Value)
            db.AddInParameter(dbcmd, "@CompareFlag", DbType.String, OrganizationFlowRow.CompareFlag.Value)
            db.AddInParameter(dbcmd, "@FlowOrganID", DbType.String, OrganizationFlowRow.FlowOrganID.Value)
            db.AddInParameter(dbcmd, "@RoleCode", DbType.String, OrganizationFlowRow.RoleCode.Value)
            db.AddInParameter(dbcmd, "@BusinessType", DbType.String, OrganizationFlowRow.BusinessType.Value)
            db.AddInParameter(dbcmd, "@OrganNo", DbType.String, OrganizationFlowRow.OrganNo.Value)
            db.AddInParameter(dbcmd, "@OrgType", DbType.String, OrganizationFlowRow.OrgType.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(OrganizationFlowRow.ValidDateB.Value), DBNull.Value, OrganizationFlowRow.ValidDateB.Value))
            db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(OrganizationFlowRow.ValidDateE.Value), DBNull.Value, OrganizationFlowRow.ValidDateE.Value))
            db.AddInParameter(dbcmd, "@Remark", DbType.String, OrganizationFlowRow.Remark.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, OrganizationFlowRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, OrganizationFlowRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(OrganizationFlowRow.LastChgDate.Value), DBNull.Value, OrganizationFlowRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal OrganizationFlowRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into OrganizationFlow")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, OrganID, OrganName, OrganEngName, VirtualFlag, UpOrganID, DeptID, GroupType,")
            strSQL.AppendLine("    GroupID, Boss, BossCompID, BossType, BossTemporary, SecBoss, SecBossCompID, InValidFlag,")
            strSQL.AppendLine("    ReportingEnd, Depth, BranchFlag, DelegateFlag, DeptID1, StopDeptID, CompareFlag, FlowOrganID,")
            strSQL.AppendLine("    RoleCode, BusinessType, OrganNo, OrgType, ValidDateB, ValidDateE, Remark, LastChgComp,")
            strSQL.AppendLine("    LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @OrganID, @OrganName, @OrganEngName, @VirtualFlag, @UpOrganID, @DeptID, @GroupType,")
            strSQL.AppendLine("    @GroupID, @Boss, @BossCompID, @BossType, @BossTemporary, @SecBoss, @SecBossCompID, @InValidFlag,")
            strSQL.AppendLine("    @ReportingEnd, @Depth, @BranchFlag, @DelegateFlag, @DeptID1, @StopDeptID, @CompareFlag, @FlowOrganID,")
            strSQL.AppendLine("    @RoleCode, @BusinessType, @OrganNo, @OrgType, @ValidDateB, @ValidDateE, @Remark, @LastChgComp,")
            strSQL.AppendLine("    @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, OrganizationFlowRow.CompID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationFlowRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@OrganName", DbType.String, OrganizationFlowRow.OrganName.Value)
            db.AddInParameter(dbcmd, "@OrganEngName", DbType.String, OrganizationFlowRow.OrganEngName.Value)
            db.AddInParameter(dbcmd, "@VirtualFlag", DbType.String, OrganizationFlowRow.VirtualFlag.Value)
            db.AddInParameter(dbcmd, "@UpOrganID", DbType.String, OrganizationFlowRow.UpOrganID.Value)
            db.AddInParameter(dbcmd, "@DeptID", DbType.String, OrganizationFlowRow.DeptID.Value)
            db.AddInParameter(dbcmd, "@GroupType", DbType.String, OrganizationFlowRow.GroupType.Value)
            db.AddInParameter(dbcmd, "@GroupID", DbType.String, OrganizationFlowRow.GroupID.Value)
            db.AddInParameter(dbcmd, "@Boss", DbType.String, OrganizationFlowRow.Boss.Value)
            db.AddInParameter(dbcmd, "@BossCompID", DbType.String, OrganizationFlowRow.BossCompID.Value)
            db.AddInParameter(dbcmd, "@BossType", DbType.String, OrganizationFlowRow.BossType.Value)
            db.AddInParameter(dbcmd, "@BossTemporary", DbType.String, OrganizationFlowRow.BossTemporary.Value)
            db.AddInParameter(dbcmd, "@SecBoss", DbType.String, OrganizationFlowRow.SecBoss.Value)
            db.AddInParameter(dbcmd, "@SecBossCompID", DbType.String, OrganizationFlowRow.SecBossCompID.Value)
            db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, OrganizationFlowRow.InValidFlag.Value)
            db.AddInParameter(dbcmd, "@ReportingEnd", DbType.String, OrganizationFlowRow.ReportingEnd.Value)
            db.AddInParameter(dbcmd, "@Depth", DbType.String, OrganizationFlowRow.Depth.Value)
            db.AddInParameter(dbcmd, "@BranchFlag", DbType.String, OrganizationFlowRow.BranchFlag.Value)
            db.AddInParameter(dbcmd, "@DelegateFlag", DbType.String, OrganizationFlowRow.DelegateFlag.Value)
            db.AddInParameter(dbcmd, "@DeptID1", DbType.String, OrganizationFlowRow.DeptID1.Value)
            db.AddInParameter(dbcmd, "@StopDeptID", DbType.String, OrganizationFlowRow.StopDeptID.Value)
            db.AddInParameter(dbcmd, "@CompareFlag", DbType.String, OrganizationFlowRow.CompareFlag.Value)
            db.AddInParameter(dbcmd, "@FlowOrganID", DbType.String, OrganizationFlowRow.FlowOrganID.Value)
            db.AddInParameter(dbcmd, "@RoleCode", DbType.String, OrganizationFlowRow.RoleCode.Value)
            db.AddInParameter(dbcmd, "@BusinessType", DbType.String, OrganizationFlowRow.BusinessType.Value)
            db.AddInParameter(dbcmd, "@OrganNo", DbType.String, OrganizationFlowRow.OrganNo.Value)
            db.AddInParameter(dbcmd, "@OrgType", DbType.String, OrganizationFlowRow.OrgType.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(OrganizationFlowRow.ValidDateB.Value), DBNull.Value, OrganizationFlowRow.ValidDateB.Value))
            db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(OrganizationFlowRow.ValidDateE.Value), DBNull.Value, OrganizationFlowRow.ValidDateE.Value))
            db.AddInParameter(dbcmd, "@Remark", DbType.String, OrganizationFlowRow.Remark.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, OrganizationFlowRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, OrganizationFlowRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(OrganizationFlowRow.LastChgDate.Value), DBNull.Value, OrganizationFlowRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal OrganizationFlowRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into OrganizationFlow")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, OrganID, OrganName, OrganEngName, VirtualFlag, UpOrganID, DeptID, GroupType,")
            strSQL.AppendLine("    GroupID, Boss, BossCompID, BossType, BossTemporary, SecBoss, SecBossCompID, InValidFlag,")
            strSQL.AppendLine("    ReportingEnd, Depth, BranchFlag, DelegateFlag, DeptID1, StopDeptID, CompareFlag, FlowOrganID,")
            strSQL.AppendLine("    RoleCode, BusinessType, OrganNo, OrgType, ValidDateB, ValidDateE, Remark, LastChgComp,")
            strSQL.AppendLine("    LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @OrganID, @OrganName, @OrganEngName, @VirtualFlag, @UpOrganID, @DeptID, @GroupType,")
            strSQL.AppendLine("    @GroupID, @Boss, @BossCompID, @BossType, @BossTemporary, @SecBoss, @SecBossCompID, @InValidFlag,")
            strSQL.AppendLine("    @ReportingEnd, @Depth, @BranchFlag, @DelegateFlag, @DeptID1, @StopDeptID, @CompareFlag, @FlowOrganID,")
            strSQL.AppendLine("    @RoleCode, @BusinessType, @OrganNo, @OrgType, @ValidDateB, @ValidDateE, @Remark, @LastChgComp,")
            strSQL.AppendLine("    @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In OrganizationFlowRow
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
                        db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, r.InValidFlag.Value)
                        db.AddInParameter(dbcmd, "@ReportingEnd", DbType.String, r.ReportingEnd.Value)
                        db.AddInParameter(dbcmd, "@Depth", DbType.String, r.Depth.Value)
                        db.AddInParameter(dbcmd, "@BranchFlag", DbType.String, r.BranchFlag.Value)
                        db.AddInParameter(dbcmd, "@DelegateFlag", DbType.String, r.DelegateFlag.Value)
                        db.AddInParameter(dbcmd, "@DeptID1", DbType.String, r.DeptID1.Value)
                        db.AddInParameter(dbcmd, "@StopDeptID", DbType.String, r.StopDeptID.Value)
                        db.AddInParameter(dbcmd, "@CompareFlag", DbType.String, r.CompareFlag.Value)
                        db.AddInParameter(dbcmd, "@FlowOrganID", DbType.String, r.FlowOrganID.Value)
                        db.AddInParameter(dbcmd, "@RoleCode", DbType.String, r.RoleCode.Value)
                        db.AddInParameter(dbcmd, "@BusinessType", DbType.String, r.BusinessType.Value)
                        db.AddInParameter(dbcmd, "@OrganNo", DbType.String, r.OrganNo.Value)
                        db.AddInParameter(dbcmd, "@OrgType", DbType.String, r.OrgType.Value)
                        db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(r.ValidDateB.Value), DBNull.Value, r.ValidDateB.Value))
                        db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(r.ValidDateE.Value), DBNull.Value, r.ValidDateE.Value))
                        db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                        db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), DBNull.Value, r.LastChgDate.Value))

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

        Public Function Insert(ByVal OrganizationFlowRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into OrganizationFlow")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, OrganID, OrganName, OrganEngName, VirtualFlag, UpOrganID, DeptID, GroupType,")
            strSQL.AppendLine("    GroupID, Boss, BossCompID, BossType, BossTemporary, SecBoss, SecBossCompID, InValidFlag,")
            strSQL.AppendLine("    ReportingEnd, Depth, BranchFlag, DelegateFlag, DeptID1, StopDeptID, CompareFlag, FlowOrganID,")
            strSQL.AppendLine("    RoleCode, BusinessType, OrganNo, OrgType, ValidDateB, ValidDateE, Remark, LastChgComp,")
            strSQL.AppendLine("    LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @OrganID, @OrganName, @OrganEngName, @VirtualFlag, @UpOrganID, @DeptID, @GroupType,")
            strSQL.AppendLine("    @GroupID, @Boss, @BossCompID, @BossType, @BossTemporary, @SecBoss, @SecBossCompID, @InValidFlag,")
            strSQL.AppendLine("    @ReportingEnd, @Depth, @BranchFlag, @DelegateFlag, @DeptID1, @StopDeptID, @CompareFlag, @FlowOrganID,")
            strSQL.AppendLine("    @RoleCode, @BusinessType, @OrganNo, @OrgType, @ValidDateB, @ValidDateE, @Remark, @LastChgComp,")
            strSQL.AppendLine("    @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In OrganizationFlowRow
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
                db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, r.InValidFlag.Value)
                db.AddInParameter(dbcmd, "@ReportingEnd", DbType.String, r.ReportingEnd.Value)
                db.AddInParameter(dbcmd, "@Depth", DbType.String, r.Depth.Value)
                db.AddInParameter(dbcmd, "@BranchFlag", DbType.String, r.BranchFlag.Value)
                db.AddInParameter(dbcmd, "@DelegateFlag", DbType.String, r.DelegateFlag.Value)
                db.AddInParameter(dbcmd, "@DeptID1", DbType.String, r.DeptID1.Value)
                db.AddInParameter(dbcmd, "@StopDeptID", DbType.String, r.StopDeptID.Value)
                db.AddInParameter(dbcmd, "@CompareFlag", DbType.String, r.CompareFlag.Value)
                db.AddInParameter(dbcmd, "@FlowOrganID", DbType.String, r.FlowOrganID.Value)
                db.AddInParameter(dbcmd, "@RoleCode", DbType.String, r.RoleCode.Value)
                db.AddInParameter(dbcmd, "@BusinessType", DbType.String, r.BusinessType.Value)
                db.AddInParameter(dbcmd, "@OrganNo", DbType.String, r.OrganNo.Value)
                db.AddInParameter(dbcmd, "@OrgType", DbType.String, r.OrgType.Value)
                db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(r.ValidDateB.Value), DBNull.Value, r.ValidDateB.Value))
                db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(r.ValidDateE.Value), DBNull.Value, r.ValidDateE.Value))
                db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), DBNull.Value, r.LastChgDate.Value))

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

