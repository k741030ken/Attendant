'****************************************************
'功能說明：群組維護-修改
'建立人員：Ann
'建立日期：2014.08.28
'****************************************************
Imports System.Data
Imports System.Data.Common

Partial Class SC_SC0402
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            Dim objSC As New SC
            Dim strSysID As String
            strSysID = Bsp.Utility.subGetSysID(UserProfile.LoginSysID)
            Dim arySysID() As String = Split(strSysID, "-")
            lblSysName.Text = strSysID
            ddlCompRoleName.Visible = False

            If UserProfile.SelectCompRoleID = "ALL" Then
                ddlCompRoleName.Visible = True
                Bsp.Utility.FillCompany(ddlCompRoleName)
                lblCompRoleName.Visible = False
            Else '系統管理者
                If UserProfile.IsSysAdmin = True Then
                    '系統管理者選擇全金控
                    If UserProfile.SelectCompRoleID = "ALL" Then
                        ddlCompRoleName.Visible = True
                        Bsp.Utility.FillCompany(ddlCompRoleName)
                        lblCompRoleName.Visible = False
                    Else
                        ddlCompRoleName.Visible = False
                        lblCompRoleName.Text = objSC.GetCompName(UserProfile.SelectCompRoleID).Rows(0).Item("CompName").ToString()
                        lblCompRoleName.Visible = True
                    End If
                Else
                    '非系統管理者
                    ddlCompRoleName.Visible = False
                    lblCompRoleName.Text = objSC.GetCompName(UserProfile.SelectCompRoleID).Rows(0).Item("CompName").ToString()
                    lblCompRoleName.Visible = True
                End If
            End If
        End If
    End Sub
 
    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

            If ht.ContainsKey("SelectedGroupID") Then
                ViewState.Item("SysID") = ht("SelectedSysID").ToString()
                ViewState.Item("CompRoleID") = ht("SelectedCompRoleID").ToString()
                ViewState.Item("GroupID") = ht("SelectedGroupID").ToString()
                GetData(ht("SelectedSysID").ToString(), ht("SelectedCompRoleID").ToString(), ht("SelectedGroupID").ToString())
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

    Private Sub GetData(ByVal SysID As String, ByVal CompRoleID As String, ByVal GroupID As String)
        Dim objSC As New SC()
        Dim bsGroup As New beSC_Group.Service()
        Dim beGroup As New beSC_Group.Row()

        Try
            beGroup.SysID.Value = SysID
            beGroup.CompRoleID.Value = CompRoleID
            beGroup.GroupID.Value = GroupID

            Using dt As DataTable = bsGroup.QueryByKey(beGroup).Tables(0)
                If dt.Rows.Count <= 0 Then Exit Sub
                beGroup = New beSC_Group.Row(dt.Rows(0))
                Bsp.Utility.SetSelectedIndex(ddlCompRoleName, beGroup.CompRoleID.Value)
 
                lblGroupID.Text = beGroup.GroupID.Value
                txtGroupName.Text = beGroup.GroupName.Value

                '20150306 Beatrice modify
                Dim CompName As String = ""
                If objSC.GetCompName(beGroup.LastChgComp.Value).Rows.Count > 0 Then
                    CompName = objSC.GetCompName(beGroup.LastChgComp.Value).Rows(0).Item("CompName").ToString()
                End If
                lblLastChgComp.Text = beGroup.LastChgComp.Value + "-" + CompName
                '20150306 Beatrice modify End
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

        If funCheckData() Then
            Using cn As DbConnection = Bsp.DB.getConnection()
                cn.Open()

                Dim tran As DbTransaction = cn.BeginTransaction
                Dim inTrans As Boolean = True

                Try
                    beGroup.SysID.Value = UserProfile.LoginSysID
                    'beGroup.CompRoleID.Value = ddlCompRoleName.SelectedItem.Value
                    If lblCompRoleName.Text <> "" Then
                        beGroup.CompRoleID.Value = UserProfile.SelectCompRoleID
                    Else
                        beGroup.CompRoleID.Value = ddlCompRoleName.SelectedValue
                    End If
                    beGroup.GroupID.Value = lblGroupID.Text
                    beGroup.GroupName.Value = txtGroupName.Text
                    beGroup.LastChgComp.Value = UserProfile.ActCompID
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

        strValue = txtGroupName.Text.Trim()
        If strValue = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "群組名稱")
            txtGroupName.Focus()
            Return False
        Else
            If Bsp.Utility.getStringLength(strValue) > txtGroupName.MaxLength Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00040", "群組名稱", txtGroupName.MaxLength.ToString())
                txtGroupName.Focus()
                Return False
            End If
            txtGroupName.Text = strValue
        End If

        Return True

    End Function
End Class
