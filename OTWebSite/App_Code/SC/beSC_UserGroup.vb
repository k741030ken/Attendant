'****************************************************************
' Table:SC_UserGroup
' Created Date: 2014.08.29
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beSC_UserGroup
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "UserID", "SysID", "CompRoleID", "GroupID", "CreateDate", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(String), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "CompID", "UserID", "SysID", "CompRoleID", "GroupID" }

        Public ReadOnly Property Rows() As beSC_UserGroup.Rows 
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
        Public Sub Transfer2Row(SC_UserGroupTable As DataTable)
            For Each dr As DataRow In SC_UserGroupTable.Rows
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
                dr(m_Rows(i).UserID.FieldName) = m_Rows(i).UserID.Value
                dr(m_Rows(i).SysID.FieldName) = m_Rows(i).SysID.Value
                dr(m_Rows(i).CompRoleID.FieldName) = m_Rows(i).CompRoleID.Value
                dr(m_Rows(i).GroupID.FieldName) = m_Rows(i).GroupID.Value
                dr(m_Rows(i).CreateDate.FieldName) = m_Rows(i).CreateDate.Value
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

        Public Sub Add(SC_UserGroupRow As Row)
            m_Rows.Add(SC_UserGroupRow)
        End Sub

        Public Sub Remove(SC_UserGroupRow As Row)
            If m_Rows.IndexOf(SC_UserGroupRow) >= 0 Then
                m_Rows.Remove(SC_UserGroupRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_UserID As Field(Of String) = new Field(Of String)("UserID", true)
        Private FI_SysID As Field(Of String) = new Field(Of String)("SysID", true)
        Private FI_CompRoleID As Field(Of String) = new Field(Of String)("CompRoleID", true)
        Private FI_GroupID As Field(Of String) = new Field(Of String)("GroupID", true)
        Private FI_CreateDate As Field(Of Date) = new Field(Of Date)("CreateDate", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "CompID", "UserID", "SysID", "CompRoleID", "GroupID", "CreateDate", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "CompID", "UserID", "SysID", "CompRoleID", "GroupID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CompID"
                    Return FI_CompID.Value
                Case "UserID"
                    Return FI_UserID.Value
                Case "SysID"
                    Return FI_SysID.Value
                Case "CompRoleID"
                    Return FI_CompRoleID.Value
                Case "GroupID"
                    Return FI_GroupID.Value
                Case "CreateDate"
                    Return FI_CreateDate.Value
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
                Case "CompID"
                    FI_CompID.SetValue(value)
                Case "UserID"
                    FI_UserID.SetValue(value)
                Case "SysID"
                    FI_SysID.SetValue(value)
                Case "CompRoleID"
                    FI_CompRoleID.SetValue(value)
                Case "GroupID"
                    FI_GroupID.SetValue(value)
                Case "CreateDate"
                    FI_CreateDate.SetValue(value)
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
                Case "CompID"
                    return FI_CompID.Updated
                Case "UserID"
                    return FI_UserID.Updated
                Case "SysID"
                    return FI_SysID.Updated
                Case "CompRoleID"
                    return FI_CompRoleID.Updated
                Case "GroupID"
                    return FI_GroupID.Updated
                Case "CreateDate"
                    return FI_CreateDate.Updated
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
                Case "CompID"
                    return FI_CompID.CreateUpdateSQL
                Case "UserID"
                    return FI_UserID.CreateUpdateSQL
                Case "SysID"
                    return FI_SysID.CreateUpdateSQL
                Case "CompRoleID"
                    return FI_CompRoleID.CreateUpdateSQL
                Case "GroupID"
                    return FI_GroupID.CreateUpdateSQL
                Case "CreateDate"
                    return FI_CreateDate.CreateUpdateSQL
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
            FI_CompID.SetInitValue("")
            FI_UserID.SetInitValue("")
            FI_SysID.SetInitValue("")
            FI_CompRoleID.SetInitValue("")
            FI_GroupID.SetInitValue("")
            FI_CreateDate.SetInitValue(DateTime.Now)
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(DateTime.Now)
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CompID.SetInitValue(dr("CompID"))
            FI_UserID.SetInitValue(dr("UserID"))
            FI_SysID.SetInitValue(dr("SysID"))
            FI_CompRoleID.SetInitValue(dr("CompRoleID"))
            FI_GroupID.SetInitValue(dr("GroupID"))
            FI_CreateDate.SetInitValue(dr("CreateDate"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CompID.Updated = False
            FI_UserID.Updated = False
            FI_SysID.Updated = False
            FI_CompRoleID.Updated = False
            FI_GroupID.Updated = False
            FI_CreateDate.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
        End Sub

        Public ReadOnly Property CompID As Field(Of String) 
            Get
                Return FI_CompID
            End Get
        End Property

        Public ReadOnly Property UserID As Field(Of String) 
            Get
                Return FI_UserID
            End Get
        End Property

        Public ReadOnly Property SysID As Field(Of String) 
            Get
                Return FI_SysID
            End Get
        End Property

        Public ReadOnly Property CompRoleID As Field(Of String) 
            Get
                Return FI_CompRoleID
            End Get
        End Property

        Public ReadOnly Property GroupID As Field(Of String) 
            Get
                Return FI_GroupID
            End Get
        End Property

        Public ReadOnly Property CreateDate As Field(Of Date) 
            Get
                Return FI_CreateDate
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
        Public Function DeleteRowByPrimaryKey(ByVal SC_UserGroupRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_UserGroup")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And UserID = @UserID")
            strSQL.AppendLine("And SysID = @SysID")
            strSQL.AppendLine("And CompRoleID = @CompRoleID")
            strSQL.AppendLine("And GroupID = @GroupID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SC_UserGroupRow.CompID.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_UserGroupRow.UserID.Value)
            db.AddInParameter(dbcmd, "@SysID", DbType.String, SC_UserGroupRow.SysID.Value)
            db.AddInParameter(dbcmd, "@CompRoleID", DbType.String, SC_UserGroupRow.CompRoleID.Value)
            db.AddInParameter(dbcmd, "@GroupID", DbType.String, SC_UserGroupRow.GroupID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal SC_UserGroupRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_UserGroup")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And UserID = @UserID")
            strSQL.AppendLine("And SysID = @SysID")
            strSQL.AppendLine("And CompRoleID = @CompRoleID")
            strSQL.AppendLine("And GroupID = @GroupID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SC_UserGroupRow.CompID.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_UserGroupRow.UserID.Value)
            db.AddInParameter(dbcmd, "@SysID", DbType.String, SC_UserGroupRow.SysID.Value)
            db.AddInParameter(dbcmd, "@CompRoleID", DbType.String, SC_UserGroupRow.CompRoleID.Value)
            db.AddInParameter(dbcmd, "@GroupID", DbType.String, SC_UserGroupRow.GroupID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal SC_UserGroupRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_UserGroup")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And UserID = @UserID")
            strSQL.AppendLine("And SysID = @SysID")
            strSQL.AppendLine("And CompRoleID = @CompRoleID")
            strSQL.AppendLine("And GroupID = @GroupID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_UserGroupRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                        db.AddInParameter(dbcmd, "@SysID", DbType.String, r.SysID.Value)
                        db.AddInParameter(dbcmd, "@CompRoleID", DbType.String, r.CompRoleID.Value)
                        db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal SC_UserGroupRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_UserGroup")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And UserID = @UserID")
            strSQL.AppendLine("And SysID = @SysID")
            strSQL.AppendLine("And CompRoleID = @CompRoleID")
            strSQL.AppendLine("And GroupID = @GroupID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_UserGroupRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                db.AddInParameter(dbcmd, "@SysID", DbType.String, r.SysID.Value)
                db.AddInParameter(dbcmd, "@CompRoleID", DbType.String, r.CompRoleID.Value)
                db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal SC_UserGroupRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_UserGroup")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And UserID = @UserID")
            strSQL.AppendLine("And SysID = @SysID")
            strSQL.AppendLine("And CompRoleID = @CompRoleID")
            strSQL.AppendLine("And GroupID = @GroupID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SC_UserGroupRow.CompID.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_UserGroupRow.UserID.Value)
            db.AddInParameter(dbcmd, "@SysID", DbType.String, SC_UserGroupRow.SysID.Value)
            db.AddInParameter(dbcmd, "@CompRoleID", DbType.String, SC_UserGroupRow.CompRoleID.Value)
            db.AddInParameter(dbcmd, "@GroupID", DbType.String, SC_UserGroupRow.GroupID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(SC_UserGroupRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_UserGroup")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And UserID = @UserID")
            strSQL.AppendLine("And SysID = @SysID")
            strSQL.AppendLine("And CompRoleID = @CompRoleID")
            strSQL.AppendLine("And GroupID = @GroupID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SC_UserGroupRow.CompID.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_UserGroupRow.UserID.Value)
            db.AddInParameter(dbcmd, "@SysID", DbType.String, SC_UserGroupRow.SysID.Value)
            db.AddInParameter(dbcmd, "@CompRoleID", DbType.String, SC_UserGroupRow.CompRoleID.Value)
            db.AddInParameter(dbcmd, "@GroupID", DbType.String, SC_UserGroupRow.GroupID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_UserGroupRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_UserGroup Set")
            For i As Integer = 0 To SC_UserGroupRow.FieldNames.Length - 1
                If Not SC_UserGroupRow.IsIdentityField(SC_UserGroupRow.FieldNames(i)) AndAlso SC_UserGroupRow.IsUpdated(SC_UserGroupRow.FieldNames(i)) AndAlso SC_UserGroupRow.CreateUpdateSQL(SC_UserGroupRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_UserGroupRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And UserID = @PKUserID")
            strSQL.AppendLine("And SysID = @PKSysID")
            strSQL.AppendLine("And CompRoleID = @PKCompRoleID")
            strSQL.AppendLine("And GroupID = @PKGroupID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_UserGroupRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, SC_UserGroupRow.CompID.Value)
            If SC_UserGroupRow.UserID.Updated Then db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_UserGroupRow.UserID.Value)
            If SC_UserGroupRow.SysID.Updated Then db.AddInParameter(dbcmd, "@SysID", DbType.String, SC_UserGroupRow.SysID.Value)
            If SC_UserGroupRow.CompRoleID.Updated Then db.AddInParameter(dbcmd, "@CompRoleID", DbType.String, SC_UserGroupRow.CompRoleID.Value)
            If SC_UserGroupRow.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, SC_UserGroupRow.GroupID.Value)
            If SC_UserGroupRow.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_UserGroupRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserGroupRow.CreateDate.Value))
            If SC_UserGroupRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, SC_UserGroupRow.LastChgComp.Value)
            If SC_UserGroupRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_UserGroupRow.LastChgID.Value)
            If SC_UserGroupRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_UserGroupRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserGroupRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(SC_UserGroupRow.LoadFromDataRow, SC_UserGroupRow.CompID.OldValue, SC_UserGroupRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKUserID", DbType.String, IIf(SC_UserGroupRow.LoadFromDataRow, SC_UserGroupRow.UserID.OldValue, SC_UserGroupRow.UserID.Value))
            db.AddInParameter(dbcmd, "@PKSysID", DbType.String, IIf(SC_UserGroupRow.LoadFromDataRow, SC_UserGroupRow.SysID.OldValue, SC_UserGroupRow.SysID.Value))
            db.AddInParameter(dbcmd, "@PKCompRoleID", DbType.String, IIf(SC_UserGroupRow.LoadFromDataRow, SC_UserGroupRow.CompRoleID.OldValue, SC_UserGroupRow.CompRoleID.Value))
            db.AddInParameter(dbcmd, "@PKGroupID", DbType.String, IIf(SC_UserGroupRow.LoadFromDataRow, SC_UserGroupRow.GroupID.OldValue, SC_UserGroupRow.GroupID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal SC_UserGroupRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_UserGroup Set")
            For i As Integer = 0 To SC_UserGroupRow.FieldNames.Length - 1
                If Not SC_UserGroupRow.IsIdentityField(SC_UserGroupRow.FieldNames(i)) AndAlso SC_UserGroupRow.IsUpdated(SC_UserGroupRow.FieldNames(i)) AndAlso SC_UserGroupRow.CreateUpdateSQL(SC_UserGroupRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_UserGroupRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And UserID = @PKUserID")
            strSQL.AppendLine("And SysID = @PKSysID")
            strSQL.AppendLine("And CompRoleID = @PKCompRoleID")
            strSQL.AppendLine("And GroupID = @PKGroupID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_UserGroupRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, SC_UserGroupRow.CompID.Value)
            If SC_UserGroupRow.UserID.Updated Then db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_UserGroupRow.UserID.Value)
            If SC_UserGroupRow.SysID.Updated Then db.AddInParameter(dbcmd, "@SysID", DbType.String, SC_UserGroupRow.SysID.Value)
            If SC_UserGroupRow.CompRoleID.Updated Then db.AddInParameter(dbcmd, "@CompRoleID", DbType.String, SC_UserGroupRow.CompRoleID.Value)
            If SC_UserGroupRow.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, SC_UserGroupRow.GroupID.Value)
            If SC_UserGroupRow.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_UserGroupRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserGroupRow.CreateDate.Value))
            If SC_UserGroupRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, SC_UserGroupRow.LastChgComp.Value)
            If SC_UserGroupRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_UserGroupRow.LastChgID.Value)
            If SC_UserGroupRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_UserGroupRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserGroupRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(SC_UserGroupRow.LoadFromDataRow, SC_UserGroupRow.CompID.OldValue, SC_UserGroupRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKUserID", DbType.String, IIf(SC_UserGroupRow.LoadFromDataRow, SC_UserGroupRow.UserID.OldValue, SC_UserGroupRow.UserID.Value))
            db.AddInParameter(dbcmd, "@PKSysID", DbType.String, IIf(SC_UserGroupRow.LoadFromDataRow, SC_UserGroupRow.SysID.OldValue, SC_UserGroupRow.SysID.Value))
            db.AddInParameter(dbcmd, "@PKCompRoleID", DbType.String, IIf(SC_UserGroupRow.LoadFromDataRow, SC_UserGroupRow.CompRoleID.OldValue, SC_UserGroupRow.CompRoleID.Value))
            db.AddInParameter(dbcmd, "@PKGroupID", DbType.String, IIf(SC_UserGroupRow.LoadFromDataRow, SC_UserGroupRow.GroupID.OldValue, SC_UserGroupRow.GroupID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_UserGroupRow As Row()) As Integer
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
                    For Each r As Row In SC_UserGroupRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update SC_UserGroup Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where CompID = @PKCompID")
                        strSQL.AppendLine("And UserID = @PKUserID")
                        strSQL.AppendLine("And SysID = @PKSysID")
                        strSQL.AppendLine("And CompRoleID = @PKCompRoleID")
                        strSQL.AppendLine("And GroupID = @PKGroupID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.UserID.Updated Then db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                        If r.SysID.Updated Then db.AddInParameter(dbcmd, "@SysID", DbType.String, r.SysID.Value)
                        If r.CompRoleID.Updated Then db.AddInParameter(dbcmd, "@CompRoleID", DbType.String, r.CompRoleID.Value)
                        If r.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                        If r.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                        db.AddInParameter(dbcmd, "@PKUserID", DbType.String, IIf(r.LoadFromDataRow, r.UserID.OldValue, r.UserID.Value))
                        db.AddInParameter(dbcmd, "@PKSysID", DbType.String, IIf(r.LoadFromDataRow, r.SysID.OldValue, r.SysID.Value))
                        db.AddInParameter(dbcmd, "@PKCompRoleID", DbType.String, IIf(r.LoadFromDataRow, r.CompRoleID.OldValue, r.CompRoleID.Value))
                        db.AddInParameter(dbcmd, "@PKGroupID", DbType.String, IIf(r.LoadFromDataRow, r.GroupID.OldValue, r.GroupID.Value))

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

        Public Function Update(ByVal SC_UserGroupRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In SC_UserGroupRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update SC_UserGroup Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where CompID = @PKCompID")
                strSQL.AppendLine("And UserID = @PKUserID")
                strSQL.AppendLine("And SysID = @PKSysID")
                strSQL.AppendLine("And CompRoleID = @PKCompRoleID")
                strSQL.AppendLine("And GroupID = @PKGroupID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.UserID.Updated Then db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                If r.SysID.Updated Then db.AddInParameter(dbcmd, "@SysID", DbType.String, r.SysID.Value)
                If r.CompRoleID.Updated Then db.AddInParameter(dbcmd, "@CompRoleID", DbType.String, r.CompRoleID.Value)
                If r.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                If r.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                db.AddInParameter(dbcmd, "@PKUserID", DbType.String, IIf(r.LoadFromDataRow, r.UserID.OldValue, r.UserID.Value))
                db.AddInParameter(dbcmd, "@PKSysID", DbType.String, IIf(r.LoadFromDataRow, r.SysID.OldValue, r.SysID.Value))
                db.AddInParameter(dbcmd, "@PKCompRoleID", DbType.String, IIf(r.LoadFromDataRow, r.CompRoleID.OldValue, r.CompRoleID.Value))
                db.AddInParameter(dbcmd, "@PKGroupID", DbType.String, IIf(r.LoadFromDataRow, r.GroupID.OldValue, r.GroupID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal SC_UserGroupRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_UserGroup")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And UserID = @UserID")
            strSQL.AppendLine("And SysID = @SysID")
            strSQL.AppendLine("And CompRoleID = @CompRoleID")
            strSQL.AppendLine("And GroupID = @GroupID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SC_UserGroupRow.CompID.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_UserGroupRow.UserID.Value)
            db.AddInParameter(dbcmd, "@SysID", DbType.String, SC_UserGroupRow.SysID.Value)
            db.AddInParameter(dbcmd, "@CompRoleID", DbType.String, SC_UserGroupRow.CompRoleID.Value)
            db.AddInParameter(dbcmd, "@GroupID", DbType.String, SC_UserGroupRow.GroupID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal SC_UserGroupRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_UserGroup")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And UserID = @UserID")
            strSQL.AppendLine("And SysID = @SysID")
            strSQL.AppendLine("And CompRoleID = @CompRoleID")
            strSQL.AppendLine("And GroupID = @GroupID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SC_UserGroupRow.CompID.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_UserGroupRow.UserID.Value)
            db.AddInParameter(dbcmd, "@SysID", DbType.String, SC_UserGroupRow.SysID.Value)
            db.AddInParameter(dbcmd, "@CompRoleID", DbType.String, SC_UserGroupRow.CompRoleID.Value)
            db.AddInParameter(dbcmd, "@GroupID", DbType.String, SC_UserGroupRow.GroupID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_UserGroup")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal SC_UserGroupRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_UserGroup")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, UserID, SysID, CompRoleID, GroupID, CreateDate, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @UserID, @SysID, @CompRoleID, @GroupID, @CreateDate, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SC_UserGroupRow.CompID.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_UserGroupRow.UserID.Value)
            db.AddInParameter(dbcmd, "@SysID", DbType.String, SC_UserGroupRow.SysID.Value)
            db.AddInParameter(dbcmd, "@CompRoleID", DbType.String, SC_UserGroupRow.CompRoleID.Value)
            db.AddInParameter(dbcmd, "@GroupID", DbType.String, SC_UserGroupRow.GroupID.Value)
            db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_UserGroupRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserGroupRow.CreateDate.Value))
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, SC_UserGroupRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_UserGroupRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_UserGroupRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserGroupRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal SC_UserGroupRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_UserGroup")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, UserID, SysID, CompRoleID, GroupID, CreateDate, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @UserID, @SysID, @CompRoleID, @GroupID, @CreateDate, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SC_UserGroupRow.CompID.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_UserGroupRow.UserID.Value)
            db.AddInParameter(dbcmd, "@SysID", DbType.String, SC_UserGroupRow.SysID.Value)
            db.AddInParameter(dbcmd, "@CompRoleID", DbType.String, SC_UserGroupRow.CompRoleID.Value)
            db.AddInParameter(dbcmd, "@GroupID", DbType.String, SC_UserGroupRow.GroupID.Value)
            db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_UserGroupRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserGroupRow.CreateDate.Value))
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, SC_UserGroupRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_UserGroupRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_UserGroupRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_UserGroupRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal SC_UserGroupRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_UserGroup")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, UserID, SysID, CompRoleID, GroupID, CreateDate, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @UserID, @SysID, @CompRoleID, @GroupID, @CreateDate, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_UserGroupRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                        db.AddInParameter(dbcmd, "@SysID", DbType.String, r.SysID.Value)
                        db.AddInParameter(dbcmd, "@CompRoleID", DbType.String, r.CompRoleID.Value)
                        db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                        db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
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

        Public Function Insert(ByVal SC_UserGroupRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_UserGroup")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, UserID, SysID, CompRoleID, GroupID, CreateDate, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @UserID, @SysID, @CompRoleID, @GroupID, @CreateDate, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_UserGroupRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                db.AddInParameter(dbcmd, "@SysID", DbType.String, r.SysID.Value)
                db.AddInParameter(dbcmd, "@CompRoleID", DbType.String, r.CompRoleID.Value)
                db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
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

