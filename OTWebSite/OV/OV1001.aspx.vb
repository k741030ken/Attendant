Imports System.Data
Imports System.Globalization
Imports System.IO
Imports SinoPac.WebExpress.Common

Partial Class OV_OV1001
    Inherits PageBase
#Region "全域變數"

    Dim OV_3 As OV_3
    Dim AttachmentID As String

#End Region

#Region "功能選單"

    ''' <summary>
    ''' 功能清單
    ''' </summary>
    ''' <param name="Param"></param>
    ''' <remarks></remarks>
    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnActionX"     '返回
                GoBack()
        End Select
    End Sub

    ''' <summary>
    ''' 返回功能
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GoBack()
        Dim ti As TransferInfo = Me.StateTransfer
        Me.TransferFramePage(ti.CallerUrl, Nothing, ti.Args)
    End Sub

#End Region

#Region "載入頁面"

    ''' <summary>
    ''' 頁面進入點
    ''' </summary>
    ''' <param name="ti"></param>
    ''' <remarks></remarks>
    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)
            OV_3 = New OV_3()
            Dim dt As DataTable
            Dim OTCompID As String = ht("OTCompID").ToString()
            Dim OTEmpID As String = ht("OTEmpID").ToString()
            Dim OTStartDate As String = ht("OTStartDate").ToString()
            Dim OTEndDate As String = ht("OTEndDate").ToString()
            Dim OTSeq As String = ht("OTSeq").ToString()
            OV_3.Type = ht("hiddenType").ToString()
            ViewState("Type") = ht("hiddenType").ToString()
            Dim OTTxnID As String = ht("OTTxnID").ToString()
            dt = OV_3.getOV1001DataTable(OTCompID, OTEmpID, OTStartDate, OTEndDate, OTSeq, OTTxnID)
            OTStartDate = dt.Rows(0).Item("labOverTimeDate").ToString().Split("~")(0)
            Dim dt2 As DataTable = OV_3.getOV1001SumDataTable(OTCompID, OTEmpID, OTStartDate)
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
        Dim objSC As New SC()
        '開始日期 結束日期 開始時間 結束時間
        Dim mStartDate As String
        Dim mEndDate As String
        Dim mStartTime As String
        Dim mEndTime As String

        If dt.Rows.Count > 0 Then
            Dim item As DataRow = dt.Rows(0)


            If dt.Rows.Count > 1 Then
                Dim item2 As DataRow = dt.Rows(1)
                mStartDate = ((item("labOverTimeDate").ToString.Split("~"))(0))
                mEndDate = (item2("labOverTimeDate").ToString.Split("~"))(1)
                mStartTime = (item("labOTStartDate").ToString)
                mEndTime = (item2("labOTEndDate").ToString)
                labMealTime.Text = (Convert.ToInt32(item("labMealTime")) + Convert.ToInt32(item2("labMealTime"))).ToString
            Else
                mStartDate = ((item("labOverTimeDate").ToString.Split("~"))(0))
                mEndDate = (item("labOverTimeDate").ToString.Split("~"))(1)
                mStartTime = (item("labOTStartDate").ToString)
                mEndTime = (item("labOTEndDate").ToString)
                labMealTime.Text = item("labMealTime").ToString
            End If
            labOTStartDate.Text = mStartTime.Substring(0, 2) + ":" + mStartTime.Substring(2, 2)
            labOTEndDate.Text = mEndTime.Substring(0, 2) + ":" + mEndTime.Substring(2, 2)
            labOverTimeDate.Text = mStartDate + "~" + mEndDate

            labCompID.Text = item("labCompID").ToString
            labDeptID.Text = item("labDeptID").ToString
            labOrganID.Text = item("labOrganID").ToString
            labOTEmpName.Text = item("labOTEmpName").ToString
            labSalaryOrAdjust.Text = item("labSalaryOrAdjust").ToString
            labAdjustInvalidDate.Text = FormatDateTime(item("labAdjustInvalidDate").ToString, DateFormat.ShortDate).ToString

            labOTTypeID.Text = item("labOTTypeID").ToString
            labOTReasonID.Text = item("labOTReasonID").ToString
            labOTAttachment.Value = (item("labOTAttachment").ToString).Trim

            labIsProcessDate.Text = (item("labIsProcessDate").ToString).Trim
            Button1.Visible = False
            If labOTAttachment.Value.Count > 0 Then
                Dim objOV_3 As OV_3 = New OV_3()
                Dim mFileName As String = objOV_3.getFileName(labOTAttachment.Value)
                If mFileName.Count > 0 Then
                    Button1.Visible = True
                    fileName.Text = "附件檔名：" + mFileName
                End If
            End If





            labWorkStatus.Text = item("labWorkStatus").ToString
            labWorkType.Text = item("labWorkType").ToString
            labSex.Text = item("labSex").ToString
            labRankID.Text = item("labRankID").ToString
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
                Dim UserName As String = OV_3.GetPersonName(item("LastChgComp").ToString, item("labLastChgID").ToString)
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


            If "after".Equals(OV_3.Type) Then
                labOTPayDate.Text = item("labOTPayDate").ToString
                If labOTPayDate.Text.Equals("0") Then
                    labOTPayDate.Text = ""
                End If
            End If


            If ("轉薪資".Equals(item("labSalaryOrAdjust"))) Then
                Label1.Visible = False
                labAdjustInvalidDate.Visible = False
            End If

            '開始日期 結束日期 開始時間 結束時間
            Dim overTimeM As String = OV_3.getOverTime(mStartDate, mEndDate, mStartTime, mEndTime, "M")
            Dim overTimeH = CDbl(FormatNumber((Convert.ToDouble(overTimeM) / 60), 1)).ToString()
            Dim str As String = "加班時數：" + overTimeH + "小時"
            Dim mealTime = 0
            If dt.Rows.Count > 1 Then
                mealTime = Convert.ToDouble(item("labMealTime")) + Convert.ToDouble(dt.Rows(1).Item("labMealTime"))
            Else
                mealTime = Convert.ToDouble(item("labMealTime"))
            End If


            If item("cbMealFlag").Equals("1") Then
                cbMealFlag.Checked = True
                If Not "0".Equals(mealTime.ToString.Trim) Then
                    Dim timeSpan1 = New TimeSpan(0, overTimeM, 0)
                    Dim timeSpan2 = New TimeSpan(0, mealTime.ToString, 0)
                    overTimeH = CDbl(FormatNumber((timeSpan1.Subtract(timeSpan2)).TotalHours, 1)).ToString()
                    str = "加班時數：" + overTimeH + "小時"
                    labOverTimeStr1.Text = "(已扣除用餐時數" + mealTime.ToString + "分鐘)"
                    cbMealFlag.Style.Value = "color:Red;"
                    labMealTime.Style.Value = "color:Red;"
                    Label5.Style.Value = "color:Red;"
                End If
            Else
                cbMealFlag.Checked = False
            End If
            cbMealFlag.Enabled = False
            labOverTimeStr.Text = str
            Dim mydataStr As String() = item("labOverTimeDate").ToString().Split("/")
            myData.Text = mydataStr(1)

            'Next
        End If
        'Dim st
        Dim OverTimeSumObjectList1 As ArrayList = New ArrayList() '送簽 2
        Dim OverTimeSumObjectList2 As ArrayList = New ArrayList() '核准 3
        Dim OverTimeSumObjectList3 As ArrayList = New ArrayList() '駁回 4




        If dt2.Rows.Count > 0 Then
            For Each item As DataRow In dt2.Rows
                Dim OverTimeSumObject As OV_3 = New OV_3()
                OverTimeSumObject.OvertimeDateB = item("OTStartDate").ToString()
                OverTimeSumObject.OvertimeDateE = item("OTEndDate").ToString()
                OverTimeSumObject.OTStartTime = item("OTStartTime").ToString()
                OverTimeSumObject.OTEndTime = item("OTEndTime").ToString()
                OverTimeSumObject.OTStatus = item("OTStatus").ToString()
                OverTimeSumObject.Time = item("OTTotalTime").ToString()
                OverTimeSumObject.mealTime = item("MealTime").ToString()
                OverTimeSumObject.mealFlag = item("MealFlag").ToString()

                'OverTimeSumObject.
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

        labSum1.Text = OV_3.getSumOTTime(OverTimeSumObjectList1).ToString()
        labSum2.Text = OV_3.getSumOTTime(OverTimeSumObjectList2).ToString()
        labSum3.Text = OV_3.getSumOTTime(OverTimeSumObjectList3).ToString()


    End Sub
#End Region

#Region "附件下載"

    ''' <summary>
    ''' 附件觸發事件點
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine("Select * From [dbo].[AttachInfo] Where AttachID = '" + labOTAttachment.Value + "'")
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
            If System.Web.HttpContext.Current.Request.Browser.Browser = "IE" Then
                FileName = System.Web.HttpContext.Current.Server.UrlPathEncode(FileName)
            End If

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

            Finally
                response.End()
            End Try
        End If
    End Sub

    Shared Function ByteArrayToStr(ByVal bt As Byte()) As String
        Dim encoding As New System.Text.ASCIIEncoding()
        Return encoding.GetString(bt)
    End Function
#End Region



End Class
