'功能說明：HR1600員工調兼紀錄維護相關Function
'建立人員：Weicheng
'建立日期：2014.11.06
'******************************************************************************
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.IO
Imports System

Public Class HR1600
#Region "EmpAddition"
    Public Function QueryEmpAddition(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()
        Dim strFieldNames As New StringBuilder()
        Dim aryStr() As String

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine("And E.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "DeptID"
                        aryStr = ht(strKey).ToString().Split(";")
                        If aryStr(0).ToString.Trim() = "True" Then
                            strSQL.AppendLine("And P.DeptID = " & Bsp.Utility.Quote(aryStr(1).ToString.Trim()))
                        End If
                    Case "EmpID"
                        aryStr = ht(strKey).ToString().Split(";")
                        If aryStr(0).ToString.Trim() = "True" Then
                            strSQL.AppendLine("And E.EmpID = " & Bsp.Utility.Quote(aryStr(1).ToString.Trim()))
                        End If
                    Case "Name"
                        aryStr = ht(strKey).ToString().Split(";")
                        If aryStr(0).ToString.Trim() = "True" Then
                            strSQL.AppendLine("And P.NameN Like N'%" & aryStr(1).ToString.Trim() & "%'")    '20150724 wei modify
                        End If
                    Case "Reason"
                        aryStr = ht(strKey).ToString().Split(";")
                        If aryStr(0).ToString.Trim() = "True" Then
                            strSQL.AppendLine("And E.Reason = " & Bsp.Utility.Quote(aryStr(1).ToString.Trim()))
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
                    Case "AdditiionCompID"
                        aryStr = ht(strKey).ToString().Split(";")
                        If aryStr(0).ToString.Trim() = "True" Then
                            strSQL.AppendLine("And E.AddCompID = " & Bsp.Utility.Quote(aryStr(1).ToString.Trim()))
                        End If
                    Case "AdditiionDeptID"
                        aryStr = ht(strKey).ToString().Split(";")
                        If aryStr(0).ToString.Trim() = "True" Then
                            strSQL.AppendLine("And E.AddDeptID = " & Bsp.Utility.Quote(aryStr(1).ToString.Trim()))
                        End If
                End Select
            End If
        Next

        strFieldNames.AppendLine("E.AddCompID, isnull(C1.CompName,'') AddCompName,")
        strFieldNames.AppendLine("E.AddDeptID, isnull(O3.OrganName,'') as AddDeptName,")
        strFieldNames.AppendLine("E.AddOrganID,isnull(O4.OrganName,'') as AddOrganName,")
        strFieldNames.AppendLine("E.CompID,isnull(C.CompName,'') CompName,")
        strFieldNames.AppendLine("P.DeptID, isnull(O1.OrganName,'') as DeptName,")
        strFieldNames.AppendLine("P.OrganID,isnull(O2.OrganName,'') as OrganName,")
        strFieldNames.AppendLine("E.EmpID,P.NameN,E.Reason,isnull(M.CodeCName,'') ReasonName,Convert(char(10),E.ValidDate,111) as ValidDate")

        Return GetEmpAdditionInfo(strFieldNames.ToString(), strSQL.ToString())
    End Function
    Public Function GetEmpAdditionInfo(Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames = "" Then FieldNames = "*"

        strSQL.AppendLine("Select " & FieldNames)
        strSQL.AppendLine("From EmpAddition E")
        strSQL.AppendLine("Left join Personal P on E.CompID = P.CompID and E.EmpID = P.EmpID")
        strSQL.AppendLine("Left join Company C on E.CompID = C.CompID")
        strSQL.AppendLine("Left join Company C1 on E.AddCompID = C1.CompID")
        strSQL.AppendLine("Left join Organization O1 on P.CompID = O1.CompID and P.DeptID = O1.OrganID")
        strSQL.AppendLine("Left join Organization O2 on P.CompID = O2.CompID and P.OrganID = O2.OrganID")
        strSQL.AppendLine("Left join Organization O3 on E.AddCompID = O3.CompID and E.AddDeptID = O3.OrganID")
        strSQL.AppendLine("Left join Organization O4 on E.AddCompID = O4.CompID and E.AddOrganID = O4.OrganID")
        strSQL.AppendLine("Left join HRCodeMap M ON E.Reason = M.Code and M.TabName = 'EmpAddition' and M.FldName = 'Reason'")
        strSQL.AppendLine("Where 1 = 1")
        If CondStr <> "" Then strSQL.AppendLine(CondStr)
        strSQL.AppendLine("Order by E.ValidDate desc ,E.Reason,E.AddCompID, E.AddDeptID, E.AddOrganID,E.EmpID") '20150708 wei modify 調整排序 先依生效日,再依原因

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
    Public Function QueryEmpAdditionByDownload(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()
        Dim strFieldNames As New StringBuilder()
        Dim aryStr() As String

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine("And E.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "DeptID"
                        aryStr = ht(strKey).ToString().Split(";")
                        If aryStr(0).ToString.Trim() = "True" Then
                            strSQL.AppendLine("And P.DeptID = " & Bsp.Utility.Quote(aryStr(1).ToString.Trim()))
                        End If
                    Case "EmpID"
                        aryStr = ht(strKey).ToString().Split(";")
                        If aryStr(0).ToString.Trim() = "True" Then
                            strSQL.AppendLine("And E.EmpID = " & Bsp.Utility.Quote(aryStr(1).ToString.Trim()))
                        End If
                    Case "Name"
                        aryStr = ht(strKey).ToString().Split(";")
                        If aryStr(0).ToString.Trim() = "True" Then
                            strSQL.AppendLine("And P.NameN Like N'%" & aryStr(1).ToString.Trim() & "%'")    '20150724 wei modify
                        End If
                    Case "Reason"
                        aryStr = ht(strKey).ToString().Split(";")
                        If aryStr(0).ToString.Trim() = "True" Then
                            strSQL.AppendLine("And E.Reason = " & Bsp.Utility.Quote(aryStr(1).ToString.Trim()))
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
                    Case "AdditiionCompID"
                        aryStr = ht(strKey).ToString().Split(";")
                        If aryStr(0).ToString.Trim() = "True" Then
                            strSQL.AppendLine("And E.AddCompID = " & Bsp.Utility.Quote(aryStr(1).ToString.Trim()))
                        End If
                    Case "AdditiionDeptID"
                        aryStr = ht(strKey).ToString().Split(";")
                        If aryStr(0).ToString.Trim() = "True" Then
                            strSQL.AppendLine("And E.AddDeptID = " & Bsp.Utility.Quote(aryStr(1).ToString.Trim()))
                        End If
                End Select
            End If
        Next

        strFieldNames.AppendLine("isnull(C1.CompName,'') as '調兼公司',")
        strFieldNames.AppendLine("isnull(O3.OrganName,'') as '調兼部門',")
        strFieldNames.AppendLine("isnull(O4.OrganName,'') as '調兼科組課',")
        strFieldNames.AppendLine("isnull(C.CompName,'') as '現任公司',")
        strFieldNames.AppendLine("isnull(O1.OrganName,'') as '現任部門',")
        strFieldNames.AppendLine("isnull(O2.OrganName,'') as '現任科組課',")
        strFieldNames.AppendLine("E.EmpID as '員工編號',P.NameN as '姓名',isnull(M.CodeCName,'') as '狀態',Convert(char(10),E.ValidDate,111) as '生效日',")
        strFieldNames.AppendLine("isnull(Po.Remark,'') as '職位(現況)',")
        strFieldNames.AppendLine("isnull(W.Remark,'') as '工作性質(現況)',")
        strFieldNames.AppendLine("isnull(T.TitleName,'') as '子公司職等(現況)',")
        strFieldNames.AppendLine("isnull(P.HoldingRankID,'') as '金控職等(現況)',")
        strFieldNames.AppendLine("isnull(H.TitleName,'') as '金控職級(現況)',")
        strFieldNames.AppendLine("E.FileNo as '人令',")
        strFieldNames.AppendLine("E.Remark as '備註',")
        strFieldNames.AppendLine("case when O5.Boss is null then 0 else 1 end as '兼任部門主管(現況)',")
        strFieldNames.AppendLine("case when O6.Boss is null then 0 else 1 end as '兼任科組課主管(現況)',")
        strFieldNames.AppendLine("convert(char(10),E.CreateDate,111) as '建檔日期',")
        strFieldNames.AppendLine("E.CreateID as '建檔人員',")
        strFieldNames.AppendLine("convert(char(10),E.LastChgDate,111) as '最後異動日期',")
        strFieldNames.AppendLine("E.LastChgID as '最後異動人員'")

        Return GetEmpAdditionInfoByDownload(strFieldNames.ToString(), strSQL.ToString())
    End Function
    Public Function GetEmpAdditionInfoByDownload(Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames = "" Then FieldNames = "*"

        strSQL.AppendLine("Select " & FieldNames)
        strSQL.AppendLine("From EmpAddition E")
        strSQL.AppendLine("Left join Personal P on E.CompID = P.CompID and E.EmpID = P.EmpID")
        strSQL.AppendLine("Left join Company C on E.CompID = C.CompID")
        strSQL.AppendLine("Left join Company C1 on E.AddCompID = C1.CompID")
        strSQL.AppendLine("Left join Organization O1 on P.CompID = O1.CompID and P.DeptID = O1.OrganID")
        strSQL.AppendLine("Left join Organization O2 on P.CompID = O2.CompID and P.OrganID = O2.OrganID")
        strSQL.AppendLine("Left join Organization O3 on E.AddCompID = O3.CompID and E.AddDeptID = O3.OrganID")
        strSQL.AppendLine("Left join Organization O4 on E.AddCompID = O4.CompID and E.AddOrganID = O4.OrganID")
        strSQL.AppendLine("Left join HRCodeMap M on E.Reason = M.Code and M.TabName = 'EmpAddition' and M.FldName = 'Reason'")
        strSQL.AppendLine("Left join EmpPosition EP ON EP.CompID = P.CompID and EP.EmpID = P.EmpID and EP.PrincipalFlag = '1'")
        strSQL.AppendLine("Left join Position Po on EP.CompID = Po.CompID and EP.PositionID = Po.PositionID")
        strSQL.AppendLine("Left join EmpWorkType EW on EW.CompID = P.CompID and EW.EmpID = P.EmpID and EW.PrincipalFlag = '1'")
        strSQL.AppendLine("Left join WorkType W on EW.CompID = W.CompID and EW.WorkTypeID = W.WorkTypeID")
        strSQL.AppendLine("Left join Title T on P.CompID = T.CompID and P.RankID = T.RankID and P.TitleID = T.TitleID")
        strSQL.AppendLine("Left join TitleByHolding H on P.HoldingRankID = H.HoldingRankID")
        strSQL.AppendLine("Left join Organization O5 on O5.CompID = E.AddCompID and O5.OrganID = E.AddDeptID and O5.OrganID = O5.DeptID and O5.Boss = E.EmpID and O5.BossCompID = E.CompID")
        strSQL.AppendLine("Left join Organization O6 on O6.CompID = E.AddCompID and O6.OrganID = E.AddOrganID and O6.OrganID <> O6.DeptID and O6.Boss = E.EmpID and O6.BossCompID = E.CompID ")
        strSQL.AppendLine("Where 1 = 1")
        If CondStr <> "" Then strSQL.AppendLine(CondStr)
        strSQL.AppendLine("Order by E.ValidDate desc ,E.Reason,E.AddCompID, E.AddDeptID, E.AddOrganID,E.EmpID") '20150708 wei modify 調整排序 先依生效日,再依原因

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
    Public Function QueryEmpAdditionByDetail(ByVal CompID As String, ByVal EmpID As String, ByVal ValidDate As String, ByVal AddCompID As String, ByVal AddDeptID As String, ByVal AddOrganID As String) As DataTable
        Dim strSQL As New StringBuilder()
        Dim strFieldNames As New StringBuilder()

        strSQL.AppendLine("And E.CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine("And E.EmpID = " & Bsp.Utility.Quote(EmpID))
        strSQL.AppendLine("And E.ValidDate = " & Bsp.Utility.Quote(ValidDate))
        strSQL.AppendLine("And E.AddCompID = " & Bsp.Utility.Quote(AddCompID))
        strSQL.AppendLine("And E.AddDeptID = " & Bsp.Utility.Quote(AddDeptID))
        strSQL.AppendLine("And E.AddOrganID = " & Bsp.Utility.Quote(AddOrganID))

        strFieldNames.AppendLine("E.AddCompID, isnull(C1.CompName,'') AddCompName,")
        strFieldNames.AppendLine("E.AddDeptID, isnull(O3.OrganName,'') as AddDeptName,")
        strFieldNames.AppendLine("E.AddOrganID,isnull(O4.OrganName,'') as AddOrganName,")
        strFieldNames.AppendLine("E.AddFlowOrganID,isnull(F.OrganName,'') as AddFlowOrganName,")
        strFieldNames.AppendLine("E.CompID,isnull(C.CompName,'') CompName,")
        strFieldNames.AppendLine("P.DeptID, isnull(O1.OrganName,'') as DeptName,")
        strFieldNames.AppendLine("P.OrganID,isnull(O2.OrganName,'') as OrganName,")
        strFieldNames.AppendLine("E.EmpID,P.NameN,isnull(P.RankID,'') RankID,E.Reason,isnull(M.CodeCName,'') as ReasonName,Convert(char(10),E.ValidDate,111) as ValidDate,")
        strFieldNames.AppendLine("isnull(EP.PositionID,'') PositionID,isnull(Po.Remark,'') as Position,")
        strFieldNames.AppendLine("isnull(EW.WorkTypeID,'') WorkTypeID,isnull(W.Remark,'') as WorkType,")
        strFieldNames.AppendLine("isnull(T.TitleName,'') Title,isnull(P.HoldingRankID,'') HoldingRank, isnull(H.TitleName,'') as HoldingTitle,")
        strFieldNames.AppendLine("E.FileNo,E.Remark,E.BossType,E.IsBoss,E.IsSecBoss,E.IsGroupBoss,E.IsSecGroupBoss,")
        strFieldNames.AppendLine("case when O5.Boss is null then 0 else 1 end as AddDeptBoss,")
        strFieldNames.AppendLine("case when O6.Boss is null then 0 else 1 end as AddOrganBoss,")
        strFieldNames.AppendLine("convert(char(10),E.CreateDate,111) as CreateDate,")
        strFieldNames.AppendLine("E.CreateID as CreateID,")
        strFieldNames.AppendLine("convert(char(10),E.LastChgDate,111) as LastChgDate,")
        strFieldNames.AppendLine("E.LastChgID as LastChgID")

        Return GetEmpAdditionInfoByDetail(strFieldNames.ToString(), strSQL.ToString())
    End Function
    Public Function GetEmpAdditionInfoByDetail(Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames = "" Then FieldNames = "*"

        strSQL.AppendLine("Select " & FieldNames)
        strSQL.AppendLine("From EmpAddition E")
        strSQL.AppendLine("Left join Personal P on E.CompID = P.CompID and E.EmpID = P.EmpID")
        strSQL.AppendLine("Left join Company C on E.CompID = C.CompID")
        strSQL.AppendLine("Left join Company C1 on E.AddCompID = C1.CompID")
        strSQL.AppendLine("Left join Organization O1 on P.CompID = O1.CompID and P.DeptID = O1.OrganID")
        strSQL.AppendLine("Left join Organization O2 on P.CompID = O2.CompID and P.OrganID = O2.OrganID")
        strSQL.AppendLine("Left join Organization O3 on E.AddCompID = O3.CompID and E.AddDeptID = O3.OrganID")
        strSQL.AppendLine("Left join Organization O4 on E.AddCompID = O4.CompID and E.AddOrganID = O4.OrganID")
        strSQL.AppendLine("Left join OrganizationFlow F on E.AddFlowOrganID = F.OrganID")
        strSQL.AppendLine("Left join HRCodeMap M on E.Reason = M.Code and M.TabName = 'EmpAddition' and M.FldName = 'Reason'")
        strSQL.AppendLine("Left join EmpPosition EP ON EP.CompID = P.CompID and EP.EmpID = P.EmpID and EP.PrincipalFlag = '1'")
        strSQL.AppendLine("Left join Position Po on EP.CompID = Po.CompID and EP.PositionID = Po.PositionID")
        strSQL.AppendLine("Left join EmpWorkType EW on EW.CompID = P.CompID and EW.EmpID = P.EmpID and EW.PrincipalFlag = '1'")
        strSQL.AppendLine("Left join WorkType W on EW.CompID = W.CompID and EW.WorkTypeID = W.WorkTypeID")
        strSQL.AppendLine("Left join Title T on P.CompID = T.CompID and P.RankID = T.RankID and P.TitleID = T.TitleID")
        strSQL.AppendLine("Left join TitleByHolding H on P.HoldingRankID = H.HoldingRankID")
        strSQL.AppendLine("Left join Organization O5 on O5.CompID = E.AddCompID and O5.OrganID = E.AddDeptID and O5.OrganID = O5.DeptID and O5.Boss = E.EmpID and O5.BossCompID = E.CompID")
        strSQL.AppendLine("Left join Organization O6 on O6.CompID = E.AddCompID and O6.OrganID = E.AddOrganID and O6.OrganID <> O6.DeptID and O6.Boss = E.EmpID and O6.BossCompID = E.CompID ")
        strSQL.AppendLine("Where 1 = 1")
        If CondStr <> "" Then strSQL.AppendLine(CondStr)
        strSQL.AppendLine("Order by E.ValidDate desc ,E.Reason,E.AddCompID, E.AddDeptID, E.AddOrganID,E.EmpID") '20150708 wei modify 調整排序 先依生效日,再依原因

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region
#Region "EmpAdditionDetail"
    Public Function QueryEmpAdditionDetail(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()
        Dim strFieldNames As New StringBuilder()
        Dim aryStr() As String

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine("And E.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "DeptID"
                        aryStr = ht(strKey).ToString().Split(";")
                        If aryStr(0).ToString.Trim() = "True" Then
                            strSQL.AppendLine("And P.DeptID = " & Bsp.Utility.Quote(aryStr(1).ToString.Trim()))
                        End If
                    Case "EmpID"
                        aryStr = ht(strKey).ToString().Split(";")
                        If aryStr(0).ToString.Trim() = "True" Then
                            strSQL.AppendLine("And E.EmpID = " & Bsp.Utility.Quote(aryStr(1).ToString.Trim()))
                        End If
                    Case "Name"
                        aryStr = ht(strKey).ToString().Split(";")
                        If aryStr(0).ToString.Trim() = "True" Then
                            strSQL.AppendLine("And P.NameN Like N'%" & aryStr(1).ToString.Trim() & "%'")    '20150724 wei modify
                        End If
                    Case "Reason"
                        aryStr = ht(strKey).ToString().Split(";")
                        If aryStr(0).ToString.Trim() = "True" Then
                            strSQL.AppendLine("And E.Reason = " & Bsp.Utility.Quote(aryStr(1).ToString.Trim()))
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
                    Case "AdditiionCompID"
                        aryStr = ht(strKey).ToString().Split(";")
                        If aryStr(0).ToString.Trim() = "True" Then
                            strSQL.AppendLine("And E.AddCompID = " & Bsp.Utility.Quote(aryStr(1).ToString.Trim()))
                        End If
                    Case "AdditiionDeptID"
                        aryStr = ht(strKey).ToString().Split(";")
                        If aryStr(0).ToString.Trim() = "True" Then
                            strSQL.AppendLine("And E.AddDeptID = " & Bsp.Utility.Quote(aryStr(1).ToString.Trim()))
                        End If
                End Select
            End If
        Next

        strFieldNames.AppendLine("E.AddCompID, isnull(C1.CompName,'') AddCompName,")
        strFieldNames.AppendLine("E.AddDeptID, isnull(O3.OrganName,'') as AddDeptName,")
        strFieldNames.AppendLine("E.AddOrganID,isnull(O4.OrganName,'') as AddOrganName,")
        strFieldNames.AppendLine("E.CompID,isnull(C.CompName,'') CompName,")
        strFieldNames.AppendLine("P.DeptID, isnull(O1.OrganName,'') as DeptName,")
        strFieldNames.AppendLine("P.OrganID,isnull(O2.OrganName,'') as OrganName,")
        strFieldNames.AppendLine("E.EmpID,P.NameN,E.Reason,isnull(M.CodeCName,'') ReasonName,Convert(char(10),E.ValidDate,111) as ValidDate")

        Return GetEmpAdditionDetailInfo(strFieldNames.ToString(), strSQL.ToString())
    End Function
    Public Function GetEmpAdditionDetailInfo(Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames = "" Then FieldNames = "*"

        strSQL.AppendLine("Select " & FieldNames)
        strSQL.AppendLine("From EmpAdditionDetail E")
        strSQL.AppendLine("Left join Personal P on E.CompID = P.CompID and E.EmpID = P.EmpID")
        strSQL.AppendLine("Left join Company C on E.CompID = C.CompID")
        strSQL.AppendLine("Left join Company C1 on E.AddCompID = C1.CompID")
        strSQL.AppendLine("Left join Organization O1 on P.CompID = O1.CompID and P.DeptID = O1.OrganID")
        strSQL.AppendLine("Left join Organization O2 on P.CompID = O2.CompID and P.OrganID = O2.OrganID")
        strSQL.AppendLine("Left join Organization O3 on E.AddCompID = O3.CompID and E.AddDeptID = O3.OrganID")
        strSQL.AppendLine("Left join Organization O4 on E.AddCompID = O4.CompID and E.AddOrganID = O4.OrganID")
        strSQL.AppendLine("Left join HRCodeMap M ON E.Reason = M.Code and M.TabName = 'EmpAddition' and M.FldName = 'Reason'")
        strSQL.AppendLine("Where 1 = 1")
        If CondStr <> "" Then strSQL.AppendLine(CondStr)
        strSQL.AppendLine("Order by E.ValidDate desc ,E.Reason,E.AddCompID, E.AddDeptID, E.AddOrganID,E.EmpID") '20150708 wei modify 調整排序 先依生效日,再依原因

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
    Public Function QueryEmpAdditionDetailByDownload(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()
        Dim strFieldNames As New StringBuilder()
        Dim aryStr() As String

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine("And E.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "DeptID"
                        aryStr = ht(strKey).ToString().Split(";")
                        If aryStr(0).ToString.Trim() = "True" Then
                            strSQL.AppendLine("And P.DeptID = " & Bsp.Utility.Quote(aryStr(1).ToString.Trim()))
                        End If
                    Case "EmpID"
                        aryStr = ht(strKey).ToString().Split(";")
                        If aryStr(0).ToString.Trim() = "True" Then
                            strSQL.AppendLine("And E.EmpID = " & Bsp.Utility.Quote(aryStr(1).ToString.Trim()))
                        End If
                    Case "Name"
                        aryStr = ht(strKey).ToString().Split(";")
                        If aryStr(0).ToString.Trim() = "True" Then
                            strSQL.AppendLine("And P.NameN Like N'%" & aryStr(1).ToString.Trim() & "'")
                        End If
                    Case "Reason"
                        aryStr = ht(strKey).ToString().Split(";")
                        If aryStr(0).ToString.Trim() = "True" Then
                            strSQL.AppendLine("And E.Reason = " & Bsp.Utility.Quote(aryStr(1).ToString.Trim()))
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
                    Case "AdditiionCompID"
                        aryStr = ht(strKey).ToString().Split(";")
                        If aryStr(0).ToString.Trim() = "True" Then
                            strSQL.AppendLine("And E.AddCompID = " & Bsp.Utility.Quote(aryStr(1).ToString.Trim()))
                        End If
                    Case "AdditiionDeptID"
                        aryStr = ht(strKey).ToString().Split(";")
                        If aryStr(0).ToString.Trim() = "True" Then
                            strSQL.AppendLine("And E.AddDeptID = " & Bsp.Utility.Quote(aryStr(1).ToString.Trim()))
                        End If
                End Select
            End If
        Next

        strFieldNames.AppendLine("isnull(C1.CompName,'') as '調兼公司',")
        strFieldNames.AppendLine("isnull(O3.OrganName,'') as '調兼部門',")
        strFieldNames.AppendLine("isnull(O4.OrganName,'') as '調兼科組課',")
        strFieldNames.AppendLine("isnull(C.CompName,'') as '現任公司',")
        strFieldNames.AppendLine("isnull(O1.OrganName,'') as '現任部門',")
        strFieldNames.AppendLine("isnull(O2.OrganName,'') as '現任科組課',")
        strFieldNames.AppendLine("E.EmpID as '員工編號',P.NameN as '姓名',isnull(M.CodeCName,'') as '狀態',Convert(char(10),E.ValidDate,111) as '生效日',")
        strFieldNames.AppendLine("isnull(Po.Remark,'') as '職位(現況)',")
        strFieldNames.AppendLine("isnull(W.Remark,'') as '工作性質(現況)',")
        strFieldNames.AppendLine("isnull(T.TitleName,'') as '子公司職等(現況)',")
        strFieldNames.AppendLine("isnull(P.HoldingRankID,'') as '金控職等(現況)',")
        strFieldNames.AppendLine("isnull(H.TitleName,'') as '金控職級(現況)',")
        strFieldNames.AppendLine("E.FileNo as '人令',")
        strFieldNames.AppendLine("E.Remark as '備註',")
        strFieldNames.AppendLine("case when O5.Boss is null then 0 else 1 end as '兼任部門主管(現況)',")
        strFieldNames.AppendLine("case when O6.Boss is null then 0 else 1 end as '兼任科組課主管(現況)',")
        strFieldNames.AppendLine("convert(char(10),E.CreateDate,111) as '建檔日期',")
        strFieldNames.AppendLine("E.CreateID as '建檔人員',")
        strFieldNames.AppendLine("convert(char(10),E.LastChgDate,111) as '最後異動日期',")
        strFieldNames.AppendLine("E.LastChgID as '最後異動人員'")

        Return GetEmpAdditionDetailInfoByDownload(strFieldNames.ToString(), strSQL.ToString())
    End Function
    Public Function GetEmpAdditionDetailInfoByDownload(Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames = "" Then FieldNames = "*"

        strSQL.AppendLine("Select " & FieldNames)
        strSQL.AppendLine("From EmpAdditionDetail E")
        strSQL.AppendLine("Left join Personal P on E.CompID = P.CompID and E.EmpID = P.EmpID")
        strSQL.AppendLine("Left join Company C on E.CompID = C.CompID")
        strSQL.AppendLine("Left join Company C1 on E.AddCompID = C1.CompID")
        strSQL.AppendLine("Left join Organization O1 on P.CompID = O1.CompID and P.DeptID = O1.OrganID")
        strSQL.AppendLine("Left join Organization O2 on P.CompID = O2.CompID and P.OrganID = O2.OrganID")
        strSQL.AppendLine("Left join Organization O3 on E.AddCompID = O3.CompID and E.AddDeptID = O3.OrganID")
        strSQL.AppendLine("Left join Organization O4 on E.AddCompID = O4.CompID and E.AddOrganID = O4.OrganID")
        strSQL.AppendLine("Left join HRCodeMap M on E.Reason = M.Code and M.TabName = 'EmpAddition' and M.FldName = 'Reason'")
        strSQL.AppendLine("Left join EmpPosition EP ON EP.CompID = P.CompID and EP.EmpID = P.EmpID and EP.PrincipalFlag = '1'")
        strSQL.AppendLine("Left join Position Po on EP.CompID = Po.CompID and EP.PositionID = Po.PositionID")
        strSQL.AppendLine("Left join EmpWorkType EW on EW.CompID = P.CompID and EW.EmpID = P.EmpID and EW.PrincipalFlag = '1'")
        strSQL.AppendLine("Left join WorkType W on EW.CompID = W.CompID and EW.WorkTypeID = W.WorkTypeID")
        strSQL.AppendLine("Left join Title T on P.CompID = T.CompID and P.RankID = T.RankID and P.TitleID = T.TitleID")
        strSQL.AppendLine("Left join TitleByHolding H on P.HoldingRankID = H.HoldingRankID")
        strSQL.AppendLine("Left join Organization O5 on O5.CompID = E.AddCompID and O5.OrganID = E.AddDeptID and O5.OrganID = O5.DeptID and O5.Boss = E.EmpID and O5.BossCompID = E.CompID")
        strSQL.AppendLine("Left join Organization O6 on O6.CompID = E.AddCompID and O6.OrganID = E.AddOrganID and O6.OrganID <> O6.DeptID and O6.Boss = E.EmpID and O6.BossCompID = E.CompID ")
        strSQL.AppendLine("Where 1 = 1")
        If CondStr <> "" Then strSQL.AppendLine(CondStr)
        strSQL.AppendLine("Order by E.ValidDate desc ,E.Reason,E.AddCompID, E.AddDeptID, E.AddOrganID,E.EmpID") '20150708 wei modify 調整排序 先依生效日,再依原因

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

    Public Function QueryEmpAdditionDetailByDetail(ByVal CompID As String, ByVal EmpID As String, ByVal ValidDate As String, ByVal AddCompID As String, ByVal AddDeptID As String, ByVal AddOrganID As String) As DataTable
        Dim strSQL As New StringBuilder()
        Dim strFieldNames As New StringBuilder()

        strSQL.AppendLine("And E.CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine("And E.EmpID = " & Bsp.Utility.Quote(EmpID))
        strSQL.AppendLine("And E.ValidDate = " & Bsp.Utility.Quote(ValidDate))
        strSQL.AppendLine("And E.AddCompID = " & Bsp.Utility.Quote(AddCompID))
        strSQL.AppendLine("And E.AddDeptID = " & Bsp.Utility.Quote(AddDeptID))
        strSQL.AppendLine("And E.AddOrganID = " & Bsp.Utility.Quote(AddOrganID))

        strFieldNames.AppendLine("E.AddCompID, isnull(C1.CompName,'') AddCompName,")
        strFieldNames.AppendLine("E.AddDeptID, isnull(O3.OrganName,'') as AddDeptName,")
        strFieldNames.AppendLine("E.AddOrganID,isnull(O4.OrganName,'') as AddOrganName,")
        strFieldNames.AppendLine("E.AddFlowOrganID,isnull(F.OrganName,'') as AddFlowOrganName,")
        strFieldNames.AppendLine("E.CompID,isnull(C.CompName,'') CompName,")
        strFieldNames.AppendLine("P.DeptID, isnull(O1.OrganName,'') as DeptName,")
        strFieldNames.AppendLine("P.OrganID,isnull(O2.OrganName,'') as OrganName,")
        strFieldNames.AppendLine("E.EmpID,P.NameN,isnull(P.RankID,'') RankID,E.Reason,isnull(M.CodeCName,'') as ReasonName,Convert(char(10),E.ValidDate,111) as ValidDate,")
        strFieldNames.AppendLine("isnull(EP.PositionID,'') PositionID,isnull(Po.Remark,'') as Position,")
        strFieldNames.AppendLine("isnull(EW.WorkTypeID,'') WorkTypeID,isnull(W.Remark,'') as WorkType,")
        strFieldNames.AppendLine("isnull(T.TitleName,'') Title,isnull(P.HoldingRankID,'') HoldingRank, isnull(H.TitleName,'') as HoldingTitle,")
        strFieldNames.AppendLine("E.FileNo,E.Remark,E.BossType,E.IsBoss,E.IsSecBoss,E.IsGroupBoss,E.IsSecGroupBoss,")
        strFieldNames.AppendLine("case when O5.Boss is null then 0 else 1 end as AddDeptBoss,")
        strFieldNames.AppendLine("case when O6.Boss is null then 0 else 1 end as AddOrganBoss,")
        strFieldNames.AppendLine("convert(char(10),E.CreateDate,111) as CreateDate,")
        strFieldNames.AppendLine("E.CreateID as CreateID,")
        strFieldNames.AppendLine("convert(char(10),E.LastChgDate,111) as LastChgDate,")
        strFieldNames.AppendLine("E.LastChgID as LastChgID")

        Return GetEmpAdditionDetailInfoByDetail(strFieldNames.ToString(), strSQL.ToString())
    End Function
    Public Function GetEmpAdditionDetailInfoByDetail(Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames = "" Then FieldNames = "*"

        strSQL.AppendLine("Select " & FieldNames)
        strSQL.AppendLine("From EmpAdditionDetail E")
        strSQL.AppendLine("Left join Personal P on E.CompID = P.CompID and E.EmpID = P.EmpID")
        strSQL.AppendLine("Left join Company C on E.CompID = C.CompID")
        strSQL.AppendLine("Left join Company C1 on E.AddCompID = C1.CompID")
        strSQL.AppendLine("Left join Organization O1 on P.CompID = O1.CompID and P.DeptID = O1.OrganID")
        strSQL.AppendLine("Left join Organization O2 on P.CompID = O2.CompID and P.OrganID = O2.OrganID")
        strSQL.AppendLine("Left join Organization O3 on E.AddCompID = O3.CompID and E.AddDeptID = O3.OrganID")
        strSQL.AppendLine("Left join Organization O4 on E.AddCompID = O4.CompID and E.AddOrganID = O4.OrganID")
        strSQL.AppendLine("Left join OrganizationFlow F on E.AddFlowOrganID = F.OrganID")
        strSQL.AppendLine("Left join HRCodeMap M on E.Reason = M.Code and M.TabName = 'EmpAddition' and M.FldName = 'Reason'")
        strSQL.AppendLine("Left join EmpPosition EP ON EP.CompID = P.CompID and EP.EmpID = P.EmpID and EP.PrincipalFlag = '1'")
        strSQL.AppendLine("Left join Position Po on EP.CompID = Po.CompID and EP.PositionID = Po.PositionID")
        strSQL.AppendLine("Left join EmpWorkType EW on EW.CompID = P.CompID and EW.EmpID = P.EmpID and EW.PrincipalFlag = '1'")
        strSQL.AppendLine("Left join WorkType W on EW.CompID = W.CompID and EW.WorkTypeID = W.WorkTypeID")
        strSQL.AppendLine("Left join Title T on P.CompID = T.CompID and P.RankID = T.RankID and P.TitleID = T.TitleID")
        strSQL.AppendLine("Left join TitleByHolding H on P.HoldingRankID = H.HoldingRankID")
        strSQL.AppendLine("Left join Organization O5 on O5.CompID = E.AddCompID and O5.OrganID = E.AddDeptID and O5.OrganID = O5.DeptID and O5.Boss = E.EmpID and O5.BossCompID = E.CompID")
        strSQL.AppendLine("Left join Organization O6 on O6.CompID = E.AddCompID and O6.OrganID = E.AddOrganID and O6.OrganID <> O6.DeptID and O6.Boss = E.EmpID and O6.BossCompID = E.CompID ")
        strSQL.AppendLine("Where 1 = 1")
        If CondStr <> "" Then strSQL.AppendLine(CondStr)
        strSQL.AppendLine("Order by E.ValidDate desc ,E.Reason,E.AddCompID, E.AddDeptID, E.AddOrganID,E.EmpID") '20150708 wei modify 調整排序 先依生效日,再依原因

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "EmpAdditionLog"
    Public Function QueryEmpAdditionLog(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()
        Dim strFieldNames As New StringBuilder()
        Dim aryStr() As String

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine("And E.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "EmpID"
                        strSQL.AppendLine("And E.EmpID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next

        strFieldNames.AppendLine("E.AddCompID, isnull(C1.CompName,'') AddCompName,")
        strFieldNames.AppendLine("E.AddDeptID, E.AddDeptName,")
        strFieldNames.AppendLine("E.AddOrganID, E.AddOrganName,")
        strFieldNames.AppendLine("E.CompID, isnull(C.CompName,'') CompName,")
        strFieldNames.AppendLine("E.DeptID, E.DeptName,")
        strFieldNames.AppendLine("E.OrganID, E.OrganName,")
        strFieldNames.AppendLine("E.EmpID,P.NameN,E.Reason,isnull(M.CodeCName,'') ReasonName,Convert(char(10),E.ValidDate,111) as ValidDate,")
        strFieldNames.AppendLine("E.RankID,E.TitleName,E.HoldingRankID,E.HoldingTitleName,")
        strFieldNames.AppendLine("E.PositionName,E.WorkTypeName,E.Remark,E.FileNo,")
        strFieldNames.AppendLine("Case When E.BossType='1' Then '1 主要' When E.BossType='2' Then '2 兼任' Else '' End As BossType,Convert(bit,Case When E.IsBoss <>'1' Then '0' Else E.IsBoss End) as IsBoss,Convert(bit,Case When E.IsSecBoss <>'1' Then '0' Else E.IsSecBoss End) as IsSecBoss,Convert(bit,Case When E.IsGroupBoss <>'1' Then '0' Else E.IsGroupBoss End) as IsGroupBoss,Convert(bit,Case When E.IsSecGroupBoss <>'1' Then '0' Else E.IsSecGroupBoss End) as IsSecGroupBoss,")
        strFieldNames.AppendLine("E.CreateDate,E.CreateID,convert(char(19),E.CreateDate,21) + ' ' + E.CreateID as CreateData,E.LastChgDate,E.LastChgID,convert(char(19),E.LastChgDate,21) + ' ' + E.LastChgID as LastChg")

        Return GetEmpAdditionLogInfo(strFieldNames.ToString(), strSQL.ToString())
    End Function
    Public Function GetEmpAdditionLogInfo(Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames = "" Then FieldNames = "*"

        strSQL.AppendLine("Select " & FieldNames)
        strSQL.AppendLine("From EmpAdditionLog E")
        strSQL.AppendLine("Left join Personal P on E.CompID = P.CompID and E.EmpID = P.EmpID")
        strSQL.AppendLine("Left join Company C on E.CompID = C.CompID")
        strSQL.AppendLine("Left join Company C1 on E.AddCompID = C1.CompID")
        strSQL.AppendLine("Left join HRCodeMap M ON E.Reason = M.Code and M.TabName = 'EmpAddition' and M.FldName = 'Reason'")
        strSQL.AppendLine("Where 1 = 1")
        If CondStr <> "" Then strSQL.AppendLine(CondStr)
        strSQL.AppendLine("Order by E.AddCompID, E.AddDeptID, E.AddOrganID,E.ValidDate")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region
#Region "Add"
    Public Function AddEmpAddition(ByVal beEmpAddition As beEmpAddition.Row, ByVal beEmpAdditionDetail As beEmpAdditionDetail.Row, ByVal beEmpAdditionLog As beEmpAdditionLog.Row) As Boolean
        Dim bsEmpAddition As New beEmpAddition.Service()
        Dim bsEmpAdditionDetail As New beEmpAdditionDetail.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                Dim strSQL As New StringBuilder()
                'EmpAddition
                If beEmpAddition.Reason.Value = "1" Then
                    bsEmpAddition.Insert(beEmpAddition, tran)
                Else
                    '20150709 wei add
                    strSQL.AppendLine("Delete From EmpAddition")
                    strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(beEmpAddition.CompID.Value))
                    strSQL.AppendLine("And EmpID = " & Bsp.Utility.Quote(beEmpAddition.EmpID.Value))
                    strSQL.AppendLine("And AddCompID = " & Bsp.Utility.Quote(beEmpAddition.AddCompID.Value))
                    strSQL.AppendLine("And AddDeptID = " & Bsp.Utility.Quote(beEmpAddition.AddDeptID.Value))
                    strSQL.AppendLine("And AddOrganID = " & Bsp.Utility.Quote(beEmpAddition.AddOrganID.Value) & ";")
                End If
                'EmpAdditionDetail
                bsEmpAdditionDetail.Insert(beEmpAdditionDetail, tran)


                'EmpAdditionWait
                strSQL.AppendLine("Insert into EmpAdditionLog (ValidDate,Reason,CompID,EmpID,AddCompID,AddDeptID,AddOrganID,AddFlowOrganID,AddDeptName,AddOrganName,AddFlowOrganName," & _
                                                                    "DeptID,OrganID,DeptName,OrganName,RankID,TitleName,PositionID,PositionName,WorkTypeID,WorkTypeName," & _
                                                                    "HoldingRankID,HoldingTitleName,Remark,FileNo,IsBoss,IsSecBoss,IsGroupBoss,IsSecGroupBoss,BossType," & _
                                                                    "CreateDate,CreateComp,CreateID,LastChgDate,LastChgComp,LastChgID)")
                strSQL.AppendLine("Select " & Bsp.Utility.Quote(beEmpAdditionLog.ValidDate.Value.ToShortDateString))
                strSQL.AppendLine("," & Bsp.Utility.Quote(beEmpAdditionLog.Reason.Value))
                strSQL.AppendLine("," & Bsp.Utility.Quote(beEmpAdditionLog.CompID.Value))
                strSQL.AppendLine(",P.EmpID")
                strSQL.AppendLine("," & Bsp.Utility.Quote(beEmpAdditionLog.AddCompID.Value))
                strSQL.AppendLine("," & Bsp.Utility.Quote(beEmpAdditionLog.AddDeptID.Value))
                strSQL.AppendLine("," & Bsp.Utility.Quote(beEmpAdditionLog.AddOrganID.Value))
                strSQL.AppendLine("," & Bsp.Utility.Quote(beEmpAdditionLog.AddFlowOrganID.Value))
                strSQL.AppendLine(",isnull(OAD.OrganName,'') as AddDeptName,isnull(OAO.OrganName,'') as AddOrganName,isnull(OAF.OrganName,'') as AddFlowOrganName")
                strSQL.AppendLine(",P.DeptID, P.OrganID, isnull(OD.OrganName,'')  as DeptName, isnull(OO.OrganName,'') as OrganName")
                strSQL.AppendLine(",P.RankID, isnull(T.TitleName,'') as TitleName")
                strSQL.AppendLine(",isnull(EP.PositionID,'') as PositionID, isnull(Po.Remark,'') as PositionName")
                strSQL.AppendLine(",isnull(EW.WorkTypeID,'') as WorkTypeID, isnull(W.Remark,'') as WorkTypeName")
                strSQL.AppendLine(",P.HoldingRankID, isnull(PT.PublicTitleName,'') as PublicTitleName")
                strSQL.AppendLine("," & Bsp.Utility.Quote(beEmpAdditionLog.Remark.Value))
                strSQL.AppendLine("," & Bsp.Utility.Quote(beEmpAdditionLog.FileNo.Value))
                strSQL.AppendLine("," & Bsp.Utility.Quote(beEmpAdditionLog.IsBoss.Value))
                strSQL.AppendLine("," & Bsp.Utility.Quote(beEmpAdditionLog.IsSecBoss.Value))
                strSQL.AppendLine("," & Bsp.Utility.Quote(beEmpAdditionLog.IsGroupBoss.Value))
                strSQL.AppendLine("," & Bsp.Utility.Quote(beEmpAdditionLog.IsSecGroupBoss.Value))
                strSQL.AppendLine("," & Bsp.Utility.Quote(beEmpAdditionLog.BossType.Value))
                strSQL.AppendLine("," & Bsp.Utility.Quote(Format(beEmpAdditionLog.CreateDate.Value, "yyyy/MM/dd HH:mm:ss")))
                strSQL.AppendLine("," & Bsp.Utility.Quote(beEmpAdditionLog.CreateComp.Value))
                strSQL.AppendLine("," & Bsp.Utility.Quote(beEmpAdditionLog.CreateID.Value))
                strSQL.AppendLine("," & Bsp.Utility.Quote(Format(beEmpAdditionLog.LastChgDate.Value, "yyyy/MM/dd HH:mm:ss")))
                strSQL.AppendLine("," & Bsp.Utility.Quote(beEmpAdditionLog.LastChgComp.Value))
                strSQL.AppendLine("," & Bsp.Utility.Quote(beEmpAdditionLog.LastChgID.Value))
                strSQL.AppendLine("From Personal P")
                strSQL.AppendLine("Left join Organization OAD on OAD.CompID = " & Bsp.Utility.Quote(beEmpAdditionLog.AddCompID.Value) & " And OAD.OrganID = " & Bsp.Utility.Quote(beEmpAdditionLog.AddDeptID.Value))
                strSQL.AppendLine("Left join Organization OAO on OAO.CompID = " & Bsp.Utility.Quote(beEmpAdditionLog.AddCompID.Value) & " And OAO.OrganID = " & Bsp.Utility.Quote(beEmpAdditionLog.AddOrganID.Value))
                strSQL.AppendLine("Left join OrganizationFlow OAF on OAF.OrganID = " & Bsp.Utility.Quote(beEmpAdditionLog.AddFlowOrganID.Value))
                strSQL.AppendLine("Left join Organization OD on OD.CompID = P.CompID and  OD.OrganID = P.DeptID")
                strSQL.AppendLine("Left join Organization OO on OO.CompID = P.CompID and  OO.OrganID = P.OrganID")
                strSQL.AppendLine("Left join Title T on T.CompID = P.CompID and T.RankID = P.RankID and T.TitleID = P.TitleID")
                strSQL.AppendLine("Left join EmpPosition EP on P.CompID = EP.CompID and P.EmpID = EP.EmpID and  EP.PrincipalFlag = '1'")
                strSQL.AppendLine("Left join Position Po on P.CompID = Po.CompID and EP.PositionID = Po.PositionID")
                strSQL.AppendLine("Left join EmpWorkType EW on P.CompID = EW.CompID and P.EmpID = EW.EmpID and  EW.PrincipalFlag = '1'")
                strSQL.AppendLine("Left join WorkType W on P.CompID = W.CompID and EW.WorkTypeID = W.WorkTypeID")
                strSQL.AppendLine("Left join PublicTitle PT on P.PublicTitleID = PT.PublicTitleID")
                strSQL.AppendLine("Where P.CompID = " & Bsp.Utility.Quote(beEmpAdditionLog.CompID.Value))
                strSQL.AppendLine("And P.EmpID = " & Bsp.Utility.Quote(beEmpAdditionLog.EmpID.Value))

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
#End Region
#Region "Update"
    Public Function UpdateEmpAddition(ByVal beEmpAddition As beEmpAddition.Row, ByVal beEmpAdditionDetail As beEmpAdditionDetail.Row, ByVal beEmpAdditionLog As beEmpAdditionLog.Row) As Boolean
        Dim bsEmpAddition As New beEmpAddition.Service()
        Dim bsEmpAdditionDetail As New beEmpAdditionDetail.Service()
        Dim bsEmpAdditionLog As New beEmpAdditionLog.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                'EmpAddition
                bsEmpAddition.Update(beEmpAddition, tran)

                'EmpAdditionDetail
                bsEmpAdditionDetail.Update(beEmpAdditionDetail, tran)

                'EmpAdditionLog
                bsEmpAdditionLog.Update(beEmpAdditionLog, tran)

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
#End Region
#Region "Del"
    Public Function DeleteEmpAddition(ByVal beEmpAddition As beEmpAddition.Row, ByVal beEmpAdditionDetail As beEmpAdditionDetail.Row, ByVal beEmpAdditionLog As beEmpAdditionLog.Row) As Boolean
        Dim bsEmpAddition As New beEmpAddition.Service()
        Dim bsEmpAdditionDetail As New beEmpAdditionDetail.Service()
        Dim bsEmpAdditionLog As New beEmpAdditionLog.Service()
        Dim strSQL As New StringBuilder()

        'EmpAddition
        strSQL.AppendLine("Delete From EmpAddition")
        strSQL.AppendLine("Where ValidDate = " & Bsp.Utility.Quote(Format(beEmpAddition.ValidDate.Value, "yyyy/MM/dd HH:mm:ss")))
        strSQL.AppendLine("And CompID = " & Bsp.Utility.Quote(beEmpAddition.CompID.Value))
        strSQL.AppendLine("And EmpID = " & Bsp.Utility.Quote(beEmpAddition.EmpID.Value))
        strSQL.AppendLine("And AddCompID = " & Bsp.Utility.Quote(beEmpAddition.AddCompID.Value))
        strSQL.AppendLine("And AddDeptID = " & Bsp.Utility.Quote(beEmpAddition.AddDeptID.Value))
        strSQL.AppendLine("And AddOrganID = " & Bsp.Utility.Quote(beEmpAddition.AddOrganID.Value) & ";")

        'EmpAdditionDetail
        strSQL.AppendLine("Delete From EmpAdditionDetail")
        strSQL.AppendLine("Where ValidDate = " & Bsp.Utility.Quote(Format(beEmpAdditionDetail.ValidDate.Value, "yyyy/MM/dd HH:mm:ss")))
        strSQL.AppendLine("And CompID = " & Bsp.Utility.Quote(beEmpAdditionDetail.CompID.Value))
        strSQL.AppendLine("And EmpID = " & Bsp.Utility.Quote(beEmpAdditionDetail.EmpID.Value))
        strSQL.AppendLine("And AddCompID = " & Bsp.Utility.Quote(beEmpAdditionDetail.AddCompID.Value))
        strSQL.AppendLine("And AddDeptID = " & Bsp.Utility.Quote(beEmpAdditionDetail.AddDeptID.Value))
        strSQL.AppendLine("And AddOrganID = " & Bsp.Utility.Quote(beEmpAdditionDetail.AddOrganID.Value) & ";")

        'EmpAdditionLog
        strSQL.AppendLine("Delete From EmpAdditionLog")
        strSQL.AppendLine("Where ValidDate = " & Bsp.Utility.Quote(Format(beEmpAdditionLog.ValidDate.Value, "yyyy/MM/dd HH:mm:ss")))
        strSQL.AppendLine("And CompID = " & Bsp.Utility.Quote(beEmpAdditionLog.CompID.Value))
        strSQL.AppendLine("And EmpID = " & Bsp.Utility.Quote(beEmpAdditionLog.EmpID.Value))
        strSQL.AppendLine("And AddCompID = " & Bsp.Utility.Quote(beEmpAdditionLog.AddCompID.Value))
        strSQL.AppendLine("And AddDeptID = " & Bsp.Utility.Quote(beEmpAdditionLog.AddDeptID.Value))
        strSQL.AppendLine("And AddOrganID = " & Bsp.Utility.Quote(beEmpAdditionLog.AddOrganID.Value) & ";")

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                'EmpAddition
                bsEmpAddition.DeleteRowByPrimaryKey(beEmpAddition, tran)
                'EmpAdditionDetail
                bsEmpAdditionDetail.DeleteRowByPrimaryKey(beEmpAdditionDetail, tran)
                'EmpAdditionLog
                bsEmpAdditionLog.DeleteRowByPrimaryKey(beEmpAdditionLog, tran)
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
    
    
End Class
