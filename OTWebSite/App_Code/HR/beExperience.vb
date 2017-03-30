'****************************************************************
' Table:Experience
' Created Date: 2015.06.11
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beExperience
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "IDNo", "Seq", "BeginDate", "EndDate", "IndustryType", "Company", "Department", "Title", "WorkType", "WorkYear" _
                                    , "Profession", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(Integer), GetType(Date), GetType(Date), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Decimal) _
                                    , GetType(String), GetType(String), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "IDNo", "Seq" }

        Public ReadOnly Property Rows() As beExperience.Rows 
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
        Public Sub Transfer2Row(ExperienceTable As DataTable)
            For Each dr As DataRow In ExperienceTable.Rows
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
                dr(m_Rows(i).Seq.FieldName) = m_Rows(i).Seq.Value
                dr(m_Rows(i).BeginDate.FieldName) = m_Rows(i).BeginDate.Value
                dr(m_Rows(i).EndDate.FieldName) = m_Rows(i).EndDate.Value
                dr(m_Rows(i).IndustryType.FieldName) = m_Rows(i).IndustryType.Value
                dr(m_Rows(i).Company.FieldName) = m_Rows(i).Company.Value
                dr(m_Rows(i).Department.FieldName) = m_Rows(i).Department.Value
                dr(m_Rows(i).Title.FieldName) = m_Rows(i).Title.Value
                dr(m_Rows(i).WorkType.FieldName) = m_Rows(i).WorkType.Value
                dr(m_Rows(i).WorkYear.FieldName) = m_Rows(i).WorkYear.Value
                dr(m_Rows(i).Profession.FieldName) = m_Rows(i).Profession.Value
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

        Public Sub Add(ExperienceRow As Row)
            m_Rows.Add(ExperienceRow)
        End Sub

        Public Sub Remove(ExperienceRow As Row)
            If m_Rows.IndexOf(ExperienceRow) >= 0 Then
                m_Rows.Remove(ExperienceRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_IDNo As Field(Of String) = new Field(Of String)("IDNo", true)
        Private FI_Seq As Field(Of Integer) = new Field(Of Integer)("Seq", true)
        Private FI_BeginDate As Field(Of Date) = new Field(Of Date)("BeginDate", true)
        Private FI_EndDate As Field(Of Date) = new Field(Of Date)("EndDate", true)
        Private FI_IndustryType As Field(Of String) = new Field(Of String)("IndustryType", true)
        Private FI_Company As Field(Of String) = new Field(Of String)("Company", true)
        Private FI_Department As Field(Of String) = new Field(Of String)("Department", true)
        Private FI_Title As Field(Of String) = new Field(Of String)("Title", true)
        Private FI_WorkType As Field(Of String) = new Field(Of String)("WorkType", true)
        Private FI_WorkYear As Field(Of Decimal) = new Field(Of Decimal)("WorkYear", true)
        Private FI_Profession As Field(Of String) = new Field(Of String)("Profession", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "IDNo", "Seq", "BeginDate", "EndDate", "IndustryType", "Company", "Department", "Title", "WorkType", "WorkYear" _
                                    , "Profession", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "IDNo", "Seq" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "IDNo"
                    Return FI_IDNo.Value
                Case "Seq"
                    Return FI_Seq.Value
                Case "BeginDate"
                    Return FI_BeginDate.Value
                Case "EndDate"
                    Return FI_EndDate.Value
                Case "IndustryType"
                    Return FI_IndustryType.Value
                Case "Company"
                    Return FI_Company.Value
                Case "Department"
                    Return FI_Department.Value
                Case "Title"
                    Return FI_Title.Value
                Case "WorkType"
                    Return FI_WorkType.Value
                Case "WorkYear"
                    Return FI_WorkYear.Value
                Case "Profession"
                    Return FI_Profession.Value
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
                Case "Seq"
                    FI_Seq.SetValue(value)
                Case "BeginDate"
                    FI_BeginDate.SetValue(value)
                Case "EndDate"
                    FI_EndDate.SetValue(value)
                Case "IndustryType"
                    FI_IndustryType.SetValue(value)
                Case "Company"
                    FI_Company.SetValue(value)
                Case "Department"
                    FI_Department.SetValue(value)
                Case "Title"
                    FI_Title.SetValue(value)
                Case "WorkType"
                    FI_WorkType.SetValue(value)
                Case "WorkYear"
                    FI_WorkYear.SetValue(value)
                Case "Profession"
                    FI_Profession.SetValue(value)
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
                Case "Seq"
                    return FI_Seq.Updated
                Case "BeginDate"
                    return FI_BeginDate.Updated
                Case "EndDate"
                    return FI_EndDate.Updated
                Case "IndustryType"
                    return FI_IndustryType.Updated
                Case "Company"
                    return FI_Company.Updated
                Case "Department"
                    return FI_Department.Updated
                Case "Title"
                    return FI_Title.Updated
                Case "WorkType"
                    return FI_WorkType.Updated
                Case "WorkYear"
                    return FI_WorkYear.Updated
                Case "Profession"
                    return FI_Profession.Updated
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
                Case "Seq"
                    return FI_Seq.CreateUpdateSQL
                Case "BeginDate"
                    return FI_BeginDate.CreateUpdateSQL
                Case "EndDate"
                    return FI_EndDate.CreateUpdateSQL
                Case "IndustryType"
                    return FI_IndustryType.CreateUpdateSQL
                Case "Company"
                    return FI_Company.CreateUpdateSQL
                Case "Department"
                    return FI_Department.CreateUpdateSQL
                Case "Title"
                    return FI_Title.CreateUpdateSQL
                Case "WorkType"
                    return FI_WorkType.CreateUpdateSQL
                Case "WorkYear"
                    return FI_WorkYear.CreateUpdateSQL
                Case "Profession"
                    return FI_Profession.CreateUpdateSQL
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
            FI_Seq.SetInitValue(0)
            FI_BeginDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_EndDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_IndustryType.SetInitValue("")
            FI_Company.SetInitValue("")
            FI_Department.SetInitValue("")
            FI_Title.SetInitValue("")
            FI_WorkType.SetInitValue("")
            FI_WorkYear.SetInitValue(0)
            FI_Profession.SetInitValue("")
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_IDNo.SetInitValue(dr("IDNo"))
            FI_Seq.SetInitValue(dr("Seq"))
            FI_BeginDate.SetInitValue(dr("BeginDate"))
            FI_EndDate.SetInitValue(dr("EndDate"))
            FI_IndustryType.SetInitValue(dr("IndustryType"))
            FI_Company.SetInitValue(dr("Company"))
            FI_Department.SetInitValue(dr("Department"))
            FI_Title.SetInitValue(dr("Title"))
            FI_WorkType.SetInitValue(dr("WorkType"))
            FI_WorkYear.SetInitValue(dr("WorkYear"))
            FI_Profession.SetInitValue(dr("Profession"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_IDNo.Updated = False
            FI_Seq.Updated = False
            FI_BeginDate.Updated = False
            FI_EndDate.Updated = False
            FI_IndustryType.Updated = False
            FI_Company.Updated = False
            FI_Department.Updated = False
            FI_Title.Updated = False
            FI_WorkType.Updated = False
            FI_WorkYear.Updated = False
            FI_Profession.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
        End Sub

        Public ReadOnly Property IDNo As Field(Of String) 
            Get
                Return FI_IDNo
            End Get
        End Property

        Public ReadOnly Property Seq As Field(Of Integer) 
            Get
                Return FI_Seq
            End Get
        End Property

        Public ReadOnly Property BeginDate As Field(Of Date) 
            Get
                Return FI_BeginDate
            End Get
        End Property

        Public ReadOnly Property EndDate As Field(Of Date) 
            Get
                Return FI_EndDate
            End Get
        End Property

        Public ReadOnly Property IndustryType As Field(Of String) 
            Get
                Return FI_IndustryType
            End Get
        End Property

        Public ReadOnly Property Company As Field(Of String) 
            Get
                Return FI_Company
            End Get
        End Property

        Public ReadOnly Property Department As Field(Of String) 
            Get
                Return FI_Department
            End Get
        End Property

        Public ReadOnly Property Title As Field(Of String) 
            Get
                Return FI_Title
            End Get
        End Property

        Public ReadOnly Property WorkType As Field(Of String) 
            Get
                Return FI_WorkType
            End Get
        End Property

        Public ReadOnly Property WorkYear As Field(Of Decimal) 
            Get
                Return FI_WorkYear
            End Get
        End Property

        Public ReadOnly Property Profession As Field(Of String) 
            Get
                Return FI_Profession
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
        Public Function DeleteRowByPrimaryKey(ByVal ExperienceRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From Experience")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, ExperienceRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, ExperienceRow.Seq.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal ExperienceRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From Experience")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, ExperienceRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, ExperienceRow.Seq.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal ExperienceRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From Experience")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In ExperienceRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
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

        Public Function DeleteRowByPrimaryKey(ByVal ExperienceRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From Experience")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In ExperienceRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal ExperienceRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Experience")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, ExperienceRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, ExperienceRow.Seq.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(ExperienceRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Experience")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, ExperienceRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, ExperienceRow.Seq.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal ExperienceRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update Experience Set")
            For i As Integer = 0 To ExperienceRow.FieldNames.Length - 1
                If Not ExperienceRow.IsIdentityField(ExperienceRow.FieldNames(i)) AndAlso ExperienceRow.IsUpdated(ExperienceRow.FieldNames(i)) AndAlso ExperienceRow.CreateUpdateSQL(ExperienceRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, ExperienceRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where IDNo = @PKIDNo")
            strSQL.AppendLine("And Seq = @PKSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If ExperienceRow.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, ExperienceRow.IDNo.Value)
            If ExperienceRow.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, ExperienceRow.Seq.Value)
            If ExperienceRow.BeginDate.Updated Then db.AddInParameter(dbcmd, "@BeginDate", DbType.Date, IIf(IsDateTimeNull(ExperienceRow.BeginDate.Value), Convert.ToDateTime("1900/1/1"), ExperienceRow.BeginDate.Value))
            If ExperienceRow.EndDate.Updated Then db.AddInParameter(dbcmd, "@EndDate", DbType.Date, IIf(IsDateTimeNull(ExperienceRow.EndDate.Value), Convert.ToDateTime("1900/1/1"), ExperienceRow.EndDate.Value))
            If ExperienceRow.IndustryType.Updated Then db.AddInParameter(dbcmd, "@IndustryType", DbType.String, ExperienceRow.IndustryType.Value)
            If ExperienceRow.Company.Updated Then db.AddInParameter(dbcmd, "@Company", DbType.String, ExperienceRow.Company.Value)
            If ExperienceRow.Department.Updated Then db.AddInParameter(dbcmd, "@Department", DbType.String, ExperienceRow.Department.Value)
            If ExperienceRow.Title.Updated Then db.AddInParameter(dbcmd, "@Title", DbType.String, ExperienceRow.Title.Value)
            If ExperienceRow.WorkType.Updated Then db.AddInParameter(dbcmd, "@WorkType", DbType.String, ExperienceRow.WorkType.Value)
            If ExperienceRow.WorkYear.Updated Then db.AddInParameter(dbcmd, "@WorkYear", DbType.Decimal, ExperienceRow.WorkYear.Value)
            If ExperienceRow.Profession.Updated Then db.AddInParameter(dbcmd, "@Profession", DbType.String, ExperienceRow.Profession.Value)
            If ExperienceRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, ExperienceRow.LastChgComp.Value)
            If ExperienceRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, ExperienceRow.LastChgID.Value)
            If ExperienceRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(ExperienceRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), ExperienceRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKIDNo", DbType.String, IIf(ExperienceRow.LoadFromDataRow, ExperienceRow.IDNo.OldValue, ExperienceRow.IDNo.Value))
            db.AddInParameter(dbcmd, "@PKSeq", DbType.Int32, IIf(ExperienceRow.LoadFromDataRow, ExperienceRow.Seq.OldValue, ExperienceRow.Seq.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal ExperienceRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update Experience Set")
            For i As Integer = 0 To ExperienceRow.FieldNames.Length - 1
                If Not ExperienceRow.IsIdentityField(ExperienceRow.FieldNames(i)) AndAlso ExperienceRow.IsUpdated(ExperienceRow.FieldNames(i)) AndAlso ExperienceRow.CreateUpdateSQL(ExperienceRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, ExperienceRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where IDNo = @PKIDNo")
            strSQL.AppendLine("And Seq = @PKSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If ExperienceRow.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, ExperienceRow.IDNo.Value)
            If ExperienceRow.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, ExperienceRow.Seq.Value)
            If ExperienceRow.BeginDate.Updated Then db.AddInParameter(dbcmd, "@BeginDate", DbType.Date, IIf(IsDateTimeNull(ExperienceRow.BeginDate.Value), Convert.ToDateTime("1900/1/1"), ExperienceRow.BeginDate.Value))
            If ExperienceRow.EndDate.Updated Then db.AddInParameter(dbcmd, "@EndDate", DbType.Date, IIf(IsDateTimeNull(ExperienceRow.EndDate.Value), Convert.ToDateTime("1900/1/1"), ExperienceRow.EndDate.Value))
            If ExperienceRow.IndustryType.Updated Then db.AddInParameter(dbcmd, "@IndustryType", DbType.String, ExperienceRow.IndustryType.Value)
            If ExperienceRow.Company.Updated Then db.AddInParameter(dbcmd, "@Company", DbType.String, ExperienceRow.Company.Value)
            If ExperienceRow.Department.Updated Then db.AddInParameter(dbcmd, "@Department", DbType.String, ExperienceRow.Department.Value)
            If ExperienceRow.Title.Updated Then db.AddInParameter(dbcmd, "@Title", DbType.String, ExperienceRow.Title.Value)
            If ExperienceRow.WorkType.Updated Then db.AddInParameter(dbcmd, "@WorkType", DbType.String, ExperienceRow.WorkType.Value)
            If ExperienceRow.WorkYear.Updated Then db.AddInParameter(dbcmd, "@WorkYear", DbType.Decimal, ExperienceRow.WorkYear.Value)
            If ExperienceRow.Profession.Updated Then db.AddInParameter(dbcmd, "@Profession", DbType.String, ExperienceRow.Profession.Value)
            If ExperienceRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, ExperienceRow.LastChgComp.Value)
            If ExperienceRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, ExperienceRow.LastChgID.Value)
            If ExperienceRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(ExperienceRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), ExperienceRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKIDNo", DbType.String, IIf(ExperienceRow.LoadFromDataRow, ExperienceRow.IDNo.OldValue, ExperienceRow.IDNo.Value))
            db.AddInParameter(dbcmd, "@PKSeq", DbType.Int32, IIf(ExperienceRow.LoadFromDataRow, ExperienceRow.Seq.OldValue, ExperienceRow.Seq.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal ExperienceRow As Row()) As Integer
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
                    For Each r As Row In ExperienceRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update Experience Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where IDNo = @PKIDNo")
                        strSQL.AppendLine("And Seq = @PKSeq")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                        If r.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                        If r.BeginDate.Updated Then db.AddInParameter(dbcmd, "@BeginDate", DbType.Date, IIf(IsDateTimeNull(r.BeginDate.Value), Convert.ToDateTime("1900/1/1"), r.BeginDate.Value))
                        If r.EndDate.Updated Then db.AddInParameter(dbcmd, "@EndDate", DbType.Date, IIf(IsDateTimeNull(r.EndDate.Value), Convert.ToDateTime("1900/1/1"), r.EndDate.Value))
                        If r.IndustryType.Updated Then db.AddInParameter(dbcmd, "@IndustryType", DbType.String, r.IndustryType.Value)
                        If r.Company.Updated Then db.AddInParameter(dbcmd, "@Company", DbType.String, r.Company.Value)
                        If r.Department.Updated Then db.AddInParameter(dbcmd, "@Department", DbType.String, r.Department.Value)
                        If r.Title.Updated Then db.AddInParameter(dbcmd, "@Title", DbType.String, r.Title.Value)
                        If r.WorkType.Updated Then db.AddInParameter(dbcmd, "@WorkType", DbType.String, r.WorkType.Value)
                        If r.WorkYear.Updated Then db.AddInParameter(dbcmd, "@WorkYear", DbType.Decimal, r.WorkYear.Value)
                        If r.Profession.Updated Then db.AddInParameter(dbcmd, "@Profession", DbType.String, r.Profession.Value)
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKIDNo", DbType.String, IIf(r.LoadFromDataRow, r.IDNo.OldValue, r.IDNo.Value))
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

        Public Function Update(ByVal ExperienceRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In ExperienceRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update Experience Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where IDNo = @PKIDNo")
                strSQL.AppendLine("And Seq = @PKSeq")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                If r.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                If r.BeginDate.Updated Then db.AddInParameter(dbcmd, "@BeginDate", DbType.Date, IIf(IsDateTimeNull(r.BeginDate.Value), Convert.ToDateTime("1900/1/1"), r.BeginDate.Value))
                If r.EndDate.Updated Then db.AddInParameter(dbcmd, "@EndDate", DbType.Date, IIf(IsDateTimeNull(r.EndDate.Value), Convert.ToDateTime("1900/1/1"), r.EndDate.Value))
                If r.IndustryType.Updated Then db.AddInParameter(dbcmd, "@IndustryType", DbType.String, r.IndustryType.Value)
                If r.Company.Updated Then db.AddInParameter(dbcmd, "@Company", DbType.String, r.Company.Value)
                If r.Department.Updated Then db.AddInParameter(dbcmd, "@Department", DbType.String, r.Department.Value)
                If r.Title.Updated Then db.AddInParameter(dbcmd, "@Title", DbType.String, r.Title.Value)
                If r.WorkType.Updated Then db.AddInParameter(dbcmd, "@WorkType", DbType.String, r.WorkType.Value)
                If r.WorkYear.Updated Then db.AddInParameter(dbcmd, "@WorkYear", DbType.Decimal, r.WorkYear.Value)
                If r.Profession.Updated Then db.AddInParameter(dbcmd, "@Profession", DbType.String, r.Profession.Value)
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKIDNo", DbType.String, IIf(r.LoadFromDataRow, r.IDNo.OldValue, r.IDNo.Value))
                db.AddInParameter(dbcmd, "@PKSeq", DbType.Int32, IIf(r.LoadFromDataRow, r.Seq.OldValue, r.Seq.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal ExperienceRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From Experience")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, ExperienceRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, ExperienceRow.Seq.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal ExperienceRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From Experience")
            strSQL.AppendLine("Where IDNo = @IDNo")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, ExperienceRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, ExperienceRow.Seq.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Experience")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal ExperienceRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into Experience")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    IDNo, Seq, BeginDate, EndDate, IndustryType, Company, Department, Title, WorkType,")
            strSQL.AppendLine("    WorkYear, Profession, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @IDNo, @Seq, @BeginDate, @EndDate, @IndustryType, @Company, @Department, @Title, @WorkType,")
            strSQL.AppendLine("    @WorkYear, @Profession, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, ExperienceRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, ExperienceRow.Seq.Value)
            db.AddInParameter(dbcmd, "@BeginDate", DbType.Date, IIf(IsDateTimeNull(ExperienceRow.BeginDate.Value), Convert.ToDateTime("1900/1/1"), ExperienceRow.BeginDate.Value))
            db.AddInParameter(dbcmd, "@EndDate", DbType.Date, IIf(IsDateTimeNull(ExperienceRow.EndDate.Value), Convert.ToDateTime("1900/1/1"), ExperienceRow.EndDate.Value))
            db.AddInParameter(dbcmd, "@IndustryType", DbType.String, ExperienceRow.IndustryType.Value)
            db.AddInParameter(dbcmd, "@Company", DbType.String, ExperienceRow.Company.Value)
            db.AddInParameter(dbcmd, "@Department", DbType.String, ExperienceRow.Department.Value)
            db.AddInParameter(dbcmd, "@Title", DbType.String, ExperienceRow.Title.Value)
            db.AddInParameter(dbcmd, "@WorkType", DbType.String, ExperienceRow.WorkType.Value)
            db.AddInParameter(dbcmd, "@WorkYear", DbType.Decimal, ExperienceRow.WorkYear.Value)
            db.AddInParameter(dbcmd, "@Profession", DbType.String, ExperienceRow.Profession.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, ExperienceRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, ExperienceRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(ExperienceRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), ExperienceRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal ExperienceRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into Experience")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    IDNo, Seq, BeginDate, EndDate, IndustryType, Company, Department, Title, WorkType,")
            strSQL.AppendLine("    WorkYear, Profession, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @IDNo, @Seq, @BeginDate, @EndDate, @IndustryType, @Company, @Department, @Title, @WorkType,")
            strSQL.AppendLine("    @WorkYear, @Profession, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, ExperienceRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, ExperienceRow.Seq.Value)
            db.AddInParameter(dbcmd, "@BeginDate", DbType.Date, IIf(IsDateTimeNull(ExperienceRow.BeginDate.Value), Convert.ToDateTime("1900/1/1"), ExperienceRow.BeginDate.Value))
            db.AddInParameter(dbcmd, "@EndDate", DbType.Date, IIf(IsDateTimeNull(ExperienceRow.EndDate.Value), Convert.ToDateTime("1900/1/1"), ExperienceRow.EndDate.Value))
            db.AddInParameter(dbcmd, "@IndustryType", DbType.String, ExperienceRow.IndustryType.Value)
            db.AddInParameter(dbcmd, "@Company", DbType.String, ExperienceRow.Company.Value)
            db.AddInParameter(dbcmd, "@Department", DbType.String, ExperienceRow.Department.Value)
            db.AddInParameter(dbcmd, "@Title", DbType.String, ExperienceRow.Title.Value)
            db.AddInParameter(dbcmd, "@WorkType", DbType.String, ExperienceRow.WorkType.Value)
            db.AddInParameter(dbcmd, "@WorkYear", DbType.Decimal, ExperienceRow.WorkYear.Value)
            db.AddInParameter(dbcmd, "@Profession", DbType.String, ExperienceRow.Profession.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, ExperienceRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, ExperienceRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(ExperienceRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), ExperienceRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal ExperienceRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into Experience")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    IDNo, Seq, BeginDate, EndDate, IndustryType, Company, Department, Title, WorkType,")
            strSQL.AppendLine("    WorkYear, Profession, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @IDNo, @Seq, @BeginDate, @EndDate, @IndustryType, @Company, @Department, @Title, @WorkType,")
            strSQL.AppendLine("    @WorkYear, @Profession, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In ExperienceRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                        db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                        db.AddInParameter(dbcmd, "@BeginDate", DbType.Date, IIf(IsDateTimeNull(r.BeginDate.Value), Convert.ToDateTime("1900/1/1"), r.BeginDate.Value))
                        db.AddInParameter(dbcmd, "@EndDate", DbType.Date, IIf(IsDateTimeNull(r.EndDate.Value), Convert.ToDateTime("1900/1/1"), r.EndDate.Value))
                        db.AddInParameter(dbcmd, "@IndustryType", DbType.String, r.IndustryType.Value)
                        db.AddInParameter(dbcmd, "@Company", DbType.String, r.Company.Value)
                        db.AddInParameter(dbcmd, "@Department", DbType.String, r.Department.Value)
                        db.AddInParameter(dbcmd, "@Title", DbType.String, r.Title.Value)
                        db.AddInParameter(dbcmd, "@WorkType", DbType.String, r.WorkType.Value)
                        db.AddInParameter(dbcmd, "@WorkYear", DbType.Decimal, r.WorkYear.Value)
                        db.AddInParameter(dbcmd, "@Profession", DbType.String, r.Profession.Value)
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

        Public Function Insert(ByVal ExperienceRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into Experience")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    IDNo, Seq, BeginDate, EndDate, IndustryType, Company, Department, Title, WorkType,")
            strSQL.AppendLine("    WorkYear, Profession, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @IDNo, @Seq, @BeginDate, @EndDate, @IndustryType, @Company, @Department, @Title, @WorkType,")
            strSQL.AppendLine("    @WorkYear, @Profession, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In ExperienceRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                db.AddInParameter(dbcmd, "@BeginDate", DbType.Date, IIf(IsDateTimeNull(r.BeginDate.Value), Convert.ToDateTime("1900/1/1"), r.BeginDate.Value))
                db.AddInParameter(dbcmd, "@EndDate", DbType.Date, IIf(IsDateTimeNull(r.EndDate.Value), Convert.ToDateTime("1900/1/1"), r.EndDate.Value))
                db.AddInParameter(dbcmd, "@IndustryType", DbType.String, r.IndustryType.Value)
                db.AddInParameter(dbcmd, "@Company", DbType.String, r.Company.Value)
                db.AddInParameter(dbcmd, "@Department", DbType.String, r.Department.Value)
                db.AddInParameter(dbcmd, "@Title", DbType.String, r.Title.Value)
                db.AddInParameter(dbcmd, "@WorkType", DbType.String, r.WorkType.Value)
                db.AddInParameter(dbcmd, "@WorkYear", DbType.Decimal, r.WorkYear.Value)
                db.AddInParameter(dbcmd, "@Profession", DbType.String, r.Profession.Value)
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

