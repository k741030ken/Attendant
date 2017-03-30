'****************************************************
'功能說明：單位班別設定
'建立人員：MickySung
'建立日期：2015.05.20
'****************************************************
Imports System.Data

Partial Class PA_PA2200
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
                PA2.FillWTID_PA2200(ddlWTID, ddlCompID.SelectedValue) '2015/07/22 OrgWorkTime 改成 WorkTime
                ddlWTID.Items.Insert(0, New ListItem("---請選擇---", ""))

                '2015/07/27 Modify 規格變更:單位代碼1連動單位代碼2
                '單位代碼1
                PA2.FillOrganID_PA2201(ddlDeptID, ddlCompID.SelectedValue, "", "1")
                ddlDeptID.Items.Insert(0, New ListItem("---請選擇---", ""))

                '單位代碼2
                PA2.FillOrganID_PA2201(ddlOrganID, ddlCompID.SelectedValue, ddlDeptID.SelectedValue, "2")
                ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
            Else
                '2015/05/28 公司代碼-名稱改寫法
                lblCompRoleID.Text = UserProfile.SelectCompRoleName
                'lblCompRoleID.Text = UserProfile.SelectCompRoleID + "-" + objSC.GetCompName(UserProfile.SelectCompRoleID).Rows(0).Item("CompName").ToString
                ddlCompID.Visible = False

                '班別代碼
                PA2.FillWTID_PA2200(ddlWTID, UserProfile.SelectCompRoleID) '2015/07/22 OrgWorkTime 改成 WorkTime
                ddlWTID.Items.Insert(0, New ListItem("---請選擇---", ""))

                '2015/07/27 Modify 規格變更:單位代碼1連動單位代碼2
                '單位代碼1
                PA2.FillOrganID_PA2201(ddlDeptID, UserProfile.SelectCompRoleID, "", "1")
                ddlDeptID.Items.Insert(0, New ListItem("---請選擇---", ""))

                '單位代碼2
                PA2.FillOrganID_PA2201(ddlOrganID, UserProfile.SelectCompRoleID, ddlDeptID.SelectedValue, "2")
                ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
            End If
        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnAdd"       '新增
                DoAdd()
            Case "btnUpdate"    '修改
                DoUpdate()
            Case "btnQuery"     '查詢
                ViewState.Item("DoQuery") = "Y"
                DoQuery()
            Case "btnDelete"    '刪除
                DoDelete()
            Case "btnActionX"   '清除
                DoClear()
            Case "btnUpload"    '上傳
                DoUpload()
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

        Me.TransferFramePage("~/PA/PA2201.aspx", New ButtonState() {btnA, btnX, btnC}, _
            ddlCompID.ID & "=" & strCompID, _
            ddlWTID.ID & "=" & ddlWTID.SelectedValue, _
            ddlDeptID.ID & "=" & ddlDeptID.SelectedValue, _
            ddlOrganID.ID & "=" & ddlOrganID.SelectedValue, _
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

        Dim strCompID As String
        If UserProfile.SelectCompRoleID = "ALL" Then
            strCompID = ddlCompID.SelectedValue
        Else
            strCompID = UserProfile.SelectCompRoleID
        End If

        Me.TransferFramePage("~/PA/PA2202.aspx", New ButtonState() {btnU, btnX, btnC}, _
            ddlCompID.ID & "=" & strCompID, _
            ddlWTID.ID & "=" & ddlWTID.SelectedValue, _
            ddlDeptID.ID & "=" & ddlDeptID.SelectedValue, _
            ddlOrganID.ID & "=" & ddlOrganID.SelectedValue, _
            "PageNo=" & pcMain.PageNo.ToString(), _
            "SelectedCompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
            "SelectedOrganID=" & gvMain.DataKeys(selectedRow(gvMain))("OrganID").ToString(), _
            "SelectedDeptID=" & gvMain.DataKeys(selectedRow(gvMain))("DeptID").ToString(), _
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
            pcMain.DataTable = objPA.OrgWorkTimeSettingQuery(
                "CompID=" & strCompID, _
                "WTID=" & ddlWTID.SelectedValue, _
                "DeptID=" & ddlDeptID.SelectedValue, _
                "OrganID=" & ddlOrganID.SelectedValue)

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
        End Try
    End Sub

    Private Sub DoDelete()
        If selectedRow(gvMain) < 0 Then
            '尚未選取資料列！
            Bsp.Utility.ShowFormatMessage(Me, "W_00000")
        Else
            Dim beOrgWorkTime As New beOrgWorkTime.Row()
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

            beOrgWorkTime.CompID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("CompID").ToString()
            beOrgWorkTime.DeptID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("DeptID").ToString()
            beOrgWorkTime.OrganID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("OrganID").ToString()
            beOrgWorkTime.WTID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("WTID").ToString()

            Try
                objPA.DeleteOrgWorkTimeSetting(beOrgWorkTime)
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

            Me.TransferFramePage("~/PA/PA2202.aspx", New ButtonState() {btnX}, _
                ddlCompID.ID & "=" & strCompID, _
                ddlWTID.ID & "=" & ddlWTID.SelectedValue, _
                ddlDeptID.ID & "=" & ddlDeptID.SelectedValue, _
                ddlOrganID.ID & "=" & ddlOrganID.SelectedValue, _
                "PageNo=" & pcMain.PageNo.ToString(), _
                "SelectedCompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
                "SelectedOrganID=" & gvMain.DataKeys(selectedRow(gvMain))("OrganID").ToString(), _
                "SelectedDeptID=" & gvMain.DataKeys(selectedRow(gvMain))("DeptID").ToString(), _
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

        '單位代碼1
        ddlDeptID.SelectedValue = ""

        '單位代碼2
        ddlOrganID.SelectedValue = ""
    End Sub

    '檔案上傳
    Private Sub DoUpload()
        Dim btnC As New ButtonState(ButtonState.emButtonType.Confirm)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)

        btnC.Caption = "確定上傳"
        btnX.Caption = "關閉離開"

        Me.CallSmallPage("~/PA/PA2203.aspx", New ButtonState() {btnC, btnX})
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

                    strFileName = Bsp.Utility.GetNewFileName("單位班別設定資料下載-") & ".xls"
                    gvExport.DataSource = objPA.QueryOrgWorkTimeByDownload( _
                        "CompID=" & strCompID, _
                        "WTID=" & ddlWTID.SelectedValue, _
                        "DeptID=" & ddlDeptID.SelectedValue, _
                        "OrganID=" & ddlOrganID.SelectedValue)

                    gvExport.DataBind()

                    Response.ClearContent()
                    Response.BufferOutput = True
                    Response.Charset = "utf-8"
                    ''Response.ContentType = "application/ms-excel"      '只寫ms-excel不OK，會變成程式碼下載@@
                    'Response.ContentType = "application/vnd.ms-excel"
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
        PA2.FillWTID_PA2200(ddlWTID, ddlCompID.SelectedValue) '2015/07/22 OrgWorkTime 改成 WorkTime
        ddlWTID.Items.Insert(0, New ListItem("---請選擇---", ""))

        '2015/07/27 Modify 規格變更:單位代碼1連動單位代碼2
        '單位代碼1
        PA2.FillOrganID_PA2201(ddlDeptID, ddlCompID.SelectedValue, "", "1", PA2.DisplayType.Full)
        ddlDeptID.Items.Insert(0, New ListItem("---請選擇---", ""))

        ddlOrganID.Items.Clear()
        ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
    End Sub

    '2015/07/27 Add 規格變更:單位代碼1連動單位代碼2
    Protected Sub ddlDeptID_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlDeptID.SelectedIndexChanged
        Dim strCompID As String = ""
        If UserProfile.SelectCompRoleID = "ALL" Then
            strCompID = ddlCompID.SelectedValue
        Else
            strCompID = UserProfile.SelectCompRoleID
        End If

        '單位代碼2
        PA2.FillOrganID_PA2201(ddlOrganID, strCompID, ddlDeptID.SelectedValue, "2", PA2.DisplayType.Full)
        ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
    End Sub

End Class
