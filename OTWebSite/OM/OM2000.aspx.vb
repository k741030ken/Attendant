'****************************************************
'功能說明：組織異動維護查詢功能
'建立人員：Rebecca Yan
'建立日期：2016.09.24
'****************************************************
Imports System.Data

Partial Class OM_OM2000
    Inherits PageBase
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            '公司代碼
            If UserProfile.SelectCompRoleID = "ALL" Then 'SelectCompRoleID：授權公司代碼
                lblCompRoleID.Visible = False
                Bsp.Utility.FillHRCompany(ddlCompID) 'FillHRCompany：填寫HR公司資料(公司代碼)
                Page.SetFocus(ddlCompID) '焦點停留在公司代碼下拉選項

            Else
                lblCompRoleID.Text = UserProfile.SelectCompRoleName '如果授權公司代碼不是選擇ALL，
                ddlCompID.Visible = False '則看不到下拉選單，顯示該公司(label)
            End If
            subLoadOrganIDAllDropDownList()
           BusinessType_Visible
            
        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String) '按鈕
        Select Case Param
            Case "btnQuery"     '查詢
                If funCheckData() Then
                    ViewState.Item("DoQuery") = "Y"
                    DoQuery()
                End If
            Case "btnDownload"  '下傳
                DoDownload()
            Case "btnActionX"   '清除
                DoClear()
        End Select
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args) '拆SQL參數(ht：Business/OM2)

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
                        txtUnValidDateB.DateText = ht("txtUnValidDateB").ToString()
                        txtUnValidDateE.DateText = ht("txtUnValidDateE").ToString()
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

    Private Sub DoQuery() '查詢
        Dim objOM As New OM2()
        gvMain.Visible = True
        ViewState.Item("DoQuery") = "Y"

        Dim strCompID As String
        If UserProfile.SelectCompRoleID = "ALL" Then
            strCompID = ddlCompID.SelectedValue
        Else
            strCompID = UserProfile.SelectCompRoleID
        End If

        Try
            IsDoQuery.Value = "Y"
            If ddlOrganType.SelectedValue = "" Then
                pcMain.DataTable = objOM.OM2000QueryOrganizationAndFlow(
                "CompID=" & strCompID, _
                "OrganID=" & Split(ucOrganID.Text, "-")(0), _
                "OrganName=" & txtOrganName.Text, _
                "BusinessType=" & ddlBusinessType.SelectedValue, _
                "InValidFlag=" & ddlInValidFlag.SelectedValue, _
                "ValidDateB=" & txtValidDateB.DateText, _
                "ValidDateE=" & txtValidDateE.DateText, _
                "UnValidDateB=" & txtUnValidDateB.DateText, _
                "UnValidDateE=" & txtUnValidDateE.DateText)
            ElseIf ddlOrganType.SelectedValue = "1" Then
                pcMain.DataTable = objOM.OM2000QueryOrganization(
                "CompID=" & strCompID, _
                "OrganID=" & Split(ucOrganID.Text, "-")(0), _
                "OrganName=" & txtOrganName.Text, _
                "InValidFlag=" & ddlInValidFlag.SelectedValue, _
                "ValidDateB=" & txtValidDateB.DateText, _
                "ValidDateE=" & txtValidDateE.DateText, _
                "UnValidDateB=" & txtUnValidDateB.DateText, _
                "UnValidDateE=" & txtUnValidDateE.DateText)
            ElseIf ddlOrganType.SelectedValue = "2" Then
                pcMain.DataTable = objOM.OM2000QueryOrganizationFlow(
                "CompID=" & strCompID, _
                "OrganID=" & Split(ucOrganID.Text, "-")(0), _
                "OrganName=" & txtOrganName.Text, _
                "BusinessType=" & ddlBusinessType.SelectedValue, _
                "InValidFlag=" & ddlInValidFlag.SelectedValue, _
                "ValidDateB=" & txtValidDateB.DateText, _
                "ValidDateE=" & txtValidDateE.DateText, _
                "UnValidDateB=" & txtUnValidDateB.DateText, _
                "UnValidDateE=" & txtUnValidDateE.DateText, _
                "BusinessType=" & ddlBusinessType.SelectedValue)
            End If

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
        End Try
    End Sub

    Private Sub DoDownload()
        Try
            If IsDoQuery.Value = "Y" Then
                Dim strCompID As String = ""
                Dim strXml As New StringBuilder()
                Dim objOM2 As New OM2
                If pcMain.DataTable.Rows.Count > 0 Then
                    '產出檔頭
                    Dim strFileName As String = ""
                    strFileName = Bsp.Utility.GetNewFileName("OM2000組織資料維護-") & ".xls"

                    Response.ClearContent()
                    Response.BufferOutput = True
                    Response.Charset = "utf-8"
                    Response.ContentType = "application/save-as"         '隱藏檔案網址路逕的下載
                    Response.AddHeader("Content-Transfer-Encoding", "binary")
                    Response.ContentEncoding = System.Text.Encoding.UTF8
                    Response.AddHeader("content-disposition", "attachment; filename=" & Server.UrlPathEncode(strFileName))

                    strXml.AppendLine("<?xml version='1.0'?><?mso-application progid='Excel.Sheet'?>")
                    strXml.AppendLine("<Workbook xmlns='urn:schemas-microsoft-com:office:spreadsheet'")
                    strXml.AppendLine("xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:x='urn:schemas-microsoft-com:office:excel'")
                    strXml.AppendLine("xmlns:ss='urn:schemas-microsoft-com:office:spreadsheet' xmlns:html='http://www.w3.org/TR/REC-html40'>")
                    strXml.AppendLine("<DocumentProperties xmlns='urn:schemas-microsoft-com:office:office'>")
                    strXml.AppendLine("<Author></Author><LastAuthor></LastAuthor><Created>2010-09-08T14:07:11Z</Created><Company>mxh</Company><Version>1990</Version>")
                    strXml.AppendLine("</DocumentProperties>")
                    strXml.AppendLine("<Styles><Style ss:ID='Default' ss:Name='Normal'>")
                    strXml.AppendLine("<Alignment ss:Vertical='Center'/>")
                    strXml.AppendLine("<Borders/>")
                    strXml.AppendLine("<Font ss:FontName='新細明體' x:CharSet='134' ss:Size='12'/>")
                    strXml.AppendLine("<Interior/>")
                    strXml.AppendLine("<NumberFormat/>")
                    strXml.AppendLine("<Protection/>")
                    strXml.AppendLine("</Style>")

                    strXml.AppendLine("<Style ss:ID='Header'>")
                    strXml.AppendLine("<Alignment ss:Horizontal='Center'/>")
                    strXml.AppendLine("<Borders>")
                    strXml.AppendLine("<Border ss:Position='Bottom' ss:LineStyle='Continuous' ss:Weight='1'/>")
                    strXml.AppendLine("<Border ss:Position='Left' ss:LineStyle='Continuous' ss:Weight='1'/>")
                    strXml.AppendLine("<Border ss:Position='Right' ss:LineStyle='Continuous' ss:Weight='1'/>")
                    strXml.AppendLine("<Border ss:Position='Top' ss:LineStyle='Continuous' ss:Weight='1'/>")
                    strXml.AppendLine("</Borders>")
                    strXml.AppendLine("<Font ss:Bold='1' ss:Size='9'/>")
                    strXml.AppendLine("</Style>")

                    strXml.AppendLine("<Style ss:ID='border'><NumberFormat ss:Format='@'/>")
                    strXml.AppendLine("<Borders>")
                    strXml.AppendLine("<Border ss:Position='Bottom' ss:LineStyle='Continuous' ss:Weight='1'/>")
                    strXml.AppendLine("<Border ss:Position='Left' ss:LineStyle='Continuous' ss:Weight='1'/>")
                    strXml.AppendLine("<Border ss:Position='Right' ss:LineStyle='Continuous' ss:Weight='1'/>")
                    strXml.AppendLine("<Border ss:Position='Top' ss:LineStyle='Continuous' ss:Weight='1'/>")
                    strXml.AppendLine("</Borders>")
                    strXml.AppendLine("<Font ss:Size='9'/>")
                    strXml.AppendLine("</Style>")
                    strXml.AppendLine("</Styles>")

                    strXml = QueryResults(strXml)

                    strXml.AppendLine("</Workbook>")

                    Response.Write(strXml.ToString())
                    'Response.End()
                    Response.Flush()
                    Response.SuppressContent = True
                    ApplicationInstance.CompleteRequest()
                Else
                    Bsp.Utility.ShowFormatMessage(Me, "W_00030", "請先查詢有資料，才能下傳!")
                End If
            Else
                Bsp.Utility.ShowFormatMessage(Me, "W_00030", "請先查詢有資料，才能下傳!")
            End If

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoDownload", ex)
        End Try

    End Sub
    '查詢結果
    Function QueryResults(ByVal strXml As StringBuilder) As StringBuilder
        Dim objOM2 As New OM2

        Try
            Dim strCompID As String = ""
            If UserProfile.SelectCompRoleID = "ALL" Then
                strCompID = ddlCompID.SelectedValue
            Else
                strCompID = UserProfile.SelectCompRoleID
            End If

            If ddlOrganType.SelectedValue = "" Then
                '行政組織
                Using dt As DataTable = objOM2.QueryOrganizationByDownload( _
                                    "CompID=" & strCompID, _
                                    "OrganID=" & Split(ucOrganID.Text, "-")(0), _
                                    "OrganName=" & txtOrganName.Text, _
                                    "InValidFlag=" & ddlInValidFlag.SelectedValue, _
                                    "ValidDateB=" & txtValidDateB.DateText, _
                                    "ValidDateE=" & txtValidDateE.DateText, _
                "UnValidDateB=" & txtUnValidDateB.DateText, _
                "UnValidDateE=" & txtUnValidDateE.DateText)

                    strXml.AppendLine("<Worksheet ss:Name='行政組織'>")
                    strXml.AppendLine("<Table x:FullColumns='1' x:FullRows='1'>")

                    If dt.Rows.Count > 0 Then
                        For i As Integer = 0 To dt.Columns.Count - 1
                            strXml.AppendLine("<Column ss:Width='100'/>")
                        Next

                        strXml = GenDatas(strXml, dt)
                    End If
                    strXml.AppendLine("</Table>")
                    strXml.AppendLine("</Worksheet>")
                End Using

                '功能組織
                Using dtFlow As DataTable = objOM2.QueryOrganizationFlowByDownload( _
                                    "CompID=" & strCompID, _
                                    "OrganID=" & Split(ucOrganID.Text, "-")(0), _
                                    "OrganName=" & txtOrganName.Text, _
                                    "InValidFlag=" & ddlInValidFlag.SelectedValue, _
                                    "ValidDateB=" & txtValidDateB.DateText, _
                                    "ValidDateE=" & txtValidDateE.DateText, _
                "UnValidDateB=" & txtUnValidDateB.DateText, _
                "UnValidDateE=" & txtUnValidDateE.DateText, _
                                    "BusinessType=" & ddlBusinessType.SelectedValue)

                    strXml.AppendLine("<Worksheet ss:Name='功能組織'>")
                    strXml.AppendLine("<Table x:FullColumns='1' x:FullRows='1'>")

                    If dtFlow.Rows.Count > 0 Then
                        For i As Integer = 0 To dtFlow.Columns.Count - 1
                            strXml.AppendLine("<Column ss:Width='100'/>")
                        Next

                        strXml = GenDatas(strXml, dtFlow)
                    End If
                    strXml.AppendLine("</Table>")
                    strXml.AppendLine("</Worksheet>")
                End Using
            ElseIf ddlOrganType.SelectedValue = "1" Then
                '行政組織
                Using dt As DataTable = objOM2.QueryOrganizationByDownload( _
                                    "CompID=" & strCompID, _
                                    "OrganID=" & Split(ucOrganID.Text, "-")(0), _
                                    "OrganName=" & txtOrganName.Text, _
                                    "InValidFlag=" & ddlInValidFlag.SelectedValue, _
                                    "ValidDateB=" & txtValidDateB.DateText, _
                                    "ValidDateE=" & txtValidDateE.DateText, _
                "UnValidDateB=" & txtUnValidDateB.DateText, _
                "UnValidDateE=" & txtUnValidDateE.DateText)

                    strXml.AppendLine("<Worksheet ss:Name='行政組織'>")
                    strXml.AppendLine("<Table x:FullColumns='1' x:FullRows='1'>")

                    If dt.Rows.Count > 0 Then
                        For i As Integer = 0 To dt.Columns.Count - 1
                            strXml.AppendLine("<Column ss:Width='100'/>")
                        Next

                        strXml = GenDatas(strXml, dt)
                    End If
                    strXml.AppendLine("</Table>")
                    strXml.AppendLine("</Worksheet>")
                End Using
            ElseIf ddlOrganType.SelectedValue = "2" Then
                '功能組織
                Using dtFlow As DataTable = objOM2.QueryOrganizationFlowByDownload( _
                                    "CompID=" & strCompID, _
                                    "OrganID=" & Split(ucOrganID.Text, "-")(0), _
                                    "OrganName=" & txtOrganName.Text, _
                                    "InValidFlag=" & ddlInValidFlag.SelectedValue, _
                                    "ValidDateB=" & txtValidDateB.DateText, _
                                    "ValidDateE=" & txtValidDateE.DateText, _
                "UnValidDateB=" & txtUnValidDateB.DateText, _
                "UnValidDateE=" & txtUnValidDateE.DateText, _
                                    "BusinessType=" & ddlBusinessType.SelectedValue)

                    strXml.AppendLine("<Worksheet ss:Name='功能組織'>")
                    strXml.AppendLine("<Table x:FullColumns='1' x:FullRows='1'>")

                    If dtFlow.Rows.Count > 0 Then
                        For i As Integer = 0 To dtFlow.Columns.Count - 1
                            strXml.AppendLine("<Column ss:Width='100'/>")
                        Next

                        strXml = GenDatas(strXml, dtFlow)
                    End If
                    strXml.AppendLine("</Table>")
                    strXml.AppendLine("</Worksheet>")
                End Using
            End If
        Catch ex As Exception
            Throw ex
        End Try

        Return strXml
    End Function

    Function GenDatas(ByVal strXml As StringBuilder, ByVal dt As DataTable) As StringBuilder
        Dim objOM As New OM2
        Dim strCompID As String
        If UserProfile.SelectCompRoleID = "ALL" Then
            strCompID = ddlCompID.SelectedValue
        Else
            strCompID = UserProfile.SelectCompRoleID
        End If

        strXml.AppendLine("<Row>")

        For i As Integer = 0 To dt.Columns.Count - 1

            strXml.AppendLine("<Cell ss:StyleID='Header'><Data ss:Type='String'>" + dt.Columns(i).ToString() + "</Data></Cell>")
        Next

        strXml.AppendLine("</Row>")

        For i As Integer = 0 To dt.Rows.Count - 1
            strXml.AppendLine("<Row>")

            For j As Integer = 0 To dt.Columns.Count - 1
                If dt.Columns(j).ToString() = "比對簽核單位" Then
                    Dim arrFlowOrganID() As String = dt.Rows(i)(j).ToString().Split("|")
                    Dim strFlowOrganID As String = ""

                    For Each FlowOrganID As String In arrFlowOrganID
                        strFlowOrganID = strFlowOrganID + "|" + objOM.GetOrganFlowName(strCompID, FlowOrganID)
                    Next

                    If strFlowOrganID <> "" Then
                        strFlowOrganID = strFlowOrganID.Substring(1)
                    End If

                    strXml.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + strFlowOrganID + "</Data></Cell>")
                Else
                    strXml.AppendLine("<Cell ss:StyleID='border'><Data ss:Type='String'>" + dt.Rows(i)(j).ToString() + "</Data></Cell>")
                End If
            Next
            strXml.AppendLine("</Row>")
        Next

        Return strXml
    End Function

    Protected Sub gvMain_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMain.RowCommand
        Dim objOM2 As New OM2()

        Dim strCompID As String
        If UserProfile.SelectCompRoleID = "ALL" Then
            strCompID = ddlCompID.SelectedValue
        Else
            strCompID = UserProfile.SelectCompRoleID
        End If

        If e.CommandName = "Detail" Then
            Dim a As New FlowBackInfo()
            a.MenuNodeTitle = "回清單"
            a.URL = "~/OM/OM2000.aspx"

            If gvMain.DataKeys(Me.selectedRow(gvMain))("OrganType").ToString() = "行政組織" Then
                TransferFramePage(Bsp.MySettings.FlowRedirectPage, Nothing, "FlowID=ORGINFO", a, _
                    ddlCompID.ID & "=" & strCompID, _
                    ddlOrganType.ID & "=" & ddlOrganType.SelectedValue, _
                    ddlOrganID.ID & "=" & Split(ucOrganID.Text, "-")(0), _
                    txtOrganName.ID & "=" & txtOrganName.Text, _
                    ddlInValidFlag.ID & "=" & ddlInValidFlag.SelectedValue, _
                    txtValidDateB.ID & "=" & txtValidDateB.DateText, _
                    txtValidDateE.ID & "=" & txtValidDateE.DateText, _
                    txtUnValidDateB.ID & "=" & txtUnValidDateB.DateText, _
                    txtUnValidDateE.ID & "=" & txtUnValidDateE.DateText, _
                    "PageNo=" & pcMain.PageNo.ToString(), _
                    "SelectedCompID=" & gvMain.DataKeys(Me.selectedRow(gvMain))("CompID").ToString(), _
                    "SelectedCompName=" & gvMain.DataKeys(Me.selectedRow(gvMain))("CompName").ToString, _
                    "SelectedOrganID=" & gvMain.DataKeys(Me.selectedRow(gvMain))("OrganID").ToString(), _
                    "SelectedOrganName=" & gvMain.DataKeys(Me.selectedRow(gvMain))("OrganName").ToString(), _
                    "SelectedValidDateB" & gvMain.DataKeys(Me.selectedRow(gvMain))("hidValidDateB").ToString(), _
                    "SelectedValidDateE" & gvMain.DataKeys(Me.selectedRow(gvMain))("ValidDateE").ToString(), _
                    "SelectedOrganType=1-行政組織", _
                    "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
            Else
                TransferFramePage(Bsp.MySettings.FlowRedirectPage, Nothing, "FlowID=ORGFLOWINFO", a, _
                    ddlCompID.ID & "=" & strCompID, _
                    ddlOrganType.ID & "=" & ddlOrganType.SelectedValue, _
                    ddlOrganID.ID & "=" & Split(ucOrganID.Text, "-")(0), _
                    txtOrganName.ID & "=" & txtOrganName.Text, _
                    ddlInValidFlag.ID & "=" & ddlInValidFlag.SelectedValue, _
                    txtValidDateB.ID & "=" & txtValidDateB.DateText, _
                    txtValidDateE.ID & "=" & txtValidDateE.DateText, _
                    txtUnValidDateB.ID & "=" & txtUnValidDateB.DateText, _
                    txtUnValidDateE.ID & "=" & txtUnValidDateE.DateText, _
                    "PageNo=" & pcMain.PageNo.ToString(), _
                    "SelectedCompID=" & gvMain.DataKeys(Me.selectedRow(gvMain))("CompID").ToString(), _
                    "SelectedCompName=" & gvMain.DataKeys(Me.selectedRow(gvMain))("CompName").ToString, _
                    "SelectedOrganID=" & gvMain.DataKeys(Me.selectedRow(gvMain))("OrganID").ToString(), _
                    "SelectedOrganName=" & gvMain.DataKeys(Me.selectedRow(gvMain))("OrganName").ToString(), _
                    "SelectedValidDateB" & gvMain.DataKeys(Me.selectedRow(gvMain))("ValidDateB").ToString(), _
                    "SelectedValidDateE" & gvMain.DataKeys(Me.selectedRow(gvMain))("ValidDateE").ToString(), _
                    "SelectedOrganType=2-功能組織", _
                    "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
            End If

        End If
    End Sub

#Region "funCheckData"
    Private Function funCheckData() As Boolean
        Dim objOM2 As New OM2()

        Dim strWhere As String = ""
        '/*********************組織資料維護查詢*********************/


        '生效起迄日
        If txtValidDateB.DateText = "" Or txtValidDateB.DateText = "____/__/__" Then
            txtValidDateB.DateText = ""
        End If

        If txtValidDateE.DateText = "" Or txtValidDateE.DateText = "____/__/__" Then
            txtValidDateE.DateText = ""
        End If

        If txtUnValidDateB.DateText = "" Or txtUnValidDateB.DateText = "____/__/__" Then
            txtUnValidDateB.DateText = ""
        End If

        If txtUnValidDateE.DateText = "" Or txtUnValidDateE.DateText = "____/__/__" Then
            txtUnValidDateE.DateText = ""
        End If

        Return True
    End Function

#End Region

    '清除
    Private Sub DoClear()
        ViewState.Item("DoQuery") = ""
        gvMain.Visible = False

        ucOrganID.Text = ""
        ddlOrganType.SelectedValue = "" '組織類型
        'ddlOrganID.SelectedValue = "" '部門代碼
        txtOrganName.Text = "" '部門名稱
        ddlInValidFlag.SelectedValue = "" '狀態(無效註記)
        txtValidDateB.DateText = "" '生效起日
        txtValidDateE.DateText = "" '生效訖日
        txtUnValidDateB.DateText = ""  '無效起日
        txtUnValidDateE.DateText = ""  '無效訖日
        'ddlBusinessType.SelectedValue = "" '業務類別
        subLoadOrganIDAllDropDownList()

        pcMain.PageNo = Nothing
        pcMain.PageCount = Nothing
        pcMain.RecordCount = Nothing
        pcMain.DataTable = Nothing
    End Sub

    Protected Sub ddlOrganType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlOrganType.SelectedIndexChanged
        If ddlOrganType.SelectedValue = "1" Then
            subLoadOrganIDDropDownList()
        ElseIf ddlOrganType.SelectedValue = "2" Then
            subLoadOrganIDFlowDropDownList()
        ElseIf ddlOrganType.SelectedValue = "" Then
            subLoadOrganIDAllDropDownList()
        End If
        BusinessType_Visible()
    End Sub

    Private Sub subLoadOrganIDDropDownList()
        OM2.FillDDL(ddlOrganID, "Organization", "RTrim(OrganID)+'-'+OrganName + case when InValidFlag = '1' then '(行政組織-無效)' else '(行政組織)' end", "OrganName + case when InValidFlag = '1' then '(行政組織-無效)' else '(行政組織)' end", OM2.DisplayType.Full, "", "And CompID = " + Bsp.Utility.Quote(UserProfile.SelectCompRoleID), "Order By InValidFlag, VirtualFlag, Code")
        'ddlOrganID.Items.Insert(0, New ListItem("----請選擇----", ""))
        ddlBusinessType.Items.Clear()
        ddlBusinessType.Items.Insert(0, New ListItem("----請選擇----", ""))
        'lblBusinessType.Visible = False
        'ddlBusinessType.Visible = False
        'lblBusinessTypeMsg.Visible = False
    End Sub

    Public Sub subLoadOrganIDFlowDropDownList() '功能
        OM2.FillDDL(ddlOrganID, "OrganizationFlow", "RTRIM(OrganID)+'-'+OrganName + case when InValidFlag = '1' then '(功能組織-無效)' else '(功能組織)' end ", "OrganName + case when InValidFlag = '1' then '(功能組織-無效)' else '(功能組織)' end", OM2.DisplayType.Full, "", "And CompID = " + Bsp.Utility.Quote(UserProfile.SelectCompRoleID), "Order By InValidFlag, CompareFlag, Code")
        'ddlOrganID.Items.Insert(0, New ListItem("----請選擇----", ""))

        OM2.FillDDL(ddlBusinessType, "HRCodeMap", "RTrim(Code)", "CodeCName", OM2.DisplayType.Full, "", "And TabName = 'Business' And FldName = 'BusinessType'", "")
        ddlBusinessType.Items.Insert(0, New ListItem("----請選擇----", ""))
        'lblBusinessType.Visible = True
        'ddlBusinessType.Visible = True
        'lblBusinessTypeMsg.Visible = True
    End Sub

    Public Sub subLoadOrganIDAllDropDownList()
        'OM2.FillDDL(ddlOrganID, " Organization ", " OrganID ", " OrganName + case when InValidFlag = '1' then '(無效)' else '' end", OM2.DisplayType.Full, "Where CompID = " + Bsp.Utility.Quote(UserProfile.SelectCompRoleID) + "And VirtualFlag = '0' Union Select OrganID,OrganName,OrganID + '-' + OrganName + case when InValidFlag = '1' then '(無效)' else '' end from OrganizationFlow ", "and CompID = " + Bsp.Utility.Quote(UserProfile.SelectCompRoleID) + " and CompareFlag = '0'", " order by OrganID ")

        'OM2.FillDDLOM2000(ddlOrganID, "Organization", " OrganID+'-'+OrganName + case when InValidFlag = '1' then '(無效)' else '' end ", " OrganName + case when InValidFlag = '1' then '(無效)' else '' end", "InValidFlag, '1' AS Seq", OM2.DisplayType.OnlyName, _
        '           "Where CompID = " + Bsp.Utility.Quote(UserProfile.SelectCompRoleID) + "And VirtualFlag = '0' Union Select  OrganID + '-' + OrganName + case when InValidFlag = '1' then '(無效)' else '' end ,OrganName + case when InValidFlag = '1' then '(無效)' else '' end, OrganID + '-' + OrganName + case when InValidFlag = '1' then '(無效)' else '' end , OrganName + case when InValidFlag = '1' then '(無效)' else '' end asFullName,  InValidFlag, '2' AS Seq from OrganizationFlow", _
        '           "AND CompID = " + Bsp.Utility.Quote(UserProfile.SelectCompRoleID) + " and CompareFlag = '0'", " order by InValidFlag, Seq, Code ")

        OM2.FillDDLOM2000(ddlOrganID, "Organization", " RTRIM(OrganID)+'-'+OrganName + case when InValidFlag = '1' then '(行政組織-無效)' else '(行政組織)' end ", " OrganName + case when InValidFlag = '1' then '(行政組織-無效)' else '(行政組織)' end", "InValidFlag, '1' AS Seq", OM2.DisplayType.Full, _
           "Where CompID = " + Bsp.Utility.Quote(UserProfile.SelectCompRoleID) + "And VirtualFlag = '0' Union Select RTRIM(OrganID)+'-'+OrganName + case when InValidFlag = '1' then '(功能組織-無效)' else '(功能組織)' end as Code ,  OrganName + case when InValidFlag = '1' then '(功能組織-無效)' else '(功能組織)' end, RTRIM(OrganID) + '-' + OrganName + case when InValidFlag = '1' then '(功能組織-無效)' else '(功能組織)' end, InValidFlag, '2' AS Seq from OrganizationFlow", _
           "AND CompID = " + Bsp.Utility.Quote(UserProfile.SelectCompRoleID), " order by InValidFlag, Seq, Code ")

        'ddlOrganID.Items.Insert(0, New ListItem("----請選擇----", ""))

        'OM2.FillDDL(ddlBusinessType, "HRCodeMap", "RTrim(Code)", "CodeCName", OM2.DisplayType.Full, "", "And TabName = 'Business' And FldName = 'BusinessType'", "")
        'ddlBusinessType.Items.Insert(0, New ListItem("----請選擇----", ""))
        ddlBusinessType.Items.Clear()
        ddlBusinessType.Items.Insert(0, New ListItem("----請選擇----", ""))
        'lblBusinessType.Visible = False
        'ddlBusinessType.Visible = False
        'lblBusinessTypeMsg.Visible = False
    End Sub
#Region "ucOrganID"
    'Public Sub GetData()
    '    Using dt = Bsp.DB.ExecuteDataSet(CommandType.Text, "Select distinct OrganID+'-'+OrganName  AS Code ,'' FROM  OrganizationWait   Where 1=1  and CompID=" + Bsp.Utility.Quote(UserProfile.SelectCompRoleID) + "  order by Code", "eHRMSDB").Tables(0)
    '        ddlOrganID.Items.Clear()
    '        For i As Integer = 0 To dt.Rows.Count - 1
    '            ddlOrganID.Items.Insert(0, New ListItem(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
    '        Next
    '    End Using

    'End Sub

    Public Delegate Sub SelectTextChangedHandler(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event ucSelectTextChangedHandler_TextChange As SelectTextChangedHandler

    Protected Overridable Sub SelectTextChanged(ByVal e As System.EventArgs)
        RaiseEvent ucSelectTextChangedHandler_TextChange(Me, e)
    End Sub

    Protected Sub ucOrganID_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTextChange.Click
        SelectTextChanged(e)
    End Sub
#End Region

    Private Sub BusinessType_Visible()
        If Not UserProfile.SelectCompRoleID = "SPHBK1" Then
            lblBusinessType.Visible = False
            ddlBusinessType.Visible = False
            lblBusinessTypeMsg.Visible = False
        Else
            If ddlOrganType.SelectedValue = "2" Then
                lblBusinessType.Visible = True
                ddlBusinessType.Visible = True
                lblBusinessTypeMsg.Visible = True
            Else
                lblBusinessType.Visible = False
                ddlBusinessType.Visible = False
                lblBusinessTypeMsg.Visible = False
            End If
        End If
    End Sub
End Class
