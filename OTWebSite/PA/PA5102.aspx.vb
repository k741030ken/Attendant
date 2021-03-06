'****************************************************
'功能說明：其他代碼設定-新增
'建立人員：MickySung
'建立日期：2015.05.25
'****************************************************
Imports System.Data

Partial Class PA_PA5101
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            '代碼類別
            PA5.FillFldName_PA5100(ddlFldName)
            ddlFldName.Items.Insert(0, New ListItem("---請選擇---", ""))
        End If
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

            If ht.ContainsKey("SelectedCode") Then
                ViewState.Item("Code") = ht("SelectedCode").ToString()
                subGetData(ht("SelectedCode").ToString(), ht("SelectedTabName").ToString(), ht("SelectedFldName").ToString())
            Else
                Return
            End If
        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnUpdate"   '存檔返回
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
        Dim beHRCodeMap As New beHRCodeMap.Row()
        Dim bsHRCodeMap As New beHRCodeMap.Service()
        Dim objPA As New PA5()

        '代碼類別
        Dim arrFldName(1) As String
        arrFldName = ddlFldName.SelectedValue.Split("\")
        beHRCodeMap.TabName.Value = arrFldName(0)
        beHRCodeMap.FldName.Value = arrFldName(1)
        '代碼
        beHRCodeMap.Code.Value = txtCode.Text
        '代碼名稱
        beHRCodeMap.CodeCName.Value = txtCodeCName.Text
        '排序順序
        beHRCodeMap.SortFld.Value = txtSortFld.Text
        '不顯示註記
        beHRCodeMap.NotShowFlag.Value = IIf(chkNotShowFlag.Checked, "1", "0")
        beHRCodeMap.LastChgComp.Value = UserProfile.ActCompID
        beHRCodeMap.LastChgID.Value = UserProfile.ActUserID
        beHRCodeMap.LastChgDate.Value = Now

        If beHRCodeMap.TabName.Value <> saveTabName.Value Or beHRCodeMap.FldName.Value <> saveFldName.Value Or beHRCodeMap.Code.Value <> saveCode.Value Then
            '檢查資料是否存在
            If bsHRCodeMap.IsDataExists(beHRCodeMap) Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00010", "")
                Return False
            End If
        End If

        '儲存資料
        Try
            Return objPA.UpdateHRCodeMapSetting(beHRCodeMap, saveTabName.Value, saveFldName.Value, saveCode.Value)
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, "[CC0201_99]" & Bsp.Utility.getMessage("E_00000") & Bsp.Utility.getInnerException(Me.FunID, ex))
            Return False
        End Try
    End Function

    Private Function funCheckData() As Boolean
        Dim objPA As New PA1()
        Dim beHRCodeMap As New beHRCodeMap.Row()
        Dim bsWorkSite As New beHRCodeMap.Service()

        '代碼類別
        If ddlFldName.SelectedValue = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblFldName.Text)
            ddlFldName.Focus()
            Return False
        End If

        '代碼
        If txtCode.Text = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblCode.Text)
            txtCode.Focus()
            Return False
        End If
        If Bsp.Utility.getStringLength(txtCode.Text.Trim) > txtCode.MaxLength Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00040", lblCode.Text, txtCode.MaxLength.ToString)
            txtCode.Focus()
            Return False
        End If

        '代碼名稱
        If txtCodeCName.Text = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblCodeCName.Text)
            txtCodeCName.Focus()
            Return False
        End If

        '排序順序
        If txtSortFld.Text = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblSortFld.Text)
            txtSortFld.Focus()
            Return False
        Else
            If IsNumeric(txtSortFld.Text.Trim) = False Then
                Bsp.Utility.ShowMessage(Me, "｢排序順序｣請輸入0~255之數字")
                txtSortFld.Focus()
                Return False
            Else
                If CInt(txtSortFld.Text.Trim) < 0 Or CInt(txtSortFld.Text.Trim) > 255 Then
                    Bsp.Utility.ShowMessage(Me, "｢排序順序｣請輸入0~255之數字")
                    txtSortFld.Focus()
                    Return False
                End If
            End If
        End If

        Return True
    End Function

    Private Sub subGetData(ByVal Code As String, ByVal TabName As String, ByVal FldName As String)
        Dim objPA As New PA5
        Dim objSC As New SC
        Dim beHRCodeMap As New beHRCodeMap.Row()
        Dim bsHRCodeMap As New beHRCodeMap.Service()

        beHRCodeMap.TabName.Value = TabName
        beHRCodeMap.FldName.Value = FldName
        beHRCodeMap.Code.Value = Code

        Try
            Using dt As DataTable = bsHRCodeMap.QueryByKey(beHRCodeMap).Tables(0)
                If dt.Rows.Count <= 0 Then Exit Sub
                beHRCodeMap = New beHRCodeMap.Row(dt.Rows(0))

                '代碼類別
                ddlFldName.SelectedValue = beHRCodeMap.TabName.Value + "\" + beHRCodeMap.FldName.Value
                '代碼
                txtCode.Text = beHRCodeMap.Code.Value
                '代碼名稱
                txtCodeCName.Text = beHRCodeMap.CodeCName.Value
                '排序順序
                txtSortFld.Text = beHRCodeMap.SortFld.Value
                '不顯示註記
                chkNotShowFlag.Checked = IIf(beHRCodeMap.NotShowFlag.Value = "1", True, False)
                '最後異動公司
                If beHRCodeMap.LastChgComp.Value.Trim <> "" Then
                    lblLastChgComp.Text = beHRCodeMap.LastChgComp.Value + "-" + objSC.GetCompName(beHRCodeMap.LastChgComp.Value).Rows(0).Item("CompName").ToString
                Else
                    lblLastChgComp.Text = ""
                End If
                '最後異動人員
                If beHRCodeMap.LastChgID.Value.Trim <> "" Then
                    Dim UserName As String = objSC.GetSC_UserName(beHRCodeMap.LastChgComp.Value, beHRCodeMap.LastChgID.Value)
                    lblLastChgID.Text = beHRCodeMap.LastChgID.Value + IIf(UserName <> "", "-" + UserName, "")
                Else
                    lblLastChgID.Text = ""
                End If
                '最後異動日期
                lblLastChgDate.Text = IIf(Format(beHRCodeMap.LastChgDate.Value, "yyyy/MM/dd") = "1900/01/01", "", beHRCodeMap.LastChgDate.Value.ToString("yyyy/MM/dd HH:mm:ss"))

                '隱藏欄位
                saveTabName.Value = beHRCodeMap.TabName.Value
                saveFldName.Value = beHRCodeMap.FldName.Value
                saveCode.Value = beHRCodeMap.Code.Value

            End Using
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & "subGetData", ex)
        End Try
    End Sub

    Private Sub ClearData()
        Dim ti As TransferInfo = Me.StateTransfer
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

        subGetData(ht("SelectedCode").ToString(), ht("SelectedTabName").ToString(), ht("SelectedFldName").ToString())
    End Sub

End Class
