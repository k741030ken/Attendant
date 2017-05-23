Imports System.Data
Imports System.Data.Common
Partial Class ATWF_ATWF00
    Inherits PageBase
    Private Property eHRMSDB As String
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
    Private _BaseSQL As String = "SELECT FE.CompID,FE.SystemID,FE.FlowCode, FE.FlowName, FE.FlowSN, FE.FlowSeq, FE.FlowSeqName, FE.FlowStartFlag, FE.FlowEndFlag, FE.InValidFlag, FE.VisableFlag, FE.Status, " &
                                 "CASE FE.FlowStartFlag WHEN '1' THEN 'Y' WHEN '0' THEN '' END AS FlowStartFlagShow," &
                                 "CASE FE.FlowEndFlag WHEN '1' THEN 'Y' WHEN '0' THEN '' END AS FlowEndFlagShow," &
                                 "CASE FE.InValidFlag WHEN '1' THEN 'Y' WHEN '0' THEN '' END AS InValidFlagShow," &
                                 "CASE FE.VisableFlag WHEN '1' THEN 'Y' WHEN '0' THEN '' END AS VisableFlagShow," &
                                 "CASE FE.Status WHEN '1' THEN 'Y' WHEN '0' THEN '' END AS StatusShow," &
                                 "CASE FE.FlowAct WHEN '1' THEN '正常' WHEN '2' THEN '跳過' END AS FlowAct," &
                                 "CASE FE.SignLineDefine WHEN '1' THEN '行政組織' WHEN '2' THEN '功能組織' WHEN '3' THEN '特定人員' ELSE '' END AS SignLineDefine," &
                                 "CASE FE.SingIDDefine WHEN '1' THEN '組織主管' WHEN '2' THEN  '特定人員' ELSE '' END AS SingIDDefine," &
                                 "FE.SpeComp, ISNULL(C.CompName,'') AS SpeCompName,FE.SpeEmpID, FE.SpeEmpID + + CASE SingIDDefine WHEN 1 THEN '' WHEN 2 THEN '-' END + ISNULL(P.NameN,'') AS SpeEmpName," &
                                 "ISNULL(C2.CompName,'') AS LastChgCompName,ISNULL(P2.NameN,'') AS LastChgName," &
                                 "FE.LastChgComp + '-' + ISNULL(C2.CompName,'') AS LastChgComp, FE.LastChgID + '-' + ISNULL(P2.NameN,'') AS LastChgID, LastChgDate = Case When Replace(Convert(Char(19), FE.LastChgDate, 120),'-','/') = '1900/01/01 00:00:00' Then '' ELSE Replace(Convert(Char(19), FE.LastChgDate, 120),'-','/') End" &
                                 " FROM HRFlowEngine FE " &
                                 " LEFT JOIN " + eHRMSDB + ".[dbo].[Company] C ON C.CompID=FE.SpeComp " &
                                 " LEFT JOIN " + eHRMSDB + ".[dbo].[Personal] P ON P.CompID=FE.SpeComp AND P.EmpID=FE.SpeEmpID" &
                                 " LEFT JOIN " + eHRMSDB + ".[dbo].[Company] C2 ON C2.CompID=FE.LastChgComp " &
                                 " LEFT JOIN " + eHRMSDB + ".[dbo].[Personal] P2  ON P2.CompID=FE.LastChgComp AND P2.EmpID=FE.LastChgID"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            '公司代碼
            If UserProfile.SelectCompRoleID = "ALL" Then 'SelectCompRoleID：授權公司代碼
                lblCompID.Visible = False
                Bsp.Utility.FillHRCompany(ddlCompID) 'FillHRCompany：填寫HR公司資料(公司代碼)
                Page.SetFocus(ddlCompID) '焦點停留在公司代碼下拉選項
            Else
                lblCompID.Text = UserProfile.SelectCompRoleName '如果授權公司代碼不是選擇ALL，
                ddlCompID.Visible = False '則看不到下拉選單，顯示該公司(label)
            End If
            LoadDate()
            ucQuerySpeEmpID.ShowCompRole = "True"
            ucQuerySpeEmpID.InValidFlag = "N"

        End If
    End Sub
    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args) '拆SQL參數(ht)

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
                        'txtValidDateB.DateText = ht("txtValidDateB").ToString()
                        'txtValidDateE.DateText = ht("txtValidDateE").ToString()
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
    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnAdd"       '新增
                DoAdd()
            Case "btnUpdate"    '修改
                DoUpdate()
            Case "btnQuery"     '查詢
                DoQuery()
            Case "btnDelete"    '刪除
                DoDelete()
            Case "btnActionX"   '清除
                DoClear()
        End Select
    End Sub
    Private Sub DoDelete()
        Dim strSQL As New StringBuilder()
        Dim chkCount As Integer = 0
        If ViewState.Item("DoQuery") <> "Y" Then
            Bsp.Utility.ShowMessage(Me, "請先查詢資料")
            Return
        End If
        If selectedRows(gvMain) = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00000")
            Return
        End If
        Dim deleteCheck As String = OV9Business.OV8And9DeleteCheck(gvMain)
        If deleteCheck <> "" Then
            Bsp.Utility.ShowMessage(Me, deleteCheck)
            Return
        End If
        For intRow As Integer = 0 To gvMain.Rows.Count - 1
            If gvMain.Rows.Count >= 1 Then
                Dim objChk As CheckBox = gvMain.Rows(intRow).FindControl("chk_gvMain")
                If objChk.Checked Then
                    chkCount = chkCount + 1
                    strSQL.AppendLine("Delete FROM HRFlowEngine Where 0=0 ")
                    strSQL.AppendLine(" and RTrim(FlowCode)=RTrim(" & Bsp.Utility.Quote(gvMain.DataKeys(intRow)("FlowCode").ToString()) & ")")
                    strSQL.AppendLine(" and RTrim(FlowSN)=RTrim(" & Bsp.Utility.Quote(gvMain.DataKeys(intRow)("FlowSN").ToString()) & ")")
                    strSQL.AppendLine(" and RTrim(FlowSeq)=RTrim(" & Bsp.Utility.Quote(gvMain.DataKeys(intRow)("FlowSeq").ToString()) & ")")
                    strSQL.AppendLine(" and RTrim(CompID)=RTrim(" & Bsp.Utility.Quote(gvMain.DataKeys(intRow)("CompID").ToString()) & ")")
                    strSQL.AppendLine(" and RTrim(SystemID)=RTrim(" & Bsp.Utility.Quote(gvMain.DataKeys(intRow)("SystemID").ToString()) & ");")
                End If
            End If
        Next

        If chkCount >= 1 Then
            Using cn As DbConnection = Bsp.DB.getConnection("AattendantDB")
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction
                Try
                    Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran, "AattendantDB")
                    tran.Commit()
                Catch ex As Exception
                    tran.Rollback()
                    Bsp.Utility.ShowMessage(Me, "刪除失敗")
                    Throw
                Finally
                    If tran IsNot Nothing Then tran.Dispose()
                End Try
            End Using
        End If
        Bsp.Utility.ShowMessage(Me, "刪除成功")
        DoQuery()
        ResetGrid()
    End Sub
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
    Private Sub DoQuery()
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(_BaseSQL & " Where FE.CompID = '" + UserProfile.SelectCompRoleID + "'")
        If (txtFlowCode.Text <> "") Then
            strSQL.AppendLine(" AND FE.FlowCode = '" + txtFlowCode.Text + "'")
        End If
        If (txtFlowName.Text <> "") Then
            strSQL.AppendLine(" AND FE.FlowName LIKE '%" + txtFlowName.Text + "%'")
        End If
        If (txtFlowSN.Text <> "") Then
            strSQL.AppendLine(" AND FE.FlowSN = '" + txtFlowSN.Text + "'")
        End If
        If (txtFlowSeq.Text <> "") Then
            strSQL.AppendLine(" And FE.FlowSeq = '" + txtFlowSeq.Text + "'")
        End If
        If (ddlFlowStartFlag.SelectedValue <> "") Then
            strSQL.AppendLine(" And FE.FlowStartFlag ='" + ddlFlowStartFlag.SelectedValue + "'")
        End If
        If (ddlFlowEndFlag.SelectedValue <> "") Then
            strSQL.AppendLine(" And FE.FlowEndFlag ='" + ddlFlowEndFlag.SelectedValue + "'")
        End If
        If (ddlInValidFlag.SelectedValue <> "") Then
            strSQL.AppendLine(" And FE.InValidFlag ='" + ddlInValidFlag.SelectedValue + "'")
        End If
        If (ddlVisableFlag.SelectedValue <> "") Then
            strSQL.AppendLine(" And FE.VisableFlag ='" + ddlVisableFlag.SelectedValue + "'")
        End If
        If (ddlStatus.SelectedValue <> "") Then
            strSQL.AppendLine(" And FE.Status ='" + ddlStatus.SelectedValue + "'")
        End If
        If (ddlFlowAct.SelectedValue <> "") Then
            strSQL.AppendLine(" And FE.FlowAct ='" + ddlFlowAct.SelectedValue + "'")
        End If
        If (ddlSignLineDefine.SelectedValue <> "") Then
            strSQL.AppendLine(" And FE.SignLineDefine ='" + ddlSignLineDefine.SelectedValue + "'")
        End If
        If (ddlSingIDDefine.SelectedValue <> "") Then
            strSQL.AppendLine(" And FE.SingIDDefine ='" + ddlSingIDDefine.SelectedValue + "'")
        End If
        If (ddlSpeComp.SelectedValue <> "") Then
            strSQL.AppendLine(" And FE.SpeComp ='" + ddlSpeComp.SelectedValue + "'")
        End If
        If (txtSpeEmpID.Text <> "") Then
            strSQL.AppendLine(" And FE.SpeEmpID ='" + txtSpeEmpID.Text + "'")
        End If
        strSQL.AppendLine(" ORDER BY FlowCode, FlowSN, FlowSeq ASC")

        Dim dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "AattendantDB").Tables(0)
        pcMain.DataTable = dt
        gvMain.DataBind()
        gvMain.Visible = True
        ViewState.Item("DoQuery") = "Y"
    End Sub
    Private Sub LoadDate()
        ddlSystemID.Items.Insert(0, New ListItem("---請選擇---", ""))
        ddlSystemID.Items.Insert(1, New ListItem("OT", "OT"))
        ddlSystemID.Items.Insert(1, New ListItem("OB", "OB"))
    End Sub

    Protected Sub DoUpdate()
        If ViewState.Item("DoQuery") <> "Y" Then
            Bsp.Utility.ShowMessage(Me, "請先查詢並選取資料")
            Return
        End If
        If selectedRows(gvMain) <> "" Then
            Dim intSelectRow As Integer
            Dim intSelectCount As Integer = 0
            For intRow As Integer = 0 To gvMain.Rows.Count - 1
                Dim objChk As CheckBox = gvMain.Rows(intRow).FindControl("chk_gvMain")
                If objChk.Checked Then
                    intSelectRow = intRow
                    intSelectCount = intSelectCount + 1
                End If
            Next
            If intSelectCount = 1 Then
                Dim btnU As New ButtonState(ButtonState.emButtonType.Update)
                Dim btnE As New ButtonState(ButtonState.emButtonType.Executes)
                Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
                Dim btnC As New ButtonState(ButtonState.emButtonType.Cancel)

                btnU.Caption = "暫存"
                btnE.Caption = "流程生效"
                btnX.Caption = "清除"
                btnC.Caption = "返回"

                Me.TransferFramePage("~/ATWF/ATWF02.aspx", New ButtonState() {btnU, btnE, btnC, btnX}, _
                    txtFlowCode.ID & "=" & txtFlowCode.Text, _
                    txtFlowName.ID & "=" & txtFlowName.Text, _
                    txtFlowSN.ID & "=" & txtFlowSN.Text, _
                    txtFlowSeq.ID & "=" & txtFlowSeq.Text, _
                    ddlFlowStartFlag.ID & "=" & ddlFlowStartFlag.SelectedValue, _
                    ddlFlowEndFlag.ID & "=" & ddlFlowEndFlag.SelectedValue, _
                    ddlInValidFlag.ID & "=" & ddlInValidFlag.SelectedValue, _
                    ddlVisableFlag.ID & "=" & ddlVisableFlag.SelectedValue, _
                    ddlFlowAct.ID & "=" & ddlFlowAct.SelectedValue, _
                    ddlSignLineDefine.ID & "=" & ddlSignLineDefine.SelectedValue, _
                    ddlSingIDDefine.ID & "=" & ddlSingIDDefine.SelectedValue, _
                    ddlSpeComp.ID & "=" & ddlSpeComp.SelectedValue, _
                    txtSpeEmpID.Text & "=" & txtSpeEmpID.Text, _
                    "PageNo=" & pcMain.PageNo.ToString(), _
                    "SelectedCompID=" & UserProfile.SelectCompRoleID, _
                    "SelectedSystemID=" & gvMain.DataKeys(intSelectRow)("SystemID").ToString(), _
                    "SelectedFlowCode=" & gvMain.DataKeys(intSelectRow)("FlowCode").ToString(), _
                    "SelectedFlowName=" & gvMain.DataKeys(intSelectRow)("FlowName").ToString, _
                    "SelectedFlowSN=" & gvMain.DataKeys(intSelectRow)("FlowSN").ToString(), _
                    "SelectedFlowSeq=" & gvMain.DataKeys(intSelectRow)("FlowSeq").ToString(), _
                    "SelectedFlowSeqName=" & gvMain.DataKeys(intSelectRow)("FlowSeqName").ToString(), _
                    "SelectedFlowStartFlag=" & gvMain.DataKeys(intSelectRow)("FlowStartFlag").ToString(), _
                    "SelectedFlowEndFlag=" & gvMain.DataKeys(intSelectRow)("FlowEndFlag").ToString, _
                    "SelectedInValidFlag=" & gvMain.DataKeys(intSelectRow)("InValidFlag").ToString(), _
                    "SelectedVisableFlag=" & gvMain.DataKeys(intSelectRow)("VisableFlag").ToString(), _
                    "SelectedFlowAct=" & gvMain.DataKeys(intSelectRow)("FlowAct").ToString(), _
                    "SelectedSignLineDefine=" & gvMain.DataKeys(intSelectRow)("SignLineDefine").ToString(), _
                    "SelectedSingIDDefine=" & gvMain.DataKeys(intSelectRow)("SingIDDefine").ToString, _
                    "SelectedSpeComp=" & gvMain.DataKeys(intSelectRow)("SpeComp").ToString(), _
                    "SelectedSpeEmpID=" & gvMain.DataKeys(intSelectRow)("SpeEmpID").ToString(), _
                    "SelectedLastChgComp=" & gvMain.DataKeys(intSelectRow)("LastChgComp").ToString(), _
                    "SelectedLastChgID=" & gvMain.DataKeys(intSelectRow)("LastChgID").ToString(), _
                    "SelectedLastChgDate=" & gvMain.DataKeys(intSelectRow)("LastChgDate").ToString(), _
                    "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
            Else
                Bsp.Utility.ShowMessage(Me, "修改只能選擇一筆資料")
            End If
        End If
    End Sub
    Protected Sub DoAdd()
        Dim btnA As New ButtonState(ButtonState.emButtonType.Add)
        Dim btnE As New ButtonState(ButtonState.emButtonType.Executes)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
        Dim btnC As New ButtonState(ButtonState.emButtonType.Cancel)
        btnA.Caption = "暫存"
        btnE.Caption = "流程生效"
        btnX.Caption = "清除"
        btnC.Caption = "返回"
        Dim btnU As New ButtonState(ButtonState.emButtonType.Update)

        Me.TransferFramePage("~/ATWF/ATWF01.aspx", New ButtonState() {btnA, btnE, btnC, btnX}, _
            txtFlowCode.ID & "=" & txtFlowCode.Text, _
            txtFlowName.ID & "=" & txtFlowName.Text, _
            txtFlowSN.ID & "=" & txtFlowSN.Text, _
            txtFlowSeq.ID & "=" & txtFlowSeq.Text, _
            ddlFlowStartFlag.ID & "=" & ddlFlowStartFlag.SelectedValue, _
            ddlFlowEndFlag.ID & "=" & ddlFlowEndFlag.SelectedValue, _
            ddlInValidFlag.ID & "=" & ddlInValidFlag.SelectedValue, _
            ddlVisableFlag.ID & "=" & ddlVisableFlag.SelectedValue, _
            ddlFlowAct.ID & "=" & ddlFlowAct.SelectedValue, _
            ddlSignLineDefine.ID & "=" & ddlSignLineDefine.SelectedValue, _
            ddlSingIDDefine.ID & "=" & ddlSingIDDefine.SelectedValue, _
            ddlSpeComp.ID & "=" & ddlSpeComp.SelectedValue, _
            txtSpeEmpID.ID & "=" & txtSpeEmpID.Text, _
            "PageNo=" & pcMain.PageNo.ToString(), _
            "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
    End Sub

    Protected Sub DoClear()
        txtFlowCode.Text = ""
        txtFlowName.Text = ""
        txtFlowSN.Text = ""
        txtFlowSeq.Text = ""
        ddlFlowStartFlag.SelectedValue = ""
        ddlFlowEndFlag.SelectedValue = ""
        ddlInValidFlag.SelectedValue = ""
        ddlVisableFlag.SelectedValue = ""
        ddlFlowAct.SelectedValue = ""
        ddlSignLineDefine.SelectedValue = ""
        ddlSingIDDefine.SelectedValue = ""
        trSpec.Visible = False
        gvMain.Visible = False
        pcMain.PageNo = Nothing
        pcMain.PageCount = Nothing
        pcMain.RecordCount = Nothing
        pcMain.DataTable = Nothing
        ViewState.Item("DoQuery") = ""
    End Sub

    Private Sub ddlSingIDCheck()
        If ddlSingIDDefine.SelectedValue = "2" Then
            Dim objOM1 As New OM1()
            trSpec.Visible = True
            OM1.FillDDL(ddlSpeComp, " Company ", " CompID ", " CompName ", OM1.DisplayType.OnlyName, "", " and InValidFlag = '0' And NotShowFlag = '0'", "ORDER BY CompID")
            ddlSpeComp.Items.Insert(0, New ListItem("---請選擇---", ""))
        Else
            trSpec.Visible = False
            ddlSpeComp.Items.Clear()
            txtSpeEmpID.Text = ""
            lblSpeEmpID.Text = ""
        End If
    End Sub

    Protected Sub ddlSingIDDefine_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlSingIDDefine.SelectedIndexChanged
        ddlSingIDCheck()
    End Sub

    Protected Sub ddlSpeComp_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlSpeComp.SelectedIndexChanged
        ucQuerySpeEmpID.SelectCompID = ddlSpeComp.SelectedValue
        txtSpeEmpID.Text = ""
        lblSpeEmpID.Text = ""
    End Sub
    Protected Sub gvMain_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMain.RowCommand
        If ViewState.Item("DoQuery") <> "Y" Then
            Bsp.Utility.ShowMessage(Me, "請先查詢並選取資料")
            Return
        End If
        Dim btnC As New ButtonState(ButtonState.emButtonType.Cancel)
        btnC.Caption = "返回"
        If e.CommandName = "Detail" Then
            Dim a As New FlowBackInfo()
            a.MenuNodeTitle = "回清單"
            a.URL = "~/ATWF/ATWF00.aspx"
            If selectedRows(gvMain) <> "" Then
                Dim intSelectRow As Integer
                Dim intSelectCount As Integer = 0
                For intRow As Integer = 0 To gvMain.Rows.Count - 1
                    Dim objChk As CheckBox = gvMain.Rows(intRow).FindControl("chk_gvMain")
                    If objChk.Checked Then
                        intSelectRow = intRow
                        intSelectCount = intSelectCount + 1
                    End If
                Next
                If intSelectCount = 1 Then
                    TransferFramePage("~/ATWF/ATWF03.aspx", New ButtonState() {btnC}, a, _
                        txtFlowCode.ID & "=" & txtFlowCode.Text, _
                        txtFlowName.ID & "=" & txtFlowName.Text, _
                        txtFlowSN.ID & "=" & txtFlowSN.Text, _
                        txtFlowSeq.ID & "=" & txtFlowSeq.Text, _
                        ddlFlowStartFlag.ID & "=" & ddlFlowStartFlag.SelectedValue, _
                        ddlFlowEndFlag.ID & "=" & ddlFlowEndFlag.SelectedValue, _
                        ddlInValidFlag.ID & "=" & ddlInValidFlag.SelectedValue, _
                        ddlVisableFlag.ID & "=" & ddlVisableFlag.SelectedValue, _
                        ddlFlowAct.ID & "=" & ddlFlowAct.SelectedValue, _
                        ddlSignLineDefine.ID & "=" & ddlSignLineDefine.SelectedValue, _
                        ddlSingIDDefine.ID & "=" & ddlSingIDDefine.SelectedValue, _
                        ddlSpeComp.ID & "=" & ddlSpeComp.SelectedValue, _
                        txtSpeEmpID.Text & "=" & txtSpeEmpID.Text, _
                        "PageNo=" & pcMain.PageNo.ToString(), _
                        "SelectedCompID=" & UserProfile.SelectCompRoleID, _
                        "SelectedSystemID=" & gvMain.DataKeys(intSelectRow)("SystemID").ToString(), _
                        "SelectedFlowCode=" & gvMain.DataKeys(intSelectRow)("FlowCode").ToString(), _
                        "SelectedFlowName=" & gvMain.DataKeys(intSelectRow)("FlowName").ToString, _
                        "SelectedFlowSN=" & gvMain.DataKeys(intSelectRow)("FlowSN").ToString(), _
                        "SelectedFlowSeq=" & gvMain.DataKeys(intSelectRow)("FlowSeq").ToString(), _
                        "SelectedFlowSeqName=" & gvMain.DataKeys(intSelectRow)("FlowSeqName").ToString(), _
                        "SelectedFlowStartFlag=" & gvMain.DataKeys(intSelectRow)("FlowStartFlag").ToString(), _
                        "SelectedFlowEndFlag=" & gvMain.DataKeys(intSelectRow)("FlowEndFlag").ToString, _
                        "SelectedInValidFlag=" & gvMain.DataKeys(intSelectRow)("InValidFlag").ToString(), _
                        "SelectedVisableFlag=" & gvMain.DataKeys(intSelectRow)("VisableFlag").ToString(), _
                        "SelectedFlowAct=" & gvMain.DataKeys(intSelectRow)("FlowAct").ToString(), _
                        "SelectedSignLineDefine=" & gvMain.DataKeys(intSelectRow)("SignLineDefine").ToString(), _
                        "SelectedSingIDDefine=" & gvMain.DataKeys(intSelectRow)("SingIDDefine").ToString, _
                        "SelectedSpeComp=" & gvMain.DataKeys(intSelectRow)("SpeComp").ToString(), _
                        "SelectedSpeEmpID=" & gvMain.DataKeys(intSelectRow)("SpeEmpID").ToString(), _
                        "SelectedLastChgComp=" & gvMain.DataKeys(intSelectRow)("LastChgComp").ToString(), _
                        "SelectedLastChgID=" & gvMain.DataKeys(intSelectRow)("LastChgID").ToString(), _
                        "SelectedLastChgDate=" & gvMain.DataKeys(intSelectRow)("LastChgDate").ToString(), _
                        "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
                End If
            End If
        End If
    End Sub

    Protected Sub txtSpeEmpID_TextChanged(sender As Object, e As System.EventArgs) Handles txtSpeEmpID.TextChanged
        If txtSpeEmpID.Text <> "" And txtSpeEmpID.Text.Length = 6 Then
            Dim objHR As New HR
            Dim rtnTable As DataTable = objHR.GetHREmpName(ddlSpeComp.SelectedValue, txtSpeEmpID.Text)
            If rtnTable.Rows.Count <= 0 Then
                lblSpeEmpID.Text = ""
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", "人事資料尚未建檔")
            Else
                lblSpeEmpID.Text = rtnTable.Rows(0).Item(0)
            End If
        Else
            lblSpeEmpID.Text = ""
        End If
    End Sub

End Class
