'****************************************************************
' Table:WF_FlowOpinTmp
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

Namespace beWF_FlowOpinTmp
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "FlowID", "FlowCaseID", "FlowLogID", "FlowStepOpinion", "LastChgDate", "LastChgID" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(String) }
        Private m_PrimaryFields As String() = { "FlowID", "FlowCaseID", "FlowLogID" }

        Public ReadOnly Property Rows() As beWF_FlowOpinTmp.Rows 
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
        Public Sub Transfer2Row(WF_FlowOpinTmpTable As DataTable)
            For Each dr As DataRow In WF_FlowOpinTmpTable.Rows
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
                dr(m_Rows(i).FlowLogID.FieldName) = m_Rows(i).FlowLogID.Value
                dr(m_Rows(i).FlowStepOpinion.FieldName) = m_Rows(i).FlowStepOpinion.Value
                dr(m_Rows(i).LastChgDate.FieldName) = m_Rows(i).LastChgDate.Value
                dr(m_Rows(i).LastChgID.FieldName) = m_Rows(i).LastChgID.Value

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

        Public Sub Add(WF_FlowOpinTmpRow As Row)
            m_Rows.Add(WF_FlowOpinTmpRow)
        End Sub

        Public Sub Remove(WF_FlowOpinTmpRow As Row)
            If m_Rows.IndexOf(WF_FlowOpinTmpRow) >= 0 Then
                m_Rows.Remove(WF_FlowOpinTmpRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_FlowID As Field(Of String) = new Field(Of String)("FlowID", true)
        Private FI_FlowCaseID As Field(Of String) = new Field(Of String)("FlowCaseID", true)
        Private FI_FlowLogID As Field(Of String) = new Field(Of String)("FlowLogID", true)
        Private FI_FlowStepOpinion As Field(Of String) = new Field(Of String)("FlowStepOpinion", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private m_FieldNames As String() = { "FlowID", "FlowCaseID", "FlowLogID", "FlowStepOpinion", "LastChgDate", "LastChgID" }
        Private m_PrimaryFields As String() = { "FlowID", "FlowCaseID", "FlowLogID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "FlowID"
                    Return FI_FlowID.Value
                Case "FlowCaseID"
                    Return FI_FlowCaseID.Value
                Case "FlowLogID"
                    Return FI_FlowLogID.Value
                Case "FlowStepOpinion"
                    Return FI_FlowStepOpinion.Value
                Case "LastChgDate"
                    Return FI_LastChgDate.Value
                Case "LastChgID"
                    Return FI_LastChgID.Value
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
                Case "FlowLogID"
                    FI_FlowLogID.SetValue(value)
                Case "FlowStepOpinion"
                    FI_FlowStepOpinion.SetValue(value)
                Case "LastChgDate"
                    FI_LastChgDate.SetValue(value)
                Case "LastChgID"
                    FI_LastChgID.SetValue(value)
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
                Case "FlowLogID"
                    return FI_FlowLogID.Updated
                Case "FlowStepOpinion"
                    return FI_FlowStepOpinion.Updated
                Case "LastChgDate"
                    return FI_LastChgDate.Updated
                Case "LastChgID"
                    return FI_LastChgID.Updated
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
                Case "FlowLogID"
                    return FI_FlowLogID.CreateUpdateSQL
                Case "FlowStepOpinion"
                    return FI_FlowStepOpinion.CreateUpdateSQL
                Case "LastChgDate"
                    return FI_LastChgDate.CreateUpdateSQL
                Case "LastChgID"
                    return FI_LastChgID.CreateUpdateSQL
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
            FI_FlowLogID.SetInitValue("")
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_FlowID.SetInitValue(dr("FlowID"))
            FI_FlowCaseID.SetInitValue(dr("FlowCaseID"))
            FI_FlowLogID.SetInitValue(dr("FlowLogID"))
            FI_FlowStepOpinion.SetInitValue(dr("FlowStepOpinion"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_FlowID.Updated = False
            FI_FlowCaseID.Updated = False
            FI_FlowLogID.Updated = False
            FI_FlowStepOpinion.Updated = False
            FI_LastChgDate.Updated = False
            FI_LastChgID.Updated = False
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

        Public ReadOnly Property FlowLogID As Field(Of String) 
            Get
                Return FI_FlowLogID
            End Get
        End Property

        Public ReadOnly Property FlowStepOpinion As Field(Of String) 
            Get
                Return FI_FlowStepOpinion
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

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal WF_FlowOpinTmpRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From WF_FlowOpinTmp")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And FlowLogID = @FlowLogID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowOpinTmpRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowOpinTmpRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, WF_FlowOpinTmpRow.FlowLogID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal WF_FlowOpinTmpRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From WF_FlowOpinTmp")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And FlowLogID = @FlowLogID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowOpinTmpRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowOpinTmpRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, WF_FlowOpinTmpRow.FlowLogID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal WF_FlowOpinTmpRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From WF_FlowOpinTmp")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And FlowLogID = @FlowLogID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In WF_FlowOpinTmpRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                        db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, r.FlowCaseID.Value)
                        db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, r.FlowLogID.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal WF_FlowOpinTmpRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From WF_FlowOpinTmp")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And FlowLogID = @FlowLogID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In WF_FlowOpinTmpRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, r.FlowCaseID.Value)
                db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, r.FlowLogID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal WF_FlowOpinTmpRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WF_FlowOpinTmp")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And FlowLogID = @FlowLogID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowOpinTmpRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowOpinTmpRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, WF_FlowOpinTmpRow.FlowLogID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(WF_FlowOpinTmpRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WF_FlowOpinTmp")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And FlowLogID = @FlowLogID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowOpinTmpRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowOpinTmpRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, WF_FlowOpinTmpRow.FlowLogID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal WF_FlowOpinTmpRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update WF_FlowOpinTmp Set")
            For i As Integer = 0 To WF_FlowOpinTmpRow.FieldNames.Length - 1
                If Not WF_FlowOpinTmpRow.IsIdentityField(WF_FlowOpinTmpRow.FieldNames(i)) AndAlso WF_FlowOpinTmpRow.IsUpdated(WF_FlowOpinTmpRow.FieldNames(i)) AndAlso WF_FlowOpinTmpRow.CreateUpdateSQL(WF_FlowOpinTmpRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, WF_FlowOpinTmpRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where FlowID = @PKFlowID")
            strSQL.AppendLine("And FlowCaseID = @PKFlowCaseID")
            strSQL.AppendLine("And FlowLogID = @PKFlowLogID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If WF_FlowOpinTmpRow.FlowID.Updated Then db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowOpinTmpRow.FlowID.Value)
            If WF_FlowOpinTmpRow.FlowCaseID.Updated Then db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowOpinTmpRow.FlowCaseID.Value)
            If WF_FlowOpinTmpRow.FlowLogID.Updated Then db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, WF_FlowOpinTmpRow.FlowLogID.Value)
            If WF_FlowOpinTmpRow.FlowStepOpinion.Updated Then db.AddInParameter(dbcmd, "@FlowStepOpinion", DbType.String, WF_FlowOpinTmpRow.FlowStepOpinion.Value)
            If WF_FlowOpinTmpRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WF_FlowOpinTmpRow.LastChgDate.Value), DBNull.Value, WF_FlowOpinTmpRow.LastChgDate.Value))
            If WF_FlowOpinTmpRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WF_FlowOpinTmpRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@PKFlowID", DbType.String, IIf(WF_FlowOpinTmpRow.LoadFromDataRow, WF_FlowOpinTmpRow.FlowID.OldValue, WF_FlowOpinTmpRow.FlowID.Value))
            db.AddInParameter(dbcmd, "@PKFlowCaseID", DbType.String, IIf(WF_FlowOpinTmpRow.LoadFromDataRow, WF_FlowOpinTmpRow.FlowCaseID.OldValue, WF_FlowOpinTmpRow.FlowCaseID.Value))
            db.AddInParameter(dbcmd, "@PKFlowLogID", DbType.String, IIf(WF_FlowOpinTmpRow.LoadFromDataRow, WF_FlowOpinTmpRow.FlowLogID.OldValue, WF_FlowOpinTmpRow.FlowLogID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal WF_FlowOpinTmpRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update WF_FlowOpinTmp Set")
            For i As Integer = 0 To WF_FlowOpinTmpRow.FieldNames.Length - 1
                If Not WF_FlowOpinTmpRow.IsIdentityField(WF_FlowOpinTmpRow.FieldNames(i)) AndAlso WF_FlowOpinTmpRow.IsUpdated(WF_FlowOpinTmpRow.FieldNames(i)) AndAlso WF_FlowOpinTmpRow.CreateUpdateSQL(WF_FlowOpinTmpRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, WF_FlowOpinTmpRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where FlowID = @PKFlowID")
            strSQL.AppendLine("And FlowCaseID = @PKFlowCaseID")
            strSQL.AppendLine("And FlowLogID = @PKFlowLogID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If WF_FlowOpinTmpRow.FlowID.Updated Then db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowOpinTmpRow.FlowID.Value)
            If WF_FlowOpinTmpRow.FlowCaseID.Updated Then db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowOpinTmpRow.FlowCaseID.Value)
            If WF_FlowOpinTmpRow.FlowLogID.Updated Then db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, WF_FlowOpinTmpRow.FlowLogID.Value)
            If WF_FlowOpinTmpRow.FlowStepOpinion.Updated Then db.AddInParameter(dbcmd, "@FlowStepOpinion", DbType.String, WF_FlowOpinTmpRow.FlowStepOpinion.Value)
            If WF_FlowOpinTmpRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WF_FlowOpinTmpRow.LastChgDate.Value), DBNull.Value, WF_FlowOpinTmpRow.LastChgDate.Value))
            If WF_FlowOpinTmpRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WF_FlowOpinTmpRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@PKFlowID", DbType.String, IIf(WF_FlowOpinTmpRow.LoadFromDataRow, WF_FlowOpinTmpRow.FlowID.OldValue, WF_FlowOpinTmpRow.FlowID.Value))
            db.AddInParameter(dbcmd, "@PKFlowCaseID", DbType.String, IIf(WF_FlowOpinTmpRow.LoadFromDataRow, WF_FlowOpinTmpRow.FlowCaseID.OldValue, WF_FlowOpinTmpRow.FlowCaseID.Value))
            db.AddInParameter(dbcmd, "@PKFlowLogID", DbType.String, IIf(WF_FlowOpinTmpRow.LoadFromDataRow, WF_FlowOpinTmpRow.FlowLogID.OldValue, WF_FlowOpinTmpRow.FlowLogID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal WF_FlowOpinTmpRow As Row()) As Integer
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
                    For Each r As Row In WF_FlowOpinTmpRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update WF_FlowOpinTmp Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where FlowID = @PKFlowID")
                        strSQL.AppendLine("And FlowCaseID = @PKFlowCaseID")
                        strSQL.AppendLine("And FlowLogID = @PKFlowLogID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.FlowID.Updated Then db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                        If r.FlowCaseID.Updated Then db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, r.FlowCaseID.Value)
                        If r.FlowLogID.Updated Then db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, r.FlowLogID.Value)
                        If r.FlowStepOpinion.Updated Then db.AddInParameter(dbcmd, "@FlowStepOpinion", DbType.String, r.FlowStepOpinion.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), DBNull.Value, r.LastChgDate.Value))
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        db.AddInParameter(dbcmd, "@PKFlowID", DbType.String, IIf(r.LoadFromDataRow, r.FlowID.OldValue, r.FlowID.Value))
                        db.AddInParameter(dbcmd, "@PKFlowCaseID", DbType.String, IIf(r.LoadFromDataRow, r.FlowCaseID.OldValue, r.FlowCaseID.Value))
                        db.AddInParameter(dbcmd, "@PKFlowLogID", DbType.String, IIf(r.LoadFromDataRow, r.FlowLogID.OldValue, r.FlowLogID.Value))

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

        Public Function Update(ByVal WF_FlowOpinTmpRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In WF_FlowOpinTmpRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update WF_FlowOpinTmp Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where FlowID = @PKFlowID")
                strSQL.AppendLine("And FlowCaseID = @PKFlowCaseID")
                strSQL.AppendLine("And FlowLogID = @PKFlowLogID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.FlowID.Updated Then db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                If r.FlowCaseID.Updated Then db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, r.FlowCaseID.Value)
                If r.FlowLogID.Updated Then db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, r.FlowLogID.Value)
                If r.FlowStepOpinion.Updated Then db.AddInParameter(dbcmd, "@FlowStepOpinion", DbType.String, r.FlowStepOpinion.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), DBNull.Value, r.LastChgDate.Value))
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                db.AddInParameter(dbcmd, "@PKFlowID", DbType.String, IIf(r.LoadFromDataRow, r.FlowID.OldValue, r.FlowID.Value))
                db.AddInParameter(dbcmd, "@PKFlowCaseID", DbType.String, IIf(r.LoadFromDataRow, r.FlowCaseID.OldValue, r.FlowCaseID.Value))
                db.AddInParameter(dbcmd, "@PKFlowLogID", DbType.String, IIf(r.LoadFromDataRow, r.FlowLogID.OldValue, r.FlowLogID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal WF_FlowOpinTmpRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From WF_FlowOpinTmp")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And FlowLogID = @FlowLogID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowOpinTmpRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowOpinTmpRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, WF_FlowOpinTmpRow.FlowLogID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal WF_FlowOpinTmpRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From WF_FlowOpinTmp")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And FlowLogID = @FlowLogID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowOpinTmpRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowOpinTmpRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, WF_FlowOpinTmpRow.FlowLogID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WF_FlowOpinTmp")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal WF_FlowOpinTmpRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into WF_FlowOpinTmp")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowID, FlowCaseID, FlowLogID, FlowStepOpinion, LastChgDate, LastChgID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowID, @FlowCaseID, @FlowLogID, @FlowStepOpinion, @LastChgDate, @LastChgID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowOpinTmpRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowOpinTmpRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, WF_FlowOpinTmpRow.FlowLogID.Value)
            db.AddInParameter(dbcmd, "@FlowStepOpinion", DbType.String, WF_FlowOpinTmpRow.FlowStepOpinion.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WF_FlowOpinTmpRow.LastChgDate.Value), DBNull.Value, WF_FlowOpinTmpRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WF_FlowOpinTmpRow.LastChgID.Value)

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal WF_FlowOpinTmpRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into WF_FlowOpinTmp")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowID, FlowCaseID, FlowLogID, FlowStepOpinion, LastChgDate, LastChgID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowID, @FlowCaseID, @FlowLogID, @FlowStepOpinion, @LastChgDate, @LastChgID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowOpinTmpRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowOpinTmpRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, WF_FlowOpinTmpRow.FlowLogID.Value)
            db.AddInParameter(dbcmd, "@FlowStepOpinion", DbType.String, WF_FlowOpinTmpRow.FlowStepOpinion.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WF_FlowOpinTmpRow.LastChgDate.Value), DBNull.Value, WF_FlowOpinTmpRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WF_FlowOpinTmpRow.LastChgID.Value)

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal WF_FlowOpinTmpRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into WF_FlowOpinTmp")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowID, FlowCaseID, FlowLogID, FlowStepOpinion, LastChgDate, LastChgID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowID, @FlowCaseID, @FlowLogID, @FlowStepOpinion, @LastChgDate, @LastChgID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In WF_FlowOpinTmpRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                        db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, r.FlowCaseID.Value)
                        db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, r.FlowLogID.Value)
                        db.AddInParameter(dbcmd, "@FlowStepOpinion", DbType.String, r.FlowStepOpinion.Value)
                        db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), DBNull.Value, r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)

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

        Public Function Insert(ByVal WF_FlowOpinTmpRow As Row(), ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into WF_FlowOpinTmp")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowID, FlowCaseID, FlowLogID, FlowStepOpinion, LastChgDate, LastChgID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowID, @FlowCaseID, @FlowLogID, @FlowStepOpinion, @LastChgDate, @LastChgID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In WF_FlowOpinTmpRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, r.FlowCaseID.Value)
                db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, r.FlowLogID.Value)
                db.AddInParameter(dbcmd, "@FlowStepOpinion", DbType.String, r.FlowStepOpinion.Value)
                db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), DBNull.Value, r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)

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

