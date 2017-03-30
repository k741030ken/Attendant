'****************************************************
'功能說明：快速查詢網頁(QFind)
'建立人員：Ann
'建立日期：2014.08.20
'****************************************************
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports System.Data.Common
Imports System.Data

Partial Class Component_PageQuerySelectSC
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim aryParam() As Object = Session(Request("ControlSessionID"))

            ViewState.Item("bolCompRole") = aryParam(0)
            ViewState.Item("InValidFlag") = aryParam(5)
            ViewState.Item("SelectCompID") = aryParam(6)

            LoadField()
        End If
    End Sub

    Private Sub LoadField()
        UC.SC_Company(ddlCompID, ViewState.Item("SelectCompID"), CBool(ViewState.Item("bolCompRole")), ViewState.Item("InValidFlag"))
        If ViewState.Item("SelectCompID") = "" Then
            ddlCompID.Items.Insert(0, New ListItem("---請選擇---", ""))
        End If

        UC.SC_DeptID(ddlDeptID, ddlCompID.SelectedItem.Value, ViewState.Item("InValidFlag"))
        ddlDeptID.Items.Insert(0, New ListItem("---請選擇---", ""))

        ddlUserID.Items.Insert(0, New ListItem("---請選擇---", ""))
    End Sub

    Protected Sub ddlCompID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCompID.SelectedIndexChanged
        UC.SC_DeptID(ddlDeptID, ddlCompID.SelectedValue, ViewState.Item("InValidFlag"))
        ddlDeptID.Items.Insert(0, New ListItem("---請選擇---", ""))

        UC.SC_User(ddlUserID, ddlCompID.SelectedValue, ddlDeptID.SelectedValue, ViewState.Item("InValidFlag"))
        ddlUserID.Items.Insert(0, "---請選擇---")
    End Sub

    Protected Sub ddlDeptID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDeptID.SelectedIndexChanged
        UC.SC_User(ddlUserID, ddlCompID.SelectedItem.Value, ddlDeptID.SelectedItem.Value, ViewState.Item("InValidFlag"))
        ddlUserID.Items.Insert(0, New ListItem("---請選擇---", ""))
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnActionC"
                Dim ReturnValue = ""

                If ddlCompID.SelectedValue <> "" Then
                    If ddlUserID.SelectedValue <> "" Then
                        ReturnValue = ddlCompID.SelectedValue & "|$|" & ddlUserID.SelectedValue & "|$|" & ddlUserID.SelectedItem.Text.Substring(7) & "|$|" & ddlCompID.SelectedItem.Text.Substring(7)
                    End If
                End If

                Bsp.Utility.RunClientScript(Me, "window.top.returnValue='" & Replace(ReturnValue, "'", "\'") & "';window.top.close();")
        End Select
    End Sub

    Protected Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        gvMain.Visible = False

        If txtQueryString.Text.Trim() = "" Then
            Bsp.Utility.ShowFormatMessage(Me.Page, "請輸入員編或姓名！")
            Return
        End If

        Dim objUC As New UC()

        Using dt As Data.DataTable = objUC.GetSC_UserByQuery(ddlCompID.SelectedValue, txtQueryString.Text)
            If dt.Rows.Count = 1 Then
                ddlCompID.SelectedValue = dt.Rows(0).Item("CompID").ToString

                UC.SC_DeptID(ddlDeptID, ddlCompID.SelectedItem.Value, ViewState.Item("InValidFlag"))
                ddlDeptID.Items.Insert(0, New ListItem("---請選擇---", ""))
                ddlDeptID.SelectedValue = dt.Rows(0).Item("DeptID").ToString

                UC.SC_User(ddlUserID, ddlCompID.SelectedItem.Value, ddlDeptID.SelectedItem.Value, ViewState.Item("InValidFlag"))
                ddlUserID.Items.Insert(0, New ListItem("---請選擇---", ""))
                ddlUserID.SelectedValue = dt.Rows(0).Item("UserID").ToString
            Else
                gvMain.Visible = True
                subLoadUser()
            End If
        End Using
    End Sub

    Protected Sub subLoadUser()
        Dim objHR As New HR()
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("SELECT DISTINCT U.CompID")
        strSQL.AppendLine(", C.CompName")
        strSQL.AppendLine(", U.UserID")
        strSQL.AppendLine(", U.UserName")
        strSQL.AppendLine(", Dep.DeptID")
        strSQL.AppendLine(", Dep.OrganName AS DeptName")
        strSQL.AppendLine(", U.OrganID")
        strSQL.AppendLine(", Org.OrganName")
        strSQL.AppendLine(", EP.PositionID")
        strSQL.AppendLine(", Po.Remark AS Position")
        strSQL.AppendLine(", E.WorkTypeID")
        strSQL.AppendLine(", WT.Remark AS WorkType")
        If objHR.IsRankIDMapFlag(ddlCompID.SelectedValue) Then
            strSQL.AppendLine(", T.TitleName AS RankTitleName")
        Else
            strSQL.AppendLine(", U.RankID + T.TitleName AS RankTitleName")
        End If
        strSQL.AppendLine(", WS.Remark AS WorkSiteName")
        strSQL.AppendLine("FROM SC_User U")
        strSQL.AppendLine("JOIN SC_Company C ON U.CompID = C.CompID")
        strSQL.AppendLine("JOIN SC_Organization Dep ON U.DeptID = Dep.OrganID")
        strSQL.AppendLine("JOIN SC_Organization Org ON U.OrganID = Org.OrganID")
        strSQL.AppendLine("LEFT JOIN " & Bsp.Utility.getAppSetting("eHRMSDB") & "..EmpPosition EP ON U.CompID = EP.CompID AND U.UserID = EP.EmpID AND EP.PrincipalFlag = '1'")
        strSQL.AppendLine("LEFT JOIN " & Bsp.Utility.getAppSetting("eHRMSDB") & "..Position Po ON U.CompID = Po.CompID AND EP.PositionID = Po.PositionID")
        strSQL.AppendLine("LEFT JOIN " & Bsp.Utility.getAppSetting("eHRMSDB") & "..EmpWorkType E ON U.CompID = E.CompID AND U.UserID = E.EmpID AND E.PrincipalFlag = '1'")
        strSQL.AppendLine("LEFT JOIN " & Bsp.Utility.getAppSetting("eHRMSDB") & "..WorkType WT ON U.CompID = WT.CompID AND E.WorkTypeID = WT.WorkTypeID")
        strSQL.AppendLine("LEFT JOIN " & Bsp.Utility.getAppSetting("eHRMSDB") & "..Title T ON U.CompID = T.CompID AND U.RankID = T.RankID AND U.TitleID = T.TitleID")
        strSQL.AppendLine("LEFT JOIN " & Bsp.Utility.getAppSetting("eHRMSDB") & "..WorkSite WS ON U.CompID = WS.CompID AND U.WorkSiteID = WS.WorkSiteID")
        strSQL.AppendLine("WHERE U.EmpType = '1'")
        If ddlCompID.SelectedValue <> "" Then
            strSQL.AppendLine("AND U.CompID = " & Bsp.Utility.Quote(ddlCompID.SelectedValue))
        End If
        strSQL.AppendLine("AND (Upper(U.UserID) Like Upper('%" & txtQueryString.Text.Trim() & "%') OR Upper(U.UserName) like Upper(N'%" & txtQueryString.Text.Trim() & "%') OR Upper(U.EngName) Like Upper('%" & txtQueryString.Text.Trim() & "%'))")


        Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString).Tables(0)
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

    Protected Sub LoadGV()
        Dim dv As Data.DataView = CType(StateMain, Data.DataTable).DefaultView
        gvMain.DataSource = dv
        gvMain.DataBind()
    End Sub

    Protected Sub gvData_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvMain.PageIndexChanging
        gvMain.PageIndex = e.NewPageIndex
        LoadGV()
    End Sub

    Protected Sub gvMain_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMain.RowCommand
        If e.CommandName = "select" Then
            Dim index As Integer = selectedRow(gvMain)
            Dim ReturnValue = gvMain.DataKeys(index)("CompID").ToString() & "|$|" & gvMain.DataKeys(index)("UserID").ToString() & "|$|" & gvMain.DataKeys(index)("UserName").ToString() & "|$|" & gvMain.DataKeys(index)("CompName").ToString()
            Bsp.Utility.RunClientScript(Me, "window.top.returnValue='" & Replace(ReturnValue, "'", "\'") & "';window.top.close();")
        End If
    End Sub
End Class
