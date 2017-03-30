'****************************************************
'功能說明：金控職等對照設定
'建立人員：MickySung
'建立日期：2015.04.30
'****************************************************
Imports System.Data

Partial Class PA_PA1500
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objPA As New PA1()
        Dim objSC As New SC()

        If Not IsPostBack Then
            If UserProfile.SelectCompRoleID = "ALL" Then
                lblCompRoleID.Visible = False
                Bsp.Utility.FillHRCompany(ddlCompID)
                Page.SetFocus(ddlCompID)

                '職等代碼
                'Bsp.Utility.Rank(ddlRankID, ddlCompID.SelectedItem.Value)
                PA1.FillRankID_PA1500(ddlRankID, ddlCompID.SelectedValue)
                ddlRankID.Items.Insert(0, New ListItem("---請選擇---", ""))
            Else
                '2015/05/28 公司代碼-名稱改寫法
                lblCompRoleID.Text = UserProfile.SelectCompRoleName
                'lblCompRoleID.Text = UserProfile.SelectCompRoleID + "-" + objSC.GetCompName(UserProfile.SelectCompRoleID).Rows(0).Item("CompName").ToString
                ddlCompID.Visible = False

                '職等代碼
                'Bsp.Utility.Rank(ddlRankID, UserProfile.SelectCompRoleID)
                PA1.FillRankID_PA1500(ddlRankID, UserProfile.SelectCompRoleID)
                ddlRankID.Items.Insert(0, New ListItem("---請選擇---", ""))
            End If

            '職稱代碼
            PA1.FillTitleByHolding(ddlHoldingRankID)
            ddlHoldingRankID.Items.Insert(0, New ListItem("---請選擇---", ""))

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
                ElseIf TypeOf ctl Is DropDownList Then
                    If ht(strKey).ToString <> "" Then
                        CType(ctl, DropDownList).SelectedValue = ht(strKey).ToString
                    End If
                End If
            Next

            '公司職等
            If UserProfile.SelectCompRoleID = "ALL" Then
                If ddlCompID.SelectedValue <> "" Then
                    PA1.FillRankID_PA1500(ddlRankID, ddlCompID.SelectedItem.Value)
                    ddlRankID.Items.Insert(0, New ListItem("---請選擇---", ""))
                    UpdRankID.Update()
                End If
            Else
                PA1.FillRankID_PA1500(ddlRankID, UserProfile.SelectCompRoleID)
                ddlRankID.Items.Insert(0, New ListItem("---請選擇---", ""))
                UpdRankID.Update()
            End If

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

        Me.TransferFramePage("~/PA/PA1501.aspx", New ButtonState() {btnA, btnX, btnC}, _
            ddlCompID.ID & "=" & strCompID, _
            ddlRankID.ID & "=" & ddlRankID.SelectedValue, _
            ddlHoldingRankID.ID & "=" & ddlHoldingRankID.SelectedValue, _
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

        Dim strSysID As String
        strSysID = Bsp.Utility.subGetSysID(UserProfile.LoginSysID)
        Dim arySysID() As String = Split(strSysID, "-")

        Dim strCompID As String
        If UserProfile.SelectCompRoleID = "ALL" Then
            strCompID = ddlCompID.SelectedValue
        Else
            strCompID = UserProfile.SelectCompRoleID
        End If

        Me.TransferFramePage("~/PA/PA1502.aspx", New ButtonState() {btnU, btnX, btnC}, _
            ddlCompID.ID & "=" & strCompID, _
            ddlRankID.ID & "=" & ddlRankID.SelectedValue, _
            ddlHoldingRankID.ID & "=" & ddlHoldingRankID.SelectedValue, _
            "PageNo=" & pcMain.PageNo.ToString(), _
            "SysID=" & arySysID(0), _
            "SelectedRankID=" & gvMain.DataKeys(selectedRow(gvMain))("RankID").ToString(), _
            "SelectedCompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
            "SelectedHoldingRankID=" & gvMain.DataKeys(selectedRow(gvMain))("HoldingRankID").ToString(), _
            "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
    End Sub

    Private Sub DoQuery()
        Dim objPA As New PA1()
        gvMain.Visible = True

        Dim strCompID As String
        If UserProfile.SelectCompRoleID = "ALL" Then
            strCompID = ddlCompID.SelectedValue
        Else
            strCompID = UserProfile.SelectCompRoleID
        End If

        Try
            pcMain.DataTable = objPA.CompareRankSettingQuery(
                "CompID=" & strCompID, _
                "RankID=" & ddlRankID.SelectedValue, _
                "HoldingRankID=" & ddlHoldingRankID.SelectedValue)

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
        End Try
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

            Me.TransferFramePage("~/PA/PA1502.aspx", New ButtonState() {btnX}, _
                ddlCompID.ID & "=" & strCompID, _
                ddlRankID.ID & "=" & ddlRankID.SelectedValue, _
                ddlHoldingRankID.ID & "=" & ddlHoldingRankID.SelectedValue, _
                "PageNo=" & pcMain.PageNo.ToString(), _
                "SelectedRankID=" & gvMain.DataKeys(selectedRow(gvMain))("RankID").ToString(), _
                "SelectedCompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
                "SelectedHoldingRankID=" & gvMain.DataKeys(selectedRow(gvMain))("HoldingRankID").ToString(), _
                "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))

        End If
    End Sub

    Private Sub DoClear()
        IsDoQuery.Value = ""
        ViewState.Item("DoQuery") = ""
        gvMain.Visible = False

        '公司代碼
        ddlCompID.SelectedValue = ""

        '職等代碼
        ddlRankID.SelectedValue = ""

        '職稱代碼
        ddlHoldingRankID.SelectedValue = ""
    End Sub

    Protected Sub ddlCompID_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlCompID.SelectedIndexChanged
        'Bsp.Utility.Rank(ddlRankID, ddlCompID.SelectedItem.Value)
        PA1.FillRankID_PA1500(ddlRankID, ddlCompID.SelectedValue)
        ddlRankID.Items.Insert(0, New ListItem("---請選擇---", ""))
        UpdRankID.Update()
    End Sub

End Class
