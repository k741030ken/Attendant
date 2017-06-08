'*********************************************************************************
'功能說明：留守人員設定查詢刪除
'建立人員： Eugerwu Kevin
'建立日期：2016.12.16
'*********************************************************************************

Imports System.Data
Imports System.Data.Common

Partial Class OV_OV3100

    Inherits PageBase
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            LoadDate()
            initArgs()
        End If
    End Sub
    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then '人員OK 縣市XX
            initArgs()
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
                        ucBeginDate.DateText = ht("ucBeginDate").ToString().Replace("-", "/")
                        ucEndDate.DateText = ht("ucEndDate").ToString().Replace("-", "/")
                    End If
                End If
            Next
            '欄位開關
            fieldSwitch()
            If ht("DoQuery") = "Y" Then
                If ht.ContainsKey("PageNo") Then pcMain1.PageNo = Convert.ToInt32(ht("PageNo"))
                DoQuery()
            Else
                DoClear()
            End If

        End If
    End Sub
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
    Public Sub initArgs()
        '員工編號
        ucQueryEmp.ShowCompRole = "False"
        ucQueryEmp.InValidFlag = "N"
        ucQueryEmp.SelectCompID = UserProfile.SelectCompRoleID
    End Sub
    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnAdd"       '新增
                DoAdd()
            Case "btnUpdate"    '修改
                DoUpdate()
            Case "btnQuery"     '查詢
                If checkdate() = True Then
                    DoQuery()
                End If
            Case "btnDelete"    '刪除

                If "-1".Equals(selectedRow(gvMain).ToString.Trim) And "-1".Equals(selectedRow(gvMain1).ToString.Trim) Then
                    Bsp.Utility.ShowMessage(Me, "請選擇資料")
                Else
                    DoDelete()
                End If

            Case "btnActionX"   '清除
                DoClear()
        End Select
    End Sub
    Private Sub LoadDate()
        Dim CompID = UserProfile.SelectCompRoleID


        Bsp.Utility.FillDDL(ddlBranchName, "eHRMSDB", "WorkSite", " convert(varchar,RTrim(WorkSiteID))", " Remark", Bsp.Utility.DisplayType.OnlyName, "", "and CompID=" + Bsp.Utility.Quote(CompID), "order by WorkSiteID")
        Bsp.Utility.FillDDL(ddlCityCode, "eHRMSDB", "HRCodeMap", " convert(varchar,RTrim(Code))", " CodeCName", Bsp.Utility.DisplayType.OnlyName, "", "and TabName='WorkSite' and FldName='CityCode'and NotShowFlag='0'", "order by SortFld, Code")
        Bsp.Utility.FillDDL(ddlDisasterType, "AattendantDB", "AT_CodeMap", " convert(varchar,RTrim(Code))", " CodeCName", Bsp.Utility.DisplayType.OnlyName, "", "and TabName='NaturalDisasterByCity' and FldName='DisasterType' and NotShowFlag='0'", "order by SortFld, Code")
        ddlCityCode.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        ddlDisasterType.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        ddlBranchName.Items.Insert(0, New ListItem("　- -請選擇- -", ""))

    End Sub
    Protected Sub DoAdd()
        Dim btnA As New ButtonState(ButtonState.emButtonType.Add)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
        Dim btnC As New ButtonState(ButtonState.emButtonType.Cancel)

        btnA.Caption = "存檔返回"
        btnX.Caption = "返回"
        btnC.Caption = "清除"

        Me.TransferFramePage("~/OV/OV3101.aspx", New ButtonState() {btnA, btnC, btnX}, _
            ddlPersonOrCity.ID & "=" & ddlPersonOrCity.SelectedValue, _
            ucBeginDate.ID & "=" & IIf(ucBeginDate.DateText = "____/__/__", "", ucBeginDate.DateText), _
            ucEndDate.ID & "=" & IIf(ucEndDate.DateText = "____/__/__", "", ucEndDate.DateText), _
            ddlDisasterType.ID & "=" & ddlDisasterType.SelectedValue, _
            ddlCityCode.ID & "=" & ddlCityCode.SelectedValue, _
            ddlBranchName.ID & "=" & ddlBranchName.SelectedValue, _
            txtEmpID.ID & "=" & txtEmpID.Text, _
            "PageNo=" & IIf(pcMain.PageNo.ToString() <> "", pcMain.PageNo.ToString(), pcMain1.PageNo.ToString()), _
            "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))

    End Sub
    Protected Sub DoUpdate() '人員OK 縣市XX
        If ViewState.Item("DoQuery") <> "Y" Then
            Bsp.Utility.ShowMessage(Me, "請先查詢並選取資料")
            Return
        Else
            Try
                Dim btnA As New ButtonState(ButtonState.emButtonType.Add)
                Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
                Dim btnC As New ButtonState(ButtonState.emButtonType.Cancel)
                Dim CityVEmp As String = ddlPersonOrCity.SelectedValue


                btnA.Caption = "存檔返回"
                btnX.Caption = "返回"
                btnC.Caption = "清除"

                If CityVEmp = "CityTable" Then

                    Dim DisasterTime() As String = gvMain.DataKeys(selectedRow(gvMain))("DisasterTime").ToString().Split("~")
                    Dim DisasterDate() As String = gvMain.DataKeys(selectedRow(gvMain))("DisasterDate").ToString().Split("~")

                    'Me.TransferFramePage("~/OV/OV1002.aspx", New ButtonState() {btnA, btnC, btnX}, _
                    'ddlPersonOrCity.ID & "=" & CityVEmp, _
                    'ucBeginDate.ID & "=" & ucBeginDate.DateText, _
                    'ucEndDate.ID & "=" & ucEndDate.DateText, _
                    'ddlType.ID & "=" & ddlType.SelectedValue, _
                    'ddlCityCode.ID & "=" & ddlCityCode.SelectedValue, _
                    'ddlBranchName.ID & "=" & ddlBranchName.SelectedValue, _
                    '"PageNo=" & pcMain.PageNo.ToString(), _
                    '"SCityCode=" & City(0), _
                    '"SCityName=" & City(1), _
                    '"ddlType=" & gvMain.DataKeys(selectedRow(gvMain))("ddlType").ToString(),
                    '"SBranchName=" & gvMain.DataKeys(selectedRow(gvMain))("BranchName").ToString(),
                    '"SWorkSiteID=" & gvMain.DataKeys(selectedRow(gvMain))("WorkSiteID").ToString(), _
                    '"SBeginDate=" & DisasterDate(0), _
                    '"SEndDate=" & DisasterDate(1), _
                    '"SBeginTime=" & DisasterTime(0), _
                    '"SEndTime=" & DisasterTime(1), _
                    '"SDisasterType=" & gvMain.DataKeys(selectedRow(gvMain))("DisasterType").ToString(), _
                    '"DoQuery=" & ViewState.Item("DoQuery")
                    ')
                    Me.TransferFramePage("~/OV/OV3102.aspx", New ButtonState() {btnA, btnC, btnX}, _
                    ddlPersonOrCity.ID & "=" & CityVEmp, _
                    ucBeginDate.ID & "=" & ucBeginDate.DateText, _
                    ucEndDate.ID & "=" & ucEndDate.DateText, _
                    ddlDisasterType.ID & "=" & ddlDisasterType.SelectedValue, _
                    ddlCityCode.ID & "=" & ddlCityCode.SelectedValue, _
                    ddlBranchName.ID & "=" & ddlBranchName.SelectedValue, _
                    "PageNo=" & pcMain.PageNo.ToString(), _
                    "SCityCode=" & gvMain.DataKeys(selectedRow(gvMain))("CityCode").ToString(), _
                    "SCityName=" & gvMain.DataKeys(selectedRow(gvMain))("CityName").ToString(), _
                    "ddlType=" & gvMain.DataKeys(selectedRow(gvMain))("ddlType").ToString(),
                    "SBranchName=" & gvMain.DataKeys(selectedRow(gvMain))("BranchName").ToString(),
                    "SWorkSiteID=" & gvMain.DataKeys(selectedRow(gvMain))("WorkSiteID").ToString(), _
                    "SBeginDate=" & DisasterDate(0), _
                    "SEndDate=" & DisasterDate(1), _
                    "SBeginTime=" & DisasterTime(0), _
                    "SEndTime=" & DisasterTime(1), _
                    "SDisasterType=" & gvMain.DataKeys(selectedRow(gvMain))("DisasterType").ToString(), _
                    "LastChgCompID=" & gvMain.DataKeys(selectedRow(gvMain))("LastChgCompID").ToString(), _
                    "LastChgEmpID=" & gvMain.DataKeys(selectedRow(gvMain))("LastChgEmpID").ToString(), _
                    "LastChgDate=" & gvMain.DataKeys(selectedRow(gvMain))("LastChgDate").ToString(), _
                    "DoQuery=" & ViewState.Item("DoQuery")
                    )
                ElseIf CityVEmp = "PersonTable" Then

                    Dim DisasterTime() As String = gvMain1.DataKeys(selectedRow(gvMain1))("DisasterTime").ToString().Split("~")
                    Dim DisasterDate() As String = gvMain1.DataKeys(selectedRow(gvMain1))("DisasterDate").ToString().Split("~")
                    Dim EmpName As String = gvMain1.DataKeys(selectedRow(gvMain1))("EmpName").ToString().Replace(" ", "-")

                    'Me.TransferFramePage("~/OV/OV1002.aspx", New ButtonState() {btnA, btnC, btnX}, _
                    'ddlPersonOrCity.ID & "=" & CityVEmp, _
                    'ucBeginDate.ID & "=" & ucBeginDate.DateText, _
                    'ucEndDate.ID & "=" & ucEndDate.DateText, _
                    'ddlType.ID & "=" & ddlType.SelectedValue, _
                    'txtEmpID.ID & "=" & txtEmpID.Text, _
                    '"ddlType=" & gvMain1.DataKeys(selectedRow(gvMain1))("ddlType").ToString(), _
                    '"PageNo=" & pcMain1.PageNo.ToString(), _
                    '"SEmpName=" & EmpName, _
                    '"SRemark=" & gvMain1.DataKeys(selectedRow(gvMain1))("Remark").ToString(), _
                    '"SBeginTime=" & DisasterTime(0), _
                    '"SEndTime=" & DisasterTime(1), _
                    '"SBeginDate=" & DisasterDate(0), _
                    '"SEndDate=" & DisasterDate(1), _
                    '"SDisasterType=" & gvMain1.DataKeys(selectedRow(gvMain1))("DisasterType").ToString(), _
                    '"DoQuery=" & ViewState.Item("DoQuery")
                    ')
                    Me.TransferFramePage("~/OV/OV3102.aspx", New ButtonState() {btnA, btnC, btnX}, _
                    ddlPersonOrCity.ID & "=" & CityVEmp, _
                    ucBeginDate.ID & "=" & ucBeginDate.DateText, _
                    ucEndDate.ID & "=" & ucEndDate.DateText, _
                    ddlDisasterType.ID & "=" & ddlDisasterType.SelectedValue, _
                    txtEmpID.ID & "=" & txtEmpID.Text, _
                    "PageNo=" & pcMain1.PageNo.ToString(), _
                    "SEmpName=" & EmpName, _
                    "SRemark=" & gvMain1.DataKeys(selectedRow(gvMain1))("Remark").ToString(), _
                    "ddlType=" & gvMain1.DataKeys(selectedRow(gvMain1))("ddlType").ToString(), _
                    "SBeginTime=" & DisasterTime(0), _
                    "SEndTime=" & DisasterTime(1), _
                    "SBeginDate=" & DisasterDate(0), _
                    "SEndDate=" & DisasterDate(1), _
                    "SDisasterType=" & gvMain1.DataKeys(selectedRow(gvMain1))("DisasterType").ToString(), _
                    "LastChgCompID=" & gvMain1.DataKeys(selectedRow(gvMain1))("LastChgCompID").ToString(), _
                    "LastChgEmpID=" & gvMain1.DataKeys(selectedRow(gvMain1))("LastChgEmpID").ToString(), _
                    "LastChgDate=" & gvMain1.DataKeys(selectedRow(gvMain1))("LastChgDate").ToString(), _
                    "DoQuery=" & ViewState.Item("DoQuery")
                    )

                End If
            Catch ex As Exception

                Bsp.Utility.ShowMessage(Me, "尚未選取欲修改資料")
                Return
            End Try
        End If


    End Sub
    Private Sub DoDelete()
        If ViewState.Item("DoQuery") <> "Y" Then
            Bsp.Utility.ShowMessage(Me, "請先查詢並選取資料")
            Return
        End If
        Dim Flag As Boolean = True
        Dim strSQL As New StringBuilder()
        If ddlPersonOrCity.SelectedValue = "CityTable" Then 'CityTable 


            Dim CityCode As String = gvMain.DataKeys(selectedRow(gvMain))("CityCode").ToString()
            Dim DisasterDate() As String = gvMain.DataKeys(selectedRow(gvMain))("DisasterDate").ToString().Split("~")
            Dim Temp() As String = gvMain.DataKeys(selectedRow(gvMain))("DisasterTime").ToString().Split("~")
            Dim DisasterTimeB() As String = Temp(0).Split(":")
            Dim DisasterTimeE() As String = Temp(1).Split(":")
            Dim WorkSiteID As String = gvMain.DataKeys(selectedRow(gvMain))("WorkSiteID").ToString()

            strSQL.AppendLine("delete from NaturalDisasterByCity ")
            strSQL.AppendLine("where 1=1 ")
            strSQL.AppendLine(" And CityCode=" & Bsp.Utility.Quote(CityCode))
            strSQL.AppendLine(" And DisasterStartDate=" & Bsp.Utility.Quote(DisasterDate(0)))
            strSQL.AppendLine(" And DisasterEndDate=" & Bsp.Utility.Quote(DisasterDate(1)))
            strSQL.AppendLine(" And BeginTime=" & Bsp.Utility.Quote(DisasterTimeB(0) & DisasterTimeB(1)))
            strSQL.AppendLine(" And EndTime=" & Bsp.Utility.Quote(DisasterTimeE(0) & DisasterTimeE(1)))
            strSQL.AppendLine(" And WorkSiteID=" & Bsp.Utility.Quote(WorkSiteID))


        ElseIf ddlPersonOrCity.SelectedValue = "PersonTable" Then
            Dim EmpName() As String = gvMain1.DataKeys(selectedRow(gvMain1))("EmpName").ToString().Split(" ")
            Dim DisasterDate() As String = gvMain1.DataKeys(selectedRow(gvMain1))("DisasterDate").ToString().Split("~")
            Dim Temp() As String = gvMain1.DataKeys(selectedRow(gvMain1))("DisasterTime").ToString().Split("~")
            Dim DisasterTime() As String = Temp(0).Split(":")

            strSQL.AppendLine("delete from NaturalDisasterByEmp ")
            strSQL.AppendLine("where 1=1 ")
            strSQL.AppendLine(" And EmpID=" & Bsp.Utility.Quote(EmpName(0)))
            strSQL.AppendLine(" And DisasterStartDate=" & Bsp.Utility.Quote(DisasterDate(0)))
            strSQL.AppendLine(" And BeginTime=" & Bsp.Utility.Quote(DisasterTime(0) & DisasterTime(1)))
        End If
        Using cn As DbConnection = Bsp.DB.getConnection("AattendantDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Try
                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran, "AattendantDB")
                tran.Commit()
            Catch ex As Exception
                tran.Rollback()
                Bsp.Utility.ShowMessage(Me, "刪除失敗")
                Flag = False
                Throw
            Finally
                If tran IsNot Nothing Then tran.Dispose()
            End Try
        End Using
        If Flag Then
            Bsp.Utility.ShowMessage(Me, "刪除成功")
            DoQuery()
        End If

        'End If
    End Sub
    Public Function getCompany(ByVal compID As String) As String
        Dim result As String = ""
        Dim strSQL As New StringBuilder
        Dim dataTable As New DataTable
        strSQL.Append("select CompName FROM Company ")
        strSQL.Append("where CompID = " & Bsp.Utility.Quote(compID))
        dataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)

        result = dataTable.Rows(0).Item("CompName").ToString
        Return result
    End Function

    Public Function getName(ByVal compID As String, ByVal empID As String) As String
        Dim result As String = ""
        Dim strSQL As New StringBuilder
        Dim dataTable As New DataTable
        strSQL.Append("select NameN FROM Personal ")
        strSQL.Append("where  CompID= " & Bsp.Utility.Quote(compID))
        strSQL.Append(" And EmpID = " & Bsp.Utility.Quote(empID))
        dataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)

        result = dataTable.Rows(0).Item("NameN").ToString
        Return result
    End Function
    Public Sub getAdjustTB(ByRef dataTable As DataTable)
        If dataTable.Rows.Count > 0 Then
            For i As Integer = 0 To dataTable.Rows.Count - 1 Step 1
                Dim comp As String = dataTable.Rows(i).Item("LastChgCompName").ToString
                Dim emp As String = dataTable.Rows(i).Item("LastChgEmpName").ToString
                dataTable.Rows(i).Item("LastChgCompName") = getCompany(comp)
                dataTable.Rows(i).Item("LastChgEmpName") = getName(comp, emp)
            Next
        End If
    End Sub
    Private Sub DoQuery()

        Dim strSQL As New StringBuilder()
        ViewState.Item("City_Emp") = ddlPersonOrCity.SelectedValue
        If ddlPersonOrCity.SelectedValue = "CityTable" Then 'CityTable
            strSQL.AppendLine(" select N.CityCode,N.CityName,N.WorkSiteName as BranchName,CONVERT(varchar(10),N.DisasterStartDate,120)+'~'+CONVERT(varchar(10),N.DisasterEndDate,120) As DisasterDate,LEFT(N.BeginTime,2)+':'+RIGHT(N.BeginTime,2) +'~'+  LEFT(N.EndTime,2)+':'+RIGHT(N.EndTime,2) AS DisasterTime,AT.CodeCName as DisasterType,N.CityCode as CityCode,N.WorkSiteID as WorkSiteID,AT.Code as ddlType,N.LastChgCompID as 'LastChgCompName',N.LastChgEmpID as 'LastChgEmpName',N.LastChgCompID,N.LastChgEmpID,format(N.LastChgDate,'yyyy/MM/dd HH:mm:ss') as 'LastChgDate'")
            strSQL.AppendLine(" from NaturalDisasterByCity N ")
            strSQL.AppendLine(" left join AT_CodeMap AT on N.DisasterType=AT.Code and AT.TabName='NaturalDisasterByCity' and AT.FldName='DisasterType'")
            strSQL.AppendLine(" where 1=1")
            strSQL.AppendLine(" and N.CompID='" + UserProfile.SelectCompRoleID + "'")
            If (DateStrF(ucBeginDate.DateText) <> "") Then
                strSQL.AppendLine(" And N.DisasterStartDate >= '" + ucBeginDate.DateText + "'")
            End If
            If (DateStrF(ucEndDate.DateText) <> "") Then
                strSQL.AppendLine(" And N.DisasterEndDate <= '" + ucEndDate.DateText + "'")
            End If
            If (ddlDisasterType.SelectedValue <> "") Then
                strSQL.AppendLine(" And N.DisasterType = '" + ddlDisasterType.SelectedValue.ToString + "'")
                'strSQL.AppendLine("and AT.TabName='NaturalDisasterByCity' and AT.FldName='DisasterType' and AT.NotShowFlag='0'") 20170204 mark by John
            End If
            If (ddlCityCode.SelectedValue <> "") Then
                strSQL.AppendLine(" And N.CityCode = '" + ddlCityCode.SelectedValue.ToString + "'")
            End If
            If (IsNumeric(txtEmpID.Text) And txtEmpID.Text <> "") Then
                strSQL.AppendLine(" And N.EmpID = '" + txtEmpID.Text + "'")
            End If
            If (ddlBranchName.SelectedValue <> "") Then
                strSQL.AppendLine(" And N.WorkSiteID = '" + ddlBranchName.SelectedValue.ToString + "'")
            End If

            CityTable.Visible = True
            Dim dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "AattendantDB").Tables(0)
            getAdjustTB(dt)
            pcMain.DataTable = dt
            gvMain.DataBind()
            gvMain.Visible = True
            PersonTable.Visible = False
            ViewState.Item("DoQuery") = "Y"
        ElseIf ddlPersonOrCity.SelectedValue = "PersonTable" Then  'PersonTable
            strSQL.AppendLine("select N.EmpID+' '+P.NameN as EmpName,N.remark as Remark,CONVERT(varchar(10), N.DisasterStartDate,120)+'~'+CONVERT(varchar(10), N.DisasterEndDate,120) As DisasterDate,LEFT(N.BeginTime,2)+':'+RIGHT(N.BeginTime,2) +'~'+  LEFT(N.EndTime,2)+':'+RIGHT(N.EndTime,2) AS DisasterTime ,AT.CodeCName as DisasterType ,AT.Code as ddlType,N.LastChgCompID,N.LastChgCompID as 'LastChgCompName',N.LastChgEmpID,N.LastChgEmpID as 'LastChgEmpName',format(N.LastChgDate,'yyyy/MM/dd HH:mm:ss') as 'LastChgDate'  ")
            strSQL.AppendLine("from NaturalDisasterByEmp N ")
            strSQL.AppendLine("left join " + eHRMSDB_ITRD + ".[dbo].Personal P on N.CompID = P.CompID and N.EmpID = P.EmpID ")
            strSQL.AppendLine("left join AT_CodeMap AT on N.DisasterType=AT.Code and AT.TabName='NaturalDisasterByCity' and AT.FldName='DisasterType'")
            strSQL.AppendLine("where 1=1 ")
            strSQL.AppendLine("and N.CompID='" + UserProfile.SelectCompRoleID + "'")
            If (DateStrF(ucBeginDate.DateText) <> "" And DateStrF(ucEndDate.DateText) <> "") Then
                strSQL.AppendLine(" And N.DisasterStartDate >= '" + ucBeginDate.DateText + "'")
                strSQL.AppendLine(" And N.DisasterEndDate <= '" + ucEndDate.DateText + "'")
            End If
            If (ddlDisasterType.SelectedValue <> "") Then
                strSQL.AppendLine(" And N.DisasterType = '" + ddlDisasterType.SelectedValue.ToString + "'")
                'strSQL.AppendLine("and AT.TabName='NaturalDisasterByCity' and AT.FldName='DisasterType' and AT.NotShowFlag='0'") 20170204 mark by John
            End If
            If (IsNumeric(txtEmpID.Text) And txtEmpID.Text <> "") Then
                strSQL.AppendLine(" And N.EmpID = '" + txtEmpID.Text + "'")
            End If
            PersonTable.Visible = True
            Dim dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "AattendantDB").Tables(0)
            getAdjustTB(dt)
            pcMain1.DataTable = dt
            gvMain1.DataBind()
            gvMain1.Visible = True
            CityTable.Visible = False
            ViewState.Item("DoQuery") = "Y"
        Else
            Bsp.Utility.ShowMessage(Me, "請先選取查詢條件")
            Return
        End If


    End Sub
    Protected Sub ChangeTypeClear() 'OK

        ddlCityCode.SelectedValue = ""
        ddlBranchName.SelectedValue = ""
        txtEmpID.Text = ""
        ucBeginDate.DateText = ""
        ucEndDate.DateText = ""
        ddlDisasterType.SelectedValue = ""

        ViewState.Item("DoQuery") = ""
        ViewState.Item("City_Emp") = ""
    End Sub
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

    Protected Sub DoClear() 'OK
        If ViewState.Item("City_Emp") = "CityTable" Then
            ddlCityCode.SelectedValue = ""
            ddlBranchName.SelectedValue = ""
        Else
            txtEmpID.Text = ""
        End If

        ddlPersonOrCity.SelectedValue = ""
        ucBeginDate.DateText = ""
        ucEndDate.DateText = ""
        ddlDisasterType.SelectedValue = ""

        trCity.Visible = False
        trEmp.Visible = False
        CityTable.Visible = False
        PersonTable.Visible = False
        ViewState.Item("DoQuery") = ""
        ViewState.Item("City_Emp") = ""
    End Sub
    Protected Sub ddlCityCode_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlCityCode.SelectedIndexChanged



        Dim objOM1 As New OM1()
        Dim strSQL As New StringBuilder()
        Dim CompID = UserProfile.SelectCompRoleID

        If ddlCityCode.SelectedValue = "" Then
            ddlBranchName.Items.Clear()
            Bsp.Utility.FillDDL(ddlBranchName, "eHRMSDB", "WorkSite", " convert(varchar,RTrim(WorkSiteID))", " Remark", Bsp.Utility.DisplayType.OnlyName, "", "and CompID=" + Bsp.Utility.Quote(CompID), "order by WorkSiteID")
            ddlBranchName.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        Else
            ddlBranchName.Items.Clear()
            Bsp.Utility.FillDDL(ddlBranchName, "eHRMSDB", "WorkSite", " convert(varchar,RTrim(WorkSiteID))", " Remark", Bsp.Utility.DisplayType.OnlyName, "", "and CompID= " + Bsp.Utility.Quote(CompID) + " and CityCode=" + ddlCityCode.SelectedValue.ToString, "order by WorkSiteID")
            ddlBranchName.Items.Insert(0, New ListItem("　- -請選擇- -", ""))


        End If
    End Sub
    Protected Sub ddlPersonOrCity_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlPersonOrCity.SelectedIndexChanged
        ChangeTypeClear()
        fieldSwitch()
        gvMain.DataSource = Nothing
        pcMain.DataBind()

        gvMain1.DataSource = Nothing
        pcMain1.DataBind()
    End Sub
    Public Sub fieldSwitch()
        Select Case ddlPersonOrCity.SelectedValue
            Case "CityTable"
                trCity.Visible = True
                trEmp.Visible = False
            Case "PersonTable"
                trCity.Visible = False
                trEmp.Visible = True
            Case Else
                trCity.Visible = False
                CityTable.Visible = False
                trEmp.Visible = False
                PersonTable.Visible = False
        End Select
    End Sub

    Public Overrides Sub DoModalReturn(ByVal returnValue As String)
        Dim strSql As String = ""

        If returnValue <> "" Then
            Dim aryData() As String = returnValue.Split(":")

            Select Case aryData(0)
                Case "ucQueryEmp"
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    txtEmpID.Text = aryValue(1)
            End Select
        End If
    End Sub

    Protected Sub gvMain1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvMain1.RowDataBound

        Select Case e.Row.RowType
            Case DataControlRowType.DataRow
                e.Row.Cells(3).ToolTip = e.Row.Cells(3).Text
                If e.Row.Cells(3).Text.Length >= 10 Then
                    e.Row.Cells(3).Text = e.Row.Cells(3).Text.Substring(0, 10)
                End If

        End Select
    End Sub



#Region "相關檢核"
    Public Function checkdate() As Boolean
        'If ucBeginDate.DateText <> "" Then
        '    Dim bDate As String = ucBeginDate.DateText.Replace("/", "")
        '    Dim eDate As String = ucEndDate.DateText.Replace("/", "")
        '    If bDate.Length <= 8 And eDate.Length <= 8 Then
        '        If IsNumeric(bDate) And IsNumeric(eDate) Then
        '            Dim begind As Integer = CInt(bDate)
        '            Dim endd As Integer = CInt(eDate)
        '            If begind > endd Then
        '                errorMsg = "起日不得大於迄日"
        '            End If
        '        Else
        '            errorMsg = "日期限定輸入數字"
        '        End If
        '    Else
        '        errorMsg = "請輸入正確日期"
        '    End If
        'End If

        If (DateStrF(ucBeginDate.DateText) <> "" And DateStrF(ucEndDate.DateText) <> "") Then
            If Not IsDate(ucBeginDate.DateText) Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00070", "起日")
                ucBeginDate.Focus()
                Return False
            ElseIf Not IsDate(ucEndDate.DateText) Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00070", "迄日")
                ucEndDate.Focus()
                Return False
            ElseIf CDate(ucEndDate.DateText) < CDate(ucBeginDate.DateText) Then
                Bsp.Utility.ShowFormatMessage(Me, "W_20720", "起日", "迄日")
                ucBeginDate.Focus()
                Return False
            End If
        ElseIf (DateStrF(ucBeginDate.DateText) <> "" And DateStrF(ucEndDate.DateText) = "") Then
            If Not IsDate(ucBeginDate.DateText) Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00070", "起日")
                ucBeginDate.Focus()
                Return False
            End If
        ElseIf (DateStrF(ucBeginDate.DateText) = "" And DateStrF(ucEndDate.DateText) <> "") Then
            If Not IsDate(ucEndDate.DateText) Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00070", "迄日")
                ucEndDate.Focus()
                Return False
            End If
        End If

        Return True
    End Function
#End Region

End Class
