'****************************************************
'功能說明：其他代碼設定-修改
'建立人員：Jason Lu
'建立日期：2016.12.27
'修改日期：2017.02.15
'****************************************************

Imports System.Data
Imports System.Data.Common

Partial Class OV_OV5002
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            Dim objOV As New OV5

            '代碼類別
            OV5.FillFldName_OV5000(ddlTabFldName)
            ddlTabFldName.Items.Insert(0, New ListItem("---請選擇---", ""))

            '排序順序,預設值為0,數值0~99
            objOV.GetSortFld(ddlSortFld)
            ddlSortFld.SelectedIndex = 0
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

            If ht.ContainsKey("DoUpdate") Then
                If ht("DoUpdate").ToString() = "Y" Then
                    '是修改
                    ViewState.Item("DoUpdate") = "Y"
                ElseIf ht("DoUpdate").ToString() = "N" Then
                    '是明細
                    ViewState.Item("DoUpdate") = "N"
                    ddlTabFldName.Enabled = False
                    ddlSortFld.Enabled = False
                    txtSortFld.Enabled = False
                    txtCode.Enabled = False
                    txtCodeCName.Enabled = False
                    chkNotShowingFlag.Enabled = False
                Else
                    Bsp.Utility.ShowMessage(Me, "ViewState Item Transport Err")
                End If
            End If
        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnUpdate"   '存檔返回
                If ViewState.Item("DoUpdate") = "Y" Then
                    If funCheckData() Then
                        If SaveData() Then
                            'Bsp.Utility.ShowMessage(Me, "代碼修改成功")
                            GoBack()
                        Else
                            'Bsp.Utility.ShowMessage(Me, "代碼修改失敗")
                        End If
                    End If
                End If
            Case "btnActionX"   '返回
                GoBack()
            Case "btnCancel"    '清除
                If ViewState.Item("DoUpdate") = "Y" Then ClearData()
        End Select
    End Sub

    Private Function funCheckData() As Boolean
        Dim beManageCodeSet As New beManageCodeSet.Row()
        Dim bsManageCodeSet As New beManageCodeSet.Service()

        beManageCodeSet.Code.Value = txtCode.Text.Trim
        beManageCodeSet.CodeCName.Value = txtCodeCName.Text.Trim

        '代碼類別
        If ddlTabFldName.SelectedValue = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblFldName.Text)
            ddlTabFldName.Focus()
            Return False
        End If

        '2017/02/15-Kat說完全比照PA51XX系列，那就全比照囉XD
        '檢查此代碼是否修改過
        'If (hidNotShowingFlag.Value = "0" And chkNotShowingFlag.Checked = False) Or _
        '    (hidNotShowingFlag.Value = "1" And chkNotShowingFlag.Checked = True) Then
        '    'If ddlSortFld.SelectedValue.ToString = hidSortFld.Value() Then
        '    If txtSortFld.Text.Trim = hidSortFld.Value Then
        '        If txtCodeCName.Text = hidCodeCName.Value.ToString() Then
        '            Bsp.Utility.ShowMessage(Me, "您尚未修改代碼設定")
        '            Return False
        '        End If
        '    End If
        'End If

        '代碼
        If txtCode.Text = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblCode.Text)
            txtCode.Focus()
            Return False
        ElseIf Bsp.Utility.getStringLength(txtCode.Text.Trim) > txtCode.MaxLength Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00040", lblCode.Text, txtCode.MaxLength.ToString)
            txtCode.Focus()
            Return False
        End If

        '2017/02/15-Kat說完全比照PA51XX系列，那就全比照囉XD
        'If bsManageCodeSet.IsCodeCNameExists(beManageCodeSet) Then
        '    Bsp.Utility.ShowMessage(Me, "已經有相同的代碼名稱，請輸入新的代碼名稱")
        '    Return False
        If txtCodeCName.Text.Trim = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblCodeCName.Text)
            txtCodeCName.Focus()
            Return False
        End If

        '排序順序
        If txtSortFld.Text.Trim = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblSortFld.Text)
            txtSortFld.Focus()
            Return False
        ElseIf IsNumeric(txtSortFld.Text.Trim) = False Then
            Bsp.Utility.ShowMessage(Me, "｢排序順序｣請輸入0~255之數字")
            txtSortFld.Focus()
            Return False
        ElseIf CInt(txtSortFld.Text.Trim) < 0 Or CInt(txtSortFld.Text.Trim) > 255 Then
            Bsp.Utility.ShowMessage(Me, "｢排序順序｣請輸入0~255之數字")
            txtSortFld.Focus()
            Return False
        End If

        '2017/02/15-Kat說完全比照PA51XX系列，那就全比照囉XD
        '檢查資料是否存在
        If beManageCodeSet.TabName.Value <> hidTabName.Value Or beManageCodeSet.FldName.Value <> hidFldName.Value Or beManageCodeSet.Code.Value <> hidCode.Value Then
            '檢查資料是否存在
            If bsManageCodeSet.IsDataExists(beManageCodeSet) Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00010", "")
                Return False
            End If
        End If

        Return True
    End Function

    Private Sub ClearData()
        '代碼類別
        ddlTabFldName.SelectedIndex = ddlTabFldName.Items.IndexOf(ddlTabFldName.Items.FindByText((hidTabName.Value.ToString() + "\" + hidFldName.ToString())))

        '代碼
        txtCode.Text = hidCode.Value.ToString()

        '代碼名稱
        txtCodeCName.Text = hidCodeCName.Value

        '排列順序
        ddlSortFld.SelectedIndex = ddlSortFld.Items.IndexOf(ddlSortFld.Items.FindByText(hidSortFld.Value))
        txtSortFld.Text = hidSortFld.Value.ToString()

        If hidNotShowingFlag.Value = "0" Then
            chkNotShowingFlag.Checked = False
        Else
            chkNotShowingFlag.Checked = True
        End If
    End Sub

    Private Sub GoBack()
        Dim ti As TransferInfo = Me.StateTransfer
        Me.TransferFramePage(ti.CallerUrl, Nothing, ti.Args)
    End Sub

    Private Sub subGetData(ByVal Code As String, ByVal TabName As String, ByVal FldName As String)
        Dim objSC As New SC
        Dim objOV As New OV5
        Dim beManageCodeSet As New beManageCodeSet.Row()
        Dim bsManageCodeSet As New beManageCodeSet.Service()

        beManageCodeSet.TabName.Value = TabName
        beManageCodeSet.FldName.Value = FldName
        beManageCodeSet.Code.Value = Code

        Try
            Using dt As DataTable = bsManageCodeSet.QueryByKey(beManageCodeSet).Tables(0)
                If dt.Rows.Count <= 0 Then
                    If dt.Rows.Count <= 0 Then Exit Sub
                    'txtCodeCName.Text = "查無此筆資料"
                    'ddlTabFldName.Enabled = False
                    'ddlSortFld.Enabled = False
                    'txtSortFld.Enabled = False
                    'txtCode.Enabled = False
                    'txtCodeCName.Enabled = False
                    'chkNotShowingFlag.Enabled = False
                    'Exit Sub
                End If

                beManageCodeSet = New beManageCodeSet.Row(dt.Rows(0))

                '隱藏欄位
                hidTabName.Value = beManageCodeSet.TabName.Value
                hidFldName.Value = beManageCodeSet.FldName.Value
                hidSortFld.Value = beManageCodeSet.SortFld.Value
                hidNotShowingFlag.Value = beManageCodeSet.NotShowFlag.Value
                hidCodeCName.Value = If(beManageCodeSet.CodeCName.Value, "")
                hidCode.Value = If(beManageCodeSet.Code.Value.ToString(), "-1")

                '代碼類別
                ddlTabFldName.SelectedIndex = ddlTabFldName.Items.IndexOf(ddlTabFldName.Items.FindByValue((hidTabName.Value.ToString() + "\" + hidFldName.Value.ToString())))

                '代碼
                If beManageCodeSet.Code.Value.Trim <> "" Then
                    txtCode.Text = beManageCodeSet.Code.Value.ToString
                Else
                    txtCode.Text = ""
                End If

                '代碼名稱
                If beManageCodeSet.CodeCName.Value.Trim <> "" Then
                    txtCodeCName.Text = beManageCodeSet.CodeCName.Value.ToString
                Else
                    txtCodeCName.Text = ""
                End If

                '排列順序
                ddlSortFld.SelectedIndex = ddlSortFld.Items.IndexOf(ddlSortFld.Items.FindByText(hidSortFld.Value))
                txtSortFld.Text = beManageCodeSet.SortFld.Value.ToString()

                '不顯示註記
                If beManageCodeSet.NotShowFlag.Value = "0" Then
                    chkNotShowingFlag.Checked = False
                Else
                    chkNotShowingFlag.Checked = True
                End If

                '最後異動公司
                If beManageCodeSet.LastChgComp.Value.Trim <> "" Then
                    Dim CompName As String = objSC.GetSC_CompName(beManageCodeSet.LastChgComp.Value)
                    lblLastChgComp.Text = beManageCodeSet.LastChgComp.Value + IIf(CompName <> "", "-" + CompName, "")
                Else
                    lblLastChgComp.Text = ""
                End If

                '最後異動人員
                If beManageCodeSet.LastChgID.Value.Trim <> "" Then
                    Using rtnTable As DataTable = objOV.GetEmpName(beManageCodeSet.LastChgComp.Value, beManageCodeSet.LastChgID.Value)
                        If rtnTable.Rows.Count <= 0 Then
                            lblLastChgID.Text = beManageCodeSet.LastChgID.Value
                        Else
                            lblLastChgID.Text = beManageCodeSet.LastChgID.Value + IIf(rtnTable.Rows(0).Item(0).ToString() <> "", "-" + rtnTable.Rows(0).Item(0).ToString(), "")
                        End If

                    End Using
                Else
                    lblLastChgID.Text = ""
                End If

                '最後異動日期
                lblLastChgDate.Text = IIf(Format(beManageCodeSet.LastChgDate.Value, "yyyy/MM/dd") = "1900/01/01", "", beManageCodeSet.LastChgDate.Value.ToString("yyyy/MM/dd HH:mm:ss"))

            End Using
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & "subGetData", ex)
        End Try
    End Sub

    Private Function SaveData() As Boolean
        Dim beManageCodeSet As New beManageCodeSet.Row()
        Dim bsManageCodeSet As New beManageCodeSet.Service()

        '取得輸入資料
        Dim arrFldName(1) As String
        arrFldName = ddlTabFldName.SelectedValue.Split("\")
        beManageCodeSet.TabName.Value = arrFldName(0)
        beManageCodeSet.FldName.Value = arrFldName(1)
        beManageCodeSet.Code.Value = txtCode.Text.Trim
        beManageCodeSet.CodeCName.Value = txtCodeCName.Text.Trim
        beManageCodeSet.SortFld.Value = ddlSortFld.SelectedValue.ToString()
        beManageCodeSet.SortFld.Value = txtSortFld.Text.Trim.ToString()
        beManageCodeSet.LastChgComp.Value = UserProfile.ActCompID
        beManageCodeSet.LastChgID.Value = UserProfile.ActUserID
        beManageCodeSet.LastChgDate.Value = Now

        '檢查不顯示註記是否有勾選 0=顯示,１=不顯示
        If chkNotShowingFlag.Checked = True Then
            beManageCodeSet.NotShowFlag.Value = "1"
        Else
            beManageCodeSet.NotShowFlag.Value = "0"
        End If

        '儲存資料
        Try
            Return UpdateOTTypeSetting(beManageCodeSet, hidCode.Value)
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, "[CC0201_99]" & Bsp.Utility.getMessage("E_00000") & Bsp.Utility.getInnerException(Me.FunID, ex))
            Return False
        End Try
    End Function

    Public Function UpdateOTTypeSetting(ByVal beManageCodeSet As beManageCodeSet.Row, ByVal Code As String) As Boolean
        Dim bsManageCodeSet As New beManageCodeSet.Service()

        IIf(IsDateTimeNull(beManageCodeSet.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), beManageCodeSet.LastChgDate.Value)

        Using cn As DbConnection = Bsp.DB.getConnection("AattendantDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsManageCodeSet.Update(beManageCodeSet, tran) = 0 Then Return False
                tran.Commit()
            Catch ex As Exception
                tran.Rollback()
                Throw
            Finally
                If tran IsNot Nothing Then tran.Dispose()
            End Try
        End Using
        Return True
    End Function

    Private Function IsDateTimeNull(ByVal Src As DateTime) As Boolean
        If Src = Convert.ToDateTime("1900/1/1") OrElse _
           Src = Convert.ToDateTime("0001/1/1") Then
            Return True
        Else
            Return False
        End If
    End Function

End Class
