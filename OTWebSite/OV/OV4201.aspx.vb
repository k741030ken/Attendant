'****************************************************
'功能說明：加班管理-事後申報-新增
'建立日期：2017.02.06
'修改日期：2017.03.20
'****************************************************

Imports System                  'DateTime.Parse
Imports System.Data
Imports System.Data.Common
Imports System.Globalization
Imports System.Diagnostics      'For Debug.Print()
Imports System.IO
Imports SinoPac.WebExpress.Common
Imports SinoPac.WebExpress.DAO

Partial Class OV_OV4201
    Inherits PageBase

#Region "全域變數"

    Private Property Config_AattendantDBName As String
        Get
            Dim result As String = OVBusinessCommon.AattendantDBName
            Return result
        End Get
        Set(ByVal value As String)

        End Set
    End Property

    Private Property _eHRMSDB_ITRD As String
        Get
            Dim result As String = ConfigurationManager.AppSettings("eHRMSDB")
            If String.IsNullOrEmpty(result) Then
                result = "eHRMSDB_ITRD"
            End If
            Return result
        End Get
        Set(ByVal value As String)

        End Set
    End Property

    Public Property _AttachID As String
        Get
            If ViewState.Item("_AttachID") Is Nothing Then ViewState.Item("_AttachID") = String.Empty

            Return ViewState.Item("_AttachID").ToString()
        End Get
        Set(value As String)
            ViewState.Item("_AttachID") = value
        End Set
    End Property

    Public Property _rankID As String
        Get
            If ViewState.Item("_rankID") Is Nothing Then ViewState.Item("_rankID") = String.Empty

            Return ViewState.Item("_rankID").ToString()
        End Get
        Set(value As String)
            ViewState.Item("_rankID") = value
        End Set
    End Property

    Public Property _FormNo As String
        Get
            If ViewState.Item("_FormNo") Is Nothing Then ViewState.Item("_FormNo") = String.Empty

            Return ViewState.Item("_FormNo").ToString()
        End Get
        Set(value As String)
            ViewState.Item("_FormNo") = value
        End Set
    End Property

    Public Property _FormNoRecord As String
        Get
            If ViewState.Item("_FormNoRecord") Is Nothing Then ViewState.Item("_FormNoRecord") = String.Empty

            Return ViewState.Item("_FormNoRecord").ToString()
        End Get
        Set(value As String)
            ViewState.Item("_FormNoRecord") = value
        End Set
    End Property

    Public Property _OTTxnID As String
        Get
            If ViewState.Item("_OTTxnID") Is Nothing Then ViewState.Item("_OTTxnID") = String.Empty

            Return ViewState.Item("_OTTxnID").ToString()
        End Get
        Set(value As String)
            ViewState.Item("_OTTxnID") = value
        End Set
    End Property

    Public Property _OTSeq As String
        Get
            If ViewState.Item("_OTSeq") Is Nothing Then ViewState.Item("_OTSeq") = String.Empty

            Return ViewState.Item("_OTSeq").ToString()
        End Get
        Set(value As String)
            ViewState.Item("_OTSeq") = value
        End Set
    End Property

    Public Property _Sex As String
        Get
            If ViewState.Item("_Sex") Is Nothing Then ViewState.Item("_Sex") = String.Empty

            Return ViewState.Item("_Sex").ToString()
        End Get
        Set(value As String)
            ViewState.Item("_Sex") = value
        End Set
    End Property

    Public Property _EmpDate As String
        Get
            'If ViewState.Item("_EmpDate") Is Nothing Then ViewState.Item("_EmpDate") = String.Empty
            If ViewState.Item("_EmpDate") Is Nothing Then ViewState.Item("_EmpDate") = "1900/01/01"

            Return ViewState.Item("_EmpDate").ToString()
        End Get
        Set(value As String)
            ViewState.Item("_EmpDate") = value
        End Set
    End Property

    Public Property _WorkSiteID As String
        Get
            If ViewState.Item("_WorkSiteID") Is Nothing Then ViewState.Item("_WorkSiteID") = String.Empty

            Return ViewState.Item("_WorkSiteID").ToString()
        End Get
        Set(value As String)
            ViewState.Item("_WorkSiteID") = value
        End Set
    End Property

    Public Property _DeptID As String
        Get
            If ViewState.Item("_DeptID") Is Nothing Then ViewState.Item("_DeptID") = String.Empty

            Return ViewState.Item("_DeptID").ToString()
        End Get
        Set(value As String)
            ViewState.Item("_DeptID") = value
        End Set
    End Property

    Public Property _DeptName As String
        Get
            If ViewState.Item("_DeptName") Is Nothing Then ViewState.Item("_DeptName") = String.Empty

            Return ViewState.Item("_DeptName").ToString()
        End Get
        Set(value As String)
            ViewState.Item("_DeptName") = value
        End Set
    End Property

    Public Property _OrganID As String
        Get
            If ViewState.Item("_OrganID") Is Nothing Then ViewState.Item("_OrganID") = String.Empty

            Return ViewState.Item("_OrganID").ToString()
        End Get
        Set(value As String)
            ViewState.Item("_OrganID") = value
        End Set
    End Property

    Public Property _OrganName As String
        Get
            If ViewState.Item("_OrganName") Is Nothing Then ViewState.Item("_OrganName") = String.Empty

            Return ViewState.Item("_OrganName").ToString()
        End Get
        Set(value As String)
            ViewState.Item("_OrganName") = value
        End Set
    End Property

    Public Property _dataCount As Integer
        Get
            If ViewState.Item("_dataCount") Is Nothing Then ViewState.Item("_dataCount") = String.Empty

            Return ViewState.Item("_dataCount").ToString()
        End Get
        Set(value As Integer)
            ViewState.Item("_dataCount") = value
        End Set
    End Property

    Public Property _dtPara As DataTable
        Get
            If ViewState.Item("_dtPara") Is Nothing Then ViewState.Item("_dtPara") = Nothing

            Return DirectCast(ViewState("_dtPara"), DataTable)
        End Get
        Set(value As DataTable)
            ViewState.Item("_dtPara") = value
        End Set
    End Property

#End Region

#Region "功能鍵設定"
    ''' <summary>
    ''' 功能鍵設定
    ''' </summary>
    ''' <param name="Param"></param>
    ''' <remarks></remarks>
    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnAdd"   '暫存
                ViewState("Param") = Param
                If funCheckData() Then
                    funPopupNotify()
                End If
            Case "btnExecutes"  '送簽
                ViewState("Param") = Param
                If funCheckData() Then
                    If funCheckStatus() Then
                        funPopupNotify()
                    End If
                End If
            Case "btnCancel"    '清除
                ClearData()
            Case "btnDelete"    '刪除
                ViewState("Param") = Param
                If funCheckStatus() Then
                    funPopupNotify()
                End If
            Case "btnActionX"   '返回
                GoBack()
        End Select
    End Sub
#End Region

#Region "畫面邏輯處理"

    ''' <summary>
    ''' 畫面載入事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Bsp.Utility.FillDDL(ddlCodeCName, "AattendantDB", "AT_CodeMap", "Code", "CodeCName", Bsp.Utility.DisplayType.OnlyName, "", "AND TabName = 'OverTime' AND FldName = 'OverTimeType' AND NotShowFlag = '0'", "ORDER BY Code") '加班類型
            ddlCodeCName.Items.Insert(0, New ListItem("　- -請選擇- -", ""))

            '授權人公司代碼
            hidOTRegisterCompID.Value = UserProfile.ActCompID.Trim

            '公司代碼
            lblCompID.Text = UserProfile.SelectCompRoleName
            hidCompID.Value = UserProfile.SelectCompRoleID.ToString().Trim

            '忘記日期~~-SA說畫面暫不顯示單位別與科別
            '單位別
            'lblDeptID.Text = UserProfile.ActDeptID.ToString().Trim
            'lblDeptName.Text = UserProfile.ActDeptName.ToString().Trim

            '科別
            'lblOrganID.Text = UserProfile.ActOrganID.ToString().Trim
            'lblOrganName.Text = UserProfile.ActOrganName.ToString().Trim

            '員工編號
            ucQueryEmp.ShowCompRole = "False"
            ucQueryEmp.InValidFlag = "N"
            ucQueryEmp.SelectCompID = UserProfile.SelectCompRoleID

            '時段顯示
            PnlgvMain.Visible = False
            'pnlCalcTotalTime.Visible = False

            rblOTPerson.SelectedIndex = 0

            callHandlerUrl.Value = Bsp.Utility.getAppSetting("AattendantWebPath")

            '開始初始化畫面
            LoadData()

            '附件Attach
            Dim strAttachID As String = ""
            Dim strAttachAdminURL As String = ""
            Dim strAttachAdminBaseURL As String = ConfigurationManager.AppSettings("AattendantWebPath") + "HandlerForOverTime/SSORecvModeForOverTime.aspx?UserID=" + UserProfile.UserID.Trim + "&SystemID=OT&TxnID=OV4201&ReturnUrl=%2FUtil%2FAttachAdmin.aspx?AttachDB={0}%26AttachID={1}%26AttachFileMaxQty={2}%26AttachFileMaxKB={3}%26AttachFileTotKB={4}%26AttachFileExtList={5}"
            Dim strAttachDownloadURL As String
            Dim strAttachDownloadBaseURL As String = Util._AttachDownloadUrl + "?AttachDB={0}&AttachID={1}"

            '附件編號
            strAttachID = "test" + UserProfile.UserID.Trim + Guid.NewGuid().ToString()
            ViewState("attach") = strAttachID
            _AttachID = strAttachID
            strAttachAdminURL = String.Format(strAttachAdminBaseURL, Config_AattendantDBName, strAttachID, "1", "3072", "3072", "")
            strAttachDownloadURL = String.Format(strAttachDownloadBaseURL, Config_AattendantDBName, strAttachID)
            frameAttach.Value = strAttachAdminURL
            getAttachName()

        End If
    End Sub

    ''' <summary>
    ''' 帶出預設值
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub LoadData()
        Dim objSC As New SC
        Dim objOV2 As New OV2
        Dim objOV42 As New OV4_2
        Dim objOVBC As New OVBusinessCommon

        'hidGridViewStartTime.Value = "true"     '記錄第一筆暫存資料時間

        '加班人公司
        hidCompID.Value = UserProfile.SelectCompRoleID.Trim
        lblCompID.Text = UserProfile.SelectCompRoleName.Trim

        '加班人單位
        'lblDeptID.Text = UserProfile.ActDeptID.Trim
        'lblDeptName.Text = UserProfile.ActDeptName.Trim

        '加班人科別
        'lblOrganID.Text = UserProfile.ActOrganID.Trim
        'lblOrganName.Text = UserProfile.ActOrganName.Trim

        '填單人員編與姓名
        lblOTRegisterID.Text = UserProfile.UserID.Trim
        lblOTRegisterNameN.Text = UserProfile.UserName.Trim

        '加班開始日期
        ucOTStartDate.DateText = FormatDateTime(Date.Now, DateFormat.ShortDate)

        '加班結束日期
        ucOTEndDate.DateText = FormatDateTime(Date.Now, DateFormat.ShortDate)

        '用餐時數 MealFlag=0不扣除,MealFlag=1扣除
        MealFlag.Checked = True
        txtMealTime.Text = "0"

        '計算總計
        'If txtOTEmpID.Text.Trim <> "" And ucOTStartDate.DateText <> "" And ucOTEndDate.DateText <> "" Then
        '    Dim OverTimeSumArr As ArrayList = objOV42.GetOverTimeSum(UserProfile.ActCompID.ToString().Trim, txtOTEmpID.Text.Trim, ucOTStartDate.DateText.ToString())
        '    If OverTimeSumArr.Count <> 0 Then
        '        Dim OverTimeSumStr As String = Month(ucOTStartDate.DateText).ToString() + "月份已申報時數合計: 送簽 " + OverTimeSumArr(0).ToString + "小時&nbsp&nbsp&nbsp核准 " + OverTimeSumArr(1).ToString + "小時&nbsp&nbsp&nbsp駁回 " + OverTimeSumArr(2).ToString + "小時"
        '        lblTotalOTCalc.Text = OverTimeSumStr
        '    End If
        'End If

        If txtOTEmpID.Text.Trim <> "" And ucOTStartDate.DateText <> "" And ucOTEndDate.DateText <> "" Then
            Dim OverTimeSumArr As ArrayList = New ArrayList
            OverTimeSumArr.AddRange(objOVBC.getTotalHR(hidCompID.Value.ToString(), txtOTEmpID.Text.Trim, ucOTStartDate.DateText.ToString()))

            If OverTimeSumArr.Count <> 0 Then
                Dim OverTimeSumStr As String = ""
                OverTimeSumStr = If(OverTimeSumArr(0) IsNot Nothing AndAlso OverTimeSumArr(0).ToString() <> "", OverTimeSumArr(0).ToString(), Month(ucOTStartDate.DateText).ToString()) _
                                               + "月份已申報時數合計: 送簽 " + If(OverTimeSumArr(1) IsNot Nothing AndAlso OverTimeSumArr(1).ToString() <> "", OverTimeSumArr(1).ToString(), "0.0") _
                                               + "小時&nbsp&nbsp&nbsp核准 " + If(OverTimeSumArr(2) IsNot Nothing AndAlso OverTimeSumArr(2).ToString() <> "", OverTimeSumArr(2).ToString(), "0.0") _
                                               + "小時&nbsp&nbsp&nbsp駁回 " + If(OverTimeSumArr(3) IsNot Nothing AndAlso OverTimeSumArr(3).ToString() <> "", OverTimeSumArr(3).ToString(), "0.0") + "小時"
                lblTotalOTCalc.Text = OverTimeSumStr
            End If
        End If

        'DataTable dtPara = at.QueryData("DayLimitHourN, DayLimitHourH, MonthLimitHour, SalaryOrAdjust, AdjustInvalidDate", "OverTimePara", " AND CompID = '" + UserInfo.getUserInfo().CompID + "'")
        _dtPara = objOV42.Json2DataTable(objOV42.QueryColumn("Para", "OverTimePara", " AND CompID = '" + hidCompID.Value.ToString() + "'"))      '帶入參數設定檔

        'ddlSalaryOrAdjust.SelectedIndex = ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByValue(_dtPara.Rows(0)("SalaryOrAjust").ToString()))
        LoadOTEmpData()
        'ddlSalaryOrAdjust_SelectedIndexChanged(Nothing, EventArgs.Empty)
    End Sub

    ''' <summary>
    ''' 當Usr輸入員工編號，查詢員工姓名與查詢員工資料
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>用員工編號和員工的CompID來查詢他的名字</remarks>
    Protected Sub txtOTEmpID_TextChanged(sender As Object, e As System.EventArgs) Handles txtOTEmpID.TextChanged
        Dim objOV42 As New OV4_2
        Dim objOVBC As New OVBusinessCommon

        '查詢員工姓名
        If txtOTEmpID.Text.Trim <> "" Then
            Dim rtnTable As DataTable = objOV42.GetEmpName(hidCompID.Value, txtOTEmpID.Text.Trim)
            If rtnTable.Rows.Count <= 0 Then
                lblOTEmpNameN.Text = "查無此筆資料!"
                lblStartSex.Visible = False
                lblEndSex.Visible = False
                Debug.Print("txtOTEmpID_TextChanged()-->查無此筆資料")
            Else
                lblOTEmpNameN.Text = rtnTable.Rows(0).Item(0)

                '取得此員工的其他相關資料
                LoadOTEmpData()
                '依照RankID階級與加班起迄日來控制 加班轉換方式的下拉選項
                ddlSalaryOrAdjustChange(_rankID, ucOTStartDate.DateText, ucOTEndDate.DateText)
                ddlSalaryOrAdjust_SelectedIndexChanged(Nothing, EventArgs.Empty)

                If ucOTStartTime.ucDefaultSelectedHH <> "" AndAlso ucOTStartTime.ucDefaultSelectedMM <> "" AndAlso ucOTStartTime.ucDefaultSelectedHH <> "請選擇" AndAlso ucOTStartTime.ucDefaultSelectedMM <> "請選擇" Then
                    ValidStartTime_Click(Nothing, EventArgs.Empty)
                    If ucOTEndTime.ucDefaultSelectedHH <> "" And ucOTEndTime.ucDefaultSelectedMM <> "" AndAlso ucOTEndTime.ucDefaultSelectedHH <> "請選擇" AndAlso ucOTEndTime.ucDefaultSelectedMM <> "請選擇" Then
                        ValidEndTime_Click(Nothing, EventArgs.Empty)
                    End If
                End If

                If ucOTStartDate.DateText <> "" AndAlso ucOTEndDate.DateText <> "" AndAlso txtOTEmpID.Text <> "" Then
                    '計算總計
                    'Dim OverTimeSumArr As ArrayList = objOV42.GetOverTimeSum(UserProfile.ActCompID.ToString().Trim, txtOTEmpID.Text.Trim, ucOTStartDate.DateText.ToString())
                    'If OverTimeSumArr.Count > 0 Then
                    '    Dim OverTimeSumStr As String = Month(ucOTStartDate.DateText).ToString() + "月份已申報時數合計: 送簽 " + OverTimeSumArr(0).ToString + "小時&nbsp&nbsp&nbsp核准 " + OverTimeSumArr(1).ToString + "小時&nbsp&nbsp&nbsp駁回 " + OverTimeSumArr(2).ToString + "小時"
                    '    lblTotalOTCalc.Text = OverTimeSumStr
                    'End If

                    Dim OverTimeSumArr As ArrayList = New ArrayList
                    OverTimeSumArr.AddRange(objOVBC.getTotalHR(hidCompID.Value.ToString(), txtOTEmpID.Text.Trim, ucOTStartDate.DateText.ToString()))

                    If OverTimeSumArr.Count <> 0 Then
                        Dim OverTimeSumStr As String = ""
                        OverTimeSumStr = If(OverTimeSumArr(0) IsNot Nothing AndAlso OverTimeSumArr(0).ToString() <> "", OverTimeSumArr(0).ToString(), Month(ucOTStartDate.DateText).ToString()) _
                                                       + "月份已申報時數合計: 送簽 " + If(OverTimeSumArr(1) IsNot Nothing AndAlso OverTimeSumArr(1).ToString() <> "", OverTimeSumArr(1).ToString(), "0.0") _
                                                       + "小時&nbsp&nbsp&nbsp核准 " + If(OverTimeSumArr(2) IsNot Nothing AndAlso OverTimeSumArr(2).ToString() <> "", OverTimeSumArr(2).ToString(), "0.0") _
                                                       + "小時&nbsp&nbsp&nbsp駁回 " + If(OverTimeSumArr(3) IsNot Nothing AndAlso OverTimeSumArr(3).ToString() <> "", OverTimeSumArr(3).ToString(), "0.0") + "小時"
                        lblTotalOTCalc.Text = OverTimeSumStr
                    End If
                End If
            End If
        Else
            lblOTEmpNameN.Text = ""
            lblStartSex.Visible = False
            lblEndSex.Visible = False
        End If
    End Sub

    ''' <summary>
    ''' 彈跳視窗-快速人員查詢(QFind)
    ''' </summary>
    ''' <param name="returnValue"></param>
    ''' <remarks></remarks>
    Public Overrides Sub DoModalReturn(ByVal returnValue As String)
        Dim strSql As String = ""
        Dim objOV42 As New OV4_2

        If returnValue <> "" Then
            Dim aryData() As String = returnValue.Split(":")

            Select Case aryData(0)
                Case "ucQueryEmp"
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    '員工編號
                    txtOTEmpID.Text = aryValue(1)
                    '員工姓名
                    lblOTEmpNameN.Text = aryValue(2)

                    '依照加班人的公司代碼來更新各項加班參數設定
                    _dtPara = objOV42.Json2DataTable(objOV42.QueryColumn("Para", "OverTimePara", " AND CompID = '" + aryValue(3).Trim + "'"))

                    '取得員工資料
                    LoadOTEmpData()
            End Select
        End If
    End Sub

    ''' <summary>
    ''' 讀取輸入之加班人的資訊
    ''' </summary>
    ''' <remarks>清除資料或回到原始資料須重新找登入者的基本資料</remarks>
    Protected Sub LoadOTEmpData()
        Dim objOV42 As New OV4_2

        Using dt As DataTable = objOV42.GetEmpData(hidCompID.Value.ToString().Trim, txtOTEmpID.Text.Trim)
            If dt.Rows.Count > 0 Then
                _Sex = dt.Rows(0).Item("Sex").ToString()

                'RankID要進行轉換~
                Dim strRankID As String = dt.Rows(0).Item("RankID").ToString()
                '_rankID = dt.Rows(0).Item("RankID").ToString()
                _rankID = OVBusinessCommon.GetRankID(hidCompID.Value.ToString().Trim, strRankID)

                _EmpDate = dt.Rows(0).Item("EmpDate").ToString()
                _WorkSiteID = dt.Rows(0).Item("WorkSiteID").ToString()
                _DeptID = dt.Rows(0).Item("DeptID").ToString()
                _DeptName = dt.Rows(0).Item("DeptName").ToString()
                _OrganID = dt.Rows(0).Item("OrganID").ToString()
                _OrganName = dt.Rows(0).Item("OrganName").ToString()

                lblDeptID.Text = dt.Rows(0).Item("DeptID").ToString().Trim
                lblDeptName.Text = dt.Rows(0).Item("DeptName").ToString().Trim

                lblOrganID.Text = dt.Rows(0).Item("OrganID").ToString().Trim
                lblOrganName.Text = dt.Rows(0).Item("OrganName").ToString().Trim()

                '---------
                Debug.Print("加班人員工編號:" + txtOTEmpID.Text.Trim)
                Debug.Print("加班人單位別:" + lblDeptID.Text.Trim)
                Debug.Print("加班人科別:" + lblOrganID.Text.Trim)
                Debug.Print("加班人性別:" + dt.Rows(0).Item("Sex").ToString())
                Debug.Print("加班人RankID:" + dt.Rows(0).Item("RankID").ToString())
                Debug.Print("加班人到職日:" + dt.Rows(0).Item("EmpDate").ToString())
                Debug.Print("加班人WorkSiteID:" + dt.Rows(0).Item("WorkSiteID").ToString())

                If (Not String.IsNullOrEmpty(ucOTStartDate.DateText)) And (Not String.IsNullOrEmpty(ucOTEndDate.DateText)) And (Not String.IsNullOrEmpty(_rankID)) Then

                    '依照RankID階級與加班起迄日來控制 加班轉換方式的下拉選項
                    ddlSalaryOrAdjustChange(_rankID, ucOTStartDate.DateText, ucOTEndDate.DateText)
                    ddlSalaryOrAdjust_SelectedIndexChanged(Nothing, EventArgs.Empty)
                End If

                '依照加班人的公司代碼來更新各項加班參數設定
                _dtPara = objOV42.Json2DataTable(objOV42.QueryColumn("Para", "OverTimePara", " AND CompID = '" + UserProfile.SelectCompRoleID.Trim + "'"))
            End If
        End Using
    End Sub

    ''' <summary>
    ''' 加班轉換方式改變事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ddlSalaryOrAdjust_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlSalaryOrAdjust.SelectedIndexChanged

        Select Case ddlSalaryOrAdjust.SelectedValue
            Case "2"
                lbl_lblAdjustInvalidDate.Visible = True
                lblAdjustInvalidDate.Visible = True
                lblAdjustInvalidDate.Text = FormatDateTime(_dtPara.Rows(0).Item("AdjustInvalidDate").ToString(), DateFormat.ShortDate).Trim()
            Case Else
                lbl_lblAdjustInvalidDate.Visible = False
                lblAdjustInvalidDate.Visible = False
        End Select
    End Sub

    ''' <summary>
    ''' 依照RankID階級與加班起迄日來控制 加班轉換方式的下拉選項
    ''' </summary>
    ''' <param name="sRankID">加班人的職等</param>
    ''' <param name="startDate">加班起日</param>
    ''' <param name="endDate">加班迄日</param>
    ''' <remarks>
    ''' RankID大於等於AdjustRankID : 只能轉補休
    ''' RankID小於AdjustRankID且兩天皆為假日 : 可轉補休或轉薪資
    ''' RankID小於AdjustRankID且除了兩天皆為假日以外 : 只能轉薪資
    ''' </remarks>
    Private Sub ddlSalaryOrAdjustChange(ByVal sRankID As String, ByVal startDate As String, ByVal endDate As String)
        Dim objOV42 As New OV4_2
        Dim AdjustRankID As String = ""     '比較的AdjustRankID
        Dim IntAdjustRankID As Integer = 0      '比較的AdjustRankID

        Try
            AdjustRankID = OVBusinessCommon.GetRankID(UserProfile.SelectCompRoleID.Trim, _dtPara.Rows(0)("AdjustRankID").ToString())
        Catch ex As Exception
            Debug.Print("Find Mapping RankID Failed==>" + ex.Message)
            Return
        End Try

        If IsNumeric(AdjustRankID) Then IntAdjustRankID = Convert.ToInt32(AdjustRankID)

        If Not String.IsNullOrEmpty(sRankID) AndAlso Not String.IsNullOrEmpty(startDate) AndAlso Not String.IsNullOrEmpty(endDate) Then
            '先回到預設值
            ddlSalaryOrAdjust.SelectedIndex = 0

            If IsNumeric(AdjustRankID) Then '判斷參數設定的轉補休職等設定有設定(也就是參數必需是數字!)，若為空則代表開放所有選擇
                If IsNumeric(sRankID) = True Then
                    Dim dRankID = Convert.ToInt32(sRankID)
                    If dRankID >= AdjustRankID Then '如果加班人職等大於等於參數設定的職等，僅能選擇轉補休
                        '如果RankID大於等於AdjustRankID只能轉補休
                        Debug.Print(ddlSalaryOrAdjust.Items.Count)
                        Debug.Print(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉補休")).ToString())

                        ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉薪資"))).Enabled = False
                        ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉補休"))).Enabled = True
                    ElseIf dRankID < AdjustRankID Then  '如果加班人職小於於參數設定的職等且加班起訖日都是假日，可選擇轉補休或轉薪資，反之則看參數設定
                        Dim dateStartIsHoliday As Boolean = objOV42.CheckHolidayOrNot(startDate)
                        Dim dateEndIsHoliday As Boolean = objOV42.CheckHolidayOrNot(endDate)
                        If dateStartIsHoliday AndAlso dateEndIsHoliday Then
                            'RankID小於AdjustRankID且兩天皆為假日 : 可轉補休或轉薪資
                            Try
                                ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉薪資"))).Enabled = True
                                ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉補休"))).Enabled = True
                            Catch ex As Exception
                                Debug.Print("ddlSalaryOrAdjust指向問題" + ex.Message)
                            End Try
                        Else
                            'RankID小於AdjustRankID且除了兩天皆為假日以外 :看參數設定之預設值
                            Select Case _dtPara.Rows(0)("SalaryOrAjust").ToString()
                                Case "1"    '參數設定預設為轉薪資
                                    ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉薪資"))).Enabled = True
                                    ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉補休"))).Enabled = False
                                Case "2"    '參數設定預設為轉補休
                                    ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉薪資"))).Enabled = False
                                    ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉補休"))).Enabled = True
                                Case Else   '參數設定錯誤，來亂的喔!
                                    Debug.Print("參數設定錯誤,SalaryOrAjust=" & _dtPara.Rows(0)("SalaryOrAjust").ToString())
                                    ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉薪資"))).Enabled = True
                                    ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉補休"))).Enabled = True
                            End Select

                            ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉薪資"))).Enabled = True
                            ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉補休"))).Enabled = False
                        End If
                    End If
                End If
            Else '如果回傳的RanKID為空，開放所有選擇
                ddlSalaryOrAdjust.SelectedIndex = 0
                ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉薪資"))).Enabled = True
                ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉補休"))).Enabled = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' 當使用者選擇單筆或多筆加班人
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rblOTPerson_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rblOTPerson.SelectedIndexChanged
        Dim objOV42 As New OV4_2
        Select Case rblOTPerson.SelectedIndex
            Case 0      '單筆加班人
                _FormNoRecord = ""

                If gvMain.Rows.Count <= 0 Then
                    PnlgvMain.Visible = False
                    gvMain.Visible = False
                Else
                    RefreshGrid()
                End If


                '從單筆到多筆資料須清除
                txtOTEmpID.Text = ""
                lblOTEmpNameN.Text = ""
                ucOTStartDate.DateText = Date.Now.ToString("yyyy/MM/dd")
                ucOTEndDate.DateText = Date.Now.ToString("yyyy/MM/dd")
                MealFlag.Checked = True
                txtMealTime.Enabled = True
                txtOTReasonMemo.Text = ""
                ddlCodeCName.SelectedIndex = -1
                chkCopyAtt.Visible = False

                '附件也需要清掉
                objOV42.DeleteAttach(_AttachID)

                LoadOTEmpData()
                getAttachName()
                initalData()

            Case 1      '多筆加班人
                _FormNoRecord = ""

                If gvMain.Rows.Count <= 0 Then
                    PnlgvMain.Visible = False
                    gvMain.Visible = False
                Else
                    RefreshGrid()
                End If

                If gvMain.Rows.Count > 0 And gvMain.Visible = True Then
                    chkCopyAtt.Enabled = True
                    chkCopyAtt.Visible = True
                Else
                    chkCopyAtt.Enabled = False
                    chkCopyAtt.Visible = False
                    PnlgvMain.Visible = False
                    gvMain.Visible = False
                    PnlgvMain.Visible = False
                End If

                '從多筆到單筆資料須清除
                txtOTEmpID.Text = ""
                lblOTEmpNameN.Text = ""
                ucOTStartDate.DateText = Date.Now.ToString("yyyy/MM/dd")
                ucOTEndDate.DateText = Date.Now.ToString("yyyy/MM/dd")
                MealFlag.Checked = True
                txtMealTime.Enabled = True
                txtOTReasonMemo.Text = ""
                ddlCodeCName.SelectedIndex = -1
                chkCopyAtt.Visible = False

                '附件也需要清掉
                objOV42.DeleteAttach(_AttachID)
                LoadOTEmpData()

                initalData()
                getAttachName()

            Case Else
                Debug.Print("rblOTPerson_SelectedIndexChanged()==>Err")
        End Select
    End Sub

#End Region

#Region "功能鍵邏輯處理"

    ''' <summary>
    ''' 返回按鈕事件
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GoBack()
        Dim ti As TransferInfo = Me.StateTransfer
        Me.TransferFramePage(ti.CallerUrl, Nothing, ti.Args)
    End Sub

    ''' <summary>
    ''' 將加班起迄時間、用餐時數、加班時數合計、加班時段表、女性不可超過十點加班警語隱藏和回復預設值
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub initalData()
        ucOTStartTime.ucDefaultSelectedHH = "請選擇"
        ucOTStartTime.ucDefaultSelectedMM = "請選擇"
        ucOTEndTime.ucDefaultSelectedHH = "請選擇"
        ucOTEndTime.ucDefaultSelectedMM = "請選擇"
        txtMealTime.Text = "0"
        lblOTTotalTime.Text = ""
        txtOTTotalDescript.Visible = False
        pnlCalcTotalTime.Visible = False

        lblStartSex.Visible = False
        lblEndSex.Visible = False
    End Sub

    ''' <summary>
    ''' 執行清除資料
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearData()
        Dim objOV2 As New OV4_2

        Try
            '多筆或單筆加班人選擇
            rblOTPerson.SelectedIndex = 0

            '加班人員編與姓名
            txtOTEmpID.Text = ""
            lblOTEmpNameN.Text = ""

            '清掉單位別與科別
            '單位別
            lblDeptID.Text = ""
            lblDeptName.Text = ""

            '科別
            lblOrganID.Text = ""
            lblOrganName.Text = ""


            '加班轉換方式
            ddlSalaryOrAdjust.SelectedIndex = 0

            '補休失效日
            lbl_lblAdjustInvalidDate.Visible = False
            lblAdjustInvalidDate.Text = ""
            lblAdjustInvalidDate.Visible = False

            '加班開始與結束日期
            ucOTStartDate.DateText = FormatDateTime(Date.Now, DateFormat.ShortDate)
            ucOTEndDate.DateText = FormatDateTime(Date.Now, DateFormat.ShortDate)

            '加班開始與結束時間
            ucOTStartTime.ucDefaultSelectedHH = "請選擇"
            ucOTStartTime.ucDefaultSelectedMM = "請選擇"

            ucOTEndTime.ucDefaultSelectedHH = "請選擇"
            ucOTEndTime.ucDefaultSelectedMM = "請選擇"

            '用餐時數 MealFlag=0不扣除,MealFlag=1扣除
            MealFlag.Checked = True
            txtMealTime.Text = "0"
            txtMealTime.Enabled = True

            '加班類型
            ddlCodeCName.SelectedIndex = 0

            '加班原因
            txtOTReasonMemo.Text = ""

            '上傳附件
            chkCopyAtt.Visible = False
            chkCopyAtt.Checked = False
            '附件也需要清掉
            If Not objOV2.DeleteAttach(_AttachID) Then Debug.Print("清除失敗!")
            getAttachName()

            '時段計算區塊
            lblOTTotalTime.Text = "0.0"
            litTable.Text = ""
            txtOTTotalDescript.Visible = False
            pnlCalcTotalTime.Visible = False

            '申報時數總計
            Dim OverTimeSumStr As String = Month(Date.Now).ToString() + "月份已申報時數合計: 送簽 0.0小時&nbsp&nbsp&nbsp核准 0.0小時&nbsp&nbsp&nbsp駁回 0.0小時"
            lblTotalOTCalc.Text = OverTimeSumStr

            LoadOTEmpData()

        Catch ex As Exception
            Debug.Print("ClearData()==>" + ex.Message)
            Bsp.Utility.ShowMessage(Me, "清除失敗")
        End Try
    End Sub

#End Region

#Region "隱藏Button-暫存與送簽提示訊息"
    ''' <summary>
    ''' 檢核後的提示訊息
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub funPopupNotify()
        Select Case ViewState("Param")
            Case "btnAdd"    '是暫存
                Bsp.Utility.RunClientScript(Me.Page, "TempSaveAsk()")
                'Return False
            Case "btnExecutes"  '是送簽
                Bsp.Utility.RunClientScript(Me.Page, "SubmitAsk()")
                'Return False
            Case "btnDelete"  '是刪除
                Bsp.Utility.RunClientScript(Me.Page, "DeleteAsk()")
                'Return False
            Case Else           '是來亂的
                Debug.Print("funPopupNotify()==>有人來亂,Param = " + ViewState("Param").ToString())
                'Return False
        End Select
    End Sub

    ''' <summary>
    ''' 轉送暫存/送簽動作
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnTransportAsk_Click(sender As Object, e As System.EventArgs) Handles btnTransportAsk.Click
        If ViewState("Param") <> "" Then
            Select Case ViewState("Param")
                Case "btnAdd"    '是暫存
                    Bsp.Utility.RunClientScript(Me.Page, "TempSave()")
                    'Return False
                Case "btnExecutes"  '是送簽
                    Bsp.Utility.RunClientScript(Me.Page, "Submit()")
                    'Return False
                Case "btnDelete"  '是刪除
                    Bsp.Utility.RunClientScript(Me.Page, "Delete()")
                Case Else           '是來亂的
                    Debug.Print("btnTransportAsk_Click()==>有人來亂,Param = " + ViewState("Param").ToString())
                    'Return False
            End Select
        End If
    End Sub
#End Region

#Region "暫存、送簽、刪除、取消"
    ''' <summary>
    ''' 執行暫存
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnTempSave_Click(sender As Object, e As System.EventArgs) Handles btnTempSave.Click
        If SaveTempData() Then
            Bsp.Utility.ShowMessage(Me, "暫存成功")
            Debug.Print("暫存成功")
            getAttachName()
            '_FormNoRecord += (_FormNo + ";")

            '選擇單筆加班人，全部欄位回到預設值及清空
            If rblOTPerson.SelectedValue = "0" Then
                Debug.Print("設定回預設值以及清空" + rblOTPerson.SelectedValue)
                txtOTEmpID.Text = "" 'UserProfile.UserID.Trim
                lblOTEmpNameN.Text = ""
                lblDeptID.Text = "" 'UserProfile.ActDeptID.Trim
                lblDeptName.Text = "" 'UserProfile.ActDeptName.Trim
                lblOrganID.Text = "" 'UserProfile.ActOrganID.Trim
                lblOrganName.Text = "" 'UserProfile.ActOrganName.Trim
                _DeptID = "" 'UserProfile.ActDeptID.Trim
                _DeptName = "" 'UserProfile.ActDeptName.Trim
                _OrganID = "" 'UserProfile.ActOrganID.Trim
                _OrganName = "" 'UserProfile.ActOrganName.Trim
                ucOTStartDate.DateText = Date.Now.ToString("yyyy/MM/dd")
                ucOTEndDate.DateText = Date.Now.ToString("yyyy/MM/dd")
                ucOTStartTime.ucDefaultSelectedHH = "請選擇"
                ucOTStartTime.ucDefaultSelectedMM = "請選擇"
                ucOTEndTime.ucDefaultSelectedHH = "請選擇"
                ucOTEndTime.ucDefaultSelectedMM = "請選擇"
                lblStartSex.Visible = False
                lblEndSex.Visible = False
                ddlCodeCName.SelectedIndex = -1
                ddlSalaryOrAdjust.SelectedIndex = -1 '_dtPara.Rows(0).Item("SalaryOrAjust").ToString()
                lbl_lblAdjustInvalidDate.Visible = False
                lblAdjustInvalidDate.Visible = False
                'LoadOTEmpData()
                MealFlag.Checked = True
                txtMealTime.Text = "0"
                txtMealTime.Enabled = True
                lblOTTotalTime.Text = "0.0"
                txtOTReasonMemo.Text = ""
                txtOTTotalDescript.Visible = False
                pnlCalcTotalTime.Visible = False
                lblTotalOTCalc.Text = Month(ucOTStartDate.DateText).ToString() + "月份已申報時數合計: 送簽 0.0小時&nbsp&nbsp&nbsp核准 0.0小時&nbsp&nbsp&nbsp駁回 0.0小時"
                _FormNoRecord += _FormNo + ";"
                _FormNo = ""    '表單編號須重找
                chkCopyAtt.Visible = False

            ElseIf rblOTPerson.SelectedValue = "1" Then     '多筆加班人
                chkCopyAtt.Visible = True
                chkCopyAtt.Enabled = True
                chkCopyAtt.Checked = False
                chkCopyAtt_CheckedChanged(Nothing, EventArgs.Empty)
                _FormNoRecord = _FormNo + ";"
                ViewState("SalaryOrAdjust") = ddlSalaryOrAdjust.SelectedValue       '多筆加班人記住上一筆選的
            Else
                Debug.Print("執行暫存錯誤~~")
            End If

            RefreshGrid()
        Else
            Bsp.Utility.ShowMessage(Me, "暫存失敗")
            Debug.Print("暫存失敗")
        End If
    End Sub

    ''' <summary>
    ''' 執行送簽
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click
        'hidAttachID.Value = _AttachID
        Dim objOV42 As New OV4_2
        Dim chkCnt As Integer = 0

        LoadCheckData()

        If gvMain.Rows.Count > 0 Then
            Dim jsonlist As New List(Of Dictionary(Of String, String))()

            For intRow As Integer = 0 To gvMain.Rows.Count - 1
                Dim objChk As CheckBox = gvMain.Rows(intRow).FindControl("chk_gvMain")
                If objChk.Checked Then
                    Dim Dictionary As New Dictionary(Of String, String)
                    Dictionary.Add("OTCompID", gvMain.DataKeys(intRow)("OTCompID").ToString())
                    Dictionary.Add("OTEmpID", gvMain.DataKeys(intRow)("OTEmpID").ToString())
                    Dictionary.Add("OTStartDate", gvMain.DataKeys(intRow)("OTDate").ToString().Split("~")(0))
                    Dictionary.Add("OTEndDate", gvMain.DataKeys(intRow)("OTDate").ToString().Split("~")(1))
                    Dictionary.Add("OTStartTime", gvMain.DataKeys(intRow)("OTTime").ToString().Split("~")(0).Replace(":", ""))
                    Dictionary.Add("OTEndTime", gvMain.DataKeys(intRow)("OTTime").ToString().Split("~")(1).Replace(":", ""))
                    Dictionary.Add("OTSeq", gvMain.DataKeys(intRow)("OTSeq").ToString())
                    Dictionary.Add("OTRegisterID", gvMain.DataKeys(intRow)("OTRegisterID").ToString())
                    Dictionary.Add("OTRegisterComp", gvMain.DataKeys(intRow)("OTRegisterComp").ToString())
                    Dictionary.Add("OTTxnID", gvMain.DataKeys(intRow)("OTTxnID").ToString())
                    jsonlist.Add(Dictionary)
                    chkCnt = chkCnt + 1
                End If
            Next

            Dim DataList As New Dictionary(Of String, List(Of Dictionary(Of String, String)))
            DataList.Add("DataList", jsonlist)

            hidGuidID.Value = Guid.NewGuid().ToString()
            Dim sb As New StringBuilder
            sb.Append("INSERT INTO CacheData (Platform,SystemID,TxnName,UserID,CacheID,CacheData,CacheDT,Aging) ")
            sb.Append(" VALUES('AP', 'OT', 'OV4201', '" + UserProfile.ActUserID + "', '" + hidGuidID.Value + "', '" + Newtonsoft.Json.JsonConvert.SerializeObject(DataList) + "', GETDATE(), '30')")
            Bsp.DB.ExecuteNonQuery(CommandType.Text, sb.ToString, "AattendantDB")
        End If

        If chkCnt = 0 Then
            If _FormNo = "" Then
                _FormNo = objOV42.QueryFormNO("AdvanceFormSeq", UserProfile.ActCompID.Trim, UserProfile.UserID.Trim)
            ElseIf rblOTPerson.SelectedValue = "0" Then
                _FormNo = objOV42.QueryFormNO("AdvanceFormSeq", UserProfile.ActCompID.Trim, UserProfile.UserID.Trim)
            End If


            Dim attach = objOV42.QueryAttach(_AttachID, hidCompID.Value.ToString().Trim, txtOTEmpID.Text.Trim)
            ViewState("attach") = attach
            hidAttachID.Value = attach
            If ViewState("attach").ToString().IndexOf("test") >= 0 Then
                chkCopyAtt.Visible = False
            Else
                chkCopyAtt.Visible = True
            End If

            Bsp.Utility.RunClientScript(Me.Page, "ExecSubmit('1')")
        Else
            Bsp.Utility.RunClientScript(Me.Page, "ExecSubmit('2')")
        End If
    End Sub

    ''' <summary>
    ''' 送簽後的清除按鈕事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnClearData_Click(sender As Object, e As System.EventArgs) Handles btnClearData.Click
        getAttachName()

        '選擇單筆加班人，全部欄位回到預設值及清空
        If rblOTPerson.SelectedValue = "0" Then
            Debug.Print("設定回預設值以及清空" + rblOTPerson.SelectedValue)
            txtOTEmpID.Text = "" 'UserProfile.UserID.Trim
            lblOTEmpNameN.Text = ""
            lblDeptID.Text = "" 'UserProfile.ActDeptID.Trim
            lblDeptName.Text = "" 'UserProfile.ActDeptName.Trim
            lblOrganID.Text = "" 'UserProfile.ActOrganID.Trim
            lblOrganName.Text = "" 'UserProfile.ActOrganName.Trim
            _DeptID = "" 'UserProfile.ActDeptID.Trim
            _DeptName = "" 'UserProfile.ActDeptName.Trim
            _OrganID = "" 'UserProfile.ActOrganID.Trim
            _OrganName = "" 'UserProfile.ActOrganName.Trim
            ucOTStartDate.DateText = Date.Now.ToString("yyyy/MM/dd")
            ucOTEndDate.DateText = Date.Now.ToString("yyyy/MM/dd")
            ucOTStartTime.ucDefaultSelectedHH = "請選擇"
            ucOTStartTime.ucDefaultSelectedMM = "請選擇"
            ucOTEndTime.ucDefaultSelectedHH = "請選擇"
            ucOTEndTime.ucDefaultSelectedMM = "請選擇"
            lblStartSex.Visible = False
            lblEndSex.Visible = False
            ddlCodeCName.SelectedIndex = -1
            ddlSalaryOrAdjust.SelectedIndex = -1 '_dtPara.Rows(0).Item("SalaryOrAjust").ToString()
            lbl_lblAdjustInvalidDate.Visible = False
            lblAdjustInvalidDate.Visible = False
            'LoadOTEmpData()
            MealFlag.Checked = True
            txtMealTime.Text = "0"
            txtMealTime.Enabled = True
            lblOTTotalTime.Text = "0.0"
            txtOTReasonMemo.Text = ""
            txtOTTotalDescript.Visible = False
            pnlCalcTotalTime.Visible = False
            lblTotalOTCalc.Text = Month(ucOTStartDate.DateText).ToString() + "月份已申報時數合計: 送簽 0.0小時&nbsp&nbsp&nbsp核准 0.0小時&nbsp&nbsp&nbsp駁回 0.0小時"
            _FormNoRecord += _FormNo + ";"
            _FormNo = ""    '表單編號須重找
            chkCopyAtt.Visible = False

        ElseIf rblOTPerson.SelectedValue = "1" Then     '多筆加班人
            chkCopyAtt.Visible = True
            chkCopyAtt.Enabled = True
            chkCopyAtt.Checked = False
            chkCopyAtt_CheckedChanged(Nothing, EventArgs.Empty)
            _FormNoRecord = _FormNo + ";"
        Else
            Debug.Print("單筆多筆加班人RadioBtnList錯誤!")
        End If

        RefreshGrid()
        If gvMain.Rows.Count <= 0 Then
            gvMain.Visible = False
            PnlgvMain.Visible = False
        End If
    End Sub

    ''' <summary>
    ''' 執行刪除
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnDelete_Click(sender As Object, e As System.EventArgs) Handles btnDelete.Click
        DoDelete()
        RefreshGrid()
    End Sub

    ''' <summary>
    ''' 執行取消
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnReject_Click(sender As Object, e As System.EventArgs) Handles btnReject.Click
        DoReject()
        RefreshGrid()
    End Sub

#Region "執行暫存加班單動作"
    ''' <summary>
    ''' 執行暫存的子Funct
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SaveTempData() As Boolean
        Dim objOV42 As New OV4_2
        Dim beManageOTDec As New beManageOTDec.Row()
        Dim bsManageOTDec As New beManageOTDec.Service()

        Dim OTSeq As Integer = 0
        Dim OTSeqs As Integer = 0
        Dim cntStart As Double = 0
        Dim cntEnd As Double = 0
        Dim cntTotal As Double = 0

        beManageOTDec.OTCompID.Value = UserProfile.SelectCompRoleID.Trim
        beManageOTDec.DeptID.Value = _DeptID.Trim
        beManageOTDec.OrganID.Value = _OrganID.Trim
        beManageOTDec.DeptName.Value = _DeptName.Trim
        beManageOTDec.OrganName.Value = _OrganName.Trim
        beManageOTDec.OTRegisterID.Value = UserProfile.UserID.Trim
        beManageOTDec.OTRegisterDate.Value = Date.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
        beManageOTDec.LastChgComp.Value = UserProfile.ActCompID.Trim
        beManageOTDec.LastChgID.Value = UserProfile.UserID.Trim
        beManageOTDec.OTRegisterComp.Value = UserProfile.ActCompID.Trim

        If _FormNo = "" Then
            _FormNo = objOV42.QueryFormNO("AdvanceFormSeq", UserProfile.ActCompID.Trim, UserProfile.UserID.Trim)
        ElseIf rblOTPerson.SelectedValue = "0" Then
            _FormNo = objOV42.QueryFormNO("AdvanceFormSeq", UserProfile.ActCompID.Trim, UserProfile.UserID.Trim)
        End If

        Dim attach = objOV42.QueryAttach(_AttachID, hidCompID.Value.ToString().Trim, txtOTEmpID.Text.Trim)
        ViewState("attach") = attach
        hidAttachID.Value = attach
        If ViewState("attach").ToString().IndexOf("test") >= 0 Then
            chkCopyAtt.Visible = False
        Else
            chkCopyAtt.Visible = True
        End If

        Dim strChkMealFlag = If(MealFlag.Checked = True, "1", "0")
        Dim strAdjustInvalidDate = If(lblAdjustInvalidDate.Visible = True, lblAdjustInvalidDate.Text.Trim, String.Empty)
        OTSeq = objOV42.QuerySeq("OverTimeDeclaration", hidCompID.Value.ToString().Trim, txtOTEmpID.Text.Trim, ucOTStartDate.DateText)
        If ucOTStartDate.DateText = ucOTEndDate.DateText Then   '不跨日送簽
            Dim strHo As String = If(objOV42.CheckHolidayOrNot(ucOTStartDate.DateText), "1", "0")

            getCntTotal(cntTotal)

            '組剩下資料~
            beManageOTDec.OTEmpID.Value = txtOTEmpID.Text.Trim
            beManageOTDec.OTStartDate.Value = ucOTStartDate.DateText
            beManageOTDec.OTEndDate.Value = ucOTEndDate.DateText
            beManageOTDec.OTSeq.Value = OTSeq.ToString("00")
            beManageOTDec.OTTxnID.Value = (UserProfile.SelectCompRoleID.Trim + txtOTEmpID.Text.Trim + Convert.ToDateTime(ucOTStartDate.DateText).ToString("yyyyMMdd") + OTSeq.ToString("00"))
            beManageOTDec.OTSeqNo.Value = "1"
            beManageOTDec.OTStartTime.Value = ucOTStartTime.ucDefaultSelectedHH + ucOTStartTime.ucDefaultSelectedMM
            beManageOTDec.OTEndTime.Value = ucOTEndTime.ucDefaultSelectedHH + ucOTEndTime.ucDefaultSelectedMM
            beManageOTDec.OTTotalTime.Value = Convert.ToDouble(cntTotal).ToString(0.0)
            beManageOTDec.SalaryOrAdjust.Value = ddlSalaryOrAdjust.SelectedValue
            If ddlSalaryOrAdjust.SelectedValue = "2" Then beManageOTDec.AdjustInvalidDate.Value = lblAdjustInvalidDate.Text.Trim
            beManageOTDec.MealFlag.Value = strChkMealFlag
            beManageOTDec.MealTime.Value = If(MealFlag.Checked, Convert.ToInt32(txtMealTime.Text.Trim), 0)
            beManageOTDec.OTTypeID.Value = ddlCodeCName.SelectedValue
            beManageOTDec.OTReasonMemo.Value = txtOTReasonMemo.Text.Trim
            beManageOTDec.OTAttachment.Value = attach.Trim
            beManageOTDec.OTFormNO.Value = _FormNo.Trim
            beManageOTDec.HolidayOrNot.Value = strHo
            beManageOTDec.LastChgDate.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")

            '開始寫入資料庫
            Try
                Using cn As DbConnection = Bsp.DB.getConnection("AattendantDB")
                    cn.Open()
                    Dim tran As DbTransaction = cn.BeginTransaction
                    Try
                        If bsManageOTDec.Insert(beManageOTDec, tran) = 0 Then
                            tran.Rollback()
                            Return False
                        Else
                            tran.Commit()
                        End If
                    Catch li As Exception
                        tran.Rollback()
                        Debug.Print("Insert-->" + li.Message)
                        Return False
                    Finally
                        If tran IsNot Nothing Then tran.Dispose()
                    End Try
                End Using
            Catch ex As Exception
                Debug.Print("DbConnection Err-->" + ex.Message)
                Return False
            End Try

        Else    '跨日暫存
            getCntStartAndCntEnd(cntStart, cntEnd)

            ' <summary>
            ' 判斷迄日是否需要扣除不足的用餐時間
            ' </summary>
            Dim mealOver As String = objOV42.MealJudge(cntStart, Convert.ToDouble(txtMealTime.Text.Trim))

            Dim strHo1 As String = If(objOV42.CheckHolidayOrNot(ucOTStartDate.DateText), "1", "0")
            Dim strHo2 As String = If(objOV42.CheckHolidayOrNot(ucOTEndDate.DateText), "1", "0")
            Dim crossDayArray As String = ucOTStartDate.DateText + "," + ucOTEndDate.DateText
            _OTTxnID = UserProfile.SelectCompRoleID.Trim + txtOTEmpID.Text.Trim + Convert.ToDateTime(ucOTStartDate.DateText).ToString("yyyyMMdd") + OTSeq.ToString("00")

            For i = 0 To (crossDayArray.Split(",").Length - 1)    'crossDayArray->方便拆單用

                beManageOTDec.OTEmpID.Value = txtOTEmpID.Text.Trim
                beManageOTDec.MealFlag.Value = mealOver.Split(","c)(0)

                If crossDayArray.Split(",")(i) = ucOTStartDate.DateText Then
                    beManageOTDec.OTStartDate.Value = crossDayArray.Split(",")(0)
                    beManageOTDec.OTEndDate.Value = crossDayArray.Split(",")(0)
                    beManageOTDec.OTSeq.Value = OTSeq.ToString("00")
                    beManageOTDec.OTSeqNo.Value = "1"
                    beManageOTDec.OTStartTime.Value = ucOTStartTime.ucDefaultSelectedHH + ucOTStartTime.ucDefaultSelectedMM
                    beManageOTDec.OTEndTime.Value = "2359"
                    beManageOTDec.OTTotalTime.Value = cntStart.ToString()
                    beManageOTDec.MealTime.Value = mealOver.Split(","c)(1)
                    beManageOTDec.HolidayOrNot.Value = strHo1
                Else
                    OTSeqs = objOV42.QuerySeq("OverTimeDeclaration", hidCompID.Value.ToString().Trim, txtOTEmpID.Text.Trim, crossDayArray.Split(",")(1))

                    beManageOTDec.OTStartDate.Value = crossDayArray.Split(",")(1)
                    beManageOTDec.OTEndDate.Value = crossDayArray.Split(",")(1)
                    beManageOTDec.OTSeq.Value = OTSeqs.ToString("00")
                    beManageOTDec.OTSeqNo.Value = "2"
                    beManageOTDec.OTStartTime.Value = "0000"
                    beManageOTDec.OTEndTime.Value = ucOTEndTime.ucDefaultSelectedHH + ucOTEndTime.ucDefaultSelectedMM
                    beManageOTDec.OTTotalTime.Value = cntEnd.ToString()
                    beManageOTDec.MealTime.Value = mealOver.Split(","c)(3)
                    beManageOTDec.HolidayOrNot.Value = strHo2
                End If

                If OTSeqs.ToString("00") <> _OTTxnID.Substring(20, 2) Then _OTTxnID = (UserProfile.SelectCompRoleID.Trim + txtOTEmpID.Text.Trim + Convert.ToDateTime(ucOTStartDate.DateText).ToString("yyyyMMdd") + OTSeq.ToString("00"))
                beManageOTDec.OTTxnID.Value = _OTTxnID
                beManageOTDec.SalaryOrAdjust.Value = ddlSalaryOrAdjust.SelectedValue
                If ddlSalaryOrAdjust.SelectedValue = "2" Then beManageOTDec.AdjustInvalidDate.Value = lblAdjustInvalidDate.Text.Trim
                beManageOTDec.OTTypeID.Value = ddlCodeCName.SelectedValue
                beManageOTDec.OTReasonMemo.Value = txtOTReasonMemo.Text.Trim
                beManageOTDec.OTAttachment.Value = attach.Trim
                beManageOTDec.OTFormNO.Value = _FormNo.Trim
                beManageOTDec.LastChgDate.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")

                '開始寫入資料庫
                Try
                    Using cn As DbConnection = Bsp.DB.getConnection("AattendantDB")
                        cn.Open()
                        Dim tran As DbTransaction = cn.BeginTransaction
                        Try
                            If bsManageOTDec.Insert(beManageOTDec, tran) = 0 Then
                                tran.Rollback()
                                Return False
                            Else
                                tran.Commit()
                            End If
                        Catch li As Exception
                            tran.Rollback()
                            Debug.Print("Insert-->" + li.Message)
                            Return False
                        Finally
                            If tran IsNot Nothing Then tran.Dispose()
                        End Try
                    End Using
                Catch ex As Exception
                    Debug.Print("DbConnection Err-->" + ex.Message)
                    Return False
                End Try
            Next
        End If
        Return True
    End Function
#End Region

#Region "執行刪除加班單動作"
    ''' <summary>
    ''' 刪除的子Funct
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DoDelete()
        Dim objOV42 As New OV4_2
        Dim sb As New StringBuilder()

        LoadCheckData()

        Try
            If gvMain.Visible = True AndAlso gvMain.Rows.Count > 0 Then
                Dim dt As DataTable = DirectCast(ViewState("dt"), DataTable)
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        If dt.Rows(i)("OTStartDate").ToString() = dt.Rows(i)("OTEndDate").ToString() Then
                            sb.Clear()
                            sb.Append("UPDATE OverTimeDeclaration SET OTStatus='5',")
                            sb.Append(" OTValidDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',")
                            sb.Append(" OTValidID='" + UserProfile.UserID.Trim + "',")
                            sb.Append(" LastChgComp='" + UserProfile.ActCompID.Trim + "',")
                            sb.Append(" LastChgID='" + UserProfile.UserID.Trim + "',")
                            sb.Append(" LastChgDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'")
                            sb.Append(" WHERE OTEmpID='" + dt.Rows(i)("EmpID") + "'")
                            sb.Append(" AND OTStartDate='" + dt.Rows(i)("OTStartDate") + "'")
                            sb.Append(" AND OTEndDate='" + dt.Rows(i)("OTEndDate") + "'")
                            sb.Append(" AND OTStartTime='" + dt.Rows(i)("OTStartTime") + "'")
                            sb.Append(" AND OTEndTime='" + dt.Rows(i)("OTEndTime") + "'")
                        Else
                            Dim crossDayArray As String = dt.Rows(i)("OTStartDate") + "," + dt.Rows(i)("OTEndDate")
                            For j As Integer = 0 To crossDayArray.Split(","c).Length - 1
                                If crossDayArray.Split(","c)(j) = dt.Rows(i)("OTStartDate").ToString() Then
                                    sb.Clear()
                                    sb.Append("UPDATE OverTimeDeclaration SET OTStatus='5',")
                                    sb.Append(" OTValidDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',")
                                    sb.Append(" OTValidID='" + UserProfile.UserID.Trim + "',")
                                    sb.Append(" LastChgComp='" + UserProfile.ActCompID.Trim + "',")
                                    sb.Append(" LastChgID='" + UserProfile.UserID.Trim + "',")
                                    sb.Append(" LastChgDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'")
                                    sb.Append(" WHERE OTEmpID='" + dt.Rows(i)("EmpID") + "'")
                                    sb.Append(" AND OTStartDate='" + dt.Rows(i)("OTStartDate") + "'")
                                    sb.Append(" AND OTEndDate='" + dt.Rows(i)("OTStartDate") + "'")
                                    sb.Append(" AND OTStartTime='" + dt.Rows(i)("OTStartTime") + "'")
                                    sb.Append(" AND OTEndTime='2359'")
                                Else
                                    sb.Clear()
                                    sb.Append("UPDATE OverTimeDeclaration SET OTStatus='5',")
                                    sb.Append(" OTValidDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',")
                                    sb.Append(" OTValidID='" + UserProfile.UserID.Trim + "',")
                                    sb.Append(" LastChgComp='" + UserProfile.ActCompID.Trim + "',")
                                    sb.Append(" LastChgID='" + UserProfile.UserID.Trim + "',")
                                    sb.Append(" LastChgDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'")
                                    sb.Append(" WHERE OTEmpID='" + dt.Rows(i)("EmpID") + "'")
                                    sb.Append(" AND OTStartDate='" + dt.Rows(i)("OTEndDate") + "'")
                                    sb.Append(" AND OTEndDate='" + dt.Rows(i)("OTEndDate") + "'")
                                    sb.Append(" AND OTStartTime='0000'")
                                    sb.Append(" AND OTEndTime='" + dt.Rows(i)("OTEndTime") + "'")
                                End If
                            Next
                        End If
                    Next
                End If

                Using cn As DbConnection = Bsp.DB.getConnection("AattendantDB")
                    cn.Open()
                    Dim tran As DbTransaction = cn.BeginTransaction

                    Try
                        Bsp.DB.ExecuteDataSet(CommandType.Text, sb.ToString(), tran, "AattendantDB")
                        tran.Commit()
                        Bsp.Utility.ShowMessage(Me, "刪除成功！")
                    Catch ex As Exception
                        LogHelper.WriteSysLog(ex)
                        Debug.Print("刪除失敗(1):" + ex.Message)
                        tran.Rollback()
                        '資料更新失敗
                        Bsp.Utility.ShowMessage(Me, "刪除失敗(1)！")
                        Throw
                    Finally
                        If tran IsNot Nothing Then tran.Dispose()
                    End Try
                End Using
            End If
        Catch ex As Exception
            Debug.Print("刪除失敗(2):" + ex.Message)
            Bsp.Utility.ShowMessage(Me, "刪除失敗(2)！")
        End Try
    End Sub

#Region "舊的刪除加班單Funct"
    Public Function Delete() As Boolean
        Dim beManageOTDec As New beManageOTDec.Row()
        Dim bsManageOTDec As New beManageOTDec.Service()

        Try
            If selectedRows(gvMain) = "-1" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00000")
                Return False
            Else
                For RowCount As Integer = 0 To (gvMain.Rows.Count - 1)
                    Dim chk As CheckBox = gvMain.Rows(RowCount).FindControl("chk_gvMain")
                    If chk.Checked Then
                        Dim OTFormNO As String
                        OTFormNO = If(gvMain.DataKeys(RowCount)("OTFormNO").ToString() IsNot Nothing, gvMain.DataKeys(RowCount)("OTFormNO").ToString(), "")
                        beManageOTDec.OTFormNO.Value = OTFormNO.Trim

                        '5代表刪除
                        beManageOTDec.OTStatus.Value = "5"

                        '檢查資料是否存在
                        If bsManageOTDec.IsDataExists(beManageOTDec) Then
                            Try
                                Return DeleteOTDecSetting(beManageOTDec)
                            Catch ex As Exception
                                Bsp.Utility.ShowMessage(Me, Me.FunID & "DeleteOTTypeSetting()", ex)
                                Return False
                            End Try
                        Else
                            Return False
                        End If
                    End If
                Next
                Return False
            End If
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & "Delete", ex)
            Return False
        End Try
    End Function

    Public Function DeleteOTDecSetting(ByVal beManageOTDec As beManageOTDec.Row) As Boolean
        Dim bsManageOTDec As New beManageOTDec.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("AattendantDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsManageOTDec.UpdateStatus(beManageOTDec, tran) = 0 Then Return False
                tran.Commit()
            Catch ex As Exception
                tran.Rollback()
                Throw New Exception(ex.Message)
            Finally
                If tran IsNot Nothing Then tran.Dispose()
            End Try
        End Using
        Return True
    End Function
#End Region

#End Region

#Region "執行送簽取消動作"
    ''' <summary>
    ''' 取消的子Funct
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DoReject()
        Dim objOV42 As New OV4_2
        Dim sb As New StringBuilder()

        LoadCheckData()

        Try
            If gvMain.Visible = True AndAlso gvMain.Rows.Count > 0 Then
                Dim dt As DataTable = DirectCast(ViewState("dt"), DataTable)
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        If dt.Rows(i)("OTStartDate").ToString() = dt.Rows(i)("OTEndDate").ToString() Then
                            sb.Clear()
                            sb.Append("UPDATE OverTimeDeclaration SET OTStatus='6',")
                            sb.Append(" OTValidDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',")
                            sb.Append(" OTValidID='" + UserProfile.UserID.Trim + "',")
                            sb.Append(" LastChgComp='" + UserProfile.ActCompID.Trim + "',")
                            sb.Append(" LastChgID='" + UserProfile.UserID.Trim + "',")
                            sb.Append(" LastChgDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'")
                            sb.Append(" WHERE OTEmpID='" + dt.Rows(i)("EmpID") + "'")
                            sb.Append(" AND OTStartDate='" + dt.Rows(i)("OTStartDate") + "'")
                            sb.Append(" AND OTEndDate='" + dt.Rows(i)("OTEndDate") + "'")
                            sb.Append(" AND OTStartTime='" + dt.Rows(i)("OTStartTime") + "'")
                            sb.Append(" AND OTEndTime='" + dt.Rows(i)("OTEndTime") + "'")
                        Else
                            Dim crossDayArray As String = dt.Rows(i)("OTStartDate") + "," + dt.Rows(i)("OTEndDate")
                            For j As Integer = 0 To crossDayArray.Split(","c).Length - 1
                                If crossDayArray.Split(","c)(j) = dt.Rows(i)("OTStartDate").ToString() Then
                                    sb.Clear()
                                    sb.Append("UPDATE OverTimeDeclaration SET OTStatus='6',")
                                    sb.Append(" OTValidDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',")
                                    sb.Append(" OTValidID='" + UserProfile.UserID.Trim + "',")
                                    sb.Append(" LastChgComp='" + UserProfile.ActCompID.Trim + "',")
                                    sb.Append(" LastChgID='" + UserProfile.UserID.Trim + "',")
                                    sb.Append(" LastChgDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'")
                                    sb.Append(" WHERE OTEmpID='" + dt.Rows(i)("EmpID") + "'")
                                    sb.Append(" AND OTStartDate='" + dt.Rows(i)("OTStartDate") + "'")
                                    sb.Append(" AND OTEndDate='" + dt.Rows(i)("OTStartDate") + "'")
                                    sb.Append(" AND OTStartTime='" + dt.Rows(i)("OTStartTime") + "'")
                                    sb.Append(" AND OTEndTime='2359'")
                                Else
                                    sb.Clear()
                                    sb.Append("UPDATE OverTimeDeclaration SET OTStatus='6',")
                                    sb.Append(" OTValidDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',")
                                    sb.Append(" OTValidID='" + UserProfile.UserID.Trim + "',")
                                    sb.Append(" LastChgComp='" + UserProfile.ActCompID.Trim + "',")
                                    sb.Append(" LastChgID='" + UserProfile.UserID.Trim + "',")
                                    sb.Append(" LastChgDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'")
                                    sb.Append(" WHERE OTEmpID='" + dt.Rows(i)("EmpID") + "'")
                                    sb.Append(" AND OTStartDate='" + dt.Rows(i)("OTEndDate") + "'")
                                    sb.Append(" AND OTEndDate='" + dt.Rows(i)("OTEndDate") + "'")
                                    sb.Append(" AND OTStartTime='0000'")
                                    sb.Append(" AND OTEndTime='" + dt.Rows(i)("OTEndTime") + "'")
                                End If
                            Next
                        End If
                    Next
                End If

                Using cn As DbConnection = Bsp.DB.getConnection("AattendantDB")
                    cn.Open()
                    Dim tran As DbTransaction = cn.BeginTransaction

                    Try
                        Bsp.DB.ExecuteDataSet(CommandType.Text, sb.ToString(), tran, "AattendantDB")
                        tran.Commit()
                        Bsp.Utility.ShowMessage(Me, "取消成功！")
                    Catch ex As Exception
                        LogHelper.WriteSysLog(ex)
                        Debug.Print("取消失敗(1):" + ex.Message)
                        tran.Rollback()
                        '資料更新失敗
                        Bsp.Utility.ShowMessage(Me, "取消失敗(1)！")
                        Throw
                    Finally
                        If tran IsNot Nothing Then tran.Dispose()
                    End Try
                End Using
                RefreshGrid()
            End If
        Catch ex As Exception
            Debug.Print("取消失敗(2):" + ex.Message)
            Bsp.Utility.ShowMessage(Me, "取消失敗(2)！")
        End Try
    End Sub
#End Region

#Region "清除送簽快取"

    '清除送簽快取
    Protected Sub ClearSubmitCache_Click1(sender As Object, e As System.EventArgs) Handles ClearSubmitCache.Click
        ClearCacheData()
    End Sub

    ''' <summary>
    ''' 清除送簽快取
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ClearCacheData() As Boolean
        Dim sb As New StringBuilder
        sb.Append("DELETE * FROM CacheData WHERE 1=1 AND UserID = " + Bsp.Utility.Quote(UserProfile.ActUserID) + " AND Platform = 'AP' AND SystemID = 'OT' AND TxnName = 'OV4201'")
        Try
            Dim tran As Integer = 0
            tran = Bsp.DB.ExecuteNonQuery(CommandType.Text, sb.ToString, "AattendantDB")
            If tran <> 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Debug.Print("ClearCache()-->" + ex.Message)
            Return False
        End Try
    End Function

#End Region

#End Region

#Region "附件專區"

    ''' <summary>
    ''' 更新附件名稱
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub updateAttachName_Click(sender As Object, e As System.EventArgs) Handles updateAttachName.Click
        getAttachName()
    End Sub

    ''' <summary>
    ''' 查詢附件的檔案名稱
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub getAttachName()
        Dim objOV42 As New OV4_2
        Dim strAttach As String = If(chkCopyAtt.Checked = True And Convert.ToBoolean(ViewState("AttchIn")) = True, ViewState("attach").ToString(), _AttachID)
        Using dt As DataTable = objOV42.QueryData("ISNULL(FileName,'') AS FileName", "AttachInfo", "AND FileSize>0 AND AttachID=" + Bsp.Utility.Quote(_AttachID))
            If dt.Rows.Count > 0 Then
                lblUploadStatus.Text = "附件檔名：" + dt.Rows(0).Item("FileName").ToString()
            Else
                lblUploadStatus.Text = "(目前無附件)"
            End If
        End Using
    End Sub

    ''' <summary>
    ''' 同上筆附檔選擇事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub chkCopyAtt_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkCopyAtt.CheckedChanged
        Dim objOV42 As New OV4_2
        Dim strAttachID As String = String.Empty

        Select Case chkCopyAtt.Checked
            Case True
                If ViewState("attach").ToString().IndexOf("test") < 0 Then
                    btnUploadAttach.Visible = False
                    _AttachID = UserProfile.UserID.Trim + DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fff")
                    ViewState("AttchIn") = True
                    strAttachID = ViewState("attach").ToString()
                    objOV42.InsertAttach(_AttachID, ViewState("attach").ToString())
                    strAttachID = _AttachID
                Else
                    btnUploadAttach.Visible = True
                    ViewState("AttchIn") = False
                    strAttachID = _AttachID
                End If
            Case False
                If ViewState("attach").ToString().IndexOf("test") < 0 Then
                    '附件也需要清掉
                    objOV42.DeleteAttach(_AttachID)
                    strAttachID = _AttachID
                    btnUploadAttach.Visible = True
                    chkCopyAtt.Visible = True
                    If ViewState("attach").ToString() = "" Then chkCopyAtt.Visible = False
                Else
                    chkCopyAtt.Visible = False
                End If
            Case Else

        End Select

        '附件Attach   
        Dim strAttachAdminURL As String
        'Dim strAttachAdminBaseURL As String = ConfigurationManager.AppSettings("AattendantWebPath") + Util._AttachAdminUrl + "?AttachDB={0}&AttachID={1}&AttachFileMaxQty={2}&AttachFileMaxKB={3}&AttachFileTotKB={4}&AttachFileExtList={5}"
        'Dim strAttachAdminBaseURL As String = Util._AttachAdminUrl + "?AttachDB={0}&AttachID={1}&AttachFileMaxQty={2}&AttachFileMaxKB={3}&AttachFileTotKB={4}&AttachFileExtList={5}"
        Dim strAttachAdminBaseURL As String = ConfigurationManager.AppSettings("AattendantWebPath") + "HandlerForOverTime/SSORecvModeForOverTime.aspx?UserID=" + UserProfile.UserID.Trim + "&SystemID=OT&TxnID=OV4201&ReturnUrl=%2FUtil%2FAttachAdmin.aspx?AttachDB={0}%26AttachID={1}%26AttachFileMaxQty={2}%26AttachFileMaxKB={3}%26AttachFileTotKB={4}%26AttachFileExtList={5}"
        Dim strAttachDownloadURL As String
        Dim strAttachDownloadBaseURL As String = Util._AttachDownloadUrl + "?AttachDB={0}&AttachID={1}"
        '附件編號
        strAttachAdminURL = String.Format(strAttachAdminBaseURL, Config_AattendantDBName, strAttachID, "1", "3072", "3072", "")
        strAttachDownloadURL = String.Format(strAttachDownloadBaseURL, Config_AattendantDBName, strAttachID)
        frameAttach.Value = strAttachAdminURL
        getAttachName()

    End Sub

#End Region

#Region "GridView相關"

    ''' <summary>
    ''' GridView事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvMain_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvMain.RowDataBound
        Dim oRow As Data.DataRowView
        Dim BgColor As System.Drawing.Color = Drawing.Color.Empty

        If e.Row.RowType = DataControlRowType.DataRow Then
            '更改表單編號顯示為 8碼+8碼
            If Len(e.Row.Cells(3).Text.Trim) = 16 Then
                Dim OTFormNO As String = Left(e.Row.Cells(3).Text.Trim, 8) & "<br />" & Right(e.Row.Cells(3).Text.Trim, 8)     '複製出來會被換行
                'Dim OTFormNO As String = Left(e.Row.Cells(3).Text.Trim, 8) & "vbCrLf" & Right(e.Row.Cells(3).Text.Trim, 8)
                e.Row.Cells(3).Text = OTFormNO
            End If

            '若表單狀態為送簽則改底色為黃色
            oRow = CType(e.Row.DataItem, Data.DataRowView)
            Select Case oRow("OTStatus")
                Case "2"
                    BgColor = System.Drawing.Color.Yellow
                    e.Row.BackColor = BgColor
                Case "4"
                    BgColor = System.Drawing.Color.LightGray
                    e.Row.BackColor = BgColor
                    Dim chk As CheckBox = e.Row.FindControl("chk_gvMain")
                    chk.Enabled = False
                Case "5"
                    BgColor = System.Drawing.Color.LightGray
                    e.Row.BackColor = BgColor
                    Dim chk As CheckBox = e.Row.FindControl("chk_gvMain")
                    chk.Enabled = False
                Case "6"
                    BgColor = System.Drawing.Color.LightGray
                    e.Row.BackColor = BgColor
                    Dim chk As CheckBox = e.Row.FindControl("chk_gvMain")
                    chk.Enabled = False
                Case Else
                    BgColor = System.Drawing.Color.White
                    e.Row.BackColor = BgColor
                    e.Row.Cells(4).Style.Add("word-break", "break-all")
            End Select
        End If
    End Sub

    ''' <summary>
    ''' GridView data
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RefreshGrid()
        Dim b = ""      '利用表單編號尋找gridview
        If Not String.IsNullOrEmpty(_FormNoRecord) Then
            'string a = "'" + (_FormNoRecord.Replace(";", "','")) + "'";
            'b = a.Substring(0, a.Length - 3);
            Dim a As String = "'" + (_FormNoRecord.Replace(";", "','")) + "'"
            b = a.Substring(0, a.Length - 3)
        Else
            b = "''" '    b = _FormNo
        End If

        'Dim db As New DbHelper("AattendantDB")
        'Dim sb As CommandHelper = db.CreateCommandHelper()
        Dim sb As New StringBuilder

        sb.Clear()
        sb.Append(" SELECT OT.OTRegisterComp, OT.OTEmpID,OT.OTCompID,OT.DeptID,OT.OrganID,OT.OTFormNO,P.NameN,OT.OTRegisterID,OT.OTTxnID,OT.OTTypeID,OTT.CodeCName,ISNULL(AI.FileName,'') AS FileName,OT.OTStatus,OT.OTSeq,ISNULL(OT.OTFromAdvanceTxnId,'') AS OTFromAdvanceTxnId,")
        sb.Append(" Case OT.OTStatus WHEN '1' THEN '暫存' WHEN '2' THEN '送簽' WHEN '3' THEN '核准' WHEN '4' THEN '駁回' WHEN '5' THEN '刪除' WHEN '6' THEN '取消' WHEN '7' THEN '作廢' WHEN '9' THEN '計薪後收回' END AS OTStatusName,")
        sb.Append(" (OT.OTStartDate+'~'+isnull(OT2.OTEndDate,OT.OTEndDate)) AS OTDate,")
        'sb.Append(" (OT.OTStartTime+'~'+isnull(OT2.OTEndTime,OT.OTEndTime)) AS OTTime,")//加format格式
        sb.Append(" (Left(OT.OTStartTime,2)+':'+Right(OT.OTStartTime,2)+'~'+ isnull(Left(OT2.OTEndTime,2)+':'+Right(OT2.OTEndTime,2),Left(OT.OTEndTime,2)+':'+Right(OT.OTEndTime,2))) AS OTTime,")
        'sb.Append(" Convert(Decimal(10,1),Round(OT.OTTotalTime-Convert(Decimal(10,1),Convert(Decimal(10,2),OT.MealTime)/60)+isnull(OT2.OTTotalTime,0)-isnull(Convert(Decimal(10,1),Convert(Decimal(10,2),OT2.MealTime)/60),0),1)) AS OTTotalTime ")
        'sb.Append(" Convert(Decimal(10,1),((CAST(OT.OTTotalTime AS FLOAT)-CAST(OT.MealTime AS FLOAT))/CAST(60 AS FLOAT))+((CAST(ISNULL(OT2.OTTotalTime,0) AS FLOAT)-CAST(ISNULL(OT2.MealTime,0) AS FLOAT))/CAST(60 AS FLOAT))) AS OTTotalTime")
        sb.Append(" Convert(Decimal(10,1),ROUND(Convert(Decimal(10,2),((CAST(OT.OTTotalTime AS FLOAT)-CAST(OT.MealTime AS FLOAT))/CAST(60 AS FLOAT))+((CAST(ISNULL(OT2.OTTotalTime,0) AS FLOAT)-CAST(ISNULL(OT2.MealTime,0) AS FLOAT))/CAST(60 AS FLOAT))),1)) AS OTTotalTime ")
        '資料庫用分鐘計算
        sb.Append(" FROM OverTimeDeclaration OT ")
        sb.Append(" LEFT JOIN OverTimeDeclaration OT2 on OT2.OTTxnID=OT.OTTxnID AND OT2.OTSeqNo=2 AND OT2.OverTimeFlag='1'")
        sb.Append(" LEFT JOIN " + _eHRMSDB_ITRD + ".[dbo].[Personal] P ON P.EmpID=OT.OTEmpID AND P.CompID = OT.OTCompID ")
        sb.Append(" LEFT JOIN AT_CodeMap AS OTT ON OT.OTTypeID = OTT.Code AND OTT.TabName='OverTime' AND OTT.FldName='OverTimeType'")
        sb.Append(" LEFT JOIN AttachInfo AI ON AI.AttachID IS NOT NULL AND AI.AttachID <> '' AND AI.AttachID = OT.OTAttachment AND FileSize>0")
        'sb.Append(" WHERE OT.OTSeqNo=1 AND OT.OTCompID = '" + UserInfo.getUserInfo().CompID + "' AND OT.OTRegisterID='" + UserInfo.getUserInfo().UserID + "' AND OT.OTFormNO='" + _FormNo + "' AND OT.OTStatus='1' AND OT.LastChgDate BETWEEN '" + hidGridViewSearchStartTime.Value + "' AND '" + hidGridViewSearchEndTime.Value + "'")
        'sb.Append(" WHERE OT.OTSeqNo=1 AND OT.OTCompID = '" + UserInfo.getUserInfo().CompID + "' AND OT.OTRegisterID='" + UserInfo.getUserInfo().UserID + "' AND OT.LastChgDate BETWEEN '" + hidGridViewSearchStartTime.Value + "' AND '" + hidGridViewSearchEndTime.Value + "' AND OT.OTStatus='1'")
        sb.Append(" WHERE OT.OTSeqNo=1 AND OT.OTCompID = '" + hidCompID.Value.ToString() + "' AND OT.OTRegisterID='" + UserProfile.UserID.Trim + "' AND OT.OTFormNO IN (" + b + ") AND OT.OTStatus='1' AND OT.OverTimeFlag='1' AND OT.HRKeyInFlag = '1'")
        'sb.Append(" WHERE OT.OTSeqNo=1 AND OT.OTCompID = '" + UserInfo.getUserInfo().CompID + "' AND OT.OTRegisterID='" + UserInfo.getUserInfo().UserID + "' AND OT.OTFormNO= '" + _FormNo + "' AND OT.OTStatus='1' AND OT.OverTimeFlag='1'")
        sb.Append(" ORDER BY OTTxnID")

        'db.ExecuteDataSet(sb.BuildCommand()).Tables(0)
        Dim dtGrid As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, sb.ToString, "AattendantDB").Tables(0)
        gvMain.DataSource = dtGrid
        gvMain.DataBind()
        ResetGrid() '清除GrideView上的資料
        PnlgvMain.Visible = True
        gvMain.Visible = True

        Try
            dtGrid.Clear()
            dtGrid.Dispose()
        Catch ex As Exception
            Debug.Print("RefreshGrid():清除DataTable失敗==>" + ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' 重整GridView
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub LoadCheckData()
        Dim objOV42 As New OV4_2
        Dim dt As New DataTable()
        dt.Columns.Add("EmpID")
        dt.Columns.Add("OTStartDate")
        dt.Columns.Add("OTEndDate")
        dt.Columns.Add("OTStartTime")
        dt.Columns.Add("OTEndTime")
        dt.Columns.Add("OTCompID")
        'dt.Columns.Add("OTSeq")
        dt.Columns.Add("OTFormNO")
        dt.Columns.Add("OTTotalTime")
        dt.Columns.Add("OTFromAdvanceTxnId")
        Dim OTSeq As String = ""
        For Each row As GridViewRow In gvMain.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim chk_chkOverTime As CheckBox = DirectCast(row.Cells(0).FindControl("chk_gvMain"), CheckBox)
                If chk_chkOverTime.Checked Then
                    Dim dr As DataRow = dt.NewRow()
                    dr("EmpID") = gvMain.DataKeys(row.RowIndex).Values("OTEmpID").ToString()
                    dr("OTStartDate") = gvMain.DataKeys(row.RowIndex).Values("OTDate").ToString().Split("~"c)(0)
                    dr("OTEndDate") = gvMain.DataKeys(row.RowIndex).Values("OTDate").ToString().Split("~"c)(1)
                    dr("OTStartTime") = (gvMain.DataKeys(row.RowIndex).Values("OTTime").ToString().Split("~"c)(0)).Replace(":", "")
                    dr("OTEndTime") = (gvMain.DataKeys(row.RowIndex).Values("OTTime").ToString().Split("~"c)(1)).Replace(":", "")
                    dr("OTTotalTime") = gvMain.DataKeys(row.RowIndex).Values("OTTotalTime").ToString()
                    dr("OTCompID") = gvMain.DataKeys(row.RowIndex).Values("OTCompID").ToString()
                    dr("OTFormNO") = gvMain.DataKeys(row.RowIndex).Values("OTFormNO").ToString()
                    dr("OTFromAdvanceTxnId") = gvMain.DataKeys(row.RowIndex).Values("OTFromAdvanceTxnId").ToString()
                    'If dr("OTStartDate").ToString() = dr("OTEndDate").ToString() Then
                    '    OTSeq = objOV42.QueryColumn("OTSeq", "OverTimeDeclaration", " AND OTEmpID='" + gvMain.DataKeys(row.RowIndex).Values("OTEmpID").ToString() + " ' AND OTStartDate='" + gvMain.DataKeys(row.RowIndex).Values("OTDate").ToString().Split("~")(0) + "' AND OTEndDate='" + gvMain.DataKeys(row.RowIndex).Values("OTDate").ToString().Split("~")(1) + "' AND OTStartTime='" + (gvMain.DataKeys(row.RowIndex).Values("OTTime").ToString().Split("~")(0)).Replace(":", "") + "' AND OTEndTime='" + (gvMain.DataKeys(row.RowIndex).Values("OTTime").ToString().Split("~")(1)).Replace(":", "") + "' and OTStatus='1'")
                    'Else
                    '    OTSeq = objOV42.QueryColumn("OTSeq", "OverTimeDeclaration", " AND OTEmpID='" + gvMain.DataKeys(row.RowIndex).Values("OTEmpID").ToString() + " ' AND OTStartDate='" + gvMain.DataKeys(row.RowIndex).Values("OTDate").ToString().Split("~")(0) + "' AND OTEndDate='" + gvMain.DataKeys(row.RowIndex).Values("OTDate").ToString().Split("~")(0) + "' AND OTStartTime='" + (gvMain.DataKeys(row.RowIndex).Values("OTTime").ToString().Split("~")(0)).Replace(":", "") + "' AND OTEndTime='2359' and OTStatus='1'")
                    'End If

                    'dr("OTSeq") = OTSeq
                    dt.Rows.Add(dr)
                End If
                ViewState("dt") = dt
            End If
        Next
    End Sub

    ''' <summary>
    ''' 清除與重整GridView
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ResetGrid()
        'Table顯示

        For i As Integer = 0 To gvMain.Rows.Count - 1 Step 1
            Dim objChk As CheckBox = gvMain.Rows(i).FindControl("chk_gvMain")
            If objChk.Checked Then
                objChk.Checked = False
            End If
        Next
        ScriptManager.RegisterStartupScript(Me, Page.GetType, "script", "gridClear()", True)
    End Sub
#End Region

#Region "檢核與條件檢查區"

#Region "多筆送簽檢核"

    ''' <summary>
    ''' 送簽前的資料檢查(是否超過每月加班時數上限、每日加班時數上限)
    ''' </summary>
    ''' <returns>boolean</returns>
    ''' <remarks></remarks>
    Private Function funCheckMultiSubmit() As Boolean
        Dim objOV42 As New OV4_2
        Dim dtData As DataTable = Nothing
        Dim dt As New DataTable
        Dim ErrMsg As String = ""
        dt.Columns.Add("EmpID")
        dt.Columns.Add("OTStartDate")
        dt.Columns.Add("OTEndDate")
        dt.Columns.Add("OTStartTime")
        dt.Columns.Add("OTEndTime")
        dt.Columns.Add("OTCompID")
        'dt.Columns.Add("OTSeq")
        dt.Columns.Add("OTFormNO")
        dt.Columns.Add("OTStatusName")
        dt.Columns.Add("OTFromAdvanceTxnId")    '從事前直接新增一筆資料到事後申報
        dt.Columns.Add("OTTotalTime")
        dt.Columns.Add("OTTxnID")

        Dim OTSeq As String = ""

        '讀取參數檔
        Dim _dtPara As DataTable = objOV42.Json2DataTable(objOV42.QueryColumn("Para", "OverTimePara", " AND CompID = " + Bsp.Utility.Quote(hidCompID.Value.ToString().Trim)))

        Try
            For RowCount As Integer = 0 To (gvMain.Rows.Count - 1)
                Dim chk As CheckBox = gvMain.Rows(RowCount).FindControl("chk_gvMain")
                If chk.Checked Then
                    Dim dr As DataRow = dt.NewRow()
                    dr("EmpID") = gvMain.DataKeys(RowCount).Values("OTEmpID").ToString()
                    dr("OTStartDate") = gvMain.DataKeys(RowCount).Values("OTDate").ToString().Split("~")(0)
                    dr("OTEndDate") = gvMain.DataKeys(RowCount).Values("OTDate").ToString().Split("~")(1)
                    dr("OTStartTime") = (gvMain.DataKeys(RowCount).Values("OTTime").ToString().Split("~")(0)).Replace(":", "")
                    dr("OTEndTime") = (gvMain.DataKeys(RowCount).Values("OTTime").ToString().Split("~")(1)).Replace(":", "")
                    dr("OTTotalTime") = gvMain.DataKeys(RowCount).Values("OTTotalTime").ToString()
                    dr("OTCompID") = gvMain.DataKeys(RowCount).Values("OTCompID").ToString()
                    dr("OTFormNO") = gvMain.DataKeys(RowCount).Values("OTFormNO").ToString()
                    dr("OTStatusName") = gvMain.DataKeys(RowCount).Values("OTStatus").ToString()
                    dr("OTTxnID") = gvMain.DataKeys(RowCount).Values("OTTxnID").ToString()
                    dr("OTFromAdvanceTxnId") = gvMain.DataKeys(RowCount).Values("OTFromAdvanceTxnId").ToString()

                    'If (gvMain.DataKeys(RowCount).Values("OTDate").ToString().Split("~")(0) = gvMain.DataKeys(RowCount).Values("OTDate").ToString().Split("~")(1)) Then
                    '    OTSeq = objOV42.QueryColumn("OTSeq", "OverTimeDeclaration", " AND OTEmpID=" + Bsp.Utility.Quote(gvMain.DataKeys(RowCount).Values("OTEmpID").ToString()) + " AND OTStartDate=" + Bsp.Utility.Quote(gvMain.DataKeys(RowCount).Values("OTDate").ToString().Split("~")(0)) + " AND OTEndDate=" + Bsp.Utility.Quote(gvMain.DataKeys(RowCount).Values("OTDate").ToString().Split("~")(1)) + " AND OTStartTime=" + Bsp.Utility.Quote((gvMain.DataKeys(RowCount).Values("OTTime").ToString().Split("~")(0)).Replace(":", "")) + " AND OTEndTime=" + Bsp.Utility.Quote((gvMain.DataKeys(RowCount).Values("OTTime").ToString().Split("~")(1)).Replace(":", "")) + " AND OTStatus=" + Bsp.Utility.Quote(gvMain.DataKeys(RowCount).Values("OTStatus").ToString()))
                    'Else
                    '    OTSeq = objOV42.QueryColumn("OTSeq", "OverTimeDeclaration", " AND OTEmpID=" + Bsp.Utility.Quote(gvMain.DataKeys(RowCount).Values("OTEmpID").ToString()) + " AND OTStartDate=" + Bsp.Utility.Quote(gvMain.DataKeys(RowCount).Values("OTDate").ToString().Split("~")(0)) + " AND OTEndDate=" + Bsp.Utility.Quote(gvMain.DataKeys(RowCount).Values("OTDate").ToString().Split("~")(0)) + " AND OTStartTime=" + Bsp.Utility.Quote((gvMain.DataKeys(RowCount).Values("OTTime").ToString().Split("~")(0)).Replace(":", "")) + " AND OTEndTime='2359' AND OTStatus=" + Bsp.Utility.Quote(gvMain.DataKeys(RowCount).Values("OTStatus").ToString()))
                    'End If
                    'dr("OTSeq") = OTSeq

                    dt.Rows.Add(dr)
                End If
            Next

            If dt.Rows.Count > 0 Then
                For i = 0 To (dt.Rows.Count - 1)
                    '檢查每天的加班時數是否超出上限
                    Dim dayNLimit As Double = Convert.ToDouble(_dtPara.Rows(0)("DayLimitHourN").ToString()) '平日加班上限
                    Dim dayHLimit As Double = Convert.ToDouble(_dtPara.Rows(0)("DayLimitHourH").ToString())    '假日加班上限
                    Dim strCheckOverTimeIsOver As String = objOV42.GetCheckOverTimeIsOver(dt, dayNLimit, dayHLimit, "OverTimeDeclaration")
                    If Not Convert.ToBoolean(strCheckOverTimeIsOver.Split(";")(0)) Then
                        'Bsp.Utility.ShowMessage(Me, "員工編號(" + strCheckOverTimeIsOver.Split(";")(1) + ")" + strCheckOverTimeIsOver.Split(";")(2) + "已超過每天上限加班時數" + strCheckOverTimeIsOver.Split(";")(3) + "小時")
                        ErrMsg += "員工編號(" + strCheckOverTimeIsOver.Split(";")(1) + ")" + strCheckOverTimeIsOver.Split(";")(2) + "已超過每天上限加班時數" + strCheckOverTimeIsOver.Split(";")(3) + "小時" & vbNewLine
                        If _dtPara.Rows(0)("MonthLimitFlag").ToString() = "1" Then
                            'Return False
                            '20170314-HR不擋僅通知-讓檢核能通過
                            Return True
                        End If
                    End If

                    '檢查每個月的加班時數是否超出上限
                    Dim resultMsg As String = objOV42.GetMulitTotal(dt, Convert.ToDouble(_dtPara.Rows(0)("MonthLimitHour").ToString()), "OverTimeDeclaration")
                    If Not Convert.ToBoolean(resultMsg.Split(";")(0)) Then
                        'Bsp.Utility.ShowMessage(Me, "員工編號(" + resultMsg.Split(";")(1) + ")" + (resultMsg.Split(";")(2)).ToString().Substring(5, 2) + "月已超過每月上限加班時數" + _dtPara.Rows(0)("MonthLimitHour") + "小時")
                        ErrMsg += "員工編號(" + resultMsg.Split(";")(1) + ")" + (resultMsg.Split(";")(2)).ToString().Substring(5, 2) + "月已超過每月上限加班時數" + _dtPara.Rows(0)("MonthLimitHour") + "小時" & vbNewLine
                        If (_dtPara.Rows(0)("MonthLimitFlag").ToString() = "1") Then
                            'Return False
                            '20170314-HR不擋僅通知-讓檢核能通過
                            Return True
                        End If
                    End If

                    '檢查連續上班是否超過限制
                    Dim strGetCheckOTLimitDay As String = objOV42.GetCheckOTLimitDay(dt, _dtPara.Rows(0)("OTLimitDay").ToString(), "OverTimeDeclaration")
                    If Not Convert.ToBoolean(strGetCheckOTLimitDay.Split(";"c)(0)) Then
                        'Bsp.Utility.ShowMessage(Me, "員工編號(" + strGetCheckOTLimitDay.Split(";"c)(1) + ")" + "不得連續上班超過" + _dtPara.Rows(0)("OTLimitDay").ToString() + "天")
                        ErrMsg += "員工編號(" + strGetCheckOTLimitDay.Split(";"c)(1) + ")" + "不得連續上班超過" + _dtPara.Rows(0)("OTLimitDay").ToString() + "天" & vbNewLine
                        If _dtPara.Rows(0)("OTLimitFlag").ToString() = "1" Then
                            'Return False
                            '20170314-HR不擋僅通知-讓檢核能通過
                            Return True
                        End If
                    End If
                Next
            End If
            If ErrMsg <> "" Then
                Bsp.Utility.ShowMessage(Me, ErrMsg)
            End If
        Catch ex As Exception
            Debug.Print("funCheckMultiSubmit()==>" + ex.Message)
            Bsp.Utility.ShowMessage(Me, "送簽失敗!!")
            Return False
        End Try
        Return True
    End Function

#End Region

    ''' <summary>
    ''' 檢查狀態
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function funCheckStatus() As Boolean
        'Bsp.Utility.ShowMessage(Me, "進來了")
        For RowCount As Integer = 0 To (gvMain.Rows.Count - 1)
            Dim chk As CheckBox = gvMain.Rows(RowCount).FindControl("chk_gvMain")
            If chk.Checked Then
                Select Case ViewState("Param")
                    Case "btnExecutes"  '是送簽
                        If gvMain.DataKeys(RowCount)("OTStatus").ToString() <> "1" Then
                            Bsp.Utility.ShowMessage(Me, "此狀態無法送簽")
                            Return False
                        Else
                            '若檢查通過則開始送簽前資料檢查(是否超過每月加班時數上限、每日加班時數上限)
                            If funCheckMultiSubmit() = False Then
                                '20170314-HR不擋僅通知-讓檢核能通過
                                'Return False
                            End If

                            'If gvMain.DataKeys(RowCount)("OTStartDate").ToString() > Now.Date.ToString("yyyy/MM/dd") Then
                            '    Bsp.Utility.ShowMessage(Me, "日期為未來日期，無法送簽")
                            '    Return False
                            'ElseIf gvMain.DataKeys(RowCount)("OTStartDate").ToString() = Now.Date.ToString("yyyy/MM/dd") Then
                            '    Dim OTStartTime As String = gvMain.DataKeys(RowCount)("OTTime").ToString().Split("~")(0).Replace(":", "")
                            '    Dim OTEndTime As String = gvMain.DataKeys(RowCount)("OTTime").ToString().Split("~")(1).Replace(":", "")
                            '    If OTStartTime > Now.ToString("HHmm") Or OTEndTime > Now.ToString("HHmm") Then
                            '        Bsp.Utility.ShowMessage(Me, "日期為未來日期，無法送簽")
                            '        Return False
                            '    End If
                            'End If
                        End If
                    Case "btnDelete"  '是刪除
                        If gvMain.DataKeys(RowCount)("OTStatus").ToString() <> "1" Then
                            Bsp.Utility.ShowMessage(Me, "此狀態無法刪除")
                            Return False
                        End If
                    Case "btnReject"    '是取消
                        If gvMain.DataKeys(RowCount)("OTStatus").ToString() <> "2" Then
                            Bsp.Utility.ShowMessage(Me, "此狀態無法取消")
                            Return False
                        End If
                    Case Else           '是來亂的
                        Debug.Print("funCheckStatus()==>有人來亂,Param = " + ViewState("Param").ToString())
                        Return False
                End Select
            End If
        Next
        ' Bsp.Utility.ShowMessage(Me, "出去了")
        Return True
    End Function

    ''' <summary>
    ''' 更新之前的條件檢查
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function funCheckData() As Boolean
        Dim beManageOTDec As New beManageOTDec.Row()
        Dim bsManageOTDec As New beManageOTDec.Service()
        Dim objOV42 As New OV4_2
        Dim Arr As New ArrayList()
        Dim ArrFlow As New ArrayList()
        Dim OrganStr As String = ""
        Dim OrganFlowStr As String = ""
        Dim i As Integer
        Dim ChkData As Boolean = True
        Dim sb As New StringBuilder
        Dim ErrMsg As String = ""       '檢核不擋的錯誤訊息串

        beManageOTDec.OTCompID.Value = UserProfile.SelectCompRoleID.Trim
        beManageOTDec.OTEmpID.Value = txtOTEmpID.Text.Trim.ToString()

        If ViewState("Param") = "btnExecutes" Then
            If gvMain.Rows.Count > 0 And gvMain.Visible = True Then
                Dim chkCnt As Integer = 0
                For intRow As Integer = 0 To gvMain.Rows.Count - 1
                    Dim objChk As CheckBox = gvMain.Rows(intRow).FindControl("chk_gvMain")
                    If objChk.Checked Then
                        chkCnt = chkCnt + 1
                    End If
                Next
                If chkCnt = 0 Then
                    Bsp.Utility.ShowMessage(Me, "尚未勾選資料")
                    Return False
                Else
                    ChkData = False
                End If
            End If
        End If

        If ChkData Then
            '檢查是否有輸入
            If txtOTEmpID.Text.Trim = "" Then
                Bsp.Utility.ShowMessage(Me, "您必須輸入加班人員工編號")
                txtOTEmpID.Focus()
                Return False
            End If

            '檢查是否是有效的使用者(看有沒有出現員工姓名~)
            If lblOTEmpNameN.Text.Trim = "" Or lblOTEmpNameN.Text.Trim = "查無此筆資料!" Then
                Bsp.Utility.ShowMessage(Me, "您必須輸入有效的加班人員工編號")
                txtOTEmpID.Focus()
                Return False
            End If

            '檢查是否有選擇多筆或單筆加班人
            If rblOTPerson.SelectedValue = "" Then
                Bsp.Utility.ShowMessage(Me, "您必須選擇是單筆或多筆加班人")
                rblOTPerson.Focus()
                Return False
            End If

            '加班轉換方式
            If ddlSalaryOrAdjust.SelectedValue = "" Then
                Bsp.Utility.ShowMessage(Me, "需選擇加班轉換方式")
                ddlSalaryOrAdjust.Focus()
                Return False
            End If

            '加班開始日期與結束日期
            If ucOTStartDate.DateText = "" Then
                Bsp.Utility.ShowMessage(Me, "您必須輸入加班開始日期")
                ucOTStartDate.Focus()
                Return False
            End If

            If ucOTEndDate.DateText = "" Then
                Bsp.Utility.ShowMessage(Me, "您必須輸入加班結束日期")
                ucOTEndDate.Focus()
                Return False
            End If

            '如果有未來日期
            If DateTime.Now.Date < Convert.ToDateTime(ucOTStartDate.DateText) OrElse DateTime.Now < Convert.ToDateTime(ucOTEndDate.DateText) Then
                Bsp.Utility.ShowMessage(Me, "加班日期不可以選擇未來日期")
                Return False
            ElseIf DateTime.Now.Date = Convert.ToDateTime(ucOTStartDate.DateText) Then
                If Convert.ToInt32(ucOTStartTime.ucDefaultSelectedHH) > Convert.ToInt32(DateTime.Now.Hour) Then
                    Bsp.Utility.ShowMessage(Me, "不可以申報未來時間點")
                    Return False
                ElseIf Convert.ToInt32(ucOTStartTime.ucDefaultSelectedHH) = Convert.ToInt32(DateTime.Now.Hour) AndAlso Convert.ToInt32(ucOTStartTime.ucDefaultSelectedMM) > Convert.ToInt32(DateTime.Now.Minute) Then
                    Bsp.Utility.ShowMessage(Me, "不可以申報未來時間點")
                    Return False
                End If
            End If

            '加班開始時間與結束時間
            If ucOTStartTime.ucDefaultSelectedHH = "請選擇" Or ucOTStartTime.ucDefaultSelectedHH = "請選擇" Then
                Bsp.Utility.ShowMessage(Me, "您必須輸入加班開始時間")
                Return False
            End If
            If ucOTEndTime.ucDefaultSelectedHH = "請選擇" Or ucOTEndTime.ucDefaultSelectedHH = "請選擇" Then
                Bsp.Utility.ShowMessage(Me, "您必須輸入加班結束時間")
                Return False
            End If

            '加班類型
            If ddlCodeCName.SelectedValue = "" Then
                Bsp.Utility.ShowMessage(Me, "您必須選擇加班類型")
                ddlCodeCName.Focus()
                Return False
            End If

            '加班原因
            If txtOTReasonMemo.Text.Trim = "" Then
                Bsp.Utility.ShowMessage(Me, "您必須填寫加班原因")
                txtOTReasonMemo.Focus()
                Return False
            End If

            '用餐時數
            If MealFlag.Checked Then
                If txtMealTime.Text = "" Or Convert.ToDouble(txtMealTime.Text.Trim) <= 0 Then
                    Bsp.Utility.ShowMessage(Me, "請填寫正確的用餐時間")
                    txtMealTime.Enabled = True
                    txtMealTime.Focus()
                    Return False
                End If
            End If

            '用餐時數大於加班時間
            Dim cntTotal As Double = 0.0
            Dim cntStart As Double = 0.0
            Dim cntEnd As Double = 0.0
            If (ucOTStartDate.DateText = ucOTEndDate.DateText) Then '不跨日
                cntTotal = (Convert.ToDouble(ucOTEndTime.ucDefaultSelectedHH) * 60 - Convert.ToDouble(ucOTStartTime.ucDefaultSelectedHH) * 60 + (Convert.ToDouble(ucOTEndTime.ucDefaultSelectedMM) - Convert.ToDouble(ucOTStartTime.ucDefaultSelectedMM)))
                If (txtMealTime.Text <> "" And Convert.ToDouble(txtMealTime.Text) >= (cntTotal)) Then
                    Bsp.Utility.ShowMessage(Me, "用餐時數超過加班時數")
                    Return False
                End If
            Else
                cntStart = (24 - (Convert.ToDouble(ucOTStartTime.ucDefaultSelectedHH))) * 60 - Convert.ToDouble(ucOTStartTime.ucDefaultSelectedMM)
                cntEnd = (Convert.ToDouble(ucOTEndTime.ucDefaultSelectedHH)) * 60 + Convert.ToDouble(ucOTEndTime.ucDefaultSelectedMM)
                If (txtMealTime.Text <> "" And Convert.ToDouble(txtMealTime.Text) >= (cntEnd + cntStart)) Then
                    Bsp.Utility.ShowMessage(Me, "用餐時數超過加班時數")
                    Return False
                End If
            End If

            '2017/03/17-新的判斷連續假日邏輯
            '先檢核目前單是否有一般假日連續上班情形
            Dim ChkMsg As String = ""
            If objOV42.CheckHolidayOrNot(ucOTStartDate.DateText) Then  '如果加班起日是假日
                If ucOTStartDate.DateText <> ucOTEndDate.DateText Then
                    If (Weekday(Convert.ToDateTime(ucOTStartDate.DateText)) = 0 OrElse Weekday(Convert.ToDateTime(ucOTStartDate.DateText)) = 7) AndAlso (Weekday(Convert.ToDateTime(ucOTEndDate.DateText)) = 0 OrElse Weekday(Convert.ToDateTime(ucOTEndDate.DateText)) = 7) Then    '如果加班起訖日是周末
                        ErrMsg += "不能假日連續加班" & vbNewLine    '周末連續加班為假日連續加班
                    ElseIf (Not OVBusinessCommon.IsNationalHoliday(ucOTStartDate.DateText)) Then    '如果加班起日是非周末之一般假日
                        If objOV42.CheckHolidayOrNot(ucOTEndDate.DateText) AndAlso (Weekday(Convert.ToDateTime(ucOTEndDate.DateText)) = 0 OrElse Weekday(Convert.ToDateTime(ucOTEndDate.DateText)) = 7) Then    '如果加班迄日為周末之一般假日
                            ErrMsg += "不能假日連續加班" & vbNewLine    '一般假日跨一般周末假日為假日連續加班
                        ElseIf Not OVBusinessCommon.IsNationalHoliday(ucOTEndDate.DateText) Then
                            ErrMsg += "不能假日連續加班" & vbNewLine    '一般假日跨非周末的一般假日為假日連續加班
                        Else
                            If Not objOV42.CheckNHolidayOTOrNot(hidCompID.Value.ToString().Trim, txtOTEmpID.Text.Trim, ucOTStartDate.DateText, ucOTEndDate.DateText, "", "Add", ChkMsg) Then
                                ErrMsg += ChkMsg
                            End If
                        End If
                    End If
                Else    '非跨日直接判斷
                    If Not objOV42.CheckNHolidayOTOrNot(hidCompID.Value.ToString().Trim, txtOTEmpID.Text.Trim, ucOTStartDate.DateText, ucOTEndDate.DateText, "", "Add", ChkMsg) Then
                        ErrMsg += ChkMsg
                    End If
                End If
            End If

            'Dim ChkMsg As String = ""
            'If Not objOV42.CheckNHolidayOTOrNot(hidCompID.Value.ToString().Trim, txtOTEmpID.Text.Trim, ucOTStartDate.DateText, ucOTEndDate.DateText, "", "Add", ChkMsg) Then
            '    ErrMsg += ChkMsg
            'End If

            '2017/03/17-舊的判斷連續假日邏輯
            ''If Not OVBusinessCommon.IsNationalHoliday(ucOTStartDate.DateText) Then  '如果是國定假日則跳過判斷
            ''檢查連續加班如果開始日期為假日
            'If objOV42.CheckHolidayOrNot(ucOTStartDate.DateText) Then
            '    Dim a As Date = Convert.ToDateTime(ucOTStartDate.DateText).AddDays(-1)  ''檢查前一天是否有存在資料庫
            '    Dim b As Date = Convert.ToDateTime(ucOTStartDate.DateText).AddDays(1)  ''檢查後一天是否有存在資料庫
            '    '檢查事後申報是否連續加班
            '    If objOV42.CheckHolidayOrNot(a.ToString("yyyy/MM/dd")) Then
            '        Using dt As DataTable = objOV42.QueryData("*", "OverTimeDeclaration", " AND OTStartDate= " + Bsp.Utility.Quote(a.ToString("yyyy/MM/dd")) + " AND OTCompID=" + Bsp.Utility.Quote(hidCompID.Value.ToString()) + " AND OTEmpID =" + Bsp.Utility.Quote(txtOTEmpID.Text.Trim) + " AND OTStatus in ('1','2','3') AND OTStartDate <> ( SELECT Code FROM AT_CodeMap WHERE Code = OTStartDate AND NotShowFlag = '0' AND TabName = 'NationalHolidayDefine' AND FldName ='HolidayDate')")
            '            If dt.Rows.Count > 0 Then
            '                '20170314-HR不擋僅通知-讓檢核能通過
            '                'Bsp.Utility.ShowMessage(Me, "不能假日連續加班")
            '                'Return False
            '                ErrMsg += "不能假日連續加班" & vbNewLine
            '            End If
            '        End Using
            '    ElseIf objOV42.CheckHolidayOrNot(b.ToString("yyyy/MM/dd")) Then
            '        Using dt As DataTable = objOV42.QueryData("*", "OverTimeDeclaration", " AND OTStartDate= " + Bsp.Utility.Quote(b.ToString("yyyy/MM/dd")) + " AND OTCompID=" + Bsp.Utility.Quote(hidCompID.Value.ToString()) + " AND OTEmpID =" + Bsp.Utility.Quote(txtOTEmpID.Text.Trim) + " AND OTStatus in ('1','2','3') AND OTStartDate <> ( SELECT Code FROM AT_CodeMap WHERE Code = OTStartDate AND NotShowFlag = '0' AND TabName = 'NationalHolidayDefine' AND FldName ='HolidayDate')")
            '            If dt.Rows.Count > 0 Then
            '                '20170314-HR不擋僅通知-讓檢核能通過
            '                'Bsp.Utility.ShowMessage(Me, "不能假日連續加班")
            '                'Return False
            '                ErrMsg += "不能假日連續加班" & vbNewLine
            '            End If
            '        End Using
            '    End If

            '    '檢查事先申請是否連續加班
            '    If objOV42.CheckHolidayOrNot(a.ToString("yyyy/MM/dd")) Then
            '        Using dt As DataTable = objOV42.QueryData("*", "OverTimeAdvance", " AND OTStartDate= " + Bsp.Utility.Quote(a.ToString("yyyy/MM/dd")) + " AND OTCompID=" + Bsp.Utility.Quote(hidCompID.Value.ToString()) + " AND OTEmpID =" + Bsp.Utility.Quote(txtOTEmpID.Text.Trim) + " AND OTStatus in ('1','2','3') AND OTStartDate <> ( SELECT Code FROM AT_CodeMap WHERE Code = OTStartDate AND NotShowFlag = '0' AND TabName = 'NationalHolidayDefine' AND FldName ='HolidayDate')")
            '            If dt.Rows.Count > 0 Then
            '                'Bsp.Utility.ShowMessage(Me, "不能假日連續加班")
            '                'Return False
            '                '20170314-HR不擋僅通知-讓檢核能通過
            '                ErrMsg += "不能假日連續加班" & vbNewLine
            '            End If
            '        End Using
            '    ElseIf (objOV42.CheckHolidayOrNot(b.ToString("yyyy/MM/dd"))) Then
            '        Using dt As DataTable = objOV42.QueryData("*", "OverTimeAdvance", " AND OTStartDate= " + Bsp.Utility.Quote(b.ToString("yyyy/MM/dd")) + " AND OTCompID=" + Bsp.Utility.Quote(hidCompID.Value.ToString()) + " AND OTEmpID =" + Bsp.Utility.Quote(txtOTEmpID.Text.Trim) + " AND OTStatus in ('1','2','3') AND OTStartDate <> ( SELECT Code FROM AT_CodeMap WHERE Code = OTStartDate AND NotShowFlag = '0' AND TabName = 'NationalHolidayDefine' AND FldName ='HolidayDate')")
            '            If dt.Rows.Count > 0 Then
            '                'Bsp.Utility.ShowMessage(Me, "不能假日連續加班")
            '                'Return False
            '                '20170314-HR不擋僅通知-讓檢核能通過
            '                ErrMsg += "不能假日連續加班" & vbNewLine
            '            End If
            '        End Using
            '    End If
            'End If
            ''End If

            '檢查加班時數(含已核准)申報時數是否超過上限
            Dim message As String = ""
            If (Not checkOverTimeIsOver(message)) Then
                '20170314-HR不擋僅通知-讓檢核能通過
                'Bsp.Utility.ShowMessage(Me, message)
                ErrMsg += message
                If _dtPara.Rows(0)("DayLimitFlag").ToString() = "1" Then
                    'Return False
                End If
            End If

            '檢查每個月的上限
            '2017/03/16如果是跨月單，就拆成兩張單日單分別檢核
            If Month(Convert.ToDateTime(ucOTStartDate.DateText)) = Month(Convert.ToDateTime(ucOTEndDate.DateText)) Then     '無跨月
                If Not objOV42.checkMonthTime("OverTimeDeclaration", hidCompID.Value.ToString(), txtOTEmpID.Text, ucOTStartDate.DateText, ucOTEndDate.DateText, Convert.ToDouble(_dtPara.Rows(0)("MonthLimitHour").ToString()), cntTotal, Convert.ToDouble(txtMealTime.Text.Trim), _
             cntStart, cntEnd, "") Then
                    '20170314-HR不擋僅通知-讓檢核能通過
                    'Bsp.Utility.ShowMessage(Me, "每月上限加班申報時數為" + _dtPara.Rows(0)("MonthLimitHour") + "小時")
                    'Return False
                    ErrMsg += "每月上限加班申報時數為" + _dtPara(0)("MonthLimitHour") + "小時" & vbNewLine
                End If
            Else    '有跨月
                '計算跨日時數
                getCntStartAndCntEnd(cntStart, cntEnd)

                Dim mealOver As String = ""
                If MealFlag.Checked = True Then
                    mealOver = objOV42.MealJudge(cntStart, Convert.ToDouble(txtMealTime.Text))
                Else
                    'Result = StartDayMealFlag + "," + StartDayMealTime + "," + EndDayMealFlag + "," + EndDayMealTime
                    mealOver = "0,0,0,0"
                End If

                '先算加班開始日期部分
                If Not objOV42.checkMonthTime("OverTimeDeclaration", hidCompID.Value.ToString(), txtOTEmpID.Text, ucOTStartDate.DateText, ucOTStartDate.DateText, Convert.ToDouble(_dtPara.Rows(0)("MonthLimitHour").ToString()), cntStart, mealOver.Split(","c)(1), _
                        cntStart, 0, "") Then
                    '20170314-HR不擋僅通知-讓檢核能通過
                    'Bsp.Utility.ShowMessage(Me, "每月上限加班申報時數為" + _dtPara.Rows(0)("MonthLimitHour") + "小時")
                    'Return False
                    ErrMsg += Month(Convert.ToDateTime(ucOTEndDate.DateText)) + "月之每月上限加班申報時數為" + _dtPara(0)("MonthLimitHour") + "小時" & vbNewLine
                End If

                '再算加班結束日期部分
                If Not objOV42.checkMonthTime("OverTimeDeclaration", hidCompID.Value.ToString(), txtOTEmpID.Text, ucOTEndDate.DateText, ucOTEndDate.DateText, Convert.ToDouble(_dtPara.Rows(0)("MonthLimitHour").ToString()), cntEnd, mealOver.Split(","c)(3), _
                        cntEnd, 0, "") Then
                    '20170314-HR不擋僅通知-讓檢核能通過
                    'Bsp.Utility.ShowMessage(Me, "每月上限加班申報時數為" + _dtPara.Rows(0)("MonthLimitHour") + "小時")
                    'Return False
                    ErrMsg += Month(Convert.ToDateTime(ucOTStartDate.DateText)) + "月之每月上限加班申報時數為" + _dtPara(0)("MonthLimitHour") + "小時" & vbNewLine
                End If
            End If

            Dim cnt As Integer = 0
            '檢查連續上班日
            If _dtPara.Rows(0)("OTMustCheck").ToString() = "0" Then
                Dim OTLimitDay As Integer = Convert.ToInt32(_dtPara.Rows(0)("OTLimitDay").ToString())
                sb.Clear()
                sb.Append("SELECT Convert(varchar,C.SysDate,111) as SysDate,ISNULL(O.OTStartDate,'') AS OTStartDate,C.Week,C.HolidayOrNot FROM (")
                sb.Append(" SELECT DISTINCT OTStartDate FROM OverTimeAdvance WHERE  OTCompID='" + hidCompID.Value.Trim + "' AND OTEmpID='" + txtOTEmpID.Text + "' AND OTStatus in ('2','3') AND OTStartDate>= DATEADD(DAY,-" + (OTLimitDay - 1).ToString() + ",'" + ucOTStartDate.DateText + "') AND  OTStartDate<= DATEADD(DAY," + (OTLimitDay - 1).ToString() + ",'" + ucOTStartDate.DateText + "')")
                sb.Append(" AND OTTxnID NOT IN")
                sb.Append(" (SELECT OTFromAdvanceTxnId FROM OverTimeDeclaration WHERE OTCompID='" + hidCompID.Value.Trim + "' AND OTEmpID='" + txtOTEmpID.Text + "' AND OTStatus in ('2','3') AND OTFromAdvanceTxnId<>'')")
                sb.Append(" UNION")
                sb.Append(" SELECT DISTINCT OTStartDate FROM OverTimeDeclaration WHERE  OTCompID='" + hidCompID.Value.Trim + "' AND OTEmpID='" + txtOTEmpID.Text + "' AND OTStatus in ('2','3') AND OTStartDate>= DATEADD(DAY,-" + (OTLimitDay - 1).ToString() + ",'" + ucOTStartDate.DateText + "') AND  OTStartDate<= DATEADD(DAY," + (OTLimitDay - 1).ToString() + ",'" + ucOTStartDate.DateText + "')) O")
                sb.Append(" FULL OUTER JOIN(")
                sb.Append(" SELECT * FROM " + _eHRMSDB_ITRD + ".[dbo].[Calendar] WHERE  CompID='" + hidCompID.Value.Trim + "' AND SysDate>= DATEADD(DAY,-" + (OTLimitDay - 1).ToString() + ",'" + ucOTStartDate.DateText + "') AND  SysDate<= DATEADD(DAY," + (OTLimitDay - 1).ToString() + ",'" + ucOTStartDate.DateText + "')) C ON O.OTStartDate=C.SysDate")
                sb.Append(" ORDER BY C.SysDate ASC")
                Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, sb.ToString(), "AattendantDB").Tables(0)
                    For j As Integer = 0 To dt.Rows.Count - 1
                        If dt.Rows(j)("SysDate").ToString() = ucOTStartDate.DateText OrElse dt.Rows(j)("SysDate").ToString() = ucOTEndDate.DateText Then    '本單
                            cnt += 1
                        Else
                            If dt.Rows(j)("HolidayOrNot").ToString() = "0" Then
                                cnt += 1
                            Else
                                If Not String.IsNullOrEmpty(dt.Rows(j)("OTStartDate").ToString()) Then
                                    cnt += 1
                                Else
                                    cnt = 0
                                End If
                            End If
                        End If
                        If cnt >= OTLimitDay Then
                            '20170314-HR不擋僅通知-讓檢核能通過
                            'Bsp.Utility.ShowMessage(Me, "不得連續上班超過" + OTLimitDay.ToString() + "天")
                            ErrMsg += "不得連續上班超過" + OTLimitDay.ToString() + "天" & vbNewLine
                            If _dtPara.Rows(0)("OTLimitFlag").ToString() = "1" Then
                                'Return False
                            End If
                            Exit For
                        End If
                    Next
                End Using
            End If

            '畫面上的時間
            Dim starttime As Integer = Convert.ToInt32(ucOTStartTime.ucDefaultSelectedHH + ucOTStartTime.ucDefaultSelectedMM)
            Dim endtime As Integer = Convert.ToInt32(ucOTEndTime.ucDefaultSelectedHH + ucOTEndTime.ucDefaultSelectedMM)

            '檢核時間重疊(OverTime_BK)
            Using dt As DataTable = objOV42.CheckOverTimeBK(ucOTStartDate.DateText, ucOTEndDate.DateText, txtOTEmpID.Text.Trim)
                If (dt.Rows.Count > 0) Then

                    For i = 0 To (dt.Rows.Count - 1)    '0500~0700
                        '起迄時間都有重疊
                        If ((dt.Rows(i).Item("BeginTime").ToString() = ucOTStartTime.ucDefaultSelectedHH + ucOTStartTime.ucDefaultSelectedMM) And (dt.Rows(i).Item("EndTime").ToString() = ucOTEndTime.ucDefaultSelectedHH + ucOTEndTime.ucDefaultSelectedMM)) Then
                            Bsp.Utility.ShowMessage(Me, "該時段已存在人力資源-加班系統，請由人力資源-加班進行查詢")
                            Return False
                        End If

                        '開始時間小於資料庫開始時間
                        If (Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) > starttime) Then
                            '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                            If (endtime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And endtime < Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                                Bsp.Utility.ShowMessage(Me, "該時段已存在人力資源-加班系統，請由人力資源-加班進行查詢")
                                Return False
                                '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                            ElseIf (endtime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And endtime = Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                                Bsp.Utility.ShowMessage(Me, "該時段已存在人力資源-加班系統，請由人力資源-加班進行查詢")
                                Return False
                                '結束時間大於資料庫的開始時間，大於資料庫的結束時間
                            ElseIf (endtime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And endtime > Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                                Bsp.Utility.ShowMessage(Me, "該時段已存在人力資源-加班系統，請由人力資源-加班進行查詢")
                                Return False
                            End If
                        End If

                        '開始時間等於資料庫開始時間
                        If (Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) = starttime) Then
                            '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                            If (endtime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And endtime < Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                                Bsp.Utility.ShowMessage(Me, "該時段已存在人力資源-加班系統，請由人力資源-加班進行查詢")
                                Return False
                                '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                            ElseIf (endtime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And endtime = Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                                Bsp.Utility.ShowMessage(Me, "該時段已存在人力資源-加班系統，請由人力資源-加班進行查詢")
                                Return False
                                '結束時間大於資料庫的開始時間，大於資料庫的結束時間
                            ElseIf (endtime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And endtime > Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                                Bsp.Utility.ShowMessage(Me, "該時段已存在人力資源-加班系統，請由人力資源-加班進行查詢")
                                Return False
                            End If
                        End If

                        '開始時間大於資料庫開始時間
                        If (Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) < starttime) Then
                            '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                            If (endtime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And endtime < Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                                Bsp.Utility.ShowMessage(Me, "該時段已存在人力資源-加班系統，請由人力資源-加班進行查詢")
                                Return False
                                '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                            ElseIf (endtime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And endtime = Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                                Bsp.Utility.ShowMessage(Me, "該時段已存在人力資源-加班系統，請由人力資源-加班進行查詢")
                                Return False
                            ElseIf (Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString()) > starttime) Then
                                Bsp.Utility.ShowMessage(Me, "該時段已存在人力資源-加班系統，請由人力資源-加班進行查詢")
                                Return False
                            End If
                        End If
                    Next
                End If
            End Using

            '檢核時間重疊(NaturalDisasterByCity)
            Using dt As DataTable = objOV42.CheckNaturalDisasterByCity(ucOTStartDate.DateText, ucOTEndDate.DateText, _WorkSiteID, hidCompID.Value.ToString())
                If (dt.Rows.Count > 0) Then
                    For i = 0 To (dt.Rows.Count - 1)
                        '起迄時間都有重疊
                        If ((dt.Rows(i).Item("BeginTime").ToString() = ucOTStartTime.ucDefaultSelectedHH + ucOTStartTime.ucDefaultSelectedMM) And (dt.Rows(i).Item("EndTime").ToString() = ucOTEndTime.ucDefaultSelectedHH + ucOTEndTime.ucDefaultSelectedMM)) Then
                            Bsp.Utility.ShowMessage(Me, "留守時段重複")
                            Return False
                        End If
                        '開始時間小於資料庫開始時間
                        If (Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) > starttime) Then
                            '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                            If (endtime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And endtime < Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                                Bsp.Utility.ShowMessage(Me, "留守時段重複")
                                Return False
                                '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                            ElseIf (endtime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And endtime = Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                                Bsp.Utility.ShowMessage(Me, "留守時段重複")
                                Return False
                                '結束時間大於資料庫的開始時間，大於資料庫的結束時間
                            ElseIf (endtime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And endtime > Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                                Bsp.Utility.ShowMessage(Me, "留守時段重複")
                                Return False
                            End If
                        End If
                        '開始時間等於資料庫開始時間
                        If (Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) = starttime) Then
                            '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                            If (endtime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And endtime < Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                                Bsp.Utility.ShowMessage(Me, "留守時段重複")
                                Return False
                                '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                            ElseIf (endtime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And endtime = Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                                Bsp.Utility.ShowMessage(Me, "留守時段重複")
                                Return False
                                '結束時間大於資料庫的開始時間，大於資料庫的結束時間
                            ElseIf (endtime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And endtime > Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                                Bsp.Utility.ShowMessage(Me, "留守時段重複")
                                Return False
                            End If
                        End If
                        '開始時間大於資料庫開始時間
                        If (Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) < starttime) Then
                            '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                            If (endtime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And endtime < Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                                Bsp.Utility.ShowMessage(Me, "留守時段重複")
                                Return False
                                '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                            ElseIf (endtime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And endtime = Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                                Bsp.Utility.ShowMessage(Me, "留守時段重複")
                                Return False
                            ElseIf (Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString()) > starttime) Then
                                Bsp.Utility.ShowMessage(Me, "留守時段重複")
                                Return False
                            End If
                        End If
                    Next
                End If
            End Using

            '檢核時間重疊(NaturalDisasterByCity)
            Using dt As DataTable = objOV42.CheckNaturalDisasterByEmp(ucOTStartDate.DateText, ucOTEndDate.DateText, txtOTEmpID.Text.Trim, hidCompID.Value.ToString())
                If (dt.Rows.Count > 0) Then
                    For i = 0 To (dt.Rows.Count - 1)
                        '起迄時間都有重疊
                        If ((dt.Rows(i).Item("BeginTime").ToString() = ucOTStartTime.ucDefaultSelectedHH + ucOTStartTime.ucDefaultSelectedMM) And (dt.Rows(i).Item("EndTime").ToString() = ucOTEndTime.ucDefaultSelectedHH + ucOTEndTime.ucDefaultSelectedMM)) Then
                            Bsp.Utility.ShowMessage(Me, "留守時段重複")
                            Return False
                        End If
                        '開始時間小於資料庫開始時間
                        If (Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) > starttime) Then
                            '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                            If (endtime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And endtime < Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                                Bsp.Utility.ShowMessage(Me, "留守時段重複")
                                Return False
                                '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                            ElseIf (endtime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And endtime = Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                                Bsp.Utility.ShowMessage(Me, "留守時段重複")
                                Return False
                                '結束時間大於資料庫的開始時間，大於資料庫的結束時間
                            ElseIf (endtime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And endtime > Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                                Bsp.Utility.ShowMessage(Me, "留守時段重複")
                                Return False
                            End If
                        End If
                        '開始時間等於資料庫開始時間
                        If (Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) = starttime) Then
                            '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                            If (endtime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And endtime < Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                                Bsp.Utility.ShowMessage(Me, "留守時段重複")
                                Return False
                                '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                            ElseIf (endtime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And endtime = Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                                Bsp.Utility.ShowMessage(Me, "留守時段重複")
                                Return False
                                '結束時間大於資料庫的開始時間，大於資料庫的結束時間
                            ElseIf (endtime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And endtime > Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                                Bsp.Utility.ShowMessage(Me, "留守時段重複")
                                Return False
                            End If
                        End If
                        '開始時間大於資料庫開始時間
                        If (Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) < starttime) Then
                            '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                            If (endtime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And endtime < Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                                Bsp.Utility.ShowMessage(Me, "留守時段重複")
                                Return False
                                '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                            ElseIf (endtime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And endtime = Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                                Bsp.Utility.ShowMessage(Me, "留守時段重複")
                                Return False
                            ElseIf (Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString()) > starttime) Then
                                Bsp.Utility.ShowMessage(Me, "留守時段重複")
                                Return False
                            End If
                        End If
                    Next
                End If
            End Using

            '檢核時間重疊(事後申報)
            Using dt As DataTable = objOV42.CheckOverTimeDeclaration(ucOTStartDate.DateText, ucOTEndDate.DateText, txtOTEmpID.Text.Trim, hidCompID.Value.ToString())
                If (dt.Rows.Count > 0) Then
                    If ucOTStartDate.DateText = ucOTEndDate.DateText Then
                        For i = 0 To (dt.Rows.Count - 1)
                            '起迄時間都有重疊  
                            If (ucOTStartDate.DateText = ucOTEndDate.DateText) Then
                                If ((dt.Rows(i).Item("OTStartTime").ToString() = ucOTStartTime.ucDefaultSelectedHH + ucOTStartTime.ucDefaultSelectedMM) And (dt.Rows(i).Item("OTEndTime").ToString() = ucOTEndTime.ucDefaultSelectedHH + ucOTEndTime.ucDefaultSelectedMM)) Then
                                    Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                    Return False
                                End If
                            End If
                            '開始時間小於資料庫開始時間
                            If (Convert.ToInt32(dt.Rows(i).Item("OTStartTime").ToString()) > starttime) Then
                                '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                                If (endtime > Convert.ToInt32(dt.Rows(i).Item("OTStartTime").ToString()) And endtime < Convert.ToInt32(dt.Rows(i).Item("OTEndTime").ToString())) Then
                                    Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                    Return False
                                    '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                                ElseIf (endtime > Convert.ToInt32(dt.Rows(i).Item("OTStartTime").ToString()) And endtime = Convert.ToInt32(dt.Rows(i).Item("OTEndTime").ToString())) Then
                                    Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                    Return False
                                    '結束時間大於資料庫的開始時間，大於資料庫的結束時間
                                ElseIf (endtime > Convert.ToInt32(dt.Rows(i).Item("OTStartTime").ToString()) And endtime > Convert.ToInt32(dt.Rows(i).Item("OTEndTime").ToString())) Then
                                    Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                    Return False
                                End If
                            End If
                            '開始時間等於資料庫開始時間
                            If (Convert.ToInt32(dt.Rows(i).Item("OTStartTime").ToString()) = starttime) Then
                                '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                                If (endtime > Convert.ToInt32(dt.Rows(i).Item("OTStartTime").ToString()) And endtime < Convert.ToInt32(dt.Rows(i).Item("OTEndTime").ToString())) Then
                                    Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                    Return False
                                    '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                                ElseIf (endtime > Convert.ToInt32(dt.Rows(i).Item("OTStartTime").ToString()) And endtime = Convert.ToInt32(dt.Rows(i).Item("OTEndTime").ToString())) Then
                                    Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                    Return False
                                    '結束時間大於資料庫的開始時間，大於資料庫的結束時間
                                ElseIf (endtime > Convert.ToInt32(dt.Rows(i).Item("OTStartTime").ToString()) And endtime > Convert.ToInt32(dt.Rows(i).Item("OTEndTime").ToString())) Then
                                    Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                    Return False
                                End If
                            End If
                            '開始時間大於資料庫開始時間
                            If (Convert.ToInt32(dt.Rows(i).Item("OTStartTime").ToString()) < starttime) Then
                                '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                                If (endtime > Convert.ToInt32(dt.Rows(i).Item("OTStartTime").ToString()) And endtime < Convert.ToInt32(dt.Rows(i).Item("OTEndTime").ToString())) Then
                                    Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                    Return False
                                    '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                                ElseIf (endtime > Convert.ToInt32(dt.Rows(i).Item("OTStartTime").ToString()) And endtime = Convert.ToInt32(dt.Rows(i).Item("OTEndTime").ToString())) Then
                                    Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                    Return False
                                ElseIf (Convert.ToInt32(dt.Rows(i).Item("OTEndTime").ToString()) > starttime) Then
                                    Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                    Return False
                                End If
                            End If
                        Next
                    Else
                        For i = 0 To dt.Rows.Count - 1
                            If dt.Rows(i)("OTStartDate").ToString() = ucOTStartDate.DateText Then
                                '起迄日重疊
                                endtime = 2359
                                starttime = Convert.ToInt32(ucOTStartTime.ucDefaultSelectedHH + ucOTStartTime.ucDefaultSelectedMM)
                                Dim strTxnID As String = objOV42.QueryColumn("OTEndTime", "OverTimeDeclaration", " AND OTTxnID='" + dt.Rows(i)("OTTxnID") + "' AND OTSeqNo='2'")
                                If (dt.Rows(i)("OTStartTime").ToString() = ucOTStartTime.ucDefaultSelectedHH + ucOTStartTime.ucDefaultSelectedMM) AndAlso strTxnID = ucOTEndTime.ucDefaultSelectedHH + ucOTEndTime.ucDefaultSelectedMM Then
                                    Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                    Return False
                                End If
                                '開始時間小於資料庫開始時間
                                If Convert.ToInt32(dt.Rows(i)("OTStartTime").ToString()) > starttime Then
                                    '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                                    If endtime > Convert.ToInt32(dt.Rows(i)("OTStartTime").ToString()) AndAlso endtime < Convert.ToInt32(dt.Rows(i)("OTEndTime").ToString()) Then
                                        Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                        Return False
                                        '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                                    ElseIf endtime > Convert.ToInt32(dt.Rows(i)("OTStartTime").ToString()) AndAlso endtime = Convert.ToInt32(dt.Rows(i)("OTEndTime").ToString()) Then
                                        Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                        Return False
                                        '結束時間大於資料庫的開始時間，大於資料庫的結束時間
                                    ElseIf endtime > Convert.ToInt32(dt.Rows(i)("OTStartTime").ToString()) AndAlso endtime > Convert.ToInt32(dt.Rows(i)("OTEndTime").ToString()) Then
                                        Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                        Return False
                                    End If
                                End If
                                '開始時間等於資料庫開始時間
                                If Convert.ToInt32(dt.Rows(i)("OTStartTime").ToString()) = starttime Then
                                    '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                                    If endtime > Convert.ToInt32(dt.Rows(i)("OTStartTime").ToString()) AndAlso endtime < Convert.ToInt32(dt.Rows(i)("OTEndTime").ToString()) Then
                                        Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                        Return False
                                        '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                                    ElseIf endtime > Convert.ToInt32(dt.Rows(i)("OTStartTime").ToString()) AndAlso endtime = Convert.ToInt32(dt.Rows(i)("OTEndTime").ToString()) Then
                                        Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                        Return False
                                        '結束時間大於資料庫的開始時間，大於資料庫的結束時間
                                    ElseIf endtime > Convert.ToInt32(dt.Rows(i)("OTStartTime").ToString()) AndAlso endtime > Convert.ToInt32(dt.Rows(i)("OTEndTime").ToString()) Then
                                        Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                        Return False
                                    End If
                                End If
                                '開始時間大於資料庫開始時間
                                If Convert.ToInt32(dt.Rows(i)("OTStartTime").ToString()) < starttime Then
                                    '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                                    If endtime > Convert.ToInt32(dt.Rows(i)("OTStartTime").ToString()) AndAlso endtime < Convert.ToInt32(dt.Rows(i)("OTEndTime").ToString()) Then
                                        Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                        Return False
                                        '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                                    ElseIf endtime > Convert.ToInt32(dt.Rows(i)("OTStartTime").ToString()) AndAlso endtime = Convert.ToInt32(dt.Rows(i)("OTEndTime").ToString()) Then
                                        Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                        Return False
                                    ElseIf Convert.ToInt32(dt.Rows(i)("OTEndTime").ToString()) > starttime Then
                                        Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                        Return False
                                    End If
                                End If
                            Else
                                '迄日
                                starttime = 0
                                endtime = Convert.ToInt32(ucOTEndTime.ucDefaultSelectedHH + ucOTEndTime.ucDefaultSelectedMM)
                                '開始時間小於資料庫開始時間
                                If Convert.ToInt32(dt.Rows(i)("OTStartTime").ToString()) > starttime Then
                                    '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                                    If endtime > Convert.ToInt32(dt.Rows(i)("OTStartTime").ToString()) AndAlso endtime < Convert.ToInt32(dt.Rows(i)("OTEndTime").ToString()) Then
                                        Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                        Return False
                                        '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                                    ElseIf endtime > Convert.ToInt32(dt.Rows(i)("OTStartTime").ToString()) AndAlso endtime = Convert.ToInt32(dt.Rows(i)("OTEndTime").ToString()) Then
                                        Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                        Return False
                                        '結束時間大於資料庫的開始時間，大於資料庫的結束時間
                                    ElseIf endtime > Convert.ToInt32(dt.Rows(i)("OTStartTime").ToString()) AndAlso endtime > Convert.ToInt32(dt.Rows(i)("OTEndTime").ToString()) Then
                                        Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                        Return False
                                    End If
                                End If
                                '開始時間等於資料庫開始時間
                                If Convert.ToInt32(dt.Rows(i)("OTStartTime").ToString()) = starttime Then
                                    '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                                    If endtime > Convert.ToInt32(dt.Rows(i)("OTStartTime").ToString()) AndAlso endtime < Convert.ToInt32(dt.Rows(i)("OTEndTime").ToString()) Then
                                        Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                        Return False
                                        '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                                    ElseIf endtime > Convert.ToInt32(dt.Rows(i)("OTStartTime").ToString()) AndAlso endtime = Convert.ToInt32(dt.Rows(i)("OTEndTime").ToString()) Then
                                        Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                        Return False
                                        '結束時間大於資料庫的開始時間，大於資料庫的結束時間
                                    ElseIf endtime > Convert.ToInt32(dt.Rows(i)("OTStartTime").ToString()) AndAlso endtime > Convert.ToInt32(dt.Rows(i)("OTEndTime").ToString()) Then
                                        Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                        Return False
                                    End If
                                End If
                                '開始時間大於資料庫開始時間
                                If Convert.ToInt32(dt.Rows(i)("OTStartTime").ToString()) < starttime Then
                                    '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                                    If endtime > Convert.ToInt32(dt.Rows(i)("OTStartTime").ToString()) AndAlso endtime < Convert.ToInt32(dt.Rows(i)("OTEndTime").ToString()) Then
                                        Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                        Return False
                                        '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                                    ElseIf endtime > Convert.ToInt32(dt.Rows(i)("OTStartTime").ToString()) AndAlso endtime = Convert.ToInt32(dt.Rows(i)("OTEndTime").ToString()) Then
                                        Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                        Return False
                                    ElseIf Convert.ToInt32(dt.Rows(i)("OTEndTime").ToString()) > starttime Then
                                        Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                        Return False
                                    End If
                                End If
                            End If
                        Next
                    End If
                End If
            End Using

            '檢核時間重疊(預先申請)
            Using dt As DataTable = objOV42.CheckOverTimeAdvance(ucOTStartDate.DateText, ucOTEndDate.DateText, txtOTEmpID.Text.Trim, hidCompID.Value.ToString())
                If (dt.Rows.Count > 0) Then
                    If ucOTStartDate.DateText = ucOTEndDate.DateText Then
                        For i = 0 To (dt.Rows.Count - 1)
                            '起迄時間都有重疊 
                            If (ucOTStartDate.DateText = ucOTEndDate.DateText) Then
                                If ((dt.Rows(i).Item("OTStartTime").ToString() = ucOTStartTime.ucDefaultSelectedHH + ucOTStartTime.ucDefaultSelectedMM) And (dt.Rows(i).Item("OTEndTime").ToString() = ucOTEndTime.ucDefaultSelectedHH + ucOTEndTime.ucDefaultSelectedMM)) Then
                                    Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                    Return False
                                End If
                            Else
                                Dim strTxnID As String = objOV42.QueryColumn("OTEndTime", "OverTimeAdvance", " AND OTTxnID='" + dt.Rows(i).Item("OTTxnID").ToString() + "' AND OTSeqNo='2'")
                                If ((dt.Rows(i).Item("OTStartTime").ToString() = ucOTStartTime.ucDefaultSelectedHH + ucOTStartTime.ucDefaultSelectedMM) And strTxnID = ucOTEndTime.ucDefaultSelectedHH + ucOTEndTime.ucDefaultSelectedMM) Then
                                    Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                    Return False
                                End If
                            End If
                            '開始時間小於資料庫開始時間
                            If (Convert.ToInt32(dt.Rows(i).Item("OTStartTime").ToString()) > starttime) Then
                                '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                                If (endtime > Convert.ToInt32(dt.Rows(i).Item("OTStartTime").ToString()) And endtime < Convert.ToInt32(dt.Rows(i).Item("OTEndTime").ToString())) Then
                                    Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                    Return False
                                    '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                                ElseIf (endtime > Convert.ToInt32(dt.Rows(i).Item("OTStartTime").ToString()) And endtime = Convert.ToInt32(dt.Rows(i).Item("OTEndTime").ToString())) Then
                                    Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                    Return False
                                    '結束時間大於資料庫的開始時間，大於資料庫的結束時間
                                ElseIf (endtime > Convert.ToInt32(dt.Rows(i).Item("OTStartTime").ToString()) And endtime > Convert.ToInt32(dt.Rows(i).Item("OTEndTime").ToString())) Then
                                    Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                    Return False
                                End If
                            End If
                            '開始時間等於資料庫開始時間
                            If (Convert.ToInt32(dt.Rows(i).Item("OTStartTime").ToString()) = starttime) Then
                                '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                                If (endtime > Convert.ToInt32(dt.Rows(i).Item("OTStartTime").ToString()) And endtime < Convert.ToInt32(dt.Rows(i).Item("OTEndTime").ToString())) Then
                                    Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                    Return False
                                    '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                                ElseIf (endtime > Convert.ToInt32(dt.Rows(i).Item("OTStartTime").ToString()) And endtime = Convert.ToInt32(dt.Rows(i).Item("OTEndTime").ToString())) Then
                                    Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                    Return False
                                    '結束時間大於資料庫的開始時間，大於資料庫的結束時間
                                ElseIf (endtime > Convert.ToInt32(dt.Rows(i).Item("OTStartTime").ToString()) And endtime > Convert.ToInt32(dt.Rows(i).Item("OTEndTime").ToString())) Then
                                    Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                    Return False
                                End If
                            End If
                            '開始時間大於資料庫開始時間
                            If (Convert.ToInt32(dt.Rows(i).Item("OTStartTime").ToString()) < starttime) Then
                                '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                                If (endtime > Convert.ToInt32(dt.Rows(i).Item("OTStartTime").ToString()) And endtime < Convert.ToInt32(dt.Rows(i).Item("OTEndTime").ToString())) Then
                                    Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                    Return False
                                    '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                                ElseIf (endtime > Convert.ToInt32(dt.Rows(i).Item("OTStartTime").ToString()) And endtime = Convert.ToInt32(dt.Rows(i).Item("OTEndTime").ToString())) Then
                                    Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                    Return False
                                ElseIf (Convert.ToInt32(dt.Rows(i).Item("OTEndTime").ToString()) > starttime) Then
                                    Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                    Return False
                                End If
                            End If
                        Next
                    Else
                        For i = 0 To dt.Rows.Count - 1
                            If dt.Rows(i)("OTStartDate").ToString() = ucOTStartDate.DateText Then
                                '起迄日重疊
                                endtime = 2359
                                starttime = Convert.ToInt32(ucOTStartTime.ucDefaultSelectedHH + ucOTStartTime.ucDefaultSelectedMM)
                                Dim strTxnID As String = objOV42.QueryColumn("OTEndTime", "OverTimeDeclaration", " AND OTTxnID='" + dt.Rows(i)("OTTxnID") + "' AND OTSeqNo='2'")
                                If (dt.Rows(i)("OTStartTime").ToString() = ucOTStartTime.ucDefaultSelectedHH + ucOTStartTime.ucDefaultSelectedMM) AndAlso strTxnID = ucOTEndTime.ucDefaultSelectedHH + ucOTEndTime.ucDefaultSelectedMM Then
                                    Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                    Return False
                                End If
                                '開始時間小於資料庫開始時間
                                If Convert.ToInt32(dt.Rows(i)("OTStartTime").ToString()) > starttime Then
                                    '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                                    If endtime > Convert.ToInt32(dt.Rows(i)("OTStartTime").ToString()) AndAlso endtime < Convert.ToInt32(dt.Rows(i)("OTEndTime").ToString()) Then
                                        Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                        Return False
                                        '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                                    ElseIf endtime > Convert.ToInt32(dt.Rows(i)("OTStartTime").ToString()) AndAlso endtime = Convert.ToInt32(dt.Rows(i)("OTEndTime").ToString()) Then
                                        Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                        Return False
                                        '結束時間大於資料庫的開始時間，大於資料庫的結束時間
                                    ElseIf endtime > Convert.ToInt32(dt.Rows(i)("OTStartTime").ToString()) AndAlso endtime > Convert.ToInt32(dt.Rows(i)("OTEndTime").ToString()) Then
                                        Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                        Return False
                                    End If
                                End If
                                '開始時間等於資料庫開始時間
                                If Convert.ToInt32(dt.Rows(i)("OTStartTime").ToString()) = starttime Then
                                    '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                                    If endtime > Convert.ToInt32(dt.Rows(i)("OTStartTime").ToString()) AndAlso endtime < Convert.ToInt32(dt.Rows(i)("OTEndTime").ToString()) Then
                                        Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                        Return False
                                        '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                                    ElseIf endtime > Convert.ToInt32(dt.Rows(i)("OTStartTime").ToString()) AndAlso endtime = Convert.ToInt32(dt.Rows(i)("OTEndTime").ToString()) Then
                                        Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                        Return False
                                        '結束時間大於資料庫的開始時間，大於資料庫的結束時間
                                    ElseIf endtime > Convert.ToInt32(dt.Rows(i)("OTStartTime").ToString()) AndAlso endtime > Convert.ToInt32(dt.Rows(i)("OTEndTime").ToString()) Then
                                        Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                        Return False
                                    End If
                                End If
                                '開始時間大於資料庫開始時間
                                If Convert.ToInt32(dt.Rows(i)("OTStartTime").ToString()) < starttime Then
                                    '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                                    If endtime > Convert.ToInt32(dt.Rows(i)("OTStartTime").ToString()) AndAlso endtime < Convert.ToInt32(dt.Rows(i)("OTEndTime").ToString()) Then
                                        Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                        Return False
                                        '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                                    ElseIf endtime > Convert.ToInt32(dt.Rows(i)("OTStartTime").ToString()) AndAlso endtime = Convert.ToInt32(dt.Rows(i)("OTEndTime").ToString()) Then
                                        Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                        Return False
                                    ElseIf Convert.ToInt32(dt.Rows(i)("OTEndTime").ToString()) > starttime Then
                                        Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                        Return False
                                    End If
                                End If
                            Else
                                '迄日
                                starttime = 0
                                endtime = Convert.ToInt32(ucOTEndTime.ucDefaultSelectedHH + ucOTEndTime.ucDefaultSelectedMM)
                                '開始時間小於資料庫開始時間
                                If Convert.ToInt32(dt.Rows(i)("OTStartTime").ToString()) > starttime Then
                                    '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                                    If endtime > Convert.ToInt32(dt.Rows(i)("OTStartTime").ToString()) AndAlso endtime < Convert.ToInt32(dt.Rows(i)("OTEndTime").ToString()) Then
                                        Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                        Return False
                                        '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                                    ElseIf endtime > Convert.ToInt32(dt.Rows(i)("OTStartTime").ToString()) AndAlso endtime = Convert.ToInt32(dt.Rows(i)("OTEndTime").ToString()) Then
                                        Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                        Return False
                                        '結束時間大於資料庫的開始時間，大於資料庫的結束時間
                                    ElseIf endtime > Convert.ToInt32(dt.Rows(i)("OTStartTime").ToString()) AndAlso endtime > Convert.ToInt32(dt.Rows(i)("OTEndTime").ToString()) Then
                                        Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                        Return False
                                    End If
                                End If
                                '開始時間等於資料庫開始時間
                                If Convert.ToInt32(dt.Rows(i)("OTStartTime").ToString()) = starttime Then
                                    '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                                    If endtime > Convert.ToInt32(dt.Rows(i)("OTStartTime").ToString()) AndAlso endtime < Convert.ToInt32(dt.Rows(i)("OTEndTime").ToString()) Then
                                        Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                        Return False
                                        '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                                    ElseIf endtime > Convert.ToInt32(dt.Rows(i)("OTStartTime").ToString()) AndAlso endtime = Convert.ToInt32(dt.Rows(i)("OTEndTime").ToString()) Then
                                        Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                        Return False
                                        '結束時間大於資料庫的開始時間，大於資料庫的結束時間
                                    ElseIf endtime > Convert.ToInt32(dt.Rows(i)("OTStartTime").ToString()) AndAlso endtime > Convert.ToInt32(dt.Rows(i)("OTEndTime").ToString()) Then
                                        Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                        Return False
                                    End If
                                End If
                                '開始時間大於資料庫開始時間
                                If Convert.ToInt32(dt.Rows(i)("OTStartTime").ToString()) < starttime Then
                                    '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                                    If endtime > Convert.ToInt32(dt.Rows(i)("OTStartTime").ToString()) AndAlso endtime < Convert.ToInt32(dt.Rows(i)("OTEndTime").ToString()) Then
                                        Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                        Return False
                                        '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                                    ElseIf endtime > Convert.ToInt32(dt.Rows(i)("OTStartTime").ToString()) AndAlso endtime = Convert.ToInt32(dt.Rows(i)("OTEndTime").ToString()) Then
                                        Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                        Return False
                                    ElseIf Convert.ToInt32(dt.Rows(i)("OTEndTime").ToString()) > starttime Then
                                        Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                                        Return False
                                    End If
                                End If
                            End If
                        Next
                    End If
                End If
            End Using
        End If

        If ErrMsg <> "" Then
            Bsp.Utility.ShowMessage(Me, ErrMsg)
        End If

        Return True
    End Function
#End Region

#Region "時段相關"

    ''' <summary>
    ''' 計算時數
    ''' </summary>
    ''' <param name="OverTimeSumObjectList"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    'Function getSumOTTime(ByVal OverTimeSumObjectList As ArrayList) As String
    '    Dim sumOTTime As Double = 0
    '    If OverTimeSumObjectList.Count > 0 Then
    '        For Each OverTimeSumObject As OV_1 In OverTimeSumObjectList
    '            Dim lastDate As String = OverTimeSumObject.getLastDate(OverTimeSumObject.OvertimeDateB.Split("/")(0), OverTimeSumObject.OvertimeDateB.Split("/")(1))
    '            Dim lastDateTime As DateTime = DateTime.ParseExact(lastDate, "yyyy/MM/dd", CultureInfo.InvariantCulture)
    '            Dim dtimeE As DateTime = DateTime.ParseExact(OverTimeSumObject.OvertimeDateE, "yyyy/MM/dd", CultureInfo.InvariantCulture)
    '            If lastDateTime >= dtimeE Then '代表沒跨月
    '                Dim OTString = OverTimeSumObject.getOverTime(OverTimeSumObject.OvertimeDateB, OverTimeSumObject.OvertimeDateE, OverTimeSumObject.OTStartTime, OverTimeSumObject.OTEndTime, "H")
    '                sumOTTime = sumOTTime + Convert.ToDouble(OTString)
    '            Else '代表跨月
    '                Dim OTString = OverTimeSumObject.getOverTimeThisMonth(OverTimeSumObject.OvertimeDateB, OverTimeSumObject.OTStartTime, "H")
    '                sumOTTime = sumOTTime + Convert.ToDouble(OTString)
    '            End If
    '        Next
    '    End If
    '    Return sumOTTime
    'End Function

    ''' <summary>
    ''' 組含本次累計時數的Table
    ''' </summary>
    ''' <param name="StartDate"></param>
    ''' <param name="EndDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function subTableGenByArr(ByVal StartDate As ArrayList, ByVal EndDate As ArrayList) As String
        Dim htmlTable As New StringBuilder
        If StartDate.Count > 0 Then
            htmlTable.AppendLine("含本次累計時數如下: <br />")
            htmlTable.AppendLine("<table border=""1"" cellpadding=""2"" cellspacing=""0"" algin=""center"" >")
            htmlTable.AppendLine("<tr>")
            htmlTable.AppendLine("<th rowspan = '2'>日期</th>")
            htmlTable.AppendLine("<th colspan = '3'>小時</th>")
            htmlTable.AppendLine("</tr>")
            htmlTable.AppendLine("<tr>")
            htmlTable.AppendLine("<th>時段(一)</th>")
            htmlTable.AppendLine("<th>時段(二)</th>")
            htmlTable.AppendLine("<th>時段(三)</th>")
            htmlTable.AppendLine("</tr>")
            htmlTable.AppendLine("<tr>")
            htmlTable.AppendLine("<td align='center'>" + StartDate(0).ToString() + "</td>")
            htmlTable.AppendLine("<td align='center'>" + StartDate(1).ToString() + "</td>")
            htmlTable.AppendLine("<td align='center'>" + StartDate(2).ToString() + "</td>")
            htmlTable.AppendLine("<td align='center'>" + StartDate(3).ToString() + "</td>")
            htmlTable.AppendLine("</tr>")
            If EndDate.Count > 0 Then
                htmlTable.AppendLine("<tr>")
                htmlTable.AppendLine("<td align='center'>" + EndDate(0).ToString() + "</td>")
                htmlTable.AppendLine("<td align='center'>" + EndDate(1).ToString() + "</td>")
                htmlTable.AppendLine("<td align='center'>" + EndDate(2).ToString() + "</td>")
                htmlTable.AppendLine("<td align='center'>" + EndDate(3).ToString() + "</td>")
                htmlTable.AppendLine("</tr>")
            End If
            htmlTable.AppendLine("</table>")
        End If
        Return htmlTable.ToString()
    End Function

    ''' <summary>
    ''' 開始日期
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ValidStartDate_Click(sender As Object, e As System.EventArgs) Handles ValidStartDate.Click
        Dim objOV42 As New OV4_2
        Dim objOVBC As New OVBusinessCommon

        initalData()
        If ucOTStartDate.DateText <> "" Then
            ucOTEndDate.DateText = ucOTStartDate.DateText
            If ucOTStartDate.DateText <> "" And ucOTEndDate.DateText <> "" Then
                '加班申報範圍
                Dim totalBefore As TimeSpan = (Date.Now.Date).Subtract(Convert.ToDateTime(ucOTStartDate.DateText))
                If Date.Now.Date > Convert.ToDateTime(ucOTStartDate.DateText) Then
                    If (Convert.ToInt32(totalBefore.Days)) > (Convert.ToInt32(_dtPara.Rows(0).Item("DeclarationBegin").ToString())) Then
                        Bsp.Utility.ShowMessage(Me, "加班申報不可早於前" + _dtPara.Rows(0).Item("DeclarationBegin").ToString() + "日")
                        ucOTStartDate.DateText = Date.Now.ToString("yyyy/MM/dd")
                        ucOTEndDate.DateText = ucOTStartDate.DateText
                        Return
                    End If
                Else
                    Bsp.Utility.ShowMessage(Me, "加班日期不可以選擇未來日期")
                    ucOTStartDate.DateText = Date.Now.ToString("yyyy/MM/dd")
                    ucOTEndDate.DateText = ucOTStartDate.DateText
                    Return
                End If
                If txtOTEmpID.Text <> "" Then
                    '檢查到職日以前不可以加
                    If (Convert.ToDateTime(ucOTStartDate.DateText) < Convert.ToDateTime(_EmpDate)) Then
                        Bsp.Utility.ShowMessage(Me, "到職日(" + Convert.ToDateTime(_EmpDate).ToString("yyyy/MM/dd") + ")以前無法申報")
                        ucOTStartDate.DateText = Date.Now.ToString("yyyy/MM/dd")
                        ucOTEndDate.DateText = ucOTStartDate.DateText
                        Return
                    Else
                        '計算總計
                        'Dim OverTimeSumArr As ArrayList = objOV42.GetOverTimeSum(UserProfile.ActCompID.ToString().Trim, txtOTEmpID.Text.Trim, ucOTStartDate.DateText.ToString())
                        'If OverTimeSumArr.Count <> 0 Then
                        '    Dim OverTimeSumStr As String = Month(ucOTStartDate.DateText).ToString() + "月份已申報時數合計: 送簽 " + OverTimeSumArr(0).ToString + "小時&nbsp&nbsp&nbsp核准 " + OverTimeSumArr(1).ToString + "小時&nbsp&nbsp&nbsp駁回 " + OverTimeSumArr(2).ToString + "小時"
                        '    lblTotalOTCalc.Text = OverTimeSumStr
                        'End If

                        Dim OverTimeSumArr As ArrayList = New ArrayList
                        OverTimeSumArr.AddRange(objOVBC.getTotalHR(hidCompID.Value.ToString(), txtOTEmpID.Text.Trim, ucOTStartDate.DateText.ToString()))

                        If OverTimeSumArr.Count <> 0 Then
                            Dim OverTimeSumStr As String = ""
                            OverTimeSumStr = If(OverTimeSumArr(0) IsNot Nothing AndAlso OverTimeSumArr(0).ToString() <> "", OverTimeSumArr(0).ToString(), Month(ucOTStartDate.DateText).ToString()) _
                                                           + "月份已申報時數合計: 送簽 " + If(OverTimeSumArr(1) IsNot Nothing AndAlso OverTimeSumArr(1).ToString() <> "", OverTimeSumArr(1).ToString(), "0.0") _
                                                           + "小時&nbsp&nbsp&nbsp核准 " + If(OverTimeSumArr(2) IsNot Nothing AndAlso OverTimeSumArr(2).ToString() <> "", OverTimeSumArr(2).ToString(), "0.0") _
                                                           + "小時&nbsp&nbsp&nbsp駁回 " + If(OverTimeSumArr(3) IsNot Nothing AndAlso OverTimeSumArr(3).ToString() <> "", OverTimeSumArr(3).ToString(), "0.0") + "小時"
                            lblTotalOTCalc.Text = OverTimeSumStr
                        End If
                    End If
                End If
            End If
            '依照RankID階級與加班起迄日來控制 加班轉換方式的下拉選項
            ddlSalaryOrAdjustChange(_rankID, ucOTStartDate.DateText, ucOTEndDate.DateText)
            ddlSalaryOrAdjust_SelectedIndexChanged(Nothing, EventArgs.Empty)
        End If
    End Sub

    ''' <summary>
    ''' 結束日期
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ValidEndDate_Click(sender As Object, e As System.EventArgs) Handles ValidEndDate.Click
        Dim objOV42 As New OV4_2
        Dim objOVBC As New OVBusinessCommon
        If ucOTStartTime.ucDefaultSelectedHH <> "請選擇" AndAlso ucOTStartTime.ucDefaultSelectedMM <> "請選擇" AndAlso ucOTEndTime.ucDefaultSelectedHH <> "請選擇" AndAlso ucOTEndTime.ucDefaultSelectedMM <> "請選擇" Then
            If ucOTStartDate.DateText <> "" And ucOTEndDate.DateText <> "" Then
                '如果結束日期選擇未來日期
                If Date.Now.Date < Convert.ToDateTime(ucOTEndDate.DateText) Then
                    Bsp.Utility.ShowMessage(Me, "加班日期不可以選擇未來日期")
                    ucOTStartDate.DateText = Date.Now.ToString("yyyy/MM/dd")
                    ucOTEndDate.DateText = ucOTStartDate.DateText
                    Return
                End If

                Dim total As TimeSpan = (Convert.ToDateTime(ucOTEndDate.DateText)).Subtract(Convert.ToDateTime(ucOTStartDate.DateText))
                Dim totalBefore As TimeSpan = (Date.Now.Date).Subtract(Convert.ToDateTime(ucOTStartDate.DateText))
                If Date.Now.Date > Convert.ToDateTime(ucOTStartDate.DateText) Then
                    If (Convert.ToInt32(totalBefore.Days)) > (Convert.ToInt32(_dtPara.Rows(0).Item("DeclarationBegin").ToString())) Then
                        Bsp.Utility.ShowMessage(Me, "加班事後申報日的範圍" + _dtPara.Rows(0).Item("DeclarationBegin").ToString() + "天前")
                        ucOTStartDate.DateText = Date.Now.ToString("yyyy/MM/dd")
                        ucOTEndDate.DateText = ucOTStartDate.DateText
                        Return
                    End If
                Else
                    Bsp.Utility.ShowMessage(Me, "加班日期不可以選擇未來日期")
                    ucOTStartDate.DateText = Date.Now.ToString("yyyy/MM/dd")
                    ucOTEndDate.DateText = ucOTStartDate.DateText
                    Return
                End If
                If (Convert.ToDateTime(ucOTEndDate.DateText) < Convert.ToDateTime(ucOTStartDate.DateText)) Then
                    Bsp.Utility.ShowMessage(Me, "加班結束日期不得小於加班開始日期")
                    ucOTStartDate.DateText = Date.Now.ToString("yyyy/MM/dd")
                    ucOTEndDate.DateText = ucOTStartDate.DateText
                    Return
                ElseIf Convert.ToInt32(total.Days) > 1 Then
                    Bsp.Utility.ShowMessage(Me, "加班日期間隔不得大於1日")
                    ucOTStartDate.DateText = Date.Now.ToString("yyyy/MM/dd")
                    ucOTEndDate.DateText = ucOTStartDate.DateText
                    Return
                End If
                If txtOTEmpID.Text <> "" Then
                    '檢查到職日以前不可以加
                    If (Convert.ToDateTime(ucOTStartDate.DateText) < Convert.ToDateTime(_EmpDate)) Then
                        Bsp.Utility.ShowMessage(Me, "到職日(" + Convert.ToDateTime(_EmpDate).ToString("yyyy/MM/dd") + ")以前無法申報")
                        ucOTStartDate.DateText = Date.Now.ToString("yyyy/MM/dd")
                        ucOTEndDate.DateText = ucOTStartDate.DateText
                        Return
                    ElseIf (Convert.ToDateTime(ucOTEndDate.DateText) < Convert.ToDateTime(_EmpDate)) Then
                        Bsp.Utility.ShowMessage(Me, "到職日(" + Convert.ToDateTime(_EmpDate).ToString("yyyy/MM/dd") + ")以前無法申報")
                        ucOTStartDate.DateText = Date.Now.ToString("yyyy/MM/dd")
                        ucOTEndDate.DateText = ucOTStartDate.DateText
                        Return
                    Else
                        '計算總計
                        'Dim OverTimeSumArr As ArrayList = objOV42.GetOverTimeSum(UserProfile.ActCompID.ToString().Trim, txtOTEmpID.Text.Trim, ucOTStartDate.DateText.ToString())
                        'If OverTimeSumArr.Count <> 0 Then
                        '    Dim OverTimeSumStr As String = Month(ucOTStartDate.DateText).ToString() + "月份已申報時數合計: 送簽 " + OverTimeSumArr(0).ToString + "小時&nbsp&nbsp&nbsp核准 " + OverTimeSumArr(1).ToString + "小時&nbsp&nbsp&nbsp駁回 " + OverTimeSumArr(2).ToString + "小時"
                        '    lblTotalOTCalc.Text = OverTimeSumStr
                        'End If

                        Dim OverTimeSumArr As ArrayList = New ArrayList
                        OverTimeSumArr.AddRange(objOVBC.getTotalHR(hidCompID.Value.ToString(), txtOTEmpID.Text.Trim, ucOTStartDate.DateText.ToString()))

                        If OverTimeSumArr.Count <> 0 Then
                            Dim OverTimeSumStr As String = ""
                            OverTimeSumStr = If(OverTimeSumArr(0) IsNot Nothing AndAlso OverTimeSumArr(0).ToString() <> "", OverTimeSumArr(0).ToString(), Month(ucOTStartDate.DateText).ToString()) _
                                                           + "月份已申報時數合計: 送簽 " + If(OverTimeSumArr(1) IsNot Nothing AndAlso OverTimeSumArr(1).ToString() <> "", OverTimeSumArr(1).ToString(), "0.0") _
                                                           + "小時&nbsp&nbsp&nbsp核准 " + If(OverTimeSumArr(2) IsNot Nothing AndAlso OverTimeSumArr(2).ToString() <> "", OverTimeSumArr(2).ToString(), "0.0") _
                                                           + "小時&nbsp&nbsp&nbsp駁回 " + If(OverTimeSumArr(3) IsNot Nothing AndAlso OverTimeSumArr(3).ToString() <> "", OverTimeSumArr(3).ToString(), "0.0") + "小時"
                            lblTotalOTCalc.Text = OverTimeSumStr
                        End If
                    End If
                End If

                If ucOTStartDate.DateText = ucOTEndDate.DateText Then
                    '若結束時間小於開始時間
                    If Convert.ToInt32(ucOTStartTime.ucDefaultSelectedHH) > Convert.ToInt32(ucOTEndTime.ucDefaultSelectedHH) Then
                        Bsp.Utility.ShowMessage(Me, "結束時間小於開始時間")
                        ucOTEndTime.ucDefaultSelectedHH = "請選擇"
                        ucOTEndTime.ucDefaultSelectedMM = "請選擇"
                        lblEndSex.Visible = False
                        Return
                    End If
                    If Convert.ToInt32(ucOTStartTime.ucDefaultSelectedHH) >= Convert.ToInt32(ucOTEndTime.ucDefaultSelectedHH) AndAlso Convert.ToInt32(ucOTStartTime.ucDefaultSelectedMM) > Convert.ToInt32(ucOTEndTime.ucDefaultSelectedMM) Then
                        Bsp.Utility.ShowMessage(Me, "結束時間小於開始時間")
                        ucOTEndTime.ucDefaultSelectedHH = "請選擇"
                        ucOTEndTime.ucDefaultSelectedMM = "請選擇"
                        lblEndSex.Visible = False
                        Return
                    End If
                End If

                '依照RankID階級與加班起迄日來控制 加班轉換方式的下拉選項
                ddlSalaryOrAdjustChange(_rankID, ucOTStartDate.DateText, ucOTEndDate.DateText)
                ddlSalaryOrAdjust_SelectedIndexChanged(Nothing, EventArgs.Empty)
            End If
        Else    '如果Usr尚未選擇時間，判斷日期不要跨超過一天以及是否是未來日期就好
            If ucOTStartDate.DateText <> "" And ucOTEndDate.DateText <> "" Then
                '如果結束日期選擇未來日期
                If Date.Now.Date < Convert.ToDateTime(ucOTEndDate.DateText) Then
                    Bsp.Utility.ShowMessage(Me, "加班日期不可以選擇未來日期")
                    ucOTStartDate.DateText = Date.Now.ToString("yyyy/MM/dd")
                    ucOTEndDate.DateText = ucOTStartDate.DateText
                    Return
                End If

                Dim total As TimeSpan = (Convert.ToDateTime(ucOTEndDate.DateText)).Subtract(Convert.ToDateTime(ucOTStartDate.DateText))
                Dim totalBefore As TimeSpan = (Date.Now.Date).Subtract(Convert.ToDateTime(ucOTStartDate.DateText))
                If Date.Now.Date > Convert.ToDateTime(ucOTStartDate.DateText) Then
                    If (Convert.ToInt32(totalBefore.Days)) > (Convert.ToInt32(_dtPara.Rows(0).Item("DeclarationBegin").ToString())) Then
                        Bsp.Utility.ShowMessage(Me, "加班事後申報日的範圍" + _dtPara.Rows(0).Item("DeclarationBegin").ToString() + "天前")
                        ucOTStartDate.DateText = Date.Now.ToString("yyyy/MM/dd")
                        ucOTEndDate.DateText = ucOTStartDate.DateText
                        Return
                    End If
                Else
                    Bsp.Utility.ShowMessage(Me, "加班日期不可以選擇未來日期")
                    ucOTStartDate.DateText = Date.Now.ToString("yyyy/MM/dd")
                    ucOTEndDate.DateText = ucOTStartDate.DateText
                    Return
                End If
                If (Convert.ToDateTime(ucOTEndDate.DateText) < Convert.ToDateTime(ucOTStartDate.DateText)) Then
                    Bsp.Utility.ShowMessage(Me, "加班結束日期不得小於加班開始日期")
                    ucOTStartDate.DateText = Date.Now.ToString("yyyy/MM/dd")
                    ucOTEndDate.DateText = ucOTStartDate.DateText
                    Return
                ElseIf Convert.ToInt32(total.Days) > 1 Then
                    Bsp.Utility.ShowMessage(Me, "加班日期間隔不得大於1日")
                    ucOTStartDate.DateText = Date.Now.ToString("yyyy/MM/dd")
                    ucOTEndDate.DateText = ucOTStartDate.DateText
                    Return
                End If

                If txtOTEmpID.Text <> "" Then
                    '檢查到職日以前不可以加
                    If (Convert.ToDateTime(ucOTStartDate.DateText) < Convert.ToDateTime(_EmpDate)) Then
                        Bsp.Utility.ShowMessage(Me, "到職日(" + Convert.ToDateTime(_EmpDate).ToString("yyyy/MM/dd") + ")以前無法申報")
                        ucOTStartDate.DateText = Date.Now.ToString("yyyy/MM/dd")
                        ucOTEndDate.DateText = ucOTStartDate.DateText
                        Return
                    ElseIf (Convert.ToDateTime(ucOTEndDate.DateText) < Convert.ToDateTime(_EmpDate)) Then
                        Bsp.Utility.ShowMessage(Me, "到職日(" + Convert.ToDateTime(_EmpDate).ToString("yyyy/MM/dd") + ")以前無法申報")
                        ucOTStartDate.DateText = Date.Now.ToString("yyyy/MM/dd")
                        ucOTEndDate.DateText = ucOTStartDate.DateText
                        Return
                    Else
                        '計算總計
                        'Dim OverTimeSumArr As ArrayList = objOV42.GetOverTimeSum(UserProfile.ActCompID.ToString().Trim, txtOTEmpID.Text.Trim, ucOTStartDate.DateText.ToString())
                        'If OverTimeSumArr.Count <> 0 Then
                        '    Dim OverTimeSumStr As String = Month(ucOTStartDate.DateText).ToString() + "月份已申報時數合計: 送簽 " + OverTimeSumArr(0).ToString + "小時&nbsp&nbsp&nbsp核准 " + OverTimeSumArr(1).ToString + "小時&nbsp&nbsp&nbsp駁回 " + OverTimeSumArr(2).ToString + "小時"
                        '    lblTotalOTCalc.Text = OverTimeSumStr
                        'End If

                        Dim OverTimeSumArr As ArrayList = New ArrayList
                        OverTimeSumArr.AddRange(objOVBC.getTotalHR(hidCompID.Value.ToString(), txtOTEmpID.Text.Trim, ucOTStartDate.DateText.ToString()))

                        If OverTimeSumArr.Count <> 0 Then
                            Dim OverTimeSumStr As String = ""
                            OverTimeSumStr = If(OverTimeSumArr(0) IsNot Nothing AndAlso OverTimeSumArr(0).ToString() <> "", OverTimeSumArr(0).ToString(), Month(ucOTStartDate.DateText).ToString()) _
                                                           + "月份已申報時數合計: 送簽 " + If(OverTimeSumArr(1) IsNot Nothing AndAlso OverTimeSumArr(1).ToString() <> "", OverTimeSumArr(1).ToString(), "0.0") _
                                                           + "小時&nbsp&nbsp&nbsp核准 " + If(OverTimeSumArr(2) IsNot Nothing AndAlso OverTimeSumArr(2).ToString() <> "", OverTimeSumArr(2).ToString(), "0.0") _
                                                           + "小時&nbsp&nbsp&nbsp駁回 " + If(OverTimeSumArr(3) IsNot Nothing AndAlso OverTimeSumArr(3).ToString() <> "", OverTimeSumArr(3).ToString(), "0.0") + "小時"
                            lblTotalOTCalc.Text = OverTimeSumStr
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' 開始時間
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ValidStartTime_Click(sender As Object, e As System.EventArgs) Handles ValidStartTime.Click
        'If ucOTStartTime.ucDefaultSelectedHH <> "請選擇" AndAlso ucOTStartTime.ucDefaultSelectedMM <> "請選擇" AndAlso ucOTEndTime.ucDefaultSelectedHH <> "請選擇" AndAlso ucOTEndTime.ucDefaultSelectedMM <> "請選擇" Then
        If txtOTEmpID.Text.Trim <> "" Then
            If _Sex <> "" And _Sex = "2" Then
                If ucOTStartDate.DateText <> "" AndAlso ucOTEndDate.DateText <> "" AndAlso ucOTStartTime.ucDefaultSelectedHH <> "請選擇" AndAlso ucOTStartTime.ucDefaultSelectedMM <> "請選擇" Then
                    '從10點開始
                    If Convert.ToInt32(ucOTStartTime.ucDefaultSelectedHH) = 22 Then
                        lblStartSex.Visible = True   '女性不可以10點後加班
                        'Bsp.Utility.ShowMessage(Me, "女性不可以10點後加班")
                    ElseIf Convert.ToInt32(ucOTStartTime.ucDefaultSelectedHH) > 22 Then
                        lblStartSex.Visible = True   '女性不可以10點後加班
                        'Bsp.Utility.ShowMessage(Me, "女性不可以10點後加班")
                    ElseIf Convert.ToInt32(ucOTStartTime.ucDefaultSelectedHH) >= 0 And Convert.ToInt32(ucOTStartTime.ucDefaultSelectedHH) < 6 Then
                        lblStartSex.Visible = True   '女性不可以10點後加班
                        'Bsp.Utility.ShowMessage(Me, "女性不可以10點後加班")
                    Else
                        lblStartSex.Visible = False
                    End If
                End If
            Else
                lblStartSex.Visible = False
                lblEndSex.Visible = False
            End If
        End If

        If (ucOTStartTime.ucDefaultSelectedHH <> "請選擇" AndAlso ucOTStartTime.ucDefaultSelectedMM <> "請選擇" AndAlso ucOTEndTime.ucDefaultSelectedHH <> "請選擇" AndAlso ucOTEndTime.ucDefaultSelectedMM <> "請選擇" AndAlso _
    ucOTStartTime.ucDefaultSelectedHH <> "" AndAlso ucOTStartTime.ucDefaultSelectedMM <> "" AndAlso ucOTEndTime.ucDefaultSelectedHH <> "" AndAlso ucOTEndTime.ucDefaultSelectedMM <> "" AndAlso _
    ucOTStartDate.DateText <> "" AndAlso ucOTEndDate.DateText <> "") Then
            '檢核若加班起迄日相同時起始時間不可大於結束時間
            If ucOTStartDate.DateText = ucOTEndDate.DateText Then
                If Convert.ToDateTime(ucOTStartDate.DateText + " " + ucOTStartTime.ucSelectedTime).ToString("yyyy/MM/dd HH:mm:ss") > Convert.ToDateTime(ucOTEndDate.DateText + " " + ucOTEndTime.ucSelectedTime).ToString("yyyy/MM/dd HH:mm:ss") Then
                    Bsp.Utility.ShowMessage(Me, "不可以申報未來時間點")
                    ucOTStartTime.ucDefaultSelectedMM = "請選擇"
                    ucOTStartTime.ucDefaultSelectedHH = "請選擇"
                    Return
                End If
            End If

            If Date.Now.Date = Convert.ToDateTime(ucOTStartDate.DateText) Then  '如果加班起日等於現在日期
                If Convert.ToInt32(ucOTStartTime.ucDefaultSelectedHH) > Convert.ToInt32(Date.Now.Hour) Then
                    Bsp.Utility.ShowMessage(Me, "不可以申報未來時間點")
                    ucOTStartTime.ucDefaultSelectedMM = "請選擇"
                    ucOTStartTime.ucDefaultSelectedHH = "請選擇"
                    pnlCalcTotalTime.Visible = False
                    litTable.Text = ""
                    lblOTTotalTime.Text = ""
                    txtOTTotalDescript.Visible = False
                    lblStartSex.Visible = False
                    Return
                ElseIf Convert.ToInt32(ucOTStartTime.ucDefaultSelectedHH) = Convert.ToInt32(Date.Now.Hour) And Convert.ToInt32(ucOTStartTime.ucDefaultSelectedMM) > Convert.ToInt32(Date.Now.Minute) Then
                    Bsp.Utility.ShowMessage(Me, "不可以申報未來時間點")
                    ucOTStartTime.ucDefaultSelectedMM = "請選擇"
                    ucOTStartTime.ucDefaultSelectedHH = "請選擇"
                    pnlCalcTotalTime.Visible = False
                    litTable.Text = ""
                    lblOTTotalTime.Text = ""
                    txtOTTotalDescript.Visible = False
                    lblStartSex.Visible = False
                    Return
                End If
            ElseIf Date.Now.Date < Convert.ToDateTime(ucOTEndDate.DateText) Then    '如果加班起日大於現在日期
                Bsp.Utility.ShowMessage(Me, "加班日期不可以選擇未來日期")
                ucOTStartDate.DateText = FormatDateTime(Date.Now, DateFormat.ShortDate)
                Return
            End If

            If Date.Now = Convert.ToDateTime(ucOTEndDate.DateText) Then '如果加班迄日等於現在日期
                If Convert.ToInt32(ucOTEndTime.ucDefaultSelectedHH) > Convert.ToInt32(Date.Now.Hour) Then
                    Bsp.Utility.ShowMessage(Me, "不可以申報未來時間點")
                    ucOTEndTime.ucDefaultSelectedHH = "請選擇"
                    ucOTEndTime.ucDefaultSelectedMM = "請選擇"
                    pnlCalcTotalTime.Visible = False
                    litTable.Text = ""
                    lblOTTotalTime.Visible = False
                    txtOTTotalDescript.Visible = False
                    lblEndSex.Visible = False
                    Return
                ElseIf Convert.ToInt32(ucOTEndTime.ucDefaultSelectedHH) = Convert.ToInt32(Date.Now.Hour) AndAlso Convert.ToInt32(ucOTEndTime.ucDefaultSelectedMM) > Convert.ToInt32(Date.Now.Minute) Then
                    Bsp.Utility.ShowMessage(Me, "不可以申報未來時間點")
                    ucOTEndTime.ucDefaultSelectedHH = "請選擇"
                    ucOTEndTime.ucDefaultSelectedMM = "請選擇"
                    pnlCalcTotalTime.Visible = False
                    litTable.Text = ""
                    lblOTTotalTime.Visible = False
                    txtOTTotalDescript.Visible = False
                    lblEndSex.Visible = False
                    Return
                End If
            End If
        End If
        If ucOTEndTime.ucDefaultSelectedHH <> "請選擇" AndAlso ucOTEndTime.ucDefaultSelectedMM <> "請選擇" Then
            ValidEndTime_Click(Nothing, EventArgs.Empty)
        End If
        'End If
    End Sub

    ''' <summary>
    ''' 結束時間
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ValidEndTime_Click(sender As Object, e As System.EventArgs) Handles ValidEndTime.Click
        Dim htmlTableString As String = ""
        Dim objOV42 As New OV4_2

        Dim chkMealFlag As String = If(MealFlag.Checked = True, "1", "0")
        Dim cntStart As Double = 0
        Dim cntEnd As Double = 0
        Dim cntTotal As Double = 0
        If ucOTStartTime.ucDefaultSelectedHH <> "" AndAlso ucOTStartTime.ucDefaultSelectedMM <> "" AndAlso ucOTEndTime.ucDefaultSelectedHH <> "" AndAlso ucOTEndTime.ucDefaultSelectedMM <> "" AndAlso ucOTStartTime.ucDefaultSelectedHH <> "請選擇" AndAlso ucOTStartTime.ucDefaultSelectedMM <> "請選擇" AndAlso ucOTEndTime.ucDefaultSelectedHH <> "請選擇" AndAlso ucOTEndTime.ucDefaultSelectedMM <> "請選擇" Then
            If _Sex <> "" And _Sex = "2" Then
                '從10點開始
                If Convert.ToInt32(ucOTEndTime.ucDefaultSelectedHH) = 22 Then
                    If ucOTEndTime.ucDefaultSelectedMM <> "請選擇" AndAlso Convert.ToInt32(ucOTEndTime.ucDefaultSelectedMM) >= 1 Then
                        lblEndSex.Visible = True   '女性不可以10點後加班
                        'Bsp.Utility.ShowMessage(Me, "女性不可以10點後加班")
                    Else
                        lblEndSex.Visible = False
                    End If
                ElseIf Convert.ToInt32(ucOTEndTime.ucDefaultSelectedHH) > 22 Then
                    lblEndSex.Visible = True   '女性不可以10點後加班
                    'Bsp.Utility.ShowMessage(Me, "女性不可以10點後加班")
                ElseIf Convert.ToInt32(ucOTEndTime.ucDefaultSelectedHH) >= 0 And Convert.ToInt32(ucOTEndTime.ucDefaultSelectedHH) < 6 Then
                    lblEndSex.Visible = True   '女性不可以10點後加班
                    'Bsp.Utility.ShowMessage(Me, "女性不可以10點後加班")
                Else
                    lblEndSex.Visible = False
                End If
            Else
                lblEndSex.Visible = False
            End If

            If ucOTStartDate.DateText = "" OrElse ucOTEndDate.DateText = "" Then
                Return
            End If

            If DateTime.Now.Date < Convert.ToDateTime(ucOTEndDate.DateText) Then '如果加班迄日大於現在日期
                Bsp.Utility.ShowMessage(Me, "加班日期不可以選擇未來日期")
                ucOTEndDate.DateText = ucOTStartDate.DateText
                Return
            ElseIf Date.Now.Date = Convert.ToDateTime(ucOTEndDate.DateText) Then '如果加班迄日等於現在日期
                If Convert.ToInt32(ucOTEndTime.ucDefaultSelectedHH) > Convert.ToInt32(Date.Now.Hour) Then
                    Bsp.Utility.ShowMessage(Me, "不可以申報未來時間點")
                    ucOTEndTime.ucDefaultSelectedHH = "請選擇"
                    ucOTEndTime.ucDefaultSelectedMM = "請選擇"
                    pnlCalcTotalTime.Visible = False
                    litTable.Text = ""
                    lblOTTotalTime.Visible = False
                    txtOTTotalDescript.Visible = False
                    lblEndSex.Visible = False
                    Return
                ElseIf Convert.ToInt32(ucOTEndTime.ucDefaultSelectedHH) = Convert.ToInt32(Date.Now.Hour) AndAlso Convert.ToInt32(ucOTEndTime.ucDefaultSelectedMM) > Convert.ToInt32(Date.Now.Minute) Then
                    Bsp.Utility.ShowMessage(Me, "不可以申報未來時間點")
                    ucOTEndTime.ucDefaultSelectedHH = "請選擇"
                    ucOTEndTime.ucDefaultSelectedMM = "請選擇"
                    pnlCalcTotalTime.Visible = False
                    litTable.Text = ""
                    lblOTTotalTime.Visible = False
                    txtOTTotalDescript.Visible = False
                    lblEndSex.Visible = False
                    Return
                End If
            End If

            If ucOTEndTime.ucDefaultSelectedHH = "00" And ucOTEndTime.ucDefaultSelectedMM = "00" Then
                Bsp.Utility.ShowMessage(Me, "最大時間上限為23:59")
                ucOTEndTime.ucDefaultSelectedHH = "請選擇"
                ucOTEndTime.ucDefaultSelectedMM = "請選擇"
                lblEndSex.Visible = False
                Return
            End If

            If ucOTStartDate.DateText = ucOTEndDate.DateText Then   '不跨日
                If Convert.ToInt32(ucOTStartTime.ucDefaultSelectedHH) > Convert.ToInt32(ucOTEndTime.ucDefaultSelectedHH) Then
                    Bsp.Utility.ShowMessage(Me, "結束時間小於開始時間")
                    ucOTEndTime.ucDefaultSelectedHH = "請選擇"
                    ucOTEndTime.ucDefaultSelectedMM = "請選擇"
                    lblEndSex.Visible = False
                    Return
                End If
                If Convert.ToInt32(ucOTStartTime.ucDefaultSelectedHH) >= Convert.ToInt32(ucOTEndTime.ucDefaultSelectedHH) AndAlso Convert.ToInt32(ucOTStartTime.ucDefaultSelectedMM) > Convert.ToInt32(ucOTEndTime.ucDefaultSelectedMM) Then
                    Bsp.Utility.ShowMessage(Me, "結束時間小於開始時間")
                    ucOTEndTime.ucDefaultSelectedHH = "請選擇"
                    ucOTEndTime.ucDefaultSelectedMM = "請選擇"
                    lblEndSex.Visible = False
                    Return
                End If

                getCntTotal(cntTotal)

                If cntTotal <= 0 Then
                    Bsp.Utility.ShowMessage(Me, "加班時間不可以低於一分鐘")
                    ucOTEndTime.ucDefaultSelectedHH = "請選擇"
                    ucOTEndTime.ucDefaultSelectedMM = "請選擇"
                    lblEndSex.Visible = False
                    Return
                End If
                If (txtOTEmpID.Text <> "") Then
                    If cntTotal > 120 Then      '加班超過兩小時需扣用餐時間60分鐘
                        EtxtMealTimeChecked(Nothing, EventArgs.Empty, True)
                    Else
                        EtxtMealTimeChecked(Nothing, EventArgs.Empty, False)
                    End If
                    ''計算時段
                    '********************************
                    Dim returnPeriodCount As String = ""
                    Dim bOTTimeStart As Boolean = Not String.IsNullOrEmpty(ucOTStartTime.ucDefaultSelectedMM) AndAlso Not String.IsNullOrEmpty(ucOTStartTime.ucDefaultSelectedHH) AndAlso ucOTStartTime.ucDefaultSelectedHH <> "請選擇" AndAlso ucOTStartTime.ucDefaultSelectedMM <> "請選擇"
                    Dim bOTTimeEnd As Boolean = Not String.IsNullOrEmpty(ucOTEndTime.ucDefaultSelectedMM) AndAlso Not String.IsNullOrEmpty(ucOTEndTime.ucDefaultSelectedHH) AndAlso ucOTEndTime.ucDefaultSelectedHH <> "請選擇" AndAlso ucOTEndTime.ucDefaultSelectedMM <> "請選擇"
                    If bOTTimeStart And bOTTimeEnd Then
                        Dim ShowStartDateArr, ShowEndDateArr As New ArrayList    '顯示用
                        Dim StartDateArr, EndDateArr As New ArrayList   '計算時段用
                        Dim sOTTimeStart = ucOTStartTime.ucDefaultSelectedHH + ucOTStartTime.ucDefaultSelectedMM
                        Dim sOTTimeEnd = ucOTEndTime.ucDefaultSelectedHH + ucOTEndTime.ucDefaultSelectedMM
                        Dim iOTTimeStart As Integer = 0
                        Dim iOTTimeEnd As Integer = 0

                        If (Integer.TryParse(sOTTimeStart, iOTTimeStart) AndAlso (Integer.TryParse(sOTTimeEnd, iOTTimeEnd))) Then
                            Dim smealFlag As String = If(MealFlag.Checked = True, "1", "0")
                            Dim sMealTime As String = If(String.IsNullOrEmpty(txtMealTime.Text) = True, "0", txtMealTime.Text.Trim)
                            Dim iMealTime As Integer = 0
                            Integer.TryParse(sMealTime, iMealTime)

                            If iOTTimeStart = 0 AndAlso iOTTimeEnd = 0 Then
                                Return
                            End If

                            StartDateArr.Add(ucOTStartDate.DateText)
                            StartDateArr.Add(sOTTimeStart)
                            StartDateArr.Add(sOTTimeEnd)

                            EndDateArr.Add("1900/01/01")
                            EndDateArr.Add("0")
                            EndDateArr.Add("0")

                            Dim bPeriodCount As Boolean = objOV42.PeriodCount("OverTimeDeclaration", txtOTEmpID.Text.Trim, cntTotal, 0, StartDateArr, EndDateArr, iMealTime, smealFlag, "", returnPeriodCount)
                            If bPeriodCount And Not String.IsNullOrEmpty(returnPeriodCount) AndAlso returnPeriodCount.Split(";"c).Length > 0 Then
                                Dim sReturnPeriodList = returnPeriodCount.Split(";"c)

                                For i = 0 To (sReturnPeriodList.Length - 1)
                                    Dim datas = sReturnPeriodList(i)
                                    If i = 0 AndAlso datas.Split(","c).Length > 0 AndAlso datas.Split(","c)(0) <> "1900/01/01" Then
                                        If datas.Split(","c).Length >= 1 Then ShowStartDateArr.Add(If(datas.Split(","c)(0) = "1900/01/01", "-", datas.Split(","c)(0)))
                                        If datas.Split(","c).Length >= 2 Then ShowStartDateArr.Add(If(datas.Split(","c)(1) = "0.0", "-", datas.Split(","c)(1)))
                                        If datas.Split(","c).Length >= 3 Then ShowStartDateArr.Add(If(datas.Split(","c)(2) = "0.0", "-", datas.Split(","c)(2)))
                                        If datas.Split(","c).Length >= 4 Then ShowStartDateArr.Add(If(datas.Split(","c)(3) = "0.0", "-", datas.Split(","c)(3)))
                                    End If

                                    If i = 1 AndAlso datas.Split(","c).Length > 0 AndAlso datas.Split(","c)(0) <> "1900/01/01" Then
                                        If datas.Split(","c).Length >= 1 Then ShowEndDateArr.Add(If(datas.Split(","c)(0) = "1900/01/01", "-", datas.Split(","c)(0)))
                                        If datas.Split(","c).Length >= 2 Then ShowEndDateArr.Add(If(datas.Split(","c)(1) = "0.0", "-", datas.Split(","c)(1)))
                                        If datas.Split(","c).Length >= 3 Then ShowEndDateArr.Add(If(datas.Split(","c)(2) = "0.0", "-", datas.Split(","c)(2)))
                                        If datas.Split(","c).Length >= 4 Then ShowEndDateArr.Add(If(datas.Split(","c)(3) = "0.0", "-", datas.Split(","c)(3)))
                                    End If
                                Next
                                htmlTableString = subTableGenByArr(ShowStartDateArr, ShowEndDateArr)
                                If htmlTableString <> "" Then
                                    litTable.Text = htmlTableString
                                    pnlCalcTotalTime.Visible = True
                                End If
                                txtOTTotalDescript.Text = (If(txtMealTime.Text <> "" AndAlso Convert.ToDouble(txtMealTime.Text) > 0 AndAlso MealFlag.Checked = True, "(已扣除用餐時數" + txtMealTime.Text + "分鐘)", ""))
                                txtOTTotalDescript.Visible = True

                                Dim meal As String = If(MealFlag.Checked = False, "0", txtMealTime.Text)
                                meal = If(String.IsNullOrEmpty(meal), "0", txtMealTime.Text)
                                lblOTTotalTime.Text = Convert.ToDouble((cntTotal - Convert.ToDouble(meal)) / 60).ToString("0.0")
                                lblOTTotalTime.Visible = True
                            Else
                                Bsp.Utility.ShowMessage(Me, returnPeriodCount)
                                initalData()
                                Return
                            End If
                        End If
                    End If
                    '********************************
                End If
            Else    '跨日
                If txtOTEmpID.Text <> "" Then
                    getCntStartAndCntEnd(cntStart, cntEnd)
                    If cntEnd + cntStart > 120 Then     '加班超過兩小時需扣用餐時間60分鐘
                        EtxtMealTimeChecked(Nothing, EventArgs.Empty, True)
                    Else
                        EtxtMealTimeChecked(Nothing, EventArgs.Empty, False)
                    End If
                    ''計算時段
                    '********************************
                    Dim returnPeriodCount As String = ""
                    Dim bOTTimeStart As Boolean = (Not String.IsNullOrEmpty(ucOTStartTime.ucDefaultSelectedMM) AndAlso Not String.IsNullOrEmpty(ucOTStartTime.ucDefaultSelectedHH) AndAlso ucOTStartTime.ucDefaultSelectedHH <> "請選擇" AndAlso ucOTStartTime.ucDefaultSelectedMM <> "請選擇")
                    Dim bOTTimeEnd As Boolean = (Not String.IsNullOrEmpty(ucOTEndTime.ucDefaultSelectedMM) AndAlso Not String.IsNullOrEmpty(ucOTEndTime.ucDefaultSelectedHH) AndAlso ucOTEndTime.ucDefaultSelectedHH <> "請選擇" AndAlso ucOTEndTime.ucDefaultSelectedMM <> "請選擇")
                    If bOTTimeStart AndAlso bOTTimeEnd Then
                        Dim ShowStartDateArr, ShowEndDateArr As New ArrayList    '顯示用
                        Dim StartDateArr, EndDateArr As New ArrayList   '計算時段用
                        Dim sOTTimeStart = ucOTStartTime.ucDefaultSelectedHH + ucOTStartTime.ucDefaultSelectedMM
                        Dim sOTTimeEnd = ucOTEndTime.ucDefaultSelectedHH + ucOTEndTime.ucDefaultSelectedMM
                        Dim iOTTimeStart As Integer = 0
                        Dim iOTTimeEnd As Integer = 0
                        If (Integer.TryParse(sOTTimeStart, iOTTimeStart) And (Integer.TryParse(sOTTimeEnd, iOTTimeEnd))) Then
                            Dim smealFlag As String = If(MealFlag.Checked, "1", "0")
                            Dim sMealTime As String = If(String.IsNullOrEmpty(txtMealTime.Text), "0", txtMealTime.Text.Trim)
                            Dim iMealTime As Integer = 0
                            Integer.TryParse(sMealTime, iMealTime)

                            If iOTTimeStart = 0 AndAlso iOTTimeEnd = 0 Then
                                Return
                            End If

                            StartDateArr.Add(ucOTStartDate.DateText)
                            StartDateArr.Add(sOTTimeStart)
                            StartDateArr.Add("2359")

                            EndDateArr.Add(ucOTEndDate.DateText)
                            EndDateArr.Add("0")
                            EndDateArr.Add(sOTTimeEnd)

                            Dim bPeriodCount As Boolean = objOV42.PeriodCount("OverTimeDeclaration", txtOTEmpID.Text.Trim, cntStart, cntEnd, StartDateArr, EndDateArr, iMealTime, smealFlag, "", returnPeriodCount)
                            If bPeriodCount And String.IsNullOrEmpty(returnPeriodCount) <> True And returnPeriodCount.Split(";"c).Length > 0 Then
                                Dim sReturnPeriodList = returnPeriodCount.Split(";"c)

                                For i = 0 To (sReturnPeriodList.Length - 1)
                                    Dim datas = sReturnPeriodList(i)
                                    If i = 0 And datas.Split(","c).Length > 0 AndAlso datas.Split(","c)(0) <> "1900/01/01" Then
                                        If datas.Split(","c).Length >= 1 Then ShowStartDateArr.Add(If(datas.Split(","c)(0) = "1900/01/01", "-", datas.Split(","c)(0)))
                                        If datas.Split(","c).Length >= 2 Then ShowStartDateArr.Add(If(datas.Split(","c)(1) = "0.0", "-", datas.Split(","c)(1)))
                                        If datas.Split(","c).Length >= 3 Then ShowStartDateArr.Add(If(datas.Split(","c)(2) = "0.0", "-", datas.Split(","c)(2)))
                                        If datas.Split(","c).Length >= 4 Then ShowStartDateArr.Add(If(datas.Split(","c)(3) = "0.0", "-", datas.Split(","c)(3)))
                                    End If

                                    If i = 1 And datas.Split(","c).Length > 0 AndAlso datas.Split(","c)(0) <> "1900/01/01" Then
                                        If datas.Split(","c).Length >= 1 Then ShowEndDateArr.Add(If(datas.Split(","c)(0) = "1900/01/01", "-", datas.Split(","c)(0)))
                                        If datas.Split(","c).Length >= 2 Then ShowEndDateArr.Add(If(datas.Split(","c)(1) = "0.0", "-", datas.Split(","c)(1)))
                                        If datas.Split(","c).Length >= 3 Then ShowEndDateArr.Add(If(datas.Split(","c)(2) = "0.0", "-", datas.Split(","c)(2)))
                                        If datas.Split(","c).Length >= 4 Then ShowEndDateArr.Add(If(datas.Split(","c)(3) = "0.0", "-", datas.Split(","c)(3)))
                                    End If
                                Next
                                htmlTableString = subTableGenByArr(ShowStartDateArr, ShowEndDateArr)
                                If htmlTableString <> "" Then
                                    litTable.Text = htmlTableString
                                    pnlCalcTotalTime.Visible = True
                                End If
                                txtOTTotalDescript.Text = (If(txtMealTime.Text <> "" And Convert.ToDouble(txtMealTime.Text) > 0 And MealFlag.Checked = True, "(已扣除用餐時數" + txtMealTime.Text + "分鐘)", ""))
                                txtOTTotalDescript.Visible = True

                                Dim meal As String = If(MealFlag.Checked = False, "0", txtMealTime.Text)
                                meal = If(String.IsNullOrEmpty(meal), "0", txtMealTime.Text)
                                lblOTTotalTime.Text = Convert.ToDouble((cntEnd + cntStart - Convert.ToDouble(meal)) / 60).ToString("0.0")
                                lblOTTotalTime.Visible = True
                            Else
                                Bsp.Utility.ShowMessage(Me, returnPeriodCount)
                                initalData()
                                Return
                            End If
                        End If
                    End If
                    '********************************
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' 加入用餐時數(從參數檔)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="isChecked"></param>
    ''' <remarks></remarks>
    Protected Sub EtxtMealTimeChecked(ByVal sender As Object, ByVal e As EventArgs, ByVal isChecked As Boolean)
        Dim objOV42 As New OV4_2
        MealFlag.Checked = isChecked
        If isChecked Then
            txtMealTime.Enabled = True
            Dim strHo As String = objOV42.QueryColumn("HolidayOrNot", _eHRMSDB_ITRD + ".[dbo].[Calendar]", " AND CompID =" + Bsp.Utility.Quote(UserProfile.SelectCompRoleID.Trim) + " AND  CONVERT(CHAR(10),SysDate, 111) = " + Bsp.Utility.Quote(ucOTStartDate.DateText))
            If Not String.IsNullOrEmpty(strHo) Then
                If strHo = "0" Then
                    txtMealTime.Text = _dtPara.Rows(0).Item("MealTimeN").ToString()
                Else
                    txtMealTime.Text = _dtPara.Rows(0).Item("MealTimeH").ToString()
                End If
            Else
                txtMealTime.Text = "60"
            End If
        Else
            txtMealTime.Text = "0"
            txtMealTime.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' 是否加入用餐時數事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub MealFlag_CheckedChanged(sender As Object, e As System.EventArgs) Handles MealFlag.CheckedChanged

        Dim objOV42 As New OV4_2
        If Not MealFlag.Checked Then
            txtMealTime.Text = "0"
            txtMealTime.Enabled = False
        Else
            Dim cntTotal As Double = 0
            If ucOTStartDate.DateText = ucOTEndDate.DateText Then   '不跨日
                getCntTotal(cntTotal)
            Else
                Dim cntStart As Double = 0
                Dim cntEnd As Double = 0
                getCntStartAndCntEnd(cntStart, cntEnd)
                cntTotal = cntStart + cntEnd
            End If

            If cntTotal > 120 Then
                '加班超過兩小時需扣用餐時間60分鐘
                txtMealTime.Enabled = True
                Dim strHo = objOV42.CheckHolidayOrNot(ucOTStartDate.DateText)
                If Not String.IsNullOrEmpty(strHo) Then
                    If strHo = "0" Then
                        txtMealTime.Text = _dtPara.Rows(0)("MealTimeN").ToString()
                    Else
                        txtMealTime.Text = _dtPara.Rows(0)("MealTimeH").ToString()
                    End If
                Else
                    txtMealTime.Text = "60"
                End If
            Else
                txtMealTime.Text = "0"
                txtMealTime.Enabled = True
            End If
        End If
        txtMealTime_TextChanged(Nothing, Nothing)
    End Sub

    ''' <summary>
    ''' 使用者輸入用餐時數事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub txtMealTime_TextChanged(sender As Object, e As System.EventArgs) Handles txtMealTime.TextChanged

        Dim objOV42 As New OV4_2
        Dim htmlTableString As String = ""
        Dim cntStart, cntEnd, cntTotal As Double
        If ucOTStartTime.ucDefaultSelectedHH <> "請選擇" AndAlso ucOTStartTime.ucDefaultSelectedMM <> "請選擇" AndAlso ucOTEndTime.ucDefaultSelectedHH <> "請選擇" AndAlso ucOTEndTime.ucDefaultSelectedMM <> "請選擇" AndAlso ucOTStartTime.ucDefaultSelectedHH <> "" AndAlso ucOTStartTime.ucDefaultSelectedMM <> "" AndAlso ucOTEndTime.ucDefaultSelectedHH <> "" AndAlso ucOTEndTime.ucDefaultSelectedMM <> "" Then
            If ucOTStartDate.DateText = ucOTEndDate.DateText Then
                '不跨日
                If ucOTEndTime.ucDefaultSelectedHH = "00" And ucOTEndTime.ucDefaultSelectedMM = "00" Then
                    Bsp.Utility.ShowMessage(Me, "最大時間上限為23:59")
                    initalData()
                    Return
                End If
                getCntTotal(cntTotal)
                If txtMealTime.Text.Trim = "" Then
                    txtMealTime.Text = "0"
                    'Return
                End If

                If Not String.IsNullOrEmpty(txtMealTime.Text) AndAlso Not IsNumeric(txtMealTime.Text) Then
                    Bsp.Utility.ShowMessage(Me, "請輸入數字!")
                    txtMealTime.Focus()
                    Return
                End If

                If txtMealTime.Text <> "" And Convert.ToDouble(txtMealTime.Text.Trim) >= cntTotal Then
                    txtMealTime.Focus()
                    Bsp.Utility.ShowMessage(Me, "用餐時數超過加班時數")
                Else
                    '計算時段
                    '********************************
                    Dim returnPeriodCount As String = ""
                    Dim bOTTimeStart As Boolean = Not String.IsNullOrEmpty(ucOTStartTime.ucDefaultSelectedMM) AndAlso Not String.IsNullOrEmpty(ucOTStartTime.ucDefaultSelectedHH) AndAlso ucOTStartTime.ucDefaultSelectedHH <> "請選擇" AndAlso ucOTStartTime.ucDefaultSelectedMM <> "請選擇"
                    Dim bOTTimeEnd As Boolean = Not String.IsNullOrEmpty(ucOTEndTime.ucDefaultSelectedMM) And Not String.IsNullOrEmpty(ucOTEndTime.ucDefaultSelectedHH) AndAlso ucOTEndTime.ucDefaultSelectedHH <> "請選擇" AndAlso ucOTEndTime.ucDefaultSelectedMM <> "請選擇"
                    If bOTTimeStart And bOTTimeEnd Then
                        Dim ShowStartDateArr, ShowEndDateArr As New ArrayList    '顯示用
                        Dim StartDateArr, EndDateArr As New ArrayList   '計算時段用
                        Dim sOTTimeStart = ucOTStartTime.ucDefaultSelectedHH + ucOTStartTime.ucDefaultSelectedMM
                        Dim sOTTimeEnd = ucOTEndTime.ucDefaultSelectedHH + ucOTEndTime.ucDefaultSelectedMM
                        Dim iOTTimeStart As Integer = 0
                        Dim iOTTimeEnd As Integer = 0
                        If (Integer.TryParse(sOTTimeStart, iOTTimeStart) And (Integer.TryParse(sOTTimeEnd, iOTTimeEnd))) Then
                            Dim smealFlag As String = If(MealFlag.Checked = True, "1", "0")
                            Dim sMealTime As String = If(String.IsNullOrEmpty(txtMealTime.Text.Trim), "0", txtMealTime.Text.Trim)
                            Dim iMealTime As Integer = 0
                            Integer.TryParse(sMealTime, iMealTime)

                            If iOTTimeStart = 0 AndAlso iOTTimeEnd = 0 Then
                                Return
                            End If

                            StartDateArr.Add(ucOTStartDate.DateText)
                            StartDateArr.Add(sOTTimeStart)
                            StartDateArr.Add(sOTTimeEnd)

                            EndDateArr.Add("1900/01/01")
                            EndDateArr.Add("0")
                            EndDateArr.Add("0")

                            Dim bPeriodCount As Boolean = objOV42.PeriodCount("OverTimeDeclaration", txtOTEmpID.Text.Trim, cntTotal, 0, StartDateArr, EndDateArr, iMealTime, smealFlag, "", returnPeriodCount)
                            If bPeriodCount And String.IsNullOrEmpty(returnPeriodCount) <> True And returnPeriodCount.Split(";"c).Length > 0 Then
                                Dim sReturnPeriodList = returnPeriodCount.Split(";"c)

                                For i = 0 To (sReturnPeriodList.Length - 1)
                                    Dim datas = sReturnPeriodList(i)
                                    If i = 0 And datas.Split(","c).Length > 0 And datas.Split(","c)(0) <> "1900/01/01" Then
                                        If datas.Split(","c).Length >= 1 Then ShowStartDateArr.Add(If(datas.Split(","c)(0) = "1900/01/01", "-", datas.Split(","c)(0)))
                                        If datas.Split(","c).Length >= 2 Then ShowStartDateArr.Add(If(datas.Split(","c)(1) = "0.0", "-", datas.Split(","c)(1)))
                                        If datas.Split(","c).Length >= 3 Then ShowStartDateArr.Add(If(datas.Split(","c)(2) = "0.0", "-", datas.Split(","c)(2)))
                                        If datas.Split(","c).Length >= 4 Then ShowStartDateArr.Add(If(datas.Split(","c)(3) = "0.0", "-", datas.Split(","c)(3)))
                                    End If

                                    If i = 1 And datas.Split(","c).Length > 0 And datas.Split(","c)(0) <> "1900/01/01" Then
                                        If datas.Split(","c).Length >= 1 Then ShowEndDateArr.Add(If(datas.Split(","c)(0) = "1900/01/01", "-", datas.Split(","c)(0)))
                                        If datas.Split(","c).Length >= 2 Then ShowEndDateArr.Add(If(datas.Split(","c)(1) = "0.0", "-", datas.Split(","c)(1)))
                                        If datas.Split(","c).Length >= 3 Then ShowEndDateArr.Add(If(datas.Split(","c)(2) = "0.0", "-", datas.Split(","c)(2)))
                                        If datas.Split(","c).Length >= 4 Then ShowEndDateArr.Add(If(datas.Split(","c)(3) = "0.0", "-", datas.Split(","c)(3)))
                                    End If
                                Next
                                htmlTableString = subTableGenByArr(ShowStartDateArr, ShowEndDateArr)
                                If htmlTableString <> "" Then
                                    litTable.Text = htmlTableString
                                    pnlCalcTotalTime.Visible = True
                                End If

                                If txtMealTime.Text <> "" AndAlso Convert.ToDouble(txtMealTime.Text) > 0 And MealFlag.Checked = True Then
                                    txtOTTotalDescript.Text = (If(txtMealTime.Text <> "" AndAlso Convert.ToDouble(txtMealTime.Text) > 0 And MealFlag.Checked = True, "(已扣除用餐時數" + txtMealTime.Text + "分鐘)", ""))
                                    txtOTTotalDescript.Visible = True
                                Else
                                    txtOTTotalDescript.Visible = False
                                End If

                                Dim meal As String = If(MealFlag.Checked = False, "0", txtMealTime.Text)
                                meal = If(String.IsNullOrEmpty(meal), "0", txtMealTime.Text)
                                lblOTTotalTime.Text = Convert.ToDouble((cntTotal - Convert.ToDouble(meal)) / 60).ToString("0.0")
                                lblOTTotalTime.Visible = True
                            Else
                                Bsp.Utility.ShowMessage(Me, returnPeriodCount)
                                initalData()
                                Return
                            End If
                        End If
                    End If
                    '********************************
                    'End Using
                End If
            Else
                '跨日
                getCntStartAndCntEnd(cntStart, cntEnd)
                If txtMealTime.Text.Trim = "" Then
                    txtMealTime.Text = "0"
                    Return
                End If
                If txtMealTime.Text <> "" And Convert.ToDouble(txtMealTime.Text.Trim) >= (cntEnd + cntStart) Then
                    txtMealTime.Focus()
                    Bsp.Utility.ShowMessage(Me, "用餐時數超過加班時數")
                Else

                    '計算時段
                    '********************************
                    Dim returnPeriodCount As String = ""

                    Dim bOTTimeStart As Boolean = Not String.IsNullOrEmpty(ucOTStartTime.ucDefaultSelectedMM) AndAlso Not String.IsNullOrEmpty(ucOTStartTime.ucDefaultSelectedHH) AndAlso ucOTStartTime.ucDefaultSelectedHH <> "請選擇" AndAlso ucOTStartTime.ucDefaultSelectedMM <> "請選擇"
                    Dim bOTTimeEnd As Boolean = Not String.IsNullOrEmpty(ucOTEndTime.ucDefaultSelectedMM) And Not String.IsNullOrEmpty(ucOTEndTime.ucDefaultSelectedHH) AndAlso ucOTEndTime.ucDefaultSelectedHH <> "請選擇" AndAlso ucOTEndTime.ucDefaultSelectedMM <> "請選擇"
                    If bOTTimeStart AndAlso bOTTimeEnd Then
                        Dim ShowStartDateArr, ShowEndDateArr As New ArrayList    '顯示用
                        Dim StartDateArr, EndDateArr As New ArrayList   '計算時段用
                        Dim sOTTimeStart = ucOTStartTime.ucDefaultSelectedHH + ucOTStartTime.ucDefaultSelectedMM
                        Dim sOTTimeEnd = ucOTEndTime.ucDefaultSelectedHH + ucOTEndTime.ucDefaultSelectedMM
                        Dim iOTTimeStart As Integer = 0
                        Dim iOTTimeEnd As Integer = 0
                        If (Integer.TryParse(sOTTimeStart, iOTTimeStart) And (Integer.TryParse(sOTTimeEnd, iOTTimeEnd))) Then
                            Dim smealFlag As String = If(MealFlag.Checked, "1", "0")
                            Dim sMealTime As String = If(String.IsNullOrEmpty(txtMealTime.Text), "0", txtMealTime.Text.Trim)
                            Dim iMealTime As Integer = 0
                            Integer.TryParse(sMealTime, iMealTime)

                            If iOTTimeStart = 0 AndAlso iOTTimeEnd = 0 Then
                                Return
                            End If

                            StartDateArr.Add(ucOTStartDate.DateText)
                            StartDateArr.Add(sOTTimeStart)
                            StartDateArr.Add("2359")

                            EndDateArr.Add(ucOTEndDate.DateText)
                            EndDateArr.Add("0")
                            EndDateArr.Add(sOTTimeEnd)

                            Dim bPeriodCount As Boolean = objOV42.PeriodCount("OverTimeDeclaration", txtOTEmpID.Text.Trim, cntStart, cntEnd, StartDateArr, EndDateArr, iMealTime, smealFlag, "", returnPeriodCount)
                            If bPeriodCount And String.IsNullOrEmpty(returnPeriodCount) <> True And returnPeriodCount.Split(";"c).Length > 0 Then
                                Dim sReturnPeriodList = returnPeriodCount.Split(";"c)

                                For i = 0 To (sReturnPeriodList.Length - 1)
                                    Dim datas = sReturnPeriodList(i)
                                    If i = 0 And datas.Split(","c).Length > 0 And datas.Split(","c)(0) <> "1900/01/01" Then
                                        If datas.Split(","c).Length >= 1 Then ShowStartDateArr.Add(If(datas.Split(","c)(0) = "1900/01/01", "-", datas.Split(","c)(0)))
                                        If datas.Split(","c).Length >= 2 Then ShowStartDateArr.Add(If(datas.Split(","c)(1) = "0.0", "-", datas.Split(","c)(1)))
                                        If datas.Split(","c).Length >= 3 Then ShowStartDateArr.Add(If(datas.Split(","c)(2) = "0.0", "-", datas.Split(","c)(2)))
                                        If datas.Split(","c).Length >= 4 Then ShowStartDateArr.Add(If(datas.Split(","c)(3) = "0.0", "-", datas.Split(","c)(3)))
                                    End If

                                    If i = 1 And datas.Split(","c).Length > 0 And datas.Split(","c)(0) <> "1900/01/01" Then
                                        If datas.Split(","c).Length >= 1 Then ShowEndDateArr.Add(If(datas.Split(","c)(0) = "1900/01/01", "-", datas.Split(","c)(0)))
                                        If datas.Split(","c).Length >= 2 Then ShowEndDateArr.Add(If(datas.Split(","c)(1) = "0.0", "-", datas.Split(","c)(1)))
                                        If datas.Split(","c).Length >= 3 Then ShowEndDateArr.Add(If(datas.Split(","c)(2) = "0.0", "-", datas.Split(","c)(2)))
                                        If datas.Split(","c).Length >= 4 Then ShowEndDateArr.Add(If(datas.Split(","c)(3) = "0.0", "-", datas.Split(","c)(3)))
                                    End If
                                Next
                                htmlTableString = subTableGenByArr(ShowStartDateArr, ShowEndDateArr)
                                If htmlTableString <> "" Then
                                    litTable.Text = htmlTableString
                                    pnlCalcTotalTime.Visible = True
                                End If

                                If txtMealTime.Text <> "" AndAlso Convert.ToDouble(txtMealTime.Text) > 0 And MealFlag.Checked = True Then
                                    txtOTTotalDescript.Text = (If(txtMealTime.Text <> "" AndAlso Convert.ToDouble(txtMealTime.Text) > 0 And MealFlag.Checked = True, "(已扣除用餐時數" + txtMealTime.Text + "分鐘)", ""))
                                    txtOTTotalDescript.Visible = True
                                Else
                                    txtOTTotalDescript.Visible = False
                                End If

                                Dim meal As String = If(MealFlag.Checked = False, "0", txtMealTime.Text)
                                meal = If(String.IsNullOrEmpty(meal), "0", txtMealTime.Text)
                                lblOTTotalTime.Text = Convert.ToDouble((cntEnd + cntStart - Convert.ToDouble(meal)) / 60).ToString("0.0")
                                lblOTTotalTime.Visible = True
                            Else
                                Bsp.Utility.ShowMessage(Me, returnPeriodCount)
                                initalData()
                                Return
                            End If
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' 不跨日時數(分鐘)
    ''' </summary>
    ''' <param name="cntTotal"></param>
    ''' <remarks></remarks>
    Private Sub getCntTotal(ByRef cntTotal As Double)
        cntTotal = 0
        Try
            If ucOTStartTime.ucDefaultSelectedHH <> "" AndAlso ucOTStartTime.ucDefaultSelectedMM <> "" AndAlso ucOTEndTime.ucDefaultSelectedHH <> "" AndAlso ucOTEndTime.ucDefaultSelectedMM <> "" AndAlso ucOTStartTime.ucDefaultSelectedHH <> "請選擇" And ucOTStartTime.ucDefaultSelectedMM <> "請選擇" And ucOTEndTime.ucDefaultSelectedHH <> "請選擇" And ucOTEndTime.ucDefaultSelectedMM <> "請選擇" Then
                'If ucOTStartDate.DateText = ucOTEndDate.DateText AndAlso (ucOTEndTime.ucSelectedTime = "23:59") Then    '如果是同一天且加班結束時間選擇為23:59(也就是24:00)
                '    cntTotal = 1440 - (Convert.ToDouble(ucOTStartTime.ucDefaultSelectedHH) * 60 + Convert.ToDouble(ucOTStartTime.ucDefaultSelectedMM))
                'Else
                cntTotal = (Convert.ToDouble(ucOTEndTime.ucDefaultSelectedHH) * 60 + Convert.ToDouble(ucOTEndTime.ucDefaultSelectedMM)) - (Convert.ToDouble(ucOTStartTime.ucDefaultSelectedHH) * 60 + Convert.ToDouble(ucOTStartTime.ucDefaultSelectedMM))
                'End If
            End If
        Catch ex As Exception
            Debug.Print("getCntTotal()=>" + ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' 跨日時數(分鐘)
    ''' </summary>
    ''' <param name="cntStart"></param>
    ''' <param name="cntEnd"></param>
    ''' <remarks></remarks>
    Private Sub getCntStartAndCntEnd(ByRef cntStart As Double, ByRef cntEnd As Double)
        cntStart = 0
        cntEnd = 0
        Try
            If ucOTStartTime.ucDefaultSelectedHH <> "請選擇" AndAlso ucOTStartTime.ucDefaultSelectedMM <> "請選擇" AndAlso ucOTEndTime.ucDefaultSelectedHH <> "請選擇" AndAlso ucOTEndTime.ucDefaultSelectedMM <> "請選擇" AndAlso ucOTStartTime.ucDefaultSelectedHH <> "" AndAlso ucOTStartTime.ucDefaultSelectedMM <> "" AndAlso ucOTEndTime.ucDefaultSelectedHH <> "" AndAlso ucOTEndTime.ucDefaultSelectedMM <> "" Then
                cntStart = (23 - (Convert.ToDouble(ucOTStartTime.ucDefaultSelectedHH))) * 60 + (60 - Convert.ToDouble(ucOTStartTime.ucDefaultSelectedMM))
                cntEnd = (Convert.ToDouble(ucOTEndTime.ucDefaultSelectedHH)) * 60 + Convert.ToDouble(ucOTEndTime.ucDefaultSelectedMM)
            End If
        Catch ex As Exception
            Debug.Print("getCntStartAndCntEnd()=>" + ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' 檢查加班時數(含已核准)申報時數是否已超過上限
    ''' </summary>
    ''' <param name="message"></param>
    ''' <returns></returns>
    ''' <remarks>參數檔</remarks>
    Private Function checkOverTimeIsOver(ByRef message As String) As Boolean
        Dim objOV42 As New OV4_2
        Dim result As Boolean = True
        Dim cntStart As Double = 0
        Dim cntEnd As Double = 0
        Dim cntTotal As Double = 0
        Dim dayNLimit As Double = 0
        Dim dayHLimit As Double = 0
        Dim hr As Double = 0
        Dim datecheck As Double = 0
        Dim sb As New StringBuilder
        Dim iMealTime As Double = 0
        Double.TryParse(txtMealTime.Text.Trim, iMealTime)

        message = ""

        dayNLimit = Convert.ToDouble(_dtPara.Rows(0).Item("DayLimitHourN").ToString())      '平日可申報
        dayHLimit = Convert.ToDouble(_dtPara.Rows(0).Item("DayLimitHourH").ToString())      '假日可申報
        If ucOTStartDate.DateText = ucOTEndDate.DateText Then    '不跨日
            sb.Clear()

            sb.Append(" SELECT ISNULL(SUM(A.OTTotalTime),0) AS TotalTime FROM(")
            sb.Append(" SELECT SUM(OTTotalTime)-SUM(MealTime) AS OTTotalTime FROM OverTimeAdvance WHERE OTStatus in('2','3') AND OTCompID=" + Bsp.Utility.Quote(hidCompID.Value.Trim) + "  AND OTEmpID=" + Bsp.Utility.Quote(txtOTEmpID.Text) + " AND OTStartDate=" + Bsp.Utility.Quote(ucOTStartDate.DateText) + " AND OTEndDate=" + Bsp.Utility.Quote(ucOTEndDate.DateText))
            sb.Append(" AND OTTxnID NOT IN")
            sb.Append(" (SELECT OTFromAdvanceTxnId FROM OverTimeDeclaration WHERE OTCompID=" + Bsp.Utility.Quote(hidCompID.Value.Trim) + " AND OTEmpID=" + Bsp.Utility.Quote(txtOTEmpID.Text) + " AND OTStatus in ('2','3') AND OTFromAdvanceTxnId<>'')")
            sb.Append(" UNION ALL")
            sb.Append(" SELECT SUM(OTTotalTime)-SUM(MealTime) AS OTTotalTime FROM OverTimeDeclaration WHERE OTStatus in('2','3') AND OTCompID=" + Bsp.Utility.Quote(hidCompID.Value.Trim) + " AND OTEmpID=" + Bsp.Utility.Quote(txtOTEmpID.Text) + " AND OTStartDate=" + Bsp.Utility.Quote(ucOTStartDate.DateText) + " AND OTEndDate=" + Bsp.Utility.Quote(ucOTEndDate.DateText) + ") A ")

            Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, sb.ToString, "AattendantDB").Tables(0)
                If dt.Rows.Count > 0 Then
                    hr = Double.Parse(dt.Rows(0)("TotalTime").ToString())
                End If
            End Using
            getCntTotal(cntTotal)
            cntTotal -= iMealTime

            Dim blHo As Boolean = objOV42.CheckHolidayOrNot(ucOTStartDate.DateText)
            If Not blHo Then    '平日檢查
                If hr + cntTotal > (dayNLimit * 60) Then
                    message = "加班時數(含已核准)申報時數已超過上限" + _dtPara.Rows(0).Item("DayLimitHourN").ToString() + "小時" & vbNewLine
                    result = False
                End If
            Else    '假日
                If hr + cntTotal > (dayHLimit * 60) Then
                    message = "加班時數(含已核准)申報時數已超過上限" + _dtPara.Rows(0).Item("DayLimitHourH").ToString() + "小時" & vbNewLine
                    result = False
                End If
            End If
        Else    '跨日
            getCntStartAndCntEnd(cntStart, cntEnd)

            '資料庫的加班總時數
            Dim dtStart As DataTable = Nothing
            Dim dtEnd As DataTable = Nothing

            Dim hrStart As Double = 0
            Dim hrEnd As Double = 0
            Dim hrStart1 As Double = 0
            Dim hrEnd1 As Double = 0

            sb.Clear()
            sb.Append(" SELECT ISNULL(SUM(A.OTTotalTime),0) AS TotalTime FROM(")
            sb.Append(" SELECT SUM(OTTotalTime)-SUM(MealTime) AS OTTotalTime FROM OverTimeAdvance WHERE OTStatus in('2','3') AND OTCompID=" + Bsp.Utility.Quote(hidCompID.Value.Trim) + " AND OTEmpID=" + Bsp.Utility.Quote(txtOTEmpID.Text) + " AND OTStartDate=" + Bsp.Utility.Quote(ucOTStartDate.DateText) + " AND OTEndDate=" + Bsp.Utility.Quote(ucOTStartDate.DateText))
            sb.Append(" AND OTTxnID NOT IN")
            sb.Append(" (SELECT OTFromAdvanceTxnId FROM OverTimeDeclaration WHERE OTCompID=" + Bsp.Utility.Quote(hidCompID.Value.Trim) + " AND OTEmpID=" + Bsp.Utility.Quote(txtOTEmpID.Text) + " AND OTStatus in ('2','3') AND OTFromAdvanceTxnId<>'')")
            sb.Append(" UNION ALL")
            sb.Append(" SELECT SUM(OTTotalTime)-SUM(MealTime) AS OTTotalTime FROM OverTimeDeclaration WHERE OTStatus in('2','3') AND OTCompID=" + Bsp.Utility.Quote(hidCompID.Value.Trim) + " AND OTEmpID=" + Bsp.Utility.Quote(txtOTEmpID.Text) + " AND OTStartDate=" + Bsp.Utility.Quote(ucOTStartDate.DateText) + " AND OTEndDate=" + Bsp.Utility.Quote(ucOTStartDate.DateText) + ") A ")
            dtStart = Bsp.DB.ExecuteDataSet(CommandType.Text, sb.ToString, "AattendantDB").Tables(0)

            sb.Clear()
            sb.Append(" SELECT ISNULL(SUM(A.OTTotalTime),0) AS TotalTime FROM(")
            sb.Append(" SELECT SUM(OTTotalTime)-SUM(MealTime) AS OTTotalTime FROM OverTimeAdvance WHERE OTStatus in('2','3') AND OTCompID=" + Bsp.Utility.Quote(hidCompID.Value.Trim) + " AND OTEmpID=" + Bsp.Utility.Quote(txtOTEmpID.Text) + " AND OTStartDate=" + Bsp.Utility.Quote(ucOTEndDate.DateText) + " AND OTEndDate=" + Bsp.Utility.Quote(ucOTEndDate.DateText))
            sb.Append(" AND OTTxnID NOT IN")
            sb.Append(" (SELECT OTFromAdvanceTxnId FROM OverTimeDeclaration WHERE OTCompID=" + Bsp.Utility.Quote(hidCompID.Value.Trim) + " AND OTEmpID=" + Bsp.Utility.Quote(txtOTEmpID.Text) + " AND OTStatus in ('2','3') AND OTFromAdvanceTxnId<>'')")
            sb.Append(" UNION ALL")
            sb.Append(" SELECT SUM(OTTotalTime)-SUM(MealTime) AS OTTotalTime FROM OverTimeDeclaration WHERE OTStatus in('2','3') AND OTCompID=" + Bsp.Utility.Quote(hidCompID.Value.Trim) + " AND OTEmpID=" + Bsp.Utility.Quote(txtOTEmpID.Text) + " AND OTStartDate=" + Bsp.Utility.Quote(ucOTEndDate.DateText) + " AND OTEndDate=" + Bsp.Utility.Quote(ucOTEndDate.DateText) + ") A ")
            dtEnd = Bsp.DB.ExecuteDataSet(CommandType.Text, sb.ToString, "AattendantDB").Tables(0)

            If dtStart.Rows.Count > 0 Then
                hrStart = Double.Parse(dtStart.Rows(0).Item("TotalTime").ToString())
            End If

            If (dtEnd.Rows.Count > 0) Then
                hrEnd = Double.Parse(dtEnd.Rows(0).Item("TotalTime").ToString())
            End If

            '國定假日若在平日是可以加班的，不算在連續加班
            Dim StartNHo As String = objOV42.QueryColumn("Week", _eHRMSDB_ITRD + ".[dbo].[Calendar]", " AND CompID =" + Bsp.Utility.Quote(UserProfile.SelectCompRoleID.Trim) + " AND  CONVERT(CHAR(10),SysDate, 111) = " + Bsp.Utility.Quote(ucOTStartDate.DateText))
            Dim EndNHo As String = objOV42.QueryColumn("Week", _eHRMSDB_ITRD + ".[dbo].[Calendar]", " AND CompID =" + Bsp.Utility.Quote(UserProfile.SelectCompRoleID.Trim) + " AND  CONVERT(CHAR(10),SysDate, 111) = " + Bsp.Utility.Quote(ucOTEndDate.DateText))
            Dim StartWeek As Integer = Convert.ToInt32(StartNHo)    '加班開始日期是星期幾
            Dim EndWeek As Integer = Convert.ToInt32(EndNHo)        '加班結束日期是星期幾

            Dim blStartHo As Boolean = objOV42.CheckHolidayOrNot(ucOTStartDate.DateText)
            Dim blEndHo As Boolean = objOV42.CheckHolidayOrNot(ucOTEndDate.DateText)
            Dim mealOver As String = objOV42.MealJudge(cntStart, iMealTime)

            If blStartHo = False AndAlso blEndHo = False Then  '平日跨平日
                If hrStart + (cntStart - Convert.ToDouble(mealOver.Split(",")(1))) > (dayNLimit * 60) Then
                    message += ucOTStartDate.DateText + "(含已核准)申報時數已超過上限" & vbNewLine
                    result = False
                End If
                If hrEnd + (cntEnd - Convert.ToDouble(mealOver.Split(",")(3))) > (dayNLimit * 60) Then
                    message += ucOTEndDate.DateText + "(含已核准)申報時數已超過上限" & vbNewLine
                    result = False
                End If
            ElseIf blStartHo = True AndAlso blEndHo = True Then   '假日跨假日
                If OVBusinessCommon.IsNationalHoliday(ucOTStartDate.DateText) Then  '國定假日跨假日不算連續加班
                    If StartWeek <> 6 OrElse StartWeek <> 7 Then
                        If (hrStart + (cntStart - Convert.ToDouble(mealOver.Split(",")(1))) > (dayHLimit * 60)) Then
                            message += ucOTStartDate.DateText + "(含已核准)申報時數已超過上限" & vbNewLine
                            result = False
                        End If
                        If (hrEnd + (cntEnd - Convert.ToDouble(mealOver.Split(",")(3))) > (dayHLimit * 60)) Then
                            message += ucOTEndDate.DateText + "(含已核准)申報時數已超過上限" & vbNewLine
                            result = False
                        End If
                    Else
                        'message = "不能假日連續加班"
                        If (hrStart + (cntStart - Convert.ToDouble(mealOver.Split(",")(1))) > (dayHLimit * 60)) Then
                            message += ucOTStartDate.DateText + "(含已核准)申報時數已超過上限" & vbNewLine
                            result = False
                        End If
                        If (hrEnd + (cntEnd - Convert.ToDouble(mealOver.Split(",")(3))) > (dayHLimit * 60)) Then
                            message += ucOTEndDate.DateText + "(含已核准)申報時數已超過上限" & vbNewLine
                            result = False
                        End If
                        result = False
                    End If
                ElseIf OVBusinessCommon.IsNationalHoliday(ucOTEndDate.DateText) Then  '假日跨國定假日不算連續加班
                    If StartWeek <> 6 Or StartWeek <> 7 Then
                        If (hrStart + (cntStart - Convert.ToDouble(mealOver.Split(",")(1))) > (dayHLimit * 60)) Then
                            message += ucOTStartDate.DateText + "(含已核准)申報時數已超過上限" & vbNewLine
                            result = False
                        End If
                        If (hrEnd + (cntEnd - Convert.ToDouble(mealOver.Split(",")(3))) > (dayHLimit * 60)) Then
                            message += ucOTEndDate.DateText + "(含已核准)申報時數已超過上限" & vbNewLine
                            result = False
                        End If
                    Else
                        'message = "不能假日連續加班"
                        If (hrStart + (cntStart - Convert.ToDouble(mealOver.Split(",")(1))) > (dayHLimit * 60)) Then
                            message += ucOTStartDate.DateText + "(含已核准)申報時數已超過上限" & vbNewLine
                            result = False
                        End If
                        If (hrEnd + (cntEnd - Convert.ToDouble(mealOver.Split(",")(3))) > (dayHLimit * 60)) Then
                            message += ucOTEndDate.DateText + "(含已核准)申報時數已超過上限" & vbNewLine
                            result = False
                        End If
                        result = False
                    End If
                Else
                    'message = "不能假日連續加班"
                    If (hrStart + (cntStart - Convert.ToDouble(mealOver.Split(",")(1))) > (dayHLimit * 60)) Then
                        message += ucOTStartDate.DateText + "(含已核准)申報時數已超過上限" & vbNewLine
                        result = False
                    End If
                    If (hrEnd + (cntEnd - Convert.ToDouble(mealOver.Split(",")(3))) > (dayHLimit * 60)) Then
                        message += ucOTEndDate.DateText + "(含已核准)申報時數已超過上限" & vbNewLine
                        result = False
                    End If
                    result = False
                End If
            ElseIf (blStartHo = False AndAlso blEndHo = True) Then  '平日跨假日
                If hrStart + (cntStart - Convert.ToDouble(mealOver.Split(",")(1))) > (dayNLimit * 60) Then
                    message += ucOTStartDate.DateText + "(含已核准)申報時數已超過上限" & vbNewLine
                    result = False
                End If
                If hrEnd + (cntEnd - Convert.ToDouble(mealOver.Split(",")(3))) > (dayHLimit * 60) Then
                    message += ucOTEndDate.DateText + "(含已核准)申報時數已超過上限" & vbNewLine
                    result = False
                End If
            ElseIf (blStartHo = True AndAlso blEndHo = False) Then  '假日跨平日
                If hrStart + (cntStart - Convert.ToDouble(mealOver.Split(",")(1))) > (dayHLimit * 60) Then
                    message += ucOTStartDate.DateText + "(含已核准)申報時數已超過上限" & vbNewLine
                    result = False
                End If
                If hrEnd + (cntEnd - Convert.ToDouble(mealOver.Split(",")(3))) > (dayNLimit * 60) Then
                    message += ucOTEndDate.DateText + "(含已核准)申報時數已超過上限" & vbNewLine
                    result = False
                End If
            End If

            '釋放資源
            Try
                dtStart.Clear()
                dtStart.Dispose()
                dtEnd.Clear()
                dtEnd.Dispose()
            Catch ex As Exception
                Debug.Print("釋放DataTable Err-->" + ex.Message)
            End Try
        End If
        Return result
    End Function

#End Region

End Class
