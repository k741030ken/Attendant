Imports System
Imports System.IO
Imports System.Security.Cryptography

#Region "<命名空間> ECSecurity 編碼元件"
Namespace ECSecurity

#Region "<介面宣告>ISecurity宣告"
    Public Interface ISecurity
        Function gfunEncryptString(ByVal strInputString As String, ByRef strOutputString As String, ByRef strMessage As String) As Boolean
        Function gfunEncryptString2W(ByVal strKey As String, ByVal strInputString As String, ByRef strOutputString As String, ByRef strMessage As String) As Boolean
        Function gfunDecryptString2W(ByVal strKey As String, ByVal strInputString As String, ByRef strOutputString As String, ByRef strMessage As String) As Boolean
        Function gfunEncryptFile(ByVal strFileName As String, ByVal strInputString As String, ByRef strMessage As String) As Boolean
        Function gfunEncryptFile(ByVal strFileName As String, ByVal msInputMemoryStream As System.IO.MemoryStream, ByRef strMessage As String) As Boolean
        Function gfunDecryptFile(ByVal strFileName As String, ByRef strOutputString As String, ByRef strMessage As String) As Boolean
        Function gfunDecryptFile(ByVal strFileName As String, ByRef msOututMemoryStream As System.IO.MemoryStream, ByRef strMessage As String) As Boolean
    End Interface
#End Region

#Region "<類別宣告>Security宣告"
    Public NotInheritable Class clsEnCoder
        Implements ISecurity

#Region "<變數宣告>公用變數宣告"
        Private encObj As System.Text.UnicodeEncoding
#End Region

#Region "<變數宣告>屬於編碼檔案用的變數宣告"
        Private intReserverLength As Integer '檔案前端(含Key & IV)保留總長度
        Private byteTdesKey() As Byte 'Key陣列
        Private byteTdesIV() As Byte 'IV陣列
        Private cryTdes As TripleDESCryptoServiceProvider '宣告編碼方式
#End Region

#Region "<變數宣告>屬於編碼字串用的變數宣告"
        Private cryEnString As MD5CryptoServiceProvider '宣告字串編碼方式
        Private cryEn2W As DESCryptoServiceProvider   '宣告雙向字串編碼方式
        Private byte2WKey() As Byte = {137, 67, 180, 127, 232, 166, 61, 85}
        Private byte2WIV() As Byte = {156, 154, 163, 62, 23, 160, 227, 208}
#End Region

#Region "<建構與解構>"
        Public Sub New()
            encObj = New System.Text.UnicodeEncoding()
            cryTdes = New TripleDESCryptoServiceProvider()
            cryEnString = New MD5CryptoServiceProvider()
            cryEn2W = New DESCryptoServiceProvider()
        End Sub

        Public Sub Dispose()
            encObj = Nothing
            cryTdes = Nothing
            cryEnString = Nothing
            cryEn2W = Nothing
        End Sub
#End Region

#Region "<Function>產生 Key & IV Function (funWriteKeyFile)"
        Private Function funWriteKeyFile(ByVal strKeyFilename As String) As Boolean
            funWriteKeyFile = False

            '隨機產生Key
            cryTdes.GenerateKey()
            '隨機產生IV(維度)
            cryTdes.GenerateIV()
            '取出Key
            byteTdesKey = cryTdes.Key
            '取出IV
            byteTdesIV = cryTdes.IV

            Dim fsKey As FileStream

            Try
                '把Key & IV 寫入檔案
                fsKey = New FileStream(strKeyFilename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite)
                '把Key長度寫入檔案
                fsKey.WriteByte(CByte(byteTdesKey.Length))
                '把IV長度寫入檔案
                fsKey.WriteByte(CByte(byteTdesIV.Length))
                '計算保留字串長度
                intReserverLength = byteTdesKey.Length + byteTdesIV.Length + 2
                '把Key寫入檔案
                fsKey.Write(byteTdesKey, 0, byteTdesKey.Length)
                '把IV寫入檔案
                fsKey.Write(byteTdesIV, 0, byteTdesIV.Length)
                fsKey.Close()
                funWriteKeyFile = True
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "<Function>讀取 Key & IV Function (funReadKeyFile)"
        Private Function funReadKeyFile(ByVal strKeyFilename As String) As Boolean
            funReadKeyFile = False

            Dim fsKey As FileStream
            Dim intKeyLength As Integer
            Dim intIVLength As Integer

            Try
                '把Key & IV 從檔案讀出
                fsKey = New FileStream(strKeyFilename, FileMode.Open, FileAccess.Read, FileShare.Read)
                '從檔案取出Key長度
                intKeyLength = fsKey.ReadByte()
                '從檔案取出IV長度
                intIVLength = fsKey.ReadByte()
                '計算保留字串長度
                intReserverLength = intKeyLength + intIVLength + 2

                ReDim byteTdesKey(intKeyLength - 1)
                ReDim byteTdesIV(intIVLength - 1)

                '取出Key值
                fsKey.Read(byteTdesKey, 0, intKeyLength)
                '取出IV值
                fsKey.Read(byteTdesIV, 0, intIVLength)
                fsKey.Close()
                '將 Key值寫入編碼元件中
                cryTdes.Key = byteTdesKey
                '將 IV值寫入編碼元件中
                cryTdes.IV = byteTdesIV

                funReadKeyFile = True
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "<Function>檔案編碼 Function (gfunEncryptFile)"
        Public Function gfunEncryptFile(ByVal strFileName As String, ByVal strInputString As String, ByRef strMessage As String) As Boolean Implements ISecurity.gfunEncryptFile
            '宣告壓密資料流
            Dim encStream As CryptoStream
            '宣告壓密檔案資料流
            Dim fsEncFile As FileStream
            '宣告Byte陣列為欲壓密字串的暫存用
            Dim byteEncTemp() As Byte

            strMessage = ""
            Try
                '產生Key
                funWriteKeyFile(UCase(strFileName))
                '建立壓密檔
                fsEncFile = New FileStream(UCase(strFileName), FileMode.Append, FileAccess.Write, FileShare.Write)
                '將加密字串傳換成Byte陣列
                byteEncTemp = encObj.GetBytes(strInputString)
                '建立壓密資料流
                encStream = New CryptoStream(fsEncFile, cryTdes.CreateEncryptor(byteTdesKey, byteTdesIV), CryptoStreamMode.Write)
                '將壓密Byte陣列寫入壓密資料流
                encStream.Write(byteEncTemp, 0, byteEncTemp.Length)
                encStream.Close()
                fsEncFile.Close()
                Return True
            Catch ex As Exception
                strMessage = "(gfunEncryptFile)" & ex.Message.ToString()
                Return False
            End Try
        End Function

        Public Function gfunEncryptFile(ByVal strFileName As String, ByVal msInputMemoryStream As System.IO.MemoryStream, ByRef strMessage As String) As Boolean Implements ISecurity.gfunEncryptFile
            '宣告壓密資料流
            Dim encStream As CryptoStream
            '宣告壓密檔案資料流
            Dim fsEncFile As FileStream
            '宣告Byte陣列為欲壓密字串的暫存用
            Dim byteEncTemp() As Byte

            strMessage = ""
            Try
                '產生Key
                funWriteKeyFile(UCase(strFileName))
                '建立壓密檔
                fsEncFile = New FileStream(UCase(strFileName), FileMode.Append, FileAccess.Write, FileShare.Write)
                '將加密字串傳換成Byte陣列
                byteEncTemp = msInputMemoryStream.ToArray() 'encObj.GetBytes(strInputString)
                '建立壓密資料流
                encStream = New CryptoStream(fsEncFile, cryTdes.CreateEncryptor(byteTdesKey, byteTdesIV), CryptoStreamMode.Write)
                '將壓密Byte陣列寫入壓密資料流
                encStream.Write(byteEncTemp, 0, byteEncTemp.Length)
                encStream.Close()
                fsEncFile.Close()
                Return True
            Catch ex As Exception
                strMessage = "(gfunEncryptFile)" & ex.Message.ToString()
                Return False
            End Try
        End Function
#End Region

#Region "<Function>檔案解碼 Function (gfunDecryptFile)"
        Public Function gfunDecryptFile(ByVal strFileName As String, ByRef strOutputString As String, ByRef strMessage As String) As Boolean Implements ISecurity.gfunDecryptFile
            '宣告壓密資料流
            Dim decStream As CryptoStream
            '宣告壓密檔案資料流
            Dim fsDecFile As FileStream
            '宣告記憶體暫存案資料流
            Dim msTemp As New MemoryStream()
            '宣告Byte陣列為欲壓密字串的暫存用
            Dim byteEncTemp() As Byte
            '宣告Byte陣列為檔案Buffer
            Dim byteBuff() As Byte

            strMessage = ""
            Try
                '讀取Key
                funReadKeyFile(UCase(strFileName))
                '讀取壓密檔
                fsDecFile = New FileStream(UCase(strFileName), FileMode.Open, FileAccess.Read, FileShare.Read)
                fsDecFile.Position = intReserverLength
                '建立壓密資料流
                decStream = New CryptoStream(msTemp, cryTdes.CreateDecryptor(byteTdesKey, byteTdesIV), CryptoStreamMode.Write)
                '重定義檔案Buffer大小
                ReDim byteBuff(CInt(fsDecFile.Length - (intReserverLength + 1)))
                '讀取壓密檔內容
                fsDecFile.Read(byteBuff, 0, CInt(fsDecFile.Length - intReserverLength))
                '將壓密Byte陣列寫入壓密資料流
                decStream.Write(byteBuff, 0, CInt(fsDecFile.Length - intReserverLength))
                decStream.Close()
                fsDecFile.Close()
                '將資料流轉成Byte陣列
                byteEncTemp = msTemp.ToArray()
                '將解密後字串寫回輸出字串
                strOutputString = encObj.GetString(byteEncTemp)
                Return True
            Catch ex As Exception
                strMessage = "(gfunDecryptFile)" & ex.Message.ToString()
                Return False
            End Try
        End Function

        Public Function gfunDecryptFile(ByVal strFileName As String, ByRef msOutputMemoryStream As System.IO.MemoryStream, ByRef strMessage As String) As Boolean Implements ISecurity.gfunDecryptFile
            '宣告壓密資料流
            Dim decStream As CryptoStream
            '宣告壓密檔案資料流
            Dim fsDecFile As FileStream
            '宣告記憶體暫存案資料流
            Dim msTemp As New MemoryStream()
            '宣告Byte陣列為欲壓密字串的暫存用
            'Dim byteEncTemp() As Byte
            '宣告Byte陣列為檔案Buffer
            Dim byteBuff() As Byte

            strMessage = ""
            Try
                '讀取Key
                funReadKeyFile(UCase(strFileName))
                '讀取壓密檔
                fsDecFile = New FileStream(UCase(strFileName), FileMode.Open, FileAccess.Read, FileShare.Read)
                fsDecFile.Position = intReserverLength
                '建立壓密資料流
                decStream = New CryptoStream(msTemp, cryTdes.CreateDecryptor(byteTdesKey, byteTdesIV), CryptoStreamMode.Write)
                '重定義檔案Buffer大小
                ReDim byteBuff(CInt(fsDecFile.Length - (intReserverLength + 1)))
                '讀取壓密檔內容
                fsDecFile.Read(byteBuff, 0, CInt(fsDecFile.Length - intReserverLength))
                '將壓密Byte陣列寫入壓密資料流
                decStream.Write(byteBuff, 0, CInt(fsDecFile.Length - intReserverLength))
                decStream.Close()
                fsDecFile.Close()
                '將資料流轉成Byte陣列
                msOutputMemoryStream = New MemoryStream(msTemp.ToArray)
                'msTemp.WriteTo(msOutputMemoryStream)
                'byteEncTemp = msOutputMemoryStream.ToArray()
                '將解密後字串寫回輸出字串
                'Dim strOutputString As String = encObj.GetString(byteEncTemp)
                Return True
            Catch ex As Exception
                strMessage = "(gfunDecryptFile)" & ex.Message.ToString()
                Return False
            End Try
        End Function
#End Region

#Region "<Function>字串編碼(單向) Function (gfunEncryptString)"
        Public Function gfunEncryptString(ByVal strInputString As String, ByRef strOutputString As String, ByRef strMessage As String) As Boolean Implements ISecurity.gfunEncryptString
            '宣告Byte陣列為欲壓密字串的暫存用
            Dim byteEncTemp() As Byte
            '宣告Byte陣列為字串的暫存用
            Dim byteOutputTemp() As Byte

            strMessage = ""
            Try
                '將輸入字串轉換成Byte陣列
                byteEncTemp = encObj.GetBytes(strInputString)
                'MD5編碼
                byteOutputTemp = cryEnString.ComputeHash(byteEncTemp)
                strOutputString = encObj.GetString(byteOutputTemp)
                Return True
            Catch ex As Exception
                strMessage = "(gfunEncryptFile)" & ex.ToString
                Return False
            End Try
        End Function
#End Region

#Region "<Function>字串編碼(雙向) Function (gfunEncryptString2W)"
        Public Function gfunEncryptString2W(ByVal strKey As String, ByVal strInputString As String, ByRef strOutputString As String, ByRef strMessage As String) As Boolean Implements ISecurity.gfunEncryptString2W
            '宣告Byte陣列為欲壓密字串的暫存用
            Dim byteEncTemp() As Byte
            '宣告Byte陣列為字串的暫存用
            Dim byteOutputTemp() As Byte
            '宣告記憶體暫存案資料流
            Dim msTemp As New MemoryStream()
            '宣告壓密資料流
            Dim encStream As CryptoStream
            Dim strTmp As String

            strMessage = ""
            strOutputString = ""
            Try
                '將輸入字串轉換成Byte陣列
                byteEncTemp = encObj.GetBytes(strKey & strInputString)
                '建立壓密資料流
                encStream = New CryptoStream(msTemp, cryEn2W.CreateEncryptor(byte2WKey, byte2WIV), CryptoStreamMode.Write)
                '將壓密Byte陣列寫入壓密資料流
                encStream.Write(byteEncTemp, 0, byteEncTemp.Length)
                encStream.Close()
                '將資料流轉成Byte陣列
                byteOutputTemp = msTemp.ToArray()
                For intLoop As Integer = 0 To byteOutputTemp.GetUpperBound(0)
                    strTmp = Hex(byteOutputTemp(intLoop))
                    If strTmp.Length = 1 Then strTmp = "0" & strTmp
                    strOutputString &= strTmp
                Next

                '將解密後字串寫回輸出字串
                'strOutputString = encObj.GetString(byteOutputTemp)
                Return True
            Catch ex As Exception
                strMessage = "(gfunEncryptString2W)" & ex.ToString
                Return False
            End Try
        End Function
#End Region

#Region "<Function>字串解碼(雙向) Function (gfunDecryptString2W)"
        Public Function gfunDecryptString2W(ByVal strKey As String, ByVal strInputString As String, ByRef strOutputString As String, ByRef strMessage As String) As Boolean Implements ISecurity.gfunDecryptString2W
            '宣告Byte陣列為欲壓密字串的暫存用
            Dim byteEncTemp() As Byte
            '宣告Byte陣列為字串的暫存用
            Dim byteOutputTemp() As Byte
            '宣告記憶體暫存案資料流
            Dim msTemp As New MemoryStream()
            '宣告壓密資料流
            Dim encStream As CryptoStream
            Dim strTemp As String

            strMessage = ""
            strOutputString = ""
            Try
                If Len(strInputString) Mod 2 <> 0 Then
                    Throw New Exception("gfunDecryptString2W:This string is not an encrypted string")
                End If

                ReDim byteEncTemp(Len(strInputString) \ 2 - 1)
                Dim strTmp As String = ""
                For intLoop As Integer = 0 To byteEncTemp.GetUpperBound(0)
                    byteEncTemp(intLoop) = CInt("&H" & strInputString.Substring(intLoop * 2, 2))
                Next

                '將輸入字串轉換成Byte陣列
                'byteEncTemp = encObj.GetBytes(strInputString)
                '建立壓密資料流
                encStream = New CryptoStream(msTemp, cryEn2W.CreateDecryptor(byte2WKey, byte2WIV), CryptoStreamMode.Write)
                '將壓密Byte陣列寫入壓密資料流
                encStream.Write(byteEncTemp, 0, byteEncTemp.Length)
                encStream.Close()
                '將資料流轉成Byte陣列
                byteOutputTemp = msTemp.ToArray()
                '將解密後字串寫回輸出字串
                strTemp = encObj.GetString(byteOutputTemp)
                If (strKey = strTemp.Substring(0, Len(strKey))) Then
                    strOutputString = strTemp.Substring(Len(strKey))
                    Return True
                Else
                    strMessage = "(gfunDecryptString2W)" & "傳入Key值錯誤"
                    Return False
                End If
            Catch ex As Exception
                strMessage = "(gfunDecryptString2W)" & ex.ToString
                Return False
            End Try
        End Function
#End Region

    End Class
#End Region

End Namespace
#End Region