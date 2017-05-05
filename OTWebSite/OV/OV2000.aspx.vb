'*********************************************************************************
'功能說明：加班時數統計查詢
'建立人員：Kevin
'建立日期：2017.1.24
'*********************************************************************************

Imports System.Data
Imports System.Globalization

Partial Class OV2000
    Inherits PageBase

#Region "全域變數"
    Private Property eHRMSDB_ITRD As String
        Get
            Dim result As String = ConfigurationManager.AppSettings("eHRMSDB")
            If String.IsNullOrEmpty(result) Then
                result = "eHRMSDB_ITRD"
            End If
            Return result
        End Get
        Set(ByVal value As String)

        End Set
    End Property

    Private Property AattendantDB As String
        Get
            Dim result As String = ConfigurationManager.AppSettings("AattendantDB")
            If String.IsNullOrEmpty(result) Then
                result = "AattendantDB"
            End If
            Return result
        End Get
        Set(ByVal value As String)

        End Set
    End Property
#End Region

#Region "功能選單"
    ''' <summary>
    ''' 功能清單
    ''' </summary>
    ''' <param name="Param"></param>
    ''' <remarks></remarks>
    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnQuery"     '查詢
                If funCheckQueryData() = True Then
                    DoQuery()
                End If
            Case "btnActionX"   '清除
                DoClear()
        End Select
    End Sub

#Region "功能群組"

    ''' <summary>
    ''' 查詢
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DoQuery()
        Dim OV2 As OV2 = SetTableProperty()

        If ddlType.SelectedValue.Equals("detail") And funCheckData() Then
            Dim dt As DataTable = OV2.getOV2000_Detial_DB()

            If dt.Rows.Count > 0 Then
                dt = (From cust In dt.AsEnumerable() _
                Order By cust.Field(Of String)("OVADate") Ascending, cust.Field(Of String)("OVATime") Ascending, cust.Field(Of String)("EmpID") Ascending).CopyToDataTable()

                '***************************' 201702/17Rebecca modify
                Dim reMoveRowForOVDDBList As DataTable = dt.Clone
                '移除中
                For i = dt.Rows.Count - 1 To 0 Step -1

                    If dt.Rows(i)("OVADate").ToString() = "" Then
                        reMoveRowForOVDDBList.ImportRow(dt.Rows(i))
                        dt.Rows.Remove(dt.Rows(i))
                    End If
                Next
                '加回去
                For i = 0 To reMoveRowForOVDDBList.Rows.Count - 1 Step 1
                    dt.ImportRow(reMoveRowForOVDDBList.Rows(i))
                Next
                '***************************'
                pcMain.DataTable = dt
                gvMain.DataBind()
            End If


            gvMain.DataBind()
            div_tb.Style.Add("display", "block")
            div_tb1.Style.Add("display", "none")
        ElseIf ddlType.SelectedValue.Equals("statistics") And funCheckData() Then
            Dim dt As DataTable = OV2.getOV2000_Statistics_DB()

            pcMain1.DataTable = dt
            gvMain1.DataBind()
            div_tb.Style.Add("display", "none")
            div_tb1.Style.Add("display", "block")
        Else

            div_tb.Style.Add("display", "none")
            div_tb1.Style.Add("display", "none")
        End If
        ViewState.Item("DoQuery") = "Y"

    End Sub

    ''' <summary>
    '''  清除資料
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DoClear()
        If Not pcMain.DataTable Is Nothing Then
            pcMain.DataTable.Clear()
            pcMain.BindGridView()
        End If
        If Not pcMain1.DataTable Is Nothing Then
            pcMain1.DataTable.Clear()
            pcMain1.BindGridView()
        End If
        subLoadOrganColor(ddlDeptID, UserProfile.SelectCompRoleID)
        If ddlDeptID.SelectedValue = "" Then
            Bsp.Utility.FillDDL(ddlOrganID, "eHRMSDB", "Organization", "RTRIM(OrganID)", "OrganName", Bsp.Utility.DisplayType.Full, "", " And InValidFlag = '0' And VirtualFlag = '0' And CompID = " & Bsp.Utility.Quote(UserProfile.SelectCompRoleID) & " AND ((OrganID = OrganID AND OrganID <> DeptID AND OrganID <> OrgType)OR(OrganID = OrganID AND OrganID = DeptID AND OrganID = OrgType)) ", "Order By OrganID")
            ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
        Else
            Bsp.Utility.FillDDL(ddlOrganID, "eHRMSDB", "Organization", "RTRIM(OrganID)", "OrganName", Bsp.Utility.DisplayType.Full, "", " And InValidFlag = '0' And VirtualFlag = '0' And CompID = " & Bsp.Utility.Quote(UserProfile.SelectCompRoleID) & " And DeptID = " & Bsp.Utility.Quote(ddlDeptID.SelectedValue) & " AND ((OrganID = OrganID AND OrganID <> DeptID AND OrganID <> OrgType)OR(OrganID = OrganID AND OrganID = DeptID AND OrganID = OrgType)) ", "Order By OrganID")
            ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
        End If

        Bsp.Utility.FillDDL(ddlPositionID, "eHRMSDB", "Position", "distinct PositionID", "Remark", Bsp.Utility.DisplayType.Full, "", "And CompID =" + Bsp.Utility.Quote(UserProfile.SelectCompRoleID), "") '職位
        ddlPositionID.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        ddlOTStatus.Enabled = True
        ddlOTStatus1.Enabled = True
        tbOTPayDate.Enabled = True

        ddlTitleIDMIN.Items.Clear()
        ddlTitleIDMAX.Items.Clear()

        ddlTitleIDMIN.Items.Insert(0, New ListItem("---請先選擇職等---", ""))
        ddlTitleIDMAX.Items.Insert(0, New ListItem("---請先選擇職等---", ""))


        ddlType.SelectedIndex = 0
        ddlCompID.SelectedIndex = 0
        ddlOrgType.SelectedIndex = 0
        ddlDeptID.SelectedIndex = 0
        ddlOrganID.SelectedIndex = 0
        ddlRankIDMIN.SelectedIndex = 0
        ddlRankIDMAX.SelectedIndex = 0

        '20170304kevin
        ddlTitleIDMIN.SelectedIndex = 0
        ddlTitleIDMAX.SelectedIndex = 0


        'ddlTitleName.SelectedIndex = 0
        ddlPositionID.SelectedIndex = 0
        ddlOTStatus.SelectedIndex = 0
        ddlWorkStatus.SelectedIndex = 0
        ddlOTStatus1.SelectedIndex = 0
        tbOTPayDate.Text = ""
        tbOTEmpID.Text = ""
        tbOTEmpName.Text = ""
        tbOTFormNO.Text = ""
        txtOvertimeDateB.DateText = ""
        txtOvertimeDateE.DateText = ""
        ddlSalaryOrAdjust.SelectedValue = ""
        'tbOTPayDateErrorMsg.Visible = False

    End Sub

#End Region

#End Region

#Region "載入頁面"

    ''' <summary>
    ''' 頁面進入點
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            '公司代碼
            If UserProfile.SelectCompRoleID = "ALL" Then
                lblCompRoleID.Visible = False
                Bsp.Utility.FillHRCompany(ddlCompID)
                Page.SetFocus(ddlCompID)
            Else
                lblCompRoleID.Text = UserProfile.SelectCompRoleName
                ddlCompID.Visible = False
            End If
            '員工編號
            ucQueryEmpID.ShowCompRole = "False"
            ucQueryEmpID.InValidFlag = "N"
            ucQueryEmpID.SelectCompID = UserProfile.SelectCompRoleID

            LoadDate()

            If ddlType.SelectedValue.Equals("detail") Then
                div_tb.Style.Add("display", "block")
                div_tb1.Style.Add("display", "none")
            Else
                div_tb.Style.Add("display", "none")
                div_tb1.Style.Add("display", "block")
            End If
        Else
            subReLoadColor(ddlOrgType)
            subReLoadColor(ddlDeptID)
        End If
    End Sub

    ''' <summary>
    ''' 返回事件
    ''' </summary>
    ''' <param name="ti"></param>
    ''' <remarks></remarks>
    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

            For Each strKey As String In ht.Keys
                Dim ctl As Control = Me.FindControl(strKey)
                If TypeOf ctl Is TextBox Then
                    CType(ctl, TextBox).Text = ht(strKey).ToString()
                ElseIf TypeOf ctl Is DropDownList Then
                    If ht(strKey).ToString <> "" Then
                        CType(ctl, DropDownList).SelectedValue = ht(strKey).ToString
                    End If
                ElseIf TypeOf ctl Is CheckBox Then
                    CType(ctl, CheckBox).Checked = ht(strKey).ToString
                ElseIf TypeOf ctl Is Component_ucCalender Then
                    CType(ctl, Component_ucCalender).DateText = ht(strKey).ToString()
                End If
            Next


            'If Regex.IsMatch(tbOTPayDate.Text, "^\d{6}$") Or "".Equals(tbOTPayDate.Text.Trim) Then
            '    tbOTPayDateErrorMsg.Visible = False
            'Else
            '    tbOTPayDateErrorMsg.Visible = True
            'End If

            If ht.ContainsKey("DoQuery") Then
                If ht("DoQuery").ToString() = "Y" Then
                    ViewState.Item("DoQuery") = "Y"
                    DoQuery()
                End If
            End If

        End If
    End Sub

    ''' <summary>
    ''' 載入資料
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadDate()
        Dim CompID As String = UserProfile.SelectCompRoleID


        Bsp.Utility.FillDDL(ddlCompID, "eHRMSDB", "Company", "CompID", "CompName")
        ddlCompID.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        'Bsp.Utility.FillDDL(ddlRankIDMAX, "eHRMSDB", "Title", "RankID", "RankID", Bsp.Utility.DisplayType.OnlyID, , "And CompID = " + Bsp.Utility.Quote(CompID), "group by RankID")
        'ddlRankIDMAX.Items.Insert(0, New ListItem("--", ""))
        'Bsp.Utility.FillDDL(ddlRankIDMIN, "eHRMSDB", "Title", "RankID", "RankID", Bsp.Utility.DisplayType.OnlyID, , "And CompID = " + Bsp.Utility.Quote(CompID), "group by RankID")
        'ddlRankIDMIN.Items.Insert(0, New ListItem("--", ""))
        'Bsp.Utility.FillDDL(ddlTitleName, "eHRMSDB", "Title", "TitleID+'-'+TitleName", "TitleID+'-'+TitleName", Bsp.Utility.DisplayType.OnlyID, , "And CompID = " + Bsp.Utility.Quote(CompID), "group by TitleID,TitleName") '職稱
        'ddlTitleName.Items.Insert(0, New ListItem("　- -請選擇- -", ""))

        '20170304 kevin
        Bsp.Utility.FillDDL(ddlRankIDMAX, "eHRMSDB", "Rank", "RankID", "RankID", Bsp.Utility.DisplayType.OnlyID, , "And CompID = " + Bsp.Utility.Quote(UserProfile.SelectCompRoleID), "group by RankID")
        ddlRankIDMAX.Items.Insert(0, New ListItem("--", ""))
        Bsp.Utility.FillDDL(ddlRankIDMIN, "eHRMSDB", "Rank", "RankID", "RankID", Bsp.Utility.DisplayType.OnlyID, , "And CompID = " + Bsp.Utility.Quote(UserProfile.SelectCompRoleID), "group by RankID")
        ddlRankIDMIN.Items.Insert(0, New ListItem("--", ""))
        ddlTitleIDMAX.Items.Insert(0, New ListItem("---請先選擇職等---", ""))
        ddlTitleIDMIN.Items.Insert(0, New ListItem("---請先選擇職等---", ""))


        Bsp.Utility.FillDDL(ddlPositionID, "eHRMSDB", "Position", "distinct PositionID", "Remark", Bsp.Utility.DisplayType.Full, "", "And CompID =" + Bsp.Utility.Quote(CompID), "") '職位
        ddlPositionID.Items.Insert(0, New ListItem("　- -請選擇- -", ""))

        Bsp.Utility.FillDDL(ddlWorkStatus, "eHRMSDB", "WorkStatus", "WorkCode", "Remark", Bsp.Utility.DisplayType.Full, "", "", "") '職位
        ddlWorkStatus.Items.Insert(0, New ListItem("　- -請選擇- -", ""))

        ViewState.Item("OrgTypeColors") = New List(Of ArrayList)()
        ViewState.Item("DeptColors") = New List(Of ArrayList)()
        subLoadOrganColor(ddlDeptID, UserProfile.SelectCompRoleID)
        subLoadOrganColor(ddlOrgType, UserProfile.SelectCompRoleID)
        '科組課
        ddlDept_Changed(Nothing, Nothing)

        Dim str As String = "join " + eHRMSDB_ITRD + ".[dbo].EmpPosition EP ON PO.CompID=EP.CompID AND PO.PositionID=EP.PositionID AND EP.PrincipalFlag='1' join " + eHRMSDB_ITRD + ".[dbo].Personal P ON EP.CompID=P.CompID AND EP.EmpID=P.EmpID  join " + eHRMSDB_ITRD + ".[dbo].Organization ORT ON  P.OrganID=ORT.OrganID AND P.CompID =ORT.CompID"

        If (Not ddlOrganID.SelectedValue.ToString.Trim.Equals("")) Then
            Bsp.Utility.FillDDL(ddlPositionID, "eHRMSDB", "Position PO", "distinct PO.PositionID", "PO.Remark", Bsp.Utility.DisplayType.Full, str, "And PO.CompID = " + Bsp.Utility.Quote(CompID) + " and ORT.OrganID= '" + ddlOrganID.SelectedValue + "'", "Order By  PO.PositionID") '職位
            ddlPositionID.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        ElseIf (Not ddlDeptID.SelectedValue.ToString.Trim.Equals("")) Then
            Bsp.Utility.FillDDL(ddlPositionID, "eHRMSDB", "Position PO", "distinct PO.PositionID", "PO.Remark", Bsp.Utility.DisplayType.Full, str, "And PO.CompID = " + Bsp.Utility.Quote(CompID) + " and P.DeptID= '" + ddlDeptID.SelectedValue + "'", "Order By  PO.PositionID") '職位
            ddlPositionID.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        ElseIf (Not ddlOrgType.SelectedValue.ToString.Trim.Equals("")) Then
            Bsp.Utility.FillDDL(ddlPositionID, "eHRMSDB", "Position PO", "distinct PO.PositionID", "PO.Remark", Bsp.Utility.DisplayType.Full, str, "And PO.CompID = " + Bsp.Utility.Quote(CompID) + " and ORT.OrgType= '" + ddlOrgType.SelectedValue + "'", "Order By  PO.PositionID") '職位
            ddlPositionID.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        Else
            Bsp.Utility.FillDDL(ddlPositionID, "eHRMSDB", "Position PO", "distinct PO.PositionID", "PO.Remark", Bsp.Utility.DisplayType.Full, str, "And PO.CompID = " + Bsp.Utility.Quote(CompID), "Order By  PO.PositionID") '職位
            ddlPositionID.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        End If
    End Sub

    ''' <summary>
    ''' 明細觸發事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvMain_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMain.RowCommand
        If e.CommandName.Equals("OVADetail") Then

            Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
            Dim value = 0
            btnX.Caption = "返回"
            Dim OVADate As String() = gvMain.DataKeys(selectedRow(gvMain))("OVADate").ToString().Split("~")
            Dim OVDDate As String() = gvMain.DataKeys(selectedRow(gvMain))("OVDDate").ToString().Split("~")
            If Not (gvMain.DataKeys(selectedRow(gvMain))("OVADate").ToString().Trim.Equals("")) Then
                Me.TransferFramePage("~/OV/OV2001.aspx", New ButtonState() {btnX}, _
              "hiddenType=" + "bef", _
              "OTCompID=" + gvMain.DataKeys(selectedRow(gvMain))("OTCompID").ToString(), _
              "OTEmpID=" + gvMain.DataKeys(selectedRow(gvMain))("EmpID").ToString(), _
              "OTStartDate=" + OVADate(0), _
              "OTEndDate=" + OVADate(1), _
               "OTTxnID=" + gvMain.DataKeys(selectedRow(gvMain))("OVAOTTxnID").ToString(), _
               ddlType.ID & "=" & ddlType.SelectedValue, _
               ddlOrgType.ID & "=" & ddlOrgType.SelectedValue, _
               ddlDeptID.ID & "=" & ddlDeptID.SelectedValue, _
               ddlOrganID.ID & "=" & ddlOrganID.SelectedValue, _
               ddlWorkStatus.ID & "=" & ddlWorkStatus.SelectedValue, _
               ddlRankIDMIN.ID & "=" & ddlRankIDMIN.SelectedValue, _
               ddlRankIDMAX.ID & "=" & ddlRankIDMAX.SelectedValue, _
               ddlTitleIDMIN.ID & "=" & ddlTitleIDMIN.SelectedValue, _
               ddlTitleIDMAX.ID & "=" & ddlTitleIDMAX.SelectedValue, _
               tbOTEmpID.ID & "=" & tbOTEmpID.Text, _
               tbOTEmpName.ID & "=" & tbOTEmpName.Text, _
               ddlPositionID.ID & "=" & ddlPositionID.SelectedValue, _
               txtOvertimeDateB.ID & "=" & IIf(txtOvertimeDateB.DateText = "____/__/__", "", txtOvertimeDateB.DateText), _
               txtOvertimeDateE.ID & "=" & IIf(txtOvertimeDateE.DateText = "____/__/__", "", txtOvertimeDateE.DateText), _
               ddlOTStatus.ID & "=" & ddlOTStatus.SelectedValue, _
               tbOTFormNO.ID & "=" & tbOTFormNO.Text, _
               tbOTPayDate.ID & "=" & tbOTPayDate.Text, _
                "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
            End If
        ElseIf e.CommandName.Equals("OVDDetail") Then
            Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
            Dim value = 0
            btnX.Caption = "返回"
            Dim OVADate As String() = gvMain.DataKeys(selectedRow(gvMain))("OVADate").ToString().Split("~")
            Dim OVDDate As String() = gvMain.DataKeys(selectedRow(gvMain))("OVDDate").ToString().Split("~")
            If Not (gvMain.DataKeys(selectedRow(gvMain))("OVDDate").ToString().Trim.Equals("")) Then
                Me.TransferFramePage("~/OV/OV2001.aspx", New ButtonState() {btnX}, _
              "hiddenType=" + "after", _
              "OTCompID=" + gvMain.DataKeys(selectedRow(gvMain))("OTCompID").ToString(), _
              "OTEmpID=" + gvMain.DataKeys(selectedRow(gvMain))("EmpID").ToString(), _
              "OTStartDate=" + OVDDate(0), _
              "OTEndDate=" + OVDDate(1), _
               "OTTxnID=" + gvMain.DataKeys(selectedRow(gvMain))("OVDOTTxnID").ToString(), _
               ddlType.ID & "=" & ddlType.SelectedValue, _
               ddlOrgType.ID & "=" & ddlOrgType.SelectedValue, _
               ddlDeptID.ID & "=" & ddlDeptID.SelectedValue, _
               ddlOrganID.ID & "=" & ddlOrganID.SelectedValue, _
               ddlWorkStatus.ID & "=" & ddlWorkStatus.SelectedValue, _
               ddlRankIDMIN.ID & "=" & ddlRankIDMIN.SelectedValue, _
               ddlRankIDMAX.ID & "=" & ddlRankIDMAX.SelectedValue, _
                ddlTitleIDMIN.ID & "=" & ddlTitleIDMIN.SelectedValue, _
               ddlTitleIDMAX.ID & "=" & ddlTitleIDMAX.SelectedValue, _
               tbOTEmpID.ID & "=" & tbOTEmpID.Text, _
               tbOTEmpName.ID & "=" & tbOTEmpName.Text, _
               ddlPositionID.ID & "=" & ddlPositionID.SelectedValue, _
               txtOvertimeDateB.ID & "=" & IIf(txtOvertimeDateB.DateText = "____/__/__", "", txtOvertimeDateB.DateText), _
               txtOvertimeDateE.ID & "=" & IIf(txtOvertimeDateE.DateText = "____/__/__", "", txtOvertimeDateE.DateText), _
               ddlOTStatus.ID & "=" & ddlOTStatus.SelectedValue, _
               tbOTFormNO.ID & "=" & tbOTFormNO.Text, _
               tbOTPayDate.ID & "=" & tbOTPayDate.Text, _
                "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))

            End If
        End If
    End Sub

#End Region

#Region "資料檢核"

    ''' <summary>
    ''' 日期檢核
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function funCheckQueryData() As Boolean
        If tbOTPayDate.Text.ToString.Trim <> "" Then
            If Bsp.Utility.CheckDate(tbOTPayDate.Text + "01") = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00060", lblPayDate.Text)
                tbOTPayDate.Text = ""
                tbOTPayDate.Focus()
                Return False
            End If
        End If

        If DateStrF(txtOvertimeDateB.DateText.ToString).Replace("/", "").Trim <> "" Then
            If Bsp.Utility.CheckDate(txtOvertimeDateB.DateText.ToString.Replace("/", "")) = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00060", "起日")
                txtOvertimeDateB.DateText = ""
                txtOvertimeDateB.Focus()
                Return False
            End If
        End If

        If DateStrF(txtOvertimeDateE.DateText.ToString).Replace("/", "").Trim <> "" Then
            If Bsp.Utility.CheckDate(txtOvertimeDateE.DateText.ToString.Replace("/", "")) = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00060", "迄日")
                txtOvertimeDateE.DateText = ""
                txtOvertimeDateE.Focus()
                Return False
            End If
        End If

        Return True
    End Function

    ''' <summary>
    ''' 資料檢核
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function funCheckData() As Boolean
        Dim checkData As Boolean = True
        '加班日期
        checkData = funCheckDate(txtOvertimeDateB, txtOvertimeDateE, labOvertimeDate.Text)
        If Not checkData Then
            Return False
        End If


        If Not checkData Then
            Return False
        End If
        If lblRankNotice.Visible Then
            Bsp.Utility.ShowFormatMessage(Me, "H_00000", "職等選擇請由小到大")
            Return False
        End If
        Return checkData
    End Function

    ''' <summary>
    ''' 日期檢核
    ''' </summary>
    ''' <param name="ucDateB"></param>
    ''' <param name="ucDateE"></param>
    ''' <param name="strLabel"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function funCheckDate(ByVal ucDateB As Component_ucCalender, ByVal ucDateE As Component_ucCalender, ByVal strLabel As String) As Boolean

        If "____/__/__".Equals(ucDateB.DateText.Trim()) Then
            ucDateB.DateText = ""
        End If
        If "____/__/__".Equals(ucDateE.DateText.Trim()) Then
            ucDateE.DateText = ""
        End If

        If ucDateB.DateText.Trim() <> "" Then
            If Bsp.Utility.CheckDate(ucDateB.DateText.Trim()) = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", strLabel & "(起) 日期格式錯誤")
                ucDateB.Focus()
                Return False
            End If
        End If

        If ucDateE.DateText.Trim() <> "" Then
            If Bsp.Utility.CheckDate(ucDateE.DateText.Trim()) = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", strLabel & "(迄) 日期格式錯誤")
                ucDateE.Focus()
                Return False
            End If
        End If

        If ucDateB.DateText.Trim() <> "" And ucDateE.DateText.Trim() <> "" Then
            If ucDateB.DateText > ucDateE.DateText Then
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", "欄位[" & strLabel & "]起日不可晚於迄日")
                ucDateB.Focus()
                Return False
            End If
        End If

        Return True
    End Function

#End Region

#Region "功能"

    ''' <summary>
    ''' 裝入頁面資料到OV2
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetTableProperty() As OV2
        Dim OV2 As New OV2
        OV2.DetailType = ddlType.SelectedValue
        OV2.CompID = UserProfile.SelectCompRoleID
        OV2.OrgType = ddlOrgType.SelectedValue
        OV2.DeptID = ddlDeptID.SelectedValue
        OV2.OrganID = ddlOrganID.SelectedValue
        OV2.RankIDMIN = ddlRankIDMIN.SelectedValue
        OV2.RankIDMAX = ddlRankIDMAX.SelectedValue

        '20170304kevin
        OV2.TitleIDMIN = ddlTitleIDMIN.SelectedValue
        OV2.TitleIDMAX = ddlTitleIDMAX.SelectedValue

        'Dim ddlTitleNameItem = ddlTitleName.SelectedIndex
        'If ddlTitleName.SelectedValue.Equals("") Then
        '    OV2.TitleID = ""
        '    OV2.TitleName = ""
        'Else
        '    OV2.TitleID = ddlTitleName.SelectedItem.Text.Split("-")(0).ToString
        '    OV2.TitleName = ddlTitleName.SelectedItem.Text.Split("-")(1).ToString
        'End If

        OV2.PositionID = ddlPositionID.SelectedValue
        OV2.OTStatus = ddlOTStatus.SelectedValue
        OV2.OTStatus1 = ddlOTStatus1.SelectedValue
        OV2.WorkStatus = ddlWorkStatus.SelectedValue
        OV2.OTEmpID = tbOTEmpID.Text
        OV2.OTEmpName = tbOTEmpName.Text
        OV2.OTFormNO = tbOTFormNO.Text
        OV2.OTPayDate = tbOTPayDate.Text
        OV2.OvertimeDateB = txtOvertimeDateB.DateText
        OV2.OvertimeDateE = txtOvertimeDateE.DateText
        OV2.OTSalaryOrAdjust = ddlSalaryOrAdjust.SelectedValue
        'If ckOTSalaryPaid.Checked Then
        '    OV2.OTSalaryPaid = "0"
        'Else
        '    OV2.OTSalaryPaid = "1"
        'End If

        Return OV2
    End Function

    ''' <summary>
    ''' 載入顏色
    ''' </summary>
    ''' <param name="objDDL"></param>
    ''' <remarks></remarks>
    Private Sub subReLoadColor(ByVal objDDL As DropDownList)
        If objDDL.Items.Count > 0 Then
            Dim ArrColors As New List(Of ArrayList)()

            Select Case objDDL.ID
                Case "ddlOrgType"
                    ArrColors = ViewState.Item("OrgTypeColors")
                Case "ddlDeptID"
                    ArrColors = ViewState.Item("DeptColors")
            End Select

            For Each item As ArrayList In ArrColors
                Dim list As ListItem = objDDL.Items.FindByValue(item(0))
                If Not list Is Nothing Then
                    list.Attributes.Add("style", "background-color:" + item(1))
                End If
            Next
        End If
    End Sub

    ''' <summary>
    ''' 載入單位類別
    ''' </summary>
    ''' <param name="objDDL"></param>
    ''' <param name="strCompID"></param>
    ''' <param name="OrgType"></param>
    ''' <remarks></remarks>
    Private Sub subLoadOrganColor(ByVal objDDL As DropDownList, ByVal strCompID As String, ByVal OrgType As String)
        Dim strSQL As New StringBuilder
        strSQL.AppendLine("Select O.OrganID")
        'strSQL.AppendLine(", Case When OrgType = DeptID Then OrganID + '-' + OrganName + '-' + RoleCode Else '　　' + OrganID + '-' + OrganName + '-' + RoleCode End OrganName")
        strSQL.AppendLine(", Case When OrgType = DeptID Then OrganID + '-' + OrganName Else '　　' + OrganID + '-' + OrganName End OrganName")
        strSQL.AppendLine(", IsNull(W.Color, '#FFFFFF') Color")
        strSQL.AppendLine("From Organization O")
        strSQL.AppendLine("Left Join OrganColor_Web W On O.CompID = W.CompID and O.SortOrder = W.SortOrder")
        If objDDL.ID = "ddlOrgType" Then
            strSQL.AppendLine("Where OrganID = OrganID AND OrganID = DeptID AND OrganID = OrgType")
        Else
            strSQL.AppendLine("Where ((OrganID = OrganID AND OrganID = DeptID AND OrganID <> OrgType)OR(OrganID = OrganID AND OrganID = DeptID AND OrganID = OrgType))")
        End If
        strSQL.AppendLine("And O.CompID = " & Bsp.Utility.Quote(strCompID))
        strSQL.AppendLine("And VirtualFlag = '0'")
        strSQL.AppendLine("And InValidFlag = '0'")
        If objDDL.ID = "ddlOrgType" Then

        Else
            strSQL.AppendLine(" and O.OrgType='" + OrgType + "'")
        End If
        strSQL.AppendLine("Order By O.InValidFlag, O.SortOrder, Right(O.GroupType, 1), O.GroupID, O.OrganID")


        Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
            objDDL.Items.Clear()
            Dim ArrColors As New List(Of ArrayList)()

            If dt.Rows.Count > 0 Then
                For Each item As DataRow In dt.Rows
                    Dim ListOpt As ListItem = New ListItem()
                    ListOpt.Value = item("OrganID").ToString()
                    ListOpt.Text = item("OrganName").ToString()

                    If item("Color").ToString().Trim() <> "#FFFFFF" Then
                        ListOpt.Attributes.Add("style", "background-color:" + item("Color").ToString().Trim())

                        Dim ArrColor As New ArrayList()
                        ArrColor.Add(item("OrganID").ToString())
                        ArrColor.Add(item("Color").ToString().Trim())
                        ArrColors.Add(ArrColor)
                    End If

                    objDDL.Items.Add(ListOpt)
                Next
            End If

            objDDL.Items.Insert(0, New ListItem("---請選擇---", ""))

            Select Case objDDL.ID
                Case "ddlOrgType"
                    ViewState.Item("OrgTypeColors") = ArrColors
                Case "ddlDeptID"
                    ViewState.Item("DeptColors") = ArrColors
            End Select
        End Using
    End Sub

    ''' <summary>
    ''' 載入部門
    ''' </summary>
    ''' <param name="objDDL"></param>
    ''' <param name="strCompID"></param>
    ''' <remarks></remarks>
    Private Sub subLoadOrganColor(ByVal objDDL As DropDownList, ByVal strCompID As String)
        Dim strSQL As New StringBuilder
        strSQL.AppendLine("Select O.OrganID")
        'strSQL.AppendLine(", Case When OrgType = DeptID Then OrganID + '-' + OrganName + '-' + RoleCode Else '　　' + OrganID + '-' + OrganName + '-' + RoleCode End OrganName")
        strSQL.AppendLine(", Case When OrgType = DeptID Then OrganID + '-' + OrganName Else '　　' + OrganID + '-' + OrganName End OrganName")
        strSQL.AppendLine(", IsNull(W.Color, '#FFFFFF') Color")
        strSQL.AppendLine("From Organization O")
        strSQL.AppendLine("Left Join OrganColor_Web W On O.CompID = W.CompID and O.SortOrder = W.SortOrder")
        If objDDL.ID = "ddlOrgType" Then
            strSQL.AppendLine("Where OrganID = OrganID AND OrganID = DeptID AND OrganID = OrgType")
        Else
            strSQL.AppendLine("Where ((OrganID = OrganID AND OrganID = DeptID AND OrganID <> OrgType)OR(OrganID = OrganID AND OrganID = DeptID AND OrganID = OrgType))")
        End If
        strSQL.AppendLine("And O.CompID = " & Bsp.Utility.Quote(strCompID))
        strSQL.AppendLine("And VirtualFlag = '0'")
        strSQL.AppendLine("And InValidFlag = '0'")
        strSQL.AppendLine("Order By O.InValidFlag, O.SortOrder, Right(O.GroupType, 1), O.GroupID, O.OrganID")

        Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
            objDDL.Items.Clear()
            Dim ArrColors As New List(Of ArrayList)()

            If dt.Rows.Count > 0 Then
                For Each item As DataRow In dt.Rows
                    Dim ListOpt As ListItem = New ListItem()
                    ListOpt.Value = item("OrganID").ToString()
                    ListOpt.Text = item("OrganName").ToString()

                    If item("Color").ToString().Trim() <> "#FFFFFF" Then
                        ListOpt.Attributes.Add("style", "background-color:" + item("Color").ToString().Trim())

                        Dim ArrColor As New ArrayList()
                        ArrColor.Add(item("OrganID").ToString())
                        ArrColor.Add(item("Color").ToString().Trim())
                        ArrColors.Add(ArrColor)
                    End If

                    objDDL.Items.Add(ListOpt)
                Next
            End If

            objDDL.Items.Insert(0, New ListItem("---請選擇---", ""))

            Select Case objDDL.ID
                Case "ddlOrgType"
                    ViewState.Item("OrgTypeColors") = ArrColors
                Case "ddlDeptID"
                    ViewState.Item("DeptColors") = ArrColors
            End Select
        End Using
    End Sub

    ''' <summary>
    ''' 移除特殊字元
    ''' </summary>
    ''' <param name="dateStr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DateStrF(ByVal dateStr As String) As String
        Dim result = ""
        If Not dateStr Is Nothing Then
            If dateStr.Replace("/", "").Replace("_", "").Trim = "" Then
                result = ""
            Else
                result = dateStr.ToString
            End If
        End If
        Return result
    End Function
#End Region

#Region "更改資料"

    Protected Sub ddlType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlType.SelectedIndexChanged
        If Not pcMain.DataTable Is Nothing Then
            pcMain.DataTable.Clear()
            pcMain.BindGridView()
        End If
        If Not pcMain1.DataTable Is Nothing Then
            pcMain1.DataTable.Clear()
            pcMain1.BindGridView()
        End If
        'tbOTPayDateErrorMsg.Visible = False

        If ddlType.SelectedValue.Equals("detail") Then
            ddlOTStatus.SelectedIndex = 0
            ddlOTStatus1.SelectedIndex = 0
            tbOTPayDate.Text = ""
            ddlOTStatus.Enabled = True
            ddlOTStatus1.Enabled = True
            tbOTPayDate.Enabled = True
        Else
            ddlOTStatus.SelectedIndex = 0
            ddlOTStatus1.SelectedIndex = 0
            tbOTPayDate.Text = ""
            ddlOTStatus.Enabled = False
            ddlOTStatus1.Enabled = False
            tbOTPayDate.Enabled = False
        End If
    End Sub

    Protected Sub ddlRankIDMIN_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlRankIDMIN.SelectedIndexChanged


        'If ddlRankIDMAX.SelectedValue <> "" And ddlRankIDMIN.SelectedValue <> "" Then
        '    If ddlRankIDMAX.SelectedValue < ddlRankIDMIN.SelectedValue Then
        '        lblRankNotice.Visible = True
        '    Else
        '        lblRankNotice.Visible = False
        '    End If
        'Else
        '    lblRankNotice.Visible = False
        'End If

        'If (Not ddlRankIDMIN.SelectedValue.Equals("")) And (Not ddlRankIDMAX.SelectedValue.Equals("")) Then
        '    Bsp.Utility.FillDDL(ddlTitleName, "eHRMSDB", "Title", "TitleID+'-'+TitleName", "TitleID+'-'+TitleName", Bsp.Utility.DisplayType.OnlyID, , "And CompID =" + Bsp.Utility.Quote(UserProfile.SelectCompRoleID) + " and RankID >= '" + ddlRankIDMIN.SelectedValue + "' and  RankID <='" + ddlRankIDMAX.SelectedValue + "'", "group by TitleID,TitleName") '職稱
        '    ddlTitleName.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        'ElseIf (Not ddlRankIDMIN.SelectedValue.Equals("")) And (ddlRankIDMAX.SelectedValue.Equals("")) Then
        '    Bsp.Utility.FillDDL(ddlTitleName, "eHRMSDB", "Title", "TitleID+'-'+TitleName", "TitleID+'-'+TitleName", Bsp.Utility.DisplayType.OnlyID, , "And CompID = " + Bsp.Utility.Quote(UserProfile.SelectCompRoleID) + " and RankID >= '" + ddlRankIDMIN.SelectedValue + "'", "group by TitleID,TitleName") '職稱
        '    ddlTitleName.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        'ElseIf (ddlRankIDMIN.SelectedValue.Equals("")) And (Not ddlRankIDMAX.SelectedValue.Equals("")) Then
        '    Bsp.Utility.FillDDL(ddlTitleName, "eHRMSDB", "Title", "TitleID+'-'+TitleName", "TitleID+'-'+TitleName", Bsp.Utility.DisplayType.OnlyID, , "And CompID = " + Bsp.Utility.Quote(UserProfile.SelectCompRoleID) + " and  RankID <='" + ddlRankIDMAX.SelectedValue + "'", "group by TitleID,TitleName") '職稱
        '    ddlTitleName.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        'Else
        '    Bsp.Utility.FillDDL(ddlTitleName, "eHRMSDB", "Title", "TitleID+'-'+TitleName", "TitleID+'-'+TitleName", Bsp.Utility.DisplayType.OnlyID, , "And CompID =" + Bsp.Utility.Quote(UserProfile.SelectCompRoleID), "group by TitleID,TitleName") '職稱
        '    ddlTitleName.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        'End If
        '20170304kevin
        Dim OV_1Obj As OV_1 = New OV_1
        Dim CompID_RankID As String = UserProfile.SelectCompRoleID
        Dim sRankID_S = OV_1Obj.StringIIF(ddlRankIDMIN.SelectedValue) '職等(起)
        Dim bRankID_S = Not String.IsNullOrEmpty(sRankID_S)
        If bRankID_S Then
            sRankID_S = OVBusinessCommon.GetRankID(CompID_RankID, sRankID_S)
        End If
        Dim sRankID_E = OV_1Obj.StringIIF(ddlRankIDMAX.SelectedValue) '職等(迄)
        Dim bRankID_E = Not String.IsNullOrEmpty(sRankID_E)
        If bRankID_E Then
            sRankID_E = OVBusinessCommon.GetRankID(CompID_RankID, sRankID_E)
        End If
        'If sRankID_S > sRankID_E Then
        '    Throw New Exception("職等(迄)不可小於職等(起) !!")
        'End If

        If bRankID_S And bRankID_E Then
            If sRankID_S <= sRankID_E Then
                lblRankNotice.Visible = False
            Else
                lblRankNotice.Visible = True
            End If
        Else
            lblRankNotice.Visible = False
        End If

        '20170219 Beatrice mod
        If bRankID_S Then
            Bsp.Utility.FillDDL(ddlTitleIDMIN, "eHRMSDB", "Title", "distinct TitleID", "TitleName", Bsp.Utility.DisplayType.Full, "", "and CompID = " + Bsp.Utility.Quote(UserProfile.SelectCompRoleID) + " and RankID = " + Bsp.Utility.Quote(ddlRankIDMIN.SelectedValue))
            ddlTitleIDMIN.Items.Insert(0, New ListItem("---請選擇---", ""))
        Else
            ddlTitleIDMIN.Items.Clear()
            ddlTitleIDMIN.Items.Insert(0, New ListItem("---請先選擇職等---", ""))
        End If








    End Sub

    Protected Sub ddlRankIDMAX_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlRankIDMAX.SelectedIndexChanged
        'If ddlRankIDMAX.SelectedValue <> "" And ddlRankIDMIN.SelectedValue <> "" Then
        '    If ddlRankIDMAX.SelectedValue < ddlRankIDMIN.SelectedValue Then
        '        lblRankNotice.Visible = True
        '    Else
        '        lblRankNotice.Visible = False
        '    End If
        'Else
        '    lblRankNotice.Visible = False
        'End If
        'If (Not ddlRankIDMIN.SelectedValue.Equals("")) And (Not ddlRankIDMAX.SelectedValue.Equals("")) Then
        '    Bsp.Utility.FillDDL(ddlTitleName, "eHRMSDB", "Title", "TitleID+'-'+TitleName", "TitleID+'-'+TitleName", Bsp.Utility.DisplayType.OnlyID, , "And CompID = " + Bsp.Utility.Quote(UserProfile.SelectCompRoleID) + " and RankID >= '" + ddlRankIDMIN.SelectedValue + "' and  RankID <='" + ddlRankIDMAX.SelectedValue + "'", "group by TitleID,TitleName") '職稱
        '    ddlTitleName.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        'ElseIf (Not ddlRankIDMIN.SelectedValue.Equals("")) And (ddlRankIDMAX.SelectedValue.Equals("")) Then
        '    Bsp.Utility.FillDDL(ddlTitleName, "eHRMSDB", "Title", "TitleID+'-'+TitleName", "TitleID+'-'+TitleName", Bsp.Utility.DisplayType.OnlyID, , "And CompID = " + Bsp.Utility.Quote(UserProfile.SelectCompRoleID) + " and RankID >= '" + ddlRankIDMIN.SelectedValue + "'", "group by TitleID,TitleName") '職稱
        '    ddlTitleName.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        'ElseIf (ddlRankIDMIN.SelectedValue.Equals("")) And (Not ddlRankIDMAX.SelectedValue.Equals("")) Then
        '    Bsp.Utility.FillDDL(ddlTitleName, "eHRMSDB", "Title", "TitleID+'-'+TitleName", "TitleID+'-'+TitleName", Bsp.Utility.DisplayType.OnlyID, , "And CompID =" + Bsp.Utility.Quote(UserProfile.SelectCompRoleID) + " and  RankID <='" + ddlRankIDMAX.SelectedValue + "'", "group by TitleID,TitleName") '職稱
        '    ddlTitleName.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        'Else
        '    Bsp.Utility.FillDDL(ddlTitleName, "eHRMSDB", "Title", "TitleID+'-'+TitleName", "TitleID+'-'+TitleName", Bsp.Utility.DisplayType.OnlyID, , "And CompID =" + Bsp.Utility.Quote(UserProfile.SelectCompRoleID), "group by TitleID,TitleName") '職稱
        '    ddlTitleName.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        'End If



        '20170304kevin
        Dim OV_1Obj As OV_1 = New OV_1
        Dim CompID_RankID As String = UserProfile.SelectCompRoleID
        Dim sRankID_S = OV_1Obj.StringIIF(ddlRankIDMIN.SelectedValue) '職等(起)
        Dim bRankID_S = Not String.IsNullOrEmpty(sRankID_S)
        If bRankID_S Then
            sRankID_S = OVBusinessCommon.GetRankID(CompID_RankID, sRankID_S)
        End If
        Dim sRankID_E = OV_1Obj.StringIIF(ddlRankIDMAX.SelectedValue) '職等(迄)
        Dim bRankID_E = Not String.IsNullOrEmpty(sRankID_E)
        If bRankID_E Then
            sRankID_E = OVBusinessCommon.GetRankID(CompID_RankID, sRankID_E)
        End If


        If bRankID_S And bRankID_E Then
            If sRankID_S <= sRankID_E Then
                lblRankNotice.Visible = False
            Else
                lblRankNotice.Visible = True
            End If
        Else
            lblRankNotice.Visible = False
        End If


        '20170219 Beatrice mod
        If bRankID_E Then
            Bsp.Utility.FillDDL(ddlTitleIDMAX, "eHRMSDB", "Title", "distinct TitleID", "TitleName", Bsp.Utility.DisplayType.Full, "", "and CompID = " + Bsp.Utility.Quote(UserProfile.SelectCompRoleID) + " and RankID = " + Bsp.Utility.Quote(ddlRankIDMAX.SelectedValue))
            ddlTitleIDMAX.Items.Insert(0, New ListItem("---請選擇---", ""))
        Else
            ddlTitleIDMAX.Items.Clear()
            ddlTitleIDMAX.Items.Insert(0, New ListItem("---請先選擇職等---", ""))
        End If
    End Sub
    '部門
    Protected Sub ddlDept_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDeptID.SelectedIndexChanged
        If ddlDeptID.SelectedValue = "" Then
            Bsp.Utility.FillDDL(ddlOrganID, "eHRMSDB", "Organization", "RTRIM(OrganID)", "OrganName", Bsp.Utility.DisplayType.Full, "", "And InValidFlag = '0' And VirtualFlag = '0' And CompID = " & Bsp.Utility.Quote(UserProfile.SelectCompRoleID) & " AND ((OrganID = OrganID AND OrganID <> DeptID AND OrganID <> OrgType)OR(OrganID = OrganID AND OrganID = DeptID AND OrganID = OrgType)) ", "Order By OrganID")
            ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
        Else
            Bsp.Utility.FillDDL(ddlOrganID, "eHRMSDB", "Organization", "RTRIM(OrganID)", "OrganName", Bsp.Utility.DisplayType.Full, "", "And InValidFlag = '0' And VirtualFlag = '0' And CompID = " & Bsp.Utility.Quote(UserProfile.SelectCompRoleID) & " And DeptID = " & Bsp.Utility.Quote(ddlDeptID.SelectedValue) & " AND ((OrganID = OrganID AND OrganID <> DeptID AND OrganID <> OrgType)OR(OrganID = OrganID AND OrganID = DeptID AND OrganID = OrgType)) ", "Order By OrganID")
            ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
        End If

        Dim str As String = "join " + eHRMSDB_ITRD + ".[dbo].EmpPosition EP ON PO.CompID=EP.CompID AND PO.PositionID=EP.PositionID AND EP.PrincipalFlag='1' join " + eHRMSDB_ITRD + ".[dbo].Personal P ON EP.CompID=P.CompID AND EP.EmpID=P.EmpID  join " + eHRMSDB_ITRD + ".[dbo].Organization ORT ON  P.OrganID=ORT.OrganID AND P.CompID =ORT.CompID"

        If (Not ddlOrganID.SelectedValue.ToString.Trim.Equals("")) Then
            Bsp.Utility.FillDDL(ddlPositionID, "eHRMSDB", "Position PO", "distinct PO.PositionID", "PO.Remark", Bsp.Utility.DisplayType.Full, str, "And PO.CompID = " + Bsp.Utility.Quote(UserProfile.SelectCompRoleID) + " and ORT.OrganID= '" + ddlOrganID.SelectedValue + "'", "Order By  PO.PositionID") '職位
            ddlPositionID.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        ElseIf (Not ddlDeptID.SelectedValue.ToString.Trim.Equals("")) Then
            Bsp.Utility.FillDDL(ddlPositionID, "eHRMSDB", "Position PO", "distinct PO.PositionID", "PO.Remark", Bsp.Utility.DisplayType.Full, str, "And PO.CompID = " + Bsp.Utility.Quote(UserProfile.SelectCompRoleID) + " and P.DeptID= '" + ddlDeptID.SelectedValue + "'", "Order By  PO.PositionID") '職位
            ddlPositionID.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        ElseIf (Not ddlOrgType.SelectedValue.ToString.Trim.Equals("")) Then
            Bsp.Utility.FillDDL(ddlPositionID, "eHRMSDB", "Position PO", "distinct PO.PositionID", "PO.Remark", Bsp.Utility.DisplayType.Full, str, "And PO.CompID =" + Bsp.Utility.Quote(UserProfile.SelectCompRoleID) + " and ORT.OrgType= '" + ddlOrgType.SelectedValue + "'", "Order By  PO.PositionID") '職位
            ddlPositionID.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        Else
            Bsp.Utility.FillDDL(ddlPositionID, "eHRMSDB", "Position PO", "distinct PO.PositionID", "PO.Remark", Bsp.Utility.DisplayType.Full, str, "And PO.CompID = " + Bsp.Utility.Quote(UserProfile.SelectCompRoleID), "Order By  PO.PositionID") '職位
            ddlPositionID.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        End If
    End Sub

    Protected Sub ddlOrgType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlOrgType.SelectedIndexChanged
        If Not ddlOrgType.SelectedValue = "" Then
            subLoadOrganColor(ddlDeptID, UserProfile.SelectCompRoleID, ddlOrgType.SelectedValue)
            Bsp.Utility.FillDDL(ddlOrganID, "eHRMSDB", "Organization", "RTRIM(OrganID)", "OrganName", Bsp.Utility.DisplayType.Full, "", "And InValidFlag = '0' And VirtualFlag = '0' And CompID = " & Bsp.Utility.Quote(UserProfile.SelectCompRoleID) & " And OrgType = " & Bsp.Utility.Quote(ddlOrgType.SelectedValue) & " AND ((OrganID = OrganID AND OrganID <> DeptID AND OrganID <> OrgType)OR(OrganID = OrganID AND OrganID = DeptID AND OrganID = OrgType)) ", "Order By OrganID")
            ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
        Else
            subLoadOrganColor(ddlDeptID, UserProfile.SelectCompRoleID)
            If ddlDeptID.SelectedValue = "" Then
                Bsp.Utility.FillDDL(ddlOrganID, "eHRMSDB", "Organization", "RTRIM(OrganID)", "OrganName", Bsp.Utility.DisplayType.Full, "", "And InValidFlag = '0' And VirtualFlag = '0' And CompID = " & Bsp.Utility.Quote(UserProfile.SelectCompRoleID) & " AND ((OrganID = OrganID AND OrganID <> DeptID AND OrganID <> OrgType)OR(OrganID = OrganID AND OrganID = DeptID AND OrganID = OrgType)) ", "Order By OrganID")
                ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
            Else
                Bsp.Utility.FillDDL(ddlOrganID, "eHRMSDB", "Organization", "RTRIM(OrganID)", "OrganName", Bsp.Utility.DisplayType.Full, "", "And InValidFlag = '0' And VirtualFlag = '0' And CompID = " & Bsp.Utility.Quote(UserProfile.SelectCompRoleID) & " And DeptID = " & Bsp.Utility.Quote(ddlDeptID.SelectedValue) & " AND ((OrganID = OrganID AND OrganID <> DeptID AND OrganID <> OrgType)OR(OrganID = OrganID AND OrganID = DeptID AND OrganID = OrgType)) ", "Order By OrganID")
                ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
            End If
        End If

        Dim str As String = "join " + eHRMSDB_ITRD + ".[dbo].EmpPosition EP ON PO.CompID=EP.CompID AND PO.PositionID=EP.PositionID AND EP.PrincipalFlag='1' join " + eHRMSDB_ITRD + ".[dbo].Personal P ON EP.CompID=P.CompID AND EP.EmpID=P.EmpID  join " + eHRMSDB_ITRD + ".[dbo].Organization ORT ON  P.OrganID=ORT.OrganID AND P.CompID =ORT.CompID"

        If (Not ddlOrganID.SelectedValue.ToString.Trim.Equals("")) Then
            Bsp.Utility.FillDDL(ddlPositionID, "eHRMSDB", "Position PO", "distinct PO.PositionID", "PO.Remark", Bsp.Utility.DisplayType.Full, str, "And PO.CompID = " + Bsp.Utility.Quote(UserProfile.SelectCompRoleID) + " and ORT.OrganID= '" + ddlOrganID.SelectedValue + "'", "Order By  PO.PositionID") '職位
            ddlPositionID.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        ElseIf (Not ddlDeptID.SelectedValue.ToString.Trim.Equals("")) Then
            Bsp.Utility.FillDDL(ddlPositionID, "eHRMSDB", "Position PO", "distinct PO.PositionID", "PO.Remark", Bsp.Utility.DisplayType.Full, str, "And PO.CompID =" + Bsp.Utility.Quote(UserProfile.SelectCompRoleID) + " and P.DeptID= '" + ddlDeptID.SelectedValue + "'", "Order By  PO.PositionID") '職位
            ddlPositionID.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        ElseIf (Not ddlOrgType.SelectedValue.ToString.Trim.Equals("")) Then
            Bsp.Utility.FillDDL(ddlPositionID, "eHRMSDB", "Position PO", "distinct PO.PositionID", "PO.Remark", Bsp.Utility.DisplayType.Full, str, "And PO.CompID =" + Bsp.Utility.Quote(UserProfile.SelectCompRoleID) + " and ORT.OrgType= '" + ddlOrgType.SelectedValue + "'", "Order By  PO.PositionID") '職位
            ddlPositionID.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        Else
            Bsp.Utility.FillDDL(ddlPositionID, "eHRMSDB", "Position PO", "distinct PO.PositionID", "PO.Remark", Bsp.Utility.DisplayType.Full, str, "And PO.CompID = " + Bsp.Utility.Quote(UserProfile.SelectCompRoleID), "Order By  PO.PositionID") '職位
            ddlPositionID.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        End If



    End Sub

    Protected Sub ddlOrganID_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlOrganID.SelectedIndexChanged
        Dim str As String = "join " + eHRMSDB_ITRD + ".[dbo].EmpPosition EP ON PO.CompID=EP.CompID AND PO.PositionID=EP.PositionID AND EP.PrincipalFlag='1' join " + eHRMSDB_ITRD + ".[dbo].Personal P ON EP.CompID=P.CompID AND EP.EmpID=P.EmpID  join " + eHRMSDB_ITRD + ".[dbo].Organization ORT ON  P.OrganID=ORT.OrganID AND P.CompID =ORT.CompID"

        If (Not ddlOrganID.SelectedValue.ToString.Trim.Equals("")) Then
            Bsp.Utility.FillDDL(ddlPositionID, "eHRMSDB", "Position PO", "distinct PO.PositionID", "PO.Remark", Bsp.Utility.DisplayType.Full, str, "And PO.CompID =" + Bsp.Utility.Quote(UserProfile.SelectCompRoleID) + "and ORT.OrganID= '" + ddlOrganID.SelectedValue + "'", "Order By  PO.PositionID") '職位
            ddlPositionID.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        ElseIf (Not ddlDeptID.SelectedValue.ToString.Trim.Equals("")) Then
            Bsp.Utility.FillDDL(ddlPositionID, "eHRMSDB", "Position PO", "distinct PO.PositionID", "PO.Remark", Bsp.Utility.DisplayType.Full, str, "And PO.CompID = " + Bsp.Utility.Quote(UserProfile.SelectCompRoleID) + " and P.DeptID= '" + ddlDeptID.SelectedValue + "'", "Order By  PO.PositionID") '職位
            ddlPositionID.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        ElseIf (Not ddlOrgType.SelectedValue.ToString.Trim.Equals("")) Then
            Bsp.Utility.FillDDL(ddlPositionID, "eHRMSDB", "Position PO", "distinct PO.PositionID", "PO.Remark", Bsp.Utility.DisplayType.Full, str, "And PO.CompID = " + Bsp.Utility.Quote(UserProfile.SelectCompRoleID) + " and ORT.OrgType= '" + ddlOrgType.SelectedValue + "'", "Order By  PO.PositionID") '職位
            ddlPositionID.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        Else
            Bsp.Utility.FillDDL(ddlPositionID, "eHRMSDB", "Position PO", "distinct PO.PositionID", "PO.Remark", Bsp.Utility.DisplayType.Full, str, "And PO.CompID = " + Bsp.Utility.Quote(UserProfile.SelectCompRoleID), "Order By  PO.PositionID") '職位
            ddlPositionID.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        End If
    End Sub

    Public Overrides Sub DoModalReturn(ByVal returnValue As String)
        If returnValue <> "" Then
            Dim aryData() As String = returnValue.Split(":")
            Select Case aryData(0)
                Case "ucQueryEmpID" '員編uc
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    tbOTEmpID.Text = aryValue(1)
                    'lblEmpID.Text = aryValue(2)
                    ddlCompID.SelectedValue = aryValue(3)
            End Select
        End If
    End Sub

#End Region

#Region "GridView 事件"

    Protected Sub grvMergeHeader_RowCreated(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.Header Then
            Dim HeaderGrid As GridView = DirectCast(sender, GridView)
            Dim HeaderGridRow As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)
            Dim HeaderCell As New TableCell()
            HeaderCell.Text = ""
            HeaderCell.ColumnSpan = 3
            HeaderCell.CssClass = "td_header"
            HeaderGridRow.Cells.Add(HeaderCell)
            HeaderCell = New TableCell()
            HeaderCell.Text = "加班單預先申請"
            HeaderCell.ColumnSpan = 6
            HeaderCell.CssClass = "td_header"
            HeaderGridRow.Cells.Add(HeaderCell)
            HeaderCell = New TableCell()
            HeaderCell.Text = "加班單事後申報"
            HeaderCell.ColumnSpan = 6
            HeaderCell.CssClass = "td_header"
            HeaderGridRow.Cells.Add(HeaderCell)
            gvMain.Controls(0).Controls.AddAt(0, HeaderGridRow)
        End If
    End Sub

    Protected Sub grvMergeHeader_RowCreated1(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.Header Then
            Dim HeaderGrid As GridView = DirectCast(sender, GridView)
            Dim HeaderGridRow As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)
            Dim HeaderCell As New TableCell()
            HeaderCell.Text = ""
            HeaderCell.ColumnSpan = 3
            HeaderCell.CssClass = "td_header"
            HeaderGridRow.Cells.Add(HeaderCell)
            HeaderCell = New TableCell()
            HeaderCell.Text = "加班單預先申請"
            HeaderCell.ColumnSpan = 3
            HeaderCell.CssClass = "td_header"
            HeaderGridRow.Cells.Add(HeaderCell)
            HeaderCell = New TableCell()
            HeaderCell.Text = "加班單事後申報"
            HeaderCell.ColumnSpan = 3
            HeaderCell.CssClass = "td_header"
            HeaderGridRow.Cells.Add(HeaderCell)
            gvMain1.Controls(0).Controls.AddAt(0, HeaderGridRow)
        End If
    End Sub

    Protected Sub gvMain_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvMain.RowDataBound
        Dim oRow As Data.DataRowView
        Dim OVADetail As LinkButton
        Dim OVDDetail As LinkButton

        OVADetail = e.Row.FindControl("OVADetail")
        OVDDetail = e.Row.FindControl("OVDDetail")

        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                e.Row.Cells(7).ToolTip = e.Row.Cells(7).Text
                If e.Row.Cells(7).Text.Length >= 7 Then
                    e.Row.Cells(7).Text = e.Row.Cells(7).Text.Substring(0, 7)
                End If

                e.Row.Cells(13).ToolTip = e.Row.Cells(13).Text
                If e.Row.Cells(13).Text.Length >= 7 Then
                    e.Row.Cells(13).Text = e.Row.Cells(13).Text.Substring(0, 7)
                End If
        End Select

        If e.Row.RowType = DataControlRowType.DataRow Then
            oRow = CType(e.Row.DataItem, Data.DataRowView)

            If Not oRow("OVADate").ToString().Trim().Equals("") Then
                'OVADetail.Text = ""
                OVADetail.Visible = True
            End If

            If Not oRow("OVDDate").ToString().Trim().Equals("") Then
                'OVDDetail.Text = ""
                OVDDetail.Visible = True
            End If
        End If

    End Sub

#End Region



End Class

