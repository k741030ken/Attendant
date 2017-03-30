'****************************************************
'功能說明：部門選取
'建立人員：Weicheng
'建立日期：2014.08.28
'****************************************************

Partial Class Component_ucSelectHROrgan
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

    'Beatrice Add
    '設定下拉選單CSS
    Public WriteOnly Property CssClass() As String
        Set(ByVal value As String)
            ddlDeptID.CssClass = value
            ddlOrganID.CssClass = value
        End Set
    End Property

    '20161108 Beatrice Add
    '是否撈取新增待異動組織(OrganizationWait)
    Public Property HasWait() As Boolean
        Get
            If ViewState.Item("_HasWait") Is Nothing Then
                ViewState.Item("_HasWait") = False
            End If
            Return ViewState.Item("_HasWait")
        End Get
        Set(ByVal value As Boolean)
            ViewState.Item("_HasWait") = value
        End Set
    End Property

    '20161108 Beatrice Add
    '待異動組織(OrganizationWait)生效日期
    Public Property ValidDate() As String
        Get
            If ViewState.Item("_ValidDate") Is Nothing Then
                ViewState.Item("_ValidDate") = ""
            End If
            Return ViewState.Item("_ValidDate")
        End Get
        Set(ByVal value As String)
            ViewState.Item("_ValidDate") = value
        End Set
    End Property

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
            If MustSelect And ddlDeptID.SelectedValue <> "" Then
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
            If MustSelect And ddlDeptID.SelectedValue <> "" Then
                If ddlDeptID.SelectedIndex >= 0 Then
                    If DisplayType = emDisplayType.Full Then
                        Return ddlDeptID.SelectedItem.Text.Substring(ddlDeptID.SelectedItem.Text.IndexOf("-") + 1)
                    Else
                        Return ddlDeptID.SelectedItem.Text
                    End If
                End If
            Else
                If ddlDeptID.SelectedIndex > 0 Then
                    If DisplayType = emDisplayType.Full Then
                        Return ddlDeptID.SelectedItem.Text.Substring(ddlDeptID.SelectedItem.Text.IndexOf("-") + 1)
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
                If MustSelect And ddlOrganID.SelectedValue <> "" Then
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
                If MustSelect And ddlOrganID.SelectedValue <> "" Then
                    If ddlOrganID.SelectedIndex >= 0 Then
                        If DisplayType = emDisplayType.Full Then
                            Return ddlOrganID.SelectedItem.Text.Substring(ddlOrganID.SelectedItem.Text.IndexOf("-") + 1)
                        Else
                            Return ddlOrganID.SelectedItem.Text
                        End If
                    End If
                Else
                    If ddlOrganID.SelectedIndex > 0 Then
                        If DisplayType = emDisplayType.Full Then
                            Return ddlOrganID.SelectedItem.Text.Substring(ddlOrganID.SelectedItem.Text.IndexOf("-") + 1)
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
    Public Sub setDeptID(ByVal strCompID As String, ByVal strDeptID As String, ByVal strOrganID As String, ByVal strShowInValidOrgan As String)
        'Beatrice Add
        If strCompID <> "" And strDeptID <> "" Then
            subLoadDeptData(strCompID, strShowInValidOrgan)
            Bsp.Utility.SetSelectedIndex(ddlDeptID, strDeptID)

            subLoadOrganData(strCompID, strShowInValidOrgan)
            Bsp.Utility.SetSelectedIndex(ddlOrganID, strOrganID)
        End If
        'Beatrice Marked
        'If strCompID <> "" And strDeptID <> "" Then
        '    If ddlDeptID.Items.Count = 0 Then subLoadDeptData(strCompID, strShowInValidOrgan)
        '    'ddlDeptID.SelectedIndex = ddlDeptID.Items.IndexOf(ddlDeptID.Items.FindByValue(strDeptID))
        '    Bsp.Utility.SetSelectedIndex(ddlDeptID, strDeptID)
        'End If
        'If strCompID <> "" And strOrganID <> "" Then
        '    If ddlDeptID.Items.Count = 0 Then subLoadOrganData(strCompID, strShowInValidOrgan)
        '    'ddlOrganID.SelectedIndex = ddlOrganID.Items.IndexOf(ddlOrganID.Items.FindByValue(strOrganID))
        '    Bsp.Utility.SetSelectedIndex(ddlOrganID, strOrganID)
        'End If
    End Sub

    '由外部來設定目前單位，若傳入為空值，則不處理
    Public Sub setDeptID(ByVal strCompID As String, ByVal strDeptID As String, ByVal strShowInValidOrgan As String)
        If strCompID <> "" AndAlso strDeptID <> "" Then
            If ddlDeptID.Items.Count = 0 Then subLoadDeptData(strCompID, strShowInValidOrgan)
            ddlDeptID.SelectedIndex = ddlDeptID.Items.IndexOf(ddlDeptID.Items.FindByValue(strDeptID))
        End If
    End Sub

    Public Sub setOrganID(ByVal strCompID As String, ByVal strOrganID As String, ByVal strShowInValidOrgan As String)
        If strCompID <> "" AndAlso strOrganID <> "" AndAlso ShowOrgan Then
            'If ddlOrganID.Items.Count = 0 OrElse (ddlOrganID.Items.Count = 1 And ddlOrganID.Items(0).Value = "") Then
            subLoadOrganData(strCompID, strShowInValidOrgan)
            'End If
            ddlOrganID.SelectedIndex = ddlOrganID.Items.IndexOf(ddlOrganID.Items.FindByValue(strOrganID))
        End If
    End Sub

    Private Sub subLoadDeptData(ByVal strCompID As String, ByVal strShowInValidOrgan As String)
        Dim strTableName As String = "Organization"
        Dim strField As String = ""
        Dim strWhere As String = ""
        Dim strIn As String = ""

        strWhere = "And CompID = " & Bsp.Utility.Quote(strCompID)
        strWhere &= vbCrLf & "And OrganID = DeptID"

        '2016/04/29 SPHBKC資料已併入Organization中
        'If strCompID <> "SPHBKC" Then
        '    strTableName = "Organization"
        'Else
        '    strTableName = "COrganization"
        'End If

        If strShowInValidOrgan = "N" Then
            strField = "OrganID, OrganName, OrganID + '-' + OrganName As FullName, InValidFlag, '1' As WaitStatus"
            strWhere &= vbCrLf & "And InValidFlag = '0'" '只顯示有效單位
        Else
            strField = "OrganID, OrganName + Case When InValidFlag = '0' Then '' Else '(無效)' End As OrganName, OrganID + '-' + OrganName + Case When InValidFlag = '0' Then '' Else '(無效)' End As FullName, InValidFlag, '1' As WaitStatus"
        End If

        If FreezeDeptID <> "" Then
            Dim aryDeptID() As String = FreezeDeptID.Split(",")

            For intLoop As Integer = 0 To aryDeptID.GetUpperBound(0)
                If strIn <> "" Then strIn &= ","

                strIn &= "'" & aryDeptID(intLoop) & "'"
            Next
            strWhere &= vbCrLf & "And OrganID In (" & strIn & ")"
        End If

        '20161108 增加待異動組織(OrganizationWait)判斷
        If HasWait Then
            strWhere &= vbCrLf & "UNION"
            strWhere &= vbCrLf & "Select OrganID, OrganName + '(未生效)' As OrganName, OrganID + '-' + OrganName + '(未生效)' As FullName, InValidFlag, WaitStatus"
            strWhere &= vbCrLf & "From OrganizationWait"
            strWhere &= vbCrLf & "Where OrganReason = '1' And WaitStatus = '0'"
            If ValidDate <> "" Then
                strWhere &= vbCrLf & "And ValidDate <= " & Bsp.Utility.Quote(ValidDate)
            End If
            strWhere &= vbCrLf & "And CompID = " & Bsp.Utility.Quote(strCompID)
            strWhere &= vbCrLf & "And OrganID = DeptID"

            If FreezeDeptID <> "" Then
                strWhere &= vbCrLf & "And OrganID In (" & strIn & ")"
            End If
        End If

        strWhere &= vbCrLf & "Order by WaitStatus, InValidFlag, OrganID"

        Dim objUC As New UC()

        Try
            Using dt As Data.DataTable = objUC.GetHROrganInfo(strTableName, "", strField, strWhere)
                With ddlDeptID
                    .DataSource = dt
                    .DataTextField = IIf(DisplayType = emDisplayType.Full, "FullName", "OrganName")
                    .DataValueField = "OrganID"
                    .DataBind()

                    If Not MustSelect Then
                        .Items.Insert(0, New ListItem("---請選擇---", ""))
                    End If
                End With

                If dt.Rows.Count = 0 Then
                    ddlDeptID.Items.Insert(0, New ListItem("---請選擇---", ""))
                End If
            End Using

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me.Parent, "ucSelectREOrgan.subLoadDeptData", ex)
            Throw
            Return
        End Try

    End Sub

    Private Sub subLoadOrganData(ByVal strCompID As String, ByVal strShowInValidOrgan As String)
        Dim strTableName As String = "Organization"
        Dim strField As String
        Dim strWhere As String

        If ddlDeptID.SelectedIndex < 0 Then
            ddlOrganID.Items.Clear()
            ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
            Return
        End If

        strWhere = "And CompID = " & Bsp.Utility.Quote(strCompID)

        '2016/04/29 SPHBKC資料已併入Organization中
        'If strCompID <> "SPHBKC" Then
        '    strTableName = "Organization"
        'Else
        '    strTableName = "COrganization"
        'End If

        If strShowInValidOrgan = "N" Then
            strField = "OrganID, OrganName, OrganID + '-' + OrganName As FullName, InValidFlag, '1' As WaitStatus"
            strWhere &= vbCrLf & "And InValidFlag = '0'" '只顯示有效單位
        Else
            strField = "OrganID, OrganName + Case When InValidFlag = '0' Then '' Else '(無效)' End As OrganName, OrganID + '-' + OrganName + Case When InValidFlag = '0' Then '' Else '(無效)' End As FullName, InValidFlag, '1' As WaitStatus"
        End If

        strWhere &= vbCrLf & "And DeptID = " & Bsp.Utility.Quote(ddlDeptID.SelectedValue)

        '20161108 增加待異動組織(OrganizationWait)判斷
        If HasWait Then
            strWhere &= vbCrLf & "UNION"
            strWhere &= vbCrLf & "Select OrganID, OrganName + '(未生效)' As OrganName, OrganID + '-' + OrganName + '(未生效)' As FullName, InValidFlag, WaitStatus"
            strWhere &= vbCrLf & "From OrganizationWait"
            strWhere &= vbCrLf & "Where OrganReason = '1' And WaitStatus = '0'"
            If ValidDate <> "" Then
                strWhere &= vbCrLf & "And ValidDate <= " & Bsp.Utility.Quote(ValidDate)
            End If
            strWhere &= vbCrLf & "And CompID = " & Bsp.Utility.Quote(strCompID)
            strWhere &= vbCrLf & "And DeptID = " & Bsp.Utility.Quote(ddlDeptID.SelectedValue)
        End If

        strWhere &= vbCrLf & "Order by WaitStatus, InValidFlag, OrganID"

        Dim objUC As New UC()
        Try
            Using dt As Data.DataTable = objUC.GetHROrganInfo(strTableName, "", strField, strWhere)
                With ddlOrganID
                    .DataSource = dt
                    .DataTextField = IIf(DisplayType = emDisplayType.Full, "FullName", "OrganName")
                    .DataValueField = "OrganID"
                    .DataBind()

                    .Items.Insert(0, New ListItem("---請選擇---", ""))
                End With
            End Using

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Bsp.Utility.getInnerException("ucSelectREOrgan.subLoadOrganData：", ex))
            Return
        End Try

    End Sub

    '' ''Private Sub subLoadGroupData(ByVal strCompID As String, ByVal strShowInValidOrgan As String)
    ' ''Private Sub subLoadGroupData(ByVal strCompID As String, ByVal strShowInValidGroupID As String)
    ' ''    Dim strField As String
    ' ''    Dim strWhere As String

    ' ''    If ddlOrganID.SelectedIndex < 0 Then Return
    ' ''    strWhere = "And OrganID = GroupID"

    ' ''    If strShowInValidGroupID = "N" Then
    ' ''        'strField = "GroupID, OrganName, OrganID + '-' + OrganName as FullName"
    ' ''        strField = "GroupID"
    ' ''        strWhere &= "And InValidFlag = '0'" '只顯示有效單位
    ' ''    End If

    ' ''    strWhere &= vbCrLf & "And OrganID = " & Bsp.Utility.Quote(ddlOrganID.SelectedValue)

    ' ''    'If ViewState.Item("_FreezeDeptID") = "" Then
    ' ''    '    strWhere &= vbCrLf & "And OrganID <> GroupID "
    ' ''    'End If
    ' ''    strWhere &= vbCrLf & "Order by GroupID"

    ' ''    Dim objRE As New RE()
    ' ''    Try
    ' ''        Using dt As Data.DataTable = objRE.GetREGroupID("", strField, strWhere)
    ' ''            ''Using dt As Data.DataTable = objRE.GetREGroupID(strShowInValidGroupID, "GroupID", strWhere)
    ' ''            With ddlOrganID
    ' ''                .DataSource = dt
    ' ''                '.DataTextField = IIf(DisplayType = emDisplayType.Full, "FullName", "OrganName")
    ' ''                .DataTextField = IIf(DisplayType = emDisplayType.Full, "", "")
    ' ''                .DataValueField = "GroupID"
    ' ''                .DataBind()
    ' ''            End With
    ' ''        End Using

    ' ''    Catch ex As Exception
    ' ''        Bsp.Utility.ShowMessage(Me, Bsp.Utility.getInnerException("ucSelectREOrgan.subLoadGroupData：", ex))
    ' ''        Return
    ' ''    End Try
    ' ''End Sub

    Public Sub LoadData(Optional ByVal strCompID As String = "", Optional ByVal strShowInValidOrgan As String = "N")
        ViewState.Item("strCompID") = strCompID
        ViewState.Item("strShowInValidOrgan") = strShowInValidOrgan
        subLoadDeptData(strCompID, strShowInValidOrgan)
        If ShowOrgan Then subLoadOrganData(strCompID, strShowInValidOrgan)
    End Sub

    Protected Sub ddlDeptID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDeptID.SelectedIndexChanged
        DeptIDSelectedIndexChanged(e)
        subLoadOrganData(ViewState.Item("strCompID"), ViewState.Item("strShowInValidOrgan"))
    End Sub

    '20131209
    'Protected Sub ddlOrganID_SelectedIndexChanged(ByVal objDDL As DropDownList, Optional ByVal CondStr As String = "") 'Handles ddlOrganID.SelectedIndexChanged
    ''subLoadGroupData(ViewState.Item("strCompID"), ViewState.Item("strShowInValidGroupID"))
    'OrganIDSelectedIndexChanged()
    'Dim objRE As New RE()
    'Try
    '    Using dt As Data.DataTable = objRE.GetREGroupID("", CondStr)
    '        ''Using dt As Data.DataTable = objRE.GetREGroupID(strShowInValidGroupID, "GroupID", strWhere)
    '        With ddlOrganID
    '            .DataSource = dt
    '            ''.DataTextField = IIf(DisplayType = emDisplayType.Full, "FullName", "OrganName")
    '            '.DataTextField = IIf(DisplayType = emDisplayType.Full, "", "")
    '            .DataTextField = "GroupID"
    '            .DataValueField = "GroupID"
    '            .DataBind()
    '        End With
    '    End Using

    'Catch ex As Exception
    '    Bsp.Utility.ShowMessage(Me, Bsp.Utility.getInnerException("ucSelectREOrgan.subLoadGroupData：", ex))
    '    Return
    'End Try

    'End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ShowOrgan Then
            ddlDeptID.AutoPostBack = True
            ddlOrganID.Visible = True
        Else
            ddlDeptID.AutoPostBack = False
            ddlOrganID.Visible = False
        End If
    End Sub

    Public Delegate Sub OrganIDSelectedIndexChangedHandler(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event ucSelectOrganIDSelectedIndexChangedHandler_SelectChange As OrganIDSelectedIndexChangedHandler

    Protected Overridable Sub OrganIDSelectedIndexChanged(ByVal e As System.EventArgs)
        RaiseEvent ucSelectOrganIDSelectedIndexChangedHandler_SelectChange(Me, e)
    End Sub

    Protected Sub ddlOrganID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlOrganID.SelectedIndexChanged
        OrganIDSelectedIndexChanged(e)
    End Sub

    Public Delegate Sub DeptIDSelectedIndexChangedHandler(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event ucSelectDeptIDSelectedIndexChangedHandler_SelectChange As DeptIDSelectedIndexChangedHandler

    Protected Overridable Sub DeptIDSelectedIndexChanged(ByVal e As System.EventArgs)
        RaiseEvent ucSelectDeptIDSelectedIndexChangedHandler_SelectChange(Me, e)
    End Sub

End Class
