Imports Newtonsoft.Json

Partial Class PO_PO1100
    Inherits PageBase

#Region "1. 全域變數"
    ''' <summary>
    ''' _PO1100Model
    ''' </summary>
    Private Property _PO1100Model As PO1100Model '全域private變數要為('_'+'小駝峰')
        Get
            Try
                If ViewState("_PO1100Model") IsNot Nothing Then 'ViewState當頁暫存使用
                    Return JsonConvert.DeserializeObject(Of PO1100Model)(ViewState("_PO1100Model").ToString())
                Else
                    Return Nothing
                End If
            Catch
                Return Nothing
            End Try
        End Get
        Set(value As PO1100Model)
            ViewState("_PO1100Model") = JsonConvert.SerializeObject(value)
        End Set
    End Property

    ''' <summary>
    ''' _OueryFlag
    ''' </summary>
    Private Property _OueryFlag As String '全域private變數要為('_'+'小駝峰')
        Get
            Try
                If ViewState("_OueryFlag") IsNot Nothing Then 'ViewState當頁暫存使用
                    Return JsonConvert.DeserializeObject(Of String)(ViewState("_OueryFlag").ToString())
                Else
                    Return Nothing
                End If
            Catch
                Return Nothing
            End Try
        End Get
        Set(value As String)
            ViewState("_OueryFlag") = JsonConvert.SerializeObject(value)
        End Set
    End Property
#End Region

#Region "2. 功能鍵處理邏輯"
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
#End Region

#Region "3. Override Method"
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
                End If
            Next
            If ht("DoQuery") = "Y" Then
                If ht.ContainsKey("PageNo") Then pcMain.PageNo = Convert.ToInt32(ht("PageNo"))
                DoQuery()
            Else
                DoClear()
            End If
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
            LoadData() '載入資料
        End If
    End Sub
#End Region

#Region "5. 畫面事件"
    ''' <summary>
    ''' gvMain_RowDataBound
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">GridViewRowEventArgs</param>
    Protected Sub gvMain_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvMain.RowDataBound

    End Sub
    ''' <summary>
    ''' gvMain_RowCommand
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">GridViewCommandEventArgs</param>
    Protected Sub gvMain_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvMain.RowCommand

    End Sub

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
    ''' 執行修改、刪除前檢核
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function queryValidation() As String
        Dim errorMsg As String = ""
        If "".Equals(_OueryFlag) Then
            errorMsg = "請先查詢資料"
        End If
        Return errorMsg
    End Function
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

#End Region

#Region "7. private Method"
    ''' <summary>
    ''' 載入資料
    ''' </summary>
    Private Sub LoadData()
        '公司
        CompID.Text = UserProfile.SelectCompRoleName

        '單位代碼1
        PA2.FillOrganID_PA2201(ddlDeptID, UserProfile.SelectCompRoleID, "", "1")
        ddlDeptID.Items.Insert(0, New ListItem("---請選擇---", ""))

        '單位代碼2
        PA2.FillOrganID_PA2201(ddlOrganID, UserProfile.SelectCompRoleID, ddlDeptID.SelectedValue, "2")
        ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
    End Sub
    ''' <summary>
    ''' 新增邏輯
    ''' </summary>
    Private Sub DoAdd()
        Dim btnA As New ButtonState(ButtonState.emButtonType.Add)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
        Dim btnC As New ButtonState(ButtonState.emButtonType.Cancel)

        btnA.Caption = "存檔返回"
        btnX.Caption = "返回"
        btnC.Caption = "清除"

        Me.TransferFramePage("~/PO/PO1101.aspx", New ButtonState() {btnA, btnX, btnC}, _
            ddlDeptID.ID & "=" & ddlDeptID.SelectedValue, _
            ddlOrganID.ID & "=" & ddlOrganID.SelectedValue, _
            "PageNo=" & IIf(pcMain.PageNo.ToString() <> "", pcMain.PageNo.ToString(), pcMain.PageNo.ToString()), _
            "DoQuery=" & _OueryFlag, _
            "DoFunction=" & "Add")
    End Sub

    ''' <summary>
    ''' 新增邏輯
    ''' </summary>
    Private Sub DoUpdate()
        If "" <> queryValidation() Then
            Bsp.Utility.ShowMessage(Me, queryValidation())
            Return
        End If

        Dim btnA As New ButtonState(ButtonState.emButtonType.Update)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
        Dim btnC As New ButtonState(ButtonState.emButtonType.Cancel)

        btnA.Caption = "存檔返回"
        btnX.Caption = "返回"
        btnC.Caption = "清除"

        Me.TransferFramePage("~/PO/PO1101.aspx", New ButtonState() {btnA, btnX, btnC}, _
            ddlDeptID.ID & "=" & ddlDeptID.SelectedValue, _
            ddlOrganID.ID & "=" & ddlOrganID.SelectedValue, _
            "CompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
            "DeptID=" & gvMain.DataKeys(selectedRow(gvMain))("DeptID").ToString(), _
            "OrganID=" & gvMain.DataKeys(selectedRow(gvMain))("OrganID").ToString(), _
            "SpecialFlag=" & gvMain.DataKeys(selectedRow(gvMain))("SpecialFlag").ToString(), _
            "LastChgCompID_Name=" & gvMain.DataKeys(selectedRow(gvMain))("LastChgCompID_Name").ToString(), _
            "LastChgID_Name=" & gvMain.DataKeys(selectedRow(gvMain))("LastChgID_Name").ToString(), _
            "LastChgDate=" & gvMain.DataKeys(selectedRow(gvMain))("LastChgDate").ToString(), _
            "PageNo=" & IIf(pcMain.PageNo.ToString() <> "", pcMain.PageNo.ToString(), pcMain.PageNo.ToString()), _
            "DoQuery=" & _OueryFlag, _
            "DoFunction=" & "Update")
    End Sub
    ''' <summary>
    ''' 查詢邏輯
    ''' </summary>
    Private Sub DoQuery()
        If "" <> deptValidation() Then
            Bsp.Utility.ShowMessage(Me, deptValidation())
            Return
        End If

        Dim isSuccess As Boolean = False
        Dim msg As String = ""
        Dim datas As List(Of PunchSpecialUnitDefineBean) = New List(Of PunchSpecialUnitDefineBean)()
        Dim viewData As PO1100Model = New PO1100Model() With _
        { _
            .CompID = StringIIF(UserProfile.SelectCompRoleID),
            .DeptID = StringIIF(ddlDeptID.SelectedValue),
            .OrganID = StringIIF(ddlOrganID.SelectedValue)
        }
        isSuccess = PO1100.SelectPunchSpecialUnitDefine(viewData, datas, msg)
        If isSuccess And datas.Count > 0 Then
            viewData.PO1100GridDataList = PO1100.SelectDataFormat(datas) 'Format Data   
            ShowTable.Visible = True
            pcMain.DataTable = PO1100.ConvertToDataTable(viewData.PO1100GridDataList)
            gvMain.DataBind()
            _PO1100Model = viewData
            _OueryFlag = "Y"
        Else
            If _OueryFlag = "" Then
                Bsp.Utility.ShowMessage(Me, "查無資料")
            End If

            ShowTable.Visible = False
            pcMain.DataTable = Nothing
            gvMain.DataBind()

            DoClear()
        End If

    End Sub
    ''' <summary>
    ''' 刪除邏輯
    ''' </summary>
    Private Sub DoDelete()
        If "" <> queryValidation() Then
            Bsp.Utility.ShowMessage(Me, queryValidation())
            Return
        End If

        Dim result As Boolean = False
        Dim seccessCount As Long = 0
        Dim msg As String = ""
        Try
            Dim model As PO1100Model = New PO1100Model() With _
                                         {.CompID = gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
                                          .DeptID = gvMain.DataKeys(selectedRow(gvMain))("DeptID").ToString(), _
                                          .OrganID = gvMain.DataKeys(selectedRow(gvMain))("OrganID").ToString()
                                          }
            result = PO1100.DeletePunchSpecialUnitDefine(model, seccessCount, msg)
            If Not result Then
                Throw New Exception(msg)
            End If
            If seccessCount = 0 Then
                Throw New Exception("無資料被刪除!!")
            End If
            Bsp.Utility.ShowMessage(Me, "刪除成功")
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, ex.Message)
        End Try
        DoQuery()
    End Sub
    ''' <summary>
    ''' 清除邏輯
    ''' </summary>
    Private Sub DoClear()
        ddlDeptID.SelectedIndex = 0
        ddlOrganID.SelectedIndex = 0
        gvMain.DataSource = Nothing
        ShowTable.Visible = False
        _OueryFlag = ""
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

#End Region

End Class
