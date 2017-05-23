'****************************************************
'功能說明：流程種類參數設定
'建立人員：John
'建立日期：2017.05.03
'****************************************************
Imports System.Data
Partial Class ATWF_ATWF22
    Inherits PageBase
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            Dim objSC As New SC

            '國別
            ATWF20.FillFlowSN(ddlFlowSN)
            ddlFlowSN.Items.Insert(0, New ListItem("---請選擇---", ""))

            '20160419 wei add 分機長度
            ddlFlowTypeFlag.Items.Insert(0, New ListItem("---請選擇---", ""))
            ddlFlowTypeFlag.Items.Insert(1, New ListItem("無流程", "0"))
            ddlFlowTypeFlag.Items.Insert(2, New ListItem("有流程", "1"))
            ddlFlowTypeFlag.Items.Insert(3, New ListItem("指定特殊流程", "2"))
        End If
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

            If ht.ContainsKey("SelectedSystemID") Then
                ViewState.Item("SystemID") = ht("SelectedSystemID").ToString()
                ViewState.Item("FlowCode") = ht("SelectedFlowCode").ToString()
                ViewState.Item("FlowType") = ht("SelectedFlowType").ToString()
                subGetData(ht("SelectedSystemID").ToString(), ht("SelectedFlowCode").ToString(), ht("SelectedFlowType").ToString())
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
        Dim beHRFlowTypeDefine As New beHRFlowTypeDefine.Row()
        Dim bsHRFlowTypeDefine As New beHRFlowTypeDefine.Service()
        Dim objATWF20 As New ATWF20()

        '取得輸入資料
        beHRFlowTypeDefine.SystemID.Value = lblSystemIDtxt.Text
        beHRFlowTypeDefine.FlowCode.Value = lblFlowCodetxt.Text
        beHRFlowTypeDefine.FlowType.Value = lblFlowTypetxt.Text
        beHRFlowTypeDefine.FlowTypeName.Value = txtFlowTypeName.Text
        beHRFlowTypeDefine.FlowTypeDescription.Value = txtFlowTypeDescription.Text
        beHRFlowTypeDefine.FlowTypeFlag.Value = ddlFlowTypeFlag.SelectedValue
        beHRFlowTypeDefine.FlowSN.Value = ddlFlowSN.SelectedValue
        beHRFlowTypeDefine.LastChgComp.Value = UserProfile.ActCompID
        beHRFlowTypeDefine.LastChgID.Value = UserProfile.ActUserID
        beHRFlowTypeDefine.LastChgDate.Value = Now

        '儲存資料
        Try
            Return objATWF20.UpdateHRFlowTypeDefine(beHRFlowTypeDefine)
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, "[CC0201_99]" & Bsp.Utility.getMessage("E_00000") & Bsp.Utility.getInnerException(Me.FunID, ex))
            Return False
        End Try
    End Function

    Private Function funCheckData() As Boolean

        '流程類型名稱
        If txtFlowTypeName.Text = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblFlowTypeName.Text)
            txtFlowTypeName.Focus()
            Return False
        End If
        '流程識別碼
        If ddlFlowTypeFlag.SelectedValue = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00000", lblFlowTypeFlag.Text)
            ddlFlowTypeFlag.Focus()
            Return False
        End If
        '流程識別碼
        If ddlFlowTypeFlag.SelectedValue = "2" Then
            If ddlFlowSN.SelectedValue = "" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00000", lblFlowSN.Text)
                ddlFlowSN.Focus()
                Return False
            End If
        End If

        Return True
    End Function

    Private Sub subGetData(ByVal SystemID As String, ByVal FlowCode As String, ByVal FlowType As String)
        Dim objSC As New SC()
        Dim objATWF20 As New ATWF20()
        Dim beHRFlowTypeDefine As New beHRFlowTypeDefine.Row()
        Dim bsHRFlowTypeDefine As New beHRFlowTypeDefine.Service()

        beHRFlowTypeDefine.SystemID.Value = SystemID
        beHRFlowTypeDefine.FlowCode.Value = FlowCode
        beHRFlowTypeDefine.FlowType.Value = FlowType

        Try
            Using dt As DataTable = bsHRFlowTypeDefine.QueryByKey(beHRFlowTypeDefine).Tables(0)
                If dt.Rows.Count <= 0 Then Exit Sub
                beHRFlowTypeDefine = New beHRFlowTypeDefine.Row(dt.Rows(0))

                '出勤類別
                lblSystemIDtxt.Text = SystemID
                '流程代碼
                lblFlowCodetxt.Text = FlowCode
                '流程種類
                lblFlowTypetxt.Text = FlowType
                '流程類型名稱
                txtFlowTypeName.Text = beHRFlowTypeDefine.FlowTypeName.Value
                '流程類型說明
                txtFlowTypeDescription.Text = beHRFlowTypeDefine.FlowTypeDescription.Value
                '流程類型註記
                If FlowCode = "OT02" Then
                    ddlFlowTypeFlag.Items.Insert(4, New ListItem("HR流程", "3"))
                    ddlFlowTypeFlag.SelectedValue = beHRFlowTypeDefine.FlowTypeFlag.Value
                Else
                    ddlFlowTypeFlag.SelectedValue = beHRFlowTypeDefine.FlowTypeFlag.Value
                End If
                '流程識別碼
                ddlFlowSN.SelectedValue = beHRFlowTypeDefine.FlowSN.Value
                If ddlFlowTypeFlag.SelectedValue = "0" Or ddlFlowTypeFlag.SelectedValue = "1" Then
                    ddlFlowSN.Enabled = False
                End If
                '最後異動公司
                If beHRFlowTypeDefine.LastChgComp.Value.Trim <> "" Then
                    lblLastChgComp.Text = beHRFlowTypeDefine.LastChgComp.Value + "-" + objSC.GetCompName(beHRFlowTypeDefine.LastChgComp.Value).Rows(0).Item("CompName").ToString
                Else
                    lblLastChgComp.Text = ""
                End If
                '最後異動人員
                If beHRFlowTypeDefine.LastChgID.Value.Trim <> "" Then
                    Dim UserName As String = objSC.GetSC_UserName(beHRFlowTypeDefine.LastChgComp.Value, beHRFlowTypeDefine.LastChgID.Value)
                    lblLastChgID.Text = beHRFlowTypeDefine.LastChgID.Value + IIf(UserName <> "", "-" + UserName, "")
                Else
                    lblLastChgID.Text = ""
                End If
                '最後異動日期
                lblLastChgDate.Text = IIf(Format(beHRFlowTypeDefine.LastChgDate.Value, "yyyy/MM/dd") = "1900/01/01", "", beHRFlowTypeDefine.LastChgDate.Value.ToString("yyyy/MM/dd HH:mm:ss"))

            End Using
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & "subGetData", ex)
        End Try
    End Sub

    Private Sub ClearData()
        subGetData(lblSystemIDtxt.Text, lblFlowCodetxt.Text, lblFlowTypetxt.Text)
    End Sub

    Protected Sub ddlFlowTypeFlag_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlFlowTypeFlag.SelectedIndexChanged
        If (ddlFlowTypeFlag.SelectedValue = "0" Or ddlFlowTypeFlag.SelectedValue = "1") And ddlFlowTypeFlag.SelectedValue <> "" Then
            ddlFlowSN.SelectedValue = ""
            ddlFlowSN.Enabled = False
        ElseIf (ddlFlowTypeFlag.SelectedValue = "2" Or ddlFlowTypeFlag.SelectedValue = "3") And ddlFlowTypeFlag.SelectedValue <> "" Then
            ddlFlowSN.SelectedValue = ""
            ddlFlowSN.Enabled = True
        End If
    End Sub
End Class
