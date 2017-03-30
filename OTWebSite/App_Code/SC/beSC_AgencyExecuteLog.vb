'****************************************************************
' Table:SC_AgencyExecuteLog
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

Namespace beSC_AgencyExecuteLog
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "UserID", "AgentUserID", "AgencyType", "AgentDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = {  }

        Public ReadOnly Property Rows() As beSC_AgencyExecuteLog.Rows 
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
        Public Sub Transfer2Row(SC_AgencyExecuteLogTable As DataTable)
            For Each dr As DataRow In SC_AgencyExecuteLogTable.Rows
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
                dr(m_Rows(i).AgentUserID.FieldName) = m_Rows(i).AgentUserID.Value
                dr(m_Rows(i).AgencyType.FieldName) = m_Rows(i).AgencyType.Value
                dr(m_Rows(i).AgentDate.FieldName) = m_Rows(i).AgentDate.Value

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

        Public Sub Add(SC_AgencyExecuteLogRow As Row)
            m_Rows.Add(SC_AgencyExecuteLogRow)
        End Sub

        Public Sub Remove(SC_AgencyExecuteLogRow As Row)
            If m_Rows.IndexOf(SC_AgencyExecuteLogRow) >= 0 Then
                m_Rows.Remove(SC_AgencyExecuteLogRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_UserID As Field(Of String) = new Field(Of String)("UserID", true)
        Private FI_AgentUserID As Field(Of String) = new Field(Of String)("AgentUserID", true)
        Private FI_AgencyType As Field(Of String) = new Field(Of String)("AgencyType", true)
        Private FI_AgentDate As Field(Of Date) = new Field(Of Date)("AgentDate", true)
        Private m_FieldNames As String() = { "UserID", "AgentUserID", "AgencyType", "AgentDate" }
        Private m_PrimaryFields As String() = {  }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "UserID"
                    Return FI_UserID.Value
                Case "AgentUserID"
                    Return FI_AgentUserID.Value
                Case "AgencyType"
                    Return FI_AgencyType.Value
                Case "AgentDate"
                    Return FI_AgentDate.Value
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Sub SetFieldValue(ByVal fieldName As String, ByVal value As Object)
            Select Case fieldName
                Case "UserID"
                    FI_UserID.SetValue(value)
                Case "AgentUserID"
                    FI_AgentUserID.SetValue(value)
                Case "AgencyType"
                    FI_AgencyType.SetValue(value)
                Case "AgentDate"
                    FI_AgentDate.SetValue(value)
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
                Case "AgentUserID"
                    return FI_AgentUserID.Updated
                Case "AgencyType"
                    return FI_AgencyType.Updated
                Case "AgentDate"
                    return FI_AgentDate.Updated
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public Function CreateUpdateSQL(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "UserID"
                    return FI_UserID.CreateUpdateSQL
                Case "AgentUserID"
                    return FI_AgentUserID.CreateUpdateSQL
                Case "AgencyType"
                    return FI_AgencyType.CreateUpdateSQL
                Case "AgentDate"
                    return FI_AgentDate.CreateUpdateSQL
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
            FI_AgentUserID.SetInitValue("")
            FI_AgencyType.SetInitValue("")
            FI_AgentDate.SetInitValue(DateTime.Now)
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_UserID.SetInitValue(dr("UserID"))
            FI_AgentUserID.SetInitValue(dr("AgentUserID"))
            FI_AgencyType.SetInitValue(dr("AgencyType"))
            FI_AgentDate.SetInitValue(dr("AgentDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_UserID.Updated = False
            FI_AgentUserID.Updated = False
            FI_AgencyType.Updated = False
            FI_AgentDate.Updated = False
        End Sub

        Public ReadOnly Property UserID As Field(Of String) 
            Get
                Return FI_UserID
            End Get
        End Property

        Public ReadOnly Property AgentUserID As Field(Of String) 
            Get
                Return FI_AgentUserID
            End Get
        End Property

        Public ReadOnly Property AgencyType As Field(Of String) 
            Get
                Return FI_AgencyType
            End Get
        End Property

        Public ReadOnly Property AgentDate As Field(Of Date) 
            Get
                Return FI_AgentDate
            End Get
        End Property

    End Class

    Public Class Service
        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_AgencyExecuteLog")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal SC_AgencyExecuteLogRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_AgencyExecuteLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    UserID, AgentUserID, AgencyType, AgentDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @UserID, @AgentUserID, @AgencyType, @AgentDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_AgencyExecuteLogRow.UserID.Value)
            db.AddInParameter(dbcmd, "@AgentUserID", DbType.String, SC_AgencyExecuteLogRow.AgentUserID.Value)
            db.AddInParameter(dbcmd, "@AgencyType", DbType.String, SC_AgencyExecuteLogRow.AgencyType.Value)
            db.AddInParameter(dbcmd, "@AgentDate", DbType.Date, IIf(IsDateTimeNull(SC_AgencyExecuteLogRow.AgentDate.Value), Convert.ToDateTime("1900/1/1"), SC_AgencyExecuteLogRow.AgentDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal SC_AgencyExecuteLogRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_AgencyExecuteLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    UserID, AgentUserID, AgencyType, AgentDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @UserID, @AgentUserID, @AgencyType, @AgentDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_AgencyExecuteLogRow.UserID.Value)
            db.AddInParameter(dbcmd, "@AgentUserID", DbType.String, SC_AgencyExecuteLogRow.AgentUserID.Value)
            db.AddInParameter(dbcmd, "@AgencyType", DbType.String, SC_AgencyExecuteLogRow.AgencyType.Value)
            db.AddInParameter(dbcmd, "@AgentDate", DbType.Date, IIf(IsDateTimeNull(SC_AgencyExecuteLogRow.AgentDate.Value), Convert.ToDateTime("1900/1/1"), SC_AgencyExecuteLogRow.AgentDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal SC_AgencyExecuteLogRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_AgencyExecuteLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    UserID, AgentUserID, AgencyType, AgentDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @UserID, @AgentUserID, @AgencyType, @AgentDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_AgencyExecuteLogRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                        db.AddInParameter(dbcmd, "@AgentUserID", DbType.String, r.AgentUserID.Value)
                        db.AddInParameter(dbcmd, "@AgencyType", DbType.String, r.AgencyType.Value)
                        db.AddInParameter(dbcmd, "@AgentDate", DbType.Date, IIf(IsDateTimeNull(r.AgentDate.Value), Convert.ToDateTime("1900/1/1"), r.AgentDate.Value))

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

        Public Function Insert(ByVal SC_AgencyExecuteLogRow As Row(), ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_AgencyExecuteLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    UserID, AgentUserID, AgencyType, AgentDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @UserID, @AgentUserID, @AgencyType, @AgentDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_AgencyExecuteLogRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                db.AddInParameter(dbcmd, "@AgentUserID", DbType.String, r.AgentUserID.Value)
                db.AddInParameter(dbcmd, "@AgencyType", DbType.String, r.AgencyType.Value)
                db.AddInParameter(dbcmd, "@AgentDate", DbType.Date, IIf(IsDateTimeNull(r.AgentDate.Value), Convert.ToDateTime("1900/1/1"), r.AgentDate.Value))

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

