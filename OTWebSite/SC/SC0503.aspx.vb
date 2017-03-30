'****************************************************
'功能說明：群組功能維護－by功能
'建立人員：Ann
'建立日期：2014/09/01
'****************************************************
Imports System.Data

Partial Class SC_SC0503
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objSC As New SC
            Dim strSysID As String
            strSysID = Bsp.Utility.subGetSysID(UserProfile.LoginSysID)
            Dim arySysID() As String = Split(strSysID, "-")
            lblSysName.Text = strSysID
            ddlCompRoleName.Visible = False

            If UserProfile.SelectCompRoleID = "ALL" Then
                ddlCompRoleName.Visible = True
                Bsp.Utility.FillCompany(ddlCompRoleName)
                lblCompRoleName.Visible = False
            Else
                '系統管理者
                If UserProfile.IsSysAdmin = True Then
                    '系統管理者選擇全金控
                    If UserProfile.SelectCompRoleID = "ALL" Then
                        ddlCompRoleName.Visible = True
                        Bsp.Utility.FillCompany(ddlCompRoleName)
                        lblCompRoleName.Visible = False
                    Else
                        ddlCompRoleName.Visible = False
                        lblCompRoleName.Text = objSC.GetCompName(UserProfile.SelectCompRoleID).Rows(0).Item("CompName").ToString()
                        lblCompRoleName.Visible = True
                    End If
                Else
                    '非系統管理者
                    ddlCompRoleName.Visible = False
                    lblCompRoleName.Text = objSC.GetCompName(UserProfile.SelectCompRoleID).Rows(0).Item("CompName").ToString()
                    lblCompRoleName.Visible = True
                End If
            End If
            'Bsp.Utility.FillFun(ddlFun, Bsp.Enums.SelectFunType.FunHasRight, Bsp.Utility.IsStringNull(ViewState.Item("SysID")), Bsp.Utility.IsStringNull(ViewState.Item("CompRoleID")))
            Bsp.Utility.FillFun(ddlFun, Bsp.Enums.SelectFunType.FunHasRight, UserProfile.LoginSysID, UserProfile.SelectCompRoleID)
            ddlFun.Items.Insert(0, New ListItem("全選", "0"))
        End If
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

            For Each strKey As String In ht.Keys
                Select Case strKey
                    Case "PageNo"
                        pcMain.PageNo = Convert.ToInt32(ht(strKey))
                    Case Else
                        Dim ctl As Control = Me.FindControl(strKey)

                        If TypeOf ctl Is DropDownList Then
                            Bsp.Utility.SetSelectedIndex(CType(ctl, DropDownList), ht(strKey).ToString())
                        ElseIf TypeOf ctl Is Label Then
                            CType(ctl, Label).Text = ht(strKey).ToString()
                        End If
                End Select
            Next

            If ht.ContainsKey("DoQuery") AndAlso ht.ContainsKey("SelectedFunID") Then
                If ht("DoQuery").ToString() = "Y" Then
                    ViewState.Item("DoQuery") = "Y"
                    DoQuery(ht("SelectedFunID").ToString())
                End If
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
            Case Else
                DoOtherAction()   '其他功能動作
        End Select
    End Sub

    Private Sub DoAdd()
        Dim btnA As New ButtonState(ButtonState.emButtonType.Add)
        Dim btnU As New ButtonState(ButtonState.emButtonType.Update)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)

        btnA.Caption = "存檔繼續"
        btnU.Caption = "存檔返回"
        btnX.Caption = "返回"

        Me.TransferFramePage("~/SC/SC0504.aspx", New ButtonState() {btnA, btnU, btnX}, _
                             Bsp.Utility.FormatToParam(ddlFun), _
                             Bsp.Utility.FormatToParam(lblFun), _
                             "PageNo=" & pcMain.PageNo.ToString(), _
                             "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery"), "N"), _
                             "SelectedFunID=" & Bsp.Utility.IsStringNull(ViewState.Item("SelectedFunID")))
    End Sub

    Private Sub DoUpdate()
        Dim btnU As New ButtonState(ButtonState.emButtonType.Update)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)

        btnU.Caption = "存檔返回"
        btnX.Caption = "返回"

        Me.TransferFramePage("~/SC/SC0502.aspx", New ButtonState() {btnU, btnX}, _
                             Bsp.Utility.FormatToParam(ddlFun), _
                             Bsp.Utility.FormatToParam(lblFun), _
                             "PageNo=" & pcMain.PageNo.ToString(), _
                             "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery"), "N"), _
                             "SelectedFunID=" & Bsp.Utility.IsStringNull(ViewState.Item("SelectedFunID")), _
                             "SelectedSysID=" & UserProfile.LoginSysID, _
                             "SelectedCompRoleID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
                             "SelectedGroupID=" & gvMain.DataKeys(selectedRow(gvMain))("GroupID").ToString())
    End Sub

    Private Sub DoQuery(Optional ByVal sFunID As String = "", Optional ByVal CompRoleID As String = "")
        If ddlFun.SelectedIndex < 0 Then Return
        Dim objSC As New SC()

        If sFunID = "" Then sFunID = ddlFun.SelectedValue
        If CompRoleID = "" Then CompRoleID = UserProfile.SelectCompRoleID

        Try
            pcMain.DataTable = objSC.GetGroupFun(Bsp.Enums.GroupFunType.Fun, sFunID, CompRoleID)

            Dim lt As ListItem = ddlFun.Items.FindByValue(sFunID)
            If lt IsNot Nothing Then
                lblFun.Text = lt.Text
            End If
            ViewState.Item("SelectedFunID") = sFunID
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
        End Try
    End Sub

    Private Sub DoDelete()
        If selectedRow(gvMain) < 0 Then Return
        Dim objSC As New SC()

        Try
            'objSC.DeleteGroupFun(gvMain.DataKeys(selectedRow(gvMain))("GroupID").ToString(), Bsp.Utility.IsStringNull(ViewState.Item("SelectedFunID")), Bsp.Utility.IsStringNull(ViewState.Item("SysID")), Bsp.Utility.IsStringNull(ViewState.Item("CompRoleID")))
            objSC.DeleteGroupFun(gvMain.DataKeys(selectedRow(gvMain))("GroupID").ToString(), Bsp.Utility.IsStringNull(ViewState.Item("SelectedFunID")), UserProfile.LoginSysID, UserProfile.SelectCompRoleID)
            gvMain.DataBind()
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoDelete", ex)
        End Try
    End Sub

    Private Sub DoOtherAction()

    End Sub

    Protected Sub btnChangeToGroup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChangeToGroup.Click
        Me.TransferFramePage("~/SC/SC0500.aspx", Nothing)
    End Sub
End Class
