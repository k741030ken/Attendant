'****************************************************
'功能說明：員工企業團中斷年資維護-新增
'建立人員：Beatrice
'建立日期：2016.03.22
'****************************************************
Imports System.Data
Imports Newtonsoft.Json

Partial Class ST_ST1901
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then

        End If
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim objSC As New SC
            Dim objST As New ST1
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

            '公司代碼
            If ht.ContainsKey("SelectedCompID") Then
                ViewState.Item("CompID") = ht("SelectedCompID").ToString()
                ViewState.Item("EmpID") = ht("SelectedEmpID").ToString()
                ViewState.Item("IDNo") = ht("SelectedIDNo").ToString()

                txtCompID.Text = ht("SelectedCompID").ToString() + "-" + ht("SelectedCompName").ToString()
                txtEmpID.Text = ht("SelectedEmpID").ToString()
                txtEmpName.Text = ht("SelectedEmpName").ToString()
            Else
                Return
            End If
        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnAdd"   '存檔返回
                If funCheckData() Then
                    If SaveData() Then
                        GoBack()
                    End If
                End If
            Case "btnActionX"   '返回
                GoBack()
            Case "btnCancel"    '清除
                ClearData()
        End Select
    End Sub

    Private Sub GoBack()
        Dim ti As TransferInfo = Me.StateTransfer
        Me.TransferFramePage(ti.CallerUrl, Nothing, ti.Args)
    End Sub

    Private Function SaveData() As Boolean
        Dim objST As New ST1
        Dim bsNotEmpLog As New beNotEmpLog.Service()
        Dim beNotEmpLogRows() As beNotEmpLog.Row = Nothing
        Dim beNotEmpLog As New beNotEmpLog.Row()

        beNotEmpLog.Seq.Value = objST.QueryMaxSeq(ViewState.Item("CompID"), ViewState.Item("EmpID"))
        beNotEmpLog.CompID.Value = ViewState.Item("CompID")
        beNotEmpLog.EmpID.Value = ViewState.Item("EmpID")
        beNotEmpLog.IDNo.Value = ViewState.Item("IDNo")
        beNotEmpLog.AdjustReason.Value = ddlAdjustReason.SelectedValue
        beNotEmpLog.PlusOrMinus.Value = ddlPlusOrMinus.SelectedValue
        beNotEmpLog.CompID_ALL.Value = "ALL"
        beNotEmpLog.Remark.Value = txtRemark.Text
        beNotEmpLog.ValidDateB.Value = IIf(txtValidDateB.DateText = "", "1900/01/01", txtValidDateB.DateText)
        beNotEmpLog.ValidDateE.Value = IIf(txtValidDateE.DateText = "", "1900/01/01", txtValidDateE.DateText)
        beNotEmpLog.LastChgComp.Value = UserProfile.ActCompID
        beNotEmpLog.LastChgID.Value = UserProfile.ActUserID
        beNotEmpLog.LastChgDate.Value = Now

        '檢查資料是否存在
        If bsNotEmpLog.IsDataExists(beNotEmpLog) Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00010", "")
            Return False
        End If

        ReDim Preserve beNotEmpLogRows(0)
        beNotEmpLogRows(0) = beNotEmpLog

        If ddlAdjustReason.SelectedValue = "1" Then
            beNotEmpLog = New beNotEmpLog.Row()
            beNotEmpLog.Seq.Value = objST.QueryMaxSeq(ViewState.Item("CompID"), ViewState.Item("EmpID")) + 1
            beNotEmpLog.CompID.Value = ViewState.Item("CompID")
            beNotEmpLog.EmpID.Value = ViewState.Item("EmpID")
            beNotEmpLog.IDNo.Value = ViewState.Item("IDNo")
            beNotEmpLog.AdjustReason.Value = "2"
            beNotEmpLog.PlusOrMinus.Value = ddlPlusOrMinus.SelectedValue
            beNotEmpLog.CompID_ALL.Value = "ALL"
            beNotEmpLog.Remark.Value = txtRemark.Text
            beNotEmpLog.ValidDateB.Value = IIf(txtValidDateB.DateText = "", "1900/01/01", txtValidDateB.DateText)
            beNotEmpLog.ValidDateE.Value = IIf(txtValidDateE.DateText = "", "1900/01/01", txtValidDateE.DateText)
            beNotEmpLog.LastChgComp.Value = UserProfile.ActCompID
            beNotEmpLog.LastChgID.Value = UserProfile.ActUserID
            beNotEmpLog.LastChgDate.Value = Now

            '檢查資料是否存在
            If bsNotEmpLog.IsDataExists(beNotEmpLog) Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00010", "")
                Return False
            End If

            ReDim Preserve beNotEmpLogRows(1)
            beNotEmpLogRows(1) = beNotEmpLog
        End If

        '儲存資料
        Try
            Return objST.AddNotEmpLog_SPHOLD(beNotEmpLogRows)
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, "[CC0201_99]" & Bsp.Utility.getMessage("E_00000") & Bsp.Utility.getInnerException(Me.FunID, ex))
            Return False
        End Try
    End Function

    Private Function funCheckData() As Boolean
        '企業團年資調整原因
        If ddlAdjustReason.SelectedValue = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblAdjustReason.Text)
            ddlAdjustReason.Focus()
            Return False
        End If

        '企業團年資加減項
        If ddlPlusOrMinus.SelectedValue = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblPlusOrMinus.Text)
            ddlPlusOrMinus.Focus()
            Return False
        End If

        '起日
        If txtValidDateB.DateText = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblValidDateB.Text)
            txtValidDateB.Focus()
            Return False
        End If

        If Bsp.Utility.CheckDate(txtValidDateB.DateText) = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00070", lblValidDateB.Text)
            txtValidDateB.Focus()
            Return False
        End If

        '迄日
        If txtValidDateE.DateText = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblValidDateE.Text)
            txtValidDateE.Focus()
            Return False
        End If

        If Bsp.Utility.CheckDate(txtValidDateE.DateText) = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00070", lblValidDateE.Text)
            txtValidDateE.Focus()
            Return False

        End If

        '起迄日
        If txtValidDateB.DateText > txtValidDateE.DateText Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00130")
            txtValidDateB.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub ClearData()
        ddlAdjustReason.SelectedIndex = 0
        ddlPlusOrMinus.SelectedIndex = 0
        txtValidDateB.DateText = ""
        txtValidDateE.DateText = ""
        txtRemark.Text = ""
    End Sub

End Class
