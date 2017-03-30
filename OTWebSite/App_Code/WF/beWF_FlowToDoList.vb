'****************************************************************
' Table:WF_FlowToDoList
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

Namespace beWF_FlowToDoList
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "AssignTo", "FlowID", "FlowCaseID", "FlowLogBatNo", "FlowLogID", "FlowStepID", "FlowStepDesc", "IsBoss", "FlowKeyValue", "FlowShowValue" _
                                    , "FlowDispatchFlag", "FlowCaseStatus", "CrDate", "CrBr", "CrBrName", "CrUser", "CrUserName", "FromDate", "FromBr", "FromBrName", "FromUser" _
                                    , "FromUserName", "PaperID" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(Integer), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(Date), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String) }
        Private m_PrimaryFields As String() = { "AssignTo", "FlowID", "FlowCaseID", "FlowLogID" }

        Public ReadOnly Property Rows() As beWF_FlowToDoList.Rows 
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
        Public Sub Transfer2Row(WF_FlowToDoListTable As DataTable)
            For Each dr As DataRow In WF_FlowToDoListTable.Rows
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

                dr(m_Rows(i).AssignTo.FieldName) = m_Rows(i).AssignTo.Value
                dr(m_Rows(i).FlowID.FieldName) = m_Rows(i).FlowID.Value
                dr(m_Rows(i).FlowCaseID.FieldName) = m_Rows(i).FlowCaseID.Value
                dr(m_Rows(i).FlowLogBatNo.FieldName) = m_Rows(i).FlowLogBatNo.Value
                dr(m_Rows(i).FlowLogID.FieldName) = m_Rows(i).FlowLogID.Value
                dr(m_Rows(i).FlowStepID.FieldName) = m_Rows(i).FlowStepID.Value
                dr(m_Rows(i).FlowStepDesc.FieldName) = m_Rows(i).FlowStepDesc.Value
                dr(m_Rows(i).IsBoss.FieldName) = m_Rows(i).IsBoss.Value
                dr(m_Rows(i).FlowKeyValue.FieldName) = m_Rows(i).FlowKeyValue.Value
                dr(m_Rows(i).FlowShowValue.FieldName) = m_Rows(i).FlowShowValue.Value
                dr(m_Rows(i).FlowDispatchFlag.FieldName) = m_Rows(i).FlowDispatchFlag.Value
                dr(m_Rows(i).FlowCaseStatus.FieldName) = m_Rows(i).FlowCaseStatus.Value
                dr(m_Rows(i).CrDate.FieldName) = m_Rows(i).CrDate.Value
                dr(m_Rows(i).CrBr.FieldName) = m_Rows(i).CrBr.Value
                dr(m_Rows(i).CrBrName.FieldName) = m_Rows(i).CrBrName.Value
                dr(m_Rows(i).CrUser.FieldName) = m_Rows(i).CrUser.Value
                dr(m_Rows(i).CrUserName.FieldName) = m_Rows(i).CrUserName.Value
                dr(m_Rows(i).FromDate.FieldName) = m_Rows(i).FromDate.Value
                dr(m_Rows(i).FromBr.FieldName) = m_Rows(i).FromBr.Value
                dr(m_Rows(i).FromBrName.FieldName) = m_Rows(i).FromBrName.Value
                dr(m_Rows(i).FromUser.FieldName) = m_Rows(i).FromUser.Value
                dr(m_Rows(i).FromUserName.FieldName) = m_Rows(i).FromUserName.Value
                dr(m_Rows(i).PaperID.FieldName) = m_Rows(i).PaperID.Value

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

        Public Sub Add(WF_FlowToDoListRow As Row)
            m_Rows.Add(WF_FlowToDoListRow)
        End Sub

        Public Sub Remove(WF_FlowToDoListRow As Row)
            If m_Rows.IndexOf(WF_FlowToDoListRow) >= 0 Then
                m_Rows.Remove(WF_FlowToDoListRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_AssignTo As Field(Of String) = new Field(Of String)("AssignTo", true)
        Private FI_FlowID As Field(Of String) = new Field(Of String)("FlowID", true)
        Private FI_FlowCaseID As Field(Of String) = new Field(Of String)("FlowCaseID", true)
        Private FI_FlowLogBatNo As Field(Of Integer) = new Field(Of Integer)("FlowLogBatNo", true)
        Private FI_FlowLogID As Field(Of String) = new Field(Of String)("FlowLogID", true)
        Private FI_FlowStepID As Field(Of String) = new Field(Of String)("FlowStepID", true)
        Private FI_FlowStepDesc As Field(Of String) = new Field(Of String)("FlowStepDesc", true)
        Private FI_IsBoss As Field(Of String) = new Field(Of String)("IsBoss", true)
        Private FI_FlowKeyValue As Field(Of String) = new Field(Of String)("FlowKeyValue", true)
        Private FI_FlowShowValue As Field(Of String) = new Field(Of String)("FlowShowValue", true)
        Private FI_FlowDispatchFlag As Field(Of String) = new Field(Of String)("FlowDispatchFlag", true)
        Private FI_FlowCaseStatus As Field(Of String) = new Field(Of String)("FlowCaseStatus", true)
        Private FI_CrDate As Field(Of Date) = new Field(Of Date)("CrDate", true)
        Private FI_CrBr As Field(Of String) = new Field(Of String)("CrBr", true)
        Private FI_CrBrName As Field(Of String) = new Field(Of String)("CrBrName", true)
        Private FI_CrUser As Field(Of String) = new Field(Of String)("CrUser", true)
        Private FI_CrUserName As Field(Of String) = new Field(Of String)("CrUserName", true)
        Private FI_FromDate As Field(Of Date) = new Field(Of Date)("FromDate", true)
        Private FI_FromBr As Field(Of String) = new Field(Of String)("FromBr", true)
        Private FI_FromBrName As Field(Of String) = new Field(Of String)("FromBrName", true)
        Private FI_FromUser As Field(Of String) = new Field(Of String)("FromUser", true)
        Private FI_FromUserName As Field(Of String) = new Field(Of String)("FromUserName", true)
        Private FI_PaperID As Field(Of String) = new Field(Of String)("PaperID", true)
        Private m_FieldNames As String() = { "AssignTo", "FlowID", "FlowCaseID", "FlowLogBatNo", "FlowLogID", "FlowStepID", "FlowStepDesc", "IsBoss", "FlowKeyValue", "FlowShowValue" _
                                    , "FlowDispatchFlag", "FlowCaseStatus", "CrDate", "CrBr", "CrBrName", "CrUser", "CrUserName", "FromDate", "FromBr", "FromBrName", "FromUser" _
                                    , "FromUserName", "PaperID" }
        Private m_PrimaryFields As String() = { "AssignTo", "FlowID", "FlowCaseID", "FlowLogID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "AssignTo"
                    Return FI_AssignTo.Value
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
                Case "IsBoss"
                    Return FI_IsBoss.Value
                Case "FlowKeyValue"
                    Return FI_FlowKeyValue.Value
                Case "FlowShowValue"
                    Return FI_FlowShowValue.Value
                Case "FlowDispatchFlag"
                    Return FI_FlowDispatchFlag.Value
                Case "FlowCaseStatus"
                    Return FI_FlowCaseStatus.Value
                Case "CrDate"
                    Return FI_CrDate.Value
                Case "CrBr"
                    Return FI_CrBr.Value
                Case "CrBrName"
                    Return FI_CrBrName.Value
                Case "CrUser"
                    Return FI_CrUser.Value
                Case "CrUserName"
                    Return FI_CrUserName.Value
                Case "FromDate"
                    Return FI_FromDate.Value
                Case "FromBr"
                    Return FI_FromBr.Value
                Case "FromBrName"
                    Return FI_FromBrName.Value
                Case "FromUser"
                    Return FI_FromUser.Value
                Case "FromUserName"
                    Return FI_FromUserName.Value
                Case "PaperID"
                    Return FI_PaperID.Value
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Sub SetFieldValue(ByVal fieldName As String, ByVal value As Object)
            Select Case fieldName
                Case "AssignTo"
                    FI_AssignTo.SetValue(value)
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
                Case "IsBoss"
                    FI_IsBoss.SetValue(value)
                Case "FlowKeyValue"
                    FI_FlowKeyValue.SetValue(value)
                Case "FlowShowValue"
                    FI_FlowShowValue.SetValue(value)
                Case "FlowDispatchFlag"
                    FI_FlowDispatchFlag.SetValue(value)
                Case "FlowCaseStatus"
                    FI_FlowCaseStatus.SetValue(value)
                Case "CrDate"
                    FI_CrDate.SetValue(value)
                Case "CrBr"
                    FI_CrBr.SetValue(value)
                Case "CrBrName"
                    FI_CrBrName.SetValue(value)
                Case "CrUser"
                    FI_CrUser.SetValue(value)
                Case "CrUserName"
                    FI_CrUserName.SetValue(value)
                Case "FromDate"
                    FI_FromDate.SetValue(value)
                Case "FromBr"
                    FI_FromBr.SetValue(value)
                Case "FromBrName"
                    FI_FromBrName.SetValue(value)
                Case "FromUser"
                    FI_FromUser.SetValue(value)
                Case "FromUserName"
                    FI_FromUserName.SetValue(value)
                Case "PaperID"
                    FI_PaperID.SetValue(value)
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
                Case "AssignTo"
                    return FI_AssignTo.Updated
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
                Case "IsBoss"
                    return FI_IsBoss.Updated
                Case "FlowKeyValue"
                    return FI_FlowKeyValue.Updated
                Case "FlowShowValue"
                    return FI_FlowShowValue.Updated
                Case "FlowDispatchFlag"
                    return FI_FlowDispatchFlag.Updated
                Case "FlowCaseStatus"
                    return FI_FlowCaseStatus.Updated
                Case "CrDate"
                    return FI_CrDate.Updated
                Case "CrBr"
                    return FI_CrBr.Updated
                Case "CrBrName"
                    return FI_CrBrName.Updated
                Case "CrUser"
                    return FI_CrUser.Updated
                Case "CrUserName"
                    return FI_CrUserName.Updated
                Case "FromDate"
                    return FI_FromDate.Updated
                Case "FromBr"
                    return FI_FromBr.Updated
                Case "FromBrName"
                    return FI_FromBrName.Updated
                Case "FromUser"
                    return FI_FromUser.Updated
                Case "FromUserName"
                    return FI_FromUserName.Updated
                Case "PaperID"
                    return FI_PaperID.Updated
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public Function CreateUpdateSQL(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "AssignTo"
                    return FI_AssignTo.CreateUpdateSQL
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
                Case "IsBoss"
                    return FI_IsBoss.CreateUpdateSQL
                Case "FlowKeyValue"
                    return FI_FlowKeyValue.CreateUpdateSQL
                Case "FlowShowValue"
                    return FI_FlowShowValue.CreateUpdateSQL
                Case "FlowDispatchFlag"
                    return FI_FlowDispatchFlag.CreateUpdateSQL
                Case "FlowCaseStatus"
                    return FI_FlowCaseStatus.CreateUpdateSQL
                Case "CrDate"
                    return FI_CrDate.CreateUpdateSQL
                Case "CrBr"
                    return FI_CrBr.CreateUpdateSQL
                Case "CrBrName"
                    return FI_CrBrName.CreateUpdateSQL
                Case "CrUser"
                    return FI_CrUser.CreateUpdateSQL
                Case "CrUserName"
                    return FI_CrUserName.CreateUpdateSQL
                Case "FromDate"
                    return FI_FromDate.CreateUpdateSQL
                Case "FromBr"
                    return FI_FromBr.CreateUpdateSQL
                Case "FromBrName"
                    return FI_FromBrName.CreateUpdateSQL
                Case "FromUser"
                    return FI_FromUser.CreateUpdateSQL
                Case "FromUserName"
                    return FI_FromUserName.CreateUpdateSQL
                Case "PaperID"
                    return FI_PaperID.CreateUpdateSQL
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
            FI_AssignTo.SetInitValue("")
            FI_FlowID.SetInitValue("")
            FI_FlowCaseID.SetInitValue("")
            FI_FlowLogID.SetInitValue("")
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_AssignTo.SetInitValue(dr("AssignTo"))
            FI_FlowID.SetInitValue(dr("FlowID"))
            FI_FlowCaseID.SetInitValue(dr("FlowCaseID"))
            FI_FlowLogBatNo.SetInitValue(dr("FlowLogBatNo"))
            FI_FlowLogID.SetInitValue(dr("FlowLogID"))
            FI_FlowStepID.SetInitValue(dr("FlowStepID"))
            FI_FlowStepDesc.SetInitValue(dr("FlowStepDesc"))
            FI_IsBoss.SetInitValue(dr("IsBoss"))
            FI_FlowKeyValue.SetInitValue(dr("FlowKeyValue"))
            FI_FlowShowValue.SetInitValue(dr("FlowShowValue"))
            FI_FlowDispatchFlag.SetInitValue(dr("FlowDispatchFlag"))
            FI_FlowCaseStatus.SetInitValue(dr("FlowCaseStatus"))
            FI_CrDate.SetInitValue(dr("CrDate"))
            FI_CrBr.SetInitValue(dr("CrBr"))
            FI_CrBrName.SetInitValue(dr("CrBrName"))
            FI_CrUser.SetInitValue(dr("CrUser"))
            FI_CrUserName.SetInitValue(dr("CrUserName"))
            FI_FromDate.SetInitValue(dr("FromDate"))
            FI_FromBr.SetInitValue(dr("FromBr"))
            FI_FromBrName.SetInitValue(dr("FromBrName"))
            FI_FromUser.SetInitValue(dr("FromUser"))
            FI_FromUserName.SetInitValue(dr("FromUserName"))
            FI_PaperID.SetInitValue(dr("PaperID"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_AssignTo.Updated = False
            FI_FlowID.Updated = False
            FI_FlowCaseID.Updated = False
            FI_FlowLogBatNo.Updated = False
            FI_FlowLogID.Updated = False
            FI_FlowStepID.Updated = False
            FI_FlowStepDesc.Updated = False
            FI_IsBoss.Updated = False
            FI_FlowKeyValue.Updated = False
            FI_FlowShowValue.Updated = False
            FI_FlowDispatchFlag.Updated = False
            FI_FlowCaseStatus.Updated = False
            FI_CrDate.Updated = False
            FI_CrBr.Updated = False
            FI_CrBrName.Updated = False
            FI_CrUser.Updated = False
            FI_CrUserName.Updated = False
            FI_FromDate.Updated = False
            FI_FromBr.Updated = False
            FI_FromBrName.Updated = False
            FI_FromUser.Updated = False
            FI_FromUserName.Updated = False
            FI_PaperID.Updated = False
        End Sub

        Public ReadOnly Property AssignTo As Field(Of String) 
            Get
                Return FI_AssignTo
            End Get
        End Property

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

        Public ReadOnly Property IsBoss As Field(Of String) 
            Get
                Return FI_IsBoss
            End Get
        End Property

        Public ReadOnly Property FlowKeyValue As Field(Of String) 
            Get
                Return FI_FlowKeyValue
            End Get
        End Property

        Public ReadOnly Property FlowShowValue As Field(Of String) 
            Get
                Return FI_FlowShowValue
            End Get
        End Property

        Public ReadOnly Property FlowDispatchFlag As Field(Of String) 
            Get
                Return FI_FlowDispatchFlag
            End Get
        End Property

        Public ReadOnly Property FlowCaseStatus As Field(Of String) 
            Get
                Return FI_FlowCaseStatus
            End Get
        End Property

        Public ReadOnly Property CrDate As Field(Of Date) 
            Get
                Return FI_CrDate
            End Get
        End Property

        Public ReadOnly Property CrBr As Field(Of String) 
            Get
                Return FI_CrBr
            End Get
        End Property

        Public ReadOnly Property CrBrName As Field(Of String) 
            Get
                Return FI_CrBrName
            End Get
        End Property

        Public ReadOnly Property CrUser As Field(Of String) 
            Get
                Return FI_CrUser
            End Get
        End Property

        Public ReadOnly Property CrUserName As Field(Of String) 
            Get
                Return FI_CrUserName
            End Get
        End Property

        Public ReadOnly Property FromDate As Field(Of Date) 
            Get
                Return FI_FromDate
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

        Public ReadOnly Property PaperID As Field(Of String) 
            Get
                Return FI_PaperID
            End Get
        End Property

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal WF_FlowToDoListRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From WF_FlowToDoList")
            strSQL.AppendLine("Where AssignTo = @AssignTo")
            strSQL.AppendLine("And FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And FlowLogID = @FlowLogID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@AssignTo", DbType.String, WF_FlowToDoListRow.AssignTo.Value)
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowToDoListRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowToDoListRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, WF_FlowToDoListRow.FlowLogID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal WF_FlowToDoListRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From WF_FlowToDoList")
            strSQL.AppendLine("Where AssignTo = @AssignTo")
            strSQL.AppendLine("And FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And FlowLogID = @FlowLogID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@AssignTo", DbType.String, WF_FlowToDoListRow.AssignTo.Value)
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowToDoListRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowToDoListRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, WF_FlowToDoListRow.FlowLogID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal WF_FlowToDoListRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From WF_FlowToDoList")
            strSQL.AppendLine("Where AssignTo = @AssignTo")
            strSQL.AppendLine("And FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And FlowLogID = @FlowLogID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In WF_FlowToDoListRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@AssignTo", DbType.String, r.AssignTo.Value)
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

        Public Function DeleteRowByPrimaryKey(ByVal WF_FlowToDoListRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From WF_FlowToDoList")
            strSQL.AppendLine("Where AssignTo = @AssignTo")
            strSQL.AppendLine("And FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And FlowLogID = @FlowLogID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In WF_FlowToDoListRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@AssignTo", DbType.String, r.AssignTo.Value)
                db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, r.FlowCaseID.Value)
                db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, r.FlowLogID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal WF_FlowToDoListRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WF_FlowToDoList")
            strSQL.AppendLine("Where AssignTo = @AssignTo")
            strSQL.AppendLine("And FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And FlowLogID = @FlowLogID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@AssignTo", DbType.String, WF_FlowToDoListRow.AssignTo.Value)
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowToDoListRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowToDoListRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, WF_FlowToDoListRow.FlowLogID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(WF_FlowToDoListRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WF_FlowToDoList")
            strSQL.AppendLine("Where AssignTo = @AssignTo")
            strSQL.AppendLine("And FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And FlowLogID = @FlowLogID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@AssignTo", DbType.String, WF_FlowToDoListRow.AssignTo.Value)
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowToDoListRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowToDoListRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, WF_FlowToDoListRow.FlowLogID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal WF_FlowToDoListRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update WF_FlowToDoList Set")
            For i As Integer = 0 To WF_FlowToDoListRow.FieldNames.Length - 1
                If Not WF_FlowToDoListRow.IsIdentityField(WF_FlowToDoListRow.FieldNames(i)) AndAlso WF_FlowToDoListRow.IsUpdated(WF_FlowToDoListRow.FieldNames(i)) AndAlso WF_FlowToDoListRow.CreateUpdateSQL(WF_FlowToDoListRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, WF_FlowToDoListRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where AssignTo = @PKAssignTo")
            strSQL.AppendLine("And FlowID = @PKFlowID")
            strSQL.AppendLine("And FlowCaseID = @PKFlowCaseID")
            strSQL.AppendLine("And FlowLogID = @PKFlowLogID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If WF_FlowToDoListRow.AssignTo.Updated Then db.AddInParameter(dbcmd, "@AssignTo", DbType.String, WF_FlowToDoListRow.AssignTo.Value)
            If WF_FlowToDoListRow.FlowID.Updated Then db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowToDoListRow.FlowID.Value)
            If WF_FlowToDoListRow.FlowCaseID.Updated Then db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowToDoListRow.FlowCaseID.Value)
            If WF_FlowToDoListRow.FlowLogBatNo.Updated Then db.AddInParameter(dbcmd, "@FlowLogBatNo", DbType.Int32, WF_FlowToDoListRow.FlowLogBatNo.Value)
            If WF_FlowToDoListRow.FlowLogID.Updated Then db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, WF_FlowToDoListRow.FlowLogID.Value)
            If WF_FlowToDoListRow.FlowStepID.Updated Then db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, WF_FlowToDoListRow.FlowStepID.Value)
            If WF_FlowToDoListRow.FlowStepDesc.Updated Then db.AddInParameter(dbcmd, "@FlowStepDesc", DbType.String, WF_FlowToDoListRow.FlowStepDesc.Value)
            If WF_FlowToDoListRow.IsBoss.Updated Then db.AddInParameter(dbcmd, "@IsBoss", DbType.String, WF_FlowToDoListRow.IsBoss.Value)
            If WF_FlowToDoListRow.FlowKeyValue.Updated Then db.AddInParameter(dbcmd, "@FlowKeyValue", DbType.String, WF_FlowToDoListRow.FlowKeyValue.Value)
            If WF_FlowToDoListRow.FlowShowValue.Updated Then db.AddInParameter(dbcmd, "@FlowShowValue", DbType.String, WF_FlowToDoListRow.FlowShowValue.Value)
            If WF_FlowToDoListRow.FlowDispatchFlag.Updated Then db.AddInParameter(dbcmd, "@FlowDispatchFlag", DbType.String, WF_FlowToDoListRow.FlowDispatchFlag.Value)
            If WF_FlowToDoListRow.FlowCaseStatus.Updated Then db.AddInParameter(dbcmd, "@FlowCaseStatus", DbType.String, WF_FlowToDoListRow.FlowCaseStatus.Value)
            If WF_FlowToDoListRow.CrDate.Updated Then db.AddInParameter(dbcmd, "@CrDate", DbType.Date, IIf(IsDateTimeNull(WF_FlowToDoListRow.CrDate.Value), DBNull.Value, WF_FlowToDoListRow.CrDate.Value))
            If WF_FlowToDoListRow.CrBr.Updated Then db.AddInParameter(dbcmd, "@CrBr", DbType.String, WF_FlowToDoListRow.CrBr.Value)
            If WF_FlowToDoListRow.CrBrName.Updated Then db.AddInParameter(dbcmd, "@CrBrName", DbType.String, WF_FlowToDoListRow.CrBrName.Value)
            If WF_FlowToDoListRow.CrUser.Updated Then db.AddInParameter(dbcmd, "@CrUser", DbType.String, WF_FlowToDoListRow.CrUser.Value)
            If WF_FlowToDoListRow.CrUserName.Updated Then db.AddInParameter(dbcmd, "@CrUserName", DbType.String, WF_FlowToDoListRow.CrUserName.Value)
            If WF_FlowToDoListRow.FromDate.Updated Then db.AddInParameter(dbcmd, "@FromDate", DbType.Date, IIf(IsDateTimeNull(WF_FlowToDoListRow.FromDate.Value), DBNull.Value, WF_FlowToDoListRow.FromDate.Value))
            If WF_FlowToDoListRow.FromBr.Updated Then db.AddInParameter(dbcmd, "@FromBr", DbType.String, WF_FlowToDoListRow.FromBr.Value)
            If WF_FlowToDoListRow.FromBrName.Updated Then db.AddInParameter(dbcmd, "@FromBrName", DbType.String, WF_FlowToDoListRow.FromBrName.Value)
            If WF_FlowToDoListRow.FromUser.Updated Then db.AddInParameter(dbcmd, "@FromUser", DbType.String, WF_FlowToDoListRow.FromUser.Value)
            If WF_FlowToDoListRow.FromUserName.Updated Then db.AddInParameter(dbcmd, "@FromUserName", DbType.String, WF_FlowToDoListRow.FromUserName.Value)
            If WF_FlowToDoListRow.PaperID.Updated Then db.AddInParameter(dbcmd, "@PaperID", DbType.String, WF_FlowToDoListRow.PaperID.Value)
            db.AddInParameter(dbcmd, "@PKAssignTo", DbType.String, IIf(WF_FlowToDoListRow.LoadFromDataRow, WF_FlowToDoListRow.AssignTo.OldValue, WF_FlowToDoListRow.AssignTo.Value))
            db.AddInParameter(dbcmd, "@PKFlowID", DbType.String, IIf(WF_FlowToDoListRow.LoadFromDataRow, WF_FlowToDoListRow.FlowID.OldValue, WF_FlowToDoListRow.FlowID.Value))
            db.AddInParameter(dbcmd, "@PKFlowCaseID", DbType.String, IIf(WF_FlowToDoListRow.LoadFromDataRow, WF_FlowToDoListRow.FlowCaseID.OldValue, WF_FlowToDoListRow.FlowCaseID.Value))
            db.AddInParameter(dbcmd, "@PKFlowLogID", DbType.String, IIf(WF_FlowToDoListRow.LoadFromDataRow, WF_FlowToDoListRow.FlowLogID.OldValue, WF_FlowToDoListRow.FlowLogID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal WF_FlowToDoListRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update WF_FlowToDoList Set")
            For i As Integer = 0 To WF_FlowToDoListRow.FieldNames.Length - 1
                If Not WF_FlowToDoListRow.IsIdentityField(WF_FlowToDoListRow.FieldNames(i)) AndAlso WF_FlowToDoListRow.IsUpdated(WF_FlowToDoListRow.FieldNames(i)) AndAlso WF_FlowToDoListRow.CreateUpdateSQL(WF_FlowToDoListRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, WF_FlowToDoListRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where AssignTo = @PKAssignTo")
            strSQL.AppendLine("And FlowID = @PKFlowID")
            strSQL.AppendLine("And FlowCaseID = @PKFlowCaseID")
            strSQL.AppendLine("And FlowLogID = @PKFlowLogID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If WF_FlowToDoListRow.AssignTo.Updated Then db.AddInParameter(dbcmd, "@AssignTo", DbType.String, WF_FlowToDoListRow.AssignTo.Value)
            If WF_FlowToDoListRow.FlowID.Updated Then db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowToDoListRow.FlowID.Value)
            If WF_FlowToDoListRow.FlowCaseID.Updated Then db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowToDoListRow.FlowCaseID.Value)
            If WF_FlowToDoListRow.FlowLogBatNo.Updated Then db.AddInParameter(dbcmd, "@FlowLogBatNo", DbType.Int32, WF_FlowToDoListRow.FlowLogBatNo.Value)
            If WF_FlowToDoListRow.FlowLogID.Updated Then db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, WF_FlowToDoListRow.FlowLogID.Value)
            If WF_FlowToDoListRow.FlowStepID.Updated Then db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, WF_FlowToDoListRow.FlowStepID.Value)
            If WF_FlowToDoListRow.FlowStepDesc.Updated Then db.AddInParameter(dbcmd, "@FlowStepDesc", DbType.String, WF_FlowToDoListRow.FlowStepDesc.Value)
            If WF_FlowToDoListRow.IsBoss.Updated Then db.AddInParameter(dbcmd, "@IsBoss", DbType.String, WF_FlowToDoListRow.IsBoss.Value)
            If WF_FlowToDoListRow.FlowKeyValue.Updated Then db.AddInParameter(dbcmd, "@FlowKeyValue", DbType.String, WF_FlowToDoListRow.FlowKeyValue.Value)
            If WF_FlowToDoListRow.FlowShowValue.Updated Then db.AddInParameter(dbcmd, "@FlowShowValue", DbType.String, WF_FlowToDoListRow.FlowShowValue.Value)
            If WF_FlowToDoListRow.FlowDispatchFlag.Updated Then db.AddInParameter(dbcmd, "@FlowDispatchFlag", DbType.String, WF_FlowToDoListRow.FlowDispatchFlag.Value)
            If WF_FlowToDoListRow.FlowCaseStatus.Updated Then db.AddInParameter(dbcmd, "@FlowCaseStatus", DbType.String, WF_FlowToDoListRow.FlowCaseStatus.Value)
            If WF_FlowToDoListRow.CrDate.Updated Then db.AddInParameter(dbcmd, "@CrDate", DbType.Date, IIf(IsDateTimeNull(WF_FlowToDoListRow.CrDate.Value), DBNull.Value, WF_FlowToDoListRow.CrDate.Value))
            If WF_FlowToDoListRow.CrBr.Updated Then db.AddInParameter(dbcmd, "@CrBr", DbType.String, WF_FlowToDoListRow.CrBr.Value)
            If WF_FlowToDoListRow.CrBrName.Updated Then db.AddInParameter(dbcmd, "@CrBrName", DbType.String, WF_FlowToDoListRow.CrBrName.Value)
            If WF_FlowToDoListRow.CrUser.Updated Then db.AddInParameter(dbcmd, "@CrUser", DbType.String, WF_FlowToDoListRow.CrUser.Value)
            If WF_FlowToDoListRow.CrUserName.Updated Then db.AddInParameter(dbcmd, "@CrUserName", DbType.String, WF_FlowToDoListRow.CrUserName.Value)
            If WF_FlowToDoListRow.FromDate.Updated Then db.AddInParameter(dbcmd, "@FromDate", DbType.Date, IIf(IsDateTimeNull(WF_FlowToDoListRow.FromDate.Value), DBNull.Value, WF_FlowToDoListRow.FromDate.Value))
            If WF_FlowToDoListRow.FromBr.Updated Then db.AddInParameter(dbcmd, "@FromBr", DbType.String, WF_FlowToDoListRow.FromBr.Value)
            If WF_FlowToDoListRow.FromBrName.Updated Then db.AddInParameter(dbcmd, "@FromBrName", DbType.String, WF_FlowToDoListRow.FromBrName.Value)
            If WF_FlowToDoListRow.FromUser.Updated Then db.AddInParameter(dbcmd, "@FromUser", DbType.String, WF_FlowToDoListRow.FromUser.Value)
            If WF_FlowToDoListRow.FromUserName.Updated Then db.AddInParameter(dbcmd, "@FromUserName", DbType.String, WF_FlowToDoListRow.FromUserName.Value)
            If WF_FlowToDoListRow.PaperID.Updated Then db.AddInParameter(dbcmd, "@PaperID", DbType.String, WF_FlowToDoListRow.PaperID.Value)
            db.AddInParameter(dbcmd, "@PKAssignTo", DbType.String, IIf(WF_FlowToDoListRow.LoadFromDataRow, WF_FlowToDoListRow.AssignTo.OldValue, WF_FlowToDoListRow.AssignTo.Value))
            db.AddInParameter(dbcmd, "@PKFlowID", DbType.String, IIf(WF_FlowToDoListRow.LoadFromDataRow, WF_FlowToDoListRow.FlowID.OldValue, WF_FlowToDoListRow.FlowID.Value))
            db.AddInParameter(dbcmd, "@PKFlowCaseID", DbType.String, IIf(WF_FlowToDoListRow.LoadFromDataRow, WF_FlowToDoListRow.FlowCaseID.OldValue, WF_FlowToDoListRow.FlowCaseID.Value))
            db.AddInParameter(dbcmd, "@PKFlowLogID", DbType.String, IIf(WF_FlowToDoListRow.LoadFromDataRow, WF_FlowToDoListRow.FlowLogID.OldValue, WF_FlowToDoListRow.FlowLogID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal WF_FlowToDoListRow As Row()) As Integer
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
                    For Each r As Row In WF_FlowToDoListRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update WF_FlowToDoList Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where AssignTo = @PKAssignTo")
                        strSQL.AppendLine("And FlowID = @PKFlowID")
                        strSQL.AppendLine("And FlowCaseID = @PKFlowCaseID")
                        strSQL.AppendLine("And FlowLogID = @PKFlowLogID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.AssignTo.Updated Then db.AddInParameter(dbcmd, "@AssignTo", DbType.String, r.AssignTo.Value)
                        If r.FlowID.Updated Then db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                        If r.FlowCaseID.Updated Then db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, r.FlowCaseID.Value)
                        If r.FlowLogBatNo.Updated Then db.AddInParameter(dbcmd, "@FlowLogBatNo", DbType.Int32, r.FlowLogBatNo.Value)
                        If r.FlowLogID.Updated Then db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, r.FlowLogID.Value)
                        If r.FlowStepID.Updated Then db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, r.FlowStepID.Value)
                        If r.FlowStepDesc.Updated Then db.AddInParameter(dbcmd, "@FlowStepDesc", DbType.String, r.FlowStepDesc.Value)
                        If r.IsBoss.Updated Then db.AddInParameter(dbcmd, "@IsBoss", DbType.String, r.IsBoss.Value)
                        If r.FlowKeyValue.Updated Then db.AddInParameter(dbcmd, "@FlowKeyValue", DbType.String, r.FlowKeyValue.Value)
                        If r.FlowShowValue.Updated Then db.AddInParameter(dbcmd, "@FlowShowValue", DbType.String, r.FlowShowValue.Value)
                        If r.FlowDispatchFlag.Updated Then db.AddInParameter(dbcmd, "@FlowDispatchFlag", DbType.String, r.FlowDispatchFlag.Value)
                        If r.FlowCaseStatus.Updated Then db.AddInParameter(dbcmd, "@FlowCaseStatus", DbType.String, r.FlowCaseStatus.Value)
                        If r.CrDate.Updated Then db.AddInParameter(dbcmd, "@CrDate", DbType.Date, IIf(IsDateTimeNull(r.CrDate.Value), DBNull.Value, r.CrDate.Value))
                        If r.CrBr.Updated Then db.AddInParameter(dbcmd, "@CrBr", DbType.String, r.CrBr.Value)
                        If r.CrBrName.Updated Then db.AddInParameter(dbcmd, "@CrBrName", DbType.String, r.CrBrName.Value)
                        If r.CrUser.Updated Then db.AddInParameter(dbcmd, "@CrUser", DbType.String, r.CrUser.Value)
                        If r.CrUserName.Updated Then db.AddInParameter(dbcmd, "@CrUserName", DbType.String, r.CrUserName.Value)
                        If r.FromDate.Updated Then db.AddInParameter(dbcmd, "@FromDate", DbType.Date, IIf(IsDateTimeNull(r.FromDate.Value), DBNull.Value, r.FromDate.Value))
                        If r.FromBr.Updated Then db.AddInParameter(dbcmd, "@FromBr", DbType.String, r.FromBr.Value)
                        If r.FromBrName.Updated Then db.AddInParameter(dbcmd, "@FromBrName", DbType.String, r.FromBrName.Value)
                        If r.FromUser.Updated Then db.AddInParameter(dbcmd, "@FromUser", DbType.String, r.FromUser.Value)
                        If r.FromUserName.Updated Then db.AddInParameter(dbcmd, "@FromUserName", DbType.String, r.FromUserName.Value)
                        If r.PaperID.Updated Then db.AddInParameter(dbcmd, "@PaperID", DbType.String, r.PaperID.Value)
                        db.AddInParameter(dbcmd, "@PKAssignTo", DbType.String, IIf(r.LoadFromDataRow, r.AssignTo.OldValue, r.AssignTo.Value))
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

        Public Function Update(ByVal WF_FlowToDoListRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In WF_FlowToDoListRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update WF_FlowToDoList Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where AssignTo = @PKAssignTo")
                strSQL.AppendLine("And FlowID = @PKFlowID")
                strSQL.AppendLine("And FlowCaseID = @PKFlowCaseID")
                strSQL.AppendLine("And FlowLogID = @PKFlowLogID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.AssignTo.Updated Then db.AddInParameter(dbcmd, "@AssignTo", DbType.String, r.AssignTo.Value)
                If r.FlowID.Updated Then db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                If r.FlowCaseID.Updated Then db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, r.FlowCaseID.Value)
                If r.FlowLogBatNo.Updated Then db.AddInParameter(dbcmd, "@FlowLogBatNo", DbType.Int32, r.FlowLogBatNo.Value)
                If r.FlowLogID.Updated Then db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, r.FlowLogID.Value)
                If r.FlowStepID.Updated Then db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, r.FlowStepID.Value)
                If r.FlowStepDesc.Updated Then db.AddInParameter(dbcmd, "@FlowStepDesc", DbType.String, r.FlowStepDesc.Value)
                If r.IsBoss.Updated Then db.AddInParameter(dbcmd, "@IsBoss", DbType.String, r.IsBoss.Value)
                If r.FlowKeyValue.Updated Then db.AddInParameter(dbcmd, "@FlowKeyValue", DbType.String, r.FlowKeyValue.Value)
                If r.FlowShowValue.Updated Then db.AddInParameter(dbcmd, "@FlowShowValue", DbType.String, r.FlowShowValue.Value)
                If r.FlowDispatchFlag.Updated Then db.AddInParameter(dbcmd, "@FlowDispatchFlag", DbType.String, r.FlowDispatchFlag.Value)
                If r.FlowCaseStatus.Updated Then db.AddInParameter(dbcmd, "@FlowCaseStatus", DbType.String, r.FlowCaseStatus.Value)
                If r.CrDate.Updated Then db.AddInParameter(dbcmd, "@CrDate", DbType.Date, IIf(IsDateTimeNull(r.CrDate.Value), DBNull.Value, r.CrDate.Value))
                If r.CrBr.Updated Then db.AddInParameter(dbcmd, "@CrBr", DbType.String, r.CrBr.Value)
                If r.CrBrName.Updated Then db.AddInParameter(dbcmd, "@CrBrName", DbType.String, r.CrBrName.Value)
                If r.CrUser.Updated Then db.AddInParameter(dbcmd, "@CrUser", DbType.String, r.CrUser.Value)
                If r.CrUserName.Updated Then db.AddInParameter(dbcmd, "@CrUserName", DbType.String, r.CrUserName.Value)
                If r.FromDate.Updated Then db.AddInParameter(dbcmd, "@FromDate", DbType.Date, IIf(IsDateTimeNull(r.FromDate.Value), DBNull.Value, r.FromDate.Value))
                If r.FromBr.Updated Then db.AddInParameter(dbcmd, "@FromBr", DbType.String, r.FromBr.Value)
                If r.FromBrName.Updated Then db.AddInParameter(dbcmd, "@FromBrName", DbType.String, r.FromBrName.Value)
                If r.FromUser.Updated Then db.AddInParameter(dbcmd, "@FromUser", DbType.String, r.FromUser.Value)
                If r.FromUserName.Updated Then db.AddInParameter(dbcmd, "@FromUserName", DbType.String, r.FromUserName.Value)
                If r.PaperID.Updated Then db.AddInParameter(dbcmd, "@PaperID", DbType.String, r.PaperID.Value)
                db.AddInParameter(dbcmd, "@PKAssignTo", DbType.String, IIf(r.LoadFromDataRow, r.AssignTo.OldValue, r.AssignTo.Value))
                db.AddInParameter(dbcmd, "@PKFlowID", DbType.String, IIf(r.LoadFromDataRow, r.FlowID.OldValue, r.FlowID.Value))
                db.AddInParameter(dbcmd, "@PKFlowCaseID", DbType.String, IIf(r.LoadFromDataRow, r.FlowCaseID.OldValue, r.FlowCaseID.Value))
                db.AddInParameter(dbcmd, "@PKFlowLogID", DbType.String, IIf(r.LoadFromDataRow, r.FlowLogID.OldValue, r.FlowLogID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal WF_FlowToDoListRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From WF_FlowToDoList")
            strSQL.AppendLine("Where AssignTo = @AssignTo")
            strSQL.AppendLine("And FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And FlowLogID = @FlowLogID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@AssignTo", DbType.String, WF_FlowToDoListRow.AssignTo.Value)
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowToDoListRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowToDoListRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, WF_FlowToDoListRow.FlowLogID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal WF_FlowToDoListRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From WF_FlowToDoList")
            strSQL.AppendLine("Where AssignTo = @AssignTo")
            strSQL.AppendLine("And FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And FlowLogID = @FlowLogID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@AssignTo", DbType.String, WF_FlowToDoListRow.AssignTo.Value)
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowToDoListRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowToDoListRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, WF_FlowToDoListRow.FlowLogID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WF_FlowToDoList")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal WF_FlowToDoListRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into WF_FlowToDoList")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    AssignTo, FlowID, FlowCaseID, FlowLogBatNo, FlowLogID, FlowStepID, FlowStepDesc, IsBoss,")
            strSQL.AppendLine("    FlowKeyValue, FlowShowValue, FlowDispatchFlag, FlowCaseStatus, CrDate, CrBr, CrBrName,")
            strSQL.AppendLine("    CrUser, CrUserName, FromDate, FromBr, FromBrName, FromUser, FromUserName, PaperID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @AssignTo, @FlowID, @FlowCaseID, @FlowLogBatNo, @FlowLogID, @FlowStepID, @FlowStepDesc, @IsBoss,")
            strSQL.AppendLine("    @FlowKeyValue, @FlowShowValue, @FlowDispatchFlag, @FlowCaseStatus, @CrDate, @CrBr, @CrBrName,")
            strSQL.AppendLine("    @CrUser, @CrUserName, @FromDate, @FromBr, @FromBrName, @FromUser, @FromUserName, @PaperID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@AssignTo", DbType.String, WF_FlowToDoListRow.AssignTo.Value)
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowToDoListRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowToDoListRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@FlowLogBatNo", DbType.Int32, WF_FlowToDoListRow.FlowLogBatNo.Value)
            db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, WF_FlowToDoListRow.FlowLogID.Value)
            db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, WF_FlowToDoListRow.FlowStepID.Value)
            db.AddInParameter(dbcmd, "@FlowStepDesc", DbType.String, WF_FlowToDoListRow.FlowStepDesc.Value)
            db.AddInParameter(dbcmd, "@IsBoss", DbType.String, WF_FlowToDoListRow.IsBoss.Value)
            db.AddInParameter(dbcmd, "@FlowKeyValue", DbType.String, WF_FlowToDoListRow.FlowKeyValue.Value)
            db.AddInParameter(dbcmd, "@FlowShowValue", DbType.String, WF_FlowToDoListRow.FlowShowValue.Value)
            db.AddInParameter(dbcmd, "@FlowDispatchFlag", DbType.String, WF_FlowToDoListRow.FlowDispatchFlag.Value)
            db.AddInParameter(dbcmd, "@FlowCaseStatus", DbType.String, WF_FlowToDoListRow.FlowCaseStatus.Value)
            db.AddInParameter(dbcmd, "@CrDate", DbType.Date, IIf(IsDateTimeNull(WF_FlowToDoListRow.CrDate.Value), DBNull.Value, WF_FlowToDoListRow.CrDate.Value))
            db.AddInParameter(dbcmd, "@CrBr", DbType.String, WF_FlowToDoListRow.CrBr.Value)
            db.AddInParameter(dbcmd, "@CrBrName", DbType.String, WF_FlowToDoListRow.CrBrName.Value)
            db.AddInParameter(dbcmd, "@CrUser", DbType.String, WF_FlowToDoListRow.CrUser.Value)
            db.AddInParameter(dbcmd, "@CrUserName", DbType.String, WF_FlowToDoListRow.CrUserName.Value)
            db.AddInParameter(dbcmd, "@FromDate", DbType.Date, IIf(IsDateTimeNull(WF_FlowToDoListRow.FromDate.Value), DBNull.Value, WF_FlowToDoListRow.FromDate.Value))
            db.AddInParameter(dbcmd, "@FromBr", DbType.String, WF_FlowToDoListRow.FromBr.Value)
            db.AddInParameter(dbcmd, "@FromBrName", DbType.String, WF_FlowToDoListRow.FromBrName.Value)
            db.AddInParameter(dbcmd, "@FromUser", DbType.String, WF_FlowToDoListRow.FromUser.Value)
            db.AddInParameter(dbcmd, "@FromUserName", DbType.String, WF_FlowToDoListRow.FromUserName.Value)
            db.AddInParameter(dbcmd, "@PaperID", DbType.String, WF_FlowToDoListRow.PaperID.Value)

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal WF_FlowToDoListRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into WF_FlowToDoList")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    AssignTo, FlowID, FlowCaseID, FlowLogBatNo, FlowLogID, FlowStepID, FlowStepDesc, IsBoss,")
            strSQL.AppendLine("    FlowKeyValue, FlowShowValue, FlowDispatchFlag, FlowCaseStatus, CrDate, CrBr, CrBrName,")
            strSQL.AppendLine("    CrUser, CrUserName, FromDate, FromBr, FromBrName, FromUser, FromUserName, PaperID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @AssignTo, @FlowID, @FlowCaseID, @FlowLogBatNo, @FlowLogID, @FlowStepID, @FlowStepDesc, @IsBoss,")
            strSQL.AppendLine("    @FlowKeyValue, @FlowShowValue, @FlowDispatchFlag, @FlowCaseStatus, @CrDate, @CrBr, @CrBrName,")
            strSQL.AppendLine("    @CrUser, @CrUserName, @FromDate, @FromBr, @FromBrName, @FromUser, @FromUserName, @PaperID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@AssignTo", DbType.String, WF_FlowToDoListRow.AssignTo.Value)
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowToDoListRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowToDoListRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@FlowLogBatNo", DbType.Int32, WF_FlowToDoListRow.FlowLogBatNo.Value)
            db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, WF_FlowToDoListRow.FlowLogID.Value)
            db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, WF_FlowToDoListRow.FlowStepID.Value)
            db.AddInParameter(dbcmd, "@FlowStepDesc", DbType.String, WF_FlowToDoListRow.FlowStepDesc.Value)
            db.AddInParameter(dbcmd, "@IsBoss", DbType.String, WF_FlowToDoListRow.IsBoss.Value)
            db.AddInParameter(dbcmd, "@FlowKeyValue", DbType.String, WF_FlowToDoListRow.FlowKeyValue.Value)
            db.AddInParameter(dbcmd, "@FlowShowValue", DbType.String, WF_FlowToDoListRow.FlowShowValue.Value)
            db.AddInParameter(dbcmd, "@FlowDispatchFlag", DbType.String, WF_FlowToDoListRow.FlowDispatchFlag.Value)
            db.AddInParameter(dbcmd, "@FlowCaseStatus", DbType.String, WF_FlowToDoListRow.FlowCaseStatus.Value)
            db.AddInParameter(dbcmd, "@CrDate", DbType.Date, IIf(IsDateTimeNull(WF_FlowToDoListRow.CrDate.Value), DBNull.Value, WF_FlowToDoListRow.CrDate.Value))
            db.AddInParameter(dbcmd, "@CrBr", DbType.String, WF_FlowToDoListRow.CrBr.Value)
            db.AddInParameter(dbcmd, "@CrBrName", DbType.String, WF_FlowToDoListRow.CrBrName.Value)
            db.AddInParameter(dbcmd, "@CrUser", DbType.String, WF_FlowToDoListRow.CrUser.Value)
            db.AddInParameter(dbcmd, "@CrUserName", DbType.String, WF_FlowToDoListRow.CrUserName.Value)
            db.AddInParameter(dbcmd, "@FromDate", DbType.Date, IIf(IsDateTimeNull(WF_FlowToDoListRow.FromDate.Value), DBNull.Value, WF_FlowToDoListRow.FromDate.Value))
            db.AddInParameter(dbcmd, "@FromBr", DbType.String, WF_FlowToDoListRow.FromBr.Value)
            db.AddInParameter(dbcmd, "@FromBrName", DbType.String, WF_FlowToDoListRow.FromBrName.Value)
            db.AddInParameter(dbcmd, "@FromUser", DbType.String, WF_FlowToDoListRow.FromUser.Value)
            db.AddInParameter(dbcmd, "@FromUserName", DbType.String, WF_FlowToDoListRow.FromUserName.Value)
            db.AddInParameter(dbcmd, "@PaperID", DbType.String, WF_FlowToDoListRow.PaperID.Value)

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal WF_FlowToDoListRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into WF_FlowToDoList")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    AssignTo, FlowID, FlowCaseID, FlowLogBatNo, FlowLogID, FlowStepID, FlowStepDesc, IsBoss,")
            strSQL.AppendLine("    FlowKeyValue, FlowShowValue, FlowDispatchFlag, FlowCaseStatus, CrDate, CrBr, CrBrName,")
            strSQL.AppendLine("    CrUser, CrUserName, FromDate, FromBr, FromBrName, FromUser, FromUserName, PaperID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @AssignTo, @FlowID, @FlowCaseID, @FlowLogBatNo, @FlowLogID, @FlowStepID, @FlowStepDesc, @IsBoss,")
            strSQL.AppendLine("    @FlowKeyValue, @FlowShowValue, @FlowDispatchFlag, @FlowCaseStatus, @CrDate, @CrBr, @CrBrName,")
            strSQL.AppendLine("    @CrUser, @CrUserName, @FromDate, @FromBr, @FromBrName, @FromUser, @FromUserName, @PaperID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In WF_FlowToDoListRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@AssignTo", DbType.String, r.AssignTo.Value)
                        db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                        db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, r.FlowCaseID.Value)
                        db.AddInParameter(dbcmd, "@FlowLogBatNo", DbType.Int32, r.FlowLogBatNo.Value)
                        db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, r.FlowLogID.Value)
                        db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, r.FlowStepID.Value)
                        db.AddInParameter(dbcmd, "@FlowStepDesc", DbType.String, r.FlowStepDesc.Value)
                        db.AddInParameter(dbcmd, "@IsBoss", DbType.String, r.IsBoss.Value)
                        db.AddInParameter(dbcmd, "@FlowKeyValue", DbType.String, r.FlowKeyValue.Value)
                        db.AddInParameter(dbcmd, "@FlowShowValue", DbType.String, r.FlowShowValue.Value)
                        db.AddInParameter(dbcmd, "@FlowDispatchFlag", DbType.String, r.FlowDispatchFlag.Value)
                        db.AddInParameter(dbcmd, "@FlowCaseStatus", DbType.String, r.FlowCaseStatus.Value)
                        db.AddInParameter(dbcmd, "@CrDate", DbType.Date, IIf(IsDateTimeNull(r.CrDate.Value), DBNull.Value, r.CrDate.Value))
                        db.AddInParameter(dbcmd, "@CrBr", DbType.String, r.CrBr.Value)
                        db.AddInParameter(dbcmd, "@CrBrName", DbType.String, r.CrBrName.Value)
                        db.AddInParameter(dbcmd, "@CrUser", DbType.String, r.CrUser.Value)
                        db.AddInParameter(dbcmd, "@CrUserName", DbType.String, r.CrUserName.Value)
                        db.AddInParameter(dbcmd, "@FromDate", DbType.Date, IIf(IsDateTimeNull(r.FromDate.Value), DBNull.Value, r.FromDate.Value))
                        db.AddInParameter(dbcmd, "@FromBr", DbType.String, r.FromBr.Value)
                        db.AddInParameter(dbcmd, "@FromBrName", DbType.String, r.FromBrName.Value)
                        db.AddInParameter(dbcmd, "@FromUser", DbType.String, r.FromUser.Value)
                        db.AddInParameter(dbcmd, "@FromUserName", DbType.String, r.FromUserName.Value)
                        db.AddInParameter(dbcmd, "@PaperID", DbType.String, r.PaperID.Value)

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

        Public Function Insert(ByVal WF_FlowToDoListRow As Row(), ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into WF_FlowToDoList")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    AssignTo, FlowID, FlowCaseID, FlowLogBatNo, FlowLogID, FlowStepID, FlowStepDesc, IsBoss,")
            strSQL.AppendLine("    FlowKeyValue, FlowShowValue, FlowDispatchFlag, FlowCaseStatus, CrDate, CrBr, CrBrName,")
            strSQL.AppendLine("    CrUser, CrUserName, FromDate, FromBr, FromBrName, FromUser, FromUserName, PaperID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @AssignTo, @FlowID, @FlowCaseID, @FlowLogBatNo, @FlowLogID, @FlowStepID, @FlowStepDesc, @IsBoss,")
            strSQL.AppendLine("    @FlowKeyValue, @FlowShowValue, @FlowDispatchFlag, @FlowCaseStatus, @CrDate, @CrBr, @CrBrName,")
            strSQL.AppendLine("    @CrUser, @CrUserName, @FromDate, @FromBr, @FromBrName, @FromUser, @FromUserName, @PaperID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In WF_FlowToDoListRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@AssignTo", DbType.String, r.AssignTo.Value)
                db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, r.FlowCaseID.Value)
                db.AddInParameter(dbcmd, "@FlowLogBatNo", DbType.Int32, r.FlowLogBatNo.Value)
                db.AddInParameter(dbcmd, "@FlowLogID", DbType.String, r.FlowLogID.Value)
                db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, r.FlowStepID.Value)
                db.AddInParameter(dbcmd, "@FlowStepDesc", DbType.String, r.FlowStepDesc.Value)
                db.AddInParameter(dbcmd, "@IsBoss", DbType.String, r.IsBoss.Value)
                db.AddInParameter(dbcmd, "@FlowKeyValue", DbType.String, r.FlowKeyValue.Value)
                db.AddInParameter(dbcmd, "@FlowShowValue", DbType.String, r.FlowShowValue.Value)
                db.AddInParameter(dbcmd, "@FlowDispatchFlag", DbType.String, r.FlowDispatchFlag.Value)
                db.AddInParameter(dbcmd, "@FlowCaseStatus", DbType.String, r.FlowCaseStatus.Value)
                db.AddInParameter(dbcmd, "@CrDate", DbType.Date, IIf(IsDateTimeNull(r.CrDate.Value), DBNull.Value, r.CrDate.Value))
                db.AddInParameter(dbcmd, "@CrBr", DbType.String, r.CrBr.Value)
                db.AddInParameter(dbcmd, "@CrBrName", DbType.String, r.CrBrName.Value)
                db.AddInParameter(dbcmd, "@CrUser", DbType.String, r.CrUser.Value)
                db.AddInParameter(dbcmd, "@CrUserName", DbType.String, r.CrUserName.Value)
                db.AddInParameter(dbcmd, "@FromDate", DbType.Date, IIf(IsDateTimeNull(r.FromDate.Value), DBNull.Value, r.FromDate.Value))
                db.AddInParameter(dbcmd, "@FromBr", DbType.String, r.FromBr.Value)
                db.AddInParameter(dbcmd, "@FromBrName", DbType.String, r.FromBrName.Value)
                db.AddInParameter(dbcmd, "@FromUser", DbType.String, r.FromUser.Value)
                db.AddInParameter(dbcmd, "@FromUserName", DbType.String, r.FromUserName.Value)
                db.AddInParameter(dbcmd, "@PaperID", DbType.String, r.PaperID.Value)

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

