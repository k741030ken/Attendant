'****************************************************
'功能說明：公司班別設定
'建立人員：MickySung
'建立日期：2015.05.19
'****************************************************
Imports System.Data

Partial Class PA_PA2100
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objSC As New SC()

        If Not IsPostBack Then
            '公司代碼
            If UserProfile.SelectCompRoleID = "ALL" Then
                lblCompRoleID.Visible = False
                Bsp.Utility.FillHRCompany(ddlCompID)
                Page.SetFocus(ddlCompID)

                '班別代碼
                PA2.FillWTID_PA2100(ddlWTID, ddlCompID.SelectedValue)
                ddlWTID.Items.Insert(0, New ListItem("---請選擇---", ""))
            Else
                '2015/05/28 公司代碼-名稱改寫法
                lblCompRoleID.Text = UserProfile.SelectCompRoleName
                'lblCompRoleID.Text = UserProfile.SelectCompRoleID + "-" + objSC.GetCompName(UserProfile.SelectCompRoleID).Rows(0).Item("CompName").ToString
                ddlCompID.Visible = False

                '班別代碼
                PA2.FillWTID_PA2100(ddlWTID, UserProfile.SelectCompRoleID)
                ddlWTID.Items.Insert(0, New ListItem("---請選擇---", ""))
            End If

            '班別類型
            Bsp.Utility.FillDDL(ddlWTIDTypeFlag, "eHRMSDB", "HRCodeMap", "Code", "CodeCName", Bsp.Utility.DisplayType.Full, "", "and TabName = 'WorkTime' and FldName = 'WTIDTypeFlag' and NotShowFlag = '0'")
            ddlWTIDTypeFlag.Items.Insert(0, New ListItem("---請選擇---", ""))

            '班別代碼
            Bsp.Utility.FillDDL(ddlRemark, "eHRMSDB", "HRCodeMap", "Code", "CodeCName", Bsp.Utility.DisplayType.Full, "", "and TabName = 'WorkTime' and FldName = 'Remark' and NotShowFlag = '0'")
            ddlRemark.Items.Insert(0, New ListItem("---請選擇---", ""))
        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnAdd"       '新增
                DoAdd()
            Case "btnUpdate"    '修改
                DoUpdate()
            Case "btnQuery"     '查詢
                '2015/07/30 add 新增查詢頁面檢核
                If funCheckData() Then
                    ViewState.Item("DoQuery") = "Y"
                    DoQuery()
                End If
            Case "btnDelete"    '刪除
                DoDelete()
            Case "btnActionX"   '清除
                DoClear()
            Case "btnDownload"  '下傳
                DoDownload()
        End Select
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

            For Each strKey As String In ht.Keys
                Dim ctl As Control = Me.FindControl(strKey)

                If TypeOf ctl Is TextBox Then
                    CType(ctl, TextBox).Text = ht(strKey).ToString()
                End If
                If TypeOf ctl Is DropDownList Then
                    If ht(strKey).ToString <> "" Then
                        CType(ctl, DropDownList).SelectedValue = ht(strKey).ToString
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

    Private Sub DoAdd()
        Dim btnA As New ButtonState(ButtonState.emButtonType.Add)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
        Dim btnC As New ButtonState(ButtonState.emButtonType.Cancel)

        btnA.Caption = "存檔返回"
        btnX.Caption = "返回"
        btnC.Caption = "清除"

        Dim strCompID As String
        If UserProfile.SelectCompRoleID = "ALL" Then
            strCompID = ddlCompID.SelectedValue
        Else
            strCompID = UserProfile.SelectCompRoleID
        End If

        Me.TransferFramePage("~/PA/PA2101.aspx", New ButtonState() {btnA, btnX, btnC}, _
            ddlCompID.ID & "=" & strCompID, _
            ddlWTID.ID & "=" & ddlWTID.SelectedValue, _
            txtBeginTimeStart.ID & "=" & txtBeginTimeStart.Text, _
            txtBeginTimeEnd.ID & "=" & txtBeginTimeEnd.Text, _
            txtEndTimeStart.ID & "=" & txtEndTimeStart.Text, _
            txtEndTimeEnd.ID & "=" & txtEndTimeEnd.Text, _
            txtRestBeginTimeStart.ID & "=" & txtRestBeginTimeStart.Text, _
            txtRestBeginTimeEnd.ID & "=" & txtRestBeginTimeEnd.Text, _
            txtRestEndTimeStart.ID & "=" & txtRestEndTimeStart.Text, _
            txtRestEndTimeEnd.ID & "=" & txtRestEndTimeEnd.Text, _
            ddlAcrossFlag.ID & "=" & ddlAcrossFlag.SelectedValue, _
            ddlInValidFlag.ID & "=" & ddlInValidFlag.SelectedValue, _
            ddlWTIDTypeFlag.ID & "=" & ddlWTIDTypeFlag.SelectedValue, _
            ddlRemark.ID & "=" & ddlRemark.SelectedValue, _
            "PageNo=" & pcMain.PageNo.ToString(), _
            "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
    End Sub

    Private Sub DoUpdate()
        Dim btnU As New ButtonState(ButtonState.emButtonType.Update)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
        Dim btnC As New ButtonState(ButtonState.emButtonType.Cancel)

        btnU.Caption = "存檔返回"
        btnX.Caption = "返回"
        btnC.Caption = "清除"

        Dim strSysID As String
        strSysID = Bsp.Utility.subGetSysID(UserProfile.LoginSysID)
        Dim arySysID() As String = Split(strSysID, "-")

        Dim strCompID As String
        If UserProfile.SelectCompRoleID = "ALL" Then
            strCompID = ddlCompID.SelectedValue
        Else
            strCompID = UserProfile.SelectCompRoleID
        End If

        Me.TransferFramePage("~/PA/PA2102.aspx", New ButtonState() {btnU, btnX, btnC}, _
            ddlCompID.ID & "=" & strCompID, _
            ddlWTID.ID & "=" & ddlWTID.SelectedValue, _
            txtBeginTimeStart.ID & "=" & txtBeginTimeStart.Text, _
            txtBeginTimeEnd.ID & "=" & txtBeginTimeEnd.Text, _
            txtEndTimeStart.ID & "=" & txtEndTimeStart.Text, _
            txtEndTimeEnd.ID & "=" & txtEndTimeEnd.Text, _
            txtRestBeginTimeStart.ID & "=" & txtRestBeginTimeStart.Text, _
            txtRestBeginTimeEnd.ID & "=" & txtRestBeginTimeEnd.Text, _
            txtRestEndTimeStart.ID & "=" & txtRestEndTimeStart.Text, _
            txtRestEndTimeEnd.ID & "=" & txtRestEndTimeEnd.Text, _
            ddlAcrossFlag.ID & "=" & ddlAcrossFlag.SelectedValue, _
            ddlInValidFlag.ID & "=" & ddlInValidFlag.SelectedValue, _
            ddlWTIDTypeFlag.ID & "=" & ddlWTIDTypeFlag.SelectedValue, _
            ddlRemark.ID & "=" & ddlRemark.SelectedValue, _
            "PageNo=" & pcMain.PageNo.ToString(), _
            "SysID=" & arySysID(0), _
            "SelectedCompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
            "SelectedWTID=" & gvMain.DataKeys(selectedRow(gvMain))("WTID").ToString(), _
            "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
    End Sub

    Private Sub DoQuery()
        Dim objPA As New PA2()
        gvMain.Visible = True

        Dim strCompID As String
        If UserProfile.SelectCompRoleID = "ALL" Then
            strCompID = ddlCompID.SelectedValue
        Else
            strCompID = UserProfile.SelectCompRoleID
        End If
        IsDoQuery.Value = "Y"

        Try
            '2015/07/28 Modify 規格變更:時間欄位移除冒號
            'pcMain.DataTable = objPA.WorkTimeSettingQuery(
            '    "CompID=" & strCompID, _
            '    "WTID=" & ddlWTID.SelectedValue, _
            '    "BeginTime=" & txtBeginTime.Text.Replace(":", ""), _
            '    "EndTime=" & txtEndTime.Text.Replace(":", ""), _
            '    "RestBeginTime=" & txtRestBeginTime.Text.Replace(":", ""), _
            '    "RestEndTime=" & txtRestEndTime.Text.Replace(":", ""), _
            '    "AcrossFlag=" & ddlAcrossFlag.SelectedValue, _
            '    "InValidFlag=" & ddlInValidFlag.SelectedValue)
            pcMain.DataTable = objPA.WorkTimeSettingQuery(
                "CompID=" & strCompID, _
                "WTID=" & ddlWTID.SelectedValue, _
                "BeginTimeStart=" & txtBeginTimeStart.Text, _
                "BeginTimeEnd=" & txtBeginTimeEnd.Text, _
                "EndTimeStart=" & txtEndTimeStart.Text, _
                "EndTimeEnd=" & txtEndTimeEnd.Text, _
                "RestBeginTimeStart=" & txtRestBeginTimeStart.Text, _
                "RestBeginTimeEnd=" & txtRestBeginTimeEnd.Text, _
                "RestEndTimeStart=" & txtRestEndTimeStart.Text, _
                "RestEndTimeEnd=" & txtRestEndTimeEnd.Text, _
                "AcrossFlag=" & ddlAcrossFlag.SelectedValue, _
                "InValidFlag=" & ddlInValidFlag.SelectedValue, _
                "WTIDTypeFlag=" & ddlWTIDTypeFlag.SelectedValue, _
                "Remark=" & ddlRemark.SelectedValue)

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
        End Try
    End Sub

    Private Sub DoDelete()
        If selectedRow(gvMain) < 0 Then
            '尚未選取資料列！
            Bsp.Utility.ShowFormatMessage(Me, "W_00000")
        Else
            Dim beWorkTime As New beWorkTime.Row()
            Dim objPA As New PA2()
            Dim strSysID As String

            strSysID = Bsp.Utility.subGetSysID(UserProfile.LoginSysID)
            Dim arySysID() As String = Split(strSysID, "-")

            Dim strCompID As String
            If UserProfile.SelectCompRoleID = "ALL" Then
                strCompID = ddlCompID.SelectedValue
            Else
                strCompID = UserProfile.SelectCompRoleID
            End If

            beWorkTime.CompID.Value = strCompID
            beWorkTime.WTID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("WTID").ToString()

            Try
                If objPA.IsDataExists("OrgWorkTime", "And CompID = " & Bsp.Utility.Quote(beWorkTime.CompID.Value) & " And WTID = " & Bsp.Utility.Quote(beWorkTime.WTID.Value)) Then
                    Bsp.Utility.ShowMessage(Me, "班別代碼已存在在單位班別內，需先刪除單位班別才可以刪除班別代碼")
                Else
                    objPA.DeleteWorkTimeSetting(beWorkTime)
                End If
            Catch ex As Exception
                Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoDelete", ex)
            End Try
            gvMain.DataBind()

            DoQuery()
        End If
    End Sub

    Protected Sub gvMain_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMain.RowCommand
        If e.CommandName = "Detail" Then
            Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)

            btnX.Caption = "返回"

            Dim strCompID As String
            If UserProfile.SelectCompRoleID = "ALL" Then
                strCompID = ddlCompID.SelectedValue
            Else
                strCompID = UserProfile.SelectCompRoleID
            End If

            '2015/07/28 Modify 規格變更:時間欄位移除冒號
            'Me.TransferFramePage("~/PA/PA2102.aspx", New ButtonState() {btnX}, _
            '    ddlCompID.ID & "=" & strCompID, _
            '    ddlWTID.ID & "=" & ddlWTID.SelectedValue, _
            '    txtBeginTime.ID & "=" & txtBeginTime.Text.Replace(":", ""), _
            '    txtEndTime.ID & "=" & txtEndTime.Text.Replace(":", ""), _
            '    txtRestBeginTime.ID & "=" & txtRestBeginTime.Text.Replace(":", ""), _
            '    txtRestEndTime.ID & "=" & txtRestEndTime.Text.Replace(":", ""), _
            '    ddlAcrossFlag.ID & "=" & ddlAcrossFlag.SelectedValue, _
            '    ddlInValidFlag.ID & "=" & ddlInValidFlag.SelectedValue, _
            '    "PageNo=" & pcMain.PageNo.ToString(), _
            '    "SelectedCompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
            '    "SelectedWTID=" & gvMain.DataKeys(selectedRow(gvMain))("WTID").ToString(), _
            '    "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
            Me.TransferFramePage("~/PA/PA2102.aspx", New ButtonState() {btnX}, _
                ddlCompID.ID & "=" & strCompID, _
                ddlWTID.ID & "=" & ddlWTID.SelectedValue, _
                txtBeginTimeStart.ID & "=" & txtBeginTimeStart.Text, _
                txtBeginTimeEnd.ID & "=" & txtBeginTimeEnd.Text, _
                txtEndTimeStart.ID & "=" & txtEndTimeStart.Text, _
                txtEndTimeEnd.ID & "=" & txtEndTimeEnd.Text, _
                txtRestBeginTimeStart.ID & "=" & txtRestBeginTimeStart.Text, _
                txtRestBeginTimeEnd.ID & "=" & txtRestBeginTimeEnd.Text, _
                txtRestEndTimeStart.ID & "=" & txtRestEndTimeStart.Text, _
                txtRestEndTimeEnd.ID & "=" & txtRestEndTimeEnd.Text, _
                ddlAcrossFlag.ID & "=" & ddlAcrossFlag.SelectedValue, _
                ddlInValidFlag.ID & "=" & ddlInValidFlag.SelectedValue, _
                ddlWTIDTypeFlag.ID & "=" & ddlWTIDTypeFlag.SelectedValue, _
                ddlRemark.ID & "=" & ddlRemark.SelectedValue, _
                "PageNo=" & pcMain.PageNo.ToString(), _
                "SelectedCompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
                "SelectedWTID=" & gvMain.DataKeys(selectedRow(gvMain))("WTID").ToString(), _
                "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))

        End If
    End Sub

    Private Sub DoClear()
        IsDoQuery.Value = ""
        ViewState.Item("DoQuery") = ""
        gvMain.Visible = False

        '公司代碼
        ddlCompID.SelectedValue = ""

        '班別代碼
        ddlWTID.SelectedValue = ""

        '上班時間
        txtBeginTimeStart.Text = ""
        txtBeginTimeEnd.Text = ""

        '下班時間
        txtEndTimeStart.Text = ""
        txtEndTimeEnd.Text = ""

        '休息開始時間
        txtRestBeginTimeStart.Text = ""
        txtRestBeginTimeEnd.Text = ""

        '休息結束時間
        txtRestEndTimeStart.Text = ""
        txtRestEndTimeEnd.Text = ""

        '跨日註記
        ddlAcrossFlag.SelectedValue = ""

        '無效註記
        ddlInValidFlag.SelectedValue = ""

        '班別類型
        ddlWTIDTypeFlag.SelectedValue = ""

        '班別說明
        ddlRemark.SelectedValue = ""
    End Sub

    Private Sub DoDownload()
        Try
            If IsDoQuery.Value = "Y" Then
                Dim strCompID As String = ""
                Dim objPA As New PA2()
                If pcMain.DataTable.Rows.Count > 0 Then
                    '產出檔頭
                    Dim strFileName As String = ""

                    '動態產生GridView以便匯出成EXCEL
                    Dim gvExport As GridView = New GridView()
                    gvExport.AllowPaging = False
                    gvExport.AllowSorting = False
                    gvExport.FooterStyle.BackColor = Drawing.ColorTranslator.FromHtml("#99CCCC")
                    gvExport.FooterStyle.ForeColor = Drawing.ColorTranslator.FromHtml("#003399")
                    gvExport.RowStyle.CssClass = "tr_evenline"
                    gvExport.AlternatingRowStyle.CssClass = "tr_oddline"
                    gvExport.EmptyDataRowStyle.CssClass = "GridView_EmptyRowStyle"

                    'gvExport = gvMain  '寫GridViewName-gvMain，會變成下載GridView畫面，會出下換頁CSS等怪東西
                    If UserProfile.SelectCompRoleID = "ALL" Then
                        strCompID = ddlCompID.SelectedValue
                    Else
                        strCompID = UserProfile.SelectCompRoleID
                    End If

                    strFileName = Bsp.Utility.GetNewFileName("公司班別設定資料下載-") & ".xls"
                    '2015/07/28 Modify 規格變更:時間欄位移除冒號
                    'gvExport.DataSource = objPA.QueryWorkTimeByDownload( _
                    '    "CompID=" & strCompID, _
                    '    "WTID=" & ddlWTID.SelectedValue, _
                    '    "BeginTime=" & txtBeginTime.Text.Replace(":", ""), _
                    '    "EndTime=" & txtEndTime.Text.Replace(":", ""), _
                    '    "RestBeginTime=" & txtRestBeginTime.Text.Replace(":", ""), _
                    '    "RestEndTime=" & txtRestEndTime.Text.Replace(":", ""), _
                    '    "AcrossFlag=" & ddlAcrossFlag.SelectedValue, _
                    '    "InValidFlag=" & ddlInValidFlag.SelectedValue)

                    gvExport.DataSource = objPA.QueryWorkTimeByDownload( _
                        "CompID=" & strCompID, _
                        "WTID=" & ddlWTID.SelectedValue, _
                        "BeginTimeStart=" & txtBeginTimeStart.Text, _
                        "BeginTimeEnd=" & txtBeginTimeEnd.Text, _
                        "EndTimeStart=" & txtEndTimeStart.Text, _
                        "EndTimeEnd=" & txtEndTimeEnd.Text, _
                        "RestBeginTimeStart=" & txtRestBeginTimeStart.Text, _
                        "RestBeginTimeEnd=" & txtRestBeginTimeEnd.Text, _
                        "RestEndTimeStart=" & txtRestEndTimeStart.Text, _
                        "RestEndTimeEnd=" & txtRestEndTimeEnd.Text, _
                        "AcrossFlag=" & ddlAcrossFlag.SelectedValue, _
                        "InValidFlag=" & ddlInValidFlag.SelectedValue, _
                        "WTIDTypeFlag=" & ddlWTIDTypeFlag.SelectedValue, _
                        "Remark=" & ddlRemark.SelectedValue)

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

                    gvExport.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
                    gvExport.RenderControl(oHtmlTextWriter)
                    Response.Write(style)
                    Response.Write(oStringWriter.ToString())
                    Response.End()
                Else
                    Bsp.Utility.ShowFormatMessage(Me, "請先查詢有資料，才能下傳!")
                End If
            Else
            Bsp.Utility.ShowFormatMessage(Me, "請先查詢有資料，才能下傳!")
            End If

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoDownload", ex)
        End Try
    End Sub

    Protected Sub ddlCompID_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlCompID.SelectedIndexChanged
        '班別代碼
        PA2.FillWTID_PA2100(ddlWTID, ddlCompID.SelectedValue)
        ddlWTID.Items.Insert(0, New ListItem("---請選擇---", ""))
    End Sub

    '2015/07/30 add 新增查詢頁面檢核功能
    Private Function funCheckData() As Boolean
        Dim objPA As New PA2()
        Dim beWorkTime As New beWorkTime.Row()
        Dim bsWorkTime As New beWorkTime.Service()
        Dim TimeFlag_BeginTime_Start As Boolean
        Dim TimeFlag_BeginTime_End As Boolean
        Dim TimeFlag_EndTime_Start As Boolean
        Dim TimeFlag_EndTime_End As Boolean
        Dim TimeFlag_RestBeginTime_Start As Boolean
        Dim TimeFlag_RestBeginTime_End As Boolean
        Dim TimeFlag_RestEndTime_Start As Boolean
        Dim TimeFlag_RestEndTime_End As Boolean

        '上班時間(起)
        Dim arrBeginTimeStart(1) As String
        If txtBeginTimeStart.Text <> "" Then
            If txtBeginTimeStart.Text.Trim.Length = 4 Then
                If IsNumeric(txtBeginTimeStart.Text.Trim) Then
                    TimeFlag_BeginTime_Start = True

                    arrBeginTimeStart(0) = Left(txtBeginTimeStart.Text.Trim, 2) '小時
                    arrBeginTimeStart(1) = Right(txtBeginTimeStart.Text.Trim, 2) '分鐘

                    If arrBeginTimeStart(0) < 0 Or arrBeginTimeStart(0) > 24 Then
                        Bsp.Utility.ShowMessage(Me, "「上班時間(起)」請輸入正確時間")
                        Return False
                    ElseIf arrBeginTimeStart(0) = 24 Then
                        Bsp.Utility.ShowMessage(Me, "「上班時間(起)」凌晨時段需設定為0000，不可為2400")
                        Return False
                    End If

                    If arrBeginTimeStart(1) < 0 Or arrBeginTimeStart(1) > 59 Then
                        Bsp.Utility.ShowMessage(Me, "「上班時間(起)」請輸入正確時間")
                        Return False
                    End If

                Else
                    Bsp.Utility.ShowMessage(Me, "「上班時間(起)」請輸入正確時間")
                    Return False
                End If
            Else
                Bsp.Utility.ShowMessage(Me, "「上班時間(起)」請輸入正確格式")
                Return False
            End If
        End If

        '上班時間(訖)
        Dim arrBeginTimeEnd(1) As String
        If txtBeginTimeEnd.Text <> "" Then
            If txtBeginTimeEnd.Text.Trim.Length = 4 Then
                If IsNumeric(txtBeginTimeEnd.Text.Trim) Then
                    TimeFlag_BeginTime_End = True

                    arrBeginTimeEnd(0) = Left(txtBeginTimeEnd.Text.Trim, 2) '小時
                    arrBeginTimeEnd(1) = Right(txtBeginTimeEnd.Text.Trim, 2) '分鐘

                    If arrBeginTimeEnd(0) < 0 Or arrBeginTimeEnd(0) > 24 Then
                        Bsp.Utility.ShowMessage(Me, "「上班時間(訖)」請輸入正確時間")
                        Return False
                    ElseIf arrBeginTimeEnd(0) = 24 Then
                        Bsp.Utility.ShowMessage(Me, "「上班時間(訖)」凌晨時段需設定為0000，不可為2400")
                        Return False
                    End If

                    If arrBeginTimeEnd(1) < 0 Or arrBeginTimeEnd(1) > 59 Then
                        Bsp.Utility.ShowMessage(Me, "「上班時間(訖)」請輸入正確時間")
                        Return False
                    End If

                Else
                    Bsp.Utility.ShowMessage(Me, "「上班時間(訖)」請輸入正確時間")
                    Return False
                End If
            Else
                Bsp.Utility.ShowMessage(Me, "「上班時間(訖)」請輸入正確格式")
                Return False
            End If
        End If

        '上班時間(訖)不可早於上班時間(起)
        If TimeFlag_BeginTime_Start = True And TimeFlag_BeginTime_End = True Then
            If arrBeginTimeStart(0) > arrBeginTimeEnd(0) Then
                Bsp.Utility.ShowMessage(Me, "「上班時間(起)」不可大於「上班時間(迄)」")
                Return False
            ElseIf arrBeginTimeStart(0) = arrBeginTimeEnd(0) Then
                If arrBeginTimeStart(1) > arrBeginTimeEnd(1) Then
                    Bsp.Utility.ShowMessage(Me, "「上班時間(起)」不可大於「上班時間(迄)」")
                    Return False
                End If
            End If
        End If

        '下班時間(起)
        Dim arrEndTimeStart(1) As String
        If txtEndTimeStart.Text <> "" Then
            If txtEndTimeStart.Text.Trim.Length = 4 Then
                If IsNumeric(txtEndTimeStart.Text.Trim) Then
                    TimeFlag_EndTime_Start = True

                    arrEndTimeStart(0) = Left(txtEndTimeStart.Text.Trim, 2) '小時
                    arrEndTimeStart(1) = Right(txtEndTimeStart.Text.Trim, 2) '分鐘

                    If arrEndTimeStart(0) < 0 Or arrEndTimeStart(0) > 24 Then
                        Bsp.Utility.ShowMessage(Me, "「下班時間(起)」請輸入正確時間")
                        Return False
                    ElseIf arrEndTimeStart(0) = 24 Then
                        Bsp.Utility.ShowMessage(Me, "「下班時間(起)」凌晨時段需設定為0000，不可為2400")
                        Return False
                    End If

                    If arrEndTimeStart(1) < 0 Or arrEndTimeStart(1) > 59 Then
                        Bsp.Utility.ShowMessage(Me, "「下班時間(起)」請輸入正確時間")
                        Return False
                    End If

                Else
                    Bsp.Utility.ShowMessage(Me, "「下班時間(起)」請輸入正確時間")
                    Return False
                End If
            Else
                Bsp.Utility.ShowMessage(Me, "「下班時間(起)」請輸入正確格式")
                Return False
            End If
        End If

        '下班時間(訖)
        Dim arrEndTimeEnd(1) As String
        If txtEndTimeEnd.Text <> "" Then
            If txtEndTimeEnd.Text.Trim.Length = 4 Then
                If IsNumeric(txtEndTimeEnd.Text.Trim) Then
                    TimeFlag_EndTime_End = True

                    arrEndTimeEnd(0) = Left(txtEndTimeEnd.Text.Trim, 2) '小時
                    arrEndTimeEnd(1) = Right(txtEndTimeEnd.Text.Trim, 2) '分鐘

                    If arrEndTimeEnd(0) < 0 Or arrEndTimeEnd(0) > 24 Then
                        Bsp.Utility.ShowMessage(Me, "「下班時間(訖)」請輸入正確時間")
                        Return False
                    ElseIf arrEndTimeEnd(0) = 24 Then
                        Bsp.Utility.ShowMessage(Me, "「下班時間(訖)」凌晨時段需設定為0000，不可為2400")
                        Return False
                    End If

                    If arrEndTimeEnd(1) < 0 Or arrEndTimeEnd(1) > 59 Then
                        Bsp.Utility.ShowMessage(Me, "「下班時間(訖)」請輸入正確時間")
                        Return False
                    End If

                Else
                    Bsp.Utility.ShowMessage(Me, "「下班時間(訖)」請輸入正確時間")
                    Return False
                End If
            Else
                Bsp.Utility.ShowMessage(Me, "「下班時間(訖)」請輸入正確格式")
                Return False
            End If
        End If

        '下班時間(訖)不可早於下班時間(起)
        If TimeFlag_EndTime_Start = True And TimeFlag_EndTime_End = True Then
            If arrEndTimeStart(0) > arrEndTimeEnd(0) Then
                Bsp.Utility.ShowMessage(Me, "「下班時間(起)」不可大於「下班時間(迄)」")
                Return False
            ElseIf arrEndTimeStart(0) = arrEndTimeEnd(0) Then
                If arrEndTimeStart(1) > arrEndTimeEnd(1) Then
                    Bsp.Utility.ShowMessage(Me, "「下班時間(起)」不可大於「下班時間(迄)」")
                    Return False
                End If
            End If
        End If

        '休息開始時間(起)
        Dim arrRestBeginTimeStart(1) As String
        If txtRestBeginTimeStart.Text <> "" Then
            If txtRestBeginTimeStart.Text.Trim.Length = 4 Then
                If IsNumeric(txtRestBeginTimeStart.Text.Trim) Then
                    TimeFlag_RestBeginTime_Start = True

                    arrRestBeginTimeStart(0) = Left(txtRestBeginTimeStart.Text.Trim, 2) '小時
                    arrRestBeginTimeStart(1) = Right(txtRestBeginTimeStart.Text.Trim, 2) '分鐘

                    If arrRestBeginTimeStart(0) < 0 Or arrRestBeginTimeStart(0) > 24 Then
                        Bsp.Utility.ShowMessage(Me, "「休息開始時間(起)」請輸入正確時間")
                        Return False
                    ElseIf arrRestBeginTimeStart(0) = 24 Then
                        Bsp.Utility.ShowMessage(Me, "「休息開始時間(起)」凌晨時段需設定為0000，不可為2400")
                        Return False
                    End If

                    If arrRestBeginTimeStart(1) < 0 Or arrRestBeginTimeStart(1) > 59 Then
                        Bsp.Utility.ShowMessage(Me, "「休息開始時間(起)」請輸入正確時間")
                        Return False
                    End If

                Else
                    Bsp.Utility.ShowMessage(Me, "「休息開始時間(起)」請輸入正確時間")
                    Return False
                End If
            Else
                Bsp.Utility.ShowMessage(Me, "「休息開始時間(起)」請輸入正確格式")
                Return False
            End If
        End If

        '休息開始時間(迄)
        Dim arrRestBeginTimeEnd(1) As String
        If txtRestBeginTimeEnd.Text <> "" Then
            If txtRestBeginTimeEnd.Text.Trim.Length = 4 Then
                If IsNumeric(txtRestBeginTimeEnd.Text.Trim) Then
                    TimeFlag_RestBeginTime_End = True

                    arrRestBeginTimeEnd(0) = Left(txtRestBeginTimeEnd.Text.Trim, 2) '小時
                    arrRestBeginTimeEnd(1) = Right(txtRestBeginTimeEnd.Text.Trim, 2) '分鐘

                    If arrRestBeginTimeEnd(0) < 0 Or arrRestBeginTimeEnd(0) > 24 Then
                        Bsp.Utility.ShowMessage(Me, "「休息開始時間(迄)」請輸入正確時間")
                        Return False
                    ElseIf arrRestBeginTimeEnd(0) = 24 Then
                        Bsp.Utility.ShowMessage(Me, "「休息開始時間(迄)」凌晨時段需設定為0000，不可為2400")
                        Return False
                    End If

                    If arrRestBeginTimeEnd(1) < 0 Or arrRestBeginTimeEnd(1) > 59 Then
                        Bsp.Utility.ShowMessage(Me, "「休息開始時間(迄)」請輸入正確時間")
                        Return False
                    End If

                Else
                    Bsp.Utility.ShowMessage(Me, "「休息開始時間(迄)」請輸入正確時間")
                    Return False
                End If
            Else
                Bsp.Utility.ShowMessage(Me, "「休息開始時間(迄)」請輸入正確格式")
                Return False
            End If
        End If

        '休息開始時間(迄)不可早於休息開始時間(起)
        If TimeFlag_RestBeginTime_Start = True And TimeFlag_RestBeginTime_End = True Then
            If arrRestBeginTimeStart(0) > arrRestBeginTimeEnd(0) Then
                Bsp.Utility.ShowMessage(Me, "「休息開始時間(起)」不可大於「休息開始時間(迄)」")
                Return False
            ElseIf arrRestBeginTimeStart(0) = arrRestBeginTimeEnd(0) Then
                If arrRestBeginTimeStart(1) > arrRestBeginTimeEnd(1) Then
                    Bsp.Utility.ShowMessage(Me, "「休息開始時間(起)」不可大於「休息開始時間(迄)」")
                    Return False
                End If
            End If
        End If

        '休息結束時間(起)
        Dim arrRestEndTimeStart(1) As String
        If txtRestEndTimeStart.Text <> "" Then
            If txtRestEndTimeStart.Text.Trim.Length = 4 Then
                If IsNumeric(txtRestEndTimeStart.Text.Trim) Then
                    TimeFlag_RestEndTime_Start = True

                    arrRestEndTimeStart(0) = Left(txtRestEndTimeStart.Text.Trim, 2) '小時
                    arrRestEndTimeStart(1) = Right(txtRestEndTimeStart.Text.Trim, 2) '分鐘

                    If arrRestEndTimeStart(0) < 0 Or arrRestEndTimeStart(0) > 24 Then
                        Bsp.Utility.ShowMessage(Me, "「休息結束時間」請輸入正確時間")
                        Return False
                    ElseIf arrRestEndTimeStart(0) = 24 Then
                        Bsp.Utility.ShowMessage(Me, "「休息結束時間」凌晨時段需設定為0000，不可為2400")
                        Return False
                    End If

                    If arrRestEndTimeStart(1) < 0 Or arrRestEndTimeStart(1) > 59 Then
                        Bsp.Utility.ShowMessage(Me, "「休息結束時間」請輸入正確時間")
                        Return False
                    End If

                Else
                    Bsp.Utility.ShowMessage(Me, "「休息結束時間」請輸入正確時間")
                    Return False
                End If
            Else
                Bsp.Utility.ShowMessage(Me, "「休息結束時間」請輸入正確格式")
                Return False
            End If
        End If

        '休息結束時間(迄)
        Dim arrRestEndTimeEnd(1) As String
        If txtRestEndTimeEnd.Text <> "" Then
            If txtRestEndTimeEnd.Text.Trim.Length = 4 Then
                If IsNumeric(txtRestEndTimeEnd.Text.Trim) Then
                    TimeFlag_RestEndTime_End = True

                    arrRestEndTimeEnd(0) = Left(txtRestEndTimeEnd.Text.Trim, 2) '小時
                    arrRestEndTimeEnd(1) = Right(txtRestEndTimeEnd.Text.Trim, 2) '分鐘

                    If arrRestEndTimeEnd(0) < 0 Or arrRestEndTimeEnd(0) > 24 Then
                        Bsp.Utility.ShowMessage(Me, "「休息結束時間」請輸入正確時間")
                        Return False
                    ElseIf arrRestEndTimeEnd(0) = 24 Then
                        Bsp.Utility.ShowMessage(Me, "「休息結束時間」凌晨時段需設定為0000，不可為2400")
                        Return False
                    End If

                    If arrRestEndTimeEnd(1) < 0 Or arrRestEndTimeEnd(1) > 59 Then
                        Bsp.Utility.ShowMessage(Me, "「休息結束時間」請輸入正確時間")
                        Return False
                    End If

                Else
                    Bsp.Utility.ShowMessage(Me, "「休息結束時間」請輸入正確時間")
                    Return False
                End If
            Else
                Bsp.Utility.ShowMessage(Me, "「休息結束時間」請輸入正確格式")
                Return False
            End If
        End If

        '休息結束時間(迄)不可早於休息結束時間(起)
        If TimeFlag_RestEndTime_Start = True And TimeFlag_RestEndTime_End = True Then
            If arrRestEndTimeStart(0) > arrRestEndTimeEnd(0) Then
                Bsp.Utility.ShowMessage(Me, "「休息結束時間(起)」不可早於「休息結束時間(迄)」")
                Return False
            ElseIf arrRestEndTimeStart(0) = arrRestEndTimeEnd(0) Then
                If arrRestEndTimeStart(1) > arrRestEndTimeEnd(1) Then
                    Bsp.Utility.ShowMessage(Me, "「休息結束時間(起)」不可早於「休息結束時間(迄)」")
                    Return False
                End If
            End If
        End If

        Return True
    End Function
End Class
