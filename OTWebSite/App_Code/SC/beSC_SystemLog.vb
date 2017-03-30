'****************************************************************
' Table:SC_SystemLog
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

Namespace beSC_SystemLog
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "LogSrc", "Status", "KeyID", "LogDateTime", "LogDesc" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(Date), GetType(String) }
        Private m_PrimaryFields As String() = {  }

        Public ReadOnly Property Rows() As beSC_SystemLog.Rows 
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
        Public Sub Transfer2Row(SC_SystemLogTable As DataTable)
            For Each dr As DataRow In SC_SystemLogTable.Rows
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

                dr(m_Rows(i).LogSrc.FieldName) = m_Rows(i).LogSrc.Value
                dr(m_Rows(i).Status.FieldName) = m_Rows(i).Status.Value
                dr(m_Rows(i).KeyID.FieldName) = m_Rows(i).KeyID.Value
                dr(m_Rows(i).LogDateTime.FieldName) = m_Rows(i).LogDateTime.Value
                dr(m_Rows(i).LogDesc.FieldName) = m_Rows(i).LogDesc.Value

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

        Public Sub Add(SC_SystemLogRow As Row)
            m_Rows.Add(SC_SystemLogRow)
        End Sub

        Public Sub Remove(SC_SystemLogRow As Row)
            If m_Rows.IndexOf(SC_SystemLogRow) >= 0 Then
                m_Rows.Remove(SC_SystemLogRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_LogSrc As Field(Of String) = new Field(Of String)("LogSrc", true)
        Private FI_Status As Field(Of String) = new Field(Of String)("Status", true)
        Private FI_KeyID As Field(Of String) = new Field(Of String)("KeyID", true)
        Private FI_LogDateTime As Field(Of Date) = new Field(Of Date)("LogDateTime", true)
        Private FI_LogDesc As Field(Of String) = new Field(Of String)("LogDesc", true)
        Private m_FieldNames As String() = { "LogSrc", "Status", "KeyID", "LogDateTime", "LogDesc" }
        Private m_PrimaryFields As String() = {  }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "LogSrc"
                    Return FI_LogSrc.Value
                Case "Status"
                    Return FI_Status.Value
                Case "KeyID"
                    Return FI_KeyID.Value
                Case "LogDateTime"
                    Return FI_LogDateTime.Value
                Case "LogDesc"
                    Return FI_LogDesc.Value
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Sub SetFieldValue(ByVal fieldName As String, ByVal value As Object)
            Select Case fieldName
                Case "LogSrc"
                    FI_LogSrc.SetValue(value)
                Case "Status"
                    FI_Status.SetValue(value)
                Case "KeyID"
                    FI_KeyID.SetValue(value)
                Case "LogDateTime"
                    FI_LogDateTime.SetValue(value)
                Case "LogDesc"
                    FI_LogDesc.SetValue(value)
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
                Case "LogSrc"
                    return FI_LogSrc.Updated
                Case "Status"
                    return FI_Status.Updated
                Case "KeyID"
                    return FI_KeyID.Updated
                Case "LogDateTime"
                    return FI_LogDateTime.Updated
                Case "LogDesc"
                    return FI_LogDesc.Updated
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public Function CreateUpdateSQL(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "LogSrc"
                    return FI_LogSrc.CreateUpdateSQL
                Case "Status"
                    return FI_Status.CreateUpdateSQL
                Case "KeyID"
                    return FI_KeyID.CreateUpdateSQL
                Case "LogDateTime"
                    return FI_LogDateTime.CreateUpdateSQL
                Case "LogDesc"
                    return FI_LogDesc.CreateUpdateSQL
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
            FI_LogSrc.SetInitValue("")
            FI_Status.SetInitValue("")
            FI_KeyID.SetInitValue("")
            FI_LogDateTime.SetInitValue(DateTime.Now)
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_LogSrc.SetInitValue(dr("LogSrc"))
            FI_Status.SetInitValue(dr("Status"))
            FI_KeyID.SetInitValue(dr("KeyID"))
            FI_LogDateTime.SetInitValue(dr("LogDateTime"))
            FI_LogDesc.SetInitValue(dr("LogDesc"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_LogSrc.Updated = False
            FI_Status.Updated = False
            FI_KeyID.Updated = False
            FI_LogDateTime.Updated = False
            FI_LogDesc.Updated = False
        End Sub

        Public ReadOnly Property LogSrc As Field(Of String) 
            Get
                Return FI_LogSrc
            End Get
        End Property

        Public ReadOnly Property Status As Field(Of String) 
            Get
                Return FI_Status
            End Get
        End Property

        Public ReadOnly Property KeyID As Field(Of String) 
            Get
                Return FI_KeyID
            End Get
        End Property

        Public ReadOnly Property LogDateTime As Field(Of Date) 
            Get
                Return FI_LogDateTime
            End Get
        End Property

        Public ReadOnly Property LogDesc As Field(Of String) 
            Get
                Return FI_LogDesc
            End Get
        End Property

    End Class

    Public Class Service
        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_SystemLog")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal SC_SystemLogRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_SystemLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    LogSrc, Status, KeyID, LogDateTime, LogDesc")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @LogSrc, @Status, @KeyID, @LogDateTime, @LogDesc")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@LogSrc", DbType.String, SC_SystemLogRow.LogSrc.Value)
            db.AddInParameter(dbcmd, "@Status", DbType.String, SC_SystemLogRow.Status.Value)
            db.AddInParameter(dbcmd, "@KeyID", DbType.String, SC_SystemLogRow.KeyID.Value)
            db.AddInParameter(dbcmd, "@LogDateTime", DbType.Date, IIf(IsDateTimeNull(SC_SystemLogRow.LogDateTime.Value), Convert.ToDateTime("1900/1/1"), SC_SystemLogRow.LogDateTime.Value))
            db.AddInParameter(dbcmd, "@LogDesc", DbType.String, SC_SystemLogRow.LogDesc.Value)

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal SC_SystemLogRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_SystemLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    LogSrc, Status, KeyID, LogDateTime, LogDesc")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @LogSrc, @Status, @KeyID, @LogDateTime, @LogDesc")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@LogSrc", DbType.String, SC_SystemLogRow.LogSrc.Value)
            db.AddInParameter(dbcmd, "@Status", DbType.String, SC_SystemLogRow.Status.Value)
            db.AddInParameter(dbcmd, "@KeyID", DbType.String, SC_SystemLogRow.KeyID.Value)
            db.AddInParameter(dbcmd, "@LogDateTime", DbType.Date, IIf(IsDateTimeNull(SC_SystemLogRow.LogDateTime.Value), Convert.ToDateTime("1900/1/1"), SC_SystemLogRow.LogDateTime.Value))
            db.AddInParameter(dbcmd, "@LogDesc", DbType.String, SC_SystemLogRow.LogDesc.Value)

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal SC_SystemLogRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_SystemLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    LogSrc, Status, KeyID, LogDateTime, LogDesc")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @LogSrc, @Status, @KeyID, @LogDateTime, @LogDesc")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_SystemLogRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@LogSrc", DbType.String, r.LogSrc.Value)
                        db.AddInParameter(dbcmd, "@Status", DbType.String, r.Status.Value)
                        db.AddInParameter(dbcmd, "@KeyID", DbType.String, r.KeyID.Value)
                        db.AddInParameter(dbcmd, "@LogDateTime", DbType.Date, IIf(IsDateTimeNull(r.LogDateTime.Value), Convert.ToDateTime("1900/1/1"), r.LogDateTime.Value))
                        db.AddInParameter(dbcmd, "@LogDesc", DbType.String, r.LogDesc.Value)

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

        Public Function Insert(ByVal SC_SystemLogRow As Row(), ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_SystemLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    LogSrc, Status, KeyID, LogDateTime, LogDesc")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @LogSrc, @Status, @KeyID, @LogDateTime, @LogDesc")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_SystemLogRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@LogSrc", DbType.String, r.LogSrc.Value)
                db.AddInParameter(dbcmd, "@Status", DbType.String, r.Status.Value)
                db.AddInParameter(dbcmd, "@KeyID", DbType.String, r.KeyID.Value)
                db.AddInParameter(dbcmd, "@LogDateTime", DbType.Date, IIf(IsDateTimeNull(r.LogDateTime.Value), Convert.ToDateTime("1900/1/1"), r.LogDateTime.Value))
                db.AddInParameter(dbcmd, "@LogDesc", DbType.String, r.LogDesc.Value)

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

