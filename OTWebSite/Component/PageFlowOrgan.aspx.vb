'****************************************************
'功能說明：比對簽核單位查詢
'建立人員：Beatrice
'建立日期：2016.09.29
'****************************************************
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports System.Data.Common
Imports System.Data

Partial Class Component_PageFlowOrgan
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then

            Dim aryParam() As Object = Session(Request("ControlSessionID"))

            If Not Session(Request("ControlSessionID")) Is Nothing Then
                ViewState.Item("QueryCompID") = aryParam(0)
                ViewState.Item("DefaultFlowOrgan") = aryParam(1)
                ViewState.Item("Fields") = aryParam(2)
                ViewState.Item("QuerySQL") = aryParam(3)

                If ViewState.Item("DefaultFlowOrgan") <> "" Then
                    '自訂內容(已選)
                    LoadRightData()
                End If

                '自訂內容(未選)
                LoadLeftData()
            End If

            LoadField()

            RegistScript()
        End If
    End Sub

    'List Load自訂內容(未選)
    Public Sub LoadLeftData()
        Dim strWhere As String = ""
        Dim strSQL As String = "Select OrganID, OrganID + '-' + OrganName From OrganizationFlow Where CompareFlag = '0'"

        Try
            '帶入公司別
            If ViewState.Item("QueryCompID").ToString <> "" Then
                strWhere = " And CompID = " & Bsp.Utility.Quote(ViewState.Item("QueryCompID").ToString)
            End If

            If txtQueryString.Text.Trim().Length > 0 Then
                strWhere += " And Upper([" & ddlField.SelectedValue & "]) like Upper('%" & txtQueryString.Text.Trim() & "%')"
            End If

            If ViewState.Item("DefaultFlowOrgan").ToString() <> "" Then
                strWhere += " And OrganID Not In (" & ViewState.Item("DefaultFlowOrgan").ToString() & ")"
            End If

            strSQL = strSQL & strWhere & " Order By OrganID"

            Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL, "eHRMSDB").Tables(0)
                With lstLeft
                    .DataSource = dt
                    .DataValueField = dt.Columns(0).ColumnName
                    .DataTextField = dt.Columns(1).ColumnName
                    .DataBind()
                End With
            End Using

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, "PageFlowOrgan.LoadLeftData", ex)
            Return
        End Try

    End Sub

    'List Load自訂內容(已選)
    Public Sub LoadRightData()
        Dim objUC As New UC
        Dim strWhere As String = ""
        Dim strValue As String = ""
        Dim strText As String = ""
        Dim strSQL As String = "Select OrganID, OrganID + '-' + OrganName From OrganizationFlow Where CompareFlag = '0'"

        Try
            lstRight.Items.Clear()
            Dim aryFlowOrgan() As String = ViewState.Item("DefaultFlowOrgan").ToString().Replace("'", "").Split(",")

            For intLoop As Integer = 0 To aryFlowOrgan.GetUpperBound(0)
                If aryFlowOrgan(intLoop) <> "" Then
                    strWhere = " And OrganID = " & Bsp.Utility.Quote(aryFlowOrgan(intLoop))

                    If ViewState.Item("QueryCompID").ToString <> "" Then
                        strWhere = strWhere & " And CompID = " & Bsp.Utility.Quote(ViewState.Item("QueryCompID").ToString)
                    End If

                    Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL & strWhere & strWhere, "eHRMSDB").Tables(0)
                        If dt.Rows.Count > 0 Then
                            '使用控制項ListBox 加入 ListItem
                            '一般來說我們使用控制項Listbox 需要加入值時，就直接打 Listbbox.items.add("something") 但這樣加入時，該筆item的text就是"something"，value也是"something" 
                            '如果我們需要將text跟value指定不同的值時，就需要用到ListItem這個控制項了 注意的是~每次加入ListItem到ListBox時，再加入下一筆，就需要再 "new" 一個ListItem 
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
                            myitem.Value = aryFlowOrgan(intLoop)
                            myitem.Text = aryFlowOrgan(intLoop) + "-"
                            lstRight.Items.Add(myitem)
                            If strValue <> "" Then strValue &= ","
                            If strText <> "" Then strText &= ","
                            strValue &= aryFlowOrgan(intLoop)
                            strText &= ""
                        End If

                    End Using
                End If
            Next

            txtValueResult.Text = strValue
            txtTextResult.Text = strText

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me.Page, "PageFlowOrgan.LoadRightData", ex)
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
