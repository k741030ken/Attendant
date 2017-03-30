'****************************************************
'功能說明：片語維護-新增/修改
'建立人員：Chung
'建立日期：2013/01/29
'****************************************************
Imports System.Data

Partial Class WF_WFA041
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim objWF As New WF
            Object2ViewState(ti.Args)

            Bsp.Utility.FillCommon(ddlFlowName, "003", Bsp.Enums.SelectCommonType.All)
            ddlFlowName.Items.Insert(0, New ListItem("----All----", ""))

            If ViewState.Item("EMode") = "UPD" Then
                Try
                    Using dt As DataTable = objWF.GetFlowPhrase(ViewState.Item("FlowID"), ViewState.Item("FlowStepID"), ViewState.Item("PhraseUser"), ViewState.Item("SeqNo"))
                        If dt.Rows.Count > 0 Then
                            Bsp.Utility.IniListWithValue(ddlFlowName, ViewState.Item("FlowID"))
                            GetFlowStep()
                            Bsp.Utility.IniListWithValue(ddlFlowStep, ViewState.Item("FlowStepID"))
                            ddlFlowName.Enabled = False
                            ddlFlowStep.Enabled = False
                            txtFlowPhrase.Text = dt.Rows(0).Item("FlowPhrase").ToString
                        End If
                    End Using
                Catch ex As Exception
                    Bsp.Utility.ShowMessage(Me, Me.FunID & ".BaseOnPageTransfer", ex)
                End Try
            ElseIf ViewState.Item("EMode") = "INS" Then
                Bsp.Utility.IniListWithValue(ddlFlowName, ViewState.Item("FlowID"))
                GetFlowStep()
                Bsp.Utility.IniListWithValue(ddlFlowStep, ViewState.Item("FlowStepID"))
            Else
                GetFlowStep()
            End If
        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnAdd"
                If funCheckData() Then
                    If SaveData() Then
                        GoBack()
                    End If
                End If
            Case "btnUpdate"
                If funCheckData() Then
                    If UpdData() Then
                        GoBack()
                    End If
                End If
            Case "btnDelete"
                Dim objWF As New WF
                objWF.Delete_FlowPhrase(ViewState.Item("FlowID"), ViewState.Item("FlowStepID"), ViewState.Item("SeqNo"), ViewState.Item("PhraseUser"))
                GoBack()
            Case "btnActionX"
                GoBack()
        End Select
    End Sub

    Private Sub GoBack()
        Dim ti As TransferInfo = Me.StateTransfer
        Dim strUrl As String = ti.CallerUrl.ToString & "?PhraseFlag=" & ViewState.Item("PhraseFlag")
        Dim btnA As New ButtonState(ButtonState.emButtonType.Add)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)
        If ViewState.Item("PhraseFlag") = "Y" Then
            Me.TransferFramePage(strUrl, New ButtonState() {btnA, btnX}, ti.Args)
        Else
            Me.TransferFramePage(strUrl, Nothing, ti.Args)
        End If
    End Sub

    Private Function UpdData() As Boolean
        Try
            Dim objWF As New WF()
            Dim PhraseUser As String = ViewState.Item("PhraseUser")

            objWF.Update_FlowPhrase(ddlFlowName.SelectedValue, ddlFlowStep.SelectedValue, _
                PhraseUser, ViewState.Item("SeqNo"), txtFlowPhrase.Text.ToString())
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".UpdData", ex)
            Return False
        End Try

        Return True
    End Function

    Private Function SaveData() As Boolean
        Try
            Dim objWF As New WF()
            Dim PhraseUser As String = ViewState.Item("PhraseUser")
            objWF.Insert_FlowPhrase(ddlFlowName.SelectedValue, ddlFlowStep.SelectedValue, PhraseUser, _
                txtFlowPhrase.Text.ToString())
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".SaveData", ex)
            Return False
        End Try

        Return True
    End Function

    Private Function funCheckData() As Boolean
        Dim strValue As String
        strValue = ddlFlowName.SelectedValue
        If strValue = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "流程名稱")
            Return False
        End If
        strValue = ddlFlowStep.SelectedValue
        If strValue = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "關卡名稱")
            Return False
        End If
        strValue = txtFlowPhrase.Text.ToString()
        If strValue = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "關卡片語")
            txtFlowPhrase.Focus()
            Return False
        Else
            If strValue.Length > 500 Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00040", "關卡片語", "500")
                txtFlowPhrase.Focus()
                Return False
            End If
            txtFlowPhrase.Text = strValue
        End If
        Return True
    End Function

    Protected Sub ddlFlowName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFlowName.SelectedIndexChanged
        GetFlowStep()
    End Sub

    Private Sub GetFlowStep()
        If ddlFlowName.SelectedIndex <> 0 Then
            Dim objWF As New WF()
            Using dt As DataTable = objWF.GetFlowStepbyFlowID(ddlFlowName.SelectedValue)
                ddlFlowStep.Items.Clear()
                ddlFlowStep.DataTextField = "FlowStepDesc"
                ddlFlowStep.DataValueField = "FlowStepID"
                ddlFlowStep.DataSource = dt
                ddlFlowStep.DataBind()
                ddlFlowStep.Items.Insert(0, New ListItem("----All----", ""))
            End Using
        Else
            ddlFlowStep.Items.Clear()
        End If
    End Sub

End Class
