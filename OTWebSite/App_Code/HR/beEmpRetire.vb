'****************************************************************
' Table:EmpRetire
' Created Date: 2015.08.13
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beEmpRetire
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "EmpID", "Kind", "EmpRatio", "CompRatio", "Amount", "ManagerFlag", "BossFlag", "ChangeCount", "LastChgDate" _
                                    , "LastChgComp", "LastChgID", "AmountNew", "TransferDate", "DeclareDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(Decimal), GetType(Decimal), GetType(String), GetType(String), GetType(String), GetType(Integer), GetType(Date) _
                                    , GetType(String), GetType(String), GetType(String), GetType(Date), GetType(Date) }
        Private m_PrimaryFields As String() = { "CompID", "EmpID" }

        Public ReadOnly Property Rows() As beEmpRetire.Rows 
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
        Public Sub Transfer2Row(EmpRetireTable As DataTable)
            For Each dr As DataRow In EmpRetireTable.Rows
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
                dr(m_Rows(i).EmpID.FieldName) = m_Rows(i).EmpID.Value
                dr(m_Rows(i).Kind.FieldName) = m_Rows(i).Kind.Value
                dr(m_Rows(i).EmpRatio.FieldName) = m_Rows(i).EmpRatio.Value
                dr(m_Rows(i).CompRatio.FieldName) = m_Rows(i).CompRatio.Value
                dr(m_Rows(i).Amount.FieldName) = m_Rows(i).Amount.Value
                dr(m_Rows(i).ManagerFlag.FieldName) = m_Rows(i).ManagerFlag.Value
                dr(m_Rows(i).BossFlag.FieldName) = m_Rows(i).BossFlag.Value
                dr(m_Rows(i).ChangeCount.FieldName) = m_Rows(i).ChangeCount.Value
                dr(m_Rows(i).LastChgDate.FieldName) = m_Rows(i).LastChgDate.Value
                dr(m_Rows(i).LastChgComp.FieldName) = m_Rows(i).LastChgComp.Value
                dr(m_Rows(i).LastChgID.FieldName) = m_Rows(i).LastChgID.Value
                dr(m_Rows(i).AmountNew.FieldName) = m_Rows(i).AmountNew.Value
                dr(m_Rows(i).TransferDate.FieldName) = m_Rows(i).TransferDate.Value
                dr(m_Rows(i).DeclareDate.FieldName) = m_Rows(i).DeclareDate.Value

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

        Public Sub Add(EmpRetireRow As Row)
            m_Rows.Add(EmpRetireRow)
        End Sub

        Public Sub Remove(EmpRetireRow As Row)
            If m_Rows.IndexOf(EmpRetireRow) >= 0 Then
                m_Rows.Remove(EmpRetireRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_EmpID As Field(Of String) = new Field(Of String)("EmpID", true)
        Private FI_Kind As Field(Of String) = new Field(Of String)("Kind", true)
        Private FI_EmpRatio As Field(Of Decimal) = new Field(Of Decimal)("EmpRatio", true)
        Private FI_CompRatio As Field(Of Decimal) = new Field(Of Decimal)("CompRatio", true)
        Private FI_Amount As Field(Of String) = new Field(Of String)("Amount", true)
        Private FI_ManagerFlag As Field(Of String) = new Field(Of String)("ManagerFlag", true)
        Private FI_BossFlag As Field(Of String) = new Field(Of String)("BossFlag", true)
        Private FI_ChangeCount As Field(Of Integer) = new Field(Of Integer)("ChangeCount", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_AmountNew As Field(Of String) = new Field(Of String)("AmountNew", true)
        Private FI_TransferDate As Field(Of Date) = new Field(Of Date)("TransferDate", true)
        Private FI_DeclareDate As Field(Of Date) = new Field(Of Date)("DeclareDate", true)
        Private m_FieldNames As String() = { "CompID", "EmpID", "Kind", "EmpRatio", "CompRatio", "Amount", "ManagerFlag", "BossFlag", "ChangeCount", "LastChgDate" _
                                    , "LastChgComp", "LastChgID", "AmountNew", "TransferDate", "DeclareDate" }
        Private m_PrimaryFields As String() = { "CompID", "EmpID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CompID"
                    Return FI_CompID.Value
                Case "EmpID"
                    Return FI_EmpID.Value
                Case "Kind"
                    Return FI_Kind.Value
                Case "EmpRatio"
                    Return FI_EmpRatio.Value
                Case "CompRatio"
                    Return FI_CompRatio.Value
                Case "Amount"
                    Return FI_Amount.Value
                Case "ManagerFlag"
                    Return FI_ManagerFlag.Value
                Case "BossFlag"
                    Return FI_BossFlag.Value
                Case "ChangeCount"
                    Return FI_ChangeCount.Value
                Case "LastChgDate"
                    Return FI_LastChgDate.Value
                Case "LastChgComp"
                    Return FI_LastChgComp.Value
                Case "LastChgID"
                    Return FI_LastChgID.Value
                Case "AmountNew"
                    Return FI_AmountNew.Value
                Case "TransferDate"
                    Return FI_TransferDate.Value
                Case "DeclareDate"
                    Return FI_DeclareDate.Value
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Sub SetFieldValue(ByVal fieldName As String, ByVal value As Object)
            Select Case fieldName
                Case "CompID"
                    FI_CompID.SetValue(value)
                Case "EmpID"
                    FI_EmpID.SetValue(value)
                Case "Kind"
                    FI_Kind.SetValue(value)
                Case "EmpRatio"
                    FI_EmpRatio.SetValue(value)
                Case "CompRatio"
                    FI_CompRatio.SetValue(value)
                Case "Amount"
                    FI_Amount.SetValue(value)
                Case "ManagerFlag"
                    FI_ManagerFlag.SetValue(value)
                Case "BossFlag"
                    FI_BossFlag.SetValue(value)
                Case "ChangeCount"
                    FI_ChangeCount.SetValue(value)
                Case "LastChgDate"
                    FI_LastChgDate.SetValue(value)
                Case "LastChgComp"
                    FI_LastChgComp.SetValue(value)
                Case "LastChgID"
                    FI_LastChgID.SetValue(value)
                Case "AmountNew"
                    FI_AmountNew.SetValue(value)
                Case "TransferDate"
                    FI_TransferDate.SetValue(value)
                Case "DeclareDate"
                    FI_DeclareDate.SetValue(value)
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
                Case "EmpID"
                    return FI_EmpID.Updated
                Case "Kind"
                    return FI_Kind.Updated
                Case "EmpRatio"
                    return FI_EmpRatio.Updated
                Case "CompRatio"
                    return FI_CompRatio.Updated
                Case "Amount"
                    return FI_Amount.Updated
                Case "ManagerFlag"
                    return FI_ManagerFlag.Updated
                Case "BossFlag"
                    return FI_BossFlag.Updated
                Case "ChangeCount"
                    return FI_ChangeCount.Updated
                Case "LastChgDate"
                    return FI_LastChgDate.Updated
                Case "LastChgComp"
                    return FI_LastChgComp.Updated
                Case "LastChgID"
                    return FI_LastChgID.Updated
                Case "AmountNew"
                    return FI_AmountNew.Updated
                Case "TransferDate"
                    return FI_TransferDate.Updated
                Case "DeclareDate"
                    return FI_DeclareDate.Updated
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public Function CreateUpdateSQL(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "CompID"
                    return FI_CompID.CreateUpdateSQL
                Case "EmpID"
                    return FI_EmpID.CreateUpdateSQL
                Case "Kind"
                    return FI_Kind.CreateUpdateSQL
                Case "EmpRatio"
                    return FI_EmpRatio.CreateUpdateSQL
                Case "CompRatio"
                    return FI_CompRatio.CreateUpdateSQL
                Case "Amount"
                    return FI_Amount.CreateUpdateSQL
                Case "ManagerFlag"
                    return FI_ManagerFlag.CreateUpdateSQL
                Case "BossFlag"
                    return FI_BossFlag.CreateUpdateSQL
                Case "ChangeCount"
                    return FI_ChangeCount.CreateUpdateSQL
                Case "LastChgDate"
                    return FI_LastChgDate.CreateUpdateSQL
                Case "LastChgComp"
                    return FI_LastChgComp.CreateUpdateSQL
                Case "LastChgID"
                    return FI_LastChgID.CreateUpdateSQL
                Case "AmountNew"
                    return FI_AmountNew.CreateUpdateSQL
                Case "TransferDate"
                    return FI_TransferDate.CreateUpdateSQL
                Case "DeclareDate"
                    return FI_DeclareDate.CreateUpdateSQL
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
            FI_EmpID.SetInitValue("")
            FI_Kind.SetInitValue("")
            FI_EmpRatio.SetInitValue(0)
            FI_CompRatio.SetInitValue(6)
            FI_Amount.SetInitValue("")
            FI_ManagerFlag.SetInitValue("0")
            FI_BossFlag.SetInitValue("0")
            FI_ChangeCount.SetInitValue(0)
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_AmountNew.SetInitValue("")
            FI_TransferDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_DeclareDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CompID.SetInitValue(dr("CompID"))
            FI_EmpID.SetInitValue(dr("EmpID"))
            FI_Kind.SetInitValue(dr("Kind"))
            FI_EmpRatio.SetInitValue(dr("EmpRatio"))
            FI_CompRatio.SetInitValue(dr("CompRatio"))
            FI_Amount.SetInitValue(dr("Amount"))
            FI_ManagerFlag.SetInitValue(dr("ManagerFlag"))
            FI_BossFlag.SetInitValue(dr("BossFlag"))
            FI_ChangeCount.SetInitValue(dr("ChangeCount"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_AmountNew.SetInitValue(dr("AmountNew"))
            FI_TransferDate.SetInitValue(dr("TransferDate"))
            FI_DeclareDate.SetInitValue(dr("DeclareDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CompID.Updated = False
            FI_EmpID.Updated = False
            FI_Kind.Updated = False
            FI_EmpRatio.Updated = False
            FI_CompRatio.Updated = False
            FI_Amount.Updated = False
            FI_ManagerFlag.Updated = False
            FI_BossFlag.Updated = False
            FI_ChangeCount.Updated = False
            FI_LastChgDate.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_AmountNew.Updated = False
            FI_TransferDate.Updated = False
            FI_DeclareDate.Updated = False
        End Sub

        Public ReadOnly Property CompID As Field(Of String) 
            Get
                Return FI_CompID
            End Get
        End Property

        Public ReadOnly Property EmpID As Field(Of String) 
            Get
                Return FI_EmpID
            End Get
        End Property

        Public ReadOnly Property Kind As Field(Of String) 
            Get
                Return FI_Kind
            End Get
        End Property

        Public ReadOnly Property EmpRatio As Field(Of Decimal) 
            Get
                Return FI_EmpRatio
            End Get
        End Property

        Public ReadOnly Property CompRatio As Field(Of Decimal) 
            Get
                Return FI_CompRatio
            End Get
        End Property

        Public ReadOnly Property Amount As Field(Of String) 
            Get
                Return FI_Amount
            End Get
        End Property

        Public ReadOnly Property ManagerFlag As Field(Of String) 
            Get
                Return FI_ManagerFlag
            End Get
        End Property

        Public ReadOnly Property BossFlag As Field(Of String) 
            Get
                Return FI_BossFlag
            End Get
        End Property

        Public ReadOnly Property ChangeCount As Field(Of Integer) 
            Get
                Return FI_ChangeCount
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

        Public ReadOnly Property AmountNew As Field(Of String) 
            Get
                Return FI_AmountNew
            End Get
        End Property

        Public ReadOnly Property TransferDate As Field(Of Date) 
            Get
                Return FI_TransferDate
            End Get
        End Property

        Public ReadOnly Property DeclareDate As Field(Of Date) 
            Get
                Return FI_DeclareDate
            End Get
        End Property

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal EmpRetireRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From EmpRetire")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpRetireRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpRetireRow.EmpID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal EmpRetireRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From EmpRetire")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpRetireRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpRetireRow.EmpID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal EmpRetireRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From EmpRetire")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In EmpRetireRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal EmpRetireRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From EmpRetire")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In EmpRetireRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal EmpRetireRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmpRetire")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpRetireRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpRetireRow.EmpID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(EmpRetireRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmpRetire")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpRetireRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpRetireRow.EmpID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal EmpRetireRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update EmpRetire Set")
            For i As Integer = 0 To EmpRetireRow.FieldNames.Length - 1
                If Not EmpRetireRow.IsIdentityField(EmpRetireRow.FieldNames(i)) AndAlso EmpRetireRow.IsUpdated(EmpRetireRow.FieldNames(i)) AndAlso EmpRetireRow.CreateUpdateSQL(EmpRetireRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, EmpRetireRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And EmpID = @PKEmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If EmpRetireRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpRetireRow.CompID.Value)
            If EmpRetireRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpRetireRow.EmpID.Value)
            If EmpRetireRow.Kind.Updated Then db.AddInParameter(dbcmd, "@Kind", DbType.String, EmpRetireRow.Kind.Value)
            If EmpRetireRow.EmpRatio.Updated Then db.AddInParameter(dbcmd, "@EmpRatio", DbType.Decimal, EmpRetireRow.EmpRatio.Value)
            If EmpRetireRow.CompRatio.Updated Then db.AddInParameter(dbcmd, "@CompRatio", DbType.Decimal, EmpRetireRow.CompRatio.Value)
            If EmpRetireRow.Amount.Updated Then db.AddInParameter(dbcmd, "@Amount", DbType.String, EmpRetireRow.Amount.Value)
            If EmpRetireRow.ManagerFlag.Updated Then db.AddInParameter(dbcmd, "@ManagerFlag", DbType.String, EmpRetireRow.ManagerFlag.Value)
            If EmpRetireRow.BossFlag.Updated Then db.AddInParameter(dbcmd, "@BossFlag", DbType.String, EmpRetireRow.BossFlag.Value)
            If EmpRetireRow.ChangeCount.Updated Then db.AddInParameter(dbcmd, "@ChangeCount", DbType.Int32, EmpRetireRow.ChangeCount.Value)
            If EmpRetireRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpRetireRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpRetireRow.LastChgDate.Value))
            If EmpRetireRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpRetireRow.LastChgComp.Value)
            If EmpRetireRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpRetireRow.LastChgID.Value)
            If EmpRetireRow.AmountNew.Updated Then db.AddInParameter(dbcmd, "@AmountNew", DbType.String, EmpRetireRow.AmountNew.Value)
            If EmpRetireRow.TransferDate.Updated Then db.AddInParameter(dbcmd, "@TransferDate", DbType.Date, IIf(IsDateTimeNull(EmpRetireRow.TransferDate.Value), Convert.ToDateTime("1900/1/1"), EmpRetireRow.TransferDate.Value))
            If EmpRetireRow.DeclareDate.Updated Then db.AddInParameter(dbcmd, "@DeclareDate", DbType.Date, IIf(IsDateTimeNull(EmpRetireRow.DeclareDate.Value), Convert.ToDateTime("1900/1/1"), EmpRetireRow.DeclareDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(EmpRetireRow.LoadFromDataRow, EmpRetireRow.CompID.OldValue, EmpRetireRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(EmpRetireRow.LoadFromDataRow, EmpRetireRow.EmpID.OldValue, EmpRetireRow.EmpID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal EmpRetireRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update EmpRetire Set")
            For i As Integer = 0 To EmpRetireRow.FieldNames.Length - 1
                If Not EmpRetireRow.IsIdentityField(EmpRetireRow.FieldNames(i)) AndAlso EmpRetireRow.IsUpdated(EmpRetireRow.FieldNames(i)) AndAlso EmpRetireRow.CreateUpdateSQL(EmpRetireRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, EmpRetireRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And EmpID = @PKEmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If EmpRetireRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpRetireRow.CompID.Value)
            If EmpRetireRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpRetireRow.EmpID.Value)
            If EmpRetireRow.Kind.Updated Then db.AddInParameter(dbcmd, "@Kind", DbType.String, EmpRetireRow.Kind.Value)
            If EmpRetireRow.EmpRatio.Updated Then db.AddInParameter(dbcmd, "@EmpRatio", DbType.Decimal, EmpRetireRow.EmpRatio.Value)
            If EmpRetireRow.CompRatio.Updated Then db.AddInParameter(dbcmd, "@CompRatio", DbType.Decimal, EmpRetireRow.CompRatio.Value)
            If EmpRetireRow.Amount.Updated Then db.AddInParameter(dbcmd, "@Amount", DbType.String, EmpRetireRow.Amount.Value)
            If EmpRetireRow.ManagerFlag.Updated Then db.AddInParameter(dbcmd, "@ManagerFlag", DbType.String, EmpRetireRow.ManagerFlag.Value)
            If EmpRetireRow.BossFlag.Updated Then db.AddInParameter(dbcmd, "@BossFlag", DbType.String, EmpRetireRow.BossFlag.Value)
            If EmpRetireRow.ChangeCount.Updated Then db.AddInParameter(dbcmd, "@ChangeCount", DbType.Int32, EmpRetireRow.ChangeCount.Value)
            If EmpRetireRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpRetireRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpRetireRow.LastChgDate.Value))
            If EmpRetireRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpRetireRow.LastChgComp.Value)
            If EmpRetireRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpRetireRow.LastChgID.Value)
            If EmpRetireRow.AmountNew.Updated Then db.AddInParameter(dbcmd, "@AmountNew", DbType.String, EmpRetireRow.AmountNew.Value)
            If EmpRetireRow.TransferDate.Updated Then db.AddInParameter(dbcmd, "@TransferDate", DbType.Date, IIf(IsDateTimeNull(EmpRetireRow.TransferDate.Value), Convert.ToDateTime("1900/1/1"), EmpRetireRow.TransferDate.Value))
            If EmpRetireRow.DeclareDate.Updated Then db.AddInParameter(dbcmd, "@DeclareDate", DbType.Date, IIf(IsDateTimeNull(EmpRetireRow.DeclareDate.Value), Convert.ToDateTime("1900/1/1"), EmpRetireRow.DeclareDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(EmpRetireRow.LoadFromDataRow, EmpRetireRow.CompID.OldValue, EmpRetireRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(EmpRetireRow.LoadFromDataRow, EmpRetireRow.EmpID.OldValue, EmpRetireRow.EmpID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal EmpRetireRow As Row()) As Integer
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
                    For Each r As Row In EmpRetireRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update EmpRetire Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where CompID = @PKCompID")
                        strSQL.AppendLine("And EmpID = @PKEmpID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        If r.Kind.Updated Then db.AddInParameter(dbcmd, "@Kind", DbType.String, r.Kind.Value)
                        If r.EmpRatio.Updated Then db.AddInParameter(dbcmd, "@EmpRatio", DbType.Decimal, r.EmpRatio.Value)
                        If r.CompRatio.Updated Then db.AddInParameter(dbcmd, "@CompRatio", DbType.Decimal, r.CompRatio.Value)
                        If r.Amount.Updated Then db.AddInParameter(dbcmd, "@Amount", DbType.String, r.Amount.Value)
                        If r.ManagerFlag.Updated Then db.AddInParameter(dbcmd, "@ManagerFlag", DbType.String, r.ManagerFlag.Value)
                        If r.BossFlag.Updated Then db.AddInParameter(dbcmd, "@BossFlag", DbType.String, r.BossFlag.Value)
                        If r.ChangeCount.Updated Then db.AddInParameter(dbcmd, "@ChangeCount", DbType.Int32, r.ChangeCount.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.AmountNew.Updated Then db.AddInParameter(dbcmd, "@AmountNew", DbType.String, r.AmountNew.Value)
                        If r.TransferDate.Updated Then db.AddInParameter(dbcmd, "@TransferDate", DbType.Date, IIf(IsDateTimeNull(r.TransferDate.Value), Convert.ToDateTime("1900/1/1"), r.TransferDate.Value))
                        If r.DeclareDate.Updated Then db.AddInParameter(dbcmd, "@DeclareDate", DbType.Date, IIf(IsDateTimeNull(r.DeclareDate.Value), Convert.ToDateTime("1900/1/1"), r.DeclareDate.Value))
                        db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                        db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))

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

        Public Function Update(ByVal EmpRetireRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In EmpRetireRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update EmpRetire Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where CompID = @PKCompID")
                strSQL.AppendLine("And EmpID = @PKEmpID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                If r.Kind.Updated Then db.AddInParameter(dbcmd, "@Kind", DbType.String, r.Kind.Value)
                If r.EmpRatio.Updated Then db.AddInParameter(dbcmd, "@EmpRatio", DbType.Decimal, r.EmpRatio.Value)
                If r.CompRatio.Updated Then db.AddInParameter(dbcmd, "@CompRatio", DbType.Decimal, r.CompRatio.Value)
                If r.Amount.Updated Then db.AddInParameter(dbcmd, "@Amount", DbType.String, r.Amount.Value)
                If r.ManagerFlag.Updated Then db.AddInParameter(dbcmd, "@ManagerFlag", DbType.String, r.ManagerFlag.Value)
                If r.BossFlag.Updated Then db.AddInParameter(dbcmd, "@BossFlag", DbType.String, r.BossFlag.Value)
                If r.ChangeCount.Updated Then db.AddInParameter(dbcmd, "@ChangeCount", DbType.Int32, r.ChangeCount.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.AmountNew.Updated Then db.AddInParameter(dbcmd, "@AmountNew", DbType.String, r.AmountNew.Value)
                If r.TransferDate.Updated Then db.AddInParameter(dbcmd, "@TransferDate", DbType.Date, IIf(IsDateTimeNull(r.TransferDate.Value), Convert.ToDateTime("1900/1/1"), r.TransferDate.Value))
                If r.DeclareDate.Updated Then db.AddInParameter(dbcmd, "@DeclareDate", DbType.Date, IIf(IsDateTimeNull(r.DeclareDate.Value), Convert.ToDateTime("1900/1/1"), r.DeclareDate.Value))
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal EmpRetireRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From EmpRetire")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpRetireRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpRetireRow.EmpID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal EmpRetireRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From EmpRetire")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpRetireRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpRetireRow.EmpID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmpRetire")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal EmpRetireRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into EmpRetire")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, Kind, EmpRatio, CompRatio, Amount, ManagerFlag, BossFlag, ChangeCount,")
            strSQL.AppendLine("    LastChgDate, LastChgComp, LastChgID, AmountNew, TransferDate, DeclareDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @Kind, @EmpRatio, @CompRatio, @Amount, @ManagerFlag, @BossFlag, @ChangeCount,")
            strSQL.AppendLine("    @LastChgDate, @LastChgComp, @LastChgID, @AmountNew, @TransferDate, @DeclareDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpRetireRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpRetireRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@Kind", DbType.String, EmpRetireRow.Kind.Value)
            db.AddInParameter(dbcmd, "@EmpRatio", DbType.Decimal, EmpRetireRow.EmpRatio.Value)
            db.AddInParameter(dbcmd, "@CompRatio", DbType.Decimal, EmpRetireRow.CompRatio.Value)
            db.AddInParameter(dbcmd, "@Amount", DbType.String, EmpRetireRow.Amount.Value)
            db.AddInParameter(dbcmd, "@ManagerFlag", DbType.String, EmpRetireRow.ManagerFlag.Value)
            db.AddInParameter(dbcmd, "@BossFlag", DbType.String, EmpRetireRow.BossFlag.Value)
            db.AddInParameter(dbcmd, "@ChangeCount", DbType.Int32, EmpRetireRow.ChangeCount.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpRetireRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpRetireRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpRetireRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpRetireRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@AmountNew", DbType.String, EmpRetireRow.AmountNew.Value)
            db.AddInParameter(dbcmd, "@TransferDate", DbType.Date, IIf(IsDateTimeNull(EmpRetireRow.TransferDate.Value), Convert.ToDateTime("1900/1/1"), EmpRetireRow.TransferDate.Value))
            db.AddInParameter(dbcmd, "@DeclareDate", DbType.Date, IIf(IsDateTimeNull(EmpRetireRow.DeclareDate.Value), Convert.ToDateTime("1900/1/1"), EmpRetireRow.DeclareDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal EmpRetireRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into EmpRetire")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, Kind, EmpRatio, CompRatio, Amount, ManagerFlag, BossFlag, ChangeCount,")
            strSQL.AppendLine("    LastChgDate, LastChgComp, LastChgID, AmountNew, TransferDate, DeclareDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @Kind, @EmpRatio, @CompRatio, @Amount, @ManagerFlag, @BossFlag, @ChangeCount,")
            strSQL.AppendLine("    @LastChgDate, @LastChgComp, @LastChgID, @AmountNew, @TransferDate, @DeclareDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpRetireRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpRetireRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@Kind", DbType.String, EmpRetireRow.Kind.Value)
            db.AddInParameter(dbcmd, "@EmpRatio", DbType.Decimal, EmpRetireRow.EmpRatio.Value)
            db.AddInParameter(dbcmd, "@CompRatio", DbType.Decimal, EmpRetireRow.CompRatio.Value)
            db.AddInParameter(dbcmd, "@Amount", DbType.String, EmpRetireRow.Amount.Value)
            db.AddInParameter(dbcmd, "@ManagerFlag", DbType.String, EmpRetireRow.ManagerFlag.Value)
            db.AddInParameter(dbcmd, "@BossFlag", DbType.String, EmpRetireRow.BossFlag.Value)
            db.AddInParameter(dbcmd, "@ChangeCount", DbType.Int32, EmpRetireRow.ChangeCount.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpRetireRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpRetireRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpRetireRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpRetireRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@AmountNew", DbType.String, EmpRetireRow.AmountNew.Value)
            db.AddInParameter(dbcmd, "@TransferDate", DbType.Date, IIf(IsDateTimeNull(EmpRetireRow.TransferDate.Value), Convert.ToDateTime("1900/1/1"), EmpRetireRow.TransferDate.Value))
            db.AddInParameter(dbcmd, "@DeclareDate", DbType.Date, IIf(IsDateTimeNull(EmpRetireRow.DeclareDate.Value), Convert.ToDateTime("1900/1/1"), EmpRetireRow.DeclareDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal EmpRetireRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into EmpRetire")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, Kind, EmpRatio, CompRatio, Amount, ManagerFlag, BossFlag, ChangeCount,")
            strSQL.AppendLine("    LastChgDate, LastChgComp, LastChgID, AmountNew, TransferDate, DeclareDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @Kind, @EmpRatio, @CompRatio, @Amount, @ManagerFlag, @BossFlag, @ChangeCount,")
            strSQL.AppendLine("    @LastChgDate, @LastChgComp, @LastChgID, @AmountNew, @TransferDate, @DeclareDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In EmpRetireRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@Kind", DbType.String, r.Kind.Value)
                        db.AddInParameter(dbcmd, "@EmpRatio", DbType.Decimal, r.EmpRatio.Value)
                        db.AddInParameter(dbcmd, "@CompRatio", DbType.Decimal, r.CompRatio.Value)
                        db.AddInParameter(dbcmd, "@Amount", DbType.String, r.Amount.Value)
                        db.AddInParameter(dbcmd, "@ManagerFlag", DbType.String, r.ManagerFlag.Value)
                        db.AddInParameter(dbcmd, "@BossFlag", DbType.String, r.BossFlag.Value)
                        db.AddInParameter(dbcmd, "@ChangeCount", DbType.Int32, r.ChangeCount.Value)
                        db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        db.AddInParameter(dbcmd, "@AmountNew", DbType.String, r.AmountNew.Value)
                        db.AddInParameter(dbcmd, "@TransferDate", DbType.Date, IIf(IsDateTimeNull(r.TransferDate.Value), Convert.ToDateTime("1900/1/1"), r.TransferDate.Value))
                        db.AddInParameter(dbcmd, "@DeclareDate", DbType.Date, IIf(IsDateTimeNull(r.DeclareDate.Value), Convert.ToDateTime("1900/1/1"), r.DeclareDate.Value))

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

        Public Function Insert(ByVal EmpRetireRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into EmpRetire")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, Kind, EmpRatio, CompRatio, Amount, ManagerFlag, BossFlag, ChangeCount,")
            strSQL.AppendLine("    LastChgDate, LastChgComp, LastChgID, AmountNew, TransferDate, DeclareDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @Kind, @EmpRatio, @CompRatio, @Amount, @ManagerFlag, @BossFlag, @ChangeCount,")
            strSQL.AppendLine("    @LastChgDate, @LastChgComp, @LastChgID, @AmountNew, @TransferDate, @DeclareDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In EmpRetireRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@Kind", DbType.String, r.Kind.Value)
                db.AddInParameter(dbcmd, "@EmpRatio", DbType.Decimal, r.EmpRatio.Value)
                db.AddInParameter(dbcmd, "@CompRatio", DbType.Decimal, r.CompRatio.Value)
                db.AddInParameter(dbcmd, "@Amount", DbType.String, r.Amount.Value)
                db.AddInParameter(dbcmd, "@ManagerFlag", DbType.String, r.ManagerFlag.Value)
                db.AddInParameter(dbcmd, "@BossFlag", DbType.String, r.BossFlag.Value)
                db.AddInParameter(dbcmd, "@ChangeCount", DbType.Int32, r.ChangeCount.Value)
                db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                db.AddInParameter(dbcmd, "@AmountNew", DbType.String, r.AmountNew.Value)
                db.AddInParameter(dbcmd, "@TransferDate", DbType.Date, IIf(IsDateTimeNull(r.TransferDate.Value), Convert.ToDateTime("1900/1/1"), r.TransferDate.Value))
                db.AddInParameter(dbcmd, "@DeclareDate", DbType.Date, IIf(IsDateTimeNull(r.DeclareDate.Value), Convert.ToDateTime("1900/1/1"), r.DeclareDate.Value))

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

