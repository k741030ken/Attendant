﻿'****************************************************************
' Table:EmployeeLogWait
' Created Date: 2014.09.26
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beEmployeeLogWait
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "EmpID", "Wait_ValidDate", "Wait_Seq", "IDNo", "ModifyDate", "Reason", "Company", "DeptID", "DeptName" _
                                    , "OrganID", "OrganName", "GroupID", "GroupName", "FlowOrganID", "FlowOrganName", "RankID", "TitleID", "TitleName", "PositionID", "Position" _
                                    , "WorkTypeID", "WorkType", "WorkStatus", "WorkStatusName", "Remark", "BossType", "IsBoss", "IsSecBoss", "IsGroupBoss", "IsSecGroupBoss", "CompIDOld" _
                                    , "CompanyOld", "DeptIDOld", "DeptNameOld", "OrganIDOld", "OrganNameOld", "GroupIDOld", "GroupNameOld", "FlowOrganIDOld", "FlowOrganNameOld", "RankIDOld", "TitleIDOld" _
                                    , "TitleNameOld", "PositionIDOld", "PositionOld", "WorkTypeIDOld", "WorkTypeOld", "WorkStatusOld", "WorkStatusNameOld", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(Date), GetType(Integer), GetType(String), GetType(Date), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "CompID", "EmpID", "Wait_ValidDate", "Wait_Seq", "IDNo", "ModifyDate", "Reason" }

        Public ReadOnly Property Rows() As beEmployeeLogWait.Rows 
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
        Public Sub Transfer2Row(EmployeeLogWaitTable As DataTable)
            For Each dr As DataRow In EmployeeLogWaitTable.Rows
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
                dr(m_Rows(i).Wait_ValidDate.FieldName) = m_Rows(i).Wait_ValidDate.Value
                dr(m_Rows(i).Wait_Seq.FieldName) = m_Rows(i).Wait_Seq.Value
                dr(m_Rows(i).IDNo.FieldName) = m_Rows(i).IDNo.Value
                dr(m_Rows(i).ModifyDate.FieldName) = m_Rows(i).ModifyDate.Value
                dr(m_Rows(i).Reason.FieldName) = m_Rows(i).Reason.Value
                dr(m_Rows(i).Company.FieldName) = m_Rows(i).Company.Value
                dr(m_Rows(i).DeptID.FieldName) = m_Rows(i).DeptID.Value
                dr(m_Rows(i).DeptName.FieldName) = m_Rows(i).DeptName.Value
                dr(m_Rows(i).OrganID.FieldName) = m_Rows(i).OrganID.Value
                dr(m_Rows(i).OrganName.FieldName) = m_Rows(i).OrganName.Value
                dr(m_Rows(i).GroupID.FieldName) = m_Rows(i).GroupID.Value
                dr(m_Rows(i).GroupName.FieldName) = m_Rows(i).GroupName.Value
                dr(m_Rows(i).FlowOrganID.FieldName) = m_Rows(i).FlowOrganID.Value
                dr(m_Rows(i).FlowOrganName.FieldName) = m_Rows(i).FlowOrganName.Value
                dr(m_Rows(i).RankID.FieldName) = m_Rows(i).RankID.Value
                dr(m_Rows(i).TitleID.FieldName) = m_Rows(i).TitleID.Value
                dr(m_Rows(i).TitleName.FieldName) = m_Rows(i).TitleName.Value
                dr(m_Rows(i).PositionID.FieldName) = m_Rows(i).PositionID.Value
                dr(m_Rows(i).Position.FieldName) = m_Rows(i).Position.Value
                dr(m_Rows(i).WorkTypeID.FieldName) = m_Rows(i).WorkTypeID.Value
                dr(m_Rows(i).WorkType.FieldName) = m_Rows(i).WorkType.Value
                dr(m_Rows(i).WorkStatus.FieldName) = m_Rows(i).WorkStatus.Value
                dr(m_Rows(i).WorkStatusName.FieldName) = m_Rows(i).WorkStatusName.Value
                dr(m_Rows(i).Remark.FieldName) = m_Rows(i).Remark.Value
                dr(m_Rows(i).BossType.FieldName) = m_Rows(i).BossType.Value
                dr(m_Rows(i).IsBoss.FieldName) = m_Rows(i).IsBoss.Value
                dr(m_Rows(i).IsSecBoss.FieldName) = m_Rows(i).IsSecBoss.Value
                dr(m_Rows(i).IsGroupBoss.FieldName) = m_Rows(i).IsGroupBoss.Value
                dr(m_Rows(i).IsSecGroupBoss.FieldName) = m_Rows(i).IsSecGroupBoss.Value
                dr(m_Rows(i).CompIDOld.FieldName) = m_Rows(i).CompIDOld.Value
                dr(m_Rows(i).CompanyOld.FieldName) = m_Rows(i).CompanyOld.Value
                dr(m_Rows(i).DeptIDOld.FieldName) = m_Rows(i).DeptIDOld.Value
                dr(m_Rows(i).DeptNameOld.FieldName) = m_Rows(i).DeptNameOld.Value
                dr(m_Rows(i).OrganIDOld.FieldName) = m_Rows(i).OrganIDOld.Value
                dr(m_Rows(i).OrganNameOld.FieldName) = m_Rows(i).OrganNameOld.Value
                dr(m_Rows(i).GroupIDOld.FieldName) = m_Rows(i).GroupIDOld.Value
                dr(m_Rows(i).GroupNameOld.FieldName) = m_Rows(i).GroupNameOld.Value
                dr(m_Rows(i).FlowOrganIDOld.FieldName) = m_Rows(i).FlowOrganIDOld.Value
                dr(m_Rows(i).FlowOrganNameOld.FieldName) = m_Rows(i).FlowOrganNameOld.Value
                dr(m_Rows(i).RankIDOld.FieldName) = m_Rows(i).RankIDOld.Value
                dr(m_Rows(i).TitleIDOld.FieldName) = m_Rows(i).TitleIDOld.Value
                dr(m_Rows(i).TitleNameOld.FieldName) = m_Rows(i).TitleNameOld.Value
                dr(m_Rows(i).PositionIDOld.FieldName) = m_Rows(i).PositionIDOld.Value
                dr(m_Rows(i).PositionOld.FieldName) = m_Rows(i).PositionOld.Value
                dr(m_Rows(i).WorkTypeIDOld.FieldName) = m_Rows(i).WorkTypeIDOld.Value
                dr(m_Rows(i).WorkTypeOld.FieldName) = m_Rows(i).WorkTypeOld.Value
                dr(m_Rows(i).WorkStatusOld.FieldName) = m_Rows(i).WorkStatusOld.Value
                dr(m_Rows(i).WorkStatusNameOld.FieldName) = m_Rows(i).WorkStatusNameOld.Value
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

        Public Sub Add(EmployeeLogWaitRow As Row)
            m_Rows.Add(EmployeeLogWaitRow)
        End Sub

        Public Sub Remove(EmployeeLogWaitRow As Row)
            If m_Rows.IndexOf(EmployeeLogWaitRow) >= 0 Then
                m_Rows.Remove(EmployeeLogWaitRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_EmpID As Field(Of String) = new Field(Of String)("EmpID", true)
        Private FI_Wait_ValidDate As Field(Of Date) = new Field(Of Date)("Wait_ValidDate", true)
        Private FI_Wait_Seq As Field(Of Integer) = new Field(Of Integer)("Wait_Seq", true)
        Private FI_IDNo As Field(Of String) = new Field(Of String)("IDNo", true)
        Private FI_ModifyDate As Field(Of Date) = new Field(Of Date)("ModifyDate", true)
        Private FI_Reason As Field(Of String) = new Field(Of String)("Reason", true)
        Private FI_Company As Field(Of String) = new Field(Of String)("Company", true)
        Private FI_DeptID As Field(Of String) = new Field(Of String)("DeptID", true)
        Private FI_DeptName As Field(Of String) = new Field(Of String)("DeptName", true)
        Private FI_OrganID As Field(Of String) = new Field(Of String)("OrganID", true)
        Private FI_OrganName As Field(Of String) = new Field(Of String)("OrganName", true)
        Private FI_GroupID As Field(Of String) = new Field(Of String)("GroupID", true)
        Private FI_GroupName As Field(Of String) = new Field(Of String)("GroupName", true)
        Private FI_FlowOrganID As Field(Of String) = new Field(Of String)("FlowOrganID", true)
        Private FI_FlowOrganName As Field(Of String) = new Field(Of String)("FlowOrganName", true)
        Private FI_RankID As Field(Of String) = new Field(Of String)("RankID", true)
        Private FI_TitleID As Field(Of String) = new Field(Of String)("TitleID", true)
        Private FI_TitleName As Field(Of String) = new Field(Of String)("TitleName", true)
        Private FI_PositionID As Field(Of String) = new Field(Of String)("PositionID", true)
        Private FI_Position As Field(Of String) = new Field(Of String)("Position", true)
        Private FI_WorkTypeID As Field(Of String) = new Field(Of String)("WorkTypeID", true)
        Private FI_WorkType As Field(Of String) = new Field(Of String)("WorkType", true)
        Private FI_WorkStatus As Field(Of String) = new Field(Of String)("WorkStatus", true)
        Private FI_WorkStatusName As Field(Of String) = new Field(Of String)("WorkStatusName", true)
        Private FI_Remark As Field(Of String) = new Field(Of String)("Remark", true)
        Private FI_BossType As Field(Of String) = new Field(Of String)("BossType", true)
        Private FI_IsBoss As Field(Of String) = new Field(Of String)("IsBoss", true)
        Private FI_IsSecBoss As Field(Of String) = new Field(Of String)("IsSecBoss", true)
        Private FI_IsGroupBoss As Field(Of String) = new Field(Of String)("IsGroupBoss", true)
        Private FI_IsSecGroupBoss As Field(Of String) = new Field(Of String)("IsSecGroupBoss", true)
        Private FI_CompIDOld As Field(Of String) = new Field(Of String)("CompIDOld", true)
        Private FI_CompanyOld As Field(Of String) = new Field(Of String)("CompanyOld", true)
        Private FI_DeptIDOld As Field(Of String) = new Field(Of String)("DeptIDOld", true)
        Private FI_DeptNameOld As Field(Of String) = new Field(Of String)("DeptNameOld", true)
        Private FI_OrganIDOld As Field(Of String) = new Field(Of String)("OrganIDOld", true)
        Private FI_OrganNameOld As Field(Of String) = new Field(Of String)("OrganNameOld", true)
        Private FI_GroupIDOld As Field(Of String) = new Field(Of String)("GroupIDOld", true)
        Private FI_GroupNameOld As Field(Of String) = new Field(Of String)("GroupNameOld", true)
        Private FI_FlowOrganIDOld As Field(Of String) = new Field(Of String)("FlowOrganIDOld", true)
        Private FI_FlowOrganNameOld As Field(Of String) = new Field(Of String)("FlowOrganNameOld", true)
        Private FI_RankIDOld As Field(Of String) = new Field(Of String)("RankIDOld", true)
        Private FI_TitleIDOld As Field(Of String) = new Field(Of String)("TitleIDOld", true)
        Private FI_TitleNameOld As Field(Of String) = new Field(Of String)("TitleNameOld", true)
        Private FI_PositionIDOld As Field(Of String) = new Field(Of String)("PositionIDOld", true)
        Private FI_PositionOld As Field(Of String) = new Field(Of String)("PositionOld", true)
        Private FI_WorkTypeIDOld As Field(Of String) = new Field(Of String)("WorkTypeIDOld", true)
        Private FI_WorkTypeOld As Field(Of String) = new Field(Of String)("WorkTypeOld", true)
        Private FI_WorkStatusOld As Field(Of String) = new Field(Of String)("WorkStatusOld", true)
        Private FI_WorkStatusNameOld As Field(Of String) = new Field(Of String)("WorkStatusNameOld", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "CompID", "EmpID", "Wait_ValidDate", "Wait_Seq", "IDNo", "ModifyDate", "Reason", "Company", "DeptID", "DeptName" _
                                    , "OrganID", "OrganName", "GroupID", "GroupName", "FlowOrganID", "FlowOrganName", "RankID", "TitleID", "TitleName", "PositionID", "Position" _
                                    , "WorkTypeID", "WorkType", "WorkStatus", "WorkStatusName", "Remark", "BossType", "IsBoss", "IsSecBoss", "IsGroupBoss", "IsSecGroupBoss", "CompIDOld" _
                                    , "CompanyOld", "DeptIDOld", "DeptNameOld", "OrganIDOld", "OrganNameOld", "GroupIDOld", "GroupNameOld", "FlowOrganIDOld", "FlowOrganNameOld", "RankIDOld", "TitleIDOld" _
                                    , "TitleNameOld", "PositionIDOld", "PositionOld", "WorkTypeIDOld", "WorkTypeOld", "WorkStatusOld", "WorkStatusNameOld", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "CompID", "EmpID", "Wait_ValidDate", "Wait_Seq", "IDNo", "ModifyDate", "Reason" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CompID"
                    Return FI_CompID.Value
                Case "EmpID"
                    Return FI_EmpID.Value
                Case "Wait_ValidDate"
                    Return FI_Wait_ValidDate.Value
                Case "Wait_Seq"
                    Return FI_Wait_Seq.Value
                Case "IDNo"
                    Return FI_IDNo.Value
                Case "ModifyDate"
                    Return FI_ModifyDate.Value
                Case "Reason"
                    Return FI_Reason.Value
                Case "Company"
                    Return FI_Company.Value
                Case "DeptID"
                    Return FI_DeptID.Value
                Case "DeptName"
                    Return FI_DeptName.Value
                Case "OrganID"
                    Return FI_OrganID.Value
                Case "OrganName"
                    Return FI_OrganName.Value
                Case "GroupID"
                    Return FI_GroupID.Value
                Case "GroupName"
                    Return FI_GroupName.Value
                Case "FlowOrganID"
                    Return FI_FlowOrganID.Value
                Case "FlowOrganName"
                    Return FI_FlowOrganName.Value
                Case "RankID"
                    Return FI_RankID.Value
                Case "TitleID"
                    Return FI_TitleID.Value
                Case "TitleName"
                    Return FI_TitleName.Value
                Case "PositionID"
                    Return FI_PositionID.Value
                Case "Position"
                    Return FI_Position.Value
                Case "WorkTypeID"
                    Return FI_WorkTypeID.Value
                Case "WorkType"
                    Return FI_WorkType.Value
                Case "WorkStatus"
                    Return FI_WorkStatus.Value
                Case "WorkStatusName"
                    Return FI_WorkStatusName.Value
                Case "Remark"
                    Return FI_Remark.Value
                Case "BossType"
                    Return FI_BossType.Value
                Case "IsBoss"
                    Return FI_IsBoss.Value
                Case "IsSecBoss"
                    Return FI_IsSecBoss.Value
                Case "IsGroupBoss"
                    Return FI_IsGroupBoss.Value
                Case "IsSecGroupBoss"
                    Return FI_IsSecGroupBoss.Value
                Case "CompIDOld"
                    Return FI_CompIDOld.Value
                Case "CompanyOld"
                    Return FI_CompanyOld.Value
                Case "DeptIDOld"
                    Return FI_DeptIDOld.Value
                Case "DeptNameOld"
                    Return FI_DeptNameOld.Value
                Case "OrganIDOld"
                    Return FI_OrganIDOld.Value
                Case "OrganNameOld"
                    Return FI_OrganNameOld.Value
                Case "GroupIDOld"
                    Return FI_GroupIDOld.Value
                Case "GroupNameOld"
                    Return FI_GroupNameOld.Value
                Case "FlowOrganIDOld"
                    Return FI_FlowOrganIDOld.Value
                Case "FlowOrganNameOld"
                    Return FI_FlowOrganNameOld.Value
                Case "RankIDOld"
                    Return FI_RankIDOld.Value
                Case "TitleIDOld"
                    Return FI_TitleIDOld.Value
                Case "TitleNameOld"
                    Return FI_TitleNameOld.Value
                Case "PositionIDOld"
                    Return FI_PositionIDOld.Value
                Case "PositionOld"
                    Return FI_PositionOld.Value
                Case "WorkTypeIDOld"
                    Return FI_WorkTypeIDOld.Value
                Case "WorkTypeOld"
                    Return FI_WorkTypeOld.Value
                Case "WorkStatusOld"
                    Return FI_WorkStatusOld.Value
                Case "WorkStatusNameOld"
                    Return FI_WorkStatusNameOld.Value
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
                Case "Wait_ValidDate"
                    FI_Wait_ValidDate.SetValue(value)
                Case "Wait_Seq"
                    FI_Wait_Seq.SetValue(value)
                Case "IDNo"
                    FI_IDNo.SetValue(value)
                Case "ModifyDate"
                    FI_ModifyDate.SetValue(value)
                Case "Reason"
                    FI_Reason.SetValue(value)
                Case "Company"
                    FI_Company.SetValue(value)
                Case "DeptID"
                    FI_DeptID.SetValue(value)
                Case "DeptName"
                    FI_DeptName.SetValue(value)
                Case "OrganID"
                    FI_OrganID.SetValue(value)
                Case "OrganName"
                    FI_OrganName.SetValue(value)
                Case "GroupID"
                    FI_GroupID.SetValue(value)
                Case "GroupName"
                    FI_GroupName.SetValue(value)
                Case "FlowOrganID"
                    FI_FlowOrganID.SetValue(value)
                Case "FlowOrganName"
                    FI_FlowOrganName.SetValue(value)
                Case "RankID"
                    FI_RankID.SetValue(value)
                Case "TitleID"
                    FI_TitleID.SetValue(value)
                Case "TitleName"
                    FI_TitleName.SetValue(value)
                Case "PositionID"
                    FI_PositionID.SetValue(value)
                Case "Position"
                    FI_Position.SetValue(value)
                Case "WorkTypeID"
                    FI_WorkTypeID.SetValue(value)
                Case "WorkType"
                    FI_WorkType.SetValue(value)
                Case "WorkStatus"
                    FI_WorkStatus.SetValue(value)
                Case "WorkStatusName"
                    FI_WorkStatusName.SetValue(value)
                Case "Remark"
                    FI_Remark.SetValue(value)
                Case "BossType"
                    FI_BossType.SetValue(value)
                Case "IsBoss"
                    FI_IsBoss.SetValue(value)
                Case "IsSecBoss"
                    FI_IsSecBoss.SetValue(value)
                Case "IsGroupBoss"
                    FI_IsGroupBoss.SetValue(value)
                Case "IsSecGroupBoss"
                    FI_IsSecGroupBoss.SetValue(value)
                Case "CompIDOld"
                    FI_CompIDOld.SetValue(value)
                Case "CompanyOld"
                    FI_CompanyOld.SetValue(value)
                Case "DeptIDOld"
                    FI_DeptIDOld.SetValue(value)
                Case "DeptNameOld"
                    FI_DeptNameOld.SetValue(value)
                Case "OrganIDOld"
                    FI_OrganIDOld.SetValue(value)
                Case "OrganNameOld"
                    FI_OrganNameOld.SetValue(value)
                Case "GroupIDOld"
                    FI_GroupIDOld.SetValue(value)
                Case "GroupNameOld"
                    FI_GroupNameOld.SetValue(value)
                Case "FlowOrganIDOld"
                    FI_FlowOrganIDOld.SetValue(value)
                Case "FlowOrganNameOld"
                    FI_FlowOrganNameOld.SetValue(value)
                Case "RankIDOld"
                    FI_RankIDOld.SetValue(value)
                Case "TitleIDOld"
                    FI_TitleIDOld.SetValue(value)
                Case "TitleNameOld"
                    FI_TitleNameOld.SetValue(value)
                Case "PositionIDOld"
                    FI_PositionIDOld.SetValue(value)
                Case "PositionOld"
                    FI_PositionOld.SetValue(value)
                Case "WorkTypeIDOld"
                    FI_WorkTypeIDOld.SetValue(value)
                Case "WorkTypeOld"
                    FI_WorkTypeOld.SetValue(value)
                Case "WorkStatusOld"
                    FI_WorkStatusOld.SetValue(value)
                Case "WorkStatusNameOld"
                    FI_WorkStatusNameOld.SetValue(value)
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
                Case "Wait_ValidDate"
                    return FI_Wait_ValidDate.Updated
                Case "Wait_Seq"
                    return FI_Wait_Seq.Updated
                Case "IDNo"
                    return FI_IDNo.Updated
                Case "ModifyDate"
                    return FI_ModifyDate.Updated
                Case "Reason"
                    return FI_Reason.Updated
                Case "Company"
                    return FI_Company.Updated
                Case "DeptID"
                    return FI_DeptID.Updated
                Case "DeptName"
                    return FI_DeptName.Updated
                Case "OrganID"
                    return FI_OrganID.Updated
                Case "OrganName"
                    return FI_OrganName.Updated
                Case "GroupID"
                    return FI_GroupID.Updated
                Case "GroupName"
                    return FI_GroupName.Updated
                Case "FlowOrganID"
                    return FI_FlowOrganID.Updated
                Case "FlowOrganName"
                    return FI_FlowOrganName.Updated
                Case "RankID"
                    return FI_RankID.Updated
                Case "TitleID"
                    return FI_TitleID.Updated
                Case "TitleName"
                    return FI_TitleName.Updated
                Case "PositionID"
                    return FI_PositionID.Updated
                Case "Position"
                    return FI_Position.Updated
                Case "WorkTypeID"
                    return FI_WorkTypeID.Updated
                Case "WorkType"
                    return FI_WorkType.Updated
                Case "WorkStatus"
                    return FI_WorkStatus.Updated
                Case "WorkStatusName"
                    return FI_WorkStatusName.Updated
                Case "Remark"
                    return FI_Remark.Updated
                Case "BossType"
                    return FI_BossType.Updated
                Case "IsBoss"
                    return FI_IsBoss.Updated
                Case "IsSecBoss"
                    return FI_IsSecBoss.Updated
                Case "IsGroupBoss"
                    return FI_IsGroupBoss.Updated
                Case "IsSecGroupBoss"
                    return FI_IsSecGroupBoss.Updated
                Case "CompIDOld"
                    return FI_CompIDOld.Updated
                Case "CompanyOld"
                    return FI_CompanyOld.Updated
                Case "DeptIDOld"
                    return FI_DeptIDOld.Updated
                Case "DeptNameOld"
                    return FI_DeptNameOld.Updated
                Case "OrganIDOld"
                    return FI_OrganIDOld.Updated
                Case "OrganNameOld"
                    return FI_OrganNameOld.Updated
                Case "GroupIDOld"
                    return FI_GroupIDOld.Updated
                Case "GroupNameOld"
                    return FI_GroupNameOld.Updated
                Case "FlowOrganIDOld"
                    return FI_FlowOrganIDOld.Updated
                Case "FlowOrganNameOld"
                    return FI_FlowOrganNameOld.Updated
                Case "RankIDOld"
                    return FI_RankIDOld.Updated
                Case "TitleIDOld"
                    return FI_TitleIDOld.Updated
                Case "TitleNameOld"
                    return FI_TitleNameOld.Updated
                Case "PositionIDOld"
                    return FI_PositionIDOld.Updated
                Case "PositionOld"
                    return FI_PositionOld.Updated
                Case "WorkTypeIDOld"
                    return FI_WorkTypeIDOld.Updated
                Case "WorkTypeOld"
                    return FI_WorkTypeOld.Updated
                Case "WorkStatusOld"
                    return FI_WorkStatusOld.Updated
                Case "WorkStatusNameOld"
                    return FI_WorkStatusNameOld.Updated
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
                Case "Wait_ValidDate"
                    return FI_Wait_ValidDate.CreateUpdateSQL
                Case "Wait_Seq"
                    return FI_Wait_Seq.CreateUpdateSQL
                Case "IDNo"
                    return FI_IDNo.CreateUpdateSQL
                Case "ModifyDate"
                    return FI_ModifyDate.CreateUpdateSQL
                Case "Reason"
                    return FI_Reason.CreateUpdateSQL
                Case "Company"
                    return FI_Company.CreateUpdateSQL
                Case "DeptID"
                    return FI_DeptID.CreateUpdateSQL
                Case "DeptName"
                    return FI_DeptName.CreateUpdateSQL
                Case "OrganID"
                    return FI_OrganID.CreateUpdateSQL
                Case "OrganName"
                    return FI_OrganName.CreateUpdateSQL
                Case "GroupID"
                    return FI_GroupID.CreateUpdateSQL
                Case "GroupName"
                    return FI_GroupName.CreateUpdateSQL
                Case "FlowOrganID"
                    return FI_FlowOrganID.CreateUpdateSQL
                Case "FlowOrganName"
                    return FI_FlowOrganName.CreateUpdateSQL
                Case "RankID"
                    return FI_RankID.CreateUpdateSQL
                Case "TitleID"
                    return FI_TitleID.CreateUpdateSQL
                Case "TitleName"
                    return FI_TitleName.CreateUpdateSQL
                Case "PositionID"
                    return FI_PositionID.CreateUpdateSQL
                Case "Position"
                    return FI_Position.CreateUpdateSQL
                Case "WorkTypeID"
                    return FI_WorkTypeID.CreateUpdateSQL
                Case "WorkType"
                    return FI_WorkType.CreateUpdateSQL
                Case "WorkStatus"
                    return FI_WorkStatus.CreateUpdateSQL
                Case "WorkStatusName"
                    return FI_WorkStatusName.CreateUpdateSQL
                Case "Remark"
                    return FI_Remark.CreateUpdateSQL
                Case "BossType"
                    return FI_BossType.CreateUpdateSQL
                Case "IsBoss"
                    return FI_IsBoss.CreateUpdateSQL
                Case "IsSecBoss"
                    return FI_IsSecBoss.CreateUpdateSQL
                Case "IsGroupBoss"
                    return FI_IsGroupBoss.CreateUpdateSQL
                Case "IsSecGroupBoss"
                    return FI_IsSecGroupBoss.CreateUpdateSQL
                Case "CompIDOld"
                    return FI_CompIDOld.CreateUpdateSQL
                Case "CompanyOld"
                    return FI_CompanyOld.CreateUpdateSQL
                Case "DeptIDOld"
                    return FI_DeptIDOld.CreateUpdateSQL
                Case "DeptNameOld"
                    return FI_DeptNameOld.CreateUpdateSQL
                Case "OrganIDOld"
                    return FI_OrganIDOld.CreateUpdateSQL
                Case "OrganNameOld"
                    return FI_OrganNameOld.CreateUpdateSQL
                Case "GroupIDOld"
                    return FI_GroupIDOld.CreateUpdateSQL
                Case "GroupNameOld"
                    return FI_GroupNameOld.CreateUpdateSQL
                Case "FlowOrganIDOld"
                    return FI_FlowOrganIDOld.CreateUpdateSQL
                Case "FlowOrganNameOld"
                    return FI_FlowOrganNameOld.CreateUpdateSQL
                Case "RankIDOld"
                    return FI_RankIDOld.CreateUpdateSQL
                Case "TitleIDOld"
                    return FI_TitleIDOld.CreateUpdateSQL
                Case "TitleNameOld"
                    return FI_TitleNameOld.CreateUpdateSQL
                Case "PositionIDOld"
                    return FI_PositionIDOld.CreateUpdateSQL
                Case "PositionOld"
                    return FI_PositionOld.CreateUpdateSQL
                Case "WorkTypeIDOld"
                    return FI_WorkTypeIDOld.CreateUpdateSQL
                Case "WorkTypeOld"
                    return FI_WorkTypeOld.CreateUpdateSQL
                Case "WorkStatusOld"
                    return FI_WorkStatusOld.CreateUpdateSQL
                Case "WorkStatusNameOld"
                    return FI_WorkStatusNameOld.CreateUpdateSQL
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
            FI_Wait_ValidDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_Wait_Seq.SetInitValue(0)
            FI_IDNo.SetInitValue("")
            FI_ModifyDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_Reason.SetInitValue("")
            FI_Company.SetInitValue("")
            FI_DeptID.SetInitValue("")
            FI_DeptName.SetInitValue("")
            FI_OrganID.SetInitValue("")
            FI_OrganName.SetInitValue("")
            FI_GroupID.SetInitValue("")
            FI_GroupName.SetInitValue("")
            FI_FlowOrganID.SetInitValue("")
            FI_FlowOrganName.SetInitValue("")
            FI_RankID.SetInitValue("")
            FI_TitleID.SetInitValue("")
            FI_TitleName.SetInitValue("")
            FI_PositionID.SetInitValue("")
            FI_Position.SetInitValue("")
            FI_WorkTypeID.SetInitValue("")
            FI_WorkType.SetInitValue("")
            FI_WorkStatus.SetInitValue("")
            FI_WorkStatusName.SetInitValue("")
            FI_Remark.SetInitValue("")
            FI_BossType.SetInitValue("")
            FI_IsBoss.SetInitValue("0")
            FI_IsSecBoss.SetInitValue("0")
            FI_IsGroupBoss.SetInitValue("0")
            FI_IsSecGroupBoss.SetInitValue("0")
            FI_CompIDOld.SetInitValue("")
            FI_CompanyOld.SetInitValue("")
            FI_DeptIDOld.SetInitValue("")
            FI_DeptNameOld.SetInitValue("")
            FI_OrganIDOld.SetInitValue("")
            FI_OrganNameOld.SetInitValue("")
            FI_GroupIDOld.SetInitValue("")
            FI_GroupNameOld.SetInitValue("")
            FI_FlowOrganIDOld.SetInitValue("")
            FI_FlowOrganNameOld.SetInitValue("")
            FI_RankIDOld.SetInitValue("")
            FI_TitleIDOld.SetInitValue("")
            FI_TitleNameOld.SetInitValue("")
            FI_PositionIDOld.SetInitValue("")
            FI_PositionOld.SetInitValue("")
            FI_WorkTypeIDOld.SetInitValue("")
            FI_WorkTypeOld.SetInitValue("")
            FI_WorkStatusOld.SetInitValue("")
            FI_WorkStatusNameOld.SetInitValue("")
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CompID.SetInitValue(dr("CompID"))
            FI_EmpID.SetInitValue(dr("EmpID"))
            FI_Wait_ValidDate.SetInitValue(dr("Wait_ValidDate"))
            FI_Wait_Seq.SetInitValue(dr("Wait_Seq"))
            FI_IDNo.SetInitValue(dr("IDNo"))
            FI_ModifyDate.SetInitValue(dr("ModifyDate"))
            FI_Reason.SetInitValue(dr("Reason"))
            FI_Company.SetInitValue(dr("Company"))
            FI_DeptID.SetInitValue(dr("DeptID"))
            FI_DeptName.SetInitValue(dr("DeptName"))
            FI_OrganID.SetInitValue(dr("OrganID"))
            FI_OrganName.SetInitValue(dr("OrganName"))
            FI_GroupID.SetInitValue(dr("GroupID"))
            FI_GroupName.SetInitValue(dr("GroupName"))
            FI_FlowOrganID.SetInitValue(dr("FlowOrganID"))
            FI_FlowOrganName.SetInitValue(dr("FlowOrganName"))
            FI_RankID.SetInitValue(dr("RankID"))
            FI_TitleID.SetInitValue(dr("TitleID"))
            FI_TitleName.SetInitValue(dr("TitleName"))
            FI_PositionID.SetInitValue(dr("PositionID"))
            FI_Position.SetInitValue(dr("Position"))
            FI_WorkTypeID.SetInitValue(dr("WorkTypeID"))
            FI_WorkType.SetInitValue(dr("WorkType"))
            FI_WorkStatus.SetInitValue(dr("WorkStatus"))
            FI_WorkStatusName.SetInitValue(dr("WorkStatusName"))
            FI_Remark.SetInitValue(dr("Remark"))
            FI_BossType.SetInitValue(dr("BossType"))
            FI_IsBoss.SetInitValue(dr("IsBoss"))
            FI_IsSecBoss.SetInitValue(dr("IsSecBoss"))
            FI_IsGroupBoss.SetInitValue(dr("IsGroupBoss"))
            FI_IsSecGroupBoss.SetInitValue(dr("IsSecGroupBoss"))
            FI_CompIDOld.SetInitValue(dr("CompIDOld"))
            FI_CompanyOld.SetInitValue(dr("CompanyOld"))
            FI_DeptIDOld.SetInitValue(dr("DeptIDOld"))
            FI_DeptNameOld.SetInitValue(dr("DeptNameOld"))
            FI_OrganIDOld.SetInitValue(dr("OrganIDOld"))
            FI_OrganNameOld.SetInitValue(dr("OrganNameOld"))
            FI_GroupIDOld.SetInitValue(dr("GroupIDOld"))
            FI_GroupNameOld.SetInitValue(dr("GroupNameOld"))
            FI_FlowOrganIDOld.SetInitValue(dr("FlowOrganIDOld"))
            FI_FlowOrganNameOld.SetInitValue(dr("FlowOrganNameOld"))
            FI_RankIDOld.SetInitValue(dr("RankIDOld"))
            FI_TitleIDOld.SetInitValue(dr("TitleIDOld"))
            FI_TitleNameOld.SetInitValue(dr("TitleNameOld"))
            FI_PositionIDOld.SetInitValue(dr("PositionIDOld"))
            FI_PositionOld.SetInitValue(dr("PositionOld"))
            FI_WorkTypeIDOld.SetInitValue(dr("WorkTypeIDOld"))
            FI_WorkTypeOld.SetInitValue(dr("WorkTypeOld"))
            FI_WorkStatusOld.SetInitValue(dr("WorkStatusOld"))
            FI_WorkStatusNameOld.SetInitValue(dr("WorkStatusNameOld"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CompID.Updated = False
            FI_EmpID.Updated = False
            FI_Wait_ValidDate.Updated = False
            FI_Wait_Seq.Updated = False
            FI_IDNo.Updated = False
            FI_ModifyDate.Updated = False
            FI_Reason.Updated = False
            FI_Company.Updated = False
            FI_DeptID.Updated = False
            FI_DeptName.Updated = False
            FI_OrganID.Updated = False
            FI_OrganName.Updated = False
            FI_GroupID.Updated = False
            FI_GroupName.Updated = False
            FI_FlowOrganID.Updated = False
            FI_FlowOrganName.Updated = False
            FI_RankID.Updated = False
            FI_TitleID.Updated = False
            FI_TitleName.Updated = False
            FI_PositionID.Updated = False
            FI_Position.Updated = False
            FI_WorkTypeID.Updated = False
            FI_WorkType.Updated = False
            FI_WorkStatus.Updated = False
            FI_WorkStatusName.Updated = False
            FI_Remark.Updated = False
            FI_BossType.Updated = False
            FI_IsBoss.Updated = False
            FI_IsSecBoss.Updated = False
            FI_IsGroupBoss.Updated = False
            FI_IsSecGroupBoss.Updated = False
            FI_CompIDOld.Updated = False
            FI_CompanyOld.Updated = False
            FI_DeptIDOld.Updated = False
            FI_DeptNameOld.Updated = False
            FI_OrganIDOld.Updated = False
            FI_OrganNameOld.Updated = False
            FI_GroupIDOld.Updated = False
            FI_GroupNameOld.Updated = False
            FI_FlowOrganIDOld.Updated = False
            FI_FlowOrganNameOld.Updated = False
            FI_RankIDOld.Updated = False
            FI_TitleIDOld.Updated = False
            FI_TitleNameOld.Updated = False
            FI_PositionIDOld.Updated = False
            FI_PositionOld.Updated = False
            FI_WorkTypeIDOld.Updated = False
            FI_WorkTypeOld.Updated = False
            FI_WorkStatusOld.Updated = False
            FI_WorkStatusNameOld.Updated = False
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

        Public ReadOnly Property Wait_ValidDate As Field(Of Date) 
            Get
                Return FI_Wait_ValidDate
            End Get
        End Property

        Public ReadOnly Property Wait_Seq As Field(Of Integer) 
            Get
                Return FI_Wait_Seq
            End Get
        End Property

        Public ReadOnly Property IDNo As Field(Of String) 
            Get
                Return FI_IDNo
            End Get
        End Property

        Public ReadOnly Property ModifyDate As Field(Of Date) 
            Get
                Return FI_ModifyDate
            End Get
        End Property

        Public ReadOnly Property Reason As Field(Of String) 
            Get
                Return FI_Reason
            End Get
        End Property

        Public ReadOnly Property Company As Field(Of String) 
            Get
                Return FI_Company
            End Get
        End Property

        Public ReadOnly Property DeptID As Field(Of String) 
            Get
                Return FI_DeptID
            End Get
        End Property

        Public ReadOnly Property DeptName As Field(Of String) 
            Get
                Return FI_DeptName
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

        Public ReadOnly Property GroupID As Field(Of String) 
            Get
                Return FI_GroupID
            End Get
        End Property

        Public ReadOnly Property GroupName As Field(Of String) 
            Get
                Return FI_GroupName
            End Get
        End Property

        Public ReadOnly Property FlowOrganID As Field(Of String) 
            Get
                Return FI_FlowOrganID
            End Get
        End Property

        Public ReadOnly Property FlowOrganName As Field(Of String) 
            Get
                Return FI_FlowOrganName
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

        Public ReadOnly Property Position As Field(Of String) 
            Get
                Return FI_Position
            End Get
        End Property

        Public ReadOnly Property WorkTypeID As Field(Of String) 
            Get
                Return FI_WorkTypeID
            End Get
        End Property

        Public ReadOnly Property WorkType As Field(Of String) 
            Get
                Return FI_WorkType
            End Get
        End Property

        Public ReadOnly Property WorkStatus As Field(Of String) 
            Get
                Return FI_WorkStatus
            End Get
        End Property

        Public ReadOnly Property WorkStatusName As Field(Of String) 
            Get
                Return FI_WorkStatusName
            End Get
        End Property

        Public ReadOnly Property Remark As Field(Of String) 
            Get
                Return FI_Remark
            End Get
        End Property

        Public ReadOnly Property BossType As Field(Of String) 
            Get
                Return FI_BossType
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

        Public ReadOnly Property CompIDOld As Field(Of String) 
            Get
                Return FI_CompIDOld
            End Get
        End Property

        Public ReadOnly Property CompanyOld As Field(Of String) 
            Get
                Return FI_CompanyOld
            End Get
        End Property

        Public ReadOnly Property DeptIDOld As Field(Of String) 
            Get
                Return FI_DeptIDOld
            End Get
        End Property

        Public ReadOnly Property DeptNameOld As Field(Of String) 
            Get
                Return FI_DeptNameOld
            End Get
        End Property

        Public ReadOnly Property OrganIDOld As Field(Of String) 
            Get
                Return FI_OrganIDOld
            End Get
        End Property

        Public ReadOnly Property OrganNameOld As Field(Of String) 
            Get
                Return FI_OrganNameOld
            End Get
        End Property

        Public ReadOnly Property GroupIDOld As Field(Of String) 
            Get
                Return FI_GroupIDOld
            End Get
        End Property

        Public ReadOnly Property GroupNameOld As Field(Of String) 
            Get
                Return FI_GroupNameOld
            End Get
        End Property

        Public ReadOnly Property FlowOrganIDOld As Field(Of String) 
            Get
                Return FI_FlowOrganIDOld
            End Get
        End Property

        Public ReadOnly Property FlowOrganNameOld As Field(Of String) 
            Get
                Return FI_FlowOrganNameOld
            End Get
        End Property

        Public ReadOnly Property RankIDOld As Field(Of String) 
            Get
                Return FI_RankIDOld
            End Get
        End Property

        Public ReadOnly Property TitleIDOld As Field(Of String) 
            Get
                Return FI_TitleIDOld
            End Get
        End Property

        Public ReadOnly Property TitleNameOld As Field(Of String) 
            Get
                Return FI_TitleNameOld
            End Get
        End Property

        Public ReadOnly Property PositionIDOld As Field(Of String) 
            Get
                Return FI_PositionIDOld
            End Get
        End Property

        Public ReadOnly Property PositionOld As Field(Of String) 
            Get
                Return FI_PositionOld
            End Get
        End Property

        Public ReadOnly Property WorkTypeIDOld As Field(Of String) 
            Get
                Return FI_WorkTypeIDOld
            End Get
        End Property

        Public ReadOnly Property WorkTypeOld As Field(Of String) 
            Get
                Return FI_WorkTypeOld
            End Get
        End Property

        Public ReadOnly Property WorkStatusOld As Field(Of String) 
            Get
                Return FI_WorkStatusOld
            End Get
        End Property

        Public ReadOnly Property WorkStatusNameOld As Field(Of String) 
            Get
                Return FI_WorkStatusNameOld
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
        Public Function DeleteRowByPrimaryKey(ByVal EmployeeLogWaitRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From EmployeeLogWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And Wait_ValidDate = @Wait_ValidDate")
            strSQL.AppendLine("And Wait_Seq = @Wait_Seq")
            strSQL.AppendLine("And IDNo = @IDNo")
            strSQL.AppendLine("And ModifyDate = @ModifyDate")
            strSQL.AppendLine("And Reason = @Reason")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmployeeLogWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmployeeLogWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@Wait_ValidDate", DbType.Date, EmployeeLogWaitRow.Wait_ValidDate.Value)
            db.AddInParameter(dbcmd, "@Wait_Seq", DbType.Int32, EmployeeLogWaitRow.Wait_Seq.Value)
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, EmployeeLogWaitRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@ModifyDate", DbType.Date, EmployeeLogWaitRow.ModifyDate.Value)
            db.AddInParameter(dbcmd, "@Reason", DbType.String, EmployeeLogWaitRow.Reason.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal EmployeeLogWaitRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From EmployeeLogWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And Wait_ValidDate = @Wait_ValidDate")
            strSQL.AppendLine("And Wait_Seq = @Wait_Seq")
            strSQL.AppendLine("And IDNo = @IDNo")
            strSQL.AppendLine("And ModifyDate = @ModifyDate")
            strSQL.AppendLine("And Reason = @Reason")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmployeeLogWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmployeeLogWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@Wait_ValidDate", DbType.Date, EmployeeLogWaitRow.Wait_ValidDate.Value)
            db.AddInParameter(dbcmd, "@Wait_Seq", DbType.Int32, EmployeeLogWaitRow.Wait_Seq.Value)
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, EmployeeLogWaitRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@ModifyDate", DbType.Date, EmployeeLogWaitRow.ModifyDate.Value)
            db.AddInParameter(dbcmd, "@Reason", DbType.String, EmployeeLogWaitRow.Reason.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal EmployeeLogWaitRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From EmployeeLogWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And Wait_ValidDate = @Wait_ValidDate")
            strSQL.AppendLine("And Wait_Seq = @Wait_Seq")
            strSQL.AppendLine("And IDNo = @IDNo")
            strSQL.AppendLine("And ModifyDate = @ModifyDate")
            strSQL.AppendLine("And Reason = @Reason")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In EmployeeLogWaitRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@Wait_ValidDate", DbType.Date, r.Wait_ValidDate.Value)
                        db.AddInParameter(dbcmd, "@Wait_Seq", DbType.Int32, r.Wait_Seq.Value)
                        db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                        db.AddInParameter(dbcmd, "@ModifyDate", DbType.Date, r.ModifyDate.Value)
                        db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal EmployeeLogWaitRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From EmployeeLogWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And Wait_ValidDate = @Wait_ValidDate")
            strSQL.AppendLine("And Wait_Seq = @Wait_Seq")
            strSQL.AppendLine("And IDNo = @IDNo")
            strSQL.AppendLine("And ModifyDate = @ModifyDate")
            strSQL.AppendLine("And Reason = @Reason")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In EmployeeLogWaitRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@Wait_ValidDate", DbType.Date, r.Wait_ValidDate.Value)
                db.AddInParameter(dbcmd, "@Wait_Seq", DbType.Int32, r.Wait_Seq.Value)
                db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                db.AddInParameter(dbcmd, "@ModifyDate", DbType.Date, r.ModifyDate.Value)
                db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal EmployeeLogWaitRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmployeeLogWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And Wait_ValidDate = @Wait_ValidDate")
            strSQL.AppendLine("And Wait_Seq = @Wait_Seq")
            strSQL.AppendLine("And IDNo = @IDNo")
            strSQL.AppendLine("And ModifyDate = @ModifyDate")
            strSQL.AppendLine("And Reason = @Reason")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmployeeLogWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmployeeLogWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@Wait_ValidDate", DbType.Date, EmployeeLogWaitRow.Wait_ValidDate.Value)
            db.AddInParameter(dbcmd, "@Wait_Seq", DbType.Int32, EmployeeLogWaitRow.Wait_Seq.Value)
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, EmployeeLogWaitRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@ModifyDate", DbType.Date, EmployeeLogWaitRow.ModifyDate.Value)
            db.AddInParameter(dbcmd, "@Reason", DbType.String, EmployeeLogWaitRow.Reason.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(EmployeeLogWaitRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmployeeLogWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And Wait_ValidDate = @Wait_ValidDate")
            strSQL.AppendLine("And Wait_Seq = @Wait_Seq")
            strSQL.AppendLine("And IDNo = @IDNo")
            strSQL.AppendLine("And ModifyDate = @ModifyDate")
            strSQL.AppendLine("And Reason = @Reason")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmployeeLogWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmployeeLogWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@Wait_ValidDate", DbType.Date, EmployeeLogWaitRow.Wait_ValidDate.Value)
            db.AddInParameter(dbcmd, "@Wait_Seq", DbType.Int32, EmployeeLogWaitRow.Wait_Seq.Value)
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, EmployeeLogWaitRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@ModifyDate", DbType.Date, EmployeeLogWaitRow.ModifyDate.Value)
            db.AddInParameter(dbcmd, "@Reason", DbType.String, EmployeeLogWaitRow.Reason.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal EmployeeLogWaitRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update EmployeeLogWait Set")
            For i As Integer = 0 To EmployeeLogWaitRow.FieldNames.Length - 1
                If Not EmployeeLogWaitRow.IsIdentityField(EmployeeLogWaitRow.FieldNames(i)) AndAlso EmployeeLogWaitRow.IsUpdated(EmployeeLogWaitRow.FieldNames(i)) AndAlso EmployeeLogWaitRow.CreateUpdateSQL(EmployeeLogWaitRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, EmployeeLogWaitRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And EmpID = @PKEmpID")
            strSQL.AppendLine("And Wait_ValidDate = @PKWait_ValidDate")
            strSQL.AppendLine("And Wait_Seq = @PKWait_Seq")
            strSQL.AppendLine("And IDNo = @PKIDNo")
            strSQL.AppendLine("And ModifyDate = @PKModifyDate")
            strSQL.AppendLine("And Reason = @PKReason")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If EmployeeLogWaitRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, EmployeeLogWaitRow.CompID.Value)
            If EmployeeLogWaitRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmployeeLogWaitRow.EmpID.Value)
            If EmployeeLogWaitRow.Wait_ValidDate.Updated Then db.AddInParameter(dbcmd, "@Wait_ValidDate", DbType.Date, IIf(IsDateTimeNull(EmployeeLogWaitRow.Wait_ValidDate.Value), Convert.ToDateTime("1900/1/1"), EmployeeLogWaitRow.Wait_ValidDate.Value))
            If EmployeeLogWaitRow.Wait_Seq.Updated Then db.AddInParameter(dbcmd, "@Wait_Seq", DbType.Int32, EmployeeLogWaitRow.Wait_Seq.Value)
            If EmployeeLogWaitRow.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, EmployeeLogWaitRow.IDNo.Value)
            If EmployeeLogWaitRow.ModifyDate.Updated Then db.AddInParameter(dbcmd, "@ModifyDate", DbType.Date, IIf(IsDateTimeNull(EmployeeLogWaitRow.ModifyDate.Value), Convert.ToDateTime("1900/1/1"), EmployeeLogWaitRow.ModifyDate.Value))
            If EmployeeLogWaitRow.Reason.Updated Then db.AddInParameter(dbcmd, "@Reason", DbType.String, EmployeeLogWaitRow.Reason.Value)
            If EmployeeLogWaitRow.Company.Updated Then db.AddInParameter(dbcmd, "@Company", DbType.String, EmployeeLogWaitRow.Company.Value)
            If EmployeeLogWaitRow.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, EmployeeLogWaitRow.DeptID.Value)
            If EmployeeLogWaitRow.DeptName.Updated Then db.AddInParameter(dbcmd, "@DeptName", DbType.String, EmployeeLogWaitRow.DeptName.Value)
            If EmployeeLogWaitRow.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, EmployeeLogWaitRow.OrganID.Value)
            If EmployeeLogWaitRow.OrganName.Updated Then db.AddInParameter(dbcmd, "@OrganName", DbType.String, EmployeeLogWaitRow.OrganName.Value)
            If EmployeeLogWaitRow.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, EmployeeLogWaitRow.GroupID.Value)
            If EmployeeLogWaitRow.GroupName.Updated Then db.AddInParameter(dbcmd, "@GroupName", DbType.String, EmployeeLogWaitRow.GroupName.Value)
            If EmployeeLogWaitRow.FlowOrganID.Updated Then db.AddInParameter(dbcmd, "@FlowOrganID", DbType.String, EmployeeLogWaitRow.FlowOrganID.Value)
            If EmployeeLogWaitRow.FlowOrganName.Updated Then db.AddInParameter(dbcmd, "@FlowOrganName", DbType.String, EmployeeLogWaitRow.FlowOrganName.Value)
            If EmployeeLogWaitRow.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, EmployeeLogWaitRow.RankID.Value)
            If EmployeeLogWaitRow.TitleID.Updated Then db.AddInParameter(dbcmd, "@TitleID", DbType.String, EmployeeLogWaitRow.TitleID.Value)
            If EmployeeLogWaitRow.TitleName.Updated Then db.AddInParameter(dbcmd, "@TitleName", DbType.String, EmployeeLogWaitRow.TitleName.Value)
            If EmployeeLogWaitRow.PositionID.Updated Then db.AddInParameter(dbcmd, "@PositionID", DbType.String, EmployeeLogWaitRow.PositionID.Value)
            If EmployeeLogWaitRow.Position.Updated Then db.AddInParameter(dbcmd, "@Position", DbType.String, EmployeeLogWaitRow.Position.Value)
            If EmployeeLogWaitRow.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, EmployeeLogWaitRow.WorkTypeID.Value)
            If EmployeeLogWaitRow.WorkType.Updated Then db.AddInParameter(dbcmd, "@WorkType", DbType.String, EmployeeLogWaitRow.WorkType.Value)
            If EmployeeLogWaitRow.WorkStatus.Updated Then db.AddInParameter(dbcmd, "@WorkStatus", DbType.String, EmployeeLogWaitRow.WorkStatus.Value)
            If EmployeeLogWaitRow.WorkStatusName.Updated Then db.AddInParameter(dbcmd, "@WorkStatusName", DbType.String, EmployeeLogWaitRow.WorkStatusName.Value)
            If EmployeeLogWaitRow.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, EmployeeLogWaitRow.Remark.Value)
            If EmployeeLogWaitRow.BossType.Updated Then db.AddInParameter(dbcmd, "@BossType", DbType.String, EmployeeLogWaitRow.BossType.Value)
            If EmployeeLogWaitRow.IsBoss.Updated Then db.AddInParameter(dbcmd, "@IsBoss", DbType.String, EmployeeLogWaitRow.IsBoss.Value)
            If EmployeeLogWaitRow.IsSecBoss.Updated Then db.AddInParameter(dbcmd, "@IsSecBoss", DbType.String, EmployeeLogWaitRow.IsSecBoss.Value)
            If EmployeeLogWaitRow.IsGroupBoss.Updated Then db.AddInParameter(dbcmd, "@IsGroupBoss", DbType.String, EmployeeLogWaitRow.IsGroupBoss.Value)
            If EmployeeLogWaitRow.IsSecGroupBoss.Updated Then db.AddInParameter(dbcmd, "@IsSecGroupBoss", DbType.String, EmployeeLogWaitRow.IsSecGroupBoss.Value)
            If EmployeeLogWaitRow.CompIDOld.Updated Then db.AddInParameter(dbcmd, "@CompIDOld", DbType.String, EmployeeLogWaitRow.CompIDOld.Value)
            If EmployeeLogWaitRow.CompanyOld.Updated Then db.AddInParameter(dbcmd, "@CompanyOld", DbType.String, EmployeeLogWaitRow.CompanyOld.Value)
            If EmployeeLogWaitRow.DeptIDOld.Updated Then db.AddInParameter(dbcmd, "@DeptIDOld", DbType.String, EmployeeLogWaitRow.DeptIDOld.Value)
            If EmployeeLogWaitRow.DeptNameOld.Updated Then db.AddInParameter(dbcmd, "@DeptNameOld", DbType.String, EmployeeLogWaitRow.DeptNameOld.Value)
            If EmployeeLogWaitRow.OrganIDOld.Updated Then db.AddInParameter(dbcmd, "@OrganIDOld", DbType.String, EmployeeLogWaitRow.OrganIDOld.Value)
            If EmployeeLogWaitRow.OrganNameOld.Updated Then db.AddInParameter(dbcmd, "@OrganNameOld", DbType.String, EmployeeLogWaitRow.OrganNameOld.Value)
            If EmployeeLogWaitRow.GroupIDOld.Updated Then db.AddInParameter(dbcmd, "@GroupIDOld", DbType.String, EmployeeLogWaitRow.GroupIDOld.Value)
            If EmployeeLogWaitRow.GroupNameOld.Updated Then db.AddInParameter(dbcmd, "@GroupNameOld", DbType.String, EmployeeLogWaitRow.GroupNameOld.Value)
            If EmployeeLogWaitRow.FlowOrganIDOld.Updated Then db.AddInParameter(dbcmd, "@FlowOrganIDOld", DbType.String, EmployeeLogWaitRow.FlowOrganIDOld.Value)
            If EmployeeLogWaitRow.FlowOrganNameOld.Updated Then db.AddInParameter(dbcmd, "@FlowOrganNameOld", DbType.String, EmployeeLogWaitRow.FlowOrganNameOld.Value)
            If EmployeeLogWaitRow.RankIDOld.Updated Then db.AddInParameter(dbcmd, "@RankIDOld", DbType.String, EmployeeLogWaitRow.RankIDOld.Value)
            If EmployeeLogWaitRow.TitleIDOld.Updated Then db.AddInParameter(dbcmd, "@TitleIDOld", DbType.String, EmployeeLogWaitRow.TitleIDOld.Value)
            If EmployeeLogWaitRow.TitleNameOld.Updated Then db.AddInParameter(dbcmd, "@TitleNameOld", DbType.String, EmployeeLogWaitRow.TitleNameOld.Value)
            If EmployeeLogWaitRow.PositionIDOld.Updated Then db.AddInParameter(dbcmd, "@PositionIDOld", DbType.String, EmployeeLogWaitRow.PositionIDOld.Value)
            If EmployeeLogWaitRow.PositionOld.Updated Then db.AddInParameter(dbcmd, "@PositionOld", DbType.String, EmployeeLogWaitRow.PositionOld.Value)
            If EmployeeLogWaitRow.WorkTypeIDOld.Updated Then db.AddInParameter(dbcmd, "@WorkTypeIDOld", DbType.String, EmployeeLogWaitRow.WorkTypeIDOld.Value)
            If EmployeeLogWaitRow.WorkTypeOld.Updated Then db.AddInParameter(dbcmd, "@WorkTypeOld", DbType.String, EmployeeLogWaitRow.WorkTypeOld.Value)
            If EmployeeLogWaitRow.WorkStatusOld.Updated Then db.AddInParameter(dbcmd, "@WorkStatusOld", DbType.String, EmployeeLogWaitRow.WorkStatusOld.Value)
            If EmployeeLogWaitRow.WorkStatusNameOld.Updated Then db.AddInParameter(dbcmd, "@WorkStatusNameOld", DbType.String, EmployeeLogWaitRow.WorkStatusNameOld.Value)
            If EmployeeLogWaitRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmployeeLogWaitRow.LastChgComp.Value)
            If EmployeeLogWaitRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmployeeLogWaitRow.LastChgID.Value)
            If EmployeeLogWaitRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmployeeLogWaitRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmployeeLogWaitRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(EmployeeLogWaitRow.LoadFromDataRow, EmployeeLogWaitRow.CompID.OldValue, EmployeeLogWaitRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(EmployeeLogWaitRow.LoadFromDataRow, EmployeeLogWaitRow.EmpID.OldValue, EmployeeLogWaitRow.EmpID.Value))
            db.AddInParameter(dbcmd, "@PKWait_ValidDate", DbType.Date, IIf(EmployeeLogWaitRow.LoadFromDataRow, EmployeeLogWaitRow.Wait_ValidDate.OldValue, EmployeeLogWaitRow.Wait_ValidDate.Value))
            db.AddInParameter(dbcmd, "@PKWait_Seq", DbType.Int32, IIf(EmployeeLogWaitRow.LoadFromDataRow, EmployeeLogWaitRow.Wait_Seq.OldValue, EmployeeLogWaitRow.Wait_Seq.Value))
            db.AddInParameter(dbcmd, "@PKIDNo", DbType.String, IIf(EmployeeLogWaitRow.LoadFromDataRow, EmployeeLogWaitRow.IDNo.OldValue, EmployeeLogWaitRow.IDNo.Value))
            db.AddInParameter(dbcmd, "@PKModifyDate", DbType.Date, IIf(EmployeeLogWaitRow.LoadFromDataRow, EmployeeLogWaitRow.ModifyDate.OldValue, EmployeeLogWaitRow.ModifyDate.Value))
            db.AddInParameter(dbcmd, "@PKReason", DbType.String, IIf(EmployeeLogWaitRow.LoadFromDataRow, EmployeeLogWaitRow.Reason.OldValue, EmployeeLogWaitRow.Reason.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal EmployeeLogWaitRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update EmployeeLogWait Set")
            For i As Integer = 0 To EmployeeLogWaitRow.FieldNames.Length - 1
                If Not EmployeeLogWaitRow.IsIdentityField(EmployeeLogWaitRow.FieldNames(i)) AndAlso EmployeeLogWaitRow.IsUpdated(EmployeeLogWaitRow.FieldNames(i)) AndAlso EmployeeLogWaitRow.CreateUpdateSQL(EmployeeLogWaitRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, EmployeeLogWaitRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And EmpID = @PKEmpID")
            strSQL.AppendLine("And Wait_ValidDate = @PKWait_ValidDate")
            strSQL.AppendLine("And Wait_Seq = @PKWait_Seq")
            strSQL.AppendLine("And IDNo = @PKIDNo")
            strSQL.AppendLine("And ModifyDate = @PKModifyDate")
            strSQL.AppendLine("And Reason = @PKReason")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If EmployeeLogWaitRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, EmployeeLogWaitRow.CompID.Value)
            If EmployeeLogWaitRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmployeeLogWaitRow.EmpID.Value)
            If EmployeeLogWaitRow.Wait_ValidDate.Updated Then db.AddInParameter(dbcmd, "@Wait_ValidDate", DbType.Date, IIf(IsDateTimeNull(EmployeeLogWaitRow.Wait_ValidDate.Value), Convert.ToDateTime("1900/1/1"), EmployeeLogWaitRow.Wait_ValidDate.Value))
            If EmployeeLogWaitRow.Wait_Seq.Updated Then db.AddInParameter(dbcmd, "@Wait_Seq", DbType.Int32, EmployeeLogWaitRow.Wait_Seq.Value)
            If EmployeeLogWaitRow.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, EmployeeLogWaitRow.IDNo.Value)
            If EmployeeLogWaitRow.ModifyDate.Updated Then db.AddInParameter(dbcmd, "@ModifyDate", DbType.Date, IIf(IsDateTimeNull(EmployeeLogWaitRow.ModifyDate.Value), Convert.ToDateTime("1900/1/1"), EmployeeLogWaitRow.ModifyDate.Value))
            If EmployeeLogWaitRow.Reason.Updated Then db.AddInParameter(dbcmd, "@Reason", DbType.String, EmployeeLogWaitRow.Reason.Value)
            If EmployeeLogWaitRow.Company.Updated Then db.AddInParameter(dbcmd, "@Company", DbType.String, EmployeeLogWaitRow.Company.Value)
            If EmployeeLogWaitRow.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, EmployeeLogWaitRow.DeptID.Value)
            If EmployeeLogWaitRow.DeptName.Updated Then db.AddInParameter(dbcmd, "@DeptName", DbType.String, EmployeeLogWaitRow.DeptName.Value)
            If EmployeeLogWaitRow.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, EmployeeLogWaitRow.OrganID.Value)
            If EmployeeLogWaitRow.OrganName.Updated Then db.AddInParameter(dbcmd, "@OrganName", DbType.String, EmployeeLogWaitRow.OrganName.Value)
            If EmployeeLogWaitRow.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, EmployeeLogWaitRow.GroupID.Value)
            If EmployeeLogWaitRow.GroupName.Updated Then db.AddInParameter(dbcmd, "@GroupName", DbType.String, EmployeeLogWaitRow.GroupName.Value)
            If EmployeeLogWaitRow.FlowOrganID.Updated Then db.AddInParameter(dbcmd, "@FlowOrganID", DbType.String, EmployeeLogWaitRow.FlowOrganID.Value)
            If EmployeeLogWaitRow.FlowOrganName.Updated Then db.AddInParameter(dbcmd, "@FlowOrganName", DbType.String, EmployeeLogWaitRow.FlowOrganName.Value)
            If EmployeeLogWaitRow.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, EmployeeLogWaitRow.RankID.Value)
            If EmployeeLogWaitRow.TitleID.Updated Then db.AddInParameter(dbcmd, "@TitleID", DbType.String, EmployeeLogWaitRow.TitleID.Value)
            If EmployeeLogWaitRow.TitleName.Updated Then db.AddInParameter(dbcmd, "@TitleName", DbType.String, EmployeeLogWaitRow.TitleName.Value)
            If EmployeeLogWaitRow.PositionID.Updated Then db.AddInParameter(dbcmd, "@PositionID", DbType.String, EmployeeLogWaitRow.PositionID.Value)
            If EmployeeLogWaitRow.Position.Updated Then db.AddInParameter(dbcmd, "@Position", DbType.String, EmployeeLogWaitRow.Position.Value)
            If EmployeeLogWaitRow.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, EmployeeLogWaitRow.WorkTypeID.Value)
            If EmployeeLogWaitRow.WorkType.Updated Then db.AddInParameter(dbcmd, "@WorkType", DbType.String, EmployeeLogWaitRow.WorkType.Value)
            If EmployeeLogWaitRow.WorkStatus.Updated Then db.AddInParameter(dbcmd, "@WorkStatus", DbType.String, EmployeeLogWaitRow.WorkStatus.Value)
            If EmployeeLogWaitRow.WorkStatusName.Updated Then db.AddInParameter(dbcmd, "@WorkStatusName", DbType.String, EmployeeLogWaitRow.WorkStatusName.Value)
            If EmployeeLogWaitRow.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, EmployeeLogWaitRow.Remark.Value)
            If EmployeeLogWaitRow.BossType.Updated Then db.AddInParameter(dbcmd, "@BossType", DbType.String, EmployeeLogWaitRow.BossType.Value)
            If EmployeeLogWaitRow.IsBoss.Updated Then db.AddInParameter(dbcmd, "@IsBoss", DbType.String, EmployeeLogWaitRow.IsBoss.Value)
            If EmployeeLogWaitRow.IsSecBoss.Updated Then db.AddInParameter(dbcmd, "@IsSecBoss", DbType.String, EmployeeLogWaitRow.IsSecBoss.Value)
            If EmployeeLogWaitRow.IsGroupBoss.Updated Then db.AddInParameter(dbcmd, "@IsGroupBoss", DbType.String, EmployeeLogWaitRow.IsGroupBoss.Value)
            If EmployeeLogWaitRow.IsSecGroupBoss.Updated Then db.AddInParameter(dbcmd, "@IsSecGroupBoss", DbType.String, EmployeeLogWaitRow.IsSecGroupBoss.Value)
            If EmployeeLogWaitRow.CompIDOld.Updated Then db.AddInParameter(dbcmd, "@CompIDOld", DbType.String, EmployeeLogWaitRow.CompIDOld.Value)
            If EmployeeLogWaitRow.CompanyOld.Updated Then db.AddInParameter(dbcmd, "@CompanyOld", DbType.String, EmployeeLogWaitRow.CompanyOld.Value)
            If EmployeeLogWaitRow.DeptIDOld.Updated Then db.AddInParameter(dbcmd, "@DeptIDOld", DbType.String, EmployeeLogWaitRow.DeptIDOld.Value)
            If EmployeeLogWaitRow.DeptNameOld.Updated Then db.AddInParameter(dbcmd, "@DeptNameOld", DbType.String, EmployeeLogWaitRow.DeptNameOld.Value)
            If EmployeeLogWaitRow.OrganIDOld.Updated Then db.AddInParameter(dbcmd, "@OrganIDOld", DbType.String, EmployeeLogWaitRow.OrganIDOld.Value)
            If EmployeeLogWaitRow.OrganNameOld.Updated Then db.AddInParameter(dbcmd, "@OrganNameOld", DbType.String, EmployeeLogWaitRow.OrganNameOld.Value)
            If EmployeeLogWaitRow.GroupIDOld.Updated Then db.AddInParameter(dbcmd, "@GroupIDOld", DbType.String, EmployeeLogWaitRow.GroupIDOld.Value)
            If EmployeeLogWaitRow.GroupNameOld.Updated Then db.AddInParameter(dbcmd, "@GroupNameOld", DbType.String, EmployeeLogWaitRow.GroupNameOld.Value)
            If EmployeeLogWaitRow.FlowOrganIDOld.Updated Then db.AddInParameter(dbcmd, "@FlowOrganIDOld", DbType.String, EmployeeLogWaitRow.FlowOrganIDOld.Value)
            If EmployeeLogWaitRow.FlowOrganNameOld.Updated Then db.AddInParameter(dbcmd, "@FlowOrganNameOld", DbType.String, EmployeeLogWaitRow.FlowOrganNameOld.Value)
            If EmployeeLogWaitRow.RankIDOld.Updated Then db.AddInParameter(dbcmd, "@RankIDOld", DbType.String, EmployeeLogWaitRow.RankIDOld.Value)
            If EmployeeLogWaitRow.TitleIDOld.Updated Then db.AddInParameter(dbcmd, "@TitleIDOld", DbType.String, EmployeeLogWaitRow.TitleIDOld.Value)
            If EmployeeLogWaitRow.TitleNameOld.Updated Then db.AddInParameter(dbcmd, "@TitleNameOld", DbType.String, EmployeeLogWaitRow.TitleNameOld.Value)
            If EmployeeLogWaitRow.PositionIDOld.Updated Then db.AddInParameter(dbcmd, "@PositionIDOld", DbType.String, EmployeeLogWaitRow.PositionIDOld.Value)
            If EmployeeLogWaitRow.PositionOld.Updated Then db.AddInParameter(dbcmd, "@PositionOld", DbType.String, EmployeeLogWaitRow.PositionOld.Value)
            If EmployeeLogWaitRow.WorkTypeIDOld.Updated Then db.AddInParameter(dbcmd, "@WorkTypeIDOld", DbType.String, EmployeeLogWaitRow.WorkTypeIDOld.Value)
            If EmployeeLogWaitRow.WorkTypeOld.Updated Then db.AddInParameter(dbcmd, "@WorkTypeOld", DbType.String, EmployeeLogWaitRow.WorkTypeOld.Value)
            If EmployeeLogWaitRow.WorkStatusOld.Updated Then db.AddInParameter(dbcmd, "@WorkStatusOld", DbType.String, EmployeeLogWaitRow.WorkStatusOld.Value)
            If EmployeeLogWaitRow.WorkStatusNameOld.Updated Then db.AddInParameter(dbcmd, "@WorkStatusNameOld", DbType.String, EmployeeLogWaitRow.WorkStatusNameOld.Value)
            If EmployeeLogWaitRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmployeeLogWaitRow.LastChgComp.Value)
            If EmployeeLogWaitRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmployeeLogWaitRow.LastChgID.Value)
            If EmployeeLogWaitRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmployeeLogWaitRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmployeeLogWaitRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(EmployeeLogWaitRow.LoadFromDataRow, EmployeeLogWaitRow.CompID.OldValue, EmployeeLogWaitRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(EmployeeLogWaitRow.LoadFromDataRow, EmployeeLogWaitRow.EmpID.OldValue, EmployeeLogWaitRow.EmpID.Value))
            db.AddInParameter(dbcmd, "@PKWait_ValidDate", DbType.Date, IIf(EmployeeLogWaitRow.LoadFromDataRow, EmployeeLogWaitRow.Wait_ValidDate.OldValue, EmployeeLogWaitRow.Wait_ValidDate.Value))
            db.AddInParameter(dbcmd, "@PKWait_Seq", DbType.Int32, IIf(EmployeeLogWaitRow.LoadFromDataRow, EmployeeLogWaitRow.Wait_Seq.OldValue, EmployeeLogWaitRow.Wait_Seq.Value))
            db.AddInParameter(dbcmd, "@PKIDNo", DbType.String, IIf(EmployeeLogWaitRow.LoadFromDataRow, EmployeeLogWaitRow.IDNo.OldValue, EmployeeLogWaitRow.IDNo.Value))
            db.AddInParameter(dbcmd, "@PKModifyDate", DbType.Date, IIf(EmployeeLogWaitRow.LoadFromDataRow, EmployeeLogWaitRow.ModifyDate.OldValue, EmployeeLogWaitRow.ModifyDate.Value))
            db.AddInParameter(dbcmd, "@PKReason", DbType.String, IIf(EmployeeLogWaitRow.LoadFromDataRow, EmployeeLogWaitRow.Reason.OldValue, EmployeeLogWaitRow.Reason.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal EmployeeLogWaitRow As Row()) As Integer
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
                    For Each r As Row In EmployeeLogWaitRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update EmployeeLogWait Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where CompID = @PKCompID")
                        strSQL.AppendLine("And EmpID = @PKEmpID")
                        strSQL.AppendLine("And Wait_ValidDate = @PKWait_ValidDate")
                        strSQL.AppendLine("And Wait_Seq = @PKWait_Seq")
                        strSQL.AppendLine("And IDNo = @PKIDNo")
                        strSQL.AppendLine("And ModifyDate = @PKModifyDate")
                        strSQL.AppendLine("And Reason = @PKReason")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        If r.Wait_ValidDate.Updated Then db.AddInParameter(dbcmd, "@Wait_ValidDate", DbType.Date, IIf(IsDateTimeNull(r.Wait_ValidDate.Value), Convert.ToDateTime("1900/1/1"), r.Wait_ValidDate.Value))
                        If r.Wait_Seq.Updated Then db.AddInParameter(dbcmd, "@Wait_Seq", DbType.Int32, r.Wait_Seq.Value)
                        If r.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                        If r.ModifyDate.Updated Then db.AddInParameter(dbcmd, "@ModifyDate", DbType.Date, IIf(IsDateTimeNull(r.ModifyDate.Value), Convert.ToDateTime("1900/1/1"), r.ModifyDate.Value))
                        If r.Reason.Updated Then db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                        If r.Company.Updated Then db.AddInParameter(dbcmd, "@Company", DbType.String, r.Company.Value)
                        If r.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                        If r.DeptName.Updated Then db.AddInParameter(dbcmd, "@DeptName", DbType.String, r.DeptName.Value)
                        If r.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                        If r.OrganName.Updated Then db.AddInParameter(dbcmd, "@OrganName", DbType.String, r.OrganName.Value)
                        If r.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                        If r.GroupName.Updated Then db.AddInParameter(dbcmd, "@GroupName", DbType.String, r.GroupName.Value)
                        If r.FlowOrganID.Updated Then db.AddInParameter(dbcmd, "@FlowOrganID", DbType.String, r.FlowOrganID.Value)
                        If r.FlowOrganName.Updated Then db.AddInParameter(dbcmd, "@FlowOrganName", DbType.String, r.FlowOrganName.Value)
                        If r.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                        If r.TitleID.Updated Then db.AddInParameter(dbcmd, "@TitleID", DbType.String, r.TitleID.Value)
                        If r.TitleName.Updated Then db.AddInParameter(dbcmd, "@TitleName", DbType.String, r.TitleName.Value)
                        If r.PositionID.Updated Then db.AddInParameter(dbcmd, "@PositionID", DbType.String, r.PositionID.Value)
                        If r.Position.Updated Then db.AddInParameter(dbcmd, "@Position", DbType.String, r.Position.Value)
                        If r.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                        If r.WorkType.Updated Then db.AddInParameter(dbcmd, "@WorkType", DbType.String, r.WorkType.Value)
                        If r.WorkStatus.Updated Then db.AddInParameter(dbcmd, "@WorkStatus", DbType.String, r.WorkStatus.Value)
                        If r.WorkStatusName.Updated Then db.AddInParameter(dbcmd, "@WorkStatusName", DbType.String, r.WorkStatusName.Value)
                        If r.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                        If r.BossType.Updated Then db.AddInParameter(dbcmd, "@BossType", DbType.String, r.BossType.Value)
                        If r.IsBoss.Updated Then db.AddInParameter(dbcmd, "@IsBoss", DbType.String, r.IsBoss.Value)
                        If r.IsSecBoss.Updated Then db.AddInParameter(dbcmd, "@IsSecBoss", DbType.String, r.IsSecBoss.Value)
                        If r.IsGroupBoss.Updated Then db.AddInParameter(dbcmd, "@IsGroupBoss", DbType.String, r.IsGroupBoss.Value)
                        If r.IsSecGroupBoss.Updated Then db.AddInParameter(dbcmd, "@IsSecGroupBoss", DbType.String, r.IsSecGroupBoss.Value)
                        If r.CompIDOld.Updated Then db.AddInParameter(dbcmd, "@CompIDOld", DbType.String, r.CompIDOld.Value)
                        If r.CompanyOld.Updated Then db.AddInParameter(dbcmd, "@CompanyOld", DbType.String, r.CompanyOld.Value)
                        If r.DeptIDOld.Updated Then db.AddInParameter(dbcmd, "@DeptIDOld", DbType.String, r.DeptIDOld.Value)
                        If r.DeptNameOld.Updated Then db.AddInParameter(dbcmd, "@DeptNameOld", DbType.String, r.DeptNameOld.Value)
                        If r.OrganIDOld.Updated Then db.AddInParameter(dbcmd, "@OrganIDOld", DbType.String, r.OrganIDOld.Value)
                        If r.OrganNameOld.Updated Then db.AddInParameter(dbcmd, "@OrganNameOld", DbType.String, r.OrganNameOld.Value)
                        If r.GroupIDOld.Updated Then db.AddInParameter(dbcmd, "@GroupIDOld", DbType.String, r.GroupIDOld.Value)
                        If r.GroupNameOld.Updated Then db.AddInParameter(dbcmd, "@GroupNameOld", DbType.String, r.GroupNameOld.Value)
                        If r.FlowOrganIDOld.Updated Then db.AddInParameter(dbcmd, "@FlowOrganIDOld", DbType.String, r.FlowOrganIDOld.Value)
                        If r.FlowOrganNameOld.Updated Then db.AddInParameter(dbcmd, "@FlowOrganNameOld", DbType.String, r.FlowOrganNameOld.Value)
                        If r.RankIDOld.Updated Then db.AddInParameter(dbcmd, "@RankIDOld", DbType.String, r.RankIDOld.Value)
                        If r.TitleIDOld.Updated Then db.AddInParameter(dbcmd, "@TitleIDOld", DbType.String, r.TitleIDOld.Value)
                        If r.TitleNameOld.Updated Then db.AddInParameter(dbcmd, "@TitleNameOld", DbType.String, r.TitleNameOld.Value)
                        If r.PositionIDOld.Updated Then db.AddInParameter(dbcmd, "@PositionIDOld", DbType.String, r.PositionIDOld.Value)
                        If r.PositionOld.Updated Then db.AddInParameter(dbcmd, "@PositionOld", DbType.String, r.PositionOld.Value)
                        If r.WorkTypeIDOld.Updated Then db.AddInParameter(dbcmd, "@WorkTypeIDOld", DbType.String, r.WorkTypeIDOld.Value)
                        If r.WorkTypeOld.Updated Then db.AddInParameter(dbcmd, "@WorkTypeOld", DbType.String, r.WorkTypeOld.Value)
                        If r.WorkStatusOld.Updated Then db.AddInParameter(dbcmd, "@WorkStatusOld", DbType.String, r.WorkStatusOld.Value)
                        If r.WorkStatusNameOld.Updated Then db.AddInParameter(dbcmd, "@WorkStatusNameOld", DbType.String, r.WorkStatusNameOld.Value)
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                        db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))
                        db.AddInParameter(dbcmd, "@PKWait_ValidDate", DbType.Date, IIf(r.LoadFromDataRow, r.Wait_ValidDate.OldValue, r.Wait_ValidDate.Value))
                        db.AddInParameter(dbcmd, "@PKWait_Seq", DbType.Int32, IIf(r.LoadFromDataRow, r.Wait_Seq.OldValue, r.Wait_Seq.Value))
                        db.AddInParameter(dbcmd, "@PKIDNo", DbType.String, IIf(r.LoadFromDataRow, r.IDNo.OldValue, r.IDNo.Value))
                        db.AddInParameter(dbcmd, "@PKModifyDate", DbType.Date, IIf(r.LoadFromDataRow, r.ModifyDate.OldValue, r.ModifyDate.Value))
                        db.AddInParameter(dbcmd, "@PKReason", DbType.String, IIf(r.LoadFromDataRow, r.Reason.OldValue, r.Reason.Value))

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

        Public Function Update(ByVal EmployeeLogWaitRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In EmployeeLogWaitRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update EmployeeLogWait Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where CompID = @PKCompID")
                strSQL.AppendLine("And EmpID = @PKEmpID")
                strSQL.AppendLine("And Wait_ValidDate = @PKWait_ValidDate")
                strSQL.AppendLine("And Wait_Seq = @PKWait_Seq")
                strSQL.AppendLine("And IDNo = @PKIDNo")
                strSQL.AppendLine("And ModifyDate = @PKModifyDate")
                strSQL.AppendLine("And Reason = @PKReason")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                If r.Wait_ValidDate.Updated Then db.AddInParameter(dbcmd, "@Wait_ValidDate", DbType.Date, IIf(IsDateTimeNull(r.Wait_ValidDate.Value), Convert.ToDateTime("1900/1/1"), r.Wait_ValidDate.Value))
                If r.Wait_Seq.Updated Then db.AddInParameter(dbcmd, "@Wait_Seq", DbType.Int32, r.Wait_Seq.Value)
                If r.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                If r.ModifyDate.Updated Then db.AddInParameter(dbcmd, "@ModifyDate", DbType.Date, IIf(IsDateTimeNull(r.ModifyDate.Value), Convert.ToDateTime("1900/1/1"), r.ModifyDate.Value))
                If r.Reason.Updated Then db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                If r.Company.Updated Then db.AddInParameter(dbcmd, "@Company", DbType.String, r.Company.Value)
                If r.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                If r.DeptName.Updated Then db.AddInParameter(dbcmd, "@DeptName", DbType.String, r.DeptName.Value)
                If r.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                If r.OrganName.Updated Then db.AddInParameter(dbcmd, "@OrganName", DbType.String, r.OrganName.Value)
                If r.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                If r.GroupName.Updated Then db.AddInParameter(dbcmd, "@GroupName", DbType.String, r.GroupName.Value)
                If r.FlowOrganID.Updated Then db.AddInParameter(dbcmd, "@FlowOrganID", DbType.String, r.FlowOrganID.Value)
                If r.FlowOrganName.Updated Then db.AddInParameter(dbcmd, "@FlowOrganName", DbType.String, r.FlowOrganName.Value)
                If r.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                If r.TitleID.Updated Then db.AddInParameter(dbcmd, "@TitleID", DbType.String, r.TitleID.Value)
                If r.TitleName.Updated Then db.AddInParameter(dbcmd, "@TitleName", DbType.String, r.TitleName.Value)
                If r.PositionID.Updated Then db.AddInParameter(dbcmd, "@PositionID", DbType.String, r.PositionID.Value)
                If r.Position.Updated Then db.AddInParameter(dbcmd, "@Position", DbType.String, r.Position.Value)
                If r.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                If r.WorkType.Updated Then db.AddInParameter(dbcmd, "@WorkType", DbType.String, r.WorkType.Value)
                If r.WorkStatus.Updated Then db.AddInParameter(dbcmd, "@WorkStatus", DbType.String, r.WorkStatus.Value)
                If r.WorkStatusName.Updated Then db.AddInParameter(dbcmd, "@WorkStatusName", DbType.String, r.WorkStatusName.Value)
                If r.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                If r.BossType.Updated Then db.AddInParameter(dbcmd, "@BossType", DbType.String, r.BossType.Value)
                If r.IsBoss.Updated Then db.AddInParameter(dbcmd, "@IsBoss", DbType.String, r.IsBoss.Value)
                If r.IsSecBoss.Updated Then db.AddInParameter(dbcmd, "@IsSecBoss", DbType.String, r.IsSecBoss.Value)
                If r.IsGroupBoss.Updated Then db.AddInParameter(dbcmd, "@IsGroupBoss", DbType.String, r.IsGroupBoss.Value)
                If r.IsSecGroupBoss.Updated Then db.AddInParameter(dbcmd, "@IsSecGroupBoss", DbType.String, r.IsSecGroupBoss.Value)
                If r.CompIDOld.Updated Then db.AddInParameter(dbcmd, "@CompIDOld", DbType.String, r.CompIDOld.Value)
                If r.CompanyOld.Updated Then db.AddInParameter(dbcmd, "@CompanyOld", DbType.String, r.CompanyOld.Value)
                If r.DeptIDOld.Updated Then db.AddInParameter(dbcmd, "@DeptIDOld", DbType.String, r.DeptIDOld.Value)
                If r.DeptNameOld.Updated Then db.AddInParameter(dbcmd, "@DeptNameOld", DbType.String, r.DeptNameOld.Value)
                If r.OrganIDOld.Updated Then db.AddInParameter(dbcmd, "@OrganIDOld", DbType.String, r.OrganIDOld.Value)
                If r.OrganNameOld.Updated Then db.AddInParameter(dbcmd, "@OrganNameOld", DbType.String, r.OrganNameOld.Value)
                If r.GroupIDOld.Updated Then db.AddInParameter(dbcmd, "@GroupIDOld", DbType.String, r.GroupIDOld.Value)
                If r.GroupNameOld.Updated Then db.AddInParameter(dbcmd, "@GroupNameOld", DbType.String, r.GroupNameOld.Value)
                If r.FlowOrganIDOld.Updated Then db.AddInParameter(dbcmd, "@FlowOrganIDOld", DbType.String, r.FlowOrganIDOld.Value)
                If r.FlowOrganNameOld.Updated Then db.AddInParameter(dbcmd, "@FlowOrganNameOld", DbType.String, r.FlowOrganNameOld.Value)
                If r.RankIDOld.Updated Then db.AddInParameter(dbcmd, "@RankIDOld", DbType.String, r.RankIDOld.Value)
                If r.TitleIDOld.Updated Then db.AddInParameter(dbcmd, "@TitleIDOld", DbType.String, r.TitleIDOld.Value)
                If r.TitleNameOld.Updated Then db.AddInParameter(dbcmd, "@TitleNameOld", DbType.String, r.TitleNameOld.Value)
                If r.PositionIDOld.Updated Then db.AddInParameter(dbcmd, "@PositionIDOld", DbType.String, r.PositionIDOld.Value)
                If r.PositionOld.Updated Then db.AddInParameter(dbcmd, "@PositionOld", DbType.String, r.PositionOld.Value)
                If r.WorkTypeIDOld.Updated Then db.AddInParameter(dbcmd, "@WorkTypeIDOld", DbType.String, r.WorkTypeIDOld.Value)
                If r.WorkTypeOld.Updated Then db.AddInParameter(dbcmd, "@WorkTypeOld", DbType.String, r.WorkTypeOld.Value)
                If r.WorkStatusOld.Updated Then db.AddInParameter(dbcmd, "@WorkStatusOld", DbType.String, r.WorkStatusOld.Value)
                If r.WorkStatusNameOld.Updated Then db.AddInParameter(dbcmd, "@WorkStatusNameOld", DbType.String, r.WorkStatusNameOld.Value)
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))
                db.AddInParameter(dbcmd, "@PKWait_ValidDate", DbType.Date, IIf(r.LoadFromDataRow, r.Wait_ValidDate.OldValue, r.Wait_ValidDate.Value))
                db.AddInParameter(dbcmd, "@PKWait_Seq", DbType.Int32, IIf(r.LoadFromDataRow, r.Wait_Seq.OldValue, r.Wait_Seq.Value))
                db.AddInParameter(dbcmd, "@PKIDNo", DbType.String, IIf(r.LoadFromDataRow, r.IDNo.OldValue, r.IDNo.Value))
                db.AddInParameter(dbcmd, "@PKModifyDate", DbType.Date, IIf(r.LoadFromDataRow, r.ModifyDate.OldValue, r.ModifyDate.Value))
                db.AddInParameter(dbcmd, "@PKReason", DbType.String, IIf(r.LoadFromDataRow, r.Reason.OldValue, r.Reason.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal EmployeeLogWaitRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From EmployeeLogWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And Wait_ValidDate = @Wait_ValidDate")
            strSQL.AppendLine("And Wait_Seq = @Wait_Seq")
            strSQL.AppendLine("And IDNo = @IDNo")
            strSQL.AppendLine("And ModifyDate = @ModifyDate")
            strSQL.AppendLine("And Reason = @Reason")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmployeeLogWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmployeeLogWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@Wait_ValidDate", DbType.Date, EmployeeLogWaitRow.Wait_ValidDate.Value)
            db.AddInParameter(dbcmd, "@Wait_Seq", DbType.Int32, EmployeeLogWaitRow.Wait_Seq.Value)
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, EmployeeLogWaitRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@ModifyDate", DbType.Date, EmployeeLogWaitRow.ModifyDate.Value)
            db.AddInParameter(dbcmd, "@Reason", DbType.String, EmployeeLogWaitRow.Reason.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal EmployeeLogWaitRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From EmployeeLogWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And Wait_ValidDate = @Wait_ValidDate")
            strSQL.AppendLine("And Wait_Seq = @Wait_Seq")
            strSQL.AppendLine("And IDNo = @IDNo")
            strSQL.AppendLine("And ModifyDate = @ModifyDate")
            strSQL.AppendLine("And Reason = @Reason")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmployeeLogWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmployeeLogWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@Wait_ValidDate", DbType.Date, EmployeeLogWaitRow.Wait_ValidDate.Value)
            db.AddInParameter(dbcmd, "@Wait_Seq", DbType.Int32, EmployeeLogWaitRow.Wait_Seq.Value)
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, EmployeeLogWaitRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@ModifyDate", DbType.Date, EmployeeLogWaitRow.ModifyDate.Value)
            db.AddInParameter(dbcmd, "@Reason", DbType.String, EmployeeLogWaitRow.Reason.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmployeeLogWait")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal EmployeeLogWaitRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into EmployeeLogWait")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, Wait_ValidDate, Wait_Seq, IDNo, ModifyDate, Reason, Company, DeptID,")
            strSQL.AppendLine("    DeptName, OrganID, OrganName, GroupID, GroupName, FlowOrganID, FlowOrganName, RankID,")
            strSQL.AppendLine("    TitleID, TitleName, PositionID, Position, WorkTypeID, WorkType, WorkStatus, WorkStatusName,")
            strSQL.AppendLine("    Remark, BossType, IsBoss, IsSecBoss, IsGroupBoss, IsSecGroupBoss, CompIDOld, CompanyOld,")
            strSQL.AppendLine("    DeptIDOld, DeptNameOld, OrganIDOld, OrganNameOld, GroupIDOld, GroupNameOld, FlowOrganIDOld,")
            strSQL.AppendLine("    FlowOrganNameOld, RankIDOld, TitleIDOld, TitleNameOld, PositionIDOld, PositionOld, WorkTypeIDOld,")
            strSQL.AppendLine("    WorkTypeOld, WorkStatusOld, WorkStatusNameOld, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @Wait_ValidDate, @Wait_Seq, @IDNo, @ModifyDate, @Reason, @Company, @DeptID,")
            strSQL.AppendLine("    @DeptName, @OrganID, @OrganName, @GroupID, @GroupName, @FlowOrganID, @FlowOrganName, @RankID,")
            strSQL.AppendLine("    @TitleID, @TitleName, @PositionID, @Position, @WorkTypeID, @WorkType, @WorkStatus, @WorkStatusName,")
            strSQL.AppendLine("    @Remark, @BossType, @IsBoss, @IsSecBoss, @IsGroupBoss, @IsSecGroupBoss, @CompIDOld, @CompanyOld,")
            strSQL.AppendLine("    @DeptIDOld, @DeptNameOld, @OrganIDOld, @OrganNameOld, @GroupIDOld, @GroupNameOld, @FlowOrganIDOld,")
            strSQL.AppendLine("    @FlowOrganNameOld, @RankIDOld, @TitleIDOld, @TitleNameOld, @PositionIDOld, @PositionOld, @WorkTypeIDOld,")
            strSQL.AppendLine("    @WorkTypeOld, @WorkStatusOld, @WorkStatusNameOld, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmployeeLogWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmployeeLogWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@Wait_ValidDate", DbType.Date, IIf(IsDateTimeNull(EmployeeLogWaitRow.Wait_ValidDate.Value), Convert.ToDateTime("1900/1/1"), EmployeeLogWaitRow.Wait_ValidDate.Value))
            db.AddInParameter(dbcmd, "@Wait_Seq", DbType.Int32, EmployeeLogWaitRow.Wait_Seq.Value)
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, EmployeeLogWaitRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@ModifyDate", DbType.Date, IIf(IsDateTimeNull(EmployeeLogWaitRow.ModifyDate.Value), Convert.ToDateTime("1900/1/1"), EmployeeLogWaitRow.ModifyDate.Value))
            db.AddInParameter(dbcmd, "@Reason", DbType.String, EmployeeLogWaitRow.Reason.Value)
            db.AddInParameter(dbcmd, "@Company", DbType.String, EmployeeLogWaitRow.Company.Value)
            db.AddInParameter(dbcmd, "@DeptID", DbType.String, EmployeeLogWaitRow.DeptID.Value)
            db.AddInParameter(dbcmd, "@DeptName", DbType.String, EmployeeLogWaitRow.DeptName.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, EmployeeLogWaitRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@OrganName", DbType.String, EmployeeLogWaitRow.OrganName.Value)
            db.AddInParameter(dbcmd, "@GroupID", DbType.String, EmployeeLogWaitRow.GroupID.Value)
            db.AddInParameter(dbcmd, "@GroupName", DbType.String, EmployeeLogWaitRow.GroupName.Value)
            db.AddInParameter(dbcmd, "@FlowOrganID", DbType.String, EmployeeLogWaitRow.FlowOrganID.Value)
            db.AddInParameter(dbcmd, "@FlowOrganName", DbType.String, EmployeeLogWaitRow.FlowOrganName.Value)
            db.AddInParameter(dbcmd, "@RankID", DbType.String, EmployeeLogWaitRow.RankID.Value)
            db.AddInParameter(dbcmd, "@TitleID", DbType.String, EmployeeLogWaitRow.TitleID.Value)
            db.AddInParameter(dbcmd, "@TitleName", DbType.String, EmployeeLogWaitRow.TitleName.Value)
            db.AddInParameter(dbcmd, "@PositionID", DbType.String, EmployeeLogWaitRow.PositionID.Value)
            db.AddInParameter(dbcmd, "@Position", DbType.String, EmployeeLogWaitRow.Position.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, EmployeeLogWaitRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@WorkType", DbType.String, EmployeeLogWaitRow.WorkType.Value)
            db.AddInParameter(dbcmd, "@WorkStatus", DbType.String, EmployeeLogWaitRow.WorkStatus.Value)
            db.AddInParameter(dbcmd, "@WorkStatusName", DbType.String, EmployeeLogWaitRow.WorkStatusName.Value)
            db.AddInParameter(dbcmd, "@Remark", DbType.String, EmployeeLogWaitRow.Remark.Value)
            db.AddInParameter(dbcmd, "@BossType", DbType.String, EmployeeLogWaitRow.BossType.Value)
            db.AddInParameter(dbcmd, "@IsBoss", DbType.String, EmployeeLogWaitRow.IsBoss.Value)
            db.AddInParameter(dbcmd, "@IsSecBoss", DbType.String, EmployeeLogWaitRow.IsSecBoss.Value)
            db.AddInParameter(dbcmd, "@IsGroupBoss", DbType.String, EmployeeLogWaitRow.IsGroupBoss.Value)
            db.AddInParameter(dbcmd, "@IsSecGroupBoss", DbType.String, EmployeeLogWaitRow.IsSecGroupBoss.Value)
            db.AddInParameter(dbcmd, "@CompIDOld", DbType.String, EmployeeLogWaitRow.CompIDOld.Value)
            db.AddInParameter(dbcmd, "@CompanyOld", DbType.String, EmployeeLogWaitRow.CompanyOld.Value)
            db.AddInParameter(dbcmd, "@DeptIDOld", DbType.String, EmployeeLogWaitRow.DeptIDOld.Value)
            db.AddInParameter(dbcmd, "@DeptNameOld", DbType.String, EmployeeLogWaitRow.DeptNameOld.Value)
            db.AddInParameter(dbcmd, "@OrganIDOld", DbType.String, EmployeeLogWaitRow.OrganIDOld.Value)
            db.AddInParameter(dbcmd, "@OrganNameOld", DbType.String, EmployeeLogWaitRow.OrganNameOld.Value)
            db.AddInParameter(dbcmd, "@GroupIDOld", DbType.String, EmployeeLogWaitRow.GroupIDOld.Value)
            db.AddInParameter(dbcmd, "@GroupNameOld", DbType.String, EmployeeLogWaitRow.GroupNameOld.Value)
            db.AddInParameter(dbcmd, "@FlowOrganIDOld", DbType.String, EmployeeLogWaitRow.FlowOrganIDOld.Value)
            db.AddInParameter(dbcmd, "@FlowOrganNameOld", DbType.String, EmployeeLogWaitRow.FlowOrganNameOld.Value)
            db.AddInParameter(dbcmd, "@RankIDOld", DbType.String, EmployeeLogWaitRow.RankIDOld.Value)
            db.AddInParameter(dbcmd, "@TitleIDOld", DbType.String, EmployeeLogWaitRow.TitleIDOld.Value)
            db.AddInParameter(dbcmd, "@TitleNameOld", DbType.String, EmployeeLogWaitRow.TitleNameOld.Value)
            db.AddInParameter(dbcmd, "@PositionIDOld", DbType.String, EmployeeLogWaitRow.PositionIDOld.Value)
            db.AddInParameter(dbcmd, "@PositionOld", DbType.String, EmployeeLogWaitRow.PositionOld.Value)
            db.AddInParameter(dbcmd, "@WorkTypeIDOld", DbType.String, EmployeeLogWaitRow.WorkTypeIDOld.Value)
            db.AddInParameter(dbcmd, "@WorkTypeOld", DbType.String, EmployeeLogWaitRow.WorkTypeOld.Value)
            db.AddInParameter(dbcmd, "@WorkStatusOld", DbType.String, EmployeeLogWaitRow.WorkStatusOld.Value)
            db.AddInParameter(dbcmd, "@WorkStatusNameOld", DbType.String, EmployeeLogWaitRow.WorkStatusNameOld.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmployeeLogWaitRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmployeeLogWaitRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmployeeLogWaitRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmployeeLogWaitRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal EmployeeLogWaitRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into EmployeeLogWait")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, Wait_ValidDate, Wait_Seq, IDNo, ModifyDate, Reason, Company, DeptID,")
            strSQL.AppendLine("    DeptName, OrganID, OrganName, GroupID, GroupName, FlowOrganID, FlowOrganName, RankID,")
            strSQL.AppendLine("    TitleID, TitleName, PositionID, Position, WorkTypeID, WorkType, WorkStatus, WorkStatusName,")
            strSQL.AppendLine("    Remark, BossType, IsBoss, IsSecBoss, IsGroupBoss, IsSecGroupBoss, CompIDOld, CompanyOld,")
            strSQL.AppendLine("    DeptIDOld, DeptNameOld, OrganIDOld, OrganNameOld, GroupIDOld, GroupNameOld, FlowOrganIDOld,")
            strSQL.AppendLine("    FlowOrganNameOld, RankIDOld, TitleIDOld, TitleNameOld, PositionIDOld, PositionOld, WorkTypeIDOld,")
            strSQL.AppendLine("    WorkTypeOld, WorkStatusOld, WorkStatusNameOld, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @Wait_ValidDate, @Wait_Seq, @IDNo, @ModifyDate, @Reason, @Company, @DeptID,")
            strSQL.AppendLine("    @DeptName, @OrganID, @OrganName, @GroupID, @GroupName, @FlowOrganID, @FlowOrganName, @RankID,")
            strSQL.AppendLine("    @TitleID, @TitleName, @PositionID, @Position, @WorkTypeID, @WorkType, @WorkStatus, @WorkStatusName,")
            strSQL.AppendLine("    @Remark, @BossType, @IsBoss, @IsSecBoss, @IsGroupBoss, @IsSecGroupBoss, @CompIDOld, @CompanyOld,")
            strSQL.AppendLine("    @DeptIDOld, @DeptNameOld, @OrganIDOld, @OrganNameOld, @GroupIDOld, @GroupNameOld, @FlowOrganIDOld,")
            strSQL.AppendLine("    @FlowOrganNameOld, @RankIDOld, @TitleIDOld, @TitleNameOld, @PositionIDOld, @PositionOld, @WorkTypeIDOld,")
            strSQL.AppendLine("    @WorkTypeOld, @WorkStatusOld, @WorkStatusNameOld, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmployeeLogWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmployeeLogWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@Wait_ValidDate", DbType.Date, IIf(IsDateTimeNull(EmployeeLogWaitRow.Wait_ValidDate.Value), Convert.ToDateTime("1900/1/1"), EmployeeLogWaitRow.Wait_ValidDate.Value))
            db.AddInParameter(dbcmd, "@Wait_Seq", DbType.Int32, EmployeeLogWaitRow.Wait_Seq.Value)
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, EmployeeLogWaitRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@ModifyDate", DbType.Date, IIf(IsDateTimeNull(EmployeeLogWaitRow.ModifyDate.Value), Convert.ToDateTime("1900/1/1"), EmployeeLogWaitRow.ModifyDate.Value))
            db.AddInParameter(dbcmd, "@Reason", DbType.String, EmployeeLogWaitRow.Reason.Value)
            db.AddInParameter(dbcmd, "@Company", DbType.String, EmployeeLogWaitRow.Company.Value)
            db.AddInParameter(dbcmd, "@DeptID", DbType.String, EmployeeLogWaitRow.DeptID.Value)
            db.AddInParameter(dbcmd, "@DeptName", DbType.String, EmployeeLogWaitRow.DeptName.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, EmployeeLogWaitRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@OrganName", DbType.String, EmployeeLogWaitRow.OrganName.Value)
            db.AddInParameter(dbcmd, "@GroupID", DbType.String, EmployeeLogWaitRow.GroupID.Value)
            db.AddInParameter(dbcmd, "@GroupName", DbType.String, EmployeeLogWaitRow.GroupName.Value)
            db.AddInParameter(dbcmd, "@FlowOrganID", DbType.String, EmployeeLogWaitRow.FlowOrganID.Value)
            db.AddInParameter(dbcmd, "@FlowOrganName", DbType.String, EmployeeLogWaitRow.FlowOrganName.Value)
            db.AddInParameter(dbcmd, "@RankID", DbType.String, EmployeeLogWaitRow.RankID.Value)
            db.AddInParameter(dbcmd, "@TitleID", DbType.String, EmployeeLogWaitRow.TitleID.Value)
            db.AddInParameter(dbcmd, "@TitleName", DbType.String, EmployeeLogWaitRow.TitleName.Value)
            db.AddInParameter(dbcmd, "@PositionID", DbType.String, EmployeeLogWaitRow.PositionID.Value)
            db.AddInParameter(dbcmd, "@Position", DbType.String, EmployeeLogWaitRow.Position.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, EmployeeLogWaitRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@WorkType", DbType.String, EmployeeLogWaitRow.WorkType.Value)
            db.AddInParameter(dbcmd, "@WorkStatus", DbType.String, EmployeeLogWaitRow.WorkStatus.Value)
            db.AddInParameter(dbcmd, "@WorkStatusName", DbType.String, EmployeeLogWaitRow.WorkStatusName.Value)
            db.AddInParameter(dbcmd, "@Remark", DbType.String, EmployeeLogWaitRow.Remark.Value)
            db.AddInParameter(dbcmd, "@BossType", DbType.String, EmployeeLogWaitRow.BossType.Value)
            db.AddInParameter(dbcmd, "@IsBoss", DbType.String, EmployeeLogWaitRow.IsBoss.Value)
            db.AddInParameter(dbcmd, "@IsSecBoss", DbType.String, EmployeeLogWaitRow.IsSecBoss.Value)
            db.AddInParameter(dbcmd, "@IsGroupBoss", DbType.String, EmployeeLogWaitRow.IsGroupBoss.Value)
            db.AddInParameter(dbcmd, "@IsSecGroupBoss", DbType.String, EmployeeLogWaitRow.IsSecGroupBoss.Value)
            db.AddInParameter(dbcmd, "@CompIDOld", DbType.String, EmployeeLogWaitRow.CompIDOld.Value)
            db.AddInParameter(dbcmd, "@CompanyOld", DbType.String, EmployeeLogWaitRow.CompanyOld.Value)
            db.AddInParameter(dbcmd, "@DeptIDOld", DbType.String, EmployeeLogWaitRow.DeptIDOld.Value)
            db.AddInParameter(dbcmd, "@DeptNameOld", DbType.String, EmployeeLogWaitRow.DeptNameOld.Value)
            db.AddInParameter(dbcmd, "@OrganIDOld", DbType.String, EmployeeLogWaitRow.OrganIDOld.Value)
            db.AddInParameter(dbcmd, "@OrganNameOld", DbType.String, EmployeeLogWaitRow.OrganNameOld.Value)
            db.AddInParameter(dbcmd, "@GroupIDOld", DbType.String, EmployeeLogWaitRow.GroupIDOld.Value)
            db.AddInParameter(dbcmd, "@GroupNameOld", DbType.String, EmployeeLogWaitRow.GroupNameOld.Value)
            db.AddInParameter(dbcmd, "@FlowOrganIDOld", DbType.String, EmployeeLogWaitRow.FlowOrganIDOld.Value)
            db.AddInParameter(dbcmd, "@FlowOrganNameOld", DbType.String, EmployeeLogWaitRow.FlowOrganNameOld.Value)
            db.AddInParameter(dbcmd, "@RankIDOld", DbType.String, EmployeeLogWaitRow.RankIDOld.Value)
            db.AddInParameter(dbcmd, "@TitleIDOld", DbType.String, EmployeeLogWaitRow.TitleIDOld.Value)
            db.AddInParameter(dbcmd, "@TitleNameOld", DbType.String, EmployeeLogWaitRow.TitleNameOld.Value)
            db.AddInParameter(dbcmd, "@PositionIDOld", DbType.String, EmployeeLogWaitRow.PositionIDOld.Value)
            db.AddInParameter(dbcmd, "@PositionOld", DbType.String, EmployeeLogWaitRow.PositionOld.Value)
            db.AddInParameter(dbcmd, "@WorkTypeIDOld", DbType.String, EmployeeLogWaitRow.WorkTypeIDOld.Value)
            db.AddInParameter(dbcmd, "@WorkTypeOld", DbType.String, EmployeeLogWaitRow.WorkTypeOld.Value)
            db.AddInParameter(dbcmd, "@WorkStatusOld", DbType.String, EmployeeLogWaitRow.WorkStatusOld.Value)
            db.AddInParameter(dbcmd, "@WorkStatusNameOld", DbType.String, EmployeeLogWaitRow.WorkStatusNameOld.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmployeeLogWaitRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmployeeLogWaitRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmployeeLogWaitRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmployeeLogWaitRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal EmployeeLogWaitRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into EmployeeLogWait")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, Wait_ValidDate, Wait_Seq, IDNo, ModifyDate, Reason, Company, DeptID,")
            strSQL.AppendLine("    DeptName, OrganID, OrganName, GroupID, GroupName, FlowOrganID, FlowOrganName, RankID,")
            strSQL.AppendLine("    TitleID, TitleName, PositionID, Position, WorkTypeID, WorkType, WorkStatus, WorkStatusName,")
            strSQL.AppendLine("    Remark, BossType, IsBoss, IsSecBoss, IsGroupBoss, IsSecGroupBoss, CompIDOld, CompanyOld,")
            strSQL.AppendLine("    DeptIDOld, DeptNameOld, OrganIDOld, OrganNameOld, GroupIDOld, GroupNameOld, FlowOrganIDOld,")
            strSQL.AppendLine("    FlowOrganNameOld, RankIDOld, TitleIDOld, TitleNameOld, PositionIDOld, PositionOld, WorkTypeIDOld,")
            strSQL.AppendLine("    WorkTypeOld, WorkStatusOld, WorkStatusNameOld, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @Wait_ValidDate, @Wait_Seq, @IDNo, @ModifyDate, @Reason, @Company, @DeptID,")
            strSQL.AppendLine("    @DeptName, @OrganID, @OrganName, @GroupID, @GroupName, @FlowOrganID, @FlowOrganName, @RankID,")
            strSQL.AppendLine("    @TitleID, @TitleName, @PositionID, @Position, @WorkTypeID, @WorkType, @WorkStatus, @WorkStatusName,")
            strSQL.AppendLine("    @Remark, @BossType, @IsBoss, @IsSecBoss, @IsGroupBoss, @IsSecGroupBoss, @CompIDOld, @CompanyOld,")
            strSQL.AppendLine("    @DeptIDOld, @DeptNameOld, @OrganIDOld, @OrganNameOld, @GroupIDOld, @GroupNameOld, @FlowOrganIDOld,")
            strSQL.AppendLine("    @FlowOrganNameOld, @RankIDOld, @TitleIDOld, @TitleNameOld, @PositionIDOld, @PositionOld, @WorkTypeIDOld,")
            strSQL.AppendLine("    @WorkTypeOld, @WorkStatusOld, @WorkStatusNameOld, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In EmployeeLogWaitRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@Wait_ValidDate", DbType.Date, IIf(IsDateTimeNull(r.Wait_ValidDate.Value), Convert.ToDateTime("1900/1/1"), r.Wait_ValidDate.Value))
                        db.AddInParameter(dbcmd, "@Wait_Seq", DbType.Int32, r.Wait_Seq.Value)
                        db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                        db.AddInParameter(dbcmd, "@ModifyDate", DbType.Date, IIf(IsDateTimeNull(r.ModifyDate.Value), Convert.ToDateTime("1900/1/1"), r.ModifyDate.Value))
                        db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                        db.AddInParameter(dbcmd, "@Company", DbType.String, r.Company.Value)
                        db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                        db.AddInParameter(dbcmd, "@DeptName", DbType.String, r.DeptName.Value)
                        db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                        db.AddInParameter(dbcmd, "@OrganName", DbType.String, r.OrganName.Value)
                        db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                        db.AddInParameter(dbcmd, "@GroupName", DbType.String, r.GroupName.Value)
                        db.AddInParameter(dbcmd, "@FlowOrganID", DbType.String, r.FlowOrganID.Value)
                        db.AddInParameter(dbcmd, "@FlowOrganName", DbType.String, r.FlowOrganName.Value)
                        db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                        db.AddInParameter(dbcmd, "@TitleID", DbType.String, r.TitleID.Value)
                        db.AddInParameter(dbcmd, "@TitleName", DbType.String, r.TitleName.Value)
                        db.AddInParameter(dbcmd, "@PositionID", DbType.String, r.PositionID.Value)
                        db.AddInParameter(dbcmd, "@Position", DbType.String, r.Position.Value)
                        db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                        db.AddInParameter(dbcmd, "@WorkType", DbType.String, r.WorkType.Value)
                        db.AddInParameter(dbcmd, "@WorkStatus", DbType.String, r.WorkStatus.Value)
                        db.AddInParameter(dbcmd, "@WorkStatusName", DbType.String, r.WorkStatusName.Value)
                        db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                        db.AddInParameter(dbcmd, "@BossType", DbType.String, r.BossType.Value)
                        db.AddInParameter(dbcmd, "@IsBoss", DbType.String, r.IsBoss.Value)
                        db.AddInParameter(dbcmd, "@IsSecBoss", DbType.String, r.IsSecBoss.Value)
                        db.AddInParameter(dbcmd, "@IsGroupBoss", DbType.String, r.IsGroupBoss.Value)
                        db.AddInParameter(dbcmd, "@IsSecGroupBoss", DbType.String, r.IsSecGroupBoss.Value)
                        db.AddInParameter(dbcmd, "@CompIDOld", DbType.String, r.CompIDOld.Value)
                        db.AddInParameter(dbcmd, "@CompanyOld", DbType.String, r.CompanyOld.Value)
                        db.AddInParameter(dbcmd, "@DeptIDOld", DbType.String, r.DeptIDOld.Value)
                        db.AddInParameter(dbcmd, "@DeptNameOld", DbType.String, r.DeptNameOld.Value)
                        db.AddInParameter(dbcmd, "@OrganIDOld", DbType.String, r.OrganIDOld.Value)
                        db.AddInParameter(dbcmd, "@OrganNameOld", DbType.String, r.OrganNameOld.Value)
                        db.AddInParameter(dbcmd, "@GroupIDOld", DbType.String, r.GroupIDOld.Value)
                        db.AddInParameter(dbcmd, "@GroupNameOld", DbType.String, r.GroupNameOld.Value)
                        db.AddInParameter(dbcmd, "@FlowOrganIDOld", DbType.String, r.FlowOrganIDOld.Value)
                        db.AddInParameter(dbcmd, "@FlowOrganNameOld", DbType.String, r.FlowOrganNameOld.Value)
                        db.AddInParameter(dbcmd, "@RankIDOld", DbType.String, r.RankIDOld.Value)
                        db.AddInParameter(dbcmd, "@TitleIDOld", DbType.String, r.TitleIDOld.Value)
                        db.AddInParameter(dbcmd, "@TitleNameOld", DbType.String, r.TitleNameOld.Value)
                        db.AddInParameter(dbcmd, "@PositionIDOld", DbType.String, r.PositionIDOld.Value)
                        db.AddInParameter(dbcmd, "@PositionOld", DbType.String, r.PositionOld.Value)
                        db.AddInParameter(dbcmd, "@WorkTypeIDOld", DbType.String, r.WorkTypeIDOld.Value)
                        db.AddInParameter(dbcmd, "@WorkTypeOld", DbType.String, r.WorkTypeOld.Value)
                        db.AddInParameter(dbcmd, "@WorkStatusOld", DbType.String, r.WorkStatusOld.Value)
                        db.AddInParameter(dbcmd, "@WorkStatusNameOld", DbType.String, r.WorkStatusNameOld.Value)
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

        Public Function Insert(ByVal EmployeeLogWaitRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into EmployeeLogWait")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, Wait_ValidDate, Wait_Seq, IDNo, ModifyDate, Reason, Company, DeptID,")
            strSQL.AppendLine("    DeptName, OrganID, OrganName, GroupID, GroupName, FlowOrganID, FlowOrganName, RankID,")
            strSQL.AppendLine("    TitleID, TitleName, PositionID, Position, WorkTypeID, WorkType, WorkStatus, WorkStatusName,")
            strSQL.AppendLine("    Remark, BossType, IsBoss, IsSecBoss, IsGroupBoss, IsSecGroupBoss, CompIDOld, CompanyOld,")
            strSQL.AppendLine("    DeptIDOld, DeptNameOld, OrganIDOld, OrganNameOld, GroupIDOld, GroupNameOld, FlowOrganIDOld,")
            strSQL.AppendLine("    FlowOrganNameOld, RankIDOld, TitleIDOld, TitleNameOld, PositionIDOld, PositionOld, WorkTypeIDOld,")
            strSQL.AppendLine("    WorkTypeOld, WorkStatusOld, WorkStatusNameOld, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @Wait_ValidDate, @Wait_Seq, @IDNo, @ModifyDate, @Reason, @Company, @DeptID,")
            strSQL.AppendLine("    @DeptName, @OrganID, @OrganName, @GroupID, @GroupName, @FlowOrganID, @FlowOrganName, @RankID,")
            strSQL.AppendLine("    @TitleID, @TitleName, @PositionID, @Position, @WorkTypeID, @WorkType, @WorkStatus, @WorkStatusName,")
            strSQL.AppendLine("    @Remark, @BossType, @IsBoss, @IsSecBoss, @IsGroupBoss, @IsSecGroupBoss, @CompIDOld, @CompanyOld,")
            strSQL.AppendLine("    @DeptIDOld, @DeptNameOld, @OrganIDOld, @OrganNameOld, @GroupIDOld, @GroupNameOld, @FlowOrganIDOld,")
            strSQL.AppendLine("    @FlowOrganNameOld, @RankIDOld, @TitleIDOld, @TitleNameOld, @PositionIDOld, @PositionOld, @WorkTypeIDOld,")
            strSQL.AppendLine("    @WorkTypeOld, @WorkStatusOld, @WorkStatusNameOld, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In EmployeeLogWaitRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@Wait_ValidDate", DbType.Date, IIf(IsDateTimeNull(r.Wait_ValidDate.Value), Convert.ToDateTime("1900/1/1"), r.Wait_ValidDate.Value))
                db.AddInParameter(dbcmd, "@Wait_Seq", DbType.Int32, r.Wait_Seq.Value)
                db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                db.AddInParameter(dbcmd, "@ModifyDate", DbType.Date, IIf(IsDateTimeNull(r.ModifyDate.Value), Convert.ToDateTime("1900/1/1"), r.ModifyDate.Value))
                db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                db.AddInParameter(dbcmd, "@Company", DbType.String, r.Company.Value)
                db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                db.AddInParameter(dbcmd, "@DeptName", DbType.String, r.DeptName.Value)
                db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                db.AddInParameter(dbcmd, "@OrganName", DbType.String, r.OrganName.Value)
                db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                db.AddInParameter(dbcmd, "@GroupName", DbType.String, r.GroupName.Value)
                db.AddInParameter(dbcmd, "@FlowOrganID", DbType.String, r.FlowOrganID.Value)
                db.AddInParameter(dbcmd, "@FlowOrganName", DbType.String, r.FlowOrganName.Value)
                db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                db.AddInParameter(dbcmd, "@TitleID", DbType.String, r.TitleID.Value)
                db.AddInParameter(dbcmd, "@TitleName", DbType.String, r.TitleName.Value)
                db.AddInParameter(dbcmd, "@PositionID", DbType.String, r.PositionID.Value)
                db.AddInParameter(dbcmd, "@Position", DbType.String, r.Position.Value)
                db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                db.AddInParameter(dbcmd, "@WorkType", DbType.String, r.WorkType.Value)
                db.AddInParameter(dbcmd, "@WorkStatus", DbType.String, r.WorkStatus.Value)
                db.AddInParameter(dbcmd, "@WorkStatusName", DbType.String, r.WorkStatusName.Value)
                db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                db.AddInParameter(dbcmd, "@BossType", DbType.String, r.BossType.Value)
                db.AddInParameter(dbcmd, "@IsBoss", DbType.String, r.IsBoss.Value)
                db.AddInParameter(dbcmd, "@IsSecBoss", DbType.String, r.IsSecBoss.Value)
                db.AddInParameter(dbcmd, "@IsGroupBoss", DbType.String, r.IsGroupBoss.Value)
                db.AddInParameter(dbcmd, "@IsSecGroupBoss", DbType.String, r.IsSecGroupBoss.Value)
                db.AddInParameter(dbcmd, "@CompIDOld", DbType.String, r.CompIDOld.Value)
                db.AddInParameter(dbcmd, "@CompanyOld", DbType.String, r.CompanyOld.Value)
                db.AddInParameter(dbcmd, "@DeptIDOld", DbType.String, r.DeptIDOld.Value)
                db.AddInParameter(dbcmd, "@DeptNameOld", DbType.String, r.DeptNameOld.Value)
                db.AddInParameter(dbcmd, "@OrganIDOld", DbType.String, r.OrganIDOld.Value)
                db.AddInParameter(dbcmd, "@OrganNameOld", DbType.String, r.OrganNameOld.Value)
                db.AddInParameter(dbcmd, "@GroupIDOld", DbType.String, r.GroupIDOld.Value)
                db.AddInParameter(dbcmd, "@GroupNameOld", DbType.String, r.GroupNameOld.Value)
                db.AddInParameter(dbcmd, "@FlowOrganIDOld", DbType.String, r.FlowOrganIDOld.Value)
                db.AddInParameter(dbcmd, "@FlowOrganNameOld", DbType.String, r.FlowOrganNameOld.Value)
                db.AddInParameter(dbcmd, "@RankIDOld", DbType.String, r.RankIDOld.Value)
                db.AddInParameter(dbcmd, "@TitleIDOld", DbType.String, r.TitleIDOld.Value)
                db.AddInParameter(dbcmd, "@TitleNameOld", DbType.String, r.TitleNameOld.Value)
                db.AddInParameter(dbcmd, "@PositionIDOld", DbType.String, r.PositionIDOld.Value)
                db.AddInParameter(dbcmd, "@PositionOld", DbType.String, r.PositionOld.Value)
                db.AddInParameter(dbcmd, "@WorkTypeIDOld", DbType.String, r.WorkTypeIDOld.Value)
                db.AddInParameter(dbcmd, "@WorkTypeOld", DbType.String, r.WorkTypeOld.Value)
                db.AddInParameter(dbcmd, "@WorkStatusOld", DbType.String, r.WorkStatusOld.Value)
                db.AddInParameter(dbcmd, "@WorkStatusNameOld", DbType.String, r.WorkStatusNameOld.Value)
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

