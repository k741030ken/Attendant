'****************************************************************
' Table:SC_OnlineUser
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

Namespace beSC_OnlineUser
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "IP", "UserID", "CompID", "LoginDateTime", "LastResponseDateTime", "Site", "ActAs" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(Date), GetType(Date), GetType(String), GetType(String) }
        Private m_PrimaryFields As String() = { "IP", "UserID" }

        Public ReadOnly Property Rows() As beSC_OnlineUser.Rows 
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
        Public Sub Transfer2Row(SC_OnlineUserTable As DataTable)
            For Each dr As DataRow In SC_OnlineUserTable.Rows
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
                dr(m_Rows(i).CompID.FieldName) = m_Rows(i).CompID.Value
                dr(m_Rows(i).LoginDateTime.FieldName) = m_Rows(i).LoginDateTime.Value
                dr(m_Rows(i).LastResponseDateTime.FieldName) = m_Rows(i).LastResponseDateTime.Value
                dr(m_Rows(i).Site.FieldName) = m_Rows(i).Site.Value
                dr(m_Rows(i).ActAs.FieldName) = m_Rows(i).ActAs.Value

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

        Public Sub Add(SC_OnlineUserRow As Row)
            m_Rows.Add(SC_OnlineUserRow)
        End Sub

        Public Sub Remove(SC_OnlineUserRow As Row)
            If m_Rows.IndexOf(SC_OnlineUserRow) >= 0 Then
                m_Rows.Remove(SC_OnlineUserRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_IP As Field(Of String) = new Field(Of String)("IP", true)
        Private FI_UserID As Field(Of String) = new Field(Of String)("UserID", true)
        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_LoginDateTime As Field(Of Date) = new Field(Of Date)("LoginDateTime", true)
        Private FI_LastResponseDateTime As Field(Of Date) = new Field(Of Date)("LastResponseDateTime", true)
        Private FI_Site As Field(Of String) = new Field(Of String)("Site", true)
        Private FI_ActAs As Field(Of String) = new Field(Of String)("ActAs", true)
        Private m_FieldNames As String() = { "IP", "UserID", "CompID", "LoginDateTime", "LastResponseDateTime", "Site", "ActAs" }
        Private m_PrimaryFields As String() = { "IP", "UserID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "IP"
                    Return FI_IP.Value
                Case "UserID"
                    Return FI_UserID.Value
                Case "CompID"
                    Return FI_CompID.Value
                Case "LoginDateTime"
                    Return FI_LoginDateTime.Value
                Case "LastResponseDateTime"
                    Return FI_LastResponseDateTime.Value
                Case "Site"
                    Return FI_Site.Value
                Case "ActAs"
                    Return FI_ActAs.Value
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
                Case "CompID"
                    FI_CompID.SetValue(value)
                Case "LoginDateTime"
                    FI_LoginDateTime.SetValue(value)
                Case "LastResponseDateTime"
                    FI_LastResponseDateTime.SetValue(value)
                Case "Site"
                    FI_Site.SetValue(value)
                Case "ActAs"
                    FI_ActAs.SetValue(value)
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
                Case "CompID"
                    return FI_CompID.Updated
                Case "LoginDateTime"
                    return FI_LoginDateTime.Updated
                Case "LastResponseDateTime"
                    return FI_LastResponseDateTime.Updated
                Case "Site"
                    return FI_Site.Updated
                Case "ActAs"
                    return FI_ActAs.Updated
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
                Case "CompID"
                    return FI_CompID.CreateUpdateSQL
                Case "LoginDateTime"
                    return FI_LoginDateTime.CreateUpdateSQL
                Case "LastResponseDateTime"
                    return FI_LastResponseDateTime.CreateUpdateSQL
                Case "Site"
                    return FI_Site.CreateUpdateSQL
                Case "ActAs"
                    return FI_ActAs.CreateUpdateSQL
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
            FI_CompID.SetInitValue("")
            FI_LoginDateTime.SetInitValue(DateTime.Now)
            FI_LastResponseDateTime.SetInitValue(DateTime.Now)
            FI_Site.SetInitValue("")
            FI_ActAs.SetInitValue("")
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_IP.SetInitValue(dr("IP"))
            FI_UserID.SetInitValue(dr("UserID"))
            FI_CompID.SetInitValue(dr("CompID"))
            FI_LoginDateTime.SetInitValue(dr("LoginDateTime"))
            FI_LastResponseDateTime.SetInitValue(dr("LastResponseDateTime"))
            FI_Site.SetInitValue(dr("Site"))
            FI_ActAs.SetInitValue(dr("ActAs"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_IP.Updated = False
            FI_UserID.Updated = False
            FI_CompID.Updated = False
            FI_LoginDateTime.Updated = False
            FI_LastResponseDateTime.Updated = False
            FI_Site.Updated = False
            FI_ActAs.Updated = False
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

        Public ReadOnly Property CompID As Field(Of String) 
            Get
                Return FI_CompID
            End Get
        End Property

        Public ReadOnly Property LoginDateTime As Field(Of Date) 
            Get
                Return FI_LoginDateTime
            End Get
        End Property

        Public ReadOnly Property LastResponseDateTime As Field(Of Date) 
            Get
                Return FI_LastResponseDateTime
            End Get
        End Property

        Public ReadOnly Property Site As Field(Of String) 
            Get
                Return FI_Site
            End Get
        End Property

        Public ReadOnly Property ActAs As Field(Of String) 
            Get
                Return FI_ActAs
            End Get
        End Property

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal SC_OnlineUserRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_OnlineUser")
            strSQL.AppendLine("Where IP = @IP")
            strSQL.AppendLine("And UserID = @UserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IP", DbType.String, SC_OnlineUserRow.IP.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_OnlineUserRow.UserID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal SC_OnlineUserRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_OnlineUser")
            strSQL.AppendLine("Where IP = @IP")
            strSQL.AppendLine("And UserID = @UserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IP", DbType.String, SC_OnlineUserRow.IP.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_OnlineUserRow.UserID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal SC_OnlineUserRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_OnlineUser")
            strSQL.AppendLine("Where IP = @IP")
            strSQL.AppendLine("And UserID = @UserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_OnlineUserRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@IP", DbType.String, r.IP.Value)
                        db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal SC_OnlineUserRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_OnlineUser")
            strSQL.AppendLine("Where IP = @IP")
            strSQL.AppendLine("And UserID = @UserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_OnlineUserRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@IP", DbType.String, r.IP.Value)
                db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal SC_OnlineUserRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_OnlineUser")
            strSQL.AppendLine("Where IP = @IP")
            strSQL.AppendLine("And UserID = @UserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IP", DbType.String, SC_OnlineUserRow.IP.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_OnlineUserRow.UserID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(SC_OnlineUserRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_OnlineUser")
            strSQL.AppendLine("Where IP = @IP")
            strSQL.AppendLine("And UserID = @UserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IP", DbType.String, SC_OnlineUserRow.IP.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_OnlineUserRow.UserID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_OnlineUserRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_OnlineUser Set")
            For i As Integer = 0 To SC_OnlineUserRow.FieldNames.Length - 1
                If Not SC_OnlineUserRow.IsIdentityField(SC_OnlineUserRow.FieldNames(i)) AndAlso SC_OnlineUserRow.IsUpdated(SC_OnlineUserRow.FieldNames(i)) AndAlso SC_OnlineUserRow.CreateUpdateSQL(SC_OnlineUserRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_OnlineUserRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where IP = @PKIP")
            strSQL.AppendLine("And UserID = @PKUserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_OnlineUserRow.IP.Updated Then db.AddInParameter(dbcmd, "@IP", DbType.String, SC_OnlineUserRow.IP.Value)
            If SC_OnlineUserRow.UserID.Updated Then db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_OnlineUserRow.UserID.Value)
            If SC_OnlineUserRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, SC_OnlineUserRow.CompID.Value)
            If SC_OnlineUserRow.LoginDateTime.Updated Then db.AddInParameter(dbcmd, "@LoginDateTime", DbType.Date, IIf(IsDateTimeNull(SC_OnlineUserRow.LoginDateTime.Value), Convert.ToDateTime("1900/1/1"), SC_OnlineUserRow.LoginDateTime.Value))
            If SC_OnlineUserRow.LastResponseDateTime.Updated Then db.AddInParameter(dbcmd, "@LastResponseDateTime", DbType.Date, IIf(IsDateTimeNull(SC_OnlineUserRow.LastResponseDateTime.Value), Convert.ToDateTime("1900/1/1"), SC_OnlineUserRow.LastResponseDateTime.Value))
            If SC_OnlineUserRow.Site.Updated Then db.AddInParameter(dbcmd, "@Site", DbType.String, SC_OnlineUserRow.Site.Value)
            If SC_OnlineUserRow.ActAs.Updated Then db.AddInParameter(dbcmd, "@ActAs", DbType.String, SC_OnlineUserRow.ActAs.Value)
            db.AddInParameter(dbcmd, "@PKIP", DbType.String, IIf(SC_OnlineUserRow.LoadFromDataRow, SC_OnlineUserRow.IP.OldValue, SC_OnlineUserRow.IP.Value))
            db.AddInParameter(dbcmd, "@PKUserID", DbType.String, IIf(SC_OnlineUserRow.LoadFromDataRow, SC_OnlineUserRow.UserID.OldValue, SC_OnlineUserRow.UserID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal SC_OnlineUserRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_OnlineUser Set")
            For i As Integer = 0 To SC_OnlineUserRow.FieldNames.Length - 1
                If Not SC_OnlineUserRow.IsIdentityField(SC_OnlineUserRow.FieldNames(i)) AndAlso SC_OnlineUserRow.IsUpdated(SC_OnlineUserRow.FieldNames(i)) AndAlso SC_OnlineUserRow.CreateUpdateSQL(SC_OnlineUserRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_OnlineUserRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where IP = @PKIP")
            strSQL.AppendLine("And UserID = @PKUserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_OnlineUserRow.IP.Updated Then db.AddInParameter(dbcmd, "@IP", DbType.String, SC_OnlineUserRow.IP.Value)
            If SC_OnlineUserRow.UserID.Updated Then db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_OnlineUserRow.UserID.Value)
            If SC_OnlineUserRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, SC_OnlineUserRow.CompID.Value)
            If SC_OnlineUserRow.LoginDateTime.Updated Then db.AddInParameter(dbcmd, "@LoginDateTime", DbType.Date, IIf(IsDateTimeNull(SC_OnlineUserRow.LoginDateTime.Value), Convert.ToDateTime("1900/1/1"), SC_OnlineUserRow.LoginDateTime.Value))
            If SC_OnlineUserRow.LastResponseDateTime.Updated Then db.AddInParameter(dbcmd, "@LastResponseDateTime", DbType.Date, IIf(IsDateTimeNull(SC_OnlineUserRow.LastResponseDateTime.Value), Convert.ToDateTime("1900/1/1"), SC_OnlineUserRow.LastResponseDateTime.Value))
            If SC_OnlineUserRow.Site.Updated Then db.AddInParameter(dbcmd, "@Site", DbType.String, SC_OnlineUserRow.Site.Value)
            If SC_OnlineUserRow.ActAs.Updated Then db.AddInParameter(dbcmd, "@ActAs", DbType.String, SC_OnlineUserRow.ActAs.Value)
            db.AddInParameter(dbcmd, "@PKIP", DbType.String, IIf(SC_OnlineUserRow.LoadFromDataRow, SC_OnlineUserRow.IP.OldValue, SC_OnlineUserRow.IP.Value))
            db.AddInParameter(dbcmd, "@PKUserID", DbType.String, IIf(SC_OnlineUserRow.LoadFromDataRow, SC_OnlineUserRow.UserID.OldValue, SC_OnlineUserRow.UserID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_OnlineUserRow As Row()) As Integer
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
                    For Each r As Row In SC_OnlineUserRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update SC_OnlineUser Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where IP = @PKIP")
                        strSQL.AppendLine("And UserID = @PKUserID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.IP.Updated Then db.AddInParameter(dbcmd, "@IP", DbType.String, r.IP.Value)
                        If r.UserID.Updated Then db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.LoginDateTime.Updated Then db.AddInParameter(dbcmd, "@LoginDateTime", DbType.Date, IIf(IsDateTimeNull(r.LoginDateTime.Value), Convert.ToDateTime("1900/1/1"), r.LoginDateTime.Value))
                        If r.LastResponseDateTime.Updated Then db.AddInParameter(dbcmd, "@LastResponseDateTime", DbType.Date, IIf(IsDateTimeNull(r.LastResponseDateTime.Value), Convert.ToDateTime("1900/1/1"), r.LastResponseDateTime.Value))
                        If r.Site.Updated Then db.AddInParameter(dbcmd, "@Site", DbType.String, r.Site.Value)
                        If r.ActAs.Updated Then db.AddInParameter(dbcmd, "@ActAs", DbType.String, r.ActAs.Value)
                        db.AddInParameter(dbcmd, "@PKIP", DbType.String, IIf(r.LoadFromDataRow, r.IP.OldValue, r.IP.Value))
                        db.AddInParameter(dbcmd, "@PKUserID", DbType.String, IIf(r.LoadFromDataRow, r.UserID.OldValue, r.UserID.Value))

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

        Public Function Update(ByVal SC_OnlineUserRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In SC_OnlineUserRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update SC_OnlineUser Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where IP = @PKIP")
                strSQL.AppendLine("And UserID = @PKUserID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.IP.Updated Then db.AddInParameter(dbcmd, "@IP", DbType.String, r.IP.Value)
                If r.UserID.Updated Then db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.LoginDateTime.Updated Then db.AddInParameter(dbcmd, "@LoginDateTime", DbType.Date, IIf(IsDateTimeNull(r.LoginDateTime.Value), Convert.ToDateTime("1900/1/1"), r.LoginDateTime.Value))
                If r.LastResponseDateTime.Updated Then db.AddInParameter(dbcmd, "@LastResponseDateTime", DbType.Date, IIf(IsDateTimeNull(r.LastResponseDateTime.Value), Convert.ToDateTime("1900/1/1"), r.LastResponseDateTime.Value))
                If r.Site.Updated Then db.AddInParameter(dbcmd, "@Site", DbType.String, r.Site.Value)
                If r.ActAs.Updated Then db.AddInParameter(dbcmd, "@ActAs", DbType.String, r.ActAs.Value)
                db.AddInParameter(dbcmd, "@PKIP", DbType.String, IIf(r.LoadFromDataRow, r.IP.OldValue, r.IP.Value))
                db.AddInParameter(dbcmd, "@PKUserID", DbType.String, IIf(r.LoadFromDataRow, r.UserID.OldValue, r.UserID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal SC_OnlineUserRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_OnlineUser")
            strSQL.AppendLine("Where IP = @IP")
            strSQL.AppendLine("And UserID = @UserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IP", DbType.String, SC_OnlineUserRow.IP.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_OnlineUserRow.UserID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal SC_OnlineUserRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_OnlineUser")
            strSQL.AppendLine("Where IP = @IP")
            strSQL.AppendLine("And UserID = @UserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IP", DbType.String, SC_OnlineUserRow.IP.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_OnlineUserRow.UserID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_OnlineUser")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal SC_OnlineUserRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_OnlineUser")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    IP, UserID, CompID, LoginDateTime, LastResponseDateTime, Site, ActAs")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @IP, @UserID, @CompID, @LoginDateTime, @LastResponseDateTime, @Site, @ActAs")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IP", DbType.String, SC_OnlineUserRow.IP.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_OnlineUserRow.UserID.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SC_OnlineUserRow.CompID.Value)
            db.AddInParameter(dbcmd, "@LoginDateTime", DbType.Date, IIf(IsDateTimeNull(SC_OnlineUserRow.LoginDateTime.Value), Convert.ToDateTime("1900/1/1"), SC_OnlineUserRow.LoginDateTime.Value))
            db.AddInParameter(dbcmd, "@LastResponseDateTime", DbType.Date, IIf(IsDateTimeNull(SC_OnlineUserRow.LastResponseDateTime.Value), Convert.ToDateTime("1900/1/1"), SC_OnlineUserRow.LastResponseDateTime.Value))
            db.AddInParameter(dbcmd, "@Site", DbType.String, SC_OnlineUserRow.Site.Value)
            db.AddInParameter(dbcmd, "@ActAs", DbType.String, SC_OnlineUserRow.ActAs.Value)

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal SC_OnlineUserRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_OnlineUser")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    IP, UserID, CompID, LoginDateTime, LastResponseDateTime, Site, ActAs")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @IP, @UserID, @CompID, @LoginDateTime, @LastResponseDateTime, @Site, @ActAs")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IP", DbType.String, SC_OnlineUserRow.IP.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_OnlineUserRow.UserID.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SC_OnlineUserRow.CompID.Value)
            db.AddInParameter(dbcmd, "@LoginDateTime", DbType.Date, IIf(IsDateTimeNull(SC_OnlineUserRow.LoginDateTime.Value), Convert.ToDateTime("1900/1/1"), SC_OnlineUserRow.LoginDateTime.Value))
            db.AddInParameter(dbcmd, "@LastResponseDateTime", DbType.Date, IIf(IsDateTimeNull(SC_OnlineUserRow.LastResponseDateTime.Value), Convert.ToDateTime("1900/1/1"), SC_OnlineUserRow.LastResponseDateTime.Value))
            db.AddInParameter(dbcmd, "@Site", DbType.String, SC_OnlineUserRow.Site.Value)
            db.AddInParameter(dbcmd, "@ActAs", DbType.String, SC_OnlineUserRow.ActAs.Value)

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal SC_OnlineUserRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_OnlineUser")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    IP, UserID, CompID, LoginDateTime, LastResponseDateTime, Site, ActAs")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @IP, @UserID, @CompID, @LoginDateTime, @LastResponseDateTime, @Site, @ActAs")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_OnlineUserRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@IP", DbType.String, r.IP.Value)
                        db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@LoginDateTime", DbType.Date, IIf(IsDateTimeNull(r.LoginDateTime.Value), Convert.ToDateTime("1900/1/1"), r.LoginDateTime.Value))
                        db.AddInParameter(dbcmd, "@LastResponseDateTime", DbType.Date, IIf(IsDateTimeNull(r.LastResponseDateTime.Value), Convert.ToDateTime("1900/1/1"), r.LastResponseDateTime.Value))
                        db.AddInParameter(dbcmd, "@Site", DbType.String, r.Site.Value)
                        db.AddInParameter(dbcmd, "@ActAs", DbType.String, r.ActAs.Value)

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

        Public Function Insert(ByVal SC_OnlineUserRow As Row(), ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_OnlineUser")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    IP, UserID, CompID, LoginDateTime, LastResponseDateTime, Site, ActAs")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @IP, @UserID, @CompID, @LoginDateTime, @LastResponseDateTime, @Site, @ActAs")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_OnlineUserRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@IP", DbType.String, r.IP.Value)
                db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@LoginDateTime", DbType.Date, IIf(IsDateTimeNull(r.LoginDateTime.Value), Convert.ToDateTime("1900/1/1"), r.LoginDateTime.Value))
                db.AddInParameter(dbcmd, "@LastResponseDateTime", DbType.Date, IIf(IsDateTimeNull(r.LastResponseDateTime.Value), Convert.ToDateTime("1900/1/1"), r.LastResponseDateTime.Value))
                db.AddInParameter(dbcmd, "@Site", DbType.String, r.Site.Value)
                db.AddInParameter(dbcmd, "@ActAs", DbType.String, r.ActAs.Value)

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

