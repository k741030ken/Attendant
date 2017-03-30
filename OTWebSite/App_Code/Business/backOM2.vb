'****************************************************
'功能說明：
'建立人員：Rebecca Yan
'建立日期：2016.09.24
'****************************************************
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.SqlCommand
Imports System.IO
Imports System
Imports System.Data.SqlClient

Public Class BackOM2
#Region "OM2000 組織待異動資料維護"
#Region "查詢"
    '行政組織-查詢Organization
    Public Function OM2000QueryOrganization(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT O.CompID ") '公司代碼
        strSQL.AppendLine(" , ISNULL(C1.CompName, '') As CompName ") '公司名稱
        strSQL.AppendLine(" , '行政組織' OrganType ") '組織類型
        strSQL.AppendLine(" , '' BusinessType") '業務類別
        strSQL.AppendLine(" , O.OrganID ") '部門代碼
        strSQL.AppendLine(" , O.OrganName ") '部門名稱
        strSQL.AppendLine(" , InValidFlag = Case O.InValidFlag WHEN '1' THEN '無效' WHEN '0' THEN '有效' END") '無效註記
        strSQL.AppendLine(" , ValidDateB = Case When Convert(Char(10), O.ValidDateB, 111) = '1900/01/01' Then '' ELSE Convert(Char(10), O.ValidDateB, 111) End") '單位有效起日
        strSQL.AppendLine(" , hidValidDateB = Convert(Char(10), O.ValidDateB, 111)") '單位有效起日(隱藏欄位)
        strSQL.AppendLine(" , ValidDateE = Case When Convert(Char(10), O.ValidDateE, 111) = '1900/01/01' Then '' ELSE Convert(Char(10), O.ValidDateE, 111) End") '單位有效迄日
        strSQL.AppendLine(" , ISNULL(C2.CompName, '') As LastChgComp ") '最後異動公司
        strSQL.AppendLine(" , ISNULL(P.NameN, '') As LastChgID") '最後異動人員公司
        strSQL.AppendLine(" , LastChgDate = Case When Convert(Char(10), O.LastChgDate, 111) = '1900/01/01' Then '' ELSE Convert(Varchar, O.LastChgDate, 120) End ") '最後異動日期
        strSQL.AppendLine(" From Organization O ")
        strSQL.AppendLine(" Left Join Company C1 on O.CompID = C1.CompID")
        strSQL.AppendLine(" Left Join Company C2 on O.LastChgComp = C2.CompID")
        strSQL.AppendLine(" Left Join Personal P on O.LastChgComp = P.CompID AND O.LastChgID = P.EmpID")
        strSQL.AppendLine(" Where 1 = 1 ")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine("And O.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "OrganID"
                        strSQL.AppendLine("And O.OrganID like UPPER('%" + ht(strKey).ToString() + "%') ")
                    Case "OrganName"
                        strSQL.AppendLine("And O.OrganName Like N" & Bsp.Utility.Quote("%" + ht(strKey).ToString() + "%"))
                    Case "InValidFlag"
                        strSQL.AppendLine("And O.InValidFlag = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next
        If Bsp.Utility.IsStringNull(ht("ValidDateB")) <> "" And Bsp.Utility.IsStringNull(ht("ValidDateE")) = "" Then
            strSQL.AppendLine("And (O.ValidDateB >= " & Bsp.Utility.Quote(ht("ValidDateB").ToString()) & " Or O.ValidDateE >= " & Bsp.Utility.Quote(ht("ValidDateB").ToString()) & ")")
        ElseIf Bsp.Utility.IsStringNull(ht("ValidDateB")) = "" And Bsp.Utility.IsStringNull(ht("ValidDateE")) <> "" Then
            strSQL.AppendLine("And (O.ValidDateB <= " & Bsp.Utility.Quote(ht("ValidDateE").ToString()) & " Or O.ValidDateE <= " & Bsp.Utility.Quote(ht("ValidDateE").ToString()) & ")")
        ElseIf Bsp.Utility.IsStringNull(ht("ValidDateB")) <> "" And Bsp.Utility.IsStringNull(ht("ValidDateE")) <> "" Then
            strSQL.AppendLine("And ((O.ValidDateB >= " & Bsp.Utility.Quote(ht("ValidDateB").ToString()) & " And O.ValidDateB <= " & Bsp.Utility.Quote(ht("ValidDateE").ToString()) & ") Or (O.ValidDateE >= " & Bsp.Utility.Quote(ht("ValidDateB").ToString()) & " AND O.ValidDateE <= " & Bsp.Utility.Quote(ht("ValidDateE").ToString()) & "))")
        End If
        strSQL.AppendLine("Order By O.InValidFlag, O.VirtualFlag, O.OrganID")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
    '功能組織-查詢OrganizationFlow
    Public Function OM2000QueryOrganizationFlow(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT O.CompID") '公司代碼
        strSQL.AppendLine(" , ISNULL(C1.CompName, '') As CompName ") '公司名稱
        strSQL.AppendLine(" , '<font color=''red''>功能組織</font>' OrganType ")
        strSQL.AppendLine(" , ISNULL(H.CodeCName, '') BusinessType")
        strSQL.AppendLine(" , O.OrganID") '部門代碼
        strSQL.AppendLine(" , O.OrganName") '部門名稱
        strSQL.AppendLine(" , InValidFlag=Case O.InValidFlag WHEN '1' THEN '無效' WHEN '0' THEN '有效' END")
        strSQL.AppendLine(" , ValidDateB = Case When Convert(Char(10), O.ValidDateB, 111) = '1900/01/01' Then '' ELSE Convert(Char(10), O.ValidDateB, 111) End") '單位有效起日
        strSQL.AppendLine(" , hidValidDateB = Convert(Char(10), O.ValidDateB, 111)") '單位有效起日(隱藏欄位)
        strSQL.AppendLine(" , ValidDateE = Case When Convert(Char(10), O.ValidDateE, 111) = '1900/01/01' Then '' ELSE Convert(Char(10), O.ValidDateE, 111) End") '單位有效迄日
        strSQL.AppendLine(" , ISNULL(C2.CompName, '') As LastChgComp ") '最後異動公司
        strSQL.AppendLine(" , ISNULL(P.NameN, '') As LastChgID") '最後異動人員
        strSQL.AppendLine(" , LastChgDate = Case When Convert(Char(10), O.LastChgDate, 111) = '1900/01/01' Then '' ELSE Convert(Varchar, O.LastChgDate, 120) End ") '最後異動日期
        strSQL.AppendLine(" From OrganizationFlow O ")
        strSQL.AppendLine(" Left Join Company C1 on O.CompID = C1.CompID")
        strSQL.AppendLine(" Left Join HRCodeMap As H On H.TabName = 'Business' And H.FldName = 'BusinessType' And O.BusinessType = H.Code ")
        strSQL.AppendLine(" Left Join Company C2 on O.LastChgComp = C2.CompID")
        strSQL.AppendLine(" Left Join Personal P on O.LastChgComp = P.CompID And O.LastChgID = P.EmpID ")
        strSQL.AppendLine(" Where 1 = 1 ")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine("And O.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "OrganID"
                        strSQL.AppendLine("And O.OrganID like UPPER('%" + ht(strKey).ToString() + "%') ")
                    Case "OrganName"
                        strSQL.AppendLine("And O.OrganName Like N" & Bsp.Utility.Quote("%" + ht(strKey).ToString() + "%"))
                    Case "InValidFlag"
                        strSQL.AppendLine("And O.InValidFlag = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "BusinessType" '功能組織
                        strSQL.AppendLine("And O.BusinessType = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next
        If Bsp.Utility.IsStringNull(ht("ValidDateB")) <> "" And Bsp.Utility.IsStringNull(ht("ValidDateE")) = "" Then
            strSQL.AppendLine("And (O.ValidDateB >= " & Bsp.Utility.Quote(ht("ValidDateB").ToString()) & " Or O.ValidDateE >= " & Bsp.Utility.Quote(ht("ValidDateB").ToString()) & ")")
        ElseIf Bsp.Utility.IsStringNull(ht("ValidDateB")) = "" And Bsp.Utility.IsStringNull(ht("ValidDateE")) <> "" Then
            strSQL.AppendLine("And (O.ValidDateB <= " & Bsp.Utility.Quote(ht("ValidDateE").ToString()) & " Or O.ValidDateE <= " & Bsp.Utility.Quote(ht("ValidDateE").ToString()) & ")")
        ElseIf Bsp.Utility.IsStringNull(ht("ValidDateB")) <> "" And Bsp.Utility.IsStringNull(ht("ValidDateE")) <> "" Then
            strSQL.AppendLine("And ((O.ValidDateB >= " & Bsp.Utility.Quote(ht("ValidDateB").ToString()) & " And O.ValidDateB <= " & Bsp.Utility.Quote(ht("ValidDateE").ToString()) & ") Or (O.ValidDateE >= " & Bsp.Utility.Quote(ht("ValidDateB").ToString()) & " AND O.ValidDateE <= " & Bsp.Utility.Quote(ht("ValidDateE").ToString()) & "))")
        End If
        strSQL.AppendLine("Order By O.InValidFlag, O.CompareFlag, O.OrganID")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
    '行政、功能組織-查詢OrganizationAndOrganizationFlow
    Public Function OM2000QueryOrganizationAndFlow(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT O.CompID ")
        strSQL.AppendLine(" , ISNULL(C1.CompName, '') As CompName ")
        strSQL.AppendLine(" , '行政組織' OrganType ")
        strSQL.AppendLine(" , '0' As Seq ")
        strSQL.AppendLine(" , O.OrganID ")
        strSQL.AppendLine(" , O.OrganName")
        strSQL.AppendLine(" , '' BusinessType")
        strSQL.AppendLine(" , InValidFlag=Case O.InValidFlag WHEN '1' THEN '無效' WHEN '0' THEN '有效' END")
        strSQL.AppendLine(" , ValidDateB = Case When Convert(Char(10), O.ValidDateB, 111) = '1900/01/01' Then '' ELSE Convert(Char(10), O.ValidDateB, 111) End")
        strSQL.AppendLine(" , hidValidDateB = Convert(Char(10), O.ValidDateB, 111)") '單位有效起日(隱藏欄位)
        strSQL.AppendLine(" , ValidDateE = Case When Convert(Char(10), O.ValidDateE, 111) = '1900/01/01' Then '' ELSE Convert(Char(10), O.ValidDateE, 111) End")
        strSQL.AppendLine(" , C2.CompName As LastChgComp ")
        strSQL.AppendLine(" , ISNULL(P.NameN, '') As LastChgID ")
        strSQL.AppendLine(" , LastChgDate = Case When Convert(Char(10), O.LastChgDate, 111) = '1900/01/01 00:00:00' Then '' ELSE Convert(Varchar, O.LastChgDate, 120) End ")
        strSQL.AppendLine(" From Organization O ")
        strSQL.AppendLine(" Left Join Company C1 on O.CompID = C1.CompID ")
        strSQL.AppendLine(" Left Join Company C2 on O.LastChgComp = C2.CompID ")
        strSQL.AppendLine(" Left Join Personal P on O.LastChgComp = P.CompID And O.LastChgID = P.EmpID ")
        strSQL.AppendLine(" Where 1=1 ")
        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine("And O.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "OrganID"
                        strSQL.AppendLine("And O.OrganID like UPPER('%" + ht(strKey).ToString() + "%') ")
                    Case "OrganName"
                        strSQL.AppendLine("And O.OrganName Like N" & Bsp.Utility.Quote("%" + ht(strKey).ToString() + "%"))
                    Case "InValidFlag"
                        strSQL.AppendLine("And O.InValidFlag = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next
        If Bsp.Utility.IsStringNull(ht("ValidDateB")) <> "" And Bsp.Utility.IsStringNull(ht("ValidDateE")) = "" Then
            strSQL.AppendLine(" And (O.ValidDateB >= " & Bsp.Utility.Quote(ht("ValidDateB").ToString()) & " Or O.ValidDateE >= " & Bsp.Utility.Quote(ht("ValidDateB").ToString()) & ") ")
        ElseIf Bsp.Utility.IsStringNull(ht("ValidDateB")) = "" And Bsp.Utility.IsStringNull(ht("ValidDateE")) <> "" Then
            strSQL.AppendLine(" And (O.ValidDateB <= " & Bsp.Utility.Quote(ht("ValidDateE").ToString()) & " Or O.ValidDateE <= " & Bsp.Utility.Quote(ht("ValidDateE").ToString()) & ") ")
        ElseIf Bsp.Utility.IsStringNull(ht("ValidDateB")) <> "" And Bsp.Utility.IsStringNull(ht("ValidDateE")) <> "" Then
            strSQL.AppendLine(" And ((O.ValidDateB >= " & Bsp.Utility.Quote(ht("ValidDateB").ToString()) & " And O.ValidDateB <= " & Bsp.Utility.Quote(ht("ValidDateE").ToString()) & ") Or (O.ValidDateE >= " & Bsp.Utility.Quote(ht("ValidDateB").ToString()) & " AND O.ValidDateE <= " & Bsp.Utility.Quote(ht("ValidDateE").ToString()) & ")) ")
        End If
        strSQL.AppendLine(" UNION SELECT OrgF.CompID ")
        strSQL.AppendLine(" , ISNULL(C1.CompName, '') As CompName ")
        strSQL.AppendLine(" , '<font color=''red''>功能組織</font>' OrganType ")
        strSQL.AppendLine(" , '1' As Seq ")
        strSQL.AppendLine(" , OrgF.OrganID ")
        strSQL.AppendLine(" , OrgF.OrganName ")
        strSQL.AppendLine(" , ISNULL(H.CodeCName, '') BusinessType ")
        strSQL.AppendLine(" , InValidFlag = Case OrgF.InValidFlag WHEN '1' THEN '無效' WHEN '0' THEN '有效' END ")
        strSQL.AppendLine(" , ValidDateB = Case When Convert(Char(10), OrgF.ValidDateB, 111) = '1900/01/01' Then '' ELSE Convert(Char(10), OrgF.ValidDateB, 111) End ")
        strSQL.AppendLine(" , hidValidDateB = Convert(Char(10), OrgF.ValidDateB, 111)") '單位有效起日(隱藏欄位)
        strSQL.AppendLine(" , ValidDateE = Case When Convert(Char(10), OrgF.ValidDateE, 111) = '1900/01/01' Then '' ELSE Convert(Char(10), OrgF.ValidDateE, 111) End ")
        strSQL.AppendLine(" , ISNULL(C2.CompName, '') As LastChgComp ")
        strSQL.AppendLine(" , ISNULL(P.NameN, '') As LastChgID ")
        strSQL.AppendLine(" , LastChgDate = Case When Convert(Char(10), OrgF.LastChgDate, 111) = '1900/01/01' Then '' ELSE Convert(Varchar, OrgF.LastChgDate, 120) End ")
        strSQL.AppendLine(" From OrganizationFlow OrgF")
        strSQL.AppendLine(" Left Join Company C1 on OrgF.CompID = C1.CompID ")
        strSQL.AppendLine(" Left Join HRCodeMap As H On H.TabName = 'Business' And H.FldName = 'BusinessType' And OrgF.BusinessType = H.Code ")
        strSQL.AppendLine(" Left Join Company C2 on OrgF.LastChgComp = C2.CompID ")
        strSQL.AppendLine(" Left Join Personal P on OrgF.LastChgComp = P.CompID And OrgF.LastChgID = P.EmpID ")
        strSQL.AppendLine(" Where 1=1 ")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine("And OrgF.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "OrganID"
                        strSQL.AppendLine("And OrgF.OrganID  like UPPER('%" + ht(strKey).ToString() + "%') ")
                    Case "OrganName"
                        strSQL.AppendLine("And OrgF.OrganName Like N" & Bsp.Utility.Quote("%" + ht(strKey).ToString() + "%"))
                    Case "InValidFlag"
                        strSQL.AppendLine("And OrgF.InValidFlag = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next

        If Bsp.Utility.IsStringNull(ht("ValidDateB")) <> "" And Bsp.Utility.IsStringNull(ht("ValidDateE")) = "" Then
            strSQL.AppendLine(" And (OrgF.ValidDateB >= " & Bsp.Utility.Quote(ht("ValidDateB").ToString()) & " Or OrgF.ValidDateE >= " & Bsp.Utility.Quote(ht("ValidDateB").ToString()) & ") ")
        ElseIf Bsp.Utility.IsStringNull(ht("ValidDateB")) = "" And Bsp.Utility.IsStringNull(ht("ValidDateE")) <> "" Then
            strSQL.AppendLine(" And (OrgF.ValidDateB <= " & Bsp.Utility.Quote(ht("ValidDateE").ToString()) & " Or OrgF.ValidDateE <= " & Bsp.Utility.Quote(ht("ValidDateE").ToString()) & ")")
        ElseIf Bsp.Utility.IsStringNull(ht("ValidDateB")) <> "" And Bsp.Utility.IsStringNull(ht("ValidDateE")) <> "" Then
            strSQL.AppendLine(" And ((OrgF.ValidDateB >= " & Bsp.Utility.Quote(ht("ValidDateB").ToString()) & " And OrgF.ValidDateB <= " & Bsp.Utility.Quote(ht("ValidDateE").ToString()) & ") Or (OrgF.ValidDateE >= " & Bsp.Utility.Quote(ht("ValidDateB").ToString()) & " AND OrgF.ValidDateE <= " & Bsp.Utility.Quote(ht("ValidDateE").ToString()) & "))")
        End If

        strSQL.AppendLine(" Order By Seq ")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

    '組織異動紀錄查詢(20161101-leo modify) '組織類型
    Public Function OM2000QueryOrganizationLog(ByVal CompID As String, ByVal OrganID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" Select CompID ")
        strSQL.AppendLine(" , ValidDateB = Case When Convert(Char(10), ValidDateB, 111) = '1900/01/01' Then '' ELSE Convert(Char(10), ValidDateB, 111) End") '生效日期"
        strSQL.AppendLine(" , OrganReason = Case OrganReason WHEN '1' THEN '組織新增' WHEN '2' THEN '組織無效' WHEN '3' THEN '組織異動' END") '異動原因
        strSQL.AppendLine(" , OrganType= Case OrganType WHEN '1' THEN '行政組織' WHEN '2' THEN '功能組織' WHEN '3' THEN '行政組織與功能組織' END") '組織類型
        strSQL.AppendLine(" , OrganNameOld ") '異動「前」部門名稱
        strSQL.AppendLine(" , BossOld") '異動「前」部門主管
        strSQL.AppendLine(" , UpOrganIDOld") '異動「前」上階部門
        strSQL.AppendLine(" , OrganID ") '異動「後」部門代碼
        strSQL.AppendLine(" , OrganName ") '異動「後」部門名稱
        strSQL.AppendLine(" , Boss ") '異動「後」部門主管
        strSQL.AppendLine(" , UpOrganID ") '異動「後」上階部門
        strSQL.AppendLine(" From OrganizationLog ")
        strSQL.AppendLine(" Where CompID =" & Bsp.Utility.Quote(CompID.ToString()) & "And OrganID =" & Bsp.Utility.Quote(OrganID.ToString()))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

    '歷任主管紀錄查詢(現任行政)
    Public Function OM2000QueryOrganizationBossLogNow(ByVal CompID As String, ByVal OrganID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT O.CompID ")
        strSQL.AppendLine(" , O.OrganID")
        strSQL.AppendLine(" , ISNULL(O1.OrganName, '') As OrganName ")
        strSQL.AppendLine(" , O.Boss ")
        strSQL.AppendLine(" , ISNULL(P.NameN, '') As BossName ")
        strSQL.AppendLine(" , ValidDateBH = Case When Convert(Char(10), O.ValidDateBH, 111) = '1900/01/01' Then '' ELSE Convert(Char(10), O.ValidDateBH, 111) End ")
        strSQL.AppendLine(" , O.BossType ")
        strSQL.AppendLine(" FROM OrganizationBossLog As O ")
        strSQL.AppendLine(" Left Join OrganizationFlow AS O1 On O.OrganID = O1.OrganID ")
        strSQL.AppendLine(" Left Join Personal As P On O.BossCompID = P.CompID And O.Boss = P.EmpID ")
        strSQL.AppendLine(" Where O.CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine(" And O.OrganID = " & Bsp.Utility.Quote(OrganID))
        strSQL.AppendLine(" And O.ValidDateEH = '1900/01/01' ") '只有現任主管

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

    '歷任主管紀錄查詢(歷任行政)
    Public Function OM2000QueryOrganizationBossLogPast(ByVal CompID As String, ByVal OrganID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT O.CompID")
        strSQL.AppendLine(" , O.OrganID")
        strSQL.AppendLine(" , ValidDateBH = Case When Convert(Char(10), O.ValidDateBH, 111) = '1900/01/01' Then '' ELSE Convert(Char(10), O.ValidDateBH, 111) End ")
        strSQL.AppendLine(" , ValidDateEH = Case When Convert(Char(10), O.ValidDateEH, 111) = '1900/01/01' Then '' ELSE Convert(Char(10), O.ValidDateEH, 111) End ")
        strSQL.AppendLine(" , O.BossCompID ")
        strSQL.AppendLine(" , O.Boss ")
        strSQL.AppendLine(" , ISNULL(P.NameN, '') As BossName")
        strSQL.AppendLine(" , BossType = Case O.BossType WHEN '1' THEN '主要' WHEN '2' THEN '兼任' END ")
        strSQL.AppendLine(", ISNULL(C.CompName, '') As LastChgComp ")
        strSQL.AppendLine(", ISNULL(P1.NameN, '') As LastChgID")
        strSQL.AppendLine(", LastChgDate = Case When Convert(Char(10), O.LastChgDate, 111) = '1900/01/01' Then '' ELSE Convert(Varchar, O.LastChgDate, 120) End ")
        strSQL.AppendLine(" FROM OrganizationBossLog As O")
        strSQL.AppendLine(" Left Join Personal As P On O.BossCompID = P.CompID And O.Boss = P.EmpID ")
        strSQL.AppendLine(" Left Join Company C on O.LastChgComp = C.CompID ")
        strSQL.AppendLine(" Left Join Personal P1 on  O.CompID = P1.CompID AND O.LastChgID = P1.EmpID ")
        strSQL.AppendLine(" Where O.CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine(" And O.OrganID = " & Bsp.Utility.Quote(OrganID))
        strSQL.AppendLine(" And O.ValidDateEH <> '1900/01/01' ") '排除現任主管
        strSQL.AppendLine(" ORDER BY O.ValidDateBH")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

    '歷任主管紀錄查詢(現任功能)
    Public Function OM2000QueryOrganizationFlowBossLogNow(ByVal CompID As String, ByVal OrganID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT O.CompID ")
        strSQL.AppendLine(" , O.OrganID ")
        strSQL.AppendLine(" , ISNULL(O1.OrganName, '') As OrganName ")
        strSQL.AppendLine(" , O.Boss ")
        strSQL.AppendLine(" , ISNULL(P.NameN, '') As BossName ")
        strSQL.AppendLine(" , ValidDateBH = Case When Convert(Char(10), O.ValidDateBH, 111) = '1900/01/01' Then '' ELSE Convert(Char(10), O.ValidDateBH, 111) End ")
        strSQL.AppendLine(" , O.BossType")
        strSQL.AppendLine(" FROM OrganizationFlowBossLog As O")
        strSQL.AppendLine(" Left Join OrganizationFlow AS O1 On O.OrganID = O1.OrganID ")
        strSQL.AppendLine(" Left Join Personal As P On O.BossCompID = P.CompID And O.Boss = P.EmpID ")
        strSQL.AppendLine(" Where O.CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine(" And O.OrganID = " & Bsp.Utility.Quote(OrganID))
        strSQL.AppendLine(" And O.ValidDateEH = '1900/01/01'") '只有現任主管

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

    '歷任主管紀錄查詢(歷任功能)
    Public Function OM2000QueryOrganizationFlowBossPast(ByVal CompID As String, ByVal OrganID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT O.CompID ")
        strSQL.AppendLine(" , O.OrganID")
        strSQL.AppendLine(" , ValidDateBH = Case When Convert(Char(10), O.ValidDateBH, 111) = '1900/01/01' Then '' ELSE Convert(Char(10), O.ValidDateBH, 111) End ")
        strSQL.AppendLine(" , ValidDateEH = Case When Convert(Char(10), O.ValidDateEH, 111) = '1900/01/01' Then '' ELSE Convert(Char(10), O.ValidDateEH, 111) End ")
        strSQL.AppendLine(" , O.BossCompID ")
        strSQL.AppendLine(" , O.Boss ")
        strSQL.AppendLine(" , ISNULL(P.NameN, '') As BossName ")
        strSQL.AppendLine(" , BossType = Case O.BossType WHEN '1' THEN '主要' WHEN '2' THEN '兼任' END ")
        strSQL.AppendLine(" , ISNULL(C.CompName, '') As LastChgComp ")
        strSQL.AppendLine(" , ISNULL(P1.NameN, '') As LastChgID ")
        strSQL.AppendLine(" , LastChgDate = Case When Convert(Char(10), O.LastChgDate, 111) = '1900/01/01' Then '' ELSE Convert(Varchar, O.LastChgDate, 120) End ")
        strSQL.AppendLine(" FROM OrganizationFlowBossLog As O ")
        strSQL.AppendLine(" Left Join Personal As P On O.BossCompID = P.CompID And O.Boss = P.EmpID ")
        strSQL.AppendLine(" Left Join Company C on O.LastChgComp = C.CompID ")
        strSQL.AppendLine(" Left Join Personal P1 on O.LastChgComp = P1.CompID AND O.LastChgID = P1.EmpID ")
        strSQL.AppendLine(" Where O.CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine(" And O.OrganID = " & Bsp.Utility.Quote(OrganID))
        strSQL.AppendLine(" And O.ValidDateEH <> '1900/01/01'") '排除現任主管
        strSQL.AppendLine(" ORDER BY O.ValidDateBH")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

    '歷任主管記錄新增-查詢主管公司代碼
    Public Function GetCompName(ByVal CompID As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine("Select distinct CompName From Company ")
        strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(CompID))
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

    '歷任主管記錄「新增」-查詢有效起訖日區間是否有重複
    '行政
    Public Function OM2301CheckBossLogValidDate(ByVal hidCompID As String, ByVal hidOrganID As String, ByVal txtValidDateB As String, txtValidDateE As String) As Boolean
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT COUNT(*) FROM OrganizationBossLog ")
        strSQL.AppendLine(" WHERE CompID = " & Bsp.Utility.Quote(hidCompID) & " And OrganID = " & Bsp.Utility.Quote(hidOrganID))
        strSQL.AppendLine(" AND ((( " & Bsp.Utility.Quote(txtValidDateB) & " BETWEEN ValidDateBH AND ValidDateEH ) OR " & Bsp.Utility.Quote(txtValidDateB) & " IN (ValidDateBH) OR " & Bsp.Utility.Quote(txtValidDateB) & " IN (ValidDateEH)) ")
        strSQL.AppendLine(" OR (( " & Bsp.Utility.Quote(txtValidDateE) & " BETWEEN ValidDateBH AND ValidDateEH ) OR " & Bsp.Utility.Quote(txtValidDateE) & " IN (ValidDateBH) OR " & Bsp.Utility.Quote(txtValidDateE) & " IN (ValidDateEH)) ")
        strSQL.AppendLine(" OR " & Bsp.Utility.Quote(txtValidDateB) & " > (SELECT MAX(ValidDateBH) FROM OrganizationBossLog ")
        strSQL.AppendLine(" WHERE CompID = " & Bsp.Utility.Quote(hidCompID) & " And OrganID = " & Bsp.Utility.Quote(hidOrganID) & " ) ")
        strSQL.AppendLine(" OR " & Bsp.Utility.Quote(txtValidDateE) & " > (SELECT MAX(ValidDateBH) FROM OrganizationBossLog ")
        strSQL.AppendLine(" WHERE CompID = " & Bsp.Utility.Quote(hidCompID) & " And OrganID = " & Bsp.Utility.Quote(hidOrganID) & " ) ")
        strSQL.AppendLine(" OR ((ValidDateBH BETWEEN " & Bsp.Utility.Quote(txtValidDateB) & " AND " & Bsp.Utility.Quote(txtValidDateB) & ") OR ValidDateBH IN (" & Bsp.Utility.Quote(txtValidDateB) & ")  OR ValidDateBH IN (" & Bsp.Utility.Quote(txtValidDateE) & " )) ")
        strSQL.AppendLine(" OR ((ValidDateEH BETWEEN " & Bsp.Utility.Quote(txtValidDateB) & " AND " & Bsp.Utility.Quote(txtValidDateE) & ") OR ValidDateEH IN (" & Bsp.Utility.Quote(txtValidDateB) & ")  OR ValidDateBH IN (" & Bsp.Utility.Quote(txtValidDateE) & " )) ")
        strSQL.AppendLine(" ) ")
        Return IIf(Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB") > 0, True, False)
    End Function
    '功能
    Public Function OM2301CheckFlowBossLogValidDate(ByVal hidCompID As String, ByVal hidOrganID As String, ByVal txtValidDateB As DateTime, txtValidDateE As DateTime) As Boolean
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT COUNT(*) FROM OrganizationFlowBossLog ")
        strSQL.AppendLine(" WHERE CompID = " & Bsp.Utility.Quote(hidCompID) & " And OrganID = " & Bsp.Utility.Quote(hidOrganID))
        strSQL.AppendLine(" AND ((( " & Bsp.Utility.Quote(txtValidDateB) & " BETWEEN ValidDateBH AND ValidDateEH ) OR " & Bsp.Utility.Quote(txtValidDateB) & " IN (ValidDateBH) OR " & Bsp.Utility.Quote(txtValidDateB) & " IN (ValidDateEH)) ")
        strSQL.AppendLine(" OR (( " & Bsp.Utility.Quote(txtValidDateE) & " BETWEEN ValidDateBH AND ValidDateEH ) OR " & Bsp.Utility.Quote(txtValidDateE) & " IN (ValidDateBH) OR " & Bsp.Utility.Quote(txtValidDateE) & " IN (ValidDateEH)) ")
        strSQL.AppendLine(" OR " & Bsp.Utility.Quote(txtValidDateB) & " > (SELECT MAX(ValidDateBH) FROM OrganizationFlowBossLog ")
        strSQL.AppendLine(" WHERE CompID = " & Bsp.Utility.Quote(hidCompID) & " And OrganID = " & Bsp.Utility.Quote(hidOrganID) & " ) ")
        strSQL.AppendLine(" OR " & Bsp.Utility.Quote(txtValidDateE) & " > (SELECT MAX(ValidDateBH) FROM OrganizationFlowBossLog ")
        strSQL.AppendLine(" WHERE CompID = " & Bsp.Utility.Quote(hidCompID) & " And OrganID = " & Bsp.Utility.Quote(hidOrganID) & " ) ")
        strSQL.AppendLine(" OR ((ValidDateBH BETWEEN " & Bsp.Utility.Quote(txtValidDateB) & " AND " & Bsp.Utility.Quote(txtValidDateB) & ") OR ValidDateBH IN (" & Bsp.Utility.Quote(txtValidDateB) & ")  OR ValidDateBH IN (" & Bsp.Utility.Quote(txtValidDateE) & " )) ")
        strSQL.AppendLine(" OR ((ValidDateEH BETWEEN " & Bsp.Utility.Quote(txtValidDateB) & " AND " & Bsp.Utility.Quote(txtValidDateE) & ") OR ValidDateEH IN (" & Bsp.Utility.Quote(txtValidDateB) & ")  OR ValidDateBH IN (" & Bsp.Utility.Quote(txtValidDateE) & " )) ")
        strSQL.AppendLine(" ) ")
        Return IIf(Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB") > 0, True, False)
    End Function

    '歷任主管記錄「修改」
    '行政
    Public Function OM2302CheckBossLogValidDate(ByVal hidCompID As String, ByVal hidOrganID As String, ByVal txtValidDateB As DateTime, txtValidDateE As DateTime, ByVal hidValidDateBH As DateTime, ByVal hidValidDateEH As DateTime) As Boolean
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT COUNT(*) FROM OrganizationBossLog ")
        strSQL.AppendLine(" WHERE CompID = " & Bsp.Utility.Quote(hidCompID) & " And OrganID = " & Bsp.Utility.Quote(hidOrganID) & " AND " & Bsp.Utility.Quote(hidValidDateBH) & " NOT IN (ValidDateBH) AND " & Bsp.Utility.Quote(hidValidDateEH) & " NOT IN (ValidDateEH) ")
        strSQL.AppendLine(" AND ((( " & Bsp.Utility.Quote(txtValidDateB) & " BETWEEN ValidDateBH AND ValidDateEH ) OR " & Bsp.Utility.Quote(txtValidDateB) & " IN (ValidDateBH) OR " & Bsp.Utility.Quote(txtValidDateB) & " IN (ValidDateEH)) ")
        strSQL.AppendLine(" OR ((" & Bsp.Utility.Quote(txtValidDateE) & " BETWEEN ValidDateBH AND ValidDateEH ) OR " & Bsp.Utility.Quote(txtValidDateE) & " IN (ValidDateBH) OR " & Bsp.Utility.Quote(txtValidDateE) & " IN (ValidDateEH)) ")
        strSQL.AppendLine(" OR " & Bsp.Utility.Quote(txtValidDateB) & " > (SELECT MAX(ValidDateBH) FROM OrganizationBossLog ")
        strSQL.AppendLine(" WHERE CompID = " & Bsp.Utility.Quote(hidCompID) & " And OrganID = " & Bsp.Utility.Quote(hidOrganID) & ") ")
        strSQL.AppendLine(" OR " & Bsp.Utility.Quote(txtValidDateE) & " > (SELECT MAX(ValidDateBH) FROM OrganizationBossLog ")
        strSQL.AppendLine(" WHERE CompID = " & Bsp.Utility.Quote(hidCompID) & " And OrganID = " & Bsp.Utility.Quote(hidOrganID) & ") ")
        strSQL.AppendLine(" OR (( ValidDateBH BETWEEN " & Bsp.Utility.Quote(txtValidDateB) & " AND " & Bsp.Utility.Quote(txtValidDateB) & ") OR ValidDateBH IN (" & Bsp.Utility.Quote(txtValidDateB) & ") OR ValidDateBH IN (" & Bsp.Utility.Quote(txtValidDateE) & ")) ")
        strSQL.AppendLine(" OR (( ValidDateEH BETWEEN " & Bsp.Utility.Quote(txtValidDateB) & " AND " & Bsp.Utility.Quote(txtValidDateE) & ") OR ValidDateEH IN (" & Bsp.Utility.Quote(txtValidDateB) & ") OR ValidDateBH IN (" & Bsp.Utility.Quote(txtValidDateE) & ")) ")
        strSQL.AppendLine(" ) ")
        Return IIf(Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB") > 0, True, False)
    End Function
    '功能
    Public Function OM2302CheckFlowBossLogValidDate(ByVal hidCompID As String, ByVal hidOrganID As String, ByVal txtValidDateB As DateTime, txtValidDateE As DateTime, ByVal hidValidDateBH As DateTime, ByVal hidValidDateEH As DateTime) As Boolean
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT COUNT(*) FROM OrganizationFlowBossLog ")
        strSQL.AppendLine(" WHERE CompID = " & Bsp.Utility.Quote(hidCompID) & " And OrganID = " & Bsp.Utility.Quote(hidOrganID) & " AND " & Bsp.Utility.Quote(hidValidDateBH) & " NOT IN (ValidDateBH) AND " & Bsp.Utility.Quote(hidValidDateEH) & " NOT IN (ValidDateEH) ")
        strSQL.AppendLine(" AND ((( " & Bsp.Utility.Quote(txtValidDateB) & " BETWEEN ValidDateBH AND ValidDateEH ) OR " & Bsp.Utility.Quote(txtValidDateB) & " IN (ValidDateBH) OR " & Bsp.Utility.Quote(txtValidDateB) & " IN (ValidDateEH)) ")
        strSQL.AppendLine(" OR ((" & Bsp.Utility.Quote(txtValidDateE) & " BETWEEN ValidDateBH AND ValidDateEH ) OR " & Bsp.Utility.Quote(txtValidDateE) & " IN (ValidDateBH) OR " & Bsp.Utility.Quote(txtValidDateE) & " IN (ValidDateEH)) ")
        strSQL.AppendLine(" OR " & Bsp.Utility.Quote(txtValidDateB) & " > (SELECT MAX(ValidDateBH) FROM OrganizationFlowBossLog ")
        strSQL.AppendLine(" WHERE CompID = " & Bsp.Utility.Quote(hidCompID) & " And OrganID = " & Bsp.Utility.Quote(hidOrganID) & ") ")
        strSQL.AppendLine(" OR " & Bsp.Utility.Quote(txtValidDateE) & " > (SELECT MAX(ValidDateBH) FROM OrganizationFlowBossLog ")
        strSQL.AppendLine(" WHERE CompID = " & Bsp.Utility.Quote(hidCompID) & " And OrganID = " & Bsp.Utility.Quote(hidOrganID) & ") ")
        strSQL.AppendLine(" OR (( ValidDateBH BETWEEN " & Bsp.Utility.Quote(txtValidDateB) & " AND " & Bsp.Utility.Quote(txtValidDateB) & ") OR ValidDateBH IN (" & Bsp.Utility.Quote(txtValidDateB) & ") OR ValidDateBH IN (" & Bsp.Utility.Quote(txtValidDateE) & ")) ")
        strSQL.AppendLine(" OR (( ValidDateEH BETWEEN " & Bsp.Utility.Quote(txtValidDateB) & " AND " & Bsp.Utility.Quote(txtValidDateE) & ") OR ValidDateEH IN (" & Bsp.Utility.Quote(txtValidDateB) & ") OR ValidDateBH IN (" & Bsp.Utility.Quote(txtValidDateE) & ")) ")
        strSQL.AppendLine(" ) ")
        Return IIf(Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB") > 0, True, False)
    End Function

    '部門ReportLine(for 簽核組織)
    Public Function OM2000QueryOrganizationFlowReportLine(ByVal CompID As String, ByVal OrganID As String) As DataTable

        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("SELECT O.BossSeq ")
        strSQL.AppendLine(" , O.BossCompID ")
        strSQL.AppendLine(" , ISNULL(C.CompName, '') As CompName ")
        strSQL.AppendLine(" , O.Boss ")
        strSQL.AppendLine(" , ISNULL(P.NameN, '') AS BossName ")
        strSQL.AppendLine(" , O.ReportLineOrganID ")
        strSQL.AppendLine(" , ISNULL(OrgF.OrganName, '') As ReportLineOrganName ")
        strSQL.AppendLine(" FROM OrganizationFlowReportLine AS O ")
        strSQL.AppendLine(" Left Join Company AS C ON O.BossCompID = C.CompID ")
        strSQL.AppendLine(" Left Join Personal AS P ON O.BossCompID = P.CompID AND O.Boss = P.EmpID ")
        strSQL.AppendLine(" Left Join OrganizationFlow AS OrgF ON O.ReportLineOrganID = OrgF.OrganID ")
        strSQL.AppendLine(" Where O.CompID = " & Bsp.Utility.Quote(CompID.ToString()) & " And O.OrganID = " & Bsp.Utility.Quote(OrganID.ToString()))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

    '組織資料修改-行政組織查詢
    Public Function subGetData_OM2010Organization(ByVal CompID As String, ByVal OrganID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT O.CompID ") '公司代碼
        strSQL.AppendLine(" , ISNULL(C.CompName, '') ") '公司名稱(參照Company)
        strSQL.AppendLine(" , O.OrganID ") '部門代號
        strSQL.AppendLine(" , O.OrganName ") '部門名稱
        strSQL.AppendLine(" , O.OrganEngName ") '部門英文名稱
        strSQL.AppendLine(" , O.InValidFlag ") '無效註記
        strSQL.AppendLine(" , O.VirtualFlag ") '是否為虛擬部門
        strSQL.AppendLine(" , O.BranchFlag ") '分行註記
        strSQL.AppendLine(" , O.OrgType ") '單位類別
        strSQL.AppendLine(" , ISNULL(H.CodeCName, '') As OrgTypeName ") '單位類別名稱
        strSQL.AppendLine(" , O.GroupID ") '所屬事業群
        strSQL.AppendLine(" , ISNULL(OrgF.OrganName, '') As GroupName ") '所屬事業群名稱
        strSQL.AppendLine(" , O.UpOrganID ") '上階部門
        strSQL.AppendLine(" , ISNULL(O1.OrganName, '') As UpOrganName ") '上階部門名稱
        strSQL.AppendLine(" , O.GroupType ") '事業群類別
        strSQL.AppendLine(" , ISNULL(H1.CodeCName, '') As GroupTypeName ") '事業群類別名稱
        strSQL.AppendLine(" , O.DeptID ") '所屬一級部門
        strSQL.AppendLine(" , ISNULL(O2.OrganName, '') As DeptName ") '所屬一級部門名稱
        strSQL.AppendLine(" , O.RoleCode ") '部門主管角色
        strSQL.AppendLine(" , ISNULL(H2.CodeCName, '') As RoleCodeName ") '部門主管角色名稱(參照HRCodeMap)
        strSQL.AppendLine(" , O.BossCompID ") '部門主管公司代碼
        strSQL.AppendLine(" , ISNULL(C1.CompName, '') As BossCompName ") '部門主管公司名稱
        strSQL.AppendLine(" , O.Boss ") '部門主管員編
        strSQL.AppendLine(" , ISNULL(P1.NameN, '') AS BossName ") '部門主管姓名(參照Personal)
        strSQL.AppendLine(" , O.BossType ") '主管任用方式
        strSQL.AppendLine(" , O.SecBossCompID ") '副主管公司代碼
        strSQL.AppendLine(" , ISNULL(C2.CompName, '') As SecBossCompName ") '副部門主管公司名稱
        strSQL.AppendLine(" , O.SecBoss ") '副主管員編
        strSQL.AppendLine(" , ISNULL(P2.NameN, '') AS SecBossName ") '副主管姓名(參照Personal)
        strSQL.AppendLine(" , O.BossTemporary ") '主管暫代
        strSQL.AppendLine(" , O.PersonPart ") '第一人事管理員員編
        strSQL.AppendLine(" , ISNULL(P3.NameN, '') As PersonPartName ") '第一人事管理員姓名(參照Personal)
        strSQL.AppendLine(" , O.SecPersonPart ") '第二人事管理員員編(尚未擴編)
        strSQL.AppendLine(" , ISNULL(P4.NameN, '') As SecPersonPartName ") '第二人事管理員姓名(參照Personal)
        strSQL.AppendLine(" , O.WorkSiteID ") '工作地點(參照WorkSite→DDL下拉)
        strSQL.AppendLine(" , ISNULL(WS.Remark, '') As WorkSiteName ") '工作地點名稱(參照WorkSite→DDL下拉)
        strSQL.AppendLine(" , O.CheckPart ") '自行查核主管
        strSQL.AppendLine(" , ISNULL(P5.NameN, '') As CheckPartName ") '自行查核主管姓名(參照Personal)
        strSQL.AppendLine(" , O.CostDeptID ") '費用分攤部門
        strSQL.AppendLine(" , O.AccountBranch ") '會計分行別
        strSQL.AppendLine(" , O.CostType ") '費用分攤科目別
        strSQL.AppendLine(" , O.LastChgComp ") '最後異動公司
        strSQL.AppendLine(" , ISNULL(C3.CompName, '') As LastChgCompName ") '最後異動人員公司名稱
        strSQL.AppendLine(" , O.LastChgID ") '最後異動人員
        strSQL.AppendLine(" , ISNULL(P6.NameN, '') As LastChgName ") '最後異動人員名稱
        strSQL.AppendLine(" , O.LastChgDate ")
        strSQL.AppendLine(" FROM Organization AS O")
        '公司名稱
        strSQL.AppendLine(" Left Join Company AS C ON O.CompID = C.CompID")
        '單位類別名稱
        strSQL.AppendLine(" Left Join HRCodeMap AS H On H.TabName = 'Organization_OrgType' AND H.FldName =" & Bsp.Utility.Quote(CompID.ToString()) & "AND H.Code = O.OrgType AND H.NotShowFlag='0'") '20161104 Rebecca Update
        '所屬事業群名稱
        strSQL.AppendLine(" Left Join OrganizationFlow AS OrgF On O.GroupID = OrgF.OrganID")
        '上階部門名稱
        strSQL.AppendLine(" Left Join Organization AS O1 On  O.CompID = O1.CompID AND  O.UpOrganID = O1.OrganID ")
        '事業群類別名稱
        strSQL.AppendLine(" Left Join HRCodeMap AS H1 On H1.TabName = 'Organization' AND H1.FldName = 'GroupType' AND H1.Code = O.GroupType ")
        '所屬一級部門名稱
        strSQL.AppendLine(" Left Join Organization AS O2 On  O.CompID = O2.CompID AND  O.DeptID = O2.OrganID ")
        '部門主管角色名稱
        strSQL.AppendLine(" Left Join HRCodeMap AS H2 On H2.TabName = 'Organization' AND H2.FldName = 'RoleCode' AND H2.Code = O.RoleCode AND H2.NotShowFlag = '0' ") '20161104 Rebecca Update
        '部門主管公司名稱
        strSQL.AppendLine(" Left Join Company AS C1 ON O.BossCompID = C1.CompID")
        '部門主管姓名
        strSQL.AppendLine(" Left Join Personal AS P1 On O.BossCompID = P1.CompID AND O.Boss = P1.EmpID")
        '副部門主管公司名稱
        strSQL.AppendLine(" Left Join Company AS C2 ON O.SecBossCompID = C2.CompID")
        '副主管姓名
        strSQL.AppendLine(" Left Join Personal AS P2 On O.SecBossCompID = P2.CompID AND O.SecBoss = P2.EmpID")
        '第一人事管理員名稱
        strSQL.AppendLine(" Left Join Personal AS P3 On O.CompID = P3.CompID AND O.PersonPart = P3.EmpID")
        '第二人事管理員名稱
        strSQL.AppendLine(" Left Join Personal AS P4 On O.CompID = P4.CompID AND O.SecPersonPart = P4.EmpID")
        '自行查核主管名稱
        strSQL.AppendLine(" Left Join Personal AS P5 On O.CompID = P5.CompID AND O.CheckPart = P5.EmpID")
        '工作地點名稱
        strSQL.AppendLine(" Left Join WorkSite AS WS On O.CompID = WS.CompID AND O.WorkSiteID = WS.WorkSiteID")
        '最後異動公司名稱
        strSQL.AppendLine(" Left Join Company AS C3 ON O.LastChgComp = C3.CompID")
        '最後異動人員名稱
        strSQL.AppendLine(" Left Join Personal AS P6 On O.LastChgComp = P6.CompID AND O.LastChgID = P6.EmpID")

        strSQL.AppendLine(" Where O.CompID=" & Bsp.Utility.Quote(CompID.ToString()))
        strSQL.AppendLine(" And O.OrganID=" & Bsp.Utility.Quote(OrganID.ToString()))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

    '組織資料修改-功能組織查詢
    Public Function subGetData_OM2010OrganizationFlow(ByVal CompID As String, ByVal OrganID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT O.CompID, ") '公司代碼
        strSQL.AppendLine(" ISNULL(C.CompName, ''), ") '公司名稱(參照Company)
        strSQL.AppendLine(" O.OrganID, ") '部門代號
        strSQL.AppendLine(" O.OrganName, ") '部門名稱
        strSQL.AppendLine(" O.OrganEngName, ") '部門英文名稱
        strSQL.AppendLine(" O.InValidFlag, ") '無效註記
        strSQL.AppendLine(" O.VirtualFlag, ") '是否為虛擬部門
        strSQL.AppendLine(" O.BranchFlag, ") '分行註記
        strSQL.AppendLine(" O.OrgType, ") '單位類別
        strSQL.AppendLine(" ISNULL(H.CodeCName, '') As OrgTypeName, ") '單位類別名稱
        strSQL.AppendLine(" O.GroupID, ") '所屬事業群
        strSQL.AppendLine(" ISNULL(OrgF.OrganName, '') As GroupName, ") '所屬事業群名稱
        strSQL.AppendLine(" O.UpOrganID, ") '上階部門
        strSQL.AppendLine(" CASE WHEN O.InValidFlag = 0 THEN RTrim(ISNULL(O1.OrganName, '')) ELSE RTrim(ISNULL(O1.OrganName, '')) + '無效' END As UpOrganName, ") '上階部門名稱
        strSQL.AppendLine(" O.GroupType, ") '事業群類別
        strSQL.AppendLine(" ISNULL(H1.CodeCName, '') As GroupTypeName, ") '事業群類別名稱
        strSQL.AppendLine(" O.DeptID, ") '所屬一級部門
        strSQL.AppendLine(" CASE WHEN O.InValidFlag='0' THEN RTrim(ISNULL(O2.OrganName, '')) ELSE RTrim(ISNULL(O2.OrganName, '')) + '無效' END As DeptName, ") '所屬一級部門名稱
        strSQL.AppendLine(" O.RoleCode, ") '部門主管角色
        strSQL.AppendLine(" ISNULL(H2.CodeCName, '') As RoleCodeName, ") '部門主管角色名稱(參照HRCodeMap)
        strSQL.AppendLine(" O.BossCompID, ") '部門主管公司代碼
        strSQL.AppendLine(" ISNULL(C1.CompName, '') As BossCompName, ") '部門主管公司名稱
        strSQL.AppendLine(" O.Boss, ") '部門主管員編
        strSQL.AppendLine(" ISNULL(P1.NameN, '') AS BossName, ") '部門主管姓名(參照Personal)
        strSQL.AppendLine(" O.BossType, ") '主管任用方式
        strSQL.AppendLine(" O.SecBossCompID, ") '副主管公司代碼
        strSQL.AppendLine(" ISNULL(C2.CompName, '') As SecBossCompName, ") '副部門主管公司名稱
        strSQL.AppendLine(" O.SecBoss, ") '副主管員編
        strSQL.AppendLine(" P2.NameN AS SecBossName, ") '副主管姓名(參照Personal)
        strSQL.AppendLine(" O.BossTemporary, ") '主管暫代
        strSQL.AppendLine(" O.FlowOrganID, ") '比對簽核單位
        strSQL.AppendLine(" O.CompareFlag, ") 'HR內部比對單位註記
        strSQL.AppendLine(" O.DelegateFlag, ") '授權單位
        strSQL.AppendLine(" O.OrganNo, ") '處級單位註記
        strSQL.AppendLine(" O.BusinessType, ") '業務類別
        strSQL.AppendLine(" ISNULL(H3.CodeCName, '') As BusinessTypeName, ") '業務類別名稱
        strSQL.AppendLine(" O.LastChgComp, ") '最後異動公司
        strSQL.AppendLine(" ISNULL(C3.CompName, '') As LastChgCompName, ") '最後異動人員公司名稱
        strSQL.AppendLine(" O.LastChgID, ") '最後異動人員
        strSQL.AppendLine(" ISNULL(P6.NameN, '') As LastChgName, ") '最後異動人員名稱
        strSQL.AppendLine(" O.LastChgDate ")
        strSQL.AppendLine(" FROM OrganizationFlow AS O ")
        strSQL.AppendLine(" Left Join Company AS C ON O.CompID = C.CompID ")
        '單位類別
        strSQL.AppendLine(" Left Join HRCodeMap AS H On H.TabName = 'OrganizationFlow_OrgType' AND H.FldName =" & Bsp.Utility.Quote(CompID.ToString()) & "AND H.Code = O.OrgType AND H.NotShowFlag = '0' ") '20161104 Rebecca Update
        strSQL.AppendLine(" Left Join OrganizationFlow AS OrgF On O.GroupID = OrgF.OrganID")
        '上階部門
        strSQL.AppendLine(" Left Join OrganizationFlow AS O1 On O.UpOrganID = O1.OrganID ") '20161104 Rebecca Update
        strSQL.AppendLine(" Left Join HRCodeMap AS H1 On H1.TabName = 'Organization' AND H1.FldName = 'GroupType' AND O.GroupType = H1.Code ")
        '所屬一級部門
        strSQL.AppendLine(" Left Join OrganizationFlow AS O2 On O.DeptID = O2.OrganID ") '20161104 Rebecca Update
        '部門主管角色
        strSQL.AppendLine(" Left Join HRCodeMap AS H2 On H2.TabName = 'OrganizationFlow' AND H2.FldName = 'RoleCode' AND O.RoleCode = H2.Code AND H2.NotShowFlag='0' ") '20161104 Rebecca Update
        '部門主管
        strSQL.AppendLine(" Left Join Company AS C1 ON O.BossCompID = C1.CompID")
        strSQL.AppendLine(" Left Join Personal AS P1 On O.BossCompID = P1.CompID AND O.Boss = P1.EmpID")
        '副主管
        strSQL.AppendLine(" Left Join Company AS C2 ON O.SecBossCompID = C2.CompID")
        strSQL.AppendLine(" Left Join Personal AS P2 On O.SecBossCompID = P2.CompID AND O.SecBoss = P2.EmpID")
        '業務類別
        strSQL.AppendLine(" Left Join HRCodeMap AS H3 On H3.TabName = 'Business' AND H3.FldName = 'BusinessType' AND RTrim(O.BusinessType) = H3.Code")

        '最後異動公司名稱
        strSQL.AppendLine(" Left Join Company AS C3 ON O.LastChgComp = C3.CompID")
        '最後異動人員名稱
        strSQL.AppendLine(" Left Join Personal AS P6 ON O.LastChgComp = P6.CompID AND O.LastChgID = P6.EmpID")

        strSQL.AppendLine(" Where O.CompID=" & Bsp.Utility.Quote(CompID.ToString()))
        strSQL.AppendLine(" And O.OrganID=" & Bsp.Utility.Quote(OrganID.ToString()))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region
#Region "新增"
    Public Function OrganizationBossLogAdd(ByVal beOrganizationBossLog As beOrganizationBossLog.Row) As Boolean
        Dim bsOrganizationBossLog As New beOrganizationBossLog.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsOrganizationBossLog.Insert(beOrganizationBossLog, tran) = 0 Then Return False
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

    Public Function OrganizationFlowBossLogAdd(ByVal beOrganizationFlowBossLog As beOrganizationFlowBossLog.Row) As Boolean
        Dim bsOrganizationFlowBossLog As New beOrganizationFlowBossLog.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsOrganizationFlowBossLog.Insert(beOrganizationFlowBossLog, tran) = 0 Then Return False
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

    '組織異動記錄新增
    Public Function OrganizationLogInsert(ByVal beOrganizationLog As beOrganizationLog.Row) As Boolean
        Dim bsOrganizationLog As New beOrganizationLog.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Dim cmd As SqlCommand
            Try
                If bsOrganizationLog.Insert(beOrganizationLog, tran) = 0 Then Return False
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

    '取得組織異動記錄檔序號
    Public Function GetSeq(ByVal strFrom As String, ByVal CompID As String, ByVal OrganReason As String, ByVal OrganType As String, ByVal ValidDateB As String, ByVal OrganID As String) As String
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" Select ISNULL(MAX(Seq), 0)+1 From OrganizationLog " & strFrom)
        strSQL.AppendLine(" Where CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine(" And OrganReason = " & Bsp.Utility.Quote(OrganReason))
        strSQL.AppendLine(" And OrganType = " & Bsp.Utility.Quote(OrganType))
        strSQL.AppendLine(" And Convert(Char(10), ValidDateB, 111) = " & Bsp.Utility.Quote(ValidDateB))
        strSQL.AppendLine(" And OrganID = " & Bsp.Utility.Quote(OrganID))
        Return Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB").ToString()
    End Function

    Public Function OM2100CheckDuplicateData(ByVal CompID As String, ByVal OrganID As String, ByVal OrganReason As String, ByVal ValidDateB As String, ByVal OrganType As String, ByVal Seq As String) As Boolean
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" Select COUNT(*) From OrganizationLog ")
        strSQL.AppendLine(" Where CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine(" And OrganReason = " & Bsp.Utility.Quote(OrganReason))
        strSQL.AppendLine(" And OrganType = " & Bsp.Utility.Quote(OrganType))
        strSQL.AppendLine(" And Convert(Char(10), ValidDateB, 111) = " & Bsp.Utility.Quote(ValidDateB))
        strSQL.AppendLine(" And OrganID = " & Bsp.Utility.Quote(OrganID))

        Return IIf(Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB") > 0, False, True)
    End Function

#End Region

#Region "修改"
    Public Function OrganizationFlowUpdate(ByVal beOrganizationFlow As beOrganizationFlow.Row) As Boolean
        Dim bsOrganizationFlow As New beOrganizationFlow.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsOrganizationFlow.Update(beOrganizationFlow, tran) = 0 Then Return False
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

#Region "⑨Update" '組織修改-行政
    Public Function UpdateOWAddition(ByVal beOrganization As beOrganization.Row, ByVal CompID As String, ByVal OrganID As String, ByVal OrganType As String, ByVal PositionID() As String, ByVal WorkTypeID() As String) As Boolean
        Dim bsOrganization As New beOrganization.Service()

        Dim beOrgWorkType As New beOrgWorkType.Row()
        Dim bsOrgWorkType As New beOrgWorkType.Service()
        Dim beOrgPosition As New beOrgPosition.Row()
        Dim bsOrgPosition As New beOrgPosition.Service()

        Dim strSQL As New StringBuilder()

        'beOrgWorkType的gencode取得值
        With beOrgWorkType
            .CompID.Value = CompID
            .OrganID.Value = OrganID
            .WorkTypeID.Value = WorkTypeID(0)
            .PrincipalFlag.Value = "1" '預設第一筆主要(1)
            .LastChgComp.Value = UserProfile.ActCompID
            .LastChgID.Value = UserProfile.ActUserID
            .LastChgDate.Value = Now
        End With
        'beOrgPosition的gencode取得值
        With beOrgPosition
            .CompID.Value = CompID
            .OrganID.Value = OrganID
            .PositionID.Value = PositionID(0)
            .PrincipalFlag.Value = "1" '預設第一筆主要(1)
            .LastChgComp.Value = UserProfile.ActCompID
            .LastChgID.Value = UserProfile.ActUserID
            .LastChgDate.Value = Now
        End With


        '/*-----------------------------*/

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Try
                If bsOrganization.Update(beOrganization, tran) = 0 Then
                    Return False
                End If

                '%%%
                If OrganType = "1" Then '行政組織
                    strSQL.AppendLine(" DELETE FROM OrgWorkType ")
                    strSQL.AppendLine(" WHERE CompID = " & Bsp.Utility.Quote(CompID))
                    strSQL.AppendLine(" AND OrganID =" & Bsp.Utility.Quote(OrganID) & "; ")
                    Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran, "eHRMSDB")
                    strSQL.Clear()
                    strSQL.AppendLine(" DELETE FROM OrgPosition ")
                    strSQL.AppendLine(" WHERE CompID = " & Bsp.Utility.Quote(CompID))
                    strSQL.AppendLine(" AND OrganID =" & Bsp.Utility.Quote(OrganID) & "; ")
                    Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran, "eHRMSDB")

                    beOrgWorkType.WorkTypeID.Value = WorkTypeID(0)
                    beOrgPosition.PositionID.Value = PositionID(0)
                    bsOrgWorkType.Insert(beOrgWorkType, tran)
                    bsOrgPosition.Insert(beOrgPosition, tran)
                    beOrgWorkType.PrincipalFlag.Value = "0" '開始塞第二筆兼任(0)
                    beOrgPosition.PrincipalFlag.Value = "0" '開始塞第二筆兼任(0)
                    For ii = 1 To PositionID.Length - 1

                        beOrgPosition.PositionID.Value = PositionID(ii)

                        bsOrgPosition.Insert(beOrgPosition, tran)
                    Next
                    For ii = 1 To WorkTypeID.Length - 1
                        beOrgWorkType.WorkTypeID.Value = WorkTypeID(ii)
                        bsOrgWorkType.Insert(beOrgWorkType, tran)
                    Next
                ElseIf OrganType = "2" Then '功能組織

                End If
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

    '歷任主管記錄-修改(行政)
    Public Function OrganizationBossLogUpdate(ByVal hidCompID As String, ByVal hidOrganID As String, ByVal txtBossCompID As String, ByVal txtBoss As String, ByVal txtBossType As String, ByVal hidValidDateBH As String, ByVal txtValidDateBH As String, ByVal txtValidDateEH As String, ByVal txtLastChgComp As String, ByVal txtLastChgID As String, ByVal txtLastChgDate As String) As Boolean
        Dim strSQL As New StringBuilder()
        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                strSQL.AppendLine(" UPDATE OrganizationBossLog ")
                strSQL.AppendLine(" SET BossCompID=" & Bsp.Utility.Quote(txtBossCompID))
                strSQL.AppendLine(", Boss = " & Bsp.Utility.Quote(txtBoss))
                strSQL.AppendLine(", BossType = " & Bsp.Utility.Quote(txtBossType))
                strSQL.AppendLine(", ValidDateBH = " & Bsp.Utility.Quote(txtValidDateBH))
                strSQL.AppendLine(", ValidDateEH = " & Bsp.Utility.Quote(txtValidDateEH))
                strSQL.AppendLine(" WHERE CompID = " & Bsp.Utility.Quote(hidCompID))
                strSQL.AppendLine(" AND OrganID =" & Bsp.Utility.Quote(hidOrganID))
                strSQL.AppendLine(" AND ValidDateBH =" & Bsp.Utility.Quote(hidValidDateBH))
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
    '歷任主管記錄-修改(功能)
    Public Function OrganizationFlowBossLogUpdate(ByVal hidOrganID As String, ByVal txtBossCompID As String, ByVal txtBoss As String, ByVal txtBossType As String, ByVal hidValidDateBH As String, ByVal txtValidDateBH As String, ByVal txtValidDateEH As String, ByVal txtLastChgComp As String, ByVal txtLastChgID As String, ByVal txtLastChgDate As String) As Boolean
        Dim strSQL As New StringBuilder()
        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                strSQL.AppendLine(" UPDATE OrganizationFlowBossLog ")
                strSQL.AppendLine(" SET BossCompID=" & Bsp.Utility.Quote(txtBossCompID))
                strSQL.AppendLine(", Boss = " & Bsp.Utility.Quote(txtBoss))
                strSQL.AppendLine(", BossType = " & Bsp.Utility.Quote(txtBossType))
                strSQL.AppendLine(", ValidDateBH = " & Bsp.Utility.Quote(txtValidDateBH))
                strSQL.AppendLine(", ValidDateEH = " & Bsp.Utility.Quote(txtValidDateEH))
                strSQL.AppendLine(" WHERE OrganID =" & Bsp.Utility.Quote(hidOrganID))
                strSQL.AppendLine(" AND ValidDateBH =" & Bsp.Utility.Quote(hidValidDateBH))
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

#Region "Add"
    Public Function AddOWAddition(ByVal beOrganization As beOrganization.Row) As Boolean
        Dim bsOrganization As New beOrganization.Service()
        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                bsOrganization.Insert(beOrganization, tran)
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

    Public Function AddOWAddition(ByVal beOrganizationFlow As beOrganizationFlow.Row) As Boolean
        Dim bsOrganizationFlow As New beOrganizationFlow.Service()
        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                bsOrganizationFlow.Insert(beOrganizationFlow, tran)
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

#Region "職位與工作性質AddOWAddition"
    'beOrgWorkType
    Public Function OWAddition(ByVal beOrgWorkType As beOrgWorkType.Row) As Boolean
        Dim bsOrgWorkType As New beOrgWorkType.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Try
                bsOrgWorkType.Insert(beOrgWorkType, tran)
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
    'beOrgPosition
    Public Function OWAddition(ByVal beOrgPosition As beOrgPosition.Row) As Boolean
        Dim bsOrgPosition As New beOrgPosition.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Try
                bsOrgPosition.Insert(beOrgPosition, tran)
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

#Region "刪除"
    '刪除組織架構檔
    Public Function OM2000DeleteOrganization(ByVal beOrganization As beOrganization.Row) As Boolean
        Dim beOrganizationDelete As New beOrganization.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If beOrganizationDelete.DeleteRowByPrimaryKey(beOrganization, tran) = 0 Then Return False
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
    '刪除簽核流程組織架構檔
    Public Function OM2000DeleteOrganizationFlow(ByVal OrganizationFlow As beOrganizationFlow.Row) As Boolean
        Dim beOrganizationDelete As New beOrganizationFlow.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If beOrganizationDelete.DeleteRowByPrimaryKey(OrganizationFlow, tran) = 0 Then Return False
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

#Region "Position and WorkType Delete"
    Public Function PositionAndWorkTypeDelete(ByVal strFrom As String, ByVal CompID As String, ByVal OrganReason As String, ByVal OrganType As String, ByVal ValidDate As String, ByVal OrganID As String) As Boolean
        Dim strSQL As New StringBuilder()
        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Try
                strSQL.AppendLine(" Delete from " & strFrom)
                strSQL.AppendLine(" where CompID = " & Bsp.Utility.Quote(CompID))
                strSQL.AppendLine(" and OrganReason = " & Bsp.Utility.Quote(OrganReason))
                strSQL.AppendLine(" and OrganType = " & Bsp.Utility.Quote(OrganType))
                strSQL.AppendLine(" and ValidDate = " & Bsp.Utility.Quote(ValidDate))
                strSQL.AppendLine(" and OrganID =" & Bsp.Utility.Quote(OrganID) & "; ")
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

    '刪除歷來主管紀錄
    Public Function OM2000DeleteOrganizationBossLog(ByVal beOrganizationBossLog As beOrganizationBossLog.Row) As Boolean
        Dim bsOrganizationBossLog As New beOrganizationBossLog.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsOrganizationBossLog.DeleteRowByPrimaryKey(beOrganizationBossLog, tran) = 0 Then Return False
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

    '刪除歷來主管紀錄(簽核)
    Public Function OM2000DeleteOrganizationFlowBossLog(ByVal beOrganizationFlowBossLog As beOrganizationFlowBossLog.Row) As Boolean
        Dim bsOrganizationFlowBossLog As New beOrganizationFlowBossLog.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsOrganizationFlowBossLog.DeleteRowByPrimaryKey(beOrganizationFlowBossLog, tran) = 0 Then Return False
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

#Region "getUnionOrderBy"
    Public Function getUnionOrderByInfo(ByVal strTabName As String, ByVal strValue As String, ByVal strText As String, ByVal strOther As String, Optional ByVal JoinStr As String = "", Optional ByVal WhereStr As String = "", Optional ByVal OrderByStr As String = "") As DataTable
        Dim strSQL As New StringBuilder
        strSQL.AppendLine("Select " & strValue & " AS Code")
        If strText <> "" Then strSQL.AppendLine(", " & strText & " AS CodeName, " & strValue & " + '-' + " & strText & " AS FullName ")
        If strOther <> "" Then strSQL.AppendLine(", " & strOther & " ")
        strSQL.AppendLine("FROM " & strTabName)
        If JoinStr <> "" Then strSQL.AppendLine(JoinStr)
        strSQL.AppendLine("Where 1=1")
        If WhereStr <> "" Then strSQL.AppendLine(WhereStr)
        If OrderByStr <> "" Then
            strSQL.AppendLine(OrderByStr)
        Else
            strSQL.AppendLine("Order By " & strValue.Replace("distinct", ""))
        End If
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "eHRMSDB").Tables(0)
    End Function
    Public Shared Sub getUnionOrderBy(ByVal objDDL As DropDownList, ByVal strTabName As String, ByVal strValue As String, ByVal strText As String, ByVal strOther As String, Optional ByVal Type As DisplayType = DisplayType.Full, Optional ByVal JoinStr As String = "", Optional ByVal WhereStr As String = "", Optional ByVal OrderByStr As String = "")
        Dim objOM2 As New OM2()
        Try
            Using dt As DataTable = objOM2.getUnionOrderByInfo(strTabName, strValue, strText, strOther, JoinStr, WhereStr, OrderByStr)
                With objDDL
                    .DataSource = dt
                    Select Case Type
                        Case DisplayType.OnlyID
                            .DataTextField = "Code"
                        Case DisplayType.OnlyName
                            .DataTextField = "CodeName"
                        Case Else
                            .DataTextField = "FullName"
                    End Select
                    .DataValueField = "Code"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Sub
#End Region

#Region "下傳"
    '取得查詢結果(FOR行政下傳)
    Public Function QueryOrganizationByDownload(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()
        Dim strFieldNames As New StringBuilder()
        Dim aryStr() As String

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine("And O.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "OrganID"
                        strSQL.AppendLine("And O.OrganID like UPPER('%" + ht(strKey).ToString() + "%') ")
                    Case "OrganName"
                        strSQL.AppendLine("And O.OrganName Like N" & Bsp.Utility.Quote("%" + ht(strKey).ToString() + "%"))
                    Case "InValidFlag"
                        strSQL.AppendLine("And O.InValidFlag = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
            If Bsp.Utility.IsStringNull(ht("ValidDateB")) <> "" And Bsp.Utility.IsStringNull(ht("ValidDateE")) = "" Then
                strSQL.AppendLine(" And (O.ValidDateB >= " & Bsp.Utility.Quote(ht("ValidDateB").ToString()) & " Or O.ValidDateE >= " & Bsp.Utility.Quote(ht("ValidDateB").ToString()) & ") ")
            ElseIf Bsp.Utility.IsStringNull(ht("ValidDateB")) = "" And Bsp.Utility.IsStringNull(ht("ValidDateE")) <> "" Then
                strSQL.AppendLine(" And (O.ValidDateB <= " & Bsp.Utility.Quote(ht("ValidDateE").ToString()) & " Or O.ValidDateE <= " & Bsp.Utility.Quote(ht("ValidDateE").ToString()) & ") ")
            ElseIf Bsp.Utility.IsStringNull(ht("ValidDateB")) <> "" And Bsp.Utility.IsStringNull(ht("ValidDateE")) <> "" Then
                strSQL.AppendLine(" And ((O.ValidDateB >= " & Bsp.Utility.Quote(ht("ValidDateB").ToString()) & " And O.ValidDateB <= " & Bsp.Utility.Quote(ht("ValidDateE").ToString()) & ") Or (O.ValidDateE >= " & Bsp.Utility.Quote(ht("ValidDateB").ToString()) & " AND O.ValidDateE <= " & Bsp.Utility.Quote(ht("ValidDateE").ToString()) & ")) ")
            End If
        Next

        strFieldNames.AppendLine(" C1.CompName As '公司名稱', ")
        strFieldNames.AppendLine(" Case When Convert(Char(10), O.ValidDateB, 111) = '1900/01/01' Then '' Else Convert(Varchar, O.ValidDateB, 111) End As '生效日期', ")
        strFieldNames.AppendLine(" O.OrganID As '部門代號', ")
        strFieldNames.AppendLine(" O.OrganName As '部門名稱', ")
        strFieldNames.AppendLine(" O.OrganEngName As '部門英文名稱', ")
        strFieldNames.AppendLine(" Case O.InValidFlag WHEN '1' THEN '無效' WHEN '0' THEN '有效' END As '無效註記', ")
        strFieldNames.AppendLine(" Case O.VirtualFlag WHEN '1' THEN '虛擬' WHEN '0' THEN '一般' END As '虛擬部門', ")
        strFieldNames.AppendLine(" Case O.BranchFlag WHEN '1' THEN '是' WHEN '0' THEN '否' END As '分行註記', ")
        strFieldNames.AppendLine(" ISNULL(H.CodeCName, '') As '單位類別', ")
        strFieldNames.AppendLine(" ISNULL(OrgF.OrganName, '') As '所屬事業群', ")
        strFieldNames.AppendLine(" ISNULL(O1.OrganName, '') As '上階部門', ")
        strFieldNames.AppendLine(" ISNULL(H1.CodeCName, '') As '事業群類別', ")
        strFieldNames.AppendLine(" ISNULL(O2.OrganName, '') As '所屬一級部門', ")
        strFieldNames.AppendLine(" ISNULL(H2.CodeCName, '') As '部門主管角色', ")
        strFieldNames.AppendLine(" ISNULL(P1.NameN, '') As '部門主管', ")
        strFieldNames.AppendLine(" Case O.BossType WHEN '1' THEN '主要' WHEN '2' THEN '兼任' END As '主管任用方式', ")
        strFieldNames.AppendLine(" ISNULL(P2.NameN, '') As '副主管', ")
        strFieldNames.AppendLine(" CASE O.BossTemporary WHEN '1' THEN '是' WHEN '0' THEN '否' END As '主管暫代', ")
        strFieldNames.AppendLine(" ISNULL(P3.NameN, '') As '第一人事管理員', ")
        strFieldNames.AppendLine(" ISNULL(P4.NameN, '') As '第二人事管理員', ")
        strFieldNames.AppendLine(" ISNULL(WS.Remark, '') As '工作地點', ")
        strFieldNames.AppendLine(" ISNULL(P5.NameN, '') As '自行查核主管', ")
        strFieldNames.AppendLine(" (SELECT P.Remark  FROM OrgPosition AS OrgP LEFT JOIN Position P ON OrgP.PositionID = P.PositionID AND OrgP.CompID = P.CompID WHERE O.CompID = OrgP.CompID AND O.OrganID = OrgP.OrganID AND OrgP.PrincipalFlag = '1') AS '主要職位', ")
        strFieldNames.AppendLine(" STUFF((SELECT ',' + P.Remark FROM OrgPosition AS OrgP LEFT JOIN Position P ON OrgP.PositionID = P.PositionID AND OrgP.CompID = P.CompID WHERE O.CompID = OrgP.CompID AND O.OrganID = OrgP.OrganID AND OrgP.PrincipalFlag = '0' FOR XML PATH('')),1,1,'') AS '兼任職位', ")
        strFieldNames.AppendLine(" CASE O3.OrganName WHEN '' THEN C3.CompName ELSE O3.OrganName END AS '費用分攤部門', ")
        strFieldNames.AppendLine(" (SELECT WT.Remark  FROM OrgWorkType AS OWT LEFT JOIN WorkType WT ON OWT.WorkTypeID = WT.WorkTypeID AND OWT.CompID = WT.CompID WHERE O.CompID = OWT.CompID AND O.OrganID = OWT.OrganID AND OWT.PrincipalFlag='1') AS '主要工作性質', ")
        strFieldNames.AppendLine(" STUFF((SELECT ',' + WT.Remark FROM OrgWorkType AS OWT LEFT JOIN WorkType WT ON OWT.WorkTypeID = WT.WorkTypeID AND OWT.CompID = WT.CompID WHERE O.CompID = OWT.CompID AND O.OrganID = OWT.OrganID AND OWT.PrincipalFlag = '0' FOR XML PATH('')),1,1,'') AS '兼任工作性質', ")
        strFieldNames.AppendLine(" O.AccountBranch As '會計分行別', ")
        strFieldNames.AppendLine(" CASE O.CostType WHEN 'A' THEN '管理' WHEN 'B' THEN '業務' ELSE '' END As '費用分攤科目別', ")

        strFieldNames.AppendLine(" C2.CompName As '最後異動公司', ")
        strFieldNames.AppendLine(" P.NameN As '最後異動人員', ")
        strFieldNames.AppendLine(" CONVERT(char(19),O.LastChgDate,20) as '最後異動時間' ")

        Return GetOrganizationInfoByDownload(strFieldNames.ToString(), strSQL.ToString())
    End Function

    '查詢資料(FOR行政下傳)
    Public Function GetOrganizationInfoByDownload(Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames = "" Then FieldNames = "*"

        strSQL.AppendLine("Select " & FieldNames)
        strSQL.AppendLine("From Organization O")
        strSQL.AppendLine("Left Join Company C1 on O.CompID = C1.CompID ") '公司名稱
        '單位類別名稱
        strSQL.AppendLine(" Left Join HRCodeMap AS H On H.TabName = 'Organization_OrgType' AND H.FldName =" & Bsp.Utility.Quote(UserProfile.SelectCompRoleID) & "AND H.Code = O.OrgType ")
        '所屬事業群名稱
        strSQL.AppendLine(" Left Join OrganizationFlow AS OrgF On O.GroupID = OrgF.OrganID ")
        '上階部門名稱
        strSQL.AppendLine(" Left Join Organization AS O1 On O.CompID = O1.CompID AND O.UpOrganID = O1.OrganID ")
        '事業群類別名稱
        strSQL.AppendLine(" Left Join HRCodeMap AS H1 On H1.TabName = 'Organization' AND H1.FldName = 'GroupType' AND H1.Code = O.GroupType ")
        '所屬一級部門名稱
        strSQL.AppendLine(" Left Join Organization AS O2 On O.CompID = O2.CompID AND O.DeptID = O2.OrganID ")
        '部門主管角色名稱
        strSQL.AppendLine(" Left Join HRCodeMap AS H2 On H2.TabName = 'Organization' AND H2.FldName = 'RoleCode' AND H2.Code = O.RoleCode ")
        '部門主管姓名
        strSQL.AppendLine(" Left Join Personal AS P1 On O.BossCompID = P1.CompID AND O.Boss = P1.EmpID ")
        '副主管姓名
        strSQL.AppendLine(" Left Join Personal AS P2 On O.SecBossCompID = P2.CompID AND O.SecBoss = P2.EmpID ")
        '第一人事管理員名稱
        strSQL.AppendLine(" Left Join Personal AS P3 On O.CompID = P3.CompID AND O.PersonPart = P3.EmpID ")
        '第二人事管理員名稱
        strSQL.AppendLine(" Left Join Personal AS P4 On O.CompID = P4.CompID AND O.SecPersonPart = P4.EmpID ")
        '自行查核主管名稱
        strSQL.AppendLine(" Left Join Personal AS P5 On O.CompID = P5.CompID AND O.CheckPart = P5.EmpID ")
        '工作地點名稱
        strSQL.AppendLine(" Left Join WorkSite AS WS On O.CompID = WS.CompID AND O.WorkSiteID = WS.WorkSiteID ")
        '費用分攤部門
        strSQL.AppendLine(" Left Join Organization AS O3 ON O.CostDeptID = O3.OrganID AND O.CompID = O3.CompID ")
        strSQL.AppendLine(" Left Join Company AS C3 ON O.CostDeptID = C3.CompID AND C3.FeeShareFlag = '1' ")

        strSQL.AppendLine(" Left Join Company C2 on O.LastChgComp = C2.CompID ") '最後異動公司
        strSQL.AppendLine(" Left Join Personal P on O.LastChgComp = P.CompID AND O.LastChgID = P.EmpID ") '最後異動人員公司
        strSQL.AppendLine(" Where 1 = 1 ")
        If CondStr <> "" Then strSQL.AppendLine(CondStr)
        strSQL.AppendLine(" Order By O.InValidFlag, O.VirtualFlag, O.OrganID ")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

    '取得查詢結果(FOR功能下傳)
    Public Function QueryOrganizationFlowByDownload(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()
        Dim strFieldNames As New StringBuilder()
        Dim aryStr() As String

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine("And OrgF.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "OrganID"
                        strSQL.AppendLine("And OrgF.OrganID like UPPER('%" + ht(strKey).ToString() + "%') ")
                    Case "OrganName"
                        strSQL.AppendLine("And OrgF.OrganName Like N" & Bsp.Utility.Quote("%" + ht(strKey).ToString() + "%"))
                    Case "InValidFlag"
                        strSQL.AppendLine("And OrgF.InValidFlag = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "BusinessType" '功能組織
                        strSQL.AppendLine("And OrgF.BusinessType = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next
        If Bsp.Utility.IsStringNull(ht("ValidDateB")) <> "" And Bsp.Utility.IsStringNull(ht("ValidDateE")) = "" Then
            strSQL.AppendLine("And (OrgF.ValidDateB >= " & Bsp.Utility.Quote(ht("ValidDateB").ToString()) & " Or OrgF.ValidDateE >= " & Bsp.Utility.Quote(ht("ValidDateB").ToString()) & ")")
        ElseIf Bsp.Utility.IsStringNull(ht("ValidDateB")) = "" And Bsp.Utility.IsStringNull(ht("ValidDateE")) <> "" Then
            strSQL.AppendLine("And (OrgF.ValidDateB <= " & Bsp.Utility.Quote(ht("ValidDateE").ToString()) & " Or OrgF.ValidDateE <= " & Bsp.Utility.Quote(ht("ValidDateE").ToString()) & ")")
        ElseIf Bsp.Utility.IsStringNull(ht("ValidDateB")) <> "" And Bsp.Utility.IsStringNull(ht("ValidDateE")) <> "" Then
            strSQL.AppendLine("And ((OrgF.ValidDateB >= " & Bsp.Utility.Quote(ht("ValidDateB").ToString()) & " And OrgF.ValidDateB <= " & Bsp.Utility.Quote(ht("ValidDateE").ToString()) & ") Or (OrgF.ValidDateE >= " & Bsp.Utility.Quote(ht("ValidDateB").ToString()) & " AND OrgF.ValidDateE <= " & Bsp.Utility.Quote(ht("ValidDateE").ToString()) & "))")
        End If

        strFieldNames.AppendLine("C1.CompName As '公司名稱', ")
        strFieldNames.AppendLine(" Case When Convert(Char(10), OrgF.ValidDateB, 111) = '1900/01/01' Then '' Else Convert(Varchar, OrgF.ValidDateB, 111) End As '生效日期', ")
        strFieldNames.AppendLine(" OrgF.OrganID As '部門代號', ")
        strFieldNames.AppendLine(" OrgF.OrganName As '部門名稱', ")
        strFieldNames.AppendLine(" OrgF.OrganEngName As '部門英文名稱', ")
        strFieldNames.AppendLine(" Case OrgF.InValidFlag WHEN '1' THEN '無效' WHEN '0' THEN '有效' END As '無效註記', ")
        strFieldNames.AppendLine(" Case OrgF.VirtualFlag WHEN '1' THEN '虛擬' WHEN '0' THEN '一般' END As '虛擬部門', ")
        strFieldNames.AppendLine(" Case OrgF.BranchFlag WHEN '1' THEN '是' WHEN '0' THEN '否' END As '分行註記', ")
        strFieldNames.AppendLine(" ISNULL(H.CodeCName, '') As '單位類別', ")
        strFieldNames.AppendLine(" ISNULL(OrgF2.OrganName, '') As '所屬事業群', ")
        strFieldNames.AppendLine(" ISNULL(O1.OrganName, '') As '上階部門', ")
        strFieldNames.AppendLine(" ISNULL(H1.CodeCName, '') As '事業群類別', ")
        strFieldNames.AppendLine(" ISNULL(O2.OrganName, '') As '所屬一級部門', ")
        strFieldNames.AppendLine(" ISNULL(H2.CodeCName, '') As '部門主管角色', ")
        strFieldNames.AppendLine(" ISNULL(P1.NameN, '') As '部門主管', ")
        strFieldNames.AppendLine(" Case OrgF.BossType WHEN '1' THEN '主要' WHEN '2' THEN '兼任' END As '主管任用方式', ")
        strFieldNames.AppendLine(" ISNULL(P2.NameN, '') As '副主管', ")
        strFieldNames.AppendLine(" CASE OrgF.BossTemporary WHEN '1' THEN '是' WHEN '0' THEN '否' END As '主管暫代', ")
        strFieldNames.AppendLine(" OrgF.FlowOrganID As '比對簽核單位', ")
        strFieldNames.AppendLine(" CASE OrgF.CompareFlag WHEN '1' THEN '是' WHEN '0' THEN '否' END As 'HR內部比對單位註記', ")
        strFieldNames.AppendLine(" CASE OrgF.DelegateFlag WHEN '1' THEN '是' WHEN '0' THEN '否' END As '授權註記', ")
        strFieldNames.AppendLine(" CASE OrgF.OrganNo WHEN '1' THEN '是' WHEN '0' THEN '否' END As '處級單位註記', ")
        strFieldNames.AppendLine(" OrgF.BusinessType As '業務類別', ")

        strFieldNames.AppendLine(" C2.CompName As '最後異動公司', ")
        strFieldNames.AppendLine(" P.NameN As '最後異動人員', ")
        strFieldNames.AppendLine(" CONVERT(char(19),OrgF.LastChgDate,20) As '最後異動時間' ")

        Return GetOrganizationFlowInfoByDownload(strFieldNames.ToString(), strSQL.ToString())
    End Function

    '查詢資料(FOR功能下傳)
    Public Function GetOrganizationFlowInfoByDownload(Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames = "" Then FieldNames = "*"

        strSQL.AppendLine(" Select " & FieldNames)
        strSQL.AppendLine(" From OrganizationFlow OrgF ")
        strSQL.AppendLine(" Left Join Company C1 on OrgF.CompID = C1.CompID ")


        '單位類別名稱
        strSQL.AppendLine(" Left Join HRCodeMap AS H On H.TabName = 'OrganizationFlow_OrgType' AND H.FldName = " & Bsp.Utility.Quote(UserProfile.SelectCompRoleID) & " AND H.Code = OrgF.OrgType ")
        '所屬事業群名稱
        strSQL.AppendLine(" Left Join Organization AS OrgF2 On OrgF.GroupID = OrgF2.OrganID ")
        '上階部門名稱
        strSQL.AppendLine(" Left Join OrganizationFlow AS O1 On OrgF.UpOrganID = O1.OrganID ")
        '事業群類別名稱
        strSQL.AppendLine(" Left Join HRCodeMap AS H1 On H1.TabName = 'Organization' AND H1.FldName = 'GroupType' AND H1.Code = OrgF.GroupType ")
        '所屬一級部門名稱
        strSQL.AppendLine(" Left Join OrganizationFlow AS O2 On OrgF.DeptID = O2.OrganID ")
        '部門主管角色名稱
        strSQL.AppendLine(" Left Join HRCodeMap AS H2 On H2.TabName = 'OrganizationFlow' AND H2.FldName = 'RoleCode' AND H2.Code = OrgF.RoleCode")
        '部門主管姓名
        strSQL.AppendLine(" Left Join Personal AS P1 On OrgF.BossCompID = P1.CompID AND OrgF.Boss = P1.EmpID")
        '副主管姓名
        strSQL.AppendLine(" Left Join Personal AS P2 On OrgF.SecBossCompID = P2.CompID AND OrgF.SecBoss = P2.EmpID")
        '業務類別
        strSQL.AppendLine(" Left Join HRCodeMap As H3 On H3.TabName = 'Business' And H3.FldName = 'BusinessType' And OrgF.BusinessType = H3.Code ")
        strSQL.AppendLine(" Left Join Company C2 on OrgF.LastChgComp = C2.CompID ")
        strSQL.AppendLine(" Left Join Personal P on OrgF.LastChgComp = P.CompID And OrgF.LastChgID = P.EmpID ")
        strSQL.AppendLine("Where 1 = 1 ")
        If CondStr <> "" Then strSQL.AppendLine(CondStr)
        strSQL.AppendLine("Order By OrgF.InValidFlag, OrgF.VirtualFlag, OrgF.OrganID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)

    End Function
#End Region

    Public Function GetOrganFlowName(ByVal CompID As String, ByVal OrganID As String) As String
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select OrganName From OrganizationFlow")
        strSQL.AppendLine("Where 1=1")
        'strSQL.AppendLine("And CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine("And OrganID = " & Bsp.Utility.Quote(OrganID))

        If Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB") = Nothing Then
            Return ""
        Else
            Return Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")
        End If
    End Function

#Region "下拉選單"
    Enum DisplayType
        OnlyName    '只顯示名字
        OnlyID      '顯示ID  
        Full        '顯示ID + 名字
    End Enum

    Public Shared Sub FillDDL(ByVal objDDL As DropDownList, ByVal strTabName As String, ByVal strValue As String, ByVal strText As String, Optional ByVal Type As DisplayType = DisplayType.Full, Optional ByVal JoinStr As String = "", Optional ByVal WhereStr As String = "", Optional ByVal OrderByStr As String = "")
        Dim objPA4 As New PA4()
        Try
            Using dt As DataTable = objPA4.GetDDLInfo(strTabName, strValue, strText, JoinStr, WhereStr, OrderByStr)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    Select Case Type
                        Case DisplayType.OnlyID
                            .DataTextField = "Code"
                        Case DisplayType.OnlyName
                            .DataTextField = "CodeName"
                        Case Else
                            .DataTextField = "FullName"
                    End Select
                    .DataValueField = "Code"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Function GetDDLInfo(ByVal strTabName As String, ByVal strValue As String, ByVal strText As String, Optional ByVal JoinStr As String = "", Optional ByVal WhereStr As String = "", Optional ByVal OrderByStr As String = "") As DataTable
        Dim strSQL As New StringBuilder
        strSQL.AppendLine("Select " & strValue & " AS Code")
        If strText <> "" Then strSQL.AppendLine(", " & strText & " AS CodeName, " & strValue & " + '-' + " & strText & " AS FullName ")
        strSQL.AppendLine("FROM " & strTabName)
        If JoinStr <> "" Then strSQL.AppendLine(JoinStr)
        strSQL.AppendLine("Where 1=1")
        If WhereStr <> "" Then strSQL.AppendLine(WhereStr)
        If OrderByStr <> "" Then
            strSQL.AppendLine(OrderByStr)
        Else
            strSQL.AppendLine("Order By " & strValue.Replace("distinct", ""))
        End If
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "eHRMSDB").Tables(0)
    End Function

    Public Shared Sub FillDDLOM2000(ByVal objDDL As DropDownList, ByVal strTabName As String, ByVal strValue As String, ByVal strText As String, ByVal str3rdOrder As String, Optional ByVal Type As DisplayType = DisplayType.Full, Optional ByVal JoinStr As String = "", Optional ByVal WhereStr As String = "", Optional ByVal OrderByStr As String = "")
        Dim objOM2 As New OM2()
        Try
            Using dt As DataTable = objOM2.GetDDLInfoOM2000(strTabName, strValue, strText, str3rdOrder, JoinStr, WhereStr, OrderByStr)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    Select Case Type
                        Case DisplayType.OnlyID
                            .DataTextField = "Code"
                        Case DisplayType.OnlyName
                            .DataTextField = "CodeName"
                        Case Else
                            .DataTextField = "FullName"
                    End Select
                    .DataValueField = "Code"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetDDLInfoOM2000(ByVal strTabName As String, ByVal strValue As String, ByVal strText As String, ByVal str3rdOrder As String, Optional ByVal JoinStr As String = "", Optional ByVal WhereStr As String = "", Optional ByVal OrderByStr As String = "") As DataTable
        Dim strSQL As New StringBuilder
        strSQL.AppendLine("Select distinct" & strValue & " AS Code")
        If strText <> "" Then strSQL.AppendLine(", " & strText & " AS CodeName, " & strValue & " + '-' + " & strText & " AS FullName ")
        If str3rdOrder <> "" Then strSQL.AppendLine(", " & str3rdOrder)
        strSQL.AppendLine("FROM " & strTabName)
        If JoinStr <> "" Then strSQL.AppendLine(JoinStr)
        strSQL.AppendLine("Where 1=1")
        If WhereStr <> "" Then strSQL.AppendLine(WhereStr)
        If OrderByStr <> "" Then
            strSQL.AppendLine(OrderByStr)
        Else
            strSQL.AppendLine("Order By " & strValue.Replace("distinct", ""))
        End If
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "eHRMSDB").Tables(0)
    End Function
#End Region

#End Region
End Class
