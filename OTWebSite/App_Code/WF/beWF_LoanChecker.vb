'****************************************************************
' Table:WF_LoanChecker
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

Namespace beWF_LoanChecker
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "FlowID", "FlowCaseID", "SetupStepID", "FlowSeq", "LoanCheckID", "LoanCheckdateBeg", "LoanCheckdateEnd", "AgreeFlag", "FlowStepAction", "FlowStepOpinion" _
                                    , "LastChgID", "LastChgDate", "LoanCheckDeptID", "LoanCheckDeptName", "FlowLogID" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(Integer), GetType(String), GetType(Date), GetType(Date), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(Date), GetType(String), GetType(String), GetType(String) }
        Private m_PrimaryFields As String() = { "FlowID", "FlowCaseID", "SetupStepID", "FlowSeq" }

        Public ReadOnly Property Rows() As beWF_LoanChecker.Rows 
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
        Public Sub Transfer2Row(WF_LoanCheckerTable As DataTable)
            For Each dr As DataRow In WF_LoanCheckerTable.Rows
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
                dr(m_Rows(i).SetupStepID.FieldName) = m_Rows(i).SetupStepID.Value
                dr(m_Rows(i).FlowSeq.FieldName) = m_Rows(i).FlowSeq.Value
                dr(m_Rows(i).LoanCheckID.FieldName) = m_Rows(i).LoanCheckID.Value
                dr(m_Rows(i).LoanCheckdateBeg.FieldName) = m_Rows(i).LoanCheckdateBeg.Value
                dr(m_Rows(i).LoanCheckdateEnd.FieldName) = m_Rows(i).LoanCheckdateEnd.Value
                dr(m_Rows(i).AgreeFlag.FieldName) = m_Rows(i).AgreeFlag.Value
                dr(m_Rows(i).FlowStepAction.FieldName) = m_Rows(i).FlowStepAction.Value
                dr(m_Rows(i).FlowStepOpinion.FieldName) = m_Rows(i).FlowStepOpinion.Value
                dr(m_Rows(i).LastChgID.FieldName) = m_Rows(i).LastChgID.Value
                dr(m_Rows(i).LastChgDate.FieldName) = m_Rows(i).LastChgDate.Value
                dr(m_Rows(i).LoanCheckDeptID.FieldName) = m_Rows(i).LoanCheckDeptID.Value
                dr(m_Rows(i).LoanCheckDeptName.FieldName) = m_Rows(i).LoanCheckDeptName.Value
                dr(m_Rows(i).FlowLogID.FieldName) = m_Rows(i).FlowLogID.Value

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

        Public Sub Add(WF_LoanCheckerRow As Row)
            m_Rows.Add(WF_LoanCheckerRow)
        End Sub

        Public Sub Remove(WF_LoanCheckerRow As Row)
            If m_Rows.IndexOf(WF_LoanCheckerRow) >= 0 Then
                m_Rows.Remove(WF_LoanCheckerRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_FlowID As Field(Of String) = new Field(Of String)("FlowID", true)
        Private FI_FlowCaseID As Field(Of String) = new Field(Of String)("FlowCaseID", true)
        Private FI_SetupStepID As Field(Of String) = new Field(Of String)("SetupStepID", true)
        Private FI_FlowSeq As Field(Of Integer) = new Field(Of Integer)("FlowSeq", true)
        Private FI_LoanCheckID As Field(Of String) = new Field(Of String)("LoanCheckID", true)
        Private FI_LoanCheckdateBeg As Field(Of Date) = new Field(Of Date)("LoanCheckdateBeg", true)
        Private FI_LoanCheckdateEnd As Field(Of Date) = new Field(Of Date)("LoanCheckdateEnd", true)
        Private FI_AgreeFlag As Field(Of String) = new Field(Of String)("AgreeFlag", true)
        Private FI_FlowStepAction As Field(Of String) = new Field(Of String)("FlowStepAction", true)
        Private FI_FlowStepOpinion As Field(Of String) = new Field(Of String)("FlowStepOpinion", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private FI_LoanCheckDeptID As Field(Of String) = new Field(Of String)("LoanCheckDeptID", true)
        Private FI_LoanCheckDeptName As Field(Of String) = new Field(Of String)("LoanCheckDeptName", true)
        Private FI_FlowLogID As Field(Of String) = new Field(Of String)("FlowLogID", true)
        Private m_FieldNames As String() = { "FlowID", "FlowCaseID", "SetupStepID", "FlowSeq", "LoanCheckID", "LoanCheckdateBeg", "LoanCheckdateEnd", "AgreeFlag", "FlowStepAction", "FlowStepOpinion" _
                                    , "LastChgID", "LastChgDate", "LoanCheckDeptID", "LoanCheckDeptName", "FlowLogID" }
        Private m_PrimaryFields As String() = { "FlowID", "FlowCaseID", "SetupStepID", "FlowSeq" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "FlowID"
                    Return FI_FlowID.Value
                Case "FlowCaseID"
                    Return FI_FlowCaseID.Value
                Case "SetupStepID"
                    Return FI_SetupStepID.Value
                Case "FlowSeq"
                    Return FI_FlowSeq.Value
                Case "LoanCheckID"
                    Return FI_LoanCheckID.Value
                Case "LoanCheckdateBeg"
                    Return FI_LoanCheckdateBeg.Value
                Case "LoanCheckdateEnd"
                    Return FI_LoanCheckdateEnd.Value
                Case "AgreeFlag"
                    Return FI_AgreeFlag.Value
                Case "FlowStepAction"
                    Return FI_FlowStepAction.Value
                Case "FlowStepOpinion"
                    Return FI_FlowStepOpinion.Value
                Case "LastChgID"
                    Return FI_LastChgID.Value
                Case "LastChgDate"
                    Return FI_LastChgDate.Value
                Case "LoanCheckDeptID"
                    Return FI_LoanCheckDeptID.Value
                Case "LoanCheckDeptName"
                    Return FI_LoanCheckDeptName.Value
                Case "FlowLogID"
                    Return FI_FlowLogID.Value
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
                Case "SetupStepID"
                    FI_SetupStepID.SetValue(value)
                Case "FlowSeq"
                    FI_FlowSeq.SetValue(value)
                Case "LoanCheckID"
                    FI_LoanCheckID.SetValue(value)
                Case "LoanCheckdateBeg"
                    FI_LoanCheckdateBeg.SetValue(value)
                Case "LoanCheckdateEnd"
                    FI_LoanCheckdateEnd.SetValue(value)
                Case "AgreeFlag"
                    FI_AgreeFlag.SetValue(value)
                Case "FlowStepAction"
                    FI_FlowStepAction.SetValue(value)
                Case "FlowStepOpinion"
                    FI_FlowStepOpinion.SetValue(value)
                Case "LastChgID"
                    FI_LastChgID.SetValue(value)
                Case "LastChgDate"
                    FI_LastChgDate.SetValue(value)
                Case "LoanCheckDeptID"
                    FI_LoanCheckDeptID.SetValue(value)
                Case "LoanCheckDeptName"
                    FI_LoanCheckDeptName.SetValue(value)
                Case "FlowLogID"
                    FI_FlowLogID.SetValue(value)
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
                Case "SetupStepID"
                    return FI_SetupStepID.Updated
                Case "FlowSeq"
                    return FI_FlowSeq.Updated
                Case "LoanCheckID"
                    return FI_LoanCheckID.Updated
                Case "LoanCheckdateBeg"
                    return FI_LoanCheckdateBeg.Updated
                Case "LoanCheckdateEnd"
                    return FI_LoanCheckdateEnd.Updated
                Case "AgreeFlag"
                    return FI_AgreeFlag.Updated
                Case "FlowStepAction"
                    return FI_FlowStepAction.Updated
                Case "FlowStepOpinion"
                    return FI_FlowStepOpinion.Updated
                Case "LastChgID"
                    return FI_LastChgID.Updated
                Case "LastChgDate"
                    return FI_LastChgDate.Updated
                Case "LoanCheckDeptID"
                    return FI_LoanCheckDeptID.Updated
                Case "LoanCheckDeptName"
                    return FI_LoanCheckDeptName.Updated
                Case "FlowLogID"
                    return FI_FlowLogID.Updated
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
                Case "SetupStepID"
                    return FI_SetupStepID.CreateUpdateSQL
                Case "FlowSeq"
                    return FI_FlowSeq.CreateUpdateSQL
                Case "LoanCheckID"
                    return FI_LoanCheckID.CreateUpdateSQL
                Case "LoanCheckdateBeg"
                    return FI_LoanCheckdateBeg.CreateUpdateSQL
                Case "LoanCheckdateEnd"
                    return FI_LoanCheckdateEnd.CreateUpdateSQL
                Case "AgreeFlag"
                    return FI_AgreeFlag.CreateUpdateSQL
                Case "FlowStepAction"
                    return FI_FlowStepAction.CreateUpdateSQL
                Case "FlowStepOpinion"
                    return FI_FlowStepOpinion.CreateUpdateSQL
                Case "LastChgID"
                    return FI_LastChgID.CreateUpdateSQL
                Case "LastChgDate"
                    return FI_LastChgDate.CreateUpdateSQL
                Case "LoanCheckDeptID"
                    return FI_LoanCheckDeptID.CreateUpdateSQL
                Case "LoanCheckDeptName"
                    return FI_LoanCheckDeptName.CreateUpdateSQL
                Case "FlowLogID"
                    return FI_FlowLogID.CreateUpdateSQL
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
            FI_SetupStepID.SetInitValue("")
            FI_FlowSeq.SetInitValue(0)
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_FlowID.SetInitValue(dr("FlowID"))
            FI_FlowCaseID.SetInitValue(dr("FlowCaseID"))
            FI_SetupStepID.SetInitValue(dr("SetupStepID"))
            FI_FlowSeq.SetInitValue(dr("FlowSeq"))
            FI_LoanCheckID.SetInitValue(dr("LoanCheckID"))
            FI_LoanCheckdateBeg.SetInitValue(dr("LoanCheckdateBeg"))
            FI_LoanCheckdateEnd.SetInitValue(dr("LoanCheckdateEnd"))
            FI_AgreeFlag.SetInitValue(dr("AgreeFlag"))
            FI_FlowStepAction.SetInitValue(dr("FlowStepAction"))
            FI_FlowStepOpinion.SetInitValue(dr("FlowStepOpinion"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))
            FI_LoanCheckDeptID.SetInitValue(dr("LoanCheckDeptID"))
            FI_LoanCheckDeptName.SetInitValue(dr("LoanCheckDeptName"))
            FI_FlowLogID.SetInitValue(dr("FlowLogID"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_FlowID.Updated = False
            FI_FlowCaseID.Updated = False
            FI_SetupStepID.Updated = False
            FI_FlowSeq.Updated = False
            FI_LoanCheckID.Updated = False
            FI_LoanCheckdateBeg.Updated = False
            FI_LoanCheckdateEnd.Updated = False
            FI_AgreeFlag.Updated = False
            FI_FlowStepAction.Updated = False
            FI_FlowStepOpinion.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
            FI_LoanCheckDeptID.Updated = False
            FI_LoanCheckDeptName.Updated = False
            FI_FlowLogID.Updated = False
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

        Public ReadOnly Property SetupStepID As Field(Of String) 
            Get
                Return FI_SetupStepID
            End Get
        End Property

        Public ReadOnly Property FlowSeq As Field(Of Integer) 
            Get
                Return FI_FlowSeq
            End Get
        End Property

        Public ReadOnly Property LoanCheckID As Field(Of String) 
            Get
                Return FI_LoanCheckID
            End Get
        End Property

        Public ReadOnly Property LoanCheckdateBeg As Field(Of Date) 
            Get
                Return FI_LoanCheckdateBeg
            End Get
        End Property

        Public ReadOnly Property LoanCheckdateEnd As Field(Of Date) 
            Get
                Return FI_LoanCheckdateEnd
            End Get
        End Property

        Public ReadOnly Property AgreeFlag As Field(Of String) 
            Get
                Return FI_AgreeFlag
            End Get
        End Property

        Public ReadOnly Property FlowStepAction As Field(Of String) 
            Get
                Return FI_FlowStepAction
            End Get
        End Property

        Public ReadOnly Property FlowStepOpinion As Field(Of String) 
            Get
                Return FI_FlowStepOpinion
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

        Public ReadOnly Property LoanCheckDeptID As Field(Of String) 
            Get
                Return FI_LoanCheckDeptID
            End Get
        End Property

        Public ReadOnly Property LoanCheckDeptName As Field(Of String) 
            Get
                Return FI_LoanCheckDeptName
            End Get
        End Property

        Public ReadOnly Property FlowLogID As Field(Of String) 
            Get
                Return FI_FlowLogID
            End Get
        End Property

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal WF_LoanCheckerRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From WF_LoanChecker")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And SetupStepID = @SetupStepID")
            strSQL.AppendLine("And FlowSeq = @FlowSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_LoanCheckerRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_LoanCheckerRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@SetupStepID", DbType.String, WF_LoanCheckerRow.SetupStepID.Value)
            db.AddInParameter(dbcmd, "@FlowSeq", DbType.Int32, WF_LoanCheckerRow.FlowSeq.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal WF_LoanCheckerRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From WF_LoanChecker")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And SetupStepID = @SetupStepID")
            strSQL.AppendLine("And FlowSeq = @FlowSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_LoanCheckerRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_LoanCheckerRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@SetupStepID", DbType.String, WF_LoanCheckerRow.SetupStepID.Value)
            db.AddInParameter(dbcmd, "@FlowSeq", DbType.Int32, WF_LoanCheckerRow.FlowSeq.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal WF_LoanCheckerRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From WF_LoanChecker")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And SetupStepID = @SetupStepID")
            strSQL.AppendLine("And FlowSeq = @FlowSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In WF_LoanCheckerRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                        db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, r.FlowCaseID.Value)
                        db.AddInParameter(dbcmd, "@SetupStepID", DbType.String, r.SetupStepID.Value)
                        db.AddInParameter(dbcmd, "@FlowSeq", DbType.Int32, r.FlowSeq.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal WF_LoanCheckerRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From WF_LoanChecker")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And SetupStepID = @SetupStepID")
            strSQL.AppendLine("And FlowSeq = @FlowSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In WF_LoanCheckerRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, r.FlowCaseID.Value)
                db.AddInParameter(dbcmd, "@SetupStepID", DbType.String, r.SetupStepID.Value)
                db.AddInParameter(dbcmd, "@FlowSeq", DbType.Int32, r.FlowSeq.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal WF_LoanCheckerRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WF_LoanChecker")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And SetupStepID = @SetupStepID")
            strSQL.AppendLine("And FlowSeq = @FlowSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_LoanCheckerRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_LoanCheckerRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@SetupStepID", DbType.String, WF_LoanCheckerRow.SetupStepID.Value)
            db.AddInParameter(dbcmd, "@FlowSeq", DbType.Int32, WF_LoanCheckerRow.FlowSeq.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(WF_LoanCheckerRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WF_LoanChecker")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And SetupStepID = @SetupStepID")
            strSQL.AppendLine("And FlowSeq = @FlowSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_LoanCheckerRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_LoanCheckerRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@SetupStepID", DbType.String, WF_LoanCheckerRow.SetupStepID.Value)
            db.AddInParameter(dbcmd, "@FlowSeq", DbType.Int32, WF_LoanCheckerRow.FlowSeq.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal WF_LoanCheckerRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update WF_LoanChecker Set")
            For i As Integer = 0 To WF_LoanCheckerRow.FieldNames.Length - 1
                If Not WF_LoanCheckerRow.IsIdentityField(WF_LoanCheckerRow.FieldNames(i)) AndAlso WF_LoanCheckerRow.IsUpdated(WF_LoanCheckerRow.FieldNames(i)) AndAlso WF_LoanCheckerRow.CreateUpdateSQL(WF_LoanCheckerRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, WF_LoanCheckerRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where FlowID = @PKFlowID")
            strSQL.AppendLine("And FlowCaseID = @PKFlowCaseID")
            strSQL.AppendLine("And SetupStepID = @PKSetupStepID")
            strSQL.AppendLine("And FlowSeq = @PKFlowSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If WF_LoanCheckerRow.FlowID.Updated Then db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_LoanCheckerRow.FlowID.Value)
            If WF_LoanCheckerRow.FlowCaseID.Updated Then db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_LoanCheckerRow.FlowCaseID.Value)
            If WF_LoanCheckerRow.SetupStepID.Updated Then db.AddInParameter(dbcmd, "@SetupStepID", DbType.String, WF_LoanCheckerRow.SetupStepID.Value)
            If WF_LoanCheckerRow.FlowSeq.Updated Then db.AddInParameter(dbcmd, "@FlowSeq", DbType.Int32, WF_LoanCheckerRow.FlowSeq.Value)
            If WF_LoanCheckerRow.LoanCheckID.Updated Then db.AddInParameter(dbcmd, "@LoanCheckID", DbType.String, WF_LoanCheckerRow.LoanCheckID.Value)
            If WF_LoanCheckerRow.LoanCheckdateBeg.Updated Then db.AddInParameter(dbcmd, "@LoanCheckdateBeg", DbType.Date, IIf(IsDateTimeNull(WF_LoanCheckerRow.LoanCheckdateBeg.Value), DBNull.Value, WF_LoanCheckerRow.LoanCheckdateBeg.Value))
            If WF_LoanCheckerRow.LoanCheckdateEnd.Updated Then db.AddInParameter(dbcmd, "@LoanCheckdateEnd", DbType.Date, IIf(IsDateTimeNull(WF_LoanCheckerRow.LoanCheckdateEnd.Value), DBNull.Value, WF_LoanCheckerRow.LoanCheckdateEnd.Value))
            If WF_LoanCheckerRow.AgreeFlag.Updated Then db.AddInParameter(dbcmd, "@AgreeFlag", DbType.String, WF_LoanCheckerRow.AgreeFlag.Value)
            If WF_LoanCheckerRow.FlowStepAction.Updated Then db.AddInParameter(dbcmd, "@FlowStepAction", DbType.String, WF_LoanCheckerRow.FlowStepAction.Value)
            If WF_LoanCheckerRow.FlowStepOpinion.Updated Then db.AddInParameter(dbcmd, "@FlowStepOpinion", DbType.String, WF_LoanCheckerRow.FlowStepOpinion.Value)
            If WF_LoanCheckerRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WF_LoanCheckerRow.LastChgID.Value)
            If WF_LoanCheckerRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WF_LoanCheckerRow.LastChgDate.Value), DBNull.Value, WF_LoanCheckerRow.LastChgDate.Value))
            If WF_LoanCheckerRow.LoanCheckDeptID.Updated Then db.AddInParameter(dbcmd, "@LoanCheckDeptID", DbType.String, WF_LoanCheckerRow.LoanCheckDeptID.Value)
            If WF_LoanCheckerRow.LoanCheckDeptName.Updated Then db.AddInParameter(dbcmd, "@LoanCheckDeptName", DbType.String, WF_LoanCheckerRow.LoanCheckDeptName.Value)
            If WF_LoanCheckerRow.FlowLogID.Updated Then db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, WF_LoanCheckerRow.FlowLogID.Value)
            db.AddInParameter(dbcmd, "@PKFlowID", DbType.String, IIf(WF_LoanCheckerRow.LoadFromDataRow, WF_LoanCheckerRow.FlowID.OldValue, WF_LoanCheckerRow.FlowID.Value))
            db.AddInParameter(dbcmd, "@PKFlowCaseID", DbType.String, IIf(WF_LoanCheckerRow.LoadFromDataRow, WF_LoanCheckerRow.FlowCaseID.OldValue, WF_LoanCheckerRow.FlowCaseID.Value))
            db.AddInParameter(dbcmd, "@PKSetupStepID", DbType.String, IIf(WF_LoanCheckerRow.LoadFromDataRow, WF_LoanCheckerRow.SetupStepID.OldValue, WF_LoanCheckerRow.SetupStepID.Value))
            db.AddInParameter(dbcmd, "@PKFlowSeq", DbType.Int32, IIf(WF_LoanCheckerRow.LoadFromDataRow, WF_LoanCheckerRow.FlowSeq.OldValue, WF_LoanCheckerRow.FlowSeq.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal WF_LoanCheckerRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update WF_LoanChecker Set")
            For i As Integer = 0 To WF_LoanCheckerRow.FieldNames.Length - 1
                If Not WF_LoanCheckerRow.IsIdentityField(WF_LoanCheckerRow.FieldNames(i)) AndAlso WF_LoanCheckerRow.IsUpdated(WF_LoanCheckerRow.FieldNames(i)) AndAlso WF_LoanCheckerRow.CreateUpdateSQL(WF_LoanCheckerRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, WF_LoanCheckerRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where FlowID = @PKFlowID")
            strSQL.AppendLine("And FlowCaseID = @PKFlowCaseID")
            strSQL.AppendLine("And SetupStepID = @PKSetupStepID")
            strSQL.AppendLine("And FlowSeq = @PKFlowSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If WF_LoanCheckerRow.FlowID.Updated Then db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_LoanCheckerRow.FlowID.Value)
            If WF_LoanCheckerRow.FlowCaseID.Updated Then db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_LoanCheckerRow.FlowCaseID.Value)
            If WF_LoanCheckerRow.SetupStepID.Updated Then db.AddInParameter(dbcmd, "@SetupStepID", DbType.String, WF_LoanCheckerRow.SetupStepID.Value)
            If WF_LoanCheckerRow.FlowSeq.Updated Then db.AddInParameter(dbcmd, "@FlowSeq", DbType.Int32, WF_LoanCheckerRow.FlowSeq.Value)
            If WF_LoanCheckerRow.LoanCheckID.Updated Then db.AddInParameter(dbcmd, "@LoanCheckID", DbType.String, WF_LoanCheckerRow.LoanCheckID.Value)
            If WF_LoanCheckerRow.LoanCheckdateBeg.Updated Then db.AddInParameter(dbcmd, "@LoanCheckdateBeg", DbType.Date, IIf(IsDateTimeNull(WF_LoanCheckerRow.LoanCheckdateBeg.Value), DBNull.Value, WF_LoanCheckerRow.LoanCheckdateBeg.Value))
            If WF_LoanCheckerRow.LoanCheckdateEnd.Updated Then db.AddInParameter(dbcmd, "@LoanCheckdateEnd", DbType.Date, IIf(IsDateTimeNull(WF_LoanCheckerRow.LoanCheckdateEnd.Value), DBNull.Value, WF_LoanCheckerRow.LoanCheckdateEnd.Value))
            If WF_LoanCheckerRow.AgreeFlag.Updated Then db.AddInParameter(dbcmd, "@AgreeFlag", DbType.String, WF_LoanCheckerRow.AgreeFlag.Value)
            If WF_LoanCheckerRow.FlowStepAction.Updated Then db.AddInParameter(dbcmd, "@FlowStepAction", DbType.String, WF_LoanCheckerRow.FlowStepAction.Value)
            If WF_LoanCheckerRow.FlowStepOpinion.Updated Then db.AddInParameter(dbcmd, "@FlowStepOpinion", DbType.String, WF_LoanCheckerRow.FlowStepOpinion.Value)
            If WF_LoanCheckerRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WF_LoanCheckerRow.LastChgID.Value)
            If WF_LoanCheckerRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WF_LoanCheckerRow.LastChgDate.Value), DBNull.Value, WF_LoanCheckerRow.LastChgDate.Value))
            If WF_LoanCheckerRow.LoanCheckDeptID.Updated Then db.AddInParameter(dbcmd, "@LoanCheckDeptID", DbType.String, WF_LoanCheckerRow.LoanCheckDeptID.Value)
            If WF_LoanCheckerRow.LoanCheckDeptName.Updated Then db.AddInParameter(dbcmd, "@LoanCheckDeptName", DbType.String, WF_LoanCheckerRow.LoanCheckDeptName.Value)
            If WF_LoanCheckerRow.FlowLogID.Updated Then db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, WF_LoanCheckerRow.FlowLogID.Value)
            db.AddInParameter(dbcmd, "@PKFlowID", DbType.String, IIf(WF_LoanCheckerRow.LoadFromDataRow, WF_LoanCheckerRow.FlowID.OldValue, WF_LoanCheckerRow.FlowID.Value))
            db.AddInParameter(dbcmd, "@PKFlowCaseID", DbType.String, IIf(WF_LoanCheckerRow.LoadFromDataRow, WF_LoanCheckerRow.FlowCaseID.OldValue, WF_LoanCheckerRow.FlowCaseID.Value))
            db.AddInParameter(dbcmd, "@PKSetupStepID", DbType.String, IIf(WF_LoanCheckerRow.LoadFromDataRow, WF_LoanCheckerRow.SetupStepID.OldValue, WF_LoanCheckerRow.SetupStepID.Value))
            db.AddInParameter(dbcmd, "@PKFlowSeq", DbType.Int32, IIf(WF_LoanCheckerRow.LoadFromDataRow, WF_LoanCheckerRow.FlowSeq.OldValue, WF_LoanCheckerRow.FlowSeq.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal WF_LoanCheckerRow As Row()) As Integer
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
                    For Each r As Row In WF_LoanCheckerRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update WF_LoanChecker Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where FlowID = @PKFlowID")
                        strSQL.AppendLine("And FlowCaseID = @PKFlowCaseID")
                        strSQL.AppendLine("And SetupStepID = @PKSetupStepID")
                        strSQL.AppendLine("And FlowSeq = @PKFlowSeq")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.FlowID.Updated Then db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                        If r.FlowCaseID.Updated Then db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, r.FlowCaseID.Value)
                        If r.SetupStepID.Updated Then db.AddInParameter(dbcmd, "@SetupStepID", DbType.String, r.SetupStepID.Value)
                        If r.FlowSeq.Updated Then db.AddInParameter(dbcmd, "@FlowSeq", DbType.Int32, r.FlowSeq.Value)
                        If r.LoanCheckID.Updated Then db.AddInParameter(dbcmd, "@LoanCheckID", DbType.String, r.LoanCheckID.Value)
                        If r.LoanCheckdateBeg.Updated Then db.AddInParameter(dbcmd, "@LoanCheckdateBeg", DbType.Date, IIf(IsDateTimeNull(r.LoanCheckdateBeg.Value), DBNull.Value, r.LoanCheckdateBeg.Value))
                        If r.LoanCheckdateEnd.Updated Then db.AddInParameter(dbcmd, "@LoanCheckdateEnd", DbType.Date, IIf(IsDateTimeNull(r.LoanCheckdateEnd.Value), DBNull.Value, r.LoanCheckdateEnd.Value))
                        If r.AgreeFlag.Updated Then db.AddInParameter(dbcmd, "@AgreeFlag", DbType.String, r.AgreeFlag.Value)
                        If r.FlowStepAction.Updated Then db.AddInParameter(dbcmd, "@FlowStepAction", DbType.String, r.FlowStepAction.Value)
                        If r.FlowStepOpinion.Updated Then db.AddInParameter(dbcmd, "@FlowStepOpinion", DbType.String, r.FlowStepOpinion.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), DBNull.Value, r.LastChgDate.Value))
                        If r.LoanCheckDeptID.Updated Then db.AddInParameter(dbcmd, "@LoanCheckDeptID", DbType.String, r.LoanCheckDeptID.Value)
                        If r.LoanCheckDeptName.Updated Then db.AddInParameter(dbcmd, "@LoanCheckDeptName", DbType.String, r.LoanCheckDeptName.Value)
                        If r.FlowLogID.Updated Then db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, r.FlowLogID.Value)
                        db.AddInParameter(dbcmd, "@PKFlowID", DbType.String, IIf(r.LoadFromDataRow, r.FlowID.OldValue, r.FlowID.Value))
                        db.AddInParameter(dbcmd, "@PKFlowCaseID", DbType.String, IIf(r.LoadFromDataRow, r.FlowCaseID.OldValue, r.FlowCaseID.Value))
                        db.AddInParameter(dbcmd, "@PKSetupStepID", DbType.String, IIf(r.LoadFromDataRow, r.SetupStepID.OldValue, r.SetupStepID.Value))
                        db.AddInParameter(dbcmd, "@PKFlowSeq", DbType.Int32, IIf(r.LoadFromDataRow, r.FlowSeq.OldValue, r.FlowSeq.Value))

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

        Public Function Update(ByVal WF_LoanCheckerRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In WF_LoanCheckerRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update WF_LoanChecker Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where FlowID = @PKFlowID")
                strSQL.AppendLine("And FlowCaseID = @PKFlowCaseID")
                strSQL.AppendLine("And SetupStepID = @PKSetupStepID")
                strSQL.AppendLine("And FlowSeq = @PKFlowSeq")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.FlowID.Updated Then db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                If r.FlowCaseID.Updated Then db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, r.FlowCaseID.Value)
                If r.SetupStepID.Updated Then db.AddInParameter(dbcmd, "@SetupStepID", DbType.String, r.SetupStepID.Value)
                If r.FlowSeq.Updated Then db.AddInParameter(dbcmd, "@FlowSeq", DbType.Int32, r.FlowSeq.Value)
                If r.LoanCheckID.Updated Then db.AddInParameter(dbcmd, "@LoanCheckID", DbType.String, r.LoanCheckID.Value)
                If r.LoanCheckdateBeg.Updated Then db.AddInParameter(dbcmd, "@LoanCheckdateBeg", DbType.Date, IIf(IsDateTimeNull(r.LoanCheckdateBeg.Value), DBNull.Value, r.LoanCheckdateBeg.Value))
                If r.LoanCheckdateEnd.Updated Then db.AddInParameter(dbcmd, "@LoanCheckdateEnd", DbType.Date, IIf(IsDateTimeNull(r.LoanCheckdateEnd.Value), DBNull.Value, r.LoanCheckdateEnd.Value))
                If r.AgreeFlag.Updated Then db.AddInParameter(dbcmd, "@AgreeFlag", DbType.String, r.AgreeFlag.Value)
                If r.FlowStepAction.Updated Then db.AddInParameter(dbcmd, "@FlowStepAction", DbType.String, r.FlowStepAction.Value)
                If r.FlowStepOpinion.Updated Then db.AddInParameter(dbcmd, "@FlowStepOpinion", DbType.String, r.FlowStepOpinion.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), DBNull.Value, r.LastChgDate.Value))
                If r.LoanCheckDeptID.Updated Then db.AddInParameter(dbcmd, "@LoanCheckDeptID", DbType.String, r.LoanCheckDeptID.Value)
                If r.LoanCheckDeptName.Updated Then db.AddInParameter(dbcmd, "@LoanCheckDeptName", DbType.String, r.LoanCheckDeptName.Value)
                If r.FlowLogID.Updated Then db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, r.FlowLogID.Value)
                db.AddInParameter(dbcmd, "@PKFlowID", DbType.String, IIf(r.LoadFromDataRow, r.FlowID.OldValue, r.FlowID.Value))
                db.AddInParameter(dbcmd, "@PKFlowCaseID", DbType.String, IIf(r.LoadFromDataRow, r.FlowCaseID.OldValue, r.FlowCaseID.Value))
                db.AddInParameter(dbcmd, "@PKSetupStepID", DbType.String, IIf(r.LoadFromDataRow, r.SetupStepID.OldValue, r.SetupStepID.Value))
                db.AddInParameter(dbcmd, "@PKFlowSeq", DbType.Int32, IIf(r.LoadFromDataRow, r.FlowSeq.OldValue, r.FlowSeq.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal WF_LoanCheckerRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From WF_LoanChecker")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And SetupStepID = @SetupStepID")
            strSQL.AppendLine("And FlowSeq = @FlowSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_LoanCheckerRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_LoanCheckerRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@SetupStepID", DbType.String, WF_LoanCheckerRow.SetupStepID.Value)
            db.AddInParameter(dbcmd, "@FlowSeq", DbType.Int32, WF_LoanCheckerRow.FlowSeq.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal WF_LoanCheckerRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From WF_LoanChecker")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And SetupStepID = @SetupStepID")
            strSQL.AppendLine("And FlowSeq = @FlowSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_LoanCheckerRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_LoanCheckerRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@SetupStepID", DbType.String, WF_LoanCheckerRow.SetupStepID.Value)
            db.AddInParameter(dbcmd, "@FlowSeq", DbType.Int32, WF_LoanCheckerRow.FlowSeq.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WF_LoanChecker")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal WF_LoanCheckerRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into WF_LoanChecker")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowID, FlowCaseID, SetupStepID, FlowSeq, LoanCheckID, LoanCheckdateBeg, LoanCheckdateEnd,")
            strSQL.AppendLine("    AgreeFlag, FlowStepAction, FlowStepOpinion, LastChgID, LastChgDate, LoanCheckDeptID,")
            strSQL.AppendLine("    LoanCheckDeptName, FlowLogID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowID, @FlowCaseID, @SetupStepID, @FlowSeq, @LoanCheckID, @LoanCheckdateBeg, @LoanCheckdateEnd,")
            strSQL.AppendLine("    @AgreeFlag, @FlowStepAction, @FlowStepOpinion, @LastChgID, @LastChgDate, @LoanCheckDeptID,")
            strSQL.AppendLine("    @LoanCheckDeptName, @FlowLogID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_LoanCheckerRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_LoanCheckerRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@SetupStepID", DbType.String, WF_LoanCheckerRow.SetupStepID.Value)
            db.AddInParameter(dbcmd, "@FlowSeq", DbType.Int32, WF_LoanCheckerRow.FlowSeq.Value)
            db.AddInParameter(dbcmd, "@LoanCheckID", DbType.String, WF_LoanCheckerRow.LoanCheckID.Value)
            db.AddInParameter(dbcmd, "@LoanCheckdateBeg", DbType.Date, IIf(IsDateTimeNull(WF_LoanCheckerRow.LoanCheckdateBeg.Value), DBNull.Value, WF_LoanCheckerRow.LoanCheckdateBeg.Value))
            db.AddInParameter(dbcmd, "@LoanCheckdateEnd", DbType.Date, IIf(IsDateTimeNull(WF_LoanCheckerRow.LoanCheckdateEnd.Value), DBNull.Value, WF_LoanCheckerRow.LoanCheckdateEnd.Value))
            db.AddInParameter(dbcmd, "@AgreeFlag", DbType.String, WF_LoanCheckerRow.AgreeFlag.Value)
            db.AddInParameter(dbcmd, "@FlowStepAction", DbType.String, WF_LoanCheckerRow.FlowStepAction.Value)
            db.AddInParameter(dbcmd, "@FlowStepOpinion", DbType.String, WF_LoanCheckerRow.FlowStepOpinion.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WF_LoanCheckerRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WF_LoanCheckerRow.LastChgDate.Value), DBNull.Value, WF_LoanCheckerRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@LoanCheckDeptID", DbType.String, WF_LoanCheckerRow.LoanCheckDeptID.Value)
            db.AddInParameter(dbcmd, "@LoanCheckDeptName", DbType.String, WF_LoanCheckerRow.LoanCheckDeptName.Value)
            db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, WF_LoanCheckerRow.FlowLogID.Value)

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal WF_LoanCheckerRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into WF_LoanChecker")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowID, FlowCaseID, SetupStepID, FlowSeq, LoanCheckID, LoanCheckdateBeg, LoanCheckdateEnd,")
            strSQL.AppendLine("    AgreeFlag, FlowStepAction, FlowStepOpinion, LastChgID, LastChgDate, LoanCheckDeptID,")
            strSQL.AppendLine("    LoanCheckDeptName, FlowLogID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowID, @FlowCaseID, @SetupStepID, @FlowSeq, @LoanCheckID, @LoanCheckdateBeg, @LoanCheckdateEnd,")
            strSQL.AppendLine("    @AgreeFlag, @FlowStepAction, @FlowStepOpinion, @LastChgID, @LastChgDate, @LoanCheckDeptID,")
            strSQL.AppendLine("    @LoanCheckDeptName, @FlowLogID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_LoanCheckerRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_LoanCheckerRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@SetupStepID", DbType.String, WF_LoanCheckerRow.SetupStepID.Value)
            db.AddInParameter(dbcmd, "@FlowSeq", DbType.Int32, WF_LoanCheckerRow.FlowSeq.Value)
            db.AddInParameter(dbcmd, "@LoanCheckID", DbType.String, WF_LoanCheckerRow.LoanCheckID.Value)
            db.AddInParameter(dbcmd, "@LoanCheckdateBeg", DbType.Date, IIf(IsDateTimeNull(WF_LoanCheckerRow.LoanCheckdateBeg.Value), DBNull.Value, WF_LoanCheckerRow.LoanCheckdateBeg.Value))
            db.AddInParameter(dbcmd, "@LoanCheckdateEnd", DbType.Date, IIf(IsDateTimeNull(WF_LoanCheckerRow.LoanCheckdateEnd.Value), DBNull.Value, WF_LoanCheckerRow.LoanCheckdateEnd.Value))
            db.AddInParameter(dbcmd, "@AgreeFlag", DbType.String, WF_LoanCheckerRow.AgreeFlag.Value)
            db.AddInParameter(dbcmd, "@FlowStepAction", DbType.String, WF_LoanCheckerRow.FlowStepAction.Value)
            db.AddInParameter(dbcmd, "@FlowStepOpinion", DbType.String, WF_LoanCheckerRow.FlowStepOpinion.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WF_LoanCheckerRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WF_LoanCheckerRow.LastChgDate.Value), DBNull.Value, WF_LoanCheckerRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@LoanCheckDeptID", DbType.String, WF_LoanCheckerRow.LoanCheckDeptID.Value)
            db.AddInParameter(dbcmd, "@LoanCheckDeptName", DbType.String, WF_LoanCheckerRow.LoanCheckDeptName.Value)
            db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, WF_LoanCheckerRow.FlowLogID.Value)

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal WF_LoanCheckerRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into WF_LoanChecker")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowID, FlowCaseID, SetupStepID, FlowSeq, LoanCheckID, LoanCheckdateBeg, LoanCheckdateEnd,")
            strSQL.AppendLine("    AgreeFlag, FlowStepAction, FlowStepOpinion, LastChgID, LastChgDate, LoanCheckDeptID,")
            strSQL.AppendLine("    LoanCheckDeptName, FlowLogID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowID, @FlowCaseID, @SetupStepID, @FlowSeq, @LoanCheckID, @LoanCheckdateBeg, @LoanCheckdateEnd,")
            strSQL.AppendLine("    @AgreeFlag, @FlowStepAction, @FlowStepOpinion, @LastChgID, @LastChgDate, @LoanCheckDeptID,")
            strSQL.AppendLine("    @LoanCheckDeptName, @FlowLogID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In WF_LoanCheckerRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                        db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, r.FlowCaseID.Value)
                        db.AddInParameter(dbcmd, "@SetupStepID", DbType.String, r.SetupStepID.Value)
                        db.AddInParameter(dbcmd, "@FlowSeq", DbType.Int32, r.FlowSeq.Value)
                        db.AddInParameter(dbcmd, "@LoanCheckID", DbType.String, r.LoanCheckID.Value)
                        db.AddInParameter(dbcmd, "@LoanCheckdateBeg", DbType.Date, IIf(IsDateTimeNull(r.LoanCheckdateBeg.Value), DBNull.Value, r.LoanCheckdateBeg.Value))
                        db.AddInParameter(dbcmd, "@LoanCheckdateEnd", DbType.Date, IIf(IsDateTimeNull(r.LoanCheckdateEnd.Value), DBNull.Value, r.LoanCheckdateEnd.Value))
                        db.AddInParameter(dbcmd, "@AgreeFlag", DbType.String, r.AgreeFlag.Value)
                        db.AddInParameter(dbcmd, "@FlowStepAction", DbType.String, r.FlowStepAction.Value)
                        db.AddInParameter(dbcmd, "@FlowStepOpinion", DbType.String, r.FlowStepOpinion.Value)
                        db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), DBNull.Value, r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@LoanCheckDeptID", DbType.String, r.LoanCheckDeptID.Value)
                        db.AddInParameter(dbcmd, "@LoanCheckDeptName", DbType.String, r.LoanCheckDeptName.Value)
                        db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, r.FlowLogID.Value)

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

        Public Function Insert(ByVal WF_LoanCheckerRow As Row(), ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into WF_LoanChecker")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowID, FlowCaseID, SetupStepID, FlowSeq, LoanCheckID, LoanCheckdateBeg, LoanCheckdateEnd,")
            strSQL.AppendLine("    AgreeFlag, FlowStepAction, FlowStepOpinion, LastChgID, LastChgDate, LoanCheckDeptID,")
            strSQL.AppendLine("    LoanCheckDeptName, FlowLogID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowID, @FlowCaseID, @SetupStepID, @FlowSeq, @LoanCheckID, @LoanCheckdateBeg, @LoanCheckdateEnd,")
            strSQL.AppendLine("    @AgreeFlag, @FlowStepAction, @FlowStepOpinion, @LastChgID, @LastChgDate, @LoanCheckDeptID,")
            strSQL.AppendLine("    @LoanCheckDeptName, @FlowLogID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In WF_LoanCheckerRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, r.FlowCaseID.Value)
                db.AddInParameter(dbcmd, "@SetupStepID", DbType.String, r.SetupStepID.Value)
                db.AddInParameter(dbcmd, "@FlowSeq", DbType.Int32, r.FlowSeq.Value)
                db.AddInParameter(dbcmd, "@LoanCheckID", DbType.String, r.LoanCheckID.Value)
                db.AddInParameter(dbcmd, "@LoanCheckdateBeg", DbType.Date, IIf(IsDateTimeNull(r.LoanCheckdateBeg.Value), DBNull.Value, r.LoanCheckdateBeg.Value))
                db.AddInParameter(dbcmd, "@LoanCheckdateEnd", DbType.Date, IIf(IsDateTimeNull(r.LoanCheckdateEnd.Value), DBNull.Value, r.LoanCheckdateEnd.Value))
                db.AddInParameter(dbcmd, "@AgreeFlag", DbType.String, r.AgreeFlag.Value)
                db.AddInParameter(dbcmd, "@FlowStepAction", DbType.String, r.FlowStepAction.Value)
                db.AddInParameter(dbcmd, "@FlowStepOpinion", DbType.String, r.FlowStepOpinion.Value)
                db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), DBNull.Value, r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@LoanCheckDeptID", DbType.String, r.LoanCheckDeptID.Value)
                db.AddInParameter(dbcmd, "@LoanCheckDeptName", DbType.String, r.LoanCheckDeptName.Value)
                db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, r.FlowLogID.Value)

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

