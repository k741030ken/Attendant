'****************************************************
'功能說明：特殊人員設定與維護-新增
'建立人員：Judy,Jason
'建立日期：2017.01.04
'修改日期：2017.02.04
'****************************************************

Imports System.Data
Imports System.Data.Common
Imports System.Diagnostics      'For Debug.Print()

Partial Class OV_OV6001
    Inherits PageBase
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            '公司代碼
            If UserProfile.SelectCompRoleID = "ALL" Then
                lblCompRoleID.Visible = False
                Bsp.Utility.FillHRCompany(ddlCompID)
                Page.SetFocus(ddlCompID)
            Else
                lblCompRoleID.Text = UserProfile.SelectCompRoleName
                hidCompID.Value = UserProfile.SelectCompRoleID
                ddlCompID.Visible = False
            End If
            If UserProfile.SelectCompRoleID = "SPHBK1" Then
                Bsp.Utility.FillDDL(ddlBusinessType, "eHRMSDB", "HRCodeMap", "Code", "CodeCName", Bsp.Utility.DisplayType.Full, "", "And TabName = 'Business' and FldName = 'BusinessType' AND NotShowFlag <> '1' ", "Order By Code")
                ddlBusinessType.Items.Insert(0, New ListItem("---請選擇---", ""))
                ddlBusinessType.SelectedIndex = 0
                hidSelectedBSValue.Value = ""
            End If

            '員工編號
            ucQueryEmp.ShowCompRole = "False"
            ucQueryEmp.InValidFlag = "N"
            ucQueryEmp.SelectCompID = UserProfile.SelectCompRoleID

            subLoadTreeOrgan(tvOrgan, UserProfile.SelectCompRoleID)
            subLoadTreeOrganFlow(tvOrganFlow, UserProfile.SelectCompRoleID)
            subLoadhidTreeOrganFlow(hidtvOrganFlow, UserProfile.SelectCompRoleID)

            '紀錄是否全選
            isCheckAllOrgan.Value = "Y"
            isCheckAllOrganFlow.Value = "Y"
        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnAdd"   '存檔返回
                If funCheckData() Then
                    If SaveData() Then
                        GoBack()
                    End If
                End If
            Case "btnActionX"   '返回
                GoBack()
            Case "btnCancel"    '清除
                ClearData()
        End Select
    End Sub

    Private Sub GoBack()
        tvOrgan.Visible = False
        tvOrganFlow.Visible = False
        Dim ti As TransferInfo = Me.StateTransfer
        Me.TransferFramePage(ti.CallerUrl, Nothing, ti.Args)
    End Sub

    ''' <summary>
    ''' 接收ucQueryEmp回傳的員工編號與姓名，並塞進txtEmpID與lblEmpName中
    ''' </summary>
    ''' <param name="returnValue"></param>
    ''' <remarks></remarks>
    Public Overrides Sub DoModalReturn(ByVal returnValue As String)
        Dim strSql As String = ""

        If returnValue <> "" Then
            Dim aryData() As String = returnValue.Split(":")

            Select Case aryData(0)
                Case "ucQueryEmp"
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    '員工編號
                    txtEmpID.Text = aryValue(1)
                    '員工姓名
                    lblEmpName.Text = aryValue(2)
            End Select
        End If
    End Sub

    Private Sub ClearData()
        txtEmpID.Text = ""
        lblEmpName.Text = ""
        hidchkOrganFlow.Value = ""
        ddlBusinessType.SelectedIndex = 0
        hidSelectedBSValue.Value = ""

        subLoadTreeOrgan(tvOrgan, UserProfile.SelectCompRoleID)
        subLoadTreeOrganFlow(tvOrganFlow, UserProfile.SelectCompRoleID)
        subLoadhidTreeOrganFlow(hidtvOrganFlow, UserProfile.SelectCompRoleID)

        chkAllOrgan.Checked = False
        chkAllOrganFlow.Checked = False

        '紀錄是否全選
        isCheckAllOrgan.Value = "Y"
        isCheckAllOrganFlow.Value = "Y"
    End Sub

    ''' <summary>
    ''' 新增人員前的條件檢查
    ''' </summary>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Private Function funCheckData() As Boolean
        Dim beOverTimeSPUser As New beOverTimeSPUser.Row()
        Dim bsOverTimeSPUser As New beOverTimeSPUser.Service()

        If ddlCompID.SelectedValue = "ALL" And ddlCompID.Visible = True Then
            Bsp.Utility.ShowMessage(Me, "請選擇公司!")
            Return False
        ElseIf ddlCompID.SelectedValue = hidCompID.Value Then
            beOverTimeSPUser.CompID.Value = ddlCompID.SelectedValue
        Else
            beOverTimeSPUser.CompID.Value = hidCompID.Value
        End If

        beOverTimeSPUser.EmpID.Value = txtEmpID.Text.Trim.ToString()

        ''檢查是否有輸入
        If txtEmpID.Text.Trim <> "" And lblEmpName.Text.Trim <> "" Then
            '檢查是否有選擇行政組織跟功能組織
            If tvOrgan.CheckedNodes.Count > 0 Or tvOrganFlow.CheckedNodes.Count > 0 Then
                '檢查資料是否存在
                If bsOverTimeSPUser.IsDataExists(beOverTimeSPUser) Then
                    Bsp.Utility.ShowMessage(Me, "此員工已有設定!")
                    Return False
                End If
            Else
                Bsp.Utility.ShowMessage(Me, "請勾選組織!")
                Return False
            End If
            Return True
        Else
            Bsp.Utility.ShowMessage(Me, "員工編號與員工姓名不得為空")
            Return False
        End If
    End Function

    ''' <summary>
    ''' 儲存資料
    ''' </summary>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Private Function SaveData() As Boolean
        Dim beOverTimeSPUser As New beOverTimeSPUser.Row()
        Dim bsOverTimeSPUser As New beOverTimeSPUser.Service()
        Dim objOV As New OV6
        Dim Arr As New ArrayList()
        Dim ArrFlow As New ArrayList()
        Dim node As New TreeNode
        Dim OrganStr As String = ""
        Dim OrganFlowStr As String = ""
        Dim i As Integer

        '儲存行政組織至ArrayList，並組成字串
        If tvOrgan.CheckedNodes.Count > 0 Then
            For Each node In tvOrgan.CheckedNodes
                Dim checkOrgan As String = node.Value.ToString()
                Arr.Add(checkOrgan)
            Next

            For i = 0 To (Arr.Count - 1)
                If i = 0 Then
                    OrganStr = Arr.Item(i).ToString()
                Else
                    OrganStr &= "," & Arr.Item(i).ToString()
                End If
            Next
        End If

        '儲存功能組織
        '更新暫存
        SaveTempData()
        OrganFlowStr = hidchkOrganFlow.Value.ToString.Trim

        If ddlCompID.Visible = False Then
            beOverTimeSPUser.CompID.Value = hidCompID.Value.ToString()
        Else
            Select Case ddlCompID.SelectedValue
                Case "ALL"
                    Bsp.Utility.ShowMessage(Me, "請選擇公司!")
                    Return False
                Case hidCompID.Value
                    beOverTimeSPUser.CompID.Value = hidCompID.Value.ToString()
                Case Else
                    beOverTimeSPUser.CompID.Value = ddlCompID.SelectedValue.ToString()
            End Select
        End If

        beOverTimeSPUser.EmpID.Value = txtEmpID.Text.Trim.ToString()
        beOverTimeSPUser.OrgList.Value = OrganStr
        beOverTimeSPUser.OrgFlowList.Value = OrganFlowStr
        beOverTimeSPUser.LastChgID.Value = UserProfile.UserID
        beOverTimeSPUser.LastChgComp.Value = UserProfile.ActCompID
        beOverTimeSPUser.LastChgDate.Value = Now

        '儲存資料
        Try
            If AddOTSUsrSetting(beOverTimeSPUser) Then
                Bsp.Utility.ShowMessage(Me, "特殊人員新增成功")
                Return True
            Else
                Bsp.Utility.ShowMessage(Me, "特殊人員新增失敗")
            End If
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, "特殊人員新增失敗")
            Debug.Print("SaveData()-->" + ex.Message)
            Return False
        End Try
        Return False
    End Function

    ''' <summary>
    ''' 新增資料至資料庫的處理動作
    ''' </summary>
    ''' <param name="beOverTimeSPUser"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Private Function AddOTSUsrSetting(ByVal beOverTimeSPUser As beOverTimeSPUser.Row) As Boolean
        Dim bsOverTimeSPUser As New beOverTimeSPUser.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("AattendantDB")
            'Using cn As DbConnection = Bsp.DB.getConnection("testConnectionString")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsOverTimeSPUser.Insert(beOverTimeSPUser, tran) = 0 Then Return False
                tran.Commit()
            Catch ex As Exception
                tran.Rollback()
                Debug.Print("AddOTSUsrSetting()-->" + ex.Message)
                Throw
            Finally
                If tran IsNot Nothing Then tran.Dispose()
            End Try
        End Using
        Return True
    End Function

#Region "部門選單"
    ''' <summary>
    ''' 長出功能組織的treeView
    ''' </summary>
    ''' <param name="objTV"></param>
    ''' <param name="strCompID"></param>
    ''' <remarks></remarks>
    Private Sub subLoadTreeOrganFlow(ByVal objTV As TreeView, ByVal strCompID As String)
        Dim objOV As New OV6
        Dim RoleCode As Integer
        Dim dt As DataTable
        Dim ArrBusinessType As ArrayList
        Dim ArrFlow As New ArrayList()

        Try
            Select Case ddlBusinessType.SelectedValue
                Case ""     '請選擇
                    ArrBusinessType = New ArrayList
                    For Each item As ListItem In ddlBusinessType.Items
                        If item.Value <> "" Then ArrBusinessType.Add(item.Value)
                    Next
                    dt = objOV.GetAllOrgFlowMenu(strCompID, ArrBusinessType)
                Case Else   '其他業務別
                    dt = objOV.GetOrgFlowMenu(strCompID, ddlBusinessType.SelectedValue)
            End Select

            objTV.Nodes.Clear()
            Dim ArrColors As New List(Of ArrayList)()

            If dt.Rows.Count > 0 Then
                For Each item As DataRow In dt.Rows
                    Dim node As New TreeNode
                    node.Value = item("OrganID").ToString().Trim()
                    node.Text = item("OrganName").ToString().Trim()
                    'node.NavigateUrl = item("Color").ToString().Trim()
                    node.Target = item("OrganID").ToString().Trim()
                    node.SelectAction = TreeNodeSelectAction.None
                    node.ExpandAll()

                    objTV.Nodes.Add(node)
                    If objTV.FindNode(item("UpOrganID").ToString()) IsNot Nothing Then
                        tvOrganFlow.FindNode(item("UpOrganID").ToString()).ChildNodes.Add(node)
                    Else
                        Dim BS As Integer = 0
                        If ddlBusinessType.SelectedValue = "" Then
                            '多個Root Node -->依照BusinessType來找正確的根節點
                            Integer.TryParse(item("BusinessType").ToString().Trim(), BS)
                        Else
                            '只有一個Root Node --> objTV.Nodes(0)
                            BS = 1
                        End If
                        Dim findNode As TreeNode = FindNodeByValue(objTV.Nodes((BS - 1).ToString()), item("UpOrganID").ToString().Trim())
                        If findNode IsNot Nothing Then
                            findNode.ChildNodes.Add(node)
                            RoleCode = 0
                            Integer.TryParse(item("RoleCode").ToString().Trim(), RoleCode)
                            If RoleCode < 20 Then
                                findNode.CollapseAll()
                            End If
                        End If
                    End If
                Next
            End If

            '------釋放資源
            Try
                dt.Clear()
                dt.Dispose()
            Catch ex As Exception
                Debug.Print("釋放dt-->" + ex.Message)
            End Try

            '依Query到的資料點選節點
            'Page.SetFocus(tvOrganFlow)
            '拆字串存進ArrayList
            If hidchkOrganFlow.Value.Trim <> "" Then
                'Note: the ","c is a character literal, where as "," is a string literal.
                ArrFlow.AddRange(hidchkOrganFlow.Value.ToString.Split(","c))
                CheckedNodeRecursive(tvOrganFlow, ArrFlow)
            End If

            '如果是全選則讓全選CheckBox為勾選狀態，反之則取消勾選
            Try
                Dim rootNode As TreeNode = tvOrganFlow.Nodes(0)
                If rootNode.Checked = True Then
                    isCheckAllOrganFlow.Value = "N"
                    chkAllOrganFlow.Checked = True
                Else
                    isCheckAllOrganFlow.Value = "Y"
                    chkAllOrganFlow.Checked = False
                End If
            Catch ss As Exception
                Debug.Print("功能組織全選功能-->" + ss.Message)
            End Try

        Catch es As Exception
            Debug.Print("subLoadhTreeOrganFlow-->" + es.Message)
        End Try

        'Using dt As DataTable = objOV.GetOrgFlowMenu(strCompID, ddlBusinessType.SelectedValue)
        '    objTV.Nodes.Clear()
        '    Dim ArrColors As New List(Of ArrayList)()

        '    If dt.Rows.Count > 0 Then
        '        For Each item As DataRow In dt.Rows
        '            Dim node As New TreeNode
        '            node.Value = item("OrganID").ToString().Trim()
        '            node.Text = item("OrganName").ToString().Trim()
        '            'node.NavigateUrl = item("Color").ToString().Trim()
        '            node.Target = item("OrganID").ToString().Trim()
        '            node.SelectAction = TreeNodeSelectAction.None
        '            node.ExpandAll()

        '            objTV.Nodes.Add(node)
        '            If objTV.FindNode(item("UpOrganID").ToString()) IsNot Nothing Then
        '                tvOrganFlow.FindNode(item("UpOrganID").ToString()).ChildNodes.Add(node)
        '            Else
        '                Dim findNode As TreeNode = FindNodeByValue(objTV.Nodes(0), item("UpOrganID").ToString().Trim())
        '                If findNode IsNot Nothing Then
        '                    findNode.ChildNodes.Add(node)
        '                    RoleCode = 0
        '                    Integer.TryParse(item("RoleCode").ToString().Trim(), RoleCode)
        '                    If RoleCode < 20 Then
        '                        findNode.CollapseAll()
        '                    End If
        '                End If
        '            End If
        '        Next
        '    End If
        'End Using

    End Sub
#End Region

#Region "部門選單"
    ''' <summary>
    ''' 長出行政組織的treeView
    ''' </summary>
    ''' <param name="objTV"></param>
    ''' <param name="strCompID"></param>
    ''' <remarks></remarks>
    Private Sub subLoadTreeOrgan(ByVal objTV As TreeView, ByVal strCompID As String)
        Dim objOV As New OV6

        Using dt As DataTable = objOV.GetOrgMenu(strCompID)
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
            End If
        End Using

        '如果是全選則讓全選CheckBox為勾選狀態，反之則取消勾選
        Try
            Dim rootNode As TreeNode = tvOrgan.Nodes(0)
            If rootNode.Checked = True Then
                isCheckAllOrgan.Value = "N"
                chkAllOrgan.Checked = True
            Else
                isCheckAllOrgan.Value = "Y"
                chkAllOrgan.Checked = False
            End If
        Catch ss As Exception
            Debug.Print("行政組織全選功能-->" + ss.Message)
        End Try
    End Sub

    ''' <summary>
    ''' 遞迴尋找Node
    ''' </summary>
    ''' <param name="Node"></param>
    ''' <param name="Value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
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

    ''' <summary>
    ''' 依業務別長出功能組織的treeView
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ddlBusinessType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlBusinessType.SelectedIndexChanged
        Dim ArrFlow As New ArrayList()

        '紀錄使用者目前選擇那些功能組織的node
        'If tvOrganFlow.CheckedNodes.Count > 0 Then
        SaveTempData()
        'End If

        '重新長功能組織的treeView
        subLoadTreeOrganFlow(tvOrganFlow, UserProfile.SelectCompRoleID)

        '更新HiddenFiled
        hidSelectedBSValue.Value = ddlBusinessType.SelectedValue.ToString
    End Sub

    ''' <summary>
    ''' 查詢員工姓名
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>用員工編號和員工的CompID來查詢他的名字</remarks>
    Protected Sub txtEmpID_TextChanged(sender As Object, e As System.EventArgs) Handles txtEmpID.TextChanged
        If txtEmpID.Text.Trim <> "" Then
            Dim objOV As New OV6
            Dim rtnTable As DataTable = objOV.GetEmpName(hidCompID.Value, txtEmpID.Text.Trim)
            If rtnTable.Rows.Count <= 0 Then
                lblEmpName.Text = ""
                Bsp.Utility.ShowFormatMessage(Me, "W_00020", "員工編號查詢姓名")
            Else
                lblEmpName.Text = rtnTable.Rows(0).Item(0)
            End If
        Else
            lblEmpName.Text = ""
        End If
    End Sub

#Region "TreeView全選功能"

    ''' <summary>
    ''' 行政組織的tree之全選功能
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub chkAllOrgan_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkAllOrgan.CheckedChanged
        Dim aNode As TreeNode
        For Each aNode In tvOrgan.Nodes
            CheckedRecursive(aNode, isCheckAllOrgan.Value)
        Next

        Select Case isCheckAllOrgan.Value
            Case "Y"
                isCheckAllOrgan.Value = "N"
            Case "N"
                isCheckAllOrgan.Value = "Y"
        End Select
    End Sub

    ''' <summary>
    ''' 功能組織的tree之全選功能
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub chkAllOrganFlow_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkAllOrganFlow.CheckedChanged
        Dim aNode As TreeNode
        For Each aNode In tvOrganFlow.Nodes
            CheckedRecursive(aNode, isCheckAllOrganFlow.Value)
        Next

        Select Case isCheckAllOrganFlow.Value
            Case "Y"
                isCheckAllOrganFlow.Value = "N"
            Case "N"
                isCheckAllOrganFlow.Value = "Y"
        End Select
    End Sub

    ''' <summary>
    ''' 全選運作Funct
    ''' </summary>
    ''' <param name="aNode"></param>
    ''' <param name="ischk"></param>
    ''' <remarks>如果全選狀態為勾選則勾選全部的node，反之則取消所有的node的勾選</remarks>
    Private Sub CheckedRecursive(ByVal aNode As TreeNode, ByVal ischk As String)
        Dim childNode As TreeNode
        Select Case ischk
            Case "Y"
                aNode.Checked = True
            Case "N"
                aNode.Checked = False
        End Select
        For Each childNode In aNode.ChildNodes
            CheckedRecursive(childNode, ischk)
        Next
    End Sub
#End Region

#Region "遞迴勾選TreeView Node"

    ''' <summary>
    ''' 依照ArrayList的值遞迴勾選TreeView的Node
    ''' </summary>
    ''' <param name="objTV"></param>
    ''' <param name="ArrList"></param>
    ''' <remarks></remarks>
    Private Sub CheckedNodeRecursive(ByVal objTV As TreeView, ByVal ArrList As ArrayList)
        Dim aNode As TreeNode
        For Each aNode In objTV.Nodes
            CheckedRecursive(aNode, ArrList)
        Next
    End Sub

    ''' <summary>
    ''' 遞迴查找此Node下的所有節點，如果有符合的就將他勾選
    ''' </summary>
    ''' <param name="aNode"></param>
    ''' <param name="ArrList"></param>
    ''' <remarks></remarks>
    Private Sub CheckedRecursive(ByVal aNode As TreeNode, ByVal ArrList As ArrayList)
        Dim childNode As TreeNode
        If (ArrList.Contains(aNode.Value.ToString())) Then
            aNode.Checked = True
        End If
        For Each childNode In aNode.ChildNodes
            CheckedRecursive(childNode, ArrList)
        Next
    End Sub

#End Region

#Region "暫存"
    ''' <summary>
    ''' 暫存功能組織
    ''' </summary>
    Private Sub SaveTempData()
        Dim ArrFlow As New ArrayList()
        Dim hidArrFlow As New ArrayList()
        Dim SelectedNode As New TreeNode
        Dim OrganFlowNode As New TreeNode
        Dim hidOrganFlowNode As New TreeNode
        Dim OrganFlowStr As String = ""
        Dim i As Integer
        Try
            '------依BussinessType選擇暫存方式
            Select Case hidSelectedBSValue.Value.ToString
                Case ""     '請選擇
                    '直接取得所有已勾選的node，並儲存至hidtvOrganFlow

                    '重載hidtvOrganFlow
                    subLoadhidTreeOrganFlow(hidtvOrganFlow, UserProfile.SelectCompRoleID)

                    '儲存功能組織至ArrayList，並組成字串
                    If tvOrganFlow.CheckedNodes.Count > 0 Then
                        '抓取使用者勾選的node並組成字串
                        For Each SelectedNode In tvOrganFlow.CheckedNodes
                            Dim checkOrganFlow As String = SelectedNode.Value.ToString()
                            ArrFlow.Add(checkOrganFlow)
                        Next

                        For i = 0 To (ArrFlow.Count - 1)
                            If i = 0 Then
                                OrganFlowStr = ArrFlow.Item(i).ToString()
                            Else
                                OrganFlowStr &= "," & ArrFlow.Item(i).ToString()
                            End If
                        Next
                    End If

                    '拆字串存進ArrayList
                    If OrganFlowStr.Trim <> "" Then
                        'Note: the ","c is a character literal, where as "," is a string literal.
                        ArrFlow.AddRange(OrganFlowStr.Split(","c))

                        '勾選hidtvOrganFlow
                        CheckedNodeRecursive(hidtvOrganFlow, ArrFlow)
                    End If

                    '將字串存進hiddenField
                    hidchkOrganFlow.Value = OrganFlowStr

                Case Else   '其他業務別
                    '依照其BusinessType指向hidtvOrganFlow之正確的Root Node後，開始比對並修改hidtvOrganFlow的值
                    Dim BS As Integer = 0
                    Integer.TryParse(hidSelectedBSValue.Value.ToString.Trim, BS)
                    '-------------
                    ''Debug.Print("hidSelectedBSValue==>" + IIf(hidSelectedBSValue.Value.ToString.Trim <> "", hidSelectedBSValue.Value.ToString.Trim, "Empty Value"))
                    ''Debug.Print("BS==>" + BS.ToString.Trim)
                    '-------------
                    For Each OrganFlowNode In tvOrganFlow.Nodes
                        Dim findNode As TreeNode = FindNodeByValue(hidtvOrganFlow.Nodes((BS - 1).ToString()), OrganFlowNode.Value.ToString.Trim)
                        If findNode IsNot Nothing Then
                            If OrganFlowNode.Checked = True Then
                                findNode.Checked = True
                            Else
                                findNode.Checked = False
                            End If
                            chkCheckRecursive(OrganFlowNode, findNode)
                        End If
                    Next

                    '抓取hidtvOrganFlow中所有已勾選的Node,並放入ArrayList中
                    For Each SelectedNode In hidtvOrganFlow.CheckedNodes
                        Dim checkhidOrganFlow As String = SelectedNode.Value.ToString()
                        hidArrFlow.Add(checkhidOrganFlow)
                    Next

                    For i = 0 To (hidArrFlow.Count - 1)
                        If i = 0 Then
                            OrganFlowStr = hidArrFlow.Item(i).ToString()
                        Else
                            OrganFlowStr &= "," & hidArrFlow.Item(i).ToString()
                        End If
                    Next

                    '拆字串存進ArrayList
                    If OrganFlowStr.Trim <> "" Then
                        'Note: the ","c is a character literal, where as "," is a string literal.
                        ArrFlow.AddRange(OrganFlowStr.Split(","c))

                        '勾選hidtvOrganFlow
                        CheckedNodeRecursive(hidtvOrganFlow, ArrFlow)
                    End If

                    '將字串存進hiddenField
                    hidchkOrganFlow.Value = OrganFlowStr
            End Select
        Catch ex As Exception
            Debug.Print("SaveTempData()-->" + ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' 遞迴比對Node並更新hidchkOrganFlow之node的狀態
    ''' </summary>
    ''' <param name="OrganFlowNode"></param>
    ''' <param name="hidOrganFlowNode"></param>
    ''' <remarks></remarks>
    Private Sub chkCheckRecursive(ByVal OrganFlowNode As TreeNode, ByVal hidOrganFlowNode As TreeNode)
        Dim childNode As TreeNode
        If OrganFlowNode.Checked = True Then
            hidOrganFlowNode.Checked = True
        Else
            hidOrganFlowNode.Checked = False
        End If

        For Each childNode In OrganFlowNode.ChildNodes
            Dim findNode As TreeNode = FindNodeByValue(hidOrganFlowNode, childNode.Value.ToString.Trim)
            If findNode IsNot Nothing Then
                chkCheckRecursive(childNode, findNode)
            End If
        Next
    End Sub

#End Region

    'Protected Sub tvOrganFlow_DataBound(sender As Object, e As TreeNodeEventArgs) Handles tvOrganFlow.DataBound
    '    DataRowView dr = e.Node.DateItem As DataRowView
    '    Dim oRow As Data.DataRowView
    '    Dim oFontColor As System.Drawing.Color = Drawing.Color.Empty
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        oRow = CType(e.Row.DataItem, Data.DataRowView)
    '        Select Case oRow("OrganType")
    '            Case "2-功能組織"
    '                oFontColor = Drawing.Color.Red
    '            Case "3-行政與功能組織"
    '                oFontColor = Drawing.Color.Blue
    '            Case Else
    '                oFontColor = Drawing.Color.Black
    '        End Select

    '        e.Row.Cells(1).ToolTip = oRow("OrganID") & "-" & oRow("OrganName")
    '    End If


#Region "隱藏選單"
    ''' <summary>
    ''' 長出隱藏選單
    ''' </summary>
    ''' <param name="objTV"></param>
    ''' <param name="strCompID"></param>
    ''' <remarks></remarks>
    Private Sub subLoadhidTreeOrganFlow(ByVal objTV As TreeView, ByVal strCompID As String)
        Dim objOV As New OV6
        Dim RoleCode As Integer
        Dim ArrBusinessType As New ArrayList()

        Try
            For Each item As ListItem In ddlBusinessType.Items
                If item.Value <> "" Then ArrBusinessType.Add(item.Value)
            Next

            Using dt As DataTable = objOV.GetAllOrgFlowMenu(strCompID, ArrBusinessType)
                objTV.Nodes.Clear()

                If dt.Rows.Count > 0 Then
                    For Each item As DataRow In dt.Rows
                        Dim node As New TreeNode
                        node.Value = item("OrganID").ToString().Trim()
                        node.Text = item("OrganName").ToString().Trim()
                        node.Target = item("OrganID").ToString().Trim()
                        node.SelectAction = TreeNodeSelectAction.None
                        node.ExpandAll()

                        objTV.Nodes.Add(node)

                        If objTV.FindNode(item("UpOrganID").ToString()) IsNot Nothing Then
                            hidtvOrganFlow.FindNode(item("UpOrganID").ToString().Trim()).ChildNodes.Add(node)
                        Else
                            Dim BS As Integer = 0
                            Integer.TryParse(item("BusinessType").ToString().Trim(), BS)
                            Dim findNode As TreeNode = FindNodeByValue(objTV.Nodes((BS - 1).ToString()), item("UpOrganID").ToString().Trim())
                            If findNode IsNot Nothing Then
                                findNode.ChildNodes.Add(node)
                                RoleCode = 0
                                Integer.TryParse(item("RoleCode").ToString().Trim(), RoleCode)
                                If RoleCode < 20 Then
                                    findNode.CollapseAll()
                                End If
                            End If
                        End If
                    Next
                End If
            End Using
        Catch ex As Exception
            Debug.Print("subLoadhidTreeOrganFlow-->" + ex.Message)
        End Try
    End Sub
#End Region
End Class
