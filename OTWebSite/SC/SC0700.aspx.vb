'****************************************************
'功能說明：系統管理者資料維護
'建立人員：Ann
'建立日期：2014.08.20
'****************************************************
Imports System.Data

Partial Class SC_SC0700
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objSC As New SC()

        txtSysID.Attributes.Add("onkeypress", "EntertoSubmit();")
        txtSysName.Attributes.Add("onkeypress", "EntertoSubmit();")

        Page.SetFocus(txtSysID)

        Dim strSysID As String
        strSysID = Bsp.Utility.subGetSysID(UserProfile.LoginSysID)
        Dim arySysID() As String = Split(strSysID, "-")
        lblSysName.Text = strSysID
        
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnAdd"       '新增
                DoAdd()
            Case "btnQuery"     '查詢
                ViewState.Item("DoQuery") = "Y"
                DoQuery()
            Case "btnDelete"    '刪除
                DoDelete()
            Case "btnActionC"   '確認
            Case "btnActionP"   '列印
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

            If ht.ContainsKey("PageNo") Then pcMain.PageNo = Convert.ToInt32(ht("PageNo"))
            If ht.ContainsKey("DoQuery") Then
                If ht("DoQuery").ToString() = "Y" Then
                    ViewState.Item("DoQuery") = "Y"
                    DoQuery()
                End If
            End If
        End If
    End Sub

    Private Sub DoAdd()
        Dim btnA As New ButtonState(ButtonState.emButtonType.Add)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)

        btnA.Caption = "存檔返回"
        btnX.Caption = "返回"

        Me.TransferFramePage("~/SC/SC0701.aspx", New ButtonState() {btnA, btnX}, _
                             Bsp.Utility.FormatToParam(txtSysID), _
                             Bsp.Utility.FormatToParam(txtSysName), _
                             "PageNo=" & pcMain.PageNo.ToString(), _
                             "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
    End Sub

    Private Sub DoQuery()
        Dim objSC As New SC()

        Try
            pcMain.DataTable = objSC.QuerySysByUser("UserID=" & txtSysID.Text.Trim().ToUpper(), "UserName=" & txtSysName.Text.Trim())
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
        End Try
    End Sub

    Private Sub DoDelete()
        If selectedRow(gvMain) < 0 Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00000")
        Else
            Dim beSCAdmin As New beSC_Admin.Row()
            Dim objSC As New SC

            beSCAdmin.SysID.Value = UserProfile.LoginSysID
            beSCAdmin.AdminComp.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("AdminComp").ToString()
            beSCAdmin.AdminID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("AdminID").ToString()

            Try
                objSC.DeleteAdmin(beSCAdmin)
            Catch ex As Exception
                Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoDelete", ex)
            End Try
            gvMain.DataBind()
        End If
    End Sub
End Class
