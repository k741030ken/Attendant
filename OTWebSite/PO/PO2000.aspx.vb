Imports Newtonsoft.Json
Imports System.Data

Partial Class PO_PO2000
    Inherits PageBase

#Region "1. 全域變數"
    ''' <summary>
    ''' _AllowOrgan
    ''' </summary>
    Private Property _AllowOrgan As List(Of OrganListModel) '全域private變數要為('_'+'小駝峰')
        Get
            Try
                If ViewState("_AllowOrgan") IsNot Nothing Then 'ViewState當頁暫存使用
                    Return JsonConvert.DeserializeObject(Of List(Of OrganListModel))(ViewState("_AllowOrgan").ToString())
                Else
                    Return Nothing
                End If
            Catch
                Return Nothing
            End Try
        End Get
        Set(value As List(Of OrganListModel))
            ViewState("_AllowOrgan") = JsonConvert.SerializeObject(value)
        End Set
    End Property

    ''' <summary>
    ''' _AllowFlowOrgan
    ''' </summary>
    Private Property _AllowFlowOrgan As List(Of FlowOrganListModel) '全域private變數要為('_'+'小駝峰')
        Get
            Try
                If ViewState("_AllowFlowOrgan") IsNot Nothing Then 'ViewState當頁暫存使用
                    Return JsonConvert.DeserializeObject(Of List(Of FlowOrganListModel))(ViewState("_AllowFlowOrgan").ToString())
                Else
                    Return Nothing
                End If
            Catch
                Return Nothing
            End Try
        End Get
        Set(value As List(Of FlowOrganListModel))
            ViewState("_AllowFlowOrgan") = JsonConvert.SerializeObject(value)
        End Set
    End Property

    ''' <summary>
    ''' _GridDataTable
    ''' </summary>
    Private Property _GridDataTable As DataTable '全域private變數要為('_'+'小駝峰')
        Get
            Try
                If ViewState("_GridDataTable") IsNot Nothing Then 'ViewState當頁暫存使用
                    Return JsonConvert.DeserializeObject(Of DataTable)(ViewState("_GridDataTable").ToString())
                Else
                    Return Nothing
                End If
            Catch
                Return Nothing
            End Try
        End Get
        Set(value As DataTable)
            ViewState("_GridDataTable") = JsonConvert.SerializeObject(value)
        End Set
    End Property

    ''' <summary>
    ''' _QueryFlag
    ''' </summary>
    Private Property _QueryFlag As String '全域private變數要為('_'+'小駝峰')
        Get
            Try
                If ViewState("_QueryFlag") IsNot Nothing Then 'ViewState當頁暫存使用
                    Return JsonConvert.DeserializeObject(Of String)(ViewState("_QueryFlag").ToString())
                Else
                    Return Nothing
                End If
            Catch
                Return Nothing
            End Try
        End Get
        Set(value As String)
            ViewState("_QueryFlag") = JsonConvert.SerializeObject(value)
        End Set
    End Property
#End Region

#Region "2. 功能鍵處理邏輯"
    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnQuery"         '查詢
                beforeQueryCheck(Param)
            Case "btnDownload"      '匯出
                beforeQueryCheck(Param)
            Case "btnActionX"       '清除
                DoClear()
        End Select
    End Sub
#End Region

#Region "3. Override Method"

#End Region

#Region "4. Page_Load"

    ''' <summary>
    ''' 起始
    ''' </summary>
    ''' <param name="sender">object</param>
    ''' <param name="e">EventArgs</param>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            LoadData()
        End If
    End Sub
#End Region

#Region "5. 畫面事件"
    ''' <summary>
    ''' 員工編號欄位畫面事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub txtEmpID_TextChanged(sender As Object, e As System.EventArgs) Handles txtEmpID.TextChanged
        If txtEmpID.Text <> "" And txtEmpID.Text.Length = 6 Then
            Dim objHR As New HR
            Dim rtnTable As DataTable = objHR.GetHREmpName(UserProfile.SelectCompRoleID, txtEmpID.Text)
            If rtnTable.Rows.Count <= 0 Then
                txtEmpName.Text = ""
                txtEmpID.Text = ""
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", "人事資料尚未建檔")
            Else
                txtEmpName.Text = rtnTable.Rows(0).Item(0)
            End If
        Else
            txtEmpName.Text = ""
        End If
    End Sub

    ''' <summary>
    ''' 查詢類別連動事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ddlType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlType.SelectedIndexChanged
        Dim searchType As String = StringIIF(ddlType.SelectedValue)
        If "5".Equals(searchType) Then
            ddlConfirmPunchFlag.Enabled = False
            ddlConfirmStatus.Enabled = False
        Else
            ddlConfirmPunchFlag.Enabled = True
            ddlConfirmStatus.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' 組織別下拉選擇連動
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ddlOrganization_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlOrganization.SelectedIndexChanged
        If ddlOrganization.SelectedValue = "Organ" Then
            Organ.Visible = True
            FlowOrgan.Visible = False
            resetDDL(ddlOrgType)
            LoadOrgType()
            resetDDL(ddlDeptID)
            resetDDL(ddlOrganID)
        ElseIf ddlOrganization.SelectedValue = "FlowOrgan" Then
            FlowOrgan.Visible = True
            Organ.Visible = False
            resetDDL(ddlRoleCode40)
            LoadRoleCode40()
            resetDDL(ddlRoleCode30)
            resetDDL(ddlRoleCode20)
            resetDDL(ddlRoleCode10)
        End If
    End Sub

    ''' <summary>
    ''' ddlOrgType_Changed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ddlOrgType_Changed(sender As Object, e As System.EventArgs) Handles ddlOrgType.SelectedIndexChanged
        If ddlOrgType.SelectedValue = "" Then
            resetDDL(ddlDeptID)
            resetDDL(ddlOrganID)
        Else
            LoadDeptID()
        End If
    End Sub

    ''' <summary>
    ''' ddlDeptID_Changed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ddlDeptID_Changed(sender As Object, e As System.EventArgs) Handles ddlDeptID.SelectedIndexChanged
        If ddlDeptID.SelectedValue = "" Then
            resetDDL(ddlOrganID)
        Else
            LoadOrganID()
        End If
    End Sub

    ''' <summary>
    ''' ddlRoleCode40_Changed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ddlRoleCode40_Changed(sender As Object, e As System.EventArgs) Handles ddlRoleCode40.SelectedIndexChanged
        If ddlRoleCode40.SelectedValue <> "" Then
            LoadRoleCode30()
        Else
            resetDDL(ddlRoleCode30)
        End If
        resetDDL(ddlRoleCode20)
        resetDDL(ddlRoleCode10)
    End Sub

    ''' <summary>
    ''' ddlRoleCode30_Changed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ddlRoleCode30_Changed(sender As Object, e As System.EventArgs) Handles ddlRoleCode30.SelectedIndexChanged
        If ddlRoleCode30.SelectedValue <> "" Then
            LoadRoleCode20()
        Else
            resetDDL(ddlRoleCode20)
        End If
        resetDDL(ddlRoleCode10)
    End Sub

    ''' <summary>
    ''' ddlRoleCode20_Changed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ddlRoleCode20_Changed(sender As Object, e As System.EventArgs) Handles ddlRoleCode20.SelectedIndexChanged
        If ddlRoleCode20.SelectedValue <> "" Then
            LoadRoleCode10()
        Else
            resetDDL(ddlRoleCode10)
        End If
    End Sub
#End Region

#Region "6. 畫面檢核與確認"
    ''' <summary>
    ''' 日期檢核
    ''' </summary>
    ''' <param name="errorMsg">檢核失敗訊息</param>
    ''' <returns>bool</returns>
    Private Function DateValidation(ByRef errorMsg As String) As Boolean
        Dim result As Boolean = True
        Dim startFlag As Boolean = True
        errorMsg = ""

        Dim after5YDate = StringIIF(DateTime.Now.AddYears(5).ToString("yyyy/MM/dd"))
        Dim before5YDate = StringIIF(DateTime.Now.AddYears(-5).ToString("yyyy/MM/dd"))
        Dim startDate = StringIIF(ucBeginDate.DateText)
        Dim endDate = StringIIF(ucEndDate.DateText)

        If "".Equals(startDate) AndAlso "".Equals(endDate) Then
            errorMsg = "請輸入查詢日期!"
        ElseIf Not "".Equals(startDate) AndAlso "".Equals(endDate) Then
            errorMsg = "請輸入查詢日期(迄日)!"
        ElseIf "".Equals(startDate) AndAlso Not "".Equals(endDate) Then
            errorMsg = "請輸入查詢日期!(起日)"
        End If

        If startDate <> "" Then
            Dim afterChkDay As Integer = POCommon.GetTimeDiff(startDate, after5YDate, "Day")
            Dim beforeChkDay As Integer = POCommon.GetTimeDiff(startDate, before5YDate, "Day")
            If afterChkDay < 0 OrElse beforeChkDay > 0 Then
                startFlag = False
                errorMsg = "查詢日期限最近5年以內。"
            End If
        End If
        If startDate <> "" AndAlso endDate <> "" AndAlso startFlag Then
            Dim sDate = Int32.Parse(startDate.Replace("/", ""))
            Dim eDate = Int32.Parse(endDate.Replace("/", ""))
            If sDate > eDate Then
                errorMsg = "起日不得大於迄日"
            Else
                Dim dayMonth As String = Convert.ToDateTime(startDate).AddMonths(1).ToString("yyyy/MM/dd")
                Dim datMonthDiff As Integer = POCommon.GetTimeDiff(startDate, dayMonth, "Day")
                Dim dayDiff As Integer = POCommon.GetTimeDiff(startDate, endDate, "Day")
                If dayDiff > datMonthDiff Then
                    errorMsg = "查詢區間限最多1個月以內"
                End If
            End If
        End If
        If Not "".Equals(errorMsg) Then
            result = False
        End If
        Return result
    End Function
    ''' <summary>
    ''' 時間檢核
    ''' </summary>
    ''' <param name="errorMsg">檢核失敗訊息</param>
    ''' <returns>bool</returns>
    Private Function TimeValidation(ByRef errorMsg As String) As Boolean
        Dim result As Boolean = True
        errorMsg = ""
        If Not "".Equals(StartTimeH.SelectedValue) AndAlso Not "".Equals(StartTimeM.SelectedValue) AndAlso Not "".Equals(EndTimeH.SelectedValue) AndAlso Not "".Equals(EndTimeM.SelectedValue) Then
            Dim BeginTimeA = StringIIF(StartTimeH.SelectedValue)
            Dim BeginTimeB = StringIIF(StartTimeM.SelectedValue)
            Dim EndTimeA = StringIIF(EndTimeH.SelectedValue)
            Dim EndTimeB = StringIIF(EndTimeM.SelectedValue)

            Dim BTimeA = If(BeginTimeA.StartsWith("0"), Int32.Parse(BeginTimeA.Substring(1)), Int32.Parse(BeginTimeA))
            Dim BTimeB = If(BeginTimeB.StartsWith("0"), Int32.Parse(BeginTimeB.Substring(1)), Int32.Parse(BeginTimeB))
            Dim ETimeA = If(EndTimeA.StartsWith("0"), Int32.Parse(EndTimeA.Substring(1)), Int32.Parse(EndTimeA))
            Dim ETimeB = If(EndTimeB.StartsWith("0"), Int32.Parse(EndTimeB.Substring(1)), Int32.Parse(EndTimeB))

            Dim startDate As Integer = Int32.Parse(ucBeginDate.DateText.Replace("/", ""))
            Dim endDate As Integer = Int32.Parse(ucEndDate.DateText.Replace("/", ""))

            If startDate = endDate Then
                If BTimeA > ETimeA Then
                    errorMsg = "開始時間不可以晚於結束時間"
                ElseIf (BTimeA = ETimeA) Then
                    If BTimeB > ETimeB Then
                        errorMsg = "開始時間不可以晚於結束時間"
                    End If
                End If
            End If
        ElseIf Not "".Equals(StartTimeH.SelectedValue) AndAlso "".Equals(StartTimeM.SelectedValue) Then
            errorMsg = "請選擇開始時間(分鐘)"
        ElseIf "".Equals(StartTimeH.SelectedValue) AndAlso Not "".Equals(StartTimeM.SelectedValue) Then
            errorMsg = "請選擇開始時間(小時)"
        ElseIf Not "".Equals(EndTimeH.SelectedValue) AndAlso "".Equals(EndTimeM.SelectedValue) Then
            errorMsg = "請選擇結束時間(分鐘)"
        ElseIf "".Equals(EndTimeH.SelectedValue) AndAlso Not "".Equals(EndTimeM.SelectedValue) Then
            errorMsg = "請選擇結束時間(小時)"
        End If

        If Not "".Equals(errorMsg) Then
            result = False
        End If
        Return result
    End Function
    ''' <summary>
    ''' 查詢類別檢核(必輸)
    ''' </summary>
    ''' <param name="errorMsg"></param>
    ''' <returns></returns>
    Private Function SearchTypeValidation(ByRef errorMsg As String) As Boolean
        Dim result As Boolean = True
        errorMsg = ""
        If String.IsNullOrEmpty(StringIIF(ddlType.SelectedValue)) Then
            errorMsg = "請選擇查詢類別"
        End If
        If Not "".Equals(errorMsg) Then
            result = False
        End If
        Return result
    End Function

#End Region

#Region "7. private Method"
    ''' <summary>
    ''' 載入資料
    ''' </summary>
    Private Sub LoadData()
        initArgs()
        initTimeHM()
        lblCompID.Text = UserProfile.ActCompID & " " & UserProfile.ActCompName
        ucBeginDate.DateText = DateTime.Now.ToString("yyyy/MM/dd")
        ucEndDate.DateText = DateTime.Now.ToString("yyyy/MM/dd")
        FlowOrgan.Visible = False
        LoadOrgType()
        resetDDL(ddlDeptID)
        resetDDL(ddlOrganID)
        resetDDL(ddlRoleCode40)
        resetDDL(ddlRoleCode30)
        resetDDL(ddlRoleCode20)
        resetDDL(ddlRoleCode10)
    End Sub
    ''' <summary>
    ''' 查詢前的畫面檢核
    ''' </summary>
    Private Sub beforeQueryCheck(ByVal regStr As String)
        Dim errorMsg As String = ""
        Try
            Select Case regStr
                Case "btnQuery"
                    If Not DateValidation(errorMsg) Then
                        Throw New Exception(errorMsg)
                    End If
                    If Not TimeValidation(errorMsg) Then
                        Throw New Exception(errorMsg)
                    End If
                    If Not SearchTypeValidation(errorMsg) Then
                        Throw New Exception(errorMsg)
                    End If
                    DoQuery()
                Case "btnDownload"
                    If "Y".Equals(_QueryFlag) Then
                        DoDownload(_GridDataTable)
                    Else
                        errorMsg = "請先查詢資料!"
                        Throw New Exception(errorMsg)
                    End If

            End Select

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, errorMsg)
        End Try
    End Sub

    ''' <summary>
    ''' 查詢邏輯
    ''' </summary>
    Private Sub DoQuery()
        Dim isSuccess As Boolean = False
        Dim msg As String = ""
        Dim datas As List(Of PunchConfirmBean) = New List(Of PunchConfirmBean)()
        Dim searchOrgType As String = StringIIF(ddlOrganization.SelectedValue)
        Dim searchType As String = StringIIF(ddlType.SelectedValue)
        Dim viewData As PO2000Model = New PO2000Model() With _
        { _
            .CompID = StringIIF(UserProfile.SelectCompRoleID),
            .OrganID = StringIIF(GetOrganWhere()),
            .FlowOrganID = StringIIF(GetFlowOrganWhere()),
            .PunchSDate = StringIIF(ucBeginDate.DateText),
            .PunchEDate = StringIIF(ucEndDate.DateText),
            .PunchSTime = StringIIF(StartTimeH.SelectedValue & ":" & StartTimeM.SelectedValue),
            .PunchETime = StringIIF(EndTimeH.SelectedValue & ":" & EndTimeM.SelectedValue),
            .ConfirmPunchFlag = StringIIF(ddlConfirmPunchFlag.SelectedValue),
            .ConfirmStatus = StringIIF(ddlConfirmStatus.SelectedValue),
            .Remedy_AbnormalFlag = StringIIF(ddlRemedy_AbnormalFlag.SelectedValue),
            .EmpID = StringIIF(txtEmpID.Text),
            .EmpName = StringIIF(txtEmpName.Text)
        }
        If "5".Equals(searchType) Then
            isSuccess = PO2000.SelectPunchLog(viewData, searchOrgType, datas, msg)
        Else
            isSuccess = PO2000.SelectPunchConfirm(viewData, searchOrgType, searchType, datas, msg)
        End If

        If isSuccess And datas.Count > 0 Then
            viewData.PO2000GridDataList = PO2000.GridDataFormat(datas) 'Format Data   
            ShowTable.Visible = True
            pcMain.DataTable = PO2000.ConvertToDataTable(viewData.PO2000GridDataList)
            gvMain.DataBind()
            _GridDataTable = PO2000.ConvertToDataTable(viewData.PO2000GridDataList)
            _QueryFlag = "Y"
        Else
            If msg <> "" Then
                Bsp.Utility.ShowMessage(Me, msg)
            End If
        End If
    End Sub

    ''' <summary>
    ''' 匯出邏輯
    ''' </summary>
    ''' <param name="dataTable"></param>
    ''' <remarks></remarks>
    Public Sub DoDownload(ByVal dataTable As DataTable)
        Try
            '產出檔頭
            Dim strFileName As String = Bsp.Utility.GetNewFileName("打卡查詢--經管單位-") & ".xls"

            '動態產生GridView以便匯出成EXCEL
            Dim gvExport As GridView = New GridView()
            gvExport.AllowPaging = False
            gvExport.AllowSorting = False
            gvExport.FooterStyle.BackColor = Drawing.ColorTranslator.FromHtml("#99CCCC")
            gvExport.FooterStyle.ForeColor = Drawing.ColorTranslator.FromHtml("#003399")
            gvExport.RowStyle.CssClass = "tr_evenline"
            gvExport.AlternatingRowStyle.CssClass = "tr_oddline"
            gvExport.EmptyDataRowStyle.CssClass = "GridView_EmptyRowStyle"
            '複製datatable待處理
            Dim dt As DataTable = dataTable.Clone()
            '複製資料
            For index As Integer = 0 To dataTable.Rows.Count - 1
                dt.ImportRow(dataTable.Rows(index))
            Next
            '處理列印欄位
            For index As Integer = dataTable.Columns.Count - 1 To 0 Step -1
                Dim itemData = dt.Columns.Item(index)
                Dim isPrintColumn = False

                Select Case itemData.ColumnName
                    Case "AbnormalType"      '狀態
                        itemData.ColumnName = "狀態"
                        isPrintColumn = True
                    Case "PunchDate"              '打卡日期
                        itemData.ColumnName = "打卡日期"
                        isPrintColumn = True
                    Case "PunchTime"              '時間
                        itemData.ColumnName = "時間"
                        isPrintColumn = True
                    Case "ConfirmPunchFlag"  '類型
                        itemData.ColumnName = "類型"
                        isPrintColumn = True
                    Case "Source"            '來源
                        itemData.ColumnName = "來源"
                        isPrintColumn = True
                    Case "OrganName"         '打卡單位
                        itemData.ColumnName = "打卡單位"
                        isPrintColumn = True
                    Case "EmpID"                  '員工編號
                        itemData.ColumnName = "員工編號"
                        isPrintColumn = True
                    Case "EmpName"                '員工姓名
                        itemData.ColumnName = "員工姓名"
                        isPrintColumn = True
                    Case "Remedy_AbnormalReasonCN" '原因
                        itemData.ColumnName = "原因"
                        isPrintColumn = True
                    Case "RotateFlag"             '輪班人員
                        itemData.ColumnName = "輪班人員"
                        isPrintColumn = True
                End Select

                If Not isPrintColumn Then '移除不必要的欄位
                    dt.Columns.RemoveAt(index)
                End If
            Next
            dt.AcceptChanges()
            gvExport.DataSource = dt
            gvExport.DataBind()

            Response.ClearContent()
            Response.BufferOutput = True
            Response.Charset = "utf-8"
            Response.ContentType = "application/save-as"         '隱藏檔案網址路逕的下載
            Response.AddHeader("Content-Transfer-Encoding", "binary")
            Response.ContentEncoding = System.Text.Encoding.UTF8
            Response.AddHeader("content-disposition", "attachment; filename=" & Server.UrlPathEncode(strFileName))

            Dim oStringWriter As New System.IO.StringWriter()
            Dim oHtmlTextWriter As New System.Web.UI.HtmlTextWriter(oStringWriter)

            Response.Write("<meta http-equiv=Content-Type content=text/html charset=utf-8>")
            Dim style As String = "<style>td{font-size:9pt} a{font-size:9pt} tr{page-break-after: always}</style>"

            gvExport.Attributes.Add("style", "vnd.ms-excel.numberformat:@") '設定所有儲存格為文字格式
            gvExport.RenderControl(oHtmlTextWriter)
            Response.Write(style)
            Response.Write(oStringWriter.ToString())
            Response.End()
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoDownload", ex)
        End Try
    End Sub
    ''' <summary>
    ''' 清除邏輯
    ''' </summary>
    Private Sub DoClear()
        ddlOrganization.SelectedIndex = 0
        ddlOrganization_SelectedIndexChanged(Nothing, EventArgs.Empty)
        ucBeginDate.DateText = DateTime.Now.ToString("yyyy/MM/dd")
        ucEndDate.DateText = DateTime.Now.ToString("yyyy/MM/dd")
        StartTimeH.SelectedIndex = 0
        StartTimeM.SelectedIndex = 0
        EndTimeH.SelectedIndex = 0
        EndTimeM.SelectedIndex = 0
        ddlType.SelectedIndex = 0
        ddlConfirmPunchFlag.SelectedIndex = 0
        ddlConfirmStatus.SelectedIndex = 0
        ddlRemedy_AbnormalFlag.SelectedIndex = 0
        ddlType_SelectedIndexChanged(Nothing, EventArgs.Empty)
        txtEmpID.Text = ""
        txtEmpName.Text = ""
        _QueryFlag = ""
    End Sub

#Region "行政線"
    Private Sub LoadOrgType()
        Dim isSuccess As Boolean = False
        Dim msg As String = ""
        Dim datas As List(Of OrganListModel) = New List(Of OrganListModel)()
        Dim viewData As PO4000Model = New PO4000Model() With _
        { _
            .CompID = StringIIF(UserProfile.SelectCompRoleID)
        }
        isSuccess = POCommon.SelectOrgan(viewData, datas, msg)
        If isSuccess And datas.Count > 0 Then
            _AllowOrgan = datas
            ddlOrgType.DataSource = datas.Where(Function(x) x.OrgType = x.OrganID).Select(Function(x) New With {x.OrgType, x.OrgTypeName}).Distinct.ToList
            ddlOrgType.DataTextField = "OrgTypeName"
            ddlOrgType.DataValueField = "OrgType"
            ddlOrgType.DataBind()
            ddlOrgType.Items.Insert(0, New ListItem("----請選擇----", ""))
        Else
            _AllowOrgan = New List(Of OrganListModel)
            resetDDL(ddlOrgType)
            resetDDL(ddlDeptID)
            resetDDL(ddlOrganID)
        End If
    End Sub
    Private Sub LoadDeptID()
        ddlDeptID.DataSource = _AllowOrgan.Where(Function(x) x.OrgType = ddlOrgType.SelectedValue AndAlso x.DeptID = x.OrganID).Select(Function(x) New With {x.DeptID, x.DeptName}).Distinct.ToList
        ddlDeptID.DataTextField = "DeptName"
        ddlDeptID.DataValueField = "DeptID"
        ddlDeptID.DataBind()
        ddlDeptID.Items.Insert(0, New ListItem("----請選擇----", ""))
        If (ddlDeptID.Items.FindByValue(ddlOrgType.SelectedValue)) Is Nothing Then
            ddlDeptID.Items.Insert(0, New ListItem("----請選擇----", ""))
        Else
            ddlDeptID.SelectedValue = ddlOrgType.SelectedValue
            LoadOrganID()
        End If
    End Sub
    Private Sub LoadOrganID()
        ddlOrganID.DataSource = _AllowOrgan.Where(Function(x) x.DeptID = ddlDeptID.SelectedValue).Select(Function(x) New With {x.OrganID, x.OrganName}).ToList()
        ddlOrganID.DataTextField = "OrganName"
        ddlOrganID.DataValueField = "OrganID"
        ddlOrganID.DataBind()
        ddlOrganID.Items.Insert(0, New ListItem("----請選擇----", ""))
    End Sub
    Private Function GetOrganWhere() As String
        Dim orgWhere As String = ""
        Dim AllSearch As Boolean = (ddlOrgType.SelectedValue = "" AndAlso ddlDeptID.SelectedValue = "" AndAlso ddlOrganID.SelectedValue = "" AndAlso ddlRoleCode40.SelectedValue = "" AndAlso ddlRoleCode30.SelectedValue = "" AndAlso ddlRoleCode20.SelectedValue = "" AndAlso ddlRoleCode10.SelectedValue = "")
        If (ddlOrgType.SelectedValue <> "" OrElse ddlDeptID.SelectedValue <> "" OrElse ddlOrganID.SelectedValue <> "") OrElse AllSearch Then
            If ddlOrganID.SelectedValue <> "" Then
                orgWhere += ddlOrganID.SelectedValue
            ElseIf ddlDeptID.SelectedValue <> "" AndAlso ddlOrganID.SelectedValue = "" Then
                orgWhere += String.Join("', '", _AllowOrgan.Where(Function(x) x.DeptID = ddlDeptID.SelectedValue).Select(Function(x) x.OrganID))
            ElseIf ddlOrgType.SelectedValue <> "" AndAlso ddlDeptID.SelectedValue = "" AndAlso ddlOrganID.SelectedValue = "" Then
                orgWhere += String.Join("', '", _AllowOrgan.Where(Function(x) x.OrgType = ddlOrgType.SelectedValue).Select(Function(x) x.OrganID))
            Else
                orgWhere += String.Join("', '", _AllowOrgan.Select(Function(x) x.OrganID))
            End If
        End If
        Return orgWhere
    End Function
#End Region
#Region "功能線"
    Private Sub LoadRoleCode40()
        Dim isSuccess As Boolean = False
        Dim msg As String = ""
        Dim datas As List(Of FlowOrganListModel) = New List(Of FlowOrganListModel)()
        Dim viewData As PO4000Model = New PO4000Model() With _
        { _
            .CompID = StringIIF(UserProfile.SelectCompRoleID)
        }
        isSuccess = POCommon.SelectFlowOrgan(viewData, datas, msg)
        If isSuccess And datas.Count > 0 Then
            _AllowFlowOrgan = datas
            ddlRoleCode40.DataSource = datas.Where(Function(x) x.RoleCode = "40").Select(Function(x) New With {x.OrganID, x.OrganName}).Distinct().ToList()
            ddlRoleCode40.DataTextField = "OrganName"
            ddlRoleCode40.DataValueField = "OrganID"
            ddlRoleCode40.DataBind()
            ddlRoleCode40.Items.Insert(0, New ListItem("----請選擇----", ""))
        Else
            _AllowFlowOrgan = New List(Of FlowOrganListModel)
            resetDDL(ddlRoleCode40)
            resetDDL(ddlRoleCode30)
            resetDDL(ddlRoleCode20)
            resetDDL(ddlRoleCode10)
        End If
    End Sub
    Private Sub LoadRoleCode30()
        ddlRoleCode30.DataSource = _AllowFlowOrgan.Where(Function(x) x.UpOrganID = ddlRoleCode40.SelectedValue AndAlso x.RoleCode = "30").Select(Function(x) New With {x.OrganID, x.OrganName}).Distinct().ToList()
        ddlRoleCode30.DataTextField = "OrganName"
        ddlRoleCode30.DataValueField = "OrganID"
        ddlRoleCode30.DataBind()
        ddlRoleCode30.Items.Insert(0, New ListItem("----請選擇----", ""))
    End Sub
    Private Sub LoadRoleCode20()
        ddlRoleCode20.DataSource = _AllowFlowOrgan.Where(Function(x) x.UpOrganID = ddlRoleCode30.SelectedValue AndAlso x.RoleCode = "20").Select(Function(x) New With {x.OrganID, x.OrganName}).Distinct().ToList()
        ddlRoleCode20.DataTextField = "OrganName"
        ddlRoleCode20.DataValueField = "OrganID"
        ddlRoleCode20.DataBind()
        ddlRoleCode20.Items.Insert(0, New ListItem("----請選擇----", ""))
    End Sub
    Private Sub LoadRoleCode10()
        ddlRoleCode10.DataSource = _AllowFlowOrgan.Where(Function(x) x.DeptID = ddlRoleCode20.SelectedValue AndAlso (x.RoleCode = "10" OrElse x.RoleCode = "0")).Select(Function(x) New With {x.OrganID, x.OrganName}).Distinct().ToList()
        ddlRoleCode10.DataTextField = "OrganName"
        ddlRoleCode10.DataValueField = "OrganID"
        ddlRoleCode10.DataBind()
        ddlRoleCode10.Items.Insert(0, New ListItem("----請選擇----", ""))
    End Sub
    Private Function GetFlowOrganWhere() As String
        Dim orgFlowWhere As String = ""
        Dim AllSearch As Boolean = (ddlOrgType.SelectedValue = "" AndAlso ddlDeptID.SelectedValue = "" AndAlso ddlOrganID.SelectedValue = "" AndAlso ddlRoleCode40.SelectedValue = "" AndAlso ddlRoleCode30.SelectedValue = "" AndAlso ddlRoleCode20.SelectedValue = "" AndAlso ddlRoleCode10.SelectedValue = "")
        If (ddlRoleCode40.SelectedValue <> "" OrElse ddlRoleCode30.SelectedValue <> "" OrElse ddlRoleCode20.SelectedValue <> "" OrElse ddlRoleCode10.SelectedValue <> "") OrElse AllSearch Then
            If ddlRoleCode10.SelectedValue <> "" Then
                If ddlRoleCode10.SelectedItem.Text.StartsWith("└") Then
                    orgFlowWhere = ddlRoleCode10.SelectedValue
                Else
                    orgFlowWhere = String.Join("', '", _AllowFlowOrgan.Where(Function(x) x.OrganLevel = ddlRoleCode10.SelectedValue).Select(Function(x) x.OrganID))
                End If
            ElseIf ddlRoleCode20.SelectedValue <> "" AndAlso ddlRoleCode10.SelectedValue = "" Then
                orgFlowWhere = String.Join("', '", _AllowFlowOrgan.Where(Function(x) x.DeptID = ddlRoleCode20.SelectedValue).Select(Function(x) x.OrganID))
            ElseIf ddlRoleCode30.SelectedValue <> "" AndAlso ddlRoleCode20.SelectedValue = "" AndAlso ddlRoleCode10.SelectedValue = "" Then
                orgFlowWhere = ddlRoleCode30.SelectedValue
                orgFlowWhere += "', '" + String.Join("', '", _AllowFlowOrgan.Where(Function(x) x.UpOrganID = ddlRoleCode30.SelectedValue).Select(Function(x) x.OrganID))

                Dim RoleCode20 = _AllowFlowOrgan.Where(Function(x) x.UpOrganID = ddlRoleCode30.SelectedValue).Select(Function(x) x.OrganID).ToArray()
                orgFlowWhere += "', '" + String.Join("', '", _AllowFlowOrgan.Where(Function(x) RoleCode20.Contains(x.DeptID)).Select(Function(x) x.OrganID))
            ElseIf ddlRoleCode40.SelectedValue <> "" AndAlso ddlRoleCode30.SelectedValue = "" AndAlso ddlRoleCode20.SelectedValue = "" AndAlso ddlRoleCode10.SelectedValue = "" Then
                Dim BusinessType = _AllowFlowOrgan.Where(Function(x) x.OrganID = ddlRoleCode40.SelectedValue).Select(Function(x) x.BusinessType).ToArray()
                orgFlowWhere = String.Join("', '", _AllowFlowOrgan.Where(Function(x) x.BusinessType = BusinessType(0)).Select(Function(x) x.OrganID))
            Else
                'orgFlowWhere = String.Join("', '", _AllowFlowOrgan.Select(Function(x) x.OrganID))
            End If
        End If
        Return orgFlowWhere
    End Function
#End Region

    ''' <summary>
    ''' 重設DDL，給予請選擇
    ''' </summary>
    ''' <param name="DDL"></param>
    ''' <remarks></remarks>
    Private Sub resetDDL(ByVal DDL As DropDownList)
        DDL.Items.Clear()
        DDL.Items.Insert(0, New ListItem("---請選擇---", ""))
    End Sub

    ''' <summary>
    ''' 取得字串(去除null)
    ''' </summary>
    ''' <param name="ob">Object</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Private Function StringIIF(ByVal ob As Object) As String
        Dim result = ""
        If Not ob Is Nothing Then
            If Not String.IsNullOrEmpty(ob.ToString().Trim) Then
                result = ob.ToString()
            End If
        End If
        Return result
    End Function

    ''' <summary>
    ''' 時間下拉選單初始化
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub initTimeHM()
        StartTimeH.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        StartTimeM.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        EndTimeH.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        EndTimeM.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        For Hr As Integer = 0 To 23 Step 1
            StartTimeH.Items.Insert(Hr + 1, New ListItem(IIf(Hr < 10, "0" + CStr(Hr), CStr(Hr)), IIf(Hr < 10, "0" + CStr(Hr), CStr(Hr))))
            EndTimeH.Items.Insert(Hr + 1, New ListItem(IIf(Hr < 10, "0" + CStr(Hr), CStr(Hr)), IIf(Hr < 10, "0" + CStr(Hr), CStr(Hr))))
        Next
        For Mt As Integer = 0 To 59 Step 1
            StartTimeM.Items.Insert(Mt + 1, New ListItem(IIf(Mt < 10, "0" + CStr(Mt), CStr(Mt)), IIf(Mt < 10, "0" + CStr(Mt), CStr(Mt))))
            EndTimeM.Items.Insert(Mt + 1, New ListItem(IIf(Mt < 10, "0" + CStr(Mt), CStr(Mt)), IIf(Mt < 10, "0" + CStr(Mt), CStr(Mt))))
        Next
    End Sub

    ''' <summary>
    ''' 員工編號快速查詢按鈕初始化
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub initArgs()
        '員工編號
        ucQueryEmp.ShowCompRole = "False"
        ucQueryEmp.InValidFlag = "N"
        ucQueryEmp.SelectCompID = UserProfile.SelectCompRoleID
        ViewState.Item("isQuery") = False
    End Sub

    ''' <summary>
    ''' 員工編號快速查詢畫面返回值
    ''' </summary>
    ''' <param name="returnValue"></param>
    ''' <remarks></remarks>
    Public Overrides Sub DoModalReturn(ByVal returnValue As String)
        Dim strSql As String = ""

        If returnValue <> "" Then
            Dim aryData() As String = returnValue.Split(":")

            Select Case aryData(0)
                Case "ucQueryEmp"
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    '員工編號
                    txtEmpID.Text = aryValue(1)
                    txtEmpName.Text = aryValue(2)
            End Select
        End If
    End Sub

#End Region


End Class
