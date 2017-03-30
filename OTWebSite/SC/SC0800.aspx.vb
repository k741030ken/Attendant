'****************************************************
'功能說明：放行密碼維護
'建立人員：Ann
'建立日期：2014.08.25
'****************************************************
Imports System.Data
Imports System.Data.Common

Partial Class SC_SC0800
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objSC As New SC()

        txtPassword.Attributes.Add("onkeypress", "EntertoSubmit();")
        txtConfirmPassword.Attributes.Add("onkeypress", "EntertoSubmit();")

        GetData() '20150225 Beatrice modify 新增欄位
        'pcMain.DataTable = objSC.QueryAdmin("AdminID=" & UserProfile.ActUserID)

    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnUpdate"     '查詢
                ViewState.Item("DoUpdate") = "Y"
                DoUpdate()
                GoBack()
        End Select
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

            For Each strKey As String In ht.Keys
                Dim ctl As Control = Me.FindControl(strKey)

                If TypeOf ctl Is TextBox Then
                    CType(ctl, TextBox).Text = ht(strKey).ToString()
                End If
            Next

            'If ht.ContainsKey("PageNo") Then pcMain.PageNo = Convert.ToInt32(ht("PageNo"))
            If ht.ContainsKey("DoQuery") Then
                If ht("DoQuery").ToString() = "Y" Then
                    ViewState.Item("DoQuery") = "Y"
                    DoUpdate()
                End If
            End If
        End If
    End Sub

    '20150225 Beatrice modify 新增欄位：公司(代碼+名稱)、員工編號、員工姓名、部門(代碼+名稱)、科組課(代碼+名稱)、最後異動公司、最後異動員編、最後異動日
    Private Sub GetData()
        Dim objSC As New SC
        Dim bsUser As New beSC_User.Service()
        Dim beUser As New beSC_User.Row()

        beUser.CompID.Value = UserProfile.ActCompID
        beUser.UserID.Value = UserProfile.ActUserID

        Using dt As DataTable = bsUser.QueryByKey(beUser).Tables(0)
            If dt.Rows.Count <= 0 Then Exit Sub
            beUser = New beSC_User.Row(dt.Rows(0))
            Try
                lblCompID.Text = beUser.CompID.Value
                lblCompName.Text = objSC.GetCompName(beUser.CompID.Value).Rows(0).Item("CompName").ToString()
                lblExpireDate.Text = beUser.ExpireDate.Value.ToString("yyyy/MM/dd")    '20151001 Ann add 密碼有效期限
                lblUserID.Text = beUser.UserID.Value
                lblUserName.Text = beUser.UserName.Value
                lblDeptName.Text = Bsp.Utility.subDeptName(beUser.CompID.Value, beUser.UserID.Value)
                lblOrganName.Text = Bsp.Utility.subOrganName(beUser.CompID.Value, beUser.UserID.Value)

                '20150312 Beatrice modify
                Dim CompName As String = objSC.GetSC_CompName(beUser.LastChgComp.Value)
                lblLastChgComp.Text = beUser.LastChgComp.Value + IIf(CompName <> "", "-" + CompName, "")
                Dim UserName As String = objSC.GetSC_UserName(beUser.LastChgComp.Value, beUser.LastChgID.Value)
                lblLastChgID.Text = beUser.LastChgID.Value + IIf(UserName <> "", "-" + UserName, "")
                '20150312 Beatrice modify End
                lblLastChgDate.Text = beUser.LastChgDate.Value.ToString("yyyy/MM/dd HH:mm:ss")
            Catch ex As Exception
                Bsp.Utility.ShowMessage(Me, Me.FunID & ".GetData", ex)
            End Try
        End Using
    End Sub
    '20150225 Beatrice modify End

    Private Function DoUpdate() As Boolean
        Dim objSC As New SC()
        Dim beSCUser As New beSC_User.Row()
        Dim bsSCUser As New beSC_User.Service()
        Dim intResult As Integer
        Dim strMsg As String = ""

        '取得輸入資料
        'Using cn As DbConnection = Bsp.DB.getConnection()
        '    cn.Open()
        '    Dim tran As DbTransaction = cn.BeginTransaction
        '    Dim inTrans As Boolean = True

        Try
            intResult = objSC.ChangePassword_SC0800(UserProfile.ActCompID, UserProfile.ActUserID, txtPassword.Text, txtConfirmPassword.Text)

            If intResult = "1" Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00030", "密碼錯誤")
            Else
                Return True
                'beSCUser.CompID.Value = UserProfile.ActCompID
                'Dim strCompID As String = beSCUser.CompID.Value

                'beSCUser.UserID.Value = UserProfile.ActUserID
                'Dim strUserID As String = beSCUser.UserID.Value

                'beSCUser.Password.Value = txtConfirmPassword.Text.ToUpper()
                'Dim strPassword As String = beSCUser.Password.Value

                'beSCUser.LastChgComp.Value = UserProfile.ActCompID
                'Dim strLastChgCompID As String = beSCUser.LastChgComp.Value

                'beSCUser.LastChgID.Value = UserProfile.ActUserID
                'Dim strLastChgID As String = beSCUser.LastChgID.Value

                'beSCUser.LastChgDate.Value = Now
                'Dim strLastChgDate As String = beSCUser.LastChgDate.Value

                'If objSC.UpdateSC_User(strCompID, strUserID, strPassword, strLastChgCompID, strLastChgID, tran) Then
                '    tran.Commit()
                '    inTrans = False
                '    Return True
                'Else
                '    tran.Rollback()
                '    inTrans = False
                '    Return False
                'End If
            End If

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
            Return False
        End Try
        'End Using
        Return True
    End Function

    Private Sub GoBack()
        'Dim ti As TransferInfo = Me.StateTransfer
        'Me.TransferFramePage(ti.CallerUrl, Nothing, ti.Args)
        If Session(Bsp.MySettings.UserProfileSessionName) Is Nothing Then
            Dim strLoginPage As String = ResolveUrl(Bsp.MySettings.LoginPage)

            Bsp.Utility.RunClientScript(Me, "window.top.location = '" & strLoginPage & "';")
        Else
            Dim strHomePage As String = ResolveUrl(Bsp.MySettings.StartPage)

            Bsp.Utility.RunClientScript(Me, "window.top.location = '" & strHomePage & "';")
        End If
    End Sub
End Class
