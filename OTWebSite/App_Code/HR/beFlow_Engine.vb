'****************************************************************
' Table:Flow_Engine
' Created Date: 2016.12.09
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beFlow_Engine
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "FlowCode", "FlowName", "FlowSN", "FlowSeq", "FlowStartFlag", "FlowEndFlag", "InValidFlag", "FlowAct", "SignLine_Define", "SingID_Define" _
                                    , "SpeComp", "SpeEmpID", "SpeName", "LastChgCompID", "LastChgEmpID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "FlowCode", "FlowSN", "FlowSeq" }

        Public ReadOnly Property Rows() As beFlow_Engine.Rows 
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
        Public Sub Transfer2Row(Flow_EngineTable As DataTable)
            For Each dr As DataRow In Flow_EngineTable.Rows
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

                dr(m_Rows(i).FlowCode.FieldName) = m_Rows(i).FlowCode.Value
                dr(m_Rows(i).FlowName.FieldName) = m_Rows(i).FlowName.Value
                dr(m_Rows(i).FlowSN.FieldName) = m_Rows(i).FlowSN.Value
                dr(m_Rows(i).FlowSeq.FieldName) = m_Rows(i).FlowSeq.Value
                dr(m_Rows(i).FlowStartFlag.FieldName) = m_Rows(i).FlowStartFlag.Value
                dr(m_Rows(i).FlowEndFlag.FieldName) = m_Rows(i).FlowEndFlag.Value
                dr(m_Rows(i).InValidFlag.FieldName) = m_Rows(i).InValidFlag.Value
                dr(m_Rows(i).FlowAct.FieldName) = m_Rows(i).FlowAct.Value
                dr(m_Rows(i).SignLine_Define.FieldName) = m_Rows(i).SignLine_Define.Value
                dr(m_Rows(i).SingID_Define.FieldName) = m_Rows(i).SingID_Define.Value
                dr(m_Rows(i).SpeComp.FieldName) = m_Rows(i).SpeComp.Value
                dr(m_Rows(i).SpeEmpID.FieldName) = m_Rows(i).SpeEmpID.Value
                dr(m_Rows(i).SpeName.FieldName) = m_Rows(i).SpeName.Value
                dr(m_Rows(i).LastChgCompID.FieldName) = m_Rows(i).LastChgCompID.Value
                dr(m_Rows(i).LastChgEmpID.FieldName) = m_Rows(i).LastChgEmpID.Value
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

        Public Sub Add(Flow_EngineRow As Row)
            m_Rows.Add(Flow_EngineRow)
        End Sub

        Public Sub Remove(Flow_EngineRow As Row)
            If m_Rows.IndexOf(Flow_EngineRow) >= 0 Then
                m_Rows.Remove(Flow_EngineRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_FlowCode As Field(Of String) = new Field(Of String)("FlowCode", true)
        Private FI_FlowName As Field(Of String) = new Field(Of String)("FlowName", true)
        Private FI_FlowSN As Field(Of String) = new Field(Of String)("FlowSN", true)
        Private FI_FlowSeq As Field(Of String) = new Field(Of String)("FlowSeq", true)
        Private FI_FlowStartFlag As Field(Of String) = new Field(Of String)("FlowStartFlag", true)
        Private FI_FlowEndFlag As Field(Of String) = new Field(Of String)("FlowEndFlag", true)
        Private FI_InValidFlag As Field(Of String) = new Field(Of String)("InValidFlag", true)
        Private FI_FlowAct As Field(Of String) = new Field(Of String)("FlowAct", true)
        Private FI_SignLine_Define As Field(Of String) = new Field(Of String)("SignLine_Define", true)
        Private FI_SingID_Define As Field(Of String) = new Field(Of String)("SingID_Define", true)
        Private FI_SpeComp As Field(Of String) = new Field(Of String)("SpeComp", true)
        Private FI_SpeEmpID As Field(Of String) = new Field(Of String)("SpeEmpID", true)
        Private FI_SpeName As Field(Of String) = new Field(Of String)("SpeName", true)
        Private FI_LastChgCompID As Field(Of String) = new Field(Of String)("LastChgCompID", true)
        Private FI_LastChgEmpID As Field(Of String) = new Field(Of String)("LastChgEmpID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "FlowCode", "FlowName", "FlowSN", "FlowSeq", "FlowStartFlag", "FlowEndFlag", "InValidFlag", "FlowAct", "SignLine_Define", "SingID_Define" _
                                    , "SpeComp", "SpeEmpID", "SpeName", "LastChgCompID", "LastChgEmpID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "FlowCode", "FlowSN", "FlowSeq" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "FlowCode"
                    Return FI_FlowCode.Value
                Case "FlowName"
                    Return FI_FlowName.Value
                Case "FlowSN"
                    Return FI_FlowSN.Value
                Case "FlowSeq"
                    Return FI_FlowSeq.Value
                Case "FlowStartFlag"
                    Return FI_FlowStartFlag.Value
                Case "FlowEndFlag"
                    Return FI_FlowEndFlag.Value
                Case "InValidFlag"
                    Return FI_InValidFlag.Value
                Case "FlowAct"
                    Return FI_FlowAct.Value
                Case "SignLine_Define"
                    Return FI_SignLine_Define.Value
                Case "SingID_Define"
                    Return FI_SingID_Define.Value
                Case "SpeComp"
                    Return FI_SpeComp.Value
                Case "SpeEmpID"
                    Return FI_SpeEmpID.Value
                Case "SpeName"
                    Return FI_SpeName.Value
                Case "LastChgCompID"
                    Return FI_LastChgCompID.Value
                Case "LastChgEmpID"
                    Return FI_LastChgEmpID.Value
                Case "LastChgDate"
                    Return FI_LastChgDate.Value
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Sub SetFieldValue(ByVal fieldName As String, ByVal value As Object)
            Select Case fieldName
                Case "FlowCode"
                    FI_FlowCode.SetValue(value)
                Case "FlowName"
                    FI_FlowName.SetValue(value)
                Case "FlowSN"
                    FI_FlowSN.SetValue(value)
                Case "FlowSeq"
                    FI_FlowSeq.SetValue(value)
                Case "FlowStartFlag"
                    FI_FlowStartFlag.SetValue(value)
                Case "FlowEndFlag"
                    FI_FlowEndFlag.SetValue(value)
                Case "InValidFlag"
                    FI_InValidFlag.SetValue(value)
                Case "FlowAct"
                    FI_FlowAct.SetValue(value)
                Case "SignLine_Define"
                    FI_SignLine_Define.SetValue(value)
                Case "SingID_Define"
                    FI_SingID_Define.SetValue(value)
                Case "SpeComp"
                    FI_SpeComp.SetValue(value)
                Case "SpeEmpID"
                    FI_SpeEmpID.SetValue(value)
                Case "SpeName"
                    FI_SpeName.SetValue(value)
                Case "LastChgCompID"
                    FI_LastChgCompID.SetValue(value)
                Case "LastChgEmpID"
                    FI_LastChgEmpID.SetValue(value)
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
                Case "FlowCode"
                    return FI_FlowCode.Updated
                Case "FlowName"
                    return FI_FlowName.Updated
                Case "FlowSN"
                    return FI_FlowSN.Updated
                Case "FlowSeq"
                    return FI_FlowSeq.Updated
                Case "FlowStartFlag"
                    return FI_FlowStartFlag.Updated
                Case "FlowEndFlag"
                    return FI_FlowEndFlag.Updated
                Case "InValidFlag"
                    return FI_InValidFlag.Updated
                Case "FlowAct"
                    return FI_FlowAct.Updated
                Case "SignLine_Define"
                    return FI_SignLine_Define.Updated
                Case "SingID_Define"
                    return FI_SingID_Define.Updated
                Case "SpeComp"
                    return FI_SpeComp.Updated
                Case "SpeEmpID"
                    return FI_SpeEmpID.Updated
                Case "SpeName"
                    return FI_SpeName.Updated
                Case "LastChgCompID"
                    return FI_LastChgCompID.Updated
                Case "LastChgEmpID"
                    return FI_LastChgEmpID.Updated
                Case "LastChgDate"
                    return FI_LastChgDate.Updated
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public Function CreateUpdateSQL(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "FlowCode"
                    return FI_FlowCode.CreateUpdateSQL
                Case "FlowName"
                    return FI_FlowName.CreateUpdateSQL
                Case "FlowSN"
                    return FI_FlowSN.CreateUpdateSQL
                Case "FlowSeq"
                    return FI_FlowSeq.CreateUpdateSQL
                Case "FlowStartFlag"
                    return FI_FlowStartFlag.CreateUpdateSQL
                Case "FlowEndFlag"
                    return FI_FlowEndFlag.CreateUpdateSQL
                Case "InValidFlag"
                    return FI_InValidFlag.CreateUpdateSQL
                Case "FlowAct"
                    return FI_FlowAct.CreateUpdateSQL
                Case "SignLine_Define"
                    return FI_SignLine_Define.CreateUpdateSQL
                Case "SingID_Define"
                    return FI_SingID_Define.CreateUpdateSQL
                Case "SpeComp"
                    return FI_SpeComp.CreateUpdateSQL
                Case "SpeEmpID"
                    return FI_SpeEmpID.CreateUpdateSQL
                Case "SpeName"
                    return FI_SpeName.CreateUpdateSQL
                Case "LastChgCompID"
                    return FI_LastChgCompID.CreateUpdateSQL
                Case "LastChgEmpID"
                    return FI_LastChgEmpID.CreateUpdateSQL
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
            FI_FlowCode.SetInitValue("")
            FI_FlowName.SetInitValue("")
            FI_FlowSN.SetInitValue("")
            FI_FlowSeq.SetInitValue("")
            FI_FlowStartFlag.SetInitValue("")
            FI_FlowEndFlag.SetInitValue("")
            FI_InValidFlag.SetInitValue("")
            FI_FlowAct.SetInitValue("")
            FI_SignLine_Define.SetInitValue("")
            FI_SingID_Define.SetInitValue("")
            FI_SpeComp.SetInitValue("")
            FI_SpeEmpID.SetInitValue("")
            FI_SpeName.SetInitValue("")
            FI_LastChgCompID.SetInitValue("")
            FI_LastChgEmpID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_FlowCode.SetInitValue(dr("FlowCode"))
            FI_FlowName.SetInitValue(dr("FlowName"))
            FI_FlowSN.SetInitValue(dr("FlowSN"))
            FI_FlowSeq.SetInitValue(dr("FlowSeq"))
            FI_FlowStartFlag.SetInitValue(dr("FlowStartFlag"))
            FI_FlowEndFlag.SetInitValue(dr("FlowEndFlag"))
            FI_InValidFlag.SetInitValue(dr("InValidFlag"))
            FI_FlowAct.SetInitValue(dr("FlowAct"))
            FI_SignLine_Define.SetInitValue(dr("SignLine_Define"))
            FI_SingID_Define.SetInitValue(dr("SingID_Define"))
            FI_SpeComp.SetInitValue(dr("SpeComp"))
            FI_SpeEmpID.SetInitValue(dr("SpeEmpID"))
            FI_SpeName.SetInitValue(dr("SpeName"))
            FI_LastChgCompID.SetInitValue(dr("LastChgCompID"))
            FI_LastChgEmpID.SetInitValue(dr("LastChgEmpID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_FlowCode.Updated = False
            FI_FlowName.Updated = False
            FI_FlowSN.Updated = False
            FI_FlowSeq.Updated = False
            FI_FlowStartFlag.Updated = False
            FI_FlowEndFlag.Updated = False
            FI_InValidFlag.Updated = False
            FI_FlowAct.Updated = False
            FI_SignLine_Define.Updated = False
            FI_SingID_Define.Updated = False
            FI_SpeComp.Updated = False
            FI_SpeEmpID.Updated = False
            FI_SpeName.Updated = False
            FI_LastChgCompID.Updated = False
            FI_LastChgEmpID.Updated = False
            FI_LastChgDate.Updated = False
        End Sub

        Public ReadOnly Property FlowCode As Field(Of String) 
            Get
                Return FI_FlowCode
            End Get
        End Property

        Public ReadOnly Property FlowName As Field(Of String) 
            Get
                Return FI_FlowName
            End Get
        End Property

        Public ReadOnly Property FlowSN As Field(Of String) 
            Get
                Return FI_FlowSN
            End Get
        End Property

        Public ReadOnly Property FlowSeq As Field(Of String) 
            Get
                Return FI_FlowSeq
            End Get
        End Property

        Public ReadOnly Property FlowStartFlag As Field(Of String) 
            Get
                Return FI_FlowStartFlag
            End Get
        End Property

        Public ReadOnly Property FlowEndFlag As Field(Of String) 
            Get
                Return FI_FlowEndFlag
            End Get
        End Property

        Public ReadOnly Property InValidFlag As Field(Of String) 
            Get
                Return FI_InValidFlag
            End Get
        End Property

        Public ReadOnly Property FlowAct As Field(Of String) 
            Get
                Return FI_FlowAct
            End Get
        End Property

        Public ReadOnly Property SignLine_Define As Field(Of String) 
            Get
                Return FI_SignLine_Define
            End Get
        End Property

        Public ReadOnly Property SingID_Define As Field(Of String) 
            Get
                Return FI_SingID_Define
            End Get
        End Property

        Public ReadOnly Property SpeComp As Field(Of String) 
            Get
                Return FI_SpeComp
            End Get
        End Property

        Public ReadOnly Property SpeEmpID As Field(Of String) 
            Get
                Return FI_SpeEmpID
            End Get
        End Property

        Public ReadOnly Property SpeName As Field(Of String) 
            Get
                Return FI_SpeName
            End Get
        End Property

        Public ReadOnly Property LastChgCompID As Field(Of String) 
            Get
                Return FI_LastChgCompID
            End Get
        End Property

        Public ReadOnly Property LastChgEmpID As Field(Of String) 
            Get
                Return FI_LastChgEmpID
            End Get
        End Property

        Public ReadOnly Property LastChgDate As Field(Of Date) 
            Get
                Return FI_LastChgDate
            End Get
        End Property

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal Flow_EngineRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From Flow_Engine")
            strSQL.AppendLine("Where FlowCode = @FlowCode")
            strSQL.AppendLine("And FlowSN = @FlowSN")
            strSQL.AppendLine("And FlowSeq = @FlowSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowCode", DbType.String, Flow_EngineRow.FlowCode.Value)
            db.AddInParameter(dbcmd, "@FlowSN", DbType.String, Flow_EngineRow.FlowSN.Value)
            db.AddInParameter(dbcmd, "@FlowSeq", DbType.String, Flow_EngineRow.FlowSeq.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal Flow_EngineRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From Flow_Engine")
            strSQL.AppendLine("Where FlowCode = @FlowCode")
            strSQL.AppendLine("And FlowSN = @FlowSN")
            strSQL.AppendLine("And FlowSeq = @FlowSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowCode", DbType.String, Flow_EngineRow.FlowCode.Value)
            db.AddInParameter(dbcmd, "@FlowSN", DbType.String, Flow_EngineRow.FlowSN.Value)
            db.AddInParameter(dbcmd, "@FlowSeq", DbType.String, Flow_EngineRow.FlowSeq.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal Flow_EngineRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From Flow_Engine")
            strSQL.AppendLine("Where FlowCode = @FlowCode")
            strSQL.AppendLine("And FlowSN = @FlowSN")
            strSQL.AppendLine("And FlowSeq = @FlowSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In Flow_EngineRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@FlowCode", DbType.String, r.FlowCode.Value)
                        db.AddInParameter(dbcmd, "@FlowSN", DbType.String, r.FlowSN.Value)
                        db.AddInParameter(dbcmd, "@FlowSeq", DbType.String, r.FlowSeq.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal Flow_EngineRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From Flow_Engine")
            strSQL.AppendLine("Where FlowCode = @FlowCode")
            strSQL.AppendLine("And FlowSN = @FlowSN")
            strSQL.AppendLine("And FlowSeq = @FlowSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In Flow_EngineRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@FlowCode", DbType.String, r.FlowCode.Value)
                db.AddInParameter(dbcmd, "@FlowSN", DbType.String, r.FlowSN.Value)
                db.AddInParameter(dbcmd, "@FlowSeq", DbType.String, r.FlowSeq.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal Flow_EngineRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Flow_Engine")
            strSQL.AppendLine("Where FlowCode = @FlowCode")
            strSQL.AppendLine("And FlowSN = @FlowSN")
            strSQL.AppendLine("And FlowSeq = @FlowSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowCode", DbType.String, Flow_EngineRow.FlowCode.Value)
            db.AddInParameter(dbcmd, "@FlowSN", DbType.String, Flow_EngineRow.FlowSN.Value)
            db.AddInParameter(dbcmd, "@FlowSeq", DbType.String, Flow_EngineRow.FlowSeq.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(Flow_EngineRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Flow_Engine")
            strSQL.AppendLine("Where FlowCode = @FlowCode")
            strSQL.AppendLine("And FlowSN = @FlowSN")
            strSQL.AppendLine("And FlowSeq = @FlowSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowCode", DbType.String, Flow_EngineRow.FlowCode.Value)
            db.AddInParameter(dbcmd, "@FlowSN", DbType.String, Flow_EngineRow.FlowSN.Value)
            db.AddInParameter(dbcmd, "@FlowSeq", DbType.String, Flow_EngineRow.FlowSeq.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal Flow_EngineRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update Flow_Engine Set")
            For i As Integer = 0 To Flow_EngineRow.FieldNames.Length - 1
                If Not Flow_EngineRow.IsIdentityField(Flow_EngineRow.FieldNames(i)) AndAlso Flow_EngineRow.IsUpdated(Flow_EngineRow.FieldNames(i)) AndAlso Flow_EngineRow.CreateUpdateSQL(Flow_EngineRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, Flow_EngineRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where FlowCode = @PKFlowCode")
            strSQL.AppendLine("And FlowSN = @PKFlowSN")
            strSQL.AppendLine("And FlowSeq = @PKFlowSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If Flow_EngineRow.FlowCode.Updated Then db.AddInParameter(dbcmd, "@FlowCode", DbType.String, Flow_EngineRow.FlowCode.Value)
            If Flow_EngineRow.FlowName.Updated Then db.AddInParameter(dbcmd, "@FlowName", DbType.String, Flow_EngineRow.FlowName.Value)
            If Flow_EngineRow.FlowSN.Updated Then db.AddInParameter(dbcmd, "@FlowSN", DbType.String, Flow_EngineRow.FlowSN.Value)
            If Flow_EngineRow.FlowSeq.Updated Then db.AddInParameter(dbcmd, "@FlowSeq", DbType.String, Flow_EngineRow.FlowSeq.Value)
            If Flow_EngineRow.FlowStartFlag.Updated Then db.AddInParameter(dbcmd, "@FlowStartFlag", DbType.String, Flow_EngineRow.FlowStartFlag.Value)
            If Flow_EngineRow.FlowEndFlag.Updated Then db.AddInParameter(dbcmd, "@FlowEndFlag", DbType.String, Flow_EngineRow.FlowEndFlag.Value)
            If Flow_EngineRow.InValidFlag.Updated Then db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, Flow_EngineRow.InValidFlag.Value)
            If Flow_EngineRow.FlowAct.Updated Then db.AddInParameter(dbcmd, "@FlowAct", DbType.String, Flow_EngineRow.FlowAct.Value)
            If Flow_EngineRow.SignLine_Define.Updated Then db.AddInParameter(dbcmd, "@SignLine_Define", DbType.String, Flow_EngineRow.SignLine_Define.Value)
            If Flow_EngineRow.SingID_Define.Updated Then db.AddInParameter(dbcmd, "@SingID_Define", DbType.String, Flow_EngineRow.SingID_Define.Value)
            If Flow_EngineRow.SpeComp.Updated Then db.AddInParameter(dbcmd, "@SpeComp", DbType.String, Flow_EngineRow.SpeComp.Value)
            If Flow_EngineRow.SpeEmpID.Updated Then db.AddInParameter(dbcmd, "@SpeEmpID", DbType.String, Flow_EngineRow.SpeEmpID.Value)
            If Flow_EngineRow.SpeName.Updated Then db.AddInParameter(dbcmd, "@SpeName", DbType.String, Flow_EngineRow.SpeName.Value)
            If Flow_EngineRow.LastChgCompID.Updated Then db.AddInParameter(dbcmd, "@LastChgCompID", DbType.String, Flow_EngineRow.LastChgCompID.Value)
            If Flow_EngineRow.LastChgEmpID.Updated Then db.AddInParameter(dbcmd, "@LastChgEmpID", DbType.String, Flow_EngineRow.LastChgEmpID.Value)
            If Flow_EngineRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(Flow_EngineRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), Flow_EngineRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKFlowCode", DbType.String, IIf(Flow_EngineRow.LoadFromDataRow, Flow_EngineRow.FlowCode.OldValue, Flow_EngineRow.FlowCode.Value))
            db.AddInParameter(dbcmd, "@PKFlowSN", DbType.String, IIf(Flow_EngineRow.LoadFromDataRow, Flow_EngineRow.FlowSN.OldValue, Flow_EngineRow.FlowSN.Value))
            db.AddInParameter(dbcmd, "@PKFlowSeq", DbType.String, IIf(Flow_EngineRow.LoadFromDataRow, Flow_EngineRow.FlowSeq.OldValue, Flow_EngineRow.FlowSeq.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal Flow_EngineRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update Flow_Engine Set")
            For i As Integer = 0 To Flow_EngineRow.FieldNames.Length - 1
                If Not Flow_EngineRow.IsIdentityField(Flow_EngineRow.FieldNames(i)) AndAlso Flow_EngineRow.IsUpdated(Flow_EngineRow.FieldNames(i)) AndAlso Flow_EngineRow.CreateUpdateSQL(Flow_EngineRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, Flow_EngineRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where FlowCode = @PKFlowCode")
            strSQL.AppendLine("And FlowSN = @PKFlowSN")
            strSQL.AppendLine("And FlowSeq = @PKFlowSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If Flow_EngineRow.FlowCode.Updated Then db.AddInParameter(dbcmd, "@FlowCode", DbType.String, Flow_EngineRow.FlowCode.Value)
            If Flow_EngineRow.FlowName.Updated Then db.AddInParameter(dbcmd, "@FlowName", DbType.String, Flow_EngineRow.FlowName.Value)
            If Flow_EngineRow.FlowSN.Updated Then db.AddInParameter(dbcmd, "@FlowSN", DbType.String, Flow_EngineRow.FlowSN.Value)
            If Flow_EngineRow.FlowSeq.Updated Then db.AddInParameter(dbcmd, "@FlowSeq", DbType.String, Flow_EngineRow.FlowSeq.Value)
            If Flow_EngineRow.FlowStartFlag.Updated Then db.AddInParameter(dbcmd, "@FlowStartFlag", DbType.String, Flow_EngineRow.FlowStartFlag.Value)
            If Flow_EngineRow.FlowEndFlag.Updated Then db.AddInParameter(dbcmd, "@FlowEndFlag", DbType.String, Flow_EngineRow.FlowEndFlag.Value)
            If Flow_EngineRow.InValidFlag.Updated Then db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, Flow_EngineRow.InValidFlag.Value)
            If Flow_EngineRow.FlowAct.Updated Then db.AddInParameter(dbcmd, "@FlowAct", DbType.String, Flow_EngineRow.FlowAct.Value)
            If Flow_EngineRow.SignLine_Define.Updated Then db.AddInParameter(dbcmd, "@SignLine_Define", DbType.String, Flow_EngineRow.SignLine_Define.Value)
            If Flow_EngineRow.SingID_Define.Updated Then db.AddInParameter(dbcmd, "@SingID_Define", DbType.String, Flow_EngineRow.SingID_Define.Value)
            If Flow_EngineRow.SpeComp.Updated Then db.AddInParameter(dbcmd, "@SpeComp", DbType.String, Flow_EngineRow.SpeComp.Value)
            If Flow_EngineRow.SpeEmpID.Updated Then db.AddInParameter(dbcmd, "@SpeEmpID", DbType.String, Flow_EngineRow.SpeEmpID.Value)
            If Flow_EngineRow.SpeName.Updated Then db.AddInParameter(dbcmd, "@SpeName", DbType.String, Flow_EngineRow.SpeName.Value)
            If Flow_EngineRow.LastChgCompID.Updated Then db.AddInParameter(dbcmd, "@LastChgCompID", DbType.String, Flow_EngineRow.LastChgCompID.Value)
            If Flow_EngineRow.LastChgEmpID.Updated Then db.AddInParameter(dbcmd, "@LastChgEmpID", DbType.String, Flow_EngineRow.LastChgEmpID.Value)
            If Flow_EngineRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(Flow_EngineRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), Flow_EngineRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKFlowCode", DbType.String, IIf(Flow_EngineRow.LoadFromDataRow, Flow_EngineRow.FlowCode.OldValue, Flow_EngineRow.FlowCode.Value))
            db.AddInParameter(dbcmd, "@PKFlowSN", DbType.String, IIf(Flow_EngineRow.LoadFromDataRow, Flow_EngineRow.FlowSN.OldValue, Flow_EngineRow.FlowSN.Value))
            db.AddInParameter(dbcmd, "@PKFlowSeq", DbType.String, IIf(Flow_EngineRow.LoadFromDataRow, Flow_EngineRow.FlowSeq.OldValue, Flow_EngineRow.FlowSeq.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal Flow_EngineRow As Row()) As Integer
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
                    For Each r As Row In Flow_EngineRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update Flow_Engine Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where FlowCode = @PKFlowCode")
                        strSQL.AppendLine("And FlowSN = @PKFlowSN")
                        strSQL.AppendLine("And FlowSeq = @PKFlowSeq")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.FlowCode.Updated Then db.AddInParameter(dbcmd, "@FlowCode", DbType.String, r.FlowCode.Value)
                        If r.FlowName.Updated Then db.AddInParameter(dbcmd, "@FlowName", DbType.String, r.FlowName.Value)
                        If r.FlowSN.Updated Then db.AddInParameter(dbcmd, "@FlowSN", DbType.String, r.FlowSN.Value)
                        If r.FlowSeq.Updated Then db.AddInParameter(dbcmd, "@FlowSeq", DbType.String, r.FlowSeq.Value)
                        If r.FlowStartFlag.Updated Then db.AddInParameter(dbcmd, "@FlowStartFlag", DbType.String, r.FlowStartFlag.Value)
                        If r.FlowEndFlag.Updated Then db.AddInParameter(dbcmd, "@FlowEndFlag", DbType.String, r.FlowEndFlag.Value)
                        If r.InValidFlag.Updated Then db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, r.InValidFlag.Value)
                        If r.FlowAct.Updated Then db.AddInParameter(dbcmd, "@FlowAct", DbType.String, r.FlowAct.Value)
                        If r.SignLine_Define.Updated Then db.AddInParameter(dbcmd, "@SignLine_Define", DbType.String, r.SignLine_Define.Value)
                        If r.SingID_Define.Updated Then db.AddInParameter(dbcmd, "@SingID_Define", DbType.String, r.SingID_Define.Value)
                        If r.SpeComp.Updated Then db.AddInParameter(dbcmd, "@SpeComp", DbType.String, r.SpeComp.Value)
                        If r.SpeEmpID.Updated Then db.AddInParameter(dbcmd, "@SpeEmpID", DbType.String, r.SpeEmpID.Value)
                        If r.SpeName.Updated Then db.AddInParameter(dbcmd, "@SpeName", DbType.String, r.SpeName.Value)
                        If r.LastChgCompID.Updated Then db.AddInParameter(dbcmd, "@LastChgCompID", DbType.String, r.LastChgCompID.Value)
                        If r.LastChgEmpID.Updated Then db.AddInParameter(dbcmd, "@LastChgEmpID", DbType.String, r.LastChgEmpID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKFlowCode", DbType.String, IIf(r.LoadFromDataRow, r.FlowCode.OldValue, r.FlowCode.Value))
                        db.AddInParameter(dbcmd, "@PKFlowSN", DbType.String, IIf(r.LoadFromDataRow, r.FlowSN.OldValue, r.FlowSN.Value))
                        db.AddInParameter(dbcmd, "@PKFlowSeq", DbType.String, IIf(r.LoadFromDataRow, r.FlowSeq.OldValue, r.FlowSeq.Value))

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

        Public Function Update(ByVal Flow_EngineRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In Flow_EngineRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update Flow_Engine Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where FlowCode = @PKFlowCode")
                strSQL.AppendLine("And FlowSN = @PKFlowSN")
                strSQL.AppendLine("And FlowSeq = @PKFlowSeq")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.FlowCode.Updated Then db.AddInParameter(dbcmd, "@FlowCode", DbType.String, r.FlowCode.Value)
                If r.FlowName.Updated Then db.AddInParameter(dbcmd, "@FlowName", DbType.String, r.FlowName.Value)
                If r.FlowSN.Updated Then db.AddInParameter(dbcmd, "@FlowSN", DbType.String, r.FlowSN.Value)
                If r.FlowSeq.Updated Then db.AddInParameter(dbcmd, "@FlowSeq", DbType.String, r.FlowSeq.Value)
                If r.FlowStartFlag.Updated Then db.AddInParameter(dbcmd, "@FlowStartFlag", DbType.String, r.FlowStartFlag.Value)
                If r.FlowEndFlag.Updated Then db.AddInParameter(dbcmd, "@FlowEndFlag", DbType.String, r.FlowEndFlag.Value)
                If r.InValidFlag.Updated Then db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, r.InValidFlag.Value)
                If r.FlowAct.Updated Then db.AddInParameter(dbcmd, "@FlowAct", DbType.String, r.FlowAct.Value)
                If r.SignLine_Define.Updated Then db.AddInParameter(dbcmd, "@SignLine_Define", DbType.String, r.SignLine_Define.Value)
                If r.SingID_Define.Updated Then db.AddInParameter(dbcmd, "@SingID_Define", DbType.String, r.SingID_Define.Value)
                If r.SpeComp.Updated Then db.AddInParameter(dbcmd, "@SpeComp", DbType.String, r.SpeComp.Value)
                If r.SpeEmpID.Updated Then db.AddInParameter(dbcmd, "@SpeEmpID", DbType.String, r.SpeEmpID.Value)
                If r.SpeName.Updated Then db.AddInParameter(dbcmd, "@SpeName", DbType.String, r.SpeName.Value)
                If r.LastChgCompID.Updated Then db.AddInParameter(dbcmd, "@LastChgCompID", DbType.String, r.LastChgCompID.Value)
                If r.LastChgEmpID.Updated Then db.AddInParameter(dbcmd, "@LastChgEmpID", DbType.String, r.LastChgEmpID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKFlowCode", DbType.String, IIf(r.LoadFromDataRow, r.FlowCode.OldValue, r.FlowCode.Value))
                db.AddInParameter(dbcmd, "@PKFlowSN", DbType.String, IIf(r.LoadFromDataRow, r.FlowSN.OldValue, r.FlowSN.Value))
                db.AddInParameter(dbcmd, "@PKFlowSeq", DbType.String, IIf(r.LoadFromDataRow, r.FlowSeq.OldValue, r.FlowSeq.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal Flow_EngineRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From Flow_Engine")
            strSQL.AppendLine("Where FlowCode = @FlowCode")
            strSQL.AppendLine("And FlowSN = @FlowSN")
            strSQL.AppendLine("And FlowSeq = @FlowSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowCode", DbType.String, Flow_EngineRow.FlowCode.Value)
            db.AddInParameter(dbcmd, "@FlowSN", DbType.String, Flow_EngineRow.FlowSN.Value)
            db.AddInParameter(dbcmd, "@FlowSeq", DbType.String, Flow_EngineRow.FlowSeq.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal Flow_EngineRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From Flow_Engine")
            strSQL.AppendLine("Where FlowCode = @FlowCode")
            strSQL.AppendLine("And FlowSN = @FlowSN")
            strSQL.AppendLine("And FlowSeq = @FlowSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowCode", DbType.String, Flow_EngineRow.FlowCode.Value)
            db.AddInParameter(dbcmd, "@FlowSN", DbType.String, Flow_EngineRow.FlowSN.Value)
            db.AddInParameter(dbcmd, "@FlowSeq", DbType.String, Flow_EngineRow.FlowSeq.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Flow_Engine")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal Flow_EngineRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into Flow_Engine")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowCode, FlowName, FlowSN, FlowSeq, FlowStartFlag, FlowEndFlag, InValidFlag, FlowAct,")
            strSQL.AppendLine("    SignLine_Define, SingID_Define, SpeComp, SpeEmpID, SpeName, LastChgCompID, LastChgEmpID,")
            strSQL.AppendLine("    LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowCode, @FlowName, @FlowSN, @FlowSeq, @FlowStartFlag, @FlowEndFlag, @InValidFlag, @FlowAct,")
            strSQL.AppendLine("    @SignLine_Define, @SingID_Define, @SpeComp, @SpeEmpID, @SpeName, @LastChgCompID, @LastChgEmpID,")
            strSQL.AppendLine("    @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowCode", DbType.String, Flow_EngineRow.FlowCode.Value)
            db.AddInParameter(dbcmd, "@FlowName", DbType.String, Flow_EngineRow.FlowName.Value)
            db.AddInParameter(dbcmd, "@FlowSN", DbType.String, Flow_EngineRow.FlowSN.Value)
            db.AddInParameter(dbcmd, "@FlowSeq", DbType.String, Flow_EngineRow.FlowSeq.Value)
            db.AddInParameter(dbcmd, "@FlowStartFlag", DbType.String, Flow_EngineRow.FlowStartFlag.Value)
            db.AddInParameter(dbcmd, "@FlowEndFlag", DbType.String, Flow_EngineRow.FlowEndFlag.Value)
            db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, Flow_EngineRow.InValidFlag.Value)
            db.AddInParameter(dbcmd, "@FlowAct", DbType.String, Flow_EngineRow.FlowAct.Value)
            db.AddInParameter(dbcmd, "@SignLine_Define", DbType.String, Flow_EngineRow.SignLine_Define.Value)
            db.AddInParameter(dbcmd, "@SingID_Define", DbType.String, Flow_EngineRow.SingID_Define.Value)
            db.AddInParameter(dbcmd, "@SpeComp", DbType.String, Flow_EngineRow.SpeComp.Value)
            db.AddInParameter(dbcmd, "@SpeEmpID", DbType.String, Flow_EngineRow.SpeEmpID.Value)
            db.AddInParameter(dbcmd, "@SpeName", DbType.String, Flow_EngineRow.SpeName.Value)
            db.AddInParameter(dbcmd, "@LastChgCompID", DbType.String, Flow_EngineRow.LastChgCompID.Value)
            db.AddInParameter(dbcmd, "@LastChgEmpID", DbType.String, Flow_EngineRow.LastChgEmpID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(Flow_EngineRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), Flow_EngineRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal Flow_EngineRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into Flow_Engine")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowCode, FlowName, FlowSN, FlowSeq, FlowStartFlag, FlowEndFlag, InValidFlag, FlowAct,")
            strSQL.AppendLine("    SignLine_Define, SingID_Define, SpeComp, SpeEmpID, SpeName, LastChgCompID, LastChgEmpID,")
            strSQL.AppendLine("    LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowCode, @FlowName, @FlowSN, @FlowSeq, @FlowStartFlag, @FlowEndFlag, @InValidFlag, @FlowAct,")
            strSQL.AppendLine("    @SignLine_Define, @SingID_Define, @SpeComp, @SpeEmpID, @SpeName, @LastChgCompID, @LastChgEmpID,")
            strSQL.AppendLine("    @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowCode", DbType.String, Flow_EngineRow.FlowCode.Value)
            db.AddInParameter(dbcmd, "@FlowName", DbType.String, Flow_EngineRow.FlowName.Value)
            db.AddInParameter(dbcmd, "@FlowSN", DbType.String, Flow_EngineRow.FlowSN.Value)
            db.AddInParameter(dbcmd, "@FlowSeq", DbType.String, Flow_EngineRow.FlowSeq.Value)
            db.AddInParameter(dbcmd, "@FlowStartFlag", DbType.String, Flow_EngineRow.FlowStartFlag.Value)
            db.AddInParameter(dbcmd, "@FlowEndFlag", DbType.String, Flow_EngineRow.FlowEndFlag.Value)
            db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, Flow_EngineRow.InValidFlag.Value)
            db.AddInParameter(dbcmd, "@FlowAct", DbType.String, Flow_EngineRow.FlowAct.Value)
            db.AddInParameter(dbcmd, "@SignLine_Define", DbType.String, Flow_EngineRow.SignLine_Define.Value)
            db.AddInParameter(dbcmd, "@SingID_Define", DbType.String, Flow_EngineRow.SingID_Define.Value)
            db.AddInParameter(dbcmd, "@SpeComp", DbType.String, Flow_EngineRow.SpeComp.Value)
            db.AddInParameter(dbcmd, "@SpeEmpID", DbType.String, Flow_EngineRow.SpeEmpID.Value)
            db.AddInParameter(dbcmd, "@SpeName", DbType.String, Flow_EngineRow.SpeName.Value)
            db.AddInParameter(dbcmd, "@LastChgCompID", DbType.String, Flow_EngineRow.LastChgCompID.Value)
            db.AddInParameter(dbcmd, "@LastChgEmpID", DbType.String, Flow_EngineRow.LastChgEmpID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(Flow_EngineRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), Flow_EngineRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal Flow_EngineRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into Flow_Engine")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowCode, FlowName, FlowSN, FlowSeq, FlowStartFlag, FlowEndFlag, InValidFlag, FlowAct,")
            strSQL.AppendLine("    SignLine_Define, SingID_Define, SpeComp, SpeEmpID, SpeName, LastChgCompID, LastChgEmpID,")
            strSQL.AppendLine("    LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowCode, @FlowName, @FlowSN, @FlowSeq, @FlowStartFlag, @FlowEndFlag, @InValidFlag, @FlowAct,")
            strSQL.AppendLine("    @SignLine_Define, @SingID_Define, @SpeComp, @SpeEmpID, @SpeName, @LastChgCompID, @LastChgEmpID,")
            strSQL.AppendLine("    @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In Flow_EngineRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@FlowCode", DbType.String, r.FlowCode.Value)
                        db.AddInParameter(dbcmd, "@FlowName", DbType.String, r.FlowName.Value)
                        db.AddInParameter(dbcmd, "@FlowSN", DbType.String, r.FlowSN.Value)
                        db.AddInParameter(dbcmd, "@FlowSeq", DbType.String, r.FlowSeq.Value)
                        db.AddInParameter(dbcmd, "@FlowStartFlag", DbType.String, r.FlowStartFlag.Value)
                        db.AddInParameter(dbcmd, "@FlowEndFlag", DbType.String, r.FlowEndFlag.Value)
                        db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, r.InValidFlag.Value)
                        db.AddInParameter(dbcmd, "@FlowAct", DbType.String, r.FlowAct.Value)
                        db.AddInParameter(dbcmd, "@SignLine_Define", DbType.String, r.SignLine_Define.Value)
                        db.AddInParameter(dbcmd, "@SingID_Define", DbType.String, r.SingID_Define.Value)
                        db.AddInParameter(dbcmd, "@SpeComp", DbType.String, r.SpeComp.Value)
                        db.AddInParameter(dbcmd, "@SpeEmpID", DbType.String, r.SpeEmpID.Value)
                        db.AddInParameter(dbcmd, "@SpeName", DbType.String, r.SpeName.Value)
                        db.AddInParameter(dbcmd, "@LastChgCompID", DbType.String, r.LastChgCompID.Value)
                        db.AddInParameter(dbcmd, "@LastChgEmpID", DbType.String, r.LastChgEmpID.Value)
                        db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))

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

        Public Function Insert(ByVal Flow_EngineRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into Flow_Engine")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowCode, FlowName, FlowSN, FlowSeq, FlowStartFlag, FlowEndFlag, InValidFlag, FlowAct,")
            strSQL.AppendLine("    SignLine_Define, SingID_Define, SpeComp, SpeEmpID, SpeName, LastChgCompID, LastChgEmpID,")
            strSQL.AppendLine("    LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowCode, @FlowName, @FlowSN, @FlowSeq, @FlowStartFlag, @FlowEndFlag, @InValidFlag, @FlowAct,")
            strSQL.AppendLine("    @SignLine_Define, @SingID_Define, @SpeComp, @SpeEmpID, @SpeName, @LastChgCompID, @LastChgEmpID,")
            strSQL.AppendLine("    @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In Flow_EngineRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@FlowCode", DbType.String, r.FlowCode.Value)
                db.AddInParameter(dbcmd, "@FlowName", DbType.String, r.FlowName.Value)
                db.AddInParameter(dbcmd, "@FlowSN", DbType.String, r.FlowSN.Value)
                db.AddInParameter(dbcmd, "@FlowSeq", DbType.String, r.FlowSeq.Value)
                db.AddInParameter(dbcmd, "@FlowStartFlag", DbType.String, r.FlowStartFlag.Value)
                db.AddInParameter(dbcmd, "@FlowEndFlag", DbType.String, r.FlowEndFlag.Value)
                db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, r.InValidFlag.Value)
                db.AddInParameter(dbcmd, "@FlowAct", DbType.String, r.FlowAct.Value)
                db.AddInParameter(dbcmd, "@SignLine_Define", DbType.String, r.SignLine_Define.Value)
                db.AddInParameter(dbcmd, "@SingID_Define", DbType.String, r.SingID_Define.Value)
                db.AddInParameter(dbcmd, "@SpeComp", DbType.String, r.SpeComp.Value)
                db.AddInParameter(dbcmd, "@SpeEmpID", DbType.String, r.SpeEmpID.Value)
                db.AddInParameter(dbcmd, "@SpeName", DbType.String, r.SpeName.Value)
                db.AddInParameter(dbcmd, "@LastChgCompID", DbType.String, r.LastChgCompID.Value)
                db.AddInParameter(dbcmd, "@LastChgEmpID", DbType.String, r.LastChgEmpID.Value)
                db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))

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

