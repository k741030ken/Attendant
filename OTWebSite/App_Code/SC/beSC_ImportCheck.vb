'****************************************************************
' Table:SC_ImportCheck
' Created Date: 2014.08.06
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beSC_ImportCheck
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "ImportType", "ImportName", "Status", "LoanDate", "Sdate", "Edate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(Date) }
        Private m_PrimaryFields As String() = { "ImportType" }

        Public ReadOnly Property Rows() As beSC_ImportCheck.Rows 
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
        Public Sub Transfer2Row(SC_ImportCheckTable As DataTable)
            For Each dr As DataRow In SC_ImportCheckTable.Rows
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

                dr(m_Rows(i).ImportType.FieldName) = m_Rows(i).ImportType.Value
                dr(m_Rows(i).ImportName.FieldName) = m_Rows(i).ImportName.Value
                dr(m_Rows(i).Status.FieldName) = m_Rows(i).Status.Value
                dr(m_Rows(i).LoanDate.FieldName) = m_Rows(i).LoanDate.Value
                dr(m_Rows(i).Sdate.FieldName) = m_Rows(i).Sdate.Value
                dr(m_Rows(i).Edate.FieldName) = m_Rows(i).Edate.Value

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

        Public Sub Add(SC_ImportCheckRow As Row)
            m_Rows.Add(SC_ImportCheckRow)
        End Sub

        Public Sub Remove(SC_ImportCheckRow As Row)
            If m_Rows.IndexOf(SC_ImportCheckRow) >= 0 Then
                m_Rows.Remove(SC_ImportCheckRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_ImportType As Field(Of String) = new Field(Of String)("ImportType", true)
        Private FI_ImportName As Field(Of String) = new Field(Of String)("ImportName", true)
        Private FI_Status As Field(Of String) = new Field(Of String)("Status", true)
        Private FI_LoanDate As Field(Of String) = new Field(Of String)("LoanDate", true)
        Private FI_Sdate As Field(Of Date) = new Field(Of Date)("Sdate", true)
        Private FI_Edate As Field(Of Date) = new Field(Of Date)("Edate", true)
        Private m_FieldNames As String() = { "ImportType", "ImportName", "Status", "LoanDate", "Sdate", "Edate" }
        Private m_PrimaryFields As String() = { "ImportType" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "ImportType"
                    Return FI_ImportType.Value
                Case "ImportName"
                    Return FI_ImportName.Value
                Case "Status"
                    Return FI_Status.Value
                Case "LoanDate"
                    Return FI_LoanDate.Value
                Case "Sdate"
                    Return FI_Sdate.Value
                Case "Edate"
                    Return FI_Edate.Value
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Sub SetFieldValue(ByVal fieldName As String, ByVal value As Object)
            Select Case fieldName
                Case "ImportType"
                    FI_ImportType.SetValue(value)
                Case "ImportName"
                    FI_ImportName.SetValue(value)
                Case "Status"
                    FI_Status.SetValue(value)
                Case "LoanDate"
                    FI_LoanDate.SetValue(value)
                Case "Sdate"
                    FI_Sdate.SetValue(value)
                Case "Edate"
                    FI_Edate.SetValue(value)
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
                Case "ImportType"
                    return FI_ImportType.Updated
                Case "ImportName"
                    return FI_ImportName.Updated
                Case "Status"
                    return FI_Status.Updated
                Case "LoanDate"
                    return FI_LoanDate.Updated
                Case "Sdate"
                    return FI_Sdate.Updated
                Case "Edate"
                    return FI_Edate.Updated
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public Function CreateUpdateSQL(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "ImportType"
                    return FI_ImportType.CreateUpdateSQL
                Case "ImportName"
                    return FI_ImportName.CreateUpdateSQL
                Case "Status"
                    return FI_Status.CreateUpdateSQL
                Case "LoanDate"
                    return FI_LoanDate.CreateUpdateSQL
                Case "Sdate"
                    return FI_Sdate.CreateUpdateSQL
                Case "Edate"
                    return FI_Edate.CreateUpdateSQL
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
            FI_ImportType.SetInitValue("")
            FI_ImportName.SetInitValue("")
            FI_Status.SetInitValue("")
            FI_LoanDate.SetInitValue("")
            FI_Sdate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_Edate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_ImportType.SetInitValue(dr("ImportType"))
            FI_ImportName.SetInitValue(dr("ImportName"))
            FI_Status.SetInitValue(dr("Status"))
            FI_LoanDate.SetInitValue(dr("LoanDate"))
            FI_Sdate.SetInitValue(dr("Sdate"))
            FI_Edate.SetInitValue(dr("Edate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_ImportType.Updated = False
            FI_ImportName.Updated = False
            FI_Status.Updated = False
            FI_LoanDate.Updated = False
            FI_Sdate.Updated = False
            FI_Edate.Updated = False
        End Sub

        Public ReadOnly Property ImportType As Field(Of String) 
            Get
                Return FI_ImportType
            End Get
        End Property

        Public ReadOnly Property ImportName As Field(Of String) 
            Get
                Return FI_ImportName
            End Get
        End Property

        Public ReadOnly Property Status As Field(Of String) 
            Get
                Return FI_Status
            End Get
        End Property

        Public ReadOnly Property LoanDate As Field(Of String) 
            Get
                Return FI_LoanDate
            End Get
        End Property

        Public ReadOnly Property Sdate As Field(Of Date) 
            Get
                Return FI_Sdate
            End Get
        End Property

        Public ReadOnly Property Edate As Field(Of Date) 
            Get
                Return FI_Edate
            End Get
        End Property

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal SC_ImportCheckRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_ImportCheck")
            strSQL.AppendLine("Where ImportType = @ImportType")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@ImportType", DbType.String, SC_ImportCheckRow.ImportType.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal SC_ImportCheckRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_ImportCheck")
            strSQL.AppendLine("Where ImportType = @ImportType")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@ImportType", DbType.String, SC_ImportCheckRow.ImportType.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal SC_ImportCheckRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_ImportCheck")
            strSQL.AppendLine("Where ImportType = @ImportType")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_ImportCheckRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@ImportType", DbType.String, r.ImportType.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal SC_ImportCheckRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_ImportCheck")
            strSQL.AppendLine("Where ImportType = @ImportType")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_ImportCheckRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@ImportType", DbType.String, r.ImportType.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal SC_ImportCheckRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_ImportCheck")
            strSQL.AppendLine("Where ImportType = @ImportType")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@ImportType", DbType.String, SC_ImportCheckRow.ImportType.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(SC_ImportCheckRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_ImportCheck")
            strSQL.AppendLine("Where ImportType = @ImportType")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@ImportType", DbType.String, SC_ImportCheckRow.ImportType.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_ImportCheckRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_ImportCheck Set")
            For i As Integer = 0 To SC_ImportCheckRow.FieldNames.Length - 1
                If Not SC_ImportCheckRow.IsIdentityField(SC_ImportCheckRow.FieldNames(i)) AndAlso SC_ImportCheckRow.IsUpdated(SC_ImportCheckRow.FieldNames(i)) AndAlso SC_ImportCheckRow.CreateUpdateSQL(SC_ImportCheckRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_ImportCheckRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where ImportType = @PKImportType")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_ImportCheckRow.ImportType.Updated Then db.AddInParameter(dbcmd, "@ImportType", DbType.String, SC_ImportCheckRow.ImportType.Value)
            If SC_ImportCheckRow.ImportName.Updated Then db.AddInParameter(dbcmd, "@ImportName", DbType.String, SC_ImportCheckRow.ImportName.Value)
            If SC_ImportCheckRow.Status.Updated Then db.AddInParameter(dbcmd, "@Status", DbType.String, SC_ImportCheckRow.Status.Value)
            If SC_ImportCheckRow.LoanDate.Updated Then db.AddInParameter(dbcmd, "@LoanDate", DbType.String, SC_ImportCheckRow.LoanDate.Value)
            If SC_ImportCheckRow.Sdate.Updated Then db.AddInParameter(dbcmd, "@Sdate", DbType.Date, IIf(IsDateTimeNull(SC_ImportCheckRow.Sdate.Value), Convert.ToDateTime("1900/1/1"), SC_ImportCheckRow.Sdate.Value))
            If SC_ImportCheckRow.Edate.Updated Then db.AddInParameter(dbcmd, "@Edate", DbType.Date, IIf(IsDateTimeNull(SC_ImportCheckRow.Edate.Value), Convert.ToDateTime("1900/1/1"), SC_ImportCheckRow.Edate.Value))
            db.AddInParameter(dbcmd, "@PKImportType", DbType.String, IIf(SC_ImportCheckRow.LoadFromDataRow, SC_ImportCheckRow.ImportType.OldValue, SC_ImportCheckRow.ImportType.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal SC_ImportCheckRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_ImportCheck Set")
            For i As Integer = 0 To SC_ImportCheckRow.FieldNames.Length - 1
                If Not SC_ImportCheckRow.IsIdentityField(SC_ImportCheckRow.FieldNames(i)) AndAlso SC_ImportCheckRow.IsUpdated(SC_ImportCheckRow.FieldNames(i)) AndAlso SC_ImportCheckRow.CreateUpdateSQL(SC_ImportCheckRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_ImportCheckRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where ImportType = @PKImportType")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_ImportCheckRow.ImportType.Updated Then db.AddInParameter(dbcmd, "@ImportType", DbType.String, SC_ImportCheckRow.ImportType.Value)
            If SC_ImportCheckRow.ImportName.Updated Then db.AddInParameter(dbcmd, "@ImportName", DbType.String, SC_ImportCheckRow.ImportName.Value)
            If SC_ImportCheckRow.Status.Updated Then db.AddInParameter(dbcmd, "@Status", DbType.String, SC_ImportCheckRow.Status.Value)
            If SC_ImportCheckRow.LoanDate.Updated Then db.AddInParameter(dbcmd, "@LoanDate", DbType.String, SC_ImportCheckRow.LoanDate.Value)
            If SC_ImportCheckRow.Sdate.Updated Then db.AddInParameter(dbcmd, "@Sdate", DbType.Date, IIf(IsDateTimeNull(SC_ImportCheckRow.Sdate.Value), Convert.ToDateTime("1900/1/1"), SC_ImportCheckRow.Sdate.Value))
            If SC_ImportCheckRow.Edate.Updated Then db.AddInParameter(dbcmd, "@Edate", DbType.Date, IIf(IsDateTimeNull(SC_ImportCheckRow.Edate.Value), Convert.ToDateTime("1900/1/1"), SC_ImportCheckRow.Edate.Value))
            db.AddInParameter(dbcmd, "@PKImportType", DbType.String, IIf(SC_ImportCheckRow.LoadFromDataRow, SC_ImportCheckRow.ImportType.OldValue, SC_ImportCheckRow.ImportType.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_ImportCheckRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_ImportCheckRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update SC_ImportCheck Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where ImportType = @PKImportType")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.ImportType.Updated Then db.AddInParameter(dbcmd, "@ImportType", DbType.String, r.ImportType.Value)
                        If r.ImportName.Updated Then db.AddInParameter(dbcmd, "@ImportName", DbType.String, r.ImportName.Value)
                        If r.Status.Updated Then db.AddInParameter(dbcmd, "@Status", DbType.String, r.Status.Value)
                        If r.LoanDate.Updated Then db.AddInParameter(dbcmd, "@LoanDate", DbType.String, r.LoanDate.Value)
                        If r.Sdate.Updated Then db.AddInParameter(dbcmd, "@Sdate", DbType.Date, IIf(IsDateTimeNull(r.Sdate.Value), Convert.ToDateTime("1900/1/1"), r.Sdate.Value))
                        If r.Edate.Updated Then db.AddInParameter(dbcmd, "@Edate", DbType.Date, IIf(IsDateTimeNull(r.Edate.Value), Convert.ToDateTime("1900/1/1"), r.Edate.Value))
                        db.AddInParameter(dbcmd, "@PKImportType", DbType.String, IIf(r.LoadFromDataRow, r.ImportType.OldValue, r.ImportType.Value))

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

        Public Function Update(ByVal SC_ImportCheckRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In SC_ImportCheckRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update SC_ImportCheck Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where ImportType = @PKImportType")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.ImportType.Updated Then db.AddInParameter(dbcmd, "@ImportType", DbType.String, r.ImportType.Value)
                If r.ImportName.Updated Then db.AddInParameter(dbcmd, "@ImportName", DbType.String, r.ImportName.Value)
                If r.Status.Updated Then db.AddInParameter(dbcmd, "@Status", DbType.String, r.Status.Value)
                If r.LoanDate.Updated Then db.AddInParameter(dbcmd, "@LoanDate", DbType.String, r.LoanDate.Value)
                If r.Sdate.Updated Then db.AddInParameter(dbcmd, "@Sdate", DbType.Date, IIf(IsDateTimeNull(r.Sdate.Value), Convert.ToDateTime("1900/1/1"), r.Sdate.Value))
                If r.Edate.Updated Then db.AddInParameter(dbcmd, "@Edate", DbType.Date, IIf(IsDateTimeNull(r.Edate.Value), Convert.ToDateTime("1900/1/1"), r.Edate.Value))
                db.AddInParameter(dbcmd, "@PKImportType", DbType.String, IIf(r.LoadFromDataRow, r.ImportType.OldValue, r.ImportType.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal SC_ImportCheckRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_ImportCheck")
            strSQL.AppendLine("Where ImportType = @ImportType")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@ImportType", DbType.String, SC_ImportCheckRow.ImportType.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal SC_ImportCheckRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_ImportCheck")
            strSQL.AppendLine("Where ImportType = @ImportType")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@ImportType", DbType.String, SC_ImportCheckRow.ImportType.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_ImportCheck")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal SC_ImportCheckRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_ImportCheck")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    ImportType, ImportName, Status, LoanDate, Sdate, Edate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @ImportType, @ImportName, @Status, @LoanDate, @Sdate, @Edate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@ImportType", DbType.String, SC_ImportCheckRow.ImportType.Value)
            db.AddInParameter(dbcmd, "@ImportName", DbType.String, SC_ImportCheckRow.ImportName.Value)
            db.AddInParameter(dbcmd, "@Status", DbType.String, SC_ImportCheckRow.Status.Value)
            db.AddInParameter(dbcmd, "@LoanDate", DbType.String, SC_ImportCheckRow.LoanDate.Value)
            db.AddInParameter(dbcmd, "@Sdate", DbType.Date, IIf(IsDateTimeNull(SC_ImportCheckRow.Sdate.Value), Convert.ToDateTime("1900/1/1"), SC_ImportCheckRow.Sdate.Value))
            db.AddInParameter(dbcmd, "@Edate", DbType.Date, IIf(IsDateTimeNull(SC_ImportCheckRow.Edate.Value), Convert.ToDateTime("1900/1/1"), SC_ImportCheckRow.Edate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal SC_ImportCheckRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_ImportCheck")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    ImportType, ImportName, Status, LoanDate, Sdate, Edate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @ImportType, @ImportName, @Status, @LoanDate, @Sdate, @Edate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@ImportType", DbType.String, SC_ImportCheckRow.ImportType.Value)
            db.AddInParameter(dbcmd, "@ImportName", DbType.String, SC_ImportCheckRow.ImportName.Value)
            db.AddInParameter(dbcmd, "@Status", DbType.String, SC_ImportCheckRow.Status.Value)
            db.AddInParameter(dbcmd, "@LoanDate", DbType.String, SC_ImportCheckRow.LoanDate.Value)
            db.AddInParameter(dbcmd, "@Sdate", DbType.Date, IIf(IsDateTimeNull(SC_ImportCheckRow.Sdate.Value), Convert.ToDateTime("1900/1/1"), SC_ImportCheckRow.Sdate.Value))
            db.AddInParameter(dbcmd, "@Edate", DbType.Date, IIf(IsDateTimeNull(SC_ImportCheckRow.Edate.Value), Convert.ToDateTime("1900/1/1"), SC_ImportCheckRow.Edate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal SC_ImportCheckRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_ImportCheck")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    ImportType, ImportName, Status, LoanDate, Sdate, Edate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @ImportType, @ImportName, @Status, @LoanDate, @Sdate, @Edate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_ImportCheckRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@ImportType", DbType.String, r.ImportType.Value)
                        db.AddInParameter(dbcmd, "@ImportName", DbType.String, r.ImportName.Value)
                        db.AddInParameter(dbcmd, "@Status", DbType.String, r.Status.Value)
                        db.AddInParameter(dbcmd, "@LoanDate", DbType.String, r.LoanDate.Value)
                        db.AddInParameter(dbcmd, "@Sdate", DbType.Date, IIf(IsDateTimeNull(r.Sdate.Value), Convert.ToDateTime("1900/1/1"), r.Sdate.Value))
                        db.AddInParameter(dbcmd, "@Edate", DbType.Date, IIf(IsDateTimeNull(r.Edate.Value), Convert.ToDateTime("1900/1/1"), r.Edate.Value))

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

        Public Function Insert(ByVal SC_ImportCheckRow As Row(), ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_ImportCheck")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    ImportType, ImportName, Status, LoanDate, Sdate, Edate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @ImportType, @ImportName, @Status, @LoanDate, @Sdate, @Edate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_ImportCheckRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@ImportType", DbType.String, r.ImportType.Value)
                db.AddInParameter(dbcmd, "@ImportName", DbType.String, r.ImportName.Value)
                db.AddInParameter(dbcmd, "@Status", DbType.String, r.Status.Value)
                db.AddInParameter(dbcmd, "@LoanDate", DbType.String, r.LoanDate.Value)
                db.AddInParameter(dbcmd, "@Sdate", DbType.Date, IIf(IsDateTimeNull(r.Sdate.Value), Convert.ToDateTime("1900/1/1"), r.Sdate.Value))
                db.AddInParameter(dbcmd, "@Edate", DbType.Date, IIf(IsDateTimeNull(r.Edate.Value), Convert.ToDateTime("1900/1/1"), r.Edate.Value))

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

