'****************************************************************
' Table:SC_SendMailControl
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

Namespace beSC_SendMailControl
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "ArrivalDt", "MailSeqNo", "SenderID", "ReceiverID", "ReceiverIP", "ReceiverTime", "MailInValidDt", "CreateDate", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(Integer), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(Date), GetType(Date), GetType(Date) }
        Private m_PrimaryFields As String() = { "ArrivalDt", "MailSeqNo", "ReceiverID" }

        Public ReadOnly Property Rows() As beSC_SendMailControl.Rows 
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
        Public Sub Transfer2Row(SC_SendMailControlTable As DataTable)
            For Each dr As DataRow In SC_SendMailControlTable.Rows
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

                dr(m_Rows(i).ArrivalDt.FieldName) = m_Rows(i).ArrivalDt.Value
                dr(m_Rows(i).MailSeqNo.FieldName) = m_Rows(i).MailSeqNo.Value
                dr(m_Rows(i).SenderID.FieldName) = m_Rows(i).SenderID.Value
                dr(m_Rows(i).ReceiverID.FieldName) = m_Rows(i).ReceiverID.Value
                dr(m_Rows(i).ReceiverIP.FieldName) = m_Rows(i).ReceiverIP.Value
                dr(m_Rows(i).ReceiverTime.FieldName) = m_Rows(i).ReceiverTime.Value
                dr(m_Rows(i).MailInValidDt.FieldName) = m_Rows(i).MailInValidDt.Value
                dr(m_Rows(i).CreateDate.FieldName) = m_Rows(i).CreateDate.Value
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

        Public Sub Add(SC_SendMailControlRow As Row)
            m_Rows.Add(SC_SendMailControlRow)
        End Sub

        Public Sub Remove(SC_SendMailControlRow As Row)
            If m_Rows.IndexOf(SC_SendMailControlRow) >= 0 Then
                m_Rows.Remove(SC_SendMailControlRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_ArrivalDt As Field(Of String) = new Field(Of String)("ArrivalDt", true)
        Private FI_MailSeqNo As Field(Of Integer) = new Field(Of Integer)("MailSeqNo", true)
        Private FI_SenderID As Field(Of String) = new Field(Of String)("SenderID", true)
        Private FI_ReceiverID As Field(Of String) = new Field(Of String)("ReceiverID", true)
        Private FI_ReceiverIP As Field(Of String) = new Field(Of String)("ReceiverIP", true)
        Private FI_ReceiverTime As Field(Of Date) = new Field(Of Date)("ReceiverTime", true)
        Private FI_MailInValidDt As Field(Of Date) = new Field(Of Date)("MailInValidDt", true)
        Private FI_CreateDate As Field(Of Date) = new Field(Of Date)("CreateDate", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "ArrivalDt", "MailSeqNo", "SenderID", "ReceiverID", "ReceiverIP", "ReceiverTime", "MailInValidDt", "CreateDate", "LastChgDate" }
        Private m_PrimaryFields As String() = { "ArrivalDt", "MailSeqNo", "ReceiverID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "ArrivalDt"
                    Return FI_ArrivalDt.Value
                Case "MailSeqNo"
                    Return FI_MailSeqNo.Value
                Case "SenderID"
                    Return FI_SenderID.Value
                Case "ReceiverID"
                    Return FI_ReceiverID.Value
                Case "ReceiverIP"
                    Return FI_ReceiverIP.Value
                Case "ReceiverTime"
                    Return FI_ReceiverTime.Value
                Case "MailInValidDt"
                    Return FI_MailInValidDt.Value
                Case "CreateDate"
                    Return FI_CreateDate.Value
                Case "LastChgDate"
                    Return FI_LastChgDate.Value
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Sub SetFieldValue(ByVal fieldName As String, ByVal value As Object)
            Select Case fieldName
                Case "ArrivalDt"
                    FI_ArrivalDt.SetValue(value)
                Case "MailSeqNo"
                    FI_MailSeqNo.SetValue(value)
                Case "SenderID"
                    FI_SenderID.SetValue(value)
                Case "ReceiverID"
                    FI_ReceiverID.SetValue(value)
                Case "ReceiverIP"
                    FI_ReceiverIP.SetValue(value)
                Case "ReceiverTime"
                    FI_ReceiverTime.SetValue(value)
                Case "MailInValidDt"
                    FI_MailInValidDt.SetValue(value)
                Case "CreateDate"
                    FI_CreateDate.SetValue(value)
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
                Case "ArrivalDt"
                    return FI_ArrivalDt.Updated
                Case "MailSeqNo"
                    return FI_MailSeqNo.Updated
                Case "SenderID"
                    return FI_SenderID.Updated
                Case "ReceiverID"
                    return FI_ReceiverID.Updated
                Case "ReceiverIP"
                    return FI_ReceiverIP.Updated
                Case "ReceiverTime"
                    return FI_ReceiverTime.Updated
                Case "MailInValidDt"
                    return FI_MailInValidDt.Updated
                Case "CreateDate"
                    return FI_CreateDate.Updated
                Case "LastChgDate"
                    return FI_LastChgDate.Updated
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public Function CreateUpdateSQL(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "ArrivalDt"
                    return FI_ArrivalDt.CreateUpdateSQL
                Case "MailSeqNo"
                    return FI_MailSeqNo.CreateUpdateSQL
                Case "SenderID"
                    return FI_SenderID.CreateUpdateSQL
                Case "ReceiverID"
                    return FI_ReceiverID.CreateUpdateSQL
                Case "ReceiverIP"
                    return FI_ReceiverIP.CreateUpdateSQL
                Case "ReceiverTime"
                    return FI_ReceiverTime.CreateUpdateSQL
                Case "MailInValidDt"
                    return FI_MailInValidDt.CreateUpdateSQL
                Case "CreateDate"
                    return FI_CreateDate.CreateUpdateSQL
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
            FI_ArrivalDt.SetInitValue("")
            FI_MailSeqNo.SetInitValue(0)
            FI_SenderID.SetInitValue("")
            FI_ReceiverID.SetInitValue("")
            FI_CreateDate.SetInitValue(DateTime.Now)
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_ArrivalDt.SetInitValue(dr("ArrivalDt"))
            FI_MailSeqNo.SetInitValue(dr("MailSeqNo"))
            FI_SenderID.SetInitValue(dr("SenderID"))
            FI_ReceiverID.SetInitValue(dr("ReceiverID"))
            FI_ReceiverIP.SetInitValue(dr("ReceiverIP"))
            FI_ReceiverTime.SetInitValue(dr("ReceiverTime"))
            FI_MailInValidDt.SetInitValue(dr("MailInValidDt"))
            FI_CreateDate.SetInitValue(dr("CreateDate"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_ArrivalDt.Updated = False
            FI_MailSeqNo.Updated = False
            FI_SenderID.Updated = False
            FI_ReceiverID.Updated = False
            FI_ReceiverIP.Updated = False
            FI_ReceiverTime.Updated = False
            FI_MailInValidDt.Updated = False
            FI_CreateDate.Updated = False
            FI_LastChgDate.Updated = False
        End Sub

        Public ReadOnly Property ArrivalDt As Field(Of String) 
            Get
                Return FI_ArrivalDt
            End Get
        End Property

        Public ReadOnly Property MailSeqNo As Field(Of Integer) 
            Get
                Return FI_MailSeqNo
            End Get
        End Property

        Public ReadOnly Property SenderID As Field(Of String) 
            Get
                Return FI_SenderID
            End Get
        End Property

        Public ReadOnly Property ReceiverID As Field(Of String) 
            Get
                Return FI_ReceiverID
            End Get
        End Property

        Public ReadOnly Property ReceiverIP As Field(Of String) 
            Get
                Return FI_ReceiverIP
            End Get
        End Property

        Public ReadOnly Property ReceiverTime As Field(Of Date) 
            Get
                Return FI_ReceiverTime
            End Get
        End Property

        Public ReadOnly Property MailInValidDt As Field(Of Date) 
            Get
                Return FI_MailInValidDt
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

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal SC_SendMailControlRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_SendMailControl")
            strSQL.AppendLine("Where ArrivalDt = @ArrivalDt")
            strSQL.AppendLine("And MailSeqNo = @MailSeqNo")
            strSQL.AppendLine("And ReceiverID = @ReceiverID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@ArrivalDt", DbType.String, SC_SendMailControlRow.ArrivalDt.Value)
            db.AddInParameter(dbcmd, "@MailSeqNo", DbType.Int32, SC_SendMailControlRow.MailSeqNo.Value)
            db.AddInParameter(dbcmd, "@ReceiverID", DbType.String, SC_SendMailControlRow.ReceiverID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal SC_SendMailControlRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_SendMailControl")
            strSQL.AppendLine("Where ArrivalDt = @ArrivalDt")
            strSQL.AppendLine("And MailSeqNo = @MailSeqNo")
            strSQL.AppendLine("And ReceiverID = @ReceiverID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@ArrivalDt", DbType.String, SC_SendMailControlRow.ArrivalDt.Value)
            db.AddInParameter(dbcmd, "@MailSeqNo", DbType.Int32, SC_SendMailControlRow.MailSeqNo.Value)
            db.AddInParameter(dbcmd, "@ReceiverID", DbType.String, SC_SendMailControlRow.ReceiverID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal SC_SendMailControlRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_SendMailControl")
            strSQL.AppendLine("Where ArrivalDt = @ArrivalDt")
            strSQL.AppendLine("And MailSeqNo = @MailSeqNo")
            strSQL.AppendLine("And ReceiverID = @ReceiverID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_SendMailControlRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@ArrivalDt", DbType.String, r.ArrivalDt.Value)
                        db.AddInParameter(dbcmd, "@MailSeqNo", DbType.Int32, r.MailSeqNo.Value)
                        db.AddInParameter(dbcmd, "@ReceiverID", DbType.String, r.ReceiverID.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal SC_SendMailControlRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_SendMailControl")
            strSQL.AppendLine("Where ArrivalDt = @ArrivalDt")
            strSQL.AppendLine("And MailSeqNo = @MailSeqNo")
            strSQL.AppendLine("And ReceiverID = @ReceiverID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_SendMailControlRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@ArrivalDt", DbType.String, r.ArrivalDt.Value)
                db.AddInParameter(dbcmd, "@MailSeqNo", DbType.Int32, r.MailSeqNo.Value)
                db.AddInParameter(dbcmd, "@ReceiverID", DbType.String, r.ReceiverID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal SC_SendMailControlRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_SendMailControl")
            strSQL.AppendLine("Where ArrivalDt = @ArrivalDt")
            strSQL.AppendLine("And MailSeqNo = @MailSeqNo")
            strSQL.AppendLine("And ReceiverID = @ReceiverID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@ArrivalDt", DbType.String, SC_SendMailControlRow.ArrivalDt.Value)
            db.AddInParameter(dbcmd, "@MailSeqNo", DbType.Int32, SC_SendMailControlRow.MailSeqNo.Value)
            db.AddInParameter(dbcmd, "@ReceiverID", DbType.String, SC_SendMailControlRow.ReceiverID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(SC_SendMailControlRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_SendMailControl")
            strSQL.AppendLine("Where ArrivalDt = @ArrivalDt")
            strSQL.AppendLine("And MailSeqNo = @MailSeqNo")
            strSQL.AppendLine("And ReceiverID = @ReceiverID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@ArrivalDt", DbType.String, SC_SendMailControlRow.ArrivalDt.Value)
            db.AddInParameter(dbcmd, "@MailSeqNo", DbType.Int32, SC_SendMailControlRow.MailSeqNo.Value)
            db.AddInParameter(dbcmd, "@ReceiverID", DbType.String, SC_SendMailControlRow.ReceiverID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_SendMailControlRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_SendMailControl Set")
            For i As Integer = 0 To SC_SendMailControlRow.FieldNames.Length - 1
                If Not SC_SendMailControlRow.IsIdentityField(SC_SendMailControlRow.FieldNames(i)) AndAlso SC_SendMailControlRow.IsUpdated(SC_SendMailControlRow.FieldNames(i)) AndAlso SC_SendMailControlRow.CreateUpdateSQL(SC_SendMailControlRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_SendMailControlRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where ArrivalDt = @PKArrivalDt")
            strSQL.AppendLine("And MailSeqNo = @PKMailSeqNo")
            strSQL.AppendLine("And ReceiverID = @PKReceiverID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_SendMailControlRow.ArrivalDt.Updated Then db.AddInParameter(dbcmd, "@ArrivalDt", DbType.String, SC_SendMailControlRow.ArrivalDt.Value)
            If SC_SendMailControlRow.MailSeqNo.Updated Then db.AddInParameter(dbcmd, "@MailSeqNo", DbType.Int32, SC_SendMailControlRow.MailSeqNo.Value)
            If SC_SendMailControlRow.SenderID.Updated Then db.AddInParameter(dbcmd, "@SenderID", DbType.String, SC_SendMailControlRow.SenderID.Value)
            If SC_SendMailControlRow.ReceiverID.Updated Then db.AddInParameter(dbcmd, "@ReceiverID", DbType.String, SC_SendMailControlRow.ReceiverID.Value)
            If SC_SendMailControlRow.ReceiverIP.Updated Then db.AddInParameter(dbcmd, "@ReceiverIP", DbType.String, SC_SendMailControlRow.ReceiverIP.Value)
            If SC_SendMailControlRow.ReceiverTime.Updated Then db.AddInParameter(dbcmd, "@ReceiverTime", DbType.Date, IIf(IsDateTimeNull(SC_SendMailControlRow.ReceiverTime.Value), DBNull.Value, SC_SendMailControlRow.ReceiverTime.Value))
            If SC_SendMailControlRow.MailInValidDt.Updated Then db.AddInParameter(dbcmd, "@MailInValidDt", DbType.Date, IIf(IsDateTimeNull(SC_SendMailControlRow.MailInValidDt.Value), DBNull.Value, SC_SendMailControlRow.MailInValidDt.Value))
            If SC_SendMailControlRow.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_SendMailControlRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_SendMailControlRow.CreateDate.Value))
            If SC_SendMailControlRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_SendMailControlRow.LastChgDate.Value), DBNull.Value, SC_SendMailControlRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKArrivalDt", DbType.String, IIf(SC_SendMailControlRow.LoadFromDataRow, SC_SendMailControlRow.ArrivalDt.OldValue, SC_SendMailControlRow.ArrivalDt.Value))
            db.AddInParameter(dbcmd, "@PKMailSeqNo", DbType.Int32, IIf(SC_SendMailControlRow.LoadFromDataRow, SC_SendMailControlRow.MailSeqNo.OldValue, SC_SendMailControlRow.MailSeqNo.Value))
            db.AddInParameter(dbcmd, "@PKReceiverID", DbType.String, IIf(SC_SendMailControlRow.LoadFromDataRow, SC_SendMailControlRow.ReceiverID.OldValue, SC_SendMailControlRow.ReceiverID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal SC_SendMailControlRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_SendMailControl Set")
            For i As Integer = 0 To SC_SendMailControlRow.FieldNames.Length - 1
                If Not SC_SendMailControlRow.IsIdentityField(SC_SendMailControlRow.FieldNames(i)) AndAlso SC_SendMailControlRow.IsUpdated(SC_SendMailControlRow.FieldNames(i)) AndAlso SC_SendMailControlRow.CreateUpdateSQL(SC_SendMailControlRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_SendMailControlRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where ArrivalDt = @PKArrivalDt")
            strSQL.AppendLine("And MailSeqNo = @PKMailSeqNo")
            strSQL.AppendLine("And ReceiverID = @PKReceiverID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_SendMailControlRow.ArrivalDt.Updated Then db.AddInParameter(dbcmd, "@ArrivalDt", DbType.String, SC_SendMailControlRow.ArrivalDt.Value)
            If SC_SendMailControlRow.MailSeqNo.Updated Then db.AddInParameter(dbcmd, "@MailSeqNo", DbType.Int32, SC_SendMailControlRow.MailSeqNo.Value)
            If SC_SendMailControlRow.SenderID.Updated Then db.AddInParameter(dbcmd, "@SenderID", DbType.String, SC_SendMailControlRow.SenderID.Value)
            If SC_SendMailControlRow.ReceiverID.Updated Then db.AddInParameter(dbcmd, "@ReceiverID", DbType.String, SC_SendMailControlRow.ReceiverID.Value)
            If SC_SendMailControlRow.ReceiverIP.Updated Then db.AddInParameter(dbcmd, "@ReceiverIP", DbType.String, SC_SendMailControlRow.ReceiverIP.Value)
            If SC_SendMailControlRow.ReceiverTime.Updated Then db.AddInParameter(dbcmd, "@ReceiverTime", DbType.Date, IIf(IsDateTimeNull(SC_SendMailControlRow.ReceiverTime.Value), DBNull.Value, SC_SendMailControlRow.ReceiverTime.Value))
            If SC_SendMailControlRow.MailInValidDt.Updated Then db.AddInParameter(dbcmd, "@MailInValidDt", DbType.Date, IIf(IsDateTimeNull(SC_SendMailControlRow.MailInValidDt.Value), DBNull.Value, SC_SendMailControlRow.MailInValidDt.Value))
            If SC_SendMailControlRow.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_SendMailControlRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_SendMailControlRow.CreateDate.Value))
            If SC_SendMailControlRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_SendMailControlRow.LastChgDate.Value), DBNull.Value, SC_SendMailControlRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKArrivalDt", DbType.String, IIf(SC_SendMailControlRow.LoadFromDataRow, SC_SendMailControlRow.ArrivalDt.OldValue, SC_SendMailControlRow.ArrivalDt.Value))
            db.AddInParameter(dbcmd, "@PKMailSeqNo", DbType.Int32, IIf(SC_SendMailControlRow.LoadFromDataRow, SC_SendMailControlRow.MailSeqNo.OldValue, SC_SendMailControlRow.MailSeqNo.Value))
            db.AddInParameter(dbcmd, "@PKReceiverID", DbType.String, IIf(SC_SendMailControlRow.LoadFromDataRow, SC_SendMailControlRow.ReceiverID.OldValue, SC_SendMailControlRow.ReceiverID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_SendMailControlRow As Row()) As Integer
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
                    For Each r As Row In SC_SendMailControlRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update SC_SendMailControl Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where ArrivalDt = @PKArrivalDt")
                        strSQL.AppendLine("And MailSeqNo = @PKMailSeqNo")
                        strSQL.AppendLine("And ReceiverID = @PKReceiverID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.ArrivalDt.Updated Then db.AddInParameter(dbcmd, "@ArrivalDt", DbType.String, r.ArrivalDt.Value)
                        If r.MailSeqNo.Updated Then db.AddInParameter(dbcmd, "@MailSeqNo", DbType.Int32, r.MailSeqNo.Value)
                        If r.SenderID.Updated Then db.AddInParameter(dbcmd, "@SenderID", DbType.String, r.SenderID.Value)
                        If r.ReceiverID.Updated Then db.AddInParameter(dbcmd, "@ReceiverID", DbType.String, r.ReceiverID.Value)
                        If r.ReceiverIP.Updated Then db.AddInParameter(dbcmd, "@ReceiverIP", DbType.String, r.ReceiverIP.Value)
                        If r.ReceiverTime.Updated Then db.AddInParameter(dbcmd, "@ReceiverTime", DbType.Date, IIf(IsDateTimeNull(r.ReceiverTime.Value), DBNull.Value, r.ReceiverTime.Value))
                        If r.MailInValidDt.Updated Then db.AddInParameter(dbcmd, "@MailInValidDt", DbType.Date, IIf(IsDateTimeNull(r.MailInValidDt.Value), DBNull.Value, r.MailInValidDt.Value))
                        If r.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), DBNull.Value, r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKArrivalDt", DbType.String, IIf(r.LoadFromDataRow, r.ArrivalDt.OldValue, r.ArrivalDt.Value))
                        db.AddInParameter(dbcmd, "@PKMailSeqNo", DbType.Int32, IIf(r.LoadFromDataRow, r.MailSeqNo.OldValue, r.MailSeqNo.Value))
                        db.AddInParameter(dbcmd, "@PKReceiverID", DbType.String, IIf(r.LoadFromDataRow, r.ReceiverID.OldValue, r.ReceiverID.Value))

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

        Public Function Update(ByVal SC_SendMailControlRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In SC_SendMailControlRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update SC_SendMailControl Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where ArrivalDt = @PKArrivalDt")
                strSQL.AppendLine("And MailSeqNo = @PKMailSeqNo")
                strSQL.AppendLine("And ReceiverID = @PKReceiverID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.ArrivalDt.Updated Then db.AddInParameter(dbcmd, "@ArrivalDt", DbType.String, r.ArrivalDt.Value)
                If r.MailSeqNo.Updated Then db.AddInParameter(dbcmd, "@MailSeqNo", DbType.Int32, r.MailSeqNo.Value)
                If r.SenderID.Updated Then db.AddInParameter(dbcmd, "@SenderID", DbType.String, r.SenderID.Value)
                If r.ReceiverID.Updated Then db.AddInParameter(dbcmd, "@ReceiverID", DbType.String, r.ReceiverID.Value)
                If r.ReceiverIP.Updated Then db.AddInParameter(dbcmd, "@ReceiverIP", DbType.String, r.ReceiverIP.Value)
                If r.ReceiverTime.Updated Then db.AddInParameter(dbcmd, "@ReceiverTime", DbType.Date, IIf(IsDateTimeNull(r.ReceiverTime.Value), DBNull.Value, r.ReceiverTime.Value))
                If r.MailInValidDt.Updated Then db.AddInParameter(dbcmd, "@MailInValidDt", DbType.Date, IIf(IsDateTimeNull(r.MailInValidDt.Value), DBNull.Value, r.MailInValidDt.Value))
                If r.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), DBNull.Value, r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKArrivalDt", DbType.String, IIf(r.LoadFromDataRow, r.ArrivalDt.OldValue, r.ArrivalDt.Value))
                db.AddInParameter(dbcmd, "@PKMailSeqNo", DbType.Int32, IIf(r.LoadFromDataRow, r.MailSeqNo.OldValue, r.MailSeqNo.Value))
                db.AddInParameter(dbcmd, "@PKReceiverID", DbType.String, IIf(r.LoadFromDataRow, r.ReceiverID.OldValue, r.ReceiverID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal SC_SendMailControlRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_SendMailControl")
            strSQL.AppendLine("Where ArrivalDt = @ArrivalDt")
            strSQL.AppendLine("And MailSeqNo = @MailSeqNo")
            strSQL.AppendLine("And ReceiverID = @ReceiverID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@ArrivalDt", DbType.String, SC_SendMailControlRow.ArrivalDt.Value)
            db.AddInParameter(dbcmd, "@MailSeqNo", DbType.Int32, SC_SendMailControlRow.MailSeqNo.Value)
            db.AddInParameter(dbcmd, "@ReceiverID", DbType.String, SC_SendMailControlRow.ReceiverID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal SC_SendMailControlRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_SendMailControl")
            strSQL.AppendLine("Where ArrivalDt = @ArrivalDt")
            strSQL.AppendLine("And MailSeqNo = @MailSeqNo")
            strSQL.AppendLine("And ReceiverID = @ReceiverID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@ArrivalDt", DbType.String, SC_SendMailControlRow.ArrivalDt.Value)
            db.AddInParameter(dbcmd, "@MailSeqNo", DbType.Int32, SC_SendMailControlRow.MailSeqNo.Value)
            db.AddInParameter(dbcmd, "@ReceiverID", DbType.String, SC_SendMailControlRow.ReceiverID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_SendMailControl")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal SC_SendMailControlRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_SendMailControl")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    ArrivalDt, MailSeqNo, SenderID, ReceiverID, ReceiverIP, ReceiverTime, MailInValidDt,")
            strSQL.AppendLine("    CreateDate, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @ArrivalDt, @MailSeqNo, @SenderID, @ReceiverID, @ReceiverIP, @ReceiverTime, @MailInValidDt,")
            strSQL.AppendLine("    @CreateDate, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@ArrivalDt", DbType.String, SC_SendMailControlRow.ArrivalDt.Value)
            db.AddInParameter(dbcmd, "@MailSeqNo", DbType.Int32, SC_SendMailControlRow.MailSeqNo.Value)
            db.AddInParameter(dbcmd, "@SenderID", DbType.String, SC_SendMailControlRow.SenderID.Value)
            db.AddInParameter(dbcmd, "@ReceiverID", DbType.String, SC_SendMailControlRow.ReceiverID.Value)
            db.AddInParameter(dbcmd, "@ReceiverIP", DbType.String, SC_SendMailControlRow.ReceiverIP.Value)
            db.AddInParameter(dbcmd, "@ReceiverTime", DbType.Date, IIf(IsDateTimeNull(SC_SendMailControlRow.ReceiverTime.Value), DBNull.Value, SC_SendMailControlRow.ReceiverTime.Value))
            db.AddInParameter(dbcmd, "@MailInValidDt", DbType.Date, IIf(IsDateTimeNull(SC_SendMailControlRow.MailInValidDt.Value), DBNull.Value, SC_SendMailControlRow.MailInValidDt.Value))
            db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_SendMailControlRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_SendMailControlRow.CreateDate.Value))
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_SendMailControlRow.LastChgDate.Value), DBNull.Value, SC_SendMailControlRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal SC_SendMailControlRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_SendMailControl")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    ArrivalDt, MailSeqNo, SenderID, ReceiverID, ReceiverIP, ReceiverTime, MailInValidDt,")
            strSQL.AppendLine("    CreateDate, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @ArrivalDt, @MailSeqNo, @SenderID, @ReceiverID, @ReceiverIP, @ReceiverTime, @MailInValidDt,")
            strSQL.AppendLine("    @CreateDate, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@ArrivalDt", DbType.String, SC_SendMailControlRow.ArrivalDt.Value)
            db.AddInParameter(dbcmd, "@MailSeqNo", DbType.Int32, SC_SendMailControlRow.MailSeqNo.Value)
            db.AddInParameter(dbcmd, "@SenderID", DbType.String, SC_SendMailControlRow.SenderID.Value)
            db.AddInParameter(dbcmd, "@ReceiverID", DbType.String, SC_SendMailControlRow.ReceiverID.Value)
            db.AddInParameter(dbcmd, "@ReceiverIP", DbType.String, SC_SendMailControlRow.ReceiverIP.Value)
            db.AddInParameter(dbcmd, "@ReceiverTime", DbType.Date, IIf(IsDateTimeNull(SC_SendMailControlRow.ReceiverTime.Value), DBNull.Value, SC_SendMailControlRow.ReceiverTime.Value))
            db.AddInParameter(dbcmd, "@MailInValidDt", DbType.Date, IIf(IsDateTimeNull(SC_SendMailControlRow.MailInValidDt.Value), DBNull.Value, SC_SendMailControlRow.MailInValidDt.Value))
            db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_SendMailControlRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_SendMailControlRow.CreateDate.Value))
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_SendMailControlRow.LastChgDate.Value), DBNull.Value, SC_SendMailControlRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal SC_SendMailControlRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_SendMailControl")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    ArrivalDt, MailSeqNo, SenderID, ReceiverID, ReceiverIP, ReceiverTime, MailInValidDt,")
            strSQL.AppendLine("    CreateDate, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @ArrivalDt, @MailSeqNo, @SenderID, @ReceiverID, @ReceiverIP, @ReceiverTime, @MailInValidDt,")
            strSQL.AppendLine("    @CreateDate, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_SendMailControlRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@ArrivalDt", DbType.String, r.ArrivalDt.Value)
                        db.AddInParameter(dbcmd, "@MailSeqNo", DbType.Int32, r.MailSeqNo.Value)
                        db.AddInParameter(dbcmd, "@SenderID", DbType.String, r.SenderID.Value)
                        db.AddInParameter(dbcmd, "@ReceiverID", DbType.String, r.ReceiverID.Value)
                        db.AddInParameter(dbcmd, "@ReceiverIP", DbType.String, r.ReceiverIP.Value)
                        db.AddInParameter(dbcmd, "@ReceiverTime", DbType.Date, IIf(IsDateTimeNull(r.ReceiverTime.Value), DBNull.Value, r.ReceiverTime.Value))
                        db.AddInParameter(dbcmd, "@MailInValidDt", DbType.Date, IIf(IsDateTimeNull(r.MailInValidDt.Value), DBNull.Value, r.MailInValidDt.Value))
                        db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
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

        Public Function Insert(ByVal SC_SendMailControlRow As Row(), ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_SendMailControl")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    ArrivalDt, MailSeqNo, SenderID, ReceiverID, ReceiverIP, ReceiverTime, MailInValidDt,")
            strSQL.AppendLine("    CreateDate, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @ArrivalDt, @MailSeqNo, @SenderID, @ReceiverID, @ReceiverIP, @ReceiverTime, @MailInValidDt,")
            strSQL.AppendLine("    @CreateDate, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_SendMailControlRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@ArrivalDt", DbType.String, r.ArrivalDt.Value)
                db.AddInParameter(dbcmd, "@MailSeqNo", DbType.Int32, r.MailSeqNo.Value)
                db.AddInParameter(dbcmd, "@SenderID", DbType.String, r.SenderID.Value)
                db.AddInParameter(dbcmd, "@ReceiverID", DbType.String, r.ReceiverID.Value)
                db.AddInParameter(dbcmd, "@ReceiverIP", DbType.String, r.ReceiverIP.Value)
                db.AddInParameter(dbcmd, "@ReceiverTime", DbType.Date, IIf(IsDateTimeNull(r.ReceiverTime.Value), DBNull.Value, r.ReceiverTime.Value))
                db.AddInParameter(dbcmd, "@MailInValidDt", DbType.Date, IIf(IsDateTimeNull(r.MailInValidDt.Value), DBNull.Value, r.MailInValidDt.Value))
                db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
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

