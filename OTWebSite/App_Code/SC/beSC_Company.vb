'****************************************************************
' Table:SC_Company
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

Namespace beSC_Company
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "CompName", "CompFullName", "InValidFlag", "SortOrder", "LastChgDate", "LastChgComp", "LastChgID" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(String), GetType(String) }
        Private m_PrimaryFields As String() = { "CompID" }

        Public ReadOnly Property Rows() As beSC_Company.Rows 
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
        Public Sub Transfer2Row(SC_CompanyTable As DataTable)
            For Each dr As DataRow In SC_CompanyTable.Rows
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

                dr(m_Rows(i).CompID.FieldName) = m_Rows(i).CompID.Value
                dr(m_Rows(i).CompName.FieldName) = m_Rows(i).CompName.Value
                dr(m_Rows(i).CompFullName.FieldName) = m_Rows(i).CompFullName.Value
                dr(m_Rows(i).InValidFlag.FieldName) = m_Rows(i).InValidFlag.Value
                dr(m_Rows(i).SortOrder.FieldName) = m_Rows(i).SortOrder.Value
                dr(m_Rows(i).LastChgDate.FieldName) = m_Rows(i).LastChgDate.Value
                dr(m_Rows(i).LastChgComp.FieldName) = m_Rows(i).LastChgComp.Value
                dr(m_Rows(i).LastChgID.FieldName) = m_Rows(i).LastChgID.Value

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

        Public Sub Add(SC_CompanyRow As Row)
            m_Rows.Add(SC_CompanyRow)
        End Sub

        Public Sub Remove(SC_CompanyRow As Row)
            If m_Rows.IndexOf(SC_CompanyRow) >= 0 Then
                m_Rows.Remove(SC_CompanyRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_CompName As Field(Of String) = new Field(Of String)("CompName", true)
        Private FI_CompFullName As Field(Of String) = new Field(Of String)("CompFullName", true)
        Private FI_InValidFlag As Field(Of String) = new Field(Of String)("InValidFlag", true)
        Private FI_SortOrder As Field(Of String) = new Field(Of String)("SortOrder", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private m_FieldNames As String() = { "CompID", "CompName", "CompFullName", "InValidFlag", "SortOrder", "LastChgDate", "LastChgComp", "LastChgID" }
        Private m_PrimaryFields As String() = { "CompID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CompID"
                    Return FI_CompID.Value
                Case "CompName"
                    Return FI_CompName.Value
                Case "CompFullName"
                    Return FI_CompFullName.Value
                Case "InValidFlag"
                    Return FI_InValidFlag.Value
                Case "SortOrder"
                    Return FI_SortOrder.Value
                Case "LastChgDate"
                    Return FI_LastChgDate.Value
                Case "LastChgComp"
                    Return FI_LastChgComp.Value
                Case "LastChgID"
                    Return FI_LastChgID.Value
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Sub SetFieldValue(ByVal fieldName As String, ByVal value As Object)
            Select Case fieldName
                Case "CompID"
                    FI_CompID.SetValue(value)
                Case "CompName"
                    FI_CompName.SetValue(value)
                Case "CompFullName"
                    FI_CompFullName.SetValue(value)
                Case "InValidFlag"
                    FI_InValidFlag.SetValue(value)
                Case "SortOrder"
                    FI_SortOrder.SetValue(value)
                Case "LastChgDate"
                    FI_LastChgDate.SetValue(value)
                Case "LastChgComp"
                    FI_LastChgComp.SetValue(value)
                Case "LastChgID"
                    FI_LastChgID.SetValue(value)
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
                Case "CompID"
                    return FI_CompID.Updated
                Case "CompName"
                    return FI_CompName.Updated
                Case "CompFullName"
                    return FI_CompFullName.Updated
                Case "InValidFlag"
                    return FI_InValidFlag.Updated
                Case "SortOrder"
                    return FI_SortOrder.Updated
                Case "LastChgDate"
                    return FI_LastChgDate.Updated
                Case "LastChgComp"
                    return FI_LastChgComp.Updated
                Case "LastChgID"
                    return FI_LastChgID.Updated
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public Function CreateUpdateSQL(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "CompID"
                    return FI_CompID.CreateUpdateSQL
                Case "CompName"
                    return FI_CompName.CreateUpdateSQL
                Case "CompFullName"
                    return FI_CompFullName.CreateUpdateSQL
                Case "InValidFlag"
                    return FI_InValidFlag.CreateUpdateSQL
                Case "SortOrder"
                    return FI_SortOrder.CreateUpdateSQL
                Case "LastChgDate"
                    return FI_LastChgDate.CreateUpdateSQL
                Case "LastChgComp"
                    return FI_LastChgComp.CreateUpdateSQL
                Case "LastChgID"
                    return FI_LastChgID.CreateUpdateSQL
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
            FI_CompID.SetInitValue("")
            FI_CompName.SetInitValue("")
            FI_CompFullName.SetInitValue("")
            FI_InValidFlag.SetInitValue("")
            FI_SortOrder.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CompID.SetInitValue(dr("CompID"))
            FI_CompName.SetInitValue(dr("CompName"))
            FI_CompFullName.SetInitValue(dr("CompFullName"))
            FI_InValidFlag.SetInitValue(dr("InValidFlag"))
            FI_SortOrder.SetInitValue(dr("SortOrder"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CompID.Updated = False
            FI_CompName.Updated = False
            FI_CompFullName.Updated = False
            FI_InValidFlag.Updated = False
            FI_SortOrder.Updated = False
            FI_LastChgDate.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
        End Sub

        Public ReadOnly Property CompID As Field(Of String) 
            Get
                Return FI_CompID
            End Get
        End Property

        Public ReadOnly Property CompName As Field(Of String) 
            Get
                Return FI_CompName
            End Get
        End Property

        Public ReadOnly Property CompFullName As Field(Of String) 
            Get
                Return FI_CompFullName
            End Get
        End Property

        Public ReadOnly Property InValidFlag As Field(Of String) 
            Get
                Return FI_InValidFlag
            End Get
        End Property

        Public ReadOnly Property SortOrder As Field(Of String) 
            Get
                Return FI_SortOrder
            End Get
        End Property

        Public ReadOnly Property LastChgDate As Field(Of Date) 
            Get
                Return FI_LastChgDate
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

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal SC_CompanyRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_Company")
            strSQL.AppendLine("Where CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SC_CompanyRow.CompID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal SC_CompanyRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_Company")
            strSQL.AppendLine("Where CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SC_CompanyRow.CompID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal SC_CompanyRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_Company")
            strSQL.AppendLine("Where CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_CompanyRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal SC_CompanyRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_Company")
            strSQL.AppendLine("Where CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_CompanyRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal SC_CompanyRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_Company")
            strSQL.AppendLine("Where CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SC_CompanyRow.CompID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(SC_CompanyRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_Company")
            strSQL.AppendLine("Where CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SC_CompanyRow.CompID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_CompanyRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_Company Set")
            For i As Integer = 0 To SC_CompanyRow.FieldNames.Length - 1
                If Not SC_CompanyRow.IsIdentityField(SC_CompanyRow.FieldNames(i)) AndAlso SC_CompanyRow.IsUpdated(SC_CompanyRow.FieldNames(i)) AndAlso SC_CompanyRow.CreateUpdateSQL(SC_CompanyRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_CompanyRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_CompanyRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, SC_CompanyRow.CompID.Value)
            If SC_CompanyRow.CompName.Updated Then db.AddInParameter(dbcmd, "@CompName", DbType.String, SC_CompanyRow.CompName.Value)
            If SC_CompanyRow.CompFullName.Updated Then db.AddInParameter(dbcmd, "@CompFullName", DbType.String, SC_CompanyRow.CompFullName.Value)
            If SC_CompanyRow.InValidFlag.Updated Then db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, SC_CompanyRow.InValidFlag.Value)
            If SC_CompanyRow.SortOrder.Updated Then db.AddInParameter(dbcmd, "@SortOrder", DbType.String, SC_CompanyRow.SortOrder.Value)
            If SC_CompanyRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_CompanyRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_CompanyRow.LastChgDate.Value))
            If SC_CompanyRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, SC_CompanyRow.LastChgComp.Value)
            If SC_CompanyRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_CompanyRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(SC_CompanyRow.LoadFromDataRow, SC_CompanyRow.CompID.OldValue, SC_CompanyRow.CompID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal SC_CompanyRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_Company Set")
            For i As Integer = 0 To SC_CompanyRow.FieldNames.Length - 1
                If Not SC_CompanyRow.IsIdentityField(SC_CompanyRow.FieldNames(i)) AndAlso SC_CompanyRow.IsUpdated(SC_CompanyRow.FieldNames(i)) AndAlso SC_CompanyRow.CreateUpdateSQL(SC_CompanyRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_CompanyRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_CompanyRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, SC_CompanyRow.CompID.Value)
            If SC_CompanyRow.CompName.Updated Then db.AddInParameter(dbcmd, "@CompName", DbType.String, SC_CompanyRow.CompName.Value)
            If SC_CompanyRow.CompFullName.Updated Then db.AddInParameter(dbcmd, "@CompFullName", DbType.String, SC_CompanyRow.CompFullName.Value)
            If SC_CompanyRow.InValidFlag.Updated Then db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, SC_CompanyRow.InValidFlag.Value)
            If SC_CompanyRow.SortOrder.Updated Then db.AddInParameter(dbcmd, "@SortOrder", DbType.String, SC_CompanyRow.SortOrder.Value)
            If SC_CompanyRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_CompanyRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_CompanyRow.LastChgDate.Value))
            If SC_CompanyRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, SC_CompanyRow.LastChgComp.Value)
            If SC_CompanyRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_CompanyRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(SC_CompanyRow.LoadFromDataRow, SC_CompanyRow.CompID.OldValue, SC_CompanyRow.CompID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_CompanyRow As Row()) As Integer
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
                    For Each r As Row In SC_CompanyRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update SC_Company Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where CompID = @PKCompID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.CompName.Updated Then db.AddInParameter(dbcmd, "@CompName", DbType.String, r.CompName.Value)
                        If r.CompFullName.Updated Then db.AddInParameter(dbcmd, "@CompFullName", DbType.String, r.CompFullName.Value)
                        If r.InValidFlag.Updated Then db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, r.InValidFlag.Value)
                        If r.SortOrder.Updated Then db.AddInParameter(dbcmd, "@SortOrder", DbType.String, r.SortOrder.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))

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

        Public Function Update(ByVal SC_CompanyRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In SC_CompanyRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update SC_Company Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where CompID = @PKCompID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.CompName.Updated Then db.AddInParameter(dbcmd, "@CompName", DbType.String, r.CompName.Value)
                If r.CompFullName.Updated Then db.AddInParameter(dbcmd, "@CompFullName", DbType.String, r.CompFullName.Value)
                If r.InValidFlag.Updated Then db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, r.InValidFlag.Value)
                If r.SortOrder.Updated Then db.AddInParameter(dbcmd, "@SortOrder", DbType.String, r.SortOrder.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal SC_CompanyRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_Company")
            strSQL.AppendLine("Where CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SC_CompanyRow.CompID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal SC_CompanyRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_Company")
            strSQL.AppendLine("Where CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SC_CompanyRow.CompID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_Company")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal SC_CompanyRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_Company")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, CompName, CompFullName, InValidFlag, SortOrder, LastChgDate, LastChgComp, LastChgID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @CompName, @CompFullName, @InValidFlag, @SortOrder, @LastChgDate, @LastChgComp, @LastChgID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SC_CompanyRow.CompID.Value)
            db.AddInParameter(dbcmd, "@CompName", DbType.String, SC_CompanyRow.CompName.Value)
            db.AddInParameter(dbcmd, "@CompFullName", DbType.String, SC_CompanyRow.CompFullName.Value)
            db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, SC_CompanyRow.InValidFlag.Value)
            db.AddInParameter(dbcmd, "@SortOrder", DbType.String, SC_CompanyRow.SortOrder.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_CompanyRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_CompanyRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, SC_CompanyRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_CompanyRow.LastChgID.Value)

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal SC_CompanyRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_Company")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, CompName, CompFullName, InValidFlag, SortOrder, LastChgDate, LastChgComp, LastChgID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @CompName, @CompFullName, @InValidFlag, @SortOrder, @LastChgDate, @LastChgComp, @LastChgID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SC_CompanyRow.CompID.Value)
            db.AddInParameter(dbcmd, "@CompName", DbType.String, SC_CompanyRow.CompName.Value)
            db.AddInParameter(dbcmd, "@CompFullName", DbType.String, SC_CompanyRow.CompFullName.Value)
            db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, SC_CompanyRow.InValidFlag.Value)
            db.AddInParameter(dbcmd, "@SortOrder", DbType.String, SC_CompanyRow.SortOrder.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_CompanyRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_CompanyRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, SC_CompanyRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_CompanyRow.LastChgID.Value)

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal SC_CompanyRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_Company")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, CompName, CompFullName, InValidFlag, SortOrder, LastChgDate, LastChgComp, LastChgID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @CompName, @CompFullName, @InValidFlag, @SortOrder, @LastChgDate, @LastChgComp, @LastChgID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_CompanyRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@CompName", DbType.String, r.CompName.Value)
                        db.AddInParameter(dbcmd, "@CompFullName", DbType.String, r.CompFullName.Value)
                        db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, r.InValidFlag.Value)
                        db.AddInParameter(dbcmd, "@SortOrder", DbType.String, r.SortOrder.Value)
                        db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)

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

        Public Function Insert(ByVal SC_CompanyRow As Row(), ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_Company")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, CompName, CompFullName, InValidFlag, SortOrder, LastChgDate, LastChgComp, LastChgID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @CompName, @CompFullName, @InValidFlag, @SortOrder, @LastChgDate, @LastChgComp, @LastChgID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_CompanyRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@CompName", DbType.String, r.CompName.Value)
                db.AddInParameter(dbcmd, "@CompFullName", DbType.String, r.CompFullName.Value)
                db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, r.InValidFlag.Value)
                db.AddInParameter(dbcmd, "@SortOrder", DbType.String, r.SortOrder.Value)
                db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)

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

