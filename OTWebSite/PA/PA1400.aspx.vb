'****************************************************
'功能說明：職稱代碼設定
'建立人員：MickySung
'建立日期：2015.04.29
'****************************************************
Imports System.Data

Partial Class PA_PA1400
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
                PA1.FillRankID(ddlRankID, ddlCompID.SelectedValue)
                ddlRankID.Items.Insert(0, New ListItem("---請選擇---", ""))

                '職稱代碼
                PA1.FillTitleID(ddlTitleID, ddlCompID.SelectedItem.Value, ddlRankID.SelectedValue)
                ddlTitleID.Items.Insert(0, New ListItem("---請選擇---", ""))
            Else
                '2015/05/28 公司代碼-名稱改寫法
                lblCompRoleID.Text = UserProfile.SelectCompRoleName
                'lblCompRoleID.Text = UserProfile.SelectCompRoleID + "-" + objSC.GetCompName(UserProfile.SelectCompRoleID).Rows(0).Item("CompName").ToString
                ddlCompID.Visible = False

                '職等代碼
                'Bsp.Utility.Rank(ddlRankID, UserProfile.SelectCompRoleID)
                PA1.FillRankID(ddlRankID, UserProfile.SelectCompRoleID)
                ddlRankID.Items.Insert(0, New ListItem("---請選擇---", ""))

                '職稱代碼
                PA1.FillTitleID(ddlTitleID, UserProfile.SelectCompRoleID, ddlRankID.SelectedValue)
                ddlTitleID.Items.Insert(0, New ListItem("---請選擇---", ""))
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
            Case "btnDelete"    '刪除
                DoDelete()
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

            '職等代碼
            If ddlCompID.SelectedValue <> "" Then
                If UserProfile.SelectCompRoleID = "ALL" Then
                    PA1.FillRankID(ddlRankID, ddlCompID.SelectedItem.Value)
                Else
                    PA1.FillRankID(ddlRankID, UserProfile.SelectCompRoleID)
                End If
                ddlRankID.Items.Insert(0, New ListItem("---請選擇---", ""))
                UpdRankID.Update()
            End If

            '職稱代碼
            If ddlRankID.SelectedValue <> "" Then
                If UserProfile.SelectCompRoleID = "ALL" Then
                    PA1.FillTitleID(ddlTitleID, ddlCompID.SelectedItem.Value, ddlRankID.SelectedValue)
                Else
                    PA1.FillTitleID(ddlTitleID, UserProfile.SelectCompRoleID, ddlRankID.SelectedValue)
                End If
                ddlTitleID.Items.Insert(0, New ListItem("---請選擇---", ""))
                UpdTitleID.Update()
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

        Me.TransferFramePage("~/PA/PA1401.aspx", New ButtonState() {btnA, btnX, btnC}, _
            ddlCompID.ID & "=" & strCompID, _
            ddlRankID.ID & "=" & ddlRankID.SelectedValue, _
            ddlTitleID.ID & "=" & ddlTitleID.SelectedValue, _
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

        Me.TransferFramePage("~/PA/PA1402.aspx", New ButtonState() {btnU, btnX, btnC}, _
            ddlCompID.ID & "=" & strCompID, _
            ddlRankID.ID & "=" & ddlRankID.SelectedValue, _
            ddlTitleID.ID & "=" & ddlTitleID.SelectedValue, _
            "PageNo=" & pcMain.PageNo.ToString(), _
            "SysID=" & arySysID(0), _
            "SelectedRankID=" & gvMain.DataKeys(selectedRow(gvMain))("RankID").ToString(), _
            "SelectedCompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
            "SelectedTitleID=" & gvMain.DataKeys(selectedRow(gvMain))("TitleID").ToString(), _
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
            pcMain.DataTable = objPA.TitleSettingQuery(
                "CompID=" & strCompID, _
                "RankID=" & ddlRankID.SelectedValue, _
                "TitleID=" & ddlTitleID.SelectedValue)

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
        End Try
    End Sub

    Private Sub DoDelete()
        If selectedRow(gvMain) < 0 Then
            '尚未選取資料列！
            Bsp.Utility.ShowFormatMessage(Me, "W_00000")
        Else
            Dim beTitle As New beTitle.Row()
            Dim objPA As New PA1()
            Dim strSysID As String

            strSysID = Bsp.Utility.subGetSysID(UserProfile.LoginSysID)
            Dim arySysID() As String = Split(strSysID, "-")

            beTitle.RankID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("RankID").ToString()
            beTitle.CompID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("CompID").ToString()
            beTitle.TitleID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("TitleID").ToString()

            Dim PersonalTable As DataTable
            PersonalTable = objPA.checkPersonal_PA1400(gvMain.DataKeys(Me.selectedRow(gvMain))("CompID").ToString(), gvMain.DataKeys(Me.selectedRow(gvMain))("RankID").ToString(), gvMain.DataKeys(Me.selectedRow(gvMain))("TitleID").ToString())


            If PersonalTable.Rows(0).Item(0).ToString() > 0 Then
                Bsp.Utility.ShowMessage(Me, "Personal人員檔已有使用此職等職稱資料，不可刪除該職等職稱")
            Else
                Try
                    objPA.DeleteTitleSetting(beTitle)
                Catch ex As Exception
                    Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoDelete", ex)
                End Try
                gvMain.DataBind()

                DoQuery()
            End If

        End If
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

            Me.TransferFramePage("~/PA/PA1402.aspx", New ButtonState() {btnX}, _
                ddlCompID.ID & "=" & strCompID, _
                ddlRankID.ID & "=" & ddlRankID.SelectedValue, _
                ddlTitleID.ID & "=" & ddlTitleID.SelectedValue, _
                "PageNo=" & pcMain.PageNo.ToString(), _
                "SelectedRankID=" & gvMain.DataKeys(selectedRow(gvMain))("RankID").ToString(), _
                "SelectedCompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
                "SelectedTitleID=" & gvMain.DataKeys(selectedRow(gvMain))("TitleID").ToString(), _
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
        ddlTitleID.SelectedValue = ""
    End Sub

    Protected Sub ddlCompID_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlCompID.SelectedIndexChanged
        PA1.FillRankID(ddlRankID, ddlCompID.SelectedValue)
        ddlRankID.Items.Insert(0, New ListItem("---請選擇---", ""))
        UpdRankID.Update()
    End Sub

    Protected Sub ddlRankID_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlRankID.SelectedIndexChanged
        If UserProfile.SelectCompRoleID = "ALL" Then
            PA1.FillTitleID(ddlTitleID, ddlCompID.SelectedItem.Value, ddlRankID.SelectedValue)
        Else
            PA1.FillTitleID(ddlTitleID, UserProfile.SelectCompRoleID, ddlRankID.SelectedValue)
        End If

        ddlTitleID.Items.Insert(0, New ListItem("---請選擇---", ""))
        UpdTitleID.Update()
    End Sub

End Class
