'****************************************************
'功能說明：職位設定_中類-修改
'建立人員：MickySung
'建立日期：2015.05.13
'****************************************************
Imports System.Data

Partial Class PA_PA1822
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then

        End If
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

            If ht.ContainsKey("SelectedCategoryII") Then
                ViewState.Item("CategoryII") = ht("SelectedCategoryII").ToString()
                subGetData(ht("SelectedCategoryI").ToString(), ht("SelectedCategoryII").ToString(), ht("SelectedCategoryIName").ToString())
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
        Dim bePosition_CategoryII As New bePosition_CategoryII.Row()
        Dim bsPosition_CategoryII As New bePosition_CategoryII.Service()
        Dim objPA As New PA1()

        '取得輸入資料
        bePosition_CategoryII.CategoryI.Value = lbltxtCategoryI.Text
        bePosition_CategoryII.CategoryII.Value = lbltxtCategoryII.Text
        bePosition_CategoryII.CategoryIIName.Value = txtCategoryIIName.Text
        bePosition_CategoryII.LastChgComp.Value = UserProfile.ActCompID
        bePosition_CategoryII.LastChgID.Value = UserProfile.ActUserID
        bePosition_CategoryII.LastChgDate.Value = Now

        '儲存資料
        Try
            Return objPA.UpdatePosition_CategoryII(bePosition_CategoryII)
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, "[CC0201_99]" & Bsp.Utility.getMessage("E_00000") & Bsp.Utility.getInnerException(Me.FunID, ex))
            Return False
        End Try
    End Function

    Private Sub subGetData(ByVal CategoryI As String, ByVal CategoryII As String, ByVal CategoryIName As String)
        Dim objPA As New PA1()
        Dim objSC As New SC()
        Dim bePosition_CategoryII As New bePosition_CategoryII.Row()
        Dim bsPosition_CategoryII As New bePosition_CategoryII.Service()

        bePosition_CategoryII.CategoryI.Value = CategoryI
        bePosition_CategoryII.CategoryII.Value = CategoryII
        Try
            Using dt As DataTable = bsPosition_CategoryII.QueryByKey(bePosition_CategoryII).Tables(0)
                If dt.Rows.Count <= 0 Then Exit Sub
                bePosition_CategoryII = New bePosition_CategoryII.Row(dt.Rows(0))

                '大類代碼
                lbltxtCategoryI.Text = bePosition_CategoryII.CategoryI.Value
                lbltxtCategoryIName.Text = CategoryIName
                '中類代碼
                lbltxtCategoryII.Text = bePosition_CategoryII.CategoryII.Value
                '中類名稱
                txtCategoryIIName.Text = bePosition_CategoryII.CategoryIIName.Value
                '最後異動公司
                If bePosition_CategoryII.LastChgComp.Value.Trim <> "" Then
                    lblLastChgComp.Text = bePosition_CategoryII.LastChgComp.Value + "-" + objSC.GetCompName(bePosition_CategoryII.LastChgComp.Value).Rows(0).Item("CompName").ToString
                Else
                    lblLastChgComp.Text = ""
                End If
                '最後異動人員
                If bePosition_CategoryII.LastChgID.Value.Trim <> "" Then
                    Dim UserName As String = objSC.GetSC_UserName(bePosition_CategoryII.LastChgComp.Value, bePosition_CategoryII.LastChgID.Value)
                    lblLastChgID.Text = bePosition_CategoryII.LastChgID.Value + IIf(UserName <> "", "-" + UserName, "")
                Else
                    lblLastChgID.Text = ""
                End If
                '最後異動日期
                lblLastChgDate.Text = IIf(Format(bePosition_CategoryII.LastChgDate.Value, "yyyy/MM/dd") = "1900/01/01", "", bePosition_CategoryII.LastChgDate.Value.ToString("yyyy/MM/dd HH:mm:ss"))

            End Using
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & "subGetData", ex)
        End Try
    End Sub

    Private Function funCheckData() As Boolean
        Dim objPA As New PA1()
        Dim bePosition_CategoryII As New bePosition_CategoryII.Row()
        Dim bsPosition_CategoryII As New bePosition_CategoryII.Service()

        '中類名稱
        If txtCategoryIIName.Text = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblCategoryIIName.Text)
            txtCategoryIIName.Focus()
            Return False
        End If
        'If Bsp.Utility.getStringLength(txtCategoryIIName.Text.Trim) > txtCategoryIIName.MaxLength Then
        '    Bsp.Utility.ShowFormatMessage(Me, "W_00040", lblCategoryIIName.Text, txtCategoryIIName.MaxLength.ToString)
        '    txtCategoryIIName.Focus()
        '    Return False
        'End If

        Return True
    End Function

    Private Sub ClearData()
        subGetData(lbltxtCategoryI.Text, lbltxtCategoryII.Text, lbltxtCategoryIName.Text)
    End Sub

End Class
