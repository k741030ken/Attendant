'****************************************************************
' Table:Education
' Created Date: 2015.05.28
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beEducation
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "IDNo", "EduID", "Seq", "GraduateYear", "SchoolType", "SchoolID", "DepartID", "SecDepartID", "School", "Depart" _
                                    , "SecDepart", "EduStatus", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(Integer), GetType(Integer), GetType(Integer), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "IDNo", "EduID", "Seq" }

        Public ReadOnly Property Rows() As beEducation.Rows 
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
        Public Sub Transfer2Row(EducationTable As DataTable)
            For Each dr As DataRow In EducationTable.Rows
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

                dr(m_Rows(i).IDNo.FieldName) = m_Rows(i).IDNo.Value
                dr(m_Rows(i).EduID.FieldName) = m_Rows(i).EduID.Value
                dr(m_Rows(i).Seq.FieldName) = m_Rows(i).Seq.Value
                dr(m_Rows(i).GraduateYear.FieldName) = m_Rows(i).GraduateYear.Value
                dr(m_Rows(i).SchoolType.FieldName) = m_Rows(i).SchoolType.Value
                dr(m_Rows(i).SchoolID.FieldName) = m_Rows(i).SchoolID.Value
                dr(m_Rows(i).DepartID.FieldName) = m_Rows(i).DepartID.Value
                dr(m_Rows(i).SecDepartID.FieldName) = m_Rows(i).SecDepartID.Value
                dr(m_Rows(i).School.FieldName) = m_Rows(i).School.Value
                dr(m_Rows(i).Depart.FieldName) = m_Rows(i).Depart.Value
                dr(m_Rows(i).SecDepart.FieldName) = m_Rows(i).SecDepart.Value
                dr(m_Rows(i).EduStatus.FieldName) = m_Rows(i).EduStatus.Value
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

        Public Sub Add(EducationRow As Row)
            m_Rows.Add(EducationRow)
        End Sub

        Public Sub Remove(EducationRow As Row)
            If m_Rows.IndexOf(EducationRow) >= 0 Then
                m_Rows.Remove(EducationRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_IDNo As Field(Of String) = new Field(Of String)("IDNo", true)
        Private FI_EduID As Field(Of String) = new Field(Of String)("EduID", true)
        Private FI_Seq As Field(Of Integer) = new Field(Of Integer)("Seq", true)
        Private FI_GraduateYear As Field(Of Integer) = new Field(Of Integer)("GraduateYear", true)
        Private FI_SchoolType As Field(Of Integer) = new Field(Of Integer)("SchoolType", true)
        Private FI_SchoolID As Field(Of String) = new Field(Of String)("SchoolID", true)
        Private FI_DepartID As Field(Of String) = new Field(Of String)("DepartID", true)
        Private FI_SecDepartID As Field(Of String) = new Field(Of String)("SecDepartID", true)
        Private FI_School As Field(Of String) = new Field(Of String)("School", true)
        Private FI_Depart As Field(Of String) = new Field(Of String)("Depart", true)
        Private FI_SecDepart As Field(Of String) = new Field(Of String)("SecDepart", true)
        Private FI_EduStatus As Field(Of String) = new Field(Of String)("EduStatus", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "IDNo", "EduID", "Seq", "GraduateYear", "SchoolType", "SchoolID", "DepartID", "SecDepartID", "School", "Depart" _
                                    , "SecDepart", "EduStatus", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "IDNo", "EduID", "Seq" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "IDNo"
                    Return FI_IDNo.Value
                Case "EduID"
                    Return FI_EduID.Value
                Case "Seq"
                    Return FI_Seq.Value
                Case "GraduateYear"
                    Return FI_GraduateYear.Value
                Case "SchoolType"
                    Return FI_SchoolType.Value
                Case "SchoolID"
                    Return FI_SchoolID.Value
                Case "DepartID"
                    Return FI_DepartID.Value
                Case "SecDepartID"
                    Return FI_SecDepartID.Value
                Case "School"
                    Return FI_School.Value
                Case "Depart"
                    Return FI_Depart.Value
                Case "SecDepart"
                    Return FI_SecDepart.Value
                Case "EduStatus"
                    Return FI_EduStatus.Value
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
                Case "IDNo"
                    FI_IDNo.SetValue(value)
                Case "EduID"
                    FI_EduID.SetValue(value)
                Case "Seq"
                    FI_Seq.SetValue(value)
                Case "GraduateYear"
                    FI_GraduateYear.SetValue(value)
                Case "SchoolType"
                    FI_SchoolType.SetValue(value)
                Case "SchoolID"
                    FI_SchoolID.SetValue(value)
                Case "DepartID"
                    FI_DepartID.SetValue(value)
                Case "SecDepartID"
                    FI_SecDepartID.SetValue(value)
                Case "School"
                    FI_School.SetValue(value)
                Case "Depart"
                    FI_Depart.SetValue(value)
                Case "SecDepart"
                    FI_SecDepart.SetValue(value)
                Case "EduStatus"
                    FI_EduStatus.SetValue(value)
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
                Case "IDNo"
                    return FI_IDNo.Updated
                Case "EduID"
                    return FI_EduID.Updated
                Case "Seq"
                    return FI_Seq.Updated
                Case "GraduateYear"
                    return FI_GraduateYear.Updated
                Case "SchoolType"
                    return FI_SchoolType.Updated
                Case "SchoolID"
                    return FI_SchoolID.Updated
                Case "DepartID"
                    return FI_DepartID.Updated
                Case "SecDepartID"
                    return FI_SecDepartID.Updated
                Case "School"
                    return FI_School.Updated
                Case "Depart"
                    return FI_Depart.Updated
                Case "SecDepart"
                    return FI_SecDepart.Updated
                Case "EduStatus"
                    return FI_EduStatus.Updated
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
                Case "IDNo"
                    return FI_IDNo.CreateUpdateSQL
                Case "EduID"
                    return FI_EduID.CreateUpdateSQL
                Case "Seq"
                    return FI_Seq.CreateUpdateSQL
                Case "GraduateYear"
                    return FI_GraduateYear.CreateUpdateSQL
                Case "SchoolType"
                    return FI_SchoolType.CreateUpdateSQL
                Case "SchoolID"
                    return FI_SchoolID.CreateUpdateSQL
                Case "DepartID"
                    return FI_DepartID.CreateUpdateSQL
                Case "SecDepartID"
                    return FI_SecDepartID.CreateUpdateSQL
                Case "School"
                    return FI_School.CreateUpdateSQL
                Case "Depart"
                    return FI_Depart.CreateUpdateSQL
                Case "SecDepart"
                    return FI_SecDepart.CreateUpdateSQL
                Case "EduStatus"
                    return FI_EduStatus.CreateUpdateSQL
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
            FI_IDNo.SetInitValue("")
            FI_EduID.SetInitValue(" ")
            FI_Seq.SetInitValue(0)
            FI_GraduateYear.SetInitValue(0)
            FI_SchoolType.SetInitValue(0)
            FI_SchoolID.SetInitValue("")
            FI_DepartID.SetInitValue("")
            FI_SecDepartID.SetInitValue("")
            FI_School.SetInitValue("")
            FI_Depart.SetInitValue("")
            FI_SecDepart.SetInitValue("")
            FI_EduStatus.SetInitValue("1")
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_IDNo.SetInitValue(dr("IDNo"))
            FI_EduID.SetInitValue(dr("EduID"))
            FI_Seq.SetInitValue(dr("Seq"))
            FI_GraduateYear.SetInitValue(dr("GraduateYear"))
            FI_SchoolType.SetInitValue(dr("SchoolType"))
            FI_SchoolID.SetInitValue(dr("SchoolID"))
            FI_DepartID.SetInitValue(dr("DepartID"))
            FI_SecDepartID.SetInitValue(dr("SecDepartID"))
            FI_School.SetInitValue(dr("School"))
            FI_Depart.SetInitValue(dr("Depart"))
            FI_SecDepart.SetInitValue(dr("SecDepart"))
            FI_EduStatus.SetInitValue(dr("EduStatus"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_IDNo.Updated = False
            FI_EduID.Updated = False
            FI_Seq.Updated = False
            FI_GraduateYear.Updated = False
            FI_SchoolType.Updated = False
            FI_SchoolID.Updated = False
            FI_DepartID.Updated = False
            FI_SecDepartID.Updated = False
            FI_School.Updated = False
            FI_Depart.Updated = False
            FI_SecDepart.Updated = False
            FI_EduStatus.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
        End Sub

        Public ReadOnly Property IDNo As Field(Of String) 
            Get
                Return FI_IDNo
            End Get
        End Property

        Public ReadOnly Property EduID As Field(Of String) 
            Get
                Return FI_EduID
            End Get
        End Property

        Public ReadOnly Property Seq As Field(Of Integer) 
            Get
                Return FI_Seq
            End Get
        End Property

        Public ReadOnly Property GraduateYear As Field(Of Integer) 
            Get
                Return FI_GraduateYear
            End Get
        End Property

        Public ReadOnly Property SchoolType As Field(Of Integer) 
            Get
                Return FI_SchoolType
            End Get
        End Property

        Public ReadOnly Property SchoolID As Field(Of String) 
            Get
                Return FI_SchoolID
            End Get
        End Property

        Public ReadOnly Property DepartID As Field(Of String) 
            Get
                Return FI_DepartID
            End Get
        End Property

        Public ReadOnly Property SecDepartID As Field(Of String) 
            Get
                Return FI_SecDepartID
            End Get
        End Property

        Public ReadOnly Property School As Field(Of String) 
            Get
                Return FI_School
            End Get
        End Property

        Public ReadOnly Property Depart As Field(Of String) 
            Get
                Return FI_Depart
            End Get
        End Property

        Public ReadOnly Property SecDepart As Field(Of String) 
            Get
                Return FI_SecDepart
            End Get
        End Property

        Public ReadOnly Property EduStatus As Field(Of String) 
            Get
                Return FI_EduStatus
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
        Public Function DeleteRowByPrimaryKey(ByVal EducationRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From Education")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And EduID = @EduID")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, EducationRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@EduID", DbType.String, EducationRow.EduID.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EducationRow.Seq.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal EducationRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From Education")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And EduID = @EduID")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, EducationRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@EduID", DbType.String, EducationRow.EduID.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EducationRow.Seq.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal EducationRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From Education")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And EduID = @EduID")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In EducationRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                        db.AddInParameter(dbcmd, "@EduID", DbType.String, r.EduID.Value)
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

        Public Function DeleteRowByPrimaryKey(ByVal EducationRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From Education")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And EduID = @EduID")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In EducationRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                db.AddInParameter(dbcmd, "@EduID", DbType.String, r.EduID.Value)
                db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal EducationRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Education")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And EduID = @EduID")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, EducationRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@EduID", DbType.String, EducationRow.EduID.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EducationRow.Seq.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(EducationRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Education")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And EduID = @EduID")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, EducationRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@EduID", DbType.String, EducationRow.EduID.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EducationRow.Seq.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal EducationRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update Education Set")
            For i As Integer = 0 To EducationRow.FieldNames.Length - 1
                If Not EducationRow.IsIdentityField(EducationRow.FieldNames(i)) AndAlso EducationRow.IsUpdated(EducationRow.FieldNames(i)) AndAlso EducationRow.CreateUpdateSQL(EducationRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, EducationRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where IDNo = @PKIDNo")
            strSQL.AppendLine("And EduID = @PKEduID")
            strSQL.AppendLine("And Seq = @PKSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If EducationRow.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, EducationRow.IDNo.Value)
            If EducationRow.EduID.Updated Then db.AddInParameter(dbcmd, "@EduID", DbType.String, EducationRow.EduID.Value)
            If EducationRow.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EducationRow.Seq.Value)
            If EducationRow.GraduateYear.Updated Then db.AddInParameter(dbcmd, "@GraduateYear", DbType.Int32, EducationRow.GraduateYear.Value)
            If EducationRow.SchoolType.Updated Then db.AddInParameter(dbcmd, "@SchoolType", DbType.Int32, EducationRow.SchoolType.Value)
            If EducationRow.SchoolID.Updated Then db.AddInParameter(dbcmd, "@SchoolID", DbType.String, EducationRow.SchoolID.Value)
            If EducationRow.DepartID.Updated Then db.AddInParameter(dbcmd, "@DepartID", DbType.String, EducationRow.DepartID.Value)
            If EducationRow.SecDepartID.Updated Then db.AddInParameter(dbcmd, "@SecDepartID", DbType.String, EducationRow.SecDepartID.Value)
            If EducationRow.School.Updated Then db.AddInParameter(dbcmd, "@School", DbType.String, EducationRow.School.Value)
            If EducationRow.Depart.Updated Then db.AddInParameter(dbcmd, "@Depart", DbType.String, EducationRow.Depart.Value)
            If EducationRow.SecDepart.Updated Then db.AddInParameter(dbcmd, "@SecDepart", DbType.String, EducationRow.SecDepart.Value)
            If EducationRow.EduStatus.Updated Then db.AddInParameter(dbcmd, "@EduStatus", DbType.String, EducationRow.EduStatus.Value)
            If EducationRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EducationRow.LastChgComp.Value)
            If EducationRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EducationRow.LastChgID.Value)
            If EducationRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EducationRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EducationRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKIDNo", DbType.String, IIf(EducationRow.LoadFromDataRow, EducationRow.IDNo.OldValue, EducationRow.IDNo.Value))
            db.AddInParameter(dbcmd, "@PKEduID", DbType.String, IIf(EducationRow.LoadFromDataRow, EducationRow.EduID.OldValue, EducationRow.EduID.Value))
            db.AddInParameter(dbcmd, "@PKSeq", DbType.Int32, IIf(EducationRow.LoadFromDataRow, EducationRow.Seq.OldValue, EducationRow.Seq.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal EducationRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update Education Set")
            For i As Integer = 0 To EducationRow.FieldNames.Length - 1
                If Not EducationRow.IsIdentityField(EducationRow.FieldNames(i)) AndAlso EducationRow.IsUpdated(EducationRow.FieldNames(i)) AndAlso EducationRow.CreateUpdateSQL(EducationRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, EducationRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where IDNo = @PKIDNo")
            strSQL.AppendLine("And EduID = @PKEduID")
            strSQL.AppendLine("And Seq = @PKSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If EducationRow.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, EducationRow.IDNo.Value)
            If EducationRow.EduID.Updated Then db.AddInParameter(dbcmd, "@EduID", DbType.String, EducationRow.EduID.Value)
            If EducationRow.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EducationRow.Seq.Value)
            If EducationRow.GraduateYear.Updated Then db.AddInParameter(dbcmd, "@GraduateYear", DbType.Int32, EducationRow.GraduateYear.Value)
            If EducationRow.SchoolType.Updated Then db.AddInParameter(dbcmd, "@SchoolType", DbType.Int32, EducationRow.SchoolType.Value)
            If EducationRow.SchoolID.Updated Then db.AddInParameter(dbcmd, "@SchoolID", DbType.String, EducationRow.SchoolID.Value)
            If EducationRow.DepartID.Updated Then db.AddInParameter(dbcmd, "@DepartID", DbType.String, EducationRow.DepartID.Value)
            If EducationRow.SecDepartID.Updated Then db.AddInParameter(dbcmd, "@SecDepartID", DbType.String, EducationRow.SecDepartID.Value)
            If EducationRow.School.Updated Then db.AddInParameter(dbcmd, "@School", DbType.String, EducationRow.School.Value)
            If EducationRow.Depart.Updated Then db.AddInParameter(dbcmd, "@Depart", DbType.String, EducationRow.Depart.Value)
            If EducationRow.SecDepart.Updated Then db.AddInParameter(dbcmd, "@SecDepart", DbType.String, EducationRow.SecDepart.Value)
            If EducationRow.EduStatus.Updated Then db.AddInParameter(dbcmd, "@EduStatus", DbType.String, EducationRow.EduStatus.Value)
            If EducationRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EducationRow.LastChgComp.Value)
            If EducationRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EducationRow.LastChgID.Value)
            If EducationRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EducationRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EducationRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKIDNo", DbType.String, IIf(EducationRow.LoadFromDataRow, EducationRow.IDNo.OldValue, EducationRow.IDNo.Value))
            db.AddInParameter(dbcmd, "@PKEduID", DbType.String, IIf(EducationRow.LoadFromDataRow, EducationRow.EduID.OldValue, EducationRow.EduID.Value))
            db.AddInParameter(dbcmd, "@PKSeq", DbType.Int32, IIf(EducationRow.LoadFromDataRow, EducationRow.Seq.OldValue, EducationRow.Seq.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal EducationRow As Row()) As Integer
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
                    For Each r As Row In EducationRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update Education Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where IDNo = @PKIDNo")
                        strSQL.AppendLine("And EduID = @PKEduID")
                        strSQL.AppendLine("And Seq = @PKSeq")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                        If r.EduID.Updated Then db.AddInParameter(dbcmd, "@EduID", DbType.String, r.EduID.Value)
                        If r.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                        If r.GraduateYear.Updated Then db.AddInParameter(dbcmd, "@GraduateYear", DbType.Int32, r.GraduateYear.Value)
                        If r.SchoolType.Updated Then db.AddInParameter(dbcmd, "@SchoolType", DbType.Int32, r.SchoolType.Value)
                        If r.SchoolID.Updated Then db.AddInParameter(dbcmd, "@SchoolID", DbType.String, r.SchoolID.Value)
                        If r.DepartID.Updated Then db.AddInParameter(dbcmd, "@DepartID", DbType.String, r.DepartID.Value)
                        If r.SecDepartID.Updated Then db.AddInParameter(dbcmd, "@SecDepartID", DbType.String, r.SecDepartID.Value)
                        If r.School.Updated Then db.AddInParameter(dbcmd, "@School", DbType.String, r.School.Value)
                        If r.Depart.Updated Then db.AddInParameter(dbcmd, "@Depart", DbType.String, r.Depart.Value)
                        If r.SecDepart.Updated Then db.AddInParameter(dbcmd, "@SecDepart", DbType.String, r.SecDepart.Value)
                        If r.EduStatus.Updated Then db.AddInParameter(dbcmd, "@EduStatus", DbType.String, r.EduStatus.Value)
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKIDNo", DbType.String, IIf(r.LoadFromDataRow, r.IDNo.OldValue, r.IDNo.Value))
                        db.AddInParameter(dbcmd, "@PKEduID", DbType.String, IIf(r.LoadFromDataRow, r.EduID.OldValue, r.EduID.Value))
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

        Public Function Update(ByVal EducationRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In EducationRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update Education Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where IDNo = @PKIDNo")
                strSQL.AppendLine("And EduID = @PKEduID")
                strSQL.AppendLine("And Seq = @PKSeq")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                If r.EduID.Updated Then db.AddInParameter(dbcmd, "@EduID", DbType.String, r.EduID.Value)
                If r.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                If r.GraduateYear.Updated Then db.AddInParameter(dbcmd, "@GraduateYear", DbType.Int32, r.GraduateYear.Value)
                If r.SchoolType.Updated Then db.AddInParameter(dbcmd, "@SchoolType", DbType.Int32, r.SchoolType.Value)
                If r.SchoolID.Updated Then db.AddInParameter(dbcmd, "@SchoolID", DbType.String, r.SchoolID.Value)
                If r.DepartID.Updated Then db.AddInParameter(dbcmd, "@DepartID", DbType.String, r.DepartID.Value)
                If r.SecDepartID.Updated Then db.AddInParameter(dbcmd, "@SecDepartID", DbType.String, r.SecDepartID.Value)
                If r.School.Updated Then db.AddInParameter(dbcmd, "@School", DbType.String, r.School.Value)
                If r.Depart.Updated Then db.AddInParameter(dbcmd, "@Depart", DbType.String, r.Depart.Value)
                If r.SecDepart.Updated Then db.AddInParameter(dbcmd, "@SecDepart", DbType.String, r.SecDepart.Value)
                If r.EduStatus.Updated Then db.AddInParameter(dbcmd, "@EduStatus", DbType.String, r.EduStatus.Value)
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKIDNo", DbType.String, IIf(r.LoadFromDataRow, r.IDNo.OldValue, r.IDNo.Value))
                db.AddInParameter(dbcmd, "@PKEduID", DbType.String, IIf(r.LoadFromDataRow, r.EduID.OldValue, r.EduID.Value))
                db.AddInParameter(dbcmd, "@PKSeq", DbType.Int32, IIf(r.LoadFromDataRow, r.Seq.OldValue, r.Seq.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal EducationRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From Education")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And EduID = @EduID")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, EducationRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@EduID", DbType.String, EducationRow.EduID.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EducationRow.Seq.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal EducationRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From Education")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And EduID = @EduID")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, EducationRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@EduID", DbType.String, EducationRow.EduID.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EducationRow.Seq.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Education")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal EducationRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into Education")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    IDNo, EduID, Seq, GraduateYear, SchoolType, SchoolID, DepartID, SecDepartID, School,")
            strSQL.AppendLine("    Depart, SecDepart, EduStatus, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @IDNo, @EduID, @Seq, @GraduateYear, @SchoolType, @SchoolID, @DepartID, @SecDepartID, @School,")
            strSQL.AppendLine("    @Depart, @SecDepart, @EduStatus, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, EducationRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@EduID", DbType.String, EducationRow.EduID.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EducationRow.Seq.Value)
            db.AddInParameter(dbcmd, "@GraduateYear", DbType.Int32, EducationRow.GraduateYear.Value)
            db.AddInParameter(dbcmd, "@SchoolType", DbType.Int32, EducationRow.SchoolType.Value)
            db.AddInParameter(dbcmd, "@SchoolID", DbType.String, EducationRow.SchoolID.Value)
            db.AddInParameter(dbcmd, "@DepartID", DbType.String, EducationRow.DepartID.Value)
            db.AddInParameter(dbcmd, "@SecDepartID", DbType.String, EducationRow.SecDepartID.Value)
            db.AddInParameter(dbcmd, "@School", DbType.String, EducationRow.School.Value)
            db.AddInParameter(dbcmd, "@Depart", DbType.String, EducationRow.Depart.Value)
            db.AddInParameter(dbcmd, "@SecDepart", DbType.String, EducationRow.SecDepart.Value)
            db.AddInParameter(dbcmd, "@EduStatus", DbType.String, EducationRow.EduStatus.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EducationRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EducationRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EducationRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EducationRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal EducationRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into Education")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    IDNo, EduID, Seq, GraduateYear, SchoolType, SchoolID, DepartID, SecDepartID, School,")
            strSQL.AppendLine("    Depart, SecDepart, EduStatus, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @IDNo, @EduID, @Seq, @GraduateYear, @SchoolType, @SchoolID, @DepartID, @SecDepartID, @School,")
            strSQL.AppendLine("    @Depart, @SecDepart, @EduStatus, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, EducationRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@EduID", DbType.String, EducationRow.EduID.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EducationRow.Seq.Value)
            db.AddInParameter(dbcmd, "@GraduateYear", DbType.Int32, EducationRow.GraduateYear.Value)
            db.AddInParameter(dbcmd, "@SchoolType", DbType.Int32, EducationRow.SchoolType.Value)
            db.AddInParameter(dbcmd, "@SchoolID", DbType.String, EducationRow.SchoolID.Value)
            db.AddInParameter(dbcmd, "@DepartID", DbType.String, EducationRow.DepartID.Value)
            db.AddInParameter(dbcmd, "@SecDepartID", DbType.String, EducationRow.SecDepartID.Value)
            db.AddInParameter(dbcmd, "@School", DbType.String, EducationRow.School.Value)
            db.AddInParameter(dbcmd, "@Depart", DbType.String, EducationRow.Depart.Value)
            db.AddInParameter(dbcmd, "@SecDepart", DbType.String, EducationRow.SecDepart.Value)
            db.AddInParameter(dbcmd, "@EduStatus", DbType.String, EducationRow.EduStatus.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EducationRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EducationRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EducationRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EducationRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal EducationRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into Education")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    IDNo, EduID, Seq, GraduateYear, SchoolType, SchoolID, DepartID, SecDepartID, School,")
            strSQL.AppendLine("    Depart, SecDepart, EduStatus, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @IDNo, @EduID, @Seq, @GraduateYear, @SchoolType, @SchoolID, @DepartID, @SecDepartID, @School,")
            strSQL.AppendLine("    @Depart, @SecDepart, @EduStatus, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In EducationRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                        db.AddInParameter(dbcmd, "@EduID", DbType.String, r.EduID.Value)
                        db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                        db.AddInParameter(dbcmd, "@GraduateYear", DbType.Int32, r.GraduateYear.Value)
                        db.AddInParameter(dbcmd, "@SchoolType", DbType.Int32, r.SchoolType.Value)
                        db.AddInParameter(dbcmd, "@SchoolID", DbType.String, r.SchoolID.Value)
                        db.AddInParameter(dbcmd, "@DepartID", DbType.String, r.DepartID.Value)
                        db.AddInParameter(dbcmd, "@SecDepartID", DbType.String, r.SecDepartID.Value)
                        db.AddInParameter(dbcmd, "@School", DbType.String, r.School.Value)
                        db.AddInParameter(dbcmd, "@Depart", DbType.String, r.Depart.Value)
                        db.AddInParameter(dbcmd, "@SecDepart", DbType.String, r.SecDepart.Value)
                        db.AddInParameter(dbcmd, "@EduStatus", DbType.String, r.EduStatus.Value)
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

        Public Function Insert(ByVal EducationRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into Education")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    IDNo, EduID, Seq, GraduateYear, SchoolType, SchoolID, DepartID, SecDepartID, School,")
            strSQL.AppendLine("    Depart, SecDepart, EduStatus, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @IDNo, @EduID, @Seq, @GraduateYear, @SchoolType, @SchoolID, @DepartID, @SecDepartID, @School,")
            strSQL.AppendLine("    @Depart, @SecDepart, @EduStatus, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In EducationRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                db.AddInParameter(dbcmd, "@EduID", DbType.String, r.EduID.Value)
                db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                db.AddInParameter(dbcmd, "@GraduateYear", DbType.Int32, r.GraduateYear.Value)
                db.AddInParameter(dbcmd, "@SchoolType", DbType.Int32, r.SchoolType.Value)
                db.AddInParameter(dbcmd, "@SchoolID", DbType.String, r.SchoolID.Value)
                db.AddInParameter(dbcmd, "@DepartID", DbType.String, r.DepartID.Value)
                db.AddInParameter(dbcmd, "@SecDepartID", DbType.String, r.SecDepartID.Value)
                db.AddInParameter(dbcmd, "@School", DbType.String, r.School.Value)
                db.AddInParameter(dbcmd, "@Depart", DbType.String, r.Depart.Value)
                db.AddInParameter(dbcmd, "@SecDepart", DbType.String, r.SecDepart.Value)
                db.AddInParameter(dbcmd, "@EduStatus", DbType.String, r.EduStatus.Value)
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

