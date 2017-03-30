'****************************************************
'功能說明：群組維護-新增
'建立人員：Chung
'建立日期：2011.05.17
'****************************************************
Imports System.Data

Partial Class SC_SC0231
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ddlGroupType.Attributes.Add("onchange", "funGroupChange();")
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



        If ddlGroupType.SelectedItem.Value = "0" Then
            beGroup.GroupID.Value = txtGroupID.Text
            beGroup.GroupName.Value = txtGroupName.Text
        Else
            beGroup.GroupID.Value = ucUser.SelectedUserID
            beGroup.GroupName.Value = ucUser.SelectedUserName
        End If

        beGroup.LastChgDate.Value = Now
        beGroup.CreateDate.Value = Now
        beGroup.LastChgID.Value = UserProfile.ActUserID

        '判斷資料是否存在
        If bsGroup.IsDataExists(beGroup) Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00010", "")
            Return False
        End If

        Try
            objSC.AddGroup(beGroup)
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".SaveData", ex)
            Return False
        End Try

        Return True
    End Function

    Private Function funCheckData() As Boolean
        '檢查功能代碼
        Dim strValue As String

        If ddlGroupType.SelectedItem.Value = "0" Then
            '一般群組
            strValue = txtGroupID.Text.Trim().ToUpper()
            If strValue = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00030", "群組代碼")
                txtGroupID.Focus()
                Return False
            Else
                If Bsp.Utility.getStringLength(strValue) > txtGroupID.MaxLength Then
                    Bsp.Utility.ShowFormatMessage(Me, "W_00040", "群組代碼", txtGroupID.MaxLength.ToString())
                    txtGroupID.Focus()
                    Return False
                End If
                txtGroupID.Text = strValue
            End If

            strValue = txtGroupName.Text.Trim()
            If strValue = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00030", "組名")
                txtGroupName.Focus()
                Return False
            Else
                If Bsp.Utility.getStringLength(strValue) > txtGroupName.MaxLength Then
                    Bsp.Utility.ShowFormatMessage(Me, "W_00040", "組名", txtGroupName.MaxLength.ToString())
                    txtGroupName.Focus()
                    Return False
                End If
                txtGroupName.Text = strValue
            End If
        Else
            '個人群組
            If ucUser.SelectedUserID = "" Then
                '顯示未選取個人群組人員
                Bsp.Utility.ShowFormatMessage(Me, "W_02310")
                Return False
            End If
        End If

        Return True

    End Function
End Class
