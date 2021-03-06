'****************************************************
'功能說明：工作性質設定-新增
'建立人員：MickySung
'建立日期：2015.05.19
'****************************************************
Imports System.Data

Partial Class PA_PA2101
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            Dim objSC As New SC()
            '2015/05/28 公司代碼-名稱改寫法
            lbltxtCompID.Text = UserProfile.SelectCompRoleName
            'lbltxtCompID.Text = UserProfile.SelectCompRoleID + "-" + objSC.GetCompName(UserProfile.SelectCompRoleID).Rows(0).Item("CompName").ToString

            '班別類型 2016/03/30 增加欄位
            Bsp.Utility.FillDDL(ddlWTIDTypeFlag, "eHRMSDB", "HRCodeMap", "Code", "CodeCName", Bsp.Utility.DisplayType.Full, "", "and TabName = 'WorkTime' and FldName = 'WTIDTypeFlag' and NotShowFlag = '0'")
            ddlWTIDTypeFlag.Items.Insert(0, New ListItem("---請選擇---", ""))

            '班別代碼 2016/03/30 增加欄位
            Bsp.Utility.FillDDL(ddlRemark, "eHRMSDB", "HRCodeMap", "Code", "CodeCName", Bsp.Utility.DisplayType.Full, "", "and TabName = 'WorkTime' and FldName = 'Remark' and NotShowFlag = '0'")
            ddlRemark.Items.Insert(0, New ListItem("---請選擇---", ""))
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
        Dim beWorkTime As New beWorkTime.Row()
        Dim bsWorkTime As New beWorkTime.Service()
        Dim objPA As New PA2()

        '取得輸入資料
        beWorkTime.CompID.Value = UserProfile.SelectCompRoleID
        beWorkTime.WTID.Value = txtWTID.Text
        beWorkTime.WTIDTypeFlag.Value = ddlWTIDTypeFlag.SelectedValue '2016/03/30 增加欄位
        beWorkTime.Remark.Value = ddlRemark.SelectedValue '2016/03/30 增加欄位

        '2015/07/28 Modify 規格變更:時間欄位移除冒號
        beWorkTime.BeginTime.Value = txtBeginTime.Text
        beWorkTime.EndTime.Value = txtEndTime.Text
        beWorkTime.RestBeginTime.Value = txtRestBeginTime.Text
        beWorkTime.RestEndTime.Value = txtRestEndTime.Text

        beWorkTime.AcrossFlag.Value = IIf(chkAcrossFlag.Checked, "1", "0")
        beWorkTime.InValidFlag.Value = IIf(chkInValidFlag.Checked, "1", "0")
        beWorkTime.LastChgComp.Value = UserProfile.ActCompID
        beWorkTime.LastChgID.Value = UserProfile.ActUserID
        beWorkTime.LastChgDate.Value = Now

        '檢查資料是否存在
        If bsWorkTime.IsDataExists(beWorkTime) Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00010", "")
            Return False
        End If

        '儲存資料
        Try
            Return objPA.AddWorkTimeSetting(beWorkTime)
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, "[CC0201_99]" & Bsp.Utility.getMessage("E_00000") & Bsp.Utility.getInnerException(Me.FunID, ex))
            Return False
        End Try
    End Function

    Private Function funCheckData() As Boolean
        Dim objPA As New PA2()
        Dim beWorkTime As New beWorkTime.Row()
        Dim bsWorkTime As New beWorkTime.Service()
        Dim TimeFlag_BeginTime As Boolean
        Dim TimeFlag_EndTime As Boolean
        Dim TimeFlag_RestBeginTime As Boolean
        Dim TimeFlag_RestEndTime As Boolean

        '工作班別代碼
        If txtWTID.Text = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblWTID.Text)
            txtWTID.Focus()
            Return False
        End If
        If bsWorkTime.IsDataExists(beWorkTime) Then
            Bsp.Utility.ShowMessage(Me, "工作班別代碼：「" + txtWTID.Text + "」資料已存在，不可新增！")
            txtWTID.Focus()
            Return False
        End If
        If Bsp.Utility.getStringLength(txtWTID.Text.Trim) > txtWTID.MaxLength Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00040", lblWTID.Text, txtWTID.MaxLength.ToString)
            txtWTID.Focus()
            Return False
        End If

        '班別類型 2016/03/30 增加欄位
        If ddlWTIDTypeFlag.SelectedValue = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblWTIDTypeFlag.Text)
            ddlWTIDTypeFlag.Focus()
            Return False
        End If

        '班別說明 2016/03/30 增加欄位
        If ddlRemark.SelectedValue = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblRemark.Text)
            ddlRemark.Focus()
            Return False
        End If

        '上班時間
        Dim arrBeginTime(1) As String
        If txtBeginTime.Text = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblBeginTime.Text)
            txtBeginTime.Focus()
            Return False
        Else
            '2015/07/28 規格變更:時間欄位移除冒號
            'If txtBeginTime.Text.Trim.Contains(":") = True Then
            '    arrBeginTime = txtBeginTime.Text.Trim.Split(":")

            '    If IsNumeric(arrBeginTime(0)) And IsNumeric(arrBeginTime(1)) Then
            '        TimeFlag_BeginTime = True

            '        If arrBeginTime(0) < 0 Or arrBeginTime(0) > 24 Then
            '            Bsp.Utility.ShowMessage(Me, "「上班時間」請輸入正確時間")
            '            Return False
            '        ElseIf arrBeginTime(0) = 24 Then
            '            Bsp.Utility.ShowMessage(Me, "「上班時間」凌晨時段需設定為00:00，不可為24:00")
            '            Return False
            '        ElseIf arrBeginTime(0).Length <> 2 Or arrBeginTime(1).Length <> 2 Then
            '            Bsp.Utility.ShowMessage(Me, "「上班時間」請輸入正確格式")
            '            Return False
            '        End If

            '        If arrBeginTime(1) < 0 Or arrBeginTime(1) > 59 Then
            '            Bsp.Utility.ShowMessage(Me, "「上班時間」請輸入正確時間")
            '            Return False
            '        End If
            '    Else
            '        Bsp.Utility.ShowMessage(Me, "「上班時間」請輸入正確時間")
            '        Return False
            '    End If
            'Else
            '    Bsp.Utility.ShowMessage(Me, "「上班時間」請輸入正確格式")
            '    Return False
            'End If
            If txtBeginTime.Text.Trim.Length = 4 Then
                If IsNumeric(txtBeginTime.Text.Trim) Then
                    TimeFlag_BeginTime = True

                    arrBeginTime(0) = Left(txtBeginTime.Text.Trim, 2) '小時
                    arrBeginTime(1) = Right(txtBeginTime.Text.Trim, 2) '分鐘

                    If arrBeginTime(0) < 0 Or arrBeginTime(0) > 24 Then
                        Bsp.Utility.ShowMessage(Me, "「上班時間」請輸入正確時間")
                        Return False
                    ElseIf arrBeginTime(0) = 24 Then
                        Bsp.Utility.ShowMessage(Me, "「上班時間」凌晨時段需設定為0000，不可為2400")
                        Return False
                    End If

                    If arrBeginTime(1) < 0 Or arrBeginTime(1) > 59 Then
                        Bsp.Utility.ShowMessage(Me, "「上班時間」請輸入正確時間")
                        Return False
                    End If

                Else
                    Bsp.Utility.ShowMessage(Me, "「上班時間」請輸入正確時間")
                    Return False
                End If
            Else
                Bsp.Utility.ShowMessage(Me, "「上班時間」請輸入正確格式")
                Return False
            End If
        End If

        '下班時間
        Dim arrEndTime(1) As String
        If txtEndTime.Text = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblEndTime.Text)
            txtEndTime.Focus()
            Return False
        Else
            '2015/07/28 規格變更:時間欄位移除冒號
            'If txtEndTime.Text.Trim.Contains(":") = True Then
            '    arrEndTime = txtEndTime.Text.Trim.Split(":")

            '    If IsNumeric(arrEndTime(0)) And IsNumeric(arrEndTime(1)) Then
            '        TimeFlag_EndTime = True

            '        If arrEndTime(0) < 0 Or arrEndTime(0) > 24 Then
            '            Bsp.Utility.ShowMessage(Me, "「下班時間」請輸入正確時間")
            '            Return False
            '        ElseIf arrEndTime(0) = 24 Then
            '            Bsp.Utility.ShowMessage(Me, "「下班時間」凌晨時段需設定為00:00，不可為24:00")
            '            Return False
            '        ElseIf arrEndTime(0).Length <> 2 Or arrEndTime(1).Length <> 2 Then
            '            Bsp.Utility.ShowMessage(Me, "「下班時間」請輸入正確格式")
            '            Return False
            '        End If

            '        If arrEndTime(1) < 0 Or arrEndTime(1) > 59 Then
            '            Bsp.Utility.ShowMessage(Me, "「下班時間」請輸入正確時間")
            '            Return False
            '        End If
            '    Else
            '        Bsp.Utility.ShowMessage(Me, "「下班時間」請輸入正確時間")
            '        Return False
            '    End If
            'Else
            '    Bsp.Utility.ShowMessage(Me, "「下班時間」請輸入正確格式")
            '    Return False
            'End If
            If txtEndTime.Text.Trim.Length = 4 Then
                If IsNumeric(txtEndTime.Text.Trim) Then
                    TimeFlag_EndTime = True

                    arrEndTime(0) = Left(txtEndTime.Text.Trim, 2) '小時
                    arrEndTime(1) = Right(txtEndTime.Text.Trim, 2) '分鐘

                    If arrEndTime(0) < 0 Or arrEndTime(0) > 24 Then
                        Bsp.Utility.ShowMessage(Me, "「下班時間」請輸入正確時間")
                        Return False
                    ElseIf arrEndTime(0) = 24 Then
                        Bsp.Utility.ShowMessage(Me, "「下班時間」凌晨時段需設定為0000，不可為2400")
                        Return False
                    End If

                    If arrEndTime(1) < 0 Or arrEndTime(1) > 59 Then
                        Bsp.Utility.ShowMessage(Me, "「下班時間」請輸入正確時間")
                        Return False
                    End If
                Else
                    Bsp.Utility.ShowMessage(Me, "「下班時間」請輸入正確時間")
                    Return False
                End If
            Else
                Bsp.Utility.ShowMessage(Me, "「下班時間」請輸入正確格式")
                Return False
            End If
        End If

        '休息開始時間
        Dim arrRestBeginTime(1) As String
        If txtRestBeginTime.Text = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblRestBeginTime.Text)
            txtRestBeginTime.Focus()
            Return False
        Else
            '2015/07/28 規格變更:時間欄位移除冒號
            'If txtRestBeginTime.Text.Trim.Contains(":") = True Then
            '    arrRestBeginTime = txtRestBeginTime.Text.Trim.Split(":")

            '    If IsNumeric(arrRestBeginTime(0)) And IsNumeric(arrRestBeginTime(1)) Then
            '        TimeFlag_RestBeginTime = True

            '        If arrRestBeginTime(0) < 0 Or arrRestBeginTime(0) > 24 Then
            '            Bsp.Utility.ShowMessage(Me, "「休息開始時間」請輸入正確時間")
            '            Return False
            '        ElseIf arrRestBeginTime(0) = 24 Then
            '            Bsp.Utility.ShowMessage(Me, "「休息開始時間」凌晨時段需設定為00:00，不可為24:00")
            '            Return False
            '        ElseIf arrRestBeginTime(0).Length <> 2 Or arrRestBeginTime(1).Length <> 2 Then
            '            Bsp.Utility.ShowMessage(Me, "「休息開始時間」請輸入正確格式")
            '            Return False
            '        End If

            '        If arrRestBeginTime(1) < 0 Or arrRestBeginTime(1) > 59 Then
            '            Bsp.Utility.ShowMessage(Me, "「休息開始時間」請輸入正確時間")
            '            Return False
            '        End If
            '    Else
            '        Bsp.Utility.ShowMessage(Me, "「休息開始時間」請輸入正確時間")
            '        Return False
            '    End If
            'Else
            '    Bsp.Utility.ShowMessage(Me, "「休息開始時間」請輸入正確格式")
            '    Return False
            'End If
            If txtRestBeginTime.Text.Trim.Length = 4 Then
                If IsNumeric(txtRestBeginTime.Text.Trim) Then
                    TimeFlag_RestBeginTime = True

                    arrRestBeginTime(0) = Left(txtRestBeginTime.Text.Trim, 2) '小時
                    arrRestBeginTime(1) = Right(txtRestBeginTime.Text.Trim, 2) '分鐘

                    If arrRestBeginTime(0) < 0 Or arrRestBeginTime(0) > 24 Then
                        Bsp.Utility.ShowMessage(Me, "「休息開始時間」請輸入正確時間")
                        Return False
                    ElseIf arrRestBeginTime(0) = 24 Then
                        Bsp.Utility.ShowMessage(Me, "「休息開始時間」凌晨時段需設定為0000，不可為2400")
                        Return False
                    End If

                    If arrRestBeginTime(1) < 0 Or arrRestBeginTime(1) > 59 Then
                        Bsp.Utility.ShowMessage(Me, "「休息開始時間」請輸入正確時間")
                        Return False
                    End If

                Else
                    Bsp.Utility.ShowMessage(Me, "「休息開始時間」請輸入正確時間")
                    Return False
                End If
            Else
                Bsp.Utility.ShowMessage(Me, "「休息開始時間」請輸入正確格式")
                Return False
            End If
        End If

        '休息結束時間
        Dim arrRestEndTime(1) As String
        If txtRestEndTime.Text = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblRestEndTime.Text)
            txtRestEndTime.Focus()
            Return False
        Else
            '2015/07/28 規格變更:時間欄位移除冒號
            'If txtRestEndTime.Text.Trim.Contains(":") = True Then
            '    arrRestEndTime = txtRestEndTime.Text.Trim.Split(":")

            '    If IsNumeric(arrRestEndTime(0)) And IsNumeric(arrRestEndTime(1)) Then
            '        TimeFlag_RestEndTime = True

            '        If arrRestEndTime(0) < 0 Or arrRestEndTime(0) > 24 Then
            '            Bsp.Utility.ShowMessage(Me, "「休息結束時間」請輸入正確時間")
            '            Return False
            '        ElseIf arrRestEndTime(0) = 24 Then
            '            Bsp.Utility.ShowMessage(Me, "「休息結束時間」凌晨時段需設定為00:00，不可為24:00")
            '            Return False
            '        ElseIf arrRestEndTime(0).Length <> 2 Or arrRestEndTime(1).Length <> 2 Then
            '            Bsp.Utility.ShowMessage(Me, "「休息結束時間」請輸入正確格式")
            '            Return False
            '        End If

            '        If arrRestEndTime(1) < 0 Or arrRestEndTime(1) > 59 Then
            '            Bsp.Utility.ShowMessage(Me, "「休息結束時間」請輸入正確時間")
            '            Return False
            '        End If
            '    Else
            '        Bsp.Utility.ShowMessage(Me, "「休息結束時間」請輸入正確時間")
            '        Return False
            '    End If
            'Else
            '    Bsp.Utility.ShowMessage(Me, "「休息結束時間」請輸入正確格式")
            '    Return False
            'End If
            If txtRestEndTime.Text.Trim.Length = 4 Then
                If IsNumeric(txtRestEndTime.Text.Trim) Then
                    TimeFlag_RestEndTime = True

                    arrRestEndTime(0) = Left(txtRestEndTime.Text.Trim, 2) '小時
                    arrRestEndTime(1) = Right(txtRestEndTime.Text.Trim, 2) '分鐘

                    If arrRestEndTime(0) < 0 Or arrRestEndTime(0) > 24 Then
                        Bsp.Utility.ShowMessage(Me, "「休息結束時間」請輸入正確時間")
                        Return False
                    ElseIf arrRestEndTime(0) = 24 Then
                        Bsp.Utility.ShowMessage(Me, "「休息結束時間」凌晨時段需設定為0000，不可為2400")
                        Return False
                    End If

                    If arrRestEndTime(1) < 0 Or arrRestEndTime(1) > 59 Then
                        Bsp.Utility.ShowMessage(Me, "「休息結束時間」請輸入正確時間")
                        Return False
                    End If

                Else
                    Bsp.Utility.ShowMessage(Me, "「休息結束時間」請輸入正確時間")
                    Return False
                End If
            Else
                Bsp.Utility.ShowMessage(Me, "「休息結束時間」請輸入正確格式")
                Return False
            End If
        End If

        '2015/07/24 Add 新增檢核:若 跨日註記 沒有勾選 下班時間不可早於上班時間
        If chkAcrossFlag.Checked = False Then
            If TimeFlag_BeginTime = True And TimeFlag_EndTime = True Then
                If arrBeginTime(0) > arrEndTime(0) Then
                    '2015/07/30 Modify 修改錯誤訊息
                    '2015/08/05 Modify 修改錯誤訊息
                    Bsp.Utility.ShowMessage(Me, "下班時間跨凌晨時段0000，請勾選跨日註記")
                    Return False
                ElseIf arrBeginTime(0) = arrEndTime(0) Then
                    '2015/08/05 Modify 下班時間不可小於等於上班時間
                    If arrBeginTime(1) >= arrEndTime(1) Then
                        Bsp.Utility.ShowMessage(Me, "下班時間跨凌晨時段0000，請勾選跨日註記")
                        Return False
                    End If
                End If
            End If
        End If

        '2015/07/24 Add 新增檢核: 休息結束時間不可早於休息開始時間
        If TimeFlag_RestBeginTime = True And TimeFlag_RestEndTime = True Then
            If arrRestBeginTime(0) > arrRestEndTime(0) Then
                Bsp.Utility.ShowMessage(Me, "「休息結束時間」不可早於或等於「休息開始時間」")
                Return False
            ElseIf arrRestBeginTime(0) = arrRestEndTime(0) Then
                '2015/08/05 Modify 休息結束時間不可小於等於休息開始時間
                If arrRestBeginTime(1) >= arrRestEndTime(1) Then
                    Bsp.Utility.ShowMessage(Me, "「休息結束時間」不可早於或等於「休息開始時間」")
                    Return False
                End If
            End If
        End If

        Return True
    End Function

    Private Sub ClearData()
        txtWTID.Text = ""
        ddlWTIDTypeFlag.SelectedValue = "" '2016/03/30 增加欄位
        ddlRemark.SelectedValue = "" '2016/03/30 增加欄位
        txtBeginTime.Text = ""
        txtEndTime.Text = ""
        txtRestBeginTime.Text = ""
        txtRestEndTime.Text = ""
        chkAcrossFlag.Checked = False
        chkInValidFlag.Checked = False
    End Sub

End Class
