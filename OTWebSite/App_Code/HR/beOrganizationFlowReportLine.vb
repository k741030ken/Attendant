'****************************************************************
' Table:OrganizationFlowReportLine
' Created Date: 2016.10.03
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beOrganizationFlowReportLine
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "OrganID", "BossSeq", "BossCompID", "Boss", "ReportLineOrganID" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(Integer), GetType(String), GetType(String), GetType(String) }
        Private m_PrimaryFields As String() = { "OrganID", "BossSeq", "BossCompID", "Boss" }

        Public ReadOnly Property Rows() As beOrganizationFlowReportLine.Rows 
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
        Public Sub Transfer2Row(OrganizationFlowReportLineTable As DataTable)
            For Each dr As DataRow In OrganizationFlowReportLineTable.Rows
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
                dr(m_Rows(i).OrganID.FieldName) = m_Rows(i).OrganID.Value
                dr(m_Rows(i).BossSeq.FieldName) = m_Rows(i).BossSeq.Value
                dr(m_Rows(i).BossCompID.FieldName) = m_Rows(i).BossCompID.Value
                dr(m_Rows(i).Boss.FieldName) = m_Rows(i).Boss.Value
                dr(m_Rows(i).ReportLineOrganID.FieldName) = m_Rows(i).ReportLineOrganID.Value

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

        Public Sub Add(OrganizationFlowReportLineRow As Row)
            m_Rows.Add(OrganizationFlowReportLineRow)
        End Sub

        Public Sub Remove(OrganizationFlowReportLineRow As Row)
            If m_Rows.IndexOf(OrganizationFlowReportLineRow) >= 0 Then
                m_Rows.Remove(OrganizationFlowReportLineRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_OrganID As Field(Of String) = new Field(Of String)("OrganID", true)
        Private FI_BossSeq As Field(Of Integer) = new Field(Of Integer)("BossSeq", true)
        Private FI_BossCompID As Field(Of String) = new Field(Of String)("BossCompID", true)
        Private FI_Boss As Field(Of String) = new Field(Of String)("Boss", true)
        Private FI_ReportLineOrganID As Field(Of String) = new Field(Of String)("ReportLineOrganID", true)
        Private m_FieldNames As String() = { "CompID", "OrganID", "BossSeq", "BossCompID", "Boss", "ReportLineOrganID" }
        Private m_PrimaryFields As String() = { "OrganID", "BossSeq", "BossCompID", "Boss" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CompID"
                    Return FI_CompID.Value
                Case "OrganID"
                    Return FI_OrganID.Value
                Case "BossSeq"
                    Return FI_BossSeq.Value
                Case "BossCompID"
                    Return FI_BossCompID.Value
                Case "Boss"
                    Return FI_Boss.Value
                Case "ReportLineOrganID"
                    Return FI_ReportLineOrganID.Value
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Sub SetFieldValue(ByVal fieldName As String, ByVal value As Object)
            Select Case fieldName
                Case "CompID"
                    FI_CompID.SetValue(value)
                Case "OrganID"
                    FI_OrganID.SetValue(value)
                Case "BossSeq"
                    FI_BossSeq.SetValue(value)
                Case "BossCompID"
                    FI_BossCompID.SetValue(value)
                Case "Boss"
                    FI_Boss.SetValue(value)
                Case "ReportLineOrganID"
                    FI_ReportLineOrganID.SetValue(value)
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
                Case "OrganID"
                    return FI_OrganID.Updated
                Case "BossSeq"
                    return FI_BossSeq.Updated
                Case "BossCompID"
                    return FI_BossCompID.Updated
                Case "Boss"
                    return FI_Boss.Updated
                Case "ReportLineOrganID"
                    return FI_ReportLineOrganID.Updated
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public Function CreateUpdateSQL(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "CompID"
                    return FI_CompID.CreateUpdateSQL
                Case "OrganID"
                    return FI_OrganID.CreateUpdateSQL
                Case "BossSeq"
                    return FI_BossSeq.CreateUpdateSQL
                Case "BossCompID"
                    return FI_BossCompID.CreateUpdateSQL
                Case "Boss"
                    return FI_Boss.CreateUpdateSQL
                Case "ReportLineOrganID"
                    return FI_ReportLineOrganID.CreateUpdateSQL
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
            FI_OrganID.SetInitValue("")
            FI_BossSeq.SetInitValue(0)
            FI_BossCompID.SetInitValue("")
            FI_Boss.SetInitValue("")
            FI_ReportLineOrganID.SetInitValue("")
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CompID.SetInitValue(dr("CompID"))
            FI_OrganID.SetInitValue(dr("OrganID"))
            FI_BossSeq.SetInitValue(dr("BossSeq"))
            FI_BossCompID.SetInitValue(dr("BossCompID"))
            FI_Boss.SetInitValue(dr("Boss"))
            FI_ReportLineOrganID.SetInitValue(dr("ReportLineOrganID"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CompID.Updated = False
            FI_OrganID.Updated = False
            FI_BossSeq.Updated = False
            FI_BossCompID.Updated = False
            FI_Boss.Updated = False
            FI_ReportLineOrganID.Updated = False
        End Sub

        Public ReadOnly Property CompID As Field(Of String) 
            Get
                Return FI_CompID
            End Get
        End Property

        Public ReadOnly Property OrganID As Field(Of String) 
            Get
                Return FI_OrganID
            End Get
        End Property

        Public ReadOnly Property BossSeq As Field(Of Integer) 
            Get
                Return FI_BossSeq
            End Get
        End Property

        Public ReadOnly Property BossCompID As Field(Of String) 
            Get
                Return FI_BossCompID
            End Get
        End Property

        Public ReadOnly Property Boss As Field(Of String) 
            Get
                Return FI_Boss
            End Get
        End Property

        Public ReadOnly Property ReportLineOrganID As Field(Of String) 
            Get
                Return FI_ReportLineOrganID
            End Get
        End Property

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal OrganizationFlowReportLineRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From OrganizationFlowReportLine")
            strSQL.AppendLine("Where OrganID = @OrganID")
            strSQL.AppendLine("And BossSeq = @BossSeq")
            strSQL.AppendLine("And BossCompID = @BossCompID")
            strSQL.AppendLine("And Boss = @Boss")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationFlowReportLineRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@BossSeq", DbType.Int32, OrganizationFlowReportLineRow.BossSeq.Value)
            db.AddInParameter(dbcmd, "@BossCompID", DbType.String, OrganizationFlowReportLineRow.BossCompID.Value)
            db.AddInParameter(dbcmd, "@Boss", DbType.String, OrganizationFlowReportLineRow.Boss.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal OrganizationFlowReportLineRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From OrganizationFlowReportLine")
            strSQL.AppendLine("Where OrganID = @OrganID")
            strSQL.AppendLine("And BossSeq = @BossSeq")
            strSQL.AppendLine("And BossCompID = @BossCompID")
            strSQL.AppendLine("And Boss = @Boss")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationFlowReportLineRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@BossSeq", DbType.Int32, OrganizationFlowReportLineRow.BossSeq.Value)
            db.AddInParameter(dbcmd, "@BossCompID", DbType.String, OrganizationFlowReportLineRow.BossCompID.Value)
            db.AddInParameter(dbcmd, "@Boss", DbType.String, OrganizationFlowReportLineRow.Boss.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal OrganizationFlowReportLineRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From OrganizationFlowReportLine")
            strSQL.AppendLine("Where OrganID = @OrganID")
            strSQL.AppendLine("And BossSeq = @BossSeq")
            strSQL.AppendLine("And BossCompID = @BossCompID")
            strSQL.AppendLine("And Boss = @Boss")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In OrganizationFlowReportLineRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                        db.AddInParameter(dbcmd, "@BossSeq", DbType.Int32, r.BossSeq.Value)
                        db.AddInParameter(dbcmd, "@BossCompID", DbType.String, r.BossCompID.Value)
                        db.AddInParameter(dbcmd, "@Boss", DbType.String, r.Boss.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal OrganizationFlowReportLineRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From OrganizationFlowReportLine")
            strSQL.AppendLine("Where OrganID = @OrganID")
            strSQL.AppendLine("And BossSeq = @BossSeq")
            strSQL.AppendLine("And BossCompID = @BossCompID")
            strSQL.AppendLine("And Boss = @Boss")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In OrganizationFlowReportLineRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                db.AddInParameter(dbcmd, "@BossSeq", DbType.Int32, r.BossSeq.Value)
                db.AddInParameter(dbcmd, "@BossCompID", DbType.String, r.BossCompID.Value)
                db.AddInParameter(dbcmd, "@Boss", DbType.String, r.Boss.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal OrganizationFlowReportLineRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From OrganizationFlowReportLine")
            strSQL.AppendLine("Where OrganID = @OrganID")
            strSQL.AppendLine("And BossSeq = @BossSeq")
            strSQL.AppendLine("And BossCompID = @BossCompID")
            strSQL.AppendLine("And Boss = @Boss")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationFlowReportLineRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@BossSeq", DbType.Int32, OrganizationFlowReportLineRow.BossSeq.Value)
            db.AddInParameter(dbcmd, "@BossCompID", DbType.String, OrganizationFlowReportLineRow.BossCompID.Value)
            db.AddInParameter(dbcmd, "@Boss", DbType.String, OrganizationFlowReportLineRow.Boss.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(OrganizationFlowReportLineRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From OrganizationFlowReportLine")
            strSQL.AppendLine("Where OrganID = @OrganID")
            strSQL.AppendLine("And BossSeq = @BossSeq")
            strSQL.AppendLine("And BossCompID = @BossCompID")
            strSQL.AppendLine("And Boss = @Boss")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationFlowReportLineRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@BossSeq", DbType.Int32, OrganizationFlowReportLineRow.BossSeq.Value)
            db.AddInParameter(dbcmd, "@BossCompID", DbType.String, OrganizationFlowReportLineRow.BossCompID.Value)
            db.AddInParameter(dbcmd, "@Boss", DbType.String, OrganizationFlowReportLineRow.Boss.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal OrganizationFlowReportLineRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update OrganizationFlowReportLine Set")
            For i As Integer = 0 To OrganizationFlowReportLineRow.FieldNames.Length - 1
                If Not OrganizationFlowReportLineRow.IsIdentityField(OrganizationFlowReportLineRow.FieldNames(i)) AndAlso OrganizationFlowReportLineRow.IsUpdated(OrganizationFlowReportLineRow.FieldNames(i)) AndAlso OrganizationFlowReportLineRow.CreateUpdateSQL(OrganizationFlowReportLineRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, OrganizationFlowReportLineRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where OrganID = @PKOrganID")
            strSQL.AppendLine("And BossSeq = @PKBossSeq")
            strSQL.AppendLine("And BossCompID = @PKBossCompID")
            strSQL.AppendLine("And Boss = @PKBoss")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If OrganizationFlowReportLineRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, OrganizationFlowReportLineRow.CompID.Value)
            If OrganizationFlowReportLineRow.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationFlowReportLineRow.OrganID.Value)
            If OrganizationFlowReportLineRow.BossSeq.Updated Then db.AddInParameter(dbcmd, "@BossSeq", DbType.Int32, OrganizationFlowReportLineRow.BossSeq.Value)
            If OrganizationFlowReportLineRow.BossCompID.Updated Then db.AddInParameter(dbcmd, "@BossCompID", DbType.String, OrganizationFlowReportLineRow.BossCompID.Value)
            If OrganizationFlowReportLineRow.Boss.Updated Then db.AddInParameter(dbcmd, "@Boss", DbType.String, OrganizationFlowReportLineRow.Boss.Value)
            If OrganizationFlowReportLineRow.ReportLineOrganID.Updated Then db.AddInParameter(dbcmd, "@ReportLineOrganID", DbType.String, OrganizationFlowReportLineRow.ReportLineOrganID.Value)
            db.AddInParameter(dbcmd, "@PKOrganID", DbType.String, IIf(OrganizationFlowReportLineRow.LoadFromDataRow, OrganizationFlowReportLineRow.OrganID.OldValue, OrganizationFlowReportLineRow.OrganID.Value))
            db.AddInParameter(dbcmd, "@PKBossSeq", DbType.Int32, IIf(OrganizationFlowReportLineRow.LoadFromDataRow, OrganizationFlowReportLineRow.BossSeq.OldValue, OrganizationFlowReportLineRow.BossSeq.Value))
            db.AddInParameter(dbcmd, "@PKBossCompID", DbType.String, IIf(OrganizationFlowReportLineRow.LoadFromDataRow, OrganizationFlowReportLineRow.BossCompID.OldValue, OrganizationFlowReportLineRow.BossCompID.Value))
            db.AddInParameter(dbcmd, "@PKBoss", DbType.String, IIf(OrganizationFlowReportLineRow.LoadFromDataRow, OrganizationFlowReportLineRow.Boss.OldValue, OrganizationFlowReportLineRow.Boss.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal OrganizationFlowReportLineRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update OrganizationFlowReportLine Set")
            For i As Integer = 0 To OrganizationFlowReportLineRow.FieldNames.Length - 1
                If Not OrganizationFlowReportLineRow.IsIdentityField(OrganizationFlowReportLineRow.FieldNames(i)) AndAlso OrganizationFlowReportLineRow.IsUpdated(OrganizationFlowReportLineRow.FieldNames(i)) AndAlso OrganizationFlowReportLineRow.CreateUpdateSQL(OrganizationFlowReportLineRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, OrganizationFlowReportLineRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where OrganID = @PKOrganID")
            strSQL.AppendLine("And BossSeq = @PKBossSeq")
            strSQL.AppendLine("And BossCompID = @PKBossCompID")
            strSQL.AppendLine("And Boss = @PKBoss")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If OrganizationFlowReportLineRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, OrganizationFlowReportLineRow.CompID.Value)
            If OrganizationFlowReportLineRow.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationFlowReportLineRow.OrganID.Value)
            If OrganizationFlowReportLineRow.BossSeq.Updated Then db.AddInParameter(dbcmd, "@BossSeq", DbType.Int32, OrganizationFlowReportLineRow.BossSeq.Value)
            If OrganizationFlowReportLineRow.BossCompID.Updated Then db.AddInParameter(dbcmd, "@BossCompID", DbType.String, OrganizationFlowReportLineRow.BossCompID.Value)
            If OrganizationFlowReportLineRow.Boss.Updated Then db.AddInParameter(dbcmd, "@Boss", DbType.String, OrganizationFlowReportLineRow.Boss.Value)
            If OrganizationFlowReportLineRow.ReportLineOrganID.Updated Then db.AddInParameter(dbcmd, "@ReportLineOrganID", DbType.String, OrganizationFlowReportLineRow.ReportLineOrganID.Value)
            db.AddInParameter(dbcmd, "@PKOrganID", DbType.String, IIf(OrganizationFlowReportLineRow.LoadFromDataRow, OrganizationFlowReportLineRow.OrganID.OldValue, OrganizationFlowReportLineRow.OrganID.Value))
            db.AddInParameter(dbcmd, "@PKBossSeq", DbType.Int32, IIf(OrganizationFlowReportLineRow.LoadFromDataRow, OrganizationFlowReportLineRow.BossSeq.OldValue, OrganizationFlowReportLineRow.BossSeq.Value))
            db.AddInParameter(dbcmd, "@PKBossCompID", DbType.String, IIf(OrganizationFlowReportLineRow.LoadFromDataRow, OrganizationFlowReportLineRow.BossCompID.OldValue, OrganizationFlowReportLineRow.BossCompID.Value))
            db.AddInParameter(dbcmd, "@PKBoss", DbType.String, IIf(OrganizationFlowReportLineRow.LoadFromDataRow, OrganizationFlowReportLineRow.Boss.OldValue, OrganizationFlowReportLineRow.Boss.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal OrganizationFlowReportLineRow As Row()) As Integer
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
                    For Each r As Row In OrganizationFlowReportLineRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update OrganizationFlowReportLine Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where OrganID = @PKOrganID")
                        strSQL.AppendLine("And BossSeq = @PKBossSeq")
                        strSQL.AppendLine("And BossCompID = @PKBossCompID")
                        strSQL.AppendLine("And Boss = @PKBoss")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                        If r.BossSeq.Updated Then db.AddInParameter(dbcmd, "@BossSeq", DbType.Int32, r.BossSeq.Value)
                        If r.BossCompID.Updated Then db.AddInParameter(dbcmd, "@BossCompID", DbType.String, r.BossCompID.Value)
                        If r.Boss.Updated Then db.AddInParameter(dbcmd, "@Boss", DbType.String, r.Boss.Value)
                        If r.ReportLineOrganID.Updated Then db.AddInParameter(dbcmd, "@ReportLineOrganID", DbType.String, r.ReportLineOrganID.Value)
                        db.AddInParameter(dbcmd, "@PKOrganID", DbType.String, IIf(r.LoadFromDataRow, r.OrganID.OldValue, r.OrganID.Value))
                        db.AddInParameter(dbcmd, "@PKBossSeq", DbType.Int32, IIf(r.LoadFromDataRow, r.BossSeq.OldValue, r.BossSeq.Value))
                        db.AddInParameter(dbcmd, "@PKBossCompID", DbType.String, IIf(r.LoadFromDataRow, r.BossCompID.OldValue, r.BossCompID.Value))
                        db.AddInParameter(dbcmd, "@PKBoss", DbType.String, IIf(r.LoadFromDataRow, r.Boss.OldValue, r.Boss.Value))

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

        Public Function Update(ByVal OrganizationFlowReportLineRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In OrganizationFlowReportLineRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update OrganizationFlowReportLine Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where OrganID = @PKOrganID")
                strSQL.AppendLine("And BossSeq = @PKBossSeq")
                strSQL.AppendLine("And BossCompID = @PKBossCompID")
                strSQL.AppendLine("And Boss = @PKBoss")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                If r.BossSeq.Updated Then db.AddInParameter(dbcmd, "@BossSeq", DbType.Int32, r.BossSeq.Value)
                If r.BossCompID.Updated Then db.AddInParameter(dbcmd, "@BossCompID", DbType.String, r.BossCompID.Value)
                If r.Boss.Updated Then db.AddInParameter(dbcmd, "@Boss", DbType.String, r.Boss.Value)
                If r.ReportLineOrganID.Updated Then db.AddInParameter(dbcmd, "@ReportLineOrganID", DbType.String, r.ReportLineOrganID.Value)
                db.AddInParameter(dbcmd, "@PKOrganID", DbType.String, IIf(r.LoadFromDataRow, r.OrganID.OldValue, r.OrganID.Value))
                db.AddInParameter(dbcmd, "@PKBossSeq", DbType.Int32, IIf(r.LoadFromDataRow, r.BossSeq.OldValue, r.BossSeq.Value))
                db.AddInParameter(dbcmd, "@PKBossCompID", DbType.String, IIf(r.LoadFromDataRow, r.BossCompID.OldValue, r.BossCompID.Value))
                db.AddInParameter(dbcmd, "@PKBoss", DbType.String, IIf(r.LoadFromDataRow, r.Boss.OldValue, r.Boss.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal OrganizationFlowReportLineRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From OrganizationFlowReportLine")
            strSQL.AppendLine("Where OrganID = @OrganID")
            strSQL.AppendLine("And BossSeq = @BossSeq")
            strSQL.AppendLine("And BossCompID = @BossCompID")
            strSQL.AppendLine("And Boss = @Boss")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationFlowReportLineRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@BossSeq", DbType.Int32, OrganizationFlowReportLineRow.BossSeq.Value)
            db.AddInParameter(dbcmd, "@BossCompID", DbType.String, OrganizationFlowReportLineRow.BossCompID.Value)
            db.AddInParameter(dbcmd, "@Boss", DbType.String, OrganizationFlowReportLineRow.Boss.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal OrganizationFlowReportLineRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From OrganizationFlowReportLine")
            strSQL.AppendLine("Where OrganID = @OrganID")
            strSQL.AppendLine("And BossSeq = @BossSeq")
            strSQL.AppendLine("And BossCompID = @BossCompID")
            strSQL.AppendLine("And Boss = @Boss")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationFlowReportLineRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@BossSeq", DbType.Int32, OrganizationFlowReportLineRow.BossSeq.Value)
            db.AddInParameter(dbcmd, "@BossCompID", DbType.String, OrganizationFlowReportLineRow.BossCompID.Value)
            db.AddInParameter(dbcmd, "@Boss", DbType.String, OrganizationFlowReportLineRow.Boss.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From OrganizationFlowReportLine")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal OrganizationFlowReportLineRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into OrganizationFlowReportLine")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, OrganID, BossSeq, BossCompID, Boss, ReportLineOrganID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @OrganID, @BossSeq, @BossCompID, @Boss, @ReportLineOrganID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, OrganizationFlowReportLineRow.CompID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationFlowReportLineRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@BossSeq", DbType.Int32, OrganizationFlowReportLineRow.BossSeq.Value)
            db.AddInParameter(dbcmd, "@BossCompID", DbType.String, OrganizationFlowReportLineRow.BossCompID.Value)
            db.AddInParameter(dbcmd, "@Boss", DbType.String, OrganizationFlowReportLineRow.Boss.Value)
            db.AddInParameter(dbcmd, "@ReportLineOrganID", DbType.String, OrganizationFlowReportLineRow.ReportLineOrganID.Value)

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal OrganizationFlowReportLineRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into OrganizationFlowReportLine")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, OrganID, BossSeq, BossCompID, Boss, ReportLineOrganID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @OrganID, @BossSeq, @BossCompID, @Boss, @ReportLineOrganID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, OrganizationFlowReportLineRow.CompID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, OrganizationFlowReportLineRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@BossSeq", DbType.Int32, OrganizationFlowReportLineRow.BossSeq.Value)
            db.AddInParameter(dbcmd, "@BossCompID", DbType.String, OrganizationFlowReportLineRow.BossCompID.Value)
            db.AddInParameter(dbcmd, "@Boss", DbType.String, OrganizationFlowReportLineRow.Boss.Value)
            db.AddInParameter(dbcmd, "@ReportLineOrganID", DbType.String, OrganizationFlowReportLineRow.ReportLineOrganID.Value)

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal OrganizationFlowReportLineRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into OrganizationFlowReportLine")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, OrganID, BossSeq, BossCompID, Boss, ReportLineOrganID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @OrganID, @BossSeq, @BossCompID, @Boss, @ReportLineOrganID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In OrganizationFlowReportLineRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                        db.AddInParameter(dbcmd, "@BossSeq", DbType.Int32, r.BossSeq.Value)
                        db.AddInParameter(dbcmd, "@BossCompID", DbType.String, r.BossCompID.Value)
                        db.AddInParameter(dbcmd, "@Boss", DbType.String, r.Boss.Value)
                        db.AddInParameter(dbcmd, "@ReportLineOrganID", DbType.String, r.ReportLineOrganID.Value)

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

        Public Function Insert(ByVal OrganizationFlowReportLineRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into OrganizationFlowReportLine")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, OrganID, BossSeq, BossCompID, Boss, ReportLineOrganID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @OrganID, @BossSeq, @BossCompID, @Boss, @ReportLineOrganID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In OrganizationFlowReportLineRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                db.AddInParameter(dbcmd, "@BossSeq", DbType.Int32, r.BossSeq.Value)
                db.AddInParameter(dbcmd, "@BossCompID", DbType.String, r.BossCompID.Value)
                db.AddInParameter(dbcmd, "@Boss", DbType.String, r.Boss.Value)
                db.AddInParameter(dbcmd, "@ReportLineOrganID", DbType.String, r.ReportLineOrganID.Value)

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

