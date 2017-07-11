'****************************************************
'功能說明：打卡特殊人員設定-修改/明細
'建立人員：Jason
'建立日期：2017.07.11
'修改日期：
'****************************************************

Imports System.Data
Imports System.Data.Common
Imports System.Diagnostics      'For Debug.Print()

Partial Class PO_PO1202
    Inherits PageBase

#Region "1. 全域變數"

#End Region

#Region "2. 功能鍵處理邏輯"

    ''' <summary>
    ''' 按鈕控管
    ''' </summary>
    ''' <param name="Param"></param>
    ''' <remarks></remarks>
    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnUpdate"   '存檔
                If ViewState.Item("DoUpdate") = "Y" Then
                    If funCheckData() Then
                        If SaveData() Then
                            GoBack()
                        End If
                    End If
                End If
            Case "btnActionX"   '返回
                GoBack()
            Case "btnCancel"    '清除
                If ViewState.Item("DoUpdate") = "Y" Then ClearData()
        End Select
    End Sub

    ''' <summary>
    ''' 儲存資料
    ''' </summary>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Private Function SaveData() As Boolean
        Dim bePOSPUser As New bePOSPUser.Row()
        Dim bsPOSPUser As New bePOSPUser.Service()
        Dim Arr As New ArrayList()
        Dim ArrFlow As New ArrayList()
        Dim node As New TreeNode
        Dim OrganStr As String = String.Empty
        Dim OrganFlowStr As String = String.Empty
        Dim i As Integer

        '取得輸入資料
        '儲存行政組織至ArrayList，並組成字串
        If tvOrgan.CheckedNodes.Count > 0 Then
            For Each node In tvOrgan.CheckedNodes
                'Dim checkOrganFlow As String = node.Text.ToString()
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

        ''儲存功能組織至ArrayList，並組成字串
        'If tvOrganFlow.CheckedNodes.Count > 0 Then
        '    For Each node In tvOrganFlow.CheckedNodes
        '        'Dim checkOrganFlow As String = node.Text.ToString()
        '        Dim checkOrganFlow As String = node.Value.ToString()
        '        ArrFlow.Add(checkOrganFlow)
        '    Next

        '    For i = 0 To (ArrFlow.Count - 1)
        '        If i = 0 Then
        '            OrganFlowStr = ArrFlow.Item(i).ToString()
        '        Else
        '            OrganFlowStr &= "," & ArrFlow.Item(i).ToString()
        '        End If
        '    Next
        'End If

        If ddlCompID.Visible = False Then
            bePOSPUser.CompID.Value = hidCompID.Value.ToString()
        Else
            Select Case ddlCompID.SelectedValue
                Case "ALL"
                    Bsp.Utility.ShowMessage(Me, "請選擇公司!")
                    Return False
                Case hidCompID.Value
                    bePOSPUser.CompID.Value = hidCompID.Value.ToString()
                Case Else
                    bePOSPUser.CompID.Value = ddlCompID.SelectedValue.ToString()
            End Select
        End If

        '設定flag為打卡
        bePOSPUser.Category.Value = "P"

        bePOSPUser.EmpID.Value = lblEmpID.Text.Trim.ToString()
        bePOSPUser.OrgList.Value = OrganStr
        bePOSPUser.OrgFlowList.Value = OrganFlowStr
        bePOSPUser.LastChgID.Value = UserProfile.UserID
        bePOSPUser.LastChgComp.Value = UserProfile.ActCompID
        bePOSPUser.LastChgDate.Value = Now

        '儲存資料
        Try
            If UpdateOTSUsrSetting(bePOSPUser) Then
                Bsp.Utility.ShowMessage(Me, "特殊人員修改成功")
                Return True
            Else
                Bsp.Utility.ShowMessage(Me, "特殊人員修改失敗")
            End If
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, "特殊人員修改失敗")
            Debug.Print("SaveData()-->" + ex.Message)
            Return False
        End Try
        Return False
    End Function

    ''' <summary>
    ''' 返回
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GoBack()
        tvOrgan.Visible = False
        tvOrganFlow.Visible = False
        Dim ti As TransferInfo = Me.StateTransfer
        Me.TransferFramePage(ti.CallerUrl, Nothing, ti.Args)
    End Sub

    ''' <summary>
    ''' 清除
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearData()
        txtEmpID.Text = hidEmpID.Value
        lblEmpName.Text = hidEmpName.Value

        subGetData(hidEmpID.Value, hidCompID.Value)
    End Sub

#End Region

#Region "3. Override Method"

#End Region

#Region "4. Page_Load"

    ''' <summary>
    ''' Page_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            '公司代碼
            'If UserProfile.SelectCompRoleID = "ALL" Then
            '    lblCompRoleID.Visible = False
            '    Bsp.Utility.FillHRCompany(ddlCompID)
            '    Page.SetFocus(ddlCompID)
            'Else
            '    lblCompRoleID.Text = ""
            '    ddlCompID.Visible = False
            'End If

            lblCompRoleID.Text = String.Empty
            Bsp.Utility.FillHRCompany(ddlCompID)
            Page.SetFocus(ddlCompID)
            ddlCompID.Visible = False

            If UserProfile.SelectCompRoleID = "SPHBK1" Then
                Bsp.Utility.FillDDL(ddlBusinessType, "eHRMSDB", "HRCodeMap", "Code", "CodeCName", Bsp.Utility.DisplayType.Full, "", "And TabName = 'Business' and FldName = 'BusinessType' And Code <> '09'", "Order By Code")
                ddlBusinessType.Items.Insert(0, New ListItem("---請選擇---", ""))
                ddlBusinessType.SelectedIndex = 0
            End If

            '紀錄是否全選
            isCheckAllOrgan.Value = "Y"
            isCheckAllOrganFlow.Value = "Y"

            '如果需求變更成要能改特殊人員員編的話改成啟用下列東東...
            '==========================
            txtEmpID.Visible = False
            txtEmpID.Enabled = False
            ucQueryEmp.Visible = False
            ucQueryEmp.Enabled = False
            '==========================
        End If
    End Sub

#End Region

#Region "5. 畫面事件"

    ''' <summary>
    ''' 取得傳遞來的參數並塞進畫面
    ''' </summary>
    ''' <param name="ti"></param>
    ''' <remarks></remarks>
    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

            If UserProfile.SelectCompRoleID = "SPHBK1" Then
                Bsp.Utility.FillDDL(ddlBusinessType, "eHRMSDB", "HRCodeMap", "Code", "CodeCName", Bsp.Utility.DisplayType.Full, "", "And TabName = 'Business' and FldName = 'BusinessType' AND NotShowFlag <> '1' ", "Order By Code")
                ddlBusinessType.Items.Insert(0, New ListItem("---請選擇---", ""))
                ddlBusinessType.SelectedIndex = 0
                hidSelectedBSValue.Value = ""
            End If

            If ht.ContainsKey("SelectedCompID") And ht.ContainsKey("SelectedEmpID") And ht.ContainsKey("SelectedNameN") Then
                ViewState.Item("EmpID") = ht("SelectedEmpID").ToString()
                ViewState.Item("NameN") = ht("SelectedNameN").ToString()
                ViewState.Item("CompID") = ht("SelectedCompID").ToString()

                Dim compRole As String = ht("SelectedCompID").ToString() + "-" + getCompanyName(ht("SelectedCompID").ToString())

                lblCompRoleID.Text = compRole
                hidCompID.Value = ht("SelectedCompID").ToString()
                lblEmpID.Text = ht("SelectedEmpID").ToString()
                txtEmpID.Text = ht("SelectedEmpID").ToString()
                lblEmpName.Text = ht("SelectedNameN").ToString()

                '查詢資料
                subGetData(ht("SelectedEmpID").ToString(), ht("SelectedCompID").ToString())
            Else
                Return
            End If

            If ht.ContainsKey("DoUpdate") Then
                If ht("DoUpdate").ToString() = "Y" Then
                    '是修改
                    ViewState.Item("DoUpdate") = "Y"
                ElseIf ht("DoUpdate").ToString() = "N" Then
                    '是明細
                    ViewState.Item("DoUpdate") = "N"
                    ucQueryEmp.Visible = False
                    'txtEmpID.Enabled = False
                    'tvOrgan.Enabled = False
                    'tvOrganFlow.Enabled = False
                    'chkAllOrgan.Enabled = False
                    'chkAllOrganFlow.Enabled = False
                Else
                    Bsp.Utility.ShowMessage(Me, "ViewState Item Transport Err")
                    GoBack()
                End If
            End If

            lblCompRoleID.Text = IIf(ht("SelectedCompID").ToString() = "", "", ht("SelectedCompID").ToString() + "-" + getCompanyName(ht("SelectedCompID").ToString()))
            Bsp.Utility.FillHRCompany(ddlCompID)
            Page.SetFocus(ddlCompID)
            ddlCompID.Visible = False

            ''查詢員工姓名
            'If txtEmpID.Text.Trim <> "" Then
            '    Dim objSPUsr As New SPUsrUtility
            '    Dim rtnTable As DataTable = objSPUsr.GetEmpName(hidCompID.Value, txtEmpID.Text.Trim)
            '    If rtnTable.Rows.Count <= 0 Then
            '        lblEmpName.Text = ""
            '        'Bsp.Utility.ShowFormatMessage(Me, "W_00020", "員工編號查詢姓名")
            '    Else
            '        lblEmpName.Text = rtnTable.Rows(0).Item(0)
            '    End If
            'Else
            '    lblEmpName.Text = ""
            'End If

            '將員工ID與姓名儲存至HiddenField作預設值
            hidEmpName.Value = ht("SelectedNameN").ToString()
            hidEmpID.Value = ht("SelectedEmpID").ToString()

            ucQueryEmp.ShowCompRole = "True"
            ucQueryEmp.InValidFlag = "N"
            ucQueryEmp.SelectCompID = UserProfile.SelectCompRoleID.ToString.Trim
        End If
    End Sub

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
        subLoadTreeOrganFlow(tvOrganFlow, UserProfile.SelectCompRoleID, hidchkOrganFlow.Value)
        'subLoadTreeOrganFlow(tvOrganFlow, UserProfile.SelectCompRoleID, hidOrgFlowList.Value)

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
        If Not String.IsNullOrEmpty(txtEmpID.Text.Trim) Then
            Dim objSPUsr As New SPUsrUtility

            Dim rtnTable As DataTable = objSPUsr.GetEmpName(hidCompID.Value, txtEmpID.Text.Trim)
            If rtnTable.Rows.Count <= 0 Then
                lblEmpName.Text = String.Empty
                Debug.Print("txtEmpID_TextChanged()-->查無此筆資料")
            Else
                lblEmpName.Text = If(String.IsNullOrEmpty(rtnTable.Rows(0).Item(0)), String.Empty, rtnTable.Rows(0).Item(0))
            End If
        Else
            lblEmpName.Text = String.Empty
        End If
    End Sub

    ''' <summary>
    ''' 彈跳視窗-快速人員查詢(QFind)
    ''' </summary>
    ''' <param name="returnValue"></param>
    ''' <remarks></remarks>
    Public Overrides Sub DoModalReturn(ByVal returnValue As String)
        Dim strSql As String = String.Empty

        If Not String.IsNullOrEmpty(returnValue) Then
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

#End Region

#Region "6. 畫面檢核與確認"

    ''' <summary>
    ''' 更新之前的條件檢查
    ''' </summary>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Private Function funCheckData() As Boolean
        Dim bePOSPUser As New bePOSPUser.Row()
        Dim bsPOSPUser As New bePOSPUser.Service()
        Dim Arr As New ArrayList()
        Dim ArrFlow As New ArrayList()
        Dim OrganStr As String = String.Empty
        Dim OrganFlowStr As String = String.Empty
        Dim i As Integer

        If ddlCompID.SelectedValue = "ALL" Then
            Bsp.Utility.ShowMessage(Me, "請選擇公司!")
            Return False
        ElseIf ddlCompID.SelectedValue = lblCompRoleID.Text.Trim.ToString() Then
            bePOSPUser.CompID.Value = lblCompRoleID.Text.Trim.ToString()
        Else
            bePOSPUser.CompID.Value = ddlCompID.SelectedValue.ToString()
        End If

        bePOSPUser.EmpID.Value = lblEmpID.Text.Trim.ToString()

        '檢查是否有輸入
        If Not String.IsNullOrEmpty(lblEmpID.Text.Trim) AndAlso Not String.IsNullOrEmpty(lblEmpName.Text.Trim) Then
            '檢查使用者是否把所有的功能組織與行政組織都取消掉
            If tvOrgan.CheckedNodes.Count = 0 And tvOrganFlow.CheckedNodes.Count = 0 Then
                Bsp.Utility.ShowMessage(Me, "您尚未勾選任一組織")
                Return False
            Else
                '檢查是否未修改任何資料
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

                '儲存功能組織至ArrayList，並組成字串
                If tvOrganFlow.CheckedNodes.Count > 0 Then
                    For Each node In tvOrganFlow.CheckedNodes
                        Dim checkOrganFlow As String = node.Value.ToString()
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

                If OrganStr = hidOrgList.Value.ToString() And OrganFlowStr = hidOrgFlowList.Value.ToString() Then
                    Bsp.Utility.ShowMessage(Me, "您尚未修改資料")
                    Return False
                End If

                Return True
            End If
        Else
            Bsp.Utility.ShowMessage(Me, "員工編號與員工姓名不得為空")
            Return False
        End If
    End Function

#End Region

#Region "7. private Method"

    ''' <summary>
    ''' 依傳遞來的條件查詢資料
    ''' </summary>
    ''' <param name="EmpID"></param>
    ''' <param name="CompID"></param>
    ''' <remarks></remarks>
    Private Sub subGetData(ByVal EmpID As String, ByVal CompID As String)
        Dim bePOSPUser As New bePOSPUser.Row()
        Dim bsPOSPUser As New bePOSPUser.Service()
        Dim ArrFlow As New ArrayList()
        Dim objSPUsr As New SPUsrUtility
        'Dim item As String

        '設定flag為打卡
        bePOSPUser.Category.Value = "P"
        bePOSPUser.EmpID.Value = EmpID.Trim
        bePOSPUser.CompID.Value = CompID.Trim

        Try
            Using dt As DataTable = bsPOSPUser.QueryByKey(bePOSPUser).Tables(0)
                If dt.Rows.Count <= 0 Then
                    Bsp.Utility.ShowMessage(Me, "發生錯誤，查無此筆資料!")
                    Exit Sub
                End If

                bePOSPUser = New bePOSPUser.Row(dt.Rows(0))

                '特殊人員行政組織字串
                If bePOSPUser.OrgList.Value.Trim <> "" Then
                    hidOrgList.Value = bePOSPUser.OrgList.Value
                End If

                '特殊人員功能組織字串
                If bePOSPUser.OrgFlowList.Value.Trim <> "" Then
                    hidOrgFlowList.Value = bePOSPUser.OrgFlowList.Value   '撈下來的值
                    hidchkOrganFlow.Value = bePOSPUser.OrgFlowList.Value  '使用者選擇的暫存，預設值為撈下來的值
                End If
            End Using

            ''---------------------校正BussinessType
            ''拆字串存進ArrayList
            ''Note: the ","c is a character literal, where as "," is a string literal.
            'ArrFlow.AddRange(hidOrgFlowList.Value.ToString().Split(","c))
            'For Each item In ArrFlow
            '    Dim rtnTable As DataTable = objSPUsr.GetBusinessType(CompID, item)
            '    If rtnTable.Rows.Count > 0 Then
            '        Dim BS As String = rtnTable.Rows(0).Item(0)
            '        If BS <> "" Then
            '            ddlBusinessType.Items.FindByValue(BS).Selected = True
            '            Exit For
            '        End If
            '    End If
            'Next

            '開始長tree
            subLoadTreeOrgan(tvOrgan, CompID, hidOrgList.Value.ToString())
            subLoadTreeOrganFlow(tvOrganFlow, CompID, hidOrgFlowList.Value.ToString())
            subLoadhidTreeOrganFlow(hidtvOrganFlow, UserProfile.SelectCompRoleID, hidOrgFlowList.Value.ToString.Trim)

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & "subGetData", ex)
        End Try
    End Sub

#Region "部門選單-功能組織"

    ''' <summary>
    ''' 長出功能組織的treeView
    ''' </summary>
    ''' <param name="objTV">功能組織的treeView元件</param>
    ''' <param name="strCompID">公司別</param>
    ''' <param name="strOrgFlow">已選擇的OrganID字串</param>
    ''' <remarks></remarks>
    Private Sub subLoadTreeOrganFlow(ByVal objTV As TreeView, ByVal strCompID As String, ByVal strOrgFlow As String)
        Dim objSPUsr As New SPUsrUtility
        'Dim RoleCode As Integer
        Dim dt As DataTable
        Dim ArrBusinessType As ArrayList
        Dim ArrFlow As New ArrayList()

        Try
            '拆字串存進ArrayList
            'Note: the ","c is a character literal, where as "," is a string literal.
            ArrFlow.AddRange(strOrgFlow.Split(","c))

            Select Case ddlBusinessType.SelectedValue
                Case ""     '請選擇
                    ArrBusinessType = New ArrayList
                    For Each item As ListItem In ddlBusinessType.Items
                        If item.Value <> "" Then ArrBusinessType.Add(item.Value)
                    Next
                    dt = objSPUsr.GetAllOrgFlowMenu(strCompID, ArrBusinessType)
                Case Else   '其他業務別
                    dt = objSPUsr.GetOrgFlowMenu(strCompID, ddlBusinessType.SelectedValue)
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
                            'RoleCode = 0
                            'Integer.TryParse(item("RoleCode").ToString().Trim(), RoleCode)
                            'If RoleCode < 20 Then
                            '    findNode.CollapseAll()
                            'End If
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
            CheckedNodeRecursive(tvOrganFlow, ArrFlow)

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
            Debug.Print("subLoadTreeOrganFlow()-->" + es.Message)
        End Try
    End Sub

#End Region

#Region "部門選單-行政組織"

    ''' <summary>
    ''' 長出行政組織的treeView
    ''' </summary>
    ''' <param name="objTV">行政組織的treeView元件</param>
    ''' <param name="strCompID">公司別</param>
    ''' <param name="strOrg">已選擇的OrganID字串</param>
    ''' <remarks></remarks>
    Private Sub subLoadTreeOrgan(ByVal objTV As TreeView, ByVal strCompID As String, ByVal strOrg As String)
        Dim objSPUsr As New SPUsrUtility
        Dim Arr As New ArrayList()

        '拆字串存進ArrayList
        'Note: the ","c is a character literal, where as "," is a string literal.
        Arr.AddRange(strOrg.Split(","c))

        Using dt As DataTable = objSPUsr.GetOrgMenu(strCompID)
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
                                        'findNode.CollapseAll()
                                    End If
                                Else
                                    objTV.FindNode(item("DeptID").ToString()).ChildNodes.Add(node)
                                    'objTV.FindNode(item("DeptID").ToString()).CollapseAll()
                                End If
                            Else
                                objTV.FindNode(item("OrgType").ToString()).ChildNodes.Add(node)
                                'objTV.FindNode(item("OrgType").ToString()).CollapseAll()
                            End If
                        Else
                            objTV.FindNode(item("OrgType").ToString() & "/" & item("DeptID").ToString()).ChildNodes.Add(node)
                            'objTV.FindNode(item("OrgType").ToString() & "/" & item("DeptID").ToString()).CollapseAll()
                        End If
                    End If
                Next
            End If
        End Using

        '依Query到的資料點選節點
        'Page.SetFocus(tvOrgan)
        CheckedNodeRecursive(tvOrgan, Arr)

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
    ''' <param name="Node">節點</param>
    ''' <param name="Value">OrganID</param>
    ''' <returns>TreeNode</returns>
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

#Region "暫存功能"

    ''' <summary>
    ''' 暫存功能組織
    ''' </summary>
    Private Sub SaveTempData()
        Dim ArrFlow As New ArrayList()
        Dim hidArrFlow As New ArrayList()
        Dim SelectedNode As New TreeNode
        Dim OrganFlowNode As New TreeNode
        Dim hidOrganFlowNode As New TreeNode
        Dim OrganFlowStr As String = String.Empty
        Dim i As Integer
        Try
            '------依BussinessType選擇暫存方式
            Select Case hidSelectedBSValue.Value.ToString
                Case ""     '請選擇
                    '直接取得所有已勾選的node，並儲存至hidtvOrganFlow

                    '重載hidtvOrganFlow
                    subLoadhidTreeOrganFlow(hidtvOrganFlow, UserProfile.SelectCompRoleID, "")

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

#Region "隱藏選單"

    ''' <summary>
    ''' 長出隱藏選單
    ''' </summary>
    ''' <param name="objTV"></param>
    ''' <param name="strCompID"></param>
    ''' <remarks></remarks>
    Private Sub subLoadhidTreeOrganFlow(ByVal objTV As TreeView, ByVal strCompID As String, ByVal strOrgFlow As String)
        Dim objSPUsr As New SPUsrUtility
        Dim RoleCode As Integer
        Dim ArrBusinessType As New ArrayList()
        Dim hidArrFlow As New ArrayList()

        Try
            For Each item As ListItem In ddlBusinessType.Items
                If item.Value <> "" Then ArrBusinessType.Add(item.Value)
            Next

            Using dt As DataTable = objSPUsr.GetAllOrgFlowMenu(strCompID, ArrBusinessType)
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
            End Using

            '依傳進來的字串點選節點
            '拆字串存進ArrayList
            If strOrgFlow.Trim <> "" Then
                'Note: the ","c is a character literal, where as "," is a string literal.
                hidArrFlow.AddRange(strOrgFlow.Split(","c))

                '勾選hidtvOrganFlow
                CheckedNodeRecursive(hidtvOrganFlow, hidArrFlow)
            End If
        Catch ex As Exception
            Debug.Print("subLoadhidTreeOrganFlow-->" + ex.Message)
        End Try
    End Sub

#End Region

    ''' <summary>
    ''' 將資料更新至資料庫的處理動作
    ''' </summary>
    ''' <param name="bePOSPUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateOTSUsrSetting(ByVal bePOSPUser As bePOSPUser.Row) As Boolean
        Dim bsPOSPUser As New bePOSPUser.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("AattendantDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsPOSPUser.Update(bePOSPUser, tran) = 0 Then Return False
                tran.Commit()
            Catch ex As Exception
                tran.Rollback()
                Debug.Print("UpdateOTSUsrSetting()-->" + ex.Message)
                Throw
            Finally
                If tran IsNot Nothing Then tran.Dispose()
            End Try
        End Using
        Return True
    End Function

    ''' <summary>
    ''' 取得公司名稱
    ''' </summary>
    ''' <param name="compID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getCompanyName(ByVal compID As String) As String
        Dim result As String = ""
        Dim strSQL As New StringBuilder
        Dim dataTable As New DataTable
        strSQL.Append(" select CompName FROM Company ")
        strSQL.Append(" where CompID = " & Bsp.Utility.Quote(compID))
        dataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)

        result = If(String.IsNullOrEmpty(dataTable.Rows(0).Item("CompName")), String.Empty, dataTable.Rows(0).Item("CompName"))
        Return result
    End Function

#End Region

End Class
