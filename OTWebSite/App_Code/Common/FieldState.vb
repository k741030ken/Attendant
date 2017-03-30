Imports Microsoft.VisualBasic

<Serializable()> _
Public Class FieldState
    Private _FieldName As String
    Private _HeaderName As String
    Private _Visible As Boolean
    Private _Return As Boolean
    Private _Width As System.Web.UI.WebControls.Unit

    Public Property FieldName() As String
        Get
            Return _FieldName
        End Get
        Set(ByVal value As String)
            _FieldName = value
        End Set
    End Property

    Public Property HeaderName() As String
        Get
            Return _HeaderName
        End Get
        Set(ByVal value As String)
            _HeaderName = value
        End Set
    End Property

    Public Property Visible() As String
        Get
            Return _Visible
        End Get
        Set(ByVal value As String)
            _Visible = value
        End Set
    End Property

    Public Property [Return]() As Boolean
        Get
            Return _Return
        End Get
        Set(ByVal value As Boolean)
            _Return = value
        End Set
    End Property

    Public Property Width() As System.Web.UI.WebControls.Unit
        Get
            Return _Width
        End Get
        Set(ByVal value As System.Web.UI.WebControls.Unit)
            _Width = value
        End Set
    End Property


    Public Sub New()
        _FieldName = ""
        _HeaderName = ""
        _Visible = True
        _Return = False
    End Sub

    Public Sub New(ByVal sFieldName As String, ByVal sHeaderName As String, ByVal bVisible As Boolean, ByVal bReturn As Boolean)
        _FieldName = sFieldName
        _HeaderName = sHeaderName
        _Visible = bVisible
        _Return = bReturn
    End Sub

    Public Sub New(ByVal sFieldName As String, ByVal sHeaderName As String, ByVal bVisible As Boolean, ByVal bReturn As Boolean, ByVal sWidth As System.Web.UI.WebControls.Unit)
        _FieldName = sFieldName
        _HeaderName = sHeaderName
        _Visible = bVisible
        _Return = bReturn
        _Width = sWidth
    End Sub
End Class
