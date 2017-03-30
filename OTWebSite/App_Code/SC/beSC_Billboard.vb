'****************************************************************
' Table:SC_Billboard
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

Namespace beSC_Billboard
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "Kind", "Seq", "Title", "Content", "ImportantFlag", "DetailFlag", "ValidFrom", "ValidTo", "ShowName", "FileName" _
                                    , "CreateDate", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(Integer), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(Date), GetType(String), GetType(String) _
                                    , GetType(Date), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "Kind", "Seq" }

        Public ReadOnly Property Rows() As beSC_Billboard.Rows 
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
        Public Sub Transfer2Row(SC_BillboardTable As DataTable)
            For Each dr As DataRow In SC_BillboardTable.Rows
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

                dr(m_Rows(i).Kind.FieldName) = m_Rows(i).Kind.Value
                dr(m_Rows(i).Seq.FieldName) = m_Rows(i).Seq.Value
                dr(m_Rows(i).Title.FieldName) = m_Rows(i).Title.Value
                dr(m_Rows(i).Content.FieldName) = m_Rows(i).Content.Value
                dr(m_Rows(i).ImportantFlag.FieldName) = m_Rows(i).ImportantFlag.Value
                dr(m_Rows(i).DetailFlag.FieldName) = m_Rows(i).DetailFlag.Value
                dr(m_Rows(i).ValidFrom.FieldName) = m_Rows(i).ValidFrom.Value
                dr(m_Rows(i).ValidTo.FieldName) = m_Rows(i).ValidTo.Value
                dr(m_Rows(i).ShowName.FieldName) = m_Rows(i).ShowName.Value
                dr(m_Rows(i).FileName.FieldName) = m_Rows(i).FileName.Value
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

        Public Sub Add(SC_BillboardRow As Row)
            m_Rows.Add(SC_BillboardRow)
        End Sub

        Public Sub Remove(SC_BillboardRow As Row)
            If m_Rows.IndexOf(SC_BillboardRow) >= 0 Then
                m_Rows.Remove(SC_BillboardRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_Kind As Field(Of String) = new Field(Of String)("Kind", true)
        Private FI_Seq As Field(Of Integer) = new Field(Of Integer)("Seq", true)
        Private FI_Title As Field(Of String) = new Field(Of String)("Title", true)
        Private FI_Content As Field(Of String) = new Field(Of String)("Content", true)
        Private FI_ImportantFlag As Field(Of String) = new Field(Of String)("ImportantFlag", true)
        Private FI_DetailFlag As Field(Of String) = new Field(Of String)("DetailFlag", true)
        Private FI_ValidFrom As Field(Of Date) = new Field(Of Date)("ValidFrom", true)
        Private FI_ValidTo As Field(Of Date) = new Field(Of Date)("ValidTo", true)
        Private FI_ShowName As Field(Of String) = new Field(Of String)("ShowName", true)
        Private FI_FileName As Field(Of String) = new Field(Of String)("FileName", true)
        Private FI_CreateDate As Field(Of Date) = new Field(Of Date)("CreateDate", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "Kind", "Seq", "Title", "Content", "ImportantFlag", "DetailFlag", "ValidFrom", "ValidTo", "ShowName", "FileName" _
                                    , "CreateDate", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "Kind", "Seq" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "Kind"
                    Return FI_Kind.Value
                Case "Seq"
                    Return FI_Seq.Value
                Case "Title"
                    Return FI_Title.Value
                Case "Content"
                    Return FI_Content.Value
                Case "ImportantFlag"
                    Return FI_ImportantFlag.Value
                Case "DetailFlag"
                    Return FI_DetailFlag.Value
                Case "ValidFrom"
                    Return FI_ValidFrom.Value
                Case "ValidTo"
                    Return FI_ValidTo.Value
                Case "ShowName"
                    Return FI_ShowName.Value
                Case "FileName"
                    Return FI_FileName.Value
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
                Case "Kind"
                    FI_Kind.SetValue(value)
                Case "Seq"
                    FI_Seq.SetValue(value)
                Case "Title"
                    FI_Title.SetValue(value)
                Case "Content"
                    FI_Content.SetValue(value)
                Case "ImportantFlag"
                    FI_ImportantFlag.SetValue(value)
                Case "DetailFlag"
                    FI_DetailFlag.SetValue(value)
                Case "ValidFrom"
                    FI_ValidFrom.SetValue(value)
                Case "ValidTo"
                    FI_ValidTo.SetValue(value)
                Case "ShowName"
                    FI_ShowName.SetValue(value)
                Case "FileName"
                    FI_FileName.SetValue(value)
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
                Case "Kind"
                    return FI_Kind.Updated
                Case "Seq"
                    return FI_Seq.Updated
                Case "Title"
                    return FI_Title.Updated
                Case "Content"
                    return FI_Content.Updated
                Case "ImportantFlag"
                    return FI_ImportantFlag.Updated
                Case "DetailFlag"
                    return FI_DetailFlag.Updated
                Case "ValidFrom"
                    return FI_ValidFrom.Updated
                Case "ValidTo"
                    return FI_ValidTo.Updated
                Case "ShowName"
                    return FI_ShowName.Updated
                Case "FileName"
                    return FI_FileName.Updated
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
                Case "Kind"
                    return FI_Kind.CreateUpdateSQL
                Case "Seq"
                    return FI_Seq.CreateUpdateSQL
                Case "Title"
                    return FI_Title.CreateUpdateSQL
                Case "Content"
                    return FI_Content.CreateUpdateSQL
                Case "ImportantFlag"
                    return FI_ImportantFlag.CreateUpdateSQL
                Case "DetailFlag"
                    return FI_DetailFlag.CreateUpdateSQL
                Case "ValidFrom"
                    return FI_ValidFrom.CreateUpdateSQL
                Case "ValidTo"
                    return FI_ValidTo.CreateUpdateSQL
                Case "ShowName"
                    return FI_ShowName.CreateUpdateSQL
                Case "FileName"
                    return FI_FileName.CreateUpdateSQL
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
            FI_Kind.SetInitValue("")
            FI_Seq.SetInitValue(0)
            FI_Title.SetInitValue("")
            FI_Content.SetInitValue("")
            FI_ImportantFlag.SetInitValue("")
            FI_DetailFlag.SetInitValue("")
            FI_ValidFrom.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_ValidTo.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_ShowName.SetInitValue("")
            FI_FileName.SetInitValue("")
            FI_CreateDate.SetInitValue(DateTime.Now)
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(DateTime.Now)
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_Kind.SetInitValue(dr("Kind"))
            FI_Seq.SetInitValue(dr("Seq"))
            FI_Title.SetInitValue(dr("Title"))
            FI_Content.SetInitValue(dr("Content"))
            FI_ImportantFlag.SetInitValue(dr("ImportantFlag"))
            FI_DetailFlag.SetInitValue(dr("DetailFlag"))
            FI_ValidFrom.SetInitValue(dr("ValidFrom"))
            FI_ValidTo.SetInitValue(dr("ValidTo"))
            FI_ShowName.SetInitValue(dr("ShowName"))
            FI_FileName.SetInitValue(dr("FileName"))
            FI_CreateDate.SetInitValue(dr("CreateDate"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_Kind.Updated = False
            FI_Seq.Updated = False
            FI_Title.Updated = False
            FI_Content.Updated = False
            FI_ImportantFlag.Updated = False
            FI_DetailFlag.Updated = False
            FI_ValidFrom.Updated = False
            FI_ValidTo.Updated = False
            FI_ShowName.Updated = False
            FI_FileName.Updated = False
            FI_CreateDate.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
        End Sub

        Public ReadOnly Property Kind As Field(Of String) 
            Get
                Return FI_Kind
            End Get
        End Property

        Public ReadOnly Property Seq As Field(Of Integer) 
            Get
                Return FI_Seq
            End Get
        End Property

        Public ReadOnly Property Title As Field(Of String) 
            Get
                Return FI_Title
            End Get
        End Property

        Public ReadOnly Property Content As Field(Of String) 
            Get
                Return FI_Content
            End Get
        End Property

        Public ReadOnly Property ImportantFlag As Field(Of String) 
            Get
                Return FI_ImportantFlag
            End Get
        End Property

        Public ReadOnly Property DetailFlag As Field(Of String) 
            Get
                Return FI_DetailFlag
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

        Public ReadOnly Property ShowName As Field(Of String) 
            Get
                Return FI_ShowName
            End Get
        End Property

        Public ReadOnly Property FileName As Field(Of String) 
            Get
                Return FI_FileName
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
        Public Function DeleteRowByPrimaryKey(ByVal SC_BillboardRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_Billboard")
            strSQL.AppendLine("Where Kind = @Kind")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@Kind", DbType.String, SC_BillboardRow.Kind.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, SC_BillboardRow.Seq.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal SC_BillboardRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SC_Billboard")
            strSQL.AppendLine("Where Kind = @Kind")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@Kind", DbType.String, SC_BillboardRow.Kind.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, SC_BillboardRow.Seq.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal SC_BillboardRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_Billboard")
            strSQL.AppendLine("Where Kind = @Kind")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_BillboardRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@Kind", DbType.String, r.Kind.Value)
                        db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal SC_BillboardRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SC_Billboard")
            strSQL.AppendLine("Where Kind = @Kind")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_BillboardRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@Kind", DbType.String, r.Kind.Value)
                db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal SC_BillboardRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_Billboard")
            strSQL.AppendLine("Where Kind = @Kind")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@Kind", DbType.String, SC_BillboardRow.Kind.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, SC_BillboardRow.Seq.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(SC_BillboardRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_Billboard")
            strSQL.AppendLine("Where Kind = @Kind")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@Kind", DbType.String, SC_BillboardRow.Kind.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, SC_BillboardRow.Seq.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_BillboardRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_Billboard Set")
            For i As Integer = 0 To SC_BillboardRow.FieldNames.Length - 1
                If Not SC_BillboardRow.IsIdentityField(SC_BillboardRow.FieldNames(i)) AndAlso SC_BillboardRow.IsUpdated(SC_BillboardRow.FieldNames(i)) AndAlso SC_BillboardRow.CreateUpdateSQL(SC_BillboardRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_BillboardRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where Kind = @PKKind")
            strSQL.AppendLine("And Seq = @PKSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_BillboardRow.Kind.Updated Then db.AddInParameter(dbcmd, "@Kind", DbType.String, SC_BillboardRow.Kind.Value)
            If SC_BillboardRow.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, SC_BillboardRow.Seq.Value)
            If SC_BillboardRow.Title.Updated Then db.AddInParameter(dbcmd, "@Title", DbType.String, SC_BillboardRow.Title.Value)
            If SC_BillboardRow.Content.Updated Then db.AddInParameter(dbcmd, "@Content", DbType.String, SC_BillboardRow.Content.Value)
            If SC_BillboardRow.ImportantFlag.Updated Then db.AddInParameter(dbcmd, "@ImportantFlag", DbType.String, SC_BillboardRow.ImportantFlag.Value)
            If SC_BillboardRow.DetailFlag.Updated Then db.AddInParameter(dbcmd, "@DetailFlag", DbType.String, SC_BillboardRow.DetailFlag.Value)
            If SC_BillboardRow.ValidFrom.Updated Then db.AddInParameter(dbcmd, "@ValidFrom", DbType.Date, IIf(IsDateTimeNull(SC_BillboardRow.ValidFrom.Value), Convert.ToDateTime("1900/1/1"), SC_BillboardRow.ValidFrom.Value))
            If SC_BillboardRow.ValidTo.Updated Then db.AddInParameter(dbcmd, "@ValidTo", DbType.Date, IIf(IsDateTimeNull(SC_BillboardRow.ValidTo.Value), Convert.ToDateTime("1900/1/1"), SC_BillboardRow.ValidTo.Value))
            If SC_BillboardRow.ShowName.Updated Then db.AddInParameter(dbcmd, "@ShowName", DbType.String, SC_BillboardRow.ShowName.Value)
            If SC_BillboardRow.FileName.Updated Then db.AddInParameter(dbcmd, "@FileName", DbType.String, SC_BillboardRow.FileName.Value)
            If SC_BillboardRow.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_BillboardRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_BillboardRow.CreateDate.Value))
            If SC_BillboardRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_BillboardRow.LastChgID.Value)
            If SC_BillboardRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_BillboardRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_BillboardRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKKind", DbType.String, IIf(SC_BillboardRow.LoadFromDataRow, SC_BillboardRow.Kind.OldValue, SC_BillboardRow.Kind.Value))
            db.AddInParameter(dbcmd, "@PKSeq", DbType.Int32, IIf(SC_BillboardRow.LoadFromDataRow, SC_BillboardRow.Seq.OldValue, SC_BillboardRow.Seq.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal SC_BillboardRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SC_Billboard Set")
            For i As Integer = 0 To SC_BillboardRow.FieldNames.Length - 1
                If Not SC_BillboardRow.IsIdentityField(SC_BillboardRow.FieldNames(i)) AndAlso SC_BillboardRow.IsUpdated(SC_BillboardRow.FieldNames(i)) AndAlso SC_BillboardRow.CreateUpdateSQL(SC_BillboardRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SC_BillboardRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where Kind = @PKKind")
            strSQL.AppendLine("And Seq = @PKSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SC_BillboardRow.Kind.Updated Then db.AddInParameter(dbcmd, "@Kind", DbType.String, SC_BillboardRow.Kind.Value)
            If SC_BillboardRow.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, SC_BillboardRow.Seq.Value)
            If SC_BillboardRow.Title.Updated Then db.AddInParameter(dbcmd, "@Title", DbType.String, SC_BillboardRow.Title.Value)
            If SC_BillboardRow.Content.Updated Then db.AddInParameter(dbcmd, "@Content", DbType.String, SC_BillboardRow.Content.Value)
            If SC_BillboardRow.ImportantFlag.Updated Then db.AddInParameter(dbcmd, "@ImportantFlag", DbType.String, SC_BillboardRow.ImportantFlag.Value)
            If SC_BillboardRow.DetailFlag.Updated Then db.AddInParameter(dbcmd, "@DetailFlag", DbType.String, SC_BillboardRow.DetailFlag.Value)
            If SC_BillboardRow.ValidFrom.Updated Then db.AddInParameter(dbcmd, "@ValidFrom", DbType.Date, IIf(IsDateTimeNull(SC_BillboardRow.ValidFrom.Value), Convert.ToDateTime("1900/1/1"), SC_BillboardRow.ValidFrom.Value))
            If SC_BillboardRow.ValidTo.Updated Then db.AddInParameter(dbcmd, "@ValidTo", DbType.Date, IIf(IsDateTimeNull(SC_BillboardRow.ValidTo.Value), Convert.ToDateTime("1900/1/1"), SC_BillboardRow.ValidTo.Value))
            If SC_BillboardRow.ShowName.Updated Then db.AddInParameter(dbcmd, "@ShowName", DbType.String, SC_BillboardRow.ShowName.Value)
            If SC_BillboardRow.FileName.Updated Then db.AddInParameter(dbcmd, "@FileName", DbType.String, SC_BillboardRow.FileName.Value)
            If SC_BillboardRow.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_BillboardRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_BillboardRow.CreateDate.Value))
            If SC_BillboardRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_BillboardRow.LastChgID.Value)
            If SC_BillboardRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_BillboardRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_BillboardRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKKind", DbType.String, IIf(SC_BillboardRow.LoadFromDataRow, SC_BillboardRow.Kind.OldValue, SC_BillboardRow.Kind.Value))
            db.AddInParameter(dbcmd, "@PKSeq", DbType.Int32, IIf(SC_BillboardRow.LoadFromDataRow, SC_BillboardRow.Seq.OldValue, SC_BillboardRow.Seq.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal SC_BillboardRow As Row()) As Integer
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
                    For Each r As Row In SC_BillboardRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update SC_Billboard Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where Kind = @PKKind")
                        strSQL.AppendLine("And Seq = @PKSeq")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.Kind.Updated Then db.AddInParameter(dbcmd, "@Kind", DbType.String, r.Kind.Value)
                        If r.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                        If r.Title.Updated Then db.AddInParameter(dbcmd, "@Title", DbType.String, r.Title.Value)
                        If r.Content.Updated Then db.AddInParameter(dbcmd, "@Content", DbType.String, r.Content.Value)
                        If r.ImportantFlag.Updated Then db.AddInParameter(dbcmd, "@ImportantFlag", DbType.String, r.ImportantFlag.Value)
                        If r.DetailFlag.Updated Then db.AddInParameter(dbcmd, "@DetailFlag", DbType.String, r.DetailFlag.Value)
                        If r.ValidFrom.Updated Then db.AddInParameter(dbcmd, "@ValidFrom", DbType.Date, IIf(IsDateTimeNull(r.ValidFrom.Value), Convert.ToDateTime("1900/1/1"), r.ValidFrom.Value))
                        If r.ValidTo.Updated Then db.AddInParameter(dbcmd, "@ValidTo", DbType.Date, IIf(IsDateTimeNull(r.ValidTo.Value), Convert.ToDateTime("1900/1/1"), r.ValidTo.Value))
                        If r.ShowName.Updated Then db.AddInParameter(dbcmd, "@ShowName", DbType.String, r.ShowName.Value)
                        If r.FileName.Updated Then db.AddInParameter(dbcmd, "@FileName", DbType.String, r.FileName.Value)
                        If r.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKKind", DbType.String, IIf(r.LoadFromDataRow, r.Kind.OldValue, r.Kind.Value))
                        db.AddInParameter(dbcmd, "@PKSeq", DbType.Int32, IIf(r.LoadFromDataRow, r.Seq.OldValue, r.Seq.Value))

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

        Public Function Update(ByVal SC_BillboardRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In SC_BillboardRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update SC_Billboard Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where Kind = @PKKind")
                strSQL.AppendLine("And Seq = @PKSeq")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.Kind.Updated Then db.AddInParameter(dbcmd, "@Kind", DbType.String, r.Kind.Value)
                If r.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                If r.Title.Updated Then db.AddInParameter(dbcmd, "@Title", DbType.String, r.Title.Value)
                If r.Content.Updated Then db.AddInParameter(dbcmd, "@Content", DbType.String, r.Content.Value)
                If r.ImportantFlag.Updated Then db.AddInParameter(dbcmd, "@ImportantFlag", DbType.String, r.ImportantFlag.Value)
                If r.DetailFlag.Updated Then db.AddInParameter(dbcmd, "@DetailFlag", DbType.String, r.DetailFlag.Value)
                If r.ValidFrom.Updated Then db.AddInParameter(dbcmd, "@ValidFrom", DbType.Date, IIf(IsDateTimeNull(r.ValidFrom.Value), Convert.ToDateTime("1900/1/1"), r.ValidFrom.Value))
                If r.ValidTo.Updated Then db.AddInParameter(dbcmd, "@ValidTo", DbType.Date, IIf(IsDateTimeNull(r.ValidTo.Value), Convert.ToDateTime("1900/1/1"), r.ValidTo.Value))
                If r.ShowName.Updated Then db.AddInParameter(dbcmd, "@ShowName", DbType.String, r.ShowName.Value)
                If r.FileName.Updated Then db.AddInParameter(dbcmd, "@FileName", DbType.String, r.FileName.Value)
                If r.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKKind", DbType.String, IIf(r.LoadFromDataRow, r.Kind.OldValue, r.Kind.Value))
                db.AddInParameter(dbcmd, "@PKSeq", DbType.Int32, IIf(r.LoadFromDataRow, r.Seq.OldValue, r.Seq.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal SC_BillboardRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_Billboard")
            strSQL.AppendLine("Where Kind = @Kind")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@Kind", DbType.String, SC_BillboardRow.Kind.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, SC_BillboardRow.Seq.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal SC_BillboardRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SC_Billboard")
            strSQL.AppendLine("Where Kind = @Kind")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@Kind", DbType.String, SC_BillboardRow.Kind.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, SC_BillboardRow.Seq.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_Billboard")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal SC_BillboardRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_Billboard")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    Kind, Seq, Title, Content, ImportantFlag, DetailFlag, ValidFrom, ValidTo, ShowName,")
            strSQL.AppendLine("    FileName, CreateDate, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @Kind, @Seq, @Title, @Content, @ImportantFlag, @DetailFlag, @ValidFrom, @ValidTo, @ShowName,")
            strSQL.AppendLine("    @FileName, @CreateDate, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@Kind", DbType.String, SC_BillboardRow.Kind.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, SC_BillboardRow.Seq.Value)
            db.AddInParameter(dbcmd, "@Title", DbType.String, SC_BillboardRow.Title.Value)
            db.AddInParameter(dbcmd, "@Content", DbType.String, SC_BillboardRow.Content.Value)
            db.AddInParameter(dbcmd, "@ImportantFlag", DbType.String, SC_BillboardRow.ImportantFlag.Value)
            db.AddInParameter(dbcmd, "@DetailFlag", DbType.String, SC_BillboardRow.DetailFlag.Value)
            db.AddInParameter(dbcmd, "@ValidFrom", DbType.Date, IIf(IsDateTimeNull(SC_BillboardRow.ValidFrom.Value), Convert.ToDateTime("1900/1/1"), SC_BillboardRow.ValidFrom.Value))
            db.AddInParameter(dbcmd, "@ValidTo", DbType.Date, IIf(IsDateTimeNull(SC_BillboardRow.ValidTo.Value), Convert.ToDateTime("1900/1/1"), SC_BillboardRow.ValidTo.Value))
            db.AddInParameter(dbcmd, "@ShowName", DbType.String, SC_BillboardRow.ShowName.Value)
            db.AddInParameter(dbcmd, "@FileName", DbType.String, SC_BillboardRow.FileName.Value)
            db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_BillboardRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_BillboardRow.CreateDate.Value))
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_BillboardRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_BillboardRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_BillboardRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal SC_BillboardRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_Billboard")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    Kind, Seq, Title, Content, ImportantFlag, DetailFlag, ValidFrom, ValidTo, ShowName,")
            strSQL.AppendLine("    FileName, CreateDate, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @Kind, @Seq, @Title, @Content, @ImportantFlag, @DetailFlag, @ValidFrom, @ValidTo, @ShowName,")
            strSQL.AppendLine("    @FileName, @CreateDate, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@Kind", DbType.String, SC_BillboardRow.Kind.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, SC_BillboardRow.Seq.Value)
            db.AddInParameter(dbcmd, "@Title", DbType.String, SC_BillboardRow.Title.Value)
            db.AddInParameter(dbcmd, "@Content", DbType.String, SC_BillboardRow.Content.Value)
            db.AddInParameter(dbcmd, "@ImportantFlag", DbType.String, SC_BillboardRow.ImportantFlag.Value)
            db.AddInParameter(dbcmd, "@DetailFlag", DbType.String, SC_BillboardRow.DetailFlag.Value)
            db.AddInParameter(dbcmd, "@ValidFrom", DbType.Date, IIf(IsDateTimeNull(SC_BillboardRow.ValidFrom.Value), Convert.ToDateTime("1900/1/1"), SC_BillboardRow.ValidFrom.Value))
            db.AddInParameter(dbcmd, "@ValidTo", DbType.Date, IIf(IsDateTimeNull(SC_BillboardRow.ValidTo.Value), Convert.ToDateTime("1900/1/1"), SC_BillboardRow.ValidTo.Value))
            db.AddInParameter(dbcmd, "@ShowName", DbType.String, SC_BillboardRow.ShowName.Value)
            db.AddInParameter(dbcmd, "@FileName", DbType.String, SC_BillboardRow.FileName.Value)
            db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_BillboardRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SC_BillboardRow.CreateDate.Value))
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SC_BillboardRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SC_BillboardRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SC_BillboardRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal SC_BillboardRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_Billboard")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    Kind, Seq, Title, Content, ImportantFlag, DetailFlag, ValidFrom, ValidTo, ShowName,")
            strSQL.AppendLine("    FileName, CreateDate, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @Kind, @Seq, @Title, @Content, @ImportantFlag, @DetailFlag, @ValidFrom, @ValidTo, @ShowName,")
            strSQL.AppendLine("    @FileName, @CreateDate, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_BillboardRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@Kind", DbType.String, r.Kind.Value)
                        db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                        db.AddInParameter(dbcmd, "@Title", DbType.String, r.Title.Value)
                        db.AddInParameter(dbcmd, "@Content", DbType.String, r.Content.Value)
                        db.AddInParameter(dbcmd, "@ImportantFlag", DbType.String, r.ImportantFlag.Value)
                        db.AddInParameter(dbcmd, "@DetailFlag", DbType.String, r.DetailFlag.Value)
                        db.AddInParameter(dbcmd, "@ValidFrom", DbType.Date, IIf(IsDateTimeNull(r.ValidFrom.Value), Convert.ToDateTime("1900/1/1"), r.ValidFrom.Value))
                        db.AddInParameter(dbcmd, "@ValidTo", DbType.Date, IIf(IsDateTimeNull(r.ValidTo.Value), Convert.ToDateTime("1900/1/1"), r.ValidTo.Value))
                        db.AddInParameter(dbcmd, "@ShowName", DbType.String, r.ShowName.Value)
                        db.AddInParameter(dbcmd, "@FileName", DbType.String, r.FileName.Value)
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

        Public Function Insert(ByVal SC_BillboardRow As Row(), ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_Billboard")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    Kind, Seq, Title, Content, ImportantFlag, DetailFlag, ValidFrom, ValidTo, ShowName,")
            strSQL.AppendLine("    FileName, CreateDate, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @Kind, @Seq, @Title, @Content, @ImportantFlag, @DetailFlag, @ValidFrom, @ValidTo, @ShowName,")
            strSQL.AppendLine("    @FileName, @CreateDate, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_BillboardRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@Kind", DbType.String, r.Kind.Value)
                db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                db.AddInParameter(dbcmd, "@Title", DbType.String, r.Title.Value)
                db.AddInParameter(dbcmd, "@Content", DbType.String, r.Content.Value)
                db.AddInParameter(dbcmd, "@ImportantFlag", DbType.String, r.ImportantFlag.Value)
                db.AddInParameter(dbcmd, "@DetailFlag", DbType.String, r.DetailFlag.Value)
                db.AddInParameter(dbcmd, "@ValidFrom", DbType.Date, IIf(IsDateTimeNull(r.ValidFrom.Value), Convert.ToDateTime("1900/1/1"), r.ValidFrom.Value))
                db.AddInParameter(dbcmd, "@ValidTo", DbType.Date, IIf(IsDateTimeNull(r.ValidTo.Value), Convert.ToDateTime("1900/1/1"), r.ValidTo.Value))
                db.AddInParameter(dbcmd, "@ShowName", DbType.String, r.ShowName.Value)
                db.AddInParameter(dbcmd, "@FileName", DbType.String, r.FileName.Value)
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

