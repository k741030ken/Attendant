Imports Microsoft.VisualBasic

Public Class BaseState
    Private _isolationKey As String
    Public Const SESSION_KEY_DELIMITER As String = ":"

    Private Function currentSession() As HttpSessionState
        Return HttpContext.Current.Session
    End Function

    Public Sub New(ByVal isolationKey As String)
        _isolationKey = isolationKey & SESSION_KEY_DELIMITER
    End Sub

    Default Public Property BaseState(ByVal name As String) As Object
        Get
            If MyClass.currentSession IsNot Nothing Then
                Return Me.currentSession(_isolationKey & name)
            End If
            Return ""
        End Get
        Set(ByVal value As Object)
            If MyClass.currentSession IsNot Nothing Then
                MyClass.currentSession(_isolationKey & name) = value
            End If
        End Set
    End Property

    Public ReadOnly Property Count() As Integer
        Get
            Dim intCount As Integer = 0

            For Each key As String In currentSession.Keys
                If key.IndexOf(_isolationKey) = 0 Then
                    intCount += 1
                End If
            Next

            Return intCount
        End Get
    End Property

    Public Sub Clear()
        Clear(False)
    End Sub

    Public Sub Clear(ByVal isClearChildState As Boolean)
        ClearSession(currentSession, _isolationKey.Substring(0, _isolationKey.Length - SESSION_KEY_DELIMITER.Length), isClearChildState)
    End Sub

    Public Sub Remove(ByVal name As String)
        MyClass.currentSession.Remove(_isolationKey & name)
    End Sub

    Public Sub RemoveAll()
        MyClass.Clear()
    End Sub

    Public Function Index(ByVal name As String) As Integer
        Dim sn As HttpSessionState = currentSession()

        For intLoop As Integer = sn.Count - 1 To 0 Step -1
            If (sn.Keys(intLoop) = _isolationKey & name) Then
                Return intLoop
            End If
        Next
        Return -1
    End Function

    Public Sub Add(ByVal name As String, ByVal value As Object)
        MyClass.currentSession.Add(_isolationKey & name, value)
    End Sub

    Public Sub ClearSession(ByVal currentSession As HttpSessionState, ByVal keyPrefix As String, ByVal isClearChildState As Boolean)
        If keyPrefix <> "" Then
            If Not isClearChildState Then
                keyPrefix = keyPrefix & SESSION_KEY_DELIMITER
            End If

            For intLoop As Integer = currentSession.Count - 1 To 0 Step -1
                If currentSession.Keys(intLoop).IndexOf(keyPrefix) = 0 Then
                    currentSession.RemoveAt(intLoop)
                End If
            Next
        End If
    End Sub

End Class
