'****************************************************
'功能說明：網頁資訊傳遞物件
'建立人員：Chung
'建立日期：2013.01.29
'****************************************************
Imports Microsoft.VisualBasic
Imports System

Public Class TransferInfo
    <Serializable()> _
    Public Enum TransferInfoType
        Transfer
        [Call]
        [Return]
        ModalChild
        ModalReturn
        ModelessChild
    End Enum

    Public ReadOnly CallerScsProgramID As String
    Public ReadOnly CallerPageSessionID As String
    Public ReadOnly CallerUrl As String
    Public ReadOnly CallerPageID As String
    Public ReadOnly CalleeUrl As String
    Public CalleePageID As String
    Public RunFunID As String
    'Public NeedButtonControl As Boolean = True    '是否使用Button功能列
    Public AllReceivers As New System.Collections.Generic.List(Of String)
    Public FlowUse As Boolean = False
    Public CommandList() As ButtonState
    Public ReadOnly Args As Object()
    Public Result As Object = Nothing
    Public Committed As Boolean = False
    Public CheckRight As Boolean = True
    Friend CallerViewState As String = ""
    Private _transferType As TransferInfoType
    Public Message As String    '當子網頁返回時將訊息傳回父網頁
    Public ForceAlertMessage As Boolean '當子網頁返回時將是否強制回應訊息旗標傳回父網頁

    'Public Sub New(ByVal TransferType As TransferInfoType, ByVal _callerScsProgramID As String, ByVal _CallerUrl As String, ByVal _CalleeUrl As String, ByVal _PageSessionID As String, ByVal bList() As ButtonInfo, ByVal _NeedButtonControl As Boolean, ByVal ParamArray _Args As Object())
    Public Sub New(ByVal TransferType As TransferInfoType, ByVal _callerScsProgramID As String, ByVal _CallerUrl As String, ByVal _CalleeUrl As String, ByVal _PageSessionID As String, ByVal bList() As ButtonState, ByVal Receiver As String, ByVal ParamArray _Args As Object())
        _transferType = TransferType
        CallerScsProgramID = _callerScsProgramID
        CallerUrl = _CallerUrl
        CallerPageID = Bsp.Utility.ExtractPageID(_CallerUrl)
        CalleeUrl = _CalleeUrl
        CalleePageID = Bsp.Utility.ExtractPageID(_CalleeUrl)
        CallerPageSessionID = _PageSessionID
        CommandList = bList
        Dim aryReceiver() As String = Receiver.Split(",")
        For Each strID As String In aryReceiver
            AllReceivers.Add(strID.ToLower())
        Next
        AllReceivers.Add(CalleePageID.ToLower())
        RunFunID = CalleePageID
        'NeedButtonControl = _NeedButtonControl

        Args = _Args
        If _Args.Length > 0 Then
            If TypeOf _Args(0) Is Object() Then
                Dim argsList As New List(Of Object)
                For Each Param As Object In _Args(0)
                    If TypeOf Param Is String Then
                        argsList.Add(Param)
                    End If
                Next
                For cnt As Integer = 1 To UBound(_Args)
                    argsList.Add(_Args(cnt))
                Next
                Args = argsList.ToArray()
            End If
        End If
    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("CallerUrl='{0}',CallerPageID='{1}',CalleeUrl='{2}',CalleePageID='{3}',CommandListCount='{4}'", CallerUrl, CallerPageID, CalleeUrl, CalleePageID, CommandList.Length.ToString())
    End Function

    Friend Property TransferType() As TransferInfoType
        Get
            Return _transferType
        End Get
        Set(ByVal value As TransferInfoType)
            _transferType = value
        End Set
    End Property

    Public Function GetTrasnferType() As TransferInfoType
        Return _transferType
    End Function

End Class
