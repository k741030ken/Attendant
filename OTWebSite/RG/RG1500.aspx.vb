'****************************************************
'功能說明：不報到員工資料輸入
'建立人員：BeatriceCheng
'建立日期：2015.07.31
'****************************************************
Imports System.Data

Partial Class RG_RG1500
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ddlCompID.Visible = False

            If UserProfile.SelectCompRoleID = "ALL" Then
                ddlCompID.Visible = True
                Bsp.Utility.FillHRCompany(ddlCompID)
                lblCompRoleID.Visible = False
            Else
                ddlCompID.Visible = False
                lblCompRoleID.Text = UserProfile.SelectCompRoleName
                lblCompRoleID.Visible = True
            End If

            LoadDate()
        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnUpdate"    '修改
                DoUpdate()
            Case "btnQuery"     '查詢
                ViewState.Item("DoQuery") = "Y"
                DoQuery()
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
                    Bsp.Utility.SetSelectedIndex(CType(ctl, DropDownList), ht(strKey).ToString())
                End If
            Next

            If ht("txtContractDateB").ToString() <> "" Then
                ContractDate_TextChanged(Nothing, Nothing)
                ddlRecID.SelectedValue = ht("ddlRecID").ToString()
            End If

            If ht.ContainsKey("PageNo") Then pcMain.PageNo = Convert.ToInt32(ht("PageNo"))
            If ht.ContainsKey("DoQuery") Then
                If ht("DoQuery").ToString() = "Y" Then
                    ViewState.Item("DoQuery") = "Y"
                    DoQuery()
                End If
            End If
        End If
    End Sub

    Private Sub LoadDate()
        txtContractDateB.Text = Now.ToString("yyyy/MM/dd")
        txtContractDateE.Text = Now.ToString("yyyy/MM/dd")

        Dim strCompID As String = ddlCompID.SelectedValue
        If strCompID = "" Then
            strCompID = UserProfile.SelectCompRoleID
        End If

        Bsp.Utility.FillDDL(ddlRecID, "Recruit", "RE_ContractData C", "C.RecID", "R.NameN", Bsp.Utility.DisplayType.Full, _
                    "INNER JOIN RE_Recruit AS R ON C.RecID = R.RecID", _
                    "AND C.CompID = " & Bsp.Utility.Quote(strCompID) & " AND C.ContractDate = " & Bsp.Utility.Quote(txtContractDateB.Text))
        ddlRecID.Items.Insert(0, New ListItem("---請選擇---", ""))
    End Sub

    Private Sub DoUpdate()
        Dim btnU As New ButtonState(ButtonState.emButtonType.Update)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
        Dim btnC As New ButtonState(ButtonState.emButtonType.Cancel)

        btnU.Caption = "存檔返回"
        btnX.Caption = "返回"
        btnC.Caption = "清除"

        Dim aa = gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString()
        Dim bb = gvMain.DataKeys(selectedRow(gvMain))("Name").ToString()
        Dim cc = gvMain.DataKeys(selectedRow(gvMain))("RecID").ToString()
        Dim dd = gvMain.DataKeys(selectedRow(gvMain))("CheckInDate").ToString()

        Me.TransferFramePage("~/RG/RG1502.aspx", New ButtonState() {btnU, btnX, btnC}, _
            "PageNo=" & pcMain.PageNo.ToString(), _
            Bsp.Utility.FormatToParam(txtContractDateB), _
            Bsp.Utility.FormatToParam(txtContractDateE), _
            "ddlRecID=" & ddlRecID.SelectedValue, _
            "SelectedCompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
            "SelectedRecID=" & gvMain.DataKeys(selectedRow(gvMain))("RecID").ToString(), _
            "SelectedCheckInDate=" & gvMain.DataKeys(selectedRow(gvMain))("CheckInDate").ToString(), _
            "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
    End Sub

    Private Sub DoQuery()
        If funCheckData() Then
            Dim objRG1 As New RG1()

            Dim strCompID As String = ddlCompID.SelectedValue
            If strCompID = "" Then
                strCompID = UserProfile.SelectCompRoleID
            End If

            Try
                pcMain.DataTable = objRG1.QueryRE_ContractData(
                    "CompID=" & strCompID, _
                    "ContractDateB=" & txtContractDateB.Text, _
                    "ContractDateE=" & txtContractDateE.Text, _
                    "RecID=" & ddlRecID.SelectedValue)

                gvMain.Visible = True

            Catch ex As Exception
                Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
            End Try
        End If
    End Sub

    Protected Sub gvMain_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMain.RowCommand
        If e.CommandName = "Detail" Then
            Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
            btnX.Caption = "返回"

            If UserProfile.SelectCompRoleID = "ALL" Then
                Me.TransferFramePage("~/RG/RG1502.aspx", New ButtonState() {btnX}, _
                    "ddlCompID=" & ddlCompID.SelectedValue, _
                    "PageNo=" & pcMain.PageNo.ToString(), _
                    Bsp.Utility.FormatToParam(txtContractDateB), _
                    Bsp.Utility.FormatToParam(txtContractDateE), _
                    "ddlRecID=" & ddlRecID.SelectedValue, _
                    "SelectedCompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
                    "SelectedRecID=" & gvMain.DataKeys(selectedRow(gvMain))("RecID").ToString(), _
                    "SelectedCheckInDate=" & gvMain.DataKeys(selectedRow(gvMain))("CheckInDate").ToString(), _
                    "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
            Else
                Me.TransferFramePage("~/RG/RG1502.aspx", New ButtonState() {btnX}, _
                    "PageNo=" & pcMain.PageNo.ToString(), _
                    Bsp.Utility.FormatToParam(txtContractDateB), _
                    Bsp.Utility.FormatToParam(txtContractDateE), _
                    "ddlRecID=" & ddlRecID.SelectedValue, _
                    "SelectedCompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
                    "SelectedRecID=" & gvMain.DataKeys(selectedRow(gvMain))("RecID").ToString(), _
                    "SelectedCheckInDate=" & gvMain.DataKeys(selectedRow(gvMain))("CheckInDate").ToString(), _
                    "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
            End If

        End If
    End Sub

    Private Sub DoClear()
        ViewState.Item("DoQuery") = ""

        gvMain.Visible = False
    End Sub

    Private Function funCheckData() As Boolean

        If txtContractDateB.Text.Trim() = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "預計報到日(起)")
            txtContractDateB.Focus()
            Return False
        Else
            If Bsp.Utility.CheckDate(txtContractDateB.Text) = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00070", "預計報到日(起)")
                txtContractDateB.Focus()
                Return False
            End If
        End If

        If txtContractDateE.Text.Trim() <> "" Then
            If Bsp.Utility.CheckDate(txtContractDateE.Text) = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00070", "預計報到日(迄)")
                txtContractDateE.Focus()
                Return False
            End If

            If txtContractDateB.Text > txtContractDateE.Text Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00130")
                txtContractDateB.Focus()
                Return False
            End If
        End If

        Return True
    End Function

    Protected Sub ContractDate_TextChanged(sender As Object, e As System.EventArgs)
        Dim strCompID As String = ddlCompID.SelectedValue
        If strCompID = "" Then
            strCompID = UserProfile.SelectCompRoleID
        End If

        ddlRecID.Items.Clear()
        ddlRecID.Items.Insert(0, New ListItem("---請選擇---", ""))

        If Bsp.Utility.CheckDate(txtContractDateB.Text) <> "" Then
            If txtContractDateE.Text.Trim() = "" Then
                Bsp.Utility.FillDDL(ddlRecID, "Recruit", "RE_ContractData C", "distinct C.RecID", "R.NameN", Bsp.Utility.DisplayType.Full, _
                                    "INNER JOIN RE_Recruit AS R ON C.RecID = R.RecID", _
                                    "AND C.CompID = " & Bsp.Utility.Quote(strCompID) & " AND C.ContractDate = " & Bsp.Utility.Quote(txtContractDateB.Text))
                ddlRecID.Items.Insert(0, New ListItem("---請選擇---", ""))
            Else
                If Bsp.Utility.CheckDate(txtContractDateE.Text) <> "" Then
                    Bsp.Utility.FillDDL(ddlRecID, "Recruit", "RE_ContractData C", "distinct C.RecID", "R.NameN", Bsp.Utility.DisplayType.Full, _
                                        "INNER JOIN RE_Recruit AS R ON C.RecID = R.RecID", _
                                        "AND C.CompID = " & Bsp.Utility.Quote(strCompID) & " AND C.ContractDate Between " & Bsp.Utility.Quote(txtContractDateB.Text) & " And " & Bsp.Utility.Quote(txtContractDateE.Text))
                    ddlRecID.Items.Insert(0, New ListItem("---請選擇---", ""))
                End If
            End If
        End If
    End Sub

    Private Sub DoDownload()
        Try
            If gvMain.Rows.Count > 0 Then
                '產出檔頭
                Dim strFileName As String = Bsp.Utility.GetNewFileName("RG1500不報到員工資料輸入_") & ".xls"

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

                gvExport.DataSource = pcMain.DataTable
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
                Bsp.Utility.ShowFormatMessage(Me, "W_00030", "請先查詢有資料，才能下傳!")
            End If

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoDownload", ex)
        End Try
    End Sub

    Public Sub GroupRows(ByVal GridViewData As GridView)
        GridViewData.HeaderRow.Cells(0).Text = "公司編號"
        GridViewData.HeaderRow.Cells(1).Text = "公司名稱"
        GridViewData.HeaderRow.Cells(2).Text = "登記日期"
        GridViewData.HeaderRow.Cells(3).Text = "應試者編號"
        GridViewData.HeaderRow.Cells(4).Text = "姓名"
        GridViewData.HeaderRow.Cells(5).Text = "不報到註記"
        GridViewData.HeaderRow.Cells(6).Text = "預計報到日"
        GridViewData.HeaderRow.Cells(7).Text = "不報到原因"
        GridViewData.HeaderRow.Cells(8).Text = "最後異動公司"
        GridViewData.HeaderRow.Cells(9).Text = "最後異動公司"
        GridViewData.HeaderRow.Cells(10).Text = "最後異動者"
        GridViewData.HeaderRow.Cells(11).Text = "最後異動者"
        GridViewData.HeaderRow.Cells(12).Text = "最後異動日期"

        GridViewData.HeaderRow.Cells(0).Visible = False
        GridViewData.HeaderRow.Cells(1).Visible = False
        GridViewData.HeaderRow.Cells(2).Visible = False
        GridViewData.HeaderRow.Cells(8).Visible = False
        GridViewData.HeaderRow.Cells(10).Visible = False

        For i As Integer = 0 To GridViewData.Rows.Count - 1
            GridViewData.Rows(i).Cells(0).Visible = False
            GridViewData.Rows(i).Cells(1).Visible = False
            GridViewData.Rows(i).Cells(2).Visible = False
            GridViewData.Rows(i).Cells(8).Visible = False
            GridViewData.Rows(i).Cells(10).Visible = False
        Next
    End Sub
End Class
