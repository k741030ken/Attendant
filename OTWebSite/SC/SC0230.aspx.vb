'****************************************************
'功能說明：群組維護
'建立人員：Chung
'建立日期：2011.04.17
'****************************************************
Imports System.Data

Partial Class SC_SC0230
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtGroupID.Attributes.Add("onkeypress", "EntertoSubmit();")
        txtGroupName.Attributes.Add("onkeypress", "EntertoSubmit();")

        Page.SetFocus(txtGroupID)
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
            Case "btnActionC"   '確認
            Case "btnActionP"   '列印
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

        Me.TransferFramePage("~/SC/SC0231.aspx", New ButtonState() {btnA, btnX}, _
                             Bsp.Utility.FormatToParam(txtGroupID), _
                             Bsp.Utility.FormatToParam(txtGroupName), _
                             "PageNo=" & pcMain.PageNo.ToString(), _
                             "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuer")))
    End Sub

    Private Sub DoUpdate()
        Dim lblGroupType As Label = gvMain.Rows(Me.selectedRow(gvMain)).FindControl("lblGroupType")
        If lblGroupType IsNot Nothing Then
            If lblGroupType.Text = "1" Then
                '個人群組不允許修改
                Bsp.Utility.ShowFormatMessage(Me, "W_02300")
                Return
            End If
        End If
        Dim btnU As New ButtonState(ButtonState.emButtonType.Update)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)

        btnU.Caption = "存檔返回"
        btnX.Caption = "返回"

        Me.TransferFramePage("~/SC/SC0232.aspx", New ButtonState() {btnU, btnX}, _
                             Bsp.Utility.FormatToParam(txtGroupID), _
                             Bsp.Utility.FormatToParam(txtGroupName), _
                             "PageNo=" & pcMain.PageNo.ToString(), _
                             "SelectedGroupID=" & gvMain.DataKeys(selectedRow(gvMain))("GroupID").ToString(), _
                             "DoQuery=Y")
    End Sub

    Private Sub DoQuery()
        Dim objSC As New SC()

        Try
            pcMain.DataTable = objSC.QueryGroup("GroupID=" & txtGroupID.Text.Trim().ToUpper(), "GroupName=" & txtGroupName.Text.Trim())
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
        End Try
    End Sub

    Private Sub DoDelete()
        If selectedRow(gvMain) < 0 Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00000")
        Else
            Dim beSC_Group As New beSC_Group.Row()
            Dim objSC As New SC

            beSC_Group.GroupID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("GroupID").ToString()

            Try
                objSC.DeleteGroup(beSC_Group)
            Catch ex As Exception
                Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoDelete", ex)
            End Try
            gvMain.DataBind()
        End If
    End Sub
End Class
