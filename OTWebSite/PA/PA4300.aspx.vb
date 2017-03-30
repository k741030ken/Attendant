'****************************************************
'功能說明：Web功能權限維護
'建立人員：BeatriceCheng
'建立日期：2015.05.13
'****************************************************
Imports System.Data

Partial Class PA_PA4300
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objSC As New SC


            ddlCompID.Visible = False
            If UserProfile.SelectCompRoleID = "ALL" Then
                ddlCompID.Visible = True
                Bsp.Utility.FillHRCompany(ddlCompID)
                'ddlCompID.Items.Insert(0, New ListItem("全金控", "0"))
                lblCompRoleID.Visible = False

                PA4.FillDDL(ddlWebID, "UserFormWeb S", "S.WebID", "F.WebName", PA3.DisplayType.Full, "Left Join FormWeb F on S.WebID = F.WebID", "And S.CompID = " & Bsp.Utility.Quote(ddlCompID.SelectedValue))
                ddlWebID.Items.Insert(0, New ListItem("---請選擇---", ""))
                'ddlWebID.Items.Insert(0, New ListItem("---請先選擇公司代碼---", ""))
            Else
                ddlCompID.Visible = False
                lblCompRoleID.Text = UserProfile.SelectCompRoleName
                lblCompRoleID.Visible = True

                PA4.FillDDL(ddlWebID, "UserFormWeb S", "S.WebID", "F.WebName", PA3.DisplayType.Full, "Left Join FormWeb F on S.WebID = F.WebID", "And S.CompID = " & Bsp.Utility.Quote(UserProfile.SelectCompRoleID))
                ddlWebID.Items.Insert(0, New ListItem("---請選擇---", ""))
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

        Me.TransferFramePage("~/PA/PA4301.aspx", New ButtonState() {btnA, btnX, btnC}, _
            "ddlWebID=" & ddlWebID.SelectedValue, _
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

        Me.TransferFramePage("~/PA/PA4302.aspx", New ButtonState() {btnU, btnX, btnC}, _
            "ddlWebID=" & ddlWebID.SelectedValue, _
            "PageNo=" & pcMain.PageNo.ToString(), _
            "SelectedCompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
            "SelectedWebID=" & gvMain.DataKeys(selectedRow(gvMain))("WebID").ToString(), _
            "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
    End Sub

    Private Sub DoQuery()
        Dim objPA4 As New PA4()

        Dim strCompID As String = ddlCompID.SelectedValue
        If strCompID = "" Then
            strCompID = UserProfile.SelectCompRoleID
        End If

        Try
            pcMain.DataTable = objPA4.UserFormWebQuery(
                "CompID=" & strCompID, _
                "WebID=" & ddlWebID.SelectedValue)
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
            Release("btnDelete")
        End If
    End Sub

    Protected Sub gvMain_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMain.RowCommand
        If e.CommandName = "Detail" Then
            Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
            btnX.Caption = "返回"

            If UserProfile.SelectCompRoleID = "ALL" Then
                Me.TransferFramePage("~/PA/PA4302.aspx", New ButtonState() {btnX}, _
                    "ddlCompID=" & ddlCompID.SelectedValue, _
                    "ddlWebID=" & ddlWebID.SelectedValue, _
                    "PageNo=" & pcMain.PageNo.ToString(), _
                    "SelectedCompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
                    "SelectedWebID=" & gvMain.DataKeys(selectedRow(gvMain))("WebID").ToString(), _
                    "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
            Else
                Me.TransferFramePage("~/PA/PA4302.aspx", New ButtonState() {btnX}, _
                    "ddlWebID=" & ddlWebID.SelectedValue, _
                    "PageNo=" & pcMain.PageNo.ToString(), _
                    "SelectedCompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
                    "SelectedWebID=" & gvMain.DataKeys(selectedRow(gvMain))("WebID").ToString(), _
                    "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
            End If

        End If
    End Sub

    Private Sub DoClear()
        ViewState.Item("DoQuery") = ""
        ddlCompID.SelectedValue = ""
        ddlWebID.SelectedIndex = 0
        gvMain.Visible = False
    End Sub

    Private Sub Release(ByVal LogFunction As String)
        ucRelease.ShowCompRole = "True"
        ucRelease.FunID = "PA4300"
        ucRelease.LogFunction = LogFunction
        ucRelease.OpenSelect()
    End Sub

    Public Overrides Sub DoModalReturn(ByVal returnValue As String)
        Dim strSql As String = ""

        If returnValue <> "" Then
            Dim aryData() As String = returnValue.Split(":")

            Select Case aryData(0)
                Case "ucRelease"
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    If aryValue(0) = "Y" Then
                        Dim beUserFormWeb As New beUserFormWeb.Row()
                        Dim objPA4 As New PA4

                        beUserFormWeb.CompID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("CompID").ToString()
                        beUserFormWeb.WebID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("WebID").ToString()

                        Try
                            objPA4.UserFormWebDelete(beUserFormWeb)
                        Catch ex As Exception
                            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoDelete", ex)
                        End Try

                        ddlWebID.Items.RemoveAt(ddlWebID.Items.IndexOf(ddlWebID.Items.FindByValue(beUserFormWeb.WebID.Value)))
                        DoQuery()
                    End If
            End Select
        End If
    End Sub

    Protected Sub ddlCompID_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlCompID.SelectedIndexChanged
        'If ddlCompID.SelectedValue = "0" Then
        '    ddlWebID.Items.Clear()
        '    ddlWebID.Items.Insert(0, New ListItem("---請先選擇公司代碼---", ""))
        'Else
        PA4.FillDDL(ddlWebID, "UserFormWeb S", "S.WebID", "F.WebName", PA3.DisplayType.Full, "Left Join FormWeb F on S.WebID = F.WebID", "And S.CompID = " & Bsp.Utility.Quote(ddlCompID.SelectedValue))
        ddlWebID.Items.Insert(0, New ListItem("---請選擇---", ""))
        'End If
    End Sub

End Class
