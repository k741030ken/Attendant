'****************************************************
'功能說明：員工基本資料異動紀錄查詢
'建立人員：MickySung
'建立日期：2015.05.29
'****************************************************
Imports System.Data

Partial Class ST_ST1700
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnQuery"     '查詢
                ViewState.Item("DoQuery") = "Y"
                DoQuery()
        End Select
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim objST As New ST1
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)
            If ht.ContainsKey("SelectedCompID") Then
                ViewState.Item("CompID") = ht("SelectedCompID").ToString()
                ViewState.Item("CompName") = ht("SelectedCompName").ToString()
                ViewState.Item("EmpID") = ht("SelectedEmpID").ToString()
                ViewState.Item("EmpName") = ht("SelectedEmpName").ToString()
                ViewState.Item("IDNo") = ht("SelectedIDNo").ToString()

                lblCompRoleID.Text = ViewState.Item("CompID").ToString + "-" + ViewState.Item("CompName")
                txtEmpID.Text = ViewState.Item("EmpID").ToString
                txtEmpName.Text = ViewState.Item("EmpName").ToString
                'imgEmpPhoto.ImageUrl = "http://10.11.36.114:8086/Upload/Photo/" + ViewState.Item("CompID") + "-" + "" + ".jpg"  'txtEmpID.Text

                '2015/12/03 Add 更改圖片顯示方式
                Try
                    imgEmpPhoto.ImageUrl = objST.EmpPhotoQuery("CompID=" & ViewState.Item("CompID"), "EmpID=" & txtEmpID.Text.ToUpper)
                    imgEmpPhoto.Visible = True
                    imgEmpPhoto_NoPic.Visible = False
                Catch ex As Exception
                    'Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)

                    imgEmpPhoto.ImageUrl = ""
                    imgEmpPhoto.Visible = False
                    imgEmpPhoto_NoPic.Visible = True
                End Try
            Else
                Return
            End If
            DoQuery()
        End If
    End Sub

    Private Sub DoQuery()
        Dim objST As New ST1
        gvMain.Visible = True

        Try
            pcMain.DataTable = objST.QueryPersonalLogSetting(
                "CompID=" & ViewState.Item("CompID"), _
                "EmpID=" & txtEmpID.Text, _
                "EmpName=" & txtEmpName.Text)

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
        End Try
    End Sub

End Class
