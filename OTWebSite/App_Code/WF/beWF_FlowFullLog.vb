'****************************************************************
' Table:WF_FlowFullLog
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

Namespace beWF_FlowFullLog
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "FlowID", "FlowCaseID", "FlowLogBatNo", "FlowLogID", "FlowStepID", "FlowStepDesc", "FlowStepAction", "FlowStepOpinion", "FlowLogStatus", "FromBr" _
                                    , "FromBrName", "FromUser", "FromUserName", "AssignTo", "AssignToName", "ToBr", "ToBrName", "ToUser", "ToUserName", "IsProxy", "LogRemark" _
                                    , "CrDate", "UpdDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(Integer), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(Date), GetType(Date) }
        Private m_PrimaryFields As String() = { "FlowID", "FlowCaseID", "FlowLogID" }

        Public ReadOnly Property Rows() As beWF_FlowFullLog.Rows 
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
        Public Sub Transfer2Row(WF_FlowFullLogTable As DataTable)
            For Each dr As DataRow In WF_FlowFullLogTable.Rows
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
                dr(m_Rows(i).FlowLogBatNo.FieldName) = m_Rows(i).FlowLogBatNo.Value
                dr(m_Rows(i).FlowLogID.FieldName) = m_Rows(i).FlowLogID.Value
                dr(m_Rows(i).FlowStepID.FieldName) = m_Rows(i).FlowStepID.Value
                dr(m_Rows(i).FlowStepDesc.FieldName) = m_Rows(i).FlowStepDesc.Value
                dr(m_Rows(i).FlowStepAction.FieldName) = m_Rows(i).FlowStepAction.Value
                dr(m_Rows(i).FlowStepOpinion.FieldName) = m_Rows(i).FlowStepOpinion.Value
                dr(m_Rows(i).FlowLogStatus.FieldName) = m_Rows(i).FlowLogStatus.Value
                dr(m_Rows(i).FromBr.FieldName) = m_Rows(i).FromBr.Value
                dr(m_Rows(i).FromBrName.FieldName) = m_Rows(i).FromBrName.Value
                dr(m_Rows(i).FromUser.FieldName) = m_Rows(i).FromUser.Value
                dr(m_Rows(i).FromUserName.FieldName) = m_Rows(i).FromUserName.Value
                dr(m_Rows(i).AssignTo.FieldName) = m_Rows(i).AssignTo.Value
                dr(m_Rows(i).AssignToName.FieldName) = m_Rows(i).AssignToName.Value
                dr(m_Rows(i).ToBr.FieldName) = m_Rows(i).ToBr.Value
                dr(m_Rows(i).ToBrName.FieldName) = m_Rows(i).ToBrName.Value
                dr(m_Rows(i).ToUser.FieldName) = m_Rows(i).ToUser.Value
                dr(m_Rows(i).ToUserName.FieldName) = m_Rows(i).ToUserName.Value
                dr(m_Rows(i).IsProxy.FieldName) = m_Rows(i).IsProxy.Value
                dr(m_Rows(i).LogRemark.FieldName) = m_Rows(i).LogRemark.Value
                dr(m_Rows(i).CrDate.FieldName) = m_Rows(i).CrDate.Value
                dr(m_Rows(i).UpdDate.FieldName) = m_Rows(i).UpdDate.Value

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

        Public Sub Add(WF_FlowFullLogRow As Row)
            m_Rows.Add(WF_FlowFullLogRow)
        End Sub

        Public Sub Remove(WF_FlowFullLogRow As Row)
            If m_Rows.IndexOf(WF_FlowFullLogRow) >= 0 Then
                m_Rows.Remove(WF_FlowFullLogRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_FlowID As Field(Of String) = new Field(Of String)("FlowID", true)
        Private FI_FlowCaseID As Field(Of String) = new Field(Of String)("FlowCaseID", true)
        Private FI_FlowLogBatNo As Field(Of Integer) = new Field(Of Integer)("FlowLogBatNo", true)
        Private FI_FlowLogID As Field(Of String) = new Field(Of String)("FlowLogID", true)
        Private FI_FlowStepID As Field(Of String) = new Field(Of String)("FlowStepID", true)
        Private FI_FlowStepDesc As Field(Of String) = new Field(Of String)("FlowStepDesc", true)
        Private FI_FlowStepAction As Field(Of String) = new Field(Of String)("FlowStepAction", true)
        Private FI_FlowStepOpinion As Field(Of String) = new Field(Of String)("FlowStepOpinion", true)
        Private FI_FlowLogStatus As Field(Of String) = new Field(Of String)("FlowLogStatus", true)
        Private FI_FromBr As Field(Of String) = new Field(Of String)("FromBr", true)
        Private FI_FromBrName As Field(Of String) = new Field(Of String)("FromBrName", true)
        Private FI_FromUser As Field(Of String) = new Field(Of String)("FromUser", true)
        Private FI_FromUserName As Field(Of String) = new Field(Of String)("FromUserName", true)
        Private FI_AssignTo As Field(Of String) = new Field(Of String)("AssignTo", true)
        Private FI_AssignToName As Field(Of String) = new Field(Of String)("AssignToName", true)
        Private FI_ToBr As Field(Of String) = new Field(Of String)("ToBr", true)
        Private FI_ToBrName As Field(Of String) = new Field(Of String)("ToBrName", true)
        Private FI_ToUser As Field(Of String) = new Field(Of String)("ToUser", true)
        Private FI_ToUserName As Field(Of String) = new Field(Of String)("ToUserName", true)
        Private FI_IsProxy As Field(Of String) = new Field(Of String)("IsProxy", true)
        Private FI_LogRemark As Field(Of String) = new Field(Of String)("LogRemark", true)
        Private FI_CrDate As Field(Of Date) = new Field(Of Date)("CrDate", true)
        Private FI_UpdDate As Field(Of Date) = new Field(Of Date)("UpdDate", true)
        Private m_FieldNames As String() = { "FlowID", "FlowCaseID", "FlowLogBatNo", "FlowLogID", "FlowStepID", "FlowStepDesc", "FlowStepAction", "FlowStepOpinion", "FlowLogStatus", "FromBr" _
                                    , "FromBrName", "FromUser", "FromUserName", "AssignTo", "AssignToName", "ToBr", "ToBrName", "ToUser", "ToUserName", "IsProxy", "LogRemark" _
                                    , "CrDate", "UpdDate" }
        Private m_PrimaryFields As String() = { "FlowID", "FlowCaseID", "FlowLogID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "FlowID"
                    Return FI_FlowID.Value
                Case "FlowCaseID"
                    Return FI_FlowCaseID.Value
                Case "FlowLogBatNo"
                    Return FI_FlowLogBatNo.Value
                Case "FlowLogID"
                    Return FI_FlowLogID.Value
                Case "FlowStepID"
                    Return FI_FlowStepID.Value
                Case "FlowStepDesc"
                    Return FI_FlowStepDesc.Value
                Case "FlowStepAction"
                    Return FI_FlowStepAction.Value
                Case "FlowStepOpinion"
                    Return FI_FlowStepOpinion.Value
                Case "FlowLogStatus"
                    Return FI_FlowLogStatus.Value
                Case "FromBr"
                    Return FI_FromBr.Value
                Case "FromBrName"
                    Return FI_FromBrName.Value
                Case "FromUser"
                    Return FI_FromUser.Value
                Case "FromUserName"
                    Return FI_FromUserName.Value
                Case "AssignTo"
                    Return FI_AssignTo.Value
                Case "AssignToName"
                    Return FI_AssignToName.Value
                Case "ToBr"
                    Return FI_ToBr.Value
                Case "ToBrName"
                    Return FI_ToBrName.Value
                Case "ToUser"
                    Return FI_ToUser.Value
                Case "ToUserName"
                    Return FI_ToUserName.Value
                Case "IsProxy"
                    Return FI_IsProxy.Value
                Case "LogRemark"
                    Return FI_LogRemark.Value
                Case "CrDate"
                    Return FI_CrDate.Value
                Case "UpdDate"
                    Return FI_UpdDate.Value
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
                Case "FlowLogBatNo"
                    FI_FlowLogBatNo.SetValue(value)
                Case "FlowLogID"
                    FI_FlowLogID.SetValue(value)
                Case "FlowStepID"
                    FI_FlowStepID.SetValue(value)
                Case "FlowStepDesc"
                    FI_FlowStepDesc.SetValue(value)
                Case "FlowStepAction"
                    FI_FlowStepAction.SetValue(value)
                Case "FlowStepOpinion"
                    FI_FlowStepOpinion.SetValue(value)
                Case "FlowLogStatus"
                    FI_FlowLogStatus.SetValue(value)
                Case "FromBr"
                    FI_FromBr.SetValue(value)
                Case "FromBrName"
                    FI_FromBrName.SetValue(value)
                Case "FromUser"
                    FI_FromUser.SetValue(value)
                Case "FromUserName"
                    FI_FromUserName.SetValue(value)
                Case "AssignTo"
                    FI_AssignTo.SetValue(value)
                Case "AssignToName"
                    FI_AssignToName.SetValue(value)
                Case "ToBr"
                    FI_ToBr.SetValue(value)
                Case "ToBrName"
                    FI_ToBrName.SetValue(value)
                Case "ToUser"
                    FI_ToUser.SetValue(value)
                Case "ToUserName"
                    FI_ToUserName.SetValue(value)
                Case "IsProxy"
                    FI_IsProxy.SetValue(value)
                Case "LogRemark"
                    FI_LogRemark.SetValue(value)
                Case "CrDate"
                    FI_CrDate.SetValue(value)
                Case "UpdDate"
                    FI_UpdDate.SetValue(value)
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
                Case "FlowLogBatNo"
                    return FI_FlowLogBatNo.Updated
                Case "FlowLogID"
                    return FI_FlowLogID.Updated
                Case "FlowStepID"
                    return FI_FlowStepID.Updated
                Case "FlowStepDesc"
                    return FI_FlowStepDesc.Updated
                Case "FlowStepAction"
                    return FI_FlowStepAction.Updated
                Case "FlowStepOpinion"
                    return FI_FlowStepOpinion.Updated
                Case "FlowLogStatus"
                    return FI_FlowLogStatus.Updated
                Case "FromBr"
                    return FI_FromBr.Updated
                Case "FromBrName"
                    return FI_FromBrName.Updated
                Case "FromUser"
                    return FI_FromUser.Updated
                Case "FromUserName"
                    return FI_FromUserName.Updated
                Case "AssignTo"
                    return FI_AssignTo.Updated
                Case "AssignToName"
                    return FI_AssignToName.Updated
                Case "ToBr"
                    return FI_ToBr.Updated
                Case "ToBrName"
                    return FI_ToBrName.Updated
                Case "ToUser"
                    return FI_ToUser.Updated
                Case "ToUserName"
                    return FI_ToUserName.Updated
                Case "IsProxy"
                    return FI_IsProxy.Updated
                Case "LogRemark"
                    return FI_LogRemark.Updated
                Case "CrDate"
                    return FI_CrDate.Updated
                Case "UpdDate"
                    return FI_UpdDate.Updated
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
                Case "FlowLogBatNo"
                    return FI_FlowLogBatNo.CreateUpdateSQL
                Case "FlowLogID"
                    return FI_FlowLogID.CreateUpdateSQL
                Case "FlowStepID"
                    return FI_FlowStepID.CreateUpdateSQL
                Case "FlowStepDesc"
                    return FI_FlowStepDesc.CreateUpdateSQL
                Case "FlowStepAction"
                    return FI_FlowStepAction.CreateUpdateSQL
                Case "FlowStepOpinion"
                    return FI_FlowStepOpinion.CreateUpdateSQL
                Case "FlowLogStatus"
                    return FI_FlowLogStatus.CreateUpdateSQL
                Case "FromBr"
                    return FI_FromBr.CreateUpdateSQL
                Case "FromBrName"
                    return FI_FromBrName.CreateUpdateSQL
                Case "FromUser"
                    return FI_FromUser.CreateUpdateSQL
                Case "FromUserName"
                    return FI_FromUserName.CreateUpdateSQL
                Case "AssignTo"
                    return FI_AssignTo.CreateUpdateSQL
                Case "AssignToName"
                    return FI_AssignToName.CreateUpdateSQL
                Case "ToBr"
                    return FI_ToBr.CreateUpdateSQL
                Case "ToBrName"
                    return FI_ToBrName.CreateUpdateSQL
                Case "ToUser"
                    return FI_ToUser.CreateUpdateSQL
                Case "ToUserName"
                    return FI_ToUserName.CreateUpdateSQL
                Case "IsProxy"
                    return FI_IsProxy.CreateUpdateSQL
                Case "LogRemark"
                    return FI_LogRemark.CreateUpdateSQL
                Case "CrDate"
                    return FI_CrDate.CreateUpdateSQL
                Case "UpdDate"
                    return FI_UpdDate.CreateUpdateSQL
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
            FI_FlowLogBatNo.SetInitValue(dr("FlowLogBatNo"))
            FI_FlowLogID.SetInitValue(dr("FlowLogID"))
            FI_FlowStepID.SetInitValue(dr("FlowStepID"))
            FI_FlowStepDesc.SetInitValue(dr("FlowStepDesc"))
            FI_FlowStepAction.SetInitValue(dr("FlowStepAction"))
            FI_FlowStepOpinion.SetInitValue(dr("FlowStepOpinion"))
            FI_FlowLogStatus.SetInitValue(dr("FlowLogStatus"))
            FI_FromBr.SetInitValue(dr("FromBr"))
            FI_FromBrName.SetInitValue(dr("FromBrName"))
            FI_FromUser.SetInitValue(dr("FromUser"))
            FI_FromUserName.SetInitValue(dr("FromUserName"))
            FI_AssignTo.SetInitValue(dr("AssignTo"))
            FI_AssignToName.SetInitValue(dr("AssignToName"))
            FI_ToBr.SetInitValue(dr("ToBr"))
            FI_ToBrName.SetInitValue(dr("ToBrName"))
            FI_ToUser.SetInitValue(dr("ToUser"))
            FI_ToUserName.SetInitValue(dr("ToUserName"))
            FI_IsProxy.SetInitValue(dr("IsProxy"))
            FI_LogRemark.SetInitValue(dr("LogRemark"))
            FI_CrDate.SetInitValue(dr("CrDate"))
            FI_UpdDate.SetInitValue(dr("UpdDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_FlowID.Updated = False
            FI_FlowCaseID.Updated = False
            FI_FlowLogBatNo.Updated = False
            FI_FlowLogID.Updated = False
            FI_FlowStepID.Updated = False
            FI_FlowStepDesc.Updated = False
            FI_FlowStepAction.Updated = False
            FI_FlowStepOpinion.Updated = False
            FI_FlowLogStatus.Updated = False
            FI_FromBr.Updated = False
            FI_FromBrName.Updated = False
            FI_FromUser.Updated = False
            FI_FromUserName.Updated = False
            FI_AssignTo.Updated = False
            FI_AssignToName.Updated = False
            FI_ToBr.Updated = False
            FI_ToBrName.Updated = False
            FI_ToUser.Updated = False
            FI_ToUserName.Updated = False
            FI_IsProxy.Updated = False
            FI_LogRemark.Updated = False
            FI_CrDate.Updated = False
            FI_UpdDate.Updated = False
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

        Public ReadOnly Property FlowLogBatNo As Field(Of Integer) 
            Get
                Return FI_FlowLogBatNo
            End Get
        End Property

        Public ReadOnly Property FlowLogID As Field(Of String) 
            Get
                Return FI_FlowLogID
            End Get
        End Property

        Public ReadOnly Property FlowStepID As Field(Of String) 
            Get
                Return FI_FlowStepID
            End Get
        End Property

        Public ReadOnly Property FlowStepDesc As Field(Of String) 
            Get
                Return FI_FlowStepDesc
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

        Public ReadOnly Property FlowLogStatus As Field(Of String) 
            Get
                Return FI_FlowLogStatus
            End Get
        End Property

        Public ReadOnly Property FromBr As Field(Of String) 
            Get
                Return FI_FromBr
            End Get
        End Property

        Public ReadOnly Property FromBrName As Field(Of String) 
            Get
                Return FI_FromBrName
            End Get
        End Property

        Public ReadOnly Property FromUser As Field(Of String) 
            Get
                Return FI_FromUser
            End Get
        End Property

        Public ReadOnly Property FromUserName As Field(Of String) 
            Get
                Return FI_FromUserName
            End Get
        End Property

        Public ReadOnly Property AssignTo As Field(Of String) 
            Get
                Return FI_AssignTo
            End Get
        End Property

        Public ReadOnly Property AssignToName As Field(Of String) 
            Get
                Return FI_AssignToName
            End Get
        End Property

        Public ReadOnly Property ToBr As Field(Of String) 
            Get
                Return FI_ToBr
            End Get
        End Property

        Public ReadOnly Property ToBrName As Field(Of String) 
            Get
                Return FI_ToBrName
            End Get
        End Property

        Public ReadOnly Property ToUser As Field(Of String) 
            Get
                Return FI_ToUser
            End Get
        End Property

        Public ReadOnly Property ToUserName As Field(Of String) 
            Get
                Return FI_ToUserName
            End Get
        End Property

        Public ReadOnly Property IsProxy As Field(Of String) 
            Get
                Return FI_IsProxy
            End Get
        End Property

        Public ReadOnly Property LogRemark As Field(Of String) 
            Get
                Return FI_LogRemark
            End Get
        End Property

        Public ReadOnly Property CrDate As Field(Of Date) 
            Get
                Return FI_CrDate
            End Get
        End Property

        Public ReadOnly Property UpdDate As Field(Of Date) 
            Get
                Return FI_UpdDate
            End Get
        End Property

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal WF_FlowFullLogRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From WF_FlowFullLog")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And FlowLogID = @FlowLogID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowFullLogRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowFullLogRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, WF_FlowFullLogRow.FlowLogID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal WF_FlowFullLogRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From WF_FlowFullLog")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And FlowLogID = @FlowLogID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowFullLogRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowFullLogRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, WF_FlowFullLogRow.FlowLogID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal WF_FlowFullLogRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From WF_FlowFullLog")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And FlowLogID = @FlowLogID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In WF_FlowFullLogRow
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

        Public Function DeleteRowByPrimaryKey(ByVal WF_FlowFullLogRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From WF_FlowFullLog")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And FlowLogID = @FlowLogID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In WF_FlowFullLogRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, r.FlowCaseID.Value)
                db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, r.FlowLogID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal WF_FlowFullLogRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WF_FlowFullLog")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And FlowLogID = @FlowLogID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowFullLogRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowFullLogRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, WF_FlowFullLogRow.FlowLogID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(WF_FlowFullLogRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WF_FlowFullLog")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And FlowLogID = @FlowLogID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowFullLogRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowFullLogRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, WF_FlowFullLogRow.FlowLogID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal WF_FlowFullLogRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update WF_FlowFullLog Set")
            For i As Integer = 0 To WF_FlowFullLogRow.FieldNames.Length - 1
                If Not WF_FlowFullLogRow.IsIdentityField(WF_FlowFullLogRow.FieldNames(i)) AndAlso WF_FlowFullLogRow.IsUpdated(WF_FlowFullLogRow.FieldNames(i)) AndAlso WF_FlowFullLogRow.CreateUpdateSQL(WF_FlowFullLogRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, WF_FlowFullLogRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where FlowID = @PKFlowID")
            strSQL.AppendLine("And FlowCaseID = @PKFlowCaseID")
            strSQL.AppendLine("And FlowLogID = @PKFlowLogID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If WF_FlowFullLogRow.FlowID.Updated Then db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowFullLogRow.FlowID.Value)
            If WF_FlowFullLogRow.FlowCaseID.Updated Then db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowFullLogRow.FlowCaseID.Value)
            If WF_FlowFullLogRow.FlowLogBatNo.Updated Then db.AddInParameter(dbcmd, "@FlowLogBatNo", DbType.Int32, WF_FlowFullLogRow.FlowLogBatNo.Value)
            If WF_FlowFullLogRow.FlowLogID.Updated Then db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, WF_FlowFullLogRow.FlowLogID.Value)
            If WF_FlowFullLogRow.FlowStepID.Updated Then db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, WF_FlowFullLogRow.FlowStepID.Value)
            If WF_FlowFullLogRow.FlowStepDesc.Updated Then db.AddInParameter(dbcmd, "@FlowStepDesc", DbType.String, WF_FlowFullLogRow.FlowStepDesc.Value)
            If WF_FlowFullLogRow.FlowStepAction.Updated Then db.AddInParameter(dbcmd, "@FlowStepAction", DbType.String, WF_FlowFullLogRow.FlowStepAction.Value)
            If WF_FlowFullLogRow.FlowStepOpinion.Updated Then db.AddInParameter(dbcmd, "@FlowStepOpinion", DbType.String, WF_FlowFullLogRow.FlowStepOpinion.Value)
            If WF_FlowFullLogRow.FlowLogStatus.Updated Then db.AddInParameter(dbcmd, "@FlowLogStatus", DbType.String, WF_FlowFullLogRow.FlowLogStatus.Value)
            If WF_FlowFullLogRow.FromBr.Updated Then db.AddInParameter(dbcmd, "@FromBr", DbType.String, WF_FlowFullLogRow.FromBr.Value)
            If WF_FlowFullLogRow.FromBrName.Updated Then db.AddInParameter(dbcmd, "@FromBrName", DbType.String, WF_FlowFullLogRow.FromBrName.Value)
            If WF_FlowFullLogRow.FromUser.Updated Then db.AddInParameter(dbcmd, "@FromUser", DbType.String, WF_FlowFullLogRow.FromUser.Value)
            If WF_FlowFullLogRow.FromUserName.Updated Then db.AddInParameter(dbcmd, "@FromUserName", DbType.String, WF_FlowFullLogRow.FromUserName.Value)
            If WF_FlowFullLogRow.AssignTo.Updated Then db.AddInParameter(dbcmd, "@AssignTo", DbType.String, WF_FlowFullLogRow.AssignTo.Value)
            If WF_FlowFullLogRow.AssignToName.Updated Then db.AddInParameter(dbcmd, "@AssignToName", DbType.String, WF_FlowFullLogRow.AssignToName.Value)
            If WF_FlowFullLogRow.ToBr.Updated Then db.AddInParameter(dbcmd, "@ToBr", DbType.String, WF_FlowFullLogRow.ToBr.Value)
            If WF_FlowFullLogRow.ToBrName.Updated Then db.AddInParameter(dbcmd, "@ToBrName", DbType.String, WF_FlowFullLogRow.ToBrName.Value)
            If WF_FlowFullLogRow.ToUser.Updated Then db.AddInParameter(dbcmd, "@ToUser", DbType.String, WF_FlowFullLogRow.ToUser.Value)
            If WF_FlowFullLogRow.ToUserName.Updated Then db.AddInParameter(dbcmd, "@ToUserName", DbType.String, WF_FlowFullLogRow.ToUserName.Value)
            If WF_FlowFullLogRow.IsProxy.Updated Then db.AddInParameter(dbcmd, "@IsProxy", DbType.String, WF_FlowFullLogRow.IsProxy.Value)
            If WF_FlowFullLogRow.LogRemark.Updated Then db.AddInParameter(dbcmd, "@LogRemark", DbType.String, WF_FlowFullLogRow.LogRemark.Value)
            If WF_FlowFullLogRow.CrDate.Updated Then db.AddInParameter(dbcmd, "@CrDate", DbType.Date, IIf(IsDateTimeNull(WF_FlowFullLogRow.CrDate.Value), DBNull.Value, WF_FlowFullLogRow.CrDate.Value))
            If WF_FlowFullLogRow.UpdDate.Updated Then db.AddInParameter(dbcmd, "@UpdDate", DbType.Date, IIf(IsDateTimeNull(WF_FlowFullLogRow.UpdDate.Value), DBNull.Value, WF_FlowFullLogRow.UpdDate.Value))
            db.AddInParameter(dbcmd, "@PKFlowID", DbType.String, IIf(WF_FlowFullLogRow.LoadFromDataRow, WF_FlowFullLogRow.FlowID.OldValue, WF_FlowFullLogRow.FlowID.Value))
            db.AddInParameter(dbcmd, "@PKFlowCaseID", DbType.String, IIf(WF_FlowFullLogRow.LoadFromDataRow, WF_FlowFullLogRow.FlowCaseID.OldValue, WF_FlowFullLogRow.FlowCaseID.Value))
            db.AddInParameter(dbcmd, "@PKFlowLogID", DbType.String, IIf(WF_FlowFullLogRow.LoadFromDataRow, WF_FlowFullLogRow.FlowLogID.OldValue, WF_FlowFullLogRow.FlowLogID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal WF_FlowFullLogRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update WF_FlowFullLog Set")
            For i As Integer = 0 To WF_FlowFullLogRow.FieldNames.Length - 1
                If Not WF_FlowFullLogRow.IsIdentityField(WF_FlowFullLogRow.FieldNames(i)) AndAlso WF_FlowFullLogRow.IsUpdated(WF_FlowFullLogRow.FieldNames(i)) AndAlso WF_FlowFullLogRow.CreateUpdateSQL(WF_FlowFullLogRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, WF_FlowFullLogRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where FlowID = @PKFlowID")
            strSQL.AppendLine("And FlowCaseID = @PKFlowCaseID")
            strSQL.AppendLine("And FlowLogID = @PKFlowLogID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If WF_FlowFullLogRow.FlowID.Updated Then db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowFullLogRow.FlowID.Value)
            If WF_FlowFullLogRow.FlowCaseID.Updated Then db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowFullLogRow.FlowCaseID.Value)
            If WF_FlowFullLogRow.FlowLogBatNo.Updated Then db.AddInParameter(dbcmd, "@FlowLogBatNo", DbType.Int32, WF_FlowFullLogRow.FlowLogBatNo.Value)
            If WF_FlowFullLogRow.FlowLogID.Updated Then db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, WF_FlowFullLogRow.FlowLogID.Value)
            If WF_FlowFullLogRow.FlowStepID.Updated Then db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, WF_FlowFullLogRow.FlowStepID.Value)
            If WF_FlowFullLogRow.FlowStepDesc.Updated Then db.AddInParameter(dbcmd, "@FlowStepDesc", DbType.String, WF_FlowFullLogRow.FlowStepDesc.Value)
            If WF_FlowFullLogRow.FlowStepAction.Updated Then db.AddInParameter(dbcmd, "@FlowStepAction", DbType.String, WF_FlowFullLogRow.FlowStepAction.Value)
            If WF_FlowFullLogRow.FlowStepOpinion.Updated Then db.AddInParameter(dbcmd, "@FlowStepOpinion", DbType.String, WF_FlowFullLogRow.FlowStepOpinion.Value)
            If WF_FlowFullLogRow.FlowLogStatus.Updated Then db.AddInParameter(dbcmd, "@FlowLogStatus", DbType.String, WF_FlowFullLogRow.FlowLogStatus.Value)
            If WF_FlowFullLogRow.FromBr.Updated Then db.AddInParameter(dbcmd, "@FromBr", DbType.String, WF_FlowFullLogRow.FromBr.Value)
            If WF_FlowFullLogRow.FromBrName.Updated Then db.AddInParameter(dbcmd, "@FromBrName", DbType.String, WF_FlowFullLogRow.FromBrName.Value)
            If WF_FlowFullLogRow.FromUser.Updated Then db.AddInParameter(dbcmd, "@FromUser", DbType.String, WF_FlowFullLogRow.FromUser.Value)
            If WF_FlowFullLogRow.FromUserName.Updated Then db.AddInParameter(dbcmd, "@FromUserName", DbType.String, WF_FlowFullLogRow.FromUserName.Value)
            If WF_FlowFullLogRow.AssignTo.Updated Then db.AddInParameter(dbcmd, "@AssignTo", DbType.String, WF_FlowFullLogRow.AssignTo.Value)
            If WF_FlowFullLogRow.AssignToName.Updated Then db.AddInParameter(dbcmd, "@AssignToName", DbType.String, WF_FlowFullLogRow.AssignToName.Value)
            If WF_FlowFullLogRow.ToBr.Updated Then db.AddInParameter(dbcmd, "@ToBr", DbType.String, WF_FlowFullLogRow.ToBr.Value)
            If WF_FlowFullLogRow.ToBrName.Updated Then db.AddInParameter(dbcmd, "@ToBrName", DbType.String, WF_FlowFullLogRow.ToBrName.Value)
            If WF_FlowFullLogRow.ToUser.Updated Then db.AddInParameter(dbcmd, "@ToUser", DbType.String, WF_FlowFullLogRow.ToUser.Value)
            If WF_FlowFullLogRow.ToUserName.Updated Then db.AddInParameter(dbcmd, "@ToUserName", DbType.String, WF_FlowFullLogRow.ToUserName.Value)
            If WF_FlowFullLogRow.IsProxy.Updated Then db.AddInParameter(dbcmd, "@IsProxy", DbType.String, WF_FlowFullLogRow.IsProxy.Value)
            If WF_FlowFullLogRow.LogRemark.Updated Then db.AddInParameter(dbcmd, "@LogRemark", DbType.String, WF_FlowFullLogRow.LogRemark.Value)
            If WF_FlowFullLogRow.CrDate.Updated Then db.AddInParameter(dbcmd, "@CrDate", DbType.Date, IIf(IsDateTimeNull(WF_FlowFullLogRow.CrDate.Value), DBNull.Value, WF_FlowFullLogRow.CrDate.Value))
            If WF_FlowFullLogRow.UpdDate.Updated Then db.AddInParameter(dbcmd, "@UpdDate", DbType.Date, IIf(IsDateTimeNull(WF_FlowFullLogRow.UpdDate.Value), DBNull.Value, WF_FlowFullLogRow.UpdDate.Value))
            db.AddInParameter(dbcmd, "@PKFlowID", DbType.String, IIf(WF_FlowFullLogRow.LoadFromDataRow, WF_FlowFullLogRow.FlowID.OldValue, WF_FlowFullLogRow.FlowID.Value))
            db.AddInParameter(dbcmd, "@PKFlowCaseID", DbType.String, IIf(WF_FlowFullLogRow.LoadFromDataRow, WF_FlowFullLogRow.FlowCaseID.OldValue, WF_FlowFullLogRow.FlowCaseID.Value))
            db.AddInParameter(dbcmd, "@PKFlowLogID", DbType.String, IIf(WF_FlowFullLogRow.LoadFromDataRow, WF_FlowFullLogRow.FlowLogID.OldValue, WF_FlowFullLogRow.FlowLogID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal WF_FlowFullLogRow As Row()) As Integer
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
                    For Each r As Row In WF_FlowFullLogRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update WF_FlowFullLog Set")
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
                        If r.FlowLogBatNo.Updated Then db.AddInParameter(dbcmd, "@FlowLogBatNo", DbType.Int32, r.FlowLogBatNo.Value)
                        If r.FlowLogID.Updated Then db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, r.FlowLogID.Value)
                        If r.FlowStepID.Updated Then db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, r.FlowStepID.Value)
                        If r.FlowStepDesc.Updated Then db.AddInParameter(dbcmd, "@FlowStepDesc", DbType.String, r.FlowStepDesc.Value)
                        If r.FlowStepAction.Updated Then db.AddInParameter(dbcmd, "@FlowStepAction", DbType.String, r.FlowStepAction.Value)
                        If r.FlowStepOpinion.Updated Then db.AddInParameter(dbcmd, "@FlowStepOpinion", DbType.String, r.FlowStepOpinion.Value)
                        If r.FlowLogStatus.Updated Then db.AddInParameter(dbcmd, "@FlowLogStatus", DbType.String, r.FlowLogStatus.Value)
                        If r.FromBr.Updated Then db.AddInParameter(dbcmd, "@FromBr", DbType.String, r.FromBr.Value)
                        If r.FromBrName.Updated Then db.AddInParameter(dbcmd, "@FromBrName", DbType.String, r.FromBrName.Value)
                        If r.FromUser.Updated Then db.AddInParameter(dbcmd, "@FromUser", DbType.String, r.FromUser.Value)
                        If r.FromUserName.Updated Then db.AddInParameter(dbcmd, "@FromUserName", DbType.String, r.FromUserName.Value)
                        If r.AssignTo.Updated Then db.AddInParameter(dbcmd, "@AssignTo", DbType.String, r.AssignTo.Value)
                        If r.AssignToName.Updated Then db.AddInParameter(dbcmd, "@AssignToName", DbType.String, r.AssignToName.Value)
                        If r.ToBr.Updated Then db.AddInParameter(dbcmd, "@ToBr", DbType.String, r.ToBr.Value)
                        If r.ToBrName.Updated Then db.AddInParameter(dbcmd, "@ToBrName", DbType.String, r.ToBrName.Value)
                        If r.ToUser.Updated Then db.AddInParameter(dbcmd, "@ToUser", DbType.String, r.ToUser.Value)
                        If r.ToUserName.Updated Then db.AddInParameter(dbcmd, "@ToUserName", DbType.String, r.ToUserName.Value)
                        If r.IsProxy.Updated Then db.AddInParameter(dbcmd, "@IsProxy", DbType.String, r.IsProxy.Value)
                        If r.LogRemark.Updated Then db.AddInParameter(dbcmd, "@LogRemark", DbType.String, r.LogRemark.Value)
                        If r.CrDate.Updated Then db.AddInParameter(dbcmd, "@CrDate", DbType.Date, IIf(IsDateTimeNull(r.CrDate.Value), DBNull.Value, r.CrDate.Value))
                        If r.UpdDate.Updated Then db.AddInParameter(dbcmd, "@UpdDate", DbType.Date, IIf(IsDateTimeNull(r.UpdDate.Value), DBNull.Value, r.UpdDate.Value))
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

        Public Function Update(ByVal WF_FlowFullLogRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In WF_FlowFullLogRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update WF_FlowFullLog Set")
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
                If r.FlowLogBatNo.Updated Then db.AddInParameter(dbcmd, "@FlowLogBatNo", DbType.Int32, r.FlowLogBatNo.Value)
                If r.FlowLogID.Updated Then db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, r.FlowLogID.Value)
                If r.FlowStepID.Updated Then db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, r.FlowStepID.Value)
                If r.FlowStepDesc.Updated Then db.AddInParameter(dbcmd, "@FlowStepDesc", DbType.String, r.FlowStepDesc.Value)
                If r.FlowStepAction.Updated Then db.AddInParameter(dbcmd, "@FlowStepAction", DbType.String, r.FlowStepAction.Value)
                If r.FlowStepOpinion.Updated Then db.AddInParameter(dbcmd, "@FlowStepOpinion", DbType.String, r.FlowStepOpinion.Value)
                If r.FlowLogStatus.Updated Then db.AddInParameter(dbcmd, "@FlowLogStatus", DbType.String, r.FlowLogStatus.Value)
                If r.FromBr.Updated Then db.AddInParameter(dbcmd, "@FromBr", DbType.String, r.FromBr.Value)
                If r.FromBrName.Updated Then db.AddInParameter(dbcmd, "@FromBrName", DbType.String, r.FromBrName.Value)
                If r.FromUser.Updated Then db.AddInParameter(dbcmd, "@FromUser", DbType.String, r.FromUser.Value)
                If r.FromUserName.Updated Then db.AddInParameter(dbcmd, "@FromUserName", DbType.String, r.FromUserName.Value)
                If r.AssignTo.Updated Then db.AddInParameter(dbcmd, "@AssignTo", DbType.String, r.AssignTo.Value)
                If r.AssignToName.Updated Then db.AddInParameter(dbcmd, "@AssignToName", DbType.String, r.AssignToName.Value)
                If r.ToBr.Updated Then db.AddInParameter(dbcmd, "@ToBr", DbType.String, r.ToBr.Value)
                If r.ToBrName.Updated Then db.AddInParameter(dbcmd, "@ToBrName", DbType.String, r.ToBrName.Value)
                If r.ToUser.Updated Then db.AddInParameter(dbcmd, "@ToUser", DbType.String, r.ToUser.Value)
                If r.ToUserName.Updated Then db.AddInParameter(dbcmd, "@ToUserName", DbType.String, r.ToUserName.Value)
                If r.IsProxy.Updated Then db.AddInParameter(dbcmd, "@IsProxy", DbType.String, r.IsProxy.Value)
                If r.LogRemark.Updated Then db.AddInParameter(dbcmd, "@LogRemark", DbType.String, r.LogRemark.Value)
                If r.CrDate.Updated Then db.AddInParameter(dbcmd, "@CrDate", DbType.Date, IIf(IsDateTimeNull(r.CrDate.Value), DBNull.Value, r.CrDate.Value))
                If r.UpdDate.Updated Then db.AddInParameter(dbcmd, "@UpdDate", DbType.Date, IIf(IsDateTimeNull(r.UpdDate.Value), DBNull.Value, r.UpdDate.Value))
                db.AddInParameter(dbcmd, "@PKFlowID", DbType.String, IIf(r.LoadFromDataRow, r.FlowID.OldValue, r.FlowID.Value))
                db.AddInParameter(dbcmd, "@PKFlowCaseID", DbType.String, IIf(r.LoadFromDataRow, r.FlowCaseID.OldValue, r.FlowCaseID.Value))
                db.AddInParameter(dbcmd, "@PKFlowLogID", DbType.String, IIf(r.LoadFromDataRow, r.FlowLogID.OldValue, r.FlowLogID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal WF_FlowFullLogRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From WF_FlowFullLog")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And FlowLogID = @FlowLogID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowFullLogRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowFullLogRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, WF_FlowFullLogRow.FlowLogID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal WF_FlowFullLogRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From WF_FlowFullLog")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And FlowLogID = @FlowLogID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowFullLogRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowFullLogRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, WF_FlowFullLogRow.FlowLogID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WF_FlowFullLog")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal WF_FlowFullLogRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into WF_FlowFullLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowID, FlowCaseID, FlowLogBatNo, FlowLogID, FlowStepID, FlowStepDesc, FlowStepAction,")
            strSQL.AppendLine("    FlowStepOpinion, FlowLogStatus, FromBr, FromBrName, FromUser, FromUserName, AssignTo,")
            strSQL.AppendLine("    AssignToName, ToBr, ToBrName, ToUser, ToUserName, IsProxy, LogRemark, CrDate, UpdDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowID, @FlowCaseID, @FlowLogBatNo, @FlowLogID, @FlowStepID, @FlowStepDesc, @FlowStepAction,")
            strSQL.AppendLine("    @FlowStepOpinion, @FlowLogStatus, @FromBr, @FromBrName, @FromUser, @FromUserName, @AssignTo,")
            strSQL.AppendLine("    @AssignToName, @ToBr, @ToBrName, @ToUser, @ToUserName, @IsProxy, @LogRemark, @CrDate, @UpdDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowFullLogRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowFullLogRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@FlowLogBatNo", DbType.Int32, WF_FlowFullLogRow.FlowLogBatNo.Value)
            db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, WF_FlowFullLogRow.FlowLogID.Value)
            db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, WF_FlowFullLogRow.FlowStepID.Value)
            db.AddInParameter(dbcmd, "@FlowStepDesc", DbType.String, WF_FlowFullLogRow.FlowStepDesc.Value)
            db.AddInParameter(dbcmd, "@FlowStepAction", DbType.String, WF_FlowFullLogRow.FlowStepAction.Value)
            db.AddInParameter(dbcmd, "@FlowStepOpinion", DbType.String, WF_FlowFullLogRow.FlowStepOpinion.Value)
            db.AddInParameter(dbcmd, "@FlowLogStatus", DbType.String, WF_FlowFullLogRow.FlowLogStatus.Value)
            db.AddInParameter(dbcmd, "@FromBr", DbType.String, WF_FlowFullLogRow.FromBr.Value)
            db.AddInParameter(dbcmd, "@FromBrName", DbType.String, WF_FlowFullLogRow.FromBrName.Value)
            db.AddInParameter(dbcmd, "@FromUser", DbType.String, WF_FlowFullLogRow.FromUser.Value)
            db.AddInParameter(dbcmd, "@FromUserName", DbType.String, WF_FlowFullLogRow.FromUserName.Value)
            db.AddInParameter(dbcmd, "@AssignTo", DbType.String, WF_FlowFullLogRow.AssignTo.Value)
            db.AddInParameter(dbcmd, "@AssignToName", DbType.String, WF_FlowFullLogRow.AssignToName.Value)
            db.AddInParameter(dbcmd, "@ToBr", DbType.String, WF_FlowFullLogRow.ToBr.Value)
            db.AddInParameter(dbcmd, "@ToBrName", DbType.String, WF_FlowFullLogRow.ToBrName.Value)
            db.AddInParameter(dbcmd, "@ToUser", DbType.String, WF_FlowFullLogRow.ToUser.Value)
            db.AddInParameter(dbcmd, "@ToUserName", DbType.String, WF_FlowFullLogRow.ToUserName.Value)
            db.AddInParameter(dbcmd, "@IsProxy", DbType.String, WF_FlowFullLogRow.IsProxy.Value)
            db.AddInParameter(dbcmd, "@LogRemark", DbType.String, WF_FlowFullLogRow.LogRemark.Value)
            db.AddInParameter(dbcmd, "@CrDate", DbType.Date, IIf(IsDateTimeNull(WF_FlowFullLogRow.CrDate.Value), DBNull.Value, WF_FlowFullLogRow.CrDate.Value))
            db.AddInParameter(dbcmd, "@UpdDate", DbType.Date, IIf(IsDateTimeNull(WF_FlowFullLogRow.UpdDate.Value), DBNull.Value, WF_FlowFullLogRow.UpdDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal WF_FlowFullLogRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into WF_FlowFullLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowID, FlowCaseID, FlowLogBatNo, FlowLogID, FlowStepID, FlowStepDesc, FlowStepAction,")
            strSQL.AppendLine("    FlowStepOpinion, FlowLogStatus, FromBr, FromBrName, FromUser, FromUserName, AssignTo,")
            strSQL.AppendLine("    AssignToName, ToBr, ToBrName, ToUser, ToUserName, IsProxy, LogRemark, CrDate, UpdDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowID, @FlowCaseID, @FlowLogBatNo, @FlowLogID, @FlowStepID, @FlowStepDesc, @FlowStepAction,")
            strSQL.AppendLine("    @FlowStepOpinion, @FlowLogStatus, @FromBr, @FromBrName, @FromUser, @FromUserName, @AssignTo,")
            strSQL.AppendLine("    @AssignToName, @ToBr, @ToBrName, @ToUser, @ToUserName, @IsProxy, @LogRemark, @CrDate, @UpdDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowFullLogRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowFullLogRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@FlowLogBatNo", DbType.Int32, WF_FlowFullLogRow.FlowLogBatNo.Value)
            db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, WF_FlowFullLogRow.FlowLogID.Value)
            db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, WF_FlowFullLogRow.FlowStepID.Value)
            db.AddInParameter(dbcmd, "@FlowStepDesc", DbType.String, WF_FlowFullLogRow.FlowStepDesc.Value)
            db.AddInParameter(dbcmd, "@FlowStepAction", DbType.String, WF_FlowFullLogRow.FlowStepAction.Value)
            db.AddInParameter(dbcmd, "@FlowStepOpinion", DbType.String, WF_FlowFullLogRow.FlowStepOpinion.Value)
            db.AddInParameter(dbcmd, "@FlowLogStatus", DbType.String, WF_FlowFullLogRow.FlowLogStatus.Value)
            db.AddInParameter(dbcmd, "@FromBr", DbType.String, WF_FlowFullLogRow.FromBr.Value)
            db.AddInParameter(dbcmd, "@FromBrName", DbType.String, WF_FlowFullLogRow.FromBrName.Value)
            db.AddInParameter(dbcmd, "@FromUser", DbType.String, WF_FlowFullLogRow.FromUser.Value)
            db.AddInParameter(dbcmd, "@FromUserName", DbType.String, WF_FlowFullLogRow.FromUserName.Value)
            db.AddInParameter(dbcmd, "@AssignTo", DbType.String, WF_FlowFullLogRow.AssignTo.Value)
            db.AddInParameter(dbcmd, "@AssignToName", DbType.String, WF_FlowFullLogRow.AssignToName.Value)
            db.AddInParameter(dbcmd, "@ToBr", DbType.String, WF_FlowFullLogRow.ToBr.Value)
            db.AddInParameter(dbcmd, "@ToBrName", DbType.String, WF_FlowFullLogRow.ToBrName.Value)
            db.AddInParameter(dbcmd, "@ToUser", DbType.String, WF_FlowFullLogRow.ToUser.Value)
            db.AddInParameter(dbcmd, "@ToUserName", DbType.String, WF_FlowFullLogRow.ToUserName.Value)
            db.AddInParameter(dbcmd, "@IsProxy", DbType.String, WF_FlowFullLogRow.IsProxy.Value)
            db.AddInParameter(dbcmd, "@LogRemark", DbType.String, WF_FlowFullLogRow.LogRemark.Value)
            db.AddInParameter(dbcmd, "@CrDate", DbType.Date, IIf(IsDateTimeNull(WF_FlowFullLogRow.CrDate.Value), DBNull.Value, WF_FlowFullLogRow.CrDate.Value))
            db.AddInParameter(dbcmd, "@UpdDate", DbType.Date, IIf(IsDateTimeNull(WF_FlowFullLogRow.UpdDate.Value), DBNull.Value, WF_FlowFullLogRow.UpdDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal WF_FlowFullLogRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into WF_FlowFullLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowID, FlowCaseID, FlowLogBatNo, FlowLogID, FlowStepID, FlowStepDesc, FlowStepAction,")
            strSQL.AppendLine("    FlowStepOpinion, FlowLogStatus, FromBr, FromBrName, FromUser, FromUserName, AssignTo,")
            strSQL.AppendLine("    AssignToName, ToBr, ToBrName, ToUser, ToUserName, IsProxy, LogRemark, CrDate, UpdDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowID, @FlowCaseID, @FlowLogBatNo, @FlowLogID, @FlowStepID, @FlowStepDesc, @FlowStepAction,")
            strSQL.AppendLine("    @FlowStepOpinion, @FlowLogStatus, @FromBr, @FromBrName, @FromUser, @FromUserName, @AssignTo,")
            strSQL.AppendLine("    @AssignToName, @ToBr, @ToBrName, @ToUser, @ToUserName, @IsProxy, @LogRemark, @CrDate, @UpdDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In WF_FlowFullLogRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                        db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, r.FlowCaseID.Value)
                        db.AddInParameter(dbcmd, "@FlowLogBatNo", DbType.Int32, r.FlowLogBatNo.Value)
                        db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, r.FlowLogID.Value)
                        db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, r.FlowStepID.Value)
                        db.AddInParameter(dbcmd, "@FlowStepDesc", DbType.String, r.FlowStepDesc.Value)
                        db.AddInParameter(dbcmd, "@FlowStepAction", DbType.String, r.FlowStepAction.Value)
                        db.AddInParameter(dbcmd, "@FlowStepOpinion", DbType.String, r.FlowStepOpinion.Value)
                        db.AddInParameter(dbcmd, "@FlowLogStatus", DbType.String, r.FlowLogStatus.Value)
                        db.AddInParameter(dbcmd, "@FromBr", DbType.String, r.FromBr.Value)
                        db.AddInParameter(dbcmd, "@FromBrName", DbType.String, r.FromBrName.Value)
                        db.AddInParameter(dbcmd, "@FromUser", DbType.String, r.FromUser.Value)
                        db.AddInParameter(dbcmd, "@FromUserName", DbType.String, r.FromUserName.Value)
                        db.AddInParameter(dbcmd, "@AssignTo", DbType.String, r.AssignTo.Value)
                        db.AddInParameter(dbcmd, "@AssignToName", DbType.String, r.AssignToName.Value)
                        db.AddInParameter(dbcmd, "@ToBr", DbType.String, r.ToBr.Value)
                        db.AddInParameter(dbcmd, "@ToBrName", DbType.String, r.ToBrName.Value)
                        db.AddInParameter(dbcmd, "@ToUser", DbType.String, r.ToUser.Value)
                        db.AddInParameter(dbcmd, "@ToUserName", DbType.String, r.ToUserName.Value)
                        db.AddInParameter(dbcmd, "@IsProxy", DbType.String, r.IsProxy.Value)
                        db.AddInParameter(dbcmd, "@LogRemark", DbType.String, r.LogRemark.Value)
                        db.AddInParameter(dbcmd, "@CrDate", DbType.Date, IIf(IsDateTimeNull(r.CrDate.Value), DBNull.Value, r.CrDate.Value))
                        db.AddInParameter(dbcmd, "@UpdDate", DbType.Date, IIf(IsDateTimeNull(r.UpdDate.Value), DBNull.Value, r.UpdDate.Value))

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

        Public Function Insert(ByVal WF_FlowFullLogRow As Row(), ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into WF_FlowFullLog")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowID, FlowCaseID, FlowLogBatNo, FlowLogID, FlowStepID, FlowStepDesc, FlowStepAction,")
            strSQL.AppendLine("    FlowStepOpinion, FlowLogStatus, FromBr, FromBrName, FromUser, FromUserName, AssignTo,")
            strSQL.AppendLine("    AssignToName, ToBr, ToBrName, ToUser, ToUserName, IsProxy, LogRemark, CrDate, UpdDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowID, @FlowCaseID, @FlowLogBatNo, @FlowLogID, @FlowStepID, @FlowStepDesc, @FlowStepAction,")
            strSQL.AppendLine("    @FlowStepOpinion, @FlowLogStatus, @FromBr, @FromBrName, @FromUser, @FromUserName, @AssignTo,")
            strSQL.AppendLine("    @AssignToName, @ToBr, @ToBrName, @ToUser, @ToUserName, @IsProxy, @LogRemark, @CrDate, @UpdDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In WF_FlowFullLogRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, r.FlowCaseID.Value)
                db.AddInParameter(dbcmd, "@FlowLogBatNo", DbType.Int32, r.FlowLogBatNo.Value)
                db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, r.FlowLogID.Value)
                db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, r.FlowStepID.Value)
                db.AddInParameter(dbcmd, "@FlowStepDesc", DbType.String, r.FlowStepDesc.Value)
                db.AddInParameter(dbcmd, "@FlowStepAction", DbType.String, r.FlowStepAction.Value)
                db.AddInParameter(dbcmd, "@FlowStepOpinion", DbType.String, r.FlowStepOpinion.Value)
                db.AddInParameter(dbcmd, "@FlowLogStatus", DbType.String, r.FlowLogStatus.Value)
                db.AddInParameter(dbcmd, "@FromBr", DbType.String, r.FromBr.Value)
                db.AddInParameter(dbcmd, "@FromBrName", DbType.String, r.FromBrName.Value)
                db.AddInParameter(dbcmd, "@FromUser", DbType.String, r.FromUser.Value)
                db.AddInParameter(dbcmd, "@FromUserName", DbType.String, r.FromUserName.Value)
                db.AddInParameter(dbcmd, "@AssignTo", DbType.String, r.AssignTo.Value)
                db.AddInParameter(dbcmd, "@AssignToName", DbType.String, r.AssignToName.Value)
                db.AddInParameter(dbcmd, "@ToBr", DbType.String, r.ToBr.Value)
                db.AddInParameter(dbcmd, "@ToBrName", DbType.String, r.ToBrName.Value)
                db.AddInParameter(dbcmd, "@ToUser", DbType.String, r.ToUser.Value)
                db.AddInParameter(dbcmd, "@ToUserName", DbType.String, r.ToUserName.Value)
                db.AddInParameter(dbcmd, "@IsProxy", DbType.String, r.IsProxy.Value)
                db.AddInParameter(dbcmd, "@LogRemark", DbType.String, r.LogRemark.Value)
                db.AddInParameter(dbcmd, "@CrDate", DbType.Date, IIf(IsDateTimeNull(r.CrDate.Value), DBNull.Value, r.CrDate.Value))
                db.AddInParameter(dbcmd, "@UpdDate", DbType.Date, IIf(IsDateTimeNull(r.UpdDate.Value), DBNull.Value, r.UpdDate.Value))

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

