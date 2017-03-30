'****************************************************
'功能說明：HR9102人事處放行作業 - 放行
'建立人員：Ann
'建立日期：2014.10.01
'****************************************************
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Partial Class HR_HR9102
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
                lblApplyDate1h.Text = ht("SelectedApplyDate1").ToString()
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
    'Dim ti As TransferInfo = Me.StateTransfer
    'Me.TransferFramePage(ti.CallerUrl, Nothing, _
    '                     "chkReason=True", _
    '                     "ddlReason=" & ViewState.Item("Reason"), _
    '                "SelectCompID=" & ViewState.Item("CompID"), _
    '                "SelectEmpID=" & ViewState.Item("EmpID"), _
    '                "SelectedReason=" & ViewState.Item("Reason"), _
    '                "SelectedApplyDate=" & ViewState.Item("ApplyDate"), _
    '                "SelectedOldData=" & ViewState.Item("OldData"), _
    '                "SelectedRelativeIDNo=" & ViewState.Item("RelativeIDNo"), _
    '                "DoQuery=Y")

    Private Sub GetData(ByVal CompID As String, ByVal EmpID As String, ByVal RelativeIDNo As String, ByVal ApplyDate As String, ByVal Reason As String, ByVal OldData As String)
        Dim bsPersonalWait As New bePersonalWait.Service()
        Dim bePersonalWait As New bePersonalWait.Row()
        Dim objHR As New HR

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

                '最後異動公司
                Using dt1 As DataTable = objHR.GetHRCompName(bePersonalWait.LastChgComp.Value)
                    If dt1.Rows.Count = 0 Then
                        lblLastChgComp.Text = bePersonalWait.LastChgComp.Value
                    Else
                        lblLastChgComp.Text = bePersonalWait.LastChgComp.Value & "-" & dt1.Rows(0).Item("CompName").ToString()
                    End If
                End Using
                '最後異動者
                Using dt1 As DataTable = objHR.GetHREmpName(bePersonalWait.LastChgComp.Value, bePersonalWait.LastChgID.Value)
                    If dt1.Rows.Count = 0 Then
                        lblLastChgID.Text = bePersonalWait.LastChgID.Value
                    Else
                        lblLastChgID.Text = bePersonalWait.LastChgID.Value & "-" & dt1.Rows(0).Item("NameN").ToString()
                    End If
                End Using
                '最後異動時間
                lblLastChgDate.Text = bePersonalWait.LastChgDate.Value.ToString("yyyy/MM/dd HH:mm:ss")

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
        Dim objSC As New SC
        Dim objHR9100 As New HR9100        Dim strSQL As New StringBuilder

        If funCheckData() Then
            Using cn As DbConnection = Bsp.DB.getConnection()
                cn.Open()

                Dim tran As DbTransaction = cn.BeginTransaction
                Dim inTrans As Boolean = True
                Try
                    '放行註記
                    bePersonalWait.ReleaseMark.Value = "0"
                    '公司代碼
                    bePersonalWait.CompID.Value = lblCompID.Text
                    '員工編號
                    bePersonalWait.EmpID.Value = lblEmpID1.Text
                    subGetName(bePersonalWait.CompID.Value, bePersonalWait.EmpID.Value)
                    '申請日期
                    bePersonalWait.ApplyDate.Value = lblApplyDate1.Text
                    '異動原因
                    bePersonalWait.Reason.Value = ddlReason.SelectedItem.Value
                    '變更前
                    bePersonalWait.OldData.Value = lblOldData1.Text
                    '變更後
                    bePersonalWait.NewData.Value = lblNewData1.Text
                    '備註
                    bePersonalWait.Remark.Value = txtRemark.Text

                    bePersonalWait.LastChgComp.Value = UserProfile.ActCompID
                    bePersonalWait.LastChgID.Value = UserProfile.ActUserID
                    bePersonalWait.LastChgDate.Value = Format(Now, "yyyy/MM/dd HH:mm:ss")
                Catch ex As Exception
                    Bsp.Utility.ShowMessage(Me, Me.FunID & ".SaveData", ex)
                    Return False
                End Try
            End Using
            Try
                If ddlReason.SelectedItem.Value = "20" Then
                    objHR9100.UpdateHR9_PersonalWait(bePersonalWait)
                Else
                    strSQL.AppendLine("update PersonalWait ")
                    strSQL.AppendLine("set Remark = '" & txtRemark.Text & "', LastChgComp = '" & UserProfile.CompID & "' ,LastChgID = '" & UserProfile.UserID & "' , LastChgDate = getdate() ")
                    strSQL.AppendLine("where CompID = '" & ViewState.Item("CompID") & "'")
                    strSQL.AppendLine(" and EmpID ='" & ViewState.Item("EmpID") & "'")
                    'strSQL.AppendLine(" and ApplyDate ='" & Format(ViewState.Item("ApplyDate"), "yyyy/MM/dd HH:mm:ss") & "'")
                    strSQL.AppendLine(" and ApplyDate ='" & lblApplyDate1h.Text & "'")
                    strSQL.AppendLine(" and Reason ='" & ViewState.Item("Reason") & "'")
                    strSQL.AppendLine(" and OldData ='" & ViewState.Item("OldData") & "'")

                    Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")
                End If
            Catch ex As Exception
                Bsp.Utility.ShowMessage(Me, Me.FunID & ".SaveData", ex)
                Return False
            End Try
        End If
        Return True
    End Function

    Private Function funCheckData() As Boolean
        Dim objHR As New HR
        Dim strWhere As String = ""
        If ddlReason.SelectedItem.Value = "50" Then   '變更帳號時，先檢核是否同時有變更入帳比例，必須要先變更入帳比例，否則萬一有變更銀行代碼，異動後PK變了，會導致變更入帳比例失敗   '20100521 emma modify
            strWhere = " and CompID='" & lblCompID.Text
            strWhere &= " AND EmpID='" & lblEmpID1.Text
            strWhere &= " AND ReleaseMark='0' "
            strWhere &= " AND Reason='51' "

            If objHR.IsDataExists("PersonalWait", strWhere) Then
                Bsp.Utility.ShowMessage(Me, "此員工同時有「變更入帳比例」，請先執行此項異動放行！")
                Return False
            End If
        End If
        Return True
    End Function
End Class
