'**************************************************************
'功能說明：寫入Client端的訊息給Server並檢查是否有需要顯示的訊息
'建立人員：Chung
'建立日期：2011.05.13
'**************************************************************
Imports System.Data
Imports System.Data.Common

Partial Class SC_SC0001
    Inherits PageBase

    Protected Sub btnResponse_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnResponse.Click
        responseClick()
    End Sub

    Private Sub responseClick()
        Dim strPort As String = Request.Url.Port.ToString()

        'If strPort = "8080" OrElse strPort = "80" Then
        '    If Request.Url.Host.ToUpper().IndexOf("COCREDIT-") >= 0 Then
        '        strPort &= "." & Request.Url.Host.ToUpper().Replace("COCREDIT-", "").Replace(".BSP", "")
        '    End If
        'End If
        Try
            Bsp.DB.ExecuteNonQuery(CommandType.StoredProcedure, "SP_SC0001_1", New DbParameter() { _
                                    Bsp.DB.getDbParameter("@argIP", Request.ServerVariables("REMOTE_ADDR")), _
                                    Bsp.DB.getDbParameter("@argSite", strPort), _
                                    Bsp.DB.getDbParameter("@argUserID", UserProfile.ActUserID), _
                                    Bsp.DB.getDbParameter("@argActAs", UserProfile.UserID)})

            Dim strMsg As String = funGetMessage()
            If strMsg <> "" Then
                Bsp.Utility.RunClientScript(Me, "funShowMessage('" & Server.UrlEncode(strMsg) & "');")
            End If
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, "btnResponse_Click", ex)
        End Try
    End Sub

    Private Function funGetMessage() As String
        Dim strMsg As String = ""

        Using dt As Data.DataTable = Bsp.DB.ExecuteDataSet(Data.CommandType.StoredProcedure, "SP_SC0001_3", _
                            New DbParameter() { _
                            Bsp.DB.getDbParameter("@argIP", Request.ServerVariables("REMOTE_ADDR")), _
                            Bsp.DB.getDbParameter("@argUserID", UserProfile.ActUserID)}).Tables(0)

            If dt.Rows.Count > 0 Then
                strMsg = dt.Rows(0).Item(0).ToString()
            End If
        End Using

        Return strMsg
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            responseClick()
        End If
    End Sub
End Class
