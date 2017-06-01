'*********************************************************************************
'功能說明：留守人員設定修改
'建立人員： Eugerwu Kevin
'建立日期：2016.12.16
'*********************************************************************************

Imports System.Data
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Partial Class OV_OV3102
    Inherits PageBase
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then

        End If
    End Sub
    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim passValue As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)
            Dim City_Emp = passValue("ddlPersonOrCity").ToString
            Dim BeginTime() As String = passValue("SBeginTime").ToString.Split(":")
            Dim EndTime() As String = passValue("SEndTime").ToString.Split(":")

            If City_Emp = "CityTable" Then
                ViewState.Item("CityCode") = passValue("SCityCode").ToString
                ViewState.Item("CityName") = passValue("SCityName").ToString
                ViewState.Item("BranchName") = passValue("SBranchName").ToString
                ViewState.Item("WorkSiteID") = passValue("SWorkSiteID").ToString
            ElseIf City_Emp = "PersonTable" Then
                ViewState.Item("EmpName") = passValue("SEmpName").ToString
                ViewState.Item("Remark") = passValue("SRemark").ToString
            End If
            ViewState.Item("ddlType") = passValue("ddlType").ToString
            ViewState.Item("BeginTimeH") = BeginTime(0)
            ViewState.Item("BeginTimeM") = BeginTime(1)
            ViewState.Item("EndTimeH") = EndTime(0)
            ViewState.Item("EndTimeM") = EndTime(1)
            ViewState.Item("BeginDate") = passValue("SBeginDate").ToString.Replace("-", "/")
            ViewState.Item("EndDate") = passValue("SEndDate").ToString.Replace("-", "/")
            ViewState.Item("DisasterType") = passValue("SDisasterType").ToString
            ViewState.Item("LastChgCompID") = passValue("LastChgCompID").ToString
            ViewState.Item("LastChgEmpID") = passValue("LastChgEmpID").ToString
            ViewState.Item("LastChgDate") = passValue("LastChgDate").ToString


            ViewState.Item("City_Emp") = City_Emp
            '畫面初始值
            ViewState.Item("init") = True
            initScreen(ViewState.Item("City_Emp"))
        End If
    End Sub
    Public Sub initScreen(ByVal str As String) '畫面初始值
        Dim objSC As New SC
        Dim OV_3 As OV_3 = New OV_3()
        '元件初始
        If ViewState.Item("init") Then
            initObject()
            ViewState.Item("init") = False
        End If
        '判斷人員/縣市
        If str = "CityTable" Then
            '欄位開關
            trCity.Visible = True
            trEmp.Visible = False
            remarkField.Visible = False

            lblCityName.Text = ViewState.Item("CityName").ToString
            lblBranchName.Text = ViewState.Item("BranchName").ToString
        ElseIf str = "PersonTable" Then
            '欄位開關
            trCity.Visible = False
            trEmp.Visible = True
            remarkField.Visible = True

            '畫面初始值
            lblEmpIDtxt.Text = ViewState.Item("EmpName").ToString
            txtRemark.Text = ViewState.Item("Remark")
        End If

        ucBeginFixDate.DateText = ViewState.Item("BeginDate").ToString
        ucEndFixDate.DateText = ViewState.Item("EndDate").ToString
        StartTimeH.SelectedValue = ViewState.Item("BeginTimeH")
        StartTimeM.SelectedValue = ViewState.Item("BeginTimeM")
        EndTimeH.SelectedValue = ViewState.Item("EndTimeH")
        EndTimeM.SelectedValue = ViewState.Item("EndTimeM")

        '最後異動公司
        Dim CompName As String = objSC.GetSC_CompName(ViewState.Item("LastChgCompID").ToString)
        lblLastChgComptxt.Text = ViewState.Item("LastChgCompID").ToString + IIf(CompName <> "", "-" + CompName, "")

        '最後異動人員
        Dim UserName As String = OV_3.GetPersonName(ViewState.Item("LastChgCompID").ToString, ViewState.Item("LastChgEmpID").ToString)
        lblLastChgIDtxt.Text = ViewState.Item("LastChgEmpID").ToString + IIf(UserName <> "", "-" + UserName, "")

        '最後異動日期
        lblLastChgDatetxt.Text = ViewState.Item("LastChgDate").ToString

        'Dim boolDate As Boolean = Format(ViewState.Item("LastChgDate").ToString, "yyyy/MM/dd") = "1900/01/01"
        'lblLastChgDate.Text = IIf(boolDate, "", ViewState.Item("LastChgDate").ToString("yyyy/MM/dd HH:mm:ss"))
        Bsp.Utility.FillDDL(ddlType, "AattendantDB", "AT_CodeMap", " convert(varchar,RTrim(Code))", " CodeCName", Bsp.Utility.DisplayType.OnlyName, "", "and TabName='NaturalDisasterByCity' and FldName='DisasterType' and NotShowFlag='0'", "order by SortFld, Code")


        ' MsgBox(ViewState.Item("DisasterType").ToString)
        ddlType.SelectedValue = ViewState.Item("ddlType").ToString
    End Sub
    Public Sub initObject()

        '元件初始
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
    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnAdd"    '存檔
                DoUpdate(ViewState.Item("City_Emp"))
            Case "btnCancel"   '清除
                'DoClear()
                initScreen(ViewState.Item("City_Emp"))
            Case "btnActionX"     '返回
                GoBack(False)
        End Select
    End Sub
    Protected Sub DoUpdate(ByVal str As String)
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

        Dim strCompID As String
        Dim strChgEmpID As String
        Dim updateCount As Integer
        Dim dbcmd As DbCommand

        strCompID = UserProfile.ActCompID
        strChgEmpID = UserProfile.ActUserID


        If checkSameData(str) Then
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim Flag As Boolean = False
            If str = "CityTable" Then
                strSQL.AppendLine("update  NaturalDisasterByCity set DisasterStartDate = @DisasterStartDate")
                strSQL.AppendLine(",DisasterEndDate = @DisasterEndDate")
                strSQL.AppendLine(",BeginTime =@BeginTime")
                strSQL.AppendLine(",EndTime = @EndTime")
                strSQL.AppendLine(",DisasterType = @DisasterType")
                strSQL.AppendLine(",LastChgCompID = @LastChgComp ")
                strSQL.AppendLine(",LastChgEmpID = @LastChgID ")
                strSQL.AppendLine(",LastChgDate = @LastChgDate ")
                strSQL.AppendLine(" where CityCode = @CityCode")
                strSQL.AppendLine(" and WorkSiteID= @WorkSiteID")
                strSQL.AppendLine(" and CityName = @CityName")
                strSQL.AppendLine(" and DisasterStartDate = @ViewDisasterStartDate")
                strSQL.AppendLine(" and DisasterEndDate = @ViewDisasterEndDate")
                strSQL.AppendLine(" and BeginTime = @ViewBeginTime")
                strSQL.AppendLine(" and EndTime = @ViewEndTime")
                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                db.AddInParameter(dbcmd, "@DisasterStartDate", DbType.String, ucBeginFixDate.DateText)
                db.AddInParameter(dbcmd, "@DisasterEndDate", DbType.String, ucEndFixDate.DateText)
                db.AddInParameter(dbcmd, "@BeginTime", DbType.String, StartTimeH.SelectedValue + StartTimeM.SelectedValue)
                db.AddInParameter(dbcmd, "@EndTime", DbType.String, EndTimeH.SelectedValue + EndTimeM.SelectedValue)
                db.AddInParameter(dbcmd, "@DisasterType", DbType.String, ddlType.SelectedValue)
                db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, strCompID)
                db.AddInParameter(dbcmd, "@LastChgID", DbType.String, strChgEmpID)
                db.AddInParameter(dbcmd, "@LastChgDate", DbType.String, Now.ToString("yyyy/MM/dd HH:mm:ss"))
                db.AddInParameter(dbcmd, "@CityCode", DbType.String, ViewState.Item("CityCode").ToString)
                db.AddInParameter(dbcmd, "@WorkSiteID", DbType.String, ViewState.Item("WorkSiteID").ToString)
                db.AddInParameter(dbcmd, "@CityName", DbType.String, ViewState.Item("CityName").ToString)
                db.AddInParameter(dbcmd, "@ViewDisasterStartDate", DbType.String, ViewState.Item("BeginDate").ToString.Replace("-", "/"))
                db.AddInParameter(dbcmd, "@ViewDisasterEndDate", DbType.String, ViewState.Item("EndDate").ToString.Replace("-", "/"))
                db.AddInParameter(dbcmd, "@ViewBeginTime", DbType.String, (ViewState.Item("BeginTimeH").ToString) & (ViewState.Item("BeginTimeM").ToString))
                db.AddInParameter(dbcmd, "@ViewEndTime", DbType.String, (ViewState.Item("EndTimeH").ToString) & (ViewState.Item("EndTimeM").ToString))

            ElseIf str = "PersonTable" Then
                strSQL.AppendLine("update  NaturalDisasterByEmp set DisasterStartDate = @DisasterStartDate")
                strSQL.AppendLine(",DisasterEndDate = @DisasterEndDate")
                strSQL.AppendLine(",BeginTime =@BeginTime")
                strSQL.AppendLine(",EndTime = @EndTime")
                strSQL.AppendLine(",DisasterType = @DisasterType")
                strSQL.AppendLine(",remark = @remark")
                strSQL.AppendLine(",LastChgCompID = @LastChgComp ")
                strSQL.AppendLine(",LastChgEmpID = @LastChgID ")
                strSQL.AppendLine(",LastChgDate = @LastChgDate ")
                strSQL.AppendLine(" where EmpID = @EmpID")
                strSQL.AppendLine(" and DisasterStartDate = @ViewDisasterStartDate")
                strSQL.AppendLine(" and DisasterEndDate = @ViewDisasterEndDate")
                strSQL.AppendLine(" and BeginTime = @ViewBeginTime")
                strSQL.AppendLine(" and EndTime = @ViewEndTime")
                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                db.AddInParameter(dbcmd, "@DisasterStartDate", DbType.String, ucBeginFixDate.DateText)
                db.AddInParameter(dbcmd, "@DisasterEndDate", DbType.String, ucEndFixDate.DateText)
                db.AddInParameter(dbcmd, "@BeginTime", DbType.String, StartTimeH.SelectedValue + StartTimeM.SelectedValue)
                db.AddInParameter(dbcmd, "@EndTime", DbType.String, EndTimeH.SelectedValue + EndTimeM.SelectedValue)
                db.AddInParameter(dbcmd, "@DisasterType", DbType.String, ddlType.SelectedValue)
                db.AddInParameter(dbcmd, "@remark", DbType.String, txtRemark.Text)
                db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, strCompID)
                db.AddInParameter(dbcmd, "@LastChgID", DbType.String, strChgEmpID)
                db.AddInParameter(dbcmd, "@LastChgDate", DbType.String, Now.ToString("yyyy/MM/dd HH:mm:ss"))
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, ViewState.Item("EmpName").ToString.Substring(0, 6))
                db.AddInParameter(dbcmd, "@ViewDisasterStartDate", DbType.String, ViewState.Item("BeginDate").ToString.Replace("-", "/"))
                db.AddInParameter(dbcmd, "@ViewDisasterEndDate", DbType.String, ViewState.Item("EndDate").ToString.Replace("-", "/"))
                db.AddInParameter(dbcmd, "@ViewBeginTime", DbType.String, (ViewState.Item("BeginTimeH").ToString) & (ViewState.Item("BeginTimeM").ToString))
                db.AddInParameter(dbcmd, "@ViewEndTime", DbType.String, (ViewState.Item("EndTimeH").ToString) & (ViewState.Item("EndTimeM").ToString))

            End If
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Try
                    updateCount = db.ExecuteNonQuery(dbcmd)
                    If updateCount > 0 Then
                        Flag = True
                    Else
                        Throw New Exception("新增筆數為0")
                    End If
                    tran.Commit()
                Catch ex As Exception
                    tran.Rollback()
                    Bsp.Utility.ShowMessage(Me, "修改失敗")
                    Flag = False
                    Throw
                Finally
                    tran.Dispose()
                    If cn.State = ConnectionState.Open Then cn.Close()
                End Try
            End Using
            If Flag Then
                Bsp.Utility.ShowMessage(Me, "修改成功")
                GoBack(True)
            End If
        Else
            Bsp.Utility.ShowMessage(Me, "在此日期時段已有申請")
        End If



    End Sub
    Private Sub GoBack(ByRef Flag As Boolean)
        Dim ti As TransferInfo = Me.StateTransfer

        'If Flag Then
        '    Dim clearValue As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)
        '    clearValue("DoQuery") = ""
        '    Me.TransferFramePage(ti.CallerUrl, Nothing, clearValue)
        'Else
        Me.TransferFramePage(ti.CallerUrl, Nothing, ti.Args)
        'End If
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

#Region "相關檢核"
    Public Function checkdate() As String
        Dim errorMsg As String = ""
        Dim bDate As String = DateStrF(ucBeginFixDate.DateText).Replace("/", "")
        Dim eDate As String = DateStrF(ucEndFixDate.DateText).Replace("/", "")
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
        If txtRemark.Text.Length > 200 Then
            errorMsg = "說明字數不得大於200"
        End If

        Return errorMsg
    End Function
    'Public Function checkSameData(ByVal CityCode As String, ByVal WorkSiteID As String, ByVal DisasterStartDate As String, ByVal DisasterEndDate As String, ByVal BeginTime As String, ByVal EndTime As String, ByVal ddlTypeAf As String) As Boolean
    Public Function checkSameData(ByVal Str As String) As Boolean
        Dim strSQL As New StringBuilder()

        If ("CityTable".Equals(Str)) Then


            Dim DisasterStartDateAft As String = "'" & ucBeginFixDate.DateText & "'"
            Dim DisasterEndDateAft As String = "'" & ucEndFixDate.DateText & "'"
            Dim BeginTimeAft As String = "'" & StartTimeH.SelectedValue + StartTimeM.SelectedValue & "'"
            Dim EndTimeAft As String = "'" & EndTimeH.SelectedValue + EndTimeM.SelectedValue & "'"
            Dim ddlTypeAft As String = "'" & ddlType.SelectedValue & "'"

            Dim CityCodeBef As String = "'" & ViewState.Item("CityCode").ToString & "'"
            Dim WorkSiteIDBef As String = "'" & ViewState.Item("WorkSiteID").ToString & "'"
            Dim ddlTypeBef As String = "'" & ViewState.Item("ddlType").ToString & "'"
            Dim DisasterStartDateBef As String = "'" & ViewState.Item("BeginDate").ToString.Replace("-", "/") & "'"
            Dim DisasterEndDateBef As String = "'" & ViewState.Item("EndDate").ToString.Replace("-", "/") & "'"
            Dim BeginTimeBef As String = "'" & (ViewState.Item("BeginTimeH").ToString) & (ViewState.Item("BeginTimeM").ToString) & "'"
            Dim EndTimeBef As String = "'" & (ViewState.Item("EndTimeH").ToString) & (ViewState.Item("EndTimeM").ToString) & "'"

            strSQL.Append("select CityCode,BeginTime,EndTime,DisasterStartDate,DisasterEndDate from NaturalDisasterByCity")
            strSQL.Append(" where (CAST(CityCode AS char)+CAST(WorkSiteID AS char)+CAST(DisasterStartDate AS char)+CAST(DisasterEndDate AS char)+CAST(BeginTime AS char)+CAST(EndTime AS char)) NOT IN(SELECT CAST(CityCode AS char)+CAST(WorkSiteID AS char)+CAST(DisasterStartDate AS char)+CAST(DisasterEndDate AS char)+CAST(BeginTime AS char)+CAST(EndTime AS char) AS ID")
            strSQL.Append(" FROM NaturalDisasterByCity ")
            strSQL.Append(" where CityCode =" + CityCodeBef + "and WorkSiteID = " + WorkSiteIDBef + " and DisasterStartDate =" + DisasterStartDateBef + "  and DisasterEndDate =" + DisasterEndDateBef + " and BeginTime = " + BeginTimeBef + " and EndTime = " + EndTimeBef + ")")
            'strSQL.Append(" and DisasterType=" + ddlTypeAf + "")'不同類型時段不可重複
            strSQL.Append(" and WorkSiteID=" + WorkSiteIDBef + "")

            'strSQL.Append("and  (((" + DisasterStartDateAft + " between DisasterStartDate and DisasterEndDate) ")
            'strSQL.Append(" and  ((CAST(" + BeginTimeAft + "AS int) between CAST(BeginTime AS int) and CAST(EndTime AS int)) or")
            'strSQL.Append("  (CAST(" + EndTimeAft + "AS int) between CAST(BeginTime AS int) and CAST(EndTime AS int)) or")
            'strSQL.Append("  (CAST(" + BeginTimeAft + "AS int) <=CAST(BeginTime AS int)) and (CAST(EndTime AS int)<=CAST(" + EndTimeAft + "AS int)))")
            'strSQL.Append(") or")
            'strSQL.Append("  ((" + DisasterEndDateAft + " between DisasterStartDate and DisasterEndDate ) ")
            'strSQL.Append(" and  ((CAST(" + BeginTimeAft + "AS int) between CAST(BeginTime AS int) and CAST(EndTime AS int)) or")
            'strSQL.Append("  (CAST(" + EndTimeAft + "AS int) between CAST(BeginTime AS int) and CAST(EndTime AS int)) or")
            'strSQL.Append("  (CAST(" + BeginTimeAft + "AS int) <=CAST(BeginTime AS int)) and (CAST(EndTime AS int)<=CAST(" + EndTimeAft + "AS int)))")
            'strSQL.Append(") or")
            'strSQL.Append("  (" + DisasterStartDateAft + "<DisasterStartDate) and (DisasterEndDate <" + DisasterEndDateAft + "))")

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
        Else 'empcheck
            Dim DisasterStartDateAft As String = "'" & ucBeginFixDate.DateText & "'"
            Dim DisasterEndDateAft As String = "'" & ucEndFixDate.DateText & "'"
            Dim BeginTimeAft As String = "'" & StartTimeH.SelectedValue + StartTimeM.SelectedValue & "'"
            Dim EndTimeAft As String = "'" & EndTimeH.SelectedValue + EndTimeM.SelectedValue & "'"
            Dim ddlTypeAft As String = "'" & ddlType.SelectedValue & "'"

            Dim EmpIDBef As String = "'" & ViewState.Item("EmpName").ToString.Split("-")(0) & "'"
            Dim ddlTypeBef As String = "'" & ViewState.Item("ddlType").ToString & "'"
            Dim DisasterStartDateBef As String = "'" & ViewState.Item("BeginDate").ToString.Replace("-", "/") & "'"
            Dim DisasterEndDateBef As String = "'" & ViewState.Item("EndDate").ToString.Replace("-", "/") & "'"
            Dim BeginTimeBef As String = "'" & (ViewState.Item("BeginTimeH").ToString) & (ViewState.Item("BeginTimeM").ToString) & "'"
            Dim EndTimeBef As String = "'" & (ViewState.Item("EndTimeH").ToString) & (ViewState.Item("EndTimeM").ToString) & "'"



            strSQL.Append("select EmpID,BeginTime,EndTime,DisasterStartDate,DisasterEndDate from NaturalDisasterByEmp")
            strSQL.Append(" where (CAST(EmpID AS char)+CAST(DisasterStartDate AS char)+CAST(DisasterEndDate AS char)+CAST(BeginTime AS char)+CAST(EndTime AS char)) NOT IN(SELECT CAST(EmpID AS char)+CAST(DisasterStartDate AS char)+CAST(DisasterEndDate AS char)+CAST(BeginTime AS char)+CAST(EndTime AS char) AS ID")
            strSQL.Append(" FROM NaturalDisasterByEmp ")
            strSQL.Append(" where EmpID =" + EmpIDBef + " and DisasterStartDate =" + DisasterStartDateBef + "  and DisasterEndDate =" + DisasterEndDateBef + " and BeginTime = " + BeginTimeBef + " and EndTime = " + EndTimeBef + ")")
            'strSQL.Append(" and DisasterType=" + ddlTypeAf + "")'不同類型時段不可重複
            strSQL.Append(" and EmpID=" + EmpIDBef + "")
            'strSQL.Append("and  (((" + DisasterStartDateAft + " between DisasterStartDate and DisasterEndDate) ")
            'strSQL.Append(" and  ((CAST(" + BeginTimeAft + "AS int) between CAST(BeginTime AS int) and CAST(EndTime AS int)) or")
            'strSQL.Append("  (CAST(" + EndTimeAft + "AS int) between CAST(BeginTime AS int) and CAST(EndTime AS int)) or")
            'strSQL.Append("  (CAST(" + BeginTimeAft + "AS int) <=CAST(BeginTime AS int)) and (CAST(EndTime AS int)<=CAST(" + EndTimeAft + "AS int)))")
            'strSQL.Append(") or")
            'strSQL.Append("  ((" + DisasterEndDateAft + " between DisasterStartDate and DisasterEndDate ) ")
            'strSQL.Append(" and  ((CAST(" + BeginTimeAft + "AS int) between CAST(BeginTime AS int) and CAST(EndTime AS int)) or")
            'strSQL.Append("  (CAST(" + EndTimeAft + "AS int) between CAST(BeginTime AS int) and CAST(EndTime AS int)) or")
            'strSQL.Append("  (CAST(" + BeginTimeAft + "AS int) <=CAST(BeginTime AS int)) and (CAST(EndTime AS int)<=CAST(" + EndTimeAft + "AS int)))")
            'strSQL.Append(") or")
            'strSQL.Append("  (" + DisasterStartDateAft + "<DisasterStartDate) and (DisasterEndDate <" + DisasterEndDateAft + "))")


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


    Public Function checktime() As String
        Dim errorMsg As String = ""
        If "".Equals(StartTimeH.SelectedValue) Then
            errorMsg = "您尚未填入留守開始時間(時)"
        ElseIf "".Equals(EndTimeH.SelectedValue) Then
            errorMsg = "您尚未填入留守結束時間(時)"
        ElseIf "".Equals(StartTimeM.SelectedValue) Then
            errorMsg = "您尚未填入留守開始時間(分)"
        ElseIf "".Equals(EndTimeM.SelectedValue) Then
            errorMsg = "您尚未填入留守結束時間(分)"
        Else
            Dim startT As Integer = CInt(StartTimeH.SelectedValue & StartTimeM.SelectedValue)
            Dim endT As Integer = CInt(EndTimeH.SelectedValue & EndTimeM.SelectedValue)
            If IsNumeric(startT) And IsNumeric(endT) Then
                Dim bDate As Integer = CInt(ucBeginFixDate.DateText.Replace("/", ""))
                Dim eDate As Integer = CInt(ucEndFixDate.DateText.Replace("/", ""))
                If bDate = eDate Then
                    If startT > endT Then
                        errorMsg = "開始時間不得大於結束時間"
                    End If
                End If
            Else
                errorMsg = "限定輸入數字"
            End If
            If (Not "".Equals(ucBeginFixDate.DateText)) And ((ucBeginFixDate.DateText).Equals(ucEndFixDate.DateText)) Then
                If startT = endT Then
                    errorMsg = "同日期不能同時間"
                End If
            End If
        End If

        Return errorMsg
    End Function
#End Region
End Class
