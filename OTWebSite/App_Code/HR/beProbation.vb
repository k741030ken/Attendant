'****************************************************************
' Table:Probation
' Created Date: 2015.08.18
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beProbation
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "ApplyTime", "ApplyID", "ProbSeq", "ProbDate", "ProbDate2", "DeptID", "DeptName", "OrganID", "OrganName" _
                                    , "PositionID", "PositionName", "WorkTypeName", "RankID", "TitleName", "EmpDate", "ExtendFlag", "Learn1", "Learn2", "Learn3", "Attitude1" _
                                    , "Attitude2", "Relation1", "Relation2", "Ability1", "Ability2", "Ability3", "Ability4", "Leader1", "Leader2", "Leader3", "Leader4" _
                                    , "Leader5", "Attachment", "Result", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(Date), GetType(String), GetType(Integer), GetType(Date), GetType(Date), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "CompID", "ApplyTime", "ApplyID" }

        Public ReadOnly Property Rows() As beProbation.Rows 
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
        Public Sub Transfer2Row(ProbationTable As DataTable)
            For Each dr As DataRow In ProbationTable.Rows
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
                dr(m_Rows(i).ApplyTime.FieldName) = m_Rows(i).ApplyTime.Value
                dr(m_Rows(i).ApplyID.FieldName) = m_Rows(i).ApplyID.Value
                dr(m_Rows(i).ProbSeq.FieldName) = m_Rows(i).ProbSeq.Value
                dr(m_Rows(i).ProbDate.FieldName) = m_Rows(i).ProbDate.Value
                dr(m_Rows(i).ProbDate2.FieldName) = m_Rows(i).ProbDate2.Value
                dr(m_Rows(i).DeptID.FieldName) = m_Rows(i).DeptID.Value
                dr(m_Rows(i).DeptName.FieldName) = m_Rows(i).DeptName.Value
                dr(m_Rows(i).OrganID.FieldName) = m_Rows(i).OrganID.Value
                dr(m_Rows(i).OrganName.FieldName) = m_Rows(i).OrganName.Value
                dr(m_Rows(i).PositionID.FieldName) = m_Rows(i).PositionID.Value
                dr(m_Rows(i).PositionName.FieldName) = m_Rows(i).PositionName.Value
                dr(m_Rows(i).WorkTypeName.FieldName) = m_Rows(i).WorkTypeName.Value
                dr(m_Rows(i).RankID.FieldName) = m_Rows(i).RankID.Value
                dr(m_Rows(i).TitleName.FieldName) = m_Rows(i).TitleName.Value
                dr(m_Rows(i).EmpDate.FieldName) = m_Rows(i).EmpDate.Value
                dr(m_Rows(i).ExtendFlag.FieldName) = m_Rows(i).ExtendFlag.Value
                dr(m_Rows(i).Learn1.FieldName) = m_Rows(i).Learn1.Value
                dr(m_Rows(i).Learn2.FieldName) = m_Rows(i).Learn2.Value
                dr(m_Rows(i).Learn3.FieldName) = m_Rows(i).Learn3.Value
                dr(m_Rows(i).Attitude1.FieldName) = m_Rows(i).Attitude1.Value
                dr(m_Rows(i).Attitude2.FieldName) = m_Rows(i).Attitude2.Value
                dr(m_Rows(i).Relation1.FieldName) = m_Rows(i).Relation1.Value
                dr(m_Rows(i).Relation2.FieldName) = m_Rows(i).Relation2.Value
                dr(m_Rows(i).Ability1.FieldName) = m_Rows(i).Ability1.Value
                dr(m_Rows(i).Ability2.FieldName) = m_Rows(i).Ability2.Value
                dr(m_Rows(i).Ability3.FieldName) = m_Rows(i).Ability3.Value
                dr(m_Rows(i).Ability4.FieldName) = m_Rows(i).Ability4.Value
                dr(m_Rows(i).Leader1.FieldName) = m_Rows(i).Leader1.Value
                dr(m_Rows(i).Leader2.FieldName) = m_Rows(i).Leader2.Value
                dr(m_Rows(i).Leader3.FieldName) = m_Rows(i).Leader3.Value
                dr(m_Rows(i).Leader4.FieldName) = m_Rows(i).Leader4.Value
                dr(m_Rows(i).Leader5.FieldName) = m_Rows(i).Leader5.Value
                dr(m_Rows(i).Attachment.FieldName) = m_Rows(i).Attachment.Value
                dr(m_Rows(i).Result.FieldName) = m_Rows(i).Result.Value
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

        Public Sub Add(ProbationRow As Row)
            m_Rows.Add(ProbationRow)
        End Sub

        Public Sub Remove(ProbationRow As Row)
            If m_Rows.IndexOf(ProbationRow) >= 0 Then
                m_Rows.Remove(ProbationRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_ApplyTime As Field(Of Date) = new Field(Of Date)("ApplyTime", true)
        Private FI_ApplyID As Field(Of String) = new Field(Of String)("ApplyID", true)
        Private FI_ProbSeq As Field(Of Integer) = new Field(Of Integer)("ProbSeq", true)
        Private FI_ProbDate As Field(Of Date) = new Field(Of Date)("ProbDate", true)
        Private FI_ProbDate2 As Field(Of Date) = new Field(Of Date)("ProbDate2", true)
        Private FI_DeptID As Field(Of String) = new Field(Of String)("DeptID", true)
        Private FI_DeptName As Field(Of String) = new Field(Of String)("DeptName", true)
        Private FI_OrganID As Field(Of String) = new Field(Of String)("OrganID", true)
        Private FI_OrganName As Field(Of String) = new Field(Of String)("OrganName", true)
        Private FI_PositionID As Field(Of String) = new Field(Of String)("PositionID", true)
        Private FI_PositionName As Field(Of String) = new Field(Of String)("PositionName", true)
        Private FI_WorkTypeName As Field(Of String) = new Field(Of String)("WorkTypeName", true)
        Private FI_RankID As Field(Of String) = new Field(Of String)("RankID", true)
        Private FI_TitleName As Field(Of String) = new Field(Of String)("TitleName", true)
        Private FI_EmpDate As Field(Of Date) = new Field(Of Date)("EmpDate", true)
        Private FI_ExtendFlag As Field(Of String) = new Field(Of String)("ExtendFlag", true)
        Private FI_Learn1 As Field(Of String) = new Field(Of String)("Learn1", true)
        Private FI_Learn2 As Field(Of String) = new Field(Of String)("Learn2", true)
        Private FI_Learn3 As Field(Of String) = new Field(Of String)("Learn3", true)
        Private FI_Attitude1 As Field(Of String) = new Field(Of String)("Attitude1", true)
        Private FI_Attitude2 As Field(Of String) = new Field(Of String)("Attitude2", true)
        Private FI_Relation1 As Field(Of String) = new Field(Of String)("Relation1", true)
        Private FI_Relation2 As Field(Of String) = new Field(Of String)("Relation2", true)
        Private FI_Ability1 As Field(Of String) = new Field(Of String)("Ability1", true)
        Private FI_Ability2 As Field(Of String) = new Field(Of String)("Ability2", true)
        Private FI_Ability3 As Field(Of String) = new Field(Of String)("Ability3", true)
        Private FI_Ability4 As Field(Of String) = new Field(Of String)("Ability4", true)
        Private FI_Leader1 As Field(Of String) = new Field(Of String)("Leader1", true)
        Private FI_Leader2 As Field(Of String) = new Field(Of String)("Leader2", true)
        Private FI_Leader3 As Field(Of String) = new Field(Of String)("Leader3", true)
        Private FI_Leader4 As Field(Of String) = new Field(Of String)("Leader4", true)
        Private FI_Leader5 As Field(Of String) = new Field(Of String)("Leader5", true)
        Private FI_Attachment As Field(Of String) = new Field(Of String)("Attachment", true)
        Private FI_Result As Field(Of String) = new Field(Of String)("Result", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "CompID", "ApplyTime", "ApplyID", "ProbSeq", "ProbDate", "ProbDate2", "DeptID", "DeptName", "OrganID", "OrganName" _
                                    , "PositionID", "PositionName", "WorkTypeName", "RankID", "TitleName", "EmpDate", "ExtendFlag", "Learn1", "Learn2", "Learn3", "Attitude1" _
                                    , "Attitude2", "Relation1", "Relation2", "Ability1", "Ability2", "Ability3", "Ability4", "Leader1", "Leader2", "Leader3", "Leader4" _
                                    , "Leader5", "Attachment", "Result", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "CompID", "ApplyTime", "ApplyID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CompID"
                    Return FI_CompID.Value
                Case "ApplyTime"
                    Return FI_ApplyTime.Value
                Case "ApplyID"
                    Return FI_ApplyID.Value
                Case "ProbSeq"
                    Return FI_ProbSeq.Value
                Case "ProbDate"
                    Return FI_ProbDate.Value
                Case "ProbDate2"
                    Return FI_ProbDate2.Value
                Case "DeptID"
                    Return FI_DeptID.Value
                Case "DeptName"
                    Return FI_DeptName.Value
                Case "OrganID"
                    Return FI_OrganID.Value
                Case "OrganName"
                    Return FI_OrganName.Value
                Case "PositionID"
                    Return FI_PositionID.Value
                Case "PositionName"
                    Return FI_PositionName.Value
                Case "WorkTypeName"
                    Return FI_WorkTypeName.Value
                Case "RankID"
                    Return FI_RankID.Value
                Case "TitleName"
                    Return FI_TitleName.Value
                Case "EmpDate"
                    Return FI_EmpDate.Value
                Case "ExtendFlag"
                    Return FI_ExtendFlag.Value
                Case "Learn1"
                    Return FI_Learn1.Value
                Case "Learn2"
                    Return FI_Learn2.Value
                Case "Learn3"
                    Return FI_Learn3.Value
                Case "Attitude1"
                    Return FI_Attitude1.Value
                Case "Attitude2"
                    Return FI_Attitude2.Value
                Case "Relation1"
                    Return FI_Relation1.Value
                Case "Relation2"
                    Return FI_Relation2.Value
                Case "Ability1"
                    Return FI_Ability1.Value
                Case "Ability2"
                    Return FI_Ability2.Value
                Case "Ability3"
                    Return FI_Ability3.Value
                Case "Ability4"
                    Return FI_Ability4.Value
                Case "Leader1"
                    Return FI_Leader1.Value
                Case "Leader2"
                    Return FI_Leader2.Value
                Case "Leader3"
                    Return FI_Leader3.Value
                Case "Leader4"
                    Return FI_Leader4.Value
                Case "Leader5"
                    Return FI_Leader5.Value
                Case "Attachment"
                    Return FI_Attachment.Value
                Case "Result"
                    Return FI_Result.Value
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
                Case "ApplyTime"
                    FI_ApplyTime.SetValue(value)
                Case "ApplyID"
                    FI_ApplyID.SetValue(value)
                Case "ProbSeq"
                    FI_ProbSeq.SetValue(value)
                Case "ProbDate"
                    FI_ProbDate.SetValue(value)
                Case "ProbDate2"
                    FI_ProbDate2.SetValue(value)
                Case "DeptID"
                    FI_DeptID.SetValue(value)
                Case "DeptName"
                    FI_DeptName.SetValue(value)
                Case "OrganID"
                    FI_OrganID.SetValue(value)
                Case "OrganName"
                    FI_OrganName.SetValue(value)
                Case "PositionID"
                    FI_PositionID.SetValue(value)
                Case "PositionName"
                    FI_PositionName.SetValue(value)
                Case "WorkTypeName"
                    FI_WorkTypeName.SetValue(value)
                Case "RankID"
                    FI_RankID.SetValue(value)
                Case "TitleName"
                    FI_TitleName.SetValue(value)
                Case "EmpDate"
                    FI_EmpDate.SetValue(value)
                Case "ExtendFlag"
                    FI_ExtendFlag.SetValue(value)
                Case "Learn1"
                    FI_Learn1.SetValue(value)
                Case "Learn2"
                    FI_Learn2.SetValue(value)
                Case "Learn3"
                    FI_Learn3.SetValue(value)
                Case "Attitude1"
                    FI_Attitude1.SetValue(value)
                Case "Attitude2"
                    FI_Attitude2.SetValue(value)
                Case "Relation1"
                    FI_Relation1.SetValue(value)
                Case "Relation2"
                    FI_Relation2.SetValue(value)
                Case "Ability1"
                    FI_Ability1.SetValue(value)
                Case "Ability2"
                    FI_Ability2.SetValue(value)
                Case "Ability3"
                    FI_Ability3.SetValue(value)
                Case "Ability4"
                    FI_Ability4.SetValue(value)
                Case "Leader1"
                    FI_Leader1.SetValue(value)
                Case "Leader2"
                    FI_Leader2.SetValue(value)
                Case "Leader3"
                    FI_Leader3.SetValue(value)
                Case "Leader4"
                    FI_Leader4.SetValue(value)
                Case "Leader5"
                    FI_Leader5.SetValue(value)
                Case "Attachment"
                    FI_Attachment.SetValue(value)
                Case "Result"
                    FI_Result.SetValue(value)
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
                Case "ApplyTime"
                    return FI_ApplyTime.Updated
                Case "ApplyID"
                    return FI_ApplyID.Updated
                Case "ProbSeq"
                    return FI_ProbSeq.Updated
                Case "ProbDate"
                    return FI_ProbDate.Updated
                Case "ProbDate2"
                    return FI_ProbDate2.Updated
                Case "DeptID"
                    return FI_DeptID.Updated
                Case "DeptName"
                    return FI_DeptName.Updated
                Case "OrganID"
                    return FI_OrganID.Updated
                Case "OrganName"
                    return FI_OrganName.Updated
                Case "PositionID"
                    return FI_PositionID.Updated
                Case "PositionName"
                    return FI_PositionName.Updated
                Case "WorkTypeName"
                    return FI_WorkTypeName.Updated
                Case "RankID"
                    return FI_RankID.Updated
                Case "TitleName"
                    return FI_TitleName.Updated
                Case "EmpDate"
                    return FI_EmpDate.Updated
                Case "ExtendFlag"
                    return FI_ExtendFlag.Updated
                Case "Learn1"
                    return FI_Learn1.Updated
                Case "Learn2"
                    return FI_Learn2.Updated
                Case "Learn3"
                    return FI_Learn3.Updated
                Case "Attitude1"
                    return FI_Attitude1.Updated
                Case "Attitude2"
                    return FI_Attitude2.Updated
                Case "Relation1"
                    return FI_Relation1.Updated
                Case "Relation2"
                    return FI_Relation2.Updated
                Case "Ability1"
                    return FI_Ability1.Updated
                Case "Ability2"
                    return FI_Ability2.Updated
                Case "Ability3"
                    return FI_Ability3.Updated
                Case "Ability4"
                    return FI_Ability4.Updated
                Case "Leader1"
                    return FI_Leader1.Updated
                Case "Leader2"
                    return FI_Leader2.Updated
                Case "Leader3"
                    return FI_Leader3.Updated
                Case "Leader4"
                    return FI_Leader4.Updated
                Case "Leader5"
                    return FI_Leader5.Updated
                Case "Attachment"
                    return FI_Attachment.Updated
                Case "Result"
                    return FI_Result.Updated
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
                Case "ApplyTime"
                    return FI_ApplyTime.CreateUpdateSQL
                Case "ApplyID"
                    return FI_ApplyID.CreateUpdateSQL
                Case "ProbSeq"
                    return FI_ProbSeq.CreateUpdateSQL
                Case "ProbDate"
                    return FI_ProbDate.CreateUpdateSQL
                Case "ProbDate2"
                    return FI_ProbDate2.CreateUpdateSQL
                Case "DeptID"
                    return FI_DeptID.CreateUpdateSQL
                Case "DeptName"
                    return FI_DeptName.CreateUpdateSQL
                Case "OrganID"
                    return FI_OrganID.CreateUpdateSQL
                Case "OrganName"
                    return FI_OrganName.CreateUpdateSQL
                Case "PositionID"
                    return FI_PositionID.CreateUpdateSQL
                Case "PositionName"
                    return FI_PositionName.CreateUpdateSQL
                Case "WorkTypeName"
                    return FI_WorkTypeName.CreateUpdateSQL
                Case "RankID"
                    return FI_RankID.CreateUpdateSQL
                Case "TitleName"
                    return FI_TitleName.CreateUpdateSQL
                Case "EmpDate"
                    return FI_EmpDate.CreateUpdateSQL
                Case "ExtendFlag"
                    return FI_ExtendFlag.CreateUpdateSQL
                Case "Learn1"
                    return FI_Learn1.CreateUpdateSQL
                Case "Learn2"
                    return FI_Learn2.CreateUpdateSQL
                Case "Learn3"
                    return FI_Learn3.CreateUpdateSQL
                Case "Attitude1"
                    return FI_Attitude1.CreateUpdateSQL
                Case "Attitude2"
                    return FI_Attitude2.CreateUpdateSQL
                Case "Relation1"
                    return FI_Relation1.CreateUpdateSQL
                Case "Relation2"
                    return FI_Relation2.CreateUpdateSQL
                Case "Ability1"
                    return FI_Ability1.CreateUpdateSQL
                Case "Ability2"
                    return FI_Ability2.CreateUpdateSQL
                Case "Ability3"
                    return FI_Ability3.CreateUpdateSQL
                Case "Ability4"
                    return FI_Ability4.CreateUpdateSQL
                Case "Leader1"
                    return FI_Leader1.CreateUpdateSQL
                Case "Leader2"
                    return FI_Leader2.CreateUpdateSQL
                Case "Leader3"
                    return FI_Leader3.CreateUpdateSQL
                Case "Leader4"
                    return FI_Leader4.CreateUpdateSQL
                Case "Leader5"
                    return FI_Leader5.CreateUpdateSQL
                Case "Attachment"
                    return FI_Attachment.CreateUpdateSQL
                Case "Result"
                    return FI_Result.CreateUpdateSQL
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
            FI_ApplyTime.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_ApplyID.SetInitValue("")
            FI_ProbSeq.SetInitValue(1)
            FI_ProbDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_ProbDate2.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_DeptID.SetInitValue("")
            FI_DeptName.SetInitValue("")
            FI_OrganID.SetInitValue("")
            FI_OrganName.SetInitValue("")
            FI_PositionID.SetInitValue("")
            FI_PositionName.SetInitValue("")
            FI_WorkTypeName.SetInitValue("")
            FI_RankID.SetInitValue("")
            FI_TitleName.SetInitValue("")
            FI_EmpDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_ExtendFlag.SetInitValue("0")
            FI_Learn1.SetInitValue("")
            FI_Learn2.SetInitValue("")
            FI_Learn3.SetInitValue("")
            FI_Attitude1.SetInitValue("")
            FI_Attitude2.SetInitValue("")
            FI_Relation1.SetInitValue("")
            FI_Relation2.SetInitValue("")
            FI_Ability1.SetInitValue("")
            FI_Ability2.SetInitValue("")
            FI_Ability3.SetInitValue("")
            FI_Ability4.SetInitValue("")
            FI_Leader1.SetInitValue("")
            FI_Leader2.SetInitValue("")
            FI_Leader3.SetInitValue("")
            FI_Leader4.SetInitValue("")
            FI_Leader5.SetInitValue("")
            FI_Attachment.SetInitValue("")
            FI_Result.SetInitValue("0")
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CompID.SetInitValue(dr("CompID"))
            FI_ApplyTime.SetInitValue(dr("ApplyTime"))
            FI_ApplyID.SetInitValue(dr("ApplyID"))
            FI_ProbSeq.SetInitValue(dr("ProbSeq"))
            FI_ProbDate.SetInitValue(dr("ProbDate"))
            FI_ProbDate2.SetInitValue(dr("ProbDate2"))
            FI_DeptID.SetInitValue(dr("DeptID"))
            FI_DeptName.SetInitValue(dr("DeptName"))
            FI_OrganID.SetInitValue(dr("OrganID"))
            FI_OrganName.SetInitValue(dr("OrganName"))
            FI_PositionID.SetInitValue(dr("PositionID"))
            FI_PositionName.SetInitValue(dr("PositionName"))
            FI_WorkTypeName.SetInitValue(dr("WorkTypeName"))
            FI_RankID.SetInitValue(dr("RankID"))
            FI_TitleName.SetInitValue(dr("TitleName"))
            FI_EmpDate.SetInitValue(dr("EmpDate"))
            FI_ExtendFlag.SetInitValue(dr("ExtendFlag"))
            FI_Learn1.SetInitValue(dr("Learn1"))
            FI_Learn2.SetInitValue(dr("Learn2"))
            FI_Learn3.SetInitValue(dr("Learn3"))
            FI_Attitude1.SetInitValue(dr("Attitude1"))
            FI_Attitude2.SetInitValue(dr("Attitude2"))
            FI_Relation1.SetInitValue(dr("Relation1"))
            FI_Relation2.SetInitValue(dr("Relation2"))
            FI_Ability1.SetInitValue(dr("Ability1"))
            FI_Ability2.SetInitValue(dr("Ability2"))
            FI_Ability3.SetInitValue(dr("Ability3"))
            FI_Ability4.SetInitValue(dr("Ability4"))
            FI_Leader1.SetInitValue(dr("Leader1"))
            FI_Leader2.SetInitValue(dr("Leader2"))
            FI_Leader3.SetInitValue(dr("Leader3"))
            FI_Leader4.SetInitValue(dr("Leader4"))
            FI_Leader5.SetInitValue(dr("Leader5"))
            FI_Attachment.SetInitValue(dr("Attachment"))
            FI_Result.SetInitValue(dr("Result"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CompID.Updated = False
            FI_ApplyTime.Updated = False
            FI_ApplyID.Updated = False
            FI_ProbSeq.Updated = False
            FI_ProbDate.Updated = False
            FI_ProbDate2.Updated = False
            FI_DeptID.Updated = False
            FI_DeptName.Updated = False
            FI_OrganID.Updated = False
            FI_OrganName.Updated = False
            FI_PositionID.Updated = False
            FI_PositionName.Updated = False
            FI_WorkTypeName.Updated = False
            FI_RankID.Updated = False
            FI_TitleName.Updated = False
            FI_EmpDate.Updated = False
            FI_ExtendFlag.Updated = False
            FI_Learn1.Updated = False
            FI_Learn2.Updated = False
            FI_Learn3.Updated = False
            FI_Attitude1.Updated = False
            FI_Attitude2.Updated = False
            FI_Relation1.Updated = False
            FI_Relation2.Updated = False
            FI_Ability1.Updated = False
            FI_Ability2.Updated = False
            FI_Ability3.Updated = False
            FI_Ability4.Updated = False
            FI_Leader1.Updated = False
            FI_Leader2.Updated = False
            FI_Leader3.Updated = False
            FI_Leader4.Updated = False
            FI_Leader5.Updated = False
            FI_Attachment.Updated = False
            FI_Result.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
        End Sub

        Public ReadOnly Property CompID As Field(Of String) 
            Get
                Return FI_CompID
            End Get
        End Property

        Public ReadOnly Property ApplyTime As Field(Of Date) 
            Get
                Return FI_ApplyTime
            End Get
        End Property

        Public ReadOnly Property ApplyID As Field(Of String) 
            Get
                Return FI_ApplyID
            End Get
        End Property

        Public ReadOnly Property ProbSeq As Field(Of Integer) 
            Get
                Return FI_ProbSeq
            End Get
        End Property

        Public ReadOnly Property ProbDate As Field(Of Date) 
            Get
                Return FI_ProbDate
            End Get
        End Property

        Public ReadOnly Property ProbDate2 As Field(Of Date) 
            Get
                Return FI_ProbDate2
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

        Public ReadOnly Property WorkTypeName As Field(Of String) 
            Get
                Return FI_WorkTypeName
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

        Public ReadOnly Property EmpDate As Field(Of Date) 
            Get
                Return FI_EmpDate
            End Get
        End Property

        Public ReadOnly Property ExtendFlag As Field(Of String) 
            Get
                Return FI_ExtendFlag
            End Get
        End Property

        Public ReadOnly Property Learn1 As Field(Of String) 
            Get
                Return FI_Learn1
            End Get
        End Property

        Public ReadOnly Property Learn2 As Field(Of String) 
            Get
                Return FI_Learn2
            End Get
        End Property

        Public ReadOnly Property Learn3 As Field(Of String) 
            Get
                Return FI_Learn3
            End Get
        End Property

        Public ReadOnly Property Attitude1 As Field(Of String) 
            Get
                Return FI_Attitude1
            End Get
        End Property

        Public ReadOnly Property Attitude2 As Field(Of String) 
            Get
                Return FI_Attitude2
            End Get
        End Property

        Public ReadOnly Property Relation1 As Field(Of String) 
            Get
                Return FI_Relation1
            End Get
        End Property

        Public ReadOnly Property Relation2 As Field(Of String) 
            Get
                Return FI_Relation2
            End Get
        End Property

        Public ReadOnly Property Ability1 As Field(Of String) 
            Get
                Return FI_Ability1
            End Get
        End Property

        Public ReadOnly Property Ability2 As Field(Of String) 
            Get
                Return FI_Ability2
            End Get
        End Property

        Public ReadOnly Property Ability3 As Field(Of String) 
            Get
                Return FI_Ability3
            End Get
        End Property

        Public ReadOnly Property Ability4 As Field(Of String) 
            Get
                Return FI_Ability4
            End Get
        End Property

        Public ReadOnly Property Leader1 As Field(Of String) 
            Get
                Return FI_Leader1
            End Get
        End Property

        Public ReadOnly Property Leader2 As Field(Of String) 
            Get
                Return FI_Leader2
            End Get
        End Property

        Public ReadOnly Property Leader3 As Field(Of String) 
            Get
                Return FI_Leader3
            End Get
        End Property

        Public ReadOnly Property Leader4 As Field(Of String) 
            Get
                Return FI_Leader4
            End Get
        End Property

        Public ReadOnly Property Leader5 As Field(Of String) 
            Get
                Return FI_Leader5
            End Get
        End Property

        Public ReadOnly Property Attachment As Field(Of String) 
            Get
                Return FI_Attachment
            End Get
        End Property

        Public ReadOnly Property Result As Field(Of String) 
            Get
                Return FI_Result
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
        Public Function DeleteRowByPrimaryKey(ByVal ProbationRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From Probation")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And ApplyTime = @ApplyTime")
            strSQL.AppendLine("And ApplyID = @ApplyID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, ProbationRow.CompID.Value)
            db.AddInParameter(dbcmd, "@ApplyTime", DbType.Date, ProbationRow.ApplyTime.Value)
            db.AddInParameter(dbcmd, "@ApplyID", DbType.String, ProbationRow.ApplyID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal ProbationRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From Probation")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And ApplyTime = @ApplyTime")
            strSQL.AppendLine("And ApplyID = @ApplyID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, ProbationRow.CompID.Value)
            db.AddInParameter(dbcmd, "@ApplyTime", DbType.Date, ProbationRow.ApplyTime.Value)
            db.AddInParameter(dbcmd, "@ApplyID", DbType.String, ProbationRow.ApplyID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal ProbationRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From Probation")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And ApplyTime = @ApplyTime")
            strSQL.AppendLine("And ApplyID = @ApplyID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In ProbationRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@ApplyTime", DbType.Date, r.ApplyTime.Value)
                        db.AddInParameter(dbcmd, "@ApplyID", DbType.String, r.ApplyID.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal ProbationRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From Probation")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And ApplyTime = @ApplyTime")
            strSQL.AppendLine("And ApplyID = @ApplyID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In ProbationRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@ApplyTime", DbType.Date, r.ApplyTime.Value)
                db.AddInParameter(dbcmd, "@ApplyID", DbType.String, r.ApplyID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal ProbationRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Probation")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And ApplyTime = @ApplyTime")
            strSQL.AppendLine("And ApplyID = @ApplyID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, ProbationRow.CompID.Value)
            db.AddInParameter(dbcmd, "@ApplyTime", DbType.Date, ProbationRow.ApplyTime.Value)
            db.AddInParameter(dbcmd, "@ApplyID", DbType.String, ProbationRow.ApplyID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(ProbationRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Probation")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And ApplyTime = @ApplyTime")
            strSQL.AppendLine("And ApplyID = @ApplyID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, ProbationRow.CompID.Value)
            db.AddInParameter(dbcmd, "@ApplyTime", DbType.Date, ProbationRow.ApplyTime.Value)
            db.AddInParameter(dbcmd, "@ApplyID", DbType.String, ProbationRow.ApplyID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal ProbationRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update Probation Set")
            For i As Integer = 0 To ProbationRow.FieldNames.Length - 1
                If Not ProbationRow.IsIdentityField(ProbationRow.FieldNames(i)) AndAlso ProbationRow.IsUpdated(ProbationRow.FieldNames(i)) AndAlso ProbationRow.CreateUpdateSQL(ProbationRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, ProbationRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And ApplyTime = @PKApplyTime")
            strSQL.AppendLine("And ApplyID = @PKApplyID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If ProbationRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, ProbationRow.CompID.Value)
            If ProbationRow.ApplyTime.Updated Then db.AddInParameter(dbcmd, "@ApplyTime", DbType.Date, IIf(IsDateTimeNull(ProbationRow.ApplyTime.Value), Convert.ToDateTime("1900/1/1"), ProbationRow.ApplyTime.Value))
            If ProbationRow.ApplyID.Updated Then db.AddInParameter(dbcmd, "@ApplyID", DbType.String, ProbationRow.ApplyID.Value)
            If ProbationRow.ProbSeq.Updated Then db.AddInParameter(dbcmd, "@ProbSeq", DbType.Int32, ProbationRow.ProbSeq.Value)
            If ProbationRow.ProbDate.Updated Then db.AddInParameter(dbcmd, "@ProbDate", DbType.Date, IIf(IsDateTimeNull(ProbationRow.ProbDate.Value), Convert.ToDateTime("1900/1/1"), ProbationRow.ProbDate.Value))
            If ProbationRow.ProbDate2.Updated Then db.AddInParameter(dbcmd, "@ProbDate2", DbType.Date, IIf(IsDateTimeNull(ProbationRow.ProbDate2.Value), Convert.ToDateTime("1900/1/1"), ProbationRow.ProbDate2.Value))
            If ProbationRow.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, ProbationRow.DeptID.Value)
            If ProbationRow.DeptName.Updated Then db.AddInParameter(dbcmd, "@DeptName", DbType.String, ProbationRow.DeptName.Value)
            If ProbationRow.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, ProbationRow.OrganID.Value)
            If ProbationRow.OrganName.Updated Then db.AddInParameter(dbcmd, "@OrganName", DbType.String, ProbationRow.OrganName.Value)
            If ProbationRow.PositionID.Updated Then db.AddInParameter(dbcmd, "@PositionID", DbType.String, ProbationRow.PositionID.Value)
            If ProbationRow.PositionName.Updated Then db.AddInParameter(dbcmd, "@PositionName", DbType.String, ProbationRow.PositionName.Value)
            If ProbationRow.WorkTypeName.Updated Then db.AddInParameter(dbcmd, "@WorkTypeName", DbType.String, ProbationRow.WorkTypeName.Value)
            If ProbationRow.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, ProbationRow.RankID.Value)
            If ProbationRow.TitleName.Updated Then db.AddInParameter(dbcmd, "@TitleName", DbType.String, ProbationRow.TitleName.Value)
            If ProbationRow.EmpDate.Updated Then db.AddInParameter(dbcmd, "@EmpDate", DbType.Date, IIf(IsDateTimeNull(ProbationRow.EmpDate.Value), Convert.ToDateTime("1900/1/1"), ProbationRow.EmpDate.Value))
            If ProbationRow.ExtendFlag.Updated Then db.AddInParameter(dbcmd, "@ExtendFlag", DbType.String, ProbationRow.ExtendFlag.Value)
            If ProbationRow.Learn1.Updated Then db.AddInParameter(dbcmd, "@Learn1", DbType.String, ProbationRow.Learn1.Value)
            If ProbationRow.Learn2.Updated Then db.AddInParameter(dbcmd, "@Learn2", DbType.String, ProbationRow.Learn2.Value)
            If ProbationRow.Learn3.Updated Then db.AddInParameter(dbcmd, "@Learn3", DbType.String, ProbationRow.Learn3.Value)
            If ProbationRow.Attitude1.Updated Then db.AddInParameter(dbcmd, "@Attitude1", DbType.String, ProbationRow.Attitude1.Value)
            If ProbationRow.Attitude2.Updated Then db.AddInParameter(dbcmd, "@Attitude2", DbType.String, ProbationRow.Attitude2.Value)
            If ProbationRow.Relation1.Updated Then db.AddInParameter(dbcmd, "@Relation1", DbType.String, ProbationRow.Relation1.Value)
            If ProbationRow.Relation2.Updated Then db.AddInParameter(dbcmd, "@Relation2", DbType.String, ProbationRow.Relation2.Value)
            If ProbationRow.Ability1.Updated Then db.AddInParameter(dbcmd, "@Ability1", DbType.String, ProbationRow.Ability1.Value)
            If ProbationRow.Ability2.Updated Then db.AddInParameter(dbcmd, "@Ability2", DbType.String, ProbationRow.Ability2.Value)
            If ProbationRow.Ability3.Updated Then db.AddInParameter(dbcmd, "@Ability3", DbType.String, ProbationRow.Ability3.Value)
            If ProbationRow.Ability4.Updated Then db.AddInParameter(dbcmd, "@Ability4", DbType.String, ProbationRow.Ability4.Value)
            If ProbationRow.Leader1.Updated Then db.AddInParameter(dbcmd, "@Leader1", DbType.String, ProbationRow.Leader1.Value)
            If ProbationRow.Leader2.Updated Then db.AddInParameter(dbcmd, "@Leader2", DbType.String, ProbationRow.Leader2.Value)
            If ProbationRow.Leader3.Updated Then db.AddInParameter(dbcmd, "@Leader3", DbType.String, ProbationRow.Leader3.Value)
            If ProbationRow.Leader4.Updated Then db.AddInParameter(dbcmd, "@Leader4", DbType.String, ProbationRow.Leader4.Value)
            If ProbationRow.Leader5.Updated Then db.AddInParameter(dbcmd, "@Leader5", DbType.String, ProbationRow.Leader5.Value)
            If ProbationRow.Attachment.Updated Then db.AddInParameter(dbcmd, "@Attachment", DbType.String, ProbationRow.Attachment.Value)
            If ProbationRow.Result.Updated Then db.AddInParameter(dbcmd, "@Result", DbType.String, ProbationRow.Result.Value)
            If ProbationRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, ProbationRow.LastChgComp.Value)
            If ProbationRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, ProbationRow.LastChgID.Value)
            If ProbationRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(ProbationRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), ProbationRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(ProbationRow.LoadFromDataRow, ProbationRow.CompID.OldValue, ProbationRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKApplyTime", DbType.Date, IIf(ProbationRow.LoadFromDataRow, ProbationRow.ApplyTime.OldValue, ProbationRow.ApplyTime.Value))
            db.AddInParameter(dbcmd, "@PKApplyID", DbType.String, IIf(ProbationRow.LoadFromDataRow, ProbationRow.ApplyID.OldValue, ProbationRow.ApplyID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal ProbationRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update Probation Set")
            For i As Integer = 0 To ProbationRow.FieldNames.Length - 1
                If Not ProbationRow.IsIdentityField(ProbationRow.FieldNames(i)) AndAlso ProbationRow.IsUpdated(ProbationRow.FieldNames(i)) AndAlso ProbationRow.CreateUpdateSQL(ProbationRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, ProbationRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And ApplyTime = @PKApplyTime")
            strSQL.AppendLine("And ApplyID = @PKApplyID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If ProbationRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, ProbationRow.CompID.Value)
            If ProbationRow.ApplyTime.Updated Then db.AddInParameter(dbcmd, "@ApplyTime", DbType.Date, IIf(IsDateTimeNull(ProbationRow.ApplyTime.Value), Convert.ToDateTime("1900/1/1"), ProbationRow.ApplyTime.Value))
            If ProbationRow.ApplyID.Updated Then db.AddInParameter(dbcmd, "@ApplyID", DbType.String, ProbationRow.ApplyID.Value)
            If ProbationRow.ProbSeq.Updated Then db.AddInParameter(dbcmd, "@ProbSeq", DbType.Int32, ProbationRow.ProbSeq.Value)
            If ProbationRow.ProbDate.Updated Then db.AddInParameter(dbcmd, "@ProbDate", DbType.Date, IIf(IsDateTimeNull(ProbationRow.ProbDate.Value), Convert.ToDateTime("1900/1/1"), ProbationRow.ProbDate.Value))
            If ProbationRow.ProbDate2.Updated Then db.AddInParameter(dbcmd, "@ProbDate2", DbType.Date, IIf(IsDateTimeNull(ProbationRow.ProbDate2.Value), Convert.ToDateTime("1900/1/1"), ProbationRow.ProbDate2.Value))
            If ProbationRow.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, ProbationRow.DeptID.Value)
            If ProbationRow.DeptName.Updated Then db.AddInParameter(dbcmd, "@DeptName", DbType.String, ProbationRow.DeptName.Value)
            If ProbationRow.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, ProbationRow.OrganID.Value)
            If ProbationRow.OrganName.Updated Then db.AddInParameter(dbcmd, "@OrganName", DbType.String, ProbationRow.OrganName.Value)
            If ProbationRow.PositionID.Updated Then db.AddInParameter(dbcmd, "@PositionID", DbType.String, ProbationRow.PositionID.Value)
            If ProbationRow.PositionName.Updated Then db.AddInParameter(dbcmd, "@PositionName", DbType.String, ProbationRow.PositionName.Value)
            If ProbationRow.WorkTypeName.Updated Then db.AddInParameter(dbcmd, "@WorkTypeName", DbType.String, ProbationRow.WorkTypeName.Value)
            If ProbationRow.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, ProbationRow.RankID.Value)
            If ProbationRow.TitleName.Updated Then db.AddInParameter(dbcmd, "@TitleName", DbType.String, ProbationRow.TitleName.Value)
            If ProbationRow.EmpDate.Updated Then db.AddInParameter(dbcmd, "@EmpDate", DbType.Date, IIf(IsDateTimeNull(ProbationRow.EmpDate.Value), Convert.ToDateTime("1900/1/1"), ProbationRow.EmpDate.Value))
            If ProbationRow.ExtendFlag.Updated Then db.AddInParameter(dbcmd, "@ExtendFlag", DbType.String, ProbationRow.ExtendFlag.Value)
            If ProbationRow.Learn1.Updated Then db.AddInParameter(dbcmd, "@Learn1", DbType.String, ProbationRow.Learn1.Value)
            If ProbationRow.Learn2.Updated Then db.AddInParameter(dbcmd, "@Learn2", DbType.String, ProbationRow.Learn2.Value)
            If ProbationRow.Learn3.Updated Then db.AddInParameter(dbcmd, "@Learn3", DbType.String, ProbationRow.Learn3.Value)
            If ProbationRow.Attitude1.Updated Then db.AddInParameter(dbcmd, "@Attitude1", DbType.String, ProbationRow.Attitude1.Value)
            If ProbationRow.Attitude2.Updated Then db.AddInParameter(dbcmd, "@Attitude2", DbType.String, ProbationRow.Attitude2.Value)
            If ProbationRow.Relation1.Updated Then db.AddInParameter(dbcmd, "@Relation1", DbType.String, ProbationRow.Relation1.Value)
            If ProbationRow.Relation2.Updated Then db.AddInParameter(dbcmd, "@Relation2", DbType.String, ProbationRow.Relation2.Value)
            If ProbationRow.Ability1.Updated Then db.AddInParameter(dbcmd, "@Ability1", DbType.String, ProbationRow.Ability1.Value)
            If ProbationRow.Ability2.Updated Then db.AddInParameter(dbcmd, "@Ability2", DbType.String, ProbationRow.Ability2.Value)
            If ProbationRow.Ability3.Updated Then db.AddInParameter(dbcmd, "@Ability3", DbType.String, ProbationRow.Ability3.Value)
            If ProbationRow.Ability4.Updated Then db.AddInParameter(dbcmd, "@Ability4", DbType.String, ProbationRow.Ability4.Value)
            If ProbationRow.Leader1.Updated Then db.AddInParameter(dbcmd, "@Leader1", DbType.String, ProbationRow.Leader1.Value)
            If ProbationRow.Leader2.Updated Then db.AddInParameter(dbcmd, "@Leader2", DbType.String, ProbationRow.Leader2.Value)
            If ProbationRow.Leader3.Updated Then db.AddInParameter(dbcmd, "@Leader3", DbType.String, ProbationRow.Leader3.Value)
            If ProbationRow.Leader4.Updated Then db.AddInParameter(dbcmd, "@Leader4", DbType.String, ProbationRow.Leader4.Value)
            If ProbationRow.Leader5.Updated Then db.AddInParameter(dbcmd, "@Leader5", DbType.String, ProbationRow.Leader5.Value)
            If ProbationRow.Attachment.Updated Then db.AddInParameter(dbcmd, "@Attachment", DbType.String, ProbationRow.Attachment.Value)
            If ProbationRow.Result.Updated Then db.AddInParameter(dbcmd, "@Result", DbType.String, ProbationRow.Result.Value)
            If ProbationRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, ProbationRow.LastChgComp.Value)
            If ProbationRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, ProbationRow.LastChgID.Value)
            If ProbationRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(ProbationRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), ProbationRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(ProbationRow.LoadFromDataRow, ProbationRow.CompID.OldValue, ProbationRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKApplyTime", DbType.Date, IIf(ProbationRow.LoadFromDataRow, ProbationRow.ApplyTime.OldValue, ProbationRow.ApplyTime.Value))
            db.AddInParameter(dbcmd, "@PKApplyID", DbType.String, IIf(ProbationRow.LoadFromDataRow, ProbationRow.ApplyID.OldValue, ProbationRow.ApplyID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal ProbationRow As Row()) As Integer
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
                    For Each r As Row In ProbationRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update Probation Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where CompID = @PKCompID")
                        strSQL.AppendLine("And ApplyTime = @PKApplyTime")
                        strSQL.AppendLine("And ApplyID = @PKApplyID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.ApplyTime.Updated Then db.AddInParameter(dbcmd, "@ApplyTime", DbType.Date, IIf(IsDateTimeNull(r.ApplyTime.Value), Convert.ToDateTime("1900/1/1"), r.ApplyTime.Value))
                        If r.ApplyID.Updated Then db.AddInParameter(dbcmd, "@ApplyID", DbType.String, r.ApplyID.Value)
                        If r.ProbSeq.Updated Then db.AddInParameter(dbcmd, "@ProbSeq", DbType.Int32, r.ProbSeq.Value)
                        If r.ProbDate.Updated Then db.AddInParameter(dbcmd, "@ProbDate", DbType.Date, IIf(IsDateTimeNull(r.ProbDate.Value), Convert.ToDateTime("1900/1/1"), r.ProbDate.Value))
                        If r.ProbDate2.Updated Then db.AddInParameter(dbcmd, "@ProbDate2", DbType.Date, IIf(IsDateTimeNull(r.ProbDate2.Value), Convert.ToDateTime("1900/1/1"), r.ProbDate2.Value))
                        If r.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                        If r.DeptName.Updated Then db.AddInParameter(dbcmd, "@DeptName", DbType.String, r.DeptName.Value)
                        If r.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                        If r.OrganName.Updated Then db.AddInParameter(dbcmd, "@OrganName", DbType.String, r.OrganName.Value)
                        If r.PositionID.Updated Then db.AddInParameter(dbcmd, "@PositionID", DbType.String, r.PositionID.Value)
                        If r.PositionName.Updated Then db.AddInParameter(dbcmd, "@PositionName", DbType.String, r.PositionName.Value)
                        If r.WorkTypeName.Updated Then db.AddInParameter(dbcmd, "@WorkTypeName", DbType.String, r.WorkTypeName.Value)
                        If r.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                        If r.TitleName.Updated Then db.AddInParameter(dbcmd, "@TitleName", DbType.String, r.TitleName.Value)
                        If r.EmpDate.Updated Then db.AddInParameter(dbcmd, "@EmpDate", DbType.Date, IIf(IsDateTimeNull(r.EmpDate.Value), Convert.ToDateTime("1900/1/1"), r.EmpDate.Value))
                        If r.ExtendFlag.Updated Then db.AddInParameter(dbcmd, "@ExtendFlag", DbType.String, r.ExtendFlag.Value)
                        If r.Learn1.Updated Then db.AddInParameter(dbcmd, "@Learn1", DbType.String, r.Learn1.Value)
                        If r.Learn2.Updated Then db.AddInParameter(dbcmd, "@Learn2", DbType.String, r.Learn2.Value)
                        If r.Learn3.Updated Then db.AddInParameter(dbcmd, "@Learn3", DbType.String, r.Learn3.Value)
                        If r.Attitude1.Updated Then db.AddInParameter(dbcmd, "@Attitude1", DbType.String, r.Attitude1.Value)
                        If r.Attitude2.Updated Then db.AddInParameter(dbcmd, "@Attitude2", DbType.String, r.Attitude2.Value)
                        If r.Relation1.Updated Then db.AddInParameter(dbcmd, "@Relation1", DbType.String, r.Relation1.Value)
                        If r.Relation2.Updated Then db.AddInParameter(dbcmd, "@Relation2", DbType.String, r.Relation2.Value)
                        If r.Ability1.Updated Then db.AddInParameter(dbcmd, "@Ability1", DbType.String, r.Ability1.Value)
                        If r.Ability2.Updated Then db.AddInParameter(dbcmd, "@Ability2", DbType.String, r.Ability2.Value)
                        If r.Ability3.Updated Then db.AddInParameter(dbcmd, "@Ability3", DbType.String, r.Ability3.Value)
                        If r.Ability4.Updated Then db.AddInParameter(dbcmd, "@Ability4", DbType.String, r.Ability4.Value)
                        If r.Leader1.Updated Then db.AddInParameter(dbcmd, "@Leader1", DbType.String, r.Leader1.Value)
                        If r.Leader2.Updated Then db.AddInParameter(dbcmd, "@Leader2", DbType.String, r.Leader2.Value)
                        If r.Leader3.Updated Then db.AddInParameter(dbcmd, "@Leader3", DbType.String, r.Leader3.Value)
                        If r.Leader4.Updated Then db.AddInParameter(dbcmd, "@Leader4", DbType.String, r.Leader4.Value)
                        If r.Leader5.Updated Then db.AddInParameter(dbcmd, "@Leader5", DbType.String, r.Leader5.Value)
                        If r.Attachment.Updated Then db.AddInParameter(dbcmd, "@Attachment", DbType.String, r.Attachment.Value)
                        If r.Result.Updated Then db.AddInParameter(dbcmd, "@Result", DbType.String, r.Result.Value)
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                        db.AddInParameter(dbcmd, "@PKApplyTime", DbType.Date, IIf(r.LoadFromDataRow, r.ApplyTime.OldValue, r.ApplyTime.Value))
                        db.AddInParameter(dbcmd, "@PKApplyID", DbType.String, IIf(r.LoadFromDataRow, r.ApplyID.OldValue, r.ApplyID.Value))

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

        Public Function Update(ByVal ProbationRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In ProbationRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update Probation Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where CompID = @PKCompID")
                strSQL.AppendLine("And ApplyTime = @PKApplyTime")
                strSQL.AppendLine("And ApplyID = @PKApplyID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.ApplyTime.Updated Then db.AddInParameter(dbcmd, "@ApplyTime", DbType.Date, IIf(IsDateTimeNull(r.ApplyTime.Value), Convert.ToDateTime("1900/1/1"), r.ApplyTime.Value))
                If r.ApplyID.Updated Then db.AddInParameter(dbcmd, "@ApplyID", DbType.String, r.ApplyID.Value)
                If r.ProbSeq.Updated Then db.AddInParameter(dbcmd, "@ProbSeq", DbType.Int32, r.ProbSeq.Value)
                If r.ProbDate.Updated Then db.AddInParameter(dbcmd, "@ProbDate", DbType.Date, IIf(IsDateTimeNull(r.ProbDate.Value), Convert.ToDateTime("1900/1/1"), r.ProbDate.Value))
                If r.ProbDate2.Updated Then db.AddInParameter(dbcmd, "@ProbDate2", DbType.Date, IIf(IsDateTimeNull(r.ProbDate2.Value), Convert.ToDateTime("1900/1/1"), r.ProbDate2.Value))
                If r.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                If r.DeptName.Updated Then db.AddInParameter(dbcmd, "@DeptName", DbType.String, r.DeptName.Value)
                If r.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                If r.OrganName.Updated Then db.AddInParameter(dbcmd, "@OrganName", DbType.String, r.OrganName.Value)
                If r.PositionID.Updated Then db.AddInParameter(dbcmd, "@PositionID", DbType.String, r.PositionID.Value)
                If r.PositionName.Updated Then db.AddInParameter(dbcmd, "@PositionName", DbType.String, r.PositionName.Value)
                If r.WorkTypeName.Updated Then db.AddInParameter(dbcmd, "@WorkTypeName", DbType.String, r.WorkTypeName.Value)
                If r.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                If r.TitleName.Updated Then db.AddInParameter(dbcmd, "@TitleName", DbType.String, r.TitleName.Value)
                If r.EmpDate.Updated Then db.AddInParameter(dbcmd, "@EmpDate", DbType.Date, IIf(IsDateTimeNull(r.EmpDate.Value), Convert.ToDateTime("1900/1/1"), r.EmpDate.Value))
                If r.ExtendFlag.Updated Then db.AddInParameter(dbcmd, "@ExtendFlag", DbType.String, r.ExtendFlag.Value)
                If r.Learn1.Updated Then db.AddInParameter(dbcmd, "@Learn1", DbType.String, r.Learn1.Value)
                If r.Learn2.Updated Then db.AddInParameter(dbcmd, "@Learn2", DbType.String, r.Learn2.Value)
                If r.Learn3.Updated Then db.AddInParameter(dbcmd, "@Learn3", DbType.String, r.Learn3.Value)
                If r.Attitude1.Updated Then db.AddInParameter(dbcmd, "@Attitude1", DbType.String, r.Attitude1.Value)
                If r.Attitude2.Updated Then db.AddInParameter(dbcmd, "@Attitude2", DbType.String, r.Attitude2.Value)
                If r.Relation1.Updated Then db.AddInParameter(dbcmd, "@Relation1", DbType.String, r.Relation1.Value)
                If r.Relation2.Updated Then db.AddInParameter(dbcmd, "@Relation2", DbType.String, r.Relation2.Value)
                If r.Ability1.Updated Then db.AddInParameter(dbcmd, "@Ability1", DbType.String, r.Ability1.Value)
                If r.Ability2.Updated Then db.AddInParameter(dbcmd, "@Ability2", DbType.String, r.Ability2.Value)
                If r.Ability3.Updated Then db.AddInParameter(dbcmd, "@Ability3", DbType.String, r.Ability3.Value)
                If r.Ability4.Updated Then db.AddInParameter(dbcmd, "@Ability4", DbType.String, r.Ability4.Value)
                If r.Leader1.Updated Then db.AddInParameter(dbcmd, "@Leader1", DbType.String, r.Leader1.Value)
                If r.Leader2.Updated Then db.AddInParameter(dbcmd, "@Leader2", DbType.String, r.Leader2.Value)
                If r.Leader3.Updated Then db.AddInParameter(dbcmd, "@Leader3", DbType.String, r.Leader3.Value)
                If r.Leader4.Updated Then db.AddInParameter(dbcmd, "@Leader4", DbType.String, r.Leader4.Value)
                If r.Leader5.Updated Then db.AddInParameter(dbcmd, "@Leader5", DbType.String, r.Leader5.Value)
                If r.Attachment.Updated Then db.AddInParameter(dbcmd, "@Attachment", DbType.String, r.Attachment.Value)
                If r.Result.Updated Then db.AddInParameter(dbcmd, "@Result", DbType.String, r.Result.Value)
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                db.AddInParameter(dbcmd, "@PKApplyTime", DbType.Date, IIf(r.LoadFromDataRow, r.ApplyTime.OldValue, r.ApplyTime.Value))
                db.AddInParameter(dbcmd, "@PKApplyID", DbType.String, IIf(r.LoadFromDataRow, r.ApplyID.OldValue, r.ApplyID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal ProbationRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From Probation")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And ApplyTime = @ApplyTime")
            strSQL.AppendLine("And ApplyID = @ApplyID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, ProbationRow.CompID.Value)
            db.AddInParameter(dbcmd, "@ApplyTime", DbType.Date, ProbationRow.ApplyTime.Value)
            db.AddInParameter(dbcmd, "@ApplyID", DbType.String, ProbationRow.ApplyID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal ProbationRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From Probation")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And ApplyTime = @ApplyTime")
            strSQL.AppendLine("And ApplyID = @ApplyID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, ProbationRow.CompID.Value)
            db.AddInParameter(dbcmd, "@ApplyTime", DbType.Date, ProbationRow.ApplyTime.Value)
            db.AddInParameter(dbcmd, "@ApplyID", DbType.String, ProbationRow.ApplyID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Probation")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal ProbationRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into Probation")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, ApplyTime, ApplyID, ProbSeq, ProbDate, ProbDate2, DeptID, DeptName, OrganID,")
            strSQL.AppendLine("    OrganName, PositionID, PositionName, WorkTypeName, RankID, TitleName, EmpDate, ExtendFlag,")
            strSQL.AppendLine("    Learn1, Learn2, Learn3, Attitude1, Attitude2, Relation1, Relation2, Ability1, Ability2,")
            strSQL.AppendLine("    Ability3, Ability4, Leader1, Leader2, Leader3, Leader4, Leader5, Attachment, Result,")
            strSQL.AppendLine("    LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @ApplyTime, @ApplyID, @ProbSeq, @ProbDate, @ProbDate2, @DeptID, @DeptName, @OrganID,")
            strSQL.AppendLine("    @OrganName, @PositionID, @PositionName, @WorkTypeName, @RankID, @TitleName, @EmpDate, @ExtendFlag,")
            strSQL.AppendLine("    @Learn1, @Learn2, @Learn3, @Attitude1, @Attitude2, @Relation1, @Relation2, @Ability1, @Ability2,")
            strSQL.AppendLine("    @Ability3, @Ability4, @Leader1, @Leader2, @Leader3, @Leader4, @Leader5, @Attachment, @Result,")
            strSQL.AppendLine("    @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, ProbationRow.CompID.Value)
            db.AddInParameter(dbcmd, "@ApplyTime", DbType.Date, IIf(IsDateTimeNull(ProbationRow.ApplyTime.Value), Convert.ToDateTime("1900/1/1"), ProbationRow.ApplyTime.Value))
            db.AddInParameter(dbcmd, "@ApplyID", DbType.String, ProbationRow.ApplyID.Value)
            db.AddInParameter(dbcmd, "@ProbSeq", DbType.Int32, ProbationRow.ProbSeq.Value)
            db.AddInParameter(dbcmd, "@ProbDate", DbType.Date, IIf(IsDateTimeNull(ProbationRow.ProbDate.Value), Convert.ToDateTime("1900/1/1"), ProbationRow.ProbDate.Value))
            db.AddInParameter(dbcmd, "@ProbDate2", DbType.Date, IIf(IsDateTimeNull(ProbationRow.ProbDate2.Value), Convert.ToDateTime("1900/1/1"), ProbationRow.ProbDate2.Value))
            db.AddInParameter(dbcmd, "@DeptID", DbType.String, ProbationRow.DeptID.Value)
            db.AddInParameter(dbcmd, "@DeptName", DbType.String, ProbationRow.DeptName.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, ProbationRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@OrganName", DbType.String, ProbationRow.OrganName.Value)
            db.AddInParameter(dbcmd, "@PositionID", DbType.String, ProbationRow.PositionID.Value)
            db.AddInParameter(dbcmd, "@PositionName", DbType.String, ProbationRow.PositionName.Value)
            db.AddInParameter(dbcmd, "@WorkTypeName", DbType.String, ProbationRow.WorkTypeName.Value)
            db.AddInParameter(dbcmd, "@RankID", DbType.String, ProbationRow.RankID.Value)
            db.AddInParameter(dbcmd, "@TitleName", DbType.String, ProbationRow.TitleName.Value)
            db.AddInParameter(dbcmd, "@EmpDate", DbType.Date, IIf(IsDateTimeNull(ProbationRow.EmpDate.Value), Convert.ToDateTime("1900/1/1"), ProbationRow.EmpDate.Value))
            db.AddInParameter(dbcmd, "@ExtendFlag", DbType.String, ProbationRow.ExtendFlag.Value)
            db.AddInParameter(dbcmd, "@Learn1", DbType.String, ProbationRow.Learn1.Value)
            db.AddInParameter(dbcmd, "@Learn2", DbType.String, ProbationRow.Learn2.Value)
            db.AddInParameter(dbcmd, "@Learn3", DbType.String, ProbationRow.Learn3.Value)
            db.AddInParameter(dbcmd, "@Attitude1", DbType.String, ProbationRow.Attitude1.Value)
            db.AddInParameter(dbcmd, "@Attitude2", DbType.String, ProbationRow.Attitude2.Value)
            db.AddInParameter(dbcmd, "@Relation1", DbType.String, ProbationRow.Relation1.Value)
            db.AddInParameter(dbcmd, "@Relation2", DbType.String, ProbationRow.Relation2.Value)
            db.AddInParameter(dbcmd, "@Ability1", DbType.String, ProbationRow.Ability1.Value)
            db.AddInParameter(dbcmd, "@Ability2", DbType.String, ProbationRow.Ability2.Value)
            db.AddInParameter(dbcmd, "@Ability3", DbType.String, ProbationRow.Ability3.Value)
            db.AddInParameter(dbcmd, "@Ability4", DbType.String, ProbationRow.Ability4.Value)
            db.AddInParameter(dbcmd, "@Leader1", DbType.String, ProbationRow.Leader1.Value)
            db.AddInParameter(dbcmd, "@Leader2", DbType.String, ProbationRow.Leader2.Value)
            db.AddInParameter(dbcmd, "@Leader3", DbType.String, ProbationRow.Leader3.Value)
            db.AddInParameter(dbcmd, "@Leader4", DbType.String, ProbationRow.Leader4.Value)
            db.AddInParameter(dbcmd, "@Leader5", DbType.String, ProbationRow.Leader5.Value)
            db.AddInParameter(dbcmd, "@Attachment", DbType.String, ProbationRow.Attachment.Value)
            db.AddInParameter(dbcmd, "@Result", DbType.String, ProbationRow.Result.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, ProbationRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, ProbationRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(ProbationRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), ProbationRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal ProbationRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into Probation")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, ApplyTime, ApplyID, ProbSeq, ProbDate, ProbDate2, DeptID, DeptName, OrganID,")
            strSQL.AppendLine("    OrganName, PositionID, PositionName, WorkTypeName, RankID, TitleName, EmpDate, ExtendFlag,")
            strSQL.AppendLine("    Learn1, Learn2, Learn3, Attitude1, Attitude2, Relation1, Relation2, Ability1, Ability2,")
            strSQL.AppendLine("    Ability3, Ability4, Leader1, Leader2, Leader3, Leader4, Leader5, Attachment, Result,")
            strSQL.AppendLine("    LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @ApplyTime, @ApplyID, @ProbSeq, @ProbDate, @ProbDate2, @DeptID, @DeptName, @OrganID,")
            strSQL.AppendLine("    @OrganName, @PositionID, @PositionName, @WorkTypeName, @RankID, @TitleName, @EmpDate, @ExtendFlag,")
            strSQL.AppendLine("    @Learn1, @Learn2, @Learn3, @Attitude1, @Attitude2, @Relation1, @Relation2, @Ability1, @Ability2,")
            strSQL.AppendLine("    @Ability3, @Ability4, @Leader1, @Leader2, @Leader3, @Leader4, @Leader5, @Attachment, @Result,")
            strSQL.AppendLine("    @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, ProbationRow.CompID.Value)
            db.AddInParameter(dbcmd, "@ApplyTime", DbType.Date, IIf(IsDateTimeNull(ProbationRow.ApplyTime.Value), Convert.ToDateTime("1900/1/1"), ProbationRow.ApplyTime.Value))
            db.AddInParameter(dbcmd, "@ApplyID", DbType.String, ProbationRow.ApplyID.Value)
            db.AddInParameter(dbcmd, "@ProbSeq", DbType.Int32, ProbationRow.ProbSeq.Value)
            db.AddInParameter(dbcmd, "@ProbDate", DbType.Date, IIf(IsDateTimeNull(ProbationRow.ProbDate.Value), Convert.ToDateTime("1900/1/1"), ProbationRow.ProbDate.Value))
            db.AddInParameter(dbcmd, "@ProbDate2", DbType.Date, IIf(IsDateTimeNull(ProbationRow.ProbDate2.Value), Convert.ToDateTime("1900/1/1"), ProbationRow.ProbDate2.Value))
            db.AddInParameter(dbcmd, "@DeptID", DbType.String, ProbationRow.DeptID.Value)
            db.AddInParameter(dbcmd, "@DeptName", DbType.String, ProbationRow.DeptName.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, ProbationRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@OrganName", DbType.String, ProbationRow.OrganName.Value)
            db.AddInParameter(dbcmd, "@PositionID", DbType.String, ProbationRow.PositionID.Value)
            db.AddInParameter(dbcmd, "@PositionName", DbType.String, ProbationRow.PositionName.Value)
            db.AddInParameter(dbcmd, "@WorkTypeName", DbType.String, ProbationRow.WorkTypeName.Value)
            db.AddInParameter(dbcmd, "@RankID", DbType.String, ProbationRow.RankID.Value)
            db.AddInParameter(dbcmd, "@TitleName", DbType.String, ProbationRow.TitleName.Value)
            db.AddInParameter(dbcmd, "@EmpDate", DbType.Date, IIf(IsDateTimeNull(ProbationRow.EmpDate.Value), Convert.ToDateTime("1900/1/1"), ProbationRow.EmpDate.Value))
            db.AddInParameter(dbcmd, "@ExtendFlag", DbType.String, ProbationRow.ExtendFlag.Value)
            db.AddInParameter(dbcmd, "@Learn1", DbType.String, ProbationRow.Learn1.Value)
            db.AddInParameter(dbcmd, "@Learn2", DbType.String, ProbationRow.Learn2.Value)
            db.AddInParameter(dbcmd, "@Learn3", DbType.String, ProbationRow.Learn3.Value)
            db.AddInParameter(dbcmd, "@Attitude1", DbType.String, ProbationRow.Attitude1.Value)
            db.AddInParameter(dbcmd, "@Attitude2", DbType.String, ProbationRow.Attitude2.Value)
            db.AddInParameter(dbcmd, "@Relation1", DbType.String, ProbationRow.Relation1.Value)
            db.AddInParameter(dbcmd, "@Relation2", DbType.String, ProbationRow.Relation2.Value)
            db.AddInParameter(dbcmd, "@Ability1", DbType.String, ProbationRow.Ability1.Value)
            db.AddInParameter(dbcmd, "@Ability2", DbType.String, ProbationRow.Ability2.Value)
            db.AddInParameter(dbcmd, "@Ability3", DbType.String, ProbationRow.Ability3.Value)
            db.AddInParameter(dbcmd, "@Ability4", DbType.String, ProbationRow.Ability4.Value)
            db.AddInParameter(dbcmd, "@Leader1", DbType.String, ProbationRow.Leader1.Value)
            db.AddInParameter(dbcmd, "@Leader2", DbType.String, ProbationRow.Leader2.Value)
            db.AddInParameter(dbcmd, "@Leader3", DbType.String, ProbationRow.Leader3.Value)
            db.AddInParameter(dbcmd, "@Leader4", DbType.String, ProbationRow.Leader4.Value)
            db.AddInParameter(dbcmd, "@Leader5", DbType.String, ProbationRow.Leader5.Value)
            db.AddInParameter(dbcmd, "@Attachment", DbType.String, ProbationRow.Attachment.Value)
            db.AddInParameter(dbcmd, "@Result", DbType.String, ProbationRow.Result.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, ProbationRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, ProbationRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(ProbationRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), ProbationRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal ProbationRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into Probation")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, ApplyTime, ApplyID, ProbSeq, ProbDate, ProbDate2, DeptID, DeptName, OrganID,")
            strSQL.AppendLine("    OrganName, PositionID, PositionName, WorkTypeName, RankID, TitleName, EmpDate, ExtendFlag,")
            strSQL.AppendLine("    Learn1, Learn2, Learn3, Attitude1, Attitude2, Relation1, Relation2, Ability1, Ability2,")
            strSQL.AppendLine("    Ability3, Ability4, Leader1, Leader2, Leader3, Leader4, Leader5, Attachment, Result,")
            strSQL.AppendLine("    LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @ApplyTime, @ApplyID, @ProbSeq, @ProbDate, @ProbDate2, @DeptID, @DeptName, @OrganID,")
            strSQL.AppendLine("    @OrganName, @PositionID, @PositionName, @WorkTypeName, @RankID, @TitleName, @EmpDate, @ExtendFlag,")
            strSQL.AppendLine("    @Learn1, @Learn2, @Learn3, @Attitude1, @Attitude2, @Relation1, @Relation2, @Ability1, @Ability2,")
            strSQL.AppendLine("    @Ability3, @Ability4, @Leader1, @Leader2, @Leader3, @Leader4, @Leader5, @Attachment, @Result,")
            strSQL.AppendLine("    @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In ProbationRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@ApplyTime", DbType.Date, IIf(IsDateTimeNull(r.ApplyTime.Value), Convert.ToDateTime("1900/1/1"), r.ApplyTime.Value))
                        db.AddInParameter(dbcmd, "@ApplyID", DbType.String, r.ApplyID.Value)
                        db.AddInParameter(dbcmd, "@ProbSeq", DbType.Int32, r.ProbSeq.Value)
                        db.AddInParameter(dbcmd, "@ProbDate", DbType.Date, IIf(IsDateTimeNull(r.ProbDate.Value), Convert.ToDateTime("1900/1/1"), r.ProbDate.Value))
                        db.AddInParameter(dbcmd, "@ProbDate2", DbType.Date, IIf(IsDateTimeNull(r.ProbDate2.Value), Convert.ToDateTime("1900/1/1"), r.ProbDate2.Value))
                        db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                        db.AddInParameter(dbcmd, "@DeptName", DbType.String, r.DeptName.Value)
                        db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                        db.AddInParameter(dbcmd, "@OrganName", DbType.String, r.OrganName.Value)
                        db.AddInParameter(dbcmd, "@PositionID", DbType.String, r.PositionID.Value)
                        db.AddInParameter(dbcmd, "@PositionName", DbType.String, r.PositionName.Value)
                        db.AddInParameter(dbcmd, "@WorkTypeName", DbType.String, r.WorkTypeName.Value)
                        db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                        db.AddInParameter(dbcmd, "@TitleName", DbType.String, r.TitleName.Value)
                        db.AddInParameter(dbcmd, "@EmpDate", DbType.Date, IIf(IsDateTimeNull(r.EmpDate.Value), Convert.ToDateTime("1900/1/1"), r.EmpDate.Value))
                        db.AddInParameter(dbcmd, "@ExtendFlag", DbType.String, r.ExtendFlag.Value)
                        db.AddInParameter(dbcmd, "@Learn1", DbType.String, r.Learn1.Value)
                        db.AddInParameter(dbcmd, "@Learn2", DbType.String, r.Learn2.Value)
                        db.AddInParameter(dbcmd, "@Learn3", DbType.String, r.Learn3.Value)
                        db.AddInParameter(dbcmd, "@Attitude1", DbType.String, r.Attitude1.Value)
                        db.AddInParameter(dbcmd, "@Attitude2", DbType.String, r.Attitude2.Value)
                        db.AddInParameter(dbcmd, "@Relation1", DbType.String, r.Relation1.Value)
                        db.AddInParameter(dbcmd, "@Relation2", DbType.String, r.Relation2.Value)
                        db.AddInParameter(dbcmd, "@Ability1", DbType.String, r.Ability1.Value)
                        db.AddInParameter(dbcmd, "@Ability2", DbType.String, r.Ability2.Value)
                        db.AddInParameter(dbcmd, "@Ability3", DbType.String, r.Ability3.Value)
                        db.AddInParameter(dbcmd, "@Ability4", DbType.String, r.Ability4.Value)
                        db.AddInParameter(dbcmd, "@Leader1", DbType.String, r.Leader1.Value)
                        db.AddInParameter(dbcmd, "@Leader2", DbType.String, r.Leader2.Value)
                        db.AddInParameter(dbcmd, "@Leader3", DbType.String, r.Leader3.Value)
                        db.AddInParameter(dbcmd, "@Leader4", DbType.String, r.Leader4.Value)
                        db.AddInParameter(dbcmd, "@Leader5", DbType.String, r.Leader5.Value)
                        db.AddInParameter(dbcmd, "@Attachment", DbType.String, r.Attachment.Value)
                        db.AddInParameter(dbcmd, "@Result", DbType.String, r.Result.Value)
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

        Public Function Insert(ByVal ProbationRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into Probation")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, ApplyTime, ApplyID, ProbSeq, ProbDate, ProbDate2, DeptID, DeptName, OrganID,")
            strSQL.AppendLine("    OrganName, PositionID, PositionName, WorkTypeName, RankID, TitleName, EmpDate, ExtendFlag,")
            strSQL.AppendLine("    Learn1, Learn2, Learn3, Attitude1, Attitude2, Relation1, Relation2, Ability1, Ability2,")
            strSQL.AppendLine("    Ability3, Ability4, Leader1, Leader2, Leader3, Leader4, Leader5, Attachment, Result,")
            strSQL.AppendLine("    LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @ApplyTime, @ApplyID, @ProbSeq, @ProbDate, @ProbDate2, @DeptID, @DeptName, @OrganID,")
            strSQL.AppendLine("    @OrganName, @PositionID, @PositionName, @WorkTypeName, @RankID, @TitleName, @EmpDate, @ExtendFlag,")
            strSQL.AppendLine("    @Learn1, @Learn2, @Learn3, @Attitude1, @Attitude2, @Relation1, @Relation2, @Ability1, @Ability2,")
            strSQL.AppendLine("    @Ability3, @Ability4, @Leader1, @Leader2, @Leader3, @Leader4, @Leader5, @Attachment, @Result,")
            strSQL.AppendLine("    @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In ProbationRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@ApplyTime", DbType.Date, IIf(IsDateTimeNull(r.ApplyTime.Value), Convert.ToDateTime("1900/1/1"), r.ApplyTime.Value))
                db.AddInParameter(dbcmd, "@ApplyID", DbType.String, r.ApplyID.Value)
                db.AddInParameter(dbcmd, "@ProbSeq", DbType.Int32, r.ProbSeq.Value)
                db.AddInParameter(dbcmd, "@ProbDate", DbType.Date, IIf(IsDateTimeNull(r.ProbDate.Value), Convert.ToDateTime("1900/1/1"), r.ProbDate.Value))
                db.AddInParameter(dbcmd, "@ProbDate2", DbType.Date, IIf(IsDateTimeNull(r.ProbDate2.Value), Convert.ToDateTime("1900/1/1"), r.ProbDate2.Value))
                db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                db.AddInParameter(dbcmd, "@DeptName", DbType.String, r.DeptName.Value)
                db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                db.AddInParameter(dbcmd, "@OrganName", DbType.String, r.OrganName.Value)
                db.AddInParameter(dbcmd, "@PositionID", DbType.String, r.PositionID.Value)
                db.AddInParameter(dbcmd, "@PositionName", DbType.String, r.PositionName.Value)
                db.AddInParameter(dbcmd, "@WorkTypeName", DbType.String, r.WorkTypeName.Value)
                db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                db.AddInParameter(dbcmd, "@TitleName", DbType.String, r.TitleName.Value)
                db.AddInParameter(dbcmd, "@EmpDate", DbType.Date, IIf(IsDateTimeNull(r.EmpDate.Value), Convert.ToDateTime("1900/1/1"), r.EmpDate.Value))
                db.AddInParameter(dbcmd, "@ExtendFlag", DbType.String, r.ExtendFlag.Value)
                db.AddInParameter(dbcmd, "@Learn1", DbType.String, r.Learn1.Value)
                db.AddInParameter(dbcmd, "@Learn2", DbType.String, r.Learn2.Value)
                db.AddInParameter(dbcmd, "@Learn3", DbType.String, r.Learn3.Value)
                db.AddInParameter(dbcmd, "@Attitude1", DbType.String, r.Attitude1.Value)
                db.AddInParameter(dbcmd, "@Attitude2", DbType.String, r.Attitude2.Value)
                db.AddInParameter(dbcmd, "@Relation1", DbType.String, r.Relation1.Value)
                db.AddInParameter(dbcmd, "@Relation2", DbType.String, r.Relation2.Value)
                db.AddInParameter(dbcmd, "@Ability1", DbType.String, r.Ability1.Value)
                db.AddInParameter(dbcmd, "@Ability2", DbType.String, r.Ability2.Value)
                db.AddInParameter(dbcmd, "@Ability3", DbType.String, r.Ability3.Value)
                db.AddInParameter(dbcmd, "@Ability4", DbType.String, r.Ability4.Value)
                db.AddInParameter(dbcmd, "@Leader1", DbType.String, r.Leader1.Value)
                db.AddInParameter(dbcmd, "@Leader2", DbType.String, r.Leader2.Value)
                db.AddInParameter(dbcmd, "@Leader3", DbType.String, r.Leader3.Value)
                db.AddInParameter(dbcmd, "@Leader4", DbType.String, r.Leader4.Value)
                db.AddInParameter(dbcmd, "@Leader5", DbType.String, r.Leader5.Value)
                db.AddInParameter(dbcmd, "@Attachment", DbType.String, r.Attachment.Value)
                db.AddInParameter(dbcmd, "@Result", DbType.String, r.Result.Value)
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

