'****************************************************************
' Table:SC_Fun
' Created Date: 2015.03.12
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beSC_Fun
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "SysID", "FunID", "FunName", "FunEngName", "ParentFormID", "OrderSeq", "IsMenu", "CheckRight", "LevelNo", "Path" _
                                    , "CreateDate", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Integer), GetType(String), GetType(String), GetType(Decimal), GetType(String) _
                                    , GetType(Date), GetType(String), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "SysID", "FunID" }

        Public ReadOnly Property Rows() As beSC_Fun.Rows 
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
        Public Sub Transfer2Row(SC_FunTable As DataTable)
            For Each dr As DataRow In SC_FunTable.Rows
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

                dr(m_Rows(i).SysID.FieldName) = m_Rows(i).SysID.Value
                dr(m_Rows(i).FunID.FieldName) = m_Rows(i).FunID.Value
                dr(m_Rows(i).FunName.FieldName) = m_Rows(i).FunName.Value
                dr(m_Rows(i).FunEngName.FieldName) = m_Rows(i).FunEngName.Value
                dr(m_Rows(i).ParentFormID.FieldName) = m_Rows(i).ParentFormID.Value
                dr(m_Rows(i).OrderSeq.FieldName) = m_Rows(i).OrderSeq.Value
                dr(m_Rows(i).IsMenu.FieldName) = m_Rows(i).IsMenu.Value
                dr(m_Rows(i).CheckRight.FieldName) = m_Rows(i).CheckRight.Value
                dr(m_Rows(i).LevelNo.FieldName) = m_Rows(i).LevelNo.Value
                dr(m_Rows(i).Path.FieldName) = m_Rows(i).Path.Value
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

        Public Sub Add(SC_FunRow As Row)
            m_Rows.Add(SC_FunRow)
        End Sub

        Public Sub Remove(SC_FunRow As Row)
            If m_Rows.IndexOf(SC_FunRow) >= 0 Then
                m_Rows.Remove(SC_FunRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_SysID As Field(Of String) = new Field(Of String)("SysID", true)
        Private FI_FunID As Field(Of String) = new Field(Of String)("FunID", true)
        Private FI_FunName As Field(Of String) = new Field(Of String)("FunName", true)
        Private FI_FunEngName As Field(Of String) = new Field(Of String)("FunEngName", true)
        Private FI_ParentFormID As Field(Of String) = new Field(Of String)("ParentFormID", true)
        Private FI_OrderSeq As Field(Of Integer) = new Field(Of Integer)("OrderSeq", true)
        Private FI_IsMenu As Field(Of String) = new Field(Of String)("IsMenu", true)
        Private FI_CheckRight As Field(Of String) = new Field(Of String)("CheckRight", true)
        Private FI_LevelNo As Field(Of Decimal) = new Field(Of Decimal)("LevelNo", true)
        Private FI_Path As Field(Of String) = new Field(Of String)("Path", true)
        Private FI_CreateDate As Field(Of Date) = new Field(Of Date)("CreateDate", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "SysID", "FunID", "FunName", "FunEngName", "ParentFormID", "OrderSeq", "IsMenu", "CheckRight", "LevelNo", "Path" _
                                    , "CreateDate", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "SysID", "FunID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "SysID"
                    Return FI_SysID.Value
                Case "FunID"
                    Return FI_FunID.Value
                Case "FunName"
                    Return FI_FunName.Value
                Case "FunEngName"
                    Return FI_FunEngName.Value
                Case "ParentFormID"
                    Return FI_ParentFormID.Value
                Case "OrderSeq"
                    Return FI_OrderSeq.Value
                Case "IsMenu"
                    Return FI_IsMenu.Value
                Case "CheckRight"
                    Return FI_CheckRight.Value
                Case "LevelNo"
                    Return FI_LevelNo.Value
                Case "Path"
                    Return FI_Path.Value
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
                Case "SysID"
                    FI_SysID.SetValue(value)
                Case "FunID"
                    FI_FunID.SetValue(value)
                Case "FunName"
                    FI_FunName.SetValue(value)
                Case "FunEngName"
                    FI_FunEngName.SetValue(value)
                Case "ParentFormID"
                    FI_ParentFormID.SetValue(value)
                Case "OrderSeq"
                    FI_OrderSeq.SetValue(value)
                Case "IsMenu"
                    FI_IsMenu.SetValue(value)
                Case "CheckRight"
                    FI_CheckRight.SetValue(value)
                Case "LevelNo"
                    FI_LevelNo.SetValue(value)
                Case "Path"
                    FI_Path.SetValue(value)
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
                Case "SysID"
                    return FI_SysID.Updated
                Case "FunID"
                    return FI_FunID.Updated
                Case "FunName"
                    return FI_FunName.Updated
                Case "FunEngName"
                    return FI_FunEngName.Updated
                Case "ParentFormID"
                    return FI_ParentFormID.Updated
                Case "OrderSeq"
                    return FI_OrderSeq.Updated
                Case "IsMenu"
                    return FI_IsMenu.Updated
                Case "CheckRight"
                    return FI_CheckRight.Updated
                Case "LevelNo"
                    return FI_LevelNo.Updated
                Case "Path"
                    return FI_Path.Updated
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
                Case "SysID"
                    return FI_SysID.CreateUpdateSQL
                Case "FunID"
                    return FI_FunID.CreateUpdateSQL
                Case "FunName"
                    return FI_FunName.CreateUpdateSQL
                Case "FunEngName"
                    return FI_FunEngName.CreateUpdateSQL
                Case "ParentFormID"
                    return FI_ParentFormID.CreateUpdateSQL
                Case "OrderSeq"
                    return FI_OrderSeq.CreateUpdateSQL
                Case "IsMenu"
                    return FI_IsMenu.CreateUpdateSQL
                Case "CheckRight"
                    return FI_CheckRight.CreateUpdateSQL
                Case "LevelNo"
                    return FI_LevelNo.CreateUpdateSQL
                Case "Path"
                    return FI_Path.CreateUpdateSQL
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
            FI_SysID.SetInitValue("")
            FI_FunID.SetInitValue("")
            FI_FunName.SetInitValue("")
            FI_FunEngName.SetInitValue("")
            FI_ParentFormID.SetInitValue("")
            FI_OrderSeq.SetInitValue(0)
            FI_IsMenu.SetInitValue("")
            FI_CheckRight.SetInitValue("1")
            FI_LevelNo.SetInitValue(0)
            FI_Path.SetInitValue("")
            FI_CreateDate.SetInitValue(DateTime.Now)
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(DateTime.Now)
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_SysID.SetInitValue(dr("SysID"))
            FI_FunID.SetInitValue(dr("FunID"))
            FI_FunName.SetInitValue(dr("FunName"))
            FI_FunEngName.SetInitValue(dr("FunEngName"))
            FI_ParentFormID.SetInitValue(dr("ParentFormID"))
            FI_OrderSeq.SetInitValue(dr("OrderSeq"))
            FI_IsMenu.SetInitValue(dr("IsMenu"))
            FI_CheckRight.SetInitValue(dr("CheckRight"))
            FI_LevelNo.SetInitValue(dr("LevelNo"))
            FI_Path.SetInitValue(dr("Path"))
            FI_CreateDate.SetInitValue(dr("CreateDate"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_SysID.Updated = False
            FI_FunID.Updated = False
            FI_FunName.Updated = False
            FI_FunEngName.Updated = False
            FI_ParentFormID.Updated = False
            FI_OrderSeq.Updated = False
            FI_IsMenu.Updated = False
            FI_CheckRight.Updated = False
            FI_LevelNo.Updated = False
            FI_Path.Updated = False
            FI_CreateDate.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
        End Sub

        Public ReadOnly Property SysID As Field(Of String) 
            Get
                Return FI_SysID
            End Get
        End Property

        Public ReadOnly Property FunID As Field(Of String) 
            Get
                Return FI_FunID
            End Get
        End Property

        Public ReadOnly Property FunName As Field(Of String) 
            Get
                Return FI_FunName
            End Get
        End Property

        Public ReadOnly Property FunEngName As Field(Of String) 
            Get
                Return FI_FunEngName
            End Get
        End Property

        Public ReadOnly Property ParentFormID As Field(Of String) 
            Get
                Return FI_ParentFormID
            End Get
        End Property

        Public ReadOnly Property OrderSeq As Field(Of Integer) 
            Get
                Return FI_OrderSeq
            End Get
        End Property

        Public ReadOnly Property IsMenu As Field(Of String) 
            Get
                Return FI_IsMenu
            End Get
        End Property

        Public ReadOnly Property CheckRight As Field(Of String) 
            Get
                Return FI_CheckRight
            End Get
        End Property

        Public ReadOnly Property LevelNo As Field(Of Decimal) 
            Get
                Return FI_LevelNo
            End Get
        End Property

        Public ReadOnly Property Path As Field(Of String) 
            Get
                Return FI_Path
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
        Public Function DeleteRowByPrimaryKey(ByVal SC_FunRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_Fun")
            strSQL.AppendLine("Where SysID = @SysID")
            strSQL.AppendLine("And FunID = @FunID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@SysID", DbType.String, SC_FunRow.SysID.Value)
            db.AddInParameter(dbcmd, "@FunID", DbType.String, SC_FunRow.FunID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal SC_FunRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_Fun")
            strSQL.AppendLine("Where SysID = @SysID")
            strSQL.AppendLine("And FunID = @FunID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@SysID", DbType.String, SC_FunRow.SysID.Value)
            db.AddInParameter(dbcmd, "@FunID", DbType.String, SC_FunRow.FunID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal SC_FunRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_Fun")
            strSQL.AppendLine("Where SysID = @SysID")
            strSQL.AppendLine("And FunID = @FunID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_FunRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@SysID", DbType.String, r.SysID.Value)
                        db.AddInParameter(dbcmd, "@FunID", DbType.String, r.FunID.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal SC_FunRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_Fun")
            strSQL.AppendLine("Where SysID = @SysID")
            strSQL.AppendLine("And FunID = @FunID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_FunRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@SysID", DbType.String, r.SysID.Value)
                db.AddInParameter(dbcmd, "@FunID", DbType.String, r.FunID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal SC_FunRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_Fun")
            strSQL.AppendLine("Where SysID = @SysID")
            strSQL.AppendLine("And FunID = @FunID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@SysID", DbType.String, SC_FunRow.SysID.Value)
            db.AddInParameter(dbcmd, "@FunID", DbType.String, SC_FunRow.FunID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(SC_FunRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_Fun")
            strSQL.AppendLine("Where SysID = @SysID")
            strSQL.AppendLine("And FunID = @FunID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@SysID", DbType.String, SC_FunRow.SysID.Value)
            db.AddInParameter(dbcmd, "@FunID", DbType.String, SC_FunRow.FunID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_FunRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_Fun Set")
            For i As Integer = 0 To SC_FunRow.FieldNames.Length - 1
                If Not SC_FunRow.IsIdentityField(SC_FunRow.FieldNames(i)) AndAlso SC_FunRow.IsUpdated(SC_FunRow.FieldNames(i)) AndAlso SC_FunRow.CreateUpdateSQL(SC_FunRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_FunRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where SysID = @PKSysID")
            strSQL.AppendLine("And FunID = @PKFunID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_FunRow.SysID.Updated Then db.AddInParameter(dbcmd, "@SysID", DbType.String, SC_FunRow.SysID.Value)
            If SC_FunRow.FunID.Updated Then db.AddInParameter(dbcmd, "@FunID", DbType.String, SC_FunRow.FunID.Value)
            If SC_FunRow.FunName.Updated Then db.AddInParameter(dbcmd, "@FunName", DbType.String, SC_FunRow.FunName.Value)
            If SC_FunRow.FunEngName.Updated Then db.AddInParameter(dbcmd, "@FunEngName", DbType.String, SC_FunRow.FunEngName.Value)
            If SC_FunRow.ParentFormID.Updated Then db.AddInParameter(dbcmd, "@ParentFormID", DbType.String, SC_FunRow.ParentFormID.Value)
            If SC_FunRow.OrderSeq.Updated Then db.AddInParameter(dbcmd, "@OrderSeq", DbType.Int32, SC_FunRow.OrderSeq.Value)
            If SC_FunRow.IsMenu.Updated Then db.AddInParameter(dbcmd, "@IsMenu", DbType.String, SC_FunRow.IsMenu.Value)
            If SC_FunRow.CheckRight.Updated Then db.AddInParameter(dbcmd, "@CheckRight", DbType.String, SC_FunRow.CheckRight.Value)
            If SC_FunRow.LevelNo.Updated Then db.AddInParameter(dbcmd, "@LevelNo", DbType.Decimal, SC_FunRow.LevelNo.Value)
            If SC_FunRow.Path.Updated Then db.AddInParameter(dbcmd, "@Path", DbType.String, SC_FunRow.Path.Value)
            If SC_FunRow.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_FunRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_FunRow.CreateDate.Value))
            If SC_FunRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, SC_FunRow.LastChgComp.Value)
            If SC_FunRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_FunRow.LastChgID.Value)
            If SC_FunRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_FunRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_FunRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKSysID", DbType.String, IIf(SC_FunRow.LoadFromDataRow, SC_FunRow.SysID.OldValue, SC_FunRow.SysID.Value))
            db.AddInParameter(dbcmd, "@PKFunID", DbType.String, IIf(SC_FunRow.LoadFromDataRow, SC_FunRow.FunID.OldValue, SC_FunRow.FunID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal SC_FunRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_Fun Set")
            For i As Integer = 0 To SC_FunRow.FieldNames.Length - 1
                If Not SC_FunRow.IsIdentityField(SC_FunRow.FieldNames(i)) AndAlso SC_FunRow.IsUpdated(SC_FunRow.FieldNames(i)) AndAlso SC_FunRow.CreateUpdateSQL(SC_FunRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_FunRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where SysID = @PKSysID")
            strSQL.AppendLine("And FunID = @PKFunID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_FunRow.SysID.Updated Then db.AddInParameter(dbcmd, "@SysID", DbType.String, SC_FunRow.SysID.Value)
            If SC_FunRow.FunID.Updated Then db.AddInParameter(dbcmd, "@FunID", DbType.String, SC_FunRow.FunID.Value)
            If SC_FunRow.FunName.Updated Then db.AddInParameter(dbcmd, "@FunName", DbType.String, SC_FunRow.FunName.Value)
            If SC_FunRow.FunEngName.Updated Then db.AddInParameter(dbcmd, "@FunEngName", DbType.String, SC_FunRow.FunEngName.Value)
            If SC_FunRow.ParentFormID.Updated Then db.AddInParameter(dbcmd, "@ParentFormID", DbType.String, SC_FunRow.ParentFormID.Value)
            If SC_FunRow.OrderSeq.Updated Then db.AddInParameter(dbcmd, "@OrderSeq", DbType.Int32, SC_FunRow.OrderSeq.Value)
            If SC_FunRow.IsMenu.Updated Then db.AddInParameter(dbcmd, "@IsMenu", DbType.String, SC_FunRow.IsMenu.Value)
            If SC_FunRow.CheckRight.Updated Then db.AddInParameter(dbcmd, "@CheckRight", DbType.String, SC_FunRow.CheckRight.Value)
            If SC_FunRow.LevelNo.Updated Then db.AddInParameter(dbcmd, "@LevelNo", DbType.Decimal, SC_FunRow.LevelNo.Value)
            If SC_FunRow.Path.Updated Then db.AddInParameter(dbcmd, "@Path", DbType.String, SC_FunRow.Path.Value)
            If SC_FunRow.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_FunRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_FunRow.CreateDate.Value))
            If SC_FunRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, SC_FunRow.LastChgComp.Value)
            If SC_FunRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_FunRow.LastChgID.Value)
            If SC_FunRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_FunRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_FunRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKSysID", DbType.String, IIf(SC_FunRow.LoadFromDataRow, SC_FunRow.SysID.OldValue, SC_FunRow.SysID.Value))
            db.AddInParameter(dbcmd, "@PKFunID", DbType.String, IIf(SC_FunRow.LoadFromDataRow, SC_FunRow.FunID.OldValue, SC_FunRow.FunID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_FunRow As Row()) As Integer
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
                    For Each r As Row In SC_FunRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update SC_Fun Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where SysID = @PKSysID")
                        strSQL.AppendLine("And FunID = @PKFunID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.SysID.Updated Then db.AddInParameter(dbcmd, "@SysID", DbType.String, r.SysID.Value)
                        If r.FunID.Updated Then db.AddInParameter(dbcmd, "@FunID", DbType.String, r.FunID.Value)
                        If r.FunName.Updated Then db.AddInParameter(dbcmd, "@FunName", DbType.String, r.FunName.Value)
                        If r.FunEngName.Updated Then db.AddInParameter(dbcmd, "@FunEngName", DbType.String, r.FunEngName.Value)
                        If r.ParentFormID.Updated Then db.AddInParameter(dbcmd, "@ParentFormID", DbType.String, r.ParentFormID.Value)
                        If r.OrderSeq.Updated Then db.AddInParameter(dbcmd, "@OrderSeq", DbType.Int32, r.OrderSeq.Value)
                        If r.IsMenu.Updated Then db.AddInParameter(dbcmd, "@IsMenu", DbType.String, r.IsMenu.Value)
                        If r.CheckRight.Updated Then db.AddInParameter(dbcmd, "@CheckRight", DbType.String, r.CheckRight.Value)
                        If r.LevelNo.Updated Then db.AddInParameter(dbcmd, "@LevelNo", DbType.Decimal, r.LevelNo.Value)
                        If r.Path.Updated Then db.AddInParameter(dbcmd, "@Path", DbType.String, r.Path.Value)
                        If r.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKSysID", DbType.String, IIf(r.LoadFromDataRow, r.SysID.OldValue, r.SysID.Value))
                        db.AddInParameter(dbcmd, "@PKFunID", DbType.String, IIf(r.LoadFromDataRow, r.FunID.OldValue, r.FunID.Value))

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

        Public Function Update(ByVal SC_FunRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In SC_FunRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update SC_Fun Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where SysID = @PKSysID")
                strSQL.AppendLine("And FunID = @PKFunID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.SysID.Updated Then db.AddInParameter(dbcmd, "@SysID", DbType.String, r.SysID.Value)
                If r.FunID.Updated Then db.AddInParameter(dbcmd, "@FunID", DbType.String, r.FunID.Value)
                If r.FunName.Updated Then db.AddInParameter(dbcmd, "@FunName", DbType.String, r.FunName.Value)
                If r.FunEngName.Updated Then db.AddInParameter(dbcmd, "@FunEngName", DbType.String, r.FunEngName.Value)
                If r.ParentFormID.Updated Then db.AddInParameter(dbcmd, "@ParentFormID", DbType.String, r.ParentFormID.Value)
                If r.OrderSeq.Updated Then db.AddInParameter(dbcmd, "@OrderSeq", DbType.Int32, r.OrderSeq.Value)
                If r.IsMenu.Updated Then db.AddInParameter(dbcmd, "@IsMenu", DbType.String, r.IsMenu.Value)
                If r.CheckRight.Updated Then db.AddInParameter(dbcmd, "@CheckRight", DbType.String, r.CheckRight.Value)
                If r.LevelNo.Updated Then db.AddInParameter(dbcmd, "@LevelNo", DbType.Decimal, r.LevelNo.Value)
                If r.Path.Updated Then db.AddInParameter(dbcmd, "@Path", DbType.String, r.Path.Value)
                If r.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKSysID", DbType.String, IIf(r.LoadFromDataRow, r.SysID.OldValue, r.SysID.Value))
                db.AddInParameter(dbcmd, "@PKFunID", DbType.String, IIf(r.LoadFromDataRow, r.FunID.OldValue, r.FunID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal SC_FunRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_Fun")
            strSQL.AppendLine("Where SysID = @SysID")
            strSQL.AppendLine("And FunID = @FunID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@SysID", DbType.String, SC_FunRow.SysID.Value)
            db.AddInParameter(dbcmd, "@FunID", DbType.String, SC_FunRow.FunID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal SC_FunRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_Fun")
            strSQL.AppendLine("Where SysID = @SysID")
            strSQL.AppendLine("And FunID = @FunID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@SysID", DbType.String, SC_FunRow.SysID.Value)
            db.AddInParameter(dbcmd, "@FunID", DbType.String, SC_FunRow.FunID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_Fun")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal SC_FunRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_Fun")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    SysID, FunID, FunName, FunEngName, ParentFormID, OrderSeq, IsMenu, CheckRight, LevelNo,")
            strSQL.AppendLine("    Path, CreateDate, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @SysID, @FunID, @FunName, @FunEngName, @ParentFormID, @OrderSeq, @IsMenu, @CheckRight, @LevelNo,")
            strSQL.AppendLine("    @Path, @CreateDate, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@SysID", DbType.String, SC_FunRow.SysID.Value)
            db.AddInParameter(dbcmd, "@FunID", DbType.String, SC_FunRow.FunID.Value)
            db.AddInParameter(dbcmd, "@FunName", DbType.String, SC_FunRow.FunName.Value)
            db.AddInParameter(dbcmd, "@FunEngName", DbType.String, SC_FunRow.FunEngName.Value)
            db.AddInParameter(dbcmd, "@ParentFormID", DbType.String, SC_FunRow.ParentFormID.Value)
            db.AddInParameter(dbcmd, "@OrderSeq", DbType.Int32, SC_FunRow.OrderSeq.Value)
            db.AddInParameter(dbcmd, "@IsMenu", DbType.String, SC_FunRow.IsMenu.Value)
            db.AddInParameter(dbcmd, "@CheckRight", DbType.String, SC_FunRow.CheckRight.Value)
            db.AddInParameter(dbcmd, "@LevelNo", DbType.Decimal, SC_FunRow.LevelNo.Value)
            db.AddInParameter(dbcmd, "@Path", DbType.String, SC_FunRow.Path.Value)
            db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_FunRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_FunRow.CreateDate.Value))
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, SC_FunRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_FunRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_FunRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_FunRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal SC_FunRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_Fun")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    SysID, FunID, FunName, FunEngName, ParentFormID, OrderSeq, IsMenu, CheckRight, LevelNo,")
            strSQL.AppendLine("    Path, CreateDate, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @SysID, @FunID, @FunName, @FunEngName, @ParentFormID, @OrderSeq, @IsMenu, @CheckRight, @LevelNo,")
            strSQL.AppendLine("    @Path, @CreateDate, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@SysID", DbType.String, SC_FunRow.SysID.Value)
            db.AddInParameter(dbcmd, "@FunID", DbType.String, SC_FunRow.FunID.Value)
            db.AddInParameter(dbcmd, "@FunName", DbType.String, SC_FunRow.FunName.Value)
            db.AddInParameter(dbcmd, "@FunEngName", DbType.String, SC_FunRow.FunEngName.Value)
            db.AddInParameter(dbcmd, "@ParentFormID", DbType.String, SC_FunRow.ParentFormID.Value)
            db.AddInParameter(dbcmd, "@OrderSeq", DbType.Int32, SC_FunRow.OrderSeq.Value)
            db.AddInParameter(dbcmd, "@IsMenu", DbType.String, SC_FunRow.IsMenu.Value)
            db.AddInParameter(dbcmd, "@CheckRight", DbType.String, SC_FunRow.CheckRight.Value)
            db.AddInParameter(dbcmd, "@LevelNo", DbType.Decimal, SC_FunRow.LevelNo.Value)
            db.AddInParameter(dbcmd, "@Path", DbType.String, SC_FunRow.Path.Value)
            db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_FunRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_FunRow.CreateDate.Value))
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, SC_FunRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_FunRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_FunRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_FunRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal SC_FunRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_Fun")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    SysID, FunID, FunName, FunEngName, ParentFormID, OrderSeq, IsMenu, CheckRight, LevelNo,")
            strSQL.AppendLine("    Path, CreateDate, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @SysID, @FunID, @FunName, @FunEngName, @ParentFormID, @OrderSeq, @IsMenu, @CheckRight, @LevelNo,")
            strSQL.AppendLine("    @Path, @CreateDate, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_FunRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@SysID", DbType.String, r.SysID.Value)
                        db.AddInParameter(dbcmd, "@FunID", DbType.String, r.FunID.Value)
                        db.AddInParameter(dbcmd, "@FunName", DbType.String, r.FunName.Value)
                        db.AddInParameter(dbcmd, "@FunEngName", DbType.String, r.FunEngName.Value)
                        db.AddInParameter(dbcmd, "@ParentFormID", DbType.String, r.ParentFormID.Value)
                        db.AddInParameter(dbcmd, "@OrderSeq", DbType.Int32, r.OrderSeq.Value)
                        db.AddInParameter(dbcmd, "@IsMenu", DbType.String, r.IsMenu.Value)
                        db.AddInParameter(dbcmd, "@CheckRight", DbType.String, r.CheckRight.Value)
                        db.AddInParameter(dbcmd, "@LevelNo", DbType.Decimal, r.LevelNo.Value)
                        db.AddInParameter(dbcmd, "@Path", DbType.String, r.Path.Value)
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

        Public Function Insert(ByVal SC_FunRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_Fun")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    SysID, FunID, FunName, FunEngName, ParentFormID, OrderSeq, IsMenu, CheckRight, LevelNo,")
            strSQL.AppendLine("    Path, CreateDate, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @SysID, @FunID, @FunName, @FunEngName, @ParentFormID, @OrderSeq, @IsMenu, @CheckRight, @LevelNo,")
            strSQL.AppendLine("    @Path, @CreateDate, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_FunRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@SysID", DbType.String, r.SysID.Value)
                db.AddInParameter(dbcmd, "@FunID", DbType.String, r.FunID.Value)
                db.AddInParameter(dbcmd, "@FunName", DbType.String, r.FunName.Value)
                db.AddInParameter(dbcmd, "@FunEngName", DbType.String, r.FunEngName.Value)
                db.AddInParameter(dbcmd, "@ParentFormID", DbType.String, r.ParentFormID.Value)
                db.AddInParameter(dbcmd, "@OrderSeq", DbType.Int32, r.OrderSeq.Value)
                db.AddInParameter(dbcmd, "@IsMenu", DbType.String, r.IsMenu.Value)
                db.AddInParameter(dbcmd, "@CheckRight", DbType.String, r.CheckRight.Value)
                db.AddInParameter(dbcmd, "@LevelNo", DbType.Decimal, r.LevelNo.Value)
                db.AddInParameter(dbcmd, "@Path", DbType.String, r.Path.Value)
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

