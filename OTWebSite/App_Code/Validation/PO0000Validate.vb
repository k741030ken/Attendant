Imports Microsoft.VisualBasic

Partial Public Class POValidation
    ''' <summary>
    ''' 字串是否為數字
    ''' </summary>
    ''' <param name="str">String</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Shared Function IsAllNumber(ByVal str As String) As Boolean
        Dim result As Boolean = False
        Dim i As Double = 0
        result = Double.TryParse(str, i)
        Return result
    End Function
End Class
