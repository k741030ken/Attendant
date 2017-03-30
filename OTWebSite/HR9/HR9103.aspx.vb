'****************************************************
'功能說明：HR9103人事處放行作業 - 扶養眷屬明細
'建立人員：Ann
'建立日期：2014.10.01
'****************************************************
Imports System.Data
Imports System.Data.Common

Partial Class HR_HR9103
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            Dim objSC As New SC
            Dim objHR9100 As New HR9100
        End If
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

            If ht.ContainsKey("SelectedEmpID") Then
                ViewState.Item("CompID") = ht("SelectedCompID").ToString()
                ViewState.Item("EmpID") = ht("SelectedEmpID").ToString()
                GetData(ht("SelectedCompID").ToString(), ht("SelectedEmpID").ToString())
            Else
                Return
            End If
        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param            
            Case "btnActionX"
                GoBack()
        End Select
    End Sub

    Private Sub GetData(ByVal CompID As String, ByVal EmpID As String)
        Dim bsPersonalWait As New bePersonalWait.Service()
        Dim bePersonalWait As New bePersonalWait.Row()
        Dim strSQL As New StringBuilder    '扶養親屬
        Try
            strSQL.AppendLine(" Select T.ReleaseMark, T.RelativeIDNo ")
            strSQL.AppendLine(" , T.Address ")
            strSQL.AppendLine(" , F.NameN as Name ")
            strSQL.AppendLine(" , F.RelativeID ")
            strSQL.AppendLine(" , convert(char(10),F.BirthDate,111) as BirthDate ")
            strSQL.AppendLine(" , case when isnull(T.Remark,'') = '' then R.Remark else T.Remark end Remark ")
            strSQL.AppendLine(" , T.ReasonID ")
            strSQL.AppendLine(" , R.TaxFamilyID ")
            strSQL.AppendLine(" from TaxFamily T ")
            strSQL.AppendLine("  left join Personal P ")
            strSQL.AppendLine("  on T.CompID = P.CompID and T.EmpID = P.EmpID ")
            strSQL.AppendLine("  left join Family F ")
            strSQL.AppendLine("  on T.RelativeIDNo = F.RelativeIDNo and P.IDNo = F.IDNo ")
            strSQL.AppendLine("  left join Relationship R ")
            strSQL.AppendLine("  on F.RelativeID = R.RelativeID ")
            strSQL.AppendLine("  where T.EmpID = '" & EmpID & "' ")
            strSQL.AppendLine("  and T.CompID ='" & CompID & "' ")
            strSQL.AppendLine("  and Rtrim(R.TaxFamilyID) <> '' ")
            strSQL.AppendLine("  order by T.ReleaseMark, R.TaxFamilyID, F.RelativeID ")
            pcMain.DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & "subGetData", ex)
        End Try

    End Sub

    Private Sub GoBack()
        Dim ti As TransferInfo = Me.StateTransfer
        Me.TransferFramePage(ti.CallerUrl, Nothing, ti.Args)
    End Sub
    'Me.TransferFramePage(ti.CallerUrl, Nothing, _
    '                      "chkReason=True", _
    '                     "ddlReason=" & ViewState.Item("Reason"), _
    '                     "SelectCompID=" & ViewState.Item("CompID"), _
    '                     "SelectEmpID=" & ViewState.Item("EmpID"), _
    '                     "PageNo=1", _
    '                     "DoQuery=Y")
    Protected Sub gvMain_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvMain.RowDataBound
        Dim bePersonalWait As New bePersonalWait.Row()
        Dim bsPersonalWait As New bePersonalWait.Service()
        Dim objSC As New SC
        Dim objHR9100 As New HR9100
        Dim strSQL As New StringBuilder

        If e.Row.RowType = DataControlRowType.DataRow Then
            If DirectCast(gvMain.DataSource, DataTable).Rows(e.Row.RowIndex)("ReleaseMark").ToString() = "0" Then
                e.Row.Cells(0).Text = "申請中"
            Else
                e.Row.Cells(0).Text = "已放行"
            End If

            If DirectCast(gvMain.DataSource, DataTable).Rows(e.Row.RowIndex)("RelativeID").ToString() <> "" Then    '異動前
                Dim strRelativeID As String = DirectCast(gvMain.DataSource, DataTable).Rows(e.Row.RowIndex)("RelativeID").ToString()
                strSQL.AppendLine(" Select Remark from Relationship where RelativeID = '" & strRelativeID & "' ")

                Dim strRelative As String = Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")
                If strRelative <> "" Then
                    e.Row.Cells(2).Text = strRelative
                End If
            End If
        End If
    End Sub
End Class
