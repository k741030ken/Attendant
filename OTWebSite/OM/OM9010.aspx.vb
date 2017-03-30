'****************************************************
'功能說明：組織圖比較
'建立人員：BeatriceCheng
'建立日期：2016.06.21
'****************************************************
Imports System.Data
Imports System.IO
Imports System.Net
Imports System.Data.Common
Imports Newtonsoft.Json

Partial Class OM_OM9010
    Inherits PageBase

    Public Shared ListOrgan As New Dictionary(Of String, Object)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objOM As New OM9()
            Dim arrFunRight() As String = objOM.funCheckRight()

            For Each FunRight In arrFunRight
                Select Case FunRight
                    Case "I"
                        btnQuery.Visible = True
                    Case "L"
                        btnDownload.Visible = True
                    Case "P"
                        btnPrint.Visible = True
                    Case "X"
                        btnClear.Visible = True
                End Select
            Next

            txtQryDate.DateText = Now.ToString("yyyy/MM/dd")
            subLoadTreeOrgan(tvOrgan, UserProfile.SelectCompRoleID)
        End If
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

        End If
    End Sub

    'Public Overrides Sub DoAction(ByVal Param As String)
    '    Select Case Param
    '        Case "btnQuery"
    '            ClearData()
    '    End Select
    'End Sub

    Private Sub GoBack()
        Dim ti As TransferInfo = Me.StateTransfer
        Me.TransferFramePage(ti.CallerUrl, Nothing, ti.Args)
    End Sub

#Region "部門選單"
    Private Sub subLoadTreeOrgan(ByVal objTV As TreeView, ByVal strCompID As String)
        Dim objOM As New OM9

        Using dt As DataTable = objOM.GetOrgMenu(strCompID, txtQryDate.DateText)
            objTV.Nodes.Clear()
            objTV.ShowLines = False
            Dim ArrColors As New List(Of ArrayList)()

            If dt.Rows.Count > 0 Then
                For Each item As DataRow In dt.Rows
                    If item("SortNode").ToString() = "1" Then
                        Dim node As New TreeNode

                        node.Value = item("OrganID").ToString().Trim()
                        node.Text = item("OrganName").ToString().Trim()
                        node.NavigateUrl = item("Color").ToString().Trim()
                        node.Target = item("OrganID").ToString().Trim()
                        node.SelectAction = TreeNodeSelectAction.None
                        node.ExpandAll()

                        objTV.Nodes.Add(node)
                    End If
                Next

                For Each item As DataRow In dt.Rows
                    If item("SortNode").ToString() = "2" Then
                        Dim node As New TreeNode

                        node.Value = item("OrganID").ToString().Trim()
                        node.Text = item("OrganName").ToString().Trim()
                        node.NavigateUrl = item("Color").ToString().Trim()
                        node.Target = item("OrganID").ToString().Trim()
                        node.SelectAction = TreeNodeSelectAction.None

                        objTV.FindNode(item("OrgType").ToString()).ChildNodes.Add(node)
                    End If
                Next

                For Each item As DataRow In dt.Rows
                    If item("SortNode").ToString() = "3" Then
                        Dim node As New TreeNode

                        node.Value = item("OrganID").ToString().Trim()
                        node.Text = item("OrganName").ToString().Trim()
                        node.NavigateUrl = item("Color").ToString().Trim()
                        node.Target = item("OrganID").ToString().Trim()
                        node.SelectAction = TreeNodeSelectAction.None

                        If objTV.FindNode(item("OrgType").ToString() & "/" & item("DeptID").ToString()) Is Nothing Then
                            If objTV.FindNode(item("OrgType").ToString()) Is Nothing Then
                                If objTV.FindNode(item("DeptID").ToString()) Is Nothing Then
                                    Dim findNode As TreeNode = FindNodeByValue(objTV.Nodes(0), item("DeptID").ToString())
                                    If findNode IsNot Nothing Then
                                        findNode.ChildNodes.Add(node)
                                        findNode.CollapseAll()
                                    End If
                                Else
                                    objTV.FindNode(item("DeptID").ToString()).ChildNodes.Add(node)
                                    objTV.FindNode(item("DeptID").ToString()).CollapseAll()
                                End If
                            Else
                                objTV.FindNode(item("OrgType").ToString()).ChildNodes.Add(node)
                                objTV.FindNode(item("OrgType").ToString()).CollapseAll()
                            End If
                        Else
                            objTV.FindNode(item("OrgType").ToString() & "/" & item("DeptID").ToString()).ChildNodes.Add(node)
                            objTV.FindNode(item("OrgType").ToString() & "/" & item("DeptID").ToString()).CollapseAll()
                        End If

                    End If
                Next

                objTV.ShowCheckBoxes = TreeNodeTypes.All
            End If
        End Using
    End Sub

    Private Function FindNodeByValue(ByVal Node As TreeNode, ByVal Value As String) As TreeNode
        Dim oNode As TreeNode
        Dim oChildNode As TreeNode

        If String.Equals(Node.Value, Value, StringComparison.CurrentCultureIgnoreCase) Then
            Return Node
        End If

        For Each oNode In Node.ChildNodes
            oChildNode = FindNodeByValue(oNode, Value)
            If oChildNode IsNot Nothing Then
                Return oChildNode
            End If
        Next
        Return Nothing
    End Function
#End Region

    <System.Web.Services.WebMethod()> _
    Public Shared Function GetWorkType(ByVal OrganID As String, ByVal QueryDate As String) As String
        Dim objOM As New OM9
        Dim returnValue As New List(Of Dictionary(Of String, Object))()
        Dim TotalCnt As Integer = 0

        Using dt = objOM.GetWorkType(UserProfile.SelectCompRoleID, OrganID, QueryDate)
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim Dictionary As New Dictionary(Of String, Object)
                Dictionary.Add("WorkTypeID", dt.Rows(i).Item("WorkTypeID").ToString.Trim)
                Dictionary.Add("WorkTypeName", dt.Rows(i).Item("WorkTypeName").ToString.Trim)
                returnValue.Add(Dictionary)
            Next
        End Using

        Return JsonConvert.SerializeObject(returnValue)
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function GetPosition(ByVal OrganID As String, ByVal QueryDate As String) As String
        Dim objOM As New OM9
        Dim returnValue As New List(Of Dictionary(Of String, Object))()
        Dim TotalCnt As Integer = 0

        Using dt = objOM.GetPosition(UserProfile.SelectCompRoleID, OrganID, QueryDate)
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim Dictionary As New Dictionary(Of String, Object)
                Dictionary.Add("PositionID", dt.Rows(i).Item("PositionID").ToString.Trim)
                Dictionary.Add("PositionName", dt.Rows(i).Item("PositionName").ToString.Trim)
                returnValue.Add(Dictionary)
            Next
        End Using

        Return JsonConvert.SerializeObject(returnValue)
    End Function


    <System.Web.Services.WebMethod()> _
    Public Shared Function GetAvgData(ByVal OrganID As String, ByVal QueryDate As String, ByVal PositionID As String, ByVal WorkTypeID As String, ByVal OrganFlag As String) As String
        Dim objOM As New OM9
        Dim strSQL As New StringBuilder()
        Dim returnValue As New List(Of Dictionary(Of String, Object))()
        Dim Dictionary As New Dictionary(Of String, Object)

        Using dt = Bsp.DB.ExecuteDataSet(CommandType.StoredProcedure, "SP_OrganizationAnalysis", _
            New DbParameter() { _
                Bsp.DB.getDbParameter("@CompID", UserProfile.SelectCompRoleID), _
                Bsp.DB.getDbParameter("@OrganID", OrganID), _
                Bsp.DB.getDbParameter("@QueryDate", QueryDate), _
                Bsp.DB.getDbParameter("@SalaryFlag", IIf(objOM.funCheckSalary, "Y", "N")), _
                Bsp.DB.getDbParameter("@QueryPositionID", PositionID), _
                Bsp.DB.getDbParameter("@QueryWorkTypeID", WorkTypeID), _
                Bsp.DB.getDbParameter("@OrganFlag", OrganFlag)}, "eHRMSDB").Tables(0)

            If dt.Rows.Count > 0 Then
                Dictionary.Add("totCnt", dt.Rows(0).Item("totCnt").ToString.Trim)
                Dictionary.Add("avgAge", dt.Rows(0).Item("avgAge").ToString.Trim)
                Dictionary.Add("totSalary", dt.Rows(0).Item("totSalary").ToString.Trim)
                Dictionary.Add("avgSen", dt.Rows(0).Item("avgSen").ToString.Trim)
                Dictionary.Add("avgSen_SPHOLD", dt.Rows(0).Item("avgSen_SPHOLD").ToString.Trim)
                Dictionary.Add("avgOrgSen", dt.Rows(0).Item("avgOrgSen").ToString.Trim)
                Dictionary.Add("avgRankSen", dt.Rows(0).Item("avgRankSen").ToString.Trim)
                Dictionary.Add("avgPosSen", dt.Rows(0).Item("avgPosSen").ToString.Trim)
                Dictionary.Add("avgWorkSen", dt.Rows(0).Item("avgWorkSen").ToString.Trim)
                Dictionary.Add("avgFlowSen", dt.Rows(0).Item("avgFlowSen").ToString.Trim)
                returnValue.Add(Dictionary)
            End If
        End Using

        Return JsonConvert.SerializeObject(returnValue)
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function DeleteOrganData(ByVal OrganID As String) As String
        If ListOrgan.ContainsKey(OrganID) Then
            ListOrgan.Remove(OrganID)
        End If

        Return ""
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function GetOrganData(ByVal OrganID As String, ByVal QueryDate As String) As String
        Dim objOM As New OM9
        Dim returnValue As New List(Of Dictionary(Of String, Object))()

        If ListOrgan.ContainsKey(OrganID) Then
            returnValue = ListOrgan.Item(OrganID)
        Else
            Dim TotalCnt As Integer = 0
            Dim strWhere As String = "And O.OrganID in ('" & String.Join("','", GetUnderOrgan(OrganID).ToArray()) & "')"

            Using dt = objOM.GetOrganData(UserProfile.SelectCompRoleID, QueryDate, strWhere)
                For i As Integer = 0 To dt.Rows.Count - 1
                    'TotalCnt = GetTotalCnt(dt.Rows(i).Item("OrganID").ToString.Trim, dt.Rows(i).Item("EmpCnt"), returnValue)
                    Dim Dictionary As New Dictionary(Of String, Object)
                    Dictionary.Add("id", dt.Rows(i).Item("OrganID").ToString.Trim)
                    Dictionary.Add("parent", dt.Rows(i).Item("UpOrganID").ToString.Trim)
                    Dictionary.Add("itemTitleColor", dt.Rows(i).Item("Color").ToString.Trim)
                    Dictionary.Add("title", dt.Rows(i).Item("OrganID").ToString.Trim + dt.Rows(i).Item("OrganName").ToString.Trim)
                    Dictionary.Add("boss", dt.Rows(i).Item("Boss").ToString.Trim + " " + dt.Rows(i).Item("NameN").ToString.Trim)
                    Dictionary.Add("bTitle", dt.Rows(i).Item("TitleName").ToString.Trim)
                    Dictionary.Add("badge", dt.Rows(i).Item("OrgCnt").ToString.Trim)
                    'Dictionary.Add("TotCnt", TotalCnt.ToString)
                    Dictionary.Add("EmpCnt", dt.Rows(i).Item("EmpCnt").ToString)
                    Dictionary.Add("href", "#")
                    Try
                        Dictionary.Add("image", New ST2().EmpPhotoQuery(dt.Rows(i).Item("BossCompID").ToString.Trim, dt.Rows(i).Item("Boss").ToString.Trim))
                    Catch ex As Exception
                        Dictionary.Add("image", "../css/primitives/demo/images/photos/n.png")
                    End Try
                    returnValue.Add(Dictionary)
                Next

                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim OrgID As String = dt.Rows(i).Item("OrganID").ToString.Trim
                    Dim TotCnt As Integer = 0
                    Dim UnderOrgans As List(Of String) = GetUnderOrgan(OrgID)
                    For Each UnderOrganID As String In UnderOrgans
                        Dim dic As Dictionary(Of String, Object) = returnValue.Find(Function(p) p.Item("id") = UnderOrganID)
                        TotCnt = TotCnt + Integer.Parse(dic.Item("EmpCnt"))
                    Next
                    returnValue.Find(Function(p) p.Item("id") = OrgID).Add("TotCnt", TotCnt.ToString())
                Next
            End Using

            ListOrgan.Add(OrganID, returnValue)
        End If

        Return JsonConvert.SerializeObject(returnValue)
    End Function

    Private Shared Function GetTotalCnt(ByVal OrganID As String, ByVal EmpCnt As Integer, ByVal List As List(Of Dictionary(Of String, Object))) As Integer

        Dim ListDic As List(Of Dictionary(Of String, Object)) = List.FindAll(Function(p) p.Item("parent") = OrganID)
        For Each dic As Dictionary(Of String, Object) In ListDic
            EmpCnt = EmpCnt + Integer.Parse(dic.Item("TotCnt"))
        Next

        Return EmpCnt
    End Function

    Private Shared Function GetUnderOrgan(ByVal OrganID As String) As List(Of String)
        Dim ArrValue = New List(Of String)

        Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.StoredProcedure, "SP_GetUnderOrgan", _
            New DbParameter() {Bsp.DB.getDbParameter("@CompID", UserProfile.SelectCompRoleID), Bsp.DB.getDbParameter("@OrganID", OrganID)}, "eHRMSDB").Tables(0)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    ArrValue.Add(dr.Item(0).ToString)
                Next
            End If
        End Using

        Return ArrValue
    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function GetSingleOrganData(ByVal OrganID As String, ByVal SelectedOrganID As String) As String
        Dim returnValue As New List(Of Dictionary(Of String, Object))()

        If ListOrgan.ContainsKey(OrganID) Then
            Dim ListOrganItme As List(Of Dictionary(Of String, Object)) = ListOrgan.Item(OrganID)
            Dim ArrList As List(Of String) = GetUnderOrgan(SelectedOrganID)
            Dim strOrganID As String = ""
            Dim UpOrganID As String = ""

            For Each strOrganID In ArrList
                Dim ListDic As List(Of Dictionary(Of String, Object)) = ListOrganItme.FindAll(Function(p) p.Item("id") = strOrganID Or p.Item("id") = OrganID)
                For Each dic As Dictionary(Of String, Object) In ListDic
                    returnValue.Add(dic)

                    If SelectedOrganID = dic.Item("id").ToString.Trim Then
                        UpOrganID = dic.Item("parent").ToString.Trim
                    End If
                Next
            Next

            If OrganID <> SelectedOrganID Then
                While (UpOrganID <> OrganID)
                    Dim ListDic As List(Of Dictionary(Of String, Object)) = ListOrganItme.FindAll(Function(p) p.Item("id") = UpOrganID)
                    For Each dic As Dictionary(Of String, Object) In ListDic
                        returnValue.Add(dic)

                        UpOrganID = dic.Item("parent").ToString.Trim
                    Next
                End While
            End If
        End If

        Return JsonConvert.SerializeObject(returnValue)
    End Function

    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        ListOrgan.Clear()
        txtQryDate.DateText = Now.Date
        subLoadTreeOrgan(tvOrgan, UserProfile.SelectCompRoleID)
    End Sub

    Protected Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        ListOrgan.Clear()
        subLoadTreeOrgan(tvOrgan, UserProfile.SelectCompRoleID)
    End Sub

    Private Sub ClearData()
        ListOrgan.Clear()
        ClearChecked(tvOrgan.Nodes)
    End Sub

    Private Sub ClearChecked(ByVal Nodes As TreeNodeCollection)
        For Each Node As TreeNode In Nodes
            Node.Checked = False
            ClearChecked(Node.ChildNodes)
        Next
    End Sub

End Class
