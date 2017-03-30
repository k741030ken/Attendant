'****************************************************************
' Table:SC_Region
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

Namespace beSC_Region
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "RegionID", "RegionName", "Boss", "UpRegionID", "CreateDate", "LastChgID", "LastChgDate", "UpBoss" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(String), GetType(Date), GetType(String) }
        Private m_PrimaryFields As String() = { "RegionID" }

        Public ReadOnly Property Rows() As beSC_Region.Rows 
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
        Public Sub Transfer2Row(SC_RegionTable As DataTable)
            For Each dr As DataRow In SC_RegionTable.Rows
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

                dr(m_Rows(i).RegionID.FieldName) = m_Rows(i).RegionID.Value
                dr(m_Rows(i).RegionName.FieldName) = m_Rows(i).RegionName.Value
                dr(m_Rows(i).Boss.FieldName) = m_Rows(i).Boss.Value
                dr(m_Rows(i).UpRegionID.FieldName) = m_Rows(i).UpRegionID.Value
                dr(m_Rows(i).CreateDate.FieldName) = m_Rows(i).CreateDate.Value
                dr(m_Rows(i).LastChgID.FieldName) = m_Rows(i).LastChgID.Value
                dr(m_Rows(i).LastChgDate.FieldName) = m_Rows(i).LastChgDate.Value
                dr(m_Rows(i).UpBoss.FieldName) = m_Rows(i).UpBoss.Value

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

        Public Sub Add(SC_RegionRow As Row)
            m_Rows.Add(SC_RegionRow)
        End Sub

        Public Sub Remove(SC_RegionRow As Row)
            If m_Rows.IndexOf(SC_RegionRow) >= 0 Then
                m_Rows.Remove(SC_RegionRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_RegionID As Field(Of String) = new Field(Of String)("RegionID", true)
        Private FI_RegionName As Field(Of String) = new Field(Of String)("RegionName", true)
        Private FI_Boss As Field(Of String) = new Field(Of String)("Boss", true)
        Private FI_UpRegionID As Field(Of String) = new Field(Of String)("UpRegionID", true)
        Private FI_CreateDate As Field(Of Date) = new Field(Of Date)("CreateDate", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private FI_UpBoss As Field(Of String) = new Field(Of String)("UpBoss", true)
        Private m_FieldNames As String() = { "RegionID", "RegionName", "Boss", "UpRegionID", "CreateDate", "LastChgID", "LastChgDate", "UpBoss" }
        Private m_PrimaryFields As String() = { "RegionID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "RegionID"
                    Return FI_RegionID.Value
                Case "RegionName"
                    Return FI_RegionName.Value
                Case "Boss"
                    Return FI_Boss.Value
                Case "UpRegionID"
                    Return FI_UpRegionID.Value
                Case "CreateDate"
                    Return FI_CreateDate.Value
                Case "LastChgID"
                    Return FI_LastChgID.Value
                Case "LastChgDate"
                    Return FI_LastChgDate.Value
                Case "UpBoss"
                    Return FI_UpBoss.Value
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Sub SetFieldValue(ByVal fieldName As String, ByVal value As Object)
            Select Case fieldName
                Case "RegionID"
                    FI_RegionID.SetValue(value)
                Case "RegionName"
                    FI_RegionName.SetValue(value)
                Case "Boss"
                    FI_Boss.SetValue(value)
                Case "UpRegionID"
                    FI_UpRegionID.SetValue(value)
                Case "CreateDate"
                    FI_CreateDate.SetValue(value)
                Case "LastChgID"
                    FI_LastChgID.SetValue(value)
                Case "LastChgDate"
                    FI_LastChgDate.SetValue(value)
                Case "UpBoss"
                    FI_UpBoss.SetValue(value)
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
                Case "RegionID"
                    return FI_RegionID.Updated
                Case "RegionName"
                    return FI_RegionName.Updated
                Case "Boss"
                    return FI_Boss.Updated
                Case "UpRegionID"
                    return FI_UpRegionID.Updated
                Case "CreateDate"
                    return FI_CreateDate.Updated
                Case "LastChgID"
                    return FI_LastChgID.Updated
                Case "LastChgDate"
                    return FI_LastChgDate.Updated
                Case "UpBoss"
                    return FI_UpBoss.Updated
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public Function CreateUpdateSQL(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "RegionID"
                    return FI_RegionID.CreateUpdateSQL
                Case "RegionName"
                    return FI_RegionName.CreateUpdateSQL
                Case "Boss"
                    return FI_Boss.CreateUpdateSQL
                Case "UpRegionID"
                    return FI_UpRegionID.CreateUpdateSQL
                Case "CreateDate"
                    return FI_CreateDate.CreateUpdateSQL
                Case "LastChgID"
                    return FI_LastChgID.CreateUpdateSQL
                Case "LastChgDate"
                    return FI_LastChgDate.CreateUpdateSQL
                Case "UpBoss"
                    return FI_UpBoss.CreateUpdateSQL
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
            FI_RegionID.SetInitValue("")
            FI_RegionName.SetInitValue("")
            FI_Boss.SetInitValue("")
            FI_UpRegionID.SetInitValue("")
            FI_CreateDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_UpBoss.SetInitValue("")
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_RegionID.SetInitValue(dr("RegionID"))
            FI_RegionName.SetInitValue(dr("RegionName"))
            FI_Boss.SetInitValue(dr("Boss"))
            FI_UpRegionID.SetInitValue(dr("UpRegionID"))
            FI_CreateDate.SetInitValue(dr("CreateDate"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))
            FI_UpBoss.SetInitValue(dr("UpBoss"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_RegionID.Updated = False
            FI_RegionName.Updated = False
            FI_Boss.Updated = False
            FI_UpRegionID.Updated = False
            FI_CreateDate.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
            FI_UpBoss.Updated = False
        End Sub

        Public ReadOnly Property RegionID As Field(Of String) 
            Get
                Return FI_RegionID
            End Get
        End Property

        Public ReadOnly Property RegionName As Field(Of String) 
            Get
                Return FI_RegionName
            End Get
        End Property

        Public ReadOnly Property Boss As Field(Of String) 
            Get
                Return FI_Boss
            End Get
        End Property

        Public ReadOnly Property UpRegionID As Field(Of String) 
            Get
                Return FI_UpRegionID
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

        Public ReadOnly Property UpBoss As Field(Of String) 
            Get
                Return FI_UpBoss
            End Get
        End Property

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal SC_RegionRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_Region")
            strSQL.AppendLine("Where RegionID = @RegionID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@RegionID", DbType.String, SC_RegionRow.RegionID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal SC_RegionRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_Region")
            strSQL.AppendLine("Where RegionID = @RegionID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@RegionID", DbType.String, SC_RegionRow.RegionID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal SC_RegionRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_Region")
            strSQL.AppendLine("Where RegionID = @RegionID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_RegionRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@RegionID", DbType.String, r.RegionID.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal SC_RegionRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_Region")
            strSQL.AppendLine("Where RegionID = @RegionID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_RegionRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@RegionID", DbType.String, r.RegionID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal SC_RegionRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_Region")
            strSQL.AppendLine("Where RegionID = @RegionID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@RegionID", DbType.String, SC_RegionRow.RegionID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(SC_RegionRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_Region")
            strSQL.AppendLine("Where RegionID = @RegionID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@RegionID", DbType.String, SC_RegionRow.RegionID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_RegionRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_Region Set")
            For i As Integer = 0 To SC_RegionRow.FieldNames.Length - 1
                If Not SC_RegionRow.IsIdentityField(SC_RegionRow.FieldNames(i)) AndAlso SC_RegionRow.IsUpdated(SC_RegionRow.FieldNames(i)) AndAlso SC_RegionRow.CreateUpdateSQL(SC_RegionRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_RegionRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where RegionID = @PKRegionID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_RegionRow.RegionID.Updated Then db.AddInParameter(dbcmd, "@RegionID", DbType.String, SC_RegionRow.RegionID.Value)
            If SC_RegionRow.RegionName.Updated Then db.AddInParameter(dbcmd, "@RegionName", DbType.String, SC_RegionRow.RegionName.Value)
            If SC_RegionRow.Boss.Updated Then db.AddInParameter(dbcmd, "@Boss", DbType.String, SC_RegionRow.Boss.Value)
            If SC_RegionRow.UpRegionID.Updated Then db.AddInParameter(dbcmd, "@UpRegionID", DbType.String, SC_RegionRow.UpRegionID.Value)
            If SC_RegionRow.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_RegionRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_RegionRow.CreateDate.Value))
            If SC_RegionRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_RegionRow.LastChgID.Value)
            If SC_RegionRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_RegionRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_RegionRow.LastChgDate.Value))
            If SC_RegionRow.UpBoss.Updated Then db.AddInParameter(dbcmd, "@UpBoss", DbType.String, SC_RegionRow.UpBoss.Value)
            db.AddInParameter(dbcmd, "@PKRegionID", DbType.String, IIf(SC_RegionRow.LoadFromDataRow, SC_RegionRow.RegionID.OldValue, SC_RegionRow.RegionID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal SC_RegionRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_Region Set")
            For i As Integer = 0 To SC_RegionRow.FieldNames.Length - 1
                If Not SC_RegionRow.IsIdentityField(SC_RegionRow.FieldNames(i)) AndAlso SC_RegionRow.IsUpdated(SC_RegionRow.FieldNames(i)) AndAlso SC_RegionRow.CreateUpdateSQL(SC_RegionRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_RegionRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where RegionID = @PKRegionID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_RegionRow.RegionID.Updated Then db.AddInParameter(dbcmd, "@RegionID", DbType.String, SC_RegionRow.RegionID.Value)
            If SC_RegionRow.RegionName.Updated Then db.AddInParameter(dbcmd, "@RegionName", DbType.String, SC_RegionRow.RegionName.Value)
            If SC_RegionRow.Boss.Updated Then db.AddInParameter(dbcmd, "@Boss", DbType.String, SC_RegionRow.Boss.Value)
            If SC_RegionRow.UpRegionID.Updated Then db.AddInParameter(dbcmd, "@UpRegionID", DbType.String, SC_RegionRow.UpRegionID.Value)
            If SC_RegionRow.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_RegionRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_RegionRow.CreateDate.Value))
            If SC_RegionRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_RegionRow.LastChgID.Value)
            If SC_RegionRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_RegionRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_RegionRow.LastChgDate.Value))
            If SC_RegionRow.UpBoss.Updated Then db.AddInParameter(dbcmd, "@UpBoss", DbType.String, SC_RegionRow.UpBoss.Value)
            db.AddInParameter(dbcmd, "@PKRegionID", DbType.String, IIf(SC_RegionRow.LoadFromDataRow, SC_RegionRow.RegionID.OldValue, SC_RegionRow.RegionID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_RegionRow As Row()) As Integer
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
                    For Each r As Row In SC_RegionRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update SC_Region Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where RegionID = @PKRegionID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.RegionID.Updated Then db.AddInParameter(dbcmd, "@RegionID", DbType.String, r.RegionID.Value)
                        If r.RegionName.Updated Then db.AddInParameter(dbcmd, "@RegionName", DbType.String, r.RegionName.Value)
                        If r.Boss.Updated Then db.AddInParameter(dbcmd, "@Boss", DbType.String, r.Boss.Value)
                        If r.UpRegionID.Updated Then db.AddInParameter(dbcmd, "@UpRegionID", DbType.String, r.UpRegionID.Value)
                        If r.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        If r.UpBoss.Updated Then db.AddInParameter(dbcmd, "@UpBoss", DbType.String, r.UpBoss.Value)
                        db.AddInParameter(dbcmd, "@PKRegionID", DbType.String, IIf(r.LoadFromDataRow, r.RegionID.OldValue, r.RegionID.Value))

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

        Public Function Update(ByVal SC_RegionRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In SC_RegionRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update SC_Region Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where RegionID = @PKRegionID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.RegionID.Updated Then db.AddInParameter(dbcmd, "@RegionID", DbType.String, r.RegionID.Value)
                If r.RegionName.Updated Then db.AddInParameter(dbcmd, "@RegionName", DbType.String, r.RegionName.Value)
                If r.Boss.Updated Then db.AddInParameter(dbcmd, "@Boss", DbType.String, r.Boss.Value)
                If r.UpRegionID.Updated Then db.AddInParameter(dbcmd, "@UpRegionID", DbType.String, r.UpRegionID.Value)
                If r.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                If r.UpBoss.Updated Then db.AddInParameter(dbcmd, "@UpBoss", DbType.String, r.UpBoss.Value)
                db.AddInParameter(dbcmd, "@PKRegionID", DbType.String, IIf(r.LoadFromDataRow, r.RegionID.OldValue, r.RegionID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal SC_RegionRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_Region")
            strSQL.AppendLine("Where RegionID = @RegionID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@RegionID", DbType.String, SC_RegionRow.RegionID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal SC_RegionRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_Region")
            strSQL.AppendLine("Where RegionID = @RegionID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@RegionID", DbType.String, SC_RegionRow.RegionID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_Region")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal SC_RegionRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_Region")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    RegionID, RegionName, Boss, UpRegionID, CreateDate, LastChgID, LastChgDate, UpBoss")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @RegionID, @RegionName, @Boss, @UpRegionID, @CreateDate, @LastChgID, @LastChgDate, @UpBoss")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@RegionID", DbType.String, SC_RegionRow.RegionID.Value)
            db.AddInParameter(dbcmd, "@RegionName", DbType.String, SC_RegionRow.RegionName.Value)
            db.AddInParameter(dbcmd, "@Boss", DbType.String, SC_RegionRow.Boss.Value)
            db.AddInParameter(dbcmd, "@UpRegionID", DbType.String, SC_RegionRow.UpRegionID.Value)
            db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_RegionRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_RegionRow.CreateDate.Value))
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_RegionRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_RegionRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_RegionRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@UpBoss", DbType.String, SC_RegionRow.UpBoss.Value)

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal SC_RegionRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_Region")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    RegionID, RegionName, Boss, UpRegionID, CreateDate, LastChgID, LastChgDate, UpBoss")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @RegionID, @RegionName, @Boss, @UpRegionID, @CreateDate, @LastChgID, @LastChgDate, @UpBoss")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@RegionID", DbType.String, SC_RegionRow.RegionID.Value)
            db.AddInParameter(dbcmd, "@RegionName", DbType.String, SC_RegionRow.RegionName.Value)
            db.AddInParameter(dbcmd, "@Boss", DbType.String, SC_RegionRow.Boss.Value)
            db.AddInParameter(dbcmd, "@UpRegionID", DbType.String, SC_RegionRow.UpRegionID.Value)
            db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_RegionRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_RegionRow.CreateDate.Value))
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_RegionRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_RegionRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_RegionRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@UpBoss", DbType.String, SC_RegionRow.UpBoss.Value)

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal SC_RegionRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_Region")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    RegionID, RegionName, Boss, UpRegionID, CreateDate, LastChgID, LastChgDate, UpBoss")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @RegionID, @RegionName, @Boss, @UpRegionID, @CreateDate, @LastChgID, @LastChgDate, @UpBoss")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_RegionRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@RegionID", DbType.String, r.RegionID.Value)
                        db.AddInParameter(dbcmd, "@RegionName", DbType.String, r.RegionName.Value)
                        db.AddInParameter(dbcmd, "@Boss", DbType.String, r.Boss.Value)
                        db.AddInParameter(dbcmd, "@UpRegionID", DbType.String, r.UpRegionID.Value)
                        db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                        db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@UpBoss", DbType.String, r.UpBoss.Value)

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

        Public Function Insert(ByVal SC_RegionRow As Row(), ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_Region")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    RegionID, RegionName, Boss, UpRegionID, CreateDate, LastChgID, LastChgDate, UpBoss")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @RegionID, @RegionName, @Boss, @UpRegionID, @CreateDate, @LastChgID, @LastChgDate, @UpBoss")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_RegionRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@RegionID", DbType.String, r.RegionID.Value)
                db.AddInParameter(dbcmd, "@RegionName", DbType.String, r.RegionName.Value)
                db.AddInParameter(dbcmd, "@Boss", DbType.String, r.Boss.Value)
                db.AddInParameter(dbcmd, "@UpRegionID", DbType.String, r.UpRegionID.Value)
                db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@UpBoss", DbType.String, r.UpBoss.Value)

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

