Imports Microsoft.VisualBasic

Public Class PO1100Model 'PCxxxModel
    Private _compID As String
    Private _compID_Old As String
    Private _compName As String
    Private _deptID_Old As String
    Private _deptID As String
    Private _deptName As String
    Private _organID As String
    Private _organID_Old As String
    Private _organName As String
    Private _specialFlag As String
    Private _lastChgComp As String
    Private _lastChgCompName As String
    Private _lastChgID As String
    Private _lastChgName As String
    Private _lastChgDate As String
    Private _po1100GridData As List(Of PO1100GridData)
    ''' <summary>
    ''' 加班人公司ID
    ''' </summary>
    Public Property CompID As String
        Get
            Return _compID_Old
        End Get
        Set(ByVal value As String)
            _compID_Old = value
        End Set
    End Property

    Public Property CompID_Old As String
        Get
            Return _compID
        End Get
        Set(ByVal value As String)
            _compID = value
        End Set
    End Property

    ''' <summary>
    ''' 加班人公司名稱
    ''' </summary>
    Public Property CompName As String
        Get
            Return _compName
        End Get
        Set(ByVal value As String)
            _compName = value
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
    ''' 部門ID
    ''' </summary>
    Public Property DeptID_Old As String
        Get
            Return _deptID_Old
        End Get
        Set(ByVal value As String)
            _deptID_Old = value
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
    ''' 科組課名ID
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
    ''' 科組課名ID
    ''' </summary>
    Public Property OrganID_Old As String
        Get
            Return _organID_Old
        End Get
        Set(ByVal value As String)
            _organID_Old = value
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
    ''' 是否為特殊單位
    ''' </summary>
    Public Property SpecialFlag As String
        Get
            Return _specialFlag
        End Get
        Set(ByVal value As String)
            _specialFlag = value
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
    ''' <summary>
    ''' Grid Data(List)
    ''' </summary>
    Public Property PO1100GridDataList As List(Of PO1100GridData)
        Get
            Return _po1100GridData
        End Get
        Set(ByVal value As List(Of PO1100GridData))
            _po1100GridData = value
        End Set
    End Property
End Class

''' <summary>
''' Grid Data
''' </summary>
Public Class PO1100GridData 'PCxxxGridData
    Private _compID As String
    Private _compName As String
    Private _compID_Name As String
    Private _deptID As String
    Private _deptName As String
    Private _deptID_Name As String
    Private _organID As String
    Private _organName As String
    Private _organID_Name As String
    Private _specialFlag As String
    Private _specialFlagName As String
    Private _lastChgComp As String
    Private _lastChgCompName As String
    Private _lastChgCompID_Name As String
    Private _lastChgID As String
    Private _lastChgName As String
    Private _lastChgID_Name As String
    Private _lastChgDate As String
    ''' <summary>
    ''' 加班人公司ID
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
    ''' 加班人公司名稱
    ''' </summary>
    Public Property CompName As String
        Get
            Return _compName
        End Get
        Set(ByVal value As String)
            _compName = value
        End Set
    End Property
    ''' <summary>
    ''' 加班人公司ID+名稱
    ''' </summary>
    Public Property CompID_Name As String
        Get
            Return _compID_Name
        End Get
        Set(ByVal value As String)
            _compID_Name = value
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
    ''' 部門ID+名稱
    ''' </summary>
    Public Property DeptID_Name As String
        Get
            Return _deptID_Name
        End Get
        Set(ByVal value As String)
            _deptID_Name = value
        End Set
    End Property
    ''' <summary>
    ''' 科組課名ID
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
    ''' 科組課ID+名稱
    ''' </summary>
    Public Property OrganID_Name As String
        Get
            Return _organID_Name
        End Get
        Set(ByVal value As String)
            _organID_Name = value
        End Set
    End Property
    ''' <summary>
    ''' 是否為特殊單位
    ''' </summary>
    Public Property SpecialFlag As String
        Get
            Return _specialFlag
        End Get
        Set(ByVal value As String)
            _specialFlag = value
        End Set
    End Property
    ''' <summary>
    ''' 是否為特殊單位
    ''' </summary>
    Public Property SpecialFlagName As String
        Get
            Return _specialFlagName
        End Get
        Set(ByVal value As String)
            _specialFlagName = value
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
    ''' 最後變更者公司ID+名稱
    ''' </summary>
    Public Property LastChgCompID_Name As String
        Get
            Return _lastChgCompID_Name
        End Get
        Set(ByVal value As String)
            _lastChgCompID_Name = value
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
    ''' 最後變更者姓名
    ''' </summary>
    Public Property LastChgID_Name As String
        Get
            Return _lastChgID_Name
        End Get
        Set(ByVal value As String)
            _lastChgID_Name = value
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
