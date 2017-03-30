'****************************************************
'功能說明：其他代碼設定-查詢
'建立人員：Jason
'建立日期：2016/12/22
'修改日期：2017/02/15
'****************************************************
Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.Common

Partial Class OV_OV5000
    Inherits PageBase

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblCompName.Text = If(UserProfile.SelectCompRoleName, "")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            '代碼類別
            OV5.FillFldName_OV5000(ddlTabFldName)
            ddlTabFldName.Items.Insert(0, New ListItem("---請選擇---", ""))

            '代碼
            OV5.FillCode_OV5000(ddlCode, "", "")
            ddlCode.Items.Insert(0, New ListItem("---請選擇---", ""))
        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnAdd"       '新增
                DoAdd()
            Case "btnUpdate"    '修改
                DoUpdate()
            Case "btnQuery"     '查詢
                ViewState.Item("DoQuery") = "Y"
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
            If ht.ContainsKey("PageNo") Then pcMain.PageNo = Convert.ToInt32(ht("PageNo"))

            For Each strKey As String In ht.Keys
                Dim ctl As Control = Me.FindControl(strKey)
                If TypeOf ctl Is DropDownList Then
                    If ht(strKey).ToString <> "" Then
                        Bsp.Utility.SetSelectedIndex(CType(ctl, DropDownList), ht(strKey).ToString)
                    End If
                End If
            Next

            '代碼
            If ddlTabFldName.SelectedValue <> "" Then
                ddlTabFldName_SelectedIndexChanged(Nothing, Nothing)
            End If

            For Each strKey As String In ht.Keys
                Dim ctl As Control = Me.FindControl(strKey)
                If TypeOf ctl Is DropDownList Then
                    If ht(strKey).ToString <> "" Then
                        Bsp.Utility.SetSelectedIndex(CType(ctl, DropDownList), ht(strKey).ToString)
                    End If
                End If
            Next

            If ht.ContainsKey("DoQuery") Then
                If ht("DoQuery").ToString() = "Y" Then
                    ViewState.Item("DoQuery") = "Y"
                    DoQuery()
                End If
            End If
        End If
    End Sub

    Private Sub DoAdd()
        Dim btnA As New ButtonState(ButtonState.emButtonType.Add)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
        Dim btnC As New ButtonState(ButtonState.emButtonType.Cancel)

        btnA.Caption = "存檔返回"
        btnX.Caption = "返回"
        btnC.Caption = "清除"

        Me.TransferFramePage("~/OV/OV5001.aspx", New ButtonState() {btnA, btnC, btnX}, _
        "ddlTabFldName=" & ddlTabFldName.SelectedValue, _
        "ddlCode=" & ddlCode.SelectedValue, _
        "PageNo=" & pcMain.PageNo.ToString(), _
        "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
    End Sub

    Private Sub DoUpdate()
        Dim btnU As New ButtonState(ButtonState.emButtonType.Update)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
        Dim btnC As New ButtonState(ButtonState.emButtonType.Cancel)

        btnU.Caption = "存檔返回"
        btnX.Caption = "返回"
        btnC.Caption = "清除"

        ViewState.Item("DoUpdate") = "Y"
        Me.TransferFramePage("~/OV/OV5002.aspx", New ButtonState() {btnU, btnX, btnC}, _
        "ddlTabFldName=" & ddlTabFldName.SelectedValue, _
        "ddlCode=" & ddlCode.SelectedValue, _
        "SelectedTabName=" & gvMain.DataKeys(selectedRow(gvMain))("TabName").ToString(), _
        "SelectedFldName=" & gvMain.DataKeys(selectedRow(gvMain))("FldName").ToString(), _
        "SelectedCode=" & gvMain.DataKeys(selectedRow(gvMain))("Code").ToString(), _
        "PageNo=" & pcMain.PageNo.ToString(), _
        "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")), _
        "DoUpdate=" & Bsp.Utility.IsStringNull(ViewState.Item("DoUpdate")))
    End Sub

    Private Sub DoQuery()
        Dim TabName As String = ""
        Dim FldName As String = ""

        If ddlTabFldName.SelectedValue <> "" Then
            hidTabName.Value = ddlTabFldName.SelectedValue.Split("\")(0)
            hidFldName.Value = ddlTabFldName.SelectedValue.Split("\")(1)
        Else
            hidTabName.Value = ""
            hidFldName.Value = ""
        End If

            hidCode.Value = ddlCode.SelectedValue.Trim

        Try
            pcMain.DataTable = Query()
            gvMain.DataBind()
            gvMain.Visible = True
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & "DoQuery", ex)
        End Try
    End Sub

    Private Sub DoDelete()
        Try
            If selectedRow(gvMain) < 0 Then
                '尚未選取資料列!
                Bsp.Utility.ShowFormatMessage(Me, "W_00000")
            Else
                If Delete() Then
                    '刪除成功
                    'Bsp.Utility.ShowMessage(Me, "刪除成功")
                    DoClear()
                    DoQuery()
                Else
                    'Bsp.Utility.ShowMessage(Me, "刪除失敗")
                End If
            End If           
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoDelete", ex)
        End Try
    End Sub

    Private Sub DoClear()
        gvMain.Visible = False

        '代碼類別
        ddlTabFldName.SelectedValue = ""

        '代碼
        ddlTabFldName_SelectedIndexChanged(Nothing, Nothing)

        ViewState.Item("DoQuery") = ""
        DoQuery()
        gvMain.Visible = False
    End Sub

    ''' <summary>
    ''' 點選GridView的明細，跳轉到明細頁
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvMain_RowDataBound(ByVal sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMain.RowCommand
        If e.CommandName = "Detail" Then
            Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)

            btnX.Caption = "返回"

            ViewState.Item("DoUpdate") = "N"
            Me.TransferFramePage("~/OV/OV5002.aspx", New ButtonState() {btnX}, _
                                 "ddlTabFldName=" & ddlTabFldName.SelectedValue, _
                                 "ddlCode=" & ddlCode.SelectedValue, _
                                 "SelectedTabName=" & gvMain.DataKeys(selectedRow(gvMain))("TabName").ToString(), _
                                 "SelectedFldName=" & gvMain.DataKeys(selectedRow(gvMain))("FldName").ToString(), _
                                 "SelectedCode=" & gvMain.DataKeys(selectedRow(gvMain))("Code").ToString(), _
                                 "PageNo=" & pcMain.PageNo.ToString(), _
                                 "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")), _
                                 "DoUpdate=" & Bsp.Utility.IsStringNull(ViewState.Item("DoUpdate")))
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

#Region "查詢代碼"
    Public Function Query(ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim objOV As New OV5
        Dim beManageCodeSet As New beManageCodeSet.Row()
        Dim bsManageCodeSet As New beManageCodeSet.Service()

        If hidTabName.Value.Trim <> "" Then beManageCodeSet.TabName.Value = hidTabName.Value.Trim
        If hidFldName.Value.Trim <> "" Then beManageCodeSet.FldName.Value = hidFldName.Value.Trim
        If hidCode.Value.Trim <> "" Then beManageCodeSet.Code.Value = hidCode.Value.Trim

        Try
            If beManageCodeSet.FldName.Value <> "" Or beManageCodeSet.TabName.Value <> "" Then
                Dim dt As DataTable = objOV.QueryByDDL(beManageCodeSet)
                If dt.Rows.Count > 0 Then
                    Return dt
                End If
                dt.Clear()
                dt.Dispose()
                Return dt
            Else
                Return bsManageCodeSet.QueryAll(beManageCodeSet).Tables(0)
            End If
        Catch eq As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & "Query", eq)
            Throw
        End Try
    End Function
#End Region

#Region "刪除代碼"
    Public Function Delete() As Boolean
        Dim beManageCodeSet As New beManageCodeSet.Row()
        Dim bsManageCodeSet As New beManageCodeSet.Service()

        Dim Code As String
        Code = gvMain.DataKeys(selectedRow(gvMain))("Code").ToString()

        '取得輸入資料
        '代碼類別
        'Dim arrFldName(1) As String
        'arrFldName = ddlTabFldName.SelectedValue.Split("\")
        'beManageCodeSet.TabName.Value = arrFldName(0)
        'beManageCodeSet.FldName.Value = arrFldName(1)
        beManageCodeSet.TabName.Value = gvMain.DataKeys(selectedRow(gvMain))("TabName").ToString()
        beManageCodeSet.FldName.Value = gvMain.DataKeys(selectedRow(gvMain))("FldName").ToString()
        beManageCodeSet.Code.Value = gvMain.DataKeys(selectedRow(gvMain))("Code")

        '檢查資料是否存在
        If bsManageCodeSet.IsDataExists(beManageCodeSet) Then
            Try
                Return DeleteOTTypeSetting(beManageCodeSet)
            Catch ex As Exception
                Bsp.Utility.ShowMessage(Me, Me.FunID & "Delete", ex)
                Return False
            End Try
        End If
        Return False
    End Function

    Public Function DeleteOTTypeSetting(ByVal beManageCodeSet As beManageCodeSet.Row) As Boolean
        Dim bsManageCodeSet As New beManageCodeSet.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("AattendantDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsManageCodeSet.DeleteRowByPrimaryKey(beManageCodeSet, tran) = 0 Then Return False
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

    ''' <summary>
    ''' 代碼類別下拉表單選擇事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>依照代碼類別的選擇查詢並填入代碼的下拉表單中</remarks>
    Protected Sub ddlTabFldName_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlTabFldName.SelectedIndexChanged
        '代碼
        If ddlTabFldName.SelectedValue <> "" Then
            OV5.FillCode_OV5000(ddlCode, ddlTabFldName.SelectedValue.Split("\")(0), ddlTabFldName.SelectedValue.Split("\")(1))
        Else
            OV5.FillCode_OV5000(ddlCode, "", "")
        End If

        ddlCode.Items.Insert(0, New ListItem("---請選擇---", ""))
    End Sub
End Class
