'****************************************************
'功能說明：信用風險額度維護
'建立人員：Tsao
'建立日期：2014/03/10
'****************************************************
Imports System.Data
Imports System.Data.Common

Partial Class SC_SC0441
    Inherits PageBase

    Protected Overrides Sub BaseOnPageCall(ByVal ti As TransferInfo)
        If Not IsPostBack Then

        End If
    End Sub
    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If Not IsPostBack Then
            BindData()
        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnAdd"
                If CheckData() Then
                    SaveData()
                End If
            Case "btnUpdate"
                If CheckData() Then
                    SaveData()
                End If
            Case "btnActionX"
                GoBack()
        End Select
    End Sub

    Private Sub GoBack()
        Bsp.Utility.RunClientScript(Me, "window.top.close();")
    End Sub

    Private Sub BindData()
        Bsp.Utility.FillCommon(Me.ddlType, "743", Bsp.Enums.SelectCommonType.Valid, Bsp.Enums.FullNameType.OnlyDefine)
    End Sub

    Private Function CheckData() As Boolean
        Dim strSQL As New StringBuilder
        strSQL.AppendLine("SELECT COUNT(*) FROM SC_RankLimit WHERE Year = " & Bsp.Utility.Quote(tbxYear.Text.Trim()) & " AND Type = " & Bsp.Utility.Quote(ddlType.SelectedValue) & " AND Rank = " & Bsp.Utility.Quote(ddlRank.SelectedValue))

        If Convert.ToInt32(Bsp.DB.ExecuteScalar(strSQL.ToString())) > 0 Then
            Bsp.Utility.ShowMessage(Me, "已有相同的信用風險額度設定!")
            Return False
        End If

        Return True
    End Function

    Private Sub SaveData()
        Dim strSQL As New StringBuilder

        Using cn As DbConnection = Bsp.DB.getConnection()
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction()
            Try
                strSQL.AppendLine("INSERT SC_RankLimit(Year,Type,Rank,RankLimit,CreateDate,LastChgID,LastChgDate)")
                strSQL.AppendLine("SELECT " & Bsp.Utility.Quote(tbxYear.Text))
                strSQL.AppendLine("     , " & Bsp.Utility.Quote(ddlType.SelectedValue))
                strSQL.AppendLine("     , " & Bsp.Utility.Quote(ddlRank.SelectedValue))
                strSQL.AppendLine("     , " & tbxRankLimit.Text.Trim())
                strSQL.AppendLine("     , GETDATE()")
                strSQL.AppendLine("     , " & Bsp.Utility.Quote(UserProfile.ActUserID))
                strSQL.AppendLine("     , GETDATE()")

                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)

                tran.Commit()
                GoBack()
            Catch ex As Exception
                tran.Rollback()
                Bsp.Utility.ShowMessage(Me, ex.Message)
            Finally
                tran.Dispose()
                cn.Close()
            End Try
        End Using
    End Sub

End Class
