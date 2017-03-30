'****************************************************************
' Table:WF_Phrase
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

Namespace beWF_Phrase
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "FlowID", "FlowStepID", "UserID", "SeqNo", "FlowPhrase", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(Integer), GetType(String), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "FlowID", "FlowStepID", "UserID", "SeqNo" }

        Public ReadOnly Property Rows() As beWF_Phrase.Rows 
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
        Public Sub Transfer2Row(WF_PhraseTable As DataTable)
            For Each dr As DataRow In WF_PhraseTable.Rows
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

                dr(m_Rows(i).FlowID.FieldName) = m_Rows(i).FlowID.Value
                dr(m_Rows(i).FlowStepID.FieldName) = m_Rows(i).FlowStepID.Value
                dr(m_Rows(i).UserID.FieldName) = m_Rows(i).UserID.Value
                dr(m_Rows(i).SeqNo.FieldName) = m_Rows(i).SeqNo.Value
                dr(m_Rows(i).FlowPhrase.FieldName) = m_Rows(i).FlowPhrase.Value
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

        Public Sub Add(WF_PhraseRow As Row)
            m_Rows.Add(WF_PhraseRow)
        End Sub

        Public Sub Remove(WF_PhraseRow As Row)
            If m_Rows.IndexOf(WF_PhraseRow) >= 0 Then
                m_Rows.Remove(WF_PhraseRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_FlowID As Field(Of String) = new Field(Of String)("FlowID", true)
        Private FI_FlowStepID As Field(Of String) = new Field(Of String)("FlowStepID", true)
        Private FI_UserID As Field(Of String) = new Field(Of String)("UserID", true)
        Private FI_SeqNo As Field(Of Integer) = new Field(Of Integer)("SeqNo", true)
        Private FI_FlowPhrase As Field(Of String) = new Field(Of String)("FlowPhrase", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "FlowID", "FlowStepID", "UserID", "SeqNo", "FlowPhrase", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "FlowID", "FlowStepID", "UserID", "SeqNo" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "FlowID"
                    Return FI_FlowID.Value
                Case "FlowStepID"
                    Return FI_FlowStepID.Value
                Case "UserID"
                    Return FI_UserID.Value
                Case "SeqNo"
                    Return FI_SeqNo.Value
                Case "FlowPhrase"
                    Return FI_FlowPhrase.Value
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
                Case "FlowID"
                    FI_FlowID.SetValue(value)
                Case "FlowStepID"
                    FI_FlowStepID.SetValue(value)
                Case "UserID"
                    FI_UserID.SetValue(value)
                Case "SeqNo"
                    FI_SeqNo.SetValue(value)
                Case "FlowPhrase"
                    FI_FlowPhrase.SetValue(value)
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
                Case "FlowID"
                    return FI_FlowID.Updated
                Case "FlowStepID"
                    return FI_FlowStepID.Updated
                Case "UserID"
                    return FI_UserID.Updated
                Case "SeqNo"
                    return FI_SeqNo.Updated
                Case "FlowPhrase"
                    return FI_FlowPhrase.Updated
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
                Case "FlowID"
                    return FI_FlowID.CreateUpdateSQL
                Case "FlowStepID"
                    return FI_FlowStepID.CreateUpdateSQL
                Case "UserID"
                    return FI_UserID.CreateUpdateSQL
                Case "SeqNo"
                    return FI_SeqNo.CreateUpdateSQL
                Case "FlowPhrase"
                    return FI_FlowPhrase.CreateUpdateSQL
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
            FI_FlowID.SetInitValue("")
            FI_FlowStepID.SetInitValue("")
            FI_UserID.SetInitValue("")
            FI_SeqNo.SetInitValue(0)
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_FlowID.SetInitValue(dr("FlowID"))
            FI_FlowStepID.SetInitValue(dr("FlowStepID"))
            FI_UserID.SetInitValue(dr("UserID"))
            FI_SeqNo.SetInitValue(dr("SeqNo"))
            FI_FlowPhrase.SetInitValue(dr("FlowPhrase"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_FlowID.Updated = False
            FI_FlowStepID.Updated = False
            FI_UserID.Updated = False
            FI_SeqNo.Updated = False
            FI_FlowPhrase.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
        End Sub

        Public ReadOnly Property FlowID As Field(Of String) 
            Get
                Return FI_FlowID
            End Get
        End Property

        Public ReadOnly Property FlowStepID As Field(Of String) 
            Get
                Return FI_FlowStepID
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

        Public ReadOnly Property FlowPhrase As Field(Of String) 
            Get
                Return FI_FlowPhrase
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
        Public Function DeleteRowByPrimaryKey(ByVal WF_PhraseRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From WF_Phrase")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowStepID = @FlowStepID")
            strSQL.AppendLine("And UserID = @UserID")
            strSQL.AppendLine("And SeqNo = @SeqNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_PhraseRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, WF_PhraseRow.FlowStepID.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, WF_PhraseRow.UserID.Value)
            db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, WF_PhraseRow.SeqNo.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal WF_PhraseRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From WF_Phrase")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowStepID = @FlowStepID")
            strSQL.AppendLine("And UserID = @UserID")
            strSQL.AppendLine("And SeqNo = @SeqNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_PhraseRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, WF_PhraseRow.FlowStepID.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, WF_PhraseRow.UserID.Value)
            db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, WF_PhraseRow.SeqNo.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal WF_PhraseRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From WF_Phrase")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowStepID = @FlowStepID")
            strSQL.AppendLine("And UserID = @UserID")
            strSQL.AppendLine("And SeqNo = @SeqNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In WF_PhraseRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                        db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, r.FlowStepID.Value)
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

        Public Function DeleteRowByPrimaryKey(ByVal WF_PhraseRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From WF_Phrase")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowStepID = @FlowStepID")
            strSQL.AppendLine("And UserID = @UserID")
            strSQL.AppendLine("And SeqNo = @SeqNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In WF_PhraseRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, r.FlowStepID.Value)
                db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, r.SeqNo.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal WF_PhraseRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WF_Phrase")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowStepID = @FlowStepID")
            strSQL.AppendLine("And UserID = @UserID")
            strSQL.AppendLine("And SeqNo = @SeqNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_PhraseRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, WF_PhraseRow.FlowStepID.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, WF_PhraseRow.UserID.Value)
            db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, WF_PhraseRow.SeqNo.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(WF_PhraseRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WF_Phrase")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowStepID = @FlowStepID")
            strSQL.AppendLine("And UserID = @UserID")
            strSQL.AppendLine("And SeqNo = @SeqNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_PhraseRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, WF_PhraseRow.FlowStepID.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, WF_PhraseRow.UserID.Value)
            db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, WF_PhraseRow.SeqNo.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal WF_PhraseRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update WF_Phrase Set")
            For i As Integer = 0 To WF_PhraseRow.FieldNames.Length - 1
                If Not WF_PhraseRow.IsIdentityField(WF_PhraseRow.FieldNames(i)) AndAlso WF_PhraseRow.IsUpdated(WF_PhraseRow.FieldNames(i)) AndAlso WF_PhraseRow.CreateUpdateSQL(WF_PhraseRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, WF_PhraseRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where FlowID = @PKFlowID")
            strSQL.AppendLine("And FlowStepID = @PKFlowStepID")
            strSQL.AppendLine("And UserID = @PKUserID")
            strSQL.AppendLine("And SeqNo = @PKSeqNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If WF_PhraseRow.FlowID.Updated Then db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_PhraseRow.FlowID.Value)
            If WF_PhraseRow.FlowStepID.Updated Then db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, WF_PhraseRow.FlowStepID.Value)
            If WF_PhraseRow.UserID.Updated Then db.AddInParameter(dbcmd, "@UserID", DbType.String, WF_PhraseRow.UserID.Value)
            If WF_PhraseRow.SeqNo.Updated Then db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, WF_PhraseRow.SeqNo.Value)
            If WF_PhraseRow.FlowPhrase.Updated Then db.AddInParameter(dbcmd, "@FlowPhrase", DbType.String, WF_PhraseRow.FlowPhrase.Value)
            If WF_PhraseRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WF_PhraseRow.LastChgID.Value)
            If WF_PhraseRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WF_PhraseRow.LastChgDate.Value), DBNull.Value, WF_PhraseRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKFlowID", DbType.String, IIf(WF_PhraseRow.LoadFromDataRow, WF_PhraseRow.FlowID.OldValue, WF_PhraseRow.FlowID.Value))
            db.AddInParameter(dbcmd, "@PKFlowStepID", DbType.String, IIf(WF_PhraseRow.LoadFromDataRow, WF_PhraseRow.FlowStepID.OldValue, WF_PhraseRow.FlowStepID.Value))
            db.AddInParameter(dbcmd, "@PKUserID", DbType.String, IIf(WF_PhraseRow.LoadFromDataRow, WF_PhraseRow.UserID.OldValue, WF_PhraseRow.UserID.Value))
            db.AddInParameter(dbcmd, "@PKSeqNo", DbType.Int32, IIf(WF_PhraseRow.LoadFromDataRow, WF_PhraseRow.SeqNo.OldValue, WF_PhraseRow.SeqNo.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal WF_PhraseRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update WF_Phrase Set")
            For i As Integer = 0 To WF_PhraseRow.FieldNames.Length - 1
                If Not WF_PhraseRow.IsIdentityField(WF_PhraseRow.FieldNames(i)) AndAlso WF_PhraseRow.IsUpdated(WF_PhraseRow.FieldNames(i)) AndAlso WF_PhraseRow.CreateUpdateSQL(WF_PhraseRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, WF_PhraseRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where FlowID = @PKFlowID")
            strSQL.AppendLine("And FlowStepID = @PKFlowStepID")
            strSQL.AppendLine("And UserID = @PKUserID")
            strSQL.AppendLine("And SeqNo = @PKSeqNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If WF_PhraseRow.FlowID.Updated Then db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_PhraseRow.FlowID.Value)
            If WF_PhraseRow.FlowStepID.Updated Then db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, WF_PhraseRow.FlowStepID.Value)
            If WF_PhraseRow.UserID.Updated Then db.AddInParameter(dbcmd, "@UserID", DbType.String, WF_PhraseRow.UserID.Value)
            If WF_PhraseRow.SeqNo.Updated Then db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, WF_PhraseRow.SeqNo.Value)
            If WF_PhraseRow.FlowPhrase.Updated Then db.AddInParameter(dbcmd, "@FlowPhrase", DbType.String, WF_PhraseRow.FlowPhrase.Value)
            If WF_PhraseRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WF_PhraseRow.LastChgID.Value)
            If WF_PhraseRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WF_PhraseRow.LastChgDate.Value), DBNull.Value, WF_PhraseRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKFlowID", DbType.String, IIf(WF_PhraseRow.LoadFromDataRow, WF_PhraseRow.FlowID.OldValue, WF_PhraseRow.FlowID.Value))
            db.AddInParameter(dbcmd, "@PKFlowStepID", DbType.String, IIf(WF_PhraseRow.LoadFromDataRow, WF_PhraseRow.FlowStepID.OldValue, WF_PhraseRow.FlowStepID.Value))
            db.AddInParameter(dbcmd, "@PKUserID", DbType.String, IIf(WF_PhraseRow.LoadFromDataRow, WF_PhraseRow.UserID.OldValue, WF_PhraseRow.UserID.Value))
            db.AddInParameter(dbcmd, "@PKSeqNo", DbType.Int32, IIf(WF_PhraseRow.LoadFromDataRow, WF_PhraseRow.SeqNo.OldValue, WF_PhraseRow.SeqNo.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal WF_PhraseRow As Row()) As Integer
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
                    For Each r As Row In WF_PhraseRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update WF_Phrase Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where FlowID = @PKFlowID")
                        strSQL.AppendLine("And FlowStepID = @PKFlowStepID")
                        strSQL.AppendLine("And UserID = @PKUserID")
                        strSQL.AppendLine("And SeqNo = @PKSeqNo")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.FlowID.Updated Then db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                        If r.FlowStepID.Updated Then db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, r.FlowStepID.Value)
                        If r.UserID.Updated Then db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                        If r.SeqNo.Updated Then db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, r.SeqNo.Value)
                        If r.FlowPhrase.Updated Then db.AddInParameter(dbcmd, "@FlowPhrase", DbType.String, r.FlowPhrase.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), DBNull.Value, r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKFlowID", DbType.String, IIf(r.LoadFromDataRow, r.FlowID.OldValue, r.FlowID.Value))
                        db.AddInParameter(dbcmd, "@PKFlowStepID", DbType.String, IIf(r.LoadFromDataRow, r.FlowStepID.OldValue, r.FlowStepID.Value))
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

        Public Function Update(ByVal WF_PhraseRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In WF_PhraseRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update WF_Phrase Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where FlowID = @PKFlowID")
                strSQL.AppendLine("And FlowStepID = @PKFlowStepID")
                strSQL.AppendLine("And UserID = @PKUserID")
                strSQL.AppendLine("And SeqNo = @PKSeqNo")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.FlowID.Updated Then db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                If r.FlowStepID.Updated Then db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, r.FlowStepID.Value)
                If r.UserID.Updated Then db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                If r.SeqNo.Updated Then db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, r.SeqNo.Value)
                If r.FlowPhrase.Updated Then db.AddInParameter(dbcmd, "@FlowPhrase", DbType.String, r.FlowPhrase.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), DBNull.Value, r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKFlowID", DbType.String, IIf(r.LoadFromDataRow, r.FlowID.OldValue, r.FlowID.Value))
                db.AddInParameter(dbcmd, "@PKFlowStepID", DbType.String, IIf(r.LoadFromDataRow, r.FlowStepID.OldValue, r.FlowStepID.Value))
                db.AddInParameter(dbcmd, "@PKUserID", DbType.String, IIf(r.LoadFromDataRow, r.UserID.OldValue, r.UserID.Value))
                db.AddInParameter(dbcmd, "@PKSeqNo", DbType.Int32, IIf(r.LoadFromDataRow, r.SeqNo.OldValue, r.SeqNo.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal WF_PhraseRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From WF_Phrase")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowStepID = @FlowStepID")
            strSQL.AppendLine("And UserID = @UserID")
            strSQL.AppendLine("And SeqNo = @SeqNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_PhraseRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, WF_PhraseRow.FlowStepID.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, WF_PhraseRow.UserID.Value)
            db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, WF_PhraseRow.SeqNo.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal WF_PhraseRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From WF_Phrase")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowStepID = @FlowStepID")
            strSQL.AppendLine("And UserID = @UserID")
            strSQL.AppendLine("And SeqNo = @SeqNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_PhraseRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, WF_PhraseRow.FlowStepID.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, WF_PhraseRow.UserID.Value)
            db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, WF_PhraseRow.SeqNo.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WF_Phrase")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal WF_PhraseRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into WF_Phrase")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowID, FlowStepID, UserID, SeqNo, FlowPhrase, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowID, @FlowStepID, @UserID, @SeqNo, @FlowPhrase, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_PhraseRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, WF_PhraseRow.FlowStepID.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, WF_PhraseRow.UserID.Value)
            db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, WF_PhraseRow.SeqNo.Value)
            db.AddInParameter(dbcmd, "@FlowPhrase", DbType.String, WF_PhraseRow.FlowPhrase.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WF_PhraseRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WF_PhraseRow.LastChgDate.Value), DBNull.Value, WF_PhraseRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal WF_PhraseRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into WF_Phrase")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowID, FlowStepID, UserID, SeqNo, FlowPhrase, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowID, @FlowStepID, @UserID, @SeqNo, @FlowPhrase, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_PhraseRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, WF_PhraseRow.FlowStepID.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, WF_PhraseRow.UserID.Value)
            db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, WF_PhraseRow.SeqNo.Value)
            db.AddInParameter(dbcmd, "@FlowPhrase", DbType.String, WF_PhraseRow.FlowPhrase.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WF_PhraseRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WF_PhraseRow.LastChgDate.Value), DBNull.Value, WF_PhraseRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal WF_PhraseRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into WF_Phrase")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowID, FlowStepID, UserID, SeqNo, FlowPhrase, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowID, @FlowStepID, @UserID, @SeqNo, @FlowPhrase, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In WF_PhraseRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                        db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, r.FlowStepID.Value)
                        db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                        db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, r.SeqNo.Value)
                        db.AddInParameter(dbcmd, "@FlowPhrase", DbType.String, r.FlowPhrase.Value)
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

        Public Function Insert(ByVal WF_PhraseRow As Row(), ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into WF_Phrase")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowID, FlowStepID, UserID, SeqNo, FlowPhrase, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowID, @FlowStepID, @UserID, @SeqNo, @FlowPhrase, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In WF_PhraseRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, r.FlowStepID.Value)
                db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, r.SeqNo.Value)
                db.AddInParameter(dbcmd, "@FlowPhrase", DbType.String, r.FlowPhrase.Value)
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

