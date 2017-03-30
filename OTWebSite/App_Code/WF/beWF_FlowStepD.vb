'****************************************************************
' Table:WF_FlowStepD
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

Namespace beWF_FlowStepD
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "FlowID", "FlowVer", "FlowStepID", "SeqNo", "ButtonName", "RequireOpinion", "NextStepID", "SendMail", "DefaultUserGroup", "CloseFlag" _
                                    , "AgreeFlag", "DefaultUserGroupEx", "AfterSQL" }
        Private m_Types As System.Type() = { GetType(String), GetType(Integer), GetType(String), GetType(Integer), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String) }
        Private m_PrimaryFields As String() = { "FlowID", "FlowVer", "FlowStepID", "SeqNo" }

        Public ReadOnly Property Rows() As beWF_FlowStepD.Rows 
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
        Public Sub Transfer2Row(WF_FlowStepDTable As DataTable)
            For Each dr As DataRow In WF_FlowStepDTable.Rows
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
                dr(m_Rows(i).FlowVer.FieldName) = m_Rows(i).FlowVer.Value
                dr(m_Rows(i).FlowStepID.FieldName) = m_Rows(i).FlowStepID.Value
                dr(m_Rows(i).SeqNo.FieldName) = m_Rows(i).SeqNo.Value
                dr(m_Rows(i).ButtonName.FieldName) = m_Rows(i).ButtonName.Value
                dr(m_Rows(i).RequireOpinion.FieldName) = m_Rows(i).RequireOpinion.Value
                dr(m_Rows(i).NextStepID.FieldName) = m_Rows(i).NextStepID.Value
                dr(m_Rows(i).SendMail.FieldName) = m_Rows(i).SendMail.Value
                dr(m_Rows(i).DefaultUserGroup.FieldName) = m_Rows(i).DefaultUserGroup.Value
                dr(m_Rows(i).CloseFlag.FieldName) = m_Rows(i).CloseFlag.Value
                dr(m_Rows(i).AgreeFlag.FieldName) = m_Rows(i).AgreeFlag.Value
                dr(m_Rows(i).DefaultUserGroupEx.FieldName) = m_Rows(i).DefaultUserGroupEx.Value
                dr(m_Rows(i).AfterSQL.FieldName) = m_Rows(i).AfterSQL.Value

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

        Public Sub Add(WF_FlowStepDRow As Row)
            m_Rows.Add(WF_FlowStepDRow)
        End Sub

        Public Sub Remove(WF_FlowStepDRow As Row)
            If m_Rows.IndexOf(WF_FlowStepDRow) >= 0 Then
                m_Rows.Remove(WF_FlowStepDRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_FlowID As Field(Of String) = new Field(Of String)("FlowID", true)
        Private FI_FlowVer As Field(Of Integer) = new Field(Of Integer)("FlowVer", true)
        Private FI_FlowStepID As Field(Of String) = new Field(Of String)("FlowStepID", true)
        Private FI_SeqNo As Field(Of Integer) = new Field(Of Integer)("SeqNo", true)
        Private FI_ButtonName As Field(Of String) = new Field(Of String)("ButtonName", true)
        Private FI_RequireOpinion As Field(Of String) = new Field(Of String)("RequireOpinion", true)
        Private FI_NextStepID As Field(Of String) = new Field(Of String)("NextStepID", true)
        Private FI_SendMail As Field(Of String) = new Field(Of String)("SendMail", true)
        Private FI_DefaultUserGroup As Field(Of String) = new Field(Of String)("DefaultUserGroup", true)
        Private FI_CloseFlag As Field(Of String) = new Field(Of String)("CloseFlag", true)
        Private FI_AgreeFlag As Field(Of String) = new Field(Of String)("AgreeFlag", true)
        Private FI_DefaultUserGroupEx As Field(Of String) = new Field(Of String)("DefaultUserGroupEx", true)
        Private FI_AfterSQL As Field(Of String) = new Field(Of String)("AfterSQL", true)
        Private m_FieldNames As String() = { "FlowID", "FlowVer", "FlowStepID", "SeqNo", "ButtonName", "RequireOpinion", "NextStepID", "SendMail", "DefaultUserGroup", "CloseFlag" _
                                    , "AgreeFlag", "DefaultUserGroupEx", "AfterSQL" }
        Private m_PrimaryFields As String() = { "FlowID", "FlowVer", "FlowStepID", "SeqNo" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "FlowID"
                    Return FI_FlowID.Value
                Case "FlowVer"
                    Return FI_FlowVer.Value
                Case "FlowStepID"
                    Return FI_FlowStepID.Value
                Case "SeqNo"
                    Return FI_SeqNo.Value
                Case "ButtonName"
                    Return FI_ButtonName.Value
                Case "RequireOpinion"
                    Return FI_RequireOpinion.Value
                Case "NextStepID"
                    Return FI_NextStepID.Value
                Case "SendMail"
                    Return FI_SendMail.Value
                Case "DefaultUserGroup"
                    Return FI_DefaultUserGroup.Value
                Case "CloseFlag"
                    Return FI_CloseFlag.Value
                Case "AgreeFlag"
                    Return FI_AgreeFlag.Value
                Case "DefaultUserGroupEx"
                    Return FI_DefaultUserGroupEx.Value
                Case "AfterSQL"
                    Return FI_AfterSQL.Value
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Sub SetFieldValue(ByVal fieldName As String, ByVal value As Object)
            Select Case fieldName
                Case "FlowID"
                    FI_FlowID.SetValue(value)
                Case "FlowVer"
                    FI_FlowVer.SetValue(value)
                Case "FlowStepID"
                    FI_FlowStepID.SetValue(value)
                Case "SeqNo"
                    FI_SeqNo.SetValue(value)
                Case "ButtonName"
                    FI_ButtonName.SetValue(value)
                Case "RequireOpinion"
                    FI_RequireOpinion.SetValue(value)
                Case "NextStepID"
                    FI_NextStepID.SetValue(value)
                Case "SendMail"
                    FI_SendMail.SetValue(value)
                Case "DefaultUserGroup"
                    FI_DefaultUserGroup.SetValue(value)
                Case "CloseFlag"
                    FI_CloseFlag.SetValue(value)
                Case "AgreeFlag"
                    FI_AgreeFlag.SetValue(value)
                Case "DefaultUserGroupEx"
                    FI_DefaultUserGroupEx.SetValue(value)
                Case "AfterSQL"
                    FI_AfterSQL.SetValue(value)
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
                Case "FlowVer"
                    return FI_FlowVer.Updated
                Case "FlowStepID"
                    return FI_FlowStepID.Updated
                Case "SeqNo"
                    return FI_SeqNo.Updated
                Case "ButtonName"
                    return FI_ButtonName.Updated
                Case "RequireOpinion"
                    return FI_RequireOpinion.Updated
                Case "NextStepID"
                    return FI_NextStepID.Updated
                Case "SendMail"
                    return FI_SendMail.Updated
                Case "DefaultUserGroup"
                    return FI_DefaultUserGroup.Updated
                Case "CloseFlag"
                    return FI_CloseFlag.Updated
                Case "AgreeFlag"
                    return FI_AgreeFlag.Updated
                Case "DefaultUserGroupEx"
                    return FI_DefaultUserGroupEx.Updated
                Case "AfterSQL"
                    return FI_AfterSQL.Updated
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public Function CreateUpdateSQL(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "FlowID"
                    return FI_FlowID.CreateUpdateSQL
                Case "FlowVer"
                    return FI_FlowVer.CreateUpdateSQL
                Case "FlowStepID"
                    return FI_FlowStepID.CreateUpdateSQL
                Case "SeqNo"
                    return FI_SeqNo.CreateUpdateSQL
                Case "ButtonName"
                    return FI_ButtonName.CreateUpdateSQL
                Case "RequireOpinion"
                    return FI_RequireOpinion.CreateUpdateSQL
                Case "NextStepID"
                    return FI_NextStepID.CreateUpdateSQL
                Case "SendMail"
                    return FI_SendMail.CreateUpdateSQL
                Case "DefaultUserGroup"
                    return FI_DefaultUserGroup.CreateUpdateSQL
                Case "CloseFlag"
                    return FI_CloseFlag.CreateUpdateSQL
                Case "AgreeFlag"
                    return FI_AgreeFlag.CreateUpdateSQL
                Case "DefaultUserGroupEx"
                    return FI_DefaultUserGroupEx.CreateUpdateSQL
                Case "AfterSQL"
                    return FI_AfterSQL.CreateUpdateSQL
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
            FI_FlowVer.SetInitValue(0)
            FI_FlowStepID.SetInitValue("")
            FI_SeqNo.SetInitValue(0)
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_FlowID.SetInitValue(dr("FlowID"))
            FI_FlowVer.SetInitValue(dr("FlowVer"))
            FI_FlowStepID.SetInitValue(dr("FlowStepID"))
            FI_SeqNo.SetInitValue(dr("SeqNo"))
            FI_ButtonName.SetInitValue(dr("ButtonName"))
            FI_RequireOpinion.SetInitValue(dr("RequireOpinion"))
            FI_NextStepID.SetInitValue(dr("NextStepID"))
            FI_SendMail.SetInitValue(dr("SendMail"))
            FI_DefaultUserGroup.SetInitValue(dr("DefaultUserGroup"))
            FI_CloseFlag.SetInitValue(dr("CloseFlag"))
            FI_AgreeFlag.SetInitValue(dr("AgreeFlag"))
            FI_DefaultUserGroupEx.SetInitValue(dr("DefaultUserGroupEx"))
            FI_AfterSQL.SetInitValue(dr("AfterSQL"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_FlowID.Updated = False
            FI_FlowVer.Updated = False
            FI_FlowStepID.Updated = False
            FI_SeqNo.Updated = False
            FI_ButtonName.Updated = False
            FI_RequireOpinion.Updated = False
            FI_NextStepID.Updated = False
            FI_SendMail.Updated = False
            FI_DefaultUserGroup.Updated = False
            FI_CloseFlag.Updated = False
            FI_AgreeFlag.Updated = False
            FI_DefaultUserGroupEx.Updated = False
            FI_AfterSQL.Updated = False
        End Sub

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

        Public ReadOnly Property FlowStepID As Field(Of String) 
            Get
                Return FI_FlowStepID
            End Get
        End Property

        Public ReadOnly Property SeqNo As Field(Of Integer) 
            Get
                Return FI_SeqNo
            End Get
        End Property

        Public ReadOnly Property ButtonName As Field(Of String) 
            Get
                Return FI_ButtonName
            End Get
        End Property

        Public ReadOnly Property RequireOpinion As Field(Of String) 
            Get
                Return FI_RequireOpinion
            End Get
        End Property

        Public ReadOnly Property NextStepID As Field(Of String) 
            Get
                Return FI_NextStepID
            End Get
        End Property

        Public ReadOnly Property SendMail As Field(Of String) 
            Get
                Return FI_SendMail
            End Get
        End Property

        Public ReadOnly Property DefaultUserGroup As Field(Of String) 
            Get
                Return FI_DefaultUserGroup
            End Get
        End Property

        Public ReadOnly Property CloseFlag As Field(Of String) 
            Get
                Return FI_CloseFlag
            End Get
        End Property

        Public ReadOnly Property AgreeFlag As Field(Of String) 
            Get
                Return FI_AgreeFlag
            End Get
        End Property

        Public ReadOnly Property DefaultUserGroupEx As Field(Of String) 
            Get
                Return FI_DefaultUserGroupEx
            End Get
        End Property

        Public ReadOnly Property AfterSQL As Field(Of String) 
            Get
                Return FI_AfterSQL
            End Get
        End Property

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal WF_FlowStepDRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From WF_FlowStepD")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowVer = @FlowVer")
            strSQL.AppendLine("And FlowStepID = @FlowStepID")
            strSQL.AppendLine("And SeqNo = @SeqNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowStepDRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowVer", DbType.Int32, WF_FlowStepDRow.FlowVer.Value)
            db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, WF_FlowStepDRow.FlowStepID.Value)
            db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, WF_FlowStepDRow.SeqNo.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal WF_FlowStepDRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From WF_FlowStepD")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowVer = @FlowVer")
            strSQL.AppendLine("And FlowStepID = @FlowStepID")
            strSQL.AppendLine("And SeqNo = @SeqNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowStepDRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowVer", DbType.Int32, WF_FlowStepDRow.FlowVer.Value)
            db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, WF_FlowStepDRow.FlowStepID.Value)
            db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, WF_FlowStepDRow.SeqNo.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal WF_FlowStepDRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From WF_FlowStepD")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowVer = @FlowVer")
            strSQL.AppendLine("And FlowStepID = @FlowStepID")
            strSQL.AppendLine("And SeqNo = @SeqNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In WF_FlowStepDRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                        db.AddInParameter(dbcmd, "@FlowVer", DbType.Int32, r.FlowVer.Value)
                        db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, r.FlowStepID.Value)
                        db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, r.SeqNo.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal WF_FlowStepDRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From WF_FlowStepD")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowVer = @FlowVer")
            strSQL.AppendLine("And FlowStepID = @FlowStepID")
            strSQL.AppendLine("And SeqNo = @SeqNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In WF_FlowStepDRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                db.AddInParameter(dbcmd, "@FlowVer", DbType.Int32, r.FlowVer.Value)
                db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, r.FlowStepID.Value)
                db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, r.SeqNo.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal WF_FlowStepDRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WF_FlowStepD")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowVer = @FlowVer")
            strSQL.AppendLine("And FlowStepID = @FlowStepID")
            strSQL.AppendLine("And SeqNo = @SeqNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowStepDRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowVer", DbType.Int32, WF_FlowStepDRow.FlowVer.Value)
            db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, WF_FlowStepDRow.FlowStepID.Value)
            db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, WF_FlowStepDRow.SeqNo.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(WF_FlowStepDRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WF_FlowStepD")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowVer = @FlowVer")
            strSQL.AppendLine("And FlowStepID = @FlowStepID")
            strSQL.AppendLine("And SeqNo = @SeqNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowStepDRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowVer", DbType.Int32, WF_FlowStepDRow.FlowVer.Value)
            db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, WF_FlowStepDRow.FlowStepID.Value)
            db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, WF_FlowStepDRow.SeqNo.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal WF_FlowStepDRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update WF_FlowStepD Set")
            For i As Integer = 0 To WF_FlowStepDRow.FieldNames.Length - 1
                If Not WF_FlowStepDRow.IsIdentityField(WF_FlowStepDRow.FieldNames(i)) AndAlso WF_FlowStepDRow.IsUpdated(WF_FlowStepDRow.FieldNames(i)) AndAlso WF_FlowStepDRow.CreateUpdateSQL(WF_FlowStepDRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, WF_FlowStepDRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where FlowID = @PKFlowID")
            strSQL.AppendLine("And FlowVer = @PKFlowVer")
            strSQL.AppendLine("And FlowStepID = @PKFlowStepID")
            strSQL.AppendLine("And SeqNo = @PKSeqNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If WF_FlowStepDRow.FlowID.Updated Then db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowStepDRow.FlowID.Value)
            If WF_FlowStepDRow.FlowVer.Updated Then db.AddInParameter(dbcmd, "@FlowVer", DbType.Int32, WF_FlowStepDRow.FlowVer.Value)
            If WF_FlowStepDRow.FlowStepID.Updated Then db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, WF_FlowStepDRow.FlowStepID.Value)
            If WF_FlowStepDRow.SeqNo.Updated Then db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, WF_FlowStepDRow.SeqNo.Value)
            If WF_FlowStepDRow.ButtonName.Updated Then db.AddInParameter(dbcmd, "@ButtonName", DbType.String, WF_FlowStepDRow.ButtonName.Value)
            If WF_FlowStepDRow.RequireOpinion.Updated Then db.AddInParameter(dbcmd, "@RequireOpinion", DbType.String, WF_FlowStepDRow.RequireOpinion.Value)
            If WF_FlowStepDRow.NextStepID.Updated Then db.AddInParameter(dbcmd, "@NextStepID", DbType.String, WF_FlowStepDRow.NextStepID.Value)
            If WF_FlowStepDRow.SendMail.Updated Then db.AddInParameter(dbcmd, "@SendMail", DbType.String, WF_FlowStepDRow.SendMail.Value)
            If WF_FlowStepDRow.DefaultUserGroup.Updated Then db.AddInParameter(dbcmd, "@DefaultUserGroup", DbType.String, WF_FlowStepDRow.DefaultUserGroup.Value)
            If WF_FlowStepDRow.CloseFlag.Updated Then db.AddInParameter(dbcmd, "@CloseFlag", DbType.String, WF_FlowStepDRow.CloseFlag.Value)
            If WF_FlowStepDRow.AgreeFlag.Updated Then db.AddInParameter(dbcmd, "@AgreeFlag", DbType.String, WF_FlowStepDRow.AgreeFlag.Value)
            If WF_FlowStepDRow.DefaultUserGroupEx.Updated Then db.AddInParameter(dbcmd, "@DefaultUserGroupEx", DbType.String, WF_FlowStepDRow.DefaultUserGroupEx.Value)
            If WF_FlowStepDRow.AfterSQL.Updated Then db.AddInParameter(dbcmd, "@AfterSQL", DbType.String, WF_FlowStepDRow.AfterSQL.Value)
            db.AddInParameter(dbcmd, "@PKFlowID", DbType.String, IIf(WF_FlowStepDRow.LoadFromDataRow, WF_FlowStepDRow.FlowID.OldValue, WF_FlowStepDRow.FlowID.Value))
            db.AddInParameter(dbcmd, "@PKFlowVer", DbType.Int32, IIf(WF_FlowStepDRow.LoadFromDataRow, WF_FlowStepDRow.FlowVer.OldValue, WF_FlowStepDRow.FlowVer.Value))
            db.AddInParameter(dbcmd, "@PKFlowStepID", DbType.String, IIf(WF_FlowStepDRow.LoadFromDataRow, WF_FlowStepDRow.FlowStepID.OldValue, WF_FlowStepDRow.FlowStepID.Value))
            db.AddInParameter(dbcmd, "@PKSeqNo", DbType.Int32, IIf(WF_FlowStepDRow.LoadFromDataRow, WF_FlowStepDRow.SeqNo.OldValue, WF_FlowStepDRow.SeqNo.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal WF_FlowStepDRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update WF_FlowStepD Set")
            For i As Integer = 0 To WF_FlowStepDRow.FieldNames.Length - 1
                If Not WF_FlowStepDRow.IsIdentityField(WF_FlowStepDRow.FieldNames(i)) AndAlso WF_FlowStepDRow.IsUpdated(WF_FlowStepDRow.FieldNames(i)) AndAlso WF_FlowStepDRow.CreateUpdateSQL(WF_FlowStepDRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, WF_FlowStepDRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where FlowID = @PKFlowID")
            strSQL.AppendLine("And FlowVer = @PKFlowVer")
            strSQL.AppendLine("And FlowStepID = @PKFlowStepID")
            strSQL.AppendLine("And SeqNo = @PKSeqNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If WF_FlowStepDRow.FlowID.Updated Then db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowStepDRow.FlowID.Value)
            If WF_FlowStepDRow.FlowVer.Updated Then db.AddInParameter(dbcmd, "@FlowVer", DbType.Int32, WF_FlowStepDRow.FlowVer.Value)
            If WF_FlowStepDRow.FlowStepID.Updated Then db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, WF_FlowStepDRow.FlowStepID.Value)
            If WF_FlowStepDRow.SeqNo.Updated Then db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, WF_FlowStepDRow.SeqNo.Value)
            If WF_FlowStepDRow.ButtonName.Updated Then db.AddInParameter(dbcmd, "@ButtonName", DbType.String, WF_FlowStepDRow.ButtonName.Value)
            If WF_FlowStepDRow.RequireOpinion.Updated Then db.AddInParameter(dbcmd, "@RequireOpinion", DbType.String, WF_FlowStepDRow.RequireOpinion.Value)
            If WF_FlowStepDRow.NextStepID.Updated Then db.AddInParameter(dbcmd, "@NextStepID", DbType.String, WF_FlowStepDRow.NextStepID.Value)
            If WF_FlowStepDRow.SendMail.Updated Then db.AddInParameter(dbcmd, "@SendMail", DbType.String, WF_FlowStepDRow.SendMail.Value)
            If WF_FlowStepDRow.DefaultUserGroup.Updated Then db.AddInParameter(dbcmd, "@DefaultUserGroup", DbType.String, WF_FlowStepDRow.DefaultUserGroup.Value)
            If WF_FlowStepDRow.CloseFlag.Updated Then db.AddInParameter(dbcmd, "@CloseFlag", DbType.String, WF_FlowStepDRow.CloseFlag.Value)
            If WF_FlowStepDRow.AgreeFlag.Updated Then db.AddInParameter(dbcmd, "@AgreeFlag", DbType.String, WF_FlowStepDRow.AgreeFlag.Value)
            If WF_FlowStepDRow.DefaultUserGroupEx.Updated Then db.AddInParameter(dbcmd, "@DefaultUserGroupEx", DbType.String, WF_FlowStepDRow.DefaultUserGroupEx.Value)
            If WF_FlowStepDRow.AfterSQL.Updated Then db.AddInParameter(dbcmd, "@AfterSQL", DbType.String, WF_FlowStepDRow.AfterSQL.Value)
            db.AddInParameter(dbcmd, "@PKFlowID", DbType.String, IIf(WF_FlowStepDRow.LoadFromDataRow, WF_FlowStepDRow.FlowID.OldValue, WF_FlowStepDRow.FlowID.Value))
            db.AddInParameter(dbcmd, "@PKFlowVer", DbType.Int32, IIf(WF_FlowStepDRow.LoadFromDataRow, WF_FlowStepDRow.FlowVer.OldValue, WF_FlowStepDRow.FlowVer.Value))
            db.AddInParameter(dbcmd, "@PKFlowStepID", DbType.String, IIf(WF_FlowStepDRow.LoadFromDataRow, WF_FlowStepDRow.FlowStepID.OldValue, WF_FlowStepDRow.FlowStepID.Value))
            db.AddInParameter(dbcmd, "@PKSeqNo", DbType.Int32, IIf(WF_FlowStepDRow.LoadFromDataRow, WF_FlowStepDRow.SeqNo.OldValue, WF_FlowStepDRow.SeqNo.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal WF_FlowStepDRow As Row()) As Integer
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
                    For Each r As Row In WF_FlowStepDRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update WF_FlowStepD Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where FlowID = @PKFlowID")
                        strSQL.AppendLine("And FlowVer = @PKFlowVer")
                        strSQL.AppendLine("And FlowStepID = @PKFlowStepID")
                        strSQL.AppendLine("And SeqNo = @PKSeqNo")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.FlowID.Updated Then db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                        If r.FlowVer.Updated Then db.AddInParameter(dbcmd, "@FlowVer", DbType.Int32, r.FlowVer.Value)
                        If r.FlowStepID.Updated Then db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, r.FlowStepID.Value)
                        If r.SeqNo.Updated Then db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, r.SeqNo.Value)
                        If r.ButtonName.Updated Then db.AddInParameter(dbcmd, "@ButtonName", DbType.String, r.ButtonName.Value)
                        If r.RequireOpinion.Updated Then db.AddInParameter(dbcmd, "@RequireOpinion", DbType.String, r.RequireOpinion.Value)
                        If r.NextStepID.Updated Then db.AddInParameter(dbcmd, "@NextStepID", DbType.String, r.NextStepID.Value)
                        If r.SendMail.Updated Then db.AddInParameter(dbcmd, "@SendMail", DbType.String, r.SendMail.Value)
                        If r.DefaultUserGroup.Updated Then db.AddInParameter(dbcmd, "@DefaultUserGroup", DbType.String, r.DefaultUserGroup.Value)
                        If r.CloseFlag.Updated Then db.AddInParameter(dbcmd, "@CloseFlag", DbType.String, r.CloseFlag.Value)
                        If r.AgreeFlag.Updated Then db.AddInParameter(dbcmd, "@AgreeFlag", DbType.String, r.AgreeFlag.Value)
                        If r.DefaultUserGroupEx.Updated Then db.AddInParameter(dbcmd, "@DefaultUserGroupEx", DbType.String, r.DefaultUserGroupEx.Value)
                        If r.AfterSQL.Updated Then db.AddInParameter(dbcmd, "@AfterSQL", DbType.String, r.AfterSQL.Value)
                        db.AddInParameter(dbcmd, "@PKFlowID", DbType.String, IIf(r.LoadFromDataRow, r.FlowID.OldValue, r.FlowID.Value))
                        db.AddInParameter(dbcmd, "@PKFlowVer", DbType.Int32, IIf(r.LoadFromDataRow, r.FlowVer.OldValue, r.FlowVer.Value))
                        db.AddInParameter(dbcmd, "@PKFlowStepID", DbType.String, IIf(r.LoadFromDataRow, r.FlowStepID.OldValue, r.FlowStepID.Value))
                        db.AddInParameter(dbcmd, "@PKSeqNo", DbType.Int32, IIf(r.LoadFromDataRow, r.SeqNo.OldValue, r.SeqNo.Value))

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

        Public Function Update(ByVal WF_FlowStepDRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In WF_FlowStepDRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update WF_FlowStepD Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where FlowID = @PKFlowID")
                strSQL.AppendLine("And FlowVer = @PKFlowVer")
                strSQL.AppendLine("And FlowStepID = @PKFlowStepID")
                strSQL.AppendLine("And SeqNo = @PKSeqNo")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.FlowID.Updated Then db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                If r.FlowVer.Updated Then db.AddInParameter(dbcmd, "@FlowVer", DbType.Int32, r.FlowVer.Value)
                If r.FlowStepID.Updated Then db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, r.FlowStepID.Value)
                If r.SeqNo.Updated Then db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, r.SeqNo.Value)
                If r.ButtonName.Updated Then db.AddInParameter(dbcmd, "@ButtonName", DbType.String, r.ButtonName.Value)
                If r.RequireOpinion.Updated Then db.AddInParameter(dbcmd, "@RequireOpinion", DbType.String, r.RequireOpinion.Value)
                If r.NextStepID.Updated Then db.AddInParameter(dbcmd, "@NextStepID", DbType.String, r.NextStepID.Value)
                If r.SendMail.Updated Then db.AddInParameter(dbcmd, "@SendMail", DbType.String, r.SendMail.Value)
                If r.DefaultUserGroup.Updated Then db.AddInParameter(dbcmd, "@DefaultUserGroup", DbType.String, r.DefaultUserGroup.Value)
                If r.CloseFlag.Updated Then db.AddInParameter(dbcmd, "@CloseFlag", DbType.String, r.CloseFlag.Value)
                If r.AgreeFlag.Updated Then db.AddInParameter(dbcmd, "@AgreeFlag", DbType.String, r.AgreeFlag.Value)
                If r.DefaultUserGroupEx.Updated Then db.AddInParameter(dbcmd, "@DefaultUserGroupEx", DbType.String, r.DefaultUserGroupEx.Value)
                If r.AfterSQL.Updated Then db.AddInParameter(dbcmd, "@AfterSQL", DbType.String, r.AfterSQL.Value)
                db.AddInParameter(dbcmd, "@PKFlowID", DbType.String, IIf(r.LoadFromDataRow, r.FlowID.OldValue, r.FlowID.Value))
                db.AddInParameter(dbcmd, "@PKFlowVer", DbType.Int32, IIf(r.LoadFromDataRow, r.FlowVer.OldValue, r.FlowVer.Value))
                db.AddInParameter(dbcmd, "@PKFlowStepID", DbType.String, IIf(r.LoadFromDataRow, r.FlowStepID.OldValue, r.FlowStepID.Value))
                db.AddInParameter(dbcmd, "@PKSeqNo", DbType.Int32, IIf(r.LoadFromDataRow, r.SeqNo.OldValue, r.SeqNo.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal WF_FlowStepDRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From WF_FlowStepD")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowVer = @FlowVer")
            strSQL.AppendLine("And FlowStepID = @FlowStepID")
            strSQL.AppendLine("And SeqNo = @SeqNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowStepDRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowVer", DbType.Int32, WF_FlowStepDRow.FlowVer.Value)
            db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, WF_FlowStepDRow.FlowStepID.Value)
            db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, WF_FlowStepDRow.SeqNo.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal WF_FlowStepDRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From WF_FlowStepD")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowVer = @FlowVer")
            strSQL.AppendLine("And FlowStepID = @FlowStepID")
            strSQL.AppendLine("And SeqNo = @SeqNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowStepDRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowVer", DbType.Int32, WF_FlowStepDRow.FlowVer.Value)
            db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, WF_FlowStepDRow.FlowStepID.Value)
            db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, WF_FlowStepDRow.SeqNo.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WF_FlowStepD")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal WF_FlowStepDRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into WF_FlowStepD")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowID, FlowVer, FlowStepID, SeqNo, ButtonName, RequireOpinion, NextStepID, SendMail,")
            strSQL.AppendLine("    DefaultUserGroup, CloseFlag, AgreeFlag, DefaultUserGroupEx, AfterSQL")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowID, @FlowVer, @FlowStepID, @SeqNo, @ButtonName, @RequireOpinion, @NextStepID, @SendMail,")
            strSQL.AppendLine("    @DefaultUserGroup, @CloseFlag, @AgreeFlag, @DefaultUserGroupEx, @AfterSQL")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowStepDRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowVer", DbType.Int32, WF_FlowStepDRow.FlowVer.Value)
            db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, WF_FlowStepDRow.FlowStepID.Value)
            db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, WF_FlowStepDRow.SeqNo.Value)
            db.AddInParameter(dbcmd, "@ButtonName", DbType.String, WF_FlowStepDRow.ButtonName.Value)
            db.AddInParameter(dbcmd, "@RequireOpinion", DbType.String, WF_FlowStepDRow.RequireOpinion.Value)
            db.AddInParameter(dbcmd, "@NextStepID", DbType.String, WF_FlowStepDRow.NextStepID.Value)
            db.AddInParameter(dbcmd, "@SendMail", DbType.String, WF_FlowStepDRow.SendMail.Value)
            db.AddInParameter(dbcmd, "@DefaultUserGroup", DbType.String, WF_FlowStepDRow.DefaultUserGroup.Value)
            db.AddInParameter(dbcmd, "@CloseFlag", DbType.String, WF_FlowStepDRow.CloseFlag.Value)
            db.AddInParameter(dbcmd, "@AgreeFlag", DbType.String, WF_FlowStepDRow.AgreeFlag.Value)
            db.AddInParameter(dbcmd, "@DefaultUserGroupEx", DbType.String, WF_FlowStepDRow.DefaultUserGroupEx.Value)
            db.AddInParameter(dbcmd, "@AfterSQL", DbType.String, WF_FlowStepDRow.AfterSQL.Value)

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal WF_FlowStepDRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into WF_FlowStepD")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowID, FlowVer, FlowStepID, SeqNo, ButtonName, RequireOpinion, NextStepID, SendMail,")
            strSQL.AppendLine("    DefaultUserGroup, CloseFlag, AgreeFlag, DefaultUserGroupEx, AfterSQL")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowID, @FlowVer, @FlowStepID, @SeqNo, @ButtonName, @RequireOpinion, @NextStepID, @SendMail,")
            strSQL.AppendLine("    @DefaultUserGroup, @CloseFlag, @AgreeFlag, @DefaultUserGroupEx, @AfterSQL")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowStepDRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowVer", DbType.Int32, WF_FlowStepDRow.FlowVer.Value)
            db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, WF_FlowStepDRow.FlowStepID.Value)
            db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, WF_FlowStepDRow.SeqNo.Value)
            db.AddInParameter(dbcmd, "@ButtonName", DbType.String, WF_FlowStepDRow.ButtonName.Value)
            db.AddInParameter(dbcmd, "@RequireOpinion", DbType.String, WF_FlowStepDRow.RequireOpinion.Value)
            db.AddInParameter(dbcmd, "@NextStepID", DbType.String, WF_FlowStepDRow.NextStepID.Value)
            db.AddInParameter(dbcmd, "@SendMail", DbType.String, WF_FlowStepDRow.SendMail.Value)
            db.AddInParameter(dbcmd, "@DefaultUserGroup", DbType.String, WF_FlowStepDRow.DefaultUserGroup.Value)
            db.AddInParameter(dbcmd, "@CloseFlag", DbType.String, WF_FlowStepDRow.CloseFlag.Value)
            db.AddInParameter(dbcmd, "@AgreeFlag", DbType.String, WF_FlowStepDRow.AgreeFlag.Value)
            db.AddInParameter(dbcmd, "@DefaultUserGroupEx", DbType.String, WF_FlowStepDRow.DefaultUserGroupEx.Value)
            db.AddInParameter(dbcmd, "@AfterSQL", DbType.String, WF_FlowStepDRow.AfterSQL.Value)

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal WF_FlowStepDRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into WF_FlowStepD")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowID, FlowVer, FlowStepID, SeqNo, ButtonName, RequireOpinion, NextStepID, SendMail,")
            strSQL.AppendLine("    DefaultUserGroup, CloseFlag, AgreeFlag, DefaultUserGroupEx, AfterSQL")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowID, @FlowVer, @FlowStepID, @SeqNo, @ButtonName, @RequireOpinion, @NextStepID, @SendMail,")
            strSQL.AppendLine("    @DefaultUserGroup, @CloseFlag, @AgreeFlag, @DefaultUserGroupEx, @AfterSQL")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In WF_FlowStepDRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                        db.AddInParameter(dbcmd, "@FlowVer", DbType.Int32, r.FlowVer.Value)
                        db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, r.FlowStepID.Value)
                        db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, r.SeqNo.Value)
                        db.AddInParameter(dbcmd, "@ButtonName", DbType.String, r.ButtonName.Value)
                        db.AddInParameter(dbcmd, "@RequireOpinion", DbType.String, r.RequireOpinion.Value)
                        db.AddInParameter(dbcmd, "@NextStepID", DbType.String, r.NextStepID.Value)
                        db.AddInParameter(dbcmd, "@SendMail", DbType.String, r.SendMail.Value)
                        db.AddInParameter(dbcmd, "@DefaultUserGroup", DbType.String, r.DefaultUserGroup.Value)
                        db.AddInParameter(dbcmd, "@CloseFlag", DbType.String, r.CloseFlag.Value)
                        db.AddInParameter(dbcmd, "@AgreeFlag", DbType.String, r.AgreeFlag.Value)
                        db.AddInParameter(dbcmd, "@DefaultUserGroupEx", DbType.String, r.DefaultUserGroupEx.Value)
                        db.AddInParameter(dbcmd, "@AfterSQL", DbType.String, r.AfterSQL.Value)

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

        Public Function Insert(ByVal WF_FlowStepDRow As Row(), ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into WF_FlowStepD")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowID, FlowVer, FlowStepID, SeqNo, ButtonName, RequireOpinion, NextStepID, SendMail,")
            strSQL.AppendLine("    DefaultUserGroup, CloseFlag, AgreeFlag, DefaultUserGroupEx, AfterSQL")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowID, @FlowVer, @FlowStepID, @SeqNo, @ButtonName, @RequireOpinion, @NextStepID, @SendMail,")
            strSQL.AppendLine("    @DefaultUserGroup, @CloseFlag, @AgreeFlag, @DefaultUserGroupEx, @AfterSQL")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In WF_FlowStepDRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                db.AddInParameter(dbcmd, "@FlowVer", DbType.Int32, r.FlowVer.Value)
                db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, r.FlowStepID.Value)
                db.AddInParameter(dbcmd, "@SeqNo", DbType.Int32, r.SeqNo.Value)
                db.AddInParameter(dbcmd, "@ButtonName", DbType.String, r.ButtonName.Value)
                db.AddInParameter(dbcmd, "@RequireOpinion", DbType.String, r.RequireOpinion.Value)
                db.AddInParameter(dbcmd, "@NextStepID", DbType.String, r.NextStepID.Value)
                db.AddInParameter(dbcmd, "@SendMail", DbType.String, r.SendMail.Value)
                db.AddInParameter(dbcmd, "@DefaultUserGroup", DbType.String, r.DefaultUserGroup.Value)
                db.AddInParameter(dbcmd, "@CloseFlag", DbType.String, r.CloseFlag.Value)
                db.AddInParameter(dbcmd, "@AgreeFlag", DbType.String, r.AgreeFlag.Value)
                db.AddInParameter(dbcmd, "@DefaultUserGroupEx", DbType.String, r.DefaultUserGroupEx.Value)
                db.AddInParameter(dbcmd, "@AfterSQL", DbType.String, r.AfterSQL.Value)

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

