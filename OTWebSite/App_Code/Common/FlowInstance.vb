'****************************************************
'功能說明：Flow Key
'建立人員：CL 
'建立日期：2009.12.20
'****************************************************

Imports Microsoft.VisualBasic

<Serializable()> _
Public Class FlowInstance
    Private _FlowID As String = ""              '流程名稱
    Private _FlowCaseID As String = ""          '案件流程編號
    Private _FlowStepID As String = ""          '關卡編號
    Private _FlowLogBatNo As Integer = 0        '流程批號
    Private _FlowLogID As String = ""           '紀錄編號
    Private _OtherInfo As String = ""           '附加訊息

    Public Property FlowID() As String
        Get
            Return _FlowID
        End Get
        Set(ByVal value As String)
            _FlowID = value
        End Set
    End Property

    Public Property FlowCaseID() As String
        Get
            Return _FlowCaseID
        End Get
        Set(ByVal value As String)
            _FlowCaseID = value
        End Set
    End Property

    Public Property FlowStepID() As String
        Get
            Return _FlowStepID
        End Get
        Set(ByVal value As String)
            _FlowStepID = value
        End Set
    End Property

    Public Property FlowLogBatNo() As Integer
        Get
            Return _FlowLogBatNo
        End Get
        Set(ByVal value As Integer)
            _FlowLogBatNo = value
        End Set
    End Property

    Public Property FlowLogID() As String
        Get
            Return _FlowLogID
        End Get
        Set(ByVal value As String)
            _FlowLogID = value
        End Set
    End Property

    Public Property OtherInfo() As String
        Get
            Return _OtherInfo
        End Get
        Set(ByVal value As String)
            _OtherInfo = value
        End Set
    End Property

    Public Sub New()

    End Sub

    Public Sub New(ByVal FlowID As String, ByVal FlowCaseID As String, ByVal FlowStepID As String, ByVal FlowLogBatNo As Integer, ByVal FlowLogID As String)
        _FlowID = FlowID
        _FlowCaseID = FlowCaseID
        _FlowStepID = FlowStepID
        _FlowLogBatNo = FlowLogBatNo
        _FlowLogID = FlowLogID
    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("FlowID={0},FlowCaseID={1},FlowStepID={2},FlowLogBatNo={3},FlowLogID={4}", _
            FlowID, FlowCaseID, FlowStepID, FlowLogBatNo.ToString(), FlowLogID)
    End Function
End Class
