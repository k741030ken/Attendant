'****************************************************************
' Table:SalaryData
' Created Date: 2015.01.20
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beSalaryData
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "EmpID", "TaxRearNo", "FeeShareComp", "FeeShareDept", "SalaryPaid", "SalaryFlag", "RetireFlag", "WelfareFlag", "UnityAccountFlag" _
                                    , "DeductTaxRatio", "DeductTaxRatioFlag", "MonthOfAnnualPay", "ProcessDate", "SalaryDate", "DelayFlag", "TaxOption", "NotFesFlag", "CreateUserComp", "CreateUserID", "CreateDate" _
                                    , "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(Integer), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(Integer), GetType(String), GetType(Integer), GetType(Integer), GetType(Date), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date) _
                                    , GetType(String), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "CompID", "EmpID" }

        Public ReadOnly Property Rows() As beSalaryData.Rows 
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
        Public Sub Transfer2Row(SalaryDataTable As DataTable)
            For Each dr As DataRow In SalaryDataTable.Rows
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
                dr(m_Rows(i).TaxRearNo.FieldName) = m_Rows(i).TaxRearNo.Value
                dr(m_Rows(i).FeeShareComp.FieldName) = m_Rows(i).FeeShareComp.Value
                dr(m_Rows(i).FeeShareDept.FieldName) = m_Rows(i).FeeShareDept.Value
                dr(m_Rows(i).SalaryPaid.FieldName) = m_Rows(i).SalaryPaid.Value
                dr(m_Rows(i).SalaryFlag.FieldName) = m_Rows(i).SalaryFlag.Value
                dr(m_Rows(i).RetireFlag.FieldName) = m_Rows(i).RetireFlag.Value
                dr(m_Rows(i).WelfareFlag.FieldName) = m_Rows(i).WelfareFlag.Value
                dr(m_Rows(i).UnityAccountFlag.FieldName) = m_Rows(i).UnityAccountFlag.Value
                dr(m_Rows(i).DeductTaxRatio.FieldName) = m_Rows(i).DeductTaxRatio.Value
                dr(m_Rows(i).DeductTaxRatioFlag.FieldName) = m_Rows(i).DeductTaxRatioFlag.Value
                dr(m_Rows(i).MonthOfAnnualPay.FieldName) = m_Rows(i).MonthOfAnnualPay.Value
                dr(m_Rows(i).ProcessDate.FieldName) = m_Rows(i).ProcessDate.Value
                dr(m_Rows(i).SalaryDate.FieldName) = m_Rows(i).SalaryDate.Value
                dr(m_Rows(i).DelayFlag.FieldName) = m_Rows(i).DelayFlag.Value
                dr(m_Rows(i).TaxOption.FieldName) = m_Rows(i).TaxOption.Value
                dr(m_Rows(i).NotFesFlag.FieldName) = m_Rows(i).NotFesFlag.Value
                dr(m_Rows(i).CreateUserComp.FieldName) = m_Rows(i).CreateUserComp.Value
                dr(m_Rows(i).CreateUserID.FieldName) = m_Rows(i).CreateUserID.Value
                dr(m_Rows(i).CreateDate.FieldName) = m_Rows(i).CreateDate.Value
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

        Public Sub Add(SalaryDataRow As Row)
            m_Rows.Add(SalaryDataRow)
        End Sub

        Public Sub Remove(SalaryDataRow As Row)
            If m_Rows.IndexOf(SalaryDataRow) >= 0 Then
                m_Rows.Remove(SalaryDataRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_EmpID As Field(Of String) = new Field(Of String)("EmpID", true)
        Private FI_TaxRearNo As Field(Of Integer) = new Field(Of Integer)("TaxRearNo", true)
        Private FI_FeeShareComp As Field(Of String) = new Field(Of String)("FeeShareComp", true)
        Private FI_FeeShareDept As Field(Of String) = new Field(Of String)("FeeShareDept", true)
        Private FI_SalaryPaid As Field(Of String) = new Field(Of String)("SalaryPaid", true)
        Private FI_SalaryFlag As Field(Of String) = new Field(Of String)("SalaryFlag", true)
        Private FI_RetireFlag As Field(Of String) = new Field(Of String)("RetireFlag", true)
        Private FI_WelfareFlag As Field(Of String) = new Field(Of String)("WelfareFlag", true)
        Private FI_UnityAccountFlag As Field(Of String) = new Field(Of String)("UnityAccountFlag", true)
        Private FI_DeductTaxRatio As Field(Of Integer) = new Field(Of Integer)("DeductTaxRatio", true)
        Private FI_DeductTaxRatioFlag As Field(Of String) = new Field(Of String)("DeductTaxRatioFlag", true)
        Private FI_MonthOfAnnualPay As Field(Of Integer) = new Field(Of Integer)("MonthOfAnnualPay", true)
        Private FI_ProcessDate As Field(Of Integer) = new Field(Of Integer)("ProcessDate", true)
        Private FI_SalaryDate As Field(Of Date) = new Field(Of Date)("SalaryDate", true)
        Private FI_DelayFlag As Field(Of String) = new Field(Of String)("DelayFlag", true)
        Private FI_TaxOption As Field(Of String) = new Field(Of String)("TaxOption", true)
        Private FI_NotFesFlag As Field(Of String) = new Field(Of String)("NotFesFlag", true)
        Private FI_CreateUserComp As Field(Of String) = new Field(Of String)("CreateUserComp", true)
        Private FI_CreateUserID As Field(Of String) = new Field(Of String)("CreateUserID", true)
        Private FI_CreateDate As Field(Of Date) = new Field(Of Date)("CreateDate", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "CompID", "EmpID", "TaxRearNo", "FeeShareComp", "FeeShareDept", "SalaryPaid", "SalaryFlag", "RetireFlag", "WelfareFlag", "UnityAccountFlag" _
                                    , "DeductTaxRatio", "DeductTaxRatioFlag", "MonthOfAnnualPay", "ProcessDate", "SalaryDate", "DelayFlag", "TaxOption", "NotFesFlag", "CreateUserComp", "CreateUserID", "CreateDate" _
                                    , "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "CompID", "EmpID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CompID"
                    Return FI_CompID.Value
                Case "EmpID"
                    Return FI_EmpID.Value
                Case "TaxRearNo"
                    Return FI_TaxRearNo.Value
                Case "FeeShareComp"
                    Return FI_FeeShareComp.Value
                Case "FeeShareDept"
                    Return FI_FeeShareDept.Value
                Case "SalaryPaid"
                    Return FI_SalaryPaid.Value
                Case "SalaryFlag"
                    Return FI_SalaryFlag.Value
                Case "RetireFlag"
                    Return FI_RetireFlag.Value
                Case "WelfareFlag"
                    Return FI_WelfareFlag.Value
                Case "UnityAccountFlag"
                    Return FI_UnityAccountFlag.Value
                Case "DeductTaxRatio"
                    Return FI_DeductTaxRatio.Value
                Case "DeductTaxRatioFlag"
                    Return FI_DeductTaxRatioFlag.Value
                Case "MonthOfAnnualPay"
                    Return FI_MonthOfAnnualPay.Value
                Case "ProcessDate"
                    Return FI_ProcessDate.Value
                Case "SalaryDate"
                    Return FI_SalaryDate.Value
                Case "DelayFlag"
                    Return FI_DelayFlag.Value
                Case "TaxOption"
                    Return FI_TaxOption.Value
                Case "NotFesFlag"
                    Return FI_NotFesFlag.Value
                Case "CreateUserComp"
                    Return FI_CreateUserComp.Value
                Case "CreateUserID"
                    Return FI_CreateUserID.Value
                Case "CreateDate"
                    Return FI_CreateDate.Value
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
                Case "TaxRearNo"
                    FI_TaxRearNo.SetValue(value)
                Case "FeeShareComp"
                    FI_FeeShareComp.SetValue(value)
                Case "FeeShareDept"
                    FI_FeeShareDept.SetValue(value)
                Case "SalaryPaid"
                    FI_SalaryPaid.SetValue(value)
                Case "SalaryFlag"
                    FI_SalaryFlag.SetValue(value)
                Case "RetireFlag"
                    FI_RetireFlag.SetValue(value)
                Case "WelfareFlag"
                    FI_WelfareFlag.SetValue(value)
                Case "UnityAccountFlag"
                    FI_UnityAccountFlag.SetValue(value)
                Case "DeductTaxRatio"
                    FI_DeductTaxRatio.SetValue(value)
                Case "DeductTaxRatioFlag"
                    FI_DeductTaxRatioFlag.SetValue(value)
                Case "MonthOfAnnualPay"
                    FI_MonthOfAnnualPay.SetValue(value)
                Case "ProcessDate"
                    FI_ProcessDate.SetValue(value)
                Case "SalaryDate"
                    FI_SalaryDate.SetValue(value)
                Case "DelayFlag"
                    FI_DelayFlag.SetValue(value)
                Case "TaxOption"
                    FI_TaxOption.SetValue(value)
                Case "NotFesFlag"
                    FI_NotFesFlag.SetValue(value)
                Case "CreateUserComp"
                    FI_CreateUserComp.SetValue(value)
                Case "CreateUserID"
                    FI_CreateUserID.SetValue(value)
                Case "CreateDate"
                    FI_CreateDate.SetValue(value)
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
                Case "TaxRearNo"
                    return FI_TaxRearNo.Updated
                Case "FeeShareComp"
                    return FI_FeeShareComp.Updated
                Case "FeeShareDept"
                    return FI_FeeShareDept.Updated
                Case "SalaryPaid"
                    return FI_SalaryPaid.Updated
                Case "SalaryFlag"
                    return FI_SalaryFlag.Updated
                Case "RetireFlag"
                    return FI_RetireFlag.Updated
                Case "WelfareFlag"
                    return FI_WelfareFlag.Updated
                Case "UnityAccountFlag"
                    return FI_UnityAccountFlag.Updated
                Case "DeductTaxRatio"
                    return FI_DeductTaxRatio.Updated
                Case "DeductTaxRatioFlag"
                    return FI_DeductTaxRatioFlag.Updated
                Case "MonthOfAnnualPay"
                    return FI_MonthOfAnnualPay.Updated
                Case "ProcessDate"
                    return FI_ProcessDate.Updated
                Case "SalaryDate"
                    return FI_SalaryDate.Updated
                Case "DelayFlag"
                    return FI_DelayFlag.Updated
                Case "TaxOption"
                    return FI_TaxOption.Updated
                Case "NotFesFlag"
                    return FI_NotFesFlag.Updated
                Case "CreateUserComp"
                    return FI_CreateUserComp.Updated
                Case "CreateUserID"
                    return FI_CreateUserID.Updated
                Case "CreateDate"
                    return FI_CreateDate.Updated
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
                Case "TaxRearNo"
                    return FI_TaxRearNo.CreateUpdateSQL
                Case "FeeShareComp"
                    return FI_FeeShareComp.CreateUpdateSQL
                Case "FeeShareDept"
                    return FI_FeeShareDept.CreateUpdateSQL
                Case "SalaryPaid"
                    return FI_SalaryPaid.CreateUpdateSQL
                Case "SalaryFlag"
                    return FI_SalaryFlag.CreateUpdateSQL
                Case "RetireFlag"
                    return FI_RetireFlag.CreateUpdateSQL
                Case "WelfareFlag"
                    return FI_WelfareFlag.CreateUpdateSQL
                Case "UnityAccountFlag"
                    return FI_UnityAccountFlag.CreateUpdateSQL
                Case "DeductTaxRatio"
                    return FI_DeductTaxRatio.CreateUpdateSQL
                Case "DeductTaxRatioFlag"
                    return FI_DeductTaxRatioFlag.CreateUpdateSQL
                Case "MonthOfAnnualPay"
                    return FI_MonthOfAnnualPay.CreateUpdateSQL
                Case "ProcessDate"
                    return FI_ProcessDate.CreateUpdateSQL
                Case "SalaryDate"
                    return FI_SalaryDate.CreateUpdateSQL
                Case "DelayFlag"
                    return FI_DelayFlag.CreateUpdateSQL
                Case "TaxOption"
                    return FI_TaxOption.CreateUpdateSQL
                Case "NotFesFlag"
                    return FI_NotFesFlag.CreateUpdateSQL
                Case "CreateUserComp"
                    return FI_CreateUserComp.CreateUpdateSQL
                Case "CreateUserID"
                    return FI_CreateUserID.CreateUpdateSQL
                Case "CreateDate"
                    return FI_CreateDate.CreateUpdateSQL
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
            FI_TaxRearNo.SetInitValue(0)
            FI_FeeShareComp.SetInitValue("")
            FI_FeeShareDept.SetInitValue("")
            FI_SalaryPaid.SetInitValue("0")
            FI_SalaryFlag.SetInitValue("0")
            FI_RetireFlag.SetInitValue("1")
            FI_WelfareFlag.SetInitValue("1")
            FI_UnityAccountFlag.SetInitValue("1")
            FI_DeductTaxRatio.SetInitValue(0)
            FI_DeductTaxRatioFlag.SetInitValue(0)
            FI_MonthOfAnnualPay.SetInitValue(12)
            FI_ProcessDate.SetInitValue(0)
            FI_SalaryDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_DelayFlag.SetInitValue("0")
            FI_TaxOption.SetInitValue("1")
            FI_NotFesFlag.SetInitValue("0")
            FI_CreateUserComp.SetInitValue("")
            FI_CreateUserID.SetInitValue("")
            FI_CreateDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CompID.SetInitValue(dr("CompID"))
            FI_EmpID.SetInitValue(dr("EmpID"))
            FI_TaxRearNo.SetInitValue(dr("TaxRearNo"))
            FI_FeeShareComp.SetInitValue(dr("FeeShareComp"))
            FI_FeeShareDept.SetInitValue(dr("FeeShareDept"))
            FI_SalaryPaid.SetInitValue(dr("SalaryPaid"))
            FI_SalaryFlag.SetInitValue(dr("SalaryFlag"))
            FI_RetireFlag.SetInitValue(dr("RetireFlag"))
            FI_WelfareFlag.SetInitValue(dr("WelfareFlag"))
            FI_UnityAccountFlag.SetInitValue(dr("UnityAccountFlag"))
            FI_DeductTaxRatio.SetInitValue(dr("DeductTaxRatio"))
            FI_DeductTaxRatioFlag.SetInitValue(dr("DeductTaxRatioFlag"))
            FI_MonthOfAnnualPay.SetInitValue(dr("MonthOfAnnualPay"))
            FI_ProcessDate.SetInitValue(dr("ProcessDate"))
            FI_SalaryDate.SetInitValue(dr("SalaryDate"))
            FI_DelayFlag.SetInitValue(dr("DelayFlag"))
            FI_TaxOption.SetInitValue(dr("TaxOption"))
            FI_NotFesFlag.SetInitValue(dr("NotFesFlag"))
            FI_CreateUserComp.SetInitValue(dr("CreateUserComp"))
            FI_CreateUserID.SetInitValue(dr("CreateUserID"))
            FI_CreateDate.SetInitValue(dr("CreateDate"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CompID.Updated = False
            FI_EmpID.Updated = False
            FI_TaxRearNo.Updated = False
            FI_FeeShareComp.Updated = False
            FI_FeeShareDept.Updated = False
            FI_SalaryPaid.Updated = False
            FI_SalaryFlag.Updated = False
            FI_RetireFlag.Updated = False
            FI_WelfareFlag.Updated = False
            FI_UnityAccountFlag.Updated = False
            FI_DeductTaxRatio.Updated = False
            FI_DeductTaxRatioFlag.Updated = False
            FI_MonthOfAnnualPay.Updated = False
            FI_ProcessDate.Updated = False
            FI_SalaryDate.Updated = False
            FI_DelayFlag.Updated = False
            FI_TaxOption.Updated = False
            FI_NotFesFlag.Updated = False
            FI_CreateUserComp.Updated = False
            FI_CreateUserID.Updated = False
            FI_CreateDate.Updated = False
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

        Public ReadOnly Property TaxRearNo As Field(Of Integer) 
            Get
                Return FI_TaxRearNo
            End Get
        End Property

        Public ReadOnly Property FeeShareComp As Field(Of String) 
            Get
                Return FI_FeeShareComp
            End Get
        End Property

        Public ReadOnly Property FeeShareDept As Field(Of String) 
            Get
                Return FI_FeeShareDept
            End Get
        End Property

        Public ReadOnly Property SalaryPaid As Field(Of String) 
            Get
                Return FI_SalaryPaid
            End Get
        End Property

        Public ReadOnly Property SalaryFlag As Field(Of String) 
            Get
                Return FI_SalaryFlag
            End Get
        End Property

        Public ReadOnly Property RetireFlag As Field(Of String) 
            Get
                Return FI_RetireFlag
            End Get
        End Property

        Public ReadOnly Property WelfareFlag As Field(Of String) 
            Get
                Return FI_WelfareFlag
            End Get
        End Property

        Public ReadOnly Property UnityAccountFlag As Field(Of String) 
            Get
                Return FI_UnityAccountFlag
            End Get
        End Property

        Public ReadOnly Property DeductTaxRatio As Field(Of Integer) 
            Get
                Return FI_DeductTaxRatio
            End Get
        End Property

        Public ReadOnly Property DeductTaxRatioFlag As Field(Of String) 
            Get
                Return FI_DeductTaxRatioFlag
            End Get
        End Property

        Public ReadOnly Property MonthOfAnnualPay As Field(Of Integer) 
            Get
                Return FI_MonthOfAnnualPay
            End Get
        End Property

        Public ReadOnly Property ProcessDate As Field(Of Integer) 
            Get
                Return FI_ProcessDate
            End Get
        End Property

        Public ReadOnly Property SalaryDate As Field(Of Date) 
            Get
                Return FI_SalaryDate
            End Get
        End Property

        Public ReadOnly Property DelayFlag As Field(Of String) 
            Get
                Return FI_DelayFlag
            End Get
        End Property

        Public ReadOnly Property TaxOption As Field(Of String) 
            Get
                Return FI_TaxOption
            End Get
        End Property

        Public ReadOnly Property NotFesFlag As Field(Of String) 
            Get
                Return FI_NotFesFlag
            End Get
        End Property

        Public ReadOnly Property CreateUserComp As Field(Of String) 
            Get
                Return FI_CreateUserComp
            End Get
        End Property

        Public ReadOnly Property CreateUserID As Field(Of String) 
            Get
                Return FI_CreateUserID
            End Get
        End Property

        Public ReadOnly Property CreateDate As Field(Of Date) 
            Get
                Return FI_CreateDate
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
        Public Function DeleteRowByPrimaryKey(ByVal SalaryDataRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SalaryData")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SalaryDataRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, SalaryDataRow.EmpID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal SalaryDataRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From SalaryData")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SalaryDataRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, SalaryDataRow.EmpID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal SalaryDataRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SalaryData")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SalaryDataRow
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

        Public Function DeleteRowByPrimaryKey(ByVal SalaryDataRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From SalaryData")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SalaryDataRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal SalaryDataRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SalaryData")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SalaryDataRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, SalaryDataRow.EmpID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(SalaryDataRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SalaryData")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SalaryDataRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, SalaryDataRow.EmpID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal SalaryDataRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SalaryData Set")
            For i As Integer = 0 To SalaryDataRow.FieldNames.Length - 1
                If Not SalaryDataRow.IsIdentityField(SalaryDataRow.FieldNames(i)) AndAlso SalaryDataRow.IsUpdated(SalaryDataRow.FieldNames(i)) AndAlso SalaryDataRow.CreateUpdateSQL(SalaryDataRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SalaryDataRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And EmpID = @PKEmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SalaryDataRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, SalaryDataRow.CompID.Value)
            If SalaryDataRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, SalaryDataRow.EmpID.Value)
            If SalaryDataRow.TaxRearNo.Updated Then db.AddInParameter(dbcmd, "@TaxRearNo", DbType.Int32, SalaryDataRow.TaxRearNo.Value)
            If SalaryDataRow.FeeShareComp.Updated Then db.AddInParameter(dbcmd, "@FeeShareComp", DbType.String, SalaryDataRow.FeeShareComp.Value)
            If SalaryDataRow.FeeShareDept.Updated Then db.AddInParameter(dbcmd, "@FeeShareDept", DbType.String, SalaryDataRow.FeeShareDept.Value)
            If SalaryDataRow.SalaryPaid.Updated Then db.AddInParameter(dbcmd, "@SalaryPaid", DbType.String, SalaryDataRow.SalaryPaid.Value)
            If SalaryDataRow.SalaryFlag.Updated Then db.AddInParameter(dbcmd, "@SalaryFlag", DbType.String, SalaryDataRow.SalaryFlag.Value)
            If SalaryDataRow.RetireFlag.Updated Then db.AddInParameter(dbcmd, "@RetireFlag", DbType.String, SalaryDataRow.RetireFlag.Value)
            If SalaryDataRow.WelfareFlag.Updated Then db.AddInParameter(dbcmd, "@WelfareFlag", DbType.String, SalaryDataRow.WelfareFlag.Value)
            If SalaryDataRow.UnityAccountFlag.Updated Then db.AddInParameter(dbcmd, "@UnityAccountFlag", DbType.String, SalaryDataRow.UnityAccountFlag.Value)
            If SalaryDataRow.DeductTaxRatio.Updated Then db.AddInParameter(dbcmd, "@DeductTaxRatio", DbType.Int32, SalaryDataRow.DeductTaxRatio.Value)
            If SalaryDataRow.DeductTaxRatioFlag.Updated Then db.AddInParameter(dbcmd, "@DeductTaxRatioFlag", DbType.String, SalaryDataRow.DeductTaxRatioFlag.Value)
            If SalaryDataRow.MonthOfAnnualPay.Updated Then db.AddInParameter(dbcmd, "@MonthOfAnnualPay", DbType.Int32, SalaryDataRow.MonthOfAnnualPay.Value)
            If SalaryDataRow.ProcessDate.Updated Then db.AddInParameter(dbcmd, "@ProcessDate", DbType.Int32, SalaryDataRow.ProcessDate.Value)
            If SalaryDataRow.SalaryDate.Updated Then db.AddInParameter(dbcmd, "@SalaryDate", DbType.Date, IIf(IsDateTimeNull(SalaryDataRow.SalaryDate.Value), Convert.ToDateTime("1900/1/1"), SalaryDataRow.SalaryDate.Value))
            If SalaryDataRow.DelayFlag.Updated Then db.AddInParameter(dbcmd, "@DelayFlag", DbType.String, SalaryDataRow.DelayFlag.Value)
            If SalaryDataRow.TaxOption.Updated Then db.AddInParameter(dbcmd, "@TaxOption", DbType.String, SalaryDataRow.TaxOption.Value)
            If SalaryDataRow.NotFesFlag.Updated Then db.AddInParameter(dbcmd, "@NotFesFlag", DbType.String, SalaryDataRow.NotFesFlag.Value)
            If SalaryDataRow.CreateUserComp.Updated Then db.AddInParameter(dbcmd, "@CreateUserComp", DbType.String, SalaryDataRow.CreateUserComp.Value)
            If SalaryDataRow.CreateUserID.Updated Then db.AddInParameter(dbcmd, "@CreateUserID", DbType.String, SalaryDataRow.CreateUserID.Value)
            If SalaryDataRow.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SalaryDataRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SalaryDataRow.CreateDate.Value))
            If SalaryDataRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, SalaryDataRow.LastChgComp.Value)
            If SalaryDataRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SalaryDataRow.LastChgID.Value)
            If SalaryDataRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SalaryDataRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SalaryDataRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(SalaryDataRow.LoadFromDataRow, SalaryDataRow.CompID.OldValue, SalaryDataRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(SalaryDataRow.LoadFromDataRow, SalaryDataRow.EmpID.OldValue, SalaryDataRow.EmpID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal SalaryDataRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update SalaryData Set")
            For i As Integer = 0 To SalaryDataRow.FieldNames.Length - 1
                If Not SalaryDataRow.IsIdentityField(SalaryDataRow.FieldNames(i)) AndAlso SalaryDataRow.IsUpdated(SalaryDataRow.FieldNames(i)) AndAlso SalaryDataRow.CreateUpdateSQL(SalaryDataRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SalaryDataRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And EmpID = @PKEmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SalaryDataRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, SalaryDataRow.CompID.Value)
            If SalaryDataRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, SalaryDataRow.EmpID.Value)
            If SalaryDataRow.TaxRearNo.Updated Then db.AddInParameter(dbcmd, "@TaxRearNo", DbType.Int32, SalaryDataRow.TaxRearNo.Value)
            If SalaryDataRow.FeeShareComp.Updated Then db.AddInParameter(dbcmd, "@FeeShareComp", DbType.String, SalaryDataRow.FeeShareComp.Value)
            If SalaryDataRow.FeeShareDept.Updated Then db.AddInParameter(dbcmd, "@FeeShareDept", DbType.String, SalaryDataRow.FeeShareDept.Value)
            If SalaryDataRow.SalaryPaid.Updated Then db.AddInParameter(dbcmd, "@SalaryPaid", DbType.String, SalaryDataRow.SalaryPaid.Value)
            If SalaryDataRow.SalaryFlag.Updated Then db.AddInParameter(dbcmd, "@SalaryFlag", DbType.String, SalaryDataRow.SalaryFlag.Value)
            If SalaryDataRow.RetireFlag.Updated Then db.AddInParameter(dbcmd, "@RetireFlag", DbType.String, SalaryDataRow.RetireFlag.Value)
            If SalaryDataRow.WelfareFlag.Updated Then db.AddInParameter(dbcmd, "@WelfareFlag", DbType.String, SalaryDataRow.WelfareFlag.Value)
            If SalaryDataRow.UnityAccountFlag.Updated Then db.AddInParameter(dbcmd, "@UnityAccountFlag", DbType.String, SalaryDataRow.UnityAccountFlag.Value)
            If SalaryDataRow.DeductTaxRatio.Updated Then db.AddInParameter(dbcmd, "@DeductTaxRatio", DbType.Int32, SalaryDataRow.DeductTaxRatio.Value)
            If SalaryDataRow.DeductTaxRatioFlag.Updated Then db.AddInParameter(dbcmd, "@DeductTaxRatioFlag", DbType.String, SalaryDataRow.DeductTaxRatioFlag.Value)
            If SalaryDataRow.MonthOfAnnualPay.Updated Then db.AddInParameter(dbcmd, "@MonthOfAnnualPay", DbType.Int32, SalaryDataRow.MonthOfAnnualPay.Value)
            If SalaryDataRow.ProcessDate.Updated Then db.AddInParameter(dbcmd, "@ProcessDate", DbType.Int32, SalaryDataRow.ProcessDate.Value)
            If SalaryDataRow.SalaryDate.Updated Then db.AddInParameter(dbcmd, "@SalaryDate", DbType.Date, IIf(IsDateTimeNull(SalaryDataRow.SalaryDate.Value), Convert.ToDateTime("1900/1/1"), SalaryDataRow.SalaryDate.Value))
            If SalaryDataRow.DelayFlag.Updated Then db.AddInParameter(dbcmd, "@DelayFlag", DbType.String, SalaryDataRow.DelayFlag.Value)
            If SalaryDataRow.TaxOption.Updated Then db.AddInParameter(dbcmd, "@TaxOption", DbType.String, SalaryDataRow.TaxOption.Value)
            If SalaryDataRow.NotFesFlag.Updated Then db.AddInParameter(dbcmd, "@NotFesFlag", DbType.String, SalaryDataRow.NotFesFlag.Value)
            If SalaryDataRow.CreateUserComp.Updated Then db.AddInParameter(dbcmd, "@CreateUserComp", DbType.String, SalaryDataRow.CreateUserComp.Value)
            If SalaryDataRow.CreateUserID.Updated Then db.AddInParameter(dbcmd, "@CreateUserID", DbType.String, SalaryDataRow.CreateUserID.Value)
            If SalaryDataRow.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SalaryDataRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SalaryDataRow.CreateDate.Value))
            If SalaryDataRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, SalaryDataRow.LastChgComp.Value)
            If SalaryDataRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SalaryDataRow.LastChgID.Value)
            If SalaryDataRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SalaryDataRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SalaryDataRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(SalaryDataRow.LoadFromDataRow, SalaryDataRow.CompID.OldValue, SalaryDataRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(SalaryDataRow.LoadFromDataRow, SalaryDataRow.EmpID.OldValue, SalaryDataRow.EmpID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal SalaryDataRow As Row()) As Integer
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
                    For Each r As Row In SalaryDataRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update SalaryData Set")
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
                        If r.TaxRearNo.Updated Then db.AddInParameter(dbcmd, "@TaxRearNo", DbType.Int32, r.TaxRearNo.Value)
                        If r.FeeShareComp.Updated Then db.AddInParameter(dbcmd, "@FeeShareComp", DbType.String, r.FeeShareComp.Value)
                        If r.FeeShareDept.Updated Then db.AddInParameter(dbcmd, "@FeeShareDept", DbType.String, r.FeeShareDept.Value)
                        If r.SalaryPaid.Updated Then db.AddInParameter(dbcmd, "@SalaryPaid", DbType.String, r.SalaryPaid.Value)
                        If r.SalaryFlag.Updated Then db.AddInParameter(dbcmd, "@SalaryFlag", DbType.String, r.SalaryFlag.Value)
                        If r.RetireFlag.Updated Then db.AddInParameter(dbcmd, "@RetireFlag", DbType.String, r.RetireFlag.Value)
                        If r.WelfareFlag.Updated Then db.AddInParameter(dbcmd, "@WelfareFlag", DbType.String, r.WelfareFlag.Value)
                        If r.UnityAccountFlag.Updated Then db.AddInParameter(dbcmd, "@UnityAccountFlag", DbType.String, r.UnityAccountFlag.Value)
                        If r.DeductTaxRatio.Updated Then db.AddInParameter(dbcmd, "@DeductTaxRatio", DbType.Int32, r.DeductTaxRatio.Value)
                        If r.DeductTaxRatioFlag.Updated Then db.AddInParameter(dbcmd, "@DeductTaxRatioFlag", DbType.String, r.DeductTaxRatioFlag.Value)
                        If r.MonthOfAnnualPay.Updated Then db.AddInParameter(dbcmd, "@MonthOfAnnualPay", DbType.Int32, r.MonthOfAnnualPay.Value)
                        If r.ProcessDate.Updated Then db.AddInParameter(dbcmd, "@ProcessDate", DbType.Int32, r.ProcessDate.Value)
                        If r.SalaryDate.Updated Then db.AddInParameter(dbcmd, "@SalaryDate", DbType.Date, IIf(IsDateTimeNull(r.SalaryDate.Value), Convert.ToDateTime("1900/1/1"), r.SalaryDate.Value))
                        If r.DelayFlag.Updated Then db.AddInParameter(dbcmd, "@DelayFlag", DbType.String, r.DelayFlag.Value)
                        If r.TaxOption.Updated Then db.AddInParameter(dbcmd, "@TaxOption", DbType.String, r.TaxOption.Value)
                        If r.NotFesFlag.Updated Then db.AddInParameter(dbcmd, "@NotFesFlag", DbType.String, r.NotFesFlag.Value)
                        If r.CreateUserComp.Updated Then db.AddInParameter(dbcmd, "@CreateUserComp", DbType.String, r.CreateUserComp.Value)
                        If r.CreateUserID.Updated Then db.AddInParameter(dbcmd, "@CreateUserID", DbType.String, r.CreateUserID.Value)
                        If r.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
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

        Public Function Update(ByVal SalaryDataRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In SalaryDataRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update SalaryData Set")
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
                If r.TaxRearNo.Updated Then db.AddInParameter(dbcmd, "@TaxRearNo", DbType.Int32, r.TaxRearNo.Value)
                If r.FeeShareComp.Updated Then db.AddInParameter(dbcmd, "@FeeShareComp", DbType.String, r.FeeShareComp.Value)
                If r.FeeShareDept.Updated Then db.AddInParameter(dbcmd, "@FeeShareDept", DbType.String, r.FeeShareDept.Value)
                If r.SalaryPaid.Updated Then db.AddInParameter(dbcmd, "@SalaryPaid", DbType.String, r.SalaryPaid.Value)
                If r.SalaryFlag.Updated Then db.AddInParameter(dbcmd, "@SalaryFlag", DbType.String, r.SalaryFlag.Value)
                If r.RetireFlag.Updated Then db.AddInParameter(dbcmd, "@RetireFlag", DbType.String, r.RetireFlag.Value)
                If r.WelfareFlag.Updated Then db.AddInParameter(dbcmd, "@WelfareFlag", DbType.String, r.WelfareFlag.Value)
                If r.UnityAccountFlag.Updated Then db.AddInParameter(dbcmd, "@UnityAccountFlag", DbType.String, r.UnityAccountFlag.Value)
                If r.DeductTaxRatio.Updated Then db.AddInParameter(dbcmd, "@DeductTaxRatio", DbType.Int32, r.DeductTaxRatio.Value)
                If r.DeductTaxRatioFlag.Updated Then db.AddInParameter(dbcmd, "@DeductTaxRatioFlag", DbType.String, r.DeductTaxRatioFlag.Value)
                If r.MonthOfAnnualPay.Updated Then db.AddInParameter(dbcmd, "@MonthOfAnnualPay", DbType.Int32, r.MonthOfAnnualPay.Value)
                If r.ProcessDate.Updated Then db.AddInParameter(dbcmd, "@ProcessDate", DbType.Int32, r.ProcessDate.Value)
                If r.SalaryDate.Updated Then db.AddInParameter(dbcmd, "@SalaryDate", DbType.Date, IIf(IsDateTimeNull(r.SalaryDate.Value), Convert.ToDateTime("1900/1/1"), r.SalaryDate.Value))
                If r.DelayFlag.Updated Then db.AddInParameter(dbcmd, "@DelayFlag", DbType.String, r.DelayFlag.Value)
                If r.TaxOption.Updated Then db.AddInParameter(dbcmd, "@TaxOption", DbType.String, r.TaxOption.Value)
                If r.NotFesFlag.Updated Then db.AddInParameter(dbcmd, "@NotFesFlag", DbType.String, r.NotFesFlag.Value)
                If r.CreateUserComp.Updated Then db.AddInParameter(dbcmd, "@CreateUserComp", DbType.String, r.CreateUserComp.Value)
                If r.CreateUserID.Updated Then db.AddInParameter(dbcmd, "@CreateUserID", DbType.String, r.CreateUserID.Value)
                If r.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal SalaryDataRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SalaryData")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SalaryDataRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, SalaryDataRow.EmpID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal SalaryDataRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From SalaryData")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SalaryDataRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, SalaryDataRow.EmpID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SalaryData")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal SalaryDataRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SalaryData")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, TaxRearNo, FeeShareComp, FeeShareDept, SalaryPaid, SalaryFlag, RetireFlag,")
            strSQL.AppendLine("    WelfareFlag, UnityAccountFlag, DeductTaxRatio, DeductTaxRatioFlag, MonthOfAnnualPay, ProcessDate,")
            strSQL.AppendLine("    SalaryDate, DelayFlag, TaxOption, NotFesFlag, CreateUserComp, CreateUserID, CreateDate,")
            strSQL.AppendLine("    LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @TaxRearNo, @FeeShareComp, @FeeShareDept, @SalaryPaid, @SalaryFlag, @RetireFlag,")
            strSQL.AppendLine("    @WelfareFlag, @UnityAccountFlag, @DeductTaxRatio, @DeductTaxRatioFlag, @MonthOfAnnualPay, @ProcessDate,")
            strSQL.AppendLine("    @SalaryDate, @DelayFlag, @TaxOption, @NotFesFlag, @CreateUserComp, @CreateUserID, @CreateDate,")
            strSQL.AppendLine("    @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SalaryDataRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, SalaryDataRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@TaxRearNo", DbType.Int32, SalaryDataRow.TaxRearNo.Value)
            db.AddInParameter(dbcmd, "@FeeShareComp", DbType.String, SalaryDataRow.FeeShareComp.Value)
            db.AddInParameter(dbcmd, "@FeeShareDept", DbType.String, SalaryDataRow.FeeShareDept.Value)
            db.AddInParameter(dbcmd, "@SalaryPaid", DbType.String, SalaryDataRow.SalaryPaid.Value)
            db.AddInParameter(dbcmd, "@SalaryFlag", DbType.String, SalaryDataRow.SalaryFlag.Value)
            db.AddInParameter(dbcmd, "@RetireFlag", DbType.String, SalaryDataRow.RetireFlag.Value)
            db.AddInParameter(dbcmd, "@WelfareFlag", DbType.String, SalaryDataRow.WelfareFlag.Value)
            db.AddInParameter(dbcmd, "@UnityAccountFlag", DbType.String, SalaryDataRow.UnityAccountFlag.Value)
            db.AddInParameter(dbcmd, "@DeductTaxRatio", DbType.Int32, SalaryDataRow.DeductTaxRatio.Value)
            db.AddInParameter(dbcmd, "@DeductTaxRatioFlag", DbType.String, SalaryDataRow.DeductTaxRatioFlag.Value)
            db.AddInParameter(dbcmd, "@MonthOfAnnualPay", DbType.Int32, SalaryDataRow.MonthOfAnnualPay.Value)
            db.AddInParameter(dbcmd, "@ProcessDate", DbType.Int32, SalaryDataRow.ProcessDate.Value)
            db.AddInParameter(dbcmd, "@SalaryDate", DbType.Date, IIf(IsDateTimeNull(SalaryDataRow.SalaryDate.Value), Convert.ToDateTime("1900/1/1"), SalaryDataRow.SalaryDate.Value))
            db.AddInParameter(dbcmd, "@DelayFlag", DbType.String, SalaryDataRow.DelayFlag.Value)
            db.AddInParameter(dbcmd, "@TaxOption", DbType.String, SalaryDataRow.TaxOption.Value)
            db.AddInParameter(dbcmd, "@NotFesFlag", DbType.String, SalaryDataRow.NotFesFlag.Value)
            db.AddInParameter(dbcmd, "@CreateUserComp", DbType.String, SalaryDataRow.CreateUserComp.Value)
            db.AddInParameter(dbcmd, "@CreateUserID", DbType.String, SalaryDataRow.CreateUserID.Value)
            db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SalaryDataRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SalaryDataRow.CreateDate.Value))
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, SalaryDataRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SalaryDataRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SalaryDataRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SalaryDataRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal SalaryDataRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SalaryData")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, TaxRearNo, FeeShareComp, FeeShareDept, SalaryPaid, SalaryFlag, RetireFlag,")
            strSQL.AppendLine("    WelfareFlag, UnityAccountFlag, DeductTaxRatio, DeductTaxRatioFlag, MonthOfAnnualPay, ProcessDate,")
            strSQL.AppendLine("    SalaryDate, DelayFlag, TaxOption, NotFesFlag, CreateUserComp, CreateUserID, CreateDate,")
            strSQL.AppendLine("    LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @TaxRearNo, @FeeShareComp, @FeeShareDept, @SalaryPaid, @SalaryFlag, @RetireFlag,")
            strSQL.AppendLine("    @WelfareFlag, @UnityAccountFlag, @DeductTaxRatio, @DeductTaxRatioFlag, @MonthOfAnnualPay, @ProcessDate,")
            strSQL.AppendLine("    @SalaryDate, @DelayFlag, @TaxOption, @NotFesFlag, @CreateUserComp, @CreateUserID, @CreateDate,")
            strSQL.AppendLine("    @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SalaryDataRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, SalaryDataRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@TaxRearNo", DbType.Int32, SalaryDataRow.TaxRearNo.Value)
            db.AddInParameter(dbcmd, "@FeeShareComp", DbType.String, SalaryDataRow.FeeShareComp.Value)
            db.AddInParameter(dbcmd, "@FeeShareDept", DbType.String, SalaryDataRow.FeeShareDept.Value)
            db.AddInParameter(dbcmd, "@SalaryPaid", DbType.String, SalaryDataRow.SalaryPaid.Value)
            db.AddInParameter(dbcmd, "@SalaryFlag", DbType.String, SalaryDataRow.SalaryFlag.Value)
            db.AddInParameter(dbcmd, "@RetireFlag", DbType.String, SalaryDataRow.RetireFlag.Value)
            db.AddInParameter(dbcmd, "@WelfareFlag", DbType.String, SalaryDataRow.WelfareFlag.Value)
            db.AddInParameter(dbcmd, "@UnityAccountFlag", DbType.String, SalaryDataRow.UnityAccountFlag.Value)
            db.AddInParameter(dbcmd, "@DeductTaxRatio", DbType.Int32, SalaryDataRow.DeductTaxRatio.Value)
            db.AddInParameter(dbcmd, "@DeductTaxRatioFlag", DbType.String, SalaryDataRow.DeductTaxRatioFlag.Value)
            db.AddInParameter(dbcmd, "@MonthOfAnnualPay", DbType.Int32, SalaryDataRow.MonthOfAnnualPay.Value)
            db.AddInParameter(dbcmd, "@ProcessDate", DbType.Int32, SalaryDataRow.ProcessDate.Value)
            db.AddInParameter(dbcmd, "@SalaryDate", DbType.Date, IIf(IsDateTimeNull(SalaryDataRow.SalaryDate.Value), Convert.ToDateTime("1900/1/1"), SalaryDataRow.SalaryDate.Value))
            db.AddInParameter(dbcmd, "@DelayFlag", DbType.String, SalaryDataRow.DelayFlag.Value)
            db.AddInParameter(dbcmd, "@TaxOption", DbType.String, SalaryDataRow.TaxOption.Value)
            db.AddInParameter(dbcmd, "@NotFesFlag", DbType.String, SalaryDataRow.NotFesFlag.Value)
            db.AddInParameter(dbcmd, "@CreateUserComp", DbType.String, SalaryDataRow.CreateUserComp.Value)
            db.AddInParameter(dbcmd, "@CreateUserID", DbType.String, SalaryDataRow.CreateUserID.Value)
            db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(SalaryDataRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), SalaryDataRow.CreateDate.Value))
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, SalaryDataRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SalaryDataRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SalaryDataRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SalaryDataRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal SalaryDataRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SalaryData")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, TaxRearNo, FeeShareComp, FeeShareDept, SalaryPaid, SalaryFlag, RetireFlag,")
            strSQL.AppendLine("    WelfareFlag, UnityAccountFlag, DeductTaxRatio, DeductTaxRatioFlag, MonthOfAnnualPay, ProcessDate,")
            strSQL.AppendLine("    SalaryDate, DelayFlag, TaxOption, NotFesFlag, CreateUserComp, CreateUserID, CreateDate,")
            strSQL.AppendLine("    LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @TaxRearNo, @FeeShareComp, @FeeShareDept, @SalaryPaid, @SalaryFlag, @RetireFlag,")
            strSQL.AppendLine("    @WelfareFlag, @UnityAccountFlag, @DeductTaxRatio, @DeductTaxRatioFlag, @MonthOfAnnualPay, @ProcessDate,")
            strSQL.AppendLine("    @SalaryDate, @DelayFlag, @TaxOption, @NotFesFlag, @CreateUserComp, @CreateUserID, @CreateDate,")
            strSQL.AppendLine("    @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SalaryDataRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@TaxRearNo", DbType.Int32, r.TaxRearNo.Value)
                        db.AddInParameter(dbcmd, "@FeeShareComp", DbType.String, r.FeeShareComp.Value)
                        db.AddInParameter(dbcmd, "@FeeShareDept", DbType.String, r.FeeShareDept.Value)
                        db.AddInParameter(dbcmd, "@SalaryPaid", DbType.String, r.SalaryPaid.Value)
                        db.AddInParameter(dbcmd, "@SalaryFlag", DbType.String, r.SalaryFlag.Value)
                        db.AddInParameter(dbcmd, "@RetireFlag", DbType.String, r.RetireFlag.Value)
                        db.AddInParameter(dbcmd, "@WelfareFlag", DbType.String, r.WelfareFlag.Value)
                        db.AddInParameter(dbcmd, "@UnityAccountFlag", DbType.String, r.UnityAccountFlag.Value)
                        db.AddInParameter(dbcmd, "@DeductTaxRatio", DbType.Int32, r.DeductTaxRatio.Value)
                        db.AddInParameter(dbcmd, "@DeductTaxRatioFlag", DbType.String, r.DeductTaxRatioFlag.Value)
                        db.AddInParameter(dbcmd, "@MonthOfAnnualPay", DbType.Int32, r.MonthOfAnnualPay.Value)
                        db.AddInParameter(dbcmd, "@ProcessDate", DbType.Int32, r.ProcessDate.Value)
                        db.AddInParameter(dbcmd, "@SalaryDate", DbType.Date, IIf(IsDateTimeNull(r.SalaryDate.Value), Convert.ToDateTime("1900/1/1"), r.SalaryDate.Value))
                        db.AddInParameter(dbcmd, "@DelayFlag", DbType.String, r.DelayFlag.Value)
                        db.AddInParameter(dbcmd, "@TaxOption", DbType.String, r.TaxOption.Value)
                        db.AddInParameter(dbcmd, "@NotFesFlag", DbType.String, r.NotFesFlag.Value)
                        db.AddInParameter(dbcmd, "@CreateUserComp", DbType.String, r.CreateUserComp.Value)
                        db.AddInParameter(dbcmd, "@CreateUserID", DbType.String, r.CreateUserID.Value)
                        db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
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

        Public Function Insert(ByVal SalaryDataRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SalaryData")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, TaxRearNo, FeeShareComp, FeeShareDept, SalaryPaid, SalaryFlag, RetireFlag,")
            strSQL.AppendLine("    WelfareFlag, UnityAccountFlag, DeductTaxRatio, DeductTaxRatioFlag, MonthOfAnnualPay, ProcessDate,")
            strSQL.AppendLine("    SalaryDate, DelayFlag, TaxOption, NotFesFlag, CreateUserComp, CreateUserID, CreateDate,")
            strSQL.AppendLine("    LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @TaxRearNo, @FeeShareComp, @FeeShareDept, @SalaryPaid, @SalaryFlag, @RetireFlag,")
            strSQL.AppendLine("    @WelfareFlag, @UnityAccountFlag, @DeductTaxRatio, @DeductTaxRatioFlag, @MonthOfAnnualPay, @ProcessDate,")
            strSQL.AppendLine("    @SalaryDate, @DelayFlag, @TaxOption, @NotFesFlag, @CreateUserComp, @CreateUserID, @CreateDate,")
            strSQL.AppendLine("    @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SalaryDataRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@TaxRearNo", DbType.Int32, r.TaxRearNo.Value)
                db.AddInParameter(dbcmd, "@FeeShareComp", DbType.String, r.FeeShareComp.Value)
                db.AddInParameter(dbcmd, "@FeeShareDept", DbType.String, r.FeeShareDept.Value)
                db.AddInParameter(dbcmd, "@SalaryPaid", DbType.String, r.SalaryPaid.Value)
                db.AddInParameter(dbcmd, "@SalaryFlag", DbType.String, r.SalaryFlag.Value)
                db.AddInParameter(dbcmd, "@RetireFlag", DbType.String, r.RetireFlag.Value)
                db.AddInParameter(dbcmd, "@WelfareFlag", DbType.String, r.WelfareFlag.Value)
                db.AddInParameter(dbcmd, "@UnityAccountFlag", DbType.String, r.UnityAccountFlag.Value)
                db.AddInParameter(dbcmd, "@DeductTaxRatio", DbType.Int32, r.DeductTaxRatio.Value)
                db.AddInParameter(dbcmd, "@DeductTaxRatioFlag", DbType.String, r.DeductTaxRatioFlag.Value)
                db.AddInParameter(dbcmd, "@MonthOfAnnualPay", DbType.Int32, r.MonthOfAnnualPay.Value)
                db.AddInParameter(dbcmd, "@ProcessDate", DbType.Int32, r.ProcessDate.Value)
                db.AddInParameter(dbcmd, "@SalaryDate", DbType.Date, IIf(IsDateTimeNull(r.SalaryDate.Value), Convert.ToDateTime("1900/1/1"), r.SalaryDate.Value))
                db.AddInParameter(dbcmd, "@DelayFlag", DbType.String, r.DelayFlag.Value)
                db.AddInParameter(dbcmd, "@TaxOption", DbType.String, r.TaxOption.Value)
                db.AddInParameter(dbcmd, "@NotFesFlag", DbType.String, r.NotFesFlag.Value)
                db.AddInParameter(dbcmd, "@CreateUserComp", DbType.String, r.CreateUserComp.Value)
                db.AddInParameter(dbcmd, "@CreateUserID", DbType.String, r.CreateUserID.Value)
                db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
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

