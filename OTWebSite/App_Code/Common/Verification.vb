'**************************************************************
'功能說明：ID邏輯檢查
'建立人員：Chung
'建立日期：2013.06.18
'**************************************************************
Imports Microsoft.VisualBasic

Namespace Bsp
    Public Class Verification
        ''' <summary>
        ''' 檢查個人身分證編號邏輯(大陸)
        ''' </summary>
        ''' <param name="ID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function CheckPRCID(ByVal ID As String) As Boolean
            '改為大寫
            ID = ID.ToUpper().Trim()

            '舊式身分證號不檢核
            If ID.Length = 15 Then Return True
            '字串Format檢查
            If Not Regex.IsMatch(ID, "^\d{17}[0-9|X]$") Then Return False
            '檢查生日是否合法
            If Bsp.Utility.CheckDate(ID.Substring(6, 8)) = "" Then Return False
            '計算係數
            Dim Key() As Integer = {7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2}
            Dim Value As Integer = 0
            Dim VerifiCode() As String = {"1", "0", "X", "9", "8", "7", "6", "5", "4", "3", "2"}
            Dim ModValue As Integer = 0

            For i As Integer = 0 To 16
                Value += Convert.ToInt32(ID.Substring(i, 1)) * Key(i)
            Next

            ModValue = Value Mod 11

            If ID.Substring(17, 1) = VerifiCode(ModValue) Then
                Return True
            Else
                Return False
            End If
        End Function

        ''' <summary>
        ''' 织机构代码编制规则檢查
        ''' </summary>
        ''' <param name="ID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function CheckORGID(ByVal ID As String) As Boolean
            '改為大寫
            ID = ID.ToUpper().Trim()

            If Not Regex.IsMatch(ID, "^[0-9|A-Z]{8}[0-9|X]$") Then Return False

            Dim W() As Integer = {3, 7, 9, 10, 5, 8, 4, 2}
            Dim Value As Integer = 0
            Dim ModValue As Integer = 0
            Dim VerifiCode As String = ""

            For i As Integer = 0 To 7
                Dim AscNum As Integer = Asc(ID.Substring(i, 1))

                If AscNum <= 57 Then
                    Value += (AscNum - 48) * W(i)   '表示為1-9
                Else
                    Value += (AscNum - 55) * W(i)   '表示為A-Z
                End If
            Next
            ModValue = 11 - (Value Mod 11)

            Select Case ModValue
                Case 10
                    VerifiCode = "X"
                Case 11
                    VerifiCode = "0"
                Case Else
                    VerifiCode = ModValue.ToString()
            End Select

            If ID.Substring(8, 1) = VerifiCode Then
                Return True
            Else
                Return False
            End If
        End Function

        ''' <summary>
        ''' 檢查貸款卡編號邏輯
        ''' </summary>
        ''' <param name="LCNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function CheckLoanCardNo(ByVal LCNo As String) As Boolean
            LCNo = LCNo.ToUpper().Trim()

            If Not Regex.IsMatch(LCNo, "^[0-9|A-Z]{3}[0-9]{13}") Then Return False

            Dim W() As Integer = {1, 3, 5, 7, 11, 2, 13, 1, 1, 17, 19, 97, 23, 29}
            Dim Value As Integer = 0
            Dim ModValue As Integer = 0
            Dim VerifiCode As String = ""

            For i As Integer = 0 To 13
                Dim AscNum As Integer = Asc(LCNo.Substring(i, 1))

                If AscNum <= 57 Then
                    Value += (AscNum - 48) * W(i)   '表示為1-9
                Else
                    Value += (AscNum - 55) * W(i)   '表示為A-Z
                End If
            Next
            ModValue = (Value Mod 97) + 1

            If ModValue < 10 Then
                VerifiCode = "0" & ModValue.ToString()
            Else
                VerifiCode = ModValue.ToString()
            End If

            If LCNo.Substring(14, 2) = VerifiCode Then
                Return True
            Else
                Return False
            End If
        End Function

        ''' <summary>
        ''' 檢查台灣身分證號邏輯
        ''' </summary>
        ''' <param name="ID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function CheckTWPID(ByVal ID As String) As Boolean
            If ID.Length <> 10 Then Return False
            If ID.Substring(1, 1) <> "1" AndAlso ID.Substring(1, 1) <> "2" Then Return False
            If Not IsNumeric(ID.Substring(1, 9)) Then Return False
            ID = ID.ToUpper()

            Dim sCode As String = "ABCDEFGHJKLMNPQRSTUVXYWZIO"
            Dim c As Integer
            Dim n As Integer

            c = sCode.IndexOf(ID.Substring(0, 1))
            If c < 0 Then Return False
            n = CInt(c \ 10) + (c Mod 10) * 9 + 1
            For intLoop As Integer = 1 To 8
                n += CInt(ID.Substring(intLoop, 1)) * (9 - intLoop)
            Next
            n = (10 - (n Mod 10)) Mod 10
            If n <> CInt(ID.Substring(9, 1)) Then Return False

            Return True
        End Function
    End Class
End Namespace
