'****************************************************
'功能說明：新進員工文件繳交作業_證券-修改
'建立人員：BeatriceCheng
'建立日期：2015.07.28
'****************************************************
Imports System.Data

Partial Class RG_RG1402
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            txtCompID.Text = UserProfile.SelectCompRoleName
        End If
    End Sub
    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

            If ht.ContainsKey("SelectedCompID") Then
                ViewState.Item("CompID") = ht("SelectedCompID").ToString()
                ViewState.Item("EmpID") = ht("SelectedCompID").ToString()
                subGetData(ht("SelectedCompID").ToString(), ht("SelectedEmpID").ToString())
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

    Private Sub subGetData(ByVal CompID As String, ByVal EmpID As String)
        Dim objRG1 As New RG1()
        Dim objSC As New SC()   '2015/12/01 Add
        Dim beCheckInFile_SPHSC1 As New beCheckInFile_SPHSC1.Row()
        Dim bsCheckInFile_SPHSC1 As New beCheckInFile_SPHSC1.Service()

        beCheckInFile_SPHSC1.CompID.Value = CompID
        beCheckInFile_SPHSC1.EmpID.Value = EmpID
        Try
            Using dt As DataTable = bsCheckInFile_SPHSC1.QueryByKey(beCheckInFile_SPHSC1).Tables(0)
                If dt.Rows.Count <= 0 Then Exit Sub
                beCheckInFile_SPHSC1 = New beCheckInFile_SPHSC1.Row(dt.Rows(0))

                txtCompID.Text = beCheckInFile_SPHSC1.CompID.Value
                txtEmpID.Text = beCheckInFile_SPHSC1.EmpID.Value
                txtRemark.Text = beCheckInFile_SPHSC1.Remark.Value

                cbFile1.Checked = IIf(beCheckInFile_SPHSC1.File1.Value = "1", True, False)
                cbFile2.Checked = IIf(beCheckInFile_SPHSC1.File2.Value = "1", True, False)
                cbFile3.Checked = IIf(beCheckInFile_SPHSC1.File3.Value = "1", True, False)
                cbFile4.Checked = IIf(beCheckInFile_SPHSC1.File4.Value = "1", True, False)
                cbFile5.Checked = IIf(beCheckInFile_SPHSC1.File5.Value = "1", True, False)
                cbFile6.Checked = IIf(beCheckInFile_SPHSC1.File6.Value = "1", True, False)
                cbFile7.Checked = IIf(beCheckInFile_SPHSC1.File7.Value = "1", True, False)
                cbFile8.Checked = IIf(beCheckInFile_SPHSC1.File8.Value = "1", True, False)
                cbFile9.Checked = IIf(beCheckInFile_SPHSC1.File9.Value = "1", True, False)
                cbFile10.Checked = IIf(beCheckInFile_SPHSC1.File10.Value = "1", True, False)
                cbFile11.Checked = IIf(beCheckInFile_SPHSC1.File11.Value = "1", True, False)
                cbFile12.Checked = IIf(beCheckInFile_SPHSC1.File12.Value = "1", True, False)
                cbFile13.Checked = IIf(beCheckInFile_SPHSC1.File13.Value = "1", True, False)
                cbFile14.Checked = IIf(beCheckInFile_SPHSC1.File14.Value = "1", True, False)
                cbFile15.Checked = IIf(beCheckInFile_SPHSC1.File15.Value = "1", True, False)
                cbFile16.Checked = IIf(beCheckInFile_SPHSC1.File16.Value = "1", True, False)
                cbFile17.Checked = IIf(beCheckInFile_SPHSC1.File17.Value = "1", True, False)
                cbFile18.Checked = IIf(beCheckInFile_SPHSC1.File18.Value = "1", True, False)

                '2015/12/01 Add 增加欄位:最後異動公司,最後異動人員,最後異動日期
                '最後異動公司
                Dim CompName As String = objSC.GetSC_CompName(beCheckInFile_SPHSC1.LastChgComp.Value)
                txtLastChgComp.Text = beCheckInFile_SPHSC1.LastChgComp.Value + IIf(CompName <> "", "-" + CompName, "")

                '最後異動人員
                Dim UserName As String = objSC.GetSC_UserName(beCheckInFile_SPHSC1.LastChgComp.Value, beCheckInFile_SPHSC1.LastChgID.Value)
                txtLastChgID.Text = beCheckInFile_SPHSC1.LastChgID.Value + IIf(UserName <> "", "-" + UserName, "")

                '最後異動日期
                Dim boolDate As Boolean = Format(beCheckInFile_SPHSC1.LastChgDate.Value, "yyyy/MM/dd") = "1900/01/01"
                txtLastChgDate.Text = IIf(boolDate, "", beCheckInFile_SPHSC1.LastChgDate.Value.ToString("yyyy/MM/dd HH:mm:ss"))

            End Using

            Using dt As DataTable = objRG1.GetEmpData(txtCompID.Text, txtEmpID.Text)
                If dt.Rows.Count <= 0 Then Exit Sub

                txtName.Text = dt.Rows(0).Item("NameN")
                txtOrgan.Text = dt.Rows(0).Item("OrganName")
                txtEmpDate.Text = dt.Rows(0).Item("EmpDate")
            End Using

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & "subGetData", ex)
        End Try

    End Sub

    Private Function SaveData() As Boolean
        Dim beCheckInFile_SPHSC1 As New beCheckInFile_SPHSC1.Row()
        Dim bsCheckInFile_SPHSC1 As New beCheckInFile_SPHSC1.Service()

        Dim bePersonal As New bePersonal.Row()
        Dim bsPersonal As New bePersonal.Service()
        Dim objRG1 As New RG1()

        '取得輸入資料
        beCheckInFile_SPHSC1.CompID.Value = UserProfile.SelectCompRoleID
        beCheckInFile_SPHSC1.EmpID.Value = txtEmpID.Text
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
        beCheckInFile_SPHSC1.File11.Value = IIf(cbFile11.Checked, "1", "0")
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
        bePersonal.EmpID.Value = txtEmpID.Text
        bePersonal.LastChgComp.Value = UserProfile.SelectCompRoleID
        bePersonal.LastChgID.Value = UserProfile.UserID
        bePersonal.LastChgDate.Value = Now

        bePersonal.CheckInFlag.Value = "3"
        For i As Integer = 1 To 18
            Dim cbFile As CheckBox = Me.FindControl("cbFile" + i.ToString)
            If cbFile.Checked = False Then
                bePersonal.CheckInFlag.Value = "1"
            End If
        Next

        '儲存資料
        Try
            Return objRG1.UpdateCheckInFile_SPHSC1(beCheckInFile_SPHSC1, bePersonal)
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, "[CC0201_99]" & Bsp.Utility.getMessage("E_00000") & Bsp.Utility.getInnerException(Me.FunID, ex))
            Return False
        End Try
    End Function

    Private Function funCheckData() As Boolean

        Dim strValue As String = ""

        strValue = txtRemark.Text
        If strValue.Length > txtRemark.MaxLength Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00040", lblRemark.Text, txtRemark.MaxLength.ToString())
            txtRemark.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub ClearData()
        subGetData(ViewState("CompID"), ViewState("EmpID"))
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
                If cbFile.Enabled = True Then
                    cbFile.Checked = False
                End If
            Next
        End If
    End Sub
End Class
