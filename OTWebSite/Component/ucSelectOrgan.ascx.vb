'****************************************************
'功能說明：部門選取
'建立人員：A02976
'建立日期：2007.03.20
'****************************************************

Partial Class Component_ucSelectOrgan
    Inherits System.Web.UI.UserControl

    'DisplayType
    Public Enum emDisplayType
        OnlyName    '只顯示名字
        Full        '顯示ID + 名字
    End Enum

    '顯示部門類型
    Public Enum emDeptType
        All
        Bussiness
    End Enum

    '是否一定選取，決定是否有請選擇字樣－僅對Dept有影響
    Public Property MustSelect() As Boolean
        Get
            If ViewState.Item("_MustSelect") Is Nothing Then
                ViewState.Item("_MustSelect") = True
            End If
            Return ViewState.Item("_MustSelect")
        End Get
        Set(ByVal value As Boolean)
            ViewState.Item("_MustSelect") = value
        End Set
    End Property

    '顯示部門類型-只對DeptID有效
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
    '鎖定選取部門
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

    '是否顯示科組課
    Public Property ShowOrgan() As Boolean
        Get
            If ViewState.Item("_ShowOrgan") Is Nothing Then
                ViewState.Item("_ShowOrgan") = True
            End If
            Return ViewState.Item("_ShowOrgan")
        End Get
        Set(ByVal value As Boolean)
            ViewState.Item("_ShowOrgan") = value
        End Set
    End Property

    Public Property Enabled() As Boolean
        Get
            Return ddlDeptID.Enabled
        End Get
        Set(ByVal value As Boolean)
            ddlDeptID.Enabled = value
            ddlOrganID.Enabled = value
        End Set
    End Property

    '選取部門代號
    Public ReadOnly Property SelectedDeptID() As String
        Get
            If MustSelect Then
                If ddlDeptID.SelectedIndex >= 0 Then
                    Return ddlDeptID.SelectedValue
                End If
            Else
                If ddlDeptID.SelectedIndex > 0 Then
                    Return ddlDeptID.SelectedValue
                End If
            End If
            Return ""
        End Get
    End Property

    '選取部門名稱
    Public ReadOnly Property SelectedDeptName() As String
        Get
            If MustSelect Then
                If ddlDeptID.SelectedIndex >= 0 Then
                    If DisplayType = emDisplayType.Full Then
                        Return ddlDeptID.SelectedItem.Text.Split("-")(1)
                    Else
                        Return ddlDeptID.SelectedItem.Text
                    End If
                End If
            Else
                If ddlDeptID.SelectedIndex > 0 Then
                    If DisplayType = emDisplayType.Full Then
                        Return ddlDeptID.SelectedItem.Text.Split("-")(1)
                    Else
                        Return ddlDeptID.SelectedItem.Text
                    End If
                End If
            End If
            Return ""
        End Get
    End Property

    '選取科組課
    Public ReadOnly Property SelectedOrganID() As String
        Get
            If ShowOrgan Then
                If MustSelect Then
                    If ddlOrganID.SelectedIndex >= 0 Then
                        Return ddlOrganID.SelectedValue
                    End If
                Else
                    If ddlOrganID.SelectedIndex > 0 Then
                        Return ddlOrganID.SelectedValue
                    End If
                End If
            End If
            Return ""
        End Get
    End Property

    '選取科組課名稱
    Public ReadOnly Property SelectedOrganName() As String
        Get
            If ShowOrgan Then
                If MustSelect Then
                    If ddlOrganID.SelectedIndex >= 0 Then
                        If DisplayType = emDisplayType.Full Then
                            Return ddlOrganID.SelectedItem.Text.Split("-")(1)
                        Else
                            Return ddlOrganID.SelectedItem.Text
                        End If
                    End If
                Else
                    If ddlOrganID.SelectedIndex > 0 Then
                        If DisplayType = emDisplayType.Full Then
                            Return ddlOrganID.SelectedItem.Text.Split("-")(1)
                        Else
                            Return ddlOrganID.SelectedItem.Text
                        End If
                    End If
                End If
            End If
            Return ""
        End Get
    End Property

    '由外部來設定目前單位，若傳入為空值，則不處理
    Public Sub setDeptID(ByVal strDeptID As String, ByVal strOrganID As String)
        If strDeptID <> "" Then
            If ddlDeptID.Items.Count = 0 Then subLoadDeptData()
            ddlDeptID.SelectedIndex = ddlDeptID.Items.IndexOf(ddlDeptID.Items.FindByValue(strDeptID))
        End If
        If strOrganID <> "" Then
            If ddlDeptID.Items.Count = 0 Then subLoadOrganData()
            ddlOrganID.SelectedIndex = ddlOrganID.Items.IndexOf(ddlOrganID.Items.FindByValue(strOrganID))
        End If
    End Sub

    '由外部來設定目前單位，若傳入為空值，則不處理
    Public Sub setDeptID(ByVal strDeptID As String)
        If strDeptID <> "" Then
            If ddlDeptID.Items.Count = 0 Then subLoadDeptData()
            ddlDeptID.SelectedIndex = ddlDeptID.Items.IndexOf(ddlDeptID.Items.FindByValue(strDeptID))
        End If
    End Sub

    Public Sub setOrganID(ByVal strOrganID As String)
        If strOrganID <> "" AndAlso ShowOrgan Then
            If ddlOrganID.Items.Count = 0 OrElse (ddlOrganID.Items.Count = 1 And ddlOrganID.Items(0).Value = "") Then
                subLoadOrganData()
            End If
            ddlOrganID.SelectedIndex = ddlOrganID.Items.IndexOf(ddlOrganID.Items.FindByValue(strOrganID))
        End If
    End Sub

    Private Sub subLoadDeptData()
        Dim strWhere As String = "And InValidFlag = '0' "

        If FreezeDeptID = "" Then
           
        Else
            Dim aryDeptID() As String = FreezeDeptID.Split(",")
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
            Using dt As Data.DataTable = objSC.GetOrganInfo("", "OrganID, OrganName, OrganID + '-' + OrganName as FullName", strWhere)
                With ddlDeptID
                    .DataSource = dt
                    .DataTextField = IIf(DisplayType = emDisplayType.Full, "FullName", "OrganName")
                    .DataValueField = "OrganID"
                    .DataBind()

                    If Not MustSelect Then
                        .Items.Insert(0, New ListItem("---請選擇---", ""))
                    End If
                End With
            End Using

        Catch ex As Exception
            'Bsp.Utility.ShowMessage(Me.Parent, "ucSelectOrgan.subLoadDeptData", ex)
            Throw
            Return
        End Try
    End Sub

    Private Sub subLoadOrganData()
        Dim strWhere As String

        If ddlDeptID.SelectedIndex < 0 Then Return

        strWhere = "And InValidFlag = '0'"
        strWhere &= vbCrLf & "And DeptID = " & Bsp.Utility.Quote(ddlDeptID.SelectedValue)

        If ViewState.Item("_FreezeDeptID") = "" Then
            strWhere &= vbCrLf & "And OrganType = '2'"
        End If
        strWhere &= vbCrLf & "Order by OrganID"

        Dim objSC As New SC()
        Try
            Using dt As Data.DataTable = objSC.GetOrganInfo("", "OrganID, OrganName, OrganID + '-' + OrganName as FullName", strWhere)
                With ddlOrganID
                    .DataSource = dt
                    .DataTextField = IIf(DisplayType = emDisplayType.Full, "FullName", "OrganName")
                    .DataValueField = "OrganID"
                    .DataBind()

                    .Items.Insert(0, New ListItem("---請選擇---", ""))
                End With
            End Using

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Bsp.Utility.getInnerException("ucSelectOrgan_2：", ex))
            Return
        End Try
    End Sub

    Public Sub LoadData()
        subLoadDeptData()
        If ShowOrgan Then subLoadOrganData()
    End Sub

    Protected Sub ddlDeptID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDeptID.SelectedIndexChanged
        subLoadOrganData()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ShowOrgan Then
            ddlDeptID.AutoPostBack = True
            ddlOrganID.Visible = True
        Else
            ddlDeptID.AutoPostBack = False
            ddlOrganID.Visible = False
        End If
    End Sub
End Class
