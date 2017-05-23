'****************************************************
'功能說明：流程種類參數設定
'建立人員：John
'建立日期：2017.05.02
'****************************************************
Imports System.Data
Partial Class ATWF_ATWF20
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            '出勤類別
            Bsp.Utility.FillDDL(ddlSystemID, "AattendantDB", "AT_CodeMap", "RTrim(Code)", "RTrim(CodeCName)", Bsp.Utility.DisplayType.Full, "", " AND TabName = 'HRFlowEngine' AND FldName = 'SystemID'", "")
            ddlSystemID.Items.Insert(0, New ListItem("---請選擇---", ""))

            '流程代碼
            Bsp.Utility.FillDDL(ddlFlowCode, "AattendantDB", "AT_CodeMap", "RTrim(Code)", "RTrim(CodeCName)", Bsp.Utility.DisplayType.Full, "", " AND TabName = 'HRFlowEngine' AND FldName = 'FlowCode'", "")
            ddlFlowCode.Items.Insert(0, New ListItem("---請選擇---", ""))

            '流程種類
            ATWF20.FillFlowType(ddlFlowType, "", "")
            ddlFlowType.Items.Insert(0, New ListItem("---請選擇---", ""))
        End If

    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnUpdate"    '修改
                DoUpdate()
            Case "btnQuery"     '查詢
                ViewState.Item("DoQuery") = "Y"
                DoQuery()
        End Select
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

            For Each strKey As String In ht.Keys
                Dim ctl As Control = Me.FindControl(strKey)

                If TypeOf ctl Is DropDownList Then
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

    Private Sub DoUpdate()
        Dim btnU As New ButtonState(ButtonState.emButtonType.Update)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
        Dim btnC As New ButtonState(ButtonState.emButtonType.Cancel)

        btnU.Caption = "存檔返回"
        btnX.Caption = "返回"
        btnC.Caption = "清除"

        Me.TransferFramePage("~/ATWF/ATWF22.aspx", New ButtonState() {btnU, btnX, btnC}, _
            ddlSystemID.ID & "=" & ddlSystemID.SelectedValue, _
            ddlFlowCode.ID & "=" & ddlFlowCode.SelectedValue, _
            ddlFlowType.ID & "=" & ddlFlowType.SelectedValue, _
            "PageNo=" & pcMain.PageNo.ToString(), _
            "SelectedSystemID=" & gvMain.DataKeys(selectedRow(gvMain))("SystemID").ToString(), _
            "SelectedFlowCode=" & gvMain.DataKeys(selectedRow(gvMain))("FlowCode").ToString(), _
            "SelectedFlowType=" & gvMain.DataKeys(selectedRow(gvMain))("FlowType").ToString(), _
            "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
    End Sub

    Private Sub DoQuery()
        Dim objATWF20 As New ATWF20
        gvMain.Visible = True

        Try
            pcMain.DataTable = objATWF20.HRFlowTypeDefineQuery(
                "SystemID=" & ddlSystemID.SelectedValue, _
                "FlowCode=" & ddlFlowCode.SelectedValue, _
                "FlowType=" & ddlFlowType.SelectedValue)

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
        End Try
    End Sub

    Protected Sub ddlSystemID_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlSystemID.SelectedIndexChanged
        '流程代碼
        ATWF20.FillFlowCode(ddlFlowCode, ddlSystemID.SelectedItem.Value)
        ddlFlowCode.Items.Insert(0, New ListItem("---請選擇---", ""))

        '流程種類
        ATWF20.FillFlowType(ddlFlowType, ddlSystemID.SelectedItem.Value, ddlFlowCode.SelectedItem.Value)
        ddlFlowType.Items.Insert(0, New ListItem("---請選擇---", ""))
    End Sub

    Protected Sub ddlFlowCode_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlFlowCode.SelectedIndexChanged
        '流程種類
        ATWF20.FillFlowType(ddlFlowType, ddlSystemID.SelectedItem.Value, ddlFlowCode.SelectedItem.Value)
        ddlFlowType.Items.Insert(0, New ListItem("---請選擇---", ""))
    End Sub
End Class
