'****************************************************
'功能說明：代理人作業
'建立人員：Chung
'建立日期：2013/01/28
'****************************************************
Imports System.Data

Partial Class SC_SC02B0
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If StateMain IsNot Nothing Then
            sdsMain.SelectCommand = CType(StateMain, String)
        End If

        If Not IsPostBack Then
            DoQuery()
        End If
        '設定畫面上初始Focus物件
        'Page.SetFocus(ObjectName)
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

    Private Sub DoAdd()
        If gvMain.Rows.Count = 0 Then
            Bsp.Utility.ShowFormatMessage(Me, "W_02B00")
            Return
        End If
        Dim intRow As Integer = selectedRow(gvMain)

        If intRow < 0 Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00000")
            Return
        End If

        Dim beAgencyExecuteLog As New beSC_AgencyExecuteLog.Row()
        With beAgencyExecuteLog
            .UserID.Value = CType(gvMain.Rows(intRow).FindControl("lblUserID"), Label).Text
            .AgentUserID.Value = UserProfile.UserID
            .AgencyType.Value = CType(gvMain.Rows(intRow).FindControl("lblAgencyType"), Label).Text
            .AgentDate.Value = Now
        End With

        Dim objSC As New SC

        Try
            ExecuteAgency(beAgencyExecuteLog.UserID.Value)
            objSC.AddAgencyExecuteLog(beAgencyExecuteLog)

            If beAgencyExecuteLog.AgencyType.Value = "1" Then
                'Bsp.Utility.RunClientScript(Me, "window.top.location='" & Bsp.Utility.getAppSetting("StartPage") & "';")
                Bsp.Utility.RunClientScript(Me, "window.top.location='" & Bsp.MySettings.StartPage & "';")
            Else
            End If
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoAdd", ex)
        End Try
    End Sub


    Private Sub ExecuteAgency(ByVal UserID As String)
        Dim objSC As New SC

        Try
            Using dt As DataTable = objSC.GetUserInfo(UserID, "*, dbo.funGetAOrgDefine('4', DeptID) DeptName, dbo.funGetAOrgDefine('4',OrganID) OrganName")
                If dt.Rows.Count = 0 Then
                    Bsp.Utility.ShowFormatMessage(Me, "E_00010")
                    Return
                End If
                If dt.Rows(0).Item("BanMark").ToString() = "1" Then
                    Bsp.Utility.ShowFormatMessage(Me, "E_00020")
                    Return
                End If
                Dim beUser As New beSC_User.Row(dt.Rows(0))

                Using dtUserGroup As DataTable = objSC.GetUserGroupInfo(UserID, "")
                    If dtUserGroup.Rows.Count = 0 Then
                        Bsp.Utility.ShowFormatMessage(Me, "E_00010")
                        Return
                    End If

                    Dim htSysID As New System.Collections.Generic.List(Of String)
                    Dim htCompRoleID As New System.Collections.Generic.List(Of String)
                    Dim htGroupID As New System.Collections.Generic.List(Of String)
                    For Each dr As DataRow In dtUserGroup.Rows
                        htSysID.Add(dr.Item("SysID").ToString())
                        htCompRoleID.Add(dr.Item("CompRoleID").ToString())
                        htGroupID.Add(dr.Item("GroupID").ToString())
                    Next

                    SC.ActAsAgent(beUser.UserID.Value, beUser.UserName.Value, beUser.CompID.Value, beUser.DeptID.Value, dt.Rows(0).Item("DeptName").ToString(), _
                                  beUser.OrganID.Value, dt.Rows(0).Item("OrganName").ToString(), htGroupID, htSysID, htCompRoleID)

                End Using

                Bsp.DB.ExecuteNonQuery(CommandType.StoredProcedure, "SP_CC0001_1", New Data.Common.DbParameter() { _
                                    Bsp.DB.getDbParameter("@argIP", Request.ServerVariables("REMOTE_ADDR")), _
                                    Bsp.DB.getDbParameter("@argSite", ""), _
                                    Bsp.DB.getDbParameter("@argUserID", UserProfile.ActUserID), _
                                    Bsp.DB.getDbParameter("@argActAs", UserProfile.UserID)})

            End Using

        Catch ex As Exception
            SC.CancelAgent()
            Throw ex
        End Try
    End Sub

    Private Sub DoUpdate()

    End Sub

    Private Sub DoQuery()
        Dim objSC As New SC()

        Try
            StateMain = objSC.GetSC02B0QueryString(UserProfile.UserID)
            sdsMain.SelectCommand = StateMain
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
        End Try
    End Sub

    Private Sub DoDelete()

    End Sub

    Private Sub DoOtherAction()

    End Sub

End Class
