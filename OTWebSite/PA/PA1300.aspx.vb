'****************************************************
'功能說明：職等代碼設定
'建立人員：MickySung
'建立日期：2015.04.27
'****************************************************
Imports System.Data

Partial Class PA_PA1300
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objPA As New PA1()
        Dim objSC As New SC()

        If Not IsPostBack Then
            '公司代碼下拉選單
            If UserProfile.SelectCompRoleID = "ALL" Then
                lblCompRoleID.Visible = False
                Bsp.Utility.FillHRCompany(ddlCompID)
                Page.SetFocus(ddlCompID)

                '職等代碼
                '2015/05/20 職等代碼隨公司代號改變
                PA1.FillRank(ddlRankID, ddlCompID.SelectedValue)
                ddlRankID.Items.Insert(0, New ListItem("---請選擇---", ""))
            Else
                '2015/05/28 公司代碼-名稱改寫法
                lblCompRoleID.Text = UserProfile.SelectCompRoleName
                'lblCompRoleID.Text = UserProfile.SelectCompRoleID + "-" + objSC.GetCompName(UserProfile.SelectCompRoleID).Rows(0).Item("CompName").ToString
                ddlCompID.Visible = False

                '職等代碼
                '2015/05/20 職等代碼隨公司代號改變
                PA1.FillRank(ddlRankID, UserProfile.SelectCompRoleID)
                ddlRankID.Items.Insert(0, New ListItem("---請選擇---", ""))
            End If

            ''職等代碼下拉選單
            'PA1.FillRank(ddlRankID)
            'ddlRankID.Items.Insert(0, New ListItem("---請選擇---", ""))
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
                End If
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

        Me.TransferFramePage("~/PA/PA1301.aspx", New ButtonState() {btnA, btnX, btnC}, _
            ddlCompID.ID & "=" & strCompID, _
            ddlRankID.ID & "=" & ddlRankID.SelectedValue, _
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

        Me.TransferFramePage("~/PA/PA1302.aspx", New ButtonState() {btnU, btnX, btnC}, _
            ddlCompID.ID & "=" & strCompID, _
            ddlRankID.ID & "=" & ddlRankID.SelectedValue, _
            "PageNo=" & pcMain.PageNo.ToString(), _
            "SysID=" & arySysID(0), _
            "SelectedRankID=" & gvMain.DataKeys(selectedRow(gvMain))("RankID").ToString(), _
            "SelectedCompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
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
            pcMain.DataTable = objPA.RankSettingQuery(
                "CompID=" & strCompID, _
                "RankID=" & ddlRankID.SelectedValue)

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
        End Try
    End Sub

    Private Sub DoDelete()
        If selectedRow(gvMain) < 0 Then
            '尚未選取資料列！
            Bsp.Utility.ShowFormatMessage(Me, "W_00000")
        Else
            Dim beRank As New beRank.Row()
            Dim objPA As New PA1()
            Dim strSysID As String

            Dim beCompareRank As New beCompareRank.Row()
            Dim beRankMapping As New beRankMapping.Row()

            strSysID = Bsp.Utility.subGetSysID(UserProfile.LoginSysID)
            Dim arySysID() As String = Split(strSysID, "-")

            beRank.RankID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("RankID").ToString()
            beRank.CompID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("CompID").ToString()

            beCompareRank.HoldingRankID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("RankID").ToString()
            beCompareRank.CompID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("CompID").ToString()
            beCompareRank.RankID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("RankID").ToString()
            
            beRankMapping.CompID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("CompID").ToString()
            beRankMapping.RankID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("RankID").ToString()
            beRankMapping.RankIDMap.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("RankID").ToString()

            Dim TitleTable As DataTable
            TitleTable = objPA.checkTitle(gvMain.DataKeys(Me.selectedRow(gvMain))("CompID").ToString(), gvMain.DataKeys(Me.selectedRow(gvMain))("RankID").ToString())

            Dim PersonalTable As DataTable
            PersonalTable = objPA.checkPersonal_PA1300(gvMain.DataKeys(Me.selectedRow(gvMain))("CompID").ToString(), gvMain.DataKeys(Me.selectedRow(gvMain))("RankID").ToString())

            If TitleTable.Rows(0).Item(0).ToString() > 0 Then
                Bsp.Utility.ShowMessage(Me, "職稱代碼檔仍有相關資料存在，請先刪除「職稱代碼檔」")
            ElseIf PersonalTable.Rows(0).Item(0).ToString() > 0 Then
                Bsp.Utility.ShowMessage(Me, "Personal人員檔已有使用此職等資料，不可刪除該職等")
            Else
                Try
                    Bsp.Utility.ShowMessage(Me, "「將連動刪除金控職等對照設定、職等比對設定」")
                    objPA.DeleteRankSetting(beRank, beCompareRank, beRankMapping)
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

            Me.TransferFramePage("~/PA/PA1302.aspx", New ButtonState() {btnX}, _
                ddlCompID.ID & "=" & strCompID, _
                ddlRankID.ID & "=" & ddlRankID.SelectedValue, _
                "PageNo=" & pcMain.PageNo.ToString(), _
                "SelectedRankID=" & gvMain.DataKeys(selectedRow(gvMain))("RankID").ToString(), _
                "SelectedCompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
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
    End Sub

    Protected Sub ddlCompID_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlCompID.SelectedIndexChanged
        PA1.FillRankID(ddlRankID, ddlCompID.SelectedItem.Value)
        ddlRankID.Items.Insert(0, New ListItem("---請選擇---", ""))
        UpdRankID.Update()
    End Sub
End Class
