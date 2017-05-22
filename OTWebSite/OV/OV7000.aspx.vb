Imports System.Data
Imports System.Data.Common
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Partial Class OV_OV7000
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            initScreen()
        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String) '按鈕
        Select Case Param
            Case "btnAdd" '存檔
                doSave()
            Case "btnActionX" '清除
                doClear()
        End Select
    End Sub
#Region "檢核欄位"
    Private Function funCheckData(ByVal CompID As String) As Boolean
        If txtAdvaceBegin.Text = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "加班預先申請日的範圍月前")
            txtAdvaceBegin.Focus()
            Return False
        End If

        If txtAdvanceEnd.Text = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "加班預先申請日的範圍月後")
            txtAdvanceEnd.Focus()
            Return False
        End If

        If txtDeclarationBegin.Text = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "事後加班申報日的範圍月前")
            txtDeclarationBegin.Focus()
            Return False
        End If

        If txtDayLimitHourN.Text = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "平日可申請加班的時數上限")
            txtDayLimitHourN.Focus()
            Return False
        End If

        If txtDayLimitHourH.Text = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "假日可申請加班的時數上限")
            txtDayLimitHourH.Focus()
            Return False
        End If

        If txtMonthLimitHour.Text = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "每月加班的時數上限")
            txtMonthLimitHour.Focus()
            Return False
        End If
        
        If txtOTLimitDay.Text = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "連續加班日數限制")
            txtOTLimitDay.Focus()
            Return False
        End If

        If txtAdjustInvalidDate.DateText = "" Or txtAdjustInvalidDate.DateText = "____/__/__" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00031", "補休失效日輸入格式錯誤")
            txtAdjustInvalidDate.Focus()
            Return False
        End If
        Return True
    End Function
#End Region

    Public Sub initScreen()
        Dim CompID As String = UserProfile.SelectCompRoleID
        Dim CompName As String = UserProfile.SelectCompRoleName
        Dim strSQL_Para As New StringBuilder
        Dim data_Para As DataTable
        Dim data_Para_Count As Integer

        ViewState.Item("CompID") = CompID
        ViewState.Item("hasData") = False
        lblCompID.Text = CompName

        strSQL_Para.Append("SELECT Para,LastChgComp,LastChgID,LastChgDate FROM OverTimePara ")
        strSQL_Para.Append("WHERE CompID = " & Bsp.Utility.Quote(CompID))
        data_Para = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL_Para.ToString(), "AattendantDB").Tables(0)
        data_Para_Count = data_Para.Rows.Count
        Bsp.Utility.FillDDL(ddlEmpRankID, "eHRMSDB", "Rank", "RTrim(RankID)", "", Bsp.Utility.DisplayType.OnlyID, "", "And CompID = " & Bsp.Utility.Quote(UserProfile.SelectCompRoleID) & "", "ORDER BY RankID ASC")
        ddlEmpRankID.Items.Insert("0", New ListItem("---請選擇---", ""))
        Bsp.Utility.FillDDL(ddlValidRankID, "eHRMSDB", "Rank", "RTrim(RankID)", "", Bsp.Utility.DisplayType.OnlyID, "", "And CompID = " & Bsp.Utility.Quote(UserProfile.SelectCompRoleID) & "", "ORDER BY RankID ASC")
        ddlValidRankID.Items.Insert("0", New ListItem("---請選擇---", ""))
        Bsp.Utility.FillDDL(ddlAdjustRankID, "eHRMSDB", "Rank", "RTrim(RankID)", "", Bsp.Utility.DisplayType.OnlyID, "", "And CompID = " & Bsp.Utility.Quote(UserProfile.SelectCompRoleID) & "", "ORDER BY RankID ASC")
        ddlAdjustRankID.Items.Insert("0", New ListItem("---請選擇---", ""))

        '有資料的時候
        If data_Para_Count <> 0 Then
            ViewState.Item("hasData") = True
            Dim jsStr As String = data_Para.Rows(0).Item("Para").ToString()
            Dim oriData As JArray = JsonConvert.DeserializeObject(Of JArray)(jsStr)
            If jsStr <> "" Then
                ViewState.Item("CompName") = lblCompID.Text
                txtAdvaceBegin.Text = getRealValue(jsStr, oriData, "AdvaceBegin") : ViewState.Item("AdvaceBegin") = txtAdvaceBegin.Text
                txtAdvanceEnd.Text = getRealValue(jsStr, oriData, "AdvanceEnd") : ViewState.Item("AdvanceEnd") = txtAdvanceEnd.Text
                txtDeclarationBegin.Text = getRealValue(jsStr, oriData, "DeclarationBegin") : ViewState.Item("DeclarationBegin") = txtDeclarationBegin.Text
                txtDayLimitHourN.Text = getRealValue(jsStr, oriData, "DayLimitHourN") : ViewState.Item("DayLimitHourN") = txtDayLimitHourN.Text
                txtDayLimitHourH.Text = getRealValue(jsStr, oriData, "DayLimitHourH") : ViewState.Item("DayLimitHourH") = txtDayLimitHourH.Text
                txtMonthLimitHour.Text = getRealValue(jsStr, oriData, "MonthLimitHour") : ViewState.Item("MonthLimitHour") = txtMonthLimitHour.Text
                txtOTLimitDay.Text = getRealValue(jsStr, oriData, "OTLimitDay") : ViewState.Item("OTLimitDay") = txtOTLimitDay.Text
                txtMealTimeN.Text = getRealValue(jsStr, oriData, "MealTimeN") : ViewState.Item("MealTimeN") = txtMealTimeN.Text
                txtMealTimeH.Text = getRealValue(jsStr, oriData, "MealTimeH") : ViewState.Item("MealTimeH") = txtMealTimeH.Text
                'txtEmpRankID.Text = oriData(0).Item("EmpRankID").ToString.Replace("""", "") : ViewState.Item("EmpRankID") = txtEmpRankID.Text
                'txtValidRankID.Text = oriData(0).Item("ValidRankID").ToString.Replace("""", "") : ViewState.Item("ValidRankID") = txtValidRankID.Text
                ddlEmpRankID.SelectedValue = getRealValue(jsStr, oriData, "EmpRankID") : ViewState.Item("EmpRankID") = ddlEmpRankID.SelectedValue
                ddlValidRankID.SelectedValue = getRealValue(jsStr, oriData, "ValidRankID") : ViewState.Item("ValidRankID") = ddlValidRankID.SelectedValue
                ddlAdjustRankID.SelectedValue = getRealValue(jsStr, oriData, "AdjustRankID") : ViewState.Item("AdjustRankID") = ddlAdjustRankID.SelectedValue
                txtAdjustInvalidDate.DateText = getRealValue(jsStr, oriData, "AdjustInvalidDate") : ViewState.Item("AdjustInvalidDate") = txtAdjustInvalidDate.DateText

                Dim tempComp1 As String = data_Para.Rows(0).Item("LastChgComp").ToString
                Dim tempComp2 As String = ""
                tempComp2 = tempComp1 + "-" + getCompany(tempComp1)
                Dim tempName As String = data_Para.Rows(0).Item("LastChgID").ToString
                tempName = tempName + "-" + getName(tempComp1, tempName)
                txtLastChgComp.Text = tempComp2 : ViewState.Item("LastChgComp") = txtLastChgComp.Text
                txtLastChgDate.Text = Format(data_Para.Rows(0).Item("LastChgDate"), "yyyy/MM/dd HH:mm:ss") : ViewState.Item("LastChgDate") = txtLastChgDate.Text
                txtLastChgID.Text = tempName : ViewState.Item("LastChgID") = txtLastChgID.Text

                Dim rDayLimitFlag As String = getRealValue(jsStr, oriData, "DayLimitFlag") : ViewState.Item("DayLimitFlag") = rDayLimitFlag
                Dim rMonthLimitFlag As String = getRealValue(jsStr, oriData, "MonthLimitFlag") : ViewState.Item("MonthLimitFlag") = rMonthLimitFlag
                Dim rOTMustCheck As String = getRealValue(jsStr, oriData, "OTMustCheck") : ViewState.Item("OTMustCheck") = rOTMustCheck
                Dim rOTLimitFlag As String = getRealValue(jsStr, oriData, "OTLimitFlag") : IIf(rOTLimitFlag.Equals("0"), rbtnOTLimitFlagPrompt.Checked = True, rbtnOTLimitFlagApply.Checked = True) : ViewState.Item("OTLimitFlag") = rOTLimitFlag
                Dim rSalaryOrAjust As String = getRealValue(jsStr, oriData, "SalaryOrAjust") : ViewState.Item("SalaryOrAjust") = rSalaryOrAjust

                If rDayLimitFlag = "0" Then
                    rbtnDayLimitFlagPrompt.Checked = True
                Else
                    rbtnDayLimitFlagApply.Checked = True
                End If

                If rMonthLimitFlag = "0" Then
                    rbtnMonthLimitFlagPrompt.Checked = True
                Else
                    rbtnMonthLimitFlagApply.Checked = True
                End If

                If rOTMustCheck = "0" Then
                    rbtnOTMustCheckYes.Checked = True
                Else
                    rbtnOTMustCheckNo.Checked = True
                End If

                If rOTLimitFlag = "0" Then
                    rbtnOTLimitFlagPrompt.Checked = True
                Else
                    rbtnOTLimitFlagApply.Checked = True
                End If

                If rSalaryOrAjust = "2" Then
                    rbtnTurnAdjust.Checked = True
                Else
                    rbtnTurnSalary.Checked = True
                End If
            End If

        End If

    End Sub
    Public Function getRealValue(ByVal jsStr As String, ByVal oriData As JArray, ByVal keyStr As String) As String
        Dim result As String = ""
        If jsStr.Contains(keyStr) Then
            result = oriData(0).Item(keyStr).ToString.Replace("""", "")
        End If
        Return result
    End Function
    Public Sub doSave()

        Dim isInsert As Boolean = IIf(ViewState.Item("hasData"), False, True) '是要Insert還是Update
        Dim compID As String = ViewState.Item("CompID")
        Dim strSQL As New StringBuilder()
        If funCheckData(compID) Then

            Dim successFlag As Boolean = False
            Dim jsAry As New JArray
            Dim jsObj As New JObject
            Dim jsStr As String = ""

            jsObj.Add("AdvaceBegin", txtAdvaceBegin.Text)
            jsObj.Add("AdvanceEnd", txtAdvanceEnd.Text)
            jsObj.Add("DeclarationBegin", txtDeclarationBegin.Text)
            jsObj.Add("DayLimitHourN", txtDayLimitHourN.Text)
            jsObj.Add("DayLimitHourH", txtDayLimitHourH.Text)
            Dim DayFlag As String = IIf(rbtnDayLimitFlagPrompt.Checked = True, "0", "1")
            jsObj.Add("DayLimitFlag", DayFlag)
            jsObj.Add("MonthLimitHour", txtMonthLimitHour.Text)
            Dim MonthFlag As String = IIf(rbtnMonthLimitFlagPrompt.Checked = True, "0", "1")
            jsObj.Add("MonthLimitFlag", MonthFlag)
            Dim MustCheck As String = IIf(rbtnOTMustCheckYes.Checked = True, "0", "1")
            jsObj.Add("OTMustCheck", MustCheck)
            jsObj.Add("OTLimitDay", txtOTLimitDay.Text)
            Dim LimitFlag As String = IIf(rbtnOTLimitFlagPrompt.Checked = True, "0", "1")
            jsObj.Add("OTLimitFlag", LimitFlag)
            jsObj.Add("MealTimeN", txtMealTimeN.Text)
            jsObj.Add("MealTimeH", txtMealTimeH.Text)
            jsObj.Add("EmpRankID", ddlEmpRankID.SelectedValue)
            jsObj.Add("ValidRankID", ddlValidRankID.SelectedValue)
            jsObj.Add("AdjustRankID", ddlAdjustRankID.SelectedValue)
            Dim Salary As String = IIf(rbtnTurnAdjust.Checked = True, "2", "1")
            jsObj.Add("SalaryOrAjust", Salary)
            jsObj.Add("AdjustInvalidDate", txtAdjustInvalidDate.DateText)

            jsAry.Add(jsObj)

            jsStr = JsonConvert.SerializeObject(jsAry, Formatting.None)

            If isInsert Then
                strSQL.Append("Insert into OverTimePara(CompID,Para,LastChgComp,LastChgID,LastChgDate) ")
                strSQL.Append("values(" & Bsp.Utility.Quote(compID) & ",")
                strSQL.Append(Bsp.Utility.Quote(jsStr) & ",")
                strSQL.Append(Bsp.Utility.Quote(UserProfile.ActCompID) & ",")
                strSQL.Append(Bsp.Utility.Quote(UserProfile.ActUserID) & ",")
                strSQL.Append("getDate())")
            Else
                strSQL.Append("Update OverTimePara ")
                strSQL.Append("Set Para = " & Bsp.Utility.Quote(jsStr) & ",")
                strSQL.Append("LastChgComp = " & Bsp.Utility.Quote(UserProfile.ActCompID) & ",")
                strSQL.Append("LastChgID = " & Bsp.Utility.Quote(UserProfile.ActUserID) & ",")
                strSQL.Append("LastChgDate = getDate() ")
                strSQL.Append("Where CompID = " & Bsp.Utility.Quote(compID))
            End If

            Using cn As DbConnection = Bsp.DB.getConnection("AattendantDB")
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction
                Try
                    Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran, "AattendantDB")
                    tran.Commit()
                    successFlag = True
                Catch ex As Exception '錯誤發生，同時RollBack
                    tran.Rollback()
                    Bsp.Utility.ShowMessage(Me, "存檔失敗")
                    Throw
                Finally
                    If tran IsNot Nothing Then tran.Dispose()
                End Try
            End Using

            If successFlag Then
                Bsp.Utility.ShowMessage(Me, "存檔成功")
            End If
        End If

    End Sub

    Public Sub doClear()
        Dim hasData As Boolean = ViewState.Item("hasData")
        If hasData Then
            lblCompID.Text = ViewState.Item("CompName")
            txtAdvaceBegin.Text = ViewState.Item("AdvaceBegin")
            txtAdvanceEnd.Text = ViewState.Item("AdvanceEnd")
            txtDeclarationBegin.Text = ViewState.Item("DeclarationBegin")
            txtDayLimitHourN.Text = ViewState.Item("DayLimitHourN")
            txtDayLimitHourH.Text = ViewState.Item("DayLimitHourH")
            Dim rDayLimitFlag As String = ViewState.Item("DayLimitFlag").ToString : IIf(rDayLimitFlag.Equals("0"), rbtnDayLimitFlagPrompt.Checked = True, rbtnDayLimitFlagApply.Checked = True)
            txtMonthLimitHour.Text = ViewState.Item("MonthLimitHour")
            Dim rMonthLimitFlag As String = ViewState.Item("MonthLimitFlag").ToString : IIf(rMonthLimitFlag.Equals("0"), rbtnMonthLimitFlagPrompt.Checked = True, rbtnMonthLimitFlagApply.Checked = True)
            Dim rOTMustCheck As String = ViewState.Item("OTMustCheck").ToString : IIf(rOTMustCheck.Equals("0"), rbtnOTMustCheckYes.Checked = True, rbtnOTMustCheckNo.Checked = True)
            txtOTLimitDay.Text = ViewState.Item("OTLimitDay")
            Dim rOTLimitFlag As String = ViewState.Item("OTLimitFlag").ToString : IIf(rOTLimitFlag.Equals("0"), rbtnOTLimitFlagPrompt.Checked = True, rbtnOTLimitFlagApply.Checked = True)
            txtMealTimeN.Text = ViewState.Item("MealTimeN")
            txtMealTimeH.Text = ViewState.Item("MealTimeH")
            ddlEmpRankID.SelectedValue = ViewState.Item("EmpRankID")
            ddlValidRankID.SelectedValue = ViewState.Item("ValidRankID")
            Dim rSalaryOrAjust As String = ViewState.Item("SalaryOrAjust").ToString : IIf(rSalaryOrAjust.Equals("2"), rbtnTurnAdjust.Checked = True, rbtnTurnSalary.Checked = True)
            txtAdjustInvalidDate.DateText = ViewState.Item("AdjustInvalidDate")
            txtLastChgComp.Text = ViewState.Item("LastChgComp")
            txtLastChgDate.Text = ViewState.Item("LastChgDate")
            txtLastChgID.Text = ViewState.Item("LastChgID")
        Else
            txtAdvaceBegin.Text = ""
            txtAdvanceEnd.Text = ""
            txtDeclarationBegin.Text = ""
            txtDayLimitHourN.Text = ""
            txtDayLimitHourH.Text = ""
            rbtnDayLimitFlagPrompt.Checked = False
            rbtnDayLimitFlagApply.Checked = False
            txtMonthLimitHour.Text = ""
            rbtnMonthLimitFlagPrompt.Checked = False
            rbtnMonthLimitFlagApply.Checked = False
            rbtnOTMustCheckYes.Checked = False
            rbtnOTMustCheckNo.Checked = False
            txtOTLimitDay.Text = ""
            rbtnOTLimitFlagPrompt.Checked = False
            rbtnOTLimitFlagApply.Checked = False
            txtMealTimeN.Text = ""
            txtMealTimeH.Text = ""
            ddlEmpRankID.SelectedValue = ""
            ddlValidRankID.SelectedValue = ""
            rbtnTurnAdjust.Checked = False
            rbtnTurnSalary.Checked = False
            txtAdjustInvalidDate.DateText = ""
            txtLastChgComp.Text = ""
            txtLastChgDate.Text = ""
            txtLastChgID.Text = ""
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
        strSQL.Append("where  CompID= " & Bsp.Utility.Quote(compID))
        strSQL.Append(" And EmpID = " & Bsp.Utility.Quote(empID))
        dataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)

        result = dataTable.Rows(0).Item("NameN").ToString
        Return result
    End Function
End Class
