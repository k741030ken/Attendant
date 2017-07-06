'****************************************************
'功能說明：打卡參數設定
'建立人員：John Lin
'建立日期：2017.04.06
'****************************************************
Imports System.Data
Imports System.Data.Common
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Partial Class PO_PO1000
    Inherits PageBase
#Region "Page_Load"

    ''' <summary>
    ''' 起始
    ''' </summary>
    ''' <param name="sender">object</param>
    ''' <param name="e">EventArgs</param>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            Dim objSC As New SC

            lblCompIDtxt.Text = UserProfile.SelectCompRoleName

            ucSelectPosition.DefaultPosition = lblSelectPositionID.Text
            ucSelectPosition.QueryCompID = UserProfile.SelectCompRoleID
            ucSelectPosition.QueryEmpID = ""
            ucSelectPosition.QueryOrganID = ""
            ucSelectPosition.Fields = New FieldState() { _
            New FieldState("PositionID", "職位代碼", True, True), _
            New FieldState("Remark", "職位名稱", True, True)}

            ucSelectWorkType.DefaultWorkType = lblSelectWorkTypeID.Text
            ucSelectWorkType.QueryCompID = UserProfile.SelectCompRoleID
            ucSelectWorkType.QueryEmpID = ""
            ucSelectWorkType.QueryOrganID = ""
            ucSelectWorkType.Fields = New FieldState() { _
            New FieldState("WorkTypeID", "工作性質代碼", True, True), _
            New FieldState("Remark", "工作性質名稱", True, True)}

            initObject()

            '職稱代碼
            PA1.FillTitleByHolding(ddlHoldingRankID)
            ddlHoldingRankID.Items.Insert(0, New ListItem("---請選擇---", ""))

            subGetData(UserProfile.SelectCompRoleID)
        End If
    End Sub
#End Region

#Region "uc-Case DoModalReturn"
    Public Overrides Sub DoModalReturn(ByVal returnValue As String)
        Dim strSql As String = ""
        Dim strRstName1 As String = ""
        Dim strRstName2 As String = ""

        If returnValue <> "" Then
            Dim aryData() As String = returnValue.Split(":")
            Select Case aryData(0)

                Case "ucSelectPosition"
                    lblSelectPositionID.Text = aryData(1)
                    If lblSelectPositionID.Text <> "''" Then  '非必填時，回傳空值
                        '載入 職位 下拉式選單
                        strSql = "and PositionID in (" + lblSelectPositionID.Text + ") and CompID = '" + UserProfile.SelectCompRoleID + "'"
                        Bsp.Utility.Position(ddlPositionID, "PositionID", , strSql)
                        '第一筆為主要職位
                        Dim strDefaultValue() As String = lblSelectPositionID.Text.Replace("'", "").Split(",")
                        Dim strPosition As String = ""
                        Bsp.Utility.SetSelectedIndex(ddlPositionID, strDefaultValue(0))
                    Else
                        ddlPositionID.Items.Clear()
                        ddlPositionID.Items.Insert(0, New ListItem("---請選擇---", ""))
                    End If
                    ucSelectPosition.DefaultPosition = lblSelectPositionID.Text
                    lblPositionID.Text = lblSelectPositionID.Text.Replace("'", "")

                    '/*-----------------------------------------*/
                Case "ucSelectWorkType" '工作性質
                    lblSelectWorkTypeID.Text = aryData(1)
                    If lblSelectWorkTypeID.Text <> "''" Then  '非必填時，回傳空值
                        strSql = " and WorkTypeID in (" + lblSelectWorkTypeID.Text + ") and CompID = '" + UserProfile.SelectCompRoleID + "'"
                        Bsp.Utility.WorkType(ddlWorkTypeID, "WorkTypeID", , strSql)
                        '第一筆為主要工作性質
                        Dim strDefaultValue() As String = lblSelectWorkTypeID.Text.Replace("'", "").Split(",")
                        Dim strWorkType As String = ""
                        Bsp.Utility.SetSelectedIndex(ddlWorkTypeID, strDefaultValue(0))
                    Else
                        ddlWorkTypeID.Items.Clear()
                        ddlWorkTypeID.Items.Insert(0, New ListItem("---請選擇---", ""))
                    End If
                    ucSelectWorkType.DefaultWorkType = lblSelectWorkTypeID.Text
                    lblWorkTypeID.Text = lblSelectWorkTypeID.Text.Replace("'", "")
            End Select
        End If
    End Sub
#End Region
   
#Region "功能鍵處理邏輯"
    Public Overrides Sub DoAction(ByVal Param As String) '按鈕
        Select Case Param
            Case "btnActionC" '存檔
                If funCheckData() Then
                    doSave()
                End If
            Case "btnActionX" '清除
                DoClear()
        End Select
    End Sub
#End Region
    
#Region "畫面檢核"
    ''' <summary>
    ''' 畫面檢核
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function funCheckData() As Boolean

        '出勤異常時間
        If rbnDutyInBT.Checked = True Then
            If txtDutyInBT.Text = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblDutyInBT.Text)
                txtDutyInBT.Focus()
                Return False
            End If

            If IsNumeric(txtDutyInBT.Text) = False Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00050", lblDutyInBT.Text)
                txtDutyInBT.Focus()
                Return False
            Else
                If CInt(txtDutyInBT.Text.Trim) < 0 Then
                    Bsp.Utility.ShowFormatMessage(Me, "W_20720", lblDutyInBT.Text, 0)
                    txtDutyInBT.Focus()
                    Return False
                End If
            End If
        End If
        
        '退勤異常時間
        If rbnDutyOutBT.Checked = True Then
            If txtDutyOutBT.Text = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblDutyOutBT.Text)
                txtDutyOutBT.Focus()
                Return False
            End If

            If IsNumeric(txtDutyOutBT.Text) = False Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00050", lblDutyOutBT.Text)
                txtDutyOutBT.Focus()
                Return False
            Else
                If CInt(txtDutyOutBT.Text.Trim) < 0 Then
                    Bsp.Utility.ShowFormatMessage(Me, "W_20720", lblDutyOutBT.Text, 0)
                    txtDutyOutBT.Focus()
                    Return False
                End If
            End If
        End If

        '出勤打卡提醒時間
        If rbnPunchInBT.Checked = True Then
            If txtPunchInBT.Text = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblPunchInBT.Text)
                txtPunchInBT.Focus()
                Return False
            End If

            If IsNumeric(txtPunchInBT.Text) = False Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00050", lblPunchInBT.Text)
                txtPunchInBT.Focus()
                Return False
            Else
                If CInt(txtPunchInBT.Text.Trim) < 0 Then
                    Bsp.Utility.ShowFormatMessage(Me, "W_20720", lblPunchInBT.Text, 0)
                    txtPunchInBT.Focus()
                    Return False
                End If
            End If
        End If

        '出勤打卡自訂提醒內容
        If rbnCustomPunchInMsg.Checked = True Then
            If txtCustomPunchInMsg.Text = "" Then
                Bsp.Utility.ShowMessage(Me, "出勤打卡自訂提醒內容未輸入!")
                txtCustomPunchInMsg.Focus()
                Return False
            End If
        End If

        If txtCustomPunchInMsg.Text.Trim.Length > 200 Then
            Bsp.Utility.ShowMessage(Me, "出勤打卡自訂提醒內容不可超過200字!")
            txtCustomPunchInMsg.Focus()
            Return False
        End If

        '退勤打卡提醒時間
        If rbnPunchOutBT.Checked = True Then
            If txtPunchOutBT.Text = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblPunchOutBT.Text)
                txtPunchOutBT.Focus()
                Return False
            End If

            If IsNumeric(txtPunchOutBT.Text) = False Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00050", lblPunchOutBT.Text)
                txtPunchOutBT.Focus()
                Return False
            Else
                If CInt(txtPunchOutBT.Text.Trim) < 0 Then
                    Bsp.Utility.ShowFormatMessage(Me, "W_20720", lblPunchOutBT.Text, 0)
                    txtPunchOutBT.Focus()
                    Return False
                End If
            End If
        End If

        '退勤打卡自訂提醒內容
        If rbnCustomPunchOutMsg.Checked = True Then
            If txtCustomPunchOutMsg.Text = "" Then
                Bsp.Utility.ShowMessage(Me, "退勤打卡自訂提醒內容未輸入!")
                txtCustomPunchOutMsg.Focus()
                Return False
            End If
        End If

        If txtCustomPunchOutMsg.Text.Trim.Length > 200 Then
            Bsp.Utility.ShowMessage(Me, "退勤打卡自訂提醒內容不可超過200字!")
            txtCustomPunchOutMsg.Focus()
            Return False
        End If

        '打卡異常(處理公務) 提醒內容
        If rbnAffairSelf.Checked = True Then
            If txtAffairSelf.Text = "" Then
                Bsp.Utility.ShowMessage(Me, "打卡異常(處理公務) 自訂提醒內容未輸入!")
                txtAffairSelf.Focus()
                Return False
            End If
        End If

        If txtAffairSelf.Text.Trim.Length > 200 Then
            Bsp.Utility.ShowMessage(Me, "打卡異常(處理公務)自訂提醒內容不可超過200字!")
            txtAffairSelf.Focus()
            Return False
        End If

        '關懷內容(超過夜間10點)
        If rbnOVTenSelf.Checked = True Then
            If txtOVTenSelf.Text = "" Then
                Bsp.Utility.ShowMessage(Me, "關懷內容(超過夜間10點)自訂提醒內容未輸入!")
                txtOVTenSelf.Focus()
                Return False
            End If
        End If

        If txtOVTenSelf.Text.Trim.Length > 200 Then
            Bsp.Utility.ShowMessage(Me, "關懷內容(超過夜間10點)自訂提醒內容不可超過200字!")
            txtOVTenSelf.Focus()
            Return False
        End If

        '關懷女性員工內容
        If rbnFemaleSelf.Checked = True Then
            If txtFemaleSelf.Text = "" Then
                Bsp.Utility.ShowMessage(Me, "關懷女性員工內容自訂提醒內容未輸入!")
                txtFemaleSelf.Focus()
                Return False
            End If
        End If

        If txtFemaleSelf.Text.Trim.Length > 200 Then
            Bsp.Utility.ShowMessage(Me, "關懷女性員工內容自訂提醒內容不可超過200字!")
            txtFemaleSelf.Focus()
            Return False
        End If

        If rbnExcludeRankID.Checked = True Then
            If ddlHoldingRankID.SelectedValue = "" Then
                Bsp.Utility.ShowMessage(Me, "請選擇欲排除的金控職等!")
                ddlHoldingRankID.Focus()
                Return False
            End If
        End If

        If rbnExcludePositionID.Checked = True Then
            If lblPositionID.Text.ToString.Trim = "" Then
                Bsp.Utility.ShowMessage(Me, "請選擇欲排除的職位!")
                ucSelectPosition.Focus()
                Return False
            End If
        End If

        If rbnExcludeWorkTypeID.Checked = True Then
            If lblWorkTypeID.Text.ToString.Trim = "" Then
                Bsp.Utility.ShowMessage(Me, "請選擇欲排除的工作性質!")
                ucSelectWorkType.Focus()
                Return False
            End If
        End If

        Return True
    End Function
#End Region

#Region "private Method"
    ''' <summary>
    ''' 資料查詢
    ''' </summary>
    ''' <param name="CompID">公司代碼</param>
    ''' <remarks></remarks>
    Private Sub subGetData(ByVal CompID As String)
        Dim objSC As New SC()
        Dim bePunchPara As New bePunchPara.Row()
        Dim bsPunchPara As New bePunchPara.Service()

        ViewState.Item("hasData") = False
        bePunchPara.CompID.Value = UserProfile.SelectCompRoleID

        Try
            Using dt As DataTable = bsPunchPara.QueryByKey(bePunchPara).Tables(0)
                If dt.Rows.Count <= 0 Then  'DB無資料，各欄位帶預設值
                    lblCompIDtxt.Text = UserProfile.SelectCompRoleName
                    rbnDefaultDutyInBT.Checked = True
                    rbnDutyInBT.Checked = False
                    txtDutyInBT.Text = "15"
                    rbnDefaultDutyOutBT.Checked = True
                    rbnDutyOutBT.Checked = False
                    txtDutyOutBT.Text = "15"
                    rbnDefaultPunchInBT.Checked = True
                    rbnPunchInBT.Checked = False
                    txtPunchInBT.Text = "30"
                    ddlVacDayBeginTimeHH.SelectedValue = "08"
                    ddlVacDayBeginTimeMM.SelectedValue = "45"
                    ddlVacAMEndTimeHH.SelectedValue = "12"
                    ddlVacAMEndTimeMM.SelectedValue = "45"
                    ddlVacPMBeginTimeHH.SelectedValue = "13"
                    ddlVacPMBeginTimeMM.SelectedValue = "45"
                    ddlVacDayEndTimeHH.SelectedValue = "17"
                    ddlVacDayEndTimeMM.SelectedValue = "45"
                    rbnDefaultPunchInMsg.Checked = True
                    rbnCustomPunchInMsg.Checked = False
                    lblDefaultPunchInMsg.Text = "您的出勤打卡時間較正常上班(或值勤)時間早30分鐘，請說明原因。"
                    rbnDefaultPunchOutBT.Checked = True
                    rbnPunchOutBT.Checked = False
                    txtPunchOutBT.Text = "30"
                    rbnDefaultPunchOutMsg.Checked = True
                    rbnCustomPunchOutMsg.Checked = False
                    lblDefaultPunchOutMsg.Text = "您的退勤打卡時間較正常上班(或值勤)時間晚30分鐘，請說明原因。"
                    rbnAffairDefault.Checked = True
                    rbnAffairSelf.Checked = False
                    rbnOVTenDefault.Checked = True
                    rbnOVTenSelf.Checked = False
                    rbnFemaleDefault.Checked = True
                    rbnFemaleSelf.Checked = False
                    rbnNoExcludeRankID.Checked = True
                    rbnExcludeRankID.Checked = False
                    rbnNoExcludePositionID.Checked = True
                    rbnExcludePositionID.Checked = False
                    rbnNoExcludeWorkTypeID.Checked = True
                    rbnExcludeWorkTypeID.Checked = False
                    rbnNoRotate.Checked = True
                    rbnRotate.Checked = False
                    lblSelectPositionID.Text = "''"
                    lblPositionID.Text = ""
                    lblSelectWorkTypeID.Text = "''"
                    lblWorkTypeID.Text = ""

                    txtCustomPunchInMsg.Enabled = False
                    txtCustomPunchOutMsg.Enabled = False
                    txtAffairSelf.Enabled = False
                    txtOVTenSelf.Enabled = False
                    txtFemaleSelf.Enabled = False
                    txtDutyInBT.Enabled = False
                    txtDutyOutBT.Enabled = False
                    txtPunchInBT.Enabled = False
                    txtPunchOutBT.Enabled = False
                    ddlHoldingRankID.Enabled = False
                    ddlPositionID.Enabled = False
                    ddlWorkTypeID.Enabled = False

                Else    'DB有資料，各欄位值由DB帶入
                    ViewState.Item("hasData") = True
                    bePunchPara = New bePunchPara.Row(dt.Rows(0))
                    Dim ParajsStr As String = GetParaStr(bePunchPara.Para.Value)
                    Dim ParaoriData As JArray = GetParaData(ParajsStr)

                    lblCompIDtxt.Text = UserProfile.SelectCompRoleName

                    Dim strDutyInFlag As String = getRealValue(ParajsStr, ParaoriData, "DutyInFlag") : ViewState.Item("DutyInFlag") = strDutyInFlag
                    Dim strDutyOutFlag As String = getRealValue(ParajsStr, ParaoriData, "DutyOutFlag") : ViewState.Item("DutyOutFlag") = strDutyOutFlag
                    Dim strPunchInFlag As String = getRealValue(ParajsStr, ParaoriData, "PunchInFlag") : ViewState.Item("PunchInFlag") = strPunchInFlag
                    Dim strPunchOutFlag As String = getRealValue(ParajsStr, ParaoriData, "PunchOutFlag") : ViewState.Item("PunchOutFlag") = strPunchOutFlag

                    If strDutyInFlag = "0" Then
                        rbnDefaultDutyInBT.Checked = True
                    Else
                        rbnDutyInBT.Checked = True
                    End If

                    If strDutyOutFlag = "0" Then
                        rbnDefaultDutyOutBT.Checked = True
                    Else
                        rbnDutyOutBT.Checked = True
                    End If

                    If strPunchInFlag = "0" Then
                        rbnDefaultPunchInBT.Checked = True
                    Else
                        rbnPunchInBT.Checked = True
                    End If

                    If strPunchOutFlag = "0" Then
                        rbnDefaultPunchOutBT.Checked = True
                    Else
                        rbnPunchOutBT.Checked = True
                    End If

                    ddlVacDayBeginTimeHH.SelectedValue = bePunchPara.VacDayBeginTime.Value.ToString.Substring(0, 2) : ViewState.Item("VacDayBeginTime") = ddlVacDayBeginTimeHH.SelectedValue
                    ddlVacDayBeginTimeMM.SelectedValue = bePunchPara.VacDayBeginTime.Value.ToString.Substring(2, 2) : ViewState.Item("VacDayBeginTime") = ddlVacDayBeginTimeMM.SelectedValue
                    ddlVacAMEndTimeHH.SelectedValue = bePunchPara.VacAMEndTime.Value.ToString.Substring(0, 2) : ViewState.Item("VacAMEndTime") = ddlVacAMEndTimeHH.SelectedValue
                    ddlVacAMEndTimeMM.SelectedValue = bePunchPara.VacAMEndTime.Value.ToString.Substring(2, 2) : ViewState.Item("VacAMEndTime") = ddlVacAMEndTimeMM.SelectedValue
                    ddlVacPMBeginTimeHH.SelectedValue = bePunchPara.VacPMBeginTime.Value.ToString.Substring(0, 2) : ViewState.Item("VacPMBeginTim") = ddlVacPMBeginTimeHH.SelectedValue
                    ddlVacPMBeginTimeMM.SelectedValue = bePunchPara.VacPMBeginTime.Value.ToString.Substring(2, 2) : ViewState.Item("VacPMBeginTim") = ddlVacPMBeginTimeMM.SelectedValue
                    ddlVacDayEndTimeHH.SelectedValue = bePunchPara.VacDayEndTime.Value.ToString.Substring(0, 2) : ViewState.Item("VacDayEndTime") = ddlVacDayEndTimeHH.SelectedValue
                    ddlVacDayEndTimeMM.SelectedValue = bePunchPara.VacDayEndTime.Value.ToString.Substring(2, 2) : ViewState.Item("VacDayEndTime") = ddlVacDayEndTimeMM.SelectedValue
                    '出勤異常時間
                    txtDutyInBT.Text = getRealValue(ParajsStr, ParaoriData, "DutyInBT") : ViewState.Item("DutyInBT") = txtDutyInBT.Text
                    '退勤異常時間
                    txtDutyOutBT.Text = getRealValue(ParajsStr, ParaoriData, "DutyOutBT") : ViewState.Item("DutyOutBT") = txtDutyOutBT.Text
                    '出勤打卡提示時間
                    txtPunchInBT.Text = getRealValue(ParajsStr, ParaoriData, "PunchInBT") : ViewState.Item("PunchInBT") = txtPunchInBT.Text
                    '退勤打卡提示時間
                    txtPunchOutBT.Text = getRealValue(ParajsStr, ParaoriData, "PunchOutBT") : ViewState.Item("PunchOutBT") = txtPunchOutBT.Text

                    Dim MsgParajsStr As String = GetParaStr(bePunchPara.MsgPara.Value)
                    Dim MsgParaoriData As JArray = GetParaData(MsgParajsStr)

                    Dim strPunchInMsgFlag As String = getRealValue(MsgParajsStr, MsgParaoriData, "PunchInMsgFlag") : ViewState.Item("PunchInMsgFlag") = strPunchInMsgFlag
                    Dim strPunchOutMsgFlag As String = getRealValue(MsgParajsStr, MsgParaoriData, "PunchOutMsgFlag") : ViewState.Item("PunchOutMsgFlag") = strPunchOutMsgFlag
                    Dim strAffairMsgFlag As String = getRealValue(MsgParajsStr, MsgParaoriData, "AffairMsgFlag") : ViewState.Item("AffairMsgFlag") = strAffairMsgFlag
                    Dim strOVTenMsgFlag As String = getRealValue(MsgParajsStr, MsgParaoriData, "OVTenMsgFlag") : ViewState.Item("OTLimitFlag") = strOVTenMsgFlag
                    Dim strFemaleMsgFlag As String = getRealValue(MsgParajsStr, MsgParaoriData, "FemaleMsgFlag") : ViewState.Item("FemaleMsgFlag") = strFemaleMsgFlag

                    If strPunchInMsgFlag = "0" Then
                        rbnDefaultPunchInMsg.Checked = True
                        txtCustomPunchInMsg.Enabled = False
                    Else
                        rbnCustomPunchInMsg.Checked = True
                    End If

                    If strPunchOutMsgFlag = "0" Then
                        rbnDefaultPunchOutMsg.Checked = True
                        txtCustomPunchOutMsg.Enabled = False
                    Else
                        rbnCustomPunchOutMsg.Checked = True
                    End If

                    If strAffairMsgFlag = "0" Then
                        rbnAffairDefault.Checked = True
                        txtAffairSelf.Enabled = False
                    Else
                        rbnAffairSelf.Checked = True
                    End If

                    If strOVTenMsgFlag = "0" Then
                        rbnOVTenDefault.Checked = True
                        txtOVTenSelf.Enabled = False
                    Else
                        rbnOVTenSelf.Checked = True
                    End If

                    If strFemaleMsgFlag = "0" Then
                        rbnFemaleDefault.Checked = True
                        txtFemaleSelf.Enabled = False
                    Else
                        rbnFemaleSelf.Checked = True
                    End If

                    '預設出勤打卡提醒內容
                    lblDefaultPunchInMsg.Text = getRealValue(MsgParajsStr, MsgParaoriData, "PunchInDefaultContent") : ViewState.Item("PunchInDefaultContent") = lblDefaultPunchInMsg.Text
                    '自訂出勤打卡提醒內容
                    txtCustomPunchInMsg.Text = getRealValue(MsgParajsStr, MsgParaoriData, "PunchInSelfContent") : ViewState.Item("PunchInSelfContent") = txtCustomPunchInMsg.Text
                    '預設退勤打卡提醒內容
                    lblDefaultPunchOutMsg.Text = getRealValue(MsgParajsStr, MsgParaoriData, "PunchOutDefaultContent") : ViewState.Item("PunchOutDefaultContent") = lblDefaultPunchOutMsg.Text
                    '自訂退勤打卡提醒內容
                    txtCustomPunchOutMsg.Text = getRealValue(MsgParajsStr, MsgParaoriData, "PunchOutSelfContent") : ViewState.Item("PunchOutSelfContent") = txtCustomPunchOutMsg.Text
                    '自訂處理公務提醒內容
                    txtAffairSelf.Text = getRealValue(MsgParajsStr, MsgParaoriData, "AffairSelfContent") : ViewState.Item("AffairSelfContent") = txtAffairSelf.Text
                    '自訂超過10點關懷內容
                    txtOVTenSelf.Text = getRealValue(MsgParajsStr, MsgParaoriData, "OVTenSelfContent") : ViewState.Item("OVTenSelfContent") = txtOVTenSelf.Text
                    '關懷女性員工內容
                    txtFemaleSelf.Text = getRealValue(MsgParajsStr, MsgParaoriData, "FemaleSelfContent") : ViewState.Item("FemaleSelfContent") = txtFemaleSelf.Text

                    Dim ExcludeParajsStr As String = GetParaStr(bePunchPara.ExcludePara.Value)
                    Dim ExcludeParaoriData As JArray = GetParaData(ExcludeParajsStr)

                    Dim strHoldingRankIDFlag As String = getRealValue(ExcludeParajsStr, ExcludeParaoriData, "HoldingRankIDFlag") : ViewState.Item("HoldingRankIDFlag") = strDutyInFlag
                    Dim strPositionFlag As String = getRealValue(ExcludeParajsStr, ExcludeParaoriData, "PositionFlag") : ViewState.Item("PositionFlag") = strPositionFlag
                    Dim strWorkTypeFlag As String = getRealValue(ExcludeParajsStr, ExcludeParaoriData, "WorkTypeFlag") : ViewState.Item("WorkTypeFlag") = strWorkTypeFlag
                    Dim strRotateFlag As String = getRealValue(ExcludeParajsStr, ExcludeParaoriData, "RotateFlag") : ViewState.Item("RotateFlag") = strRotateFlag

                    If strHoldingRankIDFlag = "0" Then
                        rbnNoExcludeRankID.Checked = True
                        ddlHoldingRankID.Enabled = False
                    Else
                        rbnExcludeRankID.Checked = True
                    End If

                    If strPositionFlag = "0" Then
                        rbnNoExcludePositionID.Checked = True
                        ddlPositionID.Enabled = False
                    Else
                        rbnExcludePositionID.Checked = True
                    End If

                    If strWorkTypeFlag = "0" Then
                        rbnNoExcludeWorkTypeID.Checked = True
                        ddlWorkTypeID.Enabled = False
                    Else
                        rbnExcludeWorkTypeID.Checked = True
                    End If

                    If strRotateFlag = "0" Then
                        rbnNoRotate.Checked = True
                    Else
                        rbnRotate.Checked = True
                    End If

                    ddlHoldingRankID.SelectedValue = getRealValue(ExcludeParajsStr, ExcludeParaoriData, "HoldingRankID") : ViewState.Item("HoldingRankID") = ddlHoldingRankID.SelectedValue

                    lblPositionID.Text = getRealValue(ExcludeParajsStr, ExcludeParaoriData, "PositionID") : ViewState.Item("PositionID") = lblPositionID.Text

                    lblSelectPositionID.Text = "'" + lblPositionID.Text.Replace(",", "','") + "'"
                    ucSelectPosition.DefaultPosition = lblSelectPositionID.Text

                    lblWorkTypeID.Text = getRealValue(ExcludeParajsStr, ExcludeParaoriData, "WorkTypeID") : ViewState.Item("WorkTypeID") = ddlHoldingRankID.SelectedValue

                    lblSelectWorkTypeID.Text = "'" + lblWorkTypeID.Text.Replace(",", "','") + "'"
                    ucSelectWorkType.DefaultWorkType = lblSelectWorkTypeID.Text

                    Dim strPositionSql As String = "and PositionID in (" + lblSelectPositionID.Text + ") and CompID = '" + UserProfile.SelectCompRoleID + "'"
                    Bsp.Utility.Position(ddlPositionID, "PositionID", , strPositionSql)

                    Dim strWorkTypeSql As String = " and WorkTypeID in (" + lblSelectWorkTypeID.Text + ") and CompID = '" + UserProfile.SelectCompRoleID + "'"
                    Bsp.Utility.WorkType(ddlWorkTypeID, "WorkTypeID", , strWorkTypeSql)

                    '最後異動公司
                    If bePunchPara.LastChgComp.Value.Trim <> "" Then
                        lblLastChgComptxt.Text = bePunchPara.LastChgComp.Value + "-" + objSC.GetCompName(bePunchPara.LastChgComp.Value).Rows(0).Item("CompName").ToString
                    Else
                        lblLastChgComptxt.Text = ""
                    End If
                    '最後異動人員
                    If bePunchPara.LastChgID.Value.Trim <> "" Then
                        Dim UserName As String = objSC.GetSC_UserName(bePunchPara.LastChgComp.Value, bePunchPara.LastChgID.Value)
                        lblLastChgIDtxt.Text = bePunchPara.LastChgID.Value + IIf(UserName <> "", "-" + UserName, "")
                    Else
                        lblLastChgIDtxt.Text = ""
                    End If
                    '最後異動日期
                    lblLastChgDatetxt.Text = IIf(Format(bePunchPara.LastChgDate.Value, "yyyy/MM/dd") = "1900/01/01", "", bePunchPara.LastChgDate.Value.ToString("yyyy/MM/dd HH:mm:ss"))
                End If

            End Using
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & "subGetData", ex)
        End Try
    End Sub

    ''' <summary>
    ''' 存檔
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub doSave()
        Dim objPO As New PO1()
        Dim bePunchPara As New bePunchPara.Row()
        Dim bsPunchPara As New bePunchPara.Service()
        Dim bePunchDetailPara As New bePunchDetailPara.Row()
        Dim bsPunchDetailPara As New bePunchDetailPara.Service()

        Dim IsInsert As Boolean = IIf(ViewState.Item("hasData"), False, True) '是要Insert還是Update

        Dim ParajsAry As New JArray
        Dim ParajsObj As New JObject
        Dim ParajsStr As String = ""

        Dim MsgParajsAry As New JArray
        Dim MsgParajsObj As New JObject
        Dim MsgParajsStr As String = ""

        Dim ExcludeParajsAry As New JArray
        Dim ExcludeParajsObj As New JObject
        Dim ExcludeParajsStr As String = ""

        'PunchPara
        Dim strDutyInFlag As String = IIf(rbnDefaultDutyInBT.Checked = True, "0", "1")
        ParajsObj.Add("DutyInFlag", strDutyInFlag)
        ParajsObj.Add("DutyInBT", txtDutyInBT.Text)
        Dim strDutyOutFlag As String = IIf(rbnDefaultDutyOutBT.Checked = True, "0", "1")
        ParajsObj.Add("DutyOutFlag", strDutyOutFlag)
        ParajsObj.Add("DutyOutBT", txtDutyOutBT.Text)
        Dim strPunchInFlag As String = IIf(rbnDefaultPunchInBT.Checked = True, "0", "1")
        ParajsObj.Add("PunchInFlag", strPunchInFlag)
        ParajsObj.Add("PunchInBT", txtPunchInBT.Text)
        Dim strPunchOutFlag As String = IIf(rbnDefaultPunchOutBT.Checked = True, "0", "1")
        ParajsObj.Add("PunchOutFlag", strPunchOutFlag)
        ParajsObj.Add("PunchOutBT", txtPunchOutBT.Text)

        ParajsObj.Add("VacDayBeginTime", ddlVacDayBeginTimeHH.SelectedValue.ToString + ddlVacDayBeginTimeMM.SelectedValue.ToString)
        ParajsObj.Add("VacAMEndTime", ddlVacAMEndTimeHH.SelectedValue.ToString + ddlVacAMEndTimeMM.SelectedValue.ToString)
        ParajsObj.Add("VacPMBeginTime", ddlVacPMBeginTimeHH.SelectedValue.ToString + ddlVacPMBeginTimeMM.SelectedValue.ToString)
        ParajsObj.Add("VacDayEndTime", ddlVacDayEndTimeHH.SelectedValue + ddlVacDayEndTimeMM.SelectedValue.ToString)

        bePunchPara.DutyInFlag.Value = strDutyInFlag
        bePunchPara.DutyInBT.Value = txtDutyInBT.Text
        bePunchPara.DutyOutFlag.Value = strDutyOutFlag
        bePunchPara.DutyOutBT.Value = txtDutyOutBT.Text
        bePunchPara.PunchInFlag.Value = strPunchInFlag
        bePunchPara.PunchInBT.Value = txtPunchInBT.Text
        bePunchPara.PunchOutFlag.Value = strPunchOutFlag
        bePunchPara.PunchOutBT.Value = txtPunchOutBT.Text

        bePunchPara.VacDayBeginTime.Value = ddlVacDayBeginTimeHH.SelectedValue.ToString + ddlVacDayBeginTimeMM.SelectedValue.ToString
        bePunchPara.VacAMEndTime.Value = ddlVacAMEndTimeHH.SelectedValue.ToString + ddlVacAMEndTimeMM.SelectedValue.ToString
        bePunchPara.VacPMBeginTime.Value = ddlVacPMBeginTimeHH.SelectedValue.ToString + ddlVacPMBeginTimeMM.SelectedValue.ToString
        bePunchPara.VacDayEndTime.Value = ddlVacDayEndTimeHH.SelectedValue + ddlVacDayEndTimeMM.SelectedValue.ToString

        Dim strPunchInMsgFlag As String = IIf(rbnDefaultPunchInMsg.Checked = True, "0", "1")
        MsgParajsObj.Add("PunchInMsgFlag", strPunchInMsgFlag)
        MsgParajsObj.Add("PunchInDefaultContent", lblDefaultPunchInMsg.Text)
        MsgParajsObj.Add("PunchInSelfContent", txtCustomPunchInMsg.Text)
        Dim strPunchOutMsgFlag As String = IIf(rbnDefaultPunchOutMsg.Checked = True, "0", "1")
        MsgParajsObj.Add("PunchOutMsgFlag", strPunchOutMsgFlag)
        MsgParajsObj.Add("PunchOutDefaultContent", lblDefaultPunchOutMsg.Text)
        MsgParajsObj.Add("PunchOutSelfContent", txtCustomPunchOutMsg.Text)
        Dim strAffairMsgFlag As String = IIf(rbnAffairDefault.Checked = True, "0", "1")
        MsgParajsObj.Add("AffairMsgFlag", strAffairMsgFlag)
        MsgParajsObj.Add("AffairDefaultContent", lblAffairDefault.Text)
        MsgParajsObj.Add("AffairSelfContent", txtAffairSelf.Text)
        Dim strOVTenMsgFlag As String = IIf(rbnOVTenDefault.Checked = True, "0", "1")
        MsgParajsObj.Add("OVTenMsgFlag", strOVTenMsgFlag)
        MsgParajsObj.Add("OVTenDefaultContent", lblOVTenDefault.Text)
        MsgParajsObj.Add("OVTenSelfContent", txtOVTenSelf.Text)
        Dim strFemaleMsgFlag As String = IIf(rbnFemaleDefault.Checked = True, "0", "1")
        MsgParajsObj.Add("FemaleMsgFlag", strFemaleMsgFlag)
        MsgParajsObj.Add("FemaleDefaultContent", lblFemaleDefault.Text)
        MsgParajsObj.Add("FemaleSelfContent", txtFemaleSelf.Text)

        bePunchPara.PunchInMsgFlag.Value = strPunchInMsgFlag
        bePunchPara.PunchInDefaultContent.Value = lblDefaultPunchInMsg.Text
        bePunchPara.PunchInSelfContent.Value = txtCustomPunchInMsg.Text
        bePunchPara.PunchOutMsgFlag.Value = strPunchOutMsgFlag
        bePunchPara.PunchOutDefaultContent.Value = lblDefaultPunchOutMsg.Text
        bePunchPara.PunchOutSelfContent.Value = txtCustomPunchOutMsg.Text
        bePunchPara.AffairMsgFlag.Value = strAffairMsgFlag
        bePunchPara.AffairDefaultContent.Value = lblAffairDefault.Text
        bePunchPara.AffairSelfContent.Value = txtAffairSelf.Text
        bePunchPara.OVTenMsgFlag.Value = strOVTenMsgFlag
        bePunchPara.OVTenDefaultContent.Value = lblOVTenDefault.Text
        bePunchPara.OVTenSelfContent.Value = txtOVTenSelf.Text
        bePunchPara.FemaleMsgFlag.Value = strFemaleMsgFlag
        bePunchPara.FemaleDefaultContent.Value = lblFemaleDefault.Text
        bePunchPara.FemaleSelfContent.Value = txtFemaleSelf.Text

        Dim strHoldingRankIDFlag As String = IIf(rbnNoExcludeRankID.Checked = True, "0", "1")
        ExcludeParajsObj.Add("HoldingRankIDFlag", strHoldingRankIDFlag)
        ExcludeParajsObj.Add("HoldingRankID", ddlHoldingRankID.SelectedValue)
        Dim strPositionFlag As String = IIf(rbnNoExcludePositionID.Checked = True, "0", "1")
        ExcludeParajsObj.Add("PositionFlag", strPositionFlag)
        ExcludeParajsObj.Add("PositionID", lblPositionID.Text)
        Dim strWorkTypeFlag As String = IIf(rbnNoExcludeWorkTypeID.Checked = True, "0", "1")
        ExcludeParajsObj.Add("WorkTypeFlag", strWorkTypeFlag)
        ExcludeParajsObj.Add("WorkTypeID", lblWorkTypeID.Text)
        Dim strRotateFlag As String = IIf(rbnNoRotate.Checked = True, "0", "1")
        ExcludeParajsObj.Add("RotateFlag", strRotateFlag)

        bePunchPara.HoldingRankIDFlag.Value = strHoldingRankIDFlag
        bePunchPara.HoldingRankID.Value = ddlHoldingRankID.SelectedValue
        bePunchPara.PositionFlag.Value = strPositionFlag
        bePunchPara.Position.Value = lblPositionID.Text
        bePunchPara.WorkTypeFlag.Value = strWorkTypeFlag
        bePunchPara.WorkTypeID.Value = lblWorkTypeID.Text
        bePunchPara.RotateFlag.Value = strRotateFlag

        ParajsAry.Add(ParajsObj)
        MsgParajsAry.Add(MsgParajsObj)
        ExcludeParajsAry.Add(ExcludeParajsObj)

        ParajsStr = JsonConvert.SerializeObject(ParajsAry, Formatting.None)
        MsgParajsStr = JsonConvert.SerializeObject(MsgParajsAry, Formatting.None)
        ExcludeParajsStr = JsonConvert.SerializeObject(ExcludeParajsAry, Formatting.None)

        bePunchPara.CompID.Value = UserProfile.SelectCompRoleID
        
        bePunchPara.Para.Value = ParajsStr
        bePunchPara.MsgPara.Value = MsgParajsStr
        bePunchPara.ExcludePara.Value = ExcludeParajsStr
        bePunchPara.LastChgComp.Value = UserProfile.ActCompID
        bePunchPara.LastChgID.Value = UserProfile.ActUserID
        bePunchPara.LastChgDate.Value = Now

        If IsInsert Then
            If objPO.AddPunchParaSetting(bePunchPara) Then
                Bsp.Utility.ShowMessage(Me, "存檔成功!")    '顯示訊息
                subGetData(UserProfile.SelectCompRoleName)  '重新取得資料
            Else
                Bsp.Utility.ShowMessage(Me, "存檔失敗!")    '顯示訊息
            End If
        Else
            If objPO.UpdatePunchParaSetting(bePunchPara) Then
                Bsp.Utility.ShowMessage(Me, "存檔成功!")    '顯示訊息
                subGetData(UserProfile.SelectCompRoleName)  '重新取得資料
            Else
                Bsp.Utility.ShowMessage(Me, "存檔失敗!")    '顯示訊息
            End If
        End If


    End Sub

    ''' <summary>
    ''' 清除
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DoClear()
        subGetData(UserProfile.SelectCompRoleID)  '重新取得資料
    End Sub

    Public Sub initObject()

        '元件初始
        For Hr As Integer = 0 To 23 Step 1
            ddlVacDayBeginTimeHH.Items.Insert(Hr, New ListItem(IIf(Hr < 10, "0" + CStr(Hr), CStr(Hr)), IIf(Hr < 10, "0" + CStr(Hr), CStr(Hr))))
            ddlVacAMEndTimeHH.Items.Insert(Hr, New ListItem(IIf(Hr < 10, "0" + CStr(Hr), CStr(Hr)), IIf(Hr < 10, "0" + CStr(Hr), CStr(Hr))))
            ddlVacPMBeginTimeHH.Items.Insert(Hr, New ListItem(IIf(Hr < 10, "0" + CStr(Hr), CStr(Hr)), IIf(Hr < 10, "0" + CStr(Hr), CStr(Hr))))
            ddlVacDayEndTimeHH.Items.Insert(Hr, New ListItem(IIf(Hr < 10, "0" + CStr(Hr), CStr(Hr)), IIf(Hr < 10, "0" + CStr(Hr), CStr(Hr))))
        Next
        For Mt As Integer = 0 To 59 Step 1
            ddlVacDayBeginTimeMM.Items.Insert(Mt, New ListItem(IIf(Mt < 10, "0" + CStr(Mt), CStr(Mt)), IIf(Mt < 10, "0" + CStr(Mt), CStr(Mt))))
            ddlVacAMEndTimeMM.Items.Insert(Mt, New ListItem(IIf(Mt < 10, "0" + CStr(Mt), CStr(Mt)), IIf(Mt < 10, "0" + CStr(Mt), CStr(Mt))))
            ddlVacPMBeginTimeMM.Items.Insert(Mt, New ListItem(IIf(Mt < 10, "0" + CStr(Mt), CStr(Mt)), IIf(Mt < 10, "0" + CStr(Mt), CStr(Mt))))
            ddlVacDayEndTimeMM.Items.Insert(Mt, New ListItem(IIf(Mt < 10, "0" + CStr(Mt), CStr(Mt)), IIf(Mt < 10, "0" + CStr(Mt), CStr(Mt))))
        Next

    End Sub
#End Region
    
#Region "格式轉換"

    ''' <summary>
    ''' 取得Json內資料
    ''' </summary>
    ''' <param name="jsStr">Json字串</param>
    ''' <param name="oriData">Json陣列</param>
    ''' <param name="keyStr">Key值名稱</param>
    ''' <returns>資料字串</returns>
    ''' <remarks></remarks>
    Public Function getRealValue(ByVal jsStr As String, ByVal oriData As JArray, ByVal keyStr As String) As String
        Dim result As String = ""
        If jsStr.Contains(keyStr) Then
            result = oriData(0).Item(keyStr).ToString.Replace("""", "")
        End If
        Return result
    End Function
    ''' <summary>
    ''' 轉為字串
    ''' </summary>
    ''' <param name="Para">DB資料</param>
    ''' <returns>Json字串</returns>
    ''' <remarks></remarks>
    Public Function GetParaStr(ByVal Para As String) As String
        Dim result As String = ""
        result = Para
        Return result
    End Function
    ''' <summary>
    ''' 轉為陣列
    ''' </summary>
    ''' <param name="Para">Json字串</param>
    ''' <returns>Json陣列</returns>
    ''' <remarks></remarks>
    Public Function GetParaData(ByVal Para As String) As JArray
        Dim result As JArray
        result = JsonConvert.DeserializeObject(Of JArray)(Para)
        Return result
    End Function
#End Region

#Region "畫面事件"

    ''' <summary>
    ''' rbnDefaultDutyInBT_CheckedChanged
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub rbnDefaultDutyInBT_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbnDefaultDutyInBT.CheckedChanged
        txtDutyInBT.Enabled = False
    End Sub

    ''' <summary>
    ''' rbnDutyInBT_CheckedChanged
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub rbnDutyInBT_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbnDutyInBT.CheckedChanged
        txtDutyInBT.Enabled = True
    End Sub

    ''' <summary>
    ''' rbnDefaultDutyOutBT_CheckedChanged
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub rbnDefaultDutyOutBT_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbnDefaultDutyOutBT.CheckedChanged
        txtDutyOutBT.Enabled = False
    End Sub

    ''' <summary>
    ''' rbnDutyOutBT_CheckedChanged
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub rbnDutyOutBT_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbnDutyOutBT.CheckedChanged
        txtDutyOutBT.Enabled = True
    End Sub

    ''' <summary>
    ''' rbnDefaultPunchInBT_CheckedChanged
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub rbnDefaultPunchInBT_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbnDefaultPunchInBT.CheckedChanged
        txtPunchInBT.Enabled = False
    End Sub

    ''' <summary>
    ''' rbnPunchInBT_CheckedChanged
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub rbnPunchInBT_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbnPunchInBT.CheckedChanged
        txtPunchInBT.Enabled = True
    End Sub

    ''' <summary>
    ''' rbnDefaultPunchOutBT_CheckedChanged
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub rbnDefaultPunchOutBT_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbnDefaultPunchOutBT.CheckedChanged
        txtPunchOutBT.Enabled = False
    End Sub

    ''' <summary>
    ''' rbnPunchOutBT_CheckedChanged
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub rbnPunchOutBT_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbnPunchOutBT.CheckedChanged
        txtPunchOutBT.Enabled = True
    End Sub

    ''' <summary>
    ''' rbnDefaultPunchInMsg_CheckedChanged
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub rbnDefaultPunchInMsg_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbnDefaultPunchInMsg.CheckedChanged
        txtCustomPunchInMsg.Enabled = False
    End Sub

    ''' <summary>
    ''' rbnCustomPunchInMsg_CheckedChanged
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub rbnCustomPunchInMsg_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbnCustomPunchInMsg.CheckedChanged
        txtCustomPunchInMsg.Enabled = True
    End Sub

    ''' <summary>
    ''' rbnDefaultPunchOutMsg_CheckedChanged
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub rbnDefaultPunchOutMsg_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbnDefaultPunchOutMsg.CheckedChanged
        txtCustomPunchOutMsg.Enabled = False
    End Sub

    ''' <summary>
    ''' rbnCustomPunchOutMsg_CheckedChanged
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub rbnCustomPunchOutMsg_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbnCustomPunchOutMsg.CheckedChanged
        txtCustomPunchOutMsg.Enabled = True
    End Sub

    ''' <summary>
    ''' rbnAffairDefault_CheckedChanged
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub rbnAffairDefault_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbnAffairDefault.CheckedChanged
        txtAffairSelf.Enabled = False
    End Sub

    ''' <summary>
    ''' rbnAffairSelf_CheckedChanged
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub rbnAffairSelf_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbnAffairSelf.CheckedChanged
        txtAffairSelf.Enabled = True
    End Sub

    ''' <summary>
    ''' rbnOVTenDefault_CheckedChanged
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub rbnOVTenDefault_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbnOVTenDefault.CheckedChanged
        txtOVTenSelf.Enabled = False
    End Sub

    ''' <summary>
    ''' rbnOVTenSelf_CheckedChanged
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub rbnOVTenSelf_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbnOVTenSelf.CheckedChanged
        txtOVTenSelf.Enabled = True
    End Sub

    ''' <summary>
    ''' rbnFemaleDefault_CheckedChanged
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub rbnFemaleDefault_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbnFemaleDefault.CheckedChanged
        txtFemaleSelf.Enabled = False
    End Sub

    ''' <summary>
    ''' rbnFemaleSelf_CheckedChanged
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub rbnFemaleSelf_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbnFemaleSelf.CheckedChanged
        txtFemaleSelf.Enabled = True
    End Sub

    ''' <summary>
    ''' txtPunchInBT_TextChanged
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub txtPunchInBT_TextChanged(sender As Object, e As System.EventArgs) Handles txtPunchInBT.TextChanged
        If txtPunchInBT.Text.Trim <> "" Then
            lblDefaultPunchInMsg.Text = "您的出勤打卡時間較正常上班(或值勤)時間早" & txtPunchInBT.Text & "分鐘，請說明原因。"
        End If
    End Sub

    ''' <summary>
    ''' txtPunchOutBT_TextChanged
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub txtPunchOutBT_TextChanged(sender As Object, e As System.EventArgs) Handles txtPunchOutBT.TextChanged
        If txtPunchOutBT.Text.Trim <> "" Then
            lblDefaultPunchOutMsg.Text = "您的退勤打卡時間較正常上班(或值勤)時間晚" & txtPunchOutBT.Text & "分鐘，請說明原因。"
        End If
    End Sub

    ''' <summary>
    ''' rbnNoExcludeRankID_CheckedChanged
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub rbnNoExcludeRankID_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbnNoExcludeRankID.CheckedChanged
        ddlHoldingRankID.Enabled = False
    End Sub

    ''' <summary>
    ''' rbnExcludeRankID_CheckedChanged
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub rbnExcludeRankID_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbnExcludeRankID.CheckedChanged
        ddlHoldingRankID.Enabled = True
    End Sub

    ''' <summary>
    ''' rbnNoExcludePositionID_CheckedChanged
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub rbnNoExcludePositionID_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbnNoExcludePositionID.CheckedChanged
        ddlPositionID.Enabled = False
    End Sub

    ''' <summary>
    ''' rbnExcludePositionID_CheckedChanged
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub rbnExcludePositionID_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbnExcludePositionID.CheckedChanged
        ddlPositionID.Enabled = True
    End Sub

    ''' <summary>
    ''' rbnNoExcludeWorkTypeID_CheckedChanged
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub rbnNoExcludeWorkTypeID_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbnNoExcludeWorkTypeID.CheckedChanged
        ddlWorkTypeID.Enabled = False
    End Sub

    ''' <summary>
    ''' rbnExcludeWorkTypeID_CheckedChanged
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub rbnExcludeWorkTypeID_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbnExcludeWorkTypeID.CheckedChanged
        ddlWorkTypeID.Enabled = True
    End Sub
#End Region
End Class
