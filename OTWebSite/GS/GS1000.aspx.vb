'****************************************************
'功能說明：年度考核待辦清單
'建立人員：Micky Sung
'建立日期：2015.11.02
'****************************************************
Imports System.Data

Partial Class GS_GS1000
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objGS As New GS1()
            Using dt As DataTable = objGS.GetGradeParameter(UserProfile.CompID)
                If dt.Rows.Count > 0 Then
                    ViewState.Item("GradeYear") = dt.Rows.Item(0)("GradeYear").ToString()
                    ViewState.Item("GradeSeq") = dt.Rows.Item(0)("GradeSeq").ToString()
                End If
            End Using

            DoQuery()
        End If
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)
            If ht.ContainsKey("PageNo") Then pcMain.PageNo = Convert.ToInt32(ht("PageNo"))
        End If
    End Sub

    Private Sub DoQuery()
        Dim objGS As New GS1()
        gvMain.Visible = True

        Try
            pcMain.DataTable = objGS.QueryGrade()

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
        End Try
    End Sub

    Protected Sub gvMain_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMain.RowCommand
        If e.CommandName = "Detail" Then
            Select Case gvMain.DataKeys(selectedRow(gvMain))("Status").ToString()
                Case "1", "2"
                    If gvMain.DataKeys(selectedRow(gvMain))("MainFlag").ToString() = "1" And gvMain.DataKeys(selectedRow(gvMain))("IsSignNext").ToString() = "Y" Then
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
                        If gvMain.DataKeys(selectedRow(gvMain))("Result").ToString() = "1" Or gvMain.DataKeys(selectedRow(gvMain))("Status").ToString() = "2" Then
                            btnU.Visible = False
                            btnS.Visible = False
                            btnE.Visible = False
                            btnC.Visible = False
                        End If
                        Me.TransferFramePage("~/GS/GS1100.aspx", New ButtonState() {btnD, btnU, btnS, btnE, btnO, btnC}, _
                                             "CompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
                                             "ApplyID=" & gvMain.DataKeys(selectedRow(gvMain))("ApplyID").ToString(), _
                                             "ApplyTime=" & Format(CDate(gvMain.DataKeys(selectedRow(gvMain))("ApplyTime").ToString()), "yyyy/MM/dd HH:mm:ss"), _
                                             "Seq=" & gvMain.DataKeys(selectedRow(gvMain))("Seq").ToString(), _
                                             "Status=" & gvMain.DataKeys(selectedRow(gvMain))("Status").ToString(), _
                                             "MainFlag=" & gvMain.DataKeys(selectedRow(gvMain))("MainFlag").ToString(), _
                                             "IsSignNext=" & gvMain.DataKeys(selectedRow(gvMain))("IsSignNext").ToString(), _
                                             "GradeYear=" & ViewState.Item("GradeYear"), _
                                             "GradeSeq=" & ViewState.Item("GradeSeq"), _
                                             "Result=" & gvMain.DataKeys(selectedRow(gvMain))("Result").ToString())
                    End If
                    If gvMain.DataKeys(selectedRow(gvMain))("MainFlag").ToString() = "1" And gvMain.DataKeys(selectedRow(gvMain))("IsSignNext").ToString() = "N" Then
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
                        If gvMain.DataKeys(selectedRow(gvMain))("Result").ToString() = "1" Or gvMain.DataKeys(selectedRow(gvMain))("Status").ToString() = "2" Then
                            btnU.Visible = False
                            btnS.Visible = False
                            btnE.Visible = False
                            btnC.Visible = False
                        End If
                        Me.TransferFramePage("~/GS/GS1100.aspx", New ButtonState() {btnD, btnU, btnS, btnE, btnO, btnC, btnC1}, _
                                             "CompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
                                             "ApplyID=" & gvMain.DataKeys(selectedRow(gvMain))("ApplyID").ToString(), _
                                             "ApplyTime=" & Format(CDate(gvMain.DataKeys(selectedRow(gvMain))("ApplyTime").ToString()), "yyyy/MM/dd HH:mm:ss"), _
                                             "Seq=" & gvMain.DataKeys(selectedRow(gvMain))("Seq").ToString(), _
                                             "Status=" & gvMain.DataKeys(selectedRow(gvMain))("Status").ToString(), _
                                             "MainFlag=" & gvMain.DataKeys(selectedRow(gvMain))("MainFlag").ToString(), _
                                             "IsSignNext=" & gvMain.DataKeys(selectedRow(gvMain))("IsSignNext").ToString(), _
                                             "GradeYear=" & ViewState.Item("GradeYear"), _
                                             "GradeSeq=" & ViewState.Item("GradeSeq"), _
                                             "Result=" & gvMain.DataKeys(selectedRow(gvMain))("Result").ToString())
                    End If
                    If gvMain.DataKeys(selectedRow(gvMain))("MainFlag").ToString() = "2" Then
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
                        If gvMain.DataKeys(selectedRow(gvMain))("Result").ToString() = "1" Or gvMain.DataKeys(selectedRow(gvMain))("Status").ToString() = "2" Then
                            btnU.Visible = False
                            btnS.Visible = False
                            btnE.Visible = False
                            btnC.Visible = False
                        End If
                        Dim objGS As New GS1()
                        Dim strWhere As String = ""
                        Dim objHR As New HR()
                        Dim DeptEx As String = "N"
                        '判斷是否全排序
                        strWhere = " And CompID = " & Bsp.Utility.Quote(gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString())
                        strWhere = strWhere & " And (DeptID=" & Bsp.Utility.Quote(gvMain.DataKeys(selectedRow(gvMain))("ApplyID").ToString()) & " Or DeptID In (Select UpOrganID From GradeBase Where CompID =" & Bsp.Utility.Quote(gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString()) & " and GradeYear=" & ViewState.Item("GradeYear") & " and GradeSeq=" & ViewState.Item("GradeSeq") & " and GradeDeptID=" & Bsp.Utility.Quote(gvMain.DataKeys(selectedRow(gvMain))("ApplyID").ToString()) & "))"
                        If objHR.IsDataExists("GradeDeptException", strWhere) Then
                            DeptEx = "Y"
                        Else
                            DeptEx = "N"
                        End If
                        If objGS.CheckSignLog(gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), gvMain.DataKeys(selectedRow(gvMain))("ApplyID").ToString(), _
                                              Format(CDate(gvMain.DataKeys(selectedRow(gvMain))("ApplyTime").ToString()), "yyyy/MM/dd HH:mm:ss"), gvMain.DataKeys(selectedRow(gvMain))("Seq").ToString(), _
                                              ViewState.Item("GradeYear"), ViewState.Item("GradeSeq"), DeptEx) Then
                            btnU.Visible = False
                            btnS.Visible = False
                            btnE.Visible = False
                            btnO.Visible = False
                            btnC.Visible = False
                            btnC1.Visible = False
                        End If
                        Me.TransferFramePage("~/GS/GS1200.aspx", New ButtonState() {btnD, btnU, btnS, btnE, btnO, btnC, btnC1}, _
                                             "CompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
                                             "ApplyID=" & gvMain.DataKeys(selectedRow(gvMain))("ApplyID").ToString(), _
                                             "ApplyTime=" & Format(CDate(gvMain.DataKeys(selectedRow(gvMain))("ApplyTime").ToString()), "yyyy/MM/dd HH:mm:ss"), _
                                             "Seq=" & gvMain.DataKeys(selectedRow(gvMain))("Seq").ToString(), _
                                             "Status=" & gvMain.DataKeys(selectedRow(gvMain))("Status").ToString(), _
                                             "MainFlag=" & gvMain.DataKeys(selectedRow(gvMain))("MainFlag").ToString(), _
                                             "IsSignNext=" & gvMain.DataKeys(selectedRow(gvMain))("IsSignNext").ToString(), _
                                             "GradeYear=" & ViewState.Item("GradeYear"), _
                                             "GradeSeq=" & ViewState.Item("GradeSeq"), _
                                             "Result=" & gvMain.DataKeys(selectedRow(gvMain))("Result").ToString())
                    End If
                Case "4", "5"
                    If gvMain.DataKeys(selectedRow(gvMain))("MainFlag").ToString() = "1" And gvMain.DataKeys(selectedRow(gvMain))("IsSignNext").ToString() = "Y" Then
                        Dim btnD As New ButtonState(ButtonState.emButtonType.Download)
                        btnD.Caption = "結果檔下傳"
                        Me.TransferFramePage("~/GS/GS1300.aspx", New ButtonState() {btnD}, _
                                             "CompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
                                             "ApplyID=" & gvMain.DataKeys(selectedRow(gvMain))("ApplyID").ToString(), _
                                             "ApplyTime=" & Format(CDate(gvMain.DataKeys(selectedRow(gvMain))("ApplyTime").ToString()), "yyyy/MM/dd HH:mm:ss"), _
                                             "Seq=" & gvMain.DataKeys(selectedRow(gvMain))("Seq").ToString(), _
                                             "Status=" & gvMain.DataKeys(selectedRow(gvMain))("Status").ToString(), _
                                             "MainFlag=" & gvMain.DataKeys(selectedRow(gvMain))("MainFlag").ToString(), _
                                             "IsSignNext=" & gvMain.DataKeys(selectedRow(gvMain))("IsSignNext").ToString(), _
                                             "GradeYear=" & ViewState.Item("GradeYear"), _
                                             "GradeSeq=" & ViewState.Item("GradeSeq"), _
                                             "Result=" & gvMain.DataKeys(selectedRow(gvMain))("Result").ToString())
                    End If
                    If gvMain.DataKeys(selectedRow(gvMain))("MainFlag").ToString() = "1" And gvMain.DataKeys(selectedRow(gvMain))("IsSignNext").ToString() = "N" Then
                        Dim btnD As New ButtonState(ButtonState.emButtonType.Download)
                        Dim btnC1 As New ButtonState(ButtonState.emButtonType.Copy)
                        btnD.Caption = "結果檔下傳"
                        btnC1.Caption = "統計表"
                        Me.TransferFramePage("~/GS/GS1300.aspx", New ButtonState() {btnD, btnC1}, _
                                             "CompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
                                             "ApplyID=" & gvMain.DataKeys(selectedRow(gvMain))("ApplyID").ToString(), _
                                             "ApplyTime=" & Format(CDate(gvMain.DataKeys(selectedRow(gvMain))("ApplyTime").ToString()), "yyyy/MM/dd HH:mm:ss"), _
                                             "Seq=" & gvMain.DataKeys(selectedRow(gvMain))("Seq").ToString(), _
                                             "Status=" & gvMain.DataKeys(selectedRow(gvMain))("Status").ToString(), _
                                             "MainFlag=" & gvMain.DataKeys(selectedRow(gvMain))("MainFlag").ToString(), _
                                             "IsSignNext=" & gvMain.DataKeys(selectedRow(gvMain))("IsSignNext").ToString(), _
                                             "GradeYear=" & ViewState.Item("GradeYear"), _
                                             "GradeSeq=" & ViewState.Item("GradeSeq"), _
                                             "Result=" & gvMain.DataKeys(selectedRow(gvMain))("Result").ToString())
                    End If
                    If gvMain.DataKeys(selectedRow(gvMain))("MainFlag").ToString() = "2" Then
                        Dim btnD As New ButtonState(ButtonState.emButtonType.Download)
                        Dim btnC1 As New ButtonState(ButtonState.emButtonType.Copy)
                        btnD.Caption = "結果檔下傳"
                        btnC1.Caption = "統計表"
                        Me.TransferFramePage("~/GS/GS1400.aspx", New ButtonState() {btnD, btnC1}, _
                                             "CompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
                                             "ApplyID=" & gvMain.DataKeys(selectedRow(gvMain))("ApplyID").ToString(), _
                                             "ApplyTime=" & Format(CDate(gvMain.DataKeys(selectedRow(gvMain))("ApplyTime").ToString()), "yyyy/MM/dd HH:mm:ss"), _
                                             "Seq=" & gvMain.DataKeys(selectedRow(gvMain))("Seq").ToString(), _
                                             "Status=" & gvMain.DataKeys(selectedRow(gvMain))("Status").ToString(), _
                                             "MainFlag=" & gvMain.DataKeys(selectedRow(gvMain))("MainFlag").ToString(), _
                                             "IsSignNext=" & gvMain.DataKeys(selectedRow(gvMain))("IsSignNext").ToString(), _
                                             "GradeYear=" & ViewState.Item("GradeYear"), _
                                             "GradeSeq=" & ViewState.Item("GradeSeq"), _
                                             "Result=" & gvMain.DataKeys(selectedRow(gvMain))("Result").ToString())
                    End If
            End Select
        End If
    End Sub

End Class
