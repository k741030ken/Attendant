'****************************************************************
' Table:WF_MsgDefine
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

Namespace beWF_MsgDefine
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "MsgCode", "MsgReason", "MsgUrl", "MsgKind", "OpenFlag", "OpenWSize", "DelKind", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "MsgCode" }

        Public ReadOnly Property Rows() As beWF_MsgDefine.Rows 
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
        Public Sub Transfer2Row(WF_MsgDefineTable As DataTable)
            For Each dr As DataRow In WF_MsgDefineTable.Rows
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

                dr(m_Rows(i).MsgCode.FieldName) = m_Rows(i).MsgCode.Value
                dr(m_Rows(i).MsgReason.FieldName) = m_Rows(i).MsgReason.Value
                dr(m_Rows(i).MsgUrl.FieldName) = m_Rows(i).MsgUrl.Value
                dr(m_Rows(i).MsgKind.FieldName) = m_Rows(i).MsgKind.Value
                dr(m_Rows(i).OpenFlag.FieldName) = m_Rows(i).OpenFlag.Value
                dr(m_Rows(i).OpenWSize.FieldName) = m_Rows(i).OpenWSize.Value
                dr(m_Rows(i).DelKind.FieldName) = m_Rows(i).DelKind.Value
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

        Public Sub Add(WF_MsgDefineRow As Row)
            m_Rows.Add(WF_MsgDefineRow)
        End Sub

        Public Sub Remove(WF_MsgDefineRow As Row)
            If m_Rows.IndexOf(WF_MsgDefineRow) >= 0 Then
                m_Rows.Remove(WF_MsgDefineRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_MsgCode As Field(Of String) = new Field(Of String)("MsgCode", true)
        Private FI_MsgReason As Field(Of String) = new Field(Of String)("MsgReason", true)
        Private FI_MsgUrl As Field(Of String) = new Field(Of String)("MsgUrl", true)
        Private FI_MsgKind As Field(Of String) = new Field(Of String)("MsgKind", true)
        Private FI_OpenFlag As Field(Of String) = new Field(Of String)("OpenFlag", true)
        Private FI_OpenWSize As Field(Of String) = new Field(Of String)("OpenWSize", true)
        Private FI_DelKind As Field(Of String) = new Field(Of String)("DelKind", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "MsgCode", "MsgReason", "MsgUrl", "MsgKind", "OpenFlag", "OpenWSize", "DelKind", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "MsgCode" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "MsgCode"
                    Return FI_MsgCode.Value
                Case "MsgReason"
                    Return FI_MsgReason.Value
                Case "MsgUrl"
                    Return FI_MsgUrl.Value
                Case "MsgKind"
                    Return FI_MsgKind.Value
                Case "OpenFlag"
                    Return FI_OpenFlag.Value
                Case "OpenWSize"
                    Return FI_OpenWSize.Value
                Case "DelKind"
                    Return FI_DelKind.Value
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
                Case "MsgCode"
                    FI_MsgCode.SetValue(value)
                Case "MsgReason"
                    FI_MsgReason.SetValue(value)
                Case "MsgUrl"
                    FI_MsgUrl.SetValue(value)
                Case "MsgKind"
                    FI_MsgKind.SetValue(value)
                Case "OpenFlag"
                    FI_OpenFlag.SetValue(value)
                Case "OpenWSize"
                    FI_OpenWSize.SetValue(value)
                Case "DelKind"
                    FI_DelKind.SetValue(value)
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
                Case "MsgCode"
                    return FI_MsgCode.Updated
                Case "MsgReason"
                    return FI_MsgReason.Updated
                Case "MsgUrl"
                    return FI_MsgUrl.Updated
                Case "MsgKind"
                    return FI_MsgKind.Updated
                Case "OpenFlag"
                    return FI_OpenFlag.Updated
                Case "OpenWSize"
                    return FI_OpenWSize.Updated
                Case "DelKind"
                    return FI_DelKind.Updated
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
                Case "MsgCode"
                    return FI_MsgCode.CreateUpdateSQL
                Case "MsgReason"
                    return FI_MsgReason.CreateUpdateSQL
                Case "MsgUrl"
                    return FI_MsgUrl.CreateUpdateSQL
                Case "MsgKind"
                    return FI_MsgKind.CreateUpdateSQL
                Case "OpenFlag"
                    return FI_OpenFlag.CreateUpdateSQL
                Case "OpenWSize"
                    return FI_OpenWSize.CreateUpdateSQL
                Case "DelKind"
                    return FI_DelKind.CreateUpdateSQL
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
            FI_MsgCode.SetInitValue("")
            FI_MsgReason.SetInitValue("")
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_MsgCode.SetInitValue(dr("MsgCode"))
            FI_MsgReason.SetInitValue(dr("MsgReason"))
            FI_MsgUrl.SetInitValue(dr("MsgUrl"))
            FI_MsgKind.SetInitValue(dr("MsgKind"))
            FI_OpenFlag.SetInitValue(dr("OpenFlag"))
            FI_OpenWSize.SetInitValue(dr("OpenWSize"))
            FI_DelKind.SetInitValue(dr("DelKind"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_MsgCode.Updated = False
            FI_MsgReason.Updated = False
            FI_MsgUrl.Updated = False
            FI_MsgKind.Updated = False
            FI_OpenFlag.Updated = False
            FI_OpenWSize.Updated = False
            FI_DelKind.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
        End Sub

        Public ReadOnly Property MsgCode As Field(Of String) 
            Get
                Return FI_MsgCode
            End Get
        End Property

        Public ReadOnly Property MsgReason As Field(Of String) 
            Get
                Return FI_MsgReason
            End Get
        End Property

        Public ReadOnly Property MsgUrl As Field(Of String) 
            Get
                Return FI_MsgUrl
            End Get
        End Property

        Public ReadOnly Property MsgKind As Field(Of String) 
            Get
                Return FI_MsgKind
            End Get
        End Property

        Public ReadOnly Property OpenFlag As Field(Of String) 
            Get
                Return FI_OpenFlag
            End Get
        End Property

        Public ReadOnly Property OpenWSize As Field(Of String) 
            Get
                Return FI_OpenWSize
            End Get
        End Property

        Public ReadOnly Property DelKind As Field(Of String) 
            Get
                Return FI_DelKind
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
        Public Function DeleteRowByPrimaryKey(ByVal WF_MsgDefineRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From WF_MsgDefine")
            strSQL.AppendLine("Where MsgCode = @MsgCode")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@MsgCode", DbType.String, WF_MsgDefineRow.MsgCode.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal WF_MsgDefineRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From WF_MsgDefine")
            strSQL.AppendLine("Where MsgCode = @MsgCode")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@MsgCode", DbType.String, WF_MsgDefineRow.MsgCode.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal WF_MsgDefineRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From WF_MsgDefine")
            strSQL.AppendLine("Where MsgCode = @MsgCode")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In WF_MsgDefineRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@MsgCode", DbType.String, r.MsgCode.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal WF_MsgDefineRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From WF_MsgDefine")
            strSQL.AppendLine("Where MsgCode = @MsgCode")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In WF_MsgDefineRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@MsgCode", DbType.String, r.MsgCode.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal WF_MsgDefineRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WF_MsgDefine")
            strSQL.AppendLine("Where MsgCode = @MsgCode")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@MsgCode", DbType.String, WF_MsgDefineRow.MsgCode.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(WF_MsgDefineRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WF_MsgDefine")
            strSQL.AppendLine("Where MsgCode = @MsgCode")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@MsgCode", DbType.String, WF_MsgDefineRow.MsgCode.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal WF_MsgDefineRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update WF_MsgDefine Set")
            For i As Integer = 0 To WF_MsgDefineRow.FieldNames.Length - 1
                If Not WF_MsgDefineRow.IsIdentityField(WF_MsgDefineRow.FieldNames(i)) AndAlso WF_MsgDefineRow.IsUpdated(WF_MsgDefineRow.FieldNames(i)) AndAlso WF_MsgDefineRow.CreateUpdateSQL(WF_MsgDefineRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, WF_MsgDefineRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where MsgCode = @PKMsgCode")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If WF_MsgDefineRow.MsgCode.Updated Then db.AddInParameter(dbcmd, "@MsgCode", DbType.String, WF_MsgDefineRow.MsgCode.Value)
            If WF_MsgDefineRow.MsgReason.Updated Then db.AddInParameter(dbcmd, "@MsgReason", DbType.String, WF_MsgDefineRow.MsgReason.Value)
            If WF_MsgDefineRow.MsgUrl.Updated Then db.AddInParameter(dbcmd, "@MsgUrl", DbType.String, WF_MsgDefineRow.MsgUrl.Value)
            If WF_MsgDefineRow.MsgKind.Updated Then db.AddInParameter(dbcmd, "@MsgKind", DbType.String, WF_MsgDefineRow.MsgKind.Value)
            If WF_MsgDefineRow.OpenFlag.Updated Then db.AddInParameter(dbcmd, "@OpenFlag", DbType.String, WF_MsgDefineRow.OpenFlag.Value)
            If WF_MsgDefineRow.OpenWSize.Updated Then db.AddInParameter(dbcmd, "@OpenWSize", DbType.String, WF_MsgDefineRow.OpenWSize.Value)
            If WF_MsgDefineRow.DelKind.Updated Then db.AddInParameter(dbcmd, "@DelKind", DbType.String, WF_MsgDefineRow.DelKind.Value)
            If WF_MsgDefineRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WF_MsgDefineRow.LastChgID.Value)
            If WF_MsgDefineRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WF_MsgDefineRow.LastChgDate.Value), DBNull.Value, WF_MsgDefineRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKMsgCode", DbType.String, IIf(WF_MsgDefineRow.LoadFromDataRow, WF_MsgDefineRow.MsgCode.OldValue, WF_MsgDefineRow.MsgCode.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal WF_MsgDefineRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update WF_MsgDefine Set")
            For i As Integer = 0 To WF_MsgDefineRow.FieldNames.Length - 1
                If Not WF_MsgDefineRow.IsIdentityField(WF_MsgDefineRow.FieldNames(i)) AndAlso WF_MsgDefineRow.IsUpdated(WF_MsgDefineRow.FieldNames(i)) AndAlso WF_MsgDefineRow.CreateUpdateSQL(WF_MsgDefineRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, WF_MsgDefineRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where MsgCode = @PKMsgCode")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If WF_MsgDefineRow.MsgCode.Updated Then db.AddInParameter(dbcmd, "@MsgCode", DbType.String, WF_MsgDefineRow.MsgCode.Value)
            If WF_MsgDefineRow.MsgReason.Updated Then db.AddInParameter(dbcmd, "@MsgReason", DbType.String, WF_MsgDefineRow.MsgReason.Value)
            If WF_MsgDefineRow.MsgUrl.Updated Then db.AddInParameter(dbcmd, "@MsgUrl", DbType.String, WF_MsgDefineRow.MsgUrl.Value)
            If WF_MsgDefineRow.MsgKind.Updated Then db.AddInParameter(dbcmd, "@MsgKind", DbType.String, WF_MsgDefineRow.MsgKind.Value)
            If WF_MsgDefineRow.OpenFlag.Updated Then db.AddInParameter(dbcmd, "@OpenFlag", DbType.String, WF_MsgDefineRow.OpenFlag.Value)
            If WF_MsgDefineRow.OpenWSize.Updated Then db.AddInParameter(dbcmd, "@OpenWSize", DbType.String, WF_MsgDefineRow.OpenWSize.Value)
            If WF_MsgDefineRow.DelKind.Updated Then db.AddInParameter(dbcmd, "@DelKind", DbType.String, WF_MsgDefineRow.DelKind.Value)
            If WF_MsgDefineRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WF_MsgDefineRow.LastChgID.Value)
            If WF_MsgDefineRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WF_MsgDefineRow.LastChgDate.Value), DBNull.Value, WF_MsgDefineRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKMsgCode", DbType.String, IIf(WF_MsgDefineRow.LoadFromDataRow, WF_MsgDefineRow.MsgCode.OldValue, WF_MsgDefineRow.MsgCode.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal WF_MsgDefineRow As Row()) As Integer
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
                    For Each r As Row In WF_MsgDefineRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update WF_MsgDefine Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where MsgCode = @PKMsgCode")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.MsgCode.Updated Then db.AddInParameter(dbcmd, "@MsgCode", DbType.String, r.MsgCode.Value)
                        If r.MsgReason.Updated Then db.AddInParameter(dbcmd, "@MsgReason", DbType.String, r.MsgReason.Value)
                        If r.MsgUrl.Updated Then db.AddInParameter(dbcmd, "@MsgUrl", DbType.String, r.MsgUrl.Value)
                        If r.MsgKind.Updated Then db.AddInParameter(dbcmd, "@MsgKind", DbType.String, r.MsgKind.Value)
                        If r.OpenFlag.Updated Then db.AddInParameter(dbcmd, "@OpenFlag", DbType.String, r.OpenFlag.Value)
                        If r.OpenWSize.Updated Then db.AddInParameter(dbcmd, "@OpenWSize", DbType.String, r.OpenWSize.Value)
                        If r.DelKind.Updated Then db.AddInParameter(dbcmd, "@DelKind", DbType.String, r.DelKind.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), DBNull.Value, r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKMsgCode", DbType.String, IIf(r.LoadFromDataRow, r.MsgCode.OldValue, r.MsgCode.Value))

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

        Public Function Update(ByVal WF_MsgDefineRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In WF_MsgDefineRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update WF_MsgDefine Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where MsgCode = @PKMsgCode")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.MsgCode.Updated Then db.AddInParameter(dbcmd, "@MsgCode", DbType.String, r.MsgCode.Value)
                If r.MsgReason.Updated Then db.AddInParameter(dbcmd, "@MsgReason", DbType.String, r.MsgReason.Value)
                If r.MsgUrl.Updated Then db.AddInParameter(dbcmd, "@MsgUrl", DbType.String, r.MsgUrl.Value)
                If r.MsgKind.Updated Then db.AddInParameter(dbcmd, "@MsgKind", DbType.String, r.MsgKind.Value)
                If r.OpenFlag.Updated Then db.AddInParameter(dbcmd, "@OpenFlag", DbType.String, r.OpenFlag.Value)
                If r.OpenWSize.Updated Then db.AddInParameter(dbcmd, "@OpenWSize", DbType.String, r.OpenWSize.Value)
                If r.DelKind.Updated Then db.AddInParameter(dbcmd, "@DelKind", DbType.String, r.DelKind.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), DBNull.Value, r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKMsgCode", DbType.String, IIf(r.LoadFromDataRow, r.MsgCode.OldValue, r.MsgCode.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal WF_MsgDefineRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From WF_MsgDefine")
            strSQL.AppendLine("Where MsgCode = @MsgCode")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@MsgCode", DbType.String, WF_MsgDefineRow.MsgCode.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal WF_MsgDefineRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From WF_MsgDefine")
            strSQL.AppendLine("Where MsgCode = @MsgCode")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@MsgCode", DbType.String, WF_MsgDefineRow.MsgCode.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WF_MsgDefine")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal WF_MsgDefineRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into WF_MsgDefine")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    MsgCode, MsgReason, MsgUrl, MsgKind, OpenFlag, OpenWSize, DelKind, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @MsgCode, @MsgReason, @MsgUrl, @MsgKind, @OpenFlag, @OpenWSize, @DelKind, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@MsgCode", DbType.String, WF_MsgDefineRow.MsgCode.Value)
            db.AddInParameter(dbcmd, "@MsgReason", DbType.String, WF_MsgDefineRow.MsgReason.Value)
            db.AddInParameter(dbcmd, "@MsgUrl", DbType.String, WF_MsgDefineRow.MsgUrl.Value)
            db.AddInParameter(dbcmd, "@MsgKind", DbType.String, WF_MsgDefineRow.MsgKind.Value)
            db.AddInParameter(dbcmd, "@OpenFlag", DbType.String, WF_MsgDefineRow.OpenFlag.Value)
            db.AddInParameter(dbcmd, "@OpenWSize", DbType.String, WF_MsgDefineRow.OpenWSize.Value)
            db.AddInParameter(dbcmd, "@DelKind", DbType.String, WF_MsgDefineRow.DelKind.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WF_MsgDefineRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WF_MsgDefineRow.LastChgDate.Value), DBNull.Value, WF_MsgDefineRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal WF_MsgDefineRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into WF_MsgDefine")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    MsgCode, MsgReason, MsgUrl, MsgKind, OpenFlag, OpenWSize, DelKind, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @MsgCode, @MsgReason, @MsgUrl, @MsgKind, @OpenFlag, @OpenWSize, @DelKind, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@MsgCode", DbType.String, WF_MsgDefineRow.MsgCode.Value)
            db.AddInParameter(dbcmd, "@MsgReason", DbType.String, WF_MsgDefineRow.MsgReason.Value)
            db.AddInParameter(dbcmd, "@MsgUrl", DbType.String, WF_MsgDefineRow.MsgUrl.Value)
            db.AddInParameter(dbcmd, "@MsgKind", DbType.String, WF_MsgDefineRow.MsgKind.Value)
            db.AddInParameter(dbcmd, "@OpenFlag", DbType.String, WF_MsgDefineRow.OpenFlag.Value)
            db.AddInParameter(dbcmd, "@OpenWSize", DbType.String, WF_MsgDefineRow.OpenWSize.Value)
            db.AddInParameter(dbcmd, "@DelKind", DbType.String, WF_MsgDefineRow.DelKind.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WF_MsgDefineRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WF_MsgDefineRow.LastChgDate.Value), DBNull.Value, WF_MsgDefineRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal WF_MsgDefineRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into WF_MsgDefine")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    MsgCode, MsgReason, MsgUrl, MsgKind, OpenFlag, OpenWSize, DelKind, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @MsgCode, @MsgReason, @MsgUrl, @MsgKind, @OpenFlag, @OpenWSize, @DelKind, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In WF_MsgDefineRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@MsgCode", DbType.String, r.MsgCode.Value)
                        db.AddInParameter(dbcmd, "@MsgReason", DbType.String, r.MsgReason.Value)
                        db.AddInParameter(dbcmd, "@MsgUrl", DbType.String, r.MsgUrl.Value)
                        db.AddInParameter(dbcmd, "@MsgKind", DbType.String, r.MsgKind.Value)
                        db.AddInParameter(dbcmd, "@OpenFlag", DbType.String, r.OpenFlag.Value)
                        db.AddInParameter(dbcmd, "@OpenWSize", DbType.String, r.OpenWSize.Value)
                        db.AddInParameter(dbcmd, "@DelKind", DbType.String, r.DelKind.Value)
                        db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), DBNull.Value, r.LastChgDate.Value))

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

        Public Function Insert(ByVal WF_MsgDefineRow As Row(), ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into WF_MsgDefine")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    MsgCode, MsgReason, MsgUrl, MsgKind, OpenFlag, OpenWSize, DelKind, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @MsgCode, @MsgReason, @MsgUrl, @MsgKind, @OpenFlag, @OpenWSize, @DelKind, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In WF_MsgDefineRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@MsgCode", DbType.String, r.MsgCode.Value)
                db.AddInParameter(dbcmd, "@MsgReason", DbType.String, r.MsgReason.Value)
                db.AddInParameter(dbcmd, "@MsgUrl", DbType.String, r.MsgUrl.Value)
                db.AddInParameter(dbcmd, "@MsgKind", DbType.String, r.MsgKind.Value)
                db.AddInParameter(dbcmd, "@OpenFlag", DbType.String, r.OpenFlag.Value)
                db.AddInParameter(dbcmd, "@OpenWSize", DbType.String, r.OpenWSize.Value)
                db.AddInParameter(dbcmd, "@DelKind", DbType.String, r.DelKind.Value)
                db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), DBNull.Value, r.LastChgDate.Value))

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

