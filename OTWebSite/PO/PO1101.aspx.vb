Imports Newtonsoft.Json

Partial Class PO_PO1101
    Inherits PageBase

#Region "1. 全域變數"
    ''' <summary>
    ''' _doFunction
    ''' </summary>
    Private Property _doFunction As String '全域private變數要為('_'+'小駝峰')
        Get
            Try
                If ViewState("_doFunction") IsNot Nothing Then 'ViewState當頁暫存使用
                    Return JsonConvert.DeserializeObject(Of String)(ViewState("_doFunction").ToString())
                Else
                    Return Nothing
                End If
            Catch
                Return Nothing
            End Try
        End Get
        Set(value As String)
            ViewState("_doFunction") = JsonConvert.SerializeObject(value)
        End Set
    End Property
#End Region

#Region "2. 功能鍵處理邏輯"
    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnAdd"       '新增 > 返回
                DoAdd()
            Case "btnUpdate"    '修改 > 返回
                DoUpdate()
            Case "btnCancel"    '清除
                initScreen()
            Case "btnActionX"   '返回
                GoBack()
        End Select
    End Sub
#End Region

#Region "3. Override Method"
    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim passValue As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)
            Dim DoFunction = passValue("DoFunction").ToString
            _doFunction = DoFunction

            If DoFunction = "Update" Then
                ViewState.Item("CompID") = passValue("CompID").ToString
                ViewState.Item("DeptID") = passValue("DeptID").ToString
                ViewState.Item("OrganID") = passValue("OrganID").ToString
                ViewState.Item("SpecialFlag") = passValue("SpecialFlag").ToString
                ViewState.Item("LastChgCompID_Name") = passValue("LastChgCompID_Name").ToString
                ViewState.Item("LastChgID_Name") = passValue("LastChgID_Name").ToString
                ViewState.Item("LastChgDate") = passValue("LastChgDate").ToString
            End If
            LoadData()
        End If
    End Sub
#End Region

#Region "4. Page_Load"

    ''' <summary>
    ''' 起始
    ''' </summary>
    ''' <param name="sender">object</param>
    ''' <param name="e">EventArgs</param>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then

        End If
    End Sub

    

#End Region

#Region "5. 畫面事件"
    ''' <summary>
    ''' ddlDeptID_SelectedIndexChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ddlDeptID_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlDeptID.SelectedIndexChanged
        Dim strCompID As String = ""
        strCompID = UserProfile.SelectCompRoleID

        '單位代碼2
        PA2.FillOrganID_PA2201(ddlOrganID, strCompID, ddlDeptID.SelectedValue, "2", PA2.DisplayType.Full)
        ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
    End Sub
#End Region

#Region "6. 畫面檢核與確認"
    ''' <summary>
    ''' 單位代碼檢核
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function deptValidation() As String
        Dim errorMsg As String = ""
        Dim deptID As String = StringIIF(ddlDeptID.SelectedValue)

        If "".Equals(deptID) Then
            errorMsg = "請選擇單位代碼-處"
        End If
        Return errorMsg
    End Function

    ''' <summary>
    ''' 單位代碼檢核
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function organValidation() As String
        Dim errorMsg As String = ""
        Dim organID As String = StringIIF(ddlOrganID.SelectedValue)

        If "".Equals(organID) Then
            errorMsg = "請選擇單位代碼-科組課"
        End If
        Return errorMsg
    End Function

#End Region

#Region "7. private Method"
    ''' <summary>
    ''' 載入資料
    ''' </summary>
    Private Sub LoadData()
        '公司
        lblCompID.Text = UserProfile.SelectCompRoleName
        '單位代碼1
        PA2.FillOrganID_PA2201(ddlDeptID, UserProfile.SelectCompRoleID, "", "1")
        ddlDeptID.Items.Insert(0, New ListItem("---請選擇---", ""))

        '單位代碼2
        PA2.FillOrganID_PA2201(ddlOrganID, UserProfile.SelectCompRoleID, ddlDeptID.SelectedValue, "2")
        ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))

        initScreen()
    End Sub
    ''' <summary>
    ''' 新增邏輯
    ''' </summary>
    Private Sub DoAdd()
        If "" <> deptValidation() Then
            Bsp.Utility.ShowMessage(Me, deptValidation())
            Return
        ElseIf "" <> organValidation() Then
            Bsp.Utility.ShowMessage(Me, organValidation())
            Return
        End If

        Dim result As Boolean = False
        Dim seccessCount As Long = 0
        Dim msg As String = ""
        Try
            Dim model As PO1100Model = New PO1100Model() With _
                                         {.CompID = UserProfile.SelectCompRoleName.Split("-")(0), _
                                          .CompName = UserProfile.SelectCompRoleName.Split("-")(1), _
                                          .DeptID = ddlDeptID.SelectedValue, _
                                          .DeptName = ddlDeptID.SelectedItem.Text.Split("-")(1), _
                                          .OrganID = ddlOrganID.SelectedValue, _
                                          .OrganName = ddlOrganID.SelectedItem.Text.Split("-")(1), _
                                          .SpecialFlag = IIf(rbtYesFlag.Checked, "1", "2"), _
                                          .LastChgComp = UserProfile.ActCompID, _
                                          .LastChgID = UserProfile.ActUserID}
            result = PO1100.InsertPunchSpecialUnitDefine(model, seccessCount, msg)
            If Not result Then
                Throw New Exception(msg)
            End If
            If seccessCount = 0 Then
                Throw New Exception("無資料被新增!!")
            End If
            Bsp.Utility.ShowMessage(Me, "新增成功")
            GoBack()
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 修改邏輯
    ''' </summary>
    Private Sub DoUpdate()
        Dim result As Boolean = False
        Dim seccessCount As Long = 0
        Dim msg As String = ""
        Dim sameFlag As Boolean = False
        Try
            Dim model As PO1100Model = New PO1100Model() With _
                                         {.CompID = StringIIF(lblCompID.Text.Split("-")(0)), _
                                          .CompName = StringIIF(lblCompID.Text.Split("-")(1)), _
                                          .CompID_Old = StringIIF(ViewState.Item("CompID")), _
                                          .DeptID = StringIIF(ddlDeptID.SelectedValue), _
                                          .DeptName = StringIIF(ddlDeptID.SelectedItem.Text.Split("-")(1)), _
                                          .DeptID_Old = StringIIF(ViewState.Item("DeptID")), _
                                          .OrganID = StringIIF(ddlOrganID.SelectedValue), _
                                          .OrganName = StringIIF(ddlOrganID.SelectedItem.Text.Split("-")(1)), _
                                          .OrganID_Old = StringIIF(ViewState.Item("OrganID")), _
                                          .SpecialFlag = IIf(rbtYesFlag.Checked, "1", "2"), _
                                          .LastChgComp = StringIIF(UserProfile.ActCompID), _
                                          .LastChgID = StringIIF(UserProfile.ActUserID)}
            sameFlag = dataCheck(model)
            result = PO1100.UpdatePunchSpecialUnitDefine(model, sameFlag, seccessCount, msg)
            If Not result Then
                Throw New Exception(msg)
            End If
            If seccessCount = 0 Then
                Throw New Exception("無資料被修改!!")
            End If
            Bsp.Utility.ShowMessage(Me, "修改成功")
            GoBack()
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, ex.Message)
        End Try
    End Sub

    Private Function dataCheck(ByVal model As PO1100Model) As Boolean
        Dim result As Boolean = True

        If model.CompID = model.CompID_Old And model.OrganID = model.OrganID_Old And model.DeptID = model.DeptID_Old Then
            result = False
        End If
        Return result
    End Function

    Private Sub initScreen()
        If _doFunction.Equals("Add") Then
            ddlDeptID.SelectedIndex = 0
            ddlOrganID.SelectedIndex = 0
            rbtYesFlag.Checked = True
        ElseIf _doFunction.Equals("Update") Then
            ddlDeptID.SelectedValue = ViewState.Item("DeptID")
            ddlDeptID_SelectedIndexChanged(Nothing, EventArgs.Empty)
            ddlOrganID.SelectedValue = ViewState.Item("OrganID")
            If ViewState.Item("SpecialFlag").Equals("1") Then
                rbtNoFlag.Checked = False
                rbtYesFlag.Checked = True
            Else
                rbtYesFlag.Checked = False
                rbtNoFlag.Checked = True
            End If
            txtLastChgComp.Text = ViewState.Item("LastChgCompID_Name")
            txtLastChgID.Text = ViewState.Item("LastChgID_Name")
            txtLastChgDate.Text = ViewState.Item("LastChgDate")
            lastComp.Visible = True
            lastEmp.Visible = True
            lastDate.Visible = True
        End If
    End Sub

    ''' <summary>
    ''' 取得字串(去除null)
    ''' </summary>
    ''' <param name="ob">Object</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Private Function StringIIF(ByVal ob As Object) As String
        Dim result = ""
        If Not ob Is Nothing Then
            If Not String.IsNullOrEmpty(ob.ToString().Trim) Then
                result = ob.ToString()
            End If
        End If
        Return result
    End Function

    ''' <summary>
    ''' 返回
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GoBack()
        Dim ti As TransferInfo = Me.StateTransfer
        Me.TransferFramePage(ti.CallerUrl, Nothing, ti.Args)
    End Sub
#End Region

End Class
