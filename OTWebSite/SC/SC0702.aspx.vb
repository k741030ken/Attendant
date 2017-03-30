'****************************************************
'功能說明：系統管理者資料維護-修改
'建立人員：Ann
'建立日期：2014.08.20
'****************************************************
Imports System.Data

Partial Class SC_SC0702
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

            If ht.ContainsKey("SelectedGroupID") Then
                ViewState.Item("GroupID") = ht("SelectedGroupID").ToString()
                GetData(ht("SelectedGroupID").ToString())
            Else
                Return
            End If
        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnUpdate"
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

    Private Sub GetData(ByVal GroupID As String)
        Dim bsGroup As New beSC_Group.Service()
        Dim beGroup As New beSC_Group.Row()

        beGroup.GroupID.Value = GroupID
        Try
            Using dt As DataTable = bsGroup.QueryByKey(beGroup).Tables(0)
                If dt.Rows.Count <= 0 Then Exit Sub
                beGroup = New beSC_Group.Row(dt.Rows(0))

                lblGroupID.Text = beGroup.GroupID.Value
                txtGroupName.Text = beGroup.GroupName.Value
                lblCreateDate.Text = beGroup.CreateDate.Value.ToString("yyyy/MM/dd HH:mm:ss")
                lblLastChgID.Text = beGroup.LastChgID.Value
                lblLastChgDate.Text = beGroup.LastChgDate.Value.ToString("yyyy/MM/dd HH:mm:ss")
            End Using
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".GetData", ex)
        End Try

    End Sub

    Private Function SaveData() As Boolean
        Dim beGroup As New beSC_Group.Row()
        Dim bsGroup As New beSC_Group.Service()
        Dim objSC As New SC

        beGroup.GroupID.Value = lblGroupID.Text
        beGroup.GroupName.Value = txtGroupName.Text
        beGroup.LastChgID.Value = UserProfile.ActUserID
        beGroup.LastChgDate.Value = Now

        If Not bsGroup.IsDataExists(beGroup) Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00020", "")
            Return False
        End If

        Try
            objSC.UpdateGroup(beGroup)
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".SaveData", ex)
            Return False
        End Try

        Return True
    End Function

    Private Function funCheckData() As Boolean
        '檢查功能代碼
        Dim strValue As String

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

        Return True

    End Function
End Class
