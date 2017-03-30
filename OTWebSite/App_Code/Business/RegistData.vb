Imports Microsoft.VisualBasic

Public Class RegistData
#Region "將數字解密"
    '********************************************************************************************
    '* 功能說明：將數字解密                                                                     *
    '* 傳入參數１：strEmpID ==> 員工編號, 一定為六碼                                          　*
    '* 傳入參數２：strData  ==> 欲解密的值                                                  　　*
    '* 回傳值：傳回解密後的結果                                                                 *
    '********************************************************************************************
    Public Function funDecryptNumber(strEmpID As String, strData As String) As String
        Try
            Dim strDESKey As String = ""
            Dim DesKey As String = ""
            Dim Key2 As String = ""
            Dim strSrc As String = ""

            If String.IsNullOrEmpty(strEmpID) = True Or String.IsNullOrEmpty(strData) = True Then
                Return ""
            End If

            If strEmpID.Length <> 6 Then
                Return ""
            End If

            Dim desObj As New GSSCM_DES.DES56()
            Dim HD As New HrmsDES.clsCode()

            strDESKey = funGetDESKey()

            DesKey = Convert.ToString(HD.DESDecode(strDESKey))
            Dim strRandomData As String = funRandomData(strData, "1")
            strSrc = Convert.ToString(desObj.strDESEncrypt(strRandomData, DesKey, 1))

            desObj = Nothing
            HD = Nothing
            If strSrc.IndexOf(strEmpID) >= 0 Then '找不到的話,傳回-1,找到的話,回傳一個 Integer數字（從零算起）,表示在字串裡面第幾個字,符合條件。
                Return strSrc.Substring(strEmpID.Length).Replace("\0", "").Trim()
            Else
                Return ""
            End If
        Catch ex As Exception
            Throw
            Return ""
        End Try

    End Function
#End Region
#Region "將數字加密"
    '********************************************************************************************
    '* 功能說明：將數字加密                                                                     *
    '* 傳入參數１：strEmpID ==> 員工編號, 一定為六碼                                            *
    '* 傳入參數２：strData  ==> 欲加密的值                                                      *
    '* 回傳值：傳回加密後的結果                                                                 *
    '********************************************************************************************
    Public Function funEncryptNumber(ByVal strEmpID As String, ByVal strData As String) As Object
        'gfunEncryptNumber = strEmpID & Format(strData, "@@@@@@@@@")
        Dim strSrc As String = ""
        Dim DesObj As New GSSCM_DES.DES56
        Dim HD As New HrmsDES.clsCode
        Dim strKey As String = ""
        Dim strEncryptNumber As String = ""

        If (Trim(strEmpID) = vbNullString) Or (Trim(strData) = vbNullString) Then
            Return ""
        End If

        If strEmpID.Length <> 6 Then
            Return ""
        End If


        strSrc = strEmpID & RSet(strData, 9)
        '    strKey = DesObj.strDESEncrypt(sysPrivateKey, sysMISKey, 1)
        strKey = HD.DESDecode(funGetDESKey())
        '    gfunEncryptNumber = DesObj.strDESEncrypt(strSrc, strKey, 0)
        strEncryptNumber = funRandomData(DesObj.strDESEncrypt(strSrc, strKey, 0), "0")

        HD = Nothing
        DesObj = Nothing
        Return strEncryptNumber
    End Function
#End Region
    

    Public Function funRandomData(ByVal strData As String, ByVal strKind As String) As String
        Dim intLoop As Integer
        Dim strResult As String
        Dim strResult2 As String

        strResult = ""
        strResult2 = ""
        For intLoop = 1 To 16
            If strKind = "0" Then
                strResult = strResult & Mid(strData, intLoop, 1) & Mid(strData, 32 - intLoop + 1, 1)
            Else
                strResult = strResult & Mid(strData, intLoop * 2 - 1, 1)
                strResult2 = Mid(strData, intLoop * 2, 1) & strResult2
            End If
        Next
        funRandomData = strResult & strResult2
    End Function
    Public Function funGetDESKey() As String
        Dim strSql As String

        strSql = "Select top 1 DESKey from Parameter"

        If Bsp.DB.ExecuteScalar(strSql.ToString(), "eHRMSDB") = Nothing Then
            Return ""
        Else
            Return Bsp.DB.ExecuteScalar(strSql.ToString(), "eHRMSDB")
        End If
    End Function
End Class
