'****************************************************
'功能說明：公出特殊人員設定
'建立人員：Jason
'建立日期：2017.07.11
'修改日期：
'****************************************************

Imports System.Data
Imports System.Data.Common
Imports System.Diagnostics      'For Debug.Print()

Partial Class OB_OB1200
    Inherits PageBase

#Region "1. 全域變數"

#End Region

#Region "2. 功能鍵處理邏輯"

    ''' <summary>
    ''' 按鈕控管
    ''' </summary>
    ''' <param name="Param"></param>
    ''' <remarks></remarks>
    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnAdd"       '新增
                DoAdd()
            Case "btnUpdate"    '修改
                DoUpdate()
            Case "btnQuery"     '查詢
                If (txtEmpID.Text.Trim <> "") Then
                    ViewState.Item("DoQuery") = "ONE"
                Else
                    ViewState.Item("DoQuery") = "ALL"
                End If
                DoQuery()
            Case "btnDelete"    '刪除
                DoDelete()
            Case "btnActionX"   '清除
                DoClear()
        End Select
    End Sub

    ''' <summary>
    ''' 新增
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DoAdd()
        Dim btnA As New ButtonState(ButtonState.emButtonType.Add)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
        Dim btnC As New ButtonState(ButtonState.emButtonType.Cancel)

        btnA.Caption = "存檔返回"
        btnX.Caption = "返回"
        btnC.Caption = "清除"

        Me.TransferFramePage("~/OB/OB1201.aspx", New ButtonState() {btnA, btnC, btnX}, _
                             "SelectedEmpID=" & hidEmpID.Value, _
                             "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
    End Sub

    ''' <summary>
    ''' 查詢
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DoQuery()
        Try
            Dim dataTable As DataTable = Query()
            For i As Integer = 0 To dataTable.Rows.Count - 1 Step 1
                Dim comp As String = dataTable.Rows(i).Item("LastChgComp").ToString
                Dim emp As String = dataTable.Rows(i).Item("LastChgID").ToString
                'Dim lastDate As String = dataTable.Rows(i).Item("LastChgDate").ToString("yyyy-MM-dd HH:mm:ss.fff")
                dataTable.Rows(i).Item("LastChgComp") = getCompanyName(comp)
                dataTable.Rows(i).Item("LastChgID") = getName(comp, emp)
                'dataTable.Rows(i).Item("LastChgDate") = lastDate
            Next
            pcMain.DataTable = dataTable
            gvMain.DataBind()
            gvMain.Visible = True
            If gvMain.Rows.Count > 0 Then
                hidEmpID.Value = txtEmpID.Text.Trim
            Else
                ViewState.Item("DoQuery") = ""
            End If
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
        End Try
    End Sub

    ''' <summary>
    ''' 修改
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DoUpdate()
        Dim btnU As New ButtonState(ButtonState.emButtonType.Update)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
        Dim btnC As New ButtonState(ButtonState.emButtonType.Cancel)

        btnU.Caption = "存檔返回"
        btnX.Caption = "返回"
        btnC.Caption = "清除"

        Dim strCompID As String
        If UserProfile.SelectCompRoleID = "ALL" Then
            strCompID = ddlCompID.SelectedValue
        Else
            strCompID = UserProfile.SelectCompRoleID
        End If

        ViewState.Item("DoUpdate") = "Y"
        Me.TransferFramePage("~/OB/OB1202.aspx", New ButtonState() {btnU, btnC, btnX}, _
               ddlCompID.ID & "=" & strCompID, _
               txtEmpID.ID & "=" & txtEmpID.Text, _
               txtEmpName.ID & "=" & txtEmpName.Text, _
               "PageNo=" & pcMain.PageNo.ToString(), _
               "SelectedCompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
               "SelectedEmpID=" & gvMain.DataKeys(selectedRow(gvMain))("EmpID").ToString(), _
               "SelectedNameN=" & gvMain.DataKeys(selectedRow(gvMain))("NameN").ToString(), _
               "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")), _
               "DoUpdate=" & Bsp.Utility.IsStringNull(ViewState.Item("DoUpdate")))
    End Sub

    ''' <summary>
    ''' 刪除
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DoDelete()
        Try
            If Delete() Then
                '刪除成功
                Bsp.Utility.ShowMessage(Me, "刪除成功")
                DoClear()
                DoQuery()
            Else
                Bsp.Utility.ShowMessage(Me, "刪除失敗")
            End If
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoDelete", ex)
        End Try
    End Sub

    ''' <summary>
    ''' 清除
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DoClear()
        IsDoQuery.Value = String.Empty
        ViewState.Item("DoQuery") = String.Empty
        ViewState.Item("DoUpdate") = String.Empty

        'GridView
        gvMain.Visible = False
        '釋放GridView資源的方法
        Try
            Using dt As DataTable = Nothing
                gvMain.DataSource = dt
                gvMain.DataBind()
            End Using
        Catch ex As Exception
            Debug.Print(ex.Message)
        End Try

        txtEmpID.Text = String.Empty
        txtEmpName.Text = String.Empty
        hidEmpID.Value = String.Empty
    End Sub

#End Region

#Region "3. Override Method"

#End Region

#Region "4. Page_Load"

    ''' <summary>
    ''' Page_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            '公司代碼
            If UserProfile.SelectCompRoleID = "ALL" Then
                lblCompRoleID.Visible = False
                Bsp.Utility.FillHRCompany(ddlCompID)
                Page.SetFocus(ddlCompID)
            Else
                lblCompRoleID.Text = UserProfile.SelectCompRoleName
                hidCompID.Value = UserProfile.SelectCompRoleID
                ddlCompID.Visible = False
            End If
        End If

        '員工編號
        ucQueryEmp.ShowCompRole = "False"
        ucQueryEmp.InValidFlag = "N"
        ucQueryEmp.SelectCompID = UserProfile.SelectCompRoleID
    End Sub

#End Region

#Region "5. 畫面事件"

    ''' <summary>
    ''' 取得畫面暫存資料
    ''' </summary>
    ''' <param name="ti"></param>
    ''' <remarks></remarks>
    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

            If ht.ContainsKey("SelectedEmpID") Then hidEmpID.Value = ht("SelectedEmpID").ToString()

            If ht.ContainsKey("PageNo") Then pcMain.PageNo = Convert.ToInt32(ht("PageNo"))
            If ht.ContainsKey("DoQuery") Then
                ViewState.Item("DoQuery") = ht("DoQuery").ToString()
                DoQuery()
            End If

            For Each strKey As String In ht.Keys
                Dim ctl As Control = Me.FindControl(strKey)
                If TypeOf ctl Is TextBox Then
                    CType(ctl, TextBox).Text = ht(strKey).ToString()
                ElseIf TypeOf ctl Is DropDownList Then
                    CType(ctl, DropDownList).SelectedValue = ht(strKey).ToString()
                End If
            Next
        End If
    End Sub

    ''' <summary>
    ''' 彈跳視窗-快速人員查詢(QFind)
    ''' </summary>
    ''' <param name="returnValue"></param>
    ''' <remarks></remarks>
    Public Overrides Sub DoModalReturn(ByVal returnValue As String)
        Dim strSql As String = ""

        If returnValue <> "" Then
            Dim aryData() As String = returnValue.Split(":")

            Select Case aryData(0)
                Case "ucQueryEmp"
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    '員工編號
                    txtEmpID.Text = aryValue(1)
                    '員工姓名
                    txtEmpName.Text = aryValue(2)
            End Select
        End If
    End Sub

    ''' <summary>
    ''' Occurs when a data row is bound to data in a GridView control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvMain_RowDataBound(ByVal sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMain.RowCommand

        If e.CommandName = "Detail" Then
            Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)

            btnX.Caption = "返回"

            Dim strCompID As String
            If UserProfile.SelectCompRoleID = "ALL" Then
                strCompID = ddlCompID.SelectedValue
            Else
                strCompID = UserProfile.SelectCompRoleID
            End If

            ViewState.Item("DoUpdate") = "N"
            Me.TransferFramePage("~/OB/OB1202.aspx", New ButtonState() {btnX}, _
                ddlCompID.ID & "=" & strCompID, _
                txtEmpID.ID & "=" & txtEmpID.Text, _
                txtEmpName.ID & "=" & txtEmpName.Text, _
                "PageNo=" & pcMain.PageNo.ToString(), _
                "SelectedCompID=" & gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString(), _
                "SelectedEmpID=" & gvMain.DataKeys(selectedRow(gvMain))("EmpID").ToString(), _
                "SelectedNameN=" & gvMain.DataKeys(selectedRow(gvMain))("NameN").ToString(), _
                "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")), _
                "DoUpdate=" & Bsp.Utility.IsStringNull(ViewState.Item("DoUpdate")))

        End If
    End Sub

    ''' <summary>
    ''' 員工編號輸入事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub txtEmpID_TextChanged(sender As Object, e As System.EventArgs) Handles txtEmpID.TextChanged
        If txtEmpID.Text.Trim <> "" Then
            Dim objSPUsr As New SPUsrUtility

            Dim rtnTable As DataTable = objSPUsr.GetEmpName(hidCompID.Value, txtEmpID.Text.Trim)
            If rtnTable.Rows.Count <= 0 Then
                txtEmpName.Text = String.Empty
                'Bsp.Utility.ShowFormatMessage(Me, "W_00020", "員工編號查詢姓名")
            Else
                txtEmpName.Text = rtnTable.Rows(0).Item(0)
            End If
        Else
            txtEmpName.Text = String.Empty
        End If
    End Sub

    ''' <summary>
    ''' 員工姓名輸入事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub txtEmpName_TextChanged(sender As Object, e As System.EventArgs) Handles txtEmpName.TextChanged
        If txtEmpName.Text.Trim <> "" Then
            Dim objSPUsr As New SPUsrUtility

            Dim rtnTable As DataTable = objSPUsr.GetEmpID(hidCompID.Value, txtEmpName.Text.Trim)
            If rtnTable.Rows.Count <= 0 Then
                txtEmpID.Text = String.Empty
                'Bsp.Utility.ShowFormatMessage(Me, "W_00020", "員工姓名查詢員工編號")
            Else
                txtEmpID.Text = rtnTable.Rows(0).Item(0)
            End If
        Else
            txtEmpID.Text = String.Empty
        End If
    End Sub

#End Region

#Region "6. 畫面檢核與確認"

#End Region

#Region "7. private Method"

    ''' <summary>
    ''' 查詢特殊人員-以公司代碼與員工編號為搜尋條件
    ''' </summary>
    ''' <param name="Params"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Query(ParamArray Params() As String) As DataTable
        Dim bePOSPUser As New bePOSPUser.Row()
        Dim bsPOSPUser As New bePOSPUser.Service()
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)

        hidCompID.Value = UserProfile.SelectCompRoleID

        '取得輸入資料
        bePOSPUser.CompID.Value = hidCompID.Value

        '設定查詢flag為公出
        bePOSPUser.Category.Value = "B"

        If String.IsNullOrEmpty(txtEmpID.Text.Trim) Then
            bePOSPUser.EmpID.Value = hidEmpID.Value
        Else
            bePOSPUser.EmpID.Value = txtEmpID.Text.Trim
        End If

        Try
            Select Case ViewState.Item("DoQuery").ToString()
                Case "ONE"
                    Return bsPOSPUser.QueryByKey(bePOSPUser).Tables(0)
                Case "ALL"
                    Return bsPOSPUser.Query(bePOSPUser).Tables(0)
                Case Else
                    Return bsPOSPUser.QueryByKey(bePOSPUser).Tables(0)
            End Select
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & "Query", ex)
            Throw
        End Try
    End Function

    ''' <summary>
    ''' 刪除前取得資料
    ''' </summary>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function Delete() As Boolean
        Dim bePOSPUser As New bePOSPUser.Row()
        Dim bsPOSPUser As New bePOSPUser.Service()

        '取得輸入資料
        bePOSPUser.CompID.Value = gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString()
        bePOSPUser.EmpID.Value = gvMain.DataKeys(selectedRow(gvMain))("EmpID").ToString()
        bePOSPUser.Category.Value = "B"

        '檢查資料是否存在
        If bsPOSPUser.IsDataExists(bePOSPUser) Then
            Try
                '進行刪除
                Return DeleteOTTypeSetting(bePOSPUser)
            Catch ex As Exception
                Bsp.Utility.ShowMessage(Me, Me.FunID & "Delete", ex)
                Return False
            End Try
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' 執行刪除動作
    ''' </summary>
    ''' <param name="bePOSPUser">欲刪除的資料</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteOTTypeSetting(ByVal bePOSPUser As bePOSPUser.Row) As Boolean
        Dim bsPOSPUser As New bePOSPUser.Service()

        '取得輸入資料
        bePOSPUser.CompID.Value = gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString()
        bePOSPUser.EmpID.Value = gvMain.DataKeys(selectedRow(gvMain))("EmpID").ToString()
        bePOSPUser.Category.Value = "B"

        Using cn As DbConnection = Bsp.DB.getConnection("AattendantDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsPOSPUser.DeleteRowByPrimaryKey(bePOSPUser, tran) = 0 Then Return False
                tran.Commit()
            Catch ex As Exception
                tran.Rollback()
                Throw New Exception(ex.Message)
            Finally
                If tran IsNot Nothing Then tran.Dispose()
            End Try
        End Using
        Return True
    End Function

    ''' <summary>
    ''' 取得公司名稱
    ''' </summary>
    ''' <param name="compID">公司別</param>
    ''' <returns>公司名稱</returns>
    ''' <remarks></remarks>
    Public Function getCompanyName(ByVal compID As String) As String
        Dim result As String = ""
        Dim strSQL As New StringBuilder
        Dim dataTable As New DataTable
        strSQL.Append(" select CompName FROM Company ")
        strSQL.Append(" where CompID = " & Bsp.Utility.Quote(compID))
        dataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)

        result = If(String.IsNullOrEmpty(dataTable.Rows(0).Item("CompName")), String.Empty, dataTable.Rows(0).Item("CompName"))
        Return result
    End Function

    ''' <summary>
    ''' 取得員工姓名
    ''' </summary>
    ''' <param name="compID">公司別</param>
    ''' <param name="empID">員工編號</param>
    ''' <returns>員工姓名</returns>
    ''' <remarks></remarks>
    Public Function getName(ByVal compID As String, ByVal empID As String) As String
        Dim result As String = ""
        Dim objSPUsr As New SPUsrUtility

        If Not String.IsNullOrEmpty(compID.Trim()) AndAlso Not String.IsNullOrEmpty(empID.Trim()) Then
            Dim rtnTable As DataTable = objSPUsr.GetEmpName(compID, empID)
            If rtnTable.Rows.Count <= 0 Then
                result = String.Empty
            Else
                result = If(String.IsNullOrEmpty(rtnTable.Rows(0).Item(0)), String.Empty, rtnTable.Rows(0).Item(0))
            End If
        Else
            result = String.Empty
        End If
        Return result
    End Function

#End Region

End Class

