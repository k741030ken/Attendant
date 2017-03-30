'****************************************************
'功能說明：流程明細查詢
'建立人員：Chung
'建立日期：2013/01/29
'****************************************************
Imports System.Data.SqlClient
Imports System.Data
Imports System.Data.Common

Partial Class WF_WFA030
    Inherits PageBase

    Protected Overrides Sub BaseOnPageCall(ByVal ti As TransferInfo)
        If Request("FlowID") Is Nothing Then
            Object2ViewState(ti.Args)
        Else
            ViewState.Item("FlowID") = Request("FlowID")
            ViewState.Item("FlowCaseID") = Request("FlowCaseID")
        End If
        Try
            Dim objWF As New WF()
            ViewState.Item("ReadAuth") = objWF.CheckWFListReadAuth(ViewState.Item("FlowCaseID"), UserProfile.UserID)
            Using dt As DataTable = objWF.GetFlowMaster(ViewState.Item("FlowID"), ViewState.Item("FlowCaseID"))
                DataListMaster.DataSource = dt
                DataListMaster.DataBind()
            End Using
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".PageLoad", ex)
        End Try
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Request("FlowID") Is Nothing Then
            Object2ViewState(ti.Args)
        Else
            ViewState.Item("FlowID") = Request("FlowID")
            ViewState.Item("FlowCaseID") = Request("FlowCaseID")
        End If
        Try
            Dim objWF As New WF()
            ViewState.Item("ReadAuth") = objWF.CheckWFListReadAuth(ViewState.Item("FlowCaseID"), UserProfile.UserID)
            Using dt As DataTable = objWF.GetFlowMaster(ViewState.Item("FlowID"), ViewState.Item("FlowCaseID"))
                DataListMaster.DataSource = dt
                DataListMaster.DataBind()
            End Using
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".PageLoad", ex)
        End Try
    End Sub

    Protected Sub DataListMaster_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles DataListMaster.ItemDataBound
        Dim objWF As New WF()
        Dim labApplyCaseDescLst As Label
        Dim labApplyNoLst As Label
        Dim labCustNmLst As Label
        Dim labNoteLst As Label
        Dim labFlowCaseDescLst As Label
        Dim FlowID As String
        Dim FlowCaseID As String
        Dim DataDetailLst As DataList
        Dim ReadAuth As String = "1"

        If Convert.ToBoolean(ViewState.Item("ReadAuth")) = False Then
            ReadAuth = "0"
        End If

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            labApplyCaseDescLst = e.Item.FindControl("labApplyCaseDesc")
            labApplyCaseDescLst.Text = "<font color='green'>[◎<B>" & DataBinder.Eval(e.Item.DataItem, "FlowName") & "</B> 申請編號：" & DataBinder.Eval(e.Item.DataItem, "KeyID") & "流程記錄◎]</font>"
            labFlowCaseDescLst = e.Item.FindControl("LabFlowCaseDesc")
            labFlowCaseDescLst.Text = " 流程資訊：" & DataBinder.Eval(e.Item.DataItem, "FlowID") & "-" & DataBinder.Eval(e.Item.DataItem, "FlowCaseID")
            labApplyNoLst = e.Item.FindControl("labApplyNo")
            labApplyNoLst.Text = DataBinder.Eval(e.Item.DataItem, "KeyID")
            labCustNmLst = e.Item.FindControl("labCustNm")
            labCustNmLst.Text = Bsp.Utility.IsStringNull(DataBinder.Eval(e.Item.DataItem, "CustomerName"))
            labNoteLst = e.Item.FindControl("labNote")
            labNoteLst.Text = Bsp.Utility.IsStringNull(DataBinder.Eval(e.Item.DataItem, "FlowNote"))
            FlowID = DataBinder.Eval(e.Item.DataItem, "FlowID")
            FlowCaseID = DataBinder.Eval(e.Item.DataItem, "FlowCaseID")
            DataDetailLst = e.Item.FindControl("DataDetail")

            Using dt As DataTable = objWF.GetFlowDetail(FlowID, FlowCaseID, ReadAuth)
                DataDetailLst.DataSource = dt
                DataDetailLst.DataBind()
            End Using
        End If
    End Sub

End Class
