'****************************************************************
' Table:SC_Agency
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

Namespace beSC_Agency
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "UserID", "AgentUserComp", "AgentUserID", "AgencyType", "ValidFrom", "ValidTo", "ValidFlag", "CreateDate", "LastChgID" _
                                    , "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(Date), GetType(String), GetType(Date), GetType(String) _
                                    , GetType(Date) }
        Private m_PrimaryFields As String() = { "UserID" }

        Public ReadOnly Property Rows() As beSC_Agency.Rows 
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
        Public Sub Transfer2Row(SC_AgencyTable As DataTable)
            For Each dr As DataRow In SC_AgencyTable.Rows
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
                dr(m_Rows(i).AgentUserComp.FieldName) = m_Rows(i).AgentUserComp.Value
                dr(m_Rows(i).AgentUserID.FieldName) = m_Rows(i).AgentUserID.Value
                dr(m_Rows(i).AgencyType.FieldName) = m_Rows(i).AgencyType.Value
                dr(m_Rows(i).ValidFrom.FieldName) = m_Rows(i).ValidFrom.Value
                dr(m_Rows(i).ValidTo.FieldName) = m_Rows(i).ValidTo.Value
                dr(m_Rows(i).ValidFlag.FieldName) = m_Rows(i).ValidFlag.Value
                dr(m_Rows(i).CreateDate.FieldName) = m_Rows(i).CreateDate.Value
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

        Public Sub Add(SC_AgencyRow As Row)
            m_Rows.Add(SC_AgencyRow)
        End Sub

        Public Sub Remove(SC_AgencyRow As Row)
            If m_Rows.IndexOf(SC_AgencyRow) >= 0 Then
                m_Rows.Remove(SC_AgencyRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_UserID As Field(Of String) = new Field(Of String)("UserID", true)
        Private FI_AgentUserComp As Field(Of String) = new Field(Of String)("AgentUserComp", true)
        Private FI_AgentUserID As Field(Of String) = new Field(Of String)("AgentUserID", true)
        Private FI_AgencyType As Field(Of String) = new Field(Of String)("AgencyType", true)
        Private FI_ValidFrom As Field(Of Date) = new Field(Of Date)("ValidFrom", true)
        Private FI_ValidTo As Field(Of Date) = new Field(Of Date)("ValidTo", true)
        Private FI_ValidFlag As Field(Of String) = new Field(Of String)("ValidFlag", true)
        Private FI_CreateDate As Field(Of Date) = new Field(Of Date)("CreateDate", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "CompID", "UserID", "AgentUserComp", "AgentUserID", "AgencyType", "ValidFrom", "ValidTo", "ValidFlag", "CreateDate", "LastChgID" _
                                    , "LastChgDate" }
        Private m_PrimaryFields As String() = { "UserID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CompID"
                    Return FI_CompID.Value
                Case "UserID"
                    Return FI_UserID.Value
                Case "AgentUserComp"
                    Return FI_AgentUserComp.Value
                Case "AgentUserID"
                    Return FI_AgentUserID.Value
                Case "AgencyType"
                    Return FI_AgencyType.Value
                Case "ValidFrom"
                    Return FI_ValidFrom.Value
                Case "ValidTo"
                    Return FI_ValidTo.Value
                Case "ValidFlag"
                    Return FI_ValidFlag.Value
                Case "CreateDate"
                    Return FI_CreateDate.Value
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
                Case "AgentUserComp"
                    FI_AgentUserComp.SetValue(value)
                Case "AgentUserID"
                    FI_AgentUserID.SetValue(value)
                Case "AgencyType"
                    FI_AgencyType.SetValue(value)
                Case "ValidFrom"
                    FI_ValidFrom.SetValue(value)
                Case "ValidTo"
                    FI_ValidTo.SetValue(value)
                Case "ValidFlag"
                    FI_ValidFlag.SetValue(value)
                Case "CreateDate"
                    FI_CreateDate.SetValue(value)
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
                Case "AgentUserComp"
                    return FI_AgentUserComp.Updated
                Case "AgentUserID"
                    return FI_AgentUserID.Updated
                Case "AgencyType"
                    return FI_AgencyType.Updated
                Case "ValidFrom"
                    return FI_ValidFrom.Updated
                Case "ValidTo"
                    return FI_ValidTo.Updated
                Case "ValidFlag"
                    return FI_ValidFlag.Updated
                Case "CreateDate"
                    return FI_CreateDate.Updated
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
                Case "AgentUserComp"
                    return FI_AgentUserComp.CreateUpdateSQL
                Case "AgentUserID"
                    return FI_AgentUserID.CreateUpdateSQL
                Case "AgencyType"
                    return FI_AgencyType.CreateUpdateSQL
                Case "ValidFrom"
                    return FI_ValidFrom.CreateUpdateSQL
                Case "ValidTo"
                    return FI_ValidTo.CreateUpdateSQL
                Case "ValidFlag"
                    return FI_ValidFlag.CreateUpdateSQL
                Case "CreateDate"
                    return FI_CreateDate.CreateUpdateSQL
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
            FI_AgentUserComp.SetInitValue("")
            FI_AgentUserID.SetInitValue("")
            FI_AgencyType.SetInitValue("")
            FI_ValidFrom.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_ValidTo.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_ValidFlag.SetInitValue("1")
            FI_CreateDate.SetInitValue(DateTime.Now)
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(DateTime.Now)
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CompID.SetInitValue(dr("CompID"))
            FI_UserID.SetInitValue(dr("UserID"))
            FI_AgentUserComp.SetInitValue(dr("AgentUserComp"))
            FI_AgentUserID.SetInitValue(dr("AgentUserID"))
            FI_AgencyType.SetInitValue(dr("AgencyType"))
            FI_ValidFrom.SetInitValue(dr("ValidFrom"))
            FI_ValidTo.SetInitValue(dr("ValidTo"))
            FI_ValidFlag.SetInitValue(dr("ValidFlag"))
            FI_CreateDate.SetInitValue(dr("CreateDate"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CompID.Updated = False
            FI_UserID.Updated = False
            FI_AgentUserComp.Updated = False
            FI_AgentUserID.Updated = False
            FI_AgencyType.Updated = False
            FI_ValidFrom.Updated = False
            FI_ValidTo.Updated = False
            FI_ValidFlag.Updated = False
            FI_CreateDate.Updated = False
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

        Public ReadOnly Property AgentUserComp As Field(Of String) 
            Get
                Return FI_AgentUserComp
            End Get
        End Property

        Public ReadOnly Property AgentUserID As Field(Of String) 
            Get
                Return FI_AgentUserID
            End Get
        End Property

        Public ReadOnly Property AgencyType As Field(Of String) 
            Get
                Return FI_AgencyType
            End Get
        End Property

        Public ReadOnly Property ValidFrom As Field(Of Date) 
            Get
                Return FI_ValidFrom
            End Get
        End Property

        Public ReadOnly Property ValidTo As Field(Of Date) 
            Get
                Return FI_ValidTo
            End Get
        End Property

        Public ReadOnly Property ValidFlag As Field(Of String) 
            Get
                Return FI_ValidFlag
            End Get
        End Property

        Public ReadOnly Property CreateDate As Field(Of Date) 
            Get
                Return FI_CreateDate
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
        Public Function DeleteRowByPrimaryKey(ByVal SC_AgencyRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_Agency")
            strSQL.AppendLine("Where UserID = @UserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_AgencyRow.UserID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal SC_AgencyRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_Agency")
            strSQL.AppendLine("Where UserID = @UserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_AgencyRow.UserID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal SC_AgencyRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_Agency")
            strSQL.AppendLine("Where UserID = @UserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_AgencyRow
                        dbcmd.Parameters.Clear()
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

        Public Function DeleteRowByPrimaryKey(ByVal SC_AgencyRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_Agency")
            strSQL.AppendLine("Where UserID = @UserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_AgencyRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal SC_AgencyRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_Agency")
            strSQL.AppendLine("Where UserID = @UserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_AgencyRow.UserID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(SC_AgencyRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_Agency")
            strSQL.AppendLine("Where UserID = @UserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_AgencyRow.UserID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_AgencyRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_Agency Set")
            For i As Integer = 0 To SC_AgencyRow.FieldNames.Length - 1
                If Not SC_AgencyRow.IsIdentityField(SC_AgencyRow.FieldNames(i)) AndAlso SC_AgencyRow.IsUpdated(SC_AgencyRow.FieldNames(i)) AndAlso SC_AgencyRow.CreateUpdateSQL(SC_AgencyRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_AgencyRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where UserID = @PKUserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_AgencyRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, SC_AgencyRow.CompID.Value)
            If SC_AgencyRow.UserID.Updated Then db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_AgencyRow.UserID.Value)
            If SC_AgencyRow.AgentUserComp.Updated Then db.AddInParameter(dbcmd, "@AgentUserComp", DbType.String, SC_AgencyRow.AgentUserComp.Value)
            If SC_AgencyRow.AgentUserID.Updated Then db.AddInParameter(dbcmd, "@AgentUserID", DbType.String, SC_AgencyRow.AgentUserID.Value)
            If SC_AgencyRow.AgencyType.Updated Then db.AddInParameter(dbcmd, "@AgencyType", DbType.String, SC_AgencyRow.AgencyType.Value)
            If SC_AgencyRow.ValidFrom.Updated Then db.AddInParameter(dbcmd, "@ValidFrom", DbType.Date, IIf(IsDateTimeNull(SC_AgencyRow.ValidFrom.Value), Convert.ToDateTime("1900/1/1"), SC_AgencyRow.ValidFrom.Value))
            If SC_AgencyRow.ValidTo.Updated Then db.AddInParameter(dbcmd, "@ValidTo", DbType.Date, IIf(IsDateTimeNull(SC_AgencyRow.ValidTo.Value), Convert.ToDateTime("1900/1/1"), SC_AgencyRow.ValidTo.Value))
            If SC_AgencyRow.ValidFlag.Updated Then db.AddInParameter(dbcmd, "@ValidFlag", DbType.String, SC_AgencyRow.ValidFlag.Value)
            If SC_AgencyRow.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_AgencyRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_AgencyRow.CreateDate.Value))
            If SC_AgencyRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_AgencyRow.LastChgID.Value)
            If SC_AgencyRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_AgencyRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_AgencyRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKUserID", DbType.String, IIf(SC_AgencyRow.LoadFromDataRow, SC_AgencyRow.UserID.OldValue, SC_AgencyRow.UserID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal SC_AgencyRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_Agency Set")
            For i As Integer = 0 To SC_AgencyRow.FieldNames.Length - 1
                If Not SC_AgencyRow.IsIdentityField(SC_AgencyRow.FieldNames(i)) AndAlso SC_AgencyRow.IsUpdated(SC_AgencyRow.FieldNames(i)) AndAlso SC_AgencyRow.CreateUpdateSQL(SC_AgencyRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_AgencyRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where UserID = @PKUserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_AgencyRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, SC_AgencyRow.CompID.Value)
            If SC_AgencyRow.UserID.Updated Then db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_AgencyRow.UserID.Value)
            If SC_AgencyRow.AgentUserComp.Updated Then db.AddInParameter(dbcmd, "@AgentUserComp", DbType.String, SC_AgencyRow.AgentUserComp.Value)
            If SC_AgencyRow.AgentUserID.Updated Then db.AddInParameter(dbcmd, "@AgentUserID", DbType.String, SC_AgencyRow.AgentUserID.Value)
            If SC_AgencyRow.AgencyType.Updated Then db.AddInParameter(dbcmd, "@AgencyType", DbType.String, SC_AgencyRow.AgencyType.Value)
            If SC_AgencyRow.ValidFrom.Updated Then db.AddInParameter(dbcmd, "@ValidFrom", DbType.Date, IIf(IsDateTimeNull(SC_AgencyRow.ValidFrom.Value), Convert.ToDateTime("1900/1/1"), SC_AgencyRow.ValidFrom.Value))
            If SC_AgencyRow.ValidTo.Updated Then db.AddInParameter(dbcmd, "@ValidTo", DbType.Date, IIf(IsDateTimeNull(SC_AgencyRow.ValidTo.Value), Convert.ToDateTime("1900/1/1"), SC_AgencyRow.ValidTo.Value))
            If SC_AgencyRow.ValidFlag.Updated Then db.AddInParameter(dbcmd, "@ValidFlag", DbType.String, SC_AgencyRow.ValidFlag.Value)
            If SC_AgencyRow.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_AgencyRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_AgencyRow.CreateDate.Value))
            If SC_AgencyRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_AgencyRow.LastChgID.Value)
            If SC_AgencyRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_AgencyRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_AgencyRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKUserID", DbType.String, IIf(SC_AgencyRow.LoadFromDataRow, SC_AgencyRow.UserID.OldValue, SC_AgencyRow.UserID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_AgencyRow As Row()) As Integer
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
                    For Each r As Row In SC_AgencyRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update SC_Agency Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where UserID = @PKUserID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.UserID.Updated Then db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                        If r.AgentUserComp.Updated Then db.AddInParameter(dbcmd, "@AgentUserComp", DbType.String, r.AgentUserComp.Value)
                        If r.AgentUserID.Updated Then db.AddInParameter(dbcmd, "@AgentUserID", DbType.String, r.AgentUserID.Value)
                        If r.AgencyType.Updated Then db.AddInParameter(dbcmd, "@AgencyType", DbType.String, r.AgencyType.Value)
                        If r.ValidFrom.Updated Then db.AddInParameter(dbcmd, "@ValidFrom", DbType.Date, IIf(IsDateTimeNull(r.ValidFrom.Value), Convert.ToDateTime("1900/1/1"), r.ValidFrom.Value))
                        If r.ValidTo.Updated Then db.AddInParameter(dbcmd, "@ValidTo", DbType.Date, IIf(IsDateTimeNull(r.ValidTo.Value), Convert.ToDateTime("1900/1/1"), r.ValidTo.Value))
                        If r.ValidFlag.Updated Then db.AddInParameter(dbcmd, "@ValidFlag", DbType.String, r.ValidFlag.Value)
                        If r.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
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

        Public Function Update(ByVal SC_AgencyRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In SC_AgencyRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update SC_Agency Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where UserID = @PKUserID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.UserID.Updated Then db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                If r.AgentUserComp.Updated Then db.AddInParameter(dbcmd, "@AgentUserComp", DbType.String, r.AgentUserComp.Value)
                If r.AgentUserID.Updated Then db.AddInParameter(dbcmd, "@AgentUserID", DbType.String, r.AgentUserID.Value)
                If r.AgencyType.Updated Then db.AddInParameter(dbcmd, "@AgencyType", DbType.String, r.AgencyType.Value)
                If r.ValidFrom.Updated Then db.AddInParameter(dbcmd, "@ValidFrom", DbType.Date, IIf(IsDateTimeNull(r.ValidFrom.Value), Convert.ToDateTime("1900/1/1"), r.ValidFrom.Value))
                If r.ValidTo.Updated Then db.AddInParameter(dbcmd, "@ValidTo", DbType.Date, IIf(IsDateTimeNull(r.ValidTo.Value), Convert.ToDateTime("1900/1/1"), r.ValidTo.Value))
                If r.ValidFlag.Updated Then db.AddInParameter(dbcmd, "@ValidFlag", DbType.String, r.ValidFlag.Value)
                If r.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKUserID", DbType.String, IIf(r.LoadFromDataRow, r.UserID.OldValue, r.UserID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal SC_AgencyRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_Agency")
            strSQL.AppendLine("Where UserID = @UserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_AgencyRow.UserID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal SC_AgencyRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_Agency")
            strSQL.AppendLine("Where UserID = @UserID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_AgencyRow.UserID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_Agency")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal SC_AgencyRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_Agency")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, UserID, AgentUserComp, AgentUserID, AgencyType, ValidFrom, ValidTo, ValidFlag,")
            strSQL.AppendLine("    CreateDate, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @UserID, @AgentUserComp, @AgentUserID, @AgencyType, @ValidFrom, @ValidTo, @ValidFlag,")
            strSQL.AppendLine("    @CreateDate, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SC_AgencyRow.CompID.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_AgencyRow.UserID.Value)
            db.AddInParameter(dbcmd, "@AgentUserComp", DbType.String, SC_AgencyRow.AgentUserComp.Value)
            db.AddInParameter(dbcmd, "@AgentUserID", DbType.String, SC_AgencyRow.AgentUserID.Value)
            db.AddInParameter(dbcmd, "@AgencyType", DbType.String, SC_AgencyRow.AgencyType.Value)
            db.AddInParameter(dbcmd, "@ValidFrom", DbType.Date, IIf(IsDateTimeNull(SC_AgencyRow.ValidFrom.Value), Convert.ToDateTime("1900/1/1"), SC_AgencyRow.ValidFrom.Value))
            db.AddInParameter(dbcmd, "@ValidTo", DbType.Date, IIf(IsDateTimeNull(SC_AgencyRow.ValidTo.Value), Convert.ToDateTime("1900/1/1"), SC_AgencyRow.ValidTo.Value))
            db.AddInParameter(dbcmd, "@ValidFlag", DbType.String, SC_AgencyRow.ValidFlag.Value)
            db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_AgencyRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_AgencyRow.CreateDate.Value))
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_AgencyRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_AgencyRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_AgencyRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal SC_AgencyRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_Agency")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, UserID, AgentUserComp, AgentUserID, AgencyType, ValidFrom, ValidTo, ValidFlag,")
            strSQL.AppendLine("    CreateDate, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @UserID, @AgentUserComp, @AgentUserID, @AgencyType, @ValidFrom, @ValidTo, @ValidFlag,")
            strSQL.AppendLine("    @CreateDate, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SC_AgencyRow.CompID.Value)
            db.AddInParameter(dbcmd, "@UserID", DbType.String, SC_AgencyRow.UserID.Value)
            db.AddInParameter(dbcmd, "@AgentUserComp", DbType.String, SC_AgencyRow.AgentUserComp.Value)
            db.AddInParameter(dbcmd, "@AgentUserID", DbType.String, SC_AgencyRow.AgentUserID.Value)
            db.AddInParameter(dbcmd, "@AgencyType", DbType.String, SC_AgencyRow.AgencyType.Value)
            db.AddInParameter(dbcmd, "@ValidFrom", DbType.Date, IIf(IsDateTimeNull(SC_AgencyRow.ValidFrom.Value), Convert.ToDateTime("1900/1/1"), SC_AgencyRow.ValidFrom.Value))
            db.AddInParameter(dbcmd, "@ValidTo", DbType.Date, IIf(IsDateTimeNull(SC_AgencyRow.ValidTo.Value), Convert.ToDateTime("1900/1/1"), SC_AgencyRow.ValidTo.Value))
            db.AddInParameter(dbcmd, "@ValidFlag", DbType.String, SC_AgencyRow.ValidFlag.Value)
            db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_AgencyRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_AgencyRow.CreateDate.Value))
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_AgencyRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_AgencyRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_AgencyRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal SC_AgencyRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_Agency")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, UserID, AgentUserComp, AgentUserID, AgencyType, ValidFrom, ValidTo, ValidFlag,")
            strSQL.AppendLine("    CreateDate, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @UserID, @AgentUserComp, @AgentUserID, @AgencyType, @ValidFrom, @ValidTo, @ValidFlag,")
            strSQL.AppendLine("    @CreateDate, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_AgencyRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                        db.AddInParameter(dbcmd, "@AgentUserComp", DbType.String, r.AgentUserComp.Value)
                        db.AddInParameter(dbcmd, "@AgentUserID", DbType.String, r.AgentUserID.Value)
                        db.AddInParameter(dbcmd, "@AgencyType", DbType.String, r.AgencyType.Value)
                        db.AddInParameter(dbcmd, "@ValidFrom", DbType.Date, IIf(IsDateTimeNull(r.ValidFrom.Value), Convert.ToDateTime("1900/1/1"), r.ValidFrom.Value))
                        db.AddInParameter(dbcmd, "@ValidTo", DbType.Date, IIf(IsDateTimeNull(r.ValidTo.Value), Convert.ToDateTime("1900/1/1"), r.ValidTo.Value))
                        db.AddInParameter(dbcmd, "@ValidFlag", DbType.String, r.ValidFlag.Value)
                        db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
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

        Public Function Insert(ByVal SC_AgencyRow As Row(), ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_Agency")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, UserID, AgentUserComp, AgentUserID, AgencyType, ValidFrom, ValidTo, ValidFlag,")
            strSQL.AppendLine("    CreateDate, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @UserID, @AgentUserComp, @AgentUserID, @AgencyType, @ValidFrom, @ValidTo, @ValidFlag,")
            strSQL.AppendLine("    @CreateDate, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_AgencyRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@UserID", DbType.String, r.UserID.Value)
                db.AddInParameter(dbcmd, "@AgentUserComp", DbType.String, r.AgentUserComp.Value)
                db.AddInParameter(dbcmd, "@AgentUserID", DbType.String, r.AgentUserID.Value)
                db.AddInParameter(dbcmd, "@AgencyType", DbType.String, r.AgencyType.Value)
                db.AddInParameter(dbcmd, "@ValidFrom", DbType.Date, IIf(IsDateTimeNull(r.ValidFrom.Value), Convert.ToDateTime("1900/1/1"), r.ValidFrom.Value))
                db.AddInParameter(dbcmd, "@ValidTo", DbType.Date, IIf(IsDateTimeNull(r.ValidTo.Value), Convert.ToDateTime("1900/1/1"), r.ValidTo.Value))
                db.AddInParameter(dbcmd, "@ValidFlag", DbType.String, r.ValidFlag.Value)
                db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
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

