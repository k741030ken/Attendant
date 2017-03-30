'****************************************************************
' Table:SC_AOCODE
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

Namespace beSC_AOCODE
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "AOCODE", "Name", "Status", "EmpNo", "DataDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) }
        Private m_PrimaryFields As String() = { "EmpNo" }

        Public ReadOnly Property Rows() As beSC_AOCODE.Rows 
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
        Public Sub Transfer2Row(SC_AOCODETable As DataTable)
            For Each dr As DataRow In SC_AOCODETable.Rows
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

                dr(m_Rows(i).AOCODE.FieldName) = m_Rows(i).AOCODE.Value
                dr(m_Rows(i).Name.FieldName) = m_Rows(i).Name.Value
                dr(m_Rows(i).Status.FieldName) = m_Rows(i).Status.Value
                dr(m_Rows(i).EmpNo.FieldName) = m_Rows(i).EmpNo.Value
                dr(m_Rows(i).DataDate.FieldName) = m_Rows(i).DataDate.Value

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

        Public Sub Add(SC_AOCODERow As Row)
            m_Rows.Add(SC_AOCODERow)
        End Sub

        Public Sub Remove(SC_AOCODERow As Row)
            If m_Rows.IndexOf(SC_AOCODERow) >= 0 Then
                m_Rows.Remove(SC_AOCODERow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_AOCODE As Field(Of String) = new Field(Of String)("AOCODE", true)
        Private FI_Name As Field(Of String) = new Field(Of String)("Name", true)
        Private FI_Status As Field(Of String) = new Field(Of String)("Status", true)
        Private FI_EmpNo As Field(Of String) = new Field(Of String)("EmpNo", true)
        Private FI_DataDate As Field(Of String) = new Field(Of String)("DataDate", true)
        Private m_FieldNames As String() = { "AOCODE", "Name", "Status", "EmpNo", "DataDate" }
        Private m_PrimaryFields As String() = { "EmpNo" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "AOCODE"
                    Return FI_AOCODE.Value
                Case "Name"
                    Return FI_Name.Value
                Case "Status"
                    Return FI_Status.Value
                Case "EmpNo"
                    Return FI_EmpNo.Value
                Case "DataDate"
                    Return FI_DataDate.Value
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Sub SetFieldValue(ByVal fieldName As String, ByVal value As Object)
            Select Case fieldName
                Case "AOCODE"
                    FI_AOCODE.SetValue(value)
                Case "Name"
                    FI_Name.SetValue(value)
                Case "Status"
                    FI_Status.SetValue(value)
                Case "EmpNo"
                    FI_EmpNo.SetValue(value)
                Case "DataDate"
                    FI_DataDate.SetValue(value)
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
                Case "AOCODE"
                    return FI_AOCODE.Updated
                Case "Name"
                    return FI_Name.Updated
                Case "Status"
                    return FI_Status.Updated
                Case "EmpNo"
                    return FI_EmpNo.Updated
                Case "DataDate"
                    return FI_DataDate.Updated
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public Function CreateUpdateSQL(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "AOCODE"
                    return FI_AOCODE.CreateUpdateSQL
                Case "Name"
                    return FI_Name.CreateUpdateSQL
                Case "Status"
                    return FI_Status.CreateUpdateSQL
                Case "EmpNo"
                    return FI_EmpNo.CreateUpdateSQL
                Case "DataDate"
                    return FI_DataDate.CreateUpdateSQL
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
            FI_EmpNo.SetInitValue("")
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_AOCODE.SetInitValue(dr("AOCODE"))
            FI_Name.SetInitValue(dr("Name"))
            FI_Status.SetInitValue(dr("Status"))
            FI_EmpNo.SetInitValue(dr("EmpNo"))
            FI_DataDate.SetInitValue(dr("DataDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_AOCODE.Updated = False
            FI_Name.Updated = False
            FI_Status.Updated = False
            FI_EmpNo.Updated = False
            FI_DataDate.Updated = False
        End Sub

        Public ReadOnly Property AOCODE As Field(Of String) 
            Get
                Return FI_AOCODE
            End Get
        End Property

        Public ReadOnly Property Name As Field(Of String) 
            Get
                Return FI_Name
            End Get
        End Property

        Public ReadOnly Property Status As Field(Of String) 
            Get
                Return FI_Status
            End Get
        End Property

        Public ReadOnly Property EmpNo As Field(Of String) 
            Get
                Return FI_EmpNo
            End Get
        End Property

        Public ReadOnly Property DataDate As Field(Of String) 
            Get
                Return FI_DataDate
            End Get
        End Property

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal SC_AOCODERow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_AOCODE")
            strSQL.AppendLine("Where EmpNo = @EmpNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@EmpNo", DbType.String, SC_AOCODERow.EmpNo.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal SC_AOCODERow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_AOCODE")
            strSQL.AppendLine("Where EmpNo = @EmpNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@EmpNo", DbType.String, SC_AOCODERow.EmpNo.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal SC_AOCODERow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_AOCODE")
            strSQL.AppendLine("Where EmpNo = @EmpNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_AOCODERow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@EmpNo", DbType.String, r.EmpNo.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal SC_AOCODERow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_AOCODE")
            strSQL.AppendLine("Where EmpNo = @EmpNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_AOCODERow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@EmpNo", DbType.String, r.EmpNo.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal SC_AOCODERow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_AOCODE")
            strSQL.AppendLine("Where EmpNo = @EmpNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@EmpNo", DbType.String, SC_AOCODERow.EmpNo.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(SC_AOCODERow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_AOCODE")
            strSQL.AppendLine("Where EmpNo = @EmpNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@EmpNo", DbType.String, SC_AOCODERow.EmpNo.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_AOCODERow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_AOCODE Set")
            For i As Integer = 0 To SC_AOCODERow.FieldNames.Length - 1
                If Not SC_AOCODERow.IsIdentityField(SC_AOCODERow.FieldNames(i)) AndAlso SC_AOCODERow.IsUpdated(SC_AOCODERow.FieldNames(i)) AndAlso SC_AOCODERow.CreateUpdateSQL(SC_AOCODERow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_AOCODERow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where EmpNo = @PKEmpNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_AOCODERow.AOCODE.Updated Then db.AddInParameter(dbcmd, "@AOCODE", DbType.String, SC_AOCODERow.AOCODE.Value)
            If SC_AOCODERow.Name.Updated Then db.AddInParameter(dbcmd, "@Name", DbType.String, SC_AOCODERow.Name.Value)
            If SC_AOCODERow.Status.Updated Then db.AddInParameter(dbcmd, "@Status", DbType.String, SC_AOCODERow.Status.Value)
            If SC_AOCODERow.EmpNo.Updated Then db.AddInParameter(dbcmd, "@EmpNo", DbType.String, SC_AOCODERow.EmpNo.Value)
            If SC_AOCODERow.DataDate.Updated Then db.AddInParameter(dbcmd, "@DataDate", DbType.String, SC_AOCODERow.DataDate.Value)
            db.AddInParameter(dbcmd, "@PKEmpNo", DbType.String, IIf(SC_AOCODERow.LoadFromDataRow, SC_AOCODERow.EmpNo.OldValue, SC_AOCODERow.EmpNo.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal SC_AOCODERow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_AOCODE Set")
            For i As Integer = 0 To SC_AOCODERow.FieldNames.Length - 1
                If Not SC_AOCODERow.IsIdentityField(SC_AOCODERow.FieldNames(i)) AndAlso SC_AOCODERow.IsUpdated(SC_AOCODERow.FieldNames(i)) AndAlso SC_AOCODERow.CreateUpdateSQL(SC_AOCODERow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_AOCODERow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where EmpNo = @PKEmpNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_AOCODERow.AOCODE.Updated Then db.AddInParameter(dbcmd, "@AOCODE", DbType.String, SC_AOCODERow.AOCODE.Value)
            If SC_AOCODERow.Name.Updated Then db.AddInParameter(dbcmd, "@Name", DbType.String, SC_AOCODERow.Name.Value)
            If SC_AOCODERow.Status.Updated Then db.AddInParameter(dbcmd, "@Status", DbType.String, SC_AOCODERow.Status.Value)
            If SC_AOCODERow.EmpNo.Updated Then db.AddInParameter(dbcmd, "@EmpNo", DbType.String, SC_AOCODERow.EmpNo.Value)
            If SC_AOCODERow.DataDate.Updated Then db.AddInParameter(dbcmd, "@DataDate", DbType.String, SC_AOCODERow.DataDate.Value)
            db.AddInParameter(dbcmd, "@PKEmpNo", DbType.String, IIf(SC_AOCODERow.LoadFromDataRow, SC_AOCODERow.EmpNo.OldValue, SC_AOCODERow.EmpNo.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_AOCODERow As Row()) As Integer
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
                    For Each r As Row In SC_AOCODERow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update SC_AOCODE Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where EmpNo = @PKEmpNo")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.AOCODE.Updated Then db.AddInParameter(dbcmd, "@AOCODE", DbType.String, r.AOCODE.Value)
                        If r.Name.Updated Then db.AddInParameter(dbcmd, "@Name", DbType.String, r.Name.Value)
                        If r.Status.Updated Then db.AddInParameter(dbcmd, "@Status", DbType.String, r.Status.Value)
                        If r.EmpNo.Updated Then db.AddInParameter(dbcmd, "@EmpNo", DbType.String, r.EmpNo.Value)
                        If r.DataDate.Updated Then db.AddInParameter(dbcmd, "@DataDate", DbType.String, r.DataDate.Value)
                        db.AddInParameter(dbcmd, "@PKEmpNo", DbType.String, IIf(r.LoadFromDataRow, r.EmpNo.OldValue, r.EmpNo.Value))

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

        Public Function Update(ByVal SC_AOCODERow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In SC_AOCODERow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update SC_AOCODE Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where EmpNo = @PKEmpNo")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.AOCODE.Updated Then db.AddInParameter(dbcmd, "@AOCODE", DbType.String, r.AOCODE.Value)
                If r.Name.Updated Then db.AddInParameter(dbcmd, "@Name", DbType.String, r.Name.Value)
                If r.Status.Updated Then db.AddInParameter(dbcmd, "@Status", DbType.String, r.Status.Value)
                If r.EmpNo.Updated Then db.AddInParameter(dbcmd, "@EmpNo", DbType.String, r.EmpNo.Value)
                If r.DataDate.Updated Then db.AddInParameter(dbcmd, "@DataDate", DbType.String, r.DataDate.Value)
                db.AddInParameter(dbcmd, "@PKEmpNo", DbType.String, IIf(r.LoadFromDataRow, r.EmpNo.OldValue, r.EmpNo.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal SC_AOCODERow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_AOCODE")
            strSQL.AppendLine("Where EmpNo = @EmpNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@EmpNo", DbType.String, SC_AOCODERow.EmpNo.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal SC_AOCODERow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_AOCODE")
            strSQL.AppendLine("Where EmpNo = @EmpNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@EmpNo", DbType.String, SC_AOCODERow.EmpNo.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_AOCODE")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal SC_AOCODERow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_AOCODE")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    AOCODE, Name, Status, EmpNo, DataDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @AOCODE, @Name, @Status, @EmpNo, @DataDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@AOCODE", DbType.String, SC_AOCODERow.AOCODE.Value)
            db.AddInParameter(dbcmd, "@Name", DbType.String, SC_AOCODERow.Name.Value)
            db.AddInParameter(dbcmd, "@Status", DbType.String, SC_AOCODERow.Status.Value)
            db.AddInParameter(dbcmd, "@EmpNo", DbType.String, SC_AOCODERow.EmpNo.Value)
            db.AddInParameter(dbcmd, "@DataDate", DbType.String, SC_AOCODERow.DataDate.Value)

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal SC_AOCODERow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_AOCODE")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    AOCODE, Name, Status, EmpNo, DataDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @AOCODE, @Name, @Status, @EmpNo, @DataDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@AOCODE", DbType.String, SC_AOCODERow.AOCODE.Value)
            db.AddInParameter(dbcmd, "@Name", DbType.String, SC_AOCODERow.Name.Value)
            db.AddInParameter(dbcmd, "@Status", DbType.String, SC_AOCODERow.Status.Value)
            db.AddInParameter(dbcmd, "@EmpNo", DbType.String, SC_AOCODERow.EmpNo.Value)
            db.AddInParameter(dbcmd, "@DataDate", DbType.String, SC_AOCODERow.DataDate.Value)

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal SC_AOCODERow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_AOCODE")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    AOCODE, Name, Status, EmpNo, DataDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @AOCODE, @Name, @Status, @EmpNo, @DataDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_AOCODERow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@AOCODE", DbType.String, r.AOCODE.Value)
                        db.AddInParameter(dbcmd, "@Name", DbType.String, r.Name.Value)
                        db.AddInParameter(dbcmd, "@Status", DbType.String, r.Status.Value)
                        db.AddInParameter(dbcmd, "@EmpNo", DbType.String, r.EmpNo.Value)
                        db.AddInParameter(dbcmd, "@DataDate", DbType.String, r.DataDate.Value)

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

        Public Function Insert(ByVal SC_AOCODERow As Row(), ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_AOCODE")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    AOCODE, Name, Status, EmpNo, DataDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @AOCODE, @Name, @Status, @EmpNo, @DataDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_AOCODERow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@AOCODE", DbType.String, r.AOCODE.Value)
                db.AddInParameter(dbcmd, "@Name", DbType.String, r.Name.Value)
                db.AddInParameter(dbcmd, "@Status", DbType.String, r.Status.Value)
                db.AddInParameter(dbcmd, "@EmpNo", DbType.String, r.EmpNo.Value)
                db.AddInParameter(dbcmd, "@DataDate", DbType.String, r.DataDate.Value)

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

