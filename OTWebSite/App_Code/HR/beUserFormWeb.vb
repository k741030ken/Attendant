'****************************************************************
' Table:UserFormWeb
' Created Date: 2015.05.07
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beUserFormWeb
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "WebID", "LastChgComp", "LastChgID", "LastChgDate", "ReleaseComp", "ReleaseID" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(String), GetType(String) }
        Private m_PrimaryFields As String() = { "CompID", "WebID" }

        Public ReadOnly Property Rows() As beUserFormWeb.Rows 
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
        Public Sub Transfer2Row(UserFormWebTable As DataTable)
            For Each dr As DataRow In UserFormWebTable.Rows
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
                dr(m_Rows(i).WebID.FieldName) = m_Rows(i).WebID.Value
                dr(m_Rows(i).LastChgComp.FieldName) = m_Rows(i).LastChgComp.Value
                dr(m_Rows(i).LastChgID.FieldName) = m_Rows(i).LastChgID.Value
                dr(m_Rows(i).LastChgDate.FieldName) = m_Rows(i).LastChgDate.Value
                dr(m_Rows(i).ReleaseComp.FieldName) = m_Rows(i).ReleaseComp.Value
                dr(m_Rows(i).ReleaseID.FieldName) = m_Rows(i).ReleaseID.Value

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

        Public Sub Add(UserFormWebRow As Row)
            m_Rows.Add(UserFormWebRow)
        End Sub

        Public Sub Remove(UserFormWebRow As Row)
            If m_Rows.IndexOf(UserFormWebRow) >= 0 Then
                m_Rows.Remove(UserFormWebRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_WebID As Field(Of String) = new Field(Of String)("WebID", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private FI_ReleaseComp As Field(Of String) = new Field(Of String)("ReleaseComp", true)
        Private FI_ReleaseID As Field(Of String) = new Field(Of String)("ReleaseID", true)
        Private m_FieldNames As String() = { "CompID", "WebID", "LastChgComp", "LastChgID", "LastChgDate", "ReleaseComp", "ReleaseID" }
        Private m_PrimaryFields As String() = { "CompID", "WebID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CompID"
                    Return FI_CompID.Value
                Case "WebID"
                    Return FI_WebID.Value
                Case "LastChgComp"
                    Return FI_LastChgComp.Value
                Case "LastChgID"
                    Return FI_LastChgID.Value
                Case "LastChgDate"
                    Return FI_LastChgDate.Value
                Case "ReleaseComp"
                    Return FI_ReleaseComp.Value
                Case "ReleaseID"
                    Return FI_ReleaseID.Value
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Sub SetFieldValue(ByVal fieldName As String, ByVal value As Object)
            Select Case fieldName
                Case "CompID"
                    FI_CompID.SetValue(value)
                Case "WebID"
                    FI_WebID.SetValue(value)
                Case "LastChgComp"
                    FI_LastChgComp.SetValue(value)
                Case "LastChgID"
                    FI_LastChgID.SetValue(value)
                Case "LastChgDate"
                    FI_LastChgDate.SetValue(value)
                Case "ReleaseComp"
                    FI_ReleaseComp.SetValue(value)
                Case "ReleaseID"
                    FI_ReleaseID.SetValue(value)
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
                Case "WebID"
                    return FI_WebID.Updated
                Case "LastChgComp"
                    return FI_LastChgComp.Updated
                Case "LastChgID"
                    return FI_LastChgID.Updated
                Case "LastChgDate"
                    return FI_LastChgDate.Updated
                Case "ReleaseComp"
                    return FI_ReleaseComp.Updated
                Case "ReleaseID"
                    return FI_ReleaseID.Updated
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public Function CreateUpdateSQL(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "CompID"
                    return FI_CompID.CreateUpdateSQL
                Case "WebID"
                    return FI_WebID.CreateUpdateSQL
                Case "LastChgComp"
                    return FI_LastChgComp.CreateUpdateSQL
                Case "LastChgID"
                    return FI_LastChgID.CreateUpdateSQL
                Case "LastChgDate"
                    return FI_LastChgDate.CreateUpdateSQL
                Case "ReleaseComp"
                    return FI_ReleaseComp.CreateUpdateSQL
                Case "ReleaseID"
                    return FI_ReleaseID.CreateUpdateSQL
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
            FI_WebID.SetInitValue("")
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_ReleaseComp.SetInitValue("")
            FI_ReleaseID.SetInitValue("")
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CompID.SetInitValue(dr("CompID"))
            FI_WebID.SetInitValue(dr("WebID"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))
            FI_ReleaseComp.SetInitValue(dr("ReleaseComp"))
            FI_ReleaseID.SetInitValue(dr("ReleaseID"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CompID.Updated = False
            FI_WebID.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
            FI_ReleaseComp.Updated = False
            FI_ReleaseID.Updated = False
        End Sub

        Public ReadOnly Property CompID As Field(Of String) 
            Get
                Return FI_CompID
            End Get
        End Property

        Public ReadOnly Property WebID As Field(Of String) 
            Get
                Return FI_WebID
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

        Public ReadOnly Property ReleaseComp As Field(Of String) 
            Get
                Return FI_ReleaseComp
            End Get
        End Property

        Public ReadOnly Property ReleaseID As Field(Of String) 
            Get
                Return FI_ReleaseID
            End Get
        End Property

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal UserFormWebRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From UserFormWeb")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And WebID = @WebID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, UserFormWebRow.CompID.Value)
            db.AddInParameter(dbcmd, "@WebID", DbType.String, UserFormWebRow.WebID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal UserFormWebRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From UserFormWeb")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And WebID = @WebID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, UserFormWebRow.CompID.Value)
            db.AddInParameter(dbcmd, "@WebID", DbType.String, UserFormWebRow.WebID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal UserFormWebRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From UserFormWeb")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And WebID = @WebID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In UserFormWebRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@WebID", DbType.String, r.WebID.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal UserFormWebRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From UserFormWeb")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And WebID = @WebID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In UserFormWebRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@WebID", DbType.String, r.WebID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal UserFormWebRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From UserFormWeb")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And WebID = @WebID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, UserFormWebRow.CompID.Value)
            db.AddInParameter(dbcmd, "@WebID", DbType.String, UserFormWebRow.WebID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(UserFormWebRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From UserFormWeb")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And WebID = @WebID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, UserFormWebRow.CompID.Value)
            db.AddInParameter(dbcmd, "@WebID", DbType.String, UserFormWebRow.WebID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal UserFormWebRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update UserFormWeb Set")
            For i As Integer = 0 To UserFormWebRow.FieldNames.Length - 1
                If Not UserFormWebRow.IsIdentityField(UserFormWebRow.FieldNames(i)) AndAlso UserFormWebRow.IsUpdated(UserFormWebRow.FieldNames(i)) AndAlso UserFormWebRow.CreateUpdateSQL(UserFormWebRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, UserFormWebRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And WebID = @PKWebID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If UserFormWebRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, UserFormWebRow.CompID.Value)
            If UserFormWebRow.WebID.Updated Then db.AddInParameter(dbcmd, "@WebID", DbType.String, UserFormWebRow.WebID.Value)
            If UserFormWebRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, UserFormWebRow.LastChgComp.Value)
            If UserFormWebRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, UserFormWebRow.LastChgID.Value)
            If UserFormWebRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(UserFormWebRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), UserFormWebRow.LastChgDate.Value))
            If UserFormWebRow.ReleaseComp.Updated Then db.AddInParameter(dbcmd, "@ReleaseComp", DbType.String, UserFormWebRow.ReleaseComp.Value)
            If UserFormWebRow.ReleaseID.Updated Then db.AddInParameter(dbcmd, "@ReleaseID", DbType.String, UserFormWebRow.ReleaseID.Value)
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(UserFormWebRow.LoadFromDataRow, UserFormWebRow.CompID.OldValue, UserFormWebRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKWebID", DbType.String, IIf(UserFormWebRow.LoadFromDataRow, UserFormWebRow.WebID.OldValue, UserFormWebRow.WebID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal UserFormWebRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update UserFormWeb Set")
            For i As Integer = 0 To UserFormWebRow.FieldNames.Length - 1
                If Not UserFormWebRow.IsIdentityField(UserFormWebRow.FieldNames(i)) AndAlso UserFormWebRow.IsUpdated(UserFormWebRow.FieldNames(i)) AndAlso UserFormWebRow.CreateUpdateSQL(UserFormWebRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, UserFormWebRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And WebID = @PKWebID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If UserFormWebRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, UserFormWebRow.CompID.Value)
            If UserFormWebRow.WebID.Updated Then db.AddInParameter(dbcmd, "@WebID", DbType.String, UserFormWebRow.WebID.Value)
            If UserFormWebRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, UserFormWebRow.LastChgComp.Value)
            If UserFormWebRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, UserFormWebRow.LastChgID.Value)
            If UserFormWebRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(UserFormWebRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), UserFormWebRow.LastChgDate.Value))
            If UserFormWebRow.ReleaseComp.Updated Then db.AddInParameter(dbcmd, "@ReleaseComp", DbType.String, UserFormWebRow.ReleaseComp.Value)
            If UserFormWebRow.ReleaseID.Updated Then db.AddInParameter(dbcmd, "@ReleaseID", DbType.String, UserFormWebRow.ReleaseID.Value)
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(UserFormWebRow.LoadFromDataRow, UserFormWebRow.CompID.OldValue, UserFormWebRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKWebID", DbType.String, IIf(UserFormWebRow.LoadFromDataRow, UserFormWebRow.WebID.OldValue, UserFormWebRow.WebID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal UserFormWebRow As Row()) As Integer
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
                    For Each r As Row In UserFormWebRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update UserFormWeb Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where CompID = @PKCompID")
                        strSQL.AppendLine("And WebID = @PKWebID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.WebID.Updated Then db.AddInParameter(dbcmd, "@WebID", DbType.String, r.WebID.Value)
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        If r.ReleaseComp.Updated Then db.AddInParameter(dbcmd, "@ReleaseComp", DbType.String, r.ReleaseComp.Value)
                        If r.ReleaseID.Updated Then db.AddInParameter(dbcmd, "@ReleaseID", DbType.String, r.ReleaseID.Value)
                        db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                        db.AddInParameter(dbcmd, "@PKWebID", DbType.String, IIf(r.LoadFromDataRow, r.WebID.OldValue, r.WebID.Value))

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

        Public Function Update(ByVal UserFormWebRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In UserFormWebRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update UserFormWeb Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where CompID = @PKCompID")
                strSQL.AppendLine("And WebID = @PKWebID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.WebID.Updated Then db.AddInParameter(dbcmd, "@WebID", DbType.String, r.WebID.Value)
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                If r.ReleaseComp.Updated Then db.AddInParameter(dbcmd, "@ReleaseComp", DbType.String, r.ReleaseComp.Value)
                If r.ReleaseID.Updated Then db.AddInParameter(dbcmd, "@ReleaseID", DbType.String, r.ReleaseID.Value)
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                db.AddInParameter(dbcmd, "@PKWebID", DbType.String, IIf(r.LoadFromDataRow, r.WebID.OldValue, r.WebID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal UserFormWebRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From UserFormWeb")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And WebID = @WebID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, UserFormWebRow.CompID.Value)
            db.AddInParameter(dbcmd, "@WebID", DbType.String, UserFormWebRow.WebID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal UserFormWebRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From UserFormWeb")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And WebID = @WebID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, UserFormWebRow.CompID.Value)
            db.AddInParameter(dbcmd, "@WebID", DbType.String, UserFormWebRow.WebID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From UserFormWeb")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal UserFormWebRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into UserFormWeb")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, WebID, LastChgComp, LastChgID, LastChgDate, ReleaseComp, ReleaseID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @WebID, @LastChgComp, @LastChgID, @LastChgDate, @ReleaseComp, @ReleaseID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, UserFormWebRow.CompID.Value)
            db.AddInParameter(dbcmd, "@WebID", DbType.String, UserFormWebRow.WebID.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, UserFormWebRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, UserFormWebRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(UserFormWebRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), UserFormWebRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@ReleaseComp", DbType.String, UserFormWebRow.ReleaseComp.Value)
            db.AddInParameter(dbcmd, "@ReleaseID", DbType.String, UserFormWebRow.ReleaseID.Value)

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal UserFormWebRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into UserFormWeb")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, WebID, LastChgComp, LastChgID, LastChgDate, ReleaseComp, ReleaseID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @WebID, @LastChgComp, @LastChgID, @LastChgDate, @ReleaseComp, @ReleaseID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, UserFormWebRow.CompID.Value)
            db.AddInParameter(dbcmd, "@WebID", DbType.String, UserFormWebRow.WebID.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, UserFormWebRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, UserFormWebRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(UserFormWebRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), UserFormWebRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@ReleaseComp", DbType.String, UserFormWebRow.ReleaseComp.Value)
            db.AddInParameter(dbcmd, "@ReleaseID", DbType.String, UserFormWebRow.ReleaseID.Value)

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal UserFormWebRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into UserFormWeb")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, WebID, LastChgComp, LastChgID, LastChgDate, ReleaseComp, ReleaseID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @WebID, @LastChgComp, @LastChgID, @LastChgDate, @ReleaseComp, @ReleaseID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In UserFormWebRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@WebID", DbType.String, r.WebID.Value)
                        db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@ReleaseComp", DbType.String, r.ReleaseComp.Value)
                        db.AddInParameter(dbcmd, "@ReleaseID", DbType.String, r.ReleaseID.Value)

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

        Public Function Insert(ByVal UserFormWebRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into UserFormWeb")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, WebID, LastChgComp, LastChgID, LastChgDate, ReleaseComp, ReleaseID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @WebID, @LastChgComp, @LastChgID, @LastChgDate, @ReleaseComp, @ReleaseID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In UserFormWebRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@WebID", DbType.String, r.WebID.Value)
                db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@ReleaseComp", DbType.String, r.ReleaseComp.Value)
                db.AddInParameter(dbcmd, "@ReleaseID", DbType.String, r.ReleaseID.Value)

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

