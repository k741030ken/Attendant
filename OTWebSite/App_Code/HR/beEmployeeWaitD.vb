'****************************************************************
' Table:EmployeeWaitD
' Created Date: 2014.09.16
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beEmployeeWaitD
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "EmpID", "ValidDate", "Seq", "SubSeq", "NewCompID", "DeptID", "OrganID", "GroupID", "GroupOrganID" _
                                    , "RankID", "TitleID", "TitleName", "IsPartTimeBoss", "IsPartTimeGroupBoss", "ValidMark", "FlowOrganID", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(Date), GetType(Integer), GetType(Integer), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "EmpID", "ValidDate", "Seq", "SubSeq", "CompID" }

        Public ReadOnly Property Rows() As beEmployeeWaitD.Rows 
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
        Public Sub Transfer2Row(EmployeeWaitDTable As DataTable)
            For Each dr As DataRow In EmployeeWaitDTable.Rows
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
                dr(m_Rows(i).ValidDate.FieldName) = m_Rows(i).ValidDate.Value
                dr(m_Rows(i).Seq.FieldName) = m_Rows(i).Seq.Value
                dr(m_Rows(i).SubSeq.FieldName) = m_Rows(i).SubSeq.Value
                dr(m_Rows(i).NewCompID.FieldName) = m_Rows(i).NewCompID.Value
                dr(m_Rows(i).DeptID.FieldName) = m_Rows(i).DeptID.Value
                dr(m_Rows(i).OrganID.FieldName) = m_Rows(i).OrganID.Value
                dr(m_Rows(i).GroupID.FieldName) = m_Rows(i).GroupID.Value
                dr(m_Rows(i).GroupOrganID.FieldName) = m_Rows(i).GroupOrganID.Value
                dr(m_Rows(i).RankID.FieldName) = m_Rows(i).RankID.Value
                dr(m_Rows(i).TitleID.FieldName) = m_Rows(i).TitleID.Value
                dr(m_Rows(i).TitleName.FieldName) = m_Rows(i).TitleName.Value
                dr(m_Rows(i).IsPartTimeBoss.FieldName) = m_Rows(i).IsPartTimeBoss.Value
                dr(m_Rows(i).IsPartTimeGroupBoss.FieldName) = m_Rows(i).IsPartTimeGroupBoss.Value
                dr(m_Rows(i).ValidMark.FieldName) = m_Rows(i).ValidMark.Value
                dr(m_Rows(i).FlowOrganID.FieldName) = m_Rows(i).FlowOrganID.Value
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

        Public Sub Add(EmployeeWaitDRow As Row)
            m_Rows.Add(EmployeeWaitDRow)
        End Sub

        Public Sub Remove(EmployeeWaitDRow As Row)
            If m_Rows.IndexOf(EmployeeWaitDRow) >= 0 Then
                m_Rows.Remove(EmployeeWaitDRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_EmpID As Field(Of String) = new Field(Of String)("EmpID", true)
        Private FI_ValidDate As Field(Of Date) = new Field(Of Date)("ValidDate", true)
        Private FI_Seq As Field(Of Integer) = new Field(Of Integer)("Seq", true)
        Private FI_SubSeq As Field(Of Integer) = new Field(Of Integer)("SubSeq", true)
        Private FI_NewCompID As Field(Of String) = new Field(Of String)("NewCompID", true)
        Private FI_DeptID As Field(Of String) = new Field(Of String)("DeptID", true)
        Private FI_OrganID As Field(Of String) = new Field(Of String)("OrganID", true)
        Private FI_GroupID As Field(Of String) = new Field(Of String)("GroupID", true)
        Private FI_GroupOrganID As Field(Of String) = new Field(Of String)("GroupOrganID", true)
        Private FI_RankID As Field(Of String) = new Field(Of String)("RankID", true)
        Private FI_TitleID As Field(Of String) = new Field(Of String)("TitleID", true)
        Private FI_TitleName As Field(Of String) = new Field(Of String)("TitleName", true)
        Private FI_IsPartTimeBoss As Field(Of String) = new Field(Of String)("IsPartTimeBoss", true)
        Private FI_IsPartTimeGroupBoss As Field(Of String) = new Field(Of String)("IsPartTimeGroupBoss", true)
        Private FI_ValidMark As Field(Of String) = new Field(Of String)("ValidMark", true)
        Private FI_FlowOrganID As Field(Of String) = new Field(Of String)("FlowOrganID", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "CompID", "EmpID", "ValidDate", "Seq", "SubSeq", "NewCompID", "DeptID", "OrganID", "GroupID", "GroupOrganID" _
                                    , "RankID", "TitleID", "TitleName", "IsPartTimeBoss", "IsPartTimeGroupBoss", "ValidMark", "FlowOrganID", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "EmpID", "ValidDate", "Seq", "SubSeq", "CompID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CompID"
                    Return FI_CompID.Value
                Case "EmpID"
                    Return FI_EmpID.Value
                Case "ValidDate"
                    Return FI_ValidDate.Value
                Case "Seq"
                    Return FI_Seq.Value
                Case "SubSeq"
                    Return FI_SubSeq.Value
                Case "NewCompID"
                    Return FI_NewCompID.Value
                Case "DeptID"
                    Return FI_DeptID.Value
                Case "OrganID"
                    Return FI_OrganID.Value
                Case "GroupID"
                    Return FI_GroupID.Value
                Case "GroupOrganID"
                    Return FI_GroupOrganID.Value
                Case "RankID"
                    Return FI_RankID.Value
                Case "TitleID"
                    Return FI_TitleID.Value
                Case "TitleName"
                    Return FI_TitleName.Value
                Case "IsPartTimeBoss"
                    Return FI_IsPartTimeBoss.Value
                Case "IsPartTimeGroupBoss"
                    Return FI_IsPartTimeGroupBoss.Value
                Case "ValidMark"
                    Return FI_ValidMark.Value
                Case "FlowOrganID"
                    Return FI_FlowOrganID.Value
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
                Case "ValidDate"
                    FI_ValidDate.SetValue(value)
                Case "Seq"
                    FI_Seq.SetValue(value)
                Case "SubSeq"
                    FI_SubSeq.SetValue(value)
                Case "NewCompID"
                    FI_NewCompID.SetValue(value)
                Case "DeptID"
                    FI_DeptID.SetValue(value)
                Case "OrganID"
                    FI_OrganID.SetValue(value)
                Case "GroupID"
                    FI_GroupID.SetValue(value)
                Case "GroupOrganID"
                    FI_GroupOrganID.SetValue(value)
                Case "RankID"
                    FI_RankID.SetValue(value)
                Case "TitleID"
                    FI_TitleID.SetValue(value)
                Case "TitleName"
                    FI_TitleName.SetValue(value)
                Case "IsPartTimeBoss"
                    FI_IsPartTimeBoss.SetValue(value)
                Case "IsPartTimeGroupBoss"
                    FI_IsPartTimeGroupBoss.SetValue(value)
                Case "ValidMark"
                    FI_ValidMark.SetValue(value)
                Case "FlowOrganID"
                    FI_FlowOrganID.SetValue(value)
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
                Case "ValidDate"
                    return FI_ValidDate.Updated
                Case "Seq"
                    return FI_Seq.Updated
                Case "SubSeq"
                    return FI_SubSeq.Updated
                Case "NewCompID"
                    return FI_NewCompID.Updated
                Case "DeptID"
                    return FI_DeptID.Updated
                Case "OrganID"
                    return FI_OrganID.Updated
                Case "GroupID"
                    return FI_GroupID.Updated
                Case "GroupOrganID"
                    return FI_GroupOrganID.Updated
                Case "RankID"
                    return FI_RankID.Updated
                Case "TitleID"
                    return FI_TitleID.Updated
                Case "TitleName"
                    return FI_TitleName.Updated
                Case "IsPartTimeBoss"
                    return FI_IsPartTimeBoss.Updated
                Case "IsPartTimeGroupBoss"
                    return FI_IsPartTimeGroupBoss.Updated
                Case "ValidMark"
                    return FI_ValidMark.Updated
                Case "FlowOrganID"
                    return FI_FlowOrganID.Updated
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
                Case "ValidDate"
                    return FI_ValidDate.CreateUpdateSQL
                Case "Seq"
                    return FI_Seq.CreateUpdateSQL
                Case "SubSeq"
                    return FI_SubSeq.CreateUpdateSQL
                Case "NewCompID"
                    return FI_NewCompID.CreateUpdateSQL
                Case "DeptID"
                    return FI_DeptID.CreateUpdateSQL
                Case "OrganID"
                    return FI_OrganID.CreateUpdateSQL
                Case "GroupID"
                    return FI_GroupID.CreateUpdateSQL
                Case "GroupOrganID"
                    return FI_GroupOrganID.CreateUpdateSQL
                Case "RankID"
                    return FI_RankID.CreateUpdateSQL
                Case "TitleID"
                    return FI_TitleID.CreateUpdateSQL
                Case "TitleName"
                    return FI_TitleName.CreateUpdateSQL
                Case "IsPartTimeBoss"
                    return FI_IsPartTimeBoss.CreateUpdateSQL
                Case "IsPartTimeGroupBoss"
                    return FI_IsPartTimeGroupBoss.CreateUpdateSQL
                Case "ValidMark"
                    return FI_ValidMark.CreateUpdateSQL
                Case "FlowOrganID"
                    return FI_FlowOrganID.CreateUpdateSQL
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
            FI_ValidDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_Seq.SetInitValue(0)
            FI_SubSeq.SetInitValue(0)
            FI_NewCompID.SetInitValue("")
            FI_DeptID.SetInitValue("")
            FI_OrganID.SetInitValue("")
            FI_GroupID.SetInitValue("")
            FI_GroupOrganID.SetInitValue("")
            FI_RankID.SetInitValue("")
            FI_TitleID.SetInitValue("")
            FI_TitleName.SetInitValue("")
            FI_IsPartTimeBoss.SetInitValue("0")
            FI_IsPartTimeGroupBoss.SetInitValue("0")
            FI_ValidMark.SetInitValue("0")
            FI_FlowOrganID.SetInitValue("")
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CompID.SetInitValue(dr("CompID"))
            FI_EmpID.SetInitValue(dr("EmpID"))
            FI_ValidDate.SetInitValue(dr("ValidDate"))
            FI_Seq.SetInitValue(dr("Seq"))
            FI_SubSeq.SetInitValue(dr("SubSeq"))
            FI_NewCompID.SetInitValue(dr("NewCompID"))
            FI_DeptID.SetInitValue(dr("DeptID"))
            FI_OrganID.SetInitValue(dr("OrganID"))
            FI_GroupID.SetInitValue(dr("GroupID"))
            FI_GroupOrganID.SetInitValue(dr("GroupOrganID"))
            FI_RankID.SetInitValue(dr("RankID"))
            FI_TitleID.SetInitValue(dr("TitleID"))
            FI_TitleName.SetInitValue(dr("TitleName"))
            FI_IsPartTimeBoss.SetInitValue(dr("IsPartTimeBoss"))
            FI_IsPartTimeGroupBoss.SetInitValue(dr("IsPartTimeGroupBoss"))
            FI_ValidMark.SetInitValue(dr("ValidMark"))
            FI_FlowOrganID.SetInitValue(dr("FlowOrganID"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CompID.Updated = False
            FI_EmpID.Updated = False
            FI_ValidDate.Updated = False
            FI_Seq.Updated = False
            FI_SubSeq.Updated = False
            FI_NewCompID.Updated = False
            FI_DeptID.Updated = False
            FI_OrganID.Updated = False
            FI_GroupID.Updated = False
            FI_GroupOrganID.Updated = False
            FI_RankID.Updated = False
            FI_TitleID.Updated = False
            FI_TitleName.Updated = False
            FI_IsPartTimeBoss.Updated = False
            FI_IsPartTimeGroupBoss.Updated = False
            FI_ValidMark.Updated = False
            FI_FlowOrganID.Updated = False
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

        Public ReadOnly Property ValidDate As Field(Of Date) 
            Get
                Return FI_ValidDate
            End Get
        End Property

        Public ReadOnly Property Seq As Field(Of Integer) 
            Get
                Return FI_Seq
            End Get
        End Property

        Public ReadOnly Property SubSeq As Field(Of Integer) 
            Get
                Return FI_SubSeq
            End Get
        End Property

        Public ReadOnly Property NewCompID As Field(Of String) 
            Get
                Return FI_NewCompID
            End Get
        End Property

        Public ReadOnly Property DeptID As Field(Of String) 
            Get
                Return FI_DeptID
            End Get
        End Property

        Public ReadOnly Property OrganID As Field(Of String) 
            Get
                Return FI_OrganID
            End Get
        End Property

        Public ReadOnly Property GroupID As Field(Of String) 
            Get
                Return FI_GroupID
            End Get
        End Property

        Public ReadOnly Property GroupOrganID As Field(Of String) 
            Get
                Return FI_GroupOrganID
            End Get
        End Property

        Public ReadOnly Property RankID As Field(Of String) 
            Get
                Return FI_RankID
            End Get
        End Property

        Public ReadOnly Property TitleID As Field(Of String) 
            Get
                Return FI_TitleID
            End Get
        End Property

        Public ReadOnly Property TitleName As Field(Of String) 
            Get
                Return FI_TitleName
            End Get
        End Property

        Public ReadOnly Property IsPartTimeBoss As Field(Of String) 
            Get
                Return FI_IsPartTimeBoss
            End Get
        End Property

        Public ReadOnly Property IsPartTimeGroupBoss As Field(Of String) 
            Get
                Return FI_IsPartTimeGroupBoss
            End Get
        End Property

        Public ReadOnly Property ValidMark As Field(Of String) 
            Get
                Return FI_ValidMark
            End Get
        End Property

        Public ReadOnly Property FlowOrganID As Field(Of String) 
            Get
                Return FI_FlowOrganID
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
        Public Function DeleteRowByPrimaryKey(ByVal EmployeeWaitDRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From EmployeeWaitD")
            strSQL.AppendLine("Where EmpID = @EmpID")
            strSQL.AppendLine("And ValidDate = @ValidDate")
            strSQL.AppendLine("And Seq = @Seq")
            strSQL.AppendLine("And SubSeq = @SubSeq")
            strSQL.AppendLine("And CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmployeeWaitDRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, EmployeeWaitDRow.ValidDate.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EmployeeWaitDRow.Seq.Value)
            db.AddInParameter(dbcmd, "@SubSeq", DbType.Int32, EmployeeWaitDRow.SubSeq.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmployeeWaitDRow.CompID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal EmployeeWaitDRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From EmployeeWaitD")
            strSQL.AppendLine("Where EmpID = @EmpID")
            strSQL.AppendLine("And ValidDate = @ValidDate")
            strSQL.AppendLine("And Seq = @Seq")
            strSQL.AppendLine("And SubSeq = @SubSeq")
            strSQL.AppendLine("And CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmployeeWaitDRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, EmployeeWaitDRow.ValidDate.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EmployeeWaitDRow.Seq.Value)
            db.AddInParameter(dbcmd, "@SubSeq", DbType.Int32, EmployeeWaitDRow.SubSeq.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmployeeWaitDRow.CompID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal EmployeeWaitDRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From EmployeeWaitD")
            strSQL.AppendLine("Where EmpID = @EmpID")
            strSQL.AppendLine("And ValidDate = @ValidDate")
            strSQL.AppendLine("And Seq = @Seq")
            strSQL.AppendLine("And SubSeq = @SubSeq")
            strSQL.AppendLine("And CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In EmployeeWaitDRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, r.ValidDate.Value)
                        db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                        db.AddInParameter(dbcmd, "@SubSeq", DbType.Int32, r.SubSeq.Value)
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal EmployeeWaitDRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From EmployeeWaitD")
            strSQL.AppendLine("Where EmpID = @EmpID")
            strSQL.AppendLine("And ValidDate = @ValidDate")
            strSQL.AppendLine("And Seq = @Seq")
            strSQL.AppendLine("And SubSeq = @SubSeq")
            strSQL.AppendLine("And CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In EmployeeWaitDRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, r.ValidDate.Value)
                db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                db.AddInParameter(dbcmd, "@SubSeq", DbType.Int32, r.SubSeq.Value)
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal EmployeeWaitDRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmployeeWaitD")
            strSQL.AppendLine("Where EmpID = @EmpID")
            strSQL.AppendLine("And ValidDate = @ValidDate")
            strSQL.AppendLine("And Seq = @Seq")
            strSQL.AppendLine("And SubSeq = @SubSeq")
            strSQL.AppendLine("And CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmployeeWaitDRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, EmployeeWaitDRow.ValidDate.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EmployeeWaitDRow.Seq.Value)
            db.AddInParameter(dbcmd, "@SubSeq", DbType.Int32, EmployeeWaitDRow.SubSeq.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmployeeWaitDRow.CompID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(EmployeeWaitDRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmployeeWaitD")
            strSQL.AppendLine("Where EmpID = @EmpID")
            strSQL.AppendLine("And ValidDate = @ValidDate")
            strSQL.AppendLine("And Seq = @Seq")
            strSQL.AppendLine("And SubSeq = @SubSeq")
            strSQL.AppendLine("And CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmployeeWaitDRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, EmployeeWaitDRow.ValidDate.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EmployeeWaitDRow.Seq.Value)
            db.AddInParameter(dbcmd, "@SubSeq", DbType.Int32, EmployeeWaitDRow.SubSeq.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmployeeWaitDRow.CompID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal EmployeeWaitDRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update EmployeeWaitD Set")
            For i As Integer = 0 To EmployeeWaitDRow.FieldNames.Length - 1
                If Not EmployeeWaitDRow.IsIdentityField(EmployeeWaitDRow.FieldNames(i)) AndAlso EmployeeWaitDRow.IsUpdated(EmployeeWaitDRow.FieldNames(i)) AndAlso EmployeeWaitDRow.CreateUpdateSQL(EmployeeWaitDRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, EmployeeWaitDRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where EmpID = @PKEmpID")
            strSQL.AppendLine("And ValidDate = @PKValidDate")
            strSQL.AppendLine("And Seq = @PKSeq")
            strSQL.AppendLine("And SubSeq = @PKSubSeq")
            strSQL.AppendLine("And CompID = @PKCompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If EmployeeWaitDRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, EmployeeWaitDRow.CompID.Value)
            If EmployeeWaitDRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmployeeWaitDRow.EmpID.Value)
            If EmployeeWaitDRow.ValidDate.Updated Then db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(EmployeeWaitDRow.ValidDate.Value), Convert.ToDateTime("1900/1/1"), EmployeeWaitDRow.ValidDate.Value))
            If EmployeeWaitDRow.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EmployeeWaitDRow.Seq.Value)
            If EmployeeWaitDRow.SubSeq.Updated Then db.AddInParameter(dbcmd, "@SubSeq", DbType.Int32, EmployeeWaitDRow.SubSeq.Value)
            If EmployeeWaitDRow.NewCompID.Updated Then db.AddInParameter(dbcmd, "@NewCompID", DbType.String, EmployeeWaitDRow.NewCompID.Value)
            If EmployeeWaitDRow.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, EmployeeWaitDRow.DeptID.Value)
            If EmployeeWaitDRow.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, EmployeeWaitDRow.OrganID.Value)
            If EmployeeWaitDRow.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, EmployeeWaitDRow.GroupID.Value)
            If EmployeeWaitDRow.GroupOrganID.Updated Then db.AddInParameter(dbcmd, "@GroupOrganID", DbType.String, EmployeeWaitDRow.GroupOrganID.Value)
            If EmployeeWaitDRow.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, EmployeeWaitDRow.RankID.Value)
            If EmployeeWaitDRow.TitleID.Updated Then db.AddInParameter(dbcmd, "@TitleID", DbType.String, EmployeeWaitDRow.TitleID.Value)
            If EmployeeWaitDRow.TitleName.Updated Then db.AddInParameter(dbcmd, "@TitleName", DbType.String, EmployeeWaitDRow.TitleName.Value)
            If EmployeeWaitDRow.IsPartTimeBoss.Updated Then db.AddInParameter(dbcmd, "@IsPartTimeBoss", DbType.String, EmployeeWaitDRow.IsPartTimeBoss.Value)
            If EmployeeWaitDRow.IsPartTimeGroupBoss.Updated Then db.AddInParameter(dbcmd, "@IsPartTimeGroupBoss", DbType.String, EmployeeWaitDRow.IsPartTimeGroupBoss.Value)
            If EmployeeWaitDRow.ValidMark.Updated Then db.AddInParameter(dbcmd, "@ValidMark", DbType.String, EmployeeWaitDRow.ValidMark.Value)
            If EmployeeWaitDRow.FlowOrganID.Updated Then db.AddInParameter(dbcmd, "@FlowOrganID", DbType.String, EmployeeWaitDRow.FlowOrganID.Value)
            If EmployeeWaitDRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmployeeWaitDRow.LastChgComp.Value)
            If EmployeeWaitDRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmployeeWaitDRow.LastChgID.Value)
            If EmployeeWaitDRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmployeeWaitDRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmployeeWaitDRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(EmployeeWaitDRow.LoadFromDataRow, EmployeeWaitDRow.EmpID.OldValue, EmployeeWaitDRow.EmpID.Value))
            db.AddInParameter(dbcmd, "@PKValidDate", DbType.Date, IIf(EmployeeWaitDRow.LoadFromDataRow, EmployeeWaitDRow.ValidDate.OldValue, EmployeeWaitDRow.ValidDate.Value))
            db.AddInParameter(dbcmd, "@PKSeq", DbType.Int32, IIf(EmployeeWaitDRow.LoadFromDataRow, EmployeeWaitDRow.Seq.OldValue, EmployeeWaitDRow.Seq.Value))
            db.AddInParameter(dbcmd, "@PKSubSeq", DbType.Int32, IIf(EmployeeWaitDRow.LoadFromDataRow, EmployeeWaitDRow.SubSeq.OldValue, EmployeeWaitDRow.SubSeq.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(EmployeeWaitDRow.LoadFromDataRow, EmployeeWaitDRow.CompID.OldValue, EmployeeWaitDRow.CompID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal EmployeeWaitDRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update EmployeeWaitD Set")
            For i As Integer = 0 To EmployeeWaitDRow.FieldNames.Length - 1
                If Not EmployeeWaitDRow.IsIdentityField(EmployeeWaitDRow.FieldNames(i)) AndAlso EmployeeWaitDRow.IsUpdated(EmployeeWaitDRow.FieldNames(i)) AndAlso EmployeeWaitDRow.CreateUpdateSQL(EmployeeWaitDRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, EmployeeWaitDRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where EmpID = @PKEmpID")
            strSQL.AppendLine("And ValidDate = @PKValidDate")
            strSQL.AppendLine("And Seq = @PKSeq")
            strSQL.AppendLine("And SubSeq = @PKSubSeq")
            strSQL.AppendLine("And CompID = @PKCompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If EmployeeWaitDRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, EmployeeWaitDRow.CompID.Value)
            If EmployeeWaitDRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmployeeWaitDRow.EmpID.Value)
            If EmployeeWaitDRow.ValidDate.Updated Then db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(EmployeeWaitDRow.ValidDate.Value), Convert.ToDateTime("1900/1/1"), EmployeeWaitDRow.ValidDate.Value))
            If EmployeeWaitDRow.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EmployeeWaitDRow.Seq.Value)
            If EmployeeWaitDRow.SubSeq.Updated Then db.AddInParameter(dbcmd, "@SubSeq", DbType.Int32, EmployeeWaitDRow.SubSeq.Value)
            If EmployeeWaitDRow.NewCompID.Updated Then db.AddInParameter(dbcmd, "@NewCompID", DbType.String, EmployeeWaitDRow.NewCompID.Value)
            If EmployeeWaitDRow.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, EmployeeWaitDRow.DeptID.Value)
            If EmployeeWaitDRow.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, EmployeeWaitDRow.OrganID.Value)
            If EmployeeWaitDRow.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, EmployeeWaitDRow.GroupID.Value)
            If EmployeeWaitDRow.GroupOrganID.Updated Then db.AddInParameter(dbcmd, "@GroupOrganID", DbType.String, EmployeeWaitDRow.GroupOrganID.Value)
            If EmployeeWaitDRow.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, EmployeeWaitDRow.RankID.Value)
            If EmployeeWaitDRow.TitleID.Updated Then db.AddInParameter(dbcmd, "@TitleID", DbType.String, EmployeeWaitDRow.TitleID.Value)
            If EmployeeWaitDRow.TitleName.Updated Then db.AddInParameter(dbcmd, "@TitleName", DbType.String, EmployeeWaitDRow.TitleName.Value)
            If EmployeeWaitDRow.IsPartTimeBoss.Updated Then db.AddInParameter(dbcmd, "@IsPartTimeBoss", DbType.String, EmployeeWaitDRow.IsPartTimeBoss.Value)
            If EmployeeWaitDRow.IsPartTimeGroupBoss.Updated Then db.AddInParameter(dbcmd, "@IsPartTimeGroupBoss", DbType.String, EmployeeWaitDRow.IsPartTimeGroupBoss.Value)
            If EmployeeWaitDRow.ValidMark.Updated Then db.AddInParameter(dbcmd, "@ValidMark", DbType.String, EmployeeWaitDRow.ValidMark.Value)
            If EmployeeWaitDRow.FlowOrganID.Updated Then db.AddInParameter(dbcmd, "@FlowOrganID", DbType.String, EmployeeWaitDRow.FlowOrganID.Value)
            If EmployeeWaitDRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmployeeWaitDRow.LastChgComp.Value)
            If EmployeeWaitDRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmployeeWaitDRow.LastChgID.Value)
            If EmployeeWaitDRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmployeeWaitDRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmployeeWaitDRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(EmployeeWaitDRow.LoadFromDataRow, EmployeeWaitDRow.EmpID.OldValue, EmployeeWaitDRow.EmpID.Value))
            db.AddInParameter(dbcmd, "@PKValidDate", DbType.Date, IIf(EmployeeWaitDRow.LoadFromDataRow, EmployeeWaitDRow.ValidDate.OldValue, EmployeeWaitDRow.ValidDate.Value))
            db.AddInParameter(dbcmd, "@PKSeq", DbType.Int32, IIf(EmployeeWaitDRow.LoadFromDataRow, EmployeeWaitDRow.Seq.OldValue, EmployeeWaitDRow.Seq.Value))
            db.AddInParameter(dbcmd, "@PKSubSeq", DbType.Int32, IIf(EmployeeWaitDRow.LoadFromDataRow, EmployeeWaitDRow.SubSeq.OldValue, EmployeeWaitDRow.SubSeq.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(EmployeeWaitDRow.LoadFromDataRow, EmployeeWaitDRow.CompID.OldValue, EmployeeWaitDRow.CompID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal EmployeeWaitDRow As Row()) As Integer
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
                    For Each r As Row In EmployeeWaitDRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update EmployeeWaitD Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where EmpID = @PKEmpID")
                        strSQL.AppendLine("And ValidDate = @PKValidDate")
                        strSQL.AppendLine("And Seq = @PKSeq")
                        strSQL.AppendLine("And SubSeq = @PKSubSeq")
                        strSQL.AppendLine("And CompID = @PKCompID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        If r.ValidDate.Updated Then db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(r.ValidDate.Value), Convert.ToDateTime("1900/1/1"), r.ValidDate.Value))
                        If r.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                        If r.SubSeq.Updated Then db.AddInParameter(dbcmd, "@SubSeq", DbType.Int32, r.SubSeq.Value)
                        If r.NewCompID.Updated Then db.AddInParameter(dbcmd, "@NewCompID", DbType.String, r.NewCompID.Value)
                        If r.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                        If r.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                        If r.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                        If r.GroupOrganID.Updated Then db.AddInParameter(dbcmd, "@GroupOrganID", DbType.String, r.GroupOrganID.Value)
                        If r.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                        If r.TitleID.Updated Then db.AddInParameter(dbcmd, "@TitleID", DbType.String, r.TitleID.Value)
                        If r.TitleName.Updated Then db.AddInParameter(dbcmd, "@TitleName", DbType.String, r.TitleName.Value)
                        If r.IsPartTimeBoss.Updated Then db.AddInParameter(dbcmd, "@IsPartTimeBoss", DbType.String, r.IsPartTimeBoss.Value)
                        If r.IsPartTimeGroupBoss.Updated Then db.AddInParameter(dbcmd, "@IsPartTimeGroupBoss", DbType.String, r.IsPartTimeGroupBoss.Value)
                        If r.ValidMark.Updated Then db.AddInParameter(dbcmd, "@ValidMark", DbType.String, r.ValidMark.Value)
                        If r.FlowOrganID.Updated Then db.AddInParameter(dbcmd, "@FlowOrganID", DbType.String, r.FlowOrganID.Value)
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))
                        db.AddInParameter(dbcmd, "@PKValidDate", DbType.Date, IIf(r.LoadFromDataRow, r.ValidDate.OldValue, r.ValidDate.Value))
                        db.AddInParameter(dbcmd, "@PKSeq", DbType.Int32, IIf(r.LoadFromDataRow, r.Seq.OldValue, r.Seq.Value))
                        db.AddInParameter(dbcmd, "@PKSubSeq", DbType.Int32, IIf(r.LoadFromDataRow, r.SubSeq.OldValue, r.SubSeq.Value))
                        db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))

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

        Public Function Update(ByVal EmployeeWaitDRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In EmployeeWaitDRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update EmployeeWaitD Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where EmpID = @PKEmpID")
                strSQL.AppendLine("And ValidDate = @PKValidDate")
                strSQL.AppendLine("And Seq = @PKSeq")
                strSQL.AppendLine("And SubSeq = @PKSubSeq")
                strSQL.AppendLine("And CompID = @PKCompID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                If r.ValidDate.Updated Then db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(r.ValidDate.Value), Convert.ToDateTime("1900/1/1"), r.ValidDate.Value))
                If r.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                If r.SubSeq.Updated Then db.AddInParameter(dbcmd, "@SubSeq", DbType.Int32, r.SubSeq.Value)
                If r.NewCompID.Updated Then db.AddInParameter(dbcmd, "@NewCompID", DbType.String, r.NewCompID.Value)
                If r.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                If r.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                If r.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                If r.GroupOrganID.Updated Then db.AddInParameter(dbcmd, "@GroupOrganID", DbType.String, r.GroupOrganID.Value)
                If r.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                If r.TitleID.Updated Then db.AddInParameter(dbcmd, "@TitleID", DbType.String, r.TitleID.Value)
                If r.TitleName.Updated Then db.AddInParameter(dbcmd, "@TitleName", DbType.String, r.TitleName.Value)
                If r.IsPartTimeBoss.Updated Then db.AddInParameter(dbcmd, "@IsPartTimeBoss", DbType.String, r.IsPartTimeBoss.Value)
                If r.IsPartTimeGroupBoss.Updated Then db.AddInParameter(dbcmd, "@IsPartTimeGroupBoss", DbType.String, r.IsPartTimeGroupBoss.Value)
                If r.ValidMark.Updated Then db.AddInParameter(dbcmd, "@ValidMark", DbType.String, r.ValidMark.Value)
                If r.FlowOrganID.Updated Then db.AddInParameter(dbcmd, "@FlowOrganID", DbType.String, r.FlowOrganID.Value)
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))
                db.AddInParameter(dbcmd, "@PKValidDate", DbType.Date, IIf(r.LoadFromDataRow, r.ValidDate.OldValue, r.ValidDate.Value))
                db.AddInParameter(dbcmd, "@PKSeq", DbType.Int32, IIf(r.LoadFromDataRow, r.Seq.OldValue, r.Seq.Value))
                db.AddInParameter(dbcmd, "@PKSubSeq", DbType.Int32, IIf(r.LoadFromDataRow, r.SubSeq.OldValue, r.SubSeq.Value))
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal EmployeeWaitDRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From EmployeeWaitD")
            strSQL.AppendLine("Where EmpID = @EmpID")
            strSQL.AppendLine("And ValidDate = @ValidDate")
            strSQL.AppendLine("And Seq = @Seq")
            strSQL.AppendLine("And SubSeq = @SubSeq")
            strSQL.AppendLine("And CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmployeeWaitDRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, EmployeeWaitDRow.ValidDate.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EmployeeWaitDRow.Seq.Value)
            db.AddInParameter(dbcmd, "@SubSeq", DbType.Int32, EmployeeWaitDRow.SubSeq.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmployeeWaitDRow.CompID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal EmployeeWaitDRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From EmployeeWaitD")
            strSQL.AppendLine("Where EmpID = @EmpID")
            strSQL.AppendLine("And ValidDate = @ValidDate")
            strSQL.AppendLine("And Seq = @Seq")
            strSQL.AppendLine("And SubSeq = @SubSeq")
            strSQL.AppendLine("And CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmployeeWaitDRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, EmployeeWaitDRow.ValidDate.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EmployeeWaitDRow.Seq.Value)
            db.AddInParameter(dbcmd, "@SubSeq", DbType.Int32, EmployeeWaitDRow.SubSeq.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmployeeWaitDRow.CompID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmployeeWaitD")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal EmployeeWaitDRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into EmployeeWaitD")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, ValidDate, Seq, SubSeq, NewCompID, DeptID, OrganID, GroupID, GroupOrganID,")
            strSQL.AppendLine("    RankID, TitleID, TitleName, IsPartTimeBoss, IsPartTimeGroupBoss, ValidMark, FlowOrganID,")
            strSQL.AppendLine("    LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @ValidDate, @Seq, @SubSeq, @NewCompID, @DeptID, @OrganID, @GroupID, @GroupOrganID,")
            strSQL.AppendLine("    @RankID, @TitleID, @TitleName, @IsPartTimeBoss, @IsPartTimeGroupBoss, @ValidMark, @FlowOrganID,")
            strSQL.AppendLine("    @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmployeeWaitDRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmployeeWaitDRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(EmployeeWaitDRow.ValidDate.Value), Convert.ToDateTime("1900/1/1"), EmployeeWaitDRow.ValidDate.Value))
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EmployeeWaitDRow.Seq.Value)
            db.AddInParameter(dbcmd, "@SubSeq", DbType.Int32, EmployeeWaitDRow.SubSeq.Value)
            db.AddInParameter(dbcmd, "@NewCompID", DbType.String, EmployeeWaitDRow.NewCompID.Value)
            db.AddInParameter(dbcmd, "@DeptID", DbType.String, EmployeeWaitDRow.DeptID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, EmployeeWaitDRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@GroupID", DbType.String, EmployeeWaitDRow.GroupID.Value)
            db.AddInParameter(dbcmd, "@GroupOrganID", DbType.String, EmployeeWaitDRow.GroupOrganID.Value)
            db.AddInParameter(dbcmd, "@RankID", DbType.String, EmployeeWaitDRow.RankID.Value)
            db.AddInParameter(dbcmd, "@TitleID", DbType.String, EmployeeWaitDRow.TitleID.Value)
            db.AddInParameter(dbcmd, "@TitleName", DbType.String, EmployeeWaitDRow.TitleName.Value)
            db.AddInParameter(dbcmd, "@IsPartTimeBoss", DbType.String, EmployeeWaitDRow.IsPartTimeBoss.Value)
            db.AddInParameter(dbcmd, "@IsPartTimeGroupBoss", DbType.String, EmployeeWaitDRow.IsPartTimeGroupBoss.Value)
            db.AddInParameter(dbcmd, "@ValidMark", DbType.String, EmployeeWaitDRow.ValidMark.Value)
            db.AddInParameter(dbcmd, "@FlowOrganID", DbType.String, EmployeeWaitDRow.FlowOrganID.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmployeeWaitDRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmployeeWaitDRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmployeeWaitDRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmployeeWaitDRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal EmployeeWaitDRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into EmployeeWaitD")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, ValidDate, Seq, SubSeq, NewCompID, DeptID, OrganID, GroupID, GroupOrganID,")
            strSQL.AppendLine("    RankID, TitleID, TitleName, IsPartTimeBoss, IsPartTimeGroupBoss, ValidMark, FlowOrganID,")
            strSQL.AppendLine("    LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @ValidDate, @Seq, @SubSeq, @NewCompID, @DeptID, @OrganID, @GroupID, @GroupOrganID,")
            strSQL.AppendLine("    @RankID, @TitleID, @TitleName, @IsPartTimeBoss, @IsPartTimeGroupBoss, @ValidMark, @FlowOrganID,")
            strSQL.AppendLine("    @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmployeeWaitDRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmployeeWaitDRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(EmployeeWaitDRow.ValidDate.Value), Convert.ToDateTime("1900/1/1"), EmployeeWaitDRow.ValidDate.Value))
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EmployeeWaitDRow.Seq.Value)
            db.AddInParameter(dbcmd, "@SubSeq", DbType.Int32, EmployeeWaitDRow.SubSeq.Value)
            db.AddInParameter(dbcmd, "@NewCompID", DbType.String, EmployeeWaitDRow.NewCompID.Value)
            db.AddInParameter(dbcmd, "@DeptID", DbType.String, EmployeeWaitDRow.DeptID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, EmployeeWaitDRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@GroupID", DbType.String, EmployeeWaitDRow.GroupID.Value)
            db.AddInParameter(dbcmd, "@GroupOrganID", DbType.String, EmployeeWaitDRow.GroupOrganID.Value)
            db.AddInParameter(dbcmd, "@RankID", DbType.String, EmployeeWaitDRow.RankID.Value)
            db.AddInParameter(dbcmd, "@TitleID", DbType.String, EmployeeWaitDRow.TitleID.Value)
            db.AddInParameter(dbcmd, "@TitleName", DbType.String, EmployeeWaitDRow.TitleName.Value)
            db.AddInParameter(dbcmd, "@IsPartTimeBoss", DbType.String, EmployeeWaitDRow.IsPartTimeBoss.Value)
            db.AddInParameter(dbcmd, "@IsPartTimeGroupBoss", DbType.String, EmployeeWaitDRow.IsPartTimeGroupBoss.Value)
            db.AddInParameter(dbcmd, "@ValidMark", DbType.String, EmployeeWaitDRow.ValidMark.Value)
            db.AddInParameter(dbcmd, "@FlowOrganID", DbType.String, EmployeeWaitDRow.FlowOrganID.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmployeeWaitDRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmployeeWaitDRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmployeeWaitDRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmployeeWaitDRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal EmployeeWaitDRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into EmployeeWaitD")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, ValidDate, Seq, SubSeq, NewCompID, DeptID, OrganID, GroupID, GroupOrganID,")
            strSQL.AppendLine("    RankID, TitleID, TitleName, IsPartTimeBoss, IsPartTimeGroupBoss, ValidMark, FlowOrganID,")
            strSQL.AppendLine("    LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @ValidDate, @Seq, @SubSeq, @NewCompID, @DeptID, @OrganID, @GroupID, @GroupOrganID,")
            strSQL.AppendLine("    @RankID, @TitleID, @TitleName, @IsPartTimeBoss, @IsPartTimeGroupBoss, @ValidMark, @FlowOrganID,")
            strSQL.AppendLine("    @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In EmployeeWaitDRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(r.ValidDate.Value), Convert.ToDateTime("1900/1/1"), r.ValidDate.Value))
                        db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                        db.AddInParameter(dbcmd, "@SubSeq", DbType.Int32, r.SubSeq.Value)
                        db.AddInParameter(dbcmd, "@NewCompID", DbType.String, r.NewCompID.Value)
                        db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                        db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                        db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                        db.AddInParameter(dbcmd, "@GroupOrganID", DbType.String, r.GroupOrganID.Value)
                        db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                        db.AddInParameter(dbcmd, "@TitleID", DbType.String, r.TitleID.Value)
                        db.AddInParameter(dbcmd, "@TitleName", DbType.String, r.TitleName.Value)
                        db.AddInParameter(dbcmd, "@IsPartTimeBoss", DbType.String, r.IsPartTimeBoss.Value)
                        db.AddInParameter(dbcmd, "@IsPartTimeGroupBoss", DbType.String, r.IsPartTimeGroupBoss.Value)
                        db.AddInParameter(dbcmd, "@ValidMark", DbType.String, r.ValidMark.Value)
                        db.AddInParameter(dbcmd, "@FlowOrganID", DbType.String, r.FlowOrganID.Value)
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

        Public Function Insert(ByVal EmployeeWaitDRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into EmployeeWaitD")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, ValidDate, Seq, SubSeq, NewCompID, DeptID, OrganID, GroupID, GroupOrganID,")
            strSQL.AppendLine("    RankID, TitleID, TitleName, IsPartTimeBoss, IsPartTimeGroupBoss, ValidMark, FlowOrganID,")
            strSQL.AppendLine("    LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @ValidDate, @Seq, @SubSeq, @NewCompID, @DeptID, @OrganID, @GroupID, @GroupOrganID,")
            strSQL.AppendLine("    @RankID, @TitleID, @TitleName, @IsPartTimeBoss, @IsPartTimeGroupBoss, @ValidMark, @FlowOrganID,")
            strSQL.AppendLine("    @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In EmployeeWaitDRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(r.ValidDate.Value), Convert.ToDateTime("1900/1/1"), r.ValidDate.Value))
                db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                db.AddInParameter(dbcmd, "@SubSeq", DbType.Int32, r.SubSeq.Value)
                db.AddInParameter(dbcmd, "@NewCompID", DbType.String, r.NewCompID.Value)
                db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                db.AddInParameter(dbcmd, "@GroupOrganID", DbType.String, r.GroupOrganID.Value)
                db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                db.AddInParameter(dbcmd, "@TitleID", DbType.String, r.TitleID.Value)
                db.AddInParameter(dbcmd, "@TitleName", DbType.String, r.TitleName.Value)
                db.AddInParameter(dbcmd, "@IsPartTimeBoss", DbType.String, r.IsPartTimeBoss.Value)
                db.AddInParameter(dbcmd, "@IsPartTimeGroupBoss", DbType.String, r.IsPartTimeGroupBoss.Value)
                db.AddInParameter(dbcmd, "@ValidMark", DbType.String, r.ValidMark.Value)
                db.AddInParameter(dbcmd, "@FlowOrganID", DbType.String, r.FlowOrganID.Value)
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

