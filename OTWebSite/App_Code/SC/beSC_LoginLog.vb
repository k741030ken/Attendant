'****************************************************************
' Table:SC_LoginLog
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

Namespace beSC_LoginLog
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "UserID", "UserName", "LogType", "Source", "LogDateTime", "HostName" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(String) }
        Private m_PrimaryFields As String() = {  }

        Public ReadOnly Property Rows() As beSC_LoginLog.Rows 
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
        Public Sub Transfer2Row(SC_LoginLogTable As DataTable)
            For Each dr As DataRow In SC_LoginLogTable.Rows
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

                dr(m_Rows(i).UserID.FieldName) = m_Rows(i).UserID.Value
                dr(m_Rows(i).UserName.FieldName) = m_Rows(i).UserName.Value
                dr(m_Rows(i).LogType.FieldName) = m_Rows(i).LogType.Value
                dr(m_Rows(i).Source.FieldName) = m_Rows(i).Source.Value
                dr(m_Rows(i).LogDateTime.FieldName) = m_Rows(i).LogDateTime.Value
                dr(m_Rows(i).HostName.FieldName) = m_Rows(i).HostName.Value

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

        Public Sub Add(SC_LoginLogRow As Row)
            m_Rows.Add(SC_LoginLogRow)
        End Sub

        Public Sub Remove(SC_LoginLogRow As Row)
            If m_Rows.IndexOf(SC_LoginLogRow) >= 0 Then
                m_Rows.Remove(SC_LoginLogRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_UserID As Field(Of String) = new Field(Of String)("UserID", true)
        Private FI_UserName As Field(Of String) = new Field(Of String)("UserName", true)
        Private FI_LogType As Field(Of String) = new Field(Of String)("LogType", true)
        Private FI_Source As Field(Of String) = new Field(Of String)("Source", true)
        Private FI_LogDateTime As Field(Of Date) = new Field(Of Date)("LogDateTime", true)
        Private FI_HostName As Field(Of String) = new Field(Of String)("HostName", true)
        Private m_FieldNames As String() = { "UserID", "UserName", "LogType", "Source", "LogDateTime", "HostName" }
        Private m_PrimaryFields As String() = {  }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "UserID"
                    Return FI_UserID.Value
                Case "UserName"
                    Return FI_UserName.Value
                Case "LogType"
                    Return FI_LogType.Value
                Case "Source"
                    Return FI_Source.Value
                Case "LogDateTime"
                    Return FI_LogDateTime.Value
                Case "HostName"
                    Return FI_HostName.Value
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Sub SetFieldValue(ByVal fieldName As String, ByVal value As Object)
            Select Case fieldName
                Case "UserID"
                    FI_UserID.SetValue(value)
                Case "UserName"
                    FI_UserName.SetValue(value)
                Case "LogType"
                    FI_LogType.SetValue(value)
                Case "Source"
                    FI_Source.SetValue(value)
                Case "LogDateTime"
                    FI_LogDateTime.SetValue(value)
                Case "HostName"
                    FI_HostName.SetValue(value)
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
                Case "UserID"
                    return FI_UserID.Updated
                Case "UserName"
                    return FI_UserName.Updated
                Case "LogType"
                    return FI_LogType.Updated
                Case "Source"
                    return FI_Source.Updated
                Case "LogDateTime"
                    return FI_LogDateTime.Updated
                Case "HostName"
                    return FI_HostName.Updated
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public Function CreateUpdateSQL(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "UserID"
                    return FI_UserID.CreateUpdateSQL
                Case "UserName"
                    return FI_UserName.CreateUpdateSQL
                Case "LogType"
                    return FI_LogType.CreateUpdateSQL
                Case "Source"
                    return FI_Source.CreateUpdateSQL
                Case "LogDateTime"
                    return FI_LogDateTime.CreateUpdateSQL
                Case "HostName"
                    return FI_HostName.CreateUpdateSQL
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
            FI_UserID.SetInitValue("")
            FI_UserName.SetInitValue("")
            FI_LogType.SetInitValue("")
            FI_Source.SetInitValue("")
            FI_LogDateTime.SetInitValue(DateTime.Now)
            FI_HostName.SetInitValue("")
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_UserID.SetInitValue(dr("UserID"))
            FI_UserName.SetInitValue(dr("UserName"))
            FI_LogType.SetInitValue(dr("LogType"))
            FI_Source.SetInitValue(dr("Source"))
            FI_LogDateTime.SetInitValue(dr("LogDateTime"))
            FI_HostName.SetInitValue(dr("HostName"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_UserID.Updated = False
            FI_UserName.Updated = False
            FI_LogType.Updated = False
            FI_Source.Updated = False
            FI_LogDateTime.Updated = False
            FI_HostName.Updated = False
        End Sub

        Public ReadOnly Property UserID As Field(Of String) 
            Get
                Return FI_UserID
            End Get
        End Property

        Public ReadOnly Property UserName As Field(Of String) 
            Get
                Return FI_UserName
            End Get
        End Property

        Public ReadOnly Property LogType As Field(Of String) 
            Get
                Return FI_LogType
            End Get
        End Property

        Public ReadOnly Property Source As Field(Of String) 
            Get
                Return FI_Source
            End Get
        End Property

        Public ReadOnly Property LogDateTime As Field(Of Date) 
            Get
                Return FI_LogDateTime
            End Get
        End Property

        Public ReadOnly Property HostName As Field(Of String) 
            Get
                Return FI_HostName
            End Get
        End Property

    End Class

    Public Class Service
        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_LoginLog")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal SC_LoginLogRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_LoginLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    UserID, UserName, LogType, Source, LogDateTime, HostName")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @UserID, @UserName, @LogType, @Source, @LogDateTime, @HostName")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_LoginLogRow.UserID.Value)
            db.AddInParameter(dbcmd, "@UserName", DbType.String, SC_LoginLogRow.UserName.Value)
            db.AddInParameter(dbcmd, "@LogType", DbType.String, SC_LoginLogRow.LogType.Value)
            db.AddInParameter(dbcmd, "@Source", DbType.String, SC_LoginLogRow.Source.Value)
            db.AddInParameter(dbcmd, "@LogDateTime", DbType.Date, IIf(IsDateTimeNull(SC_LoginLogRow.LogDateTime.Value), Convert.ToDateTime("1900/1/1"), SC_LoginLogRow.LogDateTime.Value))
            db.AddInParameter(dbcmd, "@HostName", DbType.String, SC_LoginLogRow.HostName.Value)

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal SC_LoginLogRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_LoginLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    UserID, UserName, LogType, Source, LogDateTime, HostName")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @UserID, @UserName, @LogType, @Source, @LogDateTime, @HostName")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_LoginLogRow.UserID.Value)
            db.AddInParameter(dbcmd, "@UserName", DbType.String, SC_LoginLogRow.UserName.Value)
            db.AddInParameter(dbcmd, "@LogType", DbType.String, SC_LoginLogRow.LogType.Value)
            db.AddInParameter(dbcmd, "@Source", DbType.String, SC_LoginLogRow.Source.Value)
            db.AddInParameter(dbcmd, "@LogDateTime", DbType.Date, IIf(IsDateTimeNull(SC_LoginLogRow.LogDateTime.Value), Convert.ToDateTime("1900/1/1"), SC_LoginLogRow.LogDateTime.Value))
            db.AddInParameter(dbcmd, "@HostName", DbType.String, SC_LoginLogRow.HostName.Value)

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal SC_LoginLogRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_LoginLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    UserID, UserName, LogType, Source, LogDateTime, HostName")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @UserID, @UserName, @LogType, @Source, @LogDateTime, @HostName")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_LoginLogRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                        db.AddInParameter(dbcmd, "@UserName", DbType.String, r.UserName.Value)
                        db.AddInParameter(dbcmd, "@LogType", DbType.String, r.LogType.Value)
                        db.AddInParameter(dbcmd, "@Source", DbType.String, r.Source.Value)
                        db.AddInParameter(dbcmd, "@LogDateTime", DbType.Date, IIf(IsDateTimeNull(r.LogDateTime.Value), Convert.ToDateTime("1900/1/1"), r.LogDateTime.Value))
                        db.AddInParameter(dbcmd, "@HostName", DbType.String, r.HostName.Value)

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

        Public Function Insert(ByVal SC_LoginLogRow As Row(), ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_LoginLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    UserID, UserName, LogType, Source, LogDateTime, HostName")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @UserID, @UserName, @LogType, @Source, @LogDateTime, @HostName")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_LoginLogRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                db.AddInParameter(dbcmd, "@UserName", DbType.String, r.UserName.Value)
                db.AddInParameter(dbcmd, "@LogType", DbType.String, r.LogType.Value)
                db.AddInParameter(dbcmd, "@Source", DbType.String, r.Source.Value)
                db.AddInParameter(dbcmd, "@LogDateTime", DbType.Date, IIf(IsDateTimeNull(r.LogDateTime.Value), Convert.ToDateTime("1900/1/1"), r.LogDateTime.Value))
                db.AddInParameter(dbcmd, "@HostName", DbType.String, r.HostName.Value)

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

