'****************************************************************
' Table:EmployeeReason
' Created Date: 2014.09.16
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beEmployeeReason
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "Reason", "Remark", "WebApplyFlag", "EmployeeWaitFlag", "NotEmpLogFlag", "InValidFlag" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) }
        Private m_PrimaryFields As String() = { "Reason" }

        Public ReadOnly Property Rows() As beEmployeeReason.Rows 
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
        Public Sub Transfer2Row(EmployeeReasonTable As DataTable)
            For Each dr As DataRow In EmployeeReasonTable.Rows
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

                dr(m_Rows(i).Reason.FieldName) = m_Rows(i).Reason.Value
                dr(m_Rows(i).Remark.FieldName) = m_Rows(i).Remark.Value
                dr(m_Rows(i).WebApplyFlag.FieldName) = m_Rows(i).WebApplyFlag.Value
                dr(m_Rows(i).EmployeeWaitFlag.FieldName) = m_Rows(i).EmployeeWaitFlag.Value
                dr(m_Rows(i).NotEmpLogFlag.FieldName) = m_Rows(i).NotEmpLogFlag.Value
                dr(m_Rows(i).InValidFlag.FieldName) = m_Rows(i).InValidFlag.Value

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

        Public Sub Add(EmployeeReasonRow As Row)
            m_Rows.Add(EmployeeReasonRow)
        End Sub

        Public Sub Remove(EmployeeReasonRow As Row)
            If m_Rows.IndexOf(EmployeeReasonRow) >= 0 Then
                m_Rows.Remove(EmployeeReasonRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_Reason As Field(Of String) = new Field(Of String)("Reason", true)
        Private FI_Remark As Field(Of String) = new Field(Of String)("Remark", true)
        Private FI_WebApplyFlag As Field(Of String) = new Field(Of String)("WebApplyFlag", true)
        Private FI_EmployeeWaitFlag As Field(Of String) = new Field(Of String)("EmployeeWaitFlag", true)
        Private FI_NotEmpLogFlag As Field(Of String) = new Field(Of String)("NotEmpLogFlag", true)
        Private FI_InValidFlag As Field(Of String) = new Field(Of String)("InValidFlag", true)
        Private m_FieldNames As String() = { "Reason", "Remark", "WebApplyFlag", "EmployeeWaitFlag", "NotEmpLogFlag", "InValidFlag" }
        Private m_PrimaryFields As String() = { "Reason" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "Reason"
                    Return FI_Reason.Value
                Case "Remark"
                    Return FI_Remark.Value
                Case "WebApplyFlag"
                    Return FI_WebApplyFlag.Value
                Case "EmployeeWaitFlag"
                    Return FI_EmployeeWaitFlag.Value
                Case "NotEmpLogFlag"
                    Return FI_NotEmpLogFlag.Value
                Case "InValidFlag"
                    Return FI_InValidFlag.Value
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Sub SetFieldValue(ByVal fieldName As String, ByVal value As Object)
            Select Case fieldName
                Case "Reason"
                    FI_Reason.SetValue(value)
                Case "Remark"
                    FI_Remark.SetValue(value)
                Case "WebApplyFlag"
                    FI_WebApplyFlag.SetValue(value)
                Case "EmployeeWaitFlag"
                    FI_EmployeeWaitFlag.SetValue(value)
                Case "NotEmpLogFlag"
                    FI_NotEmpLogFlag.SetValue(value)
                Case "InValidFlag"
                    FI_InValidFlag.SetValue(value)
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
                Case "Reason"
                    return FI_Reason.Updated
                Case "Remark"
                    return FI_Remark.Updated
                Case "WebApplyFlag"
                    return FI_WebApplyFlag.Updated
                Case "EmployeeWaitFlag"
                    return FI_EmployeeWaitFlag.Updated
                Case "NotEmpLogFlag"
                    return FI_NotEmpLogFlag.Updated
                Case "InValidFlag"
                    return FI_InValidFlag.Updated
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public Function CreateUpdateSQL(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "Reason"
                    return FI_Reason.CreateUpdateSQL
                Case "Remark"
                    return FI_Remark.CreateUpdateSQL
                Case "WebApplyFlag"
                    return FI_WebApplyFlag.CreateUpdateSQL
                Case "EmployeeWaitFlag"
                    return FI_EmployeeWaitFlag.CreateUpdateSQL
                Case "NotEmpLogFlag"
                    return FI_NotEmpLogFlag.CreateUpdateSQL
                Case "InValidFlag"
                    return FI_InValidFlag.CreateUpdateSQL
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
            FI_Reason.SetInitValue("")
            FI_Remark.SetInitValue("")
            FI_WebApplyFlag.SetInitValue("")
            FI_EmployeeWaitFlag.SetInitValue("0")
            FI_NotEmpLogFlag.SetInitValue("0")
            FI_InValidFlag.SetInitValue("0")
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_Reason.SetInitValue(dr("Reason"))
            FI_Remark.SetInitValue(dr("Remark"))
            FI_WebApplyFlag.SetInitValue(dr("WebApplyFlag"))
            FI_EmployeeWaitFlag.SetInitValue(dr("EmployeeWaitFlag"))
            FI_NotEmpLogFlag.SetInitValue(dr("NotEmpLogFlag"))
            FI_InValidFlag.SetInitValue(dr("InValidFlag"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_Reason.Updated = False
            FI_Remark.Updated = False
            FI_WebApplyFlag.Updated = False
            FI_EmployeeWaitFlag.Updated = False
            FI_NotEmpLogFlag.Updated = False
            FI_InValidFlag.Updated = False
        End Sub

        Public ReadOnly Property Reason As Field(Of String) 
            Get
                Return FI_Reason
            End Get
        End Property

        Public ReadOnly Property Remark As Field(Of String) 
            Get
                Return FI_Remark
            End Get
        End Property

        Public ReadOnly Property WebApplyFlag As Field(Of String) 
            Get
                Return FI_WebApplyFlag
            End Get
        End Property

        Public ReadOnly Property EmployeeWaitFlag As Field(Of String) 
            Get
                Return FI_EmployeeWaitFlag
            End Get
        End Property

        Public ReadOnly Property NotEmpLogFlag As Field(Of String) 
            Get
                Return FI_NotEmpLogFlag
            End Get
        End Property

        Public ReadOnly Property InValidFlag As Field(Of String) 
            Get
                Return FI_InValidFlag
            End Get
        End Property

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal EmployeeReasonRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From EmployeeReason")
            strSQL.AppendLine("Where Reason = @Reason")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@Reason", DbType.String, EmployeeReasonRow.Reason.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal EmployeeReasonRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From EmployeeReason")
            strSQL.AppendLine("Where Reason = @Reason")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@Reason", DbType.String, EmployeeReasonRow.Reason.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal EmployeeReasonRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From EmployeeReason")
            strSQL.AppendLine("Where Reason = @Reason")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In EmployeeReasonRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal EmployeeReasonRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From EmployeeReason")
            strSQL.AppendLine("Where Reason = @Reason")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In EmployeeReasonRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal EmployeeReasonRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmployeeReason")
            strSQL.AppendLine("Where Reason = @Reason")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@Reason", DbType.String, EmployeeReasonRow.Reason.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(EmployeeReasonRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmployeeReason")
            strSQL.AppendLine("Where Reason = @Reason")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@Reason", DbType.String, EmployeeReasonRow.Reason.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal EmployeeReasonRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update EmployeeReason Set")
            For i As Integer = 0 To EmployeeReasonRow.FieldNames.Length - 1
                If Not EmployeeReasonRow.IsIdentityField(EmployeeReasonRow.FieldNames(i)) AndAlso EmployeeReasonRow.IsUpdated(EmployeeReasonRow.FieldNames(i)) AndAlso EmployeeReasonRow.CreateUpdateSQL(EmployeeReasonRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, EmployeeReasonRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where Reason = @PKReason")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If EmployeeReasonRow.Reason.Updated Then db.AddInParameter(dbcmd, "@Reason", DbType.String, EmployeeReasonRow.Reason.Value)
            If EmployeeReasonRow.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, EmployeeReasonRow.Remark.Value)
            If EmployeeReasonRow.WebApplyFlag.Updated Then db.AddInParameter(dbcmd, "@WebApplyFlag", DbType.String, EmployeeReasonRow.WebApplyFlag.Value)
            If EmployeeReasonRow.EmployeeWaitFlag.Updated Then db.AddInParameter(dbcmd, "@EmployeeWaitFlag", DbType.String, EmployeeReasonRow.EmployeeWaitFlag.Value)
            If EmployeeReasonRow.NotEmpLogFlag.Updated Then db.AddInParameter(dbcmd, "@NotEmpLogFlag", DbType.String, EmployeeReasonRow.NotEmpLogFlag.Value)
            If EmployeeReasonRow.InValidFlag.Updated Then db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, EmployeeReasonRow.InValidFlag.Value)
            db.AddInParameter(dbcmd, "@PKReason", DbType.String, IIf(EmployeeReasonRow.LoadFromDataRow, EmployeeReasonRow.Reason.OldValue, EmployeeReasonRow.Reason.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal EmployeeReasonRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update EmployeeReason Set")
            For i As Integer = 0 To EmployeeReasonRow.FieldNames.Length - 1
                If Not EmployeeReasonRow.IsIdentityField(EmployeeReasonRow.FieldNames(i)) AndAlso EmployeeReasonRow.IsUpdated(EmployeeReasonRow.FieldNames(i)) AndAlso EmployeeReasonRow.CreateUpdateSQL(EmployeeReasonRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, EmployeeReasonRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where Reason = @PKReason")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If EmployeeReasonRow.Reason.Updated Then db.AddInParameter(dbcmd, "@Reason", DbType.String, EmployeeReasonRow.Reason.Value)
            If EmployeeReasonRow.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, EmployeeReasonRow.Remark.Value)
            If EmployeeReasonRow.WebApplyFlag.Updated Then db.AddInParameter(dbcmd, "@WebApplyFlag", DbType.String, EmployeeReasonRow.WebApplyFlag.Value)
            If EmployeeReasonRow.EmployeeWaitFlag.Updated Then db.AddInParameter(dbcmd, "@EmployeeWaitFlag", DbType.String, EmployeeReasonRow.EmployeeWaitFlag.Value)
            If EmployeeReasonRow.NotEmpLogFlag.Updated Then db.AddInParameter(dbcmd, "@NotEmpLogFlag", DbType.String, EmployeeReasonRow.NotEmpLogFlag.Value)
            If EmployeeReasonRow.InValidFlag.Updated Then db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, EmployeeReasonRow.InValidFlag.Value)
            db.AddInParameter(dbcmd, "@PKReason", DbType.String, IIf(EmployeeReasonRow.LoadFromDataRow, EmployeeReasonRow.Reason.OldValue, EmployeeReasonRow.Reason.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal EmployeeReasonRow As Row()) As Integer
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
                    For Each r As Row In EmployeeReasonRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update EmployeeReason Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where Reason = @PKReason")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.Reason.Updated Then db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                        If r.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                        If r.WebApplyFlag.Updated Then db.AddInParameter(dbcmd, "@WebApplyFlag", DbType.String, r.WebApplyFlag.Value)
                        If r.EmployeeWaitFlag.Updated Then db.AddInParameter(dbcmd, "@EmployeeWaitFlag", DbType.String, r.EmployeeWaitFlag.Value)
                        If r.NotEmpLogFlag.Updated Then db.AddInParameter(dbcmd, "@NotEmpLogFlag", DbType.String, r.NotEmpLogFlag.Value)
                        If r.InValidFlag.Updated Then db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, r.InValidFlag.Value)
                        db.AddInParameter(dbcmd, "@PKReason", DbType.String, IIf(r.LoadFromDataRow, r.Reason.OldValue, r.Reason.Value))

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

        Public Function Update(ByVal EmployeeReasonRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In EmployeeReasonRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update EmployeeReason Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where Reason = @PKReason")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.Reason.Updated Then db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                If r.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                If r.WebApplyFlag.Updated Then db.AddInParameter(dbcmd, "@WebApplyFlag", DbType.String, r.WebApplyFlag.Value)
                If r.EmployeeWaitFlag.Updated Then db.AddInParameter(dbcmd, "@EmployeeWaitFlag", DbType.String, r.EmployeeWaitFlag.Value)
                If r.NotEmpLogFlag.Updated Then db.AddInParameter(dbcmd, "@NotEmpLogFlag", DbType.String, r.NotEmpLogFlag.Value)
                If r.InValidFlag.Updated Then db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, r.InValidFlag.Value)
                db.AddInParameter(dbcmd, "@PKReason", DbType.String, IIf(r.LoadFromDataRow, r.Reason.OldValue, r.Reason.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal EmployeeReasonRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From EmployeeReason")
            strSQL.AppendLine("Where Reason = @Reason")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@Reason", DbType.String, EmployeeReasonRow.Reason.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal EmployeeReasonRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From EmployeeReason")
            strSQL.AppendLine("Where Reason = @Reason")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@Reason", DbType.String, EmployeeReasonRow.Reason.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmployeeReason")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal EmployeeReasonRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into EmployeeReason")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    Reason, Remark, WebApplyFlag, EmployeeWaitFlag, NotEmpLogFlag, InValidFlag")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @Reason, @Remark, @WebApplyFlag, @EmployeeWaitFlag, @NotEmpLogFlag, @InValidFlag")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@Reason", DbType.String, EmployeeReasonRow.Reason.Value)
            db.AddInParameter(dbcmd, "@Remark", DbType.String, EmployeeReasonRow.Remark.Value)
            db.AddInParameter(dbcmd, "@WebApplyFlag", DbType.String, EmployeeReasonRow.WebApplyFlag.Value)
            db.AddInParameter(dbcmd, "@EmployeeWaitFlag", DbType.String, EmployeeReasonRow.EmployeeWaitFlag.Value)
            db.AddInParameter(dbcmd, "@NotEmpLogFlag", DbType.String, EmployeeReasonRow.NotEmpLogFlag.Value)
            db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, EmployeeReasonRow.InValidFlag.Value)

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal EmployeeReasonRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into EmployeeReason")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    Reason, Remark, WebApplyFlag, EmployeeWaitFlag, NotEmpLogFlag, InValidFlag")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @Reason, @Remark, @WebApplyFlag, @EmployeeWaitFlag, @NotEmpLogFlag, @InValidFlag")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@Reason", DbType.String, EmployeeReasonRow.Reason.Value)
            db.AddInParameter(dbcmd, "@Remark", DbType.String, EmployeeReasonRow.Remark.Value)
            db.AddInParameter(dbcmd, "@WebApplyFlag", DbType.String, EmployeeReasonRow.WebApplyFlag.Value)
            db.AddInParameter(dbcmd, "@EmployeeWaitFlag", DbType.String, EmployeeReasonRow.EmployeeWaitFlag.Value)
            db.AddInParameter(dbcmd, "@NotEmpLogFlag", DbType.String, EmployeeReasonRow.NotEmpLogFlag.Value)
            db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, EmployeeReasonRow.InValidFlag.Value)

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal EmployeeReasonRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into EmployeeReason")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    Reason, Remark, WebApplyFlag, EmployeeWaitFlag, NotEmpLogFlag, InValidFlag")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @Reason, @Remark, @WebApplyFlag, @EmployeeWaitFlag, @NotEmpLogFlag, @InValidFlag")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In EmployeeReasonRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                        db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                        db.AddInParameter(dbcmd, "@WebApplyFlag", DbType.String, r.WebApplyFlag.Value)
                        db.AddInParameter(dbcmd, "@EmployeeWaitFlag", DbType.String, r.EmployeeWaitFlag.Value)
                        db.AddInParameter(dbcmd, "@NotEmpLogFlag", DbType.String, r.NotEmpLogFlag.Value)
                        db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, r.InValidFlag.Value)

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

        Public Function Insert(ByVal EmployeeReasonRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into EmployeeReason")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    Reason, Remark, WebApplyFlag, EmployeeWaitFlag, NotEmpLogFlag, InValidFlag")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @Reason, @Remark, @WebApplyFlag, @EmployeeWaitFlag, @NotEmpLogFlag, @InValidFlag")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In EmployeeReasonRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                db.AddInParameter(dbcmd, "@WebApplyFlag", DbType.String, r.WebApplyFlag.Value)
                db.AddInParameter(dbcmd, "@EmployeeWaitFlag", DbType.String, r.EmployeeWaitFlag.Value)
                db.AddInParameter(dbcmd, "@NotEmpLogFlag", DbType.String, r.NotEmpLogFlag.Value)
                db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, r.InValidFlag.Value)

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

