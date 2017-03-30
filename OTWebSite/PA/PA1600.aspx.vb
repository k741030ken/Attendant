'****************************************************
'功能說明：職等對照設定
'建立人員：MickySung
'建立日期：2015.05.04
'****************************************************
Imports System.Data

Partial Class PA_PA1600
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objPA As New PA1()
        Dim objSC As New SC()

        If Not IsPostBack Then
            If UserProfile.SelectCompRoleID = "ALL" Then
                '公司代碼
                lblCompRoleID.Visible = False
                Bsp.Utility.FillHRCompany(ddlCompID)
                Page.SetFocus(ddlCompID)

                '公司職等
                PA1.FillRankIDFromRank_PA1600(ddlRankID, ddlCompID.SelectedItem.Value)
                ddlRankID.Items.Insert(0, New ListItem("---請選擇---", ""))

                '對照職等
                PA1.FillRankIDMap_PA1600(ddlRankIDMap, ddlCompID.SelectedItem.Value)
                ddlRankIDMap.Items.Insert(0, New ListItem("---請選擇---", ""))
            Else
                '2015/05/28 公司代碼-名稱改寫法
                lblCompRoleID.Text = UserProfile.SelectCompRoleName
                'lblCompRoleID.Text = UserProfile.SelectCompRoleID + "-" + objSC.GetCompName(UserProfile.SelectCompRoleID).Rows(0).Item("CompName").ToString
                ddlCompID.Visible = False

                '公司職等
                PA1.FillRankIDFromRank_PA1600(ddlRankID, UserProfile.SelectCompRoleID)
                ddlRankID.Items.Insert(0, New ListItem("---請選擇---", ""))

                '對照職等
                PA1.FillRankIDMap_PA1600(ddlRankIDMap, UserProfile.SelectCompRoleID)
                ddlRankIDMap.Items.Insert(0, New ListItem("---請選擇---", ""))
            End If
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


            If UserProfile.SelectCompRoleID = "ALL" Then
                If ddlCompID.SelectedValue <> "" Then
                    '公司職等
                    PA1.FillRankIDFromRank_PA1600(ddlRankID, ddlCompID.SelectedItem.Value)
                    ddlRankID.Items.Insert(0, New ListItem("---請選擇---", ""))
                    UpdRankID.Update()

                    '對照職等
                    PA1.FillRankIDMap_PA1600(ddlRankIDMap, ddlCompID.SelectedItem.Value)
                    ddlRankIDMap.Items.Insert(0, New ListItem("---請選擇---", ""))
                    UpdRankIDMap.Update()
                End If
            Else
                '公司職等
                PA1.FillRankIDFromRank_PA1600(ddlRankID, UserProfile.SelectCompRoleID)
                ddlRankID.Items.Insert(0, New ListItem("---請選擇---", ""))
                UpdRankID.Update()

                '對照職等
                PA1.FillRankIDMap_PA1600(ddlRankIDMap, UserProfile.SelectCompRoleID)
                ddlRankIDMap.Items.Insert(0, New ListItem("---請選擇---", ""))
                UpdRankIDMap.Update()
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

        Me.TransferFramePage("~/PA/PA1601.aspx", New ButtonState() {btnA, btnX, btnC}, _
            ddlCompID.ID & "=" & strCompID, _
            ddlRankID.ID & "=" & ddlRankID.SelectedValue, _
            ddlRankIDMap.ID & "=" & ddlRankIDMap.SelectedValue, _
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

        Me.TransferFramePage("~/PA/PA1602.aspx", New ButtonState() {btnU, btnX, btnC}, _
            ddlCompID.ID & "=" & strCompID, _
            ddlRankID.ID & "=" & ddlRankID.SelectedValue, _
            ddlRankIDMap.ID & "=" & ddlRankIDMap.SelectedValue, _
            "PageNo=" & pcMain.PageNo.ToString(), _
            "SysID=" & arySysID(0), _
            "SelectedRankID=" & gvMain.DataKeys(selectedRow(gvMain))("RankID").ToString(), _
            "SelectedCompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
            "SelectedRankIDMap=" & gvMain.DataKeys(selectedRow(gvMain))("RankIDMap").ToString(), _
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
            pcMain.DataTable = objPA.RankMappingSettingQuery(
                "CompID=" & strCompID, _
                "RankID=" & ddlRankID.SelectedValue, _
                "RankIDMap=" & ddlRankIDMap.SelectedValue)

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

            Me.TransferFramePage("~/PA/PA1602.aspx", New ButtonState() {btnX}, _
                ddlCompID.ID & "=" & strCompID, _
                ddlRankID.ID & "=" & ddlRankID.SelectedValue, _
                ddlRankIDMap.ID & "=" & ddlRankIDMap.SelectedValue, _
                "PageNo=" & pcMain.PageNo.ToString(), _
                "SelectedRankID=" & gvMain.DataKeys(selectedRow(gvMain))("RankID").ToString(), _
                "SelectedCompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
                "SelectedRankIDMap=" & gvMain.DataKeys(selectedRow(gvMain))("RankIDMap").ToString(), _
                "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))

        End If
    End Sub

    Private Sub DoClear()
        IsDoQuery.Value = ""
        ViewState.Item("DoQuery") = ""
        gvMain.Visible = False

        '公司代碼
        ddlCompID.SelectedValue = ""

        '公司職等
        ddlRankID.SelectedValue = ""

        '對照職等
        ddlRankIDMap.SelectedValue = ""
    End Sub

    Protected Sub ddlCompID_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlCompID.SelectedIndexChanged
        '公司職等
        PA1.FillRankIDFromRank_PA1600(ddlRankID, ddlCompID.SelectedItem.Value)
        ddlRankID.Items.Insert(0, New ListItem("---請選擇---", ""))
        UpdRankID.Update()

        '對照職等
        PA1.FillRankIDMap_PA1600(ddlRankIDMap, ddlCompID.SelectedItem.Value)
        ddlRankIDMap.Items.Insert(0, New ListItem("---請選擇---", ""))
        UpdRankIDMap.Update()
    End Sub

End Class
