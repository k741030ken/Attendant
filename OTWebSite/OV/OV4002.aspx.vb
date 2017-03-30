Imports System.Data
Imports System.Globalization
Imports System.IO
Imports SinoPac.WebExpress.Common
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data


Partial Class OV_1_OV4002
    Inherits PageBase
#Region "全域變數"
    Private _overtimeDBName As String = "AattendantDB"
    Private _eHRMSDB_ITRD As String = Bsp.Utility.getAppSetting("eHRMSDB")

    Public Property _AttachID As String
        Get
            If ViewState.Item("_AttachID") Is Nothing Then ViewState.Item("_AttachID") = String.Empty

            Return ViewState.Item("_AttachID").ToString()
        End Get
        Set(value As String)
            ViewState.Item("_AttachID") = value
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
    Private Property eHRMSDB_ITRD As String
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
    Dim OV_1 As OV_1
    Dim AttachmentID As String

#End Region

#Region "功能選單"

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnActionX"     '返回
                GoBack()
            Case "btnUpdate"
                Update()
        End Select
    End Sub

#Region "功能群組"

    Private Sub Update()
        Dim OTStartDate As String = txtOvertimeDateB.DateText
        Dim OTEndDate As String = txtOvertimeDateE.DateText
        Dim OTStartTime As String = OTStartTimeH.SelectedValue + OTStartTimeM.SelectedValue
        Dim OTEndTime As String = OTEndTimeH.SelectedValue + OTEndTimeM.SelectedValue
        Dim MealFlag As String
        If cbMealFlag.Checked = True Then
            MealFlag = "1"
        Else
            MealFlag = "0"
        End If
        Dim MealTime As String
        If "".Equals(labMealTime.Text.Trim) Or Not IsNumeric(labMealTime.Text.Trim) Then
            MealTime = 0
        Else
            MealTime = labMealTime.Text
        End If
        Dim SalaryOrAdjust As String = ddlSalaryOrAdjust.SelectedValue
        Dim AdjustInvalidDate As String = labAdjustInvalidDate.Text
        Dim OTTypeID As String = ddlOTTypeID.SelectedValue
        Dim OTReasonMemo As String = labOTReasonID.Text

        '如果附件的key有資料
        Dim objOV42 = New OV4_2
        If labOTAttachment.Value.ToString.Trim.Count > 0 Then
            labOTAttachment.Value = labOTAttachment.Value.ToString
        Else
            labOTAttachment.Value = objOV42.QueryAttach(_AttachID, ViewState("OTCompID"), ViewState("OTEmpID"))
        End If


        Dim Attachment = labOTAttachment.Value

        '看是否同一天
        '要看之前是否為同一天
        '先看是否是 是先申請
        '是:只改我們db() PS: 注意先前是否有跨日 有的話需要移除其中一個
        '否:如下

        '再看有無拋轉 抓兩個單 需要都沒拋轉  
        '沒拋()
        '只改我們db()
        '有拋()
        '看是否計薪() 抓兩個單 需要都沒記薪
        '沒計薪(改overtime)
        '有計薪改我們db()

        Dim errorMsg As String = chackEndToUPdata()
        If errorMsg.ToString.Count = 0 Then
            '有跨日請拆單
            If OTStartDate.Equals(OTEndDate) Then '改成無跨日
                Dim OV_1 As OV_1 = setPropertyForUpData(OTStartDate, OTEndDate, OTStartTime, OTEndTime, MealFlag, MealTime, SalaryOrAdjust, AdjustInvalidDate, OTTypeID, OTReasonMemo, Attachment)
                If ViewState("Type").Equals("bef") Then
                    ' 無跨->無跨 直接upDate
                    If labOverTimeDate.Value.ToString.Split("~")(0).Equals(labOverTimeDate.Value.ToString.Split("~")(1)) Then
                        Dim Flag As Boolean = OV_1.UpDateFor4002Single(OV_1)
                        showMagAndGoBackFromflag(Flag)
                    Else ' 有跨->無跨 upDate seqNO 1 remove seqNO 2
                        Dim Flag As Boolean = OV_1.DeleteFor4002(OV_1, "2")
                        If Flag Then
                            Flag = OV_1.UpDateFor4002Single(OV_1)
                        End If
                        showMagAndGoBackFromflag(Flag)
                    End If
                ElseIf (ViewState("Type").Equals("after")) Then

                    '再看有無拋轉 需要都沒拋轉  
                    '沒拋()
                    '只改我們db()
                    '有拋()
                    '看是否計薪() 抓兩個單 需要都沒記薪
                    '沒計薪(改overtime)
                    '有計薪改我們db()
                    ' if 是拋轉 next check
                    'change db

                    Dim befSData As String = labOverTimeDate.Value.ToString().Split("~")(0)


                    Dim befSTime As String = labOTStartDate.Value.ToString().Split(":")(0) + labOTStartDate.Value.ToString().Split(":")(1)

                    'OTEndTimeH.SelectedValue = labOTEndDate.Value.ToString().Split(":")(0) '要做什麼?
                    'OTEndTimeM.SelectedValue = labOTEndDate.Value.ToString().Split(":")(1)

                    '20170308 用來紀錄是否要跟改ToOverTimeFlag 在 OV_1.UpDateFor4002Single(OV_1) 及OV_1.InsertFor4002 用到

                    ' OV_1.isSalaryPaid 無計薪
                    OV_1.isSalaryPaid = Not OV_1.checkSalaryPaid(OV_1.CompID, OV_1.OTEmpID, befSData, befSTime)

                    If OV_1.isToOverTime() And OV_1.isSalaryPaid Then
                        Dim Flag As Boolean = False
                        Dim oldDataTable As DataTable = OV_1.QuiryOldDataTableArrForOTTxnID(OV_1.OTTxnID, OV_1.Type)
                        '無跨->無跨 直接upDate
                        If labOverTimeDate.Value.ToString.Split("~")(0).Equals(labOverTimeDate.Value.ToString.Split("~")(1)) Then
                            Flag = OV_1.UpDateFor4002Single(OV_1)
                        Else '有跨->無跨 upDate seqNO 1 remove seqNO 2
                            Flag = OV_1.DeleteFor4002(OV_1, "2")
                            If Flag Then
                                Flag = OV_1.UpDateFor4002Single(OV_1)
                            End If
                        End If
                        If Flag Then '改overtime 先刪除舊的 在裝新的 如果轉補修就不裝新的
                            Flag = False
                            If oldDataTable.Rows.Count > 0 Then
                                Dim data_Period As DataTable = getTable_Period() '存放時段的Table
                                Flag = deleteOverTimeTable(oldDataTable, data_Period)
                                '如果轉補修就不裝新的
                                If Flag And ddlSalaryOrAdjust.SelectedValue.Equals("1") Then
                                    doThrow(OV_1.CompID, OV_1.OTEmpID, OV_1.OTTxnID)
                                End If
                            End If
                        End If
                        showMagAndGoBackFromflag(Flag)
                    Else '只改我們db()
                        Dim Flag As Boolean = False
                        If labOverTimeDate.Value.ToString.Split("~")(0).Equals(labOverTimeDate.Value.ToString.Split("~")(1)) Then
                            Flag = OV_1.UpDateFor4002Single(OV_1)
                        Else '有跨->無跨 upDate seqNO 1 remove seqNO 2
                            Flag = OV_1.DeleteFor4002(OV_1, "2")
                            If Flag Then
                                Flag = OV_1.UpDateFor4002Single(OV_1)
                            End If
                        End If
                        showMagAndGoBackFromflag(Flag)
                    End If
                End If
            Else '改成有跨日
                Dim OV_1 As OV_1 = setPropertyForUpData(OTStartDate, OTEndDate, OTStartTime, OTEndTime, MealFlag, MealTime, SalaryOrAdjust, AdjustInvalidDate, OTTypeID, OTReasonMemo, Attachment)
                If ViewState("Type").Equals("bef") Then
                    Dim DataTable As DataTable = OV_1.QuiryOldDataTableArrForOTTxnID(OV_1.OTTxnID, OV_1.Type)
                    '不管有無跨日我都 先查 舊資料 抓取他到Server 然刪除舊資料 新增兩筆
                    If DataTable.Rows.Count > 0 Then
                        Dim oldOVA As OV4_OVA = setProperTyForOV4_OVA(DataTable)
                        Dim Flag As Boolean = OV_1.DeleteFor4002(OV_1, "")
                        If Flag Then
                            Flag = OV_1.InsertFor4002OVA(OV_1, oldOVA)
                        End If
                        showMagAndGoBackFromflag(Flag)
                    End If
                ElseIf (ViewState("Type").Equals("after")) Then
                    Dim befSData As String = labOverTimeDate.Value.ToString().Split("~")(0)


                    Dim befSTime As String = labOTStartDate.Value.ToString().Split(":")(0) + labOTStartDate.Value.ToString().Split(":")(1)

                    'OTEndTimeH.SelectedValue = labOTEndDate.Value.ToString().Split(":")(0)
                    'OTEndTimeM.SelectedValue = labOTEndDate.Value.ToString().Split(":")(1)

                    '20170308 用來紀錄是否要跟改ToOverTimeFlag 在 OV_1.UpDateFor4002Single(OV_1) 及OV_1.InsertFor4002 用到
                    OV_1.isSalaryPaid = Not OV_1.checkSalaryPaid(OV_1.CompID, OV_1.OTEmpID, befSData, befSTime)

                    If OV_1.isToOverTime() And OV_1.isSalaryPaid Then
                        Dim oldDataTable As DataTable = OV_1.QuiryOldDataTableArrForOTTxnID(OV_1.OTTxnID, OV_1.Type)
                        Dim data_Period As DataTable = getTable_Period() '存放時段的Table
                        '不管有無跨日我都 先查 舊資料 抓取他到Server 然刪除舊資料 新增兩筆
                        If oldDataTable.Rows.Count > 0 Then
                            Dim oldOVD As OV4_OVD = setProperTyForOV4_OVD(oldDataTable)
                            Dim Flag As Boolean = OV_1.DeleteFor4002(OV_1, "")
                            If Flag Then
                                Flag = OV_1.InsertFor4002OVD(OV_1, oldOVD)
                            End If
                            If Flag Then '改overtime 先刪除舊的 在裝新的
                                Flag = False
                                Flag = deleteOverTimeTable(oldDataTable, data_Period)

                                If Flag And ddlSalaryOrAdjust.SelectedValue.Equals("1") Then
                                    doThrow(OV_1.CompID, OV_1.OTEmpID, OV_1.OTTxnID)
                                End If
                            End If
                            showMagAndGoBackFromflag(Flag)
                        End If
                    Else
                        Dim oldDataTable As DataTable = OV_1.QuiryOldDataTableArrForOTTxnID(OV_1.OTTxnID, OV_1.Type)
                        '不管有無跨日我都 先查 舊資料 抓取他到Server 然刪除舊資料 新增兩筆
                        If oldDataTable.Rows.Count > 0 Then
                            Dim oldOVD As OV4_OVD = setProperTyForOV4_OVD(oldDataTable)
                            Dim Flag As Boolean = OV_1.DeleteFor4002(OV_1, "")
                            If Flag Then
                                Flag = OV_1.InsertFor4002OVD(OV_1, oldOVD)
                            End If
                            showMagAndGoBackFromflag(Flag)
                        End If
                    End If
                End If
            End If
        Else
            Bsp.Utility.ShowMessage(Me, errorMsg)
        End If
    End Sub

    Private Sub GoBack()
        Dim ti As TransferInfo = Me.StateTransfer
        Me.TransferFramePage(ti.CallerUrl, Nothing, ti.Args)
    End Sub
#End Region

#End Region

#Region "載入頁面"

    ''' <summary>
    ''' 返回事件進入點
    ''' </summary>
    ''' <param name="ti"></param>
    ''' <remarks></remarks>
    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)
            OV_1 = New OV_1()
            Dim dt As DataTable
            Dim OTCompID As String = ht("OTCompID").ToString()
            Dim OTEmpID As String = ht("OTEmpID").ToString()
            Dim OTStartDate As String = ht("OTStartDate").ToString()
            Dim OTEndDate As String = ht("OTEndDate").ToString()
            Dim OTSeq As String = ht("OTSeq").ToString()
            OV_1.Type = ht("hiddenType").ToString()
            Dim OTTxnID As String = ht("OTTxnID").ToString()
            Dim dt2 As DataTable = OV_1.getOV4001SumDataTable(OTCompID, OTEmpID, OTStartDate)
            ViewState("Type") = ht("hiddenType").ToString()
            ViewState("OTSeq") = OTSeq
            ViewState("OTCompID") = OTCompID
            ViewState("OTEmpID") = OTEmpID
            ViewState("OTTxnID") = OTTxnID
            ViewState("Type") = ht("hiddenType").ToString()

            dt = OV_1.getOV4001DataTable(OTCompID, OTEmpID, OTStartDate, OTEndDate, OTSeq, OTTxnID)
            OTStartDate = dt.Rows(0).Item("labOverTimeDate").ToString().Split("~")(0)
            LoanData(dt, dt2)
        End If
    End Sub

    ''' <summary>
    ''' 載入資料
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <param name="dt2"></param>
    ''' <remarks></remarks>
    Public Sub LoanData(ByVal dt As DataTable, ByVal dt2 As DataTable)
        Dim objOV_1 As OV_1 = New OV_1
        '開始日期 結束日期 開始時間 結束時間
        Dim mStartDate As String
        Dim mEndDate As String
        Dim mStartTime As String
        Dim mEndTime As String
        Dim mRankID As String

        Dim hidMealTimeTemp = 0
        '附件Attach
        Dim strAttachID As String = ""
        Dim strAttachAdminURL As String = ""
        Dim strAttachAdminBaseURL As String = ConfigurationManager.AppSettings("AattendantWebPath") + "HandlerForOverTime/SSORecvModeForOverTime.aspx?UserID=" + UserProfile.UserID.Trim + "&SystemID=OT&TxnID=OV4202&ReturnUrl=%2FUtil%2FAttachAdmin.aspx?AttachDB={0}%26AttachID={1}%26AttachFileMaxQty={2}%26AttachFileMaxKB={3}%26AttachFileTotKB={4}%26AttachFileExtList={5}"
        Dim strAttachDownloadURL As String
        Dim strAttachDownloadBaseURL As String = Util._AttachDownloadUrl + "?AttachDB={0}&AttachID={1}"


        For i = 1 To 24 Step +1
            If i <= 10 Then
                OTStartTimeH.Items.Insert(i - 1, New ListItem("0" + (i - 1).ToString, "0" + (i - 1).ToString))
            Else
                OTStartTimeH.Items.Insert(i - 1, New ListItem((i - 1).ToString, (i - 1).ToString))
            End If
        Next i
        For i = 1 To 60 Step +1
            If i <= 10 Then
                OTStartTimeM.Items.Insert(i - 1, New ListItem("0" + (i - 1).ToString, "0" + (i - 1).ToString))
            Else
                OTStartTimeM.Items.Insert(i - 1, New ListItem((i - 1).ToString, (i - 1).ToString))
            End If
        Next i

        For i = 1 To 24 Step +1
            If i <= 10 Then
                OTEndTimeH.Items.Insert(i - 1, New ListItem("0" + (i - 1).ToString, "0" + (i - 1).ToString))
            Else
                OTEndTimeH.Items.Insert(i - 1, New ListItem((i - 1).ToString, (i - 1).ToString))
            End If
        Next i
        For i = 1 To 60 Step +1
            If i <= 10 Then
                OTEndTimeM.Items.Insert(i - 1, New ListItem("0" + (i - 1).ToString, "0" + (i - 1).ToString))
            Else
                OTEndTimeM.Items.Insert(i - 1, New ListItem((i - 1).ToString, (i - 1).ToString))
            End If
        Next i
        Bsp.Utility.FillDDL(ddlOTTypeID, "AattendantDB", "AT_CodeMap", "Code", "CodeCName", Bsp.Utility.DisplayType.OnlyName, , "and TabName='OverTime' and FldName='OverTimeType' and NotShowFlag='0'", "")



        If dt.Rows.Count > 0 Then
            Dim item As DataRow = dt.Rows(0)
            If dt.Rows.Count > 1 Then
                Dim item2 As DataRow = dt.Rows(1)
                mStartDate = ((item("labOverTimeDate").ToString.Split("~"))(0))
                mEndDate = (item2("labOverTimeDate").ToString.Split("~"))(1)
                mStartTime = (item("labOTStartDate").ToString)
                mEndTime = (item2("labOTEndDate").ToString)
                labMealTime.Text = (Convert.ToInt16(item("labMealTime")) + Convert.ToInt16(item2("labMealTime"))).ToString
            Else
                mStartDate = ((item("labOverTimeDate").ToString.Split("~"))(0))
                mEndDate = (item("labOverTimeDate").ToString.Split("~"))(1)
                mStartTime = (item("labOTStartDate").ToString)
                mEndTime = (item("labOTEndDate").ToString)
                labMealTime.Text = item("labMealTime").ToString
            End If
            labOTStartDate.Value = mStartTime.Substring(0, 2) + ":" + mStartTime.Substring(2, 2)
            labOTEndDate.Value = mEndTime.Substring(0, 2) + ":" + mEndTime.Substring(2, 2)
            labOverTimeDate.Value = mStartDate + "~" + mEndDate

            labCompID.Text = item("labCompID").ToString
            labDeptID.Text = item("labDeptID").ToString
            labOrganID.Text = item("labOrganID").ToString
            labOTEmpName.Text = item("labOTEmpName").ToString

            labSalaryOrAdjust.Value = item("labSalaryOrAdjust").ToString
            labAdjustInvalidDate.Text = FormatDateTime(item("labAdjustInvalidDate").ToString, DateFormat.ShortDate).ToString
            hidAdjustInvalidDate.Value = FormatDateTime(item("labAdjustInvalidDate").ToString, DateFormat.ShortDate).ToString

            labOTTypeID.Value = item("labOTTypeID").ToString
            labOTReasonID.Text = item("labOTReasonID").ToString
            '附件的FK
            labOTAttachment.Value = (item("labOTAttachment").ToString).Trim
            labIsProcessDate.Text = (item("labIsProcessDate").ToString).Trim



            labWorkStatus.Text = item("labWorkStatus").ToString
            labWorkType.Text = item("labWorkType").ToString
            labSex.Text = item("labSex").ToString
            labRankID.Text = item("labRankID").ToString
            mRankID = item("labRankID").ToString
            labTitleName.Text = item("labTitleName").ToString
            labPositionID.Text = item("labPositionID").ToString
            labHolidayOrNot.Text = item("labHolidayOrNot").ToString

            labOTStatus.Text = item("labOTStatus").ToString
            labOTFormNO.Text = item("labOTFormNO").ToString
            labTakeOfficeDate.Text = FormatDateTime(item("labTakeOfficeDate").ToString, DateFormat.ShortDate).ToString



            If (labWorkStatus.Text.Equals("在職")) Then
                labLeaveOfficeDate.Text = ""
            ElseIf (labWorkStatus.Text.Equals("離職")) Then
                labLeaveOfficeDate.Text = FormatDateTime(item("labLeaveOfficeDate").ToString, DateFormat.ShortDate).ToString
            Else
                labLeaveOfficeDate.Text = ""
            End If


            If (labOTStatus.Text.Equals("核准") Or (labOTStatus.Text.Equals("送簽"))) Then
                table_tr_Time3.Visible = True
            Else
                table_tr_Time3.Visible = False
            End If


            If "".Equals(item("OTValidID").ToString.Trim) Then
                labDateOfApproval.Text = ""

            Else
                labDateOfApproval.Text = Format(item("labDateOfApproval"), "yyyy/MM/dd HH:mm:ss").ToString
            End If



            labDateOfApplication.Text = Format(item("labDateOfApplication"), "yyyy/MM/dd HH:mm:ss").ToString

            If item("labLastChgID").ToString.Trim <> "" Then
                Dim UserName As String = objOV_1.GetPersonName(item("LastChgComp").ToString, item("labLastChgID").ToString)
                labLastChgID.Text = item("labLastChgID").ToString + IIf(UserName <> "", "-" + UserName, "")
            Else
                labLastChgID.Text = ""
            End If
            labLastChgDate.Text = Format(item("labLastChgDate"), "yyyy/MM/dd HH:mm:ss").ToString
            labOTRegisterID.Text = item("labOTRegisterID").ToString
            'labLastChgID.Text = item("labLastChgID").ToString


            lalOTStartTimeDate.Text = item("OTStartDate").ToString()
            lalTime_one.Text = item("Time_one").ToString
            lalTime_two.Text = item("Time_two").ToString
            lalTime_three.Text = item("Time_three").ToString
            'lalStay_Time.Text = item("Stay_Time").ToString
            If dt.Rows.Count > 1 Then
                Dim item2 As DataRow = dt.Rows(1)
                lalOTStartTimeDate2.Text = item2("OTStartDate").ToString()
                lalTime_one2.Text = item2("Time_one").ToString
                lalTime_two2.Text = item2("Time_two").ToString
                lalTime_three2.Text = item2("Time_three").ToString
                '  lalStay_Time2.Text = item2("Stay_Time").ToString
            Else
                lalOTStartTimeDate2.Text = ""
                lalTime_one2.Text = ""
                lalTime_two2.Text = ""
                lalTime_three2.Text = ""
                ' lalStay_Time2.Text = ""
                table_tr_Time2.Visible = False
            End If


            If "after".Equals(OV_1.Type) Then
                labOTPayDate.Text = item("labOTPayDate").ToString
                If labOTPayDate.Text.Equals("0") Then
                    labOTPayDate.Text = ""
                End If
            End If


            If ("轉薪資".Equals(item("labSalaryOrAdjust"))) Then
                ddlSalaryOrAdjust.SelectedValue = 1
                Label1.Visible = False
                labAdjustInvalidDate.Visible = False
            Else
                ddlSalaryOrAdjust.SelectedValue = 2
            End If



            txtOvertimeDateB.DateText = labOverTimeDate.Value.ToString().Split("~")(0)
            txtOvertimeDateE.DateText = labOverTimeDate.Value.ToString().Split("~")(1)

            OTStartTimeH.SelectedValue = labOTStartDate.Value.ToString().Split(":")(0)
            OTStartTimeM.SelectedValue = labOTStartDate.Value.ToString().Split(":")(1)

            OTEndTimeH.SelectedValue = labOTEndDate.Value.ToString().Split(":")(0)
            OTEndTimeM.SelectedValue = labOTEndDate.Value.ToString().Split(":")(1)


            If dt.Rows.Count > 1 Then
                hidMealTimeTemp = Convert.ToDouble(item("labMealTime")) + Convert.ToDouble(dt.Rows(1).Item("labMealTime"))
            Else
                hidMealTimeTemp = Convert.ToDouble(item("labMealTime"))
            End If
            hidMealFlag.Value = item("cbMealFlag")
            hidMealTime.Value = hidMealTimeTemp

            If "1".Equals(item("cbMealFlag")) Then
                cbMealFlag.Checked = True
            Else
                cbMealFlag.Checked = False
            End If


            LoadTime(OTEndTimeH.SelectedValue.ToString, OTEndTimeM.SelectedValue.ToString, OTStartTimeH.SelectedValue.ToString, OTStartTimeM.SelectedValue.ToString, txtOvertimeDateB.DateText.ToString, txtOvertimeDateE.DateText.ToString, cbMealFlag.Checked)

            Dim mydataStr As String() = item("labOverTimeDate").ToString().Split("/")
            myData.Text = mydataStr(1)




            '附件編號
            strAttachID = UserProfile.UserID.Trim + Date.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")
            Dim objOV42 = New OV4_2


            If labOTAttachment.Value.ToString.Count > 0 Then
                strAttachID = labOTAttachment.Value.ToString
            End If
            ViewState("attach") = strAttachID
            _AttachID = strAttachID

            strAttachAdminURL = String.Format(strAttachAdminBaseURL, _overtimeDBName, strAttachID, "1", "3072", "3072", "")
            strAttachDownloadURL = String.Format(strAttachDownloadBaseURL, _overtimeDBName, strAttachID)
            frameAttach.Value = strAttachAdminURL
            getAttachName()

            'Next
        End If

        Dim OverTimeSumObjectList1 As ArrayList = New ArrayList() '送簽 2
        Dim OverTimeSumObjectList2 As ArrayList = New ArrayList() '核准 3
        Dim OverTimeSumObjectList3 As ArrayList = New ArrayList() '駁回 4

        If dt2.Rows.Count > 0 Then
            For Each item As DataRow In dt2.Rows
                Dim OverTimeSumObject As OV_1 = New OV_1()
                OverTimeSumObject.OvertimeDateB = item("OTStartDate").ToString()
                OverTimeSumObject.OvertimeDateE = item("OTEndDate").ToString()
                OverTimeSumObject.OTStartTime = item("OTStartTime").ToString()
                OverTimeSumObject.OTEndTime = item("OTEndTime").ToString()
                OverTimeSumObject.OTStatus = item("OTStatus").ToString()
                OverTimeSumObject.Time = item("OTTotalTime").ToString()
                OverTimeSumObject.mealTime = item("MealTime").ToString()
                OverTimeSumObject.mealFlag = item("MealFlag").ToString()


                If OverTimeSumObject.OTStatus.Equals("2") Then
                    OverTimeSumObjectList1.Add(OverTimeSumObject)
                ElseIf OverTimeSumObject.OTStatus.Equals("3") Then
                    OverTimeSumObjectList2.Add(OverTimeSumObject)
                ElseIf OverTimeSumObject.OTStatus.Equals("4") Then
                    OverTimeSumObjectList3.Add(OverTimeSumObject)
                End If
            Next
        End If
        If "after".Equals(ViewState("Type")) Then
            labSum.Text = "月份已申報時數合計 : 送簽"
        Else
            labSum.Text = "月份已申請時數合計 : 送簽"
        End If
        labSum1.Text = objOV_1.getSumOTTime(OverTimeSumObjectList1).ToString()
        labSum2.Text = objOV_1.getSumOTTime(OverTimeSumObjectList2).ToString()
        labSum3.Text = objOV_1.getSumOTTime(OverTimeSumObjectList3).ToString()


        For i = 0 To ddlOTTypeID.Items.Count - 1
            If ddlOTTypeID.Items(i).Text.Equals(labOTTypeID.Value) Then
                ddlOTTypeID.SelectedIndex = i
            End If
        Next
        '暫定不擋
        'ddlSalaryOrAdjustChange(mRankID, mStartDate, mEndDate)

    End Sub
#End Region

#Region "功能"

#Region "資料檢核"

    Private Function chackEndToUPdata() As String
        '至少要>0 跟不可超過24小時
        Dim ErrorMsg As String = ""
        Dim OTStartDate As String = txtOvertimeDateB.DateText
        Dim OTEndDate As String = txtOvertimeDateE.DateText
        Dim OTStartTime As String = OTStartTimeH.SelectedValue + OTStartTimeM.SelectedValue
        Dim OTEndTime As String = OTEndTimeH.SelectedValue + OTEndTimeM.SelectedValue
        Dim OV_1 As OV_1 = New OV_1
        Dim objOV42 As OV4_2 = New OV4_2
        Dim OTTotalTime As String = "0"
        Dim CompID As String = ViewState("OTCompID")
        Dim selectDT As OV_1 = New OV_1
        Dim OVA As DataTable
        Dim OVD As DataTable
        Dim OVAOTTxnID As String = ""
        Dim OVDOTTxnID As String = ""
        Dim EmpID As String = ViewState("OTEmpID")
        Dim MealTime As String = IIf(IsNumeric(labMealTime.Text), labMealTime.Text, "0")


        'OTFromAdvanceTxnId()
        If "bef".Equals(ViewState("Type")) Then
            OVAOTTxnID = ViewState("OTTxnID")
            OVA = selectDT.QuiryOldDataTableArrForOTTxnID(OVAOTTxnID, ViewState("Type"))
            OVD = selectDT.QuiryOldDataTableArrFormOTFromAdvanceTxnId(OVAOTTxnID)
            If OVD.Rows.Count > 0 Then
                OVDOTTxnID = OVD.Rows(0).Item("OTTxnID")
            End If

        ElseIf "after".Equals(ViewState("Type")) Then
            OVDOTTxnID = ViewState("OTTxnID")
            OVD = selectDT.QuiryOldDataTableArrForOTTxnID(OVDOTTxnID, ViewState("Type"))
            If OVD.Rows.Count > 0 Then
                OVAOTTxnID = OVD.Rows(0).Item("OTFromAdvanceTxnId")
            End If
            OVA = selectDT.QuiryOldDataTableArrForOTTxnID(OVAOTTxnID, "bef")
        End If

        If "".Equals(OTStartDate.Trim) Then
            ErrorMsg = "開始日期不可為空值"
            Return ErrorMsg
        End If

        If "".Equals(OTEndDate.Trim) Then
            ErrorMsg = "結束日期不可為空值"
            Return ErrorMsg
        End If



        Try
            OTTotalTime = OV_1.getOverTime(OTStartDate, OTEndDate, OTStartTime, OTEndTime, "M")
        Catch ex As Exception
            ErrorMsg = "請確認時間日期格式"
            Return ErrorMsg
        End Try


        If Convert.ToInt32(OTTotalTime) <= 0 Then
            ErrorMsg = "請確認時間日期起訖"
            Return ErrorMsg
        End If

        If Convert.ToInt32(OTTotalTime) >= 1440 Then
            ErrorMsg = "請確認時間日期，加班時數異常"
            Return ErrorMsg
        End If

        If (Convert.ToInt32(OTTotalTime) - Convert.ToInt32(MealTime)) < 0 Then
            ErrorMsg = "加班時數不可為負"
            Return ErrorMsg
        End If


        Dim WorkSiteID As String = ""
        Using dt As DataTable = objOV42.GetEmpData(ViewState("OTCompID").Trim, EmpID)
            If dt.Rows.Count > 0 Then
                WorkSiteID = dt.Rows(0).Item("WorkSiteID").ToString()
            End If
        End Using


        '檢核時間重疊(OverTime_BK)
        Using dt As DataTable = objOV42.CheckOverTimeBK(OTStartDate, OTEndTime, EmpID)
            If (dt.Rows.Count > 0) Then

                For i = 0 To (dt.Rows.Count - 1)    '0500~0700
                    '起迄時間都有重疊
                    If ((dt.Rows(i).Item("BeginTime").ToString() = OTStartTime) And (dt.Rows(i).Item("EndTime").ToString() = OTEndTime)) Then
                        Bsp.Utility.ShowMessage(Me, "該時段已存在人力資源-加班系統，請由人力資源-加班進行查詢")
                        Return ErrorMsg
                    End If

                    '開始時間小於資料庫開始時間
                    If (Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) > OTStartTime) Then
                        '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                        If (OTEndTime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And OTEndTime < Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                            Bsp.Utility.ShowMessage(Me, "該時段已存在人力資源-加班系統，請由人力資源-加班進行查詢")
                            Return ErrorMsg
                            '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                        ElseIf (OTEndTime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And OTEndTime = Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                            Bsp.Utility.ShowMessage(Me, "該時段已存在人力資源-加班系統，請由人力資源-加班進行查詢")
                            Return ErrorMsg
                            '結束時間大於資料庫的開始時間，大於資料庫的結束時間
                        ElseIf (OTEndTime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And OTEndTime > Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                            Bsp.Utility.ShowMessage(Me, "該時段已存在人力資源-加班系統，請由人力資源-加班進行查詢")
                            Return ErrorMsg
                        End If
                    End If

                    '開始時間等於資料庫開始時間
                    If (Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) = OTStartTime) Then
                        '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                        If (OTEndTime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And OTEndTime < Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                            Bsp.Utility.ShowMessage(Me, "該時段已存在人力資源-加班系統，請由人力資源-加班進行查詢")
                            Return ErrorMsg
                            '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                        ElseIf (OTEndTime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And OTEndTime = Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                            Bsp.Utility.ShowMessage(Me, "該時段已存在人力資源-加班系統，請由人力資源-加班進行查詢")
                            Return ErrorMsg
                            '結束時間大於資料庫的開始時間，大於資料庫的結束時間
                        ElseIf (OTEndTime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And OTEndTime > Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                            Bsp.Utility.ShowMessage(Me, "該時段已存在人力資源-加班系統，請由人力資源-加班進行查詢")
                            Return ErrorMsg
                        End If
                    End If

                    '開始時間大於資料庫開始時間
                    If (Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) < OTStartTime) Then
                        '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                        If (OTEndTime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And OTEndTime < Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                            Bsp.Utility.ShowMessage(Me, "該時段已存在人力資源-加班系統，請由人力資源-加班進行查詢")
                            Return ErrorMsg
                            '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                        ElseIf (OTEndTime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And OTEndTime = Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                            Bsp.Utility.ShowMessage(Me, "該時段已存在人力資源-加班系統，請由人力資源-加班進行查詢")
                            Return ErrorMsg
                        ElseIf (Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString()) > OTStartTime) Then
                            Bsp.Utility.ShowMessage(Me, "該時段已存在人力資源-加班系統，請由人力資源-加班進行查詢")
                            Return ErrorMsg
                        End If
                    End If
                Next
            End If
        End Using



        '檢核時間重疊(NaturalDisasterByCity)
        Using dt As DataTable = objOV42.CheckNaturalDisasterByCity(OTStartDate, OTEndDate, WorkSiteID, UserProfile.SelectCompRoleID)
            If (dt.Rows.Count > 0) Then
                For i = 0 To (dt.Rows.Count - 1)
                    '起迄時間都有重疊
                    If ((dt.Rows(i).Item("BeginTime").ToString() = OTStartTime) And (dt.Rows(i).Item("EndTime").ToString() = OTEndTime)) Then
                        ErrorMsg = "留守時段重複"
                        Return ErrorMsg
                    End If
                    '開始時間小於資料庫開始時間
                    If (Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) > OTStartTime) Then
                        '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                        If (OTEndTime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And OTEndTime < Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                            ErrorMsg = "留守時段重複"
                            Return ErrorMsg
                            '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                        ElseIf (OTEndTime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And OTEndTime = Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                            ErrorMsg = "留守時段重複"
                            Return ErrorMsg
                            '結束時間大於資料庫的開始時間，大於資料庫的結束時間
                        ElseIf (OTEndTime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And OTEndTime > Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                            ErrorMsg = "留守時段重複"
                            Return ErrorMsg
                        End If
                    End If
                    '開始時間等於資料庫開始時間
                    If (Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) = OTStartTime) Then
                        '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                        If (OTEndTime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And OTEndTime < Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                            ErrorMsg = "留守時段重複"
                            Return ErrorMsg
                            '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                        ElseIf (OTEndTime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And OTEndTime = Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                            ErrorMsg = "留守時段重複"
                            Return ErrorMsg
                            '結束時間大於資料庫的開始時間，大於資料庫的結束時間
                        ElseIf (OTEndTime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And OTEndTime > Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                            ErrorMsg = "留守時段重複"
                            Return ErrorMsg
                        End If
                    End If
                    '開始時間大於資料庫開始時間
                    If (Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) < OTStartTime) Then
                        '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                        If (OTEndTime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And OTEndTime < Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                            ErrorMsg = "留守時段重複"
                            Return ErrorMsg
                            '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                        ElseIf (OTEndTime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And OTEndTime = Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                            ErrorMsg = "留守時段重複"
                            Return ErrorMsg
                        ElseIf (Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString()) > OTStartTime) Then
                            ErrorMsg = "留守時段重複"
                            Return ErrorMsg
                        End If
                    End If
                Next
            End If
        End Using

        '檢核時間重疊(NaturalDisasterByCity)
        Using dt As DataTable = objOV42.CheckNaturalDisasterByEmp(OTStartDate, OTEndDate, EmpID, UserProfile.SelectCompRoleID)
            If (dt.Rows.Count > 0) Then
                For i = 0 To (dt.Rows.Count - 1)
                    '起迄時間都有重疊
                    If ((dt.Rows(i).Item("BeginTime").ToString() = OTStartTime) And (dt.Rows(i).Item("EndTime").ToString() = OTEndTime)) Then
                        ErrorMsg = "留守時段重複"
                        Return ErrorMsg
                    End If
                    '開始時間小於資料庫開始時間
                    If (Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) > OTStartTime) Then
                        '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                        If (OTEndTime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And OTEndTime < Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                            ErrorMsg = "留守時段重複"
                            Return ErrorMsg
                            '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                        ElseIf (OTEndTime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And OTEndTime = Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                            ErrorMsg = "留守時段重複"
                            Return ErrorMsg
                            '結束時間大於資料庫的開始時間，大於資料庫的結束時間
                        ElseIf (OTEndTime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And OTEndTime > Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                            ErrorMsg = "留守時段重複"
                            Return ErrorMsg
                        End If
                    End If
                    '開始時間等於資料庫開始時間
                    If (Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) = OTStartTime) Then
                        '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                        If (OTEndTime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And OTEndTime < Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                            ErrorMsg = "留守時段重複"
                            Return ErrorMsg
                            '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                        ElseIf (OTEndTime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And OTEndTime = Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                            ErrorMsg = "留守時段重複"
                            Return ErrorMsg
                            '結束時間大於資料庫的開始時間，大於資料庫的結束時間
                        ElseIf (OTEndTime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And OTEndTime > Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                            ErrorMsg = "留守時段重複"
                            Return ErrorMsg
                        End If
                    End If
                    '開始時間大於資料庫開始時間
                    If (Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) < OTStartTime) Then
                        '結束時間大於資料庫的開始時間，小於資料庫的結束時間
                        If (OTEndTime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And OTEndTime < Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                            ErrorMsg = "留守時段重複"
                            Return ErrorMsg
                            '結束時間大於資料庫的開始時間，等於資料庫的結束時間
                        ElseIf (OTEndTime > Convert.ToInt32(dt.Rows(i).Item("BeginTime").ToString()) And OTEndTime = Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString())) Then
                            ErrorMsg = "留守時段重複"
                            Return ErrorMsg
                        ElseIf (Convert.ToInt32(dt.Rows(i).Item("EndTime").ToString()) > OTStartTime) Then
                            ErrorMsg = "留守時段重複"
                            Return ErrorMsg
                        End If
                    End If
                Next
            End If
        End Using

        '20170304kevin
        If "bef".Equals(ViewState("Type")) Then
            '檢核時間重疊(預先申請)
            Using dt As DataTable = OV_1.CheckOverTimeAdvance(OTStartDate, OTEndDate, EmpID, ViewState("OTTxnID"))
                Dim bTimeCover As Boolean = OV_1.isTimeCover(dt, CompID, EmpID, OTStartDate, OTEndDate, OTStartTime, OTEndTime, "OTCompID", "OTEmpID", "OTStartDate", "OTEndDate", "OTStartTime", "OTEndTime")
                If bTimeCover Then
                    ErrorMsg = "您欲修改的加班時間區間已有申請紀錄"
                    Return ErrorMsg
                End If
            End Using

            '檢核時間重疊(事後申報)
            Using dt As DataTable = OV_1.CheckOverTimeDeclaration(OTStartDate, OTEndDate, EmpID, OVDOTTxnID)
                Dim bTimeCover As Boolean = OV_1.isTimeCover(dt, CompID, EmpID, OTStartDate, OTEndDate, OTStartTime, OTEndTime, "OTCompID", "OTEmpID", "OTStartDate", "OTEndDate", "OTStartTime", "OTEndTime")
                If bTimeCover Then
                    ErrorMsg = "您欲修改的加班時間區間已有申報紀錄"
                    Return ErrorMsg
                End If
            End Using
        Else

            '檢核時間重疊(預先申請)
            Using dt As DataTable = OV_1.CheckOverTimeAdvance(OTStartDate, OTEndDate, EmpID, OVAOTTxnID)
                Dim bTimeCover As Boolean = OV_1.isTimeCover(dt, CompID, EmpID, OTStartDate, OTEndDate, OTStartTime, OTEndTime, "OTCompID", "OTEmpID", "OTStartDate", "OTEndDate", "OTStartTime", "OTEndTime")
                If bTimeCover Then
                    ErrorMsg = "您欲修改的加班時間區間已有申請紀錄"
                    Return ErrorMsg
                End If
            End Using

            '檢核時間重疊(事後申報)
            Using dt As DataTable = OV_1.CheckOverTimeDeclaration(OTStartDate, OTEndDate, EmpID, ViewState("OTTxnID"))
                Dim bTimeCover As Boolean = OV_1.isTimeCover(dt, CompID, EmpID, OTStartDate, OTEndDate, OTStartTime, OTEndTime, "OTCompID", "OTEmpID", "OTStartDate", "OTEndDate", "OTStartTime", "OTEndTime")
                If bTimeCover Then
                    ErrorMsg = "您欲修改的加班時間區間已有申報紀錄"
                    Return ErrorMsg
                End If
            End Using
        End If

        Return ErrorMsg
    End Function

    Private Function isRightTime() As Boolean
        Try
            Dim OV_1 = New OV_1()
            Dim StartDate As String = txtOvertimeDateB.DateText
            Dim EndDate As String = txtOvertimeDateE.DateText
            Dim StartTime As String = OTStartTimeH.SelectedValue + OTStartTimeM.SelectedValue
            Dim EndTime As String = OTEndTimeH.SelectedValue + OTEndTimeM.SelectedValue
            Dim meatTime As String = labMealTime.Text


            If Not IsNumeric(meatTime) Then
                meatTime = "0"
            End If
            Dim OverTime As String = Convert.ToInt32(OV_1.getOverTime(StartDate, EndDate, StartTime, EndTime, "M")) - Convert.ToInt32(meatTime)
            If Convert.ToInt32(OverTime) < 0 Then
                Return False
            End If
            If Convert.ToInt32(OverTime) > 24 * 60 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
        Return False
    End Function

#Region "留守檢核"
    Public Function getStayInterval(ByVal AddDate As String, ByVal AddSTime As String, ByVal AddETime As String) As Array
        Dim result(2) As String
        Dim strSQL_Stay As New StringBuilder
        Dim data_Stay As DataTable
        Dim data_Stay_Count As Integer
        Dim isNArea As Boolean = True
        Dim isNEmp As Boolean = True

        '待完成，條件尚未設定
        strSQL_Stay.Append("select ")
        strSQL_Stay.Append("where ")

        data_Stay = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL_Stay.ToString(), "AattendantDB").Tables(0)
        data_Stay_Count = data_Stay.Rows.Count

        '代表有找到相符合的資料
        If data_Stay_Count <> 0 Then
            '待處理
            isNArea = False
        End If

        If isNArea Then
            strSQL_Stay.Clear()
            data_Stay.Clear()

            strSQL_Stay.Append("select ")
            strSQL_Stay.Append("where ")

            data_Stay = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL_Stay.ToString(), "AattendantDB").Tables(0)
            data_Stay_Count = data_Stay.Rows.Count

            If data_Stay_Count <> 0 Then
                '待處理
            Else
                isNEmp = False
            End If

        End If

        Return result
    End Function
#End Region
#End Region

    ''' <summary>
    ''' 載入頁面資料到OV_1
    ''' </summary>
    ''' <param name="OTStartDate"></param>
    ''' <param name="OTEndDate"></param>
    ''' <param name="OTStartTime"></param>
    ''' <param name="OTEndTime"></param>
    ''' <param name="MealFlag"></param>
    ''' <param name="MealTime"></param>
    ''' <param name="SalaryOrAdjust"></param>
    ''' <param name="AdjustInvalidDate"></param>
    ''' <param name="OTTypeID"></param>
    ''' <param name="OTReasonMemo"></param>
    ''' <param name="Attachment"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function setPropertyForUpData(ByVal OTStartDate As String, ByVal OTEndDate As String, ByVal OTStartTime As String, ByVal OTEndTime As String, ByVal MealFlag As String, ByVal MealTime As String, ByVal SalaryOrAdjust As String, ByVal AdjustInvalidDate As String, ByVal OTTypeID As String, ByVal OTReasonMemo As String, ByVal Attachment As String) As OV_1
        Dim OV_1 = New OV_1()
        OV_1.Type = ViewState("Type")
        OV_1.OvertimeDateB = OTStartDate
        OV_1.OvertimeDateE = OTEndDate
        OV_1.OTStartTime = OTStartTime
        OV_1.OTEndTime = OTEndTime
        OV_1.mealFlag = MealFlag
        OV_1.mealTime = MealTime
        OV_1.SalaryOrAdjust = SalaryOrAdjust
        OV_1.AdjustInvalidDate = AdjustInvalidDate
        OV_1.Time = OV_1.getOverTime(OTStartDate, OTEndDate, OTStartTime, OTEndTime, "M")
        OV_1.OTTypeID = OTTypeID
        OV_1.OTReasonMemo = OTReasonMemo
        OV_1.OTAttachment = labOTAttachment.Value
        OV_1.LastChgComp = UserProfile.CompID
        OV_1.LastChgID = UserProfile.UserID
        OV_1.CompID = ViewState("OTCompID")
        OV_1.OTEmpID = ViewState("OTEmpID")
        OV_1.OTTxnID = ViewState("OTTxnID")
        Return OV_1
    End Function

    ''' <summary>
    ''' 顯示訊息
    ''' </summary>
    ''' <param name="flag"></param>
    ''' <remarks></remarks>
    Private Sub showMagAndGoBackFromflag(ByVal flag As Boolean)
        If flag Then
            Bsp.Utility.ShowMessage(Me, "修改成功")
            GoBack()
        Else
            Bsp.Utility.ShowMessage(Me, "修改失敗")
        End If
    End Sub


    ''' <summary>
    ''' 傳入事先申請DataTable 轉換成OV4_OVA物件
    ''' </summary>
    ''' <param name="DataTable"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function setProperTyForOV4_OVA(ByVal DataTable As DataTable) As OV4_OVA
        Dim ova As OV4_OVA = New OV4_OVA
        ova.OTCompID = DataTable.Rows(0).Item("OTCompID")
        ova.OTEmpID = DataTable.Rows(0).Item("OTEmpID")
        ova.OTStartDate = DataTable.Rows(0).Item("OTStartDate")
        ova.OTEndDate = DataTable.Rows(0).Item("OTEndDate")
        ova.OTSeq = DataTable.Rows(0).Item("OTSeq")
        ova.OTTxnID = DataTable.Rows(0).Item("OTTxnID")
        ova.OTSeqNo = DataTable.Rows(0).Item("OTSeqNo")
        ova.DeptID = DataTable.Rows(0).Item("DeptID")
        ova.OrganID = DataTable.Rows(0).Item("OrganID")
        ova.DeptName = DataTable.Rows(0).Item("DeptName")
        ova.OrganName = DataTable.Rows(0).Item("OrganName")
        ova.FlowCaseID = DataTable.Rows(0).Item("FlowCaseID")
        ova.OTStartTime = DataTable.Rows(0).Item("OTStartTime")
        ova.OTEndTime = DataTable.Rows(0).Item("OTEndTime")
        ova.OTTotalTime = DataTable.Rows(0).Item("OTTotalTime")
        ova.SalaryOrAdjust = DataTable.Rows(0).Item("SalaryOrAdjust")
        ova.AdjustInvalidDate = Convert.ToDateTime(DataTable.Rows(0).Item("AdjustInvalidDate")).ToString("yyyy/MM/dd HH:mm:ss")
        ova.MealFlag = DataTable.Rows(0).Item("MealFlag")
        ova.MealTime = DataTable.Rows(0).Item("MealTime")
        ova.OTTypeID = DataTable.Rows(0).Item("OTTypeID")
        ova.OTReasonID = DataTable.Rows(0).Item("OTReasonID")
        ova.OTReasonMemo = DataTable.Rows(0).Item("OTReasonMemo")
        ova.OTAttachment = DataTable.Rows(0).Item("OTAttachment")
        ova.OTFormNO = DataTable.Rows(0).Item("OTFormNO")
        ova.OTRegisterID = DataTable.Rows(0).Item("OTRegisterID")
        ova.OTRegisterDate = Convert.ToDateTime(DataTable.Rows(0).Item("OTRegisterDate")).ToString("yyyy/MM/dd HH:mm:ss")
        ova.OTStatus = DataTable.Rows(0).Item("OTStatus")
        ova.HolidayOrNot = DataTable.Rows(0).Item("HolidayOrNot")
        ova.OTValidDate = Convert.ToDateTime(DataTable.Rows(0).Item("OTValidDate")).ToString("yyyy/MM/dd HH:mm:ss")
        ova.OTValidID = DataTable.Rows(0).Item("OTValidID")
        ova.OTRejectDate = Convert.ToDateTime(DataTable.Rows(0).Item("OTRejectDate")).ToString("yyyy/MM/dd HH:mm:ss")
        ova.OTRejectID = DataTable.Rows(0).Item("OTRejectID")
        ova.OTGovernmentNo = DataTable.Rows(0).Item("OTGovernmentNo")
        ova.LastChgComp = DataTable.Rows(0).Item("LastChgComp")
        ova.LastChgID = DataTable.Rows(0).Item("LastChgID")
        ova.LastChgDate = DataTable.Rows(0).Item("LastChgDate")
        ova.LastChgDate = Convert.ToDateTime(DataTable.Rows(0).Item("LastChgDate")).ToString("yyyy/MM/dd HH:mm:ss")
        ova.OTRegisterComp = DataTable.Rows(0).Item("OTRegisterComp")

        Return ova
    End Function

    ''' <summary>
    '''  傳入事後申報DataTable 轉換成OV4_OVD物件
    ''' </summary>
    ''' <param name="DataTable"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function setProperTyForOV4_OVD(ByVal DataTable As DataTable) As OV4_OVD
        Dim ovd As OV4_OVD = New OV4_OVD
        ovd.OTCompID = DataTable.Rows(0).Item("OTCompID")
        ovd.OTEmpID = DataTable.Rows(0).Item("OTEmpID")
        ovd.OTStartDate = DataTable.Rows(0).Item("OTStartDate")
        ovd.OTEndDate = DataTable.Rows(0).Item("OTEndDate")
        ovd.OTSeq = DataTable.Rows(0).Item("OTSeq")
        ovd.OTTxnID = DataTable.Rows(0).Item("OTTxnID")
        ovd.OTSeqNo = DataTable.Rows(0).Item("OTSeqNo")
        ovd.DeptID = DataTable.Rows(0).Item("DeptID")
        ovd.OrganID = DataTable.Rows(0).Item("OrganID")
        ovd.DeptName = DataTable.Rows(0).Item("DeptName")
        ovd.OrganName = DataTable.Rows(0).Item("OrganName")
        ovd.FlowCaseID = DataTable.Rows(0).Item("FlowCaseID")
        ovd.OTStartTime = DataTable.Rows(0).Item("OTStartTime")
        ovd.OTEndTime = DataTable.Rows(0).Item("OTEndTime")
        ovd.OTTotalTime = DataTable.Rows(0).Item("OTTotalTime")
        ovd.SalaryOrAdjust = DataTable.Rows(0).Item("SalaryOrAdjust")
        ovd.AdjustInvalidDate = Convert.ToDateTime(DataTable.Rows(0).Item("AdjustInvalidDate")).ToString("yyyy/MM/dd HH:mm:ss")
        ovd.MealFlag = DataTable.Rows(0).Item("MealFlag")
        ovd.MealTime = DataTable.Rows(0).Item("MealTime")
        ovd.OTTypeID = DataTable.Rows(0).Item("OTTypeID")
        ovd.OTReasonID = DataTable.Rows(0).Item("OTReasonID")
        ovd.OTReasonMemo = DataTable.Rows(0).Item("OTReasonMemo")
        ovd.OTAttachment = DataTable.Rows(0).Item("OTAttachment")
        ovd.OTFormNO = DataTable.Rows(0).Item("OTFormNO")
        ovd.OTRegisterID = DataTable.Rows(0).Item("OTRegisterID")
        ovd.OTRegisterDate = Convert.ToDateTime(DataTable.Rows(0).Item("OTRegisterDate")).ToString("yyyy/MM/dd HH:mm:ss")
        ovd.OTStatus = DataTable.Rows(0).Item("OTStatus")
        ovd.HolidayOrNot = DataTable.Rows(0).Item("HolidayOrNot")
        ovd.OTValidDate = Convert.ToDateTime(DataTable.Rows(0).Item("OTValidDate")).ToString("yyyy/MM/dd HH:mm:ss")
        ovd.OTValidID = DataTable.Rows(0).Item("OTValidID")
        ovd.OTRejectDate = Convert.ToDateTime(DataTable.Rows(0).Item("OTRejectDate")).ToString("yyyy/MM/dd HH:mm:ss")
        ovd.OTRejectID = DataTable.Rows(0).Item("OTRejectID")
        ovd.OTGovernmentNo = DataTable.Rows(0).Item("OTGovernmentNo")
        ovd.LastChgComp = DataTable.Rows(0).Item("LastChgComp")
        ovd.LastChgID = DataTable.Rows(0).Item("LastChgID")
        ovd.LastChgDate = Convert.ToDateTime(DataTable.Rows(0).Item("LastChgDate")).ToString("yyyy/MM/dd HH:mm:ss")
        ovd.OTFromAdvanceTxnId = DataTable.Rows(0).Item("OTFromAdvanceTxnId")
        ovd.AdjustStatus = DataTable.Rows(0).Item("AdjustStatus")
        ovd.AdjustDate = Convert.ToDateTime(DataTable.Rows(0).Item("AdjustDate")).ToString("yyyy/MM/dd HH:mm:ss")
        ovd.OverTimeFlag = DataTable.Rows(0).Item("OverTimeFlag")
        ovd.ToOverTimeDate = Convert.ToDateTime(DataTable.Rows(0).Item("ToOverTimeDate")).ToString("yyyy/MM/dd HH:mm:ss")
        ovd.ToOverTimeFlag = DataTable.Rows(0).Item("ToOverTimeFlag")
        ovd.OTSalaryPaid = DataTable.Rows(0).Item("OTSalaryPaid")
        ovd.ProcessDate = Convert.ToDateTime(DataTable.Rows(0).Item("ProcessDate")).ToString("yyyy/MM/dd HH:mm:ss")
        ovd.OTPayDate = DataTable.Rows(0).Item("OTPayDate")
        ovd.OTModifyDate = Convert.ToDateTime(DataTable.Rows(0).Item("OTModifyDate")).ToString("yyyy/MM/dd HH:mm:ss")
        ovd.OTRemark = DataTable.Rows(0).Item("OTRemark")
        ovd.KeyInComp = DataTable.Rows(0).Item("KeyInComp")
        ovd.KeyInID = DataTable.Rows(0).Item("KeyInID")
        ovd.HRKeyInFlag = DataTable.Rows(0).Item("HRKeyInFlag")
        ovd.OTRegisterComp = DataTable.Rows(0).Item("OTRegisterComp")

        Return ovd
    End Function

    ''' <summary>
    ''' 頁面資料回復
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub backData()
        labAdjustInvalidDate.Text = hidAdjustInvalidDate.Value
        If ("轉薪資".Equals(labSalaryOrAdjust.Value)) Then
            ddlSalaryOrAdjust.SelectedValue = 1
            Label1.Visible = False
            labAdjustInvalidDate.Visible = False
        Else
            ddlSalaryOrAdjust.SelectedValue = 2
        End If

        txtOvertimeDateB.DateText = labOverTimeDate.Value.ToString().Split("~")(0)
        txtOvertimeDateE.DateText = labOverTimeDate.Value.ToString().Split("~")(1)

        OTStartTimeH.SelectedValue = labOTStartDate.Value.ToString().Split(":")(0)
        OTStartTimeM.SelectedValue = labOTStartDate.Value.ToString().Split(":")(1)

        OTEndTimeH.SelectedValue = labOTEndDate.Value.ToString().Split(":")(0)
        OTEndTimeM.SelectedValue = labOTEndDate.Value.ToString().Split(":")(1)
        labMealTime.Text = hidMealTime.Value
        If "1".Equals(hidMealFlag.Value) Then
            cbMealFlag.Checked = True
        Else
            cbMealFlag.Checked = False
        End If

        LoadTime(OTEndTimeH.SelectedValue.ToString, OTEndTimeM.SelectedValue.ToString, OTStartTimeH.SelectedValue.ToString, OTStartTimeM.SelectedValue.ToString, txtOvertimeDateB.DateText.ToString, txtOvertimeDateE.DateText.ToString, cbMealFlag.Checked)

    End Sub

    ''' <summary>
    '''  總時數時間計算
    ''' </summary>
    ''' <param name="OTEndTimeH"></param>
    ''' <param name="OTEndTimeM"></param>
    ''' <param name="OTStartTimeH"></param>
    ''' <param name="OTStartTimeM"></param>
    ''' <param name="txtOvertimeDateB"></param>
    ''' <param name="txtOvertimeDateE"></param>
    ''' <param name="MealFlag"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function LoadTime(ByVal OTEndTimeH As String, ByVal OTEndTimeM As String, ByVal OTStartTimeH As String, ByVal OTStartTimeM As String, ByVal txtOvertimeDateB As String, ByVal txtOvertimeDateE As String, ByVal MealFlag As Boolean) As Double
        Dim OTEndTime As String = OTEndTimeH + OTEndTimeM
        Dim OTStartTime As String = OTStartTimeH + OTStartTimeM
        Dim OV_1 = New OV_1
        '開始日期 結束日期 開始時間 結束時間
        Dim overTimeM As String = OV_1.getOverTime(txtOvertimeDateB, txtOvertimeDateE, OTStartTime, OTEndTime, "M")
        Dim overTimeH = CDbl(FormatNumber((Convert.ToDouble(overTimeM) / 60), 1)).ToString()
        Dim str As String = "加班時數：" + overTimeH + "小時"
        Dim mealTime = 0

        If Regex.IsMatch(labMealTime.Text, "^\d+$") Or "".Equals(labMealTime.Text.Trim) Then
            tbOTlabMealTimeErrorMsg.Visible = False
            If "".Equals(labMealTime.Text.Trim) Then
                mealTime = 0
            Else
                mealTime = labMealTime.Text
            End If
        Else
            tbOTlabMealTimeErrorMsg.Visible = True
        End If
        cbMealFlag.Checked = MealFlag

        If MealFlag = True Then
            labMealTime.Enabled = True
            If (Not "0".Equals(mealTime.ToString.Trim) And Not "".Equals(mealTime.ToString.Trim)) Then
                Dim timeSpan1 = New TimeSpan(0, overTimeM, 0)
                Dim timeSpan2 = New TimeSpan(0, mealTime, 0)
                overTimeH = CDbl(FormatNumber((timeSpan1.Subtract(timeSpan2)).TotalHours, 1)).ToString()
                str = "加班時數：" + overTimeH + "小時"
                labOverTimeStr1.Text = "(已扣除用餐時數" + mealTime.ToString + "分鐘)"
                cbMealFlag.Style.Value = "color:Red;"
                labMealTime.Style.Value = "color:Red;"
                Label5.Style.Value = "color:Red;"
            Else
                labOverTimeStr1.Text = ""
            End If
        Else
            labMealTime.Text = ""
            labMealTime.Enabled = False
            str = "加班時數：" + overTimeH + "小時"
            labOverTimeStr1.Text = ""
            cbMealFlag.Style.Value = "color:black;"
            labMealTime.Style.Value = "color:black;"
            Label5.Style.Value = "color:black;"
        End If
        labOverTimeStr.Text = str
        Return overTimeH
    End Function


    ''' <summary>
    ''' 時間計算(全)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub controlTimeText()
        Dim OV_1 = New OV_1()
        Dim StartDate As String = txtOvertimeDateB.DateText
        Dim EndDate As String = txtOvertimeDateE.DateText
        Dim OTCompID As String = ViewState("OTCompID")

        If StartDate.Equals(EndDate) Then
            table_tr_Time2.Visible = False
        Else
            table_tr_Time2.Visible = True
        End If
        Dim StartTime As String = OTStartTimeH.SelectedValue + OTStartTimeM.SelectedValue
        Dim EndTime As String = OTEndTimeH.SelectedValue + OTEndTimeM.SelectedValue
        Dim meatTime As String = labMealTime.Text
        If Not IsNumeric(meatTime) Then
            meatTime = "0"
        End If
        Dim OverTime As String = Convert.ToInt32(OV_1.getOverTime(StartDate, EndDate, StartTime, EndTime, "M")) - Convert.ToInt32(meatTime)
        OV_1.Type = ViewState("Type").ToString


        LoadTime(OTEndTimeH.SelectedValue.ToString, OTEndTimeM.SelectedValue.ToString, OTStartTimeH.SelectedValue.ToString, OTStartTimeM.SelectedValue.ToString, txtOvertimeDateB.DateText.ToString, txtOvertimeDateE.DateText.ToString, cbMealFlag.Checked)


        labAdjustInvalidDate.Text = OV_1.getOV4002labAdjustInvalidDate(OTCompID)


        If "".Equals(meatTime) Or Not IsNumeric(meatTime) Then
            meatTime = 0
        End If



        Dim meatFlag As String
        If cbMealFlag.Checked Then
            meatFlag = "1"
        Else
            meatFlag = "0"
        End If


        '****時段計算**************************************************************************************
        If (labOTStatus.Text.Equals("核准") Or (labOTStatus.Text.Equals("送簽"))) Then
            Dim dt As DataTable = OV_1.getOV4002ForTimeDataTable(ViewState("OTCompID").ToString(), ViewState("OTEmpID").ToString, StartDate, EndDate, StartTime, EndTime, "", ViewState("OTTxnID").ToString, meatFlag, meatTime)
            If dt.Rows.Count > 0 Then
                Dim item As DataRow = dt.Rows(0)

                lalOTStartTimeDate.Text = item("OTStartDate").ToString()
                lalTime_one.Text = item("Time_one").ToString
                lalTime_two.Text = item("Time_two").ToString
                lalTime_three.Text = item("Time_three").ToString
                'lalStay_Time.Text = item("Stay_Time").ToString
                If dt.Rows.Count > 1 Then
                    Dim item2 As DataRow = dt.Rows(1)
                    lalOTStartTimeDate2.Text = item2("OTStartDate").ToString()
                    lalTime_one2.Text = item2("Time_one").ToString
                    lalTime_two2.Text = item2("Time_two").ToString
                    lalTime_three2.Text = item2("Time_three").ToString
                    '  lalStay_Time2.Text = item2("Stay_Time").ToString
                Else
                    lalOTStartTimeDate2.Text = ""
                    lalTime_one2.Text = ""
                    lalTime_two2.Text = ""
                    lalTime_three2.Text = ""
                    ' lalStay_Time2.Text = ""
                    table_tr_Time2.Visible = False
                End If
            End If
        End If

        '******************************************************************************************


    End Sub


#End Region

#Region "附件專區"

    ' ''' <summary>
    ' ''' 上傳檔案
    ' ''' </summary>
    ' ''' <param name="dt"></param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Private Function UploadFile(ByVal dt As DataTable) As String
    '    '上傳儲存檔案的資料夾
    '    Dim savePath As String = "..\Download"
    '    If Directory.Exists(savePath) = False Then
    '        '建立savePath資料夾
    '        Directory.CreateDirectory(savePath)
    '    End If
    '    If FileUpload.HasFile Then
    '        '獲取要上傳的檔案名稱
    '        Dim fileName As String = FileUpload.FileName
    '        '獲取要上傳儲存檔案的完整路徑
    '        savePath += fileName
    '        '執行檔上傳操作
    '        FileUpload.SaveAs(savePath)
    '        lblUploadStatus.Text = "檔案上傳成功!"
    '    Else
    '        lblUploadStatus.Text = "沒有檔案被上傳!"
    '    End If
    '    Return ""
    'End Function

    ''' <summary>
    '''  顯示頁面附件檔名
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub updateAttachName_Click(sender As Object, e As System.EventArgs) Handles updateAttachName.Click
        '_SelectChange = True
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
                    'FileUpload.Visible = False
                    _AttachID = UserProfile.UserID.Trim + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")
                    ViewState("AttchIn") = True
                    strAttachID = ViewState("attach").ToString()
                    objOV42.InsertAttach(_AttachID, ViewState("attach").ToString())
                Else
                    chkCopyAtt.Visible = True
                    ViewState("AttchIn") = False
                    strAttachID = _AttachID
                End If
            Case False
                If ViewState("attach").ToString().IndexOf("test") < 0 Then
                    '附件也需要清掉
                    objOV42.DeleteAttach(_AttachID)
                    strAttachID = _AttachID
                    'FileUpload.Visible = True
                    chkCopyAtt.Visible = True
                Else
                    chkCopyAtt.Visible = False
                End If
            Case Else

        End Select

        '附件Attach   
        Dim strAttachAdminURL As String
        'Dim strAttachAdminBaseURL As String = ConfigurationManager.AppSettings("AattendantWebPath") + Util._AttachAdminUrl + "?AttachDB={0}&AttachID={1}&AttachFileMaxQty={2}&AttachFileMaxKB={3}&AttachFileTotKB={4}&AttachFileExtList={5}"
        Dim strAttachAdminBaseURL As String = Util._AttachAdminUrl + "?AttachDB={0}&AttachID={1}&AttachFileMaxQty={2}&AttachFileMaxKB={3}&AttachFileTotKB={4}&AttachFileExtList={5}"
        Dim strAttachDownloadURL As String
        Dim strAttachDownloadBaseURL As String = Util._AttachDownloadUrl + "?AttachDB={0}&AttachID={1}"
        '附件編號
        strAttachAdminURL = String.Format(strAttachAdminBaseURL, _overtimeDBName, strAttachID, "1", "3072", "3072", "")
        strAttachDownloadURL = String.Format(strAttachDownloadBaseURL, _overtimeDBName, strAttachID)
        frameAttach.Value = strAttachAdminURL
        getAttachName()

    End Sub

    Shared Function ByteArrayToStr(ByVal bt As Byte()) As String
        Dim encoding As New System.Text.ASCIIEncoding()
        Return encoding.GetString(bt)
    End Function
#End Region

#Region "資料變更事件"


    Protected Sub ddlSalaryOrAdjust_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlSalaryOrAdjust.SelectedIndexChanged
        If ("1".Equals(ddlSalaryOrAdjust.SelectedValue)) Then
            Label1.Visible = False
            labAdjustInvalidDate.Visible = False
        Else
            Label1.Visible = True
            labAdjustInvalidDate.Visible = True
        End If
        If isRightTime() Then
            controlTimeText()
        Else
            Bsp.Utility.ShowMessage(Me, "加班時數異常請確認填寫資料")
        End If

    End Sub

    Protected Sub OTStartTimeH_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles OTStartTimeH.SelectedIndexChanged
        If isRightTime() Then
            controlTimeText()
        Else
            Bsp.Utility.ShowMessage(Me, "請確認加班日期時間")
        End If

    End Sub

    Protected Sub OTStartTimeM_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles OTStartTimeM.SelectedIndexChanged
        If isRightTime() Then
            controlTimeText()
        Else
            Bsp.Utility.ShowMessage(Me, "請確認加班日期時間")
        End If
    End Sub

    Protected Sub OTEndTimeH_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles OTEndTimeH.SelectedIndexChanged
        If isRightTime() Then
            controlTimeText()
        Else
            Bsp.Utility.ShowMessage(Me, "請確認加班日期時間")
        End If
    End Sub

    Protected Sub OTEndTimeM_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles OTEndTimeM.SelectedIndexChanged
        If isRightTime() Then
            controlTimeText()
        Else
            Bsp.Utility.ShowMessage(Me, "請確認加班日期時間")
        End If
    End Sub

    Protected Sub cbMealFlag_CheckedChanged(sender As Object, e As System.EventArgs) Handles cbMealFlag.CheckedChanged
        If isRightTime() Then
            controlTimeText()
        Else
            Bsp.Utility.ShowMessage(Me, "請確認加班日期時間")
        End If
    End Sub

    Protected Sub labMealTime_TextChanged(sender As Object, e As System.EventArgs) Handles labMealTime.TextChanged
        If isRightTime() Then
            controlTimeText()
        Else
            Bsp.Utility.ShowMessage(Me, "請確認加班日期時間")
        End If
    End Sub

    Protected Sub OvertimeDate_Click(sender As Object, e As System.EventArgs) Handles OvertimeDate.Click
        If isRightTime() Then
            controlTimeText()
        Else
            Bsp.Utility.ShowMessage(Me, "請確認加班日期時間")
        End If
    End Sub

    ''' <summary>
    ''' 依照RankID階級與加班起迄日來控制 加班轉換方式的下拉選項
    ''' </summary>
    ''' <param name="sRankID"></param>
    ''' <param name="startDate"></param>
    ''' <param name="endDate"></param>
    ''' <remarks>
    ''' RankID大於等於19 : 只能轉補休
    ''' RankID小於19且兩天皆為假日 : 可轉補休或轉薪資
    ''' RankID小於19且除了兩天皆為假日以外 : 只能轉薪資
    ''' </remarks>
    Private Sub ddlSalaryOrAdjustChange(ByVal sRankID As String, ByVal startDate As String, ByVal endDate As String)
        Dim objOV42 As New OV4_2
        If Not String.IsNullOrEmpty(sRankID) AndAlso Not String.IsNullOrEmpty(startDate) AndAlso Not String.IsNullOrEmpty(endDate) Then
            If IsNumeric(sRankID) = True Then
                Dim dRankID = Decimal.Parse(sRankID)
                If dRankID >= 19 Then
                    '如果RankID大於等於19只能轉補休
                    'Debug.Print(ddlSalaryOrAdjust.Items.Count)
                    'Debug.Print(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉補休")).ToString())

                    'ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉薪資"))).Selected = False
                    ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉薪資"))).Enabled = False
                    'ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉補休"))).Selected = True
                    ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉補休"))).Enabled = True
                ElseIf dRankID < 19 Then
                    'bool datesIsDifferent = at.CompareDate(startDate, endDate) != 0;
                    Dim dateStartIsHoliday As Boolean = objOV42.CheckHolidayOrNot(startDate)
                    Dim dateEndIsHoliday As Boolean = objOV42.CheckHolidayOrNot(endDate)
                    If dateStartIsHoliday AndAlso dateEndIsHoliday Then
                        'RankID小於19且兩天皆為假日 : 可轉補休或轉薪資
                        Try
                            ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉薪資"))).Enabled = True
                            ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉補休"))).Enabled = True
                            'ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉補休"))).Selected = True
                        Catch ex As Exception
                            'Debug.Print("ddlSalaryOrAdjust指向問題" + ex.Message)
                        End Try
                    Else
                        'RankID小於19且除了兩天皆為假日以外 : 只能轉薪資
                        ' ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉薪資"))).Selected = True
                        ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉薪資"))).Enabled = True
                        'ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉補休"))).Selected = False
                        ddlSalaryOrAdjust.Items(ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉補休"))).Enabled = False
                    End If
                End If
            End If
        End If
    End Sub

#End Region

#Region "拋轉"

    ''' <summary>
    ''' 現在我刪除舊有資料 
    ''' 同時我需要判別舊有資料是否為假日
    ''' 是假日就更新之前的資料 OTHoliday1
    ''' </summary>
    ''' <param name="DataTable"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function deleteOverTimeTable(ByVal DataTable As DataTable, ByRef data_Period As DataTable) As Boolean
        Dim Flag As Boolean = False
        Dim SQLString As StringBuilder = New StringBuilder
        Dim isCount As Boolean = False


        '跨日單會跑兩次
        For i = 0 To DataTable.Rows.Count - 1
            Dim Obj4_2 As New OV4_2
            Dim OTCompID As String = DataTable.Rows(i).Item("OTCompID")
            Dim OTEmpID As String = DataTable.Rows(i).Item("OTEmpID")
            Dim OTStartDate As String = DataTable.Rows(i).Item("OTStartDate")
            Dim OTStartTime As String = DataTable.Rows(i).Item("OTStartTime")
            Dim OTEndTime As String = DataTable.Rows(i).Item("OTEndTime")
            Dim isHoliday As Boolean = Obj4_2.CheckHolidayOrNot(OTStartDate)


            SQLString.AppendLine(" DELETE FROM [OverTime] ")
            SQLString.AppendLine("  WHERE  OTDate=" + Bsp.Utility.Quote(OTStartDate))
            SQLString.AppendLine(" and CompID=" + Bsp.Utility.Quote(OTCompID))
            SQLString.AppendLine(" and EmpID=" + Bsp.Utility.Quote(OTEmpID))
            SQLString.AppendLine(" and BeginTime=" + Bsp.Utility.Quote(OTStartTime))



            '如果是假日 就必須UPDATE Holiday1
            If isHoliday Then
                isCount = getNeedUPDateOldData(data_Period, OTCompID, OTEmpID, OTStartDate, OTStartTime, OTEndTime)
            End If
        Next

        '如果有舊資料在同一天 那就更新 舊資料[OTHoliday1]
        If isCount Then
            Dim checkHDate As String = ""
            Dim InsertCount As Integer = 0
            For i = 0 To data_Period.Rows.Count - 1
                Dim CompID As String = data_Period.Rows(i).Item("CompID")
                Dim EmpID As String = data_Period.Rows(i).Item("EmpID")
                Dim OTDate As String = data_Period.Rows(i).Item("Date")
                Dim Seq As String = data_Period.Rows(i).Item("Seq")

                Dim chkdaytype As String = data_Period.Rows(i).Item("HolidayOrNot").ToString
                Dim chktime3 As Double = data_Period.Rows(i).Item("TimeMin_acc").ToString
                Dim holidayTime As Double = 0.0

                If checkHDate <> OTDate And chktime3 <> 0 And chkdaytype = "F" Then
                    InsertCount = 0
                    checkHDate = OTDate
                    InsertCount = InsertCount + 1
                    holidayTime = getHolidayForUPOldOverTime(data_Period, CompID, EmpID, OTDate, InsertCount, chktime3, chkdaytype)
                ElseIf checkHDate = OTDate And chktime3 <> 0 Then
                    InsertCount = InsertCount + 1
                    holidayTime = getHolidayForUPOldOverTime(data_Period, CompID, EmpID, OTDate, InsertCount, chktime3, chkdaytype)
                End If

                SQLString.AppendLine(" Update [dbo].[OverTime]")
                SQLString.AppendLine(" SET [OTHoliday1] =" + Bsp.Utility.Quote(holidayTime))
                SQLString.AppendLine(" ,[LastChgComp] = " + Bsp.Utility.Quote(UserProfile.CompID))
                SQLString.AppendLine(" ,[LastChgID] = " + Bsp.Utility.Quote(UserProfile.UserID))
                SQLString.AppendLine(" ,[LastChgDate] = getDate() ")
                SQLString.AppendLine(" WHERE [CompID]=" + Bsp.Utility.Quote(CompID))
                SQLString.AppendLine(" and [EmpID]=" + Bsp.Utility.Quote(EmpID))
                SQLString.AppendLine(" and [OTDate]=" + Bsp.Utility.Quote(OTDate))
                SQLString.AppendLine(" and [Seq]=" + Bsp.Utility.Quote(Seq))

            Next
        End If

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Try
                Bsp.DB.ExecuteNonQuery(CommandType.Text, SQLString.ToString(), tran, "eHRMSDB")
                tran.Commit()
                Flag = True
            Catch ex As Exception
                tran.Rollback()
                Flag = False
                Throw
            Finally
                If tran IsNot Nothing Then tran.Dispose()
            End Try
        End Using

        If Flag Then

        End If


        Return Flag
    End Function

    ''' <summary>
    ''' 拋轉資料
    ''' </summary>
    ''' <param name="OTCompID"></param>
    ''' <param name="OTEmpID"></param>
    ''' <param name="OTTxnID"></param>
    ''' <remarks>
    ''' 先做個要存放時段的table
    ''' 然後取出申報db裡此單號的資料
    ''' 
    ''' </remarks>
    Public Sub doThrow(ByVal OTCompID As String, ByVal OTEmpID As String, ByVal OTTxnID As String)

        '變數宣告
        Dim strSQL_Declaration As New StringBuilder()
        Dim data_Declaration As New DataTable            '原生資料
        Dim data_Declaration_Count As Integer            '原生資料筆數
        Dim data_Period As DataTable = getTable_Period() '存放時段的Table
        Dim transEmpID As String = ""                    '用來檢查異動的員工編號
        Dim checkEmpID As String = ""                    '用來檢查舊資料的員工編號
        Dim checkDate As String = ""                     '用來檢查舊資料的日期
        Dim throwMsg As New StringBuilder
        Dim strSQL As StringBuilder = New StringBuilder()
        strSQL.Append("select * from OverTimeDeclaration ")
        strSQL.Append(" where OTCompID=" + Bsp.Utility.Quote(OTCompID) + "and OTEmpID=" + Bsp.Utility.Quote(OTEmpID) + "and OTTxnID=" + Bsp.Utility.Quote(OTTxnID))
        strSQL.Append(" order by OTEmpID,OTStartDate,OTStartTime")

        data_Declaration = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "AattendantDB").Tables(0)
        data_Declaration_Count = data_Declaration.Rows.Count
        '從申報檔取出每一筆，開始計算時段
        For i As Integer = 0 To data_Declaration.Rows.Count - 1 Step 1
            Dim compID As String = data_Declaration.Rows(i).Item("OTCompID").ToString
            Dim empID As String = data_Declaration.Rows(i).Item("OTEmpID").ToString
            Dim startDate As String = data_Declaration.Rows(i).Item("OTStartDate").ToString
            Dim endDate As String = data_Declaration.Rows(i).Item("OTEndDate").ToString
            Dim seq As String = data_Declaration.Rows(i).Item("OTSeq").ToString
            Dim startTime As String = data_Declaration.Rows(i).Item("OTStartTime").ToString.Insert(2, ":")
            Dim endTime As String = data_Declaration.Rows(i).Item("OTEndTime").ToString.Insert(2, ":")
            Dim salaryOrAdjust As String = data_Declaration.Rows(i).Item("SalaryOrAdjust").ToString
            Dim txnID As String = data_Declaration.Rows(i).Item("OTTxnID").ToString
            Dim seqNo As String = data_Declaration.Rows(i).Item("OTSeqNo").ToString
            Dim deptID As String = data_Declaration.Rows(i).Item("DeptID").ToString
            Dim organID As String = data_Declaration.Rows(i).Item("OrganID").ToString
            Dim deptName As String = data_Declaration.Rows(i).Item("DeptName").ToString
            Dim organName As String = data_Declaration.Rows(i).Item("OrganName").ToString
            Dim mealFlag As String = data_Declaration.Rows(i).Item("MealFlag").ToString
            Dim mealTime As Double = data_Declaration.Rows(i).Item("MealTime").ToString
            Dim overTimeFlag As String = data_Declaration.Rows(i).Item("ToOverTimeFlag").ToString
            Dim time3HR As Double = 0
            Dim holiDay As String = IIf(data_Declaration.Rows(i).Item("HolidayOrNot").ToString = "0", "T", "F")
            Dim isCount As Boolean = True '檢察使否有重複資料的Flag
            '先檢查舊資料(OverTime)，檢查是否有跨日過來的資料或是先前有的加班(已計薪)
            If checkDate <> startDate Then
                checkDate = startDate
                checkEmpID = empID
                isCount = checkOldData(data_Period, compID, empID, startDate, startTime, endTime)
            ElseIf checkDate = startDate Then
                If checkEmpID <> empID Then
                    checkEmpID = empID
                    isCount = checkOldData(data_Period, compID, empID, startDate, startTime, endTime)
                ElseIf checkEmpID = empID Then
                    Dim isExist As Boolean = False
                    If data_Period.Rows.Count > 0 Then
                        For g As Integer = 0 To data_Period.Rows.Count - 1 Step 1
                            Dim exComp As String = data_Period.Rows(g).Item("CompID").ToString
                            Dim exEmp As String = data_Period.Rows(g).Item("EmpID").ToString
                            Dim exDate As String = data_Period.Rows(g).Item("Date").ToString
                            Dim exStime As String = data_Period.Rows(g).Item("StartTime").ToString
                            Dim exEtime As String = data_Period.Rows(g).Item("EndTime").ToString
                            If exComp = compID And exEmp = empID And exDate = startDate And exStime = startTime And exEtime = endTime Then
                                isExist = True
                            End If
                        Next
                        If isExist Then
                            isCount = checkOldData(data_Period, compID, empID, startDate, startTime, endTime)
                        End If
                    Else
                        isCount = checkOldData(data_Period, compID, empID, startDate, startTime, endTime)
                    End If
                End If
            End If
            '計算時段
            If isCount Then
                getTime_Interval(data_Period, compID, empID, startDate, endDate, startTime, endTime, salaryOrAdjust, seq, txnID, seqNo, deptID, organID, deptName, organName, mealFlag, mealTime, overTimeFlag, time3HR, holiDay)
            End If

        Next
        If data_Period.Rows.Count > 0 Then
            InsertDB(data_Period, OTTxnID) '新增到資料庫
        End If

    End Sub

#Region "清理時段陣列"
    Public Function clearArray() As Array
        Dim timeArray(3) As Double
        timeArray(0) = 0
        timeArray(1) = 0
        timeArray(2) = 0
        Return timeArray
    End Function
#End Region

#Region "時段計算"

    Public Sub getTime_Interval(ByRef Table_Time As DataTable, ByVal compID As String, ByVal empID As String, ByVal SDate As String, ByVal EDate As String, ByVal STime As String, ByVal ETime As String, ByVal SalaryOrAdjust As String, ByVal seq As String, ByVal txnID As String, ByVal seqNo As String, ByVal deptID As String, ByVal organID As String, ByVal deptName As String, ByVal organName As String, ByVal mealFlag As String, ByVal mealTime As Double, ByVal overTimeFlag As String, ByVal time3HR As Double, ByVal dayType As String)
        Dim beforeDate_Acc As Double = 0        '前一天的累積時間，計算跨日使用
        Dim beforeDateType As String = ""       '前一天時否為營業日或假日
        Dim beforeDate As String = ""           '前一天的日期
        Dim beforeDateAcross As String = ""     '前一天的加班是否有跨日過來的單
        Dim nowDateAcross As String = ""        '當天是否是有跨日過來
        Dim nowAddTime As Double = 0            '當天加班時間
        Dim nowDate_Acc As Double = 0           '當天的累積時間
        Dim time_Period(3) As Double            '時段<一>、<二>、<三>
        Dim laveMealTime As Integer = 0         '剩餘的吃飯時間
        Dim time3_HDHR As Double = time3HR
        '跨日時段誤差
        Dim todayMin As Double = 0             '加班分鐘數(跨日需要用到)
        Dim beforeTimeM_Acc As Double = 0           '計算加班總分鐘(跨日需要用到)

        '先清空時段
        time_Period = clearArray()

        Dim isTodayAcross As Boolean = False     '判斷今天是否有跨日過來的單
        Dim isYsdayAcross As Boolean = False     '判斷昨天是否有跨日過來的單

        Try
            If Table_Time.Rows.Count > 0 Then

                '判斷現在計算的單號是不是跨日過來的單號
                If seqNo = "2" Then
                    '當天跨日且前一天沒有跨日的狀況
                    Dim tempTime1 As Double = 0
                    Dim tempTime2 As Double = 0
                    Dim tempTime3 As Double = 0
                    '當天跨日且前一天有跨日的狀況
                    Dim tempTimeAcc1 As Double = 0
                    Dim tempTimeAcc2 As Double = 0
                    Dim tempTimeAcc3 As Double = 0

                    For i As Integer = 0 To Table_Time.Rows.Count - 1 Step 1
                        If txnID = Table_Time.Rows(i).Item("TxnID").ToString And empID = Table_Time.Rows(i).Item("EmpID").ToString Then '一定只有一筆
                            beforeDate = Table_Time.Rows(i).Item("Date")
                            tempTime1 = Table_Time.Rows(i).Item("Time_one")
                            tempTime2 = Table_Time.Rows(i).Item("Time_two")
                            tempTime3 = Table_Time.Rows(i).Item("Time_three")
                            tempTimeAcc1 = Table_Time.Rows(i).Item("TimeAcc_one")
                            tempTimeAcc2 = Table_Time.Rows(i).Item("TimeAcc_two")
                            tempTimeAcc3 = Table_Time.Rows(i).Item("TimeAcc_three")
                            'laveMealTime = CInt(Table_Time.Rows(i).Item("LaveMealTime")) '剩餘吃飯時間
                            beforeDate_Acc = Table_Time.Rows(i).Item("Time_acc").ToString
                            beforeTimeM_Acc = Table_Time.Rows(i).Item("TodayMin").ToString
                            beforeDateType = Table_Time.Rows(i).Item("HolidayOrNot").ToString
                            beforeDateAcross = Table_Time.Rows(i).Item("DayAcross").ToString
                        End If
                    Next
                    If tempTime1 = 0 And tempTime2 = 0 And tempTime3 = 0 Then
                        tempTimeAcc1 = 0
                        tempTimeAcc2 = 0
                        tempTimeAcc3 = 0
                        beforeDate_Acc = 0
                    End If
                    '有跨日的單號
                    isTodayAcross = True
                    If beforeDateAcross = "T" Then
                        isYsdayAcross = True
                        time_Period(0) = tempTimeAcc1
                        time_Period(1) = tempTimeAcc2
                        time_Period(2) = tempTimeAcc3
                    Else
                        time_Period(0) = tempTime1
                        time_Period(1) = tempTime2
                        time_Period(2) = tempTime3
                    End If
                    'If laveMealTime <> 0 Then
                    '    mealFlag = "1"
                    '    mealTime = laveMealTime
                    'End If
                End If

                '判斷當天，現在加班時間之前有沒有其他加班單        '1.可能跑多次的問題 2.可能要累加時段
                If isTodayAcross <> True Then                      '3.跨日過來的一定是當天最早的單，最開始的一筆最開始的判斷就不會進來了
                    For i As Integer = 0 To Table_Time.Rows.Count - 1 Step 1
                        '可能有當天的加班單或是當天有的加班單但以計薪
                        If (SDate = Table_Time.Rows(i).Item("Date").ToString And STime >= Table_Time.Rows(i).Item("StartTime").ToString And empID = Table_Time.Rows(i).Item("EmpID") And "0" = Table_Time.Rows(i).Item("SalaryPaid")) Or (SDate = Table_Time.Rows(i).Item("Date").ToString And empID = Table_Time.Rows(i).Item("EmpID") And "1" = Table_Time.Rows(i).Item("SalaryPaid")) Then
                            If Table_Time.Rows(i).Item("DayAcross").ToString = "T" Then '有跨日
                                time_Period(0) = Table_Time.Rows(i).Item("TimeAcc_one")
                                time_Period(1) = Table_Time.Rows(i).Item("TimeAcc_two")
                                time_Period(2) = Table_Time.Rows(i).Item("TimeAcc_three")
                                nowDate_Acc = Table_Time.Rows(i).Item("Time_acc")
                                nowDateAcross = Table_Time.Rows(i).Item("DayAcross")
                                '有跨日的單號
                                isTodayAcross = True
                            Else    '條件判斷的最後一筆數據
                                time_Period(0) = Table_Time.Rows(i).Item("Time_one")
                                time_Period(1) = Table_Time.Rows(i).Item("Time_two")
                                time_Period(2) = Table_Time.Rows(i).Item("Time_three")
                                nowDate_Acc = Table_Time.Rows(i).Item("Time_acc")
                                nowDateAcross = Table_Time.Rows(i).Item("DayAcross")
                            End If
                        End If
                    Next
                End If
            End If
            '時間轉換時段 
            nowAddTime = DateDiff("n", STime, ETime)                  '時間差/分鐘
            If ETime = "23:59" Then                                   '假如是跨日單，需要加一分鐘回去
                nowAddTime = nowAddTime + 1
            End If
            If mealFlag = "1" And mealTime <> 0 Then                  'mealFlag為1代表要扣除用餐時間

                nowAddTime = nowAddTime - mealTime
                'ElseIf nowAddTime < mealTime Then                     '當吃飯時間超過第一張單的時間(跨日會有的狀況)
                '    laveMealTime = mealTime - nowAddTime
                '    nowAddTime = 0
            End If

            todayMin = nowAddTime                                    '加班單分鐘

            '最後換算成小時
            nowAddTime = CDbl(FormatNumber((nowAddTime / 60), 1))             '加班幾分鐘/60分 = 小時
            '判斷************************************************
            If seqNo = "2" Then
                Dim result_Count As Double = 0
                Dim totalTime As Double = todayMin + beforeTimeM_Acc
                Dim beforeTimeM As Double = 0
                totalTime = CDbl(FormatNumber((totalTime / 60), 1))
                beforeTimeM = CDbl(FormatNumber((beforeTimeM_Acc / 60), 1))
                result_Count = totalTime - beforeTimeM
                result_Count = CDbl(FormatNumber(result_Count, 1))
                If result_Count <> nowAddTime Then
                    nowAddTime = result_Count
                End If
            End If

            If dayType = "T" Then '營業日
                If nowDate_Acc = 0 And isYsdayAcross <> True Then
                    If nowAddTime < 2 And seqNo = "1" And isTodayAcross <> True Then
                        time_Period = clearArray()
                        time_Period(0) = nowAddTime
                    ElseIf nowAddTime >= 2 And seqNo = "1" And isTodayAcross <> True Then
                        time_Period = clearArray()
                        time_Period(0) = 2
                        time_Period(1) = nowAddTime - 2
                    ElseIf seqNo = "2" And beforeDateType = "T" And isTodayAcross Then '平日跨日且前一天沒有跨日過來的的加班單的情況
                        If (beforeDate_Acc + nowAddTime) < 2 Then
                            time_Period = clearArray()
                            time_Period(0) = nowAddTime
                        ElseIf (beforeDate_Acc + nowAddTime) >= 2 And (beforeDate_Acc + nowAddTime) <= 4 Then
                            If beforeDate_Acc <= 2 Then
                                time_Period = clearArray()
                                time_Period(0) = 2 - beforeDate_Acc
                                time_Period(1) = (beforeDate_Acc + nowAddTime) - 2
                            ElseIf beforeDate_Acc > 2 Then
                                time_Period = clearArray()
                                time_Period(1) = nowAddTime
                            End If
                        ElseIf beforeDate_Acc + nowAddTime > 4 Then
                            If (beforeDate_Acc - time_Period(1)) < 2 Then
                                time_Period = clearArray()
                                time_Period(0) = nowAddTime - 2
                                time_Period(1) = 2
                            ElseIf (beforeDate_Acc - time_Period(1)) >= 2 Then
                                If nowAddTime < 2 Then
                                    time_Period = clearArray()
                                    time_Period(1) = nowAddTime
                                ElseIf nowAddTime >= 2 Then
                                    time_Period = clearArray()
                                    time_Period(0) = nowAddTime - 2
                                    time_Period(1) = 2
                                End If
                            End If
                        End If
                    ElseIf seqNo = "2" And beforeDateType = "F" And isTodayAcross Then '假日跨日且前一天沒有跨日過來的的加班單的情況
                        If beforeDate_Acc < 2 Then
                            If beforeDate_Acc + nowAddTime < 2 Then
                                time_Period = clearArray()
                                time_Period(0) = nowAddTime
                            ElseIf beforeDate_Acc + nowAddTime >= 2 And beforeDate_Acc + nowAddTime <= 4 Then
                                If beforeDate_Acc <= 2 Then
                                    time_Period = clearArray()
                                    time_Period(0) = 2 - beforeDate_Acc
                                    time_Period(1) = (beforeDate_Acc + nowAddTime) - 2
                                ElseIf beforeDate_Acc > 2 Then
                                    time_Period = clearArray()
                                    time_Period(1) = nowAddTime
                                End If
                            ElseIf beforeDate_Acc + nowAddTime > 4 Then
                                time_Period = clearArray()
                                time_Period(0) = nowAddTime - 2
                                time_Period(1) = 2
                            End If
                        ElseIf beforeDate_Acc >= 2 Then
                            If nowAddTime < 2 Then
                                time_Period = clearArray()
                                time_Period(1) = nowAddTime
                            ElseIf nowAddTime >= 2 Then
                                time_Period = clearArray()
                                time_Period(0) = nowAddTime - 2
                                time_Period(1) = 2
                            End If
                        End If
                    End If
                ElseIf nowDate_Acc <> 0 And nowDate_Acc <= 2 And isTodayAcross <> True Then
                    If nowDate_Acc + nowAddTime < 2 Then
                        time_Period = clearArray()
                        time_Period(0) = nowAddTime
                    ElseIf nowDate_Acc + nowAddTime >= 2 And nowDate_Acc + nowAddTime <= 4 Then
                        time_Period = clearArray()
                        time_Period(0) = 2 - nowDate_Acc
                        time_Period(1) = (nowDate_Acc + nowAddTime) - 2
                    End If
                ElseIf nowDate_Acc > 2 And isTodayAcross <> True Then
                    time_Period = clearArray()
                    time_Period(1) = nowAddTime
                End If

                '有跨日過來的計算
                If (isTodayAcross And seqNo = "1") Or (isYsdayAcross And seqNo = "2") Then
                    If isYsdayAcross And seqNo = "2" Then
                        nowDate_Acc = beforeDate_Acc
                    End If

                    If (time_Period(0) <> 0 And time_Period(1) = 0) Or (time_Period(0) <> 0 And time_Period(1) <> 0) Then
                        Dim time1_Acc As Double = nowDate_Acc - time_Period(1)
                        If time1_Acc + nowAddTime < 2 Then
                            time_Period = clearArray()
                            time_Period(0) = nowAddTime
                        ElseIf time1_Acc + nowAddTime >= 2 And time1_Acc + nowAddTime <= 4 Then
                            time_Period = clearArray()
                            time_Period(0) = 2 - time1_Acc
                            time_Period(1) = (nowAddTime + time1_Acc) - 2
                        ElseIf time1_Acc + nowAddTime > 4 Then
                            time_Period = clearArray()
                            time_Period(0) = nowAddTime - 2
                            time_Period(1) = 2
                        End If
                    ElseIf time_Period(0) = 0 And time_Period(1) <> 0 Then
                        If nowAddTime < 2 Then
                            time_Period = clearArray()
                            time_Period(0) = nowAddTime
                        ElseIf nowAddTime >= 2 Then
                            time_Period = clearArray()
                            time_Period(0) = 2
                            time_Period(1) = nowAddTime - 2
                        End If
                    ElseIf time_Period(0) = 0 And time_Period(1) = 0 And time_Period(2) <> 0 Then
                        If nowDate_Acc < 2 Then
                            If nowDate_Acc + nowAddTime < 2 Then
                                time_Period = clearArray()
                                time_Period(0) = nowAddTime
                            ElseIf nowDate_Acc + nowAddTime >= 2 And nowDate_Acc + nowAddTime <= 4 Then
                                time_Period = clearArray()
                                time_Period(0) = 2 - nowDate_Acc
                                time_Period(1) = (nowDate_Acc + nowAddTime) - 2
                            ElseIf nowDate_Acc + nowAddTime > 4 Then
                                time_Period = clearArray()
                                time_Period(0) = nowAddTime - 2
                                time_Period(1) = 2
                            End If
                        ElseIf nowDate_Acc >= 2 Then
                            If nowAddTime < 2 Then
                                time_Period = clearArray()
                                time_Period(1) = nowAddTime
                            ElseIf nowAddTime >= 2 Then
                                time_Period = clearArray()
                                time_Period(0) = nowAddTime - 2
                                time_Period(1) = 2
                            End If
                        End If
                    ElseIf time_Period(0) = 0 And time_Period(1) = 0 And time_Period(2) = 0 Then
                        If nowDate_Acc = 0 And isYsdayAcross <> True Then
                            If nowAddTime < 2 Then
                                time_Period = clearArray()
                                time_Period(0) = nowAddTime
                            ElseIf nowAddTime >= 2 Then
                                time_Period = clearArray()
                                time_Period(0) = 2
                                time_Period(1) = nowAddTime - 2
                            End If
                        End If
                    End If
                    '要把跨日第一筆的累積清空
                    If isYsdayAcross And seqNo = "2" Then
                        nowDate_Acc = 0
                    End If

                End If

            ElseIf dayType = "F" Then '休假日
                time_Period = clearArray()
                time_Period(2) = nowAddTime
            End If

            '最後寫回Table
            Dim dataRows As DataRow
            dataRows = Table_Time.NewRow
            dataRows("CompID") = compID
            dataRows("TxnID") = txnID
            dataRows("Seq") = seq
            dataRows("SeqNo") = seqNo
            dataRows("EmpID") = empID
            dataRows("StartTime") = STime
            dataRows("EndTime") = ETime
            dataRows("SalaryOrAdjust") = SalaryOrAdjust
            dataRows("MealFlag") = IIf(mealFlag = "1", "Y", "N")
            dataRows("MealTime") = mealTime
            'dataRows("LaveMealTime") = laveMealTime.ToString
            dataRows("Date") = SDate
            dataRows("Time_one") = time_Period(0)
            dataRows("Time_two") = time_Period(1)
            dataRows("Time_three") = time_Period(2)
            dataRows("Time_threeH") = time3_HDHR
            If Table_Time.Rows.Count > 0 Then     ' 第二筆開始累加當日每一筆時段
                Dim tempTime1 As Double = 0
                Dim tempTime2 As Double = 0
                Dim tempTime3 As Double = 0
                Dim tempMin As Double = 0
                For i As Integer = 0 To Table_Time.Rows.Count - 1
                    If SDate = Table_Time.Rows(i).Item("Date").ToString And empID = Table_Time.Rows(i).Item("EmpID") Then    '累加同一天的時段
                        tempTime1 = Table_Time.Rows(i).Item("TimeAcc_one")
                        tempTime2 = Table_Time.Rows(i).Item("TimeAcc_two")
                        tempTime3 = Table_Time.Rows(i).Item("TimeAcc_three")
                        tempMin = Table_Time.Rows(i).Item("TimeMin_acc")
                    End If
                Next
                If tempTime1 <> 0 Or tempTime2 <> 0 Or tempTime3 <> 0 Then
                    dataRows("TimeAcc_one") = time_Period(0) + tempTime1
                    dataRows("TimeAcc_two") = time_Period(1) + tempTime2
                    dataRows("TimeAcc_three") = time_Period(2) + tempTime3
                    dataRows("TimeMin_acc") = todayMin + tempMin
                Else
                    dataRows("TimeAcc_one") = time_Period(0)
                    dataRows("TimeAcc_two") = time_Period(1)
                    dataRows("TimeAcc_three") = time_Period(2)
                    dataRows("TimeMin_acc") = todayMin
                End If
            ElseIf Table_Time.Rows.Count = 0 Then ' 第一筆累加就是第一筆時段的數據
                dataRows("TimeAcc_one") = time_Period(0)
                dataRows("TimeAcc_two") = time_Period(1)
                dataRows("TimeAcc_three") = time_Period(2)
                dataRows("TimeMin_acc") = todayMin
            End If
            dataRows("Time_acc") = nowDate_Acc + time_Period(0) + time_Period(1) + time_Period(2)
            dataRows("TodayMin") = todayMin
            '有問題為何 SalaryPaid=overTimeFlag ps:這邊用來替代 overTimeFlag 請幫它當作overTimeFlag
            dataRows("SalaryPaid") = overTimeFlag
            'dataRows("SalaryPaid") = "0"

            dataRows("HolidayOrNot") = dayType
            dataRows("DayAcross") = IIf(isTodayAcross, "T", "F")
            dataRows("DeptID") = deptID
            dataRows("OrganID") = organID
            dataRows("DeptName") = deptName
            dataRows("OrganName") = organName
            Table_Time.Rows.Add(dataRows)

        Catch ex As Exception
            Bsp.Utility.ShowFormatMessage(Me, "Error", ex.Message)
        End Try


    End Sub
#End Region

#Region "虛擬Table-For 查詢條件(日期、人員)使用"
    Public Function getTable_Period() As DataTable
        Dim myTable As New DataTable
        Dim col As DataColumn
        '公司代碼
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "CompID"
        myTable.Columns.Add(col)
        '單號
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "TxnID"
        myTable.Columns.Add(col)
        '序號
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "Seq"
        myTable.Columns.Add(col)
        '單號序列
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "SeqNo"
        myTable.Columns.Add(col)
        '員工編號
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "EmpID"
        myTable.Columns.Add(col)
        '加班開始時間
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "StartTime"
        myTable.Columns.Add(col)
        '加班結束時間
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "EndTime"
        myTable.Columns.Add(col)
        '用餐註記
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "MealFlag"
        myTable.Columns.Add(col)
        '用餐時間
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "MealTime"
        myTable.Columns.Add(col)
        ''剩餘用餐時間
        'col = New DataColumn
        'col.DataType = System.Type.GetType("System.String")
        'col.ColumnName = "LaveMealTime"
        'myTable.Columns.Add(col)
        '日期=>開始跟結束現在都一樣
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "Date"
        myTable.Columns.Add(col)
        '時段<一>
        col = New DataColumn
        col.DataType = System.Type.GetType("System.Double")
        col.ColumnName = "Time_one"
        myTable.Columns.Add(col)
        '時段<一>累加
        col = New DataColumn
        col.DataType = System.Type.GetType("System.Double")
        col.ColumnName = "TimeAcc_one"
        myTable.Columns.Add(col)
        '時段<二>
        col = New DataColumn
        col.DataType = System.Type.GetType("System.Double")
        col.ColumnName = "Time_two"
        myTable.Columns.Add(col)
        '時段<二>累加
        col = New DataColumn
        col.DataType = System.Type.GetType("System.Double")
        col.ColumnName = "TimeAcc_two"
        myTable.Columns.Add(col)
        '時段<三>
        col = New DataColumn
        col.DataType = System.Type.GetType("System.Double")
        col.ColumnName = "Time_three"
        myTable.Columns.Add(col)
        '時段<三>累加
        col = New DataColumn
        col.DataType = System.Type.GetType("System.Double")
        col.ColumnName = "TimeAcc_three"
        myTable.Columns.Add(col)
        '時段<三>累加_抓OverTime資料用
        col = New DataColumn
        col.DataType = System.Type.GetType("System.Double")
        col.ColumnName = "Time_threeH"
        myTable.Columns.Add(col)
        '累積時間(時段)
        col = New DataColumn
        col.DataType = System.Type.GetType("System.Double")
        col.ColumnName = "Time_acc"
        myTable.Columns.Add(col)
        '累積時間(分鐘)
        col = New DataColumn
        col.DataType = System.Type.GetType("System.Double")
        col.ColumnName = "TimeMin_acc"
        myTable.Columns.Add(col)
        '前一天加班(分鐘)
        col = New DataColumn
        col.DataType = System.Type.GetType("System.Double")
        col.ColumnName = "TodayMin"
        myTable.Columns.Add(col)
        '轉薪資或轉補休註記
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "SalaryOrAdjust"
        myTable.Columns.Add(col)
        '記薪註記
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "SalaryPaid"
        myTable.Columns.Add(col)
        '假日註記
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "HolidayOrNot"
        myTable.Columns.Add(col)
        '跨日註記
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "DayAcross"
        myTable.Columns.Add(col)
        '部門代號
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "DeptID"
        myTable.Columns.Add(col)
        '科組課代號
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "OrganID"
        myTable.Columns.Add(col)
        '部門名稱
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "DeptName"
        myTable.Columns.Add(col)
        '科組課名稱
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "OrganName"
        myTable.Columns.Add(col)

        Return myTable
    End Function
#End Region

#Region "虛擬Table-For 查詢條件(人員查詢)使用"
    Public Function getTable_Emp() As DataTable
        Dim myTable As New DataTable
        Dim col As DataColumn
        '公司代碼
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "CompID"
        myTable.Columns.Add(col)
        '單號
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "TxnID"
        myTable.Columns.Add(col)
        '單號序列
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "SeqNo"
        myTable.Columns.Add(col)
        '部門名稱
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "DeptName"
        myTable.Columns.Add(col)
        '科組課名稱
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "OrganName"
        myTable.Columns.Add(col)
        '員工編號
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "EmpID"
        myTable.Columns.Add(col)
        '員工姓名
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "EmpName"
        myTable.Columns.Add(col)
        '加班日期
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "Date"
        myTable.Columns.Add(col)
        '加班時間起訖日
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "Time"
        myTable.Columns.Add(col)
        '加班類型
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "Reason"
        myTable.Columns.Add(col)
        '加班開始時間
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "StartTime"
        myTable.Columns.Add(col)
        '加班結束時間
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "EndTime"
        myTable.Columns.Add(col)

        Return myTable
    End Function
#End Region

#Region "取出申報檔(For 拋轉日期)資料"

    Public Function getOriData(ByVal ucDate As String, ByVal empID As String, ByVal isTrans As Boolean) As DataTable
        Dim dataTable As DataTable
        Dim strSQL As New StringBuilder
        Dim CompID As String = UserProfile.SelectCompRoleID

        '資料要排序 注意!
        '公司代碼-OTCompID、加班申請人-OTEmpID、加班開始日期-OTStartDate、加班結束日期-OTEndDate、加班單序號-OTSeq、加班開始時間-OTStartTime、加班結束時間-OTEndTime、加班單號-OTxnID、單號序列-OTSeqNo、
        '部門代號-DeptID、科組課代號-OrganID、部門名稱-DeptName、科組課名稱-OrganName、用餐扣除註記-MealFlag、用餐時間-MealTime、申報單狀態-OTStatus、計薪註記-OTSalaryPaid、假日註記-HolidayOrNot
        strSQL.Append("select OTCompID,OTEmpID,OTStartDate,OTEndDate,OTSeq,OTStartTime,OTEndTime,SalaryOrAdjust,OTTxnID,OTSeqNo,DeptID,OrganID,DeptName,OrganName,MealFlag,MealTime,ToOverTimeFlag,OTStatus,OTSalaryPaid,HolidayOrNot from OverTimeDeclaration ")
        strSQL.Append("Where OTStatus = '3' ")
        strSQL.Append(" And SalaryOrAdjust = '1'")
        strSQL.Append(" And OTCompID = " & Bsp.Utility.Quote(CompID))
        strSQL.Append(" And OverTimeFlag = '1' ")
        strSQL.Append(" And ToOverTimeFlag = '0'")
        If ucDate <> "" And empID = "" And isTrans <> True Then
            If ucDate <> "" Then '拋轉日期
                strSQL.Append(" And OTValidDate <=" & Bsp.Utility.Quote(ucDate & " 23:59:59"))
            End If
        ElseIf isTrans Then      '人員異動查詢
            strSQL.Append(" And OTValidDate <=" & Bsp.Utility.Quote(ucDate & " 23:59:59"))
            strSQL.Append(" And OTEmpID=" & Bsp.Utility.Quote(empID))
        End If
        strSQL.Append(" order by OTEmpID,OTStartDate,OTStartTime")

        dataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "AattendantDB").Tables(0)

        Return dataTable
    End Function

#End Region

#Region "取出申報檔(For 拋轉人員)資料"
    Public Function getOriEmpData(ByVal ucDate As String, ByVal empID As String) As DataTable
        Dim dataTable As DataTable
        Dim strSQL As New StringBuilder
        Dim CompID As String = UserProfile.SelectCompRoleID

        '資料要排序 注意!
        '公司代碼-OTCompID、加班申請人-OTEmpID、加班開始日期-OTStartDate、加班結束日期-OTEndDate、加班單序號-OTSeq、加班開始時間-OTStartTime、加班結束時間-OTEndTime、加班單號-OTxnID、單號序列-OTSeqNo、
        '部門代號-DeptID、科組課代號-OrganID、部門名稱-DeptName、科組課名稱-OrganName、用餐扣除註記-MealFlag、用餐時間-MealTime、申報單狀態-OTStatus、計薪註記-OTSalaryPaid、假日註記-HolidayOrNot
        strSQL.Append("select N.OTCompID,N.OTEmpID,N.OTStartDate,N.OTEndDate,N.OTSeq,N.OTStartTime,N.OTEndTime,N.SalaryOrAdjust,N.OTTxnID,N.OTSeqNo,N.DeptID,N.OrganID,N.DeptName,N.OrganName,N.MealFlag,N.MealTime,N.ToOverTimeFlag,MAP.CodeCName as OTReasonMemo,N.OTStatus,N.OTSalaryPaid,N.HolidayOrNot,P.NameN ")
        strSQL.Append("from OverTimeDeclaration N join " & eHRMSDB_ITRD & ".[dbo].[Personal] P On N.OTCompID = P.CompID and N.OTEmpID = P.EmpID ")
        strSQL.Append("left join AT_CodeMap MAP ON MAP.TabName='OverTime' And MAP.FldName = 'OverTimeType' And  MAP.Code=N.OTTypeID ")
        strSQL.Append("Where N.OTStatus = '3'")
        strSQL.Append(" And N.SalaryOrAdjust = '1'")
        strSQL.Append(" And N.OTCompID = " & Bsp.Utility.Quote(CompID))
        strSQL.Append(" And N.OverTimeFlag = '1' ")
        strSQL.Append(" And N.ToOverTimeFlag = '0'")
        strSQL.Append(" And N.OTEmpID=" & Bsp.Utility.Quote(empID))
        If ucDate <> "" Then
            strSQL.Append(" And N.OTValidDate <=" & Bsp.Utility.Quote(ucDate & " 23:59:59"))
        End If
        strSQL.Append(" order by N.OTEmpID,N.OTStartDate,N.OTStartTime")

        dataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "AattendantDB").Tables(0)


        Return dataTable
    End Function
#End Region
#Region "取出申報黨所有拋轉過資料"
    Public Function getOriAllData() As DataTable
        Dim dataTable As DataTable
        Dim strSQL As New StringBuilder
        Dim CompID As String = UserProfile.SelectCompRoleID

        '資料要排序 注意!
        '公司代碼-OTCompID、加班申請人-OTEmpID、加班開始日期-OTStartDate、加班結束日期-OTEndDate、加班單序號-OTSeq、加班開始時間-OTStartTime、加班結束時間-OTEndTime、加班單號-OTxnID、單號序列-OTSeqNo、
        '部門代號-DeptID、科組課代號-OrganID、部門名稱-DeptName、科組課名稱-OrganName、用餐扣除註記-MealFlag、用餐時間-MealTime、申報單狀態-OTStatus、計薪註記-OTSalaryPaid、假日註記-HolidayOrNot
        strSQL.Append("select OTD.OTCompID,OTD.OTEmpID,OTD.OTStartDate,OTD.OTEndDate,OTD.OTSeq,OTD.OTStartTime,OTD.OTEndTime,OTD.SalaryOrAdjust,OTD.OTTxnID,OTD.OTSeqNo,OTD.DeptID,OTD.OrganID,OTD.DeptName,OTD.OrganName,OTD.MealFlag,OTD.MealTime,OTD.ToOverTimeFlag,OTD.OTStatus,OTD.OTSalaryPaid,OTD.HolidayOrNot,OV.OTHoliday1 from OverTimeDeclaration OTD")
        strSQL.Append(" Left join " & eHRMSDB_ITRD & ".[dbo].[OverTime] OV On OTD.OTCompID = OV.CompID and OTD.OTEmpID = OV.EmpID and OTD.OTStartDate = OV.OTDate and OTD.OTSeq = OV.Seq and OTD.OTStartTime = OV.BeginTime and OTD.OTEndTime = OV.EndTime")
        strSQL.Append(" Where OTD.OTStatus = '3' ")
        strSQL.Append(" And OTD.SalaryOrAdjust = '1'")
        strSQL.Append(" And OTD.OTCompID = " & Bsp.Utility.Quote(CompID))
        strSQL.Append(" And OTD.OverTimeFlag = '1' ")
        strSQL.Append(" And OTD.ToOverTimeFlag = '1'")
        strSQL.Append(" order by OTD.OTEmpID,OTD.OTStartDate,OTD.OTStartTime")

        dataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "AattendantDB").Tables(0)

        Return dataTable
    End Function
#End Region

#Region "檢查OverTime有無跨月資料"
    Public Function checkOldData(ByRef Table_Time As DataTable, ByVal compID As String, ByVal empID As String, ByVal sDate As String, ByVal bTime As String, ByVal aTime As String) As Boolean
        Dim result As Boolean = True
        Dim checkData As DataTable
        Dim checkData_Count As Integer
        Dim strSQL_Check As New StringBuilder()


        strSQL_Check.Append("select Seq,BeginTime,EndTime,OTNormal,OTOver,OTHoliday,OTHoliday1,HolidayOrNot,DeptID,OrganID,DeptName,OrganName from OverTime")
        strSQL_Check.Append(" where 1=1")
        strSQL_Check.Append(" And CompID =" & Bsp.Utility.Quote(compID))
        strSQL_Check.Append(" And EmpID =" & Bsp.Utility.Quote(empID))
        strSQL_Check.Append(" And OTDate =" & Bsp.Utility.Quote(sDate))
        strSQL_Check.Append(" order by BeginTime")
        checkData = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL_Check.ToString(), "eHRMSDB").Tables(0)

        checkData_Count = checkData.Rows.Count


        For i As Integer = 0 To checkData_Count - 1 Step 1
            Dim Seq As String = checkData.Rows(i).Item("Seq").ToString
            Dim STime As String = checkData.Rows(i).Item("BeginTime").ToString
            Dim ETime As String = checkData.Rows(i).Item("EndTime").ToString
            Dim dayType As String = checkData.Rows(i).Item("HolidayOrNot").ToString
            Dim deptID As String = checkData.Rows(i).Item("DeptID").ToString
            Dim organID As String = checkData.Rows(i).Item("OrganID").ToString
            Dim deptName As String = checkData.Rows(i).Item("DeptName").ToString
            Dim organName As String = checkData.Rows(i).Item("OrganName").ToString

            Dim oriAllthrow As DataTable = getOriAllData()

            Dim newRowData As DataRow = (From column In oriAllthrow.Rows _
            Where column("OTCompID") = compID _
            And column("OTEmpID") = empID _
            And column("OTStartDate") = sDate _
            And column("OTStartTime") = STime _
            And column("OTEndTime") = ETime _
            Select column).FirstOrDefault()

            Dim ThrowcompID As String = newRowData.Item("OTCompID").ToString
            Dim ThrowempID As String = newRowData.Item("OTEmpID").ToString
            Dim ThrowstartDate As String = newRowData.Item("OTStartDate").ToString
            Dim ThrowendDate As String = newRowData.Item("OTEndDate").ToString
            Dim Throwseq As String = newRowData.Item("OTSeq").ToString
            Dim ThrowstartTime As String = newRowData.Item("OTStartTime").ToString.Insert(2, ":")
            Dim ThrowendTime As String = newRowData.Item("OTEndTime").ToString.Insert(2, ":")
            Dim ThrowsalaryOrAdjust As String = newRowData.Item("SalaryOrAdjust").ToString
            Dim ThrowtxnID As String = newRowData.Item("OTTxnID").ToString
            Dim ThrowseqNo As String = newRowData.Item("OTSeqNo").ToString
            Dim ThrowdeptID As String = newRowData.Item("DeptID").ToString
            Dim ThroworganID As String = newRowData.Item("OrganID").ToString
            Dim ThrowdeptName As String = newRowData.Item("DeptName").ToString
            Dim ThroworganName As String = newRowData.Item("OrganName").ToString
            Dim ThrowmealFlag As String = newRowData.Item("MealFlag").ToString
            Dim ThrowmealTime As Double = newRowData.Item("MealTime").ToString
            Dim ThrowoverTimeFlag As String = newRowData.Item("ToOverTimeFlag").ToString

            Dim Throwtime3HR As Double = 0.0
            If Not "".Equals(newRowData.Item("OTHoliday1").ToString.Trim) Then
                Throwtime3HR = CDbl(newRowData.Item("OTHoliday1").ToString)
            End If

            Dim ThrowholiDay As String = IIf(newRowData.Item("HolidayOrNot").ToString = "0", "T", "F")

            getTime_Interval(Table_Time, ThrowcompID, ThrowempID, ThrowstartDate, ThrowendDate, ThrowstartTime, ThrowendTime, ThrowsalaryOrAdjust, Throwseq, ThrowtxnID, ThrowseqNo, ThrowdeptID, ThroworganID, ThrowdeptName, ThroworganName, ThrowmealFlag, ThrowmealTime, ThrowoverTimeFlag, Throwtime3HR, ThrowholiDay)
        Next



        Return result
    End Function



    Public Function getNeedUPDateOldData(ByRef Table_Time As DataTable, ByVal compID As String, ByVal empID As String, ByVal sDate As String, ByVal bTime As String, ByVal aTime As String) As Boolean
        Dim result As Boolean = True
        Dim checkData As DataTable
        Dim checkData_Count As Integer
        Dim strSQL_Check As New StringBuilder()


        strSQL_Check.Append("select Seq,BeginTime,EndTime,OTNormal,OTOver,OTHoliday,OTHoliday1,HolidayOrNot,DeptID,OrganID,DeptName,OrganName from OverTime")
        strSQL_Check.Append(" where 1=1")
        strSQL_Check.Append(" And CompID =" & Bsp.Utility.Quote(compID))
        strSQL_Check.Append(" And EmpID =" & Bsp.Utility.Quote(empID))
        strSQL_Check.Append(" And OTDate =" & Bsp.Utility.Quote(sDate))
        strSQL_Check.Append(" And not BeginTime =" & Bsp.Utility.Quote(bTime))
        '不加結束時間因為有跨日單

        strSQL_Check.Append(" order by BeginTime")
        checkData = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL_Check.ToString(), "eHRMSDB").Tables(0)

        checkData_Count = checkData.Rows.Count


        For i As Integer = 0 To checkData_Count - 1 Step 1
            Dim Seq As String = checkData.Rows(i).Item("Seq").ToString
            Dim STime As String = checkData.Rows(i).Item("BeginTime").ToString
            Dim ETime As String = checkData.Rows(i).Item("EndTime").ToString
            Dim dayType As String = checkData.Rows(i).Item("HolidayOrNot").ToString
            Dim deptID As String = checkData.Rows(i).Item("DeptID").ToString
            Dim organID As String = checkData.Rows(i).Item("OrganID").ToString
            Dim deptName As String = checkData.Rows(i).Item("DeptName").ToString
            Dim organName As String = checkData.Rows(i).Item("OrganName").ToString

            Dim oriAllthrow As DataTable = getOriAllData()

            Dim newRowData As DataRow = (From column In oriAllthrow.Rows _
            Where column("OTCompID") = compID _
            And column("OTEmpID") = empID _
            And column("OTStartDate") = sDate _
            And column("OTStartTime") = STime _
            And column("OTEndTime") = ETime _
            Select column).FirstOrDefault()

            Dim ThrowcompID As String = newRowData.Item("OTCompID").ToString
            Dim ThrowempID As String = newRowData.Item("OTEmpID").ToString
            Dim ThrowstartDate As String = newRowData.Item("OTStartDate").ToString
            Dim ThrowendDate As String = newRowData.Item("OTEndDate").ToString
            Dim Throwseq As String = newRowData.Item("OTSeq").ToString
            Dim ThrowstartTime As String = newRowData.Item("OTStartTime").ToString.Insert(2, ":")
            Dim ThrowendTime As String = newRowData.Item("OTEndTime").ToString.Insert(2, ":")
            Dim ThrowsalaryOrAdjust As String = newRowData.Item("SalaryOrAdjust").ToString
            Dim ThrowtxnID As String = newRowData.Item("OTTxnID").ToString
            Dim ThrowseqNo As String = newRowData.Item("OTSeqNo").ToString
            Dim ThrowdeptID As String = newRowData.Item("DeptID").ToString
            Dim ThroworganID As String = newRowData.Item("OrganID").ToString
            Dim ThrowdeptName As String = newRowData.Item("DeptName").ToString
            Dim ThroworganName As String = newRowData.Item("OrganName").ToString
            Dim ThrowmealFlag As String = newRowData.Item("MealFlag").ToString
            Dim ThrowmealTime As Double = newRowData.Item("MealTime").ToString
            Dim ThrowoverTimeFlag As String = newRowData.Item("ToOverTimeFlag").ToString

            Dim Throwtime3HR As Double = 0.0
            If Not "".Equals(newRowData.Item("OTHoliday1").ToString.Trim) Then
                Throwtime3HR = CDbl(newRowData.Item("OTHoliday1").ToString)
            End If

            Dim ThrowholiDay As String = IIf(newRowData.Item("HolidayOrNot").ToString = "0", "T", "F")

            getTime_Interval(Table_Time, ThrowcompID, ThrowempID, ThrowstartDate, ThrowendDate, ThrowstartTime, ThrowendTime, ThrowsalaryOrAdjust, Throwseq, ThrowtxnID, ThrowseqNo, ThrowdeptID, ThroworganID, ThrowdeptName, ThroworganName, ThrowmealFlag, ThrowmealTime, ThrowoverTimeFlag, Throwtime3HR, ThrowholiDay)
        Next



        Return result
    End Function
#End Region

#Region "假日加班 4、8、12小時計算 注意已跟4300不同"
    Public Function getHoliday(ByVal dataTable As DataTable, ByVal chkcompID As String, ByVal chkempID As String, ByVal chkDate As String, ByVal count As Integer, ByVal HTime As Double, ByVal chkDaytype As String, ByVal chkTxnID As String) As Double
        Dim result As Double = 0.0
        Dim oricount As Integer = 0
        Dim isThrow As Boolean = False
        Dim ovTimeCompID As String = ""
        Dim ovTimeEmpID As String = ""
        Dim ovTimeDate As String = ""
        Dim ovTimeHMin As Double = 0
        Dim ovTimeH As Double = 0

        For i As Integer = 0 To dataTable.Rows.Count - 1 Step 1
            Dim oricomp As String = dataTable.Rows(i).Item("CompID").ToString
            Dim oriemp As String = dataTable.Rows(i).Item("EmpID").ToString
            Dim oridate As String = dataTable.Rows(i).Item("Date").ToString
            Dim oritxnID As String = dataTable.Rows(i).Item("TxnID").ToString
            Dim oridaytype As String = dataTable.Rows(i).Item("HolidayOrNot").ToString

            'Dim oripaid As String = dataTable.Rows(i).Item("SalaryPaid").ToString
            If oricomp = chkcompID And oriemp = chkempID And oridate = chkDate And oridaytype = "F" And oritxnID.Equals(chkTxnID) Then
                oricount = oricount + 1
                ovTimeCompID = oricomp
                ovTimeEmpID = oriemp
                ovTimeDate = oridate
            End If
        Next

        If oricount = count Then
            For j As Integer = 0 To dataTable.Rows.Count - 1 Step 1
                Dim tempCompID As String = dataTable.Rows(j).Item("CompID").ToString
                Dim tempEmpID As String = dataTable.Rows(j).Item("EmpID").ToString
                Dim tempDate As String = dataTable.Rows(j).Item("Date").ToString
                Dim tempTimeH As Double = IIf(dataTable.Rows(j).Item("Time_threeH").ToString Is Nothing, 0, CDbl(dataTable.Rows(j).Item("Time_threeH").ToString))
                Dim tempSalary As String = dataTable.Rows(j).Item("SalaryPaid").ToString
                If tempCompID = ovTimeCompID And tempEmpID = ovTimeEmpID And tempDate = ovTimeDate And tempSalary = "1" Then
                    ovTimeH = ovTimeH + tempTimeH
                End If
            Next

            ovTimeH = ovTimeH * 60
            If ovTimeH <> 0 Then
                If HTime <= ovTimeH Then
                    result = 0.0
                ElseIf HTime > ovTimeH Then
                    If HTime <= 240 Then
                        result = 240 - ovTimeH
                    ElseIf HTime > 240 And HTime <= 480 Then
                        result = 480 - ovTimeH
                    ElseIf HTime > 480 And HTime <= 720 Then
                        result = 720 - ovTimeH
                    ElseIf HTime > 720 Then
                        result = HTime
                    End If
                End If
            Else
                If HTime <= 240 Then
                    result = 240
                ElseIf HTime > 240 And HTime <= 480 Then
                    result = 480
                ElseIf HTime > 480 And HTime <= 720 Then
                    result = 720
                ElseIf HTime > 720 Then
                    result = HTime
                End If
            End If

        Else
            result = 0.0
        End If

        result = CDbl(FormatNumber((result / 60), 1))
        Return result
    End Function


    Public Function getHolidayForUPOldOverTime(ByVal dataTable As DataTable, ByVal chkcompID As String, ByVal chkempID As String, ByVal chkDate As String, ByVal count As Integer, ByVal HTime As Double, ByVal chkDaytype As String) As Double
        Dim result As Double = 0.0
        Dim oricount As Integer = 0
        Dim isThrow As Boolean = False
        Dim ovTimeCompID As String = ""
        Dim ovTimeEmpID As String = ""
        Dim ovTimeDate As String = ""
        Dim ovTimeHMin As Double = 0
        Dim ovTimeH As Double = 0

        For i As Integer = 0 To dataTable.Rows.Count - 1 Step 1
            Dim oricomp As String = dataTable.Rows(i).Item("CompID").ToString
            Dim oriemp As String = dataTable.Rows(i).Item("EmpID").ToString
            Dim oridate As String = dataTable.Rows(i).Item("Date").ToString
            Dim oritxnID As String = dataTable.Rows(i).Item("TxnID").ToString
            Dim oridaytype As String = dataTable.Rows(i).Item("HolidayOrNot").ToString

            If oricomp = chkcompID And oriemp = chkempID And oridate = chkDate And oridaytype = "F" Then
                oricount = oricount + 1
                ovTimeCompID = oricomp
                ovTimeEmpID = oriemp
                ovTimeDate = oridate
            End If
        Next

        If oricount = count Then

            If HTime <= 240 Then
                result = 240
            ElseIf HTime > 240 And HTime <= 480 Then
                result = 480
            ElseIf HTime > 480 And HTime <= 720 Then
                result = 720
            ElseIf HTime > 720 Then
                result = HTime
            End If


        Else
            result = 0.0
        End If

        result = CDbl(FormatNumber((result / 60), 1))
        Return result
    End Function
#End Region

#Region "新增到資料庫"
    Public Sub InsertDB(ByVal dataTable As DataTable, ByVal OTTxnID As String)
        Dim strSQL_Insert As New StringBuilder
        Dim successFlag As Boolean = False
        Dim InsertCount As Integer = 0
        Dim checkHDate As String = ""
        For i As Integer = 0 To dataTable.Rows.Count - 1 Step 1
            Dim chktxnID As String = dataTable.Rows(i).Item("TxnID").ToString
            If OTTxnID.Equals(chktxnID) Then
                Dim chkcomp As String = dataTable.Rows(i).Item("CompID").ToString
                Dim chkemp As String = dataTable.Rows(i).Item("EmpID").ToString
                Dim chkdate As String = dataTable.Rows(i).Item("Date").ToString

                Dim chkdaytype As String = dataTable.Rows(i).Item("HolidayOrNot").ToString
                Dim chktime3 As Double = dataTable.Rows(i).Item("TimeMin_acc").ToString
                Dim holidayTime As Double = 0.0
                If checkHDate <> chkdate And chktime3 <> 0 And chkdaytype = "F" Then
                    InsertCount = 0
                    checkHDate = chkdate
                    InsertCount = InsertCount + 1
                    holidayTime = getHoliday(dataTable, chkcomp, chkemp, chkdate, InsertCount, chktime3, chkdaytype, chktxnID)
                ElseIf checkHDate = chkdate And chktime3 <> 0 Then
                    InsertCount = InsertCount + 1
                    holidayTime = getHoliday(dataTable, chkcomp, chkemp, chkdate, InsertCount, chktime3, chkdaytype, chktxnID)
                End If

                Dim holiday As String = IIf(dataTable.Rows(i).Item("HolidayOrNot").ToString = "T", "0", "1")


                strSQL_Insert.Append("Insert into " + eHRMSDB_ITRD + ".[dbo].[OverTime] (CompID,EmpID,OTDate,Seq,BeginTime,EndTime,OTNormal,OTOver,OTHoliday,OTHoliday1,SPHSC1Holiday1," &
                                     "SPHSC1Holiday2,SPHSC1Holiday3,SPHSC1Rest,PayDate,HolidayOrNot,SalaryPaid,LBSalaryPaid,ReleaseMark,ReleaseComp,ReleaseID,KeyInComp,KeyInID," &
                                     "HRKeyInFlag,DeptID,OrganID,DeptName,OrganName,ReqNo,TransferDate,LastChgComp,LastChgID,LastChgDate)")
                strSQL_Insert.Append(" values (" & Bsp.Utility.Quote(dataTable.Rows(i).Item("CompID")) & ",")
                strSQL_Insert.Append(Bsp.Utility.Quote(dataTable.Rows(i).Item("EmpID")) & ",")
                strSQL_Insert.Append(Bsp.Utility.Quote(dataTable.Rows(i).Item("Date")) & ",")
                strSQL_Insert.Append(Bsp.Utility.Quote(CInt(dataTable.Rows(i).Item("Seq")).ToString) & ",")
                strSQL_Insert.Append(Bsp.Utility.Quote(dataTable.Rows(i).Item("StartTime").ToString.Replace(":", "")) & ",")
                strSQL_Insert.Append(Bsp.Utility.Quote(dataTable.Rows(i).Item("EndTime").ToString.Replace(":", "")) & ",")
                strSQL_Insert.Append(Bsp.Utility.Quote(CDbl(dataTable.Rows(i).Item("Time_one")).ToString) & ",")
                strSQL_Insert.Append(Bsp.Utility.Quote(CDbl(dataTable.Rows(i).Item("Time_two")).ToString) & ",")
                strSQL_Insert.Append(Bsp.Utility.Quote(CDbl(dataTable.Rows(i).Item("Time_three")).ToString) & ",")
                strSQL_Insert.Append(Bsp.Utility.Quote(holidayTime) & ",")
                strSQL_Insert.Append(Bsp.Utility.Quote(0) & ",")
                strSQL_Insert.Append(Bsp.Utility.Quote(0) & ",")
                strSQL_Insert.Append(Bsp.Utility.Quote(0) & ",")
                strSQL_Insert.Append(Bsp.Utility.Quote(0) & ",")
                strSQL_Insert.Append(Bsp.Utility.Quote(0) & ",") 'PayDate
                'strSQL_Insert.Append("getDate(),")                 'ProcessDate
                strSQL_Insert.Append(Bsp.Utility.Quote(holiday) & ",") '@@
                strSQL_Insert.Append(Bsp.Utility.Quote("0") & ",") '因為是剛拋轉 一定還沒計薪
                strSQL_Insert.Append(Bsp.Utility.Quote("0") & ",")
                strSQL_Insert.Append(Bsp.Utility.Quote("1") & ",")       '放行註記
                strSQL_Insert.Append(Bsp.Utility.Quote("Batch") & ",")
                strSQL_Insert.Append(Bsp.Utility.Quote("Batch") & ",")
                strSQL_Insert.Append(Bsp.Utility.Quote("") & ",")        '輸入人員公司代碼
                strSQL_Insert.Append(Bsp.Utility.Quote("") & ",")        '輸入人員代號
                strSQL_Insert.Append(Bsp.Utility.Quote("1") & ",")       '人事處輸入註記
                strSQL_Insert.Append(Bsp.Utility.Quote(dataTable.Rows(i).Item("DeptID")) & ",")
                strSQL_Insert.Append(Bsp.Utility.Quote(dataTable.Rows(i).Item("OrganID")) & ",")
                strSQL_Insert.Append(Bsp.Utility.Quote(dataTable.Rows(i).Item("DeptName")) & ",")
                strSQL_Insert.Append(Bsp.Utility.Quote(dataTable.Rows(i).Item("OrganName")) & ",")
                strSQL_Insert.Append(Bsp.Utility.Quote("") & ",")      'BPMFLOW序號
                strSQL_Insert.Append(Bsp.Utility.Quote("") & ",")
                strSQL_Insert.Append(Bsp.Utility.Quote(UserProfile.ActCompID) & ",")
                strSQL_Insert.Append(Bsp.Utility.Quote(UserProfile.ActUserID) & ",")
                strSQL_Insert.Append("getDate())")
                strSQL_Insert.Append(";")
            End If
        Next

        Using cn As DbConnection = Bsp.DB.getConnection("AattendantDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Dim errorMsg = ""
            Try
                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL_Insert.ToString(), tran, "AattendantDB")
                tran.Commit()
                successFlag = True
            Catch ex As Exception '錯誤發生，同時RollBack
                tran.Rollback()
                errorMsg = ex.Message
                Bsp.Utility.ShowMessage(Me, "拋轉失敗")
                Throw
            Finally
                If tran IsNot Nothing Then
                    tran.Dispose()
                ElseIf errorMsg <> "" Then

                End If
            End Try
        End Using

        'If successFlag Then
        '    Bsp.Utility.ShowMessage(Me, "拋轉成功")
        'End If
    End Sub
#End Region
#End Region

End Class
