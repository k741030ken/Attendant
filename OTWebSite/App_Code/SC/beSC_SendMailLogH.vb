'****************************************************************
' Table:SC_SendMailLogH
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

Namespace beSC_SendMailLogH
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "ArrivalDt", "MailSeqNo", "SenderID", "MailAddress", "MailHeader", "GreetWord", "PsDesc", "LinkPath", "Attachment", "CreateDate" _
                                    , "SendDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(Integer), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date) _
                                    , GetType(Date) }
        Private m_PrimaryFields As String() = {  }

        Public ReadOnly Property Rows() As beSC_SendMailLogH.Rows 
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
        Public Sub Transfer2Row(SC_SendMailLogHTable As DataTable)
            For Each dr As DataRow In SC_SendMailLogHTable.Rows
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

                dr(m_Rows(i).ArrivalDt.FieldName) = m_Rows(i).ArrivalDt.Value
                dr(m_Rows(i).MailSeqNo.FieldName) = m_Rows(i).MailSeqNo.Value
                dr(m_Rows(i).SenderID.FieldName) = m_Rows(i).SenderID.Value
                dr(m_Rows(i).MailAddress.FieldName) = m_Rows(i).MailAddress.Value
                dr(m_Rows(i).MailHeader.FieldName) = m_Rows(i).MailHeader.Value
                dr(m_Rows(i).GreetWord.FieldName) = m_Rows(i).GreetWord.Value
                dr(m_Rows(i).PsDesc.FieldName) = m_Rows(i).PsDesc.Value
                dr(m_Rows(i).LinkPath.FieldName) = m_Rows(i).LinkPath.Value
                dr(m_Rows(i).Attachment.FieldName) = m_Rows(i).Attachment.Value
                dr(m_Rows(i).CreateDate.FieldName) = m_Rows(i).CreateDate.Value
                dr(m_Rows(i).SendDate.FieldName) = m_Rows(i).SendDate.Value

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

        Public Sub Add(SC_SendMailLogHRow As Row)
            m_Rows.Add(SC_SendMailLogHRow)
        End Sub

        Public Sub Remove(SC_SendMailLogHRow As Row)
            If m_Rows.IndexOf(SC_SendMailLogHRow) >= 0 Then
                m_Rows.Remove(SC_SendMailLogHRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_ArrivalDt As Field(Of String) = new Field(Of String)("ArrivalDt", true)
        Private FI_MailSeqNo As Field(Of Integer) = new Field(Of Integer)("MailSeqNo", true)
        Private FI_SenderID As Field(Of String) = new Field(Of String)("SenderID", true)
        Private FI_MailAddress As Field(Of String) = new Field(Of String)("MailAddress", true)
        Private FI_MailHeader As Field(Of String) = new Field(Of String)("MailHeader", true)
        Private FI_GreetWord As Field(Of String) = new Field(Of String)("GreetWord", true)
        Private FI_PsDesc As Field(Of String) = new Field(Of String)("PsDesc", true)
        Private FI_LinkPath As Field(Of String) = new Field(Of String)("LinkPath", true)
        Private FI_Attachment As Field(Of String) = new Field(Of String)("Attachment", true)
        Private FI_CreateDate As Field(Of Date) = new Field(Of Date)("CreateDate", true)
        Private FI_SendDate As Field(Of Date) = new Field(Of Date)("SendDate", true)
        Private m_FieldNames As String() = { "ArrivalDt", "MailSeqNo", "SenderID", "MailAddress", "MailHeader", "GreetWord", "PsDesc", "LinkPath", "Attachment", "CreateDate" _
                                    , "SendDate" }
        Private m_PrimaryFields As String() = {  }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "ArrivalDt"
                    Return FI_ArrivalDt.Value
                Case "MailSeqNo"
                    Return FI_MailSeqNo.Value
                Case "SenderID"
                    Return FI_SenderID.Value
                Case "MailAddress"
                    Return FI_MailAddress.Value
                Case "MailHeader"
                    Return FI_MailHeader.Value
                Case "GreetWord"
                    Return FI_GreetWord.Value
                Case "PsDesc"
                    Return FI_PsDesc.Value
                Case "LinkPath"
                    Return FI_LinkPath.Value
                Case "Attachment"
                    Return FI_Attachment.Value
                Case "CreateDate"
                    Return FI_CreateDate.Value
                Case "SendDate"
                    Return FI_SendDate.Value
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Sub SetFieldValue(ByVal fieldName As String, ByVal value As Object)
            Select Case fieldName
                Case "ArrivalDt"
                    FI_ArrivalDt.SetValue(value)
                Case "MailSeqNo"
                    FI_MailSeqNo.SetValue(value)
                Case "SenderID"
                    FI_SenderID.SetValue(value)
                Case "MailAddress"
                    FI_MailAddress.SetValue(value)
                Case "MailHeader"
                    FI_MailHeader.SetValue(value)
                Case "GreetWord"
                    FI_GreetWord.SetValue(value)
                Case "PsDesc"
                    FI_PsDesc.SetValue(value)
                Case "LinkPath"
                    FI_LinkPath.SetValue(value)
                Case "Attachment"
                    FI_Attachment.SetValue(value)
                Case "CreateDate"
                    FI_CreateDate.SetValue(value)
                Case "SendDate"
                    FI_SendDate.SetValue(value)
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
                Case "ArrivalDt"
                    return FI_ArrivalDt.Updated
                Case "MailSeqNo"
                    return FI_MailSeqNo.Updated
                Case "SenderID"
                    return FI_SenderID.Updated
                Case "MailAddress"
                    return FI_MailAddress.Updated
                Case "MailHeader"
                    return FI_MailHeader.Updated
                Case "GreetWord"
                    return FI_GreetWord.Updated
                Case "PsDesc"
                    return FI_PsDesc.Updated
                Case "LinkPath"
                    return FI_LinkPath.Updated
                Case "Attachment"
                    return FI_Attachment.Updated
                Case "CreateDate"
                    return FI_CreateDate.Updated
                Case "SendDate"
                    return FI_SendDate.Updated
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public Function CreateUpdateSQL(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "ArrivalDt"
                    return FI_ArrivalDt.CreateUpdateSQL
                Case "MailSeqNo"
                    return FI_MailSeqNo.CreateUpdateSQL
                Case "SenderID"
                    return FI_SenderID.CreateUpdateSQL
                Case "MailAddress"
                    return FI_MailAddress.CreateUpdateSQL
                Case "MailHeader"
                    return FI_MailHeader.CreateUpdateSQL
                Case "GreetWord"
                    return FI_GreetWord.CreateUpdateSQL
                Case "PsDesc"
                    return FI_PsDesc.CreateUpdateSQL
                Case "LinkPath"
                    return FI_LinkPath.CreateUpdateSQL
                Case "Attachment"
                    return FI_Attachment.CreateUpdateSQL
                Case "CreateDate"
                    return FI_CreateDate.CreateUpdateSQL
                Case "SendDate"
                    return FI_SendDate.CreateUpdateSQL
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
            FI_ArrivalDt.SetInitValue("")
            FI_MailSeqNo.SetInitValue(0)
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_ArrivalDt.SetInitValue(dr("ArrivalDt"))
            FI_MailSeqNo.SetInitValue(dr("MailSeqNo"))
            FI_SenderID.SetInitValue(dr("SenderID"))
            FI_MailAddress.SetInitValue(dr("MailAddress"))
            FI_MailHeader.SetInitValue(dr("MailHeader"))
            FI_GreetWord.SetInitValue(dr("GreetWord"))
            FI_PsDesc.SetInitValue(dr("PsDesc"))
            FI_LinkPath.SetInitValue(dr("LinkPath"))
            FI_Attachment.SetInitValue(dr("Attachment"))
            FI_CreateDate.SetInitValue(dr("CreateDate"))
            FI_SendDate.SetInitValue(dr("SendDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_ArrivalDt.Updated = False
            FI_MailSeqNo.Updated = False
            FI_SenderID.Updated = False
            FI_MailAddress.Updated = False
            FI_MailHeader.Updated = False
            FI_GreetWord.Updated = False
            FI_PsDesc.Updated = False
            FI_LinkPath.Updated = False
            FI_Attachment.Updated = False
            FI_CreateDate.Updated = False
            FI_SendDate.Updated = False
        End Sub

        Public ReadOnly Property ArrivalDt As Field(Of String) 
            Get
                Return FI_ArrivalDt
            End Get
        End Property

        Public ReadOnly Property MailSeqNo As Field(Of Integer) 
            Get
                Return FI_MailSeqNo
            End Get
        End Property

        Public ReadOnly Property SenderID As Field(Of String) 
            Get
                Return FI_SenderID
            End Get
        End Property

        Public ReadOnly Property MailAddress As Field(Of String) 
            Get
                Return FI_MailAddress
            End Get
        End Property

        Public ReadOnly Property MailHeader As Field(Of String) 
            Get
                Return FI_MailHeader
            End Get
        End Property

        Public ReadOnly Property GreetWord As Field(Of String) 
            Get
                Return FI_GreetWord
            End Get
        End Property

        Public ReadOnly Property PsDesc As Field(Of String) 
            Get
                Return FI_PsDesc
            End Get
        End Property

        Public ReadOnly Property LinkPath As Field(Of String) 
            Get
                Return FI_LinkPath
            End Get
        End Property

        Public ReadOnly Property Attachment As Field(Of String) 
            Get
                Return FI_Attachment
            End Get
        End Property

        Public ReadOnly Property CreateDate As Field(Of Date) 
            Get
                Return FI_CreateDate
            End Get
        End Property

        Public ReadOnly Property SendDate As Field(Of Date) 
            Get
                Return FI_SendDate
            End Get
        End Property

    End Class

    Public Class Service
        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_SendMailLogH")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal SC_SendMailLogHRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_SendMailLogH")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    ArrivalDt, MailSeqNo, SenderID, MailAddress, MailHeader, GreetWord, PsDesc, LinkPath,")
            strSQL.AppendLine("    Attachment, CreateDate, SendDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @ArrivalDt, @MailSeqNo, @SenderID, @MailAddress, @MailHeader, @GreetWord, @PsDesc, @LinkPath,")
            strSQL.AppendLine("    @Attachment, @CreateDate, @SendDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@ArrivalDt", DbType.String, SC_SendMailLogHRow.ArrivalDt.Value)
            db.AddInParameter(dbcmd, "@MailSeqNo", DbType.Int32, SC_SendMailLogHRow.MailSeqNo.Value)
            db.AddInParameter(dbcmd, "@SenderID", DbType.String, SC_SendMailLogHRow.SenderID.Value)
            db.AddInParameter(dbcmd, "@MailAddress", DbType.String, SC_SendMailLogHRow.MailAddress.Value)
            db.AddInParameter(dbcmd, "@MailHeader", DbType.String, SC_SendMailLogHRow.MailHeader.Value)
            db.AddInParameter(dbcmd, "@GreetWord", DbType.String, SC_SendMailLogHRow.GreetWord.Value)
            db.AddInParameter(dbcmd, "@PsDesc", DbType.String, SC_SendMailLogHRow.PsDesc.Value)
            db.AddInParameter(dbcmd, "@LinkPath", DbType.String, SC_SendMailLogHRow.LinkPath.Value)
            db.AddInParameter(dbcmd, "@Attachment", DbType.String, SC_SendMailLogHRow.Attachment.Value)
            db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_SendMailLogHRow.CreateDate.Value), DBNull.Value, SC_SendMailLogHRow.CreateDate.Value))
            db.AddInParameter(dbcmd, "@SendDate", DbType.Date, IIf(IsDateTimeNull(SC_SendMailLogHRow.SendDate.Value), DBNull.Value, SC_SendMailLogHRow.SendDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal SC_SendMailLogHRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_SendMailLogH")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    ArrivalDt, MailSeqNo, SenderID, MailAddress, MailHeader, GreetWord, PsDesc, LinkPath,")
            strSQL.AppendLine("    Attachment, CreateDate, SendDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @ArrivalDt, @MailSeqNo, @SenderID, @MailAddress, @MailHeader, @GreetWord, @PsDesc, @LinkPath,")
            strSQL.AppendLine("    @Attachment, @CreateDate, @SendDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@ArrivalDt", DbType.String, SC_SendMailLogHRow.ArrivalDt.Value)
            db.AddInParameter(dbcmd, "@MailSeqNo", DbType.Int32, SC_SendMailLogHRow.MailSeqNo.Value)
            db.AddInParameter(dbcmd, "@SenderID", DbType.String, SC_SendMailLogHRow.SenderID.Value)
            db.AddInParameter(dbcmd, "@MailAddress", DbType.String, SC_SendMailLogHRow.MailAddress.Value)
            db.AddInParameter(dbcmd, "@MailHeader", DbType.String, SC_SendMailLogHRow.MailHeader.Value)
            db.AddInParameter(dbcmd, "@GreetWord", DbType.String, SC_SendMailLogHRow.GreetWord.Value)
            db.AddInParameter(dbcmd, "@PsDesc", DbType.String, SC_SendMailLogHRow.PsDesc.Value)
            db.AddInParameter(dbcmd, "@LinkPath", DbType.String, SC_SendMailLogHRow.LinkPath.Value)
            db.AddInParameter(dbcmd, "@Attachment", DbType.String, SC_SendMailLogHRow.Attachment.Value)
            db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SC_SendMailLogHRow.CreateDate.Value), DBNull.Value, SC_SendMailLogHRow.CreateDate.Value))
            db.AddInParameter(dbcmd, "@SendDate", DbType.Date, IIf(IsDateTimeNull(SC_SendMailLogHRow.SendDate.Value), DBNull.Value, SC_SendMailLogHRow.SendDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal SC_SendMailLogHRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_SendMailLogH")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    ArrivalDt, MailSeqNo, SenderID, MailAddress, MailHeader, GreetWord, PsDesc, LinkPath,")
            strSQL.AppendLine("    Attachment, CreateDate, SendDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @ArrivalDt, @MailSeqNo, @SenderID, @MailAddress, @MailHeader, @GreetWord, @PsDesc, @LinkPath,")
            strSQL.AppendLine("    @Attachment, @CreateDate, @SendDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_SendMailLogHRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@ArrivalDt", DbType.String, r.ArrivalDt.Value)
                        db.AddInParameter(dbcmd, "@MailSeqNo", DbType.Int32, r.MailSeqNo.Value)
                        db.AddInParameter(dbcmd, "@SenderID", DbType.String, r.SenderID.Value)
                        db.AddInParameter(dbcmd, "@MailAddress", DbType.String, r.MailAddress.Value)
                        db.AddInParameter(dbcmd, "@MailHeader", DbType.String, r.MailHeader.Value)
                        db.AddInParameter(dbcmd, "@GreetWord", DbType.String, r.GreetWord.Value)
                        db.AddInParameter(dbcmd, "@PsDesc", DbType.String, r.PsDesc.Value)
                        db.AddInParameter(dbcmd, "@LinkPath", DbType.String, r.LinkPath.Value)
                        db.AddInParameter(dbcmd, "@Attachment", DbType.String, r.Attachment.Value)
                        db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), DBNull.Value, r.CreateDate.Value))
                        db.AddInParameter(dbcmd, "@SendDate", DbType.Date, IIf(IsDateTimeNull(r.SendDate.Value), DBNull.Value, r.SendDate.Value))

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

        Public Function Insert(ByVal SC_SendMailLogHRow As Row(), ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_SendMailLogH")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    ArrivalDt, MailSeqNo, SenderID, MailAddress, MailHeader, GreetWord, PsDesc, LinkPath,")
            strSQL.AppendLine("    Attachment, CreateDate, SendDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @ArrivalDt, @MailSeqNo, @SenderID, @MailAddress, @MailHeader, @GreetWord, @PsDesc, @LinkPath,")
            strSQL.AppendLine("    @Attachment, @CreateDate, @SendDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_SendMailLogHRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@ArrivalDt", DbType.String, r.ArrivalDt.Value)
                db.AddInParameter(dbcmd, "@MailSeqNo", DbType.Int32, r.MailSeqNo.Value)
                db.AddInParameter(dbcmd, "@SenderID", DbType.String, r.SenderID.Value)
                db.AddInParameter(dbcmd, "@MailAddress", DbType.String, r.MailAddress.Value)
                db.AddInParameter(dbcmd, "@MailHeader", DbType.String, r.MailHeader.Value)
                db.AddInParameter(dbcmd, "@GreetWord", DbType.String, r.GreetWord.Value)
                db.AddInParameter(dbcmd, "@PsDesc", DbType.String, r.PsDesc.Value)
                db.AddInParameter(dbcmd, "@LinkPath", DbType.String, r.LinkPath.Value)
                db.AddInParameter(dbcmd, "@Attachment", DbType.String, r.Attachment.Value)
                db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), DBNull.Value, r.CreateDate.Value))
                db.AddInParameter(dbcmd, "@SendDate", DbType.Date, IIf(IsDateTimeNull(r.SendDate.Value), DBNull.Value, r.SendDate.Value))

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

