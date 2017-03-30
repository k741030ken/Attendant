'************************************************************
'功能說明：提供目前使用者個人基本資料
'建立人員：Wei
'建立日期：2014.08.07
'************************************************************
Imports Microsoft.VisualBasic
Imports System.Data

Public Class UserProfile
    '20140808 wei add Login的系統別代碼
    Public Shared ReadOnly Property LoginSysID() As String
        Get
            If HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName) Is Nothing Then
                Throw New Exception("[UserProfile]:无法取得用户信息，请重新登入！")
            End If
            Return CType(HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName), UserInfo).LoginSysID
        End Get
    End Property
    '20140818 wei add 選擇權限公司代碼
    Public Shared Property SelectCompRoleID() As String
        Get
            If HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName) Is Nothing Then
                Throw New Exception("[UserProfile]:无法取得用户信息，请重新登入！")
            End If
            Return CType(HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName), UserInfo).SelectCompRoleID
        End Get
        Set(ByVal value As String)
            CType(HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName), UserInfo).SelectCompRoleID = value
        End Set
    End Property
    '20150528 wei add 選擇權限公司
    Public Shared Property SelectCompRoleName() As String
        Get
            If HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName) Is Nothing Then
                Throw New Exception("[UserProfile]:无法取得用户信息，请重新登入！")
            End If
            Return CType(HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName), UserInfo).SelectCompRoleName
        End Get
        Set(ByVal value As String)
            CType(HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName), UserInfo).SelectCompRoleName = value
        End Set
    End Property
    '20150319 wei add 選擇FunID
    Public Shared Property SelectFunID() As String
        Get
            If HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName) Is Nothing Then
                Throw New Exception("[UserProfile]:无法取得用户信息，请重新登入！")
            End If
            Return CType(HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName), UserInfo).SelectFunID

        End Get
        Set(ByVal value As String)
            CType(HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName), UserInfo).SelectFunID = value
        End Set
    End Property
    '20140807 wei add 系統別代碼
    Public Shared ReadOnly Property SysID() As System.Collections.Generic.List(Of String)
        Get
            If HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName) Is Nothing Then
                Throw New Exception("[UserProfile]:无法取得用户信息，请重新登入！")
            End If
            Return CType(HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName), UserInfo).SysID
        End Get
    End Property
    '20140807 wei add 授權公司代碼
    Public Shared ReadOnly Property CompRoleID() As System.Collections.Generic.List(Of String)
        Get
            If HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName) Is Nothing Then
                Throw New Exception("[UserProfile]:无法取得用户信息，请重新登入！")
            End If
            Return CType(HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName), UserInfo).CompRoleID
        End Get
    End Property
    Public Shared ReadOnly Property UserID() As String
        Get
            If HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName) Is Nothing Then
                Throw New Exception("[UserProfile]:无法取得用户信息，请重新登入！")
            End If
            Return CType(HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName), UserInfo).UserID
        End Get
    End Property

    Public Shared ReadOnly Property UserName() As String
        Get
            If HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName) Is Nothing Then
                Throw New Exception("[UserProfile]:无法取得用户信息，请重新登入！")
            End If
            Return CType(HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName), UserInfo).UserName
        End Get
    End Property
    '20140807 wei add 公司代碼
    Public Shared ReadOnly Property CompID() As String
        Get
            If HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName) Is Nothing Then
                Throw New Exception("[UserProfile]:无法取得用户信息，请重新登入！")
            End If
            Return CType(HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName), UserInfo).CompID
        End Get
    End Property
    '20140807 wei add 公司
    Public Shared ReadOnly Property CompName() As String
        Get
            If HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName) Is Nothing Then
                Throw New Exception("[UserProfile]:无法取得用户信息，请重新登入！")
            End If
            Return CType(HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName), UserInfo).CompName
        End Get
    End Property
    Public Shared ReadOnly Property DeptID() As String
        Get
            If HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName) Is Nothing Then
                Throw New Exception("[UserProfile]:无法取得用户信息，请重新登入！")
            End If
            Return CType(HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName), UserInfo).DeptID
        End Get
    End Property

    Public Shared ReadOnly Property DeptName() As String
        Get
            If HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName) Is Nothing Then
                Throw New Exception("[UserProfile]:无法取得用户信息，请重新登入！")
            End If
            Return CType(HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName), UserInfo).DeptName
        End Get
    End Property

    Public Shared ReadOnly Property OrganID() As String
        Get
            If HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName) Is Nothing Then
                Throw New Exception("[UserProfile]:无法取得用户信息，请重新登入！")
            End If
            Return CType(HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName), UserInfo).OrganID
        End Get
    End Property

    Public Shared ReadOnly Property OrganName() As String
        Get
            If HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName) Is Nothing Then
                Throw New Exception("[UserProfile]:无法取得用户信息，请重新登入！")
            End If
            Return CType(HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName), UserInfo).OrganName
        End Get
    End Property

    Public Shared ReadOnly Property GroupID() As System.Collections.Generic.List(Of String)
        Get
            If HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName) Is Nothing Then
                Throw New Exception("[UserProfile]:无法取得用户信息，请重新登入！")
            End If
            Return CType(HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName), UserInfo).GroupID
        End Get
    End Property
    '20140807 wei add 系統別代碼
    Public Shared ReadOnly Property ActSysID() As System.Collections.Generic.List(Of String)
        Get
            If HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName) Is Nothing Then
                Throw New Exception("[UserProfile]:无法取得用户信息，请重新登入！")
            End If
            Return CType(HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName), UserInfo).ActSysID
        End Get
    End Property
    '20140807 wei add 授權公司代碼
    Public Shared ReadOnly Property ActCompRoleID() As System.Collections.Generic.List(Of String)
        Get
            If HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName) Is Nothing Then
                Throw New Exception("[UserProfile]:无法取得用户信息，请重新登入！")
            End If
            Return CType(HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName), UserInfo).ActCompRoleID
        End Get
    End Property
    Public Shared ReadOnly Property ActUserID() As String
        Get
            If HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName) Is Nothing Then
                Throw New Exception("[UserProfile]:无法取得用户信息，请重新登入！")
            End If
            Return CType(HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName), UserInfo).ActUserID
        End Get
    End Property

    Public Shared ReadOnly Property ActUserName() As String
        Get
            If HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName) Is Nothing Then
                Throw New Exception("[UserProfile]:无法取得用户信息，请重新登入！")
            End If
            Return CType(HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName), UserInfo).ActUserName
        End Get
    End Property
    '20140807 wei add 公司代碼
    Public Shared ReadOnly Property ActCompID() As String
        Get
            If HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName) Is Nothing Then
                Throw New Exception("[UserProfile]:无法取得用户信息，请重新登入！")
            End If
            Return CType(HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName), UserInfo).ActCompID
        End Get
    End Property
    '20140807 wei add 公司
    Public Shared ReadOnly Property ActCompName() As String
        Get
            If HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName) Is Nothing Then
                Throw New Exception("[UserProfile]:无法取得用户信息，请重新登入！")
            End If
            Return CType(HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName), UserInfo).ActCompName
        End Get
    End Property
    Public Shared ReadOnly Property ActDeptID() As String
        Get
            If HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName) Is Nothing Then
                Throw New Exception("[UserProfile]:无法取得用户信息，请重新登入！")
            End If
            Return CType(HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName), UserInfo).ActDeptID
        End Get
    End Property

    Public Shared ReadOnly Property ActDeptName() As String
        Get
            If HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName) Is Nothing Then
                Throw New Exception("[UserProfile]:无法取得用户信息，请重新登入！")
            End If
            Return CType(HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName), UserInfo).ActDeptName
        End Get
    End Property

    Public Shared ReadOnly Property ActOrganID() As String
        Get
            If HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName) Is Nothing Then
                Throw New Exception("[UserProfile]:无法取得用户信息，请重新登入！")
            End If
            Return CType(HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName), UserInfo).ActOrganID
        End Get
    End Property

    Public Shared ReadOnly Property ActOrganName() As String
        Get
            If HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName) Is Nothing Then
                Throw New Exception("[UserProfile]:无法取得用户信息，请重新登入！")
            End If
            Return CType(HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName), UserInfo).ActOrganName
        End Get
    End Property

    Public Shared ReadOnly Property ActGroupID() As System.Collections.Generic.List(Of String)
        Get
            If HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName) Is Nothing Then
                Throw New Exception("[UserProfile]:无法取得用户信息，请重新登入！")
            End If
            Return CType(HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName), UserInfo).ActGroupID
        End Get
    End Property

    Public ReadOnly Property BossID() As String
        Get
            If HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName) Is Nothing Then
                Throw New Exception("[UserProfile]:无法取得用户信息，请重新登入！")
            End If
            Return GetBossID()
        End Get
    End Property

    Public ReadOnly Property BossName() As String
        Get
            If HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName) Is Nothing Then
                Throw New Exception("[UserProfile]:无法取得用户信息，请重新登入！")
            End If
            Return GetBossName()
        End Get
    End Property
    '20140811 wei add 是否為系統管理者
    Public Shared ReadOnly Property IsSysAdmin() As Boolean
        Get
            If HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName) Is Nothing Then
                'Throw New Exception("[UserProfile]:无法取得用户信息，请重新登入！")
                GetLastLoginInfo()
            End If
            Select CType(HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName), UserInfo).IsBranchUser
                Case "NA"
                    Dim strSQL As New StringBuilder()

                    strSQL.AppendLine("Select Count(*) From SC_Admin")
                    strSQL.AppendLine("Where AdminID = " & Bsp.Utility.Quote(UserID))

                    If CInt(Bsp.DB.ExecuteScalar(strSQL.ToString())) = 0 Then
                        CType(HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName), UserInfo).IsSysAdmin = "N"
                        Return False
                    Else
                        CType(HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName), UserInfo).IsSysAdmin = "Y"
                        Return True
                    End If
                Case "Y"
                    Return True
                Case "N"
                    Return False
            End Select
        End Get
    End Property
    ''是否为分行人员
    'Public Shared ReadOnly Property IsBranchUser() As Boolean
    '    Get
    '        If HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName) Is Nothing Then
    '            'Throw New Exception("[UserProfile]:无法取得用户信息，请重新登入！")
    '            GetLastLoginInfo()
    '        End If
    '        Select Case CType(HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName), UserInfo).IsBranchUser
    '            Case "NA"
    '                Dim strSQL As New StringBuilder()

    '                strSQL.AppendLine("Select Count(*) From SC_Organization")
    '                strSQL.AppendLine("Where OrganID = " & Bsp.Utility.Quote(DeptID))
    '                strSQL.AppendLine("And InValidFlag = '0'")
    '                strSQL.AppendLine("And BranchFlag = '1'")
    '                strSQL.AppendLine("And BusinessFlag = '1'")

    '                If CInt(Bsp.DB.ExecuteScalar(strSQL.ToString())) = 0 Then
    '                    CType(HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName), UserInfo).IsBranchUser = "N"
    '                    Return False
    '                Else
    '                    CType(HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName), UserInfo).IsBranchUser = "Y"
    '                    Return True
    '                End If
    '            Case "Y"
    '                Return True
    '            Case "N"
    '                Return False
    '        End Select
    '    End Get
    'End Property

    Public Sub New()
        GetIdentity()
    End Sub



    Private Sub GetIdentity()

    End Sub

    Private Function GetBossID() As String
        Dim objSC As New SC
        Using dt As DataTable = objSC.GetOrganInfo(DeptID, "Boss")
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item("Boss").ToString()
            Else
                Return ""
            End If
        End Using
    End Function

    Private Function GetBossName() As String
        Dim objSC As New SC
        Using dt As DataTable = objSC.GetOrganInfo(DeptID, "dbo.funGetAOrgDefine('3', Boss) BossName")
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item("BossName").ToString()
            Else
                Return ""
            End If
        End Using
    End Function

    Public Shared Sub GetLastLoginInfo()
        Dim IP As String = HttpContext.Current.Request.ServerVariables("REMOTE_ADDR").ToString()

        Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, "Select distinct UserID, ActAs From SC_OnlineUser Where IP = " & Bsp.Utility.Quote(IP)).Tables(0)
            If dt.Rows.Count = 1 Then
                CreateUserProfile(dt.Rows(0).Item("UserID").ToString(), dt.Rows(0).Item("ActAs").ToString())
            Else
                Return
            End If
        End Using
    End Sub

    ''' <summary>
    ''' 建立UserProfile物件
    ''' </summary>
    ''' <param name="UserID"></param>
    ''' <remarks></remarks>
    Private Shared Sub CreateUserProfile(ByVal UserID As String, Optional ByVal ActAs As String = "")
        Dim objSC As New SC()

        Using dtUser As DataTable = objSC.GetUserInfo(UserID, "*, dbo.funGetAOrgDefine('4', DeptID) DeptName, dbo.funGetAOrgDefine('4', OrganID) OrganName,dbo.funGetACompDefine('4', CompID) CompName")
            If dtUser.Rows.Count > 0 Then
                Dim beUser As New beSC_User.Row(dtUser.Rows(0))
                Dim htSysID As New System.Collections.Generic.List(Of String)
                Dim htCompRoleID As New System.Collections.Generic.List(Of String)
                Dim htGroupID As New System.Collections.Generic.List(Of String)

                If beUser.BanMark.Value = "1" Then Return
                Using dtUserGroup As DataTable = objSC.GetUserGroupInfo(UserID, "", "SysID,CompRoleID,GroupID")
                    If dtUserGroup.Rows.Count = 0 Then Return
                    For Each dr As DataRow In dtUserGroup.Rows
                        htSysID.Add(dr.Item("SysID").ToString())
                        htCompRoleID.Add(dr.Item("CompRoleID").ToString())
                        htGroupID.Add(dr.Item("GroupID").ToString())
                    Next

                    Dim myUserInfo As New UserInfo

                    myUserInfo.SysID = htSysID  '20140807 wei add 系統別代碼
                    myUserInfo.CompRoleID = htCompRoleID  '20140807 wei add 授權公司代碼
                    myUserInfo.UserID = beUser.UserID.Value
                    myUserInfo.UserName = beUser.UserName.Value
                    myUserInfo.CompID = beUser.CompID.Value '20140807 wei add 公司代碼
                    myUserInfo.CompName = dtUser.Rows(0).Item("CompName").ToString()   '20140807 wei add 公司
                    myUserInfo.DeptID = beUser.DeptID.Value
                    myUserInfo.DeptName = dtUser.Rows(0).Item("DeptName").ToString()
                    myUserInfo.OrganID = beUser.OrganID.Value
                    myUserInfo.OrganName = dtUser.Rows(0).Item("OrganName").ToString()
                    myUserInfo.GroupID = htGroupID
                    myUserInfo.ActSysID = htSysID  '20140807 wei add 系統別代碼
                    myUserInfo.ActCompRoleID = htCompRoleID  '20140807 wei add 授權公司代碼
                    myUserInfo.ActUserID = beUser.UserID.Value
                    myUserInfo.ActUserName = beUser.UserName.Value
                    myUserInfo.ActCompID = beUser.CompID.Value '20140807 wei add 公司代碼
                    myUserInfo.ActCompName = dtUser.Rows(0).Item("CompName").ToString() '20140807 wei add 公司
                    myUserInfo.ActDeptID = beUser.DeptID.Value
                    myUserInfo.ActDeptName = dtUser.Rows(0).Item("DeptName").ToString()
                    myUserInfo.ActOrganID = beUser.OrganID.Value
                    myUserInfo.ActOrganName = dtUser.Rows(0).Item("OrganName").ToString()
                    myUserInfo.ActGroupID = htGroupID

                    HttpContext.Current.Session.Add(Bsp.MySettings.UserProfileSessionName, myUserInfo)
                End Using
            End If
        End Using

        If UserID <> ActAs AndAlso ActAs <> "" Then
            Using dtUser As DataTable = objSC.GetUserInfo("*, dbo.funGetAOrgDefine('4', DeptID) DeptName, dbo.funGetAOrgDefine('4', OrganID) OrganName,dbo.funGetACompDefine('4', CompID) CompName")
                If dtUser.Rows.Count > 0 Then
                    Dim beUser As New beSC_User.Row(dtUser.Rows(0))
                    Dim htSysID As New System.Collections.Generic.List(Of String)
                    Dim htCompRoleID As New System.Collections.Generic.List(Of String)
                    Dim htGroupID As New System.Collections.Generic.List(Of String)

                    Using dtUserGroup As DataTable = objSC.GetUserGroupInfo(UserID, "", "SysID,CompRoleID,GroupID")
                        If dtUserGroup.Rows.Count = 0 Then Return
                        For Each dr As DataRow In dtUserGroup.Rows
                            htSysID.Add(dr.Item("SysID").ToString())
                            htCompRoleID.Add(dr.Item("CompRoleID").ToString())
                            htGroupID.Add(dr.Item("GroupID").ToString())
                        Next

                        Dim myUserInfo As UserInfo = CType(HttpContext.Current.Session(Bsp.MySettings.UserProfileSessionName), UserInfo)

                        myUserInfo.UserID = beUser.UserID.Value
                        myUserInfo.UserName = beUser.UserName.Value
                        myUserInfo.DeptID = beUser.DeptID.Value
                        myUserInfo.DeptName = dtUser.Rows(0).Item("DeptName").ToString()
                        myUserInfo.OrganID = beUser.OrganID.Value
                        myUserInfo.OrganName = dtUser.Rows(0).Item("OrganName").ToString()

                        myUserInfo.GroupID = htGroupID
                    End Using
                End If
            End Using
        End If
    End Sub

    
End Class
