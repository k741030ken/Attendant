Imports Microsoft.VisualBasic
Imports System.Web.Script.Serialization

Public Class ReturnMessage
    Public success As Boolean
    Public target As String
    Public message As String
    Public result As Object

    Public Function ToJSONString() As String
        Dim serializer As New JavaScriptSerializer
        Return serializer.Serialize(Me)
    End Function
End Class
