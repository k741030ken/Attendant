'****************************************************
'功能說明：員工企業團中斷年資維護-新增
'建立人員：Beatrice
'建立日期：2016.03.22
'****************************************************
Imports System.Data
Imports Newtonsoft.Json
Partial Class ST_ST1902
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then

        End If
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim objST As New ST1
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

            '公司代碼
            If ht.ContainsKey("SelectedCompID") Then
                ViewState.Item("CompID") = ht("CompID").ToString()
                ViewState.Item("EmpID") = ht("EmpID").ToString()
                ViewState.Item("IDNo") = ht("SelectedIDNo").ToString()
                ViewState.Item("Seq") = ht("Seq").ToString()

                txtCompID.Text = ViewState.Item("CompID") + "-" + objST.QueryData("Company", "And CompID = " & Bsp.Utility.Quote(ViewState.Item("CompID")), "CompName")
                txtEmpID.Text = ViewState.Item("EmpID")
                txtEmpName.Text = objST.QueryData("Personal", "And CompID = " & Bsp.Utility.Quote(ViewState.Item("CompID")) & " And EmpID = " & Bsp.Utility.Quote(ViewState.Item("EmpID")), "NameN")

                subGetData(ViewState.Item("CompID"), ViewState.Item("EmpID"), ViewState.Item("Seq"))
            Else
                Return
            End If
        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnUpdate"   '存檔返回
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
        Dim beNotEmpLog As New beNotEmpLog.Row()
        Dim bsNotEmpLog As New beNotEmpLog.Service()
        Dim objST As New ST1
        '儲存資料
        beNotEmpLog.Seq.Value = ViewState.Item("Seq")
        beNotEmpLog.IDNo.Value = ViewState.Item("IDNo")
        beNotEmpLog.CompID.Value = ViewState.Item("CompID")
        beNotEmpLog.EmpID.Value = ViewState.Item("EmpID")
        beNotEmpLog.AdjustReason.Value = ddlAdjustReason.SelectedValue
        beNotEmpLog.PlusOrMinus.Value = ddlPlusOrMinus.SelectedValue
        beNotEmpLog.Remark.Value = txtRemark.Text
        beNotEmpLog.ValidDateB.Value = IIf(txtValidDateB.DateText = "", "1900/01/01", txtValidDateB.DateText)
        beNotEmpLog.ValidDateE.Value = IIf(txtValidDateE.DateText = "", "1900/01/01", txtValidDateE.DateText)
        beNotEmpLog.LastChgComp.Value = UserProfile.ActCompID
        beNotEmpLog.LastChgID.Value = UserProfile.ActUserID
        beNotEmpLog.LastChgDate.Value = Now

        Try
            Return objST.UpdateNotEmpLog_SPHOLD(beNotEmpLog)
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, "[CC0201_99]" & Bsp.Utility.getMessage("E_00000") & Bsp.Utility.getInnerException(Me.FunID, ex))
            Return False
        End Try
    End Function

    Private Sub subGetData(ByVal CompID As String, ByVal EmpID As String, ByVal Seq As String)
        Dim objST As New ST1
        Dim beNotEmpLog As New beNotEmpLog.Row()
        Dim bsNotEmpLog As New beNotEmpLog.Service()

        beNotEmpLog.CompID.Value = CompID
        beNotEmpLog.EmpID.Value = EmpID
        beNotEmpLog.Seq.Value = Seq

        Try
            Using dt As DataTable = bsNotEmpLog.QueryByKey(beNotEmpLog).Tables(0)
                If dt.Rows.Count <= 0 Then Exit Sub
                beNotEmpLog = New beNotEmpLog.Row(dt.Rows(0))

                '企業團年資調整原因
                ddlAdjustReason.SelectedValue = beNotEmpLog.AdjustReason.Value

                '企業團年資加減項
                ddlPlusOrMinus.SelectedValue = beNotEmpLog.PlusOrMinus.Value

                '中斷起日
                txtValidDateB.DateText = IIf(Format(beNotEmpLog.ValidDateB.Value, "yyyy/MM/dd") = "1900/01/01", "", Format(beNotEmpLog.ValidDateB.Value, "yyyy/MM/dd"))

                '中斷迄日
                txtValidDateE.DateText = IIf(Format(beNotEmpLog.ValidDateE.Value, "yyyy/MM/dd") = "1900/01/01", "", Format(beNotEmpLog.ValidDateE.Value, "yyyy/MM/dd"))

                '備註
                txtRemark.Text = beNotEmpLog.Remark.Value

                '最後異動公司
                Dim CompName As String = objST.QueryData("Company", "And CompID = " & Bsp.Utility.Quote(beNotEmpLog.LastChgComp.Value), "CompName")
                txtLastChgComp.Text = beNotEmpLog.LastChgComp.Value + IIf(CompName <> "", "-" + CompName, "")

                '最後異動人員
                Dim UserName As String = objST.QueryData("Personal", "And CompID = " & Bsp.Utility.Quote(beNotEmpLog.LastChgComp.Value) & " And EmpID = " & Bsp.Utility.Quote(beNotEmpLog.LastChgID.Value), "NameN")
                txtLastChgID.Text = beNotEmpLog.LastChgID.Value + IIf(UserName <> "", "-" + UserName, "")

                '最後異動日期
                Dim boolDate As Boolean = Format(beNotEmpLog.LastChgDate.Value, "yyyy/MM/dd") = "1900/01/01"
                txtLastChgDate.Text = IIf(boolDate, "", beNotEmpLog.LastChgDate.Value.ToString("yyyy/MM/dd HH:mm:ss"))

            End Using
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & "subGetData", ex)
        End Try

    End Sub

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
        subGetData(ViewState.Item("CompID"), ViewState.Item("EmpID"), ViewState.Item("Seq"))
    End Sub
End Class
