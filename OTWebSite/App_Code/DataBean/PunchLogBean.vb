Imports Microsoft.VisualBasic

Public Class PunchLogBean
    Private _compID As String
    Private _empID As String
    Private _empName As String
    Private _punchSDate As String
    Private _punchEDate As String
    Private _punchSTime As String
    Private _punchETime As String
    Private _punchSeq As String
    Private _deptID As String
    Private _deptName As String
    Private _organID As String
    Private _organName As String
    Private _flowOrganID As String
    Private _flowOrganName As String
    Private _sex As String
    Private _punchFlag As String
    Private _workTypeID As String
    Private _workType As String
    Private _mAFT10_FLAG As String
    Private _abnormalFlag As String
    Private _abnormalReasonID As String
    Private _abnormalReasonCN As String
    Private _abnormalDesc As String
    Private _batchFlag As String
    Private _source As String
    Private _punchUserIP As String
    Private _rotateFlag As String
    Private _aPPContent As String
    Private _lat As String
    Private _lon As String
    Private _gPSType As String
    Private _oS As String
    Private _deviceID As String
    Private _lastChgComp As String
    Private _lastChgCompName As String
    Private _lastChgID As String
    Private _lastChgName As String
    Private _lastChgDate As String
    ''' <summary>
    ''' 公司ID
    ''' </summary>
    Public Property CompID As String
        Get
            Return _compID
        End Get
        Set(ByVal value As String)
            _compID = value
        End Set
    End Property
    ''' <summary>
    ''' 員工ID
    ''' </summary>
    Public Property EmpID As String
        Get
            Return _empID
        End Get
        Set(ByVal value As String)
            _empID = value
        End Set
    End Property
    ''' <summary>
    ''' 員工姓名
    ''' </summary>
    Public Property EmpName As String
        Get
            Return _empName
        End Get
        Set(ByVal value As String)
            _empName = value
        End Set
    End Property
    ''' <summary>
    ''' 打卡日期開始
    ''' </summary>
    Public Property PunchSDate As String
        Get
            Return _punchSDate
        End Get
        Set(ByVal value As String)
            _punchSDate = value
        End Set
    End Property
    ''' <summary>
    ''' 打卡日期結束
    ''' </summary>
    Public Property PunchEDate As String
        Get
            Return _punchEDate
        End Get
        Set(ByVal value As String)
            _punchEDate = value
        End Set
    End Property
    ''' <summary>
    ''' 打卡時間開始
    ''' </summary>
    Public Property PunchSTime As String
        Get
            Return _punchSTime
        End Get
        Set(ByVal value As String)
            _punchSTime = value
        End Set
    End Property
    ''' <summary>
    ''' 打卡時間結束
    ''' </summary>
    Public Property PunchETime As String
        Get
            Return _punchETime
        End Get
        Set(ByVal value As String)
            _punchETime = value
        End Set
    End Property
    ''' <summary>
    ''' PunchConfirmSeq
    ''' </summary>
    Public Property PunchSeq As String
        Get
            Return _punchSeq
        End Get
        Set(ByVal value As String)
            _punchSeq = value
        End Set
    End Property
    ''' <summary>
    ''' 部門ID
    ''' </summary>
    Public Property DeptID As String
        Get
            Return _deptID
        End Get
        Set(ByVal value As String)
            _deptID = value
        End Set
    End Property
    ''' <summary>
    ''' 部門名稱
    ''' </summary>
    Public Property DeptName As String
        Get
            Return _deptName
        End Get
        Set(ByVal value As String)
            _deptName = value
        End Set
    End Property
    ''' <summary>
    ''' 科組課ID
    ''' </summary>
    Public Property OrganID As String
        Get
            Return _organID
        End Get
        Set(ByVal value As String)
            _organID = value
        End Set
    End Property
    ''' <summary>
    ''' 科組課名稱
    ''' </summary>
    Public Property OrganName As String
        Get
            Return _organName
        End Get
        Set(ByVal value As String)
            _organName = value
        End Set
    End Property
    ''' <summary>
    ''' 科組課ID
    ''' </summary>
    Public Property FlowOrganID As String
        Get
            Return _flowOrganID
        End Get
        Set(ByVal value As String)
            _organID = value
        End Set
    End Property
    ''' <summary>
    ''' 科組課名稱
    ''' </summary>
    Public Property FlowOrganName As String
        Get
            Return _flowOrganName
        End Get
        Set(ByVal value As String)
            _flowOrganName = value
        End Set
    End Property
    ''' <summary>
    ''' 性別
    ''' </summary>
    Public Property Sex As String
        Get
            Return _sex
        End Get
        Set(ByVal value As String)
            _sex = value
        End Set
    End Property
    ''' <summary>
    ''' PunchFlag
    ''' </summary>
    Public Property PunchFlag As String
        Get
            Return _punchFlag
        End Get
        Set(ByVal value As String)
            _punchFlag = value
        End Set
    End Property
    ''' <summary>
    ''' 工作性質ID
    ''' </summary>
    Public Property WorkTypeID As String
        Get
            Return _workTypeID
        End Get
        Set(ByVal value As String)
            _workTypeID = value
        End Set
    End Property
    ''' <summary>
    ''' 工作性質
    ''' </summary>
    Public Property WorkType As String
        Get
            Return _workType
        End Get
        Set(ByVal value As String)
            _workType = value
        End Set
    End Property
    ''' <summary>
    ''' 女性十點打卡註記
    ''' </summary>
    Public Property MAFT10_FLAG As String
        Get
            Return _mAFT10_FLAG
        End Get
        Set(ByVal value As String)
            _mAFT10_FLAG = value
        End Set
    End Property
    ''' <summary>
    ''' AbnormalFlag
    ''' </summary>
    Public Property AbnormalFlag As String
        Get
            Return _abnormalFlag
        End Get
        Set(ByVal value As String)
            _abnormalFlag = value
        End Set
    End Property
    ''' <summary>
    ''' AbnormalReasonID
    ''' </summary>
    Public Property AbnormalReasonID As String
        Get
            Return _abnormalReasonID
        End Get
        Set(ByVal value As String)
            _abnormalReasonID = value
        End Set
    End Property
    ''' <summary>
    ''' AbnormalReasonCN
    ''' </summary>
    Public Property AbnormalReasonCN As String
        Get
            Return _abnormalReasonCN
        End Get
        Set(ByVal value As String)
            _abnormalReasonCN = value
        End Set
    End Property
    ''' <summary>
    ''' AbnormalDesc
    ''' </summary>
    Public Property AbnormalDesc As String
        Get
            Return _abnormalDesc
        End Get
        Set(ByVal value As String)
            _abnormalDesc = value
        End Set
    End Property
    ''' <summary>
    ''' Source
    ''' </summary>
    Public Property Source As String
        Get
            Return _source
        End Get
        Set(ByVal value As String)
            _source = value
        End Set
    End Property
    ''' <summary>
    ''' PunchUserIP
    ''' </summary>
    Public Property PunchUserIP As String
        Get
            Return _punchUserIP
        End Get
        Set(ByVal value As String)
            _punchUserIP = value
        End Set
    End Property
    ''' <summary>
    ''' RotateFlag
    ''' </summary>
    Public Property RotateFlag As String
        Get
            Return _rotateFlag
        End Get
        Set(ByVal value As String)
            _rotateFlag = value
        End Set
    End Property
    ''' <summary>
    ''' APPContent
    ''' </summary>
    Public Property APPContent As String
        Get
            Return _aPPContent
        End Get
        Set(ByVal value As String)
            _aPPContent = value
        End Set
    End Property
    ''' <summary>
    ''' Lat
    ''' </summary>
    Public Property Lat As String
        Get
            Return _lat
        End Get
        Set(ByVal value As String)
            _lat = value
        End Set
    End Property
    ''' <summary>
    ''' Lon
    ''' </summary>
    Public Property Lon As String
        Get
            Return _lon
        End Get
        Set(ByVal value As String)
            _lon = value
        End Set
    End Property
    ''' <summary>
    ''' GPSType
    ''' </summary>
    Public Property GPSType As String
        Get
            Return _gPSType
        End Get
        Set(ByVal value As String)
            _gPSType = value
        End Set
    End Property
    ''' <summary>
    ''' OS
    ''' </summary>
    Public Property OS As String
        Get
            Return _oS
        End Get
        Set(ByVal value As String)
            _oS = value
        End Set
    End Property
    ''' <summary>
    ''' DeviceID
    ''' </summary>
    Public Property DeviceID As String
        Get
            Return _deviceID
        End Get
        Set(ByVal value As String)
            _deviceID = value
        End Set
    End Property
    ''' <summary>
    ''' 最後變更者公司ID
    ''' </summary>
    Public Property LastChgComp As String
        Get
            Return _lastChgComp
        End Get
        Set(ByVal value As String)
            _lastChgComp = value
        End Set
    End Property
    ''' <summary>
    ''' 最後變更者公司名稱
    ''' </summary>
    Public Property LastChgCompName As String
        Get
            Return _lastChgCompName
        End Get
        Set(ByVal value As String)
            _lastChgCompName = value
        End Set
    End Property
    ''' <summary>
    ''' 最後變更者ID
    ''' </summary>
    Public Property LastChgID As String
        Get
            Return _lastChgID
        End Get
        Set(ByVal value As String)
            _lastChgID = value
        End Set
    End Property
    ''' <summary>
    ''' 最後變更者姓名
    ''' </summary>
    Public Property LastChgName As String
        Get
            Return _lastChgName
        End Get
        Set(ByVal value As String)
            _lastChgName = value
        End Set
    End Property
    ''' <summary>
    ''' 最後變更時間
    ''' </summary>
    Public Property LastChgDate As String
        Get
            Return _lastChgDate
        End Get
        Set(ByVal value As String)
            _lastChgDate = value
        End Set
    End Property
End Class
