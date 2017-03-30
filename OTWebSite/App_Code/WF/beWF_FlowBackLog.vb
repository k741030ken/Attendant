'****************************************************************
' Table:WF_FlowBackLog
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

Namespace beWF_FlowBackLog
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "FlowStepID", "FlowID", "FlowCaseID", "AppID", "UserID", "LogDate", "Remark" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(String) }
        Private m_PrimaryFields As String() = {  }

        Public ReadOnly Property Rows() As beWF_FlowBackLog.Rows 
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
        Public Sub Transfer2Row(WF_FlowBackLogTable As DataTable)
            For Each dr As DataRow In WF_FlowBackLogTable.Rows
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

                dr(m_Rows(i).FlowStepID.FieldName) = m_Rows(i).FlowStepID.Value
                dr(m_Rows(i).FlowID.FieldName) = m_Rows(i).FlowID.Value
                dr(m_Rows(i).FlowCaseID.FieldName) = m_Rows(i).FlowCaseID.Value
                dr(m_Rows(i).AppID.FieldName) = m_Rows(i).AppID.Value
                dr(m_Rows(i).UserID.FieldName) = m_Rows(i).UserID.Value
                dr(m_Rows(i).LogDate.FieldName) = m_Rows(i).LogDate.Value
                dr(m_Rows(i).Remark.FieldName) = m_Rows(i).Remark.Value

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

        Public Sub Add(WF_FlowBackLogRow As Row)
            m_Rows.Add(WF_FlowBackLogRow)
        End Sub

        Public Sub Remove(WF_FlowBackLogRow As Row)
            If m_Rows.IndexOf(WF_FlowBackLogRow) >= 0 Then
                m_Rows.Remove(WF_FlowBackLogRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_FlowStepID As Field(Of String) = new Field(Of String)("FlowStepID", true)
        Private FI_FlowID As Field(Of String) = new Field(Of String)("FlowID", true)
        Private FI_FlowCaseID As Field(Of String) = new Field(Of String)("FlowCaseID", true)
        Private FI_AppID As Field(Of String) = new Field(Of String)("AppID", true)
        Private FI_UserID As Field(Of String) = new Field(Of String)("UserID", true)
        Private FI_LogDate As Field(Of Date) = new Field(Of Date)("LogDate", true)
        Private FI_Remark As Field(Of String) = new Field(Of String)("Remark", true)
        Private m_FieldNames As String() = { "FlowStepID", "FlowID", "FlowCaseID", "AppID", "UserID", "LogDate", "Remark" }
        Private m_PrimaryFields As String() = {  }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "FlowStepID"
                    Return FI_FlowStepID.Value
                Case "FlowID"
                    Return FI_FlowID.Value
                Case "FlowCaseID"
                    Return FI_FlowCaseID.Value
                Case "AppID"
                    Return FI_AppID.Value
                Case "UserID"
                    Return FI_UserID.Value
                Case "LogDate"
                    Return FI_LogDate.Value
                Case "Remark"
                    Return FI_Remark.Value
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Sub SetFieldValue(ByVal fieldName As String, ByVal value As Object)
            Select Case fieldName
                Case "FlowStepID"
                    FI_FlowStepID.SetValue(value)
                Case "FlowID"
                    FI_FlowID.SetValue(value)
                Case "FlowCaseID"
                    FI_FlowCaseID.SetValue(value)
                Case "AppID"
                    FI_AppID.SetValue(value)
                Case "UserID"
                    FI_UserID.SetValue(value)
                Case "LogDate"
                    FI_LogDate.SetValue(value)
                Case "Remark"
                    FI_Remark.SetValue(value)
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
                Case "FlowStepID"
                    return FI_FlowStepID.Updated
                Case "FlowID"
                    return FI_FlowID.Updated
                Case "FlowCaseID"
                    return FI_FlowCaseID.Updated
                Case "AppID"
                    return FI_AppID.Updated
                Case "UserID"
                    return FI_UserID.Updated
                Case "LogDate"
                    return FI_LogDate.Updated
                Case "Remark"
                    return FI_Remark.Updated
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public Function CreateUpdateSQL(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "FlowStepID"
                    return FI_FlowStepID.CreateUpdateSQL
                Case "FlowID"
                    return FI_FlowID.CreateUpdateSQL
                Case "FlowCaseID"
                    return FI_FlowCaseID.CreateUpdateSQL
                Case "AppID"
                    return FI_AppID.CreateUpdateSQL
                Case "UserID"
                    return FI_UserID.CreateUpdateSQL
                Case "LogDate"
                    return FI_LogDate.CreateUpdateSQL
                Case "Remark"
                    return FI_Remark.CreateUpdateSQL
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
            FI_FlowStepID.SetInitValue("")
            FI_FlowID.SetInitValue("")
            FI_FlowCaseID.SetInitValue("")
            FI_AppID.SetInitValue("")
            FI_UserID.SetInitValue("")
            FI_LogDate.SetInitValue(DateTime.Now)
            FI_Remark.SetInitValue("")
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_FlowStepID.SetInitValue(dr("FlowStepID"))
            FI_FlowID.SetInitValue(dr("FlowID"))
            FI_FlowCaseID.SetInitValue(dr("FlowCaseID"))
            FI_AppID.SetInitValue(dr("AppID"))
            FI_UserID.SetInitValue(dr("UserID"))
            FI_LogDate.SetInitValue(dr("LogDate"))
            FI_Remark.SetInitValue(dr("Remark"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_FlowStepID.Updated = False
            FI_FlowID.Updated = False
            FI_FlowCaseID.Updated = False
            FI_AppID.Updated = False
            FI_UserID.Updated = False
            FI_LogDate.Updated = False
            FI_Remark.Updated = False
        End Sub

        Public ReadOnly Property FlowStepID As Field(Of String) 
            Get
                Return FI_FlowStepID
            End Get
        End Property

        Public ReadOnly Property FlowID As Field(Of String) 
            Get
                Return FI_FlowID
            End Get
        End Property

        Public ReadOnly Property FlowCaseID As Field(Of String) 
            Get
                Return FI_FlowCaseID
            End Get
        End Property

        Public ReadOnly Property AppID As Field(Of String) 
            Get
                Return FI_AppID
            End Get
        End Property

        Public ReadOnly Property UserID As Field(Of String) 
            Get
                Return FI_UserID
            End Get
        End Property

        Public ReadOnly Property LogDate As Field(Of Date) 
            Get
                Return FI_LogDate
            End Get
        End Property

        Public ReadOnly Property Remark As Field(Of String) 
            Get
                Return FI_Remark
            End Get
        End Property

    End Class

    Public Class Service
        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WF_FlowBackLog")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal WF_FlowBackLogRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into WF_FlowBackLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowStepID, FlowID, FlowCaseID, AppID, UserID, LogDate, Remark")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowStepID, @FlowID, @FlowCaseID, @AppID, @UserID, @LogDate, @Remark")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, WF_FlowBackLogRow.FlowStepID.Value)
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowBackLogRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowBackLogRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@AppID", DbType.String, WF_FlowBackLogRow.AppID.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, WF_FlowBackLogRow.UserID.Value)
            db.AddInParameter(dbcmd, "@LogDate", DbType.Date, IIf(IsDateTimeNull(WF_FlowBackLogRow.LogDate.Value), Convert.ToDateTime("1900/1/1"), WF_FlowBackLogRow.LogDate.Value))
            db.AddInParameter(dbcmd, "@Remark", DbType.String, WF_FlowBackLogRow.Remark.Value)

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal WF_FlowBackLogRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into WF_FlowBackLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowStepID, FlowID, FlowCaseID, AppID, UserID, LogDate, Remark")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowStepID, @FlowID, @FlowCaseID, @AppID, @UserID, @LogDate, @Remark")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, WF_FlowBackLogRow.FlowStepID.Value)
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowBackLogRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowBackLogRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@AppID", DbType.String, WF_FlowBackLogRow.AppID.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, WF_FlowBackLogRow.UserID.Value)
            db.AddInParameter(dbcmd, "@LogDate", DbType.Date, IIf(IsDateTimeNull(WF_FlowBackLogRow.LogDate.Value), Convert.ToDateTime("1900/1/1"), WF_FlowBackLogRow.LogDate.Value))
            db.AddInParameter(dbcmd, "@Remark", DbType.String, WF_FlowBackLogRow.Remark.Value)

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal WF_FlowBackLogRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into WF_FlowBackLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowStepID, FlowID, FlowCaseID, AppID, UserID, LogDate, Remark")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowStepID, @FlowID, @FlowCaseID, @AppID, @UserID, @LogDate, @Remark")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In WF_FlowBackLogRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, r.FlowStepID.Value)
                        db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                        db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, r.FlowCaseID.Value)
                        db.AddInParameter(dbcmd, "@AppID", DbType.String, r.AppID.Value)
                        db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                        db.AddInParameter(dbcmd, "@LogDate", DbType.Date, IIf(IsDateTimeNull(r.LogDate.Value), Convert.ToDateTime("1900/1/1"), r.LogDate.Value))
                        db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)

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

        Public Function Insert(ByVal WF_FlowBackLogRow As Row(), ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into WF_FlowBackLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowStepID, FlowID, FlowCaseID, AppID, UserID, LogDate, Remark")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowStepID, @FlowID, @FlowCaseID, @AppID, @UserID, @LogDate, @Remark")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In WF_FlowBackLogRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, r.FlowStepID.Value)
                db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, r.FlowCaseID.Value)
                db.AddInParameter(dbcmd, "@AppID", DbType.String, r.AppID.Value)
                db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                db.AddInParameter(dbcmd, "@LogDate", DbType.Date, IIf(IsDateTimeNull(r.LogDate.Value), Convert.ToDateTime("1900/1/1"), r.LogDate.Value))
                db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)

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

