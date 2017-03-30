'****************************************************
'功能說明：員工中斷年資維護-新增
'建立人員：John Lin
'建立日期：2016.01.22
'****************************************************
Imports System.Data
Imports Newtonsoft.Json

Partial Class ST_ST1901
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            '下拉式選單
            ST1.FillDDL(ddlNotEmpReasonID, "EmployeeReason", "Rtrim(Reason)", "Rtrim(Remark)", Bsp.Enums.DisplayType.Full, "AND NotEmpLogFlag = '1' ", "")
            ddlNotEmpReasonID.Items.Insert(0, New ListItem("---請選擇---", ""))
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
        Dim beNotEmpLog As New beNotEmpLog.Row()
        Dim bsNotEmpLog As New beNotEmpLog.Service()

        beNotEmpLog.CompID.Value = ViewState.Item("CompID")
        beNotEmpLog.EmpID.Value = ViewState.Item("EmpID")
        beNotEmpLog.NotEmpReasonID.Value = ddlNotEmpReasonID.SelectedValue
        beNotEmpLog.Seq.Value = objST.QueryMaxSeq(ViewState.Item("CompID"), ViewState.Item("EmpID"))
        beNotEmpLog.IDNo.Value = ViewState.Item("IDNo")
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

        '儲存資料
        Try
            Return objST.AddNotEmpLogSetting(beNotEmpLog)
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, "[CC0201_99]" & Bsp.Utility.getMessage("E_00000") & Bsp.Utility.getInnerException(Me.FunID, ex))
            Return False
        End Try
    End Function

    Private Function funCheckData() As Boolean
        Dim objHR As New HR
        Dim objST As New ST1
        Dim beNotEmpLog As New beNotEmpLog.Row()
        Dim bsNotEmpLog As New beNotEmpLog.Service()

        '中斷原因不可為空白
        If ddlNotEmpReasonID.SelectedValue = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblNotEmpReasonID.Text)
            ddlNotEmpReasonID.Focus()
            Return False
        End If

        '起日不可為空白
        If txtValidDateB.DateText = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblValidDateB.Text)
            txtValidDateB.Focus()
            Return False
        End If

        '起日時間格式判斷
        If Bsp.Utility.CheckDate(txtValidDateB.DateText) = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00070", lblValidDateB.Text)
            txtValidDateB.Focus()
            Return False
        End If

        '迄日不可為空白
        If txtValidDateE.DateText = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblValidDateE.Text)
            txtValidDateE.Focus()
            Return False
        End If

        '迄日時間格式判斷
        If Bsp.Utility.CheckDate(txtValidDateE.DateText) = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00070", lblValidDateE.Text)
            txtValidDateE.Focus()
            Return False

        End If

        '起日不可大於迄日
        If txtValidDateB.DateText > txtValidDateE.DateText Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00130")
            txtValidDateB.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub ClearData()
        ddlNotEmpReasonID.SelectedIndex = 0
        txtValidDateB.DateText = ""
        txtValidDateE.DateText = ""
        txtRemark.Text = ""
    End Sub

End Class
