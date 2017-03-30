'******************************************************************************
'功能說明：HR3000員工待異動紀錄維護相關Function
'建立人員：Weicheng
'建立日期：2014.08.21
'******************************************************************************
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.IO
Imports System

Public Class HR3000

#Region "EmployeeReason：異動原因"
    Public Shared Sub FillEmployeeReason(ByVal objDDL As DropDownList, Optional ByVal InValidFlag As Boolean = True)
        Dim objHR3000 As New HR3000
        Dim strWhere As String = ""
        Try
            If InValidFlag Then
                strWhere = "And EmployeeWaitFlag='1' And InValidFlag='0'"
            Else
                strWhere = "And EmployeeWaitFlag='1'"
            End If
            Using dt As DataTable = objHR3000.GetEmployeeReasonInfo("", " Rtrim(Reason) as Reason,Remark,Case When InValidFlag='1' Then Rtrim(Reason) + ' ' + Remark + '(無效)' Else Rtrim(Reason) + ' ' + Remark End as FullName", strWhere)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "FullName"
                    .DataValueField = "Reason"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetEmployeeReasonInfo(ByVal Reason As String, Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames = "" Then FieldNames = "*"

        strSQL.AppendLine("Select " & FieldNames)
        strSQL.AppendLine("From EmployeeReason")
        strSQL.AppendLine("Where 1 = 1")
        If Reason <> "" Then strSQL.AppendLine("And Reason = " & Bsp.Utility.Quote(Reason))
        If CondStr <> "" Then strSQL.AppendLine(CondStr)
        strSQL.AppendLine("Order by InValidFlag,Reason")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

    Public Function ChkEmployeeReasonIsVlaid(ByVal strEmployeeReason As String) As Boolean
        Dim bolResult As Boolean = False

        Dim strSQL As String
        Dim strReturn As String = ""

        Try
            strSQL = "Select InValidFlag from EmployeeReason Where Reason = " & Bsp.Utility.Quote(strEmployeeReason)
            strReturn = Bsp.DB.ExecuteScalar(strSQL, "eHRMSDB")
            If strReturn = "0" Then
                bolResult = True
            End If
        Catch ex As Exception
            Throw
        End Try
        Return bolResult
    End Function

#End Region
#Region "FillQuitReason：填入離職原因"
    Public Shared Sub FillEmployeeQuitReason(ByVal objDDL As DropDownList)
        Dim objHR3000 As New HR3000

        Try
            Using dt As DataTable = objHR3000.GetEmployeeQuitReasonInfo("", "Rtrim(Code) as Code,CodeCName, Rtrim(Code) + ' ' + CodeCName As FullName", "And TabName='EmployeeReason' and FldName='QuitReason' and NotShowFlag='0'")
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "FullName"
                    .DataValueField = "Code"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetEmployeeQuitReasonInfo(ByVal Code As String, Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames = "" Then FieldNames = "*"

        strSQL.AppendLine("Select " & FieldNames)
        strSQL.AppendLine("From HRCodeMap")
        strSQL.AppendLine("Where 1 = 1")
        If Code <> "" Then strSQL.AppendLine("And Code = " & Bsp.Utility.Quote(Code))
        If CondStr <> "" Then strSQL.AppendLine(CondStr)
        strSQL.AppendLine("Order by SortFld")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "FillEmpAdditionReason：填入兼任狀態"
    Public Shared Sub FillEmpAdditionReason(ByVal objDDL As DropDownList)
        Dim objHR3000 As New HR3000

        Try
            Using dt As DataTable = objHR3000.GetEmpAdditionReasonInfo("", "Code , CodeCName,Code + ' ' + CodeCName As FullName", "And TabName = 'EmpAddition' And FldName = 'Reason' And NotShowFlag = '0'")
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "FullName"
                    .DataValueField = "Code"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetEmpAdditionReasonInfo(ByVal Code As String, Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames = "" Then FieldNames = "*"

        strSQL.AppendLine("Select " & FieldNames)
        strSQL.AppendLine("From HRCodeMap")
        strSQL.AppendLine("Where 1 = 1")
        If Code <> "" Then strSQL.AppendLine("And Code = " & Bsp.Utility.Quote(Code))
        If CondStr <> "" Then strSQL.AppendLine(CondStr)
        strSQL.AppendLine("Order by SortFld")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "WTID：填入班別-證券體系"
    Public Shared Sub FillEmployeeWTID(ByVal objDDL As DropDownList, ByVal strCompID As String)
        Dim objHR3000 As New HR3000

        Try
            Using dt As DataTable = objHR3000.GetEmployeeWTIDInfo(strCompID, "", "RTrim(WTID) as WTID, BeginTime+'-'+EndTime as Remark, RTrim(WTID) + ' ' + BeginTime+'-'+EndTime as FullRemark", "")
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "FullRemark"
                    .DataValueField = "WTID"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetEmployeeWTIDInfo(ByVal strCompID As String, ByVal WTID As String, Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames = "" Then FieldNames = "*"

        strSQL.AppendLine("Select " & FieldNames)
        strSQL.AppendLine("From WorkTime")
        strSQL.AppendLine("Where 1 = 1")
        If strCompID <> "" Then strSQL.AppendLine("And CompID = " & Bsp.Utility.Quote(strCompID))
        If WTID <> "" Then strSQL.AppendLine("And WTID = " & Bsp.Utility.Quote(WTID))
        If CondStr <> "" Then strSQL.AppendLine(CondStr)
        strSQL.AppendLine("Order by WTID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region
#Region "EmployeeWait"
    Public Function QueryEmployeeWait(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()
        Dim strFieldNames As New StringBuilder()
        Dim aryStr() As String

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine("And E.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "ValidOrNot"
                        aryStr = ht(strKey).ToString().Split(";")
                        If aryStr(0).ToString.Trim() = "True" Then
                            strSQL.AppendLine("And E.ValidMark = " & Bsp.Utility.Quote(aryStr(1).ToString.Trim()))
                        End If
                    Case "EmpID"
                        aryStr = ht(strKey).ToString().Split(";")
                        If aryStr(0).ToString.Trim() = "True" Then
                            strSQL.AppendLine("And E.EmpID = " & Bsp.Utility.Quote(aryStr(1).ToString.Trim()))
                        End If
                    Case "Name"
                        aryStr = ht(strKey).ToString().Split(";")
                        If aryStr(0).ToString.Trim() = "True" Then
                            strSQL.AppendLine("And P.NameN Like N'%" & aryStr(1).ToString.Trim() & "%'")
                        End If
                    Case "ValidDate"
                        aryStr = ht(strKey).ToString().Split(";")
                        If aryStr(0).ToString.Trim() = "True" Then
                            If aryStr(2).ToString.Trim() <> "" Then
                                strSQL.AppendLine("And convert(char(10),E.ValidDate,111) >= " & Bsp.Utility.Quote(aryStr(1).ToString.Trim()))
                                strSQL.AppendLine("And convert(char(10),E.ValidDate,111) <= " & Bsp.Utility.Quote(aryStr(2).ToString.Trim()))
                            Else
                                strSQL.AppendLine("And convert(char(10),E.ValidDate,111) = " & Bsp.Utility.Quote(aryStr(1).ToString.Trim()))
                            End If
                        End If

                    Case "Reason"
                        aryStr = ht(strKey).ToString().Split(";")
                        If aryStr(0).ToString.Trim() = "True" Then
                            strSQL.AppendLine("And E.Reason = " & Bsp.Utility.Quote(aryStr(1).ToString.Trim()))
                        End If
                End Select
            End If
        Next

        strFieldNames.AppendLine("Convert(bit,Case When E.ValidMark <>'1' Then '0' Else E.ValidMark End) as ValidMark,E.Seq,")
        strFieldNames.AppendLine("isnull(C.CompName,'') as CompName,E.CompID,")
        strFieldNames.AppendLine("E.EmpID,isnull(P.NameN,'') as NameN,Convert(char(10),E.ValidDate,111) as ValidDate,E.Reason,isnull(ER.Remark,'') as ReasonName,")
        strFieldNames.AppendLine("isnull(C1.CompName,'') as NewCompName,E.NewCompID,")
        'strFieldNames.AppendLine("Case when E.NewCompID='SPHBKC' Then isnull(B1.OrganName,'') Else isnull(B.OrganName,'') End COLLATE Chinese_Taiwan_Stroke_CS_AS as NewGroupName,E.GroupID,")   '20150325 wei modify
        'strFieldNames.AppendLine("Case when E.NewCompID='SPHBKC' Then isnull(O3.OrganName,'') Else isnull(O1.OrganName,'') End COLLATE Chinese_Taiwan_Stroke_CS_AS as NewDeptName,E.DeptID,") '20150325 wei modify
        'strFieldNames.AppendLine("Case when E.NewCompID='SPHBKC' Then isnull(O4.OrganName,'') Else isnull(O2.OrganName,'') End COLLATE Chinese_Taiwan_Stroke_CS_AS as NewOrganName,E.OrganID,")   '20150325 wei modify
        strFieldNames.AppendLine("isnull(B.OrganName,'') COLLATE Chinese_Taiwan_Stroke_CS_AS as NewGroupName,E.GroupID,")   '2016/05/03 SPHBKC資料已併入OrganizationFlow中
        strFieldNames.AppendLine("isnull(O1.OrganName,'') COLLATE Chinese_Taiwan_Stroke_CS_AS as NewDeptName,E.DeptID,")    '2016/05/03 SPHBKC資料已併入Organization中
        strFieldNames.AppendLine("isnull(O2.OrganName,'') COLLATE Chinese_Taiwan_Stroke_CS_AS as NewOrganName,E.OrganID,")  '2016/05/03 SPHBKC資料已併入Organization中
        strFieldNames.AppendLine("E.LastChgComp,isnull(C2.CompName,'') as LastChgCompName,E.LastChgID,isnull(P1.NameN,'') as LastChgName,convert(char(19),E.LastChgDate,121) as LastChgDate")

        Return GetEmployeeWaitInfo(strFieldNames.ToString(), strSQL.ToString())
    End Function
    Public Function GetEmployeeWaitInfo(Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames = "" Then FieldNames = "*"

        strSQL.AppendLine("Select " & FieldNames)
        strSQL.AppendLine("From EmployeeWait E")
        strSQL.AppendLine("Left join Personal P on E.CompID = P.CompID and E.EmpID = P.EmpID")
        strSQL.AppendLine("Left join Personal P1 on E.LastChgComp = P1.CompID and E.LastChgID = P1.EmpID")
        strSQL.AppendLine("Left join Company C on E.CompID = C.CompID")
        strSQL.AppendLine("Left join Company C1 on E.NewCompID = C1.CompID")
        strSQL.AppendLine("Left join Company C2 on E.LastChgComp = C2.CompID")
        strSQL.AppendLine("Left join Organization O1 on E.NewCompID = O1.CompID and E.DeptID = O1.OrganID")
        strSQL.AppendLine("Left join Organization O2 on E.NewCompID = O2.CompID and E.OrganID = O2.OrganID")
        'strSQL.AppendLine("Left join COrganization O3 on E.NewCompID = O3.CompID COLLATE Chinese_Taiwan_Stroke_CS_AS and E.DeptID = O3.OrganID COLLATE Chinese_Taiwan_Stroke_CS_AS ") '20150325 wei add '2016/05/03 SPHBKC資料已併入Organization中
        'strSQL.AppendLine("Left join COrganization O4 on E.NewCompID = O4.CompID COLLATE Chinese_Taiwan_Stroke_CS_AS and E.OrganID = O4.OrganID COLLATE Chinese_Taiwan_Stroke_CS_AS") '20150325 wei add '2016/05/03 SPHBKC資料已併入Organization中
        strSQL.AppendLine("Left join OrganizationFlow B on E.GroupID = B.OrganID and B.OrganID = B.GroupID")
        'strSQL.AppendLine("Left join COrganizationFlow B1 on E.GroupID = B1.OrganID COLLATE Chinese_Taiwan_Stroke_CS_AS and B1.OrganID = B1.GroupID") '20150325 wei add '2016/05/03 SPHBKC資料已併入OrganizationFlow中
        strSQL.AppendLine("Left join EmployeeReason ER on E.Reason = ER.Reason and ER.EmployeeWaitFlag='1'")
        strSQL.AppendLine("Where 1 = 1")
        If CondStr <> "" Then strSQL.AppendLine(CondStr)
        strSQL.AppendLine("Order by E.ValidDate , E.EmpID, E.Seq")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
    Public Function QueryEmployeeWaitByDownload(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()
        Dim strFieldNames As New StringBuilder()
        Dim aryStr() As String

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine("And E.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "ValidOrNot"
                        aryStr = ht(strKey).ToString().Split(";")
                        If aryStr(0).ToString.Trim() = "True" Then
                            strSQL.AppendLine("And E.ValidMark = " & Bsp.Utility.Quote(aryStr(1).ToString.Trim()))
                        End If
                    Case "EmpID"
                        aryStr = ht(strKey).ToString().Split(";")
                        If aryStr(0).ToString.Trim() = "True" Then
                            strSQL.AppendLine("And E.EmpID = " & Bsp.Utility.Quote(aryStr(1).ToString.Trim()))
                        End If
                    Case "Name"
                        aryStr = ht(strKey).ToString().Split(";")
                        If aryStr(0).ToString.Trim() = "True" Then
                            strSQL.AppendLine("And P.NameN = N" & Bsp.Utility.Quote(aryStr(1).ToString.Trim()))
                        End If
                    Case "ValidDate"
                        aryStr = ht(strKey).ToString().Split(";")
                        If aryStr(0).ToString.Trim() = "True" Then
                            If aryStr(2).ToString.Trim() <> "" Then
                                strSQL.AppendLine("And convert(char(10),E.ValidDate,111) >= " & Bsp.Utility.Quote(aryStr(1).ToString.Trim()))
                                strSQL.AppendLine("And convert(char(10),E.ValidDate,111) <= " & Bsp.Utility.Quote(aryStr(2).ToString.Trim()))
                            Else
                                strSQL.AppendLine("And convert(char(10),E.ValidDate,111) = " & Bsp.Utility.Quote(aryStr(1).ToString.Trim()))
                            End If
                        End If

                    Case "Reason"
                        aryStr = ht(strKey).ToString().Split(";")
                        If aryStr(0).ToString.Trim() = "True" Then
                            strSQL.AppendLine("And E.Reason = " & Bsp.Utility.Quote(aryStr(1).ToString.Trim()))
                        End If
                End Select
            End If
        Next

        strFieldNames.AppendLine("E.ValidMark as '生效',")
        strFieldNames.AppendLine("isnull(C.CompName,'') as '公司名稱',")
        strFieldNames.AppendLine("E.EmpID as '員工編號',isnull(P.NameN,'') as '姓名',Convert(char(10),E.ValidDate,111) as '生效日期',isnull(ER.Remark,'') as '異動原因',")
        strFieldNames.AppendLine("isnull(C1.CompName,'') as '異動後公司名稱',E.NewCompID,")
        strFieldNames.AppendLine("isnull(B.OrganName,'') as '事業群',")
        strFieldNames.AppendLine("isnull(O1.OrganName,'') as '部門名稱',")
        strFieldNames.AppendLine("isnull(O2.OrganName,'') as '科/組/課',")
        strFieldNames.AppendLine("isnull(WS.Remark,'') as '工作地點',")
        strFieldNames.AppendLine("isNull(GF.OrganName,'') as '簽核最小單位',")
        strFieldNames.AppendLine("E.RankID as '職等',E.TitleName as '職稱',")
        strFieldNames.AppendLine("E.BossType as '主管類別',E.IsBoss as '主管',E.IsSecBoss as '副主管',E.IsGroupBoss as '簽核主管',E.IsSecGroupBoss as '簽核副主管',")
        strFieldNames.AppendLine("E.PositionID as '主要職位',")
        strFieldNames.AppendLine("E.PositionID as '兼任職位',")
        strFieldNames.AppendLine("E.WorkTypeID as '主要工作性質',")
        strFieldNames.AppendLine("E.WorkTypeID as '兼任工作性質',")
        strFieldNames.AppendLine("Convert(char(10),E.DueDate,111) '留停迄日',")
        strFieldNames.AppendLine("E.Remark as '備註',")
        strFieldNames.AppendLine("E.QuitReason + ' ' + isnull(HR1.CodeCName,'') as '離職原因',")
        strFieldNames.AppendLine("Convert(char(10),isnull(P.EmpDate,''),111) '到職日',")
        strFieldNames.AppendLine("Case when P.Sex = '1' then '男' when P.Sex = '2' then '女' else '' end as '性別',")
        strFieldNames.AppendLine("E.SalaryPaid as '計薪註記',")
        strFieldNames.AppendLine("RTrim(E.WTID) + ' ' + isnull(WT.BeginTime,'') + '-' + isnull(WT.EndTime,'') as '班別',")
        strFieldNames.AppendLine("E.LastChgComp as '最後異動公司',")
        strFieldNames.AppendLine("E.LastChgID as '最後異動人員',")
        strFieldNames.AppendLine("convert(char(19),E.LastChgDate,20) as '最後異動時間'")

        Return GetEmployeeWaitInfoByDownload(strFieldNames.ToString(), strSQL.ToString())
    End Function
    Public Function GetEmployeeWaitInfoByDownload(Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
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
        strSQL.AppendLine("Left join WorkSite WS on E.NewCompID = WS.CompID and E.WorkSiteID = WS.WorkSiteID")
        strSQL.AppendLine("Left join OrganizationFlow GF　on E.FlowOrganID = GF.OrganID")
        strSQL.AppendLine("Left join HRCodeMap HR1 on HR1.TabName='EmployeeReason' and HR1.FldName='QuitReason' and HR1.Code=E.QuitReason")
        strSQL.AppendLine("Left join WorkTime WT on E.CompID=WT.CompID and E.WTID = WT.WTID")   '20150909 wei modify 班別增加公司代碼
        strSQL.AppendLine("Where 1 = 1")
        If CondStr <> "" Then strSQL.AppendLine(CondStr)
        strSQL.AppendLine("Order by E.ValidDate , E.EmpID, E.Seq")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

    Public Function GetEmployeeWaitSeq(ByVal strCompID As String, ByVal strEmpID As String, ByVal strValidDate As String) As Integer
        Dim strSQL As New StringBuilder
        Dim intSeq As Integer = 0

        strSQL.AppendLine("Select Max(Seq) as MaxSeq from EmployeeWait")
        strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(strCompID))
        strSQL.AppendLine("and EmpID =" & Bsp.Utility.Quote(strEmpID))
        strSQL.AppendLine("And convert(char(10),ValidDate,111) = " & Bsp.Utility.Quote(strValidDate))

        If IsDBNull(Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")) Then
            intSeq = 1
        Else
            intSeq = Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB") + 1
        End If

        Return intSeq

    End Function

    Public Function AddEmployeeWati(ByVal beEmployeeWait As beEmployeeWait.Row, ByVal TmpSeq As Integer) As Boolean
        Dim bsEmployeeWait As New beEmployeeWait.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                'If bsEmployeeWait.Insert(beEmployeeWait) = 0 Then Return False
                bsEmployeeWait.Insert(beEmployeeWait, tran)
                Dim strSQL As New StringBuilder()

                'EmpAdditionWait
                strSQL.AppendLine("Insert Into EmpAdditionWait (CompID,EmpID,ValidDate,Seq,AddSeq,AddCompID,AddDeptID,AddOrganID,AddFlowOrganID,Reason," & _
                                                                    "FileNo,Remark,IsBoss,IsSecBoss,IsGroupBoss,IsSecGroupBoss,BossType,ValidMark,ExistsEmpAddition," & _
                                                                    "CreateDate,CreateComp,CreateID,LastChgDate,LastChgComp,LastChgID)")
                strSQL.AppendLine("Select CompID,EmpID," & Bsp.Utility.Quote(beEmployeeWait.ValidDate.Value.ToShortDateString) & "," & beEmployeeWait.Seq.Value & ",AddSeq")
                strSQL.AppendLine(",AddCompID,AddDeptID,AddOrganID,AddFlowOrganID,Reason")
                strSQL.AppendLine(",FileNo,Remark,IsBoss,IsSecBoss,IsGroupBoss,IsSecGroupBoss,BossType")
                strSQL.AppendLine(",ValidMark,ExistsEmpAddition")
                strSQL.AppendLine(",getdate()")
                strSQL.AppendLine("," & Bsp.Utility.Quote(UserProfile.ActCompID))
                strSQL.AppendLine("," & Bsp.Utility.Quote(UserProfile.ActUserID))
                strSQL.AppendLine(",getdate()")
                strSQL.AppendLine("," & Bsp.Utility.Quote(UserProfile.ActCompID))
                strSQL.AppendLine("," & Bsp.Utility.Quote(UserProfile.ActUserID))
                strSQL.AppendLine("From Tmp_EmpAdditionWait")
                strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(beEmployeeWait.CompID.Value))
                strSQL.AppendLine("And EmpID = " & Bsp.Utility.Quote(beEmployeeWait.EmpID.Value))
                strSQL.AppendLine("And Seq = " & TmpSeq & ";")
                strSQL.AppendLine("Delete From Tmp_EmpAdditionWait")
                strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(beEmployeeWait.CompID.Value))
                strSQL.AppendLine("And EmpID = " & Bsp.Utility.Quote(beEmployeeWait.EmpID.Value))
                strSQL.AppendLine("And Seq = " & TmpSeq & ";")

                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran, "eHRMSDB")

                tran.Commit()
            Catch ex As Exception
                tran.Rollback()
                Throw
            Finally
                If tran IsNot Nothing Then tran.Dispose()
            End Try
        End Using

        Return True

    End Function
    Public Function UpdateEmployeeWait(ByVal beEmployeeWaitDel As beEmployeeWait.Row, ByVal beEmployeeWait As beEmployeeWait.Row, ByVal TmpSeq As Integer) As Boolean
        Dim bsEmployeeWait As New beEmployeeWait.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                'If bsEmployeeWait.Insert(beEmployeeWait) = 0 Then Return False
                bsEmployeeWait.DeleteRowByPrimaryKey(beEmployeeWaitDel, tran)
                bsEmployeeWait.Insert(beEmployeeWait, tran)
                'bsEmployeeWait.Update(beEmployeeWait, tran)
                Dim strSQL As New StringBuilder()

                'EmpAdditionWait
                strSQL.AppendLine("Delete From EmpAdditionWait")
                strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(beEmployeeWait.CompID.Value))
                strSQL.AppendLine("And EmpID = " & Bsp.Utility.Quote(beEmployeeWait.EmpID.Value))
                strSQL.AppendLine("And Seq = " & TmpSeq & ";")
                strSQL.AppendLine("Insert Into EmpAdditionWait (CompID,EmpID,ValidDate,Seq,AddSeq,AddCompID,AddDeptID,AddOrganID,AddFlowOrganID,Reason," & _
                                                                    "FileNo,Remark,IsBoss,IsSecBoss,IsGroupBoss,IsSecGroupBoss,BossType,ValidMark,ExistsEmpAddition," & _
                                                                    "CreateDate,CreateComp,CreateID,LastChgDate,LastChgComp,LastChgID)")
                strSQL.AppendLine("Select CompID,EmpID," & Bsp.Utility.Quote(beEmployeeWait.ValidDate.Value.ToShortDateString) & "," & beEmployeeWait.Seq.Value & ",AddSeq")
                strSQL.AppendLine(",AddCompID,AddDeptID,AddOrganID,AddFlowOrganID,Reason")
                strSQL.AppendLine(",FileNo,Remark,IsBoss,IsSecBoss,IsGroupBoss,IsSecGroupBoss,BossType")
                strSQL.AppendLine(",ValidMark,ExistsEmpAddition")
                strSQL.AppendLine(",getdate()")
                strSQL.AppendLine("," & Bsp.Utility.Quote(UserProfile.ActCompID))
                strSQL.AppendLine("," & Bsp.Utility.Quote(UserProfile.ActUserID))
                strSQL.AppendLine(",getdate()")
                strSQL.AppendLine("," & Bsp.Utility.Quote(UserProfile.ActCompID))
                strSQL.AppendLine("," & Bsp.Utility.Quote(UserProfile.ActUserID))
                strSQL.AppendLine("From Tmp_EmpAdditionWait")
                strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(beEmployeeWait.CompID.Value))
                strSQL.AppendLine("And EmpID = " & Bsp.Utility.Quote(beEmployeeWait.EmpID.Value))
                strSQL.AppendLine("And Seq = " & TmpSeq & ";")
                strSQL.AppendLine("Delete From Tmp_EmpAdditionWait")
                strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(beEmployeeWait.CompID.Value))
                strSQL.AppendLine("And EmpID = " & Bsp.Utility.Quote(beEmployeeWait.EmpID.Value))
                strSQL.AppendLine("And Seq = " & TmpSeq & ";")

                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran, "eHRMSDB")

                tran.Commit()
            Catch ex As Exception
                tran.Rollback()
                Throw
            Finally
                If tran IsNot Nothing Then tran.Dispose()
            End Try
        End Using

        Return True
    End Function
    Public Function DeleteEmployeeWait(ByVal beEmployeeWait As beEmployeeWait.Row) As Boolean
        Dim bsEmployeeWait As New beEmployeeWait.Service()
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Delete From EmployeeWait Where CompID = " & Bsp.Utility.Quote(beEmployeeWait.CompID.Value))
        strSQL.AppendLine("And EmpID = " & Bsp.Utility.Quote(beEmployeeWait.EmpID.Value))
        strSQL.AppendLine("And ValidDate = " & Bsp.Utility.Quote(beEmployeeWait.ValidDate.Value))
        strSQL.AppendLine("And Seq = " & beEmployeeWait.Seq.Value)

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                bsEmployeeWait.DeleteRowByPrimaryKey(beEmployeeWait, tran)
                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran, "eHRMSDB")

                tran.Commit()
                Return True
            Catch ex As Exception
                If inTrans Then tran.Rollback()
                Throw
            Finally
                tran.Dispose()
                If cn.State = ConnectionState.Open Then cn.Close()
            End Try
        End Using
    End Function

    '20150612 wea add
    Public Function GetEmployeeWaitData(ByVal strCompID As String, ByVal strEmpID As String, ByVal strValidDate As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select Seq ")
        strSQL.AppendLine("From EmployeeWait")
        strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(strCompID))
        strSQL.AppendLine("And EmpID = " & Bsp.Utility.Quote(strEmpID))
        strSQL.AppendLine("And ValidDate = " & Bsp.Utility.Quote(strValidDate))
        strSQL.AppendLine("And ValidMark = '0'")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)

    End Function

#End Region
#Region "最小簽核單位"
    Public Function GetFlowOrganID(ByVal CompID As String, ByVal strWhere As String) As DataSet
        Dim strSQL As String

        '2016/05/03 SPHBKC資料已併入OrganizationFlow中
        'If CompID = "SPHBKC" Then
        '    strSQL = "Select Rtrim(OrganID) as OrganID,OrganName,ISNULL(FlowOrganID, '') AS FlowOrganID,InValidFlag, OrganID + ' ' + OrganName + case when InValidFlag='1' then '(無效)' else '' end as FullOrganName from COrganizationFlow "
        'Else
        strSQL = "Select Rtrim(OrganID) as OrganID,OrganName,ISNULL(FlowOrganID, '') AS FlowOrganID,InValidFlag, OrganID + ' ' + OrganName + case when InValidFlag='1' then '(無效)' else '' end as FullOrganName from OrganizationFlow "
        'End If

        If strWhere.Trim() <> "" Then
            strSQL &= " " & strWhere
        End If
        strSQL &= " Order by InValidFlag"
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL, "eHRMSDB")
    End Function
#End Region

    Public Function GetBusinessType(ByVal CompID As String, ByVal OrganID As String) As String
        Dim strSQL As String = "Select BusinessType From OrganizationFlow Where CompID = " & Bsp.Utility.Quote(CompID) & " And OrganID = " & Bsp.Utility.Quote(OrganID)

        If Bsp.DB.ExecuteScalar(strSQL, "eHRMSDB") = Nothing Then
            Return ""
        Else
            Return Bsp.DB.ExecuteScalar(strSQL, "eHRMSDB").ToString
        End If
    End Function

#Region "取個人資料"
    Public Function GetEmpDataByHR3000(ByVal CompID As String, ByVal EmpID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select Top 1 P.IDNo,P.NameN as Name,P.CompID,P.DeptID,P.OrganID,P.RankID,P.TitleID,P.GroupID,P.WorkSiteID,Convert(char(10),P.EmpDate,111) as EmpDate,isnull(PO.WTID,'') as WTID,isnull(F.OrganID,'') as FlowOrganID ")
        strSQL.AppendLine(",isnull(O.BossType,'') as BossType, isnull(O1.BossType,'') as SecBossType ")  '20150306 wei add
        strSQL.AppendLine(",P.WorkStatus ")  '20150709 wei add
        strSQL.AppendLine(",isnull(F.BusinessType,'') BusinessType, isnull(F.EmpFlowRemarkID,'') EmpFlowRemarkID") '20161104 Beatrice Add
        strSQL.AppendLine("From Personal P")
        strSQL.AppendLine("Left Join PersonalOther PO On P.CompID = PO.CompID And P.EmpID = PO.EmpID ")
        strSQL.AppendLine("Left Join EmpFlow F On P.CompID = F.CompID And P.EmpID = F.EmpID ")
        strSQL.AppendLine("Left Join Organization O On P.CompID = O.BossCompID And P.EmpID = O.Boss and P.CompID=O.CompID and P.OrganID=O.OrganID and O.InValidFlag='0' ")  '20150306 wei add
        strSQL.AppendLine("Left Join Organization O1 On P.CompID = O1.SecBossCompID And P.EmpID = O1.SecBoss and P.CompID=O1.CompID and P.OrganID=O1.OrganID and O1.InValidFlag='0' ")  '20150306 wei add
        strSQL.AppendLine("Where P.CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine("And P.EmpID = " & Bsp.Utility.Quote(EmpID))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)

    End Function

    Public Function GetEmpWorkTypeByHR3000(ByVal CompID As String, ByVal EmpID As String) As DataTable
        Dim strSQL As StringBuilder = New StringBuilder()

        strSQL.AppendLine("Select * From EmpWorkType ")
        strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine("And EmpID = " & Bsp.Utility.Quote(EmpID))
        strSQL.AppendLine("order by PrincipalFlag desc , WorkTypeID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

    Public Function GetEmpPositionByHR3000(ByVal CompID As String, ByVal EmpID As String) As DataTable
        Dim strSQL As StringBuilder = New StringBuilder()

        strSQL.AppendLine("Select * From EmpPosition ")
        strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine("And EmpID = " & Bsp.Utility.Quote(EmpID))
        strSQL.AppendLine("order by PrincipalFlag desc , PositionID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "WorkType"
    Public Function GetWorkTypeID(ByVal strWhere As String) As DataSet
        Dim strSQL As String
        strSQL = "Select Rtrim(WorkTypeID) as WorkTypeID,Remark, WorkTypeID + ' ' + Remark as FullWorkTypeName From WorkType "
        If strWhere.Trim() <> "" Then
            strSQL &= " " & strWhere
        End If
        strSQL &= " order by WorkTypeID"
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL, "eHRMSDB")
    End Function
    Public Function GetCWorkTypeID(ByVal strWhere As String) As DataSet
        Dim strSQL As String
        strSQL = "Select Rtrim(Code) as WorkTypeID,CodeName as Remark, Code + ' ' + CodeName as FullWorkTypeName From CHRCodeMap "
        If strWhere.Trim() <> "" Then
            strSQL &= " " & strWhere
        End If
        strSQL &= " order by Code"
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL, "eHRMSDB")
    End Function
    Public Function funGetWorkType(ByVal strCompID As String, ByVal strWorkTypeID As String) As String
        Dim strSQL As String

        strSQL = "Select Case when InValidFlag='0' then RTrim(Remark) else RTrim(Remark)+'(無效)' end as Remark from WorkType where CompID=" & Bsp.Utility.Quote(strCompID) & " and WorkTypeID = " & Bsp.Utility.Quote(strWorkTypeID)
        Return Bsp.DB.ExecuteScalar(strSQL, "eHRMSDB")

    End Function
    Public Function GetWorkTypeIDByOrgan(ByVal CompID As String, ByVal OrganID As String) As DataTable
        Dim strSQL As StringBuilder = New StringBuilder()

        strSQL.AppendLine("Select WorkTypeID From Organization ")
        strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine("And OrganID = " & Bsp.Utility.Quote(OrganID))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

    Public Function GetEmpWorkTypeByEmpOrgan(ByVal CompID As String, ByVal EmpID As String, ByVal OrganID As String) As DataTable
        Dim strSQL As StringBuilder = New StringBuilder()

        strSQL.AppendLine("Select * From EmpWorkType ")
        strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine("And EmpID = " & Bsp.Utility.Quote(EmpID))
        strSQL.AppendLine("And WorkTypeID In (Select WorkTypeID From OrgWorkType Where CompID = " & Bsp.Utility.Quote(CompID) & " And OrganID = " & Bsp.Utility.Quote(OrganID) & ") ")
        strSQL.AppendLine("order by PrincipalFlag desc , WorkTypeID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region
#Region "Position"
    Public Function GetPositionID(ByVal strWhere As String) As DataSet
        Dim strSQL As String

        strSQL = "Select Rtrim(PositionID) as PositionID,Remark, PositionID + ' ' + Remark as FullPositionName From Position "
        If strWhere.Trim() <> "" Then
            strSQL &= " " & strWhere
        End If
        strSQL &= " order by PositionID"
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL, "eHRMSDB")
    End Function
    Public Function funGetPosition(ByVal strCompID As String, ByVal strPositionID As String) As String
        Dim strSQL As String

        strSQL = "Select Case when InValidFlag='0' then RTrim(Remark) else RTrim(Remark)+'(無效)' end as Remark from Position where CompID=" & Bsp.Utility.Quote(strCompID) & " and PositionID = " & Bsp.Utility.Quote(strPositionID)
        Return Bsp.DB.ExecuteScalar(strSQL, "eHRMSDB")

    End Function
    Public Function GetPositionIDByOrgan(ByVal CompID As String, ByVal OrganID As String) As DataTable
        Dim strSQL As StringBuilder = New StringBuilder()

        strSQL.AppendLine("Select PositionID From Organization ")
        strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine("And OrganID = " & Bsp.Utility.Quote(OrganID))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

    Public Function GetEmpPositionByEmpOrgan(ByVal CompID As String, ByVal EmpID As String, ByVal OrganID As String) As DataTable
        Dim strSQL As StringBuilder = New StringBuilder()

        strSQL.AppendLine("Select * From EmpPosition ")
        strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine("And EmpID = " & Bsp.Utility.Quote(EmpID))
        strSQL.AppendLine("And PositionID In (Select PositionID From OrgPosition Where CompID = " & Bsp.Utility.Quote(CompID) & " And OrganID = " & Bsp.Utility.Quote(OrganID) & ") ")
        strSQL.AppendLine("order by PrincipalFlag desc , PositionID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region
#Region "跨公司異動檢核"
    '生效後為【在職】的異動原因(02,03,05,06,07,50)的異動原因，都先檢查【人員主檔】在【異動後公司以外】的公司資料是否在職
    '若在職，再檢查檢查【待異動檔】在【異動後公司以外】的公司資料是否有異動後【非在職】(12,14,18,11,15,16,17,13,50,70)的待異動
    '若有，才能建檔，否則不能建檔(HR2300到職建檔亦同)
    Public Function funCheckWorkStatus(ByVal strValidDate As String, ByVal strOldCompID As String, ByVal strNewCompID As String, ByVal strEmpID As String, ByVal strIDNo As String, ByVal strReason As String) As String
        Dim objHR As New HR
        Dim strWhere As New StringBuilder

        Dim strMsgComp As String = ""
        Dim strMsgValidDate As String = ""

        '檢查是否有待異動在職紀錄
        strWhere.Length = 0
        strWhere.AppendLine(" And P.CompID <> " & Bsp.Utility.Quote(strOldCompID) & " And P.IDNo = " & Bsp.Utility.Quote(strIDNo))
        'strWhere.AppendLine(" And (E.Reason in ('02','03','05','06','07')")    '20150729 wei del
        'strWhere.AppendLine(" or (E.Reason ='50' and E.CompID=E.NewCompID) )") '20150729 wei del
        strWhere.AppendLine(" And (E.Reason in (select Reason From WorkStatus_EmployeeReason where AfterWorkStatusType='1') )")  '20150729 wei modify 改為依任職狀況vs異動原因表來判斷異動後是否在職
        strWhere.AppendLine(" And E.ValidMark='0'")
        If objHR.IsDataExists("EmployeeWait E Inner Join Personal P On E.CompID=P.CompID and E.EmpID=P.EmpID", strWhere.ToString) Then
            Using dt1 As DataTable = GetCheckMsg("P.CompID,C.CompName,Convert(char(10),E.ValidDate,111) as ValidDate", _
                                                 "EmployeeWait E Inner Join Personal P On E.CompID=P.CompID and E.EmpID=P.EmpID Inner Join Company C On P.CompID=C.CompID", _
                                                 strWhere.ToString)
                strMsgComp = dt1.Rows(0).Item("CompName").ToString
                strMsgValidDate = dt1.Rows(0).Item("ValidDate").ToString
            End Using
            Return "資料重複：該員將於" & strMsgComp & strMsgValidDate & "復職，不得重覆兩公司在職"
        End If


        strWhere.Length = 0
        '檢查是否在其它公司尚在職-台灣
        strWhere.AppendLine(" And CompID <> " & Bsp.Utility.Quote(strNewCompID) & " And IDNo = " & Bsp.Utility.Quote(strIDNo))
        strWhere.AppendLine(" And WorkStatus = '1'")
        If objHR.IsDataExists("Personal", strWhere.ToString) Then '存在則檢查是否有待異動離職紀錄

            '檢查是否有待異動離職紀錄
            strWhere.Length = 0
            strWhere.AppendLine(" And P.CompID <> " & Bsp.Utility.Quote(strNewCompID) & " And P.IDNo = " & Bsp.Utility.Quote(strIDNo))
            'strWhere.AppendLine(" And E.Reason in ('12','14','18','11','15','16','17','13','50','70','19')")    '20150721 wei modify 19留停延長    20150729 wei del
            strWhere.AppendLine(" And (E.Reason in (select Reason From WorkStatus_EmployeeReason where AfterWorkStatusType<>'1' and Reason<>'') )")  '20150729 wei modify 改為依任職狀況vs異動原因表來判斷異動後是否在職
            strWhere.AppendLine(" And E.ValidMark='0' and E.ValidDate<=" & Bsp.Utility.Quote(strValidDate))
            If Not objHR.IsDataExists("EmployeeWait E Inner Join Personal P On E.CompID=P.CompID and E.EmpID=P.EmpID and P.WorkStatus='1'", strWhere.ToString) Then
                Using dt1 As DataTable = GetCheckMsg("P.CompID,C.CompName", _
                                                 "Personal P Inner Join Company C On P.CompID=C.CompID", _
                                                 "And P.CompID <> " & Bsp.Utility.Quote(strNewCompID) & " And P.IDNo = " & Bsp.Utility.Quote(strIDNo) & " And P.WorkStatus = '1'")
                    strMsgComp = dt1.Rows(0).Item("CompName").ToString
                End Using
                Return "資料重複：該員於" & strMsgComp & strValidDate & "尚未離職"
            End If
        End If


        ''檢查是否在其它公司尚在職-南京子行
        'strWhere.AppendLine(" And CompID <> " & Bsp.Utility.Quote(strNewCompID) & " And IDNo = " & Bsp.Utility.Quote(strIDNo))
        'strWhere.AppendLine(" And WorkStatus = '1'")
        'If objHR.IsDataExists("CPersonal", strWhere.ToString) Then '存在則檢查是否有待異動離職紀錄
        'End If


        Return ""
    End Function
#End Region
    
#Region "取得員工僱用類別"
    Public Function GetEmpType(ByVal strCompID As String, ByVal strEmpID As String) As String
        Dim strSQL As String
        Dim strReturn As String = ""
        strSQL = "Select EmpType From Personal Where CompID = '" & strCompID & "' and EmpID = '" & strEmpID & "' "
        strReturn = Bsp.DB.ExecuteScalar(strSQL, "eHRMSDB")
        Return strReturn
    End Function
#End Region
#Region "GroupID"
    Public Function Get_GroupID(ByVal OrganID As String) As String
        Dim strSQL As String

        strSQL = "select GroupID from OrganizationFlow where OrganID =" & Bsp.Utility.Quote(OrganID)
        Return Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")

    End Function
    '20150325 wei add
    Public Function Get_CGroupID(ByVal OrganID As String) As String
        Dim strSQL As String

        strSQL = "select GroupID from COrganization where OrganID =" & Bsp.Utility.Quote(OrganID)
        Return Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")

    End Function
    Public Function Get_GroupInfo(ByVal OrganID As String) As String
        Dim strSQL As String

        strSQL = "select OrganName as GroupName from OrganizationFlow where OrganID =" & Bsp.Utility.Quote(OrganID)
        Return Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")

    End Function
    Public Function Get_CGroupInfo(ByVal OrganID As String) As String
        Dim strSQL As String

        strSQL = "select OrganName as GroupName from COrganizationFlow where OrganID =" & Bsp.Utility.Quote(OrganID)
        Return Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")

    End Function
#End Region


#Region "EmpAdditionWait"
    Public Function DeleteEmpAdditionWait(ByVal beEmpAdditionWait As beEmpAdditionWait.Row) As Boolean
        Dim bsEmpAdditionWait As New beEmpAdditionWait.Service()
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Delete From EmpAdditionWait Where CompID = " & Bsp.Utility.Quote(beEmpAdditionWait.CompID.Value))
        strSQL.AppendLine("And EmpID = " & Bsp.Utility.Quote(beEmpAdditionWait.EmpID.Value))
        strSQL.AppendLine("And ValidDate = " & Bsp.Utility.Quote(beEmpAdditionWait.ValidDate.Value))
        strSQL.AppendLine("And Seq = " & beEmpAdditionWait.Seq.Value)

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                'bsEmpAdditionWait.DeleteRowByPrimaryKey(beEmpAdditionWait, tran)
                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran, "eHRMSDB")

                tran.Commit()
                Return True
            Catch ex As Exception
                If inTrans Then tran.Rollback()
                Throw
            Finally
                tran.Dispose()
                If cn.State = ConnectionState.Open Then cn.Close()
            End Try
        End Using
    End Function

#End Region
#Region "Check RecID"
    Public Function CheckRecID(ByVal strCompID As String, ByVal strRecID As String, ByVal ContractDate As String) As Boolean
        Dim strSQL As String
        strSQL = "Select Count(*) Cnt From RE_ContractData"
        strSQL += " Where CompID=" & Bsp.Utility.Quote(strCompID)
        strSQL += " And RecID=" & Bsp.Utility.Quote(strRecID)
        strSQL += " And ContractDate=" & Bsp.Utility.Quote(ContractDate)
        Return IIf(Bsp.DB.ExecuteScalar(strSQL, "Recruit") > 0, True, False)
    End Function
#End Region

#Region "EmployeeLogWait"
    Public Function QueryEmployeeLogWait(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()
        Dim strFieldNames As New StringBuilder()
        Dim strValidDate As String = ""
        Dim strSeq As String = ""


        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine("And E.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "EmpID"
                        strSQL.AppendLine("And E.EmpID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "ValidDate"
                        strSQL.AppendLine("And convert(char(10),E.ModifyDate,111) >= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        strValidDate = Bsp.Utility.Quote(ht(strKey).ToString())
                    Case "Seq"
                        'strSQL.AppendLine("And W.Wait_Seq = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        strSeq = Bsp.Utility.Quote(ht(strKey).ToString())
                End Select
            End If
        Next

        strFieldNames.AppendLine("E.CompID,E.EmpID,E.IDNo,E.Reason,E.ModifyDate,Convert(char(10),E.ModifyDate,111) as ModifyDateShow,E.Seq,Case When W.EmpID is null Then " & strValidDate & " Else isnull(Convert(char(10),W.Wait_ValidDate,111),'') End as Wait_ValidDate,")
        strFieldNames.AppendLine("Case When W.EmpID is null Then " & strSeq & " Else W.Wait_Seq End as Wait_Seq,")
        strFieldNames.AppendLine("Case When W.EmpID is null Then '否' Else '是' End as ExitEmployeeLogWait,")
        strFieldNames.AppendLine("Case When W.EmpID is null Then convert(char(10)," & strValidDate & ",111) Else convert(char(10),W.Wait_ValidDate,111) End as Wait_ValidDateShow,E.Reason,E.Reason + '-' + isnull(ER.Remark,'') as ReasonName,")
        strFieldNames.AppendLine("E.Company,E.DeptID,E.DeptName,E.OrganID,E.OrganName,E.PositionID,E.Position,E.WorkTypeID,E.WorkType,E.TitleName")

        Return GetEmployeeLogWaitInfo(strFieldNames.ToString(), strSQL.ToString())
    End Function
    Public Function GetEmployeeLogWaitInfo(Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames = "" Then FieldNames = "*"

        strSQL.AppendLine("Select " & FieldNames)
        strSQL.AppendLine("From EmployeeLog E")
        strSQL.AppendLine("Left Join EmployeeLogWait W on E.CompID=W.CompID and E.EmpID=W.EmpID and E.IDNo=W.IDNo and E.Reason=W.Reason and E.ModifyDate=W.ModifyDate")
        strSQL.AppendLine("Left join EmployeeReason ER on E.Reason = ER.Reason")
        strSQL.AppendLine("Where 1 = 1")
        If CondStr <> "" Then strSQL.AppendLine(CondStr)
        strSQL.AppendLine("Order by E.ModifyDate , E.EmpID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

    Public Function AddEmployeeLogWati(ByVal beEmployeeLogWait As beEmployeeLogWait.Row) As Boolean
        Dim bsEmployeeLogWait As New beEmployeeLogWait.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                'If bsEmployeeWait.Insert(beEmployeeWait) = 0 Then Return False
                bsEmployeeLogWait.Insert(beEmployeeLogWait, tran)

                tran.Commit()
            Catch ex As Exception
                tran.Rollback()
                Throw
            Finally
                If tran IsNot Nothing Then tran.Dispose()
            End Try
        End Using

        Return True

    End Function
    Public Function UpdateEmployeeLogWati(ByVal beEmployeeLogWait As beEmployeeLogWait.Row) As Boolean
        Dim bsEmployeeLogWait As New beEmployeeLogWait.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                'If bsEmployeeWait.Insert(beEmployeeWait) = 0 Then Return False
                bsEmployeeLogWait.Update(beEmployeeLogWait, tran)

                tran.Commit()
            Catch ex As Exception
                tran.Rollback()
                Throw
            Finally
                If tran IsNot Nothing Then tran.Dispose()
            End Try
        End Using

        Return True
    End Function
    Public Function DeleteEmployeeLogWait(ByVal beEmployeeLogWait As beEmployeeLogWait.Row) As Boolean
        Dim bsEmployeeLogWait As New beEmployeeLogWait.Service()
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Delete From EmployeeLogWait Where CompID = " & Bsp.Utility.Quote(beEmployeeLogWait.CompID.Value))
        strSQL.AppendLine("And EmpID = " & Bsp.Utility.Quote(beEmployeeLogWait.EmpID.Value))
        strSQL.AppendLine("And Wait_ValidDate = " & Bsp.Utility.Quote(beEmployeeLogWait.Wait_ValidDate.Value))
        strSQL.AppendLine("And Wait_Seq = " & Bsp.Utility.Quote(beEmployeeLogWait.Wait_Seq.Value))
        strSQL.AppendLine("And IDNo = " & Bsp.Utility.Quote(beEmployeeLogWait.IDNo.Value))
        strSQL.AppendLine("And ModifyDate = " & Bsp.Utility.Quote(beEmployeeLogWait.ModifyDate.Value))
        strSQL.AppendLine("And Reason = " & Bsp.Utility.Quote(beEmployeeLogWait.Reason.Value))

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                bsEmployeeLogWait.DeleteRowByPrimaryKey(beEmployeeLogWait, tran)
                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran, "eHRMSDB")

                tran.Commit()
                Return True
            Catch ex As Exception
                If inTrans Then tran.Rollback()
                Throw
            Finally
                tran.Dispose()
                If cn.State = ConnectionState.Open Then cn.Close()
            End Try
        End Using
    End Function
    Public Function DeleteEmployeeLogWaitByEmployeeWait(ByVal beEmployeeLogWait As beEmployeeLogWait.Row) As Boolean
        Dim bsEmployeeLogWait As New beEmployeeLogWait.Service()
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Delete From EmployeeLogWait Where CompID = " & Bsp.Utility.Quote(beEmployeeLogWait.CompID.Value))
        strSQL.AppendLine("And EmpID = " & Bsp.Utility.Quote(beEmployeeLogWait.EmpID.Value))
        strSQL.AppendLine("And Wait_ValidDate = " & Bsp.Utility.Quote(beEmployeeLogWait.Wait_ValidDate.Value))
        strSQL.AppendLine("And Wait_Seq = " & Bsp.Utility.Quote(beEmployeeLogWait.Wait_Seq.Value))
        'strSQL.AppendLine("And IDNo = " & Bsp.Utility.Quote(beEmployeeLogWait.IDNo.Value))
        'strSQL.AppendLine("And ModifyDate = " & Bsp.Utility.Quote(beEmployeeLogWait.ModifyDate.Value))
        'strSQL.AppendLine("And Reason = " & Bsp.Utility.Quote(beEmployeeLogWait.Reason.Value))

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                'bsEmployeeLogWait.DeleteRowByPrimaryKey(beEmployeeLogWait, tran)
                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran, "eHRMSDB")

                tran.Commit()
                Return True
            Catch ex As Exception
                If inTrans Then tran.Rollback()
                Throw
            Finally
                tran.Dispose()
                If cn.State = ConnectionState.Open Then cn.Close()
            End Try
        End Using
    End Function
#End Region
#Region "取功能負責人(團保作業)"
    Public Function GetClerk(ByVal CompID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select isnull(P.Name,'') as Clerk,isnull(M.Telephone,'') as ClerkTel")
        strSQL.AppendLine("From Maintain M")
        strSQL.AppendLine("Inner join Personal P on M.EmpComp=P.CompID and M.EmpID=P.EmpID")
        strSQL.AppendLine("Where M.FunctionID='320' and M.Role='2'")
        strSQL.AppendLine("And M.CompID = " & Bsp.Utility.Quote(CompID))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

#End Region
#Region "Execute SP"
    Public Function Execute_sHR3001(ByVal CompID As String, ByVal EmpID As String, ByVal ValidDate As String, ByVal Seq As String, ByVal Probation_Comp As String, ByVal Clerk As String, ByVal ClerkTel As String) As String

        Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
        Dim dbCommand As DbCommand = db.GetStoredProcCommand("sHR3001")
        Dim RefNeedMailFlag As String = ""
        Dim sp_Complete1 As Integer = 0
        Dim sp_ErrorSave As Integer = 0

        Dim strReturnValue As String = ";"

        db.AddInParameter(dbCommand, "@vCompID", DbType.String, CompID)
        db.AddInParameter(dbCommand, "@vEmpID", DbType.String, EmpID)
        db.AddInParameter(dbCommand, "@vValidDate", DbType.DateTime, ValidDate)
        db.AddInParameter(dbCommand, "@vSeq", DbType.Int16, Seq)
        db.AddInParameter(dbCommand, "@Probation_Comp", DbType.String, Probation_Comp)
        db.AddInParameter(dbCommand, "@Clerk", DbType.String, Clerk)
        db.AddInParameter(dbCommand, "@ClerkTel", DbType.String, ClerkTel)
        db.AddInParameter(dbCommand, "@Src", DbType.String, "One")

        db.AddOutParameter(dbCommand, "@RefNeedMailFlag", DbType.String, 4)
        db.AddOutParameter(dbCommand, "@sp_Complete1", DbType.Int16, 4)
        db.AddOutParameter(dbCommand, "@sp_ErrorSave", DbType.Int16, 4)

        db.ExecuteNonQuery(dbCommand)

        sp_Complete1 = db.GetParameterValue(dbCommand, "@sp_Complete1")
        sp_ErrorSave = db.GetParameterValue(dbCommand, "@sp_ErrorSave")
        strReturnValue = sp_Complete1 & ";" & sp_ErrorSave

        Return strReturnValue

    End Function

#End Region
#Region "檢核取資料使用"
    Public Function GetCheckMsg(ByVal strField As String, ByVal strTable As String, ByVal strWhere As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select " + strField)
        strSQL.AppendLine("From " + strTable)
        strSQL.AppendLine("Where 1=1 " + strWhere)

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

#End Region
#Region "檢核同單位是同日是否有主管註記(待異動調兼紀錄"
    Public Function CheckOrgan_IsBoss(ByVal strType As String, ByVal strCompID As String, ByVal strEmpID As String, ByVal strValidDate As String, ByVal strReason As String, ByVal TmpSeq As Integer) As Boolean
        Dim strSQL As New StringBuilder()
        Select Case strType
            Case "Ins"
                strSQL.Length = 0
                strSQL.AppendLine("Select Count(*) From EmployeeWait E Inner join (")
                strSQL.AppendLine("Select AddCompID,AddDeptID,AddOrganID,AddFlowOrganID")
                strSQL.AppendLine("From Tmp_EmpAdditionWait")
                strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(strCompID))
                strSQL.AppendLine("And EmpID = " & Bsp.Utility.Quote(strEmpID))
                strSQL.AppendLine("And Seq = " & TmpSeq)
                strSQL.AppendLine("And Reason = '1' And IsBoss='1'")
                strSQL.AppendLine(") T on E.CompID=T.AddCompID and E.OrganID=T.AddOrganID")
                strSQL.AppendLine("Where E.CompID = E.NewCompID")
                strSQL.AppendLine("And E.EmpID <> " & Bsp.Utility.Quote(strEmpID))
                strSQL.AppendLine("And E.ValidMark = '0' And E.IsBoss='1'")
                strSQL.AppendLine("And E.ValidDate = " & Bsp.Utility.Quote(strValidDate))
                If Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB") > 0 Then
                    Return False
                End If
                strSQL.Length = 0
                strSQL.AppendLine("Select Count(*) From EmpAdditionWait E Inner join (")
                strSQL.AppendLine("Select AddCompID,AddDeptID,AddOrganID,AddFlowOrganID")
                strSQL.AppendLine("From Tmp_EmpAdditionWait")
                strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(strCompID))
                strSQL.AppendLine("And EmpID = " & Bsp.Utility.Quote(strEmpID))
                strSQL.AppendLine("And Seq = " & TmpSeq)
                strSQL.AppendLine("And Reason = '1' And IsBoss='1'")
                strSQL.AppendLine(") T on E.AddCompID=T.AddCompID and E.AddOrganID=T.AddOrganID")
                strSQL.AppendLine("Where E.Reason='1' and E.ValidMark = '0' And E.IsBoss='1'")
                strSQL.AppendLine("And E.EmpID <> " & Bsp.Utility.Quote(strEmpID))
                strSQL.AppendLine("And E.ValidDate = " & Bsp.Utility.Quote(strValidDate))
                If Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB") > 0 Then
                    Return False
                End If

                strSQL.Length = 0
                strSQL.AppendLine("Select Count(*) From EmployeeWait E Inner join (")
                strSQL.AppendLine("Select AddCompID,AddDeptID,AddOrganID,AddFlowOrganID")
                strSQL.AppendLine("From Tmp_EmpAdditionWait")
                strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(strCompID))
                strSQL.AppendLine("And EmpID = " & Bsp.Utility.Quote(strEmpID))
                strSQL.AppendLine("And Seq = " & TmpSeq)
                strSQL.AppendLine("And Reason = '1' And IsGroupBoss='1'")
                strSQL.AppendLine(") T on E.CompID=T.AddCompID and E.FlowOrganID=T.AddFlowOrganID")
                strSQL.AppendLine("Where E.CompID = E.NewCompID")
                strSQL.AppendLine("And E.EmpID <> " & Bsp.Utility.Quote(strEmpID))
                strSQL.AppendLine("And E.ValidMark = '0' And E.IsGroupBoss='1'")
                strSQL.AppendLine("And E.ValidDate = " & Bsp.Utility.Quote(strValidDate))
                If Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB") > 0 Then
                    Return False
                End If
                strSQL.Length = 0
                strSQL.AppendLine("Select Count(*) From EmpAdditionWait E Inner join (")
                strSQL.AppendLine("Select AddCompID,AddDeptID,AddOrganID,AddFlowOrganID")
                strSQL.AppendLine("From Tmp_EmpAdditionWait")
                strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(strCompID))
                strSQL.AppendLine("And EmpID = " & Bsp.Utility.Quote(strEmpID))
                strSQL.AppendLine("And Seq = " & TmpSeq)
                strSQL.AppendLine("And Reason = '1' And IsGroupBoss='1'")
                strSQL.AppendLine(") T on E.AddCompID=T.AddCompID and E.AddFlowOrganID=T.AddFlowOrganID")
                strSQL.AppendLine("Where E.Reason='1' and E.ValidMark = '0' And E.IsGroupBoss='1'")
                strSQL.AppendLine("And E.EmpID <> " & Bsp.Utility.Quote(strEmpID))
                strSQL.AppendLine("And E.ValidDate = " & Bsp.Utility.Quote(strValidDate))
                If Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB") > 0 Then
                    Return False
                End If
            Case "Upd"
                strSQL.Length = 0
                strSQL.AppendLine("Select Count(*) From EmployeeWait E Inner join (")
                strSQL.AppendLine("Select AddCompID,AddDeptID,AddOrganID,AddFlowOrganID")
                strSQL.AppendLine("From Tmp_EmpAdditionWait")
                strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(strCompID))
                strSQL.AppendLine("And EmpID = " & Bsp.Utility.Quote(strEmpID))
                strSQL.AppendLine("And Seq = " & TmpSeq)
                strSQL.AppendLine("And Reason = '1' And IsBoss='1'")
                strSQL.AppendLine(") T on E.CompID=T.AddCompID and E.OrganID=T.AddOrganID")
                strSQL.AppendLine("Where E.CompID = E.NewCompID")
                strSQL.AppendLine("And E.EmpID <> " & Bsp.Utility.Quote(strEmpID))
                strSQL.AppendLine("And E.ValidMark = '0' And E.IsBoss='1'")
                strSQL.AppendLine("And E.ValidDate = " & Bsp.Utility.Quote(strValidDate))
                If Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB") > 0 Then
                    Return False
                End If
                strSQL.Length = 0
                strSQL.AppendLine("Select Count(*) From EmpAdditionWait E Inner join (")
                strSQL.AppendLine("Select AddCompID,AddDeptID,AddOrganID,AddFlowOrganID")
                strSQL.AppendLine("From Tmp_EmpAdditionWait")
                strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(strCompID))
                strSQL.AppendLine("And EmpID = " & Bsp.Utility.Quote(strEmpID))
                strSQL.AppendLine("And Seq = " & TmpSeq)
                strSQL.AppendLine("And Reason = '1' And IsBoss='1'")
                strSQL.AppendLine(") T on E.AddCompID=T.AddCompID and E.AddOrganID=T.AddOrganID")
                strSQL.AppendLine("Where E.Reason='1' and E.ValidMark = '0' And E.IsBoss='1'")
                strSQL.AppendLine("And E.EmpID <> " & Bsp.Utility.Quote(strEmpID))
                strSQL.AppendLine("And E.ValidDate = " & Bsp.Utility.Quote(strValidDate))
                If Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB") > 0 Then
                    Return False
                End If

                strSQL.Length = 0
                strSQL.AppendLine("Select Count(*) From EmployeeWait E Inner join (")
                strSQL.AppendLine("Select AddCompID,AddDeptID,AddOrganID,AddFlowOrganID")
                strSQL.AppendLine("From Tmp_EmpAdditionWait")
                strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(strCompID))
                strSQL.AppendLine("And EmpID = " & Bsp.Utility.Quote(strEmpID))
                strSQL.AppendLine("And Seq = " & TmpSeq)
                strSQL.AppendLine("And Reason = '1' And IsGroupBoss='1'")
                strSQL.AppendLine(") T on E.CompID=T.AddCompID and E.FlowOrganID=T.AddFlowOrganID")
                strSQL.AppendLine("Where E.CompID = E.NewCompID")
                strSQL.AppendLine("And E.EmpID <> " & Bsp.Utility.Quote(strEmpID))
                strSQL.AppendLine("And E.ValidMark = '0' And E.IsGroupBoss='1'")
                strSQL.AppendLine("And E.ValidDate = " & Bsp.Utility.Quote(strValidDate))
                If Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB") > 0 Then
                    Return False
                End If
                strSQL.Length = 0
                strSQL.AppendLine("Select Count(*) From EmpAdditionWait E Inner join (")
                strSQL.AppendLine("Select AddCompID,AddDeptID,AddOrganID,AddFlowOrganID")
                strSQL.AppendLine("From Tmp_EmpAdditionWait")
                strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(strCompID))
                strSQL.AppendLine("And EmpID = " & Bsp.Utility.Quote(strEmpID))
                strSQL.AppendLine("And Seq = " & TmpSeq)
                strSQL.AppendLine("And Reason = '1' And IsGroupBoss='1'")
                strSQL.AppendLine(") T on E.AddCompID=T.AddCompID and E.AddFlowOrganID=T.AddFlowOrganID")
                strSQL.AppendLine("Where E.Reason='1' and E.ValidMark = '0' And E.IsGroupBoss='1'")
                strSQL.AppendLine("And E.EmpID <> " & Bsp.Utility.Quote(strEmpID))
                strSQL.AppendLine("And E.ValidDate = " & Bsp.Utility.Quote(strValidDate))
                If Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB") > 0 Then
                    Return False
                End If
        End Select

        Return True
    End Function
#End Region
#Region "EmpAdditionWait"
    Public Function GetEmpAdditionWaitSeq(ByVal strCompID As String, ByVal strEmpID As String, ByVal strValidDate As String, ByVal intSeq As Integer) As Integer
        Dim strSQL As New StringBuilder
        Dim intAddSeq As Integer = 0

        strSQL.AppendLine("Select Max(AddSeq) as MaxSeq from EmpAdditionWait")
        strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(strCompID))
        strSQL.AppendLine("and EmpID =" & Bsp.Utility.Quote(strEmpID))
        strSQL.AppendLine("And convert(char(10),ValidDate,111) = " & Bsp.Utility.Quote(strValidDate))
        strSQL.AppendLine("And Seq = " & intSeq)

        If IsDBNull(Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")) Then
            intAddSeq = 1
        Else
            intAddSeq = Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB") + 1
        End If

        Return intAddSeq

    End Function
#End Region
#Region "取異動後任職狀況 WorkStatus_EmployeeReason /任職狀況VS待異動異動原因參數檔檢核"
    Public Function GetAfterWorkStatusTypeByWorkStatus_EmployeeReason(ByVal strBeforeWorkStatus As String, ByVal strReason As String) As String
        Dim strSQL As String

        strSQL = "Select AfterWorkStatusType "
        strSQL = strSQL & " from WorkStatus_EmployeeReason "
        strSQL = strSQL & "where BeforeWorkStatus = " & Bsp.Utility.Quote(strBeforeWorkStatus)
        strSQL = strSQL & "and Reason = " & Bsp.Utility.Quote(strReason)
        If Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB") = Nothing Then
            Return ""
        Else
            Return Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")
        End If

    End Function

#End Region

End Class
