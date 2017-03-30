'****************************************************
'功能說明：網頁底層
'建立人員：A02976
'建立日期：2007.03.10
'****************************************************
Imports Microsoft.VisualBasic

Public Class PageBase
    Inherits CommonBase

    Private _FunID As String = ""
    Private _pageSessionID As String = ""
    Private _parentSessionID As String = ""
    Private _clusterSessionID As String = ""

    Private _clusterSession As BaseState
    Private _programSession As BaseState
    Private _pageSession As BaseState
    Private _parentSession As BaseState

    Private _windowOnLoadScripts As StringBuilder
    Private _formHeadScripts As StringBuilder

    Private _GridView_PageSize As Integer = 10

    Public Const QUERYSRTING_CLUSTER_SESSION_ID As String = "_CSI"
    Public Const QUERYSTRING_PARENT_SESSION_ID As String = "_PSI"
    Public Const QUERYSTRING_BASE_THREAD_ID As String = "_BTI"
    Public Const QUERYSTRING_PAGE_SESSION_ID As String = "_PAGESI"
    Public Const QUERYSTRING_LAUNCH_PAGE As String = "_LP"

    Private Const _DEFAULT_HOME_URL As String = "~/SC/SC0000.aspx"
    Private Const _DEFAULT_FRAME_CONTROL_URL As String = "~/SC/SC0050.aspx"
    Private Const _DEFAULT_EXCEL_EXPORT_URL As String = "~/SC/ExcelExport.aspx"
    Private Const _DEFAULT_CONTROL_PATH_URL As String = "~/Component"
    'Private Const _DEFAULT_REMOVE_THREAD_URL As String = "~/System/RemoveThread.aspx"
    Private Const _DEFAULT_DETECT_SESSION_TIMEOUT_INTERVAL As Integer = 60000

    Private Const baseFlowMenuPageID As String = "WFA010"
    Private Const baseFunBarPageID As String = "SC0060"

    Private Const Color_LightBar As String = "#a3bffc"
    Private Const Color_SelectedRow As String = "Moccasin"

    Public Property FunID() As String
        Get
            Dim objFunID As TextBox = Page.Form.FindControl("__FunID")
            If objFunID Is Nothing OrElse objFunID.Text = "" Then
                _FunID = Bsp.Utility.ExtractPageID(Me.Page.Request.Path).ToLower().Replace(".aspx", "").ToUpper()
            Else
                _FunID = objFunID.Text
            End If

            Return _FunID
        End Get
        Set(ByVal value As String)
            _FunID = value
        End Set
    End Property

    '判斷是否需要接收CurrentFlow的值
    Protected Overridable ReadOnly Property ReceiveFlowInstance() As Boolean
        Get
            Return False
        End Get
    End Property

    Protected Property GridViewNames() As System.Collections.Hashtable
        Get
            If PageSession("__GridViewNames") Is Nothing Then
                Return Nothing
            Else
                Return CType(PageSession("__GridViewNames"), System.Collections.Hashtable)
            End If
        End Get
        Set(ByVal value As System.Collections.Hashtable)
            If value Is Nothing AndAlso PageSession("__GridViewNames") IsNot Nothing Then
                PageSession.Remove("__GridViewNames")
            Else
                PageSession("__GridViewNames") = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' 是否需按鈕功能列
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable ReadOnly Property NeedFunctionBar() As Boolean
        Get
            Return True
        End Get
    End Property

    Protected Property PageSessionID() As String
        Get
            If (Request.Form(QUERYSTRING_PAGE_SESSION_ID) IsNot Nothing AndAlso Request.Form(QUERYSTRING_PAGE_SESSION_ID).ToString() <> "") Then
                _pageSessionID = Request.Form(QUERYSTRING_PAGE_SESSION_ID).ToString()
                MyBase.PageCollect.Add(_pageSessionID)
            ElseIf (Request.QueryString(QUERYSTRING_PAGE_SESSION_ID) IsNot Nothing AndAlso Request.QueryString(QUERYSTRING_PAGE_SESSION_ID).ToString() <> "") Then
                _pageSessionID = Request.QueryString(QUERYSTRING_PAGE_SESSION_ID).ToString()
                MyBase.PageCollect.Add(_pageSessionID)
            Else
                If _pageSessionID = "" Then
                    _pageSessionID = BasePageID()

                    'Mark by Chung  2007.03.22  不需要有那麼多的PageSessionID，以後若有需要再加
                    '否則每次重新進來就會給一次PageSessionID，系統恐怕會承受不了
                    '_pageSessionID = BasePageID() + "_" + BaseThreadID
                    If ParentSessionID() <> "" Then
                        _pageSessionID = ParentSessionID() & BaseState.SESSION_KEY_DELIMITER & _pageSessionID
                    End If
                    MyBase.PageCollect.Add(_pageSessionID)
                End If
            End If

            Return _pageSessionID
        End Get
        Set(ByVal value As String)
            _pageSessionID = value
        End Set
    End Property

    Public ReadOnly Property BasePageID() As String
        Get
            Return FunID
        End Get
    End Property

   

    Protected Overridable ReadOnly Property BasePageUrl() As String
        Get
            Return ResolveUrl(BasePageID() & ".aspx")
        End Get
    End Property


    '<summary>
    '	檢查程式權限時所用的程式代號:, 子網頁要沿用父網頁的安控權限設定時可以覆寫此程式代號
    '</summary>
    Public Overridable ReadOnly Property ScsProgramID() As String
        Get
            Return BasePageID()
        End Get
    End Property

    Protected ReadOnly Property BaseThreadID() As String
        Get
            Dim strThreadID As String = Request.QueryString(QUERYSTRING_BASE_THREAD_ID)
            If strThreadID Is Nothing AndAlso strThreadID = "" Then
                strThreadID = NewThreadID()
            End If
            Return strThreadID
        End Get
    End Property

    Protected ReadOnly Property ParentSessionID() As String
        Get
            Dim strID As String = Request.QueryString(QUERYSTRING_PAGE_SESSION_ID)

            If strID Is Nothing OrElse strID = "" Then strID = ""
            Return strID
        End Get
    End Property

    Protected Function NewThreadID() As String
        Return Now.ToString("HHmmss") & Now.Millisecond.ToString("000")
    End Function

    Private Property transferState() As TransferInfo
        Get
            Return TryCast(Session("sys_transferState"), TransferInfo)
        End Get
        Set(ByVal value As TransferInfo)
            If value Is Nothing Then
                Session.Remove("sys_transferState")
            Else
                Session("sys_transferState") = value
            End If
        End Set
    End Property

    Protected Property StateTransfer() As TransferInfo
        Get
            Return TryCast(PageSession("StateTransfer"), TransferInfo)
        End Get
        Set(ByVal value As TransferInfo)
            If value Is Nothing Then
                PageSession.Remove("StateTransfer")
            Else
                PageSession("StateTransfer") = value
            End If
        End Set
    End Property

    Public ReadOnly Property PageSession() As BaseState
        Get
            If _pageSession Is Nothing Then
                _pageSession = New BaseState(PageSessionID)
            End If
            Return _pageSession
        End Get
    End Property

    ' <summary>
    ' 父網頁Session區段
    ' </summary>
    Public ReadOnly Property ParentSession() As BaseState
        Get
            If _parentSession Is Nothing Then
                _parentSession = New BaseState(ParentSessionID)
            End If
            Return _parentSession
        End Get
    End Property

    ' <summary>
    ' //同一連線,相同類別網頁的狀態儲存
    ' </summary>
    Public ReadOnly Property ProgramSession() As BaseState
        Get
            If _programSession Is Nothing Then
                _programSession = New BaseState(BasePageID)
            End If
            Return _programSession
        End Get
    End Property

    ' <summary>
    ' 叢集網頁Session代碼-必須透過URL Query String 傳遞設定
    ' </summary>
    Public ReadOnly Property ClusterSessionID() As String
        Get
            Dim strID As String = Request.QueryString(QUERYSRTING_CLUSTER_SESSION_ID)

            If strID Is Nothing Then strID = ""
            Return strID
        End Get
    End Property

    ' <summary>
    ' 叢集網頁Session區段
    ' </summary>
    Public ReadOnly Property ClusterSession() As BaseState
        Get
            If _clusterSession Is Nothing Then
                _clusterSession = New BaseState(_clusterSessionID)
            End If
            Return _clusterSession
        End Get
    End Property

    ' <summary>
    ' 獨立網頁物件的主要狀態值,通常用來儲存網頁的主要交易資料集
    ' </summary>
    Public Overridable Property StateMain() As Object
        Get
            Return PageSession("StateMain")
        End Get
        Set(ByVal value As Object)
            If value Is Nothing Then
                PageSession.Remove("StateMain")
            Else
                PageSession("StateMain") = value
            End If
        End Set
    End Property

    '指定不要LightBar顯示的GridView
    Public Overridable ReadOnly Property ExcludeLightBarGridName() As String
        Get
            Return ""
        End Get
    End Property

    '當子網頁被CallPage方法呼叫時,會觸發此方法,應用程式必須實作處理邏輯
    Protected Overridable Sub BaseOnPageCall(ByVal ti As TransferInfo)

    End Sub

    '當子網頁被LaunchPage方法呼叫時,會觸發此方法,應用程式必須實作處理邏輯
    Protected Overridable Sub BaseOnPageLaunch(ByVal ti As TransferInfo)

    End Sub

    '當被CallPage方法呼叫的子網頁關閉時,會觸發母(Caller)網頁的此方法,應用程式必須實作處理邏輯
    Protected Overridable Sub BaseOnPageReturn(ByVal ti As TransferInfo)

    End Sub

    '當子網頁被TransferPage方法呼叫時,會觸發此方法,應用程式必須實作處理邏輯
    Protected Overridable Sub BaseOnPageTransfer(ByVal ti As TransferInfo)

    End Sub

    ' <summary>
    ' 開啟凍結式子視窗
    ' </summary>
    ' <param name="pageUrl">子視窗網頁位址(必須位於同一應用程式虛擬目錄)</param>
    ' <param name="commandName">傳遞給子視窗網頁的處理命令名稱</param>
    ' <param name="args">附加參數</param>
    Public Sub CallModalPage(ByVal pageUrl As String, ByVal pageWidth As String, ByVal pageHeight As String, ByVal commandList() As ButtonState, ByVal ParamArray args As Object())
        CallModalPage(ResolveUrl(pageUrl), pageWidth, pageHeight, True, commandList, args)
    End Sub

    Public Sub CallModalPageForAjax(ByVal pageUrl As String, ByVal pageWidth As String, ByVal pageHeight As String, ByVal commandList() As ButtonState, ByVal ParamArray args As Object())
        CallModalPageForAjax(ResolveUrl(pageUrl), pageWidth, pageHeight, True, commandList, args)
    End Sub

    Public Sub CallModalPage(ByVal pageUrl As String, ByVal pageWidth As String, ByVal pageHeight As String, ByVal CheckRight As Boolean, ByVal commandList() As ButtonState, ByVal ParamArray args As Object())
        Dim NeedButtonControl As Boolean = True
        'If pageUrl.ToUpper().IndexOf("CC0050.ASPX") >= 0 Then NeedButtonControl = True

        If pageWidth = "" Then pageWidth = "800"
        If pageHeight = "" Then pageHeight = "600"

        Dim ti As New TransferInfo(TransferInfo.TransferInfoType.ModalChild, FunID, BasePageUrl, pageUrl, PageSessionID, commandList, IIf(NeedButtonControl, baseFunBarPageID, ""), args) '20140818 wei modify
        ti.CheckRight = CheckRight
        transferState = ti

        If pageWidth.ToString.ToLower = "auto" AndAlso pageHeight.ToUpper.ToLower = "auto" Then
            Bsp.Utility.RunClientScript(Me, "ShowDialogWithHeader(""" & ResolveUrl(pageUrl).Replace("""", """""") & """, screen.availWidth-(screen.availWidth*3/100), screen.availHeight-(screen.availHeight*3/100));")
        Else
            Bsp.Utility.RunClientScript(Me, "ShowDialogWithHeader(""" & ResolveUrl(pageUrl).Replace("""", """""") & """, '" & pageWidth & "', '" & pageHeight & "');")
        End If
    End Sub

    Public Sub CallModalPageForAjax(ByVal pageUrl As String, ByVal pageWidth As String, ByVal pageHeight As String, ByVal CheckRight As Boolean, ByVal commandList() As ButtonState, ByVal ParamArray args As Object())
        Dim NeedButtonControl As Boolean = True

        If pageWidth = "" Then pageWidth = "800"
        If pageHeight = "" Then pageHeight = "600"

        Dim ti As New TransferInfo(TransferInfo.TransferInfoType.ModalChild, FunID, BasePageUrl, pageUrl, PageSessionID, commandList, IIf(NeedButtonControl, baseFunBarPageID, ""), args) '20140818 wei modify
        ti.CheckRight = CheckRight
        transferState = ti

        If pageWidth.ToString.ToLower = "auto" AndAlso pageHeight.ToUpper.ToLower = "auto" Then
            Bsp.Utility.RunClientScriptForAjax(Me, "ShowDialogWithHeader(""" & ResolveUrl(pageUrl).Replace("""", """""") & """, screen.availWidth-(screen.availWidth*3/100), screen.availHeight-(screen.availHeight*3/100));")
        Else
            Bsp.Utility.RunClientScriptForAjax(Me, "ShowDialogWithHeader(""" & ResolveUrl(pageUrl).Replace("""", """""") & """, '" & pageWidth & "', '" & pageHeight & "');")
        End If
    End Sub

    '說明：主要提供呼叫外部網頁，Client端呼叫ClientFun.js內的ShowDialog，不要用錯了
    Public Sub CallModalPage(ByVal pageUrl As String, ByVal pageWidth As String, ByVal pageHeight As String)
        Bsp.Utility.RunClientScript(Me, "ShowDialogWithEvent(""" & ResolveUrl(pageUrl).Replace("""", """""") & """, '" & pageWidth & "', '" & pageHeight & "');")
    End Sub

    'size 480 * 360
    Public Sub CallSmallPage(ByVal pageUrl As String, ByVal commandList() As ButtonState, ByVal ParamArray args As Object())
        CallSmallPage(ResolveUrl(pageUrl), True, commandList, args)
    End Sub

    Public Sub CallSmallPageForAjax(ByVal pageUrl As String, ByVal commandList() As ButtonState, ByVal ParamArray args As Object())
        CallSmallPageForAjax(ResolveUrl(pageUrl), True, commandList, args)
    End Sub

    Public Sub CallSmallPage(ByVal pageUrl As String, ByVal CheckRight As Boolean, ByVal commandList() As ButtonState, ByVal ParamArray args As Object())
        CallModalPage(ResolveUrl(pageUrl), "480", "360", CheckRight, commandList, args)
    End Sub

    Public Sub CallSmallPageForAjax(ByVal pageUrl As String, ByVal CheckRight As Boolean, ByVal commandList() As ButtonState, ByVal ParamArray args As Object())
        CallModalPageForAjax(ResolveUrl(pageUrl), "480", "360", CheckRight, commandList, args)
    End Sub

    'size 800 * 600
    Public Sub CallMiddlePage(ByVal pageUrl As String, ByVal commandList() As ButtonState, ByVal ParamArray args As Object())
        CallMiddlePageForAjax(ResolveUrl(pageUrl), True, commandList, args)
    End Sub

    Public Sub CallMiddlePageForAjax(ByVal pageUrl As String, ByVal commandList() As ButtonState, ByVal ParamArray args As Object())
        CallMiddlePageForAjax(ResolveUrl(pageUrl), True, commandList, args)
    End Sub

    Public Sub CallMiddlePage(ByVal pageUrl As String, ByVal CheckRight As Boolean, ByVal commandList() As ButtonState, ByVal ParamArray args As Object())
        CallModalPage(ResolveUrl(pageUrl), "800", "600", CheckRight, commandList, args)
    End Sub

    Public Sub CallMiddlePageForAjax(ByVal pageUrl As String, ByVal CheckRight As Boolean, ByVal commandList() As ButtonState, ByVal ParamArray args As Object())
        CallModalPageForAjax((pageUrl), "800", "600", CheckRight, commandList, args)
    End Sub

    'size 800 * 600
    Public Sub CallLargePage(ByVal pageUrl As String, ByVal commandList() As ButtonState, ByVal ParamArray args As Object())
        CallLargePage(ResolveUrl(pageUrl), True, commandList, args)
    End Sub

    Public Sub CallLargePage(ByVal pageUrl As String, ByVal CheckRight As Boolean, ByVal commandList() As ButtonState, ByVal ParamArray args As Object())
        CallModalPage(ResolveUrl(pageUrl), "1024", "700", CheckRight, commandList, args)
    End Sub

    ' <summary>
    ' 呼叫網頁,根據組態值會在同一個瀏覽器轉換到新的位址或者彈出新的凍結式子視窗
    ' </summary>
    ' <param name="pageUrl">網頁位址(必須位於同一應用程式虛擬目錄)</param>
    ' <param name="commandName">傳遞給子視窗網頁的處理命令名稱</param>
    ' <param name="args">附加參數</param>
    Public Sub CallPage(ByVal pageUrl As String, ByVal commandList() As ButtonState, ByVal ParamArray args As Object())
        Dim bolNeedButtonCtl As Boolean = False
        If commandList IsNot Nothing Then
            bolNeedButtonCtl = True
        End If

        Dim ti As New TransferInfo(TransferInfo.TransferInfoType.Call, FunID, BasePageUrl, pageUrl, PageSessionID, commandList, IIf(bolNeedButtonCtl, baseFunBarPageID, ""), args)    '20140818 wei modify
        transferState = ti
        If bolNeedButtonCtl Then
            Bsp.Utility.RunClientScript(Me, "OpenWindow(""" & ResolveUrl(pageUrl).Replace("""", """""") & """,true);")
        Else
            Bsp.Utility.RunClientScript(Me, "OpenWindow(""" & ResolveUrl(pageUrl).Replace("""", """""") & """);")
        End If
    End Sub

    Public Sub CallPage(ByVal pageUrl As String, ByVal ParamArray args() As Object)
        Dim bolNeedButtonCtl As Boolean = False

        Dim ti As New TransferInfo(TransferInfo.TransferInfoType.Call, FunID, BasePageUrl, pageUrl, PageSessionID, Nothing, IIf(bolNeedButtonCtl, baseFunBarPageID, ""), args)   '20140818 wei modify
        transferState = ti
        Bsp.Utility.RunClientScript(Me, "OpenWindow(""" & ResolveUrl(pageUrl).Replace("""", """""") & """);")
    End Sub

    Public Sub CallReportPage(ByVal reportName As String, ByVal ht As Hashtable)
        CallReportPage(reportName, ht, True)
    End Sub

    Public Sub CallReportPage(ByVal reportName As String, ByVal ht As Hashtable, ByVal NewPage As Boolean)
        Dim pageUrl As String = Bsp.Utility.getAppSetting("ReportPage")
        CallReportPage(reportName, ht, pageUrl, NewPage)
    End Sub

    Public Sub CallReportPage(ByVal reportName As String, ByVal ht As Hashtable, ByVal pageUrl As String)
        CallReportPage(reportName, ht, pageUrl, True)
    End Sub

    Public Sub CallReportPage(ByVal reportName As String, ByVal ht As Hashtable, ByVal pageUrl As String, ByVal NewPage As Boolean)
        Dim strParam As New StringBuilder

        pageUrl = String.Format("{0}?rpt={1}", pageUrl, reportName)
        For Each Key As String In ht.Keys
            strParam.Append(String.Format("&{0}={1}", Key, ht.Item(Key)))
        Next
        pageUrl &= strParam.ToString()

        Dim ti As New TransferInfo(TransferInfo.TransferInfoType.Call, ScsProgramID, BasePageUrl, pageUrl, PageSessionID, Nothing, False) '20140818 wei modify
        transferState = ti

        If NewPage Then
            Bsp.Utility.RunClientScript(Me, "OpenWindow(""" & ResolveUrl(pageUrl).Replace("""", """""") & """);")
        Else
            Bsp.Utility.RunClientScript(Me, "window.parent.location = """ & ResolveUrl(pageUrl).Replace("""", """""") & """;")
        End If
    End Sub
    ' <summary>
    ' 呼叫網頁,在同一個瀏覽器轉換到新的網頁位址
    ' </summary>
    ' <param name="pageUrl">網頁位址(必須位於同一應用程式虛擬目錄)</param>
    ' <param name="commandName">傳遞給子視窗網頁的處理命令名稱</param>
    ' <param name="args">附加參數</param>
    Protected Sub TransferPage(ByVal pageUrl As String, ByVal commandList() As ButtonState, ByVal ParamArray args As Object())
        pageUrl = ResolveUrl(pageUrl)
        Dim ti As New TransferInfo(TransferInfo.TransferInfoType.Transfer, FunID, BasePageUrl, pageUrl, PageSessionID, commandList, "", args) '20140818 wei modify
        transferState = ti
        UIRedirect(pageUrl)
    End Sub

    ' <summary>
    ' 呼叫網頁,在同一個Frame
    ' </summary>
    ' <param name="pageUrl">網頁位址(必須位於同一應用程式虛擬目錄)</param>
    ' <param name="commandName">傳遞給子視窗網頁的處理命令名稱</param>
    ' <param name="args">附加參數</param>
    Protected Sub TransferFramePage(ByVal pageUrl As String, ByVal commandList() As ButtonState, ByVal ParamArray args As Object())
        TransferFramePage(ResolveUrl(pageUrl), True, commandList, args)
    End Sub

    Protected Sub TransferFramePage(ByVal pageUrl As String, ByVal CheckRight As Boolean, ByVal commandList() As ButtonState, ByVal ParamArray args As Object())
        pageUrl = ResolveUrl(pageUrl)
        Dim ti As New TransferInfo(TransferInfo.TransferInfoType.Transfer, FunID, BasePageUrl, pageUrl, PageSessionID, commandList, baseFunBarPageID, args) '20140818 wei modify
        ti.CheckRight = CheckRight
        transferState = ti
        Bsp.Utility.RunClientScript(Me, "RedirectPage(""" & pageUrl & """)")
    End Sub

    Protected Sub TransferFlowPage(ByVal pageUrl As String, ByVal commandList() As ButtonState, ByVal ParamArray args As Object())
        pageUrl = ResolveUrl(pageUrl)
        Dim ti As New TransferInfo(TransferInfo.TransferInfoType.Transfer, FunID, BasePageUrl, pageUrl, PageSessionID, commandList, baseFunBarPageID & "," & baseFlowMenuPageID, args)   '20140818 wei modify
        ti.FlowUse = True
        transferState = ti
        Bsp.Utility.RunClientScript(Me, "RedirectFlowPage(""" & pageUrl & """)")
    End Sub

    Protected Sub ModalPageReturn(ByVal pageUrl As String, ByVal ParamArray args As Object())
        pageUrl = ResolveUrl(pageUrl)
        Dim ti As New TransferInfo(TransferInfo.TransferInfoType.ModalReturn, FunID, BasePageUrl, pageUrl, PageSessionID, Nothing, baseFunBarPageID, args)   '20140818 wei modify
        transferState = ti
        Bsp.Utility.RunClientScript(Me, "window.top.close();")
    End Sub

    Public Sub UIRedirect(ByVal url As String)
        PageSession.Clear()
        Response.Redirect(url, True)
    End Sub

    Private Function newExceptionInvalidConfig(ByVal configKey As String) As ApplicationException
        Throw New ApplicationException(String.Format("Invalid configuration value in web.config key='{0}'", configKey))
    End Function

    ' <summary>
    ' 離開目前網頁程式
    ' </summary>
    Protected Sub DoExit()
        Dim ti As TransferInfo = StateTransfer

        If ti IsNot Nothing Then
            Select Case ti.TransferType
                Case TransferInfo.TransferInfoType.Call
                    ti.TransferType = TransferInfo.TransferInfoType.Return
                    ti.Message = PageMessage
                    ti.ForceAlertMessage = UIForceAlertMessage
                    transferState = ti
                    UIRedirect(ti.CallerUrl)
                Case TransferInfo.TransferInfoType.ModalChild
                    ti.TransferType = TransferInfo.TransferInfoType.ModalReturn
                    ti.Message = PageMessage
                    ti.ForceAlertMessage = UIForceAlertMessage
                    transferState = ti
                    'UICloseWindow()
                    Bsp.Utility.RunClientScript(Me, "window.top.returnValue='';" & vbCrLf & "window.top.close();")
            End Select
        End If
        'UICloseWindow()
    End Sub

    Public Function selectedRow(ByVal GridViewName As String) As Integer
        Dim objSelectedRow As TextBox = Me.Page.Form.FindControl("__SelectedRow" & GridViewName)
        If objSelectedRow IsNot Nothing Then
            If objSelectedRow.Text = "" Then Return -1
            Return CInt(objSelectedRow.Text)
        Else
            Return -1
        End If
    End Function

    Public Function selectedRow(ByVal gv As GridView) As Integer
        Return selectedRow(gv.ID)
    End Function

    '給Checkbox多重選取使用
    Public Function selectedRows(ByVal GridViewName As String) As String
        Dim objSelectedRows As TextBox = Me.Page.Form.FindControl("__SelectedRows" & GridViewName)
        If objSelectedRows IsNot Nothing Then
            Return objSelectedRows.Text
        Else
            Return ""
        End If
    End Function

    Public Function selectedRows(ByVal gv As GridView) As String
        Return selectedRows(gv.ID)
    End Function

    Public Sub clearSelectedRows(ByVal GridViewName As String)
        Dim objSelectedRows As TextBox = Me.Page.Form.FindControl("__SelectedRows" & GridViewName)
        If objSelectedRows IsNot Nothing Then
            objSelectedRows.Text = ""
        End If
    End Sub

    Public Sub clearSelectedRows(ByVal gv As GridView)
        clearSelectedRows(gv.ID)
    End Sub

    Protected Property UIForceAlertMessage() As Boolean
        Get
            If Context.Items("UIForceAlertMessage") Is Nothing Then
                Context.Items("UIForceAlertMessage") = False
            End If

            Return CType(Context.Items("UIForceAlertMessage"), Boolean)
        End Get
        Set(ByVal value As Boolean)
            Context.Items("UIForceAlertMessage") = value
        End Set
    End Property

    ' <summary>
    ' 網頁執行時所要顯示給使用者的訊息
    ' </summary>
    Protected Property PageMessage() As String
        Get
            If Context.Items("PageMessage") Is Nothing Then Context.Items("PageMessage") = ""
            Return CType(Context.Items("PageMessage"), String)
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then
                Context.Items.Remove("PageMessage")
            Else
                Context.Items("PageMessage") = value
            End If
        End Set
    End Property

    Protected Overridable Sub BaseRegisterJavascript()

    End Sub

    '註冊Client端訊息傳遞時的控制項
    Private Sub RegisterParamObject()
        Dim btnAction As New Button
        Dim btnDoModalReturn As New Button
        Dim btnGridRowClick As New Button
        Dim txtParam As New TextBox
        Dim txtReturnValue As New TextBox
        Dim colGridName As New Collection
        Dim txtFunID As New TextBox
        Dim txtCompRoleID As New TextBox    '20140818 wei add

        btnAction.ID = "__btnAction"
        btnAction.Style.Item("display") = "none"
        btnAction.OnClientClick = "return SecurityActionButtonClick();"
        AddHandler btnAction.Click, AddressOf btnAction_Click

        txtParam.ID = "__ActionParam"
        txtParam.Style.Item("display") = "none"

        '紀錄FunID
        txtFunID.ID = "__FunID"
        txtFunID.Style.Item("display") = "none"
        If Request("_FunID") IsNot Nothing AndAlso Request("_FunID") <> "" Then
            txtFunID.Text = Request("_FunID")
        End If

        '20140818 wei add 紀錄CompRoleID
        txtCompRoleID.ID = "__CompRoleID"
        txtCompRoleID.Style.Item("display") = "none"
        If Request("_CompRoleID") IsNot Nothing AndAlso Request("_CompRoleID") <> "" Then
            txtCompRoleID.Text = Request("_CompRoleID")
        End If

        'ShowModal returnvalue使用
        txtReturnValue.ID = "__returnValue"
        txtReturnValue.Style.Item("display") = "none"

        btnDoModalReturn.ID = "__btnDoModalReturn"
        btnDoModalReturn.Style.Item("display") = "none"
        AddHandler btnDoModalReturn.Click, AddressOf btnDoModalReturn_Click

        'Grid row click時使用
        btnGridRowClick.ID = "__btnGridRowClick"
        btnGridRowClick.Style.Item("display") = "none"
        AddHandler btnGridRowClick.Click, AddressOf btnGridView_RowClick

        Page.Form.Controls.Add(btnAction)
        Page.Form.Controls.Add(txtParam)
        Page.Form.Controls.Add(txtFunID)
        Page.Form.Controls.Add(txtCompRoleID)    '20140818 wei add
        Page.Form.Controls.Add(txtReturnValue)
        Page.Form.Controls.Add(btnDoModalReturn)
        Page.Form.Controls.Add(btnGridRowClick)

    End Sub

    '設定網頁上物件預設的Event
    Private Sub setObjectDefaultEvent()

        If GridViewNames Is Nothing Then
            getAllGridView(Page.Form, Page.Form.ID)
        End If

        If GridViewNames IsNot Nothing Then
            Dim ht As Hashtable = CType(GridViewNames, Hashtable)
            For Each strKey As String In ht.Keys
                Dim aryGV() As String = ht(strKey).ToString().Split(".")
                Dim ctl As Control = Page
                For intLoop As Integer = 0 To aryGV.GetUpperBound(0)
                    If aryGV(intLoop) <> "" Then
                        ctl = ctl.FindControl(aryGV(intLoop))
                        If ctl Is Nothing Then Exit For
                    End If
                Next
                If ctl IsNot Nothing Then
                    AddHandler CType(ctl, GridView).RowDataBound, AddressOf GridView_RowDataBound
                    AddHandler CType(ctl, GridView).DataBinding, AddressOf GridView_DataBinding
                    createGridViewChildObject(strKey)
                End If
            Next
        End If
    End Sub

    Private Sub getAllGridView(ByVal ctl As System.Web.UI.Control, ByVal ParentName As String)
        If TypeOf ctl Is GridView Then
            Dim ht As Hashtable
            If GridViewNames Is Nothing Then
                ht = New Hashtable
            Else
                ht = CType(GridViewNames, Hashtable)
            End If
            If Not ht.ContainsKey(ctl.ID) Then
                ht.Add(ctl.ID, ParentName)
                GridViewNames = ht
            End If
        Else
            For Each subctl As Control In ctl.Controls
                getAllGridView(subctl, ParentName & "." & subctl.ID)
            Next
        End If
    End Sub

    Private Sub createGridViewChildObject(ByVal gvName As String)
        If Page.Form.FindControl("__SelectedRow" & gvName) Is Nothing Then
            Dim txtSelectedRow As New TextBox
            txtSelectedRow.ID = "__SelectedRow" & gvName
            txtSelectedRow.Style.Item("display") = "none"
            Page.Form.Controls.Add(txtSelectedRow)
        End If

        If Page.Form.FindControl("__SelectedRows" & gvName) Is Nothing Then
            Dim txtSelectedRows As New TextBox
            txtSelectedRows.ID = "__SelectedRows" & gvName
            txtSelectedRows.Style.Item("display") = "none"
            Page.Form.Controls.Add(txtSelectedRows)
        End If

        If Page.Form.FindControl("__GridViewRadioID" & gvName) Is Nothing Then
            Dim txtRowID As New TextBox
            txtRowID.ID = "__GridViewRadioID" & gvName
            txtRowID.Style.Item("display") = "none"
            Page.Form.Controls.Add(txtRowID)
        End If

        If Page.Form.FindControl("__GridViewCheckboxID" & gvName) Is Nothing Then
            Dim txtCheckBoxID As New TextBox
            txtCheckBoxID.ID = "__GridViewCheckboxID" & gvName
            txtCheckBoxID.Style.Item("display") = "none"
            Page.Form.Controls.Add(txtCheckBoxID)
        End If
    End Sub

    '啟用GridView的Row Click，但必須是在不使用checkbox及radiobutton的狀況下才允許使用
    Public Overridable ReadOnly Property EnableRowClickGridName() As String
        Get
            Return ""
        End Get
    End Property

    '設定GridView的RowCreate Event,以便控制mouseover和mouseout顏色
    Private Sub GridView_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim objGridView As GridView = CType(sender, GridView)

            If ExcludeLightBarGridName.ToString().IndexOf(objGridView.ID) < 0 OrElse FunID = "PAGEQUERYMULTISELECT" Then
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='" & Color_LightBar & "';")
            End If
            e.Row.Style.Item("cursor") = "hand"
            e.Row.Attributes.Add("id", "tr_" & objGridView.ID & "_" & e.Row.RowIndex.ToString())

            Dim objRadioButton As RadioButton = e.Row.FindControl("rdo_" & objGridView.ID)
            Dim objCheckBox As CheckBox = e.Row.FindControl("chk_" & objGridView.ID)

            If objRadioButton IsNot Nothing Then
                e.Row.Attributes.Add("onmouseout", "do_onmouseout(this, '" & objGridView.ID.ToString() & "', '" & e.Row.RowIndex.ToString() & "');")
                Dim objRadioID As TextBox = Me.Page.Form.FindControl("__GridViewRadioID" & objGridView.ID)
                If objRadioID IsNot Nothing Then
                    objRadioID.Text &= objRadioButton.ClientID & ","
                    e.Row.Attributes.Add("onclick", "__RowSelected(this, '" & objGridView.ID.ToString() & "', '" & e.Row.RowIndex.ToString() & "', '" & objRadioButton.ClientID & "');")
                Else
                    e.Row.Attributes.Add("onclick", "__RowSelected(this, '" & objGridView.ID.ToString() & "', '" & e.Row.RowIndex.ToString() & "', '');")
                End If
            ElseIf objCheckBox IsNot Nothing Then
                e.Row.Attributes.Add("onmouseout", "do_onmouseout2(this, '" & objGridView.ID.ToString() & "', '" & objGridView.PageIndex.ToString() & "', '" & e.Row.RowIndex.ToString("0000") & "');")
                Dim objSelected As TextBox = Me.Page.Form.FindControl("__SelectedRows" & objGridView.ID)
                If objSelected IsNot Nothing Then
                    If objSelected.Text.ToString().IndexOf(objGridView.PageIndex.ToString() & "." & e.Row.RowIndex.ToString("0000")) >= 0 Then
                        objCheckBox.Checked = True
                        e.Row.Style.Item("background-color") = Color_SelectedRow
                    End If
                End If

                e.Row.Attributes.Add("onclick", "__CheckboxSelected(this, '" & objGridView.ID.ToString() & "', '" & objGridView.PageIndex.ToString() & "', '" & e.Row.RowIndex.ToString("0000") & "', '" & objCheckBox.ClientID & "');")
                objCheckBox.Attributes.Add("onclick", "__CheckboxSelected(this, '" & objGridView.ID.ToString() & "', '" & objGridView.PageIndex.ToString() & "', '" & e.Row.RowIndex.ToString("0000") & "', '" & objCheckBox.ClientID & "');")
            Else
                If ExcludeLightBarGridName.ToString().IndexOf(objGridView.ID) < 0 AndAlso FunID <> "PAGEQUERYMULTISELECT" Then
                    e.Row.Attributes.Add("onmouseout", "do_onmouseout(this, '" & objGridView.ID.ToString() & "', '" & e.Row.RowIndex.ToString() & "');")
                    e.Row.Attributes.Add("onclick", "__RowSelected(this, '" & objGridView.ID.ToString() & "', '" & e.Row.RowIndex.ToString() & "', '');")
                End If
                If EnableRowClickGridName.ToString().IndexOf(objGridView.ID) >= 0 Then
                    e.Row.Attributes.Add("onclick", "__doGridRowClick('" & objGridView.ID & "','" & e.Row.RowIndex.ToString() & "');")
                End If
            End If
        End If
    End Sub

    Protected Sub GridView_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objSelectedRow As Control = Form.FindControl("__SelectedRow" & CType(sender, GridView).ID.ToString())
        Dim objRadioID As Control = Form.FindControl("__GridViewRadioID" & CType(sender, GridView).ID.ToString())
        If objSelectedRow IsNot Nothing Then
            CType(objSelectedRow, TextBox).Text = ""
        End If
        If objRadioID IsNot Nothing Then
            CType(objRadioID, TextBox).Text = ""
        End If
    End Sub

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        Me.Page.MaintainScrollPositionOnPostBack = True

        '清掉網頁的Cache
        Response.Buffer = True
        Response.Cache.SetExpires(Date.Today.AddDays(-10))

        Dim Script As String
        'If Not Page.ClientScript.IsClientScriptIncludeRegistered("jquery-1.8.3") Then
        '    Script = "~/ClientFun/jquery-1.8.3.min.js"
        '    Page.ClientScript.RegisterClientScriptInclude(GetType(Page), "jquery-1.8.3", ResolveUrl(Script))
        'End If

        'If Not Page.ClientScript.IsClientScriptIncludeRegistered("jquery.blockUI") Then
        '    Script = "~/ClientFun/jquery.blockUI.js"
        '    Page.ClientScript.RegisterClientScriptInclude(GetType(Page), "jquery.blockUI", ResolveUrl(Script))
        'End If
        If Not Page.ClientScript.IsClientScriptIncludeRegistered("ButtonAction") Then
            Script = "~/ClientFun/ButtonAction.js"
            Page.ClientScript.RegisterClientScriptInclude(GetType(Page), "ButtonAction", ResolveUrl(Script))
        End If

        If Not Page.ClientScript.IsClientScriptIncludeRegistered("UserControl") Then
            Script = "~/ClientFun/UserControl.js"
            Page.ClientScript.RegisterClientScriptInclude(GetType(Page), "UserControl", ResolveUrl(Script))
        End If

        Dim obj As Object = Page.Form.FindControl("smBase")
        If obj IsNot Nothing Then
            If TypeOf obj Is ScriptManager Then
                CType(obj, ScriptManager).Scripts.Add(New ScriptReference("~/ClentFun/ClientFun.js"))
                CType(obj, ScriptManager).Scripts.Add(New ScriptReference("~/ClentFun/UserControl.js"))
                CType(obj, ScriptManager).Scripts.Add(New ScriptReference("~/ClentFun/ButtonAction.js"))
            ElseIf TypeOf obj Is AjaxControlToolkit.ToolkitScriptManager Then
                CType(obj, AjaxControlToolkit.ToolkitScriptManager).Scripts.Add(New ScriptReference("~/ClentFun/ClientFun.js"))
                CType(obj, AjaxControlToolkit.ToolkitScriptManager).Scripts.Add(New ScriptReference("~/ClentFun/UserControl.js"))
                CType(obj, AjaxControlToolkit.ToolkitScriptManager).Scripts.Add(New ScriptReference("~/ClentFun/ButtonAction.js"))
            End If
        End If

        '設定網頁傳遞訊息的物件
        RegisterParamObject()
        '設定網頁上物件預設的Event
        setObjectDefaultEvent()

        RenewSessionTimeout()

        Dim ti As TransferInfo = Nothing
        If transferState IsNot Nothing Then

            Select Case transferState.TransferType
                Case TransferInfo.TransferInfoType.Call, TransferInfo.TransferInfoType.ModalChild, TransferInfo.TransferInfoType.ModelessChild, TransferInfo.TransferInfoType.Transfer
                    If transferState.AllReceivers.Contains(BasePageID.ToLower()) Then
                        ti = transferState
                        StateTransfer = ti
                        transferState.AllReceivers.Remove(BasePageID.ToLower())
                        If transferState.AllReceivers.Count = 0 Then
                            transferState = Nothing
                        End If
                    End If
                Case TransferInfo.TransferInfoType.Return, TransferInfo.TransferInfoType.ModalReturn
                    ti = transferState
                    transferState = Nothing
            End Select
        End If

        MyBase.OnLoad(e)

        If ti IsNot Nothing Then
            Select Case ti.TransferType
                Case TransferInfo.TransferInfoType.Call, TransferInfo.TransferInfoType.ModalChild
                    If ti.CallerPageID.ToUpper() <> "SC0060" AndAlso ti.RunFunID.ToLower() = BasePageID.ToLower() Then
                        BaseOnPageCall(ti)
                    End If
                Case TransferInfo.TransferInfoType.ModelessChild
                    If ti.CallerPageID.ToUpper() <> "SC0060" AndAlso ti.RunFunID.ToLower() = BasePageID.ToLower Then
                        BaseOnPageLaunch(ti)
                    End If
                Case TransferInfo.TransferInfoType.Return, TransferInfo.TransferInfoType.ModalReturn
                    If ti.CallerPageID.ToUpper() <> "SC0060" AndAlso ti.CallerPageID.ToLower() = BasePageID.ToLower() Then
                        BaseOnPageReturn(ti)
                    End If
                Case TransferInfo.TransferInfoType.Transfer
                    If (ti.CallerPageID.ToUpper() <> "SC0060" AndAlso ti.RunFunID.ToLower() = BasePageID.ToLower()) OrElse _
                        BasePageID.ToLower = baseFlowMenuPageID.ToLower() Then
                        BaseOnPageTransfer(ti)
                    End If
            End Select
        End If
    End Sub

    Protected Overrides Sub OnError(ByVal e As System.EventArgs)
        Dim ex As Exception = Server.GetLastError
        Dim Msg As String '= ex.Message
        'Dim StackMessage As New StringBuilder()
        Dim strFunID As String
        Try
            strFunID = FunID
        Catch ex1 As Exception
            strFunID = ""
        End Try

        Msg = Bsp.Utility.getInnerException(strFunID & ".OnError", ex) ', StackMessage.ToString())
        ' Prmpt Msg
        Response.Write("<script language=""javascript"">alert(""" & Msg.Replace("\", "\\").Replace(vbCrLf, "\r\n").Replace("""", "'") & """);</Script>")
        ' Write Event

        Server.ClearError()
        MyBase.OnError(e)
    End Sub

    Protected Overrides Sub OnPreRender(ByVal e As System.EventArgs)
        replaceObjectCaption()
        BaseRegisterJavascript()

        MyBase.OnPreRender(e)
    End Sub

    Private Sub RenewSessionTimeout()
        Dim cookie As New HttpCookie("secondsTimeout", (HttpContext.Current.Session.Timeout * 60).ToString())
        cookie.Path = "/"
        cookie.Expires = DateTime.MinValue
        HttpContext.Current.Response.Cookies.Set(cookie)
    End Sub

    '多國語系預留
    Private Sub replaceObjectCaption()

    End Sub

    '<summary>
    '   __btnAction觸發時的動作
    '</summary>
    Private Sub btnAction_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim objTextParam As TextBox = Page.Form.FindControl("__ActionParam")

        If objTextParam IsNot Nothing Then
            DoAction(objTextParam.Text)
        End If
    End Sub

    '<summary>
    '   __btnDoModalReturn觸發時的動作
    '</summary>
    Private Sub btnDoModalReturn_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim objTextParam As TextBox = Page.Form.FindControl("__returnValue")

        If objTextParam IsNot Nothing Then
            DoModalReturn(objTextParam.Text)
        End If
    End Sub

    '<summary>
    '   __btnGridRowClick觸發時的動作
    '</summary>
    Private Sub btnGridView_RowClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim objTextParam As TextBox = Page.Form.FindControl("__ActionParam")

        If objTextParam IsNot Nothing Then
            DoGridRowClick(objTextParam.Text, selectedRow(objTextParam.Text))
        End If
    End Sub

    '<summary>
    '   後代網頁需覆寫此段程序 
    '   Param參數包括btnAdd, btnUpdate, btnDelete, btnQuery, btnActionX, btnActionP, btnActionX
    '</summary>
    Public Overridable Sub DoAction(ByVal Param As String)

    End Sub

    '<summary>
    '   後代網頁需覆寫此段程序 
    '   當CallModalPage後，關閉網頁呼叫此段
    '</summary>
    Public Overridable Sub DoModalReturn(ByVal returnValue As String)

    End Sub

    '<summary>
    '   後代網頁需覆寫此段程序 
    '   當Gridview Row Click後，設定於EnableRowClickGridName的Grid，將會觸發此段程式
    '</summary>
    Public Overridable Sub DoGridRowClick(ByVal GridName As String, ByVal rowIndex As Integer)

    End Sub

    '將畫面上的元件設定為失效狀態
    Public Sub DisableObjects(ByVal ctl As Control)
        If TypeOf ctl Is TextBox Then
            CType(ctl, TextBox).Enabled = False
            Return
        End If
        If TypeOf ctl Is DropDownList Then
            CType(ctl, DropDownList).Enabled = False
            Return
        End If
        If TypeOf ctl Is RadioButton Then
            CType(ctl, RadioButton).Enabled = False
            Return
        End If
        If TypeOf ctl Is RadioButtonList Then
            CType(ctl, RadioButtonList).Enabled = False
            Return
        End If
        If TypeOf ctl Is CheckBox Then
            CType(ctl, CheckBox).Enabled = False
            Return
        End If
        If TypeOf ctl Is Button Then
            CType(ctl, Button).Enabled = False
            Return
        End If
        If TypeOf ctl Is LinkButton Then
            CType(ctl, LinkButton).Enabled = False
            Return
        End If
        If TypeOf ctl Is GridView Then
            DisableGridViewButton(CType(ctl, GridView))
            Return
        End If

        For Each subctl As Control In ctl.Controls
            DisableObjects(subctl)
        Next
    End Sub

    Private Sub DisableGridViewButton(ByVal gv As GridView)
        For Each a As GridViewRow In gv.Rows
            DisableObjects(a)
        Next
    End Sub

    ''' <summary>
    ''' 將傳入的ParamArray轉成ViewState
    ''' </summary>
    ''' <param name="Params"></param>
    ''' <remarks></remarks>
    Public Overridable Sub Object2ViewState(ByVal ParamArray Params As Object())
        If Params.Length > 0 Then
            For intLoop As Integer = 0 To Params.Length - 1
                Dim intPos As Integer = Params(intLoop).ToString().IndexOf("=")

                If intPos >= 0 Then
                    ViewState.Item(Params(intLoop).ToString().Substring(0, intPos)) = Params(intLoop).ToString().Substring(intPos + 1)
                End If
            Next
        End If
    End Sub

    ''' <summary>
    ''' 將傳入的參數字串轉成ViewState 
    ''' </summary>
    ''' <param name="Params"></param>
    ''' <remarks><![CDATA[字串格式需為A=1&B=2&C=3...依此類推]]></remarks>
    Public Overridable Sub Object2ViewState(ByVal Params As String)
        Dim aryParam() As String = Params.Split("&")

        For intLoop As Integer = 0 To aryParam.GetUpperBound(0)
            Dim intPos As Integer = aryParam(intLoop).IndexOf("=")

            If intPos >= 0 Then
                ViewState.Item(aryParam(intLoop).Substring(0, intPos)) = aryParam(intLoop).Substring(intPos + 1)
            End If
        Next
    End Sub
End Class
