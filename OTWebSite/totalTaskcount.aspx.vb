'************************************************************
'功能說明：計算人資待辦數字
'建立人員：Weicheng
'建立日期：2015.05.15
'************************************************************
Imports System.Data
Imports System.Security.Cryptography

Partial Class totalTaskcount
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objHR As New HR

        Dim vLoginCompID As String = ""
        Dim vLogonEmpID As String = ""
        Dim vResult As String = ""
        Dim vCnt As Integer = 0


        If Request("UserID") Is Nothing Then
            vCnt = -1
        Else
            vLoginCompID = Request("CompID").ToString
            vLogonEmpID = Request("UserID").ToString
            If vLogonEmpID = "" Then
                vCnt = -1
            Else
                Try
                    vCnt = objHR.SignCount(vLoginCompID, vLogonEmpID)
                Catch ex As Exception
                    vCnt = -1
                End Try

            End If
        End If
        Response.Clear()
        Response.Write(vCnt)
        Response.End()


    End Sub
End Class
