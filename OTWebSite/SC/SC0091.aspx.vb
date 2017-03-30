Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data
Imports System.Data.SqlClient

Partial Class SC_SC0091
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Bsp.Utility.InStr(UserProfile.ActUserID, "110634", "A05361", "105895") Then
            Bsp.Utility.RunClientScript(Me, "window.top.location='" & Bsp.Utility.getAppSetting("StartPage") & "';")
            Return
        End If

        If Not IsPostBack Then
            Session.Item("sys_ConnectionObject") = DatabaseFactory.CreateDatabase.CreateConnection
        End If
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Dim cn As SqlClient.SqlConnection = CType(Session.Item("sys_ConnectionObject"), SqlClient.SqlConnection)
        Dim strSQL As String = funGetSQL(txtSQL.Text)

        If strSQL = "" Then
            Bsp.Utility.ShowMessage(Me, "未輸入查詢字串！")
            Return
        End If

        If strSQL.ToString.ToUpper.IndexOf("DELETE ") >= 0 Or _
           strSQL.ToString.ToUpper.IndexOf("INSERT ") >= 0 Or _
           strSQL.ToString.ToUpper.IndexOf("UPDATE ") >= 0 Then
            Bsp.Utility.ShowMessage(Me, "此指令包含異動資料的敘述句，請查明後再執行！")
            Return
        End If

        If Session.Item("ExportObject") IsNot Nothing Then
            Session.Remove("ExportObject")
        End If

        Try
            Using dt As DataTable = MyDB.gfunExecuteQuery(strSQL, cn).Tables(0)
                If dt.Rows.Count > 65535 Then
                    Bsp.Utility.ShowMessage(Me, "筆數過多(超過65535列)，無法匯出Excel！")
                    Return
                Else
                    If dt.Rows.Count = 0 Then
                        Bsp.Utility.ShowMessage(Me, "没有資料！")
                        Return
                    End If
                End If
                Session.Item("ExportObject") = dt
                Bsp.Utility.RunClientScript(Me, "OpenWindow('" & Me.ResolveUrl("~/SC/SC0093.aspx") & "');")
            End Using
            getConnectObject()
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, "btnExport_Click", ex)
        End Try
    End Sub

    Private Function funGetSQL(ByVal strOSQL As String) As String
        Dim arySQL() As String = Split(strOSQL.Trim, vbCrLf)
        Dim intLoop As Integer
        Dim strSQL As String = ""
        Dim bolMark As Boolean = False

        For intLoop = 0 To arySQL.GetUpperBound(0)
            If arySQL(intLoop).ToString.Trim.Length > 0 Then
                If Not bolMark Then
                    If arySQL(intLoop).ToString().Trim().Length >= 2 Then
                        If arySQL(intLoop).ToString().Trim().Substring(0, 2) = "/*" Then
                            bolMark = True
                        Else
                            If arySQL(intLoop).ToString().Trim().Substring(0, 2) <> "--" Then
                                strSQL = strSQL & arySQL(intLoop).ToString().Trim() & vbCrLf
                            End If
                        End If
                    Else
                        strSQL = strSQL & arySQL(intLoop).ToString().Trim() & vbCrLf
                    End If
                Else
                    If Right(arySQL(intLoop).Trim(), 2) = "*/" Then
                        bolMark = False
                    End If
                End If
            End If
        Next
        Return strSQL
    End Function

    Protected Sub btnAddServer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddServer.Click
        AddServer()
    End Sub

    Private Sub AddServer()
        Dim strConnStr As String = txtConnStr.Text.Trim()
        Dim bolError As Boolean = False

        If strConnStr <> "" Then

            '測試連線
            Try
                Using cn As SqlConnection = MyDB.gfunGetECConnection(strConnStr)
                    cn.Open()

                    Dim aryItem() As String
                    Dim intLoop As Integer
                    Dim strWording As String = ""
                    aryItem = Split(strConnStr, ";")
                    For intLoop = 0 To aryItem.GetUpperBound(0)
                        If aryItem(intLoop).ToUpper.IndexOf("SERVER") >= 0 Then
                            If strWording = "" Then
                                strWording = aryItem(intLoop).Substring(7)
                            Else
                                strWording = aryItem(intLoop).Substring(7) & "/" & strWording
                            End If
                        ElseIf aryItem(intLoop).ToUpper.IndexOf("DATABASE") >= 0 Then
                            If strWording = "" Then
                                strWording = aryItem(intLoop).Substring(9)
                            Else
                                strWording = strWording & "/" & aryItem(intLoop).Substring(9)
                            End If
                        End If
                    Next
                    ddlServer.Items.Add(New ListItem(strWording, strConnStr))
                    subLoadImportServer()
                    txtConnStr.Text = ""
                End Using

            Catch ex As Exception
                Bsp.Utility.ShowMessage(Me, "連線失敗！Message=" & ex.Message)
            End Try
        End If
    End Sub

    Private Sub subLoadImportServer()
        ddlImportServer.Items.Clear()

        If ddlServer.Items.Count <= 1 Then Exit Sub

        For intLoop As Integer = 0 To ddlServer.Items.Count - 1
            If intLoop <> ddlServer.SelectedIndex Then
                ddlImportServer.Items.Add(ddlServer.Items(intLoop))
            End If
        Next
    End Sub

    Protected Sub ddlServer_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlServer.SelectedIndexChanged
        Dim strConnStr As String = ddlServer.SelectedValue

        If strConnStr = "PD" Then
            Session.Item("sys_ConnectionObject") = DatabaseFactory.CreateDatabase.CreateConnection
            'ddlServer.SelectedIndex = 0
        Else
            Session.Item("sys_ConnectionObject") = MyDB.gfunGetECConnection(strConnStr)
            'ddlServer.SelectedIndex = ddlServer.Items.IndexOf(ddlServer.Items.FindByValue(strConnStr))
        End If

        subLoadImportServer()
    End Sub

    Private Sub getConnectObject()
        If ddlServer.SelectedValue = "PD" Then
            Session.Item("sys_ConnectionObject") = DatabaseFactory.CreateDatabase.CreateConnection
        Else
            Session.Item("sys_ConnectionObject") = MyDB.gfunGetECConnection(ddlServer.SelectedValue)
        End If
    End Sub

    Protected Sub btnExportXML_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportXML.Click
        Dim cn As SqlClient.SqlConnection
        cn = CType(Session.Item("sys_ConnectionObject"), SqlClient.SqlConnection)
        Dim strSQL As String = funGetSQL(txtSQL.Text)

        If strSQL = "" Then
            Bsp.Utility.ShowMessage(Me, "未輸入查詢字串！")
            Return
        End If
        Try
            Using ds As DataSet = MyDB.gfunExecuteQuery(strSQL, cn)
                If ds.Tables(0).Rows.Count = 0 Then
                    Bsp.Utility.ShowMessage(Me, "沒有資料！")
                    Return
                End If
                Session.Item("ExportObject") = ds
                Bsp.Utility.RunClientScript(Me, "OpenWindow('~/SC/SC0095.aspx');")
            End Using
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, "btnExportXML_Click", ex)
        End Try
    End Sub

    Protected Sub btnExportData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportData.Click
        Dim strSQL As String = funGetSQL(txtSQL.Text)
        If strSQL = "" Then
            Bsp.Utility.ShowMessage(Me, "未輸入查詢字串！")
            Return
        End If

        If strSQL.ToString.ToUpper.IndexOf("DELETE ") >= 0 Or _
           strSQL.ToString.ToUpper.IndexOf("INSERT ") >= 0 Or _
           strSQL.ToString.ToUpper.IndexOf("UPDATE ") >= 0 Then
            Bsp.Utility.ShowMessage(Me, "此指令包含異動資料的敘述句，請查明後再執行！")
            Exit Sub
        End If

        If ddlImportServer.Items.Count <= 0 Then
            Exit Sub
        End If

        strSQL = Replace(strSQL, vbCrLf, " ")
        Dim intPosFrom As Integer = strSQL.ToUpper().IndexOf(" FROM ")
        Dim intPosWhere As Integer = strSQL.ToUpper().IndexOf(" WHERE ")
        Dim strTable As String
        If intPosFrom < 0 Then
            Bsp.Utility.ShowMessage(Me, "未具有關鍵字From，此敘述句無效！")
            Exit Sub
        End If
        If intPosWhere < 0 Then
            strTable = strSQL.Substring(intPosFrom + 6).Trim()
        Else
            strTable = strSQL.Substring(intPosFrom + 6, intPosWhere - intPosFrom - 6)
        End If

        If strTable.IndexOf(",") >= 0 OrElse _
           strTable.ToUpper().IndexOf(" JOIN ") >= 0 OrElse _
           strTable.IndexOf(" ") > 0 Then
            Bsp.Utility.ShowMessage(Me, "要匯入資料的話，Select條件式必須為單一Table")
            Exit Sub
        End If

        If strTable.IndexOf(".") >= 0 Then
            Dim aryTable() As String = Split(strTable, ".")
            strTable = aryTable(aryTable.GetUpperBound(0))
        End If


        Dim strSubSQL As String = ""
        Dim aryArgName() As String
        Dim aryPara() As SqlClient.SqlParameter

        Dim cn As SqlConnection = CType(Session.Item("sys_ConnectionObject"), SqlConnection)

        Using cnDesc As SqlConnection = MyDB.gfunGetECConnection(ddlImportServer.SelectedValue)
            cnDesc.Open()

            Try
                Using dtData As DataTable = MyDB.gfunExecuteQuery(strSQL, cn).Tables(0)
                    aryArgName = Array.CreateInstance(GetType(String), dtData.Columns.Count)

                    '組成SQL字串
                    strSQL = "Insert into " & strTable & " ("
                    strSubSQL = "values ("
                    For intLoop As Integer = 0 To dtData.Columns.Count - 1
                        strSQL = strSQL & "[" & dtData.Columns(intLoop).ColumnName & "],"
                        strSubSQL = strSubSQL & "@arg" & dtData.Columns(intLoop).ColumnName & ","
                        aryArgName(intLoop) = "@arg" & dtData.Columns(intLoop).ColumnName
                    Next
                    strSQL = strSQL.Substring(0, strSQL.Length - 1) & ")" & vbCrLf & _
                             strSubSQL.Substring(0, strSubSQL.Length - 1) & ")"

                    aryPara = Array.CreateInstance(GetType(SqlClient.SqlParameter), dtData.Columns.Count)
                    For intLoop As Integer = 0 To dtData.Rows.Count - 1
                        For intX As Integer = 0 To dtData.Columns.Count - 1
                            aryPara(intX) = New SqlClient.SqlParameter("@arg" & dtData.Columns(intX).ColumnName, dtData.Rows(intLoop).Item(intX))
                        Next

                        MyDB.gfunExecAccessSQL(strSQL, aryPara, cnDesc)

                        Array.Clear(aryPara, 0, aryPara.Length)
                    Next
                End Using
                Bsp.Utility.ShowMessage(Me, "匯入資料完畢！")
            Catch ex As Exception
                Bsp.Utility.ShowMessage(Me, "btnExportData_Click", ex)
            End Try
        End Using
    End Sub

    Protected Sub btnLinkServer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLinkServer.Click
        Dim btnC As New ButtonState(ButtonState.emButtonType.Confirm)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)

        CallModalPage("~/SC/SC0094.aspx", "470px", "240px", New ButtonState() {btnC, btnX}, txtConnStr.Text)
        'CallSmallPage("/System/CC0094.aspx", New ButtonState() {btnC, btnX}, txtConnStr.Text)
    End Sub

    Public Overrides Sub DoModalReturn(ByVal returnValue As String)
        txtConnStr.Text = returnValue
        AddServer()
    End Sub
End Class
