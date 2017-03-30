'****************************************************
'功能說明：功能維護-修改
'建立人員：Chung
'建立日期：2011.05.18
'****************************************************
Imports System.Data

Partial Class SC_SC0242
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            subGetParent()
        End If
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

            If ht.ContainsKey("SelectedFunID") Then
                ViewState.Item("FunID") = ht("SelectedFunID").ToString()
                subGetData(ht("SelectedFunID").ToString())
                subGetRight(ht("SelectedFunID").ToString())
            Else
                Return
            End If
        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnUpdate"
                If Not funCheckData() Then
                    subGetRight(ViewState.Item("FunID").ToString())
                    Return
                End If
                If SaveData() Then
                    GoBack()
                Else
                    subGetRight(ViewState.Item("FunID").ToString())
                End If
            Case "btnActionX"
                GoBack()
        End Select
    End Sub

    Private Sub GoBack()
        Dim ti As TransferInfo = Me.StateTransfer
        Me.TransferFramePage(ti.CallerUrl, Nothing, ti.Args)
    End Sub

    Private Sub subGetParent()
        Dim objSC As New SC

        Try
            Using dt As DataTable = objSC.GetFunInfo("", "FunID, FunID + char(9) + FunName as FunName", "And Path = '' Order by FunID")
                With ddlParentFormID
                    .DataTextField = "FunName"
                    .DataValueField = "FunID"
                    .DataSource = dt
                    .DataBind()

                    .Items.Insert(0, New ListItem("---不指定---", ""))
                End With
            End Using
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".subGetParent", ex)
        End Try

    End Sub

    Private Sub subGetData(ByVal FunID As String)
        Dim objSC As New SC
        Dim bsFun As New beSC_Fun.Service()
        Dim beFun As New beSC_Fun.Row()

        beFun.FunID.Value = FunID
        Try
            Using dt As DataTable = bsFun.QueryByKey(beFun).Tables(0)
                If dt.Rows.Count <= 0 Then Exit Sub
                beFun = New beSC_Fun.Row(dt.Rows(0))

                lblFunID.Text = beFun.FunID.Value
                txtFunName.Text = beFun.FunName.Value
                txtFunEngName.Text = beFun.FunEngName.Value
                Bsp.Utility.SetSelectedIndex(ddlParentFormID, beFun.ParentFormID.Value)
                txtOrderSeq.Text = beFun.OrderSeq.Value.ToString()
                rbnIsMenu_No.Checked = IIf(beFun.IsMenu.Value = "0", True, False)
                rbnIsMenu_Yes.Checked = IIf(beFun.IsMenu.Value = "1", True, False)
                rdoCheckRightY.Checked = IIf(beFun.CheckRight.Value = "1", True, False)
                rdoCheckRightN.Checked = IIf(beFun.CheckRight.Value = "0", True, False)
                txtPath.Text = beFun.Path.Value
                lblCreateDate.Text = beFun.CreateDate.Value.ToString("yyyy/MM/dd HH:mm:ss")
                lblLastChgID.Text = beFun.LastChgID.Value
                lblLastChgDate.Text = beFun.LastChgDate.Value.ToString("yyyy/MM/dd HH:mm:ss")
            End Using
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & "subGetData", ex)
        End Try

    End Sub

    Private Sub subGetRight(ByVal strFunID As String)
        Dim objSC As New SC
        Dim objTR As TableRow
        Dim objTD As TableCell
        Dim objCheckBox As CheckBox
        Dim objTextBox As TextBox
        Dim objLabel As Label
        Dim strDisable As String

        Try
            Using dtRights As DataTable = objSC.GetRightFun(strFunID)
                If dtRights.Rows.Count <= 0 Then Exit Sub

                For intLoop As Integer = 0 To dtRights.Rows.Count - 1
                    '建立一列新TR
                    objTR = New TableRow
                    objTR.Style.Item("Width") = "100%"

                    With dtRights.Rows(intLoop)
                        '建立checkbox
                        objTD = New TableCell
                        objTD.Style.Item("width") = "100"

                        objCheckBox = New CheckBox
                        objCheckBox.ID = "chk_" & .Item("RightID").ToString()
                        objCheckBox.Attributes.Add("onclick", "funRightClick(this,'" & .Item("RightID").ToString() & "');")
                        objCheckBox.Attributes.Add("value", .Item("RightID").ToString())
                        objCheckBox.Text = .Item("RightName").ToString()
                        objCheckBox.Attributes.Add("name", "chkRights")

                        If .Item("FunID").ToString() = "" Then
                            strDisable = "true"
                        Else
                            objCheckBox.Checked = True
                            strDisable = "false"
                        End If
                        objTD.Controls.Add(objCheckBox)
                        objTR.Cells.Add(objTD)

                        '建立標題input
                        objTD = New TableCell
                        objTD.Style.Item("width") = "160"
                        objTextBox = New TextBox()
                        objTextBox.ID = "txt_" & .Item("RightID").ToString()
                        objTextBox.Text = .Item("Caption").ToString()
                        objTextBox.CssClass = "InputTextStyle_Thin"
                        objTextBox.Style.Item("Width") = "130px"
                        objTextBox.MaxLength = 20
                        If strDisable = "true" Then
                            objTextBox.Attributes.Add("disabled", strDisable)
                        End If

                        objLabel = New Label
                        objLabel.Text = "標題："
                        objTD.Controls.Add(objLabel)
                        objTD.Controls.Add(objTextBox)
                        objTR.Cells.Add(objTD)

                        '建立英文標題input
                        objTD = New TableCell
                        objTD.Style.Item("width") = "160"
                        objTextBox = New TextBox()
                        objTextBox.ID = "txt_E" & .Item("RightID").ToString()
                        objTextBox.Text = .Item("CaptionEng").ToString()
                        objTextBox.CssClass = "InputTextStyle_Thin"
                        objTextBox.Style.Item("Width") = "130px"
                        objTextBox.MaxLength = 20
                        If strDisable = "true" Then
                            objTextBox.Attributes.Add("disabled", strDisable)
                        End If

                        objLabel = New Label
                        objLabel.Text = "英文標題："
                        objTD.Controls.Add(objLabel)
                        objTD.Controls.Add(objTextBox)
                        objTR.Cells.Add(objTD)

                        '建立checkbox 隱藏按鈕
                        objTD = New TableCell
                        objTD.Style.Item("width") = ""
                        objCheckBox = New CheckBox
                        objCheckBox.ID = "chkVisible_" & .Item("RightID").ToString()
                        If .Item("IsVisible").ToString() = "0" Then
                            objCheckBox.Checked = True
                        End If
                        objTD.Controls.Add(objCheckBox)
                        objLabel = New Label
                        objLabel.Text = "隱藏按鈕"
                        objTD.Controls.Add(objLabel)
                        objTR.Cells.Add(objTD)
                    End With

                    tblFunRight.Rows.Add(objTR)
                Next
            End Using
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & "subGetRight", ex)
            Return
        End Try

        '如果有輸入再把資料寫回
        If txtFunRight.Text.ToString().Trim().Length <> 0 Then
            Dim aryRights() As String = Split(txtFunRight.Text, "|$|")
            Dim intLoop As Integer

            For intLoop = 0 To aryRights.Length - 1
                Dim aryDetail() As String = Split(aryRights(intLoop), "||")

                Dim ctl1 As Control = tblFunRight.FindControl("chk_" & aryDetail(0))
                Dim ctl2 As Control = tblFunRight.FindControl("txt_" & aryDetail(0))
                Dim ctl3 As Control = tblFunRight.FindControl("txt_E" & aryDetail(0))
                Dim ctl4 As Control = tblFunRight.FindControl("chkVisible_" & aryDetail(0))

                If ctl1 IsNot Nothing Then
                    CType(ctl1, CheckBox).Checked = True
                End If

                If ctl2 IsNot Nothing Then
                    CType(ctl2, TextBox).Text = aryDetail(1)
                    CType(ctl2, TextBox).Attributes.Remove("disabled")
                End If

                If ctl3 IsNot Nothing Then
                    CType(ctl3, TextBox).Text = aryDetail(2)
                    CType(ctl3, TextBox).Attributes.Remove("disabled")
                End If

                If ctl4 IsNot Nothing Then
                    CType(ctl4, CheckBox).Checked = IIf(aryDetail(3) = "0", True, False)
                End If
            Next
        End If
    End Sub

    Private Function SaveData() As Boolean
        Dim beFun As New beSC_Fun.Row()
        Dim bsFun As New beSC_Fun.Service()
        Dim beFunRight() As beSC_FunRight.Row = Nothing
        Dim objSC As New SC

        beFun.FunID.Value = lblFunID.Text
        beFun.FunName.Value = txtFunName.Text
        beFun.FunEngName.Value = txtFunEngName.Text
        beFun.IsMenu.Value = IIf(rbnIsMenu_No.Checked, "0", "1")
        beFun.CheckRight.Value = IIf(rdoCheckRightY.Checked, "1", "0")
        beFun.OrderSeq.Value = CInt(txtOrderSeq.Text)
        beFun.ParentFormID.Value = ddlParentFormID.SelectedItem.Value
        beFun.Path.Value = txtPath.Text
        beFun.LastChgID.Value = UserProfile.ActUserID
        beFun.LastChgDate.Value = Now

        If Not bsFun.IsDataExists(beFun) Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00020", "")
            Return False
        End If

        If txtFunRight.Text <> "" Then
            Dim aryRights() As String = Split(txtFunRight.Text, "|$|")
            ReDim beFunRight(aryRights.GetUpperBound(0))

            For intLoop As Integer = 0 To aryRights.GetUpperBound(0)
                Dim aryDetail() As String = Split(aryRights(intLoop), "||")
                Dim beChildFunRight As New beSC_FunRight.Row()

                beChildFunRight.FunID.Value = lblFunID.Text
                beChildFunRight.RightID.Value = aryDetail(0)
                beChildFunRight.Caption.Value = aryDetail(1)
                beChildFunRight.CaptionEng.Value = aryDetail(2)
                beChildFunRight.IsVisible.Value = aryDetail(3)

                beFunRight(intLoop) = beChildFunRight
            Next

        End If

        Try
            objSC.UpdateFun(beFun, beFunRight)
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".SaveData", ex)
            subGetRight(lblFunID.Text)
            Return False
        End Try

        Return True

    End Function

    Private Function funCheckData() As Boolean
        '檢查功能代碼
        Dim strValue As String

        strValue = txtFunName.Text.Trim()
        If strValue = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "功能名稱")
            txtFunName.Focus()
            Return False
        Else
            If Bsp.Utility.getStringLength(strValue) > txtFunName.MaxLength Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00040", "功能名稱", txtFunName.MaxLength.ToString())
                txtFunName.Focus()
                Return False
            End If
            txtFunName.Text = strValue
        End If

        strValue = txtFunEngName.Text.Trim()
        If Bsp.Utility.getStringLength(strValue) > txtFunEngName.MaxLength Then
            'Bsp.Utility.ShowMessage(Me, "[CC0241_5]：最大長度為" & txtFunEngName.MaxLength.ToString())
            Bsp.Utility.ShowFormatMessage(Me, "W_00040", "功能英文名稱", txtFunEngName.MaxLength.ToString())
            txtFunEngName.Focus()
            Return False
        End If
        txtFunEngName.Text = strValue

        strValue = txtOrderSeq.Text.Trim()
        If Not IsNumeric(strValue) Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00050", "Menu排序")
            txtOrderSeq.Focus()
            Return False
        End If
        txtOrderSeq.Text = strValue

        strValue = txtPath.Text.Trim()
        If Bsp.Utility.getStringLength(strValue) > txtPath.MaxLength Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00040", "網頁路徑", txtPath.MaxLength.ToString())
            txtPath.Focus()
            Return False
        End If
        txtPath.Text = strValue

        'If txtFunRight.Text.Trim() = "" Then
        '    Bsp.Utility.ShowMessage(Me, "[CC0242_8]：未選取功能權限")
        '    Return False
        'End If

        Return True

    End Function
End Class
