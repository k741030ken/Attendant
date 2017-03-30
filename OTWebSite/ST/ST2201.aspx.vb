'****************************************************
'功能說明：整批員工照片檔上傳
'建立人員：BeatriceCheng
'建立日期：2015.06.04
'****************************************************
Imports System.Data
Imports System.Net
Imports System.IO

Partial Class ST_ST2201
    Inherits System.Web.UI.Page

    Dim CopyPath As String = Server.MapPath(Bsp.Utility.getAppSetting("EmpPhotoFolder")) 'Bsp.Utility.getAppSetting("EmpPhotoFolder")
    Dim allowedExtensions As String() = {".jpg", ".jpeg"}
    Dim strFileName As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strCompRoleID As String = HttpContext.Current.Request("strCompRoleID").ToString()
        Dim strLogName = HttpContext.Current.Request("strLogName").ToString()
        Dim strFileName As String = ""
        Dim strMessage As String = ""
        Try

            For i As Integer = 0 To HttpContext.Current.Request.Files.Count - 1
                strFileName = HttpContext.Current.Request.Files(i).FileName
                If funCheckData(strFileName, strCompRoleID, strLogName) Then

                    HttpContext.Current.Request.Files(i).SaveAs(CopyPath & strFileName)

                    Dim upRequest As FtpWebRequest = CType(FtpWebRequest.Create(Bsp.Utility.getAppSetting("FTP_Host") & strFileName), FtpWebRequest)
                    upRequest.Credentials = New NetworkCredential(Bsp.Utility.getAppSetting("FTP_UserName"), Bsp.Utility.getAppSetting("FTP_Password"))
                    upRequest.Method = WebRequestMethods.Ftp.UploadFile
                    upRequest.UseBinary = True

                    Dim bFile() As Byte = File.ReadAllBytes(CopyPath & strFileName)
                    Dim requestStream As Stream = upRequest.GetRequestStream()

                    requestStream.Write(bFile, 0, bFile.Length)
                    requestStream.Close()
                    requestStream.Dispose()

                    File.Delete(CopyPath & strFileName) '刪除暫存照片

                    '刪除FTP上的照片
                    'Dim delRequest As FtpWebRequest = CType(FtpWebRequest.Create(Bsp.Utility.getAppSetting("FTP_Host") & strFileName), FtpWebRequest)
                    'delRequest.Credentials = New System.Net.NetworkCredential(Bsp.Utility.getAppSetting("FTP_UserName"), Bsp.Utility.getAppSetting("FTP_Password"))
                    'delRequest.Method = WebRequestMethods.Ftp.DeleteFile
                    'Dim respose As FtpWebResponse = delRequest.GetResponse()

                    Response.Write("Success")
                Else
                    Response.Write("Error")
                End If
            Next
        Catch ex As Exception
            strMessage += "檔案：「" + strFileName + "」出現錯誤！=>" + ex.Message
            subWriteLog(strMessage, strLogName)
            Response.Write("Error")
        End Try
    End Sub

    Private Function funCheckData(ByVal strFileName As String, ByVal strCompRoleID As String, ByVal strLogName As String) As Boolean
        Dim strMessage As String = ""

        If strFileName <> "" Then
            Dim fileOK As Boolean = False

            Dim fileExtensions As String = System.IO.Path.GetExtension(strFileName).ToLower()
            For Each extension In allowedExtensions
                If fileExtensions = extension Then
                    fileOK = True
                    Exit For
                End If
            Next

            If Not fileOK Then
                strMessage += "檔案：「" + strFileName + "」不符合JPG格式！"
                subWriteLog(strMessage, strLogName)
                Return False
            End If

            If strFileName.IndexOf("-") < 0 Then
                strMessage += "檔案：「" + strFileName + "」不符合『公司代碼-員編.jpg』格式！"
                subWriteLog(strMessage, strLogName)
                Return False
            End If

            Dim fileName As String = strFileName.ToLower.Replace(".jpg", "").Replace(".jpeg", "")
            Dim strCompID As String = fileName.Substring(0, strFileName.IndexOf("-")).ToUpper
            Dim strEmpID As String = fileName.Substring(strFileName.IndexOf("-") + 1).ToUpper

            If strCompID <> strCompRoleID Then
                strMessage += "檔案：「" + strFileName + "」的公司代碼不符合畫面條件！"
                subWriteLog(strMessage, strLogName)
                Return False
            End If

            If Not IsEmpExists(strCompID, strEmpID) Then
                strMessage += "檔案：「" + strFileName + "」的員工編號不存在！"
                subWriteLog(strMessage, strLogName)
                Return False
            End If

            Return True
        End If
        Return False
    End Function

    Private Sub subWriteLog(ByVal strLogString As String, ByVal strLogFileName As String)
        Dim strFileName As String = Server.MapPath(Bsp.Utility.getAppSetting("TempPath")) & "\" & strLogFileName
        System.IO.File.AppendAllLines(strFileName, {strLogString}, Encoding.UTF8)
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
