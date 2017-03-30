'****************************************************
'功能說明：群組維護-新增
'建立人員：Ann
'建立日期：2014.08.28
'****************************************************
Imports System.Data
Imports System.Data.Common

Partial Class SC_SC0401
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            Dim objSC As New SC
            'ddlGroupType.Attributes.Add("onchange", "funGroupChange();")
            Dim strSysID As String
            strSysID = Bsp.Utility.subGetSysID(UserProfile.LoginSysID)
            Dim arySysID() As String = Split(strSysID, "-")
            lblSysName.Text = strSysID
            ddlCompRoleName.Visible = False

            If UserProfile.SelectCompRoleID = "ALL" Then
                ddlCompRoleName.Visible = True
                Bsp.Utility.FillCompany(ddlCompRoleName)
                ddlCompRoleName.SelectedIndex = 0
                lblCompRoleName.Visible = False
            Else
                '系統管理者
                If UserProfile.IsSysAdmin = True Then
                    ''系統管理者選擇全金控
                    'If UserProfile.SelectCompRoleID = "ALL" Then
                    '    ddlCompRoleName.Visible = True
                    '    Bsp.Utility.FillCompany(ddlCompRoleName)
                    '    lblCompRoleName.Visible = False
                    'Else
                    '    ddlCompRoleName.Visible = False
                    '    lblCompRoleName.Text = objSC.GetCompName(UserProfile.SelectCompRoleID).Rows(0).Item("CompName").ToString()
                    '    lblCompRoleName.Visible = True
                    'End If
                    ddlCompRoleName.Visible = True
                    Bsp.Utility.FillCompany(ddlCompRoleName)
                    ddlCompRoleName.Items.Insert(0, New ListItem("全金控", "0"))
                    lblCompRoleName.Visible = False
                Else
                    '非系統管理者
                    ddlCompRoleName.Visible = False
                    lblCompRoleName.Text = objSC.GetCompName(UserProfile.SelectCompRoleID).Rows(0).Item("CompName").ToString()
                    lblCompRoleName.Visible = True
                End If
            End If
        End If
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
        Dim beGroup As New beSC_Group.Row()
        Dim bsGroup As New beSC_Group.Service()
        Dim objSC As New SC

        If funCheckData() Then
            Using cn As DbConnection = Bsp.DB.getConnection()
                cn.Open()

                Dim tran As DbTransaction = cn.BeginTransaction
                Dim inTrans As Boolean = True

                Try
                    beGroup.SysID.Value = UserProfile.LoginSysID
                    If lblCompRoleName.Text <> "" Then
                        beGroup.CompRoleID.Value = UserProfile.SelectCompRoleID
                    Else
                        beGroup.CompRoleID.Value = ddlCompRoleName.SelectedValue
                    End If
                    beGroup.GroupID.Value = txtGroupID.Text.ToUpper
                    beGroup.GroupName.Value = txtGroupName.Text

                    beGroup.LastChgComp.Value = UserProfile.ActCompID
                    beGroup.LastChgID.Value = UserProfile.ActUserID
                    beGroup.LastChgDate.Value = Format(Now, "yyyy/MM/dd HH:mm:ss")
                    beGroup.CreateDate.Value = Format(Now, "yyyy/MM/dd HH:mm:ss")

                    '判斷資料是否存在
                    If bsGroup.IsDataExists(beGroup) Then
                        Bsp.Utility.ShowFormatMessage(Me, "W_00010", "")
                        Return False
                    End If

                    Try
                        If ddlCompRoleName.SelectedValue = "0" Then
                            objSC.AddGroupAll(txtGroupID.Text.ToUpper, txtGroupName.Text)
                        Else
                            objSC.AddGroup(beGroup)
                        End If
                    Catch ex As Exception
                        Bsp.Utility.ShowMessage(Me, Me.FunID & ".SaveData", ex)
                        Return False
                    End Try
                    tran.Commit()
                Catch ex As Exception
                    If inTrans Then tran.Rollback()
                    Return False
                End Try
            End Using
        End If
        Return True
    End Function

    Private Function funCheckData() As Boolean
        '檢查功能代碼
        Dim strValue As String

        strValue = txtGroupID.Text.Trim()
        If strValue = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "群組代碼")
            txtGroupID.Focus()
            Return False
        End If

        strValue = txtGroupName.Text.Trim()
        If strValue = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "群組名稱")
            txtGroupName.Focus()
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
End Class
