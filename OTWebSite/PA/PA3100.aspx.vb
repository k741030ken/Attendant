'****************************************************
'功能說明：學歷代碼設定
'建立人員：BeatriceCheng
'建立日期：2015.05.04
'****************************************************
Imports System.Data

Partial Class PA_PA3100
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            PA3.FillDDL(ddlEduID, "EduDegree", "EduID", "EduName", PA3.DisplayType.Full)
            ddlEduID.Items.Insert(0, New ListItem("---請選擇---", ""))
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

        Me.TransferFramePage("~/PA/PA3101.aspx", New ButtonState() {btnA, btnX, btnC}, _
            "ddlEduID=" & ddlEduID.SelectedValue, _
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

        Me.TransferFramePage("~/PA/PA3102.aspx", New ButtonState() {btnU, btnX, btnC}, _
            "ddlEduID=" & ddlEduID.SelectedValue, _
            "PageNo=" & pcMain.PageNo.ToString(), _
            "SelectedEduID=" & gvMain.DataKeys(selectedRow(gvMain))("EduID").ToString(), _
            "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
    End Sub

    Private Sub DoQuery()
        Dim objPA3 As New PA3()

        Try
            pcMain.DataTable = objPA3.EduDegreeQuery("EduID=" & ddlEduID.SelectedValue)
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
            Dim beEduDegree As New beEduDegree.Row()
            Dim objPA3 As New PA3

            beEduDegree.EduID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("EduID").ToString()

            Try
                objPA3.EduDegreeDelete(beEduDegree)
            Catch ex As Exception
                Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoDelete", ex)
            End Try

            ddlEduID.Items.RemoveAt(ddlEduID.Items.IndexOf(ddlEduID.Items.FindByValue(beEduDegree.EduID.Value)))
            DoQuery()
        End If
    End Sub

    Protected Sub gvMain_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMain.RowCommand
        If e.CommandName = "Detail" Then
            Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
            btnX.Caption = "返回"

            Me.TransferFramePage("~/PA/PA3102.aspx", New ButtonState() {btnX}, _
                "ddlEduID=" & ddlEduID.SelectedValue, _
                "PageNo=" & pcMain.PageNo.ToString(), _
                "SelectedEduID=" & gvMain.DataKeys(selectedRow(gvMain))("EduID").ToString(), _
                "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))

        End If
    End Sub

    Private Sub DoClear()
        ViewState.Item("DoQuery") = ""
        ddlEduID.SelectedValue = ""
        gvMain.Visible = False
    End Sub

End Class
