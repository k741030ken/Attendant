'****************************************************
'功能說明：部門人員選取
'建立人員：A02976
'建立日期：2007.03.20
'****************************************************

Partial Class Component_ucOneUserSelect
    Inherits System.Web.UI.UserControl

    'Private _UserType As emUserType = emUserType.ValidUser
    'Private _DisplayType As emDisplayType = emDisplayType.Full
    'Private _DeptType As emDeptType = emDeptType.All
    'Private _FreezeDeptID As String = ""
    'Private _MustSelect As Boolean

    '顯示的User類型
    Public Enum emUserType
        AO
        ValidUser
        Custom
    End Enum

    '顯示部門類型
    Public Enum emDeptType
        All
        Dept
        Bussiness
        RegionManager
        Custom
    End Enum

    'DisplayType
    Public Enum emDisplayType
        OnlyName    '只顯示名字
        Full        '顯示ID + 名字
    End Enum

    Public Enum emRegion
        C1
        C2
        All
    End Enum

    Public Property AutoPostBack() As Boolean
        Get
            Return ddlUser.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            ddlUser.AutoPostBack = value
        End Set
    End Property

    Public Property DeptControlWidth() As String
        Get
            Return ddlOrgan.Width.ToString()
        End Get
        Set(ByVal value As String)
            Try
                ddlOrgan.Width = Unit.Parse(value)
            Catch ex As Exception

            End Try
        End Set
    End Property

    Public Property UserControlWidth() As String
        Get
            Return ddlUser.Width.ToString()
        End Get
        Set(ByVal value As String)
            Try
                ddlUser.Width = Unit.Parse(value)
            Catch ex As Exception

            End Try
        End Set
    End Property

    '是否一定要選取人員
    Public Property MustSelect() As Boolean
        Get
            If ViewState.Item("_MustSelect") Is Nothing Then
                ViewState.Item("_MustSelect") = False
            End If
            Return ViewState.Item("_MustSelect")
        End Get
        Set(ByVal value As Boolean)
            ViewState.Item("_MustSelect") = value
        End Set
    End Property

    '固定單位選取(以逗號(,)隔開)
    Public Property FreezeDeptID() As String
        Get
            If ViewState.Item("_FreezeDeptID") Is Nothing Then
                ViewState.Item("_FreezeDeptID") = ""
            End If
            Return ViewState.Item("_FreezeDeptID")
        End Get
        Set(ByVal value As String)
            ViewState.Item("_FreezeDeptID") = value
        End Set
    End Property

    '固定群組(以逗號隔開)
    Public Property FreezeGroup() As String
        Get
            If ViewState.Item("_FreezeGroup") Is Nothing Then
                ViewState.Item("_FreezeGroup") = ""
            End If
            Return ViewState.Item("_FreezeGroup")
        End Get
        Set(ByVal value As String)
            ViewState.Item("_FreezeGroup") = value
        End Set
    End Property

    '顯示內容
    Public Property DisplayType() As emDisplayType
        Get
            If ViewState.Item("_DisplayType") Is Nothing Then
                ViewState.Item("_DisplayType") = emDisplayType.Full
            End If
            Return ViewState.Item("_DisplayType")
        End Get
        Set(ByVal value As emDisplayType)
            ViewState.Item("_DisplayType") = value
        End Set
    End Property

    '顯示的User類型
    Public Property UserType() As emUserType
        Get
            If ViewState.Item("_UserType") Is Nothing Then
                ViewState.Item("_UserType") = emUserType.ValidUser
            End If
            Return CType(ViewState.Item("_UserType"), emUserType)
        End Get
        Set(ByVal value As emUserType)
            ViewState.Item("_UserType") = value
        End Set
    End Property

    Public Property DeptType() As emDeptType
        Get
            If ViewState.Item("_DeptType") Is Nothing Then
                ViewState.Item("_DeptType") = emDeptType.All
            End If
            Return ViewState.Item("_DeptType")
        End Get
        Set(ByVal value As emDeptType)
            ViewState.Item("_DeptType") = value
        End Set
    End Property

    '被選取的部門
    Public ReadOnly Property SelectDeptID() As String
        Get
            If ddlOrgan.SelectedIndex >= 0 Then
                Return ddlOrgan.SelectedItem.Value
            End If
            Return ""
        End Get
    End Property

    Public ReadOnly Property SelectDeptName() As String
        Get
            If ddlOrgan.SelectedIndex >= 0 Then
                If DisplayType = emDisplayType.Full Then
                    Return ddlOrgan.SelectedItem.Text.Split("-")(1).Trim()
                Else
                    Return ddlOrgan.SelectedItem.Text
                End If
            End If
            Return ""
        End Get
    End Property

    Public ReadOnly Property SelectedUserID() As String
        Get
            If ddlUser.SelectedIndex >= 0 Then
                Return ddlUser.SelectedItem.Value
            End If
            Return ""
        End Get
    End Property

    Public ReadOnly Property SelectedUserName() As String
        Get
            If ddlUser.SelectedIndex >= 0 Then
                If DisplayType = emDisplayType.Full Then
                    Return ddlUser.SelectedItem.Text.Split("-")(1).Trim()
                Else
                    Return ddlUser.SelectedItem.Text
                End If
            End If
            Return ""
        End Get
    End Property

    'ADD by Chung 2008.12.16 增加禁止移轉單位限制, 多個以逗號隔開
    Public Property InvisibleDept() As String
        Get
            If ViewState.Item("InvisibleDept") Is Nothing Then
                ViewState.Item("InvisibleDept") = ""
            End If
            Return CType(ViewState.Item("InvisibleDept"), String)
        End Get
        Set(ByVal value As String)
            ViewState.Item("InvisibleDept") = value
        End Set
    End Property

    Public Sub setDeptID(ByVal strDeptID As String)
        If ddlOrgan.Items.Count = 0 Then
            getDept()
        End If
        If ddlOrgan.Items.Count > 0 Then
            ddlOrgan.SelectedIndex = ddlOrgan.Items.IndexOf(ddlOrgan.Items.FindByValue(strDeptID))
            getUser()
        End If
    End Sub

    Public Sub setUserID(ByVal strUserID As String)
        If ddlUser.Items.Count = 0 Then
            getUser()
        End If
        If ddlUser.Items.Count > 0 Then
            ddlUser.SelectedIndex = ddlUser.Items.IndexOf(ddlUser.Items.FindByValue(strUserID))
        End If
    End Sub

    '設定部門異動AO供客戶移轉用-抓取SC_User_Transfer
    Public Sub setTransferAO(ByVal strUserID As String)
        Dim strSQL As New StringBuilder

        strSQL.AppendLine("Select a.UserID, b.UserName, a.UserID + '-' + b.UserName as FullName")
        strSQL.AppendLine("From SC_User_Transfer a inner join SC_User b on a.UserID = b.UserID")
        strSQL.AppendLine("Where a.Status = '0'")
        strSQL.AppendLine("And a.DeptID = " & Bsp.Utility.Quote(SelectDeptID))
        strSQL.AppendLine("Order by a.UserID")

        Using dt As Data.DataTable = Bsp.DB.ExecuteDataSet(Data.CommandType.Text, strSQL.ToString()).Tables(0)
            With ddlUser
                .Items.Clear()
                .DataTextField = IIf(DisplayType = emDisplayType.Full, "FullName", "UserName")
                .DataValueField = "UserID"
                .DataSource = dt
                .DataBind()

                If Not MustSelect Then
                    .Items.Insert(0, New ListItem("---請選擇---", ""))
                End If
            End With
        End Using

        If ddlUser.Items.Count > 0 Then
            ddlUser.SelectedIndex = ddlUser.Items.IndexOf(ddlUser.Items.FindByValue(strUserID))
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            LoadData()
        End If
    End Sub

    Public Sub LoadData()
        If ddlOrgan.Items.Count = 0 Then getDept()
        If ddlUser.Items.Count = 0 Then getUser()
    End Sub

    Public Sub ReloadData()
        getDept()
        getUser()
    End Sub

    Public Sub setRegionDept(ByVal RegionID As emRegion)
        Dim strSQL As New StringBuilder

        strSQL.AppendLine("Select OrganID, OrganName, OrganID + '-' + OrganName FullName From SC_Organization")
        strSQL.AppendLine("Where InValidFlag = '0'")
        If RegionID = emRegion.C1 Then
            strSQL.AppendLine("And OrganID in (Select Code From SC_Common Where Type = '03')")
        ElseIf RegionID = emRegion.C2 Then
            strSQL.AppendLine("And OrganID in (Select Code From SC_Common Where Type = '04')")
        Else
            strSQL.AppendLine("And BusinessFlag = '1'")
        End If

        Using dt As Data.DataTable = Bsp.DB.ExecuteDataSet(Data.CommandType.Text, strSQL.ToString()).Tables(0)
            With ddlOrgan
                .DataSource = dt
                .DataTextField = IIf(DisplayType = emDisplayType.Full, "FullName", "OrganName")
                .DataValueField = "OrganID"
                .DataBind()
            End With
        End Using
    End Sub

    Private Sub getDept()
        Dim strWhere As String = "And InValidFlag = '0'"

        If FreezeDeptID = "" Then
            Select Case DeptType
                Case emDeptType.Bussiness

                Case emDeptType.Dept

                Case emDeptType.Custom  '依FreezeGroup決定顯示的部門
                    If FreezeGroup <> "" Then
                        Dim aryGroups() As String = FreezeGroup.Split(",")
                        Dim strGroups As String = ""

                        For intLoop As Integer = 0 To aryGroups.GetUpperBound(0)
                            strGroups &= ",'" & aryGroups(intLoop) & "'"
                        Next
                        strWhere &= vbCrLf & "And OrganID in (Select Distinct b.DeptID From SC_UserGroup a inner join SC_User b on a.UserID = b.UserID Where a.GroupID in (" & strGroups.Substring(1) & "))"
                    End If
            End Select
        Else
            Dim aryDeptID() As String = ViewState.Item("_FreezeDeptID").Split(",")
            Dim strIn As String = ""

            For intLoop As Integer = 0 To aryDeptID.GetUpperBound(0)
                If strIn <> "" Then strIn &= ","

                strIn &= "'" & aryDeptID(intLoop) & "'"
            Next
            strWhere &= vbCrLf & "And OrganID in (" & strIn & ")"
        End If
        If InvisibleDept <> "" Then
            strWhere &= vbCrLf & "And OrganID not in ('" & InvisibleDept.Replace(",", "','") & "')"
        End If
        strWhere &= vbCrLf & "Order by OrganID"

        Dim objSC As New SC()

        Try
            Using dt As Data.DataTable = objSC.GetOrganInfo("", "OrganID, OrganName, OrganID + '-' + OrganName as FullName", strWhere)
                With ddlOrgan
                    .DataSource = dt
                    .DataTextField = IIf(DisplayType = emDisplayType.Full, "FullName", "OrganName")
                    .DataValueField = "OrganID"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw
            'Bsp.Utility.ShowMessage(Me.Page, Bsp.Utility.getInnerException("ucOneUserSelect_1", ex))
            'Return
        End Try
    End Sub

    Private Sub getUser()
        If ddlOrgan.SelectedIndex < 0 Then Exit Sub

        Dim strWhere As String = "And BanMark = '0' and (DeptID = " & Bsp.Utility.Quote(ddlOrgan.SelectedValue) & " or OrganID = " & Bsp.Utility.Quote(ddlOrgan.SelectedValue) & ")"

        Select Case UserType
            Case emUserType.AO
                strWhere &= vbCrLf & "And UserID in (Select UserID From SC_UserGroup Where GroupID = '01')"
            Case emUserType.Custom
                If FreezeGroup <> "" Then
                    Dim aryGroups() As String = FreezeGroup.Split(",")
                    Dim strGroups As String = ""

                    For intLoop As Integer = 0 To aryGroups.GetUpperBound(0)
                        strGroups &= ",'" & aryGroups(intLoop) & "'"
                    Next
                    strWhere &= vbCrLf & "And UserID in (Select UserID From SC_UserGroup Where GroupID in (" & strGroups.Substring(1) & "))"
                End If
        End Select

        strWhere &= vbCrLf & "Order by UserID"

        Dim objSC As New SC()
        Try
            Using dt As Data.DataTable = objSC.GetUserInfo("", "UserID, UserName, UserID + '-' + UserName FullName", strWhere)
                With ddlUser
                    .Items.Clear()
                    .DataTextField = IIf(DisplayType = emDisplayType.Full, "FullName", "UserName")
                    .DataValueField = "UserID"
                    .DataSource = dt
                    .DataBind()

                    If Not MustSelect Then
                        .Items.Insert(0, New ListItem("---請選擇---", ""))
                    End If
                End With
            End Using

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub ddlOrgan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlOrgan.SelectedIndexChanged
        getUser()
    End Sub
End Class

