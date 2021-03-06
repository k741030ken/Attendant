'****************************************************
'功能說明：處別維護
'建立人員：Chung
'建立日期：2011/05/17
'****************************************************
Imports System.Data

Partial Class SC_SC0220
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtRegionID.Attributes.Add("onkeypress", "EntertoSubmit();")
        txtRegionName.Attributes.Add("onkeypress", "EntertoSubmit();")

        If Not IsPostBack Then
            Page.SetFocus(txtRegionID)
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
            Case Else
                DoOtherAction()   '其他功能動作
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

        btnA.Caption = "存檔返回"
        btnX.Caption = "返回"

        Me.TransferFramePage("~/SC/SC0221.aspx", New ButtonState() {btnA, btnX}, _
                             Bsp.Utility.FormatToParam(txtRegionID), _
                             Bsp.Utility.FormatToParam(txtRegionName), _
                             "PageNo=" & pcMain.PageNo.ToString(), _
                             "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
    End Sub

    Private Sub DoUpdate()
        Dim btnU As New ButtonState(ButtonState.emButtonType.Update)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)

        btnU.Caption = "存檔返回"
        btnX.Caption = "返回"

        Me.TransferFramePage("~/SC/SC0222.aspx", New ButtonState() {btnU, btnX}, _
                             Bsp.Utility.FormatToParam(txtRegionID), _
                             Bsp.Utility.FormatToParam(txtRegionName), _
                             "PageNo=" & pcMain.PageNo.ToString(), _
                             "SelectedRegionID=" & gvMain.DataKeys(selectedRow(gvMain))("RegionID").ToString(), _
                             "DoQuery=Y")
    End Sub

    Private Sub DoQuery()
        Dim objSC As New SC()
        Try
            pcMain.DataTable = objSC.QueryRegion("RegionID=" & txtRegionID.Text.Trim().ToUpper(), "RegionName=" & txtRegionName.Text.Trim())
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
        End Try
    End Sub

    Private Sub DoDelete()
        If selectedRow(gvMain) < 0 Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00000")
        Else
            Dim beRegion As New beSC_Region.Row()
            Dim objSC As New SC

            beRegion.RegionID.Value = gvMain.DataKeys(selectedRow(gvMain))("RegionID").ToString()

            Try
                objSC.DeleteRegion(beRegion)
            Catch ex As Exception
                Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoDelete", ex)
            End Try
            gvMain.DataBind()
        End If
    End Sub

    Private Sub DoOtherAction()

    End Sub

End Class
