Imports System.Data
Imports System.Globalization
Imports System.Data.Common
Imports System.Linq
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json

Partial Class OV_OV2100
    Inherits PageBase

#Region "功能鍵設定"
    ''' <summary>
    ''' 功能鍵設定
    ''' </summary>
    ''' <param name="Param">String</param>
    ''' <remarks></remarks>
    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnQuery"     '查詢
                QueryValidate()
            Case "btnUpdate"  '修改補休失效日
                DoUpdate()
            Case "btnDownload"  '匯出
                DoDownload()
            Case "btnActionX"   '清除
                DoCancel()
        End Select
    End Sub
#End Region

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
    '存取查詢資料
    Private Property _queryDatas As DataTable
        Get
            Return ViewState.Item("QueryDatas")
        End Get
        Set(ByVal value As DataTable)
            ViewState.Item("QueryDatas") = value
        End Set
    End Property

    '存取顯示資料
    Private Property _showDatas As DataTable
        Get
            Return ViewState.Item("ShowDatas")
        End Get
        Set(ByVal value As DataTable)
            ViewState.Item("ShowDatas") = value
        End Set
    End Property

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
#End Region

#Region "Page_Load"
    ''' <summary>
    ''' 起始頁邏輯處理
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            _goUpdateDatas = Nothing

            '公司代碼
            If UserProfile.SelectCompRoleID = "ALL" Then
                lblCompRoleID.Visible = False
                Bsp.Utility.FillHRCompany(ddlCompID)
                Page.SetFocus(ddlCompID)
            Else
                lblCompRoleID.Text = UserProfile.SelectCompRoleName
                ddlCompID.Visible = False
            End If
            '員工編號
            ucQueryEmpID.ShowCompRole = "False"
            ucQueryEmpID.InValidFlag = "N"
            ucQueryEmpID.SelectCompID = UserProfile.SelectCompRoleID

            LoadDate()
        Else
            subReLoadColor(ddlOrgType)
            subReLoadColor(ddlDeptID)
        End If
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
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
                        ucFailDateStart.DateText = ht("ucFailDateStart").ToString().Replace("-", "/")
                        ucFailDateEnd.DateText = ht("ucFailDateEnd").ToString().Replace("-", "/")
                        ucStartDate.DateText = ht("ucStartDate").ToString().Replace("-", "/")
                        ucEndDate.DateText = ht("ucEndDate").ToString().Replace("-", "/")
                    End If
                End If
            Next

            If ht.ContainsKey("PageNo") Then pcMain.PageNo = Convert.ToInt32(ht("PageNo"))
            If ht.ContainsKey("DoQuery") Then
                If ht("DoQuery").ToString() = "Y" Then
                    ViewState.Item("DoQuery") = "Y"
                    DoQuery()
                End If
            End If
        End If
    End Sub
#End Region

#Region "畫面事件"
    ''' <summary>
    ''' grvMergeHeader_RowCreated
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">GridViewRowEventArgs</param>
    ''' <remarks></remarks>
    Protected Sub grvMergeHeader_RowCreated(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.Header Then
            Dim HeaderGrid As GridView = DirectCast(sender, GridView)
            Dim HeaderGridRow As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)
            'Dim HeaderCell As New TableCell()
            'HeaderCell.Text = ""
            'HeaderCell.ColumnSpan = 3
            'HeaderCell.CssClass = "td_header"
            'HeaderGridRow.Cells.Add(HeaderCell)
            'HeaderCell = New TableCell()
            'HeaderCell.Text = "加班單預先申請"
            'HeaderCell.ColumnSpan = 6
            'HeaderCell.CssClass = "td_header"
            'HeaderGridRow.Cells.Add(HeaderCell)
            'HeaderCell = New TableCell()
            'HeaderCell.Text = "加班單事後申報"
            'HeaderCell.ColumnSpan = 6
            'HeaderCell.CssClass = "td_header"
            'HeaderGridRow.Cells.Add(HeaderCell)
            gvMain.Controls(0).Controls.AddAt(0, HeaderGridRow)
        End If
    End Sub

    ''' <summary>
    ''' ddlDept's OnCahged
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub ddlDept_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDeptID.SelectedIndexChanged
        If ddlDeptID.SelectedValue = "" Then
            Bsp.Utility.FillDDL(ddlOrganID, "eHRMSDB", "Organization", "RTRIM(OrganID)", "OrganName", Bsp.Utility.DisplayType.Full, "", " And VirtualFlag = '0' And CompID = " & Bsp.Utility.Quote(UserProfile.SelectCompRoleID) & " AND ((OrganID = OrganID AND OrganID <> DeptID AND OrganID <> OrgType)OR(OrganID = OrganID AND OrganID = DeptID AND OrganID = OrgType)) ", "Order By OrganID")
            ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
        Else
            Bsp.Utility.FillDDL(ddlOrganID, "eHRMSDB", "Organization", "RTRIM(OrganID)", "OrganName", Bsp.Utility.DisplayType.Full, "", " And VirtualFlag = '0' And CompID = " & Bsp.Utility.Quote(UserProfile.SelectCompRoleID) & " And DeptID = " & Bsp.Utility.Quote(ddlDeptID.SelectedValue) & " AND ((OrganID = OrganID AND OrganID <> DeptID AND OrganID <> OrgType)OR(OrganID = OrganID AND OrganID = DeptID AND OrganID = OrgType)) ", "Order By OrganID")
            ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
        End If

        Dim str As String = "join " & eHRMSDB_ITRD & ".[dbo].EmpPosition EP ON PO.CompID=EP.CompID AND PO.PositionID=EP.PositionID AND EP.PrincipalFlag='1' join " & eHRMSDB_ITRD & ".[dbo].Personal P ON EP.CompID=P.CompID AND EP.EmpID=P.EmpID  join " & eHRMSDB_ITRD & ".[dbo].Organization ORT ON  P.OrganID=ORT.OrganID AND P.CompID =ORT.CompID"

        If (Not ddlOrganID.SelectedValue.ToString.Trim.Equals("")) Then
            Bsp.Utility.FillDDL(ddlPositionID, "eHRMSDB", "Position PO", "distinct PO.PositionID", "PO.Remark", Bsp.Utility.DisplayType.Full, str, "And PO.CompID = 'SPHBK1' and ORT.OrganID= '" + ddlOrganID.SelectedValue + "'", "Order By  PO.PositionID") '職位
            ddlPositionID.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        ElseIf (Not ddlDeptID.SelectedValue.ToString.Trim.Equals("")) Then
            Bsp.Utility.FillDDL(ddlPositionID, "eHRMSDB", "Position PO", "distinct PO.PositionID", "PO.Remark", Bsp.Utility.DisplayType.Full, str, "And PO.CompID = 'SPHBK1' and P.DeptID= '" + ddlDeptID.SelectedValue + "'", "Order By  PO.PositionID") '職位
            ddlPositionID.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        ElseIf (Not ddlOrgType.SelectedValue.ToString.Trim.Equals("")) Then
            Bsp.Utility.FillDDL(ddlPositionID, "eHRMSDB", "Position PO", "distinct PO.PositionID", "PO.Remark", Bsp.Utility.DisplayType.Full, str, "And PO.CompID = 'SPHBK1' and ORT.OrgType= '" + ddlOrgType.SelectedValue + "'", "Order By  PO.PositionID") '職位
            ddlPositionID.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        Else
            Bsp.Utility.FillDDL(ddlPositionID, "eHRMSDB", "Position PO", "distinct PO.PositionID", "PO.Remark", Bsp.Utility.DisplayType.Full, str, "And PO.CompID = 'SPHBK1'", "Order By  PO.PositionID") '職位
            ddlPositionID.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        End If
    End Sub
#End Region

#Region "功能鍵邏輯處理"
    ''' <summary>
    ''' 修改補休失效日
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DoUpdate()
        _goUpdateDatas = Nothing
        If _queryDatas Is Nothing Then
            Bsp.Utility.ShowMessage(Me, "請先查詢！")
        Else
            If _showDatas Is Nothing Or _showDatas.Rows.Count = 0 Then
                Bsp.Utility.ShowMessage(Me, "無資料選取！")
            Else
                If selectedRows(gvMain) = "" Then
                    Bsp.Utility.ShowFormatMessage(Me, "W_00000")
                Else
                    Dim dt As DataTable = _showDatas.Clone()
                    Dim count As Integer = 0
                    For intRow As Integer = 0 To gvMain.Rows.Count - 1
                        Dim objChk As CheckBox = gvMain.Rows(intRow).FindControl("chk_gvMain")
                        Dim gvCompID As String = StringIIF(gvMain.DataKeys(intRow)("OTCompID"))
                        Dim gvEmpID As String = StringIIF(gvMain.DataKeys(intRow)("OTEmpID"))
                        Dim gvStartDate As String = StringIIF(gvMain.DataKeys(intRow)("OTStartDate"))
                        Dim gvEndDate As String = StringIIF(gvMain.DataKeys(intRow)("OTEndDate"))
                        Dim gvSeq As String = StringIIF(gvMain.DataKeys(intRow)("OTSeq"))
                        If objChk.Checked And gvCompID <> "" And gvEmpID <> "" And gvStartDate <> "" And gvEndDate <> "" And gvSeq <> "" Then
                            Dim newRowData As DataRow = (From column In _showDatas.Rows _
                                       Where column("OTCompID") = gvCompID _
                                       And column("OTEmpID") = gvEmpID _
                                       And column("OTStartDate") = gvStartDate _
                                       And column("OTEndDate") = gvEndDate _
                                       And column("OTSeq") = gvSeq _
                                       ).FirstOrDefault()
                            count = count + 1
                            newRowData.Item("GridViewIndex") = StringIIF(count)
                            dt.ImportRow(newRowData)
                        End If
                    Next
                    If dt.Rows.Count > 0 Then
                        _goUpdateDatas = dt
                        Dim btnD As New ButtonState(ButtonState.emButtonType.Delete)
                        btnD.Caption = "返回"
                        Dim btnU As New ButtonState(ButtonState.emButtonType.Update)
                        btnU.Caption = "修改"
                        Dim btnC As New ButtonState(ButtonState.emButtonType.Cancel)
                        btnC.Caption = "清除"
                        Me.TransferFramePage("~/OV/OV2102.aspx", New ButtonState() {btnU, btnD, btnC}, _
                        ddlCompID.ID & "=" & ddlCompID.SelectedValue, _
                        ddlOrgType.ID & "=" & ddlOrgType.SelectedValue, _
                        ddlDeptID.ID & "=" & ddlDeptID.SelectedValue, _
                        ddlOrganID.ID & "=" & ddlOrganID.SelectedValue, _
                        tbOTEmpID.ID & "=" & tbOTEmpID.Text, _
                        tbOTEmpName.ID & "=" & tbOTEmpName.Text, _
                        ddlWorkStatus.ID & "=" & ddlWorkStatus.SelectedValue, _
                        ddlOrganID.ID & "=" & ddlOrganID.SelectedValue, _
                        ddlRankIDMIN.ID & "=" & ddlRankIDMIN.SelectedValue, _
                        ddlRankIDMAX.ID & "=" & ddlRankIDMAX.SelectedValue, _
                        ddlTitleIDMIN.ID & "=" & ddlTitleIDMIN.SelectedValue, _
                        ddlTitleIDMAX.ID & "=" & ddlTitleIDMAX.SelectedValue, _
                        ddlPositionID.ID & "=" & ddlPositionID.SelectedValue, _
                        ucFailDateStart.ID & "=" & DateStringIIF(ucFailDateStart.DateText), _
                        ucFailDateEnd.ID & "=" & DateStringIIF(ucFailDateEnd.DateText), _
                        ucStartDate.ID & "=" & DateStringIIF(ucStartDate.DateText), _
                        ucEndDate.ID & "=" & DateStringIIF(ucEndDate.DateText), _
                        StartTimeH.ID & "=" & StartTimeH.SelectedValue, _
                        StartTimeM.ID & "=" & StartTimeM.SelectedValue, _
                        EndTimeH.ID & "=" & EndTimeH.SelectedValue, _
                        EndTimeM.ID & "=" & EndTimeM.SelectedValue, _
                        tbOTPayDate.ID & "=" & tbOTPayDate.Text, _
                        "PageNo=" & pcMain.PageNo.ToString(), _
                        "DoQuery=" & "Y")
                    End If
                End If
            End If
        End If


    End Sub

    ''' <summary>
    ''' 匯出
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DoDownload()
        If _queryDatas Is Nothing Then
            Bsp.Utility.ShowMessage(Me, "請先查詢！")
        Else
            If _showDatas Is Nothing Or _showDatas.Rows.Count = 0 Then
                Bsp.Utility.ShowMessage(Me, "無資料匯出！")
            Else
                Try
                    '產出檔頭
                    Dim strFileName As String = Bsp.Utility.GetNewFileName("補休資料列表-") & ".xls"

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
                    Dim dt As DataTable = _showDatas.Clone()
                    '複製資料
                    For index As Integer = 0 To _showDatas.Rows.Count - 1
                        dt.ImportRow(_showDatas.Rows(index))
                    Next
                    '處理列印欄位
                    For index As Integer = _showDatas.Columns.Count - 1 To 0 Step -1
                        Dim itemData = dt.Columns.Item(index)
                        Dim isPrintColumn = False

                        Select Case itemData.ColumnName
                            Case "OTEmpID"     '員工編號
                                itemData.ColumnName = "員工編號"
                                isPrintColumn = True
                            Case "OTEmpName"  '姓名
                                itemData.ColumnName = "姓名"
                                isPrintColumn = True
                            Case "ORT2ID"  '單位類別代碼
                                itemData.ColumnName = "單位類別代碼"
                                isPrintColumn = True
                            Case "ORT2Name"   '單位類別名稱
                                itemData.ColumnName = "單位類別名稱"
                                isPrintColumn = True
                            Case "DeptID"  '部門代碼
                                itemData.ColumnName = "部門代碼"
                                isPrintColumn = True
                            Case "DeptName"   '部門名稱
                                itemData.ColumnName = "部門名稱"
                                isPrintColumn = True
                            Case "ORTID"  '科組課代碼
                                itemData.ColumnName = "科組課代碼"
                                isPrintColumn = True
                            Case "ORTName"   '科組課名稱
                                itemData.ColumnName = "科組課名稱"
                                isPrintColumn = True
                            Case "OTStartDate"   '加班日期(起)
                                itemData.ColumnName = "加班日期(起)"
                                isPrintColumn = True
                            Case "OTEndDate"   '加班日期(迄)
                                itemData.ColumnName = "加班日期(迄)"
                                isPrintColumn = True
                            Case "StartTimeShow"   '開始時間
                                itemData.ColumnName = "開始時間"
                                isPrintColumn = True
                            Case "EndTimeShow"   '結束時間
                                itemData.ColumnName = "結束時間"
                                isPrintColumn = True
                            Case "OTTotalTime"   '加班時數
                                itemData.ColumnName = "加班時數"
                                isPrintColumn = True
                            Case "OTTotalTime2"   '補休時數
                                itemData.ColumnName = "補休時數"
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
                    For index As Integer = 0 To dt.Rows.Count - 1
                        For j As Integer = 0 To gvExport.Rows.Item(index).Cells.Count - 1

                            If j = 12 Or j = 13 Then '加班時數與補休時數儲存格資料為小數後一位數值格式
                                gvExport.Rows.Item(index).Cells(j).Attributes.Add("style", "vnd.ms-excel.numberformat:#,##0.0")
                            Else
                                gvExport.Rows.Item(index).Cells(j).HorizontalAlign = HorizontalAlign.Center '水平置中
                                gvExport.Rows.Item(index).Cells(j).VerticalAlign = VerticalAlign.Middle '垂直置中
                            End If
                        Next
                    Next
                    gvExport.RenderControl(oHtmlTextWriter)
                    Response.Write(style)
                    Response.Write(oStringWriter.ToString())
                    Response.End()
                Catch ex As Exception
                    Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoDownload", ex)
                End Try

            End If
        End If
    End Sub

    ''' <summary>
    ''' 查詢前檢核
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub QueryValidate()
        Try
            Dim sOTEmpID = StringIIF(tbOTEmpID.Text) '員工編號
            Dim bOTEmpID = Not String.IsNullOrEmpty(sOTEmpID)
            Dim sOTEmpName = StringIIF(tbOTEmpName.Text) '員工姓名
            Dim bOTEmpName = Not String.IsNullOrEmpty(sOTEmpName)
            Dim sFailDate_S = DateStringIIF(ucFailDateStart.DateText) '補休失效日(起)
            Dim bFailDate_S = Not String.IsNullOrEmpty(sFailDate_S)
            Dim sFailDate_E = DateStringIIF(ucFailDateEnd.DateText) '補休失效日(迄)
            Dim bFailDate_E = Not String.IsNullOrEmpty(sFailDate_E)
            Dim sStartDate = DateStringIIF(ucStartDate.DateText) '加班日期(起)
            Dim bStartDate = Not String.IsNullOrEmpty(sStartDate)
            Dim sEndDate = DateStringIIF(ucEndDate.DateText) '加班日期(迄)
            Dim bEndDate = Not String.IsNullOrEmpty(sEndDate)
            Dim sOTPayDate = StringIIF(tbOTPayDate.Text) '計薪年月
            Dim bOTPayDate = Not String.IsNullOrEmpty(sOTPayDate)

            Dim CompID_RankID As String = UserProfile.SelectCompRoleID
            Dim sRankID_S = StringIIF(ddlRankIDMIN.SelectedValue) '職等(起)
            Dim bRankID_S = Not String.IsNullOrEmpty(sRankID_S)
            If bRankID_S Then
                sRankID_S = OVBusinessCommon.GetRankID(CompID_RankID, sRankID_S)
            End If
            Dim sRankID_E = StringIIF(ddlRankIDMAX.SelectedValue) '職等(迄)
            Dim bRankID_E = Not String.IsNullOrEmpty(sRankID_E)
            If bRankID_E Then
                sRankID_E = OVBusinessCommon.GetRankID(CompID_RankID, sRankID_E)
            End If
            If sRankID_S > sRankID_E Then
                Throw New Exception("職等(迄)不可小於職等(起) !!")
            End If

            If bOTEmpID Then
                If Not IsNumeric(sOTEmpID) Then
                    Throw New Exception("員工編號請輸入數字 !!")
                End If
            End If

            If bFailDate_S And bFailDate_E Then
                If CompareDateTime(sFailDate_S, sFailDate_E, ">") Then
                    Throw New Exception("補休失效日(起) 不可大於 補休失效日(迄) !!")
                End If
            End If

            If bStartDate And bEndDate Then
                If CompareDateTime(sStartDate, sEndDate, ">") Then
                    Throw New Exception("加班日期(起) 不可大於 加班日期(迄) !!")
                End If
            End If

            If bOTPayDate Then
                Dim newDate As Date = New Date()
                If Not Date.TryParseExact(sOTPayDate, "yyyyMM", Nothing, DateTimeStyles.None, newDate) Then
                    Throw New Exception("計薪年月格式有誤，請依照 YYYYMM 格式輸入!!")
                End If
            End If

            DoQuery() '檢核無誤查詢開始

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, ex.Message) '彈出檢核錯誤訊息!
        End Try
    End Sub

    ''' <summary>
    ''' 查詢
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DoQuery()

        Dim sOrgType = StringIIF(ddlOrgType.SelectedValue) '單位類別
        Dim bOrgType = Not String.IsNullOrEmpty(sOrgType)

        Dim sDeptID = StringIIF(ddlDeptID.SelectedValue) '部門ID
        Dim bDeptID = Not String.IsNullOrEmpty(sDeptID)

        Dim sOrganID = StringIIF(ddlOrganID.SelectedValue) '科別
        Dim bOrganID = Not String.IsNullOrEmpty(sOrganID)

        Dim sOTEmpID = StringIIF(tbOTEmpID.Text) '員工編號
        Dim bOTEmpID = Not String.IsNullOrEmpty(sOTEmpID)

        Dim sOTEmpName = StringIIF(tbOTEmpName.Text) '員工姓名
        Dim bOTEmpName = Not String.IsNullOrEmpty(sOTEmpName)

        Dim sWorkStatus = StringIIF(ddlWorkStatus.SelectedValue) '在職狀態
        Dim bWorkStatus = Not String.IsNullOrEmpty(sWorkStatus)

        Dim sTitleNameMin = StringIIF(ddlTitleIDMIN.SelectedValue) '職稱(起)
        Dim bTitleNameMin = Not String.IsNullOrEmpty(sTitleNameMin)

        Dim sTitleNameMax = StringIIF(ddlTitleIDMAX.SelectedValue) '職稱(迄)
        Dim bTitleNameMax = Not String.IsNullOrEmpty(sTitleNameMax)

        Dim sPositionID = StringIIF(ddlPositionID.SelectedValue) '職位
        Dim bPositionID = Not String.IsNullOrEmpty(sPositionID)

        Dim CompID_RankID As String = UserProfile.SelectCompRoleID

        Dim sRankID_S = StringIIF(ddlRankIDMIN.SelectedValue) '職等(起)
        Dim bRankID_S = Not String.IsNullOrEmpty(sRankID_S)
        If bRankID_S Then
            sRankID_S = OVBusinessCommon.GetRankID(CompID_RankID, sRankID_S)
        End If

        Dim sRankID_E = StringIIF(ddlRankIDMAX.SelectedValue) '職等(迄)
        Dim bRankID_E = Not String.IsNullOrEmpty(sRankID_E)
        If bRankID_E Then
            sRankID_E = OVBusinessCommon.GetRankID(CompID_RankID, sRankID_E)
        End If

        Dim sOTPayDate = StringIIF(tbOTPayDate.Text) '計薪年月
        Dim bOTPayDate = Not String.IsNullOrEmpty(sOTPayDate)

        Dim sFailDate_S = DateStringIIF(ucFailDateStart.DateText) '補休失效日(起)
        Dim bFailDate_S = Not String.IsNullOrEmpty(sFailDate_S)

        Dim sFailDate_E = DateStringIIF(ucFailDateEnd.DateText) '補休失效日(迄)
        Dim bFailDate_E = Not String.IsNullOrEmpty(sFailDate_E)

        Dim sStartDate = DateStringIIF(ucStartDate.DateText) '加班日期(起)
        Dim bStartDate = Not String.IsNullOrEmpty(sStartDate)

        Dim sEndDate = DateStringIIF(ucEndDate.DateText) '加班日期(迄)
        Dim bEndDate = Not String.IsNullOrEmpty(sEndDate)

        Dim sStartTimeH = StringIIF(StartTimeH.SelectedValue) '開始時間
        Dim sStartTimeM = StringIIF(StartTimeM.SelectedValue)
        Dim sStartTime = sStartTimeH + sStartTimeM
        Dim bStartTime = Not (String.IsNullOrEmpty(sStartTimeH) Or String.IsNullOrEmpty(sStartTimeM))

        Dim sEndTimeH = StringIIF(EndTimeH.SelectedValue) '結束時間
        Dim sEndTimeM = StringIIF(EndTimeM.SelectedValue)
        Dim sEndTime = sEndTimeH + sEndTimeM
        Dim bEndTime = Not (String.IsNullOrEmpty(sEndTimeH) Or String.IsNullOrEmpty(sEndTimeM))

        Dim compID As String = UserProfile.SelectCompRoleID
        Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
        Dim dbcmd As DbCommand
        Dim strSQL As StringBuilder = New StringBuilder()
        strSQL.AppendLine("SELECT ")
        strSQL.AppendLine("OTD.OTCompID, OTD.OTEmpID, OTD.OTStartDate, OTD.OTEndDate, OTD.OTSeq ") 'Key
        strSQL.AppendLine(", OTD.SalaryOrAdjust, OTD.DeptID, OTD.OrganID, OTD.OTPayDate, OTD.AdjustInvalidDate, OTD.OTStartTime, OTD.OTEndTime ,OTD.OTRegisterID") '條件
        strSQL.AppendLine(", OTD.OTTxnID, OTD.OTSeqNo, OTD.OTStatus, OTD.OTFormNO, OTD.OTAttachment, OTD.FlowCaseID, OTD.OTTotalTime,(OTD.OTTotalTime/60) As OTTotalHour, OTD.DeptName, OTD.OrganName, OTD.OTReasonMemo, OTD.MealFlag, OTD.MealTime , OTD.LastChgComp, OTD.LastChgID, REPLACE(CONVERT(NVARCHAR (19),OTD.LastChgDate,120),'-','/') AS LastChgDate ") '其他需要的欄位
        strSQL.AppendLine(", ORT2.OrganID As ORT2ID, ORT2.OrganName As ORT2Name, ORT.OrganID As ORTID ,ORT.OrganName As ORTName ") 'Organization的欄位
        strSQL.AppendLine(", Pl.Name, Pl.RankID, Pl.WorkStatus ") 'Personal的欄位
        strSQL.AppendLine(", PR.Name As RName ") 'Personal的欄位
        strSQL.AppendLine(", Tl.TitleName ") 'Title的欄位
        strSQL.AppendLine(", MAP.CodeCName ") 'AT_CodeMap的欄位
        strSQL.AppendLine(", EP.PositionID ") 'EmpPosition的欄位
        strSQL.AppendLine(", Pt.Remark ") 'Position的欄位
        strSQL.AppendLine(", AI.FileName ") 'AttachInfo的欄位
        strSQL.AppendLine(", WS.Remark AS WorkStatusName ") 'WorkStatus的欄位
        strSQL.AppendLine("FROM OverTimeDeclaration AS OTD ")
        strSQL.AppendLine("LEFT JOIN " & eHRMSDB_ITRD & ".[dbo].[Personal] AS Pl ") 'Join Personal
        strSQL.AppendLine("ON OTD.OTCompID = Pl.CompID AND OTD.OTEmpID = Pl.EmpID ")
        strSQL.AppendLine("LEFT JOIN " & eHRMSDB_ITRD & ".[dbo].[Personal] AS PR ") 'Join Personal
        strSQL.AppendLine("ON OTD.OTRegisterComp = PR.CompID AND OTD.OTRegisterID = PR.EmpID ")
        strSQL.AppendLine("LEFT JOIN " & eHRMSDB_ITRD & ".[dbo].[Title] AS Tl ") 'Join Title
        strSQL.AppendLine("ON OTD.OTCompID = Tl.CompID AND Pl.RankID = Tl.RankID AND Pl.TitleID = Tl.TitleID ")
        strSQL.AppendLine("LEFT JOIN " & eHRMSDB_ITRD & ".[dbo].[EmpPosition] AS EP ") 'Join EmpPosition
        strSQL.AppendLine("ON OTD.OTCompID = EP.CompID AND OTD.OTEmpID = EP.EmpID And EP.PrincipalFlag = '1' ")
        strSQL.AppendLine("LEFT JOIN " & eHRMSDB_ITRD & ".[dbo].[Position] AS Pt ") 'Join Position
        strSQL.AppendLine("ON OTD.OTCompID = Pt.CompID AND EP.PositionID = Pt.PositionID ")
        strSQL.AppendLine("LEFT JOIN AttachInfo AS AI ") 'Join AttachInfo
        strSQL.AppendLine("ON OTD.OTAttachment <> '' AND OTD.OTAttachment = AI.AttachID AND AI.FileSize > 0 ")
        strSQL.AppendLine("LEFT JOIN " & eHRMSDB_ITRD & ".[dbo].[WorkStatus] AS WS ") 'Join WorkStatus
        strSQL.AppendLine("ON Pl.WorkStatus <> '' AND Pl.WorkStatus = WS.WorkCode ")
        strSQL.AppendLine("LEFT JOIN " & eHRMSDB_ITRD & ".[dbo].[RankMapping] AS RM ") 'Join RankMapping
        strSQL.AppendLine("ON Pl.CompID = RM.CompID AND Pl.RankID = RM.RankID ")
        strSQL.AppendLine("left join AT_CodeMap MAP ON MAP.TabName='OverTime' And MAP.FldName = 'OverTimeType' And  MAP.Code=OTD.OTTypeID")
        strSQL.AppendLine("left join " & eHRMSDB_ITRD & ".[dbo].Organization ORT ON  Pl.OrganID=ORT.OrganID AND Pl.CompID =ORT.CompID")
        strSQL.AppendLine("left join " & eHRMSDB_ITRD & ".[dbo].Organization ORT1 ON Pl.DeptID=ORT1.OrganID")
        strSQL.AppendLine("left join " & eHRMSDB_ITRD & ".[dbo].Organization ORT2 ON ORT.OrgType=ORT2.OrganID")
        strSQL.AppendLine("WHERE 1=1 ")
        strSQL.AppendLine("AND OTD.OTCompID = " & Bsp.Utility.Quote(compID))
        strSQL.AppendLine("AND OTD.OTStatus IN ('3') ")
        strSQL.AppendLine("AND OTD.SalaryOrAdjust = @SalaryOrAdjust ")
        If bDeptID Then '部門 有值
            strSQL.AppendLine("AND OTD.DeptID = @DeptID ")
        End If
        If bOrganID Then '科別 有值
            strSQL.AppendLine("AND OTD.OrganID = @OrganID ")
        End If
        If bOTEmpID Then '員工編號 有值
            strSQL.AppendLine("AND OTD.OTEmpID = @OTEmpID ")
        End If
        If bOTEmpName Then '員工姓名 有值
            If sOTEmpName.Length = 2 Then
                strSQL.AppendLine("AND (Pl.Name LIKE @OTEmpName ")
                strSQL.AppendLine("OR Pl.Name LIKE @OTEmpName2) ")
            Else
                strSQL.AppendLine("AND Pl.Name LIKE @OTEmpName ")
            End If

        End If
        If bWorkStatus Then '在職狀態 有值
            strSQL.AppendLine("AND Pl.WorkStatus = @WorkStatus ")
        End If
        If bTitleNameMin Then '職稱 有值
            strSQL.AppendLine("AND Tl.TitleName = @TitleNameMin ")
        End If
        If bTitleNameMax Then '職稱 有值
            strSQL.AppendLine("AND Tl.TitleName = @TitleNameMax ")
        End If
        If bPositionID Then '職位 有值
            strSQL.AppendLine("AND EP.PositionID = @PositionID ")
        End If
        If bRankID_S Then '職等(起) 有值
            strSQL.AppendLine("AND RM.RankIDMap >= @RankID_S ")
        End If
        If bRankID_E Then '職等(迄) 有值
            strSQL.AppendLine("AND RM.RankIDMap <= @RankID_E ")
        End If
        If bOTPayDate Then '計薪年月 有值
            strSQL.AppendLine("AND OTD.OTPayDate = @OTPayDate ")
        End If
        If bFailDate_S Then '補休失效日(起) 有值
            strSQL.AppendLine("AND OTD.AdjustInvalidDate  >= @FailDate_S ")
        End If
        If bFailDate_E Then '補休失效(迄)日 有值
            strSQL.AppendLine("AND OTD.AdjustInvalidDate  <= @FailDate_E ")
        End If
        If bStartDate Then '加班開始日期 有值
            strSQL.AppendLine("AND OTD.OTStartDate >= @StartDate ")
        End If
        If bEndDate Then '加班結束日期 有值
            strSQL.AppendLine("AND OTD.OTEndDate <= @EndDate ")
        End If
        If bStartTime Then '開始時間 有值
            strSQL.AppendLine("AND OTD.OTStartTime >= @StartTime ")
        End If
        If bEndTime Then '結束時間 有值
            strSQL.AppendLine("AND OTD.OTEndTime <= @EndTime ")
        End If
        strSQL.AppendLine("ORDER BY OTD.OTTxnID, OTD.OTSeqNo ")
        strSQL.AppendLine("; ")

        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
        db.AddInParameter(dbcmd, "@SalaryOrAdjust", DbType.String, "2")
        If bDeptID Then '部門 有值
            db.AddInParameter(dbcmd, "@DeptID", DbType.String, sDeptID)
        End If
        If bOrganID Then '科別 有值
            db.AddInParameter(dbcmd, "@OrganID", DbType.String, sOrganID)
        End If
        If bOTEmpID Then '員工編號 有值
            db.AddInParameter(dbcmd, "@OTEmpID", DbType.String, sOTEmpID)
        End If
        If bOTEmpName Then '員工姓名 有值
            If sOTEmpName.Length = 2 Then
                db.AddInParameter(dbcmd, "@OTEmpName", DbType.String, sOTEmpName + "%")
                db.AddInParameter(dbcmd, "@OTEmpName2", DbType.String, getEmpNameSelectDBFormat(sOTEmpName) + "%")
            Else
                db.AddInParameter(dbcmd, "@OTEmpName", DbType.String, getEmpNameSelectDBFormat(sOTEmpName) + "%")
            End If

        End If
        If bWorkStatus Then '在職狀態 有值
            db.AddInParameter(dbcmd, "@WorkStatus", DbType.String, sWorkStatus)
        End If
        If bTitleNameMin Then '職稱 有值
            db.AddInParameter(dbcmd, "@TitleNameMin", DbType.String, sTitleNameMin)
        End If
        If bTitleNameMax Then '職稱 有值
            db.AddInParameter(dbcmd, "@TitleNameMax", DbType.String, sTitleNameMax)
        End If
        If bPositionID Then '職位 有值
            db.AddInParameter(dbcmd, "@PositionID", DbType.String, sPositionID)
        End If
        If bRankID_S Then '職等(起) 有值
            db.AddInParameter(dbcmd, "@RankID_S", DbType.String, sRankID_S)
        End If
        If bRankID_E Then '職等(迄) 有值
            db.AddInParameter(dbcmd, "@RankID_E", DbType.String, sRankID_E)
        End If
        If bOTPayDate Then '計薪年月 有值
            db.AddInParameter(dbcmd, "@OTPayDate", DbType.String, sOTPayDate)
        End If
        If bFailDate_S Then '補休失效日(起) 有值
            db.AddInParameter(dbcmd, "@FailDate_S", DbType.String, sFailDate_S)
        End If
        If bFailDate_E Then '補休失效日(迄) 有值
            db.AddInParameter(dbcmd, "@FailDate_E", DbType.String, sFailDate_E)
        End If
        If bStartDate Then '加班開始日期 有值
            db.AddInParameter(dbcmd, "@StartDate", DbType.String, sStartDate)
        End If
        If bEndDate Then '加班結束日期 有值
            db.AddInParameter(dbcmd, "@EndDate", DbType.String, sEndDate)
        End If
        If bStartTime Then '開始時間 有值
            db.AddInParameter(dbcmd, "@StartTime", DbType.String, sStartTime)
        End If
        If bEndTime Then '結束時間 有值
            db.AddInParameter(dbcmd, "@EndTime", DbType.String, sEndTime)
        End If

        Dim ds As DataSet = db.ExecuteDataSet(dbcmd)
        If ds.Tables.Count > 0 Then
            _queryDatas = ds.Tables(0)
            Dim dt As DataTable = getCusDataTable(ds.Tables(0)) '顯示資料處理
            'ShowTable.Visible = True
            pcMain.DataTable = dt
            _showDatas = dt
            gvMain.DataBind()
        End If
    End Sub


    ''' <summary>
    ''' 清除
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DoCancel()
        If Not pcMain.DataTable Is Nothing Then 'GridView
            pcMain.DataTable.Clear()
            pcMain.BindGridView()
        End If
        ViewState.Item("DoQuery") = ""
        ddlOrgType.SelectedIndex = 0 '單位類別
        ddlDeptID.SelectedIndex = 0 '部門ID
        'ddlOrganID.SelectedIndex = 0 '科別
        tbOTEmpID.Text = "" '員工編號
        tbOTEmpName.Text = "" '員工姓名
        ddlWorkStatus.SelectedIndex = 0 '在職狀態
        ddlTitleIDMIN.SelectedIndex = 0 '職稱(起)
        ddlRankIDMIN_SelectedIndexChanged(Me.FindControl("ddlTitleIDMIN"), EventArgs.Empty)
        ddlTitleIDMAX.SelectedIndex = 0 '職稱(迄)
        ddlRankIDMIN_SelectedIndexChanged(Me.FindControl("ddlTitleIDMAX"), EventArgs.Empty)
        ddlPositionID.SelectedIndex = 0 '職位
        ddlRankIDMIN.SelectedIndex = 0 '職等(起)
        ddlRankIDMAX.SelectedIndex = 0 '職等(迄)
        tbOTPayDate.Text = "" '計薪年月
        ucFailDateStart.DateText = "" '補休失效日(起)
        ucFailDateEnd.DateText = "" '補休失效日(迄)
        ucStartDate.DateText = "" '加班日期(起)
        ucEndDate.DateText = "" '加班日期(迄)
        StartTimeH.SelectedIndex = 0 '開始時間
        StartTimeM.SelectedIndex = 0
        EndTimeH.SelectedIndex = 0 '結束時間
        EndTimeM.SelectedIndex = 0
        'ShowTable.Visible = False
        lblEmpID.Text = ""
        Bsp.Utility.FillDDL(ddlOrganID, "eHRMSDB", "Organization", "RTRIM(OrganID)", "OrganName + '-' + RoleCode", Bsp.Utility.DisplayType.Full, "", "And InValidFlag = '0' And VirtualFlag = '0' And CompID = " & Bsp.Utility.Quote(UserProfile.SelectCompRoleID), "Order By OrganID")
        ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
        'tbOTPayDateErrorMsg.Visible = False
    End Sub
#End Region

#Region "Private Methods"

    ''' <summary>
    ''' 日期比較
    ''' </summary>
    ''' <param name="sDateS">日期一</param>
    ''' <param name="sDateE">日期二</param>
    ''' <param name="compareFlag"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Private Function CompareDateTime(ByVal sDateS As String, ByVal sDateE As String, ByVal compareFlag As String) As Boolean
        Dim result As Boolean = True
        Dim dateS As Date = New Date()
        Dim dateE As Date = New Date()
        If Date.TryParse(sDateS, dateS) And Date.TryParse(sDateE, dateE) Then
            Dim compareValue As Integer = dateS.CompareTo(dateE)
            Select Case compareFlag
                Case ">"
                    result = (compareValue > 0)
                Case "<"
                    result = (compareValue < 0)
                Case "="
                    result = (compareValue = 0)
                Case ">="
                    result = (compareValue >= 0)
                Case "<="
                    result = (compareValue <= 0)
            End Select
        End If
        Return result
    End Function

    ''' <summary>
    ''' 日期比較
    ''' </summary>
    ''' <param name="date01">日期一</param>
    ''' <param name="date02">日期二</param>
    ''' <returns>
    ''' 0 : 日期一 等於 日期二 
    ''' -1: 日期一 小於 日期二 
    ''' 1 : 日期一 大於 日期二
    ''' </returns>
    ''' <remarks></remarks>
    Private Function CompareDateTime(ByVal date01 As Date, ByVal date02 As Date) As Integer
        Return date01.CompareTo(date02)
    End Function

    ''' <summary>
    ''' 處理下拉選單與預設值
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadDate()
        StartTimeH.Items.Insert(0, New ListItem("--", ""))
        StartTimeM.Items.Insert(0, New ListItem("--", ""))
        For i = 1 To 24 Step +1
            If i <= 10 Then
                StartTimeH.Items.Insert(i, New ListItem("0" + (i - 1).ToString, "0" + (i - 1).ToString))
            Else
                StartTimeH.Items.Insert(i, New ListItem((i - 1).ToString, (i - 1).ToString))
            End If
        Next i
        For i = 1 To 60 Step +1
            If i <= 10 Then
                StartTimeM.Items.Insert(i, New ListItem("0" + (i - 1).ToString, "0" + (i - 1).ToString))
            Else
                StartTimeM.Items.Insert(i, New ListItem((i - 1).ToString, (i - 1).ToString))
            End If
        Next i
        EndTimeH.Items.Insert(0, New ListItem("--", ""))
        EndTimeM.Items.Insert(0, New ListItem("--", ""))
        For i = 1 To 24 Step +1
            If i <= 10 Then
                EndTimeH.Items.Insert(i, New ListItem("0" + (i - 1).ToString, "0" + (i - 1).ToString))
            Else
                EndTimeH.Items.Insert(i, New ListItem((i - 1).ToString, (i - 1).ToString))
            End If
        Next i
        For i = 1 To 60 Step +1
            If i <= 10 Then
                EndTimeM.Items.Insert(i, New ListItem("0" + (i - 1).ToString, "0" + (i - 1).ToString))
            Else
                EndTimeM.Items.Insert(i, New ListItem((i - 1).ToString, (i - 1).ToString))
            End If
        Next i

        Bsp.Utility.FillDDL(ddlCompID, "eHRMSDB", "Company", "CompID", "CompName")
        ddlCompID.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        Bsp.Utility.FillDDL(ddlRankIDMAX, "eHRMSDB", "Title", "RankID", "RankID", Bsp.Utility.DisplayType.OnlyID, , "And CompID = 'SPHBK1'", "group by RankID")
        ddlRankIDMAX.Items.Insert(0, New ListItem("--", ""))
        Bsp.Utility.FillDDL(ddlRankIDMIN, "eHRMSDB", "Title", "RankID", "RankID", Bsp.Utility.DisplayType.OnlyID, , "And CompID = 'SPHBK1'", "group by RankID")
        ddlRankIDMIN.Items.Insert(0, New ListItem("--", ""))
        'Bsp.Utility.FillDDL(ddlTitleIDMIN, "eHRMSDB", "Title", "TitleID+'-'+TitleName", "TitleID+'-'+TitleName", Bsp.Utility.DisplayType.OnlyID, , "And CompID = 'SPHBK1'", "group by TitleID,TitleName") '職稱
        ddlTitleIDMIN.Items.Insert(0, New ListItem("---請先選擇職等---", ""))
        'Bsp.Utility.FillDDL(ddlTitleIDMAX, "eHRMSDB", "Title", "TitleID+'-'+TitleName", "TitleID+'-'+TitleName", Bsp.Utility.DisplayType.OnlyID, , "And CompID = 'SPHBK1'", "group by TitleID,TitleName") '職稱
        ddlTitleIDMAX.Items.Insert(0, New ListItem("---請先選擇職等---", ""))
        Bsp.Utility.FillDDL(ddlPositionID, "eHRMSDB", "Position", "distinct PositionID", "Remark", Bsp.Utility.DisplayType.Full, "", "And CompID = 'SPHBK1'", "") '職位
        ddlPositionID.Items.Insert(0, New ListItem("　- -請選擇- -", ""))

        Bsp.Utility.FillDDL(ddlWorkStatus, "eHRMSDB", "WorkStatus", "WorkCode", "Remark", Bsp.Utility.DisplayType.Full, "", "", "") '職位
        ddlWorkStatus.Items.Insert(0, New ListItem("　- -請選擇- -", ""))

        ViewState.Item("OrgTypeColors") = New List(Of ArrayList)()
        ViewState.Item("DeptColors") = New List(Of ArrayList)()
        subLoadOrganColor(ddlDeptID, UserProfile.SelectCompRoleID)
        subLoadOrganColor(ddlOrgType, UserProfile.SelectCompRoleID)
        '科組課
        ddlDept_Changed(Nothing, Nothing)
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
            If Not String.IsNullOrEmpty(ob.ToString()) Then
                result = ob.ToString()
            End If
        End If
        Return result
    End Function
    Private Function DateStringIIF(ByVal dateStr As String) As String
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

    ''' <summary>
    ''' Format成資料庫能查詢員工姓名的格式
    ''' </summary>
    ''' <param name="name">String</param>
    ''' <returns>String</returns>
    ''' <remarks>例如: 吳O慶</remarks>
    Private Function getEmpNameSelectDBFormat(ByVal name As String) As String
        Dim result = ""
        If Not String.IsNullOrEmpty(name) Then
            If name.Length < 3 Then
                If name.Length = 2 Then
                    result = name.Substring(0, 1) + "O"
                Else
                    result = name
                End If

            ElseIf name.Length >= 3 Then
                result = name.Substring(0, 1) + "O" + name.Substring(name.Length - 1, 1)
            End If
        End If
        Return result
    End Function

    ''' <summary>
    ''' 顯示資料處理
    ''' </summary>
    ''' <param name="oldTable">DataTable</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Private Function getCusDataTable(ByVal oldTable As DataTable) As DataTable
        Dim cusDT As DataTable = New DataTable("CusDataTable")
        cusDT.Columns.Add("OTCompID", Type.GetType("System.String")) 'PK
        cusDT.Columns.Add("OTEmpID", Type.GetType("System.String")) 'PK
        cusDT.Columns.Add("OTStartDate", Type.GetType("System.String")) 'PK
        cusDT.Columns.Add("OTEndDate", Type.GetType("System.String")) 'PK
        cusDT.Columns.Add("OTSeq", Type.GetType("System.String")) 'PK
        cusDT.Columns.Add("FlowCaseID", Type.GetType("System.String"))
        cusDT.Columns.Add("OTTxnID", Type.GetType("System.String")) 'FlowCaseID
        cusDT.Columns.Add("GridViewIndex", Type.GetType("System.String")) '序號
        cusDT.Columns.Add("OTFormNO", Type.GetType("System.String")) '表單編號
        cusDT.Columns.Add("OTStatus", Type.GetType("System.String")) '狀態
        cusDT.Columns.Add("OTStatusName", Type.GetType("System.String")) '狀態Name
        cusDT.Columns.Add("OTEmpName", Type.GetType("System.String")) '加班人
        cusDT.Columns.Add("OTRegisterID", Type.GetType("System.String"))
        cusDT.Columns.Add("OTEmpRName", Type.GetType("System.String")) '填單人OTRegisterID
        cusDT.Columns.Add("ORT2ID", Type.GetType("System.String")) '單位類別ID
        cusDT.Columns.Add("ORT2Name", Type.GetType("System.String")) '單位類別名稱
        cusDT.Columns.Add("DeptID", Type.GetType("System.String")) '部門ID
        cusDT.Columns.Add("DeptName", Type.GetType("System.String")) '部門名稱
        cusDT.Columns.Add("ORTID", Type.GetType("System.String")) '科組課ID
        cusDT.Columns.Add("ORTName", Type.GetType("System.String")) '科組課名稱
        cusDT.Columns.Add("SalaryOrAdjust", Type.GetType("System.String")) '加班類型
        cusDT.Columns.Add("SalaryOrAdjustName", Type.GetType("System.String")) '加班類型Name
        cusDT.Columns.Add("OTDate", Type.GetType("System.String")) '加班日期
        cusDT.Columns.Add("OTStartDateShow", Type.GetType("System.String")) '加班日期 起
        cusDT.Columns.Add("OTEndDateShow", Type.GetType("System.String")) '加班日期 訖
        cusDT.Columns.Add("StartTime", Type.GetType("System.String")) '加班起時間
        cusDT.Columns.Add("EndTime", Type.GetType("System.String")) '加班訖時間
        cusDT.Columns.Add("StartTimeShow", Type.GetType("System.String")) '加班起時間(show)
        cusDT.Columns.Add("EndTimeShow", Type.GetType("System.String")) '加班訖時間(show)
        cusDT.Columns.Add("MealTime", Type.GetType("System.String"))
        cusDT.Columns.Add("OTTime", Type.GetType("System.String")) '加班起訖時間
        cusDT.Columns.Add("OTTotalTime", Type.GetType("System.String")) '加班時數
        cusDT.Columns.Add("OTTotalTimeM", Type.GetType("System.String")) '加班分鐘數
        cusDT.Columns.Add("OTTotalTime_D", Type.GetType("System.Decimal")) '加班時數Decimal
        cusDT.Columns.Add("OTTotalTime2", Type.GetType("System.String")) '補休時數
        cusDT.Columns.Add("OTTotalTime2_D", Type.GetType("System.Decimal")) '補休時數Decimal
        cusDT.Columns.Add("AdjustInvalidDate", Type.GetType("System.String")) '補休失效日
        cusDT.Columns.Add("AdjustInvalidDateShow", Type.GetType("System.String")) '補休失效日Format
        cusDT.Columns.Add("AttachID", Type.GetType("System.String")) '上傳附件ID
        cusDT.Columns.Add("FileName", Type.GetType("System.String")) '上傳附件Name
        cusDT.Columns.Add("OTPayDate", Type.GetType("System.String")) '計薪年月
        cusDT.Columns.Add("OTPayDateShow", Type.GetType("System.String")) '計薪年月(Format)
        cusDT.Columns.Add("CodeCName", Type.GetType("System.String"))
        cusDT.Columns.Add("OTReasonMemo", Type.GetType("System.String"))
        cusDT.Columns.Add("LastChgComp", Type.GetType("System.String"))
        cusDT.Columns.Add("LastChgID", Type.GetType("System.String"))
        cusDT.Columns.Add("LastChgDate", Type.GetType("System.String"))

        Dim OTTotalTime2Datas As New Dictionary(Of String, Decimal) '暫存該日期的補休加總時數

        For index As Integer = 0 To oldTable.Rows.Count - 1
            Dim oldRow = oldTable.Rows(index)
            Dim newRow = cusDT.NewRow()
            Dim sOTEmpID As String = StringIIF(oldRow.Item("OTEmpID"))
            Dim sOTStartDate As String = StringIIF(oldRow.Item("OTStartDate"))
            Dim sOTEndDate As String = StringIIF(oldRow.Item("OTEndDate"))
            Dim sOTEndTime As String = StringIIF(oldRow.Item("OTEndTime"))
            Dim sOTTotalTime As String = StringIIF(oldRow.Item("OTTotalTime"))
            Dim sMealFlag As String = StringIIF(oldRow.Item("MealFlag"))
            Dim sMealTime As String = StringIIF(oldRow.Item("MealTime"))
            Dim sEmp_AddDate As String = ""
            sEmp_AddDate = sOTEmpID & "," & sOTStartDate

            Dim dOTTotalTime_flash As Decimal = getOTTotalTime(sOTTotalTime, sMealTime, sMealFlag)
            If OTTotalTime2Datas.ContainsKey(sEmp_AddDate) Then
                OTTotalTime2Datas.Item(sEmp_AddDate) = OTTotalTime2Datas.Item(sEmp_AddDate) + dOTTotalTime_flash
            Else
                OTTotalTime2Datas.Add(sEmp_AddDate, dOTTotalTime_flash)
            End If

            If StringIIF(oldRow.Item("OTSeqNo")) = "1" Then

                '查詢跨日迄日資料(合併處理)
                Dim oldRowData_E As DataRow = (From column In oldTable.Rows _
                                       Where column("OTTxnID") = oldRow.Item("OTTxnID") _
                                       And column("OTSeqNo") = "2" _
                                       ).FirstOrDefault()
                If oldRowData_E IsNot Nothing Then
                    If StringIIF(oldRowData_E.Item("OTSeqNo")) = "2" Then
                        sOTEndDate = StringIIF(oldRowData_E.Item("OTEndDate"))
                        sOTEndTime = StringIIF(oldRowData_E.Item("OTEndTime"))

                        Dim sOTTotalTime_S As Decimal = 0
                        Dim sOTTotalTime_E As Decimal = 0
                        If sOTTotalTime <> "" And Decimal.TryParse(sOTTotalTime, sOTTotalTime_S) And StringIIF(oldRowData_E.Item("OTTotalTime")) <> "" And Decimal.TryParse(StringIIF(oldRowData_E.Item("OTTotalTime")), sOTTotalTime_E) Then
                            sOTTotalTime = (sOTTotalTime_S + sOTTotalTime_E).ToString("0.0")
                        End If

                        Dim sMealTime_S As Decimal = 0
                        Dim sMealTime_E As Decimal = 0
                        If sMealTime <> "" And Decimal.TryParse(sMealTime, sMealTime_S) And StringIIF(oldRowData_E.Item("MealTime")) <> "" And Decimal.TryParse(StringIIF(oldRowData_E.Item("MealTime")), sMealTime_E) Then
                            sMealTime = (sMealTime_S + sMealTime_E).ToString("0")
                        End If
                    End If
                End If
                '計算加班時數
                Dim dOTTotalTime As Decimal = getOTTotalTime(sOTTotalTime, sMealTime, sMealFlag)

                newRow.Item("OTCompID") = StringIIF(oldRow.Item("OTCompID"))
                newRow.Item("OTEmpID") = StringIIF(oldRow.Item("OTEmpID"))
                newRow.Item("OTStartDate") = sOTStartDate
                newRow.Item("OTEndDate") = sOTEndDate
                newRow.Item("OTTxnID") = StringIIF(oldRow.Item("OTTxnID"))
                newRow.Item("OTSeq") = StringIIF(oldRow.Item("OTSeq"))
                newRow.Item("FlowCaseID") = StringIIF(oldRow.Item("FlowCaseID"))
                newRow.Item("GridViewIndex") = StringIIF(index + 1)
                newRow.Item("OTFormNO") = StringIIF(oldRow.Item("OTFormNO"))
                newRow.Item("OTStatus") = StringIIF(oldRow.Item("OTStatus"))
                newRow.Item("OTStatusName") = getOTStatusName(StringIIF(oldRow.Item("OTStatus")))
                newRow.Item("OTEmpName") = StringIIF(oldRow.Item("Name"))
                newRow.Item("OTRegisterID") = StringIIF(oldRow.Item("OTRegisterID"))
                newRow.Item("OTEmpRName") = StringIIF(oldRow.Item("RName"))
                newRow.Item("ORT2ID") = StringIIF(oldRow.Item("ORT2ID"))
                newRow.Item("ORT2Name") = StringIIF(oldRow.Item("ORT2Name"))
                newRow.Item("DeptID") = StringIIF(oldRow.Item("DeptID"))
                newRow.Item("DeptName") = StringIIF(oldRow.Item("DeptName"))
                newRow.Item("ORTID") = StringIIF(oldRow.Item("ORTID"))
                newRow.Item("ORTName") = StringIIF(oldRow.Item("ORTName"))
                newRow.Item("SalaryOrAdjust") = StringIIF(oldRow.Item("SalaryOrAdjust"))
                newRow.Item("SalaryOrAdjustName") = getSalaryOrAdjustName(StringIIF(oldRow.Item("SalaryOrAdjust")))
                newRow.Item("OTDate") = sOTStartDate + "~" + sOTEndDate
                newRow.Item("StartTime") = StringIIF(oldRow.Item("OTStartTime"))
                newRow.Item("EndTime") = sOTEndTime
                newRow.Item("StartTimeShow") = getTimeFormat(StringIIF(oldRow.Item("OTStartTime")))
                newRow.Item("EndTimeShow") = getTimeFormat(sOTEndTime)
                newRow.Item("MealTime") = StringIIF(sMealTime)
                newRow.Item("OTTime") = getTimeFormat(StringIIF(oldRow.Item("OTStartTime"))) + "~" + getTimeFormat(sOTEndTime)
                newRow.Item("OTTotalTime") = CDec(FormatNumber((dOTTotalTime / 60), 1)).ToString("0.0")
                newRow.Item("OTTotalTimeM") = dOTTotalTime.ToString("0.0")
                newRow.Item("OTTotalTime2") = "0.0"
                newRow.Item("AdjustInvalidDate") = StringIIF(oldRow.Item("AdjustInvalidDate"))
                newRow.Item("AdjustInvalidDateShow") = getDataTimeStr(StringIIF(oldRow.Item("AdjustInvalidDate")), "yyyy/MM/dd")
                newRow.Item("OTPayDate") = StringIIF(oldRow.Item("OTPayDate"))
                newRow.Item("OTPayDateShow") = getOTPayDateFormat(StringIIF(oldRow.Item("OTPayDate")))
                newRow.Item("FileName") = StringIIF(oldRow.Item("FileName"))
                newRow.Item("CodeCName") = StringIIF(oldRow.Item("CodeCName"))
                newRow.Item("OTReasonMemo") = StringIIF(oldRow.Item("OTReasonMemo"))
                newRow.Item("LastChgComp") = StringIIF(oldRow.Item("LastChgComp"))
                newRow.Item("LastChgID") = StringIIF(oldRow.Item("LastChgID"))
                newRow.Item("LastChgDate") = StringIIF(oldRow.Item("LastChgDate"))
                cusDT.Rows.Add(newRow)
            End If
        Next
        If cusDT.Rows.Count > 0 Then
            '畫面資料排序
            Dim olderBycusDT As DataTable = _
                (From cus In cusDT.AsEnumerable() _
                 Select cus _
                 Order By _
                 cus.Field(Of String)("OTCompID"), _
                 cus.Field(Of String)("OTEmpID"), _
                 cus.Field(Of String)("OTStartDate"), _
                 cus.Field(Of String)("OTEndDate"), _
                 cus.Field(Of String)("OTSeq") _
                 Descending).CopyToDataTable()

            '計算補休時數
            For index As Integer = 0 To olderBycusDT.Rows.Count - 1
                Dim row = olderBycusDT.Rows(index)
                Dim sOTEmpID As String = StringIIF(row.Item("OTEmpID"))
                Dim sOTStartDate As String = StringIIF(row.Item("OTStartDate"))
                Dim sOTEndDate As String = StringIIF(row.Item("OTEndDate"))
                Dim sOTStartTime As String = StringIIF(row.Item("OTEndDate"))
                Dim sOTEndTime As String = StringIIF(row.Item("OTEndDate"))
                Dim sOTTotalTime As String = StringIIF(row.Item("OTTotalTimeM"))
                Dim sOTTotalTime2 = ""
                Dim dOTTotalTime As Decimal = 0
                Dim dOTTotalTime2 As Decimal = 0
                Decimal.TryParse(sOTTotalTime, dOTTotalTime)
                Dim sEmp_AddDate As String = ""
                sEmp_AddDate = sOTEmpID & "," & sOTStartDate

                If sOTStartDate = sOTEndDate Then
                    Dim dOTTotalTime2Sum As Decimal = OTTotalTime2Datas.Item(sEmp_AddDate)
                    If dOTTotalTime2Sum > 0 Then
                        If IsHoliday(sOTStartDate) Then '是假日
                            If (dOTTotalTime2Sum <= 240) Then
                                dOTTotalTime2 = 240
                                OTTotalTime2Datas.Item(sEmp_AddDate) = dOTTotalTime2Sum - 240
                            ElseIf (dOTTotalTime2Sum <= 480) Then
                                dOTTotalTime2 = 480
                                OTTotalTime2Datas.Item(sEmp_AddDate) = dOTTotalTime2Sum - 480
                            ElseIf (dOTTotalTime2Sum <= 720) Then
                                dOTTotalTime2 = 720
                                OTTotalTime2Datas.Item(sEmp_AddDate) = dOTTotalTime2Sum - 720
                            End If
                        Else
                            If dOTTotalTime >= dOTTotalTime2Sum Then
                                dOTTotalTime2 = dOTTotalTime2Sum
                            Else
                                dOTTotalTime2 = dOTTotalTime
                                OTTotalTime2Datas.Item(sEmp_AddDate) = dOTTotalTime2Sum - dOTTotalTime
                            End If
                        End If

                    End If

                Else
                    Dim eEmp_AddDate As String = ""
                    eEmp_AddDate = sOTEmpID & "," & sOTEndDate
                    Dim dOTTotalTime2Sum As Decimal = OTTotalTime2Datas.Item(sEmp_AddDate)
                    dOTTotalTime2Sum = Math.Abs(dOTTotalTime2Sum)
                    Dim dOTTotalTime2Sum_E As Decimal = OTTotalTime2Datas.Item(eEmp_AddDate)
                    If dOTTotalTime2Sum > 0 Then
                        If IsHoliday(sOTStartDate) Then '是假日
                            If (dOTTotalTime2Sum <= 240) Then
                                dOTTotalTime2 = 240
                                OTTotalTime2Datas.Item(sEmp_AddDate) = dOTTotalTime2Sum - 240
                            ElseIf (dOTTotalTime2Sum <= 480) Then
                                dOTTotalTime2 = 480
                                OTTotalTime2Datas.Item(sEmp_AddDate) = dOTTotalTime2Sum - 480
                            ElseIf (dOTTotalTime2Sum <= 720) Then
                                dOTTotalTime2 = 720
                                OTTotalTime2Datas.Item(sEmp_AddDate) = dOTTotalTime2Sum - 720
                            End If
                        Else
                            If dOTTotalTime >= dOTTotalTime2Sum Then
                                dOTTotalTime2 = dOTTotalTime2Sum
                            Else
                                dOTTotalTime2 = dOTTotalTime
                                OTTotalTime2Datas.Item(sEmp_AddDate) = dOTTotalTime2Sum - dOTTotalTime
                            End If
                        End If

                    End If
                    If dOTTotalTime2Sum_E > 0 Then
                        If IsHoliday(sOTEndDate) Then '是假日
                            If (dOTTotalTime - dOTTotalTime2Sum_E) > 0 Then
                                If ((dOTTotalTime2Sum_E) <= 240) Then
                                    dOTTotalTime2 += 240
                                    OTTotalTime2Datas.Item(eEmp_AddDate) = dOTTotalTime2Sum_E - 240
                                ElseIf ((dOTTotalTime2Sum_E) <= 480) Then
                                    dOTTotalTime2 += 480
                                    OTTotalTime2Datas.Item(eEmp_AddDate) = dOTTotalTime2Sum_E - 480
                                ElseIf ((dOTTotalTime2Sum_E) <= 12) Then
                                    dOTTotalTime2 += 720
                                    OTTotalTime2Datas.Item(eEmp_AddDate) = dOTTotalTime2Sum_E - 720
                                End If
                            ElseIf (dOTTotalTime - dOTTotalTime2Sum_E) = 0 Then '第一天扣完的情況
                                If (dOTTotalTime2Sum_E <= 240) Then
                                    dOTTotalTime2 = 240
                                    OTTotalTime2Datas.Item(eEmp_AddDate) = dOTTotalTime2Sum_E - 240
                                ElseIf (dOTTotalTime2Sum_E <= 480) Then
                                    dOTTotalTime2 = 480
                                    OTTotalTime2Datas.Item(eEmp_AddDate) = dOTTotalTime2Sum_E - 480
                                ElseIf (dOTTotalTime2Sum_E <= 720) Then
                                    dOTTotalTime2 = 720
                                    OTTotalTime2Datas.Item(eEmp_AddDate) = dOTTotalTime2Sum_E - 720
                                End If
                            End If

                        Else

                            If (dOTTotalTime - dOTTotalTime2Sum_E) >= dOTTotalTime2Sum_E Then
                                'If dOTTotalTime2Sum_E + dOTTotalTime2Sum <> dOTTotalTime Then
                                '    dOTTotalTime2Sum_E = dOTTotalTime - dOTTotalTime2Sum
                                'End If
                                dOTTotalTime2 = dOTTotalTime2Sum_E + dOTTotalTime2
                            Else
                                dOTTotalTime2 = (dOTTotalTime - dOTTotalTime2Sum) + dOTTotalTime2
                                OTTotalTime2Datas.Item(eEmp_AddDate) = dOTTotalTime2Sum_E - (dOTTotalTime - dOTTotalTime2Sum_E)
                            End If
                        End If

                    End If
                End If
                dOTTotalTime2 = CDbl(FormatNumber((dOTTotalTime2 / 60), 1))
                sOTTotalTime2 = dOTTotalTime2.ToString("0.0")
                row.Item("OTTotalTime2") = sOTTotalTime2
            Next

            Return olderBycusDT
        Else
            Return cusDT
        End If

    End Function

    ''' <summary>
    ''' 加班時數扣除用餐時數
    ''' </summary>
    ''' <param name="sOTTotalTime">String</param>
    ''' <param name="sMealTime">String</param>
    ''' <param name="sMealFlag">String</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Private Function getOTTotalTime(ByVal sOTTotalTime As String, ByVal sMealTime As String, ByVal sMealFlag As String) As Decimal
        Dim dOTTotalTime As Decimal = 0
        If sOTTotalTime <> "" And Decimal.TryParse(sOTTotalTime, dOTTotalTime) Then
            Dim dMealTime As Decimal = 0
            If sMealFlag = "1" And sMealTime <> "" And Decimal.TryParse(sMealTime, dMealTime) Then
                dOTTotalTime = (dOTTotalTime - dMealTime)
            End If
        End If
        Return dOTTotalTime
    End Function

    ''' <summary>
    ''' 是否為假日
    ''' </summary>
    ''' <param name="sDate">String</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Private Function IsHoliday(ByVal sDate As String) As Boolean
        Dim result = True
        result = OVBusinessCommon.CheckHolidayOrNot(sDate)
        Return result
    End Function

    ''' <summary>
    ''' 取得格式化後的時間
    ''' </summary>
    ''' <param name="time">String</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Private Function getTimeFormat(ByVal time As String) As String
        Dim result = ""
        If time <> "" And time.Length = 4 Then
            result = time.Substring(0, 2) + ":" + time.Substring(2, 2)
        End If
        Return result
    End Function

    ''' <summary>
    ''' 取得格式化後的DataTime
    ''' </summary>
    ''' <param name="dateStr">String</param>
    ''' <param name="format">String</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Private Function getDataTimeStr(ByVal dateStr As String, ByVal format As String) As String
        Dim result = ""
        Dim newDate As Date = New Date()
        If dateStr <> "" And dateStr <> "1900-01-01 00:00:00.000" And dateStr <> "1900/1/1 上午 12:00:00" And Date.TryParse(dateStr, newDate) Then
            result = newDate.ToString(format)
        End If
        Return result
    End Function

    ''' <summary>
    ''' 取得格式化後的計薪年月
    ''' </summary>
    ''' <param name="payDate">String</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getOTPayDateFormat(ByVal payDate As String) As String
        Dim result = ""
        If payDate <> "" And payDate.Length = 6 Then
            result = payDate.Substring(0, 4) + "/" + payDate.Substring(4, 2)
        End If
        Return result
    End Function

    ''' <summary>
    ''' 取的轉薪資/補休中文
    ''' </summary>
    ''' <param name="salaryOrAdjust">String</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Private Function getSalaryOrAdjustName(ByVal salaryOrAdjust As String) As String
        Dim result = ""
        Select Case salaryOrAdjust
            Case "1"
                result = "轉薪資"
            Case "2"
                result = "轉補休"
        End Select
        Return result
    End Function

    ''' <summary>
    ''' 取的申報單狀態中文
    ''' </summary>
    ''' <param name="status">String</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Private Function getOTStatusName(ByVal status As String) As String
        Dim result = ""
        Select Case status
            Case "1"
                result = "暫存"
            Case "2"
                result = "送簽"
            Case "3"
                result = "核准"
            Case "4"
                result = "駁回"
            Case "5"
                result = "刪除"
            Case "6"
                result = "取消"
            Case "7"
                result = "作廢"
            Case "8"
                result = ""
            Case "9"
                result = "計薪後收回"
        End Select
        Return result
    End Function

    ''' <summary>
    ''' 下拉選單選取的樣式設定
    ''' </summary>
    ''' <param name="objDDL">DropDownList</param>
    ''' <remarks></remarks>
    Private Sub subReLoadColor(ByVal objDDL As DropDownList)
        If objDDL.Items.Count > 0 Then
            Dim ArrColors As New List(Of ArrayList)()

            Select Case objDDL.ID
                Case "ddlOrgType"
                    ArrColors = ViewState.Item("OrgTypeColors")
                Case "ddlDeptID"
                    ArrColors = ViewState.Item("DeptColors")
            End Select

            For Each item As ArrayList In ArrColors
                Dim list As ListItem = objDDL.Items.FindByValue(item(0))
                If Not list Is Nothing Then
                    list.Attributes.Add("style", "background-color:" + item(1))
                End If
            Next
        End If
    End Sub

    ''' <summary>
    ''' 下拉選單顏色配置
    ''' </summary>
    ''' <param name="objDDL">DropDownList</param>
    ''' <param name="strCompID">String</param>
    ''' <remarks></remarks>
    Private Sub subLoadOrganColor(ByVal objDDL As DropDownList, ByVal strCompID As String)
        Dim strSQL As New StringBuilder
        strSQL.AppendLine("Select O.OrganID")
        'strSQL.AppendLine(", Case When OrgType = DeptID Then OrganID + '-' + OrganName + '-' + RoleCode Else '　　' + OrganID + '-' + OrganName + '-' + RoleCode End OrganName")
        strSQL.AppendLine(", Case When OrgType = DeptID Then OrganID + '-' + OrganName Else '　　' + OrganID + '-' + OrganName End OrganName")
        strSQL.AppendLine(", IsNull(W.Color, '#FFFFFF') Color")
        strSQL.AppendLine("From Organization O")
        strSQL.AppendLine("Left Join OrganColor_Web W On O.CompID = W.CompID and O.SortOrder = W.SortOrder")
        If objDDL.ID = "ddlOrgType" Then
            strSQL.AppendLine("Where OrganID = OrgType")
        Else
            strSQL.AppendLine("Where OrganID = DeptID")
        End If
        strSQL.AppendLine("And O.CompID = " & Bsp.Utility.Quote(strCompID))
        strSQL.AppendLine("And VirtualFlag = '0'")
        strSQL.AppendLine("And InValidFlag = '0'")
        strSQL.AppendLine("Order By O.InValidFlag, O.SortOrder, Right(O.GroupType, 1), O.GroupID, O.OrganID")

        Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
            objDDL.Items.Clear()
            Dim ArrColors As New List(Of ArrayList)()

            If dt.Rows.Count > 0 Then
                For Each item As DataRow In dt.Rows
                    Dim ListOpt As ListItem = New ListItem()
                    ListOpt.Value = item("OrganID").ToString()
                    ListOpt.Text = item("OrganName").ToString()

                    If item("Color").ToString().Trim() <> "#FFFFFF" Then
                        ListOpt.Attributes.Add("style", "background-color:" + item("Color").ToString().Trim())

                        Dim ArrColor As New ArrayList()
                        ArrColor.Add(item("OrganID").ToString())
                        ArrColor.Add(item("Color").ToString().Trim())
                        ArrColors.Add(ArrColor)
                    End If

                    objDDL.Items.Add(ListOpt)
                Next
            End If

            objDDL.Items.Insert(0, New ListItem("---請選擇---", ""))

            Select Case objDDL.ID
                Case "ddlOrgType"
                    ViewState.Item("OrgTypeColors") = ArrColors
                Case "ddlDeptID"
                    ViewState.Item("DeptColors") = ArrColors
            End Select
        End Using
    End Sub
    Private Sub subLoadOrganColor(ByVal objDDL As DropDownList, ByVal strCompID As String, ByVal OrgType As String)
        Dim strSQL As New StringBuilder
        strSQL.AppendLine("Select O.OrganID")
        'strSQL.AppendLine(", Case When OrgType = DeptID Then OrganID + '-' + OrganName + '-' + RoleCode Else '　　' + OrganID + '-' + OrganName + '-' + RoleCode End OrganName")
        strSQL.AppendLine(", Case When OrgType = DeptID Then OrganID + '-' + OrganName Else '　　' + OrganID + '-' + OrganName End OrganName")
        strSQL.AppendLine(", IsNull(W.Color, '#FFFFFF') Color")
        strSQL.AppendLine("From Organization O")
        strSQL.AppendLine("Left Join OrganColor_Web W On O.CompID = W.CompID and O.SortOrder = W.SortOrder")
        If objDDL.ID = "ddlOrgType" Then
            strSQL.AppendLine("Where OrganID = OrganID AND OrganID = DeptID AND OrganID = OrgType")
        Else
            strSQL.AppendLine("Where ((OrganID = OrganID AND OrganID = DeptID AND OrganID <> OrgType)OR(OrganID = OrganID AND OrganID = DeptID AND OrganID = OrgType))")
        End If
        strSQL.AppendLine("And O.CompID = " & Bsp.Utility.Quote(strCompID))
        'If Not chkOrganName.Checked Then
        '    strSQL.AppendLine("And InValidFlag = '0'")
        'End If

        strSQL.AppendLine("And VirtualFlag = '0'")


        If objDDL.ID = "ddlOrgType" Then

        Else
            strSQL.AppendLine(" and O.OrgType='" + OrgType + "'")
        End If
        strSQL.AppendLine("Order By O.InValidFlag, O.SortOrder, Right(O.GroupType, 1), O.GroupID, O.OrganID")


        Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
            objDDL.Items.Clear()
            Dim ArrColors As New List(Of ArrayList)()

            If dt.Rows.Count > 0 Then
                For Each item As DataRow In dt.Rows
                    Dim ListOpt As ListItem = New ListItem()
                    ListOpt.Value = item("OrganID").ToString()
                    ListOpt.Text = item("OrganName").ToString()

                    If item("Color").ToString().Trim() <> "#FFFFFF" Then
                        ListOpt.Attributes.Add("style", "background-color:" + item("Color").ToString().Trim())

                        Dim ArrColor As New ArrayList()
                        ArrColor.Add(item("OrganID").ToString())
                        ArrColor.Add(item("Color").ToString().Trim())
                        ArrColors.Add(ArrColor)
                    End If

                    objDDL.Items.Add(ListOpt)
                Next
            End If

            objDDL.Items.Insert(0, New ListItem("---請選擇---", ""))

            Select Case objDDL.ID
                Case "ddlOrgType"
                    ViewState.Item("OrgTypeColors") = ArrColors
                Case "ddlDeptID"
                    ViewState.Item("DeptColors") = ArrColors
            End Select
        End Using
    End Sub
#End Region

    Protected Sub gvMain_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMain.RowCommand
        If e.CommandName = "Detail" Then
            Dim i As Integer = Convert.ToInt32(e.CommandArgument)
            'Dim empName As String = gvMain.DataKeys(selectedRows(gvMain))("OTEmpName").ToString
            _goUpdateDatas = getCusDataTable(_queryDatas)

            Dim jsAry As New JArray
            Dim jsObj As New JObject
            Dim jsStr As String = ""

            Dim compID As String = gvMain.DataKeys(i).Item("OTCompID").ToString
            Dim empID As String = gvMain.DataKeys(i).Item("OTEmpID").ToString
            Dim empName As String = gvMain.DataKeys(i).Item("OTEmpName").ToString
            Dim salaryOrAdjust As String = gvMain.DataKeys(i).Item("SalaryOrAdjustName").ToString
            Dim adjustInvalidDate As String = gvMain.DataKeys(i).Item("AdjustInvalidDateShow").ToString
            Dim OTDate() As String = gvMain.DataKeys(i).Item("OTDate").ToString.Split("~")
            Dim OTTime() As String = gvMain.DataKeys(i).Item("OTTime").ToString.Split("~")

            jsObj.Add("OTCompID", compID)
            jsObj.Add("OTEmpID", empID)
            jsObj.Add("OTEmpName", empName)
            jsObj.Add("SalaryOrAdjustName", salaryOrAdjust)
            jsObj.Add("AdjustInvalidDateShow", adjustInvalidDate)
            jsObj.Add("OTStartDate", OTDate(0))
            jsObj.Add("OTEndDate", OTDate(1))
            jsObj.Add("OTStartTime", OTTime(0).Replace(":", ""))
            jsObj.Add("OTEndTime", OTTime(1).Replace(":", ""))
            jsAry.Add(jsObj)

            jsStr = JsonConvert.SerializeObject(jsAry, Formatting.None)
            _goSelectDatas = jsStr

            Dim btnX As New ButtonState(ButtonState.emButtonType.Delete)
            btnX.Caption = "返回"
            Me.TransferFramePage("~/OV/OV2103.aspx", New ButtonState() {btnX}, _
                        ddlCompID.ID & "=" & ddlCompID.SelectedValue, _
                        ddlOrgType.ID & "=" & ddlOrgType.SelectedValue, _
                        ddlDeptID.ID & "=" & ddlDeptID.SelectedValue, _
                        ddlOrganID.ID & "=" & ddlOrganID.SelectedValue, _
                        tbOTEmpID.ID & "=" & tbOTEmpID.Text, _
                        tbOTEmpName.ID & "=" & tbOTEmpName.Text, _
                        ddlWorkStatus.ID & "=" & ddlWorkStatus.SelectedValue, _
                        ddlOrganID.ID & "=" & ddlOrganID.SelectedValue, _
                        ddlRankIDMIN.ID & "=" & ddlRankIDMIN.SelectedValue, _
                        ddlRankIDMAX.ID & "=" & ddlRankIDMAX.SelectedValue, _
                        ddlTitleIDMIN.ID & "=" & ddlTitleIDMIN.SelectedValue, _
                        ddlTitleIDMAX.ID & "=" & ddlTitleIDMAX.SelectedValue, _
                        ddlPositionID.ID & "=" & ddlPositionID.SelectedValue, _
                        ucFailDateStart.ID & "=" & DateStringIIF(ucFailDateStart.DateText), _
                        ucFailDateEnd.ID & "=" & DateStringIIF(ucFailDateEnd.DateText), _
                        ucStartDate.ID & "=" & DateStringIIF(ucStartDate.DateText), _
                        ucEndDate.ID & "=" & DateStringIIF(ucEndDate.DateText), _
                        StartTimeH.ID & "=" & StartTimeH.SelectedValue, _
                        StartTimeM.ID & "=" & StartTimeM.SelectedValue, _
                        EndTimeH.ID & "=" & EndTimeH.SelectedValue, _
                        EndTimeM.ID & "=" & EndTimeM.SelectedValue, _
                        tbOTPayDate.ID & "=" & tbOTPayDate.Text, _
                        "PageNo=" & pcMain.PageNo.ToString(), _
                        "DoQuery=" & "Y")

        End If

    End Sub
    Protected Sub ddlOrgType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlOrgType.SelectedIndexChanged
        If Not ddlOrgType.SelectedValue = "" Then
            subLoadOrganColor(ddlDeptID, UserProfile.SelectCompRoleID, ddlOrgType.SelectedValue)
            Bsp.Utility.FillDDL(ddlOrganID, "eHRMSDB", "Organization", "RTRIM(OrganID)", "OrganName", Bsp.Utility.DisplayType.Full, "", " And VirtualFlag = '0' And CompID = " & Bsp.Utility.Quote(UserProfile.SelectCompRoleID) & " And OrgType = " & Bsp.Utility.Quote(ddlOrgType.SelectedValue) & " AND ((OrganID = OrganID AND OrganID <> DeptID AND OrganID <> OrgType)OR(OrganID = OrganID AND OrganID = DeptID AND OrganID = OrgType)) ", "Order By OrganID")
            ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
        Else
            subLoadOrganColor(ddlDeptID, UserProfile.SelectCompRoleID)
            If ddlDeptID.SelectedValue = "" Then
                Bsp.Utility.FillDDL(ddlOrganID, "eHRMSDB", "Organization", "RTRIM(OrganID)", "OrganName", Bsp.Utility.DisplayType.Full, "", " And VirtualFlag = '0' And CompID = " & Bsp.Utility.Quote(UserProfile.SelectCompRoleID) & " AND ((OrganID = OrganID AND OrganID <> DeptID AND OrganID <> OrgType)OR(OrganID = OrganID AND OrganID = DeptID AND OrganID = OrgType)) ", "Order By OrganID")
                ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
            Else
                Bsp.Utility.FillDDL(ddlOrganID, "eHRMSDB", "Organization", "RTRIM(OrganID)", "OrganName", Bsp.Utility.DisplayType.Full, "", " And VirtualFlag = '0' And CompID = " & Bsp.Utility.Quote(UserProfile.SelectCompRoleID) & " And DeptID = " & Bsp.Utility.Quote(ddlDeptID.SelectedValue) & " AND ((OrganID = OrganID AND OrganID <> DeptID AND OrganID <> OrgType)OR(OrganID = OrganID AND OrganID = DeptID AND OrganID = OrgType)) ", "Order By OrganID")
                ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
            End If
        End If

        Dim str As String = "join " & eHRMSDB_ITRD & ".[dbo].EmpPosition EP ON PO.CompID=EP.CompID AND PO.PositionID=EP.PositionID AND EP.PrincipalFlag='1' join " & eHRMSDB_ITRD & ".[dbo].Personal P ON EP.CompID=P.CompID AND EP.EmpID=P.EmpID  join " & eHRMSDB_ITRD & ".[dbo].Organization ORT ON  P.OrganID=ORT.OrganID AND P.CompID =ORT.CompID"

        If (Not ddlOrganID.SelectedValue.ToString.Trim.Equals("")) Then
            Bsp.Utility.FillDDL(ddlPositionID, "eHRMSDB", "Position PO", "distinct PO.PositionID", "PO.Remark", Bsp.Utility.DisplayType.Full, str, "And PO.CompID = 'SPHBK1' and ORT.OrganID= '" + ddlOrganID.SelectedValue + "'", "Order By  PO.PositionID") '職位
            ddlPositionID.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        ElseIf (Not ddlDeptID.SelectedValue.ToString.Trim.Equals("")) Then
            Bsp.Utility.FillDDL(ddlPositionID, "eHRMSDB", "Position PO", "distinct PO.PositionID", "PO.Remark", Bsp.Utility.DisplayType.Full, str, "And PO.CompID = 'SPHBK1' and P.DeptID= '" + ddlDeptID.SelectedValue + "'", "Order By  PO.PositionID") '職位
            ddlPositionID.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        ElseIf (Not ddlOrgType.SelectedValue.ToString.Trim.Equals("")) Then
            Bsp.Utility.FillDDL(ddlPositionID, "eHRMSDB", "Position PO", "distinct PO.PositionID", "PO.Remark", Bsp.Utility.DisplayType.Full, str, "And PO.CompID = 'SPHBK1' and ORT.OrgType= '" + ddlOrgType.SelectedValue + "'", "Order By  PO.PositionID") '職位
            ddlPositionID.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        Else
            Bsp.Utility.FillDDL(ddlPositionID, "eHRMSDB", "Position PO", "distinct PO.PositionID", "PO.Remark", Bsp.Utility.DisplayType.Full, str, "And PO.CompID = 'SPHBK1'", "Order By  PO.PositionID") '職位
            ddlPositionID.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        End If


    End Sub

    Protected Sub ddlOrganID_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlOrganID.SelectedIndexChanged
        Dim str As String = "join " & eHRMSDB_ITRD & ".[dbo].EmpPosition EP ON PO.CompID=EP.CompID AND PO.PositionID=EP.PositionID AND EP.PrincipalFlag='1' join " & eHRMSDB_ITRD & ".[dbo].Personal P ON EP.CompID=P.CompID AND EP.EmpID=P.EmpID  join " & eHRMSDB_ITRD & ".[dbo].Organization ORT ON  P.OrganID=ORT.OrganID AND P.CompID =ORT.CompID"

        If (Not ddlOrganID.SelectedValue.ToString.Trim.Equals("")) Then
            Bsp.Utility.FillDDL(ddlPositionID, "eHRMSDB", "Position PO", "distinct PO.PositionID", "PO.Remark", Bsp.Utility.DisplayType.Full, str, "And PO.CompID = 'SPHBK1' and ORT.OrganID= '" + ddlOrganID.SelectedValue + "'", "Order By  PO.PositionID") '職位
            ddlPositionID.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        ElseIf (Not ddlDeptID.SelectedValue.ToString.Trim.Equals("")) Then
            Bsp.Utility.FillDDL(ddlPositionID, "eHRMSDB", "Position PO", "distinct PO.PositionID", "PO.Remark", Bsp.Utility.DisplayType.Full, str, "And PO.CompID = 'SPHBK1' and P.DeptID= '" + ddlDeptID.SelectedValue + "'", "Order By  PO.PositionID") '職位
            ddlPositionID.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        ElseIf (Not ddlOrgType.SelectedValue.ToString.Trim.Equals("")) Then
            Bsp.Utility.FillDDL(ddlPositionID, "eHRMSDB", "Position PO", "distinct PO.PositionID", "PO.Remark", Bsp.Utility.DisplayType.Full, str, "And PO.CompID = 'SPHBK1' and ORT.OrgType= '" + ddlOrgType.SelectedValue + "'", "Order By  PO.PositionID") '職位
            ddlPositionID.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        Else
            Bsp.Utility.FillDDL(ddlPositionID, "eHRMSDB", "Position PO", "distinct PO.PositionID", "PO.Remark", Bsp.Utility.DisplayType.Full, str, "And PO.CompID = 'SPHBK1'", "Order By  PO.PositionID") '職位
            ddlPositionID.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        End If
    End Sub

    Public Overrides Sub DoModalReturn(ByVal returnValue As String)
        If returnValue <> "" Then
            Dim aryData() As String = returnValue.Split(":")
            Select Case aryData(0)
                Case "ucQueryEmpID" '員編uc
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    tbOTEmpID.Text = aryValue(1)
                    lblEmpID.Text = aryValue(2)
            End Select
        End If
    End Sub

    Protected Sub tbOTEmpID_TextChanged(sender As Object, e As System.EventArgs) Handles tbOTEmpID.TextChanged
        If tbOTEmpID.Text <> "" And tbOTEmpID.Text.Length = 6 Then
            Dim objHR As New HR
            Dim rtnTable As DataTable = objHR.GetHREmpName(UserProfile.SelectCompRoleID, tbOTEmpID.Text)
            If rtnTable.Rows.Count <= 0 Then
                lblEmpID.Text = ""
                tbOTEmpID.Text = ""
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", "人事資料尚未建檔")
            Else
                lblEmpID.Text = rtnTable.Rows(0).Item(0)
            End If
        Else
            lblEmpID.Text = ""
        End If
    End Sub

    Protected Sub ddlRankIDMIN_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlRankIDMIN.SelectedIndexChanged, ddlRankIDMAX.SelectedIndexChanged
        Dim strCompID As String = ddlCompID.SelectedValue
        If strCompID = "" Then
            strCompID = UserProfile.SelectCompRoleID
        End If

        Dim ddlRank As DropDownList = CType(sender, DropDownList)
        Dim ddlTitle As DropDownList = Me.FindControl(ddlRank.ID.Replace("Rank", "Title"))

        If ddlRank.SelectedValue = "" Then
            ddlTitle.Items.Clear()
            ddlTitle.Items.Insert(0, New ListItem("---請先選擇職等---", ""))
        Else
            Bsp.Utility.FillDDL(ddlTitle, "eHRMSDB", "Title", "distinct TitleID", "TitleName", Bsp.Utility.DisplayType.Full, "", IIf(strCompID = "0", "", "and CompID = " & Bsp.Utility.Quote(strCompID)) & " and RankID = " & Bsp.Utility.Quote(ddlRank.SelectedValue))
            ddlTitle.Items.Insert(0, New ListItem("---請選擇---", ""))
        End If
    End Sub
End Class

