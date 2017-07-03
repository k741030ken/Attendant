'****************************************************
'功能說明：單位分行註記檔
'建立人員：John 
'建立日期：2017.06.06
'****************************************************
Imports System.Data

Partial Class OV_AT2000
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objSC As New SC()

        If Not IsPostBack Then
            '公司代碼
            If UserProfile.SelectCompRoleID = "ALL" Then
                lblCompRoleID.Visible = False
                Bsp.Utility.FillHRCompany(ddlCompID)
                Page.SetFocus(ddlCompID)

                '2015/07/27 Modify 規格變更:單位代碼1連動單位代碼2
                '單位代碼1
                PA2.FillOrganID_PA2201(ddlDeptID, ddlCompID.SelectedValue, "", "1")
                ddlDeptID.Items.Insert(0, New ListItem("---請選擇---", ""))

                '單位代碼2
                PA2.FillOrganID_PA2201(ddlOrganID, ddlCompID.SelectedValue, ddlDeptID.SelectedValue, "2")
                ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
            Else
                '2015/05/28 公司代碼-名稱改寫法
                lblCompRoleID.Text = UserProfile.SelectCompRoleName
                'lblCompRoleID.Text = UserProfile.SelectCompRoleID + "-" + objSC.GetCompName(UserProfile.SelectCompRoleID).Rows(0).Item("CompName").ToString
                ddlCompID.Visible = False

                '2015/07/27 Modify 規格變更:單位代碼1連動單位代碼2
                '單位代碼1
                PA2.FillOrganID_PA2201(ddlDeptID, UserProfile.SelectCompRoleID, "", "1")
                ddlDeptID.Items.Insert(0, New ListItem("---請選擇---", ""))

                '單位代碼2
                PA2.FillOrganID_PA2201(ddlOrganID, UserProfile.SelectCompRoleID, ddlDeptID.SelectedValue, "2")
                ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
            End If
        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnUpdate"    '修改
                DoUpdate()
            Case "btnQuery"     '查詢
                ViewState.Item("DoQuery") = "Y"
                DoQuery()
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

    Private Sub DoUpdate()
        Dim btnU As New ButtonState(ButtonState.emButtonType.Update)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
        Dim btnC As New ButtonState(ButtonState.emButtonType.Cancel)

        btnU.Caption = "存檔返回"
        btnX.Caption = "返回"
        btnC.Caption = "清除"

        Dim strCompID As String
        If UserProfile.SelectCompRoleID = "ALL" Then
            strCompID = ddlCompID.SelectedValue
        Else
            strCompID = UserProfile.SelectCompRoleID
        End If

        Me.TransferFramePage("~/OV/AT2002.aspx", New ButtonState() {btnU, btnX, btnC}, _
            ddlCompID.ID & "=" & strCompID, _
            ddlDeptID.ID & "=" & ddlDeptID.SelectedValue, _
            ddlOrganID.ID & "=" & ddlOrganID.SelectedValue, _
            "PageNo=" & pcMain.PageNo.ToString(), _
            "SelectedCompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
            "SelectedOrganID=" & gvMain.DataKeys(selectedRow(gvMain))("OrganID").ToString(), _
            "SelectedDeptID=" & gvMain.DataKeys(selectedRow(gvMain))("DeptID").ToString(), _
            "SelectedBranchFlag=" & gvMain.DataKeys(selectedRow(gvMain))("BranchFlag").ToString(), _
            "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
    End Sub

    Private Sub DoQuery()
        Dim objAT As New AT2()
        gvMain.Visible = True

        Dim strCompID As String
        If UserProfile.SelectCompRoleID = "ALL" Then
            strCompID = ddlCompID.SelectedValue
        Else
            strCompID = UserProfile.SelectCompRoleID
        End If
        IsDoQuery.Value = "Y"

        Try
            pcMain.DataTable = objAT.OrgBranchMarkGridViewQuery(
                "CompID=" & strCompID, _
                "DeptID=" & ddlDeptID.SelectedValue, _
                "OrganID=" & ddlOrganID.SelectedValue)

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
        End Try
    End Sub

    Protected Sub ddlCompID_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlCompID.SelectedIndexChanged

        '單位代碼1
        PA2.FillOrganID_PA2201(ddlDeptID, ddlCompID.SelectedValue, "", "1", PA2.DisplayType.Full)
        ddlDeptID.Items.Insert(0, New ListItem("---請選擇---", ""))

        ddlOrganID.Items.Clear()
        ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
    End Sub

    Protected Sub ddlDeptID_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlDeptID.SelectedIndexChanged
        Dim strCompID As String = ""
        If UserProfile.SelectCompRoleID = "ALL" Then
            strCompID = ddlCompID.SelectedValue
        Else
            strCompID = UserProfile.SelectCompRoleID
        End If

        '單位代碼2
        PA2.FillOrganID_PA2201(ddlOrganID, strCompID, ddlDeptID.SelectedValue, "2", PA2.DisplayType.Full)
        ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
    End Sub

End Class
