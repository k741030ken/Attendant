'****************************************************************  
' Subject：OV5000系列-其他代碼設定
' Author：Jason
' Table：AT_CodeMap
' Description：其他代碼設定資料庫操作相關
' Created Date：2017.01.03
' Modify Date：2017.01.25
'****************************************************************

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beManageCodeSet
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = {"TabName", "FldName", "Code", "CodeCName", "SortFld", "NotShowFlag", "LastChgComp", "LastChgID", "LastChgDate"}
        Private m_Types As System.Type() = {GetType(String), GetType(String), GetType(String), GetType(String), GetType(Integer), GetType(Char), GetType(String), GetType(String), GetType(Date)}
        Private m_PrimaryFields As String() = {"TabName", "FldName", "Code"}

        Public ReadOnly Property Rows() As beManageCodeSet.Rows
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
        ''' 將DataTable資料轉成Entity
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Transfer2Row(ManageCodeSetTable As DataTable)
            For Each dr As DataRow In ManageCodeSetTable.Rows
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

                dr(m_Rows(i).TabName.FieldName) = m_Rows(i).TabName.Value
                dr(m_Rows(i).FldName.FieldName) = m_Rows(i).FldName.Value
                dr(m_Rows(i).Code.FieldName) = m_Rows(i).Code.Value
                dr(m_Rows(i).CodeCName.FieldName) = m_Rows(i).CodeCName.Value
                dr(m_Rows(i).SortFld.FieldName) = m_Rows(i).SortFld.Value
                dr(m_Rows(i).NotShowFlag.FieldName) = m_Rows(i).NotShowFlag.Value
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

        Public Sub Add(ManageCodeSetRow As Row)
            m_Rows.Add(ManageCodeSetRow)
        End Sub

        Public Sub Remove(ManageCodeSetRow As Row)
            If m_Rows.IndexOf(ManageCodeSetRow) >= 0 Then
                m_Rows.Remove(ManageCodeSetRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_TabName As Field(Of String) = New Field(Of String)("TabName", True)
        Private FI_FldName As Field(Of String) = New Field(Of String)("FldName", True)
        Private FI_Code As Field(Of String) = New Field(Of String)("Code", True)
        Private FI_CodeCName As Field(Of String) = New Field(Of String)("CodeCName", True)
        Private FI_SortFld As Field(Of Integer) = New Field(Of Integer)("SortFld", True)
        Private FI_NotShowFlag As Field(Of Char) = New Field(Of Char)("NotShowFlag", True)

        Private FI_LastChgComp As Field(Of String) = New Field(Of String)("LastChgComp", True)
        Private FI_LastChgID As Field(Of String) = New Field(Of String)("LastChgID", True)
        Private FI_LastChgDate As Field(Of Date) = New Field(Of Date)("LastChgDate", True)
        Private m_FieldNames As String() = {"TabName", "FldName", "Code", "CodeCName", "SortFld", "NotShowFlag", "LastChgComp", "LastChgID", "LastChgDate"}
        Private m_PrimaryFields As String() = {"TabName", "FldName", "Code"}
        Private m_IdentityFields As String() = {}
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "TabName"
                    Return FI_TabName.Value
                Case "FldName"
                    Return FI_FldName.Value
                Case "Code"
                    Return FI_Code.Value
                Case "CodeCName"
                    Return FI_CodeCName.Value
                Case "SortFld"
                    Return FI_SortFld.Value
                Case "NotShowFlag"
                    Return FI_NotShowFlag.Value
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
                Case "TabName"
                    FI_TabName.SetValue(value)
                Case "FldName"
                    FI_FldName.SetValue(value)
                Case "Code"
                    FI_Code.SetValue(value)
                Case "CodeCName"
                    FI_CodeCName.SetValue(value)
                Case "SortFld"
                    FI_SortFld.SetValue(value)
                Case "NotShowFlag"
                    FI_NotShowFlag.SetValue(value)
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
                Case "TabName"
                    Return FI_TabName.Updated
                Case "FldName"
                    Return FI_FldName.Updated
                Case "Code"
                    Return FI_Code.Updated
                Case "CodeCName"
                    Return FI_CodeCName.Updated
                Case "SortFld"
                    Return FI_SortFld.Updated
                Case "NotShowFlag"
                    Return FI_NotShowFlag.Updated
                Case "LastChgComp"
                    Return FI_LastChgComp.Updated
                Case "LastChgID"
                    Return FI_LastChgID.Updated
                Case "LastChgDate"
                    Return FI_LastChgDate.Updated
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public Function CreateUpdateSQL(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "TabName"
                    Return FI_TabName.CreateUpdateSQL
                Case "FldName"
                    Return FI_FldName.CreateUpdateSQL
                Case "Code"
                    Return FI_Code.CreateUpdateSQL
                Case "CodeCName"
                    Return FI_CodeCName.CreateUpdateSQL
                Case "SortFld"
                    Return FI_SortFld.CreateUpdateSQL
                Case "NotShowFlag"
                    Return FI_NotShowFlag.CreateUpdateSQL
                Case "LastChgComp"
                    Return FI_LastChgComp.CreateUpdateSQL
                Case "LastChgID"
                    Return FI_LastChgID.CreateUpdateSQL
                Case "LastChgDate"
                    Return FI_LastChgDate.CreateUpdateSQL
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
            FI_TabName.SetInitValue("")
            FI_FldName.SetInitValue("")
            FI_Code.SetInitValue("")
            FI_CodeCName.SetInitValue("")
            FI_SortFld.SetInitValue("0")
            FI_NotShowFlag.SetInitValue("0")
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_TabName.SetInitValue(dr("TabName"))
            FI_FldName.SetInitValue(dr("FldName"))
            FI_Code.SetInitValue(dr("Code"))
            FI_CodeCName.SetInitValue(dr("CodeCName"))
            FI_SortFld.SetInitValue(dr("SortFld"))
            FI_NotShowFlag.SetInitValue(dr("NotShowFlag"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))
            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_TabName.Updated = False
            FI_FldName.Updated = False
            FI_Code.Updated = False
            FI_CodeCName.Updated = False
            FI_SortFld.Updated = False
            FI_NotShowFlag.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
        End Sub

        Public ReadOnly Property TabName As Field(Of String)
            Get
                Return FI_TabName
            End Get
        End Property

        Public ReadOnly Property FldName As Field(Of String)
            Get
                Return FI_FldName
            End Get
        End Property

        Public ReadOnly Property Code As Field(Of String)
            Get
                Return FI_Code
            End Get
        End Property

        Public ReadOnly Property CodeCName As Field(Of String)
            Get
                Return FI_CodeCName
            End Get
        End Property

        Public ReadOnly Property SortFld As Field(Of Integer)
            Get
                Return FI_SortFld
            End Get
        End Property

        Public ReadOnly Property NotShowFlag As Field(Of Char)
            Get
                Return FI_NotShowFlag
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

        Private Property _eHRMSDB_ITRD As String
            Get
                Dim result As String = ConfigurationManager.AppSettings("eHRMSDB")
                If String.IsNullOrEmpty(result) Then
                    result = "eHRMSDB_ITRD"
                End If
                Return result
            End Get
            Set(ByVal value As String)

            End Set
        End Property

        Private Property _AattendantDBName As String
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

#Region "查詢"

        ''' <summary>
        ''' 查詢全部的代碼
        ''' </summary>
        ''' <param name="ManageCodeSetRow"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function QueryAll(ByVal ManageCodeSetRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            'Dim db As Database = DatabaseFactory.CreateDatabase("testConnectionString")

            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine(" SELECT ")
            strSQL.AppendLine(" H.TabName, H.FldName, H.CodeCName AS CodeName2, H.Code, H.CodeCName, H.SortFld, H.NotShowFlag ")
            strSQL.AppendLine(" ,Comp.CompName AS LastChgComp, Pers.NameN AS LastChgID ")
            strSQL.AppendLine(" ,LastChgDate = Case When Convert(Char(10), H.LastChgDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, H.LastChgDate, 120) End ")
            strSQL.AppendLine(" FROM AT_CodeMap H ")
            strSQL.AppendLine(" LEFT JOIN " + _eHRMSDB_ITRD + ".[dbo].[Company] Comp ON H.LastChgComp = Comp.CompID ")
            strSQL.AppendLine(" LEFT JOIN " + _eHRMSDB_ITRD + ".[dbo].[Personal] Pers ON H.LastChgComp = Pers.CompID AND H.LastChgID = Pers.EmpID ")
            strSQL.AppendLine(" WHERE H.TabName IN (SELECT FldName FROM AT_CodeMap WHERE TabName='HRCodeMap_OpenMaintain') ")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            Return db.ExecuteDataSet(dbcmd)
        End Function

        ''' <summary>
        ''' (交易)查詢全部的代碼
        ''' </summary>
        ''' <param name="ManageCodeSetRow"></param>
        ''' <param name="tran"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function QueryAll(ManageCodeSetRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            'Dim db As Database = DatabaseFactory.CreateDatabase("testConnectionString")

            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine(" SELECT ")
            strSQL.AppendLine(" H.TabName, H.FldName, H.CodeCName AS CodeName2, H.Code, H.CodeCName, H.SortFld, H.NotShowFlag ")
            strSQL.AppendLine(" ,Comp.CompName AS LastChgComp, Pers.NameN AS LastChgID ")
            strSQL.AppendLine(" ,LastChgDate = Case When Convert(Char(10), H.LastChgDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, H.LastChgDate, 120) End ")
            strSQL.AppendLine(" FROM AT_CodeMap H ")
            strSQL.AppendLine(" LEFT JOIN " + _eHRMSDB_ITRD + ".[dbo].[Company] Comp ON H.LastChgComp = Comp.CompID ")
            strSQL.AppendLine(" LEFT JOIN " + _eHRMSDB_ITRD + ".[dbo].[Personal] Pers ON H.LastChgComp = Pers.CompID AND H.LastChgID = Pers.EmpID ")
            strSQL.AppendLine(" WHERE H.TabName IN (SELECT FldName FROM AT_CodeMap WHERE TabName='HRCodeMap_OpenMaintain') ")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        ''' <summary>
        ''' 查詢指定代碼
        ''' </summary>
        ''' <param name="ManageCodeSetRow"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function QueryByKey(ByVal ManageCodeSetRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            'Dim db As Database = DatabaseFactory.CreateDatabase("testConnectionString")

            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine(" Select * From AT_CodeMap")
            strSQL.AppendLine(" WHERE TabName = @TabName ")
            strSQL.AppendLine(" AND FldName = @FldName ")
            strSQL.AppendLine(" AND Code = @Code ")
            strSQL.AppendLine(" ORDER BY TabName, FldName, SortFld ")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@TabName", DbType.String, ManageCodeSetRow.TabName.Value)
            db.AddInParameter(dbcmd, "@FldName", DbType.String, ManageCodeSetRow.FldName.Value)
            db.AddInParameter(dbcmd, "@Code", DbType.String, ManageCodeSetRow.Code.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        ''' <summary>
        ''' (交易)查詢指定代碼
        ''' </summary>
        ''' <param name="ManageCodeSetRow"></param>
        ''' <param name="tran"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function QueryByKey(ManageCodeSetRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            'Dim db As Database = DatabaseFactory.CreateDatabase("testConnectionString")

            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From AT_CodeMap")
            strSQL.AppendLine(" WHERE TabName = @TabName ")
            strSQL.AppendLine(" AND FldName = @FldName ")
            strSQL.AppendLine(" AND Code = @Code ")
            strSQL.AppendLine(" ORDER BY TabName, FldName, SortFld ")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@TabName", DbType.String, ManageCodeSetRow.TabName.Value)
            db.AddInParameter(dbcmd, "@FldName", DbType.String, ManageCodeSetRow.FldName.Value)
            db.AddInParameter(dbcmd, "@Code", DbType.String, ManageCodeSetRow.Code.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        ''' <summary>
        ''' Custom查詢條件
        ''' </summary>
        ''' <param name="WhereCondition"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            'Dim db As Database = DatabaseFactory.CreateDatabase("testConnectionString")

            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine(" SELECT ")
            strSQL.AppendLine(" H.TabName, H.FldName, H.CodeCName AS CodeName2, H.Code, H.CodeCName, H.SortFld, H.NotShowFlag ")
            strSQL.AppendLine(" ,Comp.CompName AS LastChgComp, Pers.NameN AS LastChgID ")
            strSQL.AppendLine(" ,LastChgDate = Case When Convert(Char(10), H.LastChgDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, H.LastChgDate, 120) End ")
            strSQL.AppendLine(" FROM AT_CodeMap H ")
            strSQL.AppendLine(" LEFT JOIN " + _eHRMSDB_ITRD + ".[dbo].[Company] Comp ON H.LastChgComp = Comp.CompID ")
            strSQL.AppendLine(" LEFT JOIN " + _eHRMSDB_ITRD + ".[dbo].[Personal] Pers ON H.LastChgComp = Pers.CompID AND H.LastChgID = Pers.EmpID ")
            strSQL.AppendLine(" WHERE H.TabName IN (SELECT FldName FROM AT_CodeMap WHERE TabName='HRCodeMap_OpenMaintain') ")

            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

#End Region

#Region "檢查資料是否存在"

        ''' <summary>
        ''' 檢查資料是否存在
        ''' </summary>
        ''' <param name="ManageCodeSetRow"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function IsDataExists(ByVal ManageCodeSetRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            'Dim db As Database = DatabaseFactory.CreateDatabase("testConnectionString")

            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From AT_CodeMap")
            strSQL.AppendLine(" WHERE TabName = @TabName ")
            strSQL.AppendLine(" AND FldName = @FldName ")
            strSQL.AppendLine(" AND Code = @Code ")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@TabName", DbType.String, ManageCodeSetRow.TabName.Value)
            db.AddInParameter(dbcmd, "@FldName", DbType.String, ManageCodeSetRow.FldName.Value)
            db.AddInParameter(dbcmd, "@Code", DbType.String, ManageCodeSetRow.Code.Value)
            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        ''' <summary>
        ''' (交易)檢查資料是否存在
        ''' </summary>
        ''' <param name="ManageCodeSetRow"></param>
        ''' <param name="tran"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function IsDataExists(ByVal ManageCodeSetRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            'Dim db As Database = DatabaseFactory.CreateDatabase("testConnectionString")

            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine(" SELECT Count(*) Cnt FROM AT_CodeMap ")
            strSQL.AppendLine(" WHERE TabName = @TabName ")
            strSQL.AppendLine(" AND FldName = @FldName ")
            strSQL.AppendLine(" AND Code = @Code ")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@TabName", DbType.String, ManageCodeSetRow.TabName.Value)
            db.AddInParameter(dbcmd, "@FldName", DbType.String, ManageCodeSetRow.FldName.Value)
            db.AddInParameter(dbcmd, "@Code", DbType.String, ManageCodeSetRow.Code.Value)
            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        ''' <summary>
        ''' 檢查代碼名稱是否已存在
        ''' </summary>
        ''' <param name="ManageCodeSetRow"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function IsCodeCNameExists(ByVal ManageCodeSetRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            'Dim db As Database = DatabaseFactory.CreateDatabase("testConnectionString")

            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From AT_CodeMap")
            strSQL.AppendLine(" WHERE TabName = @TabName ")
            strSQL.AppendLine(" AND FldName = @FldName ")
            strSQL.AppendLine(" AND CodeCName = @CodeCName ")
            strSQL.AppendLine(" AND Code <> @Code ")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@TabName", DbType.String, ManageCodeSetRow.TabName.Value)
            db.AddInParameter(dbcmd, "@FldName", DbType.String, ManageCodeSetRow.FldName.Value)
            db.AddInParameter(dbcmd, "@Code", DbType.String, ManageCodeSetRow.Code.Value)
            db.AddInParameter(dbcmd, "@CodeCName", DbType.String, ManageCodeSetRow.CodeCName.Value)
            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        ''' <summary>
        ''' (交易)檢查代碼名稱是否已存在
        ''' </summary>
        ''' <param name="ManageCodeSetRow"></param>
        ''' <param name="tran"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function IsCodeCNameExists(ByVal ManageCodeSetRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            'Dim db As Database = DatabaseFactory.CreateDatabase("testConnectionString")

            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine(" SELECT Count(*) Cnt FROM AT_CodeMap ")
            strSQL.AppendLine(" WHERE TabName = @TabName ")
            strSQL.AppendLine(" AND FldName = @FldName ")
            strSQL.AppendLine(" AND CodeCName = @CodeCName ")
            strSQL.AppendLine(" AND Code <> @Code ")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@TabName", DbType.String, ManageCodeSetRow.TabName.Value)
            db.AddInParameter(dbcmd, "@FldName", DbType.String, ManageCodeSetRow.FldName.Value)
            db.AddInParameter(dbcmd, "@Code", DbType.String, ManageCodeSetRow.Code.Value)
            db.AddInParameter(dbcmd, "@CodeCName", DbType.String, ManageCodeSetRow.CodeCName.Value)
            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

#End Region

#Region "新增"

        ''' <summary>
        ''' 新增代碼
        ''' </summary>
        ''' <param name="ManageCodeSetRow"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Insert(ByVal ManageCodeSetRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            'Dim db As Database = DatabaseFactory.CreateDatabase("testConnectionString")

            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine(" INSERT INTO AT_CodeMap ")
            strSQL.AppendLine("(")
            strSQL.AppendLine(" TabName, FldName, Code, CodeCName, SortFld, NotShowFlag, LastChgComp, LastChgID, LastChgDate ")
            strSQL.AppendLine(")")
            strSQL.AppendLine(" VALUES")
            strSQL.AppendLine("(")
            strSQL.AppendLine(" @TabName, @FldName, @Code, @CodeCName, @SortFld, @NotShowFlag, @LastChgComp, @LastChgID, @LastChgDate ")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@TabName", DbType.String, ManageCodeSetRow.TabName.Value)
            db.AddInParameter(dbcmd, "@FldName", DbType.String, ManageCodeSetRow.FldName.Value)
            db.AddInParameter(dbcmd, "@Code", DbType.String, ManageCodeSetRow.Code.Value)
            db.AddInParameter(dbcmd, "@CodeCName", DbType.String, ManageCodeSetRow.CodeCName.Value)
            db.AddInParameter(dbcmd, "@SortFld", DbType.Int32, ManageCodeSetRow.SortFld.Value)
            db.AddInParameter(dbcmd, "@NotShowFlag", DbType.String, ManageCodeSetRow.NotShowFlag.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, ManageCodeSetRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, ManageCodeSetRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(ManageCodeSetRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), ManageCodeSetRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        ''' <summary>
        ''' (交易)新增代碼
        ''' </summary>
        ''' <param name="ManageCodeSetRow"></param>
        ''' <param name="tran"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Insert(ByVal ManageCodeSetRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            'Dim db As Database = DatabaseFactory.CreateDatabase("testConnectionString")

            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine(" INSERT INTO AT_CodeMap ")
            strSQL.AppendLine("(")
            strSQL.AppendLine(" TabName, FldName, Code, CodeCName, SortFld, NotShowFlag, LastChgComp, LastChgID, LastChgDate ")
            strSQL.AppendLine(")")
            strSQL.AppendLine(" VALUES")
            strSQL.AppendLine("(")
            strSQL.AppendLine(" @TabName, @FldName, @Code, @CodeCName, @SortFld, @NotShowFlag, @LastChgComp, @LastChgID, @LastChgDate ")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@TabName", DbType.String, ManageCodeSetRow.TabName.Value)
            db.AddInParameter(dbcmd, "@FldName", DbType.String, ManageCodeSetRow.FldName.Value)
            db.AddInParameter(dbcmd, "@Code", DbType.String, ManageCodeSetRow.Code.Value)
            db.AddInParameter(dbcmd, "@CodeCName", DbType.String, ManageCodeSetRow.CodeCName.Value)
            db.AddInParameter(dbcmd, "@SortFld", DbType.Int32, ManageCodeSetRow.SortFld.Value)
            db.AddInParameter(dbcmd, "@NotShowFlag", DbType.String, ManageCodeSetRow.NotShowFlag.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, ManageCodeSetRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, ManageCodeSetRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(ManageCodeSetRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), ManageCodeSetRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        ''' <summary>
        ''' 新增代碼-回傳有多少資料列受影響
        ''' </summary>
        ''' <param name="ManageCodeSetRow"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Insert(ByVal ManageCodeSetRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            'Dim db As Database = DatabaseFactory.CreateDatabase("testConnectionString")

            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine(" INSERT INTO AT_CodeMap ")
            strSQL.AppendLine("(")
            strSQL.AppendLine(" TabName, FldName, Code, CodeCName, SortFld, NotShowFlag, LastChgComp, LastChgID, LastChgDate ")
            strSQL.AppendLine(")")
            strSQL.AppendLine(" VALUES")
            strSQL.AppendLine("(")
            strSQL.AppendLine(" @TabName, @FldName, @Code, @CodeCName, @SortFld, @NotShowFlag, @LastChgComp, @LastChgID, @LastChgDate ")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In ManageCodeSetRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@TabName", DbType.String, r.TabName.Value)
                        db.AddInParameter(dbcmd, "@FldName", DbType.String, r.FldName.Value)
                        db.AddInParameter(dbcmd, "@Code", DbType.String, r.Code.Value)
                        db.AddInParameter(dbcmd, "@CodeCName", DbType.String, r.CodeCName.Value)
                        db.AddInParameter(dbcmd, "@SortFld", DbType.Int32, r.SortFld.Value)
                        db.AddInParameter(dbcmd, "@NotShowFlag", DbType.String, r.NotShowFlag.Value)
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

        ''' <summary>
        ''' (交易)新增代碼-回傳有多少資料列受影響
        ''' </summary>
        ''' <param name="ManageCodeSetRow"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Insert(ByVal ManageCodeSetRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            'Dim db As Database = DatabaseFactory.CreateDatabase("testConnectionString")

            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine(" INSERT INTO AT_CodeMap ")
            strSQL.AppendLine("(")
            strSQL.AppendLine(" TabName, FldName, Code, CodeCName, SortFld, NotShowFlag, LastChgComp, LastChgID, LastChgDate ")
            strSQL.AppendLine(")")
            strSQL.AppendLine(" VALUES")
            strSQL.AppendLine("(")
            strSQL.AppendLine(" @TabName, @FldName, @Code, @CodeCName, @SortFld, @NotShowFlag, @LastChgComp, @LastChgID, @LastChgDate ")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In ManageCodeSetRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@TabName", DbType.String, r.TabName.Value)
                db.AddInParameter(dbcmd, "@FldName", DbType.String, r.FldName.Value)
                db.AddInParameter(dbcmd, "@Code", DbType.String, r.Code.Value)
                db.AddInParameter(dbcmd, "@CodeCName", DbType.String, r.CodeCName.Value)
                db.AddInParameter(dbcmd, "@SortFld", DbType.Int32, r.SortFld.Value)
                db.AddInParameter(dbcmd, "@NotShowFlag", DbType.String, r.NotShowFlag.Value)
                db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

#End Region

#Region "修改"

        ''' <summary>
        ''' 修改代碼
        ''' </summary>
        ''' <param name="ManageCodeSetRow"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Update(ByVal ManageCodeSetRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine(" UPDATE AT_CodeMap SET ")
            For i As Integer = 0 To ManageCodeSetRow.FieldNames.Length - 1
                If Not ManageCodeSetRow.IsIdentityField(ManageCodeSetRow.FieldNames(i)) AndAlso ManageCodeSetRow.IsUpdated(ManageCodeSetRow.FieldNames(i)) AndAlso ManageCodeSetRow.CreateUpdateSQL(ManageCodeSetRow.FieldNames(i)) Then
                    strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, ManageCodeSetRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine(" WHERE TabName = @TabName ")
            strSQL.AppendLine(" AND FldName = @FldName ")
            strSQL.AppendLine(" AND Code = @PKCode ")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If ManageCodeSetRow.Code.Updated Then db.AddInParameter(dbcmd, "@TabName", DbType.String, ManageCodeSetRow.TabName.Value)
            If ManageCodeSetRow.Code.Updated Then db.AddInParameter(dbcmd, "@FldName", DbType.String, ManageCodeSetRow.FldName.Value)
            If ManageCodeSetRow.Code.Updated Then db.AddInParameter(dbcmd, "@Code", DbType.String, ManageCodeSetRow.Code.Value)
            If ManageCodeSetRow.CodeCName.Updated Then db.AddInParameter(dbcmd, "@CodeCName", DbType.String, ManageCodeSetRow.CodeCName.Value)
            If ManageCodeSetRow.SortFld.Updated Then db.AddInParameter(dbcmd, "@SortFld", DbType.Int32, ManageCodeSetRow.SortFld.Value)
            If ManageCodeSetRow.NotShowFlag.Updated Then db.AddInParameter(dbcmd, "@NotShowFlag", DbType.String, ManageCodeSetRow.NotShowFlag.Value)
            If ManageCodeSetRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, ManageCodeSetRow.LastChgComp.Value)
            If ManageCodeSetRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, ManageCodeSetRow.LastChgID.Value)
            If ManageCodeSetRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(ManageCodeSetRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), ManageCodeSetRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCode", DbType.String, IIf(ManageCodeSetRow.LoadFromDataRow, ManageCodeSetRow.Code.OldValue, ManageCodeSetRow.Code.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        ''' <summary>
        ''' (交易)修改代碼
        ''' </summary>
        ''' <param name="ManageCodeSetRow"></param>
        ''' <param name="tran"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Update(ByVal ManageCodeSetRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine(" UPDATE AT_CodeMap SET ")
            For i As Integer = 0 To ManageCodeSetRow.FieldNames.Length - 1
                If Not ManageCodeSetRow.IsIdentityField(ManageCodeSetRow.FieldNames(i)) AndAlso ManageCodeSetRow.IsUpdated(ManageCodeSetRow.FieldNames(i)) AndAlso ManageCodeSetRow.CreateUpdateSQL(ManageCodeSetRow.FieldNames(i)) Then
                    strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, ManageCodeSetRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine(" WHERE TabName = @TabName ")
            strSQL.AppendLine(" AND FldName = @FldName ")
            strSQL.AppendLine(" AND Code = @PKCode")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If ManageCodeSetRow.Code.Updated Then db.AddInParameter(dbcmd, "@TabName", DbType.String, ManageCodeSetRow.TabName.Value)
            If ManageCodeSetRow.Code.Updated Then db.AddInParameter(dbcmd, "@FldName", DbType.String, ManageCodeSetRow.FldName.Value)
            If ManageCodeSetRow.Code.Updated Then db.AddInParameter(dbcmd, "@Code", DbType.String, ManageCodeSetRow.Code.Value)
            If ManageCodeSetRow.CodeCName.Updated Then db.AddInParameter(dbcmd, "@CodeCName", DbType.String, ManageCodeSetRow.CodeCName.Value)
            If ManageCodeSetRow.SortFld.Updated Then db.AddInParameter(dbcmd, "@SortFld", DbType.Int32, ManageCodeSetRow.SortFld.Value)
            If ManageCodeSetRow.NotShowFlag.Updated Then db.AddInParameter(dbcmd, "@NotShowFlag", DbType.String, ManageCodeSetRow.NotShowFlag.Value)
            If ManageCodeSetRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, ManageCodeSetRow.LastChgComp.Value)
            If ManageCodeSetRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, ManageCodeSetRow.LastChgID.Value)
            If ManageCodeSetRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(ManageCodeSetRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), ManageCodeSetRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCode", DbType.String, IIf(ManageCodeSetRow.LoadFromDataRow, ManageCodeSetRow.Code.OldValue, ManageCodeSetRow.Code.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        ''' <summary>
        ''' 修改代碼-回傳有多少資料列受影響
        ''' </summary>
        ''' <param name="ManageCodeSetRow"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Update(ByVal ManageCodeSetRow As Row()) As Integer
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
                    For Each r As Row In ManageCodeSetRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine(" UPDATE AT_CodeMap SET ")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine(" WHERE TabName = @TabName ")
                        strSQL.AppendLine(" AND FldName = @FldName ")
                        strSQL.AppendLine(" AND Code = @PKCode")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.Code.Updated Then db.AddInParameter(dbcmd, "@TabName", DbType.String, r.TabName.Value)
                        If r.Code.Updated Then db.AddInParameter(dbcmd, "@FldName", DbType.String, r.FldName.Value)
                        If r.Code.Updated Then db.AddInParameter(dbcmd, "@Code", DbType.String, r.Code.Value)
                        If r.CodeCName.Updated Then db.AddInParameter(dbcmd, "@CodeCName", DbType.String, r.CodeCName.Value)
                        If r.SortFld.Updated Then db.AddInParameter(dbcmd, "@SortFld", DbType.Int32, r.SortFld.Value)
                        If r.NotShowFlag.Updated Then db.AddInParameter(dbcmd, "@NotShowFlag", DbType.String, r.NotShowFlag.Value)
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKCode", DbType.String, IIf(r.LoadFromDataRow, r.Code.OldValue, r.Code.Value))

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

        ''' <summary>
        ''' (交易)修改代碼-回傳有多少資料列受影響
        ''' </summary>
        ''' <param name="ManageCodeSetRow"></param>
        ''' <param name="tran"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Update(ByVal ManageCodeSetRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In ManageCodeSetRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine(" UPDATE AT_CodeMap SET ")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine(" WHERE TabName = @TabName ")
                strSQL.AppendLine(" AND FldName = @FldName ")
                strSQL.AppendLine(" AND Code = @PKCode")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.Code.Updated Then db.AddInParameter(dbcmd, "@TabName", DbType.String, r.TabName.Value)
                If r.Code.Updated Then db.AddInParameter(dbcmd, "@FldName", DbType.String, r.FldName.Value)
                If r.Code.Updated Then db.AddInParameter(dbcmd, "@Code", DbType.String, r.Code.Value)
                If r.CodeCName.Updated Then db.AddInParameter(dbcmd, "@CodeCName", DbType.String, r.CodeCName.Value)
                If r.SortFld.Updated Then db.AddInParameter(dbcmd, "@SortFld", DbType.Int32, r.SortFld.Value)
                If r.NotShowFlag.Updated Then db.AddInParameter(dbcmd, "@NotShowFlag", DbType.String, r.NotShowFlag.Value)
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKCode", DbType.String, IIf(r.LoadFromDataRow, r.Code.OldValue, r.Code.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

#End Region

#Region "刪除"

        ''' <summary>
        ''' 刪除代碼
        ''' </summary>
        ''' <param name="ManageCodeSetRow"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function DeleteRowByPrimaryKey(ByVal ManageCodeSetRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine(" DELETE FROM AT_CodeMap")
            strSQL.AppendLine(" WHERE TabName = @TabName ")
            strSQL.AppendLine(" AND FldName = @FldName ")
            strSQL.AppendLine(" AND Code = @Code")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@TabName", DbType.String, ManageCodeSetRow.TabName.Value)
            db.AddInParameter(dbcmd, "@FldName", DbType.String, ManageCodeSetRow.FldName.Value)
            db.AddInParameter(dbcmd, "@Code", DbType.String, ManageCodeSetRow.Code.Value)

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        ''' <summary>
        ''' (交易)刪除代碼
        ''' </summary>
        ''' <param name="ManageCodeSetRow"></param>
        ''' <param name="tran"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function DeleteRowByPrimaryKey(ByVal ManageCodeSetRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine(" DELETE FROM AT_CodeMap")
            strSQL.AppendLine(" WHERE TabName = @TabName ")
            strSQL.AppendLine(" AND FldName = @FldName ")
            strSQL.AppendLine(" AND Code = @Code")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@TabName", DbType.String, ManageCodeSetRow.TabName.Value)
            db.AddInParameter(dbcmd, "@FldName", DbType.String, ManageCodeSetRow.FldName.Value)
            db.AddInParameter(dbcmd, "@Code", DbType.String, ManageCodeSetRow.Code.Value)

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        ''' <summary>
        ''' 刪除代碼-回傳有多少資料列受影響
        ''' </summary>
        ''' <param name="ManageCodeSetRow"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function DeleteRowByPrimaryKey(ByVal ManageCodeSetRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine(" DELETE FROM AT_CodeMap")
            strSQL.AppendLine(" WHERE TabName = @TabName ")
            strSQL.AppendLine(" AND FldName = @FldName ")
            strSQL.AppendLine(" AND Code = @Code")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In ManageCodeSetRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@TabName", DbType.String, r.TabName.Value)
                        db.AddInParameter(dbcmd, "@FldName", DbType.String, r.FldName.Value)
                        db.AddInParameter(dbcmd, "@Code", DbType.String, r.Code.Value)

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

        ''' <summary>
        ''' (交易)刪除代碼-回傳有多少資料列受影響
        ''' </summary>
        ''' <param name="ManageCodeSetRow"></param>
        ''' <param name="tran"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function DeleteRowByPrimaryKey(ByVal ManageCodeSetRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine(" DELETE FROM AT_CodeMap")
            strSQL.AppendLine(" WHERE TabName = @TabName ")
            strSQL.AppendLine(" AND FldName = @FldName ")
            strSQL.AppendLine(" AND Code = @Code")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In ManageCodeSetRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@TabName", DbType.String, r.TabName.Value)
                db.AddInParameter(dbcmd, "@FldName", DbType.String, r.FldName.Value)
                db.AddInParameter(dbcmd, "@Code", DbType.String, r.Code.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

#End Region

        ''' <summary>
        ''' 檢查日期
        ''' </summary>
        ''' <param name="Src"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
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

