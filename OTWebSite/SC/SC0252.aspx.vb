'****************************************************
'功能說明：使用者群組關係維護－以群組來看
'建立人員：Chung
'建立日期：2011/05/19
'****************************************************
Imports System.Data

Partial Class SC_SC0252
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadGroupData()
        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnAdd"       '新增
                DoAdd()
            Case "btnUpdate"    '修改
                DoUpdate()
            Case "btnQuery"     '查詢
                DoQuery()
            Case "btnDelete"    '刪除
                DoDelete()
            Case Else
                DoOtherAction()   '其他功能動作
        End Select
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

        For Each strKey As String In ht.Keys
            Select Case strKey
                Case "ddlGroupID"
                    Bsp.Utility.SetSelectedIndex(ddlGroupID, ht(strKey).ToString())
                Case "SelectedGroupID"
                    LoadGroupDetail(ht(strKey).ToString())
            End Select
        Next
    End Sub

    Private Sub DoAdd()

    End Sub

    Private Sub DoUpdate()
        If lblGroupID.Text = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00000")
            Return
        End If
        Dim btnU As New ButtonState(ButtonState.emButtonType.Update)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)

        btnU.Caption = "存檔返回"
        btnX.Caption = "返回"

        Me.TransferFramePage("~/SC/SC0253.aspx", New ButtonState() {btnU, btnX}, _
                             Bsp.Utility.FormatToParam(ddlGroupID), _
                             "SelectedGroupID=" & lblGroupID.Text)
    End Sub

    Private Sub DoQuery()
        If ddlGroupID.SelectedIndex < 0 Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00000")
            Return
        End If
        LoadGroupDetail(ddlGroupID.SelectedItem.Value)
    End Sub

    Private Sub DoDelete()

    End Sub

    Private Sub DoOtherAction()

    End Sub

    Private Sub LoadGroupDetail(ByVal strGroupID As String)
        Dim objSC As New SC

        Try
            Using dt As DataTable = objSC.GetGroupInfo(strGroupID, "GroupID, GroupName")
                If dt.Rows.Count > 0 Then
                    lblGroupID.Text = dt.Rows(0).Item("GroupID").ToString()
                    lblGroupName.Text = dt.Rows(0).Item("GroupName").ToString()
                End If
            End Using

            Using dt As DataTable = objSC.GetUserInfo("", "UserID, UserID + '-' + UserName FullName", _
                                    "And Exists (Select GroupID From SC_UserGroup Where UserID = SC_User.UserID And GroupID = " & Bsp.Utility.Quote(strGroupID) & ")")
                If dt.Rows.Count > 0 Then
                    lstUser.DataSource = dt
                    lstUser.DataTextField = "FullName"
                    lstUser.DataValueField = "UserID"
                    lstUser.DataBind()
                Else
                    lstUser.Items.Clear()
                    lstUser.Items.Add(New ListItem("(無資料)", ""))
                End If
            End Using

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".LoadGroupDetail", ex)
        End Try
    End Sub

    Private Sub LoadGroupData()
        Dim objSC As New SC

        Try
            Using dt As DataTable = objSC.GetGroupInfo("", "GroupID, GroupID + '-' + GroupName FullName")
                With ddlGroupID
                    .DataSource = dt
                    .DataTextField = "FullName"
                    .DataValueField = "GroupID"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".LoadGroupData", ex)
        End Try
    End Sub

    Protected Sub btnChangeToUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChangeToUser.Click
        Me.TransferFramePage("~/SC/SC0250.aspx", Nothing)
    End Sub
End Class
