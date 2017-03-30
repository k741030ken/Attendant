'****************************************************************
' Table:WF_FlowMenu
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

Namespace beWF_FlowMenu
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "MenuName", "ParentMenu", "OrderSeq", "FunID", "LinkPage", "LinkPara", "LinkParaExt", "LinkParaSql", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(Decimal), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "MenuName", "ParentMenu" }

        Public ReadOnly Property Rows() As beWF_FlowMenu.Rows 
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
        Public Sub Transfer2Row(WF_FlowMenuTable As DataTable)
            For Each dr As DataRow In WF_FlowMenuTable.Rows
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

                dr(m_Rows(i).MenuName.FieldName) = m_Rows(i).MenuName.Value
                dr(m_Rows(i).ParentMenu.FieldName) = m_Rows(i).ParentMenu.Value
                dr(m_Rows(i).OrderSeq.FieldName) = m_Rows(i).OrderSeq.Value
                dr(m_Rows(i).FunID.FieldName) = m_Rows(i).FunID.Value
                dr(m_Rows(i).LinkPage.FieldName) = m_Rows(i).LinkPage.Value
                dr(m_Rows(i).LinkPara.FieldName) = m_Rows(i).LinkPara.Value
                dr(m_Rows(i).LinkParaExt.FieldName) = m_Rows(i).LinkParaExt.Value
                dr(m_Rows(i).LinkParaSql.FieldName) = m_Rows(i).LinkParaSql.Value
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

        Public Sub Add(WF_FlowMenuRow As Row)
            m_Rows.Add(WF_FlowMenuRow)
        End Sub

        Public Sub Remove(WF_FlowMenuRow As Row)
            If m_Rows.IndexOf(WF_FlowMenuRow) >= 0 Then
                m_Rows.Remove(WF_FlowMenuRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_MenuName As Field(Of String) = new Field(Of String)("MenuName", true)
        Private FI_ParentMenu As Field(Of String) = new Field(Of String)("ParentMenu", true)
        Private FI_OrderSeq As Field(Of Decimal) = new Field(Of Decimal)("OrderSeq", true)
        Private FI_FunID As Field(Of String) = new Field(Of String)("FunID", true)
        Private FI_LinkPage As Field(Of String) = new Field(Of String)("LinkPage", true)
        Private FI_LinkPara As Field(Of String) = new Field(Of String)("LinkPara", true)
        Private FI_LinkParaExt As Field(Of String) = new Field(Of String)("LinkParaExt", true)
        Private FI_LinkParaSql As Field(Of String) = new Field(Of String)("LinkParaSql", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "MenuName", "ParentMenu", "OrderSeq", "FunID", "LinkPage", "LinkPara", "LinkParaExt", "LinkParaSql", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "MenuName", "ParentMenu" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "MenuName"
                    Return FI_MenuName.Value
                Case "ParentMenu"
                    Return FI_ParentMenu.Value
                Case "OrderSeq"
                    Return FI_OrderSeq.Value
                Case "FunID"
                    Return FI_FunID.Value
                Case "LinkPage"
                    Return FI_LinkPage.Value
                Case "LinkPara"
                    Return FI_LinkPara.Value
                Case "LinkParaExt"
                    Return FI_LinkParaExt.Value
                Case "LinkParaSql"
                    Return FI_LinkParaSql.Value
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
                Case "MenuName"
                    FI_MenuName.SetValue(value)
                Case "ParentMenu"
                    FI_ParentMenu.SetValue(value)
                Case "OrderSeq"
                    FI_OrderSeq.SetValue(value)
                Case "FunID"
                    FI_FunID.SetValue(value)
                Case "LinkPage"
                    FI_LinkPage.SetValue(value)
                Case "LinkPara"
                    FI_LinkPara.SetValue(value)
                Case "LinkParaExt"
                    FI_LinkParaExt.SetValue(value)
                Case "LinkParaSql"
                    FI_LinkParaSql.SetValue(value)
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
                Case "MenuName"
                    return FI_MenuName.Updated
                Case "ParentMenu"
                    return FI_ParentMenu.Updated
                Case "OrderSeq"
                    return FI_OrderSeq.Updated
                Case "FunID"
                    return FI_FunID.Updated
                Case "LinkPage"
                    return FI_LinkPage.Updated
                Case "LinkPara"
                    return FI_LinkPara.Updated
                Case "LinkParaExt"
                    return FI_LinkParaExt.Updated
                Case "LinkParaSql"
                    return FI_LinkParaSql.Updated
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
                Case "MenuName"
                    return FI_MenuName.CreateUpdateSQL
                Case "ParentMenu"
                    return FI_ParentMenu.CreateUpdateSQL
                Case "OrderSeq"
                    return FI_OrderSeq.CreateUpdateSQL
                Case "FunID"
                    return FI_FunID.CreateUpdateSQL
                Case "LinkPage"
                    return FI_LinkPage.CreateUpdateSQL
                Case "LinkPara"
                    return FI_LinkPara.CreateUpdateSQL
                Case "LinkParaExt"
                    return FI_LinkParaExt.CreateUpdateSQL
                Case "LinkParaSql"
                    return FI_LinkParaSql.CreateUpdateSQL
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
            FI_MenuName.SetInitValue("")
            FI_ParentMenu.SetInitValue("")
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_MenuName.SetInitValue(dr("MenuName"))
            FI_ParentMenu.SetInitValue(dr("ParentMenu"))
            FI_OrderSeq.SetInitValue(dr("OrderSeq"))
            FI_FunID.SetInitValue(dr("FunID"))
            FI_LinkPage.SetInitValue(dr("LinkPage"))
            FI_LinkPara.SetInitValue(dr("LinkPara"))
            FI_LinkParaExt.SetInitValue(dr("LinkParaExt"))
            FI_LinkParaSql.SetInitValue(dr("LinkParaSql"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_MenuName.Updated = False
            FI_ParentMenu.Updated = False
            FI_OrderSeq.Updated = False
            FI_FunID.Updated = False
            FI_LinkPage.Updated = False
            FI_LinkPara.Updated = False
            FI_LinkParaExt.Updated = False
            FI_LinkParaSql.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
        End Sub

        Public ReadOnly Property MenuName As Field(Of String) 
            Get
                Return FI_MenuName
            End Get
        End Property

        Public ReadOnly Property ParentMenu As Field(Of String) 
            Get
                Return FI_ParentMenu
            End Get
        End Property

        Public ReadOnly Property OrderSeq As Field(Of Decimal) 
            Get
                Return FI_OrderSeq
            End Get
        End Property

        Public ReadOnly Property FunID As Field(Of String) 
            Get
                Return FI_FunID
            End Get
        End Property

        Public ReadOnly Property LinkPage As Field(Of String) 
            Get
                Return FI_LinkPage
            End Get
        End Property

        Public ReadOnly Property LinkPara As Field(Of String) 
            Get
                Return FI_LinkPara
            End Get
        End Property

        Public ReadOnly Property LinkParaExt As Field(Of String) 
            Get
                Return FI_LinkParaExt
            End Get
        End Property

        Public ReadOnly Property LinkParaSql As Field(Of String) 
            Get
                Return FI_LinkParaSql
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
        Public Function DeleteRowByPrimaryKey(ByVal WF_FlowMenuRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From WF_FlowMenu")
            strSQL.AppendLine("Where MenuName = @MenuName")
            strSQL.AppendLine("And ParentMenu = @ParentMenu")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@MenuName", DbType.String, WF_FlowMenuRow.MenuName.Value)
            db.AddInParameter(dbcmd, "@ParentMenu", DbType.String, WF_FlowMenuRow.ParentMenu.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal WF_FlowMenuRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From WF_FlowMenu")
            strSQL.AppendLine("Where MenuName = @MenuName")
            strSQL.AppendLine("And ParentMenu = @ParentMenu")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@MenuName", DbType.String, WF_FlowMenuRow.MenuName.Value)
            db.AddInParameter(dbcmd, "@ParentMenu", DbType.String, WF_FlowMenuRow.ParentMenu.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal WF_FlowMenuRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From WF_FlowMenu")
            strSQL.AppendLine("Where MenuName = @MenuName")
            strSQL.AppendLine("And ParentMenu = @ParentMenu")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In WF_FlowMenuRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@MenuName", DbType.String, r.MenuName.Value)
                        db.AddInParameter(dbcmd, "@ParentMenu", DbType.String, r.ParentMenu.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal WF_FlowMenuRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From WF_FlowMenu")
            strSQL.AppendLine("Where MenuName = @MenuName")
            strSQL.AppendLine("And ParentMenu = @ParentMenu")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In WF_FlowMenuRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@MenuName", DbType.String, r.MenuName.Value)
                db.AddInParameter(dbcmd, "@ParentMenu", DbType.String, r.ParentMenu.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal WF_FlowMenuRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WF_FlowMenu")
            strSQL.AppendLine("Where MenuName = @MenuName")
            strSQL.AppendLine("And ParentMenu = @ParentMenu")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@MenuName", DbType.String, WF_FlowMenuRow.MenuName.Value)
            db.AddInParameter(dbcmd, "@ParentMenu", DbType.String, WF_FlowMenuRow.ParentMenu.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(WF_FlowMenuRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WF_FlowMenu")
            strSQL.AppendLine("Where MenuName = @MenuName")
            strSQL.AppendLine("And ParentMenu = @ParentMenu")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@MenuName", DbType.String, WF_FlowMenuRow.MenuName.Value)
            db.AddInParameter(dbcmd, "@ParentMenu", DbType.String, WF_FlowMenuRow.ParentMenu.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal WF_FlowMenuRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update WF_FlowMenu Set")
            For i As Integer = 0 To WF_FlowMenuRow.FieldNames.Length - 1
                If Not WF_FlowMenuRow.IsIdentityField(WF_FlowMenuRow.FieldNames(i)) AndAlso WF_FlowMenuRow.IsUpdated(WF_FlowMenuRow.FieldNames(i)) AndAlso WF_FlowMenuRow.CreateUpdateSQL(WF_FlowMenuRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, WF_FlowMenuRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where MenuName = @PKMenuName")
            strSQL.AppendLine("And ParentMenu = @PKParentMenu")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If WF_FlowMenuRow.MenuName.Updated Then db.AddInParameter(dbcmd, "@MenuName", DbType.String, WF_FlowMenuRow.MenuName.Value)
            If WF_FlowMenuRow.ParentMenu.Updated Then db.AddInParameter(dbcmd, "@ParentMenu", DbType.String, WF_FlowMenuRow.ParentMenu.Value)
            If WF_FlowMenuRow.OrderSeq.Updated Then db.AddInParameter(dbcmd, "@OrderSeq", DbType.Decimal, WF_FlowMenuRow.OrderSeq.Value)
            If WF_FlowMenuRow.FunID.Updated Then db.AddInParameter(dbcmd, "@FunID", DbType.String, WF_FlowMenuRow.FunID.Value)
            If WF_FlowMenuRow.LinkPage.Updated Then db.AddInParameter(dbcmd, "@LinkPage", DbType.String, WF_FlowMenuRow.LinkPage.Value)
            If WF_FlowMenuRow.LinkPara.Updated Then db.AddInParameter(dbcmd, "@LinkPara", DbType.String, WF_FlowMenuRow.LinkPara.Value)
            If WF_FlowMenuRow.LinkParaExt.Updated Then db.AddInParameter(dbcmd, "@LinkParaExt", DbType.String, WF_FlowMenuRow.LinkParaExt.Value)
            If WF_FlowMenuRow.LinkParaSql.Updated Then db.AddInParameter(dbcmd, "@LinkParaSql", DbType.String, WF_FlowMenuRow.LinkParaSql.Value)
            If WF_FlowMenuRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WF_FlowMenuRow.LastChgID.Value)
            If WF_FlowMenuRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WF_FlowMenuRow.LastChgDate.Value), DBNull.Value, WF_FlowMenuRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKMenuName", DbType.String, IIf(WF_FlowMenuRow.LoadFromDataRow, WF_FlowMenuRow.MenuName.OldValue, WF_FlowMenuRow.MenuName.Value))
            db.AddInParameter(dbcmd, "@PKParentMenu", DbType.String, IIf(WF_FlowMenuRow.LoadFromDataRow, WF_FlowMenuRow.ParentMenu.OldValue, WF_FlowMenuRow.ParentMenu.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal WF_FlowMenuRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update WF_FlowMenu Set")
            For i As Integer = 0 To WF_FlowMenuRow.FieldNames.Length - 1
                If Not WF_FlowMenuRow.IsIdentityField(WF_FlowMenuRow.FieldNames(i)) AndAlso WF_FlowMenuRow.IsUpdated(WF_FlowMenuRow.FieldNames(i)) AndAlso WF_FlowMenuRow.CreateUpdateSQL(WF_FlowMenuRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, WF_FlowMenuRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where MenuName = @PKMenuName")
            strSQL.AppendLine("And ParentMenu = @PKParentMenu")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If WF_FlowMenuRow.MenuName.Updated Then db.AddInParameter(dbcmd, "@MenuName", DbType.String, WF_FlowMenuRow.MenuName.Value)
            If WF_FlowMenuRow.ParentMenu.Updated Then db.AddInParameter(dbcmd, "@ParentMenu", DbType.String, WF_FlowMenuRow.ParentMenu.Value)
            If WF_FlowMenuRow.OrderSeq.Updated Then db.AddInParameter(dbcmd, "@OrderSeq", DbType.Decimal, WF_FlowMenuRow.OrderSeq.Value)
            If WF_FlowMenuRow.FunID.Updated Then db.AddInParameter(dbcmd, "@FunID", DbType.String, WF_FlowMenuRow.FunID.Value)
            If WF_FlowMenuRow.LinkPage.Updated Then db.AddInParameter(dbcmd, "@LinkPage", DbType.String, WF_FlowMenuRow.LinkPage.Value)
            If WF_FlowMenuRow.LinkPara.Updated Then db.AddInParameter(dbcmd, "@LinkPara", DbType.String, WF_FlowMenuRow.LinkPara.Value)
            If WF_FlowMenuRow.LinkParaExt.Updated Then db.AddInParameter(dbcmd, "@LinkParaExt", DbType.String, WF_FlowMenuRow.LinkParaExt.Value)
            If WF_FlowMenuRow.LinkParaSql.Updated Then db.AddInParameter(dbcmd, "@LinkParaSql", DbType.String, WF_FlowMenuRow.LinkParaSql.Value)
            If WF_FlowMenuRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WF_FlowMenuRow.LastChgID.Value)
            If WF_FlowMenuRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WF_FlowMenuRow.LastChgDate.Value), DBNull.Value, WF_FlowMenuRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKMenuName", DbType.String, IIf(WF_FlowMenuRow.LoadFromDataRow, WF_FlowMenuRow.MenuName.OldValue, WF_FlowMenuRow.MenuName.Value))
            db.AddInParameter(dbcmd, "@PKParentMenu", DbType.String, IIf(WF_FlowMenuRow.LoadFromDataRow, WF_FlowMenuRow.ParentMenu.OldValue, WF_FlowMenuRow.ParentMenu.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal WF_FlowMenuRow As Row()) As Integer
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
                    For Each r As Row In WF_FlowMenuRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update WF_FlowMenu Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where MenuName = @PKMenuName")
                        strSQL.AppendLine("And ParentMenu = @PKParentMenu")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.MenuName.Updated Then db.AddInParameter(dbcmd, "@MenuName", DbType.String, r.MenuName.Value)
                        If r.ParentMenu.Updated Then db.AddInParameter(dbcmd, "@ParentMenu", DbType.String, r.ParentMenu.Value)
                        If r.OrderSeq.Updated Then db.AddInParameter(dbcmd, "@OrderSeq", DbType.Decimal, r.OrderSeq.Value)
                        If r.FunID.Updated Then db.AddInParameter(dbcmd, "@FunID", DbType.String, r.FunID.Value)
                        If r.LinkPage.Updated Then db.AddInParameter(dbcmd, "@LinkPage", DbType.String, r.LinkPage.Value)
                        If r.LinkPara.Updated Then db.AddInParameter(dbcmd, "@LinkPara", DbType.String, r.LinkPara.Value)
                        If r.LinkParaExt.Updated Then db.AddInParameter(dbcmd, "@LinkParaExt", DbType.String, r.LinkParaExt.Value)
                        If r.LinkParaSql.Updated Then db.AddInParameter(dbcmd, "@LinkParaSql", DbType.String, r.LinkParaSql.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), DBNull.Value, r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKMenuName", DbType.String, IIf(r.LoadFromDataRow, r.MenuName.OldValue, r.MenuName.Value))
                        db.AddInParameter(dbcmd, "@PKParentMenu", DbType.String, IIf(r.LoadFromDataRow, r.ParentMenu.OldValue, r.ParentMenu.Value))

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

        Public Function Update(ByVal WF_FlowMenuRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In WF_FlowMenuRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update WF_FlowMenu Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where MenuName = @PKMenuName")
                strSQL.AppendLine("And ParentMenu = @PKParentMenu")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.MenuName.Updated Then db.AddInParameter(dbcmd, "@MenuName", DbType.String, r.MenuName.Value)
                If r.ParentMenu.Updated Then db.AddInParameter(dbcmd, "@ParentMenu", DbType.String, r.ParentMenu.Value)
                If r.OrderSeq.Updated Then db.AddInParameter(dbcmd, "@OrderSeq", DbType.Decimal, r.OrderSeq.Value)
                If r.FunID.Updated Then db.AddInParameter(dbcmd, "@FunID", DbType.String, r.FunID.Value)
                If r.LinkPage.Updated Then db.AddInParameter(dbcmd, "@LinkPage", DbType.String, r.LinkPage.Value)
                If r.LinkPara.Updated Then db.AddInParameter(dbcmd, "@LinkPara", DbType.String, r.LinkPara.Value)
                If r.LinkParaExt.Updated Then db.AddInParameter(dbcmd, "@LinkParaExt", DbType.String, r.LinkParaExt.Value)
                If r.LinkParaSql.Updated Then db.AddInParameter(dbcmd, "@LinkParaSql", DbType.String, r.LinkParaSql.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), DBNull.Value, r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKMenuName", DbType.String, IIf(r.LoadFromDataRow, r.MenuName.OldValue, r.MenuName.Value))
                db.AddInParameter(dbcmd, "@PKParentMenu", DbType.String, IIf(r.LoadFromDataRow, r.ParentMenu.OldValue, r.ParentMenu.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal WF_FlowMenuRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From WF_FlowMenu")
            strSQL.AppendLine("Where MenuName = @MenuName")
            strSQL.AppendLine("And ParentMenu = @ParentMenu")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@MenuName", DbType.String, WF_FlowMenuRow.MenuName.Value)
            db.AddInParameter(dbcmd, "@ParentMenu", DbType.String, WF_FlowMenuRow.ParentMenu.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal WF_FlowMenuRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From WF_FlowMenu")
            strSQL.AppendLine("Where MenuName = @MenuName")
            strSQL.AppendLine("And ParentMenu = @ParentMenu")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@MenuName", DbType.String, WF_FlowMenuRow.MenuName.Value)
            db.AddInParameter(dbcmd, "@ParentMenu", DbType.String, WF_FlowMenuRow.ParentMenu.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From WF_FlowMenu")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal WF_FlowMenuRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into WF_FlowMenu")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    MenuName, ParentMenu, OrderSeq, FunID, LinkPage, LinkPara, LinkParaExt, LinkParaSql,")
            strSQL.AppendLine("    LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @MenuName, @ParentMenu, @OrderSeq, @FunID, @LinkPage, @LinkPara, @LinkParaExt, @LinkParaSql,")
            strSQL.AppendLine("    @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@MenuName", DbType.String, WF_FlowMenuRow.MenuName.Value)
            db.AddInParameter(dbcmd, "@ParentMenu", DbType.String, WF_FlowMenuRow.ParentMenu.Value)
            db.AddInParameter(dbcmd, "@OrderSeq", DbType.Decimal, WF_FlowMenuRow.OrderSeq.Value)
            db.AddInParameter(dbcmd, "@FunID", DbType.String, WF_FlowMenuRow.FunID.Value)
            db.AddInParameter(dbcmd, "@LinkPage", DbType.String, WF_FlowMenuRow.LinkPage.Value)
            db.AddInParameter(dbcmd, "@LinkPara", DbType.String, WF_FlowMenuRow.LinkPara.Value)
            db.AddInParameter(dbcmd, "@LinkParaExt", DbType.String, WF_FlowMenuRow.LinkParaExt.Value)
            db.AddInParameter(dbcmd, "@LinkParaSql", DbType.String, WF_FlowMenuRow.LinkParaSql.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WF_FlowMenuRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WF_FlowMenuRow.LastChgDate.Value), DBNull.Value, WF_FlowMenuRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal WF_FlowMenuRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into WF_FlowMenu")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    MenuName, ParentMenu, OrderSeq, FunID, LinkPage, LinkPara, LinkParaExt, LinkParaSql,")
            strSQL.AppendLine("    LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @MenuName, @ParentMenu, @OrderSeq, @FunID, @LinkPage, @LinkPara, @LinkParaExt, @LinkParaSql,")
            strSQL.AppendLine("    @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@MenuName", DbType.String, WF_FlowMenuRow.MenuName.Value)
            db.AddInParameter(dbcmd, "@ParentMenu", DbType.String, WF_FlowMenuRow.ParentMenu.Value)
            db.AddInParameter(dbcmd, "@OrderSeq", DbType.Decimal, WF_FlowMenuRow.OrderSeq.Value)
            db.AddInParameter(dbcmd, "@FunID", DbType.String, WF_FlowMenuRow.FunID.Value)
            db.AddInParameter(dbcmd, "@LinkPage", DbType.String, WF_FlowMenuRow.LinkPage.Value)
            db.AddInParameter(dbcmd, "@LinkPara", DbType.String, WF_FlowMenuRow.LinkPara.Value)
            db.AddInParameter(dbcmd, "@LinkParaExt", DbType.String, WF_FlowMenuRow.LinkParaExt.Value)
            db.AddInParameter(dbcmd, "@LinkParaSql", DbType.String, WF_FlowMenuRow.LinkParaSql.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, WF_FlowMenuRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(WF_FlowMenuRow.LastChgDate.Value), DBNull.Value, WF_FlowMenuRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal WF_FlowMenuRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into WF_FlowMenu")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    MenuName, ParentMenu, OrderSeq, FunID, LinkPage, LinkPara, LinkParaExt, LinkParaSql,")
            strSQL.AppendLine("    LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @MenuName, @ParentMenu, @OrderSeq, @FunID, @LinkPage, @LinkPara, @LinkParaExt, @LinkParaSql,")
            strSQL.AppendLine("    @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In WF_FlowMenuRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@MenuName", DbType.String, r.MenuName.Value)
                        db.AddInParameter(dbcmd, "@ParentMenu", DbType.String, r.ParentMenu.Value)
                        db.AddInParameter(dbcmd, "@OrderSeq", DbType.Decimal, r.OrderSeq.Value)
                        db.AddInParameter(dbcmd, "@FunID", DbType.String, r.FunID.Value)
                        db.AddInParameter(dbcmd, "@LinkPage", DbType.String, r.LinkPage.Value)
                        db.AddInParameter(dbcmd, "@LinkPara", DbType.String, r.LinkPara.Value)
                        db.AddInParameter(dbcmd, "@LinkParaExt", DbType.String, r.LinkParaExt.Value)
                        db.AddInParameter(dbcmd, "@LinkParaSql", DbType.String, r.LinkParaSql.Value)
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

        Public Function Insert(ByVal WF_FlowMenuRow As Row(), ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into WF_FlowMenu")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    MenuName, ParentMenu, OrderSeq, FunID, LinkPage, LinkPara, LinkParaExt, LinkParaSql,")
            strSQL.AppendLine("    LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @MenuName, @ParentMenu, @OrderSeq, @FunID, @LinkPage, @LinkPara, @LinkParaExt, @LinkParaSql,")
            strSQL.AppendLine("    @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In WF_FlowMenuRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@MenuName", DbType.String, r.MenuName.Value)
                db.AddInParameter(dbcmd, "@ParentMenu", DbType.String, r.ParentMenu.Value)
                db.AddInParameter(dbcmd, "@OrderSeq", DbType.Decimal, r.OrderSeq.Value)
                db.AddInParameter(dbcmd, "@FunID", DbType.String, r.FunID.Value)
                db.AddInParameter(dbcmd, "@LinkPage", DbType.String, r.LinkPage.Value)
                db.AddInParameter(dbcmd, "@LinkPara", DbType.String, r.LinkPara.Value)
                db.AddInParameter(dbcmd, "@LinkParaExt", DbType.String, r.LinkParaExt.Value)
                db.AddInParameter(dbcmd, "@LinkParaSql", DbType.String, r.LinkParaSql.Value)
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

