'****************************************************
'功能說明：資料授權設定
'建立人員：BeatriceCheng
'建立日期：2015.05.28
'****************************************************
Imports System.Data

Partial Class SC_SC0900
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objSC As New SC

            ddlCompID.Visible = False

            objSC.SC0900_FillTabName(ddlTabName)
            ddlTabName.Items.Insert(0, New ListItem("---請選擇---", ""))
            ddlFldName.Items.Insert(0, New ListItem("---請選擇---", ""))

            PA4.FillDDL(ddlCode, "HRCodeMap", "distinct Code", "", PA4.DisplayType.OnlyID, "", "And TabName like 'HRSupport%'")
            ddlCode.Items.Insert(0, New ListItem("---請選擇---", ""))

            If UserProfile.SelectCompRoleID = "ALL" Then
                ddlCompID.Visible = True
                Bsp.Utility.FillHRCompany(ddlCompID)
                'ddlCompID.Items.Insert(0, New ListItem("全金控", "0"))
                lblCompRoleID.Visible = False
            Else
                ddlCompID.Visible = False
                lblCompRoleID.Text = UserProfile.SelectCompRoleName
                lblCompRoleID.Visible = True
            End If
        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnAdd"       '新增
                DoAdd()
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

        Me.TransferFramePage("~/SC/SC0901.aspx", New ButtonState() {btnA, btnX, btnC}, _
            "ddlTabName=" & ddlTabName.SelectedValue, _
            "ddlFldName=" & ddlFldName.SelectedValue, _
            "ddlCode=" & ddlCode.SelectedValue, _
            "PageNo=" & pcMain.PageNo.ToString(), _
            "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
    End Sub

    Private Sub DoQuery()
        Dim objSC As New SC()

        Dim strCompID As String = ddlCompID.SelectedValue
        If strCompID = "" Then
            strCompID = UserProfile.SelectCompRoleID
        End If

        Try
            pcMain.DataTable = objSC.SC0900_Query(
                "TabName=" & ddlTabName.SelectedValue, _
                "FldName=" & ddlFldName.SelectedValue, _
                "Code=" & ddlCode.SelectedValue)
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
            Dim beHRCodeMap As New beHRCodeMap.Row()
            Dim objSC As New SC

            beHRCodeMap.TabName.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("TabName").ToString()
            beHRCodeMap.FldName.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("FldName").ToString()
            beHRCodeMap.Code.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("Code").ToString()
            Try
                objSC.SC0900_Delete(beHRCodeMap)
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
                Me.TransferFramePage("~/SC/SC0902.aspx", New ButtonState() {btnX}, _
                    "ddlTabName=" & ddlTabName.SelectedValue, _
                    "ddlFldName=" & ddlFldName.SelectedValue, _
                    "ddlCode=" & ddlCode.SelectedValue, _
                    "PageNo=" & pcMain.PageNo.ToString(), _
                    "SelectedTabName=" & gvMain.DataKeys(selectedRow(gvMain))("TabName").ToString(), _
                    "SelectedFldName=" & gvMain.DataKeys(selectedRow(gvMain))("FldName").ToString(), _
                    "SelectedCode=" & gvMain.DataKeys(selectedRow(gvMain))("Code").ToString(), _
                    "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
            Else
                Me.TransferFramePage("~/SC/SC0902.aspx", New ButtonState() {btnX}, _
                    "ddlTabName=" & ddlTabName.SelectedValue, _
                    "ddlFldName=" & ddlFldName.SelectedValue, _
                    "ddlCode=" & ddlCode.SelectedValue, _
                    "PageNo=" & pcMain.PageNo.ToString(), _
                    "SelectedTabName=" & gvMain.DataKeys(selectedRow(gvMain))("TabName").ToString(), _
                    "SelectedFldName=" & gvMain.DataKeys(selectedRow(gvMain))("FldName").ToString(), _
                    "SelectedCode=" & gvMain.DataKeys(selectedRow(gvMain))("Code").ToString(), _
                    "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
            End If

        End If
    End Sub

    Private Sub DoClear()
        ViewState.Item("DoQuery") = ""
        ddlCompID.SelectedValue = ""
        ddlTabName.SelectedIndex = 0
        ddlFldName.SelectedIndex = 0
        ddlCode.SelectedIndex = 0
        gvMain.Visible = False
    End Sub

    Protected Sub ddlTabName_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlTabName.SelectedIndexChanged
        Dim objSC As New SC
        objSC.SC0900_FillFldName(ddlFldName, ddlTabName.SelectedValue)

        ddlFldName.Items.Insert(0, New ListItem("---請選擇---", ""))
    End Sub

End Class
