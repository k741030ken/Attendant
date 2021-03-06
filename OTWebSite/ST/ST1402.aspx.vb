'****************************************************
'功能說明：員工家庭狀況維護-修改
'建立人員：Micky Sung
'建立日期：2015.06.04
'****************************************************
Imports System.Data

Partial Class ST_ST1402
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            '稱謂
            Bsp.Utility.FillRelativeID(ddlRelativeID)
            ddlRelativeID.Items.Insert(0, New ListItem("---請選擇---", ""))

            '眷屬產業別
            Bsp.Utility.FillIndustryType(ddlIndustryType)
            ddlIndustryType.Items.Insert(0, New ListItem("---請選擇---", ""))
        End If
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim objSC As New SC
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

            If ht.ContainsKey("SelectedIDNo") Then
                ViewState.Item("CompID") = ht("SelectedCompID").ToString()
                ViewState.Item("EmpID") = ht("SelectedEmpID").ToString()
                ViewState.Item("EmpName") = ht("SelectedEmpName").ToString()
                ViewState.Item("IDNo") = ht("SelectedIDNo").ToString()

                txtCompID.Text = ViewState.Item("CompID").ToString() + "-" + objSC.GetCompName(ViewState.Item("CompID").ToString()).Rows(0).Item("CompName").ToString
                txtEmpID.Text = ViewState.Item("EmpID").ToString()
                txtEmpName.Text = ViewState.Item("EmpName").ToString()
                subGetData(ht("SelectedIDNo").ToString(), ht("SelectedRelativeIDNo").ToString())
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
        Dim beFamily As New beFamily.Row()
        Dim bsFamily As New beFamily.Service()
        Dim objST As New ST1

        beFamily.IDNo.Value = ViewState.Item("IDNo")
        beFamily.RelativeID.Value = ddlRelativeID.SelectedValue
        beFamily.NameN.Value = txtNameN.Text
        beFamily.Name.Value = txtName.Text
        beFamily.RelativeIDNo.Value = txtRelativeIDNo.Text.ToUpper
        beFamily.BirthDate.Value = IIf(txtBirthDate.DateText <> "" And txtBirthDate.DateText <> "____/__/__", txtBirthDate.DateText, "1900/01/01")
        beFamily.Occupation.Value = txtOccupation.Text
        beFamily.IndustryType.Value = ddlIndustryType.SelectedValue
        beFamily.Company.Value = txtCompany.Text
        beFamily.IsBSPEmp.Value = ddlIsBSPEmp.SelectedValue
        beFamily.DeleteMark.Value = ddlDeleteMark.SelectedValue
        beFamily.LastChgComp.Value = UserProfile.ActCompID
        beFamily.LastChgID.Value = UserProfile.ActUserID
        beFamily.LastChgDate.Value = Now

        Dim LastChgUserName As String = UserProfile.ActUserName

        Dim isMarriage As String = ""
        If ddlRelativeID.SelectedValue = "01" And ddlDeleteMark.SelectedValue = "0" Then
            isMarriage = "2"
        ElseIf ddlRelativeID.SelectedValue = "01" And ddlDeleteMark.SelectedValue = "2" Then
            isMarriage = "1"
        End If


        Dim isIDNoChange As Boolean = False
        If hidRelativeIDNo.Value <> txtRelativeIDNo.Text.ToUpper Then
            isIDNoChange = True
        End If

        Dim isFlagChange As Boolean = False
        If ViewState("DeleteMark") = "0" And ViewState("DeleteMark") <> ddlDeleteMark.SelectedValue Then
            isFlagChange = True
        End If

        '儲存資料
        Try
            Return objST.UpdateFamilySetting(beFamily, ViewState.Item("CompID"), ViewState.Item("EmpID"), hidRelativeIDNo.Value.Trim, LastChgUserName, isMarriage, isIDNoChange, isFlagChange)
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, "[CC0201_99]" & Bsp.Utility.getMessage("E_00000") & Bsp.Utility.getInnerException(Me.FunID, ex))
            Return False
        End Try
    End Function

    Private Function funCheckData() As Boolean
        Dim objHR As New HR
        Dim objST As New ST1
        Dim beFamily As New beFamily.Row()
        Dim bsFamily As New beFamily.Service()

        '稱謂
        If ddlRelativeID.SelectedValue = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblRelativeID.Text)
            ddlRelativeID.Focus()
            Return False
        End If

        '眷屬姓名
        Dim NameN As String = txtNameN.Text.ToString().Trim()
        If NameN = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblNameN.Text)
            txtNameN.Focus()
            Return False
        Else
            If Bsp.Utility.getStringLength(NameN) > txtNameN.MaxLength Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00040", lblNameN.Text, txtNameN.MaxLength.ToString())
                txtNameN.Focus()
                Return False
            End If
            txtNameN.Text = NameN
        End If

        '眷屬姓名(拆字)
        Dim Name As String = txtName.Text.ToString().Trim()
        If txtName.Text = "" Then
            Bsp.Utility.ShowMessage(Me, "「眷屬姓名(拆字)不得為空白」")  '2015/12/23 Modify 修改錯誤訊息
            txtName.Focus()
            Return False
        Else
            If Bsp.Utility.getStringLength(Name) > txtName.MaxLength Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00040", "眷屬姓名(拆字)", txtName.MaxLength.ToString())
                txtName.Focus()
                Return False
            End If

            For iLoop As Integer = 1 To Len(Name.Replace("?", ""))
                If Bsp.Utility.getStringLength(Mid(Name, iLoop, 1)) = 1 And Not Char.IsLower(Mid(Name, iLoop, 1)) And Not Char.IsUpper(Mid(Name, iLoop, 1)) Then
                    Bsp.Utility.ShowFormatMessage(Me, "W_00031", "眷屬姓名(拆字)請勿輸入難字!")
                    txtName.Focus()
                    Return False
                End If
            Next
            txtName.Text = Name
        End If

        '狀態註記
        If ddlRelativeID.SelectedValue <> "01" And ddlDeleteMark.SelectedValue = "2" Then
            Bsp.Utility.ShowMessage(Me, "「稱謂」不為01-配偶時，不可註記為2-離婚")
            ddlDeleteMark.Focus()
            Return False
        End If

        '是否有保險
        If hidRelativeID.Value <> ddlRelativeID.Text Or
            hidNameN.Value <> txtNameN.Text Or
            hidName.Value <> txtName.Text Or
            hidRelativeIDNo.Value <> txtRelativeIDNo.Text.ToUpper Or
            hidBirthDate.DateText <> txtBirthDate.DateText Then

            If objST.checkStatus(ViewState.Item("CompID"), ViewState.Item("EmpID"), ViewState.Item("IDNo"), hidRelativeIDNo.Value).Rows(0).Item(0) > 0 Then
                Bsp.Utility.ShowMessage(Me, "「不可修改，已有加保保險資料，請由保險功能修改」")
                txtRelativeIDNo.Focus()
                Return False
            End If
        End If

        '眷屬身分證字號
        If txtRelativeIDNo.Text = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblRelativeIDNo.Text)
            txtRelativeIDNo.Focus()
            Return False
        Else
            '眷屬身分證是否重複
            If hidRelativeIDNo.Value <> txtRelativeIDNo.Text.ToUpper Then
                If objST.checkRelativeIDNo(ViewState.Item("CompID"), ViewState.Item("EmpID"), ViewState.Item("IDNo"), txtRelativeIDNo.Text.ToUpper).Rows(0).Item(0) > 0 Then
                    Bsp.Utility.ShowMessage(Me, "「相同員工或眷屬身份證字號資料已存在，請重新輸入！」")
                    txtRelativeIDNo.Focus()
                    Return False
                End If
            End If

            '身分證邏輯判斷
            If objHR.funCheckIDNO(txtRelativeIDNo.Text.ToUpper) = False Then
                Bsp.Utility.RunClientScript(Me, "confirmAdd()")
                Return False    '2015/12/23 Add
            End If
        End If

        Return True
    End Function

    Private Sub subGetData(ByVal IDNo As String, ByVal RelativeIDNo As String)
        Dim objSC As New SC
        Dim beFamily As New beFamily.Row()
        Dim bsFamily As New beFamily.Service()

        beFamily.IDNo.Value = IDNo
        beFamily.RelativeIDNo.Value = RelativeIDNo

        Try
            Using dt As DataTable = bsFamily.QueryByKey(beFamily).Tables(0)
                If dt.Rows.Count <= 0 Then Exit Sub
                beFamily = New beFamily.Row(dt.Rows(0))
                ddlRelativeID.Text = beFamily.RelativeID.Value
                txtNameN.Text = beFamily.NameN.Value
                txtName.Text = beFamily.Name.Value
                txtRelativeIDNo.Text = beFamily.RelativeIDNo.Value.Trim
                txtBirthDate.DateText = IIf(Format(beFamily.BirthDate.Value, "yyyy/MM/dd") = "1900/01/01", "", beFamily.BirthDate.Value.ToString("yyyy/MM/dd HH:mm:ss"))
                txtOccupation.Text = beFamily.Occupation.Value
                ddlIndustryType.SelectedValue = IIf(beFamily.IndustryType.Value.Trim = "0", "", beFamily.IndustryType.Value.Trim)   '2015/12/23 Modify 增加判斷是否為0
                txtCompany.Text = beFamily.Company.Value
                ddlIsBSPEmp.SelectedValue = beFamily.IsBSPEmp.Value
                ddlDeleteMark.SelectedValue = beFamily.DeleteMark.Value
                ViewState("DeleteMark") = beFamily.DeleteMark.Value

                '最後異動公司
                Dim CompName As String = objSC.GetSC_CompName(beFamily.LastChgComp.Value)
                lblLastChgComp.Text = beFamily.LastChgComp.Value + IIf(CompName <> "", "-" + CompName, "")

                '最後異動人員
                Dim UserName As String = objSC.GetSC_UserName(beFamily.LastChgComp.Value, beFamily.LastChgID.Value)
                lblLastChgID.Text = beFamily.LastChgID.Value + IIf(UserName <> "", "-" + UserName, "")

                '最後異動日期
                Dim boolDate As Boolean = Format(beFamily.LastChgDate.Value, "yyyy/MM/dd") = "1900/01/01"
                lblLastChgDate.Text = IIf(boolDate, "", beFamily.LastChgDate.Value.ToString("yyyy/MM/dd HH:mm:ss"))

                '原稱謂(隱藏欄位)
                hidRelativeID.Value = beFamily.RelativeID.Value.Trim
                hidNameN.Value = beFamily.NameN.Value
                hidName.Value = beFamily.Name.Value
                hidBirthDate.DateText = IIf(Format(beFamily.BirthDate.Value, "yyyy/MM/dd") = "1900/01/01", "", beFamily.BirthDate.Value.ToString("yyyy/MM/dd HH:mm:ss"))
                hidRelativeIDNo.Value = beFamily.RelativeIDNo.Value.Trim

            End Using
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & "subGetData", ex)
        End Try

    End Sub

    Private Sub ClearData()
        subGetData(ViewState.Item("IDNo"), hidRelativeIDNo.Value)
    End Sub

    Protected Sub btnYes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnYes.Click

    End Sub

    Protected Sub btnNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNo.Click
        If SaveData() Then
            GoBack()
        End If
    End Sub

    Protected Sub txtNameN_TextChanged(sender As Object, e As System.EventArgs) Handles txtNameN.TextChanged
        Dim objRG As New RG1
        Dim strNameN As String = txtNameN.Text.Trim.Replace(vbCrLf, "")
        Dim rtnName As String = ""

        If strNameN.Length > 0 Then
            For iLoop As Integer = 1 To Len(strNameN)
                If Bsp.Utility.getStringLength(Mid(strNameN, iLoop, 1)) = 1 And Not Char.IsLower(Mid(strNameN, iLoop, 1)) And Not Char.IsUpper(Mid(strNameN, iLoop, 1)) Then
                    Dim QName = objRG.QueryData("HRNameMap", "And NameN = N" & Bsp.Utility.Quote(Mid(strNameN, iLoop, 1)), "Name")
                    If QName <> "" Then
                        rtnName = rtnName & QName
                    Else
                        rtnName = rtnName & "?"
                    End If
                Else
                    rtnName = rtnName & Mid(strNameN, iLoop, 1)
                End If
            Next
        End If

        If rtnName = "" Then
            txtName.Text = strNameN
        Else
            txtName.Text = rtnName
        End If
    End Sub
End Class
