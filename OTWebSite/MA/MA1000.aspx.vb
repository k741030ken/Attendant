'****************************************************
'功能說明：綜合查詢
'建立人員：BeatriceCheng
'建立日期：2015.06.23
'****************************************************
Imports System.Data

Partial Class MA_MA1000
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objSC As New SC
            Dim objMA1 As New MA1()

            ViewState.Item("QueryFlag") = False

            Using dt As DataTable = objMA1.GetQueryFlag(UserProfile.ActCompID, UserProfile.ActUserID, UserProfile.SelectCompRoleID, UserProfile.LoginSysID)
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        If dr.Item(0) = "0" Then
                            TrGradeInfo.Visible = True
                            TrCommGrade.Visible = True
                            TrGrade.Visible = True
                        End If
                        If dr.Item(0) = "1" Then
                            TrSalaryInfo.Visible = True
                            TrSalary.Visible = True
                        End If
                    Next
                    ViewState.Item("QueryFlag") = True
                End If
            End Using

            ddlCompID.Visible = False
            ucSelectEmpID.ShowCompRole = False

            If UserProfile.SelectCompRoleID = "ALL" Then
                ddlCompID.Visible = True
                Bsp.Utility.FillHRCompany(ddlCompID)
                ddlCompID.Items.Insert(0, New ListItem("全金控", "0"))
                lblCompRoleID.Visible = False
                TrPosWork.Visible = False
            Else
                ddlCompID.Visible = False
                lblCompRoleID.Text = UserProfile.SelectCompRoleName
                lblCompRoleID.Visible = True

                ucSelectEmpID.SelectCompID = UserProfile.SelectCompRoleID
            End If

            If UserProfile.SelectCompRoleID = "SPHSC1" Then
                lblOfficeLoginFlag.Visible = True
                ddlOfficeLoginFlag.Visible = True
            Else
                lblOfficeLoginFlag.Visible = False
                ddlOfficeLoginFlag.Visible = False
            End If

            ViewState.Item("OrgTypeColors") = New List(Of ArrayList)()
            ViewState.Item("DeptColors") = New List(Of ArrayList)()
            ViewState.Item("LogDeptColors") = New List(Of ArrayList)()
            ViewState.Item("AddDeptColors") = New List(Of ArrayList)()
            ViewState.Item("OrgRangeColors") = New List(Of ArrayList)()

            txtAgeB.Attributes.Add("onchange", "ChkValue('txtAgeB', 'txtAgeE', 'lblAgeNotice')")
            txtAgeE.Attributes.Add("onchange", "ChkValue('txtAgeB', 'txtAgeE', 'lblAgeNotice')")

            txtEmpSeniorityB.Attributes.Add("onchange", "ChkValue('txtEmpSeniorityB', 'txtEmpSeniorityE', 'lblEmpSeniorityNotice2')")
            txtEmpSeniorityE.Attributes.Add("onchange", "ChkValue('txtEmpSeniorityB', 'txtEmpSeniorityE', 'lblEmpSeniorityNotice2')")
            txtSinopacEmpSeniorityB.Attributes.Add("onchange", "ChkValue('txtSinopacEmpSeniorityB', 'txtSinopacEmpSeniorityE', 'lblSinopacEmpSeniorityNotice2')")
            txtSinopacEmpSeniorityE.Attributes.Add("onchange", "ChkValue('txtSinopacEmpSeniorityB', 'txtSinopacEmpSeniorityE', 'lblSinopacEmpSeniorityNotice2')")
            txtEmpSenOrgTypeB.Attributes.Add("onchange", "ChkValue('txtEmpSenOrgTypeB', 'txtEmpSenOrgTypeE', 'lblEmpSenOrgTypeNotice2')")
            txtEmpSenOrgTypeE.Attributes.Add("onchange", "ChkValue('txtEmpSenOrgTypeB', 'txtEmpSenOrgTypeE', 'lblEmpSenOrgTypeNotice2')")
            txtEmpConSenOrgTypeB.Attributes.Add("onchange", "ChkValue('txtEmpConSenOrgTypeB', 'txtEmpConSenOrgTypeE', 'lblEmpConSenOrgTypeNotice2')")
            txtEmpConSenOrgTypeE.Attributes.Add("onchange", "ChkValue('txtEmpConSenOrgTypeB', 'txtEmpConSenOrgTypeE', 'lblEmpConSenOrgTypeNotice2')")
            txtEmpSenOrgTypeFlowB.Attributes.Add("onchange", "ChkValue('txtEmpSenOrgTypeFlowB', 'txtEmpSenOrgTypeFlowE', 'lblEmpSenOrgTypeFlowNotice2')")
            txtEmpSenOrgTypeFlowE.Attributes.Add("onchange", "ChkValue('txtEmpSenOrgTypeFlowB', 'txtEmpSenOrgTypeFlowE', 'lblEmpSenOrgTypeFlowNotice2')")
            txtEmpSenRankB.Attributes.Add("onchange", "ChkValue('txtEmpSenRankB', 'txtEmpSenRankE', 'lblEmpSenRankNotice2')")
            txtEmpSenRankE.Attributes.Add("onchange", "ChkValue('txtEmpSenRankB', 'txtEmpSenRankE', 'lblEmpSenRankNotice2')")
            txtEmpSenPositionB.Attributes.Add("onchange", "ChkValue('txtEmpSenPositionB', 'txtEmpSenPositionE', 'lblEmpSenPositionNotice2')")
            txtEmpSenPositionE.Attributes.Add("onchange", "ChkValue('txtEmpSenPositionB', 'txtEmpSenPositionE', 'lblEmpSenPositionNotice2')")
            txtEmpConSenPositionB.Attributes.Add("onchange", "ChkValue('txtEmpConSenPositionB', 'txtEmpConSenPositionE', 'lblEmpConSenPositionNotice2')")
            txtEmpConSenPositionE.Attributes.Add("onchange", "ChkValue('txtEmpConSenPositionB', 'txtEmpConSenPositionE', 'lblEmpConSenPositionNotice2'))")
            txtEmpSenWorkTypeB.Attributes.Add("onchange", "ChkValue('txtEmpSenWorkTypeB', 'txtEmpSenWorkTypeE', 'lblEmpSenWorkTypeNotice2')")
            txtEmpSenWorkTypeE.Attributes.Add("onchange", "ChkValue('txtEmpSenWorkTypeB', 'txtEmpSenWorkTypeE', 'lblEmpSenWorkTypeNotice2')")
            txtEmpConSenWorkTypeB.Attributes.Add("onchange", "ChkValue('txtEmpConSenWorkTypeB', 'txtEmpConSenWorkTypeE', 'lblEmpConSenWorkTypeNotice2')")
            txtEmpConSenWorkTypeE.Attributes.Add("onchange", "ChkValue('txtEmpConSenWorkTypeB', 'txtEmpConSenWorkTypeE', 'lblEmpConSenWorkTypeNotice2')")

            ddlRankB.Attributes.Add("onchange", "ChkValue('ddlRankB', 'ddlRankE', 'lblRankNotice')")
            ddlRankE.Attributes.Add("onchange", "ChkValue('ddlRankB', 'ddlRankE', 'lblRankNotice')")
            ddlTitleB.Attributes.Add("onchange", "ChkValue('ddlTitleB', 'ddlTitleE', 'lblTitleNotice')")
            ddlTitleE.Attributes.Add("onchange", "ChkValue('ddlTitleB', 'ddlTitleE', 'lblTitleNotice')")
            ddlHoldingRankB.Attributes.Add("onchange", "ChkValue('ddlHoldingRankB', 'ddlHoldingRankE', 'lblHoldingRankNotice')")
            ddlHoldingRankE.Attributes.Add("onchange", "ChkValue('ddlHoldingRankB', 'ddlHoldingRankE', 'lblHoldingRankNotice')")
            ddlHoldingTitleB.Attributes.Add("onchange", "ChkValue('ddlHoldingTitleB', 'ddlHoldingTitleE', 'lblHoldingTitleNotice')")
            ddlHoldingTitleE.Attributes.Add("onchange", "ChkValue('ddlHoldingTitleB', 'ddlHoldingTitleE', 'lblHoldingTitleNotice')")

            txtLastWorkYearE.Attributes.Add("onchange", "ChkValue('txtLastWorkYearB', 'txtLastWorkYearE', 'lblLastWorkYearNotice2')")
            txtLastWorkYearE.Attributes.Add("onchange", "ChkValue('txtLastWorkYearB', 'txtLastWorkYearE', 'lblLastWorkYearNotice2')")

            txtYearSalaryB.Attributes.Add("onchange", "ChkValue('txtYearSalaryB', 'txtYearSalaryE', 'lblYearSalaryNotice')")
            txtYearSalaryE.Attributes.Add("onchange", "ChkValue('txtYearSalaryB', 'txtYearSalaryE', 'lblYearSalaryNotice')")
            txtMonthSalaryB.Attributes.Add("onchange", "ChkValue('txtMonthSalaryB', 'txtMonthSalaryE', 'lblMonthSalaryNotice')")
            txtMonthSalaryE.Attributes.Add("onchange", "ChkValue('txtMonthSalaryB', 'txtMonthSalaryE', 'lblMonthSalaryNotice')")

            subLoadData()
        Else
            If Request.Params("__EVENTTARGET") <> "ddlCompID" Then
                subReLoadColor(ddlOrgType)
                subReLoadColor(ddlDeptID)
                subReLoadColor(ddlLogDept)
                subReLoadColor(ddlEmpAdditionDept)
                subReLoadColor(ddlOrgRange)
            End If
            Bsp.Utility.RunClientScript(Me, "ReLoadChkValue()")
        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnQuery"     '查詢
                If funCheckData() Then
                    ViewState.Item("DoQuery") = "Y"
                    DoQuery()
                End If
            Case "btnDownload"  '下傳
                DoDownload()
            Case "btnActionX"   '清除
                DoClear()
        End Select
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            For i As Integer = 0 To ti.Args.Length - 1
                If TypeOf ti.Args(i) Is Object() Then
                    Dim Params As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args(i))
                    For Each strKey As String In Params.Keys
                        Dim ctl As Control = Me.FindControl(strKey)

                        If TypeOf ctl Is TextBox Then
                            CType(ctl, TextBox).Text = Params(strKey).ToString()
                        ElseIf TypeOf ctl Is HiddenField Then
                            CType(ctl, HiddenField).Value = Params(strKey).ToString()
                        ElseIf TypeOf ctl Is DropDownList Then
                            Bsp.Utility.SetSelectedIndex(CType(ctl, DropDownList), Params(strKey).ToString())
                        ElseIf TypeOf ctl Is Component_ucCalender Then
                            CType(ctl, Component_ucCalender).DateText = Params(strKey).ToString()
                        End If
                    Next

                    If Params.ContainsKey("ddlRankB") Then
                        If Params("ddlRankB").ToString() <> "" Then
                            ddlRank_Changed(ddlRankB, Nothing)

                            If Params.ContainsKey("ddlTitleB") Then
                                ddlTitleB.SelectedValue = Params("ddlTitleB").ToString()
                            End If
                        End If
                    End If

                    If Params.ContainsKey("ddlRankE") Then
                        If Params("ddlRankE").ToString() <> "" Then
                            ddlRank_Changed(ddlRankE, Nothing)

                            If Params.ContainsKey("ddlTitleE") Then
                                ddlTitleE.SelectedValue = Params("ddlTitleE").ToString()
                            End If
                        End If
                    End If

                    If Params.ContainsKey("ddlHoldingRankB") Then
                        If Params("ddlHoldingRankB").ToString() <> "" Then
                            ddlHoldingRank_Changed(ddlHoldingRankB, Nothing)

                            If Params.ContainsKey("ddlHoldingTitleB") Then
                                ddlHoldingTitleB.SelectedValue = Params("ddlHoldingTitleB").ToString()
                            End If
                        End If
                    End If

                    If Params.ContainsKey("ddlHoldingRankE") Then
                        If Params("ddlHoldingRankE").ToString() <> "" Then
                            ddlHoldingRank_Changed(ddlHoldingRankE, Nothing)

                            If Params.ContainsKey("ddlHoldingTitleE") Then
                                ddlHoldingTitleE.SelectedValue = Params("ddlHoldingTitleE").ToString()
                            End If
                        End If
                    End If

                    If Params.ContainsKey("ddlDeptID") Then
                        If Params("ddlDeptID").ToString() <> "" Then
                            ddlDept_Changed(ddlDeptID, Nothing)

                            If Params.ContainsKey("ddlOrganID") Then
                                ddlOrganID.SelectedValue = Params("ddlOrganID").ToString()
                            End If
                        End If
                    End If

                    If Params.ContainsKey("hidPosition1") Then
                        If Params("hidPosition1").ToString() <> "" Then
                            Bsp.Utility.Position(ddlPosition1, "PositionID", Bsp.Enums.DisplayType.Full, " And PositionID in (" + Params("hidPosition1").ToString() + ")")

                            If Params.ContainsKey("ddlPosition1") Then
                                ddlPosition1.SelectedValue = Params("ddlPosition1").ToString()
                            End If
                        End If
                    End If

                    If Params.ContainsKey("hidPosition2") Then
                        If Params("hidPosition2").ToString() <> "" Then
                            Bsp.Utility.Position(ddlPosition2, "PositionID", Bsp.Enums.DisplayType.Full, " And PositionID in (" + Params("hidPosition2").ToString() + ")")

                            If Params.ContainsKey("ddlPosition2") Then
                                ddlPosition2.SelectedValue = Params("ddlPosition2").ToString()
                            End If
                        End If
                    End If

                    If Params.ContainsKey("hidWorkType1") Then
                        If Params("hidWorkType1").ToString() <> "" Then
                            Bsp.Utility.WorkType(ddlWorkType1, "WorkTypeID", Bsp.Enums.DisplayType.Full, " And WorkTypeID in (" + Params("hidWorkType1").ToString() + ")")

                            If Params.ContainsKey("ddlWorkType1") Then
                                ddlWorkType1.SelectedValue = Params("ddlWorkType1").ToString()
                            End If
                        End If
                    End If

                    If Params.ContainsKey("hidWorkType2") Then
                        If Params("hidWorkType2").ToString() <> "" Then
                            Bsp.Utility.WorkType(ddlWorkType2, "WorkTypeID", Bsp.Enums.DisplayType.Full, " And WorkTypeID in (" + Params("hidWorkType2").ToString() + ")")

                            If Params.ContainsKey("ddlWorkType2") Then
                                ddlWorkType2.SelectedValue = Params("ddlWorkType2").ToString()
                            End If
                        End If
                    End If

                    If Params.ContainsKey("ddlEmpAddition") Then
                        If Params("ddlEmpAddition").ToString() <> "" Then
                            ddlEmpAddition_Changed(Nothing, Nothing)

                            If Params.ContainsKey("ddlEmpAdditionComp") Then
                                If Params("ddlEmpAdditionComp").ToString() <> "" Then
                                    ddlEmpAdditionComp_Changed(Nothing, Nothing)
                                End If

                                If Params.ContainsKey("ddlEmpAdditionDept") Then
                                    ddlEmpAdditionDept.SelectedValue = Params("ddlEmpAdditionDept").ToString()
                                End If
                            End If
                        End If
                    End If

                    If Params.ContainsKey("hidSchool") Then
                        If Params("hidSchool").ToString() <> "" Then
                            Bsp.Utility.FillDDL(ddlSchool, "eHRMSDB", "School", "SchoolID", "Remark", Bsp.Utility.DisplayType.Full, "", "And SchoolID in (" & hidSchool.Value & ")")

                            If Params.ContainsKey("ddlSchool") Then
                                ddlSchool.SelectedValue = Params("ddlSchool").ToString()
                            End If
                        End If
                    End If

                    If Params.ContainsKey("ddlLogDept") Then
                        If Params("ddlLogDept").ToString() <> "" Then
                            ddlDept_Changed(ddlLogDept, Nothing)

                            If Params.ContainsKey("ddlLogOrgan") Then
                                ddlLogOrgan.SelectedValue = Params("ddlLogOrgan").ToString()
                            End If
                        End If
                    End If

                    If Params.ContainsKey("ddlLogRank") Then
                        If Params("ddlLogRank").ToString() <> "" Then
                            ddlRank_Changed(ddlLogRank, Nothing)

                            If Params.ContainsKey("ddlLogTitle") Then
                                ddlLogTitle.SelectedValue = Params("ddlLogTitle").ToString()
                            End If
                        End If
                    End If

                    If Params.ContainsKey("hidCategory") Then
                        If Params("hidCategory").ToString() <> "" Then
                            Bsp.Utility.FillDDL(ddlCategory, "eHRMSDB", "License", "CategoryID", "LicenseName", Bsp.Utility.DisplayType.Full, "", "And CategoryID in (" & hidCategory.Value & ")")

                            If Params.ContainsKey("ddlCategory") Then
                                ddlCategory.SelectedValue = Params("ddlCategory").ToString()
                            End If
                        End If
                    End If
                End If
            Next

            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

            If ht.ContainsKey("PageNo") Then pcMain.PageNo = Convert.ToInt32(ht("PageNo"))
            If ht.ContainsKey("DoQuery") Then
                If ht("DoQuery").ToString() = "Y" Then
                    ViewState.Item("DoQuery") = "Y"
                    DoQuery()
                End If
            End If
        End If
    End Sub

    Private Function funCheckData() As Boolean
        Dim objMA1 As New MA1()
        Dim strValueB As String = ""
        Dim strValueE As String = ""
        Dim strValue1 As String = ""
        Dim strValue2 As String = ""
        Dim intValue1 As Integer = 0
        Dim intValue2 As Integer = 0
        Dim lngValueB As Long = 0
        Dim lngValueE As Long = 0
        Dim douValueB As Double = 0
        Dim douValueE As Double = 0

        Dim strCompID As String = ddlCompID.SelectedValue
        If strCompID = "" Then
            strCompID = UserProfile.SelectCompRoleID
        End If

        '生日
        If Not funCheckDate(txtBirthDateB, txtBirthDateE, lblBirthDate.Text) Then
            Return False
        End If

        '年齡
        If txtAgeB.Text.Trim() <> "" Then
            If Not Regex.IsMatch(txtAgeB.Text, "^[0-9]+$") Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00050", lblAge.Text)
                txtAgeB.Focus()
                Return False
            End If
        End If

        If txtAgeE.Text.Trim() <> "" Then
            If Not Regex.IsMatch(txtAgeE.Text, "^[0-9]+$") Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00050", lblAge.Text)
                txtAgeE.Focus()
                Return False
            End If
        End If

        strValueB = txtAgeB.Text
        strValueE = txtAgeE.Text

        If strValueB <> "" And strValueE <> "" Then
            intValue1 = Bsp.Utility.ToInteger(strValue1)
            intValue2 = Bsp.Utility.ToInteger(strValue2)

            If intValue1 > intValue2 Then
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", lblAge.Text & "輸入請由小到大")
                txtAgeB.Focus()
                Return False
            End If
        End If

        '工作期限
        If Not funCheckDate(txtIDExpireDateB, txtIDExpireDateE, lblIDExpireDate.Text) Then
            Return False
        End If

        '試用考核試滿日
        If Not funCheckDate(txtProbDateB, txtProbDateE, lblProbDate.Text) Then
            Return False
        End If

        '公司到職日
        If Not funCheckDate(txtEmpDateB, txtEmpDateE, lblEmpDate.Text) Then
            Return False
        End If

        '企業團到職日
        If Not funCheckDate(txtSinopacEmpDateB, txtSinopacEmpDateE, lblSinopacEmpDate.Text) Then
            Return False
        End If

        '企業團年資
        If txtSinopacEmpSeniorityB.Text.Trim() <> "" Then
            If Not Regex.IsMatch(txtSinopacEmpSeniorityB.Text, "^[0-9]+(.[0-9]{1,2})?$") Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00050", lblSinopacEmpSeniority.Text)
                txtSinopacEmpSeniorityB.Focus()
                Return False
            End If
        End If

        If txtSinopacEmpSeniorityE.Text.Trim() <> "" Then
            If Not Regex.IsMatch(txtSinopacEmpSeniorityE.Text, "^[0-9]+(.[0-9]{1,2})?$") Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00050", lblSinopacEmpSeniority.Text)
                txtSinopacEmpSeniorityE.Focus()
                Return False
            End If
        End If

        strValueB = txtSinopacEmpSeniorityB.Text
        strValueE = txtSinopacEmpSeniorityE.Text
        douValueB = 0
        douValueE = 0

        If strValueB <> "" And strValueE <> "" Then
            Double.TryParse(strValueB, douValueB)
            Double.TryParse(strValueE, douValueE)

            If douValueB > douValueE Then
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", lblSinopacEmpSeniority.Text & "輸入請由小到大")
                txtSinopacEmpSeniorityB.Focus()
                Return False
            End If
        End If

        '公司年資
        If txtEmpSeniorityB.Text.Trim() <> "" Then
            If Not Regex.IsMatch(txtEmpSeniorityB.Text, "^[0-9]+(.[0-9]{1,2})?$") Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00050", lblEmpSeniority.Text)
                txtEmpSeniorityB.Focus()
                Return False
            End If
        End If

        If txtEmpSeniorityE.Text.Trim() <> "" Then
            If Not Regex.IsMatch(txtEmpSeniorityE.Text, "^[0-9]+(.[0-9]{1,2})?$") Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00050", lblEmpSeniority.Text)
                txtEmpSeniorityE.Focus()
                Return False
            End If
        End If

        strValueB = txtEmpSeniorityB.Text
        strValueE = txtEmpSeniorityE.Text
        douValueB = 0
        douValueE = 0

        If strValueB <> "" And strValueE <> "" Then
            Double.TryParse(strValueB, douValueB)
            Double.TryParse(strValueE, douValueE)

            If douValueB > douValueE Then
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", lblEmpSeniority.Text & "輸入請由小到大")
                txtEmpSeniorityB.Focus()
                Return False
            End If
        End If

        '單位年資
        If txtEmpSenOrgTypeB.Text.Trim() <> "" Then
            If Not Regex.IsMatch(txtEmpSenOrgTypeB.Text, "^[0-9]+(.[0-9]{1,2})?$") Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00050", lblEmpSenOrgType.Text)
                txtEmpSenOrgTypeB.Focus()
                Return False
            End If
        End If

        If txtEmpSenOrgTypeE.Text.Trim() <> "" Then
            If Not Regex.IsMatch(txtEmpSenOrgTypeE.Text, "^[0-9]+(.[0-9]{1,2})?$") Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00050", lblEmpSenOrgType.Text)
                txtEmpSenOrgTypeE.Focus()
                Return False
            End If
        End If

        strValueB = txtEmpSenOrgTypeB.Text
        strValueE = txtEmpSenOrgTypeE.Text
        douValueB = 0
        douValueE = 0

        If strValueB <> "" And strValueE <> "" Then
            Double.TryParse(strValueB, douValueB)
            Double.TryParse(strValueE, douValueE)

            If douValueB > douValueE Then
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", lblEmpSenOrgType.Text & "輸入請由小到大")
                txtEmpSenOrgTypeB.Focus()
                Return False
            End If
        End If

        '單位年資(連續)
        If txtEmpConSenOrgTypeB.Text.Trim() <> "" Then
            If Not Regex.IsMatch(txtEmpConSenOrgTypeB.Text, "^[0-9]+(.[0-9]{1,2})?$") Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00050", lblEmpConSenOrgType.Text)
                txtEmpConSenOrgTypeB.Focus()
                Return False
            End If
        End If

        If txtEmpConSenOrgTypeE.Text.Trim() <> "" Then
            If Not Regex.IsMatch(txtEmpConSenOrgTypeE.Text, "^[0-9]+(.[0-9]{1,2})?$") Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00050", lblEmpConSenOrgType.Text)
                txtEmpConSenOrgTypeE.Focus()
                Return False
            End If
        End If

        strValueB = txtEmpConSenOrgTypeB.Text
        strValueE = txtEmpConSenOrgTypeE.Text
        douValueB = 0
        douValueE = 0

        If strValueB <> "" And strValueE <> "" Then
            Double.TryParse(strValueB, douValueB)
            Double.TryParse(strValueE, douValueE)

            If douValueB > douValueE Then
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", lblEmpConSenOrgType.Text & "輸入請由小到大")
                txtEmpConSenOrgTypeB.Focus()
                Return False
            End If
        End If

        '簽核單位年資
        If txtEmpSenOrgTypeFlowB.Text.Trim() <> "" Then
            If Not Regex.IsMatch(txtEmpSenOrgTypeFlowB.Text, "^[0-9]+(.[0-9]{1,2})?$") Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00050", lblEmpSenOrgTypeFlow.Text)
                txtEmpSenOrgTypeFlowB.Focus()
                Return False
            End If
        End If

        If txtEmpSenOrgTypeFlowE.Text.Trim() <> "" Then
            If Not Regex.IsMatch(txtEmpSenOrgTypeFlowE.Text, "^[0-9]+(.[0-9]{1,2})?$") Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00050", lblEmpSenOrgTypeFlow.Text)
                txtEmpSenOrgTypeFlowE.Focus()
                Return False
            End If
        End If

        strValueB = txtEmpSenOrgTypeFlowB.Text
        strValueE = txtEmpSenOrgTypeFlowE.Text
        douValueB = 0
        douValueE = 0

        If strValueB <> "" And strValueE <> "" Then
            Double.TryParse(strValueB, douValueB)
            Double.TryParse(strValueE, douValueE)

            If douValueB > douValueE Then
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", lblEmpSenOrgTypeFlow.Text & "輸入請由小到大")
                txtEmpSenOrgTypeFlowB.Focus()
                Return False
            End If
        End If

        '職等年資
        If txtEmpSenRankB.Text.Trim() <> "" Then
            If Not Regex.IsMatch(txtEmpSenRankB.Text, "^[0-9]+(.[0-9]{1,2})?$") Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00050", lblEmpSenRank.Text)
                txtEmpSenRankB.Focus()
                Return False
            End If
        End If

        If txtEmpSenRankE.Text.Trim() <> "" Then
            If Not Regex.IsMatch(txtEmpSenRankE.Text, "^[0-9]+(.[0-9]{1,2})?$") Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00050", lblEmpSenRank.Text)
                txtEmpSenRankE.Focus()
                Return False
            End If
        End If

        strValueB = txtEmpSenRankB.Text
        strValueE = txtEmpSenRankE.Text
        douValueB = 0
        douValueE = 0

        If strValueB <> "" And strValueE <> "" Then
            Double.TryParse(strValueB, douValueB)
            Double.TryParse(strValueE, douValueE)

            If douValueB > douValueE Then
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", lblEmpSenRank.Text & "輸入請由小到大")
                txtEmpSenRankB.Focus()
                Return False
            End If
        End If

        '職位年資
        If txtEmpSenPositionB.Text.Trim() <> "" Then
            If Not Regex.IsMatch(txtEmpSenPositionB.Text, "^[0-9]+(.[0-9]{1,2})?$") Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00050", lblEmpSenPosition.Text)
                txtEmpSenPositionB.Focus()
                Return False
            End If
        End If

        If txtEmpSenPositionE.Text.Trim() <> "" Then
            If Not Regex.IsMatch(txtEmpSenPositionE.Text, "^[0-9]+(.[0-9]{1,2})?$") Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00050", lblEmpSenPosition.Text)
                txtEmpSenPositionE.Focus()
                Return False
            End If
        End If

        strValueB = txtEmpSenPositionB.Text
        strValueE = txtEmpSenPositionE.Text
        douValueB = 0
        douValueE = 0

        If strValueB <> "" And strValueE <> "" Then
            Double.TryParse(strValueB, douValueB)
            Double.TryParse(strValueE, douValueE)

            If douValueB > douValueE Then
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", lblEmpSenPosition.Text & "輸入請由小到大")
                txtEmpSenPositionB.Focus()
                Return False
            End If
        End If

        '職位年資(連續)
        If txtEmpConSenPositionB.Text.Trim() <> "" Then
            If Not Regex.IsMatch(txtEmpConSenPositionB.Text, "^[0-9]+(.[0-9]{1,2})?$") Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00050", lblEmpConSenPosition.Text)
                txtEmpConSenPositionB.Focus()
                Return False
            End If
        End If

        If txtEmpConSenPositionE.Text.Trim() <> "" Then
            If Not Regex.IsMatch(txtEmpConSenPositionE.Text, "^[0-9]+(.[0-9]{1,2})?$") Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00050", lblEmpConSenPosition.Text)
                txtEmpConSenPositionE.Focus()
                Return False
            End If
        End If

        strValueB = txtEmpConSenPositionB.Text
        strValueE = txtEmpConSenPositionE.Text
        douValueB = 0
        douValueE = 0

        If strValueB <> "" And strValueE <> "" Then
            Double.TryParse(strValueB, douValueB)
            Double.TryParse(strValueE, douValueE)

            If douValueB > douValueE Then
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", lblEmpConSenPosition.Text & "輸入請由小到大")
                txtEmpConSenPositionB.Focus()
                Return False
            End If
        End If

        '工作性質年資
        If txtEmpSenWorkTypeB.Text.Trim() <> "" Then
            If Not Regex.IsMatch(txtEmpSenWorkTypeB.Text, "^[0-9]+(.[0-9]{1,2})?$") Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00050", lblEmpSenWorkType.Text)
                txtEmpSenWorkTypeB.Focus()
                Return False
            End If
        End If

        If txtEmpSenWorkTypeE.Text.Trim() <> "" Then
            If Not Regex.IsMatch(txtEmpSenWorkTypeE.Text, "^[0-9]+(.[0-9]{1,2})?$") Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00050", lblEmpSenWorkType.Text)
                txtEmpSenWorkTypeE.Focus()
                Return False
            End If
        End If

        strValueB = txtEmpSenWorkTypeB.Text
        strValueE = txtEmpSenWorkTypeE.Text
        douValueB = 0
        douValueE = 0

        If strValueB <> "" And strValueE <> "" Then
            Double.TryParse(strValueB, douValueB)
            Double.TryParse(strValueE, douValueE)

            If douValueB > douValueE Then
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", lblEmpSenWorkType.Text & "輸入請由小到大")
                txtEmpSenWorkTypeB.Focus()
                Return False
            End If
        End If

        '工作性質年資(連續)
        If txtEmpConSenWorkTypeB.Text.Trim() <> "" Then
            If Not Regex.IsMatch(txtEmpConSenWorkTypeB.Text, "^[0-9]+(.[0-9]{1,2})?$") Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00050", lblEmpConSenWorkType.Text)
                txtEmpConSenWorkTypeB.Focus()
                Return False
            End If
        End If

        If txtEmpConSenWorkTypeE.Text.Trim() <> "" Then
            If Not Regex.IsMatch(txtEmpConSenWorkTypeE.Text, "^[0-9]+(.[0-9]{1,2})?$") Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00050", lblEmpConSenWorkType.Text)
                txtEmpConSenWorkTypeE.Focus()
                Return False
            End If
        End If

        strValueB = txtEmpConSenWorkTypeB.Text
        strValueE = txtEmpConSenWorkTypeE.Text
        douValueB = 0
        douValueE = 0

        If strValueB <> "" And strValueE <> "" Then
            Double.TryParse(strValueB, douValueB)
            Double.TryParse(strValueE, douValueE)

            If douValueB > douValueE Then
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", lblEmpConSenWorkType.Text & "(連續)輸入請由小到大")
                txtEmpConSenWorkTypeB.Focus()
                Return False
            End If
        End If


        '公司離職日
        If Not funCheckDate(txtQuitDateB, txtQuitDateE, lblQuitDate.Text) Then
            Return False
        End If

        '企業團離職日
        If Not funCheckDate(txtSinopacQuitDateB, txtSinopacQuitDateE, lblSinopacQuitDate.Text) Then
            Return False
        End If

        '最近升遷日/本階起始日
        If Not funCheckDate(txtRankBeginDateB, txtRankBeginDateE, lblRankBeginDate.Text) Then
            Return False
        End If

        '職等
        strValueB = ddlRankB.SelectedValue
        strValueE = ddlRankE.SelectedValue

        If strValueB <> "" And strValueE <> "" Then
            strValue1 = objMA1.GetRankMapping(strCompID, strValueB)
            strValue2 = objMA1.GetRankMapping(strCompID, strValueE)

            If strValue1 <> "" And strValue2 <> "" Then
                intValue1 = Bsp.Utility.ToInteger(strValue1)
                intValue2 = Bsp.Utility.ToInteger(strValue2)

                If intValue1 > intValue2 Then
                    Bsp.Utility.ShowFormatMessage(Me, "H_00000", lblRank.Text & "選擇請由小到大")
                    ddlRankB.Focus()
                    Return False
                End If
            End If
        End If

        '職稱
        If strValueB = strValueE Then
            strValue1 = ddlTitleB.SelectedValue
            strValue2 = ddlTitleE.SelectedValue

            If strValue1 <> "" And strValue2 <> "" Then
                intValue1 = Bsp.Utility.ToInteger(strValue1)
                intValue2 = Bsp.Utility.ToInteger(strValue2)

                If intValue1 > intValue2 Then
                    Bsp.Utility.ShowFormatMessage(Me, "H_00000", lblTitle.Text & "選擇請由小到大")
                    ddlTitleB.Focus()
                    Return False
                End If
            End If
        End If

        '金控職等
        strValueB = ddlHoldingRankB.SelectedValue
        strValueE = ddlHoldingRankE.SelectedValue

        If strValueB <> "" And strValueE <> "" Then
            intValue1 = Bsp.Utility.ToInteger(strValueB)
            intValue2 = Bsp.Utility.ToInteger(strValueE)

            If intValue1 > intValue2 Then
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", lblHoldingRank.Text & "選擇請由小到大")
                ddlHoldingRankB.Focus()
                Return False
            End If
        End If

        '金控職稱
        If strValueB = strValueE Then
            strValue1 = ddlHoldingTitleB.SelectedValue
            strValue2 = ddlHoldingTitleE.SelectedValue

            If strValue1 <> "" And strValue2 <> "" Then
                intValue1 = Bsp.Utility.ToInteger(strValue1)
                intValue2 = Bsp.Utility.ToInteger(strValue2)

                If intValue1 > intValue2 Then
                    Bsp.Utility.ShowFormatMessage(Me, "H_00000", lblHoldingTitle.Text & "選擇請由小到大")
                    ddlHoldingTitleB.Focus()
                    Return False
                End If
            End If
        End If

        '兼任生效日期
        If Not funCheckDate(txtEmpAdditionDateB, txtEmpAdditionDateE, lblEmpAdditionDate.Text) Then
            Return False
        End If

        '前職經歷起日
        If Not funCheckDate(txtLastBeginDateB, txtLastBeginDateE, lblLastJobInfo.Text & "-" & lblLastBeginDate.Text) Then
            Return False
        End If

        '前職經歷迄日
        If Not funCheckDate(txtLastEndDateB, txtLastEndDateE, lblLastJobInfo.Text & "-" & lblLastEndDate.Text) Then
            Return False
        End If

        '前職經歷年資
        If txtLastWorkYearB.Text.Trim() <> "" Then
            If Not Regex.IsMatch(txtLastWorkYearB.Text, "^[0-9]+(.[0-9]{1})?$") Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00050", lblLastJobInfo.Text & "-" & lblLastWorkYear.Text)
                txtLastWorkYearB.Focus()
                Return False
            End If
        End If

        If txtLastWorkYearE.Text.Trim() <> "" Then
            If Not Regex.IsMatch(txtLastWorkYearE.Text, "^[0-9]+(.[0-9]{1})?$") Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00050", lblLastJobInfo.Text & "-" & lblLastWorkYear.Text)
                txtLastWorkYearE.Focus()
                Return False
            End If
        End If

        strValueB = txtLastWorkYearB.Text
        strValueE = txtLastWorkYearE.Text

        If strValueB <> "" And strValueE <> "" Then
            If strValueB > strValueE Then
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", lblLastJobInfo.Text & "-" & lblLastWorkYear.Text & "輸入請由小到大")
                txtLastWorkYearB.Focus()
                Return False
            End If
        End If

        '員工企業團經歷生效日
        If Not funCheckDate(txtModifyDateB, txtModifyDateE, lblEmpLog.Text & "-" & lblModifyDate.Text) Then
            Return False
        End If

        '曾任部門區間
        If Not funCheckDate(txtOrgRangeB, txtOrgRangeE, lblOrgRange.Text) Then
            Return False
        End If

        If ddlOrgRange.SelectedValue = "" And (txtOrgRangeB.DateText <> "" Or txtOrgRangeE.DateText <> "") Then
            Bsp.Utility.ShowMessage(Me, lblOrgRange.Text & "-日期區間與選單皆為必填")
            ddlOrgRange.Focus()
            Return False
        ElseIf ddlOrgRange.SelectedValue <> "" And txtOrgRangeB.DateText = "" And txtOrgRangeE.DateText = "" Then
            Bsp.Utility.ShowMessage(Me, lblOrgRange.Text & "-日期區間與選單皆為必填")
            txtOrgRangeB.Focus()
            Return False
        End If

        '曾任職位區間
        If Not funCheckDate(txtPositionRangeB, txtPositionRangeE, lblPositionRange.Text) Then
            Return False
        End If

        If ddlPositionRange.SelectedValue = "" And (txtPositionRangeB.DateText <> "" Or txtPositionRangeE.DateText <> "") Then
            Bsp.Utility.ShowMessage(Me, lblPositionRange.Text & "-日期區間與選單皆為必填")
            ddlPositionRange.Focus()
            Return False
        ElseIf ddlPositionRange.SelectedValue <> "" And txtPositionRangeB.DateText = "" And txtPositionRangeE.DateText = "" Then
            Bsp.Utility.ShowMessage(Me, lblPositionRange.Text & "-日期區間與選單皆為必填")
            txtPositionRangeB.Focus()
            Return False
        End If

        '曾任部門區間
        If Not funCheckDate(txtWorkTypeRangeB, txtWorkTypeRangeE, lblWorkTypeRange.Text) Then
            Return False
        End If

        If ddlWorkTypeRange.SelectedValue = "" And (txtWorkTypeRangeB.DateText <> "" Or txtWorkTypeRangeE.DateText <> "") Then
            Bsp.Utility.ShowMessage(Me, lblWorkTypeRange.Text & "-日期區間與選單皆為必填")
            ddlWorkTypeRange.Focus()
            Return False
        ElseIf ddlWorkTypeRange.SelectedValue <> "" And txtWorkTypeRangeB.DateText = "" And txtWorkTypeRangeE.DateText = "" Then
            Bsp.Utility.ShowMessage(Me, lblWorkTypeRange.Text & "-日期區間與選單皆為必填")
            txtWorkTypeRangeB.Focus()
            Return False
        End If

        '年薪
        If txtYearSalaryB.Text.Trim() <> "" Then
            If Not Regex.IsMatch(txtYearSalaryB.Text, "^[0-9]+$") Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00050", lblYearSalary.Text)
                txtYearSalaryB.Focus()
                Return False
            End If
        End If

        If txtYearSalaryE.Text.Trim() <> "" Then
            If Not Regex.IsMatch(txtYearSalaryE.Text, "^[0-9]+$") Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00050", lblYearSalary.Text)
                txtYearSalaryE.Focus()
                Return False
            End If
        End If

        strValueB = txtYearSalaryB.Text
        strValueE = txtYearSalaryE.Text
        lngValueB = 0
        lngValueE = 0

        If strValueB <> "" And strValueE <> "" Then
            Long.TryParse(strValueB, lngValueB)
            Long.TryParse(strValueE, lngValueE)

            If lngValueB > lngValueE Then
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", lblYearSalary.Text & "輸入請由小到大")
                txtYearSalaryB.Focus()
                Return False
            End If
        End If

        '月薪
        If txtMonthSalaryB.Text.Trim() <> "" Then
            If Not Regex.IsMatch(txtMonthSalaryB.Text, "^[0-9]+$") Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00050", lblMonthSalary.Text)
                txtMonthSalaryB.Focus()
                Return False
            End If
        End If

        If txtMonthSalaryE.Text.Trim() <> "" Then
            If Not Regex.IsMatch(txtMonthSalaryE.Text, "^[0-9]+$") Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00050", lblMonthSalary.Text)
                txtMonthSalaryE.Focus()
                Return False
            End If
        End If

        strValueB = txtMonthSalaryB.Text
        strValueE = txtMonthSalaryE.Text
        lngValueB = 0
        lngValueE = 0

        If strValueB <> "" And strValueE <> "" Then
            Long.TryParse(strValueB, lngValueB)
            Long.TryParse(strValueE, lngValueE)

            If lngValueB > lngValueE Then
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", lblMonthSalary.Text & "輸入請由小到大")
                txtMonthSalaryB.Focus()
                Return False
            End If
        End If

        Return True
    End Function

    Private Function funCheckDate(ByVal ucDateB As Component_ucCalender, ByVal ucDateE As Component_ucCalender, ByVal strLabel As String) As Boolean

        If ucDateB.DateText.Trim() <> "" Then
            If Bsp.Utility.CheckDate(ucDateB.DateText.Trim()) = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00060", strLabel & "(起)")
                ucDateB.Focus()
                Return False
            End If
        End If

        If ucDateE.DateText.Trim() <> "" Then
            If Bsp.Utility.CheckDate(ucDateE.DateText.Trim()) = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00060", strLabel & "(迄)")
                ucDateE.Focus()
                Return False
            End If
        End If

        If ucDateB.DateText.Trim() <> "" And ucDateE.DateText.Trim() <> "" Then
            If ucDateB.DateText > ucDateE.DateText Then
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", "欄位[" & strLabel & "]起日不可晚於迄日")
                ucDateB.Focus()
                Return False
            End If
        End If

        Return True
    End Function

    Private Sub DoQuery()
        Dim objMA1 As New MA1()

        Dim strCompID As String = ddlCompID.SelectedValue
        If strCompID = "" Then
            strCompID = UserProfile.SelectCompRoleID
        End If

        Try
            pcMain.DataTable = objMA1.CompositeQuery(
                ViewState.Item("QueryFlag"), _
                "CompID=" & strCompID, _
                "EmpID=" & txtEmpID.Text.ToUpper(), _
                "Name=" & txtName.Text, _
                "IDNo=" & txtIDNo.Text.ToUpper(), _
                "EngName=" & txtEngName.Text, _
                "PassportName=" & txtPassportName.Text, _
                "Sex=" & ddlSex.SelectedValue, _
                "BirthDateB=" & txtBirthDateB.DateText, _
                "BirthDateE=" & txtBirthDateE.DateText, _
                "AgeB=" & txtAgeB.Text, _
                "AgeE=" & txtAgeE.Text, _
                "Edu=" & ddlEdu.SelectedValue, _
                "Marriage=" & ddlMarriage.SelectedValue, _
                "Nation=" & ddlNation.SelectedValue, _
                "IDExpireDateB=" & txtIDExpireDateB.DateText, _
                "IDExpireDateE=" & txtIDExpireDateE.DateText, _
                "AboriginalFlag=" & ddlAboriginalFlag.SelectedValue, _
                "AboriginalTribe=" & ddlAboriginalTribe.SelectedValue, _
                "LocalHireFlag=" & ddlLocalHireFlag.SelectedValue, _
                "PassExamFlag=" & ddlPassExamFlag.SelectedValue, _
                "EmpType=" & ddlEmpType.SelectedValue, _
                "WorkCode=" & ddlWorkCode.SelectedValue, _
                "ProbMonth=" & ddlProbMonth.SelectedValue, _
                "ProbDateB=" & txtProbDateB.DateText, _
                "ProbDateE=" & txtProbDateE.DateText, _
                "EmpDateB=" & txtEmpDateB.DateText, _
                "EmpDateE=" & txtEmpDateE.DateText, _
                "SinopacEmpDateB=" & txtSinopacEmpDateB.DateText, _
                "SinopacEmpDateE=" & txtSinopacEmpDateE.DateText, _
                "QuitDateB=" & txtQuitDateB.DateText, _
                "QuitDateE=" & txtQuitDateE.DateText, _
                "SinopacQuitDateB=" & txtSinopacQuitDateB.DateText, _
                "SinopacQuitDateE=" & txtSinopacQuitDateE.DateText, _
                "RankBeginDateB=" & txtRankBeginDateB.DateText, _
                "RankBeginDateE=" & txtRankBeginDateE.DateText, _
                "WorkSite=" & ddlWorkSite.SelectedValue, _
                "RankB=" & ddlRankB.SelectedValue, _
                "RankE=" & ddlRankE.SelectedValue, _
                "TitleB=" & ddlTitleB.SelectedValue, _
                "TitleE=" & ddlTitleE.SelectedValue, _
                "HoldingRankB=" & ddlHoldingRankB.SelectedValue, _
                "HoldingRankE=" & ddlHoldingRankE.SelectedValue, _
                "HoldingTitleB=" & ddlHoldingTitleB.SelectedValue, _
                "HoldingTitleE=" & ddlHoldingTitleE.SelectedValue, _
                "PublicTitle=" & ddlPublicTitle.SelectedValue, _
                "GroupID=" & ddlGroupID.SelectedValue, _
                "OrgType=" & ddlOrgType.SelectedValue, _
                "DeptID=" & ddlDeptID.SelectedValue, _
                "OrganID=" & ddlOrganID.SelectedValue, _
                "Position1=" & hidPosition1.Value, _
                "Position2=" & hidPosition2.Value, _
                "WorkType1=" & hidWorkType1.Value, _
                "WorkType2=" & hidWorkType2.Value, _
                "WTID=" & ddlWTID.SelectedValue, _
                "OfficeLoginFlag=" & ddlOfficeLoginFlag.SelectedValue, _
                "EmpSeniorityB=" & txtEmpSeniorityB.Text, _
                "EmpSeniorityE=" & txtEmpSeniorityE.Text, _
                "SinopacEmpSeniorityB=" & txtSinopacEmpSeniorityB.Text, _
                "SinopacEmpSeniorityE=" & txtSinopacEmpSeniorityE.Text, _
                "EmpSenOrgTypeB=" & txtEmpSenOrgTypeB.Text, _
                "EmpSenOrgTypeE=" & txtEmpSenOrgTypeE.Text, _
                "EmpConSenOrgTypeB=" & txtEmpConSenOrgTypeB.Text, _
                "EmpConSenOrgTypeE=" & txtEmpConSenOrgTypeE.Text, _
                "EmpSenOrgTypeFlowB=" & txtEmpSenOrgTypeFlowB.Text, _
                "EmpSenOrgTypeFlowE=" & txtEmpSenOrgTypeFlowE.Text, _
                "EmpSenRankB=" & txtEmpSenRankB.Text, _
                "EmpSenRankE=" & txtEmpSenRankE.Text, _
                "EmpSenPositionB=" & txtEmpSenPositionB.Text, _
                "EmpSenPositionE=" & txtEmpSenPositionE.Text, _
                "EmpConSenPositionB=" & txtEmpConSenPositionB.Text, _
                "EmpConSenPositionE=" & txtEmpConSenPositionE.Text, _
                "EmpSenWorkTypeB=" & txtEmpSenWorkTypeB.Text, _
                "EmpSenWorkTypeE=" & txtEmpSenWorkTypeE.Text, _
                "EmpConSenWorkTypeB=" & txtEmpConSenWorkTypeB.Text, _
                "EmpConSenWorkTypeE=" & txtEmpConSenWorkTypeE.Text, _
                "EmpAddition=" & ddlEmpAddition.SelectedValue, _
                "EmpAdditionComp=" & ddlEmpAdditionComp.SelectedValue, _
                "EmpAdditionDept=" & ddlEmpAdditionDept.SelectedValue, _
                "EmpAdditionReason=" & ddlEmpAdditionReason.SelectedValue, _
                "EmpAdditionDateB=" & txtEmpAdditionDateB.DateText, _
                "EmpAdditionDateE=" & txtEmpAdditionDateE.DateText, _
                "SchoolType=" & ddlSchoolType.SelectedValue, _
                "SchoolID=" & hidSchool.Value, _
                "Depart=" & txtDepart.Text, _
                "FamilyOccupation=" & txtFamilyOccupation.Text, _
                "FamilyIndustryType=" & txtFamilyIndustryType.Text, _
                "FamilyCompany=" & txtFamilyCompany.Text, _
                "RegCityCode=" & ddlRegCityCode.SelectedValue, _
                "RegAddrCode=" & ddlRegAddrCode.SelectedValue, _
                "RegAddr=" & txtRegAddr.Text, _
                "CommCityCode=" & ddlCommCityCode.SelectedValue, _
                "CommAddrCode=" & ddlCommAddrCode.SelectedValue, _
                "CommAddr=" & txtCommAddr.Text, _
                "LastBeginDateB=" & txtLastBeginDateB.DateText, _
                "LastBeginDateE=" & txtLastBeginDateE.DateText, _
                "LastEndDateB=" & txtLastEndDateB.DateText, _
                "LastEndDateE=" & txtLastEndDateE.DateText, _
                "LastIndustryType=" & ddlLastIndustryType.SelectedValue, _
                "LastCompany=" & txtLastCompany.Text, _
                "LastDept=" & txtLastDept.Text, _
                "LastTitle=" & txtLastTitle.Text, _
                "LastWorkType=" & txtLastWorkType.Text, _
                "LastWorkYearB=" & txtLastWorkYearB.Text, _
                "LastWorkYearE=" & txtLastWorkYearE.Text, _
                "Profession=" & ddlProfession.SelectedValue, _
                "ModifyDateB=" & txtModifyDateB.DateText, _
                "ModifyDateE=" & txtModifyDateE.DateText, _
                "Reason=" & ddlReason.SelectedValue, _
                "LogDept=" & ddlLogDept.SelectedValue, _
                "LogOrgan=" & ddlLogOrgan.SelectedValue, _
                "LogPosition=" & txtLogPosition.Text, _
                "LogWorkType=" & txtLogWorkType.Text, _
                "LogRank=" & ddlLogRank.SelectedValue, _
                "LogTitle=" & ddlLogTitle.SelectedValue, _
                "OrgRangeB=" & txtOrgRangeB.DateText, _
                "OrgRangeE=" & txtOrgRangeE.DateText, _
                "OrgRange=" & ddlOrgRange.SelectedValue, _
                "PositionRangeB=" & txtPositionRangeB.DateText, _
                "PositionRangeE=" & txtPositionRangeE.DateText, _
                "PositionRange=" & ddlPositionRange.SelectedValue, _
                "WorkTypeRangeB=" & txtWorkTypeRangeB.DateText, _
                "WorkTypeRangeE=" & txtWorkTypeRangeE.DateText, _
                "WorkTypeRange=" & ddlWorkTypeRange.SelectedValue, _
                "CategoryID=" & hidCategory.Value, _
                "CommGrade=" & ddlCommGrade.SelectedValue, _
                "GradeYear=" & ddlGradeYear.SelectedValue, _
                "Grade=" & ddlGrade.SelectedValue, _
                "LastGrade1=" & ddlLastGrade1.SelectedValue, _
                "LastGrade2=" & ddlLastGrade2.SelectedValue, _
                "LastGrade3=" & ddlLastGrade3.SelectedValue, _
                "LastGrade4=" & ddlLastGrade4.SelectedValue, _
                "YearSalaryB=" & txtYearSalaryB.Text, _
                "YearSalaryE=" & txtYearSalaryE.Text, _
                "MonthSalaryB=" & txtMonthSalaryB.Text, _
                "MonthSalaryE=" & txtMonthSalaryE.Text)
            gvMain.Visible = True

            Bsp.Utility.RunClientScript(Me, "ScrollButtom();")
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
        End Try
    End Sub

    Protected Sub gvMain_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMain.RowCommand
        Dim objMA1 As New MA1()

        If e.CommandName = "Detail" Then

            '確認登入者是否有ST1000的權限
            If objMA1.GetAdmission(UserProfile.ActCompID, UserProfile.ActUserID, UserProfile.LoginSysID, UserProfile.SelectCompRoleID, "ST1000") Then
                Dim a As New FlowBackInfo()
                a.MenuNodeTitle = "回清單"
                a.URL = "~/MA/MA1000.aspx"

                Dim Params As Object() = GenParams()

                TransferFramePage(Bsp.MySettings.FlowRedirectPage, Nothing, "FlowID=EMPINFO", a, _
                    Params, _
                    "SelectedCompID=" & gvMain.DataKeys(Me.selectedRow(gvMain))("CompID").ToString(), _
                    "SelectedCompName=" & gvMain.DataKeys(Me.selectedRow(gvMain))("CompName").ToString, _
                    "SelectedEmpID=" & gvMain.DataKeys(Me.selectedRow(gvMain))("EmpID").ToString(), _
                    "SelectedEmpName=" & gvMain.DataKeys(Me.selectedRow(gvMain))("NameN").ToString(), _
                    "SelectedIDNo=" & gvMain.DataKeys(Me.selectedRow(gvMain))("IDNo").ToString(), _
                    "PageNo=" & pcMain.PageNo.ToString(), _
                    "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))

            End If
        End If
    End Sub

    Private Function GenParams() As Object()
        Dim ArrParams As ArrayList = New ArrayList

        For Each ctl As Control In GetAllCtrl(UpdMain)
            If TypeOf ctl Is TextBox Then
                If CType(ctl, TextBox).ID.StartsWith("txt") And Not Me.FindControl(CType(ctl, TextBox).ID) Is Nothing And CType(ctl, TextBox).Text.Trim <> "" Then
                    ArrParams.Add(CType(ctl, TextBox).ID & "=" & CType(ctl, TextBox).Text)
                End If
            ElseIf TypeOf ctl Is HiddenField Then
                If CType(ctl, HiddenField).ID.StartsWith("hid") And Not Me.FindControl(CType(ctl, HiddenField).ID) Is Nothing And CType(ctl, HiddenField).Value.Trim <> "" Then
                    ArrParams.Add(CType(ctl, HiddenField).ID & "=" & CType(ctl, HiddenField).Value)
                End If
            ElseIf TypeOf ctl Is DropDownList Then
                If CType(ctl, DropDownList).ID.StartsWith("ddl") And Not Me.FindControl(CType(ctl, DropDownList).ID) Is Nothing And CType(ctl, DropDownList).SelectedValue <> "" Then
                    ArrParams.Add(CType(ctl, DropDownList).ID & "=" & CType(ctl, DropDownList).SelectedValue)
                End If
            ElseIf TypeOf ctl Is Component_ucCalender Then
                If CType(ctl, Component_ucCalender).ID.StartsWith("txt") And Not Me.FindControl(CType(ctl, Component_ucCalender).ID) Is Nothing And CType(ctl, Component_ucCalender).DateText.Trim <> "" Then
                    ArrParams.Add(CType(ctl, Component_ucCalender).ID & "=" & CType(ctl, Component_ucCalender).DateText)
                End If
            End If
        Next

        Return ArrParams.ToArray
    End Function

    Private Function GetAllCtrl(ByVal Root As Control) As Object()
        Dim ArrCtrl As ArrayList = New ArrayList
        ArrCtrl.Add(Root)

        If Root.HasControls() Then
            For Each ctl As Control In Root.Controls
                ArrCtrl.AddRange(GetAllCtrl(ctl))
            Next
        End If

        Return ArrCtrl.ToArray
    End Function

    Private Sub DoClear()
        ViewState.Item("DoQuery") = ""

        If ddlCompID.SelectedValue <> "" Then
            ddlCompID.SelectedIndex = 0
            ddlCompID_Changed(Nothing, Nothing)
        End If

        txtEmpID.Text = ""
        txtName.Text = ""
        txtIDNo.Text = ""
        txtEngName.Text = ""
        txtPassportName.Text = ""
        ddlSex.SelectedIndex = 0
        txtBirthDateB.DateText = ""
        txtBirthDateE.DateText = ""
        txtAgeB.Text = ""
        txtAgeE.Text = ""

        ddlEdu.SelectedIndex = 0
        ddlMarriage.SelectedIndex = 0
        ddlNation.SelectedIndex = 0
        txtIDExpireDateB.DateText = ""
        txtIDExpireDateE.DateText = ""
        ddlAboriginalFlag.SelectedIndex = 0
        ddlAboriginalTribe.SelectedIndex = 0
        ddlLocalHireFlag.SelectedIndex = 0
        ddlPassExamFlag.SelectedIndex = 0
        ddlEmpType.SelectedIndex = 1
        ddlWorkCode.SelectedIndex = 1
        ddlProbMonth.SelectedIndex = 0
        txtProbDateB.DateText = ""
        txtProbDateE.DateText = ""

        txtEmpDateB.DateText = ""
        txtEmpDateE.DateText = ""
        txtSinopacEmpDateB.DateText = ""
        txtSinopacEmpDateE.DateText = ""
        txtQuitDateB.DateText = ""
        txtQuitDateE.DateText = ""
        txtSinopacQuitDateB.DateText = ""
        txtSinopacQuitDateE.DateText = ""
        txtRankBeginDateB.DateText = ""
        txtRankBeginDateE.DateText = ""
        ddlWorkSite.SelectedIndex = 0

        ddlRankB.SelectedIndex = 0
        ddlRankE.SelectedIndex = 0
        ddlTitleB.Items.Clear()
        ddlTitleE.Items.Clear()
        ddlTitleB.Items.Insert(0, New ListItem("---請先選擇職等---", ""))
        ddlTitleE.Items.Insert(0, New ListItem("---請先選擇職等---", ""))
        ddlHoldingRankB.SelectedIndex = 0
        ddlHoldingRankE.SelectedIndex = 0
        ddlHoldingTitleB.Items.Clear()
        ddlHoldingTitleE.Items.Clear()
        ddlHoldingTitleB.Items.Insert(0, New ListItem("---請先選擇金控職等---", ""))
        ddlHoldingTitleE.Items.Insert(0, New ListItem("---請先選擇金控職等---", ""))
        ddlPublicTitle.SelectedIndex = 0
        ddlGroupID.SelectedIndex = 0
        ddlOrgType.SelectedIndex = 0
        ddlDeptID.SelectedIndex = 0
        ddlDept_Changed(ddlDeptID, Nothing)

        ddlPosition1.Items.Clear()
        ddlPosition2.Items.Clear()
        ddlWorkType1.Items.Clear()
        ddlWorkType2.Items.Clear()
        hidPosition1.Value = ""
        hidPosition2.Value = ""
        hidWorkType1.Value = ""
        hidWorkType2.Value = ""
        ddlWTID.SelectedIndex = 0
        ddlOfficeLoginFlag.SelectedIndex = 0

        txtEmpSeniorityB.Text = ""
        txtEmpSeniorityE.Text = ""
        txtSinopacEmpSeniorityB.Text = ""
        txtSinopacEmpSeniorityE.Text = ""
        txtEmpSenOrgTypeB.Text = ""
        txtEmpSenOrgTypeE.Text = ""
        txtEmpConSenOrgTypeB.Text = ""
        txtEmpConSenOrgTypeE.Text = ""
        txtEmpSenOrgTypeFlowB.Text = ""
        txtEmpSenOrgTypeFlowE.Text = ""
        txtEmpSenRankB.Text = ""
        txtEmpSenRankE.Text = ""
        txtEmpSenPositionB.Text = ""
        txtEmpSenPositionE.Text = ""
        txtEmpConSenPositionB.Text = ""
        txtEmpConSenPositionE.Text = ""
        txtEmpSenWorkTypeB.Text = ""
        txtEmpSenWorkTypeE.Text = ""
        txtEmpConSenWorkTypeB.Text = ""
        txtEmpConSenWorkTypeE.Text = ""

        ddlEmpAddition.SelectedIndex = 0
        ddlEmpAddition_Changed(Nothing, Nothing)

        ddlSchoolType.SelectedIndex = 0
        ddlSchool.Items.Clear()
        hidSchool.Value = ""
        txtDepart.Text = ""

        txtFamilyOccupation.Text = ""
        txtFamilyIndustryType.Text = ""
        txtFamilyCompany.Text = ""

        ddlRegCityCode.SelectedIndex = 0
        ddlCityCode_SelectedChanged(ddlRegCityCode, Nothing)
        txtRegAddr.Text = ""

        ddlCommCityCode.SelectedIndex = 0
        ddlCityCode_SelectedChanged(ddlCommCityCode, Nothing)
        txtCommAddr.Text = ""

        txtLastBeginDateB.DateText = ""
        txtLastBeginDateE.DateText = ""
        txtLastEndDateB.DateText = ""
        txtLastEndDateE.DateText = ""
        ddlLastIndustryType.SelectedIndex = 0
        txtLastCompany.Text = ""
        txtLastDept.Text = ""
        txtLastTitle.Text = ""
        txtLastWorkType.Text = ""
        txtLastWorkYearB.Text = ""
        txtLastWorkYearE.Text = ""
        ddlProfession.SelectedIndex = 0

        txtModifyDateB.DateText = ""
        txtModifyDateE.DateText = ""
        ddlReason.SelectedIndex = 0
        ddlLogDept.SelectedIndex = 0
        ddlDept_Changed(ddlLogDept, Nothing)
        txtLogPosition.Text = ""
        txtLogWorkType.Text = ""
        ddlLogRank.SelectedIndex = 0
        ddlLogTitle.SelectedIndex = 0

        txtOrgRangeB.DateText = ""
        txtOrgRangeE.DateText = ""
        ddlOrgRange.SelectedIndex = 0
        txtPositionRangeB.DateText = ""
        txtPositionRangeE.DateText = ""
        ddlPositionRange.SelectedIndex = 0
        txtWorkTypeRangeB.DateText = ""
        txtWorkTypeRangeE.DateText = ""
        ddlWorkTypeRange.SelectedIndex = 0

        ddlCategory.Items.Clear()
        hidCategory.Value = ""

        ddlCommGrade.SelectedIndex = 0
        ddlGradeYear.SelectedIndex = 0
        ddlGradeYear_Changed(ddlGradeYear, Nothing)

        ddlLastGrade1.SelectedIndex = 0
        ddlLastGrade2.SelectedIndex = 0
        ddlLastGrade3.SelectedIndex = 0
        ddlLastGrade4.SelectedIndex = 0

        txtYearSalaryB.Text = ""
        txtYearSalaryE.Text = ""
        txtMonthSalaryB.Text = ""
        txtMonthSalaryE.Text = ""

        If Not pcMain.DataTable Is Nothing Then
            pcMain.DataTable.Clear()
            pcMain.BindGridView()
        End If

        gvMain.Visible = False
    End Sub

    Private Sub subLoadData()
        Dim strCompID As String = ddlCompID.SelectedValue
        If strCompID = "" Then
            strCompID = UserProfile.SelectCompRoleID
        End If

        '學歷
        Bsp.Utility.FillDDL(ddlEdu, "eHRMSDB", "EduDegree", "EduID", "EduName")
        ddlEdu.Items.Insert(0, New ListItem("---請選擇---", ""))

        '原住民族別
        Bsp.Utility.FillDDL(ddlAboriginalTribe, "eHRMSDB", "HRCodeMap", "Code", "CodeCName", Bsp.Utility.DisplayType.Full, "", "and TabName = 'PersonalOther' and FldName = 'AboriginalTribe' and NotShowFlag = '0'")
        ddlAboriginalTribe.Items.Insert(0, New ListItem("---請選擇---", ""))

        '雇用類別
        Bsp.Utility.FillDDL(ddlEmpType, "eHRMSDB", "HRCodeMap", "Code", "CodeCName", Bsp.Utility.DisplayType.Full, "", "and TabName = 'Personal' and FldName = 'EmpType' and NotShowFlag = '0'")
        ddlEmpType.Items.Insert(0, New ListItem("---請選擇---", ""))
        ddlEmpType.SelectedIndex = 1

        '任職狀況
        Bsp.Utility.FillDDL(ddlWorkCode, "eHRMSDB", "WorkStatus", "WorkCode", "Remark")
        ddlWorkCode.Items.Insert(0, New ListItem("---請選擇---", ""))
        ddlWorkCode.SelectedIndex = 1

        '試用期
        Bsp.Utility.FillDDL(ddlProbMonth, "eHRMSDB", "HRCodeMap", "Code", "CodeCName", Bsp.Utility.DisplayType.Full, "", "and TabName = 'Personal' and FldName = 'ProbMonth' and NotShowFlag = '0'")
        ddlProbMonth.Items.Insert(0, New ListItem("---請選擇---", ""))

        '工作地點
        Bsp.Utility.FillDDL(ddlWorkSite, "eHRMSDB", "WorkSite", "distinct WorkSiteID", "Remark", Bsp.Utility.DisplayType.Full, "", IIf(strCompID = "0", "", "and CompID = " & Bsp.Utility.Quote(strCompID)))
        ddlWorkSite.Items.Insert(0, New ListItem("---請選擇---", ""))

        '職等
        Bsp.Utility.FillDDL(ddlRankB, "eHRMSDB", "Rank", "distinct RankID", "RankID", Bsp.Utility.DisplayType.OnlyID, "", IIf(strCompID = "0", "", "and CompID = " & Bsp.Utility.Quote(strCompID)))
        ddlRankB.Items.Insert(0, New ListItem("---請選擇---", ""))
        Bsp.Utility.FillDDL(ddlRankE, "eHRMSDB", "Rank", "distinct RankID", "RankID", Bsp.Utility.DisplayType.OnlyID, "", IIf(strCompID = "0", "", "and CompID = " & Bsp.Utility.Quote(strCompID)))
        ddlRankE.Items.Insert(0, New ListItem("---請選擇---", ""))

        '職稱
        ddlTitleB.Items.Insert(0, New ListItem("---請先選擇職等---", ""))
        ddlTitleE.Items.Insert(0, New ListItem("---請先選擇職等---", ""))

        '金控職等
        Bsp.Utility.FillDDL(ddlHoldingRankB, "eHRMSDB", "CompareRank", "distinct HoldingRankID", "HoldingRankID", Bsp.Utility.DisplayType.OnlyID, "", IIf(strCompID = "0", "", "and CompID = " & Bsp.Utility.Quote(strCompID)))
        ddlHoldingRankB.Items.Insert(0, New ListItem("---請選擇---", ""))
        Bsp.Utility.FillDDL(ddlHoldingRankE, "eHRMSDB", "CompareRank", "distinct HoldingRankID", "HoldingRankID", Bsp.Utility.DisplayType.OnlyID, "", IIf(strCompID = "0", "", "and CompID = " & Bsp.Utility.Quote(strCompID)))
        ddlHoldingRankE.Items.Insert(0, New ListItem("---請選擇---", ""))

        '金控職稱
        ddlHoldingTitleB.Items.Insert(0, New ListItem("---請先選擇金控職等---", ""))
        ddlHoldingTitleE.Items.Insert(0, New ListItem("---請先選擇金控職等---", ""))

        '對外職稱
        Bsp.Utility.FillDDL(ddlPublicTitle, "eHRMSDB", "PublicTitle", "PublicTitleID", "PublicTitleName")
        ddlPublicTitle.Items.Insert(0, New ListItem("---請選擇---", ""))

        '事業群
        Bsp.Utility.FillDDL(ddlGroupID, "eHRMSDB", "OrganizationFlow", "RTRIM(GroupID)", "OrganName + case when InValidFlag = '1' then '(無效)' else '' end", Bsp.Utility.DisplayType.Full, "", "and OrganID = GroupID " & IIf(strCompID = "0", "", "and CompID = " & Bsp.Utility.Quote(strCompID)) _
                            & " and GroupID In (Select Distinct GroupID From Organization " & IIf(strCompID = "0", "", "Where CompID = " & Bsp.Utility.Quote(strCompID)) & ")", "Order By InValidFlag, GroupID")
        ddlGroupID.Items.Insert(0, New ListItem("---請選擇---", ""))

        '單位類別
        subLoadOrganColor(ddlOrgType, strCompID)

        '部門
        subLoadOrganColor(ddlDeptID, strCompID)

        '科組課
        Bsp.Utility.FillDDL(ddlOrganID, "eHRMSDB", "Organization", "RTRIM(OrganID)", "OrganName + case when InValidFlag = '1' then '(無效)' else '' end", Bsp.Utility.DisplayType.Full, "", "and OrganID <> DeptID " & IIf(strCompID = "0", "", "and CompID = " & Bsp.Utility.Quote(strCompID)), "Order By InValidFlag, OrganID")
        ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))

        '兼任公司
        Bsp.Utility.FillHRCompany(ddlEmpAdditionComp)
        ddlEmpAdditionComp.Items.Insert(0, New ListItem("---請選擇---", ""))

        '兼任部門
        ddlEmpAdditionDept.Items.Insert(0, New ListItem("---請先選擇兼任公司---", ""))

        '兼任狀態
        Bsp.Utility.FillDDL(ddlEmpAdditionReason, "eHRMSDB", "HRCodeMap", "Code", "CodeCName", Bsp.Utility.DisplayType.Full, "", "and TabName = 'EmpAddition' and FldName = 'Reason' and NotShowFlag = '0'")
        ddlEmpAdditionReason.Items.Insert(0, New ListItem("---請選擇---", ""))

        '班別
        Bsp.Utility.FillDDL(ddlWTID, "eHRMSDB", "WorkTime", "distinct WTID", "", Bsp.Utility.DisplayType.OnlyID, "", IIf(strCompID = "0", "", "and CompID = " & Bsp.Utility.Quote(strCompID)))
        ddlWTID.Items.Insert(0, New ListItem("---請選擇---", ""))

        '學校類別
        Bsp.Utility.FillSchoolType(ddlSchoolType)
        ddlSchoolType.Items.Insert(0, New ListItem("---請選擇---", ""))

        '縣市別
        Bsp.Utility.FillDDL(ddlRegCityCode, "eHRMSDB", "PostalCode", "distinct CityCode", "LEFT(AddrCode, 1)", Bsp.Utility.DisplayType.OnlyID, "", "AND AreaCode <> ''", "ORDER BY LEFT(AddrCode, 1)")
        ddlRegCityCode.Items.Insert(0, New ListItem("---請選擇---", ""))
        ddlRegAddrCode.Items.Insert(0, New ListItem("---請選擇---", ""))

        Bsp.Utility.FillDDL(ddlCommCityCode, "eHRMSDB", "PostalCode", "distinct CityCode", "LEFT(AddrCode, 1)", Bsp.Utility.DisplayType.OnlyID, "", "AND AreaCode <> ''", "ORDER BY LEFT(AddrCode, 1)")
        ddlCommCityCode.Items.Insert(0, New ListItem("---請選擇---", ""))
        ddlCommAddrCode.Items.Insert(0, New ListItem("---請選擇---", ""))

        '產業類別
        Bsp.Utility.FillDDL(ddlLastIndustryType, "eHRMSDB", "HRCodeMap", "Code", "CodeCName", Bsp.Utility.DisplayType.Full, "", "and TabName = 'Experience' and FldName = 'IndustryType' and NotShowFlag = '0'")
        ddlLastIndustryType.Items.Insert(0, New ListItem("---請選擇---", ""))

        '異動原因
        Bsp.Utility.FillDDL(ddlReason, "eHRMSDB", "EmployeeReason", "RTRIM(Reason)", "Remark + case when InValidFlag = '1' then '(無效)' else '' end", Bsp.Utility.DisplayType.Full, "", "and EmployeeWaitFlag = '1' and InValidFlag = '0'", "Order By InValidFlag, Reason")
        ddlReason.Items.Insert(0, New ListItem("---請選擇---", ""))

        '企業團經歷部門
        subLoadOrganColor(ddlLogDept, strCompID)

        '企業團經歷科組課
        Bsp.Utility.FillDDL(ddlLogOrgan, "eHRMSDB", "Organization", "RTRIM(OrganID)", "OrganName + case when InValidFlag = '1' then '(無效)' else '' end", Bsp.Utility.DisplayType.Full, "", "and OrganID <> DeptID " & IIf(strCompID = "0", "", "and CompID = " & Bsp.Utility.Quote(strCompID)), "Order By InValidFlag, OrganID")
        ddlLogOrgan.Items.Insert(0, New ListItem("---請選擇---", ""))

        '企業團經歷職等
        Bsp.Utility.FillDDL(ddlLogRank, "eHRMSDB", "Rank", "distinct RankID", "RankID", Bsp.Utility.DisplayType.OnlyID, "", IIf(strCompID = "0", "", "and CompID = " & Bsp.Utility.Quote(strCompID)))
        ddlLogRank.Items.Insert(0, New ListItem("---請選擇---", ""))

        '企業團經歷職稱
        ddlLogTitle.Items.Insert(0, New ListItem("---請先選擇職等---", ""))

        '曾任部門區間
        subLoadOrganColor(ddlOrgRange, strCompID)

        '曾任職位區間
        Bsp.Utility.FillDDL(ddlPositionRange, "eHRMSDB", "Position", "distinct PositionID", "Remark", Bsp.Utility.DisplayType.Full, "", IIf(strCompID = "0", "", "and CompID = " & Bsp.Utility.Quote(strCompID)))
        ddlPositionRange.Items.Insert(0, New ListItem("---請選擇職位---", ""))

        '曾任工作性質區間
        Bsp.Utility.FillDDL(ddlWorkTypeRange, "eHRMSDB", "WorkType", "distinct WorkTypeID", "Remark", Bsp.Utility.DisplayType.Full, "", IIf(strCompID = "0", "", "and CompID = " & Bsp.Utility.Quote(strCompID)))
        ddlWorkTypeRange.Items.Insert(0, New ListItem("---請選擇工作性質---", ""))

        '證照代碼
        'Bsp.Utility.FillDDL(ddlCategoryID, "eHRMSDB", "License", "CategoryID", "LicenseName")
        'ddlCategoryID.Items.Insert(0, New ListItem("---請選擇---", ""))

        '考績年度
        Bsp.Utility.FillDDL(ddlGradeYear, "eHRMSDB", "EmpGrade", "distinct GradeYear", "", Bsp.Utility.DisplayType.OnlyID, "", "", "Order By GradeYear DESC")
        ddlGradeYear.Items.Insert(0, New ListItem("---請選擇---", ""))

        '考績
        ddlGrade.Items.Insert(0, New ListItem("---請先選擇考績年度---", ""))

        '考績常用條件
        Bsp.Utility.FillDDL(ddlCommGrade, "eHRMSDB", "HRCodeMap", "Code", "CodeCName", Bsp.Utility.DisplayType.Full, "", "and TabName = 'ManPower' and FldName = 'GradeCondition' and NotShowFlag = '0'", "Order By SortFld")
        ddlCommGrade.Items.Insert(0, New ListItem("---請選擇---", ""))

        '前一年考績
        Bsp.Utility.FillDDL(ddlLastGrade1, "eHRMSDB", "HRCodeMap", "Code", "CodeCName", Bsp.Utility.DisplayType.Full, "", "and TabName = 'EmpGrade' and FldName = 'Grade' and NotShowFlag = '0'", "Order By SortFld")
        ddlLastGrade1.Items.Insert(0, New ListItem("ALL-全部", "ALL"))
        ddlLastGrade1.Items.Insert(0, New ListItem("---請選擇---", ""))

        '前二年考績
        Bsp.Utility.FillDDL(ddlLastGrade2, "eHRMSDB", "HRCodeMap", "Code", "CodeCName", Bsp.Utility.DisplayType.Full, "", "and TabName = 'EmpGrade' and FldName = 'Grade' and NotShowFlag = '0'", "Order By SortFld")
        ddlLastGrade2.Items.Insert(0, New ListItem("ALL-全部", "ALL"))
        ddlLastGrade2.Items.Insert(0, New ListItem("---請選擇---", ""))

        '前三年考績
        Bsp.Utility.FillDDL(ddlLastGrade3, "eHRMSDB", "HRCodeMap", "Code", "CodeCName", Bsp.Utility.DisplayType.Full, "", "and TabName = 'EmpGrade' and FldName = 'Grade' and NotShowFlag = '0'", "Order By SortFld")
        ddlLastGrade3.Items.Insert(0, New ListItem("ALL-全部", "ALL"))
        ddlLastGrade3.Items.Insert(0, New ListItem("---請選擇---", ""))

        '前四年考績
        Bsp.Utility.FillDDL(ddlLastGrade4, "eHRMSDB", "HRCodeMap", "Code", "CodeCName", Bsp.Utility.DisplayType.Full, "", "and TabName = 'EmpGrade' and FldName = 'Grade' and NotShowFlag = '0'", "Order By SortFld")
        ddlLastGrade4.Items.Insert(0, New ListItem("ALL-全部", "ALL"))
        ddlLastGrade4.Items.Insert(0, New ListItem("---請選擇---", ""))

    End Sub

    Protected Sub ddlCompID_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCompID.SelectedIndexChanged
        Dim strCompID = ddlCompID.SelectedValue

        If strCompID <> "" Then
            If strCompID = "0" Then
                TrPosWork.Visible = False
            Else
                TrPosWork.Visible = True
            End If

            '工作地點
            Bsp.Utility.FillDDL(ddlWorkSite, "eHRMSDB", "WorkSite", "distinct WorkSiteID", "Remark", Bsp.Utility.DisplayType.Full, "", IIf(strCompID = "0", "", "and CompID = " & Bsp.Utility.Quote(strCompID)))
            ddlWorkSite.Items.Insert(0, New ListItem("---請選擇---", ""))

            '職等
            Bsp.Utility.FillDDL(ddlRankB, "eHRMSDB", "Rank", "distinct RankID", "RankID", Bsp.Utility.DisplayType.OnlyID, "", IIf(strCompID = "0", "", "and CompID = " & Bsp.Utility.Quote(strCompID)))
            ddlRankB.Items.Insert(0, New ListItem("---請選擇---", ""))
            Bsp.Utility.FillDDL(ddlRankE, "eHRMSDB", "Rank", "distinct RankID", "RankID", Bsp.Utility.DisplayType.OnlyID, "", IIf(strCompID = "0", "", "and CompID = " & Bsp.Utility.Quote(strCompID)))
            ddlRankE.Items.Insert(0, New ListItem("---請選擇---", ""))

            '職稱
            ddlTitleB.Items.Clear()
            ddlTitleB.Items.Insert(0, New ListItem("---請先選擇職等---", ""))
            ddlTitleE.Items.Clear()
            ddlTitleE.Items.Insert(0, New ListItem("---請先選擇職等---", ""))

            '金控職等
            Bsp.Utility.FillDDL(ddlHoldingRankB, "eHRMSDB", "CompareRank", "distinct HoldingRankID", "HoldingRankID", Bsp.Utility.DisplayType.OnlyID, "", IIf(strCompID = "0", "", "and CompID = " & Bsp.Utility.Quote(strCompID)))
            ddlHoldingRankB.Items.Insert(0, New ListItem("---請選擇---", ""))
            Bsp.Utility.FillDDL(ddlHoldingRankE, "eHRMSDB", "CompareRank", "distinct HoldingRankID", "HoldingRankID", Bsp.Utility.DisplayType.OnlyID, "", IIf(strCompID = "0", "", "and CompID = " & Bsp.Utility.Quote(strCompID)))
            ddlHoldingRankE.Items.Insert(0, New ListItem("---請選擇---", ""))

            '金控職稱
            ddlHoldingTitleB.Items.Clear()
            ddlHoldingTitleB.Items.Insert(0, New ListItem("---請先選擇金控職等---", ""))
            ddlHoldingTitleE.Items.Clear()
            ddlHoldingTitleE.Items.Insert(0, New ListItem("---請先選擇金控職等---", ""))

            '職位
            ddlPosition1.Items.Clear()
            ddlPosition2.Items.Clear()
            hidPosition1.Value = ""
            hidPosition2.Value = ""

            '工作性質
            ddlWorkType1.Items.Clear()
            ddlWorkType2.Items.Clear()
            hidWorkType1.Value = ""
            hidWorkType2.Value = ""

            '事業群
            Bsp.Utility.FillDDL(ddlGroupID, "eHRMSDB", "OrganizationFlow", "RTRIM(GroupID)", "OrganName + case when InValidFlag = '1' then '(無效)' else '' end", Bsp.Utility.DisplayType.Full, "", "and OrganID = GroupID " & IIf(strCompID = "0", "", "and CompID = " & Bsp.Utility.Quote(strCompID)) _
                                & " and GroupID In (Select Distinct GroupID From Organization " & IIf(strCompID = "0", "", "Where CompID = " & Bsp.Utility.Quote(strCompID)) & ")", "Order By InValidFlag, GroupID")
            ddlGroupID.Items.Insert(0, New ListItem("---請選擇---", ""))

            '單位類別
            subLoadOrganColor(ddlOrgType, strCompID)

            '部門
            subLoadOrganColor(ddlDeptID, strCompID)

            '科組課
            Bsp.Utility.FillDDL(ddlOrganID, "eHRMSDB", "Organization", "RTRIM(OrganID)", "OrganName + case when InValidFlag = '1' then '(無效)' else '' end", Bsp.Utility.DisplayType.Full, "", "and OrganID <> DeptID " & IIf(strCompID = "0", "", "and CompID = " & Bsp.Utility.Quote(strCompID)), "Order By InValidFlag, OrganID")
            ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))

            '班別
            Bsp.Utility.FillDDL(ddlWTID, "eHRMSDB", "WorkTime", "distinct WTID", "", Bsp.Utility.DisplayType.OnlyID, "", IIf(strCompID = "0", "", "and CompID = " & Bsp.Utility.Quote(strCompID)))
            ddlWTID.Items.Insert(0, New ListItem("---請選擇---", ""))

            '企業團經歷部門
            subLoadOrganColor(ddlLogDept, strCompID)

            '企業團經歷科組課
            Bsp.Utility.FillDDL(ddlLogOrgan, "eHRMSDB", "Organization", "RTRIM(OrganID)", "OrganName + case when InValidFlag = '1' then '(無效)' else '' end", Bsp.Utility.DisplayType.Full, "", "and OrganID <> DeptID " & IIf(strCompID = "0", "", "and CompID = " & Bsp.Utility.Quote(strCompID)), "Order By InValidFlag, OrganID")
            ddlLogOrgan.Items.Insert(0, New ListItem("---請選擇---", ""))

            '企業團經歷職等
            Bsp.Utility.FillDDL(ddlLogRank, "eHRMSDB", "Rank", "distinct RankID", "RankID", Bsp.Utility.DisplayType.OnlyID, "", IIf(strCompID = "0", "", "and CompID = " & Bsp.Utility.Quote(strCompID)))
            ddlLogRank.Items.Insert(0, New ListItem("---請選擇---", ""))

            '企業團經歷職稱
            ddlLogTitle.Items.Clear()
            ddlLogTitle.Items.Insert(0, New ListItem("---請選擇---", ""))

            ScriptManager.RegisterClientScriptBlock(UpdMain, UpdMain.GetType(), "", "ReLoadChkValue()", True)
        End If
    End Sub

    '調兼現況/記錄
    Protected Sub ddlEmpAddition_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEmpAddition.SelectedIndexChanged
        If ddlEmpAddition.SelectedValue <> "" Then
            ddlEmpAdditionComp.Enabled = True
            ddlEmpAdditionDept.Enabled = True
            ddlEmpAdditionReason.Enabled = True
            txtEmpAdditionDateB.Enabled = True
            txtEmpAdditionDateE.Enabled = True
        Else
            ddlEmpAdditionComp.Enabled = False
            ddlEmpAdditionDept.Enabled = False
            ddlEmpAdditionReason.Enabled = False
            txtEmpAdditionDateB.Enabled = False
            txtEmpAdditionDateE.Enabled = False

            ddlEmpAdditionComp.SelectedIndex = 0
            ddlEmpAdditionDept.Items.Clear()
            ddlEmpAdditionDept.Items.Insert(0, New ListItem("---請先選擇兼任公司---", ""))
            ddlEmpAdditionReason.SelectedIndex = 0
            txtEmpAdditionDateB.DateText = ""
            txtEmpAdditionDateE.DateText = ""

            ViewState.Item("AddDeptColors") = New List(Of ArrayList)()
        End If

        ScriptManager.RegisterClientScriptBlock(UpdMain, UpdMain.GetType(), "", "ReLoadChkValue()", True)
    End Sub

    '兼任公司
    Protected Sub ddlEmpAdditionComp_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEmpAdditionComp.SelectedIndexChanged
        If ddlEmpAdditionComp.SelectedValue <> "" Then
            subLoadOrganColor(ddlEmpAdditionDept, ddlEmpAdditionComp.SelectedValue, False)
        Else
            ddlEmpAdditionDept.Items.Clear()
            ddlEmpAdditionDept.Items.Insert(0, New ListItem("---請先選擇兼任公司---", ""))
            ViewState.Item("AddDeptColors") = New List(Of ArrayList)()
        End If

        ScriptManager.RegisterClientScriptBlock(UpdMain, UpdMain.GetType(), "", "ReLoadChkValue()", True)
    End Sub

    '部門
    Protected Sub ddlDept_Changed(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim strCompID As String = ddlCompID.SelectedValue
        If strCompID = "" Then
            strCompID = UserProfile.SelectCompRoleID
        End If

        Dim ddlDept As DropDownList = CType(sender, DropDownList)
        Dim ddlOrgan As DropDownList = Me.FindControl(ddlDept.ID.Replace("Dept", "Organ"))

        If ddlDept.SelectedValue = "" Then
            Bsp.Utility.FillDDL(ddlOrgan, "eHRMSDB", "Organization", "RTRIM(OrganID)", "OrganName + case when InValidFlag = '1' then '(無效)' else '' end", Bsp.Utility.DisplayType.Full, "", "and OrganID <> DeptID " & IIf(strCompID = "0", "", "and CompID = " & Bsp.Utility.Quote(strCompID)), "Order By InValidFlag, OrganID")
            ddlOrgan.Items.Insert(0, New ListItem("---請選擇---", ""))
        Else
            Bsp.Utility.FillDDL(ddlOrgan, "eHRMSDB", "Organization", "RTRIM(OrganID)", "OrganName + case when InValidFlag = '1' then '(無效)' else '' end", Bsp.Utility.DisplayType.Full, "", IIf(strCompID = "0", "", "and CompID = " & Bsp.Utility.Quote(strCompID)) & " and OrganID <> DeptID and DeptID = " & Bsp.Utility.Quote(ddlDept.SelectedValue), "Order By InValidFlag, OrganID")
            ddlOrgan.Items.Insert(0, New ListItem("---請選擇---", ""))
        End If

        ScriptManager.RegisterClientScriptBlock(UpdMain, UpdMain.GetType(), "", "ReLoadChkValue()", True)
    End Sub

    '職等
    Protected Sub ddlRank_Changed(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim strCompID As String = ddlCompID.SelectedValue
        If strCompID = "" Then
            strCompID = UserProfile.SelectCompRoleID
        End If

        Dim ddlRank As DropDownList = CType(sender, DropDownList)
        Dim ddlTitle As DropDownList = Me.FindControl(ddlRank.ID.Replace("Rank", "Title"))

        If ddlRank.SelectedValue = "" Then
            ddlTitle.Items.Clear()
            ddlTitle.Items.Insert(0, New ListItem("---請先選擇職等---", ""))
        Else
            Bsp.Utility.FillDDL(ddlTitle, "eHRMSDB", "Title", "distinct TitleID", "TitleName", Bsp.Utility.DisplayType.Full, "", IIf(strCompID = "0", "", "and CompID = " & Bsp.Utility.Quote(strCompID)) & " and RankID = " & Bsp.Utility.Quote(ddlRank.SelectedValue))
            ddlTitle.Items.Insert(0, New ListItem("---請選擇---", ""))
        End If

        ScriptManager.RegisterClientScriptBlock(UpdMain, UpdMain.GetType(), "", "ReLoadChkValue()", True)
    End Sub

    '金控職等
    Protected Sub ddlHoldingRank_Changed(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim strCompID As String = ddlCompID.SelectedValue
        If strCompID = "" Then
            strCompID = UserProfile.SelectCompRoleID
        End If

        Dim ddlHoldingRank As DropDownList = CType(sender, DropDownList)
        Dim ddlHoldingTitle As DropDownList = Me.FindControl(ddlHoldingRank.ID.Replace("Rank", "Title"))

        If ddlHoldingRank.SelectedValue = "" Then
            ddlHoldingTitle.Items.Clear()
            ddlHoldingTitle.Items.Insert(0, New ListItem("---請先選擇金控職等---", ""))
        Else
            Bsp.Utility.FillDDL(ddlHoldingTitle, "eHRMSDB", "TitleByHolding", "distinct HoldingRankID", "TitleName", Bsp.Utility.DisplayType.Full, "", " and HoldingRankID = " & Bsp.Utility.Quote(ddlHoldingRank.SelectedValue))
            ddlHoldingTitle.Items.Insert(0, New ListItem("---請選擇---", ""))
        End If

        ScriptManager.RegisterClientScriptBlock(UpdMain, UpdMain.GetType(), "", "ReLoadChkValue()", True)
    End Sub

    '職位
    Protected Sub ucSelectPosition_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        '載入按鈕-職位選單畫面
        Dim strCompID As String = ddlCompID.SelectedValue
        If strCompID = "" Then
            strCompID = UserProfile.SelectCompRoleID
        End If

        Dim ucPosition As ASP.component_ucbuttonposition_ascx = CType(sender, UserControl)
        Dim ctl As Control = Me.FindControl(ucPosition.ID.Replace("uc", "hid"))

        ucPosition.QueryCompID = strCompID
        ucPosition.QueryEmpID = ""
        ucPosition.DefaultPosition = CType(ctl, HiddenField).Value
        ucPosition.QueryOrganID = ""
        ucPosition.Fields = New FieldState() { _
                New FieldState("PositionID", "職位代碼", True, True), _
                New FieldState("Remark", "職位名稱", True, True)}
    End Sub

    '工作性質
    Protected Sub ucSelectWorkType_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        '載入按鈕-工作性質選單畫面
        Dim strCompID As String = ddlCompID.SelectedValue
        If strCompID = "" Then
            strCompID = UserProfile.SelectCompRoleID
        End If

        Dim ucWorkType As ASP.component_ucbuttonworktype_ascx = CType(sender, UserControl)
        Dim ctl As Control = Me.FindControl(ucWorkType.ID.Replace("uc", "hid"))

        ucWorkType.QueryCompID = strCompID
        ucWorkType.QueryEmpID = ""
        ucWorkType.DefaultWorkType = CType(ctl, HiddenField).Value
        ucWorkType.QueryOrganID = ""
        ucWorkType.Fields = New FieldState() { _
                New FieldState("WorkTypeID", "工作性質代碼", True, True), _
                New FieldState("Remark", "工作性質名稱", True, True)}
    End Sub

    '校名
    Protected Sub ucSelectSchool_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        '載入按鈕-校名選單畫面

        ucSchool.QuerySQL = "Select SchoolID, SchoolID + '-' + Remark as School from School order by SchoolID"
        ucSchool.ConnStr = "eHRMSDB"
        ucSchool.DefaultValue = hidSchool.Value
        ucSchool.DefaultField = "SchoolID"
        ucSchool.Fields = New FieldState() { _
                New FieldState("Remark", "學校名稱", True, True), _
                New FieldState("SchoolID", "學校代碼", True, True)}
    End Sub

    '證照
    Protected Sub ucSelectCategory_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        '載入按鈕-證照選單畫面

        ucCategory.QuerySQL = "Select CategoryID, CategoryID + '-' + LicenseName as LicenseName from License order by CategoryID"
        ucCategory.ConnStr = "eHRMSDB"
        ucCategory.DefaultValue = hidCategory.Value
        ucCategory.DefaultField = "CategoryID"
        ucCategory.Fields = New FieldState() { _
                New FieldState("LicenseName", "證照名稱", True, True), _
                New FieldState("CategoryID", "證照代碼", True, True)}
    End Sub

    Protected Sub ddlCityCode_SelectedChanged(sender As Object, e As System.EventArgs)
        Dim objST1 As New ST1
        Dim ddlCityCode As DropDownList = CType(sender, DropDownList)
        Dim ddlAddrCode As DropDownList = CType(Me.FindControl(ddlCityCode.ID.Replace("City", "Addr")), DropDownList)

        If ddlCityCode.SelectedValue <> "" Then
            Bsp.Utility.FillDDL(ddlAddrCode, "eHRMSDB", "PostalCode", "AddrCode", "AreaCode", Bsp.Utility.DisplayType.Full, "", "AND AddrCode <> '' AND CityCode = " & Bsp.Utility.Quote(ddlCityCode.SelectedValue))
            ddlAddrCode.Items.Insert(0, New ListItem("---請選擇---", ""))
        Else
            ddlAddrCode.Items.Clear()
            ddlAddrCode.Items.Insert(0, New ListItem("---請選擇---", ""))
        End If
    End Sub

    Protected Sub ddlGradeYear_Changed(sender As Object, e As System.EventArgs) Handles ddlGradeYear.SelectedIndexChanged
        If ddlGradeYear.SelectedValue = "" Then
            ddlGrade.Items.Clear()
            ddlGrade.Items.Insert(0, New ListItem("---請先選擇考績年度---", ""))
        ElseIf ddlGrade.Items.Count = 1 Then
            Bsp.Utility.FillDDL(ddlGrade, "eHRMSDB", "HRCodeMap", "Code", "CodeCName", Bsp.Utility.DisplayType.Full, "", "and TabName = 'EmpGrade' and FldName = 'Grade' and NotShowFlag = '0'", "Order By SortFld")
            ddlGrade.Items.Insert(0, New ListItem("ALL-全部", "ALL"))
        End If

        ScriptManager.RegisterClientScriptBlock(UpdMain, UpdMain.GetType(), "", "ReLoadChkValue()", True)
    End Sub

    Public Overrides Sub DoModalReturn(ByVal returnValue As String)
        Dim strSql As String = ""
        Dim strCompID As String = ddlCompID.SelectedValue
        If strCompID = "" Then
            strCompID = UserProfile.SelectCompRoleID
        End If

        If returnValue <> "" Then
            Dim aryData() As String = returnValue.Split(":")

            Select Case aryData(0)
                Case "ucSelectEmpID"
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    txtEmpID.Text = aryValue(1)
                    txtName.Text = aryValue(2)
                    If UserProfile.SelectCompRoleID = "ALL" Then
                        Bsp.Utility.SetSelectedIndex(ddlCompID, aryValue(3))
                    End If

                Case "ucSchool"
                    hidSchool.Value = aryData(1)
                    If hidSchool.Value <> "''" Then  '非必填時，回傳空值
                        '載入 職位 下拉式選單
                        Bsp.Utility.FillDDL(ddlSchool, "eHRMSDB", "School", "SchoolID", "Remark", Bsp.Utility.DisplayType.Full, "", "And SchoolID in (" & hidSchool.Value & ")")
                    Else
                        hidSchool.Value = ""
                        ddlSchool.Items.Clear()
                    End If

                Case "ucCategory"
                    hidCategory.Value = aryData(1)
                    If hidCategory.Value <> "''" Then  '非必填時，回傳空值
                        '載入 職位 下拉式選單
                        Bsp.Utility.FillDDL(ddlCategory, "eHRMSDB", "License", "CategoryID", "LicenseName", Bsp.Utility.DisplayType.Full, "", "And CategoryID in (" & hidCategory.Value & ")")
                    Else
                        hidCategory.Value = ""
                        ddlCategory.Items.Clear()
                    End If

                Case Else
                    Dim objHID As HiddenField = CType(Me.FindControl(aryData(0).Replace("uc", "hid")), HiddenField)
                    Dim objDDL As DropDownList = CType(Me.FindControl(aryData(0).Replace("uc", "ddl")), DropDownList)
                    objHID.Value = aryData(1)

                    If aryData(0).IndexOf("Position") > 0 Then
                        If objHID.Value <> "''" Then  '非必填時，回傳空值
                            '載入 職位 下拉式選單
                            strSql = " and PositionID in (" + objHID.Value + ")"
                            Bsp.Utility.Position(objDDL, "PositionID", Bsp.Enums.DisplayType.Full, strSql)
                        Else
                            objHID.Value = ""
                            objDDL.Items.Clear()
                        End If

                    ElseIf aryData(0).IndexOf("WorkType") > 0 Then
                        If objHID.Value <> "''" Then  '非必填時，回傳空值
                            '載入 工作性質 下拉式選單
                            strSql = " and WorkTypeID in (" + objHID.Value + ")"
                            Bsp.Utility.WorkType(objDDL, "WorkTypeID", Bsp.Enums.DisplayType.Full, strSql)
                        Else
                            objHID.Value = ""
                            objDDL.Items.Clear()
                        End If
                    End If
            End Select
        End If
    End Sub

    Private Sub subLoadOrganColor(ByVal objDDL As DropDownList, ByVal strCompID As String, Optional ByVal IsShowInValid As Boolean = True)
        Dim strSQL As New StringBuilder
        strSQL.AppendLine("Select O.OrganID")
        strSQL.AppendLine(", Case When OrgType = DeptID Then OrganID + '-' + IIF (O.InValidFlag = 0, OrganName, OrganName + '(無效)')")
        strSQL.AppendLine("Else '　　' + OrganID + '-' + IIF (O.InValidFlag = 0, OrganName, OrganName + '(無效)') End OrganName")
        strSQL.AppendLine(", IsNull(W.Color, '#FFFFFF') Color")
        strSQL.AppendLine("From Organization O")
        strSQL.AppendLine("Left Join OrganColor_Web W On O.CompID = W.CompID and O.SortOrder = W.SortOrder")
        If objDDL.ID = "ddlOrgType" Then
            strSQL.AppendLine("Where OrganID = OrgType")
        Else
            strSQL.AppendLine("Where OrganID = DeptID")
        End If
        strSQL.AppendLine(IIf(strCompID = "0", "", "And O.CompID = " & Bsp.Utility.Quote(strCompID)))
        strSQL.AppendLine("And VirtualFlag = '0'")
        If Not IsShowInValid Then
            strSQL.AppendLine("And InValidFlag = '0'")
        End If
        strSQL.AppendLine("Order By O.InValidFlag, O.SortOrder, Right(O.GroupType, 1), O.GroupID, O.OrganID")

        Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
            objDDL.Items.Clear()
            Dim ArrColors As New List(Of ArrayList)()

            If dt.Rows.Count > 0 Then
                For Each item As DataRow In dt.Rows
                    Dim ListOpt As ListItem = New ListItem()
                    ListOpt.Value = item("OrganID").ToString()
                    ListOpt.Text = item("OrganName").ToString()

                    If item("Color").ToString().Trim() <> "#FFFFFF" Then
                        ListOpt.Attributes.Add("style", "background-color:" + item("Color").ToString().Trim())

                        Dim ArrColor As New ArrayList()
                        ArrColor.Add(item("OrganID").ToString())
                        ArrColor.Add(item("Color").ToString().Trim())
                        ArrColors.Add(ArrColor)
                    End If

                    objDDL.Items.Add(ListOpt)
                Next
            End If

            Select Case objDDL.ID
                Case "ddlOrgType"
                    objDDL.Items.Insert(0, New ListItem("---請選擇---", ""))
                    ViewState.Item("OrgTypeColors") = ArrColors
                Case "ddlDeptID"
                    objDDL.Items.Insert(0, New ListItem("---請選擇---", ""))
                    ViewState.Item("DeptColors") = ArrColors
                Case "ddlLogDept"
                    objDDL.Items.Insert(0, New ListItem("---請選擇---", ""))
                    ViewState.Item("LogDeptColors") = ArrColors
                Case "ddlEmpAdditionDept"
                    objDDL.Items.Insert(0, New ListItem("---請選擇---", ""))
                    ViewState.Item("AddDeptColors") = ArrColors
                Case "ddlOrgRange"
                    objDDL.Items.Insert(0, New ListItem("---請選擇部門---", ""))
                    ViewState.Item("OrgRangeColors") = ArrColors
            End Select
        End Using
    End Sub

    Private Sub subReLoadColor(ByVal objDDL As DropDownList)
        If objDDL.Items.Count > 0 Then
            Dim ArrColors As New List(Of ArrayList)()

            Select Case objDDL.ID
                Case "ddlOrgType"
                    ArrColors = ViewState.Item("OrgTypeColors")
                Case "ddlDeptID"
                    ArrColors = ViewState.Item("DeptColors")
                Case "ddlLogDept"
                    ArrColors = ViewState.Item("LogDeptColors")
                Case "ddlEmpAdditionDept"
                    ArrColors = ViewState.Item("AddDeptColors")
                Case "ddlOrgRange"
                    ArrColors = ViewState.Item("OrgRangeColors")
            End Select

            For Each item As ArrayList In ArrColors
                Dim list As ListItem = objDDL.Items.FindByValue(item(0))
                If Not list Is Nothing Then
                    list.Attributes.Add("style", "background-color:" + item(1))
                End If
            Next
        End If
    End Sub

    <System.Web.Services.WebMethod()> _
    Public Shared Function GetRankMapping(ByVal CompID As String, ByVal Rank As String) As String
        Dim objMA1 As New MA1

        If CompID = "undefined" Then
            CompID = UserProfile.SelectCompRoleID
        End If

        Return objMA1.GetRankMapping(CompID, Rank)
    End Function

#Region "下載"
    Private Sub DoDownload()
        Try
            If gvMain.Rows.Count > 0 Then
                '產出檔頭
                Dim strFileName As String = Bsp.Utility.GetNewFileName("MA1000綜合查詢-") & ".xls"
                Dim strXml As New StringBuilder()

                Response.ClearContent()
                Response.BufferOutput = True
                Response.Charset = "utf-8"
                Response.ContentType = "application/save-as"         '隱藏檔案網址路逕的下載
                Response.AddHeader("Content-Transfer-Encoding", "binary")
                Response.ContentEncoding = System.Text.Encoding.UTF8
                Response.AddHeader("content-disposition", "attachment; filename=" & Server.UrlPathEncode(strFileName))

                strXml.AppendLine("<?xml version='1.0'?><?mso-application progid='Excel.Sheet'?>")
                strXml.AppendLine("<Workbook xmlns='urn:schemas-microsoft-com:office:spreadsheet'")
                strXml.AppendLine("xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:x='urn:schemas-microsoft-com:office:excel'")
                strXml.AppendLine("xmlns:ss='urn:schemas-microsoft-com:office:spreadsheet' xmlns:html='http://www.w3.org/TR/REC-html40'>")
                strXml.AppendLine("<DocumentProperties xmlns='urn:schemas-microsoft-com:office:office'>")
                strXml.AppendLine("<Author></Author><LastAuthor></LastAuthor><Created>2010-09-08T14:07:11Z</Created><Company>mxh</Company><Version>1990</Version>")
                strXml.AppendLine("</DocumentProperties>")
                strXml.AppendLine("<Styles><Style ss:ID='Default' ss:Name='Normal'>")
                strXml.AppendLine("<Alignment ss:Vertical='Center'/>")
                strXml.AppendLine("<Borders/>")
                strXml.AppendLine("<Font ss:FontName='新細明體' x:CharSet='134' ss:Size='12'/>")
                strXml.AppendLine("<Interior/>")
                strXml.AppendLine("<NumberFormat/>")
                strXml.AppendLine("<Protection/>")
                strXml.AppendLine("</Style>")

                strXml.AppendLine("<Style ss:ID='Header'>")
                strXml.AppendLine("<Alignment ss:Horizontal='Center'/>")
                strXml.AppendLine("<Borders>")
                strXml.AppendLine("<Border ss:Position='Bottom' ss:LineStyle='Continuous' ss:Weight='1'/>")
                strXml.AppendLine("<Border ss:Position='Left' ss:LineStyle='Continuous' ss:Weight='1'/>")
                strXml.AppendLine("<Border ss:Position='Right' ss:LineStyle='Continuous' ss:Weight='1'/>")
                strXml.AppendLine("<Border ss:Position='Top' ss:LineStyle='Continuous' ss:Weight='1'/>")
                strXml.AppendLine("</Borders>")
                strXml.AppendLine("<Font ss:Bold='1' ss:Size='9'/>")
                strXml.AppendLine("</Style>")

                strXml.AppendLine("<Style ss:ID='border'><NumberFormat ss:Format='@'/>")
                strXml.AppendLine("<Borders>")
                strXml.AppendLine("<Border ss:Position='Bottom' ss:LineStyle='Continuous' ss:Weight='1'/>")
                strXml.AppendLine("<Border ss:Position='Left' ss:LineStyle='Continuous' ss:Weight='1'/>")
                strXml.AppendLine("<Border ss:Position='Right' ss:LineStyle='Continuous' ss:Weight='1'/>")
                strXml.AppendLine("<Border ss:Position='Top' ss:LineStyle='Continuous' ss:Weight='1'/>")
                strXml.AppendLine("</Borders>")
                strXml.AppendLine("<Font ss:Size='9'/>")
                strXml.AppendLine("</Style>")
                strXml.AppendLine("</Styles>")

                strXml = QueryCondtions(strXml)
                strXml = QueryResults(strXml)

                strXml.AppendLine("</Workbook>")

                Response.Write(strXml.ToString())
                'Response.End()
                Response.Flush()
                Response.SuppressContent = True
                ApplicationInstance.CompleteRequest()
            Else
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", "請先查詢有資料，才能下傳!")
            End If

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoDownload", ex)
        End Try
    End Sub

    Function QueryCondtions(ByVal strXml As StringBuilder) As StringBuilder
        Dim intEmpInfo As Integer = 0 '員工基本資料
        Dim intEmpSenInfo As Integer = 0 '員工年資資料
        Dim intAdditionInfo As Integer = 0 '員工調兼資料
        Dim intScoolInfo As Integer = 0 '員工學校類別
        Dim intFamilyInfo As Integer = 0 '員工眷屬資料
        Dim intLastJobInfo As Integer = 0 '員工前職經歷
        Dim intEmpLog As Integer = 0 '員工企業團經歷
        Dim intEmpLogRange As Integer = 0 '員工企業團經歷區間
        Dim intLicenseInfo As Integer = 0 '員工證照資料
        Dim intCommInfo As Integer = 0 '員工通訊資料
        Dim intGradeInfo As Integer = 0 '員工考績資料(重要)
        Dim intSalaryInfo As Integer = 0 '員工薪資資料(重要)

        Dim strEmpInfo As New StringBuilder() '員工基本資料
        Dim strEmpSenInfo As New StringBuilder() '員工年資資料
        Dim strAdditionInfo As New StringBuilder() '員工調兼資料
        Dim strScoolInfo As New StringBuilder() '員工學校類別
        Dim strFamilyInfo As New StringBuilder() '員工眷屬資料
        Dim strLastJobInfo As New StringBuilder() '員工前職經歷
        Dim strEmpLog As New StringBuilder() '員工企業團經歷
        Dim strEmpLogRange As New StringBuilder() '員工企業團經歷區間
        Dim strLicenseInfo As New StringBuilder() '員工證照資料
        Dim strCommInfo As New StringBuilder() '員工通訊資料
        Dim strGradeInfo As New StringBuilder() '員工考績資料(重要)
        Dim strSalaryInfo As New StringBuilder() '員工薪資資料(重要)

        Try
            Dim strTemp As String = ""
            Dim strCompID As String = ddlCompID.SelectedValue
            If strCompID = "" Then
                strCompID = UserProfile.SelectCompRoleName
            Else
                strCompID = ddlCompID.SelectedValue & "-" & ddlCompID.SelectedItem.Text
            End If

            strXml.AppendLine("<Worksheet ss:Name='MA1000綜合查詢-查詢條件'>")
            strXml.AppendLine("<Table x:FullColumns='1' x:FullRows='1'>")

            strXml.AppendLine("<Column ss:Width='100'/>")
            strXml.AppendLine("<Column ss:Width='100'/>")
            strXml.AppendLine("<Column ss:Width='500'/>")


            '員工基本資料
            '================================================================================================================================================

            If (txtEmpID.Text.Trim() <> "") Then
                strEmpInfo.AppendLine("<Row>")
                strEmpInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>員工編號</Data></Cell>")
                strEmpInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + txtEmpID.Text + "</Data></Cell>")
                strEmpInfo.AppendLine("</Row>")

                intEmpInfo = intEmpInfo + 1
            End If

            If (txtName.Text.Trim() <> "") Then
                strEmpInfo.AppendLine("<Row>")
                strEmpInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>員工姓名</Data></Cell>")
                strEmpInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + txtName.Text + "</Data></Cell>")
                strEmpInfo.AppendLine("</Row>")

                intEmpInfo = intEmpInfo + 1
            End If

            If (txtIDNo.Text.Trim() <> "") Then
                strEmpInfo.AppendLine("<Row>")
                strEmpInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>身份證字號</Data></Cell>")
                strEmpInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + txtIDNo.Text + "</Data></Cell>")
                strEmpInfo.AppendLine("</Row>")

                intEmpInfo = intEmpInfo + 1
            End If

            If (txtEngName.Text.Trim() <> "") Then
                strEmpInfo.AppendLine("<Row>")
                strEmpInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>英文姓名</Data></Cell>")
                strEmpInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + txtEngName.Text + "</Data></Cell>")
                strEmpInfo.AppendLine("</Row>")

                intEmpInfo = intEmpInfo + 1
            End If

            If (txtPassportName.Text.Trim() <> "") Then
                strEmpInfo.AppendLine("<Row>")
                strEmpInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>護照英文姓名</Data></Cell>")
                strEmpInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + txtPassportName.Text + "</Data></Cell>")
                strEmpInfo.AppendLine("</Row>")

                intEmpInfo = intEmpInfo + 1
            End If

            If (ddlSex.SelectedValue <> "") Then
                strEmpInfo.AppendLine("<Row>")
                strEmpInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>性別</Data></Cell>")
                strEmpInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + ddlSex.SelectedItem.Text + "</Data></Cell>")
                strEmpInfo.AppendLine("</Row>")

                intEmpInfo = intEmpInfo + 1
            End If

            If (txtBirthDateB.DateText <> "" Or txtBirthDateE.DateText <> "") Then
                strEmpInfo.AppendLine("<Row>")
                strEmpInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>生日</Data></Cell>")
                strEmpInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + txtBirthDateB.DateText + "~" + txtBirthDateE.DateText + "</Data></Cell>")
                strEmpInfo.AppendLine("</Row>")

                intEmpInfo = intEmpInfo + 1
            End If

            If (txtAgeB.Text <> "" Or txtAgeE.Text <> "") Then
                strEmpInfo.AppendLine("<Row>")
                strEmpInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>年齡</Data></Cell>")
                strEmpInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>")
                If (txtAgeB.Text <> "") Then
                    strEmpInfo.AppendLine(">= " + txtAgeB.Text)

                    If (txtAgeE.Text <> "") Then
                        strEmpInfo.AppendLine("~")
                    End If
                End If

                If (txtAgeE.Text <> "") Then
                    strEmpInfo.AppendLine("<= " + txtAgeE.Text)
                End If
                strEmpInfo.AppendLine("</Data></Cell>")
                strEmpInfo.AppendLine("</Row>")

                intEmpInfo = intEmpInfo + 1
            End If

            If (ddlEdu.SelectedValue <> "") Then
                strEmpInfo.AppendLine("<Row>")
                strEmpInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>學歷</Data></Cell>")
                strEmpInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + ddlEdu.SelectedItem.Text + "</Data></Cell>")
                strEmpInfo.AppendLine("</Row>") '

                intEmpInfo = intEmpInfo + 1
            End If

            If (ddlMarriage.SelectedValue <> "") Then
                strEmpInfo.AppendLine("<Row>")
                strEmpInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>婚姻狀況</Data></Cell>")
                strEmpInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + ddlMarriage.SelectedItem.Text + "</Data></Cell>")
                strEmpInfo.AppendLine("</Row>")

                intEmpInfo = intEmpInfo + 1
            End If

            If (ddlNation.SelectedValue <> "") Then
                strEmpInfo.AppendLine("<Row>")
                strEmpInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>身分別</Data></Cell>")
                strEmpInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + ddlNation.SelectedItem.Text + "</Data></Cell>")
                strEmpInfo.AppendLine("</Row>")

                intEmpInfo = intEmpInfo + 1
            End If

            If (txtIDExpireDateB.DateText <> "" Or txtIDExpireDateE.DateText <> "") Then
                strEmpInfo.AppendLine("<Row>")
                strEmpInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>工作證期限</Data></Cell>")
                strEmpInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + txtIDExpireDateB.DateText + "~" + txtIDExpireDateE.DateText + "</Data></Cell>")
                strEmpInfo.AppendLine("</Row>")

                intEmpInfo = intEmpInfo + 1
            End If

            If (ddlAboriginalFlag.SelectedValue <> "") Then
                strEmpInfo.AppendLine("<Row>")
                strEmpInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>原住民註記</Data></Cell>")
                strEmpInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + ddlAboriginalFlag.SelectedItem.Text + "</Data></Cell>")
                strEmpInfo.AppendLine("</Row>")

                intEmpInfo = intEmpInfo + 1
            End If

            If (ddlAboriginalTribe.SelectedValue <> "") Then
                strEmpInfo.AppendLine("<Row>")
                strEmpInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>原住民族別</Data></Cell>")
                strEmpInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + ddlAboriginalTribe.SelectedItem.Text + "</Data></Cell>")
                strEmpInfo.AppendLine("</Row>")

                intEmpInfo = intEmpInfo + 1
            End If

            If (ddlLocalHireFlag.SelectedValue <> "") Then
                strEmpInfo.AppendLine("<Row>")
                strEmpInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>Local Hire註記</Data></Cell>")
                strEmpInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + ddlLocalHireFlag.SelectedItem.Text + "</Data></Cell>")
                strEmpInfo.AppendLine("</Row>")

                intEmpInfo = intEmpInfo + 1
            End If

            If (ddlPassExamFlag.SelectedValue <> "") Then
                strEmpInfo.AppendLine("<Row>")
                strEmpInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>新員招考註記</Data></Cell>")
                strEmpInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + ddlPassExamFlag.SelectedItem.Text + "</Data></Cell>")
                strEmpInfo.AppendLine("</Row>")

                intEmpInfo = intEmpInfo + 1
            End If

            If (ddlEmpType.SelectedValue <> "") Then
                strEmpInfo.AppendLine("<Row>")
                strEmpInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>雇用類別</Data></Cell>")
                strEmpInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + ddlEmpType.SelectedItem.Text + "</Data></Cell>")
                strEmpInfo.AppendLine("</Row>")

                intEmpInfo = intEmpInfo + 1
            End If

            If (ddlWorkCode.SelectedValue <> "") Then
                strEmpInfo.AppendLine("<Row>")
                strEmpInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>任職狀況</Data></Cell>")
                strEmpInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + ddlWorkCode.SelectedItem.Text + "</Data></Cell>")
                strEmpInfo.AppendLine("</Row>")

                intEmpInfo = intEmpInfo + 1
            End If

            If (ddlProbMonth.SelectedValue <> "") Then
                strEmpInfo.AppendLine("<Row>")
                strEmpInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>試用期</Data></Cell>")
                strEmpInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + ddlProbMonth.SelectedItem.Text + "</Data></Cell>")
                strEmpInfo.AppendLine("</Row>")

                intEmpInfo = intEmpInfo + 1
            End If

            If (txtProbDateB.DateText <> "" Or txtProbDateE.DateText <> "") Then
                strEmpInfo.AppendLine("<Row>")
                strEmpInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>試用考核試滿日</Data></Cell>")
                strEmpInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + txtProbDateB.DateText + "~" + txtProbDateE.DateText + "</Data></Cell>")
                strEmpInfo.AppendLine("</Row>")

                intEmpInfo = intEmpInfo + 1
            End If

            If (txtEmpDateB.DateText <> "" Or txtEmpDateE.DateText <> "") Then
                strEmpInfo.AppendLine("<Row>")
                strEmpInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>公司到職日</Data></Cell>")
                strEmpInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + txtEmpDateB.DateText + "~" + txtEmpDateE.DateText + "</Data></Cell>")
                strEmpInfo.AppendLine("</Row>")

                intEmpInfo = intEmpInfo + 1
            End If

            If (txtSinopacEmpDateB.DateText <> "" Or txtSinopacEmpDateE.DateText <> "") Then
                strEmpInfo.AppendLine("<Row>")
                strEmpInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>企業團到職日</Data></Cell>")
                strEmpInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + txtSinopacEmpDateB.DateText + "~" + txtSinopacEmpDateE.DateText + "</Data></Cell>")
                strEmpInfo.AppendLine("</Row>")

                intEmpInfo = intEmpInfo + 1
            End If

            If (txtQuitDateB.DateText <> "" Or txtQuitDateE.DateText <> "") Then
                strEmpInfo.AppendLine("<Row>")
                strEmpInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>公司離職日</Data></Cell>")
                strEmpInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + txtQuitDateB.DateText + "~" + txtQuitDateE.DateText + "</Data></Cell>")
                strEmpInfo.AppendLine("</Row>")

                intEmpInfo = intEmpInfo + 1
            End If

            If (txtSinopacQuitDateB.DateText <> "" Or txtSinopacQuitDateE.DateText <> "") Then
                strEmpInfo.AppendLine("<Row>")
                strEmpInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>企業團離職日</Data></Cell>")
                strEmpInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + txtSinopacQuitDateB.DateText + "~" + txtSinopacQuitDateE.DateText + "</Data></Cell>")
                strEmpInfo.AppendLine("</Row>")

                intEmpInfo = intEmpInfo + 1
            End If

            If (txtRankBeginDateB.DateText <> "" Or txtRankBeginDateE.DateText <> "") Then
                strEmpInfo.AppendLine("<Row>")
                strEmpInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>最近升遷日/本階起始日</Data></Cell>")
                strEmpInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + txtRankBeginDateB.DateText + "~" + txtRankBeginDateE.DateText + "</Data></Cell>")
                strEmpInfo.AppendLine("</Row>")

                intEmpInfo = intEmpInfo + 1
            End If

            If (ddlWorkSite.SelectedValue <> "") Then
                strEmpInfo.AppendLine("<Row>")
                strEmpInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>工作地點</Data></Cell>")
                strEmpInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + ddlWorkSite.SelectedItem.Text + "</Data></Cell>")
                strEmpInfo.AppendLine("</Row>")

                intEmpInfo = intEmpInfo + 1
            End If

            If (ddlRankB.SelectedValue <> "" Or ddlRankE.SelectedValue <> "") Then
                strEmpInfo.AppendLine("<Row>")
                strEmpInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>職等</Data></Cell>")
                strEmpInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>")
                If (ddlRankB.SelectedValue <> "") Then
                    strEmpInfo.AppendLine(">= " + ddlRankB.SelectedItem.Text)

                    If (ddlRankE.SelectedValue <> "") Then
                        strEmpInfo.AppendLine("~")
                    End If
                End If

                If (ddlRankE.SelectedValue <> "") Then
                    strEmpInfo.AppendLine("<= " + ddlRankE.SelectedItem.Text)
                End If
                strEmpInfo.AppendLine("</Data></Cell>")
                strEmpInfo.AppendLine("</Row>")

                intEmpInfo = intEmpInfo + 1
            End If

            If (ddlTitleB.SelectedValue <> "" Or ddlTitleE.SelectedValue <> "") Then
                strEmpInfo.AppendLine("<Row>")
                strEmpInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>職稱</Data></Cell>")
                strEmpInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>")
                If (ddlTitleB.SelectedValue <> "") Then
                    strEmpInfo.AppendLine(">= " + ddlTitleB.SelectedItem.Text)

                    If (ddlTitleE.SelectedValue <> "") Then
                        strEmpInfo.AppendLine("~")
                    End If
                End If

                If (ddlTitleE.SelectedValue <> "") Then
                    strEmpInfo.AppendLine("<= " + ddlTitleE.SelectedItem.Text)
                End If
                strEmpInfo.AppendLine("</Data></Cell>")
                strEmpInfo.AppendLine("</Row>")

                intEmpInfo = intEmpInfo + 1
            End If

            If (ddlHoldingRankB.SelectedValue <> "" Or ddlHoldingRankE.SelectedValue <> "") Then
                strEmpInfo.AppendLine("<Row>")
                strEmpInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>金控職等</Data></Cell>")
                strEmpInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>")
                If (ddlHoldingRankB.SelectedValue <> "") Then
                    strEmpInfo.AppendLine(">= " + ddlHoldingRankB.SelectedItem.Text)

                    If (ddlHoldingRankE.SelectedValue <> "") Then
                        strEmpInfo.AppendLine("~")
                    End If
                End If

                If (ddlHoldingRankE.SelectedValue <> "") Then
                    strEmpInfo.AppendLine("<= " + ddlHoldingRankE.SelectedItem.Text)
                End If
                strEmpInfo.AppendLine("</Data></Cell>")
                strEmpInfo.AppendLine("</Row>")

                intEmpInfo = intEmpInfo + 1
            End If

            If (ddlHoldingTitleB.SelectedValue <> "" Or ddlHoldingTitleE.SelectedValue <> "") Then
                strEmpInfo.AppendLine("<Row>")
                strEmpInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>金控職稱</Data></Cell>")
                strEmpInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>")
                If (ddlHoldingTitleB.SelectedValue <> "") Then
                    strEmpInfo.AppendLine(">= " + ddlHoldingTitleB.SelectedItem.Text)

                    If (ddlHoldingTitleE.SelectedValue <> "") Then
                        strEmpInfo.AppendLine("~")
                    End If
                End If

                If (ddlHoldingTitleE.SelectedValue <> "") Then
                    strEmpInfo.AppendLine("<= " + ddlHoldingTitleE.SelectedItem.Text)
                End If
                strEmpInfo.AppendLine("</Data></Cell>")
                strEmpInfo.AppendLine("</Row>")

                intEmpInfo = intEmpInfo + 1
            End If

            If (ddlPublicTitle.SelectedValue <> "") Then
                strEmpInfo.AppendLine("<Row>")
                strEmpInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>對外職稱</Data></Cell>")
                strEmpInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + ddlPublicTitle.SelectedItem.Text + "</Data></Cell>")
                strEmpInfo.AppendLine("</Row>")

                intEmpInfo = intEmpInfo + 1
            End If

            If (ddlGroupID.SelectedValue <> "") Then
                strEmpInfo.AppendLine("<Row>")
                strEmpInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>事業群</Data></Cell>")
                strEmpInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + ddlGroupID.SelectedItem.Text + "</Data></Cell>")
                strEmpInfo.AppendLine("</Row>")

                intEmpInfo = intEmpInfo + 1
            End If

            If (ddlOrgType.SelectedValue <> "") Then
                strEmpInfo.AppendLine("<Row>")
                strEmpInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>單位類別</Data></Cell>")
                strEmpInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + ddlOrgType.SelectedItem.Text + "</Data></Cell>")
                strEmpInfo.AppendLine("</Row>")

                intEmpInfo = intEmpInfo + 1
            End If

            If (ddlDeptID.SelectedValue <> "") Then
                strEmpInfo.AppendLine("<Row>")
                strEmpInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>部門</Data></Cell>")
                strEmpInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + Strings.LTrim(ddlDeptID.SelectedItem.Text) + "</Data></Cell>")
                strEmpInfo.AppendLine("</Row>")

                intEmpInfo = intEmpInfo + 1
            End If

            If (ddlOrganID.SelectedValue <> "") Then
                strEmpInfo.AppendLine("<Row>")
                strEmpInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>科/組/課</Data></Cell>")
                strEmpInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + ddlOrganID.SelectedItem.Text + "</Data></Cell>")
                strEmpInfo.AppendLine("</Row>")

                intEmpInfo = intEmpInfo + 1
            End If

            If (ddlPosition1.Items.Count > 0) Then
                strTemp = ""
                For i As Integer = 0 To ddlPosition1.Items.Count - 1
                    strTemp += "," + ddlPosition1.Items(i).Text
                Next
                strTemp = strTemp.Substring(1)

                strEmpInfo.AppendLine("<Row>")
                strEmpInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>職位(主要)</Data></Cell>")
                strEmpInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + strTemp + "</Data></Cell>")
                strEmpInfo.AppendLine("</Row>")

                intEmpInfo = intEmpInfo + 1
            End If

            If (ddlPosition2.Items.Count > 0) Then
                strTemp = ""
                For i As Integer = 0 To ddlPosition2.Items.Count - 1
                    strTemp += "," + ddlPosition2.Items(i).Text
                Next
                strTemp = strTemp.Substring(1)

                strEmpInfo.AppendLine("<Row>")
                strEmpInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>職位(次要)</Data></Cell>")
                strEmpInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + strTemp + "</Data></Cell>")
                strEmpInfo.AppendLine("</Row>")

                intEmpInfo = intEmpInfo + 1
            End If

            If (ddlWorkType1.Items.Count > 0) Then
                strTemp = ""
                For i As Integer = 0 To ddlWorkType1.Items.Count - 1
                    strTemp += "," + ddlWorkType1.Items(i).Text
                Next
                strTemp = strTemp.Substring(1)

                strEmpInfo.AppendLine("<Row>")
                strEmpInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>工作性質(主要)</Data></Cell>")
                strEmpInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + strTemp + "</Data></Cell>")
                strEmpInfo.AppendLine("</Row>")

                intEmpInfo = intEmpInfo + 1
            End If

            If (ddlWorkType2.Items.Count > 0) Then
                strTemp = ""
                For i As Integer = 0 To ddlWorkType2.Items.Count - 1
                    strTemp += "," + ddlWorkType2.Items(i).Text
                Next
                strTemp = strTemp.Substring(1)

                strEmpInfo.AppendLine("<Row>")
                strEmpInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>工作性質(次要)</Data></Cell>")
                strEmpInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + strTemp + "</Data></Cell>")
                strEmpInfo.AppendLine("</Row>")

                intEmpInfo = intEmpInfo + 1
            End If

            If (ddlWTID.SelectedValue <> "") Then
                strEmpInfo.AppendLine("<Row>")
                strEmpInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>公司班別</Data></Cell>")
                strEmpInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + ddlWTID.SelectedItem.Text + "</Data></Cell>")
                strEmpInfo.AppendLine("</Row>")

                intEmpInfo = intEmpInfo + 1
            End If

            If (ddlOfficeLoginFlag.SelectedValue <> "") Then
                strEmpInfo.AppendLine("<Row>")
                strEmpInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>刷卡註記(證券)</Data></Cell>")
                strEmpInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + ddlOfficeLoginFlag.SelectedItem.Text + "</Data></Cell>")
                strEmpInfo.AppendLine("</Row>")

                intEmpInfo = intEmpInfo + 1
            End If

            strXml.AppendLine("<Row>")
            strXml.AppendLine("<Cell ss:MergeDown='" + intEmpInfo.ToString() + "' ss:StyleID='Header'><Data ss:Type='String'>員工基本資料</Data></Cell>")
            strXml.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>公司代碼</Data></Cell>")
            strXml.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + strCompID + "</Data></Cell>")
            strXml.AppendLine("</Row>")

            strXml = strXml.AppendLine(strEmpInfo.ToString())

            '員工年資資料
            '================================================================================================================================================

            If (txtEmpSeniorityB.Text <> "" Or txtEmpSeniorityE.Text <> "") Then
                strEmpSenInfo.AppendLine("<Row>")
                strEmpSenInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>公司年資</Data></Cell>")
                strEmpSenInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>")
                If (txtEmpSeniorityB.Text <> "") Then
                    strEmpSenInfo.AppendLine(">= " + txtEmpSeniorityB.Text)

                    If (txtEmpSeniorityE.Text <> "") Then
                        strEmpSenInfo.AppendLine("~")
                    End If
                End If

                If (txtEmpSeniorityE.Text <> "") Then
                    strEmpSenInfo.AppendLine("<= " + txtEmpSeniorityE.Text)
                End If
                strEmpSenInfo.AppendLine("</Data></Cell>")
                strEmpSenInfo.AppendLine("</Row>")

                intEmpSenInfo = intEmpSenInfo + 1
            End If

            If (txtSinopacEmpSeniorityB.Text <> "" Or txtSinopacEmpSeniorityE.Text <> "") Then
                strEmpSenInfo.AppendLine("<Row>")
                strEmpSenInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>企業團年資</Data></Cell>")
                strEmpSenInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>")
                If (txtSinopacEmpSeniorityB.Text <> "") Then
                    strEmpSenInfo.AppendLine(">= " + txtSinopacEmpSeniorityB.Text)

                    If (txtSinopacEmpSeniorityE.Text <> "") Then
                        strEmpSenInfo.AppendLine("~")
                    End If
                End If

                If (txtSinopacEmpSeniorityE.Text <> "") Then
                    strEmpSenInfo.AppendLine("<= " + txtSinopacEmpSeniorityE.Text)
                End If
                strEmpSenInfo.AppendLine("</Data></Cell>")
                strEmpSenInfo.AppendLine("</Row>")

                intEmpSenInfo = intEmpSenInfo + 1
            End If

            If (txtEmpSenOrgTypeB.Text <> "" Or txtEmpSenOrgTypeE.Text <> "") Then
                strEmpSenInfo.AppendLine("<Row>")
                strEmpSenInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>單位年資</Data></Cell>")
                strEmpSenInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>")
                If (txtEmpSenOrgTypeB.Text <> "") Then
                    strEmpSenInfo.AppendLine(">= " + txtEmpSenOrgTypeB.Text)

                    If (txtEmpSenOrgTypeE.Text <> "") Then
                        strEmpSenInfo.AppendLine("~")
                    End If
                End If

                If (txtEmpSenOrgTypeE.Text <> "") Then
                    strEmpSenInfo.AppendLine("<= " + txtEmpSenOrgTypeE.Text)
                End If
                strEmpSenInfo.AppendLine("</Data></Cell>")
                strEmpSenInfo.AppendLine("</Row>")

                intEmpSenInfo = intEmpSenInfo + 1
            End If

            If (txtEmpConSenOrgTypeB.Text <> "" Or txtEmpConSenOrgTypeE.Text <> "") Then
                strEmpSenInfo.AppendLine("<Row>")
                strEmpSenInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>單位年資(連續)</Data></Cell>")
                strEmpSenInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>")
                If (txtEmpConSenOrgTypeB.Text <> "") Then
                    strEmpSenInfo.AppendLine(">= " + txtEmpConSenOrgTypeB.Text)

                    If (txtEmpConSenOrgTypeE.Text <> "") Then
                        strEmpSenInfo.AppendLine("~")
                    End If
                End If

                If (txtEmpConSenOrgTypeE.Text <> "") Then
                    strEmpSenInfo.AppendLine("<= " + txtEmpConSenOrgTypeE.Text)
                End If
                strEmpSenInfo.AppendLine("</Data></Cell>")
                strEmpSenInfo.AppendLine("</Row>")

                intEmpSenInfo = intEmpSenInfo + 1
            End If

            If (txtEmpSenOrgTypeFlowB.Text <> "" Or txtEmpSenOrgTypeFlowE.Text <> "") Then
                strEmpSenInfo.AppendLine("<Row>")
                strEmpSenInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>簽核單位年資</Data></Cell>")
                strEmpSenInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>")
                If (txtEmpSenOrgTypeFlowB.Text <> "") Then
                    strEmpSenInfo.AppendLine(">= " + txtEmpSenOrgTypeFlowB.Text)

                    If (txtEmpSenOrgTypeFlowE.Text <> "") Then
                        strEmpSenInfo.AppendLine("~")
                    End If
                End If

                If (txtEmpSenOrgTypeFlowE.Text <> "") Then
                    strEmpSenInfo.AppendLine("<= " + txtEmpSenOrgTypeFlowE.Text)
                End If
                strEmpSenInfo.AppendLine("</Data></Cell>")
                strEmpSenInfo.AppendLine("</Row>")

                intEmpSenInfo = intEmpSenInfo + 1
            End If

            If (txtEmpSenRankB.Text <> "" Or txtEmpSenRankE.Text <> "") Then
                strEmpSenInfo.AppendLine("<Row>")
                strEmpSenInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>職等年資</Data></Cell>")
                strEmpSenInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>")
                If (txtEmpSenRankB.Text <> "") Then
                    strEmpSenInfo.AppendLine(">= " + txtEmpSenRankB.Text)

                    If (txtEmpSenRankE.Text <> "") Then
                        strEmpSenInfo.AppendLine("~")
                    End If
                End If

                If (txtEmpSenRankE.Text <> "") Then
                    strEmpSenInfo.AppendLine("<= " + txtEmpSenRankE.Text)
                End If
                strEmpSenInfo.AppendLine("</Data></Cell>")
                strEmpSenInfo.AppendLine("</Row>")

                intEmpSenInfo = intEmpSenInfo + 1
            End If

            If (txtEmpSenPositionB.Text <> "" Or txtEmpSenPositionE.Text <> "") Then
                strEmpSenInfo.AppendLine("<Row>")
                strEmpSenInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>職位年資</Data></Cell>")
                strEmpSenInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>")
                If (txtEmpSenPositionB.Text <> "") Then
                    strEmpSenInfo.AppendLine(">= " + txtEmpSenPositionB.Text)

                    If (txtEmpSenPositionE.Text <> "") Then
                        strEmpSenInfo.AppendLine("~")
                    End If
                End If

                If (txtEmpSenPositionE.Text <> "") Then
                    strEmpSenInfo.AppendLine("<= " + txtEmpSenPositionE.Text)
                End If
                strEmpSenInfo.AppendLine("</Data></Cell>")
                strEmpSenInfo.AppendLine("</Row>")

                intEmpSenInfo = intEmpSenInfo + 1
            End If

            If (txtEmpConSenPositionB.Text <> "" Or txtEmpConSenPositionE.Text <> "") Then
                strEmpSenInfo.AppendLine("<Row>")
                strEmpSenInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>職位年資(連續)</Data></Cell>")
                strEmpSenInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>")
                If (txtEmpConSenPositionB.Text <> "") Then
                    strEmpSenInfo.AppendLine(">= " + txtEmpConSenPositionB.Text)

                    If (txtEmpConSenPositionE.Text <> "") Then
                        strEmpSenInfo.AppendLine("~")
                    End If
                End If

                If (txtEmpConSenPositionE.Text <> "") Then
                    strEmpSenInfo.AppendLine("<= " + txtEmpConSenPositionE.Text)
                End If
                strEmpSenInfo.AppendLine("</Data></Cell>")
                strEmpSenInfo.AppendLine("</Row>")

                intEmpSenInfo = intEmpSenInfo + 1
            End If

            If (txtEmpSenWorkTypeB.Text <> "" Or txtEmpSenWorkTypeE.Text <> "") Then
                strEmpSenInfo.AppendLine("<Row>")
                strEmpSenInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>工作性質年資</Data></Cell>")
                strEmpSenInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>")
                If (txtEmpSenWorkTypeB.Text <> "") Then
                    strEmpSenInfo.AppendLine(">= " + txtEmpSenWorkTypeB.Text)

                    If (txtEmpSenWorkTypeE.Text <> "") Then
                        strEmpSenInfo.AppendLine("~")
                    End If
                End If

                If (txtEmpSenWorkTypeE.Text <> "") Then
                    strEmpSenInfo.AppendLine("<= " + txtEmpSenWorkTypeE.Text)
                End If
                strEmpSenInfo.AppendLine("</Data></Cell>")
                strEmpSenInfo.AppendLine("</Row>")

                intEmpSenInfo = intEmpSenInfo + 1
            End If

            If (txtEmpConSenWorkTypeB.Text <> "" Or txtEmpConSenWorkTypeE.Text <> "") Then
                strEmpSenInfo.AppendLine("<Row>")
                strEmpSenInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>工作性質年資(連續)</Data></Cell>")
                strEmpSenInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>")
                If (txtEmpConSenWorkTypeB.Text <> "") Then
                    strEmpSenInfo.AppendLine(">= " + txtEmpConSenWorkTypeB.Text)

                    If (txtEmpConSenWorkTypeE.Text <> "") Then
                        strEmpSenInfo.AppendLine("~")
                    End If
                End If

                If (txtEmpConSenWorkTypeE.Text <> "") Then
                    strEmpSenInfo.AppendLine("<= " + txtEmpConSenWorkTypeE.Text)
                End If
                strEmpSenInfo.AppendLine("</Data></Cell>")
                strEmpSenInfo.AppendLine("</Row>")

                intEmpSenInfo = intEmpSenInfo + 1
            End If

            If (intEmpSenInfo > 0) Then
                strEmpSenInfo.Insert(5, "<Cell ss:MergeDown='" + (intEmpSenInfo - 1).ToString() + "' ss:StyleID='Header'><Data ss:Type='String'>員工年資資料</Data></Cell>")

                strXml = strXml.AppendLine(strEmpSenInfo.ToString())
            End If

            '員工調兼資料
            '================================================================================================================================================

            If (ddlEmpAddition.SelectedValue <> "") Then
                strAdditionInfo.AppendLine("<Row>")
                strAdditionInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>調兼現況/記錄</Data></Cell>")
                strAdditionInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + ddlEmpAddition.SelectedItem.Text + "</Data></Cell>")
                strAdditionInfo.AppendLine("</Row>")

                intAdditionInfo = intAdditionInfo + 1
            End If

            If (ddlEmpAdditionComp.SelectedValue <> "") Then
                strAdditionInfo.AppendLine("<Row>")
                strAdditionInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>兼任公司</Data></Cell>")
                strAdditionInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + ddlEmpAdditionComp.SelectedItem.Text + "</Data></Cell>")
                strAdditionInfo.AppendLine("</Row>")

                intAdditionInfo = intAdditionInfo + 1
            End If

            If (ddlEmpAdditionDept.SelectedValue <> "") Then
                strAdditionInfo.AppendLine("<Row>")
                strAdditionInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>兼任部門</Data></Cell>")
                strAdditionInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + Strings.LTrim(ddlEmpAdditionDept.SelectedItem.Text) + "</Data></Cell>")
                strAdditionInfo.AppendLine("</Row>")

                intAdditionInfo = intAdditionInfo + 1
            End If

            If (ddlEmpAdditionReason.SelectedValue <> "") Then
                strAdditionInfo.AppendLine("<Row>")
                strAdditionInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>兼任狀態</Data></Cell>")
                strAdditionInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + ddlEmpAdditionReason.SelectedItem.Text + "</Data></Cell>")
                strAdditionInfo.AppendLine("</Row>")

                intAdditionInfo = intAdditionInfo + 1
            End If

            If (txtEmpAdditionDateB.DateText <> "" Or txtEmpAdditionDateE.DateText <> "") Then
                strAdditionInfo.AppendLine("<Row>")
                strAdditionInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>兼任生效日期</Data></Cell>")
                strAdditionInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + txtEmpAdditionDateB.DateText + "~" + txtEmpAdditionDateE.DateText + "</Data></Cell>")
                strAdditionInfo.AppendLine("</Row>")

                intAdditionInfo = intAdditionInfo + 1
            End If

            If (intAdditionInfo > 0) Then
                strAdditionInfo.Insert(5, "<Cell ss:MergeDown='" + (intAdditionInfo - 1).ToString() + "' ss:StyleID='Header'><Data ss:Type='String'>員工調兼資料</Data></Cell>")

                strXml = strXml.AppendLine(strAdditionInfo.ToString())
            End If

            '員工學校類別
            '================================================================================================================================================

            If (ddlSchoolType.SelectedValue <> "") Then
                strScoolInfo.AppendLine("<Row>")
                strScoolInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>學校類別</Data></Cell>")
                strScoolInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + ddlSchoolType.SelectedItem.Text + "</Data></Cell>")
                strScoolInfo.AppendLine("</Row>")

                intScoolInfo = intScoolInfo + 1
            End If

            If (ddlSchool.Items.Count > 0) Then
                strTemp = ""
                For i As Integer = 0 To ddlSchool.Items.Count - 1
                    strTemp += "," + ddlSchool.Items(i).Text
                Next
                strTemp = strTemp.Substring(1)

                strScoolInfo.AppendLine("<Row>")
                strScoolInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>校名</Data></Cell>")
                strScoolInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + strTemp + "</Data></Cell>")
                strScoolInfo.AppendLine("</Row>")

                intScoolInfo = intScoolInfo + 1
            End If

            If (txtDepart.Text.Trim() <> "") Then
                strScoolInfo.AppendLine("<Row>")
                strScoolInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>科系</Data></Cell>")
                strScoolInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + txtDepart.Text + "</Data></Cell>")
                strScoolInfo.AppendLine("</Row>")

                intScoolInfo = intScoolInfo + 1
            End If

            If (intScoolInfo > 0) Then
                strScoolInfo.Insert(5, "<Cell ss:MergeDown='" + (intScoolInfo - 1).ToString() + "' ss:StyleID='Header'><Data ss:Type='String'>員工學校類別</Data></Cell>")

                strXml = strXml.AppendLine(strScoolInfo.ToString())
            End If

            '員工眷屬資料
            '================================================================================================================================================

            If (txtFamilyOccupation.Text.Trim() <> "") Then
                strFamilyInfo.AppendLine("<Row>")
                strFamilyInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>眷屬職業</Data></Cell>")
                strFamilyInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + txtFamilyOccupation.Text + "</Data></Cell>")
                strFamilyInfo.AppendLine("</Row>")

                intFamilyInfo = intFamilyInfo + 1
            End If

            If (txtFamilyIndustryType.Text.Trim() <> "") Then
                strFamilyInfo.AppendLine("<Row>")
                strFamilyInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>眷屬產業別</Data></Cell>")
                strFamilyInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + txtFamilyIndustryType.Text + "</Data></Cell>")
                strFamilyInfo.AppendLine("</Row>")

                intFamilyInfo = intFamilyInfo + 1
            End If

            If (txtFamilyCompany.Text.Trim() <> "") Then
                strFamilyInfo.AppendLine("<Row>")
                strFamilyInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>眷屬服務機構</Data></Cell>")
                strFamilyInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + txtFamilyCompany.Text + "</Data></Cell>")
                strFamilyInfo.AppendLine("</Row>")

                intFamilyInfo = intFamilyInfo + 1
            End If

            If (intFamilyInfo > 0) Then
                strFamilyInfo.Insert(5, "<Cell ss:MergeDown='" + (intFamilyInfo - 1).ToString() + "' ss:StyleID='Header'><Data ss:Type='String'>員工眷屬資料</Data></Cell>")

                strXml = strXml.AppendLine(strFamilyInfo.ToString())
            End If

            '員工通訊資料
            '================================================================================================================================================

            If (ddlRegCityCode.SelectedValue <> "") Then
                strCommInfo.AppendLine("<Row>")
                strCommInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>戶籍縣市別</Data></Cell>")
                strCommInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + ddlRegCityCode.SelectedItem.Text + "</Data></Cell>")
                strCommInfo.AppendLine("</Row>")

                intCommInfo = intCommInfo + 1
            End If

            If (ddlRegAddrCode.SelectedValue <> "") Then
                strCommInfo.AppendLine("<Row>")
                strCommInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>戶籍郵遞區號</Data></Cell>")
                strCommInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + ddlRegAddrCode.SelectedItem.Text + "</Data></Cell>")
                strCommInfo.AppendLine("</Row>")

                intCommInfo = intCommInfo + 1
            End If

            If (txtRegAddr.Text.Trim() <> "") Then
                strCommInfo.AppendLine("<Row>")
                strCommInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>戶籍地址</Data></Cell>")
                strCommInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + txtRegAddr.Text + "</Data></Cell>")
                strCommInfo.AppendLine("</Row>")

                intCommInfo = intCommInfo + 1
            End If

            If (ddlCommCityCode.SelectedValue <> "") Then
                strCommInfo.AppendLine("<Row>")
                strCommInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>通訊縣市別</Data></Cell>")
                strCommInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + ddlCommCityCode.SelectedItem.Text + "</Data></Cell>")
                strCommInfo.AppendLine("</Row>")

                intCommInfo = intCommInfo + 1
            End If

            If (ddlCommAddrCode.SelectedValue <> "") Then
                strCommInfo.AppendLine("<Row>")
                strCommInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>通訊郵遞區號</Data></Cell>")
                strCommInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + ddlCommAddrCode.SelectedItem.Text + "</Data></Cell>")
                strCommInfo.AppendLine("</Row>")

                intCommInfo = intCommInfo + 1
            End If

            If (txtCommAddr.Text.Trim() <> "") Then
                strCommInfo.AppendLine("<Row>")
                strCommInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>通訊地址</Data></Cell>")
                strCommInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + txtCommAddr.Text + "</Data></Cell>")
                strCommInfo.AppendLine("</Row>")

                intCommInfo = intCommInfo + 1
            End If

            If (intCommInfo > 0) Then
                strCommInfo.Insert(5, "<Cell ss:MergeDown='" + (intCommInfo - 1).ToString() + "' ss:StyleID='Header'><Data ss:Type='String'>員工通訊資料</Data></Cell>")

                strXml = strXml.AppendLine(strCommInfo.ToString())
            End If

            '員工前職經歷
            '================================================================================================================================================

            If (txtLastBeginDateB.DateText <> "" Or txtLastBeginDateE.DateText <> "") Then
                strLastJobInfo.AppendLine("<Row>")
                strLastJobInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>起日</Data></Cell>")
                strLastJobInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + txtLastBeginDateB.DateText + "~" + txtLastBeginDateE.DateText + "</Data></Cell>")
                strLastJobInfo.AppendLine("</Row>")

                intLastJobInfo = intLastJobInfo + 1
            End If

            If (txtLastEndDateB.DateText <> "" Or txtLastEndDateE.DateText <> "") Then
                strLastJobInfo.AppendLine("<Row>")
                strLastJobInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>迄日</Data></Cell>")
                strLastJobInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + txtLastEndDateB.DateText + "~" + txtLastEndDateE.DateText + "</Data></Cell>")
                strLastJobInfo.AppendLine("</Row>")

                intLastJobInfo = intLastJobInfo + 1
            End If

            If (ddlLastIndustryType.SelectedValue <> "") Then
                strLastJobInfo.AppendLine("<Row>")
                strLastJobInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>產業類別</Data></Cell>")
                strLastJobInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + ddlLastIndustryType.SelectedItem.Text + "</Data></Cell>")
                strLastJobInfo.AppendLine("</Row>") '

                intLastJobInfo = intLastJobInfo + 1
            End If

            If (txtLastCompany.Text.Trim() <> "") Then
                strLastJobInfo.AppendLine("<Row>")
                strLastJobInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>公司名稱</Data></Cell>")
                strLastJobInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + txtLastCompany.Text + "</Data></Cell>")
                strLastJobInfo.AppendLine("</Row>")

                intLastJobInfo = intLastJobInfo + 1
            End If

            If (txtLastDept.Text.Trim() <> "") Then
                strLastJobInfo.AppendLine("<Row>")
                strLastJobInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>部門</Data></Cell>")
                strLastJobInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + txtLastDept.Text + "</Data></Cell>")
                strLastJobInfo.AppendLine("</Row>")

                intLastJobInfo = intLastJobInfo + 1
            End If

            If (txtLastTitle.Text.Trim() <> "") Then
                strLastJobInfo.AppendLine("<Row>")
                strLastJobInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>職稱</Data></Cell>")
                strLastJobInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + txtLastTitle.Text + "</Data></Cell>")
                strLastJobInfo.AppendLine("</Row>")

                intLastJobInfo = intLastJobInfo + 1
            End If

            If (txtLastWorkType.Text.Trim() <> "") Then
                strLastJobInfo.AppendLine("<Row>")
                strLastJobInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>工作性質</Data></Cell>")
                strLastJobInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + txtLastWorkType.Text + "</Data></Cell>")
                strLastJobInfo.AppendLine("</Row>")

                intLastJobInfo = intLastJobInfo + 1
            End If

            If (txtLastWorkYearB.Text <> "" Or txtLastWorkYearE.Text <> "") Then
                strLastJobInfo.AppendLine("<Row>")
                strLastJobInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>年資</Data></Cell>")
                strLastJobInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>")
                If (txtLastWorkYearB.Text <> "") Then
                    strLastJobInfo.AppendLine(">= " + txtLastWorkYearB.Text)

                    If (txtLastWorkYearE.Text <> "") Then
                        strLastJobInfo.AppendLine("~")
                    End If
                End If

                If (txtLastWorkYearE.Text <> "") Then
                    strLastJobInfo.AppendLine("<= " + txtLastWorkYearE.Text)
                End If
                strLastJobInfo.AppendLine("</Data></Cell>")
                strLastJobInfo.AppendLine("</Row>")

                intLastJobInfo = intLastJobInfo + 1
            End If

            If (ddlProfession.SelectedValue <> "") Then
                strLastJobInfo.AppendLine("<Row>")
                strLastJobInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>專業記號</Data></Cell>")
                strLastJobInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + ddlProfession.SelectedItem.Text + "</Data></Cell>")
                strLastJobInfo.AppendLine("</Row>") '

                intLastJobInfo = intLastJobInfo + 1
            End If

            If (intLastJobInfo > 0) Then
                strLastJobInfo.Insert(5, "<Cell ss:MergeDown='" + (intLastJobInfo - 1).ToString() + "' ss:StyleID='Header'><Data ss:Type='String'>員工前職經歷</Data></Cell>")

                strXml = strXml.AppendLine(strLastJobInfo.ToString())
            End If

            '員工企業團經歷
            '================================================================================================================================================

            If (txtModifyDateB.DateText <> "" Or txtModifyDateE.DateText <> "") Then
                strEmpLog.AppendLine("<Row>")
                strEmpLog.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>生效日</Data></Cell>")
                strEmpLog.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + txtModifyDateB.DateText + "~" + txtModifyDateE.DateText + "</Data></Cell>")
                strEmpLog.AppendLine("</Row>")

                intEmpLog = intEmpLog + 1
            End If

            If (ddlReason.SelectedValue <> "") Then
                strEmpLog.AppendLine("<Row>")
                strEmpLog.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>異動原因</Data></Cell>")
                strEmpLog.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + ddlReason.SelectedItem.Text + "</Data></Cell>")
                strEmpLog.AppendLine("</Row>")

                intEmpLog = intEmpLog + 1
            End If

            If (ddlLogDept.SelectedValue <> "") Then
                strEmpLog.AppendLine("<Row>")
                strEmpLog.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>部門</Data></Cell>")
                strEmpLog.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + Strings.LTrim(ddlLogDept.SelectedItem.Text) + "</Data></Cell>")
                strEmpLog.AppendLine("</Row>")

                intEmpLog = intEmpLog + 1
            End If

            If (ddlLogOrgan.SelectedValue <> "") Then
                strEmpLog.AppendLine("<Row>")
                strEmpLog.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>科/組/課</Data></Cell>")
                strEmpLog.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + ddlLogOrgan.SelectedItem.Text + "</Data></Cell>")
                strEmpLog.AppendLine("</Row>")

                intEmpLog = intEmpLog + 1
            End If

            If (txtLogPosition.Text.Trim() <> "") Then
                strEmpLog.AppendLine("<Row>")
                strEmpLog.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>職位</Data></Cell>")
                strEmpLog.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + txtLogPosition.Text + "</Data></Cell>")
                strEmpLog.AppendLine("</Row>")

                intEmpLog = intEmpLog + 1
            End If

            If (txtLogWorkType.Text.Trim() <> "") Then
                strEmpLog.AppendLine("<Row>")
                strEmpLog.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>工作性質</Data></Cell>")
                strEmpLog.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + txtLogWorkType.Text + "</Data></Cell>")
                strEmpLog.AppendLine("</Row>")

                intEmpLog = intEmpLog + 1
            End If

            If (ddlLogRank.SelectedValue <> "") Then
                strEmpLog.AppendLine("<Row>")
                strEmpLog.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>職等</Data></Cell>")
                strEmpLog.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + ddlLogRank.SelectedItem.Text + "</Data></Cell>")
                strEmpLog.AppendLine("</Row>")

                intEmpLog = intEmpLog + 1
            End If

            If (ddlLogTitle.SelectedValue <> "") Then
                strEmpLog.AppendLine("<Row>")
                strEmpLog.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>職稱</Data></Cell>")
                strEmpLog.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + ddlLogTitle.SelectedItem.Text + "</Data></Cell>")
                strEmpLog.AppendLine("</Row>")

                intEmpLog = intEmpLog + 1
            End If

            If (intEmpLog > 0) Then
                strEmpLog.Insert(5, "<Cell ss:MergeDown='" + (intEmpLog - 1).ToString() + "' ss:StyleID='Header'><Data ss:Type='String'>員工企業團經歷</Data></Cell>")

                strXml = strXml.AppendLine(strEmpLog.ToString())
            End If

            '員工企業團經歷區間
            '================================================================================================================================================

            If (txtOrgRangeB.DateText <> "" Or txtOrgRangeE.DateText <> "") Then
                strEmpLogRange.AppendLine("<Row>")
                strEmpLogRange.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>曾任部門區間</Data></Cell>")
                strEmpLogRange.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + txtOrgRangeB.DateText + "~" + txtOrgRangeE.DateText + "</Data></Cell>")
                strEmpLogRange.AppendLine("</Row>")

                intEmpLogRange = intEmpLogRange + 1
            End If

            If (ddlOrgRange.SelectedValue <> "") Then
                strEmpLogRange.AppendLine("<Row>")
                strEmpLogRange.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>曾任部門</Data></Cell>")
                strEmpLogRange.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + ddlOrgRange.SelectedItem.Text + "</Data></Cell>")
                strEmpLogRange.AppendLine("</Row>")

                intEmpLogRange = intEmpLogRange + 1
            End If

            If (txtPositionRangeB.DateText <> "" Or txtPositionRangeE.DateText <> "") Then
                strEmpLogRange.AppendLine("<Row>")
                strEmpLogRange.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>曾任職位區間</Data></Cell>")
                strEmpLogRange.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + txtPositionRangeB.DateText + "~" + txtPositionRangeE.DateText + "</Data></Cell>")
                strEmpLogRange.AppendLine("</Row>")

                intEmpLogRange = intEmpLogRange + 1
            End If

            If (ddlPositionRange.SelectedValue <> "") Then
                strEmpLogRange.AppendLine("<Row>")
                strEmpLogRange.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>曾任職位</Data></Cell>")
                strEmpLogRange.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + ddlPositionRange.SelectedItem.Text + "</Data></Cell>")
                strEmpLogRange.AppendLine("</Row>")

                intEmpLogRange = intEmpLogRange + 1
            End If

            If (txtWorkTypeRangeB.DateText <> "" Or txtWorkTypeRangeE.DateText <> "") Then
                strEmpLogRange.AppendLine("<Row>")
                strEmpLogRange.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>曾任工作性質區間</Data></Cell>")
                strEmpLogRange.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + txtWorkTypeRangeB.DateText + "~" + txtWorkTypeRangeE.DateText + "</Data></Cell>")
                strEmpLogRange.AppendLine("</Row>")

                intEmpLogRange = intEmpLogRange + 1
            End If

            If (ddlWorkTypeRange.SelectedValue <> "") Then
                strEmpLogRange.AppendLine("<Row>")
                strEmpLogRange.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>曾任工作性質</Data></Cell>")
                strEmpLogRange.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + ddlWorkTypeRange.SelectedItem.Text + "</Data></Cell>")
                strEmpLogRange.AppendLine("</Row>")

                intEmpLogRange = intEmpLogRange + 1
            End If

            If (intEmpLogRange > 0) Then
                strEmpLogRange.Insert(5, "<Cell ss:MergeDown='" + (intEmpLogRange - 1).ToString() + "' ss:StyleID='Header'><Data ss:Type='String'>員工企業團經歷區間</Data></Cell>")

                strXml = strXml.AppendLine(strEmpLogRange.ToString())
            End If

            '員工證照資料
            '================================================================================================================================================

            If (ddlCategory.Items.Count > 0) Then
                strTemp = ""
                For i As Integer = 0 To ddlCategory.Items.Count - 1
                    strTemp += "," + ddlCategory.Items(i).Text
                Next
                strTemp = strTemp.Substring(1)

                strLicenseInfo.AppendLine("<Row>")
                strLicenseInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>證照代碼</Data></Cell>")
                strLicenseInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + strTemp + "</Data></Cell>")
                strLicenseInfo.AppendLine("</Row>")

                intLicenseInfo = intLicenseInfo + 1
            End If

            If (intLicenseInfo > 0) Then
                strLicenseInfo.Insert(5, "<Cell ss:MergeDown='" + (intLicenseInfo - 1).ToString() + "' ss:StyleID='Header'><Data ss:Type='String'>員工證照資料</Data></Cell>")

                strXml = strXml.AppendLine(strLicenseInfo.ToString())
            End If

            '員工考績資料(重要)
            '================================================================================================================================================

            If (ddlCommGrade.SelectedValue <> "") Then
                strGradeInfo.AppendLine("<Row>")
                strGradeInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>常用條件</Data></Cell>")
                strGradeInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + ddlCommGrade.SelectedItem.Text + "</Data></Cell>")
                strGradeInfo.AppendLine("</Row>")

                intGradeInfo = intGradeInfo + 1
            End If

            If (ddlGradeYear.SelectedValue <> "") Then
                strGradeInfo.AppendLine("<Row>")
                strGradeInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>考績年度</Data></Cell>")
                strGradeInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + ddlGradeYear.SelectedItem.Text + "</Data></Cell>")
                strGradeInfo.AppendLine("</Row>")

                intGradeInfo = intGradeInfo + 1
            End If

            If (ddlGrade.SelectedValue <> "") Then
                strGradeInfo.AppendLine("<Row>")
                strGradeInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>考績</Data></Cell>")
                strGradeInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + ddlGrade.SelectedItem.Text + "</Data></Cell>")
                strGradeInfo.AppendLine("</Row>")

                intGradeInfo = intGradeInfo + 1
            End If

            If (ddlLastGrade1.SelectedValue <> "") Then
                strGradeInfo.AppendLine("<Row>")
                strGradeInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>前一年考績</Data></Cell>")
                strGradeInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + ddlLastGrade1.SelectedItem.Text + "</Data></Cell>")
                strGradeInfo.AppendLine("</Row>")

                intGradeInfo = intGradeInfo + 1
            End If

            If (ddlLastGrade2.SelectedValue <> "") Then
                strGradeInfo.AppendLine("<Row>")
                strGradeInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>前二年考績</Data></Cell>")
                strGradeInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + ddlLastGrade2.SelectedItem.Text + "</Data></Cell>")
                strGradeInfo.AppendLine("</Row>")

                intGradeInfo = intGradeInfo + 1
            End If

            If (ddlLastGrade3.SelectedValue <> "") Then
                strGradeInfo.AppendLine("<Row>")
                strGradeInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>前三年考績</Data></Cell>")
                strGradeInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + ddlLastGrade3.SelectedItem.Text + "</Data></Cell>")
                strGradeInfo.AppendLine("</Row>")

                intGradeInfo = intGradeInfo + 1
            End If

            If (ddlLastGrade4.SelectedValue <> "") Then
                strGradeInfo.AppendLine("<Row>")
                strGradeInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>前四年考績</Data></Cell>")
                strGradeInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + ddlLastGrade4.SelectedItem.Text + "</Data></Cell>")
                strGradeInfo.AppendLine("</Row>")

                intGradeInfo = intGradeInfo + 1
            End If

            If (intGradeInfo > 0) Then
                strGradeInfo.Insert(5, "<Cell ss:MergeDown='" + (intGradeInfo - 1).ToString() + "' ss:StyleID='Header'><Data ss:Type='String'>員工考績資料(重要)</Data></Cell>")

                strXml = strXml.AppendLine(strGradeInfo.ToString())
            End If

            '員工薪資資料
            '================================================================================================================================================

            If (txtYearSalaryB.Text <> "" Or txtYearSalaryE.Text <> "") Then
                strSalaryInfo.AppendLine("<Row>")
                strSalaryInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>年薪</Data></Cell>")
                strSalaryInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>")
                If (txtYearSalaryB.Text <> "") Then
                    strSalaryInfo.AppendLine(">= " + txtYearSalaryB.Text)

                    If (txtYearSalaryE.Text <> "") Then
                        strSalaryInfo.AppendLine("~")
                    End If
                End If

                If (txtYearSalaryE.Text <> "") Then
                    strSalaryInfo.AppendLine("<= " + txtYearSalaryE.Text)
                End If
                strSalaryInfo.AppendLine("</Data></Cell>")
                strSalaryInfo.AppendLine("</Row>")

                intSalaryInfo = intSalaryInfo + 1
            End If

            If (txtMonthSalaryB.Text <> "" Or txtMonthSalaryE.Text <> "") Then
                strSalaryInfo.AppendLine("<Row>")
                strSalaryInfo.AppendLine("<Cell ss:Index='2' ss:StyleID='border'><Data ss:Type='String'>月薪</Data></Cell>")
                strSalaryInfo.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>")
                If (txtMonthSalaryB.Text <> "") Then
                    strSalaryInfo.AppendLine(">= " + txtMonthSalaryB.Text)

                    If (txtMonthSalaryE.Text <> "") Then
                        strSalaryInfo.AppendLine("~")
                    End If
                End If

                If (txtMonthSalaryE.Text <> "") Then
                    strSalaryInfo.AppendLine("<= " + txtMonthSalaryE.Text)
                End If
                strSalaryInfo.AppendLine("</Data></Cell>")
                strSalaryInfo.AppendLine("</Row>")

                intSalaryInfo = intSalaryInfo + 1
            End If

            If (intSalaryInfo > 0) Then
                strSalaryInfo.Insert(5, "<Cell ss:MergeDown='" + (intSalaryInfo - 1).ToString() + "' ss:StyleID='Header'><Data ss:Type='String'>員工薪資資料(重要)</Data></Cell>")

                strXml = strXml.AppendLine(strSalaryInfo.ToString())
            End If


            strXml.AppendLine("</Table>")
            strXml.AppendLine("</Worksheet>")

        Catch ex As Exception
            Throw ex
        End Try
        Return strXml
    End Function


    Function QueryResults(ByVal strXml As StringBuilder) As StringBuilder
        Dim objMA1 As New MA1()

        Try
            Dim strCompID As String = ddlCompID.SelectedValue
            If strCompID = "" Then
                strCompID = UserProfile.SelectCompRoleID
            End If

            Dim bGrade As String = ""
            Dim GradeYear As String = ""
            Dim GradeYearList As New ArrayList()
            If ddlCommGrade.SelectedValue <> "" Or ddlGradeYear.SelectedValue <> "" Or ddlLastGrade1.SelectedValue <> "" Or ddlLastGrade2.SelectedValue <> "" Or ddlLastGrade3.SelectedValue <> "" Or ddlLastGrade4.SelectedValue <> "" Then
                bGrade = "Y"

                Select Case ddlCommGrade.SelectedValue
                    Case "1" '1-近五年三甲
                        GradeYearList.Add(DateTime.Now.Year - 4)
                        GradeYearList.Add(DateTime.Now.Year - 3)
                        GradeYearList.Add(DateTime.Now.Year - 2)
                        GradeYearList.Add(DateTime.Now.Year - 1)
                        GradeYearList.Add(DateTime.Now.Year)
                    Case "2" '2-近三年兩乙
                        GradeYearList.Add(DateTime.Now.Year - 2)
                        GradeYearList.Add(DateTime.Now.Year - 1)
                        GradeYearList.Add(DateTime.Now.Year)
                    Case "3" '3-近兩年優
                        GradeYearList.Add(DateTime.Now.Year - 1)
                        GradeYearList.Add(DateTime.Now.Year)
                    Case "4" '4-近兩年乙
                        GradeYearList.Add(DateTime.Now.Year - 1)
                        GradeYearList.Add(DateTime.Now.Year)
                    Case "5" '5-近三年優
                        GradeYearList.Add(DateTime.Now.Year - 2)
                        GradeYearList.Add(DateTime.Now.Year - 1)
                        GradeYearList.Add(DateTime.Now.Year)
                End Select

                If ddlGradeYear.SelectedValue <> "" Then
                    GradeYearList.Add(Integer.Parse(ddlGradeYear.SelectedValue))
                End If

                If ddlLastGrade1.SelectedValue <> "" Then
                    GradeYearList.Add(DateTime.Now.Year - 1)
                End If
                If ddlLastGrade2.SelectedValue <> "" Then
                    GradeYearList.Add(DateTime.Now.Year - 2)
                End If
                If ddlLastGrade3.SelectedValue <> "" Then
                    GradeYearList.Add(DateTime.Now.Year - 3)
                End If
                If ddlLastGrade4.SelectedValue <> "" Then
                    GradeYearList.Add(DateTime.Now.Year - 4)
                End If
            End If

            If GradeYearList.Count > 0 Then
                GradeYearList.Sort()
                GradeYearList.Reverse()
                GradeYear = String.Join(",", GradeYearList.ToArray().Distinct())
            End If

            Using dt As DataTable = objMA1.CompositeDownloadQuery(
                ViewState.Item("QueryFlag"), _
                "CompID=" & strCompID, _
                "EmpID=" & GetEmpIDs(), _
                "Grade=" & bGrade, _
                "GradeYear=" & GradeYear, _
                "YearSalary=" & IIf(txtYearSalaryB.Text <> "" Or txtYearSalaryE.Text <> "", "Y", ""), _
                "MonthSalary=" & IIf(txtMonthSalaryB.Text <> "" Or txtMonthSalaryE.Text <> "", "Y", ""))

                strXml.AppendLine("<Worksheet ss:Name='MA1000綜合查詢-查詢結果'>")
                strXml.AppendLine("<Table x:FullColumns='1' x:FullRows='1'>")

                If dt.Rows.Count > 0 Then
                    For i As Integer = 1 To dt.Columns.Count - 1
                        strXml.AppendLine("<Column ss:Width='100'/>")
                    Next

                    strXml = GenDatas(strXml, dt)
                End If

                strXml.AppendLine("</Table>")
                strXml.AppendLine("</Worksheet>")
            End Using

            If txtEmpSenOrgTypeB.Text <> "" Or txtEmpSenOrgTypeE.Text <> "" Or txtEmpConSenOrgTypeB.Text <> "" Or txtEmpConSenOrgTypeE.Text <> "" Then
                Using dt As DataTable = objMA1.EmpSenOrgTypeDownload(
                    "CompID=" & strCompID, _
                    "EmpID=" & GetEmpIDs(), _
                    "EmpSenOrgTypeB=" & txtEmpSenOrgTypeB.Text, _
                    "EmpSenOrgTypeE=" & txtEmpSenOrgTypeE.Text, _
                    "EmpConSenOrgTypeB=" & txtEmpConSenOrgTypeB.Text, _
                    "EmpConSenOrgTypeE=" & txtEmpConSenOrgTypeE.Text)

                    If dt.Rows.Count > 0 Then
                        strXml.AppendLine("<Worksheet ss:Name='MA1000綜合查詢-單位年資'>")
                        strXml.AppendLine("<Table x:FullColumns='1' x:FullRows='1'>")

                        For i As Integer = 1 To dt.Columns.Count - 1
                            strXml.AppendLine("<Column ss:Width='100'/>")
                        Next

                        strXml = GenDatas(strXml, dt)
                    End If

                    strXml.AppendLine("</Table>")
                    strXml.AppendLine("</Worksheet>")
                End Using
            End If

            If txtEmpSenRankB.Text <> "" Or txtEmpSenRankE.Text <> "" Then
                Using dt As DataTable = objMA1.EmpSenRankDownload(
                    "CompID=" & strCompID, _
                    "EmpID=" & GetEmpIDs(), _
                    "EmpSenRankB=" & txtEmpSenRankB.Text, _
                    "EmpSenRankE=" & txtEmpSenRankE.Text)

                    If dt.Rows.Count > 0 Then
                        strXml.AppendLine("<Worksheet ss:Name='MA1000綜合查詢-職等年資'>")
                        strXml.AppendLine("<Table x:FullColumns='1' x:FullRows='1'>")

                        For i As Integer = 1 To dt.Columns.Count - 1
                            strXml.AppendLine("<Column ss:Width='100'/>")
                        Next

                        strXml = GenDatas(strXml, dt)
                    End If

                    strXml.AppendLine("</Table>")
                    strXml.AppendLine("</Worksheet>")
                End Using
            End If

            If txtEmpSenPositionB.Text <> "" Or txtEmpSenPositionE.Text <> "" Or txtEmpConSenPositionB.Text <> "" Or txtEmpConSenPositionE.Text <> "" Then
                Using dt As DataTable = objMA1.EmpSenPositionDownload(
                    "CompID=" & strCompID, _
                    "EmpID=" & GetEmpIDs(), _
                    "EmpSenPositionB=" & txtEmpSenPositionB.Text, _
                    "EmpSenPositionE=" & txtEmpSenPositionE.Text, _
                    "EmpConSenPositionB=" & txtEmpConSenPositionB.Text, _
                    "EmpConSenPositionE=" & txtEmpConSenPositionE.Text)

                    If dt.Rows.Count > 0 Then
                        strXml.AppendLine("<Worksheet ss:Name='MA1000綜合查詢-職位年資'>")
                        strXml.AppendLine("<Table x:FullColumns='1' x:FullRows='1'>")

                        For i As Integer = 1 To dt.Columns.Count - 1
                            strXml.AppendLine("<Column ss:Width='100'/>")
                        Next

                        strXml = GenDatas(strXml, dt)
                    End If

                    strXml.AppendLine("</Table>")
                    strXml.AppendLine("</Worksheet>")
                End Using
            End If

            If txtEmpSenWorkTypeB.Text <> "" Or txtEmpSenWorkTypeE.Text <> "" Or txtEmpConSenWorkTypeB.Text <> "" Or txtEmpConSenWorkTypeE.Text <> "" Then
                Using dt As DataTable = objMA1.EmpSenWorkTypeDownload(
                    "CompID=" & strCompID, _
                    "EmpID=" & GetEmpIDs(), _
                    "EmpSenWorkTypeB=" & txtEmpSenWorkTypeB.Text, _
                    "EmpSenWorkTypeE=" & txtEmpSenWorkTypeE.Text, _
                    "EmpConSenWorkTypeB=" & txtEmpConSenWorkTypeB.Text, _
                    "EmpConSenWorkTypeE=" & txtEmpConSenWorkTypeE.Text)

                    If dt.Rows.Count > 0 Then
                        strXml.AppendLine("<Worksheet ss:Name='MA1000綜合查詢-工作性質年資'>")
                        strXml.AppendLine("<Table x:FullColumns='1' x:FullRows='1'>")

                        For i As Integer = 1 To dt.Columns.Count - 1
                            strXml.AppendLine("<Column ss:Width='100'/>")
                        Next

                        strXml = GenDatas(strXml, dt)
                    End If

                    strXml.AppendLine("</Table>")
                    strXml.AppendLine("</Worksheet>")
                End Using
            End If

        Catch ex As Exception
            Throw ex
        End Try

        Return strXml
    End Function

    Function GenDatas(ByVal strXml As StringBuilder, ByVal dt As DataTable) As StringBuilder
        strXml.AppendLine("<Row>")

        For i As Integer = 1 To dt.Columns.Count - 1
            strXml.AppendLine("<Cell ss:StyleID='Header'><Data ss:Type='String'>" + dt.Columns(i).ToString() + "</Data></Cell>")
        Next

        strXml.AppendLine("</Row>")

        For i As Integer = 0 To dt.Rows.Count - 1
            strXml.AppendLine("<Row>")

            For j As Integer = 1 To dt.Columns.Count - 1
                strXml.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + dt.Rows(i)(j).ToString() + "</Data></Cell>")
            Next
            strXml.AppendLine("</Row>")
        Next

        Return strXml
    End Function

    Function GetEmpIDs() As String
        Dim strEmpIDs As String = ""
        Dim dt As New DataTable

        For Each dr As DataRow In pcMain.DataTable.Rows
            strEmpIDs &= "," & Bsp.Utility.Quote(dr.Item("EmpID"))
        Next

        Return strEmpIDs.Substring(1)
    End Function
#End Region
End Class
