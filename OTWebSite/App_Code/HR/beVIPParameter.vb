'****************************************************************
' Table:VIPParameter
' Created Date: 2016.09.01
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beVIPParameter
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "EmpID", "GrantFlag", "AllCompIDFlag", "UseCompID", "AllGroupIDFlag", "UseGroupID", "AllOrganIDFlag", "UseOrganID", "UseOurColleagues" _
                                    , "UseRankID", "BeginDate", "EndDate", "CreateComp", "CreateID", "CreateDate", "ReleaseComp", "ReleaseID", "ReleaseDate", "LastChgComp", "LastChgID" _
                                    , "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(Date), GetType(Date), GetType(String), GetType(String), GetType(Date), GetType(String), GetType(String), GetType(Date), GetType(String), GetType(String) _
                                    , GetType(Date) }
        Private m_PrimaryFields As String() = { "CompID", "EmpID", "GrantFlag", "UseCompID", "UseGroupID", "UseOrganID" }

        Public ReadOnly Property Rows() As beVIPParameter.Rows 
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
        Public Sub Transfer2Row(VIPParameterTable As DataTable)
            For Each dr As DataRow In VIPParameterTable.Rows
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
                dr(m_Rows(i).GrantFlag.FieldName) = m_Rows(i).GrantFlag.Value
                dr(m_Rows(i).AllCompIDFlag.FieldName) = m_Rows(i).AllCompIDFlag.Value
                dr(m_Rows(i).UseCompID.FieldName) = m_Rows(i).UseCompID.Value
                dr(m_Rows(i).AllGroupIDFlag.FieldName) = m_Rows(i).AllGroupIDFlag.Value
                dr(m_Rows(i).UseGroupID.FieldName) = m_Rows(i).UseGroupID.Value
                dr(m_Rows(i).AllOrganIDFlag.FieldName) = m_Rows(i).AllOrganIDFlag.Value
                dr(m_Rows(i).UseOrganID.FieldName) = m_Rows(i).UseOrganID.Value
                dr(m_Rows(i).UseOurColleagues.FieldName) = m_Rows(i).UseOurColleagues.Value
                dr(m_Rows(i).UseRankID.FieldName) = m_Rows(i).UseRankID.Value
                dr(m_Rows(i).BeginDate.FieldName) = m_Rows(i).BeginDate.Value
                dr(m_Rows(i).EndDate.FieldName) = m_Rows(i).EndDate.Value
                dr(m_Rows(i).CreateComp.FieldName) = m_Rows(i).CreateComp.Value
                dr(m_Rows(i).CreateID.FieldName) = m_Rows(i).CreateID.Value
                dr(m_Rows(i).CreateDate.FieldName) = m_Rows(i).CreateDate.Value
                dr(m_Rows(i).ReleaseComp.FieldName) = m_Rows(i).ReleaseComp.Value
                dr(m_Rows(i).ReleaseID.FieldName) = m_Rows(i).ReleaseID.Value
                dr(m_Rows(i).ReleaseDate.FieldName) = m_Rows(i).ReleaseDate.Value
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

        Public Sub Add(VIPParameterRow As Row)
            m_Rows.Add(VIPParameterRow)
        End Sub

        Public Sub Remove(VIPParameterRow As Row)
            If m_Rows.IndexOf(VIPParameterRow) >= 0 Then
                m_Rows.Remove(VIPParameterRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_EmpID As Field(Of String) = new Field(Of String)("EmpID", true)
        Private FI_GrantFlag As Field(Of String) = new Field(Of String)("GrantFlag", true)
        Private FI_AllCompIDFlag As Field(Of String) = new Field(Of String)("AllCompIDFlag", true)
        Private FI_UseCompID As Field(Of String) = new Field(Of String)("UseCompID", true)
        Private FI_AllGroupIDFlag As Field(Of String) = new Field(Of String)("AllGroupIDFlag", true)
        Private FI_UseGroupID As Field(Of String) = new Field(Of String)("UseGroupID", true)
        Private FI_AllOrganIDFlag As Field(Of String) = new Field(Of String)("AllOrganIDFlag", true)
        Private FI_UseOrganID As Field(Of String) = new Field(Of String)("UseOrganID", true)
        Private FI_UseOurColleagues As Field(Of String) = new Field(Of String)("UseOurColleagues", true)
        Private FI_UseRankID As Field(Of String) = new Field(Of String)("UseRankID", true)
        Private FI_BeginDate As Field(Of Date) = new Field(Of Date)("BeginDate", true)
        Private FI_EndDate As Field(Of Date) = new Field(Of Date)("EndDate", true)
        Private FI_CreateComp As Field(Of String) = new Field(Of String)("CreateComp", true)
        Private FI_CreateID As Field(Of String) = new Field(Of String)("CreateID", true)
        Private FI_CreateDate As Field(Of Date) = new Field(Of Date)("CreateDate", true)
        Private FI_ReleaseComp As Field(Of String) = new Field(Of String)("ReleaseComp", true)
        Private FI_ReleaseID As Field(Of String) = new Field(Of String)("ReleaseID", true)
        Private FI_ReleaseDate As Field(Of Date) = new Field(Of Date)("ReleaseDate", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "CompID", "EmpID", "GrantFlag", "AllCompIDFlag", "UseCompID", "AllGroupIDFlag", "UseGroupID", "AllOrganIDFlag", "UseOrganID", "UseOurColleagues" _
                                    , "UseRankID", "BeginDate", "EndDate", "CreateComp", "CreateID", "CreateDate", "ReleaseComp", "ReleaseID", "ReleaseDate", "LastChgComp", "LastChgID" _
                                    , "LastChgDate" }
        Private m_PrimaryFields As String() = { "CompID", "EmpID", "GrantFlag", "UseCompID", "UseGroupID", "UseOrganID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CompID"
                    Return FI_CompID.Value
                Case "EmpID"
                    Return FI_EmpID.Value
                Case "GrantFlag"
                    Return FI_GrantFlag.Value
                Case "AllCompIDFlag"
                    Return FI_AllCompIDFlag.Value
                Case "UseCompID"
                    Return FI_UseCompID.Value
                Case "AllGroupIDFlag"
                    Return FI_AllGroupIDFlag.Value
                Case "UseGroupID"
                    Return FI_UseGroupID.Value
                Case "AllOrganIDFlag"
                    Return FI_AllOrganIDFlag.Value
                Case "UseOrganID"
                    Return FI_UseOrganID.Value
                Case "UseOurColleagues"
                    Return FI_UseOurColleagues.Value
                Case "UseRankID"
                    Return FI_UseRankID.Value
                Case "BeginDate"
                    Return FI_BeginDate.Value
                Case "EndDate"
                    Return FI_EndDate.Value
                Case "CreateComp"
                    Return FI_CreateComp.Value
                Case "CreateID"
                    Return FI_CreateID.Value
                Case "CreateDate"
                    Return FI_CreateDate.Value
                Case "ReleaseComp"
                    Return FI_ReleaseComp.Value
                Case "ReleaseID"
                    Return FI_ReleaseID.Value
                Case "ReleaseDate"
                    Return FI_ReleaseDate.Value
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
                Case "GrantFlag"
                    FI_GrantFlag.SetValue(value)
                Case "AllCompIDFlag"
                    FI_AllCompIDFlag.SetValue(value)
                Case "UseCompID"
                    FI_UseCompID.SetValue(value)
                Case "AllGroupIDFlag"
                    FI_AllGroupIDFlag.SetValue(value)
                Case "UseGroupID"
                    FI_UseGroupID.SetValue(value)
                Case "AllOrganIDFlag"
                    FI_AllOrganIDFlag.SetValue(value)
                Case "UseOrganID"
                    FI_UseOrganID.SetValue(value)
                Case "UseOurColleagues"
                    FI_UseOurColleagues.SetValue(value)
                Case "UseRankID"
                    FI_UseRankID.SetValue(value)
                Case "BeginDate"
                    FI_BeginDate.SetValue(value)
                Case "EndDate"
                    FI_EndDate.SetValue(value)
                Case "CreateComp"
                    FI_CreateComp.SetValue(value)
                Case "CreateID"
                    FI_CreateID.SetValue(value)
                Case "CreateDate"
                    FI_CreateDate.SetValue(value)
                Case "ReleaseComp"
                    FI_ReleaseComp.SetValue(value)
                Case "ReleaseID"
                    FI_ReleaseID.SetValue(value)
                Case "ReleaseDate"
                    FI_ReleaseDate.SetValue(value)
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
                Case "GrantFlag"
                    return FI_GrantFlag.Updated
                Case "AllCompIDFlag"
                    return FI_AllCompIDFlag.Updated
                Case "UseCompID"
                    return FI_UseCompID.Updated
                Case "AllGroupIDFlag"
                    return FI_AllGroupIDFlag.Updated
                Case "UseGroupID"
                    return FI_UseGroupID.Updated
                Case "AllOrganIDFlag"
                    return FI_AllOrganIDFlag.Updated
                Case "UseOrganID"
                    return FI_UseOrganID.Updated
                Case "UseOurColleagues"
                    return FI_UseOurColleagues.Updated
                Case "UseRankID"
                    return FI_UseRankID.Updated
                Case "BeginDate"
                    return FI_BeginDate.Updated
                Case "EndDate"
                    return FI_EndDate.Updated
                Case "CreateComp"
                    return FI_CreateComp.Updated
                Case "CreateID"
                    return FI_CreateID.Updated
                Case "CreateDate"
                    return FI_CreateDate.Updated
                Case "ReleaseComp"
                    return FI_ReleaseComp.Updated
                Case "ReleaseID"
                    return FI_ReleaseID.Updated
                Case "ReleaseDate"
                    return FI_ReleaseDate.Updated
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
                Case "GrantFlag"
                    return FI_GrantFlag.CreateUpdateSQL
                Case "AllCompIDFlag"
                    return FI_AllCompIDFlag.CreateUpdateSQL
                Case "UseCompID"
                    return FI_UseCompID.CreateUpdateSQL
                Case "AllGroupIDFlag"
                    return FI_AllGroupIDFlag.CreateUpdateSQL
                Case "UseGroupID"
                    return FI_UseGroupID.CreateUpdateSQL
                Case "AllOrganIDFlag"
                    return FI_AllOrganIDFlag.CreateUpdateSQL
                Case "UseOrganID"
                    return FI_UseOrganID.CreateUpdateSQL
                Case "UseOurColleagues"
                    return FI_UseOurColleagues.CreateUpdateSQL
                Case "UseRankID"
                    return FI_UseRankID.CreateUpdateSQL
                Case "BeginDate"
                    return FI_BeginDate.CreateUpdateSQL
                Case "EndDate"
                    return FI_EndDate.CreateUpdateSQL
                Case "CreateComp"
                    return FI_CreateComp.CreateUpdateSQL
                Case "CreateID"
                    return FI_CreateID.CreateUpdateSQL
                Case "CreateDate"
                    return FI_CreateDate.CreateUpdateSQL
                Case "ReleaseComp"
                    return FI_ReleaseComp.CreateUpdateSQL
                Case "ReleaseID"
                    return FI_ReleaseID.CreateUpdateSQL
                Case "ReleaseDate"
                    return FI_ReleaseDate.CreateUpdateSQL
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
            FI_GrantFlag.SetInitValue("")
            FI_AllCompIDFlag.SetInitValue(0)
            FI_UseCompID.SetInitValue("")
            FI_AllGroupIDFlag.SetInitValue(0)
            FI_UseGroupID.SetInitValue("")
            FI_AllOrganIDFlag.SetInitValue(0)
            FI_UseOrganID.SetInitValue("")
            FI_UseOurColleagues.SetInitValue("")
            FI_UseRankID.SetInitValue("")
            FI_BeginDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_EndDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_CreateComp.SetInitValue("")
            FI_CreateID.SetInitValue("")
            FI_CreateDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_ReleaseComp.SetInitValue("")
            FI_ReleaseID.SetInitValue("")
            FI_ReleaseDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CompID.SetInitValue(dr("CompID"))
            FI_EmpID.SetInitValue(dr("EmpID"))
            FI_GrantFlag.SetInitValue(dr("GrantFlag"))
            FI_AllCompIDFlag.SetInitValue(dr("AllCompIDFlag"))
            FI_UseCompID.SetInitValue(dr("UseCompID"))
            FI_AllGroupIDFlag.SetInitValue(dr("AllGroupIDFlag"))
            FI_UseGroupID.SetInitValue(dr("UseGroupID"))
            FI_AllOrganIDFlag.SetInitValue(dr("AllOrganIDFlag"))
            FI_UseOrganID.SetInitValue(dr("UseOrganID"))
            FI_UseOurColleagues.SetInitValue(dr("UseOurColleagues"))
            FI_UseRankID.SetInitValue(dr("UseRankID"))
            FI_BeginDate.SetInitValue(dr("BeginDate"))
            FI_EndDate.SetInitValue(dr("EndDate"))
            FI_CreateComp.SetInitValue(dr("CreateComp"))
            FI_CreateID.SetInitValue(dr("CreateID"))
            FI_CreateDate.SetInitValue(dr("CreateDate"))
            FI_ReleaseComp.SetInitValue(dr("ReleaseComp"))
            FI_ReleaseID.SetInitValue(dr("ReleaseID"))
            FI_ReleaseDate.SetInitValue(dr("ReleaseDate"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CompID.Updated = False
            FI_EmpID.Updated = False
            FI_GrantFlag.Updated = False
            FI_AllCompIDFlag.Updated = False
            FI_UseCompID.Updated = False
            FI_AllGroupIDFlag.Updated = False
            FI_UseGroupID.Updated = False
            FI_AllOrganIDFlag.Updated = False
            FI_UseOrganID.Updated = False
            FI_UseOurColleagues.Updated = False
            FI_UseRankID.Updated = False
            FI_BeginDate.Updated = False
            FI_EndDate.Updated = False
            FI_CreateComp.Updated = False
            FI_CreateID.Updated = False
            FI_CreateDate.Updated = False
            FI_ReleaseComp.Updated = False
            FI_ReleaseID.Updated = False
            FI_ReleaseDate.Updated = False
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

        Public ReadOnly Property GrantFlag As Field(Of String) 
            Get
                Return FI_GrantFlag
            End Get
        End Property

        Public ReadOnly Property AllCompIDFlag As Field(Of String) 
            Get
                Return FI_AllCompIDFlag
            End Get
        End Property

        Public ReadOnly Property UseCompID As Field(Of String) 
            Get
                Return FI_UseCompID
            End Get
        End Property

        Public ReadOnly Property AllGroupIDFlag As Field(Of String) 
            Get
                Return FI_AllGroupIDFlag
            End Get
        End Property

        Public ReadOnly Property UseGroupID As Field(Of String) 
            Get
                Return FI_UseGroupID
            End Get
        End Property

        Public ReadOnly Property AllOrganIDFlag As Field(Of String) 
            Get
                Return FI_AllOrganIDFlag
            End Get
        End Property

        Public ReadOnly Property UseOrganID As Field(Of String) 
            Get
                Return FI_UseOrganID
            End Get
        End Property

        Public ReadOnly Property UseOurColleagues As Field(Of String) 
            Get
                Return FI_UseOurColleagues
            End Get
        End Property

        Public ReadOnly Property UseRankID As Field(Of String) 
            Get
                Return FI_UseRankID
            End Get
        End Property

        Public ReadOnly Property BeginDate As Field(Of Date) 
            Get
                Return FI_BeginDate
            End Get
        End Property

        Public ReadOnly Property EndDate As Field(Of Date) 
            Get
                Return FI_EndDate
            End Get
        End Property

        Public ReadOnly Property CreateComp As Field(Of String) 
            Get
                Return FI_CreateComp
            End Get
        End Property

        Public ReadOnly Property CreateID As Field(Of String) 
            Get
                Return FI_CreateID
            End Get
        End Property

        Public ReadOnly Property CreateDate As Field(Of Date) 
            Get
                Return FI_CreateDate
            End Get
        End Property

        Public ReadOnly Property ReleaseComp As Field(Of String) 
            Get
                Return FI_ReleaseComp
            End Get
        End Property

        Public ReadOnly Property ReleaseID As Field(Of String) 
            Get
                Return FI_ReleaseID
            End Get
        End Property

        Public ReadOnly Property ReleaseDate As Field(Of Date) 
            Get
                Return FI_ReleaseDate
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
        Public Function DeleteRowByPrimaryKey(ByVal VIPParameterRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From VIPParameter")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And GrantFlag = @GrantFlag")
            strSQL.AppendLine("And UseCompID = @UseCompID")
            strSQL.AppendLine("And UseGroupID = @UseGroupID")
            strSQL.AppendLine("And UseOrganID = @UseOrganID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, VIPParameterRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, VIPParameterRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@GrantFlag", DbType.String, VIPParameterRow.GrantFlag.Value)
            db.AddInParameter(dbcmd, "@UseCompID", DbType.String, VIPParameterRow.UseCompID.Value)
            db.AddInParameter(dbcmd, "@UseGroupID", DbType.String, VIPParameterRow.UseGroupID.Value)
            db.AddInParameter(dbcmd, "@UseOrganID", DbType.String, VIPParameterRow.UseOrganID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal VIPParameterRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From VIPParameter")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And GrantFlag = @GrantFlag")
            strSQL.AppendLine("And UseCompID = @UseCompID")
            strSQL.AppendLine("And UseGroupID = @UseGroupID")
            strSQL.AppendLine("And UseOrganID = @UseOrganID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, VIPParameterRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, VIPParameterRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@GrantFlag", DbType.String, VIPParameterRow.GrantFlag.Value)
            db.AddInParameter(dbcmd, "@UseCompID", DbType.String, VIPParameterRow.UseCompID.Value)
            db.AddInParameter(dbcmd, "@UseGroupID", DbType.String, VIPParameterRow.UseGroupID.Value)
            db.AddInParameter(dbcmd, "@UseOrganID", DbType.String, VIPParameterRow.UseOrganID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal VIPParameterRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From VIPParameter")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And GrantFlag = @GrantFlag")
            strSQL.AppendLine("And UseCompID = @UseCompID")
            strSQL.AppendLine("And UseGroupID = @UseGroupID")
            strSQL.AppendLine("And UseOrganID = @UseOrganID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In VIPParameterRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@GrantFlag", DbType.String, r.GrantFlag.Value)
                        db.AddInParameter(dbcmd, "@UseCompID", DbType.String, r.UseCompID.Value)
                        db.AddInParameter(dbcmd, "@UseGroupID", DbType.String, r.UseGroupID.Value)
                        db.AddInParameter(dbcmd, "@UseOrganID", DbType.String, r.UseOrganID.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal VIPParameterRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From VIPParameter")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And GrantFlag = @GrantFlag")
            strSQL.AppendLine("And UseCompID = @UseCompID")
            strSQL.AppendLine("And UseGroupID = @UseGroupID")
            strSQL.AppendLine("And UseOrganID = @UseOrganID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In VIPParameterRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@GrantFlag", DbType.String, r.GrantFlag.Value)
                db.AddInParameter(dbcmd, "@UseCompID", DbType.String, r.UseCompID.Value)
                db.AddInParameter(dbcmd, "@UseGroupID", DbType.String, r.UseGroupID.Value)
                db.AddInParameter(dbcmd, "@UseOrganID", DbType.String, r.UseOrganID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal VIPParameterRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From VIPParameter")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And GrantFlag = @GrantFlag")
            strSQL.AppendLine("And UseCompID = @UseCompID")
            strSQL.AppendLine("And UseGroupID = @UseGroupID")
            strSQL.AppendLine("And UseOrganID = @UseOrganID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, VIPParameterRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, VIPParameterRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@GrantFlag", DbType.String, VIPParameterRow.GrantFlag.Value)
            db.AddInParameter(dbcmd, "@UseCompID", DbType.String, VIPParameterRow.UseCompID.Value)
            db.AddInParameter(dbcmd, "@UseGroupID", DbType.String, VIPParameterRow.UseGroupID.Value)
            db.AddInParameter(dbcmd, "@UseOrganID", DbType.String, VIPParameterRow.UseOrganID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(VIPParameterRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From VIPParameter")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And GrantFlag = @GrantFlag")
            strSQL.AppendLine("And UseCompID = @UseCompID")
            strSQL.AppendLine("And UseGroupID = @UseGroupID")
            strSQL.AppendLine("And UseOrganID = @UseOrganID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, VIPParameterRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, VIPParameterRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@GrantFlag", DbType.String, VIPParameterRow.GrantFlag.Value)
            db.AddInParameter(dbcmd, "@UseCompID", DbType.String, VIPParameterRow.UseCompID.Value)
            db.AddInParameter(dbcmd, "@UseGroupID", DbType.String, VIPParameterRow.UseGroupID.Value)
            db.AddInParameter(dbcmd, "@UseOrganID", DbType.String, VIPParameterRow.UseOrganID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal VIPParameterRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update VIPParameter Set")
            For i As Integer = 0 To VIPParameterRow.FieldNames.Length - 1
                If Not VIPParameterRow.IsIdentityField(VIPParameterRow.FieldNames(i)) AndAlso VIPParameterRow.IsUpdated(VIPParameterRow.FieldNames(i)) AndAlso VIPParameterRow.CreateUpdateSQL(VIPParameterRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, VIPParameterRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And EmpID = @PKEmpID")
            strSQL.AppendLine("And GrantFlag = @PKGrantFlag")
            strSQL.AppendLine("And UseCompID = @PKUseCompID")
            strSQL.AppendLine("And UseGroupID = @PKUseGroupID")
            strSQL.AppendLine("And UseOrganID = @PKUseOrganID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If VIPParameterRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, VIPParameterRow.CompID.Value)
            If VIPParameterRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, VIPParameterRow.EmpID.Value)
            If VIPParameterRow.GrantFlag.Updated Then db.AddInParameter(dbcmd, "@GrantFlag", DbType.String, VIPParameterRow.GrantFlag.Value)
            If VIPParameterRow.AllCompIDFlag.Updated Then db.AddInParameter(dbcmd, "@AllCompIDFlag", DbType.String, VIPParameterRow.AllCompIDFlag.Value)
            If VIPParameterRow.UseCompID.Updated Then db.AddInParameter(dbcmd, "@UseCompID", DbType.String, VIPParameterRow.UseCompID.Value)
            If VIPParameterRow.AllGroupIDFlag.Updated Then db.AddInParameter(dbcmd, "@AllGroupIDFlag", DbType.String, VIPParameterRow.AllGroupIDFlag.Value)
            If VIPParameterRow.UseGroupID.Updated Then db.AddInParameter(dbcmd, "@UseGroupID", DbType.String, VIPParameterRow.UseGroupID.Value)
            If VIPParameterRow.AllOrganIDFlag.Updated Then db.AddInParameter(dbcmd, "@AllOrganIDFlag", DbType.String, VIPParameterRow.AllOrganIDFlag.Value)
            If VIPParameterRow.UseOrganID.Updated Then db.AddInParameter(dbcmd, "@UseOrganID", DbType.String, VIPParameterRow.UseOrganID.Value)
            If VIPParameterRow.UseOurColleagues.Updated Then db.AddInParameter(dbcmd, "@UseOurColleagues", DbType.String, VIPParameterRow.UseOurColleagues.Value)
            If VIPParameterRow.UseRankID.Updated Then db.AddInParameter(dbcmd, "@UseRankID", DbType.String, VIPParameterRow.UseRankID.Value)
            If VIPParameterRow.BeginDate.Updated Then db.AddInParameter(dbcmd, "@BeginDate", DbType.Date, IIf(IsDateTimeNull(VIPParameterRow.BeginDate.Value), Convert.ToDateTime("1900/1/1"), VIPParameterRow.BeginDate.Value))
            If VIPParameterRow.EndDate.Updated Then db.AddInParameter(dbcmd, "@EndDate", DbType.Date, IIf(IsDateTimeNull(VIPParameterRow.EndDate.Value), Convert.ToDateTime("1900/1/1"), VIPParameterRow.EndDate.Value))
            If VIPParameterRow.CreateComp.Updated Then db.AddInParameter(dbcmd, "@CreateComp", DbType.String, VIPParameterRow.CreateComp.Value)
            If VIPParameterRow.CreateID.Updated Then db.AddInParameter(dbcmd, "@CreateID", DbType.String, VIPParameterRow.CreateID.Value)
            If VIPParameterRow.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(VIPParameterRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), VIPParameterRow.CreateDate.Value))
            If VIPParameterRow.ReleaseComp.Updated Then db.AddInParameter(dbcmd, "@ReleaseComp", DbType.String, VIPParameterRow.ReleaseComp.Value)
            If VIPParameterRow.ReleaseID.Updated Then db.AddInParameter(dbcmd, "@ReleaseID", DbType.String, VIPParameterRow.ReleaseID.Value)
            If VIPParameterRow.ReleaseDate.Updated Then db.AddInParameter(dbcmd, "@ReleaseDate", DbType.Date, IIf(IsDateTimeNull(VIPParameterRow.ReleaseDate.Value), Convert.ToDateTime("1900/1/1"), VIPParameterRow.ReleaseDate.Value))
            If VIPParameterRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, VIPParameterRow.LastChgComp.Value)
            If VIPParameterRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, VIPParameterRow.LastChgID.Value)
            If VIPParameterRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(VIPParameterRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), VIPParameterRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(VIPParameterRow.LoadFromDataRow, VIPParameterRow.CompID.OldValue, VIPParameterRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(VIPParameterRow.LoadFromDataRow, VIPParameterRow.EmpID.OldValue, VIPParameterRow.EmpID.Value))
            db.AddInParameter(dbcmd, "@PKGrantFlag", DbType.String, IIf(VIPParameterRow.LoadFromDataRow, VIPParameterRow.GrantFlag.OldValue, VIPParameterRow.GrantFlag.Value))
            db.AddInParameter(dbcmd, "@PKUseCompID", DbType.String, IIf(VIPParameterRow.LoadFromDataRow, VIPParameterRow.UseCompID.OldValue, VIPParameterRow.UseCompID.Value))
            db.AddInParameter(dbcmd, "@PKUseGroupID", DbType.String, IIf(VIPParameterRow.LoadFromDataRow, VIPParameterRow.UseGroupID.OldValue, VIPParameterRow.UseGroupID.Value))
            db.AddInParameter(dbcmd, "@PKUseOrganID", DbType.String, IIf(VIPParameterRow.LoadFromDataRow, VIPParameterRow.UseOrganID.OldValue, VIPParameterRow.UseOrganID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal VIPParameterRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update VIPParameter Set")
            For i As Integer = 0 To VIPParameterRow.FieldNames.Length - 1
                If Not VIPParameterRow.IsIdentityField(VIPParameterRow.FieldNames(i)) AndAlso VIPParameterRow.IsUpdated(VIPParameterRow.FieldNames(i)) AndAlso VIPParameterRow.CreateUpdateSQL(VIPParameterRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, VIPParameterRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And EmpID = @PKEmpID")
            strSQL.AppendLine("And GrantFlag = @PKGrantFlag")
            strSQL.AppendLine("And UseCompID = @PKUseCompID")
            strSQL.AppendLine("And UseGroupID = @PKUseGroupID")
            strSQL.AppendLine("And UseOrganID = @PKUseOrganID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If VIPParameterRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, VIPParameterRow.CompID.Value)
            If VIPParameterRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, VIPParameterRow.EmpID.Value)
            If VIPParameterRow.GrantFlag.Updated Then db.AddInParameter(dbcmd, "@GrantFlag", DbType.String, VIPParameterRow.GrantFlag.Value)
            If VIPParameterRow.AllCompIDFlag.Updated Then db.AddInParameter(dbcmd, "@AllCompIDFlag", DbType.String, VIPParameterRow.AllCompIDFlag.Value)
            If VIPParameterRow.UseCompID.Updated Then db.AddInParameter(dbcmd, "@UseCompID", DbType.String, VIPParameterRow.UseCompID.Value)
            If VIPParameterRow.AllGroupIDFlag.Updated Then db.AddInParameter(dbcmd, "@AllGroupIDFlag", DbType.String, VIPParameterRow.AllGroupIDFlag.Value)
            If VIPParameterRow.UseGroupID.Updated Then db.AddInParameter(dbcmd, "@UseGroupID", DbType.String, VIPParameterRow.UseGroupID.Value)
            If VIPParameterRow.AllOrganIDFlag.Updated Then db.AddInParameter(dbcmd, "@AllOrganIDFlag", DbType.String, VIPParameterRow.AllOrganIDFlag.Value)
            If VIPParameterRow.UseOrganID.Updated Then db.AddInParameter(dbcmd, "@UseOrganID", DbType.String, VIPParameterRow.UseOrganID.Value)
            If VIPParameterRow.UseOurColleagues.Updated Then db.AddInParameter(dbcmd, "@UseOurColleagues", DbType.String, VIPParameterRow.UseOurColleagues.Value)
            If VIPParameterRow.UseRankID.Updated Then db.AddInParameter(dbcmd, "@UseRankID", DbType.String, VIPParameterRow.UseRankID.Value)
            If VIPParameterRow.BeginDate.Updated Then db.AddInParameter(dbcmd, "@BeginDate", DbType.Date, IIf(IsDateTimeNull(VIPParameterRow.BeginDate.Value), Convert.ToDateTime("1900/1/1"), VIPParameterRow.BeginDate.Value))
            If VIPParameterRow.EndDate.Updated Then db.AddInParameter(dbcmd, "@EndDate", DbType.Date, IIf(IsDateTimeNull(VIPParameterRow.EndDate.Value), Convert.ToDateTime("1900/1/1"), VIPParameterRow.EndDate.Value))
            If VIPParameterRow.CreateComp.Updated Then db.AddInParameter(dbcmd, "@CreateComp", DbType.String, VIPParameterRow.CreateComp.Value)
            If VIPParameterRow.CreateID.Updated Then db.AddInParameter(dbcmd, "@CreateID", DbType.String, VIPParameterRow.CreateID.Value)
            If VIPParameterRow.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(VIPParameterRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), VIPParameterRow.CreateDate.Value))
            If VIPParameterRow.ReleaseComp.Updated Then db.AddInParameter(dbcmd, "@ReleaseComp", DbType.String, VIPParameterRow.ReleaseComp.Value)
            If VIPParameterRow.ReleaseID.Updated Then db.AddInParameter(dbcmd, "@ReleaseID", DbType.String, VIPParameterRow.ReleaseID.Value)
            If VIPParameterRow.ReleaseDate.Updated Then db.AddInParameter(dbcmd, "@ReleaseDate", DbType.Date, IIf(IsDateTimeNull(VIPParameterRow.ReleaseDate.Value), Convert.ToDateTime("1900/1/1"), VIPParameterRow.ReleaseDate.Value))
            If VIPParameterRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, VIPParameterRow.LastChgComp.Value)
            If VIPParameterRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, VIPParameterRow.LastChgID.Value)
            If VIPParameterRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(VIPParameterRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), VIPParameterRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(VIPParameterRow.LoadFromDataRow, VIPParameterRow.CompID.OldValue, VIPParameterRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(VIPParameterRow.LoadFromDataRow, VIPParameterRow.EmpID.OldValue, VIPParameterRow.EmpID.Value))
            db.AddInParameter(dbcmd, "@PKGrantFlag", DbType.String, IIf(VIPParameterRow.LoadFromDataRow, VIPParameterRow.GrantFlag.OldValue, VIPParameterRow.GrantFlag.Value))
            db.AddInParameter(dbcmd, "@PKUseCompID", DbType.String, IIf(VIPParameterRow.LoadFromDataRow, VIPParameterRow.UseCompID.OldValue, VIPParameterRow.UseCompID.Value))
            db.AddInParameter(dbcmd, "@PKUseGroupID", DbType.String, IIf(VIPParameterRow.LoadFromDataRow, VIPParameterRow.UseGroupID.OldValue, VIPParameterRow.UseGroupID.Value))
            db.AddInParameter(dbcmd, "@PKUseOrganID", DbType.String, IIf(VIPParameterRow.LoadFromDataRow, VIPParameterRow.UseOrganID.OldValue, VIPParameterRow.UseOrganID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal VIPParameterRow As Row()) As Integer
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
                    For Each r As Row In VIPParameterRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update VIPParameter Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where CompID = @PKCompID")
                        strSQL.AppendLine("And EmpID = @PKEmpID")
                        strSQL.AppendLine("And GrantFlag = @PKGrantFlag")
                        strSQL.AppendLine("And UseCompID = @PKUseCompID")
                        strSQL.AppendLine("And UseGroupID = @PKUseGroupID")
                        strSQL.AppendLine("And UseOrganID = @PKUseOrganID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        If r.GrantFlag.Updated Then db.AddInParameter(dbcmd, "@GrantFlag", DbType.String, r.GrantFlag.Value)
                        If r.AllCompIDFlag.Updated Then db.AddInParameter(dbcmd, "@AllCompIDFlag", DbType.String, r.AllCompIDFlag.Value)
                        If r.UseCompID.Updated Then db.AddInParameter(dbcmd, "@UseCompID", DbType.String, r.UseCompID.Value)
                        If r.AllGroupIDFlag.Updated Then db.AddInParameter(dbcmd, "@AllGroupIDFlag", DbType.String, r.AllGroupIDFlag.Value)
                        If r.UseGroupID.Updated Then db.AddInParameter(dbcmd, "@UseGroupID", DbType.String, r.UseGroupID.Value)
                        If r.AllOrganIDFlag.Updated Then db.AddInParameter(dbcmd, "@AllOrganIDFlag", DbType.String, r.AllOrganIDFlag.Value)
                        If r.UseOrganID.Updated Then db.AddInParameter(dbcmd, "@UseOrganID", DbType.String, r.UseOrganID.Value)
                        If r.UseOurColleagues.Updated Then db.AddInParameter(dbcmd, "@UseOurColleagues", DbType.String, r.UseOurColleagues.Value)
                        If r.UseRankID.Updated Then db.AddInParameter(dbcmd, "@UseRankID", DbType.String, r.UseRankID.Value)
                        If r.BeginDate.Updated Then db.AddInParameter(dbcmd, "@BeginDate", DbType.Date, IIf(IsDateTimeNull(r.BeginDate.Value), Convert.ToDateTime("1900/1/1"), r.BeginDate.Value))
                        If r.EndDate.Updated Then db.AddInParameter(dbcmd, "@EndDate", DbType.Date, IIf(IsDateTimeNull(r.EndDate.Value), Convert.ToDateTime("1900/1/1"), r.EndDate.Value))
                        If r.CreateComp.Updated Then db.AddInParameter(dbcmd, "@CreateComp", DbType.String, r.CreateComp.Value)
                        If r.CreateID.Updated Then db.AddInParameter(dbcmd, "@CreateID", DbType.String, r.CreateID.Value)
                        If r.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                        If r.ReleaseComp.Updated Then db.AddInParameter(dbcmd, "@ReleaseComp", DbType.String, r.ReleaseComp.Value)
                        If r.ReleaseID.Updated Then db.AddInParameter(dbcmd, "@ReleaseID", DbType.String, r.ReleaseID.Value)
                        If r.ReleaseDate.Updated Then db.AddInParameter(dbcmd, "@ReleaseDate", DbType.Date, IIf(IsDateTimeNull(r.ReleaseDate.Value), Convert.ToDateTime("1900/1/1"), r.ReleaseDate.Value))
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                        db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))
                        db.AddInParameter(dbcmd, "@PKGrantFlag", DbType.String, IIf(r.LoadFromDataRow, r.GrantFlag.OldValue, r.GrantFlag.Value))
                        db.AddInParameter(dbcmd, "@PKUseCompID", DbType.String, IIf(r.LoadFromDataRow, r.UseCompID.OldValue, r.UseCompID.Value))
                        db.AddInParameter(dbcmd, "@PKUseGroupID", DbType.String, IIf(r.LoadFromDataRow, r.UseGroupID.OldValue, r.UseGroupID.Value))
                        db.AddInParameter(dbcmd, "@PKUseOrganID", DbType.String, IIf(r.LoadFromDataRow, r.UseOrganID.OldValue, r.UseOrganID.Value))

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

        Public Function Update(ByVal VIPParameterRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In VIPParameterRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update VIPParameter Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where CompID = @PKCompID")
                strSQL.AppendLine("And EmpID = @PKEmpID")
                strSQL.AppendLine("And GrantFlag = @PKGrantFlag")
                strSQL.AppendLine("And UseCompID = @PKUseCompID")
                strSQL.AppendLine("And UseGroupID = @PKUseGroupID")
                strSQL.AppendLine("And UseOrganID = @PKUseOrganID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                If r.GrantFlag.Updated Then db.AddInParameter(dbcmd, "@GrantFlag", DbType.String, r.GrantFlag.Value)
                If r.AllCompIDFlag.Updated Then db.AddInParameter(dbcmd, "@AllCompIDFlag", DbType.String, r.AllCompIDFlag.Value)
                If r.UseCompID.Updated Then db.AddInParameter(dbcmd, "@UseCompID", DbType.String, r.UseCompID.Value)
                If r.AllGroupIDFlag.Updated Then db.AddInParameter(dbcmd, "@AllGroupIDFlag", DbType.String, r.AllGroupIDFlag.Value)
                If r.UseGroupID.Updated Then db.AddInParameter(dbcmd, "@UseGroupID", DbType.String, r.UseGroupID.Value)
                If r.AllOrganIDFlag.Updated Then db.AddInParameter(dbcmd, "@AllOrganIDFlag", DbType.String, r.AllOrganIDFlag.Value)
                If r.UseOrganID.Updated Then db.AddInParameter(dbcmd, "@UseOrganID", DbType.String, r.UseOrganID.Value)
                If r.UseOurColleagues.Updated Then db.AddInParameter(dbcmd, "@UseOurColleagues", DbType.String, r.UseOurColleagues.Value)
                If r.UseRankID.Updated Then db.AddInParameter(dbcmd, "@UseRankID", DbType.String, r.UseRankID.Value)
                If r.BeginDate.Updated Then db.AddInParameter(dbcmd, "@BeginDate", DbType.Date, IIf(IsDateTimeNull(r.BeginDate.Value), Convert.ToDateTime("1900/1/1"), r.BeginDate.Value))
                If r.EndDate.Updated Then db.AddInParameter(dbcmd, "@EndDate", DbType.Date, IIf(IsDateTimeNull(r.EndDate.Value), Convert.ToDateTime("1900/1/1"), r.EndDate.Value))
                If r.CreateComp.Updated Then db.AddInParameter(dbcmd, "@CreateComp", DbType.String, r.CreateComp.Value)
                If r.CreateID.Updated Then db.AddInParameter(dbcmd, "@CreateID", DbType.String, r.CreateID.Value)
                If r.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                If r.ReleaseComp.Updated Then db.AddInParameter(dbcmd, "@ReleaseComp", DbType.String, r.ReleaseComp.Value)
                If r.ReleaseID.Updated Then db.AddInParameter(dbcmd, "@ReleaseID", DbType.String, r.ReleaseID.Value)
                If r.ReleaseDate.Updated Then db.AddInParameter(dbcmd, "@ReleaseDate", DbType.Date, IIf(IsDateTimeNull(r.ReleaseDate.Value), Convert.ToDateTime("1900/1/1"), r.ReleaseDate.Value))
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))
                db.AddInParameter(dbcmd, "@PKGrantFlag", DbType.String, IIf(r.LoadFromDataRow, r.GrantFlag.OldValue, r.GrantFlag.Value))
                db.AddInParameter(dbcmd, "@PKUseCompID", DbType.String, IIf(r.LoadFromDataRow, r.UseCompID.OldValue, r.UseCompID.Value))
                db.AddInParameter(dbcmd, "@PKUseGroupID", DbType.String, IIf(r.LoadFromDataRow, r.UseGroupID.OldValue, r.UseGroupID.Value))
                db.AddInParameter(dbcmd, "@PKUseOrganID", DbType.String, IIf(r.LoadFromDataRow, r.UseOrganID.OldValue, r.UseOrganID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal VIPParameterRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From VIPParameter")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And GrantFlag = @GrantFlag")
            strSQL.AppendLine("And UseCompID = @UseCompID")
            strSQL.AppendLine("And UseGroupID = @UseGroupID")
            strSQL.AppendLine("And UseOrganID = @UseOrganID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, VIPParameterRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, VIPParameterRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@GrantFlag", DbType.String, VIPParameterRow.GrantFlag.Value)
            db.AddInParameter(dbcmd, "@UseCompID", DbType.String, VIPParameterRow.UseCompID.Value)
            db.AddInParameter(dbcmd, "@UseGroupID", DbType.String, VIPParameterRow.UseGroupID.Value)
            db.AddInParameter(dbcmd, "@UseOrganID", DbType.String, VIPParameterRow.UseOrganID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal VIPParameterRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From VIPParameter")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And GrantFlag = @GrantFlag")
            strSQL.AppendLine("And UseCompID = @UseCompID")
            strSQL.AppendLine("And UseGroupID = @UseGroupID")
            strSQL.AppendLine("And UseOrganID = @UseOrganID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, VIPParameterRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, VIPParameterRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@GrantFlag", DbType.String, VIPParameterRow.GrantFlag.Value)
            db.AddInParameter(dbcmd, "@UseCompID", DbType.String, VIPParameterRow.UseCompID.Value)
            db.AddInParameter(dbcmd, "@UseGroupID", DbType.String, VIPParameterRow.UseGroupID.Value)
            db.AddInParameter(dbcmd, "@UseOrganID", DbType.String, VIPParameterRow.UseOrganID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From VIPParameter")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal VIPParameterRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into VIPParameter")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, GrantFlag, AllCompIDFlag, UseCompID, AllGroupIDFlag, UseGroupID, AllOrganIDFlag,")
            strSQL.AppendLine("    UseOrganID, UseOurColleagues, UseRankID, BeginDate, EndDate, CreateComp, CreateID, CreateDate,")
            strSQL.AppendLine("    ReleaseComp, ReleaseID, ReleaseDate, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @GrantFlag, @AllCompIDFlag, @UseCompID, @AllGroupIDFlag, @UseGroupID, @AllOrganIDFlag,")
            strSQL.AppendLine("    @UseOrganID, @UseOurColleagues, @UseRankID, @BeginDate, @EndDate, @CreateComp, @CreateID, @CreateDate,")
            strSQL.AppendLine("    @ReleaseComp, @ReleaseID, @ReleaseDate, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, VIPParameterRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, VIPParameterRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@GrantFlag", DbType.String, VIPParameterRow.GrantFlag.Value)
            db.AddInParameter(dbcmd, "@AllCompIDFlag", DbType.String, VIPParameterRow.AllCompIDFlag.Value)
            db.AddInParameter(dbcmd, "@UseCompID", DbType.String, VIPParameterRow.UseCompID.Value)
            db.AddInParameter(dbcmd, "@AllGroupIDFlag", DbType.String, VIPParameterRow.AllGroupIDFlag.Value)
            db.AddInParameter(dbcmd, "@UseGroupID", DbType.String, VIPParameterRow.UseGroupID.Value)
            db.AddInParameter(dbcmd, "@AllOrganIDFlag", DbType.String, VIPParameterRow.AllOrganIDFlag.Value)
            db.AddInParameter(dbcmd, "@UseOrganID", DbType.String, VIPParameterRow.UseOrganID.Value)
            db.AddInParameter(dbcmd, "@UseOurColleagues", DbType.String, VIPParameterRow.UseOurColleagues.Value)
            db.AddInParameter(dbcmd, "@UseRankID", DbType.String, VIPParameterRow.UseRankID.Value)
            db.AddInParameter(dbcmd, "@BeginDate", DbType.Date, IIf(IsDateTimeNull(VIPParameterRow.BeginDate.Value), Convert.ToDateTime("1900/1/1"), VIPParameterRow.BeginDate.Value))
            db.AddInParameter(dbcmd, "@EndDate", DbType.Date, IIf(IsDateTimeNull(VIPParameterRow.EndDate.Value), Convert.ToDateTime("1900/1/1"), VIPParameterRow.EndDate.Value))
            db.AddInParameter(dbcmd, "@CreateComp", DbType.String, VIPParameterRow.CreateComp.Value)
            db.AddInParameter(dbcmd, "@CreateID", DbType.String, VIPParameterRow.CreateID.Value)
            db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(VIPParameterRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), VIPParameterRow.CreateDate.Value))
            db.AddInParameter(dbcmd, "@ReleaseComp", DbType.String, VIPParameterRow.ReleaseComp.Value)
            db.AddInParameter(dbcmd, "@ReleaseID", DbType.String, VIPParameterRow.ReleaseID.Value)
            db.AddInParameter(dbcmd, "@ReleaseDate", DbType.Date, IIf(IsDateTimeNull(VIPParameterRow.ReleaseDate.Value), Convert.ToDateTime("1900/1/1"), VIPParameterRow.ReleaseDate.Value))
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, VIPParameterRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, VIPParameterRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(VIPParameterRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), VIPParameterRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal VIPParameterRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into VIPParameter")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, GrantFlag, AllCompIDFlag, UseCompID, AllGroupIDFlag, UseGroupID, AllOrganIDFlag,")
            strSQL.AppendLine("    UseOrganID, UseOurColleagues, UseRankID, BeginDate, EndDate, CreateComp, CreateID, CreateDate,")
            strSQL.AppendLine("    ReleaseComp, ReleaseID, ReleaseDate, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @GrantFlag, @AllCompIDFlag, @UseCompID, @AllGroupIDFlag, @UseGroupID, @AllOrganIDFlag,")
            strSQL.AppendLine("    @UseOrganID, @UseOurColleagues, @UseRankID, @BeginDate, @EndDate, @CreateComp, @CreateID, @CreateDate,")
            strSQL.AppendLine("    @ReleaseComp, @ReleaseID, @ReleaseDate, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, VIPParameterRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, VIPParameterRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@GrantFlag", DbType.String, VIPParameterRow.GrantFlag.Value)
            db.AddInParameter(dbcmd, "@AllCompIDFlag", DbType.String, VIPParameterRow.AllCompIDFlag.Value)
            db.AddInParameter(dbcmd, "@UseCompID", DbType.String, VIPParameterRow.UseCompID.Value)
            db.AddInParameter(dbcmd, "@AllGroupIDFlag", DbType.String, VIPParameterRow.AllGroupIDFlag.Value)
            db.AddInParameter(dbcmd, "@UseGroupID", DbType.String, VIPParameterRow.UseGroupID.Value)
            db.AddInParameter(dbcmd, "@AllOrganIDFlag", DbType.String, VIPParameterRow.AllOrganIDFlag.Value)
            db.AddInParameter(dbcmd, "@UseOrganID", DbType.String, VIPParameterRow.UseOrganID.Value)
            db.AddInParameter(dbcmd, "@UseOurColleagues", DbType.String, VIPParameterRow.UseOurColleagues.Value)
            db.AddInParameter(dbcmd, "@UseRankID", DbType.String, VIPParameterRow.UseRankID.Value)
            db.AddInParameter(dbcmd, "@BeginDate", DbType.Date, IIf(IsDateTimeNull(VIPParameterRow.BeginDate.Value), Convert.ToDateTime("1900/1/1"), VIPParameterRow.BeginDate.Value))
            db.AddInParameter(dbcmd, "@EndDate", DbType.Date, IIf(IsDateTimeNull(VIPParameterRow.EndDate.Value), Convert.ToDateTime("1900/1/1"), VIPParameterRow.EndDate.Value))
            db.AddInParameter(dbcmd, "@CreateComp", DbType.String, VIPParameterRow.CreateComp.Value)
            db.AddInParameter(dbcmd, "@CreateID", DbType.String, VIPParameterRow.CreateID.Value)
            db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(VIPParameterRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), VIPParameterRow.CreateDate.Value))
            db.AddInParameter(dbcmd, "@ReleaseComp", DbType.String, VIPParameterRow.ReleaseComp.Value)
            db.AddInParameter(dbcmd, "@ReleaseID", DbType.String, VIPParameterRow.ReleaseID.Value)
            db.AddInParameter(dbcmd, "@ReleaseDate", DbType.Date, IIf(IsDateTimeNull(VIPParameterRow.ReleaseDate.Value), Convert.ToDateTime("1900/1/1"), VIPParameterRow.ReleaseDate.Value))
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, VIPParameterRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, VIPParameterRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(VIPParameterRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), VIPParameterRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal VIPParameterRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into VIPParameter")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, GrantFlag, AllCompIDFlag, UseCompID, AllGroupIDFlag, UseGroupID, AllOrganIDFlag,")
            strSQL.AppendLine("    UseOrganID, UseOurColleagues, UseRankID, BeginDate, EndDate, CreateComp, CreateID, CreateDate,")
            strSQL.AppendLine("    ReleaseComp, ReleaseID, ReleaseDate, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @GrantFlag, @AllCompIDFlag, @UseCompID, @AllGroupIDFlag, @UseGroupID, @AllOrganIDFlag,")
            strSQL.AppendLine("    @UseOrganID, @UseOurColleagues, @UseRankID, @BeginDate, @EndDate, @CreateComp, @CreateID, @CreateDate,")
            strSQL.AppendLine("    @ReleaseComp, @ReleaseID, @ReleaseDate, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In VIPParameterRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@GrantFlag", DbType.String, r.GrantFlag.Value)
                        db.AddInParameter(dbcmd, "@AllCompIDFlag", DbType.String, r.AllCompIDFlag.Value)
                        db.AddInParameter(dbcmd, "@UseCompID", DbType.String, r.UseCompID.Value)
                        db.AddInParameter(dbcmd, "@AllGroupIDFlag", DbType.String, r.AllGroupIDFlag.Value)
                        db.AddInParameter(dbcmd, "@UseGroupID", DbType.String, r.UseGroupID.Value)
                        db.AddInParameter(dbcmd, "@AllOrganIDFlag", DbType.String, r.AllOrganIDFlag.Value)
                        db.AddInParameter(dbcmd, "@UseOrganID", DbType.String, r.UseOrganID.Value)
                        db.AddInParameter(dbcmd, "@UseOurColleagues", DbType.String, r.UseOurColleagues.Value)
                        db.AddInParameter(dbcmd, "@UseRankID", DbType.String, r.UseRankID.Value)
                        db.AddInParameter(dbcmd, "@BeginDate", DbType.Date, IIf(IsDateTimeNull(r.BeginDate.Value), Convert.ToDateTime("1900/1/1"), r.BeginDate.Value))
                        db.AddInParameter(dbcmd, "@EndDate", DbType.Date, IIf(IsDateTimeNull(r.EndDate.Value), Convert.ToDateTime("1900/1/1"), r.EndDate.Value))
                        db.AddInParameter(dbcmd, "@CreateComp", DbType.String, r.CreateComp.Value)
                        db.AddInParameter(dbcmd, "@CreateID", DbType.String, r.CreateID.Value)
                        db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                        db.AddInParameter(dbcmd, "@ReleaseComp", DbType.String, r.ReleaseComp.Value)
                        db.AddInParameter(dbcmd, "@ReleaseID", DbType.String, r.ReleaseID.Value)
                        db.AddInParameter(dbcmd, "@ReleaseDate", DbType.Date, IIf(IsDateTimeNull(r.ReleaseDate.Value), Convert.ToDateTime("1900/1/1"), r.ReleaseDate.Value))
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

        Public Function Insert(ByVal VIPParameterRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into VIPParameter")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, GrantFlag, AllCompIDFlag, UseCompID, AllGroupIDFlag, UseGroupID, AllOrganIDFlag,")
            strSQL.AppendLine("    UseOrganID, UseOurColleagues, UseRankID, BeginDate, EndDate, CreateComp, CreateID, CreateDate,")
            strSQL.AppendLine("    ReleaseComp, ReleaseID, ReleaseDate, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @GrantFlag, @AllCompIDFlag, @UseCompID, @AllGroupIDFlag, @UseGroupID, @AllOrganIDFlag,")
            strSQL.AppendLine("    @UseOrganID, @UseOurColleagues, @UseRankID, @BeginDate, @EndDate, @CreateComp, @CreateID, @CreateDate,")
            strSQL.AppendLine("    @ReleaseComp, @ReleaseID, @ReleaseDate, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In VIPParameterRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@GrantFlag", DbType.String, r.GrantFlag.Value)
                db.AddInParameter(dbcmd, "@AllCompIDFlag", DbType.String, r.AllCompIDFlag.Value)
                db.AddInParameter(dbcmd, "@UseCompID", DbType.String, r.UseCompID.Value)
                db.AddInParameter(dbcmd, "@AllGroupIDFlag", DbType.String, r.AllGroupIDFlag.Value)
                db.AddInParameter(dbcmd, "@UseGroupID", DbType.String, r.UseGroupID.Value)
                db.AddInParameter(dbcmd, "@AllOrganIDFlag", DbType.String, r.AllOrganIDFlag.Value)
                db.AddInParameter(dbcmd, "@UseOrganID", DbType.String, r.UseOrganID.Value)
                db.AddInParameter(dbcmd, "@UseOurColleagues", DbType.String, r.UseOurColleagues.Value)
                db.AddInParameter(dbcmd, "@UseRankID", DbType.String, r.UseRankID.Value)
                db.AddInParameter(dbcmd, "@BeginDate", DbType.Date, IIf(IsDateTimeNull(r.BeginDate.Value), Convert.ToDateTime("1900/1/1"), r.BeginDate.Value))
                db.AddInParameter(dbcmd, "@EndDate", DbType.Date, IIf(IsDateTimeNull(r.EndDate.Value), Convert.ToDateTime("1900/1/1"), r.EndDate.Value))
                db.AddInParameter(dbcmd, "@CreateComp", DbType.String, r.CreateComp.Value)
                db.AddInParameter(dbcmd, "@CreateID", DbType.String, r.CreateID.Value)
                db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                db.AddInParameter(dbcmd, "@ReleaseComp", DbType.String, r.ReleaseComp.Value)
                db.AddInParameter(dbcmd, "@ReleaseID", DbType.String, r.ReleaseID.Value)
                db.AddInParameter(dbcmd, "@ReleaseDate", DbType.Date, IIf(IsDateTimeNull(r.ReleaseDate.Value), Convert.ToDateTime("1900/1/1"), r.ReleaseDate.Value))
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

