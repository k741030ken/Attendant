Imports System.Data
Imports System.Data.Common
Imports SinoPac.WebExpress.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.SqlClient

Partial Class OV_OV8002
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
    '存取顯示資料
    Private Property _showDatas As DataTable
        Get
            Return ViewState.Item("ShowDatas")
        End Get
        Set(ByVal value As DataTable)
            ViewState.Item("ShowDatas") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then

        End If
    End Sub
    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            LoadData()
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)
            ViewState.Item("SystemID") = ht("SelectedSystemID").ToString()
            ViewState.Item("FlowCode") = ht("SelectedFlowCode").ToString()
            ViewState.Item("FlowName") = ht("SelectedFlowName").ToString()
            ViewState.Item("FlowSN") = ht("SelectedFlowSN").ToString()
            ViewState.Item("FlowSeq") = ht("SelectedFlowSeq").ToString()
            ViewState.Item("FlowSeqName") = ht("SelectedFlowSeqName").ToString()
            ViewState.Item("FlowStartFlag") = ht("SelectedFlowStartFlag").ToString()
            ViewState.Item("FlowEndFlag") = ht("SelectedFlowEndFlag").ToString()
            Select Case ht("SelectedFlowAct").ToString()
                Case "正常"
                    ViewState.Item("FlowAct") = "1"
                Case "跳過"
                    ViewState.Item("FlowAct") = "2"
                Case Else
            End Select
            Select Case ht("SelectedSignLineDefine").ToString()
                Case "行政組織"
                    ViewState.Item("SignLineDefine") = "1"
                Case "功能組織"
                    ViewState.Item("SignLineDefine") = "2"
                Case "特定人員"
                    ViewState.Item("SignLineDefine") = "3"
                Case Else
            End Select
            Select Case ht("SelectedSingIDDefine").ToString()
                Case "組織主管"
                    ViewState.Item("SingIDDefine") = "1"
                Case "特定人員"
                    ViewState.Item("SingIDDefine") = "2"
                Case Else
            End Select
            ViewState.Item("SpeComp") = ht("SelectedSpeComp").ToString()
            ViewState.Item("SpeEmpID") = ht("SelectedSpeEmpID").ToString()
            ViewState.Item("InValidFlag") = ht("SelectedInValidFlag").ToString()
            ViewState.Item("VisableFlag") = ht("SelectedVisableFlag").ToString()
            ViewState.Item("lblLastChgComp") = ht("SelectedLastChgComp").ToString()
            ViewState.Item("lblLastChgID") = ht("SelectedLastChgID").ToString()
            ViewState.Item("lblLastChgDate") = ht("SelectedLastChgDate").ToString()
            DoClear()
            ucQuerySpeEmpID.ShowCompRole = "True"
            ucQuerySpeEmpID.InValidFlag = "N"
            ucQuerySpeEmpID.SelectCompID = ddlSpeComp.SelectedValue
            QueryTableData(UserProfile.SelectCompRoleID, ViewState.Item("SystemID"), ViewState.Item("FlowCode"), ViewState.Item("FlowSN"), ViewState.Item("FlowSeq"))

        End If
    End Sub
    Public Overrides Sub DoAction(ByVal Param As String)
        Dim message = ""
        Select Case Param
            Case "btnUpdate"       '修改
                If ViewFieldValidate(message) Then
                    message = OV9Business.OV8And9EditCheck(UserProfile.SelectCompRoleID, ViewState.Item("FlowCode"), ViewState.Item("FlowSN"))
                    If message <> "" Then
                        msg.Text = message
                        Bsp.Utility.RunClientScript(Me.Page, "ContinueCheck();")
                    Else
                        updateData()
                    End If
                Else
                    Bsp.Utility.ShowMessage(Me, message)
                End If
                QueryTableData(UserProfile.SelectCompRoleID, ViewState.Item("SystemID"), ViewState.Item("FlowCode"), ViewState.Item("FlowSN"), ViewState.Item("FlowSeq"))
            Case "btnExecutes"  '修改並生效
                If ViewFieldValidate(message) Then
                    message = OV9Business.OV8And9EditCheck(UserProfile.SelectCompRoleID, ViewState.Item("FlowCode"), ViewState.Item("FlowSN"))
                    If message <> "" Then
                        msg.Text = message
                        Bsp.Utility.RunClientScript(Me.Page, "Continue2Check();")
                    Else
                        updateAndCheckData()
                    End If
                Else
                    Bsp.Utility.ShowMessage(Me, message)
                End If
                QueryTableData(UserProfile.SelectCompRoleID, ViewState.Item("SystemID"), ViewState.Item("FlowCode"), ViewState.Item("FlowSN"), ViewState.Item("FlowSeq"))
            Case "btnCancel"    '返回
                GoBack()
            Case "btnActionX"   '清除
                DoClear()
        End Select
    End Sub
    Protected Sub btnContinue_Click(sender As Object, e As System.EventArgs) Handles btnContinue.Click
        updateData()
    End Sub
    Protected Sub btnContinue2_Click(sender As Object, e As System.EventArgs) Handles btnContinue2.Click
        updateAndCheckData()
    End Sub
    Private Sub updateData()
        Dim message = ""
       Dim newTable = GetNewTableData()
        If DoUpdate(newTable, message) Then

            message = "修改成功"
            Bsp.Utility.ShowMessage(Me, message)
            'GoBack()
        Else
            Bsp.Utility.ShowMessage(Me, message)
        End If
        QueryTableData(UserProfile.SelectCompRoleID, ViewState.Item("SystemID"), ViewState.Item("FlowCode"), ViewState.Item("FlowSN"), ViewState.Item("FlowSeq"))
    End Sub
    Private Sub updateAndCheckData()
        Dim message = ""
        Dim newTable = GetNewTableData()
        If DoUpdate(newTable, message) Then
            If DataValidate(newTable, message) Then
                If DoExecutes(newTable, message) Then

                    message = "修改並生效"
                    Bsp.Utility.ShowMessage(Me, message)
                    'GoBack()
                Else
                    Bsp.Utility.ShowMessage(Me, message)
                End If
            Else
                Bsp.Utility.ShowMessage(Me, message)
            End If
        Else
            Bsp.Utility.ShowMessage(Me, message)
        End If
        QueryTableData(UserProfile.SelectCompRoleID, ViewState.Item("SystemID"), ViewState.Item("FlowCode"), ViewState.Item("FlowSN"), ViewState.Item("FlowSeq"))
    End Sub
    Public Overrides Sub DoModalReturn(ByVal returnValue As String)
        If returnValue <> "" Then
            Dim aryData() As String = returnValue.Split(":")
            Select Case aryData(0)
                Case "ucQuerySpeEmpID" '員編uc
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    txtSpeEmpID.Text = aryValue(1)
                    lblSpeEmpID.Text = aryValue(2)
                    ddlSpeComp.SelectedValue = aryValue(3)
            End Select
        End If
    End Sub

    Private Sub GoBack()
        Dim ti As TransferInfo = Me.StateTransfer
        Me.TransferFramePage(ti.CallerUrl, Nothing, ti.Args)
    End Sub
    Private Sub DoClear()
        lblFlowCode.Text = ViewState.Item("FlowCode")
        lblSystemID.Text = ViewState.Item("SystemID")
        txtFlowName.Text = ViewState.Item("FlowName")
        lblFlowSN.Text = ViewState.Item("FlowSN")
        lblFlowSeq.Text = ViewState.Item("FlowSeq")
        txtFlowSeqName.Text = ViewState.Item("FlowSeqName")
        chkFlowStartFlag.Checked = IIf(ViewState.Item("FlowStartFlag") = "1", True, False)
        chkFlowEndFlag.Checked = IIf(ViewState.Item("FlowEndFlag") = "1", True, False)
        ddlSignLineDefine.SelectedValue = ViewState.Item("SignLineDefine")
        ddlSingIDDefine.SelectedValue = ViewState.Item("SingIDDefine")
        'If SpeComp Then
        ddlSpeComp.SelectedValue = ViewState.Item("SpeComp")
        ddlFlowAct.SelectedValue = ViewState.Item("FlowAct")
        txtSpeEmpID.Text = ViewState.Item("SpeEmpID")
        If ddlSingIDDefine.SelectedValue = 2 Then
            Dim objHR As New HR
            Dim rtnTable As DataTable = objHR.GetHREmpName(ddlSpeComp.SelectedValue, txtSpeEmpID.Text)
            lblSpeEmpID.Text = rtnTable.Rows(0).Item(0)
        End If
        chkInValidFlag.Checked = IIf(ViewState.Item("InValidFlag") = "1", True, False)
        chkVisableFlag.Checked = IIf(ViewState.Item("VisableFlag") = "1", True, False)
        lblLastChgComp.Text = ViewState.Item("lblLastChgComp")
        lblLastChgID.Text = ViewState.Item("lblLastChgID")
        lblLastChgDate.Text = ViewState.Item("lblLastChgDate")
        ddlSingIDCheck()
    End Sub
    Private Function ViewFieldValidate(ByRef message As String) As Boolean
        Dim bl = False
        Try

            Dim sFlowAct = StringIIF(ddlFlowAct.SelectedValue) '流程動作
            Dim bFlowAct = Not String.IsNullOrEmpty(sFlowAct)
            Dim sSignLineDefine = StringIIF(ddlSignLineDefine.SelectedValue) '簽核線定義
            Dim bSignLineDefine = Not String.IsNullOrEmpty(sSignLineDefine)
            Dim sSingIDDefine = StringIIF(ddlSingIDDefine.SelectedValue) '簽核者定義
            Dim bSingIDDefine = Not String.IsNullOrEmpty(sSingIDDefine)
            Dim sSpeComp = StringIIF(ddlSpeComp.SelectedValue) '特定人員
            Dim bSpeComp = Not String.IsNullOrEmpty(sSpeComp)
            Dim sSpeEmpID = StringIIF(txtSpeEmpID.Text) '特定人員編號
            Dim bSpeEmpID = Not String.IsNullOrEmpty(sSpeEmpID)

            If Not bFlowAct Then
                Throw New Exception("請選擇流程動作!!")
            End If

            If Not bSignLineDefine Then
                Throw New Exception("請選擇簽核線定義!!")
            End If

            If Not bSingIDDefine Then
                Throw New Exception("請選擇簽核者定義!!")
            End If

            If sSingIDDefine = "2" Then

                If sSignLineDefine <> "3" Then
                    Throw New Exception("簽核線定義請選擇: 3-依特定人員!!")
                End If

                If Not bSpeComp Then
                    Throw New Exception("請選擇特定人員公司!!")
                End If

                If Not bSpeEmpID Then
                    Throw New Exception("請輸入特定人員編號!!")
                End If
            End If

            bl = True

        Catch ex As Exception
            message = ex.Message
        End Try
        Return bl
    End Function
    Private Function GetNewTableData() As DataTable
        Dim tb = New DataTable()
        If _showDatas IsNot Nothing Then
            If _showDatas.Rows.Count > 0 Then
                Dim sFlowName = StringIIF(txtFlowName.Text) '流程名稱
                Dim bFlowName = Not String.IsNullOrEmpty(sFlowName)
                Dim sFlowSeqName = StringIIF(txtFlowSeqName.Text) '關卡名稱
                Dim bFlowSeqName = Not String.IsNullOrEmpty(sFlowSeqName)
                Dim bFlowStartFlag = chkFlowStartFlag.Checked '流程起點註記
                Dim sFlowStartFlag = "0"
                Dim showFlowStartFlag = ""
                If bFlowStartFlag Then
                    sFlowStartFlag = "1"
                    showFlowStartFlag = "Y"
                End If
                Dim bFlowEndFlag = chkFlowEndFlag.Checked '流程終點註記
                Dim sFlowEndFlag = "0"
                Dim showFlowEndFlag = ""
                If bFlowEndFlag Then
                    sFlowEndFlag = "1"
                    showFlowEndFlag = "Y"
                End If
                Dim bInValidFlag = chkInValidFlag.Checked '無效註記
                Dim sInValidFlag = "0"
                Dim showInValidFlag = ""
                If bInValidFlag Then
                    sInValidFlag = "1"
                    showInValidFlag = "Y"
                End If
                Dim bVisableFlag = chkVisableFlag.Checked '隱藏流程註記
                Dim sVisableFlag = "0"
                Dim showVisableFlag = ""
                If bVisableFlag Then
                    sVisableFlag = "1"
                    showVisableFlag = "Y"
                End If
                Dim sFlowAct = StringIIF(ddlFlowAct.SelectedValue) '流程動作
                Dim bFlowAct = Not String.IsNullOrEmpty(sFlowAct)
                Dim sSignLineDefine = StringIIF(ddlSignLineDefine.SelectedValue) '簽核線定義
                Dim bSignLineDefine = Not String.IsNullOrEmpty(sSignLineDefine)
                Dim sSingIDDefine = StringIIF(ddlSingIDDefine.SelectedValue) '簽核者定義
                Dim bSingIDDefine = Not String.IsNullOrEmpty(sSingIDDefine)
                Dim sSpeComp = StringIIF(ddlSpeComp.SelectedValue) '特定人員
                Dim bSpeComp = Not String.IsNullOrEmpty(sSpeComp)
                Dim sSpeEmpID = StringIIF(txtSpeEmpID.Text) '特定人員編號
                Dim bSpeEmpID = Not String.IsNullOrEmpty(sSpeEmpID)
                Dim sLastChgComp = UserProfile.ActCompID
                Dim sLastChgID = UserProfile.ActUserID
                Dim sLastChgDate = Date.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
                Dim showLastChgDate = Date.Now.ToString("yyyy/MM/dd")

                tb = _showDatas.Clone()
                For index As Integer = 0 To _showDatas.Rows.Count - 1
                    tb.ImportRow(_showDatas.Rows(index))
                Next
                For index As Integer = 0 To tb.Rows.Count - 1
                    Dim row = tb.Rows(index)
                    If StringIIF(row.Item("IsFocus")) = "Y" Then
                        row.Item("FlowName") = sFlowName
                        row.Item("FlowSeqName") = sFlowSeqName
                        row.Item("FlowStartFlag") = sFlowStartFlag
                        row.Item("FlowEndFlag") = sFlowEndFlag
                        row.Item("InValidFlag") = sInValidFlag
                        row.Item("FlowAct") = sFlowAct
                        row.Item("SignLineDefine") = sSignLineDefine
                        row.Item("SingIDDefine") = sSingIDDefine
                        row.Item("SpeComp") = sSpeComp
                        row.Item("SpeEmpID") = sSpeEmpID
                        row.Item("VisableFlag") = sVisableFlag
                        row.Item("LastChgComp") = sLastChgComp
                        row.Item("LastChgID") = sLastChgID
                        row.Item("LastChgDate") = sLastChgDate
                        row.Item("ShowFlowStartFlag") = showFlowStartFlag
                        row.Item("ShowFlowEndFlag") = showFlowEndFlag
                        row.Item("ShowInValidFlag") = showInValidFlag
                        row.Item("ShowVisableFlag") = showVisableFlag
                        row.Item("ShowFlowAct") = FlowActFormat(sFlowAct, True)
                        row.Item("ShowSignLineDefine") = SignLineDefineFormat(sSignLineDefine, True)
                        row.Item("ShowSingIDDefine") = SingIDDefineFormat(sSingIDDefine, True)
                        row.Item("ShowLastChgDate") = showLastChgDate
                        If StringIIF(row.Item("Status")) = "1" Then
                            'If sInValidFlag = "1" Then
                            '    row.Item("Status") = "0"
                            '    row.Item("ShowStatus") = ""
                            'End If
                            row.Item("Status") = "0"
                            row.Item("ShowStatus") = ""
                        Else
                            row.Item("Status") = "0"
                            row.Item("ShowStatus") = ""
                        End If

                    End If
                Next
            End If
        End If
        Return tb
    End Function
    Private Function DataValidate(ByVal tb As DataTable, ByRef message As String) As Boolean
        Dim bl = False
        Try
            If tb IsNot Nothing Then
                If tb.Rows.Count > 0 Then
                    Dim flowStartFlagCount As Integer = (From row In tb.AsEnumerable() Where (row.Field(Of String)("FlowStartFlag") = "1" And row.Field(Of String)("InValidFlag") = "0" And row.Field(Of String)("VisableFlag") = "0")).Count()
                    Dim flowEndFlagCount As Integer = (From row In tb.AsEnumerable() Where (row.Field(Of String)("FlowEndFlag") = "1" And row.Field(Of String)("InValidFlag") = "0" And row.Field(Of String)("VisableFlag") = "0")).Count()
                    If flowStartFlagCount = 0 Then
                        Throw New Exception("無流程起點!!")
                    ElseIf flowEndFlagCount = 0 Then
                        Throw New Exception("無流程終點!!")
                    ElseIf flowStartFlagCount > 1 Then
                        Throw New Exception("不可多個流程起點!!")
                    ElseIf flowEndFlagCount > 1 Then
                        Throw New Exception("不可多個流程終點!!")
                    End If
                Else
                    Throw New Exception("無存檔資料!!")
                End If
            Else
                Throw New Exception("無存檔資料!!")
            End If

            bl = True

        Catch ex As Exception
            message = ex.Message
        End Try
        Return bl
    End Function
    Protected Function DoUpdate(ByVal newDataTable As DataTable, ByRef message As String) As Boolean
        Dim isSuccess = False
        Try
            If newDataTable IsNot Nothing Then
                If newDataTable.Rows.Count > 0 Then
                    Dim updateRowData As DataRow = (From row In newDataTable.AsEnumerable() Where (row.Field(Of String)("IsFocus") = "Y")).FirstOrDefault()
                    If updateRowData IsNot Nothing Then
                        Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
                        Dim strSQL As StringBuilder = New StringBuilder()
                        strSQL.AppendLine(" UPDATE HRFlowEngine ")
                        strSQL.AppendLine(" SET")
                        strSQL.AppendLine(" FlowName=@FlowName ")
                        strSQL.AppendLine(" ,FlowSeqName=@FlowSeqName ")
                        strSQL.AppendLine(" ,FlowStartFlag=@FlowStartFlag ")
                        strSQL.AppendLine(" ,FlowEndFlag=@FlowEndFlag ")
                        strSQL.AppendLine(" ,InValidFlag=@InValidFlag ")
                        strSQL.AppendLine(" ,VisableFlag=@VisableFlag ")
                        strSQL.AppendLine(" ,FlowAct=@FlowAct ")
                        strSQL.AppendLine(" ,SignLineDefine=@SignLineDefine ")
                        strSQL.AppendLine(" ,SingIDDefine=@SingIDDefine ")
                        strSQL.AppendLine(" ,SpeComp=@SpeComp ")
                        strSQL.AppendLine(" ,SpeEmpID=@SpeEmpID ")
                        strSQL.AppendLine(" ,LastChgComp=@LastChgComp ")
                        strSQL.AppendLine(" ,LastChgID=@LastChgID ")
                        strSQL.AppendLine(" ,LastChgDate=@LastChgDate ")
                        strSQL.AppendLine(" ,Status=@Status ")
                        strSQL.AppendLine(" WHERE 0 = 0 ")
                        strSQL.AppendLine(" AND CompID=@CompID ")
                        strSQL.AppendLine(" AND SystemID=@SystemID ")
                        strSQL.AppendLine(" AND FlowCode=@FlowCode ")
                        strSQL.AppendLine(" AND FlowSN=@FlowSN ")
                        strSQL.AppendLine(" AND FlowSeq=@FlowSeq ")
                        Dim dbcmd As DbCommand = db.GetSqlStringCommand(strSQL.ToString())
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, StringIIF(updateRowData("CompID")))
                        db.AddInParameter(dbcmd, "@SystemID", DbType.String, StringIIF(updateRowData("SystemID")))
                        db.AddInParameter(dbcmd, "@FlowCode", DbType.String, StringIIF(updateRowData("FlowCode")))
                        db.AddInParameter(dbcmd, "@FlowSN", DbType.String, StringIIF(updateRowData("FlowSN")))
                        db.AddInParameter(dbcmd, "@FlowSeq", DbType.String, StringIIF(updateRowData("FlowSeq")))
                        db.AddInParameter(dbcmd, "@FlowName", DbType.String, StringIIF(updateRowData("FlowName")))
                        db.AddInParameter(dbcmd, "@FlowSeqName", DbType.String, StringIIF(updateRowData("FlowSeqName")))
                        db.AddInParameter(dbcmd, "@FlowStartFlag", DbType.String, StringIIF(updateRowData("FlowStartFlag")))
                        db.AddInParameter(dbcmd, "@FlowEndFlag", DbType.String, StringIIF(updateRowData("FlowEndFlag")))
                        db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, StringIIF(updateRowData("InValidFlag")))
                        db.AddInParameter(dbcmd, "@VisableFlag", DbType.String, StringIIF(updateRowData("VisableFlag")))
                        db.AddInParameter(dbcmd, "@FlowAct", DbType.String, StringIIF(updateRowData("FlowAct")))
                        db.AddInParameter(dbcmd, "@SignLineDefine", DbType.String, StringIIF(updateRowData("SignLineDefine")))
                        db.AddInParameter(dbcmd, "@SingIDDefine", DbType.String, StringIIF(updateRowData("SingIDDefine")))
                        db.AddInParameter(dbcmd, "@SpeComp", DbType.String, StringIIF(updateRowData("SpeComp")))
                        db.AddInParameter(dbcmd, "@SpeEmpID", DbType.String, StringIIF(updateRowData("SpeEmpID")))
                        db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, StringIIF(updateRowData("LastChgComp")))
                        db.AddInParameter(dbcmd, "@LastChgID", DbType.String, StringIIF(updateRowData("LastChgID")))
                        db.AddInParameter(dbcmd, "@LastChgDate", DbType.String, StringIIF(updateRowData("LastChgDate")))
                        db.AddInParameter(dbcmd, "@Status", DbType.String, StringIIF(updateRowData("Status")))
                        Using cn As DbConnection = db.CreateConnection()
                            cn.Open()
                            Dim tran As DbTransaction = cn.BeginTransaction()
                            Try
                                Dim updateCount As Integer = db.ExecuteNonQuery(dbcmd)
                                If updateCount > 0 Then
                                    isSuccess = True
                                Else
                                    Throw New Exception("修改資料筆數為0")
                                End If
                                tran.Commit()
                            Catch ex As Exception
                                tran.Rollback()
                                Throw New Exception(ex.Message)
                            Finally
                                tran.Dispose()
                                If cn.State = ConnectionState.Open Then cn.Close()
                            End Try
                        End Using                       
                    Else
                        Throw New Exception("查無修改資料")
                    End If
                Else
                    Throw New Exception("查無修改資料")
                End If
            Else
                Throw New Exception("查無修改資料")
            End If
        Catch ex As Exception
            message = "修改失敗:" + ex.Message
        End Try

        Return (isSuccess)
    End Function
    Protected Function DoExecutes(ByVal newDataTable As DataTable, ByRef message As String) As Boolean
        Dim isSuccess = False
        Try
            Dim updateRowData As DataRow = (From row In newDataTable.AsEnumerable() Where (row.Field(Of String)("IsFocus") = "Y")).FirstOrDefault()
            If updateRowData IsNot Nothing Then

                Dim db2 As Database = DatabaseFactory.CreateDatabase("AattendantDB")
                Dim strSQL2 As StringBuilder = New StringBuilder()
                strSQL2.AppendLine(" UPDATE HRFlowEngine ")
                strSQL2.AppendLine(" SET")
                strSQL2.AppendLine(" Status=@Status2 ")
                strSQL2.AppendLine(" ,LastChgComp=@LastChgComp2 ")
                strSQL2.AppendLine(" ,LastChgID=@LastChgID2 ")
                strSQL2.AppendLine(" ,LastChgDate=@LastChgDate2 ")
                strSQL2.AppendLine(" WHERE 0 = 0 ")
                strSQL2.AppendLine(" AND CompID=@CompID2 ")
                strSQL2.AppendLine(" AND SystemID=@SystemID2 ")
                strSQL2.AppendLine(" AND FlowCode=@FlowCode2 ")
                strSQL2.AppendLine(" AND FlowSN=@FlowSN2 ")
                strSQL2.AppendLine(" AND InValidFlag=@InValidFlag2 ")
                Dim dbcmd2 As DbCommand = db2.GetSqlStringCommand(strSQL2.ToString())
                db2.AddInParameter(dbcmd2, "@CompID2", DbType.String, StringIIF(updateRowData("CompID")))
                db2.AddInParameter(dbcmd2, "@SystemID2", DbType.String, StringIIF(updateRowData("SystemID")))
                db2.AddInParameter(dbcmd2, "@FlowCode2", DbType.String, StringIIF(updateRowData("FlowCode")))
                db2.AddInParameter(dbcmd2, "@FlowSN2", DbType.String, StringIIF(updateRowData("FlowSN")))
                db2.AddInParameter(dbcmd2, "@InValidFlag2", DbType.String, "0")
                db2.AddInParameter(dbcmd2, "@Status2", DbType.String, "1")
                db2.AddInParameter(dbcmd2, "@LastChgComp2", DbType.String, UserProfile.ActCompID)
                db2.AddInParameter(dbcmd2, "@LastChgID2", DbType.String, UserProfile.ActUserID)
                db2.AddInParameter(dbcmd2, "@LastChgDate2", DbType.String, Date.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"))
                Using cn As DbConnection = db2.CreateConnection()
                    cn.Open()
                    Dim tran As DbTransaction = cn.BeginTransaction()
                    Try
                        Dim updateCount As Integer = db2.ExecuteNonQuery(dbcmd2)
                        If updateCount > 0 Then
                            isSuccess = True
                        Else
                            Throw New Exception("生效資料筆數為0")
                        End If
                        tran.Commit()
                    Catch ex As Exception
                        tran.Rollback()
                        Throw New Exception(ex.Message)
                    Finally
                        tran.Dispose()
                        If cn.State = ConnectionState.Open Then cn.Close()
                    End Try
                End Using
                
            Else
                Throw New Exception("查無生效資料")
            End If
        Catch ex As Exception
            message = "修改成功但生效失敗:" + ex.Message
        End Try
        Return isSuccess
    End Function
    Public Function QueryData(ByVal strColumn As String, ByVal strTable As String, ByVal strWhere As String) As String

        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" SELECT " + strColumn + " FROM " + strTable + " with (nolock) ")
        strSQL.AppendLine(" Where 1 = 1 " + strWhere)
        Dim dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
        If (dt.Rows.Count > 0) Then
            Return dt.Rows(0).Item("NameN").ToString().Trim()
        Else
            Return ""
        End If
    End Function
    Private Sub QueryTableData(ByVal sCompID As String, ByVal sSystemID As String, ByVal sFlowCode As String, ByVal sFlowSN As String, ByVal sFlowSeq As String)

        If Not String.IsNullOrEmpty(sCompID) And Not String.IsNullOrEmpty(sSystemID) And Not String.IsNullOrEmpty(sFlowCode) And Not String.IsNullOrEmpty(sFlowSN) Then
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            strSQL.AppendLine(" SELECT ")
            strSQL.AppendLine(" FE.CompID, FE.SystemID, FE.FlowCode, FE.FlowSN, FE.FlowSeq ")
            strSQL.AppendLine(" , FE.FlowName, FE.FlowSeqName, FE.FlowStartFlag, FE.FlowEndFlag, FE.InValidFlag, FE.FlowAct, FE.SignLineDefine, FE.SingIDDefine, FE.SpeComp, FE.SpeEmpID, FE.VisableFlag, FE.Status ")
            strSQL.AppendLine(" , FE.LastChgComp, FE.LastChgID, FE.LastChgDate ")
            strSQL.AppendLine(" , C.CompName AS SpeCompName ")
            strSQL.AppendLine(" , P.NameN AS SpeEmpName ")
            strSQL.AppendLine(" , C2.CompName AS LastChgCompName ")
            strSQL.AppendLine(" , P2.NameN AS LastChgName ")
            strSQL.AppendLine(" FROM HRFlowEngine AS FE ")
            strSQL.AppendLine(" LEFT JOIN " + eHRMSDB + ".[dbo].[Company] AS C ON C.CompID=FE.SpeComp ")
            strSQL.AppendLine(" LEFT JOIN " + eHRMSDB + ".[dbo].[Personal] AS P ON P.CompID=FE.SpeComp AND P.EmpID=FE.SpeEmpID ")
            strSQL.AppendLine(" LEFT JOIN " + eHRMSDB + ".[dbo].[Company] AS C2 ON C2.CompID=FE.LastChgComp ")
            strSQL.AppendLine(" LEFT JOIN " + eHRMSDB + ".[dbo].[Personal] AS P2 ON P2.CompID=FE.LastChgComp AND P2.EmpID=FE.LastChgID ")
            strSQL.AppendLine(" WHERE 0 = 0 ")
            strSQL.AppendLine(" AND FE.CompID = @CompID ")
            strSQL.AppendLine(" AND FE.SystemID = @SystemID ")
            strSQL.AppendLine(" AND FE.FlowCode = @FlowCode ")
            strSQL.AppendLine(" AND FE.FlowSN = @FlowSN ")
            strSQL.AppendLine(" ORDER BY FE.CompID, FE.SystemID, FE.FlowCode, FE.FlowSN, FE.FlowSeq ")
            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, sCompID)
            db.AddInParameter(dbcmd, "@SystemID", DbType.String, sSystemID)
            db.AddInParameter(dbcmd, "@FlowCode", DbType.String, sFlowCode)
            db.AddInParameter(dbcmd, "@FlowSN", DbType.String, sFlowSN)
            Dim ds As DataSet = db.ExecuteDataSet(dbcmd)
            If ds.Tables.Count > 0 Then
                Dim selectIndex = ""
                Dim dt = getCusDataTable(ds.Tables(0), sFlowSeq, selectIndex)
                If dt.Rows.Count > 0 Then
                    gvMain.DataSource = dt
                    _showDatas = dt
                    gvMain.DataBind()
                    If selectIndex <> "" Then
                        For index As Integer = 0 To gvMain.Rows.Count - 1
                            Dim onclickFunc As String = "" 'String.Format("__RowSelected_OT(this, '{0}', '{1}', '');", gvMain.ID.ToString(), index.ToString())
                            gvMain.Rows(index).Attributes.Add("onclick", onclickFunc)
                        Next
                        gvMain.Rows(Integer.Parse(selectIndex)).BackColor = Drawing.Color.Yellow
                        Dim onmouseoutFunc As String = String.Format("do_onmouseout_OT(this, '{0}', '{1}');", gvMain.ID.ToString(), selectIndex)
                        gvMain.Rows(Integer.Parse(selectIndex)).Attributes.Add("onmouseout", onmouseoutFunc)
                        gvMain.Rows(Integer.Parse(selectIndex)).CssClass = "tr_evenline" & " " & "IsYellow"
                    End If
                End If
            End If
        Else
            gvMain.DataSource = New DataTable()
            _showDatas = New DataTable()
            _showDatas.Columns.Add("CompID", Type.GetType("System.String")) '
            _showDatas.Columns.Add("SystemID", Type.GetType("System.String")) '
            _showDatas.Columns.Add("FlowCode", Type.GetType("System.String")) '
            _showDatas.Columns.Add("FlowSN", Type.GetType("System.String")) '
            _showDatas.Columns.Add("FlowSeq", Type.GetType("System.String")) '
            _showDatas.Columns.Add("FlowName", Type.GetType("System.String")) '
            _showDatas.Columns.Add("FlowSeqName", Type.GetType("System.String")) '
            _showDatas.Columns.Add("FlowStartFlag", Type.GetType("System.String")) '
            _showDatas.Columns.Add("FlowEndFlag", Type.GetType("System.String")) '
            _showDatas.Columns.Add("InValidFlag", Type.GetType("System.String")) '
            _showDatas.Columns.Add("FlowAct", Type.GetType("System.String")) '
            _showDatas.Columns.Add("SignLineDefine", Type.GetType("System.String")) '
            _showDatas.Columns.Add("SingIDDefine", Type.GetType("System.String")) '
            _showDatas.Columns.Add("SpeComp", Type.GetType("System.String")) '
            _showDatas.Columns.Add("SpeEmpID", Type.GetType("System.String")) '
            _showDatas.Columns.Add("VisableFlag", Type.GetType("System.String")) '
            _showDatas.Columns.Add("LastChgComp", Type.GetType("System.String")) '
            _showDatas.Columns.Add("LastChgID", Type.GetType("System.String")) '
            _showDatas.Columns.Add("LastChgDate", Type.GetType("System.String")) '
            _showDatas.Columns.Add("ShowFlowStartFlag", Type.GetType("System.String")) '
            _showDatas.Columns.Add("ShowFlowEndFlag", Type.GetType("System.String")) '
            _showDatas.Columns.Add("ShowInValidFlag", Type.GetType("System.String")) '
            _showDatas.Columns.Add("ShowVisableFlag", Type.GetType("System.String")) '
            _showDatas.Columns.Add("ShowFlowAct", Type.GetType("System.String")) '
            _showDatas.Columns.Add("ShowSignLineDefine", Type.GetType("System.String")) '
            _showDatas.Columns.Add("ShowSingIDDefine", Type.GetType("System.String")) '
            _showDatas.Columns.Add("ShowLastChgDate", Type.GetType("System.String")) '
            _showDatas.Columns.Add("Status", Type.GetType("System.String")) '
            _showDatas.Columns.Add("ShowStatus", Type.GetType("System.String")) '
            _showDatas.Columns.Add("IsFocus", Type.GetType("System.String")) '
            gvMain.DataBind()
        End If
    End Sub
    Private Function getCusDataTable(ByVal oldTable As DataTable, ByVal sFlowSeq As String, ByRef selectIndex As String) As DataTable
        Dim cusDT As DataTable = New DataTable("CusDataTable")
        cusDT.Columns.Add("CompID", Type.GetType("System.String")) '
        cusDT.Columns.Add("SystemID", Type.GetType("System.String")) '
        cusDT.Columns.Add("FlowCode", Type.GetType("System.String")) '
        cusDT.Columns.Add("FlowSN", Type.GetType("System.String")) '
        cusDT.Columns.Add("FlowSeq", Type.GetType("System.String")) '
        cusDT.Columns.Add("FlowName", Type.GetType("System.String")) '
        cusDT.Columns.Add("FlowSeqName", Type.GetType("System.String")) '
        cusDT.Columns.Add("FlowStartFlag", Type.GetType("System.String")) '
        cusDT.Columns.Add("FlowEndFlag", Type.GetType("System.String")) '
        cusDT.Columns.Add("InValidFlag", Type.GetType("System.String")) '
        cusDT.Columns.Add("FlowAct", Type.GetType("System.String")) '
        cusDT.Columns.Add("SignLineDefine", Type.GetType("System.String")) '
        cusDT.Columns.Add("SingIDDefine", Type.GetType("System.String")) '
        cusDT.Columns.Add("SpeComp", Type.GetType("System.String")) '
        cusDT.Columns.Add("SpeEmpID", Type.GetType("System.String")) '
        cusDT.Columns.Add("VisableFlag", Type.GetType("System.String")) '
        cusDT.Columns.Add("LastChgComp", Type.GetType("System.String")) '
        cusDT.Columns.Add("LastChgID", Type.GetType("System.String")) '
        cusDT.Columns.Add("LastChgDate", Type.GetType("System.String")) '
        cusDT.Columns.Add("ShowFlowStartFlag", Type.GetType("System.String")) '
        cusDT.Columns.Add("ShowFlowEndFlag", Type.GetType("System.String")) '
        cusDT.Columns.Add("ShowInValidFlag", Type.GetType("System.String")) '
        cusDT.Columns.Add("ShowVisableFlag", Type.GetType("System.String")) '
        cusDT.Columns.Add("ShowFlowAct", Type.GetType("System.String")) '
        cusDT.Columns.Add("ShowSignLineDefine", Type.GetType("System.String")) '
        cusDT.Columns.Add("ShowSingIDDefine", Type.GetType("System.String")) '
        cusDT.Columns.Add("ShowLastChgDate", Type.GetType("System.String")) '
        cusDT.Columns.Add("Status", Type.GetType("System.String")) '
        cusDT.Columns.Add("ShowStatus", Type.GetType("System.String")) '
        cusDT.Columns.Add("IsFocus", Type.GetType("System.String")) '

        For index As Integer = 0 To oldTable.Rows.Count - 1
            Dim oldRow = oldTable.Rows(index)
            Dim newRow = cusDT.NewRow()
            If sFlowSeq = StringIIF(oldRow.Item("FlowSeq")) Then
                selectIndex = index
                newRow.Item("IsFocus") = "Y"
            End If

            newRow.Item("CompID") = StringIIF(oldRow.Item("CompID"))
            newRow.Item("SystemID") = StringIIF(oldRow.Item("SystemID"))
            newRow.Item("FlowCode") = StringIIF(oldRow.Item("FlowCode"))
            newRow.Item("FlowSN") = StringIIF(oldRow.Item("FlowSN"))
            newRow.Item("FlowSeq") = StringIIF(oldRow.Item("FlowSeq"))
            newRow.Item("FlowName") = StringIIF(oldRow.Item("FlowName"))
            newRow.Item("FlowSeqName") = StringIIF(oldRow.Item("FlowSeqName"))
            newRow.Item("FlowStartFlag") = StringIIF(oldRow.Item("FlowStartFlag"))
            newRow.Item("FlowEndFlag") = StringIIF(oldRow.Item("FlowEndFlag"))
            newRow.Item("InValidFlag") = StringIIF(oldRow.Item("InValidFlag"))
            newRow.Item("FlowAct") = StringIIF(oldRow.Item("FlowAct"))
            newRow.Item("SignLineDefine") = StringIIF(oldRow.Item("SignLineDefine"))
            newRow.Item("SingIDDefine") = StringIIF(oldRow.Item("SingIDDefine"))
            Dim sSpeComp As String = StringIIF(oldRow.Item("SpeComp"))
            Dim sSpeEmpID As String = StringIIF(oldRow.Item("SpeEmpID"))
            If StringIIF(oldRow.Item("SpeCompName")) <> "" Then
                sSpeComp = sSpeComp & "-" & StringIIF(oldRow.Item("SpeCompName"))
            End If
            If StringIIF(oldRow.Item("SpeEmpName")) <> "" Then
                sSpeEmpID = sSpeEmpID & "-" & StringIIF(oldRow.Item("SpeEmpName"))
            End If
            newRow.Item("SpeComp") = sSpeComp
            newRow.Item("SpeEmpID") = sSpeEmpID
            newRow.Item("VisableFlag") = StringIIF(oldRow.Item("VisableFlag"))
            Dim sLastChgComp As String = StringIIF(oldRow.Item("LastChgComp"))
            Dim sLastChgID As String = StringIIF(oldRow.Item("LastChgID"))
            If StringIIF(oldRow.Item("LastChgCompName")) <> "" Then
                sLastChgComp = StringIIF(oldRow.Item("LastChgCompName"))
            End If
            If StringIIF(oldRow.Item("LastChgName")) <> "" Then
                sLastChgID = StringIIF(oldRow.Item("LastChgName"))
            End If
            newRow.Item("LastChgComp") = sLastChgComp
            newRow.Item("LastChgID") = sLastChgID
            newRow.Item("ShowFlowStartFlag") = FlagFormat(StringIIF(oldRow.Item("FlowStartFlag")), False)
            newRow.Item("ShowFlowEndFlag") = FlagFormat(StringIIF(oldRow.Item("FlowEndFlag")), False)
            newRow.Item("ShowInValidFlag") = FlagFormat(StringIIF(oldRow.Item("InValidFlag")), False)
            newRow.Item("ShowVisableFlag") = FlagFormat(StringIIF(oldRow.Item("VisableFlag")), False)
            newRow.Item("ShowFlowAct") = FlowActFormat(StringIIF(oldRow.Item("FlowAct")), True)
            newRow.Item("ShowSignLineDefine") = SignLineDefineFormat(StringIIF(oldRow.Item("SignLineDefine")), True)
            newRow.Item("ShowSingIDDefine") = SingIDDefineFormat(StringIIF(oldRow.Item("SingIDDefine")), True)
            newRow.Item("ShowLastChgDate") = getDataTimeStr(StringIIF(oldRow.Item("LastChgDate")), "yyyy/MM/dd HH:mm:ss")
            newRow.Item("Status") = StringIIF(oldRow.Item("Status"))
            newRow.Item("ShowStatus") = FlagFormat(StringIIF(oldRow.Item("Status")), False)
            cusDT.Rows.Add(newRow)
        Next
        Return cusDT
    End Function
    Private Function getDataTimeStr(ByVal dateStr As String, ByVal format As String) As String
        Dim result = ""
        Dim newDate As Date = New Date()
        If dateStr <> "" And dateStr <> "1900-01-01 00:00:00.000" And dateStr <> "1900/1/1 上午 12:00:00" And Date.TryParse(dateStr, newDate) Then
            result = newDate.ToString(format)
        End If
        Return result
    End Function
    Private Function SingIDDefineFormat(ByVal st As String, ByVal haveId As Boolean) As String
        Dim result = ""
        Select Case st
            Case "1"
                result = "組織主管"
                If haveId Then
                    result = st + "-" + result
                End If
            Case "2"
                result = "特定人員"
                If haveId Then
                    result = st + "-" + result
                End If
        End Select
        Return result
    End Function
    Private Function SignLineDefineFormat(ByVal st As String, ByVal haveId As Boolean) As String
        Dim result = ""
        Select Case st
            Case "1"
                result = "依行政組織"
                If haveId Then
                    result = st + "-" + result
                End If
            Case "2"
                result = "依功能組織"
                If haveId Then
                    result = st + "-" + result
                End If
            Case "3"
                result = "依特定人員"
                If haveId Then
                    result = st + "-" + result
                End If
        End Select
        Return result
    End Function
    Private Function FlowActFormat(ByVal st As String, ByVal haveId As Boolean) As String
        Dim result = ""
        Select Case st
            Case "1"
                result = "正常"
                If haveId Then
                    result = st + "-" + result
                End If
            Case "2"
                result = "跳過"
                If haveId Then
                    result = st + "-" + result
                End If
        End Select
        Return result
    End Function
    Private Function FlagFormat(ByVal st As String, ByVal haveN As Boolean) As String
        Dim result = ""
        If haveN Then
            result = "N"
        End If
        If st = "1" Then
            result = "Y"
        End If
        Return result
    End Function
    Private Function StringIIF(ByVal ob As Object) As String
        Dim result = ""
        If Not ob Is Nothing Then
            If Not String.IsNullOrEmpty(ob.ToString()) Then
                result = ob.ToString()
            End If
        End If
        Return result
    End Function
    Private Sub LoadData()
        '公司代碼
        lblCompID.Text = UserProfile.SelectCompRoleName
        'FillDDL(ddlFlowCode, "AattendantDB", "AT_CodeMap", "RTrim(Code)", "CodeCName", DisplayType.Full, "", " AND TabName='HRFlowEngine' AND FldName='FlowCode' AND NotShowFlag='0'", "ORDER BY SortFld, Code")
        lblFlowCode.Text = ViewState.Item("FlowCode")
        FillDDL(ddlSpeComp, "eHRMSDB", "Company", "CompID", "CompName", DisplayType.OnlyName, "", " AND InValidFlag = '0' AND NotShowFlag = '0'", "ORDER BY CompID")
        ddlSpeComp.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
        '系統代碼
        lblSystemID.Text = ViewState.Item("SystemID")
    End Sub
    Protected Function CheckData() As Boolean

        If (txtFlowName.Text = "") Then
            Bsp.Utility.ShowMessage(Me, "您尚未填寫流程名稱")
            Return False
        End If

        If (ddlFlowAct.SelectedValue = "") Then
            Bsp.Utility.ShowMessage(Me, "您尚未選取流程動作")
            Return False
        End If
        If (ddlSignLineDefine.SelectedValue = "") Then
            Bsp.Utility.ShowMessage(Me, "您尚未選取簽核線定義")
            Return False
        End If

        If (ddlSingIDDefine.SelectedValue = "") Then
            Bsp.Utility.ShowMessage(Me, "您尚未選取簽核者定義")
            Return False
        End If

        If (trSpec.Visible = True) Then
            If (ddlSpeComp.SelectedValue = "") Then
                Bsp.Utility.ShowMessage(Me, "您尚未選取特殊人員公司")
                Return False
            End If
            If (txtSpeEmpID.Text = "") Then
                Bsp.Utility.ShowMessage(Me, "您尚未選取特殊人員員編")
                Return False
            End If
        End If

        Return True
    End Function
    Private Sub ddlSingIDCheck()
        If ddlSingIDDefine.SelectedValue = "2" Then
            trSpec.Visible = True
            FillDDL(ddlSpeComp, "eHRMSDB", "Company", "CompID", "CompName", DisplayType.OnlyName, "", " AND InValidFlag = '0' AND NotShowFlag = '0'", "ORDER BY CompID")
            ddlSpeComp.Items.Insert(0, New ListItem("　- -請選擇- -", ""))
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

    Protected Sub txtSpeEmpID_TextChanged(sender As Object, e As System.EventArgs) Handles txtSpeEmpID.TextChanged
        If txtSpeEmpID.Text <> "" And txtSpeEmpID.Text.Length = 6 Then
            Dim objHR As New HR
            Dim rtnTable As DataTable = objHR.GetHREmpName(ddlSpeComp.SelectedValue, txtSpeEmpID.Text)
            If rtnTable.Rows.Count <= 0 Then
                lblSpeEmpID.Text = ""
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", "人事資料尚未建檔")
                lblSpeEmpID.Text = ""
            Else
                lblSpeEmpID.Text = rtnTable.Rows(0).Item(0)
            End If
        Else
            lblSpeEmpID.Text = ""
        End If
    End Sub

    Protected Sub ddlSpeComp_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlSpeComp.SelectedIndexChanged
        ucQuerySpeEmpID.SelectCompID = ddlSpeComp.SelectedValue
        txtSpeEmpID.Text = ""
        lblSpeEmpID.Text = ""
    End Sub

#Region "下拉選單-DisplayType"
    Enum DisplayType
        OnlyName    '只顯示名字
        OnlyID      '顯示ID  
        Full        '顯示ID + 名字
    End Enum
    Public Shared Sub FillDDLOV8000(ByVal objDDL As DropDownList, ByVal DBName As String, ByVal strTabName As String, ByVal strValue As String, ByVal strText As String, ByVal str3rdOrder As String, Optional ByVal Type As DisplayType = DisplayType.Full, Optional ByVal JoinStr As String = "", Optional ByVal WhereStr As String = "", Optional ByVal OrderByStr As String = "")
        Try
            Using dt As DataTable = GetDDLInfoOV8000(DBName, strTabName, strValue, strText, str3rdOrder, JoinStr, WhereStr, OrderByStr)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    Select Case Type
                        Case DisplayType.OnlyID
                            .DataTextField = "Code"
                        Case DisplayType.OnlyName
                            .DataTextField = "CodeName"
                        Case Else
                            .DataTextField = "FullName"
                    End Select
                    .DataValueField = "Code"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Shared Function GetDDLInfoOV8000(ByVal DBName As String, ByVal strTabName As String, ByVal strValue As String, ByVal strText As String, ByVal str3rdOrder As String, Optional ByVal JoinStr As String = "", Optional ByVal WhereStr As String = "", Optional ByVal OrderByStr As String = "") As DataTable
        Dim strSQL As New StringBuilder
        strSQL.AppendLine("Select distinct" & strValue & " AS Code")
        If strText <> "" Then strSQL.AppendLine(", " & strText & " AS CodeName, " & strValue & " + '-' + " & strText & " AS FullName ")
        If str3rdOrder <> "" Then strSQL.AppendLine(", " & str3rdOrder)
        strSQL.AppendLine("FROM " & strTabName)
        If JoinStr <> "" Then strSQL.AppendLine(JoinStr)
        strSQL.AppendLine("Where 1=1")
        If WhereStr <> "" Then strSQL.AppendLine(WhereStr)
        If OrderByStr <> "" Then
            strSQL.AppendLine(OrderByStr)
        Else
            strSQL.AppendLine("Order By " & strValue.Replace("distinct", ""))
        End If
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "" + DBName + "").Tables(0)
    End Function
#End Region
#Region "普通的FillDDL"
    Public Shared Sub FillDDL(ByVal objDDL As DropDownList, ByVal DBName As String, ByVal strTabName As String, ByVal strValue As String, ByVal strText As String, Optional ByVal Type As DisplayType = DisplayType.Full, Optional ByVal JoinStr As String = "", Optional ByVal WhereStr As String = "", Optional ByVal OrderByStr As String = "")
        Try
            Using dt As DataTable = GetDDLInfo(DBName, strTabName, strValue, strText, JoinStr, WhereStr, OrderByStr)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    Select Case Type
                        Case DisplayType.OnlyID
                            .DataTextField = "Code"
                        Case DisplayType.OnlyName
                            .DataTextField = "CodeName"
                        Case Else
                            .DataTextField = "FullName"
                    End Select
                    .DataValueField = "Code"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Shared Function GetDDLInfo(ByVal DBName As String, ByVal strTabName As String, ByVal strValue As String, ByVal strText As String, Optional ByVal JoinStr As String = "", Optional ByVal WhereStr As String = "", Optional ByVal OrderByStr As String = "") As DataTable
        Dim strSQL As New StringBuilder
        strSQL.AppendLine("Select " & strValue & " AS Code")
        If strText <> "" Then strSQL.AppendLine(", " & strText & " AS CodeName, " & strValue & " + '-' + " & strText & " AS FullName ")
        strSQL.AppendLine("FROM " & strTabName)
        If JoinStr <> "" Then strSQL.AppendLine(JoinStr)
        strSQL.AppendLine("Where 1=1")
        If WhereStr <> "" Then strSQL.AppendLine(WhereStr)
        If OrderByStr <> "" Then
            strSQL.AppendLine(OrderByStr)
        Else
            strSQL.AppendLine("Order By " & strValue.Replace("distinct", ""))
        End If
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "" + DBName + "").Tables(0)
    End Function
#End Region

    Public Shared Function GetEmpName(ByVal CompID As String, ByVal EmpID As String) As String
        Dim strSQL As String
        strSQL = "Select NameN From Personal"
        strSQL += " Where CompID =  " & Bsp.Utility.Quote(CompID)
        strSQL += " And EmpID =  " & Bsp.Utility.Quote(EmpID)

        Return Bsp.DB.ExecuteScalar(strSQL, "eHRMSDB")
    End Function
    Friend Function GridviewDoubleHeader(ByVal _gd As GridViewRowEventArgs, ByVal _od As Dictionary(Of String, Integer)) As GridViewRow
        Dim gvd = New GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal)

        Dim definehd = New TableCell()
        For Each oK As KeyValuePair(Of String, Integer) In _od
            definehd = New TableCell()
            definehd.Text = oK.Key
            definehd.ColumnSpan = oK.Value
            'definehd.Attributes.Add("bgcolor", "#89b3f5")
            definehd.Attributes.Add("align", "center") '標頭致中
            gvd.Cells.Add(definehd)
        Next
        gvd.BackColor = System.Drawing.Color.LightGray
        gvd.ForeColor = System.Drawing.Color.Black

        gvd.Visible = True

        Return gvd
    End Function
    Protected Sub gvMain_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.Header Then
            Dim od = New Dictionary(Of String, Integer)


            gvMain.Controls(0).Controls.AddAt(0, GridviewDoubleHeader(e, od))
        End If
    End Sub

End Class
