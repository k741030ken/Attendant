'******************************************************************************
'功能說明：HR9100人事處放行作業Function
'建立人員：Ann
'建立日期：2014.09.15
'******************************************************************************
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.IO
Imports System

Enum DisplayType
    OnlyName    '只顯示名字
    OnlyID           '顯示ID  
    Full        '顯示ID + 名字
End Enum

Public Class HR9100
#Region "HR9100..PersonalReason異動原因"
    Public Shared Sub FillPersonalReason(ByVal objDDL As DropDownList)
        Dim objHR9100 As New HR9100

        Try
            Using dt As DataTable = objHR9100.GetPersonalReasonInfo("", "Rtrim(Reason) as Reason,ReasonName", "And HR9100Flag = '1'")
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "ReasonName"
                    .DataValueField = "Reason"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetPersonalReasonInfo(ByVal Reason As String, Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames = "" Then FieldNames = "*"

        'strSQL.AppendLine("Select " & FieldNames)
        strSQL.AppendLine("Select Reason, Reason + '-' + ReasonName as ReasonName")
        strSQL.AppendLine("From PersonalReason")
        strSQL.AppendLine("Where 1 = 1")
        If Reason <> "" Then strSQL.AppendLine("And Reason = " & Bsp.Utility.Quote(Reason))
        If CondStr <> "" Then strSQL.AppendLine(CondStr)
        strSQL.AppendLine("Order by Reason")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

#End Region
    Public Function QueryEmployeeWait(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()
        Dim strFieldNames As New StringBuilder()

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine("And SysID like " & Bsp.Utility.Quote(ht(strKey).ToString() & "%"))
                    Case "ValidOrNot"
                        strSQL.AppendLine("And SysName like " & Bsp.Utility.Quote(ht(strKey).ToString() & "%"))
                    Case "EmpID"
                        strSQL.AppendLine("And SysID like " & Bsp.Utility.Quote(ht(strKey).ToString() & "%"))
                    Case "Name"
                        strSQL.AppendLine("And SysName like " & Bsp.Utility.Quote(ht(strKey).ToString() & "%"))
                    Case "ValidDate"
                        strSQL.AppendLine("And SysName like " & Bsp.Utility.Quote(ht(strKey).ToString() & "%"))
                    Case "Reason"
                        strSQL.AppendLine("And SysName like " & Bsp.Utility.Quote(ht(strKey).ToString() & "%"))
                End Select
            End If
        Next

        strFieldNames.AppendLine("E.ValidMark,E.Seq,")
        strFieldNames.AppendLine("isnull(C.CompName,'') as CompName,E.CompID,")
        strFieldNames.AppendLine("E.EmpID,isnull(P.NameN,'') as Name,E.ValidDate,E.Reason,isnull(ER.Remark,'') as ReasonName,")
        strFieldNames.AppendLine("isnull(C1.CompName,'') as NewCompName,E.NewCompID,")
        strFieldNames.AppendLine("isnull(B.OrganName,'') as NewGroupName,E.GroupID,")
        strFieldNames.AppendLine("isnull(O1.OrganName,'') as NewDeptName,E.DeptID,")
        strFieldNames.AppendLine("isnull(O2.OrganName,'') as NewOrganName,E.OrganID")

        Return GetEmployeeWaitInfo("", strSQL.ToString())
    End Function
    Public Function GetEmployeeWaitInfo(Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames = "" Then FieldNames = "*"

        strSQL.AppendLine("Select " & FieldNames)
        strSQL.AppendLine("From EmployeeWait E")
        strSQL.AppendLine("Left join Personal P on E.CompID = P.CompID and E.EmpID = P.EmpID")
        strSQL.AppendLine("Left join Company C on E.CompID = C.CompID")
        strSQL.AppendLine("Left join Company C1 on E.NewCompID = C1.CompID")
        strSQL.AppendLine("Left join Organization O1 on E.NewCompID = O1.CompID and E.DeptID = O1.OrganID")
        strSQL.AppendLine("Left join Organization O2 on E.NewCompID = O2.CompID and E.OrganID = O2.OrganID")
        strSQL.AppendLine("Left join OrganizationFlow B on E.GroupID = B.OrganID and B.OrganID = B.GroupID")
        strSQL.AppendLine("Left join EmployeeReason ER on E.Reason = ER.Reason and ER.EmployeeWaitFlag='1'")
        strSQL.AppendLine("Where 1 = 1")
        If CondStr <> "" Then strSQL.AppendLine(CondStr)

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

#Region "HR9100..PersonalWait查詢"

    Public Function GetSC_PersonalWait(ByVal ParamArray Params() As String) As DataTable
        Dim strSQL As New StringBuilder()
        Dim strWhere As New StringBuilder()
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    'Case "CompID"
                    '    strWhere.AppendLine("And CompID like " & Bsp.Utility.Quote(ht(strKey).ToString() & "%"))
                    Case "P.EmpID"
                        strWhere.AppendLine("And EmpID like " & Bsp.Utility.Quote("%" & ht(strKey).ToString() & "%"))
                        'Case "IDNo"
                        '    strWhere.AppendLine("And RR.IDNo like " & Bsp.Utility.Quote("%" & ht(strKey).ToString() & "%"))
                    Case "P1.NameN"
                        strWhere.AppendLine("And NameN like N" & Bsp.Utility.Quote("%" & ht(strKey).ToString() & "%"))
                End Select
            End If
        Next

        strSQL.AppendLine(" Select ReleaseMark, P.CompID, P.EmpID, P1.NameN, RelativeIDNo, ApplyDate, P.Reason, R.ReasonName, ReleaseEmpID, ReleaseDate, Remark, Remark1, Old, New")
        strSQL.AppendLine(" from PersonalWait P ")
        strSQL.AppendLine(" left join Personal P1 on P.CompID = P1.CompID and P.EmpID = P1.EmpID ")
        strSQL.AppendLine(" left join PersonalReason R on P.Reason = R.Reason ")
        strSQL.AppendLine(" where ReleaseMark = '0' ")
        strSQL.AppendLine(" and P.Reason  in (Select Reason from PersonalReason where HR9100Flag = '1') ")
        strSQL.AppendLine(" and P.CompID = '" & UserProfile.SelectCompRoleID & "'")
        strSQL.AppendLine(" order by P.Reason,EmpID,ApplyDate ")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

    Public Shared Function HR9_EmpName(ByVal CompID As String, ByVal EmpID As String) As String
        Dim strSQL As String

        strSQL = "select NameN from Personal where CompID = " & Bsp.Utility.Quote(CompID) & " and EmpID = " & Bsp.Utility.Quote(EmpID)
        Return Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")

    End Function

    '寫入 TaxFamily / 扶養親屬檔
    Public Function funUpdateTables(ByVal strCompID As String, ByVal strEmpID As String, ByVal strReason As String, ByVal strRelativeIDNo As String, ByVal ApplyDate As String, ByVal strOldData As String) As DataTable

        Dim strSQL As New StringBuilder

        strSQL.AppendLine(" select  ReleaseMark,  NewData, ReleaseComp, ReleaseEmpID, ReleaseDate, Remark, Remark1, LastChgComp, LastChgID, LastChgDate ")
        strSQL.AppendLine(" from PersonalWait ")
        strSQL.AppendLine(" where CompID = " & Bsp.Utility.Quote(strCompID) & " ")
        strSQL.AppendLine(" and EmpID = " & Bsp.Utility.Quote(strEmpID) & " ")
        strSQL.AppendLine(" and Reason = " & Bsp.Utility.Quote(strReason) & " ")
        strSQL.AppendLine(" and RelativeIDNo = " & Bsp.Utility.Quote(strRelativeIDNo) & " ")
        strSQL.AppendLine(" and ApplyDate = " & Bsp.Utility.Quote(ApplyDate) & " ")
        strSQL.AppendLine(" and OldData = " & Bsp.Utility.Quote(strOldData) & " ")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

    Public Function funUpdate(ByVal strCompID As String, ByVal strEmpID As String, ByVal strReason As String, ByVal strRelativeIDNo As String, ByVal strApplyDate As String, ByVal strOldData As String) As Boolean
        Dim strSQL As New StringBuilder
        Dim strSQLLog As New StringBuilder  'PersonalLog
        Dim strSQLS As New StringBuilder    '學校
        Dim strSQLD1 As New StringBuilder   '科系
        Dim strSQLD2 As New StringBuilder   '輔系
        Dim strSQLE As New StringBuilder    'Education

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try

                Using dt As DataTable = funUpdateTables(strCompID, strEmpID, strReason, strRelativeIDNo, strApplyDate, strOldData)
                    If dt.Rows.Count > 0 Then
                        Dim strReleaseMark As String
                        Dim strNewData As String
                        Dim strReleaseComp As String
                        Dim strReleaseEmpID As String
                        Dim strReleaseDate As String
                        Dim strRemark As String
                        Dim strRemark1 As String
                        Dim strLastChgComp As String
                        Dim strLastChgID As String
                        Dim strLastChgDate As String

                        strReleaseMark = dt.Rows.Item(0)("ReleaseMark").ToString()
                        strNewData = dt.Rows.Item(0)("NewData").ToString()
                        strReleaseComp = dt.Rows.Item(0)("ReleaseComp").ToString()
                        strReleaseEmpID = dt.Rows.Item(0)("ReleaseEmpID").ToString()
                        strReleaseDate = dt.Rows.Item(0)("ReleaseDate").ToString()
                        strRemark = dt.Rows.Item(0)("Remark").ToString()
                        strRemark1 = dt.Rows.Item(0)("Remark1").ToString()
                        strLastChgComp = dt.Rows.Item(0)("LastChgComp").ToString()
                        strLastChgID = dt.Rows.Item(0)("LastChgID").ToString()
                        strLastChgDate = dt.Rows.Item(0)("LastChgDate").ToString()

                        Using dt1 As DataTable = GetEmpName(strCompID, strEmpID)
                            If dt1.Rows.Count > 0 Then
                                Dim strNameN As String = dt1.Rows.Item(0)("Name").ToString()
                                Dim strBirthDate As String = Format(dt1.Rows.Item(0)("BirthDate"), "yyyy/MM/dd")

                                '寫入PersonalLog
                                strSQLLog.AppendLine(" Insert into PersonalLog ( ")
                                strSQLLog.AppendLine(" CompID,EmpID, RelativeIDNo, Name, BirthDate, RelativeID, ModifyDate, Reason, OldData, NewData, EffDate ,Remark ) ")
                                strSQLLog.AppendLine(" values ( ")
                                strSQLLog.AppendLine(" " & Bsp.Utility.Quote(strCompID) & " ")
                                strSQLLog.AppendLine(" ," & Bsp.Utility.Quote(strEmpID) & " ")
                                strSQLLog.AppendLine(" ,'' ")
                                strSQLLog.AppendLine(" ,N" & Bsp.Utility.Quote(strNameN) & " ")
                                strSQLLog.AppendLine(" ," & Bsp.Utility.Quote(strBirthDate) & " ")
                                strSQLLog.AppendLine(" ,'00' ")
                                strSQLLog.AppendLine(" ,'" & Format(Now, "yyyy/MM/dd HH:mm:ss") & "' ")
                                strSQLLog.AppendLine(" ," & Bsp.Utility.Quote(strReason) & " ")
                                strSQLLog.AppendLine(" ,N" & Bsp.Utility.Quote(strOldData) & " ")
                                strSQLLog.AppendLine(" ,N" & Bsp.Utility.Quote(strNewData) & " ")
                                strSQLLog.AppendLine(" ,'" & Format(Now, "yyyy/MM/dd HH:mm:ss") & "' ")
                                strSQLLog.AppendLine(" ," & Bsp.Utility.Quote(strRemark) & " ")
                                strSQLLog.AppendLine(" ) ")

                                strSQLLog.AppendLine(" update PersonalWait  ")
                                strSQLLog.AppendLine(" set ReleaseMark = '1'  ")
                                strSQLLog.AppendLine(" ,ReleaseComp = '" & UserProfile.SelectCompRoleID & "'  ")
                                strSQLLog.AppendLine(" ,ReleaseEmpID = '" & UserProfile.UserID & "'  ")
                                strSQLLog.AppendLine(" ,ReleaseDate = '" & Format(Now, "yyyy/MM/dd HH:mm:ss") & "'  ")
                                strSQLLog.AppendLine(" where CompID = " & Bsp.Utility.Quote(strCompID) & "  ")
                                strSQLLog.AppendLine(" and  EmpID = " & Bsp.Utility.Quote(strEmpID) & "  ")
                                strSQLLog.AppendLine(" and Reason = " & Bsp.Utility.Quote(strReason) & "  ")
                                strSQLLog.AppendLine(" and RelativeIDNo = " & Bsp.Utility.Quote(strRelativeIDNo) & "  ")
                                strSQLLog.AppendLine(" and ApplyDate = " & Bsp.Utility.Quote(strApplyDate) & "  ")
                                strSQLLog.AppendLine(" and OldData = " & Bsp.Utility.Quote(strOldData) & "  ")
                                Bsp.DB.ExecuteScalar(strSQLLog.ToString(), "eHRMSDB")
                            End If
                        End Using

                        Select Case strReason
                            Case "04"   '變更國籍
                                strSQL.AppendLine(" Update Personal set NationID = " & Bsp.Utility.Quote(strNewData) & " ")
                                strSQL.AppendLine(" , LastChgComp = '" & UserProfile.CompID & "' ")
                                strSQL.AppendLine(" , LastChgID = '" & UserProfile.UserID & "' ")
                                strSQL.AppendLine(" , LastChgDate = '" & Format(Now, "yyyy/MM/dd HH:mm:ss") & "' ")
                                strSQL.AppendLine("  where EmpID = " & Bsp.Utility.Quote(strEmpID) & " ")
                                strSQL.AppendLine("  and CompID = " & Bsp.Utility.Quote(strCompID) & " ")
                                Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")

                            Case "05"   '新增學歷
                                Dim aryEducationData() As String = Split(strOldData & Chr(9) & strNewData, Chr(9))
                                If UBound(aryEducationData) = 8 Then

                                    Dim intSeq As Integer
                                    intSeq = GetEducationSeq(strCompID, strEmpID, aryEducationData)
                                    Dim strNewEduID As String = Left(aryEducationData(0), 2)
                                    Dim nstrAdvStudyEduID As String = "080"
                                    Dim strNewEduIDP As String = GetEduID(strCompID, strEmpID)

                                    '學校
                                    strSQLS.AppendLine(" Select Remark ")
                                    strSQLS.AppendLine(" from School ")
                                    strSQLS.AppendLine(" where SchoolID = '" & aryEducationData(3) & "' ")
                                    Bsp.DB.ExecuteScalar(strSQLS.ToString(), "eHRMSDB")
                                    If Not Bsp.DB.ExecuteScalar(strSQLS.ToString(), "eHRMSDB") = Nothing Then
                                        aryEducationData(6) = Bsp.DB.ExecuteScalar(strSQLS.ToString(), "eHRMSDB")
                                    End If
                                    '科系
                                    strSQLD1.AppendLine(" Select Remark ")
                                    strSQLD1.AppendLine(" from Depart ")
                                    strSQLD1.AppendLine(" where DepartID = '" & aryEducationData(4) & "' ")
                                    Bsp.DB.ExecuteScalar(strSQLD1.ToString(), "eHRMSDB")
                                    If Not Bsp.DB.ExecuteScalar(strSQLD1.ToString(), "eHRMSDB") = Nothing Then
                                        aryEducationData(7) = Bsp.DB.ExecuteScalar(strSQLD1.ToString(), "eHRMSDB")
                                    End If
                                    '輔系
                                    strSQLD2.AppendLine(" Select Remark ")
                                    strSQLD2.AppendLine(" from Depart ")
                                    strSQLD2.AppendLine(" where DepartID = '" & aryEducationData(5) & "' ")
                                    Bsp.DB.ExecuteScalar(strSQLD2.ToString(), "eHRMSDB")
                                    If Not Bsp.DB.ExecuteScalar(strSQLD2.ToString(), "eHRMSDB") = Nothing Then
                                        aryEducationData(8) = Bsp.DB.ExecuteScalar(strSQLD2.ToString(), "eHRMSDB")
                                    End If

                                    strSQLE.AppendLine(" Insert into Education ( ")
                                    strSQLE.AppendLine(" IDNo, EduID, Seq, GraduateYear, SchoolID, DepartID, SecDepartID ")
                                    'strSQLE.AppendLine(" , School, Depart, SecDepart ) ")  '20160219 Ann modify 增加預設帶1畢業,增加最後異動公司/人員/日期
                                    strSQLE.AppendLine(" , School, Depart, SecDepart, EduStatus, LastChgComp, LastChgID, LastChgDate ) ")   '20160219 Ann modify 增加預設帶1畢業,增加最後異動公司/人員/日期
                                    strSQLE.AppendLine(" Select IDNo ")
                                    strSQLE.AppendLine(" , '" & aryEducationData(0) & "' ")
                                    strSQLE.AppendLine(" , '" & intSeq & "' ")
                                    strSQLE.AppendLine(" , '" & aryEducationData(2) & "' ")
                                    strSQLE.AppendLine(" , '" & aryEducationData(3) & "' ")
                                    strSQLE.AppendLine(" , '" & aryEducationData(4) & "' ")
                                    strSQLE.AppendLine(" , '" & aryEducationData(5) & "' ")
                                    strSQLE.AppendLine(" , '" & aryEducationData(6) & "' ")
                                    strSQLE.AppendLine(" , '" & aryEducationData(7) & "' ")
                                    strSQLE.AppendLine(" , '" & aryEducationData(8) & "' ")
                                    'strSQLE.AppendLine(" , '" & aryEducationData(8) & "' from  Personal  ")
                                    strSQLE.AppendLine(" , '1' ")   '20160219 Ann modify 增加預設帶1畢業,增加最後異動公司/人員/日期
                                    strSQLE.AppendLine(" , '" & UserProfile.CompID & "' ")   '20160219 Ann modify 增加預設帶1畢業,增加最後異動公司/人員/日期
                                    strSQLE.AppendLine(" , '" & UserProfile.UserID & "' ")   '20160219 Ann modify 增加預設帶1畢業,增加最後異動公司/人員/日期
                                    strSQLE.AppendLine(" , '" & Format(Now, "yyyy/MM/dd HH:mm:ss") & "' from  Personal ")   '20160219 Ann modify 增加預設帶1畢業,增加最後異動公司/人員/日期
                                    strSQLE.AppendLine(" where CompID = " & Bsp.Utility.Quote(strCompID) & " ")
                                    strSQLE.AppendLine(" and EmpID  = " & Bsp.Utility.Quote(strEmpID) & " ")
                                    Bsp.DB.ExecuteScalar(strSQLE.ToString(), "eHRMSDB")

                                    '檢查(是否)異動[Personal]最高學歷

                                    If strNewEduID <> nstrAdvStudyEduID Then    'nstrAdvStudyEduID --> "進修"
                                        strSQL.AppendLine(" Select EduID from Personal ")
                                        strSQL.AppendLine(" where EmpID = " & Bsp.Utility.Quote(strEmpID) & " ")
                                        strSQL.AppendLine(" and CompID = " & Bsp.Utility.Quote(strCompID) & " ")

                                        If strNewEduID > Left(Trim(strNewEduIDP), 2) Then
                                            strSQL.AppendLine(" Update Personal ")
                                            strSQL.AppendLine(" set EduID = '" & strNewEduID & "0' ")
                                            strSQL.AppendLine(" ,LastChgComp = '" & UserProfile.CompID & "' ")
                                            strSQL.AppendLine(" ,LastChgID   = '" & UserProfile.UserID & "' ")
                                            strSQL.AppendLine(" ,LastChgDate = '" & Format(Now, "yyyy/MM/dd HH:mm:ss") & "' ")
                                            strSQL.AppendLine(" where EmpID = " & Bsp.Utility.Quote(strEmpID) & " ")
                                            strSQL.AppendLine(" and CompID = " & Bsp.Utility.Quote(strCompID) & " ")
                                        End If      'strNewEduID > Left(Trim(adoRs!EduID), 2)
                                    End If
                                    Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")
                                End If

                            Case "06"   '變更戶籍地址
                                strSQL.AppendLine(" Update Communication set RegAddr = " & Bsp.Utility.Quote(strNewData) & " ")
                                strSQL.AppendLine(" , LastChgComp = '" & UserProfile.CompID & "' ")
                                strSQL.AppendLine(" , LastChgID = '" & UserProfile.UserID & "' ")
                                strSQL.AppendLine(" , LastChgDate = '" & Format(Now, "yyyy/MM/dd HH:mm:ss") & "' ")
                                strSQL.AppendLine(" from Communication C left join Personal P on C.IDNo = P.IDNo ")
                                strSQL.AppendLine(" where P.EmpID = " & Bsp.Utility.Quote(strEmpID) & " ")
                                strSQL.AppendLine(" and P.CompID = " & Bsp.Utility.Quote(strCompID) & " ")
                                Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")

                            Case "20"   '扶養親屬
                                'strSQL.AppendLine(" update S ")
                                'strSQL.AppendLine(" set S.TaxRearNo = SUBSTRING(W.NewData, 1, Len(W.NewData) - 1) ")

                                strSQL.AppendLine(" Update SalaryData set TaxRearNo = '" & Mid(Trim(strNewData), 1, Len(Trim(strNewData)) - 1) & "' ")
                                strSQL.AppendLine(" , LastChgID = '" & UserProfile.UserID & "' ")
                                strSQL.AppendLine(" , LastChgDate = '" & Format(Now, "yyyy/MM/dd HH:mm:ss") & "' ")
                                strSQL.AppendLine(" , LastChgComp = '" & UserProfile.CompID & "' ")
                                strSQL.AppendLine("  where EmpID = " & Bsp.Utility.Quote(strEmpID) & " ")
                                strSQL.AppendLine("  and  CompID = " & Bsp.Utility.Quote(strCompID) & " ")
                                Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")

                                strSQL.AppendLine(" Delete TaxFamily ")
                                strSQL.AppendLine(" where EmpID = " & Bsp.Utility.Quote(strEmpID) & " ")
                                strSQL.AppendLine(" and ReleaseMark = '1'")
                                strSQL.AppendLine(" and CompID = " & Bsp.Utility.Quote(strCompID) & " ")
                                Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")

                                strSQL.AppendLine(" update TaxFamily ")
                                strSQL.AppendLine(" set ReleaseMark = '1' , CloseDate = '" & Format(Now, "yyyy/MM/dd HH:mm:ss") & "' ")
                                strSQL.AppendLine(" where EmpID = " & Bsp.Utility.Quote(strEmpID) & " ")
                                strSQL.AppendLine(" and  CompID = " & Bsp.Utility.Quote(strCompID) & " ")
                                Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")

                            Case "50"   '變更帳號
                                Dim arraynewaccount() As String
                                Dim arrayoldaccount() As String
                                arraynewaccount = Split(strNewData, Chr(9))
                                arrayoldaccount = Split(strOldData, Chr(9))

                                strSQL.AppendLine(" Update EmpAccount ")
                                strSQL.AppendLine(" set BankID = '" & arraynewaccount(0) & "' ")
                                strSQL.AppendLine(" ,  Account = '" & arraynewaccount(1) & "' ")
                                strSQL.AppendLine(" ,  LastChgComp = '" & UserProfile.CompID & "' ")
                                strSQL.AppendLine(" ,  LastChgID = '" & UserProfile.UserID & "' ")
                                strSQL.AppendLine(" ,  LastChgDate = '" & Format(Now, "yyyy/MM/dd HH:mm:ss") & "' ")
                                strSQL.AppendLine(" where EmpID = " & Bsp.Utility.Quote(strEmpID) & " ")
                                strSQL.AppendLine(" and  CompID = " & Bsp.Utility.Quote(strCompID) & " ")
                                strSQL.AppendLine(" and  BankID = '" & arrayoldaccount(0) & "' ")
                                strSQL.AppendLine(" and  Account = '" & arrayoldaccount(1) & "' ")
                                Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")

                            Case "51"   '變更入帳比例
                                Dim arraynewAccountRatio() As String
                                Dim arrayoldAccountRatio() As String
                                arraynewAccountRatio = Split(strNewData, Chr(9))
                                arrayoldAccountRatio = Split(strOldData, Chr(9))

                                strSQL.AppendLine(" Update EmpAccount set ")
                                strSQL.AppendLine(" AccountRatio = '" & arraynewAccountRatio(1) & "' ")
                                strSQL.AppendLine(" ,  LastChgComp = '" & UserProfile.CompID & "' ")
                                strSQL.AppendLine(" ,  LastChgID = '" & UserProfile.UserID & "' ")
                                strSQL.AppendLine(" ,  LastChgDate = '" & Format(Now, "yyyy/MM/dd HH:mm:ss") & "' ")
                                strSQL.AppendLine(" where EmpID = " & Bsp.Utility.Quote(strEmpID) & " ")
                                strSQL.AppendLine(" and  CompID = " & Bsp.Utility.Quote(strCompID) & " ")
                                strSQL.AppendLine(" and  BankID = '" & arrayoldAccountRatio(0) & "' ")
                                strSQL.AppendLine(" and  AccountRatio = '" & arrayoldAccountRatio(1) & "' ")
                                Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")
                        End Select
                    End If
                    'Return Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")
                End Using
                tran.Commit()
            Catch ex As Exception
                If inTrans Then tran.Rollback()
                Return False
            End Try
        End Using
    End Function
    Public Function GetEmpName(ByVal strCompID As String, ByVal strEmpID As String) As DataTable
        Dim strSQL As New StringBuilder

        strSQL.AppendLine(" Select NameN as Name, BirthDate from Personal where EmpID = " & Bsp.Utility.Quote(strEmpID) & " and CompID = " & Bsp.Utility.Quote(strCompID) & "")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)

    End Function

    Public Shared Function GetEduID(ByVal strEmpID As String, ByVal strCompID As String) As String
        Dim strSQL As String

        strSQL = "Select EduID from Personal where EmpID = " & Bsp.Utility.Quote(strEmpID) & " and CompID = " & Bsp.Utility.Quote(strCompID)
        Return Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")

    End Function

    Public Function GetEducationSeq(ByVal strCompID As String, ByVal strEmpID As String, ByVal aryEducationData() As String) As Integer
        Dim strSQL As New StringBuilder
        Dim intSeq As Integer = 0

        strSQL.AppendLine(" Select isnull(Max(Seq),0) MaxSeq ")
        strSQL.AppendLine(" from Education E left join Personal P on E.IDNo = P.IDNo ")
        strSQL.AppendLine(" where P.EmpID  = " & Bsp.Utility.Quote(strEmpID) & " ")
        strSQL.AppendLine(" and   P.CompID = " & Bsp.Utility.Quote(strCompID) & " ")
        strSQL.AppendLine(" and   E.EduID  = '" & aryEducationData(0) & "' ")

        If IsDBNull(Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")) Then
            intSeq = 1
        Else
            intSeq = Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB") + 1
        End If

        Return intSeq

    End Function

    Public Function UpdateHR9_PersonalWait(ByVal bePersonalWait As bePersonalWait.Row) As Boolean
        Dim bsPersonalWait As New bePersonalWait.Service()
        Try
            bsPersonalWait.Update(bePersonalWait)
        Catch ex As Exception
            Throw
        End Try
        Return True
    End Function

    Public Function DeletePersonalWait(ByVal bePersonalWait As bePersonalWait.Row) As Boolean
        Dim bsPersonalWait As New bePersonalWait.Service()
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Delete From PersonalWait Where CompID = " & Bsp.Utility.Quote(bePersonalWait.CompID.Value) & "and EmpID = " & Bsp.Utility.Quote(bePersonalWait.EmpID.Value) & "and RelativeIDNo = " & Bsp.Utility.Quote(bePersonalWait.RelativeIDNo.Value) & "and ApplyDate = " & Bsp.Utility.Quote(Format(bePersonalWait.ApplyDate.Value, "yyyy/MM/dd HH:mm:ss")) & "and Reason = " & Bsp.Utility.Quote(bePersonalWait.Reason.Value) & "and OldData = " & Bsp.Utility.Quote(bePersonalWait.OldData.Value))

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As Data.Common.DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                'bsPersonalWait.DeleteRowByPrimaryKey(bePersonalWait, tran)
                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran, "eHRMSDB")

                tran.Commit()
                inTrans = False
            Catch ex As Exception
                If inTrans Then tran.Rollback()
                Throw
            Finally
                tran.Dispose()
                If cn.State = ConnectionState.Open Then cn.Close()
            End Try
        End Using

        Return True
    End Function

#End Region

End Class