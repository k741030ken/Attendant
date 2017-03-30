'****************************************************
'功能說明：HR9101人事處放行作業 - 駁回
'建立人員：Ann
'建立日期：2014.10.01
'****************************************************
Imports System.Data
Imports System.Data.Common

Partial Class HR_HR9101
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            Dim objSC As New SC
            Dim objHR9100 As New HR9100
            HR9100.FillPersonalReason(ddlReason)
        End If
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

            If ht.ContainsKey("SelectedEmpID") Then
                ViewState.Item("CompID") = ht("SelectedCompID").ToString()
                ViewState.Item("EmpID") = ht("SelectedEmpID").ToString()
                ViewState.Item("RelativeIDNo") = ht("SelectedRelativeIDNo").ToString()
                ViewState.Item("ApplyDate") = ht("SelectedApplyDate").ToString()
                ViewState.Item("Reason") = ht("SelectedReason").ToString()
                ViewState.Item("OldData") = ht("SelectedOldData").ToString()
                GetData(ht("SelectedCompID").ToString(), ht("SelectedEmpID").ToString(), ht("SelectedRelativeIDNo").ToString(), ht("SelectedApplyDate").ToString(), ht("SelectedReason").ToString(), ht("SelectedOldData").ToString())
            Else
                Return
            End If
        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnUpdate"
                If Not funCheckData() Then
                    Return
                End If
                If SaveData() Then
                    GoBack()
                End If
            Case "btnActionX"
                GoBack()
        End Select
    End Sub

    Private Sub GoBack()
        Dim ti As TransferInfo = Me.StateTransfer
        Me.TransferFramePage(ti.CallerUrl, Nothing, ti.Args)
    End Sub

    Private Sub GetData(ByVal CompID As String, ByVal EmpID As String, ByVal RelativeIDNo As String, ByVal ApplyDate As String, ByVal Reason As String, ByVal OldData As String)
        Dim bsPersonalWait As New bePersonalWait.Service()
        Dim bePersonalWait As New bePersonalWait.Row()

        Try
            bePersonalWait.CompID.Value = CompID
            bePersonalWait.EmpID.Value = EmpID
            bePersonalWait.RelativeIDNo.Value = RelativeIDNo
            bePersonalWait.ApplyDate.Value = ApplyDate
            bePersonalWait.Reason.Value = Reason
            bePersonalWait.OldData.Value = OldData

            Using dt As DataTable = bsPersonalWait.QueryByKey(bePersonalWait).Tables(0)
                Dim strSQL As New StringBuilder
                Dim strSQLE As New StringBuilder    '學歷
                Dim strSQLS As New StringBuilder    '學校
                Dim strSQLD As New StringBuilder    '科系
                Dim strSQLD1 As New StringBuilder   '輔系
                Dim strSQLL As New StringBuilder    '證照
                Dim strSQLL1 As New StringBuilder   '證照1
                Dim strSQLT As New StringBuilder    '扶養親屬

                If dt.Rows.Count <= 0 Then Exit Sub
                bePersonalWait = New bePersonalWait.Row(dt.Rows(0))
                lblCompID.Text = bePersonalWait.CompID.Value
                lblEmpID1.Text = bePersonalWait.EmpID.Value
                subGetName(bePersonalWait.CompID.Value, bePersonalWait.EmpID.Value)
                lblEmpID1.Text = bePersonalWait.EmpID.Value
                Bsp.Utility.SetSelectedIndex(ddlReason, bePersonalWait.Reason.Value)

                lblApplyDate1.Text = bePersonalWait.ApplyDate.Value
                Select Case Reason
                    Case "05"   '新增學歷
                        lblOldData1.Text = ""
                        lblNewData1.Text = bePersonalWait.OldData.Value
                    Case "07"   '新增證照
                        lblOldData1.Text = ""
                        lblNewData1.Text = bePersonalWait.OldData.Value
                    Case Else
                        lblOldData1.Text = bePersonalWait.OldData.Value
                        lblNewData1.Text = bePersonalWait.NewData.Value
                End Select

                If Reason = "05" Then
                    Dim strOldData As String = OldData
                    Dim strNewData As String = bePersonalWait.NewData.Value

                    Dim aryOldData() As String = Split(strOldData, Chr(9))
                    Dim aryNewData() As String = Split(strNewData, Chr(9))

                    '學歷
                    strSQLE.AppendLine(" Select EduName ")
                    strSQLE.AppendLine(" from EduDegree ")
                    strSQLE.AppendLine(" where EduID = '" & aryOldData(0) & "' ")
                    Dim strEduName As String
                    'If strSQLE.ToString.Count > 0 Then
                    Dim strCountE As String = Bsp.DB.ExecuteScalar(strSQLE.ToString(), "eHRMSDB")
                    If strCountE <> "" Then
                        strEduName = ""
                        strEduName = Bsp.DB.ExecuteScalar(strSQLE.ToString(), "eHRMSDB")
                    End If

                    '學校
                    strSQLS.AppendLine(" Select Remark ")
                    strSQLS.AppendLine(" from School ")
                    strSQLS.AppendLine(" where SchoolID = '" & aryOldData(3) & "' ")
                    Dim strSchool As String
                    'If strSQLS.ToString.Count > 0 Then
                    Dim strCountS As String = Bsp.DB.ExecuteScalar(strSQLS.ToString(), "eHRMSDB")
                    If strCountS <> "" Then
                        strSchool = Bsp.DB.ExecuteScalar(strSQLS.ToString(), "eHRMSDB")
                    Else
                        strSchool = Trim(aryOldData(6))
                    End If

                    '科系
                    strSQLD.AppendLine(" Select Remark ")
                    strSQLD.AppendLine(" from Depart ")
                    strSQLD.AppendLine(" where DepartID = '" & aryOldData(4) & "' ")
                    Dim strDepart As String
                    Dim strCountD As String = Bsp.DB.ExecuteScalar(strSQLD.ToString(), "eHRMSDB")
                    If strCountD <> "" Then
                        strDepart = Bsp.DB.ExecuteScalar(strSQLD.ToString(), "eHRMSDB")
                    Else
                        strDepart = Trim(aryNewData(0))
                    End If
                    '輔系
                    strSQLD1.AppendLine(" Select Remark ")
                    strSQLD1.AppendLine(" from Depart ")
                    strSQLD1.AppendLine(" where DepartID = '" & aryOldData(5) & "' ")
                    Dim strSecDepart As String
                    Dim strCountD1 As String = Bsp.DB.ExecuteScalar(strSQLD1.ToString(), "eHRMSDB")
                    If strCountD1 <> "" Then
                        strSecDepart = Bsp.DB.ExecuteScalar(strSQLD1.ToString(), "eHRMSDB")
                    Else
                        strSecDepart = Trim(aryNewData(1))
                    End If

                    lblNewData1.Text = "新增 "
                    lblNewData1.Text = lblNewData1.Text & aryOldData(2) & "年度 "
                    lblNewData1.Text = lblNewData1.Text & strSchool & " "
                    lblNewData1.Text = lblNewData1.Text & strDepart
                    If strSecDepart > "" And strDepart > "" Then
                        lblNewData1.Text = lblNewData1.Text & "/"
                    End If
                    If strSecDepart > "" Then
                        lblNewData1.Text = lblNewData1.Text & strSecDepart & "(輔系)"
                    End If
                    lblNewData1.Text = lblNewData1.Text & " " & strEduName & " 學歷申請"
                Else
                    lblOldData1.Text = bePersonalWait.OldData.Value
                    '變更後
                    lblNewData1.Text = bePersonalWait.NewData.Value
                End If
                txtRemark.Text = bePersonalWait.Remark.Value
            End Using

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & "subGetData", ex)
        End Try

    End Sub

    '取得Name
    Private Sub subGetName(ByVal CompID As String, ByVal EmpID As String)
        Dim strName As String
        Dim objHR9100 As New HR9100

        strName = HR9100.HR9_EmpName(CompID, EmpID)
        lblEmpName.Text = strName

    End Sub
    Private Function SaveData() As Boolean
        Dim bePersonalWait As New bePersonalWait.Row()
        Dim bsPersonalWait As New bePersonalWait.Service()
        Dim beMailLog As New beMailLog.Row()
        Dim objSC As New SC
        Dim objHR As New HR
        Dim objHR9100 As New HR9100
        Dim strSQL As New StringBuilder()
        Dim strSQLD As New StringBuilder()

        If funCheckData() Then
            Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction
                Dim inTrans As Boolean = True

                Try
                    Using dt As DataTable = funMail()
                        If dt.Rows.Count > 0 Then
                            Dim strClerk As String = ""
                            Dim strClerkTel As String = ""

                            If strClerk = "" Then
                                strClerk = dt.Rows.Item(0)("Name").ToString()
                                strClerkTel = dt.Rows.Item(0)("Telephone").ToString()
                            Else
                                strClerk = strClerk & ";" & dt.Rows.Item(0)("Name").ToString()
                                strClerkTel = strClerkTel & ";" & dt.Rows.Item(0)("Telephone").ToString()
                            End If

                            strSQL.AppendLine(" Select ReasonName from PersonalReason where Reason =  '" & ddlReason.SelectedItem.Value & "'")
                            Dim strCount As String = Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")
                            Dim strReason As String
                            If strCount <> "" Then
                                strReason = Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")
                            Else
                                strReason = ""
                            End If

                            Dim strDate As String
                            strDate = Format(Now, "yyyy/MM/dd HH:mm:ss")

                            Dim intMailSeq As Integer = objHR.GetMailSeq(strDate)
                            With beMailLog
                                beMailLog.Acceptor.Value = lblEmpID1.Text
                                beMailLog.AcceptorCompID.Value = lblCompID.Text
                                beMailLog.Attachment.Value = ""
                                beMailLog.CreateTime.Value = strDate
                                beMailLog.EMail.Value = objHR.GetEMailAddress(lblCompID.Text, lblEmpID1.Text)
                                beMailLog.Sender.Value = Bsp.Utility.getAppSetting("sysHRName").ToString()
                                beMailLog.Seq.Value = intMailSeq
                                beMailLog.Subject.Value = "您於" & Bsp.Utility.getAppSetting("sysName").ToString() & strReason & "之申請駁回通知"
                                beMailLog.Content.Value = "PersonalWait2||BM@ApplyDate||" & Format(lblApplyDate1.Text, "yyyy/MM/dd HH:mm:ss")
                                beMailLog.Content.Value = beMailLog.Content.Value & "||BM@Reason||" & strReason
                                beMailLog.Content.Value = beMailLog.Content.Value & "||BM@Remark||" & Trim(txtRemark.Text)
                                beMailLog.Content.Value = beMailLog.Content.Value & "||BM@Clerk||" & strClerk
                                beMailLog.Content.Value = beMailLog.Content.Value & "||BM@ClerkTel||" & strClerkTel
                            End With

                            objHR.AddMailLog(beMailLog, False)

                            If strReason = "20" Then
                                strSQLD.AppendLine(" "" ")
                                strSQLD.AppendLine(" Delete TaxFamily ")
                                strSQLD.AppendLine(" where EmpID = '" & lblEmpID1.Text & "' ")
                                strSQLD.AppendLine(" and ReleaseMark = '0' ")
                                strSQLD.AppendLine(" and CompID = '" & lblCompID.Text & "' ")
                                Bsp.DB.ExecuteScalar(strSQLD.ToString(), "eHRMSDB")
                            End If

                            '放行註記
                            bePersonalWait.ReleaseMark.Value = "9"
                            '公司代碼
                            bePersonalWait.CompID.Value = lblCompID.Text
                            '員工編號
                            bePersonalWait.EmpID.Value = lblEmpID1.Text
                            '申請日期
                            bePersonalWait.ApplyDate.Value = lblApplyDate1.Text
                            '眷屬身分證字號
                            bePersonalWait.RelativeIDNo.Value = ViewState.Item("RelativeIDNo")
                            '異動原因
                            bePersonalWait.Reason.Value = ddlReason.SelectedItem.Value
                            '變更前
                            bePersonalWait.OldData.Value = ViewState.Item("OldData") 'lblOldData1.Text
                            If bePersonalWait.OldData.Value = "" Then
                                bePersonalWait.OldData.Value = "   "
                            End If
                            '變更後
                            'bePersonalWait.NewData.Value = lblNewData1.Text
                            '備註
                            bePersonalWait.Remark.Value = txtRemark.Text

                            bePersonalWait.ReleaseComp.Value = UserProfile.SelectCompRoleID
                            bePersonalWait.ReleaseEmpID.Value = UserProfile.UserID
                            bePersonalWait.ReleaseDate.Value = Format(Now, "yyyy/MM/dd HH:mm:ss")

                            Try
                                Return objHR9100.UpdateHR9_PersonalWait(bePersonalWait)
                            Catch ex As Exception
                                Bsp.Utility.ShowMessage(Me, Me.FunID & ".SaveData", ex)
                                Return False
                            End Try
                        End If
                    End Using
                    tran.Commit()
                Catch ex As Exception
                    If inTrans Then tran.Rollback()
                    Return False
                End Try
            End Using
        End If
        Return True
    End Function
    Private Function funMail() As DataTable
        Dim strSQL As New StringBuilder()
        Dim strWhere As New StringBuilder()

        strSQL.AppendLine(" Select isnull(P.NameN,'') Name ,isnull(M.Telephone,'') Telephone  from Maintain M ")
        strSQL.AppendLine(" inner join Personal P ")
        strSQL.AppendLine(" on M.EmpID = P.EmpID and M.EmpComp = P.CompID ")
        strSQL.AppendLine(" where M.FunctionID = '100' and M.Role = '2' and M.CompID = '" & UserProfile.SelectCompRoleID & "'")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
    Private Function funCheckData() As Boolean
        If txtRemark.Text.Trim() = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "備註")
            txtRemark.Focus()
            Return False
        Else
            If Bsp.Utility.getStringLength(txtRemark.Text.Trim()) > txtRemark.MaxLength Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00040", lblRemark.Text, txtRemark.MaxLength)
                txtRemark.Focus()
                Return False
            End If
        End If
        Return True
    End Function
End Class
