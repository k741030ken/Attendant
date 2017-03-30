'****************************************************
'功能說明：快速查詢網頁(QFind)
'建立人員：Ann
'建立日期：2014.08.20
'****************************************************
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports System.Data.Common
Imports System.Data

Partial Class Component_PageQuerySelectUserID
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim aryParam() As Object = Session(Request("ControlSessionID"))

            ViewState.Item("bolCompRole") = aryParam(0)
            ViewState.Item("InValidFlag") = aryParam(5)
            ViewState.Item("SelectCompID") = aryParam(6)    '20150512 wei add 增加選擇公司

            LoadField()
        End If
    End Sub

    'Private Sub LoadField(ByVal SQL As String)
    Private Sub LoadField()
        '公司
        UC.UC_Company(ddlCompID, ViewState.Item("SelectCompID"), CBool(ViewState.Item("bolCompRole")), ViewState.Item("InValidFlag"))
        If ViewState.Item("SelectCompID") = "" Then
            ddlCompID.Items.Insert(0, New ListItem("---請選擇---", ""))
        End If

        '單位
        UC.UC_DeptID(ddlDeptID, ddlCompID.SelectedItem.Value, ViewState.Item("InValidFlag"))
        ddlDeptID.Items.Insert(0, New ListItem("---請選擇---", ""))

        ddlUserID.Items.Insert(0, New ListItem("---請選擇---", ""))
    End Sub

    Protected Sub ddlCompID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCompID.SelectedIndexChanged
        UC.UC_DeptID(ddlDeptID, ddlCompID.SelectedItem.Value, ViewState.Item("InValidFlag"))
        ddlDeptID.Items.Insert(0, New ListItem("---請選擇---", ""))

        UC.UC_User(ddlUserID, ddlCompID.SelectedItem.Value, ddlDeptID.SelectedItem.Value, ViewState.Item("InValidFlag"))
        ddlUserID.Items.Insert(0, "---請選擇---")
    End Sub

    Protected Sub ddlDeptID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDeptID.SelectedIndexChanged
        UC.UC_User(ddlUserID, ddlCompID.SelectedItem.Value, ddlDeptID.SelectedItem.Value, ViewState.Item("InValidFlag"))
        ddlUserID.Items.Insert(0, New ListItem("---請選擇---", ""))
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnActionC"
                Dim ReturnValue = ""
                Dim aryColumn() As String = Nothing

                If ddlCompID.SelectedValue <> "" Then
                    If ddlUserID.SelectedValue <> "" Then
                        ReturnValue = ((ddlCompID.SelectedItem.Text.Substring(7)) & "|$|" & (ddlUserID.SelectedValue) & "|$|" & (ddlUserID.SelectedItem.Text.Substring(7)) & "|$|" & ddlCompID.SelectedValue)
                    End If
                End If

                Bsp.Utility.RunClientScript(Me, "window.top.returnValue='" & Replace(ReturnValue, "'", "\'") & "';window.top.close();")
        End Select
    End Sub

    Private Sub subLoadUserID()

        If ddlCompID.SelectedIndex < 0 Then Return
        ddlUserID.Items.Clear()

        Dim objUC As New UC()
        Try
            Using dt As Data.DataTable = objUC.GetUC_UserInfo(ddlCompID.SelectedItem.Value, ddlDeptID.SelectedItem.Value, ViewState.Item("InValidFlag"))
                With ddlUserID
                    .DataSource = dt
                    .DataTextField = "FullName"
                    .DataValueField = "UserID"
                    .DataBind()

                    .Items.Insert(0, New ListItem("---請選擇---", ""))

                End With
            End Using

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Bsp.Utility.getInnerException("subLoadUserID：", ex))
            Return
        End Try
    End Sub

    Protected Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        gvMain.Visible = False
        If txtQueryString.Text.Trim() = "" Then    'Or ddlCompID.SelectedValue.Trim() = "---請選擇---"
            Bsp.Utility.ShowFormatMessage(Me.Page, "請輸入員編或姓名！")    '公司
            Return
        End If

        Dim objUC As New UC()
        Using dt As Data.DataTable = objUC.GetUC_UserByQuery(ddlCompID.SelectedValue.Trim, txtQueryString.Text.Trim)
            If dt.Rows.Count = 1 Then
                ddlCompID.SelectedValue = dt.Rows(0).Item("CompID").ToString
                UC.UC_DeptID(ddlDeptID, ddlCompID.SelectedItem.Value, ViewState.Item("InValidFlag"))
                ddlDeptID.Items.Insert(0, New ListItem("---請選擇---", ""))
                ddlDeptID.SelectedValue = dt.Rows(0).Item("DeptID").ToString
                UC.UC_User(ddlUserID, ddlCompID.SelectedItem.Value, ddlDeptID.SelectedItem.Value, ViewState.Item("InValidFlag"))
                ddlUserID.Items.Insert(0, New ListItem("---請選擇---", ""))
                ddlUserID.SelectedValue = dt.Rows(0).Item("EmpID").ToString
            Else
                gvMain.Visible = True
                LoadData()
            End If
        End Using

    End Sub

    Protected Sub LoadData()
        Dim objHR As New HR()
        Dim strsQL As String = ""
        strsQL = "Select DISTINCT P.CompID,C.CompName,P.EmpID,P.NameN as Name,EP.PositionID,Po.Remark AS Position,E.WorkTypeID,WT.Remark AS WorkType,P.OrganID,O1.OrganName,P.DeptID,O2.OrganName AS DeptName"
        If objHR.IsRankIDMapFlag(ddlCompID.SelectedValue) Then
            strsQL = strsQL & ",T.TitleName as RankTitleName  "
        Else
            strsQL = strsQL & ",P.RankID + T.TitleName as RankTitleName"
        End If
        strsQL = strsQL & ",WS.Remark AS WorkSiteName  "
        strsQL = strsQL & " FROM Personal P "
        strsQL = strsQL & " JOIN Company C ON P.CompID=C.CompID "
        strsQL = strsQL & " JOIN Organization O1 ON P.OrganID=O1.OrganID "
        strsQL = strsQL & " JOIN Organization O2 ON P.DeptID=O2.OrganID "
        strsQL = strsQL & " LEFT JOIN EmpPosition EP ON P.CompID=EP.CompID AND P.EmpID=EP.EmpID AND EP.PrincipalFlag='1' "
        strsQL = strsQL & " LEFT JOIN Position Po ON P.CompID=Po.CompID AND EP.PositionID=Po.PositionID "
        strsQL = strsQL & " LEFT JOIN EmpWorkType E ON P.CompID=E.CompID AND P.EmpID=E.EmpID AND E.PrincipalFlag='1' "
        strsQL = strsQL & " LEFT JOIN WorkType WT ON P.CompID=WT.CompID AND E.WorkTypeID=WT.WorkTypeID "
        strsQL = strsQL & " LEFT JOIN Title T ON P.CompID=T.CompID AND P.RankID=T.RankID AND P.TitleID=T.TitleID  "
        strsQL = strsQL & " LEFT JOIN WorkSite WS ON P.CompID=WS.CompID AND P.WorkSiteID=WS.WorkSiteID "
        strsQL = strsQL & " Where P.EmpType='1' "
        If ddlCompID.SelectedValue <> "" Then
            strsQL = strsQL & " And P.CompID=" & Bsp.Utility.Quote(ddlCompID.SelectedValue)
        End If
        strsQL = strsQL & " And (Upper(P.EmpID) Like Upper('%" & txtQueryString.Text.Trim() & "%') or Upper(P.Name) Like Upper('%" & txtQueryString.Text.Trim() & "%') or Upper(P.NameN) like Upper(N'%" & txtQueryString.Text.Trim() & "%') or Upper(P.EngName) Like Upper('%" & txtQueryString.Text.Trim() & "%')) "

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
            Dim ReturnValue = gvMain.DataKeys(index)("CompName").ToString() & "|$|" & gvMain.DataKeys(index)("EmpID").ToString() & "|$|" & gvMain.DataKeys(index)("Name").ToString() & "|$|" & gvMain.DataKeys(index)("CompID").ToString()
            Bsp.Utility.RunClientScript(Me, "window.top.returnValue='" & Replace(ReturnValue, "'", "\'") & "';window.top.close();")
        End If
    End Sub
End Class
