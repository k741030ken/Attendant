'****************************************************
'功能說明：單位分行註記檔-修改
'建立人員：John 
'建立日期：2017.06.06
'****************************************************
Imports System.Data

Partial Class OV_AT2002
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then

        End If
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

            If ht.ContainsKey("SelectedCompID") Then
                ViewState.Item("CompID") = ht("SelectedCompID").ToString()
                subGetData(ht("SelectedCompID").ToString(), ht("SelectedDeptID").ToString(), ht("SelectedOrganID").ToString())
            Else
                Return
            End If
        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnUpdate"   '存檔返回
                If funCheckData() Then
                    If SaveData() Then
                        GoBack()
                    End If
                End If
            Case "btnActionX"   '返回
                GoBack()
            Case "btnCancel"    '清除
                ClearData()
        End Select
    End Sub

    Private Sub GoBack()
        Dim ti As TransferInfo = Me.StateTransfer
        Me.TransferFramePage(ti.CallerUrl, Nothing, ti.Args)
    End Sub

    Private Function SaveData() As Boolean
        Dim beOrgBranchMark As New beOrgBranchMark.Row()
        Dim bsOrgBranchMark As New beOrgBranchMark.Service()
        Dim objAT As New AT2()

        '取得輸入資料
        beOrgBranchMark.CompID.Value = UserProfile.SelectCompRoleID
        beOrgBranchMark.DeptID.Value = saveDeptID.Value
        beOrgBranchMark.OrganID.Value = saveOrganID.Value
        beOrgBranchMark.BranchFlag.Value = IIf(rdoIsBranch.Checked, "1", "0")
        beOrgBranchMark.LastChgComp.Value = UserProfile.ActCompID
        beOrgBranchMark.LastChgID.Value = UserProfile.ActUserID
        beOrgBranchMark.LastChgDate.Value = Now

        '儲存資料
        Try
            Return objAT.UpdateOrgBranchMarkSetting(beOrgBranchMark)
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, "[CC0201_99]" & Bsp.Utility.getMessage("E_00000") & Bsp.Utility.getInnerException(Me.FunID, ex))
            Return False
        End Try
    End Function

    Private Sub subGetData(ByVal CompID As String, ByVal DeptID As String, ByVal OrganID As String)
        Dim objAT As New AT2()
        Dim objSC As New SC()
        Dim beOrgBranchMark As New beOrgBranchMark.Row()
        Dim bsOrgBranchMark As New beOrgBranchMark.Service()

        beOrgBranchMark.CompID.Value = CompID
        beOrgBranchMark.DeptID.Value = DeptID
        beOrgBranchMark.OrganID.Value = OrganID
        Try
            Using dt As DataTable = objAT.GetOrgBranchMarkData(CompID, DeptID, OrganID)
                If dt.Rows.Count <= 0 Then Exit Sub
                For Each dr As DataRow In dt.Rows
                    lbltxtCompID.Text = UserProfile.SelectCompRoleName
                    lblDeptID_Name.Text = dr.Item("DeptID").ToString.Trim + dr.Item("DeptName").ToString.Trim
                    saveDeptID.Value = dr.Item("DeptID").ToString.Trim
                    lblOrganID_Name.Text = dr.Item("OrganID").ToString.Trim + dr.Item("OrganName").ToString.Trim
                    saveOrganID.Value = dr.Item("OrganID").ToString.Trim
                    If dr.Item("BranchFlag").ToString.Trim = "0" Then
                        rdoNotBranch.Checked = True
                    Else
                        rdoIsBranch.Checked = True
                    End If

                    '最後異動公司
                    If dr.Item("LastChgID").ToString.Trim <> "" Then
                        lblLastChgComp.Text = dr.Item("LastChgComp").ToString.Trim + "-" + objSC.GetCompName(dr.Item("LastChgComp").ToString.Trim).Rows(0).Item("CompName").ToString
                    Else
                        lblLastChgComp.Text = ""
                    End If
                    '最後異動人員
                    If dr.Item("LastChgID").ToString.Trim <> "" Then
                        Dim UserName As String = objSC.GetSC_UserName(dr.Item("LastChgComp").ToString.Trim, dr.Item("LastChgID").ToString.Trim)
                        lblLastChgID.Text = dr.Item("LastChgID").ToString.Trim + IIf(UserName <> "", "-" + UserName, "")
                    Else
                        lblLastChgID.Text = ""
                    End If
                    '最後異動日期
                    lblLastChgDate.Text = dr.Item("LastChgDate").ToString().Replace("-", "/")
                Next

            End Using
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & "subGetData", ex)
        End Try
    End Sub

    Private Function funCheckData() As Boolean

        '班別代碼
        If rdoIsBranch.Checked = False And rdoNotBranch.Checked = False Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblBranchFlag.Text)
            Return False
        End If

        Return True
    End Function

    Private Sub ClearData()
        subGetData(UserProfile.SelectCompRoleID, saveDeptID.Value, saveOrganID.Value)
    End Sub

End Class
