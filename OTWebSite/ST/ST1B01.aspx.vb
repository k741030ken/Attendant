'****************************************************
'功能說明：員工調兼資料維護(EmpAddition)維護-新增
'建立人員：Weicheng
'建立日期：2014/11/07
'****************************************************
Imports System.Data

Partial Class ST_ST1B01
    Inherits PageBase


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            'Me.txtEmpID.Attributes.Add("onblur", "return funAction('btnQueryEmp');")

            '公司
            'Bsp.Utility.FillHRCompany(ddlCompID)   '20150709 wei del



            '兼任公司
            Bsp.Utility.FillHRCompany(ddlEmpAdditionCompID)

            '兼任狀態
            HR3000.FillEmpAdditionReason(ddlEmpAdditionReason)

            '兼任主管任用方式
            ddlEmpAdditionBossType.Items.Insert(0, New ListItem("---請選擇---", ""))
            ddlEmpAdditionBossType.Items.Insert(1, New ListItem("主要", "1"))
            ddlEmpAdditionBossType.Items.Insert(2, New ListItem("兼任", "2"))

        End If
    End Sub
    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then

            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)
            Dim objSC As New SC

            ViewState.Item("CompID") = ht("SelectedCompID").ToString()
            hidCompID.Value = ViewState.Item("CompID")
            'ddlCompID.SelectedValue = ViewState.Item("CompID")
            lblCompID_S.Text = ht("SelectedCompID").ToString() & "-" & objSC.GetCompName(ht("SelectedCompID").ToString()).Rows(0).Item("CompName").ToString    '20150709 wei add
            ViewState.Item("PageNo") = ht("PageNo").ToString()
            ViewState.Item("EmpID") = ht("SelectedEmpID").ToString()
            ViewState.Item("EmpName") = ht("SelectedEmpName").ToString()
            txtEmpID.Text = ViewState.Item("EmpID").ToString()
            txtEmpName.Text = ViewState.Item("EmpName").ToString()
            hidEmpID.Value = ViewState.Item("EmpID")

            '兼任公司
            ddlEmpAdditionCompID.SelectedValue = ViewState.Item("CompID")
            '兼任部門、科組課
            ucSelectEmpAdditionHROrgan.LoadData(ViewState.Item("CompID"))
            ddlEmpAdditonFlowOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
            QueryEmpData()

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
        'EmpAddition
        Dim beEmpAddition As New beEmpAddition.Row()
        Dim bsEmpAddition As New beEmpAddition.Service()
        'EmpAdditionDetail
        Dim beEmpAdditionDetail As New beEmpAdditionDetail.Row()
        Dim bsEmpAdditionDetail As New beEmpAdditionDetail.Service()
        'EmpAdditionLog
        Dim beEmpAdditionLog As New beEmpAdditionLog.Row()
        Dim bsEmpAdditionLog As New beEmpAdditionLog.Service()
        Dim objHR As New HR
        Dim objHR1600 As New HR1600

        '取得輸入資料
        'EmpAddition

        beEmpAddition.ValidDate.Value = ucValidDate.DateText
        beEmpAddition.CompID.Value = ViewState.Item("CompID")   'ddlCompID.SelectedValue    20150709 wei modify
        beEmpAddition.EmpID.Value = txtEmpID.Text.Trim
        beEmpAddition.AddCompID.Value = ddlEmpAdditionCompID.SelectedValue
        beEmpAddition.AddDeptID.Value = ucSelectEmpAdditionHROrgan.SelectedDeptID
        beEmpAddition.AddOrganID.Value = ucSelectEmpAdditionHROrgan.SelectedOrganID
        beEmpAddition.AddFlowOrganID.Value = ddlEmpAdditonFlowOrganID.SelectedValue
        beEmpAddition.Reason.Value = ddlEmpAdditionReason.SelectedValue
        beEmpAddition.FileNo.Value = txtFileNO.Text.Trim
        beEmpAddition.Remark.Value = txtEmpAdditionRemark.Text.Trim
        beEmpAddition.IsBoss.Value = IIf(chkEmpAdditionIsBoss.Checked, "1", "0")
        beEmpAddition.IsSecBoss.Value = IIf(chkEmpAdditionIsSecBoss.Checked, "1", "0")
        beEmpAddition.IsGroupBoss.Value = IIf(chkEmpAdditionIsGroupBoss.Checked, "1", "0")
        beEmpAddition.IsSecGroupBoss.Value = IIf(chkEmpAdditionIsSecGroupBoss.Checked, "1", "0")
        beEmpAddition.BossType.Value = ddlEmpAdditionBossType.SelectedValue
        beEmpAddition.CreateDate.Value = Now
        beEmpAddition.CreateComp.Value = UserProfile.ActCompID
        beEmpAddition.CreateID.Value = UserProfile.ActUserID
        beEmpAddition.LastChgDate.Value = Now
        beEmpAddition.LastChgComp.Value = UserProfile.ActCompID
        beEmpAddition.LastChgID.Value = UserProfile.ActUserID

        'EmpAdditionDetail
        beEmpAdditionDetail.ValidDate.Value = ucValidDate.DateText
        beEmpAdditionDetail.CompID.Value = ViewState.Item("CompID") 'ddlCompID.SelectedValue    20150709 wei modify
        beEmpAdditionDetail.EmpID.Value = txtEmpID.Text.Trim
        beEmpAdditionDetail.AddCompID.Value = ddlEmpAdditionCompID.SelectedValue
        beEmpAdditionDetail.AddDeptID.Value = ucSelectEmpAdditionHROrgan.SelectedDeptID
        beEmpAdditionDetail.AddOrganID.Value = ucSelectEmpAdditionHROrgan.SelectedOrganID
        beEmpAdditionDetail.AddFlowOrganID.Value = ddlEmpAdditonFlowOrganID.SelectedValue
        beEmpAdditionDetail.Reason.Value = ddlEmpAdditionReason.SelectedValue
        beEmpAdditionDetail.FileNo.Value = txtFileNO.Text.Trim
        beEmpAdditionDetail.Remark.Value = txtEmpAdditionRemark.Text.Trim
        beEmpAdditionDetail.IsBoss.Value = IIf(chkEmpAdditionIsBoss.Checked, "1", "0")
        beEmpAdditionDetail.IsSecBoss.Value = IIf(chkEmpAdditionIsSecBoss.Checked, "1", "0")
        beEmpAdditionDetail.IsGroupBoss.Value = IIf(chkEmpAdditionIsGroupBoss.Checked, "1", "0")
        beEmpAdditionDetail.IsSecGroupBoss.Value = IIf(chkEmpAdditionIsSecGroupBoss.Checked, "1", "0")
        beEmpAdditionDetail.BossType.Value = ddlEmpAdditionBossType.SelectedValue
        beEmpAdditionDetail.CreateDate.Value = Now
        beEmpAdditionDetail.CreateComp.Value = UserProfile.ActCompID
        beEmpAdditionDetail.CreateID.Value = UserProfile.ActUserID
        beEmpAdditionDetail.LastChgDate.Value = Now
        beEmpAdditionDetail.LastChgComp.Value = UserProfile.ActCompID
        beEmpAdditionDetail.LastChgID.Value = UserProfile.ActUserID

        'EmpAdditionLog
        beEmpAdditionLog.ValidDate.Value = ucValidDate.DateText
        beEmpAdditionLog.CompID.Value = ViewState.Item("CompID")    'ddlCompID.SelectedValue    '20150709 wei modify
        beEmpAdditionLog.EmpID.Value = txtEmpID.Text.Trim
        beEmpAdditionLog.AddCompID.Value = ddlEmpAdditionCompID.SelectedValue
        beEmpAdditionLog.AddDeptID.Value = ucSelectEmpAdditionHROrgan.SelectedDeptID
        beEmpAdditionLog.AddOrganID.Value = ucSelectEmpAdditionHROrgan.SelectedOrganID
        beEmpAdditionLog.AddFlowOrganID.Value = ddlEmpAdditonFlowOrganID.SelectedValue
        beEmpAdditionLog.Reason.Value = ddlEmpAdditionReason.SelectedValue
        beEmpAdditionLog.FileNo.Value = txtFileNO.Text.Trim
        beEmpAdditionLog.Remark.Value = txtEmpAdditionRemark.Text.Trim
        beEmpAdditionLog.IsBoss.Value = IIf(chkEmpAdditionIsBoss.Checked, "1", "0")
        beEmpAdditionLog.IsSecBoss.Value = IIf(chkEmpAdditionIsSecBoss.Checked, "1", "0")
        beEmpAdditionLog.IsGroupBoss.Value = IIf(chkEmpAdditionIsGroupBoss.Checked, "1", "0")
        beEmpAdditionLog.IsSecGroupBoss.Value = IIf(chkEmpAdditionIsSecGroupBoss.Checked, "1", "0")
        beEmpAdditionLog.BossType.Value = ddlEmpAdditionBossType.SelectedValue
        beEmpAdditionLog.CreateDate.Value = Now
        beEmpAdditionLog.CreateComp.Value = UserProfile.ActCompID
        beEmpAdditionLog.CreateID.Value = UserProfile.ActUserID
        beEmpAdditionLog.LastChgDate.Value = Now
        beEmpAdditionLog.LastChgComp.Value = UserProfile.ActCompID
        beEmpAdditionLog.LastChgID.Value = UserProfile.ActUserID

        '檢查資料是否存在
        If bsEmpAddition.IsDataExists(beEmpAddition) Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00010", "")
            Return False
        End If
        If bsEmpAdditionDetail.IsDataExists(beEmpAdditionDetail) Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00010", "")
            Return False
        End If
        If bsEmpAdditionLog.IsDataExists(beEmpAdditionLog) Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00010", "")
            Return False
        End If

        '儲存資料
        Try
            Return objHR1600.AddEmpAddition(beEmpAddition, beEmpAdditionDetail, beEmpAdditionLog)
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, "[CC0201_99]" & Bsp.Utility.getMessage("E_00000") & Bsp.Utility.getInnerException(Me.FunID, ex))
            Return False
        End Try
    End Function



    Private Function funCheckData() As Boolean
        Dim objHR As New HR()
        Dim strWhere As String = ""
        Dim strValue As String

        Dim strNowDate As String


        '員工編號
        If Not Len(txtEmpID.Text.Trim) = 6 Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00031", "員工編號長度錯誤")
            txtEmpID.Focus()
            Return False
        End If

        '現任公司
        '20150709 wei del
        'If ddlCompID.SelectedValue = "" Then
        '    Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblCompID.Text)
        '    ddlCompID.Focus()
        '    Return False
        'End If

        '生效日期
        strValue = ucValidDate.DateText
        If strValue = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblModifyDate.Text)
            ucValidDate.Focus()
            Return False
        Else
            If Not IsDate(strValue) Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00070", lblModifyDate.Text)
                ucValidDate.Focus()
                Return False
            Else
                If CDate(strValue) < CDate(hidEmpDate.Value) Then
                    Bsp.Utility.ShowMessage(Me, "生效日期：輸入錯誤，必須大於到職日")
                    ucValidDate.Focus()
                    Return False
                End If
            End If
        End If
        '不允許輸入比系統日大的調兼資料
        strNowDate = objHR.Get_DB_NowDateTime(0)
        If CDate(strValue) > CDate(strNowDate) Then
            Bsp.Utility.ShowMessage(Me, "生效日期：輸入錯誤，必須小於或等於系統日")
            ucValidDate.Focus()
            Return False
        End If

        '狀態
        If ddlEmpAdditionReason.SelectedValue = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "兼任狀態")
            ddlEmpAdditionReason.Focus()
            Return False
        End If

        '人令
        If txtFileNO.Text.Trim() = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "人令")
            txtFileNO.Focus()
            Return False
        Else
            If Bsp.Utility.getStringLength(txtFileNO.Text.Trim()) > txtFileNO.MaxLength Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00040", lblFileNO.Text, txtFileNO.MaxLength)
                txtFileNO.Focus()
                Return False
            End If
        End If

        '兼任公司
        If ddlEmpAdditionCompID.SelectedValue = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "兼任公司")
            ddlEmpAdditionCompID.Focus()
            Return False
        End If

        '兼任部門
        If ucSelectEmpAdditionHROrgan.SelectedDeptID = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "兼任部門")
            ucSelectEmpAdditionHROrgan.Focus()
            Return False
        End If

        '兼任科組課
        If ucSelectEmpAdditionHROrgan.SelectedOrganID = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "兼任科組課")
            ucSelectEmpAdditionHROrgan.Focus()
            Return False
        End If

        '最小簽核單位
        If ddlEmpAdditonFlowOrganID.SelectedValue = "" And chkEmpAdditionIsGroupBoss.Checked Then   '20150722 wei modify
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblEmpAdditionFlowOrganID.Text)
            ddlEmpAdditonFlowOrganID.Focus()
            Return False
        End If

        '主管
        If ddlEmpAdditionBossType.SelectedValue <> "" Then
            If Not chkEmpAdditionIsBoss.Checked And Not chkEmpAdditionIsSecBoss.Checked And Not chkEmpAdditionIsGroupBoss.Checked And Not chkEmpAdditionIsSecGroupBoss.Checked Then
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", "已選擇主管任用方式，請至少選擇一個單位(副)主或簽核單位(副)主管")
                chkEmpAdditionIsBoss.Focus()
                Return False
            End If
        Else
            If chkEmpAdditionIsBoss.Checked Or chkEmpAdditionIsSecBoss.Checked Or chkEmpAdditionIsGroupBoss.Checked Or chkEmpAdditionIsSecGroupBoss.Checked Then
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", "已選擇單位(副)主或簽核單位(副)主管，請選擇主管任用方式")
                chkEmpAdditionIsBoss.Focus()
                Return False
            End If
        End If

        '備註
        If Bsp.Utility.getStringLength(txtEmpAdditionRemark.Text.Trim()) > txtEmpAdditionRemark.MaxLength Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00040", lblEmpAdditionRemark.Text, txtEmpAdditionRemark.MaxLength)
            txtEmpAdditionRemark.Focus()
            Return False
        End If

        '檢查員工主檔
        strWhere = " And CompID = " & Bsp.Utility.Quote(ViewState.Item("CompID")) & " And EmpID = " & Bsp.Utility.Quote(txtEmpID.Text.Trim) & " And WorkStatus='1' "    '20150709 wei modify
        If Not objHR.IsDataExists("Personal", strWhere) Then
            Bsp.Utility.ShowMessage(Me, "員工主檔不存在或不在職！")
            txtEmpID.Focus()
            Return False
        End If

        strWhere = " And convert(char(10),ValidDate,111) = " & Bsp.Utility.Quote(ucValidDate.DateText)
        strWhere = strWhere & " And CompID = " & Bsp.Utility.Quote(ViewState.Item("CompID")) & " And EmpID = " & Bsp.Utility.Quote(txtEmpID.Text.Trim)  '20150709 wei modify
        strWhere = strWhere & " And AddCompID = " & Bsp.Utility.Quote(ddlEmpAdditionCompID.SelectedValue)
        strWhere = strWhere & " And  AddDeptID = " & Bsp.Utility.Quote(ucSelectEmpAdditionHROrgan.SelectedDeptID)
        strWhere = strWhere & " And  AddOrganID = " & Bsp.Utility.Quote(ucSelectEmpAdditionHROrgan.SelectedOrganID)
        If objHR.IsDataExists("EmpAdditionDetail", strWhere) Then
            Bsp.Utility.ShowMessage(Me, "資料已存在，請勿重複新增！")
            ucValidDate.Focus()
            Return False
        End If

        '20150709 wei add 不允許輸入相同公司的調兼現況檔
        If ddlEmpAdditionReason.SelectedValue = "1" Then
            strWhere = " And CompID = " & Bsp.Utility.Quote(ViewState.Item("CompID")) & " And EmpID = " & Bsp.Utility.Quote(txtEmpID.Text.Trim)  '20150709 wei modify
            strWhere = strWhere & " And AddCompID = " & Bsp.Utility.Quote(ddlEmpAdditionCompID.SelectedValue)
            strWhere = strWhere & " And  AddDeptID = " & Bsp.Utility.Quote(ucSelectEmpAdditionHROrgan.SelectedDeptID)
            strWhere = strWhere & " And  AddOrganID = " & Bsp.Utility.Quote(ucSelectEmpAdditionHROrgan.SelectedOrganID)
            If objHR.IsDataExists("EmpAddition", strWhere) Then
                Bsp.Utility.ShowMessage(Me, "調兼現況檔已有相同資料，請勿重複新增！")
                ucValidDate.Focus()
                Return False
            End If
        End If


        Return True
    End Function


#Region "Comp"    '公司

    Protected Sub ddlEmpAdditionCompID_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlEmpAdditionCompID.SelectedIndexChanged
        '兼任部門、科組課
        ucSelectEmpAdditionHROrgan.LoadData(ddlEmpAdditionCompID.SelectedValue)
        ddlEmpAdditonFlowOrganID.Items.Clear()
        ddlEmpAdditonFlowOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
        UdpEmpAdditionDeptID.Update()
        UpdEmpAdditionFlowOrgnaID.Update()

    End Sub
#End Region

#Region "ucSelectHROrgan"    '

    Protected Sub ucSelectEmpadditionHROrgan_ucSelectOrganIDSelectedIndexChangedHandler_SelectChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles ucSelectEmpAdditionHROrgan.ucSelectOrganIDSelectedIndexChangedHandler_SelectChange
        Dim objHR As New HR
        Dim objHR3000 As New HR3000

        '最小簽核單位
        GetEmpAdditionFlowOrganID()

    End Sub
#End Region


    Public Overrides Sub DoModalReturn(ByVal returnValue As String)

        Dim strSql As String = ""

        If returnValue <> "" Then
            Dim aryData() As String = returnValue.Split(":")

            Select Case aryData(0)
                Case "ucSelectPosition"


                Case "ucSelectWorkType"


            End Select
        End If
    End Sub




#Region "最小簽核單位"
    Public Sub GetEmpAdditionFlowOrganID()
        Dim objHR3000 As New HR3000
        Dim strWhere As String
        strWhere = "where OrganID = '" & ucSelectEmpAdditionHROrgan.SelectedOrganID & "'"
        Dim intCnt As Integer = 0
        Using dt As Data.DataTable = objHR3000.GetFlowOrganID(ddlEmpAdditionCompID.SelectedValue, strWhere).Tables(0)
            If dt.Rows.Count > 0 Then
                With ddlEmpAdditonFlowOrganID
                    If dt.Rows.Item(0)("FlowOrganID").ToString.Trim = "" Then
                        .DataSource = dt
                        .DataTextField = "FullOrganName"
                        .DataValueField = "OrganID"
                        .DataBind()
                    Else
                        Dim aryValue() As String = dt.Rows(0)("FlowOrganID").ToString().Trim.Split("|")
                        For intCnt = 0 To UBound(aryValue)
                            If intCnt = 0 Then
                                strWhere = Bsp.Utility.Quote(aryValue(intCnt).ToString().Trim)
                            Else
                                strWhere = strWhere & "," & Bsp.Utility.Quote(aryValue(intCnt).ToString().Trim)
                            End If
                        Next
                        strWhere = "Where OrganID In (" & strWhere & ")"
                    End If
                End With
            End If
        End Using
        If intCnt > 0 Then
            Using dt As Data.DataTable = objHR3000.GetFlowOrganID(ddlEmpAdditionCompID.SelectedValue, strWhere).Tables(0)
                With ddlEmpAdditonFlowOrganID
                    .DataSource = dt
                    .DataTextField = "FullOrganName"
                    .DataValueField = "OrganID"
                    .DataBind()
                End With
            End Using
        End If
        ddlEmpAdditonFlowOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
        UpdEmpAdditionFlowOrgnaID.Update()
    End Sub
#End Region

#Region "QueryEmp"
    Public Sub QueryEmpData()
        Dim objHR3000 As New HR3000

        Dim strEmpID As String


        strEmpID = txtEmpID.Text.ToUpper.Trim()
        Try
            '個人資料
            Using dt As DataTable = objHR3000.GetEmpDataByHR3000(ViewState.Item("CompID"), strEmpID)
                If dt.Rows.Count > 0 Then
                    If dt.Rows(0)("WorkStatus") <> "1" Then
                        Bsp.Utility.ShowMessage(Me, "該員工不在職！")
                        ClearData()
                        Exit Sub
                    End If
                    hidEmpID.Value = strEmpID
                    hidIDNo.Value = dt.Rows.Item(0)("IDNo").ToString()
                    '公司
                    'Bsp.Utility.SetSelectedIndex(ddlCompID, dt.Rows(0)("CompID").ToString())   '20150709 wei del
                    hidCompID.Value = dt.Rows(0)("CompID").ToString()


                    '部門
                    ucSelectEmpAdditionHROrgan.LoadData(dt.Rows(0)("CompID").ToString())
                    ucSelectEmpAdditionHROrgan.setDeptID(dt.Rows(0)("CompID").ToString(), dt.Rows(0)("DeptID").ToString(), "N")

                    '科組課
                    ucSelectEmpAdditionHROrgan.setOrganID(dt.Rows(0)("CompID").ToString(), dt.Rows(0)("OrganID").ToString(), "N")

                    '最小簽核單位
                    GetEmpAdditionFlowOrganID()

                    hidEmpDate.Value = dt.Rows(0)("EmpDate").ToString()

                End If
            End Using

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & "QueryEmp", ex)
        End Try
    End Sub
#End Region

    Private Sub ClearData()
        'EmployeeWait
        ucValidDate.DateText = ""
        ddlEmpAdditionReason.SelectedIndex = -1
        txtFileNO.Text = ""
        ddlEmpAdditionCompID.SelectedValue = ViewState.Item("CompID")
        '部門、科組課
        ucSelectEmpAdditionHROrgan.LoadData(ViewState.Item("CompID"))
        ddlEmpAdditonFlowOrganID.Items.Clear()
        ddlEmpAdditonFlowOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
        ddlEmpAdditionBossType.SelectedValue = ""
        chkEmpAdditionIsBoss.Checked = False
        chkEmpAdditionIsSecBoss.Checked = False
        chkEmpAdditionIsGroupBoss.Checked = False
        chkEmpAdditionIsSecGroupBoss.Checked = False
        txtEmpAdditionRemark.Text = ""
        QueryEmpData()
    End Sub
End Class
