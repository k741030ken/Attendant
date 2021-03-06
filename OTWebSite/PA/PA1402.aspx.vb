'****************************************************
'功能說明：職稱代碼設定-修改
'建立人員：MickySung
'建立日期：2015.04.29
'****************************************************
Imports System.Data

Partial Class PA_PA1402
    Inherits PageBase


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            'Dim objSC As New SC
            ''公司代碼
            'lbltxtCompID.Text = UserProfile.SelectCompRoleID + "-" + objSC.GetCompName(UserProfile.SelectCompRoleID).Rows(0).Item("CompName").ToString
        End If
    End Sub
    
    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

            If ht.ContainsKey("SelectedTitleID") Then
                ViewState.Item("CompID") = ht("SelectedCompID").ToString()
                ViewState.Item("RankID") = ht("SelectedRankID").ToString()
                ViewState.Item("TitleID") = ht("SelectedTitleID").ToString()

                subGetData(ht("SelectedRankID").ToString(), ht("SelectedCompID").ToString(), ht("SelectedTitleID").ToString())
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
        Dim beTitle As New beTitle.Row()
        Dim bsTitle As New beTitle.Service()
        Dim objPA As New PA1()

        '取得輸入資料
        beTitle.CompID.Value = UserProfile.SelectCompRoleID
        beTitle.RankID.Value = lbltxtRankID.Text
        beTitle.TitleID.Value = lbltxtTitleID.Text
        beTitle.TitleName.Value = txtTitleName.Text
        beTitle.TitleEngName.Value = txtTitleEngName.Text
        beTitle.LastChgComp.Value = UserProfile.ActCompID
        beTitle.LastChgID.Value = UserProfile.ActUserID
        beTitle.LastChgDate.Value = Now

        '儲存資料
        Try
            Return objPA.UpdateTitleSetting(beTitle)
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, "[CC0201_99]" & Bsp.Utility.getMessage("E_00000") & Bsp.Utility.getInnerException(Me.FunID, ex))
            Return False
        End Try
    End Function

    Private Sub subGetData(ByVal RankID As String, ByVal CompID As String, ByVal TitleID As String)
        Dim objPA As New PA1()
        Dim objSC As New SC()
        Dim beTitle As New beTitle.Row()
        Dim bsTitle As New beTitle.Service()

        beTitle.RankID.Value = RankID
        beTitle.CompID.Value = CompID
        beTitle.TitleID.Value = TitleID
        Try
            Using dt As DataTable = bsTitle.QueryByKey(beTitle).Tables(0)
                If dt.Rows.Count <= 0 Then Exit Sub
                beTitle = New beTitle.Row(dt.Rows(0))

                '公司代碼
                lbltxtCompID.Text = UserProfile.SelectCompRoleName
                '職等代碼
                lbltxtRankID.Text = beTitle.RankID.Value
                '職稱代碼
                lbltxtTitleID.Text = beTitle.TitleID.Value
                '職稱名稱
                txtTitleName.Text = beTitle.TitleName.Value
                '職稱英文名稱
                txtTitleEngName.Text = beTitle.TitleEngName.Value
                '最後異動公司
                If beTitle.LastChgComp.Value.Trim <> "" Then
                    lblLastChgComp.Text = beTitle.LastChgComp.Value + "-" + objSC.GetCompName(beTitle.LastChgComp.Value).Rows(0).Item("CompName").ToString
                Else
                    lblLastChgComp.Text = ""
                End If
                '最後異動人員
                If beTitle.LastChgID.Value.Trim <> "" Then
                    Dim UserName As String = objSC.GetSC_UserName(beTitle.LastChgComp.Value, beTitle.LastChgID.Value)
                    lblLastChgID.Text = beTitle.LastChgID.Value + IIf(UserName <> "", "-" + UserName, "")
                Else
                    lblLastChgID.Text = ""
                End If
                '最後異動日期
                lblLastChgDate.Text = IIf(Format(beTitle.LastChgDate.Value, "yyyy/MM/dd") = "1900/01/01", "", beTitle.LastChgDate.Value.ToString("yyyy/MM/dd HH:mm:ss"))

            End Using
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & "subGetData", ex)
        End Try

    End Sub

    Private Function funCheckData() As Boolean
        Dim objPA As New PA1()
        Dim strWhere As String = ""
        Dim beTitle As New beTitle.Row()
        Dim bsTitle As New beTitle.Service()

        '職稱名稱
        'If Bsp.Utility.getStringLength(txtTitleName.Text.Trim) > txtTitleName.MaxLength Then
        '    Bsp.Utility.ShowFormatMessage(Me, "W_00040", lblTitleName.Text, txtTitleName.MaxLength.ToString)
        '    Return False
        'End If

        '職稱英文名稱
        If txtTitleEngName.Text.Trim.Length > txtTitleEngName.MaxLength Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00040", lblTitleEngName.Text, txtTitleEngName.MaxLength.ToString)
            Return False
        End If

        Return True
    End Function

    Private Sub ClearData()
        subGetData(ViewState.Item("RankID"), ViewState.Item("CompID"), ViewState.Item("TitleID"))
    End Sub

End Class
