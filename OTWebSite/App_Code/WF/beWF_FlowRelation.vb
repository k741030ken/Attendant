'****************************************************************
' Table:WF_FlowRelation
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

Namespace beWF_FlowRelation
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "FlowID", "FlowCaseID", "ParentFlowID", "ParentFlowCaseID", "ParentFlowStepID", "ParentFlowLogBatNo", "FlowCaseStatus" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Integer), GetType(String) }
        Private m_PrimaryFields As String() = { "FlowID", "FlowCaseID" }

        Public ReadOnly Property Rows() As beWF_FlowRelation.Rows 
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
        Public Sub Transfer2Row(WF_FlowRelationTable As DataTable)
            For Each dr As DataRow In WF_FlowRelationTable.Rows
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
                dr(m_Rows(i).ParentFlowID.FieldName) = m_Rows(i).ParentFlowID.Value
                dr(m_Rows(i).ParentFlowCaseID.FieldName) = m_Rows(i).ParentFlowCaseID.Value
                dr(m_Rows(i).ParentFlowStepID.FieldName) = m_Rows(i).ParentFlowStepID.Value
                dr(m_Rows(i).ParentFlowLogBatNo.FieldName) = m_Rows(i).ParentFlowLogBatNo.Value
                dr(m_Rows(i).FlowCaseStatus.FieldName) = m_Rows(i).FlowCaseStatus.Value

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

        Public Sub Add(WF_FlowRelationRow As Row)
            m_Rows.Add(WF_FlowRelationRow)
        End Sub

        Public Sub Remove(WF_FlowRelationRow As Row)
            If m_Rows.IndexOf(WF_FlowRelationRow) >= 0 Then
                m_Rows.Remove(WF_FlowRelationRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_FlowID As Field(Of String) = new Field(Of String)("FlowID", true)
        Private FI_FlowCaseID As Field(Of String) = new Field(Of String)("FlowCaseID", true)
        Private FI_ParentFlowID As Field(Of String) = new Field(Of String)("ParentFlowID", true)
        Private FI_ParentFlowCaseID As Field(Of String) = new Field(Of String)("ParentFlowCaseID", true)
        Private FI_ParentFlowStepID As Field(Of String) = new Field(Of String)("ParentFlowStepID", true)
        Private FI_ParentFlowLogBatNo As Field(Of Integer) = new Field(Of Integer)("ParentFlowLogBatNo", true)
        Private FI_FlowCaseStatus As Field(Of String) = new Field(Of String)("FlowCaseStatus", true)
        Private m_FieldNames As String() = { "FlowID", "FlowCaseID", "ParentFlowID", "ParentFlowCaseID", "ParentFlowStepID", "ParentFlowLogBatNo", "FlowCaseStatus" }
        Private m_PrimaryFields As String() = { "FlowID", "FlowCaseID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "FlowID"
                    Return FI_FlowID.Value
                Case "FlowCaseID"
                    Return FI_FlowCaseID.Value
                Case "ParentFlowID"
                    Return FI_ParentFlowID.Value
                Case "ParentFlowCaseID"
                    Return FI_ParentFlowCaseID.Value
                Case "ParentFlowStepID"
                    Return FI_ParentFlowStepID.Value
                Case "ParentFlowLogBatNo"
                    Return FI_ParentFlowLogBatNo.Value
                Case "FlowCaseStatus"
                    Return FI_FlowCaseStatus.Value
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
                Case "ParentFlowID"
                    FI_ParentFlowID.SetValue(value)
                Case "ParentFlowCaseID"
                    FI_ParentFlowCaseID.SetValue(value)
                Case "ParentFlowStepID"
                    FI_ParentFlowStepID.SetValue(value)
                Case "ParentFlowLogBatNo"
                    FI_ParentFlowLogBatNo.SetValue(value)
                Case "FlowCaseStatus"
                    FI_FlowCaseStatus.SetValue(value)
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
                Case "ParentFlowID"
                    return FI_ParentFlowID.Updated
                Case "ParentFlowCaseID"
                    return FI_ParentFlowCaseID.Updated
                Case "ParentFlowStepID"
                    return FI_ParentFlowStepID.Updated
                Case "ParentFlowLogBatNo"
                    return FI_ParentFlowLogBatNo.Updated
                Case "FlowCaseStatus"
                    return FI_FlowCaseStatus.Updated
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
                Case "ParentFlowID"
                    return FI_ParentFlowID.CreateUpdateSQL
                Case "ParentFlowCaseID"
                    return FI_ParentFlowCaseID.CreateUpdateSQL
                Case "ParentFlowStepID"
                    return FI_ParentFlowStepID.CreateUpdateSQL
                Case "ParentFlowLogBatNo"
                    return FI_ParentFlowLogBatNo.CreateUpdateSQL
                Case "FlowCaseStatus"
                    return FI_FlowCaseStatus.CreateUpdateSQL
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
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_FlowID.SetInitValue(dr("FlowID"))
            FI_FlowCaseID.SetInitValue(dr("FlowCaseID"))
            FI_ParentFlowID.SetInitValue(dr("ParentFlowID"))
            FI_ParentFlowCaseID.SetInitValue(dr("ParentFlowCaseID"))
            FI_ParentFlowStepID.SetInitValue(dr("ParentFlowStepID"))
            FI_ParentFlowLogBatNo.SetInitValue(dr("ParentFlowLogBatNo"))
            FI_FlowCaseStatus.SetInitValue(dr("FlowCaseStatus"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_FlowID.Updated = False
            FI_FlowCaseID.Updated = False
            FI_ParentFlowID.Updated = False
            FI_ParentFlowCaseID.Updated = False
            FI_ParentFlowStepID.Updated = False
            FI_ParentFlowLogBatNo.Updated = False
            FI_FlowCaseStatus.Updated = False
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

        Public ReadOnly Property ParentFlowID As Field(Of String) 
            Get
                Return FI_ParentFlowID
            End Get
        End Property

        Public ReadOnly Property ParentFlowCaseID As Field(Of String) 
            Get
                Return FI_ParentFlowCaseID
            End Get
        End Property

        Public ReadOnly Property ParentFlowStepID As Field(Of String) 
            Get
                Return FI_ParentFlowStepID
            End Get
        End Property

        Public ReadOnly Property ParentFlowLogBatNo As Field(Of Integer) 
            Get
                Return FI_ParentFlowLogBatNo
            End Get
        End Property

        Public ReadOnly Property FlowCaseStatus As Field(Of String) 
            Get
                Return FI_FlowCaseStatus
            End Get
        End Property

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal WF_FlowRelationRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From WF_FlowRelation")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowRelationRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowRelationRow.FlowCaseID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal WF_FlowRelationRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From WF_FlowRelation")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowRelationRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowRelationRow.FlowCaseID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal WF_FlowRelationRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From WF_FlowRelation")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In WF_FlowRelationRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                        db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, r.FlowCaseID.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal WF_FlowRelationRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From WF_FlowRelation")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In WF_FlowRelationRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, r.FlowCaseID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal WF_FlowRelationRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WF_FlowRelation")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowRelationRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowRelationRow.FlowCaseID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(WF_FlowRelationRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WF_FlowRelation")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowRelationRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowRelationRow.FlowCaseID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal WF_FlowRelationRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update WF_FlowRelation Set")
            For i As Integer = 0 To WF_FlowRelationRow.FieldNames.Length - 1
                If Not WF_FlowRelationRow.IsIdentityField(WF_FlowRelationRow.FieldNames(i)) AndAlso WF_FlowRelationRow.IsUpdated(WF_FlowRelationRow.FieldNames(i)) AndAlso WF_FlowRelationRow.CreateUpdateSQL(WF_FlowRelationRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, WF_FlowRelationRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where FlowID = @PKFlowID")
            strSQL.AppendLine("And FlowCaseID = @PKFlowCaseID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If WF_FlowRelationRow.FlowID.Updated Then db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowRelationRow.FlowID.Value)
            If WF_FlowRelationRow.FlowCaseID.Updated Then db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowRelationRow.FlowCaseID.Value)
            If WF_FlowRelationRow.ParentFlowID.Updated Then db.AddInParameter(dbcmd, "@ParentFlowID", DbType.String, WF_FlowRelationRow.ParentFlowID.Value)
            If WF_FlowRelationRow.ParentFlowCaseID.Updated Then db.AddInParameter(dbcmd, "@ParentFlowCaseID", DbType.String, WF_FlowRelationRow.ParentFlowCaseID.Value)
            If WF_FlowRelationRow.ParentFlowStepID.Updated Then db.AddInParameter(dbcmd, "@ParentFlowStepID", DbType.String, WF_FlowRelationRow.ParentFlowStepID.Value)
            If WF_FlowRelationRow.ParentFlowLogBatNo.Updated Then db.AddInParameter(dbcmd, "@ParentFlowLogBatNo", DbType.Int32, WF_FlowRelationRow.ParentFlowLogBatNo.Value)
            If WF_FlowRelationRow.FlowCaseStatus.Updated Then db.AddInParameter(dbcmd, "@FlowCaseStatus", DbType.String, WF_FlowRelationRow.FlowCaseStatus.Value)
            db.AddInParameter(dbcmd, "@PKFlowID", DbType.String, IIf(WF_FlowRelationRow.LoadFromDataRow, WF_FlowRelationRow.FlowID.OldValue, WF_FlowRelationRow.FlowID.Value))
            db.AddInParameter(dbcmd, "@PKFlowCaseID", DbType.String, IIf(WF_FlowRelationRow.LoadFromDataRow, WF_FlowRelationRow.FlowCaseID.OldValue, WF_FlowRelationRow.FlowCaseID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal WF_FlowRelationRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update WF_FlowRelation Set")
            For i As Integer = 0 To WF_FlowRelationRow.FieldNames.Length - 1
                If Not WF_FlowRelationRow.IsIdentityField(WF_FlowRelationRow.FieldNames(i)) AndAlso WF_FlowRelationRow.IsUpdated(WF_FlowRelationRow.FieldNames(i)) AndAlso WF_FlowRelationRow.CreateUpdateSQL(WF_FlowRelationRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, WF_FlowRelationRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where FlowID = @PKFlowID")
            strSQL.AppendLine("And FlowCaseID = @PKFlowCaseID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If WF_FlowRelationRow.FlowID.Updated Then db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowRelationRow.FlowID.Value)
            If WF_FlowRelationRow.FlowCaseID.Updated Then db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowRelationRow.FlowCaseID.Value)
            If WF_FlowRelationRow.ParentFlowID.Updated Then db.AddInParameter(dbcmd, "@ParentFlowID", DbType.String, WF_FlowRelationRow.ParentFlowID.Value)
            If WF_FlowRelationRow.ParentFlowCaseID.Updated Then db.AddInParameter(dbcmd, "@ParentFlowCaseID", DbType.String, WF_FlowRelationRow.ParentFlowCaseID.Value)
            If WF_FlowRelationRow.ParentFlowStepID.Updated Then db.AddInParameter(dbcmd, "@ParentFlowStepID", DbType.String, WF_FlowRelationRow.ParentFlowStepID.Value)
            If WF_FlowRelationRow.ParentFlowLogBatNo.Updated Then db.AddInParameter(dbcmd, "@ParentFlowLogBatNo", DbType.Int32, WF_FlowRelationRow.ParentFlowLogBatNo.Value)
            If WF_FlowRelationRow.FlowCaseStatus.Updated Then db.AddInParameter(dbcmd, "@FlowCaseStatus", DbType.String, WF_FlowRelationRow.FlowCaseStatus.Value)
            db.AddInParameter(dbcmd, "@PKFlowID", DbType.String, IIf(WF_FlowRelationRow.LoadFromDataRow, WF_FlowRelationRow.FlowID.OldValue, WF_FlowRelationRow.FlowID.Value))
            db.AddInParameter(dbcmd, "@PKFlowCaseID", DbType.String, IIf(WF_FlowRelationRow.LoadFromDataRow, WF_FlowRelationRow.FlowCaseID.OldValue, WF_FlowRelationRow.FlowCaseID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal WF_FlowRelationRow As Row()) As Integer
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
                    For Each r As Row In WF_FlowRelationRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update WF_FlowRelation Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where FlowID = @PKFlowID")
                        strSQL.AppendLine("And FlowCaseID = @PKFlowCaseID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.FlowID.Updated Then db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                        If r.FlowCaseID.Updated Then db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, r.FlowCaseID.Value)
                        If r.ParentFlowID.Updated Then db.AddInParameter(dbcmd, "@ParentFlowID", DbType.String, r.ParentFlowID.Value)
                        If r.ParentFlowCaseID.Updated Then db.AddInParameter(dbcmd, "@ParentFlowCaseID", DbType.String, r.ParentFlowCaseID.Value)
                        If r.ParentFlowStepID.Updated Then db.AddInParameter(dbcmd, "@ParentFlowStepID", DbType.String, r.ParentFlowStepID.Value)
                        If r.ParentFlowLogBatNo.Updated Then db.AddInParameter(dbcmd, "@ParentFlowLogBatNo", DbType.Int32, r.ParentFlowLogBatNo.Value)
                        If r.FlowCaseStatus.Updated Then db.AddInParameter(dbcmd, "@FlowCaseStatus", DbType.String, r.FlowCaseStatus.Value)
                        db.AddInParameter(dbcmd, "@PKFlowID", DbType.String, IIf(r.LoadFromDataRow, r.FlowID.OldValue, r.FlowID.Value))
                        db.AddInParameter(dbcmd, "@PKFlowCaseID", DbType.String, IIf(r.LoadFromDataRow, r.FlowCaseID.OldValue, r.FlowCaseID.Value))

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

        Public Function Update(ByVal WF_FlowRelationRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In WF_FlowRelationRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update WF_FlowRelation Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where FlowID = @PKFlowID")
                strSQL.AppendLine("And FlowCaseID = @PKFlowCaseID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.FlowID.Updated Then db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                If r.FlowCaseID.Updated Then db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, r.FlowCaseID.Value)
                If r.ParentFlowID.Updated Then db.AddInParameter(dbcmd, "@ParentFlowID", DbType.String, r.ParentFlowID.Value)
                If r.ParentFlowCaseID.Updated Then db.AddInParameter(dbcmd, "@ParentFlowCaseID", DbType.String, r.ParentFlowCaseID.Value)
                If r.ParentFlowStepID.Updated Then db.AddInParameter(dbcmd, "@ParentFlowStepID", DbType.String, r.ParentFlowStepID.Value)
                If r.ParentFlowLogBatNo.Updated Then db.AddInParameter(dbcmd, "@ParentFlowLogBatNo", DbType.Int32, r.ParentFlowLogBatNo.Value)
                If r.FlowCaseStatus.Updated Then db.AddInParameter(dbcmd, "@FlowCaseStatus", DbType.String, r.FlowCaseStatus.Value)
                db.AddInParameter(dbcmd, "@PKFlowID", DbType.String, IIf(r.LoadFromDataRow, r.FlowID.OldValue, r.FlowID.Value))
                db.AddInParameter(dbcmd, "@PKFlowCaseID", DbType.String, IIf(r.LoadFromDataRow, r.FlowCaseID.OldValue, r.FlowCaseID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal WF_FlowRelationRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From WF_FlowRelation")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowRelationRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowRelationRow.FlowCaseID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal WF_FlowRelationRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From WF_FlowRelation")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowCaseID = @FlowCaseID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowRelationRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowRelationRow.FlowCaseID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WF_FlowRelation")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal WF_FlowRelationRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into WF_FlowRelation")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowID, FlowCaseID, ParentFlowID, ParentFlowCaseID, ParentFlowStepID, ParentFlowLogBatNo,")
            strSQL.AppendLine("    FlowCaseStatus")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowID, @FlowCaseID, @ParentFlowID, @ParentFlowCaseID, @ParentFlowStepID, @ParentFlowLogBatNo,")
            strSQL.AppendLine("    @FlowCaseStatus")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowRelationRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowRelationRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@ParentFlowID", DbType.String, WF_FlowRelationRow.ParentFlowID.Value)
            db.AddInParameter(dbcmd, "@ParentFlowCaseID", DbType.String, WF_FlowRelationRow.ParentFlowCaseID.Value)
            db.AddInParameter(dbcmd, "@ParentFlowStepID", DbType.String, WF_FlowRelationRow.ParentFlowStepID.Value)
            db.AddInParameter(dbcmd, "@ParentFlowLogBatNo", DbType.Int32, WF_FlowRelationRow.ParentFlowLogBatNo.Value)
            db.AddInParameter(dbcmd, "@FlowCaseStatus", DbType.String, WF_FlowRelationRow.FlowCaseStatus.Value)

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal WF_FlowRelationRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into WF_FlowRelation")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowID, FlowCaseID, ParentFlowID, ParentFlowCaseID, ParentFlowStepID, ParentFlowLogBatNo,")
            strSQL.AppendLine("    FlowCaseStatus")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowID, @FlowCaseID, @ParentFlowID, @ParentFlowCaseID, @ParentFlowStepID, @ParentFlowLogBatNo,")
            strSQL.AppendLine("    @FlowCaseStatus")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowRelationRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, WF_FlowRelationRow.FlowCaseID.Value)
            db.AddInParameter(dbcmd, "@ParentFlowID", DbType.String, WF_FlowRelationRow.ParentFlowID.Value)
            db.AddInParameter(dbcmd, "@ParentFlowCaseID", DbType.String, WF_FlowRelationRow.ParentFlowCaseID.Value)
            db.AddInParameter(dbcmd, "@ParentFlowStepID", DbType.String, WF_FlowRelationRow.ParentFlowStepID.Value)
            db.AddInParameter(dbcmd, "@ParentFlowLogBatNo", DbType.Int32, WF_FlowRelationRow.ParentFlowLogBatNo.Value)
            db.AddInParameter(dbcmd, "@FlowCaseStatus", DbType.String, WF_FlowRelationRow.FlowCaseStatus.Value)

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal WF_FlowRelationRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into WF_FlowRelation")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowID, FlowCaseID, ParentFlowID, ParentFlowCaseID, ParentFlowStepID, ParentFlowLogBatNo,")
            strSQL.AppendLine("    FlowCaseStatus")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowID, @FlowCaseID, @ParentFlowID, @ParentFlowCaseID, @ParentFlowStepID, @ParentFlowLogBatNo,")
            strSQL.AppendLine("    @FlowCaseStatus")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In WF_FlowRelationRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                        db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, r.FlowCaseID.Value)
                        db.AddInParameter(dbcmd, "@ParentFlowID", DbType.String, r.ParentFlowID.Value)
                        db.AddInParameter(dbcmd, "@ParentFlowCaseID", DbType.String, r.ParentFlowCaseID.Value)
                        db.AddInParameter(dbcmd, "@ParentFlowStepID", DbType.String, r.ParentFlowStepID.Value)
                        db.AddInParameter(dbcmd, "@ParentFlowLogBatNo", DbType.Int32, r.ParentFlowLogBatNo.Value)
                        db.AddInParameter(dbcmd, "@FlowCaseStatus", DbType.String, r.FlowCaseStatus.Value)

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

        Public Function Insert(ByVal WF_FlowRelationRow As Row(), ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into WF_FlowRelation")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowID, FlowCaseID, ParentFlowID, ParentFlowCaseID, ParentFlowStepID, ParentFlowLogBatNo,")
            strSQL.AppendLine("    FlowCaseStatus")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowID, @FlowCaseID, @ParentFlowID, @ParentFlowCaseID, @ParentFlowStepID, @ParentFlowLogBatNo,")
            strSQL.AppendLine("    @FlowCaseStatus")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In WF_FlowRelationRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                db.AddInParameter(dbcmd, "@FlowCaseID", DbType.String, r.FlowCaseID.Value)
                db.AddInParameter(dbcmd, "@ParentFlowID", DbType.String, r.ParentFlowID.Value)
                db.AddInParameter(dbcmd, "@ParentFlowCaseID", DbType.String, r.ParentFlowCaseID.Value)
                db.AddInParameter(dbcmd, "@ParentFlowStepID", DbType.String, r.ParentFlowStepID.Value)
                db.AddInParameter(dbcmd, "@ParentFlowLogBatNo", DbType.Int32, r.ParentFlowLogBatNo.Value)
                db.AddInParameter(dbcmd, "@FlowCaseStatus", DbType.String, r.FlowCaseStatus.Value)

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

