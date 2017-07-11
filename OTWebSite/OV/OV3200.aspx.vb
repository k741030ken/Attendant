'****************************************************
'功能說明：特殊人員設定與維護
'建立人員：Judy,Jason
'建立日期：2017.01.04
'修改日期：2017.07.11
'****************************************************

Imports System.Data
Imports System.Data.Common
Imports System.Diagnostics      'For Debug.Print()

Partial Class OV_OV3200
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            '公司代碼
            If UserProfile.SelectCompRoleID.Equals("ALL") Then
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

    Private Sub DoAdd()
        Dim btnA As New ButtonState(ButtonState.emButtonType.Add)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
        Dim btnC As New ButtonState(ButtonState.emButtonType.Cancel)

        btnA.Caption = "存檔返回"
        btnX.Caption = "返回"
        btnC.Caption = "清除"

        Me.TransferFramePage("~/OV/OV3201.aspx", New ButtonState() {btnA, btnC, btnX}, _
                             "SelectedEmpID=" & hidEmpID.Value, _
                             "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
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
    Private Sub DoQuery()
        Try
            Dim dataTable As DataTable = Query()
            For i As Integer = 0 To dataTable.Rows.Count - 1 Step 1
                Dim comp As String = dataTable.Rows(i).Item("LastChgComp").ToString
                Dim emp As String = dataTable.Rows(i).Item("LastChgID").ToString
                'Dim lastDate As String = dataTable.Rows(i).Item("LastChgDate").ToString("yyyy-MM-dd HH:mm:ss.fff")
                dataTable.Rows(i).Item("LastChgComp") = getCompany(comp)
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
        Me.TransferFramePage("~/OV/OV3202.aspx", New ButtonState() {btnU, btnC, btnX}, _
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

    Private Sub DoClear()
        IsDoQuery.Value = ""
        ViewState.Item("DoQuery") = ""
        ViewState.Item("DoUpdate") = ""

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

        txtEmpID.Text = ""
        txtEmpName.Text = ""
        hidEmpID.Value = ""
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

    'Protected Sub gvMain_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvMain.RowDataBound
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        For intLoop As Integer = 0 To e.Row.Cells.Count - 1
    '            e.Row.Cells(intLoop).Attributes.Add("class", "td_detail")
    '            e.Row.Cells(intLoop).Style.Add("height", "15px")
    '        Next
    '    ElseIf e.Row.RowType = DataControlRowType.Header Then
    '        For intLoop As Integer = 0 To e.Row.Cells.Count - 1
    '            e.Row.Cells(intLoop).Attributes.Add("class", "td_header")
    '            e.Row.Cells(intLoop).Style.Add("height", "16px")
    '        Next
    '    End If
    'End Sub

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
            Me.TransferFramePage("~/OV/OV3202.aspx", New ButtonState() {btnX}, _
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

#Region "查詢特殊人員"

    ''' <summary>
    ''' 查詢特殊人員-以公司代碼與員工編號為搜尋條件
    ''' </summary>
    ''' <param name="Params"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Query(ParamArray Params() As String) As DataTable
        Dim beOverTimeSPUser As New beOverTimeSPUser.Row()
        Dim bsOverTimeSPUser As New beOverTimeSPUser.Service()
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)

        hidCompID.Value = UserProfile.SelectCompRoleID

        '取得輸入資料
        beOverTimeSPUser.CompID.Value = hidCompID.Value

        If txtEmpID.Text.Trim = "" Then
            beOverTimeSPUser.EmpID.Value = hidEmpID.Value
        Else
            beOverTimeSPUser.EmpID.Value = txtEmpID.Text.Trim
        End If

        Try
            Select Case ViewState.Item("DoQuery").ToString()
                Case "ONE"
                    Return bsOverTimeSPUser.QueryByKey(beOverTimeSPUser).Tables(0)
                Case "ALL"
                    Return bsOverTimeSPUser.Query(beOverTimeSPUser).Tables(0)
                Case Else
                    Return bsOverTimeSPUser.QueryByKey(beOverTimeSPUser).Tables(0)
            End Select
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & "Query", ex)
            Throw
        End Try
    End Function
#End Region

#Region "刪除指定特殊人員"
    Public Function Delete() As Boolean
        Dim beOverTimeSPUser As New beOverTimeSPUser.Row()
        Dim bsOverTimeSPUser As New beOverTimeSPUser.Service()

        '取得輸入資料
        beOverTimeSPUser.CompID.Value = gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString()
        beOverTimeSPUser.EmpID.Value = gvMain.DataKeys(selectedRow(gvMain))("EmpID").ToString()

        '檢查資料是否存在
        If bsOverTimeSPUser.IsDataExists(beOverTimeSPUser) Then
            Try
                '進行刪除
                Return DeleteOTTypeSetting(beOverTimeSPUser)
            Catch ex As Exception
                Bsp.Utility.ShowMessage(Me, Me.FunID & "Delete", ex)
                Return False
            End Try
        Else
            Return False
        End If
    End Function

    Public Function DeleteOTTypeSetting(ByVal beOverTimeSPUser As beOverTimeSPUser.Row) As Boolean
        Dim bsOverTimeSPUser As New beOverTimeSPUser.Service()

        '取得輸入資料
        beOverTimeSPUser.CompID.Value = gvMain.DataKeys(selectedRow(gvMain))("CompID").ToString()
        beOverTimeSPUser.EmpID.Value = gvMain.DataKeys(selectedRow(gvMain))("EmpID").ToString()

        Using cn As DbConnection = Bsp.DB.getConnection("AattendantDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsOverTimeSPUser.DeleteRowByPrimaryKey(beOverTimeSPUser, tran) = 0 Then Return False
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
#End Region

    Protected Sub txtEmpID_TextChanged(sender As Object, e As System.EventArgs) Handles txtEmpID.TextChanged
        If txtEmpID.Text.Trim <> "" Then
            Dim objOV As New OV32

            Dim rtnTable As DataTable = objOV.GetEmpName(hidCompID.Value, txtEmpID.Text.Trim)
            If rtnTable.Rows.Count <= 0 Then
                txtEmpName.Text = ""
                'Bsp.Utility.ShowFormatMessage(Me, "W_00020", "員工編號查詢姓名")
            Else
                txtEmpName.Text = rtnTable.Rows(0).Item(0)
            End If
        Else
            txtEmpName.Text = ""
        End If
    End Sub

    Protected Sub txtEmpName_TextChanged(sender As Object, e As System.EventArgs) Handles txtEmpName.TextChanged
        If txtEmpName.Text.Trim <> "" Then
            Dim objOV As New OV32
            Dim rtnTable As DataTable = objOV.GetEmpID(hidCompID.Value, txtEmpName.Text.Trim)
            If rtnTable.Rows.Count <= 0 Then
                txtEmpID.Text = ""
                'Bsp.Utility.ShowFormatMessage(Me, "W_00020", "員工姓名查詢員工編號")
            Else
                txtEmpID.Text = rtnTable.Rows(0).Item(0)
            End If
        Else
            txtEmpID.Text = ""
        End If
    End Sub
End Class
