'****************************************************
'功能說明：快速查詢網頁(QFind)
'建立人員：Chung
'建立日期：2011.05.17
'****************************************************
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports System.Data.Common
Imports System.Data
Imports Newtonsoft.Json

Partial Class Component_PageQuerySelectHR
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim aryParam() As Object = Session(Request("ControlSessionID"))

            ViewState.Item("QuerySQL") = aryParam(0)
            ViewState.Item("ReturnColumnIndex") = aryParam(1)
            ViewState.Item("Fields") = aryParam(2)
            ViewState.Item("DefaultQueryField") = aryParam(3)
            ViewState.Item("DefaultQueryValue") = aryParam(4)

            Dim strSQL As String = ViewState.Item("QuerySQL").ToString()
            If strSQL.ToUpper().IndexOf("ORDER") >= 0 AndAlso _
                strSQL.Substring(strSQL.ToUpper().IndexOf("ORDER")).ToUpper().IndexOf(" BY ") >= 0 Then
                strSQL = strSQL.Substring(0, strSQL.ToUpper().IndexOf("ORDER") - 1)
                ViewState.Item("QuerySQL_Order") = ViewState.Item("QuerySQL").Substring(Len(strSQL), Len(ViewState.Item("QuerySQL").ToString()) - Len(strSQL))
            End If
            ViewState.Item("QuerySQL") = strSQL

            Session.Remove(Request("ControlSessionID"))

            LoadField(ViewState.Item("QuerySQL").ToString())
            'StateMain = QueryData(ViewState.Item("QuerySQL"), "", "")
            If ViewState.Item("DefaultQueryField") IsNot Nothing AndAlso ViewState.Item("DefaultQueryField") <> "" Then
                Bsp.Utility.SetSelectedIndex(ddlField, ViewState.Item("DefaultQueryField"))
                txtQueryString.Text = ViewState.Item("DefaultQueryValue")
            End If
            LoadData()
            'LoadDropDownList()
        End If
    End Sub

    Private Sub LoadField(ByVal SQL As String)
        SQL = "Select * From ( " & SQL & " ) a Where 1 <> 1"

        Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, SQL, "eHRMSDB").Tables(0)
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
        End Using
    End Sub

    Protected Function QueryData(ByVal strSQL As String, ByVal strField As String, ByVal strFieldData As String) As System.Data.DataTable
        If strField.Trim.Length <> 0 Then
            strSQL = "Select Top 200 * From ( " & strSQL & " ) a  where [" & strField & "] like '%" & strFieldData & "%' " & ViewState.Item("QuerySQL_Order").ToString()
        End If

        Dim dt As DataTable
        Dim df As System.Web.UI.WebControls.BoundField
        Try
            dt = Bsp.DB.ExecuteDataSet(CommandType.Text, _
                    strSQL, Bsp.DB.getDbParameter("@Field", strField), "eHRMSDB").Tables(0)

            Dim dc As New DataColumn
            dc.ColumnName = "_Key"
            dc.DataType = System.Type.GetType("System.String")
            dt.Columns.Add(dc)

            For intLoop As Integer = 0 To dt.Rows.Count - 1
                dt.Rows(intLoop).Item("_Key") = intLoop.ToString("00000000")
            Next
            dt.PrimaryKey = New DataColumn() {dc}

            For Each dm As DataColumn In dt.Columns
                If dm.ColumnName <> "_Key" Then
                    If ViewState.Item("Fields") Is Nothing Then
                        df = New System.Web.UI.WebControls.BoundField
                        df.DataField = dm.ColumnName
                        df.HeaderText = dm.ColumnName
                        gvMain.Columns.Add(df)
                    Else
                        Dim Fields() As FieldState = ViewState.Item("Fields")
                        For intX As Integer = 0 To Fields.Length - 1
                            If dm.ColumnName = Fields(intX).FieldName Then
                                If Fields(intX).Visible Then
                                    df = New System.Web.UI.WebControls.BoundField
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
        Catch ex As Exception
            Throw ex
        End Try
        Return dt
    End Function

    Protected Sub LoadData()
        Dim strsQL As String = ViewState.Item("QuerySQL").ToString()
        If txtQueryString.Text.Trim().Length > 0 Then
            strsQL = "Select Top 200 * From ( " & strsQL & " ) a  where [" & ddlField.SelectedValue & "] like N" & Bsp.Utility.Quote("%" & txtQueryString.Text.Trim() & "%") & " " & ViewState.Item("QuerySQL_Order").ToString()
        Else
            strsQL = "Select Top 200 * From ( " & strsQL & " ) a " & ViewState.Item("QuerySQL_Order").ToString()
        End If
        Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strsQL, "eHRMSDB").Tables(0)
            Dim dc As New DataColumn
            dc.ColumnName = "_Key"
            dc.DataType = System.Type.GetType("System.String")
            dt.Columns.Add(dc)

            For intLoop As Integer = 0 To dt.Rows.Count - 1
                dt.Rows(intLoop).Item("_Key") = intLoop.ToString("00000000")
            Next
            dt.PrimaryKey = New DataColumn() {dc}

            gvMain.DataSource = dt
            gvMain.DataBind()
            StateMain = dt
        End Using
    End Sub

    'Protected Sub LoadData()
    '    Dim dv As Data.DataView = CType(StateMain, Data.DataTable).DefaultView

    '    If ddlField.SelectedValue <> "" Then
    '        dv.RowFilter = "[" & ddlField.SelectedValue & "] like " & BspPub.PubFun.Quote(txtQueryString.Text.Trim() & "%")
    '    End If
    '    gvMain.DataSource = dv
    '    gvMain.DataBind()
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
                Dim intIndex As Integer
                Dim Fields() As FieldState
                Dim returnValue As New Dictionary(Of String, String)

                If selectedRow(gvMain) < 0 Then
                    Bsp.Utility.ShowFormatMessage(Me, "W_00000")
                    Return
                End If

                If ViewState.Item("Fields") Is Nothing Then
                    aryColumn = Split(ViewState.Item("ReturnColumnIndex"), ",")
                    For intLoop As Integer = 0 To aryColumn.Length - 1
                        intIndex = CInt(aryColumn(intLoop)) + 1 '(因多了選取列)
                        'txtReturnValue.Text = txtReturnValue.Text & "|$|" & gvMain.Rows(selectedRow(gvMain)).Cells(intIndex).Text

                        '2013.03.27 Chung (U) 改成用Dictionary轉Json回傳
                        If Not returnValue.ContainsKey(CType(gvMain.Columns(aryColumn(intLoop)), BoundField).DataField) Then
                            returnValue.Add(CType(gvMain.Columns(aryColumn(intLoop)), BoundField).DataField, gvMain.Rows(selectedRow(gvMain)).Cells(intIndex).Text)
                        End If
                    Next
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
                    Dim dt As DataTable = StateMain
                    Dim objKey As TextBox = gvMain.Rows(selectedRow(gvMain)).FindControl("txtKey")
                    Dim dr As DataRow = dt.Rows.Find(objKey.Text)

                    For intLoop As Integer = 0 To aryColumn.Length - 1
                        '2013.03.27 Chung (U) 改成使用Json格式傳遞
                        'txtReturnValue.Text = txtReturnValue.Text & "|$|" & dr.Item(aryColumn(intLoop)).ToString().Trim()
                        If Not returnValue.ContainsKey(aryColumn(intLoop)) Then
                            returnValue.Add(aryColumn(intLoop), dr.Item(aryColumn(intLoop)).ToString().Trim())
                        End If
                    Next
                End If

                '2013.03.27 Chung (U) 改成使用Json格式傳遞
                'txtReturnValue.Text = txtReturnValue.Text.Substring(3)
                txtReturnValue.Text = JsonConvert.SerializeObject(returnValue)

                Bsp.Utility.RunClientScript(Me, "window.top.returnValue='" & Replace(txtReturnValue.Text, "'", "\'") & "';window.top.close();")
        End Select
    End Sub

    Protected Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        LoadData()
    End Sub
End Class
