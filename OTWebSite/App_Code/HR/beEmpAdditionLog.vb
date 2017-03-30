'****************************************************************
' Table:EmpAdditionLog
' Created Date: 2014.11.07
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beEmpAdditionLog
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "ValidDate", "Reason", "CompID", "EmpID", "AddCompID", "AddDeptID", "AddOrganID", "AddFlowOrganID", "AddDeptName", "AddOrganName" _
                                    , "AddFlowOrganName", "DeptID", "OrganID", "DeptName", "OrganName", "RankID", "TitleName", "PositionID", "PositionName", "WorkTypeID", "WorkTypeName" _
                                    , "HoldingRankID", "HoldingTitleName", "Remark", "FileNo", "IsBoss", "IsSecBoss", "IsGroupBoss", "IsSecGroupBoss", "BossType", "CreateDate", "CreateComp" _
                                    , "CreateID", "LastChgDate", "LastChgComp", "LastChgID" }
        Private m_Types As System.Type() = { GetType(Date), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(String) _
                                    , GetType(String), GetType(Date), GetType(String), GetType(String) }
        Private m_PrimaryFields As String() = { "ValidDate", "Reason", "CompID", "EmpID", "AddCompID", "AddDeptID", "AddOrganID" }

        Public ReadOnly Property Rows() As beEmpAdditionLog.Rows 
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
        Public Sub Transfer2Row(EmpAdditionLogTable As DataTable)
            For Each dr As DataRow In EmpAdditionLogTable.Rows
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

                dr(m_Rows(i).ValidDate.FieldName) = m_Rows(i).ValidDate.Value
                dr(m_Rows(i).Reason.FieldName) = m_Rows(i).Reason.Value
                dr(m_Rows(i).CompID.FieldName) = m_Rows(i).CompID.Value
                dr(m_Rows(i).EmpID.FieldName) = m_Rows(i).EmpID.Value
                dr(m_Rows(i).AddCompID.FieldName) = m_Rows(i).AddCompID.Value
                dr(m_Rows(i).AddDeptID.FieldName) = m_Rows(i).AddDeptID.Value
                dr(m_Rows(i).AddOrganID.FieldName) = m_Rows(i).AddOrganID.Value
                dr(m_Rows(i).AddFlowOrganID.FieldName) = m_Rows(i).AddFlowOrganID.Value
                dr(m_Rows(i).AddDeptName.FieldName) = m_Rows(i).AddDeptName.Value
                dr(m_Rows(i).AddOrganName.FieldName) = m_Rows(i).AddOrganName.Value
                dr(m_Rows(i).AddFlowOrganName.FieldName) = m_Rows(i).AddFlowOrganName.Value
                dr(m_Rows(i).DeptID.FieldName) = m_Rows(i).DeptID.Value
                dr(m_Rows(i).OrganID.FieldName) = m_Rows(i).OrganID.Value
                dr(m_Rows(i).DeptName.FieldName) = m_Rows(i).DeptName.Value
                dr(m_Rows(i).OrganName.FieldName) = m_Rows(i).OrganName.Value
                dr(m_Rows(i).RankID.FieldName) = m_Rows(i).RankID.Value
                dr(m_Rows(i).TitleName.FieldName) = m_Rows(i).TitleName.Value
                dr(m_Rows(i).PositionID.FieldName) = m_Rows(i).PositionID.Value
                dr(m_Rows(i).PositionName.FieldName) = m_Rows(i).PositionName.Value
                dr(m_Rows(i).WorkTypeID.FieldName) = m_Rows(i).WorkTypeID.Value
                dr(m_Rows(i).WorkTypeName.FieldName) = m_Rows(i).WorkTypeName.Value
                dr(m_Rows(i).HoldingRankID.FieldName) = m_Rows(i).HoldingRankID.Value
                dr(m_Rows(i).HoldingTitleName.FieldName) = m_Rows(i).HoldingTitleName.Value
                dr(m_Rows(i).Remark.FieldName) = m_Rows(i).Remark.Value
                dr(m_Rows(i).FileNo.FieldName) = m_Rows(i).FileNo.Value
                dr(m_Rows(i).IsBoss.FieldName) = m_Rows(i).IsBoss.Value
                dr(m_Rows(i).IsSecBoss.FieldName) = m_Rows(i).IsSecBoss.Value
                dr(m_Rows(i).IsGroupBoss.FieldName) = m_Rows(i).IsGroupBoss.Value
                dr(m_Rows(i).IsSecGroupBoss.FieldName) = m_Rows(i).IsSecGroupBoss.Value
                dr(m_Rows(i).BossType.FieldName) = m_Rows(i).BossType.Value
                dr(m_Rows(i).CreateDate.FieldName) = m_Rows(i).CreateDate.Value
                dr(m_Rows(i).CreateComp.FieldName) = m_Rows(i).CreateComp.Value
                dr(m_Rows(i).CreateID.FieldName) = m_Rows(i).CreateID.Value
                dr(m_Rows(i).LastChgDate.FieldName) = m_Rows(i).LastChgDate.Value
                dr(m_Rows(i).LastChgComp.FieldName) = m_Rows(i).LastChgComp.Value
                dr(m_Rows(i).LastChgID.FieldName) = m_Rows(i).LastChgID.Value

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

        Public Sub Add(EmpAdditionLogRow As Row)
            m_Rows.Add(EmpAdditionLogRow)
        End Sub

        Public Sub Remove(EmpAdditionLogRow As Row)
            If m_Rows.IndexOf(EmpAdditionLogRow) >= 0 Then
                m_Rows.Remove(EmpAdditionLogRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_ValidDate As Field(Of Date) = new Field(Of Date)("ValidDate", true)
        Private FI_Reason As Field(Of String) = new Field(Of String)("Reason", true)
        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_EmpID As Field(Of String) = new Field(Of String)("EmpID", true)
        Private FI_AddCompID As Field(Of String) = new Field(Of String)("AddCompID", true)
        Private FI_AddDeptID As Field(Of String) = new Field(Of String)("AddDeptID", true)
        Private FI_AddOrganID As Field(Of String) = new Field(Of String)("AddOrganID", true)
        Private FI_AddFlowOrganID As Field(Of String) = new Field(Of String)("AddFlowOrganID", true)
        Private FI_AddDeptName As Field(Of String) = new Field(Of String)("AddDeptName", true)
        Private FI_AddOrganName As Field(Of String) = new Field(Of String)("AddOrganName", true)
        Private FI_AddFlowOrganName As Field(Of String) = new Field(Of String)("AddFlowOrganName", true)
        Private FI_DeptID As Field(Of String) = new Field(Of String)("DeptID", true)
        Private FI_OrganID As Field(Of String) = new Field(Of String)("OrganID", true)
        Private FI_DeptName As Field(Of String) = new Field(Of String)("DeptName", true)
        Private FI_OrganName As Field(Of String) = new Field(Of String)("OrganName", true)
        Private FI_RankID As Field(Of String) = new Field(Of String)("RankID", true)
        Private FI_TitleName As Field(Of String) = new Field(Of String)("TitleName", true)
        Private FI_PositionID As Field(Of String) = new Field(Of String)("PositionID", true)
        Private FI_PositionName As Field(Of String) = new Field(Of String)("PositionName", true)
        Private FI_WorkTypeID As Field(Of String) = new Field(Of String)("WorkTypeID", true)
        Private FI_WorkTypeName As Field(Of String) = new Field(Of String)("WorkTypeName", true)
        Private FI_HoldingRankID As Field(Of String) = new Field(Of String)("HoldingRankID", true)
        Private FI_HoldingTitleName As Field(Of String) = new Field(Of String)("HoldingTitleName", true)
        Private FI_Remark As Field(Of String) = new Field(Of String)("Remark", true)
        Private FI_FileNo As Field(Of String) = new Field(Of String)("FileNo", true)
        Private FI_IsBoss As Field(Of String) = new Field(Of String)("IsBoss", true)
        Private FI_IsSecBoss As Field(Of String) = new Field(Of String)("IsSecBoss", true)
        Private FI_IsGroupBoss As Field(Of String) = new Field(Of String)("IsGroupBoss", true)
        Private FI_IsSecGroupBoss As Field(Of String) = new Field(Of String)("IsSecGroupBoss", true)
        Private FI_BossType As Field(Of String) = new Field(Of String)("BossType", true)
        Private FI_CreateDate As Field(Of Date) = new Field(Of Date)("CreateDate", true)
        Private FI_CreateComp As Field(Of String) = new Field(Of String)("CreateComp", true)
        Private FI_CreateID As Field(Of String) = new Field(Of String)("CreateID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private m_FieldNames As String() = { "ValidDate", "Reason", "CompID", "EmpID", "AddCompID", "AddDeptID", "AddOrganID", "AddFlowOrganID", "AddDeptName", "AddOrganName" _
                                    , "AddFlowOrganName", "DeptID", "OrganID", "DeptName", "OrganName", "RankID", "TitleName", "PositionID", "PositionName", "WorkTypeID", "WorkTypeName" _
                                    , "HoldingRankID", "HoldingTitleName", "Remark", "FileNo", "IsBoss", "IsSecBoss", "IsGroupBoss", "IsSecGroupBoss", "BossType", "CreateDate", "CreateComp" _
                                    , "CreateID", "LastChgDate", "LastChgComp", "LastChgID" }
        Private m_PrimaryFields As String() = { "ValidDate", "Reason", "CompID", "EmpID", "AddCompID", "AddDeptID", "AddOrganID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "ValidDate"
                    Return FI_ValidDate.Value
                Case "Reason"
                    Return FI_Reason.Value
                Case "CompID"
                    Return FI_CompID.Value
                Case "EmpID"
                    Return FI_EmpID.Value
                Case "AddCompID"
                    Return FI_AddCompID.Value
                Case "AddDeptID"
                    Return FI_AddDeptID.Value
                Case "AddOrganID"
                    Return FI_AddOrganID.Value
                Case "AddFlowOrganID"
                    Return FI_AddFlowOrganID.Value
                Case "AddDeptName"
                    Return FI_AddDeptName.Value
                Case "AddOrganName"
                    Return FI_AddOrganName.Value
                Case "AddFlowOrganName"
                    Return FI_AddFlowOrganName.Value
                Case "DeptID"
                    Return FI_DeptID.Value
                Case "OrganID"
                    Return FI_OrganID.Value
                Case "DeptName"
                    Return FI_DeptName.Value
                Case "OrganName"
                    Return FI_OrganName.Value
                Case "RankID"
                    Return FI_RankID.Value
                Case "TitleName"
                    Return FI_TitleName.Value
                Case "PositionID"
                    Return FI_PositionID.Value
                Case "PositionName"
                    Return FI_PositionName.Value
                Case "WorkTypeID"
                    Return FI_WorkTypeID.Value
                Case "WorkTypeName"
                    Return FI_WorkTypeName.Value
                Case "HoldingRankID"
                    Return FI_HoldingRankID.Value
                Case "HoldingTitleName"
                    Return FI_HoldingTitleName.Value
                Case "Remark"
                    Return FI_Remark.Value
                Case "FileNo"
                    Return FI_FileNo.Value
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
                Case "CreateDate"
                    Return FI_CreateDate.Value
                Case "CreateComp"
                    Return FI_CreateComp.Value
                Case "CreateID"
                    Return FI_CreateID.Value
                Case "LastChgDate"
                    Return FI_LastChgDate.Value
                Case "LastChgComp"
                    Return FI_LastChgComp.Value
                Case "LastChgID"
                    Return FI_LastChgID.Value
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Sub SetFieldValue(ByVal fieldName As String, ByVal value As Object)
            Select Case fieldName
                Case "ValidDate"
                    FI_ValidDate.SetValue(value)
                Case "Reason"
                    FI_Reason.SetValue(value)
                Case "CompID"
                    FI_CompID.SetValue(value)
                Case "EmpID"
                    FI_EmpID.SetValue(value)
                Case "AddCompID"
                    FI_AddCompID.SetValue(value)
                Case "AddDeptID"
                    FI_AddDeptID.SetValue(value)
                Case "AddOrganID"
                    FI_AddOrganID.SetValue(value)
                Case "AddFlowOrganID"
                    FI_AddFlowOrganID.SetValue(value)
                Case "AddDeptName"
                    FI_AddDeptName.SetValue(value)
                Case "AddOrganName"
                    FI_AddOrganName.SetValue(value)
                Case "AddFlowOrganName"
                    FI_AddFlowOrganName.SetValue(value)
                Case "DeptID"
                    FI_DeptID.SetValue(value)
                Case "OrganID"
                    FI_OrganID.SetValue(value)
                Case "DeptName"
                    FI_DeptName.SetValue(value)
                Case "OrganName"
                    FI_OrganName.SetValue(value)
                Case "RankID"
                    FI_RankID.SetValue(value)
                Case "TitleName"
                    FI_TitleName.SetValue(value)
                Case "PositionID"
                    FI_PositionID.SetValue(value)
                Case "PositionName"
                    FI_PositionName.SetValue(value)
                Case "WorkTypeID"
                    FI_WorkTypeID.SetValue(value)
                Case "WorkTypeName"
                    FI_WorkTypeName.SetValue(value)
                Case "HoldingRankID"
                    FI_HoldingRankID.SetValue(value)
                Case "HoldingTitleName"
                    FI_HoldingTitleName.SetValue(value)
                Case "Remark"
                    FI_Remark.SetValue(value)
                Case "FileNo"
                    FI_FileNo.SetValue(value)
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
                Case "CreateDate"
                    FI_CreateDate.SetValue(value)
                Case "CreateComp"
                    FI_CreateComp.SetValue(value)
                Case "CreateID"
                    FI_CreateID.SetValue(value)
                Case "LastChgDate"
                    FI_LastChgDate.SetValue(value)
                Case "LastChgComp"
                    FI_LastChgComp.SetValue(value)
                Case "LastChgID"
                    FI_LastChgID.SetValue(value)
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
                Case "ValidDate"
                    return FI_ValidDate.Updated
                Case "Reason"
                    return FI_Reason.Updated
                Case "CompID"
                    return FI_CompID.Updated
                Case "EmpID"
                    return FI_EmpID.Updated
                Case "AddCompID"
                    return FI_AddCompID.Updated
                Case "AddDeptID"
                    return FI_AddDeptID.Updated
                Case "AddOrganID"
                    return FI_AddOrganID.Updated
                Case "AddFlowOrganID"
                    return FI_AddFlowOrganID.Updated
                Case "AddDeptName"
                    return FI_AddDeptName.Updated
                Case "AddOrganName"
                    return FI_AddOrganName.Updated
                Case "AddFlowOrganName"
                    return FI_AddFlowOrganName.Updated
                Case "DeptID"
                    return FI_DeptID.Updated
                Case "OrganID"
                    return FI_OrganID.Updated
                Case "DeptName"
                    return FI_DeptName.Updated
                Case "OrganName"
                    return FI_OrganName.Updated
                Case "RankID"
                    return FI_RankID.Updated
                Case "TitleName"
                    return FI_TitleName.Updated
                Case "PositionID"
                    return FI_PositionID.Updated
                Case "PositionName"
                    return FI_PositionName.Updated
                Case "WorkTypeID"
                    return FI_WorkTypeID.Updated
                Case "WorkTypeName"
                    return FI_WorkTypeName.Updated
                Case "HoldingRankID"
                    return FI_HoldingRankID.Updated
                Case "HoldingTitleName"
                    return FI_HoldingTitleName.Updated
                Case "Remark"
                    return FI_Remark.Updated
                Case "FileNo"
                    return FI_FileNo.Updated
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
                Case "CreateDate"
                    return FI_CreateDate.Updated
                Case "CreateComp"
                    return FI_CreateComp.Updated
                Case "CreateID"
                    return FI_CreateID.Updated
                Case "LastChgDate"
                    return FI_LastChgDate.Updated
                Case "LastChgComp"
                    return FI_LastChgComp.Updated
                Case "LastChgID"
                    return FI_LastChgID.Updated
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public Function CreateUpdateSQL(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "ValidDate"
                    return FI_ValidDate.CreateUpdateSQL
                Case "Reason"
                    return FI_Reason.CreateUpdateSQL
                Case "CompID"
                    return FI_CompID.CreateUpdateSQL
                Case "EmpID"
                    return FI_EmpID.CreateUpdateSQL
                Case "AddCompID"
                    return FI_AddCompID.CreateUpdateSQL
                Case "AddDeptID"
                    return FI_AddDeptID.CreateUpdateSQL
                Case "AddOrganID"
                    return FI_AddOrganID.CreateUpdateSQL
                Case "AddFlowOrganID"
                    return FI_AddFlowOrganID.CreateUpdateSQL
                Case "AddDeptName"
                    return FI_AddDeptName.CreateUpdateSQL
                Case "AddOrganName"
                    return FI_AddOrganName.CreateUpdateSQL
                Case "AddFlowOrganName"
                    return FI_AddFlowOrganName.CreateUpdateSQL
                Case "DeptID"
                    return FI_DeptID.CreateUpdateSQL
                Case "OrganID"
                    return FI_OrganID.CreateUpdateSQL
                Case "DeptName"
                    return FI_DeptName.CreateUpdateSQL
                Case "OrganName"
                    return FI_OrganName.CreateUpdateSQL
                Case "RankID"
                    return FI_RankID.CreateUpdateSQL
                Case "TitleName"
                    return FI_TitleName.CreateUpdateSQL
                Case "PositionID"
                    return FI_PositionID.CreateUpdateSQL
                Case "PositionName"
                    return FI_PositionName.CreateUpdateSQL
                Case "WorkTypeID"
                    return FI_WorkTypeID.CreateUpdateSQL
                Case "WorkTypeName"
                    return FI_WorkTypeName.CreateUpdateSQL
                Case "HoldingRankID"
                    return FI_HoldingRankID.CreateUpdateSQL
                Case "HoldingTitleName"
                    return FI_HoldingTitleName.CreateUpdateSQL
                Case "Remark"
                    return FI_Remark.CreateUpdateSQL
                Case "FileNo"
                    return FI_FileNo.CreateUpdateSQL
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
                Case "CreateDate"
                    return FI_CreateDate.CreateUpdateSQL
                Case "CreateComp"
                    return FI_CreateComp.CreateUpdateSQL
                Case "CreateID"
                    return FI_CreateID.CreateUpdateSQL
                Case "LastChgDate"
                    return FI_LastChgDate.CreateUpdateSQL
                Case "LastChgComp"
                    return FI_LastChgComp.CreateUpdateSQL
                Case "LastChgID"
                    return FI_LastChgID.CreateUpdateSQL
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
            FI_ValidDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_Reason.SetInitValue("")
            FI_CompID.SetInitValue("")
            FI_EmpID.SetInitValue("")
            FI_AddCompID.SetInitValue("")
            FI_AddDeptID.SetInitValue("")
            FI_AddOrganID.SetInitValue("")
            FI_AddFlowOrganID.SetInitValue("")
            FI_AddDeptName.SetInitValue("")
            FI_AddOrganName.SetInitValue("")
            FI_AddFlowOrganName.SetInitValue("")
            FI_DeptID.SetInitValue("")
            FI_OrganID.SetInitValue("")
            FI_DeptName.SetInitValue("")
            FI_OrganName.SetInitValue("")
            FI_RankID.SetInitValue("")
            FI_TitleName.SetInitValue("")
            FI_PositionID.SetInitValue("")
            FI_PositionName.SetInitValue("")
            FI_WorkTypeID.SetInitValue("")
            FI_WorkTypeName.SetInitValue("")
            FI_HoldingRankID.SetInitValue("")
            FI_HoldingTitleName.SetInitValue("")
            FI_Remark.SetInitValue("")
            FI_FileNo.SetInitValue("")
            FI_IsBoss.SetInitValue("0")
            FI_IsSecBoss.SetInitValue("0")
            FI_IsGroupBoss.SetInitValue("0")
            FI_IsSecGroupBoss.SetInitValue("0")
            FI_BossType.SetInitValue("")
            FI_CreateDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_CreateComp.SetInitValue("")
            FI_CreateID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_ValidDate.SetInitValue(dr("ValidDate"))
            FI_Reason.SetInitValue(dr("Reason"))
            FI_CompID.SetInitValue(dr("CompID"))
            FI_EmpID.SetInitValue(dr("EmpID"))
            FI_AddCompID.SetInitValue(dr("AddCompID"))
            FI_AddDeptID.SetInitValue(dr("AddDeptID"))
            FI_AddOrganID.SetInitValue(dr("AddOrganID"))
            FI_AddFlowOrganID.SetInitValue(dr("AddFlowOrganID"))
            FI_AddDeptName.SetInitValue(dr("AddDeptName"))
            FI_AddOrganName.SetInitValue(dr("AddOrganName"))
            FI_AddFlowOrganName.SetInitValue(dr("AddFlowOrganName"))
            FI_DeptID.SetInitValue(dr("DeptID"))
            FI_OrganID.SetInitValue(dr("OrganID"))
            FI_DeptName.SetInitValue(dr("DeptName"))
            FI_OrganName.SetInitValue(dr("OrganName"))
            FI_RankID.SetInitValue(dr("RankID"))
            FI_TitleName.SetInitValue(dr("TitleName"))
            FI_PositionID.SetInitValue(dr("PositionID"))
            FI_PositionName.SetInitValue(dr("PositionName"))
            FI_WorkTypeID.SetInitValue(dr("WorkTypeID"))
            FI_WorkTypeName.SetInitValue(dr("WorkTypeName"))
            FI_HoldingRankID.SetInitValue(dr("HoldingRankID"))
            FI_HoldingTitleName.SetInitValue(dr("HoldingTitleName"))
            FI_Remark.SetInitValue(dr("Remark"))
            FI_FileNo.SetInitValue(dr("FileNo"))
            FI_IsBoss.SetInitValue(dr("IsBoss"))
            FI_IsSecBoss.SetInitValue(dr("IsSecBoss"))
            FI_IsGroupBoss.SetInitValue(dr("IsGroupBoss"))
            FI_IsSecGroupBoss.SetInitValue(dr("IsSecGroupBoss"))
            FI_BossType.SetInitValue(dr("BossType"))
            FI_CreateDate.SetInitValue(dr("CreateDate"))
            FI_CreateComp.SetInitValue(dr("CreateComp"))
            FI_CreateID.SetInitValue(dr("CreateID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_ValidDate.Updated = False
            FI_Reason.Updated = False
            FI_CompID.Updated = False
            FI_EmpID.Updated = False
            FI_AddCompID.Updated = False
            FI_AddDeptID.Updated = False
            FI_AddOrganID.Updated = False
            FI_AddFlowOrganID.Updated = False
            FI_AddDeptName.Updated = False
            FI_AddOrganName.Updated = False
            FI_AddFlowOrganName.Updated = False
            FI_DeptID.Updated = False
            FI_OrganID.Updated = False
            FI_DeptName.Updated = False
            FI_OrganName.Updated = False
            FI_RankID.Updated = False
            FI_TitleName.Updated = False
            FI_PositionID.Updated = False
            FI_PositionName.Updated = False
            FI_WorkTypeID.Updated = False
            FI_WorkTypeName.Updated = False
            FI_HoldingRankID.Updated = False
            FI_HoldingTitleName.Updated = False
            FI_Remark.Updated = False
            FI_FileNo.Updated = False
            FI_IsBoss.Updated = False
            FI_IsSecBoss.Updated = False
            FI_IsGroupBoss.Updated = False
            FI_IsSecGroupBoss.Updated = False
            FI_BossType.Updated = False
            FI_CreateDate.Updated = False
            FI_CreateComp.Updated = False
            FI_CreateID.Updated = False
            FI_LastChgDate.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
        End Sub

        Public ReadOnly Property ValidDate As Field(Of Date) 
            Get
                Return FI_ValidDate
            End Get
        End Property

        Public ReadOnly Property Reason As Field(Of String) 
            Get
                Return FI_Reason
            End Get
        End Property

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

        Public ReadOnly Property AddCompID As Field(Of String) 
            Get
                Return FI_AddCompID
            End Get
        End Property

        Public ReadOnly Property AddDeptID As Field(Of String) 
            Get
                Return FI_AddDeptID
            End Get
        End Property

        Public ReadOnly Property AddOrganID As Field(Of String) 
            Get
                Return FI_AddOrganID
            End Get
        End Property

        Public ReadOnly Property AddFlowOrganID As Field(Of String) 
            Get
                Return FI_AddFlowOrganID
            End Get
        End Property

        Public ReadOnly Property AddDeptName As Field(Of String) 
            Get
                Return FI_AddDeptName
            End Get
        End Property

        Public ReadOnly Property AddOrganName As Field(Of String) 
            Get
                Return FI_AddOrganName
            End Get
        End Property

        Public ReadOnly Property AddFlowOrganName As Field(Of String) 
            Get
                Return FI_AddFlowOrganName
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

        Public ReadOnly Property DeptName As Field(Of String) 
            Get
                Return FI_DeptName
            End Get
        End Property

        Public ReadOnly Property OrganName As Field(Of String) 
            Get
                Return FI_OrganName
            End Get
        End Property

        Public ReadOnly Property RankID As Field(Of String) 
            Get
                Return FI_RankID
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

        Public ReadOnly Property PositionName As Field(Of String) 
            Get
                Return FI_PositionName
            End Get
        End Property

        Public ReadOnly Property WorkTypeID As Field(Of String) 
            Get
                Return FI_WorkTypeID
            End Get
        End Property

        Public ReadOnly Property WorkTypeName As Field(Of String) 
            Get
                Return FI_WorkTypeName
            End Get
        End Property

        Public ReadOnly Property HoldingRankID As Field(Of String) 
            Get
                Return FI_HoldingRankID
            End Get
        End Property

        Public ReadOnly Property HoldingTitleName As Field(Of String) 
            Get
                Return FI_HoldingTitleName
            End Get
        End Property

        Public ReadOnly Property Remark As Field(Of String) 
            Get
                Return FI_Remark
            End Get
        End Property

        Public ReadOnly Property FileNo As Field(Of String) 
            Get
                Return FI_FileNo
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

        Public ReadOnly Property CreateDate As Field(Of Date) 
            Get
                Return FI_CreateDate
            End Get
        End Property

        Public ReadOnly Property CreateComp As Field(Of String) 
            Get
                Return FI_CreateComp
            End Get
        End Property

        Public ReadOnly Property CreateID As Field(Of String) 
            Get
                Return FI_CreateID
            End Get
        End Property

        Public ReadOnly Property LastChgDate As Field(Of Date) 
            Get
                Return FI_LastChgDate
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

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal EmpAdditionLogRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From EmpAdditionLog")
            strSQL.AppendLine("Where ValidDate = @ValidDate")
            strSQL.AppendLine("And Reason = @Reason")
            strSQL.AppendLine("And CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And AddCompID = @AddCompID")
            strSQL.AppendLine("And AddDeptID = @AddDeptID")
            strSQL.AppendLine("And AddOrganID = @AddOrganID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, EmpAdditionLogRow.ValidDate.Value)
            db.AddInParameter(dbcmd, "@Reason", DbType.String, EmpAdditionLogRow.Reason.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpAdditionLogRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpAdditionLogRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@AddCompID", DbType.String, EmpAdditionLogRow.AddCompID.Value)
            db.AddInParameter(dbcmd, "@AddDeptID", DbType.String, EmpAdditionLogRow.AddDeptID.Value)
            db.AddInParameter(dbcmd, "@AddOrganID", DbType.String, EmpAdditionLogRow.AddOrganID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal EmpAdditionLogRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From EmpAdditionLog")
            strSQL.AppendLine("Where ValidDate = @ValidDate")
            strSQL.AppendLine("And Reason = @Reason")
            strSQL.AppendLine("And CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And AddCompID = @AddCompID")
            strSQL.AppendLine("And AddDeptID = @AddDeptID")
            strSQL.AppendLine("And AddOrganID = @AddOrganID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, EmpAdditionLogRow.ValidDate.Value)
            db.AddInParameter(dbcmd, "@Reason", DbType.String, EmpAdditionLogRow.Reason.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpAdditionLogRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpAdditionLogRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@AddCompID", DbType.String, EmpAdditionLogRow.AddCompID.Value)
            db.AddInParameter(dbcmd, "@AddDeptID", DbType.String, EmpAdditionLogRow.AddDeptID.Value)
            db.AddInParameter(dbcmd, "@AddOrganID", DbType.String, EmpAdditionLogRow.AddOrganID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal EmpAdditionLogRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From EmpAdditionLog")
            strSQL.AppendLine("Where ValidDate = @ValidDate")
            strSQL.AppendLine("And Reason = @Reason")
            strSQL.AppendLine("And CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And AddCompID = @AddCompID")
            strSQL.AppendLine("And AddDeptID = @AddDeptID")
            strSQL.AppendLine("And AddOrganID = @AddOrganID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In EmpAdditionLogRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, r.ValidDate.Value)
                        db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@AddCompID", DbType.String, r.AddCompID.Value)
                        db.AddInParameter(dbcmd, "@AddDeptID", DbType.String, r.AddDeptID.Value)
                        db.AddInParameter(dbcmd, "@AddOrganID", DbType.String, r.AddOrganID.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal EmpAdditionLogRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From EmpAdditionLog")
            strSQL.AppendLine("Where ValidDate = @ValidDate")
            strSQL.AppendLine("And Reason = @Reason")
            strSQL.AppendLine("And CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And AddCompID = @AddCompID")
            strSQL.AppendLine("And AddDeptID = @AddDeptID")
            strSQL.AppendLine("And AddOrganID = @AddOrganID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In EmpAdditionLogRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, r.ValidDate.Value)
                db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@AddCompID", DbType.String, r.AddCompID.Value)
                db.AddInParameter(dbcmd, "@AddDeptID", DbType.String, r.AddDeptID.Value)
                db.AddInParameter(dbcmd, "@AddOrganID", DbType.String, r.AddOrganID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal EmpAdditionLogRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmpAdditionLog")
            strSQL.AppendLine("Where ValidDate = @ValidDate")
            strSQL.AppendLine("And Reason = @Reason")
            strSQL.AppendLine("And CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And AddCompID = @AddCompID")
            strSQL.AppendLine("And AddDeptID = @AddDeptID")
            strSQL.AppendLine("And AddOrganID = @AddOrganID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, EmpAdditionLogRow.ValidDate.Value)
            db.AddInParameter(dbcmd, "@Reason", DbType.String, EmpAdditionLogRow.Reason.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpAdditionLogRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpAdditionLogRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@AddCompID", DbType.String, EmpAdditionLogRow.AddCompID.Value)
            db.AddInParameter(dbcmd, "@AddDeptID", DbType.String, EmpAdditionLogRow.AddDeptID.Value)
            db.AddInParameter(dbcmd, "@AddOrganID", DbType.String, EmpAdditionLogRow.AddOrganID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(EmpAdditionLogRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmpAdditionLog")
            strSQL.AppendLine("Where ValidDate = @ValidDate")
            strSQL.AppendLine("And Reason = @Reason")
            strSQL.AppendLine("And CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And AddCompID = @AddCompID")
            strSQL.AppendLine("And AddDeptID = @AddDeptID")
            strSQL.AppendLine("And AddOrganID = @AddOrganID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, EmpAdditionLogRow.ValidDate.Value)
            db.AddInParameter(dbcmd, "@Reason", DbType.String, EmpAdditionLogRow.Reason.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpAdditionLogRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpAdditionLogRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@AddCompID", DbType.String, EmpAdditionLogRow.AddCompID.Value)
            db.AddInParameter(dbcmd, "@AddDeptID", DbType.String, EmpAdditionLogRow.AddDeptID.Value)
            db.AddInParameter(dbcmd, "@AddOrganID", DbType.String, EmpAdditionLogRow.AddOrganID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal EmpAdditionLogRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update EmpAdditionLog Set")
            For i As Integer = 0 To EmpAdditionLogRow.FieldNames.Length - 1
                If Not EmpAdditionLogRow.IsIdentityField(EmpAdditionLogRow.FieldNames(i)) AndAlso EmpAdditionLogRow.IsUpdated(EmpAdditionLogRow.FieldNames(i)) AndAlso EmpAdditionLogRow.CreateUpdateSQL(EmpAdditionLogRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, EmpAdditionLogRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where ValidDate = @PKValidDate")
            strSQL.AppendLine("And Reason = @PKReason")
            strSQL.AppendLine("And CompID = @PKCompID")
            strSQL.AppendLine("And EmpID = @PKEmpID")
            strSQL.AppendLine("And AddCompID = @PKAddCompID")
            strSQL.AppendLine("And AddDeptID = @PKAddDeptID")
            strSQL.AppendLine("And AddOrganID = @PKAddOrganID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If EmpAdditionLogRow.ValidDate.Updated Then db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(EmpAdditionLogRow.ValidDate.Value), Convert.ToDateTime("1900/1/1"), EmpAdditionLogRow.ValidDate.Value))
            If EmpAdditionLogRow.Reason.Updated Then db.AddInParameter(dbcmd, "@Reason", DbType.String, EmpAdditionLogRow.Reason.Value)
            If EmpAdditionLogRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpAdditionLogRow.CompID.Value)
            If EmpAdditionLogRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpAdditionLogRow.EmpID.Value)
            If EmpAdditionLogRow.AddCompID.Updated Then db.AddInParameter(dbcmd, "@AddCompID", DbType.String, EmpAdditionLogRow.AddCompID.Value)
            If EmpAdditionLogRow.AddDeptID.Updated Then db.AddInParameter(dbcmd, "@AddDeptID", DbType.String, EmpAdditionLogRow.AddDeptID.Value)
            If EmpAdditionLogRow.AddOrganID.Updated Then db.AddInParameter(dbcmd, "@AddOrganID", DbType.String, EmpAdditionLogRow.AddOrganID.Value)
            If EmpAdditionLogRow.AddFlowOrganID.Updated Then db.AddInParameter(dbcmd, "@AddFlowOrganID", DbType.String, EmpAdditionLogRow.AddFlowOrganID.Value)
            If EmpAdditionLogRow.AddDeptName.Updated Then db.AddInParameter(dbcmd, "@AddDeptName", DbType.String, EmpAdditionLogRow.AddDeptName.Value)
            If EmpAdditionLogRow.AddOrganName.Updated Then db.AddInParameter(dbcmd, "@AddOrganName", DbType.String, EmpAdditionLogRow.AddOrganName.Value)
            If EmpAdditionLogRow.AddFlowOrganName.Updated Then db.AddInParameter(dbcmd, "@AddFlowOrganName", DbType.String, EmpAdditionLogRow.AddFlowOrganName.Value)
            If EmpAdditionLogRow.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, EmpAdditionLogRow.DeptID.Value)
            If EmpAdditionLogRow.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, EmpAdditionLogRow.OrganID.Value)
            If EmpAdditionLogRow.DeptName.Updated Then db.AddInParameter(dbcmd, "@DeptName", DbType.String, EmpAdditionLogRow.DeptName.Value)
            If EmpAdditionLogRow.OrganName.Updated Then db.AddInParameter(dbcmd, "@OrganName", DbType.String, EmpAdditionLogRow.OrganName.Value)
            If EmpAdditionLogRow.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, EmpAdditionLogRow.RankID.Value)
            If EmpAdditionLogRow.TitleName.Updated Then db.AddInParameter(dbcmd, "@TitleName", DbType.String, EmpAdditionLogRow.TitleName.Value)
            If EmpAdditionLogRow.PositionID.Updated Then db.AddInParameter(dbcmd, "@PositionID", DbType.String, EmpAdditionLogRow.PositionID.Value)
            If EmpAdditionLogRow.PositionName.Updated Then db.AddInParameter(dbcmd, "@PositionName", DbType.String, EmpAdditionLogRow.PositionName.Value)
            If EmpAdditionLogRow.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, EmpAdditionLogRow.WorkTypeID.Value)
            If EmpAdditionLogRow.WorkTypeName.Updated Then db.AddInParameter(dbcmd, "@WorkTypeName", DbType.String, EmpAdditionLogRow.WorkTypeName.Value)
            If EmpAdditionLogRow.HoldingRankID.Updated Then db.AddInParameter(dbcmd, "@HoldingRankID", DbType.String, EmpAdditionLogRow.HoldingRankID.Value)
            If EmpAdditionLogRow.HoldingTitleName.Updated Then db.AddInParameter(dbcmd, "@HoldingTitleName", DbType.String, EmpAdditionLogRow.HoldingTitleName.Value)
            If EmpAdditionLogRow.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, EmpAdditionLogRow.Remark.Value)
            If EmpAdditionLogRow.FileNo.Updated Then db.AddInParameter(dbcmd, "@FileNo", DbType.String, EmpAdditionLogRow.FileNo.Value)
            If EmpAdditionLogRow.IsBoss.Updated Then db.AddInParameter(dbcmd, "@IsBoss", DbType.String, EmpAdditionLogRow.IsBoss.Value)
            If EmpAdditionLogRow.IsSecBoss.Updated Then db.AddInParameter(dbcmd, "@IsSecBoss", DbType.String, EmpAdditionLogRow.IsSecBoss.Value)
            If EmpAdditionLogRow.IsGroupBoss.Updated Then db.AddInParameter(dbcmd, "@IsGroupBoss", DbType.String, EmpAdditionLogRow.IsGroupBoss.Value)
            If EmpAdditionLogRow.IsSecGroupBoss.Updated Then db.AddInParameter(dbcmd, "@IsSecGroupBoss", DbType.String, EmpAdditionLogRow.IsSecGroupBoss.Value)
            If EmpAdditionLogRow.BossType.Updated Then db.AddInParameter(dbcmd, "@BossType", DbType.String, EmpAdditionLogRow.BossType.Value)
            If EmpAdditionLogRow.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(EmpAdditionLogRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), EmpAdditionLogRow.CreateDate.Value))
            If EmpAdditionLogRow.CreateComp.Updated Then db.AddInParameter(dbcmd, "@CreateComp", DbType.String, EmpAdditionLogRow.CreateComp.Value)
            If EmpAdditionLogRow.CreateID.Updated Then db.AddInParameter(dbcmd, "@CreateID", DbType.String, EmpAdditionLogRow.CreateID.Value)
            If EmpAdditionLogRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpAdditionLogRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpAdditionLogRow.LastChgDate.Value))
            If EmpAdditionLogRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpAdditionLogRow.LastChgComp.Value)
            If EmpAdditionLogRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpAdditionLogRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@PKValidDate", DbType.Date, IIf(EmpAdditionLogRow.LoadFromDataRow, EmpAdditionLogRow.ValidDate.OldValue, EmpAdditionLogRow.ValidDate.Value))
            db.AddInParameter(dbcmd, "@PKReason", DbType.String, IIf(EmpAdditionLogRow.LoadFromDataRow, EmpAdditionLogRow.Reason.OldValue, EmpAdditionLogRow.Reason.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(EmpAdditionLogRow.LoadFromDataRow, EmpAdditionLogRow.CompID.OldValue, EmpAdditionLogRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(EmpAdditionLogRow.LoadFromDataRow, EmpAdditionLogRow.EmpID.OldValue, EmpAdditionLogRow.EmpID.Value))
            db.AddInParameter(dbcmd, "@PKAddCompID", DbType.String, IIf(EmpAdditionLogRow.LoadFromDataRow, EmpAdditionLogRow.AddCompID.OldValue, EmpAdditionLogRow.AddCompID.Value))
            db.AddInParameter(dbcmd, "@PKAddDeptID", DbType.String, IIf(EmpAdditionLogRow.LoadFromDataRow, EmpAdditionLogRow.AddDeptID.OldValue, EmpAdditionLogRow.AddDeptID.Value))
            db.AddInParameter(dbcmd, "@PKAddOrganID", DbType.String, IIf(EmpAdditionLogRow.LoadFromDataRow, EmpAdditionLogRow.AddOrganID.OldValue, EmpAdditionLogRow.AddOrganID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal EmpAdditionLogRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update EmpAdditionLog Set")
            For i As Integer = 0 To EmpAdditionLogRow.FieldNames.Length - 1
                If Not EmpAdditionLogRow.IsIdentityField(EmpAdditionLogRow.FieldNames(i)) AndAlso EmpAdditionLogRow.IsUpdated(EmpAdditionLogRow.FieldNames(i)) AndAlso EmpAdditionLogRow.CreateUpdateSQL(EmpAdditionLogRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, EmpAdditionLogRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where ValidDate = @PKValidDate")
            strSQL.AppendLine("And Reason = @PKReason")
            strSQL.AppendLine("And CompID = @PKCompID")
            strSQL.AppendLine("And EmpID = @PKEmpID")
            strSQL.AppendLine("And AddCompID = @PKAddCompID")
            strSQL.AppendLine("And AddDeptID = @PKAddDeptID")
            strSQL.AppendLine("And AddOrganID = @PKAddOrganID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If EmpAdditionLogRow.ValidDate.Updated Then db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(EmpAdditionLogRow.ValidDate.Value), Convert.ToDateTime("1900/1/1"), EmpAdditionLogRow.ValidDate.Value))
            If EmpAdditionLogRow.Reason.Updated Then db.AddInParameter(dbcmd, "@Reason", DbType.String, EmpAdditionLogRow.Reason.Value)
            If EmpAdditionLogRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpAdditionLogRow.CompID.Value)
            If EmpAdditionLogRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpAdditionLogRow.EmpID.Value)
            If EmpAdditionLogRow.AddCompID.Updated Then db.AddInParameter(dbcmd, "@AddCompID", DbType.String, EmpAdditionLogRow.AddCompID.Value)
            If EmpAdditionLogRow.AddDeptID.Updated Then db.AddInParameter(dbcmd, "@AddDeptID", DbType.String, EmpAdditionLogRow.AddDeptID.Value)
            If EmpAdditionLogRow.AddOrganID.Updated Then db.AddInParameter(dbcmd, "@AddOrganID", DbType.String, EmpAdditionLogRow.AddOrganID.Value)
            If EmpAdditionLogRow.AddFlowOrganID.Updated Then db.AddInParameter(dbcmd, "@AddFlowOrganID", DbType.String, EmpAdditionLogRow.AddFlowOrganID.Value)
            If EmpAdditionLogRow.AddDeptName.Updated Then db.AddInParameter(dbcmd, "@AddDeptName", DbType.String, EmpAdditionLogRow.AddDeptName.Value)
            If EmpAdditionLogRow.AddOrganName.Updated Then db.AddInParameter(dbcmd, "@AddOrganName", DbType.String, EmpAdditionLogRow.AddOrganName.Value)
            If EmpAdditionLogRow.AddFlowOrganName.Updated Then db.AddInParameter(dbcmd, "@AddFlowOrganName", DbType.String, EmpAdditionLogRow.AddFlowOrganName.Value)
            If EmpAdditionLogRow.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, EmpAdditionLogRow.DeptID.Value)
            If EmpAdditionLogRow.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, EmpAdditionLogRow.OrganID.Value)
            If EmpAdditionLogRow.DeptName.Updated Then db.AddInParameter(dbcmd, "@DeptName", DbType.String, EmpAdditionLogRow.DeptName.Value)
            If EmpAdditionLogRow.OrganName.Updated Then db.AddInParameter(dbcmd, "@OrganName", DbType.String, EmpAdditionLogRow.OrganName.Value)
            If EmpAdditionLogRow.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, EmpAdditionLogRow.RankID.Value)
            If EmpAdditionLogRow.TitleName.Updated Then db.AddInParameter(dbcmd, "@TitleName", DbType.String, EmpAdditionLogRow.TitleName.Value)
            If EmpAdditionLogRow.PositionID.Updated Then db.AddInParameter(dbcmd, "@PositionID", DbType.String, EmpAdditionLogRow.PositionID.Value)
            If EmpAdditionLogRow.PositionName.Updated Then db.AddInParameter(dbcmd, "@PositionName", DbType.String, EmpAdditionLogRow.PositionName.Value)
            If EmpAdditionLogRow.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, EmpAdditionLogRow.WorkTypeID.Value)
            If EmpAdditionLogRow.WorkTypeName.Updated Then db.AddInParameter(dbcmd, "@WorkTypeName", DbType.String, EmpAdditionLogRow.WorkTypeName.Value)
            If EmpAdditionLogRow.HoldingRankID.Updated Then db.AddInParameter(dbcmd, "@HoldingRankID", DbType.String, EmpAdditionLogRow.HoldingRankID.Value)
            If EmpAdditionLogRow.HoldingTitleName.Updated Then db.AddInParameter(dbcmd, "@HoldingTitleName", DbType.String, EmpAdditionLogRow.HoldingTitleName.Value)
            If EmpAdditionLogRow.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, EmpAdditionLogRow.Remark.Value)
            If EmpAdditionLogRow.FileNo.Updated Then db.AddInParameter(dbcmd, "@FileNo", DbType.String, EmpAdditionLogRow.FileNo.Value)
            If EmpAdditionLogRow.IsBoss.Updated Then db.AddInParameter(dbcmd, "@IsBoss", DbType.String, EmpAdditionLogRow.IsBoss.Value)
            If EmpAdditionLogRow.IsSecBoss.Updated Then db.AddInParameter(dbcmd, "@IsSecBoss", DbType.String, EmpAdditionLogRow.IsSecBoss.Value)
            If EmpAdditionLogRow.IsGroupBoss.Updated Then db.AddInParameter(dbcmd, "@IsGroupBoss", DbType.String, EmpAdditionLogRow.IsGroupBoss.Value)
            If EmpAdditionLogRow.IsSecGroupBoss.Updated Then db.AddInParameter(dbcmd, "@IsSecGroupBoss", DbType.String, EmpAdditionLogRow.IsSecGroupBoss.Value)
            If EmpAdditionLogRow.BossType.Updated Then db.AddInParameter(dbcmd, "@BossType", DbType.String, EmpAdditionLogRow.BossType.Value)
            If EmpAdditionLogRow.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(EmpAdditionLogRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), EmpAdditionLogRow.CreateDate.Value))
            If EmpAdditionLogRow.CreateComp.Updated Then db.AddInParameter(dbcmd, "@CreateComp", DbType.String, EmpAdditionLogRow.CreateComp.Value)
            If EmpAdditionLogRow.CreateID.Updated Then db.AddInParameter(dbcmd, "@CreateID", DbType.String, EmpAdditionLogRow.CreateID.Value)
            If EmpAdditionLogRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpAdditionLogRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpAdditionLogRow.LastChgDate.Value))
            If EmpAdditionLogRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpAdditionLogRow.LastChgComp.Value)
            If EmpAdditionLogRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpAdditionLogRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@PKValidDate", DbType.Date, IIf(EmpAdditionLogRow.LoadFromDataRow, EmpAdditionLogRow.ValidDate.OldValue, EmpAdditionLogRow.ValidDate.Value))
            db.AddInParameter(dbcmd, "@PKReason", DbType.String, IIf(EmpAdditionLogRow.LoadFromDataRow, EmpAdditionLogRow.Reason.OldValue, EmpAdditionLogRow.Reason.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(EmpAdditionLogRow.LoadFromDataRow, EmpAdditionLogRow.CompID.OldValue, EmpAdditionLogRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(EmpAdditionLogRow.LoadFromDataRow, EmpAdditionLogRow.EmpID.OldValue, EmpAdditionLogRow.EmpID.Value))
            db.AddInParameter(dbcmd, "@PKAddCompID", DbType.String, IIf(EmpAdditionLogRow.LoadFromDataRow, EmpAdditionLogRow.AddCompID.OldValue, EmpAdditionLogRow.AddCompID.Value))
            db.AddInParameter(dbcmd, "@PKAddDeptID", DbType.String, IIf(EmpAdditionLogRow.LoadFromDataRow, EmpAdditionLogRow.AddDeptID.OldValue, EmpAdditionLogRow.AddDeptID.Value))
            db.AddInParameter(dbcmd, "@PKAddOrganID", DbType.String, IIf(EmpAdditionLogRow.LoadFromDataRow, EmpAdditionLogRow.AddOrganID.OldValue, EmpAdditionLogRow.AddOrganID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal EmpAdditionLogRow As Row()) As Integer
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
                    For Each r As Row In EmpAdditionLogRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update EmpAdditionLog Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where ValidDate = @PKValidDate")
                        strSQL.AppendLine("And Reason = @PKReason")
                        strSQL.AppendLine("And CompID = @PKCompID")
                        strSQL.AppendLine("And EmpID = @PKEmpID")
                        strSQL.AppendLine("And AddCompID = @PKAddCompID")
                        strSQL.AppendLine("And AddDeptID = @PKAddDeptID")
                        strSQL.AppendLine("And AddOrganID = @PKAddOrganID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.ValidDate.Updated Then db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(r.ValidDate.Value), Convert.ToDateTime("1900/1/1"), r.ValidDate.Value))
                        If r.Reason.Updated Then db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        If r.AddCompID.Updated Then db.AddInParameter(dbcmd, "@AddCompID", DbType.String, r.AddCompID.Value)
                        If r.AddDeptID.Updated Then db.AddInParameter(dbcmd, "@AddDeptID", DbType.String, r.AddDeptID.Value)
                        If r.AddOrganID.Updated Then db.AddInParameter(dbcmd, "@AddOrganID", DbType.String, r.AddOrganID.Value)
                        If r.AddFlowOrganID.Updated Then db.AddInParameter(dbcmd, "@AddFlowOrganID", DbType.String, r.AddFlowOrganID.Value)
                        If r.AddDeptName.Updated Then db.AddInParameter(dbcmd, "@AddDeptName", DbType.String, r.AddDeptName.Value)
                        If r.AddOrganName.Updated Then db.AddInParameter(dbcmd, "@AddOrganName", DbType.String, r.AddOrganName.Value)
                        If r.AddFlowOrganName.Updated Then db.AddInParameter(dbcmd, "@AddFlowOrganName", DbType.String, r.AddFlowOrganName.Value)
                        If r.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                        If r.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                        If r.DeptName.Updated Then db.AddInParameter(dbcmd, "@DeptName", DbType.String, r.DeptName.Value)
                        If r.OrganName.Updated Then db.AddInParameter(dbcmd, "@OrganName", DbType.String, r.OrganName.Value)
                        If r.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                        If r.TitleName.Updated Then db.AddInParameter(dbcmd, "@TitleName", DbType.String, r.TitleName.Value)
                        If r.PositionID.Updated Then db.AddInParameter(dbcmd, "@PositionID", DbType.String, r.PositionID.Value)
                        If r.PositionName.Updated Then db.AddInParameter(dbcmd, "@PositionName", DbType.String, r.PositionName.Value)
                        If r.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                        If r.WorkTypeName.Updated Then db.AddInParameter(dbcmd, "@WorkTypeName", DbType.String, r.WorkTypeName.Value)
                        If r.HoldingRankID.Updated Then db.AddInParameter(dbcmd, "@HoldingRankID", DbType.String, r.HoldingRankID.Value)
                        If r.HoldingTitleName.Updated Then db.AddInParameter(dbcmd, "@HoldingTitleName", DbType.String, r.HoldingTitleName.Value)
                        If r.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                        If r.FileNo.Updated Then db.AddInParameter(dbcmd, "@FileNo", DbType.String, r.FileNo.Value)
                        If r.IsBoss.Updated Then db.AddInParameter(dbcmd, "@IsBoss", DbType.String, r.IsBoss.Value)
                        If r.IsSecBoss.Updated Then db.AddInParameter(dbcmd, "@IsSecBoss", DbType.String, r.IsSecBoss.Value)
                        If r.IsGroupBoss.Updated Then db.AddInParameter(dbcmd, "@IsGroupBoss", DbType.String, r.IsGroupBoss.Value)
                        If r.IsSecGroupBoss.Updated Then db.AddInParameter(dbcmd, "@IsSecGroupBoss", DbType.String, r.IsSecGroupBoss.Value)
                        If r.BossType.Updated Then db.AddInParameter(dbcmd, "@BossType", DbType.String, r.BossType.Value)
                        If r.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                        If r.CreateComp.Updated Then db.AddInParameter(dbcmd, "@CreateComp", DbType.String, r.CreateComp.Value)
                        If r.CreateID.Updated Then db.AddInParameter(dbcmd, "@CreateID", DbType.String, r.CreateID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        db.AddInParameter(dbcmd, "@PKValidDate", DbType.Date, IIf(r.LoadFromDataRow, r.ValidDate.OldValue, r.ValidDate.Value))
                        db.AddInParameter(dbcmd, "@PKReason", DbType.String, IIf(r.LoadFromDataRow, r.Reason.OldValue, r.Reason.Value))
                        db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                        db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))
                        db.AddInParameter(dbcmd, "@PKAddCompID", DbType.String, IIf(r.LoadFromDataRow, r.AddCompID.OldValue, r.AddCompID.Value))
                        db.AddInParameter(dbcmd, "@PKAddDeptID", DbType.String, IIf(r.LoadFromDataRow, r.AddDeptID.OldValue, r.AddDeptID.Value))
                        db.AddInParameter(dbcmd, "@PKAddOrganID", DbType.String, IIf(r.LoadFromDataRow, r.AddOrganID.OldValue, r.AddOrganID.Value))

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

        Public Function Update(ByVal EmpAdditionLogRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In EmpAdditionLogRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update EmpAdditionLog Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where ValidDate = @PKValidDate")
                strSQL.AppendLine("And Reason = @PKReason")
                strSQL.AppendLine("And CompID = @PKCompID")
                strSQL.AppendLine("And EmpID = @PKEmpID")
                strSQL.AppendLine("And AddCompID = @PKAddCompID")
                strSQL.AppendLine("And AddDeptID = @PKAddDeptID")
                strSQL.AppendLine("And AddOrganID = @PKAddOrganID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.ValidDate.Updated Then db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(r.ValidDate.Value), Convert.ToDateTime("1900/1/1"), r.ValidDate.Value))
                If r.Reason.Updated Then db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                If r.AddCompID.Updated Then db.AddInParameter(dbcmd, "@AddCompID", DbType.String, r.AddCompID.Value)
                If r.AddDeptID.Updated Then db.AddInParameter(dbcmd, "@AddDeptID", DbType.String, r.AddDeptID.Value)
                If r.AddOrganID.Updated Then db.AddInParameter(dbcmd, "@AddOrganID", DbType.String, r.AddOrganID.Value)
                If r.AddFlowOrganID.Updated Then db.AddInParameter(dbcmd, "@AddFlowOrganID", DbType.String, r.AddFlowOrganID.Value)
                If r.AddDeptName.Updated Then db.AddInParameter(dbcmd, "@AddDeptName", DbType.String, r.AddDeptName.Value)
                If r.AddOrganName.Updated Then db.AddInParameter(dbcmd, "@AddOrganName", DbType.String, r.AddOrganName.Value)
                If r.AddFlowOrganName.Updated Then db.AddInParameter(dbcmd, "@AddFlowOrganName", DbType.String, r.AddFlowOrganName.Value)
                If r.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                If r.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                If r.DeptName.Updated Then db.AddInParameter(dbcmd, "@DeptName", DbType.String, r.DeptName.Value)
                If r.OrganName.Updated Then db.AddInParameter(dbcmd, "@OrganName", DbType.String, r.OrganName.Value)
                If r.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                If r.TitleName.Updated Then db.AddInParameter(dbcmd, "@TitleName", DbType.String, r.TitleName.Value)
                If r.PositionID.Updated Then db.AddInParameter(dbcmd, "@PositionID", DbType.String, r.PositionID.Value)
                If r.PositionName.Updated Then db.AddInParameter(dbcmd, "@PositionName", DbType.String, r.PositionName.Value)
                If r.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                If r.WorkTypeName.Updated Then db.AddInParameter(dbcmd, "@WorkTypeName", DbType.String, r.WorkTypeName.Value)
                If r.HoldingRankID.Updated Then db.AddInParameter(dbcmd, "@HoldingRankID", DbType.String, r.HoldingRankID.Value)
                If r.HoldingTitleName.Updated Then db.AddInParameter(dbcmd, "@HoldingTitleName", DbType.String, r.HoldingTitleName.Value)
                If r.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                If r.FileNo.Updated Then db.AddInParameter(dbcmd, "@FileNo", DbType.String, r.FileNo.Value)
                If r.IsBoss.Updated Then db.AddInParameter(dbcmd, "@IsBoss", DbType.String, r.IsBoss.Value)
                If r.IsSecBoss.Updated Then db.AddInParameter(dbcmd, "@IsSecBoss", DbType.String, r.IsSecBoss.Value)
                If r.IsGroupBoss.Updated Then db.AddInParameter(dbcmd, "@IsGroupBoss", DbType.String, r.IsGroupBoss.Value)
                If r.IsSecGroupBoss.Updated Then db.AddInParameter(dbcmd, "@IsSecGroupBoss", DbType.String, r.IsSecGroupBoss.Value)
                If r.BossType.Updated Then db.AddInParameter(dbcmd, "@BossType", DbType.String, r.BossType.Value)
                If r.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                If r.CreateComp.Updated Then db.AddInParameter(dbcmd, "@CreateComp", DbType.String, r.CreateComp.Value)
                If r.CreateID.Updated Then db.AddInParameter(dbcmd, "@CreateID", DbType.String, r.CreateID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                db.AddInParameter(dbcmd, "@PKValidDate", DbType.Date, IIf(r.LoadFromDataRow, r.ValidDate.OldValue, r.ValidDate.Value))
                db.AddInParameter(dbcmd, "@PKReason", DbType.String, IIf(r.LoadFromDataRow, r.Reason.OldValue, r.Reason.Value))
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))
                db.AddInParameter(dbcmd, "@PKAddCompID", DbType.String, IIf(r.LoadFromDataRow, r.AddCompID.OldValue, r.AddCompID.Value))
                db.AddInParameter(dbcmd, "@PKAddDeptID", DbType.String, IIf(r.LoadFromDataRow, r.AddDeptID.OldValue, r.AddDeptID.Value))
                db.AddInParameter(dbcmd, "@PKAddOrganID", DbType.String, IIf(r.LoadFromDataRow, r.AddOrganID.OldValue, r.AddOrganID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal EmpAdditionLogRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From EmpAdditionLog")
            strSQL.AppendLine("Where ValidDate = @ValidDate")
            strSQL.AppendLine("And Reason = @Reason")
            strSQL.AppendLine("And CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And AddCompID = @AddCompID")
            strSQL.AppendLine("And AddDeptID = @AddDeptID")
            strSQL.AppendLine("And AddOrganID = @AddOrganID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, EmpAdditionLogRow.ValidDate.Value)
            db.AddInParameter(dbcmd, "@Reason", DbType.String, EmpAdditionLogRow.Reason.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpAdditionLogRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpAdditionLogRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@AddCompID", DbType.String, EmpAdditionLogRow.AddCompID.Value)
            db.AddInParameter(dbcmd, "@AddDeptID", DbType.String, EmpAdditionLogRow.AddDeptID.Value)
            db.AddInParameter(dbcmd, "@AddOrganID", DbType.String, EmpAdditionLogRow.AddOrganID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal EmpAdditionLogRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From EmpAdditionLog")
            strSQL.AppendLine("Where ValidDate = @ValidDate")
            strSQL.AppendLine("And Reason = @Reason")
            strSQL.AppendLine("And CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And AddCompID = @AddCompID")
            strSQL.AppendLine("And AddDeptID = @AddDeptID")
            strSQL.AppendLine("And AddOrganID = @AddOrganID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, EmpAdditionLogRow.ValidDate.Value)
            db.AddInParameter(dbcmd, "@Reason", DbType.String, EmpAdditionLogRow.Reason.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpAdditionLogRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpAdditionLogRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@AddCompID", DbType.String, EmpAdditionLogRow.AddCompID.Value)
            db.AddInParameter(dbcmd, "@AddDeptID", DbType.String, EmpAdditionLogRow.AddDeptID.Value)
            db.AddInParameter(dbcmd, "@AddOrganID", DbType.String, EmpAdditionLogRow.AddOrganID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmpAdditionLog")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal EmpAdditionLogRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into EmpAdditionLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    ValidDate, Reason, CompID, EmpID, AddCompID, AddDeptID, AddOrganID, AddFlowOrganID,")
            strSQL.AppendLine("    AddDeptName, AddOrganName, AddFlowOrganName, DeptID, OrganID, DeptName, OrganName, RankID,")
            strSQL.AppendLine("    TitleName, PositionID, PositionName, WorkTypeID, WorkTypeName, HoldingRankID, HoldingTitleName,")
            strSQL.AppendLine("    Remark, FileNo, IsBoss, IsSecBoss, IsGroupBoss, IsSecGroupBoss, BossType, CreateDate,")
            strSQL.AppendLine("    CreateComp, CreateID, LastChgDate, LastChgComp, LastChgID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @ValidDate, @Reason, @CompID, @EmpID, @AddCompID, @AddDeptID, @AddOrganID, @AddFlowOrganID,")
            strSQL.AppendLine("    @AddDeptName, @AddOrganName, @AddFlowOrganName, @DeptID, @OrganID, @DeptName, @OrganName, @RankID,")
            strSQL.AppendLine("    @TitleName, @PositionID, @PositionName, @WorkTypeID, @WorkTypeName, @HoldingRankID, @HoldingTitleName,")
            strSQL.AppendLine("    @Remark, @FileNo, @IsBoss, @IsSecBoss, @IsGroupBoss, @IsSecGroupBoss, @BossType, @CreateDate,")
            strSQL.AppendLine("    @CreateComp, @CreateID, @LastChgDate, @LastChgComp, @LastChgID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(EmpAdditionLogRow.ValidDate.Value), Convert.ToDateTime("1900/1/1"), EmpAdditionLogRow.ValidDate.Value))
            db.AddInParameter(dbcmd, "@Reason", DbType.String, EmpAdditionLogRow.Reason.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpAdditionLogRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpAdditionLogRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@AddCompID", DbType.String, EmpAdditionLogRow.AddCompID.Value)
            db.AddInParameter(dbcmd, "@AddDeptID", DbType.String, EmpAdditionLogRow.AddDeptID.Value)
            db.AddInParameter(dbcmd, "@AddOrganID", DbType.String, EmpAdditionLogRow.AddOrganID.Value)
            db.AddInParameter(dbcmd, "@AddFlowOrganID", DbType.String, EmpAdditionLogRow.AddFlowOrganID.Value)
            db.AddInParameter(dbcmd, "@AddDeptName", DbType.String, EmpAdditionLogRow.AddDeptName.Value)
            db.AddInParameter(dbcmd, "@AddOrganName", DbType.String, EmpAdditionLogRow.AddOrganName.Value)
            db.AddInParameter(dbcmd, "@AddFlowOrganName", DbType.String, EmpAdditionLogRow.AddFlowOrganName.Value)
            db.AddInParameter(dbcmd, "@DeptID", DbType.String, EmpAdditionLogRow.DeptID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, EmpAdditionLogRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@DeptName", DbType.String, EmpAdditionLogRow.DeptName.Value)
            db.AddInParameter(dbcmd, "@OrganName", DbType.String, EmpAdditionLogRow.OrganName.Value)
            db.AddInParameter(dbcmd, "@RankID", DbType.String, EmpAdditionLogRow.RankID.Value)
            db.AddInParameter(dbcmd, "@TitleName", DbType.String, EmpAdditionLogRow.TitleName.Value)
            db.AddInParameter(dbcmd, "@PositionID", DbType.String, EmpAdditionLogRow.PositionID.Value)
            db.AddInParameter(dbcmd, "@PositionName", DbType.String, EmpAdditionLogRow.PositionName.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, EmpAdditionLogRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@WorkTypeName", DbType.String, EmpAdditionLogRow.WorkTypeName.Value)
            db.AddInParameter(dbcmd, "@HoldingRankID", DbType.String, EmpAdditionLogRow.HoldingRankID.Value)
            db.AddInParameter(dbcmd, "@HoldingTitleName", DbType.String, EmpAdditionLogRow.HoldingTitleName.Value)
            db.AddInParameter(dbcmd, "@Remark", DbType.String, EmpAdditionLogRow.Remark.Value)
            db.AddInParameter(dbcmd, "@FileNo", DbType.String, EmpAdditionLogRow.FileNo.Value)
            db.AddInParameter(dbcmd, "@IsBoss", DbType.String, EmpAdditionLogRow.IsBoss.Value)
            db.AddInParameter(dbcmd, "@IsSecBoss", DbType.String, EmpAdditionLogRow.IsSecBoss.Value)
            db.AddInParameter(dbcmd, "@IsGroupBoss", DbType.String, EmpAdditionLogRow.IsGroupBoss.Value)
            db.AddInParameter(dbcmd, "@IsSecGroupBoss", DbType.String, EmpAdditionLogRow.IsSecGroupBoss.Value)
            db.AddInParameter(dbcmd, "@BossType", DbType.String, EmpAdditionLogRow.BossType.Value)
            db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(EmpAdditionLogRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), EmpAdditionLogRow.CreateDate.Value))
            db.AddInParameter(dbcmd, "@CreateComp", DbType.String, EmpAdditionLogRow.CreateComp.Value)
            db.AddInParameter(dbcmd, "@CreateID", DbType.String, EmpAdditionLogRow.CreateID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpAdditionLogRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpAdditionLogRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpAdditionLogRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpAdditionLogRow.LastChgID.Value)

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal EmpAdditionLogRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into EmpAdditionLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    ValidDate, Reason, CompID, EmpID, AddCompID, AddDeptID, AddOrganID, AddFlowOrganID,")
            strSQL.AppendLine("    AddDeptName, AddOrganName, AddFlowOrganName, DeptID, OrganID, DeptName, OrganName, RankID,")
            strSQL.AppendLine("    TitleName, PositionID, PositionName, WorkTypeID, WorkTypeName, HoldingRankID, HoldingTitleName,")
            strSQL.AppendLine("    Remark, FileNo, IsBoss, IsSecBoss, IsGroupBoss, IsSecGroupBoss, BossType, CreateDate,")
            strSQL.AppendLine("    CreateComp, CreateID, LastChgDate, LastChgComp, LastChgID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @ValidDate, @Reason, @CompID, @EmpID, @AddCompID, @AddDeptID, @AddOrganID, @AddFlowOrganID,")
            strSQL.AppendLine("    @AddDeptName, @AddOrganName, @AddFlowOrganName, @DeptID, @OrganID, @DeptName, @OrganName, @RankID,")
            strSQL.AppendLine("    @TitleName, @PositionID, @PositionName, @WorkTypeID, @WorkTypeName, @HoldingRankID, @HoldingTitleName,")
            strSQL.AppendLine("    @Remark, @FileNo, @IsBoss, @IsSecBoss, @IsGroupBoss, @IsSecGroupBoss, @BossType, @CreateDate,")
            strSQL.AppendLine("    @CreateComp, @CreateID, @LastChgDate, @LastChgComp, @LastChgID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(EmpAdditionLogRow.ValidDate.Value), Convert.ToDateTime("1900/1/1"), EmpAdditionLogRow.ValidDate.Value))
            db.AddInParameter(dbcmd, "@Reason", DbType.String, EmpAdditionLogRow.Reason.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpAdditionLogRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpAdditionLogRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@AddCompID", DbType.String, EmpAdditionLogRow.AddCompID.Value)
            db.AddInParameter(dbcmd, "@AddDeptID", DbType.String, EmpAdditionLogRow.AddDeptID.Value)
            db.AddInParameter(dbcmd, "@AddOrganID", DbType.String, EmpAdditionLogRow.AddOrganID.Value)
            db.AddInParameter(dbcmd, "@AddFlowOrganID", DbType.String, EmpAdditionLogRow.AddFlowOrganID.Value)
            db.AddInParameter(dbcmd, "@AddDeptName", DbType.String, EmpAdditionLogRow.AddDeptName.Value)
            db.AddInParameter(dbcmd, "@AddOrganName", DbType.String, EmpAdditionLogRow.AddOrganName.Value)
            db.AddInParameter(dbcmd, "@AddFlowOrganName", DbType.String, EmpAdditionLogRow.AddFlowOrganName.Value)
            db.AddInParameter(dbcmd, "@DeptID", DbType.String, EmpAdditionLogRow.DeptID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, EmpAdditionLogRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@DeptName", DbType.String, EmpAdditionLogRow.DeptName.Value)
            db.AddInParameter(dbcmd, "@OrganName", DbType.String, EmpAdditionLogRow.OrganName.Value)
            db.AddInParameter(dbcmd, "@RankID", DbType.String, EmpAdditionLogRow.RankID.Value)
            db.AddInParameter(dbcmd, "@TitleName", DbType.String, EmpAdditionLogRow.TitleName.Value)
            db.AddInParameter(dbcmd, "@PositionID", DbType.String, EmpAdditionLogRow.PositionID.Value)
            db.AddInParameter(dbcmd, "@PositionName", DbType.String, EmpAdditionLogRow.PositionName.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, EmpAdditionLogRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@WorkTypeName", DbType.String, EmpAdditionLogRow.WorkTypeName.Value)
            db.AddInParameter(dbcmd, "@HoldingRankID", DbType.String, EmpAdditionLogRow.HoldingRankID.Value)
            db.AddInParameter(dbcmd, "@HoldingTitleName", DbType.String, EmpAdditionLogRow.HoldingTitleName.Value)
            db.AddInParameter(dbcmd, "@Remark", DbType.String, EmpAdditionLogRow.Remark.Value)
            db.AddInParameter(dbcmd, "@FileNo", DbType.String, EmpAdditionLogRow.FileNo.Value)
            db.AddInParameter(dbcmd, "@IsBoss", DbType.String, EmpAdditionLogRow.IsBoss.Value)
            db.AddInParameter(dbcmd, "@IsSecBoss", DbType.String, EmpAdditionLogRow.IsSecBoss.Value)
            db.AddInParameter(dbcmd, "@IsGroupBoss", DbType.String, EmpAdditionLogRow.IsGroupBoss.Value)
            db.AddInParameter(dbcmd, "@IsSecGroupBoss", DbType.String, EmpAdditionLogRow.IsSecGroupBoss.Value)
            db.AddInParameter(dbcmd, "@BossType", DbType.String, EmpAdditionLogRow.BossType.Value)
            db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(EmpAdditionLogRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), EmpAdditionLogRow.CreateDate.Value))
            db.AddInParameter(dbcmd, "@CreateComp", DbType.String, EmpAdditionLogRow.CreateComp.Value)
            db.AddInParameter(dbcmd, "@CreateID", DbType.String, EmpAdditionLogRow.CreateID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpAdditionLogRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpAdditionLogRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpAdditionLogRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpAdditionLogRow.LastChgID.Value)

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal EmpAdditionLogRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into EmpAdditionLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    ValidDate, Reason, CompID, EmpID, AddCompID, AddDeptID, AddOrganID, AddFlowOrganID,")
            strSQL.AppendLine("    AddDeptName, AddOrganName, AddFlowOrganName, DeptID, OrganID, DeptName, OrganName, RankID,")
            strSQL.AppendLine("    TitleName, PositionID, PositionName, WorkTypeID, WorkTypeName, HoldingRankID, HoldingTitleName,")
            strSQL.AppendLine("    Remark, FileNo, IsBoss, IsSecBoss, IsGroupBoss, IsSecGroupBoss, BossType, CreateDate,")
            strSQL.AppendLine("    CreateComp, CreateID, LastChgDate, LastChgComp, LastChgID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @ValidDate, @Reason, @CompID, @EmpID, @AddCompID, @AddDeptID, @AddOrganID, @AddFlowOrganID,")
            strSQL.AppendLine("    @AddDeptName, @AddOrganName, @AddFlowOrganName, @DeptID, @OrganID, @DeptName, @OrganName, @RankID,")
            strSQL.AppendLine("    @TitleName, @PositionID, @PositionName, @WorkTypeID, @WorkTypeName, @HoldingRankID, @HoldingTitleName,")
            strSQL.AppendLine("    @Remark, @FileNo, @IsBoss, @IsSecBoss, @IsGroupBoss, @IsSecGroupBoss, @BossType, @CreateDate,")
            strSQL.AppendLine("    @CreateComp, @CreateID, @LastChgDate, @LastChgComp, @LastChgID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In EmpAdditionLogRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(r.ValidDate.Value), Convert.ToDateTime("1900/1/1"), r.ValidDate.Value))
                        db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@AddCompID", DbType.String, r.AddCompID.Value)
                        db.AddInParameter(dbcmd, "@AddDeptID", DbType.String, r.AddDeptID.Value)
                        db.AddInParameter(dbcmd, "@AddOrganID", DbType.String, r.AddOrganID.Value)
                        db.AddInParameter(dbcmd, "@AddFlowOrganID", DbType.String, r.AddFlowOrganID.Value)
                        db.AddInParameter(dbcmd, "@AddDeptName", DbType.String, r.AddDeptName.Value)
                        db.AddInParameter(dbcmd, "@AddOrganName", DbType.String, r.AddOrganName.Value)
                        db.AddInParameter(dbcmd, "@AddFlowOrganName", DbType.String, r.AddFlowOrganName.Value)
                        db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                        db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                        db.AddInParameter(dbcmd, "@DeptName", DbType.String, r.DeptName.Value)
                        db.AddInParameter(dbcmd, "@OrganName", DbType.String, r.OrganName.Value)
                        db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                        db.AddInParameter(dbcmd, "@TitleName", DbType.String, r.TitleName.Value)
                        db.AddInParameter(dbcmd, "@PositionID", DbType.String, r.PositionID.Value)
                        db.AddInParameter(dbcmd, "@PositionName", DbType.String, r.PositionName.Value)
                        db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                        db.AddInParameter(dbcmd, "@WorkTypeName", DbType.String, r.WorkTypeName.Value)
                        db.AddInParameter(dbcmd, "@HoldingRankID", DbType.String, r.HoldingRankID.Value)
                        db.AddInParameter(dbcmd, "@HoldingTitleName", DbType.String, r.HoldingTitleName.Value)
                        db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                        db.AddInParameter(dbcmd, "@FileNo", DbType.String, r.FileNo.Value)
                        db.AddInParameter(dbcmd, "@IsBoss", DbType.String, r.IsBoss.Value)
                        db.AddInParameter(dbcmd, "@IsSecBoss", DbType.String, r.IsSecBoss.Value)
                        db.AddInParameter(dbcmd, "@IsGroupBoss", DbType.String, r.IsGroupBoss.Value)
                        db.AddInParameter(dbcmd, "@IsSecGroupBoss", DbType.String, r.IsSecGroupBoss.Value)
                        db.AddInParameter(dbcmd, "@BossType", DbType.String, r.BossType.Value)
                        db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                        db.AddInParameter(dbcmd, "@CreateComp", DbType.String, r.CreateComp.Value)
                        db.AddInParameter(dbcmd, "@CreateID", DbType.String, r.CreateID.Value)
                        db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)

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

        Public Function Insert(ByVal EmpAdditionLogRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into EmpAdditionLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    ValidDate, Reason, CompID, EmpID, AddCompID, AddDeptID, AddOrganID, AddFlowOrganID,")
            strSQL.AppendLine("    AddDeptName, AddOrganName, AddFlowOrganName, DeptID, OrganID, DeptName, OrganName, RankID,")
            strSQL.AppendLine("    TitleName, PositionID, PositionName, WorkTypeID, WorkTypeName, HoldingRankID, HoldingTitleName,")
            strSQL.AppendLine("    Remark, FileNo, IsBoss, IsSecBoss, IsGroupBoss, IsSecGroupBoss, BossType, CreateDate,")
            strSQL.AppendLine("    CreateComp, CreateID, LastChgDate, LastChgComp, LastChgID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @ValidDate, @Reason, @CompID, @EmpID, @AddCompID, @AddDeptID, @AddOrganID, @AddFlowOrganID,")
            strSQL.AppendLine("    @AddDeptName, @AddOrganName, @AddFlowOrganName, @DeptID, @OrganID, @DeptName, @OrganName, @RankID,")
            strSQL.AppendLine("    @TitleName, @PositionID, @PositionName, @WorkTypeID, @WorkTypeName, @HoldingRankID, @HoldingTitleName,")
            strSQL.AppendLine("    @Remark, @FileNo, @IsBoss, @IsSecBoss, @IsGroupBoss, @IsSecGroupBoss, @BossType, @CreateDate,")
            strSQL.AppendLine("    @CreateComp, @CreateID, @LastChgDate, @LastChgComp, @LastChgID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In EmpAdditionLogRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(r.ValidDate.Value), Convert.ToDateTime("1900/1/1"), r.ValidDate.Value))
                db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@AddCompID", DbType.String, r.AddCompID.Value)
                db.AddInParameter(dbcmd, "@AddDeptID", DbType.String, r.AddDeptID.Value)
                db.AddInParameter(dbcmd, "@AddOrganID", DbType.String, r.AddOrganID.Value)
                db.AddInParameter(dbcmd, "@AddFlowOrganID", DbType.String, r.AddFlowOrganID.Value)
                db.AddInParameter(dbcmd, "@AddDeptName", DbType.String, r.AddDeptName.Value)
                db.AddInParameter(dbcmd, "@AddOrganName", DbType.String, r.AddOrganName.Value)
                db.AddInParameter(dbcmd, "@AddFlowOrganName", DbType.String, r.AddFlowOrganName.Value)
                db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                db.AddInParameter(dbcmd, "@DeptName", DbType.String, r.DeptName.Value)
                db.AddInParameter(dbcmd, "@OrganName", DbType.String, r.OrganName.Value)
                db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                db.AddInParameter(dbcmd, "@TitleName", DbType.String, r.TitleName.Value)
                db.AddInParameter(dbcmd, "@PositionID", DbType.String, r.PositionID.Value)
                db.AddInParameter(dbcmd, "@PositionName", DbType.String, r.PositionName.Value)
                db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                db.AddInParameter(dbcmd, "@WorkTypeName", DbType.String, r.WorkTypeName.Value)
                db.AddInParameter(dbcmd, "@HoldingRankID", DbType.String, r.HoldingRankID.Value)
                db.AddInParameter(dbcmd, "@HoldingTitleName", DbType.String, r.HoldingTitleName.Value)
                db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                db.AddInParameter(dbcmd, "@FileNo", DbType.String, r.FileNo.Value)
                db.AddInParameter(dbcmd, "@IsBoss", DbType.String, r.IsBoss.Value)
                db.AddInParameter(dbcmd, "@IsSecBoss", DbType.String, r.IsSecBoss.Value)
                db.AddInParameter(dbcmd, "@IsGroupBoss", DbType.String, r.IsGroupBoss.Value)
                db.AddInParameter(dbcmd, "@IsSecGroupBoss", DbType.String, r.IsSecGroupBoss.Value)
                db.AddInParameter(dbcmd, "@BossType", DbType.String, r.BossType.Value)
                db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                db.AddInParameter(dbcmd, "@CreateComp", DbType.String, r.CreateComp.Value)
                db.AddInParameter(dbcmd, "@CreateID", DbType.String, r.CreateID.Value)
                db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)

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

