'****************************************************
'功能說明：快速查詢網頁(QFind)
'建立人員：Ann
'建立日期：2013.11.27
'****************************************************
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports System.Data.Common
Imports System.Data

Partial Class Component_PageMultiListBox
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then

            Dim strSQL As String = ""
            Dim aryParam() As Object = Session(Request("ControlSessionID"))

            If Not Session(Request("ControlSessionID")) Is Nothing And aryParam(0) <> "" Then
                ViewState.Item("QuerySQL") = aryParam(0)
                ViewState.Item("ConnStr") = aryParam(1)
                ViewState.Item("Fields") = aryParam(2)
                ViewState.Item("DefaultField") = aryParam(3)
                ViewState.Item("DefaultValue") = aryParam(4)

                strSQL = ViewState.Item("QuerySQL")
                If strSQL.ToUpper().IndexOf("ORDER") >= 0 AndAlso _
                    strSQL.Substring(strSQL.ToUpper().IndexOf("ORDER")).ToUpper().IndexOf(" BY ") >= 0 Then
                    strSQL = strSQL.Substring(0, strSQL.ToUpper().IndexOf("ORDER") - 1)
                    ViewState.Item("OrderBy") = ViewState.Item("QuerySQL").Substring(Len(strSQL), Len(ViewState.Item("QuerySQL").ToString()) - Len(strSQL))
                End If
                ViewState.Item("QuerySQL") = strSQL

                If ViewState.Item("DefaultValue") <> "" Then
                    '自訂內容(已選)
                    LoadRightData()
                End If

                '自訂內容(未選)
                LoadLeftData()

                If Not aryParam(2) Is Nothing Then
                    LoadField()
                End If
            End If

            RegistScript()
        End If

        '原來版本-----
        If txtLeftResult.Text.Length > 0 Or txtRightResult.Text.Length > 0 Then
            RebuildList()
        End If

        If Not Page.ClientScript.IsClientScriptBlockRegistered("UserControl") Then
            Dim cstype As Type = Me.Page.GetType()
            Dim script As String = "<script type='text/javascript' src='" & Me.ResolveUrl("~/ClientFun/UserControl.js") & "'></script>" & vbNewLine
            Me.Page.ClientScript.RegisterClientScriptBlock(cstype, "PageSchool", script)
        End If

    End Sub

    'List Load自訂內容(未選)
    Public Sub LoadLeftData()
        Try
            Dim strSQL As String = ViewState.Item("QuerySQL") & " Where 1=1"

            If ViewState.Item("DefaultField") <> "" And ViewState.Item("DefaultValue") <> "" Then
                strSQL &= " And [" & ViewState.Item("DefaultField") & "] Not In ( " & ViewState.Item("DefaultValue") & ")"
            End If

            If txtQueryString.Text.Trim().Length > 0 Then
                strSQL &= " And [" & ddlField.SelectedValue & "] like N'%" & txtQueryString.Text.Trim() & "%'"
            End If

            Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL & ViewState.Item("OrderBy"), ViewState.Item("ConnStr")).Tables(0)
                With lstLeft
                    .DataSource = dt
                    .DataValueField = dt.Columns(0).ColumnName
                    .DataTextField = dt.Columns(1).ColumnName
                    .DataBind()

                    '扣除已選取的資料
                    If txtRightResult.Text.Trim() <> "" Then
                        Dim strLists() As String = txtRightResult.Text.Split(CChar(vbTab))
                        For intLoop As Integer = 0 To strLists.GetUpperBound(0)
                            Dim intIndex As Integer = .Items.IndexOf(.Items.FindByValue(strLists(intLoop).Split("|")(1)))

                            If intIndex >= 0 Then
                                .Items.RemoveAt(intIndex)
                            End If
                        Next
                    End If
                End With
            End Using

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, "PageSchool.LoadLeftData", ex)
            Return
        End Try

    End Sub

    'List Load自訂內容(已選)
    Public Sub LoadRightData()
        Dim strValue As String = ""
        Dim strText As String = ""

        Try
            lstRight.Items.Clear()
            Dim aryValue() As String = ViewState.Item("DefaultValue").ToString().Replace("'", "").Split(",")
            Dim strSQL As String = ""

            For intLoop As Integer = 0 To aryValue.GetUpperBound(0)
                If aryValue(intLoop) <> "" Then
                    If ViewState.Item("DefaultField") <> "" Then
                        strSQL = " Where [" & ViewState.Item("DefaultField") & "] = '" & aryValue(intLoop) & "'"
                    End If

                    Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, ViewState.Item("QuerySQL") & strSQL, ViewState.Item("ConnStr")).Tables(0)
                        If dt.Rows.Count > 0 Then
                            '使用控制項ListBox 加入 ListItem
                            '一般來說我們使用控制項Listbox 需要加入值時，就直接打 Listbbox.items.add("whohow") 但這樣加入時，該筆item的text就是"whohow"，value也是"whohow" 
                            '如果我們需要將text跟value指定不同的值時，就需要用到ListItem這個控制項了 注意的是~每次加入ListItem到ListBox時，再加入下一筆，就需要再 "new" 一個ListItem 
                            'lstRight.Items.Add(dt.Rows(0).Item(1).ToString())
                            Dim myitem As New ListItem
                            myitem.Value = Trim(dt.Rows(0).Item(0).ToString())
                            myitem.Text = Trim(dt.Rows(0).Item(1).ToString())
                            lstRight.Items.Add(myitem)

                            If strValue <> "" Then strValue &= ","
                            If strText <> "" Then strText &= ","
                            strValue &= dt.Rows(0).Item(0).ToString()
                            strText &= dt.Rows(0).Item(1).ToString()
                        Else
                            Dim myitem As New ListItem
                            myitem.Value = aryValue(intLoop)
                            myitem.Text = aryValue(intLoop) + "-"
                            lstRight.Items.Add(myitem)
                            If strValue <> "" Then strValue &= ","
                            If strText <> "" Then strText &= ","
                            strValue &= aryValue(intLoop)
                            strText &= ""
                        End If

                    End Using
                End If
            Next

            txtValueResult.Text = strValue
            txtTextResult.Text = strText

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me.Page, "PageSchool.LoadRightData", ex)
            Return
        End Try

    End Sub

    '清除List內容
    Public Sub ClearList()
        lstLeft.Items.Clear()
        lstRight.Items.Clear()
        txtLeftResult.Text = ""
        txtRightResult.Text = ""
        txtValueResult.Text = ""
        txtTextResult.Text = ""
    End Sub

    Private Sub RebuildList()
        Try
            lstRight.Items.Clear()
            lstLeft.Items.Clear()
            Dim strLists() As String
            If txtRightResult.Text.Length > 0 Then
                strLists = txtRightResult.Text.Split(CChar(vbTab))
                For Each strKey As String In strLists
                    lstRight.Items.Add(New ListItem(strKey.Split("|")(0), strKey.Split("|")(1)))
                Next
            End If

            If txtLeftResult.Text.Length > 0 Then
                strLists = txtLeftResult.Text.Split(CChar(vbTab))
                For Each strKey As String In strLists
                    lstLeft.Items.Add(New ListItem(strKey.Split("|")(0), strKey.Split("|")(1)))
                Next
            End If
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me.Page, Bsp.Utility.getInnerException("PageSchool.RebuildList", ex))
            Return
        End Try
    End Sub

    Private Sub RegistScript()
        Const CS_FUN As String = "javascript:{0}(this.form.{1},this.form.{2},this.form.{3},this.form.{4},this.form.{5},this.form.{6});return false;"
        btnMoveLeft.Attributes("OnClick") = String.Format(CS_FUN, "MoveLeft", lstLeft.ClientID, txtLeftResult.ClientID, lstRight.ClientID, txtRightResult.ClientID, txtTextResult.ClientID, txtValueResult.ClientID)
        btnMoveRight.Attributes("OnClick") = String.Format(CS_FUN, "MoveRight", lstLeft.ClientID, txtLeftResult.ClientID, lstRight.ClientID, txtRightResult.ClientID, txtTextResult.ClientID, txtValueResult.ClientID)
        btnMoveUp.Attributes("OnClick") = String.Format(CS_FUN, "MoveUp", lstLeft.ClientID, txtLeftResult.ClientID, lstRight.ClientID, txtRightResult.ClientID, txtTextResult.ClientID, txtValueResult.ClientID)
        btnMoveDown.Attributes("OnClick") = String.Format(CS_FUN, "MoveDown", lstLeft.ClientID, txtLeftResult.ClientID, lstRight.ClientID, txtRightResult.ClientID, txtTextResult.ClientID, txtValueResult.ClientID)
    End Sub

    '回傳結果
    Public Overrides Sub DoAction(ByVal Param As String)
        Dim strValueSql As String = ""

        Select Case Param
            Case "btnActionC"
                Dim aryValue() As String = Split(txtValueResult.Text, ",")
                For intLoop As Integer = 0 To aryValue.GetUpperBound(0)
                    If strValueSql <> "" Then strValueSql += ","
                    strValueSql += "'" & aryValue(intLoop) & "'"
                Next

                Bsp.Utility.RunClientScript(Me, "window.top.returnValue='" & Replace(strValueSql, "'", "\'") & "';window.top.close();")
        End Select
    End Sub

    '載入查詢的下拉式選單值
    Private Sub LoadField()
        Dim Fields() As FieldState = ViewState.Item("Fields")
        For intX As Integer = 0 To Fields.Length - 1
            ddlField.Items.Add(New ListItem(Fields(intX).HeaderName, Fields(intX).FieldName))
        Next
    End Sub

    '查詢
    Protected Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        LoadLeftData()
    End Sub

End Class
