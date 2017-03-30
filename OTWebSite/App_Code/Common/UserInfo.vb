'****************************************************
'功能說明：User Information基本資料的類別
'建立人員：A02976
'建立日期：2008.10.03
'****************************************************
Imports Microsoft.VisualBasic

Public Class UserInfo
    Private _LoginSysID As String = "" '20140808 wei add Login的系統別代碼
    Private _SelectCompRoleID As String = "" '20140818 wei add選擇授權公司代碼 
    Private _SelectCompRoleName As String = "" '20150528 wei add選擇授權公司 
    Private _SelectFunID As String = "" '20150319 wei add選擇FunID 
    Private _SysID As System.Collections.Generic.List(Of String)   '20140807 wei add 系統別代碼
    Private _CompRoleID As System.Collections.Generic.List(Of String)  '20140807 wei add 授權公司代碼
    Private _UserID As String = ""
    Private _UserName As String = ""
    Private _CompID As String = ""  '20140807 wei add 公司代碼
    Private _CompName As String = ""  '20140807 wei add 公司
    Private _DeptID As String = ""
    Private _DeptName As String = ""
    Private _OrganID As String = ""
    Private _OrganName As String = ""
    Private _GroupID As System.Collections.Generic.List(Of String)
    Private _ActSysID As System.Collections.Generic.List(Of String)    '20140807 wei add 系統別代碼
    Private _ActCompRoleID As System.Collections.Generic.List(Of String)   '20140807 wei add 授權公司代碼
    Private _ActUserID As String = ""
    Private _ActUserName As String = ""
    Private _ActDeptID As String = ""
    Private _ActCompID As String = ""   '20140807 wei add 公司代碼
    Private _ActCompName As String = ""  '20140807 wei add 公司
    Private _ActDeptName As String = ""
    Private _ActOrganID As String = ""
    Private _ActOrganName As String = ""
    Private _ActGroupID As System.Collections.Generic.List(Of String)
    Private _QueryDept As String = ""
    Private _IsBranchUser As String = "NA"
    Private _IsSysAdmin As String = "NA" '20140811 wei add 是否為系統管理者


    '20140808 wei add Login的系統別代碼
    Public Property LoginSysID() As String
        Get
            Return _LoginSysID
        End Get
        Set(ByVal value As String)
            _LoginSysID = value
        End Set
    End Property
    '20140818 wei add 選擇授權公司代碼
    Public Property SelectCompRoleID() As String
        Get
            Return _SelectCompRoleID
        End Get
        Set(ByVal value As String)
            _SelectCompRoleID = value
        End Set
    End Property
    '20150528 wei add 選擇授權公司
    Public Property SelectCompRoleName() As String
        Get
            Return _SelectCompRoleName
        End Get
        Set(ByVal value As String)
            _SelectCompRoleName = value
        End Set
    End Property
    '20150319 wei add 選擇FunID
    Public Property SelectFunID() As String
        Get
            Return _SelectFunID
        End Get
        Set(ByVal value As String)
            _SelectFunID = value
        End Set
    End Property

    '201401807 wei add 系統別代碼
    Public Property SysID() As System.Collections.Generic.List(Of String)
        Get
            Return _SysID
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of String))
            _SysID = value
        End Set
    End Property
    '201401807 wei add 授權公司代碼
    Public Property CompRoleID() As System.Collections.Generic.List(Of String)
        Get
            Return _CompRoleID
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of String))
            _CompRoleID = value
        End Set
    End Property

    Public Property UserID() As String
        Get
            Return _UserID
        End Get
        Set(ByVal value As String)
            _UserID = value
        End Set
    End Property

    Public Property UserName() As String
        Get
            Return _UserName
        End Get
        Set(ByVal value As String)
            _UserName = value
        End Set
    End Property
    '201401807 wei add 公司代碼
    Public Property CompID() As String
        Get
            Return _CompID
        End Get
        Set(ByVal value As String)
            _CompID = value
        End Set
    End Property
    '201401807 wei add 公司代碼
    Public Property CompName() As String
        Get
            Return _CompName
        End Get
        Set(ByVal value As String)
            _CompName = value
        End Set
    End Property
    Public Property DeptID() As String
        Get
            Return _DeptID
        End Get
        Set(ByVal value As String)
            _DeptID = value
        End Set
    End Property

    Public Property DeptName() As String
        Get
            Return _DeptName
        End Get
        Set(ByVal value As String)
            _DeptName = value
        End Set
    End Property

    Public Property OrganID() As String
        Get
            Return _OrganID
        End Get
        Set(ByVal value As String)
            _OrganID = value
        End Set
    End Property

    Public Property OrganName() As String
        Get
            Return _OrganName
        End Get
        Set(ByVal value As String)
            _OrganName = value
        End Set
    End Property

    Public Property GroupID() As System.Collections.Generic.List(Of String)
        Get
            Return _GroupID
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of String))
            _GroupID = value
        End Set
    End Property
    '201401807 wei add 系統別代碼
    Public Property ActSysID() As System.Collections.Generic.List(Of String)
        Get
            Return _ActSysID
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of String))
            _ActSysID = value
        End Set
    End Property
    '201401807 wei add 授權公司代碼
    Public Property ActCompRoleID() As System.Collections.Generic.List(Of String)
        Get
            Return _ActCompRoleID
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of String))
            _ActCompRoleID = value
        End Set
    End Property
    Public Property ActUserID() As String
        Get
            Return _ActUserID
        End Get
        Set(ByVal value As String)
            _ActUserID = value
        End Set
    End Property

    Public Property ActUserName() As String
        Get
            Return _ActUserName
        End Get
        Set(ByVal value As String)
            _ActUserName = value
        End Set
    End Property
    '201401807 wei add 公司代碼
    Public Property ActCompID() As String
        Get
            Return _ActCompID
        End Get
        Set(ByVal value As String)
            _ActCompID = value
        End Set
    End Property
    '201401807 wei add 公司
    Public Property ActCompName() As String
        Get
            Return _ActCompName
        End Get
        Set(ByVal value As String)
            _ActCompName = value
        End Set
    End Property
    Public Property ActDeptID() As String
        Get
            Return _ActDeptID
        End Get
        Set(ByVal value As String)
            _ActDeptID = value
        End Set
    End Property

    Public Property ActDeptName() As String
        Get
            Return _ActDeptName
        End Get
        Set(ByVal value As String)
            _ActDeptName = value
        End Set
    End Property

    Public Property ActOrganID() As String
        Get
            Return _ActOrganID
        End Get
        Set(ByVal value As String)
            _ActOrganID = value
        End Set
    End Property

    Public Property ActOrganName() As String
        Get
            Return _ActOrganName
        End Get
        Set(ByVal value As String)
            _ActOrganName = value
        End Set
    End Property
    Public Property ActGroupID() As System.Collections.Generic.List(Of String)
        Get
            Return _ActGroupID
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of String))
            _ActGroupID = value
        End Set
    End Property

    Public Property QueryDept() As String
        Get
            Return _QueryDept
        End Get
        Set(ByVal value As String)
            _QueryDept = value
        End Set
    End Property

    Public Property IsBranchUser() As String
        Get
            Return _IsBranchUser
        End Get
        Set(ByVal value As String)
            _IsBranchUser = value
        End Set
    End Property

    '20140811 wei add 是否為系統管理者
    Public Property IsSysAdmin() As String
        Get
            Return _IsSysAdmin
        End Get
        Set(ByVal value As String)
            _IsSysAdmin = value
        End Set
    End Property
End Class
