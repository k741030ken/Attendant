'****************************************************************
' Table:WF_FlowStepM
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

Namespace beWF_FlowStepM
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "FlowID", "FlowVer", "FlowStepID", "Description", "MenuTitle", "ShowModeMenuTitle", "ProcDay", "Intimation", "AgreeRate", "DefaultPage" _
                                    , "ShowModePage", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(Integer), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Integer), GetType(String), GetType(Decimal), GetType(String) _
                                    , GetType(String), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "FlowID", "FlowVer", "FlowStepID" }

        Public ReadOnly Property Rows() As beWF_FlowStepM.Rows 
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
        Public Sub Transfer2Row(WF_FlowStepMTable As DataTable)
            For Each dr As DataRow In WF_FlowStepMTable.Rows
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
                dr(m_Rows(i).Description.FieldName) = m_Rows(i).Description.Value
                dr(m_Rows(i).MenuTitle.FieldName) = m_Rows(i).MenuTitle.Value
                dr(m_Rows(i).ShowModeMenuTitle.FieldName) = m_Rows(i).ShowModeMenuTitle.Value
                dr(m_Rows(i).ProcDay.FieldName) = m_Rows(i).ProcDay.Value
                dr(m_Rows(i).Intimation.FieldName) = m_Rows(i).Intimation.Value
                dr(m_Rows(i).AgreeRate.FieldName) = m_Rows(i).AgreeRate.Value
                dr(m_Rows(i).DefaultPage.FieldName) = m_Rows(i).DefaultPage.Value
                dr(m_Rows(i).ShowModePage.FieldName) = m_Rows(i).ShowModePage.Value
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

        Public Sub Add(WF_FlowStepMRow As Row)
            m_Rows.Add(WF_FlowStepMRow)
        End Sub

        Public Sub Remove(WF_FlowStepMRow As Row)
            If m_Rows.IndexOf(WF_FlowStepMRow) >= 0 Then
                m_Rows.Remove(WF_FlowStepMRow)
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
        Private FI_Description As Field(Of String) = new Field(Of String)("Description", true)
        Private FI_MenuTitle As Field(Of String) = new Field(Of String)("MenuTitle", true)
        Private FI_ShowModeMenuTitle As Field(Of String) = new Field(Of String)("ShowModeMenuTitle", true)
        Private FI_ProcDay As Field(Of Integer) = new Field(Of Integer)("ProcDay", true)
        Private FI_Intimation As Field(Of String) = new Field(Of String)("Intimation", true)
        Private FI_AgreeRate As Field(Of Decimal) = new Field(Of Decimal)("AgreeRate", true)
        Private FI_DefaultPage As Field(Of String) = new Field(Of String)("DefaultPage", true)
        Private FI_ShowModePage As Field(Of String) = new Field(Of String)("ShowModePage", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "FlowID", "FlowVer", "FlowStepID", "Description", "MenuTitle", "ShowModeMenuTitle", "ProcDay", "Intimation", "AgreeRate", "DefaultPage" _
                                    , "ShowModePage", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "FlowID", "FlowVer", "FlowStepID" }
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
                Case "Description"
                    Return FI_Description.Value
                Case "MenuTitle"
                    Return FI_MenuTitle.Value
                Case "ShowModeMenuTitle"
                    Return FI_ShowModeMenuTitle.Value
                Case "ProcDay"
                    Return FI_ProcDay.Value
                Case "Intimation"
                    Return FI_Intimation.Value
                Case "AgreeRate"
                    Return FI_AgreeRate.Value
                Case "DefaultPage"
                    Return FI_DefaultPage.Value
                Case "ShowModePage"
                    Return FI_ShowModePage.Value
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
                Case "FlowID"
                    FI_FlowID.SetValue(value)
                Case "FlowVer"
                    FI_FlowVer.SetValue(value)
                Case "FlowStepID"
                    FI_FlowStepID.SetValue(value)
                Case "Description"
                    FI_Description.SetValue(value)
                Case "MenuTitle"
                    FI_MenuTitle.SetValue(value)
                Case "ShowModeMenuTitle"
                    FI_ShowModeMenuTitle.SetValue(value)
                Case "ProcDay"
                    FI_ProcDay.SetValue(value)
                Case "Intimation"
                    FI_Intimation.SetValue(value)
                Case "AgreeRate"
                    FI_AgreeRate.SetValue(value)
                Case "DefaultPage"
                    FI_DefaultPage.SetValue(value)
                Case "ShowModePage"
                    FI_ShowModePage.SetValue(value)
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
                Case "FlowID"
                    return FI_FlowID.Updated
                Case "FlowVer"
                    return FI_FlowVer.Updated
                Case "FlowStepID"
                    return FI_FlowStepID.Updated
                Case "Description"
                    return FI_Description.Updated
                Case "MenuTitle"
                    return FI_MenuTitle.Updated
                Case "ShowModeMenuTitle"
                    return FI_ShowModeMenuTitle.Updated
                Case "ProcDay"
                    return FI_ProcDay.Updated
                Case "Intimation"
                    return FI_Intimation.Updated
                Case "AgreeRate"
                    return FI_AgreeRate.Updated
                Case "DefaultPage"
                    return FI_DefaultPage.Updated
                Case "ShowModePage"
                    return FI_ShowModePage.Updated
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
                Case "FlowID"
                    return FI_FlowID.CreateUpdateSQL
                Case "FlowVer"
                    return FI_FlowVer.CreateUpdateSQL
                Case "FlowStepID"
                    return FI_FlowStepID.CreateUpdateSQL
                Case "Description"
                    return FI_Description.CreateUpdateSQL
                Case "MenuTitle"
                    return FI_MenuTitle.CreateUpdateSQL
                Case "ShowModeMenuTitle"
                    return FI_ShowModeMenuTitle.CreateUpdateSQL
                Case "ProcDay"
                    return FI_ProcDay.CreateUpdateSQL
                Case "Intimation"
                    return FI_Intimation.CreateUpdateSQL
                Case "AgreeRate"
                    return FI_AgreeRate.CreateUpdateSQL
                Case "DefaultPage"
                    return FI_DefaultPage.CreateUpdateSQL
                Case "ShowModePage"
                    return FI_ShowModePage.CreateUpdateSQL
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
            FI_FlowID.SetInitValue("")
            FI_FlowVer.SetInitValue(0)
            FI_FlowStepID.SetInitValue("")
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_FlowID.SetInitValue(dr("FlowID"))
            FI_FlowVer.SetInitValue(dr("FlowVer"))
            FI_FlowStepID.SetInitValue(dr("FlowStepID"))
            FI_Description.SetInitValue(dr("Description"))
            FI_MenuTitle.SetInitValue(dr("MenuTitle"))
            FI_ShowModeMenuTitle.SetInitValue(dr("ShowModeMenuTitle"))
            FI_ProcDay.SetInitValue(dr("ProcDay"))
            FI_Intimation.SetInitValue(dr("Intimation"))
            FI_AgreeRate.SetInitValue(dr("AgreeRate"))
            FI_DefaultPage.SetInitValue(dr("DefaultPage"))
            FI_ShowModePage.SetInitValue(dr("ShowModePage"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_FlowID.Updated = False
            FI_FlowVer.Updated = False
            FI_FlowStepID.Updated = False
            FI_Description.Updated = False
            FI_MenuTitle.Updated = False
            FI_ShowModeMenuTitle.Updated = False
            FI_ProcDay.Updated = False
            FI_Intimation.Updated = False
            FI_AgreeRate.Updated = False
            FI_DefaultPage.Updated = False
            FI_ShowModePage.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
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

        Public ReadOnly Property Description As Field(Of String) 
            Get
                Return FI_Description
            End Get
        End Property

        Public ReadOnly Property MenuTitle As Field(Of String) 
            Get
                Return FI_MenuTitle
            End Get
        End Property

        Public ReadOnly Property ShowModeMenuTitle As Field(Of String) 
            Get
                Return FI_ShowModeMenuTitle
            End Get
        End Property

        Public ReadOnly Property ProcDay As Field(Of Integer) 
            Get
                Return FI_ProcDay
            End Get
        End Property

        Public ReadOnly Property Intimation As Field(Of String) 
            Get
                Return FI_Intimation
            End Get
        End Property

        Public ReadOnly Property AgreeRate As Field(Of Decimal) 
            Get
                Return FI_AgreeRate
            End Get
        End Property

        Public ReadOnly Property DefaultPage As Field(Of String) 
            Get
                Return FI_DefaultPage
            End Get
        End Property

        Public ReadOnly Property ShowModePage As Field(Of String) 
            Get
                Return FI_ShowModePage
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
        Public Function DeleteRowByPrimaryKey(ByVal WF_FlowStepMRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From WF_FlowStepM")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowVer = @FlowVer")
            strSQL.AppendLine("And FlowStepID = @FlowStepID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowStepMRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowVer", DbType.Int32, WF_FlowStepMRow.FlowVer.Value)
            db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, WF_FlowStepMRow.FlowStepID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal WF_FlowStepMRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From WF_FlowStepM")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowVer = @FlowVer")
            strSQL.AppendLine("And FlowStepID = @FlowStepID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowStepMRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowVer", DbType.Int32, WF_FlowStepMRow.FlowVer.Value)
            db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, WF_FlowStepMRow.FlowStepID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal WF_FlowStepMRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From WF_FlowStepM")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowVer = @FlowVer")
            strSQL.AppendLine("And FlowStepID = @FlowStepID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In WF_FlowStepMRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                        db.AddInParameter(dbcmd, "@FlowVer", DbType.Int32, r.FlowVer.Value)
                        db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, r.FlowStepID.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal WF_FlowStepMRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From WF_FlowStepM")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowVer = @FlowVer")
            strSQL.AppendLine("And FlowStepID = @FlowStepID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In WF_FlowStepMRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                db.AddInParameter(dbcmd, "@FlowVer", DbType.Int32, r.FlowVer.Value)
                db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, r.FlowStepID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal WF_FlowStepMRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WF_FlowStepM")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowVer = @FlowVer")
            strSQL.AppendLine("And FlowStepID = @FlowStepID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowStepMRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowVer", DbType.Int32, WF_FlowStepMRow.FlowVer.Value)
            db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, WF_FlowStepMRow.FlowStepID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(WF_FlowStepMRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WF_FlowStepM")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowVer = @FlowVer")
            strSQL.AppendLine("And FlowStepID = @FlowStepID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowStepMRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowVer", DbType.Int32, WF_FlowStepMRow.FlowVer.Value)
            db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, WF_FlowStepMRow.FlowStepID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal WF_FlowStepMRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update WF_FlowStepM Set")
            For i As Integer = 0 To WF_FlowStepMRow.FieldNames.Length - 1
                If Not WF_FlowStepMRow.IsIdentityField(WF_FlowStepMRow.FieldNames(i)) AndAlso WF_FlowStepMRow.IsUpdated(WF_FlowStepMRow.FieldNames(i)) AndAlso WF_FlowStepMRow.CreateUpdateSQL(WF_FlowStepMRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, WF_FlowStepMRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where FlowID = @PKFlowID")
            strSQL.AppendLine("And FlowVer = @PKFlowVer")
            strSQL.AppendLine("And FlowStepID = @PKFlowStepID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If WF_FlowStepMRow.FlowID.Updated Then db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowStepMRow.FlowID.Value)
            If WF_FlowStepMRow.FlowVer.Updated Then db.AddInParameter(dbcmd, "@FlowVer", DbType.Int32, WF_FlowStepMRow.FlowVer.Value)
            If WF_FlowStepMRow.FlowStepID.Updated Then db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, WF_FlowStepMRow.FlowStepID.Value)
            If WF_FlowStepMRow.Description.Updated Then db.AddInParameter(dbcmd, "@Description", DbType.String, WF_FlowStepMRow.Description.Value)
            If WF_FlowStepMRow.MenuTitle.Updated Then db.AddInParameter(dbcmd, "@MenuTitle", DbType.String, WF_FlowStepMRow.MenuTitle.Value)
            If WF_FlowStepMRow.ShowModeMenuTitle.Updated Then db.AddInParameter(dbcmd, "@ShowModeMenuTitle", DbType.String, WF_FlowStepMRow.ShowModeMenuTitle.Value)
            If WF_FlowStepMRow.ProcDay.Updated Then db.AddInParameter(dbcmd, "@ProcDay", DbType.Int32, WF_FlowStepMRow.ProcDay.Value)
            If WF_FlowStepMRow.Intimation.Updated Then db.AddInParameter(dbcmd, "@Intimation", DbType.String, WF_FlowStepMRow.Intimation.Value)
            If WF_FlowStepMRow.AgreeRate.Updated Then db.AddInParameter(dbcmd, "@AgreeRate", DbType.Decimal, WF_FlowStepMRow.AgreeRate.Value)
            If WF_FlowStepMRow.DefaultPage.Updated Then db.AddInParameter(dbcmd, "@DefaultPage", DbType.String, WF_FlowStepMRow.DefaultPage.Value)
            If WF_FlowStepMRow.ShowModePage.Updated Then db.AddInParameter(dbcmd, "@ShowModePage", DbType.String, WF_FlowStepMRow.ShowModePage.Value)
            If WF_FlowStepMRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WF_FlowStepMRow.LastChgID.Value)
            If WF_FlowStepMRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WF_FlowStepMRow.LastChgDate.Value), DBNull.Value, WF_FlowStepMRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKFlowID", DbType.String, IIf(WF_FlowStepMRow.LoadFromDataRow, WF_FlowStepMRow.FlowID.OldValue, WF_FlowStepMRow.FlowID.Value))
            db.AddInParameter(dbcmd, "@PKFlowVer", DbType.Int32, IIf(WF_FlowStepMRow.LoadFromDataRow, WF_FlowStepMRow.FlowVer.OldValue, WF_FlowStepMRow.FlowVer.Value))
            db.AddInParameter(dbcmd, "@PKFlowStepID", DbType.String, IIf(WF_FlowStepMRow.LoadFromDataRow, WF_FlowStepMRow.FlowStepID.OldValue, WF_FlowStepMRow.FlowStepID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal WF_FlowStepMRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update WF_FlowStepM Set")
            For i As Integer = 0 To WF_FlowStepMRow.FieldNames.Length - 1
                If Not WF_FlowStepMRow.IsIdentityField(WF_FlowStepMRow.FieldNames(i)) AndAlso WF_FlowStepMRow.IsUpdated(WF_FlowStepMRow.FieldNames(i)) AndAlso WF_FlowStepMRow.CreateUpdateSQL(WF_FlowStepMRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, WF_FlowStepMRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where FlowID = @PKFlowID")
            strSQL.AppendLine("And FlowVer = @PKFlowVer")
            strSQL.AppendLine("And FlowStepID = @PKFlowStepID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If WF_FlowStepMRow.FlowID.Updated Then db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowStepMRow.FlowID.Value)
            If WF_FlowStepMRow.FlowVer.Updated Then db.AddInParameter(dbcmd, "@FlowVer", DbType.Int32, WF_FlowStepMRow.FlowVer.Value)
            If WF_FlowStepMRow.FlowStepID.Updated Then db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, WF_FlowStepMRow.FlowStepID.Value)
            If WF_FlowStepMRow.Description.Updated Then db.AddInParameter(dbcmd, "@Description", DbType.String, WF_FlowStepMRow.Description.Value)
            If WF_FlowStepMRow.MenuTitle.Updated Then db.AddInParameter(dbcmd, "@MenuTitle", DbType.String, WF_FlowStepMRow.MenuTitle.Value)
            If WF_FlowStepMRow.ShowModeMenuTitle.Updated Then db.AddInParameter(dbcmd, "@ShowModeMenuTitle", DbType.String, WF_FlowStepMRow.ShowModeMenuTitle.Value)
            If WF_FlowStepMRow.ProcDay.Updated Then db.AddInParameter(dbcmd, "@ProcDay", DbType.Int32, WF_FlowStepMRow.ProcDay.Value)
            If WF_FlowStepMRow.Intimation.Updated Then db.AddInParameter(dbcmd, "@Intimation", DbType.String, WF_FlowStepMRow.Intimation.Value)
            If WF_FlowStepMRow.AgreeRate.Updated Then db.AddInParameter(dbcmd, "@AgreeRate", DbType.Decimal, WF_FlowStepMRow.AgreeRate.Value)
            If WF_FlowStepMRow.DefaultPage.Updated Then db.AddInParameter(dbcmd, "@DefaultPage", DbType.String, WF_FlowStepMRow.DefaultPage.Value)
            If WF_FlowStepMRow.ShowModePage.Updated Then db.AddInParameter(dbcmd, "@ShowModePage", DbType.String, WF_FlowStepMRow.ShowModePage.Value)
            If WF_FlowStepMRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WF_FlowStepMRow.LastChgID.Value)
            If WF_FlowStepMRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WF_FlowStepMRow.LastChgDate.Value), DBNull.Value, WF_FlowStepMRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKFlowID", DbType.String, IIf(WF_FlowStepMRow.LoadFromDataRow, WF_FlowStepMRow.FlowID.OldValue, WF_FlowStepMRow.FlowID.Value))
            db.AddInParameter(dbcmd, "@PKFlowVer", DbType.Int32, IIf(WF_FlowStepMRow.LoadFromDataRow, WF_FlowStepMRow.FlowVer.OldValue, WF_FlowStepMRow.FlowVer.Value))
            db.AddInParameter(dbcmd, "@PKFlowStepID", DbType.String, IIf(WF_FlowStepMRow.LoadFromDataRow, WF_FlowStepMRow.FlowStepID.OldValue, WF_FlowStepMRow.FlowStepID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal WF_FlowStepMRow As Row()) As Integer
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
                    For Each r As Row In WF_FlowStepMRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update WF_FlowStepM Set")
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

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.FlowID.Updated Then db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                        If r.FlowVer.Updated Then db.AddInParameter(dbcmd, "@FlowVer", DbType.Int32, r.FlowVer.Value)
                        If r.FlowStepID.Updated Then db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, r.FlowStepID.Value)
                        If r.Description.Updated Then db.AddInParameter(dbcmd, "@Description", DbType.String, r.Description.Value)
                        If r.MenuTitle.Updated Then db.AddInParameter(dbcmd, "@MenuTitle", DbType.String, r.MenuTitle.Value)
                        If r.ShowModeMenuTitle.Updated Then db.AddInParameter(dbcmd, "@ShowModeMenuTitle", DbType.String, r.ShowModeMenuTitle.Value)
                        If r.ProcDay.Updated Then db.AddInParameter(dbcmd, "@ProcDay", DbType.Int32, r.ProcDay.Value)
                        If r.Intimation.Updated Then db.AddInParameter(dbcmd, "@Intimation", DbType.String, r.Intimation.Value)
                        If r.AgreeRate.Updated Then db.AddInParameter(dbcmd, "@AgreeRate", DbType.Decimal, r.AgreeRate.Value)
                        If r.DefaultPage.Updated Then db.AddInParameter(dbcmd, "@DefaultPage", DbType.String, r.DefaultPage.Value)
                        If r.ShowModePage.Updated Then db.AddInParameter(dbcmd, "@ShowModePage", DbType.String, r.ShowModePage.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), DBNull.Value, r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKFlowID", DbType.String, IIf(r.LoadFromDataRow, r.FlowID.OldValue, r.FlowID.Value))
                        db.AddInParameter(dbcmd, "@PKFlowVer", DbType.Int32, IIf(r.LoadFromDataRow, r.FlowVer.OldValue, r.FlowVer.Value))
                        db.AddInParameter(dbcmd, "@PKFlowStepID", DbType.String, IIf(r.LoadFromDataRow, r.FlowStepID.OldValue, r.FlowStepID.Value))

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

        Public Function Update(ByVal WF_FlowStepMRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In WF_FlowStepMRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update WF_FlowStepM Set")
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

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.FlowID.Updated Then db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                If r.FlowVer.Updated Then db.AddInParameter(dbcmd, "@FlowVer", DbType.Int32, r.FlowVer.Value)
                If r.FlowStepID.Updated Then db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, r.FlowStepID.Value)
                If r.Description.Updated Then db.AddInParameter(dbcmd, "@Description", DbType.String, r.Description.Value)
                If r.MenuTitle.Updated Then db.AddInParameter(dbcmd, "@MenuTitle", DbType.String, r.MenuTitle.Value)
                If r.ShowModeMenuTitle.Updated Then db.AddInParameter(dbcmd, "@ShowModeMenuTitle", DbType.String, r.ShowModeMenuTitle.Value)
                If r.ProcDay.Updated Then db.AddInParameter(dbcmd, "@ProcDay", DbType.Int32, r.ProcDay.Value)
                If r.Intimation.Updated Then db.AddInParameter(dbcmd, "@Intimation", DbType.String, r.Intimation.Value)
                If r.AgreeRate.Updated Then db.AddInParameter(dbcmd, "@AgreeRate", DbType.Decimal, r.AgreeRate.Value)
                If r.DefaultPage.Updated Then db.AddInParameter(dbcmd, "@DefaultPage", DbType.String, r.DefaultPage.Value)
                If r.ShowModePage.Updated Then db.AddInParameter(dbcmd, "@ShowModePage", DbType.String, r.ShowModePage.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), DBNull.Value, r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKFlowID", DbType.String, IIf(r.LoadFromDataRow, r.FlowID.OldValue, r.FlowID.Value))
                db.AddInParameter(dbcmd, "@PKFlowVer", DbType.Int32, IIf(r.LoadFromDataRow, r.FlowVer.OldValue, r.FlowVer.Value))
                db.AddInParameter(dbcmd, "@PKFlowStepID", DbType.String, IIf(r.LoadFromDataRow, r.FlowStepID.OldValue, r.FlowStepID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal WF_FlowStepMRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From WF_FlowStepM")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowVer = @FlowVer")
            strSQL.AppendLine("And FlowStepID = @FlowStepID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowStepMRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowVer", DbType.Int32, WF_FlowStepMRow.FlowVer.Value)
            db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, WF_FlowStepMRow.FlowStepID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal WF_FlowStepMRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From WF_FlowStepM")
            strSQL.AppendLine("Where FlowID = @FlowID")
            strSQL.AppendLine("And FlowVer = @FlowVer")
            strSQL.AppendLine("And FlowStepID = @FlowStepID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowStepMRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowVer", DbType.Int32, WF_FlowStepMRow.FlowVer.Value)
            db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, WF_FlowStepMRow.FlowStepID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WF_FlowStepM")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal WF_FlowStepMRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into WF_FlowStepM")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowID, FlowVer, FlowStepID, Description, MenuTitle, ShowModeMenuTitle, ProcDay, Intimation,")
            strSQL.AppendLine("    AgreeRate, DefaultPage, ShowModePage, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowID, @FlowVer, @FlowStepID, @Description, @MenuTitle, @ShowModeMenuTitle, @ProcDay, @Intimation,")
            strSQL.AppendLine("    @AgreeRate, @DefaultPage, @ShowModePage, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowStepMRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowVer", DbType.Int32, WF_FlowStepMRow.FlowVer.Value)
            db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, WF_FlowStepMRow.FlowStepID.Value)
            db.AddInParameter(dbcmd, "@Description", DbType.String, WF_FlowStepMRow.Description.Value)
            db.AddInParameter(dbcmd, "@MenuTitle", DbType.String, WF_FlowStepMRow.MenuTitle.Value)
            db.AddInParameter(dbcmd, "@ShowModeMenuTitle", DbType.String, WF_FlowStepMRow.ShowModeMenuTitle.Value)
            db.AddInParameter(dbcmd, "@ProcDay", DbType.Int32, WF_FlowStepMRow.ProcDay.Value)
            db.AddInParameter(dbcmd, "@Intimation", DbType.String, WF_FlowStepMRow.Intimation.Value)
            db.AddInParameter(dbcmd, "@AgreeRate", DbType.Decimal, WF_FlowStepMRow.AgreeRate.Value)
            db.AddInParameter(dbcmd, "@DefaultPage", DbType.String, WF_FlowStepMRow.DefaultPage.Value)
            db.AddInParameter(dbcmd, "@ShowModePage", DbType.String, WF_FlowStepMRow.ShowModePage.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WF_FlowStepMRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WF_FlowStepMRow.LastChgDate.Value), DBNull.Value, WF_FlowStepMRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal WF_FlowStepMRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into WF_FlowStepM")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowID, FlowVer, FlowStepID, Description, MenuTitle, ShowModeMenuTitle, ProcDay, Intimation,")
            strSQL.AppendLine("    AgreeRate, DefaultPage, ShowModePage, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowID, @FlowVer, @FlowStepID, @Description, @MenuTitle, @ShowModeMenuTitle, @ProcDay, @Intimation,")
            strSQL.AppendLine("    @AgreeRate, @DefaultPage, @ShowModePage, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@FlowID", DbType.String, WF_FlowStepMRow.FlowID.Value)
            db.AddInParameter(dbcmd, "@FlowVer", DbType.Int32, WF_FlowStepMRow.FlowVer.Value)
            db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, WF_FlowStepMRow.FlowStepID.Value)
            db.AddInParameter(dbcmd, "@Description", DbType.String, WF_FlowStepMRow.Description.Value)
            db.AddInParameter(dbcmd, "@MenuTitle", DbType.String, WF_FlowStepMRow.MenuTitle.Value)
            db.AddInParameter(dbcmd, "@ShowModeMenuTitle", DbType.String, WF_FlowStepMRow.ShowModeMenuTitle.Value)
            db.AddInParameter(dbcmd, "@ProcDay", DbType.Int32, WF_FlowStepMRow.ProcDay.Value)
            db.AddInParameter(dbcmd, "@Intimation", DbType.String, WF_FlowStepMRow.Intimation.Value)
            db.AddInParameter(dbcmd, "@AgreeRate", DbType.Decimal, WF_FlowStepMRow.AgreeRate.Value)
            db.AddInParameter(dbcmd, "@DefaultPage", DbType.String, WF_FlowStepMRow.DefaultPage.Value)
            db.AddInParameter(dbcmd, "@ShowModePage", DbType.String, WF_FlowStepMRow.ShowModePage.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WF_FlowStepMRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WF_FlowStepMRow.LastChgDate.Value), DBNull.Value, WF_FlowStepMRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal WF_FlowStepMRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into WF_FlowStepM")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowID, FlowVer, FlowStepID, Description, MenuTitle, ShowModeMenuTitle, ProcDay, Intimation,")
            strSQL.AppendLine("    AgreeRate, DefaultPage, ShowModePage, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowID, @FlowVer, @FlowStepID, @Description, @MenuTitle, @ShowModeMenuTitle, @ProcDay, @Intimation,")
            strSQL.AppendLine("    @AgreeRate, @DefaultPage, @ShowModePage, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In WF_FlowStepMRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                        db.AddInParameter(dbcmd, "@FlowVer", DbType.Int32, r.FlowVer.Value)
                        db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, r.FlowStepID.Value)
                        db.AddInParameter(dbcmd, "@Description", DbType.String, r.Description.Value)
                        db.AddInParameter(dbcmd, "@MenuTitle", DbType.String, r.MenuTitle.Value)
                        db.AddInParameter(dbcmd, "@ShowModeMenuTitle", DbType.String, r.ShowModeMenuTitle.Value)
                        db.AddInParameter(dbcmd, "@ProcDay", DbType.Int32, r.ProcDay.Value)
                        db.AddInParameter(dbcmd, "@Intimation", DbType.String, r.Intimation.Value)
                        db.AddInParameter(dbcmd, "@AgreeRate", DbType.Decimal, r.AgreeRate.Value)
                        db.AddInParameter(dbcmd, "@DefaultPage", DbType.String, r.DefaultPage.Value)
                        db.AddInParameter(dbcmd, "@ShowModePage", DbType.String, r.ShowModePage.Value)
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

        Public Function Insert(ByVal WF_FlowStepMRow As Row(), ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into WF_FlowStepM")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    FlowID, FlowVer, FlowStepID, Description, MenuTitle, ShowModeMenuTitle, ProcDay, Intimation,")
            strSQL.AppendLine("    AgreeRate, DefaultPage, ShowModePage, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @FlowID, @FlowVer, @FlowStepID, @Description, @MenuTitle, @ShowModeMenuTitle, @ProcDay, @Intimation,")
            strSQL.AppendLine("    @AgreeRate, @DefaultPage, @ShowModePage, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In WF_FlowStepMRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@FlowID", DbType.String, r.FlowID.Value)
                db.AddInParameter(dbcmd, "@FlowVer", DbType.Int32, r.FlowVer.Value)
                db.AddInParameter(dbcmd, "@FlowStepID", DbType.String, r.FlowStepID.Value)
                db.AddInParameter(dbcmd, "@Description", DbType.String, r.Description.Value)
                db.AddInParameter(dbcmd, "@MenuTitle", DbType.String, r.MenuTitle.Value)
                db.AddInParameter(dbcmd, "@ShowModeMenuTitle", DbType.String, r.ShowModeMenuTitle.Value)
                db.AddInParameter(dbcmd, "@ProcDay", DbType.Int32, r.ProcDay.Value)
                db.AddInParameter(dbcmd, "@Intimation", DbType.String, r.Intimation.Value)
                db.AddInParameter(dbcmd, "@AgreeRate", DbType.Decimal, r.AgreeRate.Value)
                db.AddInParameter(dbcmd, "@DefaultPage", DbType.String, r.DefaultPage.Value)
                db.AddInParameter(dbcmd, "@ShowModePage", DbType.String, r.ShowModePage.Value)
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

