'****************************************************
'功能說明：職位設定_細類
'建立人員：MickySung
'建立日期：2015.05.14
'****************************************************
Imports System.Data

Partial Class PA_PA1830
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objPA As New PA1()
        Dim objSC As New SC()

        If Not IsPostBack Then
            '細類
            PA1.FillCategoryIII_PA1830(ddlCategoryIII)
            ddlCategoryIII.Items.Insert(0, New ListItem("---請選擇---", ""))
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

        Me.TransferFramePage("~/PA/PA1831.aspx", New ButtonState() {btnA, btnX, btnC}, _
            ddlCategoryIII.ID & "=" & ddlCategoryIII.SelectedValue, _
            txtCategoryIIIName.ID & "=" & txtCategoryIIIName.Text, _
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

        Me.TransferFramePage("~/PA/PA1832.aspx", New ButtonState() {btnU, btnX, btnC}, _
            ddlCategoryIII.ID & "=" & ddlCategoryIII.SelectedValue, _
            txtCategoryIIIName.ID & "=" & txtCategoryIIIName.Text, _
            "PageNo=" & pcMain.PageNo.ToString(), _
            "SysID=" & arySysID(0), _
            "SelectedCategoryI=" & gvMain.DataKeys(selectedRow(gvMain))("CategoryI").ToString(), _
            "SelectedCategoryII=" & gvMain.DataKeys(selectedRow(gvMain))("CategoryII").ToString(), _
            "SelectedCategoryIII=" & gvMain.DataKeys(selectedRow(gvMain))("CategoryIII").ToString(), _
            "SelectedCategoryIName=" & gvMain.DataKeys(selectedRow(gvMain))("CategoryIName").ToString(), _
            "SelectedCategoryIIName=" & gvMain.DataKeys(selectedRow(gvMain))("CategoryIIName").ToString(), _
            "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
    End Sub

    Private Sub DoQuery()
        Dim objPA As New PA1()
        gvMain.Visible = True

        Try
            pcMain.DataTable = objPA.Position_CategoryIIIQuery(
                "CategoryIII=" & ddlCategoryIII.SelectedValue, _
                "CategoryIIIName=" & txtCategoryIIIName.Text)

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
        End Try
    End Sub

    Private Sub DoDelete()
        If selectedRow(gvMain) < 0 Then
            '尚未選取資料列！
            Bsp.Utility.ShowFormatMessage(Me, "W_00000")
        Else
            Dim bePosition_CategoryIII As New bePosition_CategoryIII.Row()
            Dim objPA As New PA1()
            Dim strSysID As String

            strSysID = Bsp.Utility.subGetSysID(UserProfile.LoginSysID)
            Dim arySysID() As String = Split(strSysID, "-")

            bePosition_CategoryIII.CategoryI.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("CategoryI").ToString()
            bePosition_CategoryIII.CategoryII.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("CategoryII").ToString()
            bePosition_CategoryIII.CategoryIII.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("CategoryIII").ToString()

            Try
                objPA.DeletePosition_CategoryIII(bePosition_CategoryIII)
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

            Me.TransferFramePage("~/PA/PA1832.aspx", New ButtonState() {btnX}, _
                ddlCategoryIII.ID & "=" & ddlCategoryIII.SelectedValue, _
                txtCategoryIIIName.ID & "=" & txtCategoryIIIName.Text, _
                "PageNo=" & pcMain.PageNo.ToString(), _
                "SelectedCategoryI=" & gvMain.DataKeys(selectedRow(gvMain))("CategoryI").ToString(), _
                "SelectedCategoryII=" & gvMain.DataKeys(selectedRow(gvMain))("CategoryII").ToString(), _
                "SelectedCategoryIII=" & gvMain.DataKeys(selectedRow(gvMain))("CategoryIII").ToString(), _
                "SelectedCategoryIName=" & gvMain.DataKeys(selectedRow(gvMain))("CategoryIName").ToString(), _
                "SelectedCategoryIIName=" & gvMain.DataKeys(selectedRow(gvMain))("CategoryIIName").ToString(), _
                "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))

        End If
    End Sub

    Private Sub DoClear()
        IsDoQuery.Value = ""
        ViewState.Item("DoQuery") = ""
        gvMain.Visible = False

        '細類
        ddlCategoryIII.SelectedValue = ""

        '細類名稱
        txtCategoryIIIName.Text = ""
    End Sub

End Class
