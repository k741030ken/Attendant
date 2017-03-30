Imports Microsoft.VisualBasic

Public Class TemplateModel 'PCxxxModel
    Private _otCompID As String
    Private _otEmpID As String
    Private _nameN As String
    Private _sex As String
    Private _templateGridData As List(Of TemplateGridData)
    ''' <summary>
    ''' 加班人公司ID
    ''' </summary>
    Public Property OTCompID As String
        Get
            Return _otCompID
        End Get
        Set(ByVal value As String)
            _otCompID = value
        End Set
    End Property
    ''' <summary>
    ''' 加班人ID
    ''' </summary>
    Public Property OTEmpID As String
        Get
            Return _otEmpID
        End Get
        Set(ByVal value As String)
            _otEmpID = value
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
    ''' Grid Data(List)
    ''' </summary>
    Public Property TemplateGridDataList As List(Of TemplateGridData)
        Get
            Return _templateGridData
        End Get
        Set(ByVal value As List(Of TemplateGridData))
            _templateGridData = value
        End Set
    End Property
End Class

''' <summary>
''' Grid Data
''' </summary>
Public Class TemplateGridData 'PCxxxGridData
    Private _otCompID As String
    Private _otEmpID As String
    Private _nameN As String
    Private _sex As String
    Private _showOTEmp As String
    Private _showSex As String
    ''' <summary>
    ''' 加班人公司ID
    ''' </summary>
    Public Property OTCompID As String
        Get
            Return _otCompID
        End Get
        Set(ByVal value As String)
            _otCompID = value
        End Set
    End Property
    ''' <summary>
    ''' 加班人ID
    ''' </summary>
    Public Property OTEmpID As String
        Get
            Return _otEmpID
        End Get
        Set(ByVal value As String)
            _otEmpID = value
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
    ''' 加班人(Format)
    ''' </summary>
    Public Property ShowOTEmp As String
        Get
            Return _showOTEmp
        End Get
        Set(ByVal value As String)
            _showOTEmp = value
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
    ''' 加班人性別(Format)
    ''' </summary>
    Public Property ShowSex As String
        Get
            Return _showSex
        End Get
        Set(ByVal value As String)
            _showSex = value
        End Set
    End Property
End Class
