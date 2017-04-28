'****************************************************
'功能說明：加班管理-事後申報-查詢
'建立日期：2017.01.12
'修改日期：2017.03.20
'****************************************************
Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Diagnostics      'For Debug.Print()
Imports System.Web.UI.WebControls
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.Common
Partial Class OV_OV4200
    Inherits PageBase

#Region "全域變數"
    Private Property Config_AattendantDBFlowCase As String
        Get
            Dim result As String = OVBusinessCommon.AattendantDBFlowCase
            Return result
        End Get
        Set(ByVal value As String)

        End Set
    End Property
    Private Property Config_AattendantDBFlowFullLog As String
        Get
            Dim result As String = OVBusinessCommon.AattendantDBFlowFullLog
            Return result
        End Get
        Set(ByVal value As String)

        End Set
    End Property

    Private _overtimeDBName As String = "AattendantDB"
#End Region

#Region "畫面與功能鍵"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            '公司代碼
            lblCompID.Text = UserProfile.SelectCompRoleName.ToString().Trim
            hidCompID.Value = UserProfile.SelectCompRoleID.ToString().Trim
            lblDeptID.Text = UserProfile.DeptID.ToString().Trim + IIf(UserProfile.DeptName.ToString().Trim <> "", vbTab & UserProfile.DeptName.ToString().Trim, "")
            hidDeptID.Value = UserProfile.DeptID.ToString().Trim
            lblOrganID.Text = UserProfile.OrganID.ToString().Trim + IIf(UserProfile.OrganName.ToString().Trim <> "", vbTab & UserProfile.OrganName.ToString().Trim, "")
            hidOrganID.Value = UserProfile.OrganID.ToString().Trim
            hidOTRegisterCompID.Value = UserProfile.ActCompID.Trim

            '員工編號
            ucQueryEmp.ShowCompRole = "False"
            ucQueryEmp.InValidFlag = "N"
            ucQueryEmp.SelectCompID = UserProfile.SelectCompRoleID

            hidUserID.Value = UserProfile.ActUserID
            callHandlerUrl.Value = Bsp.Utility.getAppSetting("AattendantWebPath")
        End If

    End Sub

    ''' <summary>
    ''' 功能鍵設定
    ''' </summary>
    ''' <param name="Param"></param>
    ''' <remarks></remarks>
    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnAdd"       '新增
                DoAdd()
            Case "btnUpdate"    '修改
                DoUpdate()
            Case "btnDelete"    '刪除
                ViewState.Item("Action") = "btnDelete"
                If funCheckStatus() Then
                    funPopupNotify()
                End If
            Case "btnQuery"     '查詢
                ViewState.Item("DoQuery") = "Y"
                DoQuery()
            Case "btnExecutes"  '送簽
                ViewState.Item("Action") = "btnExecutes"
                If funCheckStatus() Then
                    funPopupNotify()
                End If
            Case "btnReject"    '取消
                ViewState.Item("Action") = "btnReject"
                If funCheckStatus() Then
                    funPopupNotify()
                End If
            Case "btnActionX"   '清除
                DoClear()
        End Select
    End Sub

    ''' <summary>
    ''' 撈取暫存的資料
    ''' </summary>
    ''' <param name="ti"></param>
    ''' <remarks>跳去下一個頁面前暫存的資料</remarks>
    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

            'If ht.ContainsKey("SelectedEmpID") Then hidEmpID.Value = ht("SelectedEmpID").ToString()

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
                ViewState.Item("DoQuery") = ht("DoQuery").ToString()
                If ViewState.Item("DoQuery").ToString() = "Y" Then
                    DoQuery()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' 使用者輸入員編後查詢員工姓名
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub txtEmpID_TextChanged(sender As Object, e As System.EventArgs) Handles txtEmpID.TextChanged
        If txtEmpID.Text.Trim <> "" Then
            Dim objOV42 As New OV4_2()
            Dim rtnTable As DataTable = objOV42.GetEmpName(hidCompID.Value, txtEmpID.Text.Trim)
            If rtnTable.Rows.Count <= 0 Then
                lblEmpNameN.Text = ""
                'Bsp.Utility.ShowFormatMessage(Me, "W_00020", "員工編號查詢姓名")
            Else
                lblEmpNameN.Text = rtnTable.Rows(0).Item(0)
            End If
        Else
            lblEmpNameN.Text = ""
        End If
    End Sub

    ''' <summary>
    ''' 彈跳視窗-快速人員查詢(QFind)以及ucRelease
    ''' </summary>
    ''' <param name="returnValue"></param>
    ''' <remarks></remarks>
    Public Overrides Sub DoModalReturn(ByVal returnValue As String)
        Dim strSql As String = ""

        If returnValue <> "" Then
            Dim aryData() As String = returnValue.Split(":")

            Select Case aryData(0)
                Case "ucQueryEmp"
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    '員工編號
                    txtEmpID.Text = aryValue(1)
                    '員工姓名
                    lblEmpNameN.Text = aryValue(2)
            End Select
        End If
    End Sub

#End Region

#Region "功能鍵執行動作"

    ''' <summary>
    ''' 新增頁
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DoAdd()
        Dim btnA As New ButtonState(ButtonState.emButtonType.Add)
        Dim btnE As New ButtonState(ButtonState.emButtonType.Executes)
        Dim btnC As New ButtonState(ButtonState.emButtonType.Cancel)
        Dim btnD As New ButtonState(ButtonState.emButtonType.Delete)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)

        btnA.Caption = "暫存"
        btnE.Caption = "送簽"
        btnC.Caption = "清除"
        btnD.Caption = "刪除"
        btnX.Caption = "返回"

        Try
            Me.TransferFramePage("~/OV/OV4201.aspx", New ButtonState() {btnA, btnE, btnC, btnD, btnX}, _
                   lblCompID.ID & "=" & lblCompID.Text, _
                   hidCompID.ID & "=" & hidCompID.Value.ToString, _
                   lblDeptID.ID & "=" & lblDeptID.Text, _
                   hidDeptID.ID & "=" & hidDeptID.Value.ToString, _
                   lblOrganID.ID & "=" & lblOrganID.Text, _
                   hidOrganID.ID & "=" & hidOrganID.Value.ToString, _
                   txtEmpID.ID & "=" & txtEmpID.Text, _
                   lblEmpNameN.ID & "=" & lblEmpNameN.Text, _
                   txtOTFormNo.ID & "=" & txtOTFormNo.Text, _
                   ddlOTStatus.ID & "=" & ddlOTStatus.SelectedValue, _
                   hidOTStatus.ID & "=" & hidOTStatus.Value.ToString, _
                   ucOTStartDate.ID & "=" & ucOTStartDate.DateText, _
                   ucOTEndDate.ID & "=" & ucOTEndDate.DateText, _
                   "PageNo=" & pcMain.PageNo.ToString(), _
                   "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")), _
                   "DoUpdate=" & Bsp.Utility.IsStringNull(ViewState.Item("DoUpdate")))
        Catch ex As Exception
            Debug.Print("DoAdd()==>>" + ex.Message)
            Bsp.Utility.ShowMessage(Me, Me.FunID & "DoAdd()", ex)
        End Try
    End Sub

    ''' <summary>
    ''' 修改頁
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DoUpdate()
        Dim btnU As New ButtonState(ButtonState.emButtonType.Update)
        Dim btnE As New ButtonState(ButtonState.emButtonType.Executes)
        Dim btnC As New ButtonState(ButtonState.emButtonType.Cancel)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)

        btnU.Caption = "暫存"
        btnE.Caption = "送簽"
        btnC.Caption = "清除"
        btnX.Caption = "返回"

        Dim intSelectRow As Integer
        Dim intSelectCount As Integer = 0

        Try
            'If selectedRows(gvMain) = "-1" Then
            If selectedRows(gvMain) = String.Empty Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00000")
            Else
                For RowCount As Integer = 0 To (gvMain.Rows.Count - 1)
                    Dim chk As CheckBox = gvMain.Rows(RowCount).FindControl("chk_gvMain")
                    If chk.Checked Then
                        intSelectRow = RowCount
                        intSelectCount = intSelectCount + 1
                    End If
                Next

                Debug.Print("Start of Print ")
                Debug.Print("===>>intSelectRow = " + intSelectRow.ToString)
                Debug.Print("===>>intSelectCount = " + intSelectCount.ToString)
                Debug.Print("===>>SelectedOTCompID = " + gvMain.DataKeys(intSelectRow)("OTCompID").ToString())
                Debug.Print("===>>SelectedOTEmpID = " + gvMain.DataKeys(intSelectRow)("OTEmpID").ToString())
                Debug.Print("===>>SelectedOTNameN = " + gvMain.DataKeys(intSelectRow)("NameN").ToString())
                Debug.Print("===>>SelectedOTFormNO = " + gvMain.DataKeys(intSelectRow)("OTFormNO").ToString())
                Debug.Print("End of Print ")

                If intSelectCount = 1 Then
                    ViewState.Item("DoUpdate") = "Y"
                    If gvMain.DataKeys(intSelectRow)("OTStatus").ToString() <> "1" Then
                        Bsp.Utility.ShowMessage(Me, "此狀態無法修改")
                        Return
                    End If

                    Dim OTStartTime As String = gvMain.DataKeys(intSelectRow)("OTTime").ToString().Split("~")(0)
                    Dim OTEndTime As String = gvMain.DataKeys(intSelectRow)("OTTime").ToString().Split("~")(1)
                    Dim OTStartDate As String = gvMain.DataKeys(intSelectRow)("OTDate").ToString().Split("~")(0)
                    Dim OTEndDate As String = gvMain.DataKeys(intSelectRow)("OTDate").ToString().Split("~")(1)

                    Me.TransferFramePage("~/OV/OV4202.aspx", New ButtonState() {btnU, btnE, btnC, btnX}, _
                           lblCompID.ID & "=" & lblCompID.Text, _
                           hidCompID.ID & "=" & hidCompID.Value.ToString, _
                           lblDeptID.ID & "=" & lblDeptID.Text, _
                           hidDeptID.ID & "=" & hidDeptID.Value.ToString, _
                           lblOrganID.ID & "=" & lblOrganID.Text, _
                           hidOrganID.ID & "=" & hidOrganID.Value.ToString, _
                           txtEmpID.ID & "=" & txtEmpID.Text, _
                           lblEmpNameN.ID & "=" & lblEmpNameN.Text, _
                           txtOTFormNo.ID & "=" & txtOTFormNo.Text, _
                           ddlOTStatus.ID & "=" & ddlOTStatus.SelectedValue, _
                           hidOTStatus.ID & "=" & hidOTStatus.Value.ToString, _
                           ucOTStartDate.ID & "=" & ucOTStartDate.DateText, _
                           ucOTEndDate.ID & "=" & ucOTEndDate.DateText, _
                           "PageNo=" & pcMain.PageNo.ToString(), _
                           "SelectedOTCompID=" & gvMain.DataKeys(intSelectRow)("OTCompID").ToString(), _
                           "SelectedOTEmpID=" & gvMain.DataKeys(intSelectRow)("OTEmpID").ToString(), _
                           "SelectedOTNameN=" & gvMain.DataKeys(intSelectRow)("NameN").ToString(), _
                           "SelectedOTFormNO=" & gvMain.DataKeys(intSelectRow)("OTFormNO").ToString(), _
                           "SelectedOTSeq=" & gvMain.DataKeys(intSelectRow)("OTSeq").ToString(), _
                           "SelectedOTTxnID=" & gvMain.DataKeys(intSelectRow)("OTTxnID").ToString(), _
                           "SelectedOTStartDate=" & OTStartDate, _
                           "SelectedOTEndDate=" & OTEndDate, _
                           "SelectedOTFromAdvanceTxnId=" & gvMain.DataKeys(intSelectRow)("OTFromAdvanceTxnId").ToString(), _
                           "SelectedOTStartTime=" & OTStartTime, _
                           "SelectedOTEndTime=" & OTEndTime, _
                           "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")), _
                           "DoUpdate=" & Bsp.Utility.IsStringNull(ViewState.Item("DoUpdate")))
                Else
                    Bsp.Utility.ShowMessage(Me, "修改只能選擇一筆資料")
                End If
            End If
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & "DoUpdate", ex)
        End Try
    End Sub

    ''' <summary>
    ''' 進行查詢
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DoQuery()
        Try
            pcMain.DataTable = Query()
            gvMain.DataBind()
            gvMain.Visible = True
            ResetGrid()
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & "DoQuery", ex)
        End Try
    End Sub

    ''' <summary>
    ''' 清除
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DoClear()
        gvMain.Visible = False
        ddlQueryFactor.SelectedIndex = 0
        txtEmpID.Text = ""
        'txtEmpID.DataText = ""
        'lblEmpID.Text = ""
        txtEmpID.Text = ""
        lblEmpNameN.Text = ""
        txtOTFormNo.Text = ""
        ddlOTStatus.SelectedIndex = 0
        ucOTStartDate.DateText = ""
        ucOTEndDate.DateText = ""
        ddlSalaryOrAdjust.SelectedIndex = 0
        ViewState.Item("DoQuery") = ""
        DoQuery()
        pcMain.DataTable.Clear()
        gvMain.Visible = False
        ResetGrid()
    End Sub

#End Region

#Region "GridView相關"

    ''' <summary>
    ''' 明細頁面
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvMain_RowDataBound(ByVal sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMain.RowCommand
        If e.CommandName = "Detail" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)

            Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
            btnX.Caption = "返回"

            Try
                Dim OTStartTime As String = gvMain.DataKeys(index)("OTTime").ToString().Split("~")(0)
                Dim OTEndTime As String = gvMain.DataKeys(index)("OTTime").ToString().Split("~")(1)
                Dim OTStartDate As String = gvMain.DataKeys(index)("OTDate").ToString().Split("~")(0)
                Dim OTEndDate As String = gvMain.DataKeys(index)("OTDate").ToString().Split("~")(1)

                ViewState.Item("DoUpdate") = "N"
                Me.TransferFramePage("~/OV/OV4202.aspx", New ButtonState() {btnX}, _
                       lblCompID.ID & "=" & lblCompID.Text, _
                       hidCompID.ID & "=" & hidCompID.Value.ToString, _
                       lblDeptID.ID & "=" & lblDeptID.Text, _
                       hidDeptID.ID & "=" & hidDeptID.Value.ToString, _
                       lblOrganID.ID & "=" & lblOrganID.Text, _
                       hidOrganID.ID & "=" & hidOrganID.Value.ToString, _
                       txtEmpID.ID & "=" & txtEmpID.Text, _
                       lblEmpNameN.ID & "=" & lblEmpNameN.Text, _
                       txtOTFormNo.ID & "=" & txtOTFormNo.Text, _
                       ddlOTStatus.ID & "=" & ddlOTStatus.SelectedValue, _
                       hidOTStatus.ID & "=" & hidOTStatus.Value.ToString, _
                       ucOTStartDate.ID & "=" & ucOTStartDate.DateText, _
                       ucOTEndDate.ID & "=" & ucOTEndDate.DateText, _
                       "PageNo=" & pcMain.PageNo.ToString(), _
                       "SelectedOTCompID=" & gvMain.DataKeys(index)("OTCompID").ToString(), _
                       "SelectedOTEmpID=" & gvMain.DataKeys(index)("OTEmpID").ToString(), _
                       "SelectedOTNameN=" & gvMain.DataKeys(index)("NameN").ToString(), _
                       "SelectedOTFormNO=" & gvMain.DataKeys(index)("OTFormNO").ToString(), _
                       "SelectedOTSeq=" & gvMain.DataKeys(index)("OTSeq").ToString(), _
                       "SelectedOTTxnID=" & gvMain.DataKeys(index)("OTTxnID").ToString(), _
                       "SelectedOTStartDate=" & OTStartDate, _
                       "SelectedOTEndDate=" & OTEndDate, _
                       "SelectedOTFromAdvanceTxnId=" & gvMain.DataKeys(index)("OTFromAdvanceTxnId").ToString(), _
                       "SelectedOTStartTime=" & OTStartTime, _
                       "SelectedOTEndTime=" & OTEndTime, _
                       "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")), _
                       "DoUpdate=" & Bsp.Utility.IsStringNull(ViewState.Item("DoUpdate")))
            Catch ex As Exception
                Bsp.Utility.ShowMessage(Me, Me.FunID & "Detail", ex)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' GridView畫面
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvMain_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvMain.RowDataBound
        Dim oRow As Data.DataRowView
        Dim BgColor As System.Drawing.Color = Drawing.Color.Empty

        If e.Row.RowType = DataControlRowType.DataRow Then
            '更改表單編號顯示為 8碼+8碼
            If Len(e.Row.Cells(3).Text.Trim) = 16 Then
                'Dim OTFormNO As String = Left(e.Row.Cells(3).Text.Trim, 8) + Right(e.Row.Cells(3).Text.Trim, 8)     '複製出來會被換行
                'Dim OTFormNO As String = Left(e.Row.Cells(3).Text.Trim, 8) & vbCrLf & Right(e.Row.Cells(3).Text.Trim, 8)
                Dim OTFormNO As String = Left(e.Row.Cells(3).Text.Trim, 8) + "<br />" + Right(e.Row.Cells(3).Text.Trim, 8)
                e.Row.Cells(3).Text = OTFormNO.Replace(" ", "")
            End If

            '若表單狀態為送簽則改底色為黃色
            oRow = CType(e.Row.DataItem, Data.DataRowView)
            Select Case oRow("OTStatus")
                Case "2"
                    BgColor = System.Drawing.Color.Yellow
                    e.Row.BackColor = BgColor
                Case "4"
                    BgColor = System.Drawing.Color.LightGray
                    e.Row.BackColor = BgColor
                    Dim chk As CheckBox = e.Row.FindControl("chk_gvMain")
                    chk.Enabled = False
                Case "5"
                    BgColor = System.Drawing.Color.LightGray
                    e.Row.BackColor = BgColor
                    Dim chk As CheckBox = e.Row.FindControl("chk_gvMain")
                    chk.Enabled = False
                Case "6"
                    BgColor = System.Drawing.Color.LightGray
                    e.Row.BackColor = BgColor
                    Dim chk As CheckBox = e.Row.FindControl("chk_gvMain")
                    chk.Enabled = False
                Case Else
                    BgColor = System.Drawing.Color.White
                    e.Row.BackColor = BgColor
                    e.Row.Cells(4).Style.Add("word-break", "break-all")
            End Select
        End If
    End Sub

    ''' <summary>
    ''' 清除GridView
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ResetGrid()
        'Table顯示

        For i As Integer = 0 To gvMain.Rows.Count - 1 Step 1
            Dim objChk As CheckBox = gvMain.Rows(i).FindControl("chk_gvMain")
            If objChk.Checked Then
                objChk.Checked = False
            End If
        Next
        ScriptManager.RegisterStartupScript(Me, Page.GetType, "script", "gridClear();", True)
    End Sub

#End Region

#Region "子Method"
    ''' <summary>
    ''' 查詢funct
    ''' </summary>
    ''' <param name="Params"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Query(ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim objOV As New OV5
        Dim objOM As New OM1()      'For Date Cehck
        Dim beManageOTDec As New beManageOTDec.Row()
        Dim bsManageOTDec As New beManageOTDec.Service()

        Try
            If funCheckData() Then
                beManageOTDec.OTCompID.Value = hidCompID.Value
                Select Case ddlQueryFactor.SelectedValue.ToString() '依照篩選條件選擇全查還是條件篩選
                    Case ""
                        '查詢所有的加班單
                        If txtEmpID.Text <> "" Then
                            beManageOTDec.OTEmpID.Value = txtEmpID.Text.Trim
                            beManageOTDec.OTRegisterID.Value = txtEmpID.Text.Trim
                        End If

                        If txtOTFormNo.Text.Trim <> "" Then beManageOTDec.OTFormNO.Value = txtOTFormNo.Text.Trim
                        If ddlOTStatus.SelectedValue <> "" Then
                            beManageOTDec.OTStatus.Value = ddlOTStatus.SelectedValue.ToString.Trim
                        Else
                            beManageOTDec.OTStatus.Value = String.Empty
                        End If
                        If ucOTStartDate.DateText.Trim <> "" Then beManageOTDec.OTStartDate.Value = ucOTStartDate.DateText.Trim
                        If ucOTEndDate.DateText.Trim <> "" Then beManageOTDec.OTEndDate.Value = ucOTEndDate.DateText.Trim
                        If ddlSalaryOrAdjust.SelectedValue <> "" Then beManageOTDec.SalaryOrAdjust.Value = ddlSalaryOrAdjust.SelectedValue.Trim
                        Return bsManageOTDec.QueryAll(beManageOTDec).Tables(0)
                    Case "0"
                        '填單人員
                        beManageOTDec.OTRegisterID.Value = txtEmpID.Text.Trim
                    Case "1"
                        '加班人員
                        beManageOTDec.OTEmpID.Value = txtEmpID.Text.Trim
                End Select
                If txtOTFormNo.Text.Trim <> "" Then beManageOTDec.OTFormNO.Value = txtOTFormNo.Text.Trim
                If ddlOTStatus.SelectedValue <> "" Then beManageOTDec.OTStatus.Value = ddlOTStatus.SelectedValue.ToString

                If ucOTStartDate.DateText.Trim <> "" Then beManageOTDec.OTStartDate.Value = ucOTStartDate.DateText.Trim
                If ucOTEndDate.DateText.Trim <> "" Then beManageOTDec.OTEndDate.Value = ucOTEndDate.DateText.Trim

                If ddlSalaryOrAdjust.SelectedValue <> "" Then beManageOTDec.SalaryOrAdjust.Value = ddlSalaryOrAdjust.SelectedValue.Trim

                Return bsManageOTDec.Query(beManageOTDec).Tables(0)
            Else
                '查詢條件不正確
                Debug.Print("Query()-->查詢條件檢查不合格")
                Return bsManageOTDec.Query(beManageOTDec).Tables(0)
            End If
        Catch eq As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & "Query", eq)
            Throw
        End Try
    End Function

#Region "條件檢查"

    ''' <summary>
    ''' 檢查查詢條件
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function funCheckData() As Boolean
        Dim beManageOTDec As New beManageOTDec.Row()
        Dim bsManageOTDec As New beManageOTDec.Service()
        Dim objOM As New OM1()      'For Date Check

        '加班日期
        If ucOTStartDate.DateText = "____/__/__" Then
            ucOTStartDate.DateText = ""
        Else
            If objOM.Check(ucOTStartDate.DateText) Then
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", objOM.rtError)
                ucOTStartDate.DateText = objOM.DateCheck(ucOTStartDate.DateText)
                Return False
            End If
        End If

        If ucOTEndDate.DateText = "____/__/__" Then
            ucOTEndDate.DateText = ""
        Else
            If objOM.Check(ucOTEndDate.DateText) Then
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", objOM.rtError)
                ucOTEndDate.DateText = objOM.DateCheck(ucOTEndDate.DateText)
                Return False
            End If
        End If

        '如果篩選條件不為請選擇則員編為必輸入項
        If ddlQueryFactor.SelectedValue <> "" Then
            If txtEmpID.Text Is Nothing OrElse txtEmpID.Text.Trim = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00030", "員工編號")
                Return False
            End If
        End If

        'If ucOTEndDate.DateTextNoSlash.Trim < ucOTStartDate.DateTextNoSlash.Trim Then
        '    Bsp.Utility.ShowFormatMessage(Me, "請輸入正確的加班日期範圍")
        '    Return False
        'ElseIf ucOTStartDate.DateText.Trim = "" And ucOTEndDate.DateText.Trim = "" Then
        '    '不做任何事
        'Else
        '    Bsp.Utility.ShowFormatMessage(Me, "請輸入完整的加班日期範圍")
        '    If ucOTStartDate.DateText.Trim = "" Then ucOTStartDate.Focus()
        '    If ucOTEndDate.DateText.Trim = "" Then ucOTEndDate.Focus()
        '    Return False
        'End If

        'If ucOTStartDate.DateText.Trim = "" Or ucOTEndDate.DateText.Trim = "" Then
        '    Bsp.Utility.ShowFormatMessage(Me, "請輸入完整的加班日期範圍")
        '    If ucOTStartDate.DateText.Trim = "" Then ucOTStartDate.Focus()
        '    If ucOTEndDate.DateText.Trim = "" Then ucOTEndDate.Focus()
        '    Return False
        'ElseIf ucOTEndDate.DateTextNoSlash.Trim < ucOTStartDate.DateTextNoSlash.Trim Then
        '    Bsp.Utility.ShowFormatMessage(Me, "請輸入正確的加班日期範圍")
        '    Return False
        'Else

        'End If

        Return True
    End Function

    ''' <summary>
    ''' 檢查狀態
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function funCheckStatus() As Boolean
        For RowCount As Integer = 0 To (gvMain.Rows.Count - 1)
            Dim chk As CheckBox = gvMain.Rows(RowCount).FindControl("chk_gvMain")
            If chk.Checked Then
                Select Case ViewState("Action")
                    Case "btnExecutes"  '是送簽
                        If gvMain.DataKeys(RowCount)("OTStatus").ToString() <> "1" Then
                            Bsp.Utility.ShowMessage(Me, "此狀態無法送簽")
                            Return False
                        Else
                            If gvMain.DataKeys(RowCount)("OTStartDate").ToString() > Now.Date.ToString("yyyy/MM/dd") Then
                                Bsp.Utility.ShowMessage(Me, "日期為未來日期，無法送簽")
                                Return False
                            ElseIf gvMain.DataKeys(RowCount)("OTStartDate").ToString() = Now.Date.ToString("yyyy/MM/dd") Then
                                Dim OTStartTime As String = gvMain.DataKeys(RowCount)("OTTime").ToString().Split("~")(0).Replace(":", "")
                                Dim OTEndTime As String = gvMain.DataKeys(RowCount)("OTTime").ToString().Split("~")(1).Replace(":", "")
                                If OTStartTime > Now.ToString("HHmm") Or OTEndTime > Now.ToString("HHmm") Then
                                    Bsp.Utility.ShowMessage(Me, "日期為未來日期，無法送簽")
                                    Return False
                                End If
                            End If
                        End If

                        '若檢查通過則開始送簽前資料檢查(是否超過每月加班時數上限、每日加班時數上限)
                        If funCheckMultiSubmit() = False Then
                            Return False
                        End If
                    Case "btnDelete"  '是刪除
                        If gvMain.DataKeys(RowCount)("OTStatus").ToString() <> "1" Then
                            Bsp.Utility.ShowMessage(Me, "此狀態無法刪除")
                            Return False
                        End If
                    Case "btnReject"    '是取消
                        If gvMain.DataKeys(RowCount)("OTStatus").ToString() <> "2" Then
                            Bsp.Utility.ShowMessage(Me, "此狀態無法取消")
                            Return False
                        End If
                    Case Else           '是來亂的
                        Debug.Print("funCheckStatus()==>有人來亂,Param = " + ViewState("Action").ToString())
                        Return False
                End Select
            End If
        Next
        Return True
    End Function

    ''' <summary>
    ''' 送簽前的資料檢查(是否超過每月加班時數上限、每日加班時數上限)
    ''' </summary>
    ''' <returns>boolean</returns>
    ''' <remarks></remarks>
    Private Function funCheckMultiSubmit() As Boolean
        Dim objOV42 As New OV4_2
        Dim dtData As DataTable = Nothing
        Dim dt As New DataTable
        Dim ErrMsg As String = ""
        dt.Columns.Add("EmpID")
        dt.Columns.Add("OTStartDate")
        dt.Columns.Add("OTEndDate")
        dt.Columns.Add("OTStartTime")
        dt.Columns.Add("OTEndTime")
        dt.Columns.Add("OTCompID")
        'dt.Columns.Add("OTSeq")
        dt.Columns.Add("OTFormNO")
        dt.Columns.Add("OTStatusName")
        dt.Columns.Add("OTFromAdvanceTxnId")    '從事前直接新增一筆資料到事後申報
        dt.Columns.Add("OTTotalTime")
        dt.Columns.Add("OTTxnID")

        Dim OTSeq As String = ""

        '讀取參數檔
        Dim _dtPara As DataTable = objOV42.Json2DataTable(objOV42.QueryColumn("Para", "OverTimePara", " AND CompID = " + Bsp.Utility.Quote(hidCompID.Value.ToString().Trim)))

        Try
            For RowCount As Integer = 0 To (gvMain.Rows.Count - 1)
                Dim chk As CheckBox = gvMain.Rows(RowCount).FindControl("chk_gvMain")
                If chk.Checked Then
                    Dim dr As DataRow = dt.NewRow()
                    dr("EmpID") = gvMain.DataKeys(RowCount).Values("OTEmpID").ToString()
                    dr("OTStartDate") = gvMain.DataKeys(RowCount).Values("OTDate").ToString().Split("~")(0)
                    dr("OTEndDate") = gvMain.DataKeys(RowCount).Values("OTDate").ToString().Split("~")(1)
                    dr("OTStartTime") = (gvMain.DataKeys(RowCount).Values("OTTime").ToString().Split("~")(0)).Replace(":", "")
                    dr("OTEndTime") = (gvMain.DataKeys(RowCount).Values("OTTime").ToString().Split("~")(1)).Replace(":", "")
                    dr("OTTotalTime") = gvMain.DataKeys(RowCount).Values("OTTotalTime").ToString()
                    dr("OTCompID") = gvMain.DataKeys(RowCount).Values("OTCompID").ToString()
                    dr("OTFormNO") = gvMain.DataKeys(RowCount).Values("OTFormNO").ToString()
                    dr("OTStatusName") = gvMain.DataKeys(RowCount).Values("OTStatus").ToString()
                    dr("OTTxnID") = gvMain.DataKeys(RowCount).Values("OTTxnID").ToString()
                    dr("OTFromAdvanceTxnId") = gvMain.DataKeys(RowCount).Values("OTFromAdvanceTxnId").ToString()
                    'If (gvMain.DataKeys(RowCount).Values("OTDate").ToString().Split("~")(0) = gvMain.DataKeys(RowCount).Values("OTDate").ToString().Split("~")(1)) Then
                    '    OTSeq = objOV42.QueryColumn("OTSeq", "OverTimeDeclaration", " AND OTEmpID=" + Bsp.Utility.Quote(gvMain.DataKeys(RowCount).Values("OTEmpID").ToString()) + " AND OTStartDate=" + Bsp.Utility.Quote(gvMain.DataKeys(RowCount).Values("OTDate").ToString().Split("~")(0)) + " AND OTEndDate=" + Bsp.Utility.Quote(gvMain.DataKeys(RowCount).Values("OTDate").ToString().Split("~")(1)) + " AND OTStartTime=" + Bsp.Utility.Quote((gvMain.DataKeys(RowCount).Values("OTTime").ToString().Split("~")(0)).Replace(":", "")) + " AND OTEndTime=" + Bsp.Utility.Quote((gvMain.DataKeys(RowCount).Values("OTTime").ToString().Split("~")(1)).Replace(":", "")) + " AND OTStatus=" + Bsp.Utility.Quote(gvMain.DataKeys(RowCount).Values("OTStatus").ToString()))
                    'Else
                    '    OTSeq = objOV42.QueryColumn("OTSeq", "OverTimeDeclaration", " AND OTEmpID=" + Bsp.Utility.Quote(gvMain.DataKeys(RowCount).Values("OTEmpID").ToString()) + " AND OTStartDate=" + Bsp.Utility.Quote(gvMain.DataKeys(RowCount).Values("OTDate").ToString().Split("~")(0)) + " AND OTEndDate=" + Bsp.Utility.Quote(gvMain.DataKeys(RowCount).Values("OTDate").ToString().Split("~")(0)) + " AND OTStartTime=" + Bsp.Utility.Quote((gvMain.DataKeys(RowCount).Values("OTTime").ToString().Split("~")(0)).Replace(":", "")) + " AND OTEndTime='2359' AND OTStatus=" + Bsp.Utility.Quote(gvMain.DataKeys(RowCount).Values("OTStatus").ToString()))
                    'End If
                    'dr("OTSeq") = OTSeq

                    dt.Rows.Add(dr)
                End If
            Next

            If dt.Rows.Count > 0 Then
                For i = 0 To (dt.Rows.Count - 1)
                    '檢查每天的加班時數是否超出上限
                    Dim dayNLimit As Double = Convert.ToDouble(_dtPara.Rows(0)("DayLimitHourN").ToString()) '平日加班上限
                    Dim dayHLimit As Double = Convert.ToDouble(_dtPara.Rows(0)("DayLimitHourH").ToString())    '假日加班上限
                    Dim strCheckOverTimeIsOver As String = objOV42.GetCheckOverTimeIsOver(dt, dayNLimit, dayHLimit, "OverTimeDeclaration")
                    If Not Convert.ToBoolean(strCheckOverTimeIsOver.Split(";")(0)) Then
                        'Bsp.Utility.ShowMessage(Me, "員工編號(" + strCheckOverTimeIsOver.Split(";")(1) + ")" + strCheckOverTimeIsOver.Split(";")(2) + "已超過每天上限加班時數" + strCheckOverTimeIsOver.Split(";")(3) + "小時")
                        ErrMsg += "員工編號(" + strCheckOverTimeIsOver.Split(";")(1) + ")" + strCheckOverTimeIsOver.Split(";")(2) + "已超過每天上限加班時數" + strCheckOverTimeIsOver.Split(";")(3) + "小時" & vbNewLine
                        If _dtPara.Rows(0)("MonthLimitFlag").ToString() = "1" Then
                            'Return False
                        End If
                    End If

                    '檢查每個月的加班時數是否超出上限
                    Dim resultMsg As String = objOV42.GetMulitTotal(dt, Convert.ToDouble(_dtPara.Rows(0)("MonthLimitHour").ToString()), "OverTimeDeclaration")
                    If Not Convert.ToBoolean(resultMsg.Split(";")(0)) Then
                        'Bsp.Utility.ShowMessage(Me, "員工編號(" + resultMsg.Split(";")(1) + ")" + (resultMsg.Split(";")(2)).ToString().Substring(5, 2) + "月已超過每月上限加班時數" + _dtPara.Rows(0)("MonthLimitHour") + "小時")
                        ErrMsg += "員工編號(" + resultMsg.Split(";")(1) + ")" + (resultMsg.Split(";")(2)).ToString().Substring(5, 2) + "月已超過每月上限加班時數" + _dtPara.Rows(0)("MonthLimitHour") + "小時" & vbNewLine
                        If (_dtPara.Rows(0)("MonthLimitFlag").ToString() = "1") Then
                            'Return False
                        End If
                    End If

                    '檢查連續上班是否超過限制
                    Dim strGetCheckOTLimitDay As String = objOV42.GetCheckOTLimitDay(dt, _dtPara.Rows(0)("OTLimitDay").ToString(), "OverTimeDeclaration")
                    If Not Convert.ToBoolean(strGetCheckOTLimitDay.Split(";"c)(0)) Then
                        'Bsp.Utility.ShowMessage(Me, "員工編號(" + strGetCheckOTLimitDay.Split(";"c)(1) + ")" + "不得連續上班超過" + _dtPara.Rows(0)("OTLimitDay").ToString() + "天")
                        ErrMsg += "員工編號(" + strGetCheckOTLimitDay.Split(";"c)(1) + ")" + "不得連續上班超過" + _dtPara.Rows(0)("OTLimitDay").ToString() + "天" & vbNewLine
                        If _dtPara.Rows(0)("OTLimitFlag").ToString() = "1" Then
                            'Return False
                        End If
                    End If

                Next

                If ErrMsg <> "" Then
                    Bsp.Utility.ShowMessage(Me, ErrMsg)
                End If
            End If
        Catch ex As Exception
            Debug.Print("funCheckMultiSubmit()==>" + ex.Message)
            Bsp.Utility.ShowMessage(Me, "送簽失敗!!")
            Return False
        End Try
        Return True
    End Function


#End Region

    ''' <summary>
    ''' 檢核後的提示訊息
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub funPopupNotify()
        Select Case ViewState("Action")
            'Case "btnAdd"    '是暫存
            '    Bsp.Utility.RunClientScript(Me.Page, "TempSaveAsk();")
            '    'Return False
            Case "btnExecutes"  '是送簽
                Bsp.Utility.RunClientScript(Me.Page, "SubmitAsk();")
                'Return False
            Case "btnDelete"  '是刪除
                Bsp.Utility.RunClientScript(Me.Page, "DeleteAsk();")
                'Return False
            Case "btnReject"    '是取消
                Bsp.Utility.RunClientScript(Me.Page, "RejectAsk();")
            Case Else           '是來亂的
                Debug.Print("funPopupNotify()==>有人來亂,Param = " + ViewState("Param").ToString())
                'Return False
        End Select
    End Sub

    ''' <summary>
    ''' 跳轉執行Funct
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnTransportAsk_Click(sender As Object, e As System.EventArgs) Handles btnTransportAsk.Click
        If ViewState("Action") <> "" Then
            Select Case ViewState("Action")
                Case "btnExecutes"  '是送簽
                    Bsp.Utility.RunClientScript(Me.Page, "Submit();")
                Case "btnDelete"  '是刪除
                    Bsp.Utility.RunClientScript(Me.Page, "Delete();")
                Case "btnReject"    '是取消
                    Bsp.Utility.RunClientScript(Me.Page, "Reject();")
                Case Else           '是來亂的
                    Debug.Print("btnTransportAsk_Click()==>有人來亂,Param = " + ViewState("Action").ToString())
                    'Return False
            End Select
        End If
    End Sub

#Region "送簽相關"

    ''' <summary>
    ''' 送簽前組資料
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click
        Try
            If selectedRows(gvMain) = String.Empty Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00000")
            Else
                Dim jsonlist As New List(Of Dictionary(Of String, String))()
                For RowCount As Integer = 0 To (gvMain.Rows.Count - 1)
                    Dim chk As CheckBox = gvMain.Rows(RowCount).FindControl("chk_gvMain")
                    If chk.Checked Then
                        If gvMain.DataKeys(RowCount)("OTStatus").ToString() = "1" Then

                            Debug.Print("==============執行送簽==============")
                            Debug.Print("OTTimeLength" + gvMain.DataKeys(RowCount)("OTTime").ToString().Length.ToString())
                            Debug.Print("OTTime=" + gvMain.DataKeys(RowCount)("OTTime").ToString())
                            Debug.Print(gvMain.DataKeys(RowCount)("OTTime").ToString().Substring(0, 2).ToString() + gvMain.DataKeys(RowCount)("OTTime").ToString().Substring(3, 2))
                            Debug.Print(gvMain.DataKeys(RowCount)("OTTime").ToString().Substring(6, 2).ToString() + gvMain.DataKeys(RowCount)("OTTime").ToString().Substring(9, 2))
                            Debug.Print("====================================")

                            Dim Dictionary As New Dictionary(Of String, String)
                            Dictionary.Add("OTCompID", gvMain.DataKeys(RowCount)("OTCompID").ToString())
                            Dictionary.Add("OTEmpID", gvMain.DataKeys(RowCount)("OTEmpID").ToString())
                            Dictionary.Add("OTStartDate", gvMain.DataKeys(RowCount)("OTDate").ToString().ToString().Split("~")(0))
                            Dictionary.Add("OTEndDate", gvMain.DataKeys(RowCount)("OTDate").ToString().ToString().Split("~")(1))
                            Dictionary.Add("OTStartTime", gvMain.DataKeys(RowCount)("OTTime").ToString().Split("~")(0).Replace(":", ""))
                            Dictionary.Add("OTEndTime", gvMain.DataKeys(RowCount)("OTTime").ToString().Split("~")(1).Replace(":", ""))
                            Dictionary.Add("OTSeq", gvMain.DataKeys(RowCount)("OTSeq").ToString())
                            Dictionary.Add("OTRegisterID", gvMain.DataKeys(RowCount)("OTRegisterID").ToString())
                            Dictionary.Add("OTRegisterComp", gvMain.DataKeys(RowCount)("OTRegisterComp").ToString())
                            Dictionary.Add("OTTxnID", gvMain.DataKeys(RowCount)("OTTxnID").ToString())
                            jsonlist.Add(Dictionary)
                        Else
                            Bsp.Utility.ShowMessage(Me, "此狀態無法送簽")
                        End If


                    End If
                Next

                If jsonlist.Count > 0 Then
                    Dim DataList As New Dictionary(Of String, List(Of Dictionary(Of String, String)))
                    DataList.Add("DataList", jsonlist)

                    hidGuidID.Value = Guid.NewGuid().ToString()

                    Dim sb As New StringBuilder
                    sb.Append("INSERT INTO CacheData (Platform,SystemID,TxnName,UserID,CacheID,CacheData,CacheDT,Aging) ")
                    sb.Append(" VALUES('AP', 'OT', 'OV4200', '" + UserProfile.ActUserID + "', '" + hidGuidID.Value + "', '" + Newtonsoft.Json.JsonConvert.SerializeObject(DataList) + "', GETDATE(), '30')")
                    Bsp.DB.ExecuteNonQuery(CommandType.Text, sb.ToString, "AattendantDB")

                    Bsp.Utility.RunClientScript(Me.Page, "callashx();")
                End If

                'If intSelectCount = 1 Then
                '    hidOTEmpID.Value = gvMain.DataKeys(intSelectRow)("OTEmpID").ToString()
                'Else
                '    Bsp.Utility.ShowMessage(Me, "請選擇一筆送簽")
                'End If
            End If
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & "DoExecute()", ex)
        End Try
    End Sub

#Region "取消送簽"
    ''' <summary>
    ''' 執行取消
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnReject_Click(sender As Object, e As System.EventArgs) Handles btnReject.Click
        Try
            For RowCount As Integer = 0 To (gvMain.Rows.Count - 1)
                Dim chk As CheckBox = gvMain.Rows(RowCount).FindControl("chk_gvMain")
                If chk.Checked Then
                    If gvMain.DataKeys(RowCount)("OTStatus").ToString() <> "2" Then
                        Bsp.Utility.ShowMessage(Me, "此狀態無法取消!")
                    Else
                        DoReject()
                    End If
                Else
                End If
            Next
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' 執行送簽取消的子Funct
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DoReject() As Boolean
        Dim beManageOTDec As New beManageOTDec.Row()
        Dim bsManageOTDec As New beManageOTDec.Service()

        Try
            If selectedRows(gvMain) = "-1" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00000")
                Return False
            Else
                For RowCount As Integer = 0 To (gvMain.Rows.Count - 1)
                    Dim chk As CheckBox = gvMain.Rows(RowCount).FindControl("chk_gvMain")
                    If chk.Checked Then
                        Dim OTTxnID As String
                        OTTxnID = If(gvMain.DataKeys(RowCount)("OTTxnID").ToString() IsNot Nothing, gvMain.DataKeys(RowCount)("OTTxnID").ToString(), "")
                        beManageOTDec.OTTxnID.Value = OTTxnID.Trim
                        beManageOTDec.OTStatus.Value = "6"
                        beManageOTDec.LastChgComp.Value = UserProfile.ActCompID.Trim
                        beManageOTDec.LastChgDate.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
                        beManageOTDec.LastChgID.Value = UserProfile.UserID.Trim

                        Try
                            If DoOTDecSetting("Reject", beManageOTDec) Then
                                Dim strFlowCaseID As String = gvMain.DataKeys(RowCount)("FlowCaseID").ToString()
                                Dim strSQL As String = "select TOP 1 * from " + Config_AattendantDBFlowFullLog + " where  FlowCaseID='" + strFlowCaseID + "' ORDER BY FlowLogBatNo DESC"
                                Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL, "AattendantDB").Tables(0)
                                    If dt.Rows.Count > 0 Then
                                        Dim strLastLogBatNo As String = (Convert.ToInt32(dt.Rows(0).Item("FlowLogBatNo").ToString()) + 1).ToString()
                                        Dim strLastLogSeqNo As String = (Convert.ToInt32(dt.Rows(0).Item("FlowLogBatNo").ToString()) + 1).ToString()
                                        Dim strIsProxy As String = "N"

                                        Dim sb As New StringBuilder
                                        sb.AppendLine("UPDATE " + Config_AattendantDBFlowCase + " SET FlowCaseStatus='Close',FlowCurrStepID='Z03',FlowCurrStepName='取消結案',")
                                        sb.AppendLine(" LastLogBatNo='" + strLastLogBatNo + "',LastLogSeqNo='" + strLastLogSeqNo + "',UpdDateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'")
                                        sb.AppendLine(" WHERE FlowCaseID='" + strFlowCaseID + "'")

                                        sb.AppendLine("UPDATE " + Config_AattendantDBFlowFullLog + " SET FlowLogIsClose='Y',FlowStepBtnID='btnCancel',FlowStepBtnCaption='取消結案'")
                                        sb.AppendLine(" ,LogUpdDateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'")
                                        sb.AppendLine(" ,ToDept='" + UserProfile.ActDeptID + "'")
                                        sb.AppendLine(" ,ToDeptName='" + UserProfile.ActDeptName + "'")
                                        sb.AppendLine(" ,ToUser='" + UserProfile.ActUserID + "'")
                                        sb.AppendLine(" ,ToUserName='" + UserProfile.ActUserName + "'")
                                        sb.AppendLine(" ,IsProxy='" + strIsProxy + "'")
                                        sb.AppendLine(" WHERE FlowLogID='" + dt.Rows(0).Item("FlowLogID").ToString() + "'")

                                        Dim b As String = Convert.ToString(strFlowCaseID + "." + (Convert.ToInt32(dt.Rows(0).Item("FlowLogBatNo").ToString()) + 1).ToString("00000"))

                                        sb.AppendLine(" INSERT INTO " + Config_AattendantDBFlowFullLog + " (FlowCaseID,FlowLogBatNo,FlowLogID,FlowStepID,FlowStepName,FlowStepBtnID,FlowStepBtnCaption,FlowStepOpinion,FlowLogIsClose,IsProxy,AttachID,FromDept,FromDeptName,FromUser,FromUserName,AssignTo,AssignToName,ToDept,ToDeptName,ToUser,ToUserName,LogCrDateTime,LogUpdDateTime,LogRemark) ")
                                        sb.AppendLine(" VALUES('" + strFlowCaseID + "', '" + strLastLogBatNo + "', '" + b + "',")
                                        sb.AppendLine(" 'Z03','取消結案','','','','Y','','',")
                                        sb.AppendLine(" '" + UserProfile.ActDeptID + "','" + UserProfile.ActDeptName + "','" + UserProfile.ActUserID + "','" + UserProfile.ActUserName + "','" + UserProfile.ActUserID + "','" + UserProfile.ActUserName + "','" + UserProfile.ActDeptID + "','" + UserProfile.ActDeptName + "','" + UserProfile.ActUserID + "','" + UserProfile.ActUserName + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',")
                                        sb.AppendLine(" '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','')")

                                        Bsp.DB.ExecuteNonQuery(CommandType.Text, sb.ToString, "AattendantDB")
                                    End If
                                End Using
                            End If
                        Catch ex As Exception
                            Bsp.Utility.ShowMessage(Me, Me.FunID & "DeleteOTTypeSetting()", ex)
                            Return False
                        End Try
                    End If
                Next
                DoQuery()
                ResetGrid()
                Return False
            End If

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & "Delete", ex)
            Return False
        End Try
    End Function

#End Region

#Region "清除送簽快取"

    '清除送簽快取
    Protected Sub ClearSubmitCache_Click(sender As Object, e As System.EventArgs) Handles ClearSubmitCache.Click
        ClearCacheData()
    End Sub

    ''' <summary>
    ''' 清除送簽快取
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ClearCacheData() As Boolean
        Dim sb As New StringBuilder
        sb.Append("DELETE * FROM CacheData WHERE 1=1 AND UserID = " + Bsp.Utility.Quote(UserProfile.ActUserID) + " AND Platform = 'AP' AND SystemID = 'OT' AND TxnName = 'OV4200'")
        Try
            Dim tran As Integer = 0
            tran = Bsp.DB.ExecuteNonQuery(CommandType.Text, sb.ToString, "AattendantDB")
            If tran <> 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Debug.Print("ClearCache()-->" + ex.Message)
            Return False
        End Try
    End Function

#End Region

#End Region

#Region "刪除暫存單"
    ''' <summary>
    ''' 執行刪除
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnDelete_Click(sender As Object, e As System.EventArgs) Handles btnDelete.Click
        Try
            For RowCount As Integer = 0 To (gvMain.Rows.Count - 1)
                Dim chk As CheckBox = gvMain.Rows(RowCount).FindControl("chk_gvMain")
                If chk.Checked Then
                    If gvMain.DataKeys(RowCount)("OTStatus").ToString() <> "1" Then
                        Bsp.Utility.ShowMessage(Me, "此狀態無法刪除!")
                    Else
                        Delete()
                    End If
                Else
                End If
            Next
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID, ex)
        End Try
    End Sub

    ''' <summary>
    ''' 刪除子Funct
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Delete() As Boolean
        Dim beManageOTDec As New beManageOTDec.Row()
        Dim bsManageOTDec As New beManageOTDec.Service()

        Try
            If selectedRows(gvMain) = "-1" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00000")
                Return False
            Else
                For RowCount As Integer = 0 To (gvMain.Rows.Count - 1)
                    Dim chk As CheckBox = gvMain.Rows(RowCount).FindControl("chk_gvMain")
                    If chk.Checked Then
                        Dim OTTxnID As String
                        OTTxnID = If(gvMain.DataKeys(RowCount)("OTTxnID").ToString() IsNot Nothing, gvMain.DataKeys(RowCount)("OTTxnID").ToString(), "")
                        beManageOTDec.OTTxnID.Value = OTTxnID.Trim
                        beManageOTDec.OTStatus.Value = "5"
                        beManageOTDec.LastChgComp.Value = UserProfile.ActCompID.Trim
                        beManageOTDec.LastChgDate.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
                        beManageOTDec.LastChgID.Value = UserProfile.UserID.Trim

                        Try
                            If DoOTDecSetting("Delete", beManageOTDec) Then

                            End If
                        Catch ex As Exception
                            Bsp.Utility.ShowMessage(Me, Me.FunID & "DoOTDecSetting", ex)
                            Return False
                        End Try
                    End If
                Next
                DoQuery()
                ResetGrid()
                Return False
            End If
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & "Delete", ex)
            Return False
        End Try
    End Function
#End Region

    ''' <summary>
    ''' 執行更新資料庫動作
    ''' </summary>
    ''' <param name="action"></param>
    ''' <param name="beManageOTDec"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DoOTDecSetting(ByVal action As String, ByVal beManageOTDec As beManageOTDec.Row) As Boolean
        Dim bsManageOTDec As New beManageOTDec.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("AattendantDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                bsManageOTDec.UpdateStatus(beManageOTDec, tran)
                tran.Commit()
            Catch ex As Exception
                tran.Rollback()
                Throw New Exception(ex.Message)
            Finally
                If tran IsNot Nothing Then tran.Dispose()
            End Try
        End Using
        Return True
    End Function
#End Region

    ''' <summary>
    ''' 隱藏的查詢按鈕
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnQuery_Click(sender As Object, e As System.EventArgs) Handles btnQuery.Click
        DoQuery()
    End Sub
End Class
