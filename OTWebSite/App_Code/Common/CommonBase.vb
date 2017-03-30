'****************************************************
'功能說明：通用底層
'建立人員：A02976
'建立日期：2007.03.10
'****************************************************
Imports Microsoft.VisualBasic

Public Class CommonBase
    Inherits System.Web.UI.Page
    Protected Const CommonStateName As String = "CommonState"
    Protected Const PageCollectName As String = "PageCollect"
    Private _CommonState As BaseState

    '目前的語系
    Public Property myUICulture() As String
        Get
            If Session("myUICulture") Is Nothing Then
                Session("myUICulture") = "zh-TW"
            End If
            Return Session("myUICulture")
        End Get
        Set(ByVal value As String)
            Session("myUICulture") = value
        End Set
    End Property

    '紀錄目前使用過的PageSessionID
    Protected Property PageCollect() As Collection
        Get
            If CommonState(PageCollectName) Is Nothing Then
                CommonState(PageCollectName) = New Collection
            End If
            Return CType(CommonState(PageCollectName), Collection)
        End Get
        Set(ByVal value As Collection)
            If value Is Nothing Then
                CommonState.Remove(PageCollectName)
            Else
                CommonState(PageCollectName) = value
            End If
        End Set
    End Property

    '底層使用,便於回收Session空間
    Protected ReadOnly Property CommonState() As BaseState
        Get
            If _CommonState Is Nothing Then
                _CommonState = New BaseState(CommonStateName)
            End If
            Return _CommonState
        End Get
    End Property

    Protected Overrides Sub OnPreInit(ByVal e As System.EventArgs)
        If Page.Request.ServerVariables("http_user_agent").ToLower().Contains("safari") Then
            Page.ClientTarget = "uplevel"
        End If
    End Sub

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        If Not Response.IsClientConnected Then
            Response.Clear()
            Response.End()
        End If
        If Session(Bsp.MySettings.UserProfileSessionName) Is Nothing Then
            UserProfile.GetLastLoginInfo()
            If Session(Bsp.MySettings.UserProfileSessionName) Is Nothing Then
                Dim strPage As String = Bsp.Utility.getMessagePageURL("CommonBase", Bsp.Enums.MessageType.Errors, "連接逾時，請重新登入系統", "回登入頁", "~/Default.aspx")
                Bsp.Utility.RunClientScript(Me, "window.top.location = '" & ResolveUrl(strPage) & "';")
                Return
            End If
        End If

        '註冊ClientFun()
        Dim Script As String

        'If Not Page.ClientScript.IsClientScriptBlockRegistered("StyleSheet") Then
        '    Script = "<link type=""text/css"" href=""../StyleSheet.css"" rel=""stylesheet"" />"
        '    Page.ClientScript.RegisterClientScriptBlock(GetType(Page), "StyleSheet", Script)
        'End If

        If Not Page.ClientScript.IsClientScriptIncludeRegistered("DisableRightClick") Then
            Script = "~/ClientFun/DisableRightClick.js"
            Page.ClientScript.RegisterClientScriptInclude(GetType(Page), "DisableRightClick", ResolveUrl(Script))
        End If
        If Not Page.ClientScript.IsClientScriptIncludeRegistered("ClientFun") Then
            Script = "~/ClientFun/ClientFun.js"
            Page.ClientScript.RegisterClientScriptInclude(GetType(Page), "ClientFun", ResolveUrl(Script))
        End If

        MyBase.OnLoad(e)
    End Sub

    Public Function getAlertMessage(ByVal msgKey As String) As String
        Return msgKey & "：" & GetGlobalResourceObject("Message", msgKey)
    End Function

    '將Session值清除
    Protected Sub ClearSession()
        Dim colPageID As Collection = PageCollect
        Dim colSessionID As New Collection

        For intLoop As Integer = 1 To colPageID.Count
            CommonState.ClearSession(Session, CType(colPageID(intLoop), String), True)
        Next
        '多加清除非必要的Session
        For Each strKey As String In Session.Keys
            Select Case strKey
                Case Bsp.MySettings.UserProfileSessionName, "CommonState:PageCollect"
                Case Else
                    '開頭為sys_, 表示保留不刪除...
                    If strKey.Length > 4 AndAlso strKey.Substring(0, 4).ToLower() = "sys_" Then
                        Continue For
                    End If
                    colSessionID.Add(strKey)
            End Select
        Next
        For intLoop As Integer = 1 To colSessionID.Count
            Session.Remove(colSessionID(intLoop))
        Next
        PageCollect = Nothing
    End Sub

End Class
