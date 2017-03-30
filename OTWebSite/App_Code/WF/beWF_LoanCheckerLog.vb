'****************************************************************
' Table:WF_LoanCheckerLog
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

Namespace beWF_LoanCheckerLog
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "FlowID", "FlowCaseID", "SetupStepID", "FlowSeq", "LoanCheckID", "LoanCheckdateBeg", "LoanCheckdateEnd", "AgreeFlag", "FlowStepAction", "FlowStepOpinion" _
                                    , "LastChgID", "LastChgDate", "LogChgID", "LogChgDate", "LogDesc" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(Integer), GetType(String), GetType(Date), GetType(Date), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(Date), GetType(String), GetType(Date), GetType(String) }
        Private m_PrimaryFields As String() = {  }

        Public ReadOnly Property Rows() As beWF_LoanCheckerLog.Rows 
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
        Public Sub Transfer2Row(WF_LoanCheckerLogTable As DataTable)
            For Each dr As DataRow In WF_LoanCheckerLogTable.Rows
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
                dr(m_Rows(i).LogChgID.FieldName) = m_Rows(i).LogChgID.Value
                dr(m_Rows(i).LogChgDate.FieldName) = m_Rows(i).LogChgDate.Value
                dr(m_Rows(i).LogDesc.FieldName) = m_Rows(i).LogDesc.Value

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

        Public Sub Add(WF_LoanCheckerLogRow As Row)
            m_Rows.Add(WF_LoanCheckerLogRow)
        End Sub

        Public Sub Remove(WF_LoanCheckerLogRow As Row)
            If m_Rows.IndexOf(WF_LoanCheckerLogRow) >= 0 Then
                m_Rows.Remove(WF_LoanCheckerLogRow)
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
        Private FI_LogChgID As Field(Of String) = new Field(Of String)("LogChgID", true)
        Private FI_LogChgDate As Field(Of Date) = new Field(Of Date)("LogChgDate", true)
        Private FI_LogDesc As Field(Of String) = new Field(Of String)("LogDesc", true)
        Private m_FieldNames As String() = { "FlowID", "FlowCaseID", "SetupStepID", "FlowSeq", "LoanCheckID", "LoanCheckdateBeg", "LoanCheckdateEnd", "AgreeFlag", "FlowStepAction", "FlowStepOpinion" _
                                    , "LastChgID", "LastChgDate", "LogChgID", "LogChgDate", "LogDesc" }
        Private m_PrimaryFields As String() = {  }
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
                Case "LogChgID"
                    Return FI_LogChgID.Value
                Case "LogChgDate"
                    Return FI_LogChgDate.Value
                Case "LogDesc"
                    Return FI_LogDesc.Value
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
                Case "LogChgID"
                    FI_LogChgID.SetValue(value)
                Case "LogChgDate"
                    FI_LogChgDate.SetValue(value)
                Case "LogDesc"
                    FI_LogDesc.SetValue(value)
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
                Case "LogChgID"
                    return FI_LogChgID.Updated
                Case "LogChgDate"
                    return FI_LogChgDate.Updated
                Case "LogDesc"
                    return FI_LogDesc.Updated
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
                Case "LogChgID"
                    return FI_LogChgID.CreateUpdateSQL
                Case "LogChgDate"
                    return FI_LogChgDate.CreateUpdateSQL
                Case "LogDesc"
                    return FI_LogDesc.CreateUpdateSQL
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
            FI_LogChgID.SetInitValue(dr("LogChgID"))
            FI_LogChgDate.SetInitValue(dr("LogChgDate"))
            FI_LogDesc.SetInitValue(dr("LogDesc"))

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
            FI_LogChgID.Updated = False
            FI_LogChgDate.Updated = False
            FI_LogDesc.Updated = False
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

        Public ReadOnly Property LogChgID As Field(Of String) 
            Get
                Return FI_LogChgID
            End Get
        End Property

        Public ReadOnly Property LogChgDate As Field(Of Date) 
            Get
                Return FI_LogChgDate
            End Get
        End Property

        Public ReadOnly Property LogDesc As Field(Of String) 
            Get
                Return FI_LogDesc
            End Get
        End Property

    End Class

    Public Class Service
        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WF_LoanCheckerLog")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal WF_LoanCheckerLogRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into WF_LoanCheckerLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowID, FlowCaseID, SetupStepID, FlowSeq, LoanCheckID, LoanCheckdateBeg, LoanCheckdateEnd,")
            strSQL.AppendLine("    AgreeFlag, FlowStepAction, FlowStepOpinion, LastChgID, LastChgDate, LogChgID, LogChgDate,")
            strSQL.AppendLine("    LogDesc")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowID, @FlowCaseID, @SetupStepID, @FlowSeq, @LoanCheckID, @LoanCheckdateBeg, @LoanCheckdateEnd,")
            strSQL.AppendLine("    @AgreeFlag, @FlowStepAction, @FlowStepOpinion, @LastChgID, @LastChgDate, @LogChgID, @LogChgDate,")
            strSQL.AppendLine("    @LogDesc")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_LoanCheckerLogRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_LoanCheckerLogRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@SetupStepID", DbType.String, WF_LoanCheckerLogRow.SetupStepID.Value)
            db.AddInParameter(dbcmd, "@FlowSeq", DbType.Int32, WF_LoanCheckerLogRow.FlowSeq.Value)
            db.AddInParameter(dbcmd, "@LoanCheckID", DbType.String, WF_LoanCheckerLogRow.LoanCheckID.Value)
            db.AddInParameter(dbcmd, "@LoanCheckdateBeg", DbType.Date, IIf(IsDateTimeNull(WF_LoanCheckerLogRow.LoanCheckdateBeg.Value), DBNull.Value, WF_LoanCheckerLogRow.LoanCheckdateBeg.Value))
            db.AddInParameter(dbcmd, "@LoanCheckdateEnd", DbType.Date, IIf(IsDateTimeNull(WF_LoanCheckerLogRow.LoanCheckdateEnd.Value), DBNull.Value, WF_LoanCheckerLogRow.LoanCheckdateEnd.Value))
            db.AddInParameter(dbcmd, "@AgreeFlag", DbType.String, WF_LoanCheckerLogRow.AgreeFlag.Value)
            db.AddInParameter(dbcmd, "@FlowStepAction", DbType.String, WF_LoanCheckerLogRow.FlowStepAction.Value)
            db.AddInParameter(dbcmd, "@FlowStepOpinion", DbType.String, WF_LoanCheckerLogRow.FlowStepOpinion.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WF_LoanCheckerLogRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WF_LoanCheckerLogRow.LastChgDate.Value), DBNull.Value, WF_LoanCheckerLogRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@LogChgID", DbType.String, WF_LoanCheckerLogRow.LogChgID.Value)
            db.AddInParameter(dbcmd, "@LogChgDate", DbType.Date, IIf(IsDateTimeNull(WF_LoanCheckerLogRow.LogChgDate.Value), DBNull.Value, WF_LoanCheckerLogRow.LogChgDate.Value))
            db.AddInParameter(dbcmd, "@LogDesc", DbType.String, WF_LoanCheckerLogRow.LogDesc.Value)

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal WF_LoanCheckerLogRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into WF_LoanCheckerLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowID, FlowCaseID, SetupStepID, FlowSeq, LoanCheckID, LoanCheckdateBeg, LoanCheckdateEnd,")
            strSQL.AppendLine("    AgreeFlag, FlowStepAction, FlowStepOpinion, LastChgID, LastChgDate, LogChgID, LogChgDate,")
            strSQL.AppendLine("    LogDesc")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowID, @FlowCaseID, @SetupStepID, @FlowSeq, @LoanCheckID, @LoanCheckdateBeg, @LoanCheckdateEnd,")
            strSQL.AppendLine("    @AgreeFlag, @FlowStepAction, @FlowStepOpinion, @LastChgID, @LastChgDate, @LogChgID, @LogChgDate,")
            strSQL.AppendLine("    @LogDesc")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_LoanCheckerLogRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_LoanCheckerLogRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@SetupStepID", DbType.String, WF_LoanCheckerLogRow.SetupStepID.Value)
            db.AddInParameter(dbcmd, "@FlowSeq", DbType.Int32, WF_LoanCheckerLogRow.FlowSeq.Value)
            db.AddInParameter(dbcmd, "@LoanCheckID", DbType.String, WF_LoanCheckerLogRow.LoanCheckID.Value)
            db.AddInParameter(dbcmd, "@LoanCheckdateBeg", DbType.Date, IIf(IsDateTimeNull(WF_LoanCheckerLogRow.LoanCheckdateBeg.Value), DBNull.Value, WF_LoanCheckerLogRow.LoanCheckdateBeg.Value))
            db.AddInParameter(dbcmd, "@LoanCheckdateEnd", DbType.Date, IIf(IsDateTimeNull(WF_LoanCheckerLogRow.LoanCheckdateEnd.Value), DBNull.Value, WF_LoanCheckerLogRow.LoanCheckdateEnd.Value))
            db.AddInParameter(dbcmd, "@AgreeFlag", DbType.String, WF_LoanCheckerLogRow.AgreeFlag.Value)
            db.AddInParameter(dbcmd, "@FlowStepAction", DbType.String, WF_LoanCheckerLogRow.FlowStepAction.Value)
            db.AddInParameter(dbcmd, "@FlowStepOpinion", DbType.String, WF_LoanCheckerLogRow.FlowStepOpinion.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WF_LoanCheckerLogRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WF_LoanCheckerLogRow.LastChgDate.Value), DBNull.Value, WF_LoanCheckerLogRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@LogChgID", DbType.String, WF_LoanCheckerLogRow.LogChgID.Value)
            db.AddInParameter(dbcmd, "@LogChgDate", DbType.Date, IIf(IsDateTimeNull(WF_LoanCheckerLogRow.LogChgDate.Value), DBNull.Value, WF_LoanCheckerLogRow.LogChgDate.Value))
            db.AddInParameter(dbcmd, "@LogDesc", DbType.String, WF_LoanCheckerLogRow.LogDesc.Value)

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal WF_LoanCheckerLogRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into WF_LoanCheckerLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowID, FlowCaseID, SetupStepID, FlowSeq, LoanCheckID, LoanCheckdateBeg, LoanCheckdateEnd,")
            strSQL.AppendLine("    AgreeFlag, FlowStepAction, FlowStepOpinion, LastChgID, LastChgDate, LogChgID, LogChgDate,")
            strSQL.AppendLine("    LogDesc")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowID, @FlowCaseID, @SetupStepID, @FlowSeq, @LoanCheckID, @LoanCheckdateBeg, @LoanCheckdateEnd,")
            strSQL.AppendLine("    @AgreeFlag, @FlowStepAction, @FlowStepOpinion, @LastChgID, @LastChgDate, @LogChgID, @LogChgDate,")
            strSQL.AppendLine("    @LogDesc")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In WF_LoanCheckerLogRow
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
                        db.AddInParameter(dbcmd, "@LogChgID", DbType.String, r.LogChgID.Value)
                        db.AddInParameter(dbcmd, "@LogChgDate", DbType.Date, IIf(IsDateTimeNull(r.LogChgDate.Value), DBNull.Value, r.LogChgDate.Value))
                        db.AddInParameter(dbcmd, "@LogDesc", DbType.String, r.LogDesc.Value)

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

        Public Function Insert(ByVal WF_LoanCheckerLogRow As Row(), ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into WF_LoanCheckerLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowID, FlowCaseID, SetupStepID, FlowSeq, LoanCheckID, LoanCheckdateBeg, LoanCheckdateEnd,")
            strSQL.AppendLine("    AgreeFlag, FlowStepAction, FlowStepOpinion, LastChgID, LastChgDate, LogChgID, LogChgDate,")
            strSQL.AppendLine("    LogDesc")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowID, @FlowCaseID, @SetupStepID, @FlowSeq, @LoanCheckID, @LoanCheckdateBeg, @LoanCheckdateEnd,")
            strSQL.AppendLine("    @AgreeFlag, @FlowStepAction, @FlowStepOpinion, @LastChgID, @LastChgDate, @LogChgID, @LogChgDate,")
            strSQL.AppendLine("    @LogDesc")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In WF_LoanCheckerLogRow
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
                db.AddInParameter(dbcmd, "@LogChgID", DbType.String, r.LogChgID.Value)
                db.AddInParameter(dbcmd, "@LogChgDate", DbType.Date, IIf(IsDateTimeNull(r.LogChgDate.Value), DBNull.Value, r.LogChgDate.Value))
                db.AddInParameter(dbcmd, "@LogDesc", DbType.String, r.LogDesc.Value)

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

