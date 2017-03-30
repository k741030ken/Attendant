'****************************************************************
' Table:WF_Attachment
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

Namespace beWF_Attachment
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "FlowID", "FlowCaseID", "SeqNo", "PaperID", "DocType", "FileName", "ActFileName", "Path", "LastChgDate", "LastChgID" _
                                    , "CustomerID" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(Integer), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(String) _
                                    , GetType(String) }
        Private m_PrimaryFields As String() = { "FlowID", "FlowCaseID", "SeqNo" }

        Public ReadOnly Property Rows() As beWF_Attachment.Rows 
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
        Public Sub Transfer2Row(WF_AttachmentTable As DataTable)
            For Each dr As DataRow In WF_AttachmentTable.Rows
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

                dr(m_Rows(i).FlowID.FieldName) = m_Rows(i).FlowID.Value
                dr(m_Rows(i).FlowCaseID.FieldName) = m_Rows(i).FlowCaseID.Value
                dr(m_Rows(i).SeqNo.FieldName) = m_Rows(i).SeqNo.Value
                dr(m_Rows(i).PaperID.FieldName) = m_Rows(i).PaperID.Value
                dr(m_Rows(i).DocType.FieldName) = m_Rows(i).DocType.Value
                dr(m_Rows(i).FileName.FieldName) = m_Rows(i).FileName.Value
                dr(m_Rows(i).ActFileName.FieldName) = m_Rows(i).ActFileName.Value
                dr(m_Rows(i).Path.FieldName) = m_Rows(i).Path.Value
                dr(m_Rows(i).LastChgDate.FieldName) = m_Rows(i).LastChgDate.Value
                dr(m_Rows(i).LastChgID.FieldName) = m_Rows(i).LastChgID.Value
                dr(m_Rows(i).CustomerID.FieldName) = m_Rows(i).CustomerID.Value

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

        Public Sub Add(WF_AttachmentRow As Row)
            m_Rows.Add(WF_AttachmentRow)
        End Sub

        Public Sub Remove(WF_AttachmentRow As Row)
            If m_Rows.IndexOf(WF_AttachmentRow) >= 0 Then
                m_Rows.Remove(WF_AttachmentRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_FlowID As Field(Of String) = new Field(Of String)("FlowID", true)
        Private FI_FlowCaseID As Field(Of String) = new Field(Of String)("FlowCaseID", true)
        Private FI_SeqNo As Field(Of Integer) = new Field(Of Integer)("SeqNo", true)
        Private FI_PaperID As Field(Of String) = new Field(Of String)("PaperID", true)
        Private FI_DocType As Field(Of String) = new Field(Of String)("DocType", true)
        Private FI_FileName As Field(Of String) = new Field(Of String)("FileName", true)
        Private FI_ActFileName As Field(Of String) = new Field(Of String)("ActFileName", true)
        Private FI_Path As Field(Of String) = new Field(Of String)("Path", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_CustomerID As Field(Of String) = new Field(Of String)("CustomerID", true)
        Private m_FieldNames As String() = { "FlowID", "FlowCaseID", "SeqNo", "PaperID", "DocType", "FileName", "ActFileName", "Path", "LastChgDate", "LastChgID" _
                                    , "CustomerID" }
        Private m_PrimaryFields As String() = { "FlowID", "FlowCaseID", "SeqNo" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "FlowID"
                    Return FI_FlowID.Value
                Case "FlowCaseID"
                    Return FI_FlowCaseID.Value
                Case "SeqNo"
                    Return FI_SeqNo.Value
                Case "PaperID"
                    Return FI_PaperID.Value
                Case "DocType"
                    Return FI_DocType.Value
                Case "FileName"
                    Return FI_FileName.Value
                Case "ActFileName"
                    Return FI_ActFileName.Value
                Case "Path"
                    Return FI_Path.Value
                Case "LastChgDate"
                    Return FI_LastChgDate.Value
                Case "LastChgID"
                    Return FI_LastChgID.Value
                Case "CustomerID"
                    Return FI_CustomerID.Value
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Sub SetFieldValue(ByVal fieldName As String, ByVal value As Object)
            Select Case fieldName
                Case "FlowID"
                    FI_FlowID.SetValue(value)
                Case "FlowCaseID"
                    FI_FlowCaseID.SetValue(value)
                Case "SeqNo"
                    FI_SeqNo.SetValue(value)
                Case "PaperID"
                    FI_PaperID.SetValue(value)
                Case "DocType"
                    FI_DocType.SetValue(value)
                Case "FileName"
                    FI_FileName.SetValue(value)
                Case "ActFileName"
                    FI_ActFileName.SetValue(value)
                Case "Path"
                    FI_Path.SetValue(value)
                Case "LastChgDate"
                    FI_LastChgDate.SetValue(value)
                Case "LastChgID"
                    FI_LastChgID.SetValue(value)
                Case "CustomerID"
                    FI_CustomerID.SetValue(value)
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
                Case "FlowID"
                    return FI_FlowID.Updated
                Case "FlowCaseID"
                    return FI_FlowCaseID.Updated
                Case "SeqNo"
                    return FI_SeqNo.Updated
                Case "PaperID"
                    return FI_PaperID.Updated
                Case "DocType"
                    return FI_DocType.Updated
                Case "FileName"
                    return FI_FileName.Updated
                Case "ActFileName"
                    return FI_ActFileName.Updated
                Case "Path"
                    return FI_Path.Updated
                Case "LastChgDate"
                    return FI_LastChgDate.Updated
                Case "LastChgID"
                    return FI_LastChgID.Updated
                Case "CustomerID"
                    return FI_CustomerID.Updated
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public Function CreateUpdateSQL(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "FlowID"
                    return FI_FlowID.CreateUpdateSQL
                Case "FlowCaseID"
                    return FI_FlowCaseID.CreateUpdateSQL
                Case "SeqNo"
                    return FI_SeqNo.CreateUpdateSQL
                Case "PaperID"
                    return FI_PaperID.CreateUpdateSQL
                Case "DocType"
                    return FI_DocType.CreateUpdateSQL
                Case "FileName"
                    return FI_FileName.CreateUpdateSQL
                Case "ActFileName"
                    return FI_ActFileName.CreateUpdateSQL
                Case "Path"
                    return FI_Path.CreateUpdateSQL
                Case "LastChgDate"
                    return FI_LastChgDate.CreateUpdateSQL
                Case "LastChgID"
                    return FI_LastChgID.CreateUpdateSQL
                Case "CustomerID"
                    return FI_CustomerID.CreateUpdateSQL
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
            FI_FlowID.SetInitValue("")
            FI_FlowCaseID.SetInitValue("")
            FI_SeqNo.SetInitValue(0)
            FI_CustomerID.SetInitValue("")
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_FlowID.SetInitValue(dr("FlowID"))
            FI_FlowCaseID.SetInitValue(dr("FlowCaseID"))
            FI_SeqNo.SetInitValue(dr("SeqNo"))
            FI_PaperID.SetInitValue(dr("PaperID"))
            FI_DocType.SetInitValue(dr("DocType"))
            FI_FileName.SetInitValue(dr("FileName"))
            FI_ActFileName.SetInitValue(dr("ActFileName"))
            FI_Path.SetInitValue(dr("Path"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_CustomerID.SetInitValue(dr("CustomerID"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_FlowID.Updated = False
            FI_FlowCaseID.Updated = False
            FI_SeqNo.Updated = False
            FI_PaperID.Updated = False
            FI_DocType.Updated = False
            FI_FileName.Updated = False
            FI_ActFileName.Updated = False
            FI_Path.Updated = False
            FI_LastChgDate.Updated = False
            FI_LastChgID.Updated = False
            FI_CustomerID.Updated = False
        End Sub

        Public ReadOnly Property FlowID As Field(Of String) 
            Get
                Return FI_FlowID
            End Get
        End Property

        Public ReadOnly Property FlowCaseID As Field(Of String) 
            Get
                Return FI_FlowCaseID
            End Get
        End Property

        Public ReadOnly Property SeqNo As Field(Of Integer) 
            Get
                Return FI_SeqNo
            End Get
        End Property

        Public ReadOnly Property PaperID As Field(Of String) 
            Get
                Return FI_PaperID
            End Get
        End Property

        Public ReadOnly Property DocType As Field(Of String) 
            Get
                Return FI_DocType
            End Get
        End Property

        Public ReadOnly Property FileName As Field(Of String) 
            Get
                Return FI_FileName
            End Get
        End Property

        Public ReadOnly Property ActFileName As Field(Of String) 
            Get
                Return FI_ActFileName
            End Get
        End Property

        Public ReadOnly Property Path As Field(Of String) 
            Get
                Return FI_Path
            End Get
        End Property

        Public ReadOnly Property LastChgDate As Field(Of Date) 
            Get
                Return FI_LastChgDate
            End Get
        End Property

        Public ReadOnly Property LastChgID As Field(Of String) 
            Get
                Return FI_LastChgID
            End Get
        End Property

        Public ReadOnly Property CustomerID As Field(Of String) 
            Get
                Return FI_CustomerID
            End Get
        End Property

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal WF_AttachmentRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From WF_Attachment")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And SeqNo = @SeqNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_AttachmentRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_AttachmentRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, WF_AttachmentRow.SeqNo.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal WF_AttachmentRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From WF_Attachment")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And SeqNo = @SeqNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_AttachmentRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_AttachmentRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, WF_AttachmentRow.SeqNo.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal WF_AttachmentRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From WF_Attachment")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And SeqNo = @SeqNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In WF_AttachmentRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                        db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, r.FlowCaseID.Value)
                        db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, r.SeqNo.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal WF_AttachmentRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From WF_Attachment")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And SeqNo = @SeqNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In WF_AttachmentRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, r.FlowCaseID.Value)
                db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, r.SeqNo.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal WF_AttachmentRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WF_Attachment")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And SeqNo = @SeqNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_AttachmentRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_AttachmentRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, WF_AttachmentRow.SeqNo.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(WF_AttachmentRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WF_Attachment")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And SeqNo = @SeqNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_AttachmentRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_AttachmentRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, WF_AttachmentRow.SeqNo.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal WF_AttachmentRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update WF_Attachment Set")
            For i As Integer = 0 To WF_AttachmentRow.FieldNames.Length - 1
                If Not WF_AttachmentRow.IsIdentityField(WF_AttachmentRow.FieldNames(i)) AndAlso WF_AttachmentRow.IsUpdated(WF_AttachmentRow.FieldNames(i)) AndAlso WF_AttachmentRow.CreateUpdateSQL(WF_AttachmentRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, WF_AttachmentRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where FlowID = @PKFlowID")
            strSQL.AppendLine("And FlowCaseID = @PKFlowCaseID")
            strSQL.AppendLine("And SeqNo = @PKSeqNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If WF_AttachmentRow.FlowID.Updated Then db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_AttachmentRow.FlowID.Value)
            If WF_AttachmentRow.FlowCaseID.Updated Then db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_AttachmentRow.FlowCaseID.Value)
            If WF_AttachmentRow.SeqNo.Updated Then db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, WF_AttachmentRow.SeqNo.Value)
            If WF_AttachmentRow.PaperID.Updated Then db.AddInParameter(dbcmd, "@PaperID", DbType.String, WF_AttachmentRow.PaperID.Value)
            If WF_AttachmentRow.DocType.Updated Then db.AddInParameter(dbcmd, "@DocType", DbType.String, WF_AttachmentRow.DocType.Value)
            If WF_AttachmentRow.FileName.Updated Then db.AddInParameter(dbcmd, "@FileName", DbType.String, WF_AttachmentRow.FileName.Value)
            If WF_AttachmentRow.ActFileName.Updated Then db.AddInParameter(dbcmd, "@ActFileName", DbType.String, WF_AttachmentRow.ActFileName.Value)
            If WF_AttachmentRow.Path.Updated Then db.AddInParameter(dbcmd, "@Path", DbType.String, WF_AttachmentRow.Path.Value)
            If WF_AttachmentRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WF_AttachmentRow.LastChgDate.Value), DBNull.Value, WF_AttachmentRow.LastChgDate.Value))
            If WF_AttachmentRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WF_AttachmentRow.LastChgID.Value)
            If WF_AttachmentRow.CustomerID.Updated Then db.AddInParameter(dbcmd, "@CustomerID", DbType.String, WF_AttachmentRow.CustomerID.Value)
            db.AddInParameter(dbcmd, "@PKFlowID", DbType.String, IIf(WF_AttachmentRow.LoadFromDataRow, WF_AttachmentRow.FlowID.OldValue, WF_AttachmentRow.FlowID.Value))
            db.AddInParameter(dbcmd, "@PKFlowCaseID", DbType.String, IIf(WF_AttachmentRow.LoadFromDataRow, WF_AttachmentRow.FlowCaseID.OldValue, WF_AttachmentRow.FlowCaseID.Value))
            db.AddInParameter(dbcmd, "@PKSeqNo", DbType.Int32, IIf(WF_AttachmentRow.LoadFromDataRow, WF_AttachmentRow.SeqNo.OldValue, WF_AttachmentRow.SeqNo.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal WF_AttachmentRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update WF_Attachment Set")
            For i As Integer = 0 To WF_AttachmentRow.FieldNames.Length - 1
                If Not WF_AttachmentRow.IsIdentityField(WF_AttachmentRow.FieldNames(i)) AndAlso WF_AttachmentRow.IsUpdated(WF_AttachmentRow.FieldNames(i)) AndAlso WF_AttachmentRow.CreateUpdateSQL(WF_AttachmentRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, WF_AttachmentRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where FlowID = @PKFlowID")
            strSQL.AppendLine("And FlowCaseID = @PKFlowCaseID")
            strSQL.AppendLine("And SeqNo = @PKSeqNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If WF_AttachmentRow.FlowID.Updated Then db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_AttachmentRow.FlowID.Value)
            If WF_AttachmentRow.FlowCaseID.Updated Then db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_AttachmentRow.FlowCaseID.Value)
            If WF_AttachmentRow.SeqNo.Updated Then db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, WF_AttachmentRow.SeqNo.Value)
            If WF_AttachmentRow.PaperID.Updated Then db.AddInParameter(dbcmd, "@PaperID", DbType.String, WF_AttachmentRow.PaperID.Value)
            If WF_AttachmentRow.DocType.Updated Then db.AddInParameter(dbcmd, "@DocType", DbType.String, WF_AttachmentRow.DocType.Value)
            If WF_AttachmentRow.FileName.Updated Then db.AddInParameter(dbcmd, "@FileName", DbType.String, WF_AttachmentRow.FileName.Value)
            If WF_AttachmentRow.ActFileName.Updated Then db.AddInParameter(dbcmd, "@ActFileName", DbType.String, WF_AttachmentRow.ActFileName.Value)
            If WF_AttachmentRow.Path.Updated Then db.AddInParameter(dbcmd, "@Path", DbType.String, WF_AttachmentRow.Path.Value)
            If WF_AttachmentRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WF_AttachmentRow.LastChgDate.Value), DBNull.Value, WF_AttachmentRow.LastChgDate.Value))
            If WF_AttachmentRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WF_AttachmentRow.LastChgID.Value)
            If WF_AttachmentRow.CustomerID.Updated Then db.AddInParameter(dbcmd, "@CustomerID", DbType.String, WF_AttachmentRow.CustomerID.Value)
            db.AddInParameter(dbcmd, "@PKFlowID", DbType.String, IIf(WF_AttachmentRow.LoadFromDataRow, WF_AttachmentRow.FlowID.OldValue, WF_AttachmentRow.FlowID.Value))
            db.AddInParameter(dbcmd, "@PKFlowCaseID", DbType.String, IIf(WF_AttachmentRow.LoadFromDataRow, WF_AttachmentRow.FlowCaseID.OldValue, WF_AttachmentRow.FlowCaseID.Value))
            db.AddInParameter(dbcmd, "@PKSeqNo", DbType.Int32, IIf(WF_AttachmentRow.LoadFromDataRow, WF_AttachmentRow.SeqNo.OldValue, WF_AttachmentRow.SeqNo.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal WF_AttachmentRow As Row()) As Integer
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
                    For Each r As Row In WF_AttachmentRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update WF_Attachment Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where FlowID = @PKFlowID")
                        strSQL.AppendLine("And FlowCaseID = @PKFlowCaseID")
                        strSQL.AppendLine("And SeqNo = @PKSeqNo")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.FlowID.Updated Then db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                        If r.FlowCaseID.Updated Then db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, r.FlowCaseID.Value)
                        If r.SeqNo.Updated Then db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, r.SeqNo.Value)
                        If r.PaperID.Updated Then db.AddInParameter(dbcmd, "@PaperID", DbType.String, r.PaperID.Value)
                        If r.DocType.Updated Then db.AddInParameter(dbcmd, "@DocType", DbType.String, r.DocType.Value)
                        If r.FileName.Updated Then db.AddInParameter(dbcmd, "@FileName", DbType.String, r.FileName.Value)
                        If r.ActFileName.Updated Then db.AddInParameter(dbcmd, "@ActFileName", DbType.String, r.ActFileName.Value)
                        If r.Path.Updated Then db.AddInParameter(dbcmd, "@Path", DbType.String, r.Path.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), DBNull.Value, r.LastChgDate.Value))
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.CustomerID.Updated Then db.AddInParameter(dbcmd, "@CustomerID", DbType.String, r.CustomerID.Value)
                        db.AddInParameter(dbcmd, "@PKFlowID", DbType.String, IIf(r.LoadFromDataRow, r.FlowID.OldValue, r.FlowID.Value))
                        db.AddInParameter(dbcmd, "@PKFlowCaseID", DbType.String, IIf(r.LoadFromDataRow, r.FlowCaseID.OldValue, r.FlowCaseID.Value))
                        db.AddInParameter(dbcmd, "@PKSeqNo", DbType.Int32, IIf(r.LoadFromDataRow, r.SeqNo.OldValue, r.SeqNo.Value))

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

        Public Function Update(ByVal WF_AttachmentRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In WF_AttachmentRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update WF_Attachment Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where FlowID = @PKFlowID")
                strSQL.AppendLine("And FlowCaseID = @PKFlowCaseID")
                strSQL.AppendLine("And SeqNo = @PKSeqNo")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.FlowID.Updated Then db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                If r.FlowCaseID.Updated Then db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, r.FlowCaseID.Value)
                If r.SeqNo.Updated Then db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, r.SeqNo.Value)
                If r.PaperID.Updated Then db.AddInParameter(dbcmd, "@PaperID", DbType.String, r.PaperID.Value)
                If r.DocType.Updated Then db.AddInParameter(dbcmd, "@DocType", DbType.String, r.DocType.Value)
                If r.FileName.Updated Then db.AddInParameter(dbcmd, "@FileName", DbType.String, r.FileName.Value)
                If r.ActFileName.Updated Then db.AddInParameter(dbcmd, "@ActFileName", DbType.String, r.ActFileName.Value)
                If r.Path.Updated Then db.AddInParameter(dbcmd, "@Path", DbType.String, r.Path.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), DBNull.Value, r.LastChgDate.Value))
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.CustomerID.Updated Then db.AddInParameter(dbcmd, "@CustomerID", DbType.String, r.CustomerID.Value)
                db.AddInParameter(dbcmd, "@PKFlowID", DbType.String, IIf(r.LoadFromDataRow, r.FlowID.OldValue, r.FlowID.Value))
                db.AddInParameter(dbcmd, "@PKFlowCaseID", DbType.String, IIf(r.LoadFromDataRow, r.FlowCaseID.OldValue, r.FlowCaseID.Value))
                db.AddInParameter(dbcmd, "@PKSeqNo", DbType.Int32, IIf(r.LoadFromDataRow, r.SeqNo.OldValue, r.SeqNo.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal WF_AttachmentRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From WF_Attachment")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And SeqNo = @SeqNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_AttachmentRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_AttachmentRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, WF_AttachmentRow.SeqNo.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal WF_AttachmentRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From WF_Attachment")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And SeqNo = @SeqNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_AttachmentRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_AttachmentRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, WF_AttachmentRow.SeqNo.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WF_Attachment")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal WF_AttachmentRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into WF_Attachment")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowID, FlowCaseID, SeqNo, PaperID, DocType, FileName, ActFileName, Path, LastChgDate,")
            strSQL.AppendLine("    LastChgID, CustomerID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowID, @FlowCaseID, @SeqNo, @PaperID, @DocType, @FileName, @ActFileName, @Path, @LastChgDate,")
            strSQL.AppendLine("    @LastChgID, @CustomerID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_AttachmentRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_AttachmentRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, WF_AttachmentRow.SeqNo.Value)
            db.AddInParameter(dbcmd, "@PaperID", DbType.String, WF_AttachmentRow.PaperID.Value)
            db.AddInParameter(dbcmd, "@DocType", DbType.String, WF_AttachmentRow.DocType.Value)
            db.AddInParameter(dbcmd, "@FileName", DbType.String, WF_AttachmentRow.FileName.Value)
            db.AddInParameter(dbcmd, "@ActFileName", DbType.String, WF_AttachmentRow.ActFileName.Value)
            db.AddInParameter(dbcmd, "@Path", DbType.String, WF_AttachmentRow.Path.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WF_AttachmentRow.LastChgDate.Value), DBNull.Value, WF_AttachmentRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WF_AttachmentRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@CustomerID", DbType.String, WF_AttachmentRow.CustomerID.Value)

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal WF_AttachmentRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into WF_Attachment")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowID, FlowCaseID, SeqNo, PaperID, DocType, FileName, ActFileName, Path, LastChgDate,")
            strSQL.AppendLine("    LastChgID, CustomerID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowID, @FlowCaseID, @SeqNo, @PaperID, @DocType, @FileName, @ActFileName, @Path, @LastChgDate,")
            strSQL.AppendLine("    @LastChgID, @CustomerID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_AttachmentRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_AttachmentRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, WF_AttachmentRow.SeqNo.Value)
            db.AddInParameter(dbcmd, "@PaperID", DbType.String, WF_AttachmentRow.PaperID.Value)
            db.AddInParameter(dbcmd, "@DocType", DbType.String, WF_AttachmentRow.DocType.Value)
            db.AddInParameter(dbcmd, "@FileName", DbType.String, WF_AttachmentRow.FileName.Value)
            db.AddInParameter(dbcmd, "@ActFileName", DbType.String, WF_AttachmentRow.ActFileName.Value)
            db.AddInParameter(dbcmd, "@Path", DbType.String, WF_AttachmentRow.Path.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WF_AttachmentRow.LastChgDate.Value), DBNull.Value, WF_AttachmentRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WF_AttachmentRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@CustomerID", DbType.String, WF_AttachmentRow.CustomerID.Value)

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal WF_AttachmentRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into WF_Attachment")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowID, FlowCaseID, SeqNo, PaperID, DocType, FileName, ActFileName, Path, LastChgDate,")
            strSQL.AppendLine("    LastChgID, CustomerID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowID, @FlowCaseID, @SeqNo, @PaperID, @DocType, @FileName, @ActFileName, @Path, @LastChgDate,")
            strSQL.AppendLine("    @LastChgID, @CustomerID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In WF_AttachmentRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                        db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, r.FlowCaseID.Value)
                        db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, r.SeqNo.Value)
                        db.AddInParameter(dbcmd, "@PaperID", DbType.String, r.PaperID.Value)
                        db.AddInParameter(dbcmd, "@DocType", DbType.String, r.DocType.Value)
                        db.AddInParameter(dbcmd, "@FileName", DbType.String, r.FileName.Value)
                        db.AddInParameter(dbcmd, "@ActFileName", DbType.String, r.ActFileName.Value)
                        db.AddInParameter(dbcmd, "@Path", DbType.String, r.Path.Value)
                        db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), DBNull.Value, r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        db.AddInParameter(dbcmd, "@CustomerID", DbType.String, r.CustomerID.Value)

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

        Public Function Insert(ByVal WF_AttachmentRow As Row(), ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into WF_Attachment")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowID, FlowCaseID, SeqNo, PaperID, DocType, FileName, ActFileName, Path, LastChgDate,")
            strSQL.AppendLine("    LastChgID, CustomerID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowID, @FlowCaseID, @SeqNo, @PaperID, @DocType, @FileName, @ActFileName, @Path, @LastChgDate,")
            strSQL.AppendLine("    @LastChgID, @CustomerID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In WF_AttachmentRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, r.FlowCaseID.Value)
                db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, r.SeqNo.Value)
                db.AddInParameter(dbcmd, "@PaperID", DbType.String, r.PaperID.Value)
                db.AddInParameter(dbcmd, "@DocType", DbType.String, r.DocType.Value)
                db.AddInParameter(dbcmd, "@FileName", DbType.String, r.FileName.Value)
                db.AddInParameter(dbcmd, "@ActFileName", DbType.String, r.ActFileName.Value)
                db.AddInParameter(dbcmd, "@Path", DbType.String, r.Path.Value)
                db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), DBNull.Value, r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                db.AddInParameter(dbcmd, "@CustomerID", DbType.String, r.CustomerID.Value)

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

