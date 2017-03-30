'****************************************************************
' Table:EmpAdditionWait
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

Namespace beEmpAdditionWait
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "EmpID", "ValidDate", "Seq", "AddSeq", "AddCompID", "AddDeptID", "AddOrganID", "AddFlowOrganID", "Reason" _
                                    , "FileNo", "Remark", "IsBoss", "IsSecBoss", "IsGroupBoss", "IsSecGroupBoss", "BossType", "ValidMark", "ExistsEmpAddition", "CreateDate", "CreateComp" _
                                    , "CreateID", "LastChgDate", "LastChgComp", "LastChgID" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(Date), GetType(Integer), GetType(Integer), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(String) _
                                    , GetType(String), GetType(Date), GetType(String), GetType(String) }
        Private m_PrimaryFields As String() = { "CompID", "EmpID", "ValidDate", "Seq", "AddSeq" }

        Public ReadOnly Property Rows() As beEmpAdditionWait.Rows 
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
        Public Sub Transfer2Row(EmpAdditionWaitTable As DataTable)
            For Each dr As DataRow In EmpAdditionWaitTable.Rows
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
                dr(m_Rows(i).AddSeq.FieldName) = m_Rows(i).AddSeq.Value
                dr(m_Rows(i).AddCompID.FieldName) = m_Rows(i).AddCompID.Value
                dr(m_Rows(i).AddDeptID.FieldName) = m_Rows(i).AddDeptID.Value
                dr(m_Rows(i).AddOrganID.FieldName) = m_Rows(i).AddOrganID.Value
                dr(m_Rows(i).AddFlowOrganID.FieldName) = m_Rows(i).AddFlowOrganID.Value
                dr(m_Rows(i).Reason.FieldName) = m_Rows(i).Reason.Value
                dr(m_Rows(i).FileNo.FieldName) = m_Rows(i).FileNo.Value
                dr(m_Rows(i).Remark.FieldName) = m_Rows(i).Remark.Value
                dr(m_Rows(i).IsBoss.FieldName) = m_Rows(i).IsBoss.Value
                dr(m_Rows(i).IsSecBoss.FieldName) = m_Rows(i).IsSecBoss.Value
                dr(m_Rows(i).IsGroupBoss.FieldName) = m_Rows(i).IsGroupBoss.Value
                dr(m_Rows(i).IsSecGroupBoss.FieldName) = m_Rows(i).IsSecGroupBoss.Value
                dr(m_Rows(i).BossType.FieldName) = m_Rows(i).BossType.Value
                dr(m_Rows(i).ValidMark.FieldName) = m_Rows(i).ValidMark.Value
                dr(m_Rows(i).ExistsEmpAddition.FieldName) = m_Rows(i).ExistsEmpAddition.Value
                dr(m_Rows(i).CreateDate.FieldName) = m_Rows(i).CreateDate.Value
                dr(m_Rows(i).CreateComp.FieldName) = m_Rows(i).CreateComp.Value
                dr(m_Rows(i).CreateID.FieldName) = m_Rows(i).CreateID.Value
                dr(m_Rows(i).LastChgDate.FieldName) = m_Rows(i).LastChgDate.Value
                dr(m_Rows(i).LastChgComp.FieldName) = m_Rows(i).LastChgComp.Value
                dr(m_Rows(i).LastChgID.FieldName) = m_Rows(i).LastChgID.Value

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

        Public Sub Add(EmpAdditionWaitRow As Row)
            m_Rows.Add(EmpAdditionWaitRow)
        End Sub

        Public Sub Remove(EmpAdditionWaitRow As Row)
            If m_Rows.IndexOf(EmpAdditionWaitRow) >= 0 Then
                m_Rows.Remove(EmpAdditionWaitRow)
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
        Private FI_AddSeq As Field(Of Integer) = new Field(Of Integer)("AddSeq", true)
        Private FI_AddCompID As Field(Of String) = new Field(Of String)("AddCompID", true)
        Private FI_AddDeptID As Field(Of String) = new Field(Of String)("AddDeptID", true)
        Private FI_AddOrganID As Field(Of String) = new Field(Of String)("AddOrganID", true)
        Private FI_AddFlowOrganID As Field(Of String) = new Field(Of String)("AddFlowOrganID", true)
        Private FI_Reason As Field(Of String) = new Field(Of String)("Reason", true)
        Private FI_FileNo As Field(Of String) = new Field(Of String)("FileNo", true)
        Private FI_Remark As Field(Of String) = new Field(Of String)("Remark", true)
        Private FI_IsBoss As Field(Of String) = new Field(Of String)("IsBoss", true)
        Private FI_IsSecBoss As Field(Of String) = new Field(Of String)("IsSecBoss", true)
        Private FI_IsGroupBoss As Field(Of String) = new Field(Of String)("IsGroupBoss", true)
        Private FI_IsSecGroupBoss As Field(Of String) = new Field(Of String)("IsSecGroupBoss", true)
        Private FI_BossType As Field(Of String) = new Field(Of String)("BossType", true)
        Private FI_ValidMark As Field(Of String) = new Field(Of String)("ValidMark", true)
        Private FI_ExistsEmpAddition As Field(Of String) = new Field(Of String)("ExistsEmpAddition", true)
        Private FI_CreateDate As Field(Of Date) = new Field(Of Date)("CreateDate", true)
        Private FI_CreateComp As Field(Of String) = new Field(Of String)("CreateComp", true)
        Private FI_CreateID As Field(Of String) = new Field(Of String)("CreateID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private m_FieldNames As String() = { "CompID", "EmpID", "ValidDate", "Seq", "AddSeq", "AddCompID", "AddDeptID", "AddOrganID", "AddFlowOrganID", "Reason" _
                                    , "FileNo", "Remark", "IsBoss", "IsSecBoss", "IsGroupBoss", "IsSecGroupBoss", "BossType", "ValidMark", "ExistsEmpAddition", "CreateDate", "CreateComp" _
                                    , "CreateID", "LastChgDate", "LastChgComp", "LastChgID" }
        Private m_PrimaryFields As String() = { "CompID", "EmpID", "ValidDate", "Seq", "AddSeq" }
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
                Case "AddSeq"
                    Return FI_AddSeq.Value
                Case "AddCompID"
                    Return FI_AddCompID.Value
                Case "AddDeptID"
                    Return FI_AddDeptID.Value
                Case "AddOrganID"
                    Return FI_AddOrganID.Value
                Case "AddFlowOrganID"
                    Return FI_AddFlowOrganID.Value
                Case "Reason"
                    Return FI_Reason.Value
                Case "FileNo"
                    Return FI_FileNo.Value
                Case "Remark"
                    Return FI_Remark.Value
                Case "IsBoss"
                    Return FI_IsBoss.Value
                Case "IsSecBoss"
                    Return FI_IsSecBoss.Value
                Case "IsGroupBoss"
                    Return FI_IsGroupBoss.Value
                Case "IsSecGroupBoss"
                    Return FI_IsSecGroupBoss.Value
                Case "BossType"
                    Return FI_BossType.Value
                Case "ValidMark"
                    Return FI_ValidMark.Value
                Case "ExistsEmpAddition"
                    Return FI_ExistsEmpAddition.Value
                Case "CreateDate"
                    Return FI_CreateDate.Value
                Case "CreateComp"
                    Return FI_CreateComp.Value
                Case "CreateID"
                    Return FI_CreateID.Value
                Case "LastChgDate"
                    Return FI_LastChgDate.Value
                Case "LastChgComp"
                    Return FI_LastChgComp.Value
                Case "LastChgID"
                    Return FI_LastChgID.Value
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
                Case "AddSeq"
                    FI_AddSeq.SetValue(value)
                Case "AddCompID"
                    FI_AddCompID.SetValue(value)
                Case "AddDeptID"
                    FI_AddDeptID.SetValue(value)
                Case "AddOrganID"
                    FI_AddOrganID.SetValue(value)
                Case "AddFlowOrganID"
                    FI_AddFlowOrganID.SetValue(value)
                Case "Reason"
                    FI_Reason.SetValue(value)
                Case "FileNo"
                    FI_FileNo.SetValue(value)
                Case "Remark"
                    FI_Remark.SetValue(value)
                Case "IsBoss"
                    FI_IsBoss.SetValue(value)
                Case "IsSecBoss"
                    FI_IsSecBoss.SetValue(value)
                Case "IsGroupBoss"
                    FI_IsGroupBoss.SetValue(value)
                Case "IsSecGroupBoss"
                    FI_IsSecGroupBoss.SetValue(value)
                Case "BossType"
                    FI_BossType.SetValue(value)
                Case "ValidMark"
                    FI_ValidMark.SetValue(value)
                Case "ExistsEmpAddition"
                    FI_ExistsEmpAddition.SetValue(value)
                Case "CreateDate"
                    FI_CreateDate.SetValue(value)
                Case "CreateComp"
                    FI_CreateComp.SetValue(value)
                Case "CreateID"
                    FI_CreateID.SetValue(value)
                Case "LastChgDate"
                    FI_LastChgDate.SetValue(value)
                Case "LastChgComp"
                    FI_LastChgComp.SetValue(value)
                Case "LastChgID"
                    FI_LastChgID.SetValue(value)
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
                Case "AddSeq"
                    return FI_AddSeq.Updated
                Case "AddCompID"
                    return FI_AddCompID.Updated
                Case "AddDeptID"
                    return FI_AddDeptID.Updated
                Case "AddOrganID"
                    return FI_AddOrganID.Updated
                Case "AddFlowOrganID"
                    return FI_AddFlowOrganID.Updated
                Case "Reason"
                    return FI_Reason.Updated
                Case "FileNo"
                    return FI_FileNo.Updated
                Case "Remark"
                    return FI_Remark.Updated
                Case "IsBoss"
                    return FI_IsBoss.Updated
                Case "IsSecBoss"
                    return FI_IsSecBoss.Updated
                Case "IsGroupBoss"
                    return FI_IsGroupBoss.Updated
                Case "IsSecGroupBoss"
                    return FI_IsSecGroupBoss.Updated
                Case "BossType"
                    return FI_BossType.Updated
                Case "ValidMark"
                    return FI_ValidMark.Updated
                Case "ExistsEmpAddition"
                    return FI_ExistsEmpAddition.Updated
                Case "CreateDate"
                    return FI_CreateDate.Updated
                Case "CreateComp"
                    return FI_CreateComp.Updated
                Case "CreateID"
                    return FI_CreateID.Updated
                Case "LastChgDate"
                    return FI_LastChgDate.Updated
                Case "LastChgComp"
                    return FI_LastChgComp.Updated
                Case "LastChgID"
                    return FI_LastChgID.Updated
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
                Case "AddSeq"
                    return FI_AddSeq.CreateUpdateSQL
                Case "AddCompID"
                    return FI_AddCompID.CreateUpdateSQL
                Case "AddDeptID"
                    return FI_AddDeptID.CreateUpdateSQL
                Case "AddOrganID"
                    return FI_AddOrganID.CreateUpdateSQL
                Case "AddFlowOrganID"
                    return FI_AddFlowOrganID.CreateUpdateSQL
                Case "Reason"
                    return FI_Reason.CreateUpdateSQL
                Case "FileNo"
                    return FI_FileNo.CreateUpdateSQL
                Case "Remark"
                    return FI_Remark.CreateUpdateSQL
                Case "IsBoss"
                    return FI_IsBoss.CreateUpdateSQL
                Case "IsSecBoss"
                    return FI_IsSecBoss.CreateUpdateSQL
                Case "IsGroupBoss"
                    return FI_IsGroupBoss.CreateUpdateSQL
                Case "IsSecGroupBoss"
                    return FI_IsSecGroupBoss.CreateUpdateSQL
                Case "BossType"
                    return FI_BossType.CreateUpdateSQL
                Case "ValidMark"
                    return FI_ValidMark.CreateUpdateSQL
                Case "ExistsEmpAddition"
                    return FI_ExistsEmpAddition.CreateUpdateSQL
                Case "CreateDate"
                    return FI_CreateDate.CreateUpdateSQL
                Case "CreateComp"
                    return FI_CreateComp.CreateUpdateSQL
                Case "CreateID"
                    return FI_CreateID.CreateUpdateSQL
                Case "LastChgDate"
                    return FI_LastChgDate.CreateUpdateSQL
                Case "LastChgComp"
                    return FI_LastChgComp.CreateUpdateSQL
                Case "LastChgID"
                    return FI_LastChgID.CreateUpdateSQL
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
            FI_AddSeq.SetInitValue(0)
            FI_AddCompID.SetInitValue("")
            FI_AddDeptID.SetInitValue("")
            FI_AddOrganID.SetInitValue("")
            FI_AddFlowOrganID.SetInitValue("")
            FI_Reason.SetInitValue("")
            FI_FileNo.SetInitValue("")
            FI_Remark.SetInitValue("")
            FI_IsBoss.SetInitValue("0")
            FI_IsSecBoss.SetInitValue("0")
            FI_IsGroupBoss.SetInitValue("0")
            FI_IsSecGroupBoss.SetInitValue("0")
            FI_BossType.SetInitValue("")
            FI_ValidMark.SetInitValue("0")
            FI_ExistsEmpAddition.SetInitValue("0")
            FI_CreateDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_CreateComp.SetInitValue("")
            FI_CreateID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CompID.SetInitValue(dr("CompID"))
            FI_EmpID.SetInitValue(dr("EmpID"))
            FI_ValidDate.SetInitValue(dr("ValidDate"))
            FI_Seq.SetInitValue(dr("Seq"))
            FI_AddSeq.SetInitValue(dr("AddSeq"))
            FI_AddCompID.SetInitValue(dr("AddCompID"))
            FI_AddDeptID.SetInitValue(dr("AddDeptID"))
            FI_AddOrganID.SetInitValue(dr("AddOrganID"))
            FI_AddFlowOrganID.SetInitValue(dr("AddFlowOrganID"))
            FI_Reason.SetInitValue(dr("Reason"))
            FI_FileNo.SetInitValue(dr("FileNo"))
            FI_Remark.SetInitValue(dr("Remark"))
            FI_IsBoss.SetInitValue(dr("IsBoss"))
            FI_IsSecBoss.SetInitValue(dr("IsSecBoss"))
            FI_IsGroupBoss.SetInitValue(dr("IsGroupBoss"))
            FI_IsSecGroupBoss.SetInitValue(dr("IsSecGroupBoss"))
            FI_BossType.SetInitValue(dr("BossType"))
            FI_ValidMark.SetInitValue(dr("ValidMark"))
            FI_ExistsEmpAddition.SetInitValue(dr("ExistsEmpAddition"))
            FI_CreateDate.SetInitValue(dr("CreateDate"))
            FI_CreateComp.SetInitValue(dr("CreateComp"))
            FI_CreateID.SetInitValue(dr("CreateID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CompID.Updated = False
            FI_EmpID.Updated = False
            FI_ValidDate.Updated = False
            FI_Seq.Updated = False
            FI_AddSeq.Updated = False
            FI_AddCompID.Updated = False
            FI_AddDeptID.Updated = False
            FI_AddOrganID.Updated = False
            FI_AddFlowOrganID.Updated = False
            FI_Reason.Updated = False
            FI_FileNo.Updated = False
            FI_Remark.Updated = False
            FI_IsBoss.Updated = False
            FI_IsSecBoss.Updated = False
            FI_IsGroupBoss.Updated = False
            FI_IsSecGroupBoss.Updated = False
            FI_BossType.Updated = False
            FI_ValidMark.Updated = False
            FI_ExistsEmpAddition.Updated = False
            FI_CreateDate.Updated = False
            FI_CreateComp.Updated = False
            FI_CreateID.Updated = False
            FI_LastChgDate.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
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

        Public ReadOnly Property AddSeq As Field(Of Integer) 
            Get
                Return FI_AddSeq
            End Get
        End Property

        Public ReadOnly Property AddCompID As Field(Of String) 
            Get
                Return FI_AddCompID
            End Get
        End Property

        Public ReadOnly Property AddDeptID As Field(Of String) 
            Get
                Return FI_AddDeptID
            End Get
        End Property

        Public ReadOnly Property AddOrganID As Field(Of String) 
            Get
                Return FI_AddOrganID
            End Get
        End Property

        Public ReadOnly Property AddFlowOrganID As Field(Of String) 
            Get
                Return FI_AddFlowOrganID
            End Get
        End Property

        Public ReadOnly Property Reason As Field(Of String) 
            Get
                Return FI_Reason
            End Get
        End Property

        Public ReadOnly Property FileNo As Field(Of String) 
            Get
                Return FI_FileNo
            End Get
        End Property

        Public ReadOnly Property Remark As Field(Of String) 
            Get
                Return FI_Remark
            End Get
        End Property

        Public ReadOnly Property IsBoss As Field(Of String) 
            Get
                Return FI_IsBoss
            End Get
        End Property

        Public ReadOnly Property IsSecBoss As Field(Of String) 
            Get
                Return FI_IsSecBoss
            End Get
        End Property

        Public ReadOnly Property IsGroupBoss As Field(Of String) 
            Get
                Return FI_IsGroupBoss
            End Get
        End Property

        Public ReadOnly Property IsSecGroupBoss As Field(Of String) 
            Get
                Return FI_IsSecGroupBoss
            End Get
        End Property

        Public ReadOnly Property BossType As Field(Of String) 
            Get
                Return FI_BossType
            End Get
        End Property

        Public ReadOnly Property ValidMark As Field(Of String) 
            Get
                Return FI_ValidMark
            End Get
        End Property

        Public ReadOnly Property ExistsEmpAddition As Field(Of String) 
            Get
                Return FI_ExistsEmpAddition
            End Get
        End Property

        Public ReadOnly Property CreateDate As Field(Of Date) 
            Get
                Return FI_CreateDate
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

        Public ReadOnly Property LastChgDate As Field(Of Date) 
            Get
                Return FI_LastChgDate
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

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal EmpAdditionWaitRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From EmpAdditionWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And ValidDate = @ValidDate")
            strSQL.AppendLine("And Seq = @Seq")
            strSQL.AppendLine("And AddSeq = @AddSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpAdditionWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpAdditionWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, EmpAdditionWaitRow.ValidDate.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EmpAdditionWaitRow.Seq.Value)
            db.AddInParameter(dbcmd, "@AddSeq", DbType.Int32, EmpAdditionWaitRow.AddSeq.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal EmpAdditionWaitRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From EmpAdditionWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And ValidDate = @ValidDate")
            strSQL.AppendLine("And Seq = @Seq")
            strSQL.AppendLine("And AddSeq = @AddSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpAdditionWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpAdditionWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, EmpAdditionWaitRow.ValidDate.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EmpAdditionWaitRow.Seq.Value)
            db.AddInParameter(dbcmd, "@AddSeq", DbType.Int32, EmpAdditionWaitRow.AddSeq.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal EmpAdditionWaitRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From EmpAdditionWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And ValidDate = @ValidDate")
            strSQL.AppendLine("And Seq = @Seq")
            strSQL.AppendLine("And AddSeq = @AddSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In EmpAdditionWaitRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, r.ValidDate.Value)
                        db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                        db.AddInParameter(dbcmd, "@AddSeq", DbType.Int32, r.AddSeq.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal EmpAdditionWaitRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From EmpAdditionWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And ValidDate = @ValidDate")
            strSQL.AppendLine("And Seq = @Seq")
            strSQL.AppendLine("And AddSeq = @AddSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In EmpAdditionWaitRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, r.ValidDate.Value)
                db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                db.AddInParameter(dbcmd, "@AddSeq", DbType.Int32, r.AddSeq.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal EmpAdditionWaitRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmpAdditionWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And ValidDate = @ValidDate")
            strSQL.AppendLine("And Seq = @Seq")
            strSQL.AppendLine("And AddSeq = @AddSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpAdditionWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpAdditionWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, EmpAdditionWaitRow.ValidDate.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EmpAdditionWaitRow.Seq.Value)
            db.AddInParameter(dbcmd, "@AddSeq", DbType.Int32, EmpAdditionWaitRow.AddSeq.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(EmpAdditionWaitRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmpAdditionWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And ValidDate = @ValidDate")
            strSQL.AppendLine("And Seq = @Seq")
            strSQL.AppendLine("And AddSeq = @AddSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpAdditionWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpAdditionWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, EmpAdditionWaitRow.ValidDate.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EmpAdditionWaitRow.Seq.Value)
            db.AddInParameter(dbcmd, "@AddSeq", DbType.Int32, EmpAdditionWaitRow.AddSeq.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal EmpAdditionWaitRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update EmpAdditionWait Set")
            For i As Integer = 0 To EmpAdditionWaitRow.FieldNames.Length - 1
                If Not EmpAdditionWaitRow.IsIdentityField(EmpAdditionWaitRow.FieldNames(i)) AndAlso EmpAdditionWaitRow.IsUpdated(EmpAdditionWaitRow.FieldNames(i)) AndAlso EmpAdditionWaitRow.CreateUpdateSQL(EmpAdditionWaitRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, EmpAdditionWaitRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And EmpID = @PKEmpID")
            strSQL.AppendLine("And ValidDate = @PKValidDate")
            strSQL.AppendLine("And Seq = @PKSeq")
            strSQL.AppendLine("And AddSeq = @PKAddSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If EmpAdditionWaitRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpAdditionWaitRow.CompID.Value)
            If EmpAdditionWaitRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpAdditionWaitRow.EmpID.Value)
            If EmpAdditionWaitRow.ValidDate.Updated Then db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(EmpAdditionWaitRow.ValidDate.Value), Convert.ToDateTime("1900/1/1"), EmpAdditionWaitRow.ValidDate.Value))
            If EmpAdditionWaitRow.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EmpAdditionWaitRow.Seq.Value)
            If EmpAdditionWaitRow.AddSeq.Updated Then db.AddInParameter(dbcmd, "@AddSeq", DbType.Int32, EmpAdditionWaitRow.AddSeq.Value)
            If EmpAdditionWaitRow.AddCompID.Updated Then db.AddInParameter(dbcmd, "@AddCompID", DbType.String, EmpAdditionWaitRow.AddCompID.Value)
            If EmpAdditionWaitRow.AddDeptID.Updated Then db.AddInParameter(dbcmd, "@AddDeptID", DbType.String, EmpAdditionWaitRow.AddDeptID.Value)
            If EmpAdditionWaitRow.AddOrganID.Updated Then db.AddInParameter(dbcmd, "@AddOrganID", DbType.String, EmpAdditionWaitRow.AddOrganID.Value)
            If EmpAdditionWaitRow.AddFlowOrganID.Updated Then db.AddInParameter(dbcmd, "@AddFlowOrganID", DbType.String, EmpAdditionWaitRow.AddFlowOrganID.Value)
            If EmpAdditionWaitRow.Reason.Updated Then db.AddInParameter(dbcmd, "@Reason", DbType.String, EmpAdditionWaitRow.Reason.Value)
            If EmpAdditionWaitRow.FileNo.Updated Then db.AddInParameter(dbcmd, "@FileNo", DbType.String, EmpAdditionWaitRow.FileNo.Value)
            If EmpAdditionWaitRow.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, EmpAdditionWaitRow.Remark.Value)
            If EmpAdditionWaitRow.IsBoss.Updated Then db.AddInParameter(dbcmd, "@IsBoss", DbType.String, EmpAdditionWaitRow.IsBoss.Value)
            If EmpAdditionWaitRow.IsSecBoss.Updated Then db.AddInParameter(dbcmd, "@IsSecBoss", DbType.String, EmpAdditionWaitRow.IsSecBoss.Value)
            If EmpAdditionWaitRow.IsGroupBoss.Updated Then db.AddInParameter(dbcmd, "@IsGroupBoss", DbType.String, EmpAdditionWaitRow.IsGroupBoss.Value)
            If EmpAdditionWaitRow.IsSecGroupBoss.Updated Then db.AddInParameter(dbcmd, "@IsSecGroupBoss", DbType.String, EmpAdditionWaitRow.IsSecGroupBoss.Value)
            If EmpAdditionWaitRow.BossType.Updated Then db.AddInParameter(dbcmd, "@BossType", DbType.String, EmpAdditionWaitRow.BossType.Value)
            If EmpAdditionWaitRow.ValidMark.Updated Then db.AddInParameter(dbcmd, "@ValidMark", DbType.String, EmpAdditionWaitRow.ValidMark.Value)
            If EmpAdditionWaitRow.ExistsEmpAddition.Updated Then db.AddInParameter(dbcmd, "@ExistsEmpAddition", DbType.String, EmpAdditionWaitRow.ExistsEmpAddition.Value)
            If EmpAdditionWaitRow.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(EmpAdditionWaitRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), EmpAdditionWaitRow.CreateDate.Value))
            If EmpAdditionWaitRow.CreateComp.Updated Then db.AddInParameter(dbcmd, "@CreateComp", DbType.String, EmpAdditionWaitRow.CreateComp.Value)
            If EmpAdditionWaitRow.CreateID.Updated Then db.AddInParameter(dbcmd, "@CreateID", DbType.String, EmpAdditionWaitRow.CreateID.Value)
            If EmpAdditionWaitRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpAdditionWaitRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpAdditionWaitRow.LastChgDate.Value))
            If EmpAdditionWaitRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpAdditionWaitRow.LastChgComp.Value)
            If EmpAdditionWaitRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpAdditionWaitRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(EmpAdditionWaitRow.LoadFromDataRow, EmpAdditionWaitRow.CompID.OldValue, EmpAdditionWaitRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(EmpAdditionWaitRow.LoadFromDataRow, EmpAdditionWaitRow.EmpID.OldValue, EmpAdditionWaitRow.EmpID.Value))
            db.AddInParameter(dbcmd, "@PKValidDate", DbType.Date, IIf(EmpAdditionWaitRow.LoadFromDataRow, EmpAdditionWaitRow.ValidDate.OldValue, EmpAdditionWaitRow.ValidDate.Value))
            db.AddInParameter(dbcmd, "@PKSeq", DbType.Int32, IIf(EmpAdditionWaitRow.LoadFromDataRow, EmpAdditionWaitRow.Seq.OldValue, EmpAdditionWaitRow.Seq.Value))
            db.AddInParameter(dbcmd, "@PKAddSeq", DbType.Int32, IIf(EmpAdditionWaitRow.LoadFromDataRow, EmpAdditionWaitRow.AddSeq.OldValue, EmpAdditionWaitRow.AddSeq.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal EmpAdditionWaitRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update EmpAdditionWait Set")
            For i As Integer = 0 To EmpAdditionWaitRow.FieldNames.Length - 1
                If Not EmpAdditionWaitRow.IsIdentityField(EmpAdditionWaitRow.FieldNames(i)) AndAlso EmpAdditionWaitRow.IsUpdated(EmpAdditionWaitRow.FieldNames(i)) AndAlso EmpAdditionWaitRow.CreateUpdateSQL(EmpAdditionWaitRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, EmpAdditionWaitRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And EmpID = @PKEmpID")
            strSQL.AppendLine("And ValidDate = @PKValidDate")
            strSQL.AppendLine("And Seq = @PKSeq")
            strSQL.AppendLine("And AddSeq = @PKAddSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If EmpAdditionWaitRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpAdditionWaitRow.CompID.Value)
            If EmpAdditionWaitRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpAdditionWaitRow.EmpID.Value)
            If EmpAdditionWaitRow.ValidDate.Updated Then db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(EmpAdditionWaitRow.ValidDate.Value), Convert.ToDateTime("1900/1/1"), EmpAdditionWaitRow.ValidDate.Value))
            If EmpAdditionWaitRow.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EmpAdditionWaitRow.Seq.Value)
            If EmpAdditionWaitRow.AddSeq.Updated Then db.AddInParameter(dbcmd, "@AddSeq", DbType.Int32, EmpAdditionWaitRow.AddSeq.Value)
            If EmpAdditionWaitRow.AddCompID.Updated Then db.AddInParameter(dbcmd, "@AddCompID", DbType.String, EmpAdditionWaitRow.AddCompID.Value)
            If EmpAdditionWaitRow.AddDeptID.Updated Then db.AddInParameter(dbcmd, "@AddDeptID", DbType.String, EmpAdditionWaitRow.AddDeptID.Value)
            If EmpAdditionWaitRow.AddOrganID.Updated Then db.AddInParameter(dbcmd, "@AddOrganID", DbType.String, EmpAdditionWaitRow.AddOrganID.Value)
            If EmpAdditionWaitRow.AddFlowOrganID.Updated Then db.AddInParameter(dbcmd, "@AddFlowOrganID", DbType.String, EmpAdditionWaitRow.AddFlowOrganID.Value)
            If EmpAdditionWaitRow.Reason.Updated Then db.AddInParameter(dbcmd, "@Reason", DbType.String, EmpAdditionWaitRow.Reason.Value)
            If EmpAdditionWaitRow.FileNo.Updated Then db.AddInParameter(dbcmd, "@FileNo", DbType.String, EmpAdditionWaitRow.FileNo.Value)
            If EmpAdditionWaitRow.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, EmpAdditionWaitRow.Remark.Value)
            If EmpAdditionWaitRow.IsBoss.Updated Then db.AddInParameter(dbcmd, "@IsBoss", DbType.String, EmpAdditionWaitRow.IsBoss.Value)
            If EmpAdditionWaitRow.IsSecBoss.Updated Then db.AddInParameter(dbcmd, "@IsSecBoss", DbType.String, EmpAdditionWaitRow.IsSecBoss.Value)
            If EmpAdditionWaitRow.IsGroupBoss.Updated Then db.AddInParameter(dbcmd, "@IsGroupBoss", DbType.String, EmpAdditionWaitRow.IsGroupBoss.Value)
            If EmpAdditionWaitRow.IsSecGroupBoss.Updated Then db.AddInParameter(dbcmd, "@IsSecGroupBoss", DbType.String, EmpAdditionWaitRow.IsSecGroupBoss.Value)
            If EmpAdditionWaitRow.BossType.Updated Then db.AddInParameter(dbcmd, "@BossType", DbType.String, EmpAdditionWaitRow.BossType.Value)
            If EmpAdditionWaitRow.ValidMark.Updated Then db.AddInParameter(dbcmd, "@ValidMark", DbType.String, EmpAdditionWaitRow.ValidMark.Value)
            If EmpAdditionWaitRow.ExistsEmpAddition.Updated Then db.AddInParameter(dbcmd, "@ExistsEmpAddition", DbType.String, EmpAdditionWaitRow.ExistsEmpAddition.Value)
            If EmpAdditionWaitRow.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(EmpAdditionWaitRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), EmpAdditionWaitRow.CreateDate.Value))
            If EmpAdditionWaitRow.CreateComp.Updated Then db.AddInParameter(dbcmd, "@CreateComp", DbType.String, EmpAdditionWaitRow.CreateComp.Value)
            If EmpAdditionWaitRow.CreateID.Updated Then db.AddInParameter(dbcmd, "@CreateID", DbType.String, EmpAdditionWaitRow.CreateID.Value)
            If EmpAdditionWaitRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpAdditionWaitRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpAdditionWaitRow.LastChgDate.Value))
            If EmpAdditionWaitRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpAdditionWaitRow.LastChgComp.Value)
            If EmpAdditionWaitRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpAdditionWaitRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(EmpAdditionWaitRow.LoadFromDataRow, EmpAdditionWaitRow.CompID.OldValue, EmpAdditionWaitRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(EmpAdditionWaitRow.LoadFromDataRow, EmpAdditionWaitRow.EmpID.OldValue, EmpAdditionWaitRow.EmpID.Value))
            db.AddInParameter(dbcmd, "@PKValidDate", DbType.Date, IIf(EmpAdditionWaitRow.LoadFromDataRow, EmpAdditionWaitRow.ValidDate.OldValue, EmpAdditionWaitRow.ValidDate.Value))
            db.AddInParameter(dbcmd, "@PKSeq", DbType.Int32, IIf(EmpAdditionWaitRow.LoadFromDataRow, EmpAdditionWaitRow.Seq.OldValue, EmpAdditionWaitRow.Seq.Value))
            db.AddInParameter(dbcmd, "@PKAddSeq", DbType.Int32, IIf(EmpAdditionWaitRow.LoadFromDataRow, EmpAdditionWaitRow.AddSeq.OldValue, EmpAdditionWaitRow.AddSeq.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal EmpAdditionWaitRow As Row()) As Integer
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
                    For Each r As Row In EmpAdditionWaitRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update EmpAdditionWait Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where CompID = @PKCompID")
                        strSQL.AppendLine("And EmpID = @PKEmpID")
                        strSQL.AppendLine("And ValidDate = @PKValidDate")
                        strSQL.AppendLine("And Seq = @PKSeq")
                        strSQL.AppendLine("And AddSeq = @PKAddSeq")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        If r.ValidDate.Updated Then db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(r.ValidDate.Value), Convert.ToDateTime("1900/1/1"), r.ValidDate.Value))
                        If r.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                        If r.AddSeq.Updated Then db.AddInParameter(dbcmd, "@AddSeq", DbType.Int32, r.AddSeq.Value)
                        If r.AddCompID.Updated Then db.AddInParameter(dbcmd, "@AddCompID", DbType.String, r.AddCompID.Value)
                        If r.AddDeptID.Updated Then db.AddInParameter(dbcmd, "@AddDeptID", DbType.String, r.AddDeptID.Value)
                        If r.AddOrganID.Updated Then db.AddInParameter(dbcmd, "@AddOrganID", DbType.String, r.AddOrganID.Value)
                        If r.AddFlowOrganID.Updated Then db.AddInParameter(dbcmd, "@AddFlowOrganID", DbType.String, r.AddFlowOrganID.Value)
                        If r.Reason.Updated Then db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                        If r.FileNo.Updated Then db.AddInParameter(dbcmd, "@FileNo", DbType.String, r.FileNo.Value)
                        If r.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                        If r.IsBoss.Updated Then db.AddInParameter(dbcmd, "@IsBoss", DbType.String, r.IsBoss.Value)
                        If r.IsSecBoss.Updated Then db.AddInParameter(dbcmd, "@IsSecBoss", DbType.String, r.IsSecBoss.Value)
                        If r.IsGroupBoss.Updated Then db.AddInParameter(dbcmd, "@IsGroupBoss", DbType.String, r.IsGroupBoss.Value)
                        If r.IsSecGroupBoss.Updated Then db.AddInParameter(dbcmd, "@IsSecGroupBoss", DbType.String, r.IsSecGroupBoss.Value)
                        If r.BossType.Updated Then db.AddInParameter(dbcmd, "@BossType", DbType.String, r.BossType.Value)
                        If r.ValidMark.Updated Then db.AddInParameter(dbcmd, "@ValidMark", DbType.String, r.ValidMark.Value)
                        If r.ExistsEmpAddition.Updated Then db.AddInParameter(dbcmd, "@ExistsEmpAddition", DbType.String, r.ExistsEmpAddition.Value)
                        If r.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                        If r.CreateComp.Updated Then db.AddInParameter(dbcmd, "@CreateComp", DbType.String, r.CreateComp.Value)
                        If r.CreateID.Updated Then db.AddInParameter(dbcmd, "@CreateID", DbType.String, r.CreateID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                        db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))
                        db.AddInParameter(dbcmd, "@PKValidDate", DbType.Date, IIf(r.LoadFromDataRow, r.ValidDate.OldValue, r.ValidDate.Value))
                        db.AddInParameter(dbcmd, "@PKSeq", DbType.Int32, IIf(r.LoadFromDataRow, r.Seq.OldValue, r.Seq.Value))
                        db.AddInParameter(dbcmd, "@PKAddSeq", DbType.Int32, IIf(r.LoadFromDataRow, r.AddSeq.OldValue, r.AddSeq.Value))

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

        Public Function Update(ByVal EmpAdditionWaitRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In EmpAdditionWaitRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update EmpAdditionWait Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where CompID = @PKCompID")
                strSQL.AppendLine("And EmpID = @PKEmpID")
                strSQL.AppendLine("And ValidDate = @PKValidDate")
                strSQL.AppendLine("And Seq = @PKSeq")
                strSQL.AppendLine("And AddSeq = @PKAddSeq")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                If r.ValidDate.Updated Then db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(r.ValidDate.Value), Convert.ToDateTime("1900/1/1"), r.ValidDate.Value))
                If r.Seq.Updated Then db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                If r.AddSeq.Updated Then db.AddInParameter(dbcmd, "@AddSeq", DbType.Int32, r.AddSeq.Value)
                If r.AddCompID.Updated Then db.AddInParameter(dbcmd, "@AddCompID", DbType.String, r.AddCompID.Value)
                If r.AddDeptID.Updated Then db.AddInParameter(dbcmd, "@AddDeptID", DbType.String, r.AddDeptID.Value)
                If r.AddOrganID.Updated Then db.AddInParameter(dbcmd, "@AddOrganID", DbType.String, r.AddOrganID.Value)
                If r.AddFlowOrganID.Updated Then db.AddInParameter(dbcmd, "@AddFlowOrganID", DbType.String, r.AddFlowOrganID.Value)
                If r.Reason.Updated Then db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                If r.FileNo.Updated Then db.AddInParameter(dbcmd, "@FileNo", DbType.String, r.FileNo.Value)
                If r.Remark.Updated Then db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                If r.IsBoss.Updated Then db.AddInParameter(dbcmd, "@IsBoss", DbType.String, r.IsBoss.Value)
                If r.IsSecBoss.Updated Then db.AddInParameter(dbcmd, "@IsSecBoss", DbType.String, r.IsSecBoss.Value)
                If r.IsGroupBoss.Updated Then db.AddInParameter(dbcmd, "@IsGroupBoss", DbType.String, r.IsGroupBoss.Value)
                If r.IsSecGroupBoss.Updated Then db.AddInParameter(dbcmd, "@IsSecGroupBoss", DbType.String, r.IsSecGroupBoss.Value)
                If r.BossType.Updated Then db.AddInParameter(dbcmd, "@BossType", DbType.String, r.BossType.Value)
                If r.ValidMark.Updated Then db.AddInParameter(dbcmd, "@ValidMark", DbType.String, r.ValidMark.Value)
                If r.ExistsEmpAddition.Updated Then db.AddInParameter(dbcmd, "@ExistsEmpAddition", DbType.String, r.ExistsEmpAddition.Value)
                If r.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                If r.CreateComp.Updated Then db.AddInParameter(dbcmd, "@CreateComp", DbType.String, r.CreateComp.Value)
                If r.CreateID.Updated Then db.AddInParameter(dbcmd, "@CreateID", DbType.String, r.CreateID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))
                db.AddInParameter(dbcmd, "@PKValidDate", DbType.Date, IIf(r.LoadFromDataRow, r.ValidDate.OldValue, r.ValidDate.Value))
                db.AddInParameter(dbcmd, "@PKSeq", DbType.Int32, IIf(r.LoadFromDataRow, r.Seq.OldValue, r.Seq.Value))
                db.AddInParameter(dbcmd, "@PKAddSeq", DbType.Int32, IIf(r.LoadFromDataRow, r.AddSeq.OldValue, r.AddSeq.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal EmpAdditionWaitRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From EmpAdditionWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And ValidDate = @ValidDate")
            strSQL.AppendLine("And Seq = @Seq")
            strSQL.AppendLine("And AddSeq = @AddSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpAdditionWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpAdditionWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, EmpAdditionWaitRow.ValidDate.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EmpAdditionWaitRow.Seq.Value)
            db.AddInParameter(dbcmd, "@AddSeq", DbType.Int32, EmpAdditionWaitRow.AddSeq.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal EmpAdditionWaitRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From EmpAdditionWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And ValidDate = @ValidDate")
            strSQL.AppendLine("And Seq = @Seq")
            strSQL.AppendLine("And AddSeq = @AddSeq")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpAdditionWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpAdditionWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, EmpAdditionWaitRow.ValidDate.Value)
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EmpAdditionWaitRow.Seq.Value)
            db.AddInParameter(dbcmd, "@AddSeq", DbType.Int32, EmpAdditionWaitRow.AddSeq.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmpAdditionWait")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal EmpAdditionWaitRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into EmpAdditionWait")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, ValidDate, Seq, AddSeq, AddCompID, AddDeptID, AddOrganID, AddFlowOrganID,")
            strSQL.AppendLine("    Reason, FileNo, Remark, IsBoss, IsSecBoss, IsGroupBoss, IsSecGroupBoss, BossType, ValidMark,")
            strSQL.AppendLine("    ExistsEmpAddition, CreateDate, CreateComp, CreateID, LastChgDate, LastChgComp, LastChgID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @ValidDate, @Seq, @AddSeq, @AddCompID, @AddDeptID, @AddOrganID, @AddFlowOrganID,")
            strSQL.AppendLine("    @Reason, @FileNo, @Remark, @IsBoss, @IsSecBoss, @IsGroupBoss, @IsSecGroupBoss, @BossType, @ValidMark,")
            strSQL.AppendLine("    @ExistsEmpAddition, @CreateDate, @CreateComp, @CreateID, @LastChgDate, @LastChgComp, @LastChgID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpAdditionWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpAdditionWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(EmpAdditionWaitRow.ValidDate.Value), Convert.ToDateTime("1900/1/1"), EmpAdditionWaitRow.ValidDate.Value))
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EmpAdditionWaitRow.Seq.Value)
            db.AddInParameter(dbcmd, "@AddSeq", DbType.Int32, EmpAdditionWaitRow.AddSeq.Value)
            db.AddInParameter(dbcmd, "@AddCompID", DbType.String, EmpAdditionWaitRow.AddCompID.Value)
            db.AddInParameter(dbcmd, "@AddDeptID", DbType.String, EmpAdditionWaitRow.AddDeptID.Value)
            db.AddInParameter(dbcmd, "@AddOrganID", DbType.String, EmpAdditionWaitRow.AddOrganID.Value)
            db.AddInParameter(dbcmd, "@AddFlowOrganID", DbType.String, EmpAdditionWaitRow.AddFlowOrganID.Value)
            db.AddInParameter(dbcmd, "@Reason", DbType.String, EmpAdditionWaitRow.Reason.Value)
            db.AddInParameter(dbcmd, "@FileNo", DbType.String, EmpAdditionWaitRow.FileNo.Value)
            db.AddInParameter(dbcmd, "@Remark", DbType.String, EmpAdditionWaitRow.Remark.Value)
            db.AddInParameter(dbcmd, "@IsBoss", DbType.String, EmpAdditionWaitRow.IsBoss.Value)
            db.AddInParameter(dbcmd, "@IsSecBoss", DbType.String, EmpAdditionWaitRow.IsSecBoss.Value)
            db.AddInParameter(dbcmd, "@IsGroupBoss", DbType.String, EmpAdditionWaitRow.IsGroupBoss.Value)
            db.AddInParameter(dbcmd, "@IsSecGroupBoss", DbType.String, EmpAdditionWaitRow.IsSecGroupBoss.Value)
            db.AddInParameter(dbcmd, "@BossType", DbType.String, EmpAdditionWaitRow.BossType.Value)
            db.AddInParameter(dbcmd, "@ValidMark", DbType.String, EmpAdditionWaitRow.ValidMark.Value)
            db.AddInParameter(dbcmd, "@ExistsEmpAddition", DbType.String, EmpAdditionWaitRow.ExistsEmpAddition.Value)
            db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(EmpAdditionWaitRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), EmpAdditionWaitRow.CreateDate.Value))
            db.AddInParameter(dbcmd, "@CreateComp", DbType.String, EmpAdditionWaitRow.CreateComp.Value)
            db.AddInParameter(dbcmd, "@CreateID", DbType.String, EmpAdditionWaitRow.CreateID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpAdditionWaitRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpAdditionWaitRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpAdditionWaitRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpAdditionWaitRow.LastChgID.Value)

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal EmpAdditionWaitRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into EmpAdditionWait")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, ValidDate, Seq, AddSeq, AddCompID, AddDeptID, AddOrganID, AddFlowOrganID,")
            strSQL.AppendLine("    Reason, FileNo, Remark, IsBoss, IsSecBoss, IsGroupBoss, IsSecGroupBoss, BossType, ValidMark,")
            strSQL.AppendLine("    ExistsEmpAddition, CreateDate, CreateComp, CreateID, LastChgDate, LastChgComp, LastChgID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @ValidDate, @Seq, @AddSeq, @AddCompID, @AddDeptID, @AddOrganID, @AddFlowOrganID,")
            strSQL.AppendLine("    @Reason, @FileNo, @Remark, @IsBoss, @IsSecBoss, @IsGroupBoss, @IsSecGroupBoss, @BossType, @ValidMark,")
            strSQL.AppendLine("    @ExistsEmpAddition, @CreateDate, @CreateComp, @CreateID, @LastChgDate, @LastChgComp, @LastChgID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpAdditionWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpAdditionWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(EmpAdditionWaitRow.ValidDate.Value), Convert.ToDateTime("1900/1/1"), EmpAdditionWaitRow.ValidDate.Value))
            db.AddInParameter(dbcmd, "@Seq", DbType.Int32, EmpAdditionWaitRow.Seq.Value)
            db.AddInParameter(dbcmd, "@AddSeq", DbType.Int32, EmpAdditionWaitRow.AddSeq.Value)
            db.AddInParameter(dbcmd, "@AddCompID", DbType.String, EmpAdditionWaitRow.AddCompID.Value)
            db.AddInParameter(dbcmd, "@AddDeptID", DbType.String, EmpAdditionWaitRow.AddDeptID.Value)
            db.AddInParameter(dbcmd, "@AddOrganID", DbType.String, EmpAdditionWaitRow.AddOrganID.Value)
            db.AddInParameter(dbcmd, "@AddFlowOrganID", DbType.String, EmpAdditionWaitRow.AddFlowOrganID.Value)
            db.AddInParameter(dbcmd, "@Reason", DbType.String, EmpAdditionWaitRow.Reason.Value)
            db.AddInParameter(dbcmd, "@FileNo", DbType.String, EmpAdditionWaitRow.FileNo.Value)
            db.AddInParameter(dbcmd, "@Remark", DbType.String, EmpAdditionWaitRow.Remark.Value)
            db.AddInParameter(dbcmd, "@IsBoss", DbType.String, EmpAdditionWaitRow.IsBoss.Value)
            db.AddInParameter(dbcmd, "@IsSecBoss", DbType.String, EmpAdditionWaitRow.IsSecBoss.Value)
            db.AddInParameter(dbcmd, "@IsGroupBoss", DbType.String, EmpAdditionWaitRow.IsGroupBoss.Value)
            db.AddInParameter(dbcmd, "@IsSecGroupBoss", DbType.String, EmpAdditionWaitRow.IsSecGroupBoss.Value)
            db.AddInParameter(dbcmd, "@BossType", DbType.String, EmpAdditionWaitRow.BossType.Value)
            db.AddInParameter(dbcmd, "@ValidMark", DbType.String, EmpAdditionWaitRow.ValidMark.Value)
            db.AddInParameter(dbcmd, "@ExistsEmpAddition", DbType.String, EmpAdditionWaitRow.ExistsEmpAddition.Value)
            db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(EmpAdditionWaitRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), EmpAdditionWaitRow.CreateDate.Value))
            db.AddInParameter(dbcmd, "@CreateComp", DbType.String, EmpAdditionWaitRow.CreateComp.Value)
            db.AddInParameter(dbcmd, "@CreateID", DbType.String, EmpAdditionWaitRow.CreateID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpAdditionWaitRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpAdditionWaitRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpAdditionWaitRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpAdditionWaitRow.LastChgID.Value)

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal EmpAdditionWaitRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into EmpAdditionWait")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, ValidDate, Seq, AddSeq, AddCompID, AddDeptID, AddOrganID, AddFlowOrganID,")
            strSQL.AppendLine("    Reason, FileNo, Remark, IsBoss, IsSecBoss, IsGroupBoss, IsSecGroupBoss, BossType, ValidMark,")
            strSQL.AppendLine("    ExistsEmpAddition, CreateDate, CreateComp, CreateID, LastChgDate, LastChgComp, LastChgID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @ValidDate, @Seq, @AddSeq, @AddCompID, @AddDeptID, @AddOrganID, @AddFlowOrganID,")
            strSQL.AppendLine("    @Reason, @FileNo, @Remark, @IsBoss, @IsSecBoss, @IsGroupBoss, @IsSecGroupBoss, @BossType, @ValidMark,")
            strSQL.AppendLine("    @ExistsEmpAddition, @CreateDate, @CreateComp, @CreateID, @LastChgDate, @LastChgComp, @LastChgID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In EmpAdditionWaitRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(r.ValidDate.Value), Convert.ToDateTime("1900/1/1"), r.ValidDate.Value))
                        db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                        db.AddInParameter(dbcmd, "@AddSeq", DbType.Int32, r.AddSeq.Value)
                        db.AddInParameter(dbcmd, "@AddCompID", DbType.String, r.AddCompID.Value)
                        db.AddInParameter(dbcmd, "@AddDeptID", DbType.String, r.AddDeptID.Value)
                        db.AddInParameter(dbcmd, "@AddOrganID", DbType.String, r.AddOrganID.Value)
                        db.AddInParameter(dbcmd, "@AddFlowOrganID", DbType.String, r.AddFlowOrganID.Value)
                        db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                        db.AddInParameter(dbcmd, "@FileNo", DbType.String, r.FileNo.Value)
                        db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                        db.AddInParameter(dbcmd, "@IsBoss", DbType.String, r.IsBoss.Value)
                        db.AddInParameter(dbcmd, "@IsSecBoss", DbType.String, r.IsSecBoss.Value)
                        db.AddInParameter(dbcmd, "@IsGroupBoss", DbType.String, r.IsGroupBoss.Value)
                        db.AddInParameter(dbcmd, "@IsSecGroupBoss", DbType.String, r.IsSecGroupBoss.Value)
                        db.AddInParameter(dbcmd, "@BossType", DbType.String, r.BossType.Value)
                        db.AddInParameter(dbcmd, "@ValidMark", DbType.String, r.ValidMark.Value)
                        db.AddInParameter(dbcmd, "@ExistsEmpAddition", DbType.String, r.ExistsEmpAddition.Value)
                        db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                        db.AddInParameter(dbcmd, "@CreateComp", DbType.String, r.CreateComp.Value)
                        db.AddInParameter(dbcmd, "@CreateID", DbType.String, r.CreateID.Value)
                        db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)

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

        Public Function Insert(ByVal EmpAdditionWaitRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into EmpAdditionWait")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, ValidDate, Seq, AddSeq, AddCompID, AddDeptID, AddOrganID, AddFlowOrganID,")
            strSQL.AppendLine("    Reason, FileNo, Remark, IsBoss, IsSecBoss, IsGroupBoss, IsSecGroupBoss, BossType, ValidMark,")
            strSQL.AppendLine("    ExistsEmpAddition, CreateDate, CreateComp, CreateID, LastChgDate, LastChgComp, LastChgID")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @ValidDate, @Seq, @AddSeq, @AddCompID, @AddDeptID, @AddOrganID, @AddFlowOrganID,")
            strSQL.AppendLine("    @Reason, @FileNo, @Remark, @IsBoss, @IsSecBoss, @IsGroupBoss, @IsSecGroupBoss, @BossType, @ValidMark,")
            strSQL.AppendLine("    @ExistsEmpAddition, @CreateDate, @CreateComp, @CreateID, @LastChgDate, @LastChgComp, @LastChgID")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In EmpAdditionWaitRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@ValidDate", DbType.Date, IIf(IsDateTimeNull(r.ValidDate.Value), Convert.ToDateTime("1900/1/1"), r.ValidDate.Value))
                db.AddInParameter(dbcmd, "@Seq", DbType.Int32, r.Seq.Value)
                db.AddInParameter(dbcmd, "@AddSeq", DbType.Int32, r.AddSeq.Value)
                db.AddInParameter(dbcmd, "@AddCompID", DbType.String, r.AddCompID.Value)
                db.AddInParameter(dbcmd, "@AddDeptID", DbType.String, r.AddDeptID.Value)
                db.AddInParameter(dbcmd, "@AddOrganID", DbType.String, r.AddOrganID.Value)
                db.AddInParameter(dbcmd, "@AddFlowOrganID", DbType.String, r.AddFlowOrganID.Value)
                db.AddInParameter(dbcmd, "@Reason", DbType.String, r.Reason.Value)
                db.AddInParameter(dbcmd, "@FileNo", DbType.String, r.FileNo.Value)
                db.AddInParameter(dbcmd, "@Remark", DbType.String, r.Remark.Value)
                db.AddInParameter(dbcmd, "@IsBoss", DbType.String, r.IsBoss.Value)
                db.AddInParameter(dbcmd, "@IsSecBoss", DbType.String, r.IsSecBoss.Value)
                db.AddInParameter(dbcmd, "@IsGroupBoss", DbType.String, r.IsGroupBoss.Value)
                db.AddInParameter(dbcmd, "@IsSecGroupBoss", DbType.String, r.IsSecGroupBoss.Value)
                db.AddInParameter(dbcmd, "@BossType", DbType.String, r.BossType.Value)
                db.AddInParameter(dbcmd, "@ValidMark", DbType.String, r.ValidMark.Value)
                db.AddInParameter(dbcmd, "@ExistsEmpAddition", DbType.String, r.ExistsEmpAddition.Value)
                db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                db.AddInParameter(dbcmd, "@CreateComp", DbType.String, r.CreateComp.Value)
                db.AddInParameter(dbcmd, "@CreateID", DbType.String, r.CreateID.Value)
                db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)

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

