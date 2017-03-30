'****************************************************************
' Table:SC_Nation
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

Namespace beSC_Nation
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "NationID", "EngName", "ChnName", "NegotiationFlag", "T24Code", "CreateDate", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "NationID" }

        Public ReadOnly Property Rows() As beSC_Nation.Rows 
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
        Public Sub Transfer2Row(SC_NationTable As DataTable)
            For Each dr As DataRow In SC_NationTable.Rows
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

                dr(m_Rows(i).NationID.FieldName) = m_Rows(i).NationID.Value
                dr(m_Rows(i).EngName.FieldName) = m_Rows(i).EngName.Value
                dr(m_Rows(i).ChnName.FieldName) = m_Rows(i).ChnName.Value
                dr(m_Rows(i).NegotiationFlag.FieldName) = m_Rows(i).NegotiationFlag.Value
                dr(m_Rows(i).T24Code.FieldName) = m_Rows(i).T24Code.Value
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

        Public Sub Add(SC_NationRow As Row)
            m_Rows.Add(SC_NationRow)
        End Sub

        Public Sub Remove(SC_NationRow As Row)
            If m_Rows.IndexOf(SC_NationRow) >= 0 Then
                m_Rows.Remove(SC_NationRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_NationID As Field(Of String) = new Field(Of String)("NationID", true)
        Private FI_EngName As Field(Of String) = new Field(Of String)("EngName", true)
        Private FI_ChnName As Field(Of String) = new Field(Of String)("ChnName", true)
        Private FI_NegotiationFlag As Field(Of String) = new Field(Of String)("NegotiationFlag", true)
        Private FI_T24Code As Field(Of String) = new Field(Of String)("T24Code", true)
        Private FI_CreateDate As Field(Of Date) = new Field(Of Date)("CreateDate", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "NationID", "EngName", "ChnName", "NegotiationFlag", "T24Code", "CreateDate", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "NationID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "NationID"
                    Return FI_NationID.Value
                Case "EngName"
                    Return FI_EngName.Value
                Case "ChnName"
                    Return FI_ChnName.Value
                Case "NegotiationFlag"
                    Return FI_NegotiationFlag.Value
                Case "T24Code"
                    Return FI_T24Code.Value
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
                Case "NationID"
                    FI_NationID.SetValue(value)
                Case "EngName"
                    FI_EngName.SetValue(value)
                Case "ChnName"
                    FI_ChnName.SetValue(value)
                Case "NegotiationFlag"
                    FI_NegotiationFlag.SetValue(value)
                Case "T24Code"
                    FI_T24Code.SetValue(value)
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
                Case "NationID"
                    return FI_NationID.Updated
                Case "EngName"
                    return FI_EngName.Updated
                Case "ChnName"
                    return FI_ChnName.Updated
                Case "NegotiationFlag"
                    return FI_NegotiationFlag.Updated
                Case "T24Code"
                    return FI_T24Code.Updated
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
                Case "NationID"
                    return FI_NationID.CreateUpdateSQL
                Case "EngName"
                    return FI_EngName.CreateUpdateSQL
                Case "ChnName"
                    return FI_ChnName.CreateUpdateSQL
                Case "NegotiationFlag"
                    return FI_NegotiationFlag.CreateUpdateSQL
                Case "T24Code"
                    return FI_T24Code.CreateUpdateSQL
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
            FI_NationID.SetInitValue("")
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_NationID.SetInitValue(dr("NationID"))
            FI_EngName.SetInitValue(dr("EngName"))
            FI_ChnName.SetInitValue(dr("ChnName"))
            FI_NegotiationFlag.SetInitValue(dr("NegotiationFlag"))
            FI_T24Code.SetInitValue(dr("T24Code"))
            FI_CreateDate.SetInitValue(dr("CreateDate"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_NationID.Updated = False
            FI_EngName.Updated = False
            FI_ChnName.Updated = False
            FI_NegotiationFlag.Updated = False
            FI_T24Code.Updated = False
            FI_CreateDate.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
        End Sub

        Public ReadOnly Property NationID As Field(Of String) 
            Get
                Return FI_NationID
            End Get
        End Property

        Public ReadOnly Property EngName As Field(Of String) 
            Get
                Return FI_EngName
            End Get
        End Property

        Public ReadOnly Property ChnName As Field(Of String) 
            Get
                Return FI_ChnName
            End Get
        End Property

        Public ReadOnly Property NegotiationFlag As Field(Of String) 
            Get
                Return FI_NegotiationFlag
            End Get
        End Property

        Public ReadOnly Property T24Code As Field(Of String) 
            Get
                Return FI_T24Code
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
        Public Function DeleteRowByPrimaryKey(ByVal SC_NationRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_Nation")
            strSQL.AppendLine("Where NationID = @NationID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@NationID", DbType.String, SC_NationRow.NationID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal SC_NationRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_Nation")
            strSQL.AppendLine("Where NationID = @NationID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@NationID", DbType.String, SC_NationRow.NationID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal SC_NationRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_Nation")
            strSQL.AppendLine("Where NationID = @NationID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_NationRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@NationID", DbType.String, r.NationID.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal SC_NationRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_Nation")
            strSQL.AppendLine("Where NationID = @NationID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_NationRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@NationID", DbType.String, r.NationID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal SC_NationRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_Nation")
            strSQL.AppendLine("Where NationID = @NationID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@NationID", DbType.String, SC_NationRow.NationID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(SC_NationRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_Nation")
            strSQL.AppendLine("Where NationID = @NationID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@NationID", DbType.String, SC_NationRow.NationID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_NationRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_Nation Set")
            For i As Integer = 0 To SC_NationRow.FieldNames.Length - 1
                If Not SC_NationRow.IsIdentityField(SC_NationRow.FieldNames(i)) AndAlso SC_NationRow.IsUpdated(SC_NationRow.FieldNames(i)) AndAlso SC_NationRow.CreateUpdateSQL(SC_NationRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_NationRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where NationID = @PKNationID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_NationRow.NationID.Updated Then db.AddInParameter(dbcmd, "@NationID", DbType.String, SC_NationRow.NationID.Value)
            If SC_NationRow.EngName.Updated Then db.AddInParameter(dbcmd, "@EngName", DbType.String, SC_NationRow.EngName.Value)
            If SC_NationRow.ChnName.Updated Then db.AddInParameter(dbcmd, "@ChnName", DbType.String, SC_NationRow.ChnName.Value)
            If SC_NationRow.NegotiationFlag.Updated Then db.AddInParameter(dbcmd, "@NegotiationFlag", DbType.String, SC_NationRow.NegotiationFlag.Value)
            If SC_NationRow.T24Code.Updated Then db.AddInParameter(dbcmd, "@T24Code", DbType.String, SC_NationRow.T24Code.Value)
            If SC_NationRow.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_NationRow.CreateDate.Value), DBNull.Value, SC_NationRow.CreateDate.Value))
            If SC_NationRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_NationRow.LastChgID.Value)
            If SC_NationRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_NationRow.LastChgDate.Value), DBNull.Value, SC_NationRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKNationID", DbType.String, IIf(SC_NationRow.LoadFromDataRow, SC_NationRow.NationID.OldValue, SC_NationRow.NationID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal SC_NationRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_Nation Set")
            For i As Integer = 0 To SC_NationRow.FieldNames.Length - 1
                If Not SC_NationRow.IsIdentityField(SC_NationRow.FieldNames(i)) AndAlso SC_NationRow.IsUpdated(SC_NationRow.FieldNames(i)) AndAlso SC_NationRow.CreateUpdateSQL(SC_NationRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_NationRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where NationID = @PKNationID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_NationRow.NationID.Updated Then db.AddInParameter(dbcmd, "@NationID", DbType.String, SC_NationRow.NationID.Value)
            If SC_NationRow.EngName.Updated Then db.AddInParameter(dbcmd, "@EngName", DbType.String, SC_NationRow.EngName.Value)
            If SC_NationRow.ChnName.Updated Then db.AddInParameter(dbcmd, "@ChnName", DbType.String, SC_NationRow.ChnName.Value)
            If SC_NationRow.NegotiationFlag.Updated Then db.AddInParameter(dbcmd, "@NegotiationFlag", DbType.String, SC_NationRow.NegotiationFlag.Value)
            If SC_NationRow.T24Code.Updated Then db.AddInParameter(dbcmd, "@T24Code", DbType.String, SC_NationRow.T24Code.Value)
            If SC_NationRow.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_NationRow.CreateDate.Value), DBNull.Value, SC_NationRow.CreateDate.Value))
            If SC_NationRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_NationRow.LastChgID.Value)
            If SC_NationRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_NationRow.LastChgDate.Value), DBNull.Value, SC_NationRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKNationID", DbType.String, IIf(SC_NationRow.LoadFromDataRow, SC_NationRow.NationID.OldValue, SC_NationRow.NationID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_NationRow As Row()) As Integer
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
                    For Each r As Row In SC_NationRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update SC_Nation Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where NationID = @PKNationID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.NationID.Updated Then db.AddInParameter(dbcmd, "@NationID", DbType.String, r.NationID.Value)
                        If r.EngName.Updated Then db.AddInParameter(dbcmd, "@EngName", DbType.String, r.EngName.Value)
                        If r.ChnName.Updated Then db.AddInParameter(dbcmd, "@ChnName", DbType.String, r.ChnName.Value)
                        If r.NegotiationFlag.Updated Then db.AddInParameter(dbcmd, "@NegotiationFlag", DbType.String, r.NegotiationFlag.Value)
                        If r.T24Code.Updated Then db.AddInParameter(dbcmd, "@T24Code", DbType.String, r.T24Code.Value)
                        If r.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), DBNull.Value, r.CreateDate.Value))
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), DBNull.Value, r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKNationID", DbType.String, IIf(r.LoadFromDataRow, r.NationID.OldValue, r.NationID.Value))

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

        Public Function Update(ByVal SC_NationRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In SC_NationRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update SC_Nation Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where NationID = @PKNationID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.NationID.Updated Then db.AddInParameter(dbcmd, "@NationID", DbType.String, r.NationID.Value)
                If r.EngName.Updated Then db.AddInParameter(dbcmd, "@EngName", DbType.String, r.EngName.Value)
                If r.ChnName.Updated Then db.AddInParameter(dbcmd, "@ChnName", DbType.String, r.ChnName.Value)
                If r.NegotiationFlag.Updated Then db.AddInParameter(dbcmd, "@NegotiationFlag", DbType.String, r.NegotiationFlag.Value)
                If r.T24Code.Updated Then db.AddInParameter(dbcmd, "@T24Code", DbType.String, r.T24Code.Value)
                If r.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), DBNull.Value, r.CreateDate.Value))
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), DBNull.Value, r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKNationID", DbType.String, IIf(r.LoadFromDataRow, r.NationID.OldValue, r.NationID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal SC_NationRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_Nation")
            strSQL.AppendLine("Where NationID = @NationID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@NationID", DbType.String, SC_NationRow.NationID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal SC_NationRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_Nation")
            strSQL.AppendLine("Where NationID = @NationID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@NationID", DbType.String, SC_NationRow.NationID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_Nation")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal SC_NationRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_Nation")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    NationID, EngName, ChnName, NegotiationFlag, T24Code, CreateDate, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @NationID, @EngName, @ChnName, @NegotiationFlag, @T24Code, @CreateDate, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@NationID", DbType.String, SC_NationRow.NationID.Value)
            db.AddInParameter(dbcmd, "@EngName", DbType.String, SC_NationRow.EngName.Value)
            db.AddInParameter(dbcmd, "@ChnName", DbType.String, SC_NationRow.ChnName.Value)
            db.AddInParameter(dbcmd, "@NegotiationFlag", DbType.String, SC_NationRow.NegotiationFlag.Value)
            db.AddInParameter(dbcmd, "@T24Code", DbType.String, SC_NationRow.T24Code.Value)
            db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_NationRow.CreateDate.Value), DBNull.Value, SC_NationRow.CreateDate.Value))
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_NationRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_NationRow.LastChgDate.Value), DBNull.Value, SC_NationRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal SC_NationRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_Nation")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    NationID, EngName, ChnName, NegotiationFlag, T24Code, CreateDate, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @NationID, @EngName, @ChnName, @NegotiationFlag, @T24Code, @CreateDate, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@NationID", DbType.String, SC_NationRow.NationID.Value)
            db.AddInParameter(dbcmd, "@EngName", DbType.String, SC_NationRow.EngName.Value)
            db.AddInParameter(dbcmd, "@ChnName", DbType.String, SC_NationRow.ChnName.Value)
            db.AddInParameter(dbcmd, "@NegotiationFlag", DbType.String, SC_NationRow.NegotiationFlag.Value)
            db.AddInParameter(dbcmd, "@T24Code", DbType.String, SC_NationRow.T24Code.Value)
            db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_NationRow.CreateDate.Value), DBNull.Value, SC_NationRow.CreateDate.Value))
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_NationRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_NationRow.LastChgDate.Value), DBNull.Value, SC_NationRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal SC_NationRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_Nation")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    NationID, EngName, ChnName, NegotiationFlag, T24Code, CreateDate, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @NationID, @EngName, @ChnName, @NegotiationFlag, @T24Code, @CreateDate, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_NationRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@NationID", DbType.String, r.NationID.Value)
                        db.AddInParameter(dbcmd, "@EngName", DbType.String, r.EngName.Value)
                        db.AddInParameter(dbcmd, "@ChnName", DbType.String, r.ChnName.Value)
                        db.AddInParameter(dbcmd, "@NegotiationFlag", DbType.String, r.NegotiationFlag.Value)
                        db.AddInParameter(dbcmd, "@T24Code", DbType.String, r.T24Code.Value)
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

        Public Function Insert(ByVal SC_NationRow As Row(), ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_Nation")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    NationID, EngName, ChnName, NegotiationFlag, T24Code, CreateDate, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @NationID, @EngName, @ChnName, @NegotiationFlag, @T24Code, @CreateDate, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_NationRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@NationID", DbType.String, r.NationID.Value)
                db.AddInParameter(dbcmd, "@EngName", DbType.String, r.EngName.Value)
                db.AddInParameter(dbcmd, "@ChnName", DbType.String, r.ChnName.Value)
                db.AddInParameter(dbcmd, "@NegotiationFlag", DbType.String, r.NegotiationFlag.Value)
                db.AddInParameter(dbcmd, "@T24Code", DbType.String, r.T24Code.Value)
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

