'****************************************************************
' Table:PersonalOutsourcing
' Created Date: 2015.07.01
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace bePersonalOutsourcing
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "EmpID", "IDNo", "Name", "NameN", "BirthDate", "Sex", "EduID", "HighSchool", "HighDepart" _
                                    , "SchoolStatus", "EmpAttrib", "ContractStartDate", "ContractQuitDate", "EmpDate", "GroupID", "DeptID", "OrganID", "WorkSiteID", "CommTel", "CommAddr" _
                                    , "RelName", "FeeShareDept", "SalaryUnit", "Salary", "Allowance", "EmpSource", "OutsourcingComp", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(Date), GetType(Date), GetType(Date), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "CompID", "EmpID" }

        Public ReadOnly Property Rows() As bePersonalOutsourcing.Rows 
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
        Public Sub Transfer2Row(PersonalOutsourcingTable As DataTable)
            For Each dr As DataRow In PersonalOutsourcingTable.Rows
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
                dr(m_Rows(i).IDNo.FieldName) = m_Rows(i).IDNo.Value
                dr(m_Rows(i).Name.FieldName) = m_Rows(i).Name.Value
                dr(m_Rows(i).NameN.FieldName) = m_Rows(i).NameN.Value
                dr(m_Rows(i).BirthDate.FieldName) = m_Rows(i).BirthDate.Value
                dr(m_Rows(i).Sex.FieldName) = m_Rows(i).Sex.Value
                dr(m_Rows(i).EduID.FieldName) = m_Rows(i).EduID.Value
                dr(m_Rows(i).HighSchool.FieldName) = m_Rows(i).HighSchool.Value
                dr(m_Rows(i).HighDepart.FieldName) = m_Rows(i).HighDepart.Value
                dr(m_Rows(i).SchoolStatus.FieldName) = m_Rows(i).SchoolStatus.Value
                dr(m_Rows(i).EmpAttrib.FieldName) = m_Rows(i).EmpAttrib.Value
                dr(m_Rows(i).ContractStartDate.FieldName) = m_Rows(i).ContractStartDate.Value
                dr(m_Rows(i).ContractQuitDate.FieldName) = m_Rows(i).ContractQuitDate.Value
                dr(m_Rows(i).EmpDate.FieldName) = m_Rows(i).EmpDate.Value
                dr(m_Rows(i).GroupID.FieldName) = m_Rows(i).GroupID.Value
                dr(m_Rows(i).DeptID.FieldName) = m_Rows(i).DeptID.Value
                dr(m_Rows(i).OrganID.FieldName) = m_Rows(i).OrganID.Value
                dr(m_Rows(i).WorkSiteID.FieldName) = m_Rows(i).WorkSiteID.Value
                dr(m_Rows(i).CommTel.FieldName) = m_Rows(i).CommTel.Value
                dr(m_Rows(i).CommAddr.FieldName) = m_Rows(i).CommAddr.Value
                dr(m_Rows(i).RelName.FieldName) = m_Rows(i).RelName.Value
                dr(m_Rows(i).FeeShareDept.FieldName) = m_Rows(i).FeeShareDept.Value
                dr(m_Rows(i).SalaryUnit.FieldName) = m_Rows(i).SalaryUnit.Value
                dr(m_Rows(i).Salary.FieldName) = m_Rows(i).Salary.Value
                dr(m_Rows(i).Allowance.FieldName) = m_Rows(i).Allowance.Value
                dr(m_Rows(i).EmpSource.FieldName) = m_Rows(i).EmpSource.Value
                dr(m_Rows(i).OutsourcingComp.FieldName) = m_Rows(i).OutsourcingComp.Value
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

        Public Sub Add(PersonalOutsourcingRow As Row)
            m_Rows.Add(PersonalOutsourcingRow)
        End Sub

        Public Sub Remove(PersonalOutsourcingRow As Row)
            If m_Rows.IndexOf(PersonalOutsourcingRow) >= 0 Then
                m_Rows.Remove(PersonalOutsourcingRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_EmpID As Field(Of String) = new Field(Of String)("EmpID", true)
        Private FI_IDNo As Field(Of String) = new Field(Of String)("IDNo", true)
        Private FI_Name As Field(Of String) = new Field(Of String)("Name", true)
        Private FI_NameN As Field(Of String) = new Field(Of String)("NameN", true)
        Private FI_BirthDate As Field(Of Date) = new Field(Of Date)("BirthDate", true)
        Private FI_Sex As Field(Of String) = new Field(Of String)("Sex", true)
        Private FI_EduID As Field(Of String) = new Field(Of String)("EduID", true)
        Private FI_HighSchool As Field(Of String) = new Field(Of String)("HighSchool", true)
        Private FI_HighDepart As Field(Of String) = new Field(Of String)("HighDepart", true)
        Private FI_SchoolStatus As Field(Of String) = new Field(Of String)("SchoolStatus", true)
        Private FI_EmpAttrib As Field(Of String) = new Field(Of String)("EmpAttrib", true)
        Private FI_ContractStartDate As Field(Of Date) = new Field(Of Date)("ContractStartDate", true)
        Private FI_ContractQuitDate As Field(Of Date) = new Field(Of Date)("ContractQuitDate", true)
        Private FI_EmpDate As Field(Of Date) = new Field(Of Date)("EmpDate", true)
        Private FI_GroupID As Field(Of String) = new Field(Of String)("GroupID", true)
        Private FI_DeptID As Field(Of String) = new Field(Of String)("DeptID", true)
        Private FI_OrganID As Field(Of String) = new Field(Of String)("OrganID", true)
        Private FI_WorkSiteID As Field(Of String) = new Field(Of String)("WorkSiteID", true)
        Private FI_CommTel As Field(Of String) = new Field(Of String)("CommTel", true)
        Private FI_CommAddr As Field(Of String) = new Field(Of String)("CommAddr", true)
        Private FI_RelName As Field(Of String) = new Field(Of String)("RelName", true)
        Private FI_FeeShareDept As Field(Of String) = new Field(Of String)("FeeShareDept", true)
        Private FI_SalaryUnit As Field(Of String) = new Field(Of String)("SalaryUnit", true)
        Private FI_Salary As Field(Of String) = new Field(Of String)("Salary", true)
        Private FI_Allowance As Field(Of String) = new Field(Of String)("Allowance", true)
        Private FI_EmpSource As Field(Of String) = new Field(Of String)("EmpSource", true)
        Private FI_OutsourcingComp As Field(Of String) = new Field(Of String)("OutsourcingComp", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "CompID", "EmpID", "IDNo", "Name", "NameN", "BirthDate", "Sex", "EduID", "HighSchool", "HighDepart" _
                                    , "SchoolStatus", "EmpAttrib", "ContractStartDate", "ContractQuitDate", "EmpDate", "GroupID", "DeptID", "OrganID", "WorkSiteID", "CommTel", "CommAddr" _
                                    , "RelName", "FeeShareDept", "SalaryUnit", "Salary", "Allowance", "EmpSource", "OutsourcingComp", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "CompID", "EmpID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CompID"
                    Return FI_CompID.Value
                Case "EmpID"
                    Return FI_EmpID.Value
                Case "IDNo"
                    Return FI_IDNo.Value
                Case "Name"
                    Return FI_Name.Value
                Case "NameN"
                    Return FI_NameN.Value
                Case "BirthDate"
                    Return FI_BirthDate.Value
                Case "Sex"
                    Return FI_Sex.Value
                Case "EduID"
                    Return FI_EduID.Value
                Case "HighSchool"
                    Return FI_HighSchool.Value
                Case "HighDepart"
                    Return FI_HighDepart.Value
                Case "SchoolStatus"
                    Return FI_SchoolStatus.Value
                Case "EmpAttrib"
                    Return FI_EmpAttrib.Value
                Case "ContractStartDate"
                    Return FI_ContractStartDate.Value
                Case "ContractQuitDate"
                    Return FI_ContractQuitDate.Value
                Case "EmpDate"
                    Return FI_EmpDate.Value
                Case "GroupID"
                    Return FI_GroupID.Value
                Case "DeptID"
                    Return FI_DeptID.Value
                Case "OrganID"
                    Return FI_OrganID.Value
                Case "WorkSiteID"
                    Return FI_WorkSiteID.Value
                Case "CommTel"
                    Return FI_CommTel.Value
                Case "CommAddr"
                    Return FI_CommAddr.Value
                Case "RelName"
                    Return FI_RelName.Value
                Case "FeeShareDept"
                    Return FI_FeeShareDept.Value
                Case "SalaryUnit"
                    Return FI_SalaryUnit.Value
                Case "Salary"
                    Return FI_Salary.Value
                Case "Allowance"
                    Return FI_Allowance.Value
                Case "EmpSource"
                    Return FI_EmpSource.Value
                Case "OutsourcingComp"
                    Return FI_OutsourcingComp.Value
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
                Case "IDNo"
                    FI_IDNo.SetValue(value)
                Case "Name"
                    FI_Name.SetValue(value)
                Case "NameN"
                    FI_NameN.SetValue(value)
                Case "BirthDate"
                    FI_BirthDate.SetValue(value)
                Case "Sex"
                    FI_Sex.SetValue(value)
                Case "EduID"
                    FI_EduID.SetValue(value)
                Case "HighSchool"
                    FI_HighSchool.SetValue(value)
                Case "HighDepart"
                    FI_HighDepart.SetValue(value)
                Case "SchoolStatus"
                    FI_SchoolStatus.SetValue(value)
                Case "EmpAttrib"
                    FI_EmpAttrib.SetValue(value)
                Case "ContractStartDate"
                    FI_ContractStartDate.SetValue(value)
                Case "ContractQuitDate"
                    FI_ContractQuitDate.SetValue(value)
                Case "EmpDate"
                    FI_EmpDate.SetValue(value)
                Case "GroupID"
                    FI_GroupID.SetValue(value)
                Case "DeptID"
                    FI_DeptID.SetValue(value)
                Case "OrganID"
                    FI_OrganID.SetValue(value)
                Case "WorkSiteID"
                    FI_WorkSiteID.SetValue(value)
                Case "CommTel"
                    FI_CommTel.SetValue(value)
                Case "CommAddr"
                    FI_CommAddr.SetValue(value)
                Case "RelName"
                    FI_RelName.SetValue(value)
                Case "FeeShareDept"
                    FI_FeeShareDept.SetValue(value)
                Case "SalaryUnit"
                    FI_SalaryUnit.SetValue(value)
                Case "Salary"
                    FI_Salary.SetValue(value)
                Case "Allowance"
                    FI_Allowance.SetValue(value)
                Case "EmpSource"
                    FI_EmpSource.SetValue(value)
                Case "OutsourcingComp"
                    FI_OutsourcingComp.SetValue(value)
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
                Case "IDNo"
                    return FI_IDNo.Updated
                Case "Name"
                    return FI_Name.Updated
                Case "NameN"
                    return FI_NameN.Updated
                Case "BirthDate"
                    return FI_BirthDate.Updated
                Case "Sex"
                    return FI_Sex.Updated
                Case "EduID"
                    return FI_EduID.Updated
                Case "HighSchool"
                    return FI_HighSchool.Updated
                Case "HighDepart"
                    return FI_HighDepart.Updated
                Case "SchoolStatus"
                    return FI_SchoolStatus.Updated
                Case "EmpAttrib"
                    return FI_EmpAttrib.Updated
                Case "ContractStartDate"
                    return FI_ContractStartDate.Updated
                Case "ContractQuitDate"
                    return FI_ContractQuitDate.Updated
                Case "EmpDate"
                    return FI_EmpDate.Updated
                Case "GroupID"
                    return FI_GroupID.Updated
                Case "DeptID"
                    return FI_DeptID.Updated
                Case "OrganID"
                    return FI_OrganID.Updated
                Case "WorkSiteID"
                    return FI_WorkSiteID.Updated
                Case "CommTel"
                    return FI_CommTel.Updated
                Case "CommAddr"
                    return FI_CommAddr.Updated
                Case "RelName"
                    return FI_RelName.Updated
                Case "FeeShareDept"
                    return FI_FeeShareDept.Updated
                Case "SalaryUnit"
                    return FI_SalaryUnit.Updated
                Case "Salary"
                    return FI_Salary.Updated
                Case "Allowance"
                    return FI_Allowance.Updated
                Case "EmpSource"
                    return FI_EmpSource.Updated
                Case "OutsourcingComp"
                    return FI_OutsourcingComp.Updated
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
                Case "IDNo"
                    return FI_IDNo.CreateUpdateSQL
                Case "Name"
                    return FI_Name.CreateUpdateSQL
                Case "NameN"
                    return FI_NameN.CreateUpdateSQL
                Case "BirthDate"
                    return FI_BirthDate.CreateUpdateSQL
                Case "Sex"
                    return FI_Sex.CreateUpdateSQL
                Case "EduID"
                    return FI_EduID.CreateUpdateSQL
                Case "HighSchool"
                    return FI_HighSchool.CreateUpdateSQL
                Case "HighDepart"
                    return FI_HighDepart.CreateUpdateSQL
                Case "SchoolStatus"
                    return FI_SchoolStatus.CreateUpdateSQL
                Case "EmpAttrib"
                    return FI_EmpAttrib.CreateUpdateSQL
                Case "ContractStartDate"
                    return FI_ContractStartDate.CreateUpdateSQL
                Case "ContractQuitDate"
                    return FI_ContractQuitDate.CreateUpdateSQL
                Case "EmpDate"
                    return FI_EmpDate.CreateUpdateSQL
                Case "GroupID"
                    return FI_GroupID.CreateUpdateSQL
                Case "DeptID"
                    return FI_DeptID.CreateUpdateSQL
                Case "OrganID"
                    return FI_OrganID.CreateUpdateSQL
                Case "WorkSiteID"
                    return FI_WorkSiteID.CreateUpdateSQL
                Case "CommTel"
                    return FI_CommTel.CreateUpdateSQL
                Case "CommAddr"
                    return FI_CommAddr.CreateUpdateSQL
                Case "RelName"
                    return FI_RelName.CreateUpdateSQL
                Case "FeeShareDept"
                    return FI_FeeShareDept.CreateUpdateSQL
                Case "SalaryUnit"
                    return FI_SalaryUnit.CreateUpdateSQL
                Case "Salary"
                    return FI_Salary.CreateUpdateSQL
                Case "Allowance"
                    return FI_Allowance.CreateUpdateSQL
                Case "EmpSource"
                    return FI_EmpSource.CreateUpdateSQL
                Case "OutsourcingComp"
                    return FI_OutsourcingComp.CreateUpdateSQL
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
            FI_IDNo.SetInitValue("")
            FI_Name.SetInitValue("")
            FI_NameN.SetInitValue("")
            FI_BirthDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_Sex.SetInitValue("1")
            FI_EduID.SetInitValue("")
            FI_HighSchool.SetInitValue("")
            FI_HighDepart.SetInitValue("")
            FI_SchoolStatus.SetInitValue("")
            FI_EmpAttrib.SetInitValue("")
            FI_ContractStartDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_ContractQuitDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_EmpDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_GroupID.SetInitValue("")
            FI_DeptID.SetInitValue("")
            FI_OrganID.SetInitValue("")
            FI_WorkSiteID.SetInitValue("")
            FI_CommTel.SetInitValue("")
            FI_CommAddr.SetInitValue("")
            FI_RelName.SetInitValue("")
            FI_FeeShareDept.SetInitValue("")
            FI_SalaryUnit.SetInitValue("")
            FI_Salary.SetInitValue("")
            FI_Allowance.SetInitValue("")
            FI_EmpSource.SetInitValue("")
            FI_OutsourcingComp.SetInitValue("")
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CompID.SetInitValue(dr("CompID"))
            FI_EmpID.SetInitValue(dr("EmpID"))
            FI_IDNo.SetInitValue(dr("IDNo"))
            FI_Name.SetInitValue(dr("Name"))
            FI_NameN.SetInitValue(dr("NameN"))
            FI_BirthDate.SetInitValue(dr("BirthDate"))
            FI_Sex.SetInitValue(dr("Sex"))
            FI_EduID.SetInitValue(dr("EduID"))
            FI_HighSchool.SetInitValue(dr("HighSchool"))
            FI_HighDepart.SetInitValue(dr("HighDepart"))
            FI_SchoolStatus.SetInitValue(dr("SchoolStatus"))
            FI_EmpAttrib.SetInitValue(dr("EmpAttrib"))
            FI_ContractStartDate.SetInitValue(dr("ContractStartDate"))
            FI_ContractQuitDate.SetInitValue(dr("ContractQuitDate"))
            FI_EmpDate.SetInitValue(dr("EmpDate"))
            FI_GroupID.SetInitValue(dr("GroupID"))
            FI_DeptID.SetInitValue(dr("DeptID"))
            FI_OrganID.SetInitValue(dr("OrganID"))
            FI_WorkSiteID.SetInitValue(dr("WorkSiteID"))
            FI_CommTel.SetInitValue(dr("CommTel"))
            FI_CommAddr.SetInitValue(dr("CommAddr"))
            FI_RelName.SetInitValue(dr("RelName"))
            FI_FeeShareDept.SetInitValue(dr("FeeShareDept"))
            FI_SalaryUnit.SetInitValue(dr("SalaryUnit"))
            FI_Salary.SetInitValue(dr("Salary"))
            FI_Allowance.SetInitValue(dr("Allowance"))
            FI_EmpSource.SetInitValue(dr("EmpSource"))
            FI_OutsourcingComp.SetInitValue(dr("OutsourcingComp"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CompID.Updated = False
            FI_EmpID.Updated = False
            FI_IDNo.Updated = False
            FI_Name.Updated = False
            FI_NameN.Updated = False
            FI_BirthDate.Updated = False
            FI_Sex.Updated = False
            FI_EduID.Updated = False
            FI_HighSchool.Updated = False
            FI_HighDepart.Updated = False
            FI_SchoolStatus.Updated = False
            FI_EmpAttrib.Updated = False
            FI_ContractStartDate.Updated = False
            FI_ContractQuitDate.Updated = False
            FI_EmpDate.Updated = False
            FI_GroupID.Updated = False
            FI_DeptID.Updated = False
            FI_OrganID.Updated = False
            FI_WorkSiteID.Updated = False
            FI_CommTel.Updated = False
            FI_CommAddr.Updated = False
            FI_RelName.Updated = False
            FI_FeeShareDept.Updated = False
            FI_SalaryUnit.Updated = False
            FI_Salary.Updated = False
            FI_Allowance.Updated = False
            FI_EmpSource.Updated = False
            FI_OutsourcingComp.Updated = False
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

        Public ReadOnly Property IDNo As Field(Of String) 
            Get
                Return FI_IDNo
            End Get
        End Property

        Public ReadOnly Property Name As Field(Of String) 
            Get
                Return FI_Name
            End Get
        End Property

        Public ReadOnly Property NameN As Field(Of String) 
            Get
                Return FI_NameN
            End Get
        End Property

        Public ReadOnly Property BirthDate As Field(Of Date) 
            Get
                Return FI_BirthDate
            End Get
        End Property

        Public ReadOnly Property Sex As Field(Of String) 
            Get
                Return FI_Sex
            End Get
        End Property

        Public ReadOnly Property EduID As Field(Of String) 
            Get
                Return FI_EduID
            End Get
        End Property

        Public ReadOnly Property HighSchool As Field(Of String) 
            Get
                Return FI_HighSchool
            End Get
        End Property

        Public ReadOnly Property HighDepart As Field(Of String) 
            Get
                Return FI_HighDepart
            End Get
        End Property

        Public ReadOnly Property SchoolStatus As Field(Of String) 
            Get
                Return FI_SchoolStatus
            End Get
        End Property

        Public ReadOnly Property EmpAttrib As Field(Of String) 
            Get
                Return FI_EmpAttrib
            End Get
        End Property

        Public ReadOnly Property ContractStartDate As Field(Of Date) 
            Get
                Return FI_ContractStartDate
            End Get
        End Property

        Public ReadOnly Property ContractQuitDate As Field(Of Date) 
            Get
                Return FI_ContractQuitDate
            End Get
        End Property

        Public ReadOnly Property EmpDate As Field(Of Date) 
            Get
                Return FI_EmpDate
            End Get
        End Property

        Public ReadOnly Property GroupID As Field(Of String) 
            Get
                Return FI_GroupID
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

        Public ReadOnly Property WorkSiteID As Field(Of String) 
            Get
                Return FI_WorkSiteID
            End Get
        End Property

        Public ReadOnly Property CommTel As Field(Of String) 
            Get
                Return FI_CommTel
            End Get
        End Property

        Public ReadOnly Property CommAddr As Field(Of String) 
            Get
                Return FI_CommAddr
            End Get
        End Property

        Public ReadOnly Property RelName As Field(Of String) 
            Get
                Return FI_RelName
            End Get
        End Property

        Public ReadOnly Property FeeShareDept As Field(Of String) 
            Get
                Return FI_FeeShareDept
            End Get
        End Property

        Public ReadOnly Property SalaryUnit As Field(Of String) 
            Get
                Return FI_SalaryUnit
            End Get
        End Property

        Public ReadOnly Property Salary As Field(Of String) 
            Get
                Return FI_Salary
            End Get
        End Property

        Public ReadOnly Property Allowance As Field(Of String) 
            Get
                Return FI_Allowance
            End Get
        End Property

        Public ReadOnly Property EmpSource As Field(Of String) 
            Get
                Return FI_EmpSource
            End Get
        End Property

        Public ReadOnly Property OutsourcingComp As Field(Of String) 
            Get
                Return FI_OutsourcingComp
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
        Public Function DeleteRowByPrimaryKey(ByVal PersonalOutsourcingRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From PersonalOutsourcing")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PersonalOutsourcingRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, PersonalOutsourcingRow.EmpID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal PersonalOutsourcingRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From PersonalOutsourcing")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PersonalOutsourcingRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, PersonalOutsourcingRow.EmpID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal PersonalOutsourcingRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From PersonalOutsourcing")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In PersonalOutsourcingRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal PersonalOutsourcingRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From PersonalOutsourcing")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In PersonalOutsourcingRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal PersonalOutsourcingRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From PersonalOutsourcing")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PersonalOutsourcingRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, PersonalOutsourcingRow.EmpID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(PersonalOutsourcingRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From PersonalOutsourcing")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PersonalOutsourcingRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, PersonalOutsourcingRow.EmpID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal PersonalOutsourcingRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update PersonalOutsourcing Set")
            For i As Integer = 0 To PersonalOutsourcingRow.FieldNames.Length - 1
                If Not PersonalOutsourcingRow.IsIdentityField(PersonalOutsourcingRow.FieldNames(i)) AndAlso PersonalOutsourcingRow.IsUpdated(PersonalOutsourcingRow.FieldNames(i)) AndAlso PersonalOutsourcingRow.CreateUpdateSQL(PersonalOutsourcingRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, PersonalOutsourcingRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And EmpID = @PKEmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If PersonalOutsourcingRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, PersonalOutsourcingRow.CompID.Value)
            If PersonalOutsourcingRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, PersonalOutsourcingRow.EmpID.Value)
            If PersonalOutsourcingRow.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, PersonalOutsourcingRow.IDNo.Value)
            If PersonalOutsourcingRow.Name.Updated Then db.AddInParameter(dbcmd, "@Name", DbType.String, PersonalOutsourcingRow.Name.Value)
            If PersonalOutsourcingRow.NameN.Updated Then db.AddInParameter(dbcmd, "@NameN", DbType.String, PersonalOutsourcingRow.NameN.Value)
            If PersonalOutsourcingRow.BirthDate.Updated Then db.AddInParameter(dbcmd, "@BirthDate", DbType.Date, IIf(IsDateTimeNull(PersonalOutsourcingRow.BirthDate.Value), Convert.ToDateTime("1900/1/1"), PersonalOutsourcingRow.BirthDate.Value))
            If PersonalOutsourcingRow.Sex.Updated Then db.AddInParameter(dbcmd, "@Sex", DbType.String, PersonalOutsourcingRow.Sex.Value)
            If PersonalOutsourcingRow.EduID.Updated Then db.AddInParameter(dbcmd, "@EduID", DbType.String, PersonalOutsourcingRow.EduID.Value)
            If PersonalOutsourcingRow.HighSchool.Updated Then db.AddInParameter(dbcmd, "@HighSchool", DbType.String, PersonalOutsourcingRow.HighSchool.Value)
            If PersonalOutsourcingRow.HighDepart.Updated Then db.AddInParameter(dbcmd, "@HighDepart", DbType.String, PersonalOutsourcingRow.HighDepart.Value)
            If PersonalOutsourcingRow.SchoolStatus.Updated Then db.AddInParameter(dbcmd, "@SchoolStatus", DbType.String, PersonalOutsourcingRow.SchoolStatus.Value)
            If PersonalOutsourcingRow.EmpAttrib.Updated Then db.AddInParameter(dbcmd, "@EmpAttrib", DbType.String, PersonalOutsourcingRow.EmpAttrib.Value)
            If PersonalOutsourcingRow.ContractStartDate.Updated Then db.AddInParameter(dbcmd, "@ContractStartDate", DbType.Date, IIf(IsDateTimeNull(PersonalOutsourcingRow.ContractStartDate.Value), Convert.ToDateTime("1900/1/1"), PersonalOutsourcingRow.ContractStartDate.Value))
            If PersonalOutsourcingRow.ContractQuitDate.Updated Then db.AddInParameter(dbcmd, "@ContractQuitDate", DbType.Date, IIf(IsDateTimeNull(PersonalOutsourcingRow.ContractQuitDate.Value), Convert.ToDateTime("1900/1/1"), PersonalOutsourcingRow.ContractQuitDate.Value))
            If PersonalOutsourcingRow.EmpDate.Updated Then db.AddInParameter(dbcmd, "@EmpDate", DbType.Date, IIf(IsDateTimeNull(PersonalOutsourcingRow.EmpDate.Value), Convert.ToDateTime("1900/1/1"), PersonalOutsourcingRow.EmpDate.Value))
            If PersonalOutsourcingRow.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, PersonalOutsourcingRow.GroupID.Value)
            If PersonalOutsourcingRow.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, PersonalOutsourcingRow.DeptID.Value)
            If PersonalOutsourcingRow.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, PersonalOutsourcingRow.OrganID.Value)
            If PersonalOutsourcingRow.WorkSiteID.Updated Then db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, PersonalOutsourcingRow.WorkSiteID.Value)
            If PersonalOutsourcingRow.CommTel.Updated Then db.AddInParameter(dbcmd, "@CommTel", DbType.String, PersonalOutsourcingRow.CommTel.Value)
            If PersonalOutsourcingRow.CommAddr.Updated Then db.AddInParameter(dbcmd, "@CommAddr", DbType.String, PersonalOutsourcingRow.CommAddr.Value)
            If PersonalOutsourcingRow.RelName.Updated Then db.AddInParameter(dbcmd, "@RelName", DbType.String, PersonalOutsourcingRow.RelName.Value)
            If PersonalOutsourcingRow.FeeShareDept.Updated Then db.AddInParameter(dbcmd, "@FeeShareDept", DbType.String, PersonalOutsourcingRow.FeeShareDept.Value)
            If PersonalOutsourcingRow.SalaryUnit.Updated Then db.AddInParameter(dbcmd, "@SalaryUnit", DbType.String, PersonalOutsourcingRow.SalaryUnit.Value)
            If PersonalOutsourcingRow.Salary.Updated Then db.AddInParameter(dbcmd, "@Salary", DbType.String, PersonalOutsourcingRow.Salary.Value)
            If PersonalOutsourcingRow.Allowance.Updated Then db.AddInParameter(dbcmd, "@Allowance", DbType.String, PersonalOutsourcingRow.Allowance.Value)
            If PersonalOutsourcingRow.EmpSource.Updated Then db.AddInParameter(dbcmd, "@EmpSource", DbType.String, PersonalOutsourcingRow.EmpSource.Value)
            If PersonalOutsourcingRow.OutsourcingComp.Updated Then db.AddInParameter(dbcmd, "@OutsourcingComp", DbType.String, PersonalOutsourcingRow.OutsourcingComp.Value)
            If PersonalOutsourcingRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, PersonalOutsourcingRow.LastChgComp.Value)
            If PersonalOutsourcingRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, PersonalOutsourcingRow.LastChgID.Value)
            If PersonalOutsourcingRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(PersonalOutsourcingRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), PersonalOutsourcingRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(PersonalOutsourcingRow.LoadFromDataRow, PersonalOutsourcingRow.CompID.OldValue, PersonalOutsourcingRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(PersonalOutsourcingRow.LoadFromDataRow, PersonalOutsourcingRow.EmpID.OldValue, PersonalOutsourcingRow.EmpID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal PersonalOutsourcingRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update PersonalOutsourcing Set")
            For i As Integer = 0 To PersonalOutsourcingRow.FieldNames.Length - 1
                If Not PersonalOutsourcingRow.IsIdentityField(PersonalOutsourcingRow.FieldNames(i)) AndAlso PersonalOutsourcingRow.IsUpdated(PersonalOutsourcingRow.FieldNames(i)) AndAlso PersonalOutsourcingRow.CreateUpdateSQL(PersonalOutsourcingRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, PersonalOutsourcingRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And EmpID = @PKEmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If PersonalOutsourcingRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, PersonalOutsourcingRow.CompID.Value)
            If PersonalOutsourcingRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, PersonalOutsourcingRow.EmpID.Value)
            If PersonalOutsourcingRow.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, PersonalOutsourcingRow.IDNo.Value)
            If PersonalOutsourcingRow.Name.Updated Then db.AddInParameter(dbcmd, "@Name", DbType.String, PersonalOutsourcingRow.Name.Value)
            If PersonalOutsourcingRow.NameN.Updated Then db.AddInParameter(dbcmd, "@NameN", DbType.String, PersonalOutsourcingRow.NameN.Value)
            If PersonalOutsourcingRow.BirthDate.Updated Then db.AddInParameter(dbcmd, "@BirthDate", DbType.Date, IIf(IsDateTimeNull(PersonalOutsourcingRow.BirthDate.Value), Convert.ToDateTime("1900/1/1"), PersonalOutsourcingRow.BirthDate.Value))
            If PersonalOutsourcingRow.Sex.Updated Then db.AddInParameter(dbcmd, "@Sex", DbType.String, PersonalOutsourcingRow.Sex.Value)
            If PersonalOutsourcingRow.EduID.Updated Then db.AddInParameter(dbcmd, "@EduID", DbType.String, PersonalOutsourcingRow.EduID.Value)
            If PersonalOutsourcingRow.HighSchool.Updated Then db.AddInParameter(dbcmd, "@HighSchool", DbType.String, PersonalOutsourcingRow.HighSchool.Value)
            If PersonalOutsourcingRow.HighDepart.Updated Then db.AddInParameter(dbcmd, "@HighDepart", DbType.String, PersonalOutsourcingRow.HighDepart.Value)
            If PersonalOutsourcingRow.SchoolStatus.Updated Then db.AddInParameter(dbcmd, "@SchoolStatus", DbType.String, PersonalOutsourcingRow.SchoolStatus.Value)
            If PersonalOutsourcingRow.EmpAttrib.Updated Then db.AddInParameter(dbcmd, "@EmpAttrib", DbType.String, PersonalOutsourcingRow.EmpAttrib.Value)
            If PersonalOutsourcingRow.ContractStartDate.Updated Then db.AddInParameter(dbcmd, "@ContractStartDate", DbType.Date, IIf(IsDateTimeNull(PersonalOutsourcingRow.ContractStartDate.Value), Convert.ToDateTime("1900/1/1"), PersonalOutsourcingRow.ContractStartDate.Value))
            If PersonalOutsourcingRow.ContractQuitDate.Updated Then db.AddInParameter(dbcmd, "@ContractQuitDate", DbType.Date, IIf(IsDateTimeNull(PersonalOutsourcingRow.ContractQuitDate.Value), Convert.ToDateTime("1900/1/1"), PersonalOutsourcingRow.ContractQuitDate.Value))
            If PersonalOutsourcingRow.EmpDate.Updated Then db.AddInParameter(dbcmd, "@EmpDate", DbType.Date, IIf(IsDateTimeNull(PersonalOutsourcingRow.EmpDate.Value), Convert.ToDateTime("1900/1/1"), PersonalOutsourcingRow.EmpDate.Value))
            If PersonalOutsourcingRow.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, PersonalOutsourcingRow.GroupID.Value)
            If PersonalOutsourcingRow.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, PersonalOutsourcingRow.DeptID.Value)
            If PersonalOutsourcingRow.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, PersonalOutsourcingRow.OrganID.Value)
            If PersonalOutsourcingRow.WorkSiteID.Updated Then db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, PersonalOutsourcingRow.WorkSiteID.Value)
            If PersonalOutsourcingRow.CommTel.Updated Then db.AddInParameter(dbcmd, "@CommTel", DbType.String, PersonalOutsourcingRow.CommTel.Value)
            If PersonalOutsourcingRow.CommAddr.Updated Then db.AddInParameter(dbcmd, "@CommAddr", DbType.String, PersonalOutsourcingRow.CommAddr.Value)
            If PersonalOutsourcingRow.RelName.Updated Then db.AddInParameter(dbcmd, "@RelName", DbType.String, PersonalOutsourcingRow.RelName.Value)
            If PersonalOutsourcingRow.FeeShareDept.Updated Then db.AddInParameter(dbcmd, "@FeeShareDept", DbType.String, PersonalOutsourcingRow.FeeShareDept.Value)
            If PersonalOutsourcingRow.SalaryUnit.Updated Then db.AddInParameter(dbcmd, "@SalaryUnit", DbType.String, PersonalOutsourcingRow.SalaryUnit.Value)
            If PersonalOutsourcingRow.Salary.Updated Then db.AddInParameter(dbcmd, "@Salary", DbType.String, PersonalOutsourcingRow.Salary.Value)
            If PersonalOutsourcingRow.Allowance.Updated Then db.AddInParameter(dbcmd, "@Allowance", DbType.String, PersonalOutsourcingRow.Allowance.Value)
            If PersonalOutsourcingRow.EmpSource.Updated Then db.AddInParameter(dbcmd, "@EmpSource", DbType.String, PersonalOutsourcingRow.EmpSource.Value)
            If PersonalOutsourcingRow.OutsourcingComp.Updated Then db.AddInParameter(dbcmd, "@OutsourcingComp", DbType.String, PersonalOutsourcingRow.OutsourcingComp.Value)
            If PersonalOutsourcingRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, PersonalOutsourcingRow.LastChgComp.Value)
            If PersonalOutsourcingRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, PersonalOutsourcingRow.LastChgID.Value)
            If PersonalOutsourcingRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(PersonalOutsourcingRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), PersonalOutsourcingRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(PersonalOutsourcingRow.LoadFromDataRow, PersonalOutsourcingRow.CompID.OldValue, PersonalOutsourcingRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(PersonalOutsourcingRow.LoadFromDataRow, PersonalOutsourcingRow.EmpID.OldValue, PersonalOutsourcingRow.EmpID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal PersonalOutsourcingRow As Row()) As Integer
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
                    For Each r As Row In PersonalOutsourcingRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update PersonalOutsourcing Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where CompID = @PKCompID")
                        strSQL.AppendLine("And EmpID = @PKEmpID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        If r.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                        If r.Name.Updated Then db.AddInParameter(dbcmd, "@Name", DbType.String, r.Name.Value)
                        If r.NameN.Updated Then db.AddInParameter(dbcmd, "@NameN", DbType.String, r.NameN.Value)
                        If r.BirthDate.Updated Then db.AddInParameter(dbcmd, "@BirthDate", DbType.Date, IIf(IsDateTimeNull(r.BirthDate.Value), Convert.ToDateTime("1900/1/1"), r.BirthDate.Value))
                        If r.Sex.Updated Then db.AddInParameter(dbcmd, "@Sex", DbType.String, r.Sex.Value)
                        If r.EduID.Updated Then db.AddInParameter(dbcmd, "@EduID", DbType.String, r.EduID.Value)
                        If r.HighSchool.Updated Then db.AddInParameter(dbcmd, "@HighSchool", DbType.String, r.HighSchool.Value)
                        If r.HighDepart.Updated Then db.AddInParameter(dbcmd, "@HighDepart", DbType.String, r.HighDepart.Value)
                        If r.SchoolStatus.Updated Then db.AddInParameter(dbcmd, "@SchoolStatus", DbType.String, r.SchoolStatus.Value)
                        If r.EmpAttrib.Updated Then db.AddInParameter(dbcmd, "@EmpAttrib", DbType.String, r.EmpAttrib.Value)
                        If r.ContractStartDate.Updated Then db.AddInParameter(dbcmd, "@ContractStartDate", DbType.Date, IIf(IsDateTimeNull(r.ContractStartDate.Value), Convert.ToDateTime("1900/1/1"), r.ContractStartDate.Value))
                        If r.ContractQuitDate.Updated Then db.AddInParameter(dbcmd, "@ContractQuitDate", DbType.Date, IIf(IsDateTimeNull(r.ContractQuitDate.Value), Convert.ToDateTime("1900/1/1"), r.ContractQuitDate.Value))
                        If r.EmpDate.Updated Then db.AddInParameter(dbcmd, "@EmpDate", DbType.Date, IIf(IsDateTimeNull(r.EmpDate.Value), Convert.ToDateTime("1900/1/1"), r.EmpDate.Value))
                        If r.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                        If r.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                        If r.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                        If r.WorkSiteID.Updated Then db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, r.WorkSiteID.Value)
                        If r.CommTel.Updated Then db.AddInParameter(dbcmd, "@CommTel", DbType.String, r.CommTel.Value)
                        If r.CommAddr.Updated Then db.AddInParameter(dbcmd, "@CommAddr", DbType.String, r.CommAddr.Value)
                        If r.RelName.Updated Then db.AddInParameter(dbcmd, "@RelName", DbType.String, r.RelName.Value)
                        If r.FeeShareDept.Updated Then db.AddInParameter(dbcmd, "@FeeShareDept", DbType.String, r.FeeShareDept.Value)
                        If r.SalaryUnit.Updated Then db.AddInParameter(dbcmd, "@SalaryUnit", DbType.String, r.SalaryUnit.Value)
                        If r.Salary.Updated Then db.AddInParameter(dbcmd, "@Salary", DbType.String, r.Salary.Value)
                        If r.Allowance.Updated Then db.AddInParameter(dbcmd, "@Allowance", DbType.String, r.Allowance.Value)
                        If r.EmpSource.Updated Then db.AddInParameter(dbcmd, "@EmpSource", DbType.String, r.EmpSource.Value)
                        If r.OutsourcingComp.Updated Then db.AddInParameter(dbcmd, "@OutsourcingComp", DbType.String, r.OutsourcingComp.Value)
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                        db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))

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

        Public Function Update(ByVal PersonalOutsourcingRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In PersonalOutsourcingRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update PersonalOutsourcing Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where CompID = @PKCompID")
                strSQL.AppendLine("And EmpID = @PKEmpID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                If r.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                If r.Name.Updated Then db.AddInParameter(dbcmd, "@Name", DbType.String, r.Name.Value)
                If r.NameN.Updated Then db.AddInParameter(dbcmd, "@NameN", DbType.String, r.NameN.Value)
                If r.BirthDate.Updated Then db.AddInParameter(dbcmd, "@BirthDate", DbType.Date, IIf(IsDateTimeNull(r.BirthDate.Value), Convert.ToDateTime("1900/1/1"), r.BirthDate.Value))
                If r.Sex.Updated Then db.AddInParameter(dbcmd, "@Sex", DbType.String, r.Sex.Value)
                If r.EduID.Updated Then db.AddInParameter(dbcmd, "@EduID", DbType.String, r.EduID.Value)
                If r.HighSchool.Updated Then db.AddInParameter(dbcmd, "@HighSchool", DbType.String, r.HighSchool.Value)
                If r.HighDepart.Updated Then db.AddInParameter(dbcmd, "@HighDepart", DbType.String, r.HighDepart.Value)
                If r.SchoolStatus.Updated Then db.AddInParameter(dbcmd, "@SchoolStatus", DbType.String, r.SchoolStatus.Value)
                If r.EmpAttrib.Updated Then db.AddInParameter(dbcmd, "@EmpAttrib", DbType.String, r.EmpAttrib.Value)
                If r.ContractStartDate.Updated Then db.AddInParameter(dbcmd, "@ContractStartDate", DbType.Date, IIf(IsDateTimeNull(r.ContractStartDate.Value), Convert.ToDateTime("1900/1/1"), r.ContractStartDate.Value))
                If r.ContractQuitDate.Updated Then db.AddInParameter(dbcmd, "@ContractQuitDate", DbType.Date, IIf(IsDateTimeNull(r.ContractQuitDate.Value), Convert.ToDateTime("1900/1/1"), r.ContractQuitDate.Value))
                If r.EmpDate.Updated Then db.AddInParameter(dbcmd, "@EmpDate", DbType.Date, IIf(IsDateTimeNull(r.EmpDate.Value), Convert.ToDateTime("1900/1/1"), r.EmpDate.Value))
                If r.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                If r.DeptID.Updated Then db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                If r.OrganID.Updated Then db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                If r.WorkSiteID.Updated Then db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, r.WorkSiteID.Value)
                If r.CommTel.Updated Then db.AddInParameter(dbcmd, "@CommTel", DbType.String, r.CommTel.Value)
                If r.CommAddr.Updated Then db.AddInParameter(dbcmd, "@CommAddr", DbType.String, r.CommAddr.Value)
                If r.RelName.Updated Then db.AddInParameter(dbcmd, "@RelName", DbType.String, r.RelName.Value)
                If r.FeeShareDept.Updated Then db.AddInParameter(dbcmd, "@FeeShareDept", DbType.String, r.FeeShareDept.Value)
                If r.SalaryUnit.Updated Then db.AddInParameter(dbcmd, "@SalaryUnit", DbType.String, r.SalaryUnit.Value)
                If r.Salary.Updated Then db.AddInParameter(dbcmd, "@Salary", DbType.String, r.Salary.Value)
                If r.Allowance.Updated Then db.AddInParameter(dbcmd, "@Allowance", DbType.String, r.Allowance.Value)
                If r.EmpSource.Updated Then db.AddInParameter(dbcmd, "@EmpSource", DbType.String, r.EmpSource.Value)
                If r.OutsourcingComp.Updated Then db.AddInParameter(dbcmd, "@OutsourcingComp", DbType.String, r.OutsourcingComp.Value)
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal PersonalOutsourcingRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From PersonalOutsourcing")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PersonalOutsourcingRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, PersonalOutsourcingRow.EmpID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal PersonalOutsourcingRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From PersonalOutsourcing")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PersonalOutsourcingRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, PersonalOutsourcingRow.EmpID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From PersonalOutsourcing")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal PersonalOutsourcingRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into PersonalOutsourcing")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, IDNo, Name, NameN, BirthDate, Sex, EduID, HighSchool, HighDepart,")
            strSQL.AppendLine("    SchoolStatus, EmpAttrib, ContractStartDate, ContractQuitDate, EmpDate, GroupID, DeptID,")
            strSQL.AppendLine("    OrganID, WorkSiteID, CommTel, CommAddr, RelName, FeeShareDept, SalaryUnit, Salary,")
            strSQL.AppendLine("    Allowance, EmpSource, OutsourcingComp, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @IDNo, @Name, @NameN, @BirthDate, @Sex, @EduID, @HighSchool, @HighDepart,")
            strSQL.AppendLine("    @SchoolStatus, @EmpAttrib, @ContractStartDate, @ContractQuitDate, @EmpDate, @GroupID, @DeptID,")
            strSQL.AppendLine("    @OrganID, @WorkSiteID, @CommTel, @CommAddr, @RelName, @FeeShareDept, @SalaryUnit, @Salary,")
            strSQL.AppendLine("    @Allowance, @EmpSource, @OutsourcingComp, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PersonalOutsourcingRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, PersonalOutsourcingRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, PersonalOutsourcingRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@Name", DbType.String, PersonalOutsourcingRow.Name.Value)
            db.AddInParameter(dbcmd, "@NameN", DbType.String, PersonalOutsourcingRow.NameN.Value)
            db.AddInParameter(dbcmd, "@BirthDate", DbType.Date, IIf(IsDateTimeNull(PersonalOutsourcingRow.BirthDate.Value), Convert.ToDateTime("1900/1/1"), PersonalOutsourcingRow.BirthDate.Value))
            db.AddInParameter(dbcmd, "@Sex", DbType.String, PersonalOutsourcingRow.Sex.Value)
            db.AddInParameter(dbcmd, "@EduID", DbType.String, PersonalOutsourcingRow.EduID.Value)
            db.AddInParameter(dbcmd, "@HighSchool", DbType.String, PersonalOutsourcingRow.HighSchool.Value)
            db.AddInParameter(dbcmd, "@HighDepart", DbType.String, PersonalOutsourcingRow.HighDepart.Value)
            db.AddInParameter(dbcmd, "@SchoolStatus", DbType.String, PersonalOutsourcingRow.SchoolStatus.Value)
            db.AddInParameter(dbcmd, "@EmpAttrib", DbType.String, PersonalOutsourcingRow.EmpAttrib.Value)
            db.AddInParameter(dbcmd, "@ContractStartDate", DbType.Date, IIf(IsDateTimeNull(PersonalOutsourcingRow.ContractStartDate.Value), Convert.ToDateTime("1900/1/1"), PersonalOutsourcingRow.ContractStartDate.Value))
            db.AddInParameter(dbcmd, "@ContractQuitDate", DbType.Date, IIf(IsDateTimeNull(PersonalOutsourcingRow.ContractQuitDate.Value), Convert.ToDateTime("1900/1/1"), PersonalOutsourcingRow.ContractQuitDate.Value))
            db.AddInParameter(dbcmd, "@EmpDate", DbType.Date, IIf(IsDateTimeNull(PersonalOutsourcingRow.EmpDate.Value), Convert.ToDateTime("1900/1/1"), PersonalOutsourcingRow.EmpDate.Value))
            db.AddInParameter(dbcmd, "@GroupID", DbType.String, PersonalOutsourcingRow.GroupID.Value)
            db.AddInParameter(dbcmd, "@DeptID", DbType.String, PersonalOutsourcingRow.DeptID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, PersonalOutsourcingRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, PersonalOutsourcingRow.WorkSiteID.Value)
            db.AddInParameter(dbcmd, "@CommTel", DbType.String, PersonalOutsourcingRow.CommTel.Value)
            db.AddInParameter(dbcmd, "@CommAddr", DbType.String, PersonalOutsourcingRow.CommAddr.Value)
            db.AddInParameter(dbcmd, "@RelName", DbType.String, PersonalOutsourcingRow.RelName.Value)
            db.AddInParameter(dbcmd, "@FeeShareDept", DbType.String, PersonalOutsourcingRow.FeeShareDept.Value)
            db.AddInParameter(dbcmd, "@SalaryUnit", DbType.String, PersonalOutsourcingRow.SalaryUnit.Value)
            db.AddInParameter(dbcmd, "@Salary", DbType.String, PersonalOutsourcingRow.Salary.Value)
            db.AddInParameter(dbcmd, "@Allowance", DbType.String, PersonalOutsourcingRow.Allowance.Value)
            db.AddInParameter(dbcmd, "@EmpSource", DbType.String, PersonalOutsourcingRow.EmpSource.Value)
            db.AddInParameter(dbcmd, "@OutsourcingComp", DbType.String, PersonalOutsourcingRow.OutsourcingComp.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, PersonalOutsourcingRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, PersonalOutsourcingRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(PersonalOutsourcingRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), PersonalOutsourcingRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal PersonalOutsourcingRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into PersonalOutsourcing")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, IDNo, Name, NameN, BirthDate, Sex, EduID, HighSchool, HighDepart,")
            strSQL.AppendLine("    SchoolStatus, EmpAttrib, ContractStartDate, ContractQuitDate, EmpDate, GroupID, DeptID,")
            strSQL.AppendLine("    OrganID, WorkSiteID, CommTel, CommAddr, RelName, FeeShareDept, SalaryUnit, Salary,")
            strSQL.AppendLine("    Allowance, EmpSource, OutsourcingComp, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @IDNo, @Name, @NameN, @BirthDate, @Sex, @EduID, @HighSchool, @HighDepart,")
            strSQL.AppendLine("    @SchoolStatus, @EmpAttrib, @ContractStartDate, @ContractQuitDate, @EmpDate, @GroupID, @DeptID,")
            strSQL.AppendLine("    @OrganID, @WorkSiteID, @CommTel, @CommAddr, @RelName, @FeeShareDept, @SalaryUnit, @Salary,")
            strSQL.AppendLine("    @Allowance, @EmpSource, @OutsourcingComp, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PersonalOutsourcingRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, PersonalOutsourcingRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, PersonalOutsourcingRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@Name", DbType.String, PersonalOutsourcingRow.Name.Value)
            db.AddInParameter(dbcmd, "@NameN", DbType.String, PersonalOutsourcingRow.NameN.Value)
            db.AddInParameter(dbcmd, "@BirthDate", DbType.Date, IIf(IsDateTimeNull(PersonalOutsourcingRow.BirthDate.Value), Convert.ToDateTime("1900/1/1"), PersonalOutsourcingRow.BirthDate.Value))
            db.AddInParameter(dbcmd, "@Sex", DbType.String, PersonalOutsourcingRow.Sex.Value)
            db.AddInParameter(dbcmd, "@EduID", DbType.String, PersonalOutsourcingRow.EduID.Value)
            db.AddInParameter(dbcmd, "@HighSchool", DbType.String, PersonalOutsourcingRow.HighSchool.Value)
            db.AddInParameter(dbcmd, "@HighDepart", DbType.String, PersonalOutsourcingRow.HighDepart.Value)
            db.AddInParameter(dbcmd, "@SchoolStatus", DbType.String, PersonalOutsourcingRow.SchoolStatus.Value)
            db.AddInParameter(dbcmd, "@EmpAttrib", DbType.String, PersonalOutsourcingRow.EmpAttrib.Value)
            db.AddInParameter(dbcmd, "@ContractStartDate", DbType.Date, IIf(IsDateTimeNull(PersonalOutsourcingRow.ContractStartDate.Value), Convert.ToDateTime("1900/1/1"), PersonalOutsourcingRow.ContractStartDate.Value))
            db.AddInParameter(dbcmd, "@ContractQuitDate", DbType.Date, IIf(IsDateTimeNull(PersonalOutsourcingRow.ContractQuitDate.Value), Convert.ToDateTime("1900/1/1"), PersonalOutsourcingRow.ContractQuitDate.Value))
            db.AddInParameter(dbcmd, "@EmpDate", DbType.Date, IIf(IsDateTimeNull(PersonalOutsourcingRow.EmpDate.Value), Convert.ToDateTime("1900/1/1"), PersonalOutsourcingRow.EmpDate.Value))
            db.AddInParameter(dbcmd, "@GroupID", DbType.String, PersonalOutsourcingRow.GroupID.Value)
            db.AddInParameter(dbcmd, "@DeptID", DbType.String, PersonalOutsourcingRow.DeptID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, PersonalOutsourcingRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, PersonalOutsourcingRow.WorkSiteID.Value)
            db.AddInParameter(dbcmd, "@CommTel", DbType.String, PersonalOutsourcingRow.CommTel.Value)
            db.AddInParameter(dbcmd, "@CommAddr", DbType.String, PersonalOutsourcingRow.CommAddr.Value)
            db.AddInParameter(dbcmd, "@RelName", DbType.String, PersonalOutsourcingRow.RelName.Value)
            db.AddInParameter(dbcmd, "@FeeShareDept", DbType.String, PersonalOutsourcingRow.FeeShareDept.Value)
            db.AddInParameter(dbcmd, "@SalaryUnit", DbType.String, PersonalOutsourcingRow.SalaryUnit.Value)
            db.AddInParameter(dbcmd, "@Salary", DbType.String, PersonalOutsourcingRow.Salary.Value)
            db.AddInParameter(dbcmd, "@Allowance", DbType.String, PersonalOutsourcingRow.Allowance.Value)
            db.AddInParameter(dbcmd, "@EmpSource", DbType.String, PersonalOutsourcingRow.EmpSource.Value)
            db.AddInParameter(dbcmd, "@OutsourcingComp", DbType.String, PersonalOutsourcingRow.OutsourcingComp.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, PersonalOutsourcingRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, PersonalOutsourcingRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(PersonalOutsourcingRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), PersonalOutsourcingRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal PersonalOutsourcingRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into PersonalOutsourcing")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, IDNo, Name, NameN, BirthDate, Sex, EduID, HighSchool, HighDepart,")
            strSQL.AppendLine("    SchoolStatus, EmpAttrib, ContractStartDate, ContractQuitDate, EmpDate, GroupID, DeptID,")
            strSQL.AppendLine("    OrganID, WorkSiteID, CommTel, CommAddr, RelName, FeeShareDept, SalaryUnit, Salary,")
            strSQL.AppendLine("    Allowance, EmpSource, OutsourcingComp, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @IDNo, @Name, @NameN, @BirthDate, @Sex, @EduID, @HighSchool, @HighDepart,")
            strSQL.AppendLine("    @SchoolStatus, @EmpAttrib, @ContractStartDate, @ContractQuitDate, @EmpDate, @GroupID, @DeptID,")
            strSQL.AppendLine("    @OrganID, @WorkSiteID, @CommTel, @CommAddr, @RelName, @FeeShareDept, @SalaryUnit, @Salary,")
            strSQL.AppendLine("    @Allowance, @EmpSource, @OutsourcingComp, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In PersonalOutsourcingRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                        db.AddInParameter(dbcmd, "@Name", DbType.String, r.Name.Value)
                        db.AddInParameter(dbcmd, "@NameN", DbType.String, r.NameN.Value)
                        db.AddInParameter(dbcmd, "@BirthDate", DbType.Date, IIf(IsDateTimeNull(r.BirthDate.Value), Convert.ToDateTime("1900/1/1"), r.BirthDate.Value))
                        db.AddInParameter(dbcmd, "@Sex", DbType.String, r.Sex.Value)
                        db.AddInParameter(dbcmd, "@EduID", DbType.String, r.EduID.Value)
                        db.AddInParameter(dbcmd, "@HighSchool", DbType.String, r.HighSchool.Value)
                        db.AddInParameter(dbcmd, "@HighDepart", DbType.String, r.HighDepart.Value)
                        db.AddInParameter(dbcmd, "@SchoolStatus", DbType.String, r.SchoolStatus.Value)
                        db.AddInParameter(dbcmd, "@EmpAttrib", DbType.String, r.EmpAttrib.Value)
                        db.AddInParameter(dbcmd, "@ContractStartDate", DbType.Date, IIf(IsDateTimeNull(r.ContractStartDate.Value), Convert.ToDateTime("1900/1/1"), r.ContractStartDate.Value))
                        db.AddInParameter(dbcmd, "@ContractQuitDate", DbType.Date, IIf(IsDateTimeNull(r.ContractQuitDate.Value), Convert.ToDateTime("1900/1/1"), r.ContractQuitDate.Value))
                        db.AddInParameter(dbcmd, "@EmpDate", DbType.Date, IIf(IsDateTimeNull(r.EmpDate.Value), Convert.ToDateTime("1900/1/1"), r.EmpDate.Value))
                        db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                        db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                        db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                        db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, r.WorkSiteID.Value)
                        db.AddInParameter(dbcmd, "@CommTel", DbType.String, r.CommTel.Value)
                        db.AddInParameter(dbcmd, "@CommAddr", DbType.String, r.CommAddr.Value)
                        db.AddInParameter(dbcmd, "@RelName", DbType.String, r.RelName.Value)
                        db.AddInParameter(dbcmd, "@FeeShareDept", DbType.String, r.FeeShareDept.Value)
                        db.AddInParameter(dbcmd, "@SalaryUnit", DbType.String, r.SalaryUnit.Value)
                        db.AddInParameter(dbcmd, "@Salary", DbType.String, r.Salary.Value)
                        db.AddInParameter(dbcmd, "@Allowance", DbType.String, r.Allowance.Value)
                        db.AddInParameter(dbcmd, "@EmpSource", DbType.String, r.EmpSource.Value)
                        db.AddInParameter(dbcmd, "@OutsourcingComp", DbType.String, r.OutsourcingComp.Value)
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

        Public Function Insert(ByVal PersonalOutsourcingRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into PersonalOutsourcing")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, IDNo, Name, NameN, BirthDate, Sex, EduID, HighSchool, HighDepart,")
            strSQL.AppendLine("    SchoolStatus, EmpAttrib, ContractStartDate, ContractQuitDate, EmpDate, GroupID, DeptID,")
            strSQL.AppendLine("    OrganID, WorkSiteID, CommTel, CommAddr, RelName, FeeShareDept, SalaryUnit, Salary,")
            strSQL.AppendLine("    Allowance, EmpSource, OutsourcingComp, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @IDNo, @Name, @NameN, @BirthDate, @Sex, @EduID, @HighSchool, @HighDepart,")
            strSQL.AppendLine("    @SchoolStatus, @EmpAttrib, @ContractStartDate, @ContractQuitDate, @EmpDate, @GroupID, @DeptID,")
            strSQL.AppendLine("    @OrganID, @WorkSiteID, @CommTel, @CommAddr, @RelName, @FeeShareDept, @SalaryUnit, @Salary,")
            strSQL.AppendLine("    @Allowance, @EmpSource, @OutsourcingComp, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In PersonalOutsourcingRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                db.AddInParameter(dbcmd, "@Name", DbType.String, r.Name.Value)
                db.AddInParameter(dbcmd, "@NameN", DbType.String, r.NameN.Value)
                db.AddInParameter(dbcmd, "@BirthDate", DbType.Date, IIf(IsDateTimeNull(r.BirthDate.Value), Convert.ToDateTime("1900/1/1"), r.BirthDate.Value))
                db.AddInParameter(dbcmd, "@Sex", DbType.String, r.Sex.Value)
                db.AddInParameter(dbcmd, "@EduID", DbType.String, r.EduID.Value)
                db.AddInParameter(dbcmd, "@HighSchool", DbType.String, r.HighSchool.Value)
                db.AddInParameter(dbcmd, "@HighDepart", DbType.String, r.HighDepart.Value)
                db.AddInParameter(dbcmd, "@SchoolStatus", DbType.String, r.SchoolStatus.Value)
                db.AddInParameter(dbcmd, "@EmpAttrib", DbType.String, r.EmpAttrib.Value)
                db.AddInParameter(dbcmd, "@ContractStartDate", DbType.Date, IIf(IsDateTimeNull(r.ContractStartDate.Value), Convert.ToDateTime("1900/1/1"), r.ContractStartDate.Value))
                db.AddInParameter(dbcmd, "@ContractQuitDate", DbType.Date, IIf(IsDateTimeNull(r.ContractQuitDate.Value), Convert.ToDateTime("1900/1/1"), r.ContractQuitDate.Value))
                db.AddInParameter(dbcmd, "@EmpDate", DbType.Date, IIf(IsDateTimeNull(r.EmpDate.Value), Convert.ToDateTime("1900/1/1"), r.EmpDate.Value))
                db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, r.WorkSiteID.Value)
                db.AddInParameter(dbcmd, "@CommTel", DbType.String, r.CommTel.Value)
                db.AddInParameter(dbcmd, "@CommAddr", DbType.String, r.CommAddr.Value)
                db.AddInParameter(dbcmd, "@RelName", DbType.String, r.RelName.Value)
                db.AddInParameter(dbcmd, "@FeeShareDept", DbType.String, r.FeeShareDept.Value)
                db.AddInParameter(dbcmd, "@SalaryUnit", DbType.String, r.SalaryUnit.Value)
                db.AddInParameter(dbcmd, "@Salary", DbType.String, r.Salary.Value)
                db.AddInParameter(dbcmd, "@Allowance", DbType.String, r.Allowance.Value)
                db.AddInParameter(dbcmd, "@EmpSource", DbType.String, r.EmpSource.Value)
                db.AddInParameter(dbcmd, "@OutsourcingComp", DbType.String, r.OutsourcingComp.Value)
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

