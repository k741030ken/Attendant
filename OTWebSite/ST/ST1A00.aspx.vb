'****************************************************
'功能說明：員工年資紀錄查詢
'建立人員：Micky Sung
'建立日期：2015.07.13
'****************************************************
Imports System.Data

Partial Class ST_ST1A00
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnQuery"     '查詢
                ViewState.Item("DoQuery") = "Y"
                DoQuery()
        End Select
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)
            If ht.ContainsKey("SelectedCompID") Then
                ViewState.Item("CompID") = ht("SelectedCompID").ToString()
                ViewState.Item("CompName") = ht("SelectedCompName").ToString()
                ViewState.Item("EmpID") = ht("SelectedEmpID").ToString()
                ViewState.Item("EmpName") = ht("SelectedEmpName").ToString()
                ViewState.Item("IDNo") = ht("SelectedIDNo").ToString()

                lblCompRoleID.Text = ViewState.Item("CompID").ToString + "-" + ViewState.Item("CompName")
                txtEmpID.Text = ViewState.Item("EmpID").ToString
                txtEmpName.Text = ViewState.Item("EmpName").ToString
            Else
                Return
            End If
            DoQuery()
        End If
    End Sub

    Private Sub DoQuery()
        Dim objST As New ST1

        Try
            '企業團年資
            pcMain_EmpSenComp_SPHOLD.PerPageRecord = 5
            pcMain_EmpSenComp_SPHOLD.DataTable = objST.QueryEmpSenComp(
                 "EmpID=" & txtEmpID.Text)

            '公司年資
            pcMain_EmpSenComp.PerPageRecord = 5
            pcMain_EmpSenComp.DataTable = objST.QueryEmpSenComp(
                "CompID=" & ViewState.Item("CompID"), _
                "EmpID=" & txtEmpID.Text)

            '職等年資
            pcMain_EmpSenRank.PerPageRecord = 5
            pcMain_EmpSenRank.DataTable = objST.QueryEmpSenRank(
                "CompID=" & ViewState.Item("CompID"), _
                "EmpID=" & txtEmpID.Text)

            '單位年資
            pcMain_EmpSenOrgType.PerPageRecord = 5
            pcMain_EmpSenOrgType.DataTable = objST.QueryEmpSenOrgType(
                "CompID=" & ViewState.Item("CompID"), _
                "EmpID=" & txtEmpID.Text)

            '簽核單位年資
            pcMain_EmpSenOrgTypeFlow.PerPageRecord = 5
            pcMain_EmpSenOrgTypeFlow.DataTable = objST.QueryEmpSenOrgTypeFlow(
                "CompID=" & ViewState.Item("CompID"), _
                "EmpID=" & txtEmpID.Text)

            '工作性質年資
            pcMain_EmpSenWorkType.PerPageRecord = 5
            pcMain_EmpSenWorkType.DataTable = objST.QueryEmpSenWorkType(
                "CompID=" & ViewState.Item("CompID"), _
                "EmpID=" & txtEmpID.Text)

            '職位年資
            pcMain_EmpSenPosition.PerPageRecord = 5
            pcMain_EmpSenPosition.DataTable = objST.QueryEmpSenPosition(
                "CompID=" & ViewState.Item("CompID"), _
                "EmpID=" & txtEmpID.Text)

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
        End Try
    End Sub

End Class
