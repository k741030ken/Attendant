'****************************************************************
' Table:SC_Right
' Created Date: 2015.03.12
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beSC_Right
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "RightID", "RightName", "RightEngName", "OrderSeq", "CreateDate", "LastChgComp", "LastChgID", "LastChgDate", "IsCompAll" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(Integer), GetType(Date), GetType(String), GetType(String), GetType(Date), GetType(String) }
        Private m_PrimaryFields As String() = { "RightID" }

        Public ReadOnly Property Rows() As beSC_Right.Rows 
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
        Public Sub Transfer2Row(SC_RightTable As DataTable)
            For Each dr As DataRow In SC_RightTable.Rows
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

                dr(m_Rows(i).RightID.FieldName) = m_Rows(i).RightID.Value
                dr(m_Rows(i).RightName.FieldName) = m_Rows(i).RightName.Value
                dr(m_Rows(i).RightEngName.FieldName) = m_Rows(i).RightEngName.Value
                dr(m_Rows(i).OrderSeq.FieldName) = m_Rows(i).OrderSeq.Value
                dr(m_Rows(i).CreateDate.FieldName) = m_Rows(i).CreateDate.Value
                dr(m_Rows(i).LastChgComp.FieldName) = m_Rows(i).LastChgComp.Value
                dr(m_Rows(i).LastChgID.FieldName) = m_Rows(i).LastChgID.Value
                dr(m_Rows(i).LastChgDate.FieldName) = m_Rows(i).LastChgDate.Value
                dr(m_Rows(i).IsCompAll.FieldName) = m_Rows(i).IsCompAll.Value

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

        Public Sub Add(SC_RightRow As Row)
            m_Rows.Add(SC_RightRow)
        End Sub

        Public Sub Remove(SC_RightRow As Row)
            If m_Rows.IndexOf(SC_RightRow) >= 0 Then
                m_Rows.Remove(SC_RightRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_RightID As Field(Of String) = new Field(Of String)("RightID", true)
        Private FI_RightName As Field(Of String) = new Field(Of String)("RightName", true)
        Private FI_RightEngName As Field(Of String) = new Field(Of String)("RightEngName", true)
        Private FI_OrderSeq As Field(Of Integer) = new Field(Of Integer)("OrderSeq", true)
        Private FI_CreateDate As Field(Of Date) = new Field(Of Date)("CreateDate", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private FI_IsCompAll As Field(Of String) = new Field(Of String)("IsCompAll", true)
        Private m_FieldNames As String() = { "RightID", "RightName", "RightEngName", "OrderSeq", "CreateDate", "LastChgComp", "LastChgID", "LastChgDate", "IsCompAll" }
        Private m_PrimaryFields As String() = { "RightID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "RightID"
                    Return FI_RightID.Value
                Case "RightName"
                    Return FI_RightName.Value
                Case "RightEngName"
                    Return FI_RightEngName.Value
                Case "OrderSeq"
                    Return FI_OrderSeq.Value
                Case "CreateDate"
                    Return FI_CreateDate.Value
                Case "LastChgComp"
                    Return FI_LastChgComp.Value
                Case "LastChgID"
                    Return FI_LastChgID.Value
                Case "LastChgDate"
                    Return FI_LastChgDate.Value
                Case "IsCompAll"
                    Return FI_IsCompAll.Value
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Sub SetFieldValue(ByVal fieldName As String, ByVal value As Object)
            Select Case fieldName
                Case "RightID"
                    FI_RightID.SetValue(value)
                Case "RightName"
                    FI_RightName.SetValue(value)
                Case "RightEngName"
                    FI_RightEngName.SetValue(value)
                Case "OrderSeq"
                    FI_OrderSeq.SetValue(value)
                Case "CreateDate"
                    FI_CreateDate.SetValue(value)
                Case "LastChgComp"
                    FI_LastChgComp.SetValue(value)
                Case "LastChgID"
                    FI_LastChgID.SetValue(value)
                Case "LastChgDate"
                    FI_LastChgDate.SetValue(value)
                Case "IsCompAll"
                    FI_IsCompAll.SetValue(value)
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
                Case "RightID"
                    return FI_RightID.Updated
                Case "RightName"
                    return FI_RightName.Updated
                Case "RightEngName"
                    return FI_RightEngName.Updated
                Case "OrderSeq"
                    return FI_OrderSeq.Updated
                Case "CreateDate"
                    return FI_CreateDate.Updated
                Case "LastChgComp"
                    return FI_LastChgComp.Updated
                Case "LastChgID"
                    return FI_LastChgID.Updated
                Case "LastChgDate"
                    return FI_LastChgDate.Updated
                Case "IsCompAll"
                    return FI_IsCompAll.Updated
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public Function CreateUpdateSQL(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "RightID"
                    return FI_RightID.CreateUpdateSQL
                Case "RightName"
                    return FI_RightName.CreateUpdateSQL
                Case "RightEngName"
                    return FI_RightEngName.CreateUpdateSQL
                Case "OrderSeq"
                    return FI_OrderSeq.CreateUpdateSQL
                Case "CreateDate"
                    return FI_CreateDate.CreateUpdateSQL
                Case "LastChgComp"
                    return FI_LastChgComp.CreateUpdateSQL
                Case "LastChgID"
                    return FI_LastChgID.CreateUpdateSQL
                Case "LastChgDate"
                    return FI_LastChgDate.CreateUpdateSQL
                Case "IsCompAll"
                    return FI_IsCompAll.CreateUpdateSQL
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
            FI_RightID.SetInitValue("")
            FI_RightName.SetInitValue("")
            FI_RightEngName.SetInitValue("")
            FI_OrderSeq.SetInitValue(0)
            FI_CreateDate.SetInitValue(DateTime.Now)
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(DateTime.Now)
            FI_IsCompAll.SetInitValue("")
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_RightID.SetInitValue(dr("RightID"))
            FI_RightName.SetInitValue(dr("RightName"))
            FI_RightEngName.SetInitValue(dr("RightEngName"))
            FI_OrderSeq.SetInitValue(dr("OrderSeq"))
            FI_CreateDate.SetInitValue(dr("CreateDate"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))
            FI_IsCompAll.SetInitValue(dr("IsCompAll"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_RightID.Updated = False
            FI_RightName.Updated = False
            FI_RightEngName.Updated = False
            FI_OrderSeq.Updated = False
            FI_CreateDate.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
            FI_IsCompAll.Updated = False
        End Sub

        Public ReadOnly Property RightID As Field(Of String) 
            Get
                Return FI_RightID
            End Get
        End Property

        Public ReadOnly Property RightName As Field(Of String) 
            Get
                Return FI_RightName
            End Get
        End Property

        Public ReadOnly Property RightEngName As Field(Of String) 
            Get
                Return FI_RightEngName
            End Get
        End Property

        Public ReadOnly Property OrderSeq As Field(Of Integer) 
            Get
                Return FI_OrderSeq
            End Get
        End Property

        Public ReadOnly Property CreateDate As Field(Of Date) 
            Get
                Return FI_CreateDate
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

        Public ReadOnly Property IsCompAll As Field(Of String) 
            Get
                Return FI_IsCompAll
            End Get
        End Property

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal SC_RightRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_Right")
            strSQL.AppendLine("Where RightID = @RightID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@RightID", DbType.String, SC_RightRow.RightID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal SC_RightRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_Right")
            strSQL.AppendLine("Where RightID = @RightID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@RightID", DbType.String, SC_RightRow.RightID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal SC_RightRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_Right")
            strSQL.AppendLine("Where RightID = @RightID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_RightRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@RightID", DbType.String, r.RightID.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal SC_RightRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_Right")
            strSQL.AppendLine("Where RightID = @RightID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_RightRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@RightID", DbType.String, r.RightID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal SC_RightRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_Right")
            strSQL.AppendLine("Where RightID = @RightID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@RightID", DbType.String, SC_RightRow.RightID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(SC_RightRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_Right")
            strSQL.AppendLine("Where RightID = @RightID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@RightID", DbType.String, SC_RightRow.RightID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_RightRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_Right Set")
            For i As Integer = 0 To SC_RightRow.FieldNames.Length - 1
                If Not SC_RightRow.IsIdentityField(SC_RightRow.FieldNames(i)) AndAlso SC_RightRow.IsUpdated(SC_RightRow.FieldNames(i)) AndAlso SC_RightRow.CreateUpdateSQL(SC_RightRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_RightRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where RightID = @PKRightID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_RightRow.RightID.Updated Then db.AddInParameter(dbcmd, "@RightID", DbType.String, SC_RightRow.RightID.Value)
            If SC_RightRow.RightName.Updated Then db.AddInParameter(dbcmd, "@RightName", DbType.String, SC_RightRow.RightName.Value)
            If SC_RightRow.RightEngName.Updated Then db.AddInParameter(dbcmd, "@RightEngName", DbType.String, SC_RightRow.RightEngName.Value)
            If SC_RightRow.OrderSeq.Updated Then db.AddInParameter(dbcmd, "@OrderSeq", DbType.Int32, SC_RightRow.OrderSeq.Value)
            If SC_RightRow.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_RightRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_RightRow.CreateDate.Value))
            If SC_RightRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, SC_RightRow.LastChgComp.Value)
            If SC_RightRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_RightRow.LastChgID.Value)
            If SC_RightRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_RightRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_RightRow.LastChgDate.Value))
            If SC_RightRow.IsCompAll.Updated Then db.AddInParameter(dbcmd, "@IsCompAll", DbType.String, SC_RightRow.IsCompAll.Value)
            db.AddInParameter(dbcmd, "@PKRightID", DbType.String, IIf(SC_RightRow.LoadFromDataRow, SC_RightRow.RightID.OldValue, SC_RightRow.RightID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal SC_RightRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_Right Set")
            For i As Integer = 0 To SC_RightRow.FieldNames.Length - 1
                If Not SC_RightRow.IsIdentityField(SC_RightRow.FieldNames(i)) AndAlso SC_RightRow.IsUpdated(SC_RightRow.FieldNames(i)) AndAlso SC_RightRow.CreateUpdateSQL(SC_RightRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_RightRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where RightID = @PKRightID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_RightRow.RightID.Updated Then db.AddInParameter(dbcmd, "@RightID", DbType.String, SC_RightRow.RightID.Value)
            If SC_RightRow.RightName.Updated Then db.AddInParameter(dbcmd, "@RightName", DbType.String, SC_RightRow.RightName.Value)
            If SC_RightRow.RightEngName.Updated Then db.AddInParameter(dbcmd, "@RightEngName", DbType.String, SC_RightRow.RightEngName.Value)
            If SC_RightRow.OrderSeq.Updated Then db.AddInParameter(dbcmd, "@OrderSeq", DbType.Int32, SC_RightRow.OrderSeq.Value)
            If SC_RightRow.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_RightRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_RightRow.CreateDate.Value))
            If SC_RightRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, SC_RightRow.LastChgComp.Value)
            If SC_RightRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_RightRow.LastChgID.Value)
            If SC_RightRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_RightRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_RightRow.LastChgDate.Value))
            If SC_RightRow.IsCompAll.Updated Then db.AddInParameter(dbcmd, "@IsCompAll", DbType.String, SC_RightRow.IsCompAll.Value)
            db.AddInParameter(dbcmd, "@PKRightID", DbType.String, IIf(SC_RightRow.LoadFromDataRow, SC_RightRow.RightID.OldValue, SC_RightRow.RightID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_RightRow As Row()) As Integer
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
                    For Each r As Row In SC_RightRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update SC_Right Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where RightID = @PKRightID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.RightID.Updated Then db.AddInParameter(dbcmd, "@RightID", DbType.String, r.RightID.Value)
                        If r.RightName.Updated Then db.AddInParameter(dbcmd, "@RightName", DbType.String, r.RightName.Value)
                        If r.RightEngName.Updated Then db.AddInParameter(dbcmd, "@RightEngName", DbType.String, r.RightEngName.Value)
                        If r.OrderSeq.Updated Then db.AddInParameter(dbcmd, "@OrderSeq", DbType.Int32, r.OrderSeq.Value)
                        If r.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        If r.IsCompAll.Updated Then db.AddInParameter(dbcmd, "@IsCompAll", DbType.String, r.IsCompAll.Value)
                        db.AddInParameter(dbcmd, "@PKRightID", DbType.String, IIf(r.LoadFromDataRow, r.RightID.OldValue, r.RightID.Value))

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

        Public Function Update(ByVal SC_RightRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In SC_RightRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update SC_Right Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where RightID = @PKRightID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.RightID.Updated Then db.AddInParameter(dbcmd, "@RightID", DbType.String, r.RightID.Value)
                If r.RightName.Updated Then db.AddInParameter(dbcmd, "@RightName", DbType.String, r.RightName.Value)
                If r.RightEngName.Updated Then db.AddInParameter(dbcmd, "@RightEngName", DbType.String, r.RightEngName.Value)
                If r.OrderSeq.Updated Then db.AddInParameter(dbcmd, "@OrderSeq", DbType.Int32, r.OrderSeq.Value)
                If r.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                If r.IsCompAll.Updated Then db.AddInParameter(dbcmd, "@IsCompAll", DbType.String, r.IsCompAll.Value)
                db.AddInParameter(dbcmd, "@PKRightID", DbType.String, IIf(r.LoadFromDataRow, r.RightID.OldValue, r.RightID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal SC_RightRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_Right")
            strSQL.AppendLine("Where RightID = @RightID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@RightID", DbType.String, SC_RightRow.RightID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal SC_RightRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_Right")
            strSQL.AppendLine("Where RightID = @RightID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@RightID", DbType.String, SC_RightRow.RightID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_Right")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal SC_RightRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_Right")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    RightID, RightName, RightEngName, OrderSeq, CreateDate, LastChgComp, LastChgID, LastChgDate,")
            strSQL.AppendLine("    IsCompAll")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @RightID, @RightName, @RightEngName, @OrderSeq, @CreateDate, @LastChgComp, @LastChgID, @LastChgDate,")
            strSQL.AppendLine("    @IsCompAll")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@RightID", DbType.String, SC_RightRow.RightID.Value)
            db.AddInParameter(dbcmd, "@RightName", DbType.String, SC_RightRow.RightName.Value)
            db.AddInParameter(dbcmd, "@RightEngName", DbType.String, SC_RightRow.RightEngName.Value)
            db.AddInParameter(dbcmd, "@OrderSeq", DbType.Int32, SC_RightRow.OrderSeq.Value)
            db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_RightRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_RightRow.CreateDate.Value))
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, SC_RightRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_RightRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_RightRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_RightRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@IsCompAll", DbType.String, SC_RightRow.IsCompAll.Value)

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal SC_RightRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_Right")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    RightID, RightName, RightEngName, OrderSeq, CreateDate, LastChgComp, LastChgID, LastChgDate,")
            strSQL.AppendLine("    IsCompAll")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @RightID, @RightName, @RightEngName, @OrderSeq, @CreateDate, @LastChgComp, @LastChgID, @LastChgDate,")
            strSQL.AppendLine("    @IsCompAll")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@RightID", DbType.String, SC_RightRow.RightID.Value)
            db.AddInParameter(dbcmd, "@RightName", DbType.String, SC_RightRow.RightName.Value)
            db.AddInParameter(dbcmd, "@RightEngName", DbType.String, SC_RightRow.RightEngName.Value)
            db.AddInParameter(dbcmd, "@OrderSeq", DbType.Int32, SC_RightRow.OrderSeq.Value)
            db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_RightRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_RightRow.CreateDate.Value))
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, SC_RightRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_RightRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_RightRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_RightRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@IsCompAll", DbType.String, SC_RightRow.IsCompAll.Value)

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal SC_RightRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_Right")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    RightID, RightName, RightEngName, OrderSeq, CreateDate, LastChgComp, LastChgID, LastChgDate,")
            strSQL.AppendLine("    IsCompAll")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @RightID, @RightName, @RightEngName, @OrderSeq, @CreateDate, @LastChgComp, @LastChgID, @LastChgDate,")
            strSQL.AppendLine("    @IsCompAll")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_RightRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@RightID", DbType.String, r.RightID.Value)
                        db.AddInParameter(dbcmd, "@RightName", DbType.String, r.RightName.Value)
                        db.AddInParameter(dbcmd, "@RightEngName", DbType.String, r.RightEngName.Value)
                        db.AddInParameter(dbcmd, "@OrderSeq", DbType.Int32, r.OrderSeq.Value)
                        db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                        db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@IsCompAll", DbType.String, r.IsCompAll.Value)

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

        Public Function Insert(ByVal SC_RightRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_Right")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    RightID, RightName, RightEngName, OrderSeq, CreateDate, LastChgComp, LastChgID, LastChgDate,")
            strSQL.AppendLine("    IsCompAll")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @RightID, @RightName, @RightEngName, @OrderSeq, @CreateDate, @LastChgComp, @LastChgID, @LastChgDate,")
            strSQL.AppendLine("    @IsCompAll")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_RightRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@RightID", DbType.String, r.RightID.Value)
                db.AddInParameter(dbcmd, "@RightName", DbType.String, r.RightName.Value)
                db.AddInParameter(dbcmd, "@RightEngName", DbType.String, r.RightEngName.Value)
                db.AddInParameter(dbcmd, "@OrderSeq", DbType.Int32, r.OrderSeq.Value)
                db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@IsCompAll", DbType.String, r.IsCompAll.Value)

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

