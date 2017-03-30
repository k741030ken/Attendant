'****************************************************
'功能說明：Flow menu轉址介面
'建立人員：Chung
'建立日期：2013/01/29
'****************************************************
Imports System.Data

Partial Class WF_WFA012
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Params(Request.QueryString.Count) As Object
        Dim intCount As Integer = 0
        Dim RedirectTo As String = ""
        Dim bolExitFlow As Boolean = False

        For Each Key As String In Request.QueryString
            Select Case Key
                Case "LinkPage"
                    RedirectTo = Request.QueryString(Key)
                    Array.Resize(Params, Params.Length - 2)
                Case "ExitFlow" '表示為返回查詢頁
                    bolExitFlow = True
                Case Else
                    Params(intCount) = String.Format("{0}={1}", Key, Request.QueryString(Key))
                    intCount += 1
            End Select
        Next

        If RedirectTo <> "" Then
            If bolExitFlow Then
                'Bsp.Utility.RunClientScript(Me, "WFStepCloseMenu();")
                '返回查詢頁,則帶回原參數
                If Session("sys_ReArgs") IsNot Nothing Then ' AndAlso _
                    If TypeOf Session("sys_ReArgs") Is Object() Then
                        TransferFramePage(RedirectTo, Nothing, CType(Session("sys_ReArgs"), Object()))
                    ElseIf TypeOf Session("sys_ReArgs") Is Object Then
                        TransferFramePage(RedirectTo, Nothing, CType(Session("sys_ReArgs"), Object))
                    Else
                        TransferFramePage(RedirectTo, Nothing, Params)
                    End If
                    Session.Remove("sys_ReArgs")
                Else
                    TransferFramePage(RedirectTo, Nothing, Params)
                End If
            Else
                TransferFramePage(RedirectTo, Nothing, Params)
            End If
        Else
            Bsp.Utility.ShowMessage(Me, "[WFA012.Page_Load]：未傳入轉址頁！")
        End If

    End Sub

End Class
