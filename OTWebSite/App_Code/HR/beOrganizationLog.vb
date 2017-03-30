'****************************************************************
' Table:OrganizationLog
' Created Date: 2016.11.08
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beOrganizationLog
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "OrganReason", "OrganType", "ValidDateB", "Seq", "OrganID", "OrganName", "OrganEngName", "VirtualFlag", "UpOrganID" _
                                    , "DeptID", "GroupType", "GroupID", "Boss", "BossCompID", "BossType", "BossTemporary", "SecBoss", "SecBossCompID", "InValidFlag", "ReportingEnd" _
                                    , "Depth", "BranchFlag", "RoleCode", "PersonPart", "SecPersonPart", "CheckPart", "WorkSiteID", "WorkTypeID", "CostDeptID", "CostType", "AccountBranch" _
                                    , "PositionID", "CompareFlag", "FlowOrganID", "DelegateFlag", "OrganNo", "InvoiceNo", "SortOrder", "OrgType", "BusinessType", "ValidDateE", "Remark" _
                                    , "OrganNameOld", "OrganEngNameOld", "VirtualFlagOld", "UpOrganIDOld", "DeptIDOld", "GroupTypeOld", "GroupIDOld", "BossOld", "BossCompIDOld", "BossTypeOld", "BossTemporaryOld" _
                                    , "SecBossOld", "SecBossCompIDOld", "InValidFlagOld", "ReportingEndOld", "DepthOld", "BranchFlagOld", "RoleCodeOld", "PersonPartOld", "SecPersonPartOld", "CheckPartOld", "WorkSiteIDOld" _
                                    , "WorkTypeIDOld", "CostDeptIDOld", "CostTypeOld", "AccountBranchOld", "PositionIDOld", "CompareFlagOld", "FlowOrganIDOld", "DelegateFlagOld", "OrganNoOld", "InvoiceNoOld", "SortOrderOld" _
                                    , "OrgTypeOld", "BusinessTypeOld", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(Date), GetType(Integer), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "CompID", "OrganID", "OrganReason", "OrganType", "ValidDateB", "Seq" }

        Public ReadOnly Property Rows() As beOrganizationLog.Rows 
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
        Public Sub Transfer2Row(OrganizationLogTable As DataTable)
            For Each dr As DataRow In OrganizationLogTable.Rows
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
                dr(m_Rows(i).ValidDateB.FieldName) = m_Rows(i).ValidDateB.Value
                dr(m_Rows(i).Seq.FieldName) = m_Rows(i).Seq.Value
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
                dr(m_Rows(i).ValidDateE.FieldName) = m_Rows(i).ValidDateE.Value
                dr(m_Rows(i).Remark.FieldName) = m_Rows(i).Remark.Value
                dr(m_Rows(i).OrganNameOld.FieldName) = m_Rows(i).OrganNameOld.Value
                dr(m_Rows(i).OrganEngNameOld.FieldName) = m_Rows(i).OrganEngNameOld.Value
                dr(m_Rows(i).VirtualFlagOld.FieldName) = m_Rows(i).VirtualFlagOld.Value
                dr(m_Rows(i).UpOrganIDOld.FieldName) = m_Rows(i).UpOrganIDOld.Value
                dr(m_Rows(i).DeptIDOld.FieldName) = m_Rows(i).DeptIDOld.Value
                dr(m_Rows(i).GroupTypeOld.FieldName) = m_Rows(i).GroupTypeOld.Value
                dr(m_Rows(i).GroupIDOld.FieldName) = m_Rows(i).GroupIDOld.Value
                dr(m_Rows(i).BossOld.FieldName) = m_Rows(i).BossOld.Value
                dr(m_Rows(i).BossCompIDOld.FieldName) = m_Rows(i).BossCompIDOld.Value
                dr(m_Rows(i).BossTypeOld.FieldName) = m_Rows(i).BossTypeOld.Value
                dr(m_Rows(i).BossTemporaryOld.FieldName) = m_Rows(i).BossTemporaryOld.Value
                dr(m_Rows(i).SecBossOld.FieldName) = m_Rows(i).SecBossOld.Value
                dr(m_Rows(i).SecBossCompIDOld.FieldName) = m_Rows(i).SecBossCompIDOld.Value
                dr(m_Rows(i).InValidFlagOld.FieldName) = m_Rows(i).InValidFlagOld.Value
                dr(m_Rows(i).ReportingEndOld.FieldName) = m_Rows(i).ReportingEndOld.Value
                dr(m_Rows(i).DepthOld.FieldName) = m_Rows(i).DepthOld.Value
                dr(m_Rows(i).BranchFlagOld.FieldName) = m_Rows(i).BranchFlagOld.Value
                dr(m_Rows(i).RoleCodeOld.FieldName) = m_Rows(i).RoleCodeOld.Value
                dr(m_Rows(i).PersonPartOld.FieldName) = m_Rows(i).PersonPartOld.Value
                dr(m_Rows(i).SecPersonPartOld.FieldName) = m_Rows(i).SecPersonPartOld.Value
                dr(m_Rows(i).CheckPartOld.FieldName) = m_Rows(i).CheckPartOld.Value
                dr(m_Rows(i).WorkSiteIDOld.FieldName) = m_Rows(i).WorkSiteIDOld.Value
                dr(m_Rows(i).WorkTypeIDOld.FieldName) = m_Rows(i).WorkTypeIDOld.Value
                dr(m_Rows(i).CostDeptIDOld.FieldName) = m_Rows(i).CostDeptIDOld.Value
                dr(m_Rows(i).CostTypeOld.FieldName) = m_Rows(i).CostTypeOld.Value
                dr(m_Rows(i).AccountBranchOld.FieldName) = m_Rows(i).AccountBranchOld.Value
                dr(m_Rows(i).PositionIDOld.FieldName) = m_Rows(i).PositionIDOld.Value
                dr(m_Rows(i).CompareFlagOld.FieldName) = m_Rows(i).CompareFlagOld.Value
                dr(m_Rows(i).FlowOrganIDOld.FieldName) = m_Rows(i).FlowOrganIDOld.Value
                dr(m_Rows(i).DelegateFlagOld.FieldName) = m_Rows(i).DelegateFlagOld.Value
                dr(m_Rows(i).OrganNoOld.FieldName) = m_Rows(i).OrganNoOld.Value
                dr(m_Rows(i).InvoiceNoOld.FieldName) = m_Rows(i).InvoiceNoOld.Value
                dr(m_Rows(i).SortOrderOld.FieldName) = m_Rows(i).SortOrderOld.Value
                dr(m_Rows(i).OrgTypeOld.FieldName) = m_Rows(i).OrgTypeOld.Value
                dr(m_Rows(i).BusinessTypeOld.FieldName) = m_Rows(i).BusinessTypeOld.Value
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

        Public Sub Add(OrganizationLogRow As Row)
            m_Rows.Add(OrganizationLogRow)
        End Sub

        Public Sub Remove(OrganizationLogRow As Row)
            If m_Rows.IndexOf(OrganizationLogRow) >= 0 Then
                m_Rows.Remove(OrganizationLogRow)
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
        Private FI_ValidDateB As Field(Of Date) = new Field(Of Date)("ValidDateB", true)
        Private FI_Seq As Field(Of Integer) = new Field(Of Integer)("Seq", true)
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
        Private FI_ValidDateE As Field(Of Date) = new Field(Of Date)("ValidDateE", true)
        Private FI_Remark As Field(Of String) = new Field(Of String)("Remark", true)
        Private FI_OrganNameOld As Field(Of String) = new Field(Of String)("OrganNameOld", true)
        Private FI_OrganEngNameOld As Field(Of String) = new Field(Of String)("OrganEngNameOld", true)
        Private FI_VirtualFlagOld As Field(Of String) = new Field(Of String)("VirtualFlagOld", true)
        Private FI_UpOrganIDOld As Field(Of String) = new Field(Of String)("UpOrganIDOld", true)
        Private FI_DeptIDOld As Field(Of String) = new Field(Of String)("DeptIDOld", true)
        Private FI_GroupTypeOld As Field(Of String) = new Field(Of String)("GroupTypeOld", true)
        Private FI_GroupIDOld As Field(Of String) = new Field(Of String)("GroupIDOld", true)
        Private FI_BossOld As Field(Of String) = new Field(Of String)("BossOld", true)
        Private FI_BossCompIDOld As Field(Of String) = new Field(Of String)("BossCompIDOld", true)
        Private FI_BossTypeOld As Field(Of String) = new Field(Of String)("BossTypeOld", true)
        Private FI_BossTemporaryOld As Field(Of String) = new Field(Of String)("BossTemporaryOld", true)
        Private FI_SecBossOld As Field(Of String) = new Field(Of String)("SecBossOld", true)
        Private FI_SecBossCompIDOld As Field(Of String) = new Field(Of String)("SecBossCompIDOld", true)
        Private FI_InValidFlagOld As Field(Of String) = new Field(Of String)("InValidFlagOld", true)
        Private FI_ReportingEndOld As Field(Of String) = new Field(Of String)("ReportingEndOld", true)
        Private FI_DepthOld As Field(Of String) = new Field(Of String)("DepthOld", true)
        Private FI_BranchFlagOld As Field(Of String) = new Field(Of String)("BranchFlagOld", true)
        Private FI_RoleCodeOld As Field(Of String) = new Field(Of String)("RoleCodeOld", true)
        Private FI_PersonPartOld As Field(Of String) = new Field(Of String)("PersonPartOld", true)
        Private FI_SecPersonPartOld As Field(Of String) = new Field(Of String)("SecPersonPartOld", true)
        Private FI_CheckPartOld As Field(Of String) = new Field(Of String)("CheckPartOld", true)
        Private FI_WorkSiteIDOld As Field(Of String) = new Field(Of String)("WorkSiteIDOld", true)
        Private FI_WorkTypeIDOld As Field(Of String) = new Field(Of String)("WorkTypeIDOld", true)
        Private FI_CostDeptIDOld As Field(Of String) = new Field(Of String)("CostDeptIDOld", true)
        Private FI_CostTypeOld As Field(Of String) = new Field(Of String)("CostTypeOld", true)
        Private FI_AccountBranchOld As Field(Of String) = new Field(Of String)("AccountBranchOld", true)
        Private FI_PositionIDOld As Field(Of String) = new Field(Of String)("PositionIDOld", true)
        Private FI_CompareFlagOld As Field(Of String) = new Field(Of String)("CompareFlagOld", true)
        Private FI_FlowOrganIDOld As Field(Of String) = new Field(Of String)("FlowOrganIDOld", true)
        Private FI_DelegateFlagOld As Field(Of String) = new Field(Of String)("DelegateFlagOld", true)
        Private FI_OrganNoOld As Field(Of String) = new Field(Of String)("OrganNoOld", true)
        Private FI_InvoiceNoOld As Field(Of String) = new Field(Of String)("InvoiceNoOld", true)
        Private FI_SortOrderOld As Field(Of String) = new Field(Of String)("SortOrderOld", true)
        Private FI_OrgTypeOld As Field(Of String) = new Field(Of String)("OrgTypeOld", true)
        Private FI_BusinessTypeOld As Field(Of String) = new Field(Of String)("BusinessTypeOld", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "CompID", "OrganReason", "OrganType", "ValidDateB", "Seq", "OrganID", "OrganName", "OrganEngName", "VirtualFlag", "UpOrganID" _
                                    , "DeptID", "GroupType", "GroupID", "Boss", "BossCompID", "BossType", "BossTemporary", "SecBoss", "SecBossCompID", "InValidFlag", "ReportingEnd" _
                                    , "Depth", "BranchFlag", "RoleCode", "PersonPart", "SecPersonPart", "CheckPart", "WorkSiteID", "WorkTypeID", "CostDeptID", "CostType", "AccountBranch" _
                                    , "PositionID", "CompareFlag", "FlowOrganID", "DelegateFlag", "OrganNo", "InvoiceNo", "SortOrder", "OrgType", "BusinessType", "ValidDateE", "Remark" _
                                    , "OrganNameOld", "OrganEngNameOld", "VirtualFlagOld", "UpOrganIDOld", "DeptIDOld", "GroupTypeOld", "GroupIDOld", "BossOld", "BossCompIDOld", "BossTypeOld", "BossTemporaryOld" _
                                    , "SecBossOld", "SecBossCompIDOld", "InValidFlagOld", "ReportingEndOld", "DepthOld", "BranchFlagOld", "RoleCodeOld", "PersonPartOld", "SecPersonPartOld", "CheckPartOld", "WorkSiteIDOld" _
                                    , "WorkTypeIDOld", "CostDeptIDOld", "CostTypeOld", "AccountBranchOld", "PositionIDOld", "CompareFlagOld", "FlowOrganIDOld", "DelegateFlagOld", "OrganNoOld", "InvoiceNoOld", "SortOrderOld" _
                                    , "OrgTypeOld", "BusinessTypeOld", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "CompID", "OrganID", "OrganReason", "OrganType", "ValidDateB", "Seq" }
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
                Case "ValidDateB"
                    Return FI_ValidDateB.Value
                Case "Seq"
                    Return FI_Seq.Value
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
                Case "ValidDateE"
                    Return FI_ValidDateE.Value
                Case "Remark"
                    Return FI_Remark.Value
                Case "OrganNameOld"
                    Return FI_OrganNameOld.Value
                Case "OrganEngNameOld"
                    Return FI_OrganEngNameOld.Value
                Case "VirtualFlagOld"
                    Return FI_VirtualFlagOld.Value
                Case "UpOrganIDOld"
                    Return FI_UpOrganIDOld.Value
                Case "DeptIDOld"
                    Return FI_DeptIDOld.Value
                Case "GroupTypeOld"
                    Return FI_GroupTypeOld.Value
                Case "GroupIDOld"
                    Return FI_GroupIDOld.Value
                Case "BossOld"
                    Return FI_BossOld.Value
                Case "BossCompIDOld"
                    Return FI_BossCompIDOld.Value
                Case "BossTypeOld"
                    Return FI_BossTypeOld.Value
                Case "BossTemporaryOld"
                    Return FI_BossTemporaryOld.Value
                Case "SecBossOld"
                    Return FI_SecBossOld.Value
                Case "SecBossCompIDOld"
                    Return FI_SecBossCompIDOld.Value
                Case "InValidFlagOld"
                    Return FI_InValidFlagOld.Value
                Case "ReportingEndOld"
                    Return FI_ReportingEndOld.Value
                Case "DepthOld"
                    Return FI_DepthOld.Value
                Case "BranchFlagOld"
                    Return FI_BranchFlagOld.Value
                Case "RoleCodeOld"
                    Return FI_RoleCodeOld.Value
                Case "PersonPartOld"
                    Return FI_PersonPartOld.Value
                Case "SecPersonPartOld"
                    Return FI_SecPersonPartOld.Value
                Case "CheckPartOld"
                    Return FI_CheckPartOld.Value
                Case "WorkSiteIDOld"
                    Return FI_WorkSiteIDOld.Value
                Case "WorkTypeIDOld"
                    Return FI_WorkTypeIDOld.Value
                Case "CostDeptIDOld"
                    Return FI_CostDeptIDOld.Value
                Case "CostTypeOld"
                    Return FI_CostTypeOld.Value
                Case "AccountBranchOld"
                    Return FI_AccountBranchOld.Value
                Case "PositionIDOld"
                    Return FI_PositionIDOld.Value
                Case "CompareFlagOld"
                    Return FI_CompareFlagOld.Value
                Case "FlowOrganIDOld"
                    Return FI_FlowOrganIDOld.Value
                Case "DelegateFlagOld"
                    Return FI_DelegateFlagOld.Value
                Case "OrganNoOld"
                    Return FI_OrganNoOld.Value
                Case "InvoiceNoOld"
                    Return FI_InvoiceNoOld.Value
                Case "SortOrderOld"
                    Return FI_SortOrderOld.Value
                Case "OrgTypeOld"
                    Return FI_OrgTypeOld.Value
                Case "BusinessTypeOld"
                    Return FI_BusinessTypeOld.Value
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
                Case "ValidDateB"
                    FI_ValidDateB.SetValue(value)
                Case "Seq"
                    FI_Seq.SetValue(value)
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
                Case "ValidDateE"
                    FI_ValidDateE.SetValue(value)
                Case "Remark"
                    FI_Remark.SetValue(value)
                Case "OrganNameOld"
                    FI_OrganNameOld.SetValue(value)
                Case "OrganEngNameOld"
                    FI_OrganEngNameOld.SetValue(value)
                Case "VirtualFlagOld"
                    FI_VirtualFlagOld.SetValue(value)
                Case "UpOrganIDOld"
                    FI_UpOrganIDOld.SetValue(value)
                Case "DeptIDOld"
                    FI_DeptIDOld.SetValue(value)
                Case "GroupTypeOld"
                    FI_GroupTypeOld.SetValue(value)
                Case "GroupIDOld"
                    FI_GroupIDOld.SetValue(value)
                Case "BossOld"
                    FI_BossOld.SetValue(value)
                Case "BossCompIDOld"
                    FI_BossCompIDOld.SetValue(value)
                Case "BossTypeOld"
                    FI_BossTypeOld.SetValue(value)
                Case "BossTemporaryOld"
                    FI_BossTemporaryOld.SetValue(value)
                Case "SecBossOld"
                    FI_SecBossOld.SetValue(value)
                Case "SecBossCompIDOld"
                    FI_SecBossCompIDOld.SetValue(value)
                Case "InValidFlagOld"
                    FI_InValidFlagOld.SetValue(value)
                Case "ReportingEndOld"
                    FI_ReportingEndOld.SetValue(value)
                Case "DepthOld"
                    FI_DepthOld.SetValue(value)
                Case "BranchFlagOld"
                    FI_BranchFlagOld.SetValue(value)
                Case "RoleCodeOld"
                    FI_RoleCodeOld.SetValue(value)
                Case "PersonPartOld"
                    FI_PersonPartOld.SetValue(value)
                Case "SecPersonPartOld"
                    FI_SecPersonPartOld.SetValue(value)
                Case "CheckPartOld"
                    FI_CheckPartOld.SetValue(value)
                Case "WorkSiteIDOld"
                    FI_WorkSiteIDOld.SetValue(value)
                Case "WorkTypeIDOld"
                    FI_WorkTypeIDOld.SetValue(value)
                Case "CostDeptIDOld"
                    FI_CostDeptIDOld.SetValue(value)
                Case "CostTypeOld"
                    FI_CostTypeOld.SetValue(value)
                Case "AccountBranchOld"
                    FI_AccountBranchOld.SetValue(value)
                Case "PositionIDOld"
                    FI_PositionIDOld.SetValue(value)
                Case "CompareFlagOld"
                    FI_CompareFlagOld.SetValue(value)
                Case "FlowOrganIDOld"
                    FI_FlowOrganIDOld.SetValue(value)
                Case "DelegateFlagOld"
                    FI_DelegateFlagOld.SetValue(value)
                Case "OrganNoOld"
                    FI_OrganNoOld.SetValue(value)
                Case "InvoiceNoOld"
                    FI_InvoiceNoOld.SetValue(value)
                Case "SortOrderOld"
                    FI_SortOrderOld.SetValue(value)
                Case "OrgTypeOld"
                    FI_OrgTypeOld.SetValue(value)
                Case "BusinessTypeOld"
                    FI_BusinessTypeOld.SetValue(value)
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
                Case "ValidDateB"
                    return FI_ValidDateB.Updated
                Case "Seq"
                    return FI_Seq.Updated
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
                Case "ValidDateE"
                    return FI_ValidDateE.Updated
                Case "Remark"
                    return FI_Remark.Updated
                Case "OrganNameOld"
                    return FI_OrganNameOld.Updated
                Case "OrganEngNameOld"
                    return FI_OrganEngNameOld.Updated
                Case "VirtualFlagOld"
                    return FI_VirtualFlagOld.Updated
                Case "UpOrganIDOld"
                    return FI_UpOrganIDOld.Updated
                Case "DeptIDOld"
                    return FI_DeptIDOld.Updated
                Case "GroupTypeOld"
                    return FI_GroupTypeOld.Updated
                Case "GroupIDOld"
                    return FI_GroupIDOld.Updated
                Case "BossOld"
                    return FI_BossOld.Updated
                Case "BossCompIDOld"
                    return FI_BossCompIDOld.Updated
                Case "BossTypeOld"
                    return FI_BossTypeOld.Updated
                Case "BossTemporaryOld"
                    return FI_BossTemporaryOld.Updated
                Case "SecBossOld"
                    return FI_SecBossOld.Updated
                Case "SecBossCompIDOld"
                    return FI_SecBossCompIDOld.Updated
                Case "InValidFlagOld"
                    return FI_InValidFlagOld.Updated
                Case "ReportingEndOld"
                    return FI_ReportingEndOld.Updated
                Case "DepthOld"
                    return FI_DepthOld.Updated
                Case "BranchFlagOld"
                    return FI_BranchFlagOld.Updated
                Case "RoleCodeOld"
                    return FI_RoleCodeOld.Updated
                Case "PersonPartOld"
                    return FI_PersonPartOld.Updated
                Case "SecPersonPartOld"
                    return FI_SecPersonPartOld.Updated
                Case "CheckPartOld"
                    return FI_CheckPartOld.Updated
                Case "WorkSiteIDOld"
                    return FI_WorkSiteIDOld.Updated
                Case "WorkTypeIDOld"
                    return FI_WorkTypeIDOld.Updated
                Case "CostDeptIDOld"
                    return FI_CostDeptIDOld.Updated
                Case "CostTypeOld"
                    return FI_CostTypeOld.Updated
                Case "AccountBranchOld"
                    return FI_AccountBranchOld.Updated
                Case "PositionIDOld"
                    return FI_PositionIDOld.Updated
                Case "CompareFlagOld"
                    return FI_CompareFlagOld.Updated
                Case "FlowOrganIDOld"
                    return FI_FlowOrganIDOld.Updated
                Case "DelegateFlagOld"
                    return FI_DelegateFlagOld.Updated
                Case "OrganNoOld"
                    return FI_OrganNoOld.Updated
                Case "InvoiceNoOld"
                    return FI_InvoiceNoOld.Updated
                Case "SortOrderOld"
                    return FI_SortOrderOld.Updated
                Case "OrgTypeOld"
                    return FI_OrgTypeOld.Updated
                Case "BusinessTypeOld"
                    return FI_BusinessTypeOld.Updated
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
                Case "ValidDateB"
                    return FI_ValidDateB.CreateUpdateSQL
                Case "Seq"
                    return FI_Seq.CreateUpdateSQL
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
                Case "ValidDateE"
                    return FI_ValidDateE.CreateUpdateSQL
                Case "Remark"
                    return FI_Remark.CreateUpdateSQL
                Case "OrganNameOld"
                    return FI_OrganNameOld.CreateUpdateSQL
                Case "OrganEngNameOld"
                    return FI_OrganEngNameOld.CreateUpdateSQL
                Case "VirtualFlagOld"
                    return FI_VirtualFlagOld.CreateUpdateSQL
                Case "UpOrganIDOld"
                    return FI_UpOrganIDOld.CreateUpdateSQL
                Case "DeptIDOld"
                    return FI_DeptIDOld.CreateUpdateSQL
                Case "GroupTypeOld"
                    return FI_GroupTypeOld.CreateUpdateSQL
                Case "GroupIDOld"
                    return FI_GroupIDOld.CreateUpdateSQL
                Case "BossOld"
                    return FI_BossOld.CreateUpdateSQL
                Case "BossCompIDOld"
                    return FI_BossCompIDOld.CreateUpdateSQL
                Case "BossTypeOld"
                    return FI_BossTypeOld.CreateUpdateSQL
                Case "BossTemporaryOld"
                    return FI_BossTemporaryOld.CreateUpdateSQL
                Case "SecBossOld"
                    return FI_SecBossOld.CreateUpdateSQL
                Case "SecBossCompIDOld"
                    return FI_SecBossCompIDOld.CreateUpdateSQL
                Case "InValidFlagOld"
                    return FI_InValidFlagOld.CreateUpdateSQL
                Case "ReportingEndOld"
                    return FI_ReportingEndOld.CreateUpdateSQL
                Case "DepthOld"
                    return FI_DepthOld.CreateUpdateSQL
                Case "BranchFlagOld"
                    return FI_BranchFlagOld.CreateUpdateSQL
                Case "RoleCodeOld"
                    return FI_RoleCodeOld.CreateUpdateSQL
                Case "PersonPartOld"
                    return FI_PersonPartOld.CreateUpdateSQL
                Case "SecPersonPartOld"
                    return FI_SecPersonPartOld.CreateUpdateSQL
                Case "CheckPartOld"
                    return FI_CheckPartOld.CreateUpdateSQL
                Case "WorkSiteIDOld"
                    return FI_WorkSiteIDOld.CreateUpdateSQL
                Case "WorkTypeIDOld"
                    return FI_WorkTypeIDOld.CreateUpdateSQL
                Case "CostDeptIDOld"
                    return FI_CostDeptIDOld.CreateUpdateSQL
                Case "CostTypeOld"
                    return FI_CostTypeOld.CreateUpdateSQL
                Case "AccountBranchOld"
                    return FI_AccountBranchOld.CreateUpdateSQL
                Case "PositionIDOld"
                    return FI_PositionIDOld.CreateUpdateSQL
                Case "CompareFlagOld"
                    return FI_CompareFlagOld.CreateUpdateSQL
                Case "FlowOrganIDOld"
                    return FI_FlowOrganIDOld.CreateUpdateSQL
                Case "DelegateFlagOld"
                    return FI_DelegateFlagOld.CreateUpdateSQL
                Case "OrganNoOld"
                    return FI_OrganNoOld.CreateUpdateSQL
                Case "InvoiceNoOld"
                    return FI_InvoiceNoOld.CreateUpdateSQL
                Case "SortOrderOld"
                    return FI_SortOrderOld.CreateUpdateSQL
                Case "OrgTypeOld"
                    return FI_OrgTypeOld.CreateUpdateSQL
                Case "BusinessTypeOld"
                    return FI_BusinessTypeOld.CreateUpdateSQL
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
            FI_ValidDateB.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_Seq.SetInitValue(0)
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
            FI_ValidDateE.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_Remark.SetInitValue("")
            FI_OrganNameOld.SetInitValue("")
            FI_OrganEngNameOld.SetInitValue("")
            FI_VirtualFlagOld.SetInitValue("0")
            FI_UpOrganIDOld.SetInitValue("")
            FI_DeptIDOld.SetInitValue("")
            FI_GroupTypeOld.SetInitValue("")
            FI_GroupIDOld.SetInitValue("")
            FI_BossOld.SetInitValue("")
            FI_BossCompIDOld.SetInitValue("")
            FI_BossTypeOld.SetInitValue("")
            FI_BossTemporaryOld.SetInitValue("0")
            FI_SecBossOld.SetInitValue("")
            FI_SecBossCompIDOld.SetInitValue("")
            FI_InValidFlagOld.SetInitValue("0")
            FI_ReportingEndOld.SetInitValue("0")
            FI_DepthOld.SetInitValue("")
            FI_BranchFlagOld.SetInitValue("")
            FI_RoleCodeOld.SetInitValue("")
            FI_PersonPartOld.SetInitValue("")
            FI_SecPersonPartOld.SetInitValue("")
            FI_CheckPartOld.SetInitValue("")
            FI_WorkSiteIDOld.SetInitValue("")
            FI_WorkTypeIDOld.SetInitValue("")
            FI_CostDeptIDOld.SetInitValue("")
            FI_CostTypeOld.SetInitValue("")
            FI_AccountBranchOld.SetInitValue("")
            FI_PositionIDOld.SetInitValue("")
            FI_CompareFlagOld.SetInitValue("0")
            FI_FlowOrganIDOld.SetInitValue("")
            FI_DelegateFlagOld.SetInitValue("")
            FI_OrganNoOld.SetInitValue("0")
            FI_InvoiceNoOld.SetInitValue("")
            FI_SortOrderOld.SetInitValue("")
            FI_OrgTypeOld.SetInitValue("")
            FI_BusinessTypeOld.SetInitValue("")
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CompID.SetInitValue(dr("CompID"))
            FI_OrganReason.SetInitValue(dr("OrganReason"))
            FI_OrganType.SetInitValue(dr("OrganType"))
            FI_ValidDateB.SetInitValue(dr("ValidDateB"))
            FI_Seq.SetInitValue(dr("Seq"))
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
            FI_ValidDateE.SetInitValue(dr("ValidDateE"))
            FI_Remark.SetInitValue(dr("Remark"))
            FI_OrganNameOld.SetInitValue(dr("OrganNameOld"))
            FI_OrganEngNameOld.SetInitValue(dr("OrganEngNameOld"))
            FI_VirtualFlagOld.SetInitValue(dr("VirtualFlagOld"))
            FI_UpOrganIDOld.SetInitValue(dr("UpOrganIDOld"))
            FI_DeptIDOld.SetInitValue(dr("DeptIDOld"))
            FI_GroupTypeOld.SetInitValue(dr("GroupTypeOld"))
            FI_GroupIDOld.SetInitValue(dr("GroupIDOld"))
            FI_BossOld.SetInitValue(dr("BossOld"))
            FI_BossCompIDOld.SetInitValue(dr("BossCompIDOld"))
            FI_BossTypeOld.SetInitValue(dr("BossTypeOld"))
            FI_BossTemporaryOld.SetInitValue(dr("BossTemporaryOld"))
            FI_SecBossOld.SetInitValue(dr("SecBossOld"))
            FI_SecBossCompIDOld.SetInitValue(dr("SecBossCompIDOld"))
            FI_InValidFlagOld.SetInitValue(dr("InValidFlagOld"))
            FI_ReportingEndOld.SetInitValue(dr("ReportingEndOld"))
            FI_DepthOld.SetInitValue(dr("DepthOld"))
            FI_BranchFlagOld.SetInitValue(dr("BranchFlagOld"))
            FI_RoleCodeOld.SetInitValue(dr("RoleCodeOld"))
            FI_PersonPartOld.SetInitValue(dr("PersonPartOld"))
            FI_SecPersonPartOld.SetInitValue(dr("SecPersonPartOld"))
            FI_CheckPartOld.SetInitValue(dr("CheckPartOld"))
            FI_WorkSiteIDOld.SetInitValue(dr("WorkSiteIDOld"))
            FI_WorkTypeIDOld.SetInitValue(dr("WorkTypeIDOld"))
            FI_CostDeptIDOld.SetInitValue(dr("CostDeptIDOld"))
            FI_CostTypeOld.SetInitValue(dr("CostTypeOld"))
            FI_AccountBranchOld.SetInitValue(dr("AccountBranchOld"))
            FI_PositionIDOld.SetInitValue(dr("PositionIDOld"))
            FI_CompareFlagOld.SetInitValue(dr("CompareFlagOld"))
            FI_FlowOrganIDOld.SetInitValue(dr("FlowOrganIDOld"))
            FI_DelegateFlagOld.SetInitValue(dr("DelegateFlagOld"))
            FI_OrganNoOld.SetInitValue(dr("OrganNoOld"))
            FI_InvoiceNoOld.SetInitValue(dr("InvoiceNoOld"))
            FI_SortOrderOld.SetInitValue(dr("SortOrderOld"))
            FI_OrgTypeOld.SetInitValue(dr("OrgTypeOld"))
            FI_BusinessTypeOld.SetInitValue(dr("BusinessTypeOld"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CompID.Updated = False
            FI_OrganReason.Updated = False
            FI_OrganType.Updated = False
            FI_ValidDateB.Updated = False
            FI_Seq.Updated = False
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
            FI_ValidDateE.Updated = False
            FI_Remark.Updated = False
            FI_OrganNameOld.Updated = False
            FI_OrganEngNameOld.Updated = False
            FI_VirtualFlagOld.Updated = False
            FI_UpOrganIDOld.Updated = False
            FI_DeptIDOld.Updated = False
            FI_GroupTypeOld.Updated = False
            FI_GroupIDOld.Updated = False
            FI_BossOld.Updated = False
            FI_BossCompIDOld.Updated = False
            FI_BossTypeOld.Updated = False
            FI_BossTemporaryOld.Updated = False
            FI_SecBossOld.Updated = False
            FI_SecBossCompIDOld.Updated = False
            FI_InValidFlagOld.Updated = False
            FI_ReportingEndOld.Updated = False
            FI_DepthOld.Updated = False
            FI_BranchFlagOld.Updated = False
            FI_RoleCodeOld.Updated = False
            FI_PersonPartOld.Updated = False
            FI_SecPersonPartOld.Updated = False
            FI_CheckPartOld.Updated = False
            FI_WorkSiteIDOld.Updated = False
            FI_WorkTypeIDOld.Updated = False
            FI_CostDeptIDOld.Updated = False
            FI_CostTypeOld.Updated = False
            FI_AccountBranchOld.Updated = False
            FI_PositionIDOld.Updated = False
            FI_CompareFlagOld.Updated = False
            FI_FlowOrganIDOld.Updated = False
            FI_DelegateFlagOld.Updated = False
            FI_OrganNoOld.Updated = False
            FI_InvoiceNoOld.Updated = False
            FI_SortOrderOld.Updated = False
            FI_OrgTypeOld.Updated = False
            FI_BusinessTypeOld.Updated = False
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

        Public ReadOnly Property ValidDateB As Field(Of Date) 
            Get
                Return FI_ValidDateB
            End Get
        End Property

        Public ReadOnly Property Seq As Field(Of Integer) 
            Get
                Return FI_Seq
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

        Public ReadOnly Property OrganNameOld As Field(Of String) 
            Get
                Return FI_OrganNameOld
            End Get
        End Property

        Public ReadOnly Property OrganEngNameOld As Field(Of String) 
            Get
                Return FI_OrganEngNameOld
            End Get
        End Property

        Public ReadOnly Property VirtualFlagOld As Field(Of String) 
            Get
                Return FI_VirtualFlagOld
            End Get
        End Property

        Public ReadOnly Property UpOrganIDOld As Field(Of String) 
            Get
                Return FI_UpOrganIDOld
            End Get
        End Property

        Public ReadOnly Property DeptIDOld As Field(Of String) 
            Get
                Return FI_DeptIDOld
            End Get
        End Property

        Public ReadOnly Property GroupTypeOld As Field(Of String) 
            Get
                Return FI_GroupTypeOld
            End Get
        End Property

        Public ReadOnly Property GroupIDOld As Field(Of String) 
            Get
                Return FI_GroupIDOld
            End Get
        End Property

        Public ReadOnly Property BossOld As Field(Of String) 
            Get
                Return FI_BossOld
            End Get
        End Property

        Public ReadOnly Property BossCompIDOld As Field(Of String) 
            Get
                Return FI_BossCompIDOld
            End Get
        End Property

        Public ReadOnly Property BossTypeOld As Field(Of String) 
            Get
                Return FI_BossTypeOld
            End Get
        End Property

        Public ReadOnly Property BossTemporaryOld As Field(Of String) 
            Get
                Return FI_BossTemporaryOld
            End Get
        End Property

        Public ReadOnly Property SecBossOld As Field(Of String) 
            Get
                Return FI_SecBossOld
            End Get
        End Property

        Public ReadOnly Property SecBossCompIDOld As Field(Of String) 
            Get
                Return FI_SecBossCompIDOld
            End Get
        End Property

        Public ReadOnly Property InValidFlagOld As Field(Of String) 
            Get
                Return FI_InValidFlagOld
            End Get
        End Property

        Public ReadOnly Property ReportingEndOld As Field(Of String) 
            Get
                Return FI_ReportingEndOld
            End Get
        End Property

        Public ReadOnly Property DepthOld As Field(Of String) 
            Get
                Return FI_DepthOld
            End Get
        End Property

        Public ReadOnly Property BranchFlagOld As Field(Of String) 
            Get
                Return FI_BranchFlagOld
            End Get
        End Property

        Public ReadOnly Property RoleCodeOld As Field(Of String) 
            Get
                Return FI_RoleCodeOld
            End Get
        End Property

        Public ReadOnly Property PersonPartOld As Field(Of String) 
            Get
                Return FI_PersonPartOld
            End Get
        End Property

        Public ReadOnly Property SecPersonPartOld As Field(Of String) 
            Get
                Return FI_SecPersonPartOld
            End Get
        End Property

        Public ReadOnly Property CheckPartOld As Field(Of String) 
            Get
                Return FI_CheckPartOld
            End Get
        End Property

        Public ReadOnly Property WorkSiteIDOld As Field(Of String) 
            Get
                Return FI_WorkSiteIDOld
            End Get
        End Property

        Public ReadOnly Property WorkTypeIDOld As Field(Of String) 
            Get
                Return FI_WorkTypeIDOld
            End Get
        End Property

        Public ReadOnly Property CostDeptIDOld As Field(Of String) 
            Get
                Return FI_CostDeptIDOld
            End Get
        End Property

        Public ReadOnly Property CostTypeOld As Field(Of String) 
            Get
                Return FI_CostTypeOld
            End Get
        End Property

        Public ReadOnly Property AccountBranchOld As Field(Of String) 
            Get
                Return FI_AccountBranchOld
            End Get
        End Property

        Public ReadOnly Property PositionIDOld As Field(Of String) 
            Get
                Return FI_PositionIDOld
            End Get
        End Property

        Public ReadOnly Property CompareFlagOld As Field(Of String) 
            Get
                Return FI_CompareFlagOld
            End Get
        End Property

        Public ReadOnly Property FlowOrganIDOld As Field(Of String) 
            Get
                Return FI_FlowOrganIDOld
            End Get
        End Property

        Public ReadOnly Property DelegateFlagOld As Field(Of String) 
            Get
                Return FI_DelegateFlagOld
            End Get
        End Property

        Public ReadOnly Property OrganNoOld As Field(Of String) 
            Get
                Return FI_OrganNoOld
            End Get
        End Property

        Public ReadOnly Property InvoiceNoOld As Field(Of String) 
            Get
                Return FI_InvoiceNoOld
            End Get
        End Property

        Public ReadOnly Property SortOrderOld As Field(Of String) 
            Get
                Return FI_SortOrderOld
            End Get
        End Property

        Public ReadOnly Property OrgTypeOld As Field(Of String) 
            Get
                Return FI_OrgTypeOld
            End Get
        End Property

        Public ReadOnly Property BusinessTypeOld As Field(Of String) 
            Get
                Return FI_BusinessTypeOld
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
        Public Function DeleteRowByPrimaryKey(ByVal OrganizationLogRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From OrganizationLog")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And OrganID = @OrganID")
            strSQL.AppendLine("And OrganReason = @OrganReason")
            strSQL.AppendLine("And OrganType = @OrganType")
            strSQL.AppendLine("And ValidDateB = @ValidDateB")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, OrganizationLogRow.CompID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationLogRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@OrganReason", DbType.String, OrganizationLogRow.OrganReason.Value)
            db.AddInParameter(dbcmd, "@OrganType", DbType.String, OrganizationLogRow.OrganType.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, OrganizationLogRow.ValidDateB.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, OrganizationLogRow.Seq.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal OrganizationLogRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From OrganizationLog")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And OrganID = @OrganID")
            strSQL.AppendLine("And OrganReason = @OrganReason")
            strSQL.AppendLine("And OrganType = @OrganType")
            strSQL.AppendLine("And ValidDateB = @ValidDateB")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, OrganizationLogRow.CompID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationLogRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@OrganReason", DbType.String, OrganizationLogRow.OrganReason.Value)
            db.AddInParameter(dbcmd, "@OrganType", DbType.String, OrganizationLogRow.OrganType.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, OrganizationLogRow.ValidDateB.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, OrganizationLogRow.Seq.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal OrganizationLogRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From OrganizationLog")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And OrganID = @OrganID")
            strSQL.AppendLine("And OrganReason = @OrganReason")
            strSQL.AppendLine("And OrganType = @OrganType")
            strSQL.AppendLine("And ValidDateB = @ValidDateB")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In OrganizationLogRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                        db.AddInParameter(dbcmd, "@OrganReason", DbType.String, r.OrganReason.Value)
                        db.AddInParameter(dbcmd, "@OrganType", DbType.String, r.OrganType.Value)
                        db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, r.ValidDateB.Value)
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

        Public Function DeleteRowByPrimaryKey(ByVal OrganizationLogRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From OrganizationLog")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And OrganID = @OrganID")
            strSQL.AppendLine("And OrganReason = @OrganReason")
            strSQL.AppendLine("And OrganType = @OrganType")
            strSQL.AppendLine("And ValidDateB = @ValidDateB")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In OrganizationLogRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                db.AddInParameter(dbcmd, "@OrganReason", DbType.String, r.OrganReason.Value)
                db.AddInParameter(dbcmd, "@OrganType", DbType.String, r.OrganType.Value)
                db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, r.ValidDateB.Value)
                db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal OrganizationLogRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From OrganizationLog")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And OrganID = @OrganID")
            strSQL.AppendLine("And OrganReason = @OrganReason")
            strSQL.AppendLine("And OrganType = @OrganType")
            strSQL.AppendLine("And ValidDateB = @ValidDateB")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, OrganizationLogRow.CompID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationLogRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@OrganReason", DbType.String, OrganizationLogRow.OrganReason.Value)
            db.AddInParameter(dbcmd, "@OrganType", DbType.String, OrganizationLogRow.OrganType.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, OrganizationLogRow.ValidDateB.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, OrganizationLogRow.Seq.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(OrganizationLogRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From OrganizationLog")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And OrganID = @OrganID")
            strSQL.AppendLine("And OrganReason = @OrganReason")
            strSQL.AppendLine("And OrganType = @OrganType")
            strSQL.AppendLine("And ValidDateB = @ValidDateB")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, OrganizationLogRow.CompID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationLogRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@OrganReason", DbType.String, OrganizationLogRow.OrganReason.Value)
            db.AddInParameter(dbcmd, "@OrganType", DbType.String, OrganizationLogRow.OrganType.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, OrganizationLogRow.ValidDateB.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, OrganizationLogRow.Seq.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal OrganizationLogRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update OrganizationLog Set")
            For i As Integer = 0 To OrganizationLogRow.FieldNames.Length - 1
                If Not OrganizationLogRow.IsIdentityField(OrganizationLogRow.FieldNames(i)) AndAlso OrganizationLogRow.IsUpdated(OrganizationLogRow.FieldNames(i)) AndAlso OrganizationLogRow.CreateUpdateSQL(OrganizationLogRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, OrganizationLogRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And OrganID = @PKOrganID")
            strSQL.AppendLine("And OrganReason = @PKOrganReason")
            strSQL.AppendLine("And OrganType = @PKOrganType")
            strSQL.AppendLine("And ValidDateB = @PKValidDateB")
            strSQL.AppendLine("And Seq = @PKSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If OrganizationLogRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, OrganizationLogRow.CompID.Value)
            If OrganizationLogRow.OrganReason.Updated Then db.AddInParameter(dbcmd, "@OrganReason", DbType.String, OrganizationLogRow.OrganReason.Value)
            If OrganizationLogRow.OrganType.Updated Then db.AddInParameter(dbcmd, "@OrganType", DbType.String, OrganizationLogRow.OrganType.Value)
            If OrganizationLogRow.ValidDateB.Updated Then db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(OrganizationLogRow.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), OrganizationLogRow.ValidDateB.Value))
            If OrganizationLogRow.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, OrganizationLogRow.Seq.Value)
            If OrganizationLogRow.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationLogRow.OrganID.Value)
            If OrganizationLogRow.OrganName.Updated Then db.AddInParameter(dbcmd, "@OrganName", DbType.String, OrganizationLogRow.OrganName.Value)
            If OrganizationLogRow.OrganEngName.Updated Then db.AddInParameter(dbcmd, "@OrganEngName", DbType.String, OrganizationLogRow.OrganEngName.Value)
            If OrganizationLogRow.VirtualFlag.Updated Then db.AddInParameter(dbcmd, "@VirtualFlag", DbType.String, OrganizationLogRow.VirtualFlag.Value)
            If OrganizationLogRow.UpOrganID.Updated Then db.AddInParameter(dbcmd, "@UpOrganID", DbType.String, OrganizationLogRow.UpOrganID.Value)
            If OrganizationLogRow.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, OrganizationLogRow.DeptID.Value)
            If OrganizationLogRow.GroupType.Updated Then db.AddInParameter(dbcmd, "@GroupType", DbType.String, OrganizationLogRow.GroupType.Value)
            If OrganizationLogRow.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, OrganizationLogRow.GroupID.Value)
            If OrganizationLogRow.Boss.Updated Then db.AddInParameter(dbcmd, "@Boss", DbType.String, OrganizationLogRow.Boss.Value)
            If OrganizationLogRow.BossCompID.Updated Then db.AddInParameter(dbcmd, "@BossCompID", DbType.String, OrganizationLogRow.BossCompID.Value)
            If OrganizationLogRow.BossType.Updated Then db.AddInParameter(dbcmd, "@BossType", DbType.String, OrganizationLogRow.BossType.Value)
            If OrganizationLogRow.BossTemporary.Updated Then db.AddInParameter(dbcmd, "@BossTemporary", DbType.String, OrganizationLogRow.BossTemporary.Value)
            If OrganizationLogRow.SecBoss.Updated Then db.AddInParameter(dbcmd, "@SecBoss", DbType.String, OrganizationLogRow.SecBoss.Value)
            If OrganizationLogRow.SecBossCompID.Updated Then db.AddInParameter(dbcmd, "@SecBossCompID", DbType.String, OrganizationLogRow.SecBossCompID.Value)
            If OrganizationLogRow.InValidFlag.Updated Then db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, OrganizationLogRow.InValidFlag.Value)
            If OrganizationLogRow.ReportingEnd.Updated Then db.AddInParameter(dbcmd, "@ReportingEnd", DbType.String, OrganizationLogRow.ReportingEnd.Value)
            If OrganizationLogRow.Depth.Updated Then db.AddInParameter(dbcmd, "@Depth", DbType.String, OrganizationLogRow.Depth.Value)
            If OrganizationLogRow.BranchFlag.Updated Then db.AddInParameter(dbcmd, "@BranchFlag", DbType.String, OrganizationLogRow.BranchFlag.Value)
            If OrganizationLogRow.RoleCode.Updated Then db.AddInParameter(dbcmd, "@RoleCode", DbType.String, OrganizationLogRow.RoleCode.Value)
            If OrganizationLogRow.PersonPart.Updated Then db.AddInParameter(dbcmd, "@PersonPart", DbType.String, OrganizationLogRow.PersonPart.Value)
            If OrganizationLogRow.SecPersonPart.Updated Then db.AddInParameter(dbcmd, "@SecPersonPart", DbType.String, OrganizationLogRow.SecPersonPart.Value)
            If OrganizationLogRow.CheckPart.Updated Then db.AddInParameter(dbcmd, "@CheckPart", DbType.String, OrganizationLogRow.CheckPart.Value)
            If OrganizationLogRow.WorkSiteID.Updated Then db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, OrganizationLogRow.WorkSiteID.Value)
            If OrganizationLogRow.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, OrganizationLogRow.WorkTypeID.Value)
            If OrganizationLogRow.CostDeptID.Updated Then db.AddInParameter(dbcmd, "@CostDeptID", DbType.String, OrganizationLogRow.CostDeptID.Value)
            If OrganizationLogRow.CostType.Updated Then db.AddInParameter(dbcmd, "@CostType", DbType.String, OrganizationLogRow.CostType.Value)
            If OrganizationLogRow.AccountBranch.Updated Then db.AddInParameter(dbcmd, "@AccountBranch", DbType.String, OrganizationLogRow.AccountBranch.Value)
            If OrganizationLogRow.PositionID.Updated Then db.AddInParameter(dbcmd, "@PositionID", DbType.String, OrganizationLogRow.PositionID.Value)
            If OrganizationLogRow.CompareFlag.Updated Then db.AddInParameter(dbcmd, "@CompareFlag", DbType.String, OrganizationLogRow.CompareFlag.Value)
            If OrganizationLogRow.FlowOrganID.Updated Then db.AddInParameter(dbcmd, "@FlowOrganID", DbType.String, OrganizationLogRow.FlowOrganID.Value)
            If OrganizationLogRow.DelegateFlag.Updated Then db.AddInParameter(dbcmd, "@DelegateFlag", DbType.String, OrganizationLogRow.DelegateFlag.Value)
            If OrganizationLogRow.OrganNo.Updated Then db.AddInParameter(dbcmd, "@OrganNo", DbType.String, OrganizationLogRow.OrganNo.Value)
            If OrganizationLogRow.InvoiceNo.Updated Then db.AddInParameter(dbcmd, "@InvoiceNo", DbType.String, OrganizationLogRow.InvoiceNo.Value)
            If OrganizationLogRow.SortOrder.Updated Then db.AddInParameter(dbcmd, "@SortOrder", DbType.String, OrganizationLogRow.SortOrder.Value)
            If OrganizationLogRow.OrgType.Updated Then db.AddInParameter(dbcmd, "@OrgType", DbType.String, OrganizationLogRow.OrgType.Value)
            If OrganizationLogRow.BusinessType.Updated Then db.AddInParameter(dbcmd, "@BusinessType", DbType.String, OrganizationLogRow.BusinessType.Value)
            If OrganizationLogRow.ValidDateE.Updated Then db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(OrganizationLogRow.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), OrganizationLogRow.ValidDateE.Value))
            If OrganizationLogRow.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, OrganizationLogRow.Remark.Value)
            If OrganizationLogRow.OrganNameOld.Updated Then db.AddInParameter(dbcmd, "@OrganNameOld", DbType.String, OrganizationLogRow.OrganNameOld.Value)
            If OrganizationLogRow.OrganEngNameOld.Updated Then db.AddInParameter(dbcmd, "@OrganEngNameOld", DbType.String, OrganizationLogRow.OrganEngNameOld.Value)
            If OrganizationLogRow.VirtualFlagOld.Updated Then db.AddInParameter(dbcmd, "@VirtualFlagOld", DbType.String, OrganizationLogRow.VirtualFlagOld.Value)
            If OrganizationLogRow.UpOrganIDOld.Updated Then db.AddInParameter(dbcmd, "@UpOrganIDOld", DbType.String, OrganizationLogRow.UpOrganIDOld.Value)
            If OrganizationLogRow.DeptIDOld.Updated Then db.AddInParameter(dbcmd, "@DeptIDOld", DbType.String, OrganizationLogRow.DeptIDOld.Value)
            If OrganizationLogRow.GroupTypeOld.Updated Then db.AddInParameter(dbcmd, "@GroupTypeOld", DbType.String, OrganizationLogRow.GroupTypeOld.Value)
            If OrganizationLogRow.GroupIDOld.Updated Then db.AddInParameter(dbcmd, "@GroupIDOld", DbType.String, OrganizationLogRow.GroupIDOld.Value)
            If OrganizationLogRow.BossOld.Updated Then db.AddInParameter(dbcmd, "@BossOld", DbType.String, OrganizationLogRow.BossOld.Value)
            If OrganizationLogRow.BossCompIDOld.Updated Then db.AddInParameter(dbcmd, "@BossCompIDOld", DbType.String, OrganizationLogRow.BossCompIDOld.Value)
            If OrganizationLogRow.BossTypeOld.Updated Then db.AddInParameter(dbcmd, "@BossTypeOld", DbType.String, OrganizationLogRow.BossTypeOld.Value)
            If OrganizationLogRow.BossTemporaryOld.Updated Then db.AddInParameter(dbcmd, "@BossTemporaryOld", DbType.String, OrganizationLogRow.BossTemporaryOld.Value)
            If OrganizationLogRow.SecBossOld.Updated Then db.AddInParameter(dbcmd, "@SecBossOld", DbType.String, OrganizationLogRow.SecBossOld.Value)
            If OrganizationLogRow.SecBossCompIDOld.Updated Then db.AddInParameter(dbcmd, "@SecBossCompIDOld", DbType.String, OrganizationLogRow.SecBossCompIDOld.Value)
            If OrganizationLogRow.InValidFlagOld.Updated Then db.AddInParameter(dbcmd, "@InValidFlagOld", DbType.String, OrganizationLogRow.InValidFlagOld.Value)
            If OrganizationLogRow.ReportingEndOld.Updated Then db.AddInParameter(dbcmd, "@ReportingEndOld", DbType.String, OrganizationLogRow.ReportingEndOld.Value)
            If OrganizationLogRow.DepthOld.Updated Then db.AddInParameter(dbcmd, "@DepthOld", DbType.String, OrganizationLogRow.DepthOld.Value)
            If OrganizationLogRow.BranchFlagOld.Updated Then db.AddInParameter(dbcmd, "@BranchFlagOld", DbType.String, OrganizationLogRow.BranchFlagOld.Value)
            If OrganizationLogRow.RoleCodeOld.Updated Then db.AddInParameter(dbcmd, "@RoleCodeOld", DbType.String, OrganizationLogRow.RoleCodeOld.Value)
            If OrganizationLogRow.PersonPartOld.Updated Then db.AddInParameter(dbcmd, "@PersonPartOld", DbType.String, OrganizationLogRow.PersonPartOld.Value)
            If OrganizationLogRow.SecPersonPartOld.Updated Then db.AddInParameter(dbcmd, "@SecPersonPartOld", DbType.String, OrganizationLogRow.SecPersonPartOld.Value)
            If OrganizationLogRow.CheckPartOld.Updated Then db.AddInParameter(dbcmd, "@CheckPartOld", DbType.String, OrganizationLogRow.CheckPartOld.Value)
            If OrganizationLogRow.WorkSiteIDOld.Updated Then db.AddInParameter(dbcmd, "@WorkSiteIDOld", DbType.String, OrganizationLogRow.WorkSiteIDOld.Value)
            If OrganizationLogRow.WorkTypeIDOld.Updated Then db.AddInParameter(dbcmd, "@WorkTypeIDOld", DbType.String, OrganizationLogRow.WorkTypeIDOld.Value)
            If OrganizationLogRow.CostDeptIDOld.Updated Then db.AddInParameter(dbcmd, "@CostDeptIDOld", DbType.String, OrganizationLogRow.CostDeptIDOld.Value)
            If OrganizationLogRow.CostTypeOld.Updated Then db.AddInParameter(dbcmd, "@CostTypeOld", DbType.String, OrganizationLogRow.CostTypeOld.Value)
            If OrganizationLogRow.AccountBranchOld.Updated Then db.AddInParameter(dbcmd, "@AccountBranchOld", DbType.String, OrganizationLogRow.AccountBranchOld.Value)
            If OrganizationLogRow.PositionIDOld.Updated Then db.AddInParameter(dbcmd, "@PositionIDOld", DbType.String, OrganizationLogRow.PositionIDOld.Value)
            If OrganizationLogRow.CompareFlagOld.Updated Then db.AddInParameter(dbcmd, "@CompareFlagOld", DbType.String, OrganizationLogRow.CompareFlagOld.Value)
            If OrganizationLogRow.FlowOrganIDOld.Updated Then db.AddInParameter(dbcmd, "@FlowOrganIDOld", DbType.String, OrganizationLogRow.FlowOrganIDOld.Value)
            If OrganizationLogRow.DelegateFlagOld.Updated Then db.AddInParameter(dbcmd, "@DelegateFlagOld", DbType.String, OrganizationLogRow.DelegateFlagOld.Value)
            If OrganizationLogRow.OrganNoOld.Updated Then db.AddInParameter(dbcmd, "@OrganNoOld", DbType.String, OrganizationLogRow.OrganNoOld.Value)
            If OrganizationLogRow.InvoiceNoOld.Updated Then db.AddInParameter(dbcmd, "@InvoiceNoOld", DbType.String, OrganizationLogRow.InvoiceNoOld.Value)
            If OrganizationLogRow.SortOrderOld.Updated Then db.AddInParameter(dbcmd, "@SortOrderOld", DbType.String, OrganizationLogRow.SortOrderOld.Value)
            If OrganizationLogRow.OrgTypeOld.Updated Then db.AddInParameter(dbcmd, "@OrgTypeOld", DbType.String, OrganizationLogRow.OrgTypeOld.Value)
            If OrganizationLogRow.BusinessTypeOld.Updated Then db.AddInParameter(dbcmd, "@BusinessTypeOld", DbType.String, OrganizationLogRow.BusinessTypeOld.Value)
            If OrganizationLogRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, OrganizationLogRow.LastChgComp.Value)
            If OrganizationLogRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, OrganizationLogRow.LastChgID.Value)
            If OrganizationLogRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(OrganizationLogRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), OrganizationLogRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(OrganizationLogRow.LoadFromDataRow, OrganizationLogRow.CompID.OldValue, OrganizationLogRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKOrganID", DbType.String, IIf(OrganizationLogRow.LoadFromDataRow, OrganizationLogRow.OrganID.OldValue, OrganizationLogRow.OrganID.Value))
            db.AddInParameter(dbcmd, "@PKOrganReason", DbType.String, IIf(OrganizationLogRow.LoadFromDataRow, OrganizationLogRow.OrganReason.OldValue, OrganizationLogRow.OrganReason.Value))
            db.AddInParameter(dbcmd, "@PKOrganType", DbType.String, IIf(OrganizationLogRow.LoadFromDataRow, OrganizationLogRow.OrganType.OldValue, OrganizationLogRow.OrganType.Value))
            db.AddInParameter(dbcmd, "@PKValidDateB", DbType.Date, IIf(OrganizationLogRow.LoadFromDataRow, OrganizationLogRow.ValidDateB.OldValue, OrganizationLogRow.ValidDateB.Value))
            db.AddInParameter(dbcmd, "@PKSeq", DbType.Int32, IIf(OrganizationLogRow.LoadFromDataRow, OrganizationLogRow.Seq.OldValue, OrganizationLogRow.Seq.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal OrganizationLogRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update OrganizationLog Set")
            For i As Integer = 0 To OrganizationLogRow.FieldNames.Length - 1
                If Not OrganizationLogRow.IsIdentityField(OrganizationLogRow.FieldNames(i)) AndAlso OrganizationLogRow.IsUpdated(OrganizationLogRow.FieldNames(i)) AndAlso OrganizationLogRow.CreateUpdateSQL(OrganizationLogRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, OrganizationLogRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And OrganID = @PKOrganID")
            strSQL.AppendLine("And OrganReason = @PKOrganReason")
            strSQL.AppendLine("And OrganType = @PKOrganType")
            strSQL.AppendLine("And ValidDateB = @PKValidDateB")
            strSQL.AppendLine("And Seq = @PKSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If OrganizationLogRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, OrganizationLogRow.CompID.Value)
            If OrganizationLogRow.OrganReason.Updated Then db.AddInParameter(dbcmd, "@OrganReason", DbType.String, OrganizationLogRow.OrganReason.Value)
            If OrganizationLogRow.OrganType.Updated Then db.AddInParameter(dbcmd, "@OrganType", DbType.String, OrganizationLogRow.OrganType.Value)
            If OrganizationLogRow.ValidDateB.Updated Then db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(OrganizationLogRow.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), OrganizationLogRow.ValidDateB.Value))
            If OrganizationLogRow.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, OrganizationLogRow.Seq.Value)
            If OrganizationLogRow.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationLogRow.OrganID.Value)
            If OrganizationLogRow.OrganName.Updated Then db.AddInParameter(dbcmd, "@OrganName", DbType.String, OrganizationLogRow.OrganName.Value)
            If OrganizationLogRow.OrganEngName.Updated Then db.AddInParameter(dbcmd, "@OrganEngName", DbType.String, OrganizationLogRow.OrganEngName.Value)
            If OrganizationLogRow.VirtualFlag.Updated Then db.AddInParameter(dbcmd, "@VirtualFlag", DbType.String, OrganizationLogRow.VirtualFlag.Value)
            If OrganizationLogRow.UpOrganID.Updated Then db.AddInParameter(dbcmd, "@UpOrganID", DbType.String, OrganizationLogRow.UpOrganID.Value)
            If OrganizationLogRow.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, OrganizationLogRow.DeptID.Value)
            If OrganizationLogRow.GroupType.Updated Then db.AddInParameter(dbcmd, "@GroupType", DbType.String, OrganizationLogRow.GroupType.Value)
            If OrganizationLogRow.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, OrganizationLogRow.GroupID.Value)
            If OrganizationLogRow.Boss.Updated Then db.AddInParameter(dbcmd, "@Boss", DbType.String, OrganizationLogRow.Boss.Value)
            If OrganizationLogRow.BossCompID.Updated Then db.AddInParameter(dbcmd, "@BossCompID", DbType.String, OrganizationLogRow.BossCompID.Value)
            If OrganizationLogRow.BossType.Updated Then db.AddInParameter(dbcmd, "@BossType", DbType.String, OrganizationLogRow.BossType.Value)
            If OrganizationLogRow.BossTemporary.Updated Then db.AddInParameter(dbcmd, "@BossTemporary", DbType.String, OrganizationLogRow.BossTemporary.Value)
            If OrganizationLogRow.SecBoss.Updated Then db.AddInParameter(dbcmd, "@SecBoss", DbType.String, OrganizationLogRow.SecBoss.Value)
            If OrganizationLogRow.SecBossCompID.Updated Then db.AddInParameter(dbcmd, "@SecBossCompID", DbType.String, OrganizationLogRow.SecBossCompID.Value)
            If OrganizationLogRow.InValidFlag.Updated Then db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, OrganizationLogRow.InValidFlag.Value)
            If OrganizationLogRow.ReportingEnd.Updated Then db.AddInParameter(dbcmd, "@ReportingEnd", DbType.String, OrganizationLogRow.ReportingEnd.Value)
            If OrganizationLogRow.Depth.Updated Then db.AddInParameter(dbcmd, "@Depth", DbType.String, OrganizationLogRow.Depth.Value)
            If OrganizationLogRow.BranchFlag.Updated Then db.AddInParameter(dbcmd, "@BranchFlag", DbType.String, OrganizationLogRow.BranchFlag.Value)
            If OrganizationLogRow.RoleCode.Updated Then db.AddInParameter(dbcmd, "@RoleCode", DbType.String, OrganizationLogRow.RoleCode.Value)
            If OrganizationLogRow.PersonPart.Updated Then db.AddInParameter(dbcmd, "@PersonPart", DbType.String, OrganizationLogRow.PersonPart.Value)
            If OrganizationLogRow.SecPersonPart.Updated Then db.AddInParameter(dbcmd, "@SecPersonPart", DbType.String, OrganizationLogRow.SecPersonPart.Value)
            If OrganizationLogRow.CheckPart.Updated Then db.AddInParameter(dbcmd, "@CheckPart", DbType.String, OrganizationLogRow.CheckPart.Value)
            If OrganizationLogRow.WorkSiteID.Updated Then db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, OrganizationLogRow.WorkSiteID.Value)
            If OrganizationLogRow.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, OrganizationLogRow.WorkTypeID.Value)
            If OrganizationLogRow.CostDeptID.Updated Then db.AddInParameter(dbcmd, "@CostDeptID", DbType.String, OrganizationLogRow.CostDeptID.Value)
            If OrganizationLogRow.CostType.Updated Then db.AddInParameter(dbcmd, "@CostType", DbType.String, OrganizationLogRow.CostType.Value)
            If OrganizationLogRow.AccountBranch.Updated Then db.AddInParameter(dbcmd, "@AccountBranch", DbType.String, OrganizationLogRow.AccountBranch.Value)
            If OrganizationLogRow.PositionID.Updated Then db.AddInParameter(dbcmd, "@PositionID", DbType.String, OrganizationLogRow.PositionID.Value)
            If OrganizationLogRow.CompareFlag.Updated Then db.AddInParameter(dbcmd, "@CompareFlag", DbType.String, OrganizationLogRow.CompareFlag.Value)
            If OrganizationLogRow.FlowOrganID.Updated Then db.AddInParameter(dbcmd, "@FlowOrganID", DbType.String, OrganizationLogRow.FlowOrganID.Value)
            If OrganizationLogRow.DelegateFlag.Updated Then db.AddInParameter(dbcmd, "@DelegateFlag", DbType.String, OrganizationLogRow.DelegateFlag.Value)
            If OrganizationLogRow.OrganNo.Updated Then db.AddInParameter(dbcmd, "@OrganNo", DbType.String, OrganizationLogRow.OrganNo.Value)
            If OrganizationLogRow.InvoiceNo.Updated Then db.AddInParameter(dbcmd, "@InvoiceNo", DbType.String, OrganizationLogRow.InvoiceNo.Value)
            If OrganizationLogRow.SortOrder.Updated Then db.AddInParameter(dbcmd, "@SortOrder", DbType.String, OrganizationLogRow.SortOrder.Value)
            If OrganizationLogRow.OrgType.Updated Then db.AddInParameter(dbcmd, "@OrgType", DbType.String, OrganizationLogRow.OrgType.Value)
            If OrganizationLogRow.BusinessType.Updated Then db.AddInParameter(dbcmd, "@BusinessType", DbType.String, OrganizationLogRow.BusinessType.Value)
            If OrganizationLogRow.ValidDateE.Updated Then db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(OrganizationLogRow.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), OrganizationLogRow.ValidDateE.Value))
            If OrganizationLogRow.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, OrganizationLogRow.Remark.Value)
            If OrganizationLogRow.OrganNameOld.Updated Then db.AddInParameter(dbcmd, "@OrganNameOld", DbType.String, OrganizationLogRow.OrganNameOld.Value)
            If OrganizationLogRow.OrganEngNameOld.Updated Then db.AddInParameter(dbcmd, "@OrganEngNameOld", DbType.String, OrganizationLogRow.OrganEngNameOld.Value)
            If OrganizationLogRow.VirtualFlagOld.Updated Then db.AddInParameter(dbcmd, "@VirtualFlagOld", DbType.String, OrganizationLogRow.VirtualFlagOld.Value)
            If OrganizationLogRow.UpOrganIDOld.Updated Then db.AddInParameter(dbcmd, "@UpOrganIDOld", DbType.String, OrganizationLogRow.UpOrganIDOld.Value)
            If OrganizationLogRow.DeptIDOld.Updated Then db.AddInParameter(dbcmd, "@DeptIDOld", DbType.String, OrganizationLogRow.DeptIDOld.Value)
            If OrganizationLogRow.GroupTypeOld.Updated Then db.AddInParameter(dbcmd, "@GroupTypeOld", DbType.String, OrganizationLogRow.GroupTypeOld.Value)
            If OrganizationLogRow.GroupIDOld.Updated Then db.AddInParameter(dbcmd, "@GroupIDOld", DbType.String, OrganizationLogRow.GroupIDOld.Value)
            If OrganizationLogRow.BossOld.Updated Then db.AddInParameter(dbcmd, "@BossOld", DbType.String, OrganizationLogRow.BossOld.Value)
            If OrganizationLogRow.BossCompIDOld.Updated Then db.AddInParameter(dbcmd, "@BossCompIDOld", DbType.String, OrganizationLogRow.BossCompIDOld.Value)
            If OrganizationLogRow.BossTypeOld.Updated Then db.AddInParameter(dbcmd, "@BossTypeOld", DbType.String, OrganizationLogRow.BossTypeOld.Value)
            If OrganizationLogRow.BossTemporaryOld.Updated Then db.AddInParameter(dbcmd, "@BossTemporaryOld", DbType.String, OrganizationLogRow.BossTemporaryOld.Value)
            If OrganizationLogRow.SecBossOld.Updated Then db.AddInParameter(dbcmd, "@SecBossOld", DbType.String, OrganizationLogRow.SecBossOld.Value)
            If OrganizationLogRow.SecBossCompIDOld.Updated Then db.AddInParameter(dbcmd, "@SecBossCompIDOld", DbType.String, OrganizationLogRow.SecBossCompIDOld.Value)
            If OrganizationLogRow.InValidFlagOld.Updated Then db.AddInParameter(dbcmd, "@InValidFlagOld", DbType.String, OrganizationLogRow.InValidFlagOld.Value)
            If OrganizationLogRow.ReportingEndOld.Updated Then db.AddInParameter(dbcmd, "@ReportingEndOld", DbType.String, OrganizationLogRow.ReportingEndOld.Value)
            If OrganizationLogRow.DepthOld.Updated Then db.AddInParameter(dbcmd, "@DepthOld", DbType.String, OrganizationLogRow.DepthOld.Value)
            If OrganizationLogRow.BranchFlagOld.Updated Then db.AddInParameter(dbcmd, "@BranchFlagOld", DbType.String, OrganizationLogRow.BranchFlagOld.Value)
            If OrganizationLogRow.RoleCodeOld.Updated Then db.AddInParameter(dbcmd, "@RoleCodeOld", DbType.String, OrganizationLogRow.RoleCodeOld.Value)
            If OrganizationLogRow.PersonPartOld.Updated Then db.AddInParameter(dbcmd, "@PersonPartOld", DbType.String, OrganizationLogRow.PersonPartOld.Value)
            If OrganizationLogRow.SecPersonPartOld.Updated Then db.AddInParameter(dbcmd, "@SecPersonPartOld", DbType.String, OrganizationLogRow.SecPersonPartOld.Value)
            If OrganizationLogRow.CheckPartOld.Updated Then db.AddInParameter(dbcmd, "@CheckPartOld", DbType.String, OrganizationLogRow.CheckPartOld.Value)
            If OrganizationLogRow.WorkSiteIDOld.Updated Then db.AddInParameter(dbcmd, "@WorkSiteIDOld", DbType.String, OrganizationLogRow.WorkSiteIDOld.Value)
            If OrganizationLogRow.WorkTypeIDOld.Updated Then db.AddInParameter(dbcmd, "@WorkTypeIDOld", DbType.String, OrganizationLogRow.WorkTypeIDOld.Value)
            If OrganizationLogRow.CostDeptIDOld.Updated Then db.AddInParameter(dbcmd, "@CostDeptIDOld", DbType.String, OrganizationLogRow.CostDeptIDOld.Value)
            If OrganizationLogRow.CostTypeOld.Updated Then db.AddInParameter(dbcmd, "@CostTypeOld", DbType.String, OrganizationLogRow.CostTypeOld.Value)
            If OrganizationLogRow.AccountBranchOld.Updated Then db.AddInParameter(dbcmd, "@AccountBranchOld", DbType.String, OrganizationLogRow.AccountBranchOld.Value)
            If OrganizationLogRow.PositionIDOld.Updated Then db.AddInParameter(dbcmd, "@PositionIDOld", DbType.String, OrganizationLogRow.PositionIDOld.Value)
            If OrganizationLogRow.CompareFlagOld.Updated Then db.AddInParameter(dbcmd, "@CompareFlagOld", DbType.String, OrganizationLogRow.CompareFlagOld.Value)
            If OrganizationLogRow.FlowOrganIDOld.Updated Then db.AddInParameter(dbcmd, "@FlowOrganIDOld", DbType.String, OrganizationLogRow.FlowOrganIDOld.Value)
            If OrganizationLogRow.DelegateFlagOld.Updated Then db.AddInParameter(dbcmd, "@DelegateFlagOld", DbType.String, OrganizationLogRow.DelegateFlagOld.Value)
            If OrganizationLogRow.OrganNoOld.Updated Then db.AddInParameter(dbcmd, "@OrganNoOld", DbType.String, OrganizationLogRow.OrganNoOld.Value)
            If OrganizationLogRow.InvoiceNoOld.Updated Then db.AddInParameter(dbcmd, "@InvoiceNoOld", DbType.String, OrganizationLogRow.InvoiceNoOld.Value)
            If OrganizationLogRow.SortOrderOld.Updated Then db.AddInParameter(dbcmd, "@SortOrderOld", DbType.String, OrganizationLogRow.SortOrderOld.Value)
            If OrganizationLogRow.OrgTypeOld.Updated Then db.AddInParameter(dbcmd, "@OrgTypeOld", DbType.String, OrganizationLogRow.OrgTypeOld.Value)
            If OrganizationLogRow.BusinessTypeOld.Updated Then db.AddInParameter(dbcmd, "@BusinessTypeOld", DbType.String, OrganizationLogRow.BusinessTypeOld.Value)
            If OrganizationLogRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, OrganizationLogRow.LastChgComp.Value)
            If OrganizationLogRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, OrganizationLogRow.LastChgID.Value)
            If OrganizationLogRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(OrganizationLogRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), OrganizationLogRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(OrganizationLogRow.LoadFromDataRow, OrganizationLogRow.CompID.OldValue, OrganizationLogRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKOrganID", DbType.String, IIf(OrganizationLogRow.LoadFromDataRow, OrganizationLogRow.OrganID.OldValue, OrganizationLogRow.OrganID.Value))
            db.AddInParameter(dbcmd, "@PKOrganReason", DbType.String, IIf(OrganizationLogRow.LoadFromDataRow, OrganizationLogRow.OrganReason.OldValue, OrganizationLogRow.OrganReason.Value))
            db.AddInParameter(dbcmd, "@PKOrganType", DbType.String, IIf(OrganizationLogRow.LoadFromDataRow, OrganizationLogRow.OrganType.OldValue, OrganizationLogRow.OrganType.Value))
            db.AddInParameter(dbcmd, "@PKValidDateB", DbType.Date, IIf(OrganizationLogRow.LoadFromDataRow, OrganizationLogRow.ValidDateB.OldValue, OrganizationLogRow.ValidDateB.Value))
            db.AddInParameter(dbcmd, "@PKSeq", DbType.Int32, IIf(OrganizationLogRow.LoadFromDataRow, OrganizationLogRow.Seq.OldValue, OrganizationLogRow.Seq.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal OrganizationLogRow As Row()) As Integer
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
                    For Each r As Row In OrganizationLogRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update OrganizationLog Set")
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
                        strSQL.AppendLine("And ValidDateB = @PKValidDateB")
                        strSQL.AppendLine("And Seq = @PKSeq")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.OrganReason.Updated Then db.AddInParameter(dbcmd, "@OrganReason", DbType.String, r.OrganReason.Value)
                        If r.OrganType.Updated Then db.AddInParameter(dbcmd, "@OrganType", DbType.String, r.OrganType.Value)
                        If r.ValidDateB.Updated Then db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(r.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateB.Value))
                        If r.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
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
                        If r.ValidDateE.Updated Then db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(r.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateE.Value))
                        If r.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                        If r.OrganNameOld.Updated Then db.AddInParameter(dbcmd, "@OrganNameOld", DbType.String, r.OrganNameOld.Value)
                        If r.OrganEngNameOld.Updated Then db.AddInParameter(dbcmd, "@OrganEngNameOld", DbType.String, r.OrganEngNameOld.Value)
                        If r.VirtualFlagOld.Updated Then db.AddInParameter(dbcmd, "@VirtualFlagOld", DbType.String, r.VirtualFlagOld.Value)
                        If r.UpOrganIDOld.Updated Then db.AddInParameter(dbcmd, "@UpOrganIDOld", DbType.String, r.UpOrganIDOld.Value)
                        If r.DeptIDOld.Updated Then db.AddInParameter(dbcmd, "@DeptIDOld", DbType.String, r.DeptIDOld.Value)
                        If r.GroupTypeOld.Updated Then db.AddInParameter(dbcmd, "@GroupTypeOld", DbType.String, r.GroupTypeOld.Value)
                        If r.GroupIDOld.Updated Then db.AddInParameter(dbcmd, "@GroupIDOld", DbType.String, r.GroupIDOld.Value)
                        If r.BossOld.Updated Then db.AddInParameter(dbcmd, "@BossOld", DbType.String, r.BossOld.Value)
                        If r.BossCompIDOld.Updated Then db.AddInParameter(dbcmd, "@BossCompIDOld", DbType.String, r.BossCompIDOld.Value)
                        If r.BossTypeOld.Updated Then db.AddInParameter(dbcmd, "@BossTypeOld", DbType.String, r.BossTypeOld.Value)
                        If r.BossTemporaryOld.Updated Then db.AddInParameter(dbcmd, "@BossTemporaryOld", DbType.String, r.BossTemporaryOld.Value)
                        If r.SecBossOld.Updated Then db.AddInParameter(dbcmd, "@SecBossOld", DbType.String, r.SecBossOld.Value)
                        If r.SecBossCompIDOld.Updated Then db.AddInParameter(dbcmd, "@SecBossCompIDOld", DbType.String, r.SecBossCompIDOld.Value)
                        If r.InValidFlagOld.Updated Then db.AddInParameter(dbcmd, "@InValidFlagOld", DbType.String, r.InValidFlagOld.Value)
                        If r.ReportingEndOld.Updated Then db.AddInParameter(dbcmd, "@ReportingEndOld", DbType.String, r.ReportingEndOld.Value)
                        If r.DepthOld.Updated Then db.AddInParameter(dbcmd, "@DepthOld", DbType.String, r.DepthOld.Value)
                        If r.BranchFlagOld.Updated Then db.AddInParameter(dbcmd, "@BranchFlagOld", DbType.String, r.BranchFlagOld.Value)
                        If r.RoleCodeOld.Updated Then db.AddInParameter(dbcmd, "@RoleCodeOld", DbType.String, r.RoleCodeOld.Value)
                        If r.PersonPartOld.Updated Then db.AddInParameter(dbcmd, "@PersonPartOld", DbType.String, r.PersonPartOld.Value)
                        If r.SecPersonPartOld.Updated Then db.AddInParameter(dbcmd, "@SecPersonPartOld", DbType.String, r.SecPersonPartOld.Value)
                        If r.CheckPartOld.Updated Then db.AddInParameter(dbcmd, "@CheckPartOld", DbType.String, r.CheckPartOld.Value)
                        If r.WorkSiteIDOld.Updated Then db.AddInParameter(dbcmd, "@WorkSiteIDOld", DbType.String, r.WorkSiteIDOld.Value)
                        If r.WorkTypeIDOld.Updated Then db.AddInParameter(dbcmd, "@WorkTypeIDOld", DbType.String, r.WorkTypeIDOld.Value)
                        If r.CostDeptIDOld.Updated Then db.AddInParameter(dbcmd, "@CostDeptIDOld", DbType.String, r.CostDeptIDOld.Value)
                        If r.CostTypeOld.Updated Then db.AddInParameter(dbcmd, "@CostTypeOld", DbType.String, r.CostTypeOld.Value)
                        If r.AccountBranchOld.Updated Then db.AddInParameter(dbcmd, "@AccountBranchOld", DbType.String, r.AccountBranchOld.Value)
                        If r.PositionIDOld.Updated Then db.AddInParameter(dbcmd, "@PositionIDOld", DbType.String, r.PositionIDOld.Value)
                        If r.CompareFlagOld.Updated Then db.AddInParameter(dbcmd, "@CompareFlagOld", DbType.String, r.CompareFlagOld.Value)
                        If r.FlowOrganIDOld.Updated Then db.AddInParameter(dbcmd, "@FlowOrganIDOld", DbType.String, r.FlowOrganIDOld.Value)
                        If r.DelegateFlagOld.Updated Then db.AddInParameter(dbcmd, "@DelegateFlagOld", DbType.String, r.DelegateFlagOld.Value)
                        If r.OrganNoOld.Updated Then db.AddInParameter(dbcmd, "@OrganNoOld", DbType.String, r.OrganNoOld.Value)
                        If r.InvoiceNoOld.Updated Then db.AddInParameter(dbcmd, "@InvoiceNoOld", DbType.String, r.InvoiceNoOld.Value)
                        If r.SortOrderOld.Updated Then db.AddInParameter(dbcmd, "@SortOrderOld", DbType.String, r.SortOrderOld.Value)
                        If r.OrgTypeOld.Updated Then db.AddInParameter(dbcmd, "@OrgTypeOld", DbType.String, r.OrgTypeOld.Value)
                        If r.BusinessTypeOld.Updated Then db.AddInParameter(dbcmd, "@BusinessTypeOld", DbType.String, r.BusinessTypeOld.Value)
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                        db.AddInParameter(dbcmd, "@PKOrganID", DbType.String, IIf(r.LoadFromDataRow, r.OrganID.OldValue, r.OrganID.Value))
                        db.AddInParameter(dbcmd, "@PKOrganReason", DbType.String, IIf(r.LoadFromDataRow, r.OrganReason.OldValue, r.OrganReason.Value))
                        db.AddInParameter(dbcmd, "@PKOrganType", DbType.String, IIf(r.LoadFromDataRow, r.OrganType.OldValue, r.OrganType.Value))
                        db.AddInParameter(dbcmd, "@PKValidDateB", DbType.Date, IIf(r.LoadFromDataRow, r.ValidDateB.OldValue, r.ValidDateB.Value))
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

        Public Function Update(ByVal OrganizationLogRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In OrganizationLogRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update OrganizationLog Set")
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
                strSQL.AppendLine("And ValidDateB = @PKValidDateB")
                strSQL.AppendLine("And Seq = @PKSeq")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.OrganReason.Updated Then db.AddInParameter(dbcmd, "@OrganReason", DbType.String, r.OrganReason.Value)
                If r.OrganType.Updated Then db.AddInParameter(dbcmd, "@OrganType", DbType.String, r.OrganType.Value)
                If r.ValidDateB.Updated Then db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(r.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateB.Value))
                If r.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
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
                If r.ValidDateE.Updated Then db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(r.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateE.Value))
                If r.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                If r.OrganNameOld.Updated Then db.AddInParameter(dbcmd, "@OrganNameOld", DbType.String, r.OrganNameOld.Value)
                If r.OrganEngNameOld.Updated Then db.AddInParameter(dbcmd, "@OrganEngNameOld", DbType.String, r.OrganEngNameOld.Value)
                If r.VirtualFlagOld.Updated Then db.AddInParameter(dbcmd, "@VirtualFlagOld", DbType.String, r.VirtualFlagOld.Value)
                If r.UpOrganIDOld.Updated Then db.AddInParameter(dbcmd, "@UpOrganIDOld", DbType.String, r.UpOrganIDOld.Value)
                If r.DeptIDOld.Updated Then db.AddInParameter(dbcmd, "@DeptIDOld", DbType.String, r.DeptIDOld.Value)
                If r.GroupTypeOld.Updated Then db.AddInParameter(dbcmd, "@GroupTypeOld", DbType.String, r.GroupTypeOld.Value)
                If r.GroupIDOld.Updated Then db.AddInParameter(dbcmd, "@GroupIDOld", DbType.String, r.GroupIDOld.Value)
                If r.BossOld.Updated Then db.AddInParameter(dbcmd, "@BossOld", DbType.String, r.BossOld.Value)
                If r.BossCompIDOld.Updated Then db.AddInParameter(dbcmd, "@BossCompIDOld", DbType.String, r.BossCompIDOld.Value)
                If r.BossTypeOld.Updated Then db.AddInParameter(dbcmd, "@BossTypeOld", DbType.String, r.BossTypeOld.Value)
                If r.BossTemporaryOld.Updated Then db.AddInParameter(dbcmd, "@BossTemporaryOld", DbType.String, r.BossTemporaryOld.Value)
                If r.SecBossOld.Updated Then db.AddInParameter(dbcmd, "@SecBossOld", DbType.String, r.SecBossOld.Value)
                If r.SecBossCompIDOld.Updated Then db.AddInParameter(dbcmd, "@SecBossCompIDOld", DbType.String, r.SecBossCompIDOld.Value)
                If r.InValidFlagOld.Updated Then db.AddInParameter(dbcmd, "@InValidFlagOld", DbType.String, r.InValidFlagOld.Value)
                If r.ReportingEndOld.Updated Then db.AddInParameter(dbcmd, "@ReportingEndOld", DbType.String, r.ReportingEndOld.Value)
                If r.DepthOld.Updated Then db.AddInParameter(dbcmd, "@DepthOld", DbType.String, r.DepthOld.Value)
                If r.BranchFlagOld.Updated Then db.AddInParameter(dbcmd, "@BranchFlagOld", DbType.String, r.BranchFlagOld.Value)
                If r.RoleCodeOld.Updated Then db.AddInParameter(dbcmd, "@RoleCodeOld", DbType.String, r.RoleCodeOld.Value)
                If r.PersonPartOld.Updated Then db.AddInParameter(dbcmd, "@PersonPartOld", DbType.String, r.PersonPartOld.Value)
                If r.SecPersonPartOld.Updated Then db.AddInParameter(dbcmd, "@SecPersonPartOld", DbType.String, r.SecPersonPartOld.Value)
                If r.CheckPartOld.Updated Then db.AddInParameter(dbcmd, "@CheckPartOld", DbType.String, r.CheckPartOld.Value)
                If r.WorkSiteIDOld.Updated Then db.AddInParameter(dbcmd, "@WorkSiteIDOld", DbType.String, r.WorkSiteIDOld.Value)
                If r.WorkTypeIDOld.Updated Then db.AddInParameter(dbcmd, "@WorkTypeIDOld", DbType.String, r.WorkTypeIDOld.Value)
                If r.CostDeptIDOld.Updated Then db.AddInParameter(dbcmd, "@CostDeptIDOld", DbType.String, r.CostDeptIDOld.Value)
                If r.CostTypeOld.Updated Then db.AddInParameter(dbcmd, "@CostTypeOld", DbType.String, r.CostTypeOld.Value)
                If r.AccountBranchOld.Updated Then db.AddInParameter(dbcmd, "@AccountBranchOld", DbType.String, r.AccountBranchOld.Value)
                If r.PositionIDOld.Updated Then db.AddInParameter(dbcmd, "@PositionIDOld", DbType.String, r.PositionIDOld.Value)
                If r.CompareFlagOld.Updated Then db.AddInParameter(dbcmd, "@CompareFlagOld", DbType.String, r.CompareFlagOld.Value)
                If r.FlowOrganIDOld.Updated Then db.AddInParameter(dbcmd, "@FlowOrganIDOld", DbType.String, r.FlowOrganIDOld.Value)
                If r.DelegateFlagOld.Updated Then db.AddInParameter(dbcmd, "@DelegateFlagOld", DbType.String, r.DelegateFlagOld.Value)
                If r.OrganNoOld.Updated Then db.AddInParameter(dbcmd, "@OrganNoOld", DbType.String, r.OrganNoOld.Value)
                If r.InvoiceNoOld.Updated Then db.AddInParameter(dbcmd, "@InvoiceNoOld", DbType.String, r.InvoiceNoOld.Value)
                If r.SortOrderOld.Updated Then db.AddInParameter(dbcmd, "@SortOrderOld", DbType.String, r.SortOrderOld.Value)
                If r.OrgTypeOld.Updated Then db.AddInParameter(dbcmd, "@OrgTypeOld", DbType.String, r.OrgTypeOld.Value)
                If r.BusinessTypeOld.Updated Then db.AddInParameter(dbcmd, "@BusinessTypeOld", DbType.String, r.BusinessTypeOld.Value)
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                db.AddInParameter(dbcmd, "@PKOrganID", DbType.String, IIf(r.LoadFromDataRow, r.OrganID.OldValue, r.OrganID.Value))
                db.AddInParameter(dbcmd, "@PKOrganReason", DbType.String, IIf(r.LoadFromDataRow, r.OrganReason.OldValue, r.OrganReason.Value))
                db.AddInParameter(dbcmd, "@PKOrganType", DbType.String, IIf(r.LoadFromDataRow, r.OrganType.OldValue, r.OrganType.Value))
                db.AddInParameter(dbcmd, "@PKValidDateB", DbType.Date, IIf(r.LoadFromDataRow, r.ValidDateB.OldValue, r.ValidDateB.Value))
                db.AddInParameter(dbcmd, "@PKSeq", DbType.Int32, IIf(r.LoadFromDataRow, r.Seq.OldValue, r.Seq.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal OrganizationLogRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From OrganizationLog")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And OrganID = @OrganID")
            strSQL.AppendLine("And OrganReason = @OrganReason")
            strSQL.AppendLine("And OrganType = @OrganType")
            strSQL.AppendLine("And ValidDateB = @ValidDateB")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, OrganizationLogRow.CompID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationLogRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@OrganReason", DbType.String, OrganizationLogRow.OrganReason.Value)
            db.AddInParameter(dbcmd, "@OrganType", DbType.String, OrganizationLogRow.OrganType.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, OrganizationLogRow.ValidDateB.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, OrganizationLogRow.Seq.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal OrganizationLogRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From OrganizationLog")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And OrganID = @OrganID")
            strSQL.AppendLine("And OrganReason = @OrganReason")
            strSQL.AppendLine("And OrganType = @OrganType")
            strSQL.AppendLine("And ValidDateB = @ValidDateB")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, OrganizationLogRow.CompID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationLogRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@OrganReason", DbType.String, OrganizationLogRow.OrganReason.Value)
            db.AddInParameter(dbcmd, "@OrganType", DbType.String, OrganizationLogRow.OrganType.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, OrganizationLogRow.ValidDateB.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, OrganizationLogRow.Seq.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From OrganizationLog")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal OrganizationLogRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into OrganizationLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, OrganReason, OrganType, ValidDateB, Seq, OrganID, OrganName, OrganEngName,")
            strSQL.AppendLine("    VirtualFlag, UpOrganID, DeptID, GroupType, GroupID, Boss, BossCompID, BossType, BossTemporary,")
            strSQL.AppendLine("    SecBoss, SecBossCompID, InValidFlag, ReportingEnd, Depth, BranchFlag, RoleCode, PersonPart,")
            strSQL.AppendLine("    SecPersonPart, CheckPart, WorkSiteID, WorkTypeID, CostDeptID, CostType, AccountBranch,")
            strSQL.AppendLine("    PositionID, CompareFlag, FlowOrganID, DelegateFlag, OrganNo, InvoiceNo, SortOrder, OrgType,")
            strSQL.AppendLine("    BusinessType, ValidDateE, Remark, OrganNameOld, OrganEngNameOld, VirtualFlagOld, UpOrganIDOld,")
            strSQL.AppendLine("    DeptIDOld, GroupTypeOld, GroupIDOld, BossOld, BossCompIDOld, BossTypeOld, BossTemporaryOld,")
            strSQL.AppendLine("    SecBossOld, SecBossCompIDOld, InValidFlagOld, ReportingEndOld, DepthOld, BranchFlagOld,")
            strSQL.AppendLine("    RoleCodeOld, PersonPartOld, SecPersonPartOld, CheckPartOld, WorkSiteIDOld, WorkTypeIDOld,")
            strSQL.AppendLine("    CostDeptIDOld, CostTypeOld, AccountBranchOld, PositionIDOld, CompareFlagOld, FlowOrganIDOld,")
            strSQL.AppendLine("    DelegateFlagOld, OrganNoOld, InvoiceNoOld, SortOrderOld, OrgTypeOld, BusinessTypeOld,")
            strSQL.AppendLine("    LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @OrganReason, @OrganType, @ValidDateB, @Seq, @OrganID, @OrganName, @OrganEngName,")
            strSQL.AppendLine("    @VirtualFlag, @UpOrganID, @DeptID, @GroupType, @GroupID, @Boss, @BossCompID, @BossType, @BossTemporary,")
            strSQL.AppendLine("    @SecBoss, @SecBossCompID, @InValidFlag, @ReportingEnd, @Depth, @BranchFlag, @RoleCode, @PersonPart,")
            strSQL.AppendLine("    @SecPersonPart, @CheckPart, @WorkSiteID, @WorkTypeID, @CostDeptID, @CostType, @AccountBranch,")
            strSQL.AppendLine("    @PositionID, @CompareFlag, @FlowOrganID, @DelegateFlag, @OrganNo, @InvoiceNo, @SortOrder, @OrgType,")
            strSQL.AppendLine("    @BusinessType, @ValidDateE, @Remark, @OrganNameOld, @OrganEngNameOld, @VirtualFlagOld, @UpOrganIDOld,")
            strSQL.AppendLine("    @DeptIDOld, @GroupTypeOld, @GroupIDOld, @BossOld, @BossCompIDOld, @BossTypeOld, @BossTemporaryOld,")
            strSQL.AppendLine("    @SecBossOld, @SecBossCompIDOld, @InValidFlagOld, @ReportingEndOld, @DepthOld, @BranchFlagOld,")
            strSQL.AppendLine("    @RoleCodeOld, @PersonPartOld, @SecPersonPartOld, @CheckPartOld, @WorkSiteIDOld, @WorkTypeIDOld,")
            strSQL.AppendLine("    @CostDeptIDOld, @CostTypeOld, @AccountBranchOld, @PositionIDOld, @CompareFlagOld, @FlowOrganIDOld,")
            strSQL.AppendLine("    @DelegateFlagOld, @OrganNoOld, @InvoiceNoOld, @SortOrderOld, @OrgTypeOld, @BusinessTypeOld,")
            strSQL.AppendLine("    @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, OrganizationLogRow.CompID.Value)
            db.AddInParameter(dbcmd, "@OrganReason", DbType.String, OrganizationLogRow.OrganReason.Value)
            db.AddInParameter(dbcmd, "@OrganType", DbType.String, OrganizationLogRow.OrganType.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(OrganizationLogRow.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), OrganizationLogRow.ValidDateB.Value))
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, OrganizationLogRow.Seq.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationLogRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@OrganName", DbType.String, OrganizationLogRow.OrganName.Value)
            db.AddInParameter(dbcmd, "@OrganEngName", DbType.String, OrganizationLogRow.OrganEngName.Value)
            db.AddInParameter(dbcmd, "@VirtualFlag", DbType.String, OrganizationLogRow.VirtualFlag.Value)
            db.AddInParameter(dbcmd, "@UpOrganID", DbType.String, OrganizationLogRow.UpOrganID.Value)
            db.AddInParameter(dbcmd, "@DeptID", DbType.String, OrganizationLogRow.DeptID.Value)
            db.AddInParameter(dbcmd, "@GroupType", DbType.String, OrganizationLogRow.GroupType.Value)
            db.AddInParameter(dbcmd, "@GroupID", DbType.String, OrganizationLogRow.GroupID.Value)
            db.AddInParameter(dbcmd, "@Boss", DbType.String, OrganizationLogRow.Boss.Value)
            db.AddInParameter(dbcmd, "@BossCompID", DbType.String, OrganizationLogRow.BossCompID.Value)
            db.AddInParameter(dbcmd, "@BossType", DbType.String, OrganizationLogRow.BossType.Value)
            db.AddInParameter(dbcmd, "@BossTemporary", DbType.String, OrganizationLogRow.BossTemporary.Value)
            db.AddInParameter(dbcmd, "@SecBoss", DbType.String, OrganizationLogRow.SecBoss.Value)
            db.AddInParameter(dbcmd, "@SecBossCompID", DbType.String, OrganizationLogRow.SecBossCompID.Value)
            db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, OrganizationLogRow.InValidFlag.Value)
            db.AddInParameter(dbcmd, "@ReportingEnd", DbType.String, OrganizationLogRow.ReportingEnd.Value)
            db.AddInParameter(dbcmd, "@Depth", DbType.String, OrganizationLogRow.Depth.Value)
            db.AddInParameter(dbcmd, "@BranchFlag", DbType.String, OrganizationLogRow.BranchFlag.Value)
            db.AddInParameter(dbcmd, "@RoleCode", DbType.String, OrganizationLogRow.RoleCode.Value)
            db.AddInParameter(dbcmd, "@PersonPart", DbType.String, OrganizationLogRow.PersonPart.Value)
            db.AddInParameter(dbcmd, "@SecPersonPart", DbType.String, OrganizationLogRow.SecPersonPart.Value)
            db.AddInParameter(dbcmd, "@CheckPart", DbType.String, OrganizationLogRow.CheckPart.Value)
            db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, OrganizationLogRow.WorkSiteID.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, OrganizationLogRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@CostDeptID", DbType.String, OrganizationLogRow.CostDeptID.Value)
            db.AddInParameter(dbcmd, "@CostType", DbType.String, OrganizationLogRow.CostType.Value)
            db.AddInParameter(dbcmd, "@AccountBranch", DbType.String, OrganizationLogRow.AccountBranch.Value)
            db.AddInParameter(dbcmd, "@PositionID", DbType.String, OrganizationLogRow.PositionID.Value)
            db.AddInParameter(dbcmd, "@CompareFlag", DbType.String, OrganizationLogRow.CompareFlag.Value)
            db.AddInParameter(dbcmd, "@FlowOrganID", DbType.String, OrganizationLogRow.FlowOrganID.Value)
            db.AddInParameter(dbcmd, "@DelegateFlag", DbType.String, OrganizationLogRow.DelegateFlag.Value)
            db.AddInParameter(dbcmd, "@OrganNo", DbType.String, OrganizationLogRow.OrganNo.Value)
            db.AddInParameter(dbcmd, "@InvoiceNo", DbType.String, OrganizationLogRow.InvoiceNo.Value)
            db.AddInParameter(dbcmd, "@SortOrder", DbType.String, OrganizationLogRow.SortOrder.Value)
            db.AddInParameter(dbcmd, "@OrgType", DbType.String, OrganizationLogRow.OrgType.Value)
            db.AddInParameter(dbcmd, "@BusinessType", DbType.String, OrganizationLogRow.BusinessType.Value)
            db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(OrganizationLogRow.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), OrganizationLogRow.ValidDateE.Value))
            db.AddInParameter(dbcmd, "@Remark", DbType.String, OrganizationLogRow.Remark.Value)
            db.AddInParameter(dbcmd, "@OrganNameOld", DbType.String, OrganizationLogRow.OrganNameOld.Value)
            db.AddInParameter(dbcmd, "@OrganEngNameOld", DbType.String, OrganizationLogRow.OrganEngNameOld.Value)
            db.AddInParameter(dbcmd, "@VirtualFlagOld", DbType.String, OrganizationLogRow.VirtualFlagOld.Value)
            db.AddInParameter(dbcmd, "@UpOrganIDOld", DbType.String, OrganizationLogRow.UpOrganIDOld.Value)
            db.AddInParameter(dbcmd, "@DeptIDOld", DbType.String, OrganizationLogRow.DeptIDOld.Value)
            db.AddInParameter(dbcmd, "@GroupTypeOld", DbType.String, OrganizationLogRow.GroupTypeOld.Value)
            db.AddInParameter(dbcmd, "@GroupIDOld", DbType.String, OrganizationLogRow.GroupIDOld.Value)
            db.AddInParameter(dbcmd, "@BossOld", DbType.String, OrganizationLogRow.BossOld.Value)
            db.AddInParameter(dbcmd, "@BossCompIDOld", DbType.String, OrganizationLogRow.BossCompIDOld.Value)
            db.AddInParameter(dbcmd, "@BossTypeOld", DbType.String, OrganizationLogRow.BossTypeOld.Value)
            db.AddInParameter(dbcmd, "@BossTemporaryOld", DbType.String, OrganizationLogRow.BossTemporaryOld.Value)
            db.AddInParameter(dbcmd, "@SecBossOld", DbType.String, OrganizationLogRow.SecBossOld.Value)
            db.AddInParameter(dbcmd, "@SecBossCompIDOld", DbType.String, OrganizationLogRow.SecBossCompIDOld.Value)
            db.AddInParameter(dbcmd, "@InValidFlagOld", DbType.String, OrganizationLogRow.InValidFlagOld.Value)
            db.AddInParameter(dbcmd, "@ReportingEndOld", DbType.String, OrganizationLogRow.ReportingEndOld.Value)
            db.AddInParameter(dbcmd, "@DepthOld", DbType.String, OrganizationLogRow.DepthOld.Value)
            db.AddInParameter(dbcmd, "@BranchFlagOld", DbType.String, OrganizationLogRow.BranchFlagOld.Value)
            db.AddInParameter(dbcmd, "@RoleCodeOld", DbType.String, OrganizationLogRow.RoleCodeOld.Value)
            db.AddInParameter(dbcmd, "@PersonPartOld", DbType.String, OrganizationLogRow.PersonPartOld.Value)
            db.AddInParameter(dbcmd, "@SecPersonPartOld", DbType.String, OrganizationLogRow.SecPersonPartOld.Value)
            db.AddInParameter(dbcmd, "@CheckPartOld", DbType.String, OrganizationLogRow.CheckPartOld.Value)
            db.AddInParameter(dbcmd, "@WorkSiteIDOld", DbType.String, OrganizationLogRow.WorkSiteIDOld.Value)
            db.AddInParameter(dbcmd, "@WorkTypeIDOld", DbType.String, OrganizationLogRow.WorkTypeIDOld.Value)
            db.AddInParameter(dbcmd, "@CostDeptIDOld", DbType.String, OrganizationLogRow.CostDeptIDOld.Value)
            db.AddInParameter(dbcmd, "@CostTypeOld", DbType.String, OrganizationLogRow.CostTypeOld.Value)
            db.AddInParameter(dbcmd, "@AccountBranchOld", DbType.String, OrganizationLogRow.AccountBranchOld.Value)
            db.AddInParameter(dbcmd, "@PositionIDOld", DbType.String, OrganizationLogRow.PositionIDOld.Value)
            db.AddInParameter(dbcmd, "@CompareFlagOld", DbType.String, OrganizationLogRow.CompareFlagOld.Value)
            db.AddInParameter(dbcmd, "@FlowOrganIDOld", DbType.String, OrganizationLogRow.FlowOrganIDOld.Value)
            db.AddInParameter(dbcmd, "@DelegateFlagOld", DbType.String, OrganizationLogRow.DelegateFlagOld.Value)
            db.AddInParameter(dbcmd, "@OrganNoOld", DbType.String, OrganizationLogRow.OrganNoOld.Value)
            db.AddInParameter(dbcmd, "@InvoiceNoOld", DbType.String, OrganizationLogRow.InvoiceNoOld.Value)
            db.AddInParameter(dbcmd, "@SortOrderOld", DbType.String, OrganizationLogRow.SortOrderOld.Value)
            db.AddInParameter(dbcmd, "@OrgTypeOld", DbType.String, OrganizationLogRow.OrgTypeOld.Value)
            db.AddInParameter(dbcmd, "@BusinessTypeOld", DbType.String, OrganizationLogRow.BusinessTypeOld.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, OrganizationLogRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, OrganizationLogRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(OrganizationLogRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), OrganizationLogRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal OrganizationLogRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into OrganizationLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, OrganReason, OrganType, ValidDateB, Seq, OrganID, OrganName, OrganEngName,")
            strSQL.AppendLine("    VirtualFlag, UpOrganID, DeptID, GroupType, GroupID, Boss, BossCompID, BossType, BossTemporary,")
            strSQL.AppendLine("    SecBoss, SecBossCompID, InValidFlag, ReportingEnd, Depth, BranchFlag, RoleCode, PersonPart,")
            strSQL.AppendLine("    SecPersonPart, CheckPart, WorkSiteID, WorkTypeID, CostDeptID, CostType, AccountBranch,")
            strSQL.AppendLine("    PositionID, CompareFlag, FlowOrganID, DelegateFlag, OrganNo, InvoiceNo, SortOrder, OrgType,")
            strSQL.AppendLine("    BusinessType, ValidDateE, Remark, OrganNameOld, OrganEngNameOld, VirtualFlagOld, UpOrganIDOld,")
            strSQL.AppendLine("    DeptIDOld, GroupTypeOld, GroupIDOld, BossOld, BossCompIDOld, BossTypeOld, BossTemporaryOld,")
            strSQL.AppendLine("    SecBossOld, SecBossCompIDOld, InValidFlagOld, ReportingEndOld, DepthOld, BranchFlagOld,")
            strSQL.AppendLine("    RoleCodeOld, PersonPartOld, SecPersonPartOld, CheckPartOld, WorkSiteIDOld, WorkTypeIDOld,")
            strSQL.AppendLine("    CostDeptIDOld, CostTypeOld, AccountBranchOld, PositionIDOld, CompareFlagOld, FlowOrganIDOld,")
            strSQL.AppendLine("    DelegateFlagOld, OrganNoOld, InvoiceNoOld, SortOrderOld, OrgTypeOld, BusinessTypeOld,")
            strSQL.AppendLine("    LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @OrganReason, @OrganType, @ValidDateB, @Seq, @OrganID, @OrganName, @OrganEngName,")
            strSQL.AppendLine("    @VirtualFlag, @UpOrganID, @DeptID, @GroupType, @GroupID, @Boss, @BossCompID, @BossType, @BossTemporary,")
            strSQL.AppendLine("    @SecBoss, @SecBossCompID, @InValidFlag, @ReportingEnd, @Depth, @BranchFlag, @RoleCode, @PersonPart,")
            strSQL.AppendLine("    @SecPersonPart, @CheckPart, @WorkSiteID, @WorkTypeID, @CostDeptID, @CostType, @AccountBranch,")
            strSQL.AppendLine("    @PositionID, @CompareFlag, @FlowOrganID, @DelegateFlag, @OrganNo, @InvoiceNo, @SortOrder, @OrgType,")
            strSQL.AppendLine("    @BusinessType, @ValidDateE, @Remark, @OrganNameOld, @OrganEngNameOld, @VirtualFlagOld, @UpOrganIDOld,")
            strSQL.AppendLine("    @DeptIDOld, @GroupTypeOld, @GroupIDOld, @BossOld, @BossCompIDOld, @BossTypeOld, @BossTemporaryOld,")
            strSQL.AppendLine("    @SecBossOld, @SecBossCompIDOld, @InValidFlagOld, @ReportingEndOld, @DepthOld, @BranchFlagOld,")
            strSQL.AppendLine("    @RoleCodeOld, @PersonPartOld, @SecPersonPartOld, @CheckPartOld, @WorkSiteIDOld, @WorkTypeIDOld,")
            strSQL.AppendLine("    @CostDeptIDOld, @CostTypeOld, @AccountBranchOld, @PositionIDOld, @CompareFlagOld, @FlowOrganIDOld,")
            strSQL.AppendLine("    @DelegateFlagOld, @OrganNoOld, @InvoiceNoOld, @SortOrderOld, @OrgTypeOld, @BusinessTypeOld,")
            strSQL.AppendLine("    @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, OrganizationLogRow.CompID.Value)
            db.AddInParameter(dbcmd, "@OrganReason", DbType.String, OrganizationLogRow.OrganReason.Value)
            db.AddInParameter(dbcmd, "@OrganType", DbType.String, OrganizationLogRow.OrganType.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(OrganizationLogRow.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), OrganizationLogRow.ValidDateB.Value))
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, OrganizationLogRow.Seq.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationLogRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@OrganName", DbType.String, OrganizationLogRow.OrganName.Value)
            db.AddInParameter(dbcmd, "@OrganEngName", DbType.String, OrganizationLogRow.OrganEngName.Value)
            db.AddInParameter(dbcmd, "@VirtualFlag", DbType.String, OrganizationLogRow.VirtualFlag.Value)
            db.AddInParameter(dbcmd, "@UpOrganID", DbType.String, OrganizationLogRow.UpOrganID.Value)
            db.AddInParameter(dbcmd, "@DeptID", DbType.String, OrganizationLogRow.DeptID.Value)
            db.AddInParameter(dbcmd, "@GroupType", DbType.String, OrganizationLogRow.GroupType.Value)
            db.AddInParameter(dbcmd, "@GroupID", DbType.String, OrganizationLogRow.GroupID.Value)
            db.AddInParameter(dbcmd, "@Boss", DbType.String, OrganizationLogRow.Boss.Value)
            db.AddInParameter(dbcmd, "@BossCompID", DbType.String, OrganizationLogRow.BossCompID.Value)
            db.AddInParameter(dbcmd, "@BossType", DbType.String, OrganizationLogRow.BossType.Value)
            db.AddInParameter(dbcmd, "@BossTemporary", DbType.String, OrganizationLogRow.BossTemporary.Value)
            db.AddInParameter(dbcmd, "@SecBoss", DbType.String, OrganizationLogRow.SecBoss.Value)
            db.AddInParameter(dbcmd, "@SecBossCompID", DbType.String, OrganizationLogRow.SecBossCompID.Value)
            db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, OrganizationLogRow.InValidFlag.Value)
            db.AddInParameter(dbcmd, "@ReportingEnd", DbType.String, OrganizationLogRow.ReportingEnd.Value)
            db.AddInParameter(dbcmd, "@Depth", DbType.String, OrganizationLogRow.Depth.Value)
            db.AddInParameter(dbcmd, "@BranchFlag", DbType.String, OrganizationLogRow.BranchFlag.Value)
            db.AddInParameter(dbcmd, "@RoleCode", DbType.String, OrganizationLogRow.RoleCode.Value)
            db.AddInParameter(dbcmd, "@PersonPart", DbType.String, OrganizationLogRow.PersonPart.Value)
            db.AddInParameter(dbcmd, "@SecPersonPart", DbType.String, OrganizationLogRow.SecPersonPart.Value)
            db.AddInParameter(dbcmd, "@CheckPart", DbType.String, OrganizationLogRow.CheckPart.Value)
            db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, OrganizationLogRow.WorkSiteID.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, OrganizationLogRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@CostDeptID", DbType.String, OrganizationLogRow.CostDeptID.Value)
            db.AddInParameter(dbcmd, "@CostType", DbType.String, OrganizationLogRow.CostType.Value)
            db.AddInParameter(dbcmd, "@AccountBranch", DbType.String, OrganizationLogRow.AccountBranch.Value)
            db.AddInParameter(dbcmd, "@PositionID", DbType.String, OrganizationLogRow.PositionID.Value)
            db.AddInParameter(dbcmd, "@CompareFlag", DbType.String, OrganizationLogRow.CompareFlag.Value)
            db.AddInParameter(dbcmd, "@FlowOrganID", DbType.String, OrganizationLogRow.FlowOrganID.Value)
            db.AddInParameter(dbcmd, "@DelegateFlag", DbType.String, OrganizationLogRow.DelegateFlag.Value)
            db.AddInParameter(dbcmd, "@OrganNo", DbType.String, OrganizationLogRow.OrganNo.Value)
            db.AddInParameter(dbcmd, "@InvoiceNo", DbType.String, OrganizationLogRow.InvoiceNo.Value)
            db.AddInParameter(dbcmd, "@SortOrder", DbType.String, OrganizationLogRow.SortOrder.Value)
            db.AddInParameter(dbcmd, "@OrgType", DbType.String, OrganizationLogRow.OrgType.Value)
            db.AddInParameter(dbcmd, "@BusinessType", DbType.String, OrganizationLogRow.BusinessType.Value)
            db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(OrganizationLogRow.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), OrganizationLogRow.ValidDateE.Value))
            db.AddInParameter(dbcmd, "@Remark", DbType.String, OrganizationLogRow.Remark.Value)
            db.AddInParameter(dbcmd, "@OrganNameOld", DbType.String, OrganizationLogRow.OrganNameOld.Value)
            db.AddInParameter(dbcmd, "@OrganEngNameOld", DbType.String, OrganizationLogRow.OrganEngNameOld.Value)
            db.AddInParameter(dbcmd, "@VirtualFlagOld", DbType.String, OrganizationLogRow.VirtualFlagOld.Value)
            db.AddInParameter(dbcmd, "@UpOrganIDOld", DbType.String, OrganizationLogRow.UpOrganIDOld.Value)
            db.AddInParameter(dbcmd, "@DeptIDOld", DbType.String, OrganizationLogRow.DeptIDOld.Value)
            db.AddInParameter(dbcmd, "@GroupTypeOld", DbType.String, OrganizationLogRow.GroupTypeOld.Value)
            db.AddInParameter(dbcmd, "@GroupIDOld", DbType.String, OrganizationLogRow.GroupIDOld.Value)
            db.AddInParameter(dbcmd, "@BossOld", DbType.String, OrganizationLogRow.BossOld.Value)
            db.AddInParameter(dbcmd, "@BossCompIDOld", DbType.String, OrganizationLogRow.BossCompIDOld.Value)
            db.AddInParameter(dbcmd, "@BossTypeOld", DbType.String, OrganizationLogRow.BossTypeOld.Value)
            db.AddInParameter(dbcmd, "@BossTemporaryOld", DbType.String, OrganizationLogRow.BossTemporaryOld.Value)
            db.AddInParameter(dbcmd, "@SecBossOld", DbType.String, OrganizationLogRow.SecBossOld.Value)
            db.AddInParameter(dbcmd, "@SecBossCompIDOld", DbType.String, OrganizationLogRow.SecBossCompIDOld.Value)
            db.AddInParameter(dbcmd, "@InValidFlagOld", DbType.String, OrganizationLogRow.InValidFlagOld.Value)
            db.AddInParameter(dbcmd, "@ReportingEndOld", DbType.String, OrganizationLogRow.ReportingEndOld.Value)
            db.AddInParameter(dbcmd, "@DepthOld", DbType.String, OrganizationLogRow.DepthOld.Value)
            db.AddInParameter(dbcmd, "@BranchFlagOld", DbType.String, OrganizationLogRow.BranchFlagOld.Value)
            db.AddInParameter(dbcmd, "@RoleCodeOld", DbType.String, OrganizationLogRow.RoleCodeOld.Value)
            db.AddInParameter(dbcmd, "@PersonPartOld", DbType.String, OrganizationLogRow.PersonPartOld.Value)
            db.AddInParameter(dbcmd, "@SecPersonPartOld", DbType.String, OrganizationLogRow.SecPersonPartOld.Value)
            db.AddInParameter(dbcmd, "@CheckPartOld", DbType.String, OrganizationLogRow.CheckPartOld.Value)
            db.AddInParameter(dbcmd, "@WorkSiteIDOld", DbType.String, OrganizationLogRow.WorkSiteIDOld.Value)
            db.AddInParameter(dbcmd, "@WorkTypeIDOld", DbType.String, OrganizationLogRow.WorkTypeIDOld.Value)
            db.AddInParameter(dbcmd, "@CostDeptIDOld", DbType.String, OrganizationLogRow.CostDeptIDOld.Value)
            db.AddInParameter(dbcmd, "@CostTypeOld", DbType.String, OrganizationLogRow.CostTypeOld.Value)
            db.AddInParameter(dbcmd, "@AccountBranchOld", DbType.String, OrganizationLogRow.AccountBranchOld.Value)
            db.AddInParameter(dbcmd, "@PositionIDOld", DbType.String, OrganizationLogRow.PositionIDOld.Value)
            db.AddInParameter(dbcmd, "@CompareFlagOld", DbType.String, OrganizationLogRow.CompareFlagOld.Value)
            db.AddInParameter(dbcmd, "@FlowOrganIDOld", DbType.String, OrganizationLogRow.FlowOrganIDOld.Value)
            db.AddInParameter(dbcmd, "@DelegateFlagOld", DbType.String, OrganizationLogRow.DelegateFlagOld.Value)
            db.AddInParameter(dbcmd, "@OrganNoOld", DbType.String, OrganizationLogRow.OrganNoOld.Value)
            db.AddInParameter(dbcmd, "@InvoiceNoOld", DbType.String, OrganizationLogRow.InvoiceNoOld.Value)
            db.AddInParameter(dbcmd, "@SortOrderOld", DbType.String, OrganizationLogRow.SortOrderOld.Value)
            db.AddInParameter(dbcmd, "@OrgTypeOld", DbType.String, OrganizationLogRow.OrgTypeOld.Value)
            db.AddInParameter(dbcmd, "@BusinessTypeOld", DbType.String, OrganizationLogRow.BusinessTypeOld.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, OrganizationLogRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, OrganizationLogRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(OrganizationLogRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), OrganizationLogRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal OrganizationLogRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into OrganizationLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, OrganReason, OrganType, ValidDateB, Seq, OrganID, OrganName, OrganEngName,")
            strSQL.AppendLine("    VirtualFlag, UpOrganID, DeptID, GroupType, GroupID, Boss, BossCompID, BossType, BossTemporary,")
            strSQL.AppendLine("    SecBoss, SecBossCompID, InValidFlag, ReportingEnd, Depth, BranchFlag, RoleCode, PersonPart,")
            strSQL.AppendLine("    SecPersonPart, CheckPart, WorkSiteID, WorkTypeID, CostDeptID, CostType, AccountBranch,")
            strSQL.AppendLine("    PositionID, CompareFlag, FlowOrganID, DelegateFlag, OrganNo, InvoiceNo, SortOrder, OrgType,")
            strSQL.AppendLine("    BusinessType, ValidDateE, Remark, OrganNameOld, OrganEngNameOld, VirtualFlagOld, UpOrganIDOld,")
            strSQL.AppendLine("    DeptIDOld, GroupTypeOld, GroupIDOld, BossOld, BossCompIDOld, BossTypeOld, BossTemporaryOld,")
            strSQL.AppendLine("    SecBossOld, SecBossCompIDOld, InValidFlagOld, ReportingEndOld, DepthOld, BranchFlagOld,")
            strSQL.AppendLine("    RoleCodeOld, PersonPartOld, SecPersonPartOld, CheckPartOld, WorkSiteIDOld, WorkTypeIDOld,")
            strSQL.AppendLine("    CostDeptIDOld, CostTypeOld, AccountBranchOld, PositionIDOld, CompareFlagOld, FlowOrganIDOld,")
            strSQL.AppendLine("    DelegateFlagOld, OrganNoOld, InvoiceNoOld, SortOrderOld, OrgTypeOld, BusinessTypeOld,")
            strSQL.AppendLine("    LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @OrganReason, @OrganType, @ValidDateB, @Seq, @OrganID, @OrganName, @OrganEngName,")
            strSQL.AppendLine("    @VirtualFlag, @UpOrganID, @DeptID, @GroupType, @GroupID, @Boss, @BossCompID, @BossType, @BossTemporary,")
            strSQL.AppendLine("    @SecBoss, @SecBossCompID, @InValidFlag, @ReportingEnd, @Depth, @BranchFlag, @RoleCode, @PersonPart,")
            strSQL.AppendLine("    @SecPersonPart, @CheckPart, @WorkSiteID, @WorkTypeID, @CostDeptID, @CostType, @AccountBranch,")
            strSQL.AppendLine("    @PositionID, @CompareFlag, @FlowOrganID, @DelegateFlag, @OrganNo, @InvoiceNo, @SortOrder, @OrgType,")
            strSQL.AppendLine("    @BusinessType, @ValidDateE, @Remark, @OrganNameOld, @OrganEngNameOld, @VirtualFlagOld, @UpOrganIDOld,")
            strSQL.AppendLine("    @DeptIDOld, @GroupTypeOld, @GroupIDOld, @BossOld, @BossCompIDOld, @BossTypeOld, @BossTemporaryOld,")
            strSQL.AppendLine("    @SecBossOld, @SecBossCompIDOld, @InValidFlagOld, @ReportingEndOld, @DepthOld, @BranchFlagOld,")
            strSQL.AppendLine("    @RoleCodeOld, @PersonPartOld, @SecPersonPartOld, @CheckPartOld, @WorkSiteIDOld, @WorkTypeIDOld,")
            strSQL.AppendLine("    @CostDeptIDOld, @CostTypeOld, @AccountBranchOld, @PositionIDOld, @CompareFlagOld, @FlowOrganIDOld,")
            strSQL.AppendLine("    @DelegateFlagOld, @OrganNoOld, @InvoiceNoOld, @SortOrderOld, @OrgTypeOld, @BusinessTypeOld,")
            strSQL.AppendLine("    @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In OrganizationLogRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@OrganReason", DbType.String, r.OrganReason.Value)
                        db.AddInParameter(dbcmd, "@OrganType", DbType.String, r.OrganType.Value)
                        db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(r.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateB.Value))
                        db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
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
                        db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(r.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateE.Value))
                        db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                        db.AddInParameter(dbcmd, "@OrganNameOld", DbType.String, r.OrganNameOld.Value)
                        db.AddInParameter(dbcmd, "@OrganEngNameOld", DbType.String, r.OrganEngNameOld.Value)
                        db.AddInParameter(dbcmd, "@VirtualFlagOld", DbType.String, r.VirtualFlagOld.Value)
                        db.AddInParameter(dbcmd, "@UpOrganIDOld", DbType.String, r.UpOrganIDOld.Value)
                        db.AddInParameter(dbcmd, "@DeptIDOld", DbType.String, r.DeptIDOld.Value)
                        db.AddInParameter(dbcmd, "@GroupTypeOld", DbType.String, r.GroupTypeOld.Value)
                        db.AddInParameter(dbcmd, "@GroupIDOld", DbType.String, r.GroupIDOld.Value)
                        db.AddInParameter(dbcmd, "@BossOld", DbType.String, r.BossOld.Value)
                        db.AddInParameter(dbcmd, "@BossCompIDOld", DbType.String, r.BossCompIDOld.Value)
                        db.AddInParameter(dbcmd, "@BossTypeOld", DbType.String, r.BossTypeOld.Value)
                        db.AddInParameter(dbcmd, "@BossTemporaryOld", DbType.String, r.BossTemporaryOld.Value)
                        db.AddInParameter(dbcmd, "@SecBossOld", DbType.String, r.SecBossOld.Value)
                        db.AddInParameter(dbcmd, "@SecBossCompIDOld", DbType.String, r.SecBossCompIDOld.Value)
                        db.AddInParameter(dbcmd, "@InValidFlagOld", DbType.String, r.InValidFlagOld.Value)
                        db.AddInParameter(dbcmd, "@ReportingEndOld", DbType.String, r.ReportingEndOld.Value)
                        db.AddInParameter(dbcmd, "@DepthOld", DbType.String, r.DepthOld.Value)
                        db.AddInParameter(dbcmd, "@BranchFlagOld", DbType.String, r.BranchFlagOld.Value)
                        db.AddInParameter(dbcmd, "@RoleCodeOld", DbType.String, r.RoleCodeOld.Value)
                        db.AddInParameter(dbcmd, "@PersonPartOld", DbType.String, r.PersonPartOld.Value)
                        db.AddInParameter(dbcmd, "@SecPersonPartOld", DbType.String, r.SecPersonPartOld.Value)
                        db.AddInParameter(dbcmd, "@CheckPartOld", DbType.String, r.CheckPartOld.Value)
                        db.AddInParameter(dbcmd, "@WorkSiteIDOld", DbType.String, r.WorkSiteIDOld.Value)
                        db.AddInParameter(dbcmd, "@WorkTypeIDOld", DbType.String, r.WorkTypeIDOld.Value)
                        db.AddInParameter(dbcmd, "@CostDeptIDOld", DbType.String, r.CostDeptIDOld.Value)
                        db.AddInParameter(dbcmd, "@CostTypeOld", DbType.String, r.CostTypeOld.Value)
                        db.AddInParameter(dbcmd, "@AccountBranchOld", DbType.String, r.AccountBranchOld.Value)
                        db.AddInParameter(dbcmd, "@PositionIDOld", DbType.String, r.PositionIDOld.Value)
                        db.AddInParameter(dbcmd, "@CompareFlagOld", DbType.String, r.CompareFlagOld.Value)
                        db.AddInParameter(dbcmd, "@FlowOrganIDOld", DbType.String, r.FlowOrganIDOld.Value)
                        db.AddInParameter(dbcmd, "@DelegateFlagOld", DbType.String, r.DelegateFlagOld.Value)
                        db.AddInParameter(dbcmd, "@OrganNoOld", DbType.String, r.OrganNoOld.Value)
                        db.AddInParameter(dbcmd, "@InvoiceNoOld", DbType.String, r.InvoiceNoOld.Value)
                        db.AddInParameter(dbcmd, "@SortOrderOld", DbType.String, r.SortOrderOld.Value)
                        db.AddInParameter(dbcmd, "@OrgTypeOld", DbType.String, r.OrgTypeOld.Value)
                        db.AddInParameter(dbcmd, "@BusinessTypeOld", DbType.String, r.BusinessTypeOld.Value)
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

        Public Function Insert(ByVal OrganizationLogRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into OrganizationLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, OrganReason, OrganType, ValidDateB, Seq, OrganID, OrganName, OrganEngName,")
            strSQL.AppendLine("    VirtualFlag, UpOrganID, DeptID, GroupType, GroupID, Boss, BossCompID, BossType, BossTemporary,")
            strSQL.AppendLine("    SecBoss, SecBossCompID, InValidFlag, ReportingEnd, Depth, BranchFlag, RoleCode, PersonPart,")
            strSQL.AppendLine("    SecPersonPart, CheckPart, WorkSiteID, WorkTypeID, CostDeptID, CostType, AccountBranch,")
            strSQL.AppendLine("    PositionID, CompareFlag, FlowOrganID, DelegateFlag, OrganNo, InvoiceNo, SortOrder, OrgType,")
            strSQL.AppendLine("    BusinessType, ValidDateE, Remark, OrganNameOld, OrganEngNameOld, VirtualFlagOld, UpOrganIDOld,")
            strSQL.AppendLine("    DeptIDOld, GroupTypeOld, GroupIDOld, BossOld, BossCompIDOld, BossTypeOld, BossTemporaryOld,")
            strSQL.AppendLine("    SecBossOld, SecBossCompIDOld, InValidFlagOld, ReportingEndOld, DepthOld, BranchFlagOld,")
            strSQL.AppendLine("    RoleCodeOld, PersonPartOld, SecPersonPartOld, CheckPartOld, WorkSiteIDOld, WorkTypeIDOld,")
            strSQL.AppendLine("    CostDeptIDOld, CostTypeOld, AccountBranchOld, PositionIDOld, CompareFlagOld, FlowOrganIDOld,")
            strSQL.AppendLine("    DelegateFlagOld, OrganNoOld, InvoiceNoOld, SortOrderOld, OrgTypeOld, BusinessTypeOld,")
            strSQL.AppendLine("    LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @OrganReason, @OrganType, @ValidDateB, @Seq, @OrganID, @OrganName, @OrganEngName,")
            strSQL.AppendLine("    @VirtualFlag, @UpOrganID, @DeptID, @GroupType, @GroupID, @Boss, @BossCompID, @BossType, @BossTemporary,")
            strSQL.AppendLine("    @SecBoss, @SecBossCompID, @InValidFlag, @ReportingEnd, @Depth, @BranchFlag, @RoleCode, @PersonPart,")
            strSQL.AppendLine("    @SecPersonPart, @CheckPart, @WorkSiteID, @WorkTypeID, @CostDeptID, @CostType, @AccountBranch,")
            strSQL.AppendLine("    @PositionID, @CompareFlag, @FlowOrganID, @DelegateFlag, @OrganNo, @InvoiceNo, @SortOrder, @OrgType,")
            strSQL.AppendLine("    @BusinessType, @ValidDateE, @Remark, @OrganNameOld, @OrganEngNameOld, @VirtualFlagOld, @UpOrganIDOld,")
            strSQL.AppendLine("    @DeptIDOld, @GroupTypeOld, @GroupIDOld, @BossOld, @BossCompIDOld, @BossTypeOld, @BossTemporaryOld,")
            strSQL.AppendLine("    @SecBossOld, @SecBossCompIDOld, @InValidFlagOld, @ReportingEndOld, @DepthOld, @BranchFlagOld,")
            strSQL.AppendLine("    @RoleCodeOld, @PersonPartOld, @SecPersonPartOld, @CheckPartOld, @WorkSiteIDOld, @WorkTypeIDOld,")
            strSQL.AppendLine("    @CostDeptIDOld, @CostTypeOld, @AccountBranchOld, @PositionIDOld, @CompareFlagOld, @FlowOrganIDOld,")
            strSQL.AppendLine("    @DelegateFlagOld, @OrganNoOld, @InvoiceNoOld, @SortOrderOld, @OrgTypeOld, @BusinessTypeOld,")
            strSQL.AppendLine("    @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In OrganizationLogRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@OrganReason", DbType.String, r.OrganReason.Value)
                db.AddInParameter(dbcmd, "@OrganType", DbType.String, r.OrganType.Value)
                db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(r.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateB.Value))
                db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
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
                db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(r.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateE.Value))
                db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                db.AddInParameter(dbcmd, "@OrganNameOld", DbType.String, r.OrganNameOld.Value)
                db.AddInParameter(dbcmd, "@OrganEngNameOld", DbType.String, r.OrganEngNameOld.Value)
                db.AddInParameter(dbcmd, "@VirtualFlagOld", DbType.String, r.VirtualFlagOld.Value)
                db.AddInParameter(dbcmd, "@UpOrganIDOld", DbType.String, r.UpOrganIDOld.Value)
                db.AddInParameter(dbcmd, "@DeptIDOld", DbType.String, r.DeptIDOld.Value)
                db.AddInParameter(dbcmd, "@GroupTypeOld", DbType.String, r.GroupTypeOld.Value)
                db.AddInParameter(dbcmd, "@GroupIDOld", DbType.String, r.GroupIDOld.Value)
                db.AddInParameter(dbcmd, "@BossOld", DbType.String, r.BossOld.Value)
                db.AddInParameter(dbcmd, "@BossCompIDOld", DbType.String, r.BossCompIDOld.Value)
                db.AddInParameter(dbcmd, "@BossTypeOld", DbType.String, r.BossTypeOld.Value)
                db.AddInParameter(dbcmd, "@BossTemporaryOld", DbType.String, r.BossTemporaryOld.Value)
                db.AddInParameter(dbcmd, "@SecBossOld", DbType.String, r.SecBossOld.Value)
                db.AddInParameter(dbcmd, "@SecBossCompIDOld", DbType.String, r.SecBossCompIDOld.Value)
                db.AddInParameter(dbcmd, "@InValidFlagOld", DbType.String, r.InValidFlagOld.Value)
                db.AddInParameter(dbcmd, "@ReportingEndOld", DbType.String, r.ReportingEndOld.Value)
                db.AddInParameter(dbcmd, "@DepthOld", DbType.String, r.DepthOld.Value)
                db.AddInParameter(dbcmd, "@BranchFlagOld", DbType.String, r.BranchFlagOld.Value)
                db.AddInParameter(dbcmd, "@RoleCodeOld", DbType.String, r.RoleCodeOld.Value)
                db.AddInParameter(dbcmd, "@PersonPartOld", DbType.String, r.PersonPartOld.Value)
                db.AddInParameter(dbcmd, "@SecPersonPartOld", DbType.String, r.SecPersonPartOld.Value)
                db.AddInParameter(dbcmd, "@CheckPartOld", DbType.String, r.CheckPartOld.Value)
                db.AddInParameter(dbcmd, "@WorkSiteIDOld", DbType.String, r.WorkSiteIDOld.Value)
                db.AddInParameter(dbcmd, "@WorkTypeIDOld", DbType.String, r.WorkTypeIDOld.Value)
                db.AddInParameter(dbcmd, "@CostDeptIDOld", DbType.String, r.CostDeptIDOld.Value)
                db.AddInParameter(dbcmd, "@CostTypeOld", DbType.String, r.CostTypeOld.Value)
                db.AddInParameter(dbcmd, "@AccountBranchOld", DbType.String, r.AccountBranchOld.Value)
                db.AddInParameter(dbcmd, "@PositionIDOld", DbType.String, r.PositionIDOld.Value)
                db.AddInParameter(dbcmd, "@CompareFlagOld", DbType.String, r.CompareFlagOld.Value)
                db.AddInParameter(dbcmd, "@FlowOrganIDOld", DbType.String, r.FlowOrganIDOld.Value)
                db.AddInParameter(dbcmd, "@DelegateFlagOld", DbType.String, r.DelegateFlagOld.Value)
                db.AddInParameter(dbcmd, "@OrganNoOld", DbType.String, r.OrganNoOld.Value)
                db.AddInParameter(dbcmd, "@InvoiceNoOld", DbType.String, r.InvoiceNoOld.Value)
                db.AddInParameter(dbcmd, "@SortOrderOld", DbType.String, r.SortOrderOld.Value)
                db.AddInParameter(dbcmd, "@OrgTypeOld", DbType.String, r.OrgTypeOld.Value)
                db.AddInParameter(dbcmd, "@BusinessTypeOld", DbType.String, r.BusinessTypeOld.Value)
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

