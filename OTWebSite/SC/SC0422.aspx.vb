'****************************************************
'功能說明：訊息定義檔-修改
'建立人員：Chung
'建立日期：2013/01/29
'****************************************************
Imports System.Data

Partial Class SC_SC0422
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Bsp.Utility.FillCommon(rblMsgKind, "02", Bsp.Enums.SelectCommonType.All)
            Bsp.Utility.FillCommon(rblOpenFlag, "03", Bsp.Enums.SelectCommonType.All)
            Page.SetFocus(txtMsgReason)
        End If
        rblOpenWSize.Attributes.Add("onclick", "EnableSizeBox();")
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If ti.Args.Length > 0 Then
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

            If ht.ContainsKey("SelectedMsgCode") Then
                GetData(ht("SelectedMsgCode").ToString())
            End If
        End If
    End Sub

    Private Sub GetData(ByVal MsgCode As String)
        Dim beMsgCode As beWF_MsgDefine.Row = Nothing
        Dim objSC As New SC()

        Using dt As DataTable = objSC.GetMsgDefineData(MsgCode)
            If dt.Rows.Count = 0 Then Return

            beMsgCode = New beWF_MsgDefine.Row(dt.Rows(0))
        End Using

        With beMsgCode
            lblMsgCode.Text = .MsgCode.Value
            txtMsgReason.Text = .MsgReason.Value
            txtMsgUrl.Text = .MsgUrl.Value
            Bsp.Utility.IniListWithValue(rblMsgKind, .MsgKind.Value)
            Bsp.Utility.IniListWithValue(rblOpenFlag, .OpenFlag.Value)
            If Bsp.Utility.InStr(.OpenWSize.Value, "S", "M", "L") Then
                Bsp.Utility.IniListWithValue(rblOpenWSize, .OpenWSize.Value)
                txtSize.Enabled = False
            Else
                Bsp.Utility.IniListWithValue(rblOpenWSize, "C")
                txtSize.Text = .OpenWSize.Value
                txtSize.Enabled = True
            End If
            Bsp.Utility.IniListWithValue(rblDelKind, .DelKind.Value)
            lblLastChgID.Text = .LastChgID.Value
            lblLastChgDate.Text = .LastChgDate.Value.ToString("yyyy/MM/dd HH:mm:ss")
        End With
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnUpdate"
                If funCheckData() Then
                    If SaveData() Then GoBack()
                End If
            Case "btnActionX"
                GoBack()
        End Select
    End Sub

    Private Sub GoBack()
        Dim ti As TransferInfo = Me.StateTransfer
        Me.TransferFramePage(ti.CallerUrl, Nothing, ti.Args)
    End Sub

    Private Function SaveData() As Boolean
        Dim beMsgDefine As New beWF_MsgDefine.Row()
        Dim bsMsgDefine As New beWF_MsgDefine.Service()
        Dim objST As New SC()

        With beMsgDefine
            .MsgCode.Value = lblMsgCode.Text
            .MsgReason.Value = txtMsgReason.Text
            .MsgUrl.Value = txtMsgUrl.Text
            .MsgKind.Value = rblMsgKind.SelectedValue
            .OpenFlag.Value = rblOpenFlag.SelectedValue
            If rblOpenWSize.SelectedValue = "C" Then
                .OpenWSize.Value = txtSize.Text
            Else
                .OpenWSize.Value = rblOpenWSize.SelectedValue
            End If
            .DelKind.Value = rblDelKind.SelectedValue
            .LastChgDate.Value = Now
            .LastChgID.Value = UserProfile.ActUserID
        End With

        If Not bsMsgDefine.IsDataExists(beMsgDefine) Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00020", "訊息代碼")
            Return False
        End If

        Try
            bsMsgDefine.Update(beMsgDefine)
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".SaveData", ex)
            Return False
        End Try
        Return True
    End Function

    Private Function funCheckData() As Boolean
        Dim strValue As String

        strValue = txtMsgReason.Text.Trim()
        If strValue = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "訊息描述")
            txtMsgReason.Focus()
            Return False
        Else
            If Bsp.Utility.getStringLength(strValue) > txtMsgReason.MaxLength Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00040", "訊息描述", txtMsgReason.MaxLength.ToString())
                txtMsgReason.Focus()
                Return False
            End If
            txtMsgReason.Text = strValue
        End If

        strValue = txtMsgUrl.Text.Trim()
        If strValue = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "連結網頁")
            txtMsgUrl.Focus()
            Return False
        Else
            If Bsp.Utility.getStringLength(strValue) > txtMsgUrl.MaxLength Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00040", "連結網頁", txtMsgUrl.MaxLength.ToString())
                txtMsgUrl.Focus()
                Return False
            End If
            txtMsgUrl.Text = strValue
        End If
        txtMsgUrl.Text = strValue

        If rblMsgKind.SelectedIndex < 0 Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00035", "訊息類別")
            rblMsgKind.Focus()
            Return False
        End If

        If rblOpenFlag.SelectedIndex < 0 Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00035", "網頁開啟方式")
            rblOpenFlag.Focus()
            Return False
        End If

        If rblOpenWSize.SelectedIndex < 0 Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00035", "開啟視窗大小")
            rblOpenFlag.Focus()
            Return False
        End If

        If rblOpenWSize.SelectedValue = "C" Then
            strValue = txtSize.Text.Trim()
            Dim arySize() As String = strValue.Split("*")
            If arySize.Length <> 2 Then
                Bsp.Utility.ShowFormatMessage(Me, "W_03200")
                txtSize.Focus()
                Return False
            End If
            'If (Not IsNumeric(arySize(0))) OrElse (Not IsNumeric(arySize(1))) Then
            '    Bsp.Utility.ShowFormatMessage(Me, "W_03200")
            '    txtSize.Focus()
            '    Return False
            'End If
            txtSize.Text = arySize(0).Trim() & "*" & arySize(1).Trim()
        End If

        If rblDelKind.SelectedIndex < 0 Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00035", "開啟刪除註記")
            rblDelKind.Focus()
            Return False
        End If

        Return True
    End Function
End Class
