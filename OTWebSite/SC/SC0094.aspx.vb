
Partial Class SC_SC0094
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtID.Attributes.Add("onkeypress", "funKeyPress();")
        txtPassword.Attributes.Add("onkeypress", "funKeyPress();")
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If ti.Args.Length > 0 Then
            Dim strConnStr As String = ti.Args(0)
            If strConnStr <> "" Then
                Dim aryConn() As String = Split(strConnStr, ";")

                For intLoop As Integer = 0 To aryConn.GetUpperBound(0)
                    If aryConn(intLoop).ToString().ToUpper().IndexOf("SERVER") >= 0 Then
                        txtServer.Text = Replace(aryConn(intLoop).ToString(), "server=", "").Trim()
                    ElseIf aryConn(intLoop).ToString().ToUpper().IndexOf("DATABASE") >= 0 Then
                        txtDB.Text = Replace(aryConn(intLoop).ToString(), "database=", "").Trim()
                    ElseIf aryConn(intLoop).ToString().ToUpper().IndexOf("UID") >= 0 Then
                        txtID.Text = Replace(aryConn(intLoop).ToString(), "uid=", "").Trim()
                    ElseIf aryConn(intLoop).ToString().ToUpper().IndexOf("PWD") >= 0 Then
                        txtPassword.Text = Replace(aryConn(intLoop).ToString(), "pwd=", "").Trim()
                    End If
                Next
            End If
        End If
    End Sub
End Class
