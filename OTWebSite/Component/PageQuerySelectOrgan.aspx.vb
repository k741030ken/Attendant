'****************************************************
'功能說明：快速查詢網頁(QFind)
'建立人員：Ann
'建立日期：2014.08.20
'****************************************************
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports System.Data.Common
Imports System.Data

Partial Class Component_PageQuerySelectOrgan
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim aryParam() As Object = Session(Request("ControlSessionID"))

            ViewState.Item("bolCompRole") = aryParam(0)
            ViewState.Item("InValidFlag") = aryParam(1)
            ViewState.Item("SelectCompID") = aryParam(2)
            ViewState.Item("Fields") = aryParam(3)

            LoadField()
        End If
    End Sub

    Private Sub LoadField()
        Dim strWhere As String = ""

        If ViewState.Item("SelectCompID") <> "" Then
            strWhere = strWhere & " And CompID = " & Bsp.Utility.Quote(ViewState.Item("SelectCompID"))
        End If

        If ViewState.Item("InValidFlag") = "N" Then
            strWhere = strWhere & " And InValidFlag = '0'"
        End If

        Bsp.Utility.FillDDL(ddlCompID, "eHRMSDB", "Company", "CompID", "CompName + Case When InValidFlag = '1' Then '(無效)' Else '' End", Bsp.Utility.DisplayType.Full, "", strWhere, "Order By InValidFlag, CompID")
        If ViewState.Item("SelectCompID") = "" Then
            ddlCompID.Items.Insert(0, New ListItem("---請選擇---", ""))
        End If

        '組織類型
        Dim Fields() As FieldState = ViewState.Item("Fields")
        For intX As Integer = 0 To Fields.Length - 1
            ddlOrgType.Items.Add(New ListItem(Fields(intX).HeaderName, Fields(intX).FieldName))
        Next

        LoadOrganID(Nothing, Nothing)

        tblMain.Visible = False
    End Sub

    Protected Sub LoadOrganID(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim strWhere As String = ""

        If ddlCompID.SelectedValue <> "" Then
            strWhere = strWhere & " And CompID = " & Bsp.Utility.Quote(ddlCompID.SelectedValue)
        End If

        If ViewState.Item("InValidFlag") = "N" Then
            strWhere = strWhere & " And InValidFlag = '0'"
        End If

        If ddlOrgType.SelectedValue = "OrganizationWait" Then
            strWhere = strWhere & " And OrganReason = '1' And WaitStatus = '0'"
        End If

        Bsp.Utility.FillDDL(ddlOrganID, "eHRMSDB", ddlOrgType.SelectedValue, "OrganID", "OrganName + Case When InValidFlag = '1' Then '(無效)' Else '' End", Bsp.Utility.DisplayType.Full, "", strWhere, "Order By InValidFlag, OrganID")
        ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnActionC"
                Dim ReturnValue = ""
                Dim aryColumn() As String = Nothing

                If ddlCompID.SelectedValue <> "" Then
                    If ddlOrganID.SelectedValue <> "" Then
                        ReturnValue = (ddlCompID.SelectedValue & "|$|" & (ddlCompID.SelectedItem.Text.Split("-")(1)) & "|$|" & (ddlOrganID.SelectedValue) & "|$|" & (ddlOrganID.SelectedItem.Text.Split("-")(1)) & "|$|" & ddlOrgType.SelectedValue)
                    End If
                End If

                Bsp.Utility.RunClientScript(Me, "window.top.returnValue='" & Replace(ReturnValue, "'", "\'") & "';window.top.close();")
        End Select
    End Sub

    Protected Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        tblMain.Visible = False
        If txtQueryString.Text.Trim() = "" Then
            Bsp.Utility.ShowMessage(Me.Page, "請輸入單位代碼或名稱！")
            Return
        End If

        Dim strSQL As New StringBuilder()
        strSQL.AppendLine("SELECT C.CompID")
        strSQL.AppendLine(", C.CompName")
        strSQL.AppendLine(", O.OrganID")
        strSQL.AppendLine(", O.OrganName")
        strSQL.AppendLine(", CASE O.InValidFlag WHEN '0' THEN '有效' ELSE '無效' END InValidFlag")
        strSQL.AppendLine("FROM " + ddlOrgType.SelectedValue + " O")
        strSQL.AppendLine("LEFT JOIN Company C ON C.CompID = O.CompID")
        strSQL.AppendLine("WHERE 1=1")
        If ddlCompID.SelectedValue <> "" Then
            strSQL.AppendLine("AND O.CompID = " & Bsp.Utility.Quote(ddlCompID.SelectedValue))
        End If
        If ddlOrgType.SelectedValue = "OrganizationWait" Then
            strSQL.AppendLine("AND O.OrganReason = '1' AND O.WaitStatus = '0'")
        End If
        If ViewState.Item("InValidFlag") = "N" Then
            strSQL.AppendLine("AND O.InValidFlag = '0'")
        End If
        strSQL.AppendLine("AND (")
        strSQL.AppendLine("Upper(O.OrganID) LIKE Upper('%" & txtQueryString.Text.Trim() & "%')")
        strSQL.AppendLine("OR Upper(O.OrganName) LIKE Upper('%" & txtQueryString.Text.Trim() & "%')")
        strSQL.AppendLine("OR Upper(O.OrganEngName) LIKE Upper('%" & txtQueryString.Text.Trim() & "%')")
        strSQL.AppendLine(")")

        Using dt As Data.DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
            If dt.Rows.Count = 1 Then
                ddlCompID.SelectedValue = dt.Rows(0).Item("CompID").ToString
                LoadOrganID(Nothing, Nothing)
                ddlOrganID.SelectedValue = dt.Rows(0).Item("OrganID").ToString
            Else
                tblMain.Visible = True

                pcMain.DataTable = dt
                ddlOrganID.SelectedValue = ""
            End If
        End Using
    End Sub

    Protected Sub gvMain_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMain.RowCommand
        If e.CommandName = "select" Then
            Dim index As Integer = selectedRow(gvMain)
            Dim ReturnValue = gvMain.DataKeys(index)("CompID").ToString() & "|$|" & gvMain.DataKeys(index)("CompName").ToString() & "|$|" & gvMain.DataKeys(index)("OrganID").ToString() & "|$|" & gvMain.DataKeys(index)("OrganName").ToString() & "|$|" & ddlOrgType.SelectedValue
            Bsp.Utility.RunClientScript(Me, "window.top.returnValue='" & Replace(ReturnValue, "'", "\'") & "';window.top.close();")
        End If
    End Sub
End Class
