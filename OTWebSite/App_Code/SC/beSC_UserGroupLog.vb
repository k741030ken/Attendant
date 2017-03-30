'****************************************************************
' Table:SC_UserGroupLog
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

Namespace beSC_UserGroupLog
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "UserID", "UserName", "DeptID", "DeptName", "GroupID", "GroupName", "StartDate", "EndDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(Date) }
        Private m_PrimaryFields As String() = { "UserID" }

        Public ReadOnly Property Rows() As beSC_UserGroupLog.Rows 
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
        Public Sub Transfer2Row(SC_UserGroupLogTable As DataTable)
            For Each dr As DataRow In SC_UserGroupLogTable.Rows
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
                dr(m_Rows(i).DeptID.FieldName) = m_Rows(i).DeptID.Value
                dr(m_Rows(i).DeptName.FieldName) = m_Rows(i).DeptName.Value
                dr(m_Rows(i).GroupID.FieldName) = m_Rows(i).GroupID.Value
                dr(m_Rows(i).GroupName.FieldName) = m_Rows(i).GroupName.Value
                dr(m_Rows(i).StartDate.FieldName) = m_Rows(i).StartDate.Value
                dr(m_Rows(i).EndDate.FieldName) = m_Rows(i).EndDate.Value

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

        Public Sub Add(SC_UserGroupLogRow As Row)
            m_Rows.Add(SC_UserGroupLogRow)
        End Sub

        Public Sub Remove(SC_UserGroupLogRow As Row)
            If m_Rows.IndexOf(SC_UserGroupLogRow) >= 0 Then
                m_Rows.Remove(SC_UserGroupLogRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_UserID As Field(Of String) = new Field(Of String)("UserID", true)
        Private FI_UserName As Field(Of String) = new Field(Of String)("UserName", true)
        Private FI_DeptID As Field(Of String) = new Field(Of String)("DeptID", true)
        Private FI_DeptName As Field(Of String) = new Field(Of String)("DeptName", true)
        Private FI_GroupID As Field(Of String) = new Field(Of String)("GroupID", true)
        Private FI_GroupName As Field(Of String) = new Field(Of String)("GroupName", true)
        Private FI_StartDate As Field(Of Date) = new Field(Of Date)("StartDate", true)
        Private FI_EndDate As Field(Of Date) = new Field(Of Date)("EndDate", true)
        Private m_FieldNames As String() = { "UserID", "UserName", "DeptID", "DeptName", "GroupID", "GroupName", "StartDate", "EndDate" }
        Private m_PrimaryFields As String() = { "UserID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "UserID"
                    Return FI_UserID.Value
                Case "UserName"
                    Return FI_UserName.Value
                Case "DeptID"
                    Return FI_DeptID.Value
                Case "DeptName"
                    Return FI_DeptName.Value
                Case "GroupID"
                    Return FI_GroupID.Value
                Case "GroupName"
                    Return FI_GroupName.Value
                Case "StartDate"
                    Return FI_StartDate.Value
                Case "EndDate"
                    Return FI_EndDate.Value
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
                Case "DeptID"
                    FI_DeptID.SetValue(value)
                Case "DeptName"
                    FI_DeptName.SetValue(value)
                Case "GroupID"
                    FI_GroupID.SetValue(value)
                Case "GroupName"
                    FI_GroupName.SetValue(value)
                Case "StartDate"
                    FI_StartDate.SetValue(value)
                Case "EndDate"
                    FI_EndDate.SetValue(value)
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
                Case "DeptID"
                    return FI_DeptID.Updated
                Case "DeptName"
                    return FI_DeptName.Updated
                Case "GroupID"
                    return FI_GroupID.Updated
                Case "GroupName"
                    return FI_GroupName.Updated
                Case "StartDate"
                    return FI_StartDate.Updated
                Case "EndDate"
                    return FI_EndDate.Updated
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
                Case "DeptID"
                    return FI_DeptID.CreateUpdateSQL
                Case "DeptName"
                    return FI_DeptName.CreateUpdateSQL
                Case "GroupID"
                    return FI_GroupID.CreateUpdateSQL
                Case "GroupName"
                    return FI_GroupName.CreateUpdateSQL
                Case "StartDate"
                    return FI_StartDate.CreateUpdateSQL
                Case "EndDate"
                    return FI_EndDate.CreateUpdateSQL
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
            FI_DeptID.SetInitValue("")
            FI_DeptName.SetInitValue("")
            FI_GroupID.SetInitValue("")
            FI_GroupName.SetInitValue("")
            FI_StartDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_EndDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_UserID.SetInitValue(dr("UserID"))
            FI_UserName.SetInitValue(dr("UserName"))
            FI_DeptID.SetInitValue(dr("DeptID"))
            FI_DeptName.SetInitValue(dr("DeptName"))
            FI_GroupID.SetInitValue(dr("GroupID"))
            FI_GroupName.SetInitValue(dr("GroupName"))
            FI_StartDate.SetInitValue(dr("StartDate"))
            FI_EndDate.SetInitValue(dr("EndDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_UserID.Updated = False
            FI_UserName.Updated = False
            FI_DeptID.Updated = False
            FI_DeptName.Updated = False
            FI_GroupID.Updated = False
            FI_GroupName.Updated = False
            FI_StartDate.Updated = False
            FI_EndDate.Updated = False
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

        Public ReadOnly Property GroupID As Field(Of String) 
            Get
                Return FI_GroupID
            End Get
        End Property

        Public ReadOnly Property GroupName As Field(Of String) 
            Get
                Return FI_GroupName
            End Get
        End Property

        Public ReadOnly Property StartDate As Field(Of Date) 
            Get
                Return FI_StartDate
            End Get
        End Property

        Public ReadOnly Property EndDate As Field(Of Date) 
            Get
                Return FI_EndDate
            End Get
        End Property

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal SC_UserGroupLogRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_UserGroupLog")
            strSQL.AppendLine("Where UserID = @UserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_UserGroupLogRow.UserID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal SC_UserGroupLogRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_UserGroupLog")
            strSQL.AppendLine("Where UserID = @UserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_UserGroupLogRow.UserID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal SC_UserGroupLogRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_UserGroupLog")
            strSQL.AppendLine("Where UserID = @UserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_UserGroupLogRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal SC_UserGroupLogRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_UserGroupLog")
            strSQL.AppendLine("Where UserID = @UserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_UserGroupLogRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal SC_UserGroupLogRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_UserGroupLog")
            strSQL.AppendLine("Where UserID = @UserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_UserGroupLogRow.UserID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(SC_UserGroupLogRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_UserGroupLog")
            strSQL.AppendLine("Where UserID = @UserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_UserGroupLogRow.UserID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_UserGroupLogRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_UserGroupLog Set")
            For i As Integer = 0 To SC_UserGroupLogRow.FieldNames.Length - 1
                If Not SC_UserGroupLogRow.IsIdentityField(SC_UserGroupLogRow.FieldNames(i)) AndAlso SC_UserGroupLogRow.IsUpdated(SC_UserGroupLogRow.FieldNames(i)) AndAlso SC_UserGroupLogRow.CreateUpdateSQL(SC_UserGroupLogRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_UserGroupLogRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where UserID = @PKUserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_UserGroupLogRow.UserID.Updated Then db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_UserGroupLogRow.UserID.Value)
            If SC_UserGroupLogRow.UserName.Updated Then db.AddInParameter(dbcmd, "@UserName", DbType.String, SC_UserGroupLogRow.UserName.Value)
            If SC_UserGroupLogRow.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, SC_UserGroupLogRow.DeptID.Value)
            If SC_UserGroupLogRow.DeptName.Updated Then db.AddInParameter(dbcmd, "@DeptName", DbType.String, SC_UserGroupLogRow.DeptName.Value)
            If SC_UserGroupLogRow.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, SC_UserGroupLogRow.GroupID.Value)
            If SC_UserGroupLogRow.GroupName.Updated Then db.AddInParameter(dbcmd, "@GroupName", DbType.String, SC_UserGroupLogRow.GroupName.Value)
            If SC_UserGroupLogRow.StartDate.Updated Then db.AddInParameter(dbcmd, "@StartDate", DbType.Date, IIf(IsDateTimeNull(SC_UserGroupLogRow.StartDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserGroupLogRow.StartDate.Value))
            If SC_UserGroupLogRow.EndDate.Updated Then db.AddInParameter(dbcmd, "@EndDate", DbType.Date, IIf(IsDateTimeNull(SC_UserGroupLogRow.EndDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserGroupLogRow.EndDate.Value))
            db.AddInParameter(dbcmd, "@PKUserID", DbType.String, IIf(SC_UserGroupLogRow.LoadFromDataRow, SC_UserGroupLogRow.UserID.OldValue, SC_UserGroupLogRow.UserID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal SC_UserGroupLogRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_UserGroupLog Set")
            For i As Integer = 0 To SC_UserGroupLogRow.FieldNames.Length - 1
                If Not SC_UserGroupLogRow.IsIdentityField(SC_UserGroupLogRow.FieldNames(i)) AndAlso SC_UserGroupLogRow.IsUpdated(SC_UserGroupLogRow.FieldNames(i)) AndAlso SC_UserGroupLogRow.CreateUpdateSQL(SC_UserGroupLogRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_UserGroupLogRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where UserID = @PKUserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_UserGroupLogRow.UserID.Updated Then db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_UserGroupLogRow.UserID.Value)
            If SC_UserGroupLogRow.UserName.Updated Then db.AddInParameter(dbcmd, "@UserName", DbType.String, SC_UserGroupLogRow.UserName.Value)
            If SC_UserGroupLogRow.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, SC_UserGroupLogRow.DeptID.Value)
            If SC_UserGroupLogRow.DeptName.Updated Then db.AddInParameter(dbcmd, "@DeptName", DbType.String, SC_UserGroupLogRow.DeptName.Value)
            If SC_UserGroupLogRow.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, SC_UserGroupLogRow.GroupID.Value)
            If SC_UserGroupLogRow.GroupName.Updated Then db.AddInParameter(dbcmd, "@GroupName", DbType.String, SC_UserGroupLogRow.GroupName.Value)
            If SC_UserGroupLogRow.StartDate.Updated Then db.AddInParameter(dbcmd, "@StartDate", DbType.Date, IIf(IsDateTimeNull(SC_UserGroupLogRow.StartDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserGroupLogRow.StartDate.Value))
            If SC_UserGroupLogRow.EndDate.Updated Then db.AddInParameter(dbcmd, "@EndDate", DbType.Date, IIf(IsDateTimeNull(SC_UserGroupLogRow.EndDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserGroupLogRow.EndDate.Value))
            db.AddInParameter(dbcmd, "@PKUserID", DbType.String, IIf(SC_UserGroupLogRow.LoadFromDataRow, SC_UserGroupLogRow.UserID.OldValue, SC_UserGroupLogRow.UserID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_UserGroupLogRow As Row()) As Integer
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
                    For Each r As Row In SC_UserGroupLogRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update SC_UserGroupLog Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where UserID = @PKUserID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.UserID.Updated Then db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                        If r.UserName.Updated Then db.AddInParameter(dbcmd, "@UserName", DbType.String, r.UserName.Value)
                        If r.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                        If r.DeptName.Updated Then db.AddInParameter(dbcmd, "@DeptName", DbType.String, r.DeptName.Value)
                        If r.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                        If r.GroupName.Updated Then db.AddInParameter(dbcmd, "@GroupName", DbType.String, r.GroupName.Value)
                        If r.StartDate.Updated Then db.AddInParameter(dbcmd, "@StartDate", DbType.Date, IIf(IsDateTimeNull(r.StartDate.Value), Convert.ToDateTime("1900/1/1"), r.StartDate.Value))
                        If r.EndDate.Updated Then db.AddInParameter(dbcmd, "@EndDate", DbType.Date, IIf(IsDateTimeNull(r.EndDate.Value), Convert.ToDateTime("1900/1/1"), r.EndDate.Value))
                        db.AddInParameter(dbcmd, "@PKUserID", DbType.String, IIf(r.LoadFromDataRow, r.UserID.OldValue, r.UserID.Value))

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

        Public Function Update(ByVal SC_UserGroupLogRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In SC_UserGroupLogRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update SC_UserGroupLog Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where UserID = @PKUserID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.UserID.Updated Then db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                If r.UserName.Updated Then db.AddInParameter(dbcmd, "@UserName", DbType.String, r.UserName.Value)
                If r.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                If r.DeptName.Updated Then db.AddInParameter(dbcmd, "@DeptName", DbType.String, r.DeptName.Value)
                If r.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                If r.GroupName.Updated Then db.AddInParameter(dbcmd, "@GroupName", DbType.String, r.GroupName.Value)
                If r.StartDate.Updated Then db.AddInParameter(dbcmd, "@StartDate", DbType.Date, IIf(IsDateTimeNull(r.StartDate.Value), Convert.ToDateTime("1900/1/1"), r.StartDate.Value))
                If r.EndDate.Updated Then db.AddInParameter(dbcmd, "@EndDate", DbType.Date, IIf(IsDateTimeNull(r.EndDate.Value), Convert.ToDateTime("1900/1/1"), r.EndDate.Value))
                db.AddInParameter(dbcmd, "@PKUserID", DbType.String, IIf(r.LoadFromDataRow, r.UserID.OldValue, r.UserID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal SC_UserGroupLogRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_UserGroupLog")
            strSQL.AppendLine("Where UserID = @UserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_UserGroupLogRow.UserID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal SC_UserGroupLogRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_UserGroupLog")
            strSQL.AppendLine("Where UserID = @UserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_UserGroupLogRow.UserID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_UserGroupLog")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal SC_UserGroupLogRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_UserGroupLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    UserID, UserName, DeptID, DeptName, GroupID, GroupName, StartDate, EndDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @UserID, @UserName, @DeptID, @DeptName, @GroupID, @GroupName, @StartDate, @EndDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_UserGroupLogRow.UserID.Value)
            db.AddInParameter(dbcmd, "@UserName", DbType.String, SC_UserGroupLogRow.UserName.Value)
            db.AddInParameter(dbcmd, "@DeptID", DbType.String, SC_UserGroupLogRow.DeptID.Value)
            db.AddInParameter(dbcmd, "@DeptName", DbType.String, SC_UserGroupLogRow.DeptName.Value)
            db.AddInParameter(dbcmd, "@GroupID", DbType.String, SC_UserGroupLogRow.GroupID.Value)
            db.AddInParameter(dbcmd, "@GroupName", DbType.String, SC_UserGroupLogRow.GroupName.Value)
            db.AddInParameter(dbcmd, "@StartDate", DbType.Date, IIf(IsDateTimeNull(SC_UserGroupLogRow.StartDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserGroupLogRow.StartDate.Value))
            db.AddInParameter(dbcmd, "@EndDate", DbType.Date, IIf(IsDateTimeNull(SC_UserGroupLogRow.EndDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserGroupLogRow.EndDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal SC_UserGroupLogRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_UserGroupLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    UserID, UserName, DeptID, DeptName, GroupID, GroupName, StartDate, EndDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @UserID, @UserName, @DeptID, @DeptName, @GroupID, @GroupName, @StartDate, @EndDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_UserGroupLogRow.UserID.Value)
            db.AddInParameter(dbcmd, "@UserName", DbType.String, SC_UserGroupLogRow.UserName.Value)
            db.AddInParameter(dbcmd, "@DeptID", DbType.String, SC_UserGroupLogRow.DeptID.Value)
            db.AddInParameter(dbcmd, "@DeptName", DbType.String, SC_UserGroupLogRow.DeptName.Value)
            db.AddInParameter(dbcmd, "@GroupID", DbType.String, SC_UserGroupLogRow.GroupID.Value)
            db.AddInParameter(dbcmd, "@GroupName", DbType.String, SC_UserGroupLogRow.GroupName.Value)
            db.AddInParameter(dbcmd, "@StartDate", DbType.Date, IIf(IsDateTimeNull(SC_UserGroupLogRow.StartDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserGroupLogRow.StartDate.Value))
            db.AddInParameter(dbcmd, "@EndDate", DbType.Date, IIf(IsDateTimeNull(SC_UserGroupLogRow.EndDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserGroupLogRow.EndDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal SC_UserGroupLogRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_UserGroupLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    UserID, UserName, DeptID, DeptName, GroupID, GroupName, StartDate, EndDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @UserID, @UserName, @DeptID, @DeptName, @GroupID, @GroupName, @StartDate, @EndDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_UserGroupLogRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                        db.AddInParameter(dbcmd, "@UserName", DbType.String, r.UserName.Value)
                        db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                        db.AddInParameter(dbcmd, "@DeptName", DbType.String, r.DeptName.Value)
                        db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                        db.AddInParameter(dbcmd, "@GroupName", DbType.String, r.GroupName.Value)
                        db.AddInParameter(dbcmd, "@StartDate", DbType.Date, IIf(IsDateTimeNull(r.StartDate.Value), Convert.ToDateTime("1900/1/1"), r.StartDate.Value))
                        db.AddInParameter(dbcmd, "@EndDate", DbType.Date, IIf(IsDateTimeNull(r.EndDate.Value), Convert.ToDateTime("1900/1/1"), r.EndDate.Value))

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

        Public Function Insert(ByVal SC_UserGroupLogRow As Row(), ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_UserGroupLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    UserID, UserName, DeptID, DeptName, GroupID, GroupName, StartDate, EndDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @UserID, @UserName, @DeptID, @DeptName, @GroupID, @GroupName, @StartDate, @EndDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_UserGroupLogRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                db.AddInParameter(dbcmd, "@UserName", DbType.String, r.UserName.Value)
                db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                db.AddInParameter(dbcmd, "@DeptName", DbType.String, r.DeptName.Value)
                db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                db.AddInParameter(dbcmd, "@GroupName", DbType.String, r.GroupName.Value)
                db.AddInParameter(dbcmd, "@StartDate", DbType.Date, IIf(IsDateTimeNull(r.StartDate.Value), Convert.ToDateTime("1900/1/1"), r.StartDate.Value))
                db.AddInParameter(dbcmd, "@EndDate", DbType.Date, IIf(IsDateTimeNull(r.EndDate.Value), Convert.ToDateTime("1900/1/1"), r.EndDate.Value))

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

