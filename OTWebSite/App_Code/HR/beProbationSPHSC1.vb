'****************************************************************
' Table:ProbationSPHSC1
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

Namespace beProbationSPHSC1
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "ApplyTime", "ApplyID", "ProbSeq", "ProbDate", "ProbDate2", "DeptID", "DeptName", "OrganID", "OrganName" _
                                    , "WorkTypeName", "RankID", "TitleName", "EmpDate", "ExtendFlag", "Ability1", "Ability2", "Attitude1", "Attitude2", "Attitude3", "Character1" _
                                    , "Character2", "Character3", "Comment", "Attachment", "Result", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(Date), GetType(String), GetType(Integer), GetType(Date), GetType(Date), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(Date), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "CompID", "ApplyTime", "ApplyID" }

        Public ReadOnly Property Rows() As beProbationSPHSC1.Rows 
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
        Public Sub Transfer2Row(ProbationSPHSC1Table As DataTable)
            For Each dr As DataRow In ProbationSPHSC1Table.Rows
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
                dr(m_Rows(i).WorkTypeName.FieldName) = m_Rows(i).WorkTypeName.Value
                dr(m_Rows(i).RankID.FieldName) = m_Rows(i).RankID.Value
                dr(m_Rows(i).TitleName.FieldName) = m_Rows(i).TitleName.Value
                dr(m_Rows(i).EmpDate.FieldName) = m_Rows(i).EmpDate.Value
                dr(m_Rows(i).ExtendFlag.FieldName) = m_Rows(i).ExtendFlag.Value
                dr(m_Rows(i).Ability1.FieldName) = m_Rows(i).Ability1.Value
                dr(m_Rows(i).Ability2.FieldName) = m_Rows(i).Ability2.Value
                dr(m_Rows(i).Attitude1.FieldName) = m_Rows(i).Attitude1.Value
                dr(m_Rows(i).Attitude2.FieldName) = m_Rows(i).Attitude2.Value
                dr(m_Rows(i).Attitude3.FieldName) = m_Rows(i).Attitude3.Value
                dr(m_Rows(i).Character1.FieldName) = m_Rows(i).Character1.Value
                dr(m_Rows(i).Character2.FieldName) = m_Rows(i).Character2.Value
                dr(m_Rows(i).Character3.FieldName) = m_Rows(i).Character3.Value
                dr(m_Rows(i).Comment.FieldName) = m_Rows(i).Comment.Value
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

        Public Sub Add(ProbationSPHSC1Row As Row)
            m_Rows.Add(ProbationSPHSC1Row)
        End Sub

        Public Sub Remove(ProbationSPHSC1Row As Row)
            If m_Rows.IndexOf(ProbationSPHSC1Row) >= 0 Then
                m_Rows.Remove(ProbationSPHSC1Row)
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
        Private FI_WorkTypeName As Field(Of String) = new Field(Of String)("WorkTypeName", true)
        Private FI_RankID As Field(Of String) = new Field(Of String)("RankID", true)
        Private FI_TitleName As Field(Of String) = new Field(Of String)("TitleName", true)
        Private FI_EmpDate As Field(Of Date) = new Field(Of Date)("EmpDate", true)
        Private FI_ExtendFlag As Field(Of String) = new Field(Of String)("ExtendFlag", true)
        Private FI_Ability1 As Field(Of String) = new Field(Of String)("Ability1", true)
        Private FI_Ability2 As Field(Of String) = new Field(Of String)("Ability2", true)
        Private FI_Attitude1 As Field(Of String) = new Field(Of String)("Attitude1", true)
        Private FI_Attitude2 As Field(Of String) = new Field(Of String)("Attitude2", true)
        Private FI_Attitude3 As Field(Of String) = new Field(Of String)("Attitude3", true)
        Private FI_Character1 As Field(Of String) = new Field(Of String)("Character1", true)
        Private FI_Character2 As Field(Of String) = new Field(Of String)("Character2", true)
        Private FI_Character3 As Field(Of String) = new Field(Of String)("Character3", true)
        Private FI_Comment As Field(Of String) = new Field(Of String)("Comment", true)
        Private FI_Attachment As Field(Of String) = new Field(Of String)("Attachment", true)
        Private FI_Result As Field(Of String) = new Field(Of String)("Result", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "CompID", "ApplyTime", "ApplyID", "ProbSeq", "ProbDate", "ProbDate2", "DeptID", "DeptName", "OrganID", "OrganName" _
                                    , "WorkTypeName", "RankID", "TitleName", "EmpDate", "ExtendFlag", "Ability1", "Ability2", "Attitude1", "Attitude2", "Attitude3", "Character1" _
                                    , "Character2", "Character3", "Comment", "Attachment", "Result", "LastChgComp", "LastChgID", "LastChgDate" }
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
                Case "Ability1"
                    Return FI_Ability1.Value
                Case "Ability2"
                    Return FI_Ability2.Value
                Case "Attitude1"
                    Return FI_Attitude1.Value
                Case "Attitude2"
                    Return FI_Attitude2.Value
                Case "Attitude3"
                    Return FI_Attitude3.Value
                Case "Character1"
                    Return FI_Character1.Value
                Case "Character2"
                    Return FI_Character2.Value
                Case "Character3"
                    Return FI_Character3.Value
                Case "Comment"
                    Return FI_Comment.Value
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
                Case "Ability1"
                    FI_Ability1.SetValue(value)
                Case "Ability2"
                    FI_Ability2.SetValue(value)
                Case "Attitude1"
                    FI_Attitude1.SetValue(value)
                Case "Attitude2"
                    FI_Attitude2.SetValue(value)
                Case "Attitude3"
                    FI_Attitude3.SetValue(value)
                Case "Character1"
                    FI_Character1.SetValue(value)
                Case "Character2"
                    FI_Character2.SetValue(value)
                Case "Character3"
                    FI_Character3.SetValue(value)
                Case "Comment"
                    FI_Comment.SetValue(value)
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
                Case "Ability1"
                    return FI_Ability1.Updated
                Case "Ability2"
                    return FI_Ability2.Updated
                Case "Attitude1"
                    return FI_Attitude1.Updated
                Case "Attitude2"
                    return FI_Attitude2.Updated
                Case "Attitude3"
                    return FI_Attitude3.Updated
                Case "Character1"
                    return FI_Character1.Updated
                Case "Character2"
                    return FI_Character2.Updated
                Case "Character3"
                    return FI_Character3.Updated
                Case "Comment"
                    return FI_Comment.Updated
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
                Case "Ability1"
                    return FI_Ability1.CreateUpdateSQL
                Case "Ability2"
                    return FI_Ability2.CreateUpdateSQL
                Case "Attitude1"
                    return FI_Attitude1.CreateUpdateSQL
                Case "Attitude2"
                    return FI_Attitude2.CreateUpdateSQL
                Case "Attitude3"
                    return FI_Attitude3.CreateUpdateSQL
                Case "Character1"
                    return FI_Character1.CreateUpdateSQL
                Case "Character2"
                    return FI_Character2.CreateUpdateSQL
                Case "Character3"
                    return FI_Character3.CreateUpdateSQL
                Case "Comment"
                    return FI_Comment.CreateUpdateSQL
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
            FI_WorkTypeName.SetInitValue("")
            FI_RankID.SetInitValue("")
            FI_TitleName.SetInitValue("")
            FI_EmpDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_ExtendFlag.SetInitValue("0")
            FI_Ability1.SetInitValue("")
            FI_Ability2.SetInitValue("")
            FI_Attitude1.SetInitValue("")
            FI_Attitude2.SetInitValue("")
            FI_Attitude3.SetInitValue("")
            FI_Character1.SetInitValue("")
            FI_Character2.SetInitValue("")
            FI_Character3.SetInitValue("")
            FI_Comment.SetInitValue("")
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
            FI_WorkTypeName.SetInitValue(dr("WorkTypeName"))
            FI_RankID.SetInitValue(dr("RankID"))
            FI_TitleName.SetInitValue(dr("TitleName"))
            FI_EmpDate.SetInitValue(dr("EmpDate"))
            FI_ExtendFlag.SetInitValue(dr("ExtendFlag"))
            FI_Ability1.SetInitValue(dr("Ability1"))
            FI_Ability2.SetInitValue(dr("Ability2"))
            FI_Attitude1.SetInitValue(dr("Attitude1"))
            FI_Attitude2.SetInitValue(dr("Attitude2"))
            FI_Attitude3.SetInitValue(dr("Attitude3"))
            FI_Character1.SetInitValue(dr("Character1"))
            FI_Character2.SetInitValue(dr("Character2"))
            FI_Character3.SetInitValue(dr("Character3"))
            FI_Comment.SetInitValue(dr("Comment"))
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
            FI_WorkTypeName.Updated = False
            FI_RankID.Updated = False
            FI_TitleName.Updated = False
            FI_EmpDate.Updated = False
            FI_ExtendFlag.Updated = False
            FI_Ability1.Updated = False
            FI_Ability2.Updated = False
            FI_Attitude1.Updated = False
            FI_Attitude2.Updated = False
            FI_Attitude3.Updated = False
            FI_Character1.Updated = False
            FI_Character2.Updated = False
            FI_Character3.Updated = False
            FI_Comment.Updated = False
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

        Public ReadOnly Property Attitude3 As Field(Of String) 
            Get
                Return FI_Attitude3
            End Get
        End Property

        Public ReadOnly Property Character1 As Field(Of String) 
            Get
                Return FI_Character1
            End Get
        End Property

        Public ReadOnly Property Character2 As Field(Of String) 
            Get
                Return FI_Character2
            End Get
        End Property

        Public ReadOnly Property Character3 As Field(Of String) 
            Get
                Return FI_Character3
            End Get
        End Property

        Public ReadOnly Property Comment As Field(Of String) 
            Get
                Return FI_Comment
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
        Public Function DeleteRowByPrimaryKey(ByVal ProbationSPHSC1Row As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From ProbationSPHSC1")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And ApplyTime = @ApplyTime")
            strSQL.AppendLine("And ApplyID = @ApplyID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, ProbationSPHSC1Row.CompID.Value)
            db.AddInParameter(dbcmd, "@ApplyTime", DbType.Date, ProbationSPHSC1Row.ApplyTime.Value)
            db.AddInParameter(dbcmd, "@ApplyID", DbType.String, ProbationSPHSC1Row.ApplyID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal ProbationSPHSC1Row As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From ProbationSPHSC1")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And ApplyTime = @ApplyTime")
            strSQL.AppendLine("And ApplyID = @ApplyID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, ProbationSPHSC1Row.CompID.Value)
            db.AddInParameter(dbcmd, "@ApplyTime", DbType.Date, ProbationSPHSC1Row.ApplyTime.Value)
            db.AddInParameter(dbcmd, "@ApplyID", DbType.String, ProbationSPHSC1Row.ApplyID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal ProbationSPHSC1Row As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From ProbationSPHSC1")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And ApplyTime = @ApplyTime")
            strSQL.AppendLine("And ApplyID = @ApplyID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In ProbationSPHSC1Row
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

        Public Function DeleteRowByPrimaryKey(ByVal ProbationSPHSC1Row As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From ProbationSPHSC1")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And ApplyTime = @ApplyTime")
            strSQL.AppendLine("And ApplyID = @ApplyID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In ProbationSPHSC1Row
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@ApplyTime", DbType.Date, r.ApplyTime.Value)
                db.AddInParameter(dbcmd, "@ApplyID", DbType.String, r.ApplyID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal ProbationSPHSC1Row As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From ProbationSPHSC1")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And ApplyTime = @ApplyTime")
            strSQL.AppendLine("And ApplyID = @ApplyID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, ProbationSPHSC1Row.CompID.Value)
            db.AddInParameter(dbcmd, "@ApplyTime", DbType.Date, ProbationSPHSC1Row.ApplyTime.Value)
            db.AddInParameter(dbcmd, "@ApplyID", DbType.String, ProbationSPHSC1Row.ApplyID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(ProbationSPHSC1Row As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From ProbationSPHSC1")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And ApplyTime = @ApplyTime")
            strSQL.AppendLine("And ApplyID = @ApplyID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, ProbationSPHSC1Row.CompID.Value)
            db.AddInParameter(dbcmd, "@ApplyTime", DbType.Date, ProbationSPHSC1Row.ApplyTime.Value)
            db.AddInParameter(dbcmd, "@ApplyID", DbType.String, ProbationSPHSC1Row.ApplyID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal ProbationSPHSC1Row As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update ProbationSPHSC1 Set")
            For i As Integer = 0 To ProbationSPHSC1Row.FieldNames.Length - 1
                If Not ProbationSPHSC1Row.IsIdentityField(ProbationSPHSC1Row.FieldNames(i)) AndAlso ProbationSPHSC1Row.IsUpdated(ProbationSPHSC1Row.FieldNames(i)) AndAlso ProbationSPHSC1Row.CreateUpdateSQL(ProbationSPHSC1Row.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, ProbationSPHSC1Row.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And ApplyTime = @PKApplyTime")
            strSQL.AppendLine("And ApplyID = @PKApplyID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If ProbationSPHSC1Row.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, ProbationSPHSC1Row.CompID.Value)
            If ProbationSPHSC1Row.ApplyTime.Updated Then db.AddInParameter(dbcmd, "@ApplyTime", DbType.Date, IIf(IsDateTimeNull(ProbationSPHSC1Row.ApplyTime.Value), Convert.ToDateTime("1900/1/1"), ProbationSPHSC1Row.ApplyTime.Value))
            If ProbationSPHSC1Row.ApplyID.Updated Then db.AddInParameter(dbcmd, "@ApplyID", DbType.String, ProbationSPHSC1Row.ApplyID.Value)
            If ProbationSPHSC1Row.ProbSeq.Updated Then db.AddInParameter(dbcmd, "@ProbSeq", DbType.Int32, ProbationSPHSC1Row.ProbSeq.Value)
            If ProbationSPHSC1Row.ProbDate.Updated Then db.AddInParameter(dbcmd, "@ProbDate", DbType.Date, IIf(IsDateTimeNull(ProbationSPHSC1Row.ProbDate.Value), Convert.ToDateTime("1900/1/1"), ProbationSPHSC1Row.ProbDate.Value))
            If ProbationSPHSC1Row.ProbDate2.Updated Then db.AddInParameter(dbcmd, "@ProbDate2", DbType.Date, IIf(IsDateTimeNull(ProbationSPHSC1Row.ProbDate2.Value), Convert.ToDateTime("1900/1/1"), ProbationSPHSC1Row.ProbDate2.Value))
            If ProbationSPHSC1Row.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, ProbationSPHSC1Row.DeptID.Value)
            If ProbationSPHSC1Row.DeptName.Updated Then db.AddInParameter(dbcmd, "@DeptName", DbType.String, ProbationSPHSC1Row.DeptName.Value)
            If ProbationSPHSC1Row.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, ProbationSPHSC1Row.OrganID.Value)
            If ProbationSPHSC1Row.OrganName.Updated Then db.AddInParameter(dbcmd, "@OrganName", DbType.String, ProbationSPHSC1Row.OrganName.Value)
            If ProbationSPHSC1Row.WorkTypeName.Updated Then db.AddInParameter(dbcmd, "@WorkTypeName", DbType.String, ProbationSPHSC1Row.WorkTypeName.Value)
            If ProbationSPHSC1Row.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, ProbationSPHSC1Row.RankID.Value)
            If ProbationSPHSC1Row.TitleName.Updated Then db.AddInParameter(dbcmd, "@TitleName", DbType.String, ProbationSPHSC1Row.TitleName.Value)
            If ProbationSPHSC1Row.EmpDate.Updated Then db.AddInParameter(dbcmd, "@EmpDate", DbType.Date, IIf(IsDateTimeNull(ProbationSPHSC1Row.EmpDate.Value), Convert.ToDateTime("1900/1/1"), ProbationSPHSC1Row.EmpDate.Value))
            If ProbationSPHSC1Row.ExtendFlag.Updated Then db.AddInParameter(dbcmd, "@ExtendFlag", DbType.String, ProbationSPHSC1Row.ExtendFlag.Value)
            If ProbationSPHSC1Row.Ability1.Updated Then db.AddInParameter(dbcmd, "@Ability1", DbType.String, ProbationSPHSC1Row.Ability1.Value)
            If ProbationSPHSC1Row.Ability2.Updated Then db.AddInParameter(dbcmd, "@Ability2", DbType.String, ProbationSPHSC1Row.Ability2.Value)
            If ProbationSPHSC1Row.Attitude1.Updated Then db.AddInParameter(dbcmd, "@Attitude1", DbType.String, ProbationSPHSC1Row.Attitude1.Value)
            If ProbationSPHSC1Row.Attitude2.Updated Then db.AddInParameter(dbcmd, "@Attitude2", DbType.String, ProbationSPHSC1Row.Attitude2.Value)
            If ProbationSPHSC1Row.Attitude3.Updated Then db.AddInParameter(dbcmd, "@Attitude3", DbType.String, ProbationSPHSC1Row.Attitude3.Value)
            If ProbationSPHSC1Row.Character1.Updated Then db.AddInParameter(dbcmd, "@Character1", DbType.String, ProbationSPHSC1Row.Character1.Value)
            If ProbationSPHSC1Row.Character2.Updated Then db.AddInParameter(dbcmd, "@Character2", DbType.String, ProbationSPHSC1Row.Character2.Value)
            If ProbationSPHSC1Row.Character3.Updated Then db.AddInParameter(dbcmd, "@Character3", DbType.String, ProbationSPHSC1Row.Character3.Value)
            If ProbationSPHSC1Row.Comment.Updated Then db.AddInParameter(dbcmd, "@Comment", DbType.String, ProbationSPHSC1Row.Comment.Value)
            If ProbationSPHSC1Row.Attachment.Updated Then db.AddInParameter(dbcmd, "@Attachment", DbType.String, ProbationSPHSC1Row.Attachment.Value)
            If ProbationSPHSC1Row.Result.Updated Then db.AddInParameter(dbcmd, "@Result", DbType.String, ProbationSPHSC1Row.Result.Value)
            If ProbationSPHSC1Row.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, ProbationSPHSC1Row.LastChgComp.Value)
            If ProbationSPHSC1Row.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, ProbationSPHSC1Row.LastChgID.Value)
            If ProbationSPHSC1Row.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(ProbationSPHSC1Row.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), ProbationSPHSC1Row.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(ProbationSPHSC1Row.LoadFromDataRow, ProbationSPHSC1Row.CompID.OldValue, ProbationSPHSC1Row.CompID.Value))
            db.AddInParameter(dbcmd, "@PKApplyTime", DbType.Date, IIf(ProbationSPHSC1Row.LoadFromDataRow, ProbationSPHSC1Row.ApplyTime.OldValue, ProbationSPHSC1Row.ApplyTime.Value))
            db.AddInParameter(dbcmd, "@PKApplyID", DbType.String, IIf(ProbationSPHSC1Row.LoadFromDataRow, ProbationSPHSC1Row.ApplyID.OldValue, ProbationSPHSC1Row.ApplyID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal ProbationSPHSC1Row As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update ProbationSPHSC1 Set")
            For i As Integer = 0 To ProbationSPHSC1Row.FieldNames.Length - 1
                If Not ProbationSPHSC1Row.IsIdentityField(ProbationSPHSC1Row.FieldNames(i)) AndAlso ProbationSPHSC1Row.IsUpdated(ProbationSPHSC1Row.FieldNames(i)) AndAlso ProbationSPHSC1Row.CreateUpdateSQL(ProbationSPHSC1Row.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, ProbationSPHSC1Row.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And ApplyTime = @PKApplyTime")
            strSQL.AppendLine("And ApplyID = @PKApplyID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If ProbationSPHSC1Row.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, ProbationSPHSC1Row.CompID.Value)
            If ProbationSPHSC1Row.ApplyTime.Updated Then db.AddInParameter(dbcmd, "@ApplyTime", DbType.Date, IIf(IsDateTimeNull(ProbationSPHSC1Row.ApplyTime.Value), Convert.ToDateTime("1900/1/1"), ProbationSPHSC1Row.ApplyTime.Value))
            If ProbationSPHSC1Row.ApplyID.Updated Then db.AddInParameter(dbcmd, "@ApplyID", DbType.String, ProbationSPHSC1Row.ApplyID.Value)
            If ProbationSPHSC1Row.ProbSeq.Updated Then db.AddInParameter(dbcmd, "@ProbSeq", DbType.Int32, ProbationSPHSC1Row.ProbSeq.Value)
            If ProbationSPHSC1Row.ProbDate.Updated Then db.AddInParameter(dbcmd, "@ProbDate", DbType.Date, IIf(IsDateTimeNull(ProbationSPHSC1Row.ProbDate.Value), Convert.ToDateTime("1900/1/1"), ProbationSPHSC1Row.ProbDate.Value))
            If ProbationSPHSC1Row.ProbDate2.Updated Then db.AddInParameter(dbcmd, "@ProbDate2", DbType.Date, IIf(IsDateTimeNull(ProbationSPHSC1Row.ProbDate2.Value), Convert.ToDateTime("1900/1/1"), ProbationSPHSC1Row.ProbDate2.Value))
            If ProbationSPHSC1Row.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, ProbationSPHSC1Row.DeptID.Value)
            If ProbationSPHSC1Row.DeptName.Updated Then db.AddInParameter(dbcmd, "@DeptName", DbType.String, ProbationSPHSC1Row.DeptName.Value)
            If ProbationSPHSC1Row.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, ProbationSPHSC1Row.OrganID.Value)
            If ProbationSPHSC1Row.OrganName.Updated Then db.AddInParameter(dbcmd, "@OrganName", DbType.String, ProbationSPHSC1Row.OrganName.Value)
            If ProbationSPHSC1Row.WorkTypeName.Updated Then db.AddInParameter(dbcmd, "@WorkTypeName", DbType.String, ProbationSPHSC1Row.WorkTypeName.Value)
            If ProbationSPHSC1Row.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, ProbationSPHSC1Row.RankID.Value)
            If ProbationSPHSC1Row.TitleName.Updated Then db.AddInParameter(dbcmd, "@TitleName", DbType.String, ProbationSPHSC1Row.TitleName.Value)
            If ProbationSPHSC1Row.EmpDate.Updated Then db.AddInParameter(dbcmd, "@EmpDate", DbType.Date, IIf(IsDateTimeNull(ProbationSPHSC1Row.EmpDate.Value), Convert.ToDateTime("1900/1/1"), ProbationSPHSC1Row.EmpDate.Value))
            If ProbationSPHSC1Row.ExtendFlag.Updated Then db.AddInParameter(dbcmd, "@ExtendFlag", DbType.String, ProbationSPHSC1Row.ExtendFlag.Value)
            If ProbationSPHSC1Row.Ability1.Updated Then db.AddInParameter(dbcmd, "@Ability1", DbType.String, ProbationSPHSC1Row.Ability1.Value)
            If ProbationSPHSC1Row.Ability2.Updated Then db.AddInParameter(dbcmd, "@Ability2", DbType.String, ProbationSPHSC1Row.Ability2.Value)
            If ProbationSPHSC1Row.Attitude1.Updated Then db.AddInParameter(dbcmd, "@Attitude1", DbType.String, ProbationSPHSC1Row.Attitude1.Value)
            If ProbationSPHSC1Row.Attitude2.Updated Then db.AddInParameter(dbcmd, "@Attitude2", DbType.String, ProbationSPHSC1Row.Attitude2.Value)
            If ProbationSPHSC1Row.Attitude3.Updated Then db.AddInParameter(dbcmd, "@Attitude3", DbType.String, ProbationSPHSC1Row.Attitude3.Value)
            If ProbationSPHSC1Row.Character1.Updated Then db.AddInParameter(dbcmd, "@Character1", DbType.String, ProbationSPHSC1Row.Character1.Value)
            If ProbationSPHSC1Row.Character2.Updated Then db.AddInParameter(dbcmd, "@Character2", DbType.String, ProbationSPHSC1Row.Character2.Value)
            If ProbationSPHSC1Row.Character3.Updated Then db.AddInParameter(dbcmd, "@Character3", DbType.String, ProbationSPHSC1Row.Character3.Value)
            If ProbationSPHSC1Row.Comment.Updated Then db.AddInParameter(dbcmd, "@Comment", DbType.String, ProbationSPHSC1Row.Comment.Value)
            If ProbationSPHSC1Row.Attachment.Updated Then db.AddInParameter(dbcmd, "@Attachment", DbType.String, ProbationSPHSC1Row.Attachment.Value)
            If ProbationSPHSC1Row.Result.Updated Then db.AddInParameter(dbcmd, "@Result", DbType.String, ProbationSPHSC1Row.Result.Value)
            If ProbationSPHSC1Row.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, ProbationSPHSC1Row.LastChgComp.Value)
            If ProbationSPHSC1Row.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, ProbationSPHSC1Row.LastChgID.Value)
            If ProbationSPHSC1Row.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(ProbationSPHSC1Row.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), ProbationSPHSC1Row.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(ProbationSPHSC1Row.LoadFromDataRow, ProbationSPHSC1Row.CompID.OldValue, ProbationSPHSC1Row.CompID.Value))
            db.AddInParameter(dbcmd, "@PKApplyTime", DbType.Date, IIf(ProbationSPHSC1Row.LoadFromDataRow, ProbationSPHSC1Row.ApplyTime.OldValue, ProbationSPHSC1Row.ApplyTime.Value))
            db.AddInParameter(dbcmd, "@PKApplyID", DbType.String, IIf(ProbationSPHSC1Row.LoadFromDataRow, ProbationSPHSC1Row.ApplyID.OldValue, ProbationSPHSC1Row.ApplyID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal ProbationSPHSC1Row As Row()) As Integer
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
                    For Each r As Row In ProbationSPHSC1Row
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update ProbationSPHSC1 Set")
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
                        If r.WorkTypeName.Updated Then db.AddInParameter(dbcmd, "@WorkTypeName", DbType.String, r.WorkTypeName.Value)
                        If r.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                        If r.TitleName.Updated Then db.AddInParameter(dbcmd, "@TitleName", DbType.String, r.TitleName.Value)
                        If r.EmpDate.Updated Then db.AddInParameter(dbcmd, "@EmpDate", DbType.Date, IIf(IsDateTimeNull(r.EmpDate.Value), Convert.ToDateTime("1900/1/1"), r.EmpDate.Value))
                        If r.ExtendFlag.Updated Then db.AddInParameter(dbcmd, "@ExtendFlag", DbType.String, r.ExtendFlag.Value)
                        If r.Ability1.Updated Then db.AddInParameter(dbcmd, "@Ability1", DbType.String, r.Ability1.Value)
                        If r.Ability2.Updated Then db.AddInParameter(dbcmd, "@Ability2", DbType.String, r.Ability2.Value)
                        If r.Attitude1.Updated Then db.AddInParameter(dbcmd, "@Attitude1", DbType.String, r.Attitude1.Value)
                        If r.Attitude2.Updated Then db.AddInParameter(dbcmd, "@Attitude2", DbType.String, r.Attitude2.Value)
                        If r.Attitude3.Updated Then db.AddInParameter(dbcmd, "@Attitude3", DbType.String, r.Attitude3.Value)
                        If r.Character1.Updated Then db.AddInParameter(dbcmd, "@Character1", DbType.String, r.Character1.Value)
                        If r.Character2.Updated Then db.AddInParameter(dbcmd, "@Character2", DbType.String, r.Character2.Value)
                        If r.Character3.Updated Then db.AddInParameter(dbcmd, "@Character3", DbType.String, r.Character3.Value)
                        If r.Comment.Updated Then db.AddInParameter(dbcmd, "@Comment", DbType.String, r.Comment.Value)
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

        Public Function Update(ByVal ProbationSPHSC1Row As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In ProbationSPHSC1Row
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update ProbationSPHSC1 Set")
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
                If r.WorkTypeName.Updated Then db.AddInParameter(dbcmd, "@WorkTypeName", DbType.String, r.WorkTypeName.Value)
                If r.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                If r.TitleName.Updated Then db.AddInParameter(dbcmd, "@TitleName", DbType.String, r.TitleName.Value)
                If r.EmpDate.Updated Then db.AddInParameter(dbcmd, "@EmpDate", DbType.Date, IIf(IsDateTimeNull(r.EmpDate.Value), Convert.ToDateTime("1900/1/1"), r.EmpDate.Value))
                If r.ExtendFlag.Updated Then db.AddInParameter(dbcmd, "@ExtendFlag", DbType.String, r.ExtendFlag.Value)
                If r.Ability1.Updated Then db.AddInParameter(dbcmd, "@Ability1", DbType.String, r.Ability1.Value)
                If r.Ability2.Updated Then db.AddInParameter(dbcmd, "@Ability2", DbType.String, r.Ability2.Value)
                If r.Attitude1.Updated Then db.AddInParameter(dbcmd, "@Attitude1", DbType.String, r.Attitude1.Value)
                If r.Attitude2.Updated Then db.AddInParameter(dbcmd, "@Attitude2", DbType.String, r.Attitude2.Value)
                If r.Attitude3.Updated Then db.AddInParameter(dbcmd, "@Attitude3", DbType.String, r.Attitude3.Value)
                If r.Character1.Updated Then db.AddInParameter(dbcmd, "@Character1", DbType.String, r.Character1.Value)
                If r.Character2.Updated Then db.AddInParameter(dbcmd, "@Character2", DbType.String, r.Character2.Value)
                If r.Character3.Updated Then db.AddInParameter(dbcmd, "@Character3", DbType.String, r.Character3.Value)
                If r.Comment.Updated Then db.AddInParameter(dbcmd, "@Comment", DbType.String, r.Comment.Value)
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

        Public Function IsDataExists(ByVal ProbationSPHSC1Row As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From ProbationSPHSC1")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And ApplyTime = @ApplyTime")
            strSQL.AppendLine("And ApplyID = @ApplyID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, ProbationSPHSC1Row.CompID.Value)
            db.AddInParameter(dbcmd, "@ApplyTime", DbType.Date, ProbationSPHSC1Row.ApplyTime.Value)
            db.AddInParameter(dbcmd, "@ApplyID", DbType.String, ProbationSPHSC1Row.ApplyID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal ProbationSPHSC1Row As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From ProbationSPHSC1")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And ApplyTime = @ApplyTime")
            strSQL.AppendLine("And ApplyID = @ApplyID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, ProbationSPHSC1Row.CompID.Value)
            db.AddInParameter(dbcmd, "@ApplyTime", DbType.Date, ProbationSPHSC1Row.ApplyTime.Value)
            db.AddInParameter(dbcmd, "@ApplyID", DbType.String, ProbationSPHSC1Row.ApplyID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From ProbationSPHSC1")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal ProbationSPHSC1Row As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into ProbationSPHSC1")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, ApplyTime, ApplyID, ProbSeq, ProbDate, ProbDate2, DeptID, DeptName, OrganID,")
            strSQL.AppendLine("    OrganName, WorkTypeName, RankID, TitleName, EmpDate, ExtendFlag, Ability1, Ability2,")
            strSQL.AppendLine("    Attitude1, Attitude2, Attitude3, Character1, Character2, Character3, Comment, Attachment,")
            strSQL.AppendLine("    Result, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @ApplyTime, @ApplyID, @ProbSeq, @ProbDate, @ProbDate2, @DeptID, @DeptName, @OrganID,")
            strSQL.AppendLine("    @OrganName, @WorkTypeName, @RankID, @TitleName, @EmpDate, @ExtendFlag, @Ability1, @Ability2,")
            strSQL.AppendLine("    @Attitude1, @Attitude2, @Attitude3, @Character1, @Character2, @Character3, @Comment, @Attachment,")
            strSQL.AppendLine("    @Result, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, ProbationSPHSC1Row.CompID.Value)
            db.AddInParameter(dbcmd, "@ApplyTime", DbType.Date, IIf(IsDateTimeNull(ProbationSPHSC1Row.ApplyTime.Value), Convert.ToDateTime("1900/1/1"), ProbationSPHSC1Row.ApplyTime.Value))
            db.AddInParameter(dbcmd, "@ApplyID", DbType.String, ProbationSPHSC1Row.ApplyID.Value)
            db.AddInParameter(dbcmd, "@ProbSeq", DbType.Int32, ProbationSPHSC1Row.ProbSeq.Value)
            db.AddInParameter(dbcmd, "@ProbDate", DbType.Date, IIf(IsDateTimeNull(ProbationSPHSC1Row.ProbDate.Value), Convert.ToDateTime("1900/1/1"), ProbationSPHSC1Row.ProbDate.Value))
            db.AddInParameter(dbcmd, "@ProbDate2", DbType.Date, IIf(IsDateTimeNull(ProbationSPHSC1Row.ProbDate2.Value), Convert.ToDateTime("1900/1/1"), ProbationSPHSC1Row.ProbDate2.Value))
            db.AddInParameter(dbcmd, "@DeptID", DbType.String, ProbationSPHSC1Row.DeptID.Value)
            db.AddInParameter(dbcmd, "@DeptName", DbType.String, ProbationSPHSC1Row.DeptName.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, ProbationSPHSC1Row.OrganID.Value)
            db.AddInParameter(dbcmd, "@OrganName", DbType.String, ProbationSPHSC1Row.OrganName.Value)
            db.AddInParameter(dbcmd, "@WorkTypeName", DbType.String, ProbationSPHSC1Row.WorkTypeName.Value)
            db.AddInParameter(dbcmd, "@RankID", DbType.String, ProbationSPHSC1Row.RankID.Value)
            db.AddInParameter(dbcmd, "@TitleName", DbType.String, ProbationSPHSC1Row.TitleName.Value)
            db.AddInParameter(dbcmd, "@EmpDate", DbType.Date, IIf(IsDateTimeNull(ProbationSPHSC1Row.EmpDate.Value), Convert.ToDateTime("1900/1/1"), ProbationSPHSC1Row.EmpDate.Value))
            db.AddInParameter(dbcmd, "@ExtendFlag", DbType.String, ProbationSPHSC1Row.ExtendFlag.Value)
            db.AddInParameter(dbcmd, "@Ability1", DbType.String, ProbationSPHSC1Row.Ability1.Value)
            db.AddInParameter(dbcmd, "@Ability2", DbType.String, ProbationSPHSC1Row.Ability2.Value)
            db.AddInParameter(dbcmd, "@Attitude1", DbType.String, ProbationSPHSC1Row.Attitude1.Value)
            db.AddInParameter(dbcmd, "@Attitude2", DbType.String, ProbationSPHSC1Row.Attitude2.Value)
            db.AddInParameter(dbcmd, "@Attitude3", DbType.String, ProbationSPHSC1Row.Attitude3.Value)
            db.AddInParameter(dbcmd, "@Character1", DbType.String, ProbationSPHSC1Row.Character1.Value)
            db.AddInParameter(dbcmd, "@Character2", DbType.String, ProbationSPHSC1Row.Character2.Value)
            db.AddInParameter(dbcmd, "@Character3", DbType.String, ProbationSPHSC1Row.Character3.Value)
            db.AddInParameter(dbcmd, "@Comment", DbType.String, ProbationSPHSC1Row.Comment.Value)
            db.AddInParameter(dbcmd, "@Attachment", DbType.String, ProbationSPHSC1Row.Attachment.Value)
            db.AddInParameter(dbcmd, "@Result", DbType.String, ProbationSPHSC1Row.Result.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, ProbationSPHSC1Row.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, ProbationSPHSC1Row.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(ProbationSPHSC1Row.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), ProbationSPHSC1Row.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal ProbationSPHSC1Row As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into ProbationSPHSC1")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, ApplyTime, ApplyID, ProbSeq, ProbDate, ProbDate2, DeptID, DeptName, OrganID,")
            strSQL.AppendLine("    OrganName, WorkTypeName, RankID, TitleName, EmpDate, ExtendFlag, Ability1, Ability2,")
            strSQL.AppendLine("    Attitude1, Attitude2, Attitude3, Character1, Character2, Character3, Comment, Attachment,")
            strSQL.AppendLine("    Result, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @ApplyTime, @ApplyID, @ProbSeq, @ProbDate, @ProbDate2, @DeptID, @DeptName, @OrganID,")
            strSQL.AppendLine("    @OrganName, @WorkTypeName, @RankID, @TitleName, @EmpDate, @ExtendFlag, @Ability1, @Ability2,")
            strSQL.AppendLine("    @Attitude1, @Attitude2, @Attitude3, @Character1, @Character2, @Character3, @Comment, @Attachment,")
            strSQL.AppendLine("    @Result, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, ProbationSPHSC1Row.CompID.Value)
            db.AddInParameter(dbcmd, "@ApplyTime", DbType.Date, IIf(IsDateTimeNull(ProbationSPHSC1Row.ApplyTime.Value), Convert.ToDateTime("1900/1/1"), ProbationSPHSC1Row.ApplyTime.Value))
            db.AddInParameter(dbcmd, "@ApplyID", DbType.String, ProbationSPHSC1Row.ApplyID.Value)
            db.AddInParameter(dbcmd, "@ProbSeq", DbType.Int32, ProbationSPHSC1Row.ProbSeq.Value)
            db.AddInParameter(dbcmd, "@ProbDate", DbType.Date, IIf(IsDateTimeNull(ProbationSPHSC1Row.ProbDate.Value), Convert.ToDateTime("1900/1/1"), ProbationSPHSC1Row.ProbDate.Value))
            db.AddInParameter(dbcmd, "@ProbDate2", DbType.Date, IIf(IsDateTimeNull(ProbationSPHSC1Row.ProbDate2.Value), Convert.ToDateTime("1900/1/1"), ProbationSPHSC1Row.ProbDate2.Value))
            db.AddInParameter(dbcmd, "@DeptID", DbType.String, ProbationSPHSC1Row.DeptID.Value)
            db.AddInParameter(dbcmd, "@DeptName", DbType.String, ProbationSPHSC1Row.DeptName.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, ProbationSPHSC1Row.OrganID.Value)
            db.AddInParameter(dbcmd, "@OrganName", DbType.String, ProbationSPHSC1Row.OrganName.Value)
            db.AddInParameter(dbcmd, "@WorkTypeName", DbType.String, ProbationSPHSC1Row.WorkTypeName.Value)
            db.AddInParameter(dbcmd, "@RankID", DbType.String, ProbationSPHSC1Row.RankID.Value)
            db.AddInParameter(dbcmd, "@TitleName", DbType.String, ProbationSPHSC1Row.TitleName.Value)
            db.AddInParameter(dbcmd, "@EmpDate", DbType.Date, IIf(IsDateTimeNull(ProbationSPHSC1Row.EmpDate.Value), Convert.ToDateTime("1900/1/1"), ProbationSPHSC1Row.EmpDate.Value))
            db.AddInParameter(dbcmd, "@ExtendFlag", DbType.String, ProbationSPHSC1Row.ExtendFlag.Value)
            db.AddInParameter(dbcmd, "@Ability1", DbType.String, ProbationSPHSC1Row.Ability1.Value)
            db.AddInParameter(dbcmd, "@Ability2", DbType.String, ProbationSPHSC1Row.Ability2.Value)
            db.AddInParameter(dbcmd, "@Attitude1", DbType.String, ProbationSPHSC1Row.Attitude1.Value)
            db.AddInParameter(dbcmd, "@Attitude2", DbType.String, ProbationSPHSC1Row.Attitude2.Value)
            db.AddInParameter(dbcmd, "@Attitude3", DbType.String, ProbationSPHSC1Row.Attitude3.Value)
            db.AddInParameter(dbcmd, "@Character1", DbType.String, ProbationSPHSC1Row.Character1.Value)
            db.AddInParameter(dbcmd, "@Character2", DbType.String, ProbationSPHSC1Row.Character2.Value)
            db.AddInParameter(dbcmd, "@Character3", DbType.String, ProbationSPHSC1Row.Character3.Value)
            db.AddInParameter(dbcmd, "@Comment", DbType.String, ProbationSPHSC1Row.Comment.Value)
            db.AddInParameter(dbcmd, "@Attachment", DbType.String, ProbationSPHSC1Row.Attachment.Value)
            db.AddInParameter(dbcmd, "@Result", DbType.String, ProbationSPHSC1Row.Result.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, ProbationSPHSC1Row.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, ProbationSPHSC1Row.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(ProbationSPHSC1Row.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), ProbationSPHSC1Row.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal ProbationSPHSC1Row As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into ProbationSPHSC1")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, ApplyTime, ApplyID, ProbSeq, ProbDate, ProbDate2, DeptID, DeptName, OrganID,")
            strSQL.AppendLine("    OrganName, WorkTypeName, RankID, TitleName, EmpDate, ExtendFlag, Ability1, Ability2,")
            strSQL.AppendLine("    Attitude1, Attitude2, Attitude3, Character1, Character2, Character3, Comment, Attachment,")
            strSQL.AppendLine("    Result, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @ApplyTime, @ApplyID, @ProbSeq, @ProbDate, @ProbDate2, @DeptID, @DeptName, @OrganID,")
            strSQL.AppendLine("    @OrganName, @WorkTypeName, @RankID, @TitleName, @EmpDate, @ExtendFlag, @Ability1, @Ability2,")
            strSQL.AppendLine("    @Attitude1, @Attitude2, @Attitude3, @Character1, @Character2, @Character3, @Comment, @Attachment,")
            strSQL.AppendLine("    @Result, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In ProbationSPHSC1Row
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
                        db.AddInParameter(dbcmd, "@WorkTypeName", DbType.String, r.WorkTypeName.Value)
                        db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                        db.AddInParameter(dbcmd, "@TitleName", DbType.String, r.TitleName.Value)
                        db.AddInParameter(dbcmd, "@EmpDate", DbType.Date, IIf(IsDateTimeNull(r.EmpDate.Value), Convert.ToDateTime("1900/1/1"), r.EmpDate.Value))
                        db.AddInParameter(dbcmd, "@ExtendFlag", DbType.String, r.ExtendFlag.Value)
                        db.AddInParameter(dbcmd, "@Ability1", DbType.String, r.Ability1.Value)
                        db.AddInParameter(dbcmd, "@Ability2", DbType.String, r.Ability2.Value)
                        db.AddInParameter(dbcmd, "@Attitude1", DbType.String, r.Attitude1.Value)
                        db.AddInParameter(dbcmd, "@Attitude2", DbType.String, r.Attitude2.Value)
                        db.AddInParameter(dbcmd, "@Attitude3", DbType.String, r.Attitude3.Value)
                        db.AddInParameter(dbcmd, "@Character1", DbType.String, r.Character1.Value)
                        db.AddInParameter(dbcmd, "@Character2", DbType.String, r.Character2.Value)
                        db.AddInParameter(dbcmd, "@Character3", DbType.String, r.Character3.Value)
                        db.AddInParameter(dbcmd, "@Comment", DbType.String, r.Comment.Value)
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

        Public Function Insert(ByVal ProbationSPHSC1Row As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into ProbationSPHSC1")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, ApplyTime, ApplyID, ProbSeq, ProbDate, ProbDate2, DeptID, DeptName, OrganID,")
            strSQL.AppendLine("    OrganName, WorkTypeName, RankID, TitleName, EmpDate, ExtendFlag, Ability1, Ability2,")
            strSQL.AppendLine("    Attitude1, Attitude2, Attitude3, Character1, Character2, Character3, Comment, Attachment,")
            strSQL.AppendLine("    Result, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @ApplyTime, @ApplyID, @ProbSeq, @ProbDate, @ProbDate2, @DeptID, @DeptName, @OrganID,")
            strSQL.AppendLine("    @OrganName, @WorkTypeName, @RankID, @TitleName, @EmpDate, @ExtendFlag, @Ability1, @Ability2,")
            strSQL.AppendLine("    @Attitude1, @Attitude2, @Attitude3, @Character1, @Character2, @Character3, @Comment, @Attachment,")
            strSQL.AppendLine("    @Result, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In ProbationSPHSC1Row
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
                db.AddInParameter(dbcmd, "@WorkTypeName", DbType.String, r.WorkTypeName.Value)
                db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                db.AddInParameter(dbcmd, "@TitleName", DbType.String, r.TitleName.Value)
                db.AddInParameter(dbcmd, "@EmpDate", DbType.Date, IIf(IsDateTimeNull(r.EmpDate.Value), Convert.ToDateTime("1900/1/1"), r.EmpDate.Value))
                db.AddInParameter(dbcmd, "@ExtendFlag", DbType.String, r.ExtendFlag.Value)
                db.AddInParameter(dbcmd, "@Ability1", DbType.String, r.Ability1.Value)
                db.AddInParameter(dbcmd, "@Ability2", DbType.String, r.Ability2.Value)
                db.AddInParameter(dbcmd, "@Attitude1", DbType.String, r.Attitude1.Value)
                db.AddInParameter(dbcmd, "@Attitude2", DbType.String, r.Attitude2.Value)
                db.AddInParameter(dbcmd, "@Attitude3", DbType.String, r.Attitude3.Value)
                db.AddInParameter(dbcmd, "@Character1", DbType.String, r.Character1.Value)
                db.AddInParameter(dbcmd, "@Character2", DbType.String, r.Character2.Value)
                db.AddInParameter(dbcmd, "@Character3", DbType.String, r.Character3.Value)
                db.AddInParameter(dbcmd, "@Comment", DbType.String, r.Comment.Value)
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

