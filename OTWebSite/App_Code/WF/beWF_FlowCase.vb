'****************************************************************
' Table:WF_FlowCase
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

Namespace beWF_FlowCase
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "FlowCaseID", "FlowID", "FlowVer", "FlowKeyValue", "FlowShowValue", "FlowCaseStatus", "FlowDispatchFlag", "FlowCurrStepID", "FlowCurrStepDesc", "FlowCurrStepDueDate" _
                                    , "FlowSubCaseFlag", "LastLogBatNo", "LastLogID", "CrBr", "CrBrName", "CrUser", "CrUserName", "CrDate", "UpdDate", "PaperID" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(Integer), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(Integer), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(Date), GetType(String) }
        Private m_PrimaryFields As String() = { "FlowCaseID", "FlowID" }

        Public ReadOnly Property Rows() As beWF_FlowCase.Rows 
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
        Public Sub Transfer2Row(WF_FlowCaseTable As DataTable)
            For Each dr As DataRow In WF_FlowCaseTable.Rows
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

                dr(m_Rows(i).FlowCaseID.FieldName) = m_Rows(i).FlowCaseID.Value
                dr(m_Rows(i).FlowID.FieldName) = m_Rows(i).FlowID.Value
                dr(m_Rows(i).FlowVer.FieldName) = m_Rows(i).FlowVer.Value
                dr(m_Rows(i).FlowKeyValue.FieldName) = m_Rows(i).FlowKeyValue.Value
                dr(m_Rows(i).FlowShowValue.FieldName) = m_Rows(i).FlowShowValue.Value
                dr(m_Rows(i).FlowCaseStatus.FieldName) = m_Rows(i).FlowCaseStatus.Value
                dr(m_Rows(i).FlowDispatchFlag.FieldName) = m_Rows(i).FlowDispatchFlag.Value
                dr(m_Rows(i).FlowCurrStepID.FieldName) = m_Rows(i).FlowCurrStepID.Value
                dr(m_Rows(i).FlowCurrStepDesc.FieldName) = m_Rows(i).FlowCurrStepDesc.Value
                dr(m_Rows(i).FlowCurrStepDueDate.FieldName) = m_Rows(i).FlowCurrStepDueDate.Value
                dr(m_Rows(i).FlowSubCaseFlag.FieldName) = m_Rows(i).FlowSubCaseFlag.Value
                dr(m_Rows(i).LastLogBatNo.FieldName) = m_Rows(i).LastLogBatNo.Value
                dr(m_Rows(i).LastLogID.FieldName) = m_Rows(i).LastLogID.Value
                dr(m_Rows(i).CrBr.FieldName) = m_Rows(i).CrBr.Value
                dr(m_Rows(i).CrBrName.FieldName) = m_Rows(i).CrBrName.Value
                dr(m_Rows(i).CrUser.FieldName) = m_Rows(i).CrUser.Value
                dr(m_Rows(i).CrUserName.FieldName) = m_Rows(i).CrUserName.Value
                dr(m_Rows(i).CrDate.FieldName) = m_Rows(i).CrDate.Value
                dr(m_Rows(i).UpdDate.FieldName) = m_Rows(i).UpdDate.Value
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

        Public Sub Add(WF_FlowCaseRow As Row)
            m_Rows.Add(WF_FlowCaseRow)
        End Sub

        Public Sub Remove(WF_FlowCaseRow As Row)
            If m_Rows.IndexOf(WF_FlowCaseRow) >= 0 Then
                m_Rows.Remove(WF_FlowCaseRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_FlowCaseID As Field(Of String) = new Field(Of String)("FlowCaseID", true)
        Private FI_FlowID As Field(Of String) = new Field(Of String)("FlowID", true)
        Private FI_FlowVer As Field(Of Integer) = new Field(Of Integer)("FlowVer", true)
        Private FI_FlowKeyValue As Field(Of String) = new Field(Of String)("FlowKeyValue", true)
        Private FI_FlowShowValue As Field(Of String) = new Field(Of String)("FlowShowValue", true)
        Private FI_FlowCaseStatus As Field(Of String) = new Field(Of String)("FlowCaseStatus", true)
        Private FI_FlowDispatchFlag As Field(Of String) = new Field(Of String)("FlowDispatchFlag", true)
        Private FI_FlowCurrStepID As Field(Of String) = new Field(Of String)("FlowCurrStepID", true)
        Private FI_FlowCurrStepDesc As Field(Of String) = new Field(Of String)("FlowCurrStepDesc", true)
        Private FI_FlowCurrStepDueDate As Field(Of String) = new Field(Of String)("FlowCurrStepDueDate", true)
        Private FI_FlowSubCaseFlag As Field(Of String) = new Field(Of String)("FlowSubCaseFlag", true)
        Private FI_LastLogBatNo As Field(Of Integer) = new Field(Of Integer)("LastLogBatNo", true)
        Private FI_LastLogID As Field(Of String) = new Field(Of String)("LastLogID", true)
        Private FI_CrBr As Field(Of String) = new Field(Of String)("CrBr", true)
        Private FI_CrBrName As Field(Of String) = new Field(Of String)("CrBrName", true)
        Private FI_CrUser As Field(Of String) = new Field(Of String)("CrUser", true)
        Private FI_CrUserName As Field(Of String) = new Field(Of String)("CrUserName", true)
        Private FI_CrDate As Field(Of Date) = new Field(Of Date)("CrDate", true)
        Private FI_UpdDate As Field(Of Date) = new Field(Of Date)("UpdDate", true)
        Private FI_PaperID As Field(Of String) = new Field(Of String)("PaperID", true)
        Private m_FieldNames As String() = { "FlowCaseID", "FlowID", "FlowVer", "FlowKeyValue", "FlowShowValue", "FlowCaseStatus", "FlowDispatchFlag", "FlowCurrStepID", "FlowCurrStepDesc", "FlowCurrStepDueDate" _
                                    , "FlowSubCaseFlag", "LastLogBatNo", "LastLogID", "CrBr", "CrBrName", "CrUser", "CrUserName", "CrDate", "UpdDate", "PaperID" }
        Private m_PrimaryFields As String() = { "FlowCaseID", "FlowID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "FlowCaseID"
                    Return FI_FlowCaseID.Value
                Case "FlowID"
                    Return FI_FlowID.Value
                Case "FlowVer"
                    Return FI_FlowVer.Value
                Case "FlowKeyValue"
                    Return FI_FlowKeyValue.Value
                Case "FlowShowValue"
                    Return FI_FlowShowValue.Value
                Case "FlowCaseStatus"
                    Return FI_FlowCaseStatus.Value
                Case "FlowDispatchFlag"
                    Return FI_FlowDispatchFlag.Value
                Case "FlowCurrStepID"
                    Return FI_FlowCurrStepID.Value
                Case "FlowCurrStepDesc"
                    Return FI_FlowCurrStepDesc.Value
                Case "FlowCurrStepDueDate"
                    Return FI_FlowCurrStepDueDate.Value
                Case "FlowSubCaseFlag"
                    Return FI_FlowSubCaseFlag.Value
                Case "LastLogBatNo"
                    Return FI_LastLogBatNo.Value
                Case "LastLogID"
                    Return FI_LastLogID.Value
                Case "CrBr"
                    Return FI_CrBr.Value
                Case "CrBrName"
                    Return FI_CrBrName.Value
                Case "CrUser"
                    Return FI_CrUser.Value
                Case "CrUserName"
                    Return FI_CrUserName.Value
                Case "CrDate"
                    Return FI_CrDate.Value
                Case "UpdDate"
                    Return FI_UpdDate.Value
                Case "PaperID"
                    Return FI_PaperID.Value
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Sub SetFieldValue(ByVal fieldName As String, ByVal value As Object)
            Select Case fieldName
                Case "FlowCaseID"
                    FI_FlowCaseID.SetValue(value)
                Case "FlowID"
                    FI_FlowID.SetValue(value)
                Case "FlowVer"
                    FI_FlowVer.SetValue(value)
                Case "FlowKeyValue"
                    FI_FlowKeyValue.SetValue(value)
                Case "FlowShowValue"
                    FI_FlowShowValue.SetValue(value)
                Case "FlowCaseStatus"
                    FI_FlowCaseStatus.SetValue(value)
                Case "FlowDispatchFlag"
                    FI_FlowDispatchFlag.SetValue(value)
                Case "FlowCurrStepID"
                    FI_FlowCurrStepID.SetValue(value)
                Case "FlowCurrStepDesc"
                    FI_FlowCurrStepDesc.SetValue(value)
                Case "FlowCurrStepDueDate"
                    FI_FlowCurrStepDueDate.SetValue(value)
                Case "FlowSubCaseFlag"
                    FI_FlowSubCaseFlag.SetValue(value)
                Case "LastLogBatNo"
                    FI_LastLogBatNo.SetValue(value)
                Case "LastLogID"
                    FI_LastLogID.SetValue(value)
                Case "CrBr"
                    FI_CrBr.SetValue(value)
                Case "CrBrName"
                    FI_CrBrName.SetValue(value)
                Case "CrUser"
                    FI_CrUser.SetValue(value)
                Case "CrUserName"
                    FI_CrUserName.SetValue(value)
                Case "CrDate"
                    FI_CrDate.SetValue(value)
                Case "UpdDate"
                    FI_UpdDate.SetValue(value)
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
                Case "FlowCaseID"
                    return FI_FlowCaseID.Updated
                Case "FlowID"
                    return FI_FlowID.Updated
                Case "FlowVer"
                    return FI_FlowVer.Updated
                Case "FlowKeyValue"
                    return FI_FlowKeyValue.Updated
                Case "FlowShowValue"
                    return FI_FlowShowValue.Updated
                Case "FlowCaseStatus"
                    return FI_FlowCaseStatus.Updated
                Case "FlowDispatchFlag"
                    return FI_FlowDispatchFlag.Updated
                Case "FlowCurrStepID"
                    return FI_FlowCurrStepID.Updated
                Case "FlowCurrStepDesc"
                    return FI_FlowCurrStepDesc.Updated
                Case "FlowCurrStepDueDate"
                    return FI_FlowCurrStepDueDate.Updated
                Case "FlowSubCaseFlag"
                    return FI_FlowSubCaseFlag.Updated
                Case "LastLogBatNo"
                    return FI_LastLogBatNo.Updated
                Case "LastLogID"
                    return FI_LastLogID.Updated
                Case "CrBr"
                    return FI_CrBr.Updated
                Case "CrBrName"
                    return FI_CrBrName.Updated
                Case "CrUser"
                    return FI_CrUser.Updated
                Case "CrUserName"
                    return FI_CrUserName.Updated
                Case "CrDate"
                    return FI_CrDate.Updated
                Case "UpdDate"
                    return FI_UpdDate.Updated
                Case "PaperID"
                    return FI_PaperID.Updated
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public Function CreateUpdateSQL(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "FlowCaseID"
                    return FI_FlowCaseID.CreateUpdateSQL
                Case "FlowID"
                    return FI_FlowID.CreateUpdateSQL
                Case "FlowVer"
                    return FI_FlowVer.CreateUpdateSQL
                Case "FlowKeyValue"
                    return FI_FlowKeyValue.CreateUpdateSQL
                Case "FlowShowValue"
                    return FI_FlowShowValue.CreateUpdateSQL
                Case "FlowCaseStatus"
                    return FI_FlowCaseStatus.CreateUpdateSQL
                Case "FlowDispatchFlag"
                    return FI_FlowDispatchFlag.CreateUpdateSQL
                Case "FlowCurrStepID"
                    return FI_FlowCurrStepID.CreateUpdateSQL
                Case "FlowCurrStepDesc"
                    return FI_FlowCurrStepDesc.CreateUpdateSQL
                Case "FlowCurrStepDueDate"
                    return FI_FlowCurrStepDueDate.CreateUpdateSQL
                Case "FlowSubCaseFlag"
                    return FI_FlowSubCaseFlag.CreateUpdateSQL
                Case "LastLogBatNo"
                    return FI_LastLogBatNo.CreateUpdateSQL
                Case "LastLogID"
                    return FI_LastLogID.CreateUpdateSQL
                Case "CrBr"
                    return FI_CrBr.CreateUpdateSQL
                Case "CrBrName"
                    return FI_CrBrName.CreateUpdateSQL
                Case "CrUser"
                    return FI_CrUser.CreateUpdateSQL
                Case "CrUserName"
                    return FI_CrUserName.CreateUpdateSQL
                Case "CrDate"
                    return FI_CrDate.CreateUpdateSQL
                Case "UpdDate"
                    return FI_UpdDate.CreateUpdateSQL
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
            FI_FlowCaseID.SetInitValue("")
            FI_FlowID.SetInitValue("")
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_FlowCaseID.SetInitValue(dr("FlowCaseID"))
            FI_FlowID.SetInitValue(dr("FlowID"))
            FI_FlowVer.SetInitValue(dr("FlowVer"))
            FI_FlowKeyValue.SetInitValue(dr("FlowKeyValue"))
            FI_FlowShowValue.SetInitValue(dr("FlowShowValue"))
            FI_FlowCaseStatus.SetInitValue(dr("FlowCaseStatus"))
            FI_FlowDispatchFlag.SetInitValue(dr("FlowDispatchFlag"))
            FI_FlowCurrStepID.SetInitValue(dr("FlowCurrStepID"))
            FI_FlowCurrStepDesc.SetInitValue(dr("FlowCurrStepDesc"))
            FI_FlowCurrStepDueDate.SetInitValue(dr("FlowCurrStepDueDate"))
            FI_FlowSubCaseFlag.SetInitValue(dr("FlowSubCaseFlag"))
            FI_LastLogBatNo.SetInitValue(dr("LastLogBatNo"))
            FI_LastLogID.SetInitValue(dr("LastLogID"))
            FI_CrBr.SetInitValue(dr("CrBr"))
            FI_CrBrName.SetInitValue(dr("CrBrName"))
            FI_CrUser.SetInitValue(dr("CrUser"))
            FI_CrUserName.SetInitValue(dr("CrUserName"))
            FI_CrDate.SetInitValue(dr("CrDate"))
            FI_UpdDate.SetInitValue(dr("UpdDate"))
            FI_PaperID.SetInitValue(dr("PaperID"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_FlowCaseID.Updated = False
            FI_FlowID.Updated = False
            FI_FlowVer.Updated = False
            FI_FlowKeyValue.Updated = False
            FI_FlowShowValue.Updated = False
            FI_FlowCaseStatus.Updated = False
            FI_FlowDispatchFlag.Updated = False
            FI_FlowCurrStepID.Updated = False
            FI_FlowCurrStepDesc.Updated = False
            FI_FlowCurrStepDueDate.Updated = False
            FI_FlowSubCaseFlag.Updated = False
            FI_LastLogBatNo.Updated = False
            FI_LastLogID.Updated = False
            FI_CrBr.Updated = False
            FI_CrBrName.Updated = False
            FI_CrUser.Updated = False
            FI_CrUserName.Updated = False
            FI_CrDate.Updated = False
            FI_UpdDate.Updated = False
            FI_PaperID.Updated = False
        End Sub

        Public ReadOnly Property FlowCaseID As Field(Of String) 
            Get
                Return FI_FlowCaseID
            End Get
        End Property

        Public ReadOnly Property FlowID As Field(Of String) 
            Get
                Return FI_FlowID
            End Get
        End Property

        Public ReadOnly Property FlowVer As Field(Of Integer) 
            Get
                Return FI_FlowVer
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

        Public ReadOnly Property FlowCaseStatus As Field(Of String) 
            Get
                Return FI_FlowCaseStatus
            End Get
        End Property

        Public ReadOnly Property FlowDispatchFlag As Field(Of String) 
            Get
                Return FI_FlowDispatchFlag
            End Get
        End Property

        Public ReadOnly Property FlowCurrStepID As Field(Of String) 
            Get
                Return FI_FlowCurrStepID
            End Get
        End Property

        Public ReadOnly Property FlowCurrStepDesc As Field(Of String) 
            Get
                Return FI_FlowCurrStepDesc
            End Get
        End Property

        Public ReadOnly Property FlowCurrStepDueDate As Field(Of String) 
            Get
                Return FI_FlowCurrStepDueDate
            End Get
        End Property

        Public ReadOnly Property FlowSubCaseFlag As Field(Of String) 
            Get
                Return FI_FlowSubCaseFlag
            End Get
        End Property

        Public ReadOnly Property LastLogBatNo As Field(Of Integer) 
            Get
                Return FI_LastLogBatNo
            End Get
        End Property

        Public ReadOnly Property LastLogID As Field(Of String) 
            Get
                Return FI_LastLogID
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

        Public ReadOnly Property PaperID As Field(Of String) 
            Get
                Return FI_PaperID
            End Get
        End Property

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal WF_FlowCaseRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From WF_FlowCase")
            strSQL.AppendLine("Where FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And FlowID = @FlowID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowCaseRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowCaseRow.FlowID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal WF_FlowCaseRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From WF_FlowCase")
            strSQL.AppendLine("Where FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And FlowID = @FlowID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowCaseRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowCaseRow.FlowID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal WF_FlowCaseRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From WF_FlowCase")
            strSQL.AppendLine("Where FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And FlowID = @FlowID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In WF_FlowCaseRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, r.FlowCaseID.Value)
                        db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal WF_FlowCaseRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From WF_FlowCase")
            strSQL.AppendLine("Where FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And FlowID = @FlowID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In WF_FlowCaseRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, r.FlowCaseID.Value)
                db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal WF_FlowCaseRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WF_FlowCase")
            strSQL.AppendLine("Where FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And FlowID = @FlowID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowCaseRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowCaseRow.FlowID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(WF_FlowCaseRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WF_FlowCase")
            strSQL.AppendLine("Where FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And FlowID = @FlowID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowCaseRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowCaseRow.FlowID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal WF_FlowCaseRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update WF_FlowCase Set")
            For i As Integer = 0 To WF_FlowCaseRow.FieldNames.Length - 1
                If Not WF_FlowCaseRow.IsIdentityField(WF_FlowCaseRow.FieldNames(i)) AndAlso WF_FlowCaseRow.IsUpdated(WF_FlowCaseRow.FieldNames(i)) AndAlso WF_FlowCaseRow.CreateUpdateSQL(WF_FlowCaseRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, WF_FlowCaseRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where FlowCaseID = @PKFlowCaseID")
            strSQL.AppendLine("And FlowID = @PKFlowID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If WF_FlowCaseRow.FlowCaseID.Updated Then db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowCaseRow.FlowCaseID.Value)
            If WF_FlowCaseRow.FlowID.Updated Then db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowCaseRow.FlowID.Value)
            If WF_FlowCaseRow.FlowVer.Updated Then db.AddInParameter(dbcmd, "@FlowVer", DbType.Int32, WF_FlowCaseRow.FlowVer.Value)
            If WF_FlowCaseRow.FlowKeyValue.Updated Then db.AddInParameter(dbcmd, "@FlowKeyValue", DbType.String, WF_FlowCaseRow.FlowKeyValue.Value)
            If WF_FlowCaseRow.FlowShowValue.Updated Then db.AddInParameter(dbcmd, "@FlowShowValue", DbType.String, WF_FlowCaseRow.FlowShowValue.Value)
            If WF_FlowCaseRow.FlowCaseStatus.Updated Then db.AddInParameter(dbcmd, "@FlowCaseStatus", DbType.String, WF_FlowCaseRow.FlowCaseStatus.Value)
            If WF_FlowCaseRow.FlowDispatchFlag.Updated Then db.AddInParameter(dbcmd, "@FlowDispatchFlag", DbType.String, WF_FlowCaseRow.FlowDispatchFlag.Value)
            If WF_FlowCaseRow.FlowCurrStepID.Updated Then db.AddInParameter(dbcmd, "@FlowCurrStepID", DbType.String, WF_FlowCaseRow.FlowCurrStepID.Value)
            If WF_FlowCaseRow.FlowCurrStepDesc.Updated Then db.AddInParameter(dbcmd, "@FlowCurrStepDesc", DbType.String, WF_FlowCaseRow.FlowCurrStepDesc.Value)
            If WF_FlowCaseRow.FlowCurrStepDueDate.Updated Then db.AddInParameter(dbcmd, "@FlowCurrStepDueDate", DbType.String, WF_FlowCaseRow.FlowCurrStepDueDate.Value)
            If WF_FlowCaseRow.FlowSubCaseFlag.Updated Then db.AddInParameter(dbcmd, "@FlowSubCaseFlag", DbType.String, WF_FlowCaseRow.FlowSubCaseFlag.Value)
            If WF_FlowCaseRow.LastLogBatNo.Updated Then db.AddInParameter(dbcmd, "@LastLogBatNo", DbType.Int32, WF_FlowCaseRow.LastLogBatNo.Value)
            If WF_FlowCaseRow.LastLogID.Updated Then db.AddInParameter(dbcmd, "@LastLogID", DbType.String, WF_FlowCaseRow.LastLogID.Value)
            If WF_FlowCaseRow.CrBr.Updated Then db.AddInParameter(dbcmd, "@CrBr", DbType.String, WF_FlowCaseRow.CrBr.Value)
            If WF_FlowCaseRow.CrBrName.Updated Then db.AddInParameter(dbcmd, "@CrBrName", DbType.String, WF_FlowCaseRow.CrBrName.Value)
            If WF_FlowCaseRow.CrUser.Updated Then db.AddInParameter(dbcmd, "@CrUser", DbType.String, WF_FlowCaseRow.CrUser.Value)
            If WF_FlowCaseRow.CrUserName.Updated Then db.AddInParameter(dbcmd, "@CrUserName", DbType.String, WF_FlowCaseRow.CrUserName.Value)
            If WF_FlowCaseRow.CrDate.Updated Then db.AddInParameter(dbcmd, "@CrDate", DbType.Date, IIf(IsDateTimeNull(WF_FlowCaseRow.CrDate.Value), DBNull.Value, WF_FlowCaseRow.CrDate.Value))
            If WF_FlowCaseRow.UpdDate.Updated Then db.AddInParameter(dbcmd, "@UpdDate", DbType.Date, IIf(IsDateTimeNull(WF_FlowCaseRow.UpdDate.Value), DBNull.Value, WF_FlowCaseRow.UpdDate.Value))
            If WF_FlowCaseRow.PaperID.Updated Then db.AddInParameter(dbcmd, "@PaperID", DbType.String, WF_FlowCaseRow.PaperID.Value)
            db.AddInParameter(dbcmd, "@PKFlowCaseID", DbType.String, IIf(WF_FlowCaseRow.LoadFromDataRow, WF_FlowCaseRow.FlowCaseID.OldValue, WF_FlowCaseRow.FlowCaseID.Value))
            db.AddInParameter(dbcmd, "@PKFlowID", DbType.String, IIf(WF_FlowCaseRow.LoadFromDataRow, WF_FlowCaseRow.FlowID.OldValue, WF_FlowCaseRow.FlowID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal WF_FlowCaseRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update WF_FlowCase Set")
            For i As Integer = 0 To WF_FlowCaseRow.FieldNames.Length - 1
                If Not WF_FlowCaseRow.IsIdentityField(WF_FlowCaseRow.FieldNames(i)) AndAlso WF_FlowCaseRow.IsUpdated(WF_FlowCaseRow.FieldNames(i)) AndAlso WF_FlowCaseRow.CreateUpdateSQL(WF_FlowCaseRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, WF_FlowCaseRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where FlowCaseID = @PKFlowCaseID")
            strSQL.AppendLine("And FlowID = @PKFlowID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If WF_FlowCaseRow.FlowCaseID.Updated Then db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowCaseRow.FlowCaseID.Value)
            If WF_FlowCaseRow.FlowID.Updated Then db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowCaseRow.FlowID.Value)
            If WF_FlowCaseRow.FlowVer.Updated Then db.AddInParameter(dbcmd, "@FlowVer", DbType.Int32, WF_FlowCaseRow.FlowVer.Value)
            If WF_FlowCaseRow.FlowKeyValue.Updated Then db.AddInParameter(dbcmd, "@FlowKeyValue", DbType.String, WF_FlowCaseRow.FlowKeyValue.Value)
            If WF_FlowCaseRow.FlowShowValue.Updated Then db.AddInParameter(dbcmd, "@FlowShowValue", DbType.String, WF_FlowCaseRow.FlowShowValue.Value)
            If WF_FlowCaseRow.FlowCaseStatus.Updated Then db.AddInParameter(dbcmd, "@FlowCaseStatus", DbType.String, WF_FlowCaseRow.FlowCaseStatus.Value)
            If WF_FlowCaseRow.FlowDispatchFlag.Updated Then db.AddInParameter(dbcmd, "@FlowDispatchFlag", DbType.String, WF_FlowCaseRow.FlowDispatchFlag.Value)
            If WF_FlowCaseRow.FlowCurrStepID.Updated Then db.AddInParameter(dbcmd, "@FlowCurrStepID", DbType.String, WF_FlowCaseRow.FlowCurrStepID.Value)
            If WF_FlowCaseRow.FlowCurrStepDesc.Updated Then db.AddInParameter(dbcmd, "@FlowCurrStepDesc", DbType.String, WF_FlowCaseRow.FlowCurrStepDesc.Value)
            If WF_FlowCaseRow.FlowCurrStepDueDate.Updated Then db.AddInParameter(dbcmd, "@FlowCurrStepDueDate", DbType.String, WF_FlowCaseRow.FlowCurrStepDueDate.Value)
            If WF_FlowCaseRow.FlowSubCaseFlag.Updated Then db.AddInParameter(dbcmd, "@FlowSubCaseFlag", DbType.String, WF_FlowCaseRow.FlowSubCaseFlag.Value)
            If WF_FlowCaseRow.LastLogBatNo.Updated Then db.AddInParameter(dbcmd, "@LastLogBatNo", DbType.Int32, WF_FlowCaseRow.LastLogBatNo.Value)
            If WF_FlowCaseRow.LastLogID.Updated Then db.AddInParameter(dbcmd, "@LastLogID", DbType.String, WF_FlowCaseRow.LastLogID.Value)
            If WF_FlowCaseRow.CrBr.Updated Then db.AddInParameter(dbcmd, "@CrBr", DbType.String, WF_FlowCaseRow.CrBr.Value)
            If WF_FlowCaseRow.CrBrName.Updated Then db.AddInParameter(dbcmd, "@CrBrName", DbType.String, WF_FlowCaseRow.CrBrName.Value)
            If WF_FlowCaseRow.CrUser.Updated Then db.AddInParameter(dbcmd, "@CrUser", DbType.String, WF_FlowCaseRow.CrUser.Value)
            If WF_FlowCaseRow.CrUserName.Updated Then db.AddInParameter(dbcmd, "@CrUserName", DbType.String, WF_FlowCaseRow.CrUserName.Value)
            If WF_FlowCaseRow.CrDate.Updated Then db.AddInParameter(dbcmd, "@CrDate", DbType.Date, IIf(IsDateTimeNull(WF_FlowCaseRow.CrDate.Value), DBNull.Value, WF_FlowCaseRow.CrDate.Value))
            If WF_FlowCaseRow.UpdDate.Updated Then db.AddInParameter(dbcmd, "@UpdDate", DbType.Date, IIf(IsDateTimeNull(WF_FlowCaseRow.UpdDate.Value), DBNull.Value, WF_FlowCaseRow.UpdDate.Value))
            If WF_FlowCaseRow.PaperID.Updated Then db.AddInParameter(dbcmd, "@PaperID", DbType.String, WF_FlowCaseRow.PaperID.Value)
            db.AddInParameter(dbcmd, "@PKFlowCaseID", DbType.String, IIf(WF_FlowCaseRow.LoadFromDataRow, WF_FlowCaseRow.FlowCaseID.OldValue, WF_FlowCaseRow.FlowCaseID.Value))
            db.AddInParameter(dbcmd, "@PKFlowID", DbType.String, IIf(WF_FlowCaseRow.LoadFromDataRow, WF_FlowCaseRow.FlowID.OldValue, WF_FlowCaseRow.FlowID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal WF_FlowCaseRow As Row()) As Integer
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
                    For Each r As Row In WF_FlowCaseRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update WF_FlowCase Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where FlowCaseID = @PKFlowCaseID")
                        strSQL.AppendLine("And FlowID = @PKFlowID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.FlowCaseID.Updated Then db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, r.FlowCaseID.Value)
                        If r.FlowID.Updated Then db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                        If r.FlowVer.Updated Then db.AddInParameter(dbcmd, "@FlowVer", DbType.Int32, r.FlowVer.Value)
                        If r.FlowKeyValue.Updated Then db.AddInParameter(dbcmd, "@FlowKeyValue", DbType.String, r.FlowKeyValue.Value)
                        If r.FlowShowValue.Updated Then db.AddInParameter(dbcmd, "@FlowShowValue", DbType.String, r.FlowShowValue.Value)
                        If r.FlowCaseStatus.Updated Then db.AddInParameter(dbcmd, "@FlowCaseStatus", DbType.String, r.FlowCaseStatus.Value)
                        If r.FlowDispatchFlag.Updated Then db.AddInParameter(dbcmd, "@FlowDispatchFlag", DbType.String, r.FlowDispatchFlag.Value)
                        If r.FlowCurrStepID.Updated Then db.AddInParameter(dbcmd, "@FlowCurrStepID", DbType.String, r.FlowCurrStepID.Value)
                        If r.FlowCurrStepDesc.Updated Then db.AddInParameter(dbcmd, "@FlowCurrStepDesc", DbType.String, r.FlowCurrStepDesc.Value)
                        If r.FlowCurrStepDueDate.Updated Then db.AddInParameter(dbcmd, "@FlowCurrStepDueDate", DbType.String, r.FlowCurrStepDueDate.Value)
                        If r.FlowSubCaseFlag.Updated Then db.AddInParameter(dbcmd, "@FlowSubCaseFlag", DbType.String, r.FlowSubCaseFlag.Value)
                        If r.LastLogBatNo.Updated Then db.AddInParameter(dbcmd, "@LastLogBatNo", DbType.Int32, r.LastLogBatNo.Value)
                        If r.LastLogID.Updated Then db.AddInParameter(dbcmd, "@LastLogID", DbType.String, r.LastLogID.Value)
                        If r.CrBr.Updated Then db.AddInParameter(dbcmd, "@CrBr", DbType.String, r.CrBr.Value)
                        If r.CrBrName.Updated Then db.AddInParameter(dbcmd, "@CrBrName", DbType.String, r.CrBrName.Value)
                        If r.CrUser.Updated Then db.AddInParameter(dbcmd, "@CrUser", DbType.String, r.CrUser.Value)
                        If r.CrUserName.Updated Then db.AddInParameter(dbcmd, "@CrUserName", DbType.String, r.CrUserName.Value)
                        If r.CrDate.Updated Then db.AddInParameter(dbcmd, "@CrDate", DbType.Date, IIf(IsDateTimeNull(r.CrDate.Value), DBNull.Value, r.CrDate.Value))
                        If r.UpdDate.Updated Then db.AddInParameter(dbcmd, "@UpdDate", DbType.Date, IIf(IsDateTimeNull(r.UpdDate.Value), DBNull.Value, r.UpdDate.Value))
                        If r.PaperID.Updated Then db.AddInParameter(dbcmd, "@PaperID", DbType.String, r.PaperID.Value)
                        db.AddInParameter(dbcmd, "@PKFlowCaseID", DbType.String, IIf(r.LoadFromDataRow, r.FlowCaseID.OldValue, r.FlowCaseID.Value))
                        db.AddInParameter(dbcmd, "@PKFlowID", DbType.String, IIf(r.LoadFromDataRow, r.FlowID.OldValue, r.FlowID.Value))

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

        Public Function Update(ByVal WF_FlowCaseRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In WF_FlowCaseRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update WF_FlowCase Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where FlowCaseID = @PKFlowCaseID")
                strSQL.AppendLine("And FlowID = @PKFlowID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.FlowCaseID.Updated Then db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, r.FlowCaseID.Value)
                If r.FlowID.Updated Then db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                If r.FlowVer.Updated Then db.AddInParameter(dbcmd, "@FlowVer", DbType.Int32, r.FlowVer.Value)
                If r.FlowKeyValue.Updated Then db.AddInParameter(dbcmd, "@FlowKeyValue", DbType.String, r.FlowKeyValue.Value)
                If r.FlowShowValue.Updated Then db.AddInParameter(dbcmd, "@FlowShowValue", DbType.String, r.FlowShowValue.Value)
                If r.FlowCaseStatus.Updated Then db.AddInParameter(dbcmd, "@FlowCaseStatus", DbType.String, r.FlowCaseStatus.Value)
                If r.FlowDispatchFlag.Updated Then db.AddInParameter(dbcmd, "@FlowDispatchFlag", DbType.String, r.FlowDispatchFlag.Value)
                If r.FlowCurrStepID.Updated Then db.AddInParameter(dbcmd, "@FlowCurrStepID", DbType.String, r.FlowCurrStepID.Value)
                If r.FlowCurrStepDesc.Updated Then db.AddInParameter(dbcmd, "@FlowCurrStepDesc", DbType.String, r.FlowCurrStepDesc.Value)
                If r.FlowCurrStepDueDate.Updated Then db.AddInParameter(dbcmd, "@FlowCurrStepDueDate", DbType.String, r.FlowCurrStepDueDate.Value)
                If r.FlowSubCaseFlag.Updated Then db.AddInParameter(dbcmd, "@FlowSubCaseFlag", DbType.String, r.FlowSubCaseFlag.Value)
                If r.LastLogBatNo.Updated Then db.AddInParameter(dbcmd, "@LastLogBatNo", DbType.Int32, r.LastLogBatNo.Value)
                If r.LastLogID.Updated Then db.AddInParameter(dbcmd, "@LastLogID", DbType.String, r.LastLogID.Value)
                If r.CrBr.Updated Then db.AddInParameter(dbcmd, "@CrBr", DbType.String, r.CrBr.Value)
                If r.CrBrName.Updated Then db.AddInParameter(dbcmd, "@CrBrName", DbType.String, r.CrBrName.Value)
                If r.CrUser.Updated Then db.AddInParameter(dbcmd, "@CrUser", DbType.String, r.CrUser.Value)
                If r.CrUserName.Updated Then db.AddInParameter(dbcmd, "@CrUserName", DbType.String, r.CrUserName.Value)
                If r.CrDate.Updated Then db.AddInParameter(dbcmd, "@CrDate", DbType.Date, IIf(IsDateTimeNull(r.CrDate.Value), DBNull.Value, r.CrDate.Value))
                If r.UpdDate.Updated Then db.AddInParameter(dbcmd, "@UpdDate", DbType.Date, IIf(IsDateTimeNull(r.UpdDate.Value), DBNull.Value, r.UpdDate.Value))
                If r.PaperID.Updated Then db.AddInParameter(dbcmd, "@PaperID", DbType.String, r.PaperID.Value)
                db.AddInParameter(dbcmd, "@PKFlowCaseID", DbType.String, IIf(r.LoadFromDataRow, r.FlowCaseID.OldValue, r.FlowCaseID.Value))
                db.AddInParameter(dbcmd, "@PKFlowID", DbType.String, IIf(r.LoadFromDataRow, r.FlowID.OldValue, r.FlowID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal WF_FlowCaseRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From WF_FlowCase")
            strSQL.AppendLine("Where FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And FlowID = @FlowID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowCaseRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowCaseRow.FlowID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal WF_FlowCaseRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From WF_FlowCase")
            strSQL.AppendLine("Where FlowCaseID = @FlowCaseID")
            strSQL.AppendLine("And FlowID = @FlowID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowCaseRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowCaseRow.FlowID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WF_FlowCase")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal WF_FlowCaseRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into WF_FlowCase")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowCaseID, FlowID, FlowVer, FlowKeyValue, FlowShowValue, FlowCaseStatus, FlowDispatchFlag,")
            strSQL.AppendLine("    FlowCurrStepID, FlowCurrStepDesc, FlowCurrStepDueDate, FlowSubCaseFlag, LastLogBatNo,")
            strSQL.AppendLine("    LastLogID, CrBr, CrBrName, CrUser, CrUserName, CrDate, UpdDate, PaperID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowCaseID, @FlowID, @FlowVer, @FlowKeyValue, @FlowShowValue, @FlowCaseStatus, @FlowDispatchFlag,")
            strSQL.AppendLine("    @FlowCurrStepID, @FlowCurrStepDesc, @FlowCurrStepDueDate, @FlowSubCaseFlag, @LastLogBatNo,")
            strSQL.AppendLine("    @LastLogID, @CrBr, @CrBrName, @CrUser, @CrUserName, @CrDate, @UpdDate, @PaperID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowCaseRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowCaseRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowVer", DbType.Int32, WF_FlowCaseRow.FlowVer.Value)
            db.AddInParameter(dbcmd, "@FlowKeyValue", DbType.String, WF_FlowCaseRow.FlowKeyValue.Value)
            db.AddInParameter(dbcmd, "@FlowShowValue", DbType.String, WF_FlowCaseRow.FlowShowValue.Value)
            db.AddInParameter(dbcmd, "@FlowCaseStatus", DbType.String, WF_FlowCaseRow.FlowCaseStatus.Value)
            db.AddInParameter(dbcmd, "@FlowDispatchFlag", DbType.String, WF_FlowCaseRow.FlowDispatchFlag.Value)
            db.AddInParameter(dbcmd, "@FlowCurrStepID", DbType.String, WF_FlowCaseRow.FlowCurrStepID.Value)
            db.AddInParameter(dbcmd, "@FlowCurrStepDesc", DbType.String, WF_FlowCaseRow.FlowCurrStepDesc.Value)
            db.AddInParameter(dbcmd, "@FlowCurrStepDueDate", DbType.String, WF_FlowCaseRow.FlowCurrStepDueDate.Value)
            db.AddInParameter(dbcmd, "@FlowSubCaseFlag", DbType.String, WF_FlowCaseRow.FlowSubCaseFlag.Value)
            db.AddInParameter(dbcmd, "@LastLogBatNo", DbType.Int32, WF_FlowCaseRow.LastLogBatNo.Value)
            db.AddInParameter(dbcmd, "@LastLogID", DbType.String, WF_FlowCaseRow.LastLogID.Value)
            db.AddInParameter(dbcmd, "@CrBr", DbType.String, WF_FlowCaseRow.CrBr.Value)
            db.AddInParameter(dbcmd, "@CrBrName", DbType.String, WF_FlowCaseRow.CrBrName.Value)
            db.AddInParameter(dbcmd, "@CrUser", DbType.String, WF_FlowCaseRow.CrUser.Value)
            db.AddInParameter(dbcmd, "@CrUserName", DbType.String, WF_FlowCaseRow.CrUserName.Value)
            db.AddInParameter(dbcmd, "@CrDate", DbType.Date, IIf(IsDateTimeNull(WF_FlowCaseRow.CrDate.Value), DBNull.Value, WF_FlowCaseRow.CrDate.Value))
            db.AddInParameter(dbcmd, "@UpdDate", DbType.Date, IIf(IsDateTimeNull(WF_FlowCaseRow.UpdDate.Value), DBNull.Value, WF_FlowCaseRow.UpdDate.Value))
            db.AddInParameter(dbcmd, "@PaperID", DbType.String, WF_FlowCaseRow.PaperID.Value)

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal WF_FlowCaseRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into WF_FlowCase")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowCaseID, FlowID, FlowVer, FlowKeyValue, FlowShowValue, FlowCaseStatus, FlowDispatchFlag,")
            strSQL.AppendLine("    FlowCurrStepID, FlowCurrStepDesc, FlowCurrStepDueDate, FlowSubCaseFlag, LastLogBatNo,")
            strSQL.AppendLine("    LastLogID, CrBr, CrBrName, CrUser, CrUserName, CrDate, UpdDate, PaperID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowCaseID, @FlowID, @FlowVer, @FlowKeyValue, @FlowShowValue, @FlowCaseStatus, @FlowDispatchFlag,")
            strSQL.AppendLine("    @FlowCurrStepID, @FlowCurrStepDesc, @FlowCurrStepDueDate, @FlowSubCaseFlag, @LastLogBatNo,")
            strSQL.AppendLine("    @LastLogID, @CrBr, @CrBrName, @CrUser, @CrUserName, @CrDate, @UpdDate, @PaperID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowCaseRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowCaseRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowVer", DbType.Int32, WF_FlowCaseRow.FlowVer.Value)
            db.AddInParameter(dbcmd, "@FlowKeyValue", DbType.String, WF_FlowCaseRow.FlowKeyValue.Value)
            db.AddInParameter(dbcmd, "@FlowShowValue", DbType.String, WF_FlowCaseRow.FlowShowValue.Value)
            db.AddInParameter(dbcmd, "@FlowCaseStatus", DbType.String, WF_FlowCaseRow.FlowCaseStatus.Value)
            db.AddInParameter(dbcmd, "@FlowDispatchFlag", DbType.String, WF_FlowCaseRow.FlowDispatchFlag.Value)
            db.AddInParameter(dbcmd, "@FlowCurrStepID", DbType.String, WF_FlowCaseRow.FlowCurrStepID.Value)
            db.AddInParameter(dbcmd, "@FlowCurrStepDesc", DbType.String, WF_FlowCaseRow.FlowCurrStepDesc.Value)
            db.AddInParameter(dbcmd, "@FlowCurrStepDueDate", DbType.String, WF_FlowCaseRow.FlowCurrStepDueDate.Value)
            db.AddInParameter(dbcmd, "@FlowSubCaseFlag", DbType.String, WF_FlowCaseRow.FlowSubCaseFlag.Value)
            db.AddInParameter(dbcmd, "@LastLogBatNo", DbType.Int32, WF_FlowCaseRow.LastLogBatNo.Value)
            db.AddInParameter(dbcmd, "@LastLogID", DbType.String, WF_FlowCaseRow.LastLogID.Value)
            db.AddInParameter(dbcmd, "@CrBr", DbType.String, WF_FlowCaseRow.CrBr.Value)
            db.AddInParameter(dbcmd, "@CrBrName", DbType.String, WF_FlowCaseRow.CrBrName.Value)
            db.AddInParameter(dbcmd, "@CrUser", DbType.String, WF_FlowCaseRow.CrUser.Value)
            db.AddInParameter(dbcmd, "@CrUserName", DbType.String, WF_FlowCaseRow.CrUserName.Value)
            db.AddInParameter(dbcmd, "@CrDate", DbType.Date, IIf(IsDateTimeNull(WF_FlowCaseRow.CrDate.Value), DBNull.Value, WF_FlowCaseRow.CrDate.Value))
            db.AddInParameter(dbcmd, "@UpdDate", DbType.Date, IIf(IsDateTimeNull(WF_FlowCaseRow.UpdDate.Value), DBNull.Value, WF_FlowCaseRow.UpdDate.Value))
            db.AddInParameter(dbcmd, "@PaperID", DbType.String, WF_FlowCaseRow.PaperID.Value)

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal WF_FlowCaseRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into WF_FlowCase")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowCaseID, FlowID, FlowVer, FlowKeyValue, FlowShowValue, FlowCaseStatus, FlowDispatchFlag,")
            strSQL.AppendLine("    FlowCurrStepID, FlowCurrStepDesc, FlowCurrStepDueDate, FlowSubCaseFlag, LastLogBatNo,")
            strSQL.AppendLine("    LastLogID, CrBr, CrBrName, CrUser, CrUserName, CrDate, UpdDate, PaperID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowCaseID, @FlowID, @FlowVer, @FlowKeyValue, @FlowShowValue, @FlowCaseStatus, @FlowDispatchFlag,")
            strSQL.AppendLine("    @FlowCurrStepID, @FlowCurrStepDesc, @FlowCurrStepDueDate, @FlowSubCaseFlag, @LastLogBatNo,")
            strSQL.AppendLine("    @LastLogID, @CrBr, @CrBrName, @CrUser, @CrUserName, @CrDate, @UpdDate, @PaperID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In WF_FlowCaseRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, r.FlowCaseID.Value)
                        db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                        db.AddInParameter(dbcmd, "@FlowVer", DbType.Int32, r.FlowVer.Value)
                        db.AddInParameter(dbcmd, "@FlowKeyValue", DbType.String, r.FlowKeyValue.Value)
                        db.AddInParameter(dbcmd, "@FlowShowValue", DbType.String, r.FlowShowValue.Value)
                        db.AddInParameter(dbcmd, "@FlowCaseStatus", DbType.String, r.FlowCaseStatus.Value)
                        db.AddInParameter(dbcmd, "@FlowDispatchFlag", DbType.String, r.FlowDispatchFlag.Value)
                        db.AddInParameter(dbcmd, "@FlowCurrStepID", DbType.String, r.FlowCurrStepID.Value)
                        db.AddInParameter(dbcmd, "@FlowCurrStepDesc", DbType.String, r.FlowCurrStepDesc.Value)
                        db.AddInParameter(dbcmd, "@FlowCurrStepDueDate", DbType.String, r.FlowCurrStepDueDate.Value)
                        db.AddInParameter(dbcmd, "@FlowSubCaseFlag", DbType.String, r.FlowSubCaseFlag.Value)
                        db.AddInParameter(dbcmd, "@LastLogBatNo", DbType.Int32, r.LastLogBatNo.Value)
                        db.AddInParameter(dbcmd, "@LastLogID", DbType.String, r.LastLogID.Value)
                        db.AddInParameter(dbcmd, "@CrBr", DbType.String, r.CrBr.Value)
                        db.AddInParameter(dbcmd, "@CrBrName", DbType.String, r.CrBrName.Value)
                        db.AddInParameter(dbcmd, "@CrUser", DbType.String, r.CrUser.Value)
                        db.AddInParameter(dbcmd, "@CrUserName", DbType.String, r.CrUserName.Value)
                        db.AddInParameter(dbcmd, "@CrDate", DbType.Date, IIf(IsDateTimeNull(r.CrDate.Value), DBNull.Value, r.CrDate.Value))
                        db.AddInParameter(dbcmd, "@UpdDate", DbType.Date, IIf(IsDateTimeNull(r.UpdDate.Value), DBNull.Value, r.UpdDate.Value))
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

        Public Function Insert(ByVal WF_FlowCaseRow As Row(), ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into WF_FlowCase")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowCaseID, FlowID, FlowVer, FlowKeyValue, FlowShowValue, FlowCaseStatus, FlowDispatchFlag,")
            strSQL.AppendLine("    FlowCurrStepID, FlowCurrStepDesc, FlowCurrStepDueDate, FlowSubCaseFlag, LastLogBatNo,")
            strSQL.AppendLine("    LastLogID, CrBr, CrBrName, CrUser, CrUserName, CrDate, UpdDate, PaperID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowCaseID, @FlowID, @FlowVer, @FlowKeyValue, @FlowShowValue, @FlowCaseStatus, @FlowDispatchFlag,")
            strSQL.AppendLine("    @FlowCurrStepID, @FlowCurrStepDesc, @FlowCurrStepDueDate, @FlowSubCaseFlag, @LastLogBatNo,")
            strSQL.AppendLine("    @LastLogID, @CrBr, @CrBrName, @CrUser, @CrUserName, @CrDate, @UpdDate, @PaperID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In WF_FlowCaseRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, r.FlowCaseID.Value)
                db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                db.AddInParameter(dbcmd, "@FlowVer", DbType.Int32, r.FlowVer.Value)
                db.AddInParameter(dbcmd, "@FlowKeyValue", DbType.String, r.FlowKeyValue.Value)
                db.AddInParameter(dbcmd, "@FlowShowValue", DbType.String, r.FlowShowValue.Value)
                db.AddInParameter(dbcmd, "@FlowCaseStatus", DbType.String, r.FlowCaseStatus.Value)
                db.AddInParameter(dbcmd, "@FlowDispatchFlag", DbType.String, r.FlowDispatchFlag.Value)
                db.AddInParameter(dbcmd, "@FlowCurrStepID", DbType.String, r.FlowCurrStepID.Value)
                db.AddInParameter(dbcmd, "@FlowCurrStepDesc", DbType.String, r.FlowCurrStepDesc.Value)
                db.AddInParameter(dbcmd, "@FlowCurrStepDueDate", DbType.String, r.FlowCurrStepDueDate.Value)
                db.AddInParameter(dbcmd, "@FlowSubCaseFlag", DbType.String, r.FlowSubCaseFlag.Value)
                db.AddInParameter(dbcmd, "@LastLogBatNo", DbType.Int32, r.LastLogBatNo.Value)
                db.AddInParameter(dbcmd, "@LastLogID", DbType.String, r.LastLogID.Value)
                db.AddInParameter(dbcmd, "@CrBr", DbType.String, r.CrBr.Value)
                db.AddInParameter(dbcmd, "@CrBrName", DbType.String, r.CrBrName.Value)
                db.AddInParameter(dbcmd, "@CrUser", DbType.String, r.CrUser.Value)
                db.AddInParameter(dbcmd, "@CrUserName", DbType.String, r.CrUserName.Value)
                db.AddInParameter(dbcmd, "@CrDate", DbType.Date, IIf(IsDateTimeNull(r.CrDate.Value), DBNull.Value, r.CrDate.Value))
                db.AddInParameter(dbcmd, "@UpdDate", DbType.Date, IIf(IsDateTimeNull(r.UpdDate.Value), DBNull.Value, r.UpdDate.Value))
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

