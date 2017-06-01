'****************************************************
'功能說明：其他代碼設定-新增
'建立人員：Jason Lu
'建立日期：2016.12.27
'修改日期：2017.02.15
'****************************************************

Imports System.Data.Common

Partial Class AT_AT1001
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            Dim objOV As New AT1

            '排序順序,預設值為0,數值0~99
            objOV.GetSortFld(ddlSortFld)
            ddlSortFld.SelectedIndex = 0

            '代碼類別
            AT1.FillFldName_AT1000(ddlTabFldName)
            ddlTabFldName.Items.Insert(0, New ListItem("---請選擇---", ""))
        End If
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then

        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnAdd"   '新增
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

    ''' <summary>
    ''' 檢查輸入資料
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function funCheckData() As Boolean
        Dim beManageCodeSet As New beManageCodeSet.Row()
        Dim bsManageCodeSet As New beManageCodeSet.Service()

        '代碼類別
        If ddlTabFldName.SelectedValue = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblFldName.Text)
            ddlTabFldName.Focus()
            Return False
        End If

        '排序順序
        If txtSortFld.Text.Trim = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblSortFld.Text)
            txtSortFld.Focus()
            Return False
        ElseIf IsNumeric(txtSortFld.Text.Trim) = False Then
            Bsp.Utility.ShowMessage(Me, "｢排序順序｣請輸入0~255之數字")
            txtSortFld.Focus()
            Return False
        ElseIf CInt(txtSortFld.Text.Trim) < 0 Or CInt(txtSortFld.Text.Trim) > 255 Then
            Bsp.Utility.ShowMessage(Me, "｢排序順序｣請輸入0~255之數字")
            txtSortFld.Focus()
            Return False
        End If

        '代碼
        If txtCode.Text = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblCode.Text)
            txtCode.Focus()
            Return False
        End If
        If Bsp.Utility.getStringLength(txtCode.Text.Trim) > txtCode.MaxLength Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00040", lblCode.Text, txtCode.MaxLength.ToString)
            txtCode.Focus()
            Return False
        End If

        '代碼名稱
        If txtCodeCName.Text.Trim <> "" Then
            beManageCodeSet.CodeCName.Value = txtCodeCName.Text.Trim.ToString()
            '2017/02/15-Kat說不擋，那就不擋囉XD
            'If bsManageCodeSet.IsCodeCNameExists(beManageCodeSet) Then
            '    Bsp.Utility.ShowMessage(Me, "已經有相同的代碼名稱，請輸入新的代碼名稱")
            '    Return False
            'End If
            'Return True
        Else
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", lblCodeCName.Text)
            txtCodeCName.Focus()
            Return False
        End If

        '檢查資料是否存在
        '取得輸入資料
        Dim arrFldName(1) As String
        arrFldName = ddlTabFldName.SelectedValue.Split("\")
        beManageCodeSet.TabName.Value = arrFldName(0)
        beManageCodeSet.FldName.Value = arrFldName(1)

        beManageCodeSet.Code.Value = txtCode.Text.ToString().Trim
        If bsManageCodeSet.IsDataExists(beManageCodeSet) Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00010", "")
            Return False
        End If
        Return True
    End Function

    Private Function SaveData() As Boolean
        Dim objOV As New AT1
        Dim beManageCodeSet As New beManageCodeSet.Row()
        Dim bsManageCodeSet As New beManageCodeSet.Service()
        Dim CodeNum As Integer = 0

        '檢查不顯示註記是否有勾選 0=顯示,１=不顯示
        If chkNotShowingFlag.Checked = True Then
            beManageCodeSet.NotShowFlag.Value = "1"
        Else
            beManageCodeSet.NotShowFlag.Value = "0"
        End If

        '取得輸入資料
        Dim arrFldName(1) As String
        arrFldName = ddlTabFldName.SelectedValue.Split("\")
        beManageCodeSet.TabName.Value = arrFldName(0)
        beManageCodeSet.FldName.Value = arrFldName(1)
        beManageCodeSet.Code.Value = txtCode.Text.ToString()
        beManageCodeSet.CodeCName.Value = txtCodeCName.Text.Trim.ToString()
        beManageCodeSet.SortFld.Value = ddlSortFld.SelectedValue.ToString()
        beManageCodeSet.SortFld.Value = txtSortFld.Text.Trim.ToString()
        beManageCodeSet.LastChgComp.Value = UserProfile.ActCompID
        beManageCodeSet.LastChgID.Value = UserProfile.ActUserID
        beManageCodeSet.LastChgDate.Value = Now

        '儲存資料
        Try
            If AddOTTypeSetting(beManageCodeSet) Then
                '2017/02/15-Kat說完全比照PA51XX系列，那就全比照囉XD
                'Bsp.Utility.ShowMessage(Me, "代碼新增成功")
                Return True
            Else
                '2017/02/15-Kat說完全比照PA51XX系列，那就全比照囉XD
                'Bsp.Utility.ShowMessage(Me, "代碼新增失敗")
            End If
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, "[CC0201_99]" & Bsp.Utility.getMessage("E_00000") & Bsp.Utility.getInnerException(Me.FunID, ex))
            Return False
        End Try
        Return False
    End Function

    Private Sub ClearData()
        ddlTabFldName.SelectedValue = ""
        txtCode.Text = ""
        txtCodeCName.Text = ""
        txtSortFld.Text = "0"
        ddlSortFld.SelectedIndex = 0
        chkNotShowingFlag.Checked = False
    End Sub

    Private Function AddOTTypeSetting(ByVal beManageCodeSet As beManageCodeSet.Row) As Boolean
        Dim bsManageCodeSet As New beManageCodeSet.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("AattendantDB")
            'Using cn As DbConnection = Bsp.DB.getConnection("testConnectionString")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsManageCodeSet.Insert(beManageCodeSet, tran) = 0 Then Return False
                tran.Commit()
            Catch ex As Exception
                tran.Rollback()
                Throw
            Finally
                If tran IsNot Nothing Then tran.Dispose()
            End Try
        End Using
        Return True
    End Function

End Class
