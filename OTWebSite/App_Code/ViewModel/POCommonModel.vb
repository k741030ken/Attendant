Imports Microsoft.VisualBasic

''' <summary>
''' 行政組織Model
''' </summary>
''' <remarks></remarks>
Public Class OrganListModel
    Private _orgType As String
    Private _orgTypeName As String
    Private _deptID As String
    Private _deptName As String
    Private _organID As String
    Private _organName As String
    ''' <summary>
    ''' 處代碼
    ''' </summary>
    Public Property OrgType As String
        Get
            Return _orgType
        End Get
        Set(ByVal value As String)
            _orgType = value
        End Set
    End Property
    ''' <summary>
    ''' 處名稱
    ''' </summary>
    Public Property OrgTypeName As String
        Get
            Return _orgTypeName
        End Get
        Set(ByVal value As String)
            _orgTypeName = value
        End Set
    End Property
    ''' <summary>
    ''' 部代碼
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
    ''' 部名稱
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
    ''' 科組課代碼
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
End Class

''' <summary>
''' 功能組織Model
''' </summary>
''' <remarks></remarks>
Public Class FlowOrganListModel
    Private _businessType As String
    Private _roleCode As String
    Private _upOrganID As String
    Private _deptID As String
    Private _organID As String
    Private _organName As String
    Private _organLevel As String
    ''' <summary>
    ''' 業務類別
    ''' </summary>
    Public Property BusinessType As String
        Get
            Return _businessType
        End Get
        Set(ByVal value As String)
            _businessType = value
        End Set
    End Property
    ''' <summary>
    ''' RoleCode
    ''' </summary>
    Public Property RoleCode As String
        Get
            Return _roleCode
        End Get
        Set(ByVal value As String)
            _roleCode = value
        End Set
    End Property
    ''' <summary>
    ''' 上階部門
    ''' </summary>
    Public Property UpOrganID As String
        Get
            Return _upOrganID
        End Get
        Set(ByVal value As String)
            _upOrganID = value
        End Set
    End Property
    ''' <summary>
    ''' 部代碼
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
    ''' 單位代碼
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
    ''' 單位名稱
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
    ''' 單位階層
    ''' </summary>
    Public Property OrganLevel As String
        Get
            Return _organLevel
        End Get
        Set(ByVal value As String)
            _organLevel = value
        End Set
    End Property
End Class
