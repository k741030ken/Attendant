'*********************************************************************************
'功能說明：留守人員設定新增
'建立人員： Eugerwu Kevin
'建立日期：2016.12.16
'*********************************************************************************

Imports System.Data
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Partial Class OV_OV3101
    Inherits PageBase
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then

        Else
            subReLoadColor(ddlOrgType)
            subReLoadColor(ddlDeptID)
        End If
    End Sub
    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            initScreen()

        End If
    End Sub
    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnAdd"       '新增
                If rdoListType.SelectedValue = "" Then
                    Bsp.Utility.ShowMessage(Me, "請選擇設定方式")
                    Return
                ElseIf rdoListType.SelectedValue = "0" And CheckData() Then

                    If checkSameDataForAdd("CityAdd") Then
                        CityAdd()
                    Else
                        Bsp.Utility.ShowMessage(Me, "在此日期時段已有申請")
                    End If


                ElseIf rdoListType.SelectedValue = "1" And CheckData() Then
                    If checkSameDataForAdd("EmpAdd") Then
                        EmpAdd()
                    ElseIf lstRight.Items.Count = 0 Then
                        Bsp.Utility.ShowMessage(Me, "請選取相關人員")
                    Else
                        Bsp.Utility.ShowMessage(Me, "在此日期時段已有申請")
                    End If

                Else
                    'Bsp.Utility.ShowMessage(Me, "資料錯誤請確認所填資料")
                    Return
                End If
            Case "btnCancel"   '清除
                DoClear()
            Case "btnActionX"     '返回
                GoBack(False)
        End Select
    End Sub
    Protected Sub initScreen()
        '元件初始



        Bsp.Utility.FillDDL(ddlCityCode, "eHRMSDB", "HRCodeMap", " convert(varchar,RTrim(Code))+'~'+CodeCName", " CodeCName", Bsp.Utility.DisplayType.OnlyName, "", "and TabName='WorkSite' and FldName='CityCode'and NotShowFlag='0'", "order by SortFld, Code")
        ddlCityCode.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        ' MsgBox(ddlCityCode.Text)

        'ddlBranchName.Items.Insert(0, New ListItem("　- -請先選擇縣市- -", ""))
        Bsp.Utility.FillDDL(ddlType, "AattendantDB", "AT_CodeMap", " convert(varchar,RTrim(Code))", " CodeCName", Bsp.Utility.DisplayType.OnlyName, "", "and TabName='NaturalDisasterByCity' and FldName='DisasterType' and NotShowFlag='0'", "order by SortFld, Code")
        ddlType.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        ucBeginDate.DateText = Now
        ucEndDate.DateText = Now
        StartTimeH.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        StartTimeM.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        EndTimeH.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        EndTimeM.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        For Hr As Integer = 0 To 23 Step 1
            StartTimeH.Items.Insert(Hr + 1, New ListItem(IIf(Hr < 10, "0" + CStr(Hr), CStr(Hr)), IIf(Hr < 10, "0" + CStr(Hr), CStr(Hr))))
            EndTimeH.Items.Insert(Hr + 1, New ListItem(IIf(Hr < 10, "0" + CStr(Hr), CStr(Hr)), IIf(Hr < 10, "0" + CStr(Hr), CStr(Hr))))
        Next
        For Mt As Integer = 0 To 59 Step 1
            StartTimeM.Items.Insert(Mt + 1, New ListItem(IIf(Mt < 10, "0" + CStr(Mt), CStr(Mt)), IIf(Mt < 10, "0" + CStr(Mt), CStr(Mt))))
            EndTimeM.Items.Insert(Mt + 1, New ListItem(IIf(Mt < 10, "0" + CStr(Mt), CStr(Mt)), IIf(Mt < 10, "0" + CStr(Mt), CStr(Mt))))
        Next
    End Sub
#Region "下拉選單"
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
    '部門
    Protected Sub ddlDept_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDeptID.SelectedIndexChanged
        If ddlDeptID.SelectedValue = "" Then
            Bsp.Utility.FillDDL(ddlOrganID, "eHRMSDB", "Organization", "RTRIM(OrganID)", "OrganName", Bsp.Utility.DisplayType.Full, "", "And InValidFlag = '0' And VirtualFlag = '0' And CompID = " & Bsp.Utility.Quote(UserProfile.SelectCompRoleID) & " AND ((OrganID = OrganID AND OrganID <> DeptID AND OrganID <> OrgType)OR(OrganID = OrganID AND OrganID = DeptID AND OrganID = OrgType)) ", "Order By OrganID")
            ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
        Else
            Bsp.Utility.FillDDL(ddlOrganID, "eHRMSDB", "Organization", "RTRIM(OrganID)", "OrganName", Bsp.Utility.DisplayType.Full, "", "And InValidFlag = '0' And VirtualFlag = '0' And CompID = " & Bsp.Utility.Quote(UserProfile.SelectCompRoleID) & " And DeptID = " & Bsp.Utility.Quote(ddlDeptID.SelectedValue) & " AND ((OrganID = OrganID AND OrganID <> DeptID AND OrganID <> OrgType)OR(OrganID = OrganID AND OrganID = DeptID AND OrganID = OrgType)) ", "Order By OrganID")
            ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
        End If
    End Sub
#End Region

    Private Function CheckData() As Boolean

        Dim IsCheckData As Boolean = False


        If rdoListType.SelectedValue = "1" Then
            If txtRemark.Text.Length > 200 Then
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", "說明字數不得大於200")
                txtRemark.Focus()
                Return False
            Else
                IsCheckData = True
            End If


        ElseIf rdoListType.SelectedValue = "0" Then

            If ddlCityCode.SelectedValue = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", "您尚未選取縣市")
                Return False
            End If


            For intRow As Integer = 0 To gvMain1.Rows.Count - 1
                Dim objChk As CheckBox = gvMain1.Rows(intRow).FindControl("chk_gvMain")
                If objChk.Checked Then
                    IsCheckData = True
                End If
            Next

            If Not IsCheckData Then
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", "您尚未選取工作地點")
                Return False
            End If
        End If

        If DateStrF(ucBeginDate.DateText) = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "H_00000", "您尚未選取留守日期")
            Return False
        End If
        If DateStrF(ucEndDate.DateText) = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "H_00000", "您尚未選取留守日期")
            Return False
        End If

        If StartTimeH.SelectedValue = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "H_00000", "您尚未選取留守開始時間")
            Return False
        End If
        If StartTimeM.SelectedValue = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "H_00000", "您尚未選取留守開始時間")
            Return False
        End If


        If EndTimeH.SelectedValue = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "H_00000", "您尚未選取留守結束時間")
            Return False
        End If
        If EndTimeM.SelectedValue = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "H_00000", "您尚未選取留守結束時間")
            Return False
        End If
        If ddlType.SelectedValue = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "H_00000", "您尚未選取留守類型")
            Return False
        End If

        If "".Equals(checktime()) Then

        Else
            IsCheckData = False
            Bsp.Utility.ShowFormatMessage(Me, "H_00000", checktime())
        End If

        If "".Equals(checkdate()) Then

        Else
            IsCheckData = False
            Bsp.Utility.ShowFormatMessage(Me, "H_00000", checkdate())
        End If

        Return IsCheckData
    End Function

    Protected Sub ddlCityCode_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlCityCode.SelectedIndexChanged
        'MsgBox(ddlCityCode.SelectedValue)
        Dim strSQL As New StringBuilder()

        Dim CompID As String = UserProfile.SelectCompRoleID

        If ddlCityCode.SelectedValue.ToString <> "" Then
            strSQL.AppendLine("SELECT RTrim(WorkSiteID) as WorkSiteID,Remark ")
            strSQL.AppendLine("FROM WorkSite ")
            strSQL.AppendLine("where CompID=" + Bsp.Utility.Quote(CompID))
            strSQL.AppendLine("and CityCode=" + ((ddlCityCode.SelectedValue.ToString).Split("~"))(0))
            strSQL.AppendLine("order by WorkSiteID")
            Dim dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)

            gvMain1.DataSource = dt
            gvMain1.DataBind()
            gvMain1.Visible = True
            If IsNothing(dt) Or dt.Rows.Count = 0 Then
                gvMain1Hidden.Visible = True
            Else
                gvMain1Hidden.Visible = False
            End If
        End If
        If "".Equals(ddlCityCode.SelectedValue) Then
            gvMain1.DataSource = Nothing
            gvMain1.DataBind()
        End If

    End Sub

    Protected Sub ChangeTypeClear()
        ddlType.SelectedValue = ""
        trCity.Visible = False
        trPerson.Visible = False
        ucBeginDate.DateText = Now
        ucEndDate.DateText = Now
        StartTimeH.SelectedIndex = 0
        StartTimeM.SelectedIndex = 0
        EndTimeH.SelectedIndex = 0
        EndTimeM.SelectedIndex = 0

        If "0".Equals(rdoListType.SelectedValue) Then
            ddlCityCode.SelectedIndex = 0
            gvMain1.DataSource = Nothing
            gvMain1.DataBind()
        ElseIf "1".Equals(rdoListType.SelectedValue) Then
            lstRight.Items.Clear()
            lstLeft.Items.Clear()
            ddlOrgType.Items.Clear()
            ddlDeptID.Items.Clear()
            ddlOrganID.Items.Clear()
        End If


    End Sub
    Protected Sub DoClear()
        rdoListType.ClearSelection()
        ddlType.SelectedValue = ""
        trCity.Visible = False
        trPerson.Visible = False
        ucBeginDate.DateText = Now
        ucEndDate.DateText = Now
        StartTimeH.SelectedIndex = 0
        StartTimeM.SelectedIndex = 0
        EndTimeH.SelectedIndex = 0
        EndTimeM.SelectedIndex = 0


    End Sub

    'Private Sub GoBack()
    '    Dim ti As TransferInfo = Me.StateTransfer
    '    Me.TransferFramePage(ti.CallerUrl, Nothing, ti.Args)
    'End Sub
    Private Sub GoBack(ByRef Flag As Boolean) '20170204 Update By John
        Dim ti As TransferInfo = Me.StateTransfer

        'If Flag Then
        '    Dim clearValue As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)
        '    clearValue("DoQuery") = ""
        '    Me.TransferFramePage(ti.CallerUrl, Nothing, clearValue)
        'Else
        Me.TransferFramePage(ti.CallerUrl, Nothing, ti.Args)
        'End If
    End Sub

    Protected Sub rdoListType_CheckedChanged(sender As Object, e As System.EventArgs) Handles rdoListType.SelectedIndexChanged
        ChangeTypeClear()
        Select Case rdoListType.SelectedValue
            Case "0"
                trCity.Visible = True
                trPerson.Visible = False
            Case "1"
                trCity.Visible = False
                trPerson.Visible = True
                txtEmpID.Text = ""
                txtEmpName.Text = ""
                ddlOrgType.Items.Insert(0, New ListItem("---請選擇---", ""))
                ddlDeptID.Items.Insert(0, New ListItem("---請選擇---", ""))
                ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
                initArgs()
                ddlCompID_Query()
            Case Else
                trCity.Visible = False
                trPerson.Visible = False
        End Select
    End Sub
    Public Sub initArgs()
        '員工編號
        ucQueryEmp.ShowCompRole = "False"
        ucQueryEmp.InValidFlag = "N"
        ucQueryEmp.SelectCompID = UserProfile.SelectCompRoleID

    End Sub
    Public Overrides Sub DoModalReturn(ByVal returnValue As String)
        Dim strSql As String = ""

        If returnValue <> "" Then
            Dim aryData() As String = returnValue.Split(":")

            Select Case aryData(0)
                Case "ucQueryEmp"
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    txtEmpID.Text = aryValue(1)
                    txtEmpName.Text = aryValue(2)

                    If checksameDataForEmp("") Then
                        lstLeft.Items.Insert(CStr(lstLeft.Items.Count), (txtEmpID.Text + " " + txtEmpName.Text).ToString())
                    Else
                        Bsp.Utility.ShowMessage(Me, "該人員已在選單中")
                    End If
            End Select
        End If
    End Sub

    Protected Sub ddlCompID_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlCompID.SelectedIndexChanged
        Dim CompID As String = ddlCompID.SelectedValue
        ViewState.Item("OrgTypeColors") = New List(Of ArrayList)()
        ViewState.Item("DeptColors") = New List(Of ArrayList)()

        subLoadOrganColor(ddlOrgType, CompID)
        subLoadOrganColor(ddlDeptID, CompID)

        '科組課
        ddlDept_Changed(Nothing, Nothing)
    End Sub

    Public Sub ddlCompID_Query()
        Dim strSQL As New StringBuilder()


        strSQL.AppendLine("select CompID,CompID +'-'+ CompName As CompName From Company")
        Dim dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
        ddlCompID.DataSource = dt

        ddlCompID.DataTextField = "CompName"
        ddlCompID.DataValueField = "CompID"
        ddlCompID.DataBind()

        ddlCompID.SelectedValue = UserProfile.SelectCompRoleID

        Dim CompID As String = ddlCompID.SelectedValue
        ViewState.Item("OrgTypeColors") = New List(Of ArrayList)()
        ViewState.Item("DeptColors") = New List(Of ArrayList)()

        subLoadOrganColor(ddlOrgType, CompID)
        subLoadOrganColor(ddlDeptID, CompID)

        '科組課
        ddlDept_Changed(Nothing, Nothing)
    End Sub

    Protected Sub btnEmpID_Click(sender As Object, e As System.EventArgs) Handles btnEmpID.Click
        'If btnchkQuery() <> "" Then
        '    Bsp.Utility.ShowMessage(Me, btnchkQuery())
        '    Return
        'End If
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine("select EmpID,EmpID+' '+NameN As EmpName From Personal P")

        strSQL.AppendLine(" left join Organization ORT ON  P.OrganID=ORT.OrganID AND P.CompID =ORT.CompID  ")
        strSQL.AppendLine("where 1=1 ")
        If ddlOrgType.SelectedValue <> "" Then
            strSQL.AppendLine("and ORT.OrgType ='" & ddlOrgType.SelectedValue & "'")
        End If
        If ddlDeptID.SelectedValue <> "" Then
            strSQL.AppendLine("and P.DeptID ='" & ddlDeptID.SelectedValue & "'")
        End If
        If ddlOrganID.SelectedValue <> "" Then
            strSQL.AppendLine("and ORT.OrganID ='" & ddlOrganID.SelectedValue & "'")
        End If
        If txtEmpID.Text <> "" Then
            strSQL.AppendLine("and P.EmpID='" & txtEmpID.Text & "'")
        End If
        Dim dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
        lstLeft.DataSource = dt
        lstLeft.DataTextField = "EmpName"
        lstLeft.DataValueField = "EmpID"
        lstLeft.DataBind()


    End Sub

    Protected Sub btnMoveRight_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnMoveRight.Click
        If lstLeft.SelectedIndex < 0 Then
            Bsp.Utility.ShowMessage(Me, "請選取相關人員")
            Return
        End If

        Dim a As Integer = lstLeft.SelectedIndex
        Dim b = lstLeft.SelectedItem
        Dim ind As Integer = lstRight.Items.Count

        If checksameDataForEmp("lstRight") Then
            '1.左邊刪除
            lstLeft.Items.RemoveAt(a)
            '2.右邊增加
            lstRight.Items.Insert(CStr(ind), b)
        Else
            Bsp.Utility.ShowMessage(Me, "該人員已在選單中")
        End If




    End Sub

    Protected Sub btnMoveLeft_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnMoveLeft.Click
        If lstRight.SelectedIndex < 0 Then
            Bsp.Utility.ShowMessage(Me, "請選取相關人員")
            Return
        End If

        Dim a As Integer = lstRight.SelectedIndex
        Dim b = lstRight.SelectedItem
        Dim ind As Integer = lstLeft.Items.Count

        '1.右邊刪除
        lstRight.Items.RemoveAt(a)
        '2.左邊增加
        lstLeft.Items.Insert(CStr(ind), b)


    End Sub
    Public Sub CityAdd()
        Try
            Dim Flag As Boolean = True
            Dim strSQL As New StringBuilder()

            Dim CompID As String = UserProfile.SelectCompRoleID

            For intRow As Integer = 0 To gvMain1.Rows.Count - 1
                Dim objChk As CheckBox = gvMain1.Rows(intRow).FindControl("chk_gvMain")
                If objChk.Checked Then
                    strSQL.AppendLine("INSERT INTO [dbo].[NaturalDisasterByCity]")
                    strSQL.AppendLine("([CompID],[DisasterStartDate],[DisasterEndDate],[BeginTime],[EndTime],[CityCode],[CityName],[WorkSiteID],[WorkSiteName],[DisasterType],[LastChgCompID],[LastChgEmpID],[LastChgDate])")
                    strSQL.AppendLine("VALUES(")
                    strSQL.AppendLine(Bsp.Utility.Quote(CompID) + ",")
                    strSQL.AppendLine(Bsp.Utility.Quote(ucBeginDate.DateText) + ",")
                    strSQL.AppendLine(Bsp.Utility.Quote(ucEndDate.DateText) + ",")
                    strSQL.AppendLine(Bsp.Utility.Quote(StartTimeH.SelectedValue & StartTimeM.SelectedValue) + ",")
                    strSQL.AppendLine(Bsp.Utility.Quote(EndTimeH.SelectedValue & EndTimeM.SelectedValue) + ",")
                    ' gvMain1.DataKeys(intRow).Values["WorkSiteID "]
                    'gvMain1.DataKeys(selectedRow(gvMain))("OTCompID")

                    strSQL.AppendLine(Bsp.Utility.Quote((ddlCityCode.SelectedValue.ToString.Split("~"))(0)) + ",")
                    strSQL.AppendLine(Bsp.Utility.Quote((ddlCityCode.SelectedValue.ToString.Split("~"))(1)) + ",")
                    strSQL.AppendLine(Bsp.Utility.Quote((gvMain1.DataKeys(intRow)("WorkSiteID")).ToString) + ",")
                    strSQL.AppendLine(Bsp.Utility.Quote((gvMain1.DataKeys(intRow)("Remark")).ToString) + ",")
                    strSQL.AppendLine(Bsp.Utility.Quote(ddlType.SelectedValue) + ",")
                    strSQL.AppendLine(Bsp.Utility.Quote(UserProfile.ActCompID) & ",")
                    strSQL.AppendLine(Bsp.Utility.Quote(UserProfile.ActUserID) & ",")
                    strSQL.AppendLine("getdate())")
                End If
            Next

            Using cn As DbConnection = Bsp.DB.getConnection("AattendantDB")
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction
                Try
                    Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran, "AattendantDB")
                    tran.Commit()
                Catch ex As Exception
                    tran.Rollback()
                    'MsgBox(ex.Message)
                    Bsp.Utility.ShowMessage(Me, "新增失敗")
                    Flag = False
                    Throw
                Finally
                    If tran IsNot Nothing Then tran.Dispose()
                End Try
            End Using

            If Flag Then
                Bsp.Utility.ShowMessage(Me, "新增成功")
                GoBack(False)
            End If
        Catch ex As Exception


        End Try
    End Sub
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



    Public Sub EmpAdd()
        Try
            If lstRight.Items.Count = 0 Then
                Bsp.Utility.ShowMessage(Me, "請選取相關人員")
                Return
            End If
            If checkdate() <> "" Then
                Bsp.Utility.ShowMessage(Me, checkdate())
                Return
            Else
                If checktime() <> "" Then
                    Bsp.Utility.ShowMessage(Me, checktime())
                    Return
                Else
                    If ddlType.SelectedValue = "" Then
                        Bsp.Utility.ShowMessage(Me, "請選擇留守類型")
                        Return
                    End If
                End If
            End If
            Dim Flag As Boolean = True
            For Each Emp In lstRight.Items
                Dim strSQL As New StringBuilder()
                Dim EmpData() As String = Emp.ToString.Split(" ")
                strSQL.AppendLine("Insert into NaturalDisasterByEmp(CompID,DisasterStartDate,DisasterEndDate,BeginTime,EndTime,EmpID,remark,DisasterType,LastChgCompID,LastChgEmpID,LastChgDate) ")
                strSQL.AppendLine("Values('" & ddlCompID.SelectedValue & "',")
                strSQL.AppendLine(Bsp.Utility.Quote(ucBeginDate.DateText) & ",")
                strSQL.AppendLine(Bsp.Utility.Quote(ucEndDate.DateText) & ",")
                strSQL.AppendLine(Bsp.Utility.Quote(StartTimeH.SelectedValue & StartTimeM.SelectedValue) & ",")
                strSQL.AppendLine(Bsp.Utility.Quote(EndTimeH.SelectedValue & EndTimeM.SelectedValue) & ",")
                strSQL.AppendLine(Bsp.Utility.Quote(EmpData(0)) & ",")
                strSQL.AppendLine(Bsp.Utility.Quote(txtRemark.Text) & ",")
                strSQL.AppendLine(Bsp.Utility.Quote(ddlType.SelectedValue) & ",")
                strSQL.AppendLine(Bsp.Utility.Quote(UserProfile.ActCompID) & ",")
                strSQL.AppendLine(Bsp.Utility.Quote(UserProfile.ActUserID) & ",")
                strSQL.AppendLine("getdate())")
                Using cn As DbConnection = Bsp.DB.getConnection("AattendantDB")
                    cn.Open()
                    Dim tran As DbTransaction = cn.BeginTransaction
                    Try
                        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran, "AattendantDB")
                        tran.Commit()
                    Catch ex As Exception
                        tran.Rollback()
                        'MsgBox(ex.Message)
                        Bsp.Utility.ShowMessage(Me, "新增失敗")
                        Flag = False
                        Throw
                    Finally
                        If tran IsNot Nothing Then tran.Dispose()
                    End Try
                End Using
            Next

            If Flag Then
                Bsp.Utility.ShowMessage(Me, "新增成功")
                GoBack(False)
            End If
        Catch ex As Exception

        End Try
    End Sub
#Region "相關檢核"
    Public Function checkdate() As String
        Dim errorMsg As String = ""
        Dim bDate As String = ucBeginDate.DateText.Replace("/", "")
        Dim eDate As String = ucEndDate.DateText.Replace("/", "")
        If bDate.Length <= 8 And eDate.Length <= 8 Then
            If IsNumeric(bDate) And IsNumeric(eDate) Then
                Dim begind As Integer = CInt(bDate)
                Dim endd As Integer = CInt(eDate)
                If begind > endd Then
                    errorMsg = "起日不得大於迄日"
                End If
            Else
                errorMsg = "日期限定輸入數字"
            End If
        Else
            errorMsg = "請輸入正確日期"
        End If


        Return errorMsg
    End Function



    Public Function checktime() As String
        Dim errorMsg As String = ""
        If checkdate() = "" Then
            Dim startT As Integer = CInt(StartTimeH.SelectedValue & StartTimeM.SelectedValue)
            Dim endT As Integer = CInt(EndTimeH.SelectedValue & EndTimeM.SelectedValue)
            If (Not "".Equals(ucBeginDate.DateText)) And ((ucBeginDate.DateText).Equals(ucEndDate.DateText)) Then
                If startT = endT Then
                    errorMsg = "同日期不能同時間"
                End If
            End If
            If IsNumeric(startT) And IsNumeric(endT) Then
                Dim bDate As Integer = CInt(ucBeginDate.DateText.Replace("/", ""))
                Dim eDate As Integer = CInt(ucEndDate.DateText.Replace("/", ""))
                If bDate = eDate Then
                    If startT > endT Then
                        errorMsg = "開始時間不得大於結束時間"
                    End If
                End If
            Else
                errorMsg = "限定輸入數字"
            End If
        End If

        Return errorMsg
    End Function

    Public Function btnchkQuery() As String
        Dim errorMsg = ""
        If ddlOrgType.SelectedValue = "" Then
            errorMsg = "請選擇相關單位"
        End If
        Return errorMsg
    End Function


    Public Function checkSameDataForAdd(ByVal addType As String) As Boolean

        Dim strSQL As New StringBuilder()

        If ("CityAdd".Equals(addType)) Then
            Dim WorkSiteIDArrayList As ArrayList = New ArrayList()
            Dim CityCode As String = ddlCityCode.SelectedValue
            Dim DisasterStartDateAft As String = "'" & ucBeginDate.DateText & "'"
            Dim DisasterEndDateAft As String = "'" & ucEndDate.DateText & "'"
            Dim BeginTimeAft As String = "'" & StartTimeH.SelectedValue + StartTimeM.SelectedValue & "'"
            Dim EndTimeAft As String = "'" & EndTimeH.SelectedValue + EndTimeM.SelectedValue & "'"
            Dim ddlTypeaf As String = "'" & ddlType.SelectedValue & "'"

            For intRow As Integer = 0 To gvMain1.Rows.Count - 1
                Dim objChk As CheckBox = gvMain1.Rows(intRow).FindControl("chk_gvMain")
                If objChk.Checked Then
                    WorkSiteIDArrayList.Add(Bsp.Utility.Quote((gvMain1.DataKeys(intRow)("WorkSiteID")).ToString))
                End If
            Next


            strSQL.Append("select CityCode,BeginTime,EndTime")
            strSQL.Append(" FROM NaturalDisasterByCity ")
            strSQL.Append(" where '1'='1'")
            'strSQL.Append(" and DisasterType=" + ddlTypeaf + "")'不同類型時段不可重複

            If WorkSiteIDArrayList.Count > 1 Then
                strSQL.Append("and (")
                For i As Integer = 0 To WorkSiteIDArrayList.Count - 2
                    strSQL.Append("  WorkSiteID=" + CType(WorkSiteIDArrayList.Item(i), String) + "OR")
                Next
                strSQL.Append("  WorkSiteID=" + CType(WorkSiteIDArrayList.Item(WorkSiteIDArrayList.Count - 1), String) + "")
                strSQL.Append(")")
            Else
                strSQL.Append(" and WorkSiteID=" + CType(WorkSiteIDArrayList.Item(WorkSiteIDArrayList.Count - 1), String) + "")
            End If

            'convert(datetime, ('2017-01-13' + ' ' + SUBSTRING('1550',0,3) + ':' + SUBSTRING('1550',3,5)))

            ''開始日期
            'strSQL.Append(" convert(datetime, (" + DisasterStartDateAft + " + ' ' + SUBSTRING(" + BeginTimeAft + ",0,3) + ':' + SUBSTRING(" + BeginTimeAft + ",3,5)))")
            ''結束日期
            'strSQL.Append(" convert(datetime, (" + DisasterEndDateAft + " + ' ' + SUBSTRING(" + EndTimeAft + "',0,3) + ':' + SUBSTRING(" + EndTimeAft + ",3,5)))")
            ''開始日期 DB
            'strSQL.Append(" convert(datetime, ( DisasterStartDate + ' ' + SUBSTRING(BeginTime +,0,3) + ':' + SUBSTRING(BeginTime,3,5)))")
            ''結束日期 DB
            'strSQL.Append(" convert(datetime, ( DisasterEndDate + ' ' + SUBSTRING(EndTime +,0,3) + ':' + SUBSTRING(EndTime,3,5)))")

            'Convert(varchar, DisasterStartDate)
            'Convert(varchar, DisasterEndDate)
            '開始日期  Between  開始日期 DB AND 結束日期 DB
            strSQL.Append(" and " + " ((convert(datetime, (" + DisasterStartDateAft + " + ' ' + SUBSTRING(" + BeginTimeAft + ",0,3) + ':' + SUBSTRING(" + BeginTimeAft + ",3,5)))")
            strSQL.Append(" between " + " convert(datetime, ( convert(varchar,DisasterStartDate) + ' ' + SUBSTRING(BeginTime ,0,3) + ':' + SUBSTRING(BeginTime,3,5)))")
            strSQL.Append(" and convert(datetime, (  Convert(varchar, DisasterEndDate) + ' ' + SUBSTRING(EndTime ,0,3) + ':' + SUBSTRING(EndTime,3,5))))")

            '結束日期  Between  開始日期 DB AND 結束日期 DB
            strSQL.Append(" or " + " ( convert(datetime, (" + DisasterEndDateAft + " + ' ' + SUBSTRING(" + EndTimeAft + ",0,3) + ':' + SUBSTRING(" + EndTimeAft + ",3,5)))")
            strSQL.Append(" between " + " convert(datetime, ( Convert(varchar, DisasterStartDate) + ' ' + SUBSTRING(BeginTime ,0,3) + ':' + SUBSTRING(BeginTime,3,5)))")
            strSQL.Append(" and convert(datetime, (  Convert(varchar, DisasterEndDate) + ' ' + SUBSTRING(EndTime ,0,3) + ':' + SUBSTRING(EndTime,3,5))))")

            '開始日期  <=開始日期 DB and 結束日期 <= 結束日期 DB
            strSQL.Append(" or " + " ((convert(datetime, (" + DisasterStartDateAft + " + ' ' + SUBSTRING(" + BeginTimeAft + ",0,3) + ':' + SUBSTRING(" + BeginTimeAft + ",3,5)))")
            strSQL.Append(" <= " + " convert(datetime, ( Convert(varchar, DisasterStartDate) + ' ' + SUBSTRING(BeginTime ,0,3) + ':' + SUBSTRING(BeginTime,3,5))))")
            strSQL.Append(" and " + "(" + " convert(datetime, ( Convert(varchar, DisasterEndDate) + ' ' + SUBSTRING(EndTime ,0,3) + ':' + SUBSTRING(EndTime,3,5)))")
            strSQL.Append(" <= " + " convert(datetime, (" + DisasterEndDateAft + " + ' ' + SUBSTRING(" + EndTimeAft + ",0,3) + ':' + SUBSTRING(" + EndTimeAft + ",3,5))))))")


            Dim dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "AattendantDB").Tables(0)
            If dt.Rows.Count > 0 Then
                Return False
            End If
            Return True
        Else

            If lstRight.Items.Count = 0 Then
                'Bsp.Utility.ShowMessage(Me, "請選取相關人員")
                Return False
            End If


            Dim EmpIDArrayList As ArrayList = New ArrayList()
            'Dim CityCode As String = ddlCityCode.SelectedValue
            Dim DisasterStartDateAft As String = "'" & ucBeginDate.DateText & "'"
            Dim DisasterEndDateAft As String = "'" & ucEndDate.DateText & "'"
            Dim BeginTimeAft As String = "'" & StartTimeH.SelectedValue + StartTimeM.SelectedValue & "'"
            Dim EndTimeAft As String = "'" & EndTimeH.SelectedValue + EndTimeM.SelectedValue & "'"
            Dim ddlTypeaf As String = "'" & ddlType.SelectedValue & "'"



            For Each Emp In lstRight.Items
                Dim EmpData() As String = Emp.ToString.Split(" ")
                EmpIDArrayList.Add(Bsp.Utility.Quote(EmpData(0)))
            Next

            strSQL.Append("select BeginTime,EndTime")
            strSQL.Append(" FROM NaturalDisasterByEmp ")
            strSQL.Append(" where '1'='1'")


            If EmpIDArrayList.Count > 1 Then
                strSQL.Append("and (")
                For i As Integer = 0 To EmpIDArrayList.Count - 2
                    strSQL.Append("  EmpID=" + CType(EmpIDArrayList.Item(i), String) + "OR")
                Next
                strSQL.Append("  EmpID=" + CType(EmpIDArrayList.Item(EmpIDArrayList.Count - 1), String) + "")
                strSQL.Append(")")
            Else


                strSQL.Append((" and EmpID=" + CType(EmpIDArrayList.Item(EmpIDArrayList.Count - 1), String) + ""))
            End If

            '開始日期  Between  開始日期 DB AND 結束日期 DB
            strSQL.Append(" and " + " ((convert(datetime, (" + DisasterStartDateAft + " + ' ' + SUBSTRING(" + BeginTimeAft + ",0,3) + ':' + SUBSTRING(" + BeginTimeAft + ",3,5)))")
            strSQL.Append(" between " + " convert(datetime, ( convert(varchar,DisasterStartDate) + ' ' + SUBSTRING(BeginTime ,0,3) + ':' + SUBSTRING(BeginTime,3,5)))")
            strSQL.Append(" and convert(datetime, (  Convert(varchar, DisasterEndDate) + ' ' + SUBSTRING(EndTime ,0,3) + ':' + SUBSTRING(EndTime,3,5))))")

            '結束日期  Between  開始日期 DB AND 結束日期 DB
            strSQL.Append(" or " + " ( convert(datetime, (" + DisasterEndDateAft + " + ' ' + SUBSTRING(" + EndTimeAft + ",0,3) + ':' + SUBSTRING(" + EndTimeAft + ",3,5)))")
            strSQL.Append(" between " + " convert(datetime, ( Convert(varchar, DisasterStartDate) + ' ' + SUBSTRING(BeginTime ,0,3) + ':' + SUBSTRING(BeginTime,3,5)))")
            strSQL.Append(" and convert(datetime, (  Convert(varchar, DisasterEndDate) + ' ' + SUBSTRING(EndTime ,0,3) + ':' + SUBSTRING(EndTime,3,5))))")

            '開始日期  <=開始日期 DB and 結束日期 <= 結束日期 DB
            strSQL.Append(" or " + " ((convert(datetime, (" + DisasterStartDateAft + " + ' ' + SUBSTRING(" + BeginTimeAft + ",0,3) + ':' + SUBSTRING(" + BeginTimeAft + ",3,5)))")
            strSQL.Append(" <= " + " convert(datetime, ( Convert(varchar, DisasterStartDate) + ' ' + SUBSTRING(BeginTime ,0,3) + ':' + SUBSTRING(BeginTime,3,5))))")
            strSQL.Append(" and " + "(" + " convert(datetime, ( Convert(varchar, DisasterEndDate) + ' ' + SUBSTRING(EndTime ,0,3) + ':' + SUBSTRING(EndTime,3,5)))")
            strSQL.Append(" <= " + " convert(datetime, (" + DisasterEndDateAft + " + ' ' + SUBSTRING(" + EndTimeAft + ",0,3) + ':' + SUBSTRING(" + EndTimeAft + ",3,5))))))")
            Dim dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "AattendantDB").Tables(0)
            If dt.Rows.Count > 0 Then
                Return False
            End If

            Return True
        End If
    End Function
#End Region



    Protected Sub txtEmpID_TextChanged(sender As Object, e As System.EventArgs) Handles txtEmpID.TextChanged
        'txtEmpID.Attributes.Add("onfocusout", "if(this.value==''){alert('出错');this.focus();}")
        Dim CompID As String = UserProfile.SelectCompRoleID

        If Not "".Equals(txtEmpID.Text) Then
            Dim strSQL As New StringBuilder()
            strSQL.Append("Select EmpID,EmpID+' '+NameN As EmpName ,[Name] FROM [Personal]")


            strSQL.Append("where [EmpID]='" + txtEmpID.Text + "'and[CompID]=" + Bsp.Utility.Quote(CompID))

            Dim dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
            If dt.Rows.Count > 0 Then
                For Each item As DataRow In dt.Rows
                    txtEmpName.Text = item("Name").ToString()

                    If checksameDataForEmp("") Then
                        lstLeft.Items.Insert(CStr(lstLeft.Items.Count), item("EmpName").ToString())
                    Else
                        Bsp.Utility.ShowMessage(Me, "該人員已在選單中")
                    End If

                Next
            Else
                txtEmpName.Text = ""

            End If
        End If
    End Sub

    Private Function checksameDataForEmp(ByVal checkType As String) As Boolean
        Dim empDataID As String = ""
        Dim lstLeftID As String = ""
        If checkType.Equals("lstRight") Then
            lstLeftID = (lstLeft.Items.Item(lstLeft.SelectedIndex).Text.ToString.Split(" "))(0)
            For i = 0 To lstRight.Items.Count - 1
                empDataID = (lstRight.Items.Item(i).Text.ToString.Split(" "))(0)
                If (lstLeftID).Equals(empDataID) Then
                    Return False
                End If
            Next

        ElseIf checkType.Equals("lstLeft") Then
            For i = 0 To lstLeft.Items.Count - 1
                empDataID = (lstLeft.Items.Item(i).Text.ToString.Split(" "))(0)
                If (txtEmpID.Text.ToString).Equals(empDataID) Then
                    Return False
                End If
            Next

        Else

            For i = 0 To lstLeft.Items.Count - 1
                empDataID = (lstLeft.Items.Item(i).Text.ToString.Split(" "))(0)
                If (txtEmpID.Text.ToString).Equals(empDataID) Then
                    Return False
                End If
            Next

            For i = 0 To lstRight.Items.Count - 1
                empDataID = (lstRight.Items.Item(i).Text.ToString.Split(" "))(0)
                If (txtEmpID.Text.ToString).Equals(empDataID) Then
                    Return False
                End If
            Next
        End If
        Return True
    End Function

    Protected Sub ddlOrgType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlOrgType.SelectedIndexChanged
        If Not ddlOrgType.SelectedValue = "" Then
            subLoadOrganColor(ddlDeptID, UserProfile.SelectCompRoleID, ddlOrgType.SelectedValue)
            Bsp.Utility.FillDDL(ddlOrganID, "eHRMSDB", "Organization", "RTRIM(OrganID)", "OrganName", Bsp.Utility.DisplayType.Full, "", "And InValidFlag = '0' And VirtualFlag = '0' And CompID = " & Bsp.Utility.Quote(UserProfile.SelectCompRoleID) & " And OrgType = " & Bsp.Utility.Quote(ddlOrgType.SelectedValue) & " AND ((OrganID = OrganID AND OrganID <> DeptID AND OrganID <> OrgType)OR(OrganID = OrganID AND OrganID = DeptID AND OrganID = OrgType)) ", "Order By OrganID")
            ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
        End If
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
End Class
