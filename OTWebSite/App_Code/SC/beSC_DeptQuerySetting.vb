'****************************************************************
' Table:SC_DeptQuerySetting
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

Namespace beSC_DeptQuerySetting
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "OrganID", "UserID", "GroupID", "QueryOrganID" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String) }
        Private m_PrimaryFields As String() = { "OrganID", "UserID", "GroupID" }

        Public ReadOnly Property Rows() As beSC_DeptQuerySetting.Rows 
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
        Public Sub Transfer2Row(SC_DeptQuerySettingTable As DataTable)
            For Each dr As DataRow In SC_DeptQuerySettingTable.Rows
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

                dr(m_Rows(i).OrganID.FieldName) = m_Rows(i).OrganID.Value
                dr(m_Rows(i).UserID.FieldName) = m_Rows(i).UserID.Value
                dr(m_Rows(i).GroupID.FieldName) = m_Rows(i).GroupID.Value
                dr(m_Rows(i).QueryOrganID.FieldName) = m_Rows(i).QueryOrganID.Value

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

        Public Sub Add(SC_DeptQuerySettingRow As Row)
            m_Rows.Add(SC_DeptQuerySettingRow)
        End Sub

        Public Sub Remove(SC_DeptQuerySettingRow As Row)
            If m_Rows.IndexOf(SC_DeptQuerySettingRow) >= 0 Then
                m_Rows.Remove(SC_DeptQuerySettingRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_OrganID As Field(Of String) = new Field(Of String)("OrganID", true)
        Private FI_UserID As Field(Of String) = new Field(Of String)("UserID", true)
        Private FI_GroupID As Field(Of String) = new Field(Of String)("GroupID", true)
        Private FI_QueryOrganID As Field(Of String) = new Field(Of String)("QueryOrganID", true)
        Private m_FieldNames As String() = { "OrganID", "UserID", "GroupID", "QueryOrganID" }
        Private m_PrimaryFields As String() = { "OrganID", "UserID", "GroupID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "OrganID"
                    Return FI_OrganID.Value
                Case "UserID"
                    Return FI_UserID.Value
                Case "GroupID"
                    Return FI_GroupID.Value
                Case "QueryOrganID"
                    Return FI_QueryOrganID.Value
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Sub SetFieldValue(ByVal fieldName As String, ByVal value As Object)
            Select Case fieldName
                Case "OrganID"
                    FI_OrganID.SetValue(value)
                Case "UserID"
                    FI_UserID.SetValue(value)
                Case "GroupID"
                    FI_GroupID.SetValue(value)
                Case "QueryOrganID"
                    FI_QueryOrganID.SetValue(value)
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
                Case "OrganID"
                    return FI_OrganID.Updated
                Case "UserID"
                    return FI_UserID.Updated
                Case "GroupID"
                    return FI_GroupID.Updated
                Case "QueryOrganID"
                    return FI_QueryOrganID.Updated
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public Function CreateUpdateSQL(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "OrganID"
                    return FI_OrganID.CreateUpdateSQL
                Case "UserID"
                    return FI_UserID.CreateUpdateSQL
                Case "GroupID"
                    return FI_GroupID.CreateUpdateSQL
                Case "QueryOrganID"
                    return FI_QueryOrganID.CreateUpdateSQL
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
            FI_OrganID.SetInitValue("")
            FI_UserID.SetInitValue("")
            FI_GroupID.SetInitValue("")
            FI_QueryOrganID.SetInitValue("")
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_OrganID.SetInitValue(dr("OrganID"))
            FI_UserID.SetInitValue(dr("UserID"))
            FI_GroupID.SetInitValue(dr("GroupID"))
            FI_QueryOrganID.SetInitValue(dr("QueryOrganID"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_OrganID.Updated = False
            FI_UserID.Updated = False
            FI_GroupID.Updated = False
            FI_QueryOrganID.Updated = False
        End Sub

        Public ReadOnly Property OrganID As Field(Of String) 
            Get
                Return FI_OrganID
            End Get
        End Property

        Public ReadOnly Property UserID As Field(Of String) 
            Get
                Return FI_UserID
            End Get
        End Property

        Public ReadOnly Property GroupID As Field(Of String) 
            Get
                Return FI_GroupID
            End Get
        End Property

        Public ReadOnly Property QueryOrganID As Field(Of String) 
            Get
                Return FI_QueryOrganID
            End Get
        End Property

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal SC_DeptQuerySettingRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_DeptQuerySetting")
            strSQL.AppendLine("Where OrganID = @OrganID")
            strSQL.AppendLine("And UserID = @UserID")
            strSQL.AppendLine("And GroupID = @GroupID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, SC_DeptQuerySettingRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_DeptQuerySettingRow.UserID.Value)
            db.AddInParameter(dbcmd, "@GroupID", DbType.String, SC_DeptQuerySettingRow.GroupID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal SC_DeptQuerySettingRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_DeptQuerySetting")
            strSQL.AppendLine("Where OrganID = @OrganID")
            strSQL.AppendLine("And UserID = @UserID")
            strSQL.AppendLine("And GroupID = @GroupID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, SC_DeptQuerySettingRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_DeptQuerySettingRow.UserID.Value)
            db.AddInParameter(dbcmd, "@GroupID", DbType.String, SC_DeptQuerySettingRow.GroupID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal SC_DeptQuerySettingRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_DeptQuerySetting")
            strSQL.AppendLine("Where OrganID = @OrganID")
            strSQL.AppendLine("And UserID = @UserID")
            strSQL.AppendLine("And GroupID = @GroupID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_DeptQuerySettingRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                        db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
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

        Public Function DeleteRowByPrimaryKey(ByVal SC_DeptQuerySettingRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_DeptQuerySetting")
            strSQL.AppendLine("Where OrganID = @OrganID")
            strSQL.AppendLine("And UserID = @UserID")
            strSQL.AppendLine("And GroupID = @GroupID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_DeptQuerySettingRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal SC_DeptQuerySettingRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_DeptQuerySetting")
            strSQL.AppendLine("Where OrganID = @OrganID")
            strSQL.AppendLine("And UserID = @UserID")
            strSQL.AppendLine("And GroupID = @GroupID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, SC_DeptQuerySettingRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_DeptQuerySettingRow.UserID.Value)
            db.AddInParameter(dbcmd, "@GroupID", DbType.String, SC_DeptQuerySettingRow.GroupID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(SC_DeptQuerySettingRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_DeptQuerySetting")
            strSQL.AppendLine("Where OrganID = @OrganID")
            strSQL.AppendLine("And UserID = @UserID")
            strSQL.AppendLine("And GroupID = @GroupID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, SC_DeptQuerySettingRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_DeptQuerySettingRow.UserID.Value)
            db.AddInParameter(dbcmd, "@GroupID", DbType.String, SC_DeptQuerySettingRow.GroupID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_DeptQuerySettingRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_DeptQuerySetting Set")
            For i As Integer = 0 To SC_DeptQuerySettingRow.FieldNames.Length - 1
                If Not SC_DeptQuerySettingRow.IsIdentityField(SC_DeptQuerySettingRow.FieldNames(i)) AndAlso SC_DeptQuerySettingRow.IsUpdated(SC_DeptQuerySettingRow.FieldNames(i)) AndAlso SC_DeptQuerySettingRow.CreateUpdateSQL(SC_DeptQuerySettingRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_DeptQuerySettingRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where OrganID = @PKOrganID")
            strSQL.AppendLine("And UserID = @PKUserID")
            strSQL.AppendLine("And GroupID = @PKGroupID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_DeptQuerySettingRow.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, SC_DeptQuerySettingRow.OrganID.Value)
            If SC_DeptQuerySettingRow.UserID.Updated Then db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_DeptQuerySettingRow.UserID.Value)
            If SC_DeptQuerySettingRow.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, SC_DeptQuerySettingRow.GroupID.Value)
            If SC_DeptQuerySettingRow.QueryOrganID.Updated Then db.AddInParameter(dbcmd, "@QueryOrganID", DbType.String, SC_DeptQuerySettingRow.QueryOrganID.Value)
            db.AddInParameter(dbcmd, "@PKOrganID", DbType.String, IIf(SC_DeptQuerySettingRow.LoadFromDataRow, SC_DeptQuerySettingRow.OrganID.OldValue, SC_DeptQuerySettingRow.OrganID.Value))
            db.AddInParameter(dbcmd, "@PKUserID", DbType.String, IIf(SC_DeptQuerySettingRow.LoadFromDataRow, SC_DeptQuerySettingRow.UserID.OldValue, SC_DeptQuerySettingRow.UserID.Value))
            db.AddInParameter(dbcmd, "@PKGroupID", DbType.String, IIf(SC_DeptQuerySettingRow.LoadFromDataRow, SC_DeptQuerySettingRow.GroupID.OldValue, SC_DeptQuerySettingRow.GroupID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal SC_DeptQuerySettingRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_DeptQuerySetting Set")
            For i As Integer = 0 To SC_DeptQuerySettingRow.FieldNames.Length - 1
                If Not SC_DeptQuerySettingRow.IsIdentityField(SC_DeptQuerySettingRow.FieldNames(i)) AndAlso SC_DeptQuerySettingRow.IsUpdated(SC_DeptQuerySettingRow.FieldNames(i)) AndAlso SC_DeptQuerySettingRow.CreateUpdateSQL(SC_DeptQuerySettingRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_DeptQuerySettingRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where OrganID = @PKOrganID")
            strSQL.AppendLine("And UserID = @PKUserID")
            strSQL.AppendLine("And GroupID = @PKGroupID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_DeptQuerySettingRow.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, SC_DeptQuerySettingRow.OrganID.Value)
            If SC_DeptQuerySettingRow.UserID.Updated Then db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_DeptQuerySettingRow.UserID.Value)
            If SC_DeptQuerySettingRow.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, SC_DeptQuerySettingRow.GroupID.Value)
            If SC_DeptQuerySettingRow.QueryOrganID.Updated Then db.AddInParameter(dbcmd, "@QueryOrganID", DbType.String, SC_DeptQuerySettingRow.QueryOrganID.Value)
            db.AddInParameter(dbcmd, "@PKOrganID", DbType.String, IIf(SC_DeptQuerySettingRow.LoadFromDataRow, SC_DeptQuerySettingRow.OrganID.OldValue, SC_DeptQuerySettingRow.OrganID.Value))
            db.AddInParameter(dbcmd, "@PKUserID", DbType.String, IIf(SC_DeptQuerySettingRow.LoadFromDataRow, SC_DeptQuerySettingRow.UserID.OldValue, SC_DeptQuerySettingRow.UserID.Value))
            db.AddInParameter(dbcmd, "@PKGroupID", DbType.String, IIf(SC_DeptQuerySettingRow.LoadFromDataRow, SC_DeptQuerySettingRow.GroupID.OldValue, SC_DeptQuerySettingRow.GroupID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_DeptQuerySettingRow As Row()) As Integer
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
                    For Each r As Row In SC_DeptQuerySettingRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update SC_DeptQuerySetting Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where OrganID = @PKOrganID")
                        strSQL.AppendLine("And UserID = @PKUserID")
                        strSQL.AppendLine("And GroupID = @PKGroupID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                        If r.UserID.Updated Then db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                        If r.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                        If r.QueryOrganID.Updated Then db.AddInParameter(dbcmd, "@QueryOrganID", DbType.String, r.QueryOrganID.Value)
                        db.AddInParameter(dbcmd, "@PKOrganID", DbType.String, IIf(r.LoadFromDataRow, r.OrganID.OldValue, r.OrganID.Value))
                        db.AddInParameter(dbcmd, "@PKUserID", DbType.String, IIf(r.LoadFromDataRow, r.UserID.OldValue, r.UserID.Value))
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

        Public Function Update(ByVal SC_DeptQuerySettingRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In SC_DeptQuerySettingRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update SC_DeptQuerySetting Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where OrganID = @PKOrganID")
                strSQL.AppendLine("And UserID = @PKUserID")
                strSQL.AppendLine("And GroupID = @PKGroupID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                If r.UserID.Updated Then db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                If r.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                If r.QueryOrganID.Updated Then db.AddInParameter(dbcmd, "@QueryOrganID", DbType.String, r.QueryOrganID.Value)
                db.AddInParameter(dbcmd, "@PKOrganID", DbType.String, IIf(r.LoadFromDataRow, r.OrganID.OldValue, r.OrganID.Value))
                db.AddInParameter(dbcmd, "@PKUserID", DbType.String, IIf(r.LoadFromDataRow, r.UserID.OldValue, r.UserID.Value))
                db.AddInParameter(dbcmd, "@PKGroupID", DbType.String, IIf(r.LoadFromDataRow, r.GroupID.OldValue, r.GroupID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal SC_DeptQuerySettingRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_DeptQuerySetting")
            strSQL.AppendLine("Where OrganID = @OrganID")
            strSQL.AppendLine("And UserID = @UserID")
            strSQL.AppendLine("And GroupID = @GroupID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, SC_DeptQuerySettingRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_DeptQuerySettingRow.UserID.Value)
            db.AddInParameter(dbcmd, "@GroupID", DbType.String, SC_DeptQuerySettingRow.GroupID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal SC_DeptQuerySettingRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_DeptQuerySetting")
            strSQL.AppendLine("Where OrganID = @OrganID")
            strSQL.AppendLine("And UserID = @UserID")
            strSQL.AppendLine("And GroupID = @GroupID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, SC_DeptQuerySettingRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_DeptQuerySettingRow.UserID.Value)
            db.AddInParameter(dbcmd, "@GroupID", DbType.String, SC_DeptQuerySettingRow.GroupID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_DeptQuerySetting")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal SC_DeptQuerySettingRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_DeptQuerySetting")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    OrganID, UserID, GroupID, QueryOrganID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @OrganID, @UserID, @GroupID, @QueryOrganID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, SC_DeptQuerySettingRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_DeptQuerySettingRow.UserID.Value)
            db.AddInParameter(dbcmd, "@GroupID", DbType.String, SC_DeptQuerySettingRow.GroupID.Value)
            db.AddInParameter(dbcmd, "@QueryOrganID", DbType.String, SC_DeptQuerySettingRow.QueryOrganID.Value)

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal SC_DeptQuerySettingRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_DeptQuerySetting")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    OrganID, UserID, GroupID, QueryOrganID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @OrganID, @UserID, @GroupID, @QueryOrganID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, SC_DeptQuerySettingRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_DeptQuerySettingRow.UserID.Value)
            db.AddInParameter(dbcmd, "@GroupID", DbType.String, SC_DeptQuerySettingRow.GroupID.Value)
            db.AddInParameter(dbcmd, "@QueryOrganID", DbType.String, SC_DeptQuerySettingRow.QueryOrganID.Value)

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal SC_DeptQuerySettingRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_DeptQuerySetting")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    OrganID, UserID, GroupID, QueryOrganID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @OrganID, @UserID, @GroupID, @QueryOrganID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_DeptQuerySettingRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                        db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                        db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                        db.AddInParameter(dbcmd, "@QueryOrganID", DbType.String, r.QueryOrganID.Value)

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

        Public Function Insert(ByVal SC_DeptQuerySettingRow As Row(), ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_DeptQuerySetting")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    OrganID, UserID, GroupID, QueryOrganID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @OrganID, @UserID, @GroupID, @QueryOrganID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_DeptQuerySettingRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                db.AddInParameter(dbcmd, "@QueryOrganID", DbType.String, r.QueryOrganID.Value)

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

