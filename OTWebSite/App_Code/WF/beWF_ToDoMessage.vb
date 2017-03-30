'****************************************************************
' Table:WF_ToDoMessage
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

Namespace beWF_ToDoMessage
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "MsgCaseID", "AssignTo", "MsgCode", "MsgReason", "MsgNote", "PaperID", "KeyValue", "Status", "CrDate", "CrUser" _
                                    , "CrBr", "CrBrName", "LastChgDate", "LastChgID", "LastChgDept", "LastChgDeptName" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(String) _
                                    , GetType(String), GetType(String), GetType(Date), GetType(String), GetType(String), GetType(String) }
        Private m_PrimaryFields As String() = { "MsgCaseID" }

        Public ReadOnly Property Rows() As beWF_ToDoMessage.Rows 
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
        Public Sub Transfer2Row(WF_ToDoMessageTable As DataTable)
            For Each dr As DataRow In WF_ToDoMessageTable.Rows
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

                dr(m_Rows(i).MsgCaseID.FieldName) = m_Rows(i).MsgCaseID.Value
                dr(m_Rows(i).AssignTo.FieldName) = m_Rows(i).AssignTo.Value
                dr(m_Rows(i).MsgCode.FieldName) = m_Rows(i).MsgCode.Value
                dr(m_Rows(i).MsgReason.FieldName) = m_Rows(i).MsgReason.Value
                dr(m_Rows(i).MsgNote.FieldName) = m_Rows(i).MsgNote.Value
                dr(m_Rows(i).PaperID.FieldName) = m_Rows(i).PaperID.Value
                dr(m_Rows(i).KeyValue.FieldName) = m_Rows(i).KeyValue.Value
                dr(m_Rows(i).Status.FieldName) = m_Rows(i).Status.Value
                dr(m_Rows(i).CrDate.FieldName) = m_Rows(i).CrDate.Value
                dr(m_Rows(i).CrUser.FieldName) = m_Rows(i).CrUser.Value
                dr(m_Rows(i).CrBr.FieldName) = m_Rows(i).CrBr.Value
                dr(m_Rows(i).CrBrName.FieldName) = m_Rows(i).CrBrName.Value
                dr(m_Rows(i).LastChgDate.FieldName) = m_Rows(i).LastChgDate.Value
                dr(m_Rows(i).LastChgID.FieldName) = m_Rows(i).LastChgID.Value
                dr(m_Rows(i).LastChgDept.FieldName) = m_Rows(i).LastChgDept.Value
                dr(m_Rows(i).LastChgDeptName.FieldName) = m_Rows(i).LastChgDeptName.Value

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

        Public Sub Add(WF_ToDoMessageRow As Row)
            m_Rows.Add(WF_ToDoMessageRow)
        End Sub

        Public Sub Remove(WF_ToDoMessageRow As Row)
            If m_Rows.IndexOf(WF_ToDoMessageRow) >= 0 Then
                m_Rows.Remove(WF_ToDoMessageRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_MsgCaseID As Field(Of String) = new Field(Of String)("MsgCaseID", true)
        Private FI_AssignTo As Field(Of String) = new Field(Of String)("AssignTo", true)
        Private FI_MsgCode As Field(Of String) = new Field(Of String)("MsgCode", true)
        Private FI_MsgReason As Field(Of String) = new Field(Of String)("MsgReason", true)
        Private FI_MsgNote As Field(Of String) = new Field(Of String)("MsgNote", true)
        Private FI_PaperID As Field(Of String) = new Field(Of String)("PaperID", true)
        Private FI_KeyValue As Field(Of String) = new Field(Of String)("KeyValue", true)
        Private FI_Status As Field(Of String) = new Field(Of String)("Status", true)
        Private FI_CrDate As Field(Of Date) = new Field(Of Date)("CrDate", true)
        Private FI_CrUser As Field(Of String) = new Field(Of String)("CrUser", true)
        Private FI_CrBr As Field(Of String) = new Field(Of String)("CrBr", true)
        Private FI_CrBrName As Field(Of String) = new Field(Of String)("CrBrName", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDept As Field(Of String) = new Field(Of String)("LastChgDept", true)
        Private FI_LastChgDeptName As Field(Of String) = new Field(Of String)("LastChgDeptName", true)
        Private m_FieldNames As String() = { "MsgCaseID", "AssignTo", "MsgCode", "MsgReason", "MsgNote", "PaperID", "KeyValue", "Status", "CrDate", "CrUser" _
                                    , "CrBr", "CrBrName", "LastChgDate", "LastChgID", "LastChgDept", "LastChgDeptName" }
        Private m_PrimaryFields As String() = { "MsgCaseID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "MsgCaseID"
                    Return FI_MsgCaseID.Value
                Case "AssignTo"
                    Return FI_AssignTo.Value
                Case "MsgCode"
                    Return FI_MsgCode.Value
                Case "MsgReason"
                    Return FI_MsgReason.Value
                Case "MsgNote"
                    Return FI_MsgNote.Value
                Case "PaperID"
                    Return FI_PaperID.Value
                Case "KeyValue"
                    Return FI_KeyValue.Value
                Case "Status"
                    Return FI_Status.Value
                Case "CrDate"
                    Return FI_CrDate.Value
                Case "CrUser"
                    Return FI_CrUser.Value
                Case "CrBr"
                    Return FI_CrBr.Value
                Case "CrBrName"
                    Return FI_CrBrName.Value
                Case "LastChgDate"
                    Return FI_LastChgDate.Value
                Case "LastChgID"
                    Return FI_LastChgID.Value
                Case "LastChgDept"
                    Return FI_LastChgDept.Value
                Case "LastChgDeptName"
                    Return FI_LastChgDeptName.Value
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Sub SetFieldValue(ByVal fieldName As String, ByVal value As Object)
            Select Case fieldName
                Case "MsgCaseID"
                    FI_MsgCaseID.SetValue(value)
                Case "AssignTo"
                    FI_AssignTo.SetValue(value)
                Case "MsgCode"
                    FI_MsgCode.SetValue(value)
                Case "MsgReason"
                    FI_MsgReason.SetValue(value)
                Case "MsgNote"
                    FI_MsgNote.SetValue(value)
                Case "PaperID"
                    FI_PaperID.SetValue(value)
                Case "KeyValue"
                    FI_KeyValue.SetValue(value)
                Case "Status"
                    FI_Status.SetValue(value)
                Case "CrDate"
                    FI_CrDate.SetValue(value)
                Case "CrUser"
                    FI_CrUser.SetValue(value)
                Case "CrBr"
                    FI_CrBr.SetValue(value)
                Case "CrBrName"
                    FI_CrBrName.SetValue(value)
                Case "LastChgDate"
                    FI_LastChgDate.SetValue(value)
                Case "LastChgID"
                    FI_LastChgID.SetValue(value)
                Case "LastChgDept"
                    FI_LastChgDept.SetValue(value)
                Case "LastChgDeptName"
                    FI_LastChgDeptName.SetValue(value)
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
                Case "MsgCaseID"
                    return FI_MsgCaseID.Updated
                Case "AssignTo"
                    return FI_AssignTo.Updated
                Case "MsgCode"
                    return FI_MsgCode.Updated
                Case "MsgReason"
                    return FI_MsgReason.Updated
                Case "MsgNote"
                    return FI_MsgNote.Updated
                Case "PaperID"
                    return FI_PaperID.Updated
                Case "KeyValue"
                    return FI_KeyValue.Updated
                Case "Status"
                    return FI_Status.Updated
                Case "CrDate"
                    return FI_CrDate.Updated
                Case "CrUser"
                    return FI_CrUser.Updated
                Case "CrBr"
                    return FI_CrBr.Updated
                Case "CrBrName"
                    return FI_CrBrName.Updated
                Case "LastChgDate"
                    return FI_LastChgDate.Updated
                Case "LastChgID"
                    return FI_LastChgID.Updated
                Case "LastChgDept"
                    return FI_LastChgDept.Updated
                Case "LastChgDeptName"
                    return FI_LastChgDeptName.Updated
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public Function CreateUpdateSQL(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "MsgCaseID"
                    return FI_MsgCaseID.CreateUpdateSQL
                Case "AssignTo"
                    return FI_AssignTo.CreateUpdateSQL
                Case "MsgCode"
                    return FI_MsgCode.CreateUpdateSQL
                Case "MsgReason"
                    return FI_MsgReason.CreateUpdateSQL
                Case "MsgNote"
                    return FI_MsgNote.CreateUpdateSQL
                Case "PaperID"
                    return FI_PaperID.CreateUpdateSQL
                Case "KeyValue"
                    return FI_KeyValue.CreateUpdateSQL
                Case "Status"
                    return FI_Status.CreateUpdateSQL
                Case "CrDate"
                    return FI_CrDate.CreateUpdateSQL
                Case "CrUser"
                    return FI_CrUser.CreateUpdateSQL
                Case "CrBr"
                    return FI_CrBr.CreateUpdateSQL
                Case "CrBrName"
                    return FI_CrBrName.CreateUpdateSQL
                Case "LastChgDate"
                    return FI_LastChgDate.CreateUpdateSQL
                Case "LastChgID"
                    return FI_LastChgID.CreateUpdateSQL
                Case "LastChgDept"
                    return FI_LastChgDept.CreateUpdateSQL
                Case "LastChgDeptName"
                    return FI_LastChgDeptName.CreateUpdateSQL
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
            FI_MsgCaseID.SetInitValue("")
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_MsgCaseID.SetInitValue(dr("MsgCaseID"))
            FI_AssignTo.SetInitValue(dr("AssignTo"))
            FI_MsgCode.SetInitValue(dr("MsgCode"))
            FI_MsgReason.SetInitValue(dr("MsgReason"))
            FI_MsgNote.SetInitValue(dr("MsgNote"))
            FI_PaperID.SetInitValue(dr("PaperID"))
            FI_KeyValue.SetInitValue(dr("KeyValue"))
            FI_Status.SetInitValue(dr("Status"))
            FI_CrDate.SetInitValue(dr("CrDate"))
            FI_CrUser.SetInitValue(dr("CrUser"))
            FI_CrBr.SetInitValue(dr("CrBr"))
            FI_CrBrName.SetInitValue(dr("CrBrName"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDept.SetInitValue(dr("LastChgDept"))
            FI_LastChgDeptName.SetInitValue(dr("LastChgDeptName"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_MsgCaseID.Updated = False
            FI_AssignTo.Updated = False
            FI_MsgCode.Updated = False
            FI_MsgReason.Updated = False
            FI_MsgNote.Updated = False
            FI_PaperID.Updated = False
            FI_KeyValue.Updated = False
            FI_Status.Updated = False
            FI_CrDate.Updated = False
            FI_CrUser.Updated = False
            FI_CrBr.Updated = False
            FI_CrBrName.Updated = False
            FI_LastChgDate.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDept.Updated = False
            FI_LastChgDeptName.Updated = False
        End Sub

        Public ReadOnly Property MsgCaseID As Field(Of String) 
            Get
                Return FI_MsgCaseID
            End Get
        End Property

        Public ReadOnly Property AssignTo As Field(Of String) 
            Get
                Return FI_AssignTo
            End Get
        End Property

        Public ReadOnly Property MsgCode As Field(Of String) 
            Get
                Return FI_MsgCode
            End Get
        End Property

        Public ReadOnly Property MsgReason As Field(Of String) 
            Get
                Return FI_MsgReason
            End Get
        End Property

        Public ReadOnly Property MsgNote As Field(Of String) 
            Get
                Return FI_MsgNote
            End Get
        End Property

        Public ReadOnly Property PaperID As Field(Of String) 
            Get
                Return FI_PaperID
            End Get
        End Property

        Public ReadOnly Property KeyValue As Field(Of String) 
            Get
                Return FI_KeyValue
            End Get
        End Property

        Public ReadOnly Property Status As Field(Of String) 
            Get
                Return FI_Status
            End Get
        End Property

        Public ReadOnly Property CrDate As Field(Of Date) 
            Get
                Return FI_CrDate
            End Get
        End Property

        Public ReadOnly Property CrUser As Field(Of String) 
            Get
                Return FI_CrUser
            End Get
        End Property

        Public ReadOnly Property CrBr As Field(Of String) 
            Get
                Return FI_CrBr
            End Get
        End Property

        Public ReadOnly Property CrBrName As Field(Of String) 
            Get
                Return FI_CrBrName
            End Get
        End Property

        Public ReadOnly Property LastChgDate As Field(Of Date) 
            Get
                Return FI_LastChgDate
            End Get
        End Property

        Public ReadOnly Property LastChgID As Field(Of String) 
            Get
                Return FI_LastChgID
            End Get
        End Property

        Public ReadOnly Property LastChgDept As Field(Of String) 
            Get
                Return FI_LastChgDept
            End Get
        End Property

        Public ReadOnly Property LastChgDeptName As Field(Of String) 
            Get
                Return FI_LastChgDeptName
            End Get
        End Property

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal WF_ToDoMessageRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From WF_ToDoMessage")
            strSQL.AppendLine("Where MsgCaseID = @MsgCaseID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@MsgCaseID", DbType.String, WF_ToDoMessageRow.MsgCaseID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal WF_ToDoMessageRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From WF_ToDoMessage")
            strSQL.AppendLine("Where MsgCaseID = @MsgCaseID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@MsgCaseID", DbType.String, WF_ToDoMessageRow.MsgCaseID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal WF_ToDoMessageRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From WF_ToDoMessage")
            strSQL.AppendLine("Where MsgCaseID = @MsgCaseID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In WF_ToDoMessageRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@MsgCaseID", DbType.String, r.MsgCaseID.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal WF_ToDoMessageRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From WF_ToDoMessage")
            strSQL.AppendLine("Where MsgCaseID = @MsgCaseID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In WF_ToDoMessageRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@MsgCaseID", DbType.String, r.MsgCaseID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal WF_ToDoMessageRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WF_ToDoMessage")
            strSQL.AppendLine("Where MsgCaseID = @MsgCaseID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@MsgCaseID", DbType.String, WF_ToDoMessageRow.MsgCaseID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(WF_ToDoMessageRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WF_ToDoMessage")
            strSQL.AppendLine("Where MsgCaseID = @MsgCaseID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@MsgCaseID", DbType.String, WF_ToDoMessageRow.MsgCaseID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal WF_ToDoMessageRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update WF_ToDoMessage Set")
            For i As Integer = 0 To WF_ToDoMessageRow.FieldNames.Length - 1
                If Not WF_ToDoMessageRow.IsIdentityField(WF_ToDoMessageRow.FieldNames(i)) AndAlso WF_ToDoMessageRow.IsUpdated(WF_ToDoMessageRow.FieldNames(i)) AndAlso WF_ToDoMessageRow.CreateUpdateSQL(WF_ToDoMessageRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, WF_ToDoMessageRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where MsgCaseID = @PKMsgCaseID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If WF_ToDoMessageRow.MsgCaseID.Updated Then db.AddInParameter(dbcmd, "@MsgCaseID", DbType.String, WF_ToDoMessageRow.MsgCaseID.Value)
            If WF_ToDoMessageRow.AssignTo.Updated Then db.AddInParameter(dbcmd, "@AssignTo", DbType.String, WF_ToDoMessageRow.AssignTo.Value)
            If WF_ToDoMessageRow.MsgCode.Updated Then db.AddInParameter(dbcmd, "@MsgCode", DbType.String, WF_ToDoMessageRow.MsgCode.Value)
            If WF_ToDoMessageRow.MsgReason.Updated Then db.AddInParameter(dbcmd, "@MsgReason", DbType.String, WF_ToDoMessageRow.MsgReason.Value)
            If WF_ToDoMessageRow.MsgNote.Updated Then db.AddInParameter(dbcmd, "@MsgNote", DbType.String, WF_ToDoMessageRow.MsgNote.Value)
            If WF_ToDoMessageRow.PaperID.Updated Then db.AddInParameter(dbcmd, "@PaperID", DbType.String, WF_ToDoMessageRow.PaperID.Value)
            If WF_ToDoMessageRow.KeyValue.Updated Then db.AddInParameter(dbcmd, "@KeyValue", DbType.String, WF_ToDoMessageRow.KeyValue.Value)
            If WF_ToDoMessageRow.Status.Updated Then db.AddInParameter(dbcmd, "@Status", DbType.String, WF_ToDoMessageRow.Status.Value)
            If WF_ToDoMessageRow.CrDate.Updated Then db.AddInParameter(dbcmd, "@CrDate", DbType.Date, IIf(IsDateTimeNull(WF_ToDoMessageRow.CrDate.Value), DBNull.Value, WF_ToDoMessageRow.CrDate.Value))
            If WF_ToDoMessageRow.CrUser.Updated Then db.AddInParameter(dbcmd, "@CrUser", DbType.String, WF_ToDoMessageRow.CrUser.Value)
            If WF_ToDoMessageRow.CrBr.Updated Then db.AddInParameter(dbcmd, "@CrBr", DbType.String, WF_ToDoMessageRow.CrBr.Value)
            If WF_ToDoMessageRow.CrBrName.Updated Then db.AddInParameter(dbcmd, "@CrBrName", DbType.String, WF_ToDoMessageRow.CrBrName.Value)
            If WF_ToDoMessageRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WF_ToDoMessageRow.LastChgDate.Value), DBNull.Value, WF_ToDoMessageRow.LastChgDate.Value))
            If WF_ToDoMessageRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WF_ToDoMessageRow.LastChgID.Value)
            If WF_ToDoMessageRow.LastChgDept.Updated Then db.AddInParameter(dbcmd, "@LastChgDept", DbType.String, WF_ToDoMessageRow.LastChgDept.Value)
            If WF_ToDoMessageRow.LastChgDeptName.Updated Then db.AddInParameter(dbcmd, "@LastChgDeptName", DbType.String, WF_ToDoMessageRow.LastChgDeptName.Value)
            db.AddInParameter(dbcmd, "@PKMsgCaseID", DbType.String, IIf(WF_ToDoMessageRow.LoadFromDataRow, WF_ToDoMessageRow.MsgCaseID.OldValue, WF_ToDoMessageRow.MsgCaseID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal WF_ToDoMessageRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update WF_ToDoMessage Set")
            For i As Integer = 0 To WF_ToDoMessageRow.FieldNames.Length - 1
                If Not WF_ToDoMessageRow.IsIdentityField(WF_ToDoMessageRow.FieldNames(i)) AndAlso WF_ToDoMessageRow.IsUpdated(WF_ToDoMessageRow.FieldNames(i)) AndAlso WF_ToDoMessageRow.CreateUpdateSQL(WF_ToDoMessageRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, WF_ToDoMessageRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where MsgCaseID = @PKMsgCaseID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If WF_ToDoMessageRow.MsgCaseID.Updated Then db.AddInParameter(dbcmd, "@MsgCaseID", DbType.String, WF_ToDoMessageRow.MsgCaseID.Value)
            If WF_ToDoMessageRow.AssignTo.Updated Then db.AddInParameter(dbcmd, "@AssignTo", DbType.String, WF_ToDoMessageRow.AssignTo.Value)
            If WF_ToDoMessageRow.MsgCode.Updated Then db.AddInParameter(dbcmd, "@MsgCode", DbType.String, WF_ToDoMessageRow.MsgCode.Value)
            If WF_ToDoMessageRow.MsgReason.Updated Then db.AddInParameter(dbcmd, "@MsgReason", DbType.String, WF_ToDoMessageRow.MsgReason.Value)
            If WF_ToDoMessageRow.MsgNote.Updated Then db.AddInParameter(dbcmd, "@MsgNote", DbType.String, WF_ToDoMessageRow.MsgNote.Value)
            If WF_ToDoMessageRow.PaperID.Updated Then db.AddInParameter(dbcmd, "@PaperID", DbType.String, WF_ToDoMessageRow.PaperID.Value)
            If WF_ToDoMessageRow.KeyValue.Updated Then db.AddInParameter(dbcmd, "@KeyValue", DbType.String, WF_ToDoMessageRow.KeyValue.Value)
            If WF_ToDoMessageRow.Status.Updated Then db.AddInParameter(dbcmd, "@Status", DbType.String, WF_ToDoMessageRow.Status.Value)
            If WF_ToDoMessageRow.CrDate.Updated Then db.AddInParameter(dbcmd, "@CrDate", DbType.Date, IIf(IsDateTimeNull(WF_ToDoMessageRow.CrDate.Value), DBNull.Value, WF_ToDoMessageRow.CrDate.Value))
            If WF_ToDoMessageRow.CrUser.Updated Then db.AddInParameter(dbcmd, "@CrUser", DbType.String, WF_ToDoMessageRow.CrUser.Value)
            If WF_ToDoMessageRow.CrBr.Updated Then db.AddInParameter(dbcmd, "@CrBr", DbType.String, WF_ToDoMessageRow.CrBr.Value)
            If WF_ToDoMessageRow.CrBrName.Updated Then db.AddInParameter(dbcmd, "@CrBrName", DbType.String, WF_ToDoMessageRow.CrBrName.Value)
            If WF_ToDoMessageRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WF_ToDoMessageRow.LastChgDate.Value), DBNull.Value, WF_ToDoMessageRow.LastChgDate.Value))
            If WF_ToDoMessageRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WF_ToDoMessageRow.LastChgID.Value)
            If WF_ToDoMessageRow.LastChgDept.Updated Then db.AddInParameter(dbcmd, "@LastChgDept", DbType.String, WF_ToDoMessageRow.LastChgDept.Value)
            If WF_ToDoMessageRow.LastChgDeptName.Updated Then db.AddInParameter(dbcmd, "@LastChgDeptName", DbType.String, WF_ToDoMessageRow.LastChgDeptName.Value)
            db.AddInParameter(dbcmd, "@PKMsgCaseID", DbType.String, IIf(WF_ToDoMessageRow.LoadFromDataRow, WF_ToDoMessageRow.MsgCaseID.OldValue, WF_ToDoMessageRow.MsgCaseID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal WF_ToDoMessageRow As Row()) As Integer
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
                    For Each r As Row In WF_ToDoMessageRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update WF_ToDoMessage Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where MsgCaseID = @PKMsgCaseID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.MsgCaseID.Updated Then db.AddInParameter(dbcmd, "@MsgCaseID", DbType.String, r.MsgCaseID.Value)
                        If r.AssignTo.Updated Then db.AddInParameter(dbcmd, "@AssignTo", DbType.String, r.AssignTo.Value)
                        If r.MsgCode.Updated Then db.AddInParameter(dbcmd, "@MsgCode", DbType.String, r.MsgCode.Value)
                        If r.MsgReason.Updated Then db.AddInParameter(dbcmd, "@MsgReason", DbType.String, r.MsgReason.Value)
                        If r.MsgNote.Updated Then db.AddInParameter(dbcmd, "@MsgNote", DbType.String, r.MsgNote.Value)
                        If r.PaperID.Updated Then db.AddInParameter(dbcmd, "@PaperID", DbType.String, r.PaperID.Value)
                        If r.KeyValue.Updated Then db.AddInParameter(dbcmd, "@KeyValue", DbType.String, r.KeyValue.Value)
                        If r.Status.Updated Then db.AddInParameter(dbcmd, "@Status", DbType.String, r.Status.Value)
                        If r.CrDate.Updated Then db.AddInParameter(dbcmd, "@CrDate", DbType.Date, IIf(IsDateTimeNull(r.CrDate.Value), DBNull.Value, r.CrDate.Value))
                        If r.CrUser.Updated Then db.AddInParameter(dbcmd, "@CrUser", DbType.String, r.CrUser.Value)
                        If r.CrBr.Updated Then db.AddInParameter(dbcmd, "@CrBr", DbType.String, r.CrBr.Value)
                        If r.CrBrName.Updated Then db.AddInParameter(dbcmd, "@CrBrName", DbType.String, r.CrBrName.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), DBNull.Value, r.LastChgDate.Value))
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDept.Updated Then db.AddInParameter(dbcmd, "@LastChgDept", DbType.String, r.LastChgDept.Value)
                        If r.LastChgDeptName.Updated Then db.AddInParameter(dbcmd, "@LastChgDeptName", DbType.String, r.LastChgDeptName.Value)
                        db.AddInParameter(dbcmd, "@PKMsgCaseID", DbType.String, IIf(r.LoadFromDataRow, r.MsgCaseID.OldValue, r.MsgCaseID.Value))

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

        Public Function Update(ByVal WF_ToDoMessageRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In WF_ToDoMessageRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update WF_ToDoMessage Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where MsgCaseID = @PKMsgCaseID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.MsgCaseID.Updated Then db.AddInParameter(dbcmd, "@MsgCaseID", DbType.String, r.MsgCaseID.Value)
                If r.AssignTo.Updated Then db.AddInParameter(dbcmd, "@AssignTo", DbType.String, r.AssignTo.Value)
                If r.MsgCode.Updated Then db.AddInParameter(dbcmd, "@MsgCode", DbType.String, r.MsgCode.Value)
                If r.MsgReason.Updated Then db.AddInParameter(dbcmd, "@MsgReason", DbType.String, r.MsgReason.Value)
                If r.MsgNote.Updated Then db.AddInParameter(dbcmd, "@MsgNote", DbType.String, r.MsgNote.Value)
                If r.PaperID.Updated Then db.AddInParameter(dbcmd, "@PaperID", DbType.String, r.PaperID.Value)
                If r.KeyValue.Updated Then db.AddInParameter(dbcmd, "@KeyValue", DbType.String, r.KeyValue.Value)
                If r.Status.Updated Then db.AddInParameter(dbcmd, "@Status", DbType.String, r.Status.Value)
                If r.CrDate.Updated Then db.AddInParameter(dbcmd, "@CrDate", DbType.Date, IIf(IsDateTimeNull(r.CrDate.Value), DBNull.Value, r.CrDate.Value))
                If r.CrUser.Updated Then db.AddInParameter(dbcmd, "@CrUser", DbType.String, r.CrUser.Value)
                If r.CrBr.Updated Then db.AddInParameter(dbcmd, "@CrBr", DbType.String, r.CrBr.Value)
                If r.CrBrName.Updated Then db.AddInParameter(dbcmd, "@CrBrName", DbType.String, r.CrBrName.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), DBNull.Value, r.LastChgDate.Value))
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDept.Updated Then db.AddInParameter(dbcmd, "@LastChgDept", DbType.String, r.LastChgDept.Value)
                If r.LastChgDeptName.Updated Then db.AddInParameter(dbcmd, "@LastChgDeptName", DbType.String, r.LastChgDeptName.Value)
                db.AddInParameter(dbcmd, "@PKMsgCaseID", DbType.String, IIf(r.LoadFromDataRow, r.MsgCaseID.OldValue, r.MsgCaseID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal WF_ToDoMessageRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From WF_ToDoMessage")
            strSQL.AppendLine("Where MsgCaseID = @MsgCaseID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@MsgCaseID", DbType.String, WF_ToDoMessageRow.MsgCaseID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal WF_ToDoMessageRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From WF_ToDoMessage")
            strSQL.AppendLine("Where MsgCaseID = @MsgCaseID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@MsgCaseID", DbType.String, WF_ToDoMessageRow.MsgCaseID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WF_ToDoMessage")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal WF_ToDoMessageRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into WF_ToDoMessage")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    MsgCaseID, AssignTo, MsgCode, MsgReason, MsgNote, PaperID, KeyValue, Status, CrDate,")
            strSQL.AppendLine("    CrUser, CrBr, CrBrName, LastChgDate, LastChgID, LastChgDept, LastChgDeptName")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @MsgCaseID, @AssignTo, @MsgCode, @MsgReason, @MsgNote, @PaperID, @KeyValue, @Status, @CrDate,")
            strSQL.AppendLine("    @CrUser, @CrBr, @CrBrName, @LastChgDate, @LastChgID, @LastChgDept, @LastChgDeptName")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@MsgCaseID", DbType.String, WF_ToDoMessageRow.MsgCaseID.Value)
            db.AddInParameter(dbcmd, "@AssignTo", DbType.String, WF_ToDoMessageRow.AssignTo.Value)
            db.AddInParameter(dbcmd, "@MsgCode", DbType.String, WF_ToDoMessageRow.MsgCode.Value)
            db.AddInParameter(dbcmd, "@MsgReason", DbType.String, WF_ToDoMessageRow.MsgReason.Value)
            db.AddInParameter(dbcmd, "@MsgNote", DbType.String, WF_ToDoMessageRow.MsgNote.Value)
            db.AddInParameter(dbcmd, "@PaperID", DbType.String, WF_ToDoMessageRow.PaperID.Value)
            db.AddInParameter(dbcmd, "@KeyValue", DbType.String, WF_ToDoMessageRow.KeyValue.Value)
            db.AddInParameter(dbcmd, "@Status", DbType.String, WF_ToDoMessageRow.Status.Value)
            db.AddInParameter(dbcmd, "@CrDate", DbType.Date, IIf(IsDateTimeNull(WF_ToDoMessageRow.CrDate.Value), DBNull.Value, WF_ToDoMessageRow.CrDate.Value))
            db.AddInParameter(dbcmd, "@CrUser", DbType.String, WF_ToDoMessageRow.CrUser.Value)
            db.AddInParameter(dbcmd, "@CrBr", DbType.String, WF_ToDoMessageRow.CrBr.Value)
            db.AddInParameter(dbcmd, "@CrBrName", DbType.String, WF_ToDoMessageRow.CrBrName.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WF_ToDoMessageRow.LastChgDate.Value), DBNull.Value, WF_ToDoMessageRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WF_ToDoMessageRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDept", DbType.String, WF_ToDoMessageRow.LastChgDept.Value)
            db.AddInParameter(dbcmd, "@LastChgDeptName", DbType.String, WF_ToDoMessageRow.LastChgDeptName.Value)

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal WF_ToDoMessageRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into WF_ToDoMessage")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    MsgCaseID, AssignTo, MsgCode, MsgReason, MsgNote, PaperID, KeyValue, Status, CrDate,")
            strSQL.AppendLine("    CrUser, CrBr, CrBrName, LastChgDate, LastChgID, LastChgDept, LastChgDeptName")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @MsgCaseID, @AssignTo, @MsgCode, @MsgReason, @MsgNote, @PaperID, @KeyValue, @Status, @CrDate,")
            strSQL.AppendLine("    @CrUser, @CrBr, @CrBrName, @LastChgDate, @LastChgID, @LastChgDept, @LastChgDeptName")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@MsgCaseID", DbType.String, WF_ToDoMessageRow.MsgCaseID.Value)
            db.AddInParameter(dbcmd, "@AssignTo", DbType.String, WF_ToDoMessageRow.AssignTo.Value)
            db.AddInParameter(dbcmd, "@MsgCode", DbType.String, WF_ToDoMessageRow.MsgCode.Value)
            db.AddInParameter(dbcmd, "@MsgReason", DbType.String, WF_ToDoMessageRow.MsgReason.Value)
            db.AddInParameter(dbcmd, "@MsgNote", DbType.String, WF_ToDoMessageRow.MsgNote.Value)
            db.AddInParameter(dbcmd, "@PaperID", DbType.String, WF_ToDoMessageRow.PaperID.Value)
            db.AddInParameter(dbcmd, "@KeyValue", DbType.String, WF_ToDoMessageRow.KeyValue.Value)
            db.AddInParameter(dbcmd, "@Status", DbType.String, WF_ToDoMessageRow.Status.Value)
            db.AddInParameter(dbcmd, "@CrDate", DbType.Date, IIf(IsDateTimeNull(WF_ToDoMessageRow.CrDate.Value), DBNull.Value, WF_ToDoMessageRow.CrDate.Value))
            db.AddInParameter(dbcmd, "@CrUser", DbType.String, WF_ToDoMessageRow.CrUser.Value)
            db.AddInParameter(dbcmd, "@CrBr", DbType.String, WF_ToDoMessageRow.CrBr.Value)
            db.AddInParameter(dbcmd, "@CrBrName", DbType.String, WF_ToDoMessageRow.CrBrName.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WF_ToDoMessageRow.LastChgDate.Value), DBNull.Value, WF_ToDoMessageRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WF_ToDoMessageRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDept", DbType.String, WF_ToDoMessageRow.LastChgDept.Value)
            db.AddInParameter(dbcmd, "@LastChgDeptName", DbType.String, WF_ToDoMessageRow.LastChgDeptName.Value)

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal WF_ToDoMessageRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into WF_ToDoMessage")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    MsgCaseID, AssignTo, MsgCode, MsgReason, MsgNote, PaperID, KeyValue, Status, CrDate,")
            strSQL.AppendLine("    CrUser, CrBr, CrBrName, LastChgDate, LastChgID, LastChgDept, LastChgDeptName")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @MsgCaseID, @AssignTo, @MsgCode, @MsgReason, @MsgNote, @PaperID, @KeyValue, @Status, @CrDate,")
            strSQL.AppendLine("    @CrUser, @CrBr, @CrBrName, @LastChgDate, @LastChgID, @LastChgDept, @LastChgDeptName")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In WF_ToDoMessageRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@MsgCaseID", DbType.String, r.MsgCaseID.Value)
                        db.AddInParameter(dbcmd, "@AssignTo", DbType.String, r.AssignTo.Value)
                        db.AddInParameter(dbcmd, "@MsgCode", DbType.String, r.MsgCode.Value)
                        db.AddInParameter(dbcmd, "@MsgReason", DbType.String, r.MsgReason.Value)
                        db.AddInParameter(dbcmd, "@MsgNote", DbType.String, r.MsgNote.Value)
                        db.AddInParameter(dbcmd, "@PaperID", DbType.String, r.PaperID.Value)
                        db.AddInParameter(dbcmd, "@KeyValue", DbType.String, r.KeyValue.Value)
                        db.AddInParameter(dbcmd, "@Status", DbType.String, r.Status.Value)
                        db.AddInParameter(dbcmd, "@CrDate", DbType.Date, IIf(IsDateTimeNull(r.CrDate.Value), DBNull.Value, r.CrDate.Value))
                        db.AddInParameter(dbcmd, "@CrUser", DbType.String, r.CrUser.Value)
                        db.AddInParameter(dbcmd, "@CrBr", DbType.String, r.CrBr.Value)
                        db.AddInParameter(dbcmd, "@CrBrName", DbType.String, r.CrBrName.Value)
                        db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), DBNull.Value, r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        db.AddInParameter(dbcmd, "@LastChgDept", DbType.String, r.LastChgDept.Value)
                        db.AddInParameter(dbcmd, "@LastChgDeptName", DbType.String, r.LastChgDeptName.Value)

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

        Public Function Insert(ByVal WF_ToDoMessageRow As Row(), ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into WF_ToDoMessage")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    MsgCaseID, AssignTo, MsgCode, MsgReason, MsgNote, PaperID, KeyValue, Status, CrDate,")
            strSQL.AppendLine("    CrUser, CrBr, CrBrName, LastChgDate, LastChgID, LastChgDept, LastChgDeptName")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @MsgCaseID, @AssignTo, @MsgCode, @MsgReason, @MsgNote, @PaperID, @KeyValue, @Status, @CrDate,")
            strSQL.AppendLine("    @CrUser, @CrBr, @CrBrName, @LastChgDate, @LastChgID, @LastChgDept, @LastChgDeptName")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In WF_ToDoMessageRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@MsgCaseID", DbType.String, r.MsgCaseID.Value)
                db.AddInParameter(dbcmd, "@AssignTo", DbType.String, r.AssignTo.Value)
                db.AddInParameter(dbcmd, "@MsgCode", DbType.String, r.MsgCode.Value)
                db.AddInParameter(dbcmd, "@MsgReason", DbType.String, r.MsgReason.Value)
                db.AddInParameter(dbcmd, "@MsgNote", DbType.String, r.MsgNote.Value)
                db.AddInParameter(dbcmd, "@PaperID", DbType.String, r.PaperID.Value)
                db.AddInParameter(dbcmd, "@KeyValue", DbType.String, r.KeyValue.Value)
                db.AddInParameter(dbcmd, "@Status", DbType.String, r.Status.Value)
                db.AddInParameter(dbcmd, "@CrDate", DbType.Date, IIf(IsDateTimeNull(r.CrDate.Value), DBNull.Value, r.CrDate.Value))
                db.AddInParameter(dbcmd, "@CrUser", DbType.String, r.CrUser.Value)
                db.AddInParameter(dbcmd, "@CrBr", DbType.String, r.CrBr.Value)
                db.AddInParameter(dbcmd, "@CrBrName", DbType.String, r.CrBrName.Value)
                db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), DBNull.Value, r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                db.AddInParameter(dbcmd, "@LastChgDept", DbType.String, r.LastChgDept.Value)
                db.AddInParameter(dbcmd, "@LastChgDeptName", DbType.String, r.LastChgDeptName.Value)

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

