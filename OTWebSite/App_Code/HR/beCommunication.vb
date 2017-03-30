'****************************************************************
' Table:Communication
' Created Date: 2016.05.13
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beCommunication
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "IDNo", "RegAddrCode", "RegAddr", "CommAddr", "CommTelCode1", "CommTel1", "CommTelCode2", "CommTel2", "CommTelCode3", "CommTel3" _
                                    , "CommTelCode4", "CommTel4", "RelName", "RelTel", "RelRelation", "EMail", "CompTelCode", "AreaCode", "CompTel", "ExtNo", "LastChgComp" _
                                    , "LastChgID", "LastChgDate", "RegCityCode", "CommCityCode", "CommAddrCode", "Email2" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(Date), GetType(String), GetType(String), GetType(String), GetType(String) }
        Private m_PrimaryFields As String() = { "IDNo" }

        Public ReadOnly Property Rows() As beCommunication.Rows 
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
        Public Sub Transfer2Row(CommunicationTable As DataTable)
            For Each dr As DataRow In CommunicationTable.Rows
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

                dr(m_Rows(i).IDNo.FieldName) = m_Rows(i).IDNo.Value
                dr(m_Rows(i).RegAddrCode.FieldName) = m_Rows(i).RegAddrCode.Value
                dr(m_Rows(i).RegAddr.FieldName) = m_Rows(i).RegAddr.Value
                dr(m_Rows(i).CommAddr.FieldName) = m_Rows(i).CommAddr.Value
                dr(m_Rows(i).CommTelCode1.FieldName) = m_Rows(i).CommTelCode1.Value
                dr(m_Rows(i).CommTel1.FieldName) = m_Rows(i).CommTel1.Value
                dr(m_Rows(i).CommTelCode2.FieldName) = m_Rows(i).CommTelCode2.Value
                dr(m_Rows(i).CommTel2.FieldName) = m_Rows(i).CommTel2.Value
                dr(m_Rows(i).CommTelCode3.FieldName) = m_Rows(i).CommTelCode3.Value
                dr(m_Rows(i).CommTel3.FieldName) = m_Rows(i).CommTel3.Value
                dr(m_Rows(i).CommTelCode4.FieldName) = m_Rows(i).CommTelCode4.Value
                dr(m_Rows(i).CommTel4.FieldName) = m_Rows(i).CommTel4.Value
                dr(m_Rows(i).RelName.FieldName) = m_Rows(i).RelName.Value
                dr(m_Rows(i).RelTel.FieldName) = m_Rows(i).RelTel.Value
                dr(m_Rows(i).RelRelation.FieldName) = m_Rows(i).RelRelation.Value
                dr(m_Rows(i).EMail.FieldName) = m_Rows(i).EMail.Value
                dr(m_Rows(i).CompTelCode.FieldName) = m_Rows(i).CompTelCode.Value
                dr(m_Rows(i).AreaCode.FieldName) = m_Rows(i).AreaCode.Value
                dr(m_Rows(i).CompTel.FieldName) = m_Rows(i).CompTel.Value
                dr(m_Rows(i).ExtNo.FieldName) = m_Rows(i).ExtNo.Value
                dr(m_Rows(i).LastChgComp.FieldName) = m_Rows(i).LastChgComp.Value
                dr(m_Rows(i).LastChgID.FieldName) = m_Rows(i).LastChgID.Value
                dr(m_Rows(i).LastChgDate.FieldName) = m_Rows(i).LastChgDate.Value
                dr(m_Rows(i).RegCityCode.FieldName) = m_Rows(i).RegCityCode.Value
                dr(m_Rows(i).CommCityCode.FieldName) = m_Rows(i).CommCityCode.Value
                dr(m_Rows(i).CommAddrCode.FieldName) = m_Rows(i).CommAddrCode.Value
                dr(m_Rows(i).Email2.FieldName) = m_Rows(i).Email2.Value

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

        Public Sub Add(CommunicationRow As Row)
            m_Rows.Add(CommunicationRow)
        End Sub

        Public Sub Remove(CommunicationRow As Row)
            If m_Rows.IndexOf(CommunicationRow) >= 0 Then
                m_Rows.Remove(CommunicationRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_IDNo As Field(Of String) = new Field(Of String)("IDNo", true)
        Private FI_RegAddrCode As Field(Of String) = new Field(Of String)("RegAddrCode", true)
        Private FI_RegAddr As Field(Of String) = new Field(Of String)("RegAddr", true)
        Private FI_CommAddr As Field(Of String) = new Field(Of String)("CommAddr", true)
        Private FI_CommTelCode1 As Field(Of String) = new Field(Of String)("CommTelCode1", true)
        Private FI_CommTel1 As Field(Of String) = new Field(Of String)("CommTel1", true)
        Private FI_CommTelCode2 As Field(Of String) = new Field(Of String)("CommTelCode2", true)
        Private FI_CommTel2 As Field(Of String) = new Field(Of String)("CommTel2", true)
        Private FI_CommTelCode3 As Field(Of String) = new Field(Of String)("CommTelCode3", true)
        Private FI_CommTel3 As Field(Of String) = new Field(Of String)("CommTel3", true)
        Private FI_CommTelCode4 As Field(Of String) = new Field(Of String)("CommTelCode4", true)
        Private FI_CommTel4 As Field(Of String) = new Field(Of String)("CommTel4", true)
        Private FI_RelName As Field(Of String) = new Field(Of String)("RelName", true)
        Private FI_RelTel As Field(Of String) = new Field(Of String)("RelTel", true)
        Private FI_RelRelation As Field(Of String) = new Field(Of String)("RelRelation", true)
        Private FI_EMail As Field(Of String) = new Field(Of String)("EMail", true)
        Private FI_CompTelCode As Field(Of String) = new Field(Of String)("CompTelCode", true)
        Private FI_AreaCode As Field(Of String) = new Field(Of String)("AreaCode", true)
        Private FI_CompTel As Field(Of String) = new Field(Of String)("CompTel", true)
        Private FI_ExtNo As Field(Of String) = new Field(Of String)("ExtNo", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private FI_RegCityCode As Field(Of String) = new Field(Of String)("RegCityCode", true)
        Private FI_CommCityCode As Field(Of String) = new Field(Of String)("CommCityCode", true)
        Private FI_CommAddrCode As Field(Of String) = new Field(Of String)("CommAddrCode", true)
        Private FI_Email2 As Field(Of String) = new Field(Of String)("Email2", true)
        Private m_FieldNames As String() = { "IDNo", "RegAddrCode", "RegAddr", "CommAddr", "CommTelCode1", "CommTel1", "CommTelCode2", "CommTel2", "CommTelCode3", "CommTel3" _
                                    , "CommTelCode4", "CommTel4", "RelName", "RelTel", "RelRelation", "EMail", "CompTelCode", "AreaCode", "CompTel", "ExtNo", "LastChgComp" _
                                    , "LastChgID", "LastChgDate", "RegCityCode", "CommCityCode", "CommAddrCode", "Email2" }
        Private m_PrimaryFields As String() = { "IDNo" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "IDNo"
                    Return FI_IDNo.Value
                Case "RegAddrCode"
                    Return FI_RegAddrCode.Value
                Case "RegAddr"
                    Return FI_RegAddr.Value
                Case "CommAddr"
                    Return FI_CommAddr.Value
                Case "CommTelCode1"
                    Return FI_CommTelCode1.Value
                Case "CommTel1"
                    Return FI_CommTel1.Value
                Case "CommTelCode2"
                    Return FI_CommTelCode2.Value
                Case "CommTel2"
                    Return FI_CommTel2.Value
                Case "CommTelCode3"
                    Return FI_CommTelCode3.Value
                Case "CommTel3"
                    Return FI_CommTel3.Value
                Case "CommTelCode4"
                    Return FI_CommTelCode4.Value
                Case "CommTel4"
                    Return FI_CommTel4.Value
                Case "RelName"
                    Return FI_RelName.Value
                Case "RelTel"
                    Return FI_RelTel.Value
                Case "RelRelation"
                    Return FI_RelRelation.Value
                Case "EMail"
                    Return FI_EMail.Value
                Case "CompTelCode"
                    Return FI_CompTelCode.Value
                Case "AreaCode"
                    Return FI_AreaCode.Value
                Case "CompTel"
                    Return FI_CompTel.Value
                Case "ExtNo"
                    Return FI_ExtNo.Value
                Case "LastChgComp"
                    Return FI_LastChgComp.Value
                Case "LastChgID"
                    Return FI_LastChgID.Value
                Case "LastChgDate"
                    Return FI_LastChgDate.Value
                Case "RegCityCode"
                    Return FI_RegCityCode.Value
                Case "CommCityCode"
                    Return FI_CommCityCode.Value
                Case "CommAddrCode"
                    Return FI_CommAddrCode.Value
                Case "Email2"
                    Return FI_Email2.Value
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Sub SetFieldValue(ByVal fieldName As String, ByVal value As Object)
            Select Case fieldName
                Case "IDNo"
                    FI_IDNo.SetValue(value)
                Case "RegAddrCode"
                    FI_RegAddrCode.SetValue(value)
                Case "RegAddr"
                    FI_RegAddr.SetValue(value)
                Case "CommAddr"
                    FI_CommAddr.SetValue(value)
                Case "CommTelCode1"
                    FI_CommTelCode1.SetValue(value)
                Case "CommTel1"
                    FI_CommTel1.SetValue(value)
                Case "CommTelCode2"
                    FI_CommTelCode2.SetValue(value)
                Case "CommTel2"
                    FI_CommTel2.SetValue(value)
                Case "CommTelCode3"
                    FI_CommTelCode3.SetValue(value)
                Case "CommTel3"
                    FI_CommTel3.SetValue(value)
                Case "CommTelCode4"
                    FI_CommTelCode4.SetValue(value)
                Case "CommTel4"
                    FI_CommTel4.SetValue(value)
                Case "RelName"
                    FI_RelName.SetValue(value)
                Case "RelTel"
                    FI_RelTel.SetValue(value)
                Case "RelRelation"
                    FI_RelRelation.SetValue(value)
                Case "EMail"
                    FI_EMail.SetValue(value)
                Case "CompTelCode"
                    FI_CompTelCode.SetValue(value)
                Case "AreaCode"
                    FI_AreaCode.SetValue(value)
                Case "CompTel"
                    FI_CompTel.SetValue(value)
                Case "ExtNo"
                    FI_ExtNo.SetValue(value)
                Case "LastChgComp"
                    FI_LastChgComp.SetValue(value)
                Case "LastChgID"
                    FI_LastChgID.SetValue(value)
                Case "LastChgDate"
                    FI_LastChgDate.SetValue(value)
                Case "RegCityCode"
                    FI_RegCityCode.SetValue(value)
                Case "CommCityCode"
                    FI_CommCityCode.SetValue(value)
                Case "CommAddrCode"
                    FI_CommAddrCode.SetValue(value)
                Case "Email2"
                    FI_Email2.SetValue(value)
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
                Case "IDNo"
                    return FI_IDNo.Updated
                Case "RegAddrCode"
                    return FI_RegAddrCode.Updated
                Case "RegAddr"
                    return FI_RegAddr.Updated
                Case "CommAddr"
                    return FI_CommAddr.Updated
                Case "CommTelCode1"
                    return FI_CommTelCode1.Updated
                Case "CommTel1"
                    return FI_CommTel1.Updated
                Case "CommTelCode2"
                    return FI_CommTelCode2.Updated
                Case "CommTel2"
                    return FI_CommTel2.Updated
                Case "CommTelCode3"
                    return FI_CommTelCode3.Updated
                Case "CommTel3"
                    return FI_CommTel3.Updated
                Case "CommTelCode4"
                    return FI_CommTelCode4.Updated
                Case "CommTel4"
                    return FI_CommTel4.Updated
                Case "RelName"
                    return FI_RelName.Updated
                Case "RelTel"
                    return FI_RelTel.Updated
                Case "RelRelation"
                    return FI_RelRelation.Updated
                Case "EMail"
                    return FI_EMail.Updated
                Case "CompTelCode"
                    return FI_CompTelCode.Updated
                Case "AreaCode"
                    return FI_AreaCode.Updated
                Case "CompTel"
                    return FI_CompTel.Updated
                Case "ExtNo"
                    return FI_ExtNo.Updated
                Case "LastChgComp"
                    return FI_LastChgComp.Updated
                Case "LastChgID"
                    return FI_LastChgID.Updated
                Case "LastChgDate"
                    return FI_LastChgDate.Updated
                Case "RegCityCode"
                    return FI_RegCityCode.Updated
                Case "CommCityCode"
                    return FI_CommCityCode.Updated
                Case "CommAddrCode"
                    return FI_CommAddrCode.Updated
                Case "Email2"
                    return FI_Email2.Updated
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public Function CreateUpdateSQL(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "IDNo"
                    return FI_IDNo.CreateUpdateSQL
                Case "RegAddrCode"
                    return FI_RegAddrCode.CreateUpdateSQL
                Case "RegAddr"
                    return FI_RegAddr.CreateUpdateSQL
                Case "CommAddr"
                    return FI_CommAddr.CreateUpdateSQL
                Case "CommTelCode1"
                    return FI_CommTelCode1.CreateUpdateSQL
                Case "CommTel1"
                    return FI_CommTel1.CreateUpdateSQL
                Case "CommTelCode2"
                    return FI_CommTelCode2.CreateUpdateSQL
                Case "CommTel2"
                    return FI_CommTel2.CreateUpdateSQL
                Case "CommTelCode3"
                    return FI_CommTelCode3.CreateUpdateSQL
                Case "CommTel3"
                    return FI_CommTel3.CreateUpdateSQL
                Case "CommTelCode4"
                    return FI_CommTelCode4.CreateUpdateSQL
                Case "CommTel4"
                    return FI_CommTel4.CreateUpdateSQL
                Case "RelName"
                    return FI_RelName.CreateUpdateSQL
                Case "RelTel"
                    return FI_RelTel.CreateUpdateSQL
                Case "RelRelation"
                    return FI_RelRelation.CreateUpdateSQL
                Case "EMail"
                    return FI_EMail.CreateUpdateSQL
                Case "CompTelCode"
                    return FI_CompTelCode.CreateUpdateSQL
                Case "AreaCode"
                    return FI_AreaCode.CreateUpdateSQL
                Case "CompTel"
                    return FI_CompTel.CreateUpdateSQL
                Case "ExtNo"
                    return FI_ExtNo.CreateUpdateSQL
                Case "LastChgComp"
                    return FI_LastChgComp.CreateUpdateSQL
                Case "LastChgID"
                    return FI_LastChgID.CreateUpdateSQL
                Case "LastChgDate"
                    return FI_LastChgDate.CreateUpdateSQL
                Case "RegCityCode"
                    return FI_RegCityCode.CreateUpdateSQL
                Case "CommCityCode"
                    return FI_CommCityCode.CreateUpdateSQL
                Case "CommAddrCode"
                    return FI_CommAddrCode.CreateUpdateSQL
                Case "Email2"
                    return FI_Email2.CreateUpdateSQL
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
            FI_IDNo.SetInitValue("")
            FI_RegAddrCode.SetInitValue("")
            FI_RegAddr.SetInitValue("")
            FI_CommAddr.SetInitValue("")
            FI_CommTelCode1.SetInitValue("")
            FI_CommTel1.SetInitValue("")
            FI_CommTelCode2.SetInitValue("")
            FI_CommTel2.SetInitValue("")
            FI_CommTelCode3.SetInitValue("")
            FI_CommTel3.SetInitValue("")
            FI_CommTelCode4.SetInitValue("")
            FI_CommTel4.SetInitValue("")
            FI_RelName.SetInitValue("")
            FI_RelTel.SetInitValue("")
            FI_RelRelation.SetInitValue("")
            FI_EMail.SetInitValue("")
            FI_CompTelCode.SetInitValue("")
            FI_AreaCode.SetInitValue("")
            FI_CompTel.SetInitValue("")
            FI_ExtNo.SetInitValue("")
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_RegCityCode.SetInitValue("")
            FI_CommCityCode.SetInitValue("")
            FI_CommAddrCode.SetInitValue("")
            FI_Email2.SetInitValue("")
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_IDNo.SetInitValue(dr("IDNo"))
            FI_RegAddrCode.SetInitValue(dr("RegAddrCode"))
            FI_RegAddr.SetInitValue(dr("RegAddr"))
            FI_CommAddr.SetInitValue(dr("CommAddr"))
            FI_CommTelCode1.SetInitValue(dr("CommTelCode1"))
            FI_CommTel1.SetInitValue(dr("CommTel1"))
            FI_CommTelCode2.SetInitValue(dr("CommTelCode2"))
            FI_CommTel2.SetInitValue(dr("CommTel2"))
            FI_CommTelCode3.SetInitValue(dr("CommTelCode3"))
            FI_CommTel3.SetInitValue(dr("CommTel3"))
            FI_CommTelCode4.SetInitValue(dr("CommTelCode4"))
            FI_CommTel4.SetInitValue(dr("CommTel4"))
            FI_RelName.SetInitValue(dr("RelName"))
            FI_RelTel.SetInitValue(dr("RelTel"))
            FI_RelRelation.SetInitValue(dr("RelRelation"))
            FI_EMail.SetInitValue(dr("EMail"))
            FI_CompTelCode.SetInitValue(dr("CompTelCode"))
            FI_AreaCode.SetInitValue(dr("AreaCode"))
            FI_CompTel.SetInitValue(dr("CompTel"))
            FI_ExtNo.SetInitValue(dr("ExtNo"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))
            FI_RegCityCode.SetInitValue(dr("RegCityCode"))
            FI_CommCityCode.SetInitValue(dr("CommCityCode"))
            FI_CommAddrCode.SetInitValue(dr("CommAddrCode"))
            FI_Email2.SetInitValue(dr("Email2"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_IDNo.Updated = False
            FI_RegAddrCode.Updated = False
            FI_RegAddr.Updated = False
            FI_CommAddr.Updated = False
            FI_CommTelCode1.Updated = False
            FI_CommTel1.Updated = False
            FI_CommTelCode2.Updated = False
            FI_CommTel2.Updated = False
            FI_CommTelCode3.Updated = False
            FI_CommTel3.Updated = False
            FI_CommTelCode4.Updated = False
            FI_CommTel4.Updated = False
            FI_RelName.Updated = False
            FI_RelTel.Updated = False
            FI_RelRelation.Updated = False
            FI_EMail.Updated = False
            FI_CompTelCode.Updated = False
            FI_AreaCode.Updated = False
            FI_CompTel.Updated = False
            FI_ExtNo.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
            FI_RegCityCode.Updated = False
            FI_CommCityCode.Updated = False
            FI_CommAddrCode.Updated = False
            FI_Email2.Updated = False
        End Sub

        Public ReadOnly Property IDNo As Field(Of String) 
            Get
                Return FI_IDNo
            End Get
        End Property

        Public ReadOnly Property RegAddrCode As Field(Of String) 
            Get
                Return FI_RegAddrCode
            End Get
        End Property

        Public ReadOnly Property RegAddr As Field(Of String) 
            Get
                Return FI_RegAddr
            End Get
        End Property

        Public ReadOnly Property CommAddr As Field(Of String) 
            Get
                Return FI_CommAddr
            End Get
        End Property

        Public ReadOnly Property CommTelCode1 As Field(Of String) 
            Get
                Return FI_CommTelCode1
            End Get
        End Property

        Public ReadOnly Property CommTel1 As Field(Of String) 
            Get
                Return FI_CommTel1
            End Get
        End Property

        Public ReadOnly Property CommTelCode2 As Field(Of String) 
            Get
                Return FI_CommTelCode2
            End Get
        End Property

        Public ReadOnly Property CommTel2 As Field(Of String) 
            Get
                Return FI_CommTel2
            End Get
        End Property

        Public ReadOnly Property CommTelCode3 As Field(Of String) 
            Get
                Return FI_CommTelCode3
            End Get
        End Property

        Public ReadOnly Property CommTel3 As Field(Of String) 
            Get
                Return FI_CommTel3
            End Get
        End Property

        Public ReadOnly Property CommTelCode4 As Field(Of String) 
            Get
                Return FI_CommTelCode4
            End Get
        End Property

        Public ReadOnly Property CommTel4 As Field(Of String) 
            Get
                Return FI_CommTel4
            End Get
        End Property

        Public ReadOnly Property RelName As Field(Of String) 
            Get
                Return FI_RelName
            End Get
        End Property

        Public ReadOnly Property RelTel As Field(Of String) 
            Get
                Return FI_RelTel
            End Get
        End Property

        Public ReadOnly Property RelRelation As Field(Of String) 
            Get
                Return FI_RelRelation
            End Get
        End Property

        Public ReadOnly Property EMail As Field(Of String) 
            Get
                Return FI_EMail
            End Get
        End Property

        Public ReadOnly Property CompTelCode As Field(Of String) 
            Get
                Return FI_CompTelCode
            End Get
        End Property

        Public ReadOnly Property AreaCode As Field(Of String) 
            Get
                Return FI_AreaCode
            End Get
        End Property

        Public ReadOnly Property CompTel As Field(Of String) 
            Get
                Return FI_CompTel
            End Get
        End Property

        Public ReadOnly Property ExtNo As Field(Of String) 
            Get
                Return FI_ExtNo
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

        Public ReadOnly Property RegCityCode As Field(Of String) 
            Get
                Return FI_RegCityCode
            End Get
        End Property

        Public ReadOnly Property CommCityCode As Field(Of String) 
            Get
                Return FI_CommCityCode
            End Get
        End Property

        Public ReadOnly Property CommAddrCode As Field(Of String) 
            Get
                Return FI_CommAddrCode
            End Get
        End Property

        Public ReadOnly Property Email2 As Field(Of String) 
            Get
                Return FI_Email2
            End Get
        End Property

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal CommunicationRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From Communication")
            strSQL.AppendLine("Where IDNo = @IDNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, CommunicationRow.IDNo.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal CommunicationRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From Communication")
            strSQL.AppendLine("Where IDNo = @IDNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, CommunicationRow.IDNo.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal CommunicationRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From Communication")
            strSQL.AppendLine("Where IDNo = @IDNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In CommunicationRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal CommunicationRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From Communication")
            strSQL.AppendLine("Where IDNo = @IDNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In CommunicationRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal CommunicationRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Communication")
            strSQL.AppendLine("Where IDNo = @IDNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, CommunicationRow.IDNo.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(CommunicationRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Communication")
            strSQL.AppendLine("Where IDNo = @IDNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, CommunicationRow.IDNo.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal CommunicationRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update Communication Set")
            For i As Integer = 0 To CommunicationRow.FieldNames.Length - 1
                If Not CommunicationRow.IsIdentityField(CommunicationRow.FieldNames(i)) AndAlso CommunicationRow.IsUpdated(CommunicationRow.FieldNames(i)) AndAlso CommunicationRow.CreateUpdateSQL(CommunicationRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, CommunicationRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where IDNo = @PKIDNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If CommunicationRow.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, CommunicationRow.IDNo.Value)
            If CommunicationRow.RegAddrCode.Updated Then db.AddInParameter(dbcmd, "@RegAddrCode", DbType.String, CommunicationRow.RegAddrCode.Value)
            If CommunicationRow.RegAddr.Updated Then db.AddInParameter(dbcmd, "@RegAddr", DbType.String, CommunicationRow.RegAddr.Value)
            If CommunicationRow.CommAddr.Updated Then db.AddInParameter(dbcmd, "@CommAddr", DbType.String, CommunicationRow.CommAddr.Value)
            If CommunicationRow.CommTelCode1.Updated Then db.AddInParameter(dbcmd, "@CommTelCode1", DbType.String, CommunicationRow.CommTelCode1.Value)
            If CommunicationRow.CommTel1.Updated Then db.AddInParameter(dbcmd, "@CommTel1", DbType.String, CommunicationRow.CommTel1.Value)
            If CommunicationRow.CommTelCode2.Updated Then db.AddInParameter(dbcmd, "@CommTelCode2", DbType.String, CommunicationRow.CommTelCode2.Value)
            If CommunicationRow.CommTel2.Updated Then db.AddInParameter(dbcmd, "@CommTel2", DbType.String, CommunicationRow.CommTel2.Value)
            If CommunicationRow.CommTelCode3.Updated Then db.AddInParameter(dbcmd, "@CommTelCode3", DbType.String, CommunicationRow.CommTelCode3.Value)
            If CommunicationRow.CommTel3.Updated Then db.AddInParameter(dbcmd, "@CommTel3", DbType.String, CommunicationRow.CommTel3.Value)
            If CommunicationRow.CommTelCode4.Updated Then db.AddInParameter(dbcmd, "@CommTelCode4", DbType.String, CommunicationRow.CommTelCode4.Value)
            If CommunicationRow.CommTel4.Updated Then db.AddInParameter(dbcmd, "@CommTel4", DbType.String, CommunicationRow.CommTel4.Value)
            If CommunicationRow.RelName.Updated Then db.AddInParameter(dbcmd, "@RelName", DbType.String, CommunicationRow.RelName.Value)
            If CommunicationRow.RelTel.Updated Then db.AddInParameter(dbcmd, "@RelTel", DbType.String, CommunicationRow.RelTel.Value)
            If CommunicationRow.RelRelation.Updated Then db.AddInParameter(dbcmd, "@RelRelation", DbType.String, CommunicationRow.RelRelation.Value)
            If CommunicationRow.EMail.Updated Then db.AddInParameter(dbcmd, "@EMail", DbType.String, CommunicationRow.EMail.Value)
            If CommunicationRow.CompTelCode.Updated Then db.AddInParameter(dbcmd, "@CompTelCode", DbType.String, CommunicationRow.CompTelCode.Value)
            If CommunicationRow.AreaCode.Updated Then db.AddInParameter(dbcmd, "@AreaCode", DbType.String, CommunicationRow.AreaCode.Value)
            If CommunicationRow.CompTel.Updated Then db.AddInParameter(dbcmd, "@CompTel", DbType.String, CommunicationRow.CompTel.Value)
            If CommunicationRow.ExtNo.Updated Then db.AddInParameter(dbcmd, "@ExtNo", DbType.String, CommunicationRow.ExtNo.Value)
            If CommunicationRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, CommunicationRow.LastChgComp.Value)
            If CommunicationRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, CommunicationRow.LastChgID.Value)
            If CommunicationRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(CommunicationRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), CommunicationRow.LastChgDate.Value))
            If CommunicationRow.RegCityCode.Updated Then db.AddInParameter(dbcmd, "@RegCityCode", DbType.String, CommunicationRow.RegCityCode.Value)
            If CommunicationRow.CommCityCode.Updated Then db.AddInParameter(dbcmd, "@CommCityCode", DbType.String, CommunicationRow.CommCityCode.Value)
            If CommunicationRow.CommAddrCode.Updated Then db.AddInParameter(dbcmd, "@CommAddrCode", DbType.String, CommunicationRow.CommAddrCode.Value)
            If CommunicationRow.Email2.Updated Then db.AddInParameter(dbcmd, "@Email2", DbType.String, CommunicationRow.Email2.Value)
            db.AddInParameter(dbcmd, "@PKIDNo", DbType.String, IIf(CommunicationRow.LoadFromDataRow, CommunicationRow.IDNo.OldValue, CommunicationRow.IDNo.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal CommunicationRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update Communication Set")
            For i As Integer = 0 To CommunicationRow.FieldNames.Length - 1
                If Not CommunicationRow.IsIdentityField(CommunicationRow.FieldNames(i)) AndAlso CommunicationRow.IsUpdated(CommunicationRow.FieldNames(i)) AndAlso CommunicationRow.CreateUpdateSQL(CommunicationRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, CommunicationRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where IDNo = @PKIDNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If CommunicationRow.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, CommunicationRow.IDNo.Value)
            If CommunicationRow.RegAddrCode.Updated Then db.AddInParameter(dbcmd, "@RegAddrCode", DbType.String, CommunicationRow.RegAddrCode.Value)
            If CommunicationRow.RegAddr.Updated Then db.AddInParameter(dbcmd, "@RegAddr", DbType.String, CommunicationRow.RegAddr.Value)
            If CommunicationRow.CommAddr.Updated Then db.AddInParameter(dbcmd, "@CommAddr", DbType.String, CommunicationRow.CommAddr.Value)
            If CommunicationRow.CommTelCode1.Updated Then db.AddInParameter(dbcmd, "@CommTelCode1", DbType.String, CommunicationRow.CommTelCode1.Value)
            If CommunicationRow.CommTel1.Updated Then db.AddInParameter(dbcmd, "@CommTel1", DbType.String, CommunicationRow.CommTel1.Value)
            If CommunicationRow.CommTelCode2.Updated Then db.AddInParameter(dbcmd, "@CommTelCode2", DbType.String, CommunicationRow.CommTelCode2.Value)
            If CommunicationRow.CommTel2.Updated Then db.AddInParameter(dbcmd, "@CommTel2", DbType.String, CommunicationRow.CommTel2.Value)
            If CommunicationRow.CommTelCode3.Updated Then db.AddInParameter(dbcmd, "@CommTelCode3", DbType.String, CommunicationRow.CommTelCode3.Value)
            If CommunicationRow.CommTel3.Updated Then db.AddInParameter(dbcmd, "@CommTel3", DbType.String, CommunicationRow.CommTel3.Value)
            If CommunicationRow.CommTelCode4.Updated Then db.AddInParameter(dbcmd, "@CommTelCode4", DbType.String, CommunicationRow.CommTelCode4.Value)
            If CommunicationRow.CommTel4.Updated Then db.AddInParameter(dbcmd, "@CommTel4", DbType.String, CommunicationRow.CommTel4.Value)
            If CommunicationRow.RelName.Updated Then db.AddInParameter(dbcmd, "@RelName", DbType.String, CommunicationRow.RelName.Value)
            If CommunicationRow.RelTel.Updated Then db.AddInParameter(dbcmd, "@RelTel", DbType.String, CommunicationRow.RelTel.Value)
            If CommunicationRow.RelRelation.Updated Then db.AddInParameter(dbcmd, "@RelRelation", DbType.String, CommunicationRow.RelRelation.Value)
            If CommunicationRow.EMail.Updated Then db.AddInParameter(dbcmd, "@EMail", DbType.String, CommunicationRow.EMail.Value)
            If CommunicationRow.CompTelCode.Updated Then db.AddInParameter(dbcmd, "@CompTelCode", DbType.String, CommunicationRow.CompTelCode.Value)
            If CommunicationRow.AreaCode.Updated Then db.AddInParameter(dbcmd, "@AreaCode", DbType.String, CommunicationRow.AreaCode.Value)
            If CommunicationRow.CompTel.Updated Then db.AddInParameter(dbcmd, "@CompTel", DbType.String, CommunicationRow.CompTel.Value)
            If CommunicationRow.ExtNo.Updated Then db.AddInParameter(dbcmd, "@ExtNo", DbType.String, CommunicationRow.ExtNo.Value)
            If CommunicationRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, CommunicationRow.LastChgComp.Value)
            If CommunicationRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, CommunicationRow.LastChgID.Value)
            If CommunicationRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(CommunicationRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), CommunicationRow.LastChgDate.Value))
            If CommunicationRow.RegCityCode.Updated Then db.AddInParameter(dbcmd, "@RegCityCode", DbType.String, CommunicationRow.RegCityCode.Value)
            If CommunicationRow.CommCityCode.Updated Then db.AddInParameter(dbcmd, "@CommCityCode", DbType.String, CommunicationRow.CommCityCode.Value)
            If CommunicationRow.CommAddrCode.Updated Then db.AddInParameter(dbcmd, "@CommAddrCode", DbType.String, CommunicationRow.CommAddrCode.Value)
            If CommunicationRow.Email2.Updated Then db.AddInParameter(dbcmd, "@Email2", DbType.String, CommunicationRow.Email2.Value)
            db.AddInParameter(dbcmd, "@PKIDNo", DbType.String, IIf(CommunicationRow.LoadFromDataRow, CommunicationRow.IDNo.OldValue, CommunicationRow.IDNo.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal CommunicationRow As Row()) As Integer
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
                    For Each r As Row In CommunicationRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update Communication Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where IDNo = @PKIDNo")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                        If r.RegAddrCode.Updated Then db.AddInParameter(dbcmd, "@RegAddrCode", DbType.String, r.RegAddrCode.Value)
                        If r.RegAddr.Updated Then db.AddInParameter(dbcmd, "@RegAddr", DbType.String, r.RegAddr.Value)
                        If r.CommAddr.Updated Then db.AddInParameter(dbcmd, "@CommAddr", DbType.String, r.CommAddr.Value)
                        If r.CommTelCode1.Updated Then db.AddInParameter(dbcmd, "@CommTelCode1", DbType.String, r.CommTelCode1.Value)
                        If r.CommTel1.Updated Then db.AddInParameter(dbcmd, "@CommTel1", DbType.String, r.CommTel1.Value)
                        If r.CommTelCode2.Updated Then db.AddInParameter(dbcmd, "@CommTelCode2", DbType.String, r.CommTelCode2.Value)
                        If r.CommTel2.Updated Then db.AddInParameter(dbcmd, "@CommTel2", DbType.String, r.CommTel2.Value)
                        If r.CommTelCode3.Updated Then db.AddInParameter(dbcmd, "@CommTelCode3", DbType.String, r.CommTelCode3.Value)
                        If r.CommTel3.Updated Then db.AddInParameter(dbcmd, "@CommTel3", DbType.String, r.CommTel3.Value)
                        If r.CommTelCode4.Updated Then db.AddInParameter(dbcmd, "@CommTelCode4", DbType.String, r.CommTelCode4.Value)
                        If r.CommTel4.Updated Then db.AddInParameter(dbcmd, "@CommTel4", DbType.String, r.CommTel4.Value)
                        If r.RelName.Updated Then db.AddInParameter(dbcmd, "@RelName", DbType.String, r.RelName.Value)
                        If r.RelTel.Updated Then db.AddInParameter(dbcmd, "@RelTel", DbType.String, r.RelTel.Value)
                        If r.RelRelation.Updated Then db.AddInParameter(dbcmd, "@RelRelation", DbType.String, r.RelRelation.Value)
                        If r.EMail.Updated Then db.AddInParameter(dbcmd, "@EMail", DbType.String, r.EMail.Value)
                        If r.CompTelCode.Updated Then db.AddInParameter(dbcmd, "@CompTelCode", DbType.String, r.CompTelCode.Value)
                        If r.AreaCode.Updated Then db.AddInParameter(dbcmd, "@AreaCode", DbType.String, r.AreaCode.Value)
                        If r.CompTel.Updated Then db.AddInParameter(dbcmd, "@CompTel", DbType.String, r.CompTel.Value)
                        If r.ExtNo.Updated Then db.AddInParameter(dbcmd, "@ExtNo", DbType.String, r.ExtNo.Value)
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        If r.RegCityCode.Updated Then db.AddInParameter(dbcmd, "@RegCityCode", DbType.String, r.RegCityCode.Value)
                        If r.CommCityCode.Updated Then db.AddInParameter(dbcmd, "@CommCityCode", DbType.String, r.CommCityCode.Value)
                        If r.CommAddrCode.Updated Then db.AddInParameter(dbcmd, "@CommAddrCode", DbType.String, r.CommAddrCode.Value)
                        If r.Email2.Updated Then db.AddInParameter(dbcmd, "@Email2", DbType.String, r.Email2.Value)
                        db.AddInParameter(dbcmd, "@PKIDNo", DbType.String, IIf(r.LoadFromDataRow, r.IDNo.OldValue, r.IDNo.Value))

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

        Public Function Update(ByVal CommunicationRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In CommunicationRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update Communication Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where IDNo = @PKIDNo")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.IDNo.Updated Then db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                If r.RegAddrCode.Updated Then db.AddInParameter(dbcmd, "@RegAddrCode", DbType.String, r.RegAddrCode.Value)
                If r.RegAddr.Updated Then db.AddInParameter(dbcmd, "@RegAddr", DbType.String, r.RegAddr.Value)
                If r.CommAddr.Updated Then db.AddInParameter(dbcmd, "@CommAddr", DbType.String, r.CommAddr.Value)
                If r.CommTelCode1.Updated Then db.AddInParameter(dbcmd, "@CommTelCode1", DbType.String, r.CommTelCode1.Value)
                If r.CommTel1.Updated Then db.AddInParameter(dbcmd, "@CommTel1", DbType.String, r.CommTel1.Value)
                If r.CommTelCode2.Updated Then db.AddInParameter(dbcmd, "@CommTelCode2", DbType.String, r.CommTelCode2.Value)
                If r.CommTel2.Updated Then db.AddInParameter(dbcmd, "@CommTel2", DbType.String, r.CommTel2.Value)
                If r.CommTelCode3.Updated Then db.AddInParameter(dbcmd, "@CommTelCode3", DbType.String, r.CommTelCode3.Value)
                If r.CommTel3.Updated Then db.AddInParameter(dbcmd, "@CommTel3", DbType.String, r.CommTel3.Value)
                If r.CommTelCode4.Updated Then db.AddInParameter(dbcmd, "@CommTelCode4", DbType.String, r.CommTelCode4.Value)
                If r.CommTel4.Updated Then db.AddInParameter(dbcmd, "@CommTel4", DbType.String, r.CommTel4.Value)
                If r.RelName.Updated Then db.AddInParameter(dbcmd, "@RelName", DbType.String, r.RelName.Value)
                If r.RelTel.Updated Then db.AddInParameter(dbcmd, "@RelTel", DbType.String, r.RelTel.Value)
                If r.RelRelation.Updated Then db.AddInParameter(dbcmd, "@RelRelation", DbType.String, r.RelRelation.Value)
                If r.EMail.Updated Then db.AddInParameter(dbcmd, "@EMail", DbType.String, r.EMail.Value)
                If r.CompTelCode.Updated Then db.AddInParameter(dbcmd, "@CompTelCode", DbType.String, r.CompTelCode.Value)
                If r.AreaCode.Updated Then db.AddInParameter(dbcmd, "@AreaCode", DbType.String, r.AreaCode.Value)
                If r.CompTel.Updated Then db.AddInParameter(dbcmd, "@CompTel", DbType.String, r.CompTel.Value)
                If r.ExtNo.Updated Then db.AddInParameter(dbcmd, "@ExtNo", DbType.String, r.ExtNo.Value)
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                If r.RegCityCode.Updated Then db.AddInParameter(dbcmd, "@RegCityCode", DbType.String, r.RegCityCode.Value)
                If r.CommCityCode.Updated Then db.AddInParameter(dbcmd, "@CommCityCode", DbType.String, r.CommCityCode.Value)
                If r.CommAddrCode.Updated Then db.AddInParameter(dbcmd, "@CommAddrCode", DbType.String, r.CommAddrCode.Value)
                If r.Email2.Updated Then db.AddInParameter(dbcmd, "@Email2", DbType.String, r.Email2.Value)
                db.AddInParameter(dbcmd, "@PKIDNo", DbType.String, IIf(r.LoadFromDataRow, r.IDNo.OldValue, r.IDNo.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal CommunicationRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From Communication")
            strSQL.AppendLine("Where IDNo = @IDNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, CommunicationRow.IDNo.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal CommunicationRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From Communication")
            strSQL.AppendLine("Where IDNo = @IDNo")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, CommunicationRow.IDNo.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Communication")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal CommunicationRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into Communication")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    IDNo, RegAddrCode, RegAddr, CommAddr, CommTelCode1, CommTel1, CommTelCode2, CommTel2,")
            strSQL.AppendLine("    CommTelCode3, CommTel3, CommTelCode4, CommTel4, RelName, RelTel, RelRelation, EMail,")
            strSQL.AppendLine("    CompTelCode, AreaCode, CompTel, ExtNo, LastChgComp, LastChgID, LastChgDate, RegCityCode,")
            strSQL.AppendLine("    CommCityCode, CommAddrCode, Email2")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @IDNo, @RegAddrCode, @RegAddr, @CommAddr, @CommTelCode1, @CommTel1, @CommTelCode2, @CommTel2,")
            strSQL.AppendLine("    @CommTelCode3, @CommTel3, @CommTelCode4, @CommTel4, @RelName, @RelTel, @RelRelation, @EMail,")
            strSQL.AppendLine("    @CompTelCode, @AreaCode, @CompTel, @ExtNo, @LastChgComp, @LastChgID, @LastChgDate, @RegCityCode,")
            strSQL.AppendLine("    @CommCityCode, @CommAddrCode, @Email2")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, CommunicationRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@RegAddrCode", DbType.String, CommunicationRow.RegAddrCode.Value)
            db.AddInParameter(dbcmd, "@RegAddr", DbType.String, CommunicationRow.RegAddr.Value)
            db.AddInParameter(dbcmd, "@CommAddr", DbType.String, CommunicationRow.CommAddr.Value)
            db.AddInParameter(dbcmd, "@CommTelCode1", DbType.String, CommunicationRow.CommTelCode1.Value)
            db.AddInParameter(dbcmd, "@CommTel1", DbType.String, CommunicationRow.CommTel1.Value)
            db.AddInParameter(dbcmd, "@CommTelCode2", DbType.String, CommunicationRow.CommTelCode2.Value)
            db.AddInParameter(dbcmd, "@CommTel2", DbType.String, CommunicationRow.CommTel2.Value)
            db.AddInParameter(dbcmd, "@CommTelCode3", DbType.String, CommunicationRow.CommTelCode3.Value)
            db.AddInParameter(dbcmd, "@CommTel3", DbType.String, CommunicationRow.CommTel3.Value)
            db.AddInParameter(dbcmd, "@CommTelCode4", DbType.String, CommunicationRow.CommTelCode4.Value)
            db.AddInParameter(dbcmd, "@CommTel4", DbType.String, CommunicationRow.CommTel4.Value)
            db.AddInParameter(dbcmd, "@RelName", DbType.String, CommunicationRow.RelName.Value)
            db.AddInParameter(dbcmd, "@RelTel", DbType.String, CommunicationRow.RelTel.Value)
            db.AddInParameter(dbcmd, "@RelRelation", DbType.String, CommunicationRow.RelRelation.Value)
            db.AddInParameter(dbcmd, "@EMail", DbType.String, CommunicationRow.EMail.Value)
            db.AddInParameter(dbcmd, "@CompTelCode", DbType.String, CommunicationRow.CompTelCode.Value)
            db.AddInParameter(dbcmd, "@AreaCode", DbType.String, CommunicationRow.AreaCode.Value)
            db.AddInParameter(dbcmd, "@CompTel", DbType.String, CommunicationRow.CompTel.Value)
            db.AddInParameter(dbcmd, "@ExtNo", DbType.String, CommunicationRow.ExtNo.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, CommunicationRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, CommunicationRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(CommunicationRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), CommunicationRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@RegCityCode", DbType.String, CommunicationRow.RegCityCode.Value)
            db.AddInParameter(dbcmd, "@CommCityCode", DbType.String, CommunicationRow.CommCityCode.Value)
            db.AddInParameter(dbcmd, "@CommAddrCode", DbType.String, CommunicationRow.CommAddrCode.Value)
            db.AddInParameter(dbcmd, "@Email2", DbType.String, CommunicationRow.Email2.Value)

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal CommunicationRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into Communication")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    IDNo, RegAddrCode, RegAddr, CommAddr, CommTelCode1, CommTel1, CommTelCode2, CommTel2,")
            strSQL.AppendLine("    CommTelCode3, CommTel3, CommTelCode4, CommTel4, RelName, RelTel, RelRelation, EMail,")
            strSQL.AppendLine("    CompTelCode, AreaCode, CompTel, ExtNo, LastChgComp, LastChgID, LastChgDate, RegCityCode,")
            strSQL.AppendLine("    CommCityCode, CommAddrCode, Email2")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @IDNo, @RegAddrCode, @RegAddr, @CommAddr, @CommTelCode1, @CommTel1, @CommTelCode2, @CommTel2,")
            strSQL.AppendLine("    @CommTelCode3, @CommTel3, @CommTelCode4, @CommTel4, @RelName, @RelTel, @RelRelation, @EMail,")
            strSQL.AppendLine("    @CompTelCode, @AreaCode, @CompTel, @ExtNo, @LastChgComp, @LastChgID, @LastChgDate, @RegCityCode,")
            strSQL.AppendLine("    @CommCityCode, @CommAddrCode, @Email2")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@IDNo", DbType.String, CommunicationRow.IDNo.Value)
            db.AddInParameter(dbcmd, "@RegAddrCode", DbType.String, CommunicationRow.RegAddrCode.Value)
            db.AddInParameter(dbcmd, "@RegAddr", DbType.String, CommunicationRow.RegAddr.Value)
            db.AddInParameter(dbcmd, "@CommAddr", DbType.String, CommunicationRow.CommAddr.Value)
            db.AddInParameter(dbcmd, "@CommTelCode1", DbType.String, CommunicationRow.CommTelCode1.Value)
            db.AddInParameter(dbcmd, "@CommTel1", DbType.String, CommunicationRow.CommTel1.Value)
            db.AddInParameter(dbcmd, "@CommTelCode2", DbType.String, CommunicationRow.CommTelCode2.Value)
            db.AddInParameter(dbcmd, "@CommTel2", DbType.String, CommunicationRow.CommTel2.Value)
            db.AddInParameter(dbcmd, "@CommTelCode3", DbType.String, CommunicationRow.CommTelCode3.Value)
            db.AddInParameter(dbcmd, "@CommTel3", DbType.String, CommunicationRow.CommTel3.Value)
            db.AddInParameter(dbcmd, "@CommTelCode4", DbType.String, CommunicationRow.CommTelCode4.Value)
            db.AddInParameter(dbcmd, "@CommTel4", DbType.String, CommunicationRow.CommTel4.Value)
            db.AddInParameter(dbcmd, "@RelName", DbType.String, CommunicationRow.RelName.Value)
            db.AddInParameter(dbcmd, "@RelTel", DbType.String, CommunicationRow.RelTel.Value)
            db.AddInParameter(dbcmd, "@RelRelation", DbType.String, CommunicationRow.RelRelation.Value)
            db.AddInParameter(dbcmd, "@EMail", DbType.String, CommunicationRow.EMail.Value)
            db.AddInParameter(dbcmd, "@CompTelCode", DbType.String, CommunicationRow.CompTelCode.Value)
            db.AddInParameter(dbcmd, "@AreaCode", DbType.String, CommunicationRow.AreaCode.Value)
            db.AddInParameter(dbcmd, "@CompTel", DbType.String, CommunicationRow.CompTel.Value)
            db.AddInParameter(dbcmd, "@ExtNo", DbType.String, CommunicationRow.ExtNo.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, CommunicationRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, CommunicationRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(CommunicationRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), CommunicationRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@RegCityCode", DbType.String, CommunicationRow.RegCityCode.Value)
            db.AddInParameter(dbcmd, "@CommCityCode", DbType.String, CommunicationRow.CommCityCode.Value)
            db.AddInParameter(dbcmd, "@CommAddrCode", DbType.String, CommunicationRow.CommAddrCode.Value)
            db.AddInParameter(dbcmd, "@Email2", DbType.String, CommunicationRow.Email2.Value)

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal CommunicationRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into Communication")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    IDNo, RegAddrCode, RegAddr, CommAddr, CommTelCode1, CommTel1, CommTelCode2, CommTel2,")
            strSQL.AppendLine("    CommTelCode3, CommTel3, CommTelCode4, CommTel4, RelName, RelTel, RelRelation, EMail,")
            strSQL.AppendLine("    CompTelCode, AreaCode, CompTel, ExtNo, LastChgComp, LastChgID, LastChgDate, RegCityCode,")
            strSQL.AppendLine("    CommCityCode, CommAddrCode, Email2")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @IDNo, @RegAddrCode, @RegAddr, @CommAddr, @CommTelCode1, @CommTel1, @CommTelCode2, @CommTel2,")
            strSQL.AppendLine("    @CommTelCode3, @CommTel3, @CommTelCode4, @CommTel4, @RelName, @RelTel, @RelRelation, @EMail,")
            strSQL.AppendLine("    @CompTelCode, @AreaCode, @CompTel, @ExtNo, @LastChgComp, @LastChgID, @LastChgDate, @RegCityCode,")
            strSQL.AppendLine("    @CommCityCode, @CommAddrCode, @Email2")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In CommunicationRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                        db.AddInParameter(dbcmd, "@RegAddrCode", DbType.String, r.RegAddrCode.Value)
                        db.AddInParameter(dbcmd, "@RegAddr", DbType.String, r.RegAddr.Value)
                        db.AddInParameter(dbcmd, "@CommAddr", DbType.String, r.CommAddr.Value)
                        db.AddInParameter(dbcmd, "@CommTelCode1", DbType.String, r.CommTelCode1.Value)
                        db.AddInParameter(dbcmd, "@CommTel1", DbType.String, r.CommTel1.Value)
                        db.AddInParameter(dbcmd, "@CommTelCode2", DbType.String, r.CommTelCode2.Value)
                        db.AddInParameter(dbcmd, "@CommTel2", DbType.String, r.CommTel2.Value)
                        db.AddInParameter(dbcmd, "@CommTelCode3", DbType.String, r.CommTelCode3.Value)
                        db.AddInParameter(dbcmd, "@CommTel3", DbType.String, r.CommTel3.Value)
                        db.AddInParameter(dbcmd, "@CommTelCode4", DbType.String, r.CommTelCode4.Value)
                        db.AddInParameter(dbcmd, "@CommTel4", DbType.String, r.CommTel4.Value)
                        db.AddInParameter(dbcmd, "@RelName", DbType.String, r.RelName.Value)
                        db.AddInParameter(dbcmd, "@RelTel", DbType.String, r.RelTel.Value)
                        db.AddInParameter(dbcmd, "@RelRelation", DbType.String, r.RelRelation.Value)
                        db.AddInParameter(dbcmd, "@EMail", DbType.String, r.EMail.Value)
                        db.AddInParameter(dbcmd, "@CompTelCode", DbType.String, r.CompTelCode.Value)
                        db.AddInParameter(dbcmd, "@AreaCode", DbType.String, r.AreaCode.Value)
                        db.AddInParameter(dbcmd, "@CompTel", DbType.String, r.CompTel.Value)
                        db.AddInParameter(dbcmd, "@ExtNo", DbType.String, r.ExtNo.Value)
                        db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@RegCityCode", DbType.String, r.RegCityCode.Value)
                        db.AddInParameter(dbcmd, "@CommCityCode", DbType.String, r.CommCityCode.Value)
                        db.AddInParameter(dbcmd, "@CommAddrCode", DbType.String, r.CommAddrCode.Value)
                        db.AddInParameter(dbcmd, "@Email2", DbType.String, r.Email2.Value)

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

        Public Function Insert(ByVal CommunicationRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into Communication")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    IDNo, RegAddrCode, RegAddr, CommAddr, CommTelCode1, CommTel1, CommTelCode2, CommTel2,")
            strSQL.AppendLine("    CommTelCode3, CommTel3, CommTelCode4, CommTel4, RelName, RelTel, RelRelation, EMail,")
            strSQL.AppendLine("    CompTelCode, AreaCode, CompTel, ExtNo, LastChgComp, LastChgID, LastChgDate, RegCityCode,")
            strSQL.AppendLine("    CommCityCode, CommAddrCode, Email2")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @IDNo, @RegAddrCode, @RegAddr, @CommAddr, @CommTelCode1, @CommTel1, @CommTelCode2, @CommTel2,")
            strSQL.AppendLine("    @CommTelCode3, @CommTel3, @CommTelCode4, @CommTel4, @RelName, @RelTel, @RelRelation, @EMail,")
            strSQL.AppendLine("    @CompTelCode, @AreaCode, @CompTel, @ExtNo, @LastChgComp, @LastChgID, @LastChgDate, @RegCityCode,")
            strSQL.AppendLine("    @CommCityCode, @CommAddrCode, @Email2")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In CommunicationRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@IDNo", DbType.String, r.IDNo.Value)
                db.AddInParameter(dbcmd, "@RegAddrCode", DbType.String, r.RegAddrCode.Value)
                db.AddInParameter(dbcmd, "@RegAddr", DbType.String, r.RegAddr.Value)
                db.AddInParameter(dbcmd, "@CommAddr", DbType.String, r.CommAddr.Value)
                db.AddInParameter(dbcmd, "@CommTelCode1", DbType.String, r.CommTelCode1.Value)
                db.AddInParameter(dbcmd, "@CommTel1", DbType.String, r.CommTel1.Value)
                db.AddInParameter(dbcmd, "@CommTelCode2", DbType.String, r.CommTelCode2.Value)
                db.AddInParameter(dbcmd, "@CommTel2", DbType.String, r.CommTel2.Value)
                db.AddInParameter(dbcmd, "@CommTelCode3", DbType.String, r.CommTelCode3.Value)
                db.AddInParameter(dbcmd, "@CommTel3", DbType.String, r.CommTel3.Value)
                db.AddInParameter(dbcmd, "@CommTelCode4", DbType.String, r.CommTelCode4.Value)
                db.AddInParameter(dbcmd, "@CommTel4", DbType.String, r.CommTel4.Value)
                db.AddInParameter(dbcmd, "@RelName", DbType.String, r.RelName.Value)
                db.AddInParameter(dbcmd, "@RelTel", DbType.String, r.RelTel.Value)
                db.AddInParameter(dbcmd, "@RelRelation", DbType.String, r.RelRelation.Value)
                db.AddInParameter(dbcmd, "@EMail", DbType.String, r.EMail.Value)
                db.AddInParameter(dbcmd, "@CompTelCode", DbType.String, r.CompTelCode.Value)
                db.AddInParameter(dbcmd, "@AreaCode", DbType.String, r.AreaCode.Value)
                db.AddInParameter(dbcmd, "@CompTel", DbType.String, r.CompTel.Value)
                db.AddInParameter(dbcmd, "@ExtNo", DbType.String, r.ExtNo.Value)
                db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@RegCityCode", DbType.String, r.RegCityCode.Value)
                db.AddInParameter(dbcmd, "@CommCityCode", DbType.String, r.CommCityCode.Value)
                db.AddInParameter(dbcmd, "@CommAddrCode", DbType.String, r.CommAddrCode.Value)
                db.AddInParameter(dbcmd, "@Email2", DbType.String, r.Email2.Value)

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

