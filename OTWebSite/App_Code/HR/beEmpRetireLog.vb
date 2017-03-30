'****************************************************************
' Table:EmpRetireLog
' Created Date: 2015.09.03
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beEmpRetireLog
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "EmpID", "TransferType", "LastChgDate", "LastChgComp", "LastChgID", "ApplyDate", "OldData", "NewData", "TransferDate" _
                                    , "DeclareDate", "DownDate", "PayDate", "Remark" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(Date), GetType(String), GetType(String), GetType(Date), GetType(String), GetType(String), GetType(Date) _
                                    , GetType(Date), GetType(Date), GetType(Integer), GetType(String) }
        Private m_PrimaryFields As String() = { "CompID", "EmpID", "TransferType", "LastChgDate" }

        Public ReadOnly Property Rows() As beEmpRetireLog.Rows 
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
        Public Sub Transfer2Row(EmpRetireLogTable As DataTable)
            For Each dr As DataRow In EmpRetireLogTable.Rows
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
                dr(m_Rows(i).TransferType.FieldName) = m_Rows(i).TransferType.Value
                dr(m_Rows(i).LastChgDate.FieldName) = m_Rows(i).LastChgDate.Value
                dr(m_Rows(i).LastChgComp.FieldName) = m_Rows(i).LastChgComp.Value
                dr(m_Rows(i).LastChgID.FieldName) = m_Rows(i).LastChgID.Value
                dr(m_Rows(i).ApplyDate.FieldName) = m_Rows(i).ApplyDate.Value
                dr(m_Rows(i).OldData.FieldName) = m_Rows(i).OldData.Value
                dr(m_Rows(i).NewData.FieldName) = m_Rows(i).NewData.Value
                dr(m_Rows(i).TransferDate.FieldName) = m_Rows(i).TransferDate.Value
                dr(m_Rows(i).DeclareDate.FieldName) = m_Rows(i).DeclareDate.Value
                dr(m_Rows(i).DownDate.FieldName) = m_Rows(i).DownDate.Value
                dr(m_Rows(i).PayDate.FieldName) = m_Rows(i).PayDate.Value
                dr(m_Rows(i).Remark.FieldName) = m_Rows(i).Remark.Value

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

        Public Sub Add(EmpRetireLogRow As Row)
            m_Rows.Add(EmpRetireLogRow)
        End Sub

        Public Sub Remove(EmpRetireLogRow As Row)
            If m_Rows.IndexOf(EmpRetireLogRow) >= 0 Then
                m_Rows.Remove(EmpRetireLogRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_EmpID As Field(Of String) = new Field(Of String)("EmpID", true)
        Private FI_TransferType As Field(Of String) = new Field(Of String)("TransferType", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_ApplyDate As Field(Of Date) = new Field(Of Date)("ApplyDate", true)
        Private FI_OldData As Field(Of String) = new Field(Of String)("OldData", true)
        Private FI_NewData As Field(Of String) = new Field(Of String)("NewData", true)
        Private FI_TransferDate As Field(Of Date) = new Field(Of Date)("TransferDate", true)
        Private FI_DeclareDate As Field(Of Date) = new Field(Of Date)("DeclareDate", true)
        Private FI_DownDate As Field(Of Date) = new Field(Of Date)("DownDate", true)
        Private FI_PayDate As Field(Of Integer) = new Field(Of Integer)("PayDate", true)
        Private FI_Remark As Field(Of String) = new Field(Of String)("Remark", true)
        Private m_FieldNames As String() = { "CompID", "EmpID", "TransferType", "LastChgDate", "LastChgComp", "LastChgID", "ApplyDate", "OldData", "NewData", "TransferDate" _
                                    , "DeclareDate", "DownDate", "PayDate", "Remark" }
        Private m_PrimaryFields As String() = { "CompID", "EmpID", "TransferType", "LastChgDate" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CompID"
                    Return FI_CompID.Value
                Case "EmpID"
                    Return FI_EmpID.Value
                Case "TransferType"
                    Return FI_TransferType.Value
                Case "LastChgDate"
                    Return FI_LastChgDate.Value
                Case "LastChgComp"
                    Return FI_LastChgComp.Value
                Case "LastChgID"
                    Return FI_LastChgID.Value
                Case "ApplyDate"
                    Return FI_ApplyDate.Value
                Case "OldData"
                    Return FI_OldData.Value
                Case "NewData"
                    Return FI_NewData.Value
                Case "TransferDate"
                    Return FI_TransferDate.Value
                Case "DeclareDate"
                    Return FI_DeclareDate.Value
                Case "DownDate"
                    Return FI_DownDate.Value
                Case "PayDate"
                    Return FI_PayDate.Value
                Case "Remark"
                    Return FI_Remark.Value
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
                Case "TransferType"
                    FI_TransferType.SetValue(value)
                Case "LastChgDate"
                    FI_LastChgDate.SetValue(value)
                Case "LastChgComp"
                    FI_LastChgComp.SetValue(value)
                Case "LastChgID"
                    FI_LastChgID.SetValue(value)
                Case "ApplyDate"
                    FI_ApplyDate.SetValue(value)
                Case "OldData"
                    FI_OldData.SetValue(value)
                Case "NewData"
                    FI_NewData.SetValue(value)
                Case "TransferDate"
                    FI_TransferDate.SetValue(value)
                Case "DeclareDate"
                    FI_DeclareDate.SetValue(value)
                Case "DownDate"
                    FI_DownDate.SetValue(value)
                Case "PayDate"
                    FI_PayDate.SetValue(value)
                Case "Remark"
                    FI_Remark.SetValue(value)
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
                Case "TransferType"
                    return FI_TransferType.Updated
                Case "LastChgDate"
                    return FI_LastChgDate.Updated
                Case "LastChgComp"
                    return FI_LastChgComp.Updated
                Case "LastChgID"
                    return FI_LastChgID.Updated
                Case "ApplyDate"
                    return FI_ApplyDate.Updated
                Case "OldData"
                    return FI_OldData.Updated
                Case "NewData"
                    return FI_NewData.Updated
                Case "TransferDate"
                    return FI_TransferDate.Updated
                Case "DeclareDate"
                    return FI_DeclareDate.Updated
                Case "DownDate"
                    return FI_DownDate.Updated
                Case "PayDate"
                    return FI_PayDate.Updated
                Case "Remark"
                    return FI_Remark.Updated
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
                Case "TransferType"
                    return FI_TransferType.CreateUpdateSQL
                Case "LastChgDate"
                    return FI_LastChgDate.CreateUpdateSQL
                Case "LastChgComp"
                    return FI_LastChgComp.CreateUpdateSQL
                Case "LastChgID"
                    return FI_LastChgID.CreateUpdateSQL
                Case "ApplyDate"
                    return FI_ApplyDate.CreateUpdateSQL
                Case "OldData"
                    return FI_OldData.CreateUpdateSQL
                Case "NewData"
                    return FI_NewData.CreateUpdateSQL
                Case "TransferDate"
                    return FI_TransferDate.CreateUpdateSQL
                Case "DeclareDate"
                    return FI_DeclareDate.CreateUpdateSQL
                Case "DownDate"
                    return FI_DownDate.CreateUpdateSQL
                Case "PayDate"
                    return FI_PayDate.CreateUpdateSQL
                Case "Remark"
                    return FI_Remark.CreateUpdateSQL
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
            FI_TransferType.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_ApplyDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_OldData.SetInitValue("")
            FI_NewData.SetInitValue("")
            FI_TransferDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_DeclareDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_DownDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_PayDate.SetInitValue(0)
            FI_Remark.SetInitValue("")
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CompID.SetInitValue(dr("CompID"))
            FI_EmpID.SetInitValue(dr("EmpID"))
            FI_TransferType.SetInitValue(dr("TransferType"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_ApplyDate.SetInitValue(dr("ApplyDate"))
            FI_OldData.SetInitValue(dr("OldData"))
            FI_NewData.SetInitValue(dr("NewData"))
            FI_TransferDate.SetInitValue(dr("TransferDate"))
            FI_DeclareDate.SetInitValue(dr("DeclareDate"))
            FI_DownDate.SetInitValue(dr("DownDate"))
            FI_PayDate.SetInitValue(dr("PayDate"))
            FI_Remark.SetInitValue(dr("Remark"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CompID.Updated = False
            FI_EmpID.Updated = False
            FI_TransferType.Updated = False
            FI_LastChgDate.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_ApplyDate.Updated = False
            FI_OldData.Updated = False
            FI_NewData.Updated = False
            FI_TransferDate.Updated = False
            FI_DeclareDate.Updated = False
            FI_DownDate.Updated = False
            FI_PayDate.Updated = False
            FI_Remark.Updated = False
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

        Public ReadOnly Property TransferType As Field(Of String) 
            Get
                Return FI_TransferType
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

        Public ReadOnly Property ApplyDate As Field(Of Date) 
            Get
                Return FI_ApplyDate
            End Get
        End Property

        Public ReadOnly Property OldData As Field(Of String) 
            Get
                Return FI_OldData
            End Get
        End Property

        Public ReadOnly Property NewData As Field(Of String) 
            Get
                Return FI_NewData
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

        Public ReadOnly Property DownDate As Field(Of Date) 
            Get
                Return FI_DownDate
            End Get
        End Property

        Public ReadOnly Property PayDate As Field(Of Integer) 
            Get
                Return FI_PayDate
            End Get
        End Property

        Public ReadOnly Property Remark As Field(Of String) 
            Get
                Return FI_Remark
            End Get
        End Property

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal EmpRetireLogRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From EmpRetireLog")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And TransferType = @TransferType")
            strSQL.AppendLine("And LastChgDate = @LastChgDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpRetireLogRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpRetireLogRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@TransferType", DbType.String, EmpRetireLogRow.TransferType.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, EmpRetireLogRow.LastChgDate.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal EmpRetireLogRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From EmpRetireLog")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And TransferType = @TransferType")
            strSQL.AppendLine("And LastChgDate = @LastChgDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpRetireLogRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpRetireLogRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@TransferType", DbType.String, EmpRetireLogRow.TransferType.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, EmpRetireLogRow.LastChgDate.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal EmpRetireLogRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From EmpRetireLog")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And TransferType = @TransferType")
            strSQL.AppendLine("And LastChgDate = @LastChgDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In EmpRetireLogRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@TransferType", DbType.String, r.TransferType.Value)
                        db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, r.LastChgDate.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal EmpRetireLogRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From EmpRetireLog")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And TransferType = @TransferType")
            strSQL.AppendLine("And LastChgDate = @LastChgDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In EmpRetireLogRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@TransferType", DbType.String, r.TransferType.Value)
                db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, r.LastChgDate.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal EmpRetireLogRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmpRetireLog")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And TransferType = @TransferType")
            strSQL.AppendLine("And LastChgDate = @LastChgDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpRetireLogRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpRetireLogRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@TransferType", DbType.String, EmpRetireLogRow.TransferType.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, EmpRetireLogRow.LastChgDate.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(EmpRetireLogRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmpRetireLog")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And TransferType = @TransferType")
            strSQL.AppendLine("And LastChgDate = @LastChgDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpRetireLogRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpRetireLogRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@TransferType", DbType.String, EmpRetireLogRow.TransferType.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, EmpRetireLogRow.LastChgDate.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal EmpRetireLogRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update EmpRetireLog Set")
            For i As Integer = 0 To EmpRetireLogRow.FieldNames.Length - 1
                If Not EmpRetireLogRow.IsIdentityField(EmpRetireLogRow.FieldNames(i)) AndAlso EmpRetireLogRow.IsUpdated(EmpRetireLogRow.FieldNames(i)) AndAlso EmpRetireLogRow.CreateUpdateSQL(EmpRetireLogRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, EmpRetireLogRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And EmpID = @PKEmpID")
            strSQL.AppendLine("And TransferType = @PKTransferType")
            strSQL.AppendLine("And LastChgDate = @PKLastChgDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If EmpRetireLogRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpRetireLogRow.CompID.Value)
            If EmpRetireLogRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpRetireLogRow.EmpID.Value)
            If EmpRetireLogRow.TransferType.Updated Then db.AddInParameter(dbcmd, "@TransferType", DbType.String, EmpRetireLogRow.TransferType.Value)
            If EmpRetireLogRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpRetireLogRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpRetireLogRow.LastChgDate.Value))
            If EmpRetireLogRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpRetireLogRow.LastChgComp.Value)
            If EmpRetireLogRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpRetireLogRow.LastChgID.Value)
            If EmpRetireLogRow.ApplyDate.Updated Then db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, IIf(IsDateTimeNull(EmpRetireLogRow.ApplyDate.Value), Convert.ToDateTime("1900/1/1"), EmpRetireLogRow.ApplyDate.Value))
            If EmpRetireLogRow.OldData.Updated Then db.AddInParameter(dbcmd, "@OldData", DbType.String, EmpRetireLogRow.OldData.Value)
            If EmpRetireLogRow.NewData.Updated Then db.AddInParameter(dbcmd, "@NewData", DbType.String, EmpRetireLogRow.NewData.Value)
            If EmpRetireLogRow.TransferDate.Updated Then db.AddInParameter(dbcmd, "@TransferDate", DbType.Date, IIf(IsDateTimeNull(EmpRetireLogRow.TransferDate.Value), Convert.ToDateTime("1900/1/1"), EmpRetireLogRow.TransferDate.Value))
            If EmpRetireLogRow.DeclareDate.Updated Then db.AddInParameter(dbcmd, "@DeclareDate", DbType.Date, IIf(IsDateTimeNull(EmpRetireLogRow.DeclareDate.Value), Convert.ToDateTime("1900/1/1"), EmpRetireLogRow.DeclareDate.Value))
            If EmpRetireLogRow.DownDate.Updated Then db.AddInParameter(dbcmd, "@DownDate", DbType.Date, IIf(IsDateTimeNull(EmpRetireLogRow.DownDate.Value), Convert.ToDateTime("1900/1/1"), EmpRetireLogRow.DownDate.Value))
            If EmpRetireLogRow.PayDate.Updated Then db.AddInParameter(dbcmd, "@PayDate", DbType.Int32, EmpRetireLogRow.PayDate.Value)
            If EmpRetireLogRow.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, EmpRetireLogRow.Remark.Value)
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(EmpRetireLogRow.LoadFromDataRow, EmpRetireLogRow.CompID.OldValue, EmpRetireLogRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(EmpRetireLogRow.LoadFromDataRow, EmpRetireLogRow.EmpID.OldValue, EmpRetireLogRow.EmpID.Value))
            db.AddInParameter(dbcmd, "@PKTransferType", DbType.String, IIf(EmpRetireLogRow.LoadFromDataRow, EmpRetireLogRow.TransferType.OldValue, EmpRetireLogRow.TransferType.Value))
            db.AddInParameter(dbcmd, "@PKLastChgDate", DbType.Date, IIf(EmpRetireLogRow.LoadFromDataRow, EmpRetireLogRow.LastChgDate.OldValue, EmpRetireLogRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal EmpRetireLogRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update EmpRetireLog Set")
            For i As Integer = 0 To EmpRetireLogRow.FieldNames.Length - 1
                If Not EmpRetireLogRow.IsIdentityField(EmpRetireLogRow.FieldNames(i)) AndAlso EmpRetireLogRow.IsUpdated(EmpRetireLogRow.FieldNames(i)) AndAlso EmpRetireLogRow.CreateUpdateSQL(EmpRetireLogRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, EmpRetireLogRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And EmpID = @PKEmpID")
            strSQL.AppendLine("And TransferType = @PKTransferType")
            strSQL.AppendLine("And LastChgDate = @PKLastChgDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If EmpRetireLogRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpRetireLogRow.CompID.Value)
            If EmpRetireLogRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpRetireLogRow.EmpID.Value)
            If EmpRetireLogRow.TransferType.Updated Then db.AddInParameter(dbcmd, "@TransferType", DbType.String, EmpRetireLogRow.TransferType.Value)
            If EmpRetireLogRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpRetireLogRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpRetireLogRow.LastChgDate.Value))
            If EmpRetireLogRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpRetireLogRow.LastChgComp.Value)
            If EmpRetireLogRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpRetireLogRow.LastChgID.Value)
            If EmpRetireLogRow.ApplyDate.Updated Then db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, IIf(IsDateTimeNull(EmpRetireLogRow.ApplyDate.Value), Convert.ToDateTime("1900/1/1"), EmpRetireLogRow.ApplyDate.Value))
            If EmpRetireLogRow.OldData.Updated Then db.AddInParameter(dbcmd, "@OldData", DbType.String, EmpRetireLogRow.OldData.Value)
            If EmpRetireLogRow.NewData.Updated Then db.AddInParameter(dbcmd, "@NewData", DbType.String, EmpRetireLogRow.NewData.Value)
            If EmpRetireLogRow.TransferDate.Updated Then db.AddInParameter(dbcmd, "@TransferDate", DbType.Date, IIf(IsDateTimeNull(EmpRetireLogRow.TransferDate.Value), Convert.ToDateTime("1900/1/1"), EmpRetireLogRow.TransferDate.Value))
            If EmpRetireLogRow.DeclareDate.Updated Then db.AddInParameter(dbcmd, "@DeclareDate", DbType.Date, IIf(IsDateTimeNull(EmpRetireLogRow.DeclareDate.Value), Convert.ToDateTime("1900/1/1"), EmpRetireLogRow.DeclareDate.Value))
            If EmpRetireLogRow.DownDate.Updated Then db.AddInParameter(dbcmd, "@DownDate", DbType.Date, IIf(IsDateTimeNull(EmpRetireLogRow.DownDate.Value), Convert.ToDateTime("1900/1/1"), EmpRetireLogRow.DownDate.Value))
            If EmpRetireLogRow.PayDate.Updated Then db.AddInParameter(dbcmd, "@PayDate", DbType.Int32, EmpRetireLogRow.PayDate.Value)
            If EmpRetireLogRow.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, EmpRetireLogRow.Remark.Value)
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(EmpRetireLogRow.LoadFromDataRow, EmpRetireLogRow.CompID.OldValue, EmpRetireLogRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(EmpRetireLogRow.LoadFromDataRow, EmpRetireLogRow.EmpID.OldValue, EmpRetireLogRow.EmpID.Value))
            db.AddInParameter(dbcmd, "@PKTransferType", DbType.String, IIf(EmpRetireLogRow.LoadFromDataRow, EmpRetireLogRow.TransferType.OldValue, EmpRetireLogRow.TransferType.Value))
            db.AddInParameter(dbcmd, "@PKLastChgDate", DbType.Date, IIf(EmpRetireLogRow.LoadFromDataRow, EmpRetireLogRow.LastChgDate.OldValue, EmpRetireLogRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal EmpRetireLogRow As Row()) As Integer
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
                    For Each r As Row In EmpRetireLogRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update EmpRetireLog Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where CompID = @PKCompID")
                        strSQL.AppendLine("And EmpID = @PKEmpID")
                        strSQL.AppendLine("And TransferType = @PKTransferType")
                        strSQL.AppendLine("And LastChgDate = @PKLastChgDate")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        If r.TransferType.Updated Then db.AddInParameter(dbcmd, "@TransferType", DbType.String, r.TransferType.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.ApplyDate.Updated Then db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, IIf(IsDateTimeNull(r.ApplyDate.Value), Convert.ToDateTime("1900/1/1"), r.ApplyDate.Value))
                        If r.OldData.Updated Then db.AddInParameter(dbcmd, "@OldData", DbType.String, r.OldData.Value)
                        If r.NewData.Updated Then db.AddInParameter(dbcmd, "@NewData", DbType.String, r.NewData.Value)
                        If r.TransferDate.Updated Then db.AddInParameter(dbcmd, "@TransferDate", DbType.Date, IIf(IsDateTimeNull(r.TransferDate.Value), Convert.ToDateTime("1900/1/1"), r.TransferDate.Value))
                        If r.DeclareDate.Updated Then db.AddInParameter(dbcmd, "@DeclareDate", DbType.Date, IIf(IsDateTimeNull(r.DeclareDate.Value), Convert.ToDateTime("1900/1/1"), r.DeclareDate.Value))
                        If r.DownDate.Updated Then db.AddInParameter(dbcmd, "@DownDate", DbType.Date, IIf(IsDateTimeNull(r.DownDate.Value), Convert.ToDateTime("1900/1/1"), r.DownDate.Value))
                        If r.PayDate.Updated Then db.AddInParameter(dbcmd, "@PayDate", DbType.Int32, r.PayDate.Value)
                        If r.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                        db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                        db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))
                        db.AddInParameter(dbcmd, "@PKTransferType", DbType.String, IIf(r.LoadFromDataRow, r.TransferType.OldValue, r.TransferType.Value))
                        db.AddInParameter(dbcmd, "@PKLastChgDate", DbType.Date, IIf(r.LoadFromDataRow, r.LastChgDate.OldValue, r.LastChgDate.Value))

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

        Public Function Update(ByVal EmpRetireLogRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In EmpRetireLogRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update EmpRetireLog Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where CompID = @PKCompID")
                strSQL.AppendLine("And EmpID = @PKEmpID")
                strSQL.AppendLine("And TransferType = @PKTransferType")
                strSQL.AppendLine("And LastChgDate = @PKLastChgDate")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                If r.TransferType.Updated Then db.AddInParameter(dbcmd, "@TransferType", DbType.String, r.TransferType.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.ApplyDate.Updated Then db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, IIf(IsDateTimeNull(r.ApplyDate.Value), Convert.ToDateTime("1900/1/1"), r.ApplyDate.Value))
                If r.OldData.Updated Then db.AddInParameter(dbcmd, "@OldData", DbType.String, r.OldData.Value)
                If r.NewData.Updated Then db.AddInParameter(dbcmd, "@NewData", DbType.String, r.NewData.Value)
                If r.TransferDate.Updated Then db.AddInParameter(dbcmd, "@TransferDate", DbType.Date, IIf(IsDateTimeNull(r.TransferDate.Value), Convert.ToDateTime("1900/1/1"), r.TransferDate.Value))
                If r.DeclareDate.Updated Then db.AddInParameter(dbcmd, "@DeclareDate", DbType.Date, IIf(IsDateTimeNull(r.DeclareDate.Value), Convert.ToDateTime("1900/1/1"), r.DeclareDate.Value))
                If r.DownDate.Updated Then db.AddInParameter(dbcmd, "@DownDate", DbType.Date, IIf(IsDateTimeNull(r.DownDate.Value), Convert.ToDateTime("1900/1/1"), r.DownDate.Value))
                If r.PayDate.Updated Then db.AddInParameter(dbcmd, "@PayDate", DbType.Int32, r.PayDate.Value)
                If r.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))
                db.AddInParameter(dbcmd, "@PKTransferType", DbType.String, IIf(r.LoadFromDataRow, r.TransferType.OldValue, r.TransferType.Value))
                db.AddInParameter(dbcmd, "@PKLastChgDate", DbType.Date, IIf(r.LoadFromDataRow, r.LastChgDate.OldValue, r.LastChgDate.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal EmpRetireLogRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From EmpRetireLog")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And TransferType = @TransferType")
            strSQL.AppendLine("And LastChgDate = @LastChgDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpRetireLogRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpRetireLogRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@TransferType", DbType.String, EmpRetireLogRow.TransferType.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, EmpRetireLogRow.LastChgDate.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal EmpRetireLogRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From EmpRetireLog")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And TransferType = @TransferType")
            strSQL.AppendLine("And LastChgDate = @LastChgDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpRetireLogRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpRetireLogRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@TransferType", DbType.String, EmpRetireLogRow.TransferType.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, EmpRetireLogRow.LastChgDate.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmpRetireLog")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal EmpRetireLogRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into EmpRetireLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, TransferType, LastChgDate, LastChgComp, LastChgID, ApplyDate, OldData,")
            strSQL.AppendLine("    NewData, TransferDate, DeclareDate, DownDate, PayDate, Remark")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @TransferType, @LastChgDate, @LastChgComp, @LastChgID, @ApplyDate, @OldData,")
            strSQL.AppendLine("    @NewData, @TransferDate, @DeclareDate, @DownDate, @PayDate, @Remark")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpRetireLogRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpRetireLogRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@TransferType", DbType.String, EmpRetireLogRow.TransferType.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpRetireLogRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpRetireLogRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpRetireLogRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpRetireLogRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, IIf(IsDateTimeNull(EmpRetireLogRow.ApplyDate.Value), Convert.ToDateTime("1900/1/1"), EmpRetireLogRow.ApplyDate.Value))
            db.AddInParameter(dbcmd, "@OldData", DbType.String, EmpRetireLogRow.OldData.Value)
            db.AddInParameter(dbcmd, "@NewData", DbType.String, EmpRetireLogRow.NewData.Value)
            db.AddInParameter(dbcmd, "@TransferDate", DbType.Date, IIf(IsDateTimeNull(EmpRetireLogRow.TransferDate.Value), Convert.ToDateTime("1900/1/1"), EmpRetireLogRow.TransferDate.Value))
            db.AddInParameter(dbcmd, "@DeclareDate", DbType.Date, IIf(IsDateTimeNull(EmpRetireLogRow.DeclareDate.Value), Convert.ToDateTime("1900/1/1"), EmpRetireLogRow.DeclareDate.Value))
            db.AddInParameter(dbcmd, "@DownDate", DbType.Date, IIf(IsDateTimeNull(EmpRetireLogRow.DownDate.Value), Convert.ToDateTime("1900/1/1"), EmpRetireLogRow.DownDate.Value))
            db.AddInParameter(dbcmd, "@PayDate", DbType.Int32, EmpRetireLogRow.PayDate.Value)
            db.AddInParameter(dbcmd, "@Remark", DbType.String, EmpRetireLogRow.Remark.Value)

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal EmpRetireLogRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into EmpRetireLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, TransferType, LastChgDate, LastChgComp, LastChgID, ApplyDate, OldData,")
            strSQL.AppendLine("    NewData, TransferDate, DeclareDate, DownDate, PayDate, Remark")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @TransferType, @LastChgDate, @LastChgComp, @LastChgID, @ApplyDate, @OldData,")
            strSQL.AppendLine("    @NewData, @TransferDate, @DeclareDate, @DownDate, @PayDate, @Remark")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpRetireLogRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpRetireLogRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@TransferType", DbType.String, EmpRetireLogRow.TransferType.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpRetireLogRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpRetireLogRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpRetireLogRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpRetireLogRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, IIf(IsDateTimeNull(EmpRetireLogRow.ApplyDate.Value), Convert.ToDateTime("1900/1/1"), EmpRetireLogRow.ApplyDate.Value))
            db.AddInParameter(dbcmd, "@OldData", DbType.String, EmpRetireLogRow.OldData.Value)
            db.AddInParameter(dbcmd, "@NewData", DbType.String, EmpRetireLogRow.NewData.Value)
            db.AddInParameter(dbcmd, "@TransferDate", DbType.Date, IIf(IsDateTimeNull(EmpRetireLogRow.TransferDate.Value), Convert.ToDateTime("1900/1/1"), EmpRetireLogRow.TransferDate.Value))
            db.AddInParameter(dbcmd, "@DeclareDate", DbType.Date, IIf(IsDateTimeNull(EmpRetireLogRow.DeclareDate.Value), Convert.ToDateTime("1900/1/1"), EmpRetireLogRow.DeclareDate.Value))
            db.AddInParameter(dbcmd, "@DownDate", DbType.Date, IIf(IsDateTimeNull(EmpRetireLogRow.DownDate.Value), Convert.ToDateTime("1900/1/1"), EmpRetireLogRow.DownDate.Value))
            db.AddInParameter(dbcmd, "@PayDate", DbType.Int32, EmpRetireLogRow.PayDate.Value)
            db.AddInParameter(dbcmd, "@Remark", DbType.String, EmpRetireLogRow.Remark.Value)

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal EmpRetireLogRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into EmpRetireLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, TransferType, LastChgDate, LastChgComp, LastChgID, ApplyDate, OldData,")
            strSQL.AppendLine("    NewData, TransferDate, DeclareDate, DownDate, PayDate, Remark")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @TransferType, @LastChgDate, @LastChgComp, @LastChgID, @ApplyDate, @OldData,")
            strSQL.AppendLine("    @NewData, @TransferDate, @DeclareDate, @DownDate, @PayDate, @Remark")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In EmpRetireLogRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@TransferType", DbType.String, r.TransferType.Value)
                        db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, IIf(IsDateTimeNull(r.ApplyDate.Value), Convert.ToDateTime("1900/1/1"), r.ApplyDate.Value))
                        db.AddInParameter(dbcmd, "@OldData", DbType.String, r.OldData.Value)
                        db.AddInParameter(dbcmd, "@NewData", DbType.String, r.NewData.Value)
                        db.AddInParameter(dbcmd, "@TransferDate", DbType.Date, IIf(IsDateTimeNull(r.TransferDate.Value), Convert.ToDateTime("1900/1/1"), r.TransferDate.Value))
                        db.AddInParameter(dbcmd, "@DeclareDate", DbType.Date, IIf(IsDateTimeNull(r.DeclareDate.Value), Convert.ToDateTime("1900/1/1"), r.DeclareDate.Value))
                        db.AddInParameter(dbcmd, "@DownDate", DbType.Date, IIf(IsDateTimeNull(r.DownDate.Value), Convert.ToDateTime("1900/1/1"), r.DownDate.Value))
                        db.AddInParameter(dbcmd, "@PayDate", DbType.Int32, r.PayDate.Value)
                        db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)

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

        Public Function Insert(ByVal EmpRetireLogRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into EmpRetireLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, TransferType, LastChgDate, LastChgComp, LastChgID, ApplyDate, OldData,")
            strSQL.AppendLine("    NewData, TransferDate, DeclareDate, DownDate, PayDate, Remark")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @TransferType, @LastChgDate, @LastChgComp, @LastChgID, @ApplyDate, @OldData,")
            strSQL.AppendLine("    @NewData, @TransferDate, @DeclareDate, @DownDate, @PayDate, @Remark")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In EmpRetireLogRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@TransferType", DbType.String, r.TransferType.Value)
                db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, IIf(IsDateTimeNull(r.ApplyDate.Value), Convert.ToDateTime("1900/1/1"), r.ApplyDate.Value))
                db.AddInParameter(dbcmd, "@OldData", DbType.String, r.OldData.Value)
                db.AddInParameter(dbcmd, "@NewData", DbType.String, r.NewData.Value)
                db.AddInParameter(dbcmd, "@TransferDate", DbType.Date, IIf(IsDateTimeNull(r.TransferDate.Value), Convert.ToDateTime("1900/1/1"), r.TransferDate.Value))
                db.AddInParameter(dbcmd, "@DeclareDate", DbType.Date, IIf(IsDateTimeNull(r.DeclareDate.Value), Convert.ToDateTime("1900/1/1"), r.DeclareDate.Value))
                db.AddInParameter(dbcmd, "@DownDate", DbType.Date, IIf(IsDateTimeNull(r.DownDate.Value), Convert.ToDateTime("1900/1/1"), r.DownDate.Value))
                db.AddInParameter(dbcmd, "@PayDate", DbType.Int32, r.PayDate.Value)
                db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)

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

