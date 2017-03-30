
Partial Class Component_ucPagerControl
    Inherits System.Web.UI.UserControl

    Private _GridViewName As String = ""
    Private _dtTable As Data.DataTable
    Private _dtThisPage As Data.DataTable       '當頁的資料
    Private _SQL As String
    Private bolReload As Boolean = True

    '目前分頁的資料集
    Public ReadOnly Property ThisPageData() As Data.DataTable
        Get
            Return _dtThisPage
        End Get
    End Property

    '所有資料的資料集
    Public Property DataTable() As Data.DataTable
        Get
            Return Session.Item("PagerControlData_" & GridViewName)
        End Get
        Set(ByVal value As Data.DataTable)
            Session.Item("PagerControlData_" & GridViewName) = value
            getBindingData()
            initData()
            BindGridView(False)

            SortExpression = ""
            SortDirection = ""
        End Set
    End Property

    Private Property SortExpression() As String
        Get
            If ViewState.Item("SortExpression") Is Nothing Then ViewState.Item("SortExpression") = ""
            Return ViewState.Item("SortExpression")
        End Get
        Set(ByVal value As String)
            ViewState.Item("SortExpression") = value
        End Set
    End Property

    Private Property SortDirection() As String
        Get
            If ViewState.Item("SortDirection") Is Nothing Then ViewState.Item("SortDirection") = ""
            Return ViewState.Item("SortDirection")
        End Get
        Set(ByVal value As String)
            ViewState.Item("SortDirection") = value
        End Set
    End Property

    Private Sub initData()
        lblPageCount.Text = PageCount.ToString()
        lblPageNo.Text = PageNo.ToString()
        lblRecordCount.Text = RecordCount.ToString()
    End Sub

    'SQL 字串
    'Public Property SQL() As String
    '    Get
    '        Return ViewState.Item("strSQL")
    '    End Get
    '    Set(ByVal value As String)
    '        bolReload = True
    '        _SQL = value
    '        ViewState.Item("strSQL") = value
    '    End Set
    'End Property

    '對應的GridView控制項名稱
    Public Property GridViewName() As String
        Get
            Return _GridViewName
        End Get
        Set(ByVal value As String)
            _GridViewName = value
        End Set
    End Property

    '筆數
    Public Property RecordCount() As Integer
        Get
            If ViewState.Item("RecordCount") Is Nothing Then
                ViewState.Item("RecordCount") = 0
            End If
            Return Convert.ToInt32(ViewState.Item("RecordCount"))
        End Get
        Set(ByVal value As Integer)
            ViewState.Item("RecordCount") = value
        End Set
    End Property

    Public Property PageCount() As Integer
        Get
            If ViewState.Item("PageCount") Is Nothing Then
                ViewState.Item("PageCount") = 0
            End If
            Return Convert.ToInt32(ViewState.Item("PageCount"))
        End Get
        Set(ByVal value As Integer)
            ViewState.Item("PageCount") = value
        End Set
    End Property

    Public Property PageNo() As Integer
        Get
            If ViewState.Item("PageNo") Is Nothing Then
                ViewState.Item("PageNo") = 0
            End If
            Return Convert.ToInt32(ViewState.Item("PageNo"))
        End Get
        Set(ByVal value As Integer)
            ViewState.Item("PageNo") = value
        End Set
    End Property

    Public Property PerPageRecord() As Integer
        Get
            If ViewState.Item("PerPageRecord") Is Nothing Then
                ViewState.Item("PerPageRecord") = 10
            End If
            Return Convert.ToInt32(ViewState.Item("PerPageRecord"))
        End Get
        Set(ByVal value As Integer)
            ViewState.Item("PerPageRecord") = value
        End Set
    End Property

    Private Sub setPageStatus()
        lblPageCount.Text = PageCount.ToString()
        lblPageNo.Text = PageNo.ToString()
        lblRecordCount.Text = RecordCount.ToString()
        txtPerPageRecord.Text = PerPageRecord.ToString()
        txtGoPage.Text = ""

        ViewState.Item("PerPageRecord") = PerPageRecord
        ViewState.Item("PageCount") = PageCount
        ViewState.Item("PageNo") = PageNo
        ViewState.Item("RecordCount") = RecordCount

        If PageCount = 0 OrElse PageCount = 1 Then
            btnFirstPage.Enabled = False
            btnNextPage.Enabled = False
            btnLastPage.Enabled = False
            btnPreviousPage.Enabled = False
        Else
            btnFirstPage.Enabled = True
            btnNextPage.Enabled = True
            btnLastPage.Enabled = True
            btnPreviousPage.Enabled = True

            If PageNo = 1 Then
                btnFirstPage.Enabled = False
                btnPreviousPage.Enabled = False
            End If
            If PageNo = PageCount Then
                btnLastPage.Enabled = False
                btnNextPage.Enabled = False
            End If
        End If
    End Sub

    'Private Function getDataSet() As Boolean
    '    If _SQL.Trim() = "" Then Return False
    '    Try
    '        _dtTable = BspDB.ExecuteDataSet(Data.CommandType.Text, _SQL).Tables(0)
    '        Return True
    '    Catch ex As Exception
    '        Bsp.Utility.ShowMessage(Me, Bsp.Utility.getInnerException("ucPagerControl：", ex))
    '        Return False
    '    End Try
    'End Function

    Public Sub BindGridView()
        BindGridView(True)
    End Sub

    Private Sub BindGridView(ByVal ReloadThisPage As Boolean)
        If _dtThisPage Is Nothing OrElse ReloadThisPage Then
            If Not getBindingData() Then Return
        End If

        Dim gv As Object = Me.Page.Form.FindControl(_GridViewName)

        If gv IsNot Nothing Then
            CType(gv, GridView).EditIndex = -1
            CType(gv, GridView).SelectedIndex = -1
            CType(gv, GridView).DataSource = _dtThisPage
            CType(gv, GridView).DataBind()
        End If
    End Sub

    Private Function getBindingData() As Boolean
        'If Session.Item("PagerControlData_" & GridViewName) Is Nothing Then
        If DataTable Is Nothing Then
            Return False
            'If Not getDataSet() Then Return False
        End If
        _dtTable = DataTable
        '_dtTable = Session.Item("PagerControlData_" & GridViewName)

        If _dtThisPage IsNot Nothing Then _dtThisPage.Clear()

        _dtThisPage = _dtTable.Clone

        RecordCount = _dtTable.Rows.Count

        If PerPageRecord <= 0 Then PerPageRecord = 10

        If RecordCount Mod PerPageRecord = 0 Then
            PageCount = RecordCount \ PerPageRecord
        Else
            PageCount = RecordCount \ PerPageRecord + 1
        End If

        If PageNo = 0 Then
            PageNo = 1
        Else
            If PageNo > PageCount Then PageNo = PageCount
        End If

        Dim intLCount As Integer = (PageNo - 1) * PerPageRecord
        Dim intUCount As Integer = (PageNo - 1) * PerPageRecord + PerPageRecord - 1

        If RecordCount > 0 Then
            If intUCount + 1 > RecordCount Then
                intUCount = RecordCount - 1
            End If
            If SortExpression = "" Then
                For intCount As Integer = intLCount To intUCount
                    _dtThisPage.ImportRow(_dtTable.Rows(intCount))
                Next intCount
            Else
                Dim dv As Data.DataView = _dtTable.DefaultView
                dv.Sort = SortExpression
                If SortDirection.ToUpper() = "DESENDING" Then
                    dv.Sort = dv.Sort & " Desc"
                End If
                For intCount As Integer = intLCount To intUCount
                    _dtThisPage.ImportRow(dv.Item(intCount).Row)
                Next intCount
            End If
        End If

        setPageStatus()

        Return True
    End Function

    Protected Sub btnPerPageRecord_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnPerPageRecord.Click
        Dim gv As Object = Me.Page.Form.FindControl(_GridViewName)

        If IsNumeric(txtPerPageRecord.Text) Then
            PerPageRecord = Convert.ToInt32(txtPerPageRecord.Text)
            BindGridView(True)
        End If
    End Sub

    Protected Sub btnFirstPage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFirstPage.Click
        PageNo = 1
        BindGridView(False)
    End Sub

    Protected Sub btnLastPage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLastPage.Click
        PageNo = PageCount
        BindGridView(False)
    End Sub

    Protected Sub btnNextPage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNextPage.Click
        PageNo += 1
        If PageNo > PageCount Then PageNo = PageCount

        BindGridView(False)
    End Sub

    Protected Sub btnPreviousPage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPreviousPage.Click
        PageNo -= 1
        If PageNo < 1 Then PageNo = 1

        BindGridView(False)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If lblPageCount.Text <> "" Then PageCount = Convert.ToInt32(lblPageCount.Text)
        If lblPageNo.Text <> "" Then PageNo = Convert.ToInt32(lblPageNo.Text)
        If lblRecordCount.Text <> "" Then RecordCount = Convert.ToInt32(lblRecordCount.Text)
        If Not IsPostBack Then
            If txtPerPageRecord.Text <> PerPageRecord.ToString() Then
                If txtPerPageRecord.Text <> "" AndAlso IsNumeric(txtPerPageRecord.Text) Then
                    PerPageRecord = txtPerPageRecord.Text
                Else
                    txtPerPageRecord.Text = PerPageRecord
                End If
            End If
        Else
            If txtPerPageRecord.Text <> "" AndAlso IsNumeric(txtPerPageRecord.Text) Then
                PerPageRecord = Convert.ToInt32(txtPerPageRecord.Text)
            Else
                txtPerPageRecord.Text = PerPageRecord.ToString()
            End If
        End If
        'If Not IsPostBack Then
        Dim gv As Object = Me.Page.Form.FindControl(GridViewName)

        If gv IsNot Nothing Then
            AddHandler CType(gv, GridView).Sorting, AddressOf gv_Sorting
        End If
        'End If

        'If ViewState.Item("strSQL") IsNot Nothing AndAlso (_SQL Is Nothing OrElse _SQL.Trim() = "") Then
        '    If ViewState.Item("strSQL") IsNot Nothing Then _SQL = ViewState.Item("strSQL")
        'End If
        '_dtTable = DataTable
    End Sub

    Protected Sub btnGoPage_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnGoPage.Click
        If IsNumeric(txtGoPage.Text) Then
            If PageNo > PageCount Then
                PageNo = PageCount
            Else
                PageNo = CInt(txtGoPage.Text)
            End If
            BindGridView(False)
        End If
        txtGoPage.Text = ""
    End Sub

    Protected Sub gv_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs)
        If SortExpression = e.SortExpression.ToString() Then
            If SortDirection = "Ascending" Then
                SortDirection = "Desending"
            Else
                SortDirection = "Ascending"
            End If
        Else
            SortDirection = e.SortDirection.ToString()
            SortExpression = e.SortExpression.ToString()
        End If
        BindGridView()
    End Sub

    Protected Sub txtPerPageRecord_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPerPageRecord.TextChanged
        btnPerPageRecord_Click(sender, Nothing)
    End Sub

    Protected Sub txtGoPage_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGoPage.TextChanged
        btnGoPage_Click(sender, Nothing)
    End Sub
End Class
