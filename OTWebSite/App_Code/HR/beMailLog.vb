'****************************************************************
' Table:MailLog
' Created Date: 2014.10.03
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beMailLog
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CreateTime", "Seq", "Sender", "AcceptorCompID", "Acceptor", "EMail", "Subject", "Content", "Attachment", "SuccessFlag" _
                                    , "SendCount", "SendTime", "CCEMail" }
        Private m_Types As System.Type() = { GetType(Date), GetType(Integer), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(Integer), GetType(Date), GetType(String) }
        Private m_PrimaryFields As String() = { "CreateTime", "Seq" }

        Public ReadOnly Property Rows() As beMailLog.Rows 
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
        Public Sub Transfer2Row(MailLogTable As DataTable)
            For Each dr As DataRow In MailLogTable.Rows
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

                dr(m_Rows(i).CreateTime.FieldName) = m_Rows(i).CreateTime.Value
                dr(m_Rows(i).Seq.FieldName) = m_Rows(i).Seq.Value
                dr(m_Rows(i).Sender.FieldName) = m_Rows(i).Sender.Value
                dr(m_Rows(i).AcceptorCompID.FieldName) = m_Rows(i).AcceptorCompID.Value
                dr(m_Rows(i).Acceptor.FieldName) = m_Rows(i).Acceptor.Value
                dr(m_Rows(i).EMail.FieldName) = m_Rows(i).EMail.Value
                dr(m_Rows(i).Subject.FieldName) = m_Rows(i).Subject.Value
                dr(m_Rows(i).Content.FieldName) = m_Rows(i).Content.Value
                dr(m_Rows(i).Attachment.FieldName) = m_Rows(i).Attachment.Value
                dr(m_Rows(i).SuccessFlag.FieldName) = m_Rows(i).SuccessFlag.Value
                dr(m_Rows(i).SendCount.FieldName) = m_Rows(i).SendCount.Value
                dr(m_Rows(i).SendTime.FieldName) = m_Rows(i).SendTime.Value
                dr(m_Rows(i).CCEMail.FieldName) = m_Rows(i).CCEMail.Value

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

        Public Sub Add(MailLogRow As Row)
            m_Rows.Add(MailLogRow)
        End Sub

        Public Sub Remove(MailLogRow As Row)
            If m_Rows.IndexOf(MailLogRow) >= 0 Then
                m_Rows.Remove(MailLogRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CreateTime As Field(Of Date) = new Field(Of Date)("CreateTime", true)
        Private FI_Seq As Field(Of Integer) = new Field(Of Integer)("Seq", true)
        Private FI_Sender As Field(Of String) = new Field(Of String)("Sender", true)
        Private FI_AcceptorCompID As Field(Of String) = new Field(Of String)("AcceptorCompID", true)
        Private FI_Acceptor As Field(Of String) = new Field(Of String)("Acceptor", true)
        Private FI_EMail As Field(Of String) = new Field(Of String)("EMail", true)
        Private FI_Subject As Field(Of String) = new Field(Of String)("Subject", true)
        Private FI_Content As Field(Of String) = new Field(Of String)("Content", true)
        Private FI_Attachment As Field(Of String) = new Field(Of String)("Attachment", true)
        Private FI_SuccessFlag As Field(Of String) = new Field(Of String)("SuccessFlag", true)
        Private FI_SendCount As Field(Of Integer) = new Field(Of Integer)("SendCount", true)
        Private FI_SendTime As Field(Of Date) = new Field(Of Date)("SendTime", true)
        Private FI_CCEMail As Field(Of String) = new Field(Of String)("CCEMail", true)
        Private m_FieldNames As String() = { "CreateTime", "Seq", "Sender", "AcceptorCompID", "Acceptor", "EMail", "Subject", "Content", "Attachment", "SuccessFlag" _
                                    , "SendCount", "SendTime", "CCEMail" }
        Private m_PrimaryFields As String() = { "CreateTime", "Seq" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CreateTime"
                    Return FI_CreateTime.Value
                Case "Seq"
                    Return FI_Seq.Value
                Case "Sender"
                    Return FI_Sender.Value
                Case "AcceptorCompID"
                    Return FI_AcceptorCompID.Value
                Case "Acceptor"
                    Return FI_Acceptor.Value
                Case "EMail"
                    Return FI_EMail.Value
                Case "Subject"
                    Return FI_Subject.Value
                Case "Content"
                    Return FI_Content.Value
                Case "Attachment"
                    Return FI_Attachment.Value
                Case "SuccessFlag"
                    Return FI_SuccessFlag.Value
                Case "SendCount"
                    Return FI_SendCount.Value
                Case "SendTime"
                    Return FI_SendTime.Value
                Case "CCEMail"
                    Return FI_CCEMail.Value
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Sub SetFieldValue(ByVal fieldName As String, ByVal value As Object)
            Select Case fieldName
                Case "CreateTime"
                    FI_CreateTime.SetValue(value)
                Case "Seq"
                    FI_Seq.SetValue(value)
                Case "Sender"
                    FI_Sender.SetValue(value)
                Case "AcceptorCompID"
                    FI_AcceptorCompID.SetValue(value)
                Case "Acceptor"
                    FI_Acceptor.SetValue(value)
                Case "EMail"
                    FI_EMail.SetValue(value)
                Case "Subject"
                    FI_Subject.SetValue(value)
                Case "Content"
                    FI_Content.SetValue(value)
                Case "Attachment"
                    FI_Attachment.SetValue(value)
                Case "SuccessFlag"
                    FI_SuccessFlag.SetValue(value)
                Case "SendCount"
                    FI_SendCount.SetValue(value)
                Case "SendTime"
                    FI_SendTime.SetValue(value)
                Case "CCEMail"
                    FI_CCEMail.SetValue(value)
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
                Case "CreateTime"
                    return FI_CreateTime.Updated
                Case "Seq"
                    return FI_Seq.Updated
                Case "Sender"
                    return FI_Sender.Updated
                Case "AcceptorCompID"
                    return FI_AcceptorCompID.Updated
                Case "Acceptor"
                    return FI_Acceptor.Updated
                Case "EMail"
                    return FI_EMail.Updated
                Case "Subject"
                    return FI_Subject.Updated
                Case "Content"
                    return FI_Content.Updated
                Case "Attachment"
                    return FI_Attachment.Updated
                Case "SuccessFlag"
                    return FI_SuccessFlag.Updated
                Case "SendCount"
                    return FI_SendCount.Updated
                Case "SendTime"
                    return FI_SendTime.Updated
                Case "CCEMail"
                    return FI_CCEMail.Updated
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public Function CreateUpdateSQL(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "CreateTime"
                    return FI_CreateTime.CreateUpdateSQL
                Case "Seq"
                    return FI_Seq.CreateUpdateSQL
                Case "Sender"
                    return FI_Sender.CreateUpdateSQL
                Case "AcceptorCompID"
                    return FI_AcceptorCompID.CreateUpdateSQL
                Case "Acceptor"
                    return FI_Acceptor.CreateUpdateSQL
                Case "EMail"
                    return FI_EMail.CreateUpdateSQL
                Case "Subject"
                    return FI_Subject.CreateUpdateSQL
                Case "Content"
                    return FI_Content.CreateUpdateSQL
                Case "Attachment"
                    return FI_Attachment.CreateUpdateSQL
                Case "SuccessFlag"
                    return FI_SuccessFlag.CreateUpdateSQL
                Case "SendCount"
                    return FI_SendCount.CreateUpdateSQL
                Case "SendTime"
                    return FI_SendTime.CreateUpdateSQL
                Case "CCEMail"
                    return FI_CCEMail.CreateUpdateSQL
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
            FI_CreateTime.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_Seq.SetInitValue(0)
            FI_Sender.SetInitValue("")
            FI_AcceptorCompID.SetInitValue("")
            FI_Acceptor.SetInitValue("")
            FI_EMail.SetInitValue("")
            FI_Subject.SetInitValue("")
            FI_Content.SetInitValue("")
            FI_Attachment.SetInitValue("")
            FI_SuccessFlag.SetInitValue("0")
            FI_SendCount.SetInitValue(0)
            FI_SendTime.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_CCEMail.SetInitValue("")
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CreateTime.SetInitValue(dr("CreateTime"))
            FI_Seq.SetInitValue(dr("Seq"))
            FI_Sender.SetInitValue(dr("Sender"))
            FI_AcceptorCompID.SetInitValue(dr("AcceptorCompID"))
            FI_Acceptor.SetInitValue(dr("Acceptor"))
            FI_EMail.SetInitValue(dr("EMail"))
            FI_Subject.SetInitValue(dr("Subject"))
            FI_Content.SetInitValue(dr("Content"))
            FI_Attachment.SetInitValue(dr("Attachment"))
            FI_SuccessFlag.SetInitValue(dr("SuccessFlag"))
            FI_SendCount.SetInitValue(dr("SendCount"))
            FI_SendTime.SetInitValue(dr("SendTime"))
            FI_CCEMail.SetInitValue(dr("CCEMail"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CreateTime.Updated = False
            FI_Seq.Updated = False
            FI_Sender.Updated = False
            FI_AcceptorCompID.Updated = False
            FI_Acceptor.Updated = False
            FI_EMail.Updated = False
            FI_Subject.Updated = False
            FI_Content.Updated = False
            FI_Attachment.Updated = False
            FI_SuccessFlag.Updated = False
            FI_SendCount.Updated = False
            FI_SendTime.Updated = False
            FI_CCEMail.Updated = False
        End Sub

        Public ReadOnly Property CreateTime As Field(Of Date) 
            Get
                Return FI_CreateTime
            End Get
        End Property

        Public ReadOnly Property Seq As Field(Of Integer) 
            Get
                Return FI_Seq
            End Get
        End Property

        Public ReadOnly Property Sender As Field(Of String) 
            Get
                Return FI_Sender
            End Get
        End Property

        Public ReadOnly Property AcceptorCompID As Field(Of String) 
            Get
                Return FI_AcceptorCompID
            End Get
        End Property

        Public ReadOnly Property Acceptor As Field(Of String) 
            Get
                Return FI_Acceptor
            End Get
        End Property

        Public ReadOnly Property EMail As Field(Of String) 
            Get
                Return FI_EMail
            End Get
        End Property

        Public ReadOnly Property Subject As Field(Of String) 
            Get
                Return FI_Subject
            End Get
        End Property

        Public ReadOnly Property Content As Field(Of String) 
            Get
                Return FI_Content
            End Get
        End Property

        Public ReadOnly Property Attachment As Field(Of String) 
            Get
                Return FI_Attachment
            End Get
        End Property

        Public ReadOnly Property SuccessFlag As Field(Of String) 
            Get
                Return FI_SuccessFlag
            End Get
        End Property

        Public ReadOnly Property SendCount As Field(Of Integer) 
            Get
                Return FI_SendCount
            End Get
        End Property

        Public ReadOnly Property SendTime As Field(Of Date) 
            Get
                Return FI_SendTime
            End Get
        End Property

        Public ReadOnly Property CCEMail As Field(Of String) 
            Get
                Return FI_CCEMail
            End Get
        End Property

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal MailLogRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From MailLog")
            strSQL.AppendLine("Where CreateTime = @CreateTime")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CreateTime", DbType.Date, MailLogRow.CreateTime.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, MailLogRow.Seq.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal MailLogRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From MailLog")
            strSQL.AppendLine("Where CreateTime = @CreateTime")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CreateTime", DbType.Date, MailLogRow.CreateTime.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, MailLogRow.Seq.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal MailLogRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From MailLog")
            strSQL.AppendLine("Where CreateTime = @CreateTime")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In MailLogRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CreateTime", DbType.Date, r.CreateTime.Value)
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

        Public Function DeleteRowByPrimaryKey(ByVal MailLogRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From MailLog")
            strSQL.AppendLine("Where CreateTime = @CreateTime")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In MailLogRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CreateTime", DbType.Date, r.CreateTime.Value)
                db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal MailLogRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From MailLog")
            strSQL.AppendLine("Where CreateTime = @CreateTime")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CreateTime", DbType.Date, MailLogRow.CreateTime.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, MailLogRow.Seq.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(MailLogRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From MailLog")
            strSQL.AppendLine("Where CreateTime = @CreateTime")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CreateTime", DbType.Date, MailLogRow.CreateTime.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, MailLogRow.Seq.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal MailLogRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update MailLog Set")
            For i As Integer = 0 To MailLogRow.FieldNames.Length - 1
                If Not MailLogRow.IsIdentityField(MailLogRow.FieldNames(i)) AndAlso MailLogRow.IsUpdated(MailLogRow.FieldNames(i)) AndAlso MailLogRow.CreateUpdateSQL(MailLogRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, MailLogRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CreateTime = @PKCreateTime")
            strSQL.AppendLine("And Seq = @PKSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If MailLogRow.CreateTime.Updated Then db.AddInParameter(dbcmd, "@CreateTime", DbType.Date, IIf(IsDateTimeNull(MailLogRow.CreateTime.Value), Convert.ToDateTime("1900/1/1"), MailLogRow.CreateTime.Value))
            If MailLogRow.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, MailLogRow.Seq.Value)
            If MailLogRow.Sender.Updated Then db.AddInParameter(dbcmd, "@Sender", DbType.String, MailLogRow.Sender.Value)
            If MailLogRow.AcceptorCompID.Updated Then db.AddInParameter(dbcmd, "@AcceptorCompID", DbType.String, MailLogRow.AcceptorCompID.Value)
            If MailLogRow.Acceptor.Updated Then db.AddInParameter(dbcmd, "@Acceptor", DbType.String, MailLogRow.Acceptor.Value)
            If MailLogRow.EMail.Updated Then db.AddInParameter(dbcmd, "@EMail", DbType.String, MailLogRow.EMail.Value)
            If MailLogRow.Subject.Updated Then db.AddInParameter(dbcmd, "@Subject", DbType.String, MailLogRow.Subject.Value)
            If MailLogRow.Content.Updated Then db.AddInParameter(dbcmd, "@Content", DbType.String, MailLogRow.Content.Value)
            If MailLogRow.Attachment.Updated Then db.AddInParameter(dbcmd, "@Attachment", DbType.String, MailLogRow.Attachment.Value)
            If MailLogRow.SuccessFlag.Updated Then db.AddInParameter(dbcmd, "@SuccessFlag", DbType.String, MailLogRow.SuccessFlag.Value)
            If MailLogRow.SendCount.Updated Then db.AddInParameter(dbcmd, "@SendCount", DbType.Int32, MailLogRow.SendCount.Value)
            If MailLogRow.SendTime.Updated Then db.AddInParameter(dbcmd, "@SendTime", DbType.Date, IIf(IsDateTimeNull(MailLogRow.SendTime.Value), Convert.ToDateTime("1900/1/1"), MailLogRow.SendTime.Value))
            If MailLogRow.CCEMail.Updated Then db.AddInParameter(dbcmd, "@CCEMail", DbType.String, MailLogRow.CCEMail.Value)
            db.AddInParameter(dbcmd, "@PKCreateTime", DbType.Date, IIf(MailLogRow.LoadFromDataRow, MailLogRow.CreateTime.OldValue, MailLogRow.CreateTime.Value))
            db.AddInParameter(dbcmd, "@PKSeq", DbType.Int32, IIf(MailLogRow.LoadFromDataRow, MailLogRow.Seq.OldValue, MailLogRow.Seq.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal MailLogRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update MailLog Set")
            For i As Integer = 0 To MailLogRow.FieldNames.Length - 1
                If Not MailLogRow.IsIdentityField(MailLogRow.FieldNames(i)) AndAlso MailLogRow.IsUpdated(MailLogRow.FieldNames(i)) AndAlso MailLogRow.CreateUpdateSQL(MailLogRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, MailLogRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CreateTime = @PKCreateTime")
            strSQL.AppendLine("And Seq = @PKSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If MailLogRow.CreateTime.Updated Then db.AddInParameter(dbcmd, "@CreateTime", DbType.Date, IIf(IsDateTimeNull(MailLogRow.CreateTime.Value), Convert.ToDateTime("1900/1/1"), MailLogRow.CreateTime.Value))
            If MailLogRow.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, MailLogRow.Seq.Value)
            If MailLogRow.Sender.Updated Then db.AddInParameter(dbcmd, "@Sender", DbType.String, MailLogRow.Sender.Value)
            If MailLogRow.AcceptorCompID.Updated Then db.AddInParameter(dbcmd, "@AcceptorCompID", DbType.String, MailLogRow.AcceptorCompID.Value)
            If MailLogRow.Acceptor.Updated Then db.AddInParameter(dbcmd, "@Acceptor", DbType.String, MailLogRow.Acceptor.Value)
            If MailLogRow.EMail.Updated Then db.AddInParameter(dbcmd, "@EMail", DbType.String, MailLogRow.EMail.Value)
            If MailLogRow.Subject.Updated Then db.AddInParameter(dbcmd, "@Subject", DbType.String, MailLogRow.Subject.Value)
            If MailLogRow.Content.Updated Then db.AddInParameter(dbcmd, "@Content", DbType.String, MailLogRow.Content.Value)
            If MailLogRow.Attachment.Updated Then db.AddInParameter(dbcmd, "@Attachment", DbType.String, MailLogRow.Attachment.Value)
            If MailLogRow.SuccessFlag.Updated Then db.AddInParameter(dbcmd, "@SuccessFlag", DbType.String, MailLogRow.SuccessFlag.Value)
            If MailLogRow.SendCount.Updated Then db.AddInParameter(dbcmd, "@SendCount", DbType.Int32, MailLogRow.SendCount.Value)
            If MailLogRow.SendTime.Updated Then db.AddInParameter(dbcmd, "@SendTime", DbType.Date, IIf(IsDateTimeNull(MailLogRow.SendTime.Value), Convert.ToDateTime("1900/1/1"), MailLogRow.SendTime.Value))
            If MailLogRow.CCEMail.Updated Then db.AddInParameter(dbcmd, "@CCEMail", DbType.String, MailLogRow.CCEMail.Value)
            db.AddInParameter(dbcmd, "@PKCreateTime", DbType.Date, IIf(MailLogRow.LoadFromDataRow, MailLogRow.CreateTime.OldValue, MailLogRow.CreateTime.Value))
            db.AddInParameter(dbcmd, "@PKSeq", DbType.Int32, IIf(MailLogRow.LoadFromDataRow, MailLogRow.Seq.OldValue, MailLogRow.Seq.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal MailLogRow As Row()) As Integer
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
                    For Each r As Row In MailLogRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update MailLog Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where CreateTime = @PKCreateTime")
                        strSQL.AppendLine("And Seq = @PKSeq")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.CreateTime.Updated Then db.AddInParameter(dbcmd, "@CreateTime", DbType.Date, IIf(IsDateTimeNull(r.CreateTime.Value), Convert.ToDateTime("1900/1/1"), r.CreateTime.Value))
                        If r.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                        If r.Sender.Updated Then db.AddInParameter(dbcmd, "@Sender", DbType.String, r.Sender.Value)
                        If r.AcceptorCompID.Updated Then db.AddInParameter(dbcmd, "@AcceptorCompID", DbType.String, r.AcceptorCompID.Value)
                        If r.Acceptor.Updated Then db.AddInParameter(dbcmd, "@Acceptor", DbType.String, r.Acceptor.Value)
                        If r.EMail.Updated Then db.AddInParameter(dbcmd, "@EMail", DbType.String, r.EMail.Value)
                        If r.Subject.Updated Then db.AddInParameter(dbcmd, "@Subject", DbType.String, r.Subject.Value)
                        If r.Content.Updated Then db.AddInParameter(dbcmd, "@Content", DbType.String, r.Content.Value)
                        If r.Attachment.Updated Then db.AddInParameter(dbcmd, "@Attachment", DbType.String, r.Attachment.Value)
                        If r.SuccessFlag.Updated Then db.AddInParameter(dbcmd, "@SuccessFlag", DbType.String, r.SuccessFlag.Value)
                        If r.SendCount.Updated Then db.AddInParameter(dbcmd, "@SendCount", DbType.Int32, r.SendCount.Value)
                        If r.SendTime.Updated Then db.AddInParameter(dbcmd, "@SendTime", DbType.Date, IIf(IsDateTimeNull(r.SendTime.Value), Convert.ToDateTime("1900/1/1"), r.SendTime.Value))
                        If r.CCEMail.Updated Then db.AddInParameter(dbcmd, "@CCEMail", DbType.String, r.CCEMail.Value)
                        db.AddInParameter(dbcmd, "@PKCreateTime", DbType.Date, IIf(r.LoadFromDataRow, r.CreateTime.OldValue, r.CreateTime.Value))
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

        Public Function Update(ByVal MailLogRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In MailLogRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update MailLog Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where CreateTime = @PKCreateTime")
                strSQL.AppendLine("And Seq = @PKSeq")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.CreateTime.Updated Then db.AddInParameter(dbcmd, "@CreateTime", DbType.Date, IIf(IsDateTimeNull(r.CreateTime.Value), Convert.ToDateTime("1900/1/1"), r.CreateTime.Value))
                If r.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                If r.Sender.Updated Then db.AddInParameter(dbcmd, "@Sender", DbType.String, r.Sender.Value)
                If r.AcceptorCompID.Updated Then db.AddInParameter(dbcmd, "@AcceptorCompID", DbType.String, r.AcceptorCompID.Value)
                If r.Acceptor.Updated Then db.AddInParameter(dbcmd, "@Acceptor", DbType.String, r.Acceptor.Value)
                If r.EMail.Updated Then db.AddInParameter(dbcmd, "@EMail", DbType.String, r.EMail.Value)
                If r.Subject.Updated Then db.AddInParameter(dbcmd, "@Subject", DbType.String, r.Subject.Value)
                If r.Content.Updated Then db.AddInParameter(dbcmd, "@Content", DbType.String, r.Content.Value)
                If r.Attachment.Updated Then db.AddInParameter(dbcmd, "@Attachment", DbType.String, r.Attachment.Value)
                If r.SuccessFlag.Updated Then db.AddInParameter(dbcmd, "@SuccessFlag", DbType.String, r.SuccessFlag.Value)
                If r.SendCount.Updated Then db.AddInParameter(dbcmd, "@SendCount", DbType.Int32, r.SendCount.Value)
                If r.SendTime.Updated Then db.AddInParameter(dbcmd, "@SendTime", DbType.Date, IIf(IsDateTimeNull(r.SendTime.Value), Convert.ToDateTime("1900/1/1"), r.SendTime.Value))
                If r.CCEMail.Updated Then db.AddInParameter(dbcmd, "@CCEMail", DbType.String, r.CCEMail.Value)
                db.AddInParameter(dbcmd, "@PKCreateTime", DbType.Date, IIf(r.LoadFromDataRow, r.CreateTime.OldValue, r.CreateTime.Value))
                db.AddInParameter(dbcmd, "@PKSeq", DbType.Int32, IIf(r.LoadFromDataRow, r.Seq.OldValue, r.Seq.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal MailLogRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From MailLog")
            strSQL.AppendLine("Where CreateTime = @CreateTime")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CreateTime", DbType.Date, MailLogRow.CreateTime.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, MailLogRow.Seq.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal MailLogRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From MailLog")
            strSQL.AppendLine("Where CreateTime = @CreateTime")
            strSQL.AppendLine("And Seq = @Seq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CreateTime", DbType.Date, MailLogRow.CreateTime.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, MailLogRow.Seq.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From MailLog")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal MailLogRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into MailLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CreateTime, Seq, Sender, AcceptorCompID, Acceptor, EMail, Subject, Content, Attachment,")
            strSQL.AppendLine("    SuccessFlag, SendCount, SendTime, CCEMail")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CreateTime, @Seq, @Sender, @AcceptorCompID, @Acceptor, @EMail, @Subject, @Content, @Attachment,")
            strSQL.AppendLine("    @SuccessFlag, @SendCount, @SendTime, @CCEMail")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CreateTime", DbType.Date, IIf(IsDateTimeNull(MailLogRow.CreateTime.Value), Convert.ToDateTime("1900/1/1"), MailLogRow.CreateTime.Value))
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, MailLogRow.Seq.Value)
            db.AddInParameter(dbcmd, "@Sender", DbType.String, MailLogRow.Sender.Value)
            db.AddInParameter(dbcmd, "@AcceptorCompID", DbType.String, MailLogRow.AcceptorCompID.Value)
            db.AddInParameter(dbcmd, "@Acceptor", DbType.String, MailLogRow.Acceptor.Value)
            db.AddInParameter(dbcmd, "@EMail", DbType.String, MailLogRow.EMail.Value)
            db.AddInParameter(dbcmd, "@Subject", DbType.String, MailLogRow.Subject.Value)
            db.AddInParameter(dbcmd, "@Content", DbType.String, MailLogRow.Content.Value)
            db.AddInParameter(dbcmd, "@Attachment", DbType.String, MailLogRow.Attachment.Value)
            db.AddInParameter(dbcmd, "@SuccessFlag", DbType.String, MailLogRow.SuccessFlag.Value)
            db.AddInParameter(dbcmd, "@SendCount", DbType.Int32, MailLogRow.SendCount.Value)
            db.AddInParameter(dbcmd, "@SendTime", DbType.Date, IIf(IsDateTimeNull(MailLogRow.SendTime.Value), Convert.ToDateTime("1900/1/1"), MailLogRow.SendTime.Value))
            db.AddInParameter(dbcmd, "@CCEMail", DbType.String, MailLogRow.CCEMail.Value)

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal MailLogRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into MailLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CreateTime, Seq, Sender, AcceptorCompID, Acceptor, EMail, Subject, Content, Attachment,")
            strSQL.AppendLine("    SuccessFlag, SendCount, SendTime, CCEMail")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CreateTime, @Seq, @Sender, @AcceptorCompID, @Acceptor, @EMail, @Subject, @Content, @Attachment,")
            strSQL.AppendLine("    @SuccessFlag, @SendCount, @SendTime, @CCEMail")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CreateTime", DbType.Date, IIf(IsDateTimeNull(MailLogRow.CreateTime.Value), Convert.ToDateTime("1900/1/1"), MailLogRow.CreateTime.Value))
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, MailLogRow.Seq.Value)
            db.AddInParameter(dbcmd, "@Sender", DbType.String, MailLogRow.Sender.Value)
            db.AddInParameter(dbcmd, "@AcceptorCompID", DbType.String, MailLogRow.AcceptorCompID.Value)
            db.AddInParameter(dbcmd, "@Acceptor", DbType.String, MailLogRow.Acceptor.Value)
            db.AddInParameter(dbcmd, "@EMail", DbType.String, MailLogRow.EMail.Value)
            db.AddInParameter(dbcmd, "@Subject", DbType.String, MailLogRow.Subject.Value)
            db.AddInParameter(dbcmd, "@Content", DbType.String, MailLogRow.Content.Value)
            db.AddInParameter(dbcmd, "@Attachment", DbType.String, MailLogRow.Attachment.Value)
            db.AddInParameter(dbcmd, "@SuccessFlag", DbType.String, MailLogRow.SuccessFlag.Value)
            db.AddInParameter(dbcmd, "@SendCount", DbType.Int32, MailLogRow.SendCount.Value)
            db.AddInParameter(dbcmd, "@SendTime", DbType.Date, IIf(IsDateTimeNull(MailLogRow.SendTime.Value), Convert.ToDateTime("1900/1/1"), MailLogRow.SendTime.Value))
            db.AddInParameter(dbcmd, "@CCEMail", DbType.String, MailLogRow.CCEMail.Value)

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal MailLogRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into MailLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CreateTime, Seq, Sender, AcceptorCompID, Acceptor, EMail, Subject, Content, Attachment,")
            strSQL.AppendLine("    SuccessFlag, SendCount, SendTime, CCEMail")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CreateTime, @Seq, @Sender, @AcceptorCompID, @Acceptor, @EMail, @Subject, @Content, @Attachment,")
            strSQL.AppendLine("    @SuccessFlag, @SendCount, @SendTime, @CCEMail")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In MailLogRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CreateTime", DbType.Date, IIf(IsDateTimeNull(r.CreateTime.Value), Convert.ToDateTime("1900/1/1"), r.CreateTime.Value))
                        db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                        db.AddInParameter(dbcmd, "@Sender", DbType.String, r.Sender.Value)
                        db.AddInParameter(dbcmd, "@AcceptorCompID", DbType.String, r.AcceptorCompID.Value)
                        db.AddInParameter(dbcmd, "@Acceptor", DbType.String, r.Acceptor.Value)
                        db.AddInParameter(dbcmd, "@EMail", DbType.String, r.EMail.Value)
                        db.AddInParameter(dbcmd, "@Subject", DbType.String, r.Subject.Value)
                        db.AddInParameter(dbcmd, "@Content", DbType.String, r.Content.Value)
                        db.AddInParameter(dbcmd, "@Attachment", DbType.String, r.Attachment.Value)
                        db.AddInParameter(dbcmd, "@SuccessFlag", DbType.String, r.SuccessFlag.Value)
                        db.AddInParameter(dbcmd, "@SendCount", DbType.Int32, r.SendCount.Value)
                        db.AddInParameter(dbcmd, "@SendTime", DbType.Date, IIf(IsDateTimeNull(r.SendTime.Value), Convert.ToDateTime("1900/1/1"), r.SendTime.Value))
                        db.AddInParameter(dbcmd, "@CCEMail", DbType.String, r.CCEMail.Value)

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

        Public Function Insert(ByVal MailLogRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into MailLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CreateTime, Seq, Sender, AcceptorCompID, Acceptor, EMail, Subject, Content, Attachment,")
            strSQL.AppendLine("    SuccessFlag, SendCount, SendTime, CCEMail")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CreateTime, @Seq, @Sender, @AcceptorCompID, @Acceptor, @EMail, @Subject, @Content, @Attachment,")
            strSQL.AppendLine("    @SuccessFlag, @SendCount, @SendTime, @CCEMail")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In MailLogRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CreateTime", DbType.Date, IIf(IsDateTimeNull(r.CreateTime.Value), Convert.ToDateTime("1900/1/1"), r.CreateTime.Value))
                db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                db.AddInParameter(dbcmd, "@Sender", DbType.String, r.Sender.Value)
                db.AddInParameter(dbcmd, "@AcceptorCompID", DbType.String, r.AcceptorCompID.Value)
                db.AddInParameter(dbcmd, "@Acceptor", DbType.String, r.Acceptor.Value)
                db.AddInParameter(dbcmd, "@EMail", DbType.String, r.EMail.Value)
                db.AddInParameter(dbcmd, "@Subject", DbType.String, r.Subject.Value)
                db.AddInParameter(dbcmd, "@Content", DbType.String, r.Content.Value)
                db.AddInParameter(dbcmd, "@Attachment", DbType.String, r.Attachment.Value)
                db.AddInParameter(dbcmd, "@SuccessFlag", DbType.String, r.SuccessFlag.Value)
                db.AddInParameter(dbcmd, "@SendCount", DbType.Int32, r.SendCount.Value)
                db.AddInParameter(dbcmd, "@SendTime", DbType.Date, IIf(IsDateTimeNull(r.SendTime.Value), Convert.ToDateTime("1900/1/1"), r.SendTime.Value))
                db.AddInParameter(dbcmd, "@CCEMail", DbType.String, r.CCEMail.Value)

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

