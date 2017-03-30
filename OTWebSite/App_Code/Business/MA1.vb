'****************************************************
'功能說明：綜合查詢
'建立人員：BeatriceCheng
'建立日期：2015.06.23
'****************************************************
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.IO
Imports System

Public Class MA1

#Region "MA1000 綜合查詢"
#Region "查詢"
    Public Function CompositeQuery(ByVal QueryFlag As Boolean, ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        Dim dt As DataTable
        Dim strValueB As String = ""
        Dim strValueE As String = ""

        If ht("CommGrade").ToString() <> "" Or ht("GradeYear").ToString() <> "" Or ht("LastGrade1").ToString() <> "" Or ht("LastGrade2").ToString() <> "" Or ht("LastGrade3").ToString() <> "" Or ht("LastGrade4").ToString() <> "" Then
            strSQL.AppendLine(Bsp.Utility.getAppSetting("eHRMSDBDES"))
        End If

        strSQL.AppendLine("Select distinct P.CompID")
        strSQL.AppendLine(", IsNull(C1.CompName, '') CompName")
        strSQL.AppendLine(", P.EmpID")
        'strSQL.AppendLine(", P.EmpIDOld")
        strSQL.AppendLine(", P.NameN")
        strSQL.AppendLine(", P.IDNo")
        strSQL.AppendLine(", P.DeptID")
        strSQL.AppendLine(", IsNull(O1.OrganName, '') DeptName")
        strSQL.AppendLine(", P.OrganID")
        strSQL.AppendLine(", IsNull(O2.OrganName, '') OrganName")
        If Bsp.Utility.IsStringNull(ht("YearSalaryB")) <> "" Or Bsp.Utility.IsStringNull(ht("YearSalaryE")) <> "" Or Bsp.Utility.IsStringNull(ht("MonthSalaryB")) <> "" Or Bsp.Utility.IsStringNull(ht("MonthSalaryE")) <> "" Then
            strSQL.AppendLine(", IsNull(S1.Amount, '') Amount")

            If Bsp.Utility.IsStringNull(ht("MonthSalaryB")) <> "" Or Bsp.Utility.IsStringNull(ht("MonthSalaryE")) <> "" Then
                strSQL.AppendLine(", Case When S2.MonthOfAnnualPay = '12' Then '12' Else '14' End AS MonthOfAnnualPay")
            End If
        End If
        strSQL.AppendLine(", IsNull(C2.CompName, '') As LastChgComp")
        strSQL.AppendLine(", IsNull(P1.NameN, '') As LastChgID")
        strSQL.AppendLine(", LastChgDate = Case When Convert(Char(10), P.LastChgDate, 111) = '1900/01/01' Then '' Else Convert(Varchar, P.LastChgDate, 120) End")
        strSQL.AppendLine("From Personal P")
        strSQL.AppendLine("Left Join Company C1 On C1.CompID = P.CompID")
        strSQL.AppendLine("Left Join Company C2 On C2.CompID = P.LastChgComp")

        If Bsp.Utility.IsStringNull(ht("CategoryID")) <> "" Or Bsp.Utility.IsStringNull(ht("LicenseName")) <> "" Then
            strSQL.AppendLine("Left Join Certification C3 On C3.IDNo = P.IDNo")
        End If

        If Bsp.Utility.IsStringNull(ht("RegCityCode")) <> "" Or Bsp.Utility.IsStringNull(ht("RegAddr")) <> "" Or Bsp.Utility.IsStringNull(ht("CommCityCode")) <> "" Or Bsp.Utility.IsStringNull(ht("CommAddr")) <> "" Then
            strSQL.AppendLine("Left Join Communication C4 On C4.IDNo = P.IDNo")
        End If

        If Bsp.Utility.IsStringNull(ht("SchoolType")) <> "" Or Bsp.Utility.IsStringNull(ht("SchoolID")) <> "" Or Bsp.Utility.IsStringNull(ht("Depart")) <> "" Then
            strSQL.AppendLine("Left Join Education E1 On E1.IDNo = P.IDNo")
        End If

        If Bsp.Utility.IsStringNull(ht("LastBeginDateB")) <> "" Or Bsp.Utility.IsStringNull(ht("LastBeginDateE")) <> "" Or Bsp.Utility.IsStringNull(ht("LastEndDateB")) <> "" Or Bsp.Utility.IsStringNull(ht("LastEndDateE")) <> "" _
            Or Bsp.Utility.IsStringNull(ht("LastIndustryType")) <> "" Or Bsp.Utility.IsStringNull(ht("LastCompany")) <> "" Or Bsp.Utility.IsStringNull(ht("LastDept")) <> "" Or Bsp.Utility.IsStringNull(ht("LastTitle")) <> "" _
            Or Bsp.Utility.IsStringNull(ht("LastWorkType")) <> "" Or Bsp.Utility.IsStringNull(ht("LastWorkYearB")) <> "" Or Bsp.Utility.IsStringNull(ht("LastWorkYearE")) <> "" Or Bsp.Utility.IsStringNull(ht("Profession")) <> "" Then

            strSQL.AppendLine("Left Join Experience E2 On E2.IDNo = P.IDNo")
        End If


        If Bsp.Utility.IsStringNull(ht("Position1")) <> "" Or Bsp.Utility.IsStringNull(ht("Position2")) <> "" Then
            strSQL.AppendLine("Left Join EmpPosition E3 On E3.CompID = P.CompID And E3.EmpID = P.EmpID")

            If Bsp.Utility.IsStringNull(ht("Position1")) <> "" And Bsp.Utility.IsStringNull(ht("Position2")) <> "" Then
                strSQL.AppendLine("Left Join EmpPosition E4 On E4.CompID = P.CompID And E4.EmpID = P.EmpID")
            End If
        End If

        If Bsp.Utility.IsStringNull(ht("WorkType1")) <> "" Or Bsp.Utility.IsStringNull(ht("WorkType2")) <> "" Then
            strSQL.AppendLine("Left Join EmpWorkType E5 On E5.CompID = P.CompID And E5.EmpID = P.EmpID")

            If Bsp.Utility.IsStringNull(ht("WorkType1")) <> "" And Bsp.Utility.IsStringNull(ht("WorkType2")) <> "" Then
                strSQL.AppendLine("Left Join EmpWorkType E6 On E6.CompID = P.CompID And E6.EmpID = P.EmpID")
            End If
        End If

        If Bsp.Utility.IsStringNull(ht("EmpAddition")) <> "" Or Bsp.Utility.IsStringNull(ht("EmpAdditionComp")) <> "" Or Bsp.Utility.IsStringNull(ht("EmpAdditionDept")) <> "" _
            Or Bsp.Utility.IsStringNull(ht("EmpAdditionReason")) <> "" Or Bsp.Utility.IsStringNull(ht("EmpAdditionDateB")) <> "" Or Bsp.Utility.IsStringNull(ht("EmpAdditionDateE")) <> "" Then

            If Bsp.Utility.IsStringNull(ht("EmpAddition")) = "1" Then
                strSQL.AppendLine("Left Join EmpAdditionDetail E7 On E7.CompID = P.CompID And E7.EmpID = P.EmpID")
            ElseIf Bsp.Utility.IsStringNull(ht("EmpAddition")) = "2" Then
                strSQL.AppendLine("Left Join EmpAddition E8 On E8.CompID = P.CompID And E8.EmpID = P.EmpID")
            End If
        End If

        If Bsp.Utility.IsStringNull(ht("FamilyOccupation")) <> "" Or Bsp.Utility.IsStringNull(ht("FamilyIndustryType")) <> "" Or Bsp.Utility.IsStringNull(ht("FamilyCompany")) <> "" Then
            strSQL.AppendLine("Left Join Family F1 On F1.IDNo = P.IDNo")
        End If

        If Bsp.Utility.IsStringNull(ht("ModifyDateB")) <> "" Or Bsp.Utility.IsStringNull(ht("ModifyDateE")) <> "" Or Bsp.Utility.IsStringNull(ht("Reason")) <> "" _
            Or Bsp.Utility.IsStringNull(ht("LogDept")) <> "" Or Bsp.Utility.IsStringNull(ht("LogOrgan")) <> "" Or Bsp.Utility.IsStringNull(ht("LogPosition")) <> "" _
            Or Bsp.Utility.IsStringNull(ht("LogWorkType")) <> "" Or Bsp.Utility.IsStringNull(ht("LogRank")) <> "" Or Bsp.Utility.IsStringNull(ht("LogTitle")) <> "" Then

            strSQL.AppendLine("Left Join EmployeeLog L1 On L1.IDNo = P.IDNo")
        End If

        strSQL.AppendLine("Left Join Organization O1 On O1.CompID = P.CompID And O1.OrganID = P.DeptID")
        strSQL.AppendLine("Left Join Organization O2 On O2.CompID = P.CompID And O2.OrganID = P.OrganID")
        strSQL.AppendLine("Left Join Personal P1 On P1.CompID = P.LastChgComp And P1.EmpID = P.LastChgID")

        If Bsp.Utility.IsStringNull(ht("AboriginalFlag")) <> "" Or Bsp.Utility.IsStringNull(ht("AboriginalTribe")) <> "" Or Bsp.Utility.IsStringNull(ht("WTID")) <> "" Or Bsp.Utility.IsStringNull(ht("OfficeLoginFlag")) <> "" Then
            strSQL.AppendLine("Left Join PersonalOther P2 On P2.CompID = P.CompID And P2.EmpID = P.EmpID")
        End If

        If Bsp.Utility.IsStringNull(ht("YearSalaryB")) <> "" Or Bsp.Utility.IsStringNull(ht("YearSalaryE")) <> "" Or Bsp.Utility.IsStringNull(ht("MonthSalaryB")) <> "" Or Bsp.Utility.IsStringNull(ht("MonthSalaryE")) <> "" Then
            strSQL.AppendLine("Left Join Salary S1 On S1.CompID = P.CompID And S1.EmpID = P.EmpID And S1.SalaryID = 'A000'")

            If Bsp.Utility.IsStringNull(ht("MonthSalaryB")) <> "" Or Bsp.Utility.IsStringNull(ht("MonthSalaryE")) <> "" Then
                strSQL.AppendLine("Left Join SalaryData S2 On S2.CompID = S1.CompID And S2.EmpID = S1.EmpID")
            End If
        End If

        If Bsp.Utility.IsStringNull(ht("EmpSeniorityB")) <> "" Or Bsp.Utility.IsStringNull(ht("EmpSeniorityE")) <> "" Or Bsp.Utility.IsStringNull(ht("SinopacEmpSeniorityB")) <> "" Or Bsp.Utility.IsStringNull(ht("SinopacEmpSeniorityE")) <> "" Then
            strSQL.AppendLine("Left Join EmpSenComp S3 On S3.CompID = P.CompID And S3.EmpID = P.EmpID")
        End If

        If Bsp.Utility.IsStringNull(ht("OrgRange")) <> "" Or Bsp.Utility.IsStringNull(ht("EmpSenOrgTypeB")) <> "" Or Bsp.Utility.IsStringNull(ht("EmpSenOrgTypeE")) <> "" Or Bsp.Utility.IsStringNull(ht("EmpConSenOrgTypeB")) <> "" Or Bsp.Utility.IsStringNull(ht("EmpConSenOrgTypeE")) <> "" Then
            strSQL.AppendLine("Left Join EmpSenOrgType S4 On S4.CompID = P.CompID And S4.EmpID = P.EmpID")
        End If

        If Bsp.Utility.IsStringNull(ht("EmpSenRankB")) <> "" Or Bsp.Utility.IsStringNull(ht("EmpSenRankE")) <> "" Then
            strSQL.AppendLine("Left Join EmpSenRank S5 On S5.CompID = P.CompID And S5.EmpID = P.EmpID")
        End If

        If Bsp.Utility.IsStringNull(ht("PositionRange")) <> "" Or Bsp.Utility.IsStringNull(ht("EmpSenPositionB")) <> "" Or Bsp.Utility.IsStringNull(ht("EmpSenPositionE")) <> "" Or Bsp.Utility.IsStringNull(ht("EmpConSenPositionB")) <> "" Or Bsp.Utility.IsStringNull(ht("EmpConSenPositionE")) <> "" Then
            strSQL.AppendLine("Left Join EmpSenPosition S6 On S6.CompID = P.CompID And S6.EmpID = P.EmpID")
        End If

        If Bsp.Utility.IsStringNull(ht("WorkTypeRange")) <> "" Or Bsp.Utility.IsStringNull(ht("EmpSenWorkTypeB")) <> "" Or Bsp.Utility.IsStringNull(ht("EmpSenWorkTypeE")) <> "" Or Bsp.Utility.IsStringNull(ht("EmpConSenWorkTypeB")) <> "" Or Bsp.Utility.IsStringNull(ht("EmpConSenWorkTypeE")) <> "" Then
            strSQL.AppendLine("Left Join EmpSenWorkType S7 On S7.CompID = P.CompID And S7.EmpID = P.EmpID")
        End If

        If QueryFlag = True Then
            Select Case (Bsp.Utility.IsStringNull(ht("CommGrade")))
                Case "1" '1-近五年三甲
                    strSQL.AppendLine("Join (")
                    strSQL.AppendLine("Select P.CompID, P.EmpID From Personal P")
                    strSQL.AppendLine("Join EmpGrade E on E.CompID = P.CompID and E.EmpID = P.EmpID")
                    strSQL.AppendLine("And E.GradeYear >= (Year(GetDate()) - 4)")
                    strSQL.AppendLine("Where Convert(VarChar(10), DecryptByKey(E.Grade)) = '2'" & IIf(ht("CompID").ToString() = "0", "", " And P.CompID = " & Bsp.Utility.Quote(ht("CompID").ToString())))
                    strSQL.AppendLine("Group By P.CompID, P.EmpID")
                    strSQL.AppendLine("Having Count(P.EmpID) >= 3")
                    strSQL.AppendLine(") G1 On G1.CompID = P.CompID And G1.EmpID = P.EmpID")
                Case "2" '2-近三年兩乙
                    strSQL.AppendLine("Join (")
                    strSQL.AppendLine("Select P.CompID, P.EmpID From Personal P")
                    strSQL.AppendLine("Join EmpGrade E on E.CompID = P.CompID and E.EmpID = P.EmpID")
                    strSQL.AppendLine("And E.GradeYear >= (Year(GetDate()) - 2)")
                    strSQL.AppendLine("Where Convert(VarChar(10), DecryptByKey(E.Grade)) = '3'" & IIf(ht("CompID").ToString() = "0", "", " And P.CompID = " & Bsp.Utility.Quote(ht("CompID").ToString())))
                    strSQL.AppendLine("Group By P.CompID, P.EmpID")
                    strSQL.AppendLine("Having Count(P.EmpID) >= 2")
                    strSQL.AppendLine(") G1 On G1.CompID = P.CompID And G1.EmpID = P.EmpID")
                Case "3" '3-近兩年優
                    strSQL.AppendLine("Join (")
                    strSQL.AppendLine("Select P.CompID, P.EmpID From Personal P")
                    strSQL.AppendLine("Join EmpGrade E on E.CompID = P.CompID and E.EmpID = P.EmpID")
                    strSQL.AppendLine("And E.GradeYear >= (Year(GetDate()) - 1)")
                    strSQL.AppendLine("Where Convert(VarChar(10), DecryptByKey(E.Grade)) = '1'" & IIf(ht("CompID").ToString() = "0", "", " And P.CompID = " & Bsp.Utility.Quote(ht("CompID").ToString())))
                    strSQL.AppendLine("Group By P.CompID, P.EmpID")
                    strSQL.AppendLine("Having Count(P.EmpID) >= 2")
                    strSQL.AppendLine(") G1 On G1.CompID = P.CompID And G1.EmpID = P.EmpID")
                Case "4" '4-近兩年乙
                    strSQL.AppendLine("Join (")
                    strSQL.AppendLine("Select P.CompID, P.EmpID From Personal P")
                    strSQL.AppendLine("Join EmpGrade E on E.CompID = P.CompID and E.EmpID = P.EmpID")
                    strSQL.AppendLine("And E.GradeYear >= (Year(GetDate()) - 1)")
                    strSQL.AppendLine("Where Convert(VarChar(10), DecryptByKey(E.Grade)) = '3'" & IIf(ht("CompID").ToString() = "0", "", " And P.CompID = " & Bsp.Utility.Quote(ht("CompID").ToString())))
                    strSQL.AppendLine("Group By P.CompID, P.EmpID")
                    strSQL.AppendLine("Having Count(P.EmpID) >= 2")
                    strSQL.AppendLine(") G1 On G1.CompID = P.CompID And G1.EmpID = P.EmpID")
                Case "5" '5-近三年優
                    strSQL.AppendLine("Join (")
                    strSQL.AppendLine("Select P.CompID, P.EmpID From Personal P")
                    strSQL.AppendLine("Join EmpGrade E on E.CompID = P.CompID and E.EmpID = P.EmpID")
                    strSQL.AppendLine("And E.GradeYear >= (Year(GetDate()) - 2)")
                    strSQL.AppendLine("Where Convert(VarChar(10), DecryptByKey(E.Grade)) = '1'" & IIf(ht("CompID").ToString() = "0", "", " And P.CompID = " & Bsp.Utility.Quote(ht("CompID").ToString())))
                    strSQL.AppendLine("Group By P.CompID, P.EmpID")
                    strSQL.AppendLine("Having Count(P.EmpID) >= 3")
                    strSQL.AppendLine(") G1 On G1.CompID = P.CompID And G1.EmpID = P.EmpID")
            End Select

            If Bsp.Utility.IsStringNull(ht("GradeYear")) <> "" Then
                strSQL.AppendLine("Join (")
                strSQL.AppendLine("Select P.CompID, P.EmpID From Personal P")
                strSQL.AppendLine("Join EmpGrade E on E.CompID = P.CompID and E.EmpID = P.EmpID")
                strSQL.AppendLine("And E.GradeYear = " & Bsp.Utility.Quote(ht("GradeYear").ToString()))
                strSQL.AppendLine("Where 1=1" & IIf(ht("CompID").ToString() = "0", "", " And P.CompID = " & Bsp.Utility.Quote(ht("CompID").ToString())))
                If ht("Grade").ToString() <> "ALL" Then
                    strSQL.AppendLine("And Convert(VarChar(10), DecryptByKey(E.Grade)) = " & Bsp.Utility.Quote(ht("Grade").ToString()))
                End If
                strSQL.AppendLine(") G2 On G2.CompID = P.CompID And G2.EmpID = P.EmpID")
            End If

            If Bsp.Utility.IsStringNull(ht("LastGrade1")) <> "" Then
                strSQL.AppendLine("Join (")
                strSQL.AppendLine("Select P.CompID, P.EmpID From Personal P")
                strSQL.AppendLine("Join EmpGrade E on E.CompID = P.CompID and E.EmpID = P.EmpID")
                strSQL.AppendLine("And E.GradeYear = (Year(GetDate()) - 1)")
                strSQL.AppendLine("Where 1=1" & IIf(ht("CompID").ToString() = "0", "", " And P.CompID = " & Bsp.Utility.Quote(ht("CompID").ToString())))
                If ht("LastGrade1").ToString() <> "ALL" Then
                    strSQL.AppendLine("And Convert(VarChar(10), DecryptByKey(E.Grade)) = " & Bsp.Utility.Quote(ht("LastGrade1").ToString()))
                End If
                strSQL.AppendLine(") G3 On G3.CompID = P.CompID And G3.EmpID = P.EmpID")
            End If

            If Bsp.Utility.IsStringNull(ht("LastGrade2")) <> "" Then
                strSQL.AppendLine("Join (")
                strSQL.AppendLine("Select P.CompID, P.EmpID From Personal P")
                strSQL.AppendLine("Join EmpGrade E on E.CompID = P.CompID and E.EmpID = P.EmpID")
                strSQL.AppendLine("And E.GradeYear = (Year(GetDate()) - 2)")
                strSQL.AppendLine("Where 1=1" & IIf(ht("CompID").ToString() = "0", "", " And P.CompID = " & Bsp.Utility.Quote(ht("CompID").ToString())))
                If ht("LastGrade2").ToString() <> "ALL" Then
                    strSQL.AppendLine("And Convert(VarChar(10), DecryptByKey(E.Grade)) = " & Bsp.Utility.Quote(ht("LastGrade2").ToString()))
                End If
                strSQL.AppendLine(") G4 On G4.CompID = P.CompID And G4.EmpID = P.EmpID")
            End If

            If Bsp.Utility.IsStringNull(ht("LastGrade3")) <> "" Then
                strSQL.AppendLine("Join (")
                strSQL.AppendLine("Select P.CompID, P.EmpID From Personal P")
                strSQL.AppendLine("Join EmpGrade E on E.CompID = P.CompID and E.EmpID = P.EmpID")
                strSQL.AppendLine("And E.GradeYear = (Year(GetDate()) - 3)")
                strSQL.AppendLine("Where 1=1" & IIf(ht("CompID").ToString() = "0", "", " And P.CompID = " & Bsp.Utility.Quote(ht("CompID").ToString())))
                If ht("LastGrade3").ToString() <> "ALL" Then
                    strSQL.AppendLine("And Convert(VarChar(10), DecryptByKey(E.Grade)) = " & Bsp.Utility.Quote(ht("LastGrade3").ToString()))
                End If
                strSQL.AppendLine(") G5 On G5.CompID = P.CompID And G5.EmpID = P.EmpID")
            End If

            If Bsp.Utility.IsStringNull(ht("LastGrade4")) <> "" Then
                strSQL.AppendLine("Join (")
                strSQL.AppendLine("Select P.CompID, P.EmpID From Personal P")
                strSQL.AppendLine("Join EmpGrade E on E.CompID = P.CompID and E.EmpID = P.EmpID")
                strSQL.AppendLine("And E.GradeYear = (Year(GetDate()) - 4)")
                strSQL.AppendLine("Where 1=1" & IIf(ht("CompID").ToString() = "0", "", " And P.CompID = " & Bsp.Utility.Quote(ht("CompID").ToString())))
                If ht("LastGrade4").ToString() <> "ALL" Then
                    strSQL.AppendLine("And Convert(VarChar(10), DecryptByKey(E.Grade)) = " & Bsp.Utility.Quote(ht("LastGrade4").ToString()))
                End If
                strSQL.AppendLine(") G6 On G6.CompID = P.CompID And G6.EmpID = P.EmpID")
            End If
        End If

        strSQL.AppendLine("Where 1 = 1")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        If ht(strKey).ToString() <> "0" Then
                            strSQL.AppendLine("And P.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        End If
                    Case "EmpID"
                        strSQL.AppendLine("And P.EmpID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "Name"
                        strSQL.AppendLine("And P.NameN like N'%" & ht(strKey).ToString() & "%'")
                    Case "IDNo"
                        strSQL.AppendLine("And P.IDNo = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "EngName"
                        strSQL.AppendLine("And Upper(P.EngName) like Upper('%" & ht(strKey).ToString() & "%')")
                    Case "PassportName"
                        strSQL.AppendLine("And Upper(P.PassportName) like Upper('%" & ht(strKey).ToString() & "%')")
                    Case "Sex"
                        strSQL.AppendLine("And P.Sex = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "BirthDateB"
                        strSQL.AppendLine("And Convert(char(10), P.BirthDate, 111) >= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "BirthDateE"
                        strSQL.AppendLine("And Convert(char(10), P.BirthDate, 111) <= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "AgeB"
                        strSQL.AppendLine("And Case When Convert(Char(10), P.BirthDate, 111) = '1900/01/01' Then 0 Else Floor(DateDiff(Day, P.BirthDate, GETDATE()) / 365) End >= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "AgeE"
                        strSQL.AppendLine("And Case When Convert(Char(10), P.BirthDate, 111) = '1900/01/01' Then 0 Else Floor(DateDiff(Day, P.BirthDate, GETDATE()) / 365) End <= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "Edu"
                        strSQL.AppendLine("And P.EduID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "Marriage"
                        strSQL.AppendLine("And P.Marriage = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "Nation"
                        strSQL.AppendLine("And P.NationID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "IDExpireDateB"
                        strSQL.AppendLine("And Convert(char(10), P.IDExpireDate, 111) >= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "IDExpireDateE"
                        strSQL.AppendLine("And Convert(char(10), P.IDExpireDate, 111) <= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "AboriginalFlag"
                        strSQL.AppendLine("And P2.AboriginalFlag = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "AboriginalTribe"
                        strSQL.AppendLine("And P2.AboriginalTribe = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "LocalHireFlag"
                        strSQL.AppendLine("And P.LocalHireFlag = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "PassExamFlag"
                        strSQL.AppendLine("And P.PassExamFlag = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "EmpType"
                        strSQL.AppendLine("And P.EmpType = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "WorkCode"
                        strSQL.AppendLine("And P.WorkStatus = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "ProbMonth"
                        strSQL.AppendLine("And P.ProbMonth = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "ProbDateB"
                        strSQL.AppendLine("And Convert(char(10), P.ProbDate, 111) >= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "ProbDateE"
                        strSQL.AppendLine("And Convert(char(10), P.ProbDate, 111) <= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "EmpDateB"
                        strSQL.AppendLine("And Convert(char(10), P.EmpDate, 111) >= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "EmpDateE"
                        strSQL.AppendLine("And Convert(char(10), P.EmpDate, 111) <= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "SinopacEmpDateB"
                        strSQL.AppendLine("And Convert(char(10), P.SinopacEmpDate, 111) >= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "SinopacEmpDateE"
                        strSQL.AppendLine("And Convert(char(10), P.SinopacEmpDate, 111) <= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "QuitDateB"
                        strSQL.AppendLine("And Convert(char(10), P.QuitDate, 111) >= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "QuitDateE"
                        strSQL.AppendLine("And Convert(char(10), P.QuitDate, 111) <= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "SinopacQuitDateB"
                        strSQL.AppendLine("And Convert(char(10), P.SinopacQuitDate, 111) >= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "SinopacQuitDateE"
                        strSQL.AppendLine("And Convert(char(10), P.SinopacQuitDate, 111) <= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "RankBeginDateB"
                        strSQL.AppendLine("And Convert(char(10), P.RankBeginDate, 111) >= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "RankBeginDateE"
                        strSQL.AppendLine("And Convert(char(10), P.RankBeginDate, 111) <= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "WorkSite"
                        strSQL.AppendLine("And P.WorkSiteID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "RankB"
                        If ht("TitleB").ToString() <> "" Then
                            strSQL.AppendLine("And P.RankID + P.TitleID >= " & Bsp.Utility.Quote(ht(strKey).ToString() & ht("TitleB").ToString()))
                        Else
                            strSQL.AppendLine("And P.RankID >= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        End If
                    Case "RankE"
                        If ht("TitleE").ToString() <> "" Then
                            strSQL.AppendLine("And P.RankID + P.TitleID <= " & Bsp.Utility.Quote(ht(strKey).ToString() & ht("TitleE").ToString()))
                        Else
                            strSQL.AppendLine("And P.RankID <= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        End If
                    Case "HoldingRankB"
                        strSQL.AppendLine("And P.HoldingRankID >= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "HoldingRankE"
                        strSQL.AppendLine("And P.HoldingRankID <= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "HoldingTitleB"
                        strSQL.AppendLine("")
                    Case "HoldingTitleE"
                        strSQL.AppendLine("")
                    Case "PublicTitle"
                        strSQL.AppendLine("And P.PublicTitleID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "GroupID"
                        strSQL.AppendLine("And P.GroupID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "OrgType"
                        strSQL.AppendLine("And O1.OrgType = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "DeptID"
                        strSQL.AppendLine("And P.DeptID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "OrganID"
                        strSQL.AppendLine("And P.OrganID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "Position1"
                        strSQL.AppendLine("And E3.PrincipalFlag = '1'")
                        strSQL.AppendLine("And E3.PositionID in (" & ht(strKey).ToString() & ")")
                    Case "Position2"
                        If ht("Position1").ToString() = "" Then
                            strSQL.AppendLine("And E3.PrincipalFlag = '0'")
                            strSQL.AppendLine("And E3.PositionID in (" & ht(strKey).ToString() & ")")
                        Else
                            strSQL.AppendLine("And E4.PrincipalFlag = '0'")
                            strSQL.AppendLine("And E4.PositionID in (" & ht(strKey).ToString() & ")")
                        End If
                    Case "WorkType1"
                        strSQL.AppendLine("And E5.PrincipalFlag = '1'")
                        strSQL.AppendLine("And E5.WorkTypeID in (" & ht(strKey).ToString() & ")")
                    Case "WorkType2"
                        If ht("WorkType1").ToString() = "" Then
                            strSQL.AppendLine("And E5.PrincipalFlag = '0'")
                            strSQL.AppendLine("And E5.WorkTypeID in (" & ht(strKey).ToString() & ")")
                        Else
                            strSQL.AppendLine("And E6.PrincipalFlag = '0'")
                            strSQL.AppendLine("And E6.WorkTypeID in (" & ht(strKey).ToString() & ")")
                        End If
                    Case "WTID"
                        strSQL.AppendLine("And P2.WTID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "OfficeLoginFlag"
                        strSQL.AppendLine("And P2.OfficeLoginFlag = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "EmpSeniorityB"
                        strSQL.AppendLine("And S3.TotSen >= " & ht(strKey).ToString())
                    Case "EmpSeniorityE"
                        strSQL.AppendLine("And S3.TotSen <= " & ht(strKey).ToString())
                    Case "SinopacEmpSeniorityB"
                        strSQL.AppendLine("And S3.TotSen_SPHOLD >= " & ht(strKey).ToString())
                    Case "SinopacEmpSeniorityE"
                        strSQL.AppendLine("And S3.TotSen_SPHOLD <= " & ht(strKey).ToString())
                    Case "EmpSenOrgTypeB"
                        strSQL.AppendLine("And S4.TotSen >= " & ht(strKey).ToString())
                    Case "EmpSenOrgTypeE"
                        strSQL.AppendLine("And S4.TotSen <= " & ht(strKey).ToString())
                    Case "EmpConSenOrgTypeB"
                        strSQL.AppendLine("And S4.ConSen >= " & ht(strKey).ToString())
                    Case "EmpConSenOrgTypeE"
                        strSQL.AppendLine("And S4.ConSen <= " & ht(strKey).ToString())
                    Case "EmpSenOrgTypeFlowB"
                        strSQL.AppendLine("")
                    Case "EmpSenOrgTypeFlowE"
                        strSQL.AppendLine("")
                    Case "EmpSenRankB"
                        strSQL.AppendLine("And S5.TotSen >= " & ht(strKey).ToString())
                    Case "EmpSenRankE"
                        strSQL.AppendLine("And S5.TotSen <= " & ht(strKey).ToString())
                    Case "EmpSenPositionB"
                        strSQL.AppendLine("And S6.TotSen >= " & ht(strKey).ToString())
                    Case "EmpSenPositionE"
                        strSQL.AppendLine("And S6.TotSen <= " & ht(strKey).ToString())
                    Case "EmpConSenPositionB"
                        strSQL.AppendLine("And S6.ConSen >= " & ht(strKey).ToString())
                    Case "EmpConSenPositionE"
                        strSQL.AppendLine("And S6.ConSen <= " & ht(strKey).ToString())
                    Case "EmpSenWorkTypeB"
                        strSQL.AppendLine("And S7.TotSen >= " & ht(strKey).ToString())
                    Case "EmpSenWorkTypeE"
                        strSQL.AppendLine("And S7.TotSen <= " & ht(strKey).ToString())
                    Case "EmpConSenWorkTypeB"
                        strSQL.AppendLine("And S7.ConSen >= " & ht(strKey).ToString())
                    Case "EmpConSenWorkTypeE"
                        strSQL.AppendLine("And S7.ConSen <= " & ht(strKey).ToString())
                    Case "EmpAdditionComp"
                        If ht("EmpAddition").ToString() = "1" Then
                            strSQL.AppendLine("And E7.AddCompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        ElseIf ht("EmpAddition").ToString() = "2" Then
                            strSQL.AppendLine("And E8.AddCompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        End If
                    Case "EmpAdditionDept"
                        If ht("EmpAddition").ToString() = "1" Then
                            strSQL.AppendLine("And E7.AddDeptID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        ElseIf ht("EmpAddition").ToString() = "2" Then
                            strSQL.AppendLine("And E8.AddDeptID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        End If
                    Case "EmpAdditionReason"
                        If ht("EmpAddition").ToString() = "1" Then
                            strSQL.AppendLine("And E7.Reason = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        ElseIf ht("EmpAddition").ToString() = "2" Then
                            strSQL.AppendLine("And E8.Reason = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        End If
                    Case "EmpAdditionDateB"
                        If ht("EmpAddition").ToString() = "1" Then
                            strSQL.AppendLine("And E7.ValidDate >= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        ElseIf ht("EmpAddition").ToString() = "2" Then
                            strSQL.AppendLine("And E8.ValidDate >= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        End If
                    Case "EmpAdditionDateE"
                        If ht("EmpAddition").ToString() = "1" Then
                            strSQL.AppendLine("And E7.ValidDate <= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        ElseIf ht("EmpAddition").ToString() = "2" Then
                            strSQL.AppendLine("And E8.ValidDate <= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        End If
                    Case "SchoolType"
                        strSQL.AppendLine("And E1.SchoolType = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "SchoolID"
                        strSQL.AppendLine("And E1.SchoolID in (" & ht(strKey).ToString() & ")")
                    Case "Depart"
                        strSQL.AppendLine("And E1.Depart like N'%" & ht(strKey).ToString() & "%'")
                    Case "FamilyOccupation"
                        strSQL.AppendLine("And F1.Occupation like N'%" & ht(strKey).ToString() & "%'")
                    Case "FamilyIndustryType"
                        strSQL.AppendLine("And F1.IndustryType like N'%" & ht(strKey).ToString() & "%'")
                    Case "FamilyCompany"
                        strSQL.AppendLine("And F1.Company like N'%" & ht(strKey).ToString() & "%'")
                    Case "RegCityCode"
                        strSQL.AppendLine("And C4.RegCityCode = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "RegAddrCode"
                        strSQL.AppendLine("And C4.RegAddrCode = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "RegAddr"
                        strSQL.AppendLine("And C4.RegAddr like N'%" & ht(strKey).ToString() & "%'")
                    Case "CommCityCode"
                        strSQL.AppendLine("And C4.CommCityCode = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "CommAddrCode"
                        strSQL.AppendLine("And C4.CommAddrCode = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "CommAddr"
                        strSQL.AppendLine("And C4.CommAddr like N'%" & ht(strKey).ToString() & "%'")
                    Case "LastBeginDateB"
                        strSQL.AppendLine("And Convert(char(10), E2.BeginDate, 111) >= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "LastBeginDateE"
                        strSQL.AppendLine("And Convert(char(10), E2.BeginDate, 111) <= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "LastEndDateB"
                        strSQL.AppendLine("And Convert(char(10), E2.EndDate, 111) >= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "LastEndDateE"
                        strSQL.AppendLine("And Convert(char(10), E2.EndDate, 111) <= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "LastIndustryType"
                        strSQL.AppendLine("And E2.IndustryType = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "LastCompany"
                        strSQL.AppendLine("And E2.Company like N'%" & ht(strKey).ToString() & "%'")
                    Case "LastDept"
                        strSQL.AppendLine("And E2.Department like N'%" & ht(strKey).ToString() & "%'")
                    Case "LastTitle"
                        strSQL.AppendLine("And E2.Title like N'%" & ht(strKey).ToString() & "%'")
                    Case "LastWorkType"
                        strSQL.AppendLine("And E2.WorkType like N'%" & ht(strKey).ToString() & "%'")
                    Case "LastWorkYearB"
                        strSQL.AppendLine("And E2.WorkYear >= " & ht(strKey).ToString())
                    Case "LastWorkYearE"
                        strSQL.AppendLine("And E2.WorkYear <= " & ht(strKey).ToString())
                    Case "Profession"
                        strSQL.AppendLine("And E2.Profession = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "ModifyDateB"
                        strSQL.AppendLine("And Convert(char(10), L1.ModifyDate, 111) >= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "ModifyDateE"
                        strSQL.AppendLine("And Convert(char(10), L1.ModifyDate, 111) <= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "Reason"
                        strSQL.AppendLine("And L1.Reason = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "LogDept"
                        strSQL.AppendLine("And L1.DeptID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "LogOrgan"
                        strSQL.AppendLine("And L1.OrganID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "LogPosition"
                        strSQL.AppendLine("And L1.Position like N'%" & ht(strKey).ToString() & "%'")
                    Case "LogWorkType"
                        strSQL.AppendLine("And L1.WorkType like N'%" & ht(strKey).ToString() & "%'")
                    Case "LogRank"
                        strSQL.AppendLine("And L1.RankID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "LogTitle"
                        strSQL.AppendLine("And L1.TitleID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "OrgRange"
                        strSQL.AppendLine("And S4.OrganID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        If ht("OrgRangeB").ToString() <> "" And ht("OrgRangeE").ToString() <> "" Then
                            strSQL.AppendLine("And ((S4.ValidDateB >= " & Bsp.Utility.Quote(ht("OrgRangeB").ToString()) & " And S4.ValidDateB <= " & Bsp.Utility.Quote(ht("OrgRangeE").ToString()) & ")")
                            strSQL.AppendLine("Or (S4.ValidDateE >= " & Bsp.Utility.Quote(ht("OrgRangeB").ToString()) & " And S4.ValidDateE <= " & Bsp.Utility.Quote(ht("OrgRangeE").ToString()) & ")")
                            strSQL.AppendLine("Or (S4.ValidDateB <= " & Bsp.Utility.Quote(ht("OrgRangeB").ToString()) & " And S4.ValidDateE >= " & Bsp.Utility.Quote(ht("OrgRangeE").ToString()) & "))")
                        End If
                        If ht("OrgRangeB").ToString() <> "" And ht("OrgRangeE").ToString() = "" Then
                            strSQL.AppendLine("And (S4.ValidDateB >= " & Bsp.Utility.Quote(ht("OrgRangeB").ToString()) & " Or S4.ValidDateE >= " & Bsp.Utility.Quote(ht("OrgRangeB").ToString()))
                            strSQL.AppendLine("Or (S4.ValidDateB < " & Bsp.Utility.Quote(ht("OrgRangeB").ToString()) & " And S4.ValidDateE = '1900/01/01'))")
                        End If
                        If ht("OrgRangeB").ToString() = "" And ht("OrgRangeE").ToString() <> "" Then
                            strSQL.AppendLine("And S4.ValidDateB <= " & Bsp.Utility.Quote(ht("OrgRangeE").ToString()))
                        End If
                    Case "PositionRange"
                        strSQL.AppendLine("And S6.PositionID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        If ht("PositionRangeB").ToString() <> "" And ht("PositionRangeE").ToString() <> "" Then
                            strSQL.AppendLine("And ((S6.ValidDateB >= " & Bsp.Utility.Quote(ht("PositionRangeB").ToString()) & " And S6.ValidDateB <= " & Bsp.Utility.Quote(ht("PositionRangeE").ToString()) & ")")
                            strSQL.AppendLine("Or (S6.ValidDateE >= " & Bsp.Utility.Quote(ht("PositionRangeB").ToString()) & " And S6.ValidDateE <= " & Bsp.Utility.Quote(ht("PositionRangeE").ToString()) & ")")
                            strSQL.AppendLine("Or (S6.ValidDateB <= " & Bsp.Utility.Quote(ht("PositionRangeB").ToString()) & " And S6.ValidDateE >= " & Bsp.Utility.Quote(ht("PositionRangeE").ToString()) & "))")
                        End If
                        If ht("PositionRangeB").ToString() <> "" And ht("PositionRangeE").ToString() = "" Then
                            strSQL.AppendLine("And (S6.ValidDateB >= " & Bsp.Utility.Quote(ht("PositionRangeB").ToString()) & " Or S6.ValidDateE >= " & Bsp.Utility.Quote(ht("PositionRangeB").ToString()))
                            strSQL.AppendLine("Or (S6.ValidDateB < " & Bsp.Utility.Quote(ht("PositionRangeB").ToString()) & " And S6.ValidDateE = '1900/01/01'))")
                        End If
                        If ht("PositionRangeB").ToString() = "" And ht("PositionRangeE").ToString() <> "" Then
                            strSQL.AppendLine("And S6.ValidDateB <= " & Bsp.Utility.Quote(ht("PositionRangeE").ToString()))
                        End If
                    Case "WorkTypeRange"
                        strSQL.AppendLine("And S7.WorkTypeID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        If ht("WorkTypeRangeB").ToString() <> "" And ht("WorkTypeRangeE").ToString() <> "" Then
                            strSQL.AppendLine("And ((S7.ValidDateB >= " & Bsp.Utility.Quote(ht("WorkTypeRangeB").ToString()) & " And S7.ValidDateB <= " & Bsp.Utility.Quote(ht("WorkTypeRangeE").ToString()) & ")")
                            strSQL.AppendLine("Or (S7.ValidDateE >= " & Bsp.Utility.Quote(ht("WorkTypeRangeB").ToString()) & " And S7.ValidDateE <= " & Bsp.Utility.Quote(ht("WorkTypeRangeE").ToString()) & ")")
                            strSQL.AppendLine("Or (S7.ValidDateB <= " & Bsp.Utility.Quote(ht("WorkTypeRangeB").ToString()) & " And S7.ValidDateE >= " & Bsp.Utility.Quote(ht("WorkTypeRangeE").ToString()) & "))")
                        End If
                        If ht("WorkTypeRangeB").ToString() <> "" And ht("WorkTypeRangeE").ToString() = "" Then
                            strSQL.AppendLine("And (S7.ValidDateB >= " & Bsp.Utility.Quote(ht("WorkTypeRangeB").ToString()) & " Or S7.ValidDateE >= " & Bsp.Utility.Quote(ht("WorkTypeRangeB").ToString()))
                            strSQL.AppendLine("Or (S7.ValidDateB < " & Bsp.Utility.Quote(ht("WorkTypeRangeB").ToString()) & " And S7.ValidDateE = '1900/01/01'))")
                        End If
                        If ht("WorkTypeRangeB").ToString() = "" And ht("WorkTypeRangeE").ToString() <> "" Then
                            strSQL.AppendLine("And S7.ValidDateB <= " & Bsp.Utility.Quote(ht("WorkTypeRangeE").ToString()))
                        End If
                    Case "CategoryID"
                        strSQL.AppendLine("And C3.CategoryID in (" & ht(strKey).ToString() & ")")
                    Case "LicenseName"
                        strSQL.AppendLine("And C3.LicenseName like N'%" & ht(strKey).ToString() & "%'")
                End Select
            End If
        Next
        strSQL.AppendLine("Order By P.CompID, P.EmpID")
        dt = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)

        If Bsp.Utility.IsStringNull(ht("YearSalaryB")) <> "" Or Bsp.Utility.IsStringNull(ht("YearSalaryE")) <> "" Or Bsp.Utility.IsStringNull(ht("MonthSalaryB")) <> "" Or Bsp.Utility.IsStringNull(ht("MonthSalaryE")) <> "" Then
            If dt.Rows.Count > 0 Then
                dt = DecryptSalary(dt, Bsp.Utility.IsStringNull(ht("YearSalaryB")), Bsp.Utility.IsStringNull(ht("YearSalaryE")), Bsp.Utility.IsStringNull(ht("MonthSalaryB")), Bsp.Utility.IsStringNull(ht("MonthSalaryE")))
            End If
        End If

        Return dt
    End Function
#End Region

#Region "下載查詢"
    Public Function CompositeDownloadQuery(ByVal QueryFlag As Boolean, ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()
        Dim objRegistData As New RegistData()
        Dim dt As New DataTable

        If ht("Grade").ToString() = "Y" Then
            strSQL.AppendLine(Bsp.Utility.getAppSetting("eHRMSDBDES"))
        End If
        strSQL.AppendLine("Select distinct Pr.IDNo")
        strSQL.AppendLine(", WST.Remark AS '任職狀態'")
        strSQL.AppendLine(", ET.CodeCName AS '雇用類別'")
        strSQL.AppendLine(", Pr.EmpID AS '員工編號'")
        strSQL.AppendLine(", Pr.NameN AS '姓名'")
        strSQL.AppendLine(", Pr.RankID AS '職等'")
        strSQL.AppendLine(", IsNull(T.TitleName, '') AS '職稱'")
        strSQL.AppendLine(", Pr.DeptID AS '部門代碼'")
        strSQL.AppendLine(", IsNull(OD.OrganName, '') AS '部門'")
        strSQL.AppendLine(", Pr.OrganID AS '科組課代碼'")
        strSQL.AppendLine(", IsNull(OO.OrganName, '') AS '科組課'")
        strSQL.AppendLine(", Pr.CompID AS '公司代碼'")
        strSQL.AppendLine(", IsNull(C.CompName, '') AS '公司'")
        strSQL.AppendLine(", Pr.GroupID '事業單位代碼'")
        strSQL.AppendLine(", IsNull(OFW.OrganName, '') '事業單位'")
        strSQL.AppendLine(", IsNull(OFW.GroupType, '') '事業類別代碼'")
        strSQL.AppendLine(", IsNull(GT.CodeCName, '') '事業類別'")
        strSQL.AppendLine(", IsNull(EW.WorkTypeID,'') '工作性質代碼'")
        strSQL.AppendLine(", IsNull(WT.Remark, '') '工作性質'")
        strSQL.AppendLine(", IsNull (Stuff ((Select ',' + WorkTypeID From EmpWorkType SEW")
        strSQL.AppendLine("  Where Pr.CompID = SEW.CompID And Pr.EmpID = SEW.EmpID And SEW.PrincipalFlag = 0")
        strSQL.AppendLine("  For Xml Path('')), 1, 1, ''), '') AS '次要工作性質代碼'")
        strSQL.AppendLine(", IsNull (Stuff ((Select ',' + SWT.Remark From EmpWorkType SEW")
        strSQL.AppendLine("  Left Join WorkType SWT On SWT.CompID = SEW.CompID And SWT.WorkTypeID = SEW.WorkTypeID")
        strSQL.AppendLine("  Where Pr.CompID = SEW.CompID And Pr.EmpID = SEW.EmpID And SEW.PrincipalFlag = 0")
        strSQL.AppendLine("  For Xml Path('')), 1, 1, ''), '') AS '次要工作性質'")
        strSQL.AppendLine(", Case When Convert(Char(10), Pr.EmpDate, 111) = '1900/01/01' Then '' Else Convert(Varchar, Pr.EmpDate, 111) End AS '公司到職日'")
        strSQL.AppendLine(", IsNull(ESC.TotSen, 0.00) AS '公司年資'")
        strSQL.AppendLine(", Floor(IsNull(ESC.TotSen, 0)) AS '公司年資層'")
        strSQL.AppendLine(", Case When Convert(Char(10), Pr.SinopacEmpDate, 111) = '1900/01/01' Then '' Else Convert(Varchar, Pr.SinopacEmpDate, 111) End AS '企業團到職日'")
        strSQL.AppendLine(", IsNull(ESC.TotSen_SPHOLD, 0.00) AS '企業團年資'")
        strSQL.AppendLine(", Floor(IsNull(ESC.TotSen_SPHOLD, 0)) AS '企業團年資層'")
        strSQL.AppendLine(", Pr.NotEmpDay AS '中斷年資總天數'")
        strSQL.AppendLine(", Case When Convert(Char(10), Pr.QuitDate, 111) = '1900/01/01' Then '' Else Convert(Varchar, Pr.QuitDate, 111) End AS '公司離職日'")
        strSQL.AppendLine(", Case Pr.Sex When '1' Then '男' When '2' Then '女' End AS '性別'")
        strSQL.AppendLine(", IsNull(ED.EduName, '') AS '學歷'")
        strSQL.AppendLine(", '' AS '最高學歷名稱'")
        strSQL.AppendLine(", '' AS '最高學歷學校'")
        strSQL.AppendLine(", '' AS '最高學歷系所'")
        strSQL.AppendLine(", '' AS '次高學歷名稱'")
        strSQL.AppendLine(", '' AS '次高學歷學校'")
        strSQL.AppendLine(", '' AS '次高學歷系所'")
        strSQL.AppendLine(", Case When Convert(Char(10), Pr.BirthDate, 111) = '1900/01/01' Then '' Else Convert(Varchar, Pr.BirthDate, 111) End AS '生日'")
        strSQL.AppendLine(", Case When Convert(Char(10), Pr.BirthDate, 111) = '1900/01/01' Then 0 Else Convert(Decimal(4, 2), Convert(Decimal(10, 2), DateDiff(Day, Pr.BirthDate, GETDATE())) / 365) End AS '年齡'")
        strSQL.AppendLine(", Case When Convert(Char(10), Pr.BirthDate, 111) = '1900/01/01' Then 0 Else Floor(DateDiff(Day, Pr.BirthDate, GETDATE()) / 365) End AS '年齡層'")
        strSQL.AppendLine(", Pr.EngName AS '英文姓名'")
        strSQL.AppendLine(", IsNull(T.TitleEngName, '') AS '英文職稱'")
        strSQL.AppendLine(", IsNull(OD.OrganEngName, '') AS '部門英文名稱'")
        strSQL.AppendLine(", Case Pr.PassExamFlag When '1' Then '透過考試合格錄用' When '2' Then '有經驗行員不勾取' Else '' End AS '新員註記'")
        strSQL.AppendLine(", IsNull(WS.Remark, '') AS '工作地點'")
        strSQL.AppendLine(", IsNull(EF.OrganID, '') AS '最小簽核單位代碼'")
        strSQL.AppendLine(", IsNull(EFO.OrganName, '') AS '最小簽核單位名稱'")
        strSQL.AppendLine(", Case When Convert(Char(10), Pr.ProbDate, 111) = '1900/01/01' Then '' Else Convert(Varchar, Pr.ProbDate, 111) End AS '試滿日'")
        strSQL.AppendLine(", '' AS '異動紀錄1'")
        strSQL.AppendLine(", '' AS '異動紀錄2'")
        strSQL.AppendLine(", '' AS '異動紀錄3'")
        strSQL.AppendLine(", '' AS '異動紀錄4'")
        strSQL.AppendLine(", '' AS '異動紀錄5'")
        strSQL.AppendLine(", Pr.EmpIDOld AS '舊員編'")
        strSQL.AppendLine(", Pr.WorkStatus")
        strSQL.AppendLine(", Pr.PassportName AS '護照英文姓名'")
        strSQL.AppendLine(", EP.PositionID AS '職位代碼'")
        strSQL.AppendLine(", IsNull(PO.Remark, '') AS '職位'")
        strSQL.AppendLine(", IsNull (Stuff ((Select ',' + PositionID From EmpPosition SEP")
        strSQL.AppendLine("  Where(Pr.CompID = SEP.CompID And Pr.EmpID = SEP.EmpID And SEP.PrincipalFlag = 0)")
        strSQL.AppendLine("  For Xml Path('')), 1, 1, ''), '') AS '次要職位代碼'")
        strSQL.AppendLine(", IsNull (Stuff (( Select ',' + SPO.Remark From EmpPosition SEP")
        strSQL.AppendLine("  Left Join Position SPO On SEP.PositionID = SPO.PositionID And SEP.CompID = SPO.CompID")
        strSQL.AppendLine("  Where(Pr.CompID = SEP.CompID And Pr.EmpID = SEP.EmpID And SEP.PrincipalFlag = 0)")
        strSQL.AppendLine("  For Xml Path('')), 1, 1, ''), '') AS '次要職位'")
        strSQL.AppendLine(", IsNull(OD.OrgType, '') AS 'OrgType'")
        strSQL.AppendLine(", IsNull(OT.OrganName, '') AS '單位類別'")
        strSQL.AppendLine(", Case Pr.LocalHireFlag When '0' Then '否' When '1' Then '是' End AS 'LocalHireFlag'")
        strSQL.AppendLine(", Case When Convert(Char(10), Pr.SinopacQuitDate, 111) = '1900/01/01' Then '' Else Convert(Varchar, Pr.SinopacQuitDate, 111) End AS '企業團離職日期'")
        strSQL.AppendLine(", IsNull(EL.ModifyDate, '') AS '最後離職復職日期'")
        strSQL.AppendLine(", Pr.HoldingRankID AS '金控職等'")

        If QueryFlag = True Then
            If ht("Grade").ToString() = "Y" Then
                If ht("GradeYear") <> "" Then
                    For Each item In ht("GradeYear").ToString.Split(",")
                        strSQL.AppendLine(", (Select Case (Convert(VarChar(10), DecryptByKey(EG.Grade)))")
                        strSQL.AppendLine("  When '1' Then '優等' When '2' Then '甲等' When '3' Then '乙等' When '4' Then '丙等' When '6' Then '甲上' When '7' Then '甲下' When '9' Then '特優' Else ''")
                        strSQL.AppendLine("  End From EmpGrade EG")
                        strSQL.AppendLine("  Where Pr.CompID = EG.CompID And Pr.EmpID = EG.EmpID")
                        strSQL.AppendLine("  And EG.GradeYear = " & item.ToString)
                        strSQL.AppendLine("  ) AS '" & item.ToString & "考核年度'")
                    Next
                End If
            End If

            If ht("YearSalary").ToString() = "Y" Or ht("MonthSalary").ToString() = "Y" Then
                strSQL.AppendLine(", 'A000' AS '薪資代碼'")
                strSQL.AppendLine(", Case When SD.MonthOfAnnualPay = '12' Then '月薪' Else '年薪' End AS '薪資項目'")
                strSQL.AppendLine(", Case When SD.MonthOfAnnualPay = '12' Then '12' Else '14' End AS '年薪月份'")
                If ht("YearSalary").ToString() = "Y" Then
                    strSQL.AppendLine(", IsNull(S.Amount, '') AS '年薪'")
                End If
                If ht("MonthSalary").ToString() = "Y" Then
                    strSQL.AppendLine(", IsNull(S.Amount, '') AS '月薪'")
                End If
            End If
        End If

        strSQL.AppendLine("From Personal Pr")
        strSQL.AppendLine("Left Join Company C On C.CompID = Pr.CompID")
        strSQL.AppendLine("Left Join Organization OD On OD.CompID = Pr.CompID And OD.OrganID = Pr.DeptID")
        strSQL.AppendLine("Left Join Organization OO On OO.CompID = Pr.CompID And OO.OrganID = Pr.OrganID")
        strSQL.AppendLine("Left Join Organization OT On OT.CompID = OD.CompID And OT.OrganID = OD.OrgType")

        strSQL.AppendLine("Left Join OrganizationFlow OFW On OFW.CompID = Pr.CompID And OFW.GroupID = Pr.GroupID And OFW.OrganID = OFW.GroupID")
        strSQL.AppendLine("And OFW.GroupID In (Select Distinct GroupID From Organization" & IIf(ht("CompID").ToString = "0", "", " Where CompID = " & Bsp.Utility.Quote(ht("CompID").ToString)) & ")")
        strSQL.AppendLine("Left Join HRCodeMap GT On GT.Code = OFW.GroupType And GT.TabName = 'Organization' And GT.FldName = 'GroupType'")

        strSQL.AppendLine("Left Join EmpFlow EF On EF.CompID = Pr.CompID And EF.EmpID = Pr.EmpID And EF.ActionID = '01'")
        strSQL.AppendLine("Left Join OrganizationFlow EFO On EFO.CompID = EF.CompID And EFO.OrganID = EF.OrganID")

        strSQL.AppendLine("Left Join WorkStatus WST On WST.WorkCode = Pr.WorkStatus")
        strSQL.AppendLine("Left Join HRCodeMap ET On ET.Code = Pr.EmpType And ET.TabName = 'Personal' And ET.FldName = 'EmpType'")
        strSQL.AppendLine("Left Join WorkSite WS On WS.CompID = Pr.CompID And WS.WorkSiteID = Pr.WorkSiteID")

        strSQL.AppendLine("Left Join EmpPosition EP On Pr.CompID = EP.CompID And Pr.EmpID = EP.EmpID And EP.PrincipalFlag = '1'")
        strSQL.AppendLine("Left Join Position PO On EP.PositionID = PO.PositionID And EP.CompID = PO.CompID")

        strSQL.AppendLine("Left Join EmpWorkType EW On EW.CompID = Pr.CompID And EW.EmpID = Pr.EmpID And EW.PrincipalFlag = '1'")
        strSQL.AppendLine("Left Join WorkType WT On WT.CompID = EW.CompID And WT.WorkTypeID = EW.WorkTypeID")

        strSQL.AppendLine("Left Join Title T On T.CompID = Pr.CompID And T.RankID = Pr.RankID And T.TitleID = Pr.TitleID")
        strSQL.AppendLine("Left Join EduDegree ED On ED.EduID = Pr.EduID")

        strSQL.AppendLine("Left Join (Select CompID, EmpID, Case When Convert(Char(10), Max(ModifyDate), 111) = '1900/01/01' Then '' Else Convert(Varchar, Max(ModifyDate), 111) End AS ModifyDate From EmployeeLog Where Reason = '02' Group By CompID, EmpID) EL")
        strSQL.AppendLine("On EL.CompID = Pr.CompID And EL.EmpID = Pr.EmpID")

        strSQL.AppendLine("Left Join Salary S On S.CompID = Pr.CompID And S.EmpID = Pr.EmpID And S.SalaryID = 'A000'")
        strSQL.AppendLine("Left Join SalaryData SD On SD.CompID = S.CompID And SD.EmpID = S.EmpID")

        strSQL.AppendLine("Left Join EmpSenComp ESC On ESC.CompID = Pr.CompID And ESC.EmpID = Pr.EmpID")

        strSQL.AppendLine("Where 1=1")
        If ht("CompID").ToString <> "0" Then
            strSQL.AppendLine("And Pr.CompID = " & Bsp.Utility.Quote(ht("CompID").ToString()))
        End If
        strSQL.AppendLine("And Pr.EmpID in (" & ht("EmpID").ToString() & ")")

        strSQL.AppendLine("Order By Pr.WorkStatus, Pr.CompID, Pr.DeptID, Pr.OrganID, EP.PositionID, Pr.EmpID")

        dt = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)

        If dt.Rows.Count > 0 Then
            Dim mSQL As New StringBuilder()
            mSQL.AppendLine("Select Top 5 Convert(Varchar, ModifyDate, 111) + '(' + IsNull(R.Remark, '') + ')-' + DeptName + '[' + WorkType + ']' + IIF(E.Remark = '', '', '(備註：' + E.Remark + ')')")
            mSQL.AppendLine("From EmployeeLog E")
            mSQL.AppendLine("Left Join EmployeeReason R On R.Reason = E.Reason")
            mSQL.AppendLine("Where IDNo = @IDNo")
            mSQL.AppendLine("Order By ModifyDate Desc")

            Dim eSQL As New StringBuilder()
            eSQL.AppendLine("Select Top 2 ED.EduName, E.School, E.Depart, IsNull(S.Remark, E.School) As School1, IsNull(R1.Remark, E.Depart) As Depart1")
            eSQL.AppendLine("From Education E")
            eSQL.AppendLine("Left Join EduDegree ED On ED.EduID = E.EduID")
            eSQL.AppendLine("Left Join School S On S.SchoolID = E.SchoolID")
            eSQL.AppendLine("Left Join Depart R1 On R1.DepartID = E.DepartID")
            eSQL.AppendLine("Where E.EduID <> '080'")
            eSQL.AppendLine("And E.IDNo = @IDNo")
            eSQL.AppendLine("Order By E.EduID Desc")

            For dr As Integer = 0 To dt.Rows.Count - 1
                Dim mDbParam() As DbParameter = {Bsp.DB.getDbParameter("@IDNo", dt.Rows(dr).Item("IDNo").ToString())}
                Using sdt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, mSQL.ToString(), mDbParam, "eHRMSDB").Tables(0)
                    If sdt.Rows.Count > 0 Then
                        For sdr As Integer = 1 To sdt.Rows.Count
                            dt.Rows(dr).Item("異動紀錄" & sdr) = sdt.Rows(sdr - 1).Item(0)
                        Next
                    End If
                End Using

                Dim eDbParam() As DbParameter = {Bsp.DB.getDbParameter("@IDNo", dt.Rows(dr).Item("IDNo").ToString())}
                Using sdt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, eSQL.ToString(), eDbParam, "eHRMSDB").Tables(0)
                    If sdt.Rows.Count > 0 Then
                        dt.Rows(dr).Item("最高學歷名稱") = sdt.Rows(0).Item("EduName")
                        dt.Rows(dr).Item("最高學歷學校") = IIf(sdt.Rows(0).Item("School1") = "", sdt.Rows(0).Item("School"), sdt.Rows(0).Item("School1"))
                        dt.Rows(dr).Item("最高學歷系所") = IIf(sdt.Rows(0).Item("Depart1") = "", sdt.Rows(0).Item("Depart"), sdt.Rows(0).Item("Depart1"))
                    End If

                    If sdt.Rows.Count > 1 Then
                        dt.Rows(dr).Item("次高學歷名稱") = sdt.Rows(1).Item("EduName")
                        dt.Rows(dr).Item("次高學歷學校") = IIf(sdt.Rows(1).Item("School1") = "", sdt.Rows(1).Item("School"), sdt.Rows(1).Item("School1"))
                        dt.Rows(dr).Item("次高學歷系所") = IIf(sdt.Rows(1).Item("Depart1") = "", sdt.Rows(1).Item("Depart"), sdt.Rows(1).Item("Depart1"))
                    End If
                End Using

                If ht("YearSalary").ToString() = "Y" Then
                    dt.Rows(dr).Item("年薪") = objRegistData.funDecryptNumber(dt.Rows(dr).Item("員工編號"), dt.Rows(dr).Item("年薪"))
                    'dt.Rows(dr).Item("年薪") = dt.Rows(dr).Item("舊員編")
                End If

                If ht("MonthSalary").ToString() = "Y" Then
                    dt.Rows(dr).Item("月薪") = MonthSalary(objRegistData.funDecryptNumber(dt.Rows(dr).Item("員工編號"), dt.Rows(dr).Item("月薪")), dt.Rows(dr).Item("年薪月份"))
                    'dt.Rows(dr).Item("月薪") = MonthSalary(dt.Rows(dr).Item("舊員編"), dt.Rows(dr).Item("年薪月份"))
                End If
            Next
        End If

        Return dt
    End Function

    Public Function EmpSenOrgTypeDownload(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select distinct P.IDNo")
        strSQL.AppendLine(", E.CompID AS '公司代碼'")
        strSQL.AppendLine(", C.CompName AS '公司名稱'")
        strSQL.AppendLine(", E.EmpID AS '員工編號'")
        strSQL.AppendLine(", P.NameN AS '員工姓名'")
        strSQL.AppendLine(", E.OrgType AS '單位代碼'")
        strSQL.AppendLine(", E.OrgTypeName AS '單位名稱'")
        If Bsp.Utility.IsStringNull(ht("EmpSenOrgTypeB")) <> "" Or Bsp.Utility.IsStringNull(ht("EmpSenOrgTypeE")) <> "" Then
            strSQL.AppendLine(", E.TotSen AS '單位年資(累計)'")
        End If
        If Bsp.Utility.IsStringNull(ht("EmpConSenOrgTypeB")) <> "" Or Bsp.Utility.IsStringNull(ht("EmpConSenOrgTypeE")) <> "" Then
            strSQL.AppendLine(", E.ConSen AS '單位年資(連續)'")
        End If
        strSQL.AppendLine("From EmpSenOrgType E")
        strSQL.AppendLine("Left Join Company C On C.CompID = E.CompID")
        strSQL.AppendLine("Left Join Personal P On E.CompID = P.CompID And E.EmpID = P.EmpID")
        strSQL.AppendLine("Where 1=1")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        If ht(strKey).ToString() <> "0" Then
                            strSQL.AppendLine("And E.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        End If
                    Case "EmpID"
                        strSQL.AppendLine("And E.EmpID in (" & ht(strKey).ToString() & ")")
                    Case "EmpSenOrgTypeB"
                        strSQL.AppendLine("And E.TotSen >= " & ht(strKey).ToString())
                    Case "EmpSenOrgTypeE"
                        strSQL.AppendLine("And E.TotSen <= " & ht(strKey).ToString())
                    Case "EmpConSenOrgTypeB"
                        strSQL.AppendLine("And E.ConSen >= " & ht(strKey).ToString())
                    Case "EmpConSenOrgTypeE"
                        strSQL.AppendLine("And E.ConSen <= " & ht(strKey).ToString())
                End Select
            End If
        Next

        strSQL.AppendLine("Order By E.EmpID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

    Public Function EmpSenRankDownload(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select distinct P.IDNo")
        strSQL.AppendLine(", E.CompID AS '公司代碼'")
        strSQL.AppendLine(", C.CompName AS '公司名稱'")
        strSQL.AppendLine(", E.EmpID AS '員工編號'")
        strSQL.AppendLine(", P.NameN AS '員工姓名'")
        strSQL.AppendLine(", E.RankID AS '職等代碼'")
        strSQL.AppendLine(", E.TotSen AS '職等年資'")
        strSQL.AppendLine("From EmpSenRank E")
        strSQL.AppendLine("Left Join Company C On C.CompID = E.CompID")
        strSQL.AppendLine("Left Join Personal P On E.CompID = P.CompID And E.EmpID = P.EmpID")
        strSQL.AppendLine("Where 1=1")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        If ht(strKey).ToString() <> "0" Then
                            strSQL.AppendLine("And E.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        End If
                    Case "EmpID"
                        strSQL.AppendLine("And E.EmpID in (" & ht(strKey).ToString() & ")")
                    Case "EmpSenRankB"
                        strSQL.AppendLine("And E.TotSen >= " & ht(strKey).ToString())
                    Case "EmpSenRankE"
                        strSQL.AppendLine("And E.TotSen <= " & ht(strKey).ToString())
                End Select
            End If
        Next

        strSQL.AppendLine("Order By E.EmpID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

    Public Function EmpSenPositionDownload(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select distinct P.IDNo")
        strSQL.AppendLine(", E.CompID AS '公司代碼'")
        strSQL.AppendLine(", C.CompName AS '公司名稱'")
        strSQL.AppendLine(", E.EmpID AS '員工編號'")
        strSQL.AppendLine(", P.NameN AS '員工姓名'")
        strSQL.AppendLine(", E.PositionID AS '職位代碼'")
        strSQL.AppendLine(", E.Position AS '職位名稱'")
        If Bsp.Utility.IsStringNull(ht("EmpSenPositionB")) <> "" Or Bsp.Utility.IsStringNull(ht("EmpSenPositionE")) <> "" Then
            strSQL.AppendLine(", E.TotSen AS '職位年資(累計)'")
        End If
        If Bsp.Utility.IsStringNull(ht("EmpConSenPositionB")) <> "" Or Bsp.Utility.IsStringNull(ht("EmpConSenPositionE")) <> "" Then
            strSQL.AppendLine(", E.ConSen AS '職位年資(連續)'")
        End If
        strSQL.AppendLine(", PI.CategoryIName AS '大類'")
        strSQL.AppendLine(", E.TotCategoryI AS '大類年資'")
        strSQL.AppendLine(", PII.CategoryIIName AS '中類'")
        strSQL.AppendLine(", E.TotCategoryII AS '中類年資'")
        strSQL.AppendLine(", PIII.CategoryIIIName AS '細類'")
        strSQL.AppendLine(", E.TotCategoryIII AS '細類年資'")
        strSQL.AppendLine("From EmpSenPosition E")
        strSQL.AppendLine("Left Join Company C On C.CompID = E.CompID")
        strSQL.AppendLine("Left Join Personal P On E.CompID = P.CompID And E.EmpID = P.EmpID")
        strSQL.AppendLine("Left Join Position_CategoryI PI On E.CategoryI = PI.CategoryI ")
        strSQL.AppendLine("Left Join Position_CategoryII PII On E.CategoryI = PII.CategoryI And E.CategoryII = PII.CategoryII ")
        strSQL.AppendLine("Left Join Position_CategoryIII PIII On E.CategoryI = PIII.CategoryI And E.CategoryII = PIII.CategoryII And E.CategoryIII = PIII.CategoryIII ")
        strSQL.AppendLine("Where 1=1")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        If ht(strKey).ToString() <> "0" Then
                            strSQL.AppendLine("And E.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        End If
                    Case "EmpID"
                        strSQL.AppendLine("And E.EmpID in (" & ht(strKey).ToString() & ")")
                    Case "EmpSenPositionB"
                        strSQL.AppendLine("And E.TotSen >= " & ht(strKey).ToString())
                    Case "EmpSenPositionE"
                        strSQL.AppendLine("And E.TotSen <= " & ht(strKey).ToString())
                    Case "EmpConSenPositionB"
                        strSQL.AppendLine("And E.ConSen >= " & ht(strKey).ToString())
                    Case "EmpConSenPositionE"
                        strSQL.AppendLine("And E.ConSen <= " & ht(strKey).ToString())
                End Select
            End If
        Next

        strSQL.AppendLine("Order By E.EmpID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

    Public Function EmpSenWorkTypeDownload(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select distinct P.IDNo")
        strSQL.AppendLine(", E.CompID AS '公司代碼'")
        strSQL.AppendLine(", C.CompName AS '公司名稱'")
        strSQL.AppendLine(", E.EmpID AS '員工編號'")
        strSQL.AppendLine(", P.NameN AS '員工姓名'")
        strSQL.AppendLine(", E.WorkTypeID AS '工作性質代碼'")
        strSQL.AppendLine(", E.WorkType AS '工作性質名稱'")
        If Bsp.Utility.IsStringNull(ht("EmpSenWorkTypeB")) <> "" Or Bsp.Utility.IsStringNull(ht("EmpSenWorkTypeE")) <> "" Then
            strSQL.AppendLine(", E.TotSen AS '工作性質年資(累計)'")
        End If
        If Bsp.Utility.IsStringNull(ht("EmpConSenWorkTypeB")) <> "" Or Bsp.Utility.IsStringNull(ht("EmpConSenWorkTypeE")) <> "" Then
            strSQL.AppendLine(", E.ConSen AS '工作性質年資(連續)'")
        End If
        strSQL.AppendLine(", WI.CategoryIName AS '大類'")
        strSQL.AppendLine(", E.TotCategoryI AS '大類年資'")
        strSQL.AppendLine(", WII.CategoryIIName AS '中類'")
        strSQL.AppendLine(", E.TotCategoryII AS '中類年資'")
        strSQL.AppendLine(", WIII.CategoryIIIName AS '細類'")
        strSQL.AppendLine(", E.TotCategoryIII AS '細類年資'")
        strSQL.AppendLine("From EmpSenWorkType E")
        strSQL.AppendLine("Left Join Company C On C.CompID = E.CompID")
        strSQL.AppendLine("Left Join Personal P On E.CompID = P.CompID And E.EmpID = P.EmpID")
        strSQL.AppendLine("Left Join WorkType_CategoryI WI On E.CategoryI = WI.CategoryI ")
        strSQL.AppendLine("Left Join WorkType_CategoryII WII On E.CategoryI = WII.CategoryI And E.CategoryII = WII.CategoryII ")
        strSQL.AppendLine("Left Join WorkType_CategoryIII WIII On E.CategoryI = WIII.CategoryI And E.CategoryII = WIII.CategoryII And E.CategoryIII = WIII.CategoryIII ")
        strSQL.AppendLine("Where 1=1")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        If ht(strKey).ToString() <> "0" Then
                            strSQL.AppendLine("And E.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        End If
                    Case "EmpID"
                        strSQL.AppendLine("And E.EmpID in (" & ht(strKey).ToString() & ")")
                    Case "EmpSenWorkTypeB"
                        strSQL.AppendLine("And E.TotSen >= " & ht(strKey).ToString())
                    Case "EmpSenWorkTypeE"
                        strSQL.AppendLine("And E.TotSen <= " & ht(strKey).ToString())
                    Case "EmpConSenWorkTypeB"
                        strSQL.AppendLine("And E.ConSen >= " & ht(strKey).ToString())
                    Case "EmpConSenWorkTypeE"
                        strSQL.AppendLine("And E.ConSen <= " & ht(strKey).ToString())
                End Select
            End If
        Next

        strSQL.AppendLine("Order By E.EmpID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "解密考績"
    Public Function DecryptGrade(ByVal dt As DataTable, ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()
        Dim gDbParam() As DbParameter

        Dim strGrade As String = ""
        Dim intGrade As Integer = 0

        strSQL.AppendLine(Bsp.Utility.getAppSetting("eHRMSDBDES"))
        strSQL.AppendLine("Select Convert(Char(10), DecryptByKey(E.Grade))")
        strSQL.AppendLine("From Personal P")
        strSQL.AppendLine("Join EmpGrade E on E.CompID = P.CompID and E.EmpID = P.EmpID")
        strSQL.AppendLine("And E.GradeYear >= (Year(GetDate()) - 4)")
        strSQL.AppendLine("Where P.CompID = @CompID and P.EmpID = @EmpID")
        strSQL.AppendLine("Order By E.GradeYear Desc")

        For i As Integer = 0 To dt.Rows.Count - 1
            intGrade = 0
            gDbParam = {Bsp.DB.getDbParameter("@CompID", dt.Rows(i).Item("CompID").ToString()), Bsp.DB.getDbParameter("@EmpID", dt.Rows(i).Item("EmpID").ToString())}

            Using gdt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), gDbParam, "eHRMSDB").Tables(0)
                If gdt.Rows.Count <= 0 Then
                    dt.Rows(i).Delete()
                    Continue For
                End If

                '考績代碼值--1─優等，2─甲等，3─乙等，4─丙等，6─甲上，7─甲下，9─特優
                For Each strKey As String In ht.Keys
                    If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                        Select Case strKey
                            Case "CommGrade" '常用條件
                                Select Case (ht(strKey).ToString())
                                    Case "1" '1-近五年三甲
                                        If gdt.Rows.Count >= 3 Then
                                            For j As Integer = 0 To gdt.Rows.Count - 1
                                                If gdt.Rows(j).Item(0).ToString().Trim() = "2" Then '甲等
                                                    intGrade = intGrade + 1
                                                End If
                                            Next
                                        End If
                                        If intGrade < 3 Then
                                            dt.Rows(i).Delete()
                                            Exit For
                                        End If
                                    Case "2" '2-近三年兩乙
                                        If gdt.Rows.Count >= 3 Then
                                            For j As Integer = 0 To 2
                                                If gdt.Rows(j).Item(0).ToString().Trim() = "3" Then '乙等
                                                    intGrade = intGrade + 1
                                                End If
                                            Next
                                        End If
                                        If intGrade < 2 Then
                                            dt.Rows(i).Delete()
                                            Exit For
                                        End If
                                    Case "3" '3-近兩年優
                                        If gdt.Rows.Count >= 2 Then
                                            For j As Integer = 0 To 1
                                                If gdt.Rows(j).Item(0).ToString().Trim() = "1" Then '優等
                                                    intGrade = intGrade + 1
                                                End If
                                            Next
                                        End If
                                        If intGrade < 2 Then
                                            dt.Rows(i).Delete()
                                            Exit For
                                        End If
                                    Case "4" '4-近兩年乙
                                        If gdt.Rows.Count >= 2 Then
                                            For j As Integer = 0 To 1
                                                If gdt.Rows(j).Item(0).ToString().Trim() = "3" Then '乙等
                                                    intGrade = intGrade + 1
                                                End If
                                            Next
                                        End If
                                        If intGrade < 2 Then
                                            dt.Rows(i).Delete()
                                            Exit For
                                        End If
                                    Case "5" '5-近三年優
                                        If gdt.Rows.Count >= 3 Then
                                            For j As Integer = 0 To 2
                                                If gdt.Rows(j).Item(0).ToString().Trim() = "1" Then '優等
                                                    intGrade = intGrade + 1
                                                End If
                                            Next
                                        End If
                                        If intGrade < 3 Then
                                            dt.Rows(i).Delete()
                                            Exit For
                                        End If
                                End Select
                            Case "LastGrade1" '前一年考績
                                If gdt.Rows.Count < 2 Or gdt.Rows(1).Item(0).ToString().Trim() <> ht(strKey).ToString() Then
                                    dt.Rows(i).Delete()
                                    Exit For
                                End If
                            Case "LastGrade2" '前二年考績
                                If gdt.Rows.Count < 3 Or gdt.Rows(2).Item(0).ToString().Trim() <> ht(strKey).ToString() Then
                                    dt.Rows(i).Delete()
                                    Exit For
                                End If
                            Case "LastGrade3" '前三年考績
                                If gdt.Rows.Count < 4 Or gdt.Rows(3).Item(0).ToString().Trim() <> ht(strKey).ToString() Then
                                    dt.Rows(i).Delete()
                                    Exit For
                                End If
                            Case "LastGrade4" '前四年考績
                                If gdt.Rows.Count < 5 Or gdt.Rows(4).Item(0).ToString().Trim() <> ht(strKey).ToString() Then
                                    dt.Rows(i).Delete()
                                    Exit For
                                End If
                        End Select
                    End If
                Next
            End Using
        Next

        dt.AcceptChanges()

        Return dt
    End Function
#End Region

#Region "解密薪資"
    Public Function DecryptSalary(ByVal dt As DataTable, ByVal strYSalaryB As String, ByVal strYSalaryE As String, ByVal strMSalaryB As String, ByVal strMSalaryE As String) As DataTable
        Dim objRegistData As New RegistData()
        Dim strSalary As String = ""

        If strYSalaryB = "" Then
            strYSalaryB = 0
        End If

        If strYSalaryE = "" Then
            strYSalaryE = 0
        End If

        If strMSalaryB = "" Then
            strMSalaryB = 0
        End If

        If strMSalaryE = "" Then
            strMSalaryE = 0
        End If

        Dim longYSalaryB As Long = Long.Parse(strYSalaryB)
        Dim longYSalaryE As Long = Long.Parse(strYSalaryE)
        Dim longMSalaryB As Long = Long.Parse(strMSalaryB)
        Dim longMSalaryE As Long = Long.Parse(strMSalaryE)
        Dim longYSalary As Long = 0
        Dim longMSalary As Long = 0

        For i As Integer = 0 To dt.Rows.Count - 1
            strSalary = objRegistData.funDecryptNumber(dt.Rows(i).Item("EmpID"), dt.Rows(i).Item("Amount"))
            'strSalary = dt.Rows(i).Item("EmpIDOld")

            longYSalary = 0
            longMSalary = 0

            If Long.TryParse(strSalary, longYSalary) Then
                longYSalary = Long.Parse(strSalary)
                If longMSalaryB > 0 Or longMSalaryE > 0 Then
                    If dt.Rows(i).Item("MonthOfAnnualPay").ToString() = "12" Then
                        longMSalary = Math.Round(longYSalary / 12)
                    ElseIf dt.Rows(i).Item("MonthOfAnnualPay").ToString() = "14" Then
                        longMSalary = Math.Round(longYSalary / 14)
                    End If
                End If
            End If

            If longYSalaryB > 0 And longYSalary < longYSalaryB Then
                dt.Rows(i).Delete()
                Continue For
            End If

            If longYSalaryE > 0 And longYSalary > longYSalaryE Then
                dt.Rows(i).Delete()
                Continue For
            End If

            If longMSalaryB > 0 And longMSalary < longMSalaryB Then
                dt.Rows(i).Delete()
                Continue For
            End If

            If longMSalaryE > 0 And longMSalary > longMSalaryE Then
                dt.Rows(i).Delete()
                Continue For
            End If
        Next

        dt.AcceptChanges()

        Return dt
    End Function
#End Region

#Region "轉換月薪"
    Public Function MonthSalary(ByVal strSalary As String, ByVal strMonthOfAnnualPay As String) As String
        Dim longYSalary As Long = 0
        Dim longMSalary As Long = 0

        If Long.TryParse(strSalary, longYSalary) Then
            If strMonthOfAnnualPay = "12" Then
                longMSalary = Math.Round(longYSalary / 12)
            ElseIf strMonthOfAnnualPay = "14" Then
                longMSalary = Math.Round(longYSalary / 14)
            End If

            strSalary = longMSalary.ToString()
        End If

        Return strSalary
    End Function
#End Region

#Region "查詢職等對應數字"
    Public Function GetRankMapping(ByVal CompID As String, ByVal RankID As String) As String
        Dim strRankIDMap As String = ""
        Dim strSQL As String = "Select RankIDMap From RankMapping Where CompID = " & Bsp.Utility.Quote(CompID) & " And RankID = " & Bsp.Utility.Quote(RankID)

        Dim objRankIDMap As Object = Bsp.DB.ExecuteScalar(strSQL, "eHRMSDB")
        If Not objRankIDMap Is Nothing Then
            strRankIDMap = objRankIDMap.ToString()
        End If

        Return strRankIDMap
    End Function
#End Region

#Region "是否有權限查詢重要資料"
    Public Function GetQueryFlag(ByVal CompID As String, ByVal UserID As String, ByVal CompRoleID As String, ByVal SysID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select QueryID From SC_GroupMutiQry")
        strSQL.AppendLine("Where 1 = 1")
        strSQL.AppendLine("And SysID = " & Bsp.Utility.Quote(SysID))
        strSQL.AppendLine("And CompRoleID = " & Bsp.Utility.Quote(CompRoleID))
        strSQL.AppendLine("And GroupID in (")
        strSQL.AppendLine("Select GroupID From SC_UserGroup")
        strSQL.AppendLine("Where 1 = 1")
        strSQL.AppendLine("And SysID = " & Bsp.Utility.Quote(SysID))
        strSQL.AppendLine("And CompRoleID = " & Bsp.Utility.Quote(CompRoleID))
        strSQL.AppendLine("And CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine("And UserID = " & Bsp.Utility.Quote(UserID))
        strSQL.AppendLine(")")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function
#End Region

#Region "是否有權限進入交易"
    Public Function GetAdmission(ByVal CompID As String, ByVal UserID As String, ByVal SysID As String, ByVal CompRoleID As String, ByVal FunID As String) As Boolean
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select Count(*) As Cnt From SC_GroupFun")
        strSQL.AppendLine("Where 1 = 1")
        strSQL.AppendLine("And FunID = " & Bsp.Utility.Quote(FunID))
        strSQL.AppendLine("And CompRoleID = " & Bsp.Utility.Quote(CompRoleID))
        strSQL.AppendLine("And GroupID in (")
        strSQL.AppendLine("Select GroupID From SC_UserGroup")
        strSQL.AppendLine("Where 1 = 1")
        strSQL.AppendLine("And SysID = " & Bsp.Utility.Quote(SysID))
        strSQL.AppendLine("And CompRoleID = " & Bsp.Utility.Quote(CompRoleID))
        strSQL.AppendLine("And CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine("And UserID = " & Bsp.Utility.Quote(UserID))
        strSQL.AppendLine(")")

        Dim Cnt As Integer = Bsp.DB.ExecuteScalar(strSQL.ToString).ToString()
        If Cnt > 0 Then
            Return True
        End If

        Return False
    End Function
#End Region

#End Region

End Class
