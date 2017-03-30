'****************************************************
'功能說明：系統管理者資料維護-新增
'建立人員：Ann
'建立日期：2014.08.20
'****************************************************
Imports System.Data
Imports System.Data.Common

Partial Class SC_SC0701
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objSC As New SC()

        Dim strSysID As String
        strSysID = Bsp.Utility.subGetSysID(UserProfile.LoginSysID)
        Dim arySysID() As String = Split(strSysID, "-")
        lblSysName.Text = strSysID
        'ucSelectUserID.QuerySQL = "false"
        ucSelectUserID.ShowCompRole = False
        ucSelectUserID.ConnType = "SC"
 
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnAdd"
                If Not funCheckData() Then
                    Return
                End If
                If SaveData() Then
                    GoBack()
                End If
            Case "btnActionX"
                GoBack()
        End Select
    End Sub

    Private Sub GoBack()
        Dim ti As TransferInfo = Me.StateTransfer
        Me.TransferFramePage(ti.CallerUrl, Nothing, ti.Args)
    End Sub

    Private Function SaveData() As Boolean
        Dim beSCAdmin As New beSC_Admin.Row()
        Dim bsSCAdmin As New beSC_Admin.Service()
        Dim objSC As New SC

        beSCAdmin.SysID.Value = UserProfile.LoginSysID
        beSCAdmin.AdminComp.Value = lblAdminComp.Text
        beSCAdmin.AdminID.Value = lblAdminID.Text
        beSCAdmin.LastChgComp.Value = UserProfile.ActCompID
        beSCAdmin.LastChgID.Value = UserProfile.ActUserID
        beSCAdmin.LastChgDate.Value = Now

        '判斷資料是否存在
        If bsSCAdmin.IsDataExists(beSCAdmin) Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00010", "")
            Return False
        End If

        Try
            objSC.AddAdmin(beSCAdmin)
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".SaveData", ex)
            Return False
        End Try

        Return True
    End Function

    Private Function funCheckData() As Boolean
        '檢查功能代碼
        Dim strValue As String

        strValue = lblAdminID.Text.Trim()
        If strValue = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "系統管理者代碼")
            lblAdminID.Focus()
            Return False
        End If

        strValue = lblAdminName.Text.Trim()
        If strValue = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "系統管理者代碼")
            lblAdminName.Focus()
            Return False
        End If

        'If ddlGroupType.SelectedItem.Value = "0" Then
        '    '一般群組
        '    strValue = txtGroupID.Text.Trim().ToUpper()
        '    If strValue = "" Then
        '        Bsp.Utility.ShowFormatMessage(Me, "W_00030", "群組代碼")
        '        txtGroupID.Focus()
        '        Return False
        '    Else
        '        If Bsp.Utility.getStringLength(strValue) > txtGroupID.MaxLength Then
        '            Bsp.Utility.ShowFormatMessage(Me, "W_00040", "群組代碼", txtGroupID.MaxLength.ToString())
        '            txtGroupID.Focus()
        '            Return False
        '        End If
        '        txtGroupID.Text = strValue
        '    End If

        '    strValue = txtGroupName.Text.Trim()
        '    If strValue = "" Then
        '        Bsp.Utility.ShowFormatMessage(Me, "W_00030", "組名")
        '        txtGroupName.Focus()
        '        Return False
        '    Else
        '        If Bsp.Utility.getStringLength(strValue) > txtGroupName.MaxLength Then
        '            Bsp.Utility.ShowFormatMessage(Me, "W_00040", "組名", txtGroupName.MaxLength.ToString())
        '            txtGroupName.Focus()
        '            Return False
        '        End If
        '        txtGroupName.Text = strValue
        '    End If
        'Else
        '    '個人群組
        '    If ucUser.SelectedUserID = "" Then
        '        '顯示未選取個人群組人員
        '        Bsp.Utility.ShowFormatMessage(Me, "W_02310")
        '        Return False
        '    End If
        'End If

        Return True

    End Function

    'RE1701.asp.vb --> ucButtonQuerySelect.ascx --> PageQueryMultiSelect.aspx --> UserControl.js --> DoModalReturn
    Public Overrides Sub DoModalReturn(ByVal returnValue As String)
        'ex:returnValue = "ucSelectWorkType:BA0001,BAC000"

        Dim strSql As String = ""

        If returnValue <> "" Then
            Dim aryData() As String = returnValue.Split(":")

            Select Case aryData(0)

                '應試者編號
                Case "ucSelectUserID"
                    Dim aryValue() As String = Split(aryData(1), "|$|")

                    lblAdminComp.Text = aryValue(0) '20160315 Beatrice modify
                    lblAdminID.Text = aryValue(1)
                    lblAdminName.Text = aryValue(2)
                    'subLoadCheckInDate()    '20140509 Ann modify

            End Select
        End If
    End Sub

End Class
