'****************************************************
'功能說明：年度考核(區處主管排序)
'建立人員：BeatriceCheng
'建立日期：2015.10.30
'****************************************************
Imports System.Data
Imports SinoPac.WebExpress.Common   '20160513 wei add NetAP dll匯出xlsx使用
Imports OfficeOpenXml   '20160513 wei add
Imports OfficeOpenXml.Style '20160513 wei add

Partial Class GS_GS1200
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ViewState.Item("RowAdd") = False
        If Not IsPostBack Then
            ViewState.Item("Upload") = "N"
        Else
            If ViewState.Item("Upload") = "Y" Then
                ViewState.Item("Upload") = "N"
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
            End If
        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Dim strWhere As String = ""
        Dim objHR As New HR()
        Dim objGS As New GS1()
        Select Case Param
            Case "btnUpdate"    '暫存
                'If funCheckData(False) Then    20160607 wei del 考績為下拉式選單,無需Check Data
                SaveData()
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
                'End If
                'Case "btnAdd"  '配置考績
                '    If funCheckData(True) Then
                '        SaveData()
                '        strWhere = " And G.CompID = " & Bsp.Utility.Quote(ViewState.Item("CompID"))
                '        strWhere = strWhere & " And G.GradeYear = " & Bsp.Utility.Quote(ViewState.Item("GradeYear"))
                '        strWhere = strWhere & " And G.GradeSeq = " & Bsp.Utility.Quote(ViewState.Item("GradeSeq"))
                '        strWhere = strWhere & " And GS.ApplyTime = " & Bsp.Utility.Quote(ViewState.Item("ApplyTime"))
                '        If ViewState.Item("MainFlag") = "2" And ViewState.Item("DeptEX") = "N" Then
                '            strWhere = strWhere & " And G.UpOrganID =" & Bsp.Utility.Quote(ViewState.Item("ApplyID"))
                '        Else
                '            strWhere = strWhere & " And G.GradeDeptID = " & Bsp.Utility.Quote(ViewState.Item("ApplyID"))
                '        End If
                '        strWhere = strWhere & " And G.GradeDeptID = G.OrderDeptID "
                '        strWhere = strWhere & " And GS.GradeOrder=0 "
                '        If objHR.IsDataExists("GradeBase G left join GradeSignLog GS on G.CompID=GS.CompID and G.GradeDeptID=GS.ApplyID and G.EmpID=GS.EmpID and GS.Seq=" & ViewState.Item("Seq") & " And G.Online='1'", strWhere) Then
                '            Bsp.Utility.ShowMessage(Me, "排序尚未完成！")
                '            Return
                '        End If
                '        GradeData()
                '        subGetData(
                '            ViewState.Item("CompID"), _
                '            ViewState.Item("ApplyID"), _
                '            ViewState.Item("ApplyTime"), _
                '            ViewState.Item("Seq"), _
                '            ViewState.Item("Status"), _
                '            ViewState.Item("MainFlag"), _
                '            ViewState.Item("GradeYear"), _
                '            ViewState.Item("GradeSeq"), _
                '            ViewState.Item("EvaluationSeq"), _
                '            ViewState.Item("DeptEX"))
                '    End If
            Case "btnExecutes"   '整體評量調整說明
                If funCheckData(False) Then
                    SaveData()
                    'If Not objGS.IsEmpComment_ScoreAdjust(ViewState.Item("CompID"), ViewState.Item("ApplyID"), ViewState.Item("ApplyTime"), ViewState.Item("Seq"), ViewState.Item("GradeYear"), ViewState.Item("GradeSeq"), ViewState.Item("EvaluationSeq"), ViewState.Item("MainFlag"), ViewState.Item("DeptEX")) Then
                    '    Bsp.Utility.ShowMessage(Me, "無整體評量調整人員！")
                    '    Return
                    'End If
                    'TransferFPage("GS1150")
                    TransferFPage("GS1180")
                End If
            Case "btnOK"     '考績檢視建議說明
                If hidGroupID.Value = "0" Then
                    TransferFPage("GS1210")
                Else
                    If funCheckData(False) Then
                        If SaveData() Then
                            'strWhere = " And G.CompID = " & Bsp.Utility.Quote(ViewState.Item("CompID"))
                            'strWhere = strWhere & " And G.GradeYear = " & Bsp.Utility.Quote(ViewState.Item("GradeYear"))
                            'strWhere = strWhere & " And G.GradeSeq = " & Bsp.Utility.Quote(ViewState.Item("GradeSeq"))
                            'strWhere = strWhere & " And GS.ApplyTime = " & Bsp.Utility.Quote(ViewState.Item("ApplyTime"))
                            'If ViewState.Item("MainFlag") = "2" And ViewState.Item("DeptEx") = "N" Then
                            '    strWhere = strWhere & " And G.UpOrganID =" & Bsp.Utility.Quote(ViewState.Item("ApplyID"))
                            'Else
                            '    strWhere = strWhere & " And G.GradeDeptID = " & Bsp.Utility.Quote(ViewState.Item("ApplyID"))
                            'End If
                            'strWhere = strWhere & " And G.GradeDeptID = G.OrderDeptID "
                            'strWhere = strWhere & " And GS.GradeOrder=0 "
                            'If objHR.IsDataExists("GradeBase G left join GradeSignLog GS on G.CompID=GS.CompID and G.GradeDeptID=GS.ApplyID and G.EmpID=GS.EmpID and GS.Seq=" & ViewState.Item("Seq") & " And G.Online='1'", strWhere) Then
                            '    Bsp.Utility.ShowMessage(Me, "排序尚未完成！")
                            '    Return
                            'End If
                            'If Not objGS.IsEmpComment(ViewState.Item("CompID"), ViewState.Item("ApplyID"), ViewState.Item("ApplyTime"), ViewState.Item("Seq"), ViewState.Item("GradeYear"), ViewState.Item("GradeSeq"), ViewState.Item("EvaluationSeq"), ViewState.Item("MainFlag"), ViewState.Item("DeptEX")) Then
                            '    Bsp.Utility.ShowMessage(Me, "配置考績尚未完成！")
                            '    Return
                            'End If
                            TransferFPage("GS1210")
                        End If
                    End If
                End If
                
            Case "btnDownload"  '下傳
                DoDownload()
            Case "btnActionC"   '送出
                If funCheckData(True) Then
                    SaveData()
                    strWhere = " And G.CompID = " & Bsp.Utility.Quote(ViewState.Item("CompID"))
                    strWhere = strWhere & " And G.GradeYear = " & Bsp.Utility.Quote(ViewState.Item("GradeYear"))
                    strWhere = strWhere & " And G.GradeSeq = " & Bsp.Utility.Quote(ViewState.Item("GradeSeq"))
                    strWhere = strWhere & " And GS.ApplyTime = " & Bsp.Utility.Quote(ViewState.Item("ApplyTime"))
                    If ViewState.Item("MainFlag") = "2" And ViewState.Item("DeptEX") = "N" Then
                        strWhere = strWhere & " And G.UpOrganID =" & Bsp.Utility.Quote(ViewState.Item("ApplyID"))
                    Else
                        strWhere = strWhere & " And G.GradeDeptID = " & Bsp.Utility.Quote(ViewState.Item("ApplyID"))
                    End If
                    strWhere = strWhere & " And G.GradeDeptID = G.OrderDeptID "
                    strWhere = strWhere & " And GS.GradeOrder=0 "
                    If objHR.IsDataExists("GradeBase G left join GradeSignLog GS on G.CompID=GS.CompID and G.GradeDeptID=GS.ApplyID and G.EmpID=GS.EmpID and GS.Seq=" & ViewState.Item("Seq") & " And G.Online='1'", strWhere) Then
                        Bsp.Utility.ShowMessage(Me, "排序尚未完成！")
                        Return
                    End If
                    ''檢查是否已配置考績
                    'If objGS.CheckGrade(ViewState.Item("CompID"), ViewState.Item("ApplyID"), ViewState.Item("ApplyTime"), ViewState.Item("Seq"), ViewState.Item("GradeYear"), ViewState.Item("GradeSeq"), ViewState.Item("MainFlag"), ViewState.Item("DeptEX")) Then
                    '    Bsp.Utility.ShowMessage(Me, "考績配置尚未完成！")
                    '    Return
                    'End If
                    '檢查整體評量是否已填寫完成
                    'If objGS.CheckEmpComment_ScoreAdjust(ViewState.Item("CompID"), ViewState.Item("ApplyID"), ViewState.Item("ApplyTime"), ViewState.Item("Seq"), ViewState.Item("GradeYear"), ViewState.Item("GradeSeq"), ViewState.Item("EvaluationSeq"), ViewState.Item("MainFlag"), ViewState.Item("DeptEX")) Then
                    '    Bsp.Utility.ShowMessage(Me, "整體評量調整說明尚未完成！")
                    '    Return
                    'End If
                    Dim topDataCount As String = "0"
                    Dim LastDataCount As String = "0"
                    topDataCount = CInt(CSng(pcMain.DataTable.Rows.Count.ToString()) * 0.2).ToString()
                    LastDataCount = CInt(CSng(pcMain.DataTable.Rows.Count.ToString()) * 0.15).ToString()
                    '檢查考核補充說明是填寫完成
                    If objGS.CheckEmpComment(ViewState.Item("CompID"), ViewState.Item("ApplyID"), ViewState.Item("ApplyTime"), ViewState.Item("Seq"), ViewState.Item("GradeYear"), ViewState.Item("GradeSeq"), ViewState.Item("MainFlag"), ViewState.Item("DeptEX"), topDataCount, LastDataCount, ViewState.Item("IsSignNext")) Then
                        Bsp.Utility.ShowMessage(Me, "考績補充說明尚未完成！")
                        Return
                    End If
                    SendData()
                    GoBack()
                End If
            Case "btnCopy"      '統計表
                TransferFPage("GS1220")
            Case "btnUpload"    '排序上傳(專用格式)
                DoUpload()
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
                If objGS.GS1200QueryCount(ViewState.Item("CompID"), ViewState.Item("ApplyID"), ViewState.Item("ApplyTime"), ViewState.Item("Seq"), ViewState.Item("Status"), ViewState.Item("MainFlag"), ViewState.Item("GradeYear"), ViewState.Item("GradeSeq"), ViewState.Item("EvaluationSeq"), ViewState.Item("DeptEX"), iGroup.ToString()) > 0 Then
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
                    hidGroupID.Value = rbnGroup1.ClientID
                    'Util.setJSContent("var chkID = '" + rbnGroup1.ClientID + "';", "Radio_Init")
                Case "2"
                    rbnGroup2.Checked = True
                    hidGroupID.Value = rbnGroup2.ClientID
                    'Util.setJSContent("var chkID = '" + rbnGroup2.ClientID + "';", "Radio_Init")
                Case "3"
                    rbnGroup3.Checked = True
                    hidGroupID.Value = rbnGroup3.ClientID
                    'Util.setJSContent("var chkID = '" + rbnGroup3.ClientID + "';", "Radio_Init")
            End Select

            If ViewState.Item("Result") = "1" Or ViewState.Item("Status") = "2" Then
                hidGroupID.Value = "0"
            Else
                If Not objGS.CheckSignLog(ViewState.Item("CompID"), ViewState.Item("ApplyID"), _
                                              Format(CDate(ViewState.Item("ApplyTime")), "yyyy/MM/dd HH:mm:ss"), ViewState.Item("Seq"), _
                                              ViewState.Item("GradeYear"), ViewState.Item("GradeSeq"), ViewState.Item("DeptEX")) Then
                    Dim strJS As String = "var chkID=document.getElementById('hidGroupID').value;if (chkID!=this.id && confirm('是否暫存考績?')){this.checked=true;document.getElementById('hidGroupID').value=this.id;}else{document.getElementById(chkID).checked=true;}"
                    rbnGroup1.Attributes.Add("onclick", strJS)
                    rbnGroup2.Attributes.Add("onclick", strJS)
                    rbnGroup3.Attributes.Add("onclick", strJS)
                Else
                    hidGroupID.Value = "0"
                End If

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

            Using dt As DataTable = objGS1.GS1200Query(CompID, ApplyID, ApplyTime, Seq, Status, MainFlag, GradeYear, GradeSeq, EvaluationSeq, DeptEx, ViewState.Item("GroupID"))
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
    '送出
    Private Function SendData() As Boolean
        Dim objGS1 As New GS1()
        '儲存資料
        Try
            Dim topDataCount As String = "0"
            Dim LastDataCount As String = "0"
            topDataCount = CInt(CSng(pcMain.DataTable.Rows.Count.ToString()) * 0.2).ToString()
            LastDataCount = CInt(CSng(pcMain.DataTable.Rows.Count.ToString()) * 0.15).ToString()

            Return objGS1.GS1200SendData(ViewState.Item("CompID"), ViewState.Item("ApplyID"), ViewState.Item("ApplyTime"), ViewState.Item("Seq"), ViewState.Item("GradeYear"), ViewState.Item("GradeSeq"), _
                                         ViewState.Item("EvaluationSeq"), ViewState.Item("MainFlag"), ViewState.Item("DeptEX"), topDataCount, LastDataCount, ViewState.Item("IsSignNext"))
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, "[CC0201_99]" & Bsp.Utility.getMessage("E_00000") & Bsp.Utility.getInnerException(Me.FunID, ex))
            Return False
        End Try
    End Function
    Protected Sub gvMain_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvMain.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(0).RowSpan = "2"
            e.Row.Cells(1).RowSpan = "2"
            e.Row.Cells(2).ColumnSpan = "2"
            e.Row.Cells(2).Text = "單位主管初核"
            e.Row.Cells(4).RowSpan = "2"
            e.Row.Cells(5).RowSpan = "2"
            e.Row.Cells(6).RowSpan = "2"
            e.Row.Cells(7).RowSpan = "2"
            e.Row.Cells(8).RowSpan = "2"
            e.Row.Cells(9).ColumnSpan = "3"
            e.Row.Cells(9).Text = "近三年考績"
            e.Row.Cells(12).RowSpan = "2"
            e.Row.Cells(13).RowSpan = "2"


            e.Row.Cells().RemoveAt(3)
            e.Row.Cells().RemoveAt(9)
            e.Row.Cells().RemoveAt(9)
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            If ViewState.Item("RowAdd") = False Then
                Dim gvRow2 As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)

                Dim tc24 As New TableCell()
                tc24.Text = "考績"
                tc24.CssClass = "td_header"
                gvRow2.Cells.Add(tc24)

                Dim tc25 As New TableCell()
                tc25.Text = "排序"  '20160113 wei modify
                tc25.CssClass = "td_header"
                gvRow2.Cells.Add(tc25)

                Dim tc21 As New TableCell()
                tc21.Text = (CInt(ViewState.Item("GradeYear")) - 3).ToString()  '20160113 wei modify
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
            'Dim ddlScoreValue As DropDownList = CType(e.Row.FindControl("ddlScoreValue"), DropDownList)
            'Dim strDefaultValue() As String = ViewState.Item("ScoreValue").Split(";")
            'Dim strScore As String = ""
            'Dim strScoreValue As String = ""
            'ddlScoreValue.Items.Add(New ListItem("請選擇", 0))
            'For intLoop As Integer = 0 To strDefaultValue.GetUpperBound(0)
            '    strScore = strDefaultValue(intLoop).Split("=")(0)
            '    strScoreValue = strDefaultValue(intLoop).Split("=")(1)
            '    ddlScoreValue.Items.Add(New ListItem(strScoreValue & "-" & strScore, strScoreValue))
            'Next

            'Dim ScoreValue As String = DataBinder.Eval(e.Row.DataItem, "ScoreValue")
            'Dim DeptScoreValue As String = DataBinder.Eval(e.Row.DataItem, "DeptScoreValue")
            'If DeptScoreValue <> 0 Then
            '    ddlScoreValue.SelectedValue = DeptScoreValue
            'End If
            'If ScoreValue <> 0 Then
            '    ddlScoreValue.SelectedValue = ScoreValue
            'End If

            'If ScoreValue <> DeptScoreValue And ScoreValue <> 0 And DeptScoreValue <> 0 Then
            '    ddlScoreValue.ForeColor = Drawing.Color.Red
            'End If
            'Dim dt As DataTable = pcMain.DataTable
            'Dim dr() As DataRow = dt.Select("CompID='" & DirectCast(gvMain.DataSource, DataTable).Rows(e.Row.RowIndex)("CompID").ToString() & "' And EmpID='" & DirectCast(gvMain.DataSource, DataTable).Rows(e.Row.RowIndex)("EmpID").ToString() & "'")
            'If ddlScoreValue.SelectedValue <> 0 Then
            '    dr(0)("ScoreValue") = ddlScoreValue.SelectedValue
            '    dr(0)("Score") = ddlScoreValue.SelectedItem.Text.Split("-")(1)
            'End If


            Dim tmpBtn As New ImageButton
            '奬懲
            tmpBtn = e.Row.Cells(12).FindControl("ibtnReward")
            If DirectCast(gvMain.DataSource, DataTable).Rows(e.Row.RowIndex)("RewardCnt").ToString() > 0 Then
                tmpBtn.Visible = True
            End If
            '業績
            tmpBtn = e.Row.Cells(12).FindControl("ibtnPerformance")
            If DirectCast(gvMain.DataSource, DataTable).Rows(e.Row.RowIndex)("PerformanceCnt").ToString() > 0 Then
                tmpBtn.Visible = True
            End If
            '考核補充說明
            tmpBtn = e.Row.Cells(12).FindControl("ibtnComment")
            If DirectCast(gvMain.DataSource, DataTable).Rows(e.Row.RowIndex)("CommentCnt").ToString() > 0 Then
                tmpBtn.Visible = True
            End If

            'Dim txtGradeOrder As TextBox = CType(e.Row.FindControl("txtGradeOrder"), TextBox)
            'txtGradeOrder.Text = DataBinder.Eval(e.Row.DataItem, "GradeOrder")
            'If txtGradeOrder.Text = "0" Then
            '    txtGradeOrder.Text = ""
            'End If

            Dim ddlGrade As DropDownList = CType(e.Row.FindControl("ddlGrade"), DropDownList)
            ddlGrade.Items.Add(New ListItem("", ""))
            ddlGrade.Items.Add(New ListItem("特優", "9"))
            ddlGrade.Items.Add(New ListItem("優等", "1"))
            ddlGrade.Items.Add(New ListItem("甲上", "6"))
            ddlGrade.Items.Add(New ListItem("甲等", "2"))
            ddlGrade.Items.Add(New ListItem("甲下", "7"))
            ddlGrade.Items.Add(New ListItem("乙等", "3"))
            ddlGrade.Items.Add(New ListItem("丙等", "4"))

            If hidGroupID.Value = "0" Then
                ddlGrade.Enabled = False
            End If

            Dim Grade As String = DataBinder.Eval(e.Row.DataItem, "Grade")
            Grade = Grade.Trim()
            ddlGrade.SelectedValue = Grade
            Dim dt As DataTable = pcMain.DataTable
            Dim dr() As DataRow = dt.Select("CompID='" & DirectCast(gvMain.DataSource, DataTable).Rows(e.Row.RowIndex)("CompID").ToString() & "' And EmpID='" & DirectCast(gvMain.DataSource, DataTable).Rows(e.Row.RowIndex)("EmpID").ToString() & "'")
            If ddlGrade.SelectedValue <> "" Then
                dr(0)("Grade") = ddlGrade.SelectedValue
            End If

            Dim GradeDept As String = DataBinder.Eval(e.Row.DataItem, "GradeDept")
            GradeDept = GradeDept.Trim()
            If Grade <> GradeDept Then
                ddlGrade.ForeColor = System.Drawing.Color.Blue
            End If


            If e.Row.Cells(1).Text = "0" Then
                e.Row.Cells(1).Text = ""
            End If
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
            For i = 9 To 11
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

            
            '    Select Case e.Row.Cells(14).Text
            '        Case "1"
            '            e.Row.Cells(14).Text = "優"
            '        Case "2"
            '            e.Row.Cells(14).Text = "甲"
            '        Case "3"
            '            e.Row.Cells(14).Text = "乙"
            '        Case "4"
            '            e.Row.Cells(14).Text = "丙"
            '        Case "6"
            '            e.Row.Cells(14).Text = "甲上"
            '        Case "7"
            '            e.Row.Cells(14).Text = "甲下"
            '        Case "9"
            '            e.Row.Cells(14).Text = "特優"
            '        Case Else
            '            e.Row.Cells(14).Text = ""
            '    End Select
            '    If ViewState.Item("Result") = "1" Or ViewState.Item("Status") = "2" Then
            '        ddlScoreValue.Enabled = False
            '        txtGradeOrder.Enabled = False
            '        ddlGradeAdjust.Enabled = False
            '    End If
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

            'Me.CallMiddlePage("~/GS/GS1110.aspx", New ButtonState() {btnX}, _
            '    "CompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
            '    "EmpID=" & gvMain.DataKeys(selectedRow(gvMain))("EmpID").ToString())

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
            Using dt As DataTable = objGS1.GetEmpComment(gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), ViewState.Item("ApplyID"), ViewState.Item("ApplyTime"), ViewState.Item("Seq"), ViewState.Item("GradeYear"), ViewState.Item("GradeSeq"), ViewState.Item("DeptEX"), gvMain.DataKeys(selectedRow(gvMain))("EmpID").ToString())
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        Select Case dt.Rows(i)("CommentType").ToString()
                            Case "1"
                                strComment = dt.Rows(i)("Comment").ToString()
                            Case "2"
                                strComment_Adjust = dt.Rows(i)("Comment").ToString()
                        End Select
                    Next
                End If
            End Using
            Me.CallMiddlePage("~/GS/GS1170.aspx", New ButtonState() {btnX}, _
                    "CompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
                    "EmpID=" & gvMain.DataKeys(selectedRow(gvMain))("EmpID").ToString(), _
                    "Comment=" & strComment, "Comment_Adjust=" & strComment_Adjust)
        End If

    End Sub

    Private Function funCheckData(ByVal IsSave As Boolean) As Boolean
        Dim objGS1 As New GS1
        Dim dt As DataTable  'pcMain.DataTable
        dt = pcMain.DataTable
        Dim dr() As DataRow
        Dim dtSeq(dt.Rows.Count - 1) As Integer
        'Dim IsScoreValue As Boolean = False
        Dim IsGrade As Boolean = False

        Dim GradeMax As Integer = 0
        Dim GradeMin As Integer = 0

        For i As Integer = 0 To dt.Rows.Count - 1
            'If Integer.Parse(dt.Rows(i)("GradeOrder")) > pcMain.DataTable.Rows.Count Then
            '    Bsp.Utility.ShowMessage(Me, "年度排序不可超過總筆數(" + pcMain.DataTable.Rows.Count.ToString() + ")！")
            '    Return False
            'End If
            dtSeq(i) = Integer.Parse(dt.Rows(i)("GradeOrder"))
            'If dt.Rows(i)("ScoreValue") = "0" Then
            '    IsScoreValue = True
            'End If
            If dt.Rows(i)("Grade") = "" Then
                IsGrade = True
                Exit For
            End If
        Next

        If IsGrade Then
            Bsp.Utility.ShowMessage(Me, "尚有同仁無考績，請確認所有同仁皆有考績！")
            Return False
        End If

        If (IsSave) Then

            dt = objGS1.GS1200Query(ViewState.Item("CompID"), _
                    ViewState.Item("ApplyID"), _
                    ViewState.Item("ApplyTime"), _
                    ViewState.Item("Seq"), _
                    ViewState.Item("Status"), _
                    ViewState.Item("MainFlag"), _
                    ViewState.Item("GradeYear"), _
                    ViewState.Item("GradeSeq"), _
                    ViewState.Item("EvaluationSeq"), _
                    ViewState.Item("DeptEX"))

            Array.Sort(dtSeq)
            If dtSeq(0) = 0 Then
                Bsp.Utility.ShowMessage(Me, "尚未開始排序！")
                Return False
            End If

            'For i As Integer = 0 To dtSeq.Length - 2
            '    'If dtSeq(0) <> 1 Then
            '    '    Bsp.Utility.ShowMessage(Me, "年度排序需從1號開始編排！")
            '    '    Return False
            '    'End If

            '    If (dtSeq(i + 1) - dtSeq(i)) <> 1 Then
            '        Bsp.Utility.ShowMessage(Me, "年度排序不可跳號！")
            '        Return False
            '    End If
            '    'If IsScoreValue Then
            '    '    Bsp.Utility.ShowMessage(Me, "整體評量結果未輸入！")
            '    '    Return False
            '    'End If
            'Next
            Dim strGroupMsg As String = ""
            For iGroup = 1 To 3
                Select Case iGroup
                    Case 1
                        strGroupMsg = "非管理職"
                    Case 2
                        strGroupMsg = "科主管"
                    Case 3
                        strGroupMsg = "單位主管"
                End Select
                '檢查各考績之間的排序是否跳號
                '特優
                dr = dt.Select("Grade='9' and GroupID=" & Bsp.Utility.Quote(iGroup), "GradeOrder")
                If dr.GetUpperBound(0) >= 0 Then
                    GradeMin = Integer.Parse(dr(0)("GradeOrder"))
                    GradeMax = Integer.Parse(dr(dr.GetUpperBound(0))("GradeOrder"))
                    If GradeMin <> 1 Then
                        Bsp.Utility.ShowMessage(Me, strGroupMsg & "排序請依照考績順序排序！")
                        Return False
                    End If
                    If GradeMax - GradeMin <> dr.GetUpperBound(0) Then
                        Bsp.Utility.ShowMessage(Me, strGroupMsg & "未依考績做排序，請確認排序順序正確！")
                        Return False
                    End If
                End If
                '優
                dr = dt.Select("Grade='1' and GroupID=" & Bsp.Utility.Quote(iGroup), "GradeOrder")
                If dr.GetUpperBound(0) >= 0 Then
                    If Integer.Parse(dr(0)("GradeOrder")) <> 1 And GradeMin = 0 Then
                        Bsp.Utility.ShowMessage(Me, strGroupMsg & "排序請依照考績順序排序！")
                        Return False
                    End If
                    If GradeMin > 0 And GradeMax + 1 < Integer.Parse(dr(0)("GradeOrder")) Then
                        Bsp.Utility.ShowMessage(Me, strGroupMsg & "考績優等的排序不可小於前一等第！")
                        Return False
                    End If
                    GradeMin = Integer.Parse(dr(0)("GradeOrder"))
                    GradeMax = Integer.Parse(dr(dr.GetUpperBound(0))("GradeOrder"))
                    If GradeMax - GradeMin <> dr.GetUpperBound(0) Then
                        Bsp.Utility.ShowMessage(Me, strGroupMsg & "未依考績做排序，請確認排序順序正確！")
                        Return False
                    End If
                End If
                '甲上
                dr = dt.Select("Grade='6' and GroupID=" & Bsp.Utility.Quote(iGroup), "GradeOrder")
                If dr.GetUpperBound(0) >= 0 Then
                    If Integer.Parse(dr(0)("GradeOrder")) <> 1 And GradeMin = 0 Then
                        Bsp.Utility.ShowMessage(Me, strGroupMsg & "排序請依照考績順序排序！")
                        Return False
                    End If
                    If GradeMin > 0 And GradeMax + 1 < Integer.Parse(dr(0)("GradeOrder")) Then
                        Bsp.Utility.ShowMessage(Me, strGroupMsg & "考績甲上的排序不可小於前一等第！")
                        Return False
                    End If
                    GradeMin = Integer.Parse(dr(0)("GradeOrder"))
                    GradeMax = Integer.Parse(dr(dr.GetUpperBound(0))("GradeOrder"))
                    If GradeMax - GradeMin <> dr.GetUpperBound(0) Then
                        Bsp.Utility.ShowMessage(Me, strGroupMsg & "未依考績做排序，請確認排序順序正確！")
                        Return False
                    End If
                End If
                '甲
                dr = dt.Select("Grade='2' and GroupID=" & Bsp.Utility.Quote(iGroup), "GradeOrder")
                If dr.GetUpperBound(0) >= 0 Then
                    If Integer.Parse(dr(0)("GradeOrder")) <> 1 And GradeMin = 0 Then
                        Bsp.Utility.ShowMessage(Me, strGroupMsg & "排序請依照考績順序排序！")
                        Return False
                    End If
                    If GradeMin > 0 And GradeMax + 1 < Integer.Parse(dr(0)("GradeOrder")) Then
                        Bsp.Utility.ShowMessage(Me, strGroupMsg & "考績甲等的排序不可小於前一等第！")
                        Return False
                    End If
                    GradeMin = Integer.Parse(dr(0)("GradeOrder"))
                    GradeMax = Integer.Parse(dr(dr.GetUpperBound(0))("GradeOrder"))
                    If GradeMax - GradeMin <> dr.GetUpperBound(0) Then
                        Bsp.Utility.ShowMessage(Me, strGroupMsg & "未依考績做排序，請確認排序順序正確！")
                        Return False
                    End If
                End If
                '甲下
                dr = dt.Select("Grade='7' and GroupID=" & Bsp.Utility.Quote(iGroup), "GradeOrder")
                If dr.GetUpperBound(0) >= 0 Then
                    If Integer.Parse(dr(0)("GradeOrder")) <> 1 And GradeMin = 0 Then
                        Bsp.Utility.ShowMessage(Me, strGroupMsg & "排序請依照考績順序排序！")
                        Return False
                    End If
                    If GradeMin > 0 And GradeMax + 1 < Integer.Parse(dr(0)("GradeOrder")) Then
                        Bsp.Utility.ShowMessage(Me, strGroupMsg & "考績甲下的排序不可小於前一等第！")
                        Return False
                    End If
                    GradeMin = Integer.Parse(dr(0)("GradeOrder"))
                    GradeMax = Integer.Parse(dr(dr.GetUpperBound(0))("GradeOrder"))
                    If GradeMax - GradeMin <> dr.GetUpperBound(0) Then
                        Bsp.Utility.ShowMessage(Me, strGroupMsg & "未依考績做排序，請確認排序順序正確！")
                        Return False
                    End If
                End If
                '乙
                dr = dt.Select("Grade='3' and GroupID=" & Bsp.Utility.Quote(iGroup), "GradeOrder")
                If dr.GetUpperBound(0) >= 0 Then
                    If Integer.Parse(dr(0)("GradeOrder")) <> 1 And GradeMin = 0 Then
                        Bsp.Utility.ShowMessage(Me, strGroupMsg & "排序請依照考績順序排序！")
                        Return False
                    End If
                    If GradeMin > 0 And GradeMax + 1 < Integer.Parse(dr(0)("GradeOrder")) Then
                        Bsp.Utility.ShowMessage(Me, strGroupMsg & "考績乙等的排序不可小於前一等第！")
                        Return False
                    End If
                    GradeMin = Integer.Parse(dr(0)("GradeOrder"))
                    GradeMax = Integer.Parse(dr(dr.GetUpperBound(0))("GradeOrder"))
                    If GradeMax - GradeMin <> dr.GetUpperBound(0) Then
                        Bsp.Utility.ShowMessage(Me, strGroupMsg & "未依考績做排序，請確認排序順序正確！")
                        Return False
                    End If
                End If
                '丙
                dr = dt.Select("Grade='4' and GroupID=" & Bsp.Utility.Quote(iGroup), "GradeOrder")
                If dr.GetUpperBound(0) >= 0 Then
                    If Integer.Parse(dr(0)("GradeOrder")) <> 1 And GradeMin = 0 Then
                        Bsp.Utility.ShowMessage(Me, strGroupMsg & "排序請依照考績順序排序！")
                        Return False
                    End If
                    If GradeMin > 0 And GradeMax + 1 < Integer.Parse(dr(0)("GradeOrder")) Then
                        Bsp.Utility.ShowMessage(Me, strGroupMsg & "考績丙等的排序不可小於前一等第！")
                        Return False
                    End If
                    GradeMin = Integer.Parse(dr(0)("GradeOrder"))
                    GradeMax = Integer.Parse(dr(dr.GetUpperBound(0))("GradeOrder"))
                    If GradeMax - GradeMin <> dr.GetUpperBound(0) Then
                        Bsp.Utility.ShowMessage(Me, strGroupMsg & "排序不可跳號！")
                        Return False
                    End If
                End If
            Next

        End If

        Return True
    End Function

    Private Function SaveData() As Boolean
        Dim objGS1 As New GS1()
        '儲存資料
        Try
            Dim dt As DataTable = pcMain.DataTable

            Return objGS1.GS1200Update(dt, ViewState.Item("ApplyID"), ViewState.Item("ApplyTime"), ViewState.Item("Seq"), ViewState.Item("GradeSeq"), ViewState.Item("GradeYear"))
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, "[CC0201_99]" & Bsp.Utility.getMessage("E_00000") & Bsp.Utility.getInnerException(Me.FunID, ex))
            Return False
        End Try
    End Function
    '產生考績
    Private Function GradeData() As Boolean
        Dim objGS1 As New GS1()
        '儲存資料
        Try
            '撈單位考績
            Dim DeptGrade As String = ""
            Using dt As DataTable = objGS1.GetDeptGrade(ViewState.Item("CompID"), ViewState.Item("GradeYear"), ViewState.Item("GradeSeq"), ViewState.Item("MainFlag"), ViewState.Item("ApplyID"))
                If dt.Rows.Count > 0 Then
                    DeptGrade = dt.Rows(0)(0).ToString()
                End If

            End Using
            '取優甲乙比例
            Dim GradeRatio(6) As Double
            Dim intDeptCount(6) As Integer
            Dim strGrade(6) As String
            Dim intTotalCount As Integer = CInt(pcMain.DataTable.Rows.Count)
            strGrade(0) = "1"   '1-優
            strGrade(1) = "2"   '2-甲
            strGrade(2) = "3"   '3-乙
            strGrade(3) = "4"   '4-丙
            strGrade(4) = "6"   '6-甲上
            strGrade(5) = "7"   '7-甲下
            strGrade(6) = "9"   '9-特優
            Using dt As DataTable = objGS1.GetGradeRation(ViewState.Item("CompID"), ViewState.Item("GradeYear"), ViewState.Item("GradeSeq"), DeptGrade)
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        Select Case dt.Rows(i)("GradeID").ToString
                            Case 1      '1優
                                GradeRatio(0) = CDbl(dt.Rows(i)("Ratio").ToString) / 100
                            Case 3      '3乙
                                GradeRatio(2) = CDbl(dt.Rows(i)("Ratio").ToString) / 100
                            Case 4      '4丙
                                GradeRatio(3) = CDbl(dt.Rows(i)("Ratio").ToString) / 100
                            Case 6      '6甲上
                                GradeRatio(4) = CDbl(dt.Rows(i)("Ratio").ToString) / 100
                            Case 7      '7甲下
                                GradeRatio(5) = CDbl(dt.Rows(i)("Ratio").ToString) / 100
                            Case 9      '9特優
                                GradeRatio(6) = CDbl(dt.Rows(i)("Ratio").ToString) / 100
                        End Select
                    Next
                    '剩下是2甲
                    GradeRatio(1) = 1 - GradeRatio(0) - GradeRatio(2) - GradeRatio(3) - GradeRatio(4) - GradeRatio(5) - GradeRatio(6)
                End If
                intDeptCount(0) = FormatNumber(intTotalCount * GradeRatio(0), 0)
                intDeptCount(2) = FormatNumber(intTotalCount * GradeRatio(2), 0)
                intDeptCount(3) = FormatNumber(intTotalCount * GradeRatio(3), 0)
                intDeptCount(4) = FormatNumber(intTotalCount * GradeRatio(4), 0)
                intDeptCount(5) = FormatNumber(intTotalCount * GradeRatio(5), 0)
                intDeptCount(6) = FormatNumber(intTotalCount * GradeRatio(6), 0)
                intDeptCount(1) = intTotalCount - intDeptCount(0) - intDeptCount(2) - intDeptCount(3) - intDeptCount(4) - intDeptCount(5) - intDeptCount(6)
            End Using

            Return objGS1.GS1200GradeData(ViewState.Item("CompID"), ViewState.Item("ApplyID"), ViewState.Item("ApplyTime"), ViewState.Item("Seq"), ViewState.Item("GradeYear"), ViewState.Item("GradeSeq"), _
                                         ViewState.Item("MainFlag"), DeptGrade, intDeptCount, strGrade, intTotalCount, ViewState.Item("IsSignNext"), ViewState.Item("DeptEX"))
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, "[CC0201_99]" & Bsp.Utility.getMessage("E_00000") & Bsp.Utility.getInnerException(Me.FunID, ex))
            Return False
        End Try
    End Function
    Private Sub TransferFPage(ByVal TxnID As String)
        Dim btnU As New ButtonState(ButtonState.emButtonType.Update)
        Dim btnQ As New ButtonState(ButtonState.emButtonType.Query)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)

        btnX.Caption = "返回"

        If TxnID = "GS1150" Then
            Me.TransferFramePage("~/GS/GS1150.aspx", New ButtonState() {btnX}, _
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
                "Result=" & ViewState.Item("Result"))

        ElseIf TxnID = "GS1210" Then
            Me.TransferFramePage("~/GS/GS1160.aspx", New ButtonState() {btnX}, _
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
                "Result=" & ViewState.Item("Result"), _
                "GroupID=" & ViewState.Item("GroupID"))
            'Me.TransferFramePage("~/GS/GS1210.aspx", New ButtonState() {btnX}, _
            '    "CompID=" & ViewState.Item("CompID"), _
            '    "ApplyID=" & ViewState.Item("ApplyID"), _
            '    "ApplyTime=" & ViewState.Item("ApplyTime"), _
            '    "Status=" & ViewState.Item("Status"), _
            '    "Seq=" & ViewState.Item("Seq"), _
            '    "GradeYear=" & ViewState.Item("GradeYear"), _
            '    "GradeSeq=" & ViewState.Item("GradeSeq"), _
            '    "EvaluationSeq=" & ViewState.Item("EvaluationSeq"), _
            '    "MainFlag=" & ViewState.Item("MainFlag"), _
            '    "DeptEX=" & ViewState.Item("DeptEX"), _
            '    "IsSignNext=" & ViewState.Item("IsSignNext"), _
            '    "DataCount=" & pcMain.DataTable.Rows.Count.ToString(), _
            '    "Result=" & ViewState.Item("Result"))

        ElseIf TxnID = "GS1220" Then
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
        ElseIf TxnID = "GS1180" Then
            btnU.Caption = "確認"
            btnX.Caption = "返回"
            ViewState.Item("Upload") = "Y"
            Me.TransferFramePage("~/GS/GS1180.aspx", New ButtonState() {btnU, btnX}, _
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
                "Result=" & ViewState.Item("Result"), _
                "GroupID=" & ViewState.Item("GroupID"))
        End If
    End Sub

    'Protected Sub ddlScoreValue_Changed(sender As Object, e As System.EventArgs)
    '    Dim ddlScoreValue As DropDownList = CType(sender, DropDownList)
    '    Dim gvRow As GridViewRow = CType(ddlScoreValue.NamingContainer, GridViewRow)

    '    Dim CompID As String = gvMain.DataKeys(gvRow.RowIndex)("CompID").ToString()
    '    Dim EmpID As String = gvMain.DataKeys(gvRow.RowIndex)("EmpID").ToString()

    '    Dim dt As DataTable = pcMain.DataTable
    '    Dim dr() As DataRow = dt.Select("CompID='" & CompID & "' And EmpID='" & EmpID & "'")
    '    dr(0)("ScoreValue") = ddlScoreValue.SelectedValue
    '    dr(0)("Score") = ddlScoreValue.SelectedItem.Text.Split("-")(1)

    '    If dr(0)("ScoreValue") <> dr(0)("DeptScoreValue") And dr(0)("ScoreValue") <> 0 And dr(0)("DeptScoreValue") <> 0 Then
    '        ddlScoreValue.ForeColor = Drawing.Color.Red
    '    Else
    '        ddlScoreValue.ForeColor = Drawing.Color.Black
    '    End If
    'End Sub

    'Protected Sub txtGradeOrder_Changed(sender As Object, e As System.EventArgs)
    '    Dim txtGradeOrder As TextBox = CType(sender, TextBox)
    '    Dim gvRow As GridViewRow = CType(txtGradeOrder.NamingContainer, GridViewRow)

    '    Dim CompID As String = gvMain.DataKeys(gvRow.RowIndex)("CompID").ToString()
    '    Dim EmpID As String = gvMain.DataKeys(gvRow.RowIndex)("EmpID").ToString()

    '    Dim dt As DataTable = pcMain.DataTable
    '    Dim dr() As DataRow = dt.Select("CompID='" & CompID & "' And EmpID='" & EmpID & "'")
    '    dr(0)("GradeOrder") = IIf(txtGradeOrder.Text = "", 0, txtGradeOrder.Text)
    'End Sub

    Protected Sub ddlGrade_Changed(sender As Object, e As System.EventArgs)
        Dim ddlGrade As DropDownList = CType(sender, DropDownList)
        Dim gvRow As GridViewRow = CType(ddlGrade.NamingContainer, GridViewRow)

        Dim CompID As String = gvMain.DataKeys(gvRow.RowIndex)("CompID").ToString()
        Dim EmpID As String = gvMain.DataKeys(gvRow.RowIndex)("EmpID").ToString()

        Dim dt As DataTable = pcMain.DataTable
        Dim dr() As DataRow = dt.Select("CompID='" & CompID & "' And EmpID='" & EmpID & "'")
        dr(0)("Grade") = ddlGrade.SelectedValue

        If dr(0)("Grade") <> dr(0)("GradeDept") Then
            ddlGrade.ForeColor = System.Drawing.Color.Blue
        Else
            ddlGrade.ForeColor = System.Drawing.Color.Black
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
                Dim strFileName As String = Bsp.Utility.GetNewFileName("GS1200年度考核(區處主管排序)-") & ".xlsx"
                Using dtDownload = objGS1.GS1200Download(ViewState.Item("CompID"), _
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
                    ViewState.Item("DeptEX"), _
                    ViewState.Item("GroupID"))

                    Using pck As New ExcelPackage(Util.getExcelOpenXml(dtDownload, "GS1200年度考核(區處主管排序)"))
                        Dim ws As ExcelWorksheet = pck.Workbook.Worksheets("GS1200年度考核(區處主管排序)")
                        ws.InsertRow(2, 1)  '插入列
                        ws.Cells(1, 1, 2, 1).Merge = True   '合併儲存格
                        ws.Cells(1, 2, 2, 2).Merge = True   '合併儲存格
                        ws.Cells(1, 3, 2, 3).Merge = True   '合併儲存格
                        ws.Cells(1, 4, 2, 4).Merge = True   '合併儲存格
                        ws.Cells(1, 5, 2, 5).Merge = True   '合併儲存格
                        ws.Cells(1, 6, 2, 6).Merge = True   '合併儲存格
                        ws.Cells(1, 7, 1, 8).Merge = True   '合併儲存格
                        ws.Cells(1, 9, 1, 10).Merge = True   '合併儲存格
                        ws.Cells(1, 11, 2, 11).Merge = True   '合併儲存格
                        ws.Cells(1, 12, 2, 12).Merge = True   '合併儲存格
                        ws.Cells("A1:F1").Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                        ws.Cells("A1:F1").Style.VerticalAlignment = ExcelVerticalAlignment.Center
                        ws.Cells("K1:L1").Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                        ws.Cells("K1:L1").Style.VerticalAlignment = ExcelVerticalAlignment.Center
                        ws.Cells("K1:L1").Style.WrapText = True
                        ws.Cells("G1").Value = "單位主管"
                        ws.Cells("I1").Value = "區處主管"

                        ws.Cells("G2:J2").Style.Font.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#FFFFFF"))
                        ws.Cells("G2:J2").Style.Font.Bold = True
                        ws.Cells("G2:J2").Style.Fill.PatternType = ExcelFillStyle.Solid
                        ws.Cells("G2:J2").Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#507CD1"))
                        ws.Cells("2:2").Style.Fill.PatternType = ExcelFillStyle.Solid
                        ws.Cells("2:2").Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#507CD1"))
                        ws.Cells("G2").Value = "考績"
                        ws.Cells("H2").Value = "初排序"
                        ws.Cells("I2").Value = "考績"
                        ws.Cells("J2").Value = "排序"


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

                'gvExport.DataSource = objGS1.GS1200Download(ViewState.Item("CompID"), _
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
                '    ViewState.Item("DeptEX"), _
                '    ViewState.Item("GroupID"))
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

        GridViewData.HeaderRow.Cells(8).ColumnSpan = "3"
        GridViewData.HeaderRow.Cells(8).Text = "區處主管"
        GridViewData.HeaderRow.Cells().RemoveAt(9)
        GridViewData.HeaderRow.Cells().RemoveAt(10)

        GridViewData.HeaderRow.Cells(9).ColumnSpan = "2"
        GridViewData.HeaderRow.Cells(9).Text = "單位主管"
        GridViewData.HeaderRow.Cells().RemoveAt(10)

        GridViewData.HeaderRow.Cells(10).ColumnSpan = "2"
        GridViewData.HeaderRow.Cells(10).Text = "區處主管"
        GridViewData.HeaderRow.Cells().RemoveAt(11)

        If ViewState.Item("IsPerformance") = "Y" Then
            For i = 11 To 42
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
        HeaderCell_2.Text = "配置考績"
        HeaderCell_2.HorizontalAlign = HorizontalAlign.Center
        HeaderCell_2.Font.Bold = True
        HeaderGridRow.Cells.Add(HeaderCell_2)

        Dim HeaderCell_3 As New TableCell()
        HeaderCell_3.Text = "排序"
        HeaderCell_3.HorizontalAlign = HorizontalAlign.Center
        HeaderCell_3.Font.Bold = True
        HeaderGridRow.Cells.Add(HeaderCell_3)

        Dim HeaderCell_4 As New TableCell()
        HeaderCell_4.Text = "配置考績"
        HeaderCell_4.HorizontalAlign = HorizontalAlign.Center
        HeaderCell_4.Font.Bold = True
        HeaderGridRow.Cells.Add(HeaderCell_4)

        Dim HeaderCell_5 As New TableCell()
        HeaderCell_5.Text = "考績檢視建議"
        HeaderCell_5.HorizontalAlign = HorizontalAlign.Center
        HeaderCell_5.Font.Bold = True
        HeaderGridRow.Cells.Add(HeaderCell_5)

        Dim HeaderCell_6 As New TableCell()
        HeaderCell_6.Text = "整體評量調整說明"
        HeaderCell_6.HorizontalAlign = HorizontalAlign.Center
        HeaderCell_6.Font.Bold = True
        HeaderGridRow.Cells.Add(HeaderCell_6)

        Dim HeaderCell_7 As New TableCell()
        HeaderCell_7.Text = "考核補充說明"
        HeaderCell_7.HorizontalAlign = HorizontalAlign.Center
        HeaderCell_7.Font.Bold = True
        HeaderGridRow.Cells.Add(HeaderCell_7)

        Dim HeaderCell_8 As New TableCell()
        HeaderCell_8.Text = "整體評量調整說明"
        HeaderCell_8.HorizontalAlign = HorizontalAlign.Center
        HeaderCell_8.Font.Bold = True
        HeaderGridRow.Cells.Add(HeaderCell_8)

        Dim HeaderCell_9 As New TableCell()
        HeaderCell_9.Text = "考績檢視補充說明"
        HeaderCell_9.HorizontalAlign = HorizontalAlign.Center
        HeaderCell_9.Font.Bold = True
        HeaderGridRow.Cells.Add(HeaderCell_9)

        GridViewData.Controls(0).Controls.AddAt(1, HeaderGridRow)
    End Sub
    Private Sub GoBack()
        Dim ti As TransferInfo = Me.StateTransfer
        'Me.TransferFramePage(ti.CallerUrl, Nothing, ti.Args)
        Me.TransferFramePage("~/GS/GS1000.aspx", Nothing, ti.Args)
    End Sub
    '檔案上傳
    Private Sub DoUpload()
        Dim btnC As New ButtonState(ButtonState.emButtonType.Confirm)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)

        btnC.Caption = "確定上傳"
        btnX.Caption = "關閉離開"
        ViewState.Item("Upload") = "Y"
        Me.CallMiddlePage("~/GS/GS1201.aspx", New ButtonState() {btnC, btnX}, _
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
                "Result=" & ViewState.Item("Result"), _
                "GroupID=" & ViewState.Item("GroupID"))


    End Sub
    Protected Sub rbnGroup1_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbnGroup1.CheckedChanged
        'ViewState.Item("Upload") = "N"
        ViewState.Item("GroupID") = "1"
        subChangeGroup()
        'Bsp.Utility.RunClientScript(Me.Page, "ChangeGroup('" & ViewState.Item("GroupID") & "');")
    End Sub
    Protected Sub rbnGroup2_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbnGroup2.CheckedChanged
        'ViewState.Item("Upload") = "N"
        ViewState.Item("GroupID") = "2"
        subChangeGroup()
        'Bsp.Utility.RunClientScript(Me.Page, "ChangeGroup('" & ViewState.Item("GroupID") & "');")
    End Sub
    Protected Sub rbnGroup3_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbnGroup3.CheckedChanged
        'ViewState.Item("Upload") = "N"
        ViewState.Item("GroupID") = "3"
        subChangeGroup()
        'Bsp.Utility.RunClientScript(Me.Page, "ChangeGroup('" & ViewState.Item("GroupID") & "');")
    End Sub
    Private Sub subChangeGroup()
        Dim objGS1 As New GS1
        If hidGroupID.Value <> "0" Then
            SaveData()
        End If
        Using dt As DataTable = objGS1.GS1200Query(ViewState.Item("CompID"), _
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
    Protected Sub btnCheckSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChangeGroup.Click
        Dim objGS1 As New GS1
        If hidGroupID.Value <> "0" Then
            SaveData()
        End If

        Using dt As DataTable = objGS1.GS1200Query(ViewState.Item("CompID"), _
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
