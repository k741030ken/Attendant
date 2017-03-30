'****************************************************
'功能說明：Menu
'建立人員：Chung
'建立日期：2011.05.13
'****************************************************
Imports System.Data.SqlClient
Imports System.Data
Imports System.Security.Cryptography

Partial Class GS_GS0030
    Inherits CommonBase

    Implements System.Web.UI.ICallbackEventHandler

    Dim _callBackResult As String

    Public Function GetCallbackResult() As String Implements System.Web.UI.ICallbackEventHandler.GetCallbackResult
        Return _callBackResult
    End Function

    Public Sub RaiseCallbackEvent(ByVal eventArgument As String) Implements System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent
        Try
            _callBackResult = eventArgument.Split(",")(0) & "," & eventArgument.Split(",")(1) & "," & eventArgument.Split(",")(2)
        Catch ex As Exception
            _callBackResult = ""
        End Try
        MyBase.ClearSession()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            If Session("sys_LoginFrom") = "Portal-CC" Then
                Session.Abandon()
                Return
            End If
            'call back 呼叫處理
            Dim eventScript As String = Me.ClientScript.GetCallbackEventReference(Me, "Path", "redirectPage", "")

            'Dim strScript As String

            'strScript = "  function callPage(Path)"
            'strScript &= "  {"
            'strScript &= "      " & eventScript
            'strScript &= "  }"

            'If Not Page.ClientScript.IsClientScriptBlockRegistered(Me.GetType(), "callPage") Then
            '    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "callPage", strScript, True)
            'End If

            Dim strScript1 As String = ""
            strScript1 = "window.onload = function () {redirectPage('GS0040," & ResolveUrl("~/GS/GS0040.aspx") & "," & UserProfile.SelectCompRoleID & "');};"
            'If Session.Item("PageSource") IsNot Nothing AndAlso Session.Item("PageSource").ToString().Equals("GS1000") Then
            '    strScript1 = "window.onload = function () {redirectPage('GS0040,/GS/GS0040.aspx," & UserProfile.CompID & "');};"

            'End If

            If Not Page.ClientScript.IsClientScriptBlockRegistered(Me.GetType(), "WindowOnload") Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "WindowOnload", strScript1, True)
            End If

        End If
    End Sub

End Class
