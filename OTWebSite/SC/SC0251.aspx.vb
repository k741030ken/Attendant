'****************************************************
'功能說明：使用者群組關係維護-修改
'建立人員：Chung
'建立日期：2011/05/19
'****************************************************
Imports System.Data

Partial Class SC_SC0251
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '設定畫面上初始Focus物件
        'Page.SetFocus(ObjectName)
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

            If ht.ContainsKey("SelectedUserID") Then
                ViewState.Item("UserID") = ht("SelectedUserID").ToString()
                GetData(ht("SelectedUserID").ToString())
            Else
                Return
            End If
        End If
    End Sub

    Private Sub GoBack()
        Dim ti As TransferInfo = Me.StateTransfer
        Me.TransferFramePage(ti.CallerUrl, Nothing, ti.Args)
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnUpdate"
                If DoUpdate() Then GoBack()
            Case "btnActionX"
                GoBack()
        End Select
    End Sub

    Private Sub DoAdd()

    End Sub

    Private Function DoUpdate() As Boolean
        Dim arySelectedGroup() As String = ucGroupSelect.ValueResult.Split(",")
        Dim objSC As New SC
        Dim strGrpInsert As String = ""
        Dim strGrpDelete As String = ""
        Dim strPGrpDel As String = ""
        Dim beUserGroup_Ins() As beSC_UserGroup.Row = Nothing
        Dim beUserGroup_Del() As beSC_UserGroup.Row = Nothing
        Dim beGroup_Del() As beSC_Group.Row = Nothing

        Try
            Using dtUserGroup As DataTable = objSC.GetGroupInfo("", "GroupID, GroupID + '-' + GroupName GroupName, GroupType", _
                                                                "And Exists (Select GroupID From SC_UserGroup Where GroupID = SC_Group.GroupID And UserID = " & Bsp.Utility.Quote(lblUserID.Text) & ")")
                '新增Group
                For intLoop As Integer = 0 To arySelectedGroup.GetUpperBound(0)
                    If dtUserGroup.Select("GroupID=" & Bsp.Utility.Quote(arySelectedGroup(intLoop))).Length <= 0 Then
                        If strGrpInsert <> "" Then strGrpInsert &= ","
                        strGrpInsert &= arySelectedGroup(intLoop)
                    End If
                Next

                '刪除Group
                For intLoop As Integer = 0 To dtUserGroup.Rows.Count - 1
                    If Array.IndexOf(arySelectedGroup, dtUserGroup.Rows(intLoop).Item("GroupID").ToString()) < 0 Then
                        If strGrpDelete <> "" Then strGrpDelete &= ","
                        strGrpDelete &= dtUserGroup.Rows(intLoop).Item("GroupID").ToString()

                        '若為個人群組，刪除時一併刪除Group資料
                        If dtUserGroup.Rows(intLoop).Item("GroupType").ToString() = "1" Then
                            If strPGrpDel <> "" Then strPGrpDel &= ","
                            strPGrpDel &= dtUserGroup.Rows(intLoop).Item("GroupID").ToString()
                        End If
                    End If
                Next
            End Using
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoUpdate.1", ex)
            Return False
        End Try

        Dim aryIns() As String = strGrpInsert.Split(",")
        Dim aryDel() As String = strGrpDelete.Split(",")
        Dim aryDelPG() As String = strPGrpDel.Split(",")

        If aryIns.Length > 1 OrElse (aryIns.Length = 1 AndAlso aryIns(0) <> "") Then
            ReDim beUserGroup_Ins(aryIns.GetUpperBound(0))

            For intLoop As Integer = 0 To aryIns.GetUpperBound(0)
                If aryIns(intLoop) <> "" Then
                    Dim beUG As New beSC_UserGroup.Row()

                    beUG.UserID.Value = lblUserID.Text
                    beUG.GroupID.Value = aryIns(intLoop)
                    beUG.CreateDate.Value = Now
                    beUG.LastChgDate.Value = Now
                    beUG.LastChgID.Value = UserProfile.ActUserID

                    beUserGroup_Ins(intLoop) = beUG
                End If
            Next
        End If

        If aryDel.Length > 1 OrElse (aryDel.Length = 1 AndAlso aryDel(0) <> "") Then
            ReDim beUserGroup_Del(aryDel.GetUpperBound(0))

            For intLoop As Integer = 0 To aryDel.GetUpperBound(0)
                If aryDel(intLoop) <> "" Then
                    Dim beUG As New beSC_UserGroup.Row()

                    beUG.UserID.Value = lblUserID.Text
                    beUG.GroupID.Value = aryDel(intLoop)
                    beUG.CreateDate.Value = Now
                    beUG.LastChgDate.Value = Now
                    beUG.LastChgID.Value = UserProfile.ActUserID

                    beUserGroup_Del(intLoop) = beUG
                End If
            Next
        End If

        If aryDelPG.Length > 1 OrElse (aryDelPG.Length = 1 AndAlso aryDelPG(0) <> "") Then
            ReDim beGroup_Del(aryDelPG.GetUpperBound(0))

            For intLoop As Integer = 0 To aryDelPG.GetUpperBound(0)
                If aryDelPG(intLoop) <> "" Then
                    Dim beGroup As New beSC_Group.Row()

                    beGroup.GroupID.Value = aryDelPG(intLoop)
                    beGroup_Del(intLoop) = beGroup
                End If
            Next
        End If

        Try
            objSC.UpdateUserGroup(beUserGroup_Ins, beUserGroup_Del, beGroup_Del)
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoUpdate.2", ex)
            Return False
        End Try
        Return True
    End Function

    Private Sub GetData(ByVal UserID As String)
        Dim beUser As beSC_User.Row
        Dim objSC As New SC

        Try
            Using dt As DataTable = objSC.GetUserInfo(UserID)
                If dt.Rows.Count = 0 Then Exit Sub

                beUser = New beSC_User.Row(dt.Rows(0))
                lblUserID.Text = beUser.UserID.Value
                lblUserName.Text = beUser.UserName.Value

                Using dtGroups As DataTable = objSC.GetGroupInfo("", "GroupID, GroupID + '-' + GroupName GroupName, GroupType", _
                                                                "And Exists (Select GroupID From SC_UserGroup Where GroupID = SC_Group.GroupID And UserID = " & Bsp.Utility.Quote(lblUserID.Text) & ")")
                    ucGroupSelect.LoadRightData(dtGroups)
                End Using
                Using dtFreeGroups As DataTable = objSC.GetGroupInfo("", "GroupID, GroupID + '-' + GroupName GroupName, GroupType", _
                                                                "And not Exists (Select GroupID From SC_UserGroup Where GroupID = SC_Group.GroupID And UserID = " & Bsp.Utility.Quote(lblUserID.Text) & ")")
                    ucGroupSelect.LoadLeftData(dtFreeGroups)
                End Using

            End Using

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".GetData", ex)
        End Try
    End Sub

    Private Sub DoDelete()

    End Sub

    Private Sub DoOtherAction()

    End Sub

End Class
