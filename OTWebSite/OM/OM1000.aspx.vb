'****************************************************
'功能說明：新進員工文件繳交作業_銀行
'建立人員：Micky Sung
'建立日期：2015.07.03
'****************************************************
Imports System.Data

Partial Class OM_OM1000
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            '公司代碼


        End If
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            If UserProfile.SelectCompRoleID = "ALL" Then
                lblCompRoleID.Visible = False
                Bsp.Utility.FillHRCompany(ddlCompID)
                Page.SetFocus(ddlCompID)
            Else
                lblCompRoleID.Text = UserProfile.SelectCompRoleName
                ddlCompID.Visible = False
            End If
            'OM1.FillDDLOM1000(ddlOrganID, " OrganizationWait ", " OrganID ", "OrganName", "", OM1.DisplayType.Full, "", " and CompID=" + Bsp.Utility.Quote(UserProfile.SelectCompRoleID) + "", " order by OrganID ")
            'ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))

            'ucOrganID.QuerySQL = "Select distinct OrganID+'-'+OrganName  AS Code ,'' FROM  OrganizationWait   Where 1=1  and CompID=" + Bsp.Utility.Quote(UserProfile.SelectCompRoleID) + "  order by Code"
            'ucOrganID.LoadData()
            GetData()
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

            For Each strKey As String In ht.Keys
                Dim ctl As Control = Me.FindControl(strKey)
                If TypeOf ctl Is TextBox Then
                    CType(ctl, TextBox).Text = ht(strKey).ToString()
                ElseIf TypeOf ctl Is DropDownList Then
                    If ht(strKey).ToString <> "" Then
                        CType(ctl, DropDownList).SelectedValue = ht(strKey).ToString
                    End If
                Else
                    If TypeOf ctl Is UserControl Then
                        txtValidDateB.DateText = ht("txtValidDateB").ToString()
                        txtValidDateE.DateText = ht("txtValidDateE").ToString()
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

    'Protected Sub ddlOrganID_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlOrganID.SelectedIndexChanged
    '    Dim beOrganizationWait As New beOrganizationWait.Row()
    '    Dim bsOrganizationWait As New beOrganizationWait.Service()
    '    If UserProfile.SelectCompRoleID = "ALL" Then
    '        beOrganizationWait.CompID.Value = ddlCompID.SelectedValue
    '    Else
    '        beOrganizationWait.CompID.Value = UserProfile.SelectCompRoleID
    '    End If
    '    beOrganizationWait.OrganID.Value = ddlOrganID.SelectedValue
    '    Try
    '        Using dt As DataTable = bsOrganizationWait.QueryByKey(beOrganizationWait).Tables(0)
    '            If dt.Rows.Count <= 0 Then Exit Sub
    '            beOrganizationWait = New beOrganizationWait.Row(dt.Rows(0))
    '            txtOrganName.Text = beOrganizationWait.OrganName.Value.Trim
    '        End Using
    '    Catch ex As Exception
    '        Bsp.Utility.ShowMessage(Me, Me.FunID & "subGetData", ex)
    '    End Try
    'End Sub

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
                ViewState.Item("Action") = "btnDelete"
                Release("btnDelete")
                'DoDelete()

            Case "btnExecutes"   '執行
                ViewState.Item("Action") = "btnExecute"
                Release("btnExecute")
                'DoExecute()

            Case "btnActionX"   '清除
                DoClear()
            Case "btnDownload"
                DoDownload()
        End Select
    End Sub
    Private Sub Release(ByVal LogFunction As String)
        ucRelease.ShowCompRole = "True"
        ucRelease.FunID = "OM1000"
        ucRelease.LogFunction = LogFunction
        ucRelease.OpenSelect()
    End Sub
    Public Overrides Sub DoModalReturn(ByVal returnValue As String)
        Dim strSql As String = ""

        If returnValue <> "" Then
            Dim aryData() As String = returnValue.Split(":")

            Select Case aryData(0)

                Case "ucRelease"
                    lblReleaseResult.Text = ""
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    lblReleaseResult.Text = aryValue(0)
                    If lblReleaseResult.Text = "Y" Then
                        Select Case ViewState.Item("Action")
                            Case "btnDelete"
                                DoDelete()
                            Case "btnExecute"
                                DoExecute()
                        End Select
                    End If
               

            End Select
        End If
    End Sub
#Region "執行"
    Private Sub DoExecute()
        If gvMain.DataKeys(selectedRow(gvMain))("WaitStatus").ToString() = "已生效" Then
            Bsp.Utility.ShowMessage(Me, "該筆資料已生效")
            Return
        End If
        If gvMain.DataKeys(selectedRow(gvMain))("ValidDate").ToString() > Now.Date Then
            Bsp.Utility.ShowMessage(Me, "尚未到該筆資料生效日期")
            Return
        End If
        Dim objOM1 As New OM1
        Dim aryValue() As String = Split(gvMain.DataKeys(selectedRow(gvMain))("OrganReason").ToString(), "-")
        Dim OrganReasonNo As String = aryValue(0)
        aryValue = Split(gvMain.DataKeys(selectedRow(gvMain))("OrganType").ToString(), "-")
        Dim OrganTypeNo As String = aryValue(0).Substring(aryValue(0).Length - 1)
        Try
            Dim Result = objOM1.Execute_OM1000(gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
                                               gvMain.DataKeys(selectedRow(gvMain))("OrganID").ToString(), _
                                               OrganReasonNo, _
                                               OrganTypeNo, _
                                               gvMain.DataKeys(selectedRow(gvMain))("ValidDate").ToString(), _
                                               gvMain.DataKeys(selectedRow(gvMain))("Seq").ToString(), _
                                               UserProfile.ActCompID.ToString, _
                                               UserProfile.ActUserID.ToString())
            If Result(0) = "1" Then
                Bsp.Utility.ShowMessage(Me, gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString() & "執行失敗，失敗訊息：" & Result(1))
                Return
            End If
            Bsp.Utility.ShowMessage(Me, "執行完成")
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoExecute", ex)
        End Try
        DoQuery()
    End Sub
#End Region

#Region "新增"
    Private Sub DoAdd()
        Dim btnA As New ButtonState(ButtonState.emButtonType.Add)
        Dim btnF As New ButtonState(ButtonState.emButtonType.Confirm)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
        Dim btnC As New ButtonState(ButtonState.emButtonType.Cancel)

        btnA.Caption = "存檔返回"
        btnF.Caption = "存檔"
        btnX.Caption = "返回"
        btnC.Caption = "清除"

        Dim strCompID As String
        If UserProfile.SelectCompRoleID = "ALL" Then
            strCompID = ddlCompID.SelectedValue
        Else
            strCompID = UserProfile.SelectCompRoleID
        End If

        Me.TransferFramePage("~/OM/OM1001.aspx", New ButtonState() {btnA, btnF, btnX, btnC}, _
            ddlCompID.ID & "=" & strCompID, _
            ddlOrganType.ID & "=" & ddlOrganType.SelectedValue, _
            txtOrganName.ID & "=" & txtOrganName.Text, _
            ddlWaitStatus.ID & "=" & ddlWaitStatus.SelectedValue, _
            ddlOrganReason.ID & "=" & ddlOrganReason.SelectedValue, _
            txtValidDateB.ID & "=" & IIf(txtValidDateB.DateText = "____/__/__", "", txtValidDateB.DateText), _
            txtValidDateE.ID & "=" & IIf(txtValidDateE.DateText = "____/__/__", "", txtValidDateE.DateText), _
            "PageNo=" & pcMain.PageNo.ToString(), _
            "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
    End Sub
#End Region

#Region "新改"
    Private Sub DoUpdate()
        If gvMain.DataKeys(Me.selectedRow(gvMain))("WaitStatus").ToString() = "已生效" Then
            Bsp.Utility.ShowMessage(Me, "該筆資料已生效，無法修改")
            Return
        End If
        Dim btnU As New ButtonState(ButtonState.emButtonType.Update)
        Dim btnF As New ButtonState(ButtonState.emButtonType.Confirm)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
        Dim btnC As New ButtonState(ButtonState.emButtonType.Cancel)

        btnU.Caption = "存檔返回"
        btnF.Caption = "存檔"
        btnX.Caption = "返回"
        btnC.Caption = "清除"

        Dim strCompID As String
        If UserProfile.SelectCompRoleID = "ALL" Then
            strCompID = ddlCompID.SelectedValue
        Else
            strCompID = UserProfile.SelectCompRoleID
        End If

        Me.TransferFramePage("~/OM/OM1002.aspx", New ButtonState() {btnU, btnF, btnX, btnC}, _
            ddlCompID.ID & "=" & strCompID, _
            ddlOrganType.ID & "=" & ddlOrganType.SelectedValue, _
            txtOrganName.ID & "=" & txtOrganName.Text, _
            ucOrganID.ID & "=" & Split(ucOrganID.Text, "-")(0), _
            ddlWaitStatus.ID & "=" & ddlWaitStatus.SelectedValue, _
            ddlOrganReason.ID & "=" & ddlOrganReason.SelectedValue, _
            txtValidDateB.ID & "=" & IIf(txtValidDateB.DateText = "____/__/__", "", txtValidDateB.DateText), _
            txtValidDateE.ID & "=" & IIf(txtValidDateE.DateText = "____/__/__", "", txtValidDateE.DateText), _
            "PageNo=" & pcMain.PageNo.ToString(), _
            "SelectedCompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
            "SelectedOrganReason=" & gvMain.DataKeys(selectedRow(gvMain))("OrganReason").ToString(), _
            "SelectedOrganType=" & gvMain.DataKeys(selectedRow(gvMain))("OrganType").ToString(), _
            "SelectedValidDate=" & gvMain.DataKeys(selectedRow(gvMain))("ValidDate").ToString(), _
            "SelectedOrganID=" & gvMain.DataKeys(selectedRow(gvMain))("OrganID").ToString(), _
            "Seq=" & gvMain.DataKeys(selectedRow(gvMain))("Seq").ToString(), _
            "PageID=" & "ISupdate", _
            "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
    End Sub
#End Region

#Region "查詢"
    Private Sub DoQuery()
        Dim objOM As New OM1()
        'Dim DateTemp As String = ""

        If txtValidDateB.DateText = "____/__/__" Then
            txtValidDateB.DateText = ""
        Else
            If objOM.Check(txtValidDateB.DateText) Then
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", objOM.rtError)
                txtValidDateB.DateText = objOM.DateCheck(txtValidDateB.DateText)
                Return
            End If
        End If
       
        If txtValidDateE.DateText = "____/__/__" Then
            txtValidDateE.DateText = ""
        Else
            If objOM.Check(txtValidDateE.DateText) Then
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", objOM.rtError)
                txtValidDateE.DateText = objOM.DateCheck(txtValidDateE.DateText)
                Return
            End If
        End If


        gvMain.Visible = True
        ViewState.Item("DoQuery") = "Y"
        Dim strCompID As String
        If UserProfile.SelectCompRoleID = "ALL" Then
            strCompID = ddlCompID.SelectedValue
        Else
            strCompID = UserProfile.SelectCompRoleID
        End If

        Try

            pcMain.DataTable = objOM.OM1000Query(
                "CompID=" & strCompID, _
                "OrganType=" & ddlOrganType.SelectedValue, _
                "OrganID=" & Split(ucOrganID.Text, "-")(0), _
                "OrganName=" & txtOrganName.Text, _
                "WaitStatus=" & ddlWaitStatus.SelectedValue, _
                "OrganReason=" & ddlOrganReason.SelectedValue, _
                "ValidDateB=" & IIf(txtValidDateB.DateText = "____/__/__", "", txtValidDateB.DateText), _
                "ValidDateE=" & IIf(txtValidDateE.DateText = "____/__/__", "", txtValidDateE.DateText))

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
            'ViewState.Item("DoQuery") = "N"
        End Try
    End Sub

#End Region

#Region "刪除"
    Private Function funcheck() As Boolean
        Dim objOM As New OM1
        If gvMain.DataKeys(selectedRow(gvMain))("OrganID").ToString() = gvMain.DataKeys(selectedRow(gvMain))("UpOrganID").ToString() Then
            Return True
        End If
        If objOM.IsDataExists("Organization ", " and OrganID=" & Bsp.Utility.Quote(gvMain.DataKeys(selectedRow(gvMain))("UpOrganID").ToString()) & "  and InValidFlag='0' ") Then
            Bsp.Utility.ShowMessage(Me, "異動原因-組織無效：行政組織的上階部門已不存在或無效")
            Return False
        End If
        If objOM.IsDataExists("OrganizationFlow ", " and OrganID=" & Bsp.Utility.Quote(gvMain.DataKeys(selectedRow(gvMain))("UpOrganID").ToString()) & "  and InValidFlag='0' ") Then
            Bsp.Utility.ShowMessage(Me, "異動原因-組織無效：行政組織的上階部門已不存在或無效")
            Return False
        End If
        Return True
    End Function
    Private Sub DoDelete()
        If gvMain.DataKeys(Me.selectedRow(gvMain))("WaitStatus").ToString() = "已生效" Then
            Bsp.Utility.ShowMessage(Me, "該筆資料已生效，無法刪除")
            Return
        End If

        Dim beOrganizationWait As New beOrganizationWait.Row
        Dim beHRCodeMap As New beHRCodeMap.Row
        Dim objOM As New OM1
        Dim OrganTypeNo As String = ""
        Dim OrganReasonNo As String = ""
        Dim aryValue() As String
        Dim strOrganIDSQL As String = Bsp.Utility.Quote(gvMain.DataKeys(Me.selectedRow(gvMain))("OrganID").ToString())
        Dim strUpOrganIDSQL As String = Bsp.Utility.Quote(gvMain.DataKeys(Me.selectedRow(gvMain))("UpOrganID").ToString())
        Dim strCompIDSQL As String = Bsp.Utility.Quote(gvMain.DataKeys(Me.selectedRow(gvMain))("CompID").ToString())
        Dim strCompID As String
        If UserProfile.SelectCompRoleID = "ALL" Then
            strCompID = ddlCompID.SelectedValue
        Else
            strCompID = UserProfile.SelectCompRoleID
        End If
        aryValue = Split(gvMain.DataKeys(Me.selectedRow(gvMain))("OrganType").ToString(), "-")
        OrganTypeNo = aryValue(0).Substring(aryValue(0).Length - 1)
        aryValue = Split(gvMain.DataKeys(Me.selectedRow(gvMain))("OrganReason").ToString(), "-")
        OrganReasonNo = aryValue(0)
        '檢查該組織下是否有人
        If OrganReasonNo = "1" Then
            Select Case OrganTypeNo
                Case "1"
                    If Not objOM.IsDataExists("Personal", " and OrganID=" & strOrganIDSQL & " or DeptID=" & strOrganIDSQL) Then
                        Bsp.Utility.ShowMessage(Me, "該部門存在待異動人員，無法刪除")
                        Return
                    End If
                    If objOM.IsDataExists("EmpFlow", " and OrganID=" & strOrganIDSQL) Then
                        Bsp.Utility.ShowMessage(Me, "該部門存在待異動人員，無法刪除")
                        Return
                    End If

                Case "2"

                Case "3"

            End Select
        End If

        '/*===========================================*/
        '檢查上階組織是否存在且有效
        If OrganReasonNo = "2" Then
            Select Case OrganTypeNo
                Case "1"
                    If Not objOM.IsDataExists("Organization", " and OrganID=" & strUpOrganIDSQL & " and CompID=" & strCompIDSQL & " and InValidFlag='0'") Then
                        Bsp.Utility.ShowMessage(Me, "該筆資料的上階部門已不存在，無法刪除")
                        Return
                    End If
                    If objOM.IsDataExists("OrganizationWait", " and OrganID=" & strUpOrganIDSQL & " and CompID=" & strCompIDSQL & "and OrganType in('3'," & Bsp.Utility.Quote(OrganTypeNo) & ") and OrganReason='2'") Then
                        Bsp.Utility.ShowMessage(Me, "該筆資料的上階部門已無效化，無法刪除")
                        Return
                    End If
                Case "2"
                    If Not objOM.IsDataExists("OrganizationFlow", " and OrganID=" & strUpOrganIDSQL & " and InValidFlag='0'") Then
                        Bsp.Utility.ShowMessage(Me, "該筆資料的上階部門已不存在，無法刪除")
                        Return
                    End If
                    If objOM.IsDataExists("OrganizationWait", " and OrganID=" & strUpOrganIDSQL & " and CompID=" & strCompIDSQL & "and OrganType in('3'," & Bsp.Utility.Quote(OrganTypeNo) & ") and OrganReason='2'") Then
                        Bsp.Utility.ShowMessage(Me, "該筆資料的上階部門已無效化，無法刪除")
                        Return
                    End If
                Case "3"
                    If Not objOM.IsDataExists("Organization", " and OrganID=" & strUpOrganIDSQL & " and CompID=" & strCompIDSQL & " and InValidFlag='0'") Then
                        Bsp.Utility.ShowMessage(Me, "該筆資料的上階部門已不存在，無法刪除")
                        Return
                    End If
                    If Not objOM.IsDataExists("OrganizationFlow", " and OrganID=" & strUpOrganIDSQL & " and InValidFlag='0'") Then
                        Bsp.Utility.ShowMessage(Me, "該筆資料的上階部門已不存在，無法刪除")
                        Return
                    End If
                    If objOM.IsDataExists("OrganizationWait", " and OrganID=" & strUpOrganIDSQL & " and CompID=" & strCompIDSQL & "and OrganReason='2'") Then
                        Bsp.Utility.ShowMessage(Me, "該筆資料的上階部門已無效化，無法刪除")
                        Return
                    End If
            End Select

        End If

        With beOrganizationWait
            .CompID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("CompID").ToString()
            .OrganType.Value = OrganTypeNo
            .OrganReason.Value = OrganReasonNo
            .ValidDate.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("ValidDate").ToString()
            .OrganID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("OrganID").ToString()
            .Seq.Value = CInt(gvMain.DataKeys(Me.selectedRow(gvMain))("Seq").ToString())
        End With

        With beHRCodeMap
            .TabName.Value = ""
            .FldName.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("CompID").ToString()
            .Code.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("OrganID").ToString()
            .CodeCName.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("OrganNameOld").ToString()
            .LastChgComp.Value = UserProfile.ActCompID
            .LastChgID.Value = UserProfile.ActUserID
            .LastChgDate.Value = Now
        End With
        Try
            If objOM.OM1000Delete(beOrganizationWait, beHRCodeMap, OrganTypeNo, OrganReasonNo) Then
                objOM.PositionAndWorkTypeDelete("OrgPositionWait", gvMain.DataKeys(Me.selectedRow(gvMain))("CompID").ToString(), OrganReasonNo, OrganTypeNo, gvMain.DataKeys(Me.selectedRow(gvMain))("ValidDate").ToString(), gvMain.DataKeys(Me.selectedRow(gvMain))("OrganID").ToString())
                objOM.PositionAndWorkTypeDelete("OrgWorkTypeWait", gvMain.DataKeys(Me.selectedRow(gvMain))("CompID").ToString(), OrganReasonNo, OrganTypeNo, gvMain.DataKeys(Me.selectedRow(gvMain))("ValidDate").ToString(), gvMain.DataKeys(Me.selectedRow(gvMain))("OrganID").ToString())
                Bsp.Utility.ShowMessage(Me, "刪除部門「" & gvMain.DataKeys(Me.selectedRow(gvMain))("OrganID").ToString() & "」資料成功！")
            Else
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", "刪除部門「" & gvMain.DataKeys(Me.selectedRow(gvMain))("OrganID").ToString() & "」資料失敗！")
                Return
            End If


        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoDelete", ex)
        End Try

        gvMain.DataBind()
        DoQuery()
        'End If
    End Sub
#End Region

#Region "明細"
    Protected Sub gvMain_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMain.RowCommand
        If e.CommandName = "Detail" Then
            Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)

            btnX.Caption = "返回"

            Dim strCompID As String
            If UserProfile.SelectCompRoleID = "ALL" Then
                strCompID = ddlCompID.SelectedValue
            Else
                strCompID = UserProfile.SelectCompRoleID
            End If

            Me.TransferFramePage("~/OM/OM1002.aspx", New ButtonState() {btnX}, _
                ddlCompID.ID & "=" & strCompID, _
                ddlOrganType.ID & "=" & ddlOrganType.SelectedValue, _
                txtOrganName.ID & "=" & txtOrganName.Text, _
                ucOrganID.ID & "=" & Split(ucOrganID.Text, "-")(0), _
                ddlWaitStatus.ID & "=" & ddlWaitStatus.SelectedValue, _
                ddlOrganReason.ID & "=" & ddlOrganReason.SelectedValue, _
                txtValidDateB.ID & "=" & IIf(txtValidDateB.DateText = "____/__/__", "", txtValidDateB.DateText), _
                txtValidDateE.ID & "=" & IIf(txtValidDateE.DateText = "____/__/__", "", txtValidDateE.DateText), _
                "PageNo=" & pcMain.PageNo.ToString(), _
                "SelectedCompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
                "SelectedOrganReason=" & gvMain.DataKeys(selectedRow(gvMain))("OrganReason").ToString(), _
                "SelectedOrganType=" & gvMain.DataKeys(selectedRow(gvMain))("OrganType").ToString(), _
                "SelectedValidDate=" & gvMain.DataKeys(selectedRow(gvMain))("ValidDate").ToString(), _
                "SelectedOrganID=" & gvMain.DataKeys(selectedRow(gvMain))("OrganID").ToString(), _
                "Seq=" & gvMain.DataKeys(selectedRow(gvMain))("Seq").ToString(), _
                "PageID=" & "NOTupdate", _
                "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))

        End If
    End Sub
#End Region

#Region "清除"
    Private Sub DoClear()
        DoQuery()
        ViewState.Item("DoQuery") = ""
        gvMain.Visible = False

        ddlCompID.SelectedValue = ""
        ddlOrganType.SelectedIndex = 0
        ucOrganID.Text = ""
        txtOrganName.Text = ""
        ddlWaitStatus.SelectedIndex = 0
        ddlOrganReason.SelectedIndex = 0
        txtValidDateB.DateText = ""
        txtValidDateE.DateText = ""
        pcMain.PageNo = Nothing
        pcMain.PageCount = Nothing
        pcMain.RecordCount = Nothing
        pcMain.DataTable = Nothing
    End Sub
#End Region

#Region "下傳"
    Private Sub DoDownload()
        Dim objOM As New OM1
        If txtValidDateB.DateText = "____/__/__" Then
            txtValidDateB.DateText = ""
        Else
            If objOM.Check(txtValidDateB.DateText) Then
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", objOM.rtError)
                txtValidDateB.DateText = objOM.DateCheck(txtValidDateB.DateText)
                Return
            End If
        End If

        If txtValidDateE.DateText = "____/__/__" Then
            txtValidDateE.DateText = ""
        Else
            If objOM.Check(txtValidDateE.DateText) Then
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", objOM.rtError)
                txtValidDateE.DateText = objOM.DateCheck(txtValidDateE.DateText)
                Return
            End If
        End If
        Try
            If ViewState.Item("DoQuery") = "Y" Then
                Dim strCompID As String = ""

                If pcMain.DataTable.Rows.Count > 0 Then
                    '產出檔頭
                    Dim strFileName As String = ""
                    strFileName = Bsp.Utility.GetNewFileName("OM1000組織待異動資料維護-") & ".xls"


                    '動態產生GridView以便匯出成EXCEL
                    Dim gvExport As GridView = New GridView()
                    gvExport.AllowPaging = False
                    gvExport.AllowSorting = False
                    gvExport.FooterStyle.BackColor = Drawing.ColorTranslator.FromHtml("#99CCCC")
                    gvExport.FooterStyle.ForeColor = Drawing.ColorTranslator.FromHtml("#003399")
                    gvExport.RowStyle.CssClass = "tr_evenline"
                    gvExport.AlternatingRowStyle.CssClass = "tr_oddline"
                    gvExport.EmptyDataRowStyle.CssClass = "GridView_EmptyRowStyle"

                    If UserProfile.SelectCompRoleID = "ALL" Then
                        strCompID = ddlCompID.SelectedValue
                    Else
                        strCompID = UserProfile.SelectCompRoleID
                    End If
                    'objOM.QueryOrganizationWaitByDownload
                    gvExport.DataSource = objOM.QueryOrganizationWaitByDownload( _
                    "CompID=" & strCompID, _
                    "OrganType=" & ddlOrganType.SelectedValue.Trim, _
                    "OrganID=" & Split(ucOrganID.Text, "-")(0).Trim, _
                    "OrganName=" & txtOrganName.Text.Trim, _
                    "WaitStatus=" & ddlWaitStatus.SelectedValue.Trim, _
                    "OrganReason=" & ddlOrganReason.SelectedValue.Trim, _
                    "ValidDateB=" & IIf(txtValidDateB.DateText = "____/__/__", "", txtValidDateB.DateText).Trim, _
                    "ValidDateE=" & IIf(txtValidDateE.DateText = "____/__/__", "", txtValidDateE.DateText).Trim)
                    '"ValidDate=" & IIf(txtValidDateB.DateText = "____/__/__", "", txtValidDateB.DateText).Trim & ";" & IIf(txtValidDateE.DateText = "____/__/__", "", txtValidDateE.DateText).Trim()
                    AddHandler gvExport.RowDataBound, AddressOf gvExport_RowDataBound   '20140103 wei add 增加自訂事件
                    gvExport.DataBind()


                    Response.ClearContent()
                    Response.BufferOutput = True
                    Response.Charset = "utf-8"
                    ''Response.ContentType = "application/ms-excel"      '只寫ms-excel不OK，會變成程式碼下載@@
                    'Response.ContentType = "application/vnd.ms-excel"
                    Response.ContentType = "application/save-as"         '隱藏檔案網址路逕的下載
                    Response.AddHeader("Content-Transfer-Encoding", "binary")
                    Response.ContentEncoding = System.Text.Encoding.UTF8
                    Response.AddHeader("content-disposition", "attachment; filename=" & Server.UrlPathEncode(strFileName))

                    Dim oStringWriter As New System.IO.StringWriter()
                    Dim oHtmlTextWriter As New System.Web.UI.HtmlTextWriter(oStringWriter)

                    Response.Write("<meta http-equiv=Content-Type content=text/html charset=utf-8>")
                    Dim style As String = "<style>td{font-size:9pt} a{font-size:9pt} tr{page-break-after: always}</style>"

                    gvExport.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
                    gvExport.RenderControl(oHtmlTextWriter)
                    Response.Write(style)
                    Response.Write(oStringWriter.ToString())
                    Response.End()
                Else
                    Bsp.Utility.ShowFormatMessage(Me, "H_00000", "請先查詢有資料，才能下傳!")
                End If
            Else
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", "請先查詢有資料，才能下傳!")
            End If

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoDownload", ex)
        End Try

    End Sub

    Protected Sub gvExport_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)

        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                e.Row.Cells(31).Text = GetOrganFlow(DataBinder.Eval(e.Row.DataItem, "比對簽核單位").ToString())
        End Select
    End Sub

    Public Function GetOrganFlow(ByVal strOrganFlow As String) As String
        Dim objOM As New OM1
        Return objOM.GetFlowOrganName("'" & Replace(strOrganFlow, "|", "','") & "'")
    End Function
#End Region

    'Protected Sub gvMain_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvMain.RowDataBound
    '    Dim oRow As Data.DataRowView
    '    Dim oFontColor As System.Drawing.Color = Drawing.Color.Empty
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        oRow = CType(e.Row.DataItem, Data.DataRowView)
    '        'Select Case oRow("OrganType")
    '        '    Case "2-功能組織"
    '        '        oFontColor = Drawing.Color.Red
    '        '    Case "3-行政與功能組織"
    '        '        oFontColor = Drawing.Color.Blue
    '        '    Case Else
    '        '        oFontColor = Drawing.Color.Black
    '        'End Select

    '        e.Row.Cells(1).ToolTip = oRow("OrganID") & "-" & oRow("OrganName")
    '    End If
    'End Sub
    '/*============================================*/
#Region "ucOrganID"
    Public Sub GetData()
        Using dt = Bsp.DB.ExecuteDataSet(CommandType.Text, "Select distinct OrganID+'-'+OrganName  AS Code,OrganName ,'' FROM  OrganizationWait   Where 1=1  and CompID=" + Bsp.Utility.Quote(UserProfile.SelectCompRoleID) + "  order by Code", "eHRMSDB").Tables(0)
            ddlQueryValue.Items.Clear()
            For i As Integer = 0 To dt.Rows.Count - 1
                ddlQueryValue.Items.Insert(0, New ListItem(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
            Next
        End Using

    End Sub

    Public Delegate Sub SelectTextChangedHandler(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event ucSelectTextChangedHandler_TextChange As SelectTextChangedHandler

    Protected Overridable Sub SelectTextChanged(ByVal e As System.EventArgs)
        RaiseEvent ucSelectTextChangedHandler_TextChange(Me, e)
    End Sub

    Protected Sub ucOrganID_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTextChange.Click
        SelectTextChanged(e)
    End Sub
#End Region
End Class
