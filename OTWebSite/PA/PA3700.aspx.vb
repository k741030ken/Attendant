'****************************************************
'功能說明：員工任職狀態與異動原因對應表
'建立人員：Micky Sung
'建立日期：2015.07.31
'****************************************************
Imports System.Data

Partial Class PA_PA3700
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            '異動前任職狀況
            Bsp.Utility.FillDDL(ddlBeforeWorkStatus, "eHRMSDB", "WorkStatus_EmployeeReason", "distinct BeforeWorkStatus", "Remark", Bsp.Utility.DisplayType.Full, " left join WorkStatus on BeforeWorkStatus = WorkCode", "", "")
            ddlBeforeWorkStatus.Items.Insert(0, New ListItem("---請選擇---", ""))

            '可輸入異動原因
            Bsp.Utility.FillDDL(ddlReason, "eHRMSDB", "WorkStatus_EmployeeReason WE", "distinct WE.Reason", "E.Remark", Bsp.Utility.DisplayType.Full, " left join EmployeeReason E on WE.Reason = E.Reason", "", "")
            ddlReason.Items.Insert(0, New ListItem("---請選擇---", ""))

            '異動後任職狀況
            Bsp.Utility.FillDDL(ddlAfterWorkStatusType, "eHRMSDB", "WorkStatus_EmployeeReason WE", "distinct WE.AfterWorkStatusType", "W1.Remark", Bsp.Utility.DisplayType.Full, " left join WorkStatus W1 on WE.AfterWorkStatusType = W1.WorkCode ", "", "")
            ddlAfterWorkStatusType.Items.Insert(0, New ListItem("---請選擇---", ""))
        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnQuery"     '查詢
                ViewState.Item("DoQuery") = "Y"
                DoQuery()
            Case "btnActionX"   '清除
                DoClear()
        End Select
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

            For Each strKey As String In ht.Keys
                Dim ctl As Control = Me.FindControl(strKey)
                If TypeOf ctl Is TextBox Then
                    CType(ctl, TextBox).Text = ht(strKey).ToString()
                End If
                If TypeOf ctl Is DropDownList Then
                    If ht(strKey).ToString <> "" Then
                        CType(ctl, DropDownList).SelectedValue = ht(strKey).ToString
                    End If
                End If
            Next
            'If ht.ContainsKey("PageNo") Then pcMain.PageNo = Convert.ToInt32(ht("PageNo"))
            'If ht.ContainsKey("DoQuery") Then
            '    If ht("DoQuery").ToString() = "Y" Then
            '        ViewState.Item("DoQuery") = "Y"
            '        DoQuery()
            '    End If
            'End If
        End If
    End Sub

    Private Sub DoQuery()
        Dim objPA As New PA3()
        gvMain.Visible = True

        Try
            pcMain.DataTable = objPA.WorkStatus_EmployeeReasonQuery(
                "BeforeWorkStatusType=" & ddlBeforeWorkStatusType.SelectedValue, _
                "BeforeWorkStatus=" & ddlBeforeWorkStatus.SelectedValue, _
                "Reason=" & ddlReason.SelectedValue, _
                "AfterWorkStatusType=" & ddlAfterWorkStatusType.SelectedValue)

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
        End Try
    End Sub

    Private Sub DoClear()
        IsDoQuery.Value = ""
        ViewState.Item("DoQuery") = ""
        gvMain.Visible = False

        '異動前任職分類
        ddlBeforeWorkStatusType.SelectedValue = ""

        '異動前任職狀況
        ddlBeforeWorkStatus.SelectedValue = ""

        '可輸入異動原因
        ddlReason.SelectedValue = ""

        '異動後任職狀況
        ddlAfterWorkStatusType.SelectedValue = ""
    End Sub

End Class
