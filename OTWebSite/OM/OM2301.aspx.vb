'****************************************************
'功能說明：歷任主管記錄-新增
'建立人員：Rebecca Yan
'建立日期：2015.10
'****************************************************
Imports System.Data

Partial Class OM_OM2301
    Inherits PageBase
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ucQueryBoss.ShowCompRole = "False"
        ucQueryBoss.InValidFlag = "N"
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)
        If ht.ContainsKey("txtCompID") Then
            txtCompID.Text = UserProfile.SelectCompRoleName
            hidCompID.Value = UserProfile.SelectCompRoleID
            txtOrganID.Text = ht("txtOrganName").ToString()
            hidOrganType.Value = ht("hidOrganType").ToString() '隱藏欄位(組織類型)
            hidOrganID.Value = ht("hidOrganID").ToString()
            GetSelectData()
            
        Else
            Return
        End If
    End Sub

    Private Sub GetSelectData()
        'Dim objOM2 As OM2()
        '部門主管-公司代碼ddlBossCompID
        OM2.FillDDL(ddlBossCompID, " Company ", " CompID  ", " CompName ", OM2.DisplayType.Full, "", "", " Order By CompID ")
        ddlBossCompID.Items.Insert(0, New ListItem("---請選擇---", ""))
    End Sub

    '主管公司代碼-連動-主管員工編號uc
    Protected Sub ddlBossCompID_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlBossCompID.SelectedIndexChanged
        ucQueryBoss.SelectCompID = ddlBossCompID.SelectedValue
        txtBoss.Text = ""
        lblBossName.Text = ""
    End Sub

    Protected Sub txtBoss_TextChanged(sender As Object, e As System.EventArgs) Handles txtBoss.TextChanged
        If txtBoss.Text <> "" Then
            Dim objHR As New HR
            Dim rtnTable As DataTable = objHR.GetHREmpName(UserProfile.SelectCompRoleID, txtBoss.Text)
            If rtnTable.Rows.Count <= 0 Then
                lblBossName.Text = ""
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", "人事資料尚未建檔")
            Else
                lblBossName.Text = rtnTable.Rows(0).Item(0)
            End If
        Else
            lblBossName.Text = ""
        End If
    End Sub

#Region "uc-Case DoModalReturn"
    Public Overrides Sub DoModalReturn(ByVal returnValue As String)
        If returnValue <> "" Then
            Dim aryData() As String = returnValue.Split(":")
            Select Case aryData(0)
                Case "ucQueryBoss" '部門主管
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    ddlBossCompID.SelectedValue = aryValue(3)
                    txtBoss.Text = aryValue(1)
                    lblBossName.Text = aryValue(2)
            End Select
        End If
    End Sub
#End Region

#Region "funCheckData"
    Private Function funCheckData() As Boolean
        Dim objOM2 As New OM2()

        Dim strWhere As String = ""
        '/*********************歷任主管記錄新增*********************/

        '主管公司代碼
        If ddlBossCompID.SelectedValue.Trim = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "主管公司代碼")
            ddlBossCompID.Focus()
            Return False
        End If

        '主管員工編號
        If txtBoss.Text.Trim = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "主管員工編號")
            ddlBossCompID.Focus()
            Return False
        End If

        If txtBoss.Text <> "" Then
            Dim objHR As New HR
            Dim rtnTable As DataTable = objHR.GetHREmpName(ddlBossCompID.SelectedValue.Trim, txtBoss.Text)
            If rtnTable.Rows.Count <= 0 Then
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", "部門主管員編不存在")
                Return False
            End If
        End If

        '主管任用方式
        If ddlBossType.SelectedValue = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "主管任用方式")
            ddlBossCompID.Focus()
            Return False
        End If

        '生效起訖日
        If txtValidDateBH.DateText = "" Or txtValidDateBH.DateText = "____/__/__" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "生效起訖日(起)")
            ddlBossCompID.Focus()
            Return False
        End If

        If txtValidDateEH.DateText = "" Or txtValidDateEH.DateText = "____/__/__" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "生效起訖日(訖)")
            ddlBossCompID.Focus()
            Return False
        End If

        If txtValidDateBH.DateText > txtValidDateEH.DateText Then
            Bsp.Utility.ShowMessage(Me, "生效起日不可晚於訖日")
            ddlBossCompID.Focus()
            Return False
        End If

        If txtValidDateBH.DateText = txtValidDateEH.DateText Then
            Bsp.Utility.ShowMessage(Me, "生效起迄日不可重疊")
            txtValidDateEH.Focus()
            Return False
        End If

        '檢查起訖日區間不得重疊
        Return funCheckValidDate(hidCompID.Value, hidOrganID.Value, txtValidDateBH.DateText, txtValidDateEH.DateText)

        Return True

    End Function

#End Region
    '檢查起訖區間是否由重疊
    Private Function funCheckValidDate(ByVal hidCompID As String, ByVal hidOrganID As String, ByVal txtValidDateB As String, ByVal txtValidDateE As String) As Boolean
        Dim objOM As New OM2()

        Dim strSQL As New StringBuilder()
        'Dim strOrganType As String = ViewState.Item("OrganType")
        Try
            If hidOrganType.Value = "1-行政組織" Then
                If objOM.OM2301CheckBossLogValidDate(hidCompID, hidOrganID, txtValidDateB.ToString(), txtValidDateE.ToString()) Then
                    Bsp.Utility.ShowMessage(Me, "生效日期區間不可重疊")
                    Return False
                End If
            ElseIf hidOrganType.Value = "2-功能組織" Then
                If objOM.OM2301CheckFlowBossLogValidDate(hidCompID, hidOrganID, txtValidDateB.ToString(), txtValidDateE.ToString()) Then
                    Bsp.Utility.ShowMessage(Me, "生效日期區間不可重疊")
                    Return False
                End If
            End If
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & "funCheckValidDate", ex)
        End Try

        Return True
    End Function
#Region "視窗按鈕 DoAction"
    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnAdd"       '存檔返回
                If funCheckData() Then
                    If SaveData() Then
                        GoBack()
                    End If
                End If
            Case "btnActionX"     '返回
                GoBack()
            Case "btnCancel"   '清除
                ClearData()
        End Select
    End Sub

    Private Sub ClearData()
        ddlBossCompID.SelectedValue = ""
        txtBoss.Text = ""
        lblBossName.Text = ""
        ddlBossType.SelectedValue = ""
        txtValidDateBH.DateText = ""
        txtValidDateEH.DateText = ""
    End Sub

    Private Sub GoBack()
        Dim ti As TransferInfo = Me.StateTransfer
        Me.TransferFramePage(ti.CallerUrl, Nothing, ti.Args)
    End Sub

#End Region

    Private Function SaveData() As Boolean
        Dim beOrganizationBossLog As New beOrganizationBossLog.Row()
        Dim bsOrganizationBossLog As New beOrganizationBossLog.Service()

        Dim beOrganizationFlowBossLog As New beOrganizationFlowBossLog.Row()
        Dim bsOrganizationFlowBossLog As New beOrganizationFlowBossLog.Service()

        Dim objOM2 As New OM2()

        If hidOrganType.Value = "1-行政組織" Then
            '取得輸入資料
            beOrganizationBossLog.CompID.Value = UserProfile.SelectCompRoleID
            beOrganizationBossLog.OrganID.Value = hidOrganID.Value
            beOrganizationBossLog.BossCompID.Value = ddlBossCompID.SelectedValue
            beOrganizationBossLog.Boss.Value = txtBoss.Text
            beOrganizationBossLog.BossType.Value = ddlBossType.SelectedValue
            beOrganizationBossLog.ValidDateBH.Value = txtValidDateBH.DateText
            beOrganizationBossLog.ValidDateEH.Value = txtValidDateEH.DateText

            beOrganizationBossLog.LastChgComp.Value = UserProfile.ActCompID
            beOrganizationBossLog.LastChgID.Value = UserProfile.ActUserID
            beOrganizationBossLog.LastChgDate.Value = Now

            '檢查資料是否存在
            If bsOrganizationBossLog.IsDataExists(beOrganizationBossLog) Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00010", "")
                Return False
            End If

            '儲存資料
            Try
                Return objOM2.OrganizationBossLogAdd(beOrganizationBossLog)
            Catch ex As Exception
                Bsp.Utility.ShowMessage(Me, "[CC0201_99]" & Bsp.Utility.getMessage("E_00000") & Bsp.Utility.getInnerException(Me.FunID, ex))
                Return False
            End Try
        ElseIf hidOrganType.Value = "2-功能組織" Then
            '取得輸入資料
            beOrganizationFlowBossLog.CompID.Value = UserProfile.SelectCompRoleID
            beOrganizationFlowBossLog.OrganID.Value = hidOrganID.Value
            beOrganizationFlowBossLog.BossCompID.Value = ddlBossCompID.SelectedValue
            beOrganizationFlowBossLog.Boss.Value = txtBoss.Text
            beOrganizationFlowBossLog.BossType.Value = ddlBossType.SelectedValue
            beOrganizationFlowBossLog.ValidDateBH.Value = txtValidDateBH.DateText
            beOrganizationFlowBossLog.ValidDateEH.Value = txtValidDateEH.DateText

            beOrganizationFlowBossLog.LastChgComp.Value = UserProfile.ActCompID
            beOrganizationFlowBossLog.LastChgID.Value = UserProfile.ActUserID
            beOrganizationFlowBossLog.LastChgDate.Value = Now

            '檢查資料是否存在
            If bsOrganizationFlowBossLog.IsDataExists(beOrganizationFlowBossLog) Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00010", "")
                Return False
            End If

            '儲存資料
            Try
                Return objOM2.OrganizationFlowBossLogAdd(beOrganizationFlowBossLog)
            Catch ex As Exception
                Bsp.Utility.ShowMessage(Me, "[CC0201_99]" & Bsp.Utility.getMessage("E_00000") & Bsp.Utility.getInnerException(Me.FunID, ex))
                Return False
            End Try
        End If
        Return False
    End Function

End Class
