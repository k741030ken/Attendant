'****************************************************************
' Table:SC_Common
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

Namespace beSC_Common
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "Type", "Code", "Define", "ValidFlag", "OrderSeq", "Note", "CreateDate", "LastChgID", "LastChgDate", "RsvCol1" _
                                    , "RsvCol2", "RsvCol3", "RsvCol4", "RsvCol5" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(Integer), GetType(String), GetType(Date), GetType(String), GetType(Date), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String) }
        Private m_PrimaryFields As String() = { "Type", "Code" }

        Public ReadOnly Property Rows() As beSC_Common.Rows 
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
        Public Sub Transfer2Row(SC_CommonTable As DataTable)
            For Each dr As DataRow In SC_CommonTable.Rows
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

                dr(m_Rows(i).Type.FieldName) = m_Rows(i).Type.Value
                dr(m_Rows(i).Code.FieldName) = m_Rows(i).Code.Value
                dr(m_Rows(i).Define.FieldName) = m_Rows(i).Define.Value
                dr(m_Rows(i).ValidFlag.FieldName) = m_Rows(i).ValidFlag.Value
                dr(m_Rows(i).OrderSeq.FieldName) = m_Rows(i).OrderSeq.Value
                dr(m_Rows(i).Note.FieldName) = m_Rows(i).Note.Value
                dr(m_Rows(i).CreateDate.FieldName) = m_Rows(i).CreateDate.Value
                dr(m_Rows(i).LastChgID.FieldName) = m_Rows(i).LastChgID.Value
                dr(m_Rows(i).LastChgDate.FieldName) = m_Rows(i).LastChgDate.Value
                dr(m_Rows(i).RsvCol1.FieldName) = m_Rows(i).RsvCol1.Value
                dr(m_Rows(i).RsvCol2.FieldName) = m_Rows(i).RsvCol2.Value
                dr(m_Rows(i).RsvCol3.FieldName) = m_Rows(i).RsvCol3.Value
                dr(m_Rows(i).RsvCol4.FieldName) = m_Rows(i).RsvCol4.Value
                dr(m_Rows(i).RsvCol5.FieldName) = m_Rows(i).RsvCol5.Value

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

        Public Sub Add(SC_CommonRow As Row)
            m_Rows.Add(SC_CommonRow)
        End Sub

        Public Sub Remove(SC_CommonRow As Row)
            If m_Rows.IndexOf(SC_CommonRow) >= 0 Then
                m_Rows.Remove(SC_CommonRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_Type As Field(Of String) = new Field(Of String)("Type", true)
        Private FI_Code As Field(Of String) = new Field(Of String)("Code", true)
        Private FI_Define As Field(Of String) = new Field(Of String)("Define", true)
        Private FI_ValidFlag As Field(Of String) = new Field(Of String)("ValidFlag", true)
        Private FI_OrderSeq As Field(Of Integer) = new Field(Of Integer)("OrderSeq", true)
        Private FI_Note As Field(Of String) = new Field(Of String)("Note", true)
        Private FI_CreateDate As Field(Of Date) = new Field(Of Date)("CreateDate", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private FI_RsvCol1 As Field(Of String) = new Field(Of String)("RsvCol1", true)
        Private FI_RsvCol2 As Field(Of String) = new Field(Of String)("RsvCol2", true)
        Private FI_RsvCol3 As Field(Of String) = new Field(Of String)("RsvCol3", true)
        Private FI_RsvCol4 As Field(Of String) = new Field(Of String)("RsvCol4", true)
        Private FI_RsvCol5 As Field(Of String) = new Field(Of String)("RsvCol5", true)
        Private m_FieldNames As String() = { "Type", "Code", "Define", "ValidFlag", "OrderSeq", "Note", "CreateDate", "LastChgID", "LastChgDate", "RsvCol1" _
                                    , "RsvCol2", "RsvCol3", "RsvCol4", "RsvCol5" }
        Private m_PrimaryFields As String() = { "Type", "Code" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "Type"
                    Return FI_Type.Value
                Case "Code"
                    Return FI_Code.Value
                Case "Define"
                    Return FI_Define.Value
                Case "ValidFlag"
                    Return FI_ValidFlag.Value
                Case "OrderSeq"
                    Return FI_OrderSeq.Value
                Case "Note"
                    Return FI_Note.Value
                Case "CreateDate"
                    Return FI_CreateDate.Value
                Case "LastChgID"
                    Return FI_LastChgID.Value
                Case "LastChgDate"
                    Return FI_LastChgDate.Value
                Case "RsvCol1"
                    Return FI_RsvCol1.Value
                Case "RsvCol2"
                    Return FI_RsvCol2.Value
                Case "RsvCol3"
                    Return FI_RsvCol3.Value
                Case "RsvCol4"
                    Return FI_RsvCol4.Value
                Case "RsvCol5"
                    Return FI_RsvCol5.Value
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Sub SetFieldValue(ByVal fieldName As String, ByVal value As Object)
            Select Case fieldName
                Case "Type"
                    FI_Type.SetValue(value)
                Case "Code"
                    FI_Code.SetValue(value)
                Case "Define"
                    FI_Define.SetValue(value)
                Case "ValidFlag"
                    FI_ValidFlag.SetValue(value)
                Case "OrderSeq"
                    FI_OrderSeq.SetValue(value)
                Case "Note"
                    FI_Note.SetValue(value)
                Case "CreateDate"
                    FI_CreateDate.SetValue(value)
                Case "LastChgID"
                    FI_LastChgID.SetValue(value)
                Case "LastChgDate"
                    FI_LastChgDate.SetValue(value)
                Case "RsvCol1"
                    FI_RsvCol1.SetValue(value)
                Case "RsvCol2"
                    FI_RsvCol2.SetValue(value)
                Case "RsvCol3"
                    FI_RsvCol3.SetValue(value)
                Case "RsvCol4"
                    FI_RsvCol4.SetValue(value)
                Case "RsvCol5"
                    FI_RsvCol5.SetValue(value)
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
                Case "Type"
                    return FI_Type.Updated
                Case "Code"
                    return FI_Code.Updated
                Case "Define"
                    return FI_Define.Updated
                Case "ValidFlag"
                    return FI_ValidFlag.Updated
                Case "OrderSeq"
                    return FI_OrderSeq.Updated
                Case "Note"
                    return FI_Note.Updated
                Case "CreateDate"
                    return FI_CreateDate.Updated
                Case "LastChgID"
                    return FI_LastChgID.Updated
                Case "LastChgDate"
                    return FI_LastChgDate.Updated
                Case "RsvCol1"
                    return FI_RsvCol1.Updated
                Case "RsvCol2"
                    return FI_RsvCol2.Updated
                Case "RsvCol3"
                    return FI_RsvCol3.Updated
                Case "RsvCol4"
                    return FI_RsvCol4.Updated
                Case "RsvCol5"
                    return FI_RsvCol5.Updated
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public Function CreateUpdateSQL(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "Type"
                    return FI_Type.CreateUpdateSQL
                Case "Code"
                    return FI_Code.CreateUpdateSQL
                Case "Define"
                    return FI_Define.CreateUpdateSQL
                Case "ValidFlag"
                    return FI_ValidFlag.CreateUpdateSQL
                Case "OrderSeq"
                    return FI_OrderSeq.CreateUpdateSQL
                Case "Note"
                    return FI_Note.CreateUpdateSQL
                Case "CreateDate"
                    return FI_CreateDate.CreateUpdateSQL
                Case "LastChgID"
                    return FI_LastChgID.CreateUpdateSQL
                Case "LastChgDate"
                    return FI_LastChgDate.CreateUpdateSQL
                Case "RsvCol1"
                    return FI_RsvCol1.CreateUpdateSQL
                Case "RsvCol2"
                    return FI_RsvCol2.CreateUpdateSQL
                Case "RsvCol3"
                    return FI_RsvCol3.CreateUpdateSQL
                Case "RsvCol4"
                    return FI_RsvCol4.CreateUpdateSQL
                Case "RsvCol5"
                    return FI_RsvCol5.CreateUpdateSQL
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
            FI_Type.SetInitValue("")
            FI_Code.SetInitValue("")
            FI_Define.SetInitValue("")
            FI_ValidFlag.SetInitValue("")
            FI_OrderSeq.SetInitValue(0)
            FI_Note.SetInitValue("")
            FI_CreateDate.SetInitValue(DateTime.Now)
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(DateTime.Now)
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_Type.SetInitValue(dr("Type"))
            FI_Code.SetInitValue(dr("Code"))
            FI_Define.SetInitValue(dr("Define"))
            FI_ValidFlag.SetInitValue(dr("ValidFlag"))
            FI_OrderSeq.SetInitValue(dr("OrderSeq"))
            FI_Note.SetInitValue(dr("Note"))
            FI_CreateDate.SetInitValue(dr("CreateDate"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))
            FI_RsvCol1.SetInitValue(dr("RsvCol1"))
            FI_RsvCol2.SetInitValue(dr("RsvCol2"))
            FI_RsvCol3.SetInitValue(dr("RsvCol3"))
            FI_RsvCol4.SetInitValue(dr("RsvCol4"))
            FI_RsvCol5.SetInitValue(dr("RsvCol5"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_Type.Updated = False
            FI_Code.Updated = False
            FI_Define.Updated = False
            FI_ValidFlag.Updated = False
            FI_OrderSeq.Updated = False
            FI_Note.Updated = False
            FI_CreateDate.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
            FI_RsvCol1.Updated = False
            FI_RsvCol2.Updated = False
            FI_RsvCol3.Updated = False
            FI_RsvCol4.Updated = False
            FI_RsvCol5.Updated = False
        End Sub

        Public ReadOnly Property Type As Field(Of String) 
            Get
                Return FI_Type
            End Get
        End Property

        Public ReadOnly Property Code As Field(Of String) 
            Get
                Return FI_Code
            End Get
        End Property

        Public ReadOnly Property Define As Field(Of String) 
            Get
                Return FI_Define
            End Get
        End Property

        Public ReadOnly Property ValidFlag As Field(Of String) 
            Get
                Return FI_ValidFlag
            End Get
        End Property

        Public ReadOnly Property OrderSeq As Field(Of Integer) 
            Get
                Return FI_OrderSeq
            End Get
        End Property

        Public ReadOnly Property Note As Field(Of String) 
            Get
                Return FI_Note
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

        Public ReadOnly Property RsvCol1 As Field(Of String) 
            Get
                Return FI_RsvCol1
            End Get
        End Property

        Public ReadOnly Property RsvCol2 As Field(Of String) 
            Get
                Return FI_RsvCol2
            End Get
        End Property

        Public ReadOnly Property RsvCol3 As Field(Of String) 
            Get
                Return FI_RsvCol3
            End Get
        End Property

        Public ReadOnly Property RsvCol4 As Field(Of String) 
            Get
                Return FI_RsvCol4
            End Get
        End Property

        Public ReadOnly Property RsvCol5 As Field(Of String) 
            Get
                Return FI_RsvCol5
            End Get
        End Property

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal SC_CommonRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_Common")
            strSQL.AppendLine("Where Type = @Type")
            strSQL.AppendLine("And Code = @Code")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@Type", DbType.String, SC_CommonRow.Type.Value)
            db.AddInParameter(dbcmd, "@Code", DbType.String, SC_CommonRow.Code.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal SC_CommonRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_Common")
            strSQL.AppendLine("Where Type = @Type")
            strSQL.AppendLine("And Code = @Code")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@Type", DbType.String, SC_CommonRow.Type.Value)
            db.AddInParameter(dbcmd, "@Code", DbType.String, SC_CommonRow.Code.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal SC_CommonRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_Common")
            strSQL.AppendLine("Where Type = @Type")
            strSQL.AppendLine("And Code = @Code")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_CommonRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@Type", DbType.String, r.Type.Value)
                        db.AddInParameter(dbcmd, "@Code", DbType.String, r.Code.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal SC_CommonRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_Common")
            strSQL.AppendLine("Where Type = @Type")
            strSQL.AppendLine("And Code = @Code")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_CommonRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@Type", DbType.String, r.Type.Value)
                db.AddInParameter(dbcmd, "@Code", DbType.String, r.Code.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal SC_CommonRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_Common")
            strSQL.AppendLine("Where Type = @Type")
            strSQL.AppendLine("And Code = @Code")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@Type", DbType.String, SC_CommonRow.Type.Value)
            db.AddInParameter(dbcmd, "@Code", DbType.String, SC_CommonRow.Code.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(SC_CommonRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_Common")
            strSQL.AppendLine("Where Type = @Type")
            strSQL.AppendLine("And Code = @Code")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@Type", DbType.String, SC_CommonRow.Type.Value)
            db.AddInParameter(dbcmd, "@Code", DbType.String, SC_CommonRow.Code.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_CommonRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_Common Set")
            For i As Integer = 0 To SC_CommonRow.FieldNames.Length - 1
                If Not SC_CommonRow.IsIdentityField(SC_CommonRow.FieldNames(i)) AndAlso SC_CommonRow.IsUpdated(SC_CommonRow.FieldNames(i)) AndAlso SC_CommonRow.CreateUpdateSQL(SC_CommonRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_CommonRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where Type = @PKType")
            strSQL.AppendLine("And Code = @PKCode")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_CommonRow.Type.Updated Then db.AddInParameter(dbcmd, "@Type", DbType.String, SC_CommonRow.Type.Value)
            If SC_CommonRow.Code.Updated Then db.AddInParameter(dbcmd, "@Code", DbType.String, SC_CommonRow.Code.Value)
            If SC_CommonRow.Define.Updated Then db.AddInParameter(dbcmd, "@Define", DbType.String, SC_CommonRow.Define.Value)
            If SC_CommonRow.ValidFlag.Updated Then db.AddInParameter(dbcmd, "@ValidFlag", DbType.String, SC_CommonRow.ValidFlag.Value)
            If SC_CommonRow.OrderSeq.Updated Then db.AddInParameter(dbcmd, "@OrderSeq", DbType.Int32, SC_CommonRow.OrderSeq.Value)
            If SC_CommonRow.Note.Updated Then db.AddInParameter(dbcmd, "@Note", DbType.String, SC_CommonRow.Note.Value)
            If SC_CommonRow.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_CommonRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_CommonRow.CreateDate.Value))
            If SC_CommonRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_CommonRow.LastChgID.Value)
            If SC_CommonRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_CommonRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_CommonRow.LastChgDate.Value))
            If SC_CommonRow.RsvCol1.Updated Then db.AddInParameter(dbcmd, "@RsvCol1", DbType.String, SC_CommonRow.RsvCol1.Value)
            If SC_CommonRow.RsvCol2.Updated Then db.AddInParameter(dbcmd, "@RsvCol2", DbType.String, SC_CommonRow.RsvCol2.Value)
            If SC_CommonRow.RsvCol3.Updated Then db.AddInParameter(dbcmd, "@RsvCol3", DbType.String, SC_CommonRow.RsvCol3.Value)
            If SC_CommonRow.RsvCol4.Updated Then db.AddInParameter(dbcmd, "@RsvCol4", DbType.String, SC_CommonRow.RsvCol4.Value)
            If SC_CommonRow.RsvCol5.Updated Then db.AddInParameter(dbcmd, "@RsvCol5", DbType.String, SC_CommonRow.RsvCol5.Value)
            db.AddInParameter(dbcmd, "@PKType", DbType.String, IIf(SC_CommonRow.LoadFromDataRow, SC_CommonRow.Type.OldValue, SC_CommonRow.Type.Value))
            db.AddInParameter(dbcmd, "@PKCode", DbType.String, IIf(SC_CommonRow.LoadFromDataRow, SC_CommonRow.Code.OldValue, SC_CommonRow.Code.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal SC_CommonRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_Common Set")
            For i As Integer = 0 To SC_CommonRow.FieldNames.Length - 1
                If Not SC_CommonRow.IsIdentityField(SC_CommonRow.FieldNames(i)) AndAlso SC_CommonRow.IsUpdated(SC_CommonRow.FieldNames(i)) AndAlso SC_CommonRow.CreateUpdateSQL(SC_CommonRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_CommonRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where Type = @PKType")
            strSQL.AppendLine("And Code = @PKCode")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_CommonRow.Type.Updated Then db.AddInParameter(dbcmd, "@Type", DbType.String, SC_CommonRow.Type.Value)
            If SC_CommonRow.Code.Updated Then db.AddInParameter(dbcmd, "@Code", DbType.String, SC_CommonRow.Code.Value)
            If SC_CommonRow.Define.Updated Then db.AddInParameter(dbcmd, "@Define", DbType.String, SC_CommonRow.Define.Value)
            If SC_CommonRow.ValidFlag.Updated Then db.AddInParameter(dbcmd, "@ValidFlag", DbType.String, SC_CommonRow.ValidFlag.Value)
            If SC_CommonRow.OrderSeq.Updated Then db.AddInParameter(dbcmd, "@OrderSeq", DbType.Int32, SC_CommonRow.OrderSeq.Value)
            If SC_CommonRow.Note.Updated Then db.AddInParameter(dbcmd, "@Note", DbType.String, SC_CommonRow.Note.Value)
            If SC_CommonRow.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_CommonRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_CommonRow.CreateDate.Value))
            If SC_CommonRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_CommonRow.LastChgID.Value)
            If SC_CommonRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_CommonRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_CommonRow.LastChgDate.Value))
            If SC_CommonRow.RsvCol1.Updated Then db.AddInParameter(dbcmd, "@RsvCol1", DbType.String, SC_CommonRow.RsvCol1.Value)
            If SC_CommonRow.RsvCol2.Updated Then db.AddInParameter(dbcmd, "@RsvCol2", DbType.String, SC_CommonRow.RsvCol2.Value)
            If SC_CommonRow.RsvCol3.Updated Then db.AddInParameter(dbcmd, "@RsvCol3", DbType.String, SC_CommonRow.RsvCol3.Value)
            If SC_CommonRow.RsvCol4.Updated Then db.AddInParameter(dbcmd, "@RsvCol4", DbType.String, SC_CommonRow.RsvCol4.Value)
            If SC_CommonRow.RsvCol5.Updated Then db.AddInParameter(dbcmd, "@RsvCol5", DbType.String, SC_CommonRow.RsvCol5.Value)
            db.AddInParameter(dbcmd, "@PKType", DbType.String, IIf(SC_CommonRow.LoadFromDataRow, SC_CommonRow.Type.OldValue, SC_CommonRow.Type.Value))
            db.AddInParameter(dbcmd, "@PKCode", DbType.String, IIf(SC_CommonRow.LoadFromDataRow, SC_CommonRow.Code.OldValue, SC_CommonRow.Code.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_CommonRow As Row()) As Integer
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
                    For Each r As Row In SC_CommonRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update SC_Common Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where Type = @PKType")
                        strSQL.AppendLine("And Code = @PKCode")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.Type.Updated Then db.AddInParameter(dbcmd, "@Type", DbType.String, r.Type.Value)
                        If r.Code.Updated Then db.AddInParameter(dbcmd, "@Code", DbType.String, r.Code.Value)
                        If r.Define.Updated Then db.AddInParameter(dbcmd, "@Define", DbType.String, r.Define.Value)
                        If r.ValidFlag.Updated Then db.AddInParameter(dbcmd, "@ValidFlag", DbType.String, r.ValidFlag.Value)
                        If r.OrderSeq.Updated Then db.AddInParameter(dbcmd, "@OrderSeq", DbType.Int32, r.OrderSeq.Value)
                        If r.Note.Updated Then db.AddInParameter(dbcmd, "@Note", DbType.String, r.Note.Value)
                        If r.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        If r.RsvCol1.Updated Then db.AddInParameter(dbcmd, "@RsvCol1", DbType.String, r.RsvCol1.Value)
                        If r.RsvCol2.Updated Then db.AddInParameter(dbcmd, "@RsvCol2", DbType.String, r.RsvCol2.Value)
                        If r.RsvCol3.Updated Then db.AddInParameter(dbcmd, "@RsvCol3", DbType.String, r.RsvCol3.Value)
                        If r.RsvCol4.Updated Then db.AddInParameter(dbcmd, "@RsvCol4", DbType.String, r.RsvCol4.Value)
                        If r.RsvCol5.Updated Then db.AddInParameter(dbcmd, "@RsvCol5", DbType.String, r.RsvCol5.Value)
                        db.AddInParameter(dbcmd, "@PKType", DbType.String, IIf(r.LoadFromDataRow, r.Type.OldValue, r.Type.Value))
                        db.AddInParameter(dbcmd, "@PKCode", DbType.String, IIf(r.LoadFromDataRow, r.Code.OldValue, r.Code.Value))

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

        Public Function Update(ByVal SC_CommonRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In SC_CommonRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update SC_Common Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where Type = @PKType")
                strSQL.AppendLine("And Code = @PKCode")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.Type.Updated Then db.AddInParameter(dbcmd, "@Type", DbType.String, r.Type.Value)
                If r.Code.Updated Then db.AddInParameter(dbcmd, "@Code", DbType.String, r.Code.Value)
                If r.Define.Updated Then db.AddInParameter(dbcmd, "@Define", DbType.String, r.Define.Value)
                If r.ValidFlag.Updated Then db.AddInParameter(dbcmd, "@ValidFlag", DbType.String, r.ValidFlag.Value)
                If r.OrderSeq.Updated Then db.AddInParameter(dbcmd, "@OrderSeq", DbType.Int32, r.OrderSeq.Value)
                If r.Note.Updated Then db.AddInParameter(dbcmd, "@Note", DbType.String, r.Note.Value)
                If r.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                If r.RsvCol1.Updated Then db.AddInParameter(dbcmd, "@RsvCol1", DbType.String, r.RsvCol1.Value)
                If r.RsvCol2.Updated Then db.AddInParameter(dbcmd, "@RsvCol2", DbType.String, r.RsvCol2.Value)
                If r.RsvCol3.Updated Then db.AddInParameter(dbcmd, "@RsvCol3", DbType.String, r.RsvCol3.Value)
                If r.RsvCol4.Updated Then db.AddInParameter(dbcmd, "@RsvCol4", DbType.String, r.RsvCol4.Value)
                If r.RsvCol5.Updated Then db.AddInParameter(dbcmd, "@RsvCol5", DbType.String, r.RsvCol5.Value)
                db.AddInParameter(dbcmd, "@PKType", DbType.String, IIf(r.LoadFromDataRow, r.Type.OldValue, r.Type.Value))
                db.AddInParameter(dbcmd, "@PKCode", DbType.String, IIf(r.LoadFromDataRow, r.Code.OldValue, r.Code.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal SC_CommonRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_Common")
            strSQL.AppendLine("Where Type = @Type")
            strSQL.AppendLine("And Code = @Code")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@Type", DbType.String, SC_CommonRow.Type.Value)
            db.AddInParameter(dbcmd, "@Code", DbType.String, SC_CommonRow.Code.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal SC_CommonRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_Common")
            strSQL.AppendLine("Where Type = @Type")
            strSQL.AppendLine("And Code = @Code")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@Type", DbType.String, SC_CommonRow.Type.Value)
            db.AddInParameter(dbcmd, "@Code", DbType.String, SC_CommonRow.Code.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_Common")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal SC_CommonRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_Common")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    Type, Code, Define, ValidFlag, OrderSeq, Note, CreateDate, LastChgID, LastChgDate,")
            strSQL.AppendLine("    RsvCol1, RsvCol2, RsvCol3, RsvCol4, RsvCol5")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @Type, @Code, @Define, @ValidFlag, @OrderSeq, @Note, @CreateDate, @LastChgID, @LastChgDate,")
            strSQL.AppendLine("    @RsvCol1, @RsvCol2, @RsvCol3, @RsvCol4, @RsvCol5")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@Type", DbType.String, SC_CommonRow.Type.Value)
            db.AddInParameter(dbcmd, "@Code", DbType.String, SC_CommonRow.Code.Value)
            db.AddInParameter(dbcmd, "@Define", DbType.String, SC_CommonRow.Define.Value)
            db.AddInParameter(dbcmd, "@ValidFlag", DbType.String, SC_CommonRow.ValidFlag.Value)
            db.AddInParameter(dbcmd, "@OrderSeq", DbType.Int32, SC_CommonRow.OrderSeq.Value)
            db.AddInParameter(dbcmd, "@Note", DbType.String, SC_CommonRow.Note.Value)
            db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_CommonRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_CommonRow.CreateDate.Value))
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_CommonRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_CommonRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_CommonRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@RsvCol1", DbType.String, SC_CommonRow.RsvCol1.Value)
            db.AddInParameter(dbcmd, "@RsvCol2", DbType.String, SC_CommonRow.RsvCol2.Value)
            db.AddInParameter(dbcmd, "@RsvCol3", DbType.String, SC_CommonRow.RsvCol3.Value)
            db.AddInParameter(dbcmd, "@RsvCol4", DbType.String, SC_CommonRow.RsvCol4.Value)
            db.AddInParameter(dbcmd, "@RsvCol5", DbType.String, SC_CommonRow.RsvCol5.Value)

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal SC_CommonRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_Common")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    Type, Code, Define, ValidFlag, OrderSeq, Note, CreateDate, LastChgID, LastChgDate,")
            strSQL.AppendLine("    RsvCol1, RsvCol2, RsvCol3, RsvCol4, RsvCol5")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @Type, @Code, @Define, @ValidFlag, @OrderSeq, @Note, @CreateDate, @LastChgID, @LastChgDate,")
            strSQL.AppendLine("    @RsvCol1, @RsvCol2, @RsvCol3, @RsvCol4, @RsvCol5")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@Type", DbType.String, SC_CommonRow.Type.Value)
            db.AddInParameter(dbcmd, "@Code", DbType.String, SC_CommonRow.Code.Value)
            db.AddInParameter(dbcmd, "@Define", DbType.String, SC_CommonRow.Define.Value)
            db.AddInParameter(dbcmd, "@ValidFlag", DbType.String, SC_CommonRow.ValidFlag.Value)
            db.AddInParameter(dbcmd, "@OrderSeq", DbType.Int32, SC_CommonRow.OrderSeq.Value)
            db.AddInParameter(dbcmd, "@Note", DbType.String, SC_CommonRow.Note.Value)
            db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_CommonRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_CommonRow.CreateDate.Value))
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_CommonRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_CommonRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_CommonRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@RsvCol1", DbType.String, SC_CommonRow.RsvCol1.Value)
            db.AddInParameter(dbcmd, "@RsvCol2", DbType.String, SC_CommonRow.RsvCol2.Value)
            db.AddInParameter(dbcmd, "@RsvCol3", DbType.String, SC_CommonRow.RsvCol3.Value)
            db.AddInParameter(dbcmd, "@RsvCol4", DbType.String, SC_CommonRow.RsvCol4.Value)
            db.AddInParameter(dbcmd, "@RsvCol5", DbType.String, SC_CommonRow.RsvCol5.Value)

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal SC_CommonRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_Common")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    Type, Code, Define, ValidFlag, OrderSeq, Note, CreateDate, LastChgID, LastChgDate,")
            strSQL.AppendLine("    RsvCol1, RsvCol2, RsvCol3, RsvCol4, RsvCol5")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @Type, @Code, @Define, @ValidFlag, @OrderSeq, @Note, @CreateDate, @LastChgID, @LastChgDate,")
            strSQL.AppendLine("    @RsvCol1, @RsvCol2, @RsvCol3, @RsvCol4, @RsvCol5")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_CommonRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@Type", DbType.String, r.Type.Value)
                        db.AddInParameter(dbcmd, "@Code", DbType.String, r.Code.Value)
                        db.AddInParameter(dbcmd, "@Define", DbType.String, r.Define.Value)
                        db.AddInParameter(dbcmd, "@ValidFlag", DbType.String, r.ValidFlag.Value)
                        db.AddInParameter(dbcmd, "@OrderSeq", DbType.Int32, r.OrderSeq.Value)
                        db.AddInParameter(dbcmd, "@Note", DbType.String, r.Note.Value)
                        db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                        db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@RsvCol1", DbType.String, r.RsvCol1.Value)
                        db.AddInParameter(dbcmd, "@RsvCol2", DbType.String, r.RsvCol2.Value)
                        db.AddInParameter(dbcmd, "@RsvCol3", DbType.String, r.RsvCol3.Value)
                        db.AddInParameter(dbcmd, "@RsvCol4", DbType.String, r.RsvCol4.Value)
                        db.AddInParameter(dbcmd, "@RsvCol5", DbType.String, r.RsvCol5.Value)

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

        Public Function Insert(ByVal SC_CommonRow As Row(), ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_Common")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    Type, Code, Define, ValidFlag, OrderSeq, Note, CreateDate, LastChgID, LastChgDate,")
            strSQL.AppendLine("    RsvCol1, RsvCol2, RsvCol3, RsvCol4, RsvCol5")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @Type, @Code, @Define, @ValidFlag, @OrderSeq, @Note, @CreateDate, @LastChgID, @LastChgDate,")
            strSQL.AppendLine("    @RsvCol1, @RsvCol2, @RsvCol3, @RsvCol4, @RsvCol5")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_CommonRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@Type", DbType.String, r.Type.Value)
                db.AddInParameter(dbcmd, "@Code", DbType.String, r.Code.Value)
                db.AddInParameter(dbcmd, "@Define", DbType.String, r.Define.Value)
                db.AddInParameter(dbcmd, "@ValidFlag", DbType.String, r.ValidFlag.Value)
                db.AddInParameter(dbcmd, "@OrderSeq", DbType.Int32, r.OrderSeq.Value)
                db.AddInParameter(dbcmd, "@Note", DbType.String, r.Note.Value)
                db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@RsvCol1", DbType.String, r.RsvCol1.Value)
                db.AddInParameter(dbcmd, "@RsvCol2", DbType.String, r.RsvCol2.Value)
                db.AddInParameter(dbcmd, "@RsvCol3", DbType.String, r.RsvCol3.Value)
                db.AddInParameter(dbcmd, "@RsvCol4", DbType.String, r.RsvCol4.Value)
                db.AddInParameter(dbcmd, "@RsvCol5", DbType.String, r.RsvCol5.Value)

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

