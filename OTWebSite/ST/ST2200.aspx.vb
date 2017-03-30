'****************************************************
'功能說明：整批員工照片檔上傳
'建立人員：BeatriceCheng
'建立日期：2015.06.04
'****************************************************
Imports System.Data

Partial Class ST_ST2200
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objSC As New SC

            ddlCompID.Visible = False
            If UserProfile.SelectCompRoleID = "ALL" Then
                ddlCompID.Visible = True
                Bsp.Utility.FillHRCompany(ddlCompID)
                lblCompRoleID.Visible = False
                plUpload.Visible = False
            Else
                ddlCompID.Visible = False
                lblCompRoleID.Text = UserProfile.SelectCompRoleName
                lblCompRoleID.Visible = True
            End If
        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnActionC"   '上傳

        End Select
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

        End If
    End Sub

    Protected Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click

        Dim strFilePath As String = Server.MapPath(Bsp.Utility.getAppSetting("TempPath")) & "\" & hldLogName.Value

        If System.IO.File.Exists(strFilePath) Then
            Response.ClearContent()
            Response.BufferOutput = True
            Response.Charset = "utf-8"
            Response.ContentType = "application/octet-stream"

            Response.AddHeader("Content-Transfer-Encoding", "binary")
            Response.ContentEncoding = System.Text.Encoding.Default
            Response.AddHeader("Content-Disposition", "attachment; filename=" & Server.UrlPathEncode(hldLogName.Value))

            Response.BinaryWrite(System.IO.File.ReadAllBytes(strFilePath))

            '刪除Server上log檔
            System.IO.File.Delete(strFilePath)

            Response.End()
        Else
            Response.Write("無檔案")
        End If

    End Sub
End Class
