Imports Microsoft.VisualBasic

Public Class TemplateBean
    Private _compID As String
    Private _empID As String
    Private _nameN As String
    Private _sex As String
    Private _lastChgComp As String
    Private _lastChgID As String
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
    ''' 加班人ID
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
    ''' 加班人姓名
    ''' </summary>
    Public Property NameN As String
        Get
            Return _nameN
        End Get
        Set(ByVal value As String)
            _nameN = value
        End Set
    End Property
    ''' <summary>
    ''' 加班人性別
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
