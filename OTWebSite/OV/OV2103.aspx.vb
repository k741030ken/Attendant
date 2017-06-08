Imports System
Imports System.Web.HttpServerUtility
Imports System.Data
Imports System.Globalization
Imports System.IO
Imports SinoPac.WebExpress.Common
Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json

Partial Class OV_OV2103
    Inherits PageBase
    Dim OV_3 As OV_3
    Dim OVBusinessCommon As OVBusinessCommon

#Region "功能鍵設定"
    ''' <summary>
    ''' 功能鍵設定
    ''' </summary>
    ''' <param name="Param">String</param>
    ''' <remarks></remarks>
    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnDelete"     '返回
                GoBack()
        End Select
    End Sub
#End Region

    '存取傳到修改頁資料
    Private Property _goUpdateDatas As DataTable
        Get
            Return Session.Item("GoUpdateDatas")
        End Get
        Set(ByVal value As DataTable)
            Session.Item("GoUpdateDatas") = value
        End Set
    End Property
    '存取傳到明細頁的資料
    Private Property _goSelectDatas As String
        Get
            Return Session.Item("GoSelectDatas")
        End Get
        Set(ByVal value As String)
            Session.Item("GoSelectDatas") = value
        End Set
    End Property

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            initScreen()
        End If
    End Sub

    Public Sub initScreen()
        OV_3 = New OV_3()
        OVBusinessCommon = New OVBusinessCommon
        OV_3.Type = "after"
        Dim Oridata As DataTable = _goUpdateDatas
        Dim newData As DataTable = _goUpdateDatas.Clone
        Dim jsStr As String = _goSelectDatas
        Dim showData As JArray = JsonConvert.DeserializeObject(Of JArray)(jsStr)

        Dim compID As String = showData(0).Item("OTCompID").ToString.Replace("""", "")
        Dim empID As String = showData(0).Item("OTEmpID").ToString.Replace("""", "")
        Dim empName As String = showData(0).Item("OTEmpName").ToString.Replace("""", "")
        Dim startDate As String = showData(0).Item("OTStartDate").ToString.Replace("""", "")
        Dim endDate As String = showData(0).Item("OTEndDate").ToString.Replace("""", "")
        Dim startTime As String = showData(0).Item("OTStartTime").ToString.Replace("""", "")
        Dim endTime As String = showData(0).Item("OTEndTime").ToString.Replace("""", "")
        Dim salaryOrAdjust As String = showData(0).Item("SalaryOrAdjustName").ToString.Replace("""", "")
        Dim adjustInvalidDate As String = showData(0).Item("AdjustInvalidDateShow").ToString.Replace("""", "")
        Dim isExist As Boolean = False
        For i As Integer = 0 To Oridata.Rows.Count - 1 Step 1

            Dim OriCompID As String = Oridata.Rows(i).Item("OTCompID").ToString
            Dim OriEmpID As String = Oridata.Rows(i).Item("OTEmpID").ToString
            Dim OriName As String = Oridata.Rows(i).Item("OTEmpName").ToString
            Dim OriSDate As String = Oridata.Rows(i).Item("OTStartDate").ToString
            Dim OriEDate As String = Oridata.Rows(i).Item("OTEndDate").ToString
            Dim OriSTime As String = Oridata.Rows(i).Item("StartTime").ToString
            Dim OriETime As String = Oridata.Rows(i).Item("EndTime").ToString

            If OriCompID <> "" And OriEmpID <> "" And OriName <> "" And OriSDate <> "" And OriEDate <> "" And OriSTime <> "" And OriETime <> "" Then

                Dim newRowData As DataRow = (From column In Oridata.Rows _
                   Where column("OTCompID") = compID _
                   And column("OTEmpID") = empID _
                   And column("OTEmpName") = empName _
                   And column("OTStartDate") = startDate _
                   And column("OTEndDate") = endDate _
                   And column("StartTime") = startTime _
                   And column("EndTime") = endTime _
                   Select column).FirstOrDefault()
                If isExist <> True Then
                    isExist = True
                    newData.ImportRow(newRowData)
                End If

            End If
        Next


        Dim ovCompID As String = newData.Rows(0).Item("OTCompID").ToString
        Dim ovEmpID As String = newData.Rows(0).Item("OTEmpID").ToString
        Dim ovSDate As String = newData.Rows(0).Item("OTStartDate").ToString
        Dim ovEDate As String = newData.Rows(0).Item("OTEndDate").ToString
        Dim ovSeq As String = newData.Rows(0).Item("OTSeq").ToString
        Dim ovTxnID As String = newData.Rows(0).Item("OTTxnID").ToString
        Dim lastCompID As String = newData.Rows(0).Item("LastChgComp").ToString
        Dim lastEmpID As String = newData.Rows(0).Item("LastChgID").ToString
        Dim addTimeH As Double = CDbl(newData.Rows(0).Item("OTTotalTime").ToString)
        Dim mealTime As String = newData.Rows(0).Item("MealTime").ToString
        If mealTime <> "0" Then
            cbMealFlag.Checked = True
        End If
        cbMealFlag.Enabled = False

        Dim dt As DataTable = OV_3.getOV1001DataTable(ovCompID, ovEmpID, ovSDate, ovEDate, ovSeq, ovTxnID)
        Dim item As DataRow = dt.Rows(0)
        If dt.Rows.Count > 1 Then
            Dim item2 As DataRow = dt.Rows(1)
        End If

        lalOTStartTimeDate.Text = item("OTStartDate").ToString()
        lalTime_one.Text = item("Time_one").ToString
        lalTime_two.Text = item("Time_two").ToString
        lalTime_three.Text = item("Time_three").ToString
        If dt.Rows.Count > 1 Then
            Dim item2 As DataRow = dt.Rows(1)
            lalOTStartTimeDate2.Text = item2("OTStartDate").ToString()
            lalTime_one2.Text = item2("Time_one").ToString
            lalTime_two2.Text = item2("Time_two").ToString
            lalTime_three2.Text = item2("Time_three").ToString
        Else
            lalOTStartTimeDate2.Text = ""
            lalTime_one2.Text = ""
            lalTime_two2.Text = ""
            lalTime_three2.Text = ""
            table_tr_Time2.Visible = False
        End If
        labOTAttachment.Value = (item("labOTAttachment").ToString).Trim
        If (labOTAttachment.Value).Equals("") Then
            lblFileName.Text = ""
            Button1.Visible = False
        Else
            lblFileName.Text = newData.Rows(0).Item("FileName").ToString
            Button1.Visible = True
        End If

        Dim monthData() As String = OVBusinessCommon.getTotalHR(ovCompID, ovEmpID, ovSDate)
        myData.Text = monthData(0)
        labSum1.Text = monthData(1)
        labSum2.Text = monthData(2)
        labSum3.Text = monthData(3)

        labCompID.Text = newData.Rows(0).Item("OTCompID").ToString + " " + getCompany(newData.Rows(0).Item("OTCompID").ToString)
        labDeptID.Text = newData.Rows(0).Item("DeptName").ToString
        labOrganID.Text = newData.Rows(0).Item("ORTName").ToString
        labOTEmpName.Text = newData.Rows(0).Item("OTEmpID").ToString & " " & newData.Rows(0).Item("OTEmpName").ToString
        labOTRegisterID.Text = newData.Rows(0).Item("OTRegisterID").ToString & " " & newData.Rows(0).Item("OTEmpRName").ToString
        labSalaryOrAdjust.Text = salaryOrAdjust
        labAdjustInvalidDate.Text = adjustInvalidDate
        labOverTimeDate.Text = startDate & "~" & endDate
        labOTPayDate.Text = IIf(newData.Rows(0).Item("OTPayDate").ToString = "0", "", newData.Rows(0).Item("OTPayDate").ToString)
        labOTStartDate.Text = newData.Rows(0).Item("StartTime").ToString.Insert(2, ":")
        labOTEndDate.Text = newData.Rows(0).Item("EndTime").ToString.Insert(2, ":")
        labOverTimeStr.Text = "加班時數 :" & addTimeH & " 小時 " & IIf(mealTime = "0", "", " (已扣除用餐時數:" & mealTime & "分鐘)")
        labMealTime.Text = " 扣除用餐時數 :" & mealTime & "分鐘"
        labOTTypeID.Text = newData.Rows(0).Item("CodeCName").ToString
        labOTReasonID.Text = newData.Rows(0).Item("OTReasonMemo").ToString
        labLastCompID.Text = lastCompID + " " + getCompany(lastCompID)
        labLastChgID.Text = lastEmpID + " " + getName(lastCompID, lastEmpID)
        labLastChgDate.Text = newData.Rows(0).Item("LastChgDate").ToString
    End Sub
    Private Sub GoBack()
        Dim ti As TransferInfo = Me.StateTransfer
        Me.TransferFramePage(ti.CallerUrl, Nothing, ti.Args)
    End Sub

    Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine("Select * From AttachInfo Where AttachID = '" + labOTAttachment.Value + "'")
        Dim dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "AattendantDB").Tables(0)
        Dim FILEBODY As Object = Bsp.DB.ExecuteScalar(strSQL.ToString, "AattendantDB")
        Dim dtCloned As DataTable = dt.Clone()
        Dim FileName As String = ""
        Dim FileSize As Integer
        Dim buffer As Byte()
        If dt.Rows.Count > 0 Then
            For Each item As DataRow In dt.Rows
                FILEBODY = item("FileBody")
                FileName = item("FileName")
                FileSize = item("FileSize")

                If FileSize > 0 Then
                    buffer = CType(FILEBODY, Byte())
                    dtCloned.ImportRow(item)
                    Dim b As Byte() = Encoding.Default.GetBytes(FileName)   '將字串轉為byte[]
                    'Debug.Print("Converted str = " + Encoding.Default.GetString(b))     '驗證轉碼後的字串,仍再正確的顯示.
                    Dim u As Byte() = Encoding.Convert(Encoding.Default, Encoding.UTF8, b)  '進行轉碼,參數1,來源編碼,參數二,目標編碼,參數三,欲編碼變數
                    FileName = Encoding.UTF8.GetString(u)   '將轉碼結果丟進去
                    'Debug.Print("UTF-8 str = " + Encoding.UTF8.GetString(u))       '顯示轉為UTF8後,仍能正確的顯示字串
                    'Util.ExportBinary(buffer, FileName)
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

            Dim name As String
            name = HttpContext.Current.Server.UrlPathEncode(FileName)
            'Dim strContentDisposition As String = String.Format("{0}; filename=\"{1}\"", "attachment", name)

            Try
                response.ClearHeaders()
                response.Clear()
                response.Charset = "utf-8"
                response.HeaderEncoding = System.Text.Encoding.GetEncoding("big5")
                response.AddHeader("Content-type", "Application/octet-stream")
                response.AddHeader("Content-Disposition", "attachment; filename=" + name)
                response.AddHeader("Content-Length", binFileBody.Length.ToString())
                response.BinaryWrite(binFileBody)
                response.Flush()
            Catch ex As Exception
                Bsp.Utility.ShowFormatMessage("下載失敗", ex.Message)
            Finally
                response.End()
            End Try
        End If
    End Sub
    Public Function getCompany(ByVal compID As String) As String
        Dim result As String = ""
        Dim strSQL As New StringBuilder
        Dim dataTable As New DataTable
        strSQL.Append("select CompName FROM Company ")
        strSQL.Append("where CompID = " & Bsp.Utility.Quote(compID))
        dataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)

        result = dataTable.Rows(0).Item("CompName").ToString
        Return result
    End Function
    Public Function getName(ByVal compID As String, ByVal empID As String) As String
        Dim result As String = ""
        Dim strSQL As New StringBuilder
        Dim dataTable As New DataTable
        strSQL.Append("select NameN FROM Personal ")
        strSQL.Append("where CompID= " & Bsp.Utility.Quote(compID))
        strSQL.Append(" And EmpID= " & Bsp.Utility.Quote(empID))
        dataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)

        result = dataTable.Rows(0).Item("NameN").ToString
        Return result
    End Function
End Class
