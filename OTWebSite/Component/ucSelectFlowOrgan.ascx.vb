'****************************************************
'功能說明：最小單位
'建立人員：Micky Sung
'建立日期：2015.06.11
'****************************************************

Partial Class Component_ucSelectFlowOrgan
    Inherits System.Web.UI.UserControl

    'DisplayType
    Public Enum emDisplayType
        OnlyName    '只顯示名字
        Full        '顯示ID + 名字
    End Enum

    '20161108 Beatrice Add
    '是否撈取新增待異動組織(OrganizationWait)
    Public Property IsWait() As Boolean
        Get
            If ViewState.Item("_IsWait") Is Nothing Then
                ViewState.Item("_IsWait") = False
            End If
            Return ViewState.Item("_IsWait")
        End Get
        Set(ByVal value As Boolean)
            ViewState.Item("_IsWait") = value
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

    ''是否一定選取，決定是否有請選擇字樣－僅對Dept有影響
    'Public Property MustSelect() As Boolean
    '    Get
    '        If ViewState.Item("_MustSelect") Is Nothing Then
    '            ViewState.Item("_MustSelect") = True
    '        End If
    '        Return ViewState.Item("_MustSelect")
    '    End Get
    '    Set(ByVal value As Boolean)
    '        ViewState.Item("_MustSelect") = value
    '    End Set
    'End Property

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

    '選取部門代號
    Public ReadOnly Property SelectedOrganID() As String
        Get
            If ddlOrganID.SelectedIndex > 0 Then
                Return ddlOrganID.SelectedValue
            End If
            Return ""
        End Get
    End Property

    '選取部門名稱
    Public ReadOnly Property SelectedOrganIDName() As String
        Get
            If ddlOrganID.SelectedIndex > 0 Then
                If DisplayType = emDisplayType.Full Then
                    Return ddlOrganID.SelectedItem.Text.Substring(ddlOrganID.SelectedItem.Text.IndexOf(" ") + 1)
                Else
                    Return ddlOrganID.SelectedItem.Text
                End If
            End If
            Return ""
        End Get
    End Property

    '如果只有一個選項，預設選它
    Public Sub SetDefaultOrgan()
        If ddlOrganID.Items.Count = 2 Then
            ddlOrganID.SelectedIndex = 1
        End If
    End Sub

    '20161110 Beatrice Add
    Public Property Enabled() As Boolean
        Get
            Return ddlOrganID.Enabled
        End Get
        Set(ByVal value As Boolean)
            ddlOrganID.Enabled = value
        End Set
    End Property

    '由外部來設定目前單位，若傳入為空值，則不處理
    Public Sub setOrganID(ByVal strOrganID As String, ByVal strShowInValidOrgan As String)
        If strOrganID <> "" Then
            If ddlOrganID.Items.Count = 0 Then subLoadOrganIDData(strOrganID, strShowInValidOrgan)
            Bsp.Utility.SetSelectedIndex(ddlOrganID, strOrganID)
        End If
    End Sub

    Private Sub subLoadOrganIDData(ByVal strOrganID As String, ByVal strShowInValidOrgan As String)
        Dim objUC As New UC()
        Dim strTableName As String
        Dim strField As String
        Dim strFlowOrganWait As String
        Dim strFlowOrgan As String
        Dim strWhere As String
        Dim intCnt As Integer

        strField = "OrganID, OrganName, FlowOrganID, InValidFlag"
        strFlowOrgan = ", OrganID + ' ' + OrganName + Case When InValidFlag = '1' Then '(無效)' Else '' End As FullOrganName"
        strWhere = "And VirtualFlag = '0'" '2016/02/01 有效無效都要出現
        If strShowInValidOrgan = "N" Then
            strWhere &= vbCrLf & "And InValidFlag = '0'" '只顯示有效單位
        End If

        '20161108 增加待異動組織(OrganizationWait)判斷
        If IsWait Then
            strFlowOrganWait = ", OrganID + ' ' + OrganName + Case When InValidFlag = '1' Then '(無效)' When FlowOrganID = '' Then '(未生效)' Else '' End As FullOrganName"
            strTableName = "OrganizationWait"
            strWhere &= vbCrLf & "And OrganReason = '1' And WaitStatus = '0' And OrganType In ('2', '3')"

            If ValidDate <> "" Then
                strWhere &= vbCrLf & "And ValidDate <= " & Bsp.Utility.Quote(ValidDate)
            End If
        Else
            strFlowOrganWait = strFlowOrgan
            strTableName = "OrganizationFlow"
        End If

        Try
            ddlOrganID.Items.Clear()
            If strOrganID <> "" Then
                Using dt As Data.DataTable = objUC.GetHROrganInfo(strTableName, strOrganID, strField + strFlowOrganWait, strWhere)
                    If dt.Rows.Count > 0 Then
                        If dt.Rows.Item(0)("FlowOrganID").ToString.Trim = "" Then
                            With ddlOrganID
                                .DataSource = dt
                                .DataTextField = "FullOrganName"
                                .DataValueField = "OrganID"
                                .DataBind()
                            End With
                        Else
                            Dim aryValue() As String = dt.Rows(0)("FlowOrganID").ToString().Trim.Split("|")
                            For intCnt = 0 To UBound(aryValue)
                                If intCnt = 0 Then
                                    strWhere = Bsp.Utility.Quote(aryValue(intCnt).ToString().Trim)
                                Else
                                    strWhere = strWhere & "," & Bsp.Utility.Quote(aryValue(intCnt).ToString().Trim)
                                End If
                            Next
                            strWhere = "AND OrganID In (" & strWhere & ")"
                            If strShowInValidOrgan = "N" Then
                                strWhere &= vbCrLf & "And InValidFlag = '0'" '只顯示有效單位
                            End If
                        End If
                    End If
                End Using

                If intCnt > 0 Then
                    Using dt As Data.DataTable = objUC.GetHROrganInfo("OrganizationFlow", "", strField + strFlowOrgan, strWhere)
                        If dt.Rows.Count > 0 Then
                            With ddlOrganID
                                .DataSource = dt
                                .DataTextField = "FullOrganName"
                                .DataValueField = "OrganID"
                                .DataBind()
                            End With
                        End If
                    End Using
                End If
            End If
            ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me.Parent, "ucSelectREOrgan.subLoadDeptData", ex)
            Throw
            Return
        End Try

    End Sub

    Public Sub LoadData(Optional ByVal strOrganID As String = "", Optional ByVal strShowInValidOrgan As String = "")
        subLoadOrganIDData(strOrganID, strShowInValidOrgan)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Delegate Sub OrganIDSelectedIndexChangedHandler(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event ucSelectOrganIDSelectedIndexChangedHandler_SelectChange As OrganIDSelectedIndexChangedHandler

    Protected Overridable Sub OrganIDSelectedIndexChanged(ByVal e As System.EventArgs)
        RaiseEvent ucSelectOrganIDSelectedIndexChangedHandler_SelectChange(Me, e)
    End Sub

    Protected Sub ddlOrganID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlOrganID.SelectedIndexChanged
        OrganIDSelectedIndexChanged(e)
    End Sub

End Class
