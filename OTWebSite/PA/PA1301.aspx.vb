'****************************************************
'功能說明：職等代碼設定-新增
'建立人員：MickySung
'建立日期：2015.04.27
'****************************************************
Imports System.Data

Partial Class PA_PA1301
    Inherits PageBase


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            Dim objSC As New SC
            '2015/05/28 公司代碼-名稱改寫法
            lblCompID2.Text = UserProfile.SelectCompRoleName
            'lblCompID2.Text = UserProfile.SelectCompRoleID + "-" + objSC.GetCompName(UserProfile.SelectCompRoleID).Rows(0).Item("CompName").ToString
        End If
    End Sub
    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then

        End If
    End Sub
    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnAdd"   '存檔返回
                If funCheckData() Then
                    If SaveData() Then
                        GoBack()
                    End If
                End If
            Case "btnActionX"   '返回
                GoBack()
            Case "btnCancel"    '清除
                ClearData()
        End Select
    End Sub

    Private Sub GoBack()
        Dim ti As TransferInfo = Me.StateTransfer
        Me.TransferFramePage(ti.CallerUrl, Nothing, ti.Args)
    End Sub

    Private Function SaveData() As Boolean
        Dim beRank As New beRank.Row()
        Dim bsRank As New beRank.Service()
        Dim objPA As New PA1()

        '取得輸入資料
        beRank.CompID.Value = UserProfile.SelectCompRoleID
        beRank.RankID.Value = txtRankID.Text

        If txtFixAmount.Text.Trim = "" Then
            beRank.FixAmount.Value = "0"
        Else
            '2015/08/06 規格變更:DB型態改成Decimal(10,0)
            beRank.FixAmount.Value = txtFixAmount.Text
        End If

        If txtYearHolidays.Text.Trim = "" Then
            beRank.YearHolidays.Value = "0.0"
        Else
            beRank.YearHolidays.Value = txtYearHolidays.Text
        End If

        beRank.GrpLvl.Value = txtGrpLvl.Text

        beRank.LastChgComp.Value = UserProfile.ActCompID
        beRank.LastChgID.Value = UserProfile.ActUserID
        beRank.LastChgDate.Value = Now

        '檢查資料是否存在
        If bsRank.IsDataExists(beRank) Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00010", "")
            Return False
        End If

        '儲存資料
        Try
            Return objPA.AddRankSetting(beRank, chkHoldingRankID.Checked, chkRankIDMap.Checked)
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, "[CC0201_99]" & Bsp.Utility.getMessage("E_00000") & Bsp.Utility.getInnerException(Me.FunID, ex))
            Return False
        End Try
    End Function

    Private Function funCheckData() As Boolean
        Dim objPA As New PA1()
        Dim strWhere As String = ""
        Dim beRank As New beRank.Row()
        Dim bsRank As New beRank.Service()
        Dim str(1) As String

        '職等代碼        
        If txtRankID.Text.Trim = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblRankID.Text)
            txtRankID.Focus()
            Return False
        End If
        If Bsp.Utility.getStringLength(txtRankID.Text.Trim) > txtRankID.MaxLength Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00040", lblRankID.Text, txtRankID.MaxLength.ToString)
            Return False
        End If

        '固定入帳金額
        If txtFixAmount.Text.Trim <> "" Then
            If IsNumeric(txtFixAmount.Text.Trim) = False Then
                Bsp.Utility.ShowMessage(Me, "欄位[固定入帳金額]請輸入數字")
                txtFixAmount.Focus()
                Return False
            Else
                '2015/07/30 mark
                'If txtFixAmount.Text.Trim < -2147483648 Or txtFixAmount.Text.Trim > 2147483647 Then
                '    Bsp.Utility.ShowMessage(Me, "欄位[固定入帳金額]超過最大長度")
                '    txtFixAmount.Focus()
                '    Return False
                'End If

                '2015/07/30 add 長度檢核
                If Bsp.Utility.getStringLength(txtFixAmount.Text.Trim) > txtFixAmount.MaxLength Then
                    Bsp.Utility.ShowFormatMessage(Me, "W_00040", lblFixAmount.Text, txtFixAmount.MaxLength.ToString)
                    Return False
                End If
            End If
        End If

        '年休假天數
        If txtYearHolidays.Text.Trim <> "" Then
            If IsNumeric(txtYearHolidays.Text.Trim) = False Then
                Bsp.Utility.ShowMessage(Me, "欄位[年休假天數]請輸入數字")
                txtYearHolidays.Focus()
                Return False
            Else
                If txtYearHolidays.Text.Trim.Contains(".") = True Then
                    str = txtYearHolidays.Text.Trim.Split(".")
                    If str(1).Length > 1 Then
                        Bsp.Utility.ShowMessage(Me, "欄位[年休假天數]只可輸入一位小數")
                        Return False
                    End If
                End If

                '2015/07/30 modify 改為不可超過30
                If txtYearHolidays.Text.Trim > 30 Then
                    Bsp.Utility.ShowMessage(Me, "欄位[年休假天數]不可大於30")
                    Return False
                End If
            End If
        End If

        '團保級數
        If txtGrpLvl.Text.Trim = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblGrpLvl.Text)
            txtGrpLvl.Focus()
            Return False
        End If
        If Bsp.Utility.getStringLength(txtGrpLvl.Text.Trim) > txtGrpLvl.MaxLength Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00040", lblGrpLvl.Text, txtGrpLvl.MaxLength.ToString)
            Return False
        End If

        Return True
    End Function

    Private Sub ClearData()
        '職等代碼
        txtRankID.Text = ""

        '固定入帳金額
        txtFixAmount.Text = ""

        '年休假天數
        txtYearHolidays.Text = ""

        '團保級數
        txtGrpLvl.Text = ""

        '連動寫入金控職等
        chkHoldingRankID.Checked = False

        '連動寫入職等比對
        chkRankIDMap.Checked = False
    End Sub

End Class
