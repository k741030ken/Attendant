'****************************************************************
' Table:PunchPara
' Created Date: 2017.06.20
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace bePunchPara
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "Para", "DutyInFlag", "DutyInBT", "DutyOutFlag", "DutyOutBT", "PunchInFlag", "PunchInBT", "PunchOutFlag", "PunchOutBT" _
                                    , "MsgPara", "PunchInMsgFlag", "PunchInDefaultContent", "PunchInSelfContent", "PunchOutMsgFlag", "PunchOutDefaultContent", "PunchOutSelfContent", "AffairMsgFlag", "AffairDefaultContent", "AffairSelfContent", "OVTenMsgFlag" _
                                    , "OVTenDefaultContent", "OVTenSelfContent", "FemaleMsgFlag", "FemaleDefaultContent", "FemaleSelfContent", "ExcludePara", "HoldingRankIDFlag", "HoldingRankID", "PositionFlag", "Position", "WorkTypeFlag" _
                                    , "WorkTypeID", "RotateFlag", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "CompID" }

        Public ReadOnly Property Rows() As bePunchPara.Rows 
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
        Public Sub Transfer2Row(PunchParaTable As DataTable)
            For Each dr As DataRow In PunchParaTable.Rows
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
                dr(m_Rows(i).Para.FieldName) = m_Rows(i).Para.Value
                dr(m_Rows(i).DutyInFlag.FieldName) = m_Rows(i).DutyInFlag.Value
                dr(m_Rows(i).DutyInBT.FieldName) = m_Rows(i).DutyInBT.Value
                dr(m_Rows(i).DutyOutFlag.FieldName) = m_Rows(i).DutyOutFlag.Value
                dr(m_Rows(i).DutyOutBT.FieldName) = m_Rows(i).DutyOutBT.Value
                dr(m_Rows(i).PunchInFlag.FieldName) = m_Rows(i).PunchInFlag.Value
                dr(m_Rows(i).PunchInBT.FieldName) = m_Rows(i).PunchInBT.Value
                dr(m_Rows(i).PunchOutFlag.FieldName) = m_Rows(i).PunchOutFlag.Value
                dr(m_Rows(i).PunchOutBT.FieldName) = m_Rows(i).PunchOutBT.Value
                dr(m_Rows(i).MsgPara.FieldName) = m_Rows(i).MsgPara.Value
                dr(m_Rows(i).PunchInMsgFlag.FieldName) = m_Rows(i).PunchInMsgFlag.Value
                dr(m_Rows(i).PunchInDefaultContent.FieldName) = m_Rows(i).PunchInDefaultContent.Value
                dr(m_Rows(i).PunchInSelfContent.FieldName) = m_Rows(i).PunchInSelfContent.Value
                dr(m_Rows(i).PunchOutMsgFlag.FieldName) = m_Rows(i).PunchOutMsgFlag.Value
                dr(m_Rows(i).PunchOutDefaultContent.FieldName) = m_Rows(i).PunchOutDefaultContent.Value
                dr(m_Rows(i).PunchOutSelfContent.FieldName) = m_Rows(i).PunchOutSelfContent.Value
                dr(m_Rows(i).AffairMsgFlag.FieldName) = m_Rows(i).AffairMsgFlag.Value
                dr(m_Rows(i).AffairDefaultContent.FieldName) = m_Rows(i).AffairDefaultContent.Value
                dr(m_Rows(i).AffairSelfContent.FieldName) = m_Rows(i).AffairSelfContent.Value
                dr(m_Rows(i).OVTenMsgFlag.FieldName) = m_Rows(i).OVTenMsgFlag.Value
                dr(m_Rows(i).OVTenDefaultContent.FieldName) = m_Rows(i).OVTenDefaultContent.Value
                dr(m_Rows(i).OVTenSelfContent.FieldName) = m_Rows(i).OVTenSelfContent.Value
                dr(m_Rows(i).FemaleMsgFlag.FieldName) = m_Rows(i).FemaleMsgFlag.Value
                dr(m_Rows(i).FemaleDefaultContent.FieldName) = m_Rows(i).FemaleDefaultContent.Value
                dr(m_Rows(i).FemaleSelfContent.FieldName) = m_Rows(i).FemaleSelfContent.Value
                dr(m_Rows(i).ExcludePara.FieldName) = m_Rows(i).ExcludePara.Value
                dr(m_Rows(i).HoldingRankIDFlag.FieldName) = m_Rows(i).HoldingRankIDFlag.Value
                dr(m_Rows(i).HoldingRankID.FieldName) = m_Rows(i).HoldingRankID.Value
                dr(m_Rows(i).PositionFlag.FieldName) = m_Rows(i).PositionFlag.Value
                dr(m_Rows(i).Position.FieldName) = m_Rows(i).Position.Value
                dr(m_Rows(i).WorkTypeFlag.FieldName) = m_Rows(i).WorkTypeFlag.Value
                dr(m_Rows(i).WorkTypeID.FieldName) = m_Rows(i).WorkTypeID.Value
                dr(m_Rows(i).RotateFlag.FieldName) = m_Rows(i).RotateFlag.Value
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

        Public Sub Add(PunchParaRow As Row)
            m_Rows.Add(PunchParaRow)
        End Sub

        Public Sub Remove(PunchParaRow As Row)
            If m_Rows.IndexOf(PunchParaRow) >= 0 Then
                m_Rows.Remove(PunchParaRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_Para As Field(Of String) = new Field(Of String)("Para", true)
        Private FI_DutyInFlag As Field(Of String) = new Field(Of String)("DutyInFlag", true)
        Private FI_DutyInBT As Field(Of String) = new Field(Of String)("DutyInBT", true)
        Private FI_DutyOutFlag As Field(Of String) = new Field(Of String)("DutyOutFlag", true)
        Private FI_DutyOutBT As Field(Of String) = new Field(Of String)("DutyOutBT", true)
        Private FI_PunchInFlag As Field(Of String) = new Field(Of String)("PunchInFlag", true)
        Private FI_PunchInBT As Field(Of String) = new Field(Of String)("PunchInBT", true)
        Private FI_PunchOutFlag As Field(Of String) = new Field(Of String)("PunchOutFlag", true)
        Private FI_PunchOutBT As Field(Of String) = new Field(Of String)("PunchOutBT", true)
        Private FI_MsgPara As Field(Of String) = new Field(Of String)("MsgPara", true)
        Private FI_PunchInMsgFlag As Field(Of String) = new Field(Of String)("PunchInMsgFlag", true)
        Private FI_PunchInDefaultContent As Field(Of String) = new Field(Of String)("PunchInDefaultContent", true)
        Private FI_PunchInSelfContent As Field(Of String) = new Field(Of String)("PunchInSelfContent", true)
        Private FI_PunchOutMsgFlag As Field(Of String) = new Field(Of String)("PunchOutMsgFlag", true)
        Private FI_PunchOutDefaultContent As Field(Of String) = new Field(Of String)("PunchOutDefaultContent", true)
        Private FI_PunchOutSelfContent As Field(Of String) = new Field(Of String)("PunchOutSelfContent", true)
        Private FI_AffairMsgFlag As Field(Of String) = new Field(Of String)("AffairMsgFlag", true)
        Private FI_AffairDefaultContent As Field(Of String) = new Field(Of String)("AffairDefaultContent", true)
        Private FI_AffairSelfContent As Field(Of String) = new Field(Of String)("AffairSelfContent", true)
        Private FI_OVTenMsgFlag As Field(Of String) = new Field(Of String)("OVTenMsgFlag", true)
        Private FI_OVTenDefaultContent As Field(Of String) = new Field(Of String)("OVTenDefaultContent", true)
        Private FI_OVTenSelfContent As Field(Of String) = new Field(Of String)("OVTenSelfContent", true)
        Private FI_FemaleMsgFlag As Field(Of String) = new Field(Of String)("FemaleMsgFlag", true)
        Private FI_FemaleDefaultContent As Field(Of String) = new Field(Of String)("FemaleDefaultContent", true)
        Private FI_FemaleSelfContent As Field(Of String) = new Field(Of String)("FemaleSelfContent", true)
        Private FI_ExcludePara As Field(Of String) = new Field(Of String)("ExcludePara", true)
        Private FI_HoldingRankIDFlag As Field(Of String) = new Field(Of String)("HoldingRankIDFlag", true)
        Private FI_HoldingRankID As Field(Of String) = new Field(Of String)("HoldingRankID", true)
        Private FI_PositionFlag As Field(Of String) = new Field(Of String)("PositionFlag", true)
        Private FI_Position As Field(Of String) = new Field(Of String)("Position", true)
        Private FI_WorkTypeFlag As Field(Of String) = new Field(Of String)("WorkTypeFlag", true)
        Private FI_WorkTypeID As Field(Of String) = new Field(Of String)("WorkTypeID", true)
        Private FI_RotateFlag As Field(Of String) = new Field(Of String)("RotateFlag", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "CompID", "Para", "DutyInFlag", "DutyInBT", "DutyOutFlag", "DutyOutBT", "PunchInFlag", "PunchInBT", "PunchOutFlag", "PunchOutBT" _
                                    , "MsgPara", "PunchInMsgFlag", "PunchInDefaultContent", "PunchInSelfContent", "PunchOutMsgFlag", "PunchOutDefaultContent", "PunchOutSelfContent", "AffairMsgFlag", "AffairDefaultContent", "AffairSelfContent", "OVTenMsgFlag" _
                                    , "OVTenDefaultContent", "OVTenSelfContent", "FemaleMsgFlag", "FemaleDefaultContent", "FemaleSelfContent", "ExcludePara", "HoldingRankIDFlag", "HoldingRankID", "PositionFlag", "Position", "WorkTypeFlag" _
                                    , "WorkTypeID", "RotateFlag", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "CompID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CompID"
                    Return FI_CompID.Value
                Case "Para"
                    Return FI_Para.Value
                Case "DutyInFlag"
                    Return FI_DutyInFlag.Value
                Case "DutyInBT"
                    Return FI_DutyInBT.Value
                Case "DutyOutFlag"
                    Return FI_DutyOutFlag.Value
                Case "DutyOutBT"
                    Return FI_DutyOutBT.Value
                Case "PunchInFlag"
                    Return FI_PunchInFlag.Value
                Case "PunchInBT"
                    Return FI_PunchInBT.Value
                Case "PunchOutFlag"
                    Return FI_PunchOutFlag.Value
                Case "PunchOutBT"
                    Return FI_PunchOutBT.Value
                Case "MsgPara"
                    Return FI_MsgPara.Value
                Case "PunchInMsgFlag"
                    Return FI_PunchInMsgFlag.Value
                Case "PunchInDefaultContent"
                    Return FI_PunchInDefaultContent.Value
                Case "PunchInSelfContent"
                    Return FI_PunchInSelfContent.Value
                Case "PunchOutMsgFlag"
                    Return FI_PunchOutMsgFlag.Value
                Case "PunchOutDefaultContent"
                    Return FI_PunchOutDefaultContent.Value
                Case "PunchOutSelfContent"
                    Return FI_PunchOutSelfContent.Value
                Case "AffairMsgFlag"
                    Return FI_AffairMsgFlag.Value
                Case "AffairDefaultContent"
                    Return FI_AffairDefaultContent.Value
                Case "AffairSelfContent"
                    Return FI_AffairSelfContent.Value
                Case "OVTenMsgFlag"
                    Return FI_OVTenMsgFlag.Value
                Case "OVTenDefaultContent"
                    Return FI_OVTenDefaultContent.Value
                Case "OVTenSelfContent"
                    Return FI_OVTenSelfContent.Value
                Case "FemaleMsgFlag"
                    Return FI_FemaleMsgFlag.Value
                Case "FemaleDefaultContent"
                    Return FI_FemaleDefaultContent.Value
                Case "FemaleSelfContent"
                    Return FI_FemaleSelfContent.Value
                Case "ExcludePara"
                    Return FI_ExcludePara.Value
                Case "HoldingRankIDFlag"
                    Return FI_HoldingRankIDFlag.Value
                Case "HoldingRankID"
                    Return FI_HoldingRankID.Value
                Case "PositionFlag"
                    Return FI_PositionFlag.Value
                Case "Position"
                    Return FI_Position.Value
                Case "WorkTypeFlag"
                    Return FI_WorkTypeFlag.Value
                Case "WorkTypeID"
                    Return FI_WorkTypeID.Value
                Case "RotateFlag"
                    Return FI_RotateFlag.Value
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
                Case "Para"
                    FI_Para.SetValue(value)
                Case "DutyInFlag"
                    FI_DutyInFlag.SetValue(value)
                Case "DutyInBT"
                    FI_DutyInBT.SetValue(value)
                Case "DutyOutFlag"
                    FI_DutyOutFlag.SetValue(value)
                Case "DutyOutBT"
                    FI_DutyOutBT.SetValue(value)
                Case "PunchInFlag"
                    FI_PunchInFlag.SetValue(value)
                Case "PunchInBT"
                    FI_PunchInBT.SetValue(value)
                Case "PunchOutFlag"
                    FI_PunchOutFlag.SetValue(value)
                Case "PunchOutBT"
                    FI_PunchOutBT.SetValue(value)
                Case "MsgPara"
                    FI_MsgPara.SetValue(value)
                Case "PunchInMsgFlag"
                    FI_PunchInMsgFlag.SetValue(value)
                Case "PunchInDefaultContent"
                    FI_PunchInDefaultContent.SetValue(value)
                Case "PunchInSelfContent"
                    FI_PunchInSelfContent.SetValue(value)
                Case "PunchOutMsgFlag"
                    FI_PunchOutMsgFlag.SetValue(value)
                Case "PunchOutDefaultContent"
                    FI_PunchOutDefaultContent.SetValue(value)
                Case "PunchOutSelfContent"
                    FI_PunchOutSelfContent.SetValue(value)
                Case "AffairMsgFlag"
                    FI_AffairMsgFlag.SetValue(value)
                Case "AffairDefaultContent"
                    FI_AffairDefaultContent.SetValue(value)
                Case "AffairSelfContent"
                    FI_AffairSelfContent.SetValue(value)
                Case "OVTenMsgFlag"
                    FI_OVTenMsgFlag.SetValue(value)
                Case "OVTenDefaultContent"
                    FI_OVTenDefaultContent.SetValue(value)
                Case "OVTenSelfContent"
                    FI_OVTenSelfContent.SetValue(value)
                Case "FemaleMsgFlag"
                    FI_FemaleMsgFlag.SetValue(value)
                Case "FemaleDefaultContent"
                    FI_FemaleDefaultContent.SetValue(value)
                Case "FemaleSelfContent"
                    FI_FemaleSelfContent.SetValue(value)
                Case "ExcludePara"
                    FI_ExcludePara.SetValue(value)
                Case "HoldingRankIDFlag"
                    FI_HoldingRankIDFlag.SetValue(value)
                Case "HoldingRankID"
                    FI_HoldingRankID.SetValue(value)
                Case "PositionFlag"
                    FI_PositionFlag.SetValue(value)
                Case "Position"
                    FI_Position.SetValue(value)
                Case "WorkTypeFlag"
                    FI_WorkTypeFlag.SetValue(value)
                Case "WorkTypeID"
                    FI_WorkTypeID.SetValue(value)
                Case "RotateFlag"
                    FI_RotateFlag.SetValue(value)
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
                Case "Para"
                    return FI_Para.Updated
                Case "DutyInFlag"
                    return FI_DutyInFlag.Updated
                Case "DutyInBT"
                    return FI_DutyInBT.Updated
                Case "DutyOutFlag"
                    return FI_DutyOutFlag.Updated
                Case "DutyOutBT"
                    return FI_DutyOutBT.Updated
                Case "PunchInFlag"
                    return FI_PunchInFlag.Updated
                Case "PunchInBT"
                    return FI_PunchInBT.Updated
                Case "PunchOutFlag"
                    return FI_PunchOutFlag.Updated
                Case "PunchOutBT"
                    return FI_PunchOutBT.Updated
                Case "MsgPara"
                    return FI_MsgPara.Updated
                Case "PunchInMsgFlag"
                    return FI_PunchInMsgFlag.Updated
                Case "PunchInDefaultContent"
                    return FI_PunchInDefaultContent.Updated
                Case "PunchInSelfContent"
                    return FI_PunchInSelfContent.Updated
                Case "PunchOutMsgFlag"
                    return FI_PunchOutMsgFlag.Updated
                Case "PunchOutDefaultContent"
                    return FI_PunchOutDefaultContent.Updated
                Case "PunchOutSelfContent"
                    return FI_PunchOutSelfContent.Updated
                Case "AffairMsgFlag"
                    return FI_AffairMsgFlag.Updated
                Case "AffairDefaultContent"
                    return FI_AffairDefaultContent.Updated
                Case "AffairSelfContent"
                    return FI_AffairSelfContent.Updated
                Case "OVTenMsgFlag"
                    return FI_OVTenMsgFlag.Updated
                Case "OVTenDefaultContent"
                    return FI_OVTenDefaultContent.Updated
                Case "OVTenSelfContent"
                    return FI_OVTenSelfContent.Updated
                Case "FemaleMsgFlag"
                    return FI_FemaleMsgFlag.Updated
                Case "FemaleDefaultContent"
                    return FI_FemaleDefaultContent.Updated
                Case "FemaleSelfContent"
                    return FI_FemaleSelfContent.Updated
                Case "ExcludePara"
                    return FI_ExcludePara.Updated
                Case "HoldingRankIDFlag"
                    return FI_HoldingRankIDFlag.Updated
                Case "HoldingRankID"
                    return FI_HoldingRankID.Updated
                Case "PositionFlag"
                    return FI_PositionFlag.Updated
                Case "Position"
                    return FI_Position.Updated
                Case "WorkTypeFlag"
                    return FI_WorkTypeFlag.Updated
                Case "WorkTypeID"
                    return FI_WorkTypeID.Updated
                Case "RotateFlag"
                    return FI_RotateFlag.Updated
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
                Case "Para"
                    return FI_Para.CreateUpdateSQL
                Case "DutyInFlag"
                    return FI_DutyInFlag.CreateUpdateSQL
                Case "DutyInBT"
                    return FI_DutyInBT.CreateUpdateSQL
                Case "DutyOutFlag"
                    return FI_DutyOutFlag.CreateUpdateSQL
                Case "DutyOutBT"
                    return FI_DutyOutBT.CreateUpdateSQL
                Case "PunchInFlag"
                    return FI_PunchInFlag.CreateUpdateSQL
                Case "PunchInBT"
                    return FI_PunchInBT.CreateUpdateSQL
                Case "PunchOutFlag"
                    return FI_PunchOutFlag.CreateUpdateSQL
                Case "PunchOutBT"
                    return FI_PunchOutBT.CreateUpdateSQL
                Case "MsgPara"
                    return FI_MsgPara.CreateUpdateSQL
                Case "PunchInMsgFlag"
                    return FI_PunchInMsgFlag.CreateUpdateSQL
                Case "PunchInDefaultContent"
                    return FI_PunchInDefaultContent.CreateUpdateSQL
                Case "PunchInSelfContent"
                    return FI_PunchInSelfContent.CreateUpdateSQL
                Case "PunchOutMsgFlag"
                    return FI_PunchOutMsgFlag.CreateUpdateSQL
                Case "PunchOutDefaultContent"
                    return FI_PunchOutDefaultContent.CreateUpdateSQL
                Case "PunchOutSelfContent"
                    return FI_PunchOutSelfContent.CreateUpdateSQL
                Case "AffairMsgFlag"
                    return FI_AffairMsgFlag.CreateUpdateSQL
                Case "AffairDefaultContent"
                    return FI_AffairDefaultContent.CreateUpdateSQL
                Case "AffairSelfContent"
                    return FI_AffairSelfContent.CreateUpdateSQL
                Case "OVTenMsgFlag"
                    return FI_OVTenMsgFlag.CreateUpdateSQL
                Case "OVTenDefaultContent"
                    return FI_OVTenDefaultContent.CreateUpdateSQL
                Case "OVTenSelfContent"
                    return FI_OVTenSelfContent.CreateUpdateSQL
                Case "FemaleMsgFlag"
                    return FI_FemaleMsgFlag.CreateUpdateSQL
                Case "FemaleDefaultContent"
                    return FI_FemaleDefaultContent.CreateUpdateSQL
                Case "FemaleSelfContent"
                    return FI_FemaleSelfContent.CreateUpdateSQL
                Case "ExcludePara"
                    return FI_ExcludePara.CreateUpdateSQL
                Case "HoldingRankIDFlag"
                    return FI_HoldingRankIDFlag.CreateUpdateSQL
                Case "HoldingRankID"
                    return FI_HoldingRankID.CreateUpdateSQL
                Case "PositionFlag"
                    return FI_PositionFlag.CreateUpdateSQL
                Case "Position"
                    return FI_Position.CreateUpdateSQL
                Case "WorkTypeFlag"
                    return FI_WorkTypeFlag.CreateUpdateSQL
                Case "WorkTypeID"
                    return FI_WorkTypeID.CreateUpdateSQL
                Case "RotateFlag"
                    return FI_RotateFlag.CreateUpdateSQL
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
            FI_Para.SetInitValue("")
            FI_DutyInFlag.SetInitValue("")
            FI_DutyInBT.SetInitValue("")
            FI_DutyOutFlag.SetInitValue("")
            FI_DutyOutBT.SetInitValue("")
            FI_PunchInFlag.SetInitValue("")
            FI_PunchInBT.SetInitValue("")
            FI_PunchOutFlag.SetInitValue("")
            FI_PunchOutBT.SetInitValue("")
            FI_MsgPara.SetInitValue("")
            FI_PunchInMsgFlag.SetInitValue("")
            FI_PunchInDefaultContent.SetInitValue("")
            FI_PunchInSelfContent.SetInitValue("")
            FI_PunchOutMsgFlag.SetInitValue("")
            FI_PunchOutDefaultContent.SetInitValue("")
            FI_PunchOutSelfContent.SetInitValue("")
            FI_AffairMsgFlag.SetInitValue("")
            FI_AffairDefaultContent.SetInitValue("")
            FI_AffairSelfContent.SetInitValue("")
            FI_OVTenMsgFlag.SetInitValue("")
            FI_OVTenDefaultContent.SetInitValue("")
            FI_OVTenSelfContent.SetInitValue("")
            FI_FemaleMsgFlag.SetInitValue("")
            FI_FemaleDefaultContent.SetInitValue("")
            FI_FemaleSelfContent.SetInitValue("")
            FI_ExcludePara.SetInitValue("")
            FI_HoldingRankIDFlag.SetInitValue("")
            FI_HoldingRankID.SetInitValue("")
            FI_PositionFlag.SetInitValue("")
            FI_Position.SetInitValue("")
            FI_WorkTypeFlag.SetInitValue("")
            FI_WorkTypeID.SetInitValue("")
            FI_RotateFlag.SetInitValue("")
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CompID.SetInitValue(dr("CompID"))
            FI_Para.SetInitValue(dr("Para"))
            FI_DutyInFlag.SetInitValue(dr("DutyInFlag"))
            FI_DutyInBT.SetInitValue(dr("DutyInBT"))
            FI_DutyOutFlag.SetInitValue(dr("DutyOutFlag"))
            FI_DutyOutBT.SetInitValue(dr("DutyOutBT"))
            FI_PunchInFlag.SetInitValue(dr("PunchInFlag"))
            FI_PunchInBT.SetInitValue(dr("PunchInBT"))
            FI_PunchOutFlag.SetInitValue(dr("PunchOutFlag"))
            FI_PunchOutBT.SetInitValue(dr("PunchOutBT"))
            FI_MsgPara.SetInitValue(dr("MsgPara"))
            FI_PunchInMsgFlag.SetInitValue(dr("PunchInMsgFlag"))
            FI_PunchInDefaultContent.SetInitValue(dr("PunchInDefaultContent"))
            FI_PunchInSelfContent.SetInitValue(dr("PunchInSelfContent"))
            FI_PunchOutMsgFlag.SetInitValue(dr("PunchOutMsgFlag"))
            FI_PunchOutDefaultContent.SetInitValue(dr("PunchOutDefaultContent"))
            FI_PunchOutSelfContent.SetInitValue(dr("PunchOutSelfContent"))
            FI_AffairMsgFlag.SetInitValue(dr("AffairMsgFlag"))
            FI_AffairDefaultContent.SetInitValue(dr("AffairDefaultContent"))
            FI_AffairSelfContent.SetInitValue(dr("AffairSelfContent"))
            FI_OVTenMsgFlag.SetInitValue(dr("OVTenMsgFlag"))
            FI_OVTenDefaultContent.SetInitValue(dr("OVTenDefaultContent"))
            FI_OVTenSelfContent.SetInitValue(dr("OVTenSelfContent"))
            FI_FemaleMsgFlag.SetInitValue(dr("FemaleMsgFlag"))
            FI_FemaleDefaultContent.SetInitValue(dr("FemaleDefaultContent"))
            FI_FemaleSelfContent.SetInitValue(dr("FemaleSelfContent"))
            FI_ExcludePara.SetInitValue(dr("ExcludePara"))
            FI_HoldingRankIDFlag.SetInitValue(dr("HoldingRankIDFlag"))
            FI_HoldingRankID.SetInitValue(dr("HoldingRankID"))
            FI_PositionFlag.SetInitValue(dr("PositionFlag"))
            FI_Position.SetInitValue(dr("Position"))
            FI_WorkTypeFlag.SetInitValue(dr("WorkTypeFlag"))
            FI_WorkTypeID.SetInitValue(dr("WorkTypeID"))
            FI_RotateFlag.SetInitValue(dr("RotateFlag"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CompID.Updated = False
            FI_Para.Updated = False
            FI_DutyInFlag.Updated = False
            FI_DutyInBT.Updated = False
            FI_DutyOutFlag.Updated = False
            FI_DutyOutBT.Updated = False
            FI_PunchInFlag.Updated = False
            FI_PunchInBT.Updated = False
            FI_PunchOutFlag.Updated = False
            FI_PunchOutBT.Updated = False
            FI_MsgPara.Updated = False
            FI_PunchInMsgFlag.Updated = False
            FI_PunchInDefaultContent.Updated = False
            FI_PunchInSelfContent.Updated = False
            FI_PunchOutMsgFlag.Updated = False
            FI_PunchOutDefaultContent.Updated = False
            FI_PunchOutSelfContent.Updated = False
            FI_AffairMsgFlag.Updated = False
            FI_AffairDefaultContent.Updated = False
            FI_AffairSelfContent.Updated = False
            FI_OVTenMsgFlag.Updated = False
            FI_OVTenDefaultContent.Updated = False
            FI_OVTenSelfContent.Updated = False
            FI_FemaleMsgFlag.Updated = False
            FI_FemaleDefaultContent.Updated = False
            FI_FemaleSelfContent.Updated = False
            FI_ExcludePara.Updated = False
            FI_HoldingRankIDFlag.Updated = False
            FI_HoldingRankID.Updated = False
            FI_PositionFlag.Updated = False
            FI_Position.Updated = False
            FI_WorkTypeFlag.Updated = False
            FI_WorkTypeID.Updated = False
            FI_RotateFlag.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
        End Sub

        Public ReadOnly Property CompID As Field(Of String) 
            Get
                Return FI_CompID
            End Get
        End Property

        Public ReadOnly Property Para As Field(Of String) 
            Get
                Return FI_Para
            End Get
        End Property

        Public ReadOnly Property DutyInFlag As Field(Of String) 
            Get
                Return FI_DutyInFlag
            End Get
        End Property

        Public ReadOnly Property DutyInBT As Field(Of String) 
            Get
                Return FI_DutyInBT
            End Get
        End Property

        Public ReadOnly Property DutyOutFlag As Field(Of String) 
            Get
                Return FI_DutyOutFlag
            End Get
        End Property

        Public ReadOnly Property DutyOutBT As Field(Of String) 
            Get
                Return FI_DutyOutBT
            End Get
        End Property

        Public ReadOnly Property PunchInFlag As Field(Of String) 
            Get
                Return FI_PunchInFlag
            End Get
        End Property

        Public ReadOnly Property PunchInBT As Field(Of String) 
            Get
                Return FI_PunchInBT
            End Get
        End Property

        Public ReadOnly Property PunchOutFlag As Field(Of String) 
            Get
                Return FI_PunchOutFlag
            End Get
        End Property

        Public ReadOnly Property PunchOutBT As Field(Of String) 
            Get
                Return FI_PunchOutBT
            End Get
        End Property

        Public ReadOnly Property MsgPara As Field(Of String) 
            Get
                Return FI_MsgPara
            End Get
        End Property

        Public ReadOnly Property PunchInMsgFlag As Field(Of String) 
            Get
                Return FI_PunchInMsgFlag
            End Get
        End Property

        Public ReadOnly Property PunchInDefaultContent As Field(Of String) 
            Get
                Return FI_PunchInDefaultContent
            End Get
        End Property

        Public ReadOnly Property PunchInSelfContent As Field(Of String) 
            Get
                Return FI_PunchInSelfContent
            End Get
        End Property

        Public ReadOnly Property PunchOutMsgFlag As Field(Of String) 
            Get
                Return FI_PunchOutMsgFlag
            End Get
        End Property

        Public ReadOnly Property PunchOutDefaultContent As Field(Of String) 
            Get
                Return FI_PunchOutDefaultContent
            End Get
        End Property

        Public ReadOnly Property PunchOutSelfContent As Field(Of String) 
            Get
                Return FI_PunchOutSelfContent
            End Get
        End Property

        Public ReadOnly Property AffairMsgFlag As Field(Of String) 
            Get
                Return FI_AffairMsgFlag
            End Get
        End Property

        Public ReadOnly Property AffairDefaultContent As Field(Of String) 
            Get
                Return FI_AffairDefaultContent
            End Get
        End Property

        Public ReadOnly Property AffairSelfContent As Field(Of String) 
            Get
                Return FI_AffairSelfContent
            End Get
        End Property

        Public ReadOnly Property OVTenMsgFlag As Field(Of String) 
            Get
                Return FI_OVTenMsgFlag
            End Get
        End Property

        Public ReadOnly Property OVTenDefaultContent As Field(Of String) 
            Get
                Return FI_OVTenDefaultContent
            End Get
        End Property

        Public ReadOnly Property OVTenSelfContent As Field(Of String) 
            Get
                Return FI_OVTenSelfContent
            End Get
        End Property

        Public ReadOnly Property FemaleMsgFlag As Field(Of String) 
            Get
                Return FI_FemaleMsgFlag
            End Get
        End Property

        Public ReadOnly Property FemaleDefaultContent As Field(Of String) 
            Get
                Return FI_FemaleDefaultContent
            End Get
        End Property

        Public ReadOnly Property FemaleSelfContent As Field(Of String) 
            Get
                Return FI_FemaleSelfContent
            End Get
        End Property

        Public ReadOnly Property ExcludePara As Field(Of String) 
            Get
                Return FI_ExcludePara
            End Get
        End Property

        Public ReadOnly Property HoldingRankIDFlag As Field(Of String) 
            Get
                Return FI_HoldingRankIDFlag
            End Get
        End Property

        Public ReadOnly Property HoldingRankID As Field(Of String) 
            Get
                Return FI_HoldingRankID
            End Get
        End Property

        Public ReadOnly Property PositionFlag As Field(Of String) 
            Get
                Return FI_PositionFlag
            End Get
        End Property

        Public ReadOnly Property Position As Field(Of String) 
            Get
                Return FI_Position
            End Get
        End Property

        Public ReadOnly Property WorkTypeFlag As Field(Of String) 
            Get
                Return FI_WorkTypeFlag
            End Get
        End Property

        Public ReadOnly Property WorkTypeID As Field(Of String) 
            Get
                Return FI_WorkTypeID
            End Get
        End Property

        Public ReadOnly Property RotateFlag As Field(Of String) 
            Get
                Return FI_RotateFlag
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
        Public Function DeleteRowByPrimaryKey(ByVal PunchParaRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From PunchPara")
            strSQL.AppendLine("Where CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PunchParaRow.CompID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal PunchParaRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From PunchPara")
            strSQL.AppendLine("Where CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PunchParaRow.CompID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal PunchParaRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From PunchPara")
            strSQL.AppendLine("Where CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In PunchParaRow
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

        Public Function DeleteRowByPrimaryKey(ByVal PunchParaRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From PunchPara")
            strSQL.AppendLine("Where CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In PunchParaRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal PunchParaRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From PunchPara")
            strSQL.AppendLine("Where CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PunchParaRow.CompID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(PunchParaRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From PunchPara")
            strSQL.AppendLine("Where CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PunchParaRow.CompID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal PunchParaRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update PunchPara Set")
            For i As Integer = 0 To PunchParaRow.FieldNames.Length - 1
                If Not PunchParaRow.IsIdentityField(PunchParaRow.FieldNames(i)) AndAlso PunchParaRow.IsUpdated(PunchParaRow.FieldNames(i)) AndAlso PunchParaRow.CreateUpdateSQL(PunchParaRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, PunchParaRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If PunchParaRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, PunchParaRow.CompID.Value)
            If PunchParaRow.Para.Updated Then db.AddInParameter(dbcmd, "@Para", DbType.String, PunchParaRow.Para.Value)
            If PunchParaRow.DutyInFlag.Updated Then db.AddInParameter(dbcmd, "@DutyInFlag", DbType.String, PunchParaRow.DutyInFlag.Value)
            If PunchParaRow.DutyInBT.Updated Then db.AddInParameter(dbcmd, "@DutyInBT", DbType.String, PunchParaRow.DutyInBT.Value)
            If PunchParaRow.DutyOutFlag.Updated Then db.AddInParameter(dbcmd, "@DutyOutFlag", DbType.String, PunchParaRow.DutyOutFlag.Value)
            If PunchParaRow.DutyOutBT.Updated Then db.AddInParameter(dbcmd, "@DutyOutBT", DbType.String, PunchParaRow.DutyOutBT.Value)
            If PunchParaRow.PunchInFlag.Updated Then db.AddInParameter(dbcmd, "@PunchInFlag", DbType.String, PunchParaRow.PunchInFlag.Value)
            If PunchParaRow.PunchInBT.Updated Then db.AddInParameter(dbcmd, "@PunchInBT", DbType.String, PunchParaRow.PunchInBT.Value)
            If PunchParaRow.PunchOutFlag.Updated Then db.AddInParameter(dbcmd, "@PunchOutFlag", DbType.String, PunchParaRow.PunchOutFlag.Value)
            If PunchParaRow.PunchOutBT.Updated Then db.AddInParameter(dbcmd, "@PunchOutBT", DbType.String, PunchParaRow.PunchOutBT.Value)
            If PunchParaRow.MsgPara.Updated Then db.AddInParameter(dbcmd, "@MsgPara", DbType.String, PunchParaRow.MsgPara.Value)
            If PunchParaRow.PunchInMsgFlag.Updated Then db.AddInParameter(dbcmd, "@PunchInMsgFlag", DbType.String, PunchParaRow.PunchInMsgFlag.Value)
            If PunchParaRow.PunchInDefaultContent.Updated Then db.AddInParameter(dbcmd, "@PunchInDefaultContent", DbType.String, PunchParaRow.PunchInDefaultContent.Value)
            If PunchParaRow.PunchInSelfContent.Updated Then db.AddInParameter(dbcmd, "@PunchInSelfContent", DbType.String, PunchParaRow.PunchInSelfContent.Value)
            If PunchParaRow.PunchOutMsgFlag.Updated Then db.AddInParameter(dbcmd, "@PunchOutMsgFlag", DbType.String, PunchParaRow.PunchOutMsgFlag.Value)
            If PunchParaRow.PunchOutDefaultContent.Updated Then db.AddInParameter(dbcmd, "@PunchOutDefaultContent", DbType.String, PunchParaRow.PunchOutDefaultContent.Value)
            If PunchParaRow.PunchOutSelfContent.Updated Then db.AddInParameter(dbcmd, "@PunchOutSelfContent", DbType.String, PunchParaRow.PunchOutSelfContent.Value)
            If PunchParaRow.AffairMsgFlag.Updated Then db.AddInParameter(dbcmd, "@AffairMsgFlag", DbType.String, PunchParaRow.AffairMsgFlag.Value)
            If PunchParaRow.AffairDefaultContent.Updated Then db.AddInParameter(dbcmd, "@AffairDefaultContent", DbType.String, PunchParaRow.AffairDefaultContent.Value)
            If PunchParaRow.AffairSelfContent.Updated Then db.AddInParameter(dbcmd, "@AffairSelfContent", DbType.String, PunchParaRow.AffairSelfContent.Value)
            If PunchParaRow.OVTenMsgFlag.Updated Then db.AddInParameter(dbcmd, "@OVTenMsgFlag", DbType.String, PunchParaRow.OVTenMsgFlag.Value)
            If PunchParaRow.OVTenDefaultContent.Updated Then db.AddInParameter(dbcmd, "@OVTenDefaultContent", DbType.String, PunchParaRow.OVTenDefaultContent.Value)
            If PunchParaRow.OVTenSelfContent.Updated Then db.AddInParameter(dbcmd, "@OVTenSelfContent", DbType.String, PunchParaRow.OVTenSelfContent.Value)
            If PunchParaRow.FemaleMsgFlag.Updated Then db.AddInParameter(dbcmd, "@FemaleMsgFlag", DbType.String, PunchParaRow.FemaleMsgFlag.Value)
            If PunchParaRow.FemaleDefaultContent.Updated Then db.AddInParameter(dbcmd, "@FemaleDefaultContent", DbType.String, PunchParaRow.FemaleDefaultContent.Value)
            If PunchParaRow.FemaleSelfContent.Updated Then db.AddInParameter(dbcmd, "@FemaleSelfContent", DbType.String, PunchParaRow.FemaleSelfContent.Value)
            If PunchParaRow.ExcludePara.Updated Then db.AddInParameter(dbcmd, "@ExcludePara", DbType.String, PunchParaRow.ExcludePara.Value)
            If PunchParaRow.HoldingRankIDFlag.Updated Then db.AddInParameter(dbcmd, "@HoldingRankIDFlag", DbType.String, PunchParaRow.HoldingRankIDFlag.Value)
            If PunchParaRow.HoldingRankID.Updated Then db.AddInParameter(dbcmd, "@HoldingRankID", DbType.String, PunchParaRow.HoldingRankID.Value)
            If PunchParaRow.PositionFlag.Updated Then db.AddInParameter(dbcmd, "@PositionFlag", DbType.String, PunchParaRow.PositionFlag.Value)
            If PunchParaRow.Position.Updated Then db.AddInParameter(dbcmd, "@Position", DbType.String, PunchParaRow.Position.Value)
            If PunchParaRow.WorkTypeFlag.Updated Then db.AddInParameter(dbcmd, "@WorkTypeFlag", DbType.String, PunchParaRow.WorkTypeFlag.Value)
            If PunchParaRow.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, PunchParaRow.WorkTypeID.Value)
            If PunchParaRow.RotateFlag.Updated Then db.AddInParameter(dbcmd, "@RotateFlag", DbType.String, PunchParaRow.RotateFlag.Value)
            If PunchParaRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, PunchParaRow.LastChgComp.Value)
            If PunchParaRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, PunchParaRow.LastChgID.Value)
            If PunchParaRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(PunchParaRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), PunchParaRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(PunchParaRow.LoadFromDataRow, PunchParaRow.CompID.OldValue, PunchParaRow.CompID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal PunchParaRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update PunchPara Set")
            For i As Integer = 0 To PunchParaRow.FieldNames.Length - 1
                If Not PunchParaRow.IsIdentityField(PunchParaRow.FieldNames(i)) AndAlso PunchParaRow.IsUpdated(PunchParaRow.FieldNames(i)) AndAlso PunchParaRow.CreateUpdateSQL(PunchParaRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, PunchParaRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If PunchParaRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, PunchParaRow.CompID.Value)
            If PunchParaRow.Para.Updated Then db.AddInParameter(dbcmd, "@Para", DbType.String, PunchParaRow.Para.Value)
            If PunchParaRow.DutyInFlag.Updated Then db.AddInParameter(dbcmd, "@DutyInFlag", DbType.String, PunchParaRow.DutyInFlag.Value)
            If PunchParaRow.DutyInBT.Updated Then db.AddInParameter(dbcmd, "@DutyInBT", DbType.String, PunchParaRow.DutyInBT.Value)
            If PunchParaRow.DutyOutFlag.Updated Then db.AddInParameter(dbcmd, "@DutyOutFlag", DbType.String, PunchParaRow.DutyOutFlag.Value)
            If PunchParaRow.DutyOutBT.Updated Then db.AddInParameter(dbcmd, "@DutyOutBT", DbType.String, PunchParaRow.DutyOutBT.Value)
            If PunchParaRow.PunchInFlag.Updated Then db.AddInParameter(dbcmd, "@PunchInFlag", DbType.String, PunchParaRow.PunchInFlag.Value)
            If PunchParaRow.PunchInBT.Updated Then db.AddInParameter(dbcmd, "@PunchInBT", DbType.String, PunchParaRow.PunchInBT.Value)
            If PunchParaRow.PunchOutFlag.Updated Then db.AddInParameter(dbcmd, "@PunchOutFlag", DbType.String, PunchParaRow.PunchOutFlag.Value)
            If PunchParaRow.PunchOutBT.Updated Then db.AddInParameter(dbcmd, "@PunchOutBT", DbType.String, PunchParaRow.PunchOutBT.Value)
            If PunchParaRow.MsgPara.Updated Then db.AddInParameter(dbcmd, "@MsgPara", DbType.String, PunchParaRow.MsgPara.Value)
            If PunchParaRow.PunchInMsgFlag.Updated Then db.AddInParameter(dbcmd, "@PunchInMsgFlag", DbType.String, PunchParaRow.PunchInMsgFlag.Value)
            If PunchParaRow.PunchInDefaultContent.Updated Then db.AddInParameter(dbcmd, "@PunchInDefaultContent", DbType.String, PunchParaRow.PunchInDefaultContent.Value)
            If PunchParaRow.PunchInSelfContent.Updated Then db.AddInParameter(dbcmd, "@PunchInSelfContent", DbType.String, PunchParaRow.PunchInSelfContent.Value)
            If PunchParaRow.PunchOutMsgFlag.Updated Then db.AddInParameter(dbcmd, "@PunchOutMsgFlag", DbType.String, PunchParaRow.PunchOutMsgFlag.Value)
            If PunchParaRow.PunchOutDefaultContent.Updated Then db.AddInParameter(dbcmd, "@PunchOutDefaultContent", DbType.String, PunchParaRow.PunchOutDefaultContent.Value)
            If PunchParaRow.PunchOutSelfContent.Updated Then db.AddInParameter(dbcmd, "@PunchOutSelfContent", DbType.String, PunchParaRow.PunchOutSelfContent.Value)
            If PunchParaRow.AffairMsgFlag.Updated Then db.AddInParameter(dbcmd, "@AffairMsgFlag", DbType.String, PunchParaRow.AffairMsgFlag.Value)
            If PunchParaRow.AffairDefaultContent.Updated Then db.AddInParameter(dbcmd, "@AffairDefaultContent", DbType.String, PunchParaRow.AffairDefaultContent.Value)
            If PunchParaRow.AffairSelfContent.Updated Then db.AddInParameter(dbcmd, "@AffairSelfContent", DbType.String, PunchParaRow.AffairSelfContent.Value)
            If PunchParaRow.OVTenMsgFlag.Updated Then db.AddInParameter(dbcmd, "@OVTenMsgFlag", DbType.String, PunchParaRow.OVTenMsgFlag.Value)
            If PunchParaRow.OVTenDefaultContent.Updated Then db.AddInParameter(dbcmd, "@OVTenDefaultContent", DbType.String, PunchParaRow.OVTenDefaultContent.Value)
            If PunchParaRow.OVTenSelfContent.Updated Then db.AddInParameter(dbcmd, "@OVTenSelfContent", DbType.String, PunchParaRow.OVTenSelfContent.Value)
            If PunchParaRow.FemaleMsgFlag.Updated Then db.AddInParameter(dbcmd, "@FemaleMsgFlag", DbType.String, PunchParaRow.FemaleMsgFlag.Value)
            If PunchParaRow.FemaleDefaultContent.Updated Then db.AddInParameter(dbcmd, "@FemaleDefaultContent", DbType.String, PunchParaRow.FemaleDefaultContent.Value)
            If PunchParaRow.FemaleSelfContent.Updated Then db.AddInParameter(dbcmd, "@FemaleSelfContent", DbType.String, PunchParaRow.FemaleSelfContent.Value)
            If PunchParaRow.ExcludePara.Updated Then db.AddInParameter(dbcmd, "@ExcludePara", DbType.String, PunchParaRow.ExcludePara.Value)
            If PunchParaRow.HoldingRankIDFlag.Updated Then db.AddInParameter(dbcmd, "@HoldingRankIDFlag", DbType.String, PunchParaRow.HoldingRankIDFlag.Value)
            If PunchParaRow.HoldingRankID.Updated Then db.AddInParameter(dbcmd, "@HoldingRankID", DbType.String, PunchParaRow.HoldingRankID.Value)
            If PunchParaRow.PositionFlag.Updated Then db.AddInParameter(dbcmd, "@PositionFlag", DbType.String, PunchParaRow.PositionFlag.Value)
            If PunchParaRow.Position.Updated Then db.AddInParameter(dbcmd, "@Position", DbType.String, PunchParaRow.Position.Value)
            If PunchParaRow.WorkTypeFlag.Updated Then db.AddInParameter(dbcmd, "@WorkTypeFlag", DbType.String, PunchParaRow.WorkTypeFlag.Value)
            If PunchParaRow.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, PunchParaRow.WorkTypeID.Value)
            If PunchParaRow.RotateFlag.Updated Then db.AddInParameter(dbcmd, "@RotateFlag", DbType.String, PunchParaRow.RotateFlag.Value)
            If PunchParaRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, PunchParaRow.LastChgComp.Value)
            If PunchParaRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, PunchParaRow.LastChgID.Value)
            If PunchParaRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(PunchParaRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), PunchParaRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(PunchParaRow.LoadFromDataRow, PunchParaRow.CompID.OldValue, PunchParaRow.CompID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal PunchParaRow As Row()) As Integer
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
                    For Each r As Row In PunchParaRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update PunchPara Set")
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
                        If r.Para.Updated Then db.AddInParameter(dbcmd, "@Para", DbType.String, r.Para.Value)
                        If r.DutyInFlag.Updated Then db.AddInParameter(dbcmd, "@DutyInFlag", DbType.String, r.DutyInFlag.Value)
                        If r.DutyInBT.Updated Then db.AddInParameter(dbcmd, "@DutyInBT", DbType.String, r.DutyInBT.Value)
                        If r.DutyOutFlag.Updated Then db.AddInParameter(dbcmd, "@DutyOutFlag", DbType.String, r.DutyOutFlag.Value)
                        If r.DutyOutBT.Updated Then db.AddInParameter(dbcmd, "@DutyOutBT", DbType.String, r.DutyOutBT.Value)
                        If r.PunchInFlag.Updated Then db.AddInParameter(dbcmd, "@PunchInFlag", DbType.String, r.PunchInFlag.Value)
                        If r.PunchInBT.Updated Then db.AddInParameter(dbcmd, "@PunchInBT", DbType.String, r.PunchInBT.Value)
                        If r.PunchOutFlag.Updated Then db.AddInParameter(dbcmd, "@PunchOutFlag", DbType.String, r.PunchOutFlag.Value)
                        If r.PunchOutBT.Updated Then db.AddInParameter(dbcmd, "@PunchOutBT", DbType.String, r.PunchOutBT.Value)
                        If r.MsgPara.Updated Then db.AddInParameter(dbcmd, "@MsgPara", DbType.String, r.MsgPara.Value)
                        If r.PunchInMsgFlag.Updated Then db.AddInParameter(dbcmd, "@PunchInMsgFlag", DbType.String, r.PunchInMsgFlag.Value)
                        If r.PunchInDefaultContent.Updated Then db.AddInParameter(dbcmd, "@PunchInDefaultContent", DbType.String, r.PunchInDefaultContent.Value)
                        If r.PunchInSelfContent.Updated Then db.AddInParameter(dbcmd, "@PunchInSelfContent", DbType.String, r.PunchInSelfContent.Value)
                        If r.PunchOutMsgFlag.Updated Then db.AddInParameter(dbcmd, "@PunchOutMsgFlag", DbType.String, r.PunchOutMsgFlag.Value)
                        If r.PunchOutDefaultContent.Updated Then db.AddInParameter(dbcmd, "@PunchOutDefaultContent", DbType.String, r.PunchOutDefaultContent.Value)
                        If r.PunchOutSelfContent.Updated Then db.AddInParameter(dbcmd, "@PunchOutSelfContent", DbType.String, r.PunchOutSelfContent.Value)
                        If r.AffairMsgFlag.Updated Then db.AddInParameter(dbcmd, "@AffairMsgFlag", DbType.String, r.AffairMsgFlag.Value)
                        If r.AffairDefaultContent.Updated Then db.AddInParameter(dbcmd, "@AffairDefaultContent", DbType.String, r.AffairDefaultContent.Value)
                        If r.AffairSelfContent.Updated Then db.AddInParameter(dbcmd, "@AffairSelfContent", DbType.String, r.AffairSelfContent.Value)
                        If r.OVTenMsgFlag.Updated Then db.AddInParameter(dbcmd, "@OVTenMsgFlag", DbType.String, r.OVTenMsgFlag.Value)
                        If r.OVTenDefaultContent.Updated Then db.AddInParameter(dbcmd, "@OVTenDefaultContent", DbType.String, r.OVTenDefaultContent.Value)
                        If r.OVTenSelfContent.Updated Then db.AddInParameter(dbcmd, "@OVTenSelfContent", DbType.String, r.OVTenSelfContent.Value)
                        If r.FemaleMsgFlag.Updated Then db.AddInParameter(dbcmd, "@FemaleMsgFlag", DbType.String, r.FemaleMsgFlag.Value)
                        If r.FemaleDefaultContent.Updated Then db.AddInParameter(dbcmd, "@FemaleDefaultContent", DbType.String, r.FemaleDefaultContent.Value)
                        If r.FemaleSelfContent.Updated Then db.AddInParameter(dbcmd, "@FemaleSelfContent", DbType.String, r.FemaleSelfContent.Value)
                        If r.ExcludePara.Updated Then db.AddInParameter(dbcmd, "@ExcludePara", DbType.String, r.ExcludePara.Value)
                        If r.HoldingRankIDFlag.Updated Then db.AddInParameter(dbcmd, "@HoldingRankIDFlag", DbType.String, r.HoldingRankIDFlag.Value)
                        If r.HoldingRankID.Updated Then db.AddInParameter(dbcmd, "@HoldingRankID", DbType.String, r.HoldingRankID.Value)
                        If r.PositionFlag.Updated Then db.AddInParameter(dbcmd, "@PositionFlag", DbType.String, r.PositionFlag.Value)
                        If r.Position.Updated Then db.AddInParameter(dbcmd, "@Position", DbType.String, r.Position.Value)
                        If r.WorkTypeFlag.Updated Then db.AddInParameter(dbcmd, "@WorkTypeFlag", DbType.String, r.WorkTypeFlag.Value)
                        If r.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                        If r.RotateFlag.Updated Then db.AddInParameter(dbcmd, "@RotateFlag", DbType.String, r.RotateFlag.Value)
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

        Public Function Update(ByVal PunchParaRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In PunchParaRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update PunchPara Set")
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
                If r.Para.Updated Then db.AddInParameter(dbcmd, "@Para", DbType.String, r.Para.Value)
                If r.DutyInFlag.Updated Then db.AddInParameter(dbcmd, "@DutyInFlag", DbType.String, r.DutyInFlag.Value)
                If r.DutyInBT.Updated Then db.AddInParameter(dbcmd, "@DutyInBT", DbType.String, r.DutyInBT.Value)
                If r.DutyOutFlag.Updated Then db.AddInParameter(dbcmd, "@DutyOutFlag", DbType.String, r.DutyOutFlag.Value)
                If r.DutyOutBT.Updated Then db.AddInParameter(dbcmd, "@DutyOutBT", DbType.String, r.DutyOutBT.Value)
                If r.PunchInFlag.Updated Then db.AddInParameter(dbcmd, "@PunchInFlag", DbType.String, r.PunchInFlag.Value)
                If r.PunchInBT.Updated Then db.AddInParameter(dbcmd, "@PunchInBT", DbType.String, r.PunchInBT.Value)
                If r.PunchOutFlag.Updated Then db.AddInParameter(dbcmd, "@PunchOutFlag", DbType.String, r.PunchOutFlag.Value)
                If r.PunchOutBT.Updated Then db.AddInParameter(dbcmd, "@PunchOutBT", DbType.String, r.PunchOutBT.Value)
                If r.MsgPara.Updated Then db.AddInParameter(dbcmd, "@MsgPara", DbType.String, r.MsgPara.Value)
                If r.PunchInMsgFlag.Updated Then db.AddInParameter(dbcmd, "@PunchInMsgFlag", DbType.String, r.PunchInMsgFlag.Value)
                If r.PunchInDefaultContent.Updated Then db.AddInParameter(dbcmd, "@PunchInDefaultContent", DbType.String, r.PunchInDefaultContent.Value)
                If r.PunchInSelfContent.Updated Then db.AddInParameter(dbcmd, "@PunchInSelfContent", DbType.String, r.PunchInSelfContent.Value)
                If r.PunchOutMsgFlag.Updated Then db.AddInParameter(dbcmd, "@PunchOutMsgFlag", DbType.String, r.PunchOutMsgFlag.Value)
                If r.PunchOutDefaultContent.Updated Then db.AddInParameter(dbcmd, "@PunchOutDefaultContent", DbType.String, r.PunchOutDefaultContent.Value)
                If r.PunchOutSelfContent.Updated Then db.AddInParameter(dbcmd, "@PunchOutSelfContent", DbType.String, r.PunchOutSelfContent.Value)
                If r.AffairMsgFlag.Updated Then db.AddInParameter(dbcmd, "@AffairMsgFlag", DbType.String, r.AffairMsgFlag.Value)
                If r.AffairDefaultContent.Updated Then db.AddInParameter(dbcmd, "@AffairDefaultContent", DbType.String, r.AffairDefaultContent.Value)
                If r.AffairSelfContent.Updated Then db.AddInParameter(dbcmd, "@AffairSelfContent", DbType.String, r.AffairSelfContent.Value)
                If r.OVTenMsgFlag.Updated Then db.AddInParameter(dbcmd, "@OVTenMsgFlag", DbType.String, r.OVTenMsgFlag.Value)
                If r.OVTenDefaultContent.Updated Then db.AddInParameter(dbcmd, "@OVTenDefaultContent", DbType.String, r.OVTenDefaultContent.Value)
                If r.OVTenSelfContent.Updated Then db.AddInParameter(dbcmd, "@OVTenSelfContent", DbType.String, r.OVTenSelfContent.Value)
                If r.FemaleMsgFlag.Updated Then db.AddInParameter(dbcmd, "@FemaleMsgFlag", DbType.String, r.FemaleMsgFlag.Value)
                If r.FemaleDefaultContent.Updated Then db.AddInParameter(dbcmd, "@FemaleDefaultContent", DbType.String, r.FemaleDefaultContent.Value)
                If r.FemaleSelfContent.Updated Then db.AddInParameter(dbcmd, "@FemaleSelfContent", DbType.String, r.FemaleSelfContent.Value)
                If r.ExcludePara.Updated Then db.AddInParameter(dbcmd, "@ExcludePara", DbType.String, r.ExcludePara.Value)
                If r.HoldingRankIDFlag.Updated Then db.AddInParameter(dbcmd, "@HoldingRankIDFlag", DbType.String, r.HoldingRankIDFlag.Value)
                If r.HoldingRankID.Updated Then db.AddInParameter(dbcmd, "@HoldingRankID", DbType.String, r.HoldingRankID.Value)
                If r.PositionFlag.Updated Then db.AddInParameter(dbcmd, "@PositionFlag", DbType.String, r.PositionFlag.Value)
                If r.Position.Updated Then db.AddInParameter(dbcmd, "@Position", DbType.String, r.Position.Value)
                If r.WorkTypeFlag.Updated Then db.AddInParameter(dbcmd, "@WorkTypeFlag", DbType.String, r.WorkTypeFlag.Value)
                If r.WorkTypeID.Updated Then db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                If r.RotateFlag.Updated Then db.AddInParameter(dbcmd, "@RotateFlag", DbType.String, r.RotateFlag.Value)
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal PunchParaRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From PunchPara")
            strSQL.AppendLine("Where CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PunchParaRow.CompID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal PunchParaRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From PunchPara")
            strSQL.AppendLine("Where CompID = @CompID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PunchParaRow.CompID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From PunchPara")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal PunchParaRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into PunchPara")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, Para, DutyInFlag, DutyInBT, DutyOutFlag, DutyOutBT, PunchInFlag, PunchInBT,")
            strSQL.AppendLine("    PunchOutFlag, PunchOutBT, MsgPara, PunchInMsgFlag, PunchInDefaultContent, PunchInSelfContent,")
            strSQL.AppendLine("    PunchOutMsgFlag, PunchOutDefaultContent, PunchOutSelfContent, AffairMsgFlag, AffairDefaultContent,")
            strSQL.AppendLine("    AffairSelfContent, OVTenMsgFlag, OVTenDefaultContent, OVTenSelfContent, FemaleMsgFlag,")
            strSQL.AppendLine("    FemaleDefaultContent, FemaleSelfContent, ExcludePara, HoldingRankIDFlag, HoldingRankID,")
            strSQL.AppendLine("    PositionFlag, Position, WorkTypeFlag, WorkTypeID, RotateFlag, LastChgComp, LastChgID,")
            strSQL.AppendLine("    LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @Para, @DutyInFlag, @DutyInBT, @DutyOutFlag, @DutyOutBT, @PunchInFlag, @PunchInBT,")
            strSQL.AppendLine("    @PunchOutFlag, @PunchOutBT, @MsgPara, @PunchInMsgFlag, @PunchInDefaultContent, @PunchInSelfContent,")
            strSQL.AppendLine("    @PunchOutMsgFlag, @PunchOutDefaultContent, @PunchOutSelfContent, @AffairMsgFlag, @AffairDefaultContent,")
            strSQL.AppendLine("    @AffairSelfContent, @OVTenMsgFlag, @OVTenDefaultContent, @OVTenSelfContent, @FemaleMsgFlag,")
            strSQL.AppendLine("    @FemaleDefaultContent, @FemaleSelfContent, @ExcludePara, @HoldingRankIDFlag, @HoldingRankID,")
            strSQL.AppendLine("    @PositionFlag, @Position, @WorkTypeFlag, @WorkTypeID, @RotateFlag, @LastChgComp, @LastChgID,")
            strSQL.AppendLine("    @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PunchParaRow.CompID.Value)
            db.AddInParameter(dbcmd, "@Para", DbType.String, PunchParaRow.Para.Value)
            db.AddInParameter(dbcmd, "@DutyInFlag", DbType.String, PunchParaRow.DutyInFlag.Value)
            db.AddInParameter(dbcmd, "@DutyInBT", DbType.String, PunchParaRow.DutyInBT.Value)
            db.AddInParameter(dbcmd, "@DutyOutFlag", DbType.String, PunchParaRow.DutyOutFlag.Value)
            db.AddInParameter(dbcmd, "@DutyOutBT", DbType.String, PunchParaRow.DutyOutBT.Value)
            db.AddInParameter(dbcmd, "@PunchInFlag", DbType.String, PunchParaRow.PunchInFlag.Value)
            db.AddInParameter(dbcmd, "@PunchInBT", DbType.String, PunchParaRow.PunchInBT.Value)
            db.AddInParameter(dbcmd, "@PunchOutFlag", DbType.String, PunchParaRow.PunchOutFlag.Value)
            db.AddInParameter(dbcmd, "@PunchOutBT", DbType.String, PunchParaRow.PunchOutBT.Value)
            db.AddInParameter(dbcmd, "@MsgPara", DbType.String, PunchParaRow.MsgPara.Value)
            db.AddInParameter(dbcmd, "@PunchInMsgFlag", DbType.String, PunchParaRow.PunchInMsgFlag.Value)
            db.AddInParameter(dbcmd, "@PunchInDefaultContent", DbType.String, PunchParaRow.PunchInDefaultContent.Value)
            db.AddInParameter(dbcmd, "@PunchInSelfContent", DbType.String, PunchParaRow.PunchInSelfContent.Value)
            db.AddInParameter(dbcmd, "@PunchOutMsgFlag", DbType.String, PunchParaRow.PunchOutMsgFlag.Value)
            db.AddInParameter(dbcmd, "@PunchOutDefaultContent", DbType.String, PunchParaRow.PunchOutDefaultContent.Value)
            db.AddInParameter(dbcmd, "@PunchOutSelfContent", DbType.String, PunchParaRow.PunchOutSelfContent.Value)
            db.AddInParameter(dbcmd, "@AffairMsgFlag", DbType.String, PunchParaRow.AffairMsgFlag.Value)
            db.AddInParameter(dbcmd, "@AffairDefaultContent", DbType.String, PunchParaRow.AffairDefaultContent.Value)
            db.AddInParameter(dbcmd, "@AffairSelfContent", DbType.String, PunchParaRow.AffairSelfContent.Value)
            db.AddInParameter(dbcmd, "@OVTenMsgFlag", DbType.String, PunchParaRow.OVTenMsgFlag.Value)
            db.AddInParameter(dbcmd, "@OVTenDefaultContent", DbType.String, PunchParaRow.OVTenDefaultContent.Value)
            db.AddInParameter(dbcmd, "@OVTenSelfContent", DbType.String, PunchParaRow.OVTenSelfContent.Value)
            db.AddInParameter(dbcmd, "@FemaleMsgFlag", DbType.String, PunchParaRow.FemaleMsgFlag.Value)
            db.AddInParameter(dbcmd, "@FemaleDefaultContent", DbType.String, PunchParaRow.FemaleDefaultContent.Value)
            db.AddInParameter(dbcmd, "@FemaleSelfContent", DbType.String, PunchParaRow.FemaleSelfContent.Value)
            db.AddInParameter(dbcmd, "@ExcludePara", DbType.String, PunchParaRow.ExcludePara.Value)
            db.AddInParameter(dbcmd, "@HoldingRankIDFlag", DbType.String, PunchParaRow.HoldingRankIDFlag.Value)
            db.AddInParameter(dbcmd, "@HoldingRankID", DbType.String, PunchParaRow.HoldingRankID.Value)
            db.AddInParameter(dbcmd, "@PositionFlag", DbType.String, PunchParaRow.PositionFlag.Value)
            db.AddInParameter(dbcmd, "@Position", DbType.String, PunchParaRow.Position.Value)
            db.AddInParameter(dbcmd, "@WorkTypeFlag", DbType.String, PunchParaRow.WorkTypeFlag.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, PunchParaRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@RotateFlag", DbType.String, PunchParaRow.RotateFlag.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, PunchParaRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, PunchParaRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(PunchParaRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), PunchParaRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal PunchParaRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into PunchPara")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, Para, DutyInFlag, DutyInBT, DutyOutFlag, DutyOutBT, PunchInFlag, PunchInBT,")
            strSQL.AppendLine("    PunchOutFlag, PunchOutBT, MsgPara, PunchInMsgFlag, PunchInDefaultContent, PunchInSelfContent,")
            strSQL.AppendLine("    PunchOutMsgFlag, PunchOutDefaultContent, PunchOutSelfContent, AffairMsgFlag, AffairDefaultContent,")
            strSQL.AppendLine("    AffairSelfContent, OVTenMsgFlag, OVTenDefaultContent, OVTenSelfContent, FemaleMsgFlag,")
            strSQL.AppendLine("    FemaleDefaultContent, FemaleSelfContent, ExcludePara, HoldingRankIDFlag, HoldingRankID,")
            strSQL.AppendLine("    PositionFlag, Position, WorkTypeFlag, WorkTypeID, RotateFlag, LastChgComp, LastChgID,")
            strSQL.AppendLine("    LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @Para, @DutyInFlag, @DutyInBT, @DutyOutFlag, @DutyOutBT, @PunchInFlag, @PunchInBT,")
            strSQL.AppendLine("    @PunchOutFlag, @PunchOutBT, @MsgPara, @PunchInMsgFlag, @PunchInDefaultContent, @PunchInSelfContent,")
            strSQL.AppendLine("    @PunchOutMsgFlag, @PunchOutDefaultContent, @PunchOutSelfContent, @AffairMsgFlag, @AffairDefaultContent,")
            strSQL.AppendLine("    @AffairSelfContent, @OVTenMsgFlag, @OVTenDefaultContent, @OVTenSelfContent, @FemaleMsgFlag,")
            strSQL.AppendLine("    @FemaleDefaultContent, @FemaleSelfContent, @ExcludePara, @HoldingRankIDFlag, @HoldingRankID,")
            strSQL.AppendLine("    @PositionFlag, @Position, @WorkTypeFlag, @WorkTypeID, @RotateFlag, @LastChgComp, @LastChgID,")
            strSQL.AppendLine("    @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, PunchParaRow.CompID.Value)
            db.AddInParameter(dbcmd, "@Para", DbType.String, PunchParaRow.Para.Value)
            db.AddInParameter(dbcmd, "@DutyInFlag", DbType.String, PunchParaRow.DutyInFlag.Value)
            db.AddInParameter(dbcmd, "@DutyInBT", DbType.String, PunchParaRow.DutyInBT.Value)
            db.AddInParameter(dbcmd, "@DutyOutFlag", DbType.String, PunchParaRow.DutyOutFlag.Value)
            db.AddInParameter(dbcmd, "@DutyOutBT", DbType.String, PunchParaRow.DutyOutBT.Value)
            db.AddInParameter(dbcmd, "@PunchInFlag", DbType.String, PunchParaRow.PunchInFlag.Value)
            db.AddInParameter(dbcmd, "@PunchInBT", DbType.String, PunchParaRow.PunchInBT.Value)
            db.AddInParameter(dbcmd, "@PunchOutFlag", DbType.String, PunchParaRow.PunchOutFlag.Value)
            db.AddInParameter(dbcmd, "@PunchOutBT", DbType.String, PunchParaRow.PunchOutBT.Value)
            db.AddInParameter(dbcmd, "@MsgPara", DbType.String, PunchParaRow.MsgPara.Value)
            db.AddInParameter(dbcmd, "@PunchInMsgFlag", DbType.String, PunchParaRow.PunchInMsgFlag.Value)
            db.AddInParameter(dbcmd, "@PunchInDefaultContent", DbType.String, PunchParaRow.PunchInDefaultContent.Value)
            db.AddInParameter(dbcmd, "@PunchInSelfContent", DbType.String, PunchParaRow.PunchInSelfContent.Value)
            db.AddInParameter(dbcmd, "@PunchOutMsgFlag", DbType.String, PunchParaRow.PunchOutMsgFlag.Value)
            db.AddInParameter(dbcmd, "@PunchOutDefaultContent", DbType.String, PunchParaRow.PunchOutDefaultContent.Value)
            db.AddInParameter(dbcmd, "@PunchOutSelfContent", DbType.String, PunchParaRow.PunchOutSelfContent.Value)
            db.AddInParameter(dbcmd, "@AffairMsgFlag", DbType.String, PunchParaRow.AffairMsgFlag.Value)
            db.AddInParameter(dbcmd, "@AffairDefaultContent", DbType.String, PunchParaRow.AffairDefaultContent.Value)
            db.AddInParameter(dbcmd, "@AffairSelfContent", DbType.String, PunchParaRow.AffairSelfContent.Value)
            db.AddInParameter(dbcmd, "@OVTenMsgFlag", DbType.String, PunchParaRow.OVTenMsgFlag.Value)
            db.AddInParameter(dbcmd, "@OVTenDefaultContent", DbType.String, PunchParaRow.OVTenDefaultContent.Value)
            db.AddInParameter(dbcmd, "@OVTenSelfContent", DbType.String, PunchParaRow.OVTenSelfContent.Value)
            db.AddInParameter(dbcmd, "@FemaleMsgFlag", DbType.String, PunchParaRow.FemaleMsgFlag.Value)
            db.AddInParameter(dbcmd, "@FemaleDefaultContent", DbType.String, PunchParaRow.FemaleDefaultContent.Value)
            db.AddInParameter(dbcmd, "@FemaleSelfContent", DbType.String, PunchParaRow.FemaleSelfContent.Value)
            db.AddInParameter(dbcmd, "@ExcludePara", DbType.String, PunchParaRow.ExcludePara.Value)
            db.AddInParameter(dbcmd, "@HoldingRankIDFlag", DbType.String, PunchParaRow.HoldingRankIDFlag.Value)
            db.AddInParameter(dbcmd, "@HoldingRankID", DbType.String, PunchParaRow.HoldingRankID.Value)
            db.AddInParameter(dbcmd, "@PositionFlag", DbType.String, PunchParaRow.PositionFlag.Value)
            db.AddInParameter(dbcmd, "@Position", DbType.String, PunchParaRow.Position.Value)
            db.AddInParameter(dbcmd, "@WorkTypeFlag", DbType.String, PunchParaRow.WorkTypeFlag.Value)
            db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, PunchParaRow.WorkTypeID.Value)
            db.AddInParameter(dbcmd, "@RotateFlag", DbType.String, PunchParaRow.RotateFlag.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, PunchParaRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, PunchParaRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(PunchParaRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), PunchParaRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal PunchParaRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into PunchPara")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, Para, DutyInFlag, DutyInBT, DutyOutFlag, DutyOutBT, PunchInFlag, PunchInBT,")
            strSQL.AppendLine("    PunchOutFlag, PunchOutBT, MsgPara, PunchInMsgFlag, PunchInDefaultContent, PunchInSelfContent,")
            strSQL.AppendLine("    PunchOutMsgFlag, PunchOutDefaultContent, PunchOutSelfContent, AffairMsgFlag, AffairDefaultContent,")
            strSQL.AppendLine("    AffairSelfContent, OVTenMsgFlag, OVTenDefaultContent, OVTenSelfContent, FemaleMsgFlag,")
            strSQL.AppendLine("    FemaleDefaultContent, FemaleSelfContent, ExcludePara, HoldingRankIDFlag, HoldingRankID,")
            strSQL.AppendLine("    PositionFlag, Position, WorkTypeFlag, WorkTypeID, RotateFlag, LastChgComp, LastChgID,")
            strSQL.AppendLine("    LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @Para, @DutyInFlag, @DutyInBT, @DutyOutFlag, @DutyOutBT, @PunchInFlag, @PunchInBT,")
            strSQL.AppendLine("    @PunchOutFlag, @PunchOutBT, @MsgPara, @PunchInMsgFlag, @PunchInDefaultContent, @PunchInSelfContent,")
            strSQL.AppendLine("    @PunchOutMsgFlag, @PunchOutDefaultContent, @PunchOutSelfContent, @AffairMsgFlag, @AffairDefaultContent,")
            strSQL.AppendLine("    @AffairSelfContent, @OVTenMsgFlag, @OVTenDefaultContent, @OVTenSelfContent, @FemaleMsgFlag,")
            strSQL.AppendLine("    @FemaleDefaultContent, @FemaleSelfContent, @ExcludePara, @HoldingRankIDFlag, @HoldingRankID,")
            strSQL.AppendLine("    @PositionFlag, @Position, @WorkTypeFlag, @WorkTypeID, @RotateFlag, @LastChgComp, @LastChgID,")
            strSQL.AppendLine("    @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In PunchParaRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@Para", DbType.String, r.Para.Value)
                        db.AddInParameter(dbcmd, "@DutyInFlag", DbType.String, r.DutyInFlag.Value)
                        db.AddInParameter(dbcmd, "@DutyInBT", DbType.String, r.DutyInBT.Value)
                        db.AddInParameter(dbcmd, "@DutyOutFlag", DbType.String, r.DutyOutFlag.Value)
                        db.AddInParameter(dbcmd, "@DutyOutBT", DbType.String, r.DutyOutBT.Value)
                        db.AddInParameter(dbcmd, "@PunchInFlag", DbType.String, r.PunchInFlag.Value)
                        db.AddInParameter(dbcmd, "@PunchInBT", DbType.String, r.PunchInBT.Value)
                        db.AddInParameter(dbcmd, "@PunchOutFlag", DbType.String, r.PunchOutFlag.Value)
                        db.AddInParameter(dbcmd, "@PunchOutBT", DbType.String, r.PunchOutBT.Value)
                        db.AddInParameter(dbcmd, "@MsgPara", DbType.String, r.MsgPara.Value)
                        db.AddInParameter(dbcmd, "@PunchInMsgFlag", DbType.String, r.PunchInMsgFlag.Value)
                        db.AddInParameter(dbcmd, "@PunchInDefaultContent", DbType.String, r.PunchInDefaultContent.Value)
                        db.AddInParameter(dbcmd, "@PunchInSelfContent", DbType.String, r.PunchInSelfContent.Value)
                        db.AddInParameter(dbcmd, "@PunchOutMsgFlag", DbType.String, r.PunchOutMsgFlag.Value)
                        db.AddInParameter(dbcmd, "@PunchOutDefaultContent", DbType.String, r.PunchOutDefaultContent.Value)
                        db.AddInParameter(dbcmd, "@PunchOutSelfContent", DbType.String, r.PunchOutSelfContent.Value)
                        db.AddInParameter(dbcmd, "@AffairMsgFlag", DbType.String, r.AffairMsgFlag.Value)
                        db.AddInParameter(dbcmd, "@AffairDefaultContent", DbType.String, r.AffairDefaultContent.Value)
                        db.AddInParameter(dbcmd, "@AffairSelfContent", DbType.String, r.AffairSelfContent.Value)
                        db.AddInParameter(dbcmd, "@OVTenMsgFlag", DbType.String, r.OVTenMsgFlag.Value)
                        db.AddInParameter(dbcmd, "@OVTenDefaultContent", DbType.String, r.OVTenDefaultContent.Value)
                        db.AddInParameter(dbcmd, "@OVTenSelfContent", DbType.String, r.OVTenSelfContent.Value)
                        db.AddInParameter(dbcmd, "@FemaleMsgFlag", DbType.String, r.FemaleMsgFlag.Value)
                        db.AddInParameter(dbcmd, "@FemaleDefaultContent", DbType.String, r.FemaleDefaultContent.Value)
                        db.AddInParameter(dbcmd, "@FemaleSelfContent", DbType.String, r.FemaleSelfContent.Value)
                        db.AddInParameter(dbcmd, "@ExcludePara", DbType.String, r.ExcludePara.Value)
                        db.AddInParameter(dbcmd, "@HoldingRankIDFlag", DbType.String, r.HoldingRankIDFlag.Value)
                        db.AddInParameter(dbcmd, "@HoldingRankID", DbType.String, r.HoldingRankID.Value)
                        db.AddInParameter(dbcmd, "@PositionFlag", DbType.String, r.PositionFlag.Value)
                        db.AddInParameter(dbcmd, "@Position", DbType.String, r.Position.Value)
                        db.AddInParameter(dbcmd, "@WorkTypeFlag", DbType.String, r.WorkTypeFlag.Value)
                        db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                        db.AddInParameter(dbcmd, "@RotateFlag", DbType.String, r.RotateFlag.Value)
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

        Public Function Insert(ByVal PunchParaRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into PunchPara")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, Para, DutyInFlag, DutyInBT, DutyOutFlag, DutyOutBT, PunchInFlag, PunchInBT,")
            strSQL.AppendLine("    PunchOutFlag, PunchOutBT, MsgPara, PunchInMsgFlag, PunchInDefaultContent, PunchInSelfContent,")
            strSQL.AppendLine("    PunchOutMsgFlag, PunchOutDefaultContent, PunchOutSelfContent, AffairMsgFlag, AffairDefaultContent,")
            strSQL.AppendLine("    AffairSelfContent, OVTenMsgFlag, OVTenDefaultContent, OVTenSelfContent, FemaleMsgFlag,")
            strSQL.AppendLine("    FemaleDefaultContent, FemaleSelfContent, ExcludePara, HoldingRankIDFlag, HoldingRankID,")
            strSQL.AppendLine("    PositionFlag, Position, WorkTypeFlag, WorkTypeID, RotateFlag, LastChgComp, LastChgID,")
            strSQL.AppendLine("    LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @Para, @DutyInFlag, @DutyInBT, @DutyOutFlag, @DutyOutBT, @PunchInFlag, @PunchInBT,")
            strSQL.AppendLine("    @PunchOutFlag, @PunchOutBT, @MsgPara, @PunchInMsgFlag, @PunchInDefaultContent, @PunchInSelfContent,")
            strSQL.AppendLine("    @PunchOutMsgFlag, @PunchOutDefaultContent, @PunchOutSelfContent, @AffairMsgFlag, @AffairDefaultContent,")
            strSQL.AppendLine("    @AffairSelfContent, @OVTenMsgFlag, @OVTenDefaultContent, @OVTenSelfContent, @FemaleMsgFlag,")
            strSQL.AppendLine("    @FemaleDefaultContent, @FemaleSelfContent, @ExcludePara, @HoldingRankIDFlag, @HoldingRankID,")
            strSQL.AppendLine("    @PositionFlag, @Position, @WorkTypeFlag, @WorkTypeID, @RotateFlag, @LastChgComp, @LastChgID,")
            strSQL.AppendLine("    @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In PunchParaRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@Para", DbType.String, r.Para.Value)
                db.AddInParameter(dbcmd, "@DutyInFlag", DbType.String, r.DutyInFlag.Value)
                db.AddInParameter(dbcmd, "@DutyInBT", DbType.String, r.DutyInBT.Value)
                db.AddInParameter(dbcmd, "@DutyOutFlag", DbType.String, r.DutyOutFlag.Value)
                db.AddInParameter(dbcmd, "@DutyOutBT", DbType.String, r.DutyOutBT.Value)
                db.AddInParameter(dbcmd, "@PunchInFlag", DbType.String, r.PunchInFlag.Value)
                db.AddInParameter(dbcmd, "@PunchInBT", DbType.String, r.PunchInBT.Value)
                db.AddInParameter(dbcmd, "@PunchOutFlag", DbType.String, r.PunchOutFlag.Value)
                db.AddInParameter(dbcmd, "@PunchOutBT", DbType.String, r.PunchOutBT.Value)
                db.AddInParameter(dbcmd, "@MsgPara", DbType.String, r.MsgPara.Value)
                db.AddInParameter(dbcmd, "@PunchInMsgFlag", DbType.String, r.PunchInMsgFlag.Value)
                db.AddInParameter(dbcmd, "@PunchInDefaultContent", DbType.String, r.PunchInDefaultContent.Value)
                db.AddInParameter(dbcmd, "@PunchInSelfContent", DbType.String, r.PunchInSelfContent.Value)
                db.AddInParameter(dbcmd, "@PunchOutMsgFlag", DbType.String, r.PunchOutMsgFlag.Value)
                db.AddInParameter(dbcmd, "@PunchOutDefaultContent", DbType.String, r.PunchOutDefaultContent.Value)
                db.AddInParameter(dbcmd, "@PunchOutSelfContent", DbType.String, r.PunchOutSelfContent.Value)
                db.AddInParameter(dbcmd, "@AffairMsgFlag", DbType.String, r.AffairMsgFlag.Value)
                db.AddInParameter(dbcmd, "@AffairDefaultContent", DbType.String, r.AffairDefaultContent.Value)
                db.AddInParameter(dbcmd, "@AffairSelfContent", DbType.String, r.AffairSelfContent.Value)
                db.AddInParameter(dbcmd, "@OVTenMsgFlag", DbType.String, r.OVTenMsgFlag.Value)
                db.AddInParameter(dbcmd, "@OVTenDefaultContent", DbType.String, r.OVTenDefaultContent.Value)
                db.AddInParameter(dbcmd, "@OVTenSelfContent", DbType.String, r.OVTenSelfContent.Value)
                db.AddInParameter(dbcmd, "@FemaleMsgFlag", DbType.String, r.FemaleMsgFlag.Value)
                db.AddInParameter(dbcmd, "@FemaleDefaultContent", DbType.String, r.FemaleDefaultContent.Value)
                db.AddInParameter(dbcmd, "@FemaleSelfContent", DbType.String, r.FemaleSelfContent.Value)
                db.AddInParameter(dbcmd, "@ExcludePara", DbType.String, r.ExcludePara.Value)
                db.AddInParameter(dbcmd, "@HoldingRankIDFlag", DbType.String, r.HoldingRankIDFlag.Value)
                db.AddInParameter(dbcmd, "@HoldingRankID", DbType.String, r.HoldingRankID.Value)
                db.AddInParameter(dbcmd, "@PositionFlag", DbType.String, r.PositionFlag.Value)
                db.AddInParameter(dbcmd, "@Position", DbType.String, r.Position.Value)
                db.AddInParameter(dbcmd, "@WorkTypeFlag", DbType.String, r.WorkTypeFlag.Value)
                db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, r.WorkTypeID.Value)
                db.AddInParameter(dbcmd, "@RotateFlag", DbType.String, r.RotateFlag.Value)
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

