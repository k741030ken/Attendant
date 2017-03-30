Imports System.Data

Partial Class OM_OM2302
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ucQueryBoss.ShowCompRole = "False"
        ucQueryBoss.InValidFlag = "N"
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)
        If ht.ContainsKey("SelectedCompID") Then
            txtCompID.Text = ht("SelectedCompID").ToString() + "-" + ht("SelectedCompName").ToString()
            ViewState.Item("CompID") = ht("SelectedCompID").ToString()
            ViewState.Item("OrganID") = ht("SelectedOrganID").ToString()
            ViewState.Item("OrganType") = ht("SelectedOrganType").ToString()
            ViewState.Item("ValidDateBH") = ht("ValidDateBH").ToString()
            
            hidCompID.Value = UserProfile.SelectCompRoleID
            txtOrganID.Text = ht("txtOrganName").ToString()
            hidOrganType.Value = ht("hidOrganType").ToString() '隱藏欄位(組織類型)
            hidOrganID.Value = ht("hidOrganID").ToString()
            hidBossCompID.Value = ht("BossCompID").ToString()
            hidBoss.Value = ht("Boss").ToString()
            hidBossName.Value = ht("BossName").ToString()
            hidBossType.Value = ht("BossType").ToString()
            hidValidDateBH.Value = ht("ValidDateBH").ToString()
            hidValidDateEH.Value = ht("ValidDateEH").ToString()
            GetSelectData()

            subGetData(ViewState.Item("CompID"), ViewState.Item("OrganID"), ViewState.Item("ValidDateBH"), ViewState.Item("OrganType"))
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

    Private Sub subGetData(ByVal CompID As String, ByVal OrganID As String, ByVal ValidDateBH As String, ByVal OrganType As String)
        Dim objSC As New SC
        Dim beOrganizationBossLog As New beOrganizationBossLog.Row()
        Dim bsOrganizationBossLog As New beOrganizationBossLog.Service()

        Dim beOrganizationFlowBossLog As New beOrganizationFlowBossLog.Row()
        Dim bsOrganizationFlowBossLog As New beOrganizationFlowBossLog.Service()

        Dim objOM2 As New OM2()

        If OrganType = "1-行政組織" Then
            '取得輸入資料
            beOrganizationBossLog.CompID.Value = CompID
            beOrganizationBossLog.OrganID.Value = OrganID
            beOrganizationBossLog.ValidDateBH.Value = ValidDateBH

            Try
                Using dt As DataTable = bsOrganizationBossLog.QueryByKey(beOrganizationBossLog).Tables(0)
                    If dt.Rows.Count <= 0 Then Exit Sub
                    beOrganizationBossLog = New beOrganizationBossLog.Row(dt.Rows(0))

                    ddlBossCompID.SelectedValue = beOrganizationBossLog.BossCompID.Value
                    txtBoss.Text = beOrganizationBossLog.Boss.Value

                    Dim BossName As String = objSC.GetSC_UserName(beOrganizationBossLog.BossCompID.Value, beOrganizationBossLog.Boss.Value)
                    lblBossName.Text = IIf(BossName <> "", BossName, "")

                    ddlBossType.SelectedValue = beOrganizationBossLog.BossType.Value
                    txtValidDateBH.DateText = Format(Date.Parse(beOrganizationBossLog.ValidDateBH.Value.ToString), "yyyy/MM/dd")
                    txtValidDateEH.DateText = Format(Date.Parse(beOrganizationBossLog.ValidDateEH.Value.ToString), "yyyy/MM/dd")

                    '最後異動公司
                    Dim CompName As String = objSC.GetSC_CompName(beOrganizationBossLog.LastChgComp.Value)
                    txtLastChgComp.Text = beOrganizationBossLog.LastChgComp.Value + IIf(CompName <> "", "-" + CompName, "")
                    '最後異動人員
                    Dim UserName As String = objSC.GetSC_UserName(beOrganizationBossLog.LastChgComp.Value, beOrganizationBossLog.LastChgID.Value)
                    txtLastChgID.Text = beOrganizationBossLog.LastChgID.Value + IIf(UserName <> "", "-" + UserName, "")
                    '最後異動日期
                    Dim boolDate As Boolean = Format(beOrganizationBossLog.LastChgDate.Value, "yyyy/MM/dd") = "1900/01/01"
                    txtLastChgDate.Text = IIf(boolDate, "", beOrganizationBossLog.LastChgDate.Value.ToString("yyyy/MM/dd HH:mm:ss"))
                End Using
            Catch ex As Exception
                Bsp.Utility.ShowMessage(Me, Me.FunID & "subGetData", ex)
            End Try

        ElseIf OrganType = "2-功能組織" Then
            beOrganizationFlowBossLog.OrganID.Value = OrganID
            beOrganizationFlowBossLog.ValidDateBH.Value = ValidDateBH

            Try
                Using dt As DataTable = bsOrganizationFlowBossLog.QueryByKey(beOrganizationFlowBossLog).Tables(0)
                    If dt.Rows.Count <= 0 Then Exit Sub
                    beOrganizationFlowBossLog = New beOrganizationFlowBossLog.Row(dt.Rows(0))

                    ddlBossCompID.SelectedValue = beOrganizationFlowBossLog.BossCompID.Value
                    txtBoss.Text = beOrganizationFlowBossLog.Boss.Value

                    Dim BossName As String = objSC.GetSC_UserName(beOrganizationFlowBossLog.BossCompID.Value, beOrganizationFlowBossLog.Boss.Value)
                    lblBossName.Text = IIf(BossName <> "", BossName, "")

                    ddlBossType.SelectedValue = beOrganizationFlowBossLog.BossType.Value
                    txtValidDateBH.DateText = Format(Date.Parse(beOrganizationFlowBossLog.ValidDateBH.Value.ToString), "yyyy/MM/dd")
                    txtValidDateEH.DateText = Format(Date.Parse(beOrganizationFlowBossLog.ValidDateEH.Value.ToString), "yyyy/MM/dd")

                    '最後異動公司
                    Dim CompName As String = objSC.GetSC_CompName(beOrganizationFlowBossLog.LastChgComp.Value)
                    txtLastChgComp.Text = beOrganizationFlowBossLog.LastChgComp.Value + IIf(CompName <> "", "-" + CompName, "")
                    '最後異動人員
                    Dim UserName As String = objSC.GetSC_UserName(beOrganizationFlowBossLog.LastChgComp.Value, beOrganizationFlowBossLog.LastChgID.Value)
                    txtLastChgID.Text = beOrganizationFlowBossLog.LastChgID.Value + IIf(UserName <> "", "-" + UserName, "")
                    '最後異動日期
                    Dim boolDate As Boolean = Format(beOrganizationFlowBossLog.LastChgDate.Value, "yyyy/MM/dd") = "1900/01/01"
                    txtLastChgDate.Text = IIf(boolDate, "", beOrganizationFlowBossLog.LastChgDate.Value.ToString("yyyy/MM/dd HH:mm:ss"))
                End Using
            Catch ex As Exception
                Bsp.Utility.ShowMessage(Me, Me.FunID & "subGetData", ex)
            End Try
        End If
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
        '/*********************歷任主管記錄修改*********************/

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
            ddlBossType.Focus()
            Return False
        End If

        '生效起迄日
        If txtValidDateBH.DateText = "" Or txtValidDateBH.DateText = "____/__/__" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "生效起迄日(起)")
            txtValidDateBH.Focus()
            Return False
        End If

        If txtValidDateEH.DateText = "" Or txtValidDateEH.DateText = "____/__/__" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "生效起迄日(迄)")
            txtValidDateEH.Focus()
            Return False
        End If

        If txtValidDateBH.DateText > txtValidDateEH.DateText Then
            Bsp.Utility.ShowMessage(Me, "生效起日不可晚於迄日")
            txtValidDateBH.Focus()
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
                If objOM.OM2302CheckBossLogValidDate(hidCompID, hidOrganID, txtValidDateB.ToString(), txtValidDateE.ToString(), hidValidDateBH.Value, hidValidDateEH.Value) Then
                    Bsp.Utility.ShowMessage(Me, "生效日期區間不可重疊")
                    Return False
                End If
            ElseIf hidOrganType.Value = "2-功能組織" Then
                If objOM.OM2302CheckFlowBossLogValidDate(hidCompID, hidOrganID, txtValidDateB.ToString(), txtValidDateE.ToString(), hidValidDateBH.Value, hidValidDateEH.Value) Then
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
            Case "btnUpdate"       '存檔返回
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
        ddlBossCompID.SelectedValue = hidBossCompID.Value
        txtBoss.Text = hidBoss.Value
        lblBossName.Text = hidBossName.Value
        ddlBossType.SelectedValue = hidBossType.Value
        txtValidDateBH.DateText = hidValidDateBH.Value
        txtValidDateEH.DateText = hidValidDateEH.Value
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

        If ViewState.Item("OrganType") = "1-行政組織" Then
            '取得輸入資料
            Dim txtBossCompID As String = ddlBossCompID.SelectedValue
            Dim txtBossType As String = ddlBossType.SelectedValue

            txtLastChgComp.Text = UserProfile.ActCompID
            txtLastChgID.Text = UserProfile.ActUserID
            txtLastChgDate.Text = Now

            '儲存資料
            Try
                Return objOM2.OrganizationBossLogUpdate(hidCompID.Value, hidOrganID.Value, txtBossCompID, txtBoss.Text, txtBossType, hidValidDateBH.Value, txtValidDateBH.DateText, txtValidDateEH.DateText, txtLastChgComp.Text, txtLastChgID.Text, txtLastChgDate.Text)
            Catch ex As Exception
                Bsp.Utility.ShowMessage(Me, "[CC0201_99]" & Bsp.Utility.getMessage("E_00000") & Bsp.Utility.getInnerException(Me.FunID, ex))
                Return False
            End Try
        ElseIf ViewState.Item("OrganType") = "2-功能組織" Then
            '取得輸入資料

            Dim txtBossCompID As String = ddlBossCompID.SelectedValue
            Dim txtBossType As String = ddlBossType.SelectedValue

            txtLastChgComp.Text = UserProfile.ActCompID
            txtLastChgID.Text = UserProfile.ActUserID
            txtLastChgDate.Text = Now

            '儲存資料
            Try
                Return objOM2.OrganizationFlowBossLogUpdate(hidOrganID.Value, txtBossCompID, txtBoss.Text, txtBossType, hidValidDateBH.Value, txtValidDateBH.DateText, txtValidDateEH.DateText, txtLastChgComp.Text, txtLastChgID.Text, txtLastChgDate.Text)
            Catch ex As Exception
                Bsp.Utility.ShowMessage(Me, "[CC0201_99]" & Bsp.Utility.getMessage("E_00000") & Bsp.Utility.getInnerException(Me.FunID, ex))
                Return False
            End Try
        End If
        Return False
    End Function
End Class
