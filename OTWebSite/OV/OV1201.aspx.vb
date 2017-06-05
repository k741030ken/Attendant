Imports System.Data
Imports System.Data.Common

Partial Class OV_OV1200

    Inherits PageBase
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
    Private Property Config_AattendantDBFlowOpenLog As String
        Get
            Dim result As String = OVBusinessCommon.AattendantDBFlowOpenLog
            Return result
        End Get
        Set(ByVal value As String)

        End Set
    End Property
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
    '存取傳到明細頁的資料
    Private Property DetailDatas As DataTable
        Get
            Return ViewState.Item("DetailDatas")
        End Get
        Set(ByVal value As DataTable)
            ViewState.Item("DetailDatas") = value
        End Set
    End Property
    '存取傳到明細頁的資料
    Private Property SelectDatas As DataTable
        Get
            Return Session.Item("SelectDatas")
        End Get
        Set(ByVal value As DataTable)
            Session.Item("SelectDatas") = value
        End Set
    End Property

    Private Property OldFlowLogID As String
        Get
            Return ViewState.Item("OldFlowLogID")
        End Get
        Set(ByVal value As String)
            ViewState.Item("OldFlowLogID") = value
        End Set
    End Property

    Private Property NewFlowLogID As String
        Get
            Return ViewState.Item("NewFlowLogID")
        End Get
        Set(ByVal value As String)
            ViewState.Item("NewFlowLogID") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            DetailDatas = SelectDatas
            SelectDatas = Nothing
            initScreen()
        End If
    End Sub
    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then '
            initScreen()
        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnDelete"    '確定
                If funCheckData() Then
                    insertAndUpdateDB()
                End If
            Case "btnCancel"   '清除
                doClear() '還沒寫
            Case "btnReject"     '返回
                GoBack()
        End Select
    End Sub
#Region "畫面初始化"
    Public Sub initScreen()

        lblCompID.Text = DetailDatas.Rows(0).Item("CompID").ToString & "-" & getCompany(DetailDatas.Rows(0).Item("CompID").ToString)
        lblFromName.Text = DetailDatas.Rows(0).Item("FormName").ToString
        lblFormEmpID.Text = DetailDatas.Rows(0).Item("AddEmpID").ToString & "-" & DetailDatas.Rows(0).Item("AddEmpName").ToString
        lblAppDate.Text = DetailDatas.Rows(0).Item("AddDate").ToString
        lblAddDate.Text = DetailDatas.Rows(0).Item("StartDate").ToString & "~" & DetailDatas.Rows(0).Item("EndDate").ToString
        lblAddTime.Text = DetailDatas.Rows(0).Item("StartTime").ToString & "~" & DetailDatas.Rows(0).Item("EndTime").ToString
        lblAppEmpID.Text = DetailDatas.Rows(0).Item("AdjustEmpID").ToString & "-" & DetailDatas.Rows(0).Item("AdjustEmpName").ToString
        lblLastChgComp.Text = DetailDatas.Rows(0).Item("LastChgComp").ToString & "-" & getCompany(DetailDatas.Rows(0).Item("LastChgComp").ToString)
        lblLastChgID.Text = DetailDatas.Rows(0).Item("LastChgID").ToString & "-" & getName(DetailDatas.Rows(0).Item("LastChgComp").ToString, DetailDatas.Rows(0).Item("LastChgID").ToString)
        lblLastChgDate.Text = DetailDatas.Rows(0).Item("LastChgDate").ToString
        initEmpArgs()
    End Sub
#End Region

#Region "員工編號UC元件"
    Public Sub initEmpArgs()
        ''員工編號
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
                    '員工編號
                    txtAdjustEmpID.Text = aryValue(1)
                    lblAdjustEmpID.Text = aryValue(2)
            End Select
        End If
    End Sub
#End Region

#Region "方法"
    Private Function StrF(ByVal ob As Object) As String
        Dim result = ""
        If Not ob Is Nothing Then
            If Not String.IsNullOrEmpty(ob.ToString()) Then
                result = ob.ToString()
            End If
        End If
        Return result
    End Function
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
#End Region

    Private Function funCheckData() As Boolean
        If txtAdjustEmpID.Text = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "調整處理人員 ")
            txtAdjustEmpID.Focus()
            Return False
        Else
            Dim objHR As New HR
            Dim rtnTable As DataTable = objHR.GetHREmpName(UserProfile.SelectCompRoleID, txtAdjustEmpID.Text)
            If rtnTable.Rows.Count <= 0 Then
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", "人事資料尚未建檔")
                txtAdjustEmpID.Focus()
                Return False
            End If
        End If

        If txtAdjustEmpID.Text <> "" Then
            If txtAdjustEmpID.Text.Equals(lblAppEmpID.Text.Split("-")(0)) Then
                Bsp.Utility.ShowMessage(Me, "調整處理人員與目前處理人員相同，請再確認!")
                txtAdjustEmpID.Focus()
                Return False
            End If
        End If


        Return True
    End Function

    Protected Sub txtAdjustEmpID_TextChanged(sender As Object, e As System.EventArgs) Handles txtAdjustEmpID.TextChanged
        If txtAdjustEmpID.Text <> "" Then
            Dim objHR As New HR
            Dim rtnTable As DataTable = objHR.GetHREmpName(UserProfile.SelectCompRoleID, txtAdjustEmpID.Text)
            If rtnTable.Rows.Count <= 0 Then
                lblAdjustEmpID.Text = ""
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", "人事資料尚未建檔")
            Else
                lblAdjustEmpID.Text = rtnTable.Rows(0).Item(0)
            End If
        Else
            lblAdjustEmpID.Text = ""
        End If
    End Sub

#Region "新增到資料庫和更新資料庫"
    Public Sub insertAndUpdateDB()
        Dim strSQL_InsertFullLog As StringBuilder = Insert_AattendantDBFlowFullLog()
        Dim strSQL_UpdateFullLog As StringBuilder = Update_AattendantDBFlowFullLog()
        Dim strSQL_UpdateOpenLog As StringBuilder = Update_AattendantDBFlowOpenLog()
        Dim strSQL_UpdateFlowCase As StringBuilder = Update_AattendantDBFlowCase()
        Dim strSQL_InsertTimeLog As StringBuilder = Insert_HROverTimeLog()
        Dim successFlag As Boolean = False

        Using cn As DbConnection = Bsp.DB.getConnection("AattendantDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Dim errorMsg = ""
            Try
                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL_InsertFullLog.ToString + strSQL_UpdateFullLog.ToString + strSQL_UpdateOpenLog.ToString + strSQL_UpdateFlowCase.ToString + strSQL_InsertTimeLog.ToString, tran, "AattendantDB")
                tran.Commit()
                successFlag = True
            Catch ex As Exception '錯誤發生，同時RollBack
                tran.Rollback()
                errorMsg = ex.Message
                Bsp.Utility.ShowMessage(Me, "保存失敗")
                Throw
            Finally
                If tran IsNot Nothing Then
                    tran.Dispose()
                ElseIf errorMsg <> "" Then
                    Bsp.Utility.ShowMessage(Me, errorMsg)
                End If

            End Try
        End Using

        If successFlag Then
            Bsp.Utility.ShowMessage(Me, "保存成功")
            GoBack()
        End If
    End Sub
#End Region

#Region "取得FlowLogID"
    Private Function Select_FlowLogID() As String
        Dim result As String = ""
        Dim dataTable As New DataTable
        Dim strSQL As New StringBuilder
        Dim flowCaseID As String = DetailDatas.Rows(0).Item("FlowCaseID").ToString
        Dim adjustEmpID As String = DetailDatas.Rows(0).Item("AdjustEmpID").ToString
        strSQL.AppendLine(" select FlowLogID from " & Config_AattendantDBFlowFullLog)
        strSQL.AppendLine(" where FlowCaseID = " & Bsp.Utility.Quote(flowCaseID))
        strSQL.AppendLine(" And AssignTo = " & Bsp.Utility.Quote(adjustEmpID))

        dataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "AattendantDB").Tables(0)
        result = dataTable.Rows(0).Item("FlowLogID").ToString
        Return result
    End Function

    Private Function GetNewFlowLogID(ByVal OldFlowLogID As String) As String
        Dim result As String = ""

        Dim Seq As Integer = Integer.Parse(Strings.Right(OldFlowLogID, 5)) + 1
        result = Strings.Left(OldFlowLogID, 15) + Seq.ToString("D5")

        Return result
    End Function
#End Region

#Region "取得改派後人員的名稱和公司"
    Private Function getEmpName() As String
        Dim result As String = ""
        Dim strSQL As New StringBuilder
        Dim dataTable As New DataTable
        Dim empID As String = StrF(txtAdjustEmpID.Text)

        strSQL.AppendLine("Select P.CompID, P.Name, O.OrganName FROM Personal as P")
        'strSQL.AppendLine("Left Join Organization as O on P.OrganID = O.OrganID")
        strSQL.AppendLine("Left Join Organization as O on P.DeptID = O.OrganID")
        strSQL.AppendLine("Where P.EmpID = " & Bsp.Utility.Quote(empID))
        dataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)

        ViewState.Item("EmpID") = empID
        ViewState.Item("CompID") = dataTable.Rows(0).Item("CompID").ToString
        result = dataTable.Rows(0).Item("OrganName").ToString & "-" & dataTable.Rows(0).Item("Name").ToString
        'result = dataTable.Rows(0).Item("Name").ToString
        Return result
    End Function
#End Region

#Region "取得原本AattendantDBFlowFullLog的資料，FlowLogBatNo + 1"
    Private Function Select_AattendantDBFlowFullLog() As DataTable
        Dim result As New DataTable
        Dim tempCount As Integer
        Dim strSQL As New StringBuilder
        Dim flowCaseID As String = DetailDatas.Rows(0).Item("FlowCaseID").ToString

        OldFlowLogID = Select_FlowLogID()
        NewFlowLogID = GetNewFlowLogID(OldFlowLogID)

        strSQL.AppendLine(" SELECT * FROM " & Config_AattendantDBFlowFullLog)
        strSQL.AppendLine(" where FlowCaseID = " & Bsp.Utility.Quote(flowCaseID))
        strSQL.AppendLine(" And FlowLogID = " & Bsp.Utility.Quote(OldFlowLogID))
        result = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "AattendantDB").Tables(0)

        tempCount = CInt(result.Rows(0).Item("FlowLogBatNo").ToString) + 1 : result.Rows(0).Item("FlowLogBatNo") = CStr(tempCount)

        'Dim i As Integer = result.Rows.Count
        Return result
    End Function
    Private Function Count_AattendantDBFlowFullLog() As Integer
        Dim strSQL As New StringBuilder
        Dim dataTable As New DataTable
        strSQL.AppendLine("select * from " & Config_AattendantDBFlowOpenLog)
        dataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "AattendantDB").Tables(0)

        Return dataTable.Rows.Count
    End Function
#End Region

#Region "新增到一筆AattendantDBFlowFullLog"
    Private Function Insert_AattendantDBFlowFullLog() As StringBuilder
        Dim result As New StringBuilder

        Dim adjustEmpName As String = getEmpName()
        Dim compID As String = ViewState.Item("CompID").ToString
        Dim empID As String = ViewState.Item("EmpID").ToString

        Dim oriData As DataTable = Select_AattendantDBFlowFullLog()
        Dim oldData = oriData.Rows(0)
        ViewState.Item("upFlowCaseID") = oldData.Item("FlowCaseID").ToString
        ViewState.Item("upFlowLogBatNo") = oldData.Item("FlowLogBatNo").ToString

        result.AppendLine("Insert Into " & Config_AattendantDBFlowFullLog & "(FlowCaseID, FlowLogBatNo, FlowLogID, FlowStepID, FlowStepName,")
        result.AppendLine("FlowStepBtnID, FlowStepBtnCaption, FlowStepOpinion, FlowLogIsClose, IsProxy, AttachID, FromDept, FromDeptName,")
        result.AppendLine("FromUser, FromUserName, AssignTo, AssignToName, ToDept, ToDeptName, ToUser, ToUserName, LogCrDateTime, LogUpdDateTime,LogRemark) ")
        result.AppendLine("Values(")
        result.AppendLine(Bsp.Utility.Quote(oldData.Item("FlowCaseID").ToString) & ",")
        result.AppendLine(Bsp.Utility.Quote(oldData.Item("FlowLogBatNo").ToString) & ",")
        result.AppendLine(Bsp.Utility.Quote(NewFlowLogID) & ",")
        result.AppendLine(Bsp.Utility.Quote(oldData.Item("FlowStepID").ToString) & ",")
        result.AppendLine(Bsp.Utility.Quote(oldData.Item("FlowStepName").ToString) & ",")
        result.AppendLine(Bsp.Utility.Quote(oldData.Item("FlowStepBtnID").ToString) & ",")
        result.AppendLine(Bsp.Utility.Quote(oldData.Item("FlowStepBtnCaption").ToString) & ",")
        result.AppendLine(Bsp.Utility.Quote(oldData.Item("FlowStepOpinion").ToString) & ",")
        result.AppendLine(Bsp.Utility.Quote(oldData.Item("FlowLogIsClose").ToString) & ",")
        result.AppendLine(Bsp.Utility.Quote(oldData.Item("IsProxy").ToString) & ",")
        result.AppendLine(Bsp.Utility.Quote(oldData.Item("AttachID").ToString) & ",")
        result.AppendLine(Bsp.Utility.Quote("FlowExpress") & ",")
        result.AppendLine(Bsp.Utility.Quote("流程系統") & ",")
        result.AppendLine(Bsp.Utility.Quote("FlowAgent") & ",")
        result.AppendLine(Bsp.Utility.Quote("流程代理") & ",")
        result.AppendLine(Bsp.Utility.Quote(empID) & ",")
        result.AppendLine(Bsp.Utility.Quote(adjustEmpName) & ",")
        result.AppendLine(Bsp.Utility.Quote(oldData.Item("ToDept").ToString) & ",")
        result.AppendLine(Bsp.Utility.Quote(oldData.Item("ToDeptName").ToString) & ",")
        result.AppendLine(Bsp.Utility.Quote(oldData.Item("ToUser").ToString) & ",")
        result.AppendLine(Bsp.Utility.Quote(oldData.Item("ToUserName").ToString) & ",")
        result.AppendLine("getDate(),")
        result.AppendLine(Bsp.Utility.Quote(Format(oldData.Item("LogUpdDateTime"), "yyyy/MM/dd HH:mm:ss")) & ",")
        result.AppendLine(Bsp.Utility.Quote(oldData.Item("LogRemark").ToString) & ")")
        result.AppendLine(";")
        Return result
    End Function
#End Region

#Region "更新AattendantDBFlowFullLog的資料"
    Private Function Update_AattendantDBFlowFullLog() As StringBuilder
        Dim result As New StringBuilder

        result.AppendLine("Update " & Config_AattendantDBFlowFullLog & " ")
        result.AppendLine("Set FlowStepBtnID = " & Bsp.Utility.Quote("FlowReassign") & ",")
        result.AppendLine("FlowStepBtnCaption = " & Bsp.Utility.Quote("改派") & ",")
        result.AppendLine("FlowLogIsClose = " & Bsp.Utility.Quote("Y") & ",")
        result.AppendLine("IsProxy = " & Bsp.Utility.Quote("N") & ",")
        result.AppendLine("ToDept = " & Bsp.Utility.Quote("FlowExpress") & ",")
        result.AppendLine("ToDeptName = " & Bsp.Utility.Quote("流程系統") & ",")
        result.AppendLine("ToUser = " & Bsp.Utility.Quote("FlowAgent") & ",")
        result.AppendLine("ToUserName = " & Bsp.Utility.Quote("流程代理") & ",")
        result.AppendLine("LogUpdDateTime = getDate() ")
        result.AppendLine("Where FlowLogID = " & Bsp.Utility.Quote(OldFlowLogID))
        result.AppendLine(";")

        Return result
    End Function
#End Region

#Region "更新AattendantDBFlowOpenLog的資料"
    Private Function Update_AattendantDBFlowOpenLog() As StringBuilder
        Dim result As New StringBuilder
        'Dim adjustEmpName As String = getEmpName()
        'Dim adjustEmpID As String = ViewState.Item("EmpID").ToString

        'result.AppendLine("Update AattendantDBFlowOpenLog ")
        'result.AppendLine("Set AssignTo = " & Bsp.Utility.Quote(ViewState.Item("EmpID")) & ",")
        'result.AppendLine("LogCrDateTime = getDate() ,")
        'result.AppendLine("FlowLogID = " & Bsp.Utility.Quote(NewFlowLogID) & ",")
        'result.AppendLine("AssignToName = " & Bsp.Utility.Quote(adjustEmpName) & ",")
        'result.AppendLine("FromDept = " & Bsp.Utility.Quote("FlowExpress") & ",")
        'result.AppendLine("FromDeptName = " & Bsp.Utility.Quote("流程系統") & ",")
        'result.AppendLine("FromUser = " & Bsp.Utility.Quote("FlowAgent") & ",")
        'result.AppendLine("FromUserName = " & Bsp.Utility.Quote("流程代理"))
        'result.AppendLine("Where FlowLogID = " & Bsp.Utility.Quote(OldFlowLogID))
        'result.AppendLine(";")

        '因為流程引擎會自動產生新的FlowLogID，所以只要刪除舊資料即可
        result.AppendLine("Delete " & Config_AattendantDBFlowOpenLog & " Where FlowLogID = " & Bsp.Utility.Quote(OldFlowLogID))
        Return result
    End Function
#End Region

#Region "取得原本HROverTimeLog及Seq的Max"
    Private Function Select_HROverTimeLog() As DataTable
        Dim result As New DataTable
        Dim strSQL As New StringBuilder
        Dim flowCaseID As String = DetailDatas.Rows(0).Item("FlowCaseID").ToString

        strSQL.AppendLine("Select *,")
        strSQL.AppendLine("(select MAX(Seq) from HROverTimeLog where FlowCaseID = " & Bsp.Utility.Quote(flowCaseID) & "group by FlowCaseID) as MaxNumber ")
        strSQL.AppendLine("from HROverTimeLog where FlowCaseID = " & Bsp.Utility.Quote(flowCaseID))
        result = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "AattendantDB").Tables(0)

        Return result
    End Function
#End Region

#Region "取得調整後人員的所屬單位以及最小功能單位"
    Private Function Select_SignOrganID(ByVal CompID As String, ByVal EmpID As String) As DataTable
        Dim strSQL As New StringBuilder

        strSQL.AppendLine("Select P.OrganID, ISNULL(F.OrganID, '') FlowOrganID From Personal P")
        strSQL.AppendLine("Left Join EmpFlow F On P.CompID = F.CompID And P.EmpID = F.EmpID And F.ActionID = '01'")
        strSQL.AppendLine("Where P.CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine("And P.EmpID = " & Bsp.Utility.Quote(EmpID))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "新增一筆資料至HROverTimeLog"
    Private Function Insert_HROverTimeLog() As StringBuilder
        Dim result As New StringBuilder

        Dim adjustCompID As String = ViewState.Item("CompID").ToString
        Dim adjustEmpID As String = ViewState.Item("EmpID").ToString

        Dim SignData As DataTable = Select_SignOrganID(adjustCompID, adjustEmpID)
        Dim oriData As DataTable = Select_HROverTimeLog()
        Dim oldData = oriData.Rows(0)

        result.AppendLine("Insert Into HROverTimeLog(FlowCaseID, FlowLogBatNo, FlowLogID, Seq, OTMode, ApplyID, OTEmpID, EmpOrganID, EmpFlowOrganID,")
        result.AppendLine("SignOrganID, SignFlowOrganID, FlowCode, FlowSN, FlowSeq, SignLine, SignIDComp, SignID, SignTime, FlowStatus, ReAssignFlag,")
        result.AppendLine("ReAssignComp, ReAssignEmpID, Remark, LastChgComp, LastChgID, LastChgDate) ")
        result.AppendLine("Values(")
        result.AppendLine(Bsp.Utility.Quote(oldData.Item("FlowCaseID").ToString) & ",")
        result.AppendLine(Bsp.Utility.Quote(oldData.Item("FlowLogBatNo").ToString) & ",")
        result.AppendLine(Bsp.Utility.Quote(OldFlowLogID) & ",")
        result.AppendLine(Bsp.Utility.Quote(StrF(CInt(oldData.Item("MaxNumber")) + 1)) & ",")
        result.AppendLine(Bsp.Utility.Quote(oldData.Item("OTMode").ToString) & ",")
        result.AppendLine(Bsp.Utility.Quote(UserProfile.ActUserID) & ",")
        result.AppendLine(Bsp.Utility.Quote(oldData.Item("OTEmpID").ToString) & ",")
        result.AppendLine(Bsp.Utility.Quote(oldData.Item("EmpOrganID").ToString) & ",")
        result.AppendLine(Bsp.Utility.Quote(oldData.Item("EmpFlowOrganID").ToString) & ",")
        result.AppendLine(Bsp.Utility.Quote(SignData.Rows(0).Item(0)) & ",")
        result.AppendLine(Bsp.Utility.Quote(SignData.Rows(0).Item(1)) & ",")
        result.AppendLine(Bsp.Utility.Quote(oldData.Item("FlowCode").ToString) & ",")
        result.AppendLine(Bsp.Utility.Quote(oldData.Item("FlowSN").ToString) & ",")
        result.AppendLine(Bsp.Utility.Quote(oldData.Item("FlowSeq").ToString) & ",")
        result.AppendLine(Bsp.Utility.Quote("3") & ",")
        result.AppendLine(Bsp.Utility.Quote(adjustCompID) & ",")
        result.AppendLine(Bsp.Utility.Quote(adjustEmpID) & ",")
        result.AppendLine("getDate(),")
        result.AppendLine(Bsp.Utility.Quote("1") & ",")
        result.AppendLine(Bsp.Utility.Quote("1") & ",")
        result.AppendLine(Bsp.Utility.Quote(adjustCompID) & ",")
        result.AppendLine(Bsp.Utility.Quote(adjustEmpID) & ",")
        result.AppendLine(Bsp.Utility.Quote(oldData.Item("Remark").ToString) & ",")
        '以下三個規格書上沒有說明是要抓原本的還是UserProfile的資料------------------------------------------------<<<
        result.AppendLine(Bsp.Utility.Quote(UserProfile.ActCompID) & ",")
        result.AppendLine(Bsp.Utility.Quote(UserProfile.ActUserID) & ",")
        result.AppendLine("getDate())")
        result.AppendLine(";")


        Return result
    End Function
#End Region

#Region "更新AattendantDBFlowCase的資料"
    Private Function Update_AattendantDBFlowCase() As StringBuilder
        Dim result As New StringBuilder
        Dim upFlowCaseIDa As String = ViewState.Item("upFlowCaseID").ToString
        Dim upFlowLogBatNo As String = ViewState.Item("upFlowLogBatNo").ToString

        result.AppendLine("Update " & Config_AattendantDBFlowCase & " ")
        result.AppendLine("Set LastLogBatNo = " & Bsp.Utility.Quote(upFlowLogBatNo) & ",")
        result.AppendLine("LastLogSeqNo = " & Bsp.Utility.Quote(upFlowLogBatNo) & ",")
        result.AppendLine("CrDateTime = getDate() ")
        result.AppendLine("Where FlowCaseID = " & Bsp.Utility.Quote(upFlowCaseIDa))
        result.AppendLine(";")

        Return result
    End Function
#End Region

    Private Sub GoBack()
        Dim ti As TransferInfo = Me.StateTransfer
        Me.TransferFramePage(ti.CallerUrl, Nothing, ti.Args)
    End Sub

    Private Sub doClear()
        txtAdjustEmpID.Text = ""
        lblAdjustEmpID.Text = ""
    End Sub
End Class
