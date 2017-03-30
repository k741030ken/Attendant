'****************************************************
'功能說明：流程Menu
'建立人員：Chung
'建立日期：2013/01/31
'****************************************************
Imports System.Data

Partial Class WF_WFA010
    Inherits PageBase
    Implements System.Web.UI.ICallbackEventHandler

    Dim _callBackResult As String
    Dim _FlowID As String = ""
    Dim _FlowCaseID As String = ""
    Dim _FlowLogID As String = ""
    Dim _FlowVer As String = ""
    Dim _FlowStepID As String = ""
    Const AdapterPage As String = "~/WF/WFA012.aspx"

    Public Overrides ReadOnly Property NeedFunctionBar() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Function GetCallbackResult() As String Implements System.Web.UI.ICallbackEventHandler.GetCallbackResult
        Return _callBackResult
    End Function

    Public Sub RaiseCallbackEvent(ByVal eventArgument As String) Implements System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent
        Try
            Dim strURL As String = Bsp.Utility.menuDecoding(eventArgument)
            If strURL.Substring(0, 7) = "Return:" Then
                strURL = strURL.Substring(7)
                Session("sys_ReArgs") = ViewState.Item("ReturnArgs")
                PageSession.Remove("sys_FlowBackInfo")
            End If

            _callBackResult = strURL

        Catch ex As Exception
            _callBackResult = ""
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objWF As New WF
        'call back 呼叫處理

        If Not IsPostBack Then
            Dim eventScript As String = Me.ClientScript.GetCallbackEventReference(Me, "Path", "redirectPage", "")

            Dim strScript As String

            strScript = "  function callPage(Path)"
            strScript &= "  {"
            strScript &= "      " & eventScript
            strScript &= "  }"

            If Not Page.ClientScript.IsClientScriptBlockRegistered(Me.GetType(), "callPage") Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "callPage", strScript, True)
            End If
        End If
    End Sub

    Private Function GetMenuTitle(ByVal FlowID As String, ByVal FlowCaseID As String, Optional ByVal Params As Hashtable = Nothing) As String
        Dim strValue As String = ""
        Dim objWF As New WF()

        If FlowCaseID <> "" Then
            Using dt As DataTable = objWF.GetFlowCase(FlowID, FlowCaseID)
                Select Case FlowID
                    Case Else
                        Return " <font style=""font-size:16px"">申請編號：</font><U>" & dt.Rows(0).Item("PaperID").ToString() & "</U>"
                End Select
            End Using
        Else
            Select Case FlowID
                Case "CUSDATA"
                    Using dt As DataTable = objWF.GetBR_Customer(Params("CID").ToString())
                        strValue = String.Format("<font style=""color:Navy; font-weight:bold"">{0}</font><br>{1}<hr>", _
                                                        dt.Rows(0).Item("CustomerID").ToString(), _
                                                        dt.Rows(0).Item("CName").ToString())
                    End Using
                Case "PROPOSAL"
                    strValue = String.Format("&nbsp;<font style=""font-size:16px"">申請編號：</font><U>{0}</U><br>" & _
                                             "&nbsp;<font style=""font-size:16px"">客戶編號：</font><font style=""color:black"">{1}</font><br>" & _
                                             "&nbsp;<font style=""font-size:16px"">客戶名稱：</font><font style=""color:black"">{2}</font><hr>", _
                                                    Params("SourceAppID").ToString(), _
                                                    Params("CustomerID").ToString(), _
                                                    Params("CName").ToString())
                Case "EMPINFO"
                    strValue = " <font style=""font-size:16px"">員工：</font><U>" & Params("SelectedCompName").ToString() & "/" & Params("SelectedEmpID").ToString() & "-" & Params("SelectedEmpName").ToString() & "</U>"
                Case "ORGINFO" '20161012 Beatrice Add
                    strValue = " <font style=""font-size:16px"">組織：</font><U>" & Params("SelectedCompName").ToString() & "/" & Params("SelectedOrganID").ToString() & "-" & Params("SelectedOrganName").ToString() & "</U>"
                Case "ORGFLOWINFO" '20161012 Beatrice Add
                    strValue = " <font style=""font-size:16px"">組織：</font><U>" & Params("SelectedCompName").ToString() & "/" & Params("SelectedOrganID").ToString() & "-" & Params("SelectedOrganName").ToString() & "</U>"
            End Select
        End If

        Return strValue
    End Function

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        Dim objWF As New WF
        Dim strMenuTitle As String = ""
        Dim strKeyID As String = ""

        If ti.Args.Length = 0 Then Return
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)
        GetFlowBackInfo(ti.Args)

        Try
            _FlowID = Bsp.Utility.IsStringNull(ht("FlowID"))
            _FlowCaseID = Bsp.Utility.IsStringNull(ht("FlowCaseID"))
            '判斷是否為流程
            If _FlowID <> "" AndAlso _FlowCaseID <> "" Then
                'GetFlowVer(_FlowID, _FlowCaseID)If PageSession("FlowBackInfo") Is Nothing 
                Using dt As DataTable = objWF.GetFlowStepMbyFlowCase(_FlowID, _FlowCaseID)
                    Dim beFlowStepM As New beWF_FlowStepM.Row(dt.Rows(0))

                    If Not ht.ContainsKey("FlowStepID") Then ht.Add("FlowStepID", beFlowStepM.FlowStepID.Value)
                    If ht.ContainsKey("ShowMode") AndAlso ht("ShowMode").ToString() = "Y" Then
                        strMenuTitle = beFlowStepM.ShowModeMenuTitle.Value
                    Else
                        strMenuTitle = beFlowStepM.MenuTitle.Value
                    End If
                End Using
                GetBaseNode(ht, strMenuTitle)
            Else
                Dim objSC As New SC()

                'TODO : 需考慮非流程之Menu
                Using dt As DataTable = objSC.GetFlowStepM(Bsp.Utility.IsStringNull(ht("FlowID")), "1", "0000")
                    Dim beFlowStepM As New beWF_FlowStepM.Row(dt.Rows(0))

                    strMenuTitle = beFlowStepM.MenuTitle.Value
                End Using
                GetBaseNode(ht, strMenuTitle, False)
            End If
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".BaseOnPageTransfer", ex)
        End Try
        tdHeadLine.InnerHtml = GetMenuTitle(_FlowID, _FlowCaseID, ht)
    End Sub


    Private Sub GetBaseNode(ByVal BaseParam As Hashtable, ByVal BaseMenuTitle As String, Optional ByVal WithFlow As Boolean = True)
        Me.FvFun.Nodes.Clear()
        Dim Node As New TreeNode()
        Dim NavigateUrl As String = ""

        Try
            Node.SelectAction = TreeNodeSelectAction.Expand
            If WithFlow Then
                Node.Text = String.Format("{0}-{1}", BaseParam("FlowStepID").ToString(), (BaseMenuTitle).Split("*")(0))
                Node.Value = String.Format("{0}-{1}", BaseParam("FlowStepID").ToString(), (BaseMenuTitle).Split("*")(0))
            Else
                Node.Text = String.Format("{0}", (BaseMenuTitle).Split("*")(0))
                Node.Value = String.Format("{0}", (BaseMenuTitle).Split("*")(0))
            End If
            FvFun.Nodes.Add(Node)
            BuildChildNode(BaseMenuTitle, Node.ChildNodes, BaseParam)
            Node.Expand()
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".GetBaseNode", ex)
        End Try

        If WithFlow Then
            If BaseParam.ContainsKey("ShowMode") AndAlso BaseParam("ShowMode").ToString() = "Y" Then
                Node = New TreeNode()
                Node.Text = "[案件流程明細]"
                Node.Value = "[案件流程明細]"
                NavigateUrl = Bsp.Utility.menuEncoding(String.Format("{0}?LinkPage={1}&{2}", ResolveUrl(AdapterPage), ResolveUrl("~/WF/WFA030.aspx"), Bsp.Utility.getParamFromHashTable(BaseParam)))
                Node.NavigateUrl = "javascript:callPage('" & NavigateUrl & "');"
                Me.FvFun.Nodes.Add(Node)
            Else
                Node = New TreeNode()
                Node.Text = "[案件流程處理]"
                Node.Value = "[案件流程處理]"
                NavigateUrl = Bsp.Utility.menuEncoding(String.Format("{0}?LinkPage={1}&{2}", ResolveUrl(AdapterPage), ResolveUrl("~/WF/WFA020.aspx"), Bsp.Utility.getParamFromHashTable(BaseParam)))
                Node.NavigateUrl = "javascript:callPage('" & NavigateUrl & "');"
                Me.FvFun.Nodes.Add(Node)
            End If
        End If
        '建立返回的選項 
        If PageSession("sys_FlowBackInfo") IsNot Nothing Then
            For Each fb As FlowBackInfo In CType(PageSession("sys_FlowBackInfo"), System.Collections.Generic.List(Of FlowBackInfo))
                Node = New TreeNode()
                Node.Text = "[" & fb.MenuNodeTitle & "]"
                Node.Value = "[" & fb.MenuNodeTitle & "]"
                NavigateUrl = Bsp.Utility.menuEncoding(String.Format("Return:{0}?LinkPage={1}&ExitFlow=T", ResolveUrl(AdapterPage), ResolveUrl(fb.URL)))
                Node.NavigateUrl = "javascript:callPage('" & NavigateUrl & "');"
                Me.FvFun.Nodes.Add(Node)
            Next
        End If

        If WithFlow Then
            Node = New TreeNode
            Node.Text = "[回待辦清單]"
            Node.Value = "[回待辦清單]"
            NavigateUrl = Bsp.Utility.menuEncoding(String.Format("{0}?LinkPage={1}&ExitFlow=T", ResolveUrl(AdapterPage), ResolveUrl("~/WF/WFA000.aspx")))
            Node.NavigateUrl = "javascript:callPage('" & NavigateUrl & "');"
            Me.FvFun.Nodes.Add(Node)
        End If
    End Sub

    Sub BuildChildNode(ByVal MenuTitle As String, ByRef Nodes As Web.UI.WebControls.TreeNodeCollection, ByVal Params As Hashtable)
        Dim objWF As New WF()
        Dim LinkPara As String
        Dim NavigateUrl As String
        Dim ToNextParam As Hashtable
        Dim strParams As String = ""
        Dim NewNode As TreeNode

        Using dt As DataTable = objWF.GetFlowChildFunction(MenuTitle)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    ToNextParam = Params.Clone()
                    LinkPara = dr.Item("LinkPara").ToString()

                    '加入額外參數
                    If LinkPara <> "" Then
                        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(LinkPara)

                        For Each strKey As String In ht.Keys
                            If Not ToNextParam.ContainsKey(strKey) Then
                                ToNextParam.Add(strKey, ht(strKey))
                            End If
                        Next
                    End If

                    '判斷是否有SQL子項聯結
                    If dr.Item("LinkParaExt").ToString() = "Y" AndAlso dr.Item("LinkParaSql").ToString() <> "" Then
                        Dim dbParams() As Data.Common.DbParameter = Nothing
                        Dim MenuName As String = dr.Item("MenuName").ToString.Trim()

                        '載入參數
                        For Each strKey As String In ToNextParam.Keys
                            If dr.Item("LinkParaSql").ToString().IndexOf("@" & strKey) Then
                                If dbParams Is Nothing Then
                                    Array.Resize(dbParams, 1)
                                Else
                                    Array.Resize(dbParams, dbParams.Length + 1)
                                End If
                                dbParams(dbParams.GetUpperBound(0)) = Bsp.DB.getDbParameter("@" & strKey, ToNextParam(strKey))
                            End If
                        Next

                        Using dtMulti As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, dr.Item("LinkParaSql").ToString(), dbParams).Tables(0)
                            For Each drItem As DataRow In dtMulti.Rows
                                '建立要往下傳遞的參數
                                Dim SubParams As Hashtable = ToNextParam.Clone

                                '加入第一個參數
                                If Not SubParams.ContainsKey(dtMulti.Columns(0).ColumnName) Then
                                    SubParams.Add(dtMulti.Columns(0).ColumnName, drItem.Item(0).ToString())
                                Else
                                    SubParams(dtMulti.Columns(0).ColumnName) = drItem.Item(0).ToString()
                                End If

                                '若有多個參數,請全加於第二個參數,並帶上ColumnName...(注意：資料內如若有&會有問題)
                                'Ex: Select CustomerID, 'ErrCode=' + ErrCode + '&CName=' + CName From BR_Customer ...
                                '加入第二個以上的參數
                                If dtMulti.Columns.Count > 1 Then
                                    Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Bsp.Utility.IsStringNull(drItem(1)))

                                    For Each strKey As String In ht.Keys
                                        If SubParams.ContainsKey(strKey) Then
                                            '若有新的值，以新值取代舊值...主要是參照時使用...以RefAppID取代AppID
                                            SubParams(strKey) = ht(strKey)
                                        Else
                                            SubParams.Add(strKey, ht(strKey))
                                        End If
                                    Next
                                End If

                                '檢查是否有需要加入Node Text的設定...
                                '以下為設定參數:
                                '1. _BeforeNodeText : 加到原來Node text前
                                '2. _AfterNodeText : 加到原來Node text後
                                '3. _ReplaceNodeText : 取代原來的Node Text

                                NewNode = New TreeNode()
                                NewNode.SelectAction = TreeNodeSelectAction.Expand

                                If SubParams.ContainsKey("_BeforeNodeText") Then
                                    NewNode.Text = SubParams("_BeforeNodeText").ToString() & dr.Item("FunShowName").ToString.Trim()
                                    NewNode.Value = SubParams("_BeforeNodeText").ToString() & dr.Item("FunShowName").ToString.Trim()
                                    SubParams.Remove("_BeforeNodeText")
                                ElseIf SubParams.ContainsKey("_AfterNodeText") Then
                                    NewNode.Text = dr.Item("FunShowName").ToString.Trim() & SubParams("_AfterNodeText").ToString()
                                    NewNode.Value = dr.Item("FunShowName").ToString.Trim() & SubParams("_AfterNodeText").ToString()
                                    SubParams.Remove("_AfterNodeText")
                                ElseIf SubParams.ContainsKey("_ReplaceNodeText") Then
                                    NewNode.Text = SubParams("_ReplaceNodeText").ToString()
                                    NewNode.Value = SubParams("_ReplaceNodeText").ToString()
                                    SubParams.Remove("_ReplaceNodeText")
                                Else
                                    NewNode.Text = dr.Item("FunShowName").ToString.Trim()
                                    NewNode.Value = dr.Item("FunShowName").ToString.Trim()
                                End If


                                If dr.Item("LinkPage").ToString() <> "" Then
                                    NavigateUrl = Bsp.Utility.menuEncoding(String.Format("{0}?LinkPage={1}&{2}", ResolveUrl(AdapterPage), dr.Item("LinkPage").ToString.Trim(), Bsp.Utility.getParamFromHashTable(SubParams)))
                                    NewNode.NavigateUrl = "javascript:callPage('" & NavigateUrl & "');"
                                End If
                                Nodes.Add(NewNode)
                                BuildChildNode(MenuName, NewNode.ChildNodes, SubParams)
                                NewNode.CollapseAll()
                            Next
                        End Using
                    Else
                        NewNode = New TreeNode()
                        NewNode.SelectAction = TreeNodeSelectAction.Expand
                        Dim Prompt As String = dr.Item("MenuName").ToString().Trim()
                        'NewNode.Text = dr.Item("FunShowName").ToString.Trim()                                      '2016/08/12 Beatrice del
                        NewNode.Text = dr.Item("FunID").ToString.Trim() + dr.Item("FunShowName").ToString.Trim()    '2016/08/12 Beatrice modify
                        NewNode.Value = dr.Item("FunShowName").ToString.Trim()

                        If dr.Item("LinkPage").ToString() <> "" Then
                            If ToNextParam.Count > 0 Then
                                NavigateUrl = Bsp.Utility.menuEncoding(String.Format("{0}?LinkPage={1}&{2}", ResolveUrl(AdapterPage), dr.Item("LinkPage").ToString.Trim(), Bsp.Utility.getParamFromHashTable(ToNextParam)))
                            Else
                                NavigateUrl = Bsp.Utility.menuEncoding(String.Format("{0}?LinkPage={1}", ResolveUrl(AdapterPage), dr.Item("LinkPage").ToString.Trim()))
                            End If

                            NewNode.NavigateUrl = "javascript:callPage('" & NavigateUrl & "');"
                        End If
                        'Nodes.Add(NewNode)
                        BuildChildNode(Prompt, NewNode.ChildNodes, ToNextParam)
                        '2014.06.20 (U) Chung 若向下沒有Node且無功能則不顯示
                        If dr.Item("LinkPage").ToString() <> "" OrElse NewNode.ChildNodes.Count > 0 Then
                            Nodes.Add(NewNode)
                        End If
                        NewNode.CollapseAll()
                    End If
                Next
            End If
        End Using
    End Sub

    Private Sub GetFlowVer(ByVal FlowID As String, ByVal FlowCaseID As String)
        Dim objWF As New WF

        Try
            Using dt As DataTable = objWF.GetFlowCase(FlowID, FlowCaseID)
                If dt.Rows.Count > 0 Then
                    Dim beFlowCase As New beWF_FlowCase.Row(dt.Rows(0))

                    _FlowVer = beFlowCase.FlowVer.Value.ToString()
                    _FlowStepID = beFlowCase.FlowCurrStepID.Value
                End If
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub GetFlowBackInfo(ByVal Args() As Object)
        PageSession.Remove("sys_FlowBackInfo")
        For Each Param As Object In Args
            If TypeOf Param Is FlowBackInfo Then
                Dim lstFlowBackInfo As System.Collections.Generic.List(Of FlowBackInfo)

                If PageSession("sys_FlowBackInfo") Is Nothing Then
                    lstFlowBackInfo = New System.Collections.Generic.List(Of FlowBackInfo)
                Else
                    lstFlowBackInfo = CType(PageSession("sys_FlowBackInfo"), System.Collections.Generic.List(Of FlowBackInfo))
                End If
                lstFlowBackInfo.Add(CType(Param, FlowBackInfo))
                PageSession("sys_FlowBackInfo") = lstFlowBackInfo
                If TypeOf Args(0) Is Object() Then
                    ViewState.Item("ReturnArgs") = Args(0)
                Else
                    ViewState.Item("ReturnArgs") = Args
                End If
                Exit For
            End If
        Next
    End Sub

    Public Overrides Sub Object2ViewState(ByVal ParamArray Args As Object())
        For Each Param As Object In Args
            If TypeOf Param Is String Then
                Dim intPos As Integer = Param.ToString().IndexOf("=")

                If intPos >= 0 Then
                    ViewState.Item(Param.ToString().Substring(0, intPos)) = Param.ToString().Substring(intPos + 1)
                End If
            ElseIf TypeOf Param Is FlowBackInfo Then
                Dim lstFlowBackInfo As System.Collections.Generic.List(Of FlowBackInfo)

                If PageSession("sys_FlowBackInfo") Is Nothing Then
                    lstFlowBackInfo = New System.Collections.Generic.List(Of FlowBackInfo)
                Else
                    lstFlowBackInfo = CType(PageSession("sys_FlowBackInfo"), System.Collections.Generic.List(Of FlowBackInfo))
                End If
                lstFlowBackInfo.Add(CType(Param, FlowBackInfo))
                PageSession("sys_FlowBackInfo") = lstFlowBackInfo
            End If
        Next
    End Sub

    Private Sub GetFlowKey()
        For Each strKey As String In ViewState.Keys
            Select Case strKey
                Case "FlowID"
                    _FlowID = ViewState.Item(strKey)
                Case "FlowCaseID"
                    _FlowCaseID = ViewState.Item(strKey)
                Case "FlowLogID"
                    _FlowLogID = ViewState.Item(strKey)
            End Select
        Next
    End Sub
End Class
