'****************************************************
'功能說明：多選項選取
'建立人員：A03018
'建立日期：2007.03.20
'****************************************************


Partial Class Component_ucMultiSelect
    Inherits System.Web.UI.UserControl

    '顯示部門類型
    Public Enum emDeptType
        All = 0
        Dept = 1
        Bussiness = 2
    End Enum

    '顯示的User類型   
    Public Enum emUserType
        AO = 0
        AuthorizedUser = 1 '核貸委員        
        ValidUser = 2
    End Enum

    Public Enum emDisplayType
        OnlyName = 0    '只顯示名字
        Full = 1 '顯示ID + 名字
    End Enum

    '顯示的User類型
    Public Property UserType() As emUserType
        Get
            If ViewState.Item("UserType") Is Nothing Then
                ViewState.Item("UserType") = emUserType.ValidUser
            End If
            Return ViewState.Item("UserType")
        End Get
        Set(ByVal value As emUserType)
            ViewState.Item("UserType") = value
        End Set
    End Property

    '顯示內容
    Public Property DisplayType() As emDisplayType
        Get
            If ViewState.Item("DisplayType") Is Nothing Then
                ViewState.Item("DisplayType") = emDisplayType.Full
            End If
            Return ViewState.Item("DisplayType")
        End Get
        Set(ByVal value As emDisplayType)
            ViewState.Item("DisplayType") = value
        End Set
    End Property

    '是否要顯示分類選單
    Public Property ShowDeptType() As Boolean
        Get
            If ViewState.Item("ShowDeptType") Is Nothing Then
                ViewState.Item("ShowDeptType") = True
            End If
            Return ViewState.Item("ShowDeptType")
        End Get
        Set(ByVal value As Boolean)
            ViewState.Item("ShowDeptType") = value
        End Set
    End Property


    Public Property DeptType() As emDeptType
        Get
            If ViewState.Item("DeptType") Is Nothing Then
                ViewState.Item("DeptType") = emDeptType.All
            End If
            Return ViewState.Item("DeptType")
        End Get
        Set(ByVal value As emDeptType)
            ViewState.Item("DeptType") = value
        End Set
    End Property

    '固定單位選取(以逗號(,)隔開)
    Public Property FreezeDeptID() As String
        Get
            If ViewState.Item("FreezeDeptID") Is Nothing Then
                ViewState.Item("FreezeDeptID") = ""
            End If
            Return ViewState.Item("FreezeDeptID")
        End Get
        Set(ByVal value As String)
            ViewState.Item("FreezeDeptID") = value
        End Set
    End Property

    '左選項Title說明
    Public Property LeftCaption() As String
        Get
            Return lblLeftCaption.Text
        End Get
        Set(ByVal Value As String)
            lblLeftCaption.Text = Value
        End Set
    End Property

    '右選項Title說明
    Public Property RightCaption() As String
        Get
            Return lblRightCaption.Text
        End Get
        Set(ByVal Value As String)
            lblRightCaption.Text = Value
        End Set
    End Property

    Public ReadOnly Property ValueResult() As String
        Get
            Return txtValueResult.Text
        End Get
    End Property

    Public ReadOnly Property TextResult() As String
        Get
            Return txtTextResult.Text
        End Get
    End Property

    Public Property ListRows() As Integer
        Get
            Return lstLeft.Rows
        End Get
        Set(ByVal value As Integer)
            lstLeft.Rows = value
            lstRight.Rows = value
        End Set
    End Property

    'List Load自訂內容(未選)
    Public Sub LoadLeftData(ByVal dt As Data.DataTable)
        Try
            With lstLeft
                .DataSource = dt
                .DataValueField = dt.Columns(0).ColumnName
                .DataTextField = dt.Columns(1).ColumnName
                .DataBind()

                '扣除已選取的資料
                If txtRightResult.Text.Trim() <> "" Then
                    Dim strLists() As String = txtRightResult.Text.Split(CChar(vbTab))
                    For intLoop As Integer = 0 To strLists.GetUpperBound(0)
                        Dim intIndex As Integer = .Items.IndexOf(.Items.FindByValue(strLists(intLoop).Split("|")(1)))

                        If intIndex >= 0 Then
                            .Items.RemoveAt(intIndex)
                        End If
                    Next
                End If
            End With
        Catch ex As Exception          
            Bsp.Utility.ShowMessage(Me, "ucMultiSelect.LoadLeftData", ex)
            Return
        End Try

    End Sub

    'List Load自訂內容(已選)
    Public Sub LoadRightData(ByVal dt As Data.DataTable)
        Try
            With lstRight
                .DataSource = dt
                .DataValueField = dt.Columns(0).ColumnName
                .DataTextField = dt.Columns(1).ColumnName
                .DataBind()

                Dim strValue As String = ""
                Dim strText As String = ""
                For intLoop As Integer = 0 To dt.Rows.Count - 1
                    If strValue <> "" Then strValue &= ","
                    If strText <> "" Then strText &= ","

                    strValue &= dt.Rows(intLoop).Item(0).ToString()
                    strText &= dt.Rows(intLoop).Item(1).ToString()
                Next
                txtValueResult.Text = strValue
                txtTextResult.Text = strText
            End With
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me.Page, "ucMutiSelect.LoadRightData", ex)
            Return
        End Try

    End Sub


    '清除List內容
    Public Sub ClearList()
        lstLeft.Items.Clear()
        lstRight.Items.Clear()
        txtLeftResult.Text = ""
        txtRightResult.Text = ""
        txtValueResult.Text = ""
        txtTextResult.Text = ""
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            RegistScript()

            If ViewState.Item("ShowDeptType") = True Then
                pnlDeptTitle.Visible = True
                getDept()
            Else
                pnlDeptTitle.Visible = False
            End If
        End If

        If txtLeftResult.Text.Length > 0 Or txtRightResult.Text.Length > 0 Then
            RebuildList()
        End If

        If Not Page.ClientScript.IsClientScriptBlockRegistered("UserControl") Then
            Dim cstype As Type = Me.Page.GetType()
            Dim script As String = "<script  type='text/javascript' src='" & Me.ResolveUrl("~/ClientFun/UserControl.js") & "'></script>" & vbNewLine
            Me.Page.ClientScript.RegisterClientScriptBlock(cstype, "ucMutiSelect", script)
        End If
    End Sub

    Private Sub RebuildList()
        Try
            lstRight.Items.Clear()
            lstLeft.Items.Clear()
            Dim strLists() As String
            If txtRightResult.Text.Length > 0 Then
                strLists = txtRightResult.Text.Split(CChar(vbTab))
                For Each strKey As String In strLists
                    lstRight.Items.Add(New ListItem(strKey.Split("|")(0), strKey.Split("|")(1)))
                Next
            End If

            If txtLeftResult.Text.Length > 0 Then
                strLists = txtLeftResult.Text.Split(CChar(vbTab))
                For Each strKey As String In strLists
                    lstLeft.Items.Add(New ListItem(strKey.Split("|")(0), strKey.Split("|")(1)))
                Next
            End If
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me.Page, Bsp.Utility.getInnerException("ucMutiSelect.RebuildList", ex))
            Return
        End Try
    End Sub

    Private Sub RegistScript()
        Const CS_FUN As String = "javascript:{0}(this.form.{1},this.form.{2},this.form.{3},this.form.{4},this.form.{5},this.form.{6});return false;"
        btnMoveLeft.Attributes("OnClick") = String.Format(CS_FUN, "MoveLeft", lstLeft.ClientID, txtLeftResult.ClientID, lstRight.ClientID, txtRightResult.ClientID, txtTextResult.ClientID, txtValueResult.ClientID)
        btnMoveRight.Attributes("OnClick") = String.Format(CS_FUN, "MoveRight", lstLeft.ClientID, txtLeftResult.ClientID, lstRight.ClientID, txtRightResult.ClientID, txtTextResult.ClientID, txtValueResult.ClientID)
        btnMoveUp.Attributes("OnClick") = String.Format(CS_FUN, "MoveUp", lstLeft.ClientID, txtLeftResult.ClientID, lstRight.ClientID, txtRightResult.ClientID, txtTextResult.ClientID, txtValueResult.ClientID)
        btnMoveDown.Attributes("OnClick") = String.Format(CS_FUN, "MoveDown", lstLeft.ClientID, txtLeftResult.ClientID, lstRight.ClientID, txtRightResult.ClientID, txtTextResult.ClientID, txtValueResult.ClientID)
    End Sub

    Private Sub getDept()
        Dim strWhere As String = "And InValidFlag = '0'"

        If ViewState.Item("FreezeDeptID") = "" Then
            Select Case CType(ViewState.Item("DeptType"), emDeptType)
                Case emDeptType.Bussiness
                    strWhere &= vbCrLf & "And BusinessFlag = '1'"
                Case emDeptType.Dept
                    strWhere &= vbCrLf & "And OrganType = '1'"
            End Select
        Else
            Dim aryDeptID() As String = ViewState.Item("FreezeDeptID").Split(",")
            Dim strIn As String = ""

            For intLoop As Integer = 0 To aryDeptID.GetUpperBound(0)
                If strIn <> "" Then strIn &= ","

                strIn &= "'" & aryDeptID(intLoop) & "'"
            Next
            strWhere &= vbCrLf & "And OrganID in (" & strIn & ")"
        End If
        strWhere &= vbCrLf & "Order by OrganID"

        Dim objSC As New SC()

        Try
            Using dt As Data.DataTable = objSC.GetOrganInfo("", "OrganID, OrganName, OrganID + '-' + OrganName FullName", strWhere)
                With ddlDeptType
                    .DataSource = dt
                    .DataTextField = IIf(DisplayType = emDisplayType.Full, "FullName", "OrganName")
                    .DataValueField = "OrganID"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, "ucMultiSelect.getDept", ex)
            Return
        End Try
    End Sub


    Private Sub getUser()
        If ddlDeptType.SelectedIndex < 0 Then Exit Sub

        Dim strWhere As String = "And BanMark = '0' and DeptID = " & Bsp.Utility.Quote(ddlDeptType.SelectedItem.Value)

        Select Case CType(ViewState.Item("UserType"), emUserType)
            Case emUserType.AO
                strWhere &= vbCrLf & "And UserID in (Select UserID From SC_UserGroup Where GroupID = '01')"

            Case emUserType.AuthorizedUser '核貸委員


            Case emUserType.ValidUser

        End Select

        strWhere &= vbCrLf & "Order by UserID"

        Dim objSC As New SC()

        Try
            Using dt As Data.DataTable = objSC.GetUserInfo("", "UserID, UserName, UserID + '-' + UserName FullName", strWhere)
                With lstLeft
                    .Items.Clear()
                    .DataTextField = IIf(DisplayType = emDisplayType.Full, "FullName", "UserName")
                    .DataValueField = "UserID"
                    .DataSource = dt
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, "ucMultiSelect.getUser", ex)
            Return
        End Try
    End Sub

    Protected Sub ddlDeptType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDeptType.SelectedIndexChanged
        getUser()
    End Sub


End Class


