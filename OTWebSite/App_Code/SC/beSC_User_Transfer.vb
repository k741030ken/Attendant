'****************************************************************
' Table:SC_User_Transfer
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

Namespace beSC_User_Transfer
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "UserID", "DeptID", "DeptName", "Status", "ProcessUserID", "ProcessDeptID", "ProcessDeptName", "CreateDate", "LastChgDate", "LastChgID" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(Date), GetType(String) }
        Private m_PrimaryFields As String() = {  }

        Public ReadOnly Property Rows() As beSC_User_Transfer.Rows 
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
        Public Sub Transfer2Row(SC_User_TransferTable As DataTable)
            For Each dr As DataRow In SC_User_TransferTable.Rows
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
                dr(m_Rows(i).DeptID.FieldName) = m_Rows(i).DeptID.Value
                dr(m_Rows(i).DeptName.FieldName) = m_Rows(i).DeptName.Value
                dr(m_Rows(i).Status.FieldName) = m_Rows(i).Status.Value
                dr(m_Rows(i).ProcessUserID.FieldName) = m_Rows(i).ProcessUserID.Value
                dr(m_Rows(i).ProcessDeptID.FieldName) = m_Rows(i).ProcessDeptID.Value
                dr(m_Rows(i).ProcessDeptName.FieldName) = m_Rows(i).ProcessDeptName.Value
                dr(m_Rows(i).CreateDate.FieldName) = m_Rows(i).CreateDate.Value
                dr(m_Rows(i).LastChgDate.FieldName) = m_Rows(i).LastChgDate.Value
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

        Public Sub Add(SC_User_TransferRow As Row)
            m_Rows.Add(SC_User_TransferRow)
        End Sub

        Public Sub Remove(SC_User_TransferRow As Row)
            If m_Rows.IndexOf(SC_User_TransferRow) >= 0 Then
                m_Rows.Remove(SC_User_TransferRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_UserID As Field(Of String) = new Field(Of String)("UserID", true)
        Private FI_DeptID As Field(Of String) = new Field(Of String)("DeptID", true)
        Private FI_DeptName As Field(Of String) = new Field(Of String)("DeptName", true)
        Private FI_Status As Field(Of String) = new Field(Of String)("Status", true)
        Private FI_ProcessUserID As Field(Of String) = new Field(Of String)("ProcessUserID", true)
        Private FI_ProcessDeptID As Field(Of String) = new Field(Of String)("ProcessDeptID", true)
        Private FI_ProcessDeptName As Field(Of String) = new Field(Of String)("ProcessDeptName", true)
        Private FI_CreateDate As Field(Of Date) = new Field(Of Date)("CreateDate", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private m_FieldNames As String() = { "UserID", "DeptID", "DeptName", "Status", "ProcessUserID", "ProcessDeptID", "ProcessDeptName", "CreateDate", "LastChgDate", "LastChgID" }
        Private m_PrimaryFields As String() = {  }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "UserID"
                    Return FI_UserID.Value
                Case "DeptID"
                    Return FI_DeptID.Value
                Case "DeptName"
                    Return FI_DeptName.Value
                Case "Status"
                    Return FI_Status.Value
                Case "ProcessUserID"
                    Return FI_ProcessUserID.Value
                Case "ProcessDeptID"
                    Return FI_ProcessDeptID.Value
                Case "ProcessDeptName"
                    Return FI_ProcessDeptName.Value
                Case "CreateDate"
                    Return FI_CreateDate.Value
                Case "LastChgDate"
                    Return FI_LastChgDate.Value
                Case "LastChgID"
                    Return FI_LastChgID.Value
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Sub SetFieldValue(ByVal fieldName As String, ByVal value As Object)
            Select Case fieldName
                Case "UserID"
                    FI_UserID.SetValue(value)
                Case "DeptID"
                    FI_DeptID.SetValue(value)
                Case "DeptName"
                    FI_DeptName.SetValue(value)
                Case "Status"
                    FI_Status.SetValue(value)
                Case "ProcessUserID"
                    FI_ProcessUserID.SetValue(value)
                Case "ProcessDeptID"
                    FI_ProcessDeptID.SetValue(value)
                Case "ProcessDeptName"
                    FI_ProcessDeptName.SetValue(value)
                Case "CreateDate"
                    FI_CreateDate.SetValue(value)
                Case "LastChgDate"
                    FI_LastChgDate.SetValue(value)
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
                Case "UserID"
                    return FI_UserID.Updated
                Case "DeptID"
                    return FI_DeptID.Updated
                Case "DeptName"
                    return FI_DeptName.Updated
                Case "Status"
                    return FI_Status.Updated
                Case "ProcessUserID"
                    return FI_ProcessUserID.Updated
                Case "ProcessDeptID"
                    return FI_ProcessDeptID.Updated
                Case "ProcessDeptName"
                    return FI_ProcessDeptName.Updated
                Case "CreateDate"
                    return FI_CreateDate.Updated
                Case "LastChgDate"
                    return FI_LastChgDate.Updated
                Case "LastChgID"
                    return FI_LastChgID.Updated
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public Function CreateUpdateSQL(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "UserID"
                    return FI_UserID.CreateUpdateSQL
                Case "DeptID"
                    return FI_DeptID.CreateUpdateSQL
                Case "DeptName"
                    return FI_DeptName.CreateUpdateSQL
                Case "Status"
                    return FI_Status.CreateUpdateSQL
                Case "ProcessUserID"
                    return FI_ProcessUserID.CreateUpdateSQL
                Case "ProcessDeptID"
                    return FI_ProcessDeptID.CreateUpdateSQL
                Case "ProcessDeptName"
                    return FI_ProcessDeptName.CreateUpdateSQL
                Case "CreateDate"
                    return FI_CreateDate.CreateUpdateSQL
                Case "LastChgDate"
                    return FI_LastChgDate.CreateUpdateSQL
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
            FI_UserID.SetInitValue("")
            FI_DeptID.SetInitValue("")
            FI_CreateDate.SetInitValue(DateTime.Now)
            FI_LastChgDate.SetInitValue(DateTime.Now)
            FI_LastChgID.SetInitValue("")
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_UserID.SetInitValue(dr("UserID"))
            FI_DeptID.SetInitValue(dr("DeptID"))
            FI_DeptName.SetInitValue(dr("DeptName"))
            FI_Status.SetInitValue(dr("Status"))
            FI_ProcessUserID.SetInitValue(dr("ProcessUserID"))
            FI_ProcessDeptID.SetInitValue(dr("ProcessDeptID"))
            FI_ProcessDeptName.SetInitValue(dr("ProcessDeptName"))
            FI_CreateDate.SetInitValue(dr("CreateDate"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_UserID.Updated = False
            FI_DeptID.Updated = False
            FI_DeptName.Updated = False
            FI_Status.Updated = False
            FI_ProcessUserID.Updated = False
            FI_ProcessDeptID.Updated = False
            FI_ProcessDeptName.Updated = False
            FI_CreateDate.Updated = False
            FI_LastChgDate.Updated = False
            FI_LastChgID.Updated = False
        End Sub

        Public ReadOnly Property UserID As Field(Of String) 
            Get
                Return FI_UserID
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

        Public ReadOnly Property Status As Field(Of String) 
            Get
                Return FI_Status
            End Get
        End Property

        Public ReadOnly Property ProcessUserID As Field(Of String) 
            Get
                Return FI_ProcessUserID
            End Get
        End Property

        Public ReadOnly Property ProcessDeptID As Field(Of String) 
            Get
                Return FI_ProcessDeptID
            End Get
        End Property

        Public ReadOnly Property ProcessDeptName As Field(Of String) 
            Get
                Return FI_ProcessDeptName
            End Get
        End Property

        Public ReadOnly Property CreateDate As Field(Of Date) 
            Get
                Return FI_CreateDate
            End Get
        End Property

        Public ReadOnly Property LastChgDate As Field(Of Date) 
            Get
                Return FI_LastChgDate
            End Get
        End Property

        Public ReadOnly Property LastChgID As Field(Of String) 
            Get
                Return FI_LastChgID
            End Get
        End Property

    End Class

    Public Class Service
        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_User_Transfer")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal SC_User_TransferRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_User_Transfer")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    UserID, DeptID, DeptName, Status, ProcessUserID, ProcessDeptID, ProcessDeptName, CreateDate,")
            strSQL.AppendLine("    LastChgDate, LastChgID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @UserID, @DeptID, @DeptName, @Status, @ProcessUserID, @ProcessDeptID, @ProcessDeptName, @CreateDate,")
            strSQL.AppendLine("    @LastChgDate, @LastChgID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_User_TransferRow.UserID.Value)
            db.AddInParameter(dbcmd, "@DeptID", DbType.String, SC_User_TransferRow.DeptID.Value)
            db.AddInParameter(dbcmd, "@DeptName", DbType.String, SC_User_TransferRow.DeptName.Value)
            db.AddInParameter(dbcmd, "@Status", DbType.String, SC_User_TransferRow.Status.Value)
            db.AddInParameter(dbcmd, "@ProcessUserID", DbType.String, SC_User_TransferRow.ProcessUserID.Value)
            db.AddInParameter(dbcmd, "@ProcessDeptID", DbType.String, SC_User_TransferRow.ProcessDeptID.Value)
            db.AddInParameter(dbcmd, "@ProcessDeptName", DbType.String, SC_User_TransferRow.ProcessDeptName.Value)
            db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_User_TransferRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_User_TransferRow.CreateDate.Value))
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_User_TransferRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_User_TransferRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_User_TransferRow.LastChgID.Value)

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal SC_User_TransferRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_User_Transfer")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    UserID, DeptID, DeptName, Status, ProcessUserID, ProcessDeptID, ProcessDeptName, CreateDate,")
            strSQL.AppendLine("    LastChgDate, LastChgID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @UserID, @DeptID, @DeptName, @Status, @ProcessUserID, @ProcessDeptID, @ProcessDeptName, @CreateDate,")
            strSQL.AppendLine("    @LastChgDate, @LastChgID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_User_TransferRow.UserID.Value)
            db.AddInParameter(dbcmd, "@DeptID", DbType.String, SC_User_TransferRow.DeptID.Value)
            db.AddInParameter(dbcmd, "@DeptName", DbType.String, SC_User_TransferRow.DeptName.Value)
            db.AddInParameter(dbcmd, "@Status", DbType.String, SC_User_TransferRow.Status.Value)
            db.AddInParameter(dbcmd, "@ProcessUserID", DbType.String, SC_User_TransferRow.ProcessUserID.Value)
            db.AddInParameter(dbcmd, "@ProcessDeptID", DbType.String, SC_User_TransferRow.ProcessDeptID.Value)
            db.AddInParameter(dbcmd, "@ProcessDeptName", DbType.String, SC_User_TransferRow.ProcessDeptName.Value)
            db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_User_TransferRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_User_TransferRow.CreateDate.Value))
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_User_TransferRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_User_TransferRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_User_TransferRow.LastChgID.Value)

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal SC_User_TransferRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_User_Transfer")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    UserID, DeptID, DeptName, Status, ProcessUserID, ProcessDeptID, ProcessDeptName, CreateDate,")
            strSQL.AppendLine("    LastChgDate, LastChgID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @UserID, @DeptID, @DeptName, @Status, @ProcessUserID, @ProcessDeptID, @ProcessDeptName, @CreateDate,")
            strSQL.AppendLine("    @LastChgDate, @LastChgID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_User_TransferRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                        db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                        db.AddInParameter(dbcmd, "@DeptName", DbType.String, r.DeptName.Value)
                        db.AddInParameter(dbcmd, "@Status", DbType.String, r.Status.Value)
                        db.AddInParameter(dbcmd, "@ProcessUserID", DbType.String, r.ProcessUserID.Value)
                        db.AddInParameter(dbcmd, "@ProcessDeptID", DbType.String, r.ProcessDeptID.Value)
                        db.AddInParameter(dbcmd, "@ProcessDeptName", DbType.String, r.ProcessDeptName.Value)
                        db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                        db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
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

        Public Function Insert(ByVal SC_User_TransferRow As Row(), ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_User_Transfer")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    UserID, DeptID, DeptName, Status, ProcessUserID, ProcessDeptID, ProcessDeptName, CreateDate,")
            strSQL.AppendLine("    LastChgDate, LastChgID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @UserID, @DeptID, @DeptName, @Status, @ProcessUserID, @ProcessDeptID, @ProcessDeptName, @CreateDate,")
            strSQL.AppendLine("    @LastChgDate, @LastChgID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_User_TransferRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                db.AddInParameter(dbcmd, "@DeptName", DbType.String, r.DeptName.Value)
                db.AddInParameter(dbcmd, "@Status", DbType.String, r.Status.Value)
                db.AddInParameter(dbcmd, "@ProcessUserID", DbType.String, r.ProcessUserID.Value)
                db.AddInParameter(dbcmd, "@ProcessDeptID", DbType.String, r.ProcessDeptID.Value)
                db.AddInParameter(dbcmd, "@ProcessDeptName", DbType.String, r.ProcessDeptName.Value)
                db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
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

