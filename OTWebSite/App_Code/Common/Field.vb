Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Public Class Field(Of T)
    Private m_FieldName As String = String.Empty
    Private m_Value As T = Nothing
    Private m_OldValue As T = Nothing
    Private m_Updated As Boolean = False
    Private m_CreateUpdateSQL As Boolean = True
    Private m_Null As Boolean = False

    Public Sub New()

    End Sub

    Public Sub New(ByVal fieldName As String, ByVal createUpdateSQL As Boolean)
        m_FieldName = fieldName
        m_CreateUpdateSQL = createUpdateSQL
    End Sub

    Public Sub New(ByVal fieldName As String, ByVal value As Object)
        m_FieldName = fieldName
        SetValue(value, False)
    End Sub

    Public Property FieldName() As String
        Get
            Return m_FieldName
        End Get
        Set(ByVal value As String)
            m_FieldName = value
        End Set
    End Property

    Public Property Value() As T
        Get
            Return m_Value
        End Get
        Set(ByVal value As T)
            m_Value = value
            m_Updated = True
        End Set
    End Property

    Public Property OldValue() As T
        Get
            Return m_OldValue
        End Get
        Set(ByVal value As T)
            m_OldValue = value
        End Set
    End Property

    Public Property Updated() As Boolean
        Get
            Return m_Updated
        End Get
        Set(ByVal value As Boolean)
            m_Updated = value
        End Set
    End Property

    Public ReadOnly Property CreateUpdateSQL() As Boolean
        Get
            Return m_CreateUpdateSQL
        End Get
    End Property

    Public ReadOnly Property Null() As Boolean
        Get
            Return m_Null
        End Get
    End Property

    Public Sub SetInitValue(ByVal value As Object)
        If value Is Nothing OrElse Convert.IsDBNull(value) Then
            m_Null = True
        End If

        If m_Null Then
            m_Value = Nothing
            m_OldValue = Nothing
        Else
            m_Value = CType(value, T)
            m_OldValue = CType(value, T)
        End If
    End Sub

    Public Sub SetValue(ByVal value As Object)
        SetValue(value, True)
    End Sub

    Public Sub SetValue(ByVal value As Object, ByVal updated As Boolean)
        If value Is Nothing OrElse Convert.IsDBNull(value) Then
            m_Null = True
        End If

        If Not m_Updated Then m_OldValue = m_Value
        m_Updated = updated

        If m_Null Then
            m_Value = Nothing
        Else
            m_Value = CType(value, T)
        End If
    End Sub
End Class
