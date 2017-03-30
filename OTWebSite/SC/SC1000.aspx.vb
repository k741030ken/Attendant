'****************************************************
'功能說明：綜合查詢授權設定
'建立人員：BeatriceCheng
'建立日期：2015.11.23
'****************************************************
Imports System.Data

Partial Class SC_SC1000
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objSC As New SC

            ddlCompRoleID.Visible = False

            If UserProfile.SelectCompRoleID = "ALL" Then
                ddlCompRoleID.Visible = True
                Bsp.Utility.FillCompany(ddlCompRoleID)
                'ddlCompID.Items.Insert(0, New ListItem("全金控", "0"))
                lblCompRoleName.Visible = False

                Bsp.Utility.FillGroup_0501(ddlGroupID, ddlCompRoleID.SelectedValue)
                ddlGroupID.Items.Insert(0, New ListItem("---請選擇---", ""))
            Else
                ddlCompRoleID.Visible = False
                lblCompRoleName.Text = UserProfile.SelectCompRoleName
                lblCompRoleName.Visible = True

                Bsp.Utility.FillGroup(ddlGroupID)
                ddlGroupID.Items.Insert(0, New ListItem("---請選擇---", ""))
            End If

            Bsp.Utility.FillDDL(ddlQueryFlag, "eHRMSDB", "HRCodeMap", "Code", "CodeCName", Bsp.Utility.DisplayType.Full, "", " AND TabName = 'SC_GroupMutiQry' AND FldName = 'QueryID' AND NotShowFlag = '0'")
            ddlQueryFlag.Items.Insert(0, New ListItem("---請選擇---", ""))
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
                    CType(ctl, DropDownList).SelectedValue = ht(strKey).ToString()
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

        Me.TransferFramePage("~/SC/SC1001.aspx", New ButtonState() {btnA, btnX, btnC}, _
            "ddlGroupID=" & ddlGroupID.SelectedValue, _
            "ddlQueryFlag=" & ddlQueryFlag.SelectedValue, _
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

        Me.TransferFramePage("~/SC/SC1002.aspx", New ButtonState() {btnU, btnX, btnC}, _
            "ddlGroupID=" & ddlGroupID.SelectedValue, _
            "ddlQueryFlag=" & ddlQueryFlag.SelectedValue, _
            "PageNo=" & pcMain.PageNo.ToString(), _
            "SelectedCompRoleID=" & gvMain.DataKeys(selectedRow(gvMain))("CompRoleID").ToString(), _
            "SelectedGroupID=" & gvMain.DataKeys(selectedRow(gvMain))("GroupID").ToString(), _
            "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
    End Sub

    Private Sub DoQuery()
        Dim objSC As New SC()

        Dim strCompID As String = ddlCompRoleID.SelectedValue
        If strCompID = "" Then
            strCompID = UserProfile.SelectCompRoleID
        End If

        Try
            pcMain.DataTable = objSC.SC1000_Query(
                "CompRoleID=" & strCompID, _
                "GroupID=" & ddlGroupID.SelectedValue, _
                "QueryFlag=" & ddlQueryFlag.SelectedValue)
            gvMain.Visible = True

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
        End Try
    End Sub

    Private Sub DoDelete()
        If selectedRow(gvMain) < 0 Then
            '尚未選取資料列！
            Bsp.Utility.ShowFormatMessage(Me, "W_00000")
        Else
            Dim beSC_GroupMutiQry As New beSC_GroupMutiQry.Row()
            Dim objSC As New SC
            beSC_GroupMutiQry.SysID.Value = UserProfile.LoginSysID
            beSC_GroupMutiQry.CompRoleID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("CompRoleID").ToString()
            beSC_GroupMutiQry.GroupID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("GroupID").ToString()

            Try
                objSC.SC1000_Delete(beSC_GroupMutiQry)
            Catch ex As Exception
                Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoDelete", ex)
            End Try
            DoQuery()
        End If
    End Sub

    Protected Sub gvMain_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMain.RowCommand
        If e.CommandName = "Detail" Then
            Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
            btnX.Caption = "返回"

            If UserProfile.SelectCompRoleID = "ALL" Then
                Me.TransferFramePage("~/SC/SC1002.aspx", New ButtonState() {btnX}, _
                    "ddlCompRoleID=" & ddlCompRoleID.SelectedValue, _
                    "ddlGroupID=" & ddlGroupID.SelectedValue, _
                    "ddlQueryFlag=" & ddlQueryFlag.SelectedValue, _
                    "PageNo=" & pcMain.PageNo.ToString(), _
                    "SelectedCompRoleID=" & gvMain.DataKeys(selectedRow(gvMain))("CompRoleID").ToString(), _
                    "SelectedGroupID=" & gvMain.DataKeys(selectedRow(gvMain))("GroupID").ToString(), _
                    "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
            Else
                Me.TransferFramePage("~/SC/SC1002.aspx", New ButtonState() {btnX}, _
                    "ddlGroupID=" & ddlGroupID.SelectedValue, _
                    "ddlQueryFlag=" & ddlQueryFlag.SelectedValue, _
                    "PageNo=" & pcMain.PageNo.ToString(), _
                    "SelectedCompRoleID=" & gvMain.DataKeys(selectedRow(gvMain))("CompRoleID").ToString(), _
                    "SelectedGroupID=" & gvMain.DataKeys(selectedRow(gvMain))("GroupID").ToString(), _
                    "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
            End If

        End If
    End Sub

    Private Sub DoClear()
        If ddlCompRoleID.SelectedValue <> "" Then
            ddlCompRoleID.SelectedIndex = 0
        End If

        ddlGroupID.SelectedIndex = 0
        ddlQueryFlag.SelectedIndex = 0

        ViewState.Item("DoQuery") = ""
        gvMain.Visible = False
    End Sub

    Protected Sub ddlCompRoleID_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlCompRoleID.SelectedIndexChanged
        Bsp.Utility.FillGroup_0501(ddlGroupID, ddlCompRoleID.SelectedValue)
        ddlGroupID.Items.Insert(0, New ListItem("---請選擇---", ""))
    End Sub

End Class
