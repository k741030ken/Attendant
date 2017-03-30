Imports System.Data

Partial Class GS_GS0040
    Inherits PageBase

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not IsPostBack() Then

            Try

                If Session.Item("PageSource") IsNot Nothing Then

                    ViewState.Item("PageSource") = Session.Item("PageSource")
                    Session.Remove("PageSource")
                    ViewState.Item("CompID") = Session.Item("hidCompID")
                    Session.Remove("hidCompID")
                    ViewState.Item("ApplyID") = Session.Item("ApplyID")
                    Session.Remove("ApplyID")
                    ViewState.Item("ApplyTime") = Session.Item("ApplyTime")
                    Session.Remove("ApplyTime")
                    ViewState.Item("Seq") = Session.Item("Seq")
                    Session.Remove("Seq")
                    ViewState.Item("Status") = Session.Item("Status")
                    Session.Remove("Status")
                    ViewState.Item("MainFlag") = Session.Item("MainFlag")
                    Session.Remove("MainFlag")
                    Dim objGS As New GS1()
                    Dim strWhere As String = ""
                    Dim objHR As New HR()
                    Dim DeptEx As String = "N"
                    Dim strGradeYear As String = ""
                    Dim strGradeSeq As String = ""
                    Dim IsSignNext As String = "N"
                    Dim Result As String = "0"

                    Using dt As DataTable = objGS.GetGradeParameter(UserProfile.CompID)
                        strGradeYear = dt.Rows.Item(0)("GradeYear").ToString()
                        strGradeSeq = dt.Rows.Item(0)("GradeSeq").ToString()
                    End Using
                    strWhere = " And CompID = " & Bsp.Utility.Quote(ViewState.Item("CompID"))
                    strWhere = strWhere & " And ApplyTime = " & Bsp.Utility.Quote(Format(CDate(ViewState.Item("ApplyTime")), "yyyy/MM/dd HH:mm:ss"))
                    strWhere = strWhere & " And ApplyID = " & Bsp.Utility.Quote(ViewState.Item("ApplyID"))
                    strWhere = strWhere & " And Seq > " & Bsp.Utility.Quote(ViewState.Item("Seq"))
                    If objHR.IsDataExists("SignLog", strWhere) Then
                        IsSignNext = "Y"
                    Else
                        IsSignNext = "N"
                    End If
                    strWhere = " And CompID = " & Bsp.Utility.Quote(ViewState.Item("CompID"))
                    strWhere = strWhere & " And ApplyTime = " & Bsp.Utility.Quote(Format(CDate(ViewState.Item("ApplyTime")), "yyyy/MM/dd HH:mm:ss"))
                    strWhere = strWhere & " And ApplyID = " & Bsp.Utility.Quote(ViewState.Item("ApplyID"))
                    strWhere = strWhere & " And Seq = " & Bsp.Utility.Quote(ViewState.Item("Seq"))
                    strWhere = strWhere & " And Result = '1'"
                    If objHR.IsDataExists("SignLog", strWhere) Then
                        Result = "1"
                    Else
                        Result = "0"
                    End If
                    If ViewState.Item("PageSource").Equals("GS1000") Then
                        Select Case ViewState.Item("Status")
                            Case "1", "2"
                                If ViewState.Item("MainFlag") = "1" And IsSignNext = "Y" Then
                                    Dim btnD As New ButtonState(ButtonState.emButtonType.Download)
                                    Dim btnU As New ButtonState(ButtonState.emButtonType.Upload)
                                    Dim btnS As New ButtonState(ButtonState.emButtonType.Update)
                                    Dim btnE As New ButtonState(ButtonState.emButtonType.Executes)
                                    Dim btnO As New ButtonState(ButtonState.emButtonType.OK)
                                    Dim btnC As New ButtonState(ButtonState.emButtonType.Confirm)
                                    btnD.Caption = "結果檔下傳"
                                    btnU.Caption = "上傳(專用格式)"
                                    btnS.Caption = "考績暫存"
                                    btnE.Caption = "排序作業"
                                    btnO.Caption = "考績補充說明"
                                    btnC.Caption = "呈上階主管審核"
                                    If Result = "1" Then
                                        btnU.Visible = False
                                        btnS.Visible = False
                                        btnE.Visible = False
                                        btnC.Visible = False
                                    End If
                                    Me.TransferFramePage("~/GS/GS1100.aspx", New ButtonState() {btnD, btnU, btnS, btnE, btnO, btnC}, _
                                                         "CompID=" & ViewState.Item("CompID"), _
                                                         "ApplyID=" & ViewState.Item("ApplyID"), _
                                                         "ApplyTime=" & Format(CDate(ViewState.Item("ApplyTime")), "yyyy/MM/dd HH:mm:ss"), _
                                                         "Seq=" & ViewState.Item("Seq"), _
                                                         "Status=" & ViewState.Item("Status"), _
                                                         "MainFlag=" & ViewState.Item("MainFlag"), _
                                                         "IsSignNext=" & IsSignNext, _
                                                         "GradeYear=" & strGradeYear, _
                                                         "GradeSeq=" & strGradeSeq, _
                                                         "Result=" & Result)
                                End If
                                If ViewState.Item("MainFlag") = "1" And IsSignNext = "N" Then
                                    Dim btnD As New ButtonState(ButtonState.emButtonType.Download)
                                    Dim btnU As New ButtonState(ButtonState.emButtonType.Upload)
                                    Dim btnS As New ButtonState(ButtonState.emButtonType.Update)
                                    Dim btnE As New ButtonState(ButtonState.emButtonType.Executes)
                                    Dim btnO As New ButtonState(ButtonState.emButtonType.OK)
                                    Dim btnC As New ButtonState(ButtonState.emButtonType.Confirm)
                                    Dim btnC1 As New ButtonState(ButtonState.emButtonType.Copy)
                                    btnD.Caption = "結果檔下傳"
                                    btnU.Caption = "上傳(專用格式)"
                                    btnS.Caption = "考績暫存"
                                    btnE.Caption = "排序作業"
                                    btnO.Caption = "考績補充說明"
                                    btnC.Caption = "送出"
                                    btnC1.Caption = "統計表"
                                    If Result = "1" Or ViewState.Item("Status") = "2" Then
                                        btnU.Visible = False
                                        btnS.Visible = False
                                        btnC.Visible = False
                                    End If
                                    Me.TransferFramePage("~/GS/GS1100.aspx", New ButtonState() {btnD, btnU, btnS, btnE, btnO, btnC, btnC1}, _
                                                         "CompID=" & ViewState.Item("CompID"), _
                                                         "ApplyID=" & ViewState.Item("ApplyID"), _
                                                         "ApplyTime=" & Format(CDate(ViewState.Item("ApplyTime")), "yyyy/MM/dd HH:mm:ss"), _
                                                         "Seq=" & ViewState.Item("Seq"), _
                                                         "Status=" & ViewState.Item("Status"), _
                                                         "MainFlag=" & ViewState.Item("MainFlag"), _
                                                         "IsSignNext=" & IsSignNext, _
                                                         "GradeYear=" & strGradeYear, _
                                                         "GradeSeq=" & strGradeSeq, _
                                                         "Result=" & Result)
                                End If
                                If ViewState.Item("MainFlag") = "2" Then
                                    Dim btnD As New ButtonState(ButtonState.emButtonType.Download)
                                    Dim btnU As New ButtonState(ButtonState.emButtonType.Upload)
                                    Dim btnS As New ButtonState(ButtonState.emButtonType.Update)
                                    Dim btnE As New ButtonState(ButtonState.emButtonType.Executes)
                                    Dim btnO As New ButtonState(ButtonState.emButtonType.OK)
                                    Dim btnC As New ButtonState(ButtonState.emButtonType.Confirm)
                                    Dim btnC1 As New ButtonState(ButtonState.emButtonType.Copy)
                                    btnD.Caption = "結果檔下傳"
                                    btnU.Caption = "上傳(專用格式)"
                                    btnS.Caption = "考績暫存"
                                    btnE.Caption = "排序作業"
                                    btnO.Caption = "考績補充說明"
                                    btnC.Caption = "送出"
                                    btnC1.Caption = "統計表"
                                    If Result = "1" Or ViewState.Item("Status") = "2" Then
                                        btnU.Visible = False
                                        btnS.Visible = False
                                        btnE.Visible = False
                                        btnC.Visible = False
                                    End If

                                    '判斷是否全排序
                                    strWhere = " And CompID = " & Bsp.Utility.Quote(ViewState.Item("CompID"))
                                    strWhere = strWhere & " And (DeptID=" & Bsp.Utility.Quote(ViewState.Item("ApplyID")) & " Or DeptID In (Select UpOrganID From GradeBase Where CompID =" & Bsp.Utility.Quote(ViewState.Item("CompID")) & " and GradeYear=" & strGradeYear & " and GradeSeq=" & strGradeSeq & " and GradeDeptID=" & Bsp.Utility.Quote(ViewState.Item("ApplyID")) & "))"
                                    If objHR.IsDataExists("GradeDeptException", strWhere) Then
                                        DeptEx = "Y"
                                    Else
                                        DeptEx = "N"
                                    End If
                                    If objGS.CheckSignLog(ViewState.Item("CompID"), ViewState.Item("ApplyID"), _
                                                          Format(CDate(ViewState.Item("ApplyTime")), "yyyy/MM/dd HH:mm:ss"), ViewState.Item("Seq"), _
                                                          strGradeYear, strGradeSeq, DeptEx) Then
                                        btnU.Visible = False
                                        btnS.Visible = False
                                        btnE.Visible = False
                                        btnO.Visible = False
                                        btnC.Visible = False
                                        btnC1.Visible = False
                                    End If
                                    Me.TransferFramePage("~/GS/GS1200.aspx", New ButtonState() {btnD, btnU, btnS, btnE, btnO, btnC, btnC1}, _
                                                         "CompID=" & ViewState.Item("CompID"), _
                                                         "ApplyID=" & ViewState.Item("ApplyID"), _
                                                         "ApplyTime=" & Format(CDate(ViewState.Item("ApplyTime")), "yyyy/MM/dd HH:mm:ss"), _
                                                         "Seq=" & ViewState.Item("Seq"), _
                                                         "Status=" & ViewState.Item("Status"), _
                                                         "MainFlag=" & ViewState.Item("MainFlag"), _
                                                         "IsSignNext=" & IsSignNext, _
                                                         "GradeYear=" & strGradeYear, _
                                                         "GradeSeq=" & strGradeSeq, _
                                                         "Result=" & Result)
                                End If
                            Case "4", "5"
                                If ViewState.Item("MainFlag") = "1" And IsSignNext = "Y" Then
                                    Dim btnD As New ButtonState(ButtonState.emButtonType.Download)
                                    btnD.Caption = "結果檔下傳"

                                    Me.TransferFramePage("~/GS/GS1300.aspx", New ButtonState() {btnD}, _
                                                         "CompID=" & ViewState.Item("CompID"), _
                                                         "ApplyID=" & ViewState.Item("ApplyID"), _
                                                         "ApplyTime=" & Format(CDate(ViewState.Item("ApplyTime")), "yyyy/MM/dd HH:mm:ss"), _
                                                         "Seq=" & ViewState.Item("Seq"), _
                                                         "Status=" & ViewState.Item("Status"), _
                                                         "MainFlag=" & ViewState.Item("MainFlag"), _
                                                         "IsSignNext=" & IsSignNext, _
                                                         "GradeYear=" & strGradeYear, _
                                                         "GradeSeq=" & strGradeSeq, _
                                                         "Result=" & Result)
                                End If
                                If ViewState.Item("MainFlag") = "1" And IsSignNext = "N" Then
                                    Dim btnD As New ButtonState(ButtonState.emButtonType.Download)
                                    Dim btnC1 As New ButtonState(ButtonState.emButtonType.Copy)
                                    btnD.Caption = "結果檔下傳"
                                    btnC1.Caption = "統計表"
                                    Me.TransferFramePage("~/GS/GS1300.aspx", New ButtonState() {btnD, btnC1}, _
                                                         "CompID=" & ViewState.Item("CompID"), _
                                                         "ApplyID=" & ViewState.Item("ApplyID"), _
                                                         "ApplyTime=" & Format(CDate(ViewState.Item("ApplyTime")), "yyyy/MM/dd HH:mm:ss"), _
                                                         "Seq=" & ViewState.Item("Seq"), _
                                                         "Status=" & ViewState.Item("Status"), _
                                                         "MainFlag=" & ViewState.Item("MainFlag"), _
                                                         "IsSignNext=" & IsSignNext, _
                                                         "GradeYear=" & strGradeYear, _
                                                         "GradeSeq=" & strGradeSeq, _
                                                         "Result=" & Result)
                                End If
                                If ViewState.Item("MainFlag") = "2" Then
                                    Dim btnD As New ButtonState(ButtonState.emButtonType.Download)
                                    Dim btnC1 As New ButtonState(ButtonState.emButtonType.Copy)
                                    btnD.Caption = "結果檔下傳"
                                    btnC1.Caption = "統計表"
                                    Me.TransferFramePage("~/GS/GS1400.aspx", New ButtonState() {btnD, btnC1}, _
                                                         "CompID=" & ViewState.Item("CompID"), _
                                                         "ApplyID=" & ViewState.Item("ApplyID"), _
                                                         "ApplyTime=" & Format(CDate(ViewState.Item("ApplyTime")), "yyyy/MM/dd HH:mm:ss"), _
                                                         "Seq=" & ViewState.Item("Seq"), _
                                                         "Status=" & ViewState.Item("Status"), _
                                                         "MainFlag=" & ViewState.Item("MainFlag"), _
                                                         "IsSignNext=" & IsSignNext, _
                                                         "GradeYear=" & strGradeYear, _
                                                         "GradeSeq=" & strGradeSeq, _
                                                         "Result=" & Result)
                                End If
                        End Select

                    End If
                Else
                    Return
                End If

            Catch ex As Exception
                lblMessage.Text = "錯誤訊息：" + ex.Message + "，如有其他疑問請與人力資源處聯繫。"
            End Try

        End If
        

    End Sub

End Class
