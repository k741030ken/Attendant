'****************************************************
'功能說明：上傳
'建立人員：Tsao
'建立日期：2014/03/12
'****************************************************
Imports System.Data
Imports System.Data.Common
Imports System.IO
Imports OfficeOpenXml

Partial Class SC_SC0443

    Inherits PageBase


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Overrides Sub BaseOnPageCall(ByVal ti As TransferInfo)
        Try
            If Not IsPostBack Then

            End If
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".BaseOnPageTransfer", ex)
        End Try
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnAdd"       '新增
                DoAdd()
            Case "btnActionX"
                GoBack()
        End Select
    End Sub

    Private Sub DoAdd()
        If Not myFileUpload.HasFile Then
            Bsp.Utility.ShowFormatMessage(Me.Page, "W_00120")
            Return
        End If
        Dim strMaxFileSize As String = Bsp.MySettings.UploadMaxFileSize
        Dim intFileSize As Integer = myFileUpload.FileBytes.Length

        If Not IsNumeric(strMaxFileSize) Then strMaxFileSize = "512"
        Dim strFileName As String = myFileUpload.FileName
        Dim strExtension As String = System.IO.Path.GetExtension(strFileName)
        Dim strActFileName As String = System.Guid.NewGuid().ToString().ToUpper() & strExtension
        Dim strAllowFileType As String = Bsp.MySettings.UploadAllowFileType
        Dim strUnlimitSizeFileType As String = Bsp.MySettings.UploadUnlimitSizeFileType

        strExtension = strExtension.ToUpper().Replace(".", "")

        If strAllowFileType.IndexOf(strExtension) >= 0 Then
            If strUnlimitSizeFileType.IndexOf(strExtension) < 0 Then
                If intFileSize > CInt(strMaxFileSize) * 1024 Then
                    Bsp.Utility.ShowFormatMessage(Me.Page, "E_00040", strMaxFileSize)
                    Return
                End If
            End If
        Else
            Bsp.Utility.ShowFormatMessage(Me.Page, "E_00030")
            Return
        End If

        Dim strPath As String = Server.MapPath(Bsp.MySettings.TempPath)
        Dim strSubPath As String = ""

        Try
            If Not System.IO.Directory.Exists(strPath & "\" & strSubPath) Then
                System.IO.Directory.CreateDirectory(strPath & "\" & strSubPath)
            End If
            myFileUpload.PostedFile.SaveAs(strPath & "\" & strSubPath & "\" & strActFileName)

            DoRead(strPath & "\" & strSubPath & "\" & strActFileName)

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoAdd", ex)
        End Try
    End Sub

    Private Sub DoRead(ByVal ExcelPath As String)

        Dim dt As New DataTable

        Dim column As DataColumn
        Dim row As DataRow

        Dim fs = New FileStream(ExcelPath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite)

        Dim strSQL As New StringBuilder
        Dim errFlag As String = ""

        errFlag = "Read Excel;"
        Using p As New OfficeOpenXml.ExcelPackage(fs)
            Dim epbook As ExcelWorkbook = p.Workbook
            Dim xlSheet1 As ExcelWorksheet = epbook.Worksheets(1)

            With dt
                For c As Integer = 0 To 3
                    column = New DataColumn()
                    .Columns.Add(column)
                Next
                For s1_r As Integer = 2 To 37
                    row = .NewRow()
                    For s1_c As Integer = 1 To 4
                        row.Item(s1_c - 1) = xlSheet1.Cells(s1_r, s1_c).Value
                    Next
                    .Rows.Add(row)
                Next
            End With

            

        End Using

        fs.Close()
        fs.Dispose()

        If dt.Rows.Count <= 0 Then
            Bsp.Utility.ShowMessage(Me, "無資料匯入")
            Return
        End If

        '數字check
        Dim strCheck As String = ""
        With dt
            For r As Integer = 0 To 35
                If Not IsNumeric(.Rows(r).Item(3).ToString().Trim()) Then
                    strCheck += "(" + .Rows(r).Item(0).ToString().Trim() + ")" + .Rows(r).Item(1).ToString().Trim() + "(" + .Rows(r).Item(2).ToString().Trim() + ") 限額非數值格式" & vbCrLf
                End If
            Next
        End With

        If strCheck <> "" Then
            Bsp.Utility.ShowMessage(Me, strCheck.ToString())
            Return
        End If

        Dim Year As String = ""
        Dim Type As String = ""
        Dim Rank As String = ""
        Dim RankLimit As String = ""


        errFlag = "汇入开始;"
        Using cn As DbConnection = Bsp.DB.getConnection()
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction()
            Try
                For r As Integer = 0 To dt.Rows.Count - 1
                    Year = dt.Rows(r).Item(0).ToString()
                    Type = Left(dt.Rows(r).Item(1).ToString(), 1)
                    Rank = dt.Rows(r).Item(2).ToString()
                    RankLimit = dt.Rows(r).Item(3).ToString()
                    errFlag = "匯入覆蓋資料;"
                    strSQL = New StringBuilder
                    strSQL.AppendLine("IF EXISTS(SELECT * FROM SC_RankLimit WHERE Year = " & Bsp.Utility.Quote(Year) & " AND Type = " & Bsp.Utility.Quote(Type) & " AND Rank = " & Bsp.Utility.Quote(Rank) & ")")
                    strSQL.AppendLine("BEGIN")
                    strSQL.AppendLine("    UPDATE SC_RankLimit SET RankLimit = " & RankLimit & ", LastChgID = " & Bsp.Utility.Quote(UserProfile.ActUserID) & ", LastChgDate = GETDATE()")
                    strSQL.AppendLine("    WHERE Year = " & Bsp.Utility.Quote(Year) & " AND Type = " & Bsp.Utility.Quote(Type) & " AND Rank = " & Bsp.Utility.Quote(Rank))
                    strSQL.AppendLine("END")
                    strSQL.AppendLine("ELSE")
                    strSQL.AppendLine("BEGIN")
                    strSQL.AppendLine("    INSERT SC_RankLimit(Year, Type, Rank, RankLimit, CreateDate, LastChgID, LastChgDate)")
                    strSQL.AppendLine("    SELECT " & Bsp.Utility.Quote(Year))
                    strSQL.AppendLine("         , " & Bsp.Utility.Quote(Type))
                    strSQL.AppendLine("         , " & Bsp.Utility.Quote(Rank))
                    strSQL.AppendLine("         , " & RankLimit)
                    strSQL.AppendLine("         , GETDATE()")
                    strSQL.AppendLine("         , " & Bsp.Utility.Quote(UserProfile.ActUserID))
                    strSQL.AppendLine("         , GETDATE()")
                    strSQL.AppendLine("END")

                    Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)
                Next

                tran.Commit()
                Bsp.Utility.ShowMessage(Me, "匯入成功", True)
                GoBack()
            Catch ex As Exception
                tran.Rollback()
                Bsp.Utility.ShowMessage(Me, errFlag.ToString() & ex.Message & ";" & strSQL.ToString())
            Finally
                tran.Dispose()
                cn.Close()
            End Try
        End Using

    End Sub

    Private Sub GoBack()
        Bsp.Utility.RunClientScript(Me, "window.top.close();")
    End Sub

End Class
