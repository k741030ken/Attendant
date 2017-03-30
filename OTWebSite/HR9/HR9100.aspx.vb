'****************************************************
'功能說明：HR9100人事處放行作業 - 查詢
'建立人員：Ann
'建立日期：2014.10.01
'****************************************************
Imports System.Data
Imports System.Data.Common

Partial Class HR_HR9100
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            Dim objSC As New SC
            Dim objHR9100 As New HR9100

            If UserProfile.SelectCompRoleID = "ALL" Then
                lblCompRoleID.Visible = False
                Bsp.Utility.FillHRCompany(ddlCompID)
                Page.SetFocus(ddlCompID)
            Else
                lblCompRoleID.Text = UserProfile.SelectCompRoleID + "-" + objSC.GetCompName(UserProfile.SelectCompRoleID).Rows(0).Item("CompName").ToString
                ddlCompID.Visible = False
                Page.SetFocus(txtEmpID)
            End If

            HR9100.FillPersonalReason(ddlReason)
            ddlReason.Items.Insert(0, New ListItem("全選", "0"))

        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        '未寫死button：確認ActionC、離開ActionX
        Select Case Param
            Case "btnUpdate"    '修改
                DoUpdate(Param)
            Case "btnQuery"     '查詢
                ViewState.Item("DoQuery") = "Y"
                DoQuery()
            Case "btnDelete"    '刪除
                DoDelete()
            Case "btnRelease"   '放行
                'ViewState.Item("Action") = "btnRelease"
                DoRelease()
                'Case "btnAdd"       '駁回 '20150716 Ann modify
            Case "btnReject"       '駁回 '20150716 Ann modify
                DoOverrule()
            Case "btnActionC"   '確認
                DoUpdate(Param) '明細
            Case "btnActionX"   '離開
                'Case "btnPrint"     '眷屬查詢  '20150716 Ann del
                '   DoUpdate(Param)             '20150716 Ann del
        End Select
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

            For Each strKey As String In ht.Keys
                Dim ctl As Control = Me.FindControl(strKey)

                If TypeOf ctl Is TextBox Then
                    CType(ctl, TextBox).Text = ht(strKey).ToString()
                End If
                If TypeOf ctl Is CheckBox Then
                    CType(ctl, CheckBox).Checked = CBool(ht(strKey).ToString())
                End If
                If TypeOf ctl Is DropDownList Then
                    CType(ctl, DropDownList).SelectedValue = ht(strKey).ToString
                End If
            Next

            If ht.ContainsKey("PageNo") Then pcMain.PageNo = Convert.ToInt32(ht("PageNo"))
            If ht.ContainsKey("DoQuery") Then
                If ht("DoQuery").ToString() = "Y" Then
                    ViewState.Item("DoQuery") = "Y"
                    DoQuery()
                End If
            End If
        End If
    End Sub

    Private Sub DoUpdate(ByVal Param As String)
        If selectedRows(gvMain) <> "" Then
            Dim intSelectRow As Integer
            Dim intSelectCount As Integer = 0
            For intRow As Integer = 0 To gvMain.Rows.Count - 1
                Dim objChk As CheckBox = gvMain.Rows(intRow).FindControl("chk_gvMain")
                If objChk.Checked Then
                    intSelectRow = intRow
                    intSelectCount = intSelectCount + 1
                End If
            Next
            If intSelectCount = 1 Then
                Dim btnU As New ButtonState(ButtonState.emButtonType.Update)
                Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
                Dim strApplyDate As String = gvMain.DataKeys(intSelectRow)("ApplyDate1").ToString()

                Select Case Param
                    Case "btnUpdate"    '修改
                        btnU.Caption = "存檔返回"
                        btnX.Caption = "返回"
                        Me.TransferFramePage("~/HR9/HR9102.aspx", New ButtonState() {btnU, btnX}, _
                                              chkReason.ID & "=" & CStr(chkReason.Checked), _
                                              ddlReason.ID & "=" & ddlReason.SelectedValue, _
                                              chkEmpID.ID & "=" & CStr(chkEmpID.Checked), _
                                              txtEmpID.ID & "=" & txtEmpID.Text, _
                                              chkApplyDate.ID & "=" & CStr(chkApplyDate.Checked), _
                                             txtApplyDateB.ID & "=" & txtApplyDateB.Text, _
                                             txtApplyDateE.ID & "=" & txtApplyDateE.Text, _
                                             "PageNo=" & pcMain.PageNo.ToString(), _
                                             "SelectedCompID=" & gvMain.DataKeys(intSelectRow)("CompID").ToString(), _
                                             "SelectedEmpID=" & gvMain.DataKeys(intSelectRow)("EmpID").ToString(), _
                                             "SelectedReason=" & gvMain.DataKeys(intSelectRow)("Reason").ToString(), _
                                             "SelectedApplyDate=" & Convert.ToDateTime(gvMain.DataKeys(intSelectRow)("ApplyDate").ToString()), _
                                             "SelectedOldData=" & gvMain.DataKeys(intSelectRow)("OldData").ToString(), _
                                             "SelectedRelativeIDNo=" & gvMain.DataKeys(intSelectRow)("RelativeIDNo").ToString(), _
                                             "SelectedApplyDate1=" & strApplyDate, _
                                             "DoQuery=Y")
                    Case "btnPrint"     '扶養眷屬明細
                        btnX.Caption = "返回"
                        Me.TransferFramePage("~/HR9/HR9103.aspx", New ButtonState() {btnX}, _
                                              chkReason.ID & "=" & CStr(chkReason.Checked), _
                                              ddlReason.ID & "=" & ddlReason.SelectedValue, _
                                              chkEmpID.ID & "=" & CStr(chkEmpID.Checked), _
                                              txtEmpID.ID & "=" & txtEmpID.Text, _
                                              chkName.ID & "=" & CStr(chkName.Checked), _
                                              txtName.ID & "=" & txtName.Text, _
                                              chkApplyDate.ID & "=" & CStr(chkApplyDate.Checked), _
                                              txtApplyDateB.ID & "=" & txtApplyDateB.Text, _
                                              txtApplyDateE.ID & "=" & txtApplyDateE.Text, _
                                              "PageNo=" & pcMain.PageNo.ToString(), _
                                              "SelectedCompID=" & gvMain.DataKeys(intSelectRow)("CompID").ToString(), _
                                              "SelectedEmpID=" & gvMain.DataKeys(intSelectRow)("EmpID").ToString(), _
                                              "DoQuery=Y")
                    Case "btnActionC"   '明細
                        btnX.Caption = "返回"
                        Me.TransferFramePage("~/HR9/HR9102.aspx", New ButtonState() {btnX}, _
                                             chkReason.ID & "=" & CStr(chkReason.Checked), _
                                             ddlReason.ID & "=" & ddlReason.SelectedValue, _
                                             chkEmpID.ID & "=" & CStr(chkEmpID.Checked), _
                                             txtEmpID.ID & "=" & txtEmpID.Text, _
                                             chkName.ID & "=" & CStr(chkName.Checked), _
                                             txtName.ID & "=" & txtName.Text, _
                                             chkApplyDate.ID & "=" & CStr(chkApplyDate.Checked), _
                                             txtApplyDateB.ID & "=" & txtApplyDateB.Text, _
                                             txtApplyDateE.ID & "=" & txtApplyDateE.Text, _
                                             "PageNo=" & pcMain.PageNo.ToString(), _
                                             "SelectedCompID=" & gvMain.DataKeys(intSelectRow)("CompID").ToString(), _
                                             "SelectedEmpID=" & gvMain.DataKeys(intSelectRow)("EmpID").ToString(), _
                                             "SelectedReason=" & gvMain.DataKeys(intSelectRow)("Reason").ToString(), _
                                             "SelectedApplyDate=" & gvMain.DataKeys(intSelectRow)("ApplyDate").ToString(), _
                                             "SelectedOldData=" & gvMain.DataKeys(intSelectRow)("OldData").ToString(), _
                                             "SelectedRelativeIDNo=" & gvMain.DataKeys(intSelectRow)("RelativeIDNo").ToString(), _
                                             "SelectedApplyDate1=" & strApplyDate, _
                                             "DoQuery=Y")
                End Select
            Else
                Bsp.Utility.ShowMessage(Me, "修改只能選擇一筆資料")
            End If
        End If
    End Sub

    Private Sub DoQuery()
        Dim objSC As New SC()
        Dim objHR9100 As New HR9100
        Dim strSQL As New StringBuilder()
        Dim strWhere As New StringBuilder()
        Dim strCompID As String

        If funCheckData() Then
            If UserProfile.SelectCompRoleID = "ALL" Then
                strCompID = ddlCompID.SelectedValue
            Else
                strCompID = UserProfile.SelectCompRoleID
            End If
            Try
                strSQL.AppendLine(" Select ReleaseMark, P.CompID, P.EmpID, P1.NameN, RelativeIDNo, ApplyDate, P.Reason, R.ReasonName, ReleaseEmpID, ReleaseDate, Remark, Remark1, OldData, NewData, convert(varchar,ApplyDate,120) as ApplyDate1 ")
                strSQL.AppendLine(" ,isnull(C.CompName,'') as LastChgComp, isnull(P2.NameN,'') as LastChgID, isnull(P.LastChgDate,'') as LastchgDate ")
                strSQL.AppendLine(" from PersonalWait P ")
                strSQL.AppendLine(" left join Personal P1 on P.CompID = P1.CompID and P.EmpID = P1.EmpID ")
                strSQL.AppendLine(" left join Company C on C.CompID = P.LastChgComp ")
                strSQL.AppendLine(" left join Personal P2 on P.LastChgComp = P2.CompID and P.LastChgID = P2.EmpID ")
                strSQL.AppendLine(" left join PersonalReason R on P.Reason = R.Reason ")
                strSQL.AppendLine(" where ReleaseMark = '0' ")
                strSQL.AppendLine(" and P.Reason  in (Select Reason from PersonalReason where HR9100Flag = '1') ")
                strSQL.AppendLine(" and P.CompID = '" & strCompID & "'")
                If chkReason.Checked = True Then
                    If ddlReason.SelectedItem.Value <> "0" Then
                        strSQL.AppendLine(" and P.Reason = '" & ddlReason.SelectedItem.Value & "'")
                    End If
                End If
                If chkEmpID.Checked = True And txtEmpID.Text.Trim() <> "" Then
                    strSQL.AppendLine(" and P.EmpID = '" & txtEmpID.Text.Trim() & "'")
                End If
                If chkName.Checked = True And txtName.Text.Trim() <> "" Then
                    strSQL.AppendLine(" and P1.NameN like N'%" & txtName.Text.Trim() & "%'")
                End If
                If chkApplyDate.Checked = True And txtApplyDateE.Text.Trim() <> "" Then
                    strSQL.AppendLine(" and P.ApplyDate between '" & txtApplyDateB.Text.Trim() & "' and '" & txtApplyDateE.Text.Trim() & "'")
                ElseIf chkApplyDate.Checked = True And txtApplyDateB.Text.Trim() <> "" Then
                    strSQL.AppendLine(" and convert(char(10),P.ApplyDate,111) = '" & txtApplyDateB.Text.Trim() & "'")
                End If
                strSQL.AppendLine(" order by P.Reason,EmpID,ApplyDate ")

                pcMain.DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)

            Catch ex As Exception
                Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
            End Try
        End If
    End Sub

    Private Sub DoDelete()
        If selectedRows(gvMain) = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00000")
        Else
            Dim bePersonalWait As New bePersonalWait.Row()
            Dim objHR9100 As New HR9100
            Try
                For intRow As Integer = 0 To gvMain.Rows.Count - 1
                    Dim objChk As CheckBox = gvMain.Rows(intRow).FindControl("chk_gvMain")
                    If objChk.Checked Then
                        bePersonalWait.CompID.Value = gvMain.DataKeys(intRow)("CompID").ToString()
                        bePersonalWait.EmpID.Value = gvMain.DataKeys(intRow)("EmpID").ToString()
                        bePersonalWait.RelativeIDNo.Value = gvMain.DataKeys(intRow)("RelativeIDNo").ToString()
                        bePersonalWait.ApplyDate.Value = Format(gvMain.DataKeys(intRow)("ApplyDate"), "yyyy/MM/dd HH:mm:ss")
                        bePersonalWait.Reason.Value = gvMain.DataKeys(intRow)("Reason").ToString()
                        bePersonalWait.OldData.Value = gvMain.DataKeys(intRow)("OldData").ToString()
                        objHR9100.DeletePersonalWait(bePersonalWait)
                    End If
                Next
            Catch ex As Exception
                Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoDelete", ex)
            End Try
            gvMain.DataBind()   '20141029
        End If
    End Sub
    '放行
    Private Function DoRelease() As Boolean
        Dim bePersonalWait As New bePersonalWait.Row()
        Dim bsPersonalWait As New bePersonalWait.Service()
        Dim objSC As New SC
        Dim objHR9100 As New HR9100

        If selectedRows(gvMain) = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00000")
        Else
            For intRow As Integer = 0 To gvMain.Rows.Count - 1
                Dim objChk As CheckBox = gvMain.Rows(intRow).FindControl("chk_gvMain")
                If objChk.Checked Then
                    bePersonalWait.CompID.Value = UserProfile.SelectCompRoleID
                    Dim strCompID As String = gvMain.DataKeys(intRow)("CompID").ToString()
                    Dim strEmpID As String = gvMain.DataKeys(intRow)("EmpID").ToString()

                    bePersonalWait.EmpID.Value = txtEmpID.Text.ToString()

                    Dim strRelativeIDNo As String = bePersonalWait.RelativeIDNo.Value '= gvMain.DataKeys(Me.selectedRow(gvMain))("RelativeIDNo").ToString()
                    Dim strReason As String = gvMain.DataKeys(intRow)("Reason").ToString()
                    Dim strApplyDate As String = Format(gvMain.DataKeys(intRow)("ApplyDate"), "yyyy/MM/dd HH:mm:ss")
                    Dim strOldData As String = gvMain.DataKeys(intRow)("OldData").ToString()

                    '取得輸入資料
                    Using cn As DbConnection = Bsp.DB.getConnection()
                        cn.Open()
                        Dim tran As DbTransaction = cn.BeginTransaction
                        Dim inTrans As Boolean = True

                        Try
                            'objHR9100.funUpdateTables(strCompID, strEmpID, strReason, strRelativeIDNo, strApplyDate, strOldData)
                            objHR9100.funUpdate(strCompID, strEmpID, strReason, strRelativeIDNo, strApplyDate, strOldData)
                        Catch ex As Exception
                            If inTrans Then tran.Rollback()
                            Bsp.Utility.ShowMessage(Me, Me.FunID & ".SaveData", ex)
                            Return False
                        End Try
                        'End If
                    End Using
                End If
            Next
        End If
        gvMain.DataBind()   '20141029
        DoQuery()
        Return True
    End Function

    Private Sub DoOverrule()
        If selectedRows(gvMain) <> "" Then
            Dim intSelectRow As Integer
            Dim intSelectCount As Integer = 0
            For intRow As Integer = 0 To gvMain.Rows.Count - 1
                Dim objChk As CheckBox = gvMain.Rows(intRow).FindControl("chk_gvMain")
                If objChk.Checked Then
                    intSelectRow = intRow
                    intSelectCount = intSelectCount + 1
                End If
            Next
            If intSelectCount = 1 Then
                Dim btnU As New ButtonState(ButtonState.emButtonType.Update)
                Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)

                btnU.Caption = "確認駁回"
                btnX.Caption = "返回"
                Me.TransferFramePage("~/HR9/HR9101.aspx", New ButtonState() {btnU, btnX}, _
                                      chkReason.ID & "=" & CStr(chkReason.Checked), _
                                    ddlReason.ID & "=" & ddlReason.SelectedValue, _
                                     "PageNo=" & pcMain.PageNo.ToString(), _
                                     "SelectedCompID=" & gvMain.DataKeys(intSelectRow)("CompID").ToString(), _
                                     "SelectedEmpID=" & gvMain.DataKeys(intSelectRow)("EmpID").ToString(), _
                                     "SelectedReason=" & gvMain.DataKeys(intSelectRow)("Reason").ToString(), _
                                     "SelectedApplyDate=" & gvMain.DataKeys(intSelectRow)("ApplyDate").ToString(), _
                                     "SelectedOldData=" & gvMain.DataKeys(intSelectRow)("OldData").ToString(), _
                                     "SelectedRelativeIDNo=" & gvMain.DataKeys(intSelectRow)("RelativeIDNo").ToString(), _
                                     "DoQuery=Y")
            Else
                Bsp.Utility.ShowMessage(Me, "「修改、駁回」只能選擇一筆資料")
            End If
        End If
        gvMain.DataBind()   '20141029
        DoQuery()
    End Sub

    Private Function funCheckData() As Boolean
        Dim objSC As New SC
        Dim objHR9100 As New HR9100

        Dim strValue As String
        Dim strReason As String = ""

        Dim strWhere As String = ""

        '查詢條件必須選擇
        If Not chkReason.Checked And Not chkEmpID.Checked And Not chkName.Checked And Not chkApplyDate.Checked And Not chkReason.Checked Then
            Bsp.Utility.ShowFormatMessage(Me, "W_C1A00")
            Return False
        End If

        '員工編號
        If chkEmpID.Checked Then
            If txtEmpID.Text.Trim() = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00030", "員工編號")
                txtEmpID.Focus()
                Return False
            Else
                If Bsp.Utility.getStringLength(txtEmpID.Text) <> txtEmpID.MaxLength Then
                    Bsp.Utility.ShowFormatMessage(Me, "W_00040", "員工編號", txtEmpID.MaxLength.ToString())
                    txtEmpID.Focus()
                    Return False
                End If
            End If
        End If

        '員工姓名
        If chkName.Checked Then
            If txtName.Text.Trim() = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00030", "員工姓名")
                txtName.Focus()
            Else
                If Bsp.Utility.getStringLength(txtName.Text) > txtName.MaxLength Then
                    Bsp.Utility.ShowFormatMessage(Me, "W_00040", "員工姓名", txtName.MaxLength.ToString())
                    txtName.Focus()
                    Return False
                End If
            End If
        End If

        '異動原因
        If chkReason.Checked Then
            If ddlReason.SelectedValue = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00035", "異動原因")
                ddlReason.Focus()
                Return False
            End If
        End If

        '申請日期
        If chkApplyDate.Checked Then
            If txtApplyDateB.Text.Trim() = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00030", "申請日期(起)")
                txtApplyDateB.Focus()
                Return False
            Else
                If Bsp.Utility.getStringLength(txtApplyDateB.Text) > txtApplyDateB.MaxLength Then
                    Bsp.Utility.ShowFormatMessage(Me, "W_00040", "申請日期(起)", txtApplyDateB.MaxLength.ToString())
                    txtApplyDateB.Focus()
                    Return False
                Else
                    If Not IsDate(txtApplyDateB.Text) Then
                        Bsp.Utility.ShowFormatMessage(Me, "W_00070", "申請日期(起)")
                        txtApplyDateB.Focus()
                        Return False
                    End If
                End If
            End If

            If txtApplyDateE.Text.Trim() <> "" Then
                If Bsp.Utility.getStringLength(txtApplyDateE.Text) > txtApplyDateE.MaxLength Then
                    Bsp.Utility.ShowFormatMessage(Me, "W_00040", "申請日期(迄)", txtApplyDateE.MaxLength.ToString())
                    txtApplyDateE.Focus()
                    Return False
                Else
                    If Not IsDate(txtApplyDateE.Text) Then
                        Bsp.Utility.ShowFormatMessage(Me, "W_00070", "申請日期(迄)")
                        txtApplyDateE.Focus()
                        Return False
                    End If
                End If
                If txtApplyDateB.Text > txtApplyDateE.Text Then
                    Bsp.Utility.ShowFormatMessage(Me, "W_00130", "")
                    txtApplyDateE.Focus()
                    Return False
                End If
            End If
        End If

        Return True
    End Function

    Private Function funCheckInput() As Boolean
        Dim objSC As New SC
        Dim objHR9100 As New HR9100

        Dim strValue As String
        Dim strReason As String = ""

        Dim strWhere As String = ""

        '查詢條件必須選擇
        If Not chkReason.Checked And Not chkEmpID.Checked And Not chkName.Checked And Not chkApplyDate.Checked And Not chkReason.Checked Then
            Bsp.Utility.ShowFormatMessage(Me, "W_C1A00")
            Return False
        End If
        Return True
    End Function

    Protected Sub gvMain_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvMain.RowCreated
        '隱藏欄位
        Select Case e.Row.RowType
            Case DataControlRowType.Header
                e.Row.Cells(2).Visible = False  '申請日期
            Case DataControlRowType.DataRow
                e.Row.Cells(2).Visible = False
        End Select
    End Sub

    Protected Sub gvMain_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvMain.RowDataBound
        Dim bePersonalWait As New bePersonalWait.Row()
        Dim bsPersonalWait As New bePersonalWait.Service()
        Dim objSC As New SC
        Dim objHR9100 As New HR9100
        Dim strSQL As New StringBuilder
        Dim strSQLE As New StringBuilder    '學歷
        Dim strSQLS As New StringBuilder    '學校
        Dim strSQLD As New StringBuilder    '科系
        Dim strSQLD1 As New StringBuilder   '輔系
        Dim strSQLL As New StringBuilder    '證照
        Dim strSQLL1 As New StringBuilder   '證照1
        Dim strSQLT As New StringBuilder    '扶養親屬

        If e.Row.RowType = DataControlRowType.DataRow Then
            If DirectCast(gvMain.DataSource, DataTable).Rows(e.Row.RowIndex)("Reason").ToString() = "05" Then    '異動原因-學歷
                If DirectCast(gvMain.DataSource, DataTable).Rows(e.Row.RowIndex)("OldData").ToString() <> "" Then    '異動前
                    Dim strOldData As String = DirectCast(gvMain.DataSource, DataTable).Rows(e.Row.RowIndex)("OldData").ToString()  'e.Row.Cells(8).Text  '異動前
                    Dim strNewData As String = DirectCast(gvMain.DataSource, DataTable).Rows(e.Row.RowIndex)("NewData").ToString()  'e.Row.Cells(9).Text  '異動後
                    Dim aryOldData() As String = Split(strOldData, Chr(9))
                    Dim aryNewData() As String = Split(strNewData, Chr(9))
                    '學歷
                    strSQLE.AppendLine(" Select EduName ")
                    strSQLE.AppendLine(" from EduDegree ")
                    strSQLE.AppendLine(" where EduID = '" & aryOldData(0) & "' ")
                    Dim strEduName As String
                    'If strSQLE.ToString.Count > 0 Then
                    Dim strCountE As String = Bsp.DB.ExecuteScalar(strSQLE.ToString(), "eHRMSDB")
                    If strCountE <> "" Then
                        strEduName = ""
                        strEduName = Bsp.DB.ExecuteScalar(strSQLE.ToString(), "eHRMSDB")
                    End If

                    '學校
                    strSQLS.AppendLine(" Select Remark ")
                    strSQLS.AppendLine(" from School ")
                    strSQLS.AppendLine(" where SchoolID = '" & aryOldData(3) & "' ")
                    Dim strSchool As String
                    'If strSQLS.ToString.Count > 0 Then
                    Dim strCountS As String = Bsp.DB.ExecuteScalar(strSQLS.ToString(), "eHRMSDB")
                    If strCountS <> "" Then
                        strSchool = Bsp.DB.ExecuteScalar(strSQLS.ToString(), "eHRMSDB")
                    Else
                        strSchool = Trim(aryOldData(6))
                    End If

                    '科系
                    strSQLD.AppendLine(" Select Remark ")
                    strSQLD.AppendLine(" from Depart ")
                    strSQLD.AppendLine(" where DepartID = '" & aryOldData(4) & "' ")
                    Dim strDepart As String
                    Dim strCountD As String = Bsp.DB.ExecuteScalar(strSQLD.ToString(), "eHRMSDB")
                    If strCountD <> "" Then
                        strDepart = Bsp.DB.ExecuteScalar(strSQLD.ToString(), "eHRMSDB")
                    Else
                        strDepart = Trim(aryNewData(0))
                    End If
                    '輔系
                    strSQLD1.AppendLine(" Select Remark ")
                    strSQLD1.AppendLine(" from Depart ")
                    strSQLD1.AppendLine(" where DepartID = '" & aryOldData(5) & "' ")
                    Dim strSecDepart As String
                    Dim strCountD1 As String = Bsp.DB.ExecuteScalar(strSQLD1.ToString(), "eHRMSDB")
                    If strCountD1 <> "" Then
                        strSecDepart = Bsp.DB.ExecuteScalar(strSQLD1.ToString(), "eHRMSDB")
                    Else
                        strSecDepart = Trim(aryNewData(1))
                    End If

                    e.Row.Cells(10).Text = "新增 "
                    e.Row.Cells(10).Text = e.Row.Cells(10).Text & aryOldData(2) & "年度 "
                    e.Row.Cells(10).Text = e.Row.Cells(10).Text & strSchool & " "
                    e.Row.Cells(10).Text = e.Row.Cells(10).Text & strDepart
                    If strSecDepart > "" And strDepart > "" Then
                        e.Row.Cells(10).Text = e.Row.Cells(10).Text & "/"
                    End If
                    If strSecDepart > "" Then
                        e.Row.Cells(10).Text = e.Row.Cells(10).Text & strSecDepart & "(輔系)"
                    End If
                    e.Row.Cells(10).Text = e.Row.Cells(10).Text & " " & strEduName & " 學歷申請"
                End If
            End If
            If DirectCast(gvMain.DataSource, DataTable).Rows(e.Row.RowIndex)("Reason").ToString() = "20" Then    '異動原因-扶養親屬資料
                'If e.Row.Cells(6).Text = "05" Then
                'Dim tmpBtn As Button
                Dim tmpBtn As LinkButton
                tmpBtn = e.Row.Cells(1).FindControl("ibnRelative")
                If DirectCast(gvMain.DataSource, DataTable).Rows(e.Row.RowIndex)("OldData").ToString() <> "" Then    '異動前
                    'If e.Row.Cells(8).Text <> "" Then
                    Dim strOldData As String = DirectCast(gvMain.DataSource, DataTable).Rows(e.Row.RowIndex)("OldData").ToString()  'e.Row.Cells(8).Text  '異動前
                    Dim aryOldData() As String = Split(strOldData, Chr(9))
                    strSQLT.AppendLine(" Select count(T.RelativeIDNo ) ")
                    strSQLT.AppendLine(" from TaxFamily T ")
                    strSQLT.AppendLine("  left join Personal P ")
                    strSQLT.AppendLine("  on T.CompID = P.CompID and T.EmpID = P.EmpID ")
                    strSQLT.AppendLine("  left join Family F ")
                    strSQLT.AppendLine("  on T.RelativeIDNo = F.RelativeIDNo and P.IDNo = F.IDNo ")
                    strSQLT.AppendLine("  left join Relationship R ")
                    strSQLT.AppendLine("  on F.RelativeID = R.RelativeID ")
                    strSQLT.AppendLine("  where T.EmpID = '" & DirectCast(gvMain.DataSource, DataTable).Rows(e.Row.RowIndex)("EmpID").ToString() & "' ")
                    strSQLT.AppendLine("  and T.CompID ='" & DirectCast(gvMain.DataSource, DataTable).Rows(e.Row.RowIndex)("CompID").ToString() & "' ")
                    strSQLT.AppendLine("  and Rtrim(R.TaxFamilyID) <> '' ")
                    Dim intCountT As Integer = Bsp.DB.ExecuteScalar(strSQLT.ToString(), "eHRMSDB")
                    If intCountT > 0 Then
                        tmpBtn.Visible = True
                    Else
                        tmpBtn.Visible = False
                    End If
                Else
                    tmpBtn.Visible = False
                End If
            End If

            If DirectCast(gvMain.DataSource, DataTable).Rows(e.Row.RowIndex)("Reason").ToString() <> "20" Then    '異動原因-扶養親屬資料
                'Dim tmpBtn As Button
                Dim tmpBtn As LinkButton
                tmpBtn = e.Row.Cells(1).FindControl("ibnRelative")
                tmpBtn.Visible = False
            End If
        End If
        '    If e.Row.Cells(6).Text = "07" Then  '異動原因-證照
        '        If e.Row.Cells(8).Text <> "" Then
        '            Dim strOldData As String = e.Row.Cells(8).Text  '異動前
        '            Dim strNewData As String = e.Row.Cells(9).Text  '異動後

        '            Dim aryOldData() As String = Split(strOldData, Chr(9))
        '            Dim aryNewData() As String = Split(strNewData, Chr(9))
        '            '證照
        '            strSQLL.AppendLine(" Select LicenseName")
        '            strSQLL.AppendLine(" from License")
        '            strSQLL.AppendLine(" where LicenseID = '" & aryOldData(0) & "' ")
        '            Dim strLicenseName As String
        '            Dim strCertiTo As String
        '            If strSQLL.ToString.Count > 0 Then
        '                strLicenseName = Bsp.DB.ExecuteScalar(strSQLL.ToString(), "eHRMSDB")
        '                If aryOldData(2) <> "" Then
        '                    strCertiTo = aryOldData(2)
        '                Else
        '                    strCertiTo = "永久"
        '                End If
        '            End If

        '            strSQLL1.AppendLine(" Select Institution ")
        '            strSQLL1.AppendLine(" from License")
        '            strSQLL1.AppendLine(" where LicenseID = '" & aryOldData(0) & "' ")
        '            Dim strInstitution As String

        '            If strSQLL1.ToString.Count > 0 Then
        '                strInstitution = Bsp.DB.ExecuteScalar(strSQLL1.ToString(), "eHRMSDB")
        '                If aryOldData(2) <> "" Then
        '                    strCertiTo = aryOldData(2)
        '                Else
        '                    strCertiTo = "永久"
        '                End If                 
        '            End If
        '            e.Row.Cells(8).Text = "新增 "
        '            e.Row.Cells(8).Text = "新增 "
        '            e.Row.Cells(8).Text = e.Row.Cells(8).Text & aryOldData(1) & "取得, "
        '            e.Row.Cells(8).Text = e.Row.Cells(8).Text & strCertiTo & "有效, "
        '            e.Row.Cells(8).Text = e.Row.Cells(8).Text & aryOldData(0) & " "
        '            e.Row.Cells(8).Text = e.Row.Cells(8).Text  & strLicenseName & ", ")
        '            e.Row.Cells(8).Text = e.Row.Cells(8).Text & strInstitution & ", "
        '            e.Row.Cells(8).Text = e.Row.Cells(8).Text & aryOldData(3) & " 證照申請"
        '        End If
        '    End If
    End Sub
    Protected Sub gvMain_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Select Case e.CommandName
            Case "Update"
                'DoAction("btnPrint")   '20150716 Ann modify
                DoUpdate("btnPrint")    '20150716 Ann modify
            Case "Detail"
                DoAction("btnActionC")
        End Select
        '20150311 Ann del
        'Dim ibn As Button = DirectCast(e.CommandSource, Button)
        'Dim gvr As GridViewRow = DirectCast(ibn.NamingContainer, GridViewRow)
        'If selectedRows(gvMain) <> "" Then
        '    Dim intSelectRow As Integer
        '    Dim intSelectCount As Integer = 0
        '    For intRow As Integer = 0 To gvMain.Rows.Count - 1
        '        Dim objChk As CheckBox = gvMain.Rows(intRow).FindControl("chk_gvMain")
        '        If objChk.Checked Then
        '            intSelectRow = intRow
        '            intSelectCount = intSelectCount + 1
        '        End If
        '    Next
        '    If intSelectCount = 1 Then
        '        Dim btnU As New ButtonState(ButtonState.emButtonType.Update)
        '        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
        '        Select Case e.CommandName
        '            Case "Update"
        '                Dim strCompID As String = gvMain.DataKeys(selectedRows(gvMain))("CompID").ToString() 'CType(gvMain.Rows(index).Cells(1).Text.ToString(), String)    'gvr.Cells(1).Text
        '                Dim strEmpID As String = gvMain.DataKeys(selectedRows(gvMain))("EmpID").ToString() 'CType(gvMain.Rows(index).Cells(2).Text.ToString(), String) 'gvr.Cells(2).Text
        '                'gvMail_Relative(gvr)
        '                DoAction("btnPrint")
        '            Case "Detail"
        '                Dim strCompID As String = gvMain.DataKeys(selectedRows(gvMain))("CompID").ToString() 'CType(gvMain.Rows(index).Cells(1).Text.ToString(), String)    'gvr.Cells(1).Text
        '                Dim strEmpID As String = gvMain.DataKeys(selectedRows(gvMain))("EmpID").ToString() 'CType(gvMain.Rows(index).Cells(2).Text.ToString(), String) 'gvr.Cells(2).Text
        '                'gvMail_Relative(gvr)
        '                DoAction("btnActionC")
        '        End Select
        '    End If
        'End If
    End Sub
    Protected Sub gvMain_RowUpdating(sender As Object, e As System.Web.UI.WebControls.GridViewUpdateEventArgs)
        '要寫update code
        '==== 離開編輯模式 ====
        gvMain.EditIndex = -1
    End Sub

End Class
