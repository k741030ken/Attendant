'****************************************************************
' Table:NotEmpLog
' Created Date: 2016.03.22
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beNotEmpLog
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "Seq", "CompID", "EmpID", "IDNo", "ValidDateB", "ValidDateE", "CompID_ALL", "AdjustReason", "PlusOrMinus", "NotEmpReasonID" _
                                    , "Remark", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(Integer), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(Date), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "Seq", "CompID", "EmpID" }

        Public ReadOnly Property Rows() As beNotEmpLog.Rows 
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
        Public Sub Transfer2Row(NotEmpLogTable As DataTable)
            For Each dr As DataRow In NotEmpLogTable.Rows
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

                dr(m_Rows(i).Seq.FieldName) = m_Rows(i).Seq.Value
                dr(m_Rows(i).CompID.FieldName) = m_Rows(i).CompID.Value
                dr(m_Rows(i).EmpID.FieldName) = m_Rows(i).EmpID.Value
                dr(m_Rows(i).IDNo.FieldName) = m_Rows(i).IDNo.Value
                dr(m_Rows(i).ValidDateB.FieldName) = m_Rows(i).ValidDateB.Value
                dr(m_Rows(i).ValidDateE.FieldName) = m_Rows(i).ValidDateE.Value
                dr(m_Rows(i).CompID_ALL.FieldName) = m_Rows(i).CompID_ALL.Value
                dr(m_Rows(i).AdjustReason.FieldName) = m_Rows(i).AdjustReason.Value
                dr(m_Rows(i).PlusOrMinus.FieldName) = m_Rows(i).PlusOrMinus.Value
                dr(m_Rows(i).NotEmpReasonID.FieldName) = m_Rows(i).NotEmpReasonID.Value
                dr(m_Rows(i).Remark.FieldName) = m_Rows(i).Remark.Value
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

        Public Sub Add(NotEmpLogRow As Row)
            m_Rows.Add(NotEmpLogRow)
        End Sub

        Public Sub Remove(NotEmpLogRow As Row)
            If m_Rows.IndexOf(NotEmpLogRow) >= 0 Then
                m_Rows.Remove(NotEmpLogRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_Seq As Field(Of Integer) = new Field(Of Integer)("Seq", true)
        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_EmpID As Field(Of String) = new Field(Of String)("EmpID", true)
        Private FI_IDNo As Field(Of String) = new Field(Of String)("IDNo", true)
        Private FI_ValidDateB As Field(Of Date) = new Field(Of Date)("ValidDateB", true)
        Private FI_ValidDateE As Field(Of Date) = new Field(Of Date)("ValidDateE", true)
        Private FI_CompID_ALL As Field(Of String) = new Field(Of String)("CompID_ALL", true)
        Private FI_AdjustReason As Field(Of String) = new Field(Of String)("AdjustReason", true)
        Private FI_PlusOrMinus As Field(Of String) = new Field(Of String)("PlusOrMinus", true)
        Private FI_NotEmpReasonID As Field(Of String) = new Field(Of String)("NotEmpReasonID", true)
        Private FI_Remark As Field(Of String) = new Field(Of String)("Remark", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "Seq", "CompID", "EmpID", "IDNo", "ValidDateB", "ValidDateE", "CompID_ALL", "AdjustReason", "PlusOrMinus", "NotEmpReasonID" _
                                    , "Remark", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "Seq", "CompID", "EmpID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "Seq"
                    Return FI_Seq.Value
                Case "CompID"
                    Return FI_CompID.Value
                Case "EmpID"
                    Return FI_EmpID.Value
                Case "IDNo"
                    Return FI_IDNo.Value
                Case "ValidDateB"
                    Return FI_ValidDateB.Value
                Case "ValidDateE"
                    Return FI_ValidDateE.Value
                Case "CompID_ALL"
                    Return FI_CompID_ALL.Value
                Case "AdjustReason"
                    Return FI_AdjustReason.Value
                Case "PlusOrMinus"
                    Return FI_PlusOrMinus.Value
                Case "NotEmpReasonID"
                    Return FI_NotEmpReasonID.Value
                Case "Remark"
                    Return FI_Remark.Value
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
                Case "Seq"
                    FI_Seq.SetValue(value)
                Case "CompID"
                    FI_CompID.SetValue(value)
                Case "EmpID"
                    FI_EmpID.SetValue(value)
                Case "IDNo"
                    FI_IDNo.SetValue(value)
                Case "ValidDateB"
                    FI_ValidDateB.SetValue(value)
                Case "ValidDateE"
                    FI_ValidDateE.SetValue(value)
                Case "CompID_ALL"
                    FI_CompID_ALL.SetValue(value)
                Case "AdjustReason"
                    FI_AdjustReason.SetValue(value)
                Case "PlusOrMinus"
                    FI_PlusOrMinus.SetValue(value)
                Case "NotEmpReasonID"
                    FI_NotEmpReasonID.SetValue(value)
                Case "Remark"
                    FI_Remark.SetValue(value)
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
                Case "Seq"
                    return FI_Seq.Updated
                Case "CompID"
                    return FI_CompID.Updated
                Case "EmpID"
                    return FI_EmpID.Updated
                Case "IDNo"
                    return FI_IDNo.Updated
                Case "ValidDateB"
                    return FI_ValidDateB.Updated
                Case "ValidDateE"
                    return FI_ValidDateE.Updated
                Case "CompID_ALL"
                    return FI_CompID_ALL.Updated
                Case "AdjustReason"
                    return FI_AdjustReason.Updated
                Case "PlusOrMinus"
                    return FI_PlusOrMinus.Updated
                Case "NotEmpReasonID"
                    return FI_NotEmpReasonID.Updated
                Case "Remark"
                    return FI_Remark.Updated
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
                Case "Seq"
                    return FI_Seq.CreateUpdateSQL
                Case "CompID"
                    return FI_CompID.CreateUpdateSQL
                Case "EmpID"
                    return FI_EmpID.CreateUpdateSQL
                Case "IDNo"
                    return FI_IDNo.CreateUpdateSQL
                Case "ValidDateB"
                    return FI_ValidDateB.CreateUpdateSQL
                Case "ValidDateE"
                    return FI_ValidDateE.CreateUpdateSQL
                Case "CompID_ALL"
                    return FI_CompID_ALL.CreateUpdateSQL
                Case "AdjustReason"
                    return FI_AdjustReason.CreateUpdateSQL
                Case "PlusOrMinus"
                    return FI_PlusOrMinus.CreateUpdateSQL
                Case "NotEmpReasonID"
                    return FI_NotEmpReasonID.CreateUpdateSQL
                Case "Remark"
                    return FI_Remark.CreateUpdateSQL
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
            FI_Seq.SetInitValue(0)
            FI_CompID.SetInitValue("")
            FI_EmpID.SetInitValue("")
            FI_IDNo.SetInitValue("")
            FI_ValidDateB.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_ValidDateE.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_CompID_ALL.SetInitValue("")
            FI_AdjustReason.SetInitValue("")
            FI_PlusOrMinus.SetInitValue("")
            FI_NotEmpReasonID.SetInitValue("")
            FI_Remark.SetInitValue("")
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_Seq.SetInitValue(dr("Seq"))
            FI_CompID.SetInitValue(dr("CompID"))
            FI_EmpID.SetInitValue(dr("EmpID"))
            FI_IDNo.SetInitValue(dr("IDNo"))
            FI_ValidDateB.SetInitValue(dr("ValidDateB"))
            FI_ValidDateE.SetInitValue(dr("ValidDateE"))
            FI_CompID_ALL.SetInitValue(dr("CompID_ALL"))
            FI_AdjustReason.SetInitValue(dr("AdjustReason"))
            FI_PlusOrMinus.SetInitValue(dr("PlusOrMinus"))
            FI_NotEmpReasonID.SetInitValue(dr("NotEmpReasonID"))
            FI_Remark.SetInitValue(dr("Remark"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_Seq.Updated = False
            FI_CompID.Updated = False
            FI_EmpID.Updated = False
            FI_IDNo.Updated = False
            FI_ValidDateB.Updated = False
            FI_ValidDateE.Updated = False
            FI_CompID_ALL.Updated = False
            FI_AdjustReason.Updated = False
            FI_PlusOrMinus.Updated = False
            FI_NotEmpReasonID.Updated = False
            FI_Remark.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
        End Sub

        Public ReadOnly Property Seq As Field(Of Integer) 
            Get
                Return FI_Seq
            End Get
        End Property

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

        Public ReadOnly Property IDNo As Field(Of String) 
            Get
                Return FI_IDNo
            End Get
        End Property

        Public ReadOnly Property ValidDateB As Field(Of Date) 
            Get
                Return FI_ValidDateB
            End Get
        End Property

        Public ReadOnly Property ValidDateE As Field(Of Date) 
            Get
                Return FI_ValidDateE
            End Get
        End Property

        Public ReadOnly Property CompID_ALL As Field(Of String) 
            Get
                Return FI_CompID_ALL
            End Get
        End Property

        Public ReadOnly Property AdjustReason As Field(Of String) 
            Get
                Return FI_AdjustReason
            End Get
        End Property

        Public ReadOnly Property PlusOrMinus As Field(Of String) 
            Get
                Return FI_PlusOrMinus
            End Get
        End Property

        Public ReadOnly Property NotEmpReasonID As Field(Of String) 
            Get
                Return FI_NotEmpReasonID
            End Get
        End Property

        Public ReadOnly Property Remark As Field(Of String) 
            Get
                Return FI_Remark
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
        Public Function DeleteRowByPrimaryKey(ByVal NotEmpLogRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From NotEmpLog")
            strSQL.AppendLine("Where Seq = @Seq")
            strSQL.AppendLine("And CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, NotEmpLogRow.Seq.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, NotEmpLogRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, NotEmpLogRow.EmpID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal NotEmpLogRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From NotEmpLog")
            strSQL.AppendLine("Where Seq = @Seq")
            strSQL.AppendLine("And CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, NotEmpLogRow.Seq.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, NotEmpLogRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, NotEmpLogRow.EmpID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal NotEmpLogRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From NotEmpLog")
            strSQL.AppendLine("Where Seq = @Seq")
            strSQL.AppendLine("And CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In NotEmpLogRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
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

        Public Function DeleteRowByPrimaryKey(ByVal NotEmpLogRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From NotEmpLog")
            strSQL.AppendLine("Where Seq = @Seq")
            strSQL.AppendLine("And CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In NotEmpLogRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal NotEmpLogRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From NotEmpLog")
            strSQL.AppendLine("Where Seq = @Seq")
            strSQL.AppendLine("And CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, NotEmpLogRow.Seq.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, NotEmpLogRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, NotEmpLogRow.EmpID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(NotEmpLogRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From NotEmpLog")
            strSQL.AppendLine("Where Seq = @Seq")
            strSQL.AppendLine("And CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, NotEmpLogRow.Seq.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, NotEmpLogRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, NotEmpLogRow.EmpID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal NotEmpLogRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update NotEmpLog Set")
            For i As Integer = 0 To NotEmpLogRow.FieldNames.Length - 1
                If Not NotEmpLogRow.IsIdentityField(NotEmpLogRow.FieldNames(i)) AndAlso NotEmpLogRow.IsUpdated(NotEmpLogRow.FieldNames(i)) AndAlso NotEmpLogRow.CreateUpdateSQL(NotEmpLogRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, NotEmpLogRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where Seq = @PKSeq")
            strSQL.AppendLine("And CompID = @PKCompID")
            strSQL.AppendLine("And EmpID = @PKEmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If NotEmpLogRow.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, NotEmpLogRow.Seq.Value)
            If NotEmpLogRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, NotEmpLogRow.CompID.Value)
            If NotEmpLogRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, NotEmpLogRow.EmpID.Value)
            If NotEmpLogRow.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, NotEmpLogRow.IDNo.Value)
            If NotEmpLogRow.ValidDateB.Updated Then db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(NotEmpLogRow.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), NotEmpLogRow.ValidDateB.Value))
            If NotEmpLogRow.ValidDateE.Updated Then db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(NotEmpLogRow.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), NotEmpLogRow.ValidDateE.Value))
            If NotEmpLogRow.CompID_ALL.Updated Then db.AddInParameter(dbcmd, "@CompID_ALL", DbType.String, NotEmpLogRow.CompID_ALL.Value)
            If NotEmpLogRow.AdjustReason.Updated Then db.AddInParameter(dbcmd, "@AdjustReason", DbType.String, NotEmpLogRow.AdjustReason.Value)
            If NotEmpLogRow.PlusOrMinus.Updated Then db.AddInParameter(dbcmd, "@PlusOrMinus", DbType.String, NotEmpLogRow.PlusOrMinus.Value)
            If NotEmpLogRow.NotEmpReasonID.Updated Then db.AddInParameter(dbcmd, "@NotEmpReasonID", DbType.String, NotEmpLogRow.NotEmpReasonID.Value)
            If NotEmpLogRow.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, NotEmpLogRow.Remark.Value)
            If NotEmpLogRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, NotEmpLogRow.LastChgComp.Value)
            If NotEmpLogRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, NotEmpLogRow.LastChgID.Value)
            If NotEmpLogRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(NotEmpLogRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), NotEmpLogRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKSeq", DbType.Int32, IIf(NotEmpLogRow.LoadFromDataRow, NotEmpLogRow.Seq.OldValue, NotEmpLogRow.Seq.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(NotEmpLogRow.LoadFromDataRow, NotEmpLogRow.CompID.OldValue, NotEmpLogRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(NotEmpLogRow.LoadFromDataRow, NotEmpLogRow.EmpID.OldValue, NotEmpLogRow.EmpID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal NotEmpLogRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update NotEmpLog Set")
            For i As Integer = 0 To NotEmpLogRow.FieldNames.Length - 1
                If Not NotEmpLogRow.IsIdentityField(NotEmpLogRow.FieldNames(i)) AndAlso NotEmpLogRow.IsUpdated(NotEmpLogRow.FieldNames(i)) AndAlso NotEmpLogRow.CreateUpdateSQL(NotEmpLogRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, NotEmpLogRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where Seq = @PKSeq")
            strSQL.AppendLine("And CompID = @PKCompID")
            strSQL.AppendLine("And EmpID = @PKEmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If NotEmpLogRow.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, NotEmpLogRow.Seq.Value)
            If NotEmpLogRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, NotEmpLogRow.CompID.Value)
            If NotEmpLogRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, NotEmpLogRow.EmpID.Value)
            If NotEmpLogRow.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, NotEmpLogRow.IDNo.Value)
            If NotEmpLogRow.ValidDateB.Updated Then db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(NotEmpLogRow.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), NotEmpLogRow.ValidDateB.Value))
            If NotEmpLogRow.ValidDateE.Updated Then db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(NotEmpLogRow.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), NotEmpLogRow.ValidDateE.Value))
            If NotEmpLogRow.CompID_ALL.Updated Then db.AddInParameter(dbcmd, "@CompID_ALL", DbType.String, NotEmpLogRow.CompID_ALL.Value)
            If NotEmpLogRow.AdjustReason.Updated Then db.AddInParameter(dbcmd, "@AdjustReason", DbType.String, NotEmpLogRow.AdjustReason.Value)
            If NotEmpLogRow.PlusOrMinus.Updated Then db.AddInParameter(dbcmd, "@PlusOrMinus", DbType.String, NotEmpLogRow.PlusOrMinus.Value)
            If NotEmpLogRow.NotEmpReasonID.Updated Then db.AddInParameter(dbcmd, "@NotEmpReasonID", DbType.String, NotEmpLogRow.NotEmpReasonID.Value)
            If NotEmpLogRow.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, NotEmpLogRow.Remark.Value)
            If NotEmpLogRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, NotEmpLogRow.LastChgComp.Value)
            If NotEmpLogRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, NotEmpLogRow.LastChgID.Value)
            If NotEmpLogRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(NotEmpLogRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), NotEmpLogRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKSeq", DbType.Int32, IIf(NotEmpLogRow.LoadFromDataRow, NotEmpLogRow.Seq.OldValue, NotEmpLogRow.Seq.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(NotEmpLogRow.LoadFromDataRow, NotEmpLogRow.CompID.OldValue, NotEmpLogRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(NotEmpLogRow.LoadFromDataRow, NotEmpLogRow.EmpID.OldValue, NotEmpLogRow.EmpID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal NotEmpLogRow As Row()) As Integer
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
                    For Each r As Row In NotEmpLogRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update NotEmpLog Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where Seq = @PKSeq")
                        strSQL.AppendLine("And CompID = @PKCompID")
                        strSQL.AppendLine("And EmpID = @PKEmpID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        If r.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                        If r.ValidDateB.Updated Then db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(r.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateB.Value))
                        If r.ValidDateE.Updated Then db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(r.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateE.Value))
                        If r.CompID_ALL.Updated Then db.AddInParameter(dbcmd, "@CompID_ALL", DbType.String, r.CompID_ALL.Value)
                        If r.AdjustReason.Updated Then db.AddInParameter(dbcmd, "@AdjustReason", DbType.String, r.AdjustReason.Value)
                        If r.PlusOrMinus.Updated Then db.AddInParameter(dbcmd, "@PlusOrMinus", DbType.String, r.PlusOrMinus.Value)
                        If r.NotEmpReasonID.Updated Then db.AddInParameter(dbcmd, "@NotEmpReasonID", DbType.String, r.NotEmpReasonID.Value)
                        If r.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKSeq", DbType.Int32, IIf(r.LoadFromDataRow, r.Seq.OldValue, r.Seq.Value))
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

        Public Function Update(ByVal NotEmpLogRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In NotEmpLogRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update NotEmpLog Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where Seq = @PKSeq")
                strSQL.AppendLine("And CompID = @PKCompID")
                strSQL.AppendLine("And EmpID = @PKEmpID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                If r.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                If r.ValidDateB.Updated Then db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(r.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateB.Value))
                If r.ValidDateE.Updated Then db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(r.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateE.Value))
                If r.CompID_ALL.Updated Then db.AddInParameter(dbcmd, "@CompID_ALL", DbType.String, r.CompID_ALL.Value)
                If r.AdjustReason.Updated Then db.AddInParameter(dbcmd, "@AdjustReason", DbType.String, r.AdjustReason.Value)
                If r.PlusOrMinus.Updated Then db.AddInParameter(dbcmd, "@PlusOrMinus", DbType.String, r.PlusOrMinus.Value)
                If r.NotEmpReasonID.Updated Then db.AddInParameter(dbcmd, "@NotEmpReasonID", DbType.String, r.NotEmpReasonID.Value)
                If r.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKSeq", DbType.Int32, IIf(r.LoadFromDataRow, r.Seq.OldValue, r.Seq.Value))
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal NotEmpLogRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From NotEmpLog")
            strSQL.AppendLine("Where Seq = @Seq")
            strSQL.AppendLine("And CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, NotEmpLogRow.Seq.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, NotEmpLogRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, NotEmpLogRow.EmpID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal NotEmpLogRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From NotEmpLog")
            strSQL.AppendLine("Where Seq = @Seq")
            strSQL.AppendLine("And CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, NotEmpLogRow.Seq.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, NotEmpLogRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, NotEmpLogRow.EmpID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From NotEmpLog")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal NotEmpLogRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into NotEmpLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    Seq, CompID, EmpID, IDNo, ValidDateB, ValidDateE, CompID_ALL, AdjustReason, PlusOrMinus,")
            strSQL.AppendLine("    NotEmpReasonID, Remark, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @Seq, @CompID, @EmpID, @IDNo, @ValidDateB, @ValidDateE, @CompID_ALL, @AdjustReason, @PlusOrMinus,")
            strSQL.AppendLine("    @NotEmpReasonID, @Remark, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, NotEmpLogRow.Seq.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, NotEmpLogRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, NotEmpLogRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, NotEmpLogRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(NotEmpLogRow.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), NotEmpLogRow.ValidDateB.Value))
            db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(NotEmpLogRow.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), NotEmpLogRow.ValidDateE.Value))
            db.AddInParameter(dbcmd, "@CompID_ALL", DbType.String, NotEmpLogRow.CompID_ALL.Value)
            db.AddInParameter(dbcmd, "@AdjustReason", DbType.String, NotEmpLogRow.AdjustReason.Value)
            db.AddInParameter(dbcmd, "@PlusOrMinus", DbType.String, NotEmpLogRow.PlusOrMinus.Value)
            db.AddInParameter(dbcmd, "@NotEmpReasonID", DbType.String, NotEmpLogRow.NotEmpReasonID.Value)
            db.AddInParameter(dbcmd, "@Remark", DbType.String, NotEmpLogRow.Remark.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, NotEmpLogRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, NotEmpLogRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(NotEmpLogRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), NotEmpLogRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal NotEmpLogRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into NotEmpLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    Seq, CompID, EmpID, IDNo, ValidDateB, ValidDateE, CompID_ALL, AdjustReason, PlusOrMinus,")
            strSQL.AppendLine("    NotEmpReasonID, Remark, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @Seq, @CompID, @EmpID, @IDNo, @ValidDateB, @ValidDateE, @CompID_ALL, @AdjustReason, @PlusOrMinus,")
            strSQL.AppendLine("    @NotEmpReasonID, @Remark, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, NotEmpLogRow.Seq.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, NotEmpLogRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, NotEmpLogRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, NotEmpLogRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(NotEmpLogRow.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), NotEmpLogRow.ValidDateB.Value))
            db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(NotEmpLogRow.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), NotEmpLogRow.ValidDateE.Value))
            db.AddInParameter(dbcmd, "@CompID_ALL", DbType.String, NotEmpLogRow.CompID_ALL.Value)
            db.AddInParameter(dbcmd, "@AdjustReason", DbType.String, NotEmpLogRow.AdjustReason.Value)
            db.AddInParameter(dbcmd, "@PlusOrMinus", DbType.String, NotEmpLogRow.PlusOrMinus.Value)
            db.AddInParameter(dbcmd, "@NotEmpReasonID", DbType.String, NotEmpLogRow.NotEmpReasonID.Value)
            db.AddInParameter(dbcmd, "@Remark", DbType.String, NotEmpLogRow.Remark.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, NotEmpLogRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, NotEmpLogRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(NotEmpLogRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), NotEmpLogRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal NotEmpLogRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into NotEmpLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    Seq, CompID, EmpID, IDNo, ValidDateB, ValidDateE, CompID_ALL, AdjustReason, PlusOrMinus,")
            strSQL.AppendLine("    NotEmpReasonID, Remark, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @Seq, @CompID, @EmpID, @IDNo, @ValidDateB, @ValidDateE, @CompID_ALL, @AdjustReason, @PlusOrMinus,")
            strSQL.AppendLine("    @NotEmpReasonID, @Remark, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In NotEmpLogRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                        db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(r.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateB.Value))
                        db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(r.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateE.Value))
                        db.AddInParameter(dbcmd, "@CompID_ALL", DbType.String, r.CompID_ALL.Value)
                        db.AddInParameter(dbcmd, "@AdjustReason", DbType.String, r.AdjustReason.Value)
                        db.AddInParameter(dbcmd, "@PlusOrMinus", DbType.String, r.PlusOrMinus.Value)
                        db.AddInParameter(dbcmd, "@NotEmpReasonID", DbType.String, r.NotEmpReasonID.Value)
                        db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
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

        Public Function Insert(ByVal NotEmpLogRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into NotEmpLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    Seq, CompID, EmpID, IDNo, ValidDateB, ValidDateE, CompID_ALL, AdjustReason, PlusOrMinus,")
            strSQL.AppendLine("    NotEmpReasonID, Remark, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @Seq, @CompID, @EmpID, @IDNo, @ValidDateB, @ValidDateE, @CompID_ALL, @AdjustReason, @PlusOrMinus,")
            strSQL.AppendLine("    @NotEmpReasonID, @Remark, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In NotEmpLogRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                db.AddInParameter(dbcmd, "@ValidDateB", DbType.Date, IIf(IsDateTimeNull(r.ValidDateB.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateB.Value))
                db.AddInParameter(dbcmd, "@ValidDateE", DbType.Date, IIf(IsDateTimeNull(r.ValidDateE.Value), Convert.ToDateTime("1900/1/1"), r.ValidDateE.Value))
                db.AddInParameter(dbcmd, "@CompID_ALL", DbType.String, r.CompID_ALL.Value)
                db.AddInParameter(dbcmd, "@AdjustReason", DbType.String, r.AdjustReason.Value)
                db.AddInParameter(dbcmd, "@PlusOrMinus", DbType.String, r.PlusOrMinus.Value)
                db.AddInParameter(dbcmd, "@NotEmpReasonID", DbType.String, r.NotEmpReasonID.Value)
                db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
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

