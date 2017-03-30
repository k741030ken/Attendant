'****************************************************
'功能說明：員工訓練資料查詢
'建立人員：BeatriceCheng
'建立日期：2015.06.11
'****************************************************
Imports System.Data

Partial Class ST_ST2300
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            ddlCompID.Visible = False
            If UserProfile.SelectCompRoleID = "ALL" Then
                ddlCompID.Visible = True
                Bsp.Utility.FillHRCompany(ddlCompID)
                'ddlCompID.Items.Insert(0, New ListItem("全金控", "0"))
                lblCompRoleID.Visible = False

            Else
                ddlCompID.Visible = False
                lblCompRoleID.Text = UserProfile.SelectCompRoleName
                lblCompRoleID.Visible = True
            End If
        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnQuery"     '查詢
                ViewState.Item("DoQuery") = "Y"
                DoQuery()
            Case "btnDownload"    '刪除
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
                Else
                    If TypeOf ctl Is UserControl Then
                        txtBeginDateB.DateText = ht("txtBeginDateB").ToString()
                        txtBeginDateE.DateText = ht("txtBeginDateE").ToString()
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

    Private Sub DoQuery()
        Dim objST2 As New ST2()

        Dim strCompID As String = ddlCompID.SelectedValue
        If strCompID = "" Then
            strCompID = UserProfile.SelectCompRoleID
        End If

        Try
            If funCheckData() Then
                pcMain.DataTable = objST2.TrainingQuery(
                    "CompID=" & strCompID, _
                    "EmpIDB=" & txtEmpIDB.Text.ToUpper(), _
                    "EmpIDE=" & txtEmpIDE.Text.ToUpper(), _
                    "BeginDateB=" & txtBeginDateB.DateText, _
                    "BeginDateE=" & txtBeginDateE.DateText, _
                    "Name=" & txtName.Text)
                gvMain.Visible = True
            End If
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
        End Try
    End Sub

    Private Sub DoDownload()
        Try
            If gvMain.Rows.Count > 0 Then
                '產出檔頭
                Dim strFileName As String = Bsp.Utility.GetNewFileName("ST2300員工訓練-") & ".xls"

                '動態產生GridView以便匯出成EXCEL
                Dim gvExport As GridView = New GridView()
                gvExport.AllowPaging = False
                gvExport.AllowSorting = False
                gvExport.FooterStyle.BackColor = Drawing.ColorTranslator.FromHtml("#99CCCC")
                gvExport.FooterStyle.ForeColor = Drawing.ColorTranslator.FromHtml("#003399")
                gvExport.RowStyle.CssClass = "tr_evenline"
                gvExport.AlternatingRowStyle.CssClass = "tr_oddline"
                gvExport.EmptyDataRowStyle.CssClass = "GridView_EmptyRowStyle"

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

    Protected Sub gvMain_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMain.RowCommand
        If e.CommandName = "Detail" Then
            Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
            btnX.Caption = "返回"

            If UserProfile.SelectCompRoleID = "ALL" Then
                Me.TransferFramePage("~/ST/ST2302.aspx", New ButtonState() {btnX}, _
                    "ddlCompID=" & ddlCompID.SelectedValue, _
                    Bsp.Utility.FormatToParam(txtEmpIDB), _
                    Bsp.Utility.FormatToParam(txtEmpIDE), _
                    "txtBeginDateB=" & txtBeginDateB.DateText, _
                    "txtBeginDateE=" & txtBeginDateE.DateText, _
                    Bsp.Utility.FormatToParam(txtName), _
                    "PageNo=" & pcMain.PageNo.ToString(), _
                    "SelectedCompID=" & ddlCompID.SelectedValue, _
                    "SelectedIDNo=" & gvMain.DataKeys(selectedRow(gvMain))("IDNo").ToString(), _
                    "SelectedBeginDate=" & gvMain.DataKeys(selectedRow(gvMain))("BeginDate").ToString(), _
                    "SelectedLessonName=" & gvMain.DataKeys(selectedRow(gvMain))("LessonName").ToString(), _
                    "SelectedLessonID=" & gvMain.DataKeys(selectedRow(gvMain))("LessonID").ToString(), _
                    "SelectedActivityID=" & gvMain.DataKeys(selectedRow(gvMain))("ActivityID").ToString(), _
                    "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
            Else
                Me.TransferFramePage("~/ST/ST2302.aspx", New ButtonState() {btnX}, _
                    Bsp.Utility.FormatToParam(txtEmpIDB), _
                    Bsp.Utility.FormatToParam(txtEmpIDE), _
                    "txtBeginDateB=" & txtBeginDateB.DateText, _
                    "txtBeginDateE=" & txtBeginDateE.DateText, _
                    Bsp.Utility.FormatToParam(txtName), _
                    "PageNo=" & pcMain.PageNo.ToString(), _
                    "SelectedCompID=" & UserProfile.SelectCompRoleID, _
                    "SelectedIDNo=" & gvMain.DataKeys(selectedRow(gvMain))("IDNo").ToString(), _
                    "SelectedBeginDate=" & gvMain.DataKeys(selectedRow(gvMain))("BeginDate").ToString(), _
                    "SelectedLessonName=" & gvMain.DataKeys(selectedRow(gvMain))("LessonName").ToString(), _
                    "SelectedLessonID=" & gvMain.DataKeys(selectedRow(gvMain))("LessonID").ToString(), _
                    "SelectedActivityID=" & gvMain.DataKeys(selectedRow(gvMain))("ActivityID").ToString(), _
                    "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
            End If

        End If
    End Sub

    Private Function funCheckData() As Boolean
        '員工編號        
        If txtEmpIDB.Text <> "" And txtEmpIDE.Text <> "" Then
            If txtEmpIDB.Text > txtEmpIDE.Text Then
                Bsp.Utility.ShowFormatMessage(Me, "W_20720", lblEmpID.Text & "(迄)", lblEmpID.Text & "(起)")
                txtEmpIDB.Focus()
                Return False
            End If
        End If

        '開始日期
        If txtBeginDateB.DateText <> "" And txtBeginDateE.DateText <> "" Then
            If txtBeginDateB.DateText > txtBeginDateE.DateText Then
                Bsp.Utility.ShowFormatMessage(Me, "W_20720", lblBeginDate.Text & "(迄)", lblBeginDate.Text & "(起)")
                txtEmpIDB.Focus()
                Return False
            End If
        End If

        Return True
    End Function

    Public Sub GroupRows(ByVal GridViewData As GridView)
        GridViewData.HeaderRow.Cells(0).Text = "員工身分證字號"
        GridViewData.HeaderRow.Cells(1).Text = "員工編號"
        GridViewData.HeaderRow.Cells(2).Text = "姓名"
        GridViewData.HeaderRow.Cells(3).Text = "開始日期"
        GridViewData.HeaderRow.Cells(4).Text = "結束日期"
        GridViewData.HeaderRow.Cells(5).Text = "開課單位"
        GridViewData.HeaderRow.Cells(6).Text = "課程代號"
        GridViewData.HeaderRow.Cells(7).Text = "課程名稱"
        GridViewData.HeaderRow.Cells(8).Text = "班別代號"
        GridViewData.HeaderRow.Cells(9).Text = "上課時數"
        GridViewData.HeaderRow.Cells(10).Text = "訓練費用"
        GridViewData.HeaderRow.Cells(11).Text = "訓練別名稱"
        GridViewData.HeaderRow.Cells(12).Text = "訓練時部門名稱"

        GridViewData.HeaderRow.Cells(0).Visible = False

        Dim i As Integer = 0
        While i < GridViewData.Rows.Count
            GridViewData.Rows(i).Cells(0).Visible = False
            i = i + 1
        End While
    End Sub

End Class
