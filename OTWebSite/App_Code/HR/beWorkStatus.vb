'****************************************************************
' Table:WorkStatus
' Created Date: 2015.04.29
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beWorkStatus
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "WorkCode", "Remark", "RemarkCN", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "WorkCode" }

        Public ReadOnly Property Rows() As beWorkStatus.Rows 
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
        Public Sub Transfer2Row(WorkStatusTable As DataTable)
            For Each dr As DataRow In WorkStatusTable.Rows
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

                dr(m_Rows(i).WorkCode.FieldName) = m_Rows(i).WorkCode.Value
                dr(m_Rows(i).Remark.FieldName) = m_Rows(i).Remark.Value
                dr(m_Rows(i).RemarkCN.FieldName) = m_Rows(i).RemarkCN.Value
                dr(m_Rows(i).LastChgComp.FieldName) = m_Rows(i).LastChgComp.Value
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

        Public Sub Add(WorkStatusRow As Row)
            m_Rows.Add(WorkStatusRow)
        End Sub

        Public Sub Remove(WorkStatusRow As Row)
            If m_Rows.IndexOf(WorkStatusRow) >= 0 Then
                m_Rows.Remove(WorkStatusRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_WorkCode As Field(Of String) = new Field(Of String)("WorkCode", true)
        Private FI_Remark As Field(Of String) = new Field(Of String)("Remark", true)
        Private FI_RemarkCN As Field(Of String) = new Field(Of String)("RemarkCN", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "WorkCode", "Remark", "RemarkCN", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "WorkCode" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "WorkCode"
                    Return FI_WorkCode.Value
                Case "Remark"
                    Return FI_Remark.Value
                Case "RemarkCN"
                    Return FI_RemarkCN.Value
                Case "LastChgComp"
                    Return FI_LastChgComp.Value
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
                Case "WorkCode"
                    FI_WorkCode.SetValue(value)
                Case "Remark"
                    FI_Remark.SetValue(value)
                Case "RemarkCN"
                    FI_RemarkCN.SetValue(value)
                Case "LastChgComp"
                    FI_LastChgComp.SetValue(value)
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
                Case "WorkCode"
                    return FI_WorkCode.Updated
                Case "Remark"
                    return FI_Remark.Updated
                Case "RemarkCN"
                    return FI_RemarkCN.Updated
                Case "LastChgComp"
                    return FI_LastChgComp.Updated
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
                Case "WorkCode"
                    return FI_WorkCode.CreateUpdateSQL
                Case "Remark"
                    return FI_Remark.CreateUpdateSQL
                Case "RemarkCN"
                    return FI_RemarkCN.CreateUpdateSQL
                Case "LastChgComp"
                    return FI_LastChgComp.CreateUpdateSQL
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
            FI_WorkCode.SetInitValue("")
            FI_Remark.SetInitValue("")
            FI_RemarkCN.SetInitValue("")
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_WorkCode.SetInitValue(dr("WorkCode"))
            FI_Remark.SetInitValue(dr("Remark"))
            FI_RemarkCN.SetInitValue(dr("RemarkCN"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_WorkCode.Updated = False
            FI_Remark.Updated = False
            FI_RemarkCN.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
        End Sub

        Public ReadOnly Property WorkCode As Field(Of String) 
            Get
                Return FI_WorkCode
            End Get
        End Property

        Public ReadOnly Property Remark As Field(Of String) 
            Get
                Return FI_Remark
            End Get
        End Property

        Public ReadOnly Property RemarkCN As Field(Of String) 
            Get
                Return FI_RemarkCN
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

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal WorkStatusRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From WorkStatus")
            strSQL.AppendLine("Where WorkCode = @WorkCode")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@WorkCode", DbType.String, WorkStatusRow.WorkCode.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal WorkStatusRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From WorkStatus")
            strSQL.AppendLine("Where WorkCode = @WorkCode")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@WorkCode", DbType.String, WorkStatusRow.WorkCode.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal WorkStatusRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From WorkStatus")
            strSQL.AppendLine("Where WorkCode = @WorkCode")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In WorkStatusRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@WorkCode", DbType.String, r.WorkCode.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal WorkStatusRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From WorkStatus")
            strSQL.AppendLine("Where WorkCode = @WorkCode")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In WorkStatusRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@WorkCode", DbType.String, r.WorkCode.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal WorkStatusRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WorkStatus")
            strSQL.AppendLine("Where WorkCode = @WorkCode")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@WorkCode", DbType.String, WorkStatusRow.WorkCode.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(WorkStatusRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WorkStatus")
            strSQL.AppendLine("Where WorkCode = @WorkCode")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@WorkCode", DbType.String, WorkStatusRow.WorkCode.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal WorkStatusRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update WorkStatus Set")
            For i As Integer = 0 To WorkStatusRow.FieldNames.Length - 1
                If Not WorkStatusRow.IsIdentityField(WorkStatusRow.FieldNames(i)) AndAlso WorkStatusRow.IsUpdated(WorkStatusRow.FieldNames(i)) AndAlso WorkStatusRow.CreateUpdateSQL(WorkStatusRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, WorkStatusRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where WorkCode = @PKWorkCode")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If WorkStatusRow.WorkCode.Updated Then db.AddInParameter(dbcmd, "@WorkCode", DbType.String, WorkStatusRow.WorkCode.Value)
            If WorkStatusRow.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, WorkStatusRow.Remark.Value)
            If WorkStatusRow.RemarkCN.Updated Then db.AddInParameter(dbcmd, "@RemarkCN", DbType.String, WorkStatusRow.RemarkCN.Value)
            If WorkStatusRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, WorkStatusRow.LastChgComp.Value)
            If WorkStatusRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WorkStatusRow.LastChgID.Value)
            If WorkStatusRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WorkStatusRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), WorkStatusRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKWorkCode", DbType.String, IIf(WorkStatusRow.LoadFromDataRow, WorkStatusRow.WorkCode.OldValue, WorkStatusRow.WorkCode.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal WorkStatusRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update WorkStatus Set")
            For i As Integer = 0 To WorkStatusRow.FieldNames.Length - 1
                If Not WorkStatusRow.IsIdentityField(WorkStatusRow.FieldNames(i)) AndAlso WorkStatusRow.IsUpdated(WorkStatusRow.FieldNames(i)) AndAlso WorkStatusRow.CreateUpdateSQL(WorkStatusRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, WorkStatusRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where WorkCode = @PKWorkCode")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If WorkStatusRow.WorkCode.Updated Then db.AddInParameter(dbcmd, "@WorkCode", DbType.String, WorkStatusRow.WorkCode.Value)
            If WorkStatusRow.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, WorkStatusRow.Remark.Value)
            If WorkStatusRow.RemarkCN.Updated Then db.AddInParameter(dbcmd, "@RemarkCN", DbType.String, WorkStatusRow.RemarkCN.Value)
            If WorkStatusRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, WorkStatusRow.LastChgComp.Value)
            If WorkStatusRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WorkStatusRow.LastChgID.Value)
            If WorkStatusRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WorkStatusRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), WorkStatusRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKWorkCode", DbType.String, IIf(WorkStatusRow.LoadFromDataRow, WorkStatusRow.WorkCode.OldValue, WorkStatusRow.WorkCode.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal WorkStatusRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In WorkStatusRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update WorkStatus Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where WorkCode = @PKWorkCode")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.WorkCode.Updated Then db.AddInParameter(dbcmd, "@WorkCode", DbType.String, r.WorkCode.Value)
                        If r.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                        If r.RemarkCN.Updated Then db.AddInParameter(dbcmd, "@RemarkCN", DbType.String, r.RemarkCN.Value)
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKWorkCode", DbType.String, IIf(r.LoadFromDataRow, r.WorkCode.OldValue, r.WorkCode.Value))

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

        Public Function Update(ByVal WorkStatusRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In WorkStatusRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update WorkStatus Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where WorkCode = @PKWorkCode")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.WorkCode.Updated Then db.AddInParameter(dbcmd, "@WorkCode", DbType.String, r.WorkCode.Value)
                If r.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                If r.RemarkCN.Updated Then db.AddInParameter(dbcmd, "@RemarkCN", DbType.String, r.RemarkCN.Value)
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKWorkCode", DbType.String, IIf(r.LoadFromDataRow, r.WorkCode.OldValue, r.WorkCode.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal WorkStatusRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From WorkStatus")
            strSQL.AppendLine("Where WorkCode = @WorkCode")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@WorkCode", DbType.String, WorkStatusRow.WorkCode.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal WorkStatusRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From WorkStatus")
            strSQL.AppendLine("Where WorkCode = @WorkCode")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@WorkCode", DbType.String, WorkStatusRow.WorkCode.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WorkStatus")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal WorkStatusRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into WorkStatus")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    WorkCode, Remark, RemarkCN, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @WorkCode, @Remark, @RemarkCN, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@WorkCode", DbType.String, WorkStatusRow.WorkCode.Value)
            db.AddInParameter(dbcmd, "@Remark", DbType.String, WorkStatusRow.Remark.Value)
            db.AddInParameter(dbcmd, "@RemarkCN", DbType.String, WorkStatusRow.RemarkCN.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, WorkStatusRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WorkStatusRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WorkStatusRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), WorkStatusRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal WorkStatusRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into WorkStatus")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    WorkCode, Remark, RemarkCN, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @WorkCode, @Remark, @RemarkCN, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@WorkCode", DbType.String, WorkStatusRow.WorkCode.Value)
            db.AddInParameter(dbcmd, "@Remark", DbType.String, WorkStatusRow.Remark.Value)
            db.AddInParameter(dbcmd, "@RemarkCN", DbType.String, WorkStatusRow.RemarkCN.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, WorkStatusRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WorkStatusRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WorkStatusRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), WorkStatusRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal WorkStatusRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into WorkStatus")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    WorkCode, Remark, RemarkCN, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @WorkCode, @Remark, @RemarkCN, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In WorkStatusRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@WorkCode", DbType.String, r.WorkCode.Value)
                        db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                        db.AddInParameter(dbcmd, "@RemarkCN", DbType.String, r.RemarkCN.Value)
                        db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
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

        Public Function Insert(ByVal WorkStatusRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into WorkStatus")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    WorkCode, Remark, RemarkCN, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @WorkCode, @Remark, @RemarkCN, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In WorkStatusRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@WorkCode", DbType.String, r.WorkCode.Value)
                db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                db.AddInParameter(dbcmd, "@RemarkCN", DbType.String, r.RemarkCN.Value)
                db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
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

