'****************************************************
'功能說明：銀行維護
'建立人員：A02976
'建立日期：2007/04/06
'****************************************************
Imports System.Data
Imports System.Collections
Imports System.IO

Partial Class SC_SC0350
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtBankID.Attributes.Add("onkeypress", "EntertoSubmit();")
        txtBankName.Attributes.Add("onkeypress", "EntertoSubmit();")

        If StateMain IsNot Nothing Then
            sdsMain.SelectCommand = CType(StateMain, String)
        End If

        If Not IsPostBack Then
            Page.SetFocus(txtBankID)
        End If
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack Then
            If ti.Args.Length > 0 Then
                txtBankID.Text = ti.Args(0)
                txtBankName.Text = ti.Args(1)
            End If
        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnAdd"       '新增
                DoAdd()
            Case "btnUpdate"    '修改
                DoUpdate()
            Case "btnQuery"     '查詢
                DoQuery()
            Case "btnDelete"    '刪除
                DoDelete()
            Case Else
                DoOtherAction()   '其他功能動作
        End Select
    End Sub

    Private Sub DoAdd()
        Dim btnA As New ButtonState(ButtonState.emButtonType.Add)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)

        btnA.Caption = "存檔返回"
        btnX.Caption = "返回"

        Me.TransferFramePage("~/SC/SC0351.aspx", New ButtonState() {btnA, btnX}, _
                txtBankID.Text, txtBankName.Text, gvMain.PageIndex)
    End Sub

    Private Sub DoUpdate()
        Dim btnU As New ButtonState(ButtonState.emButtonType.Update)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)

        btnU.Caption = "存檔返回"
        btnX.Caption = "返回"

        Me.TransferFramePage("~/SC/SC0352.aspx", New ButtonState() {btnU, btnX}, _
                txtBankID.Text, txtBankName.Text, gvMain.PageIndex, _
                gvMain.Rows(selectedRow(gvMain)).Cells(1).Text)
    End Sub

    Private Sub DoQuery()
        Dim strSQL As New StringBuilder
        Try
            strSQL.AppendLine("SELECT * FROM SC_Bank WHERE 1 = 1")
            If txtBankID.Text.Trim() <> "" Then
                strSQL.AppendLine("AND BankID like " & Bsp.Utility.Quote("%" + txtBankID.Text.Trim() + "%"))
            End If
            If txtBankName.Text.Trim() <> "" Then
                strSQL.AppendLine("AND BankName like " & Bsp.Utility.Quote("%" + txtBankName.Text.Trim() + "%"))
            End If

            sdsMain.SelectCommand = strSQL.ToString()
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
        End Try
    End Sub

    Private Sub DoDelete()
        Dim objSC As New SC
        Dim strSQL As New StringBuilder
        strSQL.AppendLine("DELETE SC_Bank WHERE BankID = " & Bsp.Utility.Quote(gvMain.Rows(selectedRow(gvMain)).Cells(1).Text))
        Try
            Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString())
            gvMain.DataBind()
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoDelete", ex)
        End Try
    End Sub

    Private Sub DoOtherAction()

    End Sub

End Class
