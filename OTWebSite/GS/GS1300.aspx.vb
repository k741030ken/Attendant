'****************************************************
'功能說明：年度考核(單位主管查詢)
'建立人員：Weicheng
'建立日期：2015.11.16
'****************************************************
Imports System.Data
Imports SinoPac.WebExpress.Common   '20160513 wei add NetAP dll匯出xlsx使用
Imports OfficeOpenXml   '20160513 wei add
Imports OfficeOpenXml.Style '20160513 wei add

Partial Class GS_GS1300
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ViewState.Item("RowAdd") = False
        If Not IsPostBack Then

        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Dim strWhere As String = ""
        Dim objHR As New HR()
        Dim objGS As New GS1()
        Select Case Param
            Case "btnDownload"  '結果檔下傳
                DoDownload()
            Case "btnCopy"      '統計表
                TransferFPage("GS1220")
        End Select
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)
            Dim objGS As New GS1()
            Dim strWhere As String = ""
            Dim objHR As New HR()

            ViewState.Item("CompID") = ht("CompID").ToString()
            ViewState.Item("ApplyID") = ht("ApplyID").ToString()
            ViewState.Item("ApplyTime") = ht("ApplyTime").ToString()
            ViewState.Item("Seq") = ht("Seq").ToString()
            ViewState.Item("Status") = ht("Status").ToString()
            ViewState.Item("MainFlag") = ht("MainFlag").ToString()
            ViewState.Item("GradeYear") = ht("GradeYear").ToString()
            ViewState.Item("GradeSeq") = ht("GradeSeq").ToString()
            ViewState.Item("IsSignNext") = ht("IsSignNext").ToString()
            ViewState.Item("Result") = ht("Result").ToString()

            '20160826 wei add
            Using dt As DataTable = objGS.GetScoreValueParameter(ViewState.Item("CompID"), ViewState.Item("GradeYear"))
                If dt.Rows.Count > 0 Then
                    ViewState.Item("ScoreValue") = dt.Rows.Item(0)("FinalScore").ToString()
                    ViewState.Item("EvaluationSeq") = dt.Rows.Item(0)("EvaluationSeq").ToString()
                End If
            End Using

            Dim posGroup As Integer = 1
            For iGroup As Integer = 1 To 3

                If objGS.GS1300QueryCount(ViewState.Item("CompID"), ViewState.Item("ApplyID"), ViewState.Item("ApplyTime"), ViewState.Item("Seq"), ViewState.Item("Status"), ViewState.Item("MainFlag"), ViewState.Item("GradeYear"), ViewState.Item("GradeSeq"), ViewState.Item("EvaluationSeq"), iGroup.ToString()) > 0 Then
                    posGroup = iGroup
                Else
                    Select Case iGroup
                        Case "1"
                            rbnGroup1.Style("Display") = "none"
                        Case "2"
                            rbnGroup2.Style("Display") = "none"
                        Case "3"
                            rbnGroup3.Style("Display") = "none"
                    End Select
                End If
            Next

            If ht.ContainsKey("GroupID") Then
                ViewState.Item("GroupID") = ht("GroupID").ToString()
            Else
                ViewState.Item("GroupID") = posGroup
            End If

            Select Case ViewState.Item("GroupID")
                Case "1"
                    rbnGroup1.Checked = True
                Case "2"
                    rbnGroup2.Checked = True
                Case "3"
                    rbnGroup3.Checked = True
            End Select


            '判斷是否全排序
            strWhere = " And CompID = " & Bsp.Utility.Quote(ViewState.Item("CompID"))
            strWhere = " And (DeptID=" & Bsp.Utility.Quote(ViewState.Item("ApplyID")) & " Or DeptID In (Select UpOrganID From GradeBase Where CompID =" & Bsp.Utility.Quote(ViewState.Item("CompID")) & " and GradeYear=" & ViewState.Item("GradeYear") & " and GradeSeq=" & ViewState.Item("GradeSeq") & " and GradeDeptID=" & Bsp.Utility.Quote(ViewState.Item("ApplyID")) & "))"
            If objHR.IsDataExists("GradeDeptException", strWhere) Then
                ViewState.Item("DeptEX") = "Y"
            Else
                ViewState.Item("DeptEX") = "N"
            End If
            If ht.ContainsKey("ApplyID") Then
                subGetData(
                    ViewState.Item("CompID"), _
                    ViewState.Item("ApplyID"), _
                    ViewState.Item("ApplyTime"), _
                    ViewState.Item("Seq"), _
                    ViewState.Item("Status"), _
                    ViewState.Item("MainFlag"), _
                    ViewState.Item("GradeYear"), _
                    ViewState.Item("GradeSeq"), _
                    ViewState.Item("EvaluationSeq"))
            Else
                Return
            End If
        End If
    End Sub

    Private Sub subGetData(ByVal CompID As String, ByVal ApplyID As String, ByVal ApplyTime As String, ByVal Seq As String, ByVal Status As String, ByVal MainFlag As String _
                           , ByVal GradeYear As String, ByVal GradeSeq As String, ByVal EvaluationSeq As String)
        Dim objGS1 As New GS1()
        Dim objHR As New HR()

        Try
            Using dt As DataTable = objHR.GetHROrganName(CompID, ApplyID)
                If dt.Rows.Count > 0 Then
                    txtDeptID.Text = dt.Rows(0)(0).ToString()
                End If
            End Using

            Using dt As DataTable = objGS1.GS1300Query(CompID, ApplyID, ApplyTime, Seq, Status, MainFlag, GradeYear, GradeSeq, EvaluationSeq, ViewState.Item("GroupID"))
                If dt.Rows.Count > 50 Then
                    pcMain.PerPageRecord = 50
                Else
                    pcMain.PerPageRecord = dt.Rows.Count
                End If

                pcMain.DataTable = dt

            End Using
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
        End Try
    End Sub

    Protected Sub gvMain_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvMain.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            'If ViewState.Item("Status") = "4" Then
            '    e.Row.Cells(15).Visible = False
            'End If
            'If ViewState.Item("IsSignNext") = "N" Then
            '    e.Row.Cells(10).Visible = False
            'Else
            '    e.Row.Cells(11).Visible = False '20160114 wei add
            '    e.Row.Cells(12).Visible = False
            '    e.Row.Cells(13).Visible = False
            'End If

            e.Row.Cells(0).ColumnSpan = "2"
            e.Row.Cells(0).Text = "半年度"
            e.Row.Cells(2).RowSpan = "2"
            e.Row.Cells(3).RowSpan = "2"
            e.Row.Cells(4).RowSpan = "2"
            e.Row.Cells(5).RowSpan = "2"
            e.Row.Cells(6).ColumnSpan = "3"
            e.Row.Cells(6).Text = "近三年考績"
            e.Row.Cells(9).RowSpan = "2"
            e.Row.Cells(10).RowSpan = "2"


            e.Row.Cells().RemoveAt(1)

            e.Row.Cells().RemoveAt(6)
            e.Row.Cells().RemoveAt(6)


        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            'If ViewState.Item("Status") = "4" Then
            '    e.Row.Cells(15).Visible = False
            'End If
            'If ViewState.Item("IsSignNext") = "N" Then
            '    e.Row.Cells(10).Visible = False
            'Else
            '    e.Row.Cells(11).Visible = False '20160114 wei add
            '    e.Row.Cells(12).Visible = False
            '    e.Row.Cells(13).Visible = False
            'End If

            If ViewState.Item("RowAdd") = False Then
                Dim gvRow2 As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)

                'Dim tc24 As New TableCell()
                'tc24.Text = "初核考績"
                'tc24.CssClass = "td_header"
                'gvRow2.Cells.Add(tc24)

                Dim tc25 As New TableCell()
                tc25.Text = "核定考績"
                tc25.CssClass = "td_header"
                gvRow2.Cells.Add(tc25)

                Dim tc26 As New TableCell()
                tc26.Text = "排序"
                tc26.CssClass = "td_header"
                gvRow2.Cells.Add(tc26)


                Dim tc21 As New TableCell()
                tc21.Text = (CInt(ViewState.Item("GradeYear")) - 3).ToString()   '20160113 wei modify
                tc21.CssClass = "td_header"
                gvRow2.Cells.Add(tc21)

                Dim tc22 As New TableCell()
                tc22.Text = (CInt(ViewState.Item("GradeYear")) - 2).ToString()  '20160113 wei modify
                tc22.CssClass = "td_header"
                gvRow2.Cells.Add(tc22)

                Dim tc23 As New TableCell()
                tc23.Text = (CInt(ViewState.Item("GradeYear")) - 1).ToString()  '20160113 wei modify
                tc23.CssClass = "td_header"
                gvRow2.Cells.Add(tc23)

                gvMain.Controls(0).Controls.AddAt(1, gvRow2)
                ViewState.Item("RowAdd") = True
            End If
        End If
    End Sub

    Protected Sub gvMain_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvMain.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then


            Dim tmpBtn As New ImageButton
            '奬懲
            tmpBtn = e.Row.Cells(9).FindControl("ibtnReward")
            If DirectCast(gvMain.DataSource, DataTable).Rows(e.Row.RowIndex)("RewardCnt").ToString() > 0 Then
                tmpBtn.Visible = True
            End If
            '業績
            tmpBtn = e.Row.Cells(9).FindControl("ibtnPerformance")
            If DirectCast(gvMain.DataSource, DataTable).Rows(e.Row.RowIndex)("PerformanceCnt").ToString() > 0 Then
                tmpBtn.Visible = True
            End If
            '考核補充說明
            tmpBtn = e.Row.Cells(9).FindControl("ibtnComment")
            If DirectCast(gvMain.DataSource, DataTable).Rows(e.Row.RowIndex)("CommentCnt").ToString() > 0 Then
                tmpBtn.Visible = True
            End If

            For i = 0 To 0
                Select Case e.Row.Cells(i).Text
                    Case "1"
                        e.Row.Cells(i).Text = "優等"
                    Case "2"
                        e.Row.Cells(i).Text = "甲等"
                    Case "3"
                        e.Row.Cells(i).Text = "乙等"
                    Case "4"
                        e.Row.Cells(i).Text = "丙等"
                    Case "6"
                        e.Row.Cells(i).Text = "甲上"
                    Case "7"
                        e.Row.Cells(i).Text = "甲下"
                    Case "9"
                        e.Row.Cells(i).Text = "特優"
                    Case Else
                        e.Row.Cells(i).Text = ""
                End Select
            Next i
            For i = 6 To 8
                Select Case e.Row.Cells(i).Text
                    Case "1"
                        e.Row.Cells(i).Text = "優等"
                    Case "2"
                        e.Row.Cells(i).Text = "甲等"
                    Case "3"
                        e.Row.Cells(i).Text = "乙等"
                    Case "4"
                        e.Row.Cells(i).Text = "丙等"
                    Case "6"
                        e.Row.Cells(i).Text = "甲上"
                    Case "7"
                        e.Row.Cells(i).Text = "甲下"
                    Case "9"
                        e.Row.Cells(i).Text = "特優"
                    Case Else
                        e.Row.Cells(i).Text = ""
                End Select
            Next i

            'If ViewState.Item("IsSignNext") = "Y" Then
            '    Dim ScoreValue As String = DataBinder.Eval(e.Row.DataItem, "ScoreValue")
            '    Dim DeptScoreValue As String = DataBinder.Eval(e.Row.DataItem, "ScoreValueDept")
            '    If ScoreValue <> DeptScoreValue And ScoreValue <> 0 And DeptScoreValue <> 0 Then
            '        e.Row.Cells(10).ForeColor = Drawing.Color.Red
            '    End If
            'End If
            'If ViewState.Item("Status") = "4" And ViewState.Item("IsSignNext") = "N" Then
            '    Dim GradeAdjust As String = DataBinder.Eval(e.Row.DataItem, "GradeAdjust")
            '    Dim GradeHR As String = DataBinder.Eval(e.Row.DataItem, "GradeHR")

            '    If GradeHR <> "" And GradeAdjust <> "" And GradeHR <> GradeAdjust Then
            '        e.Row.Cells(13).ForeColor = Drawing.Color.Red
            '        e.Row.Cells(14).ForeColor = Drawing.Color.Red
            '    End If
            'End If
            'If ViewState.Item("Status") = "5" Then
            '    Dim GradeHR As String = DataBinder.Eval(e.Row.DataItem, "GradeHR")
            '    Dim Grade2 As String = DataBinder.Eval(e.Row.DataItem, "Grade2")
            '    If GradeHR <> "" And Grade2 <> "" And GradeHR <> Grade2 Then
            '        e.Row.Cells(14).ForeColor = Drawing.Color.Red
            '        e.Row.Cells(15).ForeColor = Drawing.Color.Red
            '    End If
            'End If


        End If
    End Sub
    Protected Sub gvMain_DataBound(sender As Object, e As EventArgs) Handles gvMain.DataBound
        ViewState.Item("RowAdd") = False
    End Sub

    Protected Sub gvMain_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMain.RowCommand
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
        btnX.Caption = "關閉"

        If e.CommandName = "Detail" Then
            Bsp.Utility.RunClientScript(Me, "ShowDialog('" & ConfigurationManager.AppSettings("eHRMSWEB").ToString & "?CompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString() & "&EmpID=" & gvMain.DataKeys(selectedRow(gvMain))("EmpID").ToString() & "&PromoteFlag=Y', '750', '580')")

        ElseIf e.CommandName = "Evaluation" Then
            Me.CallMiddlePage("~/GS/GS1120.aspx", New ButtonState() {btnX}, _
                "CompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
                "EmpID=" & gvMain.DataKeys(selectedRow(gvMain))("EmpID").ToString(), _
                "EvaluationYear=" & ViewState.Item("GradeYear"), _
                "EvaluationSeq=" & ViewState.Item("EvaluationSeq"))

        ElseIf e.CommandName = "Reward" Then
            Me.CallMiddlePage("~/GS/GS1130.aspx", New ButtonState() {btnX}, _
                "CompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
                "EmpID=" & gvMain.DataKeys(selectedRow(gvMain))("EmpID").ToString(), _
                "ValidYear=" & ViewState.Item("GradeYear"))

        ElseIf e.CommandName = "Performance" Then
            Me.CallMiddlePage("~/GS/GS1140.aspx", New ButtonState() {btnX}, _
                "CompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
                "EmpID=" & gvMain.DataKeys(selectedRow(gvMain))("EmpID").ToString(), _
                "EPYear=" & ViewState.Item("GradeYear"))
        ElseIf e.CommandName = "Comment" Then
            Dim objGS1 As New GS1()
            Dim strComment As String = ""
            Dim strComment_Adjust As String = ""
            Dim strComment_SignID As String = ""
            Dim strComment_SignCompID As String = ""
            Dim strComment1 As String = ""
            Dim strComment_Adjust1 As String = ""
            Dim strComment_SignID1 As String = ""
            Dim strComment_SignCompID1 As String = ""
            Using dt As DataTable = objGS1.GetEmpCommentByQuery(gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), ViewState.Item("ApplyID"), ViewState.Item("ApplyTime"), ViewState.Item("Seq"), ViewState.Item("GradeYear"), ViewState.Item("GradeSeq"), ViewState.Item("DeptEX"), gvMain.DataKeys(selectedRow(gvMain))("EmpID").ToString())
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        Select Case i
                            Case "0"
                                Select Case dt.Rows(i)("CommentType").ToString()
                                    Case "1"
                                        strComment = dt.Rows(i)("Comment").ToString()
                                    Case "2"
                                        strComment_Adjust = dt.Rows(i)("Comment").ToString()
                                End Select
                                strComment_SignID = dt.Rows(i)("SignID").ToString()
                                strComment_SignCompID = dt.Rows(i)("SignIDComp").ToString()
                            Case "1"
                                Select Case dt.Rows(i)("CommentType").ToString()
                                    Case "1"
                                        strComment1 = dt.Rows(i)("Comment").ToString()
                                    Case "2"
                                        strComment_Adjust1 = dt.Rows(i)("Comment").ToString()
                                End Select
                                strComment_SignID1 = dt.Rows(i)("SignID").ToString()
                                strComment_SignCompID1 = dt.Rows(i)("SignIDComp").ToString()
                        End Select

                    Next
                End If
            End Using
            Me.CallMiddlePage("~/GS/GS1310.aspx", New ButtonState() {btnX}, _
                    "CompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
                    "EmpID=" & gvMain.DataKeys(selectedRow(gvMain))("EmpID").ToString(), _
                    "MainFlag=" & ViewState.Item("MainFlag"), _
                    "IsSignNext=" & ViewState.Item("IsSignNext"), _
                    "Comment=" & strComment, "Comment_Adjust=" & strComment_Adjust, "Comment_SignID=" & strComment_SignID, "Comment_SignCompID=" & strComment_SignCompID, _
                    "Comment1=" & strComment1, "Comment_Adjust1=" & strComment_Adjust1, "Comment_SignID1=" & strComment_SignID1, "Comment_SignCompID1=" & strComment_SignCompID1)
        End If

    End Sub
    Private Sub TransferFPage(ByVal TxnID As String)
        Dim btnQ As New ButtonState(ButtonState.emButtonType.Query)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)

        btnX.Caption = "返回"

        If TxnID = "GS1220" Then
            btnX.Caption = "關閉"
            Me.CallMiddlePage("~/GS/GS1220.aspx", New ButtonState() {btnQ, btnX}, _
                "CompID=" & ViewState.Item("CompID"), _
                "ApplyID=" & ViewState.Item("ApplyID"), _
                "ApplyTime=" & ViewState.Item("ApplyTime"), _
                "Status=" & ViewState.Item("Status"), _
                "Seq=" & ViewState.Item("Seq"), _
                "GradeYear=" & ViewState.Item("GradeYear"), _
                "GradeSeq=" & ViewState.Item("GradeSeq"), _
                "EvaluationSeq=" & ViewState.Item("EvaluationSeq"), _
                "MainFlag=" & ViewState.Item("MainFlag"), _
                "DeptEX=" & ViewState.Item("DeptEX"), _
                "IsSignNext=" & ViewState.Item("IsSignNext"), _
                "DataCount=" & pcMain.DataTable.Rows.Count.ToString(), _
                "Result=" & ViewState.Item("Result"))

        End If
    End Sub

    

   
    Private Sub DoDownload()
        Dim objGS1 As New GS1()

        Try
            If gvMain.Rows.Count > 0 Then
                Dim IsPerformance As String = ""
                Dim dt As DataTable = pcMain.DataTable
                For i = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)("PerformanceCnt") > 0 Then
                        IsPerformance = "Y"
                        Exit For
                    End If
                Next

                '產出檔頭
                Dim strFileName As String = Bsp.Utility.GetNewFileName("GS1300年度考核(單位主管查詢)-") & ".xlsx"

                Using dtDownload = objGS1.GS1300Download(ViewState.Item("CompID"), _
                    ViewState.Item("ApplyID"), _
                        ViewState.Item("ApplyTime"), _
                        ViewState.Item("Seq"), _
                        ViewState.Item("Status"), _
                        ViewState.Item("MainFlag"), _
                        ViewState.Item("GradeYear"), _
                        ViewState.Item("GradeSeq"), _
                        ViewState.Item("EvaluationSeq"), _
                        IsPerformance, _
                        ViewState.Item("IsSignNext"))

                    Using pck As New ExcelPackage(Util.getExcelOpenXml(dtDownload, "GS1300年度考核(單位主管查詢)"))
                        Dim ws As ExcelWorksheet = pck.Workbook.Worksheets("GS1300年度考核(單位主管查詢)")
                        ws.InsertRow(1, 1)  '插入列
                        ws.Cells(1, 1, 1, 10).Merge = True   '合併儲存格
                        ws.Cells("A1").Value = "部門：" & txtDeptID.Text & "，部門總人數" & dtDownload.Rows.Count
                        'ws.Cells(1, 1, 2, 1).Merge = True   '合併儲存格
                        'ws.Cells(1, 2, 2, 2).Merge = True   '合併儲存格
                        'ws.Cells(1, 3, 2, 3).Merge = True   '合併儲存格
                        'ws.Cells(1, 4, 2, 4).Merge = True   '合併儲存格
                        'ws.Cells(1, 5, 2, 5).Merge = True   '合併儲存格
                        'ws.Cells(1, 6, 2, 6).Merge = True   '合併儲存格
                        'ws.Cells(1, 7, 1, 8).Merge = True   '合併儲存格
                        'ws.Cells(1, 9, 1, 10).Merge = True   '合併儲存格
                        'ws.Cells(1, 11, 2, 11).Merge = True   '合併儲存格
                        'ws.Cells(1, 12, 2, 12).Merge = True   '合併儲存格
                        'ws.Cells("A1:F1").Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                        'ws.Cells("A1:F1").Style.VerticalAlignment = ExcelVerticalAlignment.Center
                        'ws.Cells("K1:L1").Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                        'ws.Cells("K1:L1").Style.VerticalAlignment = ExcelVerticalAlignment.Center
                        'ws.Cells("K1:L1").Style.WrapText = True
                        'ws.Cells("G1").Value = "單位主管"
                        'ws.Cells("I1").Value = "區處主管"

                        'ws.Cells("G2:J2").Style.Font.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#FFFFFF"))
                        'ws.Cells("G2:J2").Style.Font.Bold = True
                        'ws.Cells("G2:J2").Style.Fill.PatternType = ExcelFillStyle.Solid
                        'ws.Cells("G2:J2").Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#507CD1"))
                        'ws.Cells("2:2").Style.Fill.PatternType = ExcelFillStyle.Solid
                        'ws.Cells("2:2").Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#507CD1"))
                        'ws.Cells("G2").Value = "考績"
                        'ws.Cells("H2").Value = "初排序"
                        'ws.Cells("I2").Value = "考績"
                        'ws.Cells("J2").Value = "排序"


                        'ws.Cells("A1:J1").Style.Font.Color.SetColor(System.Drawing.Color.Black)
                        'ws.Cells.Style.Border.Left.Style = ExcelBorderStyle.Thin
                        'ws.Cells.Style.Border.Right.Style = ExcelBorderStyle.Thin
                        'ws.Cells.Style.Border.Top.Style = ExcelBorderStyle.Thin
                        'ws.Cells.Style.Border.Bottom.Style = ExcelBorderStyle.Thin
                        ws.Cells.Style.ShrinkToFit = True

                        ''修改指定 Cell 樣式
                        ''ws.Cells("A1:C1").Style.Font.Color.SetColor(System.Drawing.Color.Red)
                        'ws.Cells("H1:H" & Convert.ToInt32(dt.Rows.Count.ToString) + 1).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow)
                        'ws.Cells("I1:I" & Convert.ToInt32(dt.Rows.Count.ToString) + 1).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow)
                        'ws.Cells("J1:J" & Convert.ToInt32(dt.Rows.Count.ToString) + 1).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow)

                        'Dim val = ws.DataValidations.AddListValidation(ws.Cells("H1:H" & Convert.ToInt32(dt.Rows.Count.ToString) + 1).Address)
                        ''设置下拉框显示的数据区域
                        'val.Formula.Values.Add("特優")
                        'val.Formula.Values.Add("優")
                        'val.Formula.Values.Add("甲上")
                        'val.Formula.Values.Add("甲")
                        'val.Formula.Values.Add("甲下")
                        'val.Formula.Values.Add("乙")
                        'val.Formula.Values.Add("丙")
                        'val.Error = "請輸入正確考績"
                        'val.ShowErrorMessage = True


                        '匯出
                        Util.ExportBinary(pck.GetAsByteArray(), strFileName)
                    End Using
                End Using

                ''動態產生GridView以便匯出成EXCEL
                'Dim gvExport As GridView = New GridView()
                'gvExport.AllowPaging = False
                'gvExport.AllowSorting = False
                'gvExport.FooterStyle.BackColor = Drawing.ColorTranslator.FromHtml("#99CCCC")
                'gvExport.FooterStyle.ForeColor = Drawing.ColorTranslator.FromHtml("#003399")
                'gvExport.RowStyle.CssClass = "tr_evenline"
                'gvExport.AlternatingRowStyle.CssClass = "tr_oddline"
                'gvExport.EmptyDataRowStyle.CssClass = "GridView_EmptyRowStyle"

                'gvExport.DataSource = objGS1.GS1300Download(ViewState.Item("CompID"), _
                '    ViewState.Item("ApplyID"), _
                '    ViewState.Item("ApplyTime"), _
                '    ViewState.Item("Seq"), _
                '    ViewState.Item("Status"), _
                '    ViewState.Item("MainFlag"), _
                '    ViewState.Item("GradeYear"), _
                '    ViewState.Item("GradeSeq"), _
                '    ViewState.Item("EvaluationSeq"), _
                '    IsPerformance, _
                '    ViewState.Item("IsSignNext"))
                'gvExport.DataBind()

                'Response.ClearContent()
                'Response.BufferOutput = True
                'Response.Charset = "utf-8"
                'Response.ContentType = "application/save-as"         '隱藏檔案網址路逕的下載
                'Response.AddHeader("Content-Transfer-Encoding", "binary")
                'Response.ContentEncoding = System.Text.Encoding.UTF8
                'Response.AddHeader("content-disposition", "attachment; filename=" & Server.UrlPathEncode(strFileName))

                'Dim oStringWriter As New System.IO.StringWriter()
                'Dim oHtmlTextWriter As New System.Web.UI.HtmlTextWriter(oStringWriter)
                'Response.Write("<meta http-equiv=Content-Type content=text/html charset=utf-8>")
                'oHtmlTextWriter.WriteLine("<table width='100%'>")
                'oHtmlTextWriter.WriteLine("<tr>")
                'oHtmlTextWriter.WriteLine("<td align='left'>部門：" & txtDeptID.Text & "，部門總人數" & gvExport.Rows.Count & "</td>")
                'oHtmlTextWriter.WriteLine("</tr>")
                'oHtmlTextWriter.WriteLine("</table>")
                'Dim style As String = "<style>td{font-size:9pt} a{font-size:9pt} tr{page-break-after: always}</style>"

                'gvExport.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
                'gvExport.RenderControl(oHtmlTextWriter)
                'Response.Write(style)
                'Response.Write(oStringWriter.ToString())
                'Response.End()
            Else
                Bsp.Utility.ShowFormatMessage(Me, "W_00030", "請先查詢有資料，才能下傳!")
            End If

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoDownload", ex)
        End Try
    End Sub
    Private Sub GoBack()
        Dim ti As TransferInfo = Me.StateTransfer
        'Me.TransferFramePage(ti.CallerUrl, Nothing, ti.Args)
        Me.TransferFramePage("~/GS/GS1000.aspx", Nothing, ti.Args)
    End Sub
    Protected Sub rbnGroup1_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbnGroup1.CheckedChanged
        ViewState.Item("GroupID") = "1"
        ChangeGroup()
    End Sub
    Protected Sub rbnGroup2_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbnGroup2.CheckedChanged
        ViewState.Item("GroupID") = "2"
        ChangeGroup()
    End Sub
    Protected Sub rbnGroup3_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbnGroup3.CheckedChanged
        ViewState.Item("GroupID") = "3"
        ChangeGroup()
    End Sub
    Private Sub ChangeGroup()
        Dim objGS1 As New GS1
        Using dt As DataTable = objGS1.GS1300Query(ViewState.Item("CompID"), _
                    ViewState.Item("ApplyID"), _
                    ViewState.Item("ApplyTime"), _
                    ViewState.Item("Seq"), _
                    ViewState.Item("Status"), _
                    ViewState.Item("MainFlag"), _
                    ViewState.Item("GradeYear"), _
                    ViewState.Item("GradeSeq"), _
                    ViewState.Item("EvaluationSeq"), ViewState.Item("GroupID"))
            If dt.Rows.Count > 50 Then
                pcMain.PerPageRecord = 50
            Else
                pcMain.PerPageRecord = dt.Rows.Count
            End If

            pcMain.DataTable = dt
        End Using
    End Sub
End Class
