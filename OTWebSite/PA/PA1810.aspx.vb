﻿'****************************************************
'功能說明：職位設定_大類
'建立人員：MickySung
'建立日期：2015.05.13
'****************************************************
Imports System.Data

Partial Class PA_PA1810
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objPA As New PA1()
        Dim objSC As New SC()

        If Not IsPostBack Then
            '大類
            PA1.FillCategoryI_PA1800(ddlCategoryI)
            ddlCategoryI.Items.Insert(0, New ListItem("---請選擇---", ""))
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

        Me.TransferFramePage("~/PA/PA1811.aspx", New ButtonState() {btnA, btnX, btnC}, _
            ddlCategoryI.ID & "=" & ddlCategoryI.SelectedValue, _
            txtCategoryIName.ID & "=" & txtCategoryIName.Text, _
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

        Me.TransferFramePage("~/PA/PA1812.aspx", New ButtonState() {btnU, btnX, btnC}, _
            ddlCategoryI.ID & "=" & ddlCategoryI.SelectedValue, _
            txtCategoryIName.ID & "=" & txtCategoryIName.Text, _
            "PageNo=" & pcMain.PageNo.ToString(), _
            "SysID=" & arySysID(0), _
            "SelectedCategoryI=" & gvMain.DataKeys(selectedRow(gvMain))("CategoryI").ToString(), _
            "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
    End Sub

    Private Sub DoQuery()
        Dim objPA As New PA1()
        gvMain.Visible = True

        Try
            pcMain.DataTable = objPA.Position_CategoryIQuery(
                "CategoryI=" & ddlCategoryI.SelectedValue, _
                "CategoryIName=" & txtCategoryIName.Text)

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
        End Try
    End Sub

    Private Sub DoDelete()
        If selectedRow(gvMain) < 0 Then
            '尚未選取資料列！
            Bsp.Utility.ShowFormatMessage(Me, "W_00000")
        Else
            Dim bePosition_CategoryI As New bePosition_CategoryI.Row()
            Dim objPA As New PA1()
            Dim strSysID As String
            Dim table As DataTable

            strSysID = Bsp.Utility.subGetSysID(UserProfile.LoginSysID)
            Dim arySysID() As String = Split(strSysID, "-")

            bePosition_CategoryI.CategoryI.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("CategoryI").ToString()

            table = objPA.IsDataExists_Position_CategoryII(gvMain.DataKeys(Me.selectedRow(gvMain))("CategoryI").ToString())
            If table.Rows.Count > 0 Then
                Dim Count As String = table.Rows(0).Item(0).ToString()
                If (CInt(Count) > 0) Then
                    Bsp.Utility.ShowFormatMessage(Me, "H_00000", "「" + gvMain.DataKeys(Me.selectedRow(gvMain))("CategoryI").ToString() + "」已存在職位_中類檔，不可刪除")
                Else
                    Try
                        objPA.DeletePosition_CategoryI(bePosition_CategoryI)
                    Catch ex As Exception
                        Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoDelete", ex)
                    End Try
                    gvMain.DataBind()
                    DoQuery()
                End If
            End If

        End If
    End Sub

    Protected Sub gvMain_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMain.RowCommand
        If e.CommandName = "Detail" Then
            Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)

            btnX.Caption = "返回"

            Me.TransferFramePage("~/PA/PA1812.aspx", New ButtonState() {btnX}, _
                ddlCategoryI.ID & "=" & ddlCategoryI.SelectedValue, _
                txtCategoryIName.ID & "=" & txtCategoryIName.Text, _
                "PageNo=" & pcMain.PageNo.ToString(), _
                "SelectedCategoryI=" & gvMain.DataKeys(selectedRow(gvMain))("CategoryI").ToString(), _
                "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))

        End If
    End Sub

    Private Sub DoClear()
        IsDoQuery.Value = ""
        ViewState.Item("DoQuery") = ""
        gvMain.Visible = False

        '大類
        ddlCategoryI.SelectedValue = ""

        '大類名稱
        txtCategoryIName.Text = ""
    End Sub

End Class
