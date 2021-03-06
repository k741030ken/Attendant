'****************************************************
'功能說明：代碼維護-新增
'建立人員：Chung
'建立日期：2011/05/24
'****************************************************
Imports System.Data

Partial Class SC_SC0321
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Bsp.Utility.FillCommon(ddlType, "000", Bsp.Enums.SelectCommonType.All)
            Page.SetFocus(ddlType)
        End If
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack Then
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

            If ht.ContainsKey("ddlType") Then
                Bsp.Utility.SetSelectedIndex(ddlType, ht("ddlType").ToString())
            End If
        End If
    End Sub

    Private Sub InitialScreen()
        txtCode.Text = ""
        txtDefine.Text = ""
        txtOrderSeq.Text = ""
        txtNote.Text = ""
        txtRsvCol1.Text = ""
        txtRsvCol2.Text = ""
        txtRsvCol3.Text = ""
        txtRsvCol4.Text = ""
        txtRsvCol5.Text = ""
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnAdd"
                If funCheckData() Then
                    If SaveData() Then
                        InitialScreen()
                        txtCode.Focus()
                    End If
                End If
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
        Dim beCommon As New beSC_Common.Row()
        Dim bsCommon As New beSC_Common.Service()

        With beCommon
            .Type.Value = ddlType.SelectedValue
            .Code.Value = txtCode.Text
            .Define.Value = txtDefine.Text
            If txtOrderSeq.Text = "" OrElse Not IsNumeric(txtOrderSeq.Text) Then
                .OrderSeq.Value = 0
            Else
                .OrderSeq.Value = CInt(txtOrderSeq.Text)
            End If
            .Note.Value = txtNote.Text
            .RsvCol1.Value = txtRsvCol1.Text
            .RsvCol2.Value = txtRsvCol2.Text
            .RsvCol3.Value = txtRsvCol3.Text
            .RsvCol4.Value = txtRsvCol4.Text
            .RsvCol5.Value = txtRsvCol5.Text
            .ValidFlag.Value = IIf(chkValidFlag.Checked, "1", "0")
            .CreateDate.Value = Now
            .LastChgDate.Value = Now
            .LastChgID.Value = UserProfile.ActUserID
        End With

        If bsCommon.IsDataExists(beCommon) Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00010", "代碼")
            Return False
        End If

        Try
            bsCommon.Insert(beCommon)
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".SaveData", ex)
            Return False
        End Try
        Return True
    End Function

    Private Function funCheckData() As Boolean
        Dim strValue As String

        strValue = txtCode.Text.ToString()
        If strValue = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "代碼")
            txtCode.Focus()
            Return False
        Else
            If Bsp.Utility.getStringLength(strValue) > txtCode.MaxLength Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00040", "代碼", txtCode.MaxLength.ToString())
                txtCode.Focus()
                Return False
            End If
            txtCode.Text = strValue
        End If

        strValue = txtDefine.Text.ToString()
        If strValue = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "代碼說明")
            txtDefine.Focus()
            Return False
        Else
            If Bsp.Utility.getStringLength(strValue) > txtDefine.MaxLength Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00040", "代碼說明", txtDefine.MaxLength.ToString())
                txtDefine.Focus()
                Return False
            End If
            txtDefine.Text = strValue
        End If

        strValue = txtOrderSeq.Text.ToString()
        If Bsp.Utility.getStringLength(strValue) > txtOrderSeq.MaxLength Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00040", "排序", txtOrderSeq.MaxLength.ToString())
            txtOrderSeq.Focus()
            Return False
        End If
        If strValue <> "" AndAlso Not IsNumeric(strValue) Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00050", "排序", txtOrderSeq.MaxLength.ToString())
            txtOrderSeq.Focus()
            Return False
        End If
        txtOrderSeq.Text = strValue

        strValue = txtNote.Text.ToString()
        If Bsp.Utility.getStringLength(strValue) > txtNote.MaxLength Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00040", "補充說明", txtNote.MaxLength.ToString())
            txtNote.Focus()
            Return False
        End If
        txtNote.Text = strValue

        strValue = txtRsvCol1.Text.ToString()
        If Bsp.Utility.getStringLength(strValue) > txtRsvCol1.MaxLength Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00040", "預留欄位1", txtRsvCol1.MaxLength.ToString())
            txtRsvCol1.Focus()
            Return False
        End If
        txtRsvCol1.Text = strValue

        strValue = txtRsvCol2.Text.ToString()
        If Bsp.Utility.getStringLength(strValue) > txtRsvCol2.MaxLength Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00040", "預留欄位2", txtRsvCol2.MaxLength.ToString())
            txtRsvCol2.Focus()
            Return False
        End If
        txtRsvCol2.Text = strValue

        strValue = txtRsvCol3.Text.ToString()
        If Bsp.Utility.getStringLength(strValue) > txtRsvCol3.MaxLength Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00040", "預留欄位3", txtRsvCol3.MaxLength.ToString())
            txtRsvCol3.Focus()
            Return False
        End If
        txtRsvCol3.Text = strValue

        strValue = txtRsvCol4.Text.ToString()
        If Bsp.Utility.getStringLength(strValue) > txtRsvCol4.MaxLength Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00040", "預留欄位4", txtRsvCol4.MaxLength.ToString())
            txtRsvCol4.Focus()
            Return False
        End If
        txtRsvCol4.Text = strValue

        strValue = txtRsvCol5.Text.ToString()
        If Bsp.Utility.getStringLength(strValue) > txtRsvCol5.MaxLength Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00040", "預留欄位5", txtRsvCol5.MaxLength.ToString())
            txtRsvCol5.Focus()
            Return False
        End If
        txtRsvCol5.Text = strValue
        Return True
    End Function
End Class
