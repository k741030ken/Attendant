'****************************************************
'功能說明：流程關卡處理主程式
'建立人員：Chung
'建立日期：2013/01/29
'****************************************************
Imports System.Data.SqlClient
Imports System.Data
Imports System.Data.Common

Partial Class WF_WFA020
    Inherits PageBase

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack Then
            Try
                DoInit(ti)
            Catch ex As Exception
                If TypeOf ex Is SqlException Then
                    '判斷若為Deadlock, 重跑一次
                    If CType(ex, SqlException).Number = 1205 Then
                        Bsp.Utility.WriteLog(Me.FunID & ".BaseOnPageTransfer:" & String.Format("Deadlock:重新發動交易...UserID={0}", UserProfile.UserID))

                        DoInit(ti)
                        Return
                    End If
                End If
                Bsp.Utility.ShowMessage(Me, Me.FunID & ".BaseOnPageTransfer", ex)
                Bsp.Utility.RunClientScript(Me, "GetMessagePage('流程處理','ERROR'," & Bsp.Utility.Quote(Server.UrlEncode(ex.Message)) & ", '', '');")

            End Try
        End If

    End Sub

    Private Sub DoInit(ByVal ti As TransferInfo)
        Dim RtnMsg As String = ""
        Dim objWF As New WF()

        Bsp.Utility.RunClientScript(Me, "openWFMenu()")

        If ti.Args.Length > 0 Then Object2ViewState(ti.Args)

        Try
            ViewState.Item("FlowVer") = GetFlowVer(ViewState.Item("FlowID").ToString(), ViewState.Item("FlowCaseID").ToString())
        Catch ex As Exception
            ViewState.Item("FlowVer") = "1"
        End Try

        '取得ToDoList內的參數
        GetToDoInfo()

        LabFlowCaseDesc.Text = "  流程資訊：[ " & ViewState.Item("FlowID") & "-" & ViewState.Item("FlowCaseID") & "-" & ViewState.Item("FlowLogBatNo") & "-" & ViewState.Item("FlowLogID") & " ]"
        lab_FLowStepDesc.Text = "[" & ViewState.Item("FlowStepID") & "-" & ViewState.Item("FlowStepDesc") & "]"

        '讀取待辦資訊
        Using dt As DataTable = objWF.GetFlowToDoInfo(ViewState.Item("FlowID"), ViewState.Item("FlowCaseID"), ViewState.Item("FlowLogID"))
            gvMain.DataSource = dt
            gvMain.DataBind()
        End Using

        LabShowFlowLog.Text = "<font style=""color:blue; cursor:hand;"" onclick=""funShowFlowDetail();"">◎<u>查閱詳細流程紀錄</u>◎</font>"
        '載入上一關意見
        setLastOpinion()
        '載入操作提示
        setStepIntimation()
        '取得目前關卡需顯示的按鈕及下一關處理人員
        Dim Params() As String = New String() {}
        For Each strKey As String In ViewState.Keys
            If TypeOf ViewState.Item(strKey) Is String OrElse TypeOf ViewState.Item(strKey) Is Integer OrElse TypeOf ViewState.Item(strKey) Is Decimal OrElse TypeOf ViewState.Item(strKey) Is Double Then
                Array.Resize(Params, Params.GetLength(0) + 1)
                Params(Params.GetUpperBound(0)) = String.Format("{0}={1}", strKey, ViewState.Item(strKey))
            End If
        Next

        Using dt As DataTable = objWF.GetCurrentStepInfo( _
                ViewState.Item("FlowID").ToString(), ViewState.Item("FlowVer").ToString(), ViewState.Item("FlowStepID").ToString(), RtnMsg, Params)

            If RtnMsg <> "" Then
                Bsp.Utility.ShowMessage(Me, RtnMsg)
                'Return
            End If

            dlNextStep.DataSource = dt
            dlNextStep.DataBind()
        End Using

        '載入片語設定
        setPhrase()
        '取得暫存意見
        TxtAreaOption.Value = objWF.GetFlowOpinTemp(ViewState.Item("FlowID").ToString(), ViewState.Item("FlowCaseID").ToString(), ViewState.Item("FlowLogID").ToString())
    End Sub

    Private Function GetFlowVer(ByVal FlowID As String, ByVal FlowCaseID As String) As String
        Dim objWF As New WF()

        Using dt As DataTable = objWF.GetFlowCase(FlowID, FlowCaseID)
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item("FlowVer").ToString()
            Else
                Return "1"
            End If
        End Using
    End Function

    Private Sub GetToDoInfo()
        Dim objWF As New WF()

        Using dt As DataTable = objWF.GetFlowToDoList(ViewState.Item("FlowID"), ViewState.Item("FlowCaseID"), ViewState.Item("FlowLogID"))
            If dt.Rows.Count > 0 Then
                Dim beToDoList As New beWF_FlowToDoList.Row(dt.Rows(0))

                ViewState.Item("FlowStepDesc") = beToDoList.FlowStepDesc.Value
                ViewState.Item("FlowStepID") = beToDoList.FlowStepID.Value
                ViewState.Item("FlowKeyValue") = beToDoList.FlowKeyValue.Value
                ViewState.Item("FlowCaseCrBr") = beToDoList.CrBr.Value
                ViewState.Item("FromDate") = beToDoList.FromDate.Value.ToString("yyyy/MM/dd HH:mm:ss")
                ViewState.Item("FlowLogBatNo") = beToDoList.FlowLogBatNo.Value

                Object2ViewState(beToDoList.FlowKeyValue.Value)
            End If
        End Using
        '檢核是否為此關人員執行人員
        If ViewState.Item("FlowLogID") IsNot Nothing Then
            If Not objWF.CheckFlowLogCurrUser(ViewState.Item("FlowID"), ViewState.Item("FlowCaseID"), ViewState.Item("FlowLogID"), UserProfile.UserID) Then
                Throw New Exception("非目前關卡處理人員，請查詢流程清單確認!")
            End If
        End If
    End Sub

    Private Sub setLastOpinion()
        Dim objWF As New WF

        Using dt As DataTable = objWF.GetFlowFullLog(ViewState.Item("FlowID"), ViewState.Item("FlowCaseID"), (CInt(ViewState.Item("FlowLogBatNo")) - 1).ToString, True)
            If dt.Rows.Count > 0 Then
                LabPreStepDesc.Text = dt.Rows(0).Item("FlowStepDesc").ToString
                ViewState.Item("PreFlowStepID") = dt.Rows(0).Item("FlowStepDesc").ToString.Split("-")(0).ToString().Trim()
                LabPreStepAction.Text = dt.Rows(0).Item("FlowStepAction").ToString()
                ViewState.Item("PreStepAction") = LabPreStepAction.Text.ToString().Trim()
                LabPreStepActUser.Text = dt.Rows(0).Item("ToUserName").ToString() & dt.Rows(0).Item("ProxyStr").ToString()
                If IsDBNull(dt.Rows(0).Item("UpdDate")) Then
                    LabPreStepTime.Text = ""
                Else
                    LabPreStepTime.Text = Convert.ToDateTime(dt.Rows(0).Item("UpdDate")).ToString("yyyy/MM/dd HH:mm:ss")
                End If

                Dim FlowStepOpinion As String = ""
                If dt.Rows(0).Item("LogRemark").ToString() <> "" Then
                    FlowStepOpinion = dt.Rows(0).Item("FlowStepOpinion").ToString() & "<BR>" & "(" & dt.Rows(0).Item("LogRemark").ToString() & ")"
                Else
                    FlowStepOpinion = dt.Rows(0).Item("FlowStepOpinion").ToString
                End If
                LabPreStepOpin.Text = FlowStepOpinion.Replace(Chr(10), "<BR>")
            End If
        End Using
    End Sub

    Private Sub setStepIntimation()
        Dim objWF As New WF()

        Using dt As DataTable = objWF.GetFlowStepM(ViewState.Item("FlowID"), ViewState.Item("FlowStepID"), ViewState.Item("FlowVer"))
            If dt.Rows.Count > 0 Then
                If dt.Rows(0).Item("Intimation").ToString().Trim() <> "" Then
                    LabOpin.Text = dt.Rows(0).Item("Intimation").ToString
                Else
                    DivOpin.Visible = False
                End If
            End If
        End Using
    End Sub

    Private Sub setPhrase()
        Dim objWF As New WF

        Using dt As DataTable = objWF.GetStepPhrase(ViewState.Item("FlowID").ToString(), ViewState.Item("FlowStepID").ToString(), UserProfile.UserID)
            FlowPhraseDrpList.DataSource = dt
            FlowPhraseDrpList.DataBind()
            FlowPhraseDrpList.Items.Insert(0, New ListItem("", ""))
        End Using
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnRefresh.Style.Add("display", "none")
        If Not IsPostBack Then
            'If ViewState.Item("FlowLogID") IsNot Nothing Then
            '    lab_FLowStepDesc.Text = "[" & ViewState.Item("FlowStepID") & "-" & ViewState.Item("FlowStepDesc") & "]"
            '    LabShowFlowLog.Text = "<a href='/Intranet/ezFlow/FlowLogList.asp?FlowID=" & ViewState.Item("FlowID") & "&FlowLogID=" & ViewState.Item("FlowLogID") & "' target='_blank'>＊查閱詳細的流程記錄＊</a>"
            '    dlNextStep.DataSource = ViewState.Item("AssignTable")
            '    dlNextStep.DataBind()
            'End If

            'If StateMain IsNot Nothing Then
            '    sdsMain.SelectCommand = StateMain
            'End If
        End If
    End Sub

    Protected Sub dlNextStep_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlNextStep.ItemDataBound
        Dim objbtnAction As Button
        Dim objddlUserList As DropDownList
        Dim objlblDesc As Label
        Dim objlblMustOpin As Label
        Dim objlblBeforeUrl As HiddenField
        Dim objlblFinalOpin As HiddenField
        Dim objlblFlowStepMail As HiddenField
        Dim objtxtUserList As TextBox
        Dim objWF As New WF()
        Dim checkList As String = "" '若有檢核事項，需串出。

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            objbtnAction = e.Item.FindControl("btnAction")
            objbtnAction.Text = DataBinder.Eval(e.Item.DataItem, "btnActionName")
            objlblDesc = e.Item.FindControl("lblDesc")
            objlblDesc.Text = DataBinder.Eval(e.Item.DataItem, "DescText")
            objddlUserList = e.Item.FindControl("ddlUserList")
            objtxtUserList = e.Item.FindControl("txtUserList")
            objlblMustOpin = e.Item.FindControl("lblMustOpin")
            objlblBeforeUrl = e.Item.FindControl("lblBeforeUrl")
            objlblFinalOpin = e.Item.FindControl("lblFinalOpin")
            objlblFlowStepMail = e.Item.FindControl("lblFlowStepMail")
            objlblFlowStepMail.Value = DataBinder.Eval(e.Item.DataItem, "FlowStepMail")

            Dim UserList As String = DataBinder.Eval(e.Item.DataItem, "UserList").ToString
            If UserList.IndexOf(",") > 0 Then
                Dim aryUserList() As String = UserList.Split(",")
                objddlUserList.Items.Add("---請選擇---")
                For intLoop As Integer = 0 To aryUserList.GetUpperBound(0)
                    objddlUserList.Items.Add(aryUserList(intLoop))
                Next
                objtxtUserList.Visible = False
            Else
                objtxtUserList.Text = UserList
                objtxtUserList.ReadOnly = True
                objddlUserList.Visible = False
            End If

            Dim FlowStepBtnReq As String = DataBinder.Eval(e.Item.DataItem, "FlowStepBtnReqList").ToString.Trim
            objbtnAction.Attributes.Add("onclick", "return StepCheck(this,'" & FlowStepBtnReq & "','" & objbtnAction.Text & "','" & checkList & "');")
            If FlowStepBtnReq = "Y" Then
                objlblMustOpin.Text = "<font color=red>＊</font>"
            Else
                objlblMustOpin.Text = "　"
            End If

            '按下按鈕送出時，有必須先處理之網頁
            objlblBeforeUrl.Value = GetBeforeURL(ViewState.Item("FlowStepID"), objlblDesc.Text.ToString.Split("-")(0).ToUpper, objbtnAction.Text)
        End If
    End Sub

    Private Function GetBeforeURL(ByVal CurrentFlowStepID As String, ByVal NextFlowStepID As String, ByVal FlowStepAction As String) As String
        Dim strURL As String = ""
        Dim objWF As New WF()

        Select Case NextFlowStepID
            Case "S999"
                Using dt As DataTable = objWF.GetAP_Profile(ViewState.Item("AppID").ToString(), "AppType")
                    If dt.Rows.Count > 0 AndAlso dt.Rows(0).Item("AppType").ToString() <> "2" Then
                        strURL = "?FunID=AL0600&Path=" & ResolveUrl("~/AL/AL0600.aspx") & "?AppID=" & ViewState.Item("AppID").ToString()
                    End If
                End Using
        End Select

        Return strURL
    End Function

    Protected Sub dlNextStep_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlNextStep.ItemCommand '流程送出之作業
        Dim objWF As New WF()
        Dim objbtnAction As Button
        Dim objddlUserList As DropDownList
        Dim objlblDesc As Label
        Dim objtxtUserList As TextBox
        Dim TraceFlag As String
        Dim FlowDispatchFlag As String
        Dim AssignTo As String
        Dim AssingToStr As String
        Dim objSendMail As HiddenField
        Dim FlowOption As String = TxtAreaOption.Value.Trim()
        Dim ExtraOpinion As String = ""

        Select Case e.CommandName
            Case "UcSubmit"

                objbtnAction = e.Item.FindControl("btnAction")
                objlblDesc = e.Item.FindControl("lblDesc")
                objSendMail = e.Item.FindControl("lblFlowStepMail")

                '*********************************************************
                '取得給予待辦事項的名單
                '*********************************************************
                If e.Item.FindControl("ddlUserList").Visible Then
                    objddlUserList = e.Item.FindControl("ddlUserList")
                    AssignTo = objddlUserList.SelectedItem.Value.ToString.Split("-")(0)
                    AssingToStr = objddlUserList.SelectedItem.Value.ToString()
                    If AssignTo.Trim() = "" Then
                        Bsp.Utility.ShowMessage(Me, "請選取處理人員！")
                        Return
                    End If
                Else
                    objtxtUserList = e.Item.FindControl("txtUserList")
                    AssignTo = objtxtUserList.Text.ToString.Split("-")(0)
                    AssingToStr = objtxtUserList.Text.ToString()
                End If

                '取得是否寫入追蹤
                If CkboxTrace.Checked.ToString Then
                    TraceFlag = "Y"
                Else
                    TraceFlag = "N"
                End If

                '取得急件註記
                If CkFlowDispatchFlag.Checked.ToString Then
                    FlowDispatchFlag = "Y"
                Else
                    FlowDispatchFlag = "N"
                End If

                '*********************************************************
                '流程處理前檢核
                '*********************************************************
                Try
                    '判斷是否為目前關卡處理人員, 若為核准後撤件則不需檢查
                    If Not objWF.CheckFlowLogCurrUser(ViewState.Item("FlowID"), ViewState.Item("FlowCaseID"), ViewState.Item("FlowLogID"), UserProfile.UserID) Then
                        Throw New Exception("非目前關卡處理人員，請查詢流程清單確認!")
                    End If
                Catch ex As Exception
                    Bsp.Utility.ShowMessage(Me, Me.FunID & ".ItemCommand.CheckData", ex)
                    Me.TransferFramePage("~/WF/WFA021.aspx", Nothing, "MessageType=ERROR", "InforPath=流程處理", "Message=" & Server.UrlEncode(ex.ToString))
                    Return
                End Try

                '蒐集所有參數
                Dim AllParams() As String = New String() {}
                For Each strKey As String In ViewState.Keys
                    If TypeOf ViewState.Item(strKey) Is String Then
                        Array.Resize(AllParams, AllParams.GetLength(0) + 1)
                        AllParams(AllParams.GetUpperBound(0)) = String.Format("{0}={1}", strKey, ViewState.Item(strKey))
                    End If
                Next

                '加入AssignTo
                Array.Resize(AllParams, AllParams.GetLength(0) + 1)
                AllParams(AllParams.GetUpperBound(0)) = String.Format("AssignTo={0}", AssignTo)

                Using cn As DbConnection = Bsp.DB.getConnection()
                    cn.Open()
                    Dim tran As DbTransaction = cn.BeginTransaction
                    Try
                        '*********************************************************
                        '流程送出之作業
                        '*********************************************************
                        objWF.GoToNextStep( _
                            ViewState.Item("FlowID"), ViewState.Item("FlowVer"), ViewState.Item("FlowCaseID"), _
                            ViewState.Item("FlowStepID"), ViewState.Item("FlowLogBatNo"), ViewState.Item("FlowLogID"), _
                            objbtnAction.Text, ExtraOpinion & FlowOption, UserProfile.ActUserID, _
                            AssignTo, "", objlblDesc.Text.ToString.Split("-")(0), _
                            TraceFlag, "", FlowDispatchFlag, tran)


                        '*********************************************************
                        '流程送出後，之後續作業
                        '*********************************************************
                        Select Case ViewState.Item("FlowStepID").ToString()
                            Case "S002"
                                objWF.UpdCurrLoanCheckActOpin(ViewState.Item("FlowCaseID").ToString(), ViewState.Item("FlowLogID").ToString(), UserProfile.UserID, objbtnAction.Text.Trim(), TxtAreaOption.Value.ToString(), ViewState.Item("FlowStepID").ToString(), tran)
                            Case "S004"
                                objWF.UpdCurrLoanCheckActOpin(ViewState.Item("FlowCaseID").ToString(), ViewState.Item("FlowLogID").ToString(), UserProfile.UserID, objbtnAction.Text.Trim(), TxtAreaOption.Value.ToString(), ViewState.Item("FlowStepID").ToString(), tran)
                                'Case "DD01"
                                '    Array.Resize(AllParams, AllParams.GetLength(0) + 1)
                                '    AllParams(AllParams.GetUpperBound(0)) = String.Format("AssignTo={0}", AssignTo)
                            Case Else
                        End Select
                        objWF.FlowStepSubmitAction(ViewState.Item("FlowID"), ViewState.Item("FlowVer"), ViewState.Item("FlowStepID"), objbtnAction.Text.ToString.Trim(), ViewState.Item("FlowCaseID"), tran, AllParams)

                        If objSendMail.Value.Trim() = "Y" Then
                            SendFlowMail(ViewState.Item("FlowID").ToString(), ViewState.Item("FlowCaseID").ToString(), _
                                         objlblDesc.Text.Trim(), objbtnAction.Text.Trim(), AssignTo, AssingToStr, tran)
                        End If

                        tran.Commit()

                        '*********************************************************
                        '執行後之訊息
                        '*********************************************************
                        ShowFlowMessage(objbtnAction.Text.ToString & "->[" & objlblDesc.Text & "]", AssingToStr)

                    Catch ex As Exception
                        tran.Rollback()
                        Bsp.Utility.ShowMessage(Me, Me.FunID & ".ItemCommand", ex)
                        Me.TransferFramePage("~/WF/WFA021.aspx", Nothing, "MessageType=ERROR", "InforPath=流程處理", "Message=" & Server.UrlEncode(ex.ToString))
                    End Try
                End Using
        End Select
    End Sub

    Private Sub ShowFlowMessage(ByVal Message As String, ByVal AssignTo As String)
        Dim strMessage As String = "<table><tr><th width=""20%"" align=""Right"">流程主鍵值：</th><td>【" & ViewState.Item("FlowCaseID") & "】</td></tr>"
        strMessage &= "<tr><th width=""20%"" align=""Right"">主键值：</th><td>" & ViewState.Item("FlowKeyValue").ToString().Replace("&", ", ") & "</td></tr>"
        strMessage &= "<tr><th width=""20%"" align=""Right"">意見內容：</th><td>" & TxtAreaOption.Value.ToString() & "</td></tr>"
        strMessage &= "<tr><th width=""20%"" align=""Right"">執行動作：</th><td>" & Message & "　指派：" & AssignTo & "</td></tr></table>"
        Me.TransferFramePage("~/WF/WFA021.aspx", Nothing, "InforPath=流程處理", "Message=" & Server.UrlEncode(strMessage))
    End Sub

    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) '片語重整
        Dim objWF As New WF()

        Using dt As DataTable = objWF.GetStepPhrase(ViewState.Item("FlowID"), ViewState.Item("FlowStepID"), UserProfile.ActUserID)
            FlowPhraseDrpList.DataSource = dt
            FlowPhraseDrpList.DataBind()
            FlowPhraseDrpList.Items.Insert(0, New ListItem("", ""))
        End Using
    End Sub

    Protected Sub FlowPhraseDrpList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles FlowPhraseDrpList.SelectedIndexChanged
        TxtAreaOption.Value &= FlowPhraseDrpList.SelectedValue
    End Sub

    Protected Sub btnOpinTemp_Click(ByVal sender As Object, ByVal e As System.EventArgs) '意見寫入暫存資料表
        Try
            Dim objWF As New WF()

            objWF.SaveFlowOpinTemp(ViewState.Item("FlowID"), ViewState.Item("FlowCaseID").ToString(), ViewState.Item("FlowLogID").ToString(), TxtAreaOption.Value)
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".btnOpinTemp_Click", ex)
        End Try
    End Sub

    Protected Sub btnShowFlowDetail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShowFlowDetail.Click
        Dim btnP As New ButtonState(ButtonState.emButtonType.Print)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
        Dim strURL As String = "~/WF/WFA030.aspx"

        strURL &= "?FlowID=" & ViewState.Item("FlowID").ToString()
        strURL &= "&FlowCaseID=" & ViewState.Item("FlowCaseID").ToString()
        strURL &= "&FlowLogID=" & ViewState.Item("FlowLogID").ToString()

        Me.CallPage(strURL, New ButtonState() {btnP, btnX})
    End Sub

    Protected Sub lbnPhraseEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbnPhraseEdit.Click
        Dim btnA As New ButtonState(ButtonState.emButtonType.Add)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)

        Me.CallPage("~/WF/WFA040.aspx", New ButtonState() {btnA, btnX}, "PhraseFlag=Y", _
            "FlowID=" & ViewState.Item("FlowID"), _
            "FlowStepID=" & ViewState.Item("FlowStepID"))
    End Sub

    ''' <summary>
    ''' 寄送流程信件
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SendFlowMail(ByVal FlowID As String, ByVal FlowCaseID As String, ByVal NextFlowDesc As String, ByVal NextAction As String, ByVal AssignTo As String, _
        ByVal AssignToStr As String, ByVal tran As DbTransaction)

        Dim DocKey As String = "" '用來寄送mail之key值
        Dim MailHeader As String = ""
        Dim PsDesc As String = GetMailTemplate()
        Dim PsDescEx As String = ""
        Dim objWF As New WF()
        Dim strFlowStepID As String = NextFlowDesc.Split("-")(0)

        '取得信件標題
        MailHeader = Bsp.Utility.IsStringNull(Bsp.DB.ExecuteScalar("Select Top 1 Define From SC_Common with (nolock) Where Type = '003' And Code = " & Bsp.Utility.Quote(FlowID)))
        If MailHeader <> "" Then
            MailHeader = String.Format("{0}-{1}", Bsp.MySettings.FlowMailHeader, MailHeader)
        Else
            MailHeader = Bsp.MySettings.FlowMailHeader
        End If


        '加入摘要
        Using dt As DataTable = objWF.GetFlowCase(FlowID, FlowCaseID)
            If dt.Rows.Count > 0 Then
                PsDesc = PsDesc.Replace("@@Summary", dt.Rows(0).Item("FlowShowValue").ToString().Replace("|", ", "))
            Else
                PsDesc = PsDesc.Replace("@@Summary", "")
            End If
        End Using

        '加入待處理人員
        PsDesc = PsDesc.Replace("@@AssignToStr", AssignToStr)

        '加入執行動作
        PsDesc = PsDesc.Replace("@@NextAction", NextAction)
        PsDesc = PsDesc.Replace("@@NextFlowDesc", NextFlowDesc)

        '加入上一關處理意見內容
        PsDesc = PsDesc.Replace("@@Opinion", TxtAreaOption.Value.ToString())

        '線審案件單獨處理-需待最後一名核委同意後始通知審查
        If strFlowStepID = "S011" AndAlso Bsp.Utility.IsStringNull(ViewState.Item("FlowStepID")) = "S010" Then
            If objWF.CheckGoNextStep(ViewState.Item("FlowID"), ViewState.Item("FlowCaseID"), ViewState.Item("FlowVer"), ViewState.Item("FlowLogBatNo"), ViewState.Item("FlowStepID"), NextAction, tran) Then
                MailHeader &= "(授信審查委員會案件)"

                PsDesc = PsDesc.Replace("@@AssignToStr", AssignToStr)

                '加入上一關處理人員
                Select Case objWF.GetDirectType(ViewState.Item("AppID").ToString())
                    Case "1"
                        PsDesc = PsDesc.Replace("@@ProcessUser", "Q9_1-" & WF.GetGroupName("Q9_1"))
                    Case "2"
                        PsDesc = PsDesc.Replace("@@ProcessUser", "Q9-" & WF.GetGroupName("Q9"))
                End Select

                Bsp.DB.ExecuteNonQuery(CommandType.StoredProcedure, "SP_SendMail", _
                    New DbParameter() { _
                    Bsp.DB.getDbParameter("@argSenderID", ""), _
                    Bsp.DB.getDbParameter("@argMailAddress", AssignTo), _
                    Bsp.DB.getDbParameter("@argMailHeader", MailHeader), _
                    Bsp.DB.getDbParameter("@argGreetWord", ""), _
                    Bsp.DB.getDbParameter("@argPsDesc", PsDesc.ToString()), _
                    Bsp.DB.getDbParameter("@argLinkPath", ""), _
                    Bsp.DB.getDbParameter("@argAttachment", "")}, tran)

            Else
                If NextAction <> "同意送審查維護" Then
                    '判斷是否為第一個點選不同意或有條件同意
                    If objWF.GetDisagreeCount(ViewState.Item("FlowID"), ViewState.Item("FlowCaseID"), tran) = 1 Then
                        MailHeader &= "(授審會案件提醒通知)：系統提會改書面提會"
                        PsDesc = PsDesc.Replace("<table>", "<table><td colspan =""2"">　　以系統提會之授審會案件已有簽核委員表達『不同意』或『有條件同意』之意見，本案依規應該以書面方式提會，僅此通知。<P></td></tr>")

                        Bsp.DB.ExecuteNonQuery(CommandType.StoredProcedure, "SP_SendMail", _
                           New DbParameter() { _
                           Bsp.DB.getDbParameter("@argSenderID", ""), _
                           Bsp.DB.getDbParameter("@argMailAddress", AssignTo), _
                           Bsp.DB.getDbParameter("@argMailHeader", MailHeader), _
                           Bsp.DB.getDbParameter("@argGreetWord", ""), _
                           Bsp.DB.getDbParameter("@argPsDesc", PsDesc.ToString()), _
                           Bsp.DB.getDbParameter("@argLinkPath", ""), _
                           Bsp.DB.getDbParameter("@argAttachment", "")}, tran)
                    End If
                End If
            End If

            Return
        End If

        '加入上一關處理人員
        PsDesc = PsDesc.Replace("@@ProcessUser", UserProfile.ActUserID & "-" & UserProfile.ActUserName)

        '檢查AssignTo,若為Q9,Q9_1,CM02則另外處理
        Select Case AssignTo
            Case "Q9", "Q9_1"
                AssignTo = objWF.GetGroupUserInfo(AssignTo)
            Case "CM02"
                AssignTo = objWF.GetAllCheckerID(FlowID, FlowCaseID, tran)
        End Select

        Bsp.DB.ExecuteNonQuery(CommandType.StoredProcedure, "SP_SendMail", _
                New DbParameter() { _
                Bsp.DB.getDbParameter("@argSenderID", ""), _
                Bsp.DB.getDbParameter("@argMailAddress", AssignTo), _
                Bsp.DB.getDbParameter("@argMailHeader", MailHeader), _
                Bsp.DB.getDbParameter("@argGreetWord", ""), _
                Bsp.DB.getDbParameter("@argPsDesc", PsDesc.ToString()), _
                Bsp.DB.getDbParameter("@argLinkPath", ""), _
                Bsp.DB.getDbParameter("@argAttachment", "")}, tran)
    End Sub

    ''' <summary>
    ''' 信件範本檔
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetMailTemplate() As String
        Dim strMail As New StringBuilder()

        strMail.AppendLine("<table>")
        strMail.AppendLine("<tr><th align=right>内容摘要：</th><td>@@Summary</td></tr>")
        strMail.AppendLine("<tr><th align=right>待處理人员：</th><td>@@AssignToStr</td></tr>")
        strMail.AppendLine("<tr><th align=right>上一關處理人員：</th><td>@@ProcessUser</td></tr>")
        strMail.AppendLine("<tr><th align=right>執行動作：</th><td>@@NextAction->[@@NextFlowDesc]</td></tr>")
        strMail.AppendLine("<tr><th align=right>上一關處理時間：</th><td>" & Format(Now, "yyyy/MM/dd HH:mm:ss") & "</td></tr>")
        strMail.AppendLine("<tr><th align=right>上一關處理意見內容：</th><td>@@Opinion</td></tr>")
        strMail.AppendLine("</table>")

        Return strMail.ToString()
    End Function
End Class
