'****************************************************
'功能說明：其他代碼設定
'建立人員：MickySung
'建立日期：2015.05.25
'****************************************************
Imports System.Data

Partial Class PA_PA5100
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            '代碼類別
            PA5.FillFldName_PA5100(ddlTabFldName)
            ddlTabFldName.Items.Insert(0, New ListItem("---請選擇---", ""))

            '代碼
            PA5.FillCode_PA5100(ddlCode, "", "")
            ddlCode.Items.Insert(0, New ListItem("---請選擇---", ""))
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
                        'CType(ctl, DropDownList).SelectedValue = ht(strKey).ToString
                        Bsp.Utility.SetSelectedIndex(CType(ctl, DropDownList), ht(strKey).ToString)
                    End If
                End If
            Next

            '代碼
            If ddlTabFldName.SelectedValue <> "" Then
                ddlTabFldName_SelectedIndexChanged(Nothing, Nothing)
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

        Me.TransferFramePage("~/PA/PA5101.aspx", New ButtonState() {btnA, btnX, btnC}, _
            "ddlTabFldName=" & ddlTabFldName.SelectedValue, _
            "ddlCode=" & ddlCode.SelectedValue, _
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

        Me.TransferFramePage("~/PA/PA5102.aspx", New ButtonState() {btnU, btnX, btnC}, _
            "ddlTabFldName=" & ddlTabFldName.SelectedValue, _
            "ddlCode=" & ddlCode.SelectedValue, _
            "PageNo=" & pcMain.PageNo.ToString(), _
            "SelectedCode=" & gvMain.DataKeys(selectedRow(gvMain))("Code").ToString(), _
            "SelectedTabName=" & gvMain.DataKeys(selectedRow(gvMain))("TabName").ToString(), _
            "SelectedFldName=" & gvMain.DataKeys(selectedRow(gvMain))("FldName").ToString(), _
            "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
    End Sub

    Private Sub DoQuery()
        Dim objPA As New PA5()
        gvMain.Visible = True

        Dim TabName As String = ""
        Dim FldName As String = ""

        If ddlTabFldName.SelectedValue <> "" Then
            TabName = ddlTabFldName.SelectedValue.Split("\")(0)
            FldName = ddlTabFldName.SelectedValue.Split("\")(1)
        End If

        Try
            pcMain.DataTable = objPA.HRCodeMapSettingQuery(
                "TabName=" & TabName, _
                "FldName=" & FldName, _
                "Code=" & ddlCode.SelectedValue)
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
            Dim objPA As New PA5

            Dim TabName As String = ""
            Dim FldName As String = ""

            If ddlTabFldName.SelectedValue <> "" Then
                TabName = ddlTabFldName.SelectedValue.Split("\")(0)
                FldName = ddlTabFldName.SelectedValue.Split("\")(1)
            End If

            beHRCodeMap.TabName.Value = TabName
            beHRCodeMap.FldName.Value = FldName
            beHRCodeMap.Code.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("Code").ToString()

            Try
                objPA.DeleteHRCodeMapSetting(beHRCodeMap)
            Catch ex As Exception
                Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoDelete", ex)
            End Try
            gvMain.DataBind()

            DoQuery()

        End If
    End Sub

    Protected Sub gvMain_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMain.RowCommand
        If e.CommandName = "Detail" Then
            Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)

            btnX.Caption = "返回"

            Me.TransferFramePage("~/PA/PA5102.aspx", New ButtonState() {btnX}, _
                "ddlTabFldName=" & ddlTabFldName.SelectedValue, _
                "ddlCode=" & ddlCode.SelectedValue, _
                "PageNo=" & pcMain.PageNo.ToString(), _
                "SelectedCode=" & gvMain.DataKeys(selectedRow(gvMain))("Code").ToString(), _
                "SelectedTabName=" & gvMain.DataKeys(selectedRow(gvMain))("TabName").ToString(), _
                "SelectedFldName=" & gvMain.DataKeys(selectedRow(gvMain))("FldName").ToString(), _
                "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))

        End If
    End Sub

    Private Sub DoClear()
        IsDoQuery.Value = ""
        ViewState.Item("DoQuery") = ""
        gvMain.Visible = False
        '代碼類別
        ddlTabFldName.SelectedValue = ""

        '代碼
        ddlTabFldName_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Protected Sub ddlTabFldName_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlTabFldName.SelectedIndexChanged
        '代碼
        If ddlTabFldName.SelectedValue <> "" Then
            PA5.FillCode_PA5100(ddlCode, ddlTabFldName.SelectedValue.Split("\")(0), ddlTabFldName.SelectedValue.Split("\")(1))
        Else
            PA5.FillCode_PA5100(ddlCode, "", "")
        End If

        ddlCode.Items.Insert(0, New ListItem("---請選擇---", ""))
    End Sub

End Class
