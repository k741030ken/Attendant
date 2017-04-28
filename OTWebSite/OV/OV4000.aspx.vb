'*********************************************************************************
'功能說明：加班管理查詢與維護
'建立人員：Kevin
'建立日期：2016.12.16
'*********************************************************************************

Imports System.Data
Imports System.Globalization

Partial Class OV4000
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
            Case "btnQuery"     '修改
                DoUpdate()
            Case "btnDownload"  '匯出
                DoDownload()
            Case "btnActionX"   '清除
                DoClear()
            Case "btnActionC"   '記薪
                DoActionC()
            Case "btnUpdate"    '查詢
                DoQuery()
            Case "btnExecutes"  '作廢
                DoDelete()
        End Select
    End Sub

#Region "功能群組"

#Region "修改功能"
    Private Sub DoUpdate()
        If (Not "-1".Equals(selectedRow(gvMain).ToString.Trim)) Or (Not "-1".Equals(selectedRow(gvMain1).ToString.Trim)) Then
            If hiddenType.Value.Equals("after") Then
                Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
                Dim btnUp As New ButtonState(ButtonState.emButtonType.Update)
                btnX.Caption = "返回"
                btnUp.Caption = "存檔返回"
                Me.TransferFramePage("~/OV/OV4002.aspx", New ButtonState() {btnX, btnUp}, _
              "hiddenType=" + hiddenType.Value, _
              "OTCompID=" + gvMain1.DataKeys(selectedRow(gvMain1))("OTCompID").ToString(), _
              "OTEmpID=" + gvMain1.DataKeys(selectedRow(gvMain1))("OTEmpID").ToString(), _
              "OTStartDate=" + gvMain1.DataKeys(selectedRow(gvMain1))("OTStartDate").ToString(), _
              "OTEndDate=" + gvMain1.DataKeys(selectedRow(gvMain1))("OTEndDate").ToString(), _
              "OTSeq=" + gvMain1.DataKeys(selectedRow(gvMain1))("OTSeq").ToString(), _
                "OTTxnID=" + gvMain1.DataKeys(selectedRow(gvMain1))("OTTxnID").ToString(), _
                ckOTSalaryPaid.ID & "=" & ckOTSalaryPaid.Checked, _
                ddlIsProcessDate.ID & "=" & ddlIsProcessDate.SelectedValue, _
                  ddlType.ID & "=" & ddlType.SelectedValue, _
              ddlOrgType.ID & "=" & ddlOrgType.SelectedValue, _
               ddlDeptID.ID & "=" & ddlDeptID.SelectedValue, _
               ddlOrganID.ID & "=" & ddlOrganID.SelectedValue, _
               ddlWorkStatus.ID & "=" & ddlWorkStatus.SelectedValue, _
               ddlRankIDMIN.ID & "=" & ddlRankIDMIN.SelectedValue, _
               ddlRankIDMAX.ID & "=" & ddlRankIDMAX.SelectedValue, _
               ddlTitleIDMIN.ID & "=" & ddlTitleIDMIN.SelectedValue, _
               ddlTitleIDMAX.ID & "=" & ddlTitleIDMAX.SelectedValue, _
               ddlSex.ID & "=" & ddlSex.SelectedValue, _
               tbOTEmpID.ID & "=" & tbOTEmpID.Text, _
               tbOTEmpName.ID & "=" & tbOTEmpName.Text, _
               ddlPositionID.ID & "=" & ddlPositionID.SelectedValue, _
               ddlWorkType.ID & "=" & ddlWorkType.SelectedValue, _
               txtOvertimeDateB.ID & "=" & IIf(txtOvertimeDateB.DateText = "____/__/__", "", txtOvertimeDateB.DateText), _
               txtOvertimeDateE.ID & "=" & IIf(txtOvertimeDateE.DateText = "____/__/__", "", txtOvertimeDateE.DateText), _
               OTStartTimeH.ID & "=" & OTStartTimeH.SelectedValue, _
               OTStartTimeM.ID & "=" & OTStartTimeM.SelectedValue, _
               OTEndTimeH.ID & "=" & OTEndTimeH.SelectedValue, _
               OTEndTimeM.ID & "=" & OTEndTimeM.SelectedValue, _
               ddlOTTypeID.ID & "=" & ddlOTTypeID.SelectedValue, _
               ddlHolidayOrNot.ID & "=" & ddlHolidayOrNot.SelectedValue, _
               ddlTime.ID & "=" & ddlTime.SelectedValue, _
               ddlOTStatus.ID & "=" & ddlOTStatus.SelectedValue, _
               tbOTFormNO.ID & "=" & tbOTFormNO.Text, _
               tbOTPayDate.ID & "=" & tbOTPayDate.Text, _
               txtTakeOfficeDateB.ID & "=" & IIf(txtTakeOfficeDateB.DateText = "____/__/__", "", txtTakeOfficeDateB.DateText), _
               txtTakeOfficeDateE.ID & "=" & IIf(txtTakeOfficeDateE.DateText = "____/__/__", "", txtTakeOfficeDateE.DateText), _
               txtLeaveOfficeDateB.ID & "=" & IIf(txtLeaveOfficeDateB.DateText = "____/__/__", "", txtLeaveOfficeDateB.DateText), _
               txtLeaveOfficeDateE.ID & "=" & IIf(txtLeaveOfficeDateE.DateText = "____/__/__", "", txtLeaveOfficeDateE.DateText), _
               txtDateOfApprovalB.ID & "=" & IIf(txtDateOfApprovalB.DateText = "____/__/__", "", txtDateOfApprovalB.DateText), _
               txtDateOfApprovalE.ID & "=" & IIf(txtDateOfApprovalE.DateText = "____/__/__", "", txtDateOfApprovalE.DateText), _
               txtDateOfApplicationB.ID & "=" & IIf(txtDateOfApplicationB.DateText = "____/__/__", "", txtDateOfApplicationB.DateText), _
               txtDateOfApplicationE.ID & "=" & IIf(txtDateOfApplicationE.DateText = "____/__/__", "", txtDateOfApplicationE.DateText), _
                "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
                'chkOrganName.ID & "=" & chkOrganName.Checked, _
            ElseIf hiddenType.Value.Equals("bef") Then
                Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
                Dim btnUp As New ButtonState(ButtonState.emButtonType.Update)
                btnX.Caption = "返回"
                btnUp.Caption = "存檔返回"
                Me.TransferFramePage("~/OV/OV4002.aspx", New ButtonState() {btnX, btnUp}, _
              "hiddenType=" + hiddenType.Value, _
              "OTCompID=" + gvMain.DataKeys(selectedRow(gvMain))("OTCompID").ToString(), _
              "OTEmpID=" + gvMain.DataKeys(selectedRow(gvMain))("OTEmpID").ToString(), _
              "OTStartDate=" + gvMain.DataKeys(selectedRow(gvMain))("OTStartDate").ToString(), _
              "OTEndDate=" + gvMain.DataKeys(selectedRow(gvMain))("OTEndDate").ToString(), _
              "OTSeq=" + gvMain.DataKeys(selectedRow(gvMain))("OTSeq").ToString(), _
               "OTTxnID=" + gvMain.DataKeys(selectedRow(gvMain))("OTTxnID").ToString(), _
               ddlType.ID & "=" & ddlType.SelectedValue, _
               ddlOrgType.ID & "=" & ddlOrgType.SelectedValue, _
               ddlDeptID.ID & "=" & ddlDeptID.SelectedValue, _
               ddlOrganID.ID & "=" & ddlOrganID.SelectedValue, _
               ddlWorkStatus.ID & "=" & ddlWorkStatus.SelectedValue, _
               ddlRankIDMIN.ID & "=" & ddlRankIDMIN.SelectedValue, _
               ddlRankIDMAX.ID & "=" & ddlRankIDMAX.SelectedValue, _
               ddlTitleIDMIN.ID & "=" & ddlTitleIDMIN.SelectedValue, _
               ddlTitleIDMAX.ID & "=" & ddlTitleIDMAX.SelectedValue, _
               ddlSex.ID & "=" & ddlSex.SelectedValue, _
               tbOTEmpID.ID & "=" & tbOTEmpID.Text, _
               tbOTEmpName.ID & "=" & tbOTEmpName.Text, _
               ddlPositionID.ID & "=" & ddlPositionID.SelectedValue, _
               ddlWorkType.ID & "=" & ddlWorkType.SelectedValue, _
               txtOvertimeDateB.ID & "=" & IIf(txtOvertimeDateB.DateText = "____/__/__", "", txtOvertimeDateB.DateText), _
               txtOvertimeDateE.ID & "=" & IIf(txtOvertimeDateE.DateText = "____/__/__", "", txtOvertimeDateE.DateText), _
               OTStartTimeH.ID & "=" & OTStartTimeH.SelectedValue, _
               OTStartTimeM.ID & "=" & OTStartTimeM.SelectedValue, _
               OTEndTimeH.ID & "=" & OTEndTimeH.SelectedValue, _
               OTEndTimeM.ID & "=" & OTEndTimeM.SelectedValue, _
               ddlOTTypeID.ID & "=" & ddlOTTypeID.SelectedValue, _
               ddlHolidayOrNot.ID & "=" & ddlHolidayOrNot.SelectedValue, _
               ddlTime.ID & "=" & ddlTime.SelectedValue, _
               ddlOTStatus.ID & "=" & ddlOTStatus.SelectedValue, _
               tbOTFormNO.ID & "=" & tbOTFormNO.Text, _
               tbOTPayDate.ID & "=" & tbOTPayDate.Text, _
               txtTakeOfficeDateB.ID & "=" & IIf(txtTakeOfficeDateB.DateText = "____/__/__", "", txtTakeOfficeDateB.DateText), _
               txtTakeOfficeDateE.ID & "=" & IIf(txtTakeOfficeDateE.DateText = "____/__/__", "", txtTakeOfficeDateE.DateText), _
               txtLeaveOfficeDateB.ID & "=" & IIf(txtLeaveOfficeDateB.DateText = "____/__/__", "", txtLeaveOfficeDateB.DateText), _
               txtLeaveOfficeDateE.ID & "=" & IIf(txtLeaveOfficeDateE.DateText = "____/__/__", "", txtLeaveOfficeDateE.DateText), _
               txtDateOfApprovalB.ID & "=" & IIf(txtDateOfApprovalB.DateText = "____/__/__", "", txtDateOfApprovalB.DateText), _
               txtDateOfApprovalE.ID & "=" & IIf(txtDateOfApprovalE.DateText = "____/__/__", "", txtDateOfApprovalE.DateText), _
               txtDateOfApplicationB.ID & "=" & IIf(txtDateOfApplicationB.DateText = "____/__/__", "", txtDateOfApplicationB.DateText), _
               txtDateOfApplicationE.ID & "=" & IIf(txtDateOfApplicationE.DateText = "____/__/__", "", txtDateOfApplicationE.DateText), _
                "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
            Else
                Bsp.Utility.ShowMessage(Me, "請選擇資料")
            End If
        Else
            Bsp.Utility.ShowMessage(Me, "未選取資料列!")
        End If
    End Sub
#End Region

#Region "匯出功能"

    ''' <summary>
    ''' 匯出資料
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DoDownload()
        If ViewState.Item("DoQuery") = "Y" Then
            If gvMain.Rows.Count = 0 And gvMain1.Rows.Count = 0 Then   '2015/12/01 Modify
                Bsp.Utility.ShowMessage(Me, "無資料下傳！")
            Else
                Try
                    Dim objOV_1 = Me.SetTableProperty()
                    Dim strFileName As String = ""


                    If objOV_1.Type.Equals("bef") Then
                        '產出檔頭
                        strFileName = Bsp.Utility.GetNewFileName("加班單預先申請列表-") & ".xls"
                    Else
                        '產出檔頭
                        strFileName = Bsp.Utility.GetNewFileName("加班單事後申報列表-") & ".xls"
                    End If


                    '動態產生GridView以便匯出成EXCEL
                    Dim gvExport As GridView = New GridView()
                    gvExport.AllowPaging = False
                    gvExport.AllowSorting = False
                    gvExport.FooterStyle.BackColor = Drawing.ColorTranslator.FromHtml("#99CCCC")
                    gvExport.FooterStyle.ForeColor = Drawing.ColorTranslator.FromHtml("#003399")
                    gvExport.RowStyle.CssClass = "tr_evenline"
                    gvExport.AlternatingRowStyle.CssClass = "tr_oddline"
                    gvExport.EmptyDataRowStyle.CssClass = "GridView_EmptyRowStyle"

                    Dim dt As DataTable = objOV_1.getDownLoadTable()
                    Dim Array As ArrayList = New ArrayList()
                    If dt.Rows.Count > 0 Then
                        If "1".Equals(objOV_1.Time) Then

                            For Each item As DataRow In dt.Rows
                                If (item("時段一").ToString()).Equals("0") Or (item("時段一").ToString()).Equals("_") Then
                                    Array.Add(item)
                                End If
                            Next
                            For Each item As DataRow In Array
                                dt.Rows.Remove(item)
                            Next

                        ElseIf "2".Equals(objOV_1.Time) Then
                            For Each item As DataRow In dt.Rows
                                If (item("時段二").ToString()).Equals("0") Or (item("時段二").ToString()).Equals("_") Then
                                    Array.Add(item)
                                End If
                            Next
                            For Each item As DataRow In Array
                                dt.Rows.Remove(item)
                            Next

                        ElseIf "3".Equals(objOV_1.Time) Then
                            For Each item As DataRow In dt.Rows
                                If (item("時段三").ToString()).Equals("0") Or (item("時段三").ToString()).Equals("_") Then
                                    Array.Add(item)
                                End If
                            Next
                            For Each item As DataRow In Array
                                dt.Rows.Remove(item)
                            Next
                        ElseIf "4".Equals(objOV_1.Time) Then
                            For Each item As DataRow In dt.Rows
                                If (item("留守時段").ToString()).Equals("0") Or (item("時段三").ToString()).Equals("_") Then
                                    Array.Add(item)
                                End If
                            Next
                            For Each item As DataRow In Array
                                dt.Rows.Remove(item)
                            Next
                        End If
                    End If
                    gvExport.DataSource = dt
                    gvExport.DataBind()
                    Response.ClearContent()
                    Response.BufferOutput = True
                    Response.Charset = "utf-8"
                    Response.ContentType = "application/save-as"         '隱藏檔案網址路逕的下載
                    Response.AddHeader("Content-Transfer-Encoding", "binary")
                    Response.ContentEncoding = System.Text.Encoding.UTF8
                    Response.AddHeader("content-disposition", "attachment; filename=" & Server.UrlPathEncode(strFileName))

                    Dim oStringWriter As New System.IO.StringWriter()
                    Dim oHtmlTextWriter As New System.Web.UI.HtmlTextWriter(oStringWriter)

                    Response.Write("<meta http-equiv=Content-Type content=text/html charset=utf-8>")
                    Dim style As String = "<style>td{font-size:9pt} a{font-size:9pt} tr{page-break-after: always}</style>"


                    gvExport.Attributes.Add("style", "vnd.ms-excel.numberformat:@")
                    If objOV_1.Type.Equals("bef") Then
                        For i = 0 To gvExport.Rows.Count - 1
                            '20170219 Beatrice mod
                            gvExport.Rows(i).Cells(25).Attributes.Add("style", "vnd.ms-excel.numberformat:#,##0.0")
                            gvExport.Rows(i).Cells(32).Attributes.Add("style", "vnd.ms-excel.numberformat:#,##0.0")
                        Next
                    Else
                        For i = 0 To gvExport.Rows.Count - 1
                            '20170219 Beatrice mod
                            gvExport.Rows(i).Cells(24).Attributes.Add("style", "vnd.ms-excel.numberformat:#,##0.0")
                            gvExport.Rows(i).Cells(25).Attributes.Add("style", "vnd.ms-excel.numberformat:#,##0.0")
                            gvExport.Rows(i).Cells(26).Attributes.Add("style", "vnd.ms-excel.numberformat:#,##0.0")
                            gvExport.Rows(i).Cells(30).Attributes.Add("style", "vnd.ms-excel.numberformat:#,##0.0")
                            gvExport.Rows(i).Cells(37).Attributes.Add("style", "vnd.ms-excel.numberformat:#,##0.0")
                        Next

                    End If



                    gvExport.RenderControl(oHtmlTextWriter)
                    Response.Write(style)
                    Response.Write(oStringWriter.ToString())
                    Response.End()
                Catch ex As Exception
                    Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoDownload", ex)
                End Try

            End If
        Else
            Bsp.Utility.ShowMessage(Me, "請先查詢！")
        End If
    End Sub
#End Region

#Region "清除功能"

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
            Bsp.Utility.FillDDL(ddlOrganID, "eHRMSDB", "Organization", "RTRIM(OrganID)", "OrganName ", Bsp.Utility.DisplayType.Full, "", " And VirtualFlag = '0' And CompID = " & Bsp.Utility.Quote(UserProfile.SelectCompRoleID) & " AND ((OrganID = OrganID AND OrganID <> DeptID AND OrganID <> OrgType)OR(OrganID = OrganID AND OrganID = DeptID AND OrganID = OrgType)) ", "Order By OrganID")
            ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
        Else
            Bsp.Utility.FillDDL(ddlOrganID, "eHRMSDB", "Organization", "RTRIM(OrganID)", "OrganName ", Bsp.Utility.DisplayType.Full, "", " And VirtualFlag = '0' And CompID = " & Bsp.Utility.Quote(UserProfile.SelectCompRoleID) & " AND ((OrganID = OrganID AND OrganID <> DeptID AND OrganID <> OrgType)OR(OrganID = OrganID AND OrganID = DeptID AND OrganID = OrgType)) " & " And DeptID = " & Bsp.Utility.Quote(ddlDeptID.SelectedValue), "Order By OrganID")
            ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
        End If

        Bsp.Utility.FillDDL(ddlPositionID, "eHRMSDB", "Position", "distinct PositionID", "Remark", Bsp.Utility.DisplayType.Full, "", "And CompID =" + Bsp.Utility.Quote(UserProfile.SelectCompRoleID), "") '職位
        ddlPositionID.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        ddlTitleIDMIN.Items.Clear()
        ddlTitleIDMAX.Items.Clear()

        ddlTitleIDMIN.Items.Insert(0, New ListItem("---請先選擇職等---", ""))
        ddlTitleIDMAX.Items.Insert(0, New ListItem("---請先選擇職等---", ""))

        ddlType.SelectedIndex = 0
        ddlCompID.SelectedIndex = 0
        ddlOrgType.SelectedIndex = 0
        ddlDeptID.SelectedIndex = 0
        OTStartTimeH.SelectedIndex = 0
        OTStartTimeM.SelectedIndex = 0
        OTEndTimeH.SelectedIndex = 0
        OTEndTimeM.SelectedIndex = 0
        ddlOrganID.SelectedIndex = 0
        ddlRankIDMIN.SelectedIndex = 0
        ddlRankIDMAX.SelectedIndex = 0
        ddlTitleIDMIN.SelectedIndex = 0 '20170219 Beatrice mod
        ddlTitleIDMAX.SelectedIndex = 0 '20170219 Beatrice mod
        ddlOTTypeID.SelectedIndex = 0

        'ddlTitleName.SelectedIndex = 0
        ddlTime.SelectedIndex = 0
        ddlSex.SelectedIndex = 0
        ddlPositionID.SelectedIndex = 0
        ddlWorkType.SelectedIndex = 0
        ddlHolidayOrNot.SelectedIndex = 0
        ddlOTStatus.SelectedIndex = 0
        ddlWorkStatus.SelectedIndex = 0
        ddlIsProcessDate.SelectedIndex = 0
        tbOTPayDate.Text = ""
        tbOTEmpID.Text = ""
        tbOTEmpName.Text = ""
        tbOTFormNO.Text = ""
        txtOvertimeDateB.DateText = ""
        txtOvertimeDateE.DateText = ""
        txtTakeOfficeDateB.DateText = ""
        txtTakeOfficeDateE.DateText = ""
        txtLeaveOfficeDateB.DateText = ""
        txtLeaveOfficeDateE.DateText = ""
        txtDateOfApprovalB.DateText = ""
        txtDateOfApprovalE.DateText = ""
        txtDateOfApplicationB.DateText = ""
        txtDateOfApplicationE.DateText = ""
        ddlSalaryOrAdjust.SelectedValue = ""

        tbOTPayDateErrorMsg.Visible = False
        If ddlType.SelectedValue.Equals("bef") Then
            ddlTime.SelectedIndex = 0
            ddlIsProcessDate.SelectedIndex = 0
            tbOTPayDate.Text = ""
            ddlTime.Enabled = False
            tbOTPayDate.Enabled = False
            ddlIsProcessDate.Enabled = False
            ckOTSalaryPaid.Enabled = False


        Else
            tbOTPayDate.Text = ""
            ddlTime.Enabled = True
            tbOTPayDate.Enabled = True
            ddlIsProcessDate.Enabled = True
            ckOTSalaryPaid.Enabled = True

        End If
        ckOTSalaryPaid.Checked = False
    End Sub
#End Region

#Region "計薪後收回功能"
    Private Sub DoActionC()
        If hiddenType.Value.Equals("bef") And gvMain.Rows.Count > 0 Then
            Bsp.Utility.ShowMessage(Me, "只有事後申報表單才可計薪後收回")
        ElseIf hiddenType.Value.Equals("after") And gvMain1.Rows.Count > 0 Then
            Dim errorMsg As String = checkData()
            Dim OV_1 As OV_1 = SetTableProperty()
            If errorMsg.Count = 0 And (Not selectedRow(gvMain1) = -1) Then
                Dim compID = gvMain1.DataKeys(selectedRow(gvMain1))("OTCompID").ToString()
                Dim empID = gvMain1.DataKeys(selectedRow(gvMain1))("OTEmpID").ToString()
                Dim TxnID = gvMain1.DataKeys(selectedRow(gvMain1))("OTTxnID").ToString()
                Dim flag As Boolean = OV_1.changeOTStatusTo9(compID, empID, TxnID, Bsp.Utility.Quote(UserProfile.CompID), Bsp.Utility.Quote(UserProfile.UserID))
                If flag Then
                    Bsp.Utility.ShowMessage(Me, "修改成功")
                    If funCheckData() Then
                        setDataForGridView()
                        hiddenType.Value = ddlType.SelectedValue
                    End If
                Else
                    Bsp.Utility.ShowMessage(Me, "修改失敗")
                End If
            Else
                Bsp.Utility.ShowMessage(Me, errorMsg)
            End If
        End If


    End Sub
#End Region

#Region "查詢功能"

    Private Sub DoQuery()
        If funCheckData() Then
            setDataForGridView()
            hiddenType.Value = ddlType.SelectedValue
        End If
    End Sub
#End Region

#Region "作廢功能"
    Private Sub DoDelete()
        If hiddenType.Value.Equals("bef") And gvMain.Rows.Count > 0 Then
            Bsp.Utility.ShowMessage(Me, "只有事後申報表單才可作廢")
        ElseIf hiddenType.Value.Equals("after") And gvMain1.Rows.Count > 0 Then
            Dim errorMsg As String = checkData1()
            Dim OV_1 As OV_1 = SetTableProperty()
            If errorMsg.Count = 0 And (Not selectedRow(gvMain1) = -1) Then

                '若未拋轉至OverTime，則直接更新此筆狀態為作廢(OTStatus ->7)；
                '   若已拋轉至OverTime，應判斷OverTime的SalaryPaid欄位是否為0，0為未計薪，1為有計薪，
                '   若為0則刪除OverTime此筆資料，若為1，則不可作廢，應出現提示訊息告知使用者此筆已計薪不可作廢


                If (Not gvMain1.DataKeys(selectedRow(gvMain1))("ToOverTimeFlag").Equals("1")) Then
                    Dim compID = gvMain1.DataKeys(selectedRow(gvMain1))("OTCompID").ToString()
                    Dim empID = gvMain1.DataKeys(selectedRow(gvMain1))("OTEmpID").ToString()
                    Dim TxnID = gvMain1.DataKeys(selectedRow(gvMain1))("OTTxnID").ToString()
                    Dim OTFromAdvanceTxnId = gvMain1.DataKeys(selectedRow(gvMain1))("OTFromAdvanceTxnId").ToString()
                    Dim FlowCaseID = gvMain1.DataKeys(selectedRow(gvMain1))("FlowCaseID").ToString().Trim
                    Dim flag As Boolean = OV_1.changeOTStatusTo7(compID, empID, TxnID, Bsp.Utility.Quote(UserProfile.CompID), Bsp.Utility.Quote(UserProfile.UserID))
                    If flag Then
                        OV_1.UPdateAattendantDBFlowCase(UserProfile.ActDeptID, UserProfile.ActDeptName, UserProfile.ActUserID, UserProfile.ActUserName, FlowCaseID, OTFromAdvanceTxnId)

                        Bsp.Utility.ShowMessage(Me, "作廢成功")
                        If funCheckData() Then
                            setDataForGridView()
                            hiddenType.Value = ddlType.SelectedValue
                        End If
                    Else
                        Bsp.Utility.ShowMessage(Me, "作廢失敗")
                    End If
                Else
                    Dim compID As String = gvMain1.DataKeys(selectedRow(gvMain1))("OTCompID").ToString()
                    Dim empID = gvMain1.DataKeys(selectedRow(gvMain1))("OTEmpID").ToString()
                    Dim OTDate = gvMain1.DataKeys(selectedRow(gvMain1))("OTDate").ToString().Split("~")(0)
                    Dim OTStartTime = gvMain1.DataKeys(selectedRow(gvMain1))("OTStartTime").ToString()
                    Dim isSalaryPaid As Boolean = OV_1.checkSalaryPaid(compID, empID, OTDate, OTStartTime)
                    Dim FlowCaseID = gvMain1.DataKeys(selectedRow(gvMain1))("FlowCaseID").ToString().Trim
                    Dim TxnID As String = gvMain1.DataKeys(selectedRow(gvMain1))("OTTxnID").ToString()
                    Dim OTFromAdvanceTxnId = gvMain1.DataKeys(selectedRow(gvMain1))("OTFromAdvanceTxnId").ToString()

                    Dim OVDDB As DataTable = OV_1.QuiryOldDataTableArrForOTTxnID(TxnID, "after")
                    If isSalaryPaid Then
                        Bsp.Utility.ShowMessage(Me, "此資料已計薪不可作廢")
                    Else
                        If OV_1.deleteOverTimeTable(OVDDB) Then
                            Dim flag As Boolean = OV_1.changeOTStatusTo7(compID, empID, TxnID, Bsp.Utility.Quote(UserProfile.CompID), Bsp.Utility.Quote(UserProfile.UserID))
                            If flag Then
                                OV_1.UPdateAattendantDBFlowCase(UserProfile.ActDeptID, UserProfile.ActDeptName, UserProfile.ActUserID, UserProfile.ActUserName, FlowCaseID, OTFromAdvanceTxnId)
                                Bsp.Utility.ShowMessage(Me, "作廢成功")
                                If funCheckData() Then
                                    setDataForGridView()
                                    hiddenType.Value = ddlType.SelectedValue
                                End If
                            Else
                                Bsp.Utility.ShowMessage(Me, "作廢失敗")
                            End If
                        End If

                    End If
                End If
            Else
                Bsp.Utility.ShowMessage(Me, errorMsg)
            End If

        End If
    End Sub
#End Region

#End Region


#End Region

#Region "載入頁面"

    ''' <summary>
    ''' 此為頁面進入點 
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

            '載入頁面
            LoadDate()

            If ddlType.SelectedValue.Equals("bef") Then
                tbOTPayDate.Text = ""
                ddlTime.Enabled = False
                tbOTPayDate.Enabled = False
                ddlIsProcessDate.Enabled = False
                ckOTSalaryPaid.Enabled = False
                div_tb.Style.Add("display", "block")
                div_tb1.Style.Add("display", "none")

            Else
                tbOTPayDate.Text = ""
                ddlTime.Enabled = True
                tbOTPayDate.Enabled = True
                ddlIsProcessDate.Enabled = True
                ckOTSalaryPaid.Enabled = True
                div_tb.Style.Add("display", "none")
                div_tb1.Style.Add("display", "block")
            End If
        Else
            '單位部門 塞顏色
            subReLoadColor(ddlOrgType)
            subReLoadColor(ddlDeptID)
        End If
    End Sub

    ''' <summary>
    ''' 載入頁面資料
    ''' </sum面進入點mary>
    ''' <remarks></remarks>
    Private Sub LoadDate()
        OTStartTimeH.Items.Insert(0, New ListItem("--", ""))
        OTStartTimeM.Items.Insert(0, New ListItem("--", ""))
        For i = 1 To 24 Step +1
            If i <= 10 Then
                OTStartTimeH.Items.Insert(i, New ListItem("0" + (i - 1).ToString, "0" + (i - 1).ToString))
            Else
                OTStartTimeH.Items.Insert(i, New ListItem((i - 1).ToString, i - 1.ToString))
            End If
        Next i
        For i = 1 To 60 Step +1
            If i <= 10 Then
                OTStartTimeM.Items.Insert(i, New ListItem("0" + (i - 1).ToString, "0" + (i - 1).ToString))
            Else
                OTStartTimeM.Items.Insert(i, New ListItem((i - 1).ToString, (i - 1).ToString))
            End If
        Next i
        OTEndTimeH.Items.Insert(0, New ListItem("--", ""))
        OTEndTimeM.Items.Insert(0, New ListItem("--", ""))
        For i = 1 To 24 Step +1
            If i <= 10 Then
                OTEndTimeH.Items.Insert(i, New ListItem("0" + (i - 1).ToString, "0" + (i - 1).ToString))
            Else
                OTEndTimeH.Items.Insert(i, New ListItem((i - 1).ToString, (i - 1).ToString))
            End If
        Next i
        For i = 1 To 60 Step +1
            If i <= 10 Then
                OTEndTimeM.Items.Insert(i, New ListItem("0" + (i - 1).ToString, "0" + (i - 1).ToString))
            Else
                OTEndTimeM.Items.Insert(i, New ListItem((i - 1).ToString, (i - 1).ToString))
            End If
        Next i

        Bsp.Utility.FillDDL(ddlCompID, "eHRMSDB", "Company", "CompID", "CompName")
        ddlCompID.Items.Insert(0, New ListItem("　- -請選擇- -", ""))

        Bsp.Utility.FillDDL(ddlOTTypeID, "AattendantDB", "AT_CodeMap", "Code", "CodeCName", Bsp.Utility.DisplayType.OnlyName, , "and TabName='OverTime' and FldName='OverTimeType' and NotShowFlag='0'", "")

        Bsp.Utility.FillDDL(ddlRankIDMAX, "eHRMSDB", "Rank", "RankID", "RankID", Bsp.Utility.DisplayType.OnlyID, , "And CompID = " + Bsp.Utility.Quote(UserProfile.SelectCompRoleID), "group by RankID")
        ddlRankIDMAX.Items.Insert(0, New ListItem("--", ""))
        Bsp.Utility.FillDDL(ddlRankIDMIN, "eHRMSDB", "Rank", "RankID", "RankID", Bsp.Utility.DisplayType.OnlyID, , "And CompID = " + Bsp.Utility.Quote(UserProfile.SelectCompRoleID), "group by RankID")
        ddlRankIDMIN.Items.Insert(0, New ListItem("--", ""))
        ddlTitleIDMAX.Items.Insert(0, New ListItem("---請先選擇職等---", "")) '20170219 Beatrice mod
        ddlTitleIDMIN.Items.Insert(0, New ListItem("---請先選擇職等---", "")) '20170219 Beatrice mod


        Dim str As String = "join " + eHRMSDB_ITRD + ".[dbo].EmpPosition EP ON PO.CompID=EP.CompID AND PO.PositionID=EP.PositionID AND EP.PrincipalFlag='1' join " + eHRMSDB_ITRD + ".[dbo].Personal P ON EP.CompID=P.CompID AND EP.EmpID=P.EmpID  join " + eHRMSDB_ITRD + ".[dbo].Organization ORT ON  P.OrganID=ORT.OrganID AND P.CompID =ORT.CompID"


        If (Not ddlOrganID.SelectedValue.ToString.Trim.Equals("")) Then
            Bsp.Utility.FillDDL(ddlPositionID, "eHRMSDB", "Position PO", "distinct PO.PositionID", "PO.Remark", Bsp.Utility.DisplayType.Full, str, "And PO.CompID =" + Bsp.Utility.Quote(UserProfile.SelectCompRoleID) + " and ORT.OrganID= '" + ddlOrganID.SelectedValue + "'", "Order By  PO.PositionID") '職位
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


        Bsp.Utility.FillDDL(ddlWorkType, "eHRMSDB", "WorkType", "WorkTypeID", "Remark", Bsp.Utility.DisplayType.Full, "", "And CompID = " + Bsp.Utility.Quote(UserProfile.SelectCompRoleID), "") '工作性質
        ddlWorkType.Items.Insert(0, New ListItem("　- -請選擇- -", ""))

        Bsp.Utility.FillDDL(ddlWorkStatus, "eHRMSDB", "WorkStatus", "WorkCode", "Remark", Bsp.Utility.DisplayType.Full, "", "", "") '職位
        ddlWorkStatus.Items.Insert(0, New ListItem("　- -請選擇- -", ""))

        ddlOTTypeID.Items.Insert(0, New ListItem("　- -請選擇- -", ""))

        If ddlType.SelectedValue.Equals("bef") Then
            ddlOTStatus.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
            ddlOTStatus.Items.Insert(1, New ListItem("暫存", "1"))
            ddlOTStatus.Items.Insert(2, New ListItem("送簽", "2"))
            ddlOTStatus.Items.Insert(3, New ListItem("核准", "3"))
            ddlOTStatus.Items.Insert(4, New ListItem("駁回", "4"))
            ddlOTStatus.Items.Insert(5, New ListItem("刪除", "5"))
            ddlOTStatus.Items.Insert(6, New ListItem("取消", "9"))
        Else
            ddlOTStatus.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
            ddlOTStatus.Items.Insert(1, New ListItem("暫存", "1"))
            ddlOTStatus.Items.Insert(2, New ListItem("送簽", "2"))
            ddlOTStatus.Items.Insert(3, New ListItem("核准", "3"))
            ddlOTStatus.Items.Insert(4, New ListItem("駁回", "4"))
            ddlOTStatus.Items.Insert(5, New ListItem("刪除", "5"))
            ddlOTStatus.Items.Insert(6, New ListItem("取消", "6"))
            ddlOTStatus.Items.Insert(7, New ListItem("作廢", "7"))
            ddlOTStatus.Items.Insert(8, New ListItem("計薪後收回", "9"))
        End If

        ViewState.Item("OrgTypeColors") = New List(Of ArrayList)()
        ViewState.Item("DeptColors") = New List(Of ArrayList)()
        subLoadOrganColor(ddlDeptID, UserProfile.SelectCompRoleID)
        subLoadOrganColor(ddlOrgType, UserProfile.SelectCompRoleID)
        '科組課
        ddlDept_Changed(Nothing, Nothing)


        'Bsp.Utility.FillDDL(ddlOTTypeID, "eHRMSDB", "dOTReasonID", "OTReasonId", "OTReasonName")
        'Bsp.Utility.FillDDL(ddlOTTypeID, "eHRMSDB", "OverTimeType", "OTTypeId", "OTTypeName")
    End Sub

    ''' <summary>
    ''' 返回頁面事件
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

            If ddlType.SelectedValue.Equals("bef") Then
                ddlTime.Enabled = False
                tbOTPayDate.Enabled = False
                ddlIsProcessDate.Enabled = False
                ckOTSalaryPaid.Enabled = False '20170219 Beatrice Add
            Else
                ddlTime.Enabled = True
                tbOTPayDate.Enabled = True
                ddlIsProcessDate.Enabled = True
                ckOTSalaryPaid.Enabled = True '20170219 Beatrice Add
                'ddlOTStatus.Items.Clear()
                'ddlOTStatus.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
                'ddlOTStatus.Items.Insert(1, New ListItem("暫存", "1"))
                'ddlOTStatus.Items.Insert(2, New ListItem("送簽", "2"))
                'ddlOTStatus.Items.Insert(3, New ListItem("核准", "3"))
                'ddlOTStatus.Items.Insert(4, New ListItem("駁回", "4"))
                'ddlOTStatus.Items.Insert(5, New ListItem("刪除", "5"))
                'ddlOTStatus.Items.Insert(6, New ListItem("取消", "6"))
                'ddlOTStatus.Items.Insert(7, New ListItem("作廢", "7"))
                'ddlOTStatus.Items.Insert(8, New ListItem("計薪後收回", "9"))
            End If
            If Regex.IsMatch(tbOTPayDate.Text, "^\d{6}$") Or "".Equals(tbOTPayDate.Text.Trim) Then
                tbOTPayDateErrorMsg.Visible = False
            Else
                tbOTPayDateErrorMsg.Visible = True
            End If

            If ht.ContainsKey("DoQuery") Then
                If ht("DoQuery").ToString() = "Y" Then
                    ViewState.Item("DoQuery") = "Y"
                    DoQuery()
                End If
            End If

        End If
    End Sub

#Region "明細跳轉事件"
    Protected Sub gvMain_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMain.RowCommand

        If e.CommandName = "Detail" Then
            Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
            btnX.Caption = "返回"
            Me.TransferFramePage("~/OV/OV4001.aspx", New ButtonState() {btnX}, _
          "hiddenType=" + hiddenType.Value, _
          "OTCompID=" + gvMain.DataKeys(selectedRow(gvMain))("OTCompID").ToString(), _
          "OTEmpID=" + gvMain.DataKeys(selectedRow(gvMain))("OTEmpID").ToString(), _
          "OTStartDate=" + gvMain.DataKeys(selectedRow(gvMain))("OTStartDate").ToString(), _
          "OTEndDate=" + gvMain.DataKeys(selectedRow(gvMain))("OTEndDate").ToString(), _
          "OTSeq=" + gvMain.DataKeys(selectedRow(gvMain))("OTSeq").ToString(), _
           "OTTxnID=" + gvMain.DataKeys(selectedRow(gvMain))("OTTxnID").ToString(), _
           ddlType.ID & "=" & ddlType.SelectedValue, _
           ddlOrgType.ID & "=" & ddlOrgType.SelectedValue, _
           ddlDeptID.ID & "=" & ddlDeptID.SelectedValue, _
           ddlOrganID.ID & "=" & ddlOrganID.SelectedValue, _
           ddlWorkStatus.ID & "=" & ddlWorkStatus.SelectedValue, _
           ddlRankIDMIN.ID & "=" & ddlRankIDMIN.SelectedValue, _
           ddlRankIDMAX.ID & "=" & ddlRankIDMAX.SelectedValue, _
           ddlTitleIDMIN.ID & "=" & ddlTitleIDMIN.SelectedValue, _
           ddlTitleIDMAX.ID & "=" & ddlTitleIDMAX.SelectedValue, _
           ddlSex.ID & "=" & ddlSex.SelectedValue, _
           tbOTEmpID.ID & "=" & tbOTEmpID.Text, _
           tbOTEmpName.ID & "=" & tbOTEmpName.Text, _
           ddlPositionID.ID & "=" & ddlPositionID.SelectedValue, _
           ddlWorkType.ID & "=" & ddlWorkType.SelectedValue, _
           txtOvertimeDateB.ID & "=" & IIf(txtOvertimeDateB.DateText = "____/__/__", "", txtOvertimeDateB.DateText), _
           txtOvertimeDateE.ID & "=" & IIf(txtOvertimeDateE.DateText = "____/__/__", "", txtOvertimeDateE.DateText), _
           OTStartTimeH.ID & "=" & OTStartTimeH.SelectedValue, _
           OTStartTimeM.ID & "=" & OTStartTimeM.SelectedValue, _
           OTEndTimeH.ID & "=" & OTEndTimeH.SelectedValue, _
           OTEndTimeM.ID & "=" & OTEndTimeM.SelectedValue, _
           ddlOTTypeID.ID & "=" & ddlOTTypeID.SelectedValue, _
           ddlHolidayOrNot.ID & "=" & ddlHolidayOrNot.SelectedValue, _
           ddlTime.ID & "=" & ddlTime.SelectedValue, _
           ddlOTStatus.ID & "=" & ddlOTStatus.SelectedValue, _
           tbOTFormNO.ID & "=" & tbOTFormNO.Text, _
           tbOTPayDate.ID & "=" & tbOTPayDate.Text, _
           txtTakeOfficeDateB.ID & "=" & IIf(txtTakeOfficeDateB.DateText = "____/__/__", "", txtTakeOfficeDateB.DateText), _
           txtTakeOfficeDateE.ID & "=" & IIf(txtTakeOfficeDateE.DateText = "____/__/__", "", txtTakeOfficeDateE.DateText), _
           txtLeaveOfficeDateB.ID & "=" & IIf(txtLeaveOfficeDateB.DateText = "____/__/__", "", txtLeaveOfficeDateB.DateText), _
           txtLeaveOfficeDateE.ID & "=" & IIf(txtLeaveOfficeDateE.DateText = "____/__/__", "", txtLeaveOfficeDateE.DateText), _
           txtDateOfApprovalB.ID & "=" & IIf(txtDateOfApprovalB.DateText = "____/__/__", "", txtDateOfApprovalB.DateText), _
           txtDateOfApprovalE.ID & "=" & IIf(txtDateOfApprovalE.DateText = "____/__/__", "", txtDateOfApprovalE.DateText), _
           txtDateOfApplicationB.ID & "=" & IIf(txtDateOfApplicationB.DateText = "____/__/__", "", txtDateOfApplicationB.DateText), _
           txtDateOfApplicationE.ID & "=" & IIf(txtDateOfApplicationE.DateText = "____/__/__", "", txtDateOfApplicationE.DateText), _
            "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))

            'chkOrganName.ID & "=" & chkOrganName.Checked, _

        End If
    End Sub


    Protected Sub gvMain1_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMain1.RowCommand
        If e.CommandName = "Detail" Then
            Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
            btnX.Caption = "返回"
            Me.TransferFramePage("~/OV/OV4001.aspx", New ButtonState() {btnX}, _
          "hiddenType=" + hiddenType.Value, _
          "OTCompID=" + gvMain1.DataKeys(selectedRow(gvMain1))("OTCompID").ToString(), _
          "OTEmpID=" + gvMain1.DataKeys(selectedRow(gvMain1))("OTEmpID").ToString(), _
          "OTStartDate=" + gvMain1.DataKeys(selectedRow(gvMain1))("OTStartDate").ToString(), _
          "OTEndDate=" + gvMain1.DataKeys(selectedRow(gvMain1))("OTEndDate").ToString(), _
          "OTSeq=" + gvMain1.DataKeys(selectedRow(gvMain1))("OTSeq").ToString(), _
          "OTTxnID=" + gvMain1.DataKeys(selectedRow(gvMain1))("OTTxnID").ToString(), _
           ddlType.ID & "=" & ddlType.SelectedValue, _
           ddlOrgType.ID & "=" & ddlOrgType.SelectedValue, _
           ddlDeptID.ID & "=" & ddlDeptID.SelectedValue, _
           ddlOrganID.ID & "=" & ddlOrganID.SelectedValue, _
           ddlWorkStatus.ID & "=" & ddlWorkStatus.SelectedValue, _
           ddlRankIDMIN.ID & "=" & ddlRankIDMIN.SelectedValue, _
           ddlRankIDMAX.ID & "=" & ddlRankIDMAX.SelectedValue, _
           ddlTitleIDMIN.ID & "=" & ddlTitleIDMIN.SelectedValue, _
           ddlTitleIDMAX.ID & "=" & ddlTitleIDMAX.SelectedValue, _
           ddlSex.ID & "=" & ddlSex.SelectedValue, _
           tbOTEmpID.ID & "=" & tbOTEmpID.Text, _
           tbOTEmpName.ID & "=" & tbOTEmpName.Text, _
           ddlPositionID.ID & "=" & ddlPositionID.SelectedValue, _
           ddlWorkType.ID & "=" & ddlWorkType.SelectedValue, _
           txtOvertimeDateB.ID & "=" & IIf(txtOvertimeDateB.DateText = "____/__/__", "", txtOvertimeDateB.DateText), _
           txtOvertimeDateE.ID & "=" & IIf(txtOvertimeDateE.DateText = "____/__/__", "", txtOvertimeDateE.DateText), _
           OTStartTimeH.ID & "=" & OTStartTimeH.SelectedValue, _
           OTStartTimeM.ID & "=" & OTStartTimeM.SelectedValue, _
           OTEndTimeH.ID & "=" & OTEndTimeH.SelectedValue, _
           OTEndTimeM.ID & "=" & OTEndTimeM.SelectedValue, _
           ddlOTTypeID.ID & "=" & ddlOTTypeID.SelectedValue, _
           ddlHolidayOrNot.ID & "=" & ddlHolidayOrNot.SelectedValue, _
           ddlTime.ID & "=" & ddlTime.SelectedValue, _
           ddlOTStatus.ID & "=" & ddlOTStatus.SelectedValue, _
           tbOTFormNO.ID & "=" & tbOTFormNO.Text, _
           tbOTPayDate.ID & "=" & tbOTPayDate.Text, _
           ckOTSalaryPaid.ID & "=" & ckOTSalaryPaid.Checked, _
           ddlIsProcessDate.ID & "=" & ddlIsProcessDate.SelectedValue, _
           txtTakeOfficeDateB.ID & "=" & IIf(txtTakeOfficeDateB.DateText = "____/__/__", "", txtTakeOfficeDateB.DateText), _
           txtTakeOfficeDateE.ID & "=" & IIf(txtTakeOfficeDateE.DateText = "____/__/__", "", txtTakeOfficeDateE.DateText), _
           txtLeaveOfficeDateB.ID & "=" & IIf(txtLeaveOfficeDateB.DateText = "____/__/__", "", txtLeaveOfficeDateB.DateText), _
           txtLeaveOfficeDateE.ID & "=" & IIf(txtLeaveOfficeDateE.DateText = "____/__/__", "", txtLeaveOfficeDateE.DateText), _
           txtDateOfApprovalB.ID & "=" & IIf(txtDateOfApprovalB.DateText = "____/__/__", "", txtDateOfApprovalB.DateText), _
           txtDateOfApprovalE.ID & "=" & IIf(txtDateOfApprovalE.DateText = "____/__/__", "", txtDateOfApprovalE.DateText), _
           txtDateOfApplicationB.ID & "=" & IIf(txtDateOfApplicationB.DateText = "____/__/__", "", txtDateOfApplicationB.DateText), _
           txtDateOfApplicationE.ID & "=" & IIf(txtDateOfApplicationE.DateText = "____/__/__", "", txtDateOfApplicationE.DateText), _
           "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
            'chkOrganName.ID & "=" & chkOrganName.Checked, _
        End If
    End Sub
#End Region



#End Region

#Region "功能"

    ''' <summary>
    ''' ' 裝入頁面資料 到OV_1
    ''' </summary>
    ''' <returns>OV_1</returns>
    ''' <remarks></remarks>
    Private Function SetTableProperty() As OV_1
        Dim OV_1 As New OV_1
        OV_1.ProcessDate = ddlIsProcessDate.SelectedValue
        OV_1.Type = ddlType.SelectedValue
        OV_1.CompID = UserProfile.SelectCompRoleID
        OV_1.OrgType = ddlOrgType.SelectedValue
        OV_1.DeptID = ddlDeptID.SelectedValue



        If Not IsNumeric(OTStartTimeH.SelectedValue) And IsNumeric(OTStartTimeM.SelectedValue) Then
            OV_1.OTStartTime = "00" + OTStartTimeM.SelectedValue
        ElseIf IsNumeric(OTStartTimeH.SelectedValue) And Not IsNumeric(OTStartTimeM.SelectedValue) Then
            OV_1.OTStartTime = OTStartTimeH.SelectedValue + "00"
        Else
            OV_1.OTStartTime = OTStartTimeH.SelectedValue + OTStartTimeM.SelectedValue
        End If

        OV_1.Time = ddlTime.SelectedValue



        If Not IsNumeric(OTEndTimeH.SelectedValue) And IsNumeric(OTEndTimeM.SelectedValue) Then
            OV_1.OTEndTime = "23" + OTStartTimeM.SelectedValue
        ElseIf IsNumeric(OTEndTimeH.SelectedValue) And Not IsNumeric(OTEndTimeM.SelectedValue) Then
            OV_1.OTEndTime = OTEndTimeH.SelectedValue + "59"
        Else
            OV_1.OTEndTime = OTEndTimeH.SelectedValue + OTEndTimeM.SelectedValue
        End If

        OV_1.OrganID = ddlOrganID.SelectedValue
        OV_1.RankIDMIN = ddlRankIDMIN.SelectedValue
        OV_1.RankIDMAX = ddlRankIDMAX.SelectedValue
        '20170219 Beatrice mod
        OV_1.TitleIDMIN = ddlTitleIDMIN.SelectedValue
        OV_1.TitleIDMAX = ddlTitleIDMAX.SelectedValue
        'Dim ddlTitleNameItem = ddlTitleName.SelectedIndex
        'If ddlTitleName.SelectedValue.Equals("") Then
        '    OV_1.TitleID = ""
        '    OV_1.TitleName = ""
        'Else
        '    OV_1.TitleID = ddlTitleName.SelectedItem.Text.Split("-")(0).ToString
        '    OV_1.TitleName = ddlTitleName.SelectedItem.Text.Split("-")(1).ToString
        'End If

        OV_1.Sex = ddlSex.SelectedValue
        OV_1.PositionID = ddlPositionID.SelectedValue
        OV_1.WorkType = ddlWorkType.SelectedValue
        OV_1.OTTypeID = ddlOTTypeID.SelectedValue
        OV_1.HolidayOrNot = ddlHolidayOrNot.SelectedValue
        OV_1.OTStatus = ddlOTStatus.SelectedValue

        OV_1.WorkStatus = ddlWorkStatus.SelectedValue
        OV_1.OTEmpID = tbOTEmpID.Text
        OV_1.OTEmpName = tbOTEmpName.Text
        OV_1.OTFormNO = tbOTFormNO.Text
        OV_1.OTPayDate = tbOTPayDate.Text
        OV_1.OvertimeDateB = txtOvertimeDateB.DateText
        OV_1.OvertimeDateE = txtOvertimeDateE.DateText
        OV_1.TakeOfficeDateB = txtTakeOfficeDateB.DateText
        OV_1.TakeOfficeDateE = txtTakeOfficeDateE.DateText
        OV_1.LeaveOfficeDateB = txtLeaveOfficeDateB.DateText
        OV_1.LeaveOfficeDateE = txtLeaveOfficeDateE.DateText
        OV_1.DateOfApprovalB = txtDateOfApprovalB.DateText
        OV_1.DateOfApprovalE = txtDateOfApprovalE.DateText
        OV_1.DateOfApplicationB = txtDateOfApplicationB.DateText
        OV_1.DateOfApplicationE = txtDateOfApplicationE.DateText
        OV_1.OTSalaryOrAdjust = ddlSalaryOrAdjust.SelectedValue
        If ckOTSalaryPaid.Checked Then
            OV_1.OTSalaryPaid = "1"
        Else
            OV_1.OTSalaryPaid = ""
        End If
        Return OV_1
    End Function

#Region "資料檢核"

    ''' <summary>
    ''' 記薪用資料檢核
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>errorMsg</remarks>
    Private Function checkData() As String
        Dim errorMsg As String = ""
        If "-1".Equals(selectedRow(gvMain).ToString.Trim) And "-1".Equals(selectedRow(gvMain1).ToString.Trim) Then
            errorMsg = "請選擇資料"
            Return errorMsg
        End If
        'overtime 需要計薪 看overtime
        If (Not gvMain1.DataKeys(selectedRow(gvMain1))("OTSalaryPaid").Equals("1")) Then
            errorMsg = "此資料未計薪"
            Return errorMsg
        End If
        If (gvMain1.DataKeys(selectedRow(gvMain1))("OTStatus").Equals("計薪後收回")) Then
            errorMsg = "此資料已收回"
            Return errorMsg
        End If

        Return errorMsg
    End Function

    ''' <summary>
    ''' 做廢用資料檢核
    ''' </summary>
    ''' <returns>errorMsg</returns>
    ''' <remarks></remarks>
    Private Function checkData1() As String
        Dim errorMsg As String = ""
        If "-1".Equals(selectedRow(gvMain).ToString.Trim) And "-1".Equals(selectedRow(gvMain1).ToString.Trim) Then
            errorMsg = "請選擇資料"
            Return errorMsg
        End If
        If (gvMain1.DataKeys(selectedRow(gvMain1))("OTStatus").Equals("作廢")) Then
            errorMsg = "此資料已作廢"
            Return errorMsg
        End If
        If (Not gvMain1.DataKeys(selectedRow(gvMain1))("OTStatus").Equals("核准")) Then
            errorMsg = "此資料狀態非核准"
            Return errorMsg
        End If
        Return errorMsg
    End Function

    ''' <summary>
    ''' 檢核日期
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

        '到職日期
        checkData = funCheckDate(txtTakeOfficeDateB, txtTakeOfficeDateE, labTakeOfficeDate.Text)
        If Not checkData Then
            Return False
        End If
        '離職日期
        checkData = funCheckDate(txtLeaveOfficeDateB, txtLeaveOfficeDateE, LeaveOfficeDate.Text)
        If Not checkData Then
            Return False
        End If
        '核准日期
        checkData = funCheckDate(txtDateOfApprovalB, txtDateOfApprovalE, labDateOfApproval.Text)
        If Not checkData Then
            Return False
        End If
        '申請日期
        checkData = funCheckDate(txtDateOfApplicationB, txtDateOfApplicationE, labDateOfApplication.Text)
        If Not checkData Then
            Return False
        End If
        If lblRankNotice.Visible Then
            Bsp.Utility.ShowFormatMessage(Me, "H_00000", "職等選擇請由小到大")
            Return False
        End If
        '20170219 Beatrice Add
        If lblTitleNotice.Visible Then
            Bsp.Utility.ShowFormatMessage(Me, "H_00000", "職稱選擇請由小到大")
            Return False
        End If
        Return checkData
    End Function

    ''' <summary>
    ''' 查詢用檢核資料
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


#Region "單位類別部門用function"

    ''' <summary>
    '''  產出DataTable並放入GridView
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setDataForGridView()
        Dim OV_1 = SetTableProperty()
        Dim dt As DataTable = OV_1.getTable()


        ViewState.Item("DoQuery") = "Y"
        If OV_1.Type.Equals("bef") Then
            div_tb.Style.Add("display", "block")
            div_tb1.Style.Add("display", "none")
            pcMain.DataTable = dt
            gvMain.DataBind()
            gvMain.Visible = True
            ViewState.Item("DoQuery") = "Y"
        Else
            Dim array As ArrayList = New ArrayList()
            div_tb.Style.Add("display", "none")
            div_tb1.Style.Add("display", "block")

            '時段ddl  ddlTime
            If dt.Rows.Count > 0 Then
                If "1".Equals(OV_1.Time) Then

                    For Each item As DataRow In dt.Rows
                        If (item("Time_one").ToString()).Equals("0") Or (item("Time_one").ToString()).Equals("_") Then
                            array.Add(item)
                        End If
                    Next
                    For Each item As DataRow In array
                        dt.Rows.Remove(item)
                    Next

                ElseIf "2".Equals(OV_1.Time) Then
                    For Each item As DataRow In dt.Rows
                        If (item("Time_two").ToString()).Equals("0") Or (item("Time_two").ToString()).Equals("_") Then
                            array.Add(item)
                        End If
                    Next
                    For Each item As DataRow In array
                        dt.Rows.Remove(item)
                    Next

                ElseIf "3".Equals(OV_1.Time) Then
                    For Each item As DataRow In dt.Rows
                        If (item("Time_three").ToString()).Equals("0") Or (item("Time_three").ToString()).Equals("_") Then
                            array.Add(item)
                        End If
                    Next
                    For Each item As DataRow In array
                        dt.Rows.Remove(item)
                    Next
                End If
            End If
            pcMain1.DataTable = dt
            gvMain1.DataBind()
            gvMain1.Visible = True
            ViewState.Item("DoQuery") = "Y"
        End If
    End Sub

    ''' <summary>
    ''' 產出部門dropDownList
    ''' </summary>
    ''' <param name="objDDL"></param>
    ''' <param name="strCompID"></param>
    ''' <remarks></remarks>
    Private Sub subLoadOrganColor(ByVal objDDL As DropDownList, ByVal strCompID As String)
        Dim strSQL As New StringBuilder
        strSQL.AppendLine("Select O.OrganID")
        'strSQL.AppendLine(", Case When OrgType = DeptID Then OrganID + '-' + OrganName  Else '　　' + OrganID + '-' + OrganName + '-' + RoleCode End OrganName")
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
    '''  產出單位類別dropDownList
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
    ''' 更改單位類別
    ''' 部門顏色
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
#End Region



#End Region

#Region "資料變更事件"

#Region "DropDownList變更事件"

    Protected Sub ddlType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlType.SelectedIndexChanged
        If Not pcMain.DataTable Is Nothing Then

            pcMain.DataTable.Clear()
            pcMain.BindGridView()

        End If
        If Not pcMain1.DataTable Is Nothing Then
            pcMain1.DataTable.Clear()
            pcMain1.BindGridView()
        End If

        If ddlType.SelectedValue.Equals("bef") Then
            ddlOTStatus.Items.Clear()
            ddlOTStatus.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
            ddlOTStatus.Items.Insert(1, New ListItem("暫存", "1"))
            ddlOTStatus.Items.Insert(2, New ListItem("送簽", "2"))
            ddlOTStatus.Items.Insert(3, New ListItem("核准", "3"))
            ddlOTStatus.Items.Insert(4, New ListItem("駁回", "4"))
            ddlOTStatus.Items.Insert(5, New ListItem("刪除", "5"))
            ddlOTStatus.Items.Insert(6, New ListItem("取消", "9"))
        Else
            ddlOTStatus.Items.Clear()
            ddlOTStatus.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
            ddlOTStatus.Items.Insert(1, New ListItem("暫存", "1"))
            ddlOTStatus.Items.Insert(2, New ListItem("送簽", "2"))
            ddlOTStatus.Items.Insert(3, New ListItem("核准", "3"))
            ddlOTStatus.Items.Insert(4, New ListItem("駁回", "4"))
            ddlOTStatus.Items.Insert(5, New ListItem("刪除", "5"))
            ddlOTStatus.Items.Insert(6, New ListItem("取消", "6"))
            ddlOTStatus.Items.Insert(7, New ListItem("作廢", "7"))
            ddlOTStatus.Items.Insert(8, New ListItem("計薪後收回", "9"))
        End If

        tbOTPayDateErrorMsg.Visible = False
        If ddlType.SelectedValue.Equals("bef") Then
            ddlTime.SelectedIndex = 0
            ddlIsProcessDate.SelectedIndex = 0
            tbOTPayDate.Text = ""
            ddlTime.Enabled = False
            tbOTPayDate.Enabled = False
            ddlIsProcessDate.Enabled = False
            ckOTSalaryPaid.Enabled = False
        Else
            tbOTPayDate.Text = ""
            ddlTime.Enabled = True
            tbOTPayDate.Enabled = True
            ddlIsProcessDate.Enabled = True
            ckOTSalaryPaid.Enabled = True
        End If
        ckOTSalaryPaid.Checked = False
    End Sub

    Protected Sub ddlRankIDMIN_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlRankIDMIN.SelectedIndexChanged
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

    Protected Sub ddlDept_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDeptID.SelectedIndexChanged
        If ddlDeptID.SelectedValue = "" Then
            Bsp.Utility.FillDDL(ddlOrganID, "eHRMSDB", "Organization", "RTRIM(OrganID)", "OrganName  ", Bsp.Utility.DisplayType.Full, "", " And VirtualFlag = '0' And CompID = " & Bsp.Utility.Quote(UserProfile.SelectCompRoleID), "Order By OrganID")
            ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
        Else
            Bsp.Utility.FillDDL(ddlOrganID, "eHRMSDB", "Organization", "RTRIM(OrganID)", "OrganName  ", Bsp.Utility.DisplayType.Full, "", " And VirtualFlag = '0' And CompID = " & Bsp.Utility.Quote(UserProfile.SelectCompRoleID) & " And DeptID = " & Bsp.Utility.Quote(ddlDeptID.SelectedValue), "Order By OrganID")
            ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
        End If

        Dim str As String = "join " + eHRMSDB_ITRD + ".[dbo].EmpPosition EP ON PO.CompID=EP.CompID AND PO.PositionID=EP.PositionID AND EP.PrincipalFlag='1' join " + eHRMSDB_ITRD + ".[dbo].Personal P ON EP.CompID=P.CompID AND EP.EmpID=P.EmpID  join " + eHRMSDB_ITRD + ".[dbo].Organization ORT ON  P.OrganID=ORT.OrganID AND P.CompID =ORT.CompID"

        If (Not ddlOrganID.SelectedValue.ToString.Trim.Equals("")) Then
            Bsp.Utility.FillDDL(ddlPositionID, "eHRMSDB", "Position PO", "distinct PO.PositionID", "PO.Remark", Bsp.Utility.DisplayType.Full, str, "And PO.CompID =" + Bsp.Utility.Quote(UserProfile.SelectCompRoleID) + " and ORT.OrganID= '" + ddlOrganID.SelectedValue + "'", "Order By  PO.PositionID") '職位
            ddlPositionID.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        ElseIf (Not ddlDeptID.SelectedValue.ToString.Trim.Equals("")) Then
            Bsp.Utility.FillDDL(ddlPositionID, "eHRMSDB", "Position PO", "distinct PO.PositionID", "PO.Remark", Bsp.Utility.DisplayType.Full, str, "And PO.CompID = " + Bsp.Utility.Quote(UserProfile.SelectCompRoleID) + " and P.DeptID= '" + ddlDeptID.SelectedValue + "'", "Order By  PO.PositionID") '職位
            ddlPositionID.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        ElseIf (Not ddlOrgType.SelectedValue.ToString.Trim.Equals("")) Then
            Bsp.Utility.FillDDL(ddlPositionID, "eHRMSDB", "Position PO", "distinct PO.PositionID", "PO.Remark", Bsp.Utility.DisplayType.Full, str, "And PO.CompID = " + Bsp.Utility.Quote(UserProfile.SelectCompRoleID) + "and ORT.OrgType= '" + ddlOrgType.SelectedValue + "'", "Order By  PO.PositionID") '職位
            ddlPositionID.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        Else
            Bsp.Utility.FillDDL(ddlPositionID, "eHRMSDB", "Position PO", "distinct PO.PositionID", "PO.Remark", Bsp.Utility.DisplayType.Full, str, "And PO.CompID = " + Bsp.Utility.Quote(UserProfile.SelectCompRoleID), "Order By  PO.PositionID") '職位
            ddlPositionID.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        End If
    End Sub

    Protected Sub tbOTPayDate_TextChanged(sender As Object, e As System.EventArgs) Handles tbOTPayDate.TextChanged
        If Regex.IsMatch(tbOTPayDate.Text, "^\d{6}$") Or "".Equals(tbOTPayDate.Text.Trim) Then
            tbOTPayDateErrorMsg.Visible = False
        Else
            tbOTPayDateErrorMsg.Visible = True
        End If
    End Sub

    Protected Sub ddlOrgType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlOrgType.SelectedIndexChanged
        If Not ddlOrgType.SelectedValue = "" Then
            subLoadOrganColor(ddlDeptID, UserProfile.SelectCompRoleID, ddlOrgType.SelectedValue)
            Bsp.Utility.FillDDL(ddlOrganID, "eHRMSDB", "Organization", "RTRIM(OrganID)", "OrganName  ", Bsp.Utility.DisplayType.Full, "", " And VirtualFlag = '0' And CompID = " & Bsp.Utility.Quote(UserProfile.SelectCompRoleID) & " AND ((OrganID = OrganID AND OrganID <> DeptID AND OrganID <> OrgType)OR(OrganID = OrganID AND OrganID = DeptID AND OrganID = OrgType)) ", "Order By OrganID")
            ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
        Else
            subLoadOrganColor(ddlDeptID, UserProfile.SelectCompRoleID)
            If ddlDeptID.SelectedValue = "" Then
                Bsp.Utility.FillDDL(ddlOrganID, "eHRMSDB", "Organization", "RTRIM(OrganID)", "OrganName  ", Bsp.Utility.DisplayType.Full, "", " And VirtualFlag = '0' And CompID = " & Bsp.Utility.Quote(UserProfile.SelectCompRoleID) & " AND ((OrganID = OrganID AND OrganID <> DeptID AND OrganID <> OrgType)OR(OrganID = OrganID AND OrganID = DeptID AND OrganID = OrgType)) ", "Order By OrganID")
                ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
            Else
                Bsp.Utility.FillDDL(ddlOrganID, "eHRMSDB", "Organization", "RTRIM(OrganID)", "OrganName  ", Bsp.Utility.DisplayType.Full, "", " And VirtualFlag = '0' And CompID = " & Bsp.Utility.Quote(UserProfile.SelectCompRoleID) & " AND ((OrganID = OrganID AND OrganID <> DeptID AND OrganID <> OrgType)OR(OrganID = OrganID AND OrganID = DeptID AND OrganID = OrgType)) " & " And DeptID = " & Bsp.Utility.Quote(ddlDeptID.SelectedValue), "Order By OrganID")
                ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
            End If
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
            Bsp.Utility.FillDDL(ddlPositionID, "eHRMSDB", "Position PO", "distinct PO.PositionID", "PO.Remark", Bsp.Utility.DisplayType.Full, str, "And PO.CompID =" + Bsp.Utility.Quote(UserProfile.SelectCompRoleID), "Order By  PO.PositionID") '職位
            ddlPositionID.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        End If
    End Sub

    Protected Sub ddlOrganID_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlOrganID.SelectedIndexChanged
        Dim str As String = "join " + eHRMSDB_ITRD + ".[dbo].EmpPosition EP ON PO.CompID=EP.CompID AND PO.PositionID=EP.PositionID AND EP.PrincipalFlag='1' join " + eHRMSDB_ITRD + ".[dbo].Personal P ON EP.CompID=P.CompID AND EP.EmpID=P.EmpID  join " + eHRMSDB_ITRD + ".[dbo].Organization ORT ON  P.OrganID=ORT.OrganID AND P.CompID =ORT.CompID"
        If (Not ddlOrganID.SelectedValue.ToString.Trim.Equals("")) Then
            Bsp.Utility.FillDDL(ddlPositionID, "eHRMSDB", "Position PO", "distinct PO.PositionID", "PO.Remark", Bsp.Utility.DisplayType.Full, str, "And PO.CompID = " + Bsp.Utility.Quote(UserProfile.SelectCompRoleID) + " and ORT.OrganID= '" + ddlOrganID.SelectedValue + "'", "Order By  PO.PositionID") '職位
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

#End Region
    ''' <summary>
    ''' 快速搜索員工編號事件
    ''' </summary>
    ''' <param name="returnValue"></param>
    ''' <remarks></remarks>
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



End Class

