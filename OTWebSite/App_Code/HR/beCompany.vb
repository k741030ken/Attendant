'****************************************************************
' Table:Company
' Created Date: 2015.12.02
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beCompany
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "CompName", "CompNameCN", "CompEngName", "CompChnName", "CompChnNameCN", "FeeShareFlag", "GroupID", "CheckInFile", "Calendar" _
                                    , "Payroll", "PayrollMaintain", "SPHSC1GrpFlag", "Probation", "InValidFlag", "NotShowFlag", "HRISFlag", "Address", "AddressCN", "EngAddress", "NotQueryFlag" _
                                    , "RankIDMapFlag", "NotShowWorkType", "NotShowRankID", "EmpSource", "CNFlag", "RankIDMapValidDate", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(String), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "CompID" }

        Public ReadOnly Property Rows() As beCompany.Rows 
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
        Public Sub Transfer2Row(CompanyTable As DataTable)
            For Each dr As DataRow In CompanyTable.Rows
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
                dr(m_Rows(i).CompName.FieldName) = m_Rows(i).CompName.Value
                dr(m_Rows(i).CompNameCN.FieldName) = m_Rows(i).CompNameCN.Value
                dr(m_Rows(i).CompEngName.FieldName) = m_Rows(i).CompEngName.Value
                dr(m_Rows(i).CompChnName.FieldName) = m_Rows(i).CompChnName.Value
                dr(m_Rows(i).CompChnNameCN.FieldName) = m_Rows(i).CompChnNameCN.Value
                dr(m_Rows(i).FeeShareFlag.FieldName) = m_Rows(i).FeeShareFlag.Value
                dr(m_Rows(i).GroupID.FieldName) = m_Rows(i).GroupID.Value
                dr(m_Rows(i).CheckInFile.FieldName) = m_Rows(i).CheckInFile.Value
                dr(m_Rows(i).Calendar.FieldName) = m_Rows(i).Calendar.Value
                dr(m_Rows(i).Payroll.FieldName) = m_Rows(i).Payroll.Value
                dr(m_Rows(i).PayrollMaintain.FieldName) = m_Rows(i).PayrollMaintain.Value
                dr(m_Rows(i).SPHSC1GrpFlag.FieldName) = m_Rows(i).SPHSC1GrpFlag.Value
                dr(m_Rows(i).Probation.FieldName) = m_Rows(i).Probation.Value
                dr(m_Rows(i).InValidFlag.FieldName) = m_Rows(i).InValidFlag.Value
                dr(m_Rows(i).NotShowFlag.FieldName) = m_Rows(i).NotShowFlag.Value
                dr(m_Rows(i).HRISFlag.FieldName) = m_Rows(i).HRISFlag.Value
                dr(m_Rows(i).Address.FieldName) = m_Rows(i).Address.Value
                dr(m_Rows(i).AddressCN.FieldName) = m_Rows(i).AddressCN.Value
                dr(m_Rows(i).EngAddress.FieldName) = m_Rows(i).EngAddress.Value
                dr(m_Rows(i).NotQueryFlag.FieldName) = m_Rows(i).NotQueryFlag.Value
                dr(m_Rows(i).RankIDMapFlag.FieldName) = m_Rows(i).RankIDMapFlag.Value
                dr(m_Rows(i).NotShowWorkType.FieldName) = m_Rows(i).NotShowWorkType.Value
                dr(m_Rows(i).NotShowRankID.FieldName) = m_Rows(i).NotShowRankID.Value
                dr(m_Rows(i).EmpSource.FieldName) = m_Rows(i).EmpSource.Value
                dr(m_Rows(i).CNFlag.FieldName) = m_Rows(i).CNFlag.Value
                dr(m_Rows(i).RankIDMapValidDate.FieldName) = m_Rows(i).RankIDMapValidDate.Value
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

        Public Sub Add(CompanyRow As Row)
            m_Rows.Add(CompanyRow)
        End Sub

        Public Sub Remove(CompanyRow As Row)
            If m_Rows.IndexOf(CompanyRow) >= 0 Then
                m_Rows.Remove(CompanyRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_CompName As Field(Of String) = new Field(Of String)("CompName", true)
        Private FI_CompNameCN As Field(Of String) = new Field(Of String)("CompNameCN", true)
        Private FI_CompEngName As Field(Of String) = new Field(Of String)("CompEngName", true)
        Private FI_CompChnName As Field(Of String) = new Field(Of String)("CompChnName", true)
        Private FI_CompChnNameCN As Field(Of String) = new Field(Of String)("CompChnNameCN", true)
        Private FI_FeeShareFlag As Field(Of String) = new Field(Of String)("FeeShareFlag", true)
        Private FI_GroupID As Field(Of String) = new Field(Of String)("GroupID", true)
        Private FI_CheckInFile As Field(Of String) = new Field(Of String)("CheckInFile", true)
        Private FI_Calendar As Field(Of String) = new Field(Of String)("Calendar", true)
        Private FI_Payroll As Field(Of String) = new Field(Of String)("Payroll", true)
        Private FI_PayrollMaintain As Field(Of String) = new Field(Of String)("PayrollMaintain", true)
        Private FI_SPHSC1GrpFlag As Field(Of String) = new Field(Of String)("SPHSC1GrpFlag", true)
        Private FI_Probation As Field(Of String) = new Field(Of String)("Probation", true)
        Private FI_InValidFlag As Field(Of String) = new Field(Of String)("InValidFlag", true)
        Private FI_NotShowFlag As Field(Of String) = new Field(Of String)("NotShowFlag", true)
        Private FI_HRISFlag As Field(Of String) = new Field(Of String)("HRISFlag", true)
        Private FI_Address As Field(Of String) = new Field(Of String)("Address", true)
        Private FI_AddressCN As Field(Of String) = new Field(Of String)("AddressCN", true)
        Private FI_EngAddress As Field(Of String) = new Field(Of String)("EngAddress", true)
        Private FI_NotQueryFlag As Field(Of String) = new Field(Of String)("NotQueryFlag", true)
        Private FI_RankIDMapFlag As Field(Of String) = new Field(Of String)("RankIDMapFlag", true)
        Private FI_NotShowWorkType As Field(Of String) = new Field(Of String)("NotShowWorkType", true)
        Private FI_NotShowRankID As Field(Of String) = new Field(Of String)("NotShowRankID", true)
        Private FI_EmpSource As Field(Of String) = new Field(Of String)("EmpSource", true)
        Private FI_CNFlag As Field(Of String) = new Field(Of String)("CNFlag", true)
        Private FI_RankIDMapValidDate As Field(Of Date) = new Field(Of Date)("RankIDMapValidDate", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "CompID", "CompName", "CompNameCN", "CompEngName", "CompChnName", "CompChnNameCN", "FeeShareFlag", "GroupID", "CheckInFile", "Calendar" _
                                    , "Payroll", "PayrollMaintain", "SPHSC1GrpFlag", "Probation", "InValidFlag", "NotShowFlag", "HRISFlag", "Address", "AddressCN", "EngAddress", "NotQueryFlag" _
                                    , "RankIDMapFlag", "NotShowWorkType", "NotShowRankID", "EmpSource", "CNFlag", "RankIDMapValidDate", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "CompID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CompID"
                    Return FI_CompID.Value
                Case "CompName"
                    Return FI_CompName.Value
                Case "CompNameCN"
                    Return FI_CompNameCN.Value
                Case "CompEngName"
                    Return FI_CompEngName.Value
                Case "CompChnName"
                    Return FI_CompChnName.Value
                Case "CompChnNameCN"
                    Return FI_CompChnNameCN.Value
                Case "FeeShareFlag"
                    Return FI_FeeShareFlag.Value
                Case "GroupID"
                    Return FI_GroupID.Value
                Case "CheckInFile"
                    Return FI_CheckInFile.Value
                Case "Calendar"
                    Return FI_Calendar.Value
                Case "Payroll"
                    Return FI_Payroll.Value
                Case "PayrollMaintain"
                    Return FI_PayrollMaintain.Value
                Case "SPHSC1GrpFlag"
                    Return FI_SPHSC1GrpFlag.Value
                Case "Probation"
                    Return FI_Probation.Value
                Case "InValidFlag"
                    Return FI_InValidFlag.Value
                Case "NotShowFlag"
                    Return FI_NotShowFlag.Value
                Case "HRISFlag"
                    Return FI_HRISFlag.Value
                Case "Address"
                    Return FI_Address.Value
                Case "AddressCN"
                    Return FI_AddressCN.Value
                Case "EngAddress"
                    Return FI_EngAddress.Value
                Case "NotQueryFlag"
                    Return FI_NotQueryFlag.Value
                Case "RankIDMapFlag"
                    Return FI_RankIDMapFlag.Value
                Case "NotShowWorkType"
                    Return FI_NotShowWorkType.Value
                Case "NotShowRankID"
                    Return FI_NotShowRankID.Value
                Case "EmpSource"
                    Return FI_EmpSource.Value
                Case "CNFlag"
                    Return FI_CNFlag.Value
                Case "RankIDMapValidDate"
                    Return FI_RankIDMapValidDate.Value
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
                Case "CompName"
                    FI_CompName.SetValue(value)
                Case "CompNameCN"
                    FI_CompNameCN.SetValue(value)
                Case "CompEngName"
                    FI_CompEngName.SetValue(value)
                Case "CompChnName"
                    FI_CompChnName.SetValue(value)
                Case "CompChnNameCN"
                    FI_CompChnNameCN.SetValue(value)
                Case "FeeShareFlag"
                    FI_FeeShareFlag.SetValue(value)
                Case "GroupID"
                    FI_GroupID.SetValue(value)
                Case "CheckInFile"
                    FI_CheckInFile.SetValue(value)
                Case "Calendar"
                    FI_Calendar.SetValue(value)
                Case "Payroll"
                    FI_Payroll.SetValue(value)
                Case "PayrollMaintain"
                    FI_PayrollMaintain.SetValue(value)
                Case "SPHSC1GrpFlag"
                    FI_SPHSC1GrpFlag.SetValue(value)
                Case "Probation"
                    FI_Probation.SetValue(value)
                Case "InValidFlag"
                    FI_InValidFlag.SetValue(value)
                Case "NotShowFlag"
                    FI_NotShowFlag.SetValue(value)
                Case "HRISFlag"
                    FI_HRISFlag.SetValue(value)
                Case "Address"
                    FI_Address.SetValue(value)
                Case "AddressCN"
                    FI_AddressCN.SetValue(value)
                Case "EngAddress"
                    FI_EngAddress.SetValue(value)
                Case "NotQueryFlag"
                    FI_NotQueryFlag.SetValue(value)
                Case "RankIDMapFlag"
                    FI_RankIDMapFlag.SetValue(value)
                Case "NotShowWorkType"
                    FI_NotShowWorkType.SetValue(value)
                Case "NotShowRankID"
                    FI_NotShowRankID.SetValue(value)
                Case "EmpSource"
                    FI_EmpSource.SetValue(value)
                Case "CNFlag"
                    FI_CNFlag.SetValue(value)
                Case "RankIDMapValidDate"
                    FI_RankIDMapValidDate.SetValue(value)
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
                Case "CompName"
                    return FI_CompName.Updated
                Case "CompNameCN"
                    return FI_CompNameCN.Updated
                Case "CompEngName"
                    return FI_CompEngName.Updated
                Case "CompChnName"
                    return FI_CompChnName.Updated
                Case "CompChnNameCN"
                    return FI_CompChnNameCN.Updated
                Case "FeeShareFlag"
                    return FI_FeeShareFlag.Updated
                Case "GroupID"
                    return FI_GroupID.Updated
                Case "CheckInFile"
                    return FI_CheckInFile.Updated
                Case "Calendar"
                    return FI_Calendar.Updated
                Case "Payroll"
                    return FI_Payroll.Updated
                Case "PayrollMaintain"
                    return FI_PayrollMaintain.Updated
                Case "SPHSC1GrpFlag"
                    return FI_SPHSC1GrpFlag.Updated
                Case "Probation"
                    return FI_Probation.Updated
                Case "InValidFlag"
                    return FI_InValidFlag.Updated
                Case "NotShowFlag"
                    return FI_NotShowFlag.Updated
                Case "HRISFlag"
                    return FI_HRISFlag.Updated
                Case "Address"
                    return FI_Address.Updated
                Case "AddressCN"
                    return FI_AddressCN.Updated
                Case "EngAddress"
                    return FI_EngAddress.Updated
                Case "NotQueryFlag"
                    return FI_NotQueryFlag.Updated
                Case "RankIDMapFlag"
                    return FI_RankIDMapFlag.Updated
                Case "NotShowWorkType"
                    return FI_NotShowWorkType.Updated
                Case "NotShowRankID"
                    return FI_NotShowRankID.Updated
                Case "EmpSource"
                    return FI_EmpSource.Updated
                Case "CNFlag"
                    return FI_CNFlag.Updated
                Case "RankIDMapValidDate"
                    return FI_RankIDMapValidDate.Updated
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
                Case "CompName"
                    return FI_CompName.CreateUpdateSQL
                Case "CompNameCN"
                    return FI_CompNameCN.CreateUpdateSQL
                Case "CompEngName"
                    return FI_CompEngName.CreateUpdateSQL
                Case "CompChnName"
                    return FI_CompChnName.CreateUpdateSQL
                Case "CompChnNameCN"
                    return FI_CompChnNameCN.CreateUpdateSQL
                Case "FeeShareFlag"
                    return FI_FeeShareFlag.CreateUpdateSQL
                Case "GroupID"
                    return FI_GroupID.CreateUpdateSQL
                Case "CheckInFile"
                    return FI_CheckInFile.CreateUpdateSQL
                Case "Calendar"
                    return FI_Calendar.CreateUpdateSQL
                Case "Payroll"
                    return FI_Payroll.CreateUpdateSQL
                Case "PayrollMaintain"
                    return FI_PayrollMaintain.CreateUpdateSQL
                Case "SPHSC1GrpFlag"
                    return FI_SPHSC1GrpFlag.CreateUpdateSQL
                Case "Probation"
                    return FI_Probation.CreateUpdateSQL
                Case "InValidFlag"
                    return FI_InValidFlag.CreateUpdateSQL
                Case "NotShowFlag"
                    return FI_NotShowFlag.CreateUpdateSQL
                Case "HRISFlag"
                    return FI_HRISFlag.CreateUpdateSQL
                Case "Address"
                    return FI_Address.CreateUpdateSQL
                Case "AddressCN"
                    return FI_AddressCN.CreateUpdateSQL
                Case "EngAddress"
                    return FI_EngAddress.CreateUpdateSQL
                Case "NotQueryFlag"
                    return FI_NotQueryFlag.CreateUpdateSQL
                Case "RankIDMapFlag"
                    return FI_RankIDMapFlag.CreateUpdateSQL
                Case "NotShowWorkType"
                    return FI_NotShowWorkType.CreateUpdateSQL
                Case "NotShowRankID"
                    return FI_NotShowRankID.CreateUpdateSQL
                Case "EmpSource"
                    return FI_EmpSource.CreateUpdateSQL
                Case "CNFlag"
                    return FI_CNFlag.CreateUpdateSQL
                Case "RankIDMapValidDate"
                    return FI_RankIDMapValidDate.CreateUpdateSQL
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
            FI_CompName.SetInitValue(" ")
            FI_CompNameCN.SetInitValue("")
            FI_CompEngName.SetInitValue(" ")
            FI_CompChnName.SetInitValue(" ")
            FI_CompChnNameCN.SetInitValue("")
            FI_FeeShareFlag.SetInitValue("0")
            FI_GroupID.SetInitValue("")
            FI_CheckInFile.SetInitValue("")
            FI_Calendar.SetInitValue("")
            FI_Payroll.SetInitValue("")
            FI_PayrollMaintain.SetInitValue("")
            FI_SPHSC1GrpFlag.SetInitValue("0")
            FI_Probation.SetInitValue("")
            FI_InValidFlag.SetInitValue("0")
            FI_NotShowFlag.SetInitValue("0")
            FI_HRISFlag.SetInitValue("0")
            FI_Address.SetInitValue("")
            FI_AddressCN.SetInitValue("")
            FI_EngAddress.SetInitValue("")
            FI_NotQueryFlag.SetInitValue("0")
            FI_RankIDMapFlag.SetInitValue("0")
            FI_NotShowWorkType.SetInitValue("0")
            FI_NotShowRankID.SetInitValue("0")
            FI_EmpSource.SetInitValue("")
            FI_CNFlag.SetInitValue("")
            FI_RankIDMapValidDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CompID.SetInitValue(dr("CompID"))
            FI_CompName.SetInitValue(dr("CompName"))
            FI_CompNameCN.SetInitValue(dr("CompNameCN"))
            FI_CompEngName.SetInitValue(dr("CompEngName"))
            FI_CompChnName.SetInitValue(dr("CompChnName"))
            FI_CompChnNameCN.SetInitValue(dr("CompChnNameCN"))
            FI_FeeShareFlag.SetInitValue(dr("FeeShareFlag"))
            FI_GroupID.SetInitValue(dr("GroupID"))
            FI_CheckInFile.SetInitValue(dr("CheckInFile"))
            FI_Calendar.SetInitValue(dr("Calendar"))
            FI_Payroll.SetInitValue(dr("Payroll"))
            FI_PayrollMaintain.SetInitValue(dr("PayrollMaintain"))
            FI_SPHSC1GrpFlag.SetInitValue(dr("SPHSC1GrpFlag"))
            FI_Probation.SetInitValue(dr("Probation"))
            FI_InValidFlag.SetInitValue(dr("InValidFlag"))
            FI_NotShowFlag.SetInitValue(dr("NotShowFlag"))
            FI_HRISFlag.SetInitValue(dr("HRISFlag"))
            FI_Address.SetInitValue(dr("Address"))
            FI_AddressCN.SetInitValue(dr("AddressCN"))
            FI_EngAddress.SetInitValue(dr("EngAddress"))
            FI_NotQueryFlag.SetInitValue(dr("NotQueryFlag"))
            FI_RankIDMapFlag.SetInitValue(dr("RankIDMapFlag"))
            FI_NotShowWorkType.SetInitValue(dr("NotShowWorkType"))
            FI_NotShowRankID.SetInitValue(dr("NotShowRankID"))
            FI_EmpSource.SetInitValue(dr("EmpSource"))
            FI_CNFlag.SetInitValue(dr("CNFlag"))
            FI_RankIDMapValidDate.SetInitValue(dr("RankIDMapValidDate"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CompID.Updated = False
            FI_CompName.Updated = False
            FI_CompNameCN.Updated = False
            FI_CompEngName.Updated = False
            FI_CompChnName.Updated = False
            FI_CompChnNameCN.Updated = False
            FI_FeeShareFlag.Updated = False
            FI_GroupID.Updated = False
            FI_CheckInFile.Updated = False
            FI_Calendar.Updated = False
            FI_Payroll.Updated = False
            FI_PayrollMaintain.Updated = False
            FI_SPHSC1GrpFlag.Updated = False
            FI_Probation.Updated = False
            FI_InValidFlag.Updated = False
            FI_NotShowFlag.Updated = False
            FI_HRISFlag.Updated = False
            FI_Address.Updated = False
            FI_AddressCN.Updated = False
            FI_EngAddress.Updated = False
            FI_NotQueryFlag.Updated = False
            FI_RankIDMapFlag.Updated = False
            FI_NotShowWorkType.Updated = False
            FI_NotShowRankID.Updated = False
            FI_EmpSource.Updated = False
            FI_CNFlag.Updated = False
            FI_RankIDMapValidDate.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
        End Sub

        Public ReadOnly Property CompID As Field(Of String) 
            Get
                Return FI_CompID
            End Get
        End Property

        Public ReadOnly Property CompName As Field(Of String) 
            Get
                Return FI_CompName
            End Get
        End Property

        Public ReadOnly Property CompNameCN As Field(Of String) 
            Get
                Return FI_CompNameCN
            End Get
        End Property

        Public ReadOnly Property CompEngName As Field(Of String) 
            Get
                Return FI_CompEngName
            End Get
        End Property

        Public ReadOnly Property CompChnName As Field(Of String) 
            Get
                Return FI_CompChnName
            End Get
        End Property

        Public ReadOnly Property CompChnNameCN As Field(Of String) 
            Get
                Return FI_CompChnNameCN
            End Get
        End Property

        Public ReadOnly Property FeeShareFlag As Field(Of String) 
            Get
                Return FI_FeeShareFlag
            End Get
        End Property

        Public ReadOnly Property GroupID As Field(Of String) 
            Get
                Return FI_GroupID
            End Get
        End Property

        Public ReadOnly Property CheckInFile As Field(Of String) 
            Get
                Return FI_CheckInFile
            End Get
        End Property

        Public ReadOnly Property Calendar As Field(Of String) 
            Get
                Return FI_Calendar
            End Get
        End Property

        Public ReadOnly Property Payroll As Field(Of String) 
            Get
                Return FI_Payroll
            End Get
        End Property

        Public ReadOnly Property PayrollMaintain As Field(Of String) 
            Get
                Return FI_PayrollMaintain
            End Get
        End Property

        Public ReadOnly Property SPHSC1GrpFlag As Field(Of String) 
            Get
                Return FI_SPHSC1GrpFlag
            End Get
        End Property

        Public ReadOnly Property Probation As Field(Of String) 
            Get
                Return FI_Probation
            End Get
        End Property

        Public ReadOnly Property InValidFlag As Field(Of String) 
            Get
                Return FI_InValidFlag
            End Get
        End Property

        Public ReadOnly Property NotShowFlag As Field(Of String) 
            Get
                Return FI_NotShowFlag
            End Get
        End Property

        Public ReadOnly Property HRISFlag As Field(Of String) 
            Get
                Return FI_HRISFlag
            End Get
        End Property

        Public ReadOnly Property Address As Field(Of String) 
            Get
                Return FI_Address
            End Get
        End Property

        Public ReadOnly Property AddressCN As Field(Of String) 
            Get
                Return FI_AddressCN
            End Get
        End Property

        Public ReadOnly Property EngAddress As Field(Of String) 
            Get
                Return FI_EngAddress
            End Get
        End Property

        Public ReadOnly Property NotQueryFlag As Field(Of String) 
            Get
                Return FI_NotQueryFlag
            End Get
        End Property

        Public ReadOnly Property RankIDMapFlag As Field(Of String) 
            Get
                Return FI_RankIDMapFlag
            End Get
        End Property

        Public ReadOnly Property NotShowWorkType As Field(Of String) 
            Get
                Return FI_NotShowWorkType
            End Get
        End Property

        Public ReadOnly Property NotShowRankID As Field(Of String) 
            Get
                Return FI_NotShowRankID
            End Get
        End Property

        Public ReadOnly Property EmpSource As Field(Of String) 
            Get
                Return FI_EmpSource
            End Get
        End Property

        Public ReadOnly Property CNFlag As Field(Of String) 
            Get
                Return FI_CNFlag
            End Get
        End Property

        Public ReadOnly Property RankIDMapValidDate As Field(Of Date) 
            Get
                Return FI_RankIDMapValidDate
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
        Public Function DeleteRowByPrimaryKey(ByVal CompanyRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From Company")
            strSQL.AppendLine("Where CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, CompanyRow.CompID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal CompanyRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From Company")
            strSQL.AppendLine("Where CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, CompanyRow.CompID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal CompanyRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From Company")
            strSQL.AppendLine("Where CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In CompanyRow
                        dbcmd.Parameters.Clear()
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

        Public Function DeleteRowByPrimaryKey(ByVal CompanyRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From Company")
            strSQL.AppendLine("Where CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In CompanyRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal CompanyRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Company")
            strSQL.AppendLine("Where CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, CompanyRow.CompID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(CompanyRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Company")
            strSQL.AppendLine("Where CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, CompanyRow.CompID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal CompanyRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update Company Set")
            For i As Integer = 0 To CompanyRow.FieldNames.Length - 1
                If Not CompanyRow.IsIdentityField(CompanyRow.FieldNames(i)) AndAlso CompanyRow.IsUpdated(CompanyRow.FieldNames(i)) AndAlso CompanyRow.CreateUpdateSQL(CompanyRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, CompanyRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If CompanyRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, CompanyRow.CompID.Value)
            If CompanyRow.CompName.Updated Then db.AddInParameter(dbcmd, "@CompName", DbType.String, CompanyRow.CompName.Value)
            If CompanyRow.CompNameCN.Updated Then db.AddInParameter(dbcmd, "@CompNameCN", DbType.String, CompanyRow.CompNameCN.Value)
            If CompanyRow.CompEngName.Updated Then db.AddInParameter(dbcmd, "@CompEngName", DbType.String, CompanyRow.CompEngName.Value)
            If CompanyRow.CompChnName.Updated Then db.AddInParameter(dbcmd, "@CompChnName", DbType.String, CompanyRow.CompChnName.Value)
            If CompanyRow.CompChnNameCN.Updated Then db.AddInParameter(dbcmd, "@CompChnNameCN", DbType.String, CompanyRow.CompChnNameCN.Value)
            If CompanyRow.FeeShareFlag.Updated Then db.AddInParameter(dbcmd, "@FeeShareFlag", DbType.String, CompanyRow.FeeShareFlag.Value)
            If CompanyRow.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, CompanyRow.GroupID.Value)
            If CompanyRow.CheckInFile.Updated Then db.AddInParameter(dbcmd, "@CheckInFile", DbType.String, CompanyRow.CheckInFile.Value)
            If CompanyRow.Calendar.Updated Then db.AddInParameter(dbcmd, "@Calendar", DbType.String, CompanyRow.Calendar.Value)
            If CompanyRow.Payroll.Updated Then db.AddInParameter(dbcmd, "@Payroll", DbType.String, CompanyRow.Payroll.Value)
            If CompanyRow.PayrollMaintain.Updated Then db.AddInParameter(dbcmd, "@PayrollMaintain", DbType.String, CompanyRow.PayrollMaintain.Value)
            If CompanyRow.SPHSC1GrpFlag.Updated Then db.AddInParameter(dbcmd, "@SPHSC1GrpFlag", DbType.String, CompanyRow.SPHSC1GrpFlag.Value)
            If CompanyRow.Probation.Updated Then db.AddInParameter(dbcmd, "@Probation", DbType.String, CompanyRow.Probation.Value)
            If CompanyRow.InValidFlag.Updated Then db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, CompanyRow.InValidFlag.Value)
            If CompanyRow.NotShowFlag.Updated Then db.AddInParameter(dbcmd, "@NotShowFlag", DbType.String, CompanyRow.NotShowFlag.Value)
            If CompanyRow.HRISFlag.Updated Then db.AddInParameter(dbcmd, "@HRISFlag", DbType.String, CompanyRow.HRISFlag.Value)
            If CompanyRow.Address.Updated Then db.AddInParameter(dbcmd, "@Address", DbType.String, CompanyRow.Address.Value)
            If CompanyRow.AddressCN.Updated Then db.AddInParameter(dbcmd, "@AddressCN", DbType.String, CompanyRow.AddressCN.Value)
            If CompanyRow.EngAddress.Updated Then db.AddInParameter(dbcmd, "@EngAddress", DbType.String, CompanyRow.EngAddress.Value)
            If CompanyRow.NotQueryFlag.Updated Then db.AddInParameter(dbcmd, "@NotQueryFlag", DbType.String, CompanyRow.NotQueryFlag.Value)
            If CompanyRow.RankIDMapFlag.Updated Then db.AddInParameter(dbcmd, "@RankIDMapFlag", DbType.String, CompanyRow.RankIDMapFlag.Value)
            If CompanyRow.NotShowWorkType.Updated Then db.AddInParameter(dbcmd, "@NotShowWorkType", DbType.String, CompanyRow.NotShowWorkType.Value)
            If CompanyRow.NotShowRankID.Updated Then db.AddInParameter(dbcmd, "@NotShowRankID", DbType.String, CompanyRow.NotShowRankID.Value)
            If CompanyRow.EmpSource.Updated Then db.AddInParameter(dbcmd, "@EmpSource", DbType.String, CompanyRow.EmpSource.Value)
            If CompanyRow.CNFlag.Updated Then db.AddInParameter(dbcmd, "@CNFlag", DbType.String, CompanyRow.CNFlag.Value)
            If CompanyRow.RankIDMapValidDate.Updated Then db.AddInParameter(dbcmd, "@RankIDMapValidDate", DbType.Date, IIf(IsDateTimeNull(CompanyRow.RankIDMapValidDate.Value), Convert.ToDateTime("1900/1/1"), CompanyRow.RankIDMapValidDate.Value))
            If CompanyRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, CompanyRow.LastChgComp.Value)
            If CompanyRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, CompanyRow.LastChgID.Value)
            If CompanyRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(CompanyRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), CompanyRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(CompanyRow.LoadFromDataRow, CompanyRow.CompID.OldValue, CompanyRow.CompID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal CompanyRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update Company Set")
            For i As Integer = 0 To CompanyRow.FieldNames.Length - 1
                If Not CompanyRow.IsIdentityField(CompanyRow.FieldNames(i)) AndAlso CompanyRow.IsUpdated(CompanyRow.FieldNames(i)) AndAlso CompanyRow.CreateUpdateSQL(CompanyRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, CompanyRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If CompanyRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, CompanyRow.CompID.Value)
            If CompanyRow.CompName.Updated Then db.AddInParameter(dbcmd, "@CompName", DbType.String, CompanyRow.CompName.Value)
            If CompanyRow.CompNameCN.Updated Then db.AddInParameter(dbcmd, "@CompNameCN", DbType.String, CompanyRow.CompNameCN.Value)
            If CompanyRow.CompEngName.Updated Then db.AddInParameter(dbcmd, "@CompEngName", DbType.String, CompanyRow.CompEngName.Value)
            If CompanyRow.CompChnName.Updated Then db.AddInParameter(dbcmd, "@CompChnName", DbType.String, CompanyRow.CompChnName.Value)
            If CompanyRow.CompChnNameCN.Updated Then db.AddInParameter(dbcmd, "@CompChnNameCN", DbType.String, CompanyRow.CompChnNameCN.Value)
            If CompanyRow.FeeShareFlag.Updated Then db.AddInParameter(dbcmd, "@FeeShareFlag", DbType.String, CompanyRow.FeeShareFlag.Value)
            If CompanyRow.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, CompanyRow.GroupID.Value)
            If CompanyRow.CheckInFile.Updated Then db.AddInParameter(dbcmd, "@CheckInFile", DbType.String, CompanyRow.CheckInFile.Value)
            If CompanyRow.Calendar.Updated Then db.AddInParameter(dbcmd, "@Calendar", DbType.String, CompanyRow.Calendar.Value)
            If CompanyRow.Payroll.Updated Then db.AddInParameter(dbcmd, "@Payroll", DbType.String, CompanyRow.Payroll.Value)
            If CompanyRow.PayrollMaintain.Updated Then db.AddInParameter(dbcmd, "@PayrollMaintain", DbType.String, CompanyRow.PayrollMaintain.Value)
            If CompanyRow.SPHSC1GrpFlag.Updated Then db.AddInParameter(dbcmd, "@SPHSC1GrpFlag", DbType.String, CompanyRow.SPHSC1GrpFlag.Value)
            If CompanyRow.Probation.Updated Then db.AddInParameter(dbcmd, "@Probation", DbType.String, CompanyRow.Probation.Value)
            If CompanyRow.InValidFlag.Updated Then db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, CompanyRow.InValidFlag.Value)
            If CompanyRow.NotShowFlag.Updated Then db.AddInParameter(dbcmd, "@NotShowFlag", DbType.String, CompanyRow.NotShowFlag.Value)
            If CompanyRow.HRISFlag.Updated Then db.AddInParameter(dbcmd, "@HRISFlag", DbType.String, CompanyRow.HRISFlag.Value)
            If CompanyRow.Address.Updated Then db.AddInParameter(dbcmd, "@Address", DbType.String, CompanyRow.Address.Value)
            If CompanyRow.AddressCN.Updated Then db.AddInParameter(dbcmd, "@AddressCN", DbType.String, CompanyRow.AddressCN.Value)
            If CompanyRow.EngAddress.Updated Then db.AddInParameter(dbcmd, "@EngAddress", DbType.String, CompanyRow.EngAddress.Value)
            If CompanyRow.NotQueryFlag.Updated Then db.AddInParameter(dbcmd, "@NotQueryFlag", DbType.String, CompanyRow.NotQueryFlag.Value)
            If CompanyRow.RankIDMapFlag.Updated Then db.AddInParameter(dbcmd, "@RankIDMapFlag", DbType.String, CompanyRow.RankIDMapFlag.Value)
            If CompanyRow.NotShowWorkType.Updated Then db.AddInParameter(dbcmd, "@NotShowWorkType", DbType.String, CompanyRow.NotShowWorkType.Value)
            If CompanyRow.NotShowRankID.Updated Then db.AddInParameter(dbcmd, "@NotShowRankID", DbType.String, CompanyRow.NotShowRankID.Value)
            If CompanyRow.EmpSource.Updated Then db.AddInParameter(dbcmd, "@EmpSource", DbType.String, CompanyRow.EmpSource.Value)
            If CompanyRow.CNFlag.Updated Then db.AddInParameter(dbcmd, "@CNFlag", DbType.String, CompanyRow.CNFlag.Value)
            If CompanyRow.RankIDMapValidDate.Updated Then db.AddInParameter(dbcmd, "@RankIDMapValidDate", DbType.Date, IIf(IsDateTimeNull(CompanyRow.RankIDMapValidDate.Value), Convert.ToDateTime("1900/1/1"), CompanyRow.RankIDMapValidDate.Value))
            If CompanyRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, CompanyRow.LastChgComp.Value)
            If CompanyRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, CompanyRow.LastChgID.Value)
            If CompanyRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(CompanyRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), CompanyRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(CompanyRow.LoadFromDataRow, CompanyRow.CompID.OldValue, CompanyRow.CompID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal CompanyRow As Row()) As Integer
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
                    For Each r As Row In CompanyRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update Company Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where CompID = @PKCompID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.CompName.Updated Then db.AddInParameter(dbcmd, "@CompName", DbType.String, r.CompName.Value)
                        If r.CompNameCN.Updated Then db.AddInParameter(dbcmd, "@CompNameCN", DbType.String, r.CompNameCN.Value)
                        If r.CompEngName.Updated Then db.AddInParameter(dbcmd, "@CompEngName", DbType.String, r.CompEngName.Value)
                        If r.CompChnName.Updated Then db.AddInParameter(dbcmd, "@CompChnName", DbType.String, r.CompChnName.Value)
                        If r.CompChnNameCN.Updated Then db.AddInParameter(dbcmd, "@CompChnNameCN", DbType.String, r.CompChnNameCN.Value)
                        If r.FeeShareFlag.Updated Then db.AddInParameter(dbcmd, "@FeeShareFlag", DbType.String, r.FeeShareFlag.Value)
                        If r.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                        If r.CheckInFile.Updated Then db.AddInParameter(dbcmd, "@CheckInFile", DbType.String, r.CheckInFile.Value)
                        If r.Calendar.Updated Then db.AddInParameter(dbcmd, "@Calendar", DbType.String, r.Calendar.Value)
                        If r.Payroll.Updated Then db.AddInParameter(dbcmd, "@Payroll", DbType.String, r.Payroll.Value)
                        If r.PayrollMaintain.Updated Then db.AddInParameter(dbcmd, "@PayrollMaintain", DbType.String, r.PayrollMaintain.Value)
                        If r.SPHSC1GrpFlag.Updated Then db.AddInParameter(dbcmd, "@SPHSC1GrpFlag", DbType.String, r.SPHSC1GrpFlag.Value)
                        If r.Probation.Updated Then db.AddInParameter(dbcmd, "@Probation", DbType.String, r.Probation.Value)
                        If r.InValidFlag.Updated Then db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, r.InValidFlag.Value)
                        If r.NotShowFlag.Updated Then db.AddInParameter(dbcmd, "@NotShowFlag", DbType.String, r.NotShowFlag.Value)
                        If r.HRISFlag.Updated Then db.AddInParameter(dbcmd, "@HRISFlag", DbType.String, r.HRISFlag.Value)
                        If r.Address.Updated Then db.AddInParameter(dbcmd, "@Address", DbType.String, r.Address.Value)
                        If r.AddressCN.Updated Then db.AddInParameter(dbcmd, "@AddressCN", DbType.String, r.AddressCN.Value)
                        If r.EngAddress.Updated Then db.AddInParameter(dbcmd, "@EngAddress", DbType.String, r.EngAddress.Value)
                        If r.NotQueryFlag.Updated Then db.AddInParameter(dbcmd, "@NotQueryFlag", DbType.String, r.NotQueryFlag.Value)
                        If r.RankIDMapFlag.Updated Then db.AddInParameter(dbcmd, "@RankIDMapFlag", DbType.String, r.RankIDMapFlag.Value)
                        If r.NotShowWorkType.Updated Then db.AddInParameter(dbcmd, "@NotShowWorkType", DbType.String, r.NotShowWorkType.Value)
                        If r.NotShowRankID.Updated Then db.AddInParameter(dbcmd, "@NotShowRankID", DbType.String, r.NotShowRankID.Value)
                        If r.EmpSource.Updated Then db.AddInParameter(dbcmd, "@EmpSource", DbType.String, r.EmpSource.Value)
                        If r.CNFlag.Updated Then db.AddInParameter(dbcmd, "@CNFlag", DbType.String, r.CNFlag.Value)
                        If r.RankIDMapValidDate.Updated Then db.AddInParameter(dbcmd, "@RankIDMapValidDate", DbType.Date, IIf(IsDateTimeNull(r.RankIDMapValidDate.Value), Convert.ToDateTime("1900/1/1"), r.RankIDMapValidDate.Value))
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
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

        Public Function Update(ByVal CompanyRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In CompanyRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update Company Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where CompID = @PKCompID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.CompName.Updated Then db.AddInParameter(dbcmd, "@CompName", DbType.String, r.CompName.Value)
                If r.CompNameCN.Updated Then db.AddInParameter(dbcmd, "@CompNameCN", DbType.String, r.CompNameCN.Value)
                If r.CompEngName.Updated Then db.AddInParameter(dbcmd, "@CompEngName", DbType.String, r.CompEngName.Value)
                If r.CompChnName.Updated Then db.AddInParameter(dbcmd, "@CompChnName", DbType.String, r.CompChnName.Value)
                If r.CompChnNameCN.Updated Then db.AddInParameter(dbcmd, "@CompChnNameCN", DbType.String, r.CompChnNameCN.Value)
                If r.FeeShareFlag.Updated Then db.AddInParameter(dbcmd, "@FeeShareFlag", DbType.String, r.FeeShareFlag.Value)
                If r.GroupID.Updated Then db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                If r.CheckInFile.Updated Then db.AddInParameter(dbcmd, "@CheckInFile", DbType.String, r.CheckInFile.Value)
                If r.Calendar.Updated Then db.AddInParameter(dbcmd, "@Calendar", DbType.String, r.Calendar.Value)
                If r.Payroll.Updated Then db.AddInParameter(dbcmd, "@Payroll", DbType.String, r.Payroll.Value)
                If r.PayrollMaintain.Updated Then db.AddInParameter(dbcmd, "@PayrollMaintain", DbType.String, r.PayrollMaintain.Value)
                If r.SPHSC1GrpFlag.Updated Then db.AddInParameter(dbcmd, "@SPHSC1GrpFlag", DbType.String, r.SPHSC1GrpFlag.Value)
                If r.Probation.Updated Then db.AddInParameter(dbcmd, "@Probation", DbType.String, r.Probation.Value)
                If r.InValidFlag.Updated Then db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, r.InValidFlag.Value)
                If r.NotShowFlag.Updated Then db.AddInParameter(dbcmd, "@NotShowFlag", DbType.String, r.NotShowFlag.Value)
                If r.HRISFlag.Updated Then db.AddInParameter(dbcmd, "@HRISFlag", DbType.String, r.HRISFlag.Value)
                If r.Address.Updated Then db.AddInParameter(dbcmd, "@Address", DbType.String, r.Address.Value)
                If r.AddressCN.Updated Then db.AddInParameter(dbcmd, "@AddressCN", DbType.String, r.AddressCN.Value)
                If r.EngAddress.Updated Then db.AddInParameter(dbcmd, "@EngAddress", DbType.String, r.EngAddress.Value)
                If r.NotQueryFlag.Updated Then db.AddInParameter(dbcmd, "@NotQueryFlag", DbType.String, r.NotQueryFlag.Value)
                If r.RankIDMapFlag.Updated Then db.AddInParameter(dbcmd, "@RankIDMapFlag", DbType.String, r.RankIDMapFlag.Value)
                If r.NotShowWorkType.Updated Then db.AddInParameter(dbcmd, "@NotShowWorkType", DbType.String, r.NotShowWorkType.Value)
                If r.NotShowRankID.Updated Then db.AddInParameter(dbcmd, "@NotShowRankID", DbType.String, r.NotShowRankID.Value)
                If r.EmpSource.Updated Then db.AddInParameter(dbcmd, "@EmpSource", DbType.String, r.EmpSource.Value)
                If r.CNFlag.Updated Then db.AddInParameter(dbcmd, "@CNFlag", DbType.String, r.CNFlag.Value)
                If r.RankIDMapValidDate.Updated Then db.AddInParameter(dbcmd, "@RankIDMapValidDate", DbType.Date, IIf(IsDateTimeNull(r.RankIDMapValidDate.Value), Convert.ToDateTime("1900/1/1"), r.RankIDMapValidDate.Value))
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal CompanyRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From Company")
            strSQL.AppendLine("Where CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, CompanyRow.CompID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal CompanyRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From Company")
            strSQL.AppendLine("Where CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, CompanyRow.CompID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Company")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal CompanyRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into Company")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, CompName, CompNameCN, CompEngName, CompChnName, CompChnNameCN, FeeShareFlag,")
            strSQL.AppendLine("    GroupID, CheckInFile, Calendar, Payroll, PayrollMaintain, SPHSC1GrpFlag, Probation,")
            strSQL.AppendLine("    InValidFlag, NotShowFlag, HRISFlag, Address, AddressCN, EngAddress, NotQueryFlag, RankIDMapFlag,")
            strSQL.AppendLine("    NotShowWorkType, NotShowRankID, EmpSource, CNFlag, RankIDMapValidDate, LastChgComp, LastChgID,")
            strSQL.AppendLine("    LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @CompName, @CompNameCN, @CompEngName, @CompChnName, @CompChnNameCN, @FeeShareFlag,")
            strSQL.AppendLine("    @GroupID, @CheckInFile, @Calendar, @Payroll, @PayrollMaintain, @SPHSC1GrpFlag, @Probation,")
            strSQL.AppendLine("    @InValidFlag, @NotShowFlag, @HRISFlag, @Address, @AddressCN, @EngAddress, @NotQueryFlag, @RankIDMapFlag,")
            strSQL.AppendLine("    @NotShowWorkType, @NotShowRankID, @EmpSource, @CNFlag, @RankIDMapValidDate, @LastChgComp, @LastChgID,")
            strSQL.AppendLine("    @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, CompanyRow.CompID.Value)
            db.AddInParameter(dbcmd, "@CompName", DbType.String, CompanyRow.CompName.Value)
            db.AddInParameter(dbcmd, "@CompNameCN", DbType.String, CompanyRow.CompNameCN.Value)
            db.AddInParameter(dbcmd, "@CompEngName", DbType.String, CompanyRow.CompEngName.Value)
            db.AddInParameter(dbcmd, "@CompChnName", DbType.String, CompanyRow.CompChnName.Value)
            db.AddInParameter(dbcmd, "@CompChnNameCN", DbType.String, CompanyRow.CompChnNameCN.Value)
            db.AddInParameter(dbcmd, "@FeeShareFlag", DbType.String, CompanyRow.FeeShareFlag.Value)
            db.AddInParameter(dbcmd, "@GroupID", DbType.String, CompanyRow.GroupID.Value)
            db.AddInParameter(dbcmd, "@CheckInFile", DbType.String, CompanyRow.CheckInFile.Value)
            db.AddInParameter(dbcmd, "@Calendar", DbType.String, CompanyRow.Calendar.Value)
            db.AddInParameter(dbcmd, "@Payroll", DbType.String, CompanyRow.Payroll.Value)
            db.AddInParameter(dbcmd, "@PayrollMaintain", DbType.String, CompanyRow.PayrollMaintain.Value)
            db.AddInParameter(dbcmd, "@SPHSC1GrpFlag", DbType.String, CompanyRow.SPHSC1GrpFlag.Value)
            db.AddInParameter(dbcmd, "@Probation", DbType.String, CompanyRow.Probation.Value)
            db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, CompanyRow.InValidFlag.Value)
            db.AddInParameter(dbcmd, "@NotShowFlag", DbType.String, CompanyRow.NotShowFlag.Value)
            db.AddInParameter(dbcmd, "@HRISFlag", DbType.String, CompanyRow.HRISFlag.Value)
            db.AddInParameter(dbcmd, "@Address", DbType.String, CompanyRow.Address.Value)
            db.AddInParameter(dbcmd, "@AddressCN", DbType.String, CompanyRow.AddressCN.Value)
            db.AddInParameter(dbcmd, "@EngAddress", DbType.String, CompanyRow.EngAddress.Value)
            db.AddInParameter(dbcmd, "@NotQueryFlag", DbType.String, CompanyRow.NotQueryFlag.Value)
            db.AddInParameter(dbcmd, "@RankIDMapFlag", DbType.String, CompanyRow.RankIDMapFlag.Value)
            db.AddInParameter(dbcmd, "@NotShowWorkType", DbType.String, CompanyRow.NotShowWorkType.Value)
            db.AddInParameter(dbcmd, "@NotShowRankID", DbType.String, CompanyRow.NotShowRankID.Value)
            db.AddInParameter(dbcmd, "@EmpSource", DbType.String, CompanyRow.EmpSource.Value)
            db.AddInParameter(dbcmd, "@CNFlag", DbType.String, CompanyRow.CNFlag.Value)
            db.AddInParameter(dbcmd, "@RankIDMapValidDate", DbType.Date, IIf(IsDateTimeNull(CompanyRow.RankIDMapValidDate.Value), Convert.ToDateTime("1900/1/1"), CompanyRow.RankIDMapValidDate.Value))
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, CompanyRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, CompanyRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(CompanyRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), CompanyRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal CompanyRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into Company")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, CompName, CompNameCN, CompEngName, CompChnName, CompChnNameCN, FeeShareFlag,")
            strSQL.AppendLine("    GroupID, CheckInFile, Calendar, Payroll, PayrollMaintain, SPHSC1GrpFlag, Probation,")
            strSQL.AppendLine("    InValidFlag, NotShowFlag, HRISFlag, Address, AddressCN, EngAddress, NotQueryFlag, RankIDMapFlag,")
            strSQL.AppendLine("    NotShowWorkType, NotShowRankID, EmpSource, CNFlag, RankIDMapValidDate, LastChgComp, LastChgID,")
            strSQL.AppendLine("    LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @CompName, @CompNameCN, @CompEngName, @CompChnName, @CompChnNameCN, @FeeShareFlag,")
            strSQL.AppendLine("    @GroupID, @CheckInFile, @Calendar, @Payroll, @PayrollMaintain, @SPHSC1GrpFlag, @Probation,")
            strSQL.AppendLine("    @InValidFlag, @NotShowFlag, @HRISFlag, @Address, @AddressCN, @EngAddress, @NotQueryFlag, @RankIDMapFlag,")
            strSQL.AppendLine("    @NotShowWorkType, @NotShowRankID, @EmpSource, @CNFlag, @RankIDMapValidDate, @LastChgComp, @LastChgID,")
            strSQL.AppendLine("    @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, CompanyRow.CompID.Value)
            db.AddInParameter(dbcmd, "@CompName", DbType.String, CompanyRow.CompName.Value)
            db.AddInParameter(dbcmd, "@CompNameCN", DbType.String, CompanyRow.CompNameCN.Value)
            db.AddInParameter(dbcmd, "@CompEngName", DbType.String, CompanyRow.CompEngName.Value)
            db.AddInParameter(dbcmd, "@CompChnName", DbType.String, CompanyRow.CompChnName.Value)
            db.AddInParameter(dbcmd, "@CompChnNameCN", DbType.String, CompanyRow.CompChnNameCN.Value)
            db.AddInParameter(dbcmd, "@FeeShareFlag", DbType.String, CompanyRow.FeeShareFlag.Value)
            db.AddInParameter(dbcmd, "@GroupID", DbType.String, CompanyRow.GroupID.Value)
            db.AddInParameter(dbcmd, "@CheckInFile", DbType.String, CompanyRow.CheckInFile.Value)
            db.AddInParameter(dbcmd, "@Calendar", DbType.String, CompanyRow.Calendar.Value)
            db.AddInParameter(dbcmd, "@Payroll", DbType.String, CompanyRow.Payroll.Value)
            db.AddInParameter(dbcmd, "@PayrollMaintain", DbType.String, CompanyRow.PayrollMaintain.Value)
            db.AddInParameter(dbcmd, "@SPHSC1GrpFlag", DbType.String, CompanyRow.SPHSC1GrpFlag.Value)
            db.AddInParameter(dbcmd, "@Probation", DbType.String, CompanyRow.Probation.Value)
            db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, CompanyRow.InValidFlag.Value)
            db.AddInParameter(dbcmd, "@NotShowFlag", DbType.String, CompanyRow.NotShowFlag.Value)
            db.AddInParameter(dbcmd, "@HRISFlag", DbType.String, CompanyRow.HRISFlag.Value)
            db.AddInParameter(dbcmd, "@Address", DbType.String, CompanyRow.Address.Value)
            db.AddInParameter(dbcmd, "@AddressCN", DbType.String, CompanyRow.AddressCN.Value)
            db.AddInParameter(dbcmd, "@EngAddress", DbType.String, CompanyRow.EngAddress.Value)
            db.AddInParameter(dbcmd, "@NotQueryFlag", DbType.String, CompanyRow.NotQueryFlag.Value)
            db.AddInParameter(dbcmd, "@RankIDMapFlag", DbType.String, CompanyRow.RankIDMapFlag.Value)
            db.AddInParameter(dbcmd, "@NotShowWorkType", DbType.String, CompanyRow.NotShowWorkType.Value)
            db.AddInParameter(dbcmd, "@NotShowRankID", DbType.String, CompanyRow.NotShowRankID.Value)
            db.AddInParameter(dbcmd, "@EmpSource", DbType.String, CompanyRow.EmpSource.Value)
            db.AddInParameter(dbcmd, "@CNFlag", DbType.String, CompanyRow.CNFlag.Value)
            db.AddInParameter(dbcmd, "@RankIDMapValidDate", DbType.Date, IIf(IsDateTimeNull(CompanyRow.RankIDMapValidDate.Value), Convert.ToDateTime("1900/1/1"), CompanyRow.RankIDMapValidDate.Value))
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, CompanyRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, CompanyRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(CompanyRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), CompanyRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal CompanyRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into Company")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, CompName, CompNameCN, CompEngName, CompChnName, CompChnNameCN, FeeShareFlag,")
            strSQL.AppendLine("    GroupID, CheckInFile, Calendar, Payroll, PayrollMaintain, SPHSC1GrpFlag, Probation,")
            strSQL.AppendLine("    InValidFlag, NotShowFlag, HRISFlag, Address, AddressCN, EngAddress, NotQueryFlag, RankIDMapFlag,")
            strSQL.AppendLine("    NotShowWorkType, NotShowRankID, EmpSource, CNFlag, RankIDMapValidDate, LastChgComp, LastChgID,")
            strSQL.AppendLine("    LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @CompName, @CompNameCN, @CompEngName, @CompChnName, @CompChnNameCN, @FeeShareFlag,")
            strSQL.AppendLine("    @GroupID, @CheckInFile, @Calendar, @Payroll, @PayrollMaintain, @SPHSC1GrpFlag, @Probation,")
            strSQL.AppendLine("    @InValidFlag, @NotShowFlag, @HRISFlag, @Address, @AddressCN, @EngAddress, @NotQueryFlag, @RankIDMapFlag,")
            strSQL.AppendLine("    @NotShowWorkType, @NotShowRankID, @EmpSource, @CNFlag, @RankIDMapValidDate, @LastChgComp, @LastChgID,")
            strSQL.AppendLine("    @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In CompanyRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@CompName", DbType.String, r.CompName.Value)
                        db.AddInParameter(dbcmd, "@CompNameCN", DbType.String, r.CompNameCN.Value)
                        db.AddInParameter(dbcmd, "@CompEngName", DbType.String, r.CompEngName.Value)
                        db.AddInParameter(dbcmd, "@CompChnName", DbType.String, r.CompChnName.Value)
                        db.AddInParameter(dbcmd, "@CompChnNameCN", DbType.String, r.CompChnNameCN.Value)
                        db.AddInParameter(dbcmd, "@FeeShareFlag", DbType.String, r.FeeShareFlag.Value)
                        db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                        db.AddInParameter(dbcmd, "@CheckInFile", DbType.String, r.CheckInFile.Value)
                        db.AddInParameter(dbcmd, "@Calendar", DbType.String, r.Calendar.Value)
                        db.AddInParameter(dbcmd, "@Payroll", DbType.String, r.Payroll.Value)
                        db.AddInParameter(dbcmd, "@PayrollMaintain", DbType.String, r.PayrollMaintain.Value)
                        db.AddInParameter(dbcmd, "@SPHSC1GrpFlag", DbType.String, r.SPHSC1GrpFlag.Value)
                        db.AddInParameter(dbcmd, "@Probation", DbType.String, r.Probation.Value)
                        db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, r.InValidFlag.Value)
                        db.AddInParameter(dbcmd, "@NotShowFlag", DbType.String, r.NotShowFlag.Value)
                        db.AddInParameter(dbcmd, "@HRISFlag", DbType.String, r.HRISFlag.Value)
                        db.AddInParameter(dbcmd, "@Address", DbType.String, r.Address.Value)
                        db.AddInParameter(dbcmd, "@AddressCN", DbType.String, r.AddressCN.Value)
                        db.AddInParameter(dbcmd, "@EngAddress", DbType.String, r.EngAddress.Value)
                        db.AddInParameter(dbcmd, "@NotQueryFlag", DbType.String, r.NotQueryFlag.Value)
                        db.AddInParameter(dbcmd, "@RankIDMapFlag", DbType.String, r.RankIDMapFlag.Value)
                        db.AddInParameter(dbcmd, "@NotShowWorkType", DbType.String, r.NotShowWorkType.Value)
                        db.AddInParameter(dbcmd, "@NotShowRankID", DbType.String, r.NotShowRankID.Value)
                        db.AddInParameter(dbcmd, "@EmpSource", DbType.String, r.EmpSource.Value)
                        db.AddInParameter(dbcmd, "@CNFlag", DbType.String, r.CNFlag.Value)
                        db.AddInParameter(dbcmd, "@RankIDMapValidDate", DbType.Date, IIf(IsDateTimeNull(r.RankIDMapValidDate.Value), Convert.ToDateTime("1900/1/1"), r.RankIDMapValidDate.Value))
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

        Public Function Insert(ByVal CompanyRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into Company")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, CompName, CompNameCN, CompEngName, CompChnName, CompChnNameCN, FeeShareFlag,")
            strSQL.AppendLine("    GroupID, CheckInFile, Calendar, Payroll, PayrollMaintain, SPHSC1GrpFlag, Probation,")
            strSQL.AppendLine("    InValidFlag, NotShowFlag, HRISFlag, Address, AddressCN, EngAddress, NotQueryFlag, RankIDMapFlag,")
            strSQL.AppendLine("    NotShowWorkType, NotShowRankID, EmpSource, CNFlag, RankIDMapValidDate, LastChgComp, LastChgID,")
            strSQL.AppendLine("    LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @CompName, @CompNameCN, @CompEngName, @CompChnName, @CompChnNameCN, @FeeShareFlag,")
            strSQL.AppendLine("    @GroupID, @CheckInFile, @Calendar, @Payroll, @PayrollMaintain, @SPHSC1GrpFlag, @Probation,")
            strSQL.AppendLine("    @InValidFlag, @NotShowFlag, @HRISFlag, @Address, @AddressCN, @EngAddress, @NotQueryFlag, @RankIDMapFlag,")
            strSQL.AppendLine("    @NotShowWorkType, @NotShowRankID, @EmpSource, @CNFlag, @RankIDMapValidDate, @LastChgComp, @LastChgID,")
            strSQL.AppendLine("    @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In CompanyRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@CompName", DbType.String, r.CompName.Value)
                db.AddInParameter(dbcmd, "@CompNameCN", DbType.String, r.CompNameCN.Value)
                db.AddInParameter(dbcmd, "@CompEngName", DbType.String, r.CompEngName.Value)
                db.AddInParameter(dbcmd, "@CompChnName", DbType.String, r.CompChnName.Value)
                db.AddInParameter(dbcmd, "@CompChnNameCN", DbType.String, r.CompChnNameCN.Value)
                db.AddInParameter(dbcmd, "@FeeShareFlag", DbType.String, r.FeeShareFlag.Value)
                db.AddInParameter(dbcmd, "@GroupID", DbType.String, r.GroupID.Value)
                db.AddInParameter(dbcmd, "@CheckInFile", DbType.String, r.CheckInFile.Value)
                db.AddInParameter(dbcmd, "@Calendar", DbType.String, r.Calendar.Value)
                db.AddInParameter(dbcmd, "@Payroll", DbType.String, r.Payroll.Value)
                db.AddInParameter(dbcmd, "@PayrollMaintain", DbType.String, r.PayrollMaintain.Value)
                db.AddInParameter(dbcmd, "@SPHSC1GrpFlag", DbType.String, r.SPHSC1GrpFlag.Value)
                db.AddInParameter(dbcmd, "@Probation", DbType.String, r.Probation.Value)
                db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, r.InValidFlag.Value)
                db.AddInParameter(dbcmd, "@NotShowFlag", DbType.String, r.NotShowFlag.Value)
                db.AddInParameter(dbcmd, "@HRISFlag", DbType.String, r.HRISFlag.Value)
                db.AddInParameter(dbcmd, "@Address", DbType.String, r.Address.Value)
                db.AddInParameter(dbcmd, "@AddressCN", DbType.String, r.AddressCN.Value)
                db.AddInParameter(dbcmd, "@EngAddress", DbType.String, r.EngAddress.Value)
                db.AddInParameter(dbcmd, "@NotQueryFlag", DbType.String, r.NotQueryFlag.Value)
                db.AddInParameter(dbcmd, "@RankIDMapFlag", DbType.String, r.RankIDMapFlag.Value)
                db.AddInParameter(dbcmd, "@NotShowWorkType", DbType.String, r.NotShowWorkType.Value)
                db.AddInParameter(dbcmd, "@NotShowRankID", DbType.String, r.NotShowRankID.Value)
                db.AddInParameter(dbcmd, "@EmpSource", DbType.String, r.EmpSource.Value)
                db.AddInParameter(dbcmd, "@CNFlag", DbType.String, r.CNFlag.Value)
                db.AddInParameter(dbcmd, "@RankIDMapValidDate", DbType.Date, IIf(IsDateTimeNull(r.RankIDMapValidDate.Value), Convert.ToDateTime("1900/1/1"), r.RankIDMapValidDate.Value))
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

