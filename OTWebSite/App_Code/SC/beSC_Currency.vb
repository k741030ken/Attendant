'****************************************************************
' Table:SC_Currency
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

Namespace beSC_Currency
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CurrencyID", "CurrencyNo", "CurrencyName", "MDFlag_USD", "MDFlag_CNY", "CreateDate", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "CurrencyID" }

        Public ReadOnly Property Rows() As beSC_Currency.Rows 
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
        Public Sub Transfer2Row(SC_CurrencyTable As DataTable)
            For Each dr As DataRow In SC_CurrencyTable.Rows
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

                dr(m_Rows(i).CurrencyID.FieldName) = m_Rows(i).CurrencyID.Value
                dr(m_Rows(i).CurrencyNo.FieldName) = m_Rows(i).CurrencyNo.Value
                dr(m_Rows(i).CurrencyName.FieldName) = m_Rows(i).CurrencyName.Value
                dr(m_Rows(i).MDFlag_USD.FieldName) = m_Rows(i).MDFlag_USD.Value
                dr(m_Rows(i).MDFlag_CNY.FieldName) = m_Rows(i).MDFlag_CNY.Value
                dr(m_Rows(i).CreateDate.FieldName) = m_Rows(i).CreateDate.Value
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

        Public Sub Add(SC_CurrencyRow As Row)
            m_Rows.Add(SC_CurrencyRow)
        End Sub

        Public Sub Remove(SC_CurrencyRow As Row)
            If m_Rows.IndexOf(SC_CurrencyRow) >= 0 Then
                m_Rows.Remove(SC_CurrencyRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CurrencyID As Field(Of String) = new Field(Of String)("CurrencyID", true)
        Private FI_CurrencyNo As Field(Of String) = new Field(Of String)("CurrencyNo", true)
        Private FI_CurrencyName As Field(Of String) = new Field(Of String)("CurrencyName", true)
        Private FI_MDFlag_USD As Field(Of String) = new Field(Of String)("MDFlag_USD", true)
        Private FI_MDFlag_CNY As Field(Of String) = new Field(Of String)("MDFlag_CNY", true)
        Private FI_CreateDate As Field(Of Date) = new Field(Of Date)("CreateDate", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "CurrencyID", "CurrencyNo", "CurrencyName", "MDFlag_USD", "MDFlag_CNY", "CreateDate", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "CurrencyID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CurrencyID"
                    Return FI_CurrencyID.Value
                Case "CurrencyNo"
                    Return FI_CurrencyNo.Value
                Case "CurrencyName"
                    Return FI_CurrencyName.Value
                Case "MDFlag_USD"
                    Return FI_MDFlag_USD.Value
                Case "MDFlag_CNY"
                    Return FI_MDFlag_CNY.Value
                Case "CreateDate"
                    Return FI_CreateDate.Value
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
                Case "CurrencyID"
                    FI_CurrencyID.SetValue(value)
                Case "CurrencyNo"
                    FI_CurrencyNo.SetValue(value)
                Case "CurrencyName"
                    FI_CurrencyName.SetValue(value)
                Case "MDFlag_USD"
                    FI_MDFlag_USD.SetValue(value)
                Case "MDFlag_CNY"
                    FI_MDFlag_CNY.SetValue(value)
                Case "CreateDate"
                    FI_CreateDate.SetValue(value)
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
                Case "CurrencyID"
                    return FI_CurrencyID.Updated
                Case "CurrencyNo"
                    return FI_CurrencyNo.Updated
                Case "CurrencyName"
                    return FI_CurrencyName.Updated
                Case "MDFlag_USD"
                    return FI_MDFlag_USD.Updated
                Case "MDFlag_CNY"
                    return FI_MDFlag_CNY.Updated
                Case "CreateDate"
                    return FI_CreateDate.Updated
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
                Case "CurrencyID"
                    return FI_CurrencyID.CreateUpdateSQL
                Case "CurrencyNo"
                    return FI_CurrencyNo.CreateUpdateSQL
                Case "CurrencyName"
                    return FI_CurrencyName.CreateUpdateSQL
                Case "MDFlag_USD"
                    return FI_MDFlag_USD.CreateUpdateSQL
                Case "MDFlag_CNY"
                    return FI_MDFlag_CNY.CreateUpdateSQL
                Case "CreateDate"
                    return FI_CreateDate.CreateUpdateSQL
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
            FI_CurrencyID.SetInitValue("")
            FI_CurrencyNo.SetInitValue("")
            FI_CurrencyName.SetInitValue("")
            FI_CreateDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CurrencyID.SetInitValue(dr("CurrencyID"))
            FI_CurrencyNo.SetInitValue(dr("CurrencyNo"))
            FI_CurrencyName.SetInitValue(dr("CurrencyName"))
            FI_MDFlag_USD.SetInitValue(dr("MDFlag_USD"))
            FI_MDFlag_CNY.SetInitValue(dr("MDFlag_CNY"))
            FI_CreateDate.SetInitValue(dr("CreateDate"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CurrencyID.Updated = False
            FI_CurrencyNo.Updated = False
            FI_CurrencyName.Updated = False
            FI_MDFlag_USD.Updated = False
            FI_MDFlag_CNY.Updated = False
            FI_CreateDate.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
        End Sub

        Public ReadOnly Property CurrencyID As Field(Of String) 
            Get
                Return FI_CurrencyID
            End Get
        End Property

        Public ReadOnly Property CurrencyNo As Field(Of String) 
            Get
                Return FI_CurrencyNo
            End Get
        End Property

        Public ReadOnly Property CurrencyName As Field(Of String) 
            Get
                Return FI_CurrencyName
            End Get
        End Property

        Public ReadOnly Property MDFlag_USD As Field(Of String) 
            Get
                Return FI_MDFlag_USD
            End Get
        End Property

        Public ReadOnly Property MDFlag_CNY As Field(Of String) 
            Get
                Return FI_MDFlag_CNY
            End Get
        End Property

        Public ReadOnly Property CreateDate As Field(Of Date) 
            Get
                Return FI_CreateDate
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
        Public Function DeleteRowByPrimaryKey(ByVal SC_CurrencyRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_Currency")
            strSQL.AppendLine("Where CurrencyID = @CurrencyID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CurrencyID", DbType.String, SC_CurrencyRow.CurrencyID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal SC_CurrencyRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_Currency")
            strSQL.AppendLine("Where CurrencyID = @CurrencyID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CurrencyID", DbType.String, SC_CurrencyRow.CurrencyID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal SC_CurrencyRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_Currency")
            strSQL.AppendLine("Where CurrencyID = @CurrencyID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_CurrencyRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CurrencyID", DbType.String, r.CurrencyID.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal SC_CurrencyRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_Currency")
            strSQL.AppendLine("Where CurrencyID = @CurrencyID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_CurrencyRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CurrencyID", DbType.String, r.CurrencyID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal SC_CurrencyRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_Currency")
            strSQL.AppendLine("Where CurrencyID = @CurrencyID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CurrencyID", DbType.String, SC_CurrencyRow.CurrencyID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(SC_CurrencyRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_Currency")
            strSQL.AppendLine("Where CurrencyID = @CurrencyID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CurrencyID", DbType.String, SC_CurrencyRow.CurrencyID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_CurrencyRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_Currency Set")
            For i As Integer = 0 To SC_CurrencyRow.FieldNames.Length - 1
                If Not SC_CurrencyRow.IsIdentityField(SC_CurrencyRow.FieldNames(i)) AndAlso SC_CurrencyRow.IsUpdated(SC_CurrencyRow.FieldNames(i)) AndAlso SC_CurrencyRow.CreateUpdateSQL(SC_CurrencyRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_CurrencyRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CurrencyID = @PKCurrencyID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_CurrencyRow.CurrencyID.Updated Then db.AddInParameter(dbcmd, "@CurrencyID", DbType.String, SC_CurrencyRow.CurrencyID.Value)
            If SC_CurrencyRow.CurrencyNo.Updated Then db.AddInParameter(dbcmd, "@CurrencyNo", DbType.String, SC_CurrencyRow.CurrencyNo.Value)
            If SC_CurrencyRow.CurrencyName.Updated Then db.AddInParameter(dbcmd, "@CurrencyName", DbType.String, SC_CurrencyRow.CurrencyName.Value)
            If SC_CurrencyRow.MDFlag_USD.Updated Then db.AddInParameter(dbcmd, "@MDFlag_USD", DbType.String, SC_CurrencyRow.MDFlag_USD.Value)
            If SC_CurrencyRow.MDFlag_CNY.Updated Then db.AddInParameter(dbcmd, "@MDFlag_CNY", DbType.String, SC_CurrencyRow.MDFlag_CNY.Value)
            If SC_CurrencyRow.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_CurrencyRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_CurrencyRow.CreateDate.Value))
            If SC_CurrencyRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_CurrencyRow.LastChgID.Value)
            If SC_CurrencyRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_CurrencyRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_CurrencyRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCurrencyID", DbType.String, IIf(SC_CurrencyRow.LoadFromDataRow, SC_CurrencyRow.CurrencyID.OldValue, SC_CurrencyRow.CurrencyID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal SC_CurrencyRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_Currency Set")
            For i As Integer = 0 To SC_CurrencyRow.FieldNames.Length - 1
                If Not SC_CurrencyRow.IsIdentityField(SC_CurrencyRow.FieldNames(i)) AndAlso SC_CurrencyRow.IsUpdated(SC_CurrencyRow.FieldNames(i)) AndAlso SC_CurrencyRow.CreateUpdateSQL(SC_CurrencyRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_CurrencyRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CurrencyID = @PKCurrencyID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_CurrencyRow.CurrencyID.Updated Then db.AddInParameter(dbcmd, "@CurrencyID", DbType.String, SC_CurrencyRow.CurrencyID.Value)
            If SC_CurrencyRow.CurrencyNo.Updated Then db.AddInParameter(dbcmd, "@CurrencyNo", DbType.String, SC_CurrencyRow.CurrencyNo.Value)
            If SC_CurrencyRow.CurrencyName.Updated Then db.AddInParameter(dbcmd, "@CurrencyName", DbType.String, SC_CurrencyRow.CurrencyName.Value)
            If SC_CurrencyRow.MDFlag_USD.Updated Then db.AddInParameter(dbcmd, "@MDFlag_USD", DbType.String, SC_CurrencyRow.MDFlag_USD.Value)
            If SC_CurrencyRow.MDFlag_CNY.Updated Then db.AddInParameter(dbcmd, "@MDFlag_CNY", DbType.String, SC_CurrencyRow.MDFlag_CNY.Value)
            If SC_CurrencyRow.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_CurrencyRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_CurrencyRow.CreateDate.Value))
            If SC_CurrencyRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_CurrencyRow.LastChgID.Value)
            If SC_CurrencyRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_CurrencyRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_CurrencyRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCurrencyID", DbType.String, IIf(SC_CurrencyRow.LoadFromDataRow, SC_CurrencyRow.CurrencyID.OldValue, SC_CurrencyRow.CurrencyID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_CurrencyRow As Row()) As Integer
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
                    For Each r As Row In SC_CurrencyRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update SC_Currency Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where CurrencyID = @PKCurrencyID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.CurrencyID.Updated Then db.AddInParameter(dbcmd, "@CurrencyID", DbType.String, r.CurrencyID.Value)
                        If r.CurrencyNo.Updated Then db.AddInParameter(dbcmd, "@CurrencyNo", DbType.String, r.CurrencyNo.Value)
                        If r.CurrencyName.Updated Then db.AddInParameter(dbcmd, "@CurrencyName", DbType.String, r.CurrencyName.Value)
                        If r.MDFlag_USD.Updated Then db.AddInParameter(dbcmd, "@MDFlag_USD", DbType.String, r.MDFlag_USD.Value)
                        If r.MDFlag_CNY.Updated Then db.AddInParameter(dbcmd, "@MDFlag_CNY", DbType.String, r.MDFlag_CNY.Value)
                        If r.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKCurrencyID", DbType.String, IIf(r.LoadFromDataRow, r.CurrencyID.OldValue, r.CurrencyID.Value))

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

        Public Function Update(ByVal SC_CurrencyRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In SC_CurrencyRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update SC_Currency Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where CurrencyID = @PKCurrencyID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.CurrencyID.Updated Then db.AddInParameter(dbcmd, "@CurrencyID", DbType.String, r.CurrencyID.Value)
                If r.CurrencyNo.Updated Then db.AddInParameter(dbcmd, "@CurrencyNo", DbType.String, r.CurrencyNo.Value)
                If r.CurrencyName.Updated Then db.AddInParameter(dbcmd, "@CurrencyName", DbType.String, r.CurrencyName.Value)
                If r.MDFlag_USD.Updated Then db.AddInParameter(dbcmd, "@MDFlag_USD", DbType.String, r.MDFlag_USD.Value)
                If r.MDFlag_CNY.Updated Then db.AddInParameter(dbcmd, "@MDFlag_CNY", DbType.String, r.MDFlag_CNY.Value)
                If r.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKCurrencyID", DbType.String, IIf(r.LoadFromDataRow, r.CurrencyID.OldValue, r.CurrencyID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal SC_CurrencyRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_Currency")
            strSQL.AppendLine("Where CurrencyID = @CurrencyID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CurrencyID", DbType.String, SC_CurrencyRow.CurrencyID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal SC_CurrencyRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_Currency")
            strSQL.AppendLine("Where CurrencyID = @CurrencyID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CurrencyID", DbType.String, SC_CurrencyRow.CurrencyID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_Currency")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal SC_CurrencyRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_Currency")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CurrencyID, CurrencyNo, CurrencyName, MDFlag_USD, MDFlag_CNY, CreateDate, LastChgID,")
            strSQL.AppendLine("    LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CurrencyID, @CurrencyNo, @CurrencyName, @MDFlag_USD, @MDFlag_CNY, @CreateDate, @LastChgID,")
            strSQL.AppendLine("    @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CurrencyID", DbType.String, SC_CurrencyRow.CurrencyID.Value)
            db.AddInParameter(dbcmd, "@CurrencyNo", DbType.String, SC_CurrencyRow.CurrencyNo.Value)
            db.AddInParameter(dbcmd, "@CurrencyName", DbType.String, SC_CurrencyRow.CurrencyName.Value)
            db.AddInParameter(dbcmd, "@MDFlag_USD", DbType.String, SC_CurrencyRow.MDFlag_USD.Value)
            db.AddInParameter(dbcmd, "@MDFlag_CNY", DbType.String, SC_CurrencyRow.MDFlag_CNY.Value)
            db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_CurrencyRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_CurrencyRow.CreateDate.Value))
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_CurrencyRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_CurrencyRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_CurrencyRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal SC_CurrencyRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_Currency")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CurrencyID, CurrencyNo, CurrencyName, MDFlag_USD, MDFlag_CNY, CreateDate, LastChgID,")
            strSQL.AppendLine("    LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CurrencyID, @CurrencyNo, @CurrencyName, @MDFlag_USD, @MDFlag_CNY, @CreateDate, @LastChgID,")
            strSQL.AppendLine("    @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CurrencyID", DbType.String, SC_CurrencyRow.CurrencyID.Value)
            db.AddInParameter(dbcmd, "@CurrencyNo", DbType.String, SC_CurrencyRow.CurrencyNo.Value)
            db.AddInParameter(dbcmd, "@CurrencyName", DbType.String, SC_CurrencyRow.CurrencyName.Value)
            db.AddInParameter(dbcmd, "@MDFlag_USD", DbType.String, SC_CurrencyRow.MDFlag_USD.Value)
            db.AddInParameter(dbcmd, "@MDFlag_CNY", DbType.String, SC_CurrencyRow.MDFlag_CNY.Value)
            db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_CurrencyRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_CurrencyRow.CreateDate.Value))
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_CurrencyRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_CurrencyRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_CurrencyRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal SC_CurrencyRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_Currency")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CurrencyID, CurrencyNo, CurrencyName, MDFlag_USD, MDFlag_CNY, CreateDate, LastChgID,")
            strSQL.AppendLine("    LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CurrencyID, @CurrencyNo, @CurrencyName, @MDFlag_USD, @MDFlag_CNY, @CreateDate, @LastChgID,")
            strSQL.AppendLine("    @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_CurrencyRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CurrencyID", DbType.String, r.CurrencyID.Value)
                        db.AddInParameter(dbcmd, "@CurrencyNo", DbType.String, r.CurrencyNo.Value)
                        db.AddInParameter(dbcmd, "@CurrencyName", DbType.String, r.CurrencyName.Value)
                        db.AddInParameter(dbcmd, "@MDFlag_USD", DbType.String, r.MDFlag_USD.Value)
                        db.AddInParameter(dbcmd, "@MDFlag_CNY", DbType.String, r.MDFlag_CNY.Value)
                        db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
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

        Public Function Insert(ByVal SC_CurrencyRow As Row(), ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_Currency")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CurrencyID, CurrencyNo, CurrencyName, MDFlag_USD, MDFlag_CNY, CreateDate, LastChgID,")
            strSQL.AppendLine("    LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CurrencyID, @CurrencyNo, @CurrencyName, @MDFlag_USD, @MDFlag_CNY, @CreateDate, @LastChgID,")
            strSQL.AppendLine("    @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_CurrencyRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CurrencyID", DbType.String, r.CurrencyID.Value)
                db.AddInParameter(dbcmd, "@CurrencyNo", DbType.String, r.CurrencyNo.Value)
                db.AddInParameter(dbcmd, "@CurrencyName", DbType.String, r.CurrencyName.Value)
                db.AddInParameter(dbcmd, "@MDFlag_USD", DbType.String, r.MDFlag_USD.Value)
                db.AddInParameter(dbcmd, "@MDFlag_CNY", DbType.String, r.MDFlag_CNY.Value)
                db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
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

