Imports Microsoft.VisualBasic

Partial Public Class POValidation
    ' ''' <summary>
    ' ''' 字串是否為數字
    ' ''' </summary>
    ' ''' <param name="str">String</param>
    ' ''' <returns>Boolean</returns>
    ' ''' <remarks></remarks>
    'Public Shared Function IsAllNumber(ByVal str As String) As Boolean
    '    Dim result As Boolean = False
    '    Dim i As Double = 0
    '    result = Double.TryParse(str, i)
    '    Return result
    'End Function
    ''' <summary>
    ''' String判斷全形(不含中文)
    ''' </summary>
    ''' <param name="str">String</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Shared Function IsAnyOneFullWidthWord(ByVal str As String) As Boolean
        Dim result As Boolean = False
        Dim pattern As String = "^[\u4E00-\u9fa5]+$"
        For index As Integer = 0 To str.Length - 1
            Dim item As Char = str(index)
            '以Regex判斷是否為中文字，中文字視為全形
            If Not Regex.IsMatch(item.ToString(), pattern) Then
                '以16進位值長度判斷是否為全形字
                If String.Format("{0:X}", Convert.ToInt32(item)).Length <> 2 Then
                    result = True
                    Exit For
                End If
            End If
        Next
        Return result
    End Function
    ''' <summary>
    ''' String判斷中文字
    ''' </summary>
    ''' <param name="str">String</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Shared Function IsAnyOneChineseWord(ByVal str As String) As Boolean
        Dim result As Boolean = False
        Dim pattern As String = "^[\u4E00-\u9fa5]+$"
        For index As Integer = 0 To str.Length - 1
            Dim item As Char = str(index)
            '以Regex判斷是否為中文字，中文字視為全形
            If Regex.IsMatch(item.ToString(), pattern) Then
                result = True
                Exit For
            End If
        Next
        Return result
    End Function
End Class
