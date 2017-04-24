Imports System.Data
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Partial Class OV_OV4300
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            initArgs()
        End If
    End Sub
    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            initArgs()
        End If
    End Sub
#Region "全域變數"
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
    Private Property AattendantDB As String
        Get
            Dim result As String = ConfigurationManager.AppSettings("AattendantDB")
            If String.IsNullOrEmpty(result) Then
                result = "AattendantDB"
            End If
            Return result
        End Get
        Set(ByVal value As String)

        End Set
    End Property
    Private Property queryDatas As DataTable
        Get
            Return ViewState.Item("QueryDatas")
        End Get
        Set(ByVal value As DataTable)
            ViewState.Item("QueryDatas") = value
        End Set
    End Property
    Private Property fixDatas As DataTable
        Get
            Return ViewState.Item("FixDatas")
        End Get
        Set(ByVal value As DataTable)
            ViewState.Item("FixDatas") = value
        End Set
    End Property
#End Region
#Region "功能鍵"
    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnAdd"       '拋轉
                doThrow()
            Case "btnQuery"     '查詢
                doQuery("F")
            Case "btnDownload"  '匯出

            Case "btnActionX"   '清除
                doClear(True)
                '呼叫Client端 Javascript
                ScriptManager.RegisterStartupScript(Me, Page.GetType, "script", "btnClear();", True)
        End Select
    End Sub
#End Region
#Region "下拉選單"
    Protected Sub ddlPersonOrDate_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlPersonOrDate.SelectedIndexChanged
        '預防殘值
        doClear(False)
        Dim PersonOrCity As String = ddlPersonOrDate.Text
        ViewState.Item("ChoseType") = PersonOrCity
        Select Case PersonOrCity
            Case Is = "DateBlock"
                trDate.Visible = True
                trEmp1.Visible = False
                trEmp2.Visible = False
                'trStay1.Visible = False
                'trStay2.Visible = False
                PersonTable.Visible = False
                '預防殘值
                txtEmpID.Text = ""
                ucEmpDate.DateText = ""
                'ucStayDate.DateText = ""
                'txtPayDate.Text = ""
            Case Is = "PersonBlock"
                trDate.Visible = False
                trEmp1.Visible = True
                trEmp2.Visible = True
                'PersonTable.Visible = True
                'trStay1.Visible = False
                'trStay2.Visible = False
                '預防殘值
                ucDate.DateText = ""
                'ucStayDate.DateText = ""
                'txtPayDate.Text = ""
            Case Is = "StayBlock"
                trDate.Visible = False
                trEmp1.Visible = False
                trEmp2.Visible = False
                'trStay1.Visible = True
                'trStay2.Visible = True
                '預防殘值
                txtEmpID.Text = ""
                ucEmpDate.DateText = ""
                ucDate.DateText = ""
        End Select
    End Sub
#End Region
#Region "拋轉設定為人員-查詢"
    Public Sub doQuery(ByVal times As String)
        If ddlPersonOrDate.Text = "" Then
            Bsp.Utility.ShowMessage(Me, "請先設定拋轉條件")
            Return
        End If

        pcMain.DataTable = Nothing
        gvMain.DataBind()

        Dim choseType As String = ViewState.Item("ChoseType").ToString
        Dim isQuery As Boolean = False
        Dim data_OriEmp As New DataTable
        Dim data_OriEmp_Count As Integer = 0
        Dim data_FixEmp As DataTable = getTable_Emp()
        Dim screenEmp As String = StrF(txtEmpID.Text)
        Dim screenEmpDate As String = DateStrF(ucEmpDate.DateText)

        If choseType = "PersonBlock" Then
            If StrF(txtEmpID.Text) = "" Then
                Bsp.Utility.ShowMessage(Me, "請先設定拋轉人員")
                Return
            Else
                isQuery = True
            End If
        Else
            Bsp.Utility.ShowMessage(Me, "設定不為拋轉人員")
            Return
        End If

        If isQuery Then
            data_OriEmp = getOriEmpData(screenEmpDate, screenEmp)
            data_OriEmp_Count = data_OriEmp.Rows.Count
            ViewState.Item("isQuery") = isQuery
            ViewState.Item("data_OriEmp") = data_OriEmp
        End If

        If data_OriEmp_Count <> 0 Then
            For i As Integer = 0 To data_OriEmp_Count - 1 Step 1
                Dim dataRows As DataRow
                dataRows = data_FixEmp.NewRow
                dataRows("CompID") = data_OriEmp.Rows(i).Item("OTCompID").ToString
                dataRows("TxnID") = data_OriEmp.Rows(i).Item("OTTxnID").ToString
                dataRows("SeqNo") = data_OriEmp.Rows(i).Item("OTSeqNo").ToString
                dataRows("DeptName") = data_OriEmp.Rows(i).Item("DeptName").ToString
                dataRows("OrganName") = data_OriEmp.Rows(i).Item("OrganName").ToString
                dataRows("EmpID") = data_OriEmp.Rows(i).Item("OTEmpID").ToString
                dataRows("EmpName") = data_OriEmp.Rows(i).Item("NameN").ToString
                dataRows("Date") = data_OriEmp.Rows(i).Item("OTStartDate").ToString
                dataRows("Time") = data_OriEmp.Rows(i).Item("OTStartTime").ToString.Insert(2, ":") & "~" & data_OriEmp.Rows(i).Item("OTEndTime").ToString.Insert(2, ":")
                dataRows("Reason") = data_OriEmp.Rows(i).Item("OTReasonMemo").ToString
                dataRows("StartTime") = data_OriEmp.Rows(i).Item("OTStartTime").ToString
                dataRows("EndTime") = data_OriEmp.Rows(i).Item("OTEndTime").ToString
                data_FixEmp.Rows.Add(dataRows)
            Next
            '拋轉時需要使用
            ViewState.Item("data_FixEmp") = data_FixEmp
            'Table顯示
            PersonTable.Visible = True
            pcMain.DataTable = data_FixEmp
            gvMain.DataBind()

        ElseIf data_OriEmp_Count = 0 And times = "F" Then
            ViewState.Item("isQuery") = False
            Bsp.Utility.ShowMessage(Me, "該人員尚未有核准過的加班單")
            Return
        End If


    End Sub
#End Region
#Region "清理時段陣列"
    Public Function clearArray() As Array
        Dim timeArray(3) As Double
        timeArray(0) = 0
        timeArray(1) = 0
        timeArray(2) = 0
        Return timeArray
    End Function
#End Region
#Region "拋轉"
    Public Sub doThrow()
        If ddlPersonOrDate.Text = "" Then
            Bsp.Utility.ShowMessage(Me, "請先設定拋轉條件")
            Return
        End If

        '變數宣告
        Dim strSQL_Declaration As New StringBuilder()
        Dim data_Declaration As New DataTable            '原生資料
        Dim data_Declaration_Count As Integer            '原生資料筆數
        Dim data_Period As DataTable = getTable_Period() '存放時段的Table
        Dim transEmpID As String = ""                    '用來檢查異動的員工編號
        Dim checkEmpID As String = ""                    '用來檢查舊資料的員工編號
        Dim checkDate As String = ""                     '用來檢查舊資料的日期
        Dim choseType As String = ViewState.Item("ChoseType").ToString '下拉選單選擇的拋轉條件

        If choseType = "DateBlock" Then

            If DateStrF(ucDate.DateText) = "" Then
                Bsp.Utility.ShowMessage(Me, "請先設定拋轉日期")
                Return
            End If

            Dim screenDate As String = ""
            Dim screenEmp As String = ""

            '畫面-拋轉日期
            If DateStrF(ucDate.DateText) <> "" Then
                screenDate = DateStrF(ucDate.DateText)
            End If

            '查詢資料(加班申報檔)
            data_Declaration = getOriData(screenDate, "", False)
            data_Declaration_Count = data_Declaration.Rows.Count

            '查無資料
            If data_Declaration_Count = 0 Then
                Bsp.Utility.ShowMessage(Me, "目前並無審核過的加班單")
                Return
            End If

            If screenDate <> "" Then '拋轉條件設定為日期且有輸入日期
                '檢查異動員工資料 
                For i As Integer = 0 To data_Declaration_Count - 1 Step 1
                    Dim transData As DataTable
                    Dim transData_Count As Integer
                    Dim strSQL_Trans As New StringBuilder()
                    Dim empID As String = data_Declaration.Rows(i).Item("OTEmpID").ToString
                    Dim CompID As String = data_Declaration.Rows(i).Item("OTCompID").ToString
                    If empID <> transEmpID Then
                        transEmpID = empID
                        strSQL_Trans.Append("select ValidDate,DATEADD (MONTH, DATEDIFF (MONTH ,0,ValidDate)+1, 0) As EndDate from [eHRMSDB].[dbo].[EmployeeWait]") '創一個欄位給某筆日期下個月一號
                        strSQL_Trans.Append("Where 1=1")
                        strSQL_Trans.Append(" And CompID = " & Bsp.Utility.Quote(CompID))
                        strSQL_Trans.Append(" And EmpID = " & Bsp.Utility.Quote(empID))
                        strSQL_Trans.Append(" And Reason In('11','12','13','14','15','16','17','18')")
                        strSQL_Trans.Append(" And ValidDate Between " & Bsp.Utility.Quote(screenDate) & " And DATEADD (MONTH, DATEDIFF (MONTH ,0," & Bsp.Utility.Quote(screenDate) & ")+1, 0)")
                        transData = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL_Trans.ToString(), "AattendantDB").Tables(0)

                        '異動資料筆數
                        transData_Count = transData.Rows.Count

                        '如果有異動資料的話....
                        If transData_Count <> 0 Then
                            Dim data_DTemp As DataTable                '原生資料(異動)
                            Dim data_DTemp_Count As Integer            '原生資料筆數(異動)
                            Dim ucDate As String = transData.Rows(0).Item("EndDate").ToString
                            data_DTemp = getOriData(ucDate, transEmpID, True)
                            data_DTemp_Count = data_DTemp.Rows.Count
                            '先清除原本的資料
                            For b As Integer = data_Declaration_Count - 1 To 0 Step -1
                                If screenEmp = transEmpID Then
                                    data_Declaration.Rows.RemoveAt(b)
                                End If
                            Next
                            '加回重新抓取的資料
                            For a As Integer = 0 To data_DTemp_Count - 1 Step 1
                                data_Declaration.ImportRow(data_DTemp.Rows(a))
                            Next
                        End If

                    End If
                Next
            End If

        ElseIf choseType = "PersonBlock" Then
            If StrF(txtEmpID.Text) = "" Then
                Bsp.Utility.ShowMessage(Me, "請先設定拋轉人員")
                Return
            End If
            If ViewState.Item("isQuery") <> True Then
                Bsp.Utility.ShowMessage(Me, "請先查詢資料")
                Return
            End If
            '如果沒有選取資料的檢核處理-----------------------------------------<<<<
            Dim check_chb As Boolean = False
            For i As Integer = 0 To gvMain.Rows.Count - 1 Step 1
                Dim objChk As CheckBox = gvMain.Rows(i).FindControl("chk_gvMain")
                If objChk.Checked Then
                    check_chb = True
                End If
            Next
            If check_chb <> True Then
                Bsp.Utility.ShowMessage(Me, "請先選取欲拋轉資料")
                Return
            Else
                Dim errorMsg As String = chkToCross()
                If errorMsg <> "" Then
                    Bsp.Utility.ShowMessage(Me, errorMsg)
                    Return
                End If
            End If
            queryDatas = ViewState.Item("data_OriEmp")
            data_Declaration = queryDatas.Copy
            data_Declaration_Count = data_Declaration.Rows.Count

            fixDatas = ViewState.Item("data_FixEmp")
            Dim data_Emp As DataTable = fixDatas.Copy
            Dim data_Emp_Count As Integer = 0
            data_Emp_Count = data_Emp.Rows.Count

            Dim tempDatas As DataTable = data_Emp.Clone
            Dim tempCount As Integer = 0
            Dim checkAll As Boolean = False
            '畫面比對
            For i As Integer = data_Emp_Count - 1 To 0 Step -1
                Dim isRemove = True
                For j As Integer = 0 To gvMain.Rows.Count - 1 Step 1
                    Dim objChk As CheckBox = gvMain.Rows(j).FindControl("chk_gvMain")
                    If objChk.Checked Then
                        If gvMain.DataKeys(j).Item("Date") = data_Emp.Rows(i).Item("Date") And gvMain.DataKeys(j).Item("Time") = data_Emp.Rows(i).Item("Time").ToString Then
                            tempCount = tempCount + 1
                            isRemove = False
                        End If
                    End If
                Next
                If isRemove Then
                    tempDatas.ImportRow(data_Emp.Rows(i))
                    data_Emp.Rows.RemoveAt(i)
                End If
                isRemove = True
            Next
            If data_Emp_Count = tempCount Then
                checkAll = True
                For g As Integer = 0 To data_Emp_Count - 1 Step 1
                    tempDatas.ImportRow(data_Emp.Rows(g))
                Next
            End If
            '重新抓次數
            data_Emp_Count = data_Emp.Rows.Count

            Dim newScreenData As DataTable = _
                (From cus In tempDatas.AsEnumerable() _
                 Select cus _
                 Order By _
                 cus.Field(Of String)("EmpID"), _
                 cus.Field(Of String)("Date"), _
                 cus.Field(Of String)("Time") _
                 ).CopyToDataTable()

            Dim newOriData As DataTable = data_Declaration.Clone
            '內部資料比對
            If checkAll And data_Emp_Count = data_Declaration_Count Then
                'ViewState.Item("data_ResetEmp") = Nothing
            Else
                'If data_Emp_Count = gvMain.Rows.Count Then
                '    ViewState.Item("data_ResetEmp") = Nothing
                'Else
                '    ViewState.Item("data_ResetEmp") = newScreenData
                'End If


                For i As Integer = data_Declaration_Count - 1 To 0 Step -1
                    Dim isRemove = True
                    For j As Integer = 0 To data_Emp_Count - 1 Step 1
                        If data_Emp.Rows(j).Item("TxnID") = data_Declaration.Rows(i).Item("OTTxnID") And data_Emp.Rows(j).Item("SeqNo") = data_Declaration.Rows(i).Item("OTSeqNo").ToString Then
                            isRemove = False
                        End If
                    Next
                    If isRemove Then
                        newOriData.ImportRow(data_Declaration.Rows(i))
                        data_Declaration.Rows.RemoveAt(i)
                    End If
                    isRemove = True
                Next
                data_Declaration = _
                (From cus In data_Declaration.AsEnumerable() _
                 Select cus _
                 Order By _
                 cus.Field(Of String)("OTEmpID"), _
                 cus.Field(Of String)("OTStartDate"), _
                 cus.Field(Of String)("OTStartTime") _
                 ).CopyToDataTable()
                'ViewState.Item("data_OriEmp") = newOriData
            End If
        End If

        '從申報檔取出每一筆，開始計算時段
        Dim throwMsg As New StringBuilder
        Dim errorList As New StringBuilder
        ViewState.Item("throwMsg") = ""
        ViewState.Item("errorList") = ""
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
            Dim chkWord As String = "" '檢察使否有重複資料的Flag或是錯誤資料的Flag
            '先檢查舊資料(OverTime)，檢查是否有跨日過來的資料或是先前有的加班(以計薪)
            If checkDate <> startDate Then
                checkDate = startDate
                checkEmpID = empID
                chkWord = checkOldData(data_Period, compID, empID, startDate, startTime, endTime)
            ElseIf checkDate = startDate Then
                If checkEmpID <> empID Then
                    checkEmpID = empID
                    chkWord = checkOldData(data_Period, compID, empID, startDate, startTime, endTime)
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
                            chkWord = checkOldData(data_Period, compID, empID, startDate, startTime, endTime)
                        End If
                    Else
                        chkWord = checkOldData(data_Period, compID, empID, startDate, startTime, endTime)
                    End If
                    
                End If
            End If
            '計算時段
            If chkWord = "Correct" Then
                getTime_Interval(data_Period, compID, empID, startDate, endDate, startTime, endTime, salaryOrAdjust, seq, txnID, seqNo, deptID, organID, deptName, organName, mealFlag, mealTime, overTimeFlag, time3HR, holiDay)
            ElseIf chkWord = "Same" Then
                throwMsg.AppendLine("員工編號:" & empID & ",加班日期:" & startDate & "時間:" & startTime & "~" & endTime & ",資料已存在於OverTime")
            ElseIf chkWord = "Error" Then
                errorList.AppendLine("員工編號:" & empID & ",加班日期:" & startDate & "時間:" & startTime & "~" & endTime & ",資料有誤")
            End If
        Next

        If throwMsg.ToString <> "" Then
            ViewState.Item("throwMsg") = throwMsg.ToString
        End If
        If errorList.ToString <> "" Then
            ViewState.Item("errorList") = errorList.ToString
        End If
        'Table 查看用的
        ShowTable.Visible = False
        pcMain1.DataTable = data_Period
        gvMain1.DataBind()

        If data_Period.Rows.Count > 0 Then
            InsertDB(data_Period) '新增到資料庫
        Else
            If ViewState.Item("throwMsg").ToString <> "" Then
                Bsp.Utility.ShowMessage(Me, ViewState.Item("throwMsg"))
                Return
            End If
            If ViewState.Item("errorList").ToString <> "" Then
                Bsp.Utility.ShowMessage(Me, ViewState.Item("errorList"))
                Return
            End If
        End If
    End Sub
#End Region
#Region "檢核跨日單選取"
    Public Function chkToCross() As String
        Dim result As String = ""
        Dim errorDt As DataTable = New DataTable("errDataTable")
        errorDt.Columns.Add("chkTxnID", Type.GetType("System.String"))
        errorDt.Columns.Add("chkEmpID", Type.GetType("System.String"))
        errorDt.Columns.Add("chkSeqNo", Type.GetType("System.String"))
        errorDt.Columns.Add("alertDate", Type.GetType("System.String"))

        '畫面上的資料
        Dim data_Emp As DataTable = ViewState.Item("data_FixEmp")
        Dim data_Emp_Count As Integer = 0
        data_Emp_Count = data_Emp.Rows.Count

        Dim chkTxnID As String = ""
        Dim chkEmpID As String = ""
        Dim chkSeqNo As String = ""
        Dim alertDate As String = ""

        Dim isClick As Boolean = False
        Dim sameData As Boolean = False

        For j As Integer = 0 To gvMain.Rows.Count - 1 Step 1
            Dim objChk As CheckBox = gvMain.Rows(j).FindControl("chk_gvMain")

            Dim screenTxnID As String = gvMain.DataKeys(j).Item("TxnID").ToString
            Dim screenEmpID As String = gvMain.DataKeys(j).Item("EmpID").ToString
            Dim screenSeqNo As String = gvMain.DataKeys(j).Item("SeqNo").ToString

            If chkTxnID <> "" And chkEmpID <> "" And chkSeqNo <> "" Then
                If screenTxnID = chkTxnID And screenEmpID = chkEmpID And screenSeqNo = chkSeqNo Then
                    sameData = True
                Else
                    sameData = False
                End If
            End If

            If objChk.Checked Then
                If sameData Then
                    isClick = True
                    For i As Integer = 0 To errorDt.Rows.Count - 1 Step 1
                        If screenTxnID = errorDt.Rows(i).Item("chkTxnID").ToString And screenEmpID = errorDt.Rows(i).Item("chkEmpID").ToString And screenSeqNo = errorDt.Rows(i).Item("chkSeqNo").ToString Then
                            errorDt.Rows.RemoveAt(i)
                        End If
                    Next
                Else
                    For i As Integer = 0 To data_Emp_Count - 1 Step 1
                        If screenTxnID = data_Emp.Rows(i).Item("TxnID").ToString And screenEmpID = data_Emp.Rows(i).Item("EmpID").ToString And IIf(screenSeqNo.Equals("1"), "2", "1") = data_Emp.Rows(i).Item("SeqNo").ToString Then
                            Dim dataRow As DataRow
                            dataRow = errorDt.NewRow
                            chkTxnID = data_Emp.Rows(i).Item("TxnID").ToString : dataRow("chkTxnID") = chkTxnID
                            chkEmpID = data_Emp.Rows(i).Item("EmpID").ToString : dataRow("chkEmpID") = chkEmpID
                            chkSeqNo = data_Emp.Rows(i).Item("SeqNo").ToString : dataRow("chkSeqNo") = chkSeqNo
                            alertDate = data_Emp.Rows(i).Item("Date").ToString : dataRow("alertDate") = alertDate
                            errorDt.Rows.Add(dataRow)
                        End If
                    Next
                End If
            Else
                isClick = False
            End If
        Next

        If errorDt.Rows.Count <> 0 Then
            For i As Integer = 0 To errorDt.Rows.Count - 1 Step 1
                Dim errorMsg As New StringBuilder
                Dim EmpID As String = errorDt.Rows(i).Item("chkEmpID").ToString
                Dim AddDate As String = errorDt.Rows(i).Item("alertDate").ToString
                Dim Seq As String = errorDt.Rows(i).Item("chkSeqNo").ToString
                errorMsg.AppendLine("員工編號 : " & EmpID & " ,加班日期:" & AddDate & " 的第" & Seq & "單號尚未選取(跨日單)")
                result = result & errorMsg.ToString
            Next
        End If

        Return result.Trim
    End Function
#End Region
#Region "畫面清值"
    Public Sub doClear(ByVal Flag As Boolean)
        If Flag Then
            ddlPersonOrDate.Text = ""
        End If
        trEmp1.Visible = False
        trEmp2.Visible = False
        'trStay1.Visible = False
        'trStay2.Visible = False
        trDate.Visible = False
        PersonTable.Visible = False
        pcMain.DataTable = Nothing
        gvMain.DataBind()

        ShowTable.Visible = False
        pcMain1.DataTable = Nothing
        gvMain1.DataBind()

        txtEmpID.Text = ""
        lblEmpID.Text = ""
        ucDate.DateText = ""

        ViewState.Item("isQuery") = Nothing
        ViewState.Item("data_OriEmp") = Nothing
        ViewState.Item("data_FixEmp") = Nothing
        'ucStayDate.DateText = ""
        'txtPayDate.Text = ""
    End Sub
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
            If ETime = "23:59" Then                                   '假如是跨日單，需要加一分鐘回去'跨日單判定??
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
            dataRows("SalaryPaid") = overTimeFlag
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
    Public Function checkOldData(ByRef Table_Time As DataTable, ByVal compID As String, ByVal empID As String, ByVal sDate As String, ByVal bTime As String, ByVal aTime As String) As String
        Dim result As String = "Correct"
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
        If checkData_Count <> 0 Then
            Dim chkSame As Boolean = True
            For chk As Integer = 0 To checkData_Count - 1 Step 1
                Dim STime As String = checkData.Rows(chk).Item("BeginTime").ToString
                Dim ETime As String = checkData.Rows(chk).Item("EndTime").ToString
                If STime = bTime.Replace(":", "") And ETime = aTime.Replace(":", "") Then
                    chkSame = False
                End If
            Next

            Try
                If chkSame Then
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
                        Dim Throwtime3HR As Double = CDbl(IIf(newRowData.Item("OTHoliday1").ToString.Equals(""), "0", newRowData.Item("OTHoliday1").ToString))
                        Dim ThrowholiDay As String = IIf(newRowData.Item("HolidayOrNot").ToString = "0", "T", "F")

                        getTime_Interval(Table_Time, ThrowcompID, ThrowempID, ThrowstartDate, ThrowendDate, ThrowstartTime, ThrowendTime, ThrowsalaryOrAdjust, Throwseq, ThrowtxnID, ThrowseqNo, ThrowdeptID, ThroworganID, ThrowdeptName, ThroworganName, ThrowmealFlag, ThrowmealTime, ThrowoverTimeFlag, Throwtime3HR, ThrowholiDay)
                    Next
                Else
                    result = "Same"
                End If
            Catch ex As Exception
                result = "Error"
            End Try


        End If
        Return result
    End Function
#End Region
#Region "假日加班 4、8、12小時計算"
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
            Dim oripaid As String = dataTable.Rows(i).Item("SalaryPaid").ToString
            If oricomp = chkcompID And oriemp = chkempID And oridate = chkDate And oridaytype = "F" And oripaid.Equals("0") Then
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
                    ElseIf HTime > 480 Then
                        result = 720 - ovTimeH
                    End If
                End If
            Else
                If HTime <= 240 Then
                    result = 240
                ElseIf HTime > 240 And HTime <= 480 Then
                    result = 480
                ElseIf HTime > 480 Then
                    result = 720
                End If
            End If
            
        Else
            result = 0.0
        End If

        result = CDbl(FormatNumber((result / 60), 1))
        Return result
    End Function
#End Region
#Region "新增到資料庫"
    Public Sub InsertDB(ByVal dataTable As DataTable)
        Dim strSQL_Insert As New StringBuilder
        Dim successFlag As Boolean = False
        Dim InsertCount As Integer = 0
        Dim checkEmp As String = ""
        Dim checkHDate As String = ""
        For i As Integer = 0 To dataTable.Rows.Count - 1 Step 1
            If "0" = dataTable.Rows(i).Item("SalaryPaid").ToString And "1" = dataTable.Rows(i).Item("SalaryOrAdjust").ToString Then
                Dim chkcomp As String = dataTable.Rows(i).Item("CompID").ToString
                Dim chkemp As String = dataTable.Rows(i).Item("EmpID").ToString
                Dim chkdate As String = dataTable.Rows(i).Item("Date").ToString
                Dim chktxnID As String = dataTable.Rows(i).Item("TxnID").ToString
                Dim chkdaytype As String = dataTable.Rows(i).Item("HolidayOrNot").ToString
                Dim chktime3 As Double = dataTable.Rows(i).Item("TimeMin_acc").ToString
                Dim holidayTime As Double = 0.0
                If checkEmp <> chkemp Then
                    checkEmp = chkemp
                    checkHDate = ""
                End If
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


                strSQL_Insert.Append("Insert into " & eHRMSDB_ITRD & ".[dbo].[OverTime] (CompID,EmpID,OTDate,Seq,BeginTime,EndTime,OTNormal,OTOver,OTHoliday,OTHoliday1,SPHSC1Holiday1," &
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
                strSQL_Insert.Append(Bsp.Utility.Quote(dataTable.Rows(i).Item("SalaryPaid")) & ",")
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
        Dim strSQL_Update As New StringBuilder
        For i As Integer = 0 To dataTable.Rows.Count - 1 Step 1
            If "0" = dataTable.Rows(i).Item("SalaryPaid").ToString Then
                strSQL_Update.Append("Update OverTimeDeclaration ")
                strSQL_Update.Append("set ToOverTimeDate = getDate(),")
                strSQL_Update.Append("ToOverTimeFlag = '1' ")
                'strSQL_Update.Append("ToOverTimeFlag = '1' ,")
                'strSQL_Update.Append("LastChgComp = " & Bsp.Utility.Quote(UserProfile.ActCompID) & ",") '20170222 Update by John
                'strSQL_Update.Append("LastChgID = " & Bsp.Utility.Quote(UserProfile.ActUserID) & ",") '20170222 Update by John
                'strSQL_Update.Append("LastChgDate = getDate() ") '20170222 Update by John
                strSQL_Update.Append("where OTCompID = " & Bsp.Utility.Quote(dataTable.Rows(i).Item("CompID")))
                strSQL_Update.Append(" And OTEmpID = " & Bsp.Utility.Quote(dataTable.Rows(i).Item("EmpID")))
                strSQL_Update.Append(" And OTStartDate = " & Bsp.Utility.Quote(dataTable.Rows(i).Item("Date")))
                strSQL_Update.Append(" And OTTxnID = " & Bsp.Utility.Quote(dataTable.Rows(i).Item("TxnID")))
                strSQL_Update.Append(" And OTSeqNo = " & Bsp.Utility.Quote(dataTable.Rows(i).Item("SeqNo")))
                strSQL_Update.Append(";")
            End If
        Next
        Using cn As DbConnection = Bsp.DB.getConnection("AattendantDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Dim errorMsg = ""
            Try
                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL_Insert.ToString() & strSQL_Update.ToString, tran, "AattendantDB")
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

        If successFlag Then
            Bsp.Utility.ShowMessage(Me, "拋轉成功")
            If ViewState.Item("throwMsg").ToString <> "" Then
                Bsp.Utility.ShowMessage(Me, ViewState.Item("throwMsg"))
            End If
            If ViewState.Item("errorList").ToString <> "" Then
                Bsp.Utility.ShowMessage(Me, ViewState.Item("errorList"))
            End If

            Dim choseType As String = ViewState.Item("ChoseType").ToString '下拉選單選擇的拋轉條件
            If choseType = "PersonBlock" Then
                resetGrid()
            End If

        End If
    End Sub
#End Region
#Region "員工編號UC元件"
    Public Sub initArgs()
        '員工編號
        ucQueryEmp.ShowCompRole = "False"
        ucQueryEmp.InValidFlag = "N"
        ucQueryEmp.SelectCompID = UserProfile.SelectCompRoleID
        ViewState.Item("isQuery") = False
    End Sub
    Public Overrides Sub DoModalReturn(ByVal returnValue As String)
        Dim strSql As String = ""

        If returnValue <> "" Then
            Dim aryData() As String = returnValue.Split(":")

            Select Case aryData(0)
                Case "ucQueryEmp"
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    '員工編號
                    txtEmpID.Text = aryValue(1)
                    lblEmpID.Text = aryValue(2)
            End Select
        End If
    End Sub
    Protected Sub txtEmpID_TextChanged(sender As Object, e As System.EventArgs) Handles txtEmpID.TextChanged
        If txtEmpID.Text <> "" And txtEmpID.Text.Length = 6 Then
            Dim objHR As New HR
            Dim rtnTable As DataTable = objHR.GetHREmpName(UserProfile.SelectCompRoleID, txtEmpID.Text)
            If rtnTable.Rows.Count <= 0 Then
                lblEmpID.Text = ""
                txtEmpID.Text = ""
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", "人事資料尚未建檔")
            Else
                lblEmpID.Text = rtnTable.Rows(0).Item(0)
            End If
        Else
            lblEmpID.Text = ""
        End If
    End Sub
#End Region
#Region "人員查詢-拋轉畫面清值"
    Public Sub resetGrid()
        ''Table顯示
        'PersonTable.Visible = True
        'pcMain.DataTable = ViewState.Item("data_ResetEmp")
        'gvMain.DataBind()
        doQuery("T")
        For i As Integer = 0 To gvMain.Rows.Count - 1 Step 1
            Dim objChk As CheckBox = gvMain.Rows(i).FindControl("chk_gvMain")
            If objChk.Checked Then
                objChk.Checked = False
            End If
        Next
        ScriptManager.RegisterStartupScript(Me, Page.GetType, "script", "gridClear();", True)
    End Sub
#End Region
#Region "方法"
    Private Function StrF(ByVal ob As Object) As String
        Dim result = ""
        If Not ob Is Nothing Then
            If Not String.IsNullOrEmpty(ob.ToString()) Then
                result = ob.ToString()
            End If
        End If
        Return result
    End Function
    Private Function DateStrF(ByVal dateStr As String) As String
        Dim result = ""
        If Not dateStr Is Nothing Then
            If dateStr.Replace("/", "").Replace("_", "").Trim = "" Then
                result = ""
            Else
                result = dateStr.ToString
            End If
        End If
        Return result
    End Function
#End Region

    '#Region "留守檢核"
    '    Public Function getStayInterval(ByVal AddDate As String, ByVal AddSTime As String, ByVal AddETime As String) As Array
    '        Dim result(2) As String
    '        Dim strSQL_Stay As New StringBuilder
    '        Dim data_Stay As DataTable
    '        Dim data_Stay_Count As Integer
    '        Dim isNArea As Boolean = True
    '        Dim isNEmp As Boolean = True

    '        '待完成，條件尚未設定
    '        strSQL_Stay.Append("select ")
    '        strSQL_Stay.Append("where ")

    '        data_Stay = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL_Stay.ToString(), "AattendantDB").Tables(0)
    '        data_Stay_Count = data_Stay.Rows.Count

    '        '代表有找到相符合的資料
    '        If data_Stay_Count <> 0 Then
    '            '待處理
    '            isNArea = False
    '        End If

    '        If isNArea Then
    '            strSQL_Stay.Clear()
    '            data_Stay.Clear()

    '            strSQL_Stay.Append("select ")
    '            strSQL_Stay.Append("where ")

    '            data_Stay = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL_Stay.ToString(), "AattendantDB").Tables(0)
    '            data_Stay_Count = data_Stay.Rows.Count

    '            If data_Stay_Count <> 0 Then
    '                '待處理
    '            Else
    '                isNEmp = False
    '            End If

    '        End If



    '        Return result
    '    End Function
    '#End Region

End Class
