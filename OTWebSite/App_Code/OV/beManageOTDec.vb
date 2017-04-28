'****************************************************************  
' Subject：OV4200系列-加班管理-事後申報
' Author：Jason
' Table：OverTimeDeclaration
' Description：加班管理-事後申報資料庫操作相關
' Created Date：2017.1.12
' Modify Date：2017.3.11
'****************************************************************

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beManageOTDec
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = {"OTCompID", "OTEmpID", "OTStartDate", "OTEndDate", "OTSeq", "OTTxnID", "OTSeqNo", "OTFromAdvanceTxnId", "DeptID", "OrganID", _
                                        "DeptName", "OrganName", "FlowCaseID", "OTStartTime", "OTEndTime", "OTTotalTime", "SalaryOrAdjust", "AdjustInvalidDate", "AdjustStatus", "AdjustDate", _
                                        "MealFlag", "MealTime", "OTTypeID", "OTReasonID", "OTReasonMemo", "OTAttachment", "OTFormNO", "OTRegisterID", "OTRegisterDate", "OTStatus", _
                                        "OTValidDate", "OTValidID", "OverTimeFlag", "ToOverTimeDate", "ToOverTimeFlag", "OTRejectDate", "OTRejectID", "OTGovernmentNo", "OTSalaryPaid", "HolidayOrNot", "ProcessDate", "OTPayDate", "OTModifyDate", _
                                        "OTRemark", "KeyInComp", "KeyInID", "HRKeyInFlag", "LastChgComp", "LastChgID", "LastChgDate", "OTRegisterComp"}
        Private m_Types As System.Type() = {GetType(String), GetType(String), GetType(String), GetType(String), GetType(Integer), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), _
                                            GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Integer), GetType(String), GetType(Date), GetType(String), GetType(Date), _
                                            GetType(String), GetType(Integer), GetType(String), GetType(Integer), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(String), _
                                            GetType(Date), GetType(String), GetType(String), GetType(Date), GetType(String), GetType(Date), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(Integer), GetType(Date), _
                                            GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(String)}
        Private m_PrimaryFields As String() = {"OTCompID", "OTEmpID", "OTStartDate", "OTEndDate", "OTSeq"}

        Public ReadOnly Property Rows() As beManageOTDec.Rows
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
        Public Sub Transfer2Row(ManageOTDecTable As DataTable)
            For Each dr As DataRow In ManageOTDecTable.Rows
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

                dr(m_Rows(i).OTCompID.FieldName) = m_Rows(i).OTCompID.Value
                dr(m_Rows(i).OTEmpID.FieldName) = m_Rows(i).OTEmpID.Value
                dr(m_Rows(i).OTStartDate.FieldName) = m_Rows(i).OTStartDate.Value
                dr(m_Rows(i).OTEndDate.FieldName) = m_Rows(i).OTEndDate.Value
                dr(m_Rows(i).OTSeq.FieldName) = m_Rows(i).OTSeq.Value
                dr(m_Rows(i).OTTxnID.FieldName) = m_Rows(i).OTTxnID.Value
                dr(m_Rows(i).OTSeqNo.FieldName) = m_Rows(i).OTSeqNo.Value
                dr(m_Rows(i).OTFromAdvanceTxnId.FieldName) = m_Rows(i).OTFromAdvanceTxnId.Value
                dr(m_Rows(i).DeptID.FieldName) = m_Rows(i).DeptID.Value
                dr(m_Rows(i).OrganID.FieldName) = m_Rows(i).OrganID.Value

                dr(m_Rows(i).DeptName.FieldName) = m_Rows(i).DeptName.Value
                dr(m_Rows(i).OrganName.FieldName) = m_Rows(i).OrganName.Value
                dr(m_Rows(i).FlowCaseID.FieldName) = m_Rows(i).FlowCaseID.Value
                dr(m_Rows(i).OTStartTime.FieldName) = m_Rows(i).OTStartTime.Value
                dr(m_Rows(i).OTEndTime.FieldName) = m_Rows(i).OTEndTime.Value
                dr(m_Rows(i).OTTotalTime.FieldName) = m_Rows(i).OTTotalTime.Value
                dr(m_Rows(i).SalaryOrAdjust.FieldName) = m_Rows(i).SalaryOrAdjust.Value
                dr(m_Rows(i).AdjustInvalidDate.FieldName) = m_Rows(i).AdjustInvalidDate.Value
                dr(m_Rows(i).AdjustStatus.FieldName) = m_Rows(i).AdjustStatus.Value
                dr(m_Rows(i).AdjustDate.FieldName) = m_Rows(i).AdjustDate.Value

                dr(m_Rows(i).MealFlag.FieldName) = m_Rows(i).MealFlag.Value
                dr(m_Rows(i).MealTime.FieldName) = m_Rows(i).MealTime.Value
                dr(m_Rows(i).OTTypeID.FieldName) = m_Rows(i).OTTypeID.Value
                dr(m_Rows(i).OTReasonID.FieldName) = m_Rows(i).OTReasonID.Value
                dr(m_Rows(i).OTReasonMemo.FieldName) = m_Rows(i).OTReasonMemo.Value
                dr(m_Rows(i).OTAttachment.FieldName) = m_Rows(i).OTAttachment.Value
                dr(m_Rows(i).OTFormNO.FieldName) = m_Rows(i).OTFormNO.Value
                dr(m_Rows(i).OTRegisterID.FieldName) = m_Rows(i).OTRegisterID.Value
                dr(m_Rows(i).OTRegisterDate.FieldName) = m_Rows(i).OTRegisterDate.Value
                dr(m_Rows(i).OTStatus.FieldName) = m_Rows(i).OTStatus.Value

                dr(m_Rows(i).OTValidDate.FieldName) = m_Rows(i).OTValidDate.Value
                dr(m_Rows(i).OTValidID.FieldName) = m_Rows(i).OTValidID.Value
                dr(m_Rows(i).OverTimeFlag.FieldName) = m_Rows(i).OverTimeFlag.Value
                dr(m_Rows(i).ToOverTimeDate.FieldName) = m_Rows(i).ToOverTimeDate.Value
                dr(m_Rows(i).ToOverTimeFlag.FieldName) = m_Rows(i).ToOverTimeFlag.Value
                dr(m_Rows(i).OTRejectDate.FieldName) = m_Rows(i).OTRejectDate.Value
                dr(m_Rows(i).OTRejectID.FieldName) = m_Rows(i).OTRejectID.Value
                dr(m_Rows(i).OTGovernmentNo.FieldName) = m_Rows(i).OTGovernmentNo.Value
                dr(m_Rows(i).OTSalaryPaid.FieldName) = m_Rows(i).OTSalaryPaid.Value
                dr(m_Rows(i).HolidayOrNot.FieldName) = m_Rows(i).HolidayOrNot.Value
                dr(m_Rows(i).ProcessDate.FieldName) = m_Rows(i).ProcessDate.Value
                dr(m_Rows(i).OTPayDate.FieldName) = m_Rows(i).OTPayDate.Value
                dr(m_Rows(i).OTModifyDate.FieldName) = m_Rows(i).OTModifyDate.Value
                dr(m_Rows(i).OTRemark.FieldName) = m_Rows(i).OTRemark.Value

                dr(m_Rows(i).KeyInComp.FieldName) = m_Rows(i).KeyInComp.Value
                dr(m_Rows(i).KeyInID.FieldName) = m_Rows(i).KeyInID.Value
                dr(m_Rows(i).HRKeyInFlag.FieldName) = m_Rows(i).HRKeyInFlag.Value
                dr(m_Rows(i).LastChgComp.FieldName) = m_Rows(i).LastChgComp.Value
                dr(m_Rows(i).LastChgID.FieldName) = m_Rows(i).LastChgID.Value
                dr(m_Rows(i).LastChgDate.FieldName) = m_Rows(i).LastChgDate.Value
                dr(m_Rows(i).OTRegisterComp.FieldName) = m_Rows(i).OTRegisterComp.Value

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

        Public Sub Add(ManageOTDecRow As Row)
            m_Rows.Add(ManageOTDecRow)
        End Sub

        Public Sub Remove(ManageOTDecRow As Row)
            If m_Rows.IndexOf(ManageOTDecRow) >= 0 Then
                m_Rows.Remove(ManageOTDecRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_OTCompID As Field(Of String) = New Field(Of String)("OTCompID", True)
        Private FI_OTEmpID As Field(Of String) = New Field(Of String)("OTEmpID", True)
        Private FI_OTStartDate As Field(Of String) = New Field(Of String)("OTStartDate", True)
        Private FI_OTEndDate As Field(Of String) = New Field(Of String)("OTEndDate", True)
        Private FI_OTSeq As Field(Of Integer) = New Field(Of Integer)("OTSeq", True)
        Private FI_OTTxnID As Field(Of String) = New Field(Of String)("OTTxnID", True)
        Private FI_OTSeqNo As Field(Of String) = New Field(Of String)("OTSeqNo", True)
        Private FI_OTFromAdvanceTxnId As Field(Of String) = New Field(Of String)("OTFromAdvanceTxnId", True)
        Private FI_DeptID As Field(Of String) = New Field(Of String)("DeptID", True)
        Private FI_OrganID As Field(Of String) = New Field(Of String)("OrganID", True)

        Private FI_DeptName As Field(Of String) = New Field(Of String)("DeptName", True)
        Private FI_OrganName As Field(Of String) = New Field(Of String)("OrganName", True)
        Private FI_FlowCaseID As Field(Of String) = New Field(Of String)("FlowCaseID", True)
        Private FI_OTStartTime As Field(Of String) = New Field(Of String)("OTStartTime", True)
        Private FI_OTEndTime As Field(Of String) = New Field(Of String)("OTEndTime", True)
        Private FI_OTTotalTime As Field(Of Integer) = New Field(Of Integer)("OTTotalTime", True)
        Private FI_SalaryOrAdjust As Field(Of String) = New Field(Of String)("SalaryOrAdjust", True)
        Private FI_AdjustInvalidDate As Field(Of Date) = New Field(Of Date)("AdjustInvalidDate", True)
        Private FI_AdjustStatus As Field(Of String) = New Field(Of String)("AdjustStatus", True)
        Private FI_AdjustDate As Field(Of Date) = New Field(Of Date)("AdjustDate", True)

        Private FI_MealFlag As Field(Of String) = New Field(Of String)("MealFlag", True)
        Private FI_MealTime As Field(Of Integer) = New Field(Of Integer)("MealTime", True)
        Private FI_OTTypeID As Field(Of String) = New Field(Of String)("OTTypeID", True)
        Private FI_OTReasonID As Field(Of Integer) = New Field(Of Integer)("OTReasonID", True)
        Private FI_OTReasonMemo As Field(Of String) = New Field(Of String)("OTReasonMemo", True)
        Private FI_OTAttachment As Field(Of String) = New Field(Of String)("OTAttachment", True)
        Private FI_OTFormNO As Field(Of String) = New Field(Of String)("OTFormNO", True)
        Private FI_OTRegisterID As Field(Of String) = New Field(Of String)("OTRegisterID", True)
        Private FI_OTRegisterDate As Field(Of Date) = New Field(Of Date)("OTRegisterDate", True)
        Private FI_OTStatus As Field(Of String) = New Field(Of String)("OTStatus", True)

        Private FI_OTValidDate As Field(Of Date) = New Field(Of Date)("OTValidDate", True)
        Private FI_OTValidID As Field(Of String) = New Field(Of String)("OTValidID", True)
        Private FI_OverTimeFlag As Field(Of String) = New Field(Of String)("OverTimeFlag", True)
        Private FI_ToOverTimeDate As Field(Of Date) = New Field(Of Date)("ToOverTimeDate", True)
        Private FI_ToOverTimeFlag As Field(Of String) = New Field(Of String)("ToOverTimeFlag", True)
        Private FI_OTRejectDate As Field(Of Date) = New Field(Of Date)("OTRejectDate", True)
        Private FI_OTRejectID As Field(Of String) = New Field(Of String)("OTRejectID", True)
        Private FI_OTGovernmentNo As Field(Of String) = New Field(Of String)("OTGovernmentNo", True)
        Private FI_OTSalaryPaid As Field(Of String) = New Field(Of String)("OTSalaryPaid", True)
        Private FI_HolidayOrNot As Field(Of String) = New Field(Of String)("HolidayOrNot", True)
        Private FI_ProcessDate As Field(Of Date) = New Field(Of Date)("ProcessDate", True)
        Private FI_OTPayDate As Field(Of Integer) = New Field(Of Integer)("OTPayDate", True)
        Private FI_OTModifyDate As Field(Of Date) = New Field(Of Date)("OTModifyDate", True)

        Private FI_OTRemark As Field(Of String) = New Field(Of String)("OTRemark", True)
        Private FI_KeyInComp As Field(Of String) = New Field(Of String)("KeyInComp", True)
        Private FI_KeyInID As Field(Of String) = New Field(Of String)("KeyInID", True)
        Private FI_HRKeyInFlag As Field(Of String) = New Field(Of String)("HRKeyInFlag", True)
        Private FI_LastChgComp As Field(Of String) = New Field(Of String)("LastChgComp", True)
        Private FI_LastChgID As Field(Of String) = New Field(Of String)("LastChgID", True)
        Private FI_LastChgDate As Field(Of Date) = New Field(Of Date)("LastChgDate", True)
        Private FI_OTRegisterComp As Field(Of String) = New Field(Of String)("OTRegisterComp", True)

        Private m_FieldNames As String() = {"OTCompID", "OTEmpID", "OTStartDate", "OTEndDate", "OTSeq", "OTTxnID", "OTSeqNo", "DeptID", "OrganID", "DeptName", _
                                       "OrganName", "FlowCaseID", "OTStartTime", "OTEndTime", "OTTotalTime", "SalaryOrAdjust", "AdjustInvalidDate", "AdjustStatus", "AdjustDate", "MealFlag", _
                                       "MealTime", "OTTypeID", "OTReasonID", "OTReasonMemo", "OTAttachment", "OTFormNO", "OTRegisterID", "OTRegisterDate", "OTStatus", "OTValidDate", _
                                       "OTValidID", "OverTimeFlag", "ToOverTimeDate", "ToOverTimeFlag", "OTRejectDate", "OTRejectID", "OTGovernmentNo", "OTSalaryPaid", "HolidayOrNot", "ProcessDate", "OTPayDate", "OTModifyDate", "OTRemark", _
                                       "KeyInComp", "KeyInID", "HRKeyInFlag", "LastChgComp", "LastChgID", "LastChgDate", "OTRegisterComp"}
        Private m_PrimaryFields As String() = {"OTCompID", "OTEmpID", "OTStartDate", "OTEndDate", "OTSeq"}
        Private m_IdentityFields As String() = {}
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "OTCompID"
                    Return FI_OTCompID.Value
                Case "OTEmpID"
                    Return FI_OTEmpID.Value
                Case "OTStartDate"
                    Return FI_OTStartDate.Value
                Case "OTEndDate"
                    Return FI_OTEndDate.Value
                Case "OTSeq"
                    Return FI_OTSeq.Value
                Case "OTTxnID"
                    Return FI_OTTxnID.Value
                Case "OTSeqNo"
                    Return FI_OTSeqNo.Value
                Case "OTFromAdvanceTxnId"
                    Return FI_OTFromAdvanceTxnId.Value
                Case "DeptID"
                    Return FI_DeptID.Value
                Case "OrganID"
                    Return FI_OrganID.Value
                    '-------------------------------------
                Case "DeptName"
                    Return FI_DeptName.Value
                Case "OrganName"
                    Return FI_OrganName.Value
                Case "FlowCaseID"
                    Return FI_FlowCaseID.Value
                Case "OTStartTime"
                    Return FI_OTStartTime.Value
                Case "OTEndTime"
                    Return FI_OTEndTime.Value
                Case "OTTotalTime"
                    Return FI_OTTotalTime.Value
                Case "SalaryOrAdjust"
                    Return FI_SalaryOrAdjust.Value
                Case "AdjustInvalidDate"
                    Return FI_AdjustInvalidDate.Value
                Case "AdjustStatus"
                    Return FI_AdjustStatus.Value
                Case "AdjustDate"
                    Return FI_AdjustDate.Value
                    '-------------------------------------
                Case "MealFlag"
                    Return FI_MealFlag.Value
                Case "MealTime"
                    Return FI_MealTime.Value
                Case "OTTypeID"
                    Return FI_OTTypeID.Value
                Case "OTReasonID"
                    Return FI_OTReasonID.Value
                Case "OTReasonMemo"
                    Return FI_OTReasonMemo.Value
                Case "OTAttachment"
                    Return FI_OTAttachment.Value
                Case "OTFormNO"
                    Return FI_OTFormNO.Value
                Case "OTRegisterID"
                    Return FI_OTRegisterID.Value
                Case "OTRegisterDate"
                    Return FI_OTRegisterDate.Value
                Case "OTStatus"
                    Return FI_OTStatus.Value
                    '-------------------------------------
                Case "OTValidDate"
                    Return FI_OTValidDate.Value
                Case "OTValidID"
                    Return FI_OTValidID.Value
                Case "OverTimeFlag"
                    Return FI_OverTimeFlag.Value
                Case "ToOverTimeDate"
                    Return FI_ToOverTimeDate.Value
                Case "ToOverTimeFlag"
                    Return FI_ToOverTimeFlag.Value
                Case "OTRejectDate"
                    Return FI_OTRejectDate.Value
                Case "OTRejectID"
                    Return FI_OTRejectID.Value
                Case "OTGovernmentNo"
                    Return FI_OTGovernmentNo.Value
                Case "OTSalaryPaid"
                    Return FI_OTSalaryPaid.Value
                Case "HolidayOrNot"
                    Return FI_HolidayOrNot.Value
                Case "ProcessDate"
                    Return FI_ProcessDate.Value
                Case "OTPayDate"
                    Return FI_OTPayDate.Value
                Case "OTModifyDate"
                    Return FI_OTModifyDate.Value
                    '-------------------------------------
                Case "OTRemark"
                    Return FI_OTRemark.Value
                Case "KeyInComp"
                    Return FI_KeyInComp.Value
                Case "KeyInID"
                    Return FI_KeyInID.Value
                Case "HRKeyInFlag"
                    Return FI_HRKeyInFlag.Value
                Case "LastChgComp"
                    Return FI_LastChgComp.Value
                Case "LastChgID"
                    Return FI_LastChgID.Value
                Case "LastChgDate"
                    Return FI_LastChgDate.Value
                Case "OTRegisterComp"
                    Return FI_OTRegisterComp.Value
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Sub SetFieldValue(ByVal fieldName As String, ByVal value As Object)
            Select Case fieldName
                Case "OTCompID"
                    FI_OTCompID.SetValue(value)
                Case "OTEmpID"
                    FI_OTEmpID.SetValue(value)
                Case "OTStartDate"
                    FI_OTStartDate.SetValue(value)
                Case "OTEndDate"
                    FI_OTEndDate.SetValue(value)
                Case "OTSeq"
                    FI_OTSeq.SetValue(value)
                Case "OTTxnID"
                    FI_OTTxnID.SetValue(value)
                Case "OTSeqNo"
                    FI_OTSeqNo.SetValue(value)
                Case "OTFromAdvanceTxnId"
                    FI_OTFromAdvanceTxnId.SetValue(value)
                Case "DeptID"
                    FI_DeptID.SetValue(value)
                Case "OrganID"
                    FI_OrganID.SetValue(value)
                    '-------------------------------------
                Case "DeptName"
                    FI_DeptName.SetValue(value)
                Case "OrganName"
                    FI_OrganName.SetValue(value)
                Case "FlowCaseID"
                    FI_FlowCaseID.SetValue(value)
                Case "OTStartTime"
                    FI_OTStartTime.SetValue(value)
                Case "OTEndTime"
                    FI_OTEndTime.SetValue(value)
                Case "OTTotalTime"
                    FI_OTTotalTime.SetValue(value)
                Case "SalaryOrAdjust"
                    FI_SalaryOrAdjust.SetValue(value)
                Case "AdjustInvalidDate"
                    FI_AdjustInvalidDate.SetValue(value)
                Case "AdjustStatus"
                    FI_AdjustStatus.SetValue(value)
                Case "AdjustDate"
                    FI_AdjustDate.SetValue(value)
                    '-------------------------------------
                Case "MealFlag"
                    FI_MealFlag.SetValue(value)
                Case "MealTime"
                    FI_MealTime.SetValue(value)
                Case "OTTypeID"
                    FI_OTTypeID.SetValue(value)
                Case "OTReasonID"
                    FI_OTReasonID.SetValue(value)
                Case "OTReasonMemo"
                    FI_OTReasonMemo.SetValue(value)
                Case "OTAttachment"
                    FI_OTAttachment.SetValue(value)
                Case "OTFormNO"
                    FI_OTFormNO.SetValue(value)
                Case "OTRegisterID"
                    FI_OTRegisterID.SetValue(value)
                Case "OTRegisterDate"
                    FI_OTRegisterDate.SetValue(value)
                Case "OTStatus"
                    FI_OTStatus.SetValue(value)
                    '-------------------------------------
                Case "OTValidDate"
                    FI_OTValidDate.SetValue(value)
                Case "OTValidID"
                    FI_OTValidID.SetValue(value)
                Case "OverTimeFlag"
                    FI_OverTimeFlag.SetValue(value)
                Case "ToOverTimeDate"
                    FI_ToOverTimeDate.SetValue(value)
                Case "ToOverTimeFlag"
                    FI_ToOverTimeFlag.SetValue(value)
                Case "OTRejectDate"
                    FI_OTRejectDate.SetValue(value)
                Case "OTRejectID"
                    FI_OTRejectID.SetValue(value)
                Case "OTGovernmentNo"
                    FI_OTGovernmentNo.SetValue(value)
                Case "OTSalaryPaid"
                    FI_OTSalaryPaid.SetValue(value)
                Case "HolidayOrNot"
                    FI_HolidayOrNot.SetValue(value)
                Case "ProcessDate"
                    FI_ProcessDate.SetValue(value)
                Case "OTPayDate"
                    FI_OTPayDate.SetValue(value)
                Case "OTModifyDate"
                    FI_OTModifyDate.SetValue(value)
                    '-------------------------------------
                Case "OTRemark"
                    FI_OTRemark.SetValue(value)
                Case "KeyInComp"
                    FI_KeyInComp.SetValue(value)
                Case "KeyInID"
                    FI_KeyInID.SetValue(value)
                Case "HRKeyInFlag"
                    FI_HRKeyInFlag.SetValue(value)
                Case "LastChgComp"
                    FI_LastChgComp.SetValue(value)
                Case "LastChgID"
                    FI_LastChgID.SetValue(value)
                Case "LastChgDate"
                    FI_LastChgDate.SetValue(value)
                Case "OTRegisterComp"
                    FI_OTRegisterComp.SetValue(value)
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
                Case "OTCompID"
                    Return FI_OTCompID.Updated
                Case "OTEmpID"
                    Return FI_OTEmpID.Updated
                Case "OTStartDate"
                    Return FI_OTStartDate.Updated
                Case "OTEndDate"
                    Return FI_OTEndDate.Updated
                Case "OTSeq"
                    Return FI_OTSeq.Updated
                Case "OTTxnID"
                    Return FI_OTTxnID.Updated
                Case "OTSeqNo"
                    Return FI_OTSeqNo.Updated
                Case "OTFromAdvanceTxnId"
                    Return FI_OTFromAdvanceTxnId.Updated
                Case "DeptID"
                    Return FI_DeptID.Updated
                Case "OrganID"
                    Return FI_OrganID.Updated
                    '-------------------------------------
                Case "DeptName"
                    Return FI_DeptName.Updated
                Case "OrganName"
                    Return FI_OrganName.Updated
                Case "FlowCaseID"
                    Return FI_FlowCaseID.Updated
                Case "OTStartTime"
                    Return FI_OTStartTime.Updated
                Case "OTEndTime"
                    Return FI_OTEndTime.Updated
                Case "OTTotalTime"
                    Return FI_OTTotalTime.Updated
                Case "SalaryOrAdjust"
                    Return FI_SalaryOrAdjust.Updated
                Case "AdjustInvalidDate"
                    Return FI_AdjustInvalidDate.Updated
                Case "AdjustStatus"
                    Return FI_AdjustStatus.Updated
                Case "AdjustDate"
                    Return FI_AdjustDate.Updated
                    '-------------------------------------
                Case "MealFlag"
                    Return FI_MealFlag.Updated
                Case "MealTime"
                    Return FI_MealTime.Updated
                Case "OTTypeID"
                    Return FI_OTTypeID.Updated
                Case "OTReasonID"
                    Return FI_OTReasonID.Updated
                Case "OTReasonMemo"
                    Return FI_OTReasonMemo.Updated
                Case "OTAttachment"
                    Return FI_OTAttachment.Updated
                Case "OTFormNO"
                    Return FI_OTFormNO.Updated
                Case "OTRegisterID"
                    Return FI_OTRegisterID.Updated
                Case "OTRegisterDate"
                    Return FI_OTRegisterDate.Updated
                Case "OTStatus"
                    Return FI_OTStatus.Updated
                    '-------------------------------------
                Case "OTValidDate"
                    Return FI_OTValidDate.Updated
                Case "OTValidID"
                    Return FI_OTValidID.Updated
                Case "OverTimeFlag"
                    Return FI_OverTimeFlag.Updated
                Case "ToOverTimeDate"
                    Return FI_ToOverTimeDate.Updated
                Case "ToOverTimeFlag"
                    Return FI_ToOverTimeFlag.Updated
                Case "OTRejectDate"
                    Return FI_OTRejectDate.Updated
                Case "OTRejectID"
                    Return FI_OTRejectID.Updated
                Case "OTGovernmentNo"
                    Return FI_OTGovernmentNo.Updated
                Case "OTSalaryPaid"
                    Return FI_OTSalaryPaid.Updated
                Case "HolidayOrNot"
                    Return FI_HolidayOrNot.Updated
                Case "ProcessDate"
                    Return FI_ProcessDate.Updated
                Case "OTPayDate"
                    Return FI_OTPayDate.Updated
                Case "OTModifyDate"
                    Return FI_OTModifyDate.Updated
                    '-------------------------------------
                Case "OTRemark"
                    Return FI_OTRemark.Updated
                Case "KeyInComp"
                    Return FI_KeyInComp.Updated
                Case "KeyInID"
                    Return FI_KeyInID.Updated
                Case "HRKeyInFlag"
                    Return FI_HRKeyInFlag.Updated
                Case "LastChgComp"
                    Return FI_LastChgComp.Updated
                Case "LastChgID"
                    Return FI_LastChgID.Updated
                Case "LastChgDate"
                    Return FI_LastChgDate.Updated
                Case "OTRegisterComp"
                    Return FI_OTRegisterComp.Updated
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public Function CreateUpdateSQL(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "OTCompID"
                    Return FI_OTCompID.CreateUpdateSQL
                Case "OTEmpID"
                    Return FI_OTEmpID.CreateUpdateSQL
                Case "OTStartDate"
                    Return FI_OTStartDate.CreateUpdateSQL
                Case "OTEndDate"
                    Return FI_OTEndDate.CreateUpdateSQL
                Case "OTSeq"
                    Return FI_OTSeq.CreateUpdateSQL
                Case "OTTxnID"
                    Return FI_OTTxnID.CreateUpdateSQL
                Case "OTSeqNo"
                    Return FI_OTSeqNo.CreateUpdateSQL
                Case "OTFromAdvanceTxnId"
                    Return FI_OTFromAdvanceTxnId.CreateUpdateSQL
                Case "DeptID"
                    Return FI_DeptID.CreateUpdateSQL
                Case "OrganID"
                    Return FI_OrganID.CreateUpdateSQL
                    '-------------------------------------
                Case "DeptName"
                    Return FI_DeptName.CreateUpdateSQL
                Case "OrganName"
                    Return FI_OrganName.CreateUpdateSQL
                Case "FlowCaseID"
                    Return FI_FlowCaseID.CreateUpdateSQL
                Case "OTStartTime"
                    Return FI_OTStartTime.CreateUpdateSQL
                Case "OTEndTime"
                    Return FI_OTEndTime.CreateUpdateSQL
                Case "OTTotalTime"
                    Return FI_OTTotalTime.CreateUpdateSQL
                Case "SalaryOrAdjust"
                    Return FI_SalaryOrAdjust.CreateUpdateSQL
                Case "AdjustInvalidDate"
                    Return FI_AdjustInvalidDate.CreateUpdateSQL
                Case "AdjustStatus"
                    Return FI_AdjustStatus.CreateUpdateSQL
                Case "AdjustDate"
                    Return FI_AdjustDate.CreateUpdateSQL
                    '-------------------------------------
                Case "MealFlag"
                    Return FI_MealFlag.CreateUpdateSQL
                Case "MealTime"
                    Return FI_MealTime.CreateUpdateSQL
                Case "OTTypeID"
                    Return FI_OTTypeID.CreateUpdateSQL
                Case "OTReasonID"
                    Return FI_OTReasonID.CreateUpdateSQL
                Case "OTReasonMemo"
                    Return FI_OTReasonMemo.CreateUpdateSQL
                Case "OTAttachment"
                    Return FI_OTAttachment.CreateUpdateSQL
                Case "OTFormNO"
                    Return FI_OTFormNO.CreateUpdateSQL
                Case "OTRegisterID"
                    Return FI_OTRegisterID.CreateUpdateSQL
                Case "OTRegisterDate"
                    Return FI_OTRegisterDate.CreateUpdateSQL
                Case "OTStatus"
                    Return FI_OTStatus.CreateUpdateSQL
                    '-------------------------------------
                Case "OTValidDate"
                    Return FI_OTValidDate.CreateUpdateSQL
                Case "OTValidID"
                    Return FI_OTValidID.CreateUpdateSQL
                Case "OverTimeFlag"
                    Return FI_OverTimeFlag.CreateUpdateSQL
                Case "ToOverTimeDate"
                    Return FI_ToOverTimeDate.CreateUpdateSQL
                Case "ToOverTimeFlag"
                    Return FI_ToOverTimeFlag.CreateUpdateSQL
                Case "OTRejectDate"
                    Return FI_OTRejectDate.CreateUpdateSQL
                Case "OTRejectID"
                    Return FI_OTRejectID.CreateUpdateSQL
                Case "OTGovernmentNo"
                    Return FI_OTGovernmentNo.CreateUpdateSQL
                Case "OTSalaryPaid"
                    Return FI_OTSalaryPaid.CreateUpdateSQL
                Case "HolidayOrNot"
                    Return FI_HolidayOrNot.CreateUpdateSQL
                Case "ProcessDate"
                    Return FI_ProcessDate.CreateUpdateSQL
                Case "OTPayDate"
                    Return FI_OTPayDate.CreateUpdateSQL
                Case "OTModifyDate"
                    Return FI_OTModifyDate.CreateUpdateSQL
                    '-------------------------------------
                Case "OTRemark"
                    Return FI_OTRemark.CreateUpdateSQL
                Case "KeyInComp"
                    Return FI_KeyInComp.CreateUpdateSQL
                Case "KeyInID"
                    Return FI_KeyInID.CreateUpdateSQL
                Case "HRKeyInFlag"
                    Return FI_HRKeyInFlag.CreateUpdateSQL
                Case "LastChgComp"
                    Return FI_LastChgComp.CreateUpdateSQL
                Case "LastChgID"
                    Return FI_LastChgID.CreateUpdateSQL
                Case "LastChgDate"
                    Return FI_LastChgDate.CreateUpdateSQL
                Case "OTRegisterComp"
                    Return FI_OTRegisterComp.CreateUpdateSQL
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
            FI_OTCompID.SetInitValue("")
            FI_OTEmpID.SetInitValue("")
            FI_OTStartDate.SetInitValue("")
            FI_OTEndDate.SetInitValue("")
            FI_OTSeq.SetInitValue(0)
            FI_OTTxnID.SetInitValue("")
            FI_OTSeqNo.SetInitValue("")
            FI_OTFromAdvanceTxnId.SetInitValue("")
            FI_DeptID.SetInitValue("")
            FI_OrganID.SetInitValue("")
            '-------------------------------------
            FI_DeptName.SetInitValue("")
            FI_OrganName.SetInitValue("")
            FI_FlowCaseID.SetInitValue("")
            FI_OTStartTime.SetInitValue("")
            FI_OTEndTime.SetInitValue("")
            FI_OTTotalTime.SetInitValue(0)
            FI_SalaryOrAdjust.SetInitValue("")
            FI_AdjustInvalidDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_AdjustStatus.SetInitValue("")
            FI_AdjustDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            '-------------------------------------
            FI_MealFlag.SetInitValue("")
            FI_MealTime.SetInitValue(0)
            FI_OTTypeID.SetInitValue("")
            FI_OTReasonID.SetInitValue(0)
            FI_OTReasonMemo.SetInitValue("")
            FI_OTAttachment.SetInitValue("")
            FI_OTFormNO.SetInitValue("")
            FI_OTRegisterID.SetInitValue("")
            FI_OTRegisterDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_OTStatus.SetInitValue("")
            '-------------------------------------
            FI_OTValidDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_OTValidID.SetInitValue("")
            FI_OverTimeFlag.SetInitValue("")
            FI_ToOverTimeDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_ToOverTimeFlag.SetInitValue("")
            FI_OTRejectDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_OTRejectID.SetInitValue("")
            FI_OTGovernmentNo.SetInitValue("")
            FI_OTSalaryPaid.SetInitValue("")
            FI_HolidayOrNot.SetInitValue("")
            FI_ProcessDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_OTPayDate.SetInitValue(0)
            FI_OTModifyDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            '-------------------------------------
            FI_OTRemark.SetInitValue("")
            FI_KeyInComp.SetInitValue("")
            FI_KeyInID.SetInitValue("")
            FI_HRKeyInFlag.SetInitValue("")
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_OTRegisterComp.SetInitValue("")
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_OTCompID.SetInitValue(dr("OTCompID"))
            FI_OTEmpID.SetInitValue(dr("OTEmpID"))
            FI_OTStartDate.SetInitValue(dr("OTStartDate"))
            FI_OTEndDate.SetInitValue(dr("OTEndDate"))
            FI_OTSeq.SetInitValue(dr("OTSeq"))
            FI_OTTxnID.SetInitValue(dr("OTTxnID"))
            FI_OTSeqNo.SetInitValue(dr("OTSeqNo"))
            FI_OTFromAdvanceTxnId.SetInitValue(dr("OTFromAdvanceTxnId"))
            FI_DeptID.SetInitValue(dr("DeptID"))
            FI_OrganID.SetInitValue(dr("OrganID"))
            '-------------------------------------
            FI_DeptName.SetInitValue(dr("DeptName"))
            FI_OrganName.SetInitValue(dr("OrganName"))
            FI_FlowCaseID.SetInitValue(dr("FlowCaseID"))
            FI_OTStartTime.SetInitValue(dr("OTStartTime"))
            FI_OTEndTime.SetInitValue(dr("OTEndTime"))
            FI_OTTotalTime.SetInitValue(dr("OTTotalTime"))
            FI_SalaryOrAdjust.SetInitValue(dr("SalaryOrAdjust"))
            FI_AdjustInvalidDate.SetInitValue(dr("AdjustInvalidDate"))
            FI_AdjustStatus.SetInitValue(dr("AdjustStatus"))
            FI_AdjustDate.SetInitValue(dr("AdjustDate"))
            '-------------------------------------
            FI_MealFlag.SetInitValue(dr("MealFlag"))
            FI_MealTime.SetInitValue(dr("MealTime"))
            FI_OTTypeID.SetInitValue(dr("OTTypeID"))
            FI_OTReasonID.SetInitValue(dr("OTReasonID"))
            FI_OTReasonMemo.SetInitValue(dr("OTReasonMemo"))
            FI_OTAttachment.SetInitValue(dr("OTAttachment"))
            FI_OTFormNO.SetInitValue(dr("OTFormNO"))
            FI_OTRegisterID.SetInitValue(dr("OTRegisterID"))
            FI_OTRegisterDate.SetInitValue(dr("OTRegisterDate"))
            FI_OTStatus.SetInitValue(dr("OTStatus"))
            '-------------------------------------
            FI_OTValidDate.SetInitValue(dr("OTValidDate"))
            FI_OTValidID.SetInitValue(dr("OTValidID"))
            FI_OverTimeFlag.SetInitValue(dr("OverTimeFlag"))
            FI_ToOverTimeDate.SetInitValue(dr("ToOverTimeDate"))
            FI_ToOverTimeFlag.SetInitValue(dr("ToOverTimeFlag"))
            FI_OTRejectDate.SetInitValue(dr("OTRejectDate"))
            FI_OTRejectID.SetInitValue(dr("OTRejectID"))
            FI_OTGovernmentNo.SetInitValue(dr("OTGovernmentNo"))
            FI_OTSalaryPaid.SetInitValue(dr("OTSalaryPaid"))
            FI_HolidayOrNot.SetInitValue(dr("HolidayOrNot"))
            FI_ProcessDate.SetInitValue(dr("ProcessDate"))
            FI_OTPayDate.SetInitValue(dr("OTPayDate"))
            FI_OTModifyDate.SetInitValue(dr("OTModifyDate"))
            '-------------------------------------
            FI_OTRemark.SetInitValue(dr("OTRemark"))
            FI_KeyInComp.SetInitValue(dr("KeyInComp"))
            FI_KeyInID.SetInitValue(dr("KeyInID"))
            FI_HRKeyInFlag.SetInitValue(dr("HRKeyInFlag"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))
            FI_LastChgDate.SetInitValue(dr("OTRegisterComp"))
            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_OTCompID.Updated = False
            FI_OTEmpID.Updated = False
            FI_OTStartDate.Updated = False
            FI_OTEndDate.Updated = False
            FI_OTSeq.Updated = False
            FI_OTTxnID.Updated = False
            FI_OTSeqNo.Updated = False
            FI_OTFromAdvanceTxnId.Updated = False
            FI_DeptID.Updated = False
            FI_OrganID.Updated = False
            '-------------------------------------
            FI_DeptName.Updated = False
            FI_OrganName.Updated = False
            FI_FlowCaseID.Updated = False
            FI_OTStartTime.Updated = False
            FI_OTEndTime.Updated = False
            FI_OTTotalTime.Updated = False
            FI_SalaryOrAdjust.Updated = False
            FI_AdjustInvalidDate.Updated = False
            FI_AdjustStatus.Updated = False
            FI_AdjustDate.Updated = False
            '-------------------------------------
            FI_MealFlag.Updated = False
            FI_MealTime.Updated = False
            FI_OTTypeID.Updated = False
            FI_OTReasonID.Updated = False
            FI_OTReasonMemo.Updated = False
            FI_OTAttachment.Updated = False
            FI_OTFormNO.Updated = False
            FI_OTRegisterID.Updated = False
            FI_OTRegisterDate.Updated = False
            FI_OTStatus.Updated = False
            '-------------------------------------
            FI_OTValidDate.Updated = False
            FI_OTValidID.Updated = False
            FI_OverTimeFlag.Updated = False
            FI_ToOverTimeDate.Updated = False
            FI_ToOverTimeFlag.Updated = False
            FI_OTRejectDate.Updated = False
            FI_OTRejectID.Updated = False
            FI_OTGovernmentNo.Updated = False
            FI_OTSalaryPaid.Updated = False
            FI_HolidayOrNot.Updated = False
            FI_ProcessDate.Updated = False
            FI_OTPayDate.Updated = False
            FI_OTModifyDate.Updated = False
            '-------------------------------------
            FI_OTRemark.Updated = False
            FI_KeyInComp.Updated = False
            FI_KeyInID.Updated = False
            FI_HRKeyInFlag.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
            FI_OTRegisterComp.Updated = False
        End Sub

        Public ReadOnly Property OTCompID As Field(Of String)
            Get
                Return FI_OTCompID
            End Get
        End Property

        Public ReadOnly Property OTEmpID As Field(Of String)
            Get
                Return FI_OTEmpID
            End Get
        End Property

        Public ReadOnly Property OTStartDate As Field(Of String)
            Get
                Return FI_OTStartDate
            End Get
        End Property

        Public ReadOnly Property OTEndDate As Field(Of String)
            Get
                Return FI_OTEndDate
            End Get
        End Property

        Public ReadOnly Property OTSeq As Field(Of Integer)
            Get
                Return FI_OTSeq
            End Get
        End Property

        Public ReadOnly Property OTTxnID As Field(Of String)
            Get
                Return FI_OTTxnID
            End Get
        End Property

        Public ReadOnly Property OTSeqNo As Field(Of String)
            Get
                Return FI_OTSeqNo
            End Get
        End Property

        Public ReadOnly Property OTFromAdvanceTxnId As Field(Of String)
            Get
                Return FI_OTFromAdvanceTxnId
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

        Public ReadOnly Property DeptName As Field(Of String)
            Get
                Return FI_DeptName
            End Get
        End Property

        Public ReadOnly Property OrganName As Field(Of String)
            Get
                Return FI_OrganName
            End Get
        End Property

        Public ReadOnly Property FlowCaseID As Field(Of String)
            Get
                Return FI_FlowCaseID
            End Get
        End Property

        Public ReadOnly Property OTStartTime As Field(Of String)
            Get
                Return FI_OTStartTime
            End Get
        End Property

        Public ReadOnly Property OTEndTime As Field(Of String)
            Get
                Return FI_OTEndTime
            End Get
        End Property

        Public ReadOnly Property OTTotalTime As Field(Of Integer)
            Get
                Return FI_OTTotalTime
            End Get
        End Property

        Public ReadOnly Property SalaryOrAdjust As Field(Of String)
            Get
                Return FI_SalaryOrAdjust
            End Get
        End Property

        Public ReadOnly Property AdjustInvalidDate As Field(Of Date)
            Get
                Return FI_AdjustInvalidDate
            End Get
        End Property

        Public ReadOnly Property AdjustStatus As Field(Of String)
            Get
                Return FI_AdjustStatus
            End Get
        End Property

        Public ReadOnly Property AdjustDate As Field(Of Date)
            Get
                Return FI_AdjustDate
            End Get
        End Property

        Public ReadOnly Property MealFlag As Field(Of String)
            Get
                Return FI_MealFlag
            End Get
        End Property

        Public ReadOnly Property MealTime As Field(Of Integer)
            Get
                Return FI_MealTime
            End Get
        End Property

        Public ReadOnly Property OTTypeID As Field(Of String)
            Get
                Return FI_OTTypeID
            End Get
        End Property

        Public ReadOnly Property OTReasonID As Field(Of Integer)
            Get
                Return FI_OTReasonID
            End Get
        End Property

        Public ReadOnly Property OTReasonMemo As Field(Of String)
            Get
                Return FI_OTReasonMemo
            End Get
        End Property

        Public ReadOnly Property OTAttachment As Field(Of String)
            Get
                Return FI_OTAttachment
            End Get
        End Property

        Public ReadOnly Property OTFormNO As Field(Of String)
            Get
                Return FI_OTFormNO
            End Get
        End Property

        Public ReadOnly Property OTRegisterID As Field(Of String)
            Get
                Return FI_OTRegisterID
            End Get
        End Property

        Public ReadOnly Property OTRegisterDate As Field(Of Date)
            Get
                Return FI_OTRegisterDate
            End Get
        End Property

        Public ReadOnly Property OTStatus As Field(Of String)
            Get
                Return FI_OTStatus
            End Get
        End Property

        Public ReadOnly Property OTValidDate As Field(Of Date)
            Get
                Return FI_OTValidDate
            End Get
        End Property

        Public ReadOnly Property OTValidID As Field(Of String)
            Get
                Return FI_OTValidID
            End Get
        End Property

        Public ReadOnly Property OverTimeFlag As Field(Of String)
            Get
                Return FI_OverTimeFlag
            End Get
        End Property

        Public ReadOnly Property ToOverTimeDate As Field(Of Date)
            Get
                Return FI_ToOverTimeDate
            End Get
        End Property

        Public ReadOnly Property ToOverTimeFlag As Field(Of String)
            Get
                Return FI_ToOverTimeFlag
            End Get
        End Property

        Public ReadOnly Property OTRejectDate As Field(Of Date)
            Get
                Return FI_OTRejectDate
            End Get
        End Property

        Public ReadOnly Property OTRejectID As Field(Of String)
            Get
                Return FI_OTRejectID
            End Get
        End Property

        Public ReadOnly Property OTGovernmentNo As Field(Of String)
            Get
                Return FI_OTGovernmentNo
            End Get
        End Property

        Public ReadOnly Property OTSalaryPaid As Field(Of String)
            Get
                Return FI_OTSalaryPaid
            End Get
        End Property

        Public ReadOnly Property HolidayOrNot As Field(Of String)
            Get
                Return FI_HolidayOrNot
            End Get
        End Property

        Public ReadOnly Property ProcessDate As Field(Of Date)
            Get
                Return FI_ProcessDate
            End Get
        End Property

        Public ReadOnly Property OTPayDate As Field(Of Integer)
            Get
                Return FI_OTPayDate
            End Get
        End Property

        Public ReadOnly Property OTModifyDate As Field(Of Date)
            Get
                Return FI_OTModifyDate
            End Get
        End Property

        Public ReadOnly Property OTRemark As Field(Of String)
            Get
                Return FI_OTRemark
            End Get
        End Property

        Public ReadOnly Property KeyInComp As Field(Of String)
            Get
                Return FI_KeyInComp
            End Get
        End Property

        Public ReadOnly Property KeyInID As Field(Of String)
            Get
                Return FI_KeyInID
            End Get
        End Property

        Public ReadOnly Property HRKeyInFlag As Field(Of String)
            Get
                Return FI_HRKeyInFlag
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

        Public ReadOnly Property OTRegisterComp As Field(Of String)
            Get
                Return FI_OTRegisterComp
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
        ''' 查詢全部的加班事後申報單
        ''' </summary>
        ''' <param name="ManageOTDecRow"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function QueryAll(ByVal ManageOTDecRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            'Dim db As Database = DatabaseFactory.CreateDatabase("testConnectionString")

            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine(" SELECT * ")
            strSQL.AppendLine(" FROM (SELECT OT.HRKeyInFlag, OT.OTCompID, OT.OTEmpID, P.NameN, OT.DeptID, OT.OrganID, OT.OTRegisterID, OT.OTTxnID, OT.OTTypeID, OTT.CodeCName, OT.OTStatus ")
            strSQL.AppendLine(" ,OT.OTFormNO ")
            'strSQL.AppendLine(" , LEFT(OT.OTFormNO, 8) + '<br />' + RIGHT(OT.OTFormNO, 8) AS OTFormNO  ")
            strSQL.AppendLine(" , CASE OT.OTStatus WHEN '1' THEN '暫存' WHEN '2' THEN '送簽' WHEN '3' THEN '核准' WHEN '4' THEN '駁回' WHEN '5' THEN '刪除' WHEN '6' THEN '取消' WHEN '7' THEN '作廢' WHEN '9' THEN '計薪後收回' END AS OTStatusName ")
            strSQL.AppendLine(" , (OT.OTStartDate + '~' + ISNULL(OT2.OTEndDate, OT.OTEndDate)) AS OTDate ")
            strSQL.AppendLine(" , OT.OTStartDate, OT.OTEndDate ")
            strSQL.AppendLine(" , (LEFT(OT.OTStartTime, 2) + ':' + RIGHT(OT.OTStartTime, 2) + '~' + ISNULL(LEFT(OT2.OTEndTime, 2) + ':' + RIGHT(OT2.OTEndTime, 2), LEFT(OT.OTEndTime, 2) + ':' + RIGHT(OT.OTEndTime, 2))) AS OTTime ")
            'strSQL.AppendLine(" ,  Convert(Decimal(10,1),((CAST(OT.OTTotalTime AS FLOAT)-CAST(OT.MealTime AS FLOAT))/CAST(60 AS FLOAT))+((CAST(ISNULL(OT2.OTTotalTime,0) AS FLOAT)-CAST(ISNULL(OT2.MealTime,0) AS FLOAT))/CAST(60 AS FLOAT))) AS OTTotalTime")
            strSQL.AppendLine(" ,  Convert(Decimal(10,1),Round(Convert(Decimal(10,2),((CAST(OT.OTTotalTime AS FLOAT)-CAST(OT.MealTime AS FLOAT))/CAST(60 AS FLOAT))+((CAST(ISNULL(OT2.OTTotalTime,0) AS FLOAT)-CAST(ISNULL(OT2.MealTime,0) AS FLOAT))/CAST(60 AS FLOAT))),1),1) AS OTTotalTime")
            strSQL.AppendLine(" , ISNULL(AI.FileName, '') AS FileName ")
            strSQL.AppendLine(" , OT.OTSeq, OT.OTSeqNo, OT.OTFromAdvanceTxnId, OT.DeptName, OT.OrganName, OT.FlowCaseID, OT.SalaryOrAdjust, OT.AdjustInvalidDate, OT.AdjustStatus, OT.AdjustDate, OT.MealFlag, OT.MealTime, OT.OTReasonID, OT.OTReasonMemo, OT.OTRegisterDate, OT.OTValidDate, OT.OTValidID, OT.OverTimeFlag, OT.ToOverTimeDate, OT.ToOverTimeFlag, OT.OTRejectDate, OT.OTRejectID, OT.OTGovernmentNo, OT.OTSalaryPaid, OT.HolidayOrNot, OT.ProcessDate, OT.OTPayDate, OT.OTModifyDate, OT.OTRemark, OT.KeyInComp, OT.KeyInID, OT.LastChgComp, OT.LastChgID, OT.LastChgDate, OT.OTAttachment, OT.OTRegisterComp ")
            strSQL.AppendLine(" FROM OverTimeDeclaration OT ")
            strSQL.AppendLine(" LEFT JOIN OverTimeDeclaration OT2 ON OT2.OTTxnID = OT.OTTxnID AND OT2.OTSeqNo = 2 AND OT2.OverTimeFlag='1'")
            strSQL.AppendLine(" LEFT JOIN " + _eHRMSDB_ITRD + ".[dbo].[Personal] P ON P.EmpID = OT.OTEmpID AND P.CompID = OT.OTCompID ")
            strSQL.AppendLine(" LEFT JOIN AT_CodeMap AS OTT ON OT.OTTypeID = OTT.Code AND  OTT.TabName = 'OverTime' AND  OTT.FldName = 'OverTimeType' AND OTT.NotShowFlag = '0' ")
            strSQL.AppendLine(" LEFT JOIN AttachInfo AI ON AI.AttachID IS NOT NULL AND AI.AttachID <> '' AND AI.AttachID = OT.OTAttachment AND AI.FileSize > 0 WHERE OT.OTSeqNo = 1 AND OT.OverTimeFlag='1') A ")
            strSQL.AppendLine(" WHERE 1 = 1 AND A.HRKeyInFlag = '1' AND A.OTCompID = @OTCompID ")

            If ManageOTDecRow.OTRegisterID.Value.Trim <> "" Or ManageOTDecRow.OTEmpID.Value.Trim <> "" Then
                strSQL.AppendLine(" AND ( A.OTRegisterID = @OTRegisterID OR A.OTEmpID = @OTEmpID ) ")
            End If

            If ManageOTDecRow.OTFormNO.Value.Trim <> "" Then strSQL.AppendLine(" AND A.OTFormNO = @OTFormNO ")
            If ManageOTDecRow.OTStatus.Value <> "" Then strSQL.AppendLine(" AND A.OTStatus = @OTStatus ")

            'If ManageOTDecRow.OTStartDate.Value.Trim <> "" And ManageOTDecRow.OTEndDate.Value.Trim <> "" Then strSQL.AppendLine(" AND (A.OTStartDate = @OTStartDate AND A.OTEndDate = @OTEndDate) ") '查出起迄日皆符合的加班單
            If ManageOTDecRow.OTStartDate.Value.Trim <> "" And ManageOTDecRow.OTEndDate.Value.Trim <> "" Then strSQL.AppendLine(" AND CONVERT(DATETIME, LEFT(A.OTDate, 10), 111) >= @OTStartDate AND CONVERT( DATETIME, LEFT(A.OTDate, 10), 111) <= @OTEndDate ") '查出起迄日皆符合的加班單
            If ManageOTDecRow.OTStartDate.Value.Trim <> "" And ManageOTDecRow.OTEndDate.Value.Trim = "" Then strSQL.AppendLine(" AND CONVERT(DATETIME,LEFT(A.OTDate,10),111) >= (convert(char(10),@OTStartDate,111)) ") '只有起日，查出所有起日之後的加班單
            If ManageOTDecRow.OTStartDate.Value.Trim = "" And ManageOTDecRow.OTEndDate.Value.Trim <> "" Then strSQL.AppendLine(" AND CONVERT(DATETIME,LEFT(A.OTDate,10),111) <= (convert(char(10),@OTEndDate,111)) ") '只有迄日，查出所有迄日之前的加班單
            If ManageOTDecRow.SalaryOrAdjust.Value.Trim <> "" Then strSQL.AppendLine(" AND A.SalaryOrAdjust = @SalaryOrAdjust ")
            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@OTCompID", DbType.String, ManageOTDecRow.OTCompID.Value)
            db.AddInParameter(dbcmd, "@OTRegisterID", DbType.String, ManageOTDecRow.OTRegisterID.Value)
            db.AddInParameter(dbcmd, "@OTEmpID", DbType.String, ManageOTDecRow.OTEmpID.Value)
            db.AddInParameter(dbcmd, "@OTFormNO", DbType.String, ManageOTDecRow.OTFormNO.Value)
            db.AddInParameter(dbcmd, "@OTStatus", DbType.String, ManageOTDecRow.OTStatus.Value)
            db.AddInParameter(dbcmd, "@OTStartDate", DbType.String, ManageOTDecRow.OTStartDate.Value)
            db.AddInParameter(dbcmd, "@OTEndDate", DbType.String, ManageOTDecRow.OTEndDate.Value)
            db.AddInParameter(dbcmd, "@SalaryOrAdjust", DbType.String, ManageOTDecRow.SalaryOrAdjust.Value)
            Return db.ExecuteDataSet(dbcmd)
        End Function

        ''' <summary>
        ''' (交易)查詢全部的事後申報單
        ''' </summary>
        ''' <param name="ManageOTDecRow"></param>
        ''' <param name="tran"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function QueryAll(ManageOTDecRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            ''Dim db As Database = DatabaseFactory.CreateDatabase("testConnectionString")

            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine(" SELECT * ")
            strSQL.AppendLine(" FROM (SELECT OT.HRKeyInFlag, OT.OTCompID, OT.OTEmpID, P.NameN, OT.DeptID, OT.OrganID, OT.OTRegisterID, OT.OTTxnID, OT.OTTypeID, OTT.CodeCName, OT.OTStatus ")
            strSQL.AppendLine(" ,OT.OTFormNO ")
            strSQL.AppendLine(" , CASE OT.OTStatus WHEN '1' THEN '暫存' WHEN '2' THEN '送簽' WHEN '3' THEN '核准' WHEN '4' THEN '駁回' WHEN '5' THEN '刪除' WHEN '6' THEN '取消' WHEN '7' THEN '作廢' WHEN '9' THEN '計薪後收回' END AS OTStatusName ")
            strSQL.AppendLine(" , (OT.OTStartDate + '~' + ISNULL(OT2.OTEndDate, OT.OTEndDate)) AS OTDate ")
            strSQL.AppendLine(" , OT.OTStartDate, OT.OTEndDate ")
            strSQL.AppendLine(" , (LEFT(OT.OTStartTime, 2) + ':' + RIGHT(OT.OTStartTime, 2) + '~' + ISNULL(LEFT(OT2.OTEndTime, 2) + ':' + RIGHT(OT2.OTEndTime, 2), LEFT(OT.OTEndTime, 2) + ':' + RIGHT(OT.OTEndTime, 2))) AS OTTime ")
            'strSQL.AppendLine(" ,  Convert(Decimal(10,1),((CAST(OT.OTTotalTime AS FLOAT)-CAST(OT.MealTime AS FLOAT))/CAST(60 AS FLOAT))+((CAST(ISNULL(OT2.OTTotalTime,0) AS FLOAT)-CAST(ISNULL(OT2.MealTime,0) AS FLOAT))/CAST(60 AS FLOAT))) AS OTTotalTime")
            strSQL.AppendLine(" ,  Convert(Decimal(10,1),Round(Convert(Decimal(10,2),((CAST(OT.OTTotalTime AS FLOAT)-CAST(OT.MealTime AS FLOAT))/CAST(60 AS FLOAT))+((CAST(ISNULL(OT2.OTTotalTime,0) AS FLOAT)-CAST(ISNULL(OT2.MealTime,0) AS FLOAT))/CAST(60 AS FLOAT))),1),1) AS OTTotalTime")
            strSQL.AppendLine(" , ISNULL(AI.FileName, '') AS FileName ")
            strSQL.AppendLine(" , OT.OTSeq, OT.OTSeqNo, OT.OTFromAdvanceTxnId, OT.DeptName, OT.OrganName, OT.FlowCaseID, OT.SalaryOrAdjust, OT.AdjustInvalidDate, OT.AdjustStatus, OT.AdjustDate, OT.MealFlag, OT.MealTime, OT.OTReasonID, OT.OTReasonMemo, OT.OTRegisterDate, OT.OTValidDate, OT.OTValidID, OT.OverTimeFlag, OT.ToOverTimeDate, OT.ToOverTimeFlag, OT.OTRejectDate, OT.OTRejectID, OT.OTGovernmentNo, OT.OTSalaryPaid, OT.HolidayOrNot, OT.ProcessDate, OT.OTPayDate, OT.OTModifyDate, OT.OTRemark, OT.KeyInComp, OT.KeyInID, OT.LastChgComp, OT.LastChgID, OT.LastChgDate, OT.OTAttachment, OT.OTRegisterComp")
            strSQL.AppendLine(" FROM OverTimeDeclaration OT ")
            strSQL.AppendLine(" LEFT JOIN OverTimeDeclaration OT2 ON OT2.OTTxnID = OT.OTTxnID AND OT2.OTSeqNo = 2 AND OT2.OverTimeFlag='1'")
            strSQL.AppendLine(" LEFT JOIN " + _eHRMSDB_ITRD + ".[dbo].[Personal] P ON P.EmpID = OT.OTEmpID AND P.CompID = OT.OTCompID ")
            strSQL.AppendLine(" LEFT JOIN AT_CodeMap AS OTT ON OT.OTTypeID = OTT.Code AND  OTT.TabName = 'OverTime' AND  OTT.FldName = 'OverTimeType' AND OTT.NotShowFlag = '0' ")
            strSQL.AppendLine(" LEFT JOIN AttachInfo AI ON AI.AttachID IS NOT NULL AND AI.AttachID <> '' AND AI.AttachID = OT.OTAttachment AND AI.FileSize > 0 WHERE OT.OTSeqNo = 1) A ")
            strSQL.AppendLine(" WHERE 1 = 1 AND A.HRKeyInFlag = '1' AND A.OTCompID = @OTCompID ")

            If ManageOTDecRow.OTRegisterID.Value.Trim <> "" Or ManageOTDecRow.OTEmpID.Value.Trim <> "" Then
                strSQL.AppendLine(" AND ( A.OTRegisterID = @OTRegisterID OR A.OTEmpID = @OTEmpID ) ")
            End If

            If ManageOTDecRow.OTFormNO.Value.Trim <> "" Then strSQL.AppendLine(" AND A.OTFormNO = @OTFormNO ")
            If ManageOTDecRow.OTStatus.Value <> "" Then strSQL.AppendLine(" AND A.OTStatus = @OTStatus ")

            'If ManageOTDecRow.OTStartDate.Value.Trim <> "" And ManageOTDecRow.OTEndDate.Value.Trim <> "" Then strSQL.AppendLine(" AND (A.OTStartDate = @OTStartDate AND A.OTEndDate = @OTEndDate) ") '查出起迄日皆符合的加班單
            If ManageOTDecRow.OTStartDate.Value.Trim <> "" And ManageOTDecRow.OTEndDate.Value.Trim <> "" Then strSQL.AppendLine(" AND CONVERT(DATETIME, LEFT(A.OTDate, 10), 111) >= @OTStartDate AND CONVERT( DATETIME, LEFT(A.OTDate, 10), 111) <= @OTEndDate ") '查出起迄日皆符合的加班單
            If ManageOTDecRow.OTStartDate.Value.Trim <> "" And ManageOTDecRow.OTEndDate.Value.Trim = "" Then strSQL.AppendLine(" AND CONVERT(DATETIME,LEFT(A.OTDate,10),111) >= (convert(char(10),@OTStartDate,111)) ") '只有起日，查出所有起日之後的加班單
            If ManageOTDecRow.OTStartDate.Value.Trim = "" And ManageOTDecRow.OTEndDate.Value.Trim <> "" Then strSQL.AppendLine(" AND CONVERT(DATETIME,LEFT(A.OTDate,10),111) <= (convert(char(10),@OTEndDate,111)) ") '只有迄日，查出所有迄日之前的加班單

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@OTCompID", DbType.String, ManageOTDecRow.OTCompID.Value)
            db.AddInParameter(dbcmd, "@OTRegisterID", DbType.String, ManageOTDecRow.OTRegisterID.Value)
            db.AddInParameter(dbcmd, "@OTEmpID", DbType.String, ManageOTDecRow.OTEmpID.Value)
            db.AddInParameter(dbcmd, "@OTFormNO", DbType.String, ManageOTDecRow.OTFormNO.Value)
            db.AddInParameter(dbcmd, "@OTStatus", DbType.String, ManageOTDecRow.OTStatus.Value)
            db.AddInParameter(dbcmd, "@OTStartDate", DbType.String, ManageOTDecRow.OTStartDate.Value)
            db.AddInParameter(dbcmd, "@OTEndDate", DbType.String, ManageOTDecRow.OTEndDate.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        ''' <summary>
        ''' 查詢事後申報單-條件篩選
        ''' </summary>
        ''' <param name="ManageOTDecRow"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Query(ByVal ManageOTDecRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            ''Dim db As Database = DatabaseFactory.CreateDatabase("testConnectionString")

            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine(" SELECT * ")
            strSQL.AppendLine(" FROM (SELECT OT.HRKeyInFlag, OT.OTCompID, OT.OTEmpID, P.NameN, OT.DeptID, OT.OrganID, OT.OTFormNO, OT.OTRegisterID, OT.OTTxnID, OT.OTTypeID, OTT.CodeCName, OT.OTStatus ")
            strSQL.AppendLine(" , CASE OT.OTStatus WHEN '1' THEN '暫存' WHEN '2' THEN '送簽' WHEN '3' THEN '核准' WHEN '4' THEN '駁回' WHEN '5' THEN '刪除' WHEN '6' THEN '取消' WHEN '7' THEN '作廢' WHEN '9' THEN '計薪後收回' END AS OTStatusName ")
            strSQL.AppendLine(" , (OT.OTStartDate + '~' + ISNULL(OT2.OTEndDate, OT.OTEndDate)) AS OTDate ")
            strSQL.AppendLine(" , OT.OTStartDate, OT.OTEndDate ")
            strSQL.AppendLine(" , (LEFT(OT.OTStartTime, 2) + ':' + RIGHT(OT.OTStartTime, 2) + '~' + ISNULL(LEFT(OT2.OTEndTime, 2) + ':' + RIGHT(OT2.OTEndTime, 2), LEFT(OT.OTEndTime, 2) + ':' + RIGHT(OT.OTEndTime, 2))) AS OTTime ")
            'strSQL.AppendLine(" ,  Convert(Decimal(10,1),((CAST(OT.OTTotalTime AS FLOAT)-CAST(OT.MealTime AS FLOAT))/CAST(60 AS FLOAT))+((CAST(ISNULL(OT2.OTTotalTime,0) AS FLOAT)-CAST(ISNULL(OT2.MealTime,0) AS FLOAT))/CAST(60 AS FLOAT))) AS OTTotalTime")
            strSQL.AppendLine(" ,  Convert(Decimal(10,1),Round(Convert(Decimal(10,2),((CAST(OT.OTTotalTime AS FLOAT)-CAST(OT.MealTime AS FLOAT))/CAST(60 AS FLOAT))+((CAST(ISNULL(OT2.OTTotalTime,0) AS FLOAT)-CAST(ISNULL(OT2.MealTime,0) AS FLOAT))/CAST(60 AS FLOAT))),1),1) AS OTTotalTime")
            strSQL.AppendLine(" , ISNULL(AI.FileName, '') AS FileName ")
            strSQL.AppendLine(" , OT.OTSeq, OT.OTSeqNo, OT.OTFromAdvanceTxnId, OT.DeptName, OT.OrganName, OT.FlowCaseID, OT.SalaryOrAdjust, OT.AdjustInvalidDate, OT.AdjustStatus, OT.AdjustDate, OT.MealFlag, OT.MealTime, OT.OTReasonID, OT.OTReasonMemo, OT.OTRegisterDate, OT.OTValidDate, OT.OTValidID, OT.OverTimeFlag, OT.ToOverTimeDate, OT.ToOverTimeFlag, OT.OTRejectDate, OT.OTRejectID, OT.OTGovernmentNo, OT.OTSalaryPaid, OT.HolidayOrNot, OT.ProcessDate, OT.OTPayDate, OT.OTModifyDate, OT.OTRemark, OT.KeyInComp, OT.KeyInID, OT.LastChgComp, OT.LastChgID, OT.LastChgDate, OT.OTAttachment, OT.OTRegisterComp ")
            strSQL.AppendLine(" FROM OverTimeDeclaration OT ")
            strSQL.AppendLine(" LEFT JOIN OverTimeDeclaration OT2 ON OT2.OTTxnID = OT.OTTxnID AND OT2.OTSeqNo = 2 AND OT2.OverTimeFlag='1'")
            strSQL.AppendLine(" LEFT JOIN " + _eHRMSDB_ITRD + ".[dbo].[Personal] P ON P.EmpID = OT.OTEmpID AND P.CompID = OT.OTCompID ")
            strSQL.AppendLine(" LEFT JOIN AT_CodeMap AS OTT ON OT.OTTypeID = OTT.Code AND  OTT.TabName = 'OverTime' AND  OTT.FldName = 'OverTimeType' AND OTT.NotShowFlag = '0' ")
            strSQL.AppendLine(" LEFT JOIN AttachInfo AI ON AI.AttachID IS NOT NULL AND AI.AttachID <> '' AND AI.AttachID = OT.OTAttachment AND AI.FileSize > 0 WHERE OT.OTSeqNo = 1) A ")
            strSQL.AppendLine(" WHERE 1 = 1 AND A.HRKeyInFlag = '1' AND A.OTCompID = @OTCompID ")

            If ManageOTDecRow.OTRegisterID.Value.Trim <> "" Then strSQL.AppendLine(" AND A.OTRegisterID = @OTRegisterID ")
            If ManageOTDecRow.OTEmpID.Value.Trim <> "" Then strSQL.AppendLine(" AND A.OTEmpID = @OTEmpID ")
            If ManageOTDecRow.OTFormNO.Value.Trim <> "" Then strSQL.AppendLine(" AND A.OTFormNO = @OTFormNO ")
            If ManageOTDecRow.OTStatus.Value.Trim <> "" Then strSQL.AppendLine(" AND A.OTStatus = @OTStatus ")

            'If ManageOTDecRow.OTStartDate.Value.Trim <> "" And ManageOTDecRow.OTEndDate.Value.Trim <> "" Then strSQL.AppendLine(" AND (A.OTStartDate = @OTStartDate AND A.OTEndDate = @OTEndDate) ") '查出起迄日皆符合的加班單
            If ManageOTDecRow.OTStartDate.Value.Trim <> "" And ManageOTDecRow.OTEndDate.Value.Trim <> "" Then strSQL.AppendLine(" AND CONVERT(DATETIME, LEFT(A.OTDate, 10), 111) >= @OTStartDate AND CONVERT( DATETIME, LEFT(A.OTDate, 10), 111) <= @OTEndDate ") '查出起迄日皆符合的加班單
            If ManageOTDecRow.OTStartDate.Value.Trim <> "" And ManageOTDecRow.OTEndDate.Value.Trim = "" Then strSQL.AppendLine(" AND CONVERT(DATETIME,LEFT(A.OTDate,10),111) >= (convert(char(10),@OTStartDate,111)) ") '只有起日，查出所有起日之後的加班單
            If ManageOTDecRow.OTStartDate.Value.Trim = "" And ManageOTDecRow.OTEndDate.Value.Trim <> "" Then strSQL.AppendLine(" AND CONVERT(DATETIME,LEFT(A.OTDate,10),111) <= (convert(char(10),@OTEndDate,111)) ") '只有迄日，查出所有迄日之前的加班單
            If ManageOTDecRow.SalaryOrAdjust.Value.Trim <> "" Then strSQL.AppendLine(" AND A.SalaryOrAdjust = @SalaryOrAdjust ")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@OTCompID", DbType.String, ManageOTDecRow.OTCompID.Value)
            db.AddInParameter(dbcmd, "@OTRegisterID", DbType.String, ManageOTDecRow.OTRegisterID.Value)
            db.AddInParameter(dbcmd, "@OTEmpID", DbType.String, ManageOTDecRow.OTEmpID.Value)
            db.AddInParameter(dbcmd, "@OTFormNO", DbType.String, ManageOTDecRow.OTFormNO.Value)
            db.AddInParameter(dbcmd, "@OTStatus", DbType.String, ManageOTDecRow.OTStatus.Value)
            db.AddInParameter(dbcmd, "@OTStartDate", DbType.String, ManageOTDecRow.OTStartDate.Value)
            db.AddInParameter(dbcmd, "@OTEndDate", DbType.String, ManageOTDecRow.OTEndDate.Value)
            db.AddInParameter(dbcmd, "@SalaryOrAdjust", DbType.String, ManageOTDecRow.SalaryOrAdjust.Value)
            Return db.ExecuteDataSet(dbcmd)
        End Function

        ''' <summary>
        ''' (交易)查詢事後申報單-條件篩選
        ''' </summary>
        ''' <param name="ManageOTDecRow"></param>
        ''' <param name="tran"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Query(ManageOTDecRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            'Dim db As Database = DatabaseFactory.CreateDatabase("testConnectionString")

            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine(" SELECT * ")
            strSQL.AppendLine(" FROM (SELECT OT.HRKeyInFlag, OT.OTCompID, OT.OTEmpID, P.NameN, OT.DeptID, OT.OrganID, OT.OTFormNO, OT.OTRegisterID, OT.OTTxnID, OT.OTTypeID, OTT.CodeCName, OT.OTStatus ")
            strSQL.AppendLine(" , CASE OT.OTStatus WHEN '1' THEN '暫存' WHEN '2' THEN '送簽' WHEN '3' THEN '核准' WHEN '4' THEN '駁回' WHEN '5' THEN '刪除' WHEN '6' THEN '取消' WHEN '7' THEN '作廢' WHEN '9' THEN '計薪後收回' END AS OTStatusName ")
            strSQL.AppendLine(" , (OT.OTStartDate + '~' + ISNULL(OT2.OTEndDate, OT.OTEndDate)) AS OTDate ")
            strSQL.AppendLine(" , OT.OTStartDate, OT.OTEndDate ")
            strSQL.AppendLine(" , (LEFT(OT.OTStartTime, 2) + ':' + RIGHT(OT.OTStartTime, 2) + '~' + ISNULL(LEFT(OT2.OTEndTime, 2) + ':' + RIGHT(OT2.OTEndTime, 2), LEFT(OT.OTEndTime, 2) + ':' + RIGHT(OT.OTEndTime, 2))) AS OTTime ")
            'strSQL.AppendLine(" ,  Convert(Decimal(10,1),((CAST(OT.OTTotalTime AS FLOAT)-CAST(OT.MealTime AS FLOAT))/CAST(60 AS FLOAT))+((CAST(ISNULL(OT2.OTTotalTime,0) AS FLOAT)-CAST(ISNULL(OT2.MealTime,0) AS FLOAT))/CAST(60 AS FLOAT))) AS OTTotalTime")
            strSQL.AppendLine(" ,  Convert(Decimal(10,1),Round(Convert(Decimal(10,2),((CAST(OT.OTTotalTime AS FLOAT)-CAST(OT.MealTime AS FLOAT))/CAST(60 AS FLOAT))+((CAST(ISNULL(OT2.OTTotalTime,0) AS FLOAT)-CAST(ISNULL(OT2.MealTime,0) AS FLOAT))/CAST(60 AS FLOAT))),1),1) AS OTTotalTime")
            strSQL.AppendLine(" , ISNULL(AI.FileName, '') AS FileName ")
            strSQL.AppendLine(" , OT.OTSeq, OT.OTSeqNo, OT.OTFromAdvanceTxnId, OT.DeptName, OT.OrganName, OT.FlowCaseID, OT.SalaryOrAdjust, OT.AdjustInvalidDate, OT.AdjustStatus, OT.AdjustDate, OT.MealFlag, OT.MealTime, OT.OTReasonID, OT.OTReasonMemo, OT.OTRegisterDate, OT.OTValidDate, OT.OTValidID, OT.OverTimeFlag, OT.ToOverTimeDate, OT.ToOverTimeFlag, OT.OTRejectDate, OT.OTRejectID, OT.OTGovernmentNo, OT.OTSalaryPaid, OT.HolidayOrNot, OT.ProcessDate, OT.OTPayDate, OT.OTModifyDate, OT.OTRemark, OT.KeyInComp, OT.KeyInID, OT.LastChgComp, OT.LastChgID, OT.LastChgDate, OT.OTAttachment, OT.OTRegisterComp ")
            strSQL.AppendLine(" FROM OverTimeDeclaration OT ")
            strSQL.AppendLine(" LEFT JOIN OverTimeDeclaration OT2 ON OT2.OTTxnID = OT.OTTxnID AND OT2.OTSeqNo = 2 AND OT2.OverTimeFlag='1'")
            strSQL.AppendLine(" LEFT JOIN " + _eHRMSDB_ITRD + ".[dbo].[Personal] P ON P.EmpID = OT.OTEmpID AND P.CompID = OT.OTCompID ")
            strSQL.AppendLine(" LEFT JOIN AT_CodeMap AS OTT ON OT.OTTypeID = OTT.Code AND  OTT.TabName = 'OverTime' AND  OTT.FldName = 'OverTimeType' AND OTT.NotShowFlag = '0' ")
            strSQL.AppendLine(" LEFT JOIN AttachInfo AI ON AI.AttachID IS NOT NULL AND AI.AttachID <> '' AND AI.AttachID = OT.OTAttachment AND AI.FileSize > 0 WHERE OT.OTSeqNo = 1) A ")
            strSQL.AppendLine(" WHERE 1 = 1 AND A.HRKeyInFlag = '1' AND A.OTCompID = @OTCompID ")

            If ManageOTDecRow.OTRegisterID.Value.Trim <> "" Then strSQL.AppendLine(" AND A.OTRegisterID = @OTRegisterID ")
            If ManageOTDecRow.OTEmpID.Value.Trim <> "" Then strSQL.AppendLine(" AND A.OTEmpID = @OTEmpID ")
            If ManageOTDecRow.OTFormNO.Value.Trim <> "" Then strSQL.AppendLine(" AND A.OTFormNO = @OTFormNO ")
            If ManageOTDecRow.OTStatus.Value.ToString().Trim <> "" Then strSQL.AppendLine(" AND A.OTStatus = @OTStatus ")

            'If ManageOTDecRow.OTStartDate.Value.Trim <> "" And ManageOTDecRow.OTEndDate.Value.Trim <> "" Then strSQL.AppendLine(" AND (A.OTStartDate = @OTStartDate AND A.OTEndDate = @OTEndDate) ") '查出起迄日皆符合的加班單
            If ManageOTDecRow.OTStartDate.Value.Trim <> "" And ManageOTDecRow.OTEndDate.Value.Trim <> "" Then strSQL.AppendLine(" AND (CONVERT(DATETIME, LEFT(A.OTDate, 10), 111) >= @OTStartDate AND CONVERT( DATETIME, LEFT(A.OTDate, 10), 111) <= @OTEndDate ") '查出起迄日皆符合的加班單
            If ManageOTDecRow.OTStartDate.Value.Trim <> "" And ManageOTDecRow.OTEndDate.Value.Trim = "" Then strSQL.AppendLine(" AND (convert(char(10), A.OTStartDate,111)) >= (convert(char(10),@OTStartDate,111)) ") '只有起日，查出所有起日之後的加班單
            If ManageOTDecRow.OTStartDate.Value.Trim = "" And ManageOTDecRow.OTEndDate.Value.Trim <> "" Then strSQL.AppendLine(" AND (convert(char(10), A.OTEndDate,111)) <= (convert(char(10),@OTEndDate,111)) ") '只有迄日，查出所有迄日之前的加班單

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@OTCompID", DbType.String, ManageOTDecRow.OTCompID.Value)
            db.AddInParameter(dbcmd, "@OTRegisterID", DbType.String, ManageOTDecRow.OTRegisterID.Value)
            db.AddInParameter(dbcmd, "@OTEmpID", DbType.String, ManageOTDecRow.OTEmpID.Value)
            db.AddInParameter(dbcmd, "@OTFormNO", DbType.String, ManageOTDecRow.OTFormNO.Value)
            db.AddInParameter(dbcmd, "@OTStatus", DbType.String, ManageOTDecRow.OTStatus.Value)
            db.AddInParameter(dbcmd, "@OTStartDate", DbType.String, ManageOTDecRow.OTStartDate.Value)
            db.AddInParameter(dbcmd, "@OTEndDate", DbType.String, ManageOTDecRow.OTEndDate.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        ''' <summary>
        ''' 查詢特定加班事後申報單
        ''' </summary>
        ''' <param name="ManageOTDecRow"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function QueryByKey(ByVal ManageOTDecRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            'Dim db As Database = DatabaseFactory.CreateDatabase("testConnectionString")

            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine(" SELECT * ")
            strSQL.AppendLine(" FROM (SELECT OT.HRKeyInFlag, OT.OTCompID, OT.OTEmpID, P.NameN, OT.DeptID, OT.OrganID, OT.OTFormNO, OT.OTRegisterID, OT.OTTxnID, OT.OTTypeID, OTT.CodeCName, OT.OTStatus ")
            strSQL.AppendLine(" , CASE OT.OTStatus WHEN '1' THEN '暫存' WHEN '2' THEN '送簽' WHEN '3' THEN '核准' WHEN '4' THEN '駁回' WHEN '5' THEN '刪除' WHEN '6' THEN '取消' WHEN '7' THEN '作廢' WHEN '9' THEN '計薪後收回' END AS OTStatusName ")
            'strSQL.AppendLine(" , (OT.OTStartDate + '~' + ISNULL(OT2.OTEndDate, OT.OTEndDate)) AS OTDate ")
            strSQL.AppendLine(" , OT.OTStartDate ,ISNULL(OT2.OTEndDate, OT.OTEndDate) AS OTEndDate ")
            'strSQL.AppendLine(" , (LEFT(OT.OTStartTime, 2) + ':' + RIGHT(OT.OTStartTime, 2) + '~' + ISNULL(LEFT(OT2.OTEndTime, 2) + ':' + RIGHT(OT2.OTEndTime, 2), LEFT(OT.OTEndTime, 2) + ':' + RIGHT(OT.OTEndTime, 2))) AS OTTime ")
            '-----------------------
            strSQL.AppendLine(" , LEFT(OT.OTStartTime, 2) + ':' + RIGHT(OT.OTStartTime, 2) AS OTStartTime ")
            strSQL.AppendLine(" , ISNULL(LEFT(OT2.OTEndTime, 2) + ':' + RIGHT(OT2.OTEndTime, 2), LEFT(OT.OTEndTime, 2) + ':' + RIGHT(OT.OTEndTime, 2)) AS OTEndTime ")
            '-----------------------
            'strSQL.AppendLine(" ,  Convert(Decimal(10,1),((CAST(OT.OTTotalTime AS FLOAT)-CAST(OT.MealTime AS FLOAT))/CAST(60 AS FLOAT))+((CAST(ISNULL(OT2.OTTotalTime,0) AS FLOAT)-CAST(ISNULL(OT2.MealTime,0) AS FLOAT))/CAST(60 AS FLOAT))) AS OTTotalTime")
            strSQL.AppendLine(" ,  Convert(Decimal(10,1),Round(Convert(Decimal(10,2),((CAST(OT.OTTotalTime AS FLOAT)-CAST(OT.MealTime AS FLOAT))/CAST(60 AS FLOAT))+((CAST(ISNULL(OT2.OTTotalTime,0) AS FLOAT)-CAST(ISNULL(OT2.MealTime,0) AS FLOAT))/CAST(60 AS FLOAT))),1),1) AS OTTotalTime")
            strSQL.AppendLine(" , ISNULL(AI.FileName, '') AS FileName ")
            strSQL.AppendLine(" , OT.OTSeq, OT.OTSeqNo, OT.OTFromAdvanceTxnId, OT.DeptName, OT.OrganName, OT.FlowCaseID, OT.SalaryOrAdjust, OT.AdjustInvalidDate, OT.AdjustStatus, OT.AdjustDate, OT.MealFlag, OT.MealTime, OT.OTReasonID, OT.OTReasonMemo, OT.OTRegisterDate, OT.OTValidDate, OT.OTValidID, OT.OverTimeFlag, OT.ToOverTimeDate, OT.ToOverTimeFlag, OT.OTRejectDate, OT.OTRejectID, OT.OTGovernmentNo, OT.OTSalaryPaid, OT.HolidayOrNot, OT.ProcessDate, OT.OTPayDate, OT.OTModifyDate, OT.OTRemark, OT.KeyInComp, OT.KeyInID, OT.LastChgComp, OT.LastChgID, OT.LastChgDate, OT.OTAttachment, OT.OTRegisterComp ")
            strSQL.AppendLine(" FROM OverTimeDeclaration OT ")
            strSQL.AppendLine(" LEFT JOIN OverTimeDeclaration OT2 ON OT2.OTTxnID = OT.OTTxnID AND OT2.OTSeqNo = 2 AND OT2.OverTimeFlag='1'")
            strSQL.AppendLine(" LEFT JOIN " + _eHRMSDB_ITRD + ".[dbo].[Personal] P ON P.EmpID = OT.OTEmpID AND P.CompID = OT.OTCompID ")
            strSQL.AppendLine(" LEFT JOIN AT_CodeMap AS OTT ON OT.OTTypeID = OTT.Code AND  OTT.TabName = 'OverTime' AND  OTT.FldName = 'OverTimeType' AND OTT.NotShowFlag = '0' ")
            strSQL.AppendLine(" LEFT JOIN AttachInfo AI ON AI.AttachID IS NOT NULL AND AI.AttachID <> '' AND AI.AttachID = OT.OTAttachment AND AI.FileSize > 0 WHERE OT.OTSeqNo = 1) A ")
            strSQL.AppendLine(" WHERE 1 = 1 AND A.HRKeyInFlag = '1' AND A.OTCompID = @OTCompID ")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@OTCompID", DbType.String, ManageOTDecRow.OTCompID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        ''' <summary>
        ''' (交易)查詢特定加班事後申報單
        ''' </summary>
        ''' <param name="ManageOTDecRow"></param>
        ''' <param name="tran"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function QueryByKey(ManageOTDecRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            'Dim db As Database = DatabaseFactory.CreateDatabase("testConnectionString")

            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine(" SELECT * ")
            strSQL.AppendLine(" FROM (SELECT OT.HRKeyInFlag, OT.OTCompID, OT.OTEmpID, P.NameN, OT.DeptID, OT.OrganID, OT.OTFormNO, OT.OTRegisterID, OT.OTTxnID, OT.OTTypeID, OTT.CodeCName, OT.OTStatus ")
            strSQL.AppendLine(" , CASE OT.OTStatus WHEN '1' THEN '暫存' WHEN '2' THEN '送簽' WHEN '3' THEN '核准' WHEN '4' THEN '駁回' WHEN '5' THEN '刪除' WHEN '6' THEN '取消' WHEN '7' THEN '作廢' WHEN '9' THEN '計薪後收回' END AS OTStatusName ")
            'strSQL.AppendLine(" , (OT.OTStartDate + '~' + ISNULL(OT2.OTEndDate, OT.OTEndDate)) AS OTDate ")
            strSQL.AppendLine(" , OT.OTStartDate ,ISNULL(OT2.OTEndDate, OT.OTEndDate) AS OTEndDate ")
            'strSQL.AppendLine(" , (LEFT(OT.OTStartTime, 2) + ':' + RIGHT(OT.OTStartTime, 2) + '~' + ISNULL(LEFT(OT2.OTEndTime, 2) + ':' + RIGHT(OT2.OTEndTime, 2), LEFT(OT.OTEndTime, 2) + ':' + RIGHT(OT.OTEndTime, 2))) AS OTTime ")
            '-----------------------
            strSQL.AppendLine(" , LEFT(OT.OTStartTime, 2) + ':' + RIGHT(OT.OTStartTime, 2) AS OTStartTime ")
            strSQL.AppendLine(" , ISNULL(LEFT(OT2.OTEndTime, 2) + ':' + RIGHT(OT2.OTEndTime, 2), LEFT(OT.OTEndTime, 2) + ':' + RIGHT(OT.OTEndTime, 2)) AS OTEndTime ")
            '-----------------------
            'strSQL.AppendLine(" ,  Convert(Decimal(10,1),((CAST(OT.OTTotalTime AS FLOAT)-CAST(OT.MealTime AS FLOAT))/CAST(60 AS FLOAT))+((CAST(ISNULL(OT2.OTTotalTime,0) AS FLOAT)-CAST(ISNULL(OT2.MealTime,0) AS FLOAT))/CAST(60 AS FLOAT))) AS OTTotalTime")
            strSQL.AppendLine(" ,  Convert(Decimal(10,1),Round(Convert(Decimal(10,2),((CAST(OT.OTTotalTime AS FLOAT)-CAST(OT.MealTime AS FLOAT))/CAST(60 AS FLOAT))+((CAST(ISNULL(OT2.OTTotalTime,0) AS FLOAT)-CAST(ISNULL(OT2.MealTime,0) AS FLOAT))/CAST(60 AS FLOAT))),1),1) AS OTTotalTime")
            strSQL.AppendLine(" , ISNULL(AI.FileName, '') AS FileName ")
            strSQL.AppendLine(" , OT.OTSeq, OT.OTSeqNo, OT.OTFromAdvanceTxnId, OT.DeptName, OT.OrganName, OT.FlowCaseID, OT.SalaryOrAdjust, OT.AdjustInvalidDate, OT.AdjustStatus, OT.MealFlag, OT.MealTime, OT.OTReasonID, OT.OTRegisterDate, OT.OTValidDate, OT.OTValidID, OT.OverTimeFlag, OT.ToOverTimeDate, OT.ToOverTimeFlag, OT.OTRejectDate, OT.OTRejectID, OT.OTGovernmentNo, OT.OTSalaryPaid, OT.HolidayOrNot, OT.ProcessDate, OT.OTPayDate, OT.OTModifyDate, OT.OTRemark, OT.KeyInComp, OT.KeyInID, OT.LastChgComp, OT.LastChgID, OT.LastChgDate, OT.OTRegisterComp ")
            strSQL.AppendLine(" FROM OverTimeDeclaration OT ")
            strSQL.AppendLine(" LEFT JOIN OverTimeDeclaration OT2 ON OT2.OTTxnID = OT.OTTxnID AND OT2.OTSeqNo = 2 AND OT2.OverTimeFlag='1'")
            strSQL.AppendLine(" LEFT JOIN " + _eHRMSDB_ITRD + ".[dbo].[Personal] P ON P.EmpID = OT.OTEmpID AND P.CompID = OT.OTCompID ")
            strSQL.AppendLine(" LEFT JOIN AT_CodeMap AS OTT ON OT.OTTypeID = OTT.Code AND  OTT.TabName = 'OverTime' AND  OTT.FldName = 'OverTimeType' AND OTT.NotShowFlag = '0' ")
            strSQL.AppendLine(" LEFT JOIN AttachInfo AI ON AI.AttachID IS NOT NULL AND AI.AttachID <> '' AND AI.AttachID = OT.OTAttachment AND AI.FileSize > 0 WHERE OT.OTSeqNo = 1) A ")
            strSQL.AppendLine(" WHERE 1 = 1 AND A.HRKeyInFlag = '1' AND A.OTCompID = @OTCompID ")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@OTCompID", DbType.String, ManageOTDecRow.OTCompID.Value)

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
            ''Dim db As Database = DatabaseFactory.CreateDatabase("testConnectionString")

            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine(" SELECT * From OverTimeDeclaration")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

#End Region

#Region "檢查資料是否存在"

        ''' <summary>
        ''' 檢查資料是否存在
        ''' </summary>
        ''' <param name="ManageOTDecRow"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function IsDataExists(ByVal ManageOTDecRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            ''Dim db As Database = DatabaseFactory.CreateDatabase("testConnectionString")

            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From OverTimeDeclaration")
            strSQL.AppendLine("Where OTFormNO = @OTFormNO")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@OTFormNO", DbType.String, ManageOTDecRow.OTFormNO.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        ''' <summary>
        ''' (交易)檢查資料是否存在
        ''' </summary>
        ''' <param name="ManageOTDecRow"></param>
        ''' <param name="tran"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function IsDataExists(ByVal ManageOTDecRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            ''Dim db As Database = DatabaseFactory.CreateDatabase("testConnectionString")

            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From OverTimeDeclaration")
            strSQL.AppendLine("Where OTFormNO = @OTFormNO")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@OTFormNO", DbType.String, ManageOTDecRow.OTFormNO.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

#End Region

#Region "新增"

        ''' <summary>
        ''' 新增加班事後申報單
        ''' </summary>
        ''' <param name="ManageOTDecRow"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Insert(ByVal ManageOTDecRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            ''Dim db As Database = DatabaseFactory.CreateDatabase("testConnectionString")

            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine(" INSERT INTO OverTimeDeclaration (OTCompID, OTEmpID, OTStartDate, OTEndDate, OTSeq, OTTxnID, OTSeqNo, OTFromAdvanceTxnId, DeptID, OrganID, DeptName, OrganName, FlowCaseID, OTStartTime, OTEndTime, OTTotalTime, SalaryOrAdjust, AdjustInvalidDate, AdjustStatus, AdjustDate, MealFlag, MealTime, OTTypeID, OTReasonID, OTReasonMemo, OTAttachment, OTFormNO, OTRegisterID, OTRegisterDate, OTStatus, OTValidDate, OTValidID, OTRejectDate, OTRejectID, OTGovernmentNo, OTSalaryPaid, HolidayOrNot, ProcessDate, OTPayDate, OTModifyDate, OTRemark, KeyInComp, KeyInID, HRKeyInFlag, LastChgComp, LastChgID, LastChgDate, OTRegisterComp) ")
            strSQL.AppendLine(" VALUES ( ")
            strSQL.AppendLine(" @OTCompID, @OTEmpID, @OTStartDate, @OTEndDate, @OTSeq, @OTTxnID, @OTSeqNo, '', @DeptID, @OrganID ")
            strSQL.AppendLine(" , @DeptName, @OrganName, '', @OTStartTime, @OTEndTime, @OTTotalTime, @SalaryOrAdjust, @AdjustInvalidDate, '', '1900-01-01 00:00:00.000' ")
            strSQL.AppendLine(" , @MealFlag, @MealTime, @OTTypeID, '', @OTReasonMemo, @OTAttachment, @OTFormNO, @OTRegisterID, @OTRegisterDate, '1' ")
            strSQL.AppendLine(" , '1900-01-01 00:00:00.000', '', '1900-01-01 00:00:00.000', '', '', '0', @HolidayOrNot, '1900-01-01 00:00:00.000', '', '1900-01-01 00:00:00.000' ")
            strSQL.AppendLine(" , '', '', '', '1', @LastChgComp, @LastChgID, @LastChgDate, @OTRegisterComp ")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@OTCompID", DbType.String, ManageOTDecRow.OTCompID.Value)
            db.AddInParameter(dbcmd, "@OTEmpID", DbType.String, ManageOTDecRow.OTEmpID.Value)
            db.AddInParameter(dbcmd, "@OTStartDate", DbType.String, ManageOTDecRow.OTStartDate.Value)
            db.AddInParameter(dbcmd, "@OTEndDate", DbType.String, ManageOTDecRow.OTEndDate.Value)
            db.AddInParameter(dbcmd, "@OTSeq", DbType.Int32, ManageOTDecRow.OTSeq.Value)
            db.AddInParameter(dbcmd, "@OTTxnID", DbType.String, ManageOTDecRow.OTTxnID.Value)
            db.AddInParameter(dbcmd, "@OTSeqNo", DbType.String, ManageOTDecRow.OTSeqNo.Value)
            db.AddInParameter(dbcmd, "@DeptID", DbType.String, ManageOTDecRow.DeptID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, ManageOTDecRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@DeptName", DbType.String, ManageOTDecRow.DeptName.Value)
            db.AddInParameter(dbcmd, "@OrganName", DbType.String, ManageOTDecRow.OrganName.Value)
            db.AddInParameter(dbcmd, "@OTStartTime", DbType.String, ManageOTDecRow.OTStartTime.Value)
            db.AddInParameter(dbcmd, "@OTEndTime", DbType.String, ManageOTDecRow.OTEndTime.Value)
            db.AddInParameter(dbcmd, "@OTTotalTime", DbType.Int32, ManageOTDecRow.OTTotalTime.Value)
            db.AddInParameter(dbcmd, "@SalaryOrAdjust", DbType.String, ManageOTDecRow.SalaryOrAdjust.Value)
            db.AddInParameter(dbcmd, "@AdjustInvalidDate", DbType.Date, If(IsDateTimeNull(ManageOTDecRow.AdjustInvalidDate.Value), Convert.ToDateTime("1900/1/1"), ManageOTDecRow.AdjustInvalidDate.Value))
            db.AddInParameter(dbcmd, "@MealFlag", DbType.String, ManageOTDecRow.MealFlag.Value)
            db.AddInParameter(dbcmd, "@MealTime", DbType.Int32, ManageOTDecRow.MealTime.Value)
            db.AddInParameter(dbcmd, "@OTTypeID", DbType.String, ManageOTDecRow.OTTypeID.Value)
            db.AddInParameter(dbcmd, "@OTReasonMemo", DbType.String, ManageOTDecRow.OTReasonMemo.Value)
            db.AddInParameter(dbcmd, "@OTAttachment", DbType.String, ManageOTDecRow.OTAttachment.Value)
            db.AddInParameter(dbcmd, "@OTFormNO", DbType.String, ManageOTDecRow.OTFormNO.Value)
            db.AddInParameter(dbcmd, "@OTRegisterID", DbType.String, ManageOTDecRow.OTRegisterID.Value)
            db.AddInParameter(dbcmd, "@OTRegisterDate", DbType.Date, If(IsDateTimeNull(ManageOTDecRow.OTRegisterDate.Value) = True, Convert.ToDateTime("1900/1/1"), ManageOTDecRow.OTRegisterDate.Value))
            db.AddInParameter(dbcmd, "@HolidayOrNot", DbType.String, ManageOTDecRow.HolidayOrNot.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, ManageOTDecRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, ManageOTDecRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, If(IsDateTimeNull(ManageOTDecRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), ManageOTDecRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@OTRegisterComp", DbType.String, ManageOTDecRow.OTRegisterComp.Value)
            Return db.ExecuteNonQuery(dbcmd)
        End Function

        ''' <summary>
        ''' (交易)新增加班事後申報單
        ''' </summary>
        ''' <param name="ManageOTDecRow"></param>
        ''' <param name="tran"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Insert(ByVal ManageOTDecRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            ''Dim db As Database = DatabaseFactory.CreateDatabase("testConnectionString")

            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine(" INSERT INTO OverTimeDeclaration (OTCompID, OTEmpID, OTStartDate, OTEndDate, OTSeq, OTTxnID, OTSeqNo, OTFromAdvanceTxnId, DeptID, OrganID, DeptName, OrganName, FlowCaseID, OTStartTime, OTEndTime, OTTotalTime, SalaryOrAdjust, AdjustInvalidDate, AdjustStatus, AdjustDate, MealFlag, MealTime, OTTypeID, OTReasonID, OTReasonMemo, OTAttachment, OTFormNO, OTRegisterID, OTRegisterDate, OTStatus, OTValidDate, OTValidID, OTRejectDate, OTRejectID, OTGovernmentNo, OTSalaryPaid, HolidayOrNot, ProcessDate, OTPayDate, OTModifyDate, OTRemark, KeyInComp, KeyInID, HRKeyInFlag, LastChgComp, LastChgID, LastChgDate, OTRegisterComp) ")
            strSQL.AppendLine(" VALUES ( ")
            strSQL.AppendLine(" @OTCompID, @OTEmpID, @OTStartDate, @OTEndDate, @OTSeq, @OTTxnID, @OTSeqNo, '', @DeptID, @OrganID ")
            strSQL.AppendLine(" , @DeptName, @OrganName, '', @OTStartTime, @OTEndTime, @OTTotalTime, @SalaryOrAdjust, @AdjustInvalidDate, '', '1900-01-01 00:00:00.000' ")
            strSQL.AppendLine(" , @MealFlag, @MealTime, @OTTypeID, '', @OTReasonMemo, @OTAttachment, @OTFormNO, @OTRegisterID, @OTRegisterDate, '1' ")
            strSQL.AppendLine(" , '1900-01-01 00:00:00.000', '', '1900-01-01 00:00:00.000', '', '', '0', @HolidayOrNot, '1900-01-01 00:00:00.000', '', '1900-01-01 00:00:00.000' ")
            strSQL.AppendLine(" , '', '', '', '1', @LastChgComp, @LastChgID, @LastChgDate, @OTRegisterComp ")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@OTCompID", DbType.String, ManageOTDecRow.OTCompID.Value)
            db.AddInParameter(dbcmd, "@OTEmpID", DbType.String, ManageOTDecRow.OTEmpID.Value)
            db.AddInParameter(dbcmd, "@OTStartDate", DbType.String, ManageOTDecRow.OTStartDate.Value)
            db.AddInParameter(dbcmd, "@OTEndDate", DbType.String, ManageOTDecRow.OTEndDate.Value)
            db.AddInParameter(dbcmd, "@OTSeq", DbType.Int32, ManageOTDecRow.OTSeq.Value)
            db.AddInParameter(dbcmd, "@OTTxnID", DbType.String, ManageOTDecRow.OTTxnID.Value)
            db.AddInParameter(dbcmd, "@OTSeqNo", DbType.String, ManageOTDecRow.OTSeqNo.Value)
            db.AddInParameter(dbcmd, "@DeptID", DbType.String, ManageOTDecRow.DeptID.Value)
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, ManageOTDecRow.OrganID.Value)
            db.AddInParameter(dbcmd, "@DeptName", DbType.String, ManageOTDecRow.DeptName.Value)
            db.AddInParameter(dbcmd, "@OrganName", DbType.String, ManageOTDecRow.OrganName.Value)
            db.AddInParameter(dbcmd, "@OTStartTime", DbType.String, ManageOTDecRow.OTStartTime.Value)
            db.AddInParameter(dbcmd, "@OTEndTime", DbType.String, ManageOTDecRow.OTEndTime.Value)
            db.AddInParameter(dbcmd, "@OTTotalTime", DbType.Int32, ManageOTDecRow.OTTotalTime.Value)
            db.AddInParameter(dbcmd, "@SalaryOrAdjust", DbType.String, ManageOTDecRow.SalaryOrAdjust.Value)
            db.AddInParameter(dbcmd, "@AdjustInvalidDate", DbType.Date, If(IsDateTimeNull(ManageOTDecRow.AdjustInvalidDate.Value), Convert.ToDateTime("1900/1/1"), ManageOTDecRow.AdjustInvalidDate.Value))
            db.AddInParameter(dbcmd, "@MealFlag", DbType.String, ManageOTDecRow.MealFlag.Value)
            db.AddInParameter(dbcmd, "@MealTime", DbType.Int32, ManageOTDecRow.MealTime.Value)
            db.AddInParameter(dbcmd, "@OTTypeID", DbType.String, ManageOTDecRow.OTTypeID.Value)
            db.AddInParameter(dbcmd, "@OTReasonMemo", DbType.String, ManageOTDecRow.OTReasonMemo.Value)
            db.AddInParameter(dbcmd, "@OTAttachment", DbType.String, ManageOTDecRow.OTAttachment.Value)
            db.AddInParameter(dbcmd, "@OTFormNO", DbType.String, ManageOTDecRow.OTFormNO.Value)
            db.AddInParameter(dbcmd, "@OTRegisterID", DbType.String, ManageOTDecRow.OTRegisterID.Value)
            db.AddInParameter(dbcmd, "@OTRegisterDate", DbType.Date, If(IsDateTimeNull(ManageOTDecRow.OTRegisterDate.Value) = True, Convert.ToDateTime("1900/1/1"), ManageOTDecRow.OTRegisterDate.Value))
            db.AddInParameter(dbcmd, "@HolidayOrNot", DbType.String, ManageOTDecRow.HolidayOrNot.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, ManageOTDecRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, ManageOTDecRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, If(IsDateTimeNull(ManageOTDecRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), ManageOTDecRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@OTRegisterComp", DbType.String, ManageOTDecRow.OTRegisterComp.Value)
            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        ''' <summary>
        ''' 新增加班事後申報單-回傳有多少資料列受影響
        ''' </summary>
        ''' <param name="ManageOTDecRow"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Insert(ByVal ManageOTDecRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            ''Dim db As Database = DatabaseFactory.CreateDatabase("testConnectionString")

            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine(" INSERT INTO OverTimeDeclaration (OTCompID, OTEmpID, OTStartDate, OTEndDate, OTSeq, OTTxnID, OTSeqNo, OTFromAdvanceTxnId, DeptID, OrganID, DeptName, OrganName, FlowCaseID, OTStartTime, OTEndTime, OTTotalTime, SalaryOrAdjust, AdjustInvalidDate, AdjustStatus, AdjustDate, MealFlag, MealTime, OTTypeID, OTReasonID, OTReasonMemo, OTAttachment, OTFormNO, OTRegisterID, OTRegisterDate, OTStatus, OTValidDate, OTValidID, OTRejectDate, OTRejectID, OTGovernmentNo, OTSalaryPaid, HolidayOrNot, ProcessDate, OTPayDate, OTModifyDate, OTRemark, KeyInComp, KeyInID, HRKeyInFlag, LastChgComp, LastChgID, LastChgDate, OTRegisterComp) ")
            strSQL.AppendLine(" VALUES ( ")
            strSQL.AppendLine(" @OTCompID, @OTEmpID, @OTStartDate, @OTEndDate, @OTSeq, @OTTxnID, @OTSeqNo, '', @DeptID, @OrganID ")
            strSQL.AppendLine(" , @DeptName, @OrganName, '', @OTStartTime, @OTEndTime, @OTTotalTime, @SalaryOrAdjust, @AdjustInvalidDate, '', '1900-01-01 00:00:00.000' ")
            strSQL.AppendLine(" , @MealFlag, @MealTime, @OTTypeID, '', @OTReasonMemo, @OTAttachment, @OTFormNO, @OTRegisterID, @OTRegisterDate, '1' ")
            strSQL.AppendLine(" , '1900-01-01 00:00:00.000', '', '1900-01-01 00:00:00.000', '', '', '0', @HolidayOrNot, '1900-01-01 00:00:00.000', '', '1900-01-01 00:00:00.000' ")
            strSQL.AppendLine(" , '', '', '', '1', @LastChgComp, @LastChgID, @LastChgDate, @OTRegisterComp")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In ManageOTDecRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@OTCompID", DbType.String, r.OTCompID.Value)
                        db.AddInParameter(dbcmd, "@OTEmpID", DbType.String, r.OTEmpID.Value)
                        db.AddInParameter(dbcmd, "@OTStartDate", DbType.String, r.OTStartDate.Value)
                        db.AddInParameter(dbcmd, "@OTEndDate", DbType.String, r.OTEndDate.Value)
                        db.AddInParameter(dbcmd, "@OTSeq", DbType.Int32, r.OTSeq.Value)
                        db.AddInParameter(dbcmd, "@OTTxnID", DbType.String, r.OTTxnID.Value)
                        db.AddInParameter(dbcmd, "@OTSeqNo", DbType.String, r.OTSeqNo.Value)
                        db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                        db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                        db.AddInParameter(dbcmd, "@DeptName", DbType.String, r.DeptName.Value)
                        db.AddInParameter(dbcmd, "@OrganName", DbType.String, r.OrganName.Value)
                        db.AddInParameter(dbcmd, "@OTStartTime", DbType.String, r.OTStartTime.Value)
                        db.AddInParameter(dbcmd, "@OTEndTime", DbType.String, r.OTEndTime.Value)
                        db.AddInParameter(dbcmd, "@OTTotalTime", DbType.Int32, r.OTTotalTime.Value)
                        db.AddInParameter(dbcmd, "@SalaryOrAdjust", DbType.String, r.SalaryOrAdjust.Value)
                        db.AddInParameter(dbcmd, "@AdjustInvalidDate", DbType.Date, If(IsDateTimeNull(r.AdjustInvalidDate.Value), Convert.ToDateTime("1900/1/1"), r.AdjustInvalidDate.Value))
                        db.AddInParameter(dbcmd, "@MealFlag", DbType.String, r.MealFlag.Value)
                        db.AddInParameter(dbcmd, "@MealTime", DbType.Int32, r.MealTime.Value)
                        db.AddInParameter(dbcmd, "@OTTypeID", DbType.String, r.OTTypeID.Value)
                        db.AddInParameter(dbcmd, "@OTReasonMemo", DbType.String, r.OTReasonMemo.Value)
                        db.AddInParameter(dbcmd, "@OTAttachment", DbType.String, r.OTAttachment.Value)
                        db.AddInParameter(dbcmd, "@OTFormNO", DbType.String, r.OTFormNO.Value)
                        db.AddInParameter(dbcmd, "@OTRegisterID", DbType.String, r.OTRegisterID.Value)
                        db.AddInParameter(dbcmd, "@OTRegisterDate", DbType.Date, If(IsDateTimeNull(r.OTRegisterDate.Value) = True, Convert.ToDateTime("1900/1/1"), r.OTRegisterDate.Value))
                        db.AddInParameter(dbcmd, "@HolidayOrNot", DbType.String, r.HolidayOrNot.Value)
                        db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, If(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@OTRegisterComp", DbType.String, r.OTRegisterComp.Value)

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
        ''' (交易)新增加班事後申報單-回傳有多少資料列受影響
        ''' </summary>
        ''' <param name="ManageOTDecRow"></param>
        ''' <param name="tran"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Insert(ByVal ManageOTDecRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            ''Dim db As Database = DatabaseFactory.CreateDatabase("testConnectionString")

            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine(" INSERT INTO OverTimeDeclaration (OTCompID, OTEmpID, OTStartDate, OTEndDate, OTSeq, OTTxnID, OTSeqNo, OTFromAdvanceTxnId, DeptID, OrganID, DeptName, OrganName, FlowCaseID, OTStartTime, OTEndTime, OTTotalTime, SalaryOrAdjust, AdjustInvalidDate, AdjustStatus, AdjustDate, MealFlag, MealTime, OTTypeID, OTReasonID, OTReasonMemo, OTAttachment, OTFormNO, OTRegisterID, OTRegisterDate, OTStatus, OTValidDate, OTValidID, OTRejectDate, OTRejectID, OTGovernmentNo, OTSalaryPaid, HolidayOrNot, ProcessDate, OTPayDate, OTModifyDate, OTRemark, KeyInComp, KeyInID, HRKeyInFlag, LastChgComp, LastChgID, LastChgDate) ")
            strSQL.AppendLine(" VALUES ( ")
            strSQL.AppendLine(" @OTCompID, @OTEmpID, @OTStartDate, @OTEndDate, @OTSeq, @OTTxnID, @OTSeqNo, '', @DeptID, @OrganID ")
            strSQL.AppendLine(" , @DeptName, @OrganName, '', @OTStartTime, @OTEndTime, @OTTotalTime, @SalaryOrAdjust, @AdjustInvalidDate, '', '1900-01-01 00:00:00.000' ")
            strSQL.AppendLine(" , @MealFlag, @MealTime, @OTTypeID, '', @OTReasonMemo, @OTAttachment, @OTFormNO, @OTRegisterID, @OTRegisterDate, '1' ")
            strSQL.AppendLine(" , '1900-01-01 00:00:00.000', '', '1900-01-01 00:00:00.000', '', '', '0', @HolidayOrNot, '1900-01-01 00:00:00.000', '', '1900-01-01 00:00:00.000' ")
            strSQL.AppendLine(" , '', '', '', '1', @LastChgComp, @LastChgID, @LastChgDate, @OTRegisterComp")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In ManageOTDecRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@OTCompID", DbType.String, r.OTCompID.Value)
                db.AddInParameter(dbcmd, "@OTEmpID", DbType.String, r.OTEmpID.Value)
                db.AddInParameter(dbcmd, "@OTStartDate", DbType.String, r.OTStartDate.Value)
                db.AddInParameter(dbcmd, "@OTEndDate", DbType.String, r.OTEndDate.Value)
                db.AddInParameter(dbcmd, "@OTSeq", DbType.Int32, r.OTSeq.Value)
                db.AddInParameter(dbcmd, "@OTTxnID", DbType.String, r.OTTxnID.Value)
                db.AddInParameter(dbcmd, "@OTSeqNo", DbType.String, r.OTSeqNo.Value)
                db.AddInParameter(dbcmd, "@DeptID", DbType.String, r.DeptID.Value)
                db.AddInParameter(dbcmd, "@OrganID", DbType.String, r.OrganID.Value)
                db.AddInParameter(dbcmd, "@DeptName", DbType.String, r.DeptName.Value)
                db.AddInParameter(dbcmd, "@OrganName", DbType.String, r.OrganName.Value)
                db.AddInParameter(dbcmd, "@OTStartTime", DbType.String, r.OTStartTime.Value)
                db.AddInParameter(dbcmd, "@OTEndTime", DbType.String, r.OTEndTime.Value)
                db.AddInParameter(dbcmd, "@OTTotalTime", DbType.Int32, r.OTTotalTime.Value)
                db.AddInParameter(dbcmd, "@SalaryOrAdjust", DbType.String, r.SalaryOrAdjust.Value)
                db.AddInParameter(dbcmd, "@AdjustInvalidDate", DbType.Date, If(IsDateTimeNull(r.AdjustInvalidDate.Value), Convert.ToDateTime("1900/1/1"), r.AdjustInvalidDate.Value))
                db.AddInParameter(dbcmd, "@MealFlag", DbType.String, r.MealFlag.Value)
                db.AddInParameter(dbcmd, "@MealTime", DbType.Int32, r.MealTime.Value)
                db.AddInParameter(dbcmd, "@OTTypeID", DbType.String, r.OTTypeID.Value)
                db.AddInParameter(dbcmd, "@OTReasonMemo", DbType.String, r.OTReasonMemo.Value)
                db.AddInParameter(dbcmd, "@OTAttachment", DbType.String, r.OTAttachment.Value)
                db.AddInParameter(dbcmd, "@OTFormNO", DbType.String, r.OTFormNO.Value)
                db.AddInParameter(dbcmd, "@OTRegisterID", DbType.String, r.OTRegisterID.Value)
                db.AddInParameter(dbcmd, "@OTRegisterDate", DbType.Date, If(IsDateTimeNull(r.OTRegisterDate.Value) = True, Convert.ToDateTime("1900/1/1"), r.OTRegisterDate.Value))
                db.AddInParameter(dbcmd, "@HolidayOrNot", DbType.String, r.HolidayOrNot.Value)
                db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, If(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@OTRegisterComp", DbType.String, r.OTRegisterComp.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

#End Region

#Region "修改"

        ''' <summary>
        ''' 修改加班事後申報單狀態為刪除
        ''' </summary>
        ''' <param name="ManageOTDecRow"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function UpdateStatus(ByVal ManageOTDecRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine(" UPDATE OverTimeDeclaration SET ")
            For i As Integer = 0 To ManageOTDecRow.FieldNames.Length - 1
                If Not ManageOTDecRow.IsIdentityField(ManageOTDecRow.FieldNames(i)) AndAlso ManageOTDecRow.IsUpdated(ManageOTDecRow.FieldNames(i)) AndAlso ManageOTDecRow.CreateUpdateSQL(ManageOTDecRow.FieldNames(i)) Then
                    strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, ManageOTDecRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine(" WHERE OTTxnID = @OTTxnID ")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If ManageOTDecRow.OTTxnID.Updated Then db.AddInParameter(dbcmd, "@OTTxnID", DbType.String, ManageOTDecRow.OTTxnID.Value)
            If ManageOTDecRow.OTStatus.Updated Then db.AddInParameter(dbcmd, "@OTStatus", DbType.String, ManageOTDecRow.OTStatus.Value)
            If ManageOTDecRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, ManageOTDecRow.LastChgComp.Value)
            If ManageOTDecRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(ManageOTDecRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), ManageOTDecRow.LastChgDate.Value))
            If ManageOTDecRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, ManageOTDecRow.LastChgID.Value)
            Return db.ExecuteNonQuery(dbcmd)
        End Function

        ''' <summary>
        ''' (交易)修改加班事後申報單
        ''' </summary>
        ''' <param name="ManageOTDecRow"></param>
        ''' <param name="tran"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        'Public Function Update(ByVal ManageOTDecRow As Row, ByVal tran As DbTransaction) As Integer
        '    Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
        '    Dim dbcmd As DbCommand
        '    Dim strSQL As StringBuilder = New StringBuilder()
        '    Dim strDot As String = String.Empty

        '    strSQL.AppendLine(" UPDATE OverTimeDeclaration SET ")
        '    For i As Integer = 0 To ManageOTDecRow.FieldNames.Length - 1
        '        If Not ManageOTDecRow.IsIdentityField(ManageOTDecRow.FieldNames(i)) AndAlso ManageOTDecRow.IsUpdated(ManageOTDecRow.FieldNames(i)) AndAlso ManageOTDecRow.CreateUpdateSQL(ManageOTDecRow.FieldNames(i)) Then
        '            strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, ManageOTDecRow.FieldNames(i)))
        '            strDot = ","
        '        End If
        '    Next
        '    If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
        '    strSQL.AppendLine(" WHERE OTTxnID = @OTTxnID ")

        '    dbcmd = db.GetSqlStringCommand(strSQL.ToString())
        '    If ManageOTDecRow.OTTxnID.Updated Then db.AddInParameter(dbcmd, "@OTTxnID", DbType.String, ManageOTDecRow.OTTxnID.Value)
        '    If ManageOTDecRow.OTStatus.Updated Then db.AddInParameter(dbcmd, "@OTStatus", DbType.String, ManageOTDecRow.OTStatus.Value)

        '    Return db.ExecuteNonQuery(dbcmd, tran)
        'End Function

#End Region

#Region "刪除"

        ''' <summary>
        ''' 刪除加班事後申報單
        ''' </summary>
        ''' <param name="ManageOTDecRow"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        'Public Function DeleteRowByPrimaryKey(ByVal ManageOTDecRow As Row) As Integer
        '    Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
        '    Dim dbcmd As DbCommand
        '    Dim strSQL As StringBuilder = New StringBuilder()

        '    strSQL.AppendLine(" DELETE FROM OverTimeDeclaration")
        '    strSQL.AppendLine(" WHERE OTFormNO = @OTFormNO ")

        '    dbcmd = db.GetSqlStringCommand(strSQL.ToString())
        '    db.AddInParameter(dbcmd, "@OTFormNO", DbType.String, ManageOTDecRow.OTFormNO.Value)

        '    Return db.ExecuteNonQuery(dbcmd)
        'End Function

        ''' <summary>
        ''' (交易)刪除加班事後申報單
        ''' </summary>
        ''' <param name="ManageOTDecRow"></param>
        ''' <param name="tran"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        'Public Function DeleteRowByPrimaryKey(ByVal ManageOTDecRow As Row, ByVal tran As DbTransaction) As Integer
        '    Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
        '    Dim dbcmd As DbCommand
        '    Dim strSQL As StringBuilder = New StringBuilder()

        '    strSQL.AppendLine(" DELETE FROM OverTimeDeclaration")
        '    strSQL.AppendLine(" WHERE OTFormNO = @OTFormNO ")

        '    dbcmd = db.GetSqlStringCommand(strSQL.ToString())
        '    db.AddInParameter(dbcmd, "@OTFormNO", DbType.String, ManageOTDecRow.OTFormNO.Value)

        '    If db.ExecuteNonQuery(dbcmd, tran) <> 0 Then
        '        strSQL.Clear()
        '        strSQL.AppendLine(" SELECT * FROM OverTimeAdvance")
        '        strSQL.AppendLine(" WHERE OTTxnID = @OTTxnID ")
        '        Dim dba As Database = DatabaseFactory.CreateDatabase("AattendantDB")
        '        Dim cmda As DbCommand = dba.GetSqlStringCommand(strSQL.ToString())
        '        dba.AddInParameter(cmda, "@OTTxnID", DbType.String, ManageOTDecRow.OTTxnID.Value)
        '        Using dt As DataTable = dba.ExecuteDataSet(cmda).Tables(0)
        '            If dt.Rows.Count > 0 Then
        '                strSQL.Clear()
        '                strSQL.AppendLine(" DELETE FROM OverTimeAdvance")
        '                strSQL.AppendLine(" WHERE OTTxnID = @OTTxnID ")

        '                Dim dbb As Database = DatabaseFactory.CreateDatabase("AattendantDB")
        '                Dim cmdb As DbCommand = dbb.GetSqlStringCommand(strSQL.ToString())
        '                dbb.AddInParameter(cmdb, "@OTTxnID", DbType.String, ManageOTDecRow.OTTxnID.Value)
        '                Return db.ExecuteNonQuery(cmdb)
        '            End If
        '        End Using
        '    End If
        '    'Return db.ExecuteNonQuery(dbcmd, tran)
        'End Function

        ''' <summary>
        ''' 刪除加班事後申報單-回傳有多少資料列受影響
        ''' </summary>
        ''' <param name="ManageOTDecRow"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        'Public Function DeleteRowByPrimaryKey(ByVal ManageOTDecRow As Row()) As Integer
        '    Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
        '    Dim dbcmd As DbCommand
        '    Dim strSQL As StringBuilder = New StringBuilder()
        '    Dim intRowsAffected As Integer = 0

        '    strSQL.AppendLine(" DELETE FROM OverTimeDeclaration")
        '    strSQL.AppendLine(" WHERE OTFormNO = @OTFormNO ")

        '    dbcmd = db.GetSqlStringCommand(strSQL.ToString())
        '    Using cn As DbConnection = db.CreateConnection()
        '        cn.Open()
        '        Dim tran As DbTransaction = cn.BeginTransaction()
        '        Dim inTrans As Boolean = True

        '        Try
        '            For Each r As Row In ManageOTDecRow
        '                dbcmd.Parameters.Clear()
        '                db.AddInParameter(dbcmd, "@OTFormNO", DbType.String, r.OTFormNO.Value)

        '                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
        '            Next
        '            tran.Commit()
        '            inTrans = False
        '        Catch ex As Exception
        '            If inTrans Then tran.Rollback()
        '            Throw
        '        Finally
        '            tran.Dispose()
        '            If cn.State = ConnectionState.Open Then cn.Close()
        '        End Try
        '    End Using
        '    Return intRowsAffected
        'End Function

        ''' <summary>
        ''' (交易)刪除加班事後申報單-回傳有多少資料列受影響
        ''' </summary>
        ''' <param name="ManageOTDecRow"></param>
        ''' <param name="tran"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        'Public Function DeleteRowByPrimaryKey(ByVal ManageOTDecRow As Row(), ByVal tran As DbTransaction) As Integer
        '    Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
        '    Dim dbcmd As DbCommand
        '    Dim strSQL As StringBuilder = New StringBuilder()
        '    Dim intRowsAffected As Integer = 0

        '    strSQL.AppendLine(" DELETE FROM OverTimeDeclaration")
        '    strSQL.AppendLine(" WHERE OTFormNO = @OTFormNO ")

        '    dbcmd = db.GetSqlStringCommand(strSQL.ToString())

        '    For Each r As Row In ManageOTDecRow
        '        dbcmd.Parameters.Clear()
        '        db.AddInParameter(dbcmd, "@OTFormNO", DbType.String, r.OTFormNO.Value)

        '        intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
        '    Next
        '    Return intRowsAffected
        'End Function

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
