'****************************************************
'功能說明：新進員工文件繳交作業_銀行
'建立人員：Micky Sung
'建立日期：2015.07.03
'****************************************************
Imports System.Data

Partial Class RG_RG1300
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            '公司代碼
            If UserProfile.SelectCompRoleID = "ALL" Then
                lblCompRoleID.Visible = False
                Bsp.Utility.FillHRCompany(ddlCompID)
                Page.SetFocus(ddlCompID)

            Else
                lblCompRoleID.Text = UserProfile.SelectCompRoleName
                ddlCompID.Visible = False
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
                'DoDelete()
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

        Me.TransferFramePage("~/RG/RG1301.aspx", New ButtonState() {btnA, btnX, btnC}, _
            ddlCompID.ID & "=" & strCompID, _
            ddlEmpDay.ID & "=" & ddlEmpDay.SelectedValue, _
            txtEmpID.ID & "=" & txtEmpID.Text, _
            txtName.ID & "=" & txtName.Text, _
            ddlStatus.ID & "=" & ddlStatus.SelectedValue, _
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

        Me.TransferFramePage("~/RG/RG1302.aspx", New ButtonState() {btnU, btnX, btnC}, _
            ddlCompID.ID & "=" & strCompID, _
            ddlEmpDay.ID & "=" & ddlEmpDay.SelectedValue, _
            txtEmpID.ID & "=" & txtEmpID.Text, _
            txtName.ID & "=" & txtName.Text, _
            ddlStatus.ID & "=" & ddlStatus.SelectedValue, _
            "PageNo=" & pcMain.PageNo.ToString(), _
            "SelectedCompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
            "SelectedEmpID=" & gvMain.DataKeys(selectedRow(gvMain))("EmpID").ToString(), _
            "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
    End Sub

    Private Sub DoQuery()
        Dim objRG As New RG1()
        gvMain.Visible = True
        ViewState.Item("DoQuery") = "Y"

        Dim strCompID As String
        If UserProfile.SelectCompRoleID = "ALL" Then
            strCompID = ddlCompID.SelectedValue
        Else
            strCompID = UserProfile.SelectCompRoleID
        End If

        Try
            pcMain.DataTable = objRG.QueryCheckInFile_SPHBK1(
                "CompID=" & strCompID, _
                "EmpDay=" & ddlEmpDay.SelectedValue, _
                "EmpID=" & txtEmpID.Text, _
                "Name=" & txtName.Text, _
                "Status=" & ddlStatus.SelectedValue)

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
        End Try
    End Sub

    Private Sub DoDelete()
        If selectedRow(gvMain) < 0 Then
            '尚未選取資料列！
            Bsp.Utility.ShowFormatMessage(Me, "W_00000")
        Else
            Dim beCheckInFile_SPHBK1 As New beCheckInFile_SPHBK1.Row()
            Dim objRG As New RG1

            Dim strCompID As String
            If UserProfile.SelectCompRoleID = "ALL" Then
                strCompID = ddlCompID.SelectedValue
            Else
                strCompID = UserProfile.SelectCompRoleID
            End If

            beCheckInFile_SPHBK1.CompID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("CompID").ToString()
            beCheckInFile_SPHBK1.EmpID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("EmpID").ToString()

            Try
                objRG.DeleteCheckInFile_SPHBK1(beCheckInFile_SPHBK1)
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

            Me.TransferFramePage("~/RG/RG1302.aspx", New ButtonState() {btnX}, _
                ddlCompID.ID & "=" & strCompID, _
                ddlEmpDay.ID & "=" & ddlEmpDay.SelectedValue, _
                txtEmpID.ID & "=" & txtEmpID.Text, _
                txtName.ID & "=" & txtName.Text, _
                ddlStatus.ID & "=" & ddlStatus.SelectedValue, _
                "PageNo=" & pcMain.PageNo.ToString(), _
                "SelectedCompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
                "SelectedEmpID=" & gvMain.DataKeys(selectedRow(gvMain))("EmpID").ToString(), _
                "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))

        End If
    End Sub

    Private Sub DoClear()
        ViewState.Item("DoQuery") = ""
        gvMain.Visible = False

        '公司代碼
        ddlCompID.SelectedValue = ""

        '報到日迄今
        ddlEmpDay.SelectedIndex = 0

        '文件繳交狀態
        ddlStatus.SelectedIndex = 0

        '員工編號
        txtEmpID.Text = ""

        '員工姓名
        txtName.Text = ""
    End Sub

    Public Overrides Sub DoModalReturn(ByVal returnValue As String)
        Dim strSql As String = ""

        If returnValue <> "" Then
            Dim aryData() As String = returnValue.Split(":")

            Select Case aryData(0)
                Case "ucQueryEmp"
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    '員工編號
                    txtEmpID.Text = aryValue(1).ToUpper
                    '員工姓名
                    txtName.Text = aryValue(2)
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
        ucRelease.FunID = "RG1300"
        ucRelease.LogFunction = LogFunction
        ucRelease.OpenSelect()
    End Sub

    Private Sub DoDownload()
        Try
            If ddlStatus.SelectedValue <> "1" Then
                Bsp.Utility.ShowMessage(Me, "【文件繳交狀態】請挑選「未繳齊」，才可下傳")
            Else
                If pcMain.DataTable.Rows.Count > 0 Then
                    Dim objRG As New RG1()
                    Dim strFileName As String = Bsp.Utility.GetNewFileName("RG1300銀行文件繳交紀錄表_") & ".xls"

                    Dim gvExport As GridView = New GridView()
                    gvExport.AllowPaging = False
                    gvExport.AllowSorting = False
                    gvExport.FooterStyle.BackColor = Drawing.ColorTranslator.FromHtml("#99CCCC")
                    gvExport.FooterStyle.ForeColor = Drawing.ColorTranslator.FromHtml("#003399")
                    gvExport.RowStyle.CssClass = "tr_evenline"
                    gvExport.AlternatingRowStyle.CssClass = "tr_oddline"
                    gvExport.EmptyDataRowStyle.CssClass = "GridView_EmptyRowStyle"

                    Dim strCompID As String
                    If UserProfile.SelectCompRoleID = "ALL" Then
                        strCompID = ddlCompID.SelectedValue
                    Else
                        strCompID = UserProfile.SelectCompRoleID
                    End If

                    gvExport.DataSource = objRG.CheckInFile_SPHBK1Download( _
                        "CompID=" & strCompID, _
                        "EmpDay=" & ddlEmpDay.SelectedValue, _
                        "EmpID=" & txtEmpID.Text, _
                        "Name=" & txtName.Text)

                    gvExport.DataBind()
                    GroupRows(gvExport) '表頭

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
        Dim HeaderGridRow As GridViewRow = New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)
        Dim HeaderCell As New TableCell()
        HeaderCell.Text = "RG1300　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　永豐商業銀行─人力資源處　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　產生日期：" + Format(Now, "yyyy/MM/dd") + "<br>新進人員文件缺繳報表─永豐銀行<br>報到日迄今" + ddlEmpDay.SelectedItem.Text
        HeaderCell.HorizontalAlign = HorizontalAlign.Center
        HeaderCell.ColumnSpan = "25"
        HeaderGridRow.Cells.Add(HeaderCell)
        GridViewData.Controls(0).Controls.AddAt(0, HeaderGridRow)
    End Sub


End Class
