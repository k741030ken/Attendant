'****************************************************
'功能說明：取得召募應試者資料
'建立人員：Micky
'建立日期：2015.08.31
'****************************************************
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports System.Data.Common
Imports System.Data
Imports Newtonsoft.Json

Partial Class Component_PageSelectRecruit
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim aryParam() As Object = Session(Request("ControlSessionID"))

            ViewState.Item("bolCompRole") = aryParam(0)
            ViewState.Item("InValidFlag") = aryParam(5)
            ViewState.Item("SelectCompID") = aryParam(6)

            LoadField()
        End If
    End Sub

    Private Sub LoadField()
        Dim objSC As New SC()
        '公司
        txtCompID.Text = ViewState.Item("SelectCompID") + "-" + objSC.GetCompName(ViewState.Item("SelectCompID")).Rows(0).Item("CompName").ToString
        hidCompID.Value = ViewState.Item("SelectCompID")
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnQuery"     '查詢
                ViewState.Item("DoQuery") = "Y"
                DoQuery()
            Case "btnActionC"   '選取
                'DoActionC()

                '2015/11/18 Add 未選取時顯示錯誤訊息
                If selectedRow(gvMain) < 0 Then
                    Bsp.Utility.ShowFormatMessage(Me, "W_00000")
                    Return
                End If

                '2015/11/19 Modify
                If gvMain.Rows(selectedRow(gvMain)).Cells(1).Text = gvMain.Rows(selectedRow(gvMain)).Cells(3).Text Then
                    Bsp.Utility.RunClientScript(Me, "alert('" & "「應試者的身分證字號有問題，系統將無法自動將招募系統-學歷資料\前職資料\家庭資料\通訊資料\核薪資料，直接帶入系統」" & "');" & vbCrLf & "window.top.returnValue = 'OK';")
                End If

                Dim ReturnValue As String
                ReturnValue = hidCompID.Value & "|$|" & gvMain.Rows(selectedRow(gvMain)).Cells(1).Text & "|$|" & gvMain.DataKeys(Me.selectedRow(gvMain))("CheckInDate").ToString()  '2015/11/19 Modify
                Bsp.Utility.RunClientScript(Me, "window.top.returnValue='" & ReturnValue & "';window.top.close();")
            Case "btnDownload"  '下載
                If ViewState.Item("DoQuery") = "Y" Then
                    If gvMain.Rows.Count = 0 Then   '2015/12/01 Modify
                        Bsp.Utility.ShowMessage(Me, "無資料下傳！")
                    Else
                        DoDownload()
                    End If
                Else
                    Bsp.Utility.ShowMessage(Me, "請先查詢！")
                End If
        End Select
    End Sub

    Private Sub DoQuery()
        gvMain.Visible = True
        LoadData()
    End Sub

    Protected Sub LoadData()
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" SELECT ")
        strSQL.AppendLine(" C.CompID, R.RecID, isnull(R.NameN, '') as NameN, isnull(R.IDNo,'') as IDNo ")
        strSQL.AppendLine(" , Convert(char(10), C.ContractDate, 111) as ContractDate ")
        strSQL.AppendLine(" , C1.GroupID, isnull(C1.DeptID + '-' + O1.OrganName, '') as DeptID, isnull(C1.OrganID + '-' + O2.OrganName, '') as OrganID ")
        strSQL.AppendLine(" , Convert(char(10), C.CheckInDate, 111) as CheckInDate ")
        strSQL.AppendLine(" , isnull((Select Code + ' ' + CodeName From RE_CodeMap Where Code = C1.RecIdentityID and TabName = 'RE_CheckInData' and FldName = 'RecIdentityID'), '') as RecIdentity ")
        strSQL.AppendLine(" , isnull(C1.RecIdentityRemark,'') as RecIdentityRemark ")
        strSQL.AppendLine(" , E.PositionID + ISNULL(RP.Remark, '') AS PositionID, BirthDate = Case When Convert(Char(10), R.BirthDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, R.BirthDate, 111) END ")
        strSQL.AppendLine(" From RE_ContractData C ")
        strSQL.AppendLine(" Inner Join RE_CheckInData C1 On C.CompID = C1.CompID and C.RecID = C1.RecID and C.CheckInDate = C1.CheckInDate ")
        strSQL.AppendLine(" Inner Join RE_Recruit R On C.RecID = R.RecID ")
        strSQL.AppendLine(" Inner Join RE_EmpPosition E On C.CompID=E.CompID and C.RecID = E.RecID  and C.CheckInDate =E.CheckInDate")
        strSQL.AppendLine(" Left Join SC_Organization O1 On C1.CompID = O1.CompID and C1.DeptID = O1.OrganID ")
        strSQL.AppendLine(" Left Join SC_Organization O2 On C1.CompID = O2.CompID and C1.OrganID = O2.OrganID ")
        strSQL.AppendLine(" Left Join RE_Position RP on RP.CompID = C1.CompID and RP.PositionID = E.PositionID ")
        strSQL.AppendLine(" WHERE 1 = 1 ")
        strSQL.AppendLine(" AND C.CompID = " & Bsp.Utility.Quote(hidCompID.Value))

        '2015/11/19 Modify
        'If txtRecID.Text <> "" Then
        '    strSQL.AppendLine(" AND R.RecID LIKE '%" & txtRecID.Text + "%' ")
        'End If
        'If txtNameN.Text <> "" Then
        '    strSQL.AppendLine(" OR R.NameN LIKE '%" & txtNameN.Text + "%' ")
        'End If
        If txtRecID.Text <> "" And txtNameN.Text <> "" Then
            strSQL.AppendLine(" AND (R.RecID LIKE '%" & txtRecID.Text + "%' OR R.NameN LIKE N'%" & txtNameN.Text + "%') ")
        ElseIf txtRecID.Text <> "" And txtNameN.Text = "" Then
            strSQL.AppendLine(" AND R.RecID LIKE '%" & txtRecID.Text + "%' ")
        ElseIf txtRecID.Text = "" And txtNameN.Text <> "" Then
            strSQL.AppendLine(" AND R.NameN LIKE N'%" & txtNameN.Text + "%' ")
        End If

        strSQL.AppendLine(" AND Convert(char(10), C.ContractDate, 111) <> '1900/01/01' ")
        strSQL.AppendLine(" AND C.FinalFlag = '' ")
        strSQL.AppendLine(" AND C.CheckInFlag = '' ")
        strSQL.AppendLine(" AND Convert(char(10), C.EmpDate, 111) = '1900/01/01' ")
        strSQL.AppendLine(" AND E.REType = '1' and E.PrincipalFlag = '1' ")
        strSQL.AppendLine(" Order By C.ContractDate, R.RecID ")

        Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "Recruit").Tables(0)
            Dim dc As New DataColumn
            dc.ColumnName = "_Key"
            dc.DataType = System.Type.GetType("System.String")
            dt.Columns.Add(dc)

            For intLoop As Integer = 0 To dt.Rows.Count - 1
                dt.Rows(intLoop).Item("_Key") = intLoop.ToString("00000000")
            Next
            dt.PrimaryKey = New DataColumn() {dc}

            gvMain.DataSource = dt
            gvMain.DataBind()
            StateMain = dt
        End Using
    End Sub

    Protected Sub gvMain_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMain.RowCommand
        If e.CommandName = "select" Then
            Dim index As Integer = selectedRow(gvMain)
            txtReturnValue.Text = ((gvMain.DataKeys(index)("CompID").ToString()) & "|$|" & (gvMain.DataKeys(index)("RecID").ToString()) & "|$|" & (gvMain.DataKeys(index)("CheckInDate").ToString()))
            Bsp.Utility.RunClientScript(Me, "window.top.returnValue='" & Replace(txtReturnValue.Text, "'", "\'") & "';window.top.close();")
        End If
    End Sub

    Private Sub DoDownload()
        Try
            Dim objUC As New UC()
            '產出檔頭
            Dim strFileName As String = Bsp.Utility.GetNewFileName("召募應試者資料下載-") & ".xls"

            '動態產生GridView以便匯出成EXCEL
            Dim gvExport As GridView = New GridView()
            gvExport.AllowPaging = False
            gvExport.AllowSorting = False
            gvExport.FooterStyle.BackColor = Drawing.ColorTranslator.FromHtml("#99CCCC")
            gvExport.FooterStyle.ForeColor = Drawing.ColorTranslator.FromHtml("#003399")
            gvExport.RowStyle.CssClass = "tr_evenline"
            gvExport.AlternatingRowStyle.CssClass = "tr_oddline"
            gvExport.EmptyDataRowStyle.CssClass = "GridView_EmptyRowStyle"

            gvExport.DataSource = objUC.QueryRE_ContractDataByDownload( _
                "CompID=" & hidCompID.Value, _
                "RecID=" & txtRecID.Text, _
                "NameN=" & txtNameN.Text)

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
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoDownload", ex)
        End Try
    End Sub

    Private Sub DoActionC()
        txtReturnValue.Text = ""
        Dim aryColumn() As String = Nothing
        Dim dt As DataTable = StateMain
        Dim ReturnValue As String = ""
        ReturnValue = hidCompID.Value & "|$|" & gvMain.Rows(selectedRow(gvMain)).Cells(1).Text & gvMain.Rows(selectedRow(gvMain)).Cells(4).Text

        Bsp.Utility.RunClientScript(Me, "window.top.returnValue='" & ReturnValue & "';window.top.close();")
    End Sub

    '2015/11/18 Add 換頁
    Protected Sub gvData_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvMain.PageIndexChanging
        gvMain.PageIndex = e.NewPageIndex
        LoadData()
    End Sub
End Class
