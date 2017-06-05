Imports System.Data
Imports System.Data.Common

Partial Class OV_OV1200

    Inherits PageBase
    Private Property Config_TableName As String
        Get
            Dim result As String = OVBusinessCommon.AattendantDBFlowOpenLog
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
    '存取傳到明細頁的資料
    Private Property SelectDatas As DataTable
        Get
            Return Session.Item("SelectDatas")
        End Get
        Set(ByVal value As DataTable)
            Session.Item("SelectDatas") = value
        End Set
    End Property
    '存取查詢資料
    Private Property QueryDatas As DataTable
        Get
            Return ViewState.Item("QueryDatas")
        End Get
        Set(ByVal value As DataTable)
            ViewState.Item("QueryDatas") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            initScreen()
        End If
    End Sub
    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            initScreen()
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

            For Each strKey As String In ht.Keys
                Dim ctl As Control = Me.FindControl(strKey)
                If TypeOf ctl Is TextBox Then
                    CType(ctl, TextBox).Text = ht(strKey).ToString()
                ElseIf TypeOf ctl Is DropDownList Then
                    If ht(strKey).ToString <> "" Then
                        CType(ctl, DropDownList).SelectedValue = ht(strKey).ToString
                    End If
                ElseIf TypeOf ctl Is CheckBox Then
                    CType(ctl, CheckBox).Checked = ht(strKey)
                Else
                    If TypeOf ctl Is UserControl Then
                        ucAppSDate.DateText = ht("ucAppSDate").ToString().Replace("-", "/")
                        ucAppEDate.DateText = ht("ucAppEDate").ToString().Replace("-", "/")
                        ucAddSDate.DateText = ht("ucAddSDate").ToString().Replace("-", "/")
                        ucAddEDate.DateText = ht("ucAddEDate").ToString().Replace("-", "/")
                    End If
                End If
            Next

            If ht.ContainsKey("PageNo") Then pcMain.PageNo = Convert.ToInt32(ht("PageNo"))
            If ht.ContainsKey("DoQuery") Then
                If ht("DoQuery").ToString() = "Y" Then
                    ViewState.Item("DoQuery") = "Y"
                    doQuery()
                End If
            End If
        End If
    End Sub
    Public Sub initScreen()
        SelectDatas = Nothing
        lblCompID.Text = UserProfile.SelectCompRoleName
        initEmpArgs()
    End Sub
    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnQuery"     '查詢
                doQuery()
            Case "btnUpdate"    '流程改派
                doProcess()
            Case "btnActionX"   '清除
                doClear()
        End Select
    End Sub
#Region "員工編號 UC元件"
    Public Sub initEmpArgs()
        '員工編號
        ucQueryEmp1.ShowCompRole = "False"
        ucQueryEmp1.InValidFlag = "N"
        ucQueryEmp1.SelectCompID = UserProfile.SelectCompRoleID
        ucQueryEmp2.ShowCompRole = "False"
        ucQueryEmp2.InValidFlag = "N"
        ucQueryEmp2.SelectCompID = UserProfile.SelectCompRoleID
    End Sub
    Public Overrides Sub DoModalReturn(ByVal returnValue As String)
        Dim strSql As String = ""

        If returnValue <> "" Then
            Dim aryData() As String = returnValue.Split(":")

            Select Case aryData(0)
                Case "ucQueryEmp1"
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    '員工編號
                    txtFormEmpID.Text = aryValue(1)
                    lblFormEmpID.Text = aryValue(2)
                Case "ucQueryEmp2"
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    '員工編號
                    txtAdjustEmpID.Text = aryValue(1)
                    lblAdjustEmpID.Text = aryValue(2)
            End Select
        End If
    End Sub
#End Region
#Region "虛擬Table-For 查詢條件使用"
    Public Function getTable() As DataTable
        Dim myTable As New DataTable
        Dim col As DataColumn
        '表單名稱
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "FormName"
        myTable.Columns.Add(col)
        '公司代碼
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "CompID"
        myTable.Columns.Add(col)
        '加班人
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "AddEmpID"
        myTable.Columns.Add(col)
        '加班人姓名
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "AddEmpName"
        myTable.Columns.Add(col)
        '目前處理人員
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "AdjustEmpID"
        myTable.Columns.Add(col)
        'FlowCaseID 
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "FlowCaseID"
        myTable.Columns.Add(col)
        '目前處理人員姓名
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "AdjustEmpName"
        myTable.Columns.Add(col)
        '表單申請日期
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "AddDate"
        myTable.Columns.Add(col)
        '加班開始日期
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "StartDate"
        myTable.Columns.Add(col)
        '加班結束日期
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "EndDate"
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
        '最後異動公司
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "LastChgComp"
        myTable.Columns.Add(col)
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "LastChgCompName"
        myTable.Columns.Add(col)
        '最後異動員工
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "LastChgID"
        myTable.Columns.Add(col)
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "LastChgName"
        myTable.Columns.Add(col)
        '最後異動時間
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "LastChgDate"
        myTable.Columns.Add(col)


        Return myTable
    End Function
#End Region
    Public Sub doQuery()
        Dim formName As String = StrF(ddlFormName.SelectedValue)
        Dim formEmpID As String = StrF(txtFormEmpID.Text)
        Dim adjustEmpID As String = StrF(txtAdjustEmpID.Text)
        Dim appSDate As String = DateStrF(ucAppSDate.DateText)
        Dim appEDate As String = DateStrF(ucAppEDate.DateText)
        Dim addSDate As String = DateStrF(ucAddSDate.DateText)
        Dim addEDate As String = DateStrF(ucAddEDate.DateText)
        Dim showView As DataTable = getTable()
        Dim tempData As New DataTable
        Dim tableName As String = ""

        If formName <> "" Then
            Select Case formName
                Case "ddlAfter"     '
                    tableName = "OverTimeAdvance"
                Case "ddlBefore"    '
                    tableName = "OverTimeDeclaration"
            End Select

            tempData = getDBData(tableName, formEmpID, adjustEmpID, appSDate, appEDate, addSDate, addEDate)
            setTable(showView, tempData, tableName)
        Else
            Dim tempData2 As New DataTable
            tempData = getDBData("OverTimeAdvance", formEmpID, adjustEmpID, appSDate, appEDate, addSDate, addEDate)
            setTable(showView, tempData, "OverTimeAdvance")
            tempData2 = getDBData("OverTimeDeclaration", formEmpID, adjustEmpID, appSDate, appEDate, addSDate, addEDate)
            setTable(showView, tempData2, "OverTimeDeclaration")

        End If
        For i As Integer = 0 To showView.Rows.Count - 1 Step 1
            Dim comp As String = showView.Rows(i).Item("LastChgComp").ToString
            Dim emp As String = showView.Rows(i).Item("LastChgID").ToString
            showView.Rows(i).Item("LastChgCompName") = getCompany(comp)
            showView.Rows(i).Item("LastChgName") = getName(comp, emp)
        Next
        ViewState.Item("DoQuery") = "Y"
        'Table 查看用的
        QueryDatas = showView
        'ShowTable.Visible = True
        pcMain.DataTable = showView
        gvMain.DataBind()
        gvMain.Visible = True
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
    Private Sub setTable(ByRef dataTable As DataTable, ByVal tempData As DataTable, ByVal tableName As String)
        Dim title As String = ""
        If tableName = "OverTimeAdvance" Then
            title = "A-	加班事前申請"
        ElseIf tableName = "OverTimeDeclaration" Then
            title = "D- 加班事後申報"
        End If
        For i As Integer = 0 To tempData.Rows.Count - 1 Step 1
            Dim dataRows As DataRow
            dataRows = dataTable.NewRow
            dataRows("FormName") = title
            dataRows("CompID") = tempData.Rows(i).Item("OTCompID").ToString
            dataRows("AddEmpID") = tempData.Rows(i).Item("OTEmpID").ToString
            dataRows("AddEmpName") = tempData.Rows(i).Item("Name").ToString
            dataRows("AdjustEmpID") = tempData.Rows(i).Item("AssignTo").ToString
            dataRows("AdjustEmpName") = tempData.Rows(i).Item("Name2").ToString
            dataRows("FlowCaseID") = tempData.Rows(i).Item("FlowCaseID").ToString
            dataRows("AddDate") = tempData.Rows(i).Item("OTRegisterDate").ToString
            dataRows("StartDate") = tempData.Rows(i).Item("OTStartDate").ToString
            dataRows("EndDate") = tempData.Rows(i).Item("OTEndDate").ToString
            dataRows("StartTime") = tempData.Rows(i).Item("OTStartTime").ToString.Insert(2, ":")
            dataRows("EndTime") = tempData.Rows(i).Item("OTEndTime").ToString.Insert(2, ":")
            dataRows("LastChgComp") = tempData.Rows(i).Item("LastChgComp").ToString
            dataRows("LastChgID") = tempData.Rows(i).Item("LastChgID").ToString
            dataRows("LastChgDate") = tempData.Rows(i).Item("LastChgDate").ToString
            dataTable.Rows.Add(dataRows)
        Next
    End Sub
    Private Function getDBData(ByVal tableName As String, ByVal formEmpID As String, ByVal adjustEmpID As String, ByVal appSDate As String, ByVal appEDate As String, ByVal addSDate As String, ByVal addEDate As String) As DataTable
        Dim dataTable As New DataTable

        Dim strSQL As New StringBuilder()
        'Dim OVCommon = New OVBusinessCommon
        'Dim DBName As String = OVBusinessCommon.AattendantDBFlowOpenLog

        strSQL.AppendLine("select OT.OTCompID,OT.OTEmpID,OT.FlowCaseID,REPLACE(CONVERT(NVARCHAR (19),OT.OTRegisterDate,120),'-','/') AS OTRegisterDate,OT.OTStartDate,OT.OTStartTime,OT.LastChgComp,OT.LastChgID,REPLACE(CONVERT(NVARCHAR (19),OT.LastChgDate,120),'-','/') AS LastChgDate ,")
        strSQL.AppendLine("IIF(OT2.OTSeqNo IS NULL, OT.OTEndDate, OT2.OTEndDate) As OTEndDate,")
        strSQL.AppendLine("IIF(OT2.OTSeqNo IS NULL, OT.OTEndTime, OT2.OTEndTime) As OTEndTime,")
        strSQL.AppendLine("P.Name,")
        strSQL.AppendLine("D.AssignTo,")
        strSQL.AppendLine("Pl.Name As Name2")
        strSQL.AppendLine("FROM " & tableName & " AS OT ")
        strSQL.AppendLine("LEFT JOIN " & tableName & " OT2")
        strSQL.AppendLine("ON OT.OTCompID = OT2.OTCompID AND OT.OTEmpID = OT2.OTEmpID AND OT.OTTxnID = OT2.OTTxnID AND OT2.OTSeqNo = '2'")
        strSQL.AppendLine("LEFT JOIN " & eHRMSDB_ITRD & ".[dbo].[Personal] AS P ") 'Join Personal
        strSQL.AppendLine("ON OT.OTCompID = P.CompID AND OT.OTEmpID = P.EmpID ")
        strSQL.AppendLine("inner JOIN " & Config_TableName & " AS D ")
        strSQL.AppendLine("ON OT.FlowCaseID = D.FlowCaseID ")
        strSQL.AppendLine("LEFT JOIN " & eHRMSDB_ITRD & ".[dbo].[Personal] AS Pl ") 'Join Personal
        strSQL.AppendLine("ON D.AssignTo = Pl.EmpID AND Pl.CompID = (SELECT TOP 1 SignIDComp FROM HROverTimeLog AS HRL WHERE HRL.FlowCaseID = OT.FlowCaseID AND HRL.SignID = D.AssignTo ORDER BY HRL.Seq DESC) ")
        strSQL.AppendLine("where 1=1 ")
        strSQL.AppendLine(" And OT.OTCompID = " & Bsp.Utility.Quote(UserProfile.SelectCompRoleID))
        strSQL.AppendLine(" And OT.OTStatus = '2'")
        strSQL.AppendLine(" And OT.OTSeqNo = '1'")
        If formEmpID <> "" Then
            strSQL.AppendLine(" And OT.OTEmpID = " & Bsp.Utility.Quote(formEmpID))
        End If
        If adjustEmpID <> "" Then
            strSQL.AppendLine(" And D.AssignTo = " & Bsp.Utility.Quote(adjustEmpID))
        End If
        If appSDate <> "" Then
            strSQL.AppendLine(" And CONVERT(VARCHAR,OT.OTRegisterDate,111) >= " & Bsp.Utility.Quote(appSDate))
        End If
        If appEDate <> "" Then
            strSQL.AppendLine(" And CONVERT(VARCHAR,OT.OTRegisterDate,111) <= " & Bsp.Utility.Quote(appEDate))
        End If
        If addSDate <> "" Then
            strSQL.AppendLine(" And OT.OTStartDate >= " & Bsp.Utility.Quote(addSDate))
        End If
        If addEDate <> "" Then
            strSQL.AppendLine(" And OT.OTEndDate <= " & Bsp.Utility.Quote(addEDate))
        End If
        strSQL.Append(" order by OT.OTEmpID,OT.OTStartDate,OT.OTStartTime")
        dataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "AattendantDB").Tables(0)

        Return dataTable
    End Function
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

    Public Sub doProcess()
        If ViewState.Item("DoQuery") = "" Then
            Bsp.Utility.ShowMessage(Me, "請先查詢資料")
            Return
        End If
        '如果沒有選取資料的檢核處理-----------------------------------------<<<<
        Dim check_chb As Boolean = False
        For i As Integer = 0 To gvMain.Rows.Count - 1 Step 1
            Dim objChk As RadioButton = gvMain.Rows(i).FindControl("rdo_gvMain")
            If objChk.Checked Then
                check_chb = True
            End If
        Next
        If check_chb <> True Then
            Bsp.Utility.ShowMessage(Me, "請先選取欲改派資料")
            Return
        End If

        Dim newData As DataTable = getTable()
        For i As Integer = 0 To gvMain.Rows.Count - 1 Step 1
            Dim objChk As RadioButton = gvMain.Rows(i).FindControl("rdo_gvMain")
            If objChk.Checked Then
                Dim FormName As String = gvMain.DataKeys(i)("FormName").ToString
                Dim AddEmpName As String = gvMain.DataKeys(i)("AddEmpName").ToString
                Dim AdjustEmpName As String = gvMain.DataKeys(i)("AdjustEmpName").ToString
                Dim StartDate As String = gvMain.DataKeys(i)("StartDate").ToString
                Dim StartTime As String = gvMain.DataKeys(i)("StartTime").ToString
                For j As Integer = 0 To QueryDatas.Rows.Count - 1 Step 1
                    Dim query = QueryDatas.Rows(j)
                    If FormName = query.Item("FormName") And AddEmpName = query.Item("AddEmpName") And AdjustEmpName = query.Item("AdjustEmpName") And StartDate = query.Item("StartDate") And StartTime = query.Item("StartTime") Then
                        newData.ImportRow(query)
                    End If
                Next
            End If
        Next

        SelectDatas = newData

        Dim btnD As New ButtonState(ButtonState.emButtonType.Delete)
        btnD.Caption = "確定"
        Dim btnC As New ButtonState(ButtonState.emButtonType.Cancel)
        btnC.Caption = "清除"
        Dim btnR As New ButtonState(ButtonState.emButtonType.Reject)
        btnR.Caption = "返回"
        Me.TransferFramePage("~/OV/OV1201.aspx", New ButtonState() {btnD, btnR, btnC}, _
                        ddlFormName.ID & "=" & ddlFormName.SelectedValue, _
                        txtFormEmpID.ID & "=" & txtFormEmpID.Text, _
                        txtAdjustEmpID.ID & "=" & txtAdjustEmpID.Text, _
                        ucAppSDate.ID & "=" & ucAppSDate.DateText, _
                        ucAppEDate.ID & "=" & ucAppEDate.DateText, _
                        ucAddSDate.ID & "=" & ucAddSDate.DateText, _
                        ucAddEDate.ID & "=" & ucAddEDate.DateText, _
                        "PageNo=" & pcMain.PageNo.ToString(), _
                        "DoQuery=" & "Y")
    End Sub

    Private Sub doClear()
        'ShowTable.Visible = False
        gvMain.Visible = False
        ViewState.Item("DoQuery") = ""
        ddlFormName.SelectedValue = ""
        txtFormEmpID.Text = ""
        lblFormEmpID.Text = ""
        txtAdjustEmpID.Text = ""
        lblAdjustEmpID.Text = ""
        ucAppSDate.DateText = ""
        ucAppEDate.DateText = ""
        ucAddSDate.DateText = ""
        ucAddEDate.DateText = ""
    End Sub

    Protected Sub txtFormEmpID_TextChanged(sender As Object, e As System.EventArgs) Handles txtFormEmpID.TextChanged
        If txtFormEmpID.Text <> "" And txtFormEmpID.Text.Length = 6 Then
            Dim objHR As New HR
            Dim rtnTable As DataTable = objHR.GetHREmpName(UserProfile.SelectCompRoleID, txtFormEmpID.Text)
            If rtnTable.Rows.Count <= 0 Then
                lblFormEmpID.Text = ""
                txtFormEmpID.Text = ""
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", "人事資料尚未建檔")
            Else
                lblFormEmpID.Text = rtnTable.Rows(0).Item(0)
            End If
        Else
            lblFormEmpID.Text = ""
        End If
    End Sub

    Protected Sub txtAdjustEmpID_TextChanged(sender As Object, e As System.EventArgs) Handles txtAdjustEmpID.TextChanged
        If txtAdjustEmpID.Text <> "" And txtAdjustEmpID.Text.Length = 6 Then
            Dim objHR As New HR
            Dim rtnTable As DataTable = objHR.GetHREmpName(UserProfile.SelectCompRoleID, txtAdjustEmpID.Text)
            If rtnTable.Rows.Count <= 0 Then
                lblAdjustEmpID.Text = ""
                txtAdjustEmpID.Text = ""
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", "人事資料尚未建檔")
            Else
                lblAdjustEmpID.Text = rtnTable.Rows(0).Item(0)
            End If
        Else
            lblAdjustEmpID.Text = ""
        End If
    End Sub
End Class
