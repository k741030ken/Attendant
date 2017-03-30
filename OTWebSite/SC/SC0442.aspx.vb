'****************************************************
'功能說明：信用風險額度維護
'建立人員：Tsao
'建立日期：2014/03/10
'****************************************************
Imports System.Data
Imports System.Data.Common

Partial Class SC_SC0442
    Inherits PageBase

    Protected Overrides Sub BaseOnPageCall(ByVal ti As TransferInfo)
        If Not IsPostBack Then
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)
            tbxYear.Text = ht("Year")
            ViewState("Type") = ht("Type")
            ViewState("Rank") = ht("Rank")
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
        ddlType.SelectedValue = ViewState("Type")
        ddlRank.SelectedValue = ViewState("Rank")

        tbxRankLimit.Text = Bsp.DB.ExecuteScalar("SELECT RankLimit FROM SC_RankLimit WHERE Year = " & Bsp.Utility.Quote(tbxYear.Text) & " AND Type = " & Bsp.Utility.Quote(ViewState("Type")) & " AND RANK = " & Bsp.Utility.Quote(ViewState("Rank")))
    End Sub

    Private Function CheckData() As Boolean
        'Dim strSQL As New StringBuilder
        'strSQL.AppendLine("SELECT COUNT(*) FROM SC_RankLimit WHERE Year = " & Bsp.Utility.Quote(tbxYear.Text.Trim()) & " AND Type = " & Bsp.Utility.Quote(ddlType.SelectedValue) & " AND Rank = " & Bsp.Utility.Quote(ddlRank.SelectedValue))

        'If Convert.ToInt32(Bsp.DB.ExecuteScalar(strSQL.ToString())) > 0 Then
        '    Bsp.Utility.ShowMessage(Me, "已有相同的信用風險額度设定!")
        '    Return False
        'End If

        Return True
    End Function

    Private Sub SaveData()
        Dim strSQL As New StringBuilder

        Using cn As DbConnection = Bsp.DB.getConnection()
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction()
            Try
                strSQL.AppendLine("UPDATE SC_RankLimit")
                strSQL.AppendLine("SET RankLimit = " & tbxRankLimit.Text.Trim())
                strSQL.AppendLine("  , LastChgID = " & Bsp.Utility.Quote(UserProfile.ActUserID))
                strSQL.AppendLine("  , LastChgDate = GETDATE()")
                strSQL.AppendLine("WHERE Year = " & Bsp.Utility.Quote(tbxYear.Text))
                strSQL.AppendLine("  AND Type = " & Bsp.Utility.Quote(ddlType.SelectedValue))
                strSQL.AppendLine("  AND Rank = " & Bsp.Utility.Quote(ddlRank.SelectedValue))

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
