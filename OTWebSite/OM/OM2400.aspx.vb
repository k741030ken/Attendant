
Partial Class OM_OM2400
    Inherits PageBase
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)
            If ht.ContainsKey("SelectedCompID") Then
                ViewState.Item("CompID") = ht("SelectedCompID").ToString()
                ViewState.Item("OrganID") = ht("SelectedOrganID").ToString()

                DoQuery()
            Else
                Return
            End If
        End If
    End Sub

    Private Sub DoQuery() '查詢
        Dim objOM As New OM2()
        Dim strCompID As String
        strCompID = UserProfile.SelectCompRoleID

        Try
            pcMain.DataTable = objOM.OM2000QueryOrganizationFlowReportLine(
            ViewState.Item("CompID"), _
            ViewState.Item("OrganID"))

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
        End Try
    End Sub
End Class
