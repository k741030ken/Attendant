'****************************************************
'功能說明：加班管理-事後申報-修改/明細
'建立日期：2017.02.06
'修改日期：2017.03.20
'****************************************************

Imports System                  'DateTime.Parse
Imports System.Data
Imports System.Data.Common
Imports System.Globalization
Imports System.Diagnostics      'For Debug.Print()
Imports SinoPac.WebExpress.Common
Imports SinoPac.WebExpress.DAO
Imports System.Web
Imports System.Web.HttpResponse

Partial Class OV_OV1102
    Inherits PageBase

#Region "全域變數"

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

    Private Property Config_AattendantDBName As String
        Get
            Dim result As String = OVBusinessCommon.AattendantDBName
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

    Public Property _strAttachID As String
        Get
            If ViewState.Item("_strAttachID") Is Nothing Then ViewState.Item("_strAttachID") = String.Empty

            Return ViewState.Item("_strAttachID").ToString()
        End Get
        Set(value As String)
            ViewState.Item("_strAttachID") = value
        End Set
    End Property

    Public Property _OTStartDate As String
        Get
            If ViewState.Item("_OTStartDate") Is Nothing Then ViewState.Item("_OTStartDate") = String.Empty

            Return ViewState.Item("_OTStartDate").ToString()
        End Get
        Set(value As String)
            ViewState.Item("_OTStartDate") = value
        End Set
    End Property

    Public Property _OTEndDate As String
        Get
            If ViewState.Item("_OTEndDate") Is Nothing Then ViewState.Item("_OTEndDate") = String.Empty

            Return ViewState.Item("_OTEndDate").ToString()
        End Get
        Set(value As String)
            ViewState.Item("_OTEndDate") = value
        End Set
    End Property

    Public Property _OTStartTime As String
        Get
            If ViewState.Item("_OTStartTime") Is Nothing Then ViewState.Item("_OTStartTime") = String.Empty

            Return ViewState.Item("_OTStartTime").ToString()
        End Get
        Set(value As String)
            ViewState.Item("_OTStartTime") = value
        End Set
    End Property

    Public Property _OTEndTime As String
        Get
            If ViewState.Item("_OTEndTime") Is Nothing Then ViewState.Item("_OTEndTime") = String.Empty

            Return ViewState.Item("_OTEndTime").ToString()
        End Get
        Set(value As String)
            ViewState.Item("_OTEndTime") = value
        End Set
    End Property

    Public Property _EmpID As String
        Get
            If ViewState.Item("_EmpID") Is Nothing Then ViewState.Item("_EmpID") = String.Empty

            Return ViewState.Item("_EmpID").ToString()
        End Get
        Set(value As String)
            ViewState.Item("_EmpID") = value
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

    Public Property _OTFormNO As String
        Get
            If ViewState.Item("_OTFormNO") Is Nothing Then ViewState.Item("_OTFormNO") = String.Empty

            Return ViewState.Item("_OTFormNO").ToString()
        End Get
        Set(value As String)
            ViewState.Item("_OTFormNO") = value
        End Set
    End Property

    Public Property _OTFromAdvanceTxnId As String
        Get
            If ViewState.Item("_OTFromAdvanceTxnId") Is Nothing Then ViewState.Item("_OTFromAdvanceTxnId") = String.Empty

            Return ViewState.Item("_OTFromAdvanceTxnId").ToString()
        End Get
        Set(value As String)
            ViewState.Item("_OTFromAdvanceTxnId") = value
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
            Case "btnUpdate"    '暫存
                If ViewState.Item("DoUpdate") = "Y" Then
                    ViewState("Param") = Param
                    If checkData("1") Then
                        funPopupNotify()
                        'If SaveData() Then
                        '    GoBack()
                        'End If
                    End If
                End If
            Case "btnExecutes"  '送簽
                ViewState("Param") = Param
                If checkData("2") Then  '檢查日期是否為未來日期
                    funPopupNotify()
                End If
                'DoExecute()
            Case "btnCancel"    '清除
                If ViewState.Item("DoUpdate") = "Y" Then LoadData() 'ClearData()
            Case "btnActionX"   '返回
                GoBack()
        End Select
    End Sub
#End Region

#Region "畫面邏輯"

    ''' <summary>
    ''' 起始頁邏輯處理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Bsp.Utility.FillDDL(ddlCodeCName, "AattendantDB", "AT_CodeMap", "Code", "CodeCName", Bsp.Utility.DisplayType.OnlyName, "", "AND TabName = 'OverTime' AND FldName = 'OverTimeType' AND NotShowFlag = '0'", "ORDER BY Code") '加班類型
            ddlCodeCName.Items.Insert(0, New ListItem("　- -請選擇- -", ""))

            '授權人公司代碼
            'hidOTRegisterCompID.Value = UserProfile.ActCompID.Trim

            hidUserComp.Value = UserProfile.ActCompID.Trim
            hidUserID.Value = UserProfile.UserID.Trim

            callHandlerUrl.Value = Bsp.Utility.getAppSetting("AattendantWebPath")
        End If
    End Sub

    ''' <summary>
    ''' 接收上一頁傳遞來的資料
    ''' </summary>
    ''' <param name="ti"></param>
    ''' <remarks></remarks>
    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

        If ht.ContainsKey("SelectedOTCompID") And ht.ContainsKey("SelectedOTEmpID") And ht.ContainsKey("SelectedOTFormNO") Then
            _EmpID = If(ht("SelectedOTEmpID") <> Nothing, ht("SelectedOTEmpID").ToString(), "")
            _OTStartDate = If(ht("SelectedOTStartDate") <> Nothing, ht("SelectedOTStartDate"), "")
            _OTEndDate = If(ht("SelectedOTEndDate") <> Nothing, ht("SelectedOTEndDate").ToString(), "")
            _OTStartTime = If(ht("SelectedOTStartTime") <> Nothing, (ht("SelectedOTStartTime").ToString()).Replace(":", ""), "")
            _OTEndTime = If(ht("SelectedOTEndTime") <> Nothing, (ht("SelectedOTEndTime").ToString()).Replace(":", ""), "")
            _OTFormNO = If(ht("SelectedOTFormNO") <> Nothing, ht("SelectedOTFormNO").ToString(), "")
            _OTSeq = If(ht("SelectedOTSeq") <> Nothing, ht("SelectedOTSeq").ToString(), "")
            _OTTxnID = If(ht("SelectedOTTxnID") <> Nothing, ht("SelectedOTTxnID").ToString(), "")
            _OTFromAdvanceTxnId = If(ht("OTFromAdvanceTxnId") <> Nothing, ht("OTFromAdvanceTxnId").ToString(), "")
        Else
            Return
        End If

        If ht.ContainsKey("DoUpdate") Then
            If ht("DoUpdate").ToString() = "Y" Then
                '是修改
                ViewState.Item("DoUpdate") = "Y"
                'LastChgPanel.Visible = False
                btnDownloadAttach.Visible = False
                btnUploadAttach.Visible = True
            ElseIf ht("DoUpdate").ToString() = "N" Then
                '是明細
                ViewState.Item("DoUpdate") = "N"
                'LastChgPanel.Visible = True
                btnDownloadAttach.Visible = True
                btnUploadAttach.Visible = False
            Else
                Bsp.Utility.ShowMessage(Me, "ViewState Item Transport Err")
                GoBack()
            End If
        End If

        LoadData()
    End Sub

    ''' <summary>
    ''' 計算時段與時間合計
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub LoadData()
        Dim htmlTableString As String = ""
        Dim ShowStartDateArr, ShowEndDateArr As New ArrayList    '顯示用
        Dim StartDateArr, EndDateArr As New ArrayList   '計算時段用
        Dim objOV11 As New OV1_2

        Dim cntStart As Double = 0
        Dim cntEnd As Double = 0
        Dim cntTotal As Double = 0

        '畫面長出資料
        subGetData()

        Dim sOTTimeStart As String = _OTStartTime
        Dim sOTTimeEnd As String = _OTEndTime

        If _OTStartDate = _OTEndDate Then
            cntTotal = (Convert.ToDouble(_OTEndTime.Substring(0, 2)) * 60 + Convert.ToDouble(_OTEndTime.Substring(2, 2))) - (Convert.ToDouble(_OTStartTime.Substring(0, 2)) * 60 + Convert.ToDouble(_OTStartTime.Substring(2, 2)))

            '計算時段
            Dim returnPeriodCount As String = ""

            Dim iOTTimeStart As Integer = 0
            Dim iOTTimeEnd As Integer = 0
            If Integer.TryParse(sOTTimeStart, iOTTimeStart) AndAlso Integer.TryParse(sOTTimeEnd, iOTTimeEnd) Then
                Dim mealFlag As String = If(chkMealFlag.Checked, "1", "0")
                Dim sMealTime As String = If(String.IsNullOrEmpty(txtMealTime.Text), "0", txtMealTime.Text.Trim())
                Dim iMealTime As Integer = 0
                Integer.TryParse(sMealTime, iMealTime)

                StartDateArr.Add(ucOTStartDate.DateText)
                StartDateArr.Add(iOTTimeStart.ToString())
                StartDateArr.Add(iOTTimeEnd.ToString())

                EndDateArr.Add("1900/01/01")
                EndDateArr.Add("0")
                EndDateArr.Add("0")

                'public bool PeriodCount(string table, string strOTEmpID, double cntStart, double cntEnd, int StartbeginTime, int StartendTime, string StartDate, int EndbeginTime, int EndendTime, string EndDate, double MealTime, string MealFlag, string ottxnid, out string reMsg)//跨日的時段計算
                Dim bPeriodCount As Boolean = objOV11.PeriodCount("OverTimeDeclaration", _EmpID, cntTotal, 0, StartDateArr, EndDateArr, _
                 iMealTime, mealFlag, _OTTxnID, returnPeriodCount)

                If bPeriodCount AndAlso Not String.IsNullOrEmpty(returnPeriodCount) AndAlso returnPeriodCount.Split(";"c).Length > 0 Then
                    Dim sReturnPeriodList = returnPeriodCount.Split(";"c)
                    For i = 0 To sReturnPeriodList.Length - 1
                        Dim datas = sReturnPeriodList(i)
                        'ViewState("dataOne") = datas

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

                    txtOTTotalDescript.Text = If((txtMealTime.Text <> "" AndAlso Convert.ToDouble(txtMealTime.Text) > 0 AndAlso chkMealFlag.Checked = True), "(已扣除用餐時數" + txtMealTime.Text + "分鐘)", "")
                    txtOTTotalDescript.Visible = True
                    Dim meal As String = If((chkMealFlag.Checked = False), "0", txtMealTime.Text)
                    meal = If((String.IsNullOrEmpty(meal)), "0", txtMealTime.Text)
                    lblOTTotalTime.Text = Convert.ToDouble((cntTotal - Convert.ToDouble(meal)) / 60).ToString("0.0")
                    lblOTTotalTime.Visible = True
                Else
                    Bsp.Utility.ShowMessage(Me, returnPeriodCount)
                    initalData()
                    Return
                End If
            End If
        Else    '跨日
            cntStart = (23 - (Convert.ToDouble(_OTStartTime.Substring(0, 2)))) * 60 + (60 - Convert.ToDouble(_OTStartTime.Substring(2, 2)))
            cntEnd = (Convert.ToDouble(_OTEndTime.Substring(0, 2))) * 60 + Convert.ToDouble(_OTEndTime.Substring(2, 2))
            '計算時段
            Dim returnPeriodCount As String = ""

            Dim iOTTimeStart As Integer = 0
            Dim iOTTimeEnd As Integer = 0
            If Integer.TryParse(sOTTimeStart, iOTTimeStart) AndAlso Integer.TryParse(sOTTimeEnd, iOTTimeEnd) Then
                Dim mealFlag As String = If(chkMealFlag.Checked, "1", "0")
                Dim sMealTime As String = If(String.IsNullOrEmpty(txtMealTime.Text), "0", txtMealTime.Text.Trim())
                Dim iMealTime As Integer = 0
                Integer.TryParse(sMealTime, iMealTime)

                StartDateArr.Add(ucOTStartDate.DateText)
                StartDateArr.Add(sOTTimeStart)
                StartDateArr.Add("2359")

                EndDateArr.Add(ucOTEndDate.DateText)
                EndDateArr.Add("0")
                EndDateArr.Add(sOTTimeEnd)

                Dim bPeriodCount As Boolean = objOV11.PeriodCount("OverTimeDeclaration", _EmpID, cntStart, cntEnd, StartDateArr, EndDateArr, iMealTime, mealFlag, _OTTxnID, returnPeriodCount)

                If bPeriodCount AndAlso Not String.IsNullOrEmpty(returnPeriodCount) AndAlso returnPeriodCount.Split(";"c).Length > 0 Then
                    Dim sReturnPeriodList = returnPeriodCount.Split(";"c)

                    For i = 0 To sReturnPeriodList.Length - 1
                        Dim datas = sReturnPeriodList(i)
                        'ViewState("dataTwo") = datas
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

                    txtOTTotalDescript.Text = If((txtMealTime.Text <> "" AndAlso Convert.ToDouble(txtMealTime.Text) > 0 AndAlso chkMealFlag.Checked = True), "(已扣除用餐時數" + txtMealTime.Text + "分鐘)", "")
                    txtOTTotalDescript.Visible = True
                    Dim meal As String = If((chkMealFlag.Checked = False), "0", txtMealTime.Text)
                    meal = If((String.IsNullOrEmpty(meal)), "0", txtMealTime.Text)
                    lblOTTotalTime.Text = Convert.ToDouble((cntEnd + cntStart - Convert.ToDouble(meal)) / 60).ToString("0.0")
                    lblOTTotalTime.Visible = True
                Else
                    Bsp.Utility.ShowMessage(Me, returnPeriodCount)
                    initalData()
                    Return
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' 取得並塞畫面資料
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub subGetData()
        Dim objOV11 As New OV1_2
        'Dim db As New DbHelper("AattendantDB")
        'Dim sb As CommandHelper = db.CreateCommandHelper()
        Dim sb As New StringBuilder
        _dtPara = objOV11.Json2DataTable(objOV11.QueryColumn("Para", "OverTimePara", " AND CompID = '" + UserProfile.SelectCompRoleID.Trim + "'"))
        sb.Append(" SELECT * FROM (")
        sb.Append(" SELECT OT.OTRegisterComp,OT.OTEmpID,OT.OTCompID,C.CompName,OT.DeptID,OT.DeptName,OT.OrganID,OT.OrganName,OT.OTFormNO,P.NameN,P.RankID,P.EmpDate,P.Sex,P.WorkSiteID,OT.OTRegisterID,PR.NameN AS RegisterNameN,OT.OTTxnID,OTT.CodeCName,ISNULL(AI.FileName,'') AS FileName,OT.MealFlag,isnull(OT.MealTime,0)+isnull(OT2.MealTime,0) AS MealTime, OT.OTSeq,")
        sb.Append(" OT.OTStatus,CASE OT.SalaryOrAdjust WHEN '1' THEN '轉薪資' WHEN '2' THEN '轉補休' END AS SalaryOrAdjustName,OT.OTAttachment,OT.OTTypeID, ")
        sb.Append(" (OT.OTStartDate+'~'+isnull(OT2.OTEndDate,OT.OTEndDate)) AS OTDate,")
        sb.Append(" (OT.OTStartTime+'~'+isnull(OT2.OTEndTime,OT.OTEndTime)) AS OTTime,OT.AdjustInvalidDate,OT.SalaryOrAdjust,")
        'sb.Append(" Convert(Decimal(10,1),Round(OT.OTTotalTime-Convert(Decimal(10,1),Convert(Decimal(10,2),OT.MealTime)/60)+isnull(OT2.OTTotalTime,0)-isnull(Convert(Decimal(10,1),Convert(Decimal(10,2),OT2.MealTime)/60),0),1)) AS OTTotalTime, ");
        sb.Append(" Convert(Decimal(10,1),((CAST(OT.OTTotalTime AS FLOAT)-CAST(OT.MealTime AS FLOAT))/CAST(60 AS FLOAT))+((CAST(ISNULL(OT2.OTTotalTime,0) AS FLOAT)-CAST(ISNULL(OT2.MealTime,0) AS FLOAT))/CAST(60 AS FLOAT))) AS OTTotalTime,")
        sb.Append(" OT.LastChgID,PL.NameN AS LastChgNameN,OT.LastChgDate,OT.OTReasonMemo")
        sb.Append(" FROM OverTimeDeclaration OT ")
        sb.Append(" LEFT JOIN OverTimeDeclaration OT2 on OT2.OTTxnID=OT.OTTxnID AND OT2.OTSeqNo=2 AND OT2.OverTimeFlag='1'")
        sb.Append(" LEFT JOIN " + _eHRMSDB_ITRD + ".[dbo].[Company] C ON C.CompID=OT.OTCompID AND C.InValidFlag = '0' And C.NotShowFlag = '0'")
        sb.Append(" LEFT JOIN " + _eHRMSDB_ITRD + ".[dbo].[Personal] P ON P.CompID = OT.OTCompID AND P.EmpID=OT.OTEmpID")
        sb.Append(" LEFT JOIN " + _eHRMSDB_ITRD + ".[dbo].[Personal] PR ON PR.CompID = OT.OTRegisterComp AND PR.EmpID=OT.OTRegisterID")
        sb.Append(" LEFT JOIN " + _eHRMSDB_ITRD + ".[dbo].[Personal] PL ON PL.CompID = OT.LastChgComp AND PL.EmpID=OT.LastChgID")
        sb.Append(" LEFT JOIN AttachInfo AI ON AI.AttachID IS NOT NULL AND AI.AttachID <> '' AND AI.AttachID = OT.OTAttachment  AND FileSize > 0")
        sb.Append(" LEFT JOIN AT_CodeMap AS OTT ON OT.OTTypeID = OTT.Code AND OTT.TabName='OverTime' AND OTT.FldName='OverTimeType'")
        sb.Append(" WHERE OT.OTSeqNo=1 AND OT.OverTimeFlag='1' AND OT.OTTxnID='" + _OTTxnID + "') A")
        'sb.Append(" WHERE OT.OTSeqNo=1 AND OT.OverTimeFlag='1' AND OT.OTSeq='" + _OTSeq + "') A")
        sb.Append(" WHERE 1=1 AND A.OTEmpID = '" + _EmpID + "'")
        sb.Append(" AND A.OTDate='" + _OTStartDate + "~" + _OTEndDate + "'")
        sb.Append(" AND A.OTTime='" + _OTStartTime + "~" + _OTEndTime + "'")

        Dim dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, sb.ToString, "AattendantDB").Tables(0)

        _dtPara = objOV11.Json2DataTable(objOV11.QueryColumn("Para", "OverTimePara", " AND CompID = '" + dt.Rows(0)("OTCompID").ToString().Trim + "'"))
        lblCompID.Text = dt.Rows(0)("OTCompID").ToString()
        lblCompName.Text = dt.Rows(0)("CompName").ToString()

        lblOTEmpID.Text = _EmpID
        lblOTEmpNameN.Text = dt.Rows(0)("NameN").ToString()

        lblDeptID.Text = dt.Rows(0)("DeptID").ToString()
        lblDeptName.Text = dt.Rows(0)("DeptName").ToString()

        '加班人科別
        lblOrganID.Text = dt.Rows(0)("OrganID").ToString()
        lblOrganName.Text = dt.Rows(0)("OrganName").ToString()

        lblOTRegisterID.Text = dt.Rows(0)("OTRegisterID").ToString()
        lblOTRegisterNameN.Text = dt.Rows(0)("RegisterNameN").ToString()
        hidOTRegisterCompID.Value = dt.Rows(0)("OTRegisterComp").ToString()

        ucOTStartDate.DateText = _OTStartDate
        ucOTEndDate.DateText = _OTEndDate

        ucOTStartTime.ucDefaultSelectedHH = _OTStartTime.Substring(0, 2)
        ucOTStartTime.ucDefaultSelectedMM = _OTStartTime.Substring(2, 2)
        ucOTEndTime.ucDefaultSelectedHH = _OTEndTime.Substring(0, 2)
        ucOTEndTime.ucDefaultSelectedMM = _OTEndTime.Substring(2, 2)

        chkMealFlag.Checked = If((dt.Rows(0)("MealFlag").ToString() = "1"), True, False)
        chkMealFlag_CheckedChanged(Nothing, EventArgs.Empty)
        txtMealTime.Text = dt.Rows(0)("MealTime").ToString()
        lblOTTotalTime.Text = Convert.ToDouble(dt.Rows(0)("OTTotalTime").ToString()).ToString("0.0")
        ddlCodeCName.SelectedValue = dt.Rows(0)("OTTypeID").ToString()
        txtOTReasonMemo.Text = dt.Rows(0)("OTReasonMemo").ToString()
        '_OTTxnID = dt.Rows(0)("OTTxnID").ToString()
        hidOTTxnID.Value = _OTTxnID 'dt.Rows(0)("OTTxnID").ToString()

        ddlSalaryOrAdjust.SelectedValue = dt.Rows(0)("SalaryOrAdjust").ToString()

        '依照RankID階級與加班起迄日來控制 加班轉換方式的下拉選項
        'RankID要進行轉換~
        '但是明細頁要直接顯示資料庫上的加班轉換方式，不能關閉選項
        _rankID = dt.Rows(0)("RankID").ToString()
        _rankID = OVBusinessCommon.GetRankID(dt.Rows(0)("OTCompID").ToString().Trim, _rankID)

        _EmpDate = dt.Rows(0)("EmpDate").ToString()
        _Sex = dt.Rows(0)("Sex").ToString()
        _WorkSiteID = dt.Rows(0)("WorkSiteID").ToString()
        If ViewState.Item("DoUpdate") = "N" Then
            If ddlSalaryOrAdjust.SelectedValue = "2" Then
                lbl_lblAdjustInvalidDate.Visible = True
                lblAdjustInvalidDate.Text = FormatDateTime(_dtPara.Rows(0).Item("AdjustInvalidDate").ToString(), DateFormat.ShortDate).Trim()
                lblAdjustInvalidDate.Visible = True
            End If
        Else
            ddlSalaryOrAdjustChange(_rankID, ucOTStartDate.DateText, ucOTEndDate.DateText)
            Bsp.Utility.SetSelectedIndex(ddlSalaryOrAdjust, dt.Rows(0)("SalaryOrAdjust").ToString())
            ddlSalaryOrAdjust_SelectedIndexChanged(Nothing, EventArgs.Empty)
        End If

        '計算本月的總時數
        SignData()

        '是明細則顯示最後異動區塊
        'LastChgPanel.Visible = True
        lblLastChgID.Text = dt.Rows(0)("LastChgID").ToString()
        lblLastChgNameN.Text = dt.Rows(0)("LastChgNameN").ToString()
        LastChgDate.Text = Format(dt.Rows(0)("LastChgDate"), "yyyy/MM/dd HH:mm:ss")

        '附件Attach
        Dim strAttachID As String = ""
        Dim strAttachAdminURL As String
        Dim strAttachAdminBaseURL As String = ConfigurationManager.AppSettings("AattendantWebPath") + "HandlerForOverTime/SSORecvModeForOverTime.aspx?UserID=" + UserProfile.UserID.Trim + "&SystemID=OT&TxnID=OV1101&ReturnUrl=%2FUtil%2FAttachAdmin.aspx?AttachDB={0}%26AttachID={1}%26AttachFileMaxQty={2}%26AttachFileMaxKB={3}%26AttachFileTotKB={4}%26AttachFileExtList={5}"
        Dim strAttachDownloadURL As String
        Dim strAttachDownloadBaseURL As String = Util._AttachDownloadUrl + "?AttachDB={0}&AttachID={1}"
        '附件編號
        _AttachID = If(String.IsNullOrEmpty(dt.Rows(0)("OTAttachment").ToString().Trim()), "", dt.Rows(0)("OTAttachment").ToString().Trim())
        hidAttachID.Value = _AttachID

        '附件
        If dt.Rows(0)("FileName").ToString() <> "" Then
            lblAttachName.Text = "附件檔名：" + dt.Rows(0)("FileName").ToString()
            labOTAttachment.Value = dt.Rows(0)("OTAttachment").ToString()
        Else
            btnDownloadAttach.Visible = False
        End If

        If String.IsNullOrEmpty(_AttachID) Then
            _AttachID = "test" + UserProfile.UserID.Trim + Guid.NewGuid().ToString()
        End If
        strAttachID = _AttachID
        ViewState("attach") = _AttachID
        hidAttachID.Value = _AttachID
        strAttachAdminURL = String.Format(strAttachAdminBaseURL, Config_AattendantDBName, strAttachID, "1", "3072", "3072", "")
        strAttachDownloadURL = String.Format(strAttachDownloadBaseURL, Config_AattendantDBName, strAttachID)
        frameAttach.Value = strAttachAdminURL

        If _Sex <> "" AndAlso _Sex = "2" Then
            If ucOTStartDate.DateText <> "" AndAlso ucOTEndDate.DateText <> "" Then
                '從10點開始
                If Convert.ToInt32(_OTStartTime.Substring(0, 2)) = 22 Then
                    'Bsp.Utility.ShowMessage(Me,"女性不可以10點後加班");
                    lblStartSex.Visible = True
                ElseIf Convert.ToInt32(_OTStartTime.Substring(0, 2)) > 22 Then
                    'Bsp.Utility.ShowMessage(Me,"女性不可以10點後加班");
                    lblStartSex.Visible = True
                    '從凌晨開始到六點
                ElseIf Convert.ToInt32(_OTStartTime.Substring(0, 2)) >= 0 AndAlso Convert.ToInt32(_OTStartTime.Substring(0, 2)) < 6 Then
                    'Bsp.Utility.ShowMessage(Me,"女性不可以10點後加班");
                    lblStartSex.Visible = True
                Else
                    lblStartSex.Visible = False
                End If

                '從10點開始
                If Convert.ToInt32(_OTEndTime.Substring(0, 2)) = 22 Then
                    'Bsp.Utility.ShowMessage(Me,"女性不可以10點後加班");
                    lblEndSex.Visible = True
                ElseIf Convert.ToInt32(_OTEndTime.Substring(0, 2)) > 22 Then
                    'Bsp.Utility.ShowMessage(Me,"女性不可以10點後加班");
                    lblEndSex.Visible = True
                    '從凌晨開始到六點
                ElseIf Convert.ToInt32(_OTEndTime.Substring(0, 2)) >= 0 AndAlso Convert.ToInt32(_OTEndTime.Substring(0, 2)) < 6 Then
                    'Bsp.Utility.ShowMessage(Me,"女性不可以10點後加班");
                    lblEndSex.Visible = True
                Else
                    lblEndSex.Visible = False
                End If
            End If
        Else
            lblStartSex.Visible = False
            lblEndSex.Visible = False
        End If
    End Sub

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
    ''' 初始化
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

#End Region

#Region "功能鍵邏輯處理"

    ''' <summary>
    ''' 返回
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GoBack()
        Dim ti As TransferInfo = Me.StateTransfer
        Me.TransferFramePage(ti.CallerUrl, Nothing, ti.Args)
    End Sub

    ''' <summary>
    ''' 執行清除
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearData()
        subGetData()
        Dim htmlTableString As String
        Dim ShowStartDateArr, ShowEndDateArr As New ArrayList    '顯示用
        Dim StartDateArr, EndDateArr As New ArrayList   '計算時段用

        If _OTStartDate = _OTEndDate Then
            Dim datas As String = Convert.ToString(ViewState("dataOne"))
            For i = 0 To datas.Split(","c).Length - 1
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
        Else
            Dim datas As String = Convert.ToString(ViewState("dataTwo"))
            For i = 0 To datas.Split(","c).Length - 1
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
        End If

        htmlTableString = subTableGenByArr(ShowStartDateArr, ShowEndDateArr)
        If htmlTableString <> "" Then
            litTable.Text = htmlTableString
            pnlCalcTotalTime.Visible = True
        End If

        txtOTTotalDescript.Text = If((txtMealTime.Text <> "" AndAlso Convert.ToDouble(txtMealTime.Text) > 0 AndAlso chkMealFlag.Checked = True), "(已扣除用餐時數" + txtMealTime.Text + "分鐘)", "")
        txtOTTotalDescript.Visible = True
    End Sub

    ''' <summary>
    ''' 執行暫存或送簽
    ''' </summary>
    ''' <param name="flag"></param>
    ''' <remarks></remarks>
    Public Sub SaveData(flag As String)
        Dim objOV11 As New OV1_2
        'Dim db As New DbHelper("AattendantDB")
        'Dim sb As CommandHelper = db.CreateCommandHelper()
        'Dim cn As DbConnection = db.OpenConnection()
        Dim cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
        Dim sb As New StringBuilder
        Dim tx As DbTransaction = cn.BeginTransaction()
        Dim strcheckMealFlag As String = If((chkMealFlag.Checked = True), "1", "0")
        Dim cntStart As Double = 0
        Dim cntEnd As Double = 0
        Dim cntTotal As Double = 0
        getCntStartAndCntEnd(cntStart, cntEnd)
        Dim mealOver As String = objOV11.MealJudge(cntStart, Convert.ToDouble(txtMealTime.Text))

        Dim attach As String = objOV11.QueryAttach(_AttachID, lblCompID.Text.Trim, _EmpID)
        If String.IsNullOrEmpty(attach) Then
            ViewState("attach") = "test" + UserProfile.UserID.Trim + Guid.NewGuid().ToString()
        Else
            ViewState("attach") = attach
        End If
        _AttachID = attach
        hidAttachID.Value = _AttachID

        Dim OTSeq As Integer = 0
        Dim OTSeq_1 As Integer = 0
        'If flag = "2" Then
        '    Dim strDirectSubmit As String = DirectSubmit(ucOTStartDate.DateText, ucOTEndDate.DateText, lblCompID.Text.Trim, _EmpID, ucOTStartTime.ucDefaultSelectedHH + ucOTStartTime.ucDefaultSelectedMM, ucOTEndTime.ucDefaultSelectedHH + ucOTEndTime.ucDefaultSelectedMM, _OTFromAdvanceTxnId, lblOTTotalTime.Text)
        '    If strDirectSubmit = "Y" Then
        '        '判斷是否直接送簽
        '        flag = "3"
        '    End If
        'End If

        If _OTStartDate = _OTEndDate Then     '原本不跨日
            If ucOTStartDate.DateText = ucOTEndDate.DateText Then    '不跨日
                Dim strHo As String = objOV11.QueryColumn("HolidayOrNot", _eHRMSDB_ITRD + ".[dbo].[Calendar] ", " AND CompID = '" + lblCompID.Text.Trim + "' AND CONVERT(CHAR(10),SysDate, 111) ='" + ucOTStartDate.DateText + "'")
                getCntTotal(cntTotal)
                OTSeq = objOV11.QuerySeq("OverTimeDeclaration", lblCompID.Text.Trim, _EmpID, ucOTStartDate.DateText)
                sb.AppendLine("UPDATE OverTimeDeclaration SET OTStartDate='" + ucOTStartDate.DateText + "',OTEndDate='" + ucOTEndDate.DateText + "',OTStartTime='" + ucOTStartTime.ucDefaultSelectedHH + ucOTStartTime.ucDefaultSelectedMM + "', OTEndTime='" + ucOTEndTime.ucDefaultSelectedHH + ucOTEndTime.ucDefaultSelectedMM + "',")
                sb.AppendLine(" SalaryOrAdjust='" + ddlSalaryOrAdjust.SelectedValue + "',OTSeq='" + OTSeq.ToString() + "',OTStatus='" + flag + "',")
                If ddlSalaryOrAdjust.SelectedValue = "2" Then
                    sb.AppendLine(" AdjustInvalidDate='" + lblAdjustInvalidDate.Text + "', ")        '失效時間
                Else
                    sb.AppendLine(" AdjustInvalidDate='', ")         '失效時間
                End If
                sb.AppendLine(" OTAttachment='" + attach + "', ")
                sb.AppendLine(" OTTotalTime='" + cntTotal.ToString() + "',MealFlag='" + strcheckMealFlag + "',MealTime='" + txtMealTime.Text + "',OTTypeID='" + ddlCodeCName.SelectedValue + "',OTReasonMemo='" + (txtOTReasonMemo.Text).Replace("'", "''") + "',")
                sb.AppendLine(" HolidayOrNot='" + strHo + "',LastChgComp='" + UserProfile.ActCompID.Trim + "',LastChgID='" + UserProfile.UserID.Trim + "',LastChgDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'")
                sb.AppendLine(" WHERE OTCompID='" + lblCompID.Text.Trim + "' AND OTEmpID='" + _EmpID + "'")
                sb.AppendLine(" AND OTStartDate='" + _OTStartDate + "'")
                sb.AppendLine(" AND OTEndDate='" + _OTEndDate + "'")
                sb.AppendLine(" AND OTStartTime='" + _OTStartTime + "'")
                sb.AppendLine(" AND OTEndTime='" + _OTEndTime + "'")
                sb.AppendLine(" AND OTStatus='1'")
            Else
                Dim strHo1 As String = objOV11.QueryColumn("HolidayOrNot", _eHRMSDB_ITRD + ".[dbo].[Calendar] ", " AND CompID = '" + lblCompID.Text.Trim + "' AND CONVERT(CHAR(10),SysDate, 111) ='" + ucOTStartDate.DateText + "'")
                Dim strHo2 As String = objOV11.QueryColumn("HolidayOrNot", _eHRMSDB_ITRD + ".[dbo].[Calendar] ", " AND CompID = '" + lblCompID.Text.Trim + "' AND CONVERT(CHAR(10),SysDate, 111) ='" + ucOTEndDate.DateText + "'")
                getCntStartAndCntEnd(cntStart, cntEnd)
                OTSeq = objOV11.QuerySeq("OverTimeDeclaration", lblCompID.Text.Trim, _EmpID, ucOTStartDate.DateText)
                sb.AppendLine("UPDATE OverTimeDeclaration SET OTStartDate='" + ucOTStartDate.DateText + "',OTEndDate='" + ucOTStartDate.DateText + "',OTStartTime='" + ucOTStartTime.ucDefaultSelectedHH + ucOTStartTime.ucDefaultSelectedMM + "', OTEndTime='2359',")
                sb.Append(" SalaryOrAdjust='" + ddlSalaryOrAdjust.SelectedValue + "',OTSeq='" + OTSeq.ToString() + "',OTStatus='" + flag + "',")
                If ddlSalaryOrAdjust.SelectedValue = "2" Then
                    sb.AppendLine(" AdjustInvalidDate='" + lblAdjustInvalidDate.Text + "', ")       '失效時間
                Else
                    sb.AppendLine(" AdjustInvalidDate='', ")         '失效時間
                End If
                sb.AppendLine(" OTAttachment='" + attach + "', ")
                sb.AppendLine(" OTTotalTime='" + cntStart.ToString() + "',MealFlag='" + mealOver.Split(","c)(0) + "',MealTime='" + mealOver.Split(","c)(1) + "',OTTypeID='" + ddlCodeCName.SelectedValue + "',OTReasonMemo='" + (txtOTReasonMemo.Text).Replace("'", "''") + "',")
                sb.AppendLine(" HolidayOrNot='" + strHo1 + "',LastChgComp='" + UserProfile.ActCompID.Trim + "',LastChgID='" + UserProfile.UserID.Trim + "',LastChgDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'")
                sb.AppendLine(" WHERE OTCompID='" + lblCompID.Text.Trim + "' AND OTEmpID='" + _EmpID + "'")
                sb.AppendLine(" AND OTStartDate='" + _OTStartDate + "'")
                sb.AppendLine(" AND OTEndDate='" + _OTEndDate + "'")
                sb.AppendLine(" AND OTStartTime='" + _OTStartTime + "'")
                sb.AppendLine(" AND OTEndTime='" + _OTEndTime + "'")
                sb.AppendLine(" AND OTStatus='1';")

                OTSeq_1 = objOV11.QuerySeq("OverTimeDeclaration", lblCompID.Text.Trim, _EmpID, ucOTEndDate.DateText)
                sb.AppendLine(" INSERT INTO OverTimeDeclaration(OTCompID,OTEmpID,OTStartDate,OTEndDate,OTSeq,OTTxnID,OTSeqNo,DeptID,OrganID,DeptName,OrganName,FlowCaseID,OTStartTime,OTEndTime,OTTotalTime,SalaryOrAdjust,AdjustInvalidDate,AdjustStatus,AdjustDate,MealFlag,MealTime,OTTypeID,OTReasonID,OTReasonMemo,OTAttachment,OTFormNO,OTRegisterID,OTRegisterDate,OTStatus,OTValidDate,OTValidID,OTRejectDate,OTRejectID,OTGovernmentNo,OTSalaryPaid,HolidayOrNot,ProcessDate,OTPayDate,OTModifyDate,OTRemark,KeyInComp,KeyInID,HRKeyInFlag,LastChgComp,LastChgID,LastChgDate,OTRegisterComp) ")
                sb.AppendLine(" SELECT  OTCompID,OTEmpID,'" + ucOTEndDate.DateText + "','" + ucOTEndDate.DateText + "','" + OTSeq_1.ToString() + "',OTTxnID,'2',DeptID,OrganID,DeptName,OrganName,FlowCaseID,'0000','" + ucOTEndTime.ucDefaultSelectedHH + ucOTEndTime.ucDefaultSelectedMM + "','" + cntEnd.ToString() + "',SalaryOrAdjust,AdjustInvalidDate,AdjustStatus,AdjustDate,MealFlag,'" + mealOver.Split(","c)(3) + "',OTTypeID,OTReasonID,OTReasonMemo,OTAttachment,OTFormNO,OTRegisterID,OTRegisterDate,OTStatus,OTValidDate,OTValidID,OTRejectDate,OTRejectID,OTGovernmentNo,OTSalaryPaid,'" + strHo2 + "',ProcessDate,OTPayDate,OTModifyDate,OTRemark,KeyInComp,KeyInID,HRKeyInFlag,LastChgComp,LastChgID,LastChgDate,OTRegisterComp FROM OverTimeDeclaration")
                sb.AppendLine(" WHERE OTCompID='" + lblCompID.Text.Trim + "' AND OTEmpID='" + _EmpID + "'")
                sb.AppendLine(" AND OTStartDate='" + ucOTStartDate.DateText + "'")
                sb.AppendLine(" AND OTEndDate='" + ucOTStartDate.DateText + "'")
                sb.AppendLine(" AND OTStartTime='" + ucOTStartTime.ucDefaultSelectedHH + ucOTStartTime.ucDefaultSelectedMM + "'")
                sb.AppendLine(" AND OTEndTime='2359'")
            End If
        Else
            If ucOTStartDate.DateText = ucOTEndDate.DateText Then
                '不跨日
                Dim strHo As String = objOV11.QueryColumn("HolidayOrNot", _eHRMSDB_ITRD + ".[dbo].[Calendar] ", " AND CompID = '" + lblCompID.Text.Trim + "' AND CONVERT(CHAR(10),SysDate, 111) ='" + ucOTStartDate.DateText + "'")
                OTSeq = objOV11.QuerySeq("OverTimeDeclaration", lblCompID.Text.Trim, _EmpID, ucOTStartDate.DateText)
                getCntTotal(cntTotal)
                sb.AppendLine("UPDATE OverTimeDeclaration SET OTStartDate='" + ucOTStartDate.DateText + "',OTEndDate='" + ucOTEndDate.DateText + "',OTStartTime='" + ucOTStartTime.ucDefaultSelectedHH + ucOTStartTime.ucDefaultSelectedMM + "', OTEndTime='" + ucOTEndTime.ucDefaultSelectedHH + ucOTEndTime.ucDefaultSelectedMM + "',")
                sb.AppendLine(" SalaryOrAdjust='" + ddlSalaryOrAdjust.SelectedValue + "',OTSeq='" + OTSeq.ToString() + "',OTStatus='" + flag + "',")
                If ddlSalaryOrAdjust.SelectedValue = "2" Then
                    sb.AppendLine(" AdjustInvalidDate='" + lblAdjustInvalidDate.Text + "', ")       '失效時間
                Else
                    sb.AppendLine(" AdjustInvalidDate='', ")        '失效時間
                End If
                sb.AppendLine(" OTAttachment='" + attach + "', ")
                sb.AppendLine(" OTTotalTime='" + cntTotal.ToString() + "',MealFlag='" + strcheckMealFlag + "',MealTime='" + txtMealTime.Text + "',OTTypeID='" + ddlCodeCName.SelectedValue + "',OTReasonMemo='" + (txtOTReasonMemo.Text).Replace("'", "''") + "',")
                sb.AppendLine(" HolidayOrNot='" + strHo + "',LastChgComp='" + UserProfile.ActCompID.Trim + "',LastChgID='" + UserProfile.UserID.Trim + "',LastChgDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'")
                sb.AppendLine(" WHERE OTCompID='" + lblCompID.Text.Trim + "' AND OTEmpID='" + _EmpID + "'")
                sb.AppendLine(" AND OTStartDate='" + _OTStartDate + "'")
                sb.AppendLine(" AND OTEndDate='" + _OTStartDate + "'")
                sb.AppendLine(" AND OTStartTime='" + _OTStartTime + "'")
                sb.AppendLine(" AND OTEndTime='2359'")
                sb.AppendLine(" AND OTStatus='1'")

                sb.AppendLine("DELETE FROM OverTimeDeclaration ")
                sb.AppendLine(" WHERE OTCompID='" + lblCompID.Text.Trim + "' AND OTEmpID='" + _EmpID + "'")
                sb.AppendLine(" AND OTStartDate='" + _OTEndDate + "'")
                sb.AppendLine(" AND OTEndDate='" + _OTEndDate + "'")
                sb.AppendLine(" AND OTStartTime='0000'")
                sb.AppendLine(" AND OTEndTime='" + _OTEndTime + "'")
            Else
                Dim strHo1 As String = objOV11.QueryColumn("HolidayOrNot", _eHRMSDB_ITRD + ".[dbo].[Calendar] ", " AND CompID = '" + lblCompID.Text.Trim + "' AND CONVERT(CHAR(10),SysDate, 111) ='" + ucOTStartDate.DateText + "'")
                Dim strHo2 As String = objOV11.QueryColumn("HolidayOrNot", _eHRMSDB_ITRD + ".[dbo].[Calendar] ", " AND CompID = '" + lblCompID.Text.Trim + "' AND CONVERT(CHAR(10),SysDate, 111) ='" + ucOTEndDate.DateText + "'")
                getCntStartAndCntEnd(cntStart, cntEnd)
                OTSeq = objOV11.QuerySeq("OverTimeDeclaration", lblCompID.Text.Trim, _EmpID, ucOTStartDate.DateText)
                sb.AppendLine("UPDATE OverTimeDeclaration SET OTStartDate='" + ucOTStartDate.DateText + "',OTEndDate='" + ucOTStartDate.DateText + "',OTStartTime='" + ucOTStartTime.ucDefaultSelectedHH + ucOTStartTime.ucDefaultSelectedMM + "', OTEndTime='2359',")
                sb.AppendLine(" SalaryOrAdjust='" + ddlSalaryOrAdjust.SelectedValue + "',OTSeq='" + OTSeq.ToString() + "',OTStatus='" + flag + "',")
                If ddlSalaryOrAdjust.SelectedValue = "2" Then
                    '失效時間
                    sb.AppendLine(" AdjustInvalidDate='" + lblAdjustInvalidDate.Text + "', ")
                Else
                    '失效時間
                    sb.AppendLine(" AdjustInvalidDate='', ")
                End If
                sb.AppendLine(" OTAttachment='" + attach + "', ")
                sb.AppendLine(" OTTotalTime='" + cntStart.ToString() + "',MealFlag='" + mealOver.Split(","c)(0) + "',MealTime='" + mealOver.Split(","c)(1) + "',OTTypeID='" + ddlCodeCName.SelectedValue + "',OTReasonMemo='" + (txtOTReasonMemo.Text).Replace("'", "''") + "',")
                sb.AppendLine(" HolidayOrNot='" + strHo1 + "',LastChgComp='" + UserProfile.ActCompID.Trim + "',LastChgID='" + UserProfile.UserID.Trim + "',LastChgDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'")
                sb.AppendLine(" WHERE OTCompID='" + lblCompID.Text.Trim + "' AND OTEmpID='" + _EmpID + "'")
                sb.AppendLine(" AND OTStartDate='" + _OTStartDate + "'")
                sb.AppendLine(" AND OTEndDate='" + _OTStartDate + "'")
                sb.AppendLine(" AND OTStartTime='" + _OTStartTime + "'")
                sb.AppendLine(" AND OTEndTime='2359'")
                sb.AppendLine(" AND OTStatus='1'")

                OTSeq_1 = objOV11.QuerySeq("OverTimeDeclaration", lblCompID.Text.Trim, _EmpID, ucOTEndDate.DateText)
                sb.AppendLine("UPDATE OverTimeDeclaration SET OTStartDate='" + ucOTEndDate.DateText + "',OTEndDate='" + ucOTEndDate.DateText + "',OTStartTime='0000', OTEndTime='" + ucOTEndTime.ucDefaultSelectedHH + ucOTEndTime.ucDefaultSelectedMM + "',")
                sb.AppendLine(" SalaryOrAdjust='" + ddlSalaryOrAdjust.SelectedValue + "',OTSeq='" + OTSeq_1.ToString() + "',OTStatus='" + flag + "',")
                If ddlSalaryOrAdjust.SelectedValue = "2" Then
                    sb.AppendLine(" AdjustInvalidDate='" + lblAdjustInvalidDate.Text + "', ")       '失效時間
                Else
                    sb.AppendLine(" AdjustInvalidDate='', ")        '失效時間
                End If
                sb.AppendLine(" OTAttachment='" + attach + "', ")
                sb.AppendLine(" OTTotalTime='" + cntEnd.ToString() + "',MealFlag='" + mealOver.Split(","c)(0) + "',MealTime='" + mealOver.Split(","c)(3) + "',OTTypeID='" + ddlCodeCName.SelectedValue + "',OTReasonMemo='" + (txtOTReasonMemo.Text).Replace("'", "''") + "',")
                sb.AppendLine(" HolidayOrNot='" + strHo2 + "',LastChgComp='" + UserProfile.ActCompID.Trim + "',LastChgID='" + UserProfile.UserID.Trim + "',LastChgDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'")
                sb.AppendLine(" WHERE OTCompID='" + lblCompID.Text.Trim + "' AND OTEmpID='" + _EmpID + "'")
                sb.AppendLine(" AND OTStartDate='" + _OTEndDate + "'")
                sb.AppendLine(" AND OTEndDate='" + _OTEndDate + "'")
                sb.AppendLine(" AND OTStartTime='0000'")
                sb.AppendLine(" AND OTEndTime='" + _OTEndTime + "'")
                sb.AppendLine(" AND OTStatus='1'")
            End If
        End If

        If flag = "1" Then
            '暫存
            Try
                Bsp.DB.ExecuteNonQuery(CommandType.Text, sb.ToString, tx, "AattendantDB")
                'db.ExecuteNonQuery(sb.BuildCommand(), tx)
                tx.Commit()
                _OTStartDate = ucOTStartDate.DateText
                _OTEndDate = ucOTEndDate.DateText
                _OTStartTime = ucOTStartTime.ucDefaultSelectedHH + ucOTStartTime.ucDefaultSelectedMM
                _OTEndTime = ucOTEndTime.ucDefaultSelectedHH + ucOTEndTime.ucDefaultSelectedMM
                _OTSeq = Convert.ToString(OTSeq)
                LoadAttch()
                Bsp.Utility.ShowMessage(Me, "暫存成功！")
            Catch ex As Exception
                LogHelper.WriteSysLog(ex)
                '將 Exception 丟給 Log 模組
                tx.Rollback()
                '資料更新失敗
                Bsp.Utility.ShowMessage(Me, "暫存失敗！")
            Finally
                cn.Close()
                cn.Dispose()
                tx.Dispose()
            End Try

        ElseIf flag = "2" Then
            '送簽
            Try
                'db.ExecuteNonQuery(sb.BuildCommand(), tx)
                'tx.Commit()
                '提案送審成功
                'Bsp.Utility.RunClientScript(Me.Page, "ExecSubmit('1');")
                ExecuteSubmit()
                'Bsp.Utility.ShowMessage(Me, "送簽成功")
            Catch ex As Exception
                LogHelper.WriteSysLog(ex)
                '將 Exception 丟給 Log 模組
                'tx.Rollback()
                '資料更新失敗
                'Bsp.Utility.ShowMessage(Me, "送簽失敗！")
            Finally
                'cn.Close()
                'cn.Dispose()
                'tx.Dispose()
            End Try
        Else         '直接送簽 flag = "3"
            Try
                'db.ExecuteNonQuery(sb.BuildCommand(), tx)
                'tx.Commit()
                '提案送審成功
                'Bsp.Utility.RunClientScript(Me.Page, "ExecSubmit('1');")
                ExecuteSubmit()
                'Bsp.Utility.ShowMessage(Me, "送簽成功")
            Catch ex As Exception
                LogHelper.WriteSysLog(ex)
                '將 Exception 丟給 Log 模組
                'tx.Rollback()
                '資料更新失敗
                'Bsp.Utility.ShowMessage(Me, "送簽失敗！")
            Finally
                'cn.Close()
                'cn.Dispose()
                'tx.Dispose()
            End Try
        End If
    End Sub

    ''' <summary>
    ''' 開始送簽
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ExecuteSubmit()
        'Dim jsonlist As New List(Of Dictionary(Of String, String))()
        'Dim Dictionary As New Dictionary(Of String, String)

        ''Dictionary.Add("OTCompID", gvMain.DataKeys(intRow)("OTCompID").ToString())
        ''Dictionary.Add("OTEmpID", gvMain.DataKeys(intRow)("OTEmpID").ToString())
        ''Dictionary.Add("OTStartDate", gvMain.DataKeys(intRow)("OTDate").ToString().Split("~")(0))
        ''Dictionary.Add("OTEndDate", gvMain.DataKeys(intRow)("OTDate").ToString().Split("~")(1))
        ''Dictionary.Add("OTStartTime", gvMain.DataKeys(intRow)("OTTime").ToString().Split("~")(0).Replace(":", ""))
        ''Dictionary.Add("OTEndTime", gvMain.DataKeys(intRow)("OTTime").ToString().Split("~")(1).Replace(":", ""))
        ''Dictionary.Add("OTSeq", gvMain.DataKeys(intRow)("OTSeq").ToString())
        ''Dictionary.Add("OTRegisterID", gvMain.DataKeys(intRow)("OTRegisterID").ToString())

        'Dictionary.Add("OTCompID", lblCompID.Text.Trim)
        'Dictionary.Add("OTEmpID", _EmpID)
        'Dictionary.Add("OTStartDate", ucOTStartDate.DateText)
        'Dictionary.Add("OTEndDate", ucOTEndDate.DateText)
        'Dictionary.Add("OTStartTime", (ucOTStartTime.ucDefaultSelectedHH + ucOTStartTime.ucDefaultSelectedMM))
        'Dictionary.Add("OTEndTime", (ucOTEndTime.ucDefaultSelectedHH + ucOTEndTime.ucDefaultSelectedMM))
        'Dictionary.Add("OTSeq", _OTSeq)
        'Dictionary.Add("OTRegisterID", lblOTRegisterID.Text.Trim)
        'Dictionary.Add("OTRegisterComp", hidOTRegisterCompID.Value.ToString())

        'jsonlist.Add(Dictionary)

        'Dim DataList As New Dictionary(Of String, List(Of Dictionary(Of String, String)))
        'DataList.Add("DataList", jsonlist)

        'hidGuidID.Value = Guid.NewGuid().ToString()
        'Dim sb As New StringBuilder
        'sb.Append("INSERT INTO CacheData (Platform,SystemID,TxnName,UserID,CacheID,CacheData,CacheDT,Aging) ")
        'sb.Append(" VALUES('AP', 'OT', 'OV1102', '" + UserProfile.ActUserID + "', '" + hidGuidID.Value + "', '" + Newtonsoft.Json.JsonConvert.SerializeObject(DataList) + "', GETDATE(), '30')")
        'Bsp.DB.ExecuteNonQuery(CommandType.Text, sb.ToString, "AattendantDB")

        Bsp.Utility.RunClientScript(Me.Page, "ExecSubmit();")
    End Sub
#End Region

#Region "計算已申報時數合計"
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

#End Region

#Region "加班轉換方式"
    ''' <summary>
    ''' 加班轉換方式改變事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ddlSalaryOrAdjust_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlSalaryOrAdjust.SelectedIndexChanged
        'If ViewState.Item("DoUpdate") = "Y" Then
        Select Case ddlSalaryOrAdjust.SelectedValue
            Case "2"
                lbl_lblAdjustInvalidDate.Visible = True
                lblAdjustInvalidDate.Visible = True
                lblAdjustInvalidDate.Text = FormatDateTime(_dtPara.Rows(0).Item("AdjustInvalidDate").ToString(), DateFormat.ShortDate).Trim()
            Case Else
                lbl_lblAdjustInvalidDate.Visible = False
                lblAdjustInvalidDate.Visible = False
        End Select
        'End If
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
        Dim objOV11 As New OV1_2
        Dim AdjustRankID As String = ""     '比較的AdjustRankID
        Dim IntAdjustRankID As Integer = 0      '比較的AdjustRankID

        Try
            AdjustRankID = OVBusinessCommon.GetRankID(UserProfile.SelectCompRoleID.Trim, _dtPara.Rows(0)("AdjustRankID").ToString())
        Catch ex As Exception
            Debug.Print("Find Mapping RankID Failed==>" + ex.Message)
            Return
        End Try

        If IsNumeric(AdjustRankID) Then IntAdjustRankID = Convert.ToInt32(AdjustRankID)

        Dim dateStartIsHoliday As Boolean = objOV11.CheckHolidayOrNot(startDate)
        Dim dateEndIsHoliday As Boolean = objOV11.CheckHolidayOrNot(endDate)
        Dim isSameDate As Boolean = If(startDate = endDate, True, False)

        If Not String.IsNullOrEmpty(startDate) AndAlso Not String.IsNullOrEmpty(endDate) Then
            '先回到預設值並enable
            ddlSalaryOrAdjust.Enabled = True
            ddlSalaryOrAdjust.SelectedIndex = 0

            If IsNumeric(AdjustRankID) AndAlso IsNumeric(sRankID) Then '判斷參數設定的轉補休職等設定有設定(也就是參數必需是數字!)，若不為數字則參照參數設定預設值選擇
                Dim dRankID = Convert.ToInt32(sRankID)
                If dRankID >= AdjustRankID Then '如果加班人職等大於等於參數設定的職等，僅能選擇轉補休
                    '如果RankID大於等於AdjustRankID只能轉補休
                    ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉薪資"))).Enabled = False
                    ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉補休"))).Enabled = True

                    '2017/05/09 - 若有限制選項，將預設選項選為未受限制的選項
                    ddlSalaryOrAdjust.SelectedIndex = ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉補休"))

                ElseIf dRankID < AdjustRankID Then  '如果加班人職小於於參數設定的職等且加班起訖日都是假日，可選擇轉補休或轉薪資，反之則看參數設定    
                    If isSameDate Then  '是單日單
                        '是單日單且加班日是假日則開放所有選項
                        If dateStartIsHoliday Then
                            ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉薪資"))).Enabled = True
                            ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉補休"))).Enabled = True
                            '2017/05/08 HR 要求如前台假日預先加班申請初始帶入轉補休
                            ddlSalaryOrAdjust.SelectedIndex = ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉補休"))

                        Else    '單日單且加班日是平日則看參數檔預設值
                            Select Case _dtPara.Rows(0)("SalaryOrAjust").ToString()
                                Case "1"    '參數設定預設為轉薪資
                                    ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉薪資"))).Enabled = True
                                    ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉補休"))).Enabled = False
                                    '2017/05/10 - 若有限制選項，將預設選項選為未受限制的選項且鎖定不給修改
                                    ddlSalaryOrAdjust.SelectedIndex = ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉薪資"))
                                    ddlSalaryOrAdjust.Enabled = False
                                Case "2"    '參數設定預設為轉補休
                                    ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉薪資"))).Enabled = False
                                    ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉補休"))).Enabled = True
                                    '2017/05/10 - 若有限制選項，將預設選項選為未受限制的選項且鎖定不給修改
                                    ddlSalaryOrAdjust.SelectedIndex = ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉補休"))
                                    ddlSalaryOrAdjust.Enabled = False
                                Case Else   '參數設定錯誤，來亂的喔!
                                    Debug.Print("參數設定錯誤,SalaryOrAjust=" & _dtPara.Rows(0)("SalaryOrAjust").ToString())
                                    ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉薪資"))).Enabled = True
                                    ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉補休"))).Enabled = True
                                    '2017/05/08 - 參數有誤則就回到預設-請選擇
                                    ddlSalaryOrAdjust.SelectedIndex = 0
                            End Select
                        End If
                    Else    '是跨日單
                        '加班起訖日皆是假日則開放所有選項
                        If dateStartIsHoliday AndAlso dateEndIsHoliday Then
                            ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉薪資"))).Enabled = True
                            ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉補休"))).Enabled = True
                            '假日預設至轉補休
                            ddlSalaryOrAdjust.SelectedIndex = ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉補休"))
                        Else
                            ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉薪資"))).Enabled = True
                            ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉補休"))).Enabled = False
                            '加班起訖日非全是假日則只能轉薪資
                            ddlSalaryOrAdjust.SelectedIndex = ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉薪資"))
                        End If
                    End If
                End If
            Else '如果回傳的RanKID不為數字，依參數設定值選擇
                If isSameDate Then  '是單日單
                    '是單日單且加班日是假日則開放所有選項
                    If dateStartIsHoliday Then
                        ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉薪資"))).Enabled = True
                        ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉補休"))).Enabled = True
                        '2017/05/08 HR 要求如前台假日預先加班申請初始帶入轉補休
                        ddlSalaryOrAdjust.SelectedIndex = ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉補休"))
                    Else    '單日單且加班日是平日則看參數檔預設值
                        Select Case _dtPara.Rows(0)("SalaryOrAjust").ToString()
                            Case "1"    '參數設定預設為轉薪資
                                ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉薪資"))).Enabled = True
                                ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉補休"))).Enabled = False
                                '2017/05/10 - 若有限制選項，將預設選項選為未受限制的選項且鎖定不給修改
                                ddlSalaryOrAdjust.SelectedIndex = ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉薪資"))
                                ddlSalaryOrAdjust.Enabled = False
                            Case "2"    '參數設定預設為轉補休
                                ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉薪資"))).Enabled = False
                                ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉補休"))).Enabled = True
                                '2017/05/10 - 若有限制選項，將預設選項選為未受限制的選項且鎖定不給修改
                                ddlSalaryOrAdjust.SelectedIndex = ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉補休"))
                                ddlSalaryOrAdjust.Enabled = False
                            Case Else   '參數設定錯誤，來亂的喔!
                                Debug.Print("參數設定錯誤,SalaryOrAjust=" & _dtPara.Rows(0)("SalaryOrAjust").ToString())
                                ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉薪資"))).Enabled = True
                                ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉補休"))).Enabled = True
                                '2017/05/08 - 參數有誤則就回到預設-請選擇
                                ddlSalaryOrAdjust.SelectedIndex = 0
                        End Select
                    End If
                Else    '是跨日單
                    '加班起訖日皆是假日則開放所有選項
                    If dateStartIsHoliday AndAlso dateEndIsHoliday Then
                        ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉薪資"))).Enabled = True
                        ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉補休"))).Enabled = True
                        '假日預設至轉補休
                        ddlSalaryOrAdjust.SelectedIndex = ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉補休"))
                    Else    '加班起訖日非全是假日則只能轉薪資
                        ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉薪資"))).Enabled = True
                        ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉補休"))).Enabled = False
                        ddlSalaryOrAdjust.SelectedIndex = ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉薪資"))
                    End If
                End If
            End If
        End If
    End Sub
#End Region

#Region "檢查Funct"
    ''' <summary>
    ''' 檢查Function
    ''' </summary>
    ''' <param name="flag"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function checkData(flag As String) As Boolean
        Dim objOV11 As New OV1_2
        Dim ErrMsg As String = ""

        If flag = "2" Then
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
        End If
        If ddlSalaryOrAdjust.SelectedValue = "" Then
            Bsp.Utility.ShowMessage(Me, "需選擇加班轉換方式")
            Return False
        End If
        If ucOTStartDate.DateText = "" Then
            Bsp.Utility.ShowMessage(Me, "您必須輸入加班開始日期")
            Return False
        End If
        If ucOTEndDate.DateText = "" Then
            Bsp.Utility.ShowMessage(Me, "您必須輸入加班結束日期")
            Return False
        End If

        If ucOTStartTime.ucDefaultSelectedHH = "請選擇" OrElse ucOTStartTime.ucDefaultSelectedMM = "請選擇" Then
            Bsp.Utility.ShowMessage(Me, "您必須輸入加班開始時間")
            Return False
        End If
        If ucOTEndTime.ucDefaultSelectedHH = "請選擇" OrElse ucOTEndTime.ucDefaultSelectedMM = "請選擇" Then
            Bsp.Utility.ShowMessage(Me, "您必須輸入加班結束時間")
            Return False
        End If
        If ddlCodeCName.SelectedValue = "" Then
            Bsp.Utility.ShowMessage(Me, "需選擇加班類型")
            Return False
        End If
        If txtOTReasonMemo.Text.Trim = "" Then
            Bsp.Utility.ShowMessage(Me, "需填寫加班原因")
            Return False
        End If
        If chkMealFlag.Checked Then
            If txtMealTime.Text = "" OrElse Convert.ToDouble(txtMealTime.Text) <= 0 Then
                Bsp.Utility.ShowMessage(Me, "您必須填寫用餐時間")
                Return False
            End If
        End If
        'Dim db As New DbHelper("AattendantDB")
        'Dim sb As CommandHelper = db.CreateCommandHelper()
        Dim sb As New StringBuilder
        Dim dt As DataTable = Nothing
        '用餐時數大於加班時間
        Dim cntTotal As Double = 0.0
        Dim cntStart As Double = 0.0
        Dim cntEnd As Double = 0.0
        If ucOTStartDate.DateText = ucOTEndDate.DateText Then
            '不跨日
            cntTotal = (Convert.ToDouble(ucOTEndTime.ucDefaultSelectedHH) * 60 - Convert.ToDouble(ucOTStartTime.ucDefaultSelectedHH) * 60 + (Convert.ToDouble(ucOTEndTime.ucDefaultSelectedMM) - Convert.ToDouble(ucOTStartTime.ucDefaultSelectedMM)))
            If txtMealTime.Text <> "" AndAlso Convert.ToInt32(txtMealTime.Text) >= (cntTotal) Then
                Bsp.Utility.ShowMessage(Me, "用餐時數超過加班時數")
                Return False
            End If
        Else
            cntStart = (24 - (Convert.ToDouble(ucOTStartTime.ucDefaultSelectedHH))) * 60 - Convert.ToDouble(ucOTStartTime.ucDefaultSelectedMM)
            cntEnd = (Convert.ToDouble(ucOTEndTime.ucDefaultSelectedHH)) * 60 + Convert.ToDouble(ucOTEndTime.ucDefaultSelectedMM)
            If txtMealTime.Text <> "" AndAlso Convert.ToInt32(txtMealTime.Text) >= (cntEnd + cntStart) Then
                Bsp.Utility.ShowMessage(Me, "用餐時數超過加班時數")
                Return False
            End If
        End If

        '2017/03/17-新的判斷連續假日邏輯
        '先檢核目前單是否有一般假日連續上班情形
        If objOV11.CheckHolidayOrNot(ucOTStartDate.DateText) Then  '如果加班起日是假日
            If ucOTStartDate.DateText <> ucOTEndDate.DateText Then
                If (Weekday(Convert.ToDateTime(ucOTStartDate.DateText)) = 0 OrElse Weekday(Convert.ToDateTime(ucOTStartDate.DateText)) = 7) AndAlso (Weekday(Convert.ToDateTime(ucOTEndDate.DateText)) = 0 OrElse Weekday(Convert.ToDateTime(ucOTEndDate.DateText)) = 7) Then    '如果加班起訖日是周末
                    ErrMsg += "不能假日連續加班" & vbNewLine    '周末連續加班為假日連續加班
                ElseIf (Not OVBusinessCommon.IsNationalHoliday(ucOTStartDate.DateText)) Then    '如果加班起日是非周末之一般假日
                    If objOV11.CheckHolidayOrNot(ucOTEndDate.DateText) AndAlso (Weekday(Convert.ToDateTime(ucOTEndDate.DateText)) = 0 OrElse Weekday(Convert.ToDateTime(ucOTEndDate.DateText)) = 7) Then    '如果加班迄日為周末之一般假日
                        ErrMsg += "不能假日連續加班" & vbNewLine    '一般假日跨一般周末假日為假日連續加班
                    ElseIf Not OVBusinessCommon.IsNationalHoliday(ucOTEndDate.DateText) Then
                        ErrMsg += "不能假日連續加班" & vbNewLine    '一般假日跨非周末的一般假日為假日連續加班
                    Else
                        Dim ChkMsg As String = ""
                        If Not objOV11.CheckNHolidayOTOrNot(lblCompID.Text.Trim, _EmpID, ucOTStartDate.DateText, ucOTEndDate.DateText, _OTTxnID, "Upd", ChkMsg) Then
                            ErrMsg += ChkMsg
                        End If
                    End If
                    'ElseIf Weekday(Convert.ToDateTime(ucOTStartDate.DateText)) = 0 Then '如果加班起日是周末之一般假日
                    '    If objOV11.CheckHolidayOrNot(ucOTEndDate.DateText) AndAlso Not OVBusinessCommon.IsNationalHoliday(ucOTStartDate.DateText) Then
                    '        ErrMsg += "不能假日連續加班" & vbNewLine    '一般周末假日跨非周末的一般假日為假日連續加班
                    '    End If
                    '    If Not OVBusinessCommon.IsNationalHoliday(ucOTStartDate.DateText) AndAlso objOV11.CheckHolidayOrNot(ucOTEndDate.DateText) Then
                    '        If Weekday(Convert.ToDateTime(ucOTEndDate.DateText)) = 0 OrElse Weekday(Convert.ToDateTime(ucOTEndDate.DateText)) = 7 Then
                    '            ErrMsg += "不能假日連續加班" & vbNewLine
                    '        End If
                    '    End If
                End If
            Else    '非跨日直接判斷
                Dim ChkMsg As String = ""
                If Not objOV11.CheckNHolidayOTOrNot(lblCompID.Text.Trim, _EmpID, ucOTStartDate.DateText, ucOTEndDate.DateText, _OTTxnID, "Upd", ChkMsg) Then
                    ErrMsg += ChkMsg
                End If
            End If
        End If



        '2017/03/17-舊的判斷連續假日邏輯
        ''If Not OVBusinessCommon.IsNationalHoliday(ucOTStartDate.DateText) Then  '如果是國定假日則跳過判斷
        ''檢查連續加班如果開始日期為假日
        'If objOV11.CheckHolidayOrNot(ucOTStartDate.DateText) Then
        '    '檢查事後申報是否連續加班
        '    Dim a As DateTime = Convert.ToDateTime(ucOTStartDate.DateText).AddDays(-1)       '檢查前一天是否有存在資料庫
        '    Dim b As DateTime = Convert.ToDateTime(ucOTStartDate.DateText).AddDays(1)       '檢查後一天是否有存在資料庫

        '    If objOV11.CheckHolidayOrNot(a.ToString("yyyy/MM/dd")) Then
        '        dt = objOV11.QueryData("*", "OverTimeDeclaration", " AND OTStartDate='" + a.ToString("yyyy/MM/dd") + "' AND OTCompID='" + lblCompID.Text.Trim + "' AND OTEmpID='" + _EmpID + "' AND OTStatus in ('1','2','3') AND OTStartDate <> ( SELECT Code FROM AT_CodeMap WHERE Code = OTStartDate AND NotShowFlag = '0' AND TabName = 'NationalHolidayDefine' AND FldName ='HolidayDate')")
        '        If dt.Rows.Count > 0 Then
        '            '20170314-HR不擋僅通知-讓檢核能通過
        '            'Bsp.Utility.ShowMessage(Me, "不能假日連續加班")
        '            'Return False
        '            ErrMsg += "不能假日連續加班" & vbNewLine
        '        End If
        '    ElseIf objOV11.CheckHolidayOrNot(b.ToString("yyyy/MM/dd")) Then
        '        dt = objOV11.QueryData("*", "OverTimeDeclaration", " AND OTStartDate='" + b.ToString("yyyy/MM/dd") + "' AND OTCompID='" + lblCompID.Text.Trim + "' AND OTEmpID='" + _EmpID + "' AND OTStatus in ('1','2','3') AND OTStartDate <> ( SELECT Code FROM AT_CodeMap WHERE Code = OTStartDate AND NotShowFlag = '0' AND TabName = 'NationalHolidayDefine' AND FldName ='HolidayDate')")
        '        If dt.Rows.Count > 0 Then
        '            '20170314-HR不擋僅通知-讓檢核能通過
        '            'Bsp.Utility.ShowMessage(Me, "不能假日連續加班")
        '            'Return False
        '            ErrMsg += "不能假日連續加班" & vbNewLine
        '        End If
        '    End If

        '    '檢查事先申請是否連續加班
        '    If objOV11.CheckHolidayOrNot(a.ToString("yyyy/MM/dd")) Then
        '        dt = objOV11.QueryData("*", "OverTimeAdvance", " AND OTStartDate='" + a.ToString("yyyy/MM/dd") + "' AND OTCompID='" + lblCompID.Text.Trim + "' AND OTEmpID='" + _EmpID + "' AND OTStatus in ('1','2','3') AND OTStartDate <> ( SELECT Code FROM AT_CodeMap WHERE Code = OTStartDate AND NotShowFlag = '0' AND TabName = 'NationalHolidayDefine' AND FldName ='HolidayDate')")
        '        If dt.Rows.Count > 0 Then
        '            '20170314-HR不擋僅通知-讓檢核能通過
        '            'Bsp.Utility.ShowMessage(Me, "不能假日連續加班")
        '            'Return False
        '            ErrMsg += "不能假日連續加班" & vbNewLine
        '        End If
        '    ElseIf objOV11.CheckHolidayOrNot(b.ToString("yyyy/MM/dd")) Then
        '        dt = objOV11.QueryData("*", "OverTimeAdvance", " AND OTStartDate='" + b.ToString("yyyy/MM/dd") + "' AND OTCompID='" + lblCompID.Text.Trim + "' AND OTEmpID='" + _EmpID + "' AND OTStatus in ('1','2','3') AND OTStartDate <> ( SELECT Code FROM AT_CodeMap WHERE Code = OTStartDate AND NotShowFlag = '0' AND TabName = 'NationalHolidayDefine' AND FldName ='HolidayDate')")
        '        If dt.Rows.Count > 0 Then
        '            '20170314-HR不擋僅通知-讓檢核能通過
        '            'Bsp.Utility.ShowMessage(Me, "不能假日連續加班")
        '            'Return False
        '            ErrMsg += "不能假日連續加班" & vbNewLine
        '        End If

        '    End If
        'End If
        ''End If

        '檢查加班時數(含已核准)申報時數是否超過上限
        Dim message As String = ""
        If Not checkOverTimeIsOver(message) Then
            '20170314-HR不擋僅通知-讓檢核能通過
            'Bsp.Utility.ShowMessage(Me, message)
            ErrMsg += message
            If _dtPara.Rows(0)("DayLimitFlag").ToString() = "1" Then
                'Return False
                '20170314-HR不擋僅通知-讓檢核能通過
            End If
        End If

        '檢查每個月的上限
        '2017/03/16如果是跨月單，就拆成兩張單日單分別檢核
        If Month(Convert.ToDateTime(ucOTStartDate.DateText)) = Month(Convert.ToDateTime(ucOTEndDate.DateText)) Then     '無跨月
            If Not objOV11.checkMonthTime("OverTimeDeclaration", lblCompID.Text.Trim, _EmpID, ucOTStartDate.DateText, ucOTEndDate.DateText, Convert.ToDouble(_dtPara.Rows(0)("MonthLimitHour").ToString()), cntTotal, Convert.ToDouble(txtMealTime.Text.Trim), _
         cntStart, cntEnd, _OTFromAdvanceTxnId) Then
                '20170314-HR不擋僅通知-讓檢核能通過
                'Bsp.Utility.ShowMessage(Me, "每月上限加班申報時數為" + _dtPara.Rows(0)("MonthLimitHour") + "小時")
                'Return False
                ErrMsg += "每月上限加班申報時數為" + _dtPara(0)("MonthLimitHour") + "小時" & vbNewLine
                If _dtPara.Rows(0)("DayLimitFlag").ToString() = "1" Then
                    'Return False
                End If
            End If
        Else    '有跨月
            '計算跨日時數
            getCntStartAndCntEnd(cntStart, cntEnd)

            Dim mealOver As String = ""
            If chkMealFlag.Checked = True Then
                mealOver = objOV11.MealJudge(cntStart, Convert.ToDouble(txtMealTime.Text))
            Else
                'Result = StartDayMealFlag + "," + StartDayMealTime + "," + EndDayMealFlag + "," + EndDayMealTime
                mealOver = "0,0,0,0"
            End If

            '先算加班開始日期部分
            If Not objOV11.checkMonthTime("OverTimeDeclaration", lblCompID.Text.Trim, _EmpID, ucOTStartDate.DateText, ucOTStartDate.DateText, Convert.ToDouble(_dtPara.Rows(0)("MonthLimitHour").ToString()), cntStart, mealOver.Split(","c)(1), _
                    cntStart, 0, _OTFromAdvanceTxnId) Then
                '20170314-HR不擋僅通知-讓檢核能通過
                'Bsp.Utility.ShowMessage(Me, "每月上限加班申報時數為" + _dtPara.Rows(0)("MonthLimitHour") + "小時")
                'Return False
                ErrMsg += Month(Convert.ToDateTime(ucOTEndDate.DateText)) + "月之每月上限加班申報時數為" + _dtPara(0)("MonthLimitHour") + "小時" & vbNewLine
                If _dtPara.Rows(0)("DayLimitFlag").ToString() = "1" Then
                    'Return False
                End If
            End If

            '再算加班結束日期部分
            If Not objOV11.checkMonthTime("OverTimeDeclaration", lblCompID.Text.Trim, _EmpID, ucOTEndDate.DateText, ucOTEndDate.DateText, Convert.ToDouble(_dtPara.Rows(0)("MonthLimitHour").ToString()), cntEnd, mealOver.Split(","c)(3), _
                    cntEnd, 0, _OTFromAdvanceTxnId) Then
                '20170314-HR不擋僅通知-讓檢核能通過
                'Bsp.Utility.ShowMessage(Me, "每月上限加班申報時數為" + _dtPara.Rows(0)("MonthLimitHour") + "小時")
                'Return False
                ErrMsg += Month(Convert.ToDateTime(ucOTStartDate.DateText)) + "月之每月上限加班申報時數為" + _dtPara(0)("MonthLimitHour") + "小時" & vbNewLine
                If _dtPara.Rows(0)("DayLimitFlag").ToString() = "1" Then
                    'Return False
                End If
            End If
        End If

        '檢查連續上班是否超過限制
        Dim cnt As Integer = 0
        '檢查連續上班日
        If _dtPara.Rows(0)("OTMustCheck").ToString() = "0" Then
            Dim OTLimitDay As Integer = Convert.ToInt32(_dtPara.Rows(0)("OTLimitDay").ToString())
            sb.Clear()
            sb.Append("SELECT Convert(varchar,C.SysDate,111) as SysDate,ISNULL(O.OTStartDate,'') AS OTStartDate,C.Week,C.HolidayOrNot FROM (")
            sb.Append(" SELECT DISTINCT OTStartDate FROM OverTimeAdvance WHERE  OTCompID=" + Bsp.Utility.Quote(lblCompID.Text.Trim) + " AND OTEmpID=" + Bsp.Utility.Quote(lblOTEmpID.Text) + " AND OTStatus in ('2','3') AND OTStartDate>= DATEADD(DAY,-" + (OTLimitDay - 1).ToString() + "," + Bsp.Utility.Quote(ucOTStartDate.DateText) + ") AND  OTStartDate<= DATEADD(DAY," + (OTLimitDay - 1).ToString() + "," + Bsp.Utility.Quote(ucOTStartDate.DateText) + ")")
            sb.Append(" AND OTTxnID NOT IN('" + _OTFromAdvanceTxnId + "')")
            sb.Append(" AND OTTxnID NOT IN")
            sb.Append(" (SELECT OTFromAdvanceTxnId FROM OverTimeDeclaration WHERE OTCompID=" + Bsp.Utility.Quote(lblCompID.Text.Trim) + " AND OTEmpID=" + Bsp.Utility.Quote(lblOTEmpID.Text) + " AND OTStatus in ('2','3') AND OTFromAdvanceTxnId<>'')")
            sb.Append(" UNION")
            sb.Append(" SELECT DISTINCT OTStartDate FROM OverTimeDeclaration WHERE  OTCompID=" + Bsp.Utility.Quote(lblCompID.Text.Trim) + " AND OTEmpID=" + Bsp.Utility.Quote(lblOTEmpID.Text) + " AND OTStatus in ('2','3') AND OTStartDate>= DATEADD(DAY,-" + (OTLimitDay - 1).ToString() + "," + Bsp.Utility.Quote(ucOTStartDate.DateText) + ") AND  OTStartDate<= DATEADD(DAY," + (OTLimitDay - 1).ToString() + "," + Bsp.Utility.Quote(ucOTStartDate.DateText) + ")) O")
            sb.Append(" FULL OUTER JOIN(")
            sb.Append(" SELECT * FROM " + _eHRMSDB_ITRD + ".[dbo].[Calendar] WHERE  CompID=" + Bsp.Utility.Quote(lblCompID.Text.Trim) + " AND SysDate>= DATEADD(DAY,-" + (OTLimitDay - 1).ToString() + "," + Bsp.Utility.Quote(ucOTStartDate.DateText) + ") AND  SysDate<= DATEADD(DAY," + (OTLimitDay - 1).ToString() + "," + Bsp.Utility.Quote(ucOTStartDate.DateText) + ")) C ON O.OTStartDate=C.SysDate")

            sb.Append(" ORDER BY C.SysDate ASC")
            Using dtOTday As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, sb.ToString(), "AattendantDB").Tables(0)
                For j As Integer = 0 To dtOTday.Rows.Count - 1
                    If dtOTday.Rows(j)("SysDate").ToString() = ucOTStartDate.DateText OrElse dtOTday.Rows(j)("SysDate").ToString() = ucOTEndDate.DateText Then  '本單
                        cnt += 1
                    Else
                        If dtOTday.Rows(j)("HolidayOrNot").ToString() = "0" Then
                            cnt += 1
                        Else
                            If Not String.IsNullOrEmpty(dtOTday.Rows(j)("OTStartDate").ToString()) Then
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
        sb.Clear()
        sb.Append(" SELECT BeginTime,EndTime FROM OverTime_BK  WHERE CompID= '" + lblCompID.Text.Trim + "' And EmpID='" + _EmpID + "' ")
        If ucOTStartDate.DateText = ucOTEndDate.DateText Then
            sb.Append(" AND Convert(varchar,OTDate,111) ='" + ucOTStartDate.DateText + "'")
        Else
            sb.Append(" AND Convert(varchar,OTDate,111) IN ('" + ucOTStartDate.DateText + "','" + ucOTEndDate.DateText + "')  ")
        End If
        dt = Bsp.DB.ExecuteDataSet(CommandType.Text, sb.ToString(), "AattendantDB").Tables(0)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                '起迄時間都有重疊
                If (dt.Rows(i)("BeginTime").ToString() = ucOTStartTime.ucDefaultSelectedHH + ucOTStartTime.ucDefaultSelectedMM) AndAlso (dt.Rows(i)("EndTime").ToString() = ucOTEndTime.ucDefaultSelectedHH + ucOTEndTime.ucDefaultSelectedMM) Then
                    Bsp.Utility.ShowMessage(Me, "該時段已存在人力資源-加班系統，請由人力資源-加班進行查詢")
                    Return False
                End If
                '開始時間小於資料庫開始時間
                If Convert.ToInt32(dt.Rows(i)("BeginTime").ToString()) > starttime Then
                    '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                    If endtime > Convert.ToInt32(dt.Rows(i)("BeginTime").ToString()) AndAlso endtime < Convert.ToInt32(dt.Rows(i)("EndTime").ToString()) Then
                        Bsp.Utility.ShowMessage(Me, "該時段已存在人力資源-加班系統，請由人力資源-加班進行查詢")
                        Return False
                        '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                    ElseIf endtime > Convert.ToInt32(dt.Rows(i)("BeginTime").ToString()) AndAlso endtime = Convert.ToInt32(dt.Rows(i)("EndTime").ToString()) Then
                        Bsp.Utility.ShowMessage(Me, "該時段已存在人力資源-加班系統，請由人力資源-加班進行查詢")
                        Return False
                        '結束時間大於資料庫的開始時間，大於資料庫的結束時間
                    ElseIf endtime > Convert.ToInt32(dt.Rows(i)("BeginTime").ToString()) AndAlso endtime > Convert.ToInt32(dt.Rows(i)("EndTime").ToString()) Then
                        Bsp.Utility.ShowMessage(Me, "該時段已存在人力資源-加班系統，請由人力資源-加班進行查詢")
                        Return False
                    End If
                End If
                '開始時間等於資料庫開始時間
                If Convert.ToInt32(dt.Rows(i)("BeginTime").ToString()) = starttime Then
                    '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                    If endtime > Convert.ToInt32(dt.Rows(i)("BeginTime").ToString()) AndAlso endtime < Convert.ToInt32(dt.Rows(i)("EndTime").ToString()) Then
                        Bsp.Utility.ShowMessage(Me, "該時段已存在人力資源-加班系統，請由人力資源-加班進行查詢")
                        Return False
                        '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                    ElseIf endtime > Convert.ToInt32(dt.Rows(i)("BeginTime").ToString()) AndAlso endtime = Convert.ToInt32(dt.Rows(i)("EndTime").ToString()) Then
                        Bsp.Utility.ShowMessage(Me, "該時段已存在人力資源-加班系統，請由人力資源-加班進行查詢")
                        Return False
                        '結束時間大於資料庫的開始時間，大於資料庫的結束時間
                    ElseIf endtime > Convert.ToInt32(dt.Rows(i)("BeginTime").ToString()) AndAlso endtime > Convert.ToInt32(dt.Rows(i)("EndTime").ToString()) Then
                        Bsp.Utility.ShowMessage(Me, "該時段已存在人力資源-加班系統，請由人力資源-加班進行查詢")
                        Return False
                    End If
                End If
                '開始時間大於資料庫開始時間
                If Convert.ToInt32(dt.Rows(i)("BeginTime").ToString()) < starttime Then
                    '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                    If endtime > Convert.ToInt32(dt.Rows(i)("BeginTime").ToString()) AndAlso endtime < Convert.ToInt32(dt.Rows(i)("EndTime").ToString()) Then
                        Bsp.Utility.ShowMessage(Me, "該時段已存在人力資源-加班系統，請由人力資源-加班進行查詢")
                        Return False
                        '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                    ElseIf endtime > Convert.ToInt32(dt.Rows(i)("BeginTime").ToString()) AndAlso endtime = Convert.ToInt32(dt.Rows(i)("EndTime").ToString()) Then
                        Bsp.Utility.ShowMessage(Me, "該時段已存在人力資源-加班系統，請由人力資源-加班進行查詢")
                        Return False
                    ElseIf Convert.ToInt32(dt.Rows(i)("EndTime").ToString()) > starttime Then
                        Bsp.Utility.ShowMessage(Me, "該時段已存在人力資源-加班系統，請由人力資源-加班進行查詢")
                        Return False
                    End If
                End If
            Next
        End If

        '檢核時間重疊(NaturalDisasterByCity)
        sb.Clear()
        sb.Append(" SELECT BeginTime,EndTime FROM NaturalDisasterByCity  WHERE WorkSiteID='" + _WorkSiteID + "' AND CompID= '" + lblCompID.Text.Trim + "' ")
        If ucOTStartDate.DateText = ucOTEndDate.DateText Then
            sb.Append(" AND DisasterStartDate ='" + ucOTStartDate.DateText + "'")
        Else
            sb.Append(" AND DisasterStartDate IN ('" + ucOTStartDate.DateText + "','" + ucOTEndDate.DateText + "')  ")
        End If
        dt = Bsp.DB.ExecuteDataSet(CommandType.Text, sb.ToString(), "AattendantDB").Tables(0)
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                '起迄時間都有重疊
                If (dt.Rows(i)("BeginTime").ToString() = ucOTStartTime.ucDefaultSelectedHH + ucOTStartTime.ucDefaultSelectedMM) AndAlso (dt.Rows(i)("EndTime").ToString() = ucOTEndTime.ucDefaultSelectedHH + ucOTEndTime.ucDefaultSelectedMM) Then
                    Bsp.Utility.ShowMessage(Me, "留守時段重複")
                    Return False
                End If
                '開始時間小於資料庫開始時間
                If Convert.ToInt32(dt.Rows(i)("BeginTime").ToString()) > starttime Then
                    '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                    If endtime > Convert.ToInt32(dt.Rows(i)("BeginTime").ToString()) AndAlso endtime < Convert.ToInt32(dt.Rows(i)("EndTime").ToString()) Then
                        Bsp.Utility.ShowMessage(Me, "留守時段重複")
                        Return False
                        '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                    ElseIf endtime > Convert.ToInt32(dt.Rows(i)("BeginTime").ToString()) AndAlso endtime = Convert.ToInt32(dt.Rows(i)("EndTime").ToString()) Then
                        Bsp.Utility.ShowMessage(Me, "留守時段重複")
                        Return False
                        '結束時間大於資料庫的開始時間，大於資料庫的結束時間
                    ElseIf endtime > Convert.ToInt32(dt.Rows(i)("BeginTime").ToString()) AndAlso endtime > Convert.ToInt32(dt.Rows(i)("EndTime").ToString()) Then
                        Bsp.Utility.ShowMessage(Me, "留守時段重複")
                        Return False
                    End If
                End If
                '開始時間等於資料庫開始時間
                If Convert.ToInt32(dt.Rows(i)("BeginTime").ToString()) = starttime Then
                    '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                    If endtime > Convert.ToInt32(dt.Rows(i)("BeginTime").ToString()) AndAlso endtime < Convert.ToInt32(dt.Rows(i)("EndTime").ToString()) Then
                        Bsp.Utility.ShowMessage(Me, "留守時段重複")
                        Return False
                        '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                    ElseIf endtime > Convert.ToInt32(dt.Rows(i)("BeginTime").ToString()) AndAlso endtime = Convert.ToInt32(dt.Rows(i)("EndTime").ToString()) Then
                        Bsp.Utility.ShowMessage(Me, "留守時段重複")
                        Return False
                        '結束時間大於資料庫的開始時間，大於資料庫的結束時間
                    ElseIf endtime > Convert.ToInt32(dt.Rows(i)("BeginTime").ToString()) AndAlso endtime > Convert.ToInt32(dt.Rows(i)("EndTime").ToString()) Then
                        Bsp.Utility.ShowMessage(Me, "留守時段重複")
                        Return False
                    End If
                End If
                '開始時間大於資料庫開始時間
                If Convert.ToInt32(dt.Rows(i)("BeginTime").ToString()) < starttime Then
                    '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                    If endtime > Convert.ToInt32(dt.Rows(i)("BeginTime").ToString()) AndAlso endtime < Convert.ToInt32(dt.Rows(i)("EndTime").ToString()) Then
                        Bsp.Utility.ShowMessage(Me, "留守時段重複")
                        Return False
                        '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                    ElseIf endtime > Convert.ToInt32(dt.Rows(i)("BeginTime").ToString()) AndAlso endtime = Convert.ToInt32(dt.Rows(i)("EndTime").ToString()) Then
                        Bsp.Utility.ShowMessage(Me, "留守時段重複")
                        Return False
                    ElseIf Convert.ToInt32(dt.Rows(i)("EndTime").ToString()) > starttime Then
                        Bsp.Utility.ShowMessage(Me, "留守時段重複")
                        Return False
                    End If
                End If
            Next
        End If

        '檢核時間重疊(NaturalDisasterByCity)
        sb.Clear()
        sb.Append(" SELECT BeginTime,EndTime,LEFT(BeginTime,2) AS StartTimeHr,RIGHT(BeginTime,2) AS StartTimeM,LEFT(EndTime,2) AS EndTimeHr,RIGHT(EndTime,2) AS EndTimeM FROM NaturalDisasterByEmp  WHERE EmpID='" + _EmpID + "' AND CompID= '" + lblCompID.Text.Trim + "' ")
        If ucOTStartDate.DateText = ucOTEndDate.DateText Then
            sb.Append(" AND DisasterStartDate ='" + ucOTStartDate.DateText + "'")
        Else
            sb.Append(" AND DisasterStartDate IN ('" + ucOTStartDate.DateText + "','" + ucOTEndDate.DateText + "')  ")
        End If
        dt = Bsp.DB.ExecuteDataSet(CommandType.Text, sb.ToString(), "AattendantDB").Tables(0)
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                '起迄時間都有重疊
                If (dt.Rows(i)("BeginTime").ToString() = ucOTStartTime.ucDefaultSelectedHH + ucOTStartTime.ucDefaultSelectedMM) AndAlso (dt.Rows(i)("EndTime").ToString() = ucOTEndTime.ucDefaultSelectedHH + ucOTEndTime.ucDefaultSelectedMM) Then
                    Bsp.Utility.ShowMessage(Me, "留守時段重複")
                    Return False
                End If
                '開始時間小於資料庫開始時間
                If Convert.ToInt32(dt.Rows(i)("BeginTime").ToString()) > starttime Then
                    '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                    If endtime > Convert.ToInt32(dt.Rows(i)("BeginTime").ToString()) AndAlso endtime < Convert.ToInt32(dt.Rows(i)("EndTime").ToString()) Then
                        Bsp.Utility.ShowMessage(Me, "留守時段重複")
                        Return False
                        '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                    ElseIf endtime > Convert.ToInt32(dt.Rows(i)("BeginTime").ToString()) AndAlso endtime = Convert.ToInt32(dt.Rows(i)("EndTime").ToString()) Then
                        Bsp.Utility.ShowMessage(Me, "留守時段重複")
                        Return False
                        '結束時間大於資料庫的開始時間，大於資料庫的結束時間
                    ElseIf endtime > Convert.ToInt32(dt.Rows(i)("BeginTime").ToString()) AndAlso endtime > Convert.ToInt32(dt.Rows(i)("EndTime").ToString()) Then
                        Bsp.Utility.ShowMessage(Me, "留守時段重複")
                        Return False
                    End If
                End If
                '開始時間等於資料庫開始時間
                If Convert.ToInt32(dt.Rows(i)("BeginTime").ToString()) = starttime Then
                    '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                    If endtime > Convert.ToInt32(dt.Rows(i)("BeginTime").ToString()) AndAlso endtime < Convert.ToInt32(dt.Rows(i)("EndTime").ToString()) Then
                        Bsp.Utility.ShowMessage(Me, "留守時段重複")
                        Return False
                        '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                    ElseIf endtime > Convert.ToInt32(dt.Rows(i)("BeginTime").ToString()) AndAlso endtime = Convert.ToInt32(dt.Rows(i)("EndTime").ToString()) Then
                        Bsp.Utility.ShowMessage(Me, "留守時段重複")
                        Return False
                        '結束時間大於資料庫的開始時間，大於資料庫的結束時間
                    ElseIf endtime > Convert.ToInt32(dt.Rows(i)("BeginTime").ToString()) AndAlso endtime > Convert.ToInt32(dt.Rows(i)("EndTime").ToString()) Then
                        Bsp.Utility.ShowMessage(Me, "留守時段重複")
                        Return False
                    End If
                End If
                '開始時間大於資料庫開始時間
                If Convert.ToInt32(dt.Rows(i)("BeginTime").ToString()) < starttime Then
                    '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                    If endtime > Convert.ToInt32(dt.Rows(i)("BeginTime").ToString()) AndAlso endtime < Convert.ToInt32(dt.Rows(i)("EndTime").ToString()) Then
                        Bsp.Utility.ShowMessage(Me, "留守時段重複")
                        Return False
                        '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                    ElseIf endtime > Convert.ToInt32(dt.Rows(i)("BeginTime").ToString()) AndAlso endtime = Convert.ToInt32(dt.Rows(i)("EndTime").ToString()) Then
                        Bsp.Utility.ShowMessage(Me, "留守時段重複")
                        Return False
                    ElseIf Convert.ToInt32(dt.Rows(i)("EndTime").ToString()) > starttime Then
                        Bsp.Utility.ShowMessage(Me, "留守時段重複")
                        Return False
                    End If
                End If
            Next
        End If

        '檢核時間重疊(事後申報)
        If ucOTStartDate.DateText = ucOTEndDate.DateText Then
            sb.Clear()
            sb.Append(" SELECT OTStartTime,OTEndTime,LEFT(OTStartTime,2) AS StartTimeHr,RIGHT(OTStartTime,2) AS StartTimeM,LEFT(OTEndTime,2) AS EndTimeHr,RIGHT(OTEndTime,2) AS EndTimeM FROM OverTimeDeclaration  WHERE OTStatus in ('1','2','3') AND OTEmpID='" + _EmpID + "' AND OTStartDate='" + ucOTStartDate.DateText + "' AND OTEndDate='" + ucOTEndDate.DateText + "' AND OTCompID='" + lblCompID.Text.Trim + "' ")
            sb.Append(" AND NOT(OTCompID='" + lblCompID.Text.Trim + "' AND OTEmpID='" + _EmpID + "' AND OTTxnID='" + _OTTxnID + "') ")
            dt = Bsp.DB.ExecuteDataSet(CommandType.Text, sb.ToString(), "AattendantDB").Tables(0)

            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    '起迄時間都有重疊 
                    If (dt.Rows(i)("OTStartTime").ToString() = ucOTStartTime.ucDefaultSelectedHH + ucOTStartTime.ucDefaultSelectedMM) AndAlso (dt.Rows(i)("OTEndTime").ToString() = ucOTEndTime.ucDefaultSelectedHH + ucOTEndTime.ucDefaultSelectedMM) Then
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
                            '結束時間大於資料庫的開始時間，大於資料庫的結束時間
                            'else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime > Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            '{
                            '    lblMsg.Text = "您欲申報的加班時間區間已有紀錄10";
                            '    return;
                            '}
                        ElseIf Convert.ToInt32(dt.Rows(i)("OTEndTime").ToString()) > starttime Then
                            Bsp.Utility.ShowMessage(Me, "您欲申報的加班時間區間已有紀錄")
                            Return False
                        End If
                    End If
                Next
            End If
        Else
            sb.Clear()
            sb.Append("SELECT OTTxnID,OTStartDate,OTStartTime,OTEndTime,LEFT(OTStartTime,2) AS StartTimeHr,RIGHT(OTStartTime,2) AS StartTimeM,LEFT(OTEndTime,2) AS EndTimeHr,RIGHT(OTEndTime,2) AS EndTimeM FROM OverTimeDeclaration WHERE OTEmpID='" + _EmpID + "' AND OTStatus IN ('1','2','3') AND OTStartDate='" + ucOTStartDate.DateText + "' AND OTCompID='" + lblCompID.Text.Trim + "' ")
            sb.Append(" AND NOT(OTCompID='" + lblCompID.Text.Trim + "' AND OTEmpID='" + _EmpID + "' AND OTTxnID='" + _OTTxnID + "')")
            sb.Append(" UNION ALL")
            sb.Append(" SELECT OTTxnID,OTStartDate,OTStartTime,OTEndTime,LEFT(OTStartTime,2) AS StartTimeHr,RIGHT(OTStartTime,2) AS StartTimeM,LEFT(OTEndTime,2) AS EndTimeHr,RIGHT(OTEndTime,2) AS EndTimeM FROM OverTimeDeclaration WHERE OTEmpID='" + _EmpID + "' AND OTStatus IN ('1','2','3') AND OTStartDate='" + ucOTEndDate.DateText + "' AND OTCompID='" + lblCompID.Text.Trim + "' ")
            sb.Append(" AND NOT(OTCompID='" + lblCompID.Text.Trim + "' AND OTEmpID='" + _EmpID + "' AND OTTxnID='" + _OTTxnID + "')")
            dt = Bsp.DB.ExecuteDataSet(CommandType.Text, sb.ToString(), "AattendantDB").Tables(0)

            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)("OTStartDate").ToString() = ucOTStartDate.DateText Then
                        '起迄日重疊
                        endtime = 2359
                        starttime = Convert.ToInt32(ucOTStartTime.ucDefaultSelectedHH + ucOTStartTime.ucDefaultSelectedMM)
                        Dim strTxnID As String = objOV11.QueryColumn("OTEndTime", "OverTimeDeclaration", " AND OTTxnID='" + dt.Rows(i)("OTTxnID") + "' AND OTSeqNo='2'")
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
        '檢核時間重疊(預先申請)
        If ucOTStartDate.DateText = ucOTEndDate.DateText Then
            sb.Clear()
            sb.Append(" SELECT OTStartTime,OTEndTime,LEFT(OTStartTime,2) AS StartTimeHr,RIGHT(OTStartTime,2) AS StartTimeM,LEFT(OTEndTime,2) AS EndTimeHr,RIGHT(OTEndTime,2) AS EndTimeM FROM OverTimeAdvance  WHERE OTStatus in ('1','2','3') AND OTEmpID='" + _EmpID + "' AND OTStartDate='" + ucOTStartDate.DateText + "' AND OTEndDate='" + ucOTEndDate.DateText + "' AND OTCompID='" + lblCompID.Text.Trim + "' ")
            'sb.Append(" AND NOT(OTCompID='" + UserProfile.SelectCompRoleID.Trim + "' AND OTEmpID='" + _EmpID + "' AND OTTxnID='" + _OTFromAdvanceTxnId + "') ")
            sb.Append(" AND OTTxnID NOT IN (SELECT OTFromAdvanceTxnId FROM OverTimeDeclaration WHERE OTCompID='" + lblCompID.Text.Trim + "' AND OTEmpID='" + _EmpID + "' AND OTStartDate='" + ucOTStartDate.DateText + "' AND OTStatus in ('1','2','3')) ")
            dt = Bsp.DB.ExecuteDataSet(CommandType.Text, sb.ToString(), "AattendantDB").Tables(0)
            starttime = Convert.ToInt32(ucOTStartTime.ucDefaultSelectedHH + ucOTStartTime.ucDefaultSelectedMM)
            endtime = Convert.ToInt32(ucOTEndTime.ucDefaultSelectedHH + ucOTEndTime.ucDefaultSelectedMM)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    '起迄時間都有重疊 
                    If (dt.Rows(i)("OTStartTime").ToString() = ucOTStartTime.ucDefaultSelectedHH + ucOTStartTime.ucDefaultSelectedMM) AndAlso (dt.Rows(i)("OTEndTime").ToString() = ucOTEndTime.ucDefaultSelectedHH + ucOTEndTime.ucDefaultSelectedMM) Then
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
                Next
            End If
        Else
            sb.Clear()
            sb.Append("SELECT OTTxnID,OTStartDate,OTStartTime,OTEndTime,LEFT(OTStartTime,2) AS StartTimeHr,RIGHT(OTStartTime,2) AS StartTimeM,LEFT(OTEndTime,2) AS EndTimeHr,RIGHT(OTEndTime,2) AS EndTimeM FROM OverTimeAdvance WHERE OTEmpID='" + _EmpID + "' AND OTStatus IN ('1','2','3') AND OTStartDate='" + ucOTStartDate.DateText + "' AND OTCompID='" + lblCompID.Text.Trim + "' ")
            'sb.Append(" AND NOT(OTCompID='" + UserProfile.SelectCompRoleID.Trim + "' AND OTEmpID='" + _EmpID + "' AND OTTxnID='" + _OTFromAdvanceTxnId + "') ")
            sb.Append(" AND OTTxnID NOT IN (SELECT OTFromAdvanceTxnId FROM OverTimeDeclaration WHERE OTCompID='" + lblCompID.Text.Trim + "' AND OTEmpID='" + _EmpID + "' AND OTStartDate='" + ucOTStartDate.DateText + "' AND OTStatus in ('1','2','3')) ")
            sb.Append(" UNION ALL")
            sb.Append(" SELECT OTTxnID,OTStartDate,OTStartTime,OTEndTime,LEFT(OTStartTime,2) AS StartTimeHr,RIGHT(OTStartTime,2) AS StartTimeM,LEFT(OTEndTime,2) AS EndTimeHr,RIGHT(OTEndTime,2) AS EndTimeM FROM OverTimeAdvance WHERE OTEmpID='" + _EmpID + "' AND OTStatus IN ('1','2','3') AND OTStartDate='" + ucOTEndDate.DateText + "' AND OTCompID='" + lblCompID.Text.Trim + "' ")
            'sb.Append(" AND NOT(OTCompID='" + UserProfile.SelectCompRoleID.Trim + "' AND OTEmpID='" + _EmpID + "' AND OTTxnID='" + _OTFromAdvanceTxnId + "') ")
            sb.Append(" AND OTTxnID NOT IN (SELECT OTFromAdvanceTxnId FROM OverTimeDeclaration WHERE OTCompID='" + lblCompID.Text.Trim + "' AND OTEmpID='" + _EmpID + "' AND OTStartDate='" + ucOTEndDate.DateText + "' AND OTStatus in ('1','2','3')) ")
            dt = Bsp.DB.ExecuteDataSet(CommandType.Text, sb.ToString(), "AattendantDB").Tables(0)

            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)("OTStartDate").ToString() = ucOTStartDate.DateText Then
                        '起迄日重疊
                        endtime = 2359
                        starttime = Convert.ToInt32(ucOTStartTime.ucDefaultSelectedHH + ucOTStartTime.ucDefaultSelectedMM)
                        Dim strTxnID As String = objOV11.QueryColumn("OTEndTime", "OverTimeDeclaration", " AND OTTxnID='" + dt.Rows(i)("OTTxnID") + "' AND OTSeqNo='2'")
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

        If ErrMsg <> "" Then
            Bsp.Utility.ShowMessage(Me, ErrMsg)
        End If

        Return True
    End Function
#End Region

#Region "隱藏Button-暫存與送簽提示訊息"
    ''' <summary>
    ''' 檢核後的提示訊息
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub funPopupNotify()
        Select Case ViewState("Param")
            Case "btnUpdate"    '是暫存
                Bsp.Utility.RunClientScript(Me.Page, "TempSaveAsk();")
                'Return False
            Case "btnExecutes"  '是送簽
                Bsp.Utility.RunClientScript(Me.Page, "SubmitAsk();")
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
                Case "btnUpdate"    '是暫存
                    Bsp.Utility.RunClientScript(Me.Page, "TempSave();")
                    'Return False
                Case "btnExecutes"  '是送簽
                    Bsp.Utility.RunClientScript(Me.Page, "Submit();")
                    'Return False
                Case Else           '是來亂的
                    Debug.Print("btnTransportAsk_Click()==>有人來亂,Param = " + ViewState("Param").ToString())
                    'Return False
            End Select
        End If
    End Sub

    ''' <summary>
    ''' 執行暫存
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnTempSave_Click(sender As Object, e As System.EventArgs) Handles btnTempSave.Click
        SaveData("1")
    End Sub

    ''' <summary>
    ''' 執行送簽
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click
        SaveData("2")
    End Sub

#End Region

#Region "附件相關"
    ''' <summary>
    ''' 下載附件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnDownloadAttach_Click(sender As Object, e As System.EventArgs) Handles btnDownloadAttach.Click
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine("Select * From AttachInfo Where AttachID = '" + labOTAttachment.Value + "'")
        Dim dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "AattendantDB").Tables(0)
        Dim FILEBODY As Object = Bsp.DB.ExecuteScalar(strSQL.ToString, "AattendantDB")
        Dim dtCloned As DataTable = dt.Clone()
        Dim FileName As String = ""
        Dim fileSize As Integer

        Dim buffer As Byte()
        If dt.Rows.Count > 0 Then
            For Each item As DataRow In dt.Rows
                FILEBODY = item("FileBody")
                FileName = item("FileName")
                fileSize = item("FileSize")
                If fileSize > 0 Then
                    buffer = CType(FILEBODY, Byte())
                    dtCloned.ImportRow(item)
                    ''2017/03/01-轉成UTF8來解決中文亂碼問題
                    'Dim UTF8FileName As String      '轉碼後的FileaName
                    'Dim b As Byte() = Encoding.Default.GetBytes(FileName)   '將字串轉為byte[]
                    'Debug.Print("Converted str = " + Encoding.Default.GetString(b))     '驗證轉碼後的字串,仍再正確的顯示.
                    'Dim u As Byte() = Encoding.Convert(Encoding.Default, Encoding.UTF8, b)  '進行轉碼,參數1,來源編碼,參數二,目標編碼,參數三,欲編碼變數
                    'UTF8FileName = Encoding.UTF8.GetString(u)   '將轉碼結果丟進去
                    'Debug.Print("UTF-8 str = " + Encoding.UTF8.GetString(u))       '顯示轉為UTF8後,仍能正確的顯示字串
                    ''Util.ExportBinary(buffer, FileName)
                    'Util.ExportBinary(buffer, UTF8FileName)
                    DownloadAttach(buffer, FileName)
                End If

            Next
        End If
    End Sub

    ''' <summary>
    ''' 下載附件的子Funct
    ''' </summary>
    ''' <param name="binFileBody"></param>
    ''' <param name="FileName"></param>
    ''' <remarks>使用HeaderEncoding來解決中文亂碼問題</remarks>
    Public Shared Sub DownloadAttach(ByVal binFileBody As Byte(), ByVal FileName As String)
        If String.IsNullOrEmpty(FileName) Then
            FileName = "DownloadFile"
        End If

        If binFileBody.Length > 0 Then
            'If System.Web.HttpContext.Current.Request.Browser.Browser = "IE" Then
            FileName = System.Web.HttpContext.Current.Server.UrlPathEncode(FileName)
            'End If

            Dim response As System.Web.HttpResponse = System.Web.HttpContext.Current.Response

            Try
                response.ClearHeaders()
                response.Clear()
                response.Charset = "utf-8"
                response.HeaderEncoding = System.Text.Encoding.GetEncoding("big5")
                response.AddHeader("Content-type", "Application/octet-stream")
                response.AddHeader("Content-Disposition", "attachment; filename=" + FileName)
                response.AddHeader("Content-Length", binFileBody.Length.ToString())
                response.BinaryWrite(binFileBody)
                response.Flush()
            Catch ex As Exception
                Debug.Print("下載失敗 :" + ex.Message)
            Finally
                response.End()
            End Try
        End If
    End Sub

    ''' <summary>
    ''' 查詢附件的檔案名稱
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub getAttachName()
        Dim objOV11 As New OV1_2
        Using dt As DataTable = objOV11.QueryData("ISNULL(FileName,'') AS FileName", "AttachInfo", "AND FileSize>0 AND AttachID=" + Bsp.Utility.Quote(_AttachID))
            _AttachID = ViewState("attach").ToString()
            hidAttachID.Value = _AttachID
            If dt.Rows.Count > 0 Then
                lblAttachName.Text = "附件檔名：" + dt.Rows(0).Item("FileName").ToString()
                'btnDownloadAttach.Visible = True
            Else
                lblAttachName.Text = "(目前無附件)"
                btnDownloadAttach.Visible = False
            End If
        End Using
    End Sub

    ''' <summary>
    ''' 更新附件名
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub updateAttachName_Click(sender As Object, e As System.EventArgs) Handles updateAttachName.Click
        getAttachName()
    End Sub

    ''' <summary>
    ''' 暫存後更新附件
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub LoadAttch()
        '附件Attach  
        If String.IsNullOrEmpty(_AttachID) Then
            _AttachID = "test" + UserProfile.UserID.Trim + Guid.NewGuid().ToString()
            hidAttachID.Value = _AttachID
        End If
        Dim strAttachAdminURL As String
        Dim strAttachAdminBaseURL As String = ConfigurationManager.AppSettings("AattendantWebPath") + "HandlerForOverTime/SSORecvModeForOverTime.aspx?UserID=" + UserProfile.UserID.Trim + "&SystemID=OT&TxnID=OV1101&ReturnUrl=%2FUtil%2FAttachAdmin.aspx?AttachDB={0}%26AttachID={1}%26AttachFileMaxQty={2}%26AttachFileMaxKB={3}%26AttachFileTotKB={4}%26AttachFileExtList={5}"
        Dim strAttachDownloadURL As String
        Dim strAttachDownloadBaseURL As String = Util._AttachDownloadUrl + "?AttachDB={0}&AttachID={1}"

        strAttachAdminURL = String.Format(strAttachAdminBaseURL, Config_AattendantDBName, ViewState("attach").ToString(), "1", "3072", "3072", "")
        strAttachDownloadURL = String.Format(strAttachDownloadBaseURL, Config_AattendantDBName, ViewState("attach").ToString())
        frameAttach.Value = strAttachAdminURL
        getAttachName()

    End Sub

#End Region

#Region "日期與時間連動"
    ''' <summary>
    ''' 開始日期
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ValidStartDate_Click(sender As Object, e As System.EventArgs) Handles ValidStartDate.Click
        Dim objOV11 As New OV1_2
        If ViewState.Item("DoUpdate") = "Y" Then
            initalData()
            ucOTEndDate.DateText = ucOTStartDate.DateText
            If ucOTStartDate.DateText <> "" And ucOTEndDate.DateText <> "" Then
                '加班申報範圍
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
                If lblOTEmpID.Text <> "" Then
                    '檢查到職日以前不可以加
                    Try
                        Convert.ToDateTime(_EmpDate)
                    Catch ex As Exception
                        _EmpDate = "1900/01/01"
                    End Try

                    If (Convert.ToDateTime(ucOTStartDate.DateText) < Convert.ToDateTime(_EmpDate)) Then
                        Bsp.Utility.ShowMessage(Me, "到職日(" + Convert.ToDateTime(_EmpDate).ToString("yyyy/MM/dd") + ")以前無法申報")
                        ucOTStartDate.DateText = Date.Now.ToString("yyyy/MM/dd")
                        ucOTEndDate.DateText = ucOTStartDate.DateText
                        Return
                    Else
                        '計算總計
                        SignData()
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
        Dim objOV11 As New OV1_2

        If ViewState.Item("DoUpdate") = "Y" Then
            If ucOTStartTime.ucDefaultSelectedHH <> "請選擇" AndAlso ucOTStartTime.ucDefaultSelectedMM <> "請選擇" AndAlso ucOTEndTime.ucDefaultSelectedHH <> "請選擇" AndAlso ucOTEndTime.ucDefaultSelectedMM <> "請選擇" Then
                If ucOTStartDate.DateText <> "" And ucOTEndDate.DateText <> "" Then
                    '如果結束日期選擇未來日期
                    If Date.Now.Date < Convert.ToDateTime(ucOTEndDate.DateText) Then
                        Bsp.Utility.ShowMessage(Me, "加班日期不可以選擇未來日期")
                        ucOTStartDate.DateText = Date.Now.ToString("yyyy/MM/dd")
                        ucOTEndDate.DateText = ucOTStartDate.DateText
                        Return
                    End If

                    Dim total As TimeSpan = (Convert.ToDateTime(ucOTStartDate.DateText)).Subtract(Convert.ToDateTime(ucOTStartDate.DateText))
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
                    If lblOTEmpID.Text <> "" Then
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
                            SignData()
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

                    If lblOTEmpID.Text <> "" Then
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
                            SignData()
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
    ''' 開始時間
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ValidStartTime_Click(sender As Object, e As System.EventArgs) Handles ValidStartTime.Click
        Dim objOV11 As New OV1_2
        If ViewState.Item("DoUpdate") = "Y" Then
            Dim Sex As String = objOV11.QueryColumn("Sex", _eHRMSDB_ITRD + ".[dbo].[Personal] ", " AND EmpID='" + _EmpID + "'")
            If Sex <> "" And Sex = "2" Then
                If ucOTStartDate.DateText <> "" AndAlso ucOTEndDate.DateText <> "" AndAlso ucOTStartTime.ucDefaultSelectedHH <> "請選擇" AndAlso ucOTStartTime.ucDefaultSelectedMM <> "請選擇" Then
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
                        ucOTStartTime.ucDefaultSelectedHH = "請選擇"
                        ucOTStartTime.ucDefaultSelectedMM = "請選擇"
                        pnlCalcTotalTime.Visible = False
                        litTable.Text = ""
                        lblOTTotalTime.Text = ""
                        txtOTTotalDescript.Visible = False
                        lblStartSex.Visible = False
                        Return
                    ElseIf Convert.ToInt32(ucOTStartTime.ucDefaultSelectedHH) = Convert.ToInt32(Date.Now.Hour) And Convert.ToInt32(ucOTStartTime.ucDefaultSelectedMM) > Convert.ToInt32(Date.Now.Minute) Then
                        Bsp.Utility.ShowMessage(Me, "不可以申報未來時間點")
                        ucOTStartTime.ucDefaultSelectedHH = "請選擇"
                        ucOTStartTime.ucDefaultSelectedMM = "請選擇"
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

                If Date.Now.Date = Convert.ToDateTime(ucOTEndDate.DateText) Then '如果加班迄日等於現在日期
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
            If ucOTEndTime.ucDefaultSelectedHH <> "請選擇" And ucOTEndTime.ucDefaultSelectedMM <> "請選擇" Then
                ValidEndTime_Click(Nothing, EventArgs.Empty)
            End If
        End If
    End Sub

    ''' <summary>
    ''' 結束時間
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ValidEndTime_Click(sender As Object, e As System.EventArgs) Handles ValidEndTime.Click
        Dim htmlTableString As String = ""
        Dim objOV11 As New OV1_2

        Dim strcheckMealFlag As String = If(chkMealFlag.Checked = True, "1", "0")
        Dim cntStart As Double = 0
        Dim cntEnd As Double = 0
        Dim cntTotal As Double = 0

        If ViewState.Item("DoUpdate") = "Y" Then

            If ucOTStartTime.ucDefaultSelectedHH <> "請選擇" And ucOTStartTime.ucDefaultSelectedMM <> "請選擇" And ucOTStartTime.ucDefaultSelectedHH <> "" And ucOTStartTime.ucDefaultSelectedMM <> "" And ucOTEndTime.ucDefaultSelectedHH <> "" And ucOTEndTime.ucDefaultSelectedMM <> "" And ucOTEndTime.ucDefaultSelectedHH <> "請選擇" And ucOTEndTime.ucDefaultSelectedMM <> "請選擇" Then
                Dim Sex As String = objOV11.QueryColumn("Sex", _eHRMSDB_ITRD + ".[dbo].[Personal] ", " AND EmpID =" + Bsp.Utility.Quote(_EmpID))
                If _Sex <> "" And _Sex = "2" Then
                    '從10點開始
                    If ucOTEndTime.ucDefaultSelectedMM <> "" And Convert.ToInt32(ucOTEndTime.ucDefaultSelectedHH) = 22 Then
                        If Convert.ToInt32(ucOTEndTime.ucDefaultSelectedMM) >= 1 Then
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
                    If Convert.ToInt32(ucOTStartTime.ucDefaultSelectedHH) >= Convert.ToInt32(ucOTEndTime.ucDefaultSelectedHH) And Convert.ToInt32(ucOTStartTime.ucDefaultSelectedMM) > Convert.ToInt32(ucOTEndTime.ucDefaultSelectedMM) Then
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
                    If (_EmpID <> "") Then
                        If cntTotal > 120 Then      '加班超過兩小時需扣用餐時間60分鐘
                            EtxtMealTimeChecked(Nothing, EventArgs.Empty, True)
                        Else
                            EtxtMealTimeChecked(Nothing, EventArgs.Empty, False)
                        End If
                        ''計算時段
                        '********************************
                        Dim returnPeriodCount As String = ""
                        Dim bOTTimeStart As Boolean = Not String.IsNullOrEmpty(ucOTStartTime.ucDefaultSelectedMM) AndAlso Not String.IsNullOrEmpty(ucOTStartTime.ucDefaultSelectedHH) AndAlso ucOTStartTime.ucDefaultSelectedHH <> "請選擇" AndAlso ucOTStartTime.ucDefaultSelectedMM <> "請選擇"
                        Dim bOTTimeEnd As Boolean = Not String.IsNullOrEmpty(ucOTEndTime.ucDefaultSelectedMM) AndAlso Not String.IsNullOrEmpty(ucOTEndTime.ucDefaultSelectedHH) AndAlso ucOTStartTime.ucDefaultSelectedHH <> "請選擇" AndAlso ucOTStartTime.ucDefaultSelectedMM <> "請選擇"
                        If bOTTimeStart And bOTTimeEnd Then
                            Dim ShowStartDateArr, ShowEndDateArr As New ArrayList    '顯示用
                            Dim StartDateArr, EndDateArr As New ArrayList   '計算時段用
                            Dim sOTTimeStart = ucOTStartTime.ucDefaultSelectedHH + ucOTStartTime.ucDefaultSelectedMM
                            Dim sOTTimeEnd = ucOTEndTime.ucDefaultSelectedHH + ucOTEndTime.ucDefaultSelectedMM
                            Dim iOTTimeStart As Integer = 0
                            Dim iOTTimeEnd As Integer = 0
                            If (Integer.TryParse(sOTTimeStart.Replace(":", ""), iOTTimeStart) And (Integer.TryParse(sOTTimeEnd.Replace(":", ""), iOTTimeEnd))) Then
                                Dim smealFlag As String = If(chkMealFlag.Checked = True, "1", "0")
                                Dim sMealTime As String = If(String.IsNullOrEmpty(txtMealTime.Text) = True, "0", txtMealTime.Text.Trim)
                                Dim iMealTime As Integer = 0
                                Integer.TryParse(sMealTime, iMealTime)

                                StartDateArr.Add(ucOTStartDate.DateText)
                                StartDateArr.Add(iOTTimeStart.ToString())
                                StartDateArr.Add(iOTTimeEnd.ToString())

                                EndDateArr.Add("1900/01/01")
                                EndDateArr.Add("0")
                                EndDateArr.Add("0")

                                Dim bPeriodCount As Boolean = objOV11.PeriodCount("OverTimeDeclaration", lblOTEmpID.Text.Trim, cntTotal, 0, StartDateArr, EndDateArr, iMealTime, smealFlag, "", returnPeriodCount)
                                If bPeriodCount And String.IsNullOrEmpty(returnPeriodCount) <> True And returnPeriodCount.Split(";").Length > 0 Then
                                    Dim sReturnPeriodList = returnPeriodCount.Split(";")

                                    For i = 0 To (sReturnPeriodList.Length - 1)
                                        Dim datas = sReturnPeriodList(i)
                                        If i = 0 And datas.Split(",").Length > 0 And datas.Split(",")(0) <> "1900/01/01" Then
                                            If datas.Split(",").Length >= 1 Then ShowStartDateArr.Add(If(datas.Split(",")(0) = "1900/01/01", "-", datas.Split(",")(0)))
                                            If datas.Split(",").Length >= 2 Then ShowStartDateArr.Add(If(datas.Split(",")(1) = "0.0", "-", datas.Split(",")(1)))
                                            If datas.Split(",").Length >= 3 Then ShowStartDateArr.Add(If(datas.Split(",")(2) = "0.0", "-", datas.Split(",")(2)))
                                            If datas.Split(",").Length >= 4 Then ShowStartDateArr.Add(If(datas.Split(",")(3) = "0.0", "-", datas.Split(",")(3)))
                                        End If

                                        If i = 1 And datas.Split(",").Length > 0 And datas.Split(",")(0) <> "1900/01/01" Then
                                            If datas.Split(",").Length >= 1 Then ShowEndDateArr.Add(If(datas.Split(",")(0) = "1900/01/01", "-", datas.Split(",")(0)))
                                            If datas.Split(",").Length >= 2 Then ShowEndDateArr.Add(If(datas.Split(",")(1) = "0.0", "-", datas.Split(",")(1)))
                                            If datas.Split(",").Length >= 3 Then ShowEndDateArr.Add(If(datas.Split(",")(2) = "0.0", "-", datas.Split(",")(2)))
                                            If datas.Split(",").Length >= 4 Then ShowEndDateArr.Add(If(datas.Split(",")(3) = "0.0", "-", datas.Split(",")(3)))
                                        End If
                                    Next
                                    htmlTableString = subTableGenByArr(ShowStartDateArr, ShowEndDateArr)
                                    If htmlTableString <> "" Then
                                        litTable.Text = htmlTableString
                                        pnlCalcTotalTime.Visible = True
                                    End If
                                    txtOTTotalDescript.Text = (If(txtMealTime.Text <> "" And Convert.ToDouble(txtMealTime.Text) > 0 And chkMealFlag.Checked = True, "(已扣除用餐時數" + txtMealTime.Text + "分鐘)", ""))
                                    txtOTTotalDescript.Visible = True

                                    Dim meal As String = If(chkMealFlag.Checked = False, "0", txtMealTime.Text)
                                    meal = If(String.IsNullOrEmpty(meal), "0", txtMealTime.Text)
                                    lblOTTotalTime.Text = Convert.ToDouble((cntTotal - Convert.ToDouble(meal)) / 60).ToString("0.0")
                                    lblOTTotalTime.Visible = True
                                Else
                                    Bsp.Utility.ShowMessage(Me, returnPeriodCount)
                                    Return
                                End If
                            End If
                        End If
                        '********************************
                    End If
                Else    '跨日
                    If _EmpID <> "" Then
                        getCntStartAndCntEnd(cntStart, cntEnd)
                        If cntEnd + cntStart > 120 Then     '加班超過兩小時需扣用餐時間60分鐘
                            EtxtMealTimeChecked(Nothing, EventArgs.Empty, True)
                        Else
                            EtxtMealTimeChecked(Nothing, EventArgs.Empty, False)
                        End If
                        ''計算時段
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
                                Dim smealFlag As String = If(chkMealFlag.Checked = True, "1", "0")
                                Dim sMealTime As String = If(String.IsNullOrEmpty(txtMealTime.Text) = True, "0", txtMealTime.Text.Trim)
                                Dim iMealTime As Integer = 0
                                Integer.TryParse(sMealTime, iMealTime)

                                StartDateArr.Add(ucOTStartDate.DateText)
                                StartDateArr.Add(iOTTimeStart.ToString())
                                StartDateArr.Add("2359")

                                EndDateArr.Add(ucOTEndDate.DateText)
                                EndDateArr.Add("0")
                                EndDateArr.Add(iOTTimeEnd.ToString())

                                Dim bPeriodCount As Boolean = objOV11.PeriodCount("OverTimeDeclaration", lblOTEmpID.Text.Trim, cntStart, cntEnd, StartDateArr, EndDateArr, iMealTime, smealFlag, "", returnPeriodCount)
                                If bPeriodCount And String.IsNullOrEmpty(returnPeriodCount) <> True And returnPeriodCount.Split(";").Length > 0 Then
                                    Dim sReturnPeriodList = returnPeriodCount.Split(";")

                                    For i = 0 To (sReturnPeriodList.Length - 1)
                                        Dim datas = sReturnPeriodList(i)
                                        If i = 0 And datas.Split(",").Length > 0 And datas.Split(",")(0) <> "1900/01/01" Then
                                            If datas.Split(",").Length >= 1 Then ShowStartDateArr.Add(If(datas.Split(",")(0) = "1900/01/01", "-", datas.Split(",")(0)))
                                            If datas.Split(",").Length >= 2 Then ShowStartDateArr.Add(If(datas.Split(",")(1) = "0.0", "-", datas.Split(",")(1)))
                                            If datas.Split(",").Length >= 3 Then ShowStartDateArr.Add(If(datas.Split(",")(2) = "0.0", "-", datas.Split(",")(2)))
                                            If datas.Split(",").Length >= 4 Then ShowStartDateArr.Add(If(datas.Split(",")(3) = "0.0", "-", datas.Split(",")(3)))
                                        End If

                                        If i = 1 And datas.Split(",").Length > 0 And datas.Split(",")(0) <> "1900/01/01" Then
                                            If datas.Split(",").Length >= 1 Then ShowEndDateArr.Add(If(datas.Split(",")(0) = "1900/01/01", "-", datas.Split(",")(0)))
                                            If datas.Split(",").Length >= 2 Then ShowEndDateArr.Add(If(datas.Split(",")(1) = "0.0", "-", datas.Split(",")(1)))
                                            If datas.Split(",").Length >= 3 Then ShowEndDateArr.Add(If(datas.Split(",")(2) = "0.0", "-", datas.Split(",")(2)))
                                            If datas.Split(",").Length >= 4 Then ShowEndDateArr.Add(If(datas.Split(",")(3) = "0.0", "-", datas.Split(",")(3)))
                                        End If
                                    Next
                                    htmlTableString = subTableGenByArr(ShowStartDateArr, ShowEndDateArr)
                                    If htmlTableString <> "" Then
                                        litTable.Text = htmlTableString
                                        pnlCalcTotalTime.Visible = True
                                    End If
                                    txtOTTotalDescript.Text = (If(txtMealTime.Text <> "" And Convert.ToDouble(txtMealTime.Text) > 0 And chkMealFlag.Checked = True, "(已扣除用餐時數" + txtMealTime.Text + "分鐘)", ""))
                                    txtOTTotalDescript.Visible = True

                                    Dim meal As String = If(chkMealFlag.Checked = False, "0", txtMealTime.Text)
                                    meal = If(String.IsNullOrEmpty(meal), "0", txtMealTime.Text)
                                    lblOTTotalTime.Text = Convert.ToDouble((cntEnd + cntStart - Convert.ToDouble(meal)) / 60).ToString("0.0")
                                    lblOTTotalTime.Visible = True
                                Else
                                    Bsp.Utility.ShowMessage(Me, returnPeriodCount)
                                    Return
                                End If
                            End If
                        End If
                        '********************************
                    End If
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
        Dim objOV11 As New OV1_2
        chkMealFlag.Checked = isChecked
        If isChecked Then
            txtMealTime.Enabled = True
            Dim strHo As String = objOV11.QueryColumn("HolidayOrNot", _eHRMSDB_ITRD + ".[dbo].[Calendar]", " AND CompID =" + Bsp.Utility.Quote(UserProfile.SelectCompRoleID.Trim) + " AND  CONVERT(CHAR(10),SysDate, 111) = " + Bsp.Utility.Quote(ucOTStartDate.DateText))
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
    ''' 勾選扣除用餐時數事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub chkMealFlag_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkMealFlag.CheckedChanged
        Dim objOV11 As New OV1_2
        If ViewState.Item("DoUpdate") = "Y" Then
            If Not chkMealFlag.Checked Then
                txtMealTime.Text = "0"
                txtMealTime.Enabled = False
            Else
                Dim cntTotal As Double = 0
                If ucOTStartDate.DateText = ucOTEndDate.DateText Then    '不跨日
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
                    Dim strHo As String = objOV11.QueryColumn("HolidayOrNot", _eHRMSDB_ITRD + ".[dbo].[Calendar] ", " AND CompID = '" + UserProfile.SelectCompRoleID.Trim + "' AND CONVERT(CHAR(10),SysDate, 111) ='" + ucOTStartDate.DateText + "'")
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
            txtMealTime_TextChanged(Nothing, EventArgs.Empty)
        End If
    End Sub

    ''' <summary>
    ''' Usr輸入用餐時數的觸發事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub txtMealTime_TextChanged(sender As Object, e As EventArgs) Handles txtMealTime.TextChanged
        Dim objOV11 As New OV1_2
        Dim cntStart As Double
        Dim cntEnd As Double
        Dim cntTotal As Double
        Dim htmlTableString As String = ""

        If ucOTStartTime.ucDefaultSelectedHH <> "請選擇" AndAlso ucOTStartTime.ucDefaultSelectedMM <> "請選擇" AndAlso ucOTEndTime.ucDefaultSelectedHH <> "請選擇" AndAlso ucOTEndTime.ucDefaultSelectedMM <> "請選擇" AndAlso ucOTStartTime.ucDefaultSelectedHH <> "" AndAlso ucOTStartTime.ucDefaultSelectedMM <> "" AndAlso ucOTEndTime.ucDefaultSelectedHH <> "" AndAlso ucOTEndTime.ucDefaultSelectedMM <> "" AndAlso _EmpID <> "" Then
            If ucOTStartDate.DateText = ucOTEndDate.DateText Then      '不跨日
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

                If txtMealTime.Text <> "" AndAlso Convert.ToInt32(txtMealTime.Text) >= (cntTotal) Then
                    txtMealTime.Focus()
                    Bsp.Utility.ShowMessage(Me, "用餐時數超過加班時數")
                Else
                    '#Region "計算時段"
                    Dim returnPeriodCount As String = ""
                    Dim bOTTimeStart As Boolean = Not String.IsNullOrEmpty(ucOTStartTime.ucDefaultSelectedHH) AndAlso ucOTStartTime.ucDefaultSelectedHH <> "請選擇" AndAlso Not String.IsNullOrEmpty(ucOTStartTime.ucDefaultSelectedMM) AndAlso ucOTStartTime.ucDefaultSelectedMM <> "請選擇"
                    Dim bOTTimeEnd As Boolean = Not String.IsNullOrEmpty(ucOTEndTime.ucDefaultSelectedHH) AndAlso ucOTEndTime.ucDefaultSelectedHH <> "請選擇" AndAlso Not String.IsNullOrEmpty(ucOTEndTime.ucDefaultSelectedMM) AndAlso ucOTEndTime.ucDefaultSelectedMM <> "請選擇"
                    Dim ShowStartDateArr, ShowEndDateArr As New ArrayList    '顯示用
                    Dim StartDateArr, EndDateArr As New ArrayList   '計算時段用

                    If bOTTimeStart AndAlso bOTTimeEnd Then
                        Dim sOTTimeStart As String = ucOTStartTime.ucDefaultSelectedHH + ucOTStartTime.ucDefaultSelectedMM
                        Dim sOTTimeEnd As String = ucOTEndTime.ucDefaultSelectedHH + ucOTEndTime.ucDefaultSelectedMM
                        Dim iOTTimeStart As Integer = 0
                        Dim iOTTimeEnd As Integer = 0
                        If Integer.TryParse(sOTTimeStart, iOTTimeStart) AndAlso Integer.TryParse(sOTTimeEnd, iOTTimeEnd) Then
                            Dim mealFlag As String = If(chkMealFlag.Checked, "1", "0")
                            Dim sMealTime As String = If(String.IsNullOrEmpty(txtMealTime.Text), "0", txtMealTime.Text.Trim())
                            Dim iMealTime As Integer = 0
                            Integer.TryParse(sMealTime, iMealTime)

                            StartDateArr.Add(ucOTStartDate.DateText)
                            StartDateArr.Add(iOTTimeStart.ToString())
                            StartDateArr.Add(iOTTimeEnd.ToString())

                            EndDateArr.Add("1900/01/01")
                            EndDateArr.Add("0")
                            EndDateArr.Add("0")

                            Dim bPeriodCount As Boolean = objOV11.PeriodCount("OverTimeDeclaration", _EmpID, cntTotal, 0, StartDateArr, EndDateArr, iMealTime, mealFlag, _
                             _OTTxnID, returnPeriodCount)

                            If bPeriodCount AndAlso Not String.IsNullOrEmpty(returnPeriodCount) AndAlso returnPeriodCount.Split(";"c).Length > 0 Then
                                Dim sReturnPeriodList = returnPeriodCount.Split(";"c)

                                For i = 0 To sReturnPeriodList.Length - 1
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

                                If txtMealTime.Text <> "" AndAlso Convert.ToDouble(txtMealTime.Text) > 0 And chkMealFlag.Checked = True Then
                                    txtOTTotalDescript.Text = (If(txtMealTime.Text <> "" AndAlso Convert.ToDouble(txtMealTime.Text) > 0 And chkMealFlag.Checked = True, "(已扣除用餐時數" + txtMealTime.Text + "分鐘)", ""))
                                    txtOTTotalDescript.Visible = True
                                Else
                                    txtOTTotalDescript.Visible = False
                                End If

                                Dim meal As String = If((chkMealFlag.Checked = False), "0", txtMealTime.Text)
                                meal = If((String.IsNullOrEmpty(meal)), "0", txtMealTime.Text)
                                lblOTTotalTime.Text = Convert.ToDouble((cntTotal - Convert.ToDouble(meal)) / 60).ToString("0.0")
                                lblOTTotalTime.Visible = True
                            Else
                                Bsp.Utility.ShowMessage(Me, returnPeriodCount)
                                initalData()
                                Return
                            End If
                        End If
                    End If
                End If
            Else
                '跨日
                getCntStartAndCntEnd(cntStart, cntEnd)
                If txtMealTime.Text.Trim = "" Then
                    txtMealTime.Text = "0"
                    'Return
                End If
                If txtMealTime.Text <> "" AndAlso Convert.ToInt32(txtMealTime.Text) >= (cntEnd + cntStart) Then
                    txtMealTime.Focus()
                    Bsp.Utility.ShowMessage(Me, "用餐時數超過加班時數")
                Else
                    '計算時段"
                    Dim ShowStartDateArr, ShowEndDateArr As New ArrayList    '顯示用
                    Dim StartDateArr, EndDateArr As New ArrayList   '計算時段用
                    Dim returnPeriodCount As String = ""
                    Dim bOTTimeStart As Boolean = Not String.IsNullOrEmpty(ucOTStartTime.ucDefaultSelectedHH) AndAlso ucOTStartTime.ucDefaultSelectedHH <> "請選擇" AndAlso Not String.IsNullOrEmpty(ucOTStartTime.ucDefaultSelectedMM) AndAlso ucOTStartTime.ucDefaultSelectedMM <> "請選擇"
                    Dim bOTTimeEnd As Boolean = Not String.IsNullOrEmpty(ucOTEndTime.ucDefaultSelectedHH) AndAlso ucOTEndTime.ucDefaultSelectedHH <> "請選擇" AndAlso Not String.IsNullOrEmpty(ucOTEndTime.ucDefaultSelectedMM) AndAlso ucOTEndTime.ucDefaultSelectedMM <> "請選擇"

                    If bOTTimeStart AndAlso bOTTimeEnd Then
                        Dim sOTTimeStart As String = ucOTStartTime.ucDefaultSelectedHH + ucOTStartTime.ucDefaultSelectedMM
                        Dim sOTTimeEnd As String = ucOTEndTime.ucDefaultSelectedHH + ucOTEndTime.ucDefaultSelectedMM
                        Dim iOTTimeStart As Integer = 0
                        Dim iOTTimeEnd As Integer = 0
                        If Integer.TryParse(sOTTimeStart, iOTTimeStart) AndAlso Integer.TryParse(sOTTimeEnd, iOTTimeEnd) Then
                            Dim mealFlag As String = If(chkMealFlag.Checked, "1", "0")
                            Dim sMealTime As String = If(String.IsNullOrEmpty(txtMealTime.Text), "0", txtMealTime.Text.Trim())
                            Dim iMealTime As Integer = 0
                            Integer.TryParse(sMealTime, iMealTime)

                            StartDateArr.Add(ucOTStartDate.DateText)
                            StartDateArr.Add(iOTTimeStart.ToString())
                            StartDateArr.Add("2359")

                            EndDateArr.Add(ucOTEndDate.DateText)
                            EndDateArr.Add("0")
                            EndDateArr.Add(iOTTimeEnd.ToString())

                            Dim bPeriodCount As Boolean = objOV11.PeriodCount("OverTimeDeclaration", _EmpID, cntStart, cntEnd, StartDateArr, EndDateArr, iMealTime, mealFlag, _
                             _OTTxnID, returnPeriodCount)

                            If bPeriodCount AndAlso Not String.IsNullOrEmpty(returnPeriodCount) AndAlso returnPeriodCount.Split(";"c).Length > 0 Then
                                Dim sReturnPeriodList = returnPeriodCount.Split(";"c)

                                For i = 0 To sReturnPeriodList.Length - 1
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

                                If txtMealTime.Text <> "" AndAlso Convert.ToDouble(txtMealTime.Text) > 0 And chkMealFlag.Checked = True Then
                                    txtOTTotalDescript.Text = (If(txtMealTime.Text <> "" AndAlso Convert.ToDouble(txtMealTime.Text) > 0 And chkMealFlag.Checked = True, "(已扣除用餐時數" + txtMealTime.Text + "分鐘)", ""))
                                    txtOTTotalDescript.Visible = True
                                Else
                                    txtOTTotalDescript.Visible = False
                                End If

                                Dim meal As String = If((chkMealFlag.Checked = False), "0", txtMealTime.Text)
                                meal = If((String.IsNullOrEmpty(meal)), "0", txtMealTime.Text)
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

#End Region

#Region "時段相關"

    ''' <summary>
    ''' 檢查加班時數(含已核准)申報時數是否已超過上限
    ''' </summary>
    ''' <param name="message"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function checkOverTimeIsOver(ByRef message As String) As Boolean
        Dim objOV11 As New OV1_2
        Dim result As Boolean = True
        Dim cntStart As Double = 0
        Dim cntEnd As Double = 0
        Dim cntTotal As Double = 0
        Dim dayNLimit As Double = 0
        Dim dayHLimit As Double = 0
        Dim hr As Double = 0
        Dim hr1 As Double = 0
        Dim iMealTime As Double = 0
        Dim sb As New StringBuilder()
        Double.TryParse(txtMealTime.Text, iMealTime)
        message = ""
        Dim blHo As Boolean = objOV11.CheckHolidayOrNot(ucOTStartDate.DateText)
        dayNLimit = Convert.ToDouble(_dtPara.Rows(0)("DayLimitHourN").ToString())       '平日可申報
        dayHLimit = Convert.ToDouble(_dtPara.Rows(0)("DayLimitHourH").ToString())       '假日可申報

        If ucOTStartDate.DateText = ucOTEndDate.DateText Then    '不跨日
            Dim dt As DataTable = Nothing
            sb.Clear()
            sb.Append(" SELECT ISNULL(SUM(A.OTTotalTime),0) AS TotalTime FROM(")
            sb.Append(" SELECT SUM(OTTotalTime)-SUM(MealTime) AS OTTotalTime FROM OverTimeAdvance WHERE OTCompID=" + Bsp.Utility.Quote(lblCompID.Text.Trim) + " AND OTStatus in('2','3') AND OTEmpID=" + Bsp.Utility.Quote(_EmpID) + " AND OTStartDate=" + Bsp.Utility.Quote(ucOTStartDate.DateText) + " AND OTEndDate=" + Bsp.Utility.Quote(ucOTEndDate.DateText))
            sb.Append(" AND OTTxnID NOT IN ('" + _OTFromAdvanceTxnId + "')")
            sb.Append(" AND OTTxnID NOT IN")
            sb.Append(" (SELECT OTFromAdvanceTxnId FROM OverTimeDeclaration WHERE OTCompID=" + Bsp.Utility.Quote(lblCompID.Text.Trim) + " AND OTEmpID=" + Bsp.Utility.Quote(_EmpID) + " AND OTStatus in ('2','3') AND OTFromAdvanceTxnId<>'')")
            sb.Append(" UNION ALL")
            sb.Append(" SELECT SUM(OTTotalTime)-SUM(MealTime) AS OTTotalTime FROM OverTimeDeclaration WHERE OTCompID=" + Bsp.Utility.Quote(lblCompID.Text.Trim) + " AND OTStatus in('2','3') AND OTEmpID=" + Bsp.Utility.Quote(_EmpID) + " AND OTStartDate=" + Bsp.Utility.Quote(ucOTStartDate.DateText) + " AND OTEndDate=" + Bsp.Utility.Quote(ucOTEndDate.DateText) + ") A ")
            dt = Bsp.DB.ExecuteDataSet(CommandType.Text, sb.ToString(), "AattendantDB").Tables(0)
            If dt.Rows.Count > 0 Then
                hr = Double.Parse(dt.Rows(0)("TotalTime").ToString())
            End If
            getCntTotal(cntTotal)
            cntTotal -= iMealTime
            '檢查連續加班如果開始日期為假日
            If Not blHo Then    '平日檢查
                If cntTotal + hr > (dayNLimit * 60) Then
                    message = "加班時數(含已核准)申報時數已超過上限" + _dtPara.Rows(0)("DayLimitHourN").ToString() + "小時"
                    result = False
                End If
            Else           '假日
                If cntTotal + hr > (dayNLimit * 60) Then
                    message = "加班時數(含已核准)申報時數已超過上限" + _dtPara.Rows(0)("DayLimitHourH").ToString() + "小時"
                    result = False
                End If
            End If

        Else        '跨日
            getCntStartAndCntEnd(cntStart, cntEnd)

            '資料庫的加班總時數
            Dim dtStart As DataTable = Nothing
            Dim dtEnd As DataTable = Nothing
            sb.Clear()
            sb.Append(" SELECT ISNULL(SUM(A.OTTotalTime),0) AS TotalTime FROM(")
            sb.Append(" SELECT SUM(OTTotalTime)-SUM(MealTime) AS OTTotalTime FROM OverTimeAdvance WHERE OTCompID=" + Bsp.Utility.Quote(lblCompID.Text.Trim) + " AND OTStatus in('2','3') AND OTEmpID=" + Bsp.Utility.Quote(_EmpID) + " AND OTStartDate=" + Bsp.Utility.Quote(ucOTStartDate.DateText) + " AND OTEndDate=" + Bsp.Utility.Quote(ucOTStartDate.DateText))
            sb.Append(" AND OTTxnID NOT IN ('" + _OTFromAdvanceTxnId + "')")
            sb.Append(" AND OTTxnID NOT IN")
            sb.Append(" (SELECT OTFromAdvanceTxnId FROM OverTimeDeclaration WHERE OTCompID=" + Bsp.Utility.Quote(lblCompID.Text.Trim) + " AND OTEmpID=" + Bsp.Utility.Quote(_EmpID) + " AND OTStatus in ('2','3') AND OTFromAdvanceTxnId<>'')")
            sb.Append(" UNION ALL")
            sb.Append(" SELECT SUM(OTTotalTime)-SUM(MealTime) AS OTTotalTime FROM OverTimeDeclaration WHERE OTCompID=" + Bsp.Utility.Quote(lblCompID.Text.Trim) + " AND OTStatus in('2','3') AND OTEmpID=" + Bsp.Utility.Quote(_EmpID) + " AND OTStartDate=" + Bsp.Utility.Quote(ucOTStartDate.DateText) + " AND OTEndDate=" + Bsp.Utility.Quote(ucOTStartDate.DateText) + ") A ")
            dtStart = Bsp.DB.ExecuteDataSet(CommandType.Text, sb.ToString(), "AattendantDB").Tables(0)

            sb.Clear()
            sb.Append(" SELECT ISNULL(SUM(A.OTTotalTime),0) AS TotalTime FROM(")
            sb.Append(" SELECT SUM(OTTotalTime)-SUM(MealTime) AS OTTotalTime FROM OverTimeAdvance WHERE OTCompID=" + Bsp.Utility.Quote(lblCompID.Text.Trim) + " AND OTStatus in('2','3') AND OTEmpID=" + Bsp.Utility.Quote(_EmpID) + " AND OTStartDate=" + Bsp.Utility.Quote(ucOTEndDate.DateText) + " AND OTEndDate=" + Bsp.Utility.Quote(ucOTEndDate.DateText))
            sb.Append(" AND OTTxnID NOT IN ('" + _OTFromAdvanceTxnId + "')")
            sb.Append(" AND OTTxnID NOT IN")
            sb.Append(" (SELECT OTFromAdvanceTxnId FROM OverTimeDeclaration WHERE OTCompID=" + Bsp.Utility.Quote(lblCompID.Text.Trim) + " AND OTEmpID=" + Bsp.Utility.Quote(_EmpID) + " AND OTStatus in ('2','3') AND OTFromAdvanceTxnId<>'')")
            sb.Append(" UNION ALL")
            sb.Append(" SELECT SUM(OTTotalTime)-SUM(MealTime) AS OTTotalTime FROM OverTimeDeclaration WHERE OTCompID=" + Bsp.Utility.Quote(lblCompID.Text.Trim) + " AND OTStatus in('2','3') AND OTEmpID=" + Bsp.Utility.Quote(_EmpID) + " AND OTStartDate=" + Bsp.Utility.Quote(ucOTEndDate.DateText) + " AND OTEndDate=" + Bsp.Utility.Quote(ucOTEndDate.DateText) + ") A ")
            dtEnd = Bsp.DB.ExecuteDataSet(CommandType.Text, sb.ToString(), "AattendantDB").Tables(0)

            Dim hrStart As Double = 0    '資料庫的加班總時數
            Dim hrEnd As Double = 0
            Dim hrStart1 As Double = 0
            Dim hrEnd1 As Double = 0
            If dtStart.Rows.Count > 0 Then
                hrStart = Double.Parse(dtStart.Rows(0)("TotalTime").ToString())
            End If
            If dtEnd.Rows.Count > 0 Then
                hrEnd = Double.Parse(dtEnd.Rows(0)("TotalTime").ToString())
            End If

            '國定假日若在平日是可以加班的，不算在連續加班
            Dim blStartHo As Boolean = objOV11.CheckHolidayOrNot(ucOTStartDate.DateText)
            Dim blEndHo As Boolean = objOV11.CheckHolidayOrNot(ucOTEndDate.DateText)
            Dim StartNHo As String = objOV11.QueryColumn("Week", _eHRMSDB_ITRD + ".[dbo].[Calendar]", " AND CompID =" + Bsp.Utility.Quote(UserProfile.SelectCompRoleID.Trim) + " AND  CONVERT(CHAR(10),SysDate, 111) = " + Bsp.Utility.Quote(ucOTStartDate.DateText))
            Dim EndNHo As String = objOV11.QueryColumn("Week", _eHRMSDB_ITRD + ".[dbo].[Calendar]", " AND CompID =" + Bsp.Utility.Quote(UserProfile.SelectCompRoleID.Trim) + " AND  CONVERT(CHAR(10),SysDate, 111) = " + Bsp.Utility.Quote(ucOTEndDate.DateText))
            Dim mealOver As String = objOV11.MealJudge(cntStart, iMealTime)

            Dim StartWeek As Integer = Convert.ToInt32(StartNHo)    '加班開始日期是星期幾
            Dim EndWeek As Integer = Convert.ToInt32(EndNHo)        '加班結束日期是星期幾

            If blStartHo = False AndAlso blEndHo = False Then  '平日跨平日
                If hrStart + (cntStart - Convert.ToDouble(mealOver.Split(",")(1))) > (dayNLimit * 60) Then
                    message = ucOTStartDate.DateText + "(含已核准)申報時數已超過上限"
                    result = False
                End If
                If hrEnd + (cntEnd - Convert.ToDouble(mealOver.Split(",")(3))) > (dayNLimit * 60) Then
                    message = ucOTEndDate.DateText + "(含已核准)申報時數已超過上限"
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
        End If
        Return result
    End Function

    ''' <summary>
    ''' 本月的總時數
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SignData()
        '本月的總時數

        'Dim db As New DbHelper("AattendantDB")
        'Dim sb As CommandHelper = db.CreateCommandHelper()
        'sb.Append("SELECT A.Submit,A.Approval,(A.Submit+A.Approval) AS Total,A.Reject FROM (")
        ''sb.Append(" SELECT SUM(CASE OTStatus WHEN '2' THEN (CASE datas.Split(","c) WHEN 1 THEN OTTotalTime-CAST(MealTime AS FLOAT)/CAST(60 AS FLOAT) ELSE OTTotalTime END) ELSE '0.0' END) AS Submit,");
        ''sb.Append(" SUM(CASE OTStatus WHEN '3' THEN (CASE datas.Split(","c) WHEN 1 THEN OTTotalTime-CAST(MealTime AS FLOAT)/CAST(60 AS FLOAT) ELSE OTTotalTime END) ELSE '0.0' END)  AS Approval,");
        ''sb.Append(" SUM(CASE OTStatus WHEN '4' THEN (CASE datas.Split(","c) WHEN 1 THEN OTTotalTime-CAST(MealTime AS FLOAT)/CAST(60 AS FLOAT) ELSE OTTotalTime END) ELSE '0.0' END) AS Reject");
        'sb.Append(" SELECT SUM(CASE OTStatus WHEN '2' THEN (CASE MealFlag WHEN 1 THEN Convert(Decimal(10,2),(CAST(OTTotalTime AS FLOAT)-CAST(MealTime AS FLOAT))/CAST(60 AS FLOAT)) ELSE Convert(Decimal(10,2),(CAST(OTTotalTime AS FLOAT)/CAST(60 AS FLOAT))) END) ELSE '0.0' END) AS Submit, ")
        'sb.Append(" SUM(CASE OTStatus WHEN '3' THEN (CASE MealFlag WHEN 1 THEN Convert(Decimal(10,2),(CAST(OTTotalTime AS FLOAT)-CAST(MealTime AS FLOAT))/CAST(60 AS FLOAT)) ELSE Convert(Decimal(10,2),(CAST(OTTotalTime AS FLOAT)/CAST(60 AS FLOAT))) END) ELSE '0.0' END)  AS Approval, ")
        'sb.Append(" SUM(CASE OTStatus WHEN '4' THEN (CASE MealFlag WHEN 1 THEN Convert(Decimal(10,2),(CAST(OTTotalTime AS FLOAT)-CAST(MealTime AS FLOAT))/CAST(60 AS FLOAT)) ELSE Convert(Decimal(10,2),(CAST(OTTotalTime AS FLOAT)/CAST(60 AS FLOAT))) END) ELSE '0.0' END) AS Reject ")
        ''sb.Append(" SELECT SUM(CASE OTStatus WHEN '2' THEN (CASE datas.Split(","c) WHEN 1 THEN (CAST(OTTotalTime AS FLOAT)-CAST(MealTime AS FLOAT))/CAST(60 AS FLOAT) ELSE (CAST(OTTotalTime AS FLOAT)/CAST(60 AS FLOAT)) END) ELSE '0.0' END) AS Submit,");
        ''sb.Append(" SUM(CASE OTStatus WHEN '3' THEN (CASE datas.Split(","c) WHEN 1 THEN (CAST(OTTotalTime AS FLOAT)-CAST(MealTime AS FLOAT))/CAST(60 AS FLOAT) ELSE (CAST(OTTotalTime AS FLOAT)/CAST(60 AS FLOAT)) END) ELSE '0.0' END)  AS Approval,");
        ''sb.Append(" SUM(CASE OTStatus WHEN '4' THEN (CASE datas.Split(","c) WHEN 1 THEN (CAST(OTTotalTime AS FLOAT)-CAST(MealTime AS FLOAT))/CAST(60 AS FLOAT) ELSE (CAST(OTTotalTime AS FLOAT)/CAST(60 AS FLOAT)) END) ELSE '0.0' END) AS Reject");
        'sb.Append(" FROM OverTimeDeclaration ")
        'sb.Append(" WHERE OTCompID = '" + lblCompID.Text.Trim + "' AND OTEmpID = '" + _EmpID + "'")
        'sb.Append(" AND MONTH(OTStartDate) = MONTH('" + ucOTStartDate.DateText + "') ")
        'sb.Append(" AND YEAR(OTStartDate) = YEAR('" + ucOTEndDate.DateText + "') ")
        'sb.Append(" AND OTEmpID = '" + _EmpID + "') A")
        'Dim dt As DataTable = db.ExecuteDataSet(sb.BuildCommand()).Tables(0)

        'If dt.Rows.Count > 0 Then
        '    If ucOTStartDate.DateText.Substring(5, 2).Substring(0, 1).ToString() = "0" Then
        '        lblTotalOTCalc.Text = ucOTStartDate.DateText.Substring(5, 2).Substring(1, 1).ToString()
        '    Else
        '        lblTotalOTCalc.Text = ucOTStartDate.DateText.Substring(5, 2).Substring(1, 1).ToString()
        '    End If
        '    If (dt.Rows(0)("Submit").ToString() = "0.00") Or (dt.Rows(0)("Submit").ToString() = "") Then
        '        lblSubmit.Text = "0.0"
        '    Else
        '        lblSubmit.Text = Math.Round(Convert.ToDouble(dt.Rows(0)("Submit")), 1).ToString("0.0")
        '    End If

        '    If (dt.Rows(0)("Approval").ToString() = "0.00") Or (dt.Rows(0)("Approval").ToString() = "") Then
        '        lblApproval.Text = "0.0"
        '    Else
        '        lblApproval.Text = Math.Round(Convert.ToDouble(dt.Rows(0)("Approval")), 1).ToString("0.0")
        '    End If

        '    If (dt.Rows(0)("Reject").ToString() = "0.00") Or (dt.Rows(0)("Reject").ToString() = "") Then
        '        lblReject.Text = "0.0"
        '    Else
        '        lblReject.Text = Math.Round(Convert.ToDouble(dt.Rows(0)("Reject")), 1).ToString("0.0")
        '    End If
        'End If


        'OVBusinessCommon的算法
        Dim objOVBC As New OVBusinessCommon
        Dim OverTimeSumArr As ArrayList = New ArrayList
        OverTimeSumArr.AddRange(objOVBC.getTotalHR(lblCompID.Text.Trim, _EmpID, ucOTStartDate.DateText))

        If OverTimeSumArr.Count <> 0 Then
            lblTotalOTCalc.Text = If(OverTimeSumArr(0) IsNot Nothing AndAlso OverTimeSumArr(0).ToString() <> "", OverTimeSumArr(0).ToString(), Month(ucOTStartDate.DateText).ToString())
            lblSubmit.Text = If(OverTimeSumArr(1) IsNot Nothing AndAlso OverTimeSumArr(1).ToString() <> "", OverTimeSumArr(1).ToString(), "0.0")
            lblApproval.Text = If(OverTimeSumArr(2) IsNot Nothing AndAlso OverTimeSumArr(2).ToString() <> "", OverTimeSumArr(2).ToString(), "0.0")
            lblReject.Text = If(OverTimeSumArr(3) IsNot Nothing AndAlso OverTimeSumArr(3).ToString() <> "", OverTimeSumArr(3).ToString(), "0.0")
        End If
    End Sub

    ''' <summary>
    ''' 不跨日時數(分鐘)
    ''' </summary>
    ''' <param name="cntTotal"></param>
    ''' <remarks></remarks>
    Private Sub getCntTotal(ByRef cntTotal As Double)
        cntTotal = 0
        If ucOTStartTime.ucDefaultSelectedHH <> "請選擇" AndAlso ucOTStartTime.ucDefaultSelectedMM <> "請選擇" AndAlso ucOTEndTime.ucDefaultSelectedHH <> "請選擇" AndAlso ucOTEndTime.ucDefaultSelectedMM <> "請選擇" AndAlso ucOTStartTime.ucDefaultSelectedHH <> "" AndAlso ucOTStartTime.ucDefaultSelectedMM <> "" AndAlso ucOTEndTime.ucDefaultSelectedHH <> "" AndAlso ucOTEndTime.ucDefaultSelectedMM <> "" Then
            cntTotal = (Convert.ToDouble(ucOTEndTime.ucDefaultSelectedHH) * 60 + Convert.ToDouble(ucOTEndTime.ucDefaultSelectedMM)) - (Convert.ToDouble(ucOTStartTime.ucDefaultSelectedHH) * 60 + Convert.ToDouble(ucOTStartTime.ucDefaultSelectedMM))
        End If
    End Sub

    ''' <summary>
    ''' 時數計算(轉分鐘)
    ''' </summary>
    ''' <param name="cntStart"></param>
    ''' <param name="cntEnd"></param>
    ''' <remarks></remarks>
    Private Sub getCntStartAndCntEnd(ByRef cntStart As Double, ByRef cntEnd As Double)
        cntStart = 0
        cntEnd = 0
        Try
            If ucOTStartTime.ucDefaultSelectedHH <> "請選擇" And ucOTStartTime.ucDefaultSelectedMM <> "請選擇" And ucOTEndTime.ucDefaultSelectedHH <> "請選擇" And ucOTEndTime.ucDefaultSelectedMM <> "請選擇" And ucOTStartTime.ucDefaultSelectedHH <> "" And ucOTStartTime.ucDefaultSelectedMM <> "" And ucOTEndTime.ucDefaultSelectedHH <> "" And ucOTEndTime.ucDefaultSelectedMM <> "" Then
                cntStart = (23 - Convert.ToDouble(ucOTStartTime.ucDefaultSelectedHH)) * 60 + (60 - Convert.ToDouble(ucOTStartTime.ucDefaultSelectedMM))
                cntEnd = ((Convert.ToDouble(ucOTEndTime.ucDefaultSelectedHH) * 60) + (Convert.ToDouble(ucOTEndTime.ucDefaultSelectedMM)))
            End If
        Catch ex As Exception
            Debug.Print("getCntStartAndCntEnd()=>" + ex.Message)
        End Try
    End Sub

#End Region

#Region "Private Method"
    ''' <summary>
    ''' 檢查是否直接送簽，修改後必須小於等於修改前的時段
    ''' </summary>
    ''' <param name="strOTStartDate">加班起日</param>
    ''' <param name="strOTEndDate">加班迄日</param>
    ''' <param name="StrComp">加班人公司代碼</param>
    ''' <param name="strEmpID">加班人員編</param>
    ''' <param name="strOTStartTime">加班開始時間</param>
    ''' <param name="strOTEndTime">加班結束時間</param>
    ''' <param name="strOTFromAdvanceTxnId"></param>
    ''' <returns>boolean</returns>
    ''' <remarks></remarks>
    Protected Function DirectSubmit(strOTStartDate As String, strOTEndDate As String, StrComp As String, strEmpID As String, strOTStartTime As String, strOTEndTime As String, strOTFromAdvanceTxnId As String, strOTTotalTime As String) As String
        'Dim db As New DbHelper("AattendantDB")
        'Dim sb As CommandHelper = db.CreateCommandHelper()
        Dim sb As New StringBuilder
        Dim dt As DataTable = Nothing
        If strOTStartDate = strOTEndDate Then
            sb.Append("SELECT SUM(OT.OTTotalTime)-SUM(OT.MealTime) AS TotalTime,CASE WHEN (OT.OTStartTime='" + strOTStartTime + "' AND OT.OTEndTime='" + strOTEndTime + "') THEN 'Y'")
            sb.Append(" WHEN (CONVERT(int,OT.OTStartTime)<=" + (Convert.ToInt32(strOTStartTime)).ToString() + " ) AND")
            sb.Append(" (CONVERT(int,OT.OTEndTime)>=" + (Convert.ToInt32(strOTEndTime)).ToString() + " )THEN 'Y'")
            sb.Append(" ELSE 'N'")
            sb.Append(" END AS CheckTime FROM OverTimeAdvance OT")
            sb.Append(" LEFT JOIN OverTimeDeclaration OD ON OD.OTFromAdvanceTxnId=OT.OTTxnID")

            sb.Append(" WHERE OT.OTStatus='3' AND OT.OTCompID='" + StrComp + "' AND OT.OTEmpID='" + strEmpID + "' AND OT.OTStartDate='" + strOTStartDate + "' AND OT.OTEndDate='" + strOTEndDate + "' AND OTFromAdvanceTxnId='" + strOTFromAdvanceTxnId + "' ")     'AND OT.OTStartTime='" + _OTStartTime + "' AND OT.OTEndTime='" + _OTEndTime+ "'
            sb.Append(" GROUP BY OT.OTStartTime,OT.OTEndTime")
            dt = Bsp.DB.ExecuteDataSet(CommandType.Text, sb.ToString, "AattendantDB").Tables(0)
        Else
            sb.Append("SELECT TotalTime,CASE WHEN (LEFT(A.OTTime,4)='" + strOTStartTime + "' AND RIGHT(A.OTTime,4)='" + strOTEndTime + "') THEN 'Y'")
            sb.Append(" WHEN (CONVERT(int,LEFT(A.OTTime,4))<=" + (Convert.ToInt32(strOTStartTime)).ToString() + ") AND")
            sb.Append(" (CONVERT(int,RIGHT(A.OTTime,4))>=" + (Convert.ToInt32(strOTEndTime)).ToString() + ")")
            sb.Append(" THEN 'Y' ELSE 'N'")
            sb.Append(" END AS CheckTime")
            sb.Append(" FROM")
            sb.Append(" (SELECT (OT.OTStartDate+'~'+isnull(OT2.OTEndDate,OT.OTEndDate)) AS OTDate,")
            sb.Append(" (LEFT(OT.OTStartTime,2)+RIGHT(OT.OTStartTime,2)+'~'+ isnull(LEFT(OT2.OTEndTime,2)+RIGHT(OT2.OTEndTime,2),LEFT(OT.OTEndTime,2)+RIGHT(OT.OTEndTime,2))) AS OTTime ,")
            sb.Append(" Convert(Decimal(10,1),(CAST(OT.OTTotalTime AS FLOAT)-CAST(OT.MealTime AS FLOAT))+(CAST(ISNULL(OT2.OTTotalTime,0) AS FLOAT)-CAST(ISNULL(OT2.MealTime,0) AS FLOAT))) AS TotalTime")
            sb.Append(" FROM OverTimeAdvance OT ")
            sb.Append(" LEFT JOIN OverTimeDeclaration OD ON OD.OTFromAdvanceTxnId=OT.OTTxnID AND OD.OTSeqNo=2")
            sb.Append(" LEFT JOIN OverTimeAdvance OT2 on OT2.OTTxnID=OT.OTTxnID AND OT2.OTSeqNo=2")
            sb.Append(" WHERE OT.OTSeqNo=1 AND OT.OTCompID='" + StrComp + "' AND OT.OTEmpID='" + strEmpID + "' AND OT.OTStatus='3' AND OD.OTFromAdvanceTxnId='" + strOTFromAdvanceTxnId + "') A")
            sb.Append(" WHERE LEFT(A.OTDate,10) <> RIGHT(A.OTDate,10) AND LEFT(A.OTDate,10)='" + strOTStartDate + "' AND RIGHT(A.OTDate,10)='" + strOTEndDate + "'")
            dt = Bsp.DB.ExecuteDataSet(CommandType.Text, sb.ToString, "AattendantDB").Tables(0)
        End If

        If dt.Rows.Count > 0 Then
            If dt.Rows(0)("CheckTime").ToString() = "Y" Then
                If (Convert.ToDouble(dt.Rows(0)("TotalTime").ToString()) / 60) >= Convert.ToDouble(strOTTotalTime) Then
                    dt.Clear()
                    dt.Dispose()
                    Return "Y"
                Else
                    dt.Clear()
                    dt.Dispose()
                    Return "N"
                End If
            Else
                dt.Clear()
                dt.Dispose()
                Return "N"
            End If
        Else
            dt.Clear()
            dt.Dispose()
            Return "N"
        End If
    End Function

    ''' <summary>
    ''' 送簽後的動作
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnAfterSubmit_Click(sender As Object, e As System.EventArgs) Handles btnAfterSubmit.Click
        If hidSubmitStatus.Value = "Y" Then
            LoadAttch()  '如果送簽成功才要更新附件
            GoBack()        '送簽成功後就返回
        End If
    End Sub
#End Region

#Region "清除送簽快取"

    '清除送簽快取
    Protected Sub ClearSubmitCache_Click(sender As Object, e As System.EventArgs) Handles ClearSubmitCache.Click
        ClearCacheData()
    End Sub

    ''' <summary>
    ''' 清除送簽快取
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ClearCacheData() As Boolean
        Dim sb As New StringBuilder
        sb.Append("DELETE * FROM CacheData WHERE 1=1 AND UserID = " + Bsp.Utility.Quote(UserProfile.ActUserID) + " AND Platform = 'AP' AND SystemID = 'OT' AND TxnName = 'OV1102'")
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

End Class
