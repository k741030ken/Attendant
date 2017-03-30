'****************************************************
'功能說明：員工照片檔上傳
'建立人員：BeatriceCheng
'建立日期：2015.06.03
'****************************************************
Imports System.Data
Imports System.IO
Imports System.Net

Partial Class ST_ST2100
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objSC As New SC

            ucSelectEmpID.ShowCompRole = False
            fuEmpPhotoUrl.Attributes.Add("accept", "image/jpeg")
            fuEmpPhotoUrl.Attributes.Add("onchange", "onLoadImage(this)")

            ddlCompID.Visible = False
            If UserProfile.SelectCompRoleID = "ALL" Then
                ddlCompID.Visible = True
                Bsp.Utility.FillHRCompany(ddlCompID)
                lblCompRoleID.Visible = False
            Else
                ddlCompID.Visible = False
                lblCompRoleID.Text = UserProfile.SelectCompRoleName
                lblCompRoleID.Visible = True
            End If
        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnQuery"     '查詢
                DoQuery()
            Case "btnUpload"   '上傳
                If funCheckData() Then
                    DoUpload()
                End If
        End Select
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

        End If
    End Sub

    Private Sub DoQuery()
        If txtEmpID.Text.Trim() = "" Then
            Bsp.Utility.ShowMessage(Me, "請輸入員工編號！")
            Return
        End If

        Dim strCompID As String = ddlCompID.SelectedValue
        If strCompID = "" Then
            strCompID = UserProfile.SelectCompRoleID
        End If

        Try
            Dim objST2 As New ST2()
            imgOldPhoto.ImageUrl = objST2.EmpPhotoQuery(strCompID, txtEmpID.Text.ToUpper)
            imgOldPhoto.Visible = True
            lblOldPhoto_NoPic.Visible = False
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)

            imgOldPhoto.ImageUrl = ""
            imgOldPhoto.Visible = False
            lblOldPhoto_NoPic.Visible = True
        End Try
    End Sub

    Private Sub DoClear()
        imgOldPhoto.ImageUrl = ""
        imgOldPhoto.Visible = False
        lblOldPhoto_NoPic.Visible = True
    End Sub

    Private Function funCheckData() As Boolean
        If txtEmpID.Text = "" Then
            Bsp.Utility.ShowMessage(Me, "員工編號未輸入！")
            Return False
        End If

        If Not fuEmpPhotoUrl.HasFile Then
            Bsp.Utility.ShowMessage(Me, "未選擇上傳檔案！")
            Return False
        End If

        Dim fileName As String = fuEmpPhotoUrl.FileName
        Dim fileOK As Boolean = False

        Dim fileExtensions As String = System.IO.Path.GetExtension(fileName).ToLower()
        Dim allowedExtensions As String() = {".jpg", ".jpeg"}

        For Each extension In allowedExtensions
            If fileExtensions = extension Then
                fileOK = True
                Exit For
            End If
        Next

        If Not fileOK Then
            Bsp.Utility.ShowMessage(Me, "請上傳jpg檔！")
            Return False
        End If

        If fileName.IndexOf("-") < 0 Then
            Bsp.Utility.ShowMessage(Me, "檔名請用『公司代碼-員編.jpg』格式！")
            Return False
        End If

        Dim strFileName As String = fileName.ToLower.Replace(".jpg", "").Replace(".jpeg", "")
        Dim strCompID As String = strFileName.Substring(0, fileName.IndexOf("-")).ToUpper
        Dim strEmpID As String = strFileName.Substring(fileName.IndexOf("-") + 1).ToUpper

        Dim UserCompID As String = ddlCompID.SelectedValue
        If UserCompID = "" Then
            UserCompID = UserProfile.SelectCompRoleID
        End If

        If strCompID <> UserCompID Then
            Bsp.Utility.ShowMessage(Me, "公司代碼不符合！")
            Return False
        End If

        If strEmpID <> txtEmpID.Text Then
            Bsp.Utility.ShowMessage(Me, "員工編號不符合！")
            Return False
        End If

        If Not IsEmpExists(strCompID, strEmpID) Then
            Bsp.Utility.ShowMessage(Me, "員工編號：" & strEmpID & "不存在！")
            Return False
        End If

        Return True
    End Function

    Private Sub DoUpload()
        'Dim SavePath As String = Server.MapPath("~/Upload/Photo/") & fuEmpPhotoUrl.FileName
        Dim CopyPath As String = Server.MapPath(Bsp.Utility.getAppSetting("EmpPhotoFolder")) & fuEmpPhotoUrl.FileName

        Try
            'fuEmpPhotoUrl.SaveAs(SavePath)
            fuEmpPhotoUrl.SaveAs(CopyPath)

            '測試環境
            Dim upRequest As FtpWebRequest = CType(FtpWebRequest.Create(Bsp.Utility.getAppSetting("FTP_Host") & fuEmpPhotoUrl.FileName), FtpWebRequest)
            upRequest.Credentials = New NetworkCredential(Bsp.Utility.getAppSetting("FTP_UserName"), Bsp.Utility.getAppSetting("FTP_Password"))
            upRequest.Method = WebRequestMethods.Ftp.UploadFile
            upRequest.UseBinary = True

            Dim bFile() As Byte = File.ReadAllBytes(CopyPath)
            Dim requestStream As Stream = upRequest.GetRequestStream()

            requestStream.Write(bFile, 0, bFile.Length)
            requestStream.Close()
            requestStream.Dispose()

            File.Delete(CopyPath) '刪除暫存照片

            '刪除FTP上的照片
            'Dim delRequest As FtpWebRequest = CType(FtpWebRequest.Create(Bsp.Utility.getAppSetting("FTP_Host") & fuEmpPhotoUrl.FileName), FtpWebRequest)
            'delRequest.Credentials = New System.Net.NetworkCredential(Bsp.Utility.getAppSetting("FTP_UserName"), Bsp.Utility.getAppSetting("FTP_Password"))
            'delRequest.Method = WebRequestMethods.Ftp.DeleteFile
            'Dim respose As FtpWebResponse = delRequest.GetResponse()

            DoClear()

            Bsp.Utility.ShowMessage(Me, "上傳成功！")
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoUpload", ex)
        End Try
    End Sub

    Public Overrides Sub DoModalReturn(ByVal returnValue As String)
        Dim strSql As String = ""

        If returnValue <> "" Then
            Dim aryData() As String = returnValue.Split(":")

            Select Case aryData(0)
                Case "ucSelectEmpID"
                    Dim aryValue() As String = Split(aryData(1), "|$|")
                    txtEmpID.Text = aryValue(1)
                    txtEmpName.Text = aryValue(2)
                    If UserProfile.SelectCompRoleID = "ALL" Then
                        Bsp.Utility.SetSelectedIndex(ddlCompID, aryValue(3))
                    End If
            End Select
        End If
    End Sub

    Protected Sub txtEmpID_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEmpID.TextChanged
        Dim objHR As New HR

        Dim strCompID As String = ddlCompID.SelectedValue
        If strCompID = "" Then
            strCompID = UserProfile.SelectCompRoleID
        End If

        If Not IsEmpExists(strCompID, txtEmpID.Text) Then
            txtEmpName.Text = ""
            Return
        Else
            txtEmpName.Text = objHR.GetHREmpName(strCompID, txtEmpID.Text).Rows(0).Item(0)
        End If

    End Sub

    Private Function IsEmpExists(ByVal strCompID As String, ByVal strEmpID As String) As Boolean
        Dim strSqlWhere As String = " And CompID = " & Bsp.Utility.Quote(strCompID).ToString() & " And EmpID = " & Bsp.Utility.Quote(strEmpID).ToString()

        Dim objHR As New HR
        If Not objHR.IsDataExists("Personal", strSqlWhere) Then
            Return False
        End If
        Return True
    End Function

End Class
