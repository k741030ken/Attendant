'****************************************************
'功能說明：多選查詢(MultiSelect)
'建立人員：Chung
'建立日期：2011.05.17
'****************************************************
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports System.Data.Common
Imports System.Data
Imports Newtonsoft.Json

Partial Class Component_PageQueryMultiSelect
    Inherits PageBase

    Public Overrides ReadOnly Property ExcludeLightBarGridName() As String
        Get
            Return "gvMain"
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim aryParam() As Object = Session(Request("ControlSessionID"))

            ViewState.Item("QuerySQL") = aryParam(0)
            ViewState.Item("ReturnColumnIndex") = aryParam(1)
            ViewState.Item("Fields") = aryParam(2)
            ViewState.Item("DefaultQueryField") = aryParam(3)
            ViewState.Item("DefaultQueryValue") = aryParam(4)

            Session.Remove(Request("ControlSessionID"))

            StateMain = QueryData(ViewState.Item("QuerySQL"), "", "")

            If ViewState.Item("DefaultQueryField") IsNot Nothing AndAlso ViewState.Item("DefaultQueryField") <> "" Then
                Bsp.Utility.SetSelectedIndex(ddlField, ViewState.Item("DefaultQueryField"))
                txtQueryString.Text = ViewState.Item("DefaultQueryValue")
            End If
            LoadData()
            'LoadDropDownList()
        End If
    End Sub

    Protected Function QueryData(ByVal strSQL As String, ByVal strField As String, ByVal strFieldData As String) As System.Data.DataTable
        If strField.Trim.Length <> 0 Then
            strSQL = "Select * From ( " & strSQL & " ) a  where [" & strField & "] like '" & strFieldData & "%' "
        End If

        Dim dt As DataTable
        Try
            dt = Bsp.DB.ExecuteDataSet(CommandType.Text, _
                    strSQL, Bsp.DB.getDbParameter("@Field", strField)).Tables(0)

            Dim dc As New DataColumn
            dc.ColumnName = "_Key"
            dc.DataType = System.Type.GetType("System.String")
            dt.Columns.Add(dc)

            For intLoop As Integer = 0 To dt.Rows.Count - 1
                dt.Rows(intLoop).Item("_Key") = intLoop.ToString("00000000")
            Next
            dt.PrimaryKey = New DataColumn() {dc}

            Dim df As System.Web.UI.WebControls.BoundField

            For Each dm As DataColumn In dt.Columns
                If dm.ColumnName <> "_Key" Then
                    If ViewState.Item("Fields") Is Nothing Then
                        df = New System.Web.UI.WebControls.BoundField
                        df.ItemStyle.CssClass = "td_detail"
                        df.HeaderStyle.CssClass = "td_header"
                        df.DataField = dm.ColumnName
                        df.HeaderText = dm.ColumnName
                        gvMain.Columns.Add(df)
                    Else
                        Dim Fields() As FieldState = ViewState.Item("Fields")
                        For intX As Integer = 0 To Fields.Length - 1
                            If dm.ColumnName = Fields(intX).FieldName Then
                                If Fields(intX).Visible Then
                                    df = New System.Web.UI.WebControls.BoundField
                                    df.ItemStyle.CssClass = "td_detail"
                                    df.HeaderStyle.CssClass = "td_header"
                                    df.DataField = Fields(intX).FieldName
                                    df.HeaderText = Fields(intX).HeaderName
                                    df.ItemStyle.Width = Fields(intX).Width
                                    gvMain.Columns.Add(df)

                                    ddlField.Items.Add(New ListItem(Fields(intX).HeaderName, Fields(intX).FieldName))
                                End If
                            End If
                        Next
                    End If
                End If
            Next
            'gvMain.AutoGenerateColumns = False
        Catch ex As Exception
            Throw ex
        End Try
        Return dt
    End Function


    Protected Sub LoadData()
        Dim dv As Data.DataView = CType(StateMain, Data.DataTable).DefaultView

        If ddlField.SelectedValue <> "" Then
            dv.RowFilter = "[" & ddlField.SelectedValue & "] like " & Bsp.Utility.Quote(txtQueryString.Text.Trim() & "%")
        End If
        gvMain.DataSource = dv
        gvMain.DataBind()
    End Sub

    'DropDownList要帶出所有欄位的名稱
    'Protected Sub LoadDropDownList()
    '    If gvMain.Rows.Count > 0 Then
    '        Dim intLoop As Integer
    '        Dim FieldListItem As ListItem

    '        For intLoop = 1 To gvMain.HeaderRow.Cells.Count - 1
    '            FieldListItem = New ListItem
    '            FieldListItem.Text = gvMain.HeaderRow.Cells(intLoop).Text
    '            FieldListItem.Value = gvMain.HeaderRow.Cells(intLoop).Text
    '            ddlField.Items.Add(FieldListItem)
    '        Next
    '    End If
    'End Sub

    Protected Sub gvData_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvMain.PageIndexChanging
        gvMain.PageIndex = e.NewPageIndex
        LoadData()
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnActionC"
                txtReturnValue.Text = ""
                Dim aryColumn() As String = Nothing
                Dim objSelected As TextBox = Me.Page.Form.FindControl("__SelectedRowsgvMain")
                Dim aryKey() As String = objSelected.Text.Split(",")
                Dim dt As DataTable = StateMain
                Dim intIndex As Integer
                Dim strItem As String
                Dim Fields() As FieldState
                Dim ltReturnValue As New List(Of Dictionary(Of String, String))

                If objSelected.Text = "" Then
                    Bsp.Utility.ShowFormatMessage(Me, "W_00000")
                    Return
                End If

                If ViewState.Item("Fields") Is Nothing Then
                    aryColumn = Split(ViewState.Item("ReturnColumnIndex"), ",")
                Else
                    Fields = ViewState.Item("Fields")
                    For intLoop As Integer = 0 To Fields.Length - 1
                        If Fields(intLoop).Return Then
                            If aryColumn Is Nothing Then
                                ReDim aryColumn(0)
                            Else
                                ReDim Preserve aryColumn(aryColumn.GetUpperBound(0) + 1)
                            End If
                            aryColumn(aryColumn.GetUpperBound(0)) = Fields(intLoop).FieldName
                        End If
                    Next
                End If

                For intX As Integer = 0 To aryKey.Length - 1
                    If aryKey(intX).Trim() <> "" Then
                        Dim dr As DataRow = dt.Rows.Find(aryKey(intX))
                        Dim dicData As New Dictionary(Of String, String)

                        strItem = ""
                        If ViewState.Item("Fields") Is Nothing Then
                            For intLoop As Integer = 0 To aryColumn.Length - 1
                                intIndex = CInt(aryColumn(intLoop))
                                '2013.03.28 Chung (U) 改為使用dictionary
                                'If strItem <> "" Then strItem &= "|"
                                'strItem &= dr.Item(intIndex).ToString().Trim()
                                If Not dicData.ContainsKey(dt.Columns(intIndex).ColumnName) Then
                                    dicData.Add(dt.Columns(intIndex).ColumnName, dr.Item(intIndex).ToString().Trim())
                                End If
                            Next
                        Else
                            For intLoop As Integer = 0 To aryColumn.Length - 1
                                '2013.03.28 Chung (U) 改為使用dictionary
                                'If strItem <> "" Then strItem &= "|"
                                'strItem &= dr.Item(aryColumn(intLoop)).ToString().Trim()

                                If Not dicData.ContainsKey(aryColumn(intLoop)) Then
                                    dicData.Add(aryColumn(intLoop), dr.Item(aryColumn(intLoop)).ToString().Trim())
                                End If
                            Next
                        End If
                        '2013.03.28 Chung (U) 改為使用dictionary
                        'If txtReturnValue.Text <> "" Then txtReturnValue.Text &= "|$|"
                        'txtReturnValue.Text &= strItem

                        ltReturnValue.Add(dicData)
                    End If
                Next

                txtReturnValue.Text = JsonConvert.SerializeObject(ltReturnValue)
                Bsp.Utility.RunClientScript(Me, "window.top.returnValue='" & Replace(txtReturnValue.Text, "'", "\'") & "';window.top.close();")
        End Select
    End Sub

    Protected Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        LoadData()
    End Sub

    Protected Sub gvMain_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvMain.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim objGridView As GridView = CType(sender, GridView)
            Dim objKey As TextBox = e.Row.FindControl("txtKey")
            Dim objCheckBox As CheckBox = e.Row.FindControl("chk_GridView")
            e.Row.Attributes.Add("onmouseout", "__pqmsOnmouseout(this, '" & objGridView.ID.ToString() & "', '" & objKey.Text & "', " & e.Row.RowIndex.ToString() & ");")
            e.Row.Attributes.Add("onclick", "__pqmsMultiSelected('" & objGridView.ID.ToString() & "', '" & objCheckBox.ClientID & "', '" & objKey.ClientID & "');")

            Dim objSelected As TextBox = Me.Page.Form.FindControl("__SelectedRows" & objGridView.ID)
            If objSelected IsNot Nothing Then
                If objSelected.Text.ToString().IndexOf(objKey.Text) >= 0 Then
                    objCheckBox.Checked = True
                    e.Row.Style.Item("background-color") = "#b5efff"
                End If
            End If
        End If
    End Sub
End Class
