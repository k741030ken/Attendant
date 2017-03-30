'****************************************************
'功能說明：工作地點設定
'建立人員：MickySung
'建立日期：2015.05.14
'****************************************************
Imports System.Data

Partial Class PA_PA1900
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

                '工作地點代碼
                PA1.FillWorkSiteID_PA1900(ddlWorkSiteID, ddlCompID.SelectedItem.Value)
                ddlWorkSiteID.Items.Insert(0, New ListItem("---請選擇---", ""))
            Else
                '2015/05/28 公司代碼-名稱改寫法
                lblCompRoleID.Text = UserProfile.SelectCompRoleName
                'lblCompRoleID.Text = UserProfile.SelectCompRoleID + "-" + objSC.GetCompName(UserProfile.SelectCompRoleID).Rows(0).Item("CompName").ToString
                ddlCompID.Visible = False

                '工作地點代碼
                PA1.FillWorkSiteID_PA1900(ddlWorkSiteID, UserProfile.SelectCompRoleID)
                ddlWorkSiteID.Items.Insert(0, New ListItem("---請選擇---", ""))
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

            '公司職等
            If UserProfile.SelectCompRoleID = "ALL" Then
                If ddlCompID.SelectedValue <> "" Then
                    PA1.FillWorkSiteID_PA1900(ddlWorkSiteID, ddlCompID.SelectedItem.Value)
                    ddlWorkSiteID.Items.Insert(0, New ListItem("---請選擇---", ""))
                    UpdWorkSiteID.Update()
                End If
            Else
                PA1.FillWorkSiteID_PA1900(ddlWorkSiteID, UserProfile.SelectCompRoleID)
                ddlWorkSiteID.Items.Insert(0, New ListItem("---請選擇---", ""))
                UpdWorkSiteID.Update()
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

        Me.TransferFramePage("~/PA/PA1901.aspx", New ButtonState() {btnA, btnX, btnC}, _
            ddlCompID.ID & "=" & strCompID, _
            ddlWorkSiteID.ID & "=" & ddlWorkSiteID.SelectedValue, _
            txtRemark.ID & "=" & txtRemark.Text, _
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

        Me.TransferFramePage("~/PA/PA1902.aspx", New ButtonState() {btnU, btnX, btnC}, _
            ddlCompID.ID & "=" & strCompID, _
            ddlWorkSiteID.ID & "=" & ddlWorkSiteID.SelectedValue, _
            txtRemark.ID & "=" & txtRemark.Text, _
            "PageNo=" & pcMain.PageNo.ToString(), _
            "SysID=" & arySysID(0), _
            "SelectedWorkSiteID=" & gvMain.DataKeys(selectedRow(gvMain))("WorkSiteID").ToString(), _
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
            pcMain.DataTable = objPA.WorkSiteSettingQuery(
                "CompID=" & strCompID, _
                "WorkSiteID=" & ddlWorkSiteID.SelectedValue, _
                "Remark=" & txtRemark.Text)

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
        End Try
    End Sub

    Private Sub DoDelete()
        If selectedRow(gvMain) < 0 Then
            '尚未選取資料列！
            Bsp.Utility.ShowFormatMessage(Me, "W_00000")
        Else
            Dim beWorkSite As New beWorkSite.Row()
            Dim objPA As New PA1()
            Dim strSysID As String

            strSysID = Bsp.Utility.subGetSysID(UserProfile.LoginSysID)
            Dim arySysID() As String = Split(strSysID, "-")

            Dim strCompID As String
            If UserProfile.SelectCompRoleID = "ALL" Then
                strCompID = ddlCompID.SelectedValue
            Else
                strCompID = UserProfile.SelectCompRoleID
            End If
            beWorkSite.CompID.Value = strCompID
            beWorkSite.WorkSiteID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("WorkSiteID").ToString()

            '查詢人事主檔是否有使用工作地點資料
            Dim PersonalTable As DataTable
            PersonalTable = objPA.checkPersonal_PA1900(strCompID, gvMain.DataKeys(Me.selectedRow(gvMain))("WorkSiteID").ToString())

            If PersonalTable.Rows(0).Item(0).ToString() > 0 Then
                Bsp.Utility.ShowMessage(Me, "人事主檔有使用工作地點資料，不可刪除")
            Else
                Try
                    objPA.DeleteWorkSiteSetting(beWorkSite)
                Catch ex As Exception
                    Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoDelete", ex)
                End Try
                gvMain.DataBind()

                DoQuery()
            End If

            '2015/05/25 工作地點代碼 下拉選單重新讀取
            PA1.FillWorkSiteID_PA1900(ddlWorkSiteID, UserProfile.SelectCompRoleID)
            ddlWorkSiteID.Items.Insert(0, New ListItem("---請選擇---", ""))

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

            Me.TransferFramePage("~/PA/PA1902.aspx", New ButtonState() {btnX}, _
                ddlCompID.ID & "=" & strCompID, _
                ddlWorkSiteID.ID & "=" & ddlWorkSiteID.SelectedValue, _
                txtRemark.ID & "=" & txtRemark.Text, _
                "PageNo=" & pcMain.PageNo.ToString(), _
                "SelectedWorkSiteID=" & gvMain.DataKeys(selectedRow(gvMain))("WorkSiteID").ToString(), _
                "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))

        End If
    End Sub

    Private Sub DoClear()
        IsDoQuery.Value = ""
        ViewState.Item("DoQuery") = ""
        gvMain.Visible = False

        '公司代碼
        ddlCompID.SelectedValue = ""

        '工作地點代碼
        ddlWorkSiteID.SelectedValue = ""

        '工作地點
        txtRemark.Text = ""
    End Sub

    Protected Sub ddlCompID_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlCompID.SelectedIndexChanged
        '工作地點代碼
        PA1.FillWorkSiteID_PA1900(ddlWorkSiteID, ddlCompID.SelectedItem.Value)
        ddlWorkSiteID.Items.Insert(0, New ListItem("---請選擇---", ""))
        UpdWorkSiteID.Update()
    End Sub

End Class
