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
            subGetData(UserProfile.SelectCompRoleName)
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
        If txtDutyInBT.Text = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblDutyInBT.Text)
            txtDutyInBT.Focus()
            Return False
        End If

        If IsNumeric(txtDutyInBT.Text) = False Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00050", lblDutyInBT.Text)
            txtDutyInBT.Focus()
            Return False
        End If

        '退勤異常時間
        If txtDutyOutBT.Text = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblDutyOutBT.Text)
            txtDutyOutBT.Focus()
            Return False
        End If

        If IsNumeric(txtDutyOutBT.Text) = False Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00050", lblDutyOutBT.Text)
            txtDutyOutBT.Focus()
            Return False
        End If

        '出勤打卡提醒時間
        If txtPunchInBT.Text = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblPunchInBT.Text)
            txtPunchInBT.Focus()
            Return False
        End If

        If IsNumeric(txtPunchInBT.Text) = False Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00050", lblPunchInBT.Text)
            txtPunchInBT.Focus()
            Return False
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
        If txtPunchOutBT.Text = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblPunchOutBT.Text)
            txtPunchOutBT.Focus()
            Return False
        End If

        If IsNumeric(txtPunchOutBT.Text) = False Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00050", lblPunchOutBT.Text)
            txtPunchOutBT.Focus()
            Return False
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
                    rbnNotSpecialUnit.Checked = True
                    txtDutyInBT.Text = "15"
                    txtDutyOutBT.Text = "15"
                    txtPunchInBT.Text = "60"
                    rbnDefaultPunchInMsg.Checked = True
                    lblDefaultPunchInMsg.Text = "您的出勤打卡時間較正常上班(或值勤)時間早60分鐘，請說明原因。"
                    txtPunchOutBT.Text = "30"
                    rbnDefaultPunchOutMsg.Checked = True
                    lblDefaultPunchOutMsg.Text = "您的退勤打卡時間較正常上班(或值勤)時間晚30分鐘，請說明原因。"
                    rbnAffairDefault.Checked = True
                    rbnOVTenDefault.Checked = True
                    rbnFemaleDefault.Checked = True

                    txtCustomPunchInMsg.Enabled = False
                    txtCustomPunchOutMsg.Enabled = False
                    txtAffairSelf.Enabled = False
                    txtOVTenSelf.Enabled = False
                    txtFemaleSelf.Enabled = False
                Else    'DB有資料，各欄位值由DB帶入
                    ViewState.Item("hasData") = True
                    bePunchPara = New bePunchPara.Row(dt.Rows(0))
                    Dim ParajsStr As String = GetParaStr(bePunchPara.Para.Value)
                    Dim ParaoriData As JArray = GetParaData(ParajsStr)

                    lblCompIDtxt.Text = UserProfile.SelectCompRoleName
                    If bePunchPara.SpecialUnitFlag.Value = "0" Then
                        rbnNotSpecialUnit.Checked = True
                    Else
                        rbnIsSpecialUnit.Checked = True
                    End If

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

        Dim IsInsert As Boolean = IIf(ViewState.Item("hasData"), False, True) '是要Insert還是Update

        Dim ParajsAry As New JArray
        Dim ParajsObj As New JObject
        Dim ParajsStr As String = ""

        Dim MsgParajsAry As New JArray
        Dim MsgParajsObj As New JObject
        Dim MsgParajsStr As String = ""

        ParajsObj.Add("DutyInBT", txtDutyInBT.Text)
        ParajsObj.Add("DutyOutBT", txtDutyOutBT.Text)
        ParajsObj.Add("PunchInBT", txtPunchInBT.Text)
        ParajsObj.Add("PunchOutBT", txtPunchOutBT.Text)

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


        ParajsAry.Add(ParajsObj)
        MsgParajsAry.Add(MsgParajsObj)

        ParajsStr = JsonConvert.SerializeObject(ParajsAry, Formatting.None)
        MsgParajsStr = JsonConvert.SerializeObject(MsgParajsAry, Formatting.None)

        bePunchPara.CompID.Value = UserProfile.SelectCompRoleID
        If rbnNotSpecialUnit.Checked = True Then
            bePunchPara.SpecialUnitFlag.Value = "0"
        Else
            bePunchPara.SpecialUnitFlag.Value = "1"
        End If
        bePunchPara.Para.Value = ParajsStr
        bePunchPara.MsgPara.Value = MsgParajsStr
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
            If objPO.UpdateWorkSiteSetting(bePunchPara) Then
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
        subGetData(UserProfile.SelectCompRoleName)  '重新取得資料
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
#End Region
End Class
