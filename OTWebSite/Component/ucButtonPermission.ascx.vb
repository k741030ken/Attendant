'****************************************************
'功能說明：功能頁上的Button ascx
'建立人員：Chung
'建立日期：2011.05.13
'20150715 wei modify 增加Button 新Button如下:
'A-新增，C-確認，D-刪除，I-查詢，L-下傳，P-列印，R-放行，U-修改，X-清除(改名)，B-駁回(新增)，E-執行(新增)，F-上傳(新增)，G-複製(新增)，H-保留(新增)，J-保留(新增)
'****************************************************
Partial Class Component_ucButtonPermission
    Inherits System.Web.UI.UserControl

    Private _buttonList As ButtonState()

    '是否自動載入
    Public Property AutoLoad() As Boolean
        Get
            If ViewState.Item("_AutoLoading") Is Nothing Then
                ViewState.Item("_AutoLoading") = False
            End If
            Return ViewState.Item("_AutoLoading")
        End Get
        Set(ByVal value As Boolean)
            ViewState.Item("_AutoLoading") = value
        End Set
    End Property

    Public Property FunID() As String
        Get
            If ViewState.Item("_FunID") Is Nothing Then
                ViewState.Item("_FunID") = ""
            End If
            Return ViewState.Item("_FunID")
        End Get
        Set(ByVal value As String)
            ViewState.Item("_FunID") = value
        End Set
    End Property
    '20140808 wei add SysID
    Public Property SysID() As String
        Get
            If ViewState.Item("_SysID") Is Nothing Then
                ViewState.Item("_SysID") = ""
            End If
            Return ViewState.Item("_SysID")
        End Get
        Set(ByVal value As String)
            ViewState.Item("_SysID") = value
        End Set
    End Property
    '20140808 wei CompRoleID
    Public Property CompRoleID() As String
        Get
            If ViewState.Item("_CompRoleID") Is Nothing Then
                ViewState.Item("_CompRoleID") = ""
            End If
            Return ViewState.Item("_CompRoleID")
        End Get
        Set(ByVal value As String)
            ViewState.Item("_CompRoleID") = value
        End Set
    End Property

    Public Property CheckRight() As Boolean
        Get
            If ViewState.Item("_CheckRight") Is Nothing Then
                ViewState.Item("_CheckRight") = True
            End If
            Return ViewState.Item("_CheckRight")
        End Get
        Set(ByVal value As Boolean)
            ViewState.Item("_CheckRight") = value
        End Set
    End Property

    Public ReadOnly Property ButtonCount() As Integer
        Get
            If ViewState.Item("_ButtonCount") Is Nothing Then
                ViewState.Item("_ButtonCount") = 0
            End If
            Return ViewState.Item("_ButtonCount")
        End Get
    End Property

    Public Property ButtonList() As ButtonState()
        Get
            Return _buttonList
        End Get
        Set(ByVal value As ButtonState())
            _buttonList = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If AutoLoad Then
                DoLoad()
            End If
        End If
    End Sub

    '必要呼叫項目
    Public Sub DoLoad()
        If CheckRight Then
            buildButton(getButtonList(FunID, SysID,CompRoleID))
        Else
            If ButtonList IsNot Nothing Then
                buildButton(ButtonList)
            Else
                If FunID IsNot Nothing Then
                    buildButton(getButtonList(FunID, SysID, CompRoleID))
                End If
            End If
        End If
    End Sub

    Private Function getButtonList(ByVal strFunID As String, ByVal strSysID As String, ByVal strCompRoleID As String) As ButtonState()
        Dim objUC As New UC()
        Dim dtRight As Data.DataTable

        Try
            Using dt As Data.DataTable = objUC.BP_GetFun(strFunID, strSysID)
                If dt.Rows.Count = 0 Then
                    dtRight = objUC.GetFunRight(strFunID, strSysID)
                Else
                    If dt.Rows(0).Item("CheckRight").ToString() = "1" Then
                        If CheckRight Then
                            dtRight = objUC.GetGroupFunRightbyUserID(UserProfile.UserID, strFunID, strSysID, strCompRoleID)
                        Else
                            dtRight = objUC.GetFunRight(strFunID, strSysID)
                        End If
                    Else
                        dtRight = objUC.GetFunRight(strFunID, strSysID)
                    End If
                End If
            End Using
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me.Page, "Component_ucButtonPermission.getButtonList", ex, strFunID)
            Return Nothing
        End Try

        If dtRight.Rows.Count = 0 Then Return Nothing

        Dim btnList(dtRight.Rows.Count - 1) As ButtonState
        Try
            For intLoop As Integer = 0 To dtRight.Rows.Count - 1
                Dim bs As New ButtonState

                bs.RightID = dtRight.Rows(intLoop).Item("RightID").ToString()
                Select Case bs.RightID
                    Case "A"    'A-新增
                        bs.ActionParam = "btnAdd"
                        bs.Name = "btnAdd"
                    Case "U"    'U-修改
                        bs.ActionParam = "btnUpdate"
                        bs.Name = "btnUpdate"
                    Case "D"    'D-刪除
                        bs.ActionParam = "btnDelete"
                        bs.Name = "btnDelete"
                    Case "I"    'I-查詢
                        bs.ActionParam = "btnQuery"
                        bs.Name = "btnQuery"
                    Case "R"    'R-放行
                        bs.ActionParam = "btnRelease"
                        bs.Name = "btnRelease"
                    Case "L"    'L-下傳
                        bs.ActionParam = "btnDownload"
                        bs.Name = "btnDownload"
                    Case "P"    'P-列印
                        bs.ActionParam = "btnPrint"
                        bs.Name = "btnPrint"
                    Case "B"    'B-駁回(新增) 20150716 wei add
                        bs.ActionParam = "btnReject"
                        bs.Name = "btnReject"
                    Case "E"    'E-執行(新增) 20150716 wei add
                        bs.ActionParam = "btnExecutes"
                        bs.Name = "btnExecutes"
                    Case "F"    'F-上傳(新增) 20150716 wei add
                        bs.ActionParam = "btnUpload"
                        bs.Name = "btnUpload"
                    Case "G"    'G-複製(新增) 20150716 wei add
                        bs.ActionParam = "btnCopy"
                        bs.Name = "btnCopy"
                    Case Else   'C-確認，X-清除(改名)，H-保留(新增)，J-保留(新增)
                        bs.ActionParam = "btnAction" & bs.RightID
                        bs.Name = "btnAction" & bs.RightID
                End Select
                'Button Caption
                If dtRight.Rows(intLoop).Item("Caption").ToString() = "" Then
                    bs.Caption = dtRight.Rows(intLoop).Item("RightName").ToString()
                Else
                    bs.Caption = dtRight.Rows(intLoop).Item("Caption").ToString()
                End If
                bs.Visible = IIf(dtRight.Rows(intLoop).Item("IsVisible").ToString() = "1", True, False)
                btnList(intLoop) = bs
            Next
        Catch ex As Exception
            Throw
        Finally
            If dtRight IsNot Nothing Then dtRight.Dispose()
        End Try

        Return btnList
    End Function

    Private Sub buildButton(ByVal buttonList() As ButtonState)
        If buttonList Is Nothing Then Return

        Dim intButtonLen As Integer

        ViewState.Item("_ButtonCount") = 0

        For intLoop As Integer = 0 To buttonList.GetUpperBound(0)
            If buttonList(intLoop).Visible Then
                Dim btnVirual As New Button()
                Dim lblSpace As New Label()

                With btnVirual
                    .ID = buttonList(intLoop).Name
                    .Text = buttonList(intLoop).Caption

                    intButtonLen = Bsp.Utility.getStringLength(.Text) * 11
                    If intButtonLen < 75 Then intButtonLen = 75

                    .Width = Unit.Parse(intButtonLen.ToString() & "px")
                    .Attributes("class") = "buttonface"
                    .Attributes("Language") = "javascript"
                    .Attributes("OnClick") = "return funAction('" & .ID & "')"
                End With
                If intLoop = 0 Then
                    lblSpace.Text = "　"
                Else
                    lblSpace.Text = "  "
                End If
                phButton.Controls.Add(lblSpace)
                phButton.Controls.Add(btnVirual)

                ViewState.Item("_ButtonCount") += 1
            End If
        Next
    End Sub
End Class
