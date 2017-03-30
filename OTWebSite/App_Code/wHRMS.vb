Imports Microsoft.VisualBasic

Public Class wHRMS
#Region " 字串加解密 "

    '- 字串加密
    Public Function stringEncoding(ByVal strSrc As String) As String
        Dim strKey As String = "Bsp.wHRMS"
        Dim objSec As New ECSecurity.clsEnCoder()
        Dim strOutput As String = ""
        Dim strMsg As String = ""

        Try
            If objSec.gfunEncryptString2W(strKey, strSrc, strOutput, strMsg) = True Then
                Return strOutput
            Else
                Throw New Exception(strMsg)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '- 字串解密
    Public Shared Function stringDecoding(ByVal strSrc As String) As String
        Dim strKey As String = "Bsp.wHRMS"
        Dim objSec As New ECSecurity.clsEnCoder()
        Dim strOutput As String = ""
        Dim strMsg As String = ""

        Try
            If objSec.gfunDecryptString2W(strKey, strSrc, strOutput, strMsg) = True Then
                Return strOutput
            Else
                Throw New Exception(strMsg)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
End Class
