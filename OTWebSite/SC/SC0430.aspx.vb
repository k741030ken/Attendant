'****************************************************
'功能說明：疑難排解
'建立人員：Chung
'建立日期：2014/02/12
'****************************************************
Imports System.Data

Partial Class SC_SC0430
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)

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

    End Sub

    Private Sub DoUpdate()
        Dim objSC As New SC
        Dim myUserInfo As UserInfo
        Dim strUserID As String

        Try
            strUserID = txtUserID.Text.ToString().ToUpper()
            Using dt As DataTable = objSC.GetUserInfo(strUserID, "*, (Select Top 1 OrganName From SC_Organization Where OrganID = SC_User.DeptID) as DeptName, " & _
                                                                 "Isnull((Select Top 1 OrganName From SC_Organization Where OrganID = SC_User.OrganID),'') as OrganName")
                If dt.Rows.Count = 0 Then
                    Bsp.Utility.ShowFormatMessage(Me, "E_00010")
                    Return
                End If
                If dt.Rows(0).Item("BanMark").ToString() = "1" Then
                    Bsp.Utility.ShowFormatMessage(Me, "E_00020")
                    Return
                End If
                Dim beUser As New beSC_User.Row(dt.Rows(0))

                Using dtUserGroup As DataTable = objSC.GetUserGroupInfo(strUserID)
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

                    myUserInfo = CType(Session(Bsp.MySettings.UserProfileSessionName), UserInfo)

                    myUserInfo.SysID = htSysID  '20140807 wei add 系統別代碼
                    myUserInfo.CompRoleID = htCompRoleID  '20140807 wei add 授權公司代碼
                    myUserInfo.UserID = beUser.UserID.Value
                    myUserInfo.UserName = beUser.UserName.Value
                    myUserInfo.CompID = beUser.CompID.Value '20140807 wei add 公司代碼
                    myUserInfo.DeptID = beUser.DeptID.Value
                    myUserInfo.DeptName = dt.Rows(0).Item("DeptName").ToString()
                    myUserInfo.OrganID = beUser.OrganID.Value
                    myUserInfo.OrganName = dt.Rows(0).Item("OrganName").ToString()

                    myUserInfo.GroupID = htGroupID
                End Using
                Session(Bsp.MySettings.UserProfileSessionName) = myUserInfo

                Bsp.DB.ExecuteNonQuery(CommandType.StoredProcedure, "SP_SC0001_1", New Data.Common.DbParameter() { _
                            Bsp.DB.getDbParameter("@argIP", Request.ServerVariables("REMOTE_ADDR")), _
                            Bsp.DB.getDbParameter("@argSite", ""), _
                            Bsp.DB.getDbParameter("@argUserID", UserProfile.ActUserID), _
                            Bsp.DB.getDbParameter("@argActAs", UserProfile.UserID)})

            End Using
            Bsp.Utility.RunClientScript(Me, "window.top.location='" & Bsp.MySettings.StartPage & "';")

        Catch ex As Exception
            myUserInfo = CType(Session(Bsp.MySettings.UserProfileSessionName), UserInfo)

            myUserInfo.SysID = myUserInfo.ActSysID  '20140807 wei add 系統別代碼
            myUserInfo.CompRoleID = myUserInfo.ActCompRoleID  '20140807 wei add 授權公司代碼
            myUserInfo.UserID = myUserInfo.ActUserID
            myUserInfo.UserName = myUserInfo.ActUserName
            myUserInfo.CompID = myUserInfo.ActCompID    '20140807 wei add 公司代碼
            myUserInfo.DeptID = myUserInfo.ActDeptID
            myUserInfo.DeptName = myUserInfo.ActDeptName
            myUserInfo.OrganID = myUserInfo.ActOrganID
            myUserInfo.OrganName = myUserInfo.ActOrganName

            myUserInfo.GroupID = myUserInfo.ActGroupID
            Throw ex
        End Try
    End Sub

    Private Sub DoQuery()

    End Sub

    Private Sub Transfer(ByVal ParamArray Args() As Object)

    End Sub

    Private Sub DoDelete()

    End Sub

    Private Sub DoOtherAction()

    End Sub

End Class
