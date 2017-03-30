'****************************************************
'功能說明：群組維護
'建立人員：Ann
'建立日期：2014.08.28
'****************************************************
Imports System.Data

Partial Class SC_SC0400
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            Dim objSC As New SC
            ddlGroupID.Attributes.Add("onkeypress", "EntertoSubmit();")
            txtGroupName.Attributes.Add("onkeypress", "EntertoSubmit();")
            Dim strSysID As String
            strSysID = Bsp.Utility.subGetSysID(UserProfile.LoginSysID)
            Dim arySysID() As String = Split(strSysID, "-")
            lblSysName.Text = strSysID

            ddlCompRoleName.Visible = False
            If UserProfile.SelectCompRoleID = "ALL" Then
                ddlCompRoleName.Visible = True
                Bsp.Utility.FillCompany(ddlCompRoleName)
                ddlCompRoleName.Items.Insert(0, New ListItem("全金控", "0"))   '20150112 Ann modify
                lblCompRoleName.Visible = False

                '20150225 Beatrice modify 群組代碼改下拉式選單
                subGetAllGroupID()
            Else
                '系統管理者
                If UserProfile.IsSysAdmin = True Then
                    '系統管理者選擇全金控
                    If UserProfile.SelectCompRoleID = "ALL" Then
                        ddlCompRoleName.Visible = True
                        Bsp.Utility.FillCompany(ddlCompRoleName)
                        lblCompRoleName.Visible = False

                        '20150225 Beatrice modify 群組代碼改下拉式選單
                        Bsp.Utility.FillGroup_0501(ddlGroupID, ddlCompRoleName.SelectedValue)
                        ddlGroupID.Items.Insert(0, New ListItem("---請選擇---", ""))
                    Else
                        ddlCompRoleName.Visible = False
                        lblCompRoleName.Text = objSC.GetCompName(UserProfile.SelectCompRoleID).Rows(0).Item("CompName").ToString()
                        lblCompRoleName.Visible = True

                        '20150225 Beatrice modify 群組代碼改下拉式選單
                        Bsp.Utility.FillGroup(ddlGroupID)
                        ddlGroupID.Items.Insert(0, New ListItem("---請選擇---", ""))
                    End If
                Else
                    '非系統管理者
                    ddlCompRoleName.Visible = False
                    lblCompRoleName.Text = objSC.GetCompName(UserProfile.SelectCompRoleID).Rows(0).Item("CompName").ToString()
                    lblCompRoleName.Visible = True

                    '20150225 Beatrice modify 群組代碼改下拉式選單
                    Bsp.Utility.FillGroup(ddlGroupID)
                    ddlGroupID.Items.Insert(0, New ListItem("---請選擇---", ""))
                End If
            End If
            Page.SetFocus(ddlGroupID)
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
                ElseIf TypeOf ctl Is DropDownList Then
                    '20150302 Beatrice Add
                    CType(ctl, DropDownList).SelectedValue = ht(strKey).ToString()

                    If ctl.ID.Equals("ddlCompRoleName") Then
                        If ht(strKey).ToString() <> "" Then
                            If ht(strKey).ToString() = "0" Then
                                subGetAllGroupID()
                                ddlGroupID.SelectedValue = ht("ddlGroupID").ToString()
                            Else
                                Try
                                    Bsp.Utility.FillGroup_0501(ddlGroupID, ht(strKey).ToString())
                                    ddlGroupID.Items.Insert(0, New ListItem("---請選擇---", ""))
                                Catch ex As Exception
                                    ddlGroupID.Items.Insert(0, New ListItem("---請選擇---", ""))
                                End Try
                            End If
                        End If
                    End If
                    '20150302 Beatrice Add End
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

        Me.TransferFramePage("~/SC/SC0401.aspx", New ButtonState() {btnA, btnX}, _
                             Bsp.Utility.FormatToParam(ddlCompRoleName), _
                             Bsp.Utility.FormatToParam(ddlGroupID), _
                             Bsp.Utility.FormatToParam(txtGroupName), _
                             "PageNo=" & pcMain.PageNo.ToString(), _
                             "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
    End Sub

    Private Sub DoUpdate()
        'Dim lblGroupType As Label = gvMain.Rows(Me.selectedRow(gvMain)).FindControl("lblGroupType")
        'If lblGroupType IsNot Nothing Then
        '    If lblGroupType.Text = "1" Then
        '        '個人群組不允許修改
        '        Bsp.Utility.ShowFormatMessage(Me, "W_02300")
        '        Return
        '    End If
        'End If
        Dim btnU As New ButtonState(ButtonState.emButtonType.Update)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)

        btnU.Caption = "存檔返回"
        btnX.Caption = "返回"

        Me.TransferFramePage("~/SC/SC0402.aspx", New ButtonState() {btnU, btnX}, _
                             Bsp.Utility.FormatToParam(ddlCompRoleName), _
                             Bsp.Utility.FormatToParam(ddlGroupID), _
                             Bsp.Utility.FormatToParam(txtGroupName), _
                             "PageNo=" & pcMain.PageNo.ToString(), _
                             "SelectedSysID=" & gvMain.DataKeys(selectedRow(gvMain))("SysID").ToString(), _
                             "SelectedCompRoleID=" & gvMain.DataKeys(selectedRow(gvMain))("CompRoleID").ToString(), _
                             "SelectedGroupID=" & gvMain.DataKeys(selectedRow(gvMain))("GroupID").ToString(), _
                             "DoQuery=Y")
    End Sub

    Private Sub DoQuery()
        Dim objSC As New SC()
        Dim btnA As New ButtonState(ButtonState.emButtonType.Add)
        Dim btnU As New ButtonState(ButtonState.emButtonType.Update)
        Dim btnD As New ButtonState(ButtonState.emButtonType.Delete)

        Try
            Dim strSysID As String
            strSysID = Bsp.Utility.subGetSysID(UserProfile.LoginSysID)
            Dim arySysID() As String = Split(strSysID, "-")
            Dim strCompRoleID As String
            strCompRoleID = ddlCompRoleName.SelectedValue
            If strCompRoleID = "" Then
                strCompRoleID = UserProfile.SelectCompRoleID
            End If

            '20150302 Beatrice modify
            If strCompRoleID <> "0" Then
                pcMain.DataTable = objSC.QueryGroup( _
               "SysID=" & arySysID(0), _
               "CompRoleID=" & strCompRoleID, _
               "GroupID=" & ddlGroupID.SelectedValue, _
               "GroupName=" & txtGroupName.Text.Trim())
            Else
                If ddlGroupID.SelectedValue = "" Then
                    pcMain.DataTable = objSC.QueryGroup( _
                     "SysID=" & arySysID(0), _
                     "CompRoleID=" & "", _
                     "GroupID=" & "", _
                     "GroupName=" & txtGroupName.Text.Trim())
                Else
                    pcMain.DataTable = objSC.QueryGroup( _
                     "SysID=" & arySysID(0), _
                     "CompRoleID=" & ddlGroupID.SelectedValue.Split("-")(0), _
                     "GroupID=" & ddlGroupID.SelectedValue.Split("-")(1), _
                     "GroupName=" & txtGroupName.Text.Trim())
                End If
            End If
            '20150302 Beatrice modify End

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

            beSC_Group.SysID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("SysID").ToString()
            beSC_Group.CompRoleID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("CompRoleID").ToString()
            beSC_Group.GroupID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("GroupID").ToString()

            Try
                objSC.DeleteGroup(beSC_Group)
                objSC.DeleteGroupFun(beSC_Group.GroupID.Value, "", beSC_Group.SysID.Value, beSC_Group.CompRoleID.Value)
                objSC.DeleteUserGroup_0400(beSC_Group.SysID.Value, beSC_Group.CompRoleID.Value, beSC_Group.GroupID.Value)

            Catch ex As Exception
                Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoDelete", ex)
            End Try
            gvMain.DataBind()
        End If
    End Sub

    Protected Sub ddlCompRoleName_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlCompRoleName.SelectedIndexChanged
        If ddlCompRoleName.SelectedValue = "0" Then
            Dim btnA As New ButtonState(ButtonState.emButtonType.Add)
            Dim btnU As New ButtonState(ButtonState.emButtonType.Update)
            Dim btnD As New ButtonState(ButtonState.emButtonType.Delete)
            btnA.Visible = False
            btnU.Visible = False
            btnD.Visible = False

            subGetAllGroupID() '20150302 Beatrice modify
        Else
            '20150302 Beatrice modify
            Bsp.Utility.FillGroup_0501(ddlGroupID, ddlCompRoleName.SelectedValue)
            ddlGroupID.Items.Insert(0, New ListItem("---請選擇---", ""))
        End If
    End Sub

    '20150302 Beatrice Add
    Private Sub subGetAllGroupID()
        Dim objSC As New SC
        Try
            ddlGroupID.Items.Clear()
            Dim dt As DataTable = objSC.GetGroupInfo("", UserProfile.LoginSysID, "", "Order by G.CompRoleID, G.GroupID")
            For i = 0 To dt.Rows.Count - 1
                Dim strText As String = ""
                Dim strValue As String = ""

                If dt.Rows(i).Item("CompRoleID").ToString() = "ALL" Then
                    strText = dt.Rows(i).Item("CompRoleID").ToString() + "-" + "全金控" + "-" + dt.Rows(i).Item("GroupID").ToString() + "-" + dt.Rows(i).Item("GroupName").ToString()
                Else
                    strText = dt.Rows(i).Item("CompRoleID").ToString() + "-" + dt.Rows(i).Item("CompRoleName").ToString() + "-" + dt.Rows(i).Item("GroupID").ToString() + "-" + dt.Rows(i).Item("GroupName").ToString()
                End If
                strValue = dt.Rows(i).Item("CompRoleID").ToString() + "-" + dt.Rows(i).Item("GroupID").ToString()

                ddlGroupID.Items.Insert(i, New ListItem(strText, strValue))
            Next
            ddlGroupID.Items.Insert(0, New ListItem("---請選擇---", ""))
        Catch ex As Exception
            ddlGroupID.Items.Insert(0, New ListItem("---請選擇---", ""))
        End Try
    End Sub
    '20150302 Beatrice Add End
End Class
