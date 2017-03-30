'****************************************************************
' Table:SC_OnlineUserMsg
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

Namespace beSC_OnlineUserMsg
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "IP", "UserID", "SeqNo", "Message", "Priority" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(Integer), GetType(String), GetType(Integer) }
        Private m_PrimaryFields As String() = { "IP", "UserID", "SeqNo" }

        Public ReadOnly Property Rows() As beSC_OnlineUserMsg.Rows 
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
        Public Sub Transfer2Row(SC_OnlineUserMsgTable As DataTable)
            For Each dr As DataRow In SC_OnlineUserMsgTable.Rows
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

                dr(m_Rows(i).IP.FieldName) = m_Rows(i).IP.Value
                dr(m_Rows(i).UserID.FieldName) = m_Rows(i).UserID.Value
                dr(m_Rows(i).SeqNo.FieldName) = m_Rows(i).SeqNo.Value
                dr(m_Rows(i).Message.FieldName) = m_Rows(i).Message.Value
                dr(m_Rows(i).Priority.FieldName) = m_Rows(i).Priority.Value

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

        Public Sub Add(SC_OnlineUserMsgRow As Row)
            m_Rows.Add(SC_OnlineUserMsgRow)
        End Sub

        Public Sub Remove(SC_OnlineUserMsgRow As Row)
            If m_Rows.IndexOf(SC_OnlineUserMsgRow) >= 0 Then
                m_Rows.Remove(SC_OnlineUserMsgRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_IP As Field(Of String) = new Field(Of String)("IP", true)
        Private FI_UserID As Field(Of String) = new Field(Of String)("UserID", true)
        Private FI_SeqNo As Field(Of Integer) = new Field(Of Integer)("SeqNo", true)
        Private FI_Message As Field(Of String) = new Field(Of String)("Message", true)
        Private FI_Priority As Field(Of Integer) = new Field(Of Integer)("Priority", true)
        Private m_FieldNames As String() = { "IP", "UserID", "SeqNo", "Message", "Priority" }
        Private m_PrimaryFields As String() = { "IP", "UserID", "SeqNo" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "IP"
                    Return FI_IP.Value
                Case "UserID"
                    Return FI_UserID.Value
                Case "SeqNo"
                    Return FI_SeqNo.Value
                Case "Message"
                    Return FI_Message.Value
                Case "Priority"
                    Return FI_Priority.Value
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Sub SetFieldValue(ByVal fieldName As String, ByVal value As Object)
            Select Case fieldName
                Case "IP"
                    FI_IP.SetValue(value)
                Case "UserID"
                    FI_UserID.SetValue(value)
                Case "SeqNo"
                    FI_SeqNo.SetValue(value)
                Case "Message"
                    FI_Message.SetValue(value)
                Case "Priority"
                    FI_Priority.SetValue(value)
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
                Case "IP"
                    return FI_IP.Updated
                Case "UserID"
                    return FI_UserID.Updated
                Case "SeqNo"
                    return FI_SeqNo.Updated
                Case "Message"
                    return FI_Message.Updated
                Case "Priority"
                    return FI_Priority.Updated
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public Function CreateUpdateSQL(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "IP"
                    return FI_IP.CreateUpdateSQL
                Case "UserID"
                    return FI_UserID.CreateUpdateSQL
                Case "SeqNo"
                    return FI_SeqNo.CreateUpdateSQL
                Case "Message"
                    return FI_Message.CreateUpdateSQL
                Case "Priority"
                    return FI_Priority.CreateUpdateSQL
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
            FI_IP.SetInitValue("")
            FI_UserID.SetInitValue("")
            FI_SeqNo.SetInitValue(0)
            FI_Message.SetInitValue("")
            FI_Priority.SetInitValue(254)
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_IP.SetInitValue(dr("IP"))
            FI_UserID.SetInitValue(dr("UserID"))
            FI_SeqNo.SetInitValue(dr("SeqNo"))
            FI_Message.SetInitValue(dr("Message"))
            FI_Priority.SetInitValue(dr("Priority"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_IP.Updated = False
            FI_UserID.Updated = False
            FI_SeqNo.Updated = False
            FI_Message.Updated = False
            FI_Priority.Updated = False
        End Sub

        Public ReadOnly Property IP As Field(Of String) 
            Get
                Return FI_IP
            End Get
        End Property

        Public ReadOnly Property UserID As Field(Of String) 
            Get
                Return FI_UserID
            End Get
        End Property

        Public ReadOnly Property SeqNo As Field(Of Integer) 
            Get
                Return FI_SeqNo
            End Get
        End Property

        Public ReadOnly Property Message As Field(Of String) 
            Get
                Return FI_Message
            End Get
        End Property

        Public ReadOnly Property Priority As Field(Of Integer) 
            Get
                Return FI_Priority
            End Get
        End Property

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal SC_OnlineUserMsgRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_OnlineUserMsg")
            strSQL.AppendLine("Where IP = @IP")
            strSQL.AppendLine("And UserID = @UserID")
            strSQL.AppendLine("And SeqNo = @SeqNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IP", DbType.String, SC_OnlineUserMsgRow.IP.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_OnlineUserMsgRow.UserID.Value)
            db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, SC_OnlineUserMsgRow.SeqNo.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal SC_OnlineUserMsgRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_OnlineUserMsg")
            strSQL.AppendLine("Where IP = @IP")
            strSQL.AppendLine("And UserID = @UserID")
            strSQL.AppendLine("And SeqNo = @SeqNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IP", DbType.String, SC_OnlineUserMsgRow.IP.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_OnlineUserMsgRow.UserID.Value)
            db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, SC_OnlineUserMsgRow.SeqNo.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal SC_OnlineUserMsgRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_OnlineUserMsg")
            strSQL.AppendLine("Where IP = @IP")
            strSQL.AppendLine("And UserID = @UserID")
            strSQL.AppendLine("And SeqNo = @SeqNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_OnlineUserMsgRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@IP", DbType.String, r.IP.Value)
                        db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                        db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, r.SeqNo.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal SC_OnlineUserMsgRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_OnlineUserMsg")
            strSQL.AppendLine("Where IP = @IP")
            strSQL.AppendLine("And UserID = @UserID")
            strSQL.AppendLine("And SeqNo = @SeqNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_OnlineUserMsgRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@IP", DbType.String, r.IP.Value)
                db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, r.SeqNo.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal SC_OnlineUserMsgRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_OnlineUserMsg")
            strSQL.AppendLine("Where IP = @IP")
            strSQL.AppendLine("And UserID = @UserID")
            strSQL.AppendLine("And SeqNo = @SeqNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IP", DbType.String, SC_OnlineUserMsgRow.IP.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_OnlineUserMsgRow.UserID.Value)
            db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, SC_OnlineUserMsgRow.SeqNo.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(SC_OnlineUserMsgRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_OnlineUserMsg")
            strSQL.AppendLine("Where IP = @IP")
            strSQL.AppendLine("And UserID = @UserID")
            strSQL.AppendLine("And SeqNo = @SeqNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IP", DbType.String, SC_OnlineUserMsgRow.IP.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_OnlineUserMsgRow.UserID.Value)
            db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, SC_OnlineUserMsgRow.SeqNo.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_OnlineUserMsgRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_OnlineUserMsg Set")
            For i As Integer = 0 To SC_OnlineUserMsgRow.FieldNames.Length - 1
                If Not SC_OnlineUserMsgRow.IsIdentityField(SC_OnlineUserMsgRow.FieldNames(i)) AndAlso SC_OnlineUserMsgRow.IsUpdated(SC_OnlineUserMsgRow.FieldNames(i)) AndAlso SC_OnlineUserMsgRow.CreateUpdateSQL(SC_OnlineUserMsgRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_OnlineUserMsgRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where IP = @PKIP")
            strSQL.AppendLine("And UserID = @PKUserID")
            strSQL.AppendLine("And SeqNo = @PKSeqNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_OnlineUserMsgRow.IP.Updated Then db.AddInParameter(dbcmd, "@IP", DbType.String, SC_OnlineUserMsgRow.IP.Value)
            If SC_OnlineUserMsgRow.UserID.Updated Then db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_OnlineUserMsgRow.UserID.Value)
            If SC_OnlineUserMsgRow.SeqNo.Updated Then db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, SC_OnlineUserMsgRow.SeqNo.Value)
            If SC_OnlineUserMsgRow.Message.Updated Then db.AddInParameter(dbcmd, "@Message", DbType.String, SC_OnlineUserMsgRow.Message.Value)
            If SC_OnlineUserMsgRow.Priority.Updated Then db.AddInParameter(dbcmd, "@Priority", DbType.Int32, SC_OnlineUserMsgRow.Priority.Value)
            db.AddInParameter(dbcmd, "@PKIP", DbType.String, IIf(SC_OnlineUserMsgRow.LoadFromDataRow, SC_OnlineUserMsgRow.IP.OldValue, SC_OnlineUserMsgRow.IP.Value))
            db.AddInParameter(dbcmd, "@PKUserID", DbType.String, IIf(SC_OnlineUserMsgRow.LoadFromDataRow, SC_OnlineUserMsgRow.UserID.OldValue, SC_OnlineUserMsgRow.UserID.Value))
            db.AddInParameter(dbcmd, "@PKSeqNo", DbType.Int32, IIf(SC_OnlineUserMsgRow.LoadFromDataRow, SC_OnlineUserMsgRow.SeqNo.OldValue, SC_OnlineUserMsgRow.SeqNo.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal SC_OnlineUserMsgRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_OnlineUserMsg Set")
            For i As Integer = 0 To SC_OnlineUserMsgRow.FieldNames.Length - 1
                If Not SC_OnlineUserMsgRow.IsIdentityField(SC_OnlineUserMsgRow.FieldNames(i)) AndAlso SC_OnlineUserMsgRow.IsUpdated(SC_OnlineUserMsgRow.FieldNames(i)) AndAlso SC_OnlineUserMsgRow.CreateUpdateSQL(SC_OnlineUserMsgRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_OnlineUserMsgRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where IP = @PKIP")
            strSQL.AppendLine("And UserID = @PKUserID")
            strSQL.AppendLine("And SeqNo = @PKSeqNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_OnlineUserMsgRow.IP.Updated Then db.AddInParameter(dbcmd, "@IP", DbType.String, SC_OnlineUserMsgRow.IP.Value)
            If SC_OnlineUserMsgRow.UserID.Updated Then db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_OnlineUserMsgRow.UserID.Value)
            If SC_OnlineUserMsgRow.SeqNo.Updated Then db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, SC_OnlineUserMsgRow.SeqNo.Value)
            If SC_OnlineUserMsgRow.Message.Updated Then db.AddInParameter(dbcmd, "@Message", DbType.String, SC_OnlineUserMsgRow.Message.Value)
            If SC_OnlineUserMsgRow.Priority.Updated Then db.AddInParameter(dbcmd, "@Priority", DbType.Int32, SC_OnlineUserMsgRow.Priority.Value)
            db.AddInParameter(dbcmd, "@PKIP", DbType.String, IIf(SC_OnlineUserMsgRow.LoadFromDataRow, SC_OnlineUserMsgRow.IP.OldValue, SC_OnlineUserMsgRow.IP.Value))
            db.AddInParameter(dbcmd, "@PKUserID", DbType.String, IIf(SC_OnlineUserMsgRow.LoadFromDataRow, SC_OnlineUserMsgRow.UserID.OldValue, SC_OnlineUserMsgRow.UserID.Value))
            db.AddInParameter(dbcmd, "@PKSeqNo", DbType.Int32, IIf(SC_OnlineUserMsgRow.LoadFromDataRow, SC_OnlineUserMsgRow.SeqNo.OldValue, SC_OnlineUserMsgRow.SeqNo.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_OnlineUserMsgRow As Row()) As Integer
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
                    For Each r As Row In SC_OnlineUserMsgRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update SC_OnlineUserMsg Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where IP = @PKIP")
                        strSQL.AppendLine("And UserID = @PKUserID")
                        strSQL.AppendLine("And SeqNo = @PKSeqNo")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.IP.Updated Then db.AddInParameter(dbcmd, "@IP", DbType.String, r.IP.Value)
                        If r.UserID.Updated Then db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                        If r.SeqNo.Updated Then db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, r.SeqNo.Value)
                        If r.Message.Updated Then db.AddInParameter(dbcmd, "@Message", DbType.String, r.Message.Value)
                        If r.Priority.Updated Then db.AddInParameter(dbcmd, "@Priority", DbType.Int32, r.Priority.Value)
                        db.AddInParameter(dbcmd, "@PKIP", DbType.String, IIf(r.LoadFromDataRow, r.IP.OldValue, r.IP.Value))
                        db.AddInParameter(dbcmd, "@PKUserID", DbType.String, IIf(r.LoadFromDataRow, r.UserID.OldValue, r.UserID.Value))
                        db.AddInParameter(dbcmd, "@PKSeqNo", DbType.Int32, IIf(r.LoadFromDataRow, r.SeqNo.OldValue, r.SeqNo.Value))

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

        Public Function Update(ByVal SC_OnlineUserMsgRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In SC_OnlineUserMsgRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update SC_OnlineUserMsg Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where IP = @PKIP")
                strSQL.AppendLine("And UserID = @PKUserID")
                strSQL.AppendLine("And SeqNo = @PKSeqNo")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.IP.Updated Then db.AddInParameter(dbcmd, "@IP", DbType.String, r.IP.Value)
                If r.UserID.Updated Then db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                If r.SeqNo.Updated Then db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, r.SeqNo.Value)
                If r.Message.Updated Then db.AddInParameter(dbcmd, "@Message", DbType.String, r.Message.Value)
                If r.Priority.Updated Then db.AddInParameter(dbcmd, "@Priority", DbType.Int32, r.Priority.Value)
                db.AddInParameter(dbcmd, "@PKIP", DbType.String, IIf(r.LoadFromDataRow, r.IP.OldValue, r.IP.Value))
                db.AddInParameter(dbcmd, "@PKUserID", DbType.String, IIf(r.LoadFromDataRow, r.UserID.OldValue, r.UserID.Value))
                db.AddInParameter(dbcmd, "@PKSeqNo", DbType.Int32, IIf(r.LoadFromDataRow, r.SeqNo.OldValue, r.SeqNo.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal SC_OnlineUserMsgRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_OnlineUserMsg")
            strSQL.AppendLine("Where IP = @IP")
            strSQL.AppendLine("And UserID = @UserID")
            strSQL.AppendLine("And SeqNo = @SeqNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IP", DbType.String, SC_OnlineUserMsgRow.IP.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_OnlineUserMsgRow.UserID.Value)
            db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, SC_OnlineUserMsgRow.SeqNo.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal SC_OnlineUserMsgRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_OnlineUserMsg")
            strSQL.AppendLine("Where IP = @IP")
            strSQL.AppendLine("And UserID = @UserID")
            strSQL.AppendLine("And SeqNo = @SeqNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IP", DbType.String, SC_OnlineUserMsgRow.IP.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_OnlineUserMsgRow.UserID.Value)
            db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, SC_OnlineUserMsgRow.SeqNo.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_OnlineUserMsg")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal SC_OnlineUserMsgRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_OnlineUserMsg")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    IP, UserID, SeqNo, Message, Priority")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @IP, @UserID, @SeqNo, @Message, @Priority")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IP", DbType.String, SC_OnlineUserMsgRow.IP.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_OnlineUserMsgRow.UserID.Value)
            db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, SC_OnlineUserMsgRow.SeqNo.Value)
            db.AddInParameter(dbcmd, "@Message", DbType.String, SC_OnlineUserMsgRow.Message.Value)
            db.AddInParameter(dbcmd, "@Priority", DbType.Int32, SC_OnlineUserMsgRow.Priority.Value)

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal SC_OnlineUserMsgRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_OnlineUserMsg")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    IP, UserID, SeqNo, Message, Priority")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @IP, @UserID, @SeqNo, @Message, @Priority")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IP", DbType.String, SC_OnlineUserMsgRow.IP.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_OnlineUserMsgRow.UserID.Value)
            db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, SC_OnlineUserMsgRow.SeqNo.Value)
            db.AddInParameter(dbcmd, "@Message", DbType.String, SC_OnlineUserMsgRow.Message.Value)
            db.AddInParameter(dbcmd, "@Priority", DbType.Int32, SC_OnlineUserMsgRow.Priority.Value)

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal SC_OnlineUserMsgRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_OnlineUserMsg")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    IP, UserID, SeqNo, Message, Priority")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @IP, @UserID, @SeqNo, @Message, @Priority")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_OnlineUserMsgRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@IP", DbType.String, r.IP.Value)
                        db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                        db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, r.SeqNo.Value)
                        db.AddInParameter(dbcmd, "@Message", DbType.String, r.Message.Value)
                        db.AddInParameter(dbcmd, "@Priority", DbType.Int32, r.Priority.Value)

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

        Public Function Insert(ByVal SC_OnlineUserMsgRow As Row(), ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_OnlineUserMsg")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    IP, UserID, SeqNo, Message, Priority")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @IP, @UserID, @SeqNo, @Message, @Priority")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_OnlineUserMsgRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@IP", DbType.String, r.IP.Value)
                db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, r.SeqNo.Value)
                db.AddInParameter(dbcmd, "@Message", DbType.String, r.Message.Value)
                db.AddInParameter(dbcmd, "@Priority", DbType.Int32, r.Priority.Value)

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

