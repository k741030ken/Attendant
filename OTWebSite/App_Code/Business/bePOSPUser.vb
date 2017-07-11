'****************************************************************
' Subject：打卡PO1200與公出OB1200的特殊人員設定DB method
' Created Date: 2017.07.11
' Author：Jason
' Table：POSPUser
' Note：AppCodeGen重新產生記得比對差異，有手動修改過
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace bePOSPUser
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "EmpID", "Category", "OrgList", "OrgFlowList", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "CompID", "EmpID", "Category" }

        Public ReadOnly Property Rows() As bePOSPUser.Rows 
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
        Public Sub Transfer2Row(POSPUserTable As DataTable)
            For Each dr As DataRow In POSPUserTable.Rows
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

                dr(m_Rows(i).CompID.FieldName) = m_Rows(i).CompID.Value
                dr(m_Rows(i).EmpID.FieldName) = m_Rows(i).EmpID.Value
                dr(m_Rows(i).Category.FieldName) = m_Rows(i).Category.Value
                dr(m_Rows(i).OrgList.FieldName) = m_Rows(i).OrgList.Value
                dr(m_Rows(i).OrgFlowList.FieldName) = m_Rows(i).OrgFlowList.Value
                dr(m_Rows(i).LastChgComp.FieldName) = m_Rows(i).LastChgComp.Value
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

        Public Sub Add(POSPUserRow As Row)
            m_Rows.Add(POSPUserRow)
        End Sub

        Public Sub Remove(POSPUserRow As Row)
            If m_Rows.IndexOf(POSPUserRow) >= 0 Then
                m_Rows.Remove(POSPUserRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_EmpID As Field(Of String) = new Field(Of String)("EmpID", true)
        Private FI_Category As Field(Of String) = new Field(Of String)("Category", true)
        Private FI_OrgList As Field(Of String) = new Field(Of String)("OrgList", true)
        Private FI_OrgFlowList As Field(Of String) = new Field(Of String)("OrgFlowList", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "CompID", "EmpID", "Category", "OrgList", "OrgFlowList", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "CompID", "EmpID", "Category" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CompID"
                    Return FI_CompID.Value
                Case "EmpID"
                    Return FI_EmpID.Value
                Case "Category"
                    Return FI_Category.Value
                Case "OrgList"
                    Return FI_OrgList.Value
                Case "OrgFlowList"
                    Return FI_OrgFlowList.Value
                Case "LastChgComp"
                    Return FI_LastChgComp.Value
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
                Case "CompID"
                    FI_CompID.SetValue(value)
                Case "EmpID"
                    FI_EmpID.SetValue(value)
                Case "Category"
                    FI_Category.SetValue(value)
                Case "OrgList"
                    FI_OrgList.SetValue(value)
                Case "OrgFlowList"
                    FI_OrgFlowList.SetValue(value)
                Case "LastChgComp"
                    FI_LastChgComp.SetValue(value)
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
                Case "CompID"
                    return FI_CompID.Updated
                Case "EmpID"
                    return FI_EmpID.Updated
                Case "Category"
                    return FI_Category.Updated
                Case "OrgList"
                    return FI_OrgList.Updated
                Case "OrgFlowList"
                    return FI_OrgFlowList.Updated
                Case "LastChgComp"
                    return FI_LastChgComp.Updated
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
                Case "CompID"
                    return FI_CompID.CreateUpdateSQL
                Case "EmpID"
                    return FI_EmpID.CreateUpdateSQL
                Case "Category"
                    return FI_Category.CreateUpdateSQL
                Case "OrgList"
                    return FI_OrgList.CreateUpdateSQL
                Case "OrgFlowList"
                    return FI_OrgFlowList.CreateUpdateSQL
                Case "LastChgComp"
                    return FI_LastChgComp.CreateUpdateSQL
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
            FI_CompID.SetInitValue("")
            FI_EmpID.SetInitValue("")
            FI_Category.SetInitValue("")
            FI_OrgList.SetInitValue("")
            FI_OrgFlowList.SetInitValue("")
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CompID.SetInitValue(dr("CompID"))
            FI_EmpID.SetInitValue(dr("EmpID"))
            FI_Category.SetInitValue(dr("Category"))
            FI_OrgList.SetInitValue(dr("OrgList"))
            FI_OrgFlowList.SetInitValue(dr("OrgFlowList"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CompID.Updated = False
            FI_EmpID.Updated = False
            FI_Category.Updated = False
            FI_OrgList.Updated = False
            FI_OrgFlowList.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
        End Sub

        Public ReadOnly Property CompID As Field(Of String) 
            Get
                Return FI_CompID
            End Get
        End Property

        Public ReadOnly Property EmpID As Field(Of String) 
            Get
                Return FI_EmpID
            End Get
        End Property

        Public ReadOnly Property Category As Field(Of String) 
            Get
                Return FI_Category
            End Get
        End Property

        Public ReadOnly Property OrgList As Field(Of String) 
            Get
                Return FI_OrgList
            End Get
        End Property

        Public ReadOnly Property OrgFlowList As Field(Of String) 
            Get
                Return FI_OrgFlowList
            End Get
        End Property

        Public ReadOnly Property LastChgComp As Field(Of String) 
            Get
                Return FI_LastChgComp
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

        Private Property eHRMSDB As String
            Get
                Dim result As String = ConfigurationManager.AppSettings("eHRMSDB")
                If String.IsNullOrEmpty(result) Then
                    result = "eHRMSDB"
                End If
                Return result
            End Get
            Set(ByVal value As String)

            End Set
        End Property

        Private Property AattendantDB As String
            Get
                Dim result As String = ConfigurationManager.AppSettings("AattendantDB")
                If String.IsNullOrEmpty(result) Then
                    result = "AattendantDB"
                End If
                Return result
            End Get
            Set(ByVal value As String)

            End Set
        End Property

        Public Function DeleteRowByPrimaryKey(ByVal POSPUserRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From POSPUser")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And Category = @Category")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, POSPUserRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, POSPUserRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@Category", DbType.String, POSPUserRow.Category.Value)

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal POSPUserRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From POSPUser")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And Category = @Category")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, POSPUserRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, POSPUserRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@Category", DbType.String, POSPUserRow.Category.Value)

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal POSPUserRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From POSPUser")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And Category = @Category")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In POSPUserRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@Category", DbType.String, r.Category.Value)

                        intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
                    Next
                    tran.Commit()
                    inTrans = False
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

        Public Function DeleteRowByPrimaryKey(ByVal POSPUserRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From POSPUser")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And Category = @Category")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In POSPUserRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@Category", DbType.String, r.Category.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function Query(ByVal POSPUserRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine(" SELECT po.CompID, po.EmpID, p.NameN, po.Category, po.OrgList, po.OrgFlowList, po.LastChgComp, po.LastChgID ")
            strSQL.AppendLine(" , CASE WHEN CONVERT(CHAR(10), po.LastChgDate, 111) = '1900/01/01' THEN ''	ELSE CONVERT(VARCHAR, po.LastChgDate, 120) END AS LastChgDate ")
            strSQL.AppendLine(" FROM POSPUser po ")
            strSQL.AppendLine(" LEFT JOIN [" + eHRMSDB + "].[dbo].[Personal] p ON po.CompID = p.CompID AND po.EmpID = p.EmpID ")
            strSQL.AppendLine(" WHERE 1 = 1 ")
            strSQL.AppendLine(" AND po.Category = @Category ")
            strSQL.AppendLine(" AND po.CompID =  @CompID ")
            strSQL.AppendLine(" ORDER BY po.CompID, po.EmpID  ")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, POSPUserRow.CompID.Value)
            db.AddInParameter(dbcmd, "@Category", DbType.String, POSPUserRow.Category.Value)
            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(ByVal POSPUserRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine(" SELECT po.CompID, po.EmpID, p.NameN, po.Category, po.OrgList, po.OrgFlowList, po.LastChgComp, po.LastChgID ")
            strSQL.AppendLine(" , CASE WHEN CONVERT(CHAR(10), po.LastChgDate, 111) = '1900/01/01' THEN ''	ELSE CONVERT(VARCHAR, po.LastChgDate, 120) END AS LastChgDate ")
            strSQL.AppendLine(" FROM POSPUser po ")
            strSQL.AppendLine(" LEFT JOIN [" + eHRMSDB + "].[dbo].[Personal] p ON po.CompID = p.CompID AND po.EmpID = p.EmpID ")
            strSQL.AppendLine(" WHERE 1 = 1 ")
            strSQL.AppendLine(" AND po.Category = @Category ")
            strSQL.AppendLine(" AND po.CompID =  @CompID ")
            strSQL.AppendLine(" AND po.EmpID = @EmpID ")
            strSQL.AppendLine(" ORDER BY po.CompID, po.EmpID  ")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, POSPUserRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, POSPUserRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@Category", DbType.String, POSPUserRow.Category.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(POSPUserRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine(" SELECT po.CompID, po.EmpID, p.NameN, po.Category, po.OrgList, po.OrgFlowList, po.LastChgComp, po.LastChgID ")
            strSQL.AppendLine(" , CASE WHEN CONVERT(CHAR(10), po.LastChgDate, 111) = '1900/01/01' THEN ''	ELSE CONVERT(VARCHAR, po.LastChgDate, 120) END AS LastChgDate ")
            strSQL.AppendLine(" FROM POSPUser po ")
            strSQL.AppendLine(" LEFT JOIN [" + eHRMSDB + "].[dbo].[Personal] p ON po.CompID = p.CompID AND po.EmpID = p.EmpID ")
            strSQL.AppendLine(" WHERE 1 = 1 ")
            strSQL.AppendLine(" AND po.Category = @Category ")
            strSQL.AppendLine(" AND po.CompID =  @CompID ")
            strSQL.AppendLine(" AND po.EmpID = @EmpID ")
            strSQL.AppendLine(" ORDER BY po.CompID, po.EmpID  ")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, POSPUserRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, POSPUserRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@Category", DbType.String, POSPUserRow.Category.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal POSPUserRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update POSPUser Set")
            For i As Integer = 0 To POSPUserRow.FieldNames.Length - 1
                If Not POSPUserRow.IsIdentityField(POSPUserRow.FieldNames(i)) AndAlso POSPUserRow.IsUpdated(POSPUserRow.FieldNames(i)) AndAlso POSPUserRow.CreateUpdateSQL(POSPUserRow.FieldNames(i)) Then
                    strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, POSPUserRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And EmpID = @PKEmpID")
            strSQL.AppendLine("And Category = @PKCategory")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If POSPUserRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, POSPUserRow.CompID.Value)
            If POSPUserRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, POSPUserRow.EmpID.Value)
            If POSPUserRow.Category.Updated Then db.AddInParameter(dbcmd, "@Category", DbType.String, POSPUserRow.Category.Value)
            If POSPUserRow.OrgList.Updated Then db.AddInParameter(dbcmd, "@OrgList", DbType.String, POSPUserRow.OrgList.Value)
            If POSPUserRow.OrgFlowList.Updated Then db.AddInParameter(dbcmd, "@OrgFlowList", DbType.String, POSPUserRow.OrgFlowList.Value)
            If POSPUserRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, POSPUserRow.LastChgComp.Value)
            If POSPUserRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, POSPUserRow.LastChgID.Value)
            If POSPUserRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(POSPUserRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), POSPUserRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(POSPUserRow.LoadFromDataRow, POSPUserRow.CompID.OldValue, POSPUserRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(POSPUserRow.LoadFromDataRow, POSPUserRow.EmpID.OldValue, POSPUserRow.EmpID.Value))
            db.AddInParameter(dbcmd, "@PKCategory", DbType.String, IIf(POSPUserRow.LoadFromDataRow, POSPUserRow.Category.OldValue, POSPUserRow.Category.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal POSPUserRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update POSPUser Set")
            For i As Integer = 0 To POSPUserRow.FieldNames.Length - 1
                If Not POSPUserRow.IsIdentityField(POSPUserRow.FieldNames(i)) AndAlso POSPUserRow.IsUpdated(POSPUserRow.FieldNames(i)) AndAlso POSPUserRow.CreateUpdateSQL(POSPUserRow.FieldNames(i)) Then
                    strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, POSPUserRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And EmpID = @PKEmpID")
            strSQL.AppendLine("And Category = @PKCategory")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If POSPUserRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, POSPUserRow.CompID.Value)
            If POSPUserRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, POSPUserRow.EmpID.Value)
            If POSPUserRow.Category.Updated Then db.AddInParameter(dbcmd, "@Category", DbType.String, POSPUserRow.Category.Value)
            If POSPUserRow.OrgList.Updated Then db.AddInParameter(dbcmd, "@OrgList", DbType.String, POSPUserRow.OrgList.Value)
            If POSPUserRow.OrgFlowList.Updated Then db.AddInParameter(dbcmd, "@OrgFlowList", DbType.String, POSPUserRow.OrgFlowList.Value)
            If POSPUserRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, POSPUserRow.LastChgComp.Value)
            If POSPUserRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, POSPUserRow.LastChgID.Value)
            If POSPUserRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(POSPUserRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), POSPUserRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(POSPUserRow.LoadFromDataRow, POSPUserRow.CompID.OldValue, POSPUserRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(POSPUserRow.LoadFromDataRow, POSPUserRow.EmpID.OldValue, POSPUserRow.EmpID.Value))
            db.AddInParameter(dbcmd, "@PKCategory", DbType.String, IIf(POSPUserRow.LoadFromDataRow, POSPUserRow.Category.OldValue, POSPUserRow.Category.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal POSPUserRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In POSPUserRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update POSPUser Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where CompID = @PKCompID")
                        strSQL.AppendLine("And EmpID = @PKEmpID")
                        strSQL.AppendLine("And Category = @PKCategory")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        If r.Category.Updated Then db.AddInParameter(dbcmd, "@Category", DbType.String, r.Category.Value)
                        If r.OrgList.Updated Then db.AddInParameter(dbcmd, "@OrgList", DbType.String, r.OrgList.Value)
                        If r.OrgFlowList.Updated Then db.AddInParameter(dbcmd, "@OrgFlowList", DbType.String, r.OrgFlowList.Value)
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                        db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))
                        db.AddInParameter(dbcmd, "@PKCategory", DbType.String, IIf(r.LoadFromDataRow, r.Category.OldValue, r.Category.Value))

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

        Public Function Update(ByVal POSPUserRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In POSPUserRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update POSPUser Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where CompID = @PKCompID")
                strSQL.AppendLine("And EmpID = @PKEmpID")
                strSQL.AppendLine("And Category = @PKCategory")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                If r.Category.Updated Then db.AddInParameter(dbcmd, "@Category", DbType.String, r.Category.Value)
                If r.OrgList.Updated Then db.AddInParameter(dbcmd, "@OrgList", DbType.String, r.OrgList.Value)
                If r.OrgFlowList.Updated Then db.AddInParameter(dbcmd, "@OrgFlowList", DbType.String, r.OrgFlowList.Value)
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))
                db.AddInParameter(dbcmd, "@PKCategory", DbType.String, IIf(r.LoadFromDataRow, r.Category.OldValue, r.Category.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal POSPUserRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From POSPUser")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And Category = @Category")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, POSPUserRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, POSPUserRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@Category", DbType.String, POSPUserRow.Category.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal POSPUserRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From POSPUser")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And Category = @Category")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, POSPUserRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, POSPUserRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@Category", DbType.String, POSPUserRow.Category.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From POSPUser")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal POSPUserRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into POSPUser")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, Category, OrgList, OrgFlowList, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @Category, @OrgList, @OrgFlowList, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, POSPUserRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, POSPUserRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@Category", DbType.String, POSPUserRow.Category.Value)
            db.AddInParameter(dbcmd, "@OrgList", DbType.String, POSPUserRow.OrgList.Value)
            db.AddInParameter(dbcmd, "@OrgFlowList", DbType.String, POSPUserRow.OrgFlowList.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, POSPUserRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, POSPUserRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(POSPUserRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), POSPUserRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal POSPUserRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into POSPUser")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, Category, OrgList, OrgFlowList, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @Category, @OrgList, @OrgFlowList, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, POSPUserRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, POSPUserRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@Category", DbType.String, POSPUserRow.Category.Value)
            db.AddInParameter(dbcmd, "@OrgList", DbType.String, POSPUserRow.OrgList.Value)
            db.AddInParameter(dbcmd, "@OrgFlowList", DbType.String, POSPUserRow.OrgFlowList.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, POSPUserRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, POSPUserRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(POSPUserRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), POSPUserRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal POSPUserRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into POSPUser")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, Category, OrgList, OrgFlowList, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @Category, @OrgList, @OrgFlowList, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In POSPUserRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@Category", DbType.String, r.Category.Value)
                        db.AddInParameter(dbcmd, "@OrgList", DbType.String, r.OrgList.Value)
                        db.AddInParameter(dbcmd, "@OrgFlowList", DbType.String, r.OrgFlowList.Value)
                        db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
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

        Public Function Insert(ByVal POSPUserRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into POSPUser")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, Category, OrgList, OrgFlowList, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @Category, @OrgList, @OrgFlowList, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In POSPUserRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@Category", DbType.String, r.Category.Value)
                db.AddInParameter(dbcmd, "@OrgList", DbType.String, r.OrgList.Value)
                db.AddInParameter(dbcmd, "@OrgFlowList", DbType.String, r.OrgFlowList.Value)
                db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
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

