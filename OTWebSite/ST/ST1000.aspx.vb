'****************************************************
'功能說明：員工資料維護作業
'建立人員：MickySung
'建立日期：2015.05.29
'****************************************************
Imports System.Data

Partial Class ST_ST1000
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            '公司代碼
            If UserProfile.SelectCompRoleID = "ALL" Then
                lblCompRoleID.Visible = False
                Bsp.Utility.FillHRCompany(ddlCompID)
                Page.SetFocus(ddlCompID)
            Else
                lblCompRoleID.Text = UserProfile.SelectCompRoleName
                ddlCompID.Visible = False
            End If
        End If

        '員工編號
        ucQueryEmp.ShowCompRole = "False"
        ucQueryEmp.InValidFlag = "N"
        ucQueryEmp.SelectCompID = UserProfile.SelectCompRoleID
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnQuery"     '查詢
                ViewState.Item("DoQuery") = "Y"
                DoQuery()
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

    Protected Sub gvMain_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMain.RowCommand
        If e.CommandName = "Detail" Then
            Dim objSC As New SC
            Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
            Dim strCompID As String = gvMain.DataKeys(Me.selectedRow(gvMain))("CompID").ToString()
            Dim a As New FlowBackInfo()

            a.URL = "~/ST/ST1000.aspx"
            btnX.Caption = "返回"
            a.MenuNodeTitle = "回清單" '2015/11/09 Add

            TransferFramePage(Bsp.MySettings.FlowRedirectPage, Nothing, "FlowID=EMPINFO", a, _
            lblCompRoleID.ID & "=" & UserProfile.SelectCompRoleID, _
            txtEmpID.ID & "=" & txtEmpID.Text, _
            txtEmpIDOld.ID & "=" & txtEmpIDOld.Text, _
            txtEmpName.ID & "=" & txtEmpName.Text, _
            txtIDNo.ID & "=" & txtIDNo.Text, _
            "SelectedCompID=" & strCompID, _
            "SelectedCompName=" & objSC.GetCompName(strCompID).Rows(0).Item("CompName").ToString, _
            "SelectedEmpID=" & gvMain.DataKeys(Me.selectedRow(gvMain))("EmpID").ToString(), _
            "SelectedEmpName=" & gvMain.DataKeys(Me.selectedRow(gvMain))("NameN").ToString(), _
            "SelectedIDNo=" & gvMain.DataKeys(Me.selectedRow(gvMain))("IDNo").ToString(), _
            "PageNo=" & pcMain.PageNo.ToString(), _
            "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
        End If
    End Sub

    Private Sub DoQuery()
        Dim objST As New ST1
        gvMain.Visible = True

        Dim strCompID As String
        If UserProfile.SelectCompRoleID = "ALL" Then
            strCompID = ddlCompID.SelectedValue
        Else
            strCompID = UserProfile.SelectCompRoleID
        End If

        Try
            pcMain.DataTable = objST.QueryPersonalSetting(
                "CompID=" & strCompID, _
                "EmpID=" & txtEmpID.Text.ToUpper, _
                "EmpIDOld=" & txtEmpIDOld.Text.ToUpper, _
                "EmpName=" & txtEmpName.Text, _
                "IDNo=" & txtIDNo.Text.ToUpper())

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
        End Try
    End Sub

    Private Sub DoClear()
        ViewState.Item("DoQuery") = ""
        gvMain.Visible = False

        ddlCompID.SelectedValue = ""
        txtEmpID.Text = ""
        txtEmpIDOld.Text = ""
        txtEmpName.Text = ""
        txtIDNo.Text = ""
    End Sub

    Public Overrides Sub DoModalReturn(ByVal returnValue As String)
        Dim strSql As String = ""

        If returnValue <> "" Then
            Dim aryData() As String = returnValue.Split(":")

            Select Case aryData(0)
                Case "ucQueryEmp"
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    '員工編號
                    txtEmpID.Text = aryValue(1)
            End Select
        End If
    End Sub

End Class
