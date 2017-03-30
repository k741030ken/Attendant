'****************************************************
'功能說明：年度考核(區處主管查詢)
'建立人員：Weicheng
'建立日期：2015.11.16
'****************************************************
Imports System
Imports System.Data
Imports System.IO
Imports SinoPac.WebExpress.Common
Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports System.Drawing

Partial Class GS_GS1400
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
            Case "btnDownload"  '下傳
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
            Using dt As DataTable = objGS.GetScoreValueParameter(ViewState.Item("CompID"), ViewState.Item("GradeYear"))
                If dt.Rows.Count > 0 Then
                    ViewState.Item("ScoreValue") = dt.Rows.Item(0)("FinalScore").ToString()
                    ViewState.Item("EvaluationSeq") = dt.Rows.Item(0)("EvaluationSeq").ToString()
                End If
            End Using

            '判斷是否全排序
            strWhere = " And CompID = " & Bsp.Utility.Quote(ViewState.Item("CompID"))
            strWhere = " And (DeptID=" & Bsp.Utility.Quote(ViewState.Item("ApplyID")) & " Or DeptID In (Select UpOrganID From GradeBase Where CompID =" & Bsp.Utility.Quote(ViewState.Item("CompID")) & " and GradeYear=" & ViewState.Item("GradeYear") & " and GradeSeq=" & ViewState.Item("GradeSeq") & " and GradeDeptID=" & Bsp.Utility.Quote(ViewState.Item("ApplyID")) & "))"
            If objHR.IsDataExists("GradeDeptException", strWhere) Then
                ViewState.Item("DeptEX") = "Y"
            Else
                ViewState.Item("DeptEX") = "N"
            End If

            Dim posGroup As Integer = 1
            For iGroup As Integer = 1 To 3
                If objGS.GS1400QueryCount(ViewState.Item("CompID"), ViewState.Item("ApplyID"), ViewState.Item("ApplyTime"), ViewState.Item("Seq"), ViewState.Item("Status"), ViewState.Item("MainFlag"), ViewState.Item("GradeYear"), ViewState.Item("GradeSeq"), ViewState.Item("EvaluationSeq"), ViewState.Item("DeptEX"), iGroup.ToString()) > 0 Then
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
                    ViewState.Item("EvaluationSeq"), _
                    ViewState.Item("DeptEX"))
            Else
                Return
            End If
        End If
    End Sub

    Private Sub subGetData(ByVal CompID As String, ByVal ApplyID As String, ByVal ApplyTime As String, ByVal Seq As String, ByVal Status As String, ByVal MainFlag As String _
                           , ByVal GradeYear As String, ByVal GradeSeq As String, ByVal EvaluationSeq As String, ByVal DeptEx As String)
        Dim objGS1 As New GS1()
        Dim objHR As New HR()

        Try
            Using dt As DataTable = objHR.GetHROrganName(CompID, ApplyID)
                If dt.Rows.Count > 0 Then
                    txtDeptID.Text = dt.Rows(0)(0).ToString()
                End If
            End Using

            Using dt As DataTable = objGS1.GS1400Query(CompID, ApplyID, ApplyTime, Seq, Status, MainFlag, GradeYear, GradeSeq, EvaluationSeq, DeptEx, ViewState.Item("GroupID"))
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
            If ViewState.Item("Status") = "4" Then
                e.Row.Cells(17).Visible = False
            End If

            e.Row.Cells(0).ColumnSpan = "2"
            e.Row.Cells(0).Text = "單位主管"
            e.Row.Cells(2).ColumnSpan = "2"
            e.Row.Cells(2).Text = "區處主管"
            e.Row.Cells(4).RowSpan = "2"
            e.Row.Cells(5).RowSpan = "2"
            e.Row.Cells(6).RowSpan = "2"
            e.Row.Cells(7).RowSpan = "2"
            e.Row.Cells(8).RowSpan = "2"
            e.Row.Cells(9).RowSpan = "2"
            e.Row.Cells(10).ColumnSpan = "3"
            e.Row.Cells(10).Text = "近三年考績"
            e.Row.Cells(13).RowSpan = "2"
            e.Row.Cells(14).RowSpan = "2"


            e.Row.Cells().RemoveAt(1)
            e.Row.Cells().RemoveAt(2)
            e.Row.Cells().RemoveAt(9)
            e.Row.Cells().RemoveAt(9)

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            'If ViewState.Item("Status") = "4" Then
            '    e.Row.Cells(17).Visible = False
            'End If
            If ViewState.Item("RowAdd") = False Then
                Dim gvRow2 As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)

                Dim tc24 As New TableCell()
                tc24.Text = "考績"
                tc24.CssClass = "td_header"
                gvRow2.Cells.Add(tc24)

                Dim tc25 As New TableCell()
                tc25.Text = "初排序"  '20160113 wei modify
                tc25.CssClass = "td_header"
                gvRow2.Cells.Add(tc25)

                Dim tc26 As New TableCell()
                tc26.Text = "考績"
                tc26.CssClass = "td_header"
                gvRow2.Cells.Add(tc26)

                Dim tc27 As New TableCell()
                tc27.Text = "排序"
                tc27.CssClass = "td_header"
                gvRow2.Cells.Add(tc27)

                Dim tc21 As New TableCell()
                tc21.Text = (CInt(ViewState.Item("GradeYear")) - 3).ToString()  '20160113 wei modify
                tc21.CssClass = "td_header"
                gvRow2.Cells.Add(tc21)

                Dim tc22 As New TableCell()
                tc22.Text = (CInt(ViewState.Item("GradeYear")) - 2).ToString()  '20160113 wei modify
                tc22.CssClass = "td_header"
                gvRow2.Cells.Add(tc22)

                Dim tc23 As New TableCell()
                tc23.Text = (CInt(ViewState.Item("GradeYear")) - 1).ToString()   '20160113 wei modify
                tc23.CssClass = "td_header"
                gvRow2.Cells.Add(tc23)

                

                'Dim tc28 As New TableCell()
                'tc28.Text = "考績檢視建議"
                'tc28.CssClass = "td_header"
                'gvRow2.Cells.Add(tc28)

                'Dim tc29 As New TableCell()
                'tc29.Text = "會議初核考績"
                'tc29.CssClass = "td_header"
                'gvRow2.Cells.Add(tc29)

                'If ViewState.Item("Status") = "5" Then
                '    Dim tc30 As New TableCell()
                '    tc30.Text = "核定考績"
                '    tc30.CssClass = "td_header"
                '    gvRow2.Cells.Add(tc30)
                'End If


                gvMain.Controls(0).Controls.AddAt(1, gvRow2)
                ViewState.Item("RowAdd") = True
            End If
        End If
    End Sub

    Protected Sub gvMain_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvMain.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim tmpBtn As New ImageButton
            '奬懲
            tmpBtn = e.Row.Cells(13).FindControl("ibtnReward")
            If DirectCast(gvMain.DataSource, DataTable).Rows(e.Row.RowIndex)("RewardCnt").ToString() > 0 Then
                tmpBtn.Visible = True
            End If
            '業績
            tmpBtn = e.Row.Cells(13).FindControl("ibtnPerformance")
            If DirectCast(gvMain.DataSource, DataTable).Rows(e.Row.RowIndex)("PerformanceCnt").ToString() > 0 Then
                tmpBtn.Visible = True
            End If
            '考核補充說明
            tmpBtn = e.Row.Cells(13).FindControl("ibtnComment")
            If DirectCast(gvMain.DataSource, DataTable).Rows(e.Row.RowIndex)("CommentCnt").ToString() > 0 Then
                tmpBtn.Visible = True
            End If

            Select Case e.Row.Cells(0).Text
                Case "1"
                    e.Row.Cells(0).Text = "優等"
                Case "2"
                    e.Row.Cells(0).Text = "甲等"
                Case "3"
                    e.Row.Cells(0).Text = "乙等"
                Case "4"
                    e.Row.Cells(0).Text = "丙等"
                Case "6"
                    e.Row.Cells(0).Text = "甲上"
                Case "7"
                    e.Row.Cells(0).Text = "甲下"
                Case "9"
                    e.Row.Cells(0).Text = "特優"
                Case Else
                    e.Row.Cells(0).Text = ""
            End Select

            Select Case e.Row.Cells(2).Text
                Case "1"
                    e.Row.Cells(2).Text = "優等"
                Case "2"
                    e.Row.Cells(2).Text = "甲等"
                Case "3"
                    e.Row.Cells(2).Text = "乙等"
                Case "4"
                    e.Row.Cells(2).Text = "丙等"
                Case "6"
                    e.Row.Cells(2).Text = "甲上"
                Case "7"
                    e.Row.Cells(2).Text = "甲下"
                Case "9"
                    e.Row.Cells(2).Text = "特優"
                Case Else
                    e.Row.Cells(2).Text = ""
            End Select

            For i = 4 To 4
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

            For i = 10 To 12
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
            'If ViewState.Item("Status") = "4" Then
            '    Dim GradeAdjust As String = DataBinder.Eval(e.Row.DataItem, "GradeAdjust")
            '    Dim GradeHR As String = DataBinder.Eval(e.Row.DataItem, "GradeHR")

            '    If GradeHR <> "" And GradeAdjust <> "" And GradeHR <> GradeAdjust Then
            '        e.Row.Cells(15).ForeColor = System.Drawing.Color.Red
            '        e.Row.Cells(16).ForeColor = System.Drawing.Color.Red
            '    End If
            'End If
            'If ViewState.Item("Status") = "5" Then
            '    Dim GradeHR As String = DataBinder.Eval(e.Row.DataItem, "GradeHR")
            '    Dim Grade2 As String = DataBinder.Eval(e.Row.DataItem, "Grade2")
            '    If GradeHR <> "" And Grade2 <> "" And GradeHR <> Grade2 Then
            '        e.Row.Cells(4).ForeColor = System.Drawing.Color.Red
            '        e.Row.Cells(5).ForeColor = System.Drawing.Color.Red
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
                ViewState.Item("IsPerformance") = ""
                Dim dt As DataTable = pcMain.DataTable
                For i = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)("PerformanceCnt") > 0 Then
                        ViewState.Item("IsPerformance") = "Y"
                        Exit For
                    End If
                Next

                '產出檔頭
                Dim strFileName As String = Bsp.Utility.GetNewFileName("GS1400年度考核(區處主管查詢)-") & ".xlsx"
                Using dtDownload = objGS1.GS1400Download(ViewState.Item("CompID"), _
                    ViewState.Item("ApplyID"), _
                    ViewState.Item("ApplyTime"), _
                    ViewState.Item("Seq"), _
                    ViewState.Item("Status"), _
                    ViewState.Item("MainFlag"), _
                    ViewState.Item("GradeYear"), _
                    ViewState.Item("GradeSeq"), _
                    ViewState.Item("EvaluationSeq"), _
                    ViewState.Item("IsPerformance"), _
                    ViewState.Item("IsSignNext"), _
                    ViewState.Item("DeptEX"))

                    Using pck As New ExcelPackage(Util.getExcelOpenXml(dtDownload, "GS1400年度考核(區處主管查詢)"))
                        Dim ws As ExcelWorksheet = pck.Workbook.Worksheets("GS1400年度考核(區處主管查詢)")
                        ws.InsertRow(1, 1)  '插入列
                        ws.Cells(1, 1, 1, 10).Merge = True   '合併儲存格
                        ws.Cells("A1").Value = "部門：" & txtDeptID.Text & "，部門總人數" & dtDownload.Rows.Count
                        ws.InsertRow(2, 1)  '插入列
                        ws.Cells(2, 1, 3, 1).Merge = True   '合併儲存格
                        ws.Cells(2, 2, 3, 2).Merge = True   '合併儲存格
                        ws.Cells(2, 3, 3, 3).Merge = True   '合併儲存格
                        ws.Cells(2, 4, 3, 4).Merge = True   '合併儲存格
                        ws.Cells(2, 5, 3, 5).Merge = True   '合併儲存格
                        ws.Cells(2, 6, 3, 6).Merge = True   '合併儲存格
                        'ws.Cells(2, 7, 3, 7).Merge = True   '合併儲存格
                        ws.Cells(2, 7, 2, 8).Merge = True   '合併儲存格
                        ws.Cells(2, 9, 2, 10).Merge = True   '合併儲存格
                        ws.Cells(2, 11, 3, 11).Merge = True   '合併儲存格
                        ws.Cells(2, 12, 3, 12).Merge = True   '合併儲存格
                        ws.Cells(2, 13, 3, 13).Merge = True   '合併儲存格
                        ws.Cells("A2:F2").Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                        ws.Cells("A2:F2").Style.VerticalAlignment = ExcelVerticalAlignment.Center
                        ws.Cells("K2:M2").Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                        ws.Cells("K2:M2").Style.VerticalAlignment = ExcelVerticalAlignment.Center
                        ws.Cells("K2:M2").Style.WrapText = True
                        ws.Cells("A2").Value = "單位"
                        ws.Cells("B2").Value = "員編"
                        ws.Cells("C2").Value = "姓名"
                        ws.Cells("D2").Value = "到職日"
                        ws.Cells("E2").Value = "職位"
                        ws.Cells("F2").Value = "職等"
                        'ws.Cells("G2").Value = "年度整體評量結果"
                        ws.Cells("G2").Value = "單位主管"
                        ws.Cells("I2").Value = "區處主管"

                        ws.Cells("G3:J3").Style.Font.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#FFFFFF"))
                        ws.Cells("G3:J3").Style.Font.Bold = True
                        ws.Cells("G3:J3").Style.Fill.PatternType = ExcelFillStyle.Solid
                        ws.Cells("G3:J3").Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#507CD1"))
                        ws.Cells("2:2").Style.Font.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#FFFFFF"))
                        ws.Cells("2:2").Style.Fill.PatternType = ExcelFillStyle.Solid
                        ws.Cells("2:2").Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#507CD1"))
                        ws.Cells("3:3").Style.Fill.PatternType = ExcelFillStyle.Solid
                        ws.Cells("3:3").Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#507CD1"))
                        ws.Cells("G3").Value = "考績"
                        ws.Cells("H3").Value = "初排序"
                        ws.Cells("I3").Value = "考績"
                        ws.Cells("J3").Value = "排序"

                        ws.Cells("K2").Value = "核定考績"
                        ws.Cells("L2").Value = "單位考績補充說明"
                        ws.Cells("M2").Value = "區處考績補充說明"


                        If ViewState.Item("IsPerformance") = "Y" Then
                            For i = 14 To 50
                                ws.Cells(2, i, 3, i).Merge = True   '合併儲存格
                            Next
                            ws.Cells("N2:AW2").Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                            ws.Cells("N2:AW2").Style.VerticalAlignment = ExcelVerticalAlignment.Center
                            ws.Cells("N2:AW2").Style.WrapText = True

                            ws.Cells("N2").Value = "Q1\個金業務\房貸撥款量(仟元)"
                            ws.Cells("O2").Value = "Q1\個金業務\信貸撥款量(仟元)"
                            ws.Cells("P2").Value = "Q1\個金業務\總AP(仟點)"
                            ws.Cells("Q2").Value = "Q1\個金業務\個金Score Card"
                            ws.Cells("R2").Value = "Q1\個金業務\理財Score Card"
                            ws.Cells("S2").Value = "Q1\理財業務\總AP(仟點)"
                            ws.Cells("T2").Value = "Q1\理財業務\Score Card"
                            ws.Cells("U2").Value = "Q1\法金業務(單位)\總AP(仟點)"
                            ws.Cells("V2").Value = "Q1\法金業務(單位)\單位職系達成率"

                            ws.Cells("W2").Value = "Q2\個金業務\房貸撥款量(仟元)"
                            ws.Cells("X2").Value = "Q2\個金業務\信貸撥款量(仟元)"
                            ws.Cells("Y2").Value = "Q2\個金業務\總AP(仟點)"
                            ws.Cells("Z2").Value = "Q2\個金業務\個金Score Card"
                            ws.Cells("AA2").Value = "Q2\個金業務\理財Score Card"
                            ws.Cells("AB2").Value = "Q2\理財業務\總AP(仟點)"
                            ws.Cells("AC2").Value = "Q2\理財業務\Score Card"
                            ws.Cells("AD2").Value = "Q2\法金業務(單位)\總AP(仟點)"
                            ws.Cells("AE2").Value = "Q2\法金業務(單位)\單位職系達成率"

                            ws.Cells("AF2").Value = "Q3\個金業務\房貸撥款量(仟元)"
                            ws.Cells("AG2").Value = "Q3\個金業務\信貸撥款量(仟元)"
                            ws.Cells("AH2").Value = "Q3\個金業務\總AP(仟點)"
                            ws.Cells("AI2").Value = "Q3\個金業務\個金Score Card"
                            ws.Cells("AJ2").Value = "Q3\個金業務\理財Score Card"
                            ws.Cells("AK2").Value = "Q3\理財業務\總AP(仟點)"
                            ws.Cells("AL2").Value = "Q3\理財業務\Score Card"
                            ws.Cells("AM2").Value = "Q3\法金業務(單位)\總AP(仟點)"
                            ws.Cells("AN2").Value = "Q3\法金業務(單位)\單位職系達成率"

                            ws.Cells("AO2").Value = ViewState.Item("GradeYear") & "年度平均\個金業務\房貸撥款量(仟元)"
                            ws.Cells("AP2").Value = ViewState.Item("GradeYear") & "年度平均\個金業務\信貸撥款量(仟元)"
                            ws.Cells("AQ2").Value = ViewState.Item("GradeYear") & "年度平均\個金業務\總AP(仟點)"
                            ws.Cells("AR2").Value = ViewState.Item("GradeYear") & "年度平均\個金業務\個金Score Card"
                            ws.Cells("AS2").Value = ViewState.Item("GradeYear") & "年度平均\個金業務\理財Score Card"
                            ws.Cells("AT2").Value = ViewState.Item("GradeYear") & "年度平均\理財業務\總AP(仟點)"
                            ws.Cells("AU2").Value = ViewState.Item("GradeYear") & "年度平均\理財業務\Score Card"
                            ws.Cells("AV2").Value = ViewState.Item("GradeYear") & "年度平均\法金業務(單位)\總AP(仟點)"
                            ws.Cells("AW2").Value = ViewState.Item("GradeYear") & "年度平均\法金業務(單位)\單位職系達成率"

                        End If


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
                'gvExport.FooterStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#99CCCC")
                'gvExport.FooterStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#003399")
                'gvExport.RowStyle.CssClass = "tr_evenline"
                'gvExport.AlternatingRowStyle.CssClass = "tr_oddline"
                'gvExport.EmptyDataRowStyle.CssClass = "GridView_EmptyRowStyle"

                'gvExport.DataSource = objGS1.GS1400Download(ViewState.Item("CompID"), _
                '    ViewState.Item("ApplyID"), _
                '    ViewState.Item("ApplyTime"), _
                '    ViewState.Item("Seq"), _
                '    ViewState.Item("Status"), _
                '    ViewState.Item("MainFlag"), _
                '    ViewState.Item("GradeYear"), _
                '    ViewState.Item("GradeSeq"), _
                '    ViewState.Item("EvaluationSeq"), _
                '    ViewState.Item("IsPerformance"), _
                '    ViewState.Item("IsSignNext"), _
                '    ViewState.Item("DeptEX"))
                'gvExport.DataBind()
                'GroupRows(gvExport)

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

    Public Sub GroupRows(ByVal GridViewData As GridView)
        GridViewData.HeaderRow.Cells(0).RowSpan = "2"
        GridViewData.HeaderRow.Cells(1).RowSpan = "2"
        GridViewData.HeaderRow.Cells(2).RowSpan = "2"
        GridViewData.HeaderRow.Cells(3).RowSpan = "2"
        GridViewData.HeaderRow.Cells(4).RowSpan = "2"
        GridViewData.HeaderRow.Cells(5).RowSpan = "2"
        GridViewData.HeaderRow.Cells(6).RowSpan = "2"

        GridViewData.HeaderRow.Cells(7).ColumnSpan = "2"
        GridViewData.HeaderRow.Cells(7).Text = "單位主管"
        GridViewData.HeaderRow.Cells().RemoveAt(8)

        If ViewState.Item("Status") = "4" Then
            GridViewData.HeaderRow.Cells(8).ColumnSpan = "4"
            GridViewData.HeaderRow.Cells(8).Text = "區處主管"
            GridViewData.HeaderRow.Cells().RemoveAt(9)
            GridViewData.HeaderRow.Cells().RemoveAt(10)
            GridViewData.HeaderRow.Cells().RemoveAt(11)

        Else
            GridViewData.HeaderRow.Cells(8).ColumnSpan = "2"
            GridViewData.HeaderRow.Cells(8).Text = "區處主管"
            GridViewData.HeaderRow.Cells().RemoveAt(9)
            'GridViewData.HeaderRow.Cells().RemoveAt(10)
            'GridViewData.HeaderRow.Cells().RemoveAt(11)
            'GridViewData.HeaderRow.Cells().RemoveAt(12)

        End If

        GridViewData.HeaderRow.Cells(9).RowSpan = "2"
        'GridViewData.HeaderRow.Cells(9).ColumnSpan = "1"
        'GridViewData.HeaderRow.Cells(9).Text = "單位主管"
        'GridViewData.HeaderRow.Cells().RemoveAt(10)

        GridViewData.HeaderRow.Cells(10).RowSpan = "2"
        'GridViewData.HeaderRow.Cells(10).ColumnSpan = "1"
        'GridViewData.HeaderRow.Cells(10).Text = "區處主管"
        'GridViewData.HeaderRow.Cells().RemoveAt(11)

        GridViewData.HeaderRow.Cells(11).RowSpan = "2"

        If ViewState.Item("IsPerformance") = "Y" Then
            For i = 12 To 46
                GridViewData.HeaderRow.Cells(i).RowSpan = "2"
            Next
        End If

        Dim HeaderGridRow As GridViewRow = New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)
        Dim HeaderCell_1 As New TableCell()
        HeaderCell_1.Text = "初排序"
        HeaderCell_1.HorizontalAlign = HorizontalAlign.Center
        HeaderCell_1.Font.Bold = True
        HeaderGridRow.Cells.Add(HeaderCell_1)

        Dim HeaderCell_2 As New TableCell()
        HeaderCell_2.Text = "考績"
        HeaderCell_2.HorizontalAlign = HorizontalAlign.Center
        HeaderCell_2.Font.Bold = True
        HeaderGridRow.Cells.Add(HeaderCell_2)

        Dim HeaderCell_3 As New TableCell()
        HeaderCell_3.Text = "排序"
        HeaderCell_3.HorizontalAlign = HorizontalAlign.Center
        HeaderCell_3.Font.Bold = True
        HeaderGridRow.Cells.Add(HeaderCell_3)

        Dim HeaderCell_4 As New TableCell()
        HeaderCell_4.Text = "考績"
        HeaderCell_4.HorizontalAlign = HorizontalAlign.Center
        HeaderCell_4.Font.Bold = True
        HeaderGridRow.Cells.Add(HeaderCell_4)

        'Dim HeaderCell_5 As New TableCell()
        'HeaderCell_5.Text = "考績檢視建議"
        'HeaderCell_5.HorizontalAlign = HorizontalAlign.Center
        'HeaderCell_5.Font.Bold = True
        'HeaderGridRow.Cells.Add(HeaderCell_5)

        If ViewState.Item("Status") = "4" Then
            Dim HeaderCell_6 As New TableCell()
            HeaderCell_6.Text = "會議初核考績"
            HeaderCell_6.HorizontalAlign = HorizontalAlign.Center
            HeaderCell_6.Font.Bold = True
            HeaderGridRow.Cells.Add(HeaderCell_6)

            Dim HeaderCell_7 As New TableCell()
            HeaderCell_7.Text = "整體評量調整說明"
            HeaderCell_7.HorizontalAlign = HorizontalAlign.Center
            HeaderCell_7.Font.Bold = True
            HeaderGridRow.Cells.Add(HeaderCell_7)

            Dim HeaderCell_8 As New TableCell()
            HeaderCell_8.Text = "考核補充說明"
            HeaderCell_8.HorizontalAlign = HorizontalAlign.Center
            HeaderCell_8.Font.Bold = True
            HeaderGridRow.Cells.Add(HeaderCell_8)

            Dim HeaderCell_9 As New TableCell()
            HeaderCell_9.Text = "整體評量調整說明"
            HeaderCell_9.HorizontalAlign = HorizontalAlign.Center
            HeaderCell_9.Font.Bold = True
            HeaderGridRow.Cells.Add(HeaderCell_9)

            Dim HeaderCell_10 As New TableCell()
            HeaderCell_10.Text = "考績檢視補充說明"
            HeaderCell_10.HorizontalAlign = HorizontalAlign.Center
            HeaderCell_10.Font.Bold = True
            HeaderGridRow.Cells.Add(HeaderCell_10)
        Else
            'Dim HeaderCell_6 As New TableCell()
            'HeaderCell_6.Text = "會議初核考績"
            'HeaderCell_6.HorizontalAlign = HorizontalAlign.Center
            'HeaderCell_6.Font.Bold = True
            'HeaderGridRow.Cells.Add(HeaderCell_6)

            'Dim HeaderCell_7 As New TableCell()
            'HeaderCell_7.Text = "核定考績"
            'HeaderCell_7.HorizontalAlign = HorizontalAlign.Center
            'HeaderCell_7.Font.Bold = True
            'HeaderGridRow.Cells.Add(HeaderCell_7)

            'Dim HeaderCell_8 As New TableCell()
            'HeaderCell_8.Text = "整體評量調整說明"
            'HeaderCell_8.HorizontalAlign = HorizontalAlign.Center
            'HeaderCell_8.Font.Bold = True
            'HeaderGridRow.Cells.Add(HeaderCell_8)

            'Dim HeaderCell_9 As New TableCell()
            'HeaderCell_9.Text = "考核補充說明"
            'HeaderCell_9.HorizontalAlign = HorizontalAlign.Center
            'HeaderCell_9.Font.Bold = True
            'HeaderGridRow.Cells.Add(HeaderCell_9)

            'Dim HeaderCell_10 As New TableCell()
            'HeaderCell_10.Text = "整體評量調整說明"
            'HeaderCell_10.HorizontalAlign = HorizontalAlign.Center
            'HeaderCell_10.Font.Bold = True
            'HeaderGridRow.Cells.Add(HeaderCell_10)

            'Dim HeaderCell_11 As New TableCell()
            'HeaderCell_11.Text = "考績檢視補充說明"
            'HeaderCell_11.HorizontalAlign = HorizontalAlign.Center
            'HeaderCell_11.Font.Bold = True
            'HeaderGridRow.Cells.Add(HeaderCell_11)
        End If


        GridViewData.Controls(0).Controls.AddAt(1, HeaderGridRow)
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
        Using dt As DataTable = objGS1.GS1400Query(ViewState.Item("CompID"), _
                    ViewState.Item("ApplyID"), _
                    ViewState.Item("ApplyTime"), _
                    ViewState.Item("Seq"), _
                    ViewState.Item("Status"), _
                    ViewState.Item("MainFlag"), _
                    ViewState.Item("GradeYear"), _
                    ViewState.Item("GradeSeq"), _
                    ViewState.Item("EvaluationSeq"), _
                    ViewState.Item("DeptEX"), ViewState.Item("GroupID"))
            If dt.Rows.Count > 50 Then
                pcMain.PerPageRecord = 50
            Else
                pcMain.PerPageRecord = dt.Rows.Count
            End If

            pcMain.DataTable = dt
        End Using
    End Sub
End Class
