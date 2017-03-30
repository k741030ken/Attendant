'****************************************************
'功能說明：Menu
'建立人員：Chung
'建立日期：2011.05.13
'****************************************************
Imports System.Data.SqlClient
Imports System.Data
Imports System.Security.Cryptography

Partial Class SC_SC0030
    Inherits CommonBase

    Implements System.Web.UI.ICallbackEventHandler

    Dim _callBackResult As String

    Public Function GetCallbackResult() As String Implements System.Web.UI.ICallbackEventHandler.GetCallbackResult
        Return _callBackResult
    End Function

    Public Sub RaiseCallbackEvent(ByVal eventArgument As String) Implements System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent
        Try
            _callBackResult = eventArgument.Split(",")(0) & "," & eventArgument.Split(",")(1) & "," & eventArgument.Split(",")(2)
        Catch ex As Exception
            _callBackResult = ""
        End Try
        MyBase.ClearSession()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            If Session("sys_LoginFrom") = "Portal-CC" Then
                Session.Abandon()
                Return
            End If
            'call back 呼叫處理
            Dim eventScript As String = Me.ClientScript.GetCallbackEventReference(Me, "Path", "redirectPage", "")

            Dim strScript As String

            strScript = "  function callPage(Path)"
            strScript &= "  {"
            strScript &= "      " & eventScript
            strScript &= "  }"

            If Not Page.ClientScript.IsClientScriptBlockRegistered(Me.GetType(), "callPage") Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "callPage", strScript, True)
            End If

            subLoadCompRoleID()
            If ddlCompRoleID.Items.Count > 0 Then
                Bsp.Utility.SetSelectedIndex(ddlCompRoleID, UserProfile.ActCompID)
                lblCompRoleID.Text = ddlCompRoleID.SelectedItem.ToString
                UserProfile.SelectCompRoleID = ddlCompRoleID.SelectedValue
                UserProfile.SelectCompRoleName = ddlCompRoleID.SelectedItem.ToString    '20150527 wei add 選擇授權公司
                If ddlCompRoleID.Items.Count > 1 Then
                    ddlCompRoleID.Visible = True
                    lblCompRoleID.Visible = False
                Else
                    ddlCompRoleID.Visible = False
                    lblCompRoleID.Visible = True
                End If
            End If

            GetNode(tvFun)
            If Session("PageFlag") IsNot Nothing Then
                Select Case Session("PageFlag").ToString().ToUpper()
                    Case "TODOLIST"
                        txtToDoList.Text = ViewState.Item("WFPath")
                End Select
                Session.Remove("PageFlag")
            End If
        End If
    End Sub

    Private Function HideFunction(ByVal HideFunID As String, ByVal UserID As String, ByVal ActUserID As String) As Boolean
        If UserID = ActUserID Then Return False
        Return Bsp.Utility.InStr(HideFunID, "Agency", "Overseas")
    End Function

    Private Sub GetNode(ByVal tv As TreeView)
        Dim objSC As New SC()

        tv.Nodes.Clear()

        Dim strPath As String
        Dim strSSODate As String
        Dim strSSODate1 As String           '20160415 emma add 配合證券計薪報表
        Dim strSSOTime As String
        Dim strPolicy As String
        Dim strPolicy_SC1Report As String   '20160415 emma add 配合證券計薪報表

        strSSODate = Now().ToString("yyyy/MM/dd")
        strSSODate1 = Now().ToString("yyyyMMdd")    '20160415 emma add 配合證券計薪報表
        strSSOTime = Now().ToString("HH:mm:ss")

        '20150820 wei add 配合證券法扣
        strPolicy = "Start" & Mid(UserProfile.ActUserID, 3, 4) & "Evaluation" & Mid(strSSODate, 1, 4) & "End"
        Dim MD5hasher As MD5 = MD5.Create()
        Dim myMD5Data As Byte() = MD5hasher.ComputeHash(Encoding.Default.GetBytes(strPolicy))
        Dim strAfterHash As String = ""

        For i As Integer = 0 To myMD5Data.Length - 1
            strAfterHash &= myMD5Data(i).ToString("x2")
            'ToString("X2")中的"X2"是什么意思
            '如果两个数10和26，正常情况十六进制显示0xA、0x1A，这样看起来不整齐，为了好看，我们可以指定X2，这样显示出来就是：0x0A、0x1A
            'msdn里面的string(Format)
        Next

        '20160415 emma add 配合證券計薪報表
        strPolicy_SC1Report = "Start" & Mid(UserProfile.ActUserID, 2, 4) & "SC1Report" & Mid(strSSODate1, 5, 4) & "End"
        Dim MD5hasher_SC1Report As MD5 = MD5.Create()
        Dim myMD5Data_SC1Report As Byte() = MD5hasher.ComputeHash(Encoding.Default.GetBytes(strPolicy_SC1Report))
        Dim strAfterHash_SC1Report As String = ""
        For i As Integer = 0 To myMD5Data_SC1Report.Length - 1
            strAfterHash_SC1Report &= myMD5Data_SC1Report(i).ToString("x2")
            'ToString("X2")中的"X2"是什么意思
            '如果两个数10和26，正常情况十六进制显示0xA、0x1A，这样看起来不整齐，为了好看，我们可以指定X2，这样显示出来就是：0x0A、0x1A
            'msdn里面的string(Format)
        Next

        Using dt As DataTable = objSC.GetAllFunction(UserProfile.UserID, UserProfile.LoginSysID, ddlCompRoleID.SelectedValue)
            Dim drs() As DataRow = dt.Select("ParentFormID = ''")
            For Each dr As DataRow In drs
                If dr.Item("Path").ToString() = "" Then
                    If dt.Select("ParentFormID=" & Bsp.Utility.Quote(dr.Item("FunID").ToString())).Length <= 0 Then
                        Continue For
                    End If
                    If HideFunction(dr.Item("FunID").ToString()) Then Continue For
                End If

                Dim node As New TreeNode

                node.SelectAction = TreeNodeSelectAction.Expand
                node.Text = dr.Item("FunName").ToString() '20140820 wei modify 功能前面顯示代碼 "[" & dr.Item("FunID").ToString() & "]" &
                node.Value = dr.Item("FunID").ToString()
                '取得待辦清單的Path
                If dr.Item("FunID").ToString() = "WFA010" Then
                    ViewState.Item("WFPath") = dr.Item("FunID").ToString() & "," & ResolveUrl(dr.Item("Path").ToString())
                End If
                '20150820 wei add 配合證券法扣
                If dr.Item("Path").ToString() <> "" Then
                    If InStr(dr.Item("Path").ToString(), "http") = 0 Then
                        node.NavigateUrl = "javascript:callPage('" & node.Value & "," & ResolveUrl(dr.Item("Path").ToString()) & "," & UserProfile.SelectCompRoleID & "');"
                    Else
                        '20160415 emma modify 配合證券計薪報表
                        Select Case dr.Item("FunID").ToString()
                            Case "LB0000"   '法扣作業
                                strPath = dr.Item("Path").ToString() & "&SSODate=" & strSSODate & "&SSOTime=" & strSSOTime & "&MD5=" & strAfterHash & "&UserID=" & UserProfile.ActUserID & "&CompID=" & UserProfile.ActCompID
                            Case "PRA000"   '證券計薪報表
                                strPath = dr.Item("Path").ToString() & "&SSODate=" & strSSODate1 & "&SSOTime=" & strSSOTime & "&MD5=" & strAfterHash_SC1Report & "&EmpID=" & UserProfile.ActUserID & "&CompID=" & UserProfile.ActCompID
                        End Select
                        node.NavigateUrl = "javascript:OpenWindow('" & strPath & "','');"
                    End If

                    tv.Nodes.Add(node)  '20150724 wei add
                Else
                    GetNode(node, dt)
                    '20150724 wei add
                    If node.ChildNodes.Count > 0 Then
                        node.CollapseAll()
                        tv.Nodes.Add(node)
                    End If
                    'node.CollapseAll() '20150724 wei del
                End If
                'tv.Nodes.Add(node) 20150724 wei del
            Next
        End Using
    End Sub

    Private Sub GetNode(ByVal ParentNode As TreeNode, ByVal dt As DataTable)
        Dim drs() As DataRow = dt.Select("ParentFormID = " & Bsp.Utility.Quote(ParentNode.Value))

        Dim strPath As String
        Dim strSSODate As String
        Dim strSSODate1 As String           '20160415 emma add 配合證券計薪報表
        Dim strSSOTime As String
        Dim strPolicy As String             '20150820 wei add 配合證券法扣
        Dim strPolicy_SC1Report As String   '20160415 emma add 配合證券計薪報表

        strSSODate = Now().ToString("yyyy/MM/dd")
        strSSODate1 = Now().ToString("yyyyMMdd")    '20160415 emma add 配合證券計薪報表
        strSSOTime = Now().ToString("HH:mm:ss")

        '20150820 wei add 配合證券法扣
        strPolicy = "Start" & Mid(UserProfile.ActUserID, 3, 4) & "Evaluation" & Mid(strSSODate, 1, 4) & "End"
        Dim MD5hasher As MD5 = MD5.Create()
        Dim myMD5Data As Byte() = MD5hasher.ComputeHash(Encoding.Default.GetBytes(strPolicy))
        Dim strAfterHash As String = ""

        For i As Integer = 0 To myMD5Data.Length - 1
            strAfterHash &= myMD5Data(i).ToString("x2")
            'ToString("X2")中的"X2"是什么意思
            '如果两个数10和26，正常情况十六进制显示0xA、0x1A，这样看起来不整齐，为了好看，我们可以指定X2，这样显示出来就是：0x0A、0x1A
            'msdn里面的string(Format)
        Next

        '20160415 emma add 配合證券計薪報表
        strPolicy_SC1Report = "Start" & Mid(UserProfile.ActUserID, 3, 4) & "SC1Report" & Mid(strSSODate1, 5, 4) & "End"
        Dim MD5hasher_SC1Report As MD5 = MD5.Create()
        Dim myMD5Data_SC1Report As Byte() = MD5hasher.ComputeHash(Encoding.Default.GetBytes(strPolicy_SC1Report))
        Dim strAfterHash_SC1Report As String = ""
        For i As Integer = 0 To myMD5Data_SC1Report.Length - 1
            strAfterHash_SC1Report &= myMD5Data_SC1Report(i).ToString("x2")
            'ToString("X2")中的"X2"是什么意思
            '如果两个数10和26，正常情况十六进制显示0xA、0x1A，这样看起来不整齐，为了好看，我们可以指定X2，这样显示出来就是：0x0A、0x1A
            'msdn里面的string(Format)
        Next

        For Each dr As DataRow In drs
            If dr.Item("Path").ToString() = "" Then
                If dt.Select("ParentFormID=" & Bsp.Utility.Quote(dr.Item("FunID").ToString())).Length <= 0 Then
                    Continue For
                End If
                If HideFunction(dr.Item("FunID").ToString()) Then Continue For
            End If

            Dim node As New TreeNode()

            node.SelectAction = TreeNodeSelectAction.Expand
            node.Text = dr.Item("FunID").ToString() & dr.Item("FunName").ToString() '20140820 wei modify 功能前面顯示代碼
            node.Value = dr.Item("FunID").ToString()
            '取得待辦清單的Path
            If dr.Item("FunID").ToString() = "WFA010" Then
                ViewState.Item("WFPath") = dr.Item("FunID").ToString() & "," & ResolveUrl(dr.Item("Path").ToString())
            End If
            '20150820 wei add 配合證券法扣
            If dr.Item("Path").ToString() <> "" Then
                If InStr(dr.Item("Path").ToString(), "http") = 0 Then
                    node.NavigateUrl = "javascript:callPage('" & node.Value & "," & ResolveUrl(dr.Item("Path").ToString()) & "," & UserProfile.SelectCompRoleID & "');"
                Else
                    '20160415 emma modify 配合證券計薪報表
                    Select Case dr.Item("FunID").ToString()
                        Case "LB0000"   '法扣作業
                            strPath = dr.Item("Path").ToString() & "&SSODate=" & strSSODate & "&SSOTime=" & strSSOTime & "&MD5=" & strAfterHash & "&UserID=" & UserProfile.ActUserID & "&CompID=" & UserProfile.ActCompID
                        Case "PRA000"   '證券計薪報表
                            strPath = dr.Item("Path").ToString() & "&SSODate=" & strSSODate1 & "&SSOTime=" & strSSOTime & "&MD5=" & strAfterHash_SC1Report & "&EmpID=" & UserProfile.ActUserID & "&CompID=" & UserProfile.ActCompID
                    End Select

                    node.NavigateUrl = "javascript:OpenWindow('" & strPath & "','');"
                    'node.NavigateUrl = "javascript:OpenWindow('" & dr.Item("Path").ToString() & "','');"
                End If
                ParentNode.ChildNodes.Add(node) '20150724 wei add
            Else
                GetNode(node, dt)
                '20150724 wei add
                If node.ChildNodes.Count > 0 Then
                    node.CollapseAll()
                    ParentNode.ChildNodes.Add(node)
                End If
                'node.CollapseAll() '20150724 wei del
            End If

            'ParentNode.ChildNodes.Add(node)    20150724 wei del
        Next
    End Sub

    Private Function HideFunction(ByVal HideFunID As String) As Boolean
        If UserProfile.UserID = UserProfile.ActUserID Then Return False
        Return Bsp.Utility.InStr(HideFunID, "Agency", "Overseas")
    End Function
    '20140808 wei add 載入授權公司
    Private Sub subLoadCompRoleID()
        Dim objSC As New SC()

        Try
            Using dt As Data.DataTable = objSC.GetCompRoleID(UserProfile.UserID,UserProfile.LoginSysID)
                With ddlCompRoleID
                    .DataSource = dt
                    .DataTextField = "CompName"
                    .DataValueField = "CompRoleID"
                    .DataBind()

                End With
            End Using

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, "subLoadCompRoleID", ex)
            Throw
            Return
        End Try
    End Sub

    Protected Sub ddlCompRoleID_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlCompRoleID.SelectedIndexChanged
        'call back 呼叫處理
        Dim eventScript As String = Me.ClientScript.GetCallbackEventReference(Me, "Path", "redirectPage", "")

        Dim strScript As String

        strScript = "  function callPage(Path)"
        strScript &= "  {"
        strScript &= "      " & eventScript
        strScript &= "  }"

        If Not Page.ClientScript.IsClientScriptBlockRegistered(Me.GetType(), "callPage") Then
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "callPage", strScript, True)
        End If

        UserProfile.SelectCompRoleID = ddlCompRoleID.SelectedValue
        UserProfile.SelectCompRoleName = ddlCompRoleID.SelectedItem.ToString    '20150528 wei add 選擇授權公司
        GetNode(tvFun)

        '回Default
        'Bsp.Utility.RunClientScript(Me, "window.parent.frames[3].location = 'SC0040.aspx';")
        '20150319 wei add
        Dim objSC As New SC()
        Dim strPath As String = ""
        strPath = objSC.GetFunctionByCompRoleID(UserProfile.UserID, UserProfile.LoginSysID, ddlCompRoleID.SelectedValue, UserProfile.SelectFunID)
        If strPath = "" Then
            Bsp.Utility.RunClientScript(Me, "window.parent.frames[3].location = 'SC0040.aspx';")
        Else
            Dim strScript1 As String = ""
            strScript1 = "window.onload = function () {redirectPage('" + UserProfile.SelectFunID + "," + ResolveUrl(strPath) + "," + ddlCompRoleID.SelectedValue + "');};"

            If Not Page.ClientScript.IsClientScriptBlockRegistered(Me.GetType(), "WindowOnload") Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "WindowOnload", strScript1, True)
            End If
        End If
        

    End Sub
    
End Class
