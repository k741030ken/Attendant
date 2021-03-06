'****************************************************
'功能說明：附件上傳
'建立人員：Chung
'建立日期：2013/01/29
'****************************************************
Imports System.Data

Partial Class WF_WFA060
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            If Bsp.Utility.IsStringNull(Request("DocType")) <> "" Then
                ViewState.Item("DocType") = Request("DocType").ToString()

            End If
        Else
            If StateMain IsNot Nothing Then
                sdsMain.SelectCommand = StateMain
            End If
        End If
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        'For Each strKey As String In Request.QueryString.Keys
        '    ViewState.Item(strKey) = Request.QueryString(strKey)
        'Next
        Object2ViewState(ti.Args)
        If ViewState.Item("FlowID") Is Nothing OrElse ViewState.Item("FlowID") = "" Then
            Dim strPage As String = Bsp.Utility.getMessagePageURL(Me.FunID, Bsp.Utility.MessageType.Errors, "傳遞參數錯誤(FlowID)！", "", "")
            Bsp.Utility.RunClientScript(Me, "window.parent.location=" & Bsp.Utility.Quote(strPage) & ";")
            Return
        End If
        If ViewState.Item("FlowCaseID") Is Nothing OrElse ViewState.Item("FlowCaseID") = "" Then
            Dim strPage As String = Bsp.Utility.getMessagePageURL(Me.FunID, Bsp.Utility.MessageType.Errors, "傳遞參數錯誤(FlowCaseID)！", "", "")
            Bsp.Utility.RunClientScript(Me, "window.parent.location=" & Bsp.Utility.Quote(strPage) & ";")
            Return
        End If
        If ViewState.Item("DocType") Is Nothing Then
            ViewState.Item("DocType") = "99"
        End If
        Dim objWF As New WF()
        Select Case Bsp.Utility.IsStringNull(ViewState.Item("UploadType"))
            Case "SingleCustomer"   '顯示個別公司上傳的附件
                StateMain = objWF.GetAttachmentbyID(ViewState.Item("AppID"), ViewState.Item("DocType"), ViewState.Item("CustomerID"))
            Case "FlowCustomer" '各關卡+ID
                StateMain = objWF.GetAttachmentQueryString(ViewState.Item("FlowID"), ViewState.Item("FlowCaseID"), ViewState.Item("DocType"), ViewState.Item("CustomerID"))
            Case "ALL" '顯示案件內所有的附件資料
                StateMain = objWF.GetAllAttachment(ViewState.Item("AppID"), ViewState.Item("DocType"))
            Case Else
                If Bsp.Utility.InStr(Bsp.Utility.IsStringNull(ViewState.Item("FlowID")), "CC", "DD") Then
                    StateMain = objWF.GetAttachmentQueryString(ViewState.Item("FlowID"), ViewState.Item("FlowCaseID"), ViewState.Item("DocType"), ViewState.Item("CustomerID"))
                Else
                    StateMain = objWF.GetAttachmentQueryString(ViewState.Item("FlowID"), ViewState.Item("FlowCaseID"), ViewState.Item("DocType"))
                End If
        End Select
        sdsMain.SelectCommand = CType(StateMain, String)

        Bsp.Utility.FillCommon(ddlDocType, "103", Bsp.Enums.SelectCommonType.Valid, Bsp.Enums.FullNameType.OnlyDefine)


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
                'DoDelete()
            Case Else
                DoOtherAction()   '其他功能動作
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

        Dim strPath As String = Server.MapPath(Bsp.MySettings.UploadPath)
        Dim strSubPath As String = ""
        Dim beAttachment As New beWF_Attachment.Row()
        Dim objWF As New WF()
        Dim FlowID As String = ViewState.Item("FlowID").ToString()
        Dim FlowCaseID As String = ViewState.Item("FlowCaseID").ToString()
        Dim DocType As String = ViewState.Item("DocType")

        strSubPath = String.Format("{0}\{1}\{2}-{3}", FlowCaseID.Substring(0, 4), FlowCaseID.Substring(4, 2), FlowID, FlowCaseID)

        Try
            If Not System.IO.Directory.Exists(strPath & "\" & strSubPath) Then
                System.IO.Directory.CreateDirectory(strPath & "\" & strSubPath)
            End If
            myFileUpload.PostedFile.SaveAs(strPath & "\" & strSubPath & "\" & strActFileName)

            With beAttachment
                .FlowID.Value = FlowID
                .FlowCaseID.Value = FlowCaseID
                .PaperID.Value = objWF.GetPaperID(FlowID, FlowCaseID)
                .DocType.Value = ViewState.Item("DocType").ToString()
                .FileName.Value = strFileName
                .ActFileName.Value = strActFileName
                .LastChgDate.Value = Now
                .LastChgID.Value = UserProfile.ActUserID
                .Path.Value = System.IO.Path.Combine(strPath, strSubPath)

                If Bsp.Utility.InStr(Bsp.Utility.IsStringNull(ViewState.Item("UploadType")), "FlowCustomer", "SingleCustomer") OrElse _
                   Bsp.Utility.InStr(Bsp.Utility.IsStringNull(ViewState.Item("FlowID")), "CC", "DD") Then
                    .CustomerID.Value = Bsp.Utility.IsStringNull(ViewState.Item("CustomerID"))
                End If

            End With

            Using cn As System.Data.Common.DbConnection = Bsp.DB.getConnection()
                cn.Open()
                Dim tran As System.Data.Common.DbTransaction = cn.BeginTransaction()
                Try
                    objWF.Insert_Attachment(beAttachment, tran)
                    tran.Commit()
                Catch ex As Exception
                    tran.Rollback()
                    Throw
                End Try
            End Using
            DoQuery()
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoAdd", ex)
        End Try
    End Sub

    Private Sub DoUpdate()

    End Sub

    Private Sub DoQuery()
        Try
            sdsMain.SelectCommand = StateMain
            gvMain.DataBind()
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
        End Try
    End Sub

    Private Sub DoDelete(ByVal FlowID As String, ByVal FlowCaseID As String, ByVal SeqNo As String)
        Dim objWF As New WF()
        Dim beAttachment As beWF_Attachment.Row

        Try
            Using dt As DataTable = objWF.GetAttachment(FlowID, FlowCaseID, Convert.ToInt32(SeqNo))
                If dt.Rows.Count = 0 Then Return
                beAttachment = New beWF_Attachment.Row(dt.Rows(0))
                objWF.Delete_Attachment(beAttachment)
            End Using
            gvMain.DataBind()
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoDelete", ex)
        End Try
    End Sub

    Private Sub DoOtherAction()

    End Sub

    Protected Sub gvMain_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMain.RowCommand
        Select Case e.CommandName
            Case "DeleteUploadData"
                Dim Args() As String = e.CommandArgument.ToString().Split(",")
                If Args.Length = 3 Then
                    DoDelete(Args(0), Args(1), Args(2))
                End If
            Case "OpenFile"
                Dim objWF As New WF()
                Dim Args() As String = e.CommandArgument.ToString().Split(",")

                Try
                    Using dt As DataTable = objWF.GetAttachment(Args(0), Args(1), Args(2))
                        If dt.Rows.Count > 0 Then
                            Dim beAttachment As New beWF_Attachment.Row(dt.Rows(0))
                            Dim strURL As String

                            strURL = Bsp.MySettings.TempPath & "/" & beAttachment.ActFileName.Value
                            System.IO.File.Copy( _
                                        System.IO.Path.Combine(beAttachment.Path.Value, beAttachment.ActFileName.Value), _
                                        System.IO.Path.Combine(Server.MapPath(Bsp.MySettings.TempPath), beAttachment.ActFileName.Value), True)

                            Bsp.Utility.RunClientScript(Me, "OpenWindow('" & strURL & "')")
                        End If
                    End Using
                Catch ex As Exception
                    Bsp.Utility.ShowMessage(Me, Me.FunID & ".gvMain_RowCommand", ex)
                End Try
        End Select
    End Sub

    Protected Sub gvMain_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvMain.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim objDelButton As LinkButton = e.Row.FindControl("btnDelete")
            Dim bolEnableDel As String = True

            If bolEnableDel Then
                'Update by Chung 2008.07.08 確認可以刪除後再判斷是否有權限刪除
                Dim objUserID As Label = e.Row.FindControl("LastChgID")

                If objUserID IsNot Nothing Then
                    If objUserID.Text = UserProfile.UserID Then
                        Dim strMsg As String = Bsp.Utility.getMessage("Q_00010")
                        objDelButton.Attributes.Add("onclick", "return confirm('" & strMsg & "');")
                        objDelButton.CommandArgument = String.Format("{0},{1},{2}", _
                                                                     gvMain.DataKeys(e.Row.RowIndex)("FlowID").ToString(), _
                                                                     gvMain.DataKeys(e.Row.RowIndex)("FlowCaseID").ToString(), _
                                                                     gvMain.DataKeys(e.Row.RowIndex)("SeqNo").ToString())
                    Else
                        objDelButton.Enabled = False
                    End If
                End If
            Else
                objDelButton.Enabled = False
            End If
        End If
    End Sub

End Class
