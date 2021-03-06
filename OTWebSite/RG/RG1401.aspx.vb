'****************************************************
'功能說明：新進員工文件繳交作業_證券-新增
'建立人員：BeatriceCheng
'建立日期：2015.07.28
'****************************************************
Imports System.Data

Partial Class RG_RG1401
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            txtCompID.Text = UserProfile.SelectCompRoleName

            ViewState("Retire") = "0"

            '員工編號、員工姓名
            'ucQueryEmp.ShowCompRole = False
            ucQueryEmp.ShowCompRole = "False"
            ucQueryEmp.InValidFlag = "N"
            ucQueryEmp.SelectCompID = UserProfile.SelectCompRoleID
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
        Dim beCheckInFile_SPHSC1 As New beCheckInFile_SPHSC1.Row()
        Dim bsCheckInFile_SPHSC1 As New beCheckInFile_SPHSC1.Service()

        Dim bePersonal As New bePersonal.Row()
        Dim bsPersonal As New bePersonal.Service()
        Dim objRG1 As New RG1()

        '取得輸入資料
        beCheckInFile_SPHSC1.CompID.Value = UserProfile.SelectCompRoleID
        beCheckInFile_SPHSC1.EmpID.Value = txtEmpID.Text.ToUpper
        beCheckInFile_SPHSC1.Remark.Value = txtRemark.Text
        beCheckInFile_SPHSC1.File1.Value = IIf(cbFile1.Checked, "1", "0")
        beCheckInFile_SPHSC1.File2.Value = IIf(cbFile2.Checked, "1", "0")
        beCheckInFile_SPHSC1.File3.Value = IIf(cbFile3.Checked, "1", "0")
        beCheckInFile_SPHSC1.File4.Value = IIf(cbFile4.Checked, "1", "0")
        beCheckInFile_SPHSC1.File5.Value = IIf(cbFile5.Checked, "1", "0")
        beCheckInFile_SPHSC1.File6.Value = IIf(cbFile6.Checked, "1", "0")
        beCheckInFile_SPHSC1.File7.Value = IIf(cbFile7.Checked, "1", "0")
        beCheckInFile_SPHSC1.File8.Value = IIf(cbFile8.Checked, "1", "0")
        beCheckInFile_SPHSC1.File9.Value = IIf(cbFile9.Checked, "1", "0")
        beCheckInFile_SPHSC1.File10.Value = IIf(cbFile10.Checked, "1", "0")
        beCheckInFile_SPHSC1.File11.Value = ViewState("Retire")
        beCheckInFile_SPHSC1.File12.Value = IIf(cbFile12.Checked, "1", "0")
        beCheckInFile_SPHSC1.File13.Value = IIf(cbFile13.Checked, "1", "0")
        beCheckInFile_SPHSC1.File14.Value = IIf(cbFile14.Checked, "1", "0")
        beCheckInFile_SPHSC1.File15.Value = IIf(cbFile15.Checked, "1", "0")
        beCheckInFile_SPHSC1.File16.Value = IIf(cbFile16.Checked, "1", "0")
        beCheckInFile_SPHSC1.File17.Value = IIf(cbFile17.Checked, "1", "0")
        beCheckInFile_SPHSC1.File18.Value = IIf(cbFile18.Checked, "1", "0")
        '2015/12/01 Add
        beCheckInFile_SPHSC1.LastChgComp.Value = UserProfile.ActCompID
        beCheckInFile_SPHSC1.LastChgID.Value = UserProfile.ActUserID
        beCheckInFile_SPHSC1.LastChgDate.Value = Now

        bePersonal.CompID.Value = UserProfile.SelectCompRoleID
        bePersonal.EmpID.Value = txtEmpID.Text.ToUpper
        bePersonal.CheckInFlag.Value = "1"
        bePersonal.LastChgComp.Value = UserProfile.SelectCompRoleID
        bePersonal.LastChgID.Value = UserProfile.UserID
        bePersonal.LastChgDate.Value = Now

        '檢查資料是否存在
        If bsCheckInFile_SPHSC1.IsDataExists(beCheckInFile_SPHSC1) Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00010", "")
            Return False
        End If

        '儲存資料
        Try
            Return objRG1.AddCheckInFile_SPHSC1(beCheckInFile_SPHSC1, bePersonal)
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, "[CC0201_99]" & Bsp.Utility.getMessage("E_00000") & Bsp.Utility.getInnerException(Me.FunID, ex))
            Return False
        End Try
    End Function

    Private Function funCheckData() As Boolean

        '員工編號
        If txtEmpID.Text.Trim = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblEmpID.Text)
            txtEmpID.Focus()
            Return False
        End If
        If Bsp.Utility.getStringLength(txtEmpID.Text.Trim) > txtEmpID.MaxLength Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00040", lblEmpID.Text, txtEmpID.MaxLength.ToString)
            Return False
        End If

        '員工編號不存在
        If Not CheckEmpData() Then
            Return False
        End If

        '備註
        If txtRemark.Text.Trim.Length > txtRemark.MaxLength Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00040", lblRemark.Text, txtRemark.MaxLength.ToString)
            txtRemark.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub ClearData()
        txtEmpID.Text = ""

        ClearCheckBox()
    End Sub

    Private Sub ClearCheckBox()
        txtName.Text = ""
        txtOrgan.Text = ""
        txtEmpDate.Text = ""
        txtRemark.Text = ""
        cbFileAll.Checked = False
        cbFileAll.Enabled = False

        For i As Integer = 1 To 18
            Dim cbFile As CheckBox = Me.FindControl("cbFile" + i.ToString)
            cbFile.Checked = False
        Next
    End Sub

    Public Overrides Sub DoModalReturn(ByVal returnValue As String)
        Dim strSql As String = ""

        If returnValue <> "" Then
            Dim aryData() As String = returnValue.Split(":")

            Select Case aryData(0)
                Case "ucQueryEmp"
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    txtEmpID.Text = aryValue(1)
                    'txtName.Text = aryValue(2)
                    ClearCheckBox()
                    CheckEmpData()
            End Select
        End If
    End Sub

    Private Function CheckEmpData() As Boolean
        Dim objRG1 As New RG1
        Dim PersonalTable As DataTable = objRG1.GetEmpData(UserProfile.SelectCompRoleID, txtEmpID.Text.ToUpper)

        If PersonalTable.Rows.Count > 0 Then
            txtName.Text = PersonalTable.Rows(0).Item("NameN")
            txtOrgan.Text = PersonalTable.Rows(0).Item("OrganName")
            txtEmpDate.Text = PersonalTable.Rows(0).Item("EmpDate")
            cbFileAll.Enabled = True

            If PersonalTable.Rows(0).Item("Sex") = "2" Or PersonalTable.Rows(0).Item("NationID") = "2" Then
                ViewState("Retire") = "1"
            Else
                ViewState("Retire") = "0"
            End If
            Return True
        Else
            Bsp.Utility.ShowMessage(Me, "員工編號不存在！")
            txtEmpID.Focus()
            Return False
        End If
    End Function

    Protected Sub txtEmpID_TextChanged(sender As Object, e As System.EventArgs) Handles txtEmpID.TextChanged
        ClearCheckBox()
        CheckEmpData()
    End Sub

    Protected Sub cbFileAll_CheckedChanged(sender As Object, e As System.EventArgs) Handles cbFileAll.CheckedChanged
        If cbFileAll.Checked = True Then
            For i As Integer = 1 To 18
                Dim cbFile As CheckBox = Me.FindControl("cbFile" + i.ToString)
                If cbFile.Enabled = True Then
                    cbFile.Checked = True
                End If
            Next
        Else
            For i As Integer = 1 To 18
                Dim cbFile As CheckBox = Me.FindControl("cbFile" + i.ToString)
                cbFile.Checked = False
            Next
        End If
    End Sub
End Class
