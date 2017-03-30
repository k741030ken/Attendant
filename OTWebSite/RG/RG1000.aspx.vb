'****************************************************
'功能說明：新進員工資料輸入
'建立人員：MickySung
'建立日期：2015.05.29
'****************************************************
Imports System.Data

Partial Class RG_RG1000
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
            Case "btnAdd"       '新增
                DoAdd()
            Case "btnQuery"     '查詢
                ViewState.Item("DoQuery") = "Y"
                DoQuery()
            Case "btnDelete"    '刪除
                If funCheckData_Delete() Then
                    Release("btnDelete")
                    'DoDelete()
                End If
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
        Dim objSC As New SC
        Dim btnA As New ButtonState(ButtonState.emButtonType.Add)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
        Dim btnC As New ButtonState(ButtonState.emButtonType.Cancel)

        btnA.Caption = "新增"
        btnX.Caption = "返回"
        btnC.Caption = "清除"

        Me.TransferFramePage("~/RG/RG1001.aspx", New ButtonState() {btnA, btnX, btnC}, _
            lblCompRoleID.ID & "=" & UserProfile.SelectCompRoleID, _
            txtEmpID.ID & "=" & txtEmpID.Text, _
            txtEmpName.ID & "=" & txtEmpName.Text, _
            txtEmpIDOld.ID & "=" & txtEmpName.Text, _
            txtIDNo.ID & "=" & txtIDNo.Text, _
            "PageNo=" & pcMain.PageNo.ToString(), _
            "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
    End Sub

    Protected Sub gvMain_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMain.RowCommand
        If e.CommandName = "Detail" Then
            Dim objSC As New SC
            Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
            Dim strCompID As String = gvMain.DataKeys(Me.selectedRow(gvMain))("CompID").ToString()
            Dim a As New FlowBackInfo()

            a.URL = "~/RG/RG1000.aspx"
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

    Private Sub DoDelete()
        If selectedRow(gvMain) < 0 Then
            '尚未選取資料列！
            Bsp.Utility.ShowFormatMessage(Me, "W_00000")
        Else
            Dim bePersonal As New bePersonal.Row()
            Dim objRG As New RG1

            Dim strCompID As String
            If UserProfile.SelectCompRoleID = "ALL" Then
                strCompID = ddlCompID.SelectedValue
            Else
                strCompID = UserProfile.SelectCompRoleID
            End If

            bePersonal.CompID.Value = strCompID
            bePersonal.EmpID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("EmpID").ToString()

            Try
                objRG.DeletePersonalSetting(bePersonal)
                Bsp.Utility.ShowMessage(Me, "刪除「" & gvMain.DataKeys(Me.selectedRow(gvMain))("EmpID").ToString() & "」員工資料成功！")   '2015/11/19 Add 顯示訊息
            Catch ex As Exception
                Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoDelete", ex)
            End Try
            gvMain.DataBind()

            DoQuery()
        End If
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
                Case "ucRelease"
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    If aryValue(0) = "Y" Then
                        DoDelete()
                    End If
            End Select
        End If
    End Sub

    Private Sub Release(ByVal LogFunction As String)
        Dim EmpDate As String = gvMain.DataKeys(selectedRow(gvMain))("EmpDate").ToString()
        Dim today As String = Format(Now, "yyyy/MM/dd")
        If EmpDate <> today Then
            ucRelease.ShowCompRole = "True"
            ucRelease.FunID = "RG1000"
            ucRelease.LogFunction = LogFunction
            ucRelease.OpenSelect()
        Else
            DoDelete()
        End If
    End Sub

    '2015/10/14 刪除時檢核
    Private Function funCheckData_Delete() As Boolean
        Dim objRG As New RG1()

        If objRG.IsDataExists("[LNK_HRISDB].HRISDB.dbo.EmpShare", " AND CompID = " & Bsp.Utility.Quote(gvMain.DataKeys(Me.selectedRow(gvMain))("CompID").ToString()) & " AND EmpID = " & Bsp.Utility.Quote(gvMain.DataKeys(Me.selectedRow(gvMain))("EmpID").ToString())) Then
            Bsp.Utility.ShowMessage(Me, "【本資料如需刪除，請提需求單會辦金控人資處】")
            Return False
        End If

        Return True
    End Function

End Class
