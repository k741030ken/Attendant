'****************************************************
'功能說明：新進員工文件繳交作業_證券
'建立人員：BeatriceCheng
'建立日期：2015.07.24
'****************************************************
Imports System.Data

Partial Class RG_RG1400
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ddlCompID.Visible = False
            ucQueryEmp.ShowCompRole = False

            If UserProfile.SelectCompRoleID = "ALL" Then
                ddlCompID.Visible = True
                Bsp.Utility.FillHRCompany(ddlCompID)
                lblCompRoleID.Visible = False
            Else
                ddlCompID.Visible = False
                lblCompRoleID.Text = UserProfile.SelectCompRoleName
                lblCompRoleID.Visible = True
            End If

            '員工編號、員工姓名
            ucQueryEmp.ShowCompRole = "False"
            ucQueryEmp.InValidFlag = "N"
            ucQueryEmp.SelectCompID = UserProfile.SelectCompRoleID
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
                Release("btnDelete")
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
                ElseIf TypeOf ctl Is DropDownList Then
                    CType(ctl, DropDownList).SelectedValue = ht(strKey).ToString()
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

        Me.TransferFramePage("~/RG/RG1401.aspx", New ButtonState() {btnA, btnX, btnC}, _
            Bsp.Utility.FormatToParam(ddlEmpDay), _
            Bsp.Utility.FormatToParam(txtEmpID), _
            Bsp.Utility.FormatToParam(txtName), _
            Bsp.Utility.FormatToParam(ddlStatus), _
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

        Me.TransferFramePage("~/RG/RG1402.aspx", New ButtonState() {btnU, btnX, btnC}, _
            Bsp.Utility.FormatToParam(ddlEmpDay), _
            Bsp.Utility.FormatToParam(txtEmpID), _
            Bsp.Utility.FormatToParam(txtName), _
            Bsp.Utility.FormatToParam(ddlStatus), _
            "PageNo=" & pcMain.PageNo.ToString(), _
            "SelectedCompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
            "SelectedEmpID=" & gvMain.DataKeys(selectedRow(gvMain))("EmpID").ToString(), _
            "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
    End Sub

    Private Sub DoQuery()
        Dim objRG1 As New RG1()

        Dim strCompID As String = ddlCompID.SelectedValue
        If strCompID = "" Then
            strCompID = UserProfile.SelectCompRoleID
        End If

        Try
            pcMain.DataTable = objRG1.QueryCheckInFile_SPHSC1(
                "CompID=" & strCompID, _
                "EmpDay=" & ddlEmpDay.SelectedValue, _
                "EmpID=" & txtEmpID.Text.ToUpper, _
                "Name=" & txtName.Text, _
                "Status=" & ddlStatus.SelectedValue)
            gvMain.Visible = True

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
        End Try
    End Sub

    Private Sub DoDelete()
        If selectedRow(gvMain) < 0 Then
            '尚未選取資料列！
            Bsp.Utility.ShowFormatMessage(Me, "W_00000")
        Else
            Dim beCheckInFile_SPHSC1 As New beCheckInFile_SPHSC1.Row()
            Dim objRG1 As New RG1

            beCheckInFile_SPHSC1.CompID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("CompID").ToString()
            beCheckInFile_SPHSC1.EmpID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("EmpID").ToString()

            Try
                objRG1.DeleteCheckInFile_SPHSC1(beCheckInFile_SPHSC1)
            Catch ex As Exception
                Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoDelete", ex)
            End Try

            DoQuery()
        End If
    End Sub

    Protected Sub gvMain_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMain.RowCommand
        If e.CommandName = "Detail" Then
            Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
            btnX.Caption = "返回"

            Me.TransferFramePage("~/RG/RG1402.aspx", New ButtonState() {btnX}, _
                Bsp.Utility.FormatToParam(ddlCompID), _
                Bsp.Utility.FormatToParam(ddlEmpDay), _
                Bsp.Utility.FormatToParam(txtEmpID), _
                Bsp.Utility.FormatToParam(txtName), _
                Bsp.Utility.FormatToParam(ddlStatus), _
                "PageNo=" & pcMain.PageNo.ToString(), _
                "SelectedCompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
                "SelectedEmpID=" & gvMain.DataKeys(selectedRow(gvMain))("EmpID").ToString(), _
                "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
        End If
    End Sub

    Private Sub DoClear()
        ViewState.Item("DoQuery") = ""
        ddlEmpDay.SelectedIndex = 0
        ddlStatus.SelectedIndex = 0
        txtEmpID.Text = ""
        txtName.Text = ""
        gvMain.Visible = False
    End Sub

    Public Overrides Sub DoModalReturn(ByVal returnValue As String)
        Dim strSql As String = ""

        If returnValue <> "" Then
            Dim aryData() As String = returnValue.Split(":")

            Select Case aryData(0)
                Case "ucQueryEmp"
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    txtEmpID.Text = aryValue(1)
                    'txtName.Text = aryValue(2)

                Case "ucRelease"
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    If aryValue(0) = "Y" Then
                        DoDelete()
                    End If
            End Select
        End If
    End Sub

    Private Sub Release(ByVal LogFunction As String)
        ucRelease.ShowCompRole = "True"
        ucRelease.FunID = "RG1400"
        ucRelease.LogFunction = LogFunction
        ucRelease.OpenSelect()
    End Sub

    Private Sub DoDownload()
        Try
            If ddlStatus.SelectedValue <> "1" Then
                Bsp.Utility.ShowMessage(Me, "【文件繳交狀態】請挑選「未繳齊」，才可下傳")
            Else
                If gvMain.Rows.Count > 0 Then
                    '產出檔頭
                    Dim strFileName As String = Bsp.Utility.GetNewFileName("RG1400證券文件繳交紀錄表_") & ".xls"

                    '動態產生GridView以便匯出成EXCEL
                    Dim gvExport As GridView = New GridView()
                    gvExport.AllowPaging = False
                    gvExport.AllowSorting = False
                    gvExport.FooterStyle.BackColor = Drawing.ColorTranslator.FromHtml("#99CCCC")
                    gvExport.FooterStyle.ForeColor = Drawing.ColorTranslator.FromHtml("#003399")
                    gvExport.RowStyle.CssClass = "tr_evenline"
                    gvExport.AlternatingRowStyle.CssClass = "tr_oddline"
                    gvExport.EmptyDataRowStyle.CssClass = "GridView_EmptyRowStyle"
                    gvExport.RowStyle.HorizontalAlign = HorizontalAlign.Center

                    Dim objRG As New RG1()
                    Dim strCompID As String
                    If UserProfile.SelectCompRoleID = "ALL" Then
                        strCompID = ddlCompID.SelectedValue
                    Else
                        strCompID = UserProfile.SelectCompRoleID
                    End If

                    gvExport.DataSource = objRG.QueryCheckInFile_SPHSC1(
                        "CompID=" & strCompID, _
                        "EmpDay=" & ddlEmpDay.SelectedValue, _
                        "EmpID=" & txtEmpID.Text.ToUpper, _
                        "Name=" & txtName.Text, _
                        "Status=1")

                    gvExport.DataBind()
                    GroupRows(gvExport)

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
                    Bsp.Utility.ShowMessage(Me, "請先查詢有資料，才能下傳!")
                End If
            End If
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoDownload", ex)
        End Try
    End Sub

    Public Sub GroupRows(ByVal GridViewData As GridView)
        GridViewData.HeaderRow.Cells(0).Text = "公司編號"
        GridViewData.HeaderRow.Cells(1).Text = "公司名稱"
        GridViewData.HeaderRow.Cells(2).Text = "員工編號"
        GridViewData.HeaderRow.Cells(3).Text = "姓名"
        GridViewData.HeaderRow.Cells(4).Text = "部門"
        GridViewData.HeaderRow.Cells(5).Text = "到職日"
        GridViewData.HeaderRow.Cells(6).Text = "報到天數"
        GridViewData.HeaderRow.Cells(7).Text = "缺件數"
        GridViewData.HeaderRow.Cells(8).Text = "身分證影本乙份"
        GridViewData.HeaderRow.Cells(9).Text = "學歷證件影本乙份"
        GridViewData.HeaderRow.Cells(10).Text = "戶籍謄本影本或戶口名簿影本乙份"
        GridViewData.HeaderRow.Cells(11).Text = "照片五張"
        GridViewData.HeaderRow.Cells(12).Text = "保證書二份"
        GridViewData.HeaderRow.Cells(13).Text = "承諾書乙份"
        GridViewData.HeaderRow.Cells(14).Text = "客戶資料保密切結書乙份"
        GridViewData.HeaderRow.Cells(15).Text = "扶養親屬表"
        GridViewData.HeaderRow.Cells(16).Text = "人事資料表"
        GridViewData.HeaderRow.Cells(17).Text = "原公司離職證明乙份"
        GridViewData.HeaderRow.Cells(18).Text = "退伍令"
        GridViewData.HeaderRow.Cells(19).Text = "永豐銀行存摺影本"
        GridViewData.HeaderRow.Cells(20).Text = "團體醫療保險申請表"
        GridViewData.HeaderRow.Cells(21).Text = "勞、健保加保申請書"
        GridViewData.HeaderRow.Cells(22).Text = "識別證製作申請書"
        GridViewData.HeaderRow.Cells(23).Text = "員工二親等眷屬資料表"
        GridViewData.HeaderRow.Cells(24).Text = "健康檢查報告"
        GridViewData.HeaderRow.Cells(25).Text = "同意書及員工基本工作規範"

        GridViewData.HeaderRow.Cells(0).Visible = False
        GridViewData.HeaderRow.Cells(1).Visible = False

        For i As Integer = 0 To GridViewData.Rows.Count - 1
            GridViewData.Rows(i).Cells(0).Visible = False
            GridViewData.Rows(i).Cells(1).Visible = False

            For j As Integer = 8 To 25
                If GridViewData.Rows(i).Cells(j).Text = "1" Then
                    GridViewData.Rows(i).Cells(j).Text = "●"
                ElseIf GridViewData.Rows(i).Cells(j).Text = "0" Then
                    GridViewData.Rows(i).Cells(j).Text = "○"
                End If
            Next
        Next

        Dim HeaderGridRow As GridViewRow = New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)
        Dim HeaderCell As New TableCell()

        Dim strTitle As String = ""
        strTitle &= "RG1400"
        strTitle &= Strings.StrConv(Strings.Space(100), VbStrConv.Wide)
        strTitle &= "永豐金證券─人力資源處"
        strTitle &= Strings.StrConv(Strings.Space(94), VbStrConv.Wide)
        strTitle &= "產生日期：" + Format(Now, "yyyy/MM/dd")
        strTitle &= "<br>新進人員文件缺繳報表─永豐金證券"
        strTitle &= "<br>報到日迄今" + ddlEmpDay.SelectedItem.Text

        HeaderCell.Text = strTitle
        HeaderCell.HorizontalAlign = HorizontalAlign.Center
        HeaderCell.ColumnSpan = "24"
        HeaderGridRow.Cells.Add(HeaderCell)
        GridViewData.Controls(0).Controls.AddAt(0, HeaderGridRow)
    End Sub

End Class
