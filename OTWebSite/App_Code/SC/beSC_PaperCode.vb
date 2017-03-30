'****************************************************************
' Table:SC_PaperCode
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

Namespace beSC_PaperCode
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "PaperCodeType", "PaperDate", "SeqNo" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(Integer) }
        Private m_PrimaryFields As String() = { "PaperCodeType", "PaperDate" }

        Public ReadOnly Property Rows() As beSC_PaperCode.Rows 
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
        Public Sub Transfer2Row(SC_PaperCodeTable As DataTable)
            For Each dr As DataRow In SC_PaperCodeTable.Rows
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

                dr(m_Rows(i).PaperCodeType.FieldName) = m_Rows(i).PaperCodeType.Value
                dr(m_Rows(i).PaperDate.FieldName) = m_Rows(i).PaperDate.Value
                dr(m_Rows(i).SeqNo.FieldName) = m_Rows(i).SeqNo.Value

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

        Public Sub Add(SC_PaperCodeRow As Row)
            m_Rows.Add(SC_PaperCodeRow)
        End Sub

        Public Sub Remove(SC_PaperCodeRow As Row)
            If m_Rows.IndexOf(SC_PaperCodeRow) >= 0 Then
                m_Rows.Remove(SC_PaperCodeRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_PaperCodeType As Field(Of String) = new Field(Of String)("PaperCodeType", true)
        Private FI_PaperDate As Field(Of String) = new Field(Of String)("PaperDate", true)
        Private FI_SeqNo As Field(Of Integer) = new Field(Of Integer)("SeqNo", true)
        Private m_FieldNames As String() = { "PaperCodeType", "PaperDate", "SeqNo" }
        Private m_PrimaryFields As String() = { "PaperCodeType", "PaperDate" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "PaperCodeType"
                    Return FI_PaperCodeType.Value
                Case "PaperDate"
                    Return FI_PaperDate.Value
                Case "SeqNo"
                    Return FI_SeqNo.Value
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Sub SetFieldValue(ByVal fieldName As String, ByVal value As Object)
            Select Case fieldName
                Case "PaperCodeType"
                    FI_PaperCodeType.SetValue(value)
                Case "PaperDate"
                    FI_PaperDate.SetValue(value)
                Case "SeqNo"
                    FI_SeqNo.SetValue(value)
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
                Case "PaperCodeType"
                    return FI_PaperCodeType.Updated
                Case "PaperDate"
                    return FI_PaperDate.Updated
                Case "SeqNo"
                    return FI_SeqNo.Updated
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public Function CreateUpdateSQL(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "PaperCodeType"
                    return FI_PaperCodeType.CreateUpdateSQL
                Case "PaperDate"
                    return FI_PaperDate.CreateUpdateSQL
                Case "SeqNo"
                    return FI_SeqNo.CreateUpdateSQL
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
            FI_PaperCodeType.SetInitValue("")
            FI_PaperDate.SetInitValue("")
            FI_SeqNo.SetInitValue(0)
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_PaperCodeType.SetInitValue(dr("PaperCodeType"))
            FI_PaperDate.SetInitValue(dr("PaperDate"))
            FI_SeqNo.SetInitValue(dr("SeqNo"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_PaperCodeType.Updated = False
            FI_PaperDate.Updated = False
            FI_SeqNo.Updated = False
        End Sub

        Public ReadOnly Property PaperCodeType As Field(Of String) 
            Get
                Return FI_PaperCodeType
            End Get
        End Property

        Public ReadOnly Property PaperDate As Field(Of String) 
            Get
                Return FI_PaperDate
            End Get
        End Property

        Public ReadOnly Property SeqNo As Field(Of Integer) 
            Get
                Return FI_SeqNo
            End Get
        End Property

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal SC_PaperCodeRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_PaperCode")
            strSQL.AppendLine("Where PaperCodeType = @PaperCodeType")
            strSQL.AppendLine("And PaperDate = @PaperDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@PaperCodeType", DbType.String, SC_PaperCodeRow.PaperCodeType.Value)
            db.AddInParameter(dbcmd, "@PaperDate", DbType.String, SC_PaperCodeRow.PaperDate.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal SC_PaperCodeRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_PaperCode")
            strSQL.AppendLine("Where PaperCodeType = @PaperCodeType")
            strSQL.AppendLine("And PaperDate = @PaperDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@PaperCodeType", DbType.String, SC_PaperCodeRow.PaperCodeType.Value)
            db.AddInParameter(dbcmd, "@PaperDate", DbType.String, SC_PaperCodeRow.PaperDate.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal SC_PaperCodeRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_PaperCode")
            strSQL.AppendLine("Where PaperCodeType = @PaperCodeType")
            strSQL.AppendLine("And PaperDate = @PaperDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_PaperCodeRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@PaperCodeType", DbType.String, r.PaperCodeType.Value)
                        db.AddInParameter(dbcmd, "@PaperDate", DbType.String, r.PaperDate.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal SC_PaperCodeRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_PaperCode")
            strSQL.AppendLine("Where PaperCodeType = @PaperCodeType")
            strSQL.AppendLine("And PaperDate = @PaperDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_PaperCodeRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@PaperCodeType", DbType.String, r.PaperCodeType.Value)
                db.AddInParameter(dbcmd, "@PaperDate", DbType.String, r.PaperDate.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal SC_PaperCodeRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_PaperCode")
            strSQL.AppendLine("Where PaperCodeType = @PaperCodeType")
            strSQL.AppendLine("And PaperDate = @PaperDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@PaperCodeType", DbType.String, SC_PaperCodeRow.PaperCodeType.Value)
            db.AddInParameter(dbcmd, "@PaperDate", DbType.String, SC_PaperCodeRow.PaperDate.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(SC_PaperCodeRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_PaperCode")
            strSQL.AppendLine("Where PaperCodeType = @PaperCodeType")
            strSQL.AppendLine("And PaperDate = @PaperDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@PaperCodeType", DbType.String, SC_PaperCodeRow.PaperCodeType.Value)
            db.AddInParameter(dbcmd, "@PaperDate", DbType.String, SC_PaperCodeRow.PaperDate.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_PaperCodeRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_PaperCode Set")
            For i As Integer = 0 To SC_PaperCodeRow.FieldNames.Length - 1
                If Not SC_PaperCodeRow.IsIdentityField(SC_PaperCodeRow.FieldNames(i)) AndAlso SC_PaperCodeRow.IsUpdated(SC_PaperCodeRow.FieldNames(i)) AndAlso SC_PaperCodeRow.CreateUpdateSQL(SC_PaperCodeRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_PaperCodeRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where PaperCodeType = @PKPaperCodeType")
            strSQL.AppendLine("And PaperDate = @PKPaperDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_PaperCodeRow.PaperCodeType.Updated Then db.AddInParameter(dbcmd, "@PaperCodeType", DbType.String, SC_PaperCodeRow.PaperCodeType.Value)
            If SC_PaperCodeRow.PaperDate.Updated Then db.AddInParameter(dbcmd, "@PaperDate", DbType.String, SC_PaperCodeRow.PaperDate.Value)
            If SC_PaperCodeRow.SeqNo.Updated Then db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, SC_PaperCodeRow.SeqNo.Value)
            db.AddInParameter(dbcmd, "@PKPaperCodeType", DbType.String, IIf(SC_PaperCodeRow.LoadFromDataRow, SC_PaperCodeRow.PaperCodeType.OldValue, SC_PaperCodeRow.PaperCodeType.Value))
            db.AddInParameter(dbcmd, "@PKPaperDate", DbType.String, IIf(SC_PaperCodeRow.LoadFromDataRow, SC_PaperCodeRow.PaperDate.OldValue, SC_PaperCodeRow.PaperDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal SC_PaperCodeRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_PaperCode Set")
            For i As Integer = 0 To SC_PaperCodeRow.FieldNames.Length - 1
                If Not SC_PaperCodeRow.IsIdentityField(SC_PaperCodeRow.FieldNames(i)) AndAlso SC_PaperCodeRow.IsUpdated(SC_PaperCodeRow.FieldNames(i)) AndAlso SC_PaperCodeRow.CreateUpdateSQL(SC_PaperCodeRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_PaperCodeRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where PaperCodeType = @PKPaperCodeType")
            strSQL.AppendLine("And PaperDate = @PKPaperDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_PaperCodeRow.PaperCodeType.Updated Then db.AddInParameter(dbcmd, "@PaperCodeType", DbType.String, SC_PaperCodeRow.PaperCodeType.Value)
            If SC_PaperCodeRow.PaperDate.Updated Then db.AddInParameter(dbcmd, "@PaperDate", DbType.String, SC_PaperCodeRow.PaperDate.Value)
            If SC_PaperCodeRow.SeqNo.Updated Then db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, SC_PaperCodeRow.SeqNo.Value)
            db.AddInParameter(dbcmd, "@PKPaperCodeType", DbType.String, IIf(SC_PaperCodeRow.LoadFromDataRow, SC_PaperCodeRow.PaperCodeType.OldValue, SC_PaperCodeRow.PaperCodeType.Value))
            db.AddInParameter(dbcmd, "@PKPaperDate", DbType.String, IIf(SC_PaperCodeRow.LoadFromDataRow, SC_PaperCodeRow.PaperDate.OldValue, SC_PaperCodeRow.PaperDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_PaperCodeRow As Row()) As Integer
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
                    For Each r As Row In SC_PaperCodeRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update SC_PaperCode Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where PaperCodeType = @PKPaperCodeType")
                        strSQL.AppendLine("And PaperDate = @PKPaperDate")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.PaperCodeType.Updated Then db.AddInParameter(dbcmd, "@PaperCodeType", DbType.String, r.PaperCodeType.Value)
                        If r.PaperDate.Updated Then db.AddInParameter(dbcmd, "@PaperDate", DbType.String, r.PaperDate.Value)
                        If r.SeqNo.Updated Then db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, r.SeqNo.Value)
                        db.AddInParameter(dbcmd, "@PKPaperCodeType", DbType.String, IIf(r.LoadFromDataRow, r.PaperCodeType.OldValue, r.PaperCodeType.Value))
                        db.AddInParameter(dbcmd, "@PKPaperDate", DbType.String, IIf(r.LoadFromDataRow, r.PaperDate.OldValue, r.PaperDate.Value))

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

        Public Function Update(ByVal SC_PaperCodeRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In SC_PaperCodeRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update SC_PaperCode Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where PaperCodeType = @PKPaperCodeType")
                strSQL.AppendLine("And PaperDate = @PKPaperDate")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.PaperCodeType.Updated Then db.AddInParameter(dbcmd, "@PaperCodeType", DbType.String, r.PaperCodeType.Value)
                If r.PaperDate.Updated Then db.AddInParameter(dbcmd, "@PaperDate", DbType.String, r.PaperDate.Value)
                If r.SeqNo.Updated Then db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, r.SeqNo.Value)
                db.AddInParameter(dbcmd, "@PKPaperCodeType", DbType.String, IIf(r.LoadFromDataRow, r.PaperCodeType.OldValue, r.PaperCodeType.Value))
                db.AddInParameter(dbcmd, "@PKPaperDate", DbType.String, IIf(r.LoadFromDataRow, r.PaperDate.OldValue, r.PaperDate.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal SC_PaperCodeRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_PaperCode")
            strSQL.AppendLine("Where PaperCodeType = @PaperCodeType")
            strSQL.AppendLine("And PaperDate = @PaperDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@PaperCodeType", DbType.String, SC_PaperCodeRow.PaperCodeType.Value)
            db.AddInParameter(dbcmd, "@PaperDate", DbType.String, SC_PaperCodeRow.PaperDate.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal SC_PaperCodeRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_PaperCode")
            strSQL.AppendLine("Where PaperCodeType = @PaperCodeType")
            strSQL.AppendLine("And PaperDate = @PaperDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@PaperCodeType", DbType.String, SC_PaperCodeRow.PaperCodeType.Value)
            db.AddInParameter(dbcmd, "@PaperDate", DbType.String, SC_PaperCodeRow.PaperDate.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_PaperCode")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal SC_PaperCodeRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_PaperCode")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    PaperCodeType, PaperDate, SeqNo")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @PaperCodeType, @PaperDate, @SeqNo")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@PaperCodeType", DbType.String, SC_PaperCodeRow.PaperCodeType.Value)
            db.AddInParameter(dbcmd, "@PaperDate", DbType.String, SC_PaperCodeRow.PaperDate.Value)
            db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, SC_PaperCodeRow.SeqNo.Value)

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal SC_PaperCodeRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_PaperCode")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    PaperCodeType, PaperDate, SeqNo")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @PaperCodeType, @PaperDate, @SeqNo")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@PaperCodeType", DbType.String, SC_PaperCodeRow.PaperCodeType.Value)
            db.AddInParameter(dbcmd, "@PaperDate", DbType.String, SC_PaperCodeRow.PaperDate.Value)
            db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, SC_PaperCodeRow.SeqNo.Value)

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal SC_PaperCodeRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_PaperCode")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    PaperCodeType, PaperDate, SeqNo")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @PaperCodeType, @PaperDate, @SeqNo")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_PaperCodeRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@PaperCodeType", DbType.String, r.PaperCodeType.Value)
                        db.AddInParameter(dbcmd, "@PaperDate", DbType.String, r.PaperDate.Value)
                        db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, r.SeqNo.Value)

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

        Public Function Insert(ByVal SC_PaperCodeRow As Row(), ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_PaperCode")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    PaperCodeType, PaperDate, SeqNo")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @PaperCodeType, @PaperDate, @SeqNo")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_PaperCodeRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@PaperCodeType", DbType.String, r.PaperCodeType.Value)
                db.AddInParameter(dbcmd, "@PaperDate", DbType.String, r.PaperDate.Value)
                db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, r.SeqNo.Value)

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

