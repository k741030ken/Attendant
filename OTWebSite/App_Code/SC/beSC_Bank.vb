'****************************************************************
' Table:SC_Bank
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

Namespace beSC_Bank
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "BankID", "BankName", "WorldRank", "MoodyGrade", "CreateDate", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "BankID" }

        Public ReadOnly Property Rows() As beSC_Bank.Rows 
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
        Public Sub Transfer2Row(SC_BankTable As DataTable)
            For Each dr As DataRow In SC_BankTable.Rows
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

                dr(m_Rows(i).BankID.FieldName) = m_Rows(i).BankID.Value
                dr(m_Rows(i).BankName.FieldName) = m_Rows(i).BankName.Value
                dr(m_Rows(i).WorldRank.FieldName) = m_Rows(i).WorldRank.Value
                dr(m_Rows(i).MoodyGrade.FieldName) = m_Rows(i).MoodyGrade.Value
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

        Public Sub Add(SC_BankRow As Row)
            m_Rows.Add(SC_BankRow)
        End Sub

        Public Sub Remove(SC_BankRow As Row)
            If m_Rows.IndexOf(SC_BankRow) >= 0 Then
                m_Rows.Remove(SC_BankRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_BankID As Field(Of String) = new Field(Of String)("BankID", true)
        Private FI_BankName As Field(Of String) = new Field(Of String)("BankName", true)
        Private FI_WorldRank As Field(Of String) = new Field(Of String)("WorldRank", true)
        Private FI_MoodyGrade As Field(Of String) = new Field(Of String)("MoodyGrade", true)
        Private FI_CreateDate As Field(Of Date) = new Field(Of Date)("CreateDate", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "BankID", "BankName", "WorldRank", "MoodyGrade", "CreateDate", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "BankID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "BankID"
                    Return FI_BankID.Value
                Case "BankName"
                    Return FI_BankName.Value
                Case "WorldRank"
                    Return FI_WorldRank.Value
                Case "MoodyGrade"
                    Return FI_MoodyGrade.Value
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
                Case "BankID"
                    FI_BankID.SetValue(value)
                Case "BankName"
                    FI_BankName.SetValue(value)
                Case "WorldRank"
                    FI_WorldRank.SetValue(value)
                Case "MoodyGrade"
                    FI_MoodyGrade.SetValue(value)
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
                Case "BankID"
                    return FI_BankID.Updated
                Case "BankName"
                    return FI_BankName.Updated
                Case "WorldRank"
                    return FI_WorldRank.Updated
                Case "MoodyGrade"
                    return FI_MoodyGrade.Updated
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
                Case "BankID"
                    return FI_BankID.CreateUpdateSQL
                Case "BankName"
                    return FI_BankName.CreateUpdateSQL
                Case "WorldRank"
                    return FI_WorldRank.CreateUpdateSQL
                Case "MoodyGrade"
                    return FI_MoodyGrade.CreateUpdateSQL
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
            FI_BankID.SetInitValue("")
            FI_BankName.SetInitValue("")
            FI_WorldRank.SetInitValue("")
            FI_MoodyGrade.SetInitValue("")
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_BankID.SetInitValue(dr("BankID"))
            FI_BankName.SetInitValue(dr("BankName"))
            FI_WorldRank.SetInitValue(dr("WorldRank"))
            FI_MoodyGrade.SetInitValue(dr("MoodyGrade"))
            FI_CreateDate.SetInitValue(dr("CreateDate"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_BankID.Updated = False
            FI_BankName.Updated = False
            FI_WorldRank.Updated = False
            FI_MoodyGrade.Updated = False
            FI_CreateDate.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
        End Sub

        Public ReadOnly Property BankID As Field(Of String) 
            Get
                Return FI_BankID
            End Get
        End Property

        Public ReadOnly Property BankName As Field(Of String) 
            Get
                Return FI_BankName
            End Get
        End Property

        Public ReadOnly Property WorldRank As Field(Of String) 
            Get
                Return FI_WorldRank
            End Get
        End Property

        Public ReadOnly Property MoodyGrade As Field(Of String) 
            Get
                Return FI_MoodyGrade
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
        Public Function DeleteRowByPrimaryKey(ByVal SC_BankRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_Bank")
            strSQL.AppendLine("Where BankID = @BankID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@BankID", DbType.String, SC_BankRow.BankID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal SC_BankRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_Bank")
            strSQL.AppendLine("Where BankID = @BankID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@BankID", DbType.String, SC_BankRow.BankID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal SC_BankRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_Bank")
            strSQL.AppendLine("Where BankID = @BankID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_BankRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@BankID", DbType.String, r.BankID.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal SC_BankRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_Bank")
            strSQL.AppendLine("Where BankID = @BankID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_BankRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@BankID", DbType.String, r.BankID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal SC_BankRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_Bank")
            strSQL.AppendLine("Where BankID = @BankID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@BankID", DbType.String, SC_BankRow.BankID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(SC_BankRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_Bank")
            strSQL.AppendLine("Where BankID = @BankID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@BankID", DbType.String, SC_BankRow.BankID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_BankRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_Bank Set")
            For i As Integer = 0 To SC_BankRow.FieldNames.Length - 1
                If Not SC_BankRow.IsIdentityField(SC_BankRow.FieldNames(i)) AndAlso SC_BankRow.IsUpdated(SC_BankRow.FieldNames(i)) AndAlso SC_BankRow.CreateUpdateSQL(SC_BankRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_BankRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where BankID = @PKBankID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_BankRow.BankID.Updated Then db.AddInParameter(dbcmd, "@BankID", DbType.String, SC_BankRow.BankID.Value)
            If SC_BankRow.BankName.Updated Then db.AddInParameter(dbcmd, "@BankName", DbType.String, SC_BankRow.BankName.Value)
            If SC_BankRow.WorldRank.Updated Then db.AddInParameter(dbcmd, "@WorldRank", DbType.String, SC_BankRow.WorldRank.Value)
            If SC_BankRow.MoodyGrade.Updated Then db.AddInParameter(dbcmd, "@MoodyGrade", DbType.String, SC_BankRow.MoodyGrade.Value)
            If SC_BankRow.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_BankRow.CreateDate.Value), DBNull.Value, SC_BankRow.CreateDate.Value))
            If SC_BankRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_BankRow.LastChgID.Value)
            If SC_BankRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_BankRow.LastChgDate.Value), DBNull.Value, SC_BankRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKBankID", DbType.String, IIf(SC_BankRow.LoadFromDataRow, SC_BankRow.BankID.OldValue, SC_BankRow.BankID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal SC_BankRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_Bank Set")
            For i As Integer = 0 To SC_BankRow.FieldNames.Length - 1
                If Not SC_BankRow.IsIdentityField(SC_BankRow.FieldNames(i)) AndAlso SC_BankRow.IsUpdated(SC_BankRow.FieldNames(i)) AndAlso SC_BankRow.CreateUpdateSQL(SC_BankRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_BankRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where BankID = @PKBankID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_BankRow.BankID.Updated Then db.AddInParameter(dbcmd, "@BankID", DbType.String, SC_BankRow.BankID.Value)
            If SC_BankRow.BankName.Updated Then db.AddInParameter(dbcmd, "@BankName", DbType.String, SC_BankRow.BankName.Value)
            If SC_BankRow.WorldRank.Updated Then db.AddInParameter(dbcmd, "@WorldRank", DbType.String, SC_BankRow.WorldRank.Value)
            If SC_BankRow.MoodyGrade.Updated Then db.AddInParameter(dbcmd, "@MoodyGrade", DbType.String, SC_BankRow.MoodyGrade.Value)
            If SC_BankRow.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_BankRow.CreateDate.Value), DBNull.Value, SC_BankRow.CreateDate.Value))
            If SC_BankRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_BankRow.LastChgID.Value)
            If SC_BankRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_BankRow.LastChgDate.Value), DBNull.Value, SC_BankRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKBankID", DbType.String, IIf(SC_BankRow.LoadFromDataRow, SC_BankRow.BankID.OldValue, SC_BankRow.BankID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_BankRow As Row()) As Integer
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
                    For Each r As Row In SC_BankRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update SC_Bank Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where BankID = @PKBankID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.BankID.Updated Then db.AddInParameter(dbcmd, "@BankID", DbType.String, r.BankID.Value)
                        If r.BankName.Updated Then db.AddInParameter(dbcmd, "@BankName", DbType.String, r.BankName.Value)
                        If r.WorldRank.Updated Then db.AddInParameter(dbcmd, "@WorldRank", DbType.String, r.WorldRank.Value)
                        If r.MoodyGrade.Updated Then db.AddInParameter(dbcmd, "@MoodyGrade", DbType.String, r.MoodyGrade.Value)
                        If r.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), DBNull.Value, r.CreateDate.Value))
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), DBNull.Value, r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKBankID", DbType.String, IIf(r.LoadFromDataRow, r.BankID.OldValue, r.BankID.Value))

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

        Public Function Update(ByVal SC_BankRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In SC_BankRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update SC_Bank Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where BankID = @PKBankID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.BankID.Updated Then db.AddInParameter(dbcmd, "@BankID", DbType.String, r.BankID.Value)
                If r.BankName.Updated Then db.AddInParameter(dbcmd, "@BankName", DbType.String, r.BankName.Value)
                If r.WorldRank.Updated Then db.AddInParameter(dbcmd, "@WorldRank", DbType.String, r.WorldRank.Value)
                If r.MoodyGrade.Updated Then db.AddInParameter(dbcmd, "@MoodyGrade", DbType.String, r.MoodyGrade.Value)
                If r.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), DBNull.Value, r.CreateDate.Value))
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), DBNull.Value, r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKBankID", DbType.String, IIf(r.LoadFromDataRow, r.BankID.OldValue, r.BankID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal SC_BankRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_Bank")
            strSQL.AppendLine("Where BankID = @BankID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@BankID", DbType.String, SC_BankRow.BankID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal SC_BankRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_Bank")
            strSQL.AppendLine("Where BankID = @BankID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@BankID", DbType.String, SC_BankRow.BankID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_Bank")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal SC_BankRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_Bank")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    BankID, BankName, WorldRank, MoodyGrade, CreateDate, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @BankID, @BankName, @WorldRank, @MoodyGrade, @CreateDate, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@BankID", DbType.String, SC_BankRow.BankID.Value)
            db.AddInParameter(dbcmd, "@BankName", DbType.String, SC_BankRow.BankName.Value)
            db.AddInParameter(dbcmd, "@WorldRank", DbType.String, SC_BankRow.WorldRank.Value)
            db.AddInParameter(dbcmd, "@MoodyGrade", DbType.String, SC_BankRow.MoodyGrade.Value)
            db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_BankRow.CreateDate.Value), DBNull.Value, SC_BankRow.CreateDate.Value))
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_BankRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_BankRow.LastChgDate.Value), DBNull.Value, SC_BankRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal SC_BankRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_Bank")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    BankID, BankName, WorldRank, MoodyGrade, CreateDate, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @BankID, @BankName, @WorldRank, @MoodyGrade, @CreateDate, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@BankID", DbType.String, SC_BankRow.BankID.Value)
            db.AddInParameter(dbcmd, "@BankName", DbType.String, SC_BankRow.BankName.Value)
            db.AddInParameter(dbcmd, "@WorldRank", DbType.String, SC_BankRow.WorldRank.Value)
            db.AddInParameter(dbcmd, "@MoodyGrade", DbType.String, SC_BankRow.MoodyGrade.Value)
            db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_BankRow.CreateDate.Value), DBNull.Value, SC_BankRow.CreateDate.Value))
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_BankRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_BankRow.LastChgDate.Value), DBNull.Value, SC_BankRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal SC_BankRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_Bank")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    BankID, BankName, WorldRank, MoodyGrade, CreateDate, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @BankID, @BankName, @WorldRank, @MoodyGrade, @CreateDate, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_BankRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@BankID", DbType.String, r.BankID.Value)
                        db.AddInParameter(dbcmd, "@BankName", DbType.String, r.BankName.Value)
                        db.AddInParameter(dbcmd, "@WorldRank", DbType.String, r.WorldRank.Value)
                        db.AddInParameter(dbcmd, "@MoodyGrade", DbType.String, r.MoodyGrade.Value)
                        db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), DBNull.Value, r.CreateDate.Value))
                        db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), DBNull.Value, r.LastChgDate.Value))

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

        Public Function Insert(ByVal SC_BankRow As Row(), ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_Bank")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    BankID, BankName, WorldRank, MoodyGrade, CreateDate, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @BankID, @BankName, @WorldRank, @MoodyGrade, @CreateDate, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_BankRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@BankID", DbType.String, r.BankID.Value)
                db.AddInParameter(dbcmd, "@BankName", DbType.String, r.BankName.Value)
                db.AddInParameter(dbcmd, "@WorldRank", DbType.String, r.WorldRank.Value)
                db.AddInParameter(dbcmd, "@MoodyGrade", DbType.String, r.MoodyGrade.Value)
                db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), DBNull.Value, r.CreateDate.Value))
                db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), DBNull.Value, r.LastChgDate.Value))

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

