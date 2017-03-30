'****************************************************
'功能說明：管理分析-Web參數設定
'建立人員：BeatriceCheng
'建立日期：2015/05/08
'****************************************************
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.Common

Public Class PA4

#Region "PA4100 Web人員資料查詢-VIP"
#Region "查詢Web人員資料-VIPParameter"
    Public Function VIPPQuery(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select S.CompID, IsNull(C.CompName,'') As CompName")
        strSQL.AppendLine(", S.EmpID, IsNull(P.NameN, '') As Name")
        strSQL.AppendLine(", GrantFlag")
        strSQL.AppendLine(", Case S.GrantFlag When '0' Then '排除授權' When '1' Then '授權' ELSE '' End As GrantFlagName")
        strSQL.AppendLine(", S.UseCompID, IsNull(C1.CompName,'全選') As UseCompName")
        strSQL.AppendLine(", S.UseGroupID, IsNull(O.OrganName,'全選') As UseGroupName")
        strSQL.AppendLine(", S.UseOrganID, IsNull(O1.OrganName,'全選') As UseOrganName")
        strSQL.AppendLine(", S.UseOurColleagues")
        strSQL.AppendLine(", S.UseRankID")
        strSQL.AppendLine(", BeginDate = Case When Convert(Char(10), S.BeginDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, S.BeginDate, 111) End")
        strSQL.AppendLine(", EndDate = Case When Convert(Char(10), S.EndDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, S.EndDate, 111) End")
        strSQL.AppendLine(", S.LastChgComp, IsNull(C2.CompName, '') As LastChgCompName")
        strSQL.AppendLine(", S.LastChgID, IsNull(P1.NameN, '') as LastChgName")
        strSQL.AppendLine(", LastChgDate = Case When Convert(Char(10), S.LastChgDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, S.LastChgDate, 120) End")
        strSQL.AppendLine("From VIPParameter S")
        strSQL.AppendLine("Left Join Personal P on S.EmpID = P.EmpID and S.CompID = P.CompID")
        strSQL.AppendLine("Left Join Personal P1 on S.LastChgID = P1.EmpID and S.LastChgComp = P1.CompID")
        strSQL.AppendLine("Left Join Company C on S.CompID = C.CompID")
        strSQL.AppendLine("Left Join Company C1 on S.UseCompID = C1.CompID")
        strSQL.AppendLine("Left Join Company C2 on S.LastChgComp = C2.CompID")
        strSQL.AppendLine("Left Join OrganizationFlow O on S.UseGroupID = O.OrganID")
        strSQL.AppendLine("Left Join Organization O1 on S.UseCompID = O1.CompID and S.UseOrganID = O1.OrganID")
        strSQL.AppendLine("Where 1 = 1")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine("And S.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "EmpID"
                        strSQL.AppendLine("And S.EmpID like '" & ht(strKey).ToString() & "%'")
                    Case "Name"
                        strSQL.AppendLine("And P.NameN like N'%" & ht(strKey).ToString() & "%'")
                    Case "AllCompIDFlag"
                        strSQL.AppendLine("And S.AllCompIDFlag = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "AllGroupIDFlag"
                        strSQL.AppendLine("And S.AllGroupIDFlag = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "AllOrganIDFlag"
                        strSQL.AppendLine("And S.AllOrganIDFlag = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "UseCompID"
                        strSQL.AppendLine("And S.UseCompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "UseGroupID"
                        strSQL.AppendLine("And S.UseGroupID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "UseOrganID"
                        strSQL.AppendLine("And S.UseOrganID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "GrantFlag"
                        strSQL.AppendLine("And S.GrantFlag = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next

        strSQL.AppendLine("Order by S.EmpID, S.UseCompID, S.UseGroupID, S.UseOrganID, S.UseRankID")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

    Public Function VIPPFlowQuery(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select S.CompID, IsNull(C.CompName,'') As CompName")
        strSQL.AppendLine(", S.EmpID, IsNull(P.NameN, '') As Name")
        strSQL.AppendLine(", GrantFlag")
        strSQL.AppendLine(", Case S.GrantFlag When '0' Then '排除授權' When '1' Then '授權' ELSE '' End As GrantFlagName")
        strSQL.AppendLine(", S.UseCompID, IsNull(C1.CompName,'全選') As UseCompName")
        strSQL.AppendLine(", S.UseBusinessType As UseGroupID, IsNull(H.CodeCName,'全選') As UseGroupName")
        strSQL.AppendLine(", S.UseOrganID, IsNull(O1.OrganName,'全選') As UseOrganName")
        strSQL.AppendLine(", S.UseOurColleagues")
        strSQL.AppendLine(", '' UseRankID")
        strSQL.AppendLine(", BeginDate = Case When Convert(Char(10), S.BeginDate, 111) = '1900/01/01' Then '' ELSE Convert(Varchar, S.BeginDate, 111) End")
        strSQL.AppendLine(", EndDate = Case When Convert(Char(10), S.EndDate, 111) = '1900/01/01' Then '' ELSE Convert(Varchar, S.EndDate, 111) End")
        strSQL.AppendLine(", S.LastChgComp, IsNull(C2.CompName, '') As LastChgCompName")
        strSQL.AppendLine(", S.LastChgID, IsNull(P1.NameN, '') as LastChgName")
        strSQL.AppendLine(", LastChgDate = Case When Convert(Char(10), S.LastChgDate, 111) = '1900/01/01' Then '' ELSE Convert(Varchar, S.LastChgDate, 120) End")
        strSQL.AppendLine("From VIPParameterFlow S")
        strSQL.AppendLine("Left Join Personal P on S.EmpID = P.EmpID and S.CompID = P.CompID")
        strSQL.AppendLine("Left Join Personal P1 on S.LastChgID = P1.EmpID and S.LastChgComp = P1.CompID")
        strSQL.AppendLine("Left Join Company C on S.CompID = C.CompID")
        strSQL.AppendLine("Left Join Company C1 on S.UseCompID = C1.CompID")
        strSQL.AppendLine("Left Join Company C2 on S.LastChgComp = C2.CompID")
        strSQL.AppendLine("Left Join HRCodeMap H on S.UseBusinessType = H.Code And H.TabName = 'Business' And H.FldName = 'BusinessType'")
        strSQL.AppendLine("Left Join OrganizationFlow O1 on S.UseCompID = O1.CompID and S.UseOrganID = O1.OrganID")
        strSQL.AppendLine("Where 1 = 1")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine("And S.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "EmpID"
                        strSQL.AppendLine("And S.EmpID like '" & ht(strKey).ToString() & "%'")
                    Case "Name"
                        strSQL.AppendLine("And P.NameN like N'%" & ht(strKey).ToString() & "%'")
                    Case "AllBusinessType"
                        strSQL.AppendLine("And S.AllBusinessType = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "AllOrganIDFlag"
                        strSQL.AppendLine("And S.AllOrganIDFlag = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "UseCompID"
                        strSQL.AppendLine("And S.UseCompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "UseBusinessType"
                        strSQL.AppendLine("And S.UseBusinessType = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "UseOrganID"
                        strSQL.AppendLine("And S.UseOrganID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "GrantFlag"
                        strSQL.AppendLine("And S.GrantFlag = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next

        strSQL.AppendLine("Order by S.EmpID, S.UseCompID, S.UseBusinessType, S.UseOrganID")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "查詢Web人員資料-VIP"
    Public Function VIPQuery(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select S.CompID, IsNull(C.CompName,'') As CompName")
        strSQL.AppendLine(", S.EmpID, IsNull(P.NameN, '') As Name")
        strSQL.AppendLine(", IsNull(C1.CompName,'') As UseCompName")
        strSQL.AppendLine(", IsNull(O.OrganName,'') As UseGroupName")
        strSQL.AppendLine(", IsNull(O1.OrganName,'') As UseOrganName")
        strSQL.AppendLine(", S.UseOurColleagues")
        strSQL.AppendLine(", S.UseRankID")
        strSQL.AppendLine(", BeginDate = Case When Convert(Char(10), S.BeginDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, S.BeginDate, 111) End")
        strSQL.AppendLine(", EndDate = Case When Convert(Char(10), S.EndDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, S.EndDate, 111) End")
        strSQL.AppendLine(", S.LastChgComp, IsNull(C2.CompName, '') As LastChgCompName")
        strSQL.AppendLine(", S.LastChgID, IsNull(P1.NameN, '') as LastChgName")
        strSQL.AppendLine(", LastChgDate = Case When Convert(Char(10), S.LastChgDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, S.LastChgDate, 120) End")
        strSQL.AppendLine("From VIP S")
        strSQL.AppendLine("Left Join Personal P on S.EmpID = P.EmpID and S.CompID = P.CompID")
        strSQL.AppendLine("Left Join Personal P1 on S.LastChgID = P1.EmpID and S.LastChgComp = P1.CompID")
        strSQL.AppendLine("Left Join Company C on S.CompID = C.CompID")
        strSQL.AppendLine("Left Join Company C1 on S.UseCompID = C1.CompID")
        strSQL.AppendLine("Left Join Company C2 on S.LastChgComp = C2.CompID")
        strSQL.AppendLine("Left Join OrganizationFlow O on S.UseGroupID = O.OrganID")
        strSQL.AppendLine("Left Join Organization O1 on S.UseCompID = O1.CompID and S.UseOrganID = O1.OrganID")
        strSQL.AppendLine("Where 1 = 1")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine("And S.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "EmpID"
                        strSQL.AppendLine("And S.EmpID like N'" & ht(strKey).ToString() & "%'")
                    Case "Name"
                        strSQL.AppendLine("And P.NameN like N'%" & ht(strKey).ToString() & "%'")
                    Case "UseCompID"
                        strSQL.AppendLine("And S.UseCompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "UseGroupID"
                        strSQL.AppendLine("And S.UseGroupID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "UseOrganID"
                        strSQL.AppendLine("And S.UseOrganID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next

        strSQL.AppendLine("Order by S.EmpID, S.UseCompID, S.UseGroupID, S.UseOrganID, S.UseRankID")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

    Public Function VIPFlowQuery(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select S.CompID, IsNull(C.CompName,'') As CompName")
        strSQL.AppendLine(", S.EmpID, IsNull(P.NameN, '') As Name")
        strSQL.AppendLine(", IsNull(C1.CompName,'') As UseCompName")
        strSQL.AppendLine(", IsNull(H.CodeCName,'') As UseBusinessType")
        strSQL.AppendLine(", IsNull(O1.OrganName,'') As UseOrganName")
        strSQL.AppendLine(", S.UseOurColleagues")
        strSQL.AppendLine(", BeginDate = Case When Convert(Char(10), S.BeginDate, 111) = '1900/01/01' Then '' ELSE Convert(Varchar, S.BeginDate, 111) End")
        strSQL.AppendLine(", EndDate = Case When Convert(Char(10), S.EndDate, 111) = '1900/01/01' Then '' ELSE Convert(Varchar, S.EndDate, 111) End")
        strSQL.AppendLine(", S.LastChgComp, IsNull(C2.CompName, '') As LastChgCompName")
        strSQL.AppendLine(", S.LastChgID, IsNull(P1.NameN, '') as LastChgName")
        strSQL.AppendLine(", LastChgDate = Case When Convert(Char(10), S.LastChgDate, 111) = '1900/01/01' Then '' ELSE Convert(Varchar, S.LastChgDate, 120) End")
        strSQL.AppendLine("From VIPFlow S")
        strSQL.AppendLine("Left Join Personal P on S.EmpID = P.EmpID and S.CompID = P.CompID")
        strSQL.AppendLine("Left Join Personal P1 on S.LastChgID = P1.EmpID and S.LastChgComp = P1.CompID")
        strSQL.AppendLine("Left Join Company C on S.CompID = C.CompID")
        strSQL.AppendLine("Left Join Company C1 on S.UseCompID = C1.CompID")
        strSQL.AppendLine("Left Join Company C2 on S.LastChgComp = C2.CompID")
        strSQL.AppendLine("Left Join HRCodeMap H on S.UseBusinessType = H.Code And H.TabName = 'Business' And H.FldName = 'BusinessType'")
        strSQL.AppendLine("Left Join OrganizationFlow O1 on S.UseCompID = O1.CompID and S.UseOrganID = O1.OrganID")
        strSQL.AppendLine("Where 1 = 1")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine("And S.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "EmpID"
                        strSQL.AppendLine("And S.EmpID like N'" & ht(strKey).ToString() & "%'")
                    Case "Name"
                        strSQL.AppendLine("And P.NameN like N'%" & ht(strKey).ToString() & "%'")
                    Case "UseCompID"
                        strSQL.AppendLine("And S.UseCompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "UseBusinessType"
                        strSQL.AppendLine("And S.UseBusinessType = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "UseOrganID"
                        strSQL.AppendLine("And S.UseOrganID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next

        strSQL.AppendLine("Order by S.EmpID, S.UseCompID, S.UseBusinessType, S.UseOrganID")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "新增Web人員資料-VIP"
    Public Function VIPAdd(ByVal beVIPParameter() As beVIPParameter.Row) As Boolean
        Dim bsVIPParameter As New beVIPParameter.Service()
        Dim strSQL As New StringBuilder()
        Dim strWhere As String = ""
        Dim strSelect As String = "Select * From VIPParameter"

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                For Each VIPParameter In beVIPParameter
                    If VIPParameter.AllCompIDFlag.Value = "1" And VIPParameter.AllGroupIDFlag.Value = "1" And VIPParameter.AllOrganIDFlag.Value = "1" Then
                        strSQL.AppendLine("Delete VIPParameter")
                        strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(VIPParameter.CompID.Value))
                        strSQL.AppendLine("And EmpID = " & Bsp.Utility.Quote(VIPParameter.EmpID.Value))
                        strSQL.AppendLine("And GrantFlag = " & Bsp.Utility.Quote(VIPParameter.GrantFlag.Value))
                        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran, "eHRMSDB")

                    ElseIf VIPParameter.AllCompIDFlag.Value = "0" And VIPParameter.AllGroupIDFlag.Value = "1" And VIPParameter.AllOrganIDFlag.Value = "1" Then
                        strWhere = " Where CompID = " & Bsp.Utility.Quote(VIPParameter.CompID.Value)
                        strWhere += " And EmpID = " & Bsp.Utility.Quote(VIPParameter.EmpID.Value)
                        strWhere += " And GrantFlag = " & Bsp.Utility.Quote(VIPParameter.GrantFlag.Value)
                        strWhere += " And AllCompIDFlag = '1' And AllGroupIDFlag = '1' And AllOrganIDFlag = '1'"

                        If Bsp.DB.ExecuteDataSet(CommandType.Text, strSelect & strWhere, tran, "eHRMSDB").Tables(0).Rows.Count > 0 Then
                            strSQL.AppendLine("Delete VIPParameter")
                            strSQL.AppendLine(strWhere)
                            Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran, "eHRMSDB")
                        Else
                            strSQL.AppendLine("Delete VIPParameter")
                            strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(VIPParameter.CompID.Value))
                            strSQL.AppendLine("And EmpID = " & Bsp.Utility.Quote(VIPParameter.EmpID.Value))
                            strSQL.AppendLine("And GrantFlag = " & Bsp.Utility.Quote(VIPParameter.GrantFlag.Value))
                            strSQL.AppendLine("And UseCompID = " & Bsp.Utility.Quote(VIPParameter.UseCompID.Value))
                            Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran, "eHRMSDB")
                        End If

                    ElseIf VIPParameter.AllCompIDFlag.Value = "0" And VIPParameter.AllGroupIDFlag.Value = "0" And VIPParameter.AllOrganIDFlag.Value = "1" Then
                        strWhere = " Where CompID = " & Bsp.Utility.Quote(VIPParameter.CompID.Value)
                        strWhere += " And EmpID = " & Bsp.Utility.Quote(VIPParameter.EmpID.Value)
                        strWhere += " And GrantFlag = " & Bsp.Utility.Quote(VIPParameter.GrantFlag.Value)
                        strWhere += " And AllCompIDFlag = '1' And AllGroupIDFlag = '1' And AllOrganIDFlag = '1'"

                        If Bsp.DB.ExecuteDataSet(CommandType.Text, strSelect & strWhere, tran, "eHRMSDB").Tables(0).Rows.Count > 0 Then
                            strSQL.AppendLine("Delete VIPParameter")
                            strSQL.AppendLine(strWhere)
                            Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran, "eHRMSDB")
                        Else
                            strWhere = " Where CompID = " & Bsp.Utility.Quote(VIPParameter.CompID.Value)
                            strWhere += " And EmpID = " & Bsp.Utility.Quote(VIPParameter.EmpID.Value)
                            strWhere += " And GrantFlag = " & Bsp.Utility.Quote(VIPParameter.GrantFlag.Value)
                            strWhere += " And UseCompID = " & Bsp.Utility.Quote(VIPParameter.UseCompID.Value)
                            strWhere += " And AllCompIDFlag = '0' And AllGroupIDFlag = '1' And AllOrganIDFlag = '1'"

                            If Bsp.DB.ExecuteDataSet(CommandType.Text, strSelect & strWhere, tran, "eHRMSDB").Tables(0).Rows.Count > 0 Then
                                strSQL.AppendLine("Delete VIPParameter")
                                strSQL.AppendLine(strWhere)
                                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran, "eHRMSDB")
                            Else
                                strSQL.AppendLine("Delete VIPParameter")
                                strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(VIPParameter.CompID.Value))
                                strSQL.AppendLine("And EmpID = " & Bsp.Utility.Quote(VIPParameter.EmpID.Value))
                                strSQL.AppendLine("And GrantFlag = " & Bsp.Utility.Quote(VIPParameter.GrantFlag.Value))
                                strSQL.AppendLine("And UseCompID = " & Bsp.Utility.Quote(VIPParameter.UseCompID.Value))
                                strSQL.AppendLine("And UseGroupID = " & Bsp.Utility.Quote(VIPParameter.UseGroupID.Value))
                                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran, "eHRMSDB")
                            End If
                        End If

                    ElseIf VIPParameter.AllCompIDFlag.Value = "0" And VIPParameter.AllGroupIDFlag.Value = "0" And VIPParameter.AllOrganIDFlag.Value = "0" Then
                        strWhere = " Where CompID = " & Bsp.Utility.Quote(VIPParameter.CompID.Value)
                        strWhere += " And EmpID = " & Bsp.Utility.Quote(VIPParameter.EmpID.Value)
                        strWhere += " And GrantFlag = " & Bsp.Utility.Quote(VIPParameter.GrantFlag.Value)
                        strWhere += " And AllCompIDFlag = '1' And AllGroupIDFlag = '1' And AllOrganIDFlag = '1'"

                        If Bsp.DB.ExecuteDataSet(CommandType.Text, strSelect & strWhere, tran, "eHRMSDB").Tables(0).Rows.Count > 0 Then
                            strSQL.AppendLine("Delete VIPParameter")
                            strSQL.AppendLine(strWhere)
                            Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran, "eHRMSDB")
                        Else
                            strWhere = " Where CompID = " & Bsp.Utility.Quote(VIPParameter.CompID.Value)
                            strWhere += " And EmpID = " & Bsp.Utility.Quote(VIPParameter.EmpID.Value)
                            strWhere += " And GrantFlag = " & Bsp.Utility.Quote(VIPParameter.GrantFlag.Value)
                            strWhere += " And UseCompID = " & Bsp.Utility.Quote(VIPParameter.UseCompID.Value)
                            strWhere += " And AllCompIDFlag = '0' And AllGroupIDFlag = '1' And AllOrganIDFlag = '1'"

                            If Bsp.DB.ExecuteDataSet(CommandType.Text, strSelect & strWhere, tran, "eHRMSDB").Tables(0).Rows.Count > 0 Then
                                strSQL.AppendLine("Delete VIPParameter")
                                strSQL.AppendLine(strWhere)
                                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran, "eHRMSDB")
                            Else
                                strWhere = " Where CompID = " & Bsp.Utility.Quote(VIPParameter.CompID.Value)
                                strWhere += " And EmpID = " & Bsp.Utility.Quote(VIPParameter.EmpID.Value)
                                strWhere += " And GrantFlag = " & Bsp.Utility.Quote(VIPParameter.GrantFlag.Value)
                                strWhere += " And UseCompID = " & Bsp.Utility.Quote(VIPParameter.UseCompID.Value)
                                strWhere += " And UseGroupID = " & Bsp.Utility.Quote(VIPParameter.UseGroupID.Value)
                                strWhere += " And AllCompIDFlag = '0' And AllGroupIDFlag = '0' And AllOrganIDFlag = '1'"

                                If Bsp.DB.ExecuteDataSet(CommandType.Text, strSelect & strWhere, tran, "eHRMSDB").Tables(0).Rows.Count > 0 Then
                                    strSQL.AppendLine("Delete VIPParameter")
                                    strSQL.AppendLine(strWhere)
                                    Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran, "eHRMSDB")
                                Else
                                    strSQL.AppendLine("Delete VIPParameter")
                                    strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(VIPParameter.CompID.Value))
                                    strSQL.AppendLine("And EmpID = " & Bsp.Utility.Quote(VIPParameter.EmpID.Value))
                                    strSQL.AppendLine("And GrantFlag = " & Bsp.Utility.Quote(VIPParameter.GrantFlag.Value))
                                    strSQL.AppendLine("And UseCompID = " & Bsp.Utility.Quote(VIPParameter.UseCompID.Value))
                                    strSQL.AppendLine("And UseGroupID = " & Bsp.Utility.Quote(VIPParameter.UseGroupID.Value))
                                    strSQL.AppendLine("And UseOrganID = " & Bsp.Utility.Quote(VIPParameter.UseOrganID.Value))
                                    Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran, "eHRMSDB")
                                End If
                            End If
                        End If
                    End If
                Next

                If bsVIPParameter.Insert(beVIPParameter, tran) = 0 Then Return False

                Bsp.DB.ExecuteNonQuery(CommandType.StoredProcedure, "SP_VIP", _
                   New DbParameter() { _
                   Bsp.DB.getDbParameter("@CompID", beVIPParameter(0).CompID.Value), _
                   Bsp.DB.getDbParameter("@EmpID", beVIPParameter(0).EmpID.Value)}, tran)

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

#Region "刪除Web人員資料-VIP"
    Public Function VIPDelete(ByVal beVIPParameter As beVIPParameter.Row) As Boolean
        Dim bsVIPParameter As New beVIPParameter.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsVIPParameter.DeleteRowByPrimaryKey(beVIPParameter, tran) = 0 Then Return False

                Bsp.DB.ExecuteNonQuery(CommandType.StoredProcedure, "SP_VIP", _
                   New DbParameter() { _
                   Bsp.DB.getDbParameter("@CompID", beVIPParameter.CompID.Value), _
                   Bsp.DB.getDbParameter("@EmpID", beVIPParameter.EmpID.Value)}, tran)

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

    Public Function VIPFlowDelete(ByVal beVIPParameterFlow As beVIPParameterFlow.Row) As Boolean
        Dim bsVIPParameterFlow As New beVIPParameterFlow.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsVIPParameterFlow.DeleteRowByPrimaryKey(beVIPParameterFlow, tran) = 0 Then Return False

                Bsp.DB.ExecuteNonQuery(CommandType.StoredProcedure, "SP_VIPFlow", _
                   New DbParameter() { _
                   Bsp.DB.getDbParameter("@CompID", beVIPParameterFlow.CompID.Value), _
                   Bsp.DB.getDbParameter("@EmpID", beVIPParameterFlow.EmpID.Value)}, tran)

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

#Region "新增Web人員資料-VIPFlow"
    Public Function VIPFlowAdd(ByVal beVIPParameterFlow() As beVIPParameterFlow.Row) As Boolean
        Dim bsVIPParameterFlow As New beVIPParameterFlow.Service()
        Dim strSQL As New StringBuilder()
        Dim strWhere As String = ""
        Dim strSelect As String = "Select * From VIPParameterFlow"

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                For Each VIPParameterFlow In beVIPParameterFlow
                    If VIPParameterFlow.AllBusinessType.Value = "1" And VIPParameterFlow.AllOrganIDFlag.Value = "1" Then
                        strSQL.AppendLine("Delete VIPParameterFlow")
                        strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(VIPParameterFlow.CompID.Value))
                        strSQL.AppendLine("And EmpID = " & Bsp.Utility.Quote(VIPParameterFlow.EmpID.Value))
                        strSQL.AppendLine("And GrantFlag = " & Bsp.Utility.Quote(VIPParameterFlow.GrantFlag.Value))
                        strSQL.AppendLine("And UseCompID = " & Bsp.Utility.Quote(VIPParameterFlow.UseCompID.Value))
                        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran, "eHRMSDB")

                    ElseIf VIPParameterFlow.AllBusinessType.Value = "0" And VIPParameterFlow.AllOrganIDFlag.Value = "1" Then
                        strWhere = " Where CompID = " & Bsp.Utility.Quote(VIPParameterFlow.CompID.Value)
                        strWhere += " And EmpID = " & Bsp.Utility.Quote(VIPParameterFlow.EmpID.Value)
                        strWhere += " And GrantFlag = " & Bsp.Utility.Quote(VIPParameterFlow.GrantFlag.Value)
                        strWhere += " And UseCompID = " & Bsp.Utility.Quote(VIPParameterFlow.UseCompID.Value)
                        strWhere += " And AllBusinessType = '1' And AllOrganIDFlag = '1'"

                        If Bsp.DB.ExecuteDataSet(CommandType.Text, strSelect & strWhere, tran, "eHRMSDB").Tables(0).Rows.Count > 0 Then
                            strSQL.AppendLine("Delete VIPParameterFlow")
                            strSQL.AppendLine(strWhere)
                            Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran, "eHRMSDB")
                        Else
                            strSQL.AppendLine("Delete VIPParameterFlow")
                            strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(VIPParameterFlow.CompID.Value))
                            strSQL.AppendLine("And EmpID = " & Bsp.Utility.Quote(VIPParameterFlow.EmpID.Value))
                            strSQL.AppendLine("And GrantFlag = " & Bsp.Utility.Quote(VIPParameterFlow.GrantFlag.Value))
                            strSQL.AppendLine("And UseCompID = " & Bsp.Utility.Quote(VIPParameterFlow.UseCompID.Value))
                            strSQL.AppendLine("And UseBusinessType = " & Bsp.Utility.Quote(VIPParameterFlow.UseBusinessType.Value))
                            Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran, "eHRMSDB")
                        End If

                    ElseIf VIPParameterFlow.AllBusinessType.Value = "0" And VIPParameterFlow.AllOrganIDFlag.Value = "0" Then
                        strWhere = " Where CompID = " & Bsp.Utility.Quote(VIPParameterFlow.CompID.Value)
                        strWhere += " And EmpID = " & Bsp.Utility.Quote(VIPParameterFlow.EmpID.Value)
                        strWhere += " And GrantFlag = " & Bsp.Utility.Quote(VIPParameterFlow.GrantFlag.Value)
                        strWhere += " And UseCompID = " & Bsp.Utility.Quote(VIPParameterFlow.UseCompID.Value)
                        strWhere += " And AllBusinessType = '1' And AllOrganIDFlag = '1'"

                        If Bsp.DB.ExecuteDataSet(CommandType.Text, strSelect & strWhere, tran, "eHRMSDB").Tables(0).Rows.Count > 0 Then
                            strSQL.AppendLine("Delete VIPParameterFlow")
                            strSQL.AppendLine(strWhere)
                            Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran, "eHRMSDB")
                        Else
                            strWhere = " Where CompID = " & Bsp.Utility.Quote(VIPParameterFlow.CompID.Value)
                            strWhere += " And EmpID = " & Bsp.Utility.Quote(VIPParameterFlow.EmpID.Value)
                            strWhere += " And GrantFlag = " & Bsp.Utility.Quote(VIPParameterFlow.GrantFlag.Value)
                            strWhere += " And UseCompID = " & Bsp.Utility.Quote(VIPParameterFlow.UseCompID.Value)
                            strWhere += " And UseBusinessType = " & Bsp.Utility.Quote(VIPParameterFlow.UseBusinessType.Value)
                            strWhere += " And AllBusinessType = '0' And AllOrganIDFlag = '1'"

                            If Bsp.DB.ExecuteDataSet(CommandType.Text, strSelect & strWhere, tran, "eHRMSDB").Tables(0).Rows.Count > 0 Then
                                strSQL.AppendLine("Delete VIPParameterFlow")
                                strSQL.AppendLine(strWhere)
                                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran, "eHRMSDB")
                            Else
                                strSQL.AppendLine("Delete VIPParameterFlow")
                                strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(VIPParameterFlow.CompID.Value))
                                strSQL.AppendLine("And EmpID = " & Bsp.Utility.Quote(VIPParameterFlow.EmpID.Value))
                                strSQL.AppendLine("And GrantFlag = " & Bsp.Utility.Quote(VIPParameterFlow.GrantFlag.Value))
                                strSQL.AppendLine("And UseCompID = " & Bsp.Utility.Quote(VIPParameterFlow.UseCompID.Value))
                                strSQL.AppendLine("And UseBusinessType = " & Bsp.Utility.Quote(VIPParameterFlow.UseBusinessType.Value))
                                strSQL.AppendLine("And UseOrganID = " & Bsp.Utility.Quote(VIPParameterFlow.UseOrganID.Value))
                                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran, "eHRMSDB")
                            End If
                        End If
                    End If
                Next

                If bsVIPParameterFlow.Insert(beVIPParameterFlow, tran) = 0 Then Return False

                Bsp.DB.ExecuteNonQuery(CommandType.StoredProcedure, "SP_VIPFlow", _
                   New DbParameter() { _
                   Bsp.DB.getDbParameter("@CompID", beVIPParameterFlow(0).CompID.Value), _
                   Bsp.DB.getDbParameter("@EmpID", beVIPParameterFlow(0).EmpID.Value)}, tran)

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

#Region "PA4100 OldFun"
    '#Region "查詢Web人員資料-VIP"
    '    Public Function VIPQuery(ByVal ParamArray Params() As String) As DataTable
    '        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
    '        Dim strSQL As New StringBuilder()

    '        strSQL.AppendLine("Select S.CompID, IsNull(C.CompName,'') As CompName")
    '        strSQL.AppendLine(", S.EmpID, IsNull(P.NameN, '') as Name")
    '        strSQL.AppendLine(", S.UseCompID, IsNull(C1.CompName,'') As UseCompName")
    '        strSQL.AppendLine(", S.UseGroupID, IsNull(O.OrganName,'') As UseGroupName")
    '        strSQL.AppendLine(", S.UseOrganID, IsNull(O1.OrganName,'') As UseOrganName")
    '        strSQL.AppendLine(", S.UseRankID")
    '        strSQL.AppendLine(", BeginDate = Case When Convert(Char(10), S.BeginDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, S.BeginDate, 111) End")
    '        strSQL.AppendLine(", EndDate = Case When Convert(Char(10), S.EndDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, S.EndDate, 111) End")
    '        strSQL.AppendLine(", S.LastChgComp, IsNull(C2.CompName, '') As LastChgCompName")
    '        strSQL.AppendLine(", S.LastChgID, IsNull(P1.NameN, '') as LastChgName")
    '        strSQL.AppendLine(", LastChgDate = Case When Convert(Char(10), S.LastChgDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, S.LastChgDate, 120) End")
    '        strSQL.AppendLine("From VIP S")
    '        strSQL.AppendLine("Left Join Personal P on S.EmpID = P.EmpID and S.CompID = P.CompID")
    '        strSQL.AppendLine("Left Join Personal P1 on S.LastChgID = P1.EmpID and S.LastChgComp = P1.CompID")
    '        strSQL.AppendLine("Left Join Company C on S.CompID = C.CompID")
    '        strSQL.AppendLine("Left Join Company C1 on S.UseCompID = C1.CompID")
    '        strSQL.AppendLine("Left Join Company C2 on S.LastChgComp = C2.CompID")
    '        strSQL.AppendLine("Left Join OrganizationFlow O on S.UseGroupID = O.OrganID")
    '        strSQL.AppendLine("Left Join Organization O1 on S.UseCompID = O1.CompID and S.UseOrganID = O1.OrganID")
    '        strSQL.AppendLine("Where 1 = 1")

    '        For Each strKey As String In ht.Keys
    '            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
    '                Select Case strKey
    '                    Case "CompID"
    '                        strSQL.AppendLine("And S.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
    '                    Case "EmpID"
    '                        strSQL.AppendLine("And S.EmpID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
    '                    Case "Name"
    '                        strSQL.AppendLine("And P.NameN like N'%" & ht(strKey).ToString() & "%'")
    '                End Select
    '            End If
    '        Next

    '        strSQL.AppendLine("Order by S.EmpID, S.UseCompID, S.UseGroupID, S.UseOrganID, S.UseRankID")
    '        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    '    End Function
    '#End Region

    '#Region "新增Web人員資料-VIP"
    '    Public Function VIPAdd(ByVal beVIP As beVIP.Row) As Boolean
    '        Dim bsVIP As New beVIP.Service()

    '        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
    '            cn.Open()
    '            Dim tran As DbTransaction = cn.BeginTransaction

    '            Try
    '                If bsVIP.Insert(beVIP, tran) = 0 Then Return False
    '                tran.Commit()
    '            Catch ex As Exception
    '                tran.Rollback()
    '                Throw
    '            Finally
    '                If tran IsNot Nothing Then tran.Dispose()
    '            End Try
    '        End Using

    '        Return True
    '    End Function
    '#End Region

    '#Region "新增Web人員資料(全選)-VIP"
    '    Public Function VIPAdd(ByVal strDoType As String, ByVal beVIP As beVIP.Row, ByVal beVIPParameter As beVIPParameter.Row) As Boolean
    '        Dim bsVIP As New beVIP.Service()
    '        Dim strSQL As New StringBuilder()
    '        Dim strTable() As String = {"VIP", "VIPParameter"}

    '        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
    '            cn.Open()
    '            Dim tran As DbTransaction = cn.BeginTransaction

    '            Try
    '                For Each item As String In strTable

    '                    strSQL = New StringBuilder()
    '                    strSQL.AppendLine("Delete " & item)
    '                    strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(beVIP.CompID.Value))
    '                    strSQL.AppendLine("And EmpID = " & Bsp.Utility.Quote(beVIP.EmpID.Value))

    '                    Select Case strDoType
    '                        Case "1A" '(111)

    '                        Case "1B" '(101)
    '                            strSQL.AppendLine("And UseGroupID = " & Bsp.Utility.Quote(beVIP.UseGroupID.Value))

    '                        Case "1C" '(011)
    '                            strSQL.AppendLine("And UseCompID = " & Bsp.Utility.Quote(beVIP.UseCompID.Value))

    '                        Case "1D" '(010)
    '                            strSQL.AppendLine("And UseCompID = " & Bsp.Utility.Quote(beVIP.UseCompID.Value))
    '                            strSQL.AppendLine("And UseOrganID = " & Bsp.Utility.Quote(beVIP.UseOrganID.Value))

    '                        Case "1E" '(001)
    '                            strSQL.AppendLine("And UseCompID = " & Bsp.Utility.Quote(beVIP.UseCompID.Value))
    '                            strSQL.AppendLine("And UseGroupID = " & Bsp.Utility.Quote(beVIP.UseGroupID.Value))

    '                    End Select

    '                    Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran, "eHRMSDB")
    '                Next

    '                For Each item As String In strTable
    '                    strSQL = New StringBuilder()
    '                    strSQL.AppendLine("Insert Into " & item & " (")
    '                    strSQL.AppendLine("CompID, EmpID")
    '                    strSQL.AppendLine(", UseCompID, UseGroupID, UseOrganID, UseRankID")

    '                    If "VIPParameter".Equals(item) Then
    '                        strSQL.AppendLine(", AllCompIDFlag, AllGroupIDFlag")
    '                        strSQL.AppendLine(", AllOrganIDFlag, AllRankIDFlag")
    '                    End If

    '                    strSQL.AppendLine(", BeginDate, EndDate")
    '                    strSQL.AppendLine(", CreateComp, CreateID, CreateDate")
    '                    strSQL.AppendLine(", ReleaseComp, ReleaseID, ReleaseDate")
    '                    strSQL.AppendLine(", LastChgComp, LastChgID, LastChgDate")
    '                    strSQL.AppendLine(")")

    '                    strSQL.AppendLine("Select " & Bsp.Utility.Quote(beVIP.CompID.Value) & ", " & Bsp.Utility.Quote(beVIP.EmpID.Value))
    '                    strSQL.AppendLine(", CompID, GroupID, OrganID, " & Bsp.Utility.Quote(beVIP.UseRankID.Value))

    '                    If "VIPParameter".Equals(item) Then
    '                        strSQL.AppendLine(", " & Bsp.Utility.Quote(beVIPParameter.AllCompIDFlag.Value) & ", " & Bsp.Utility.Quote(beVIPParameter.AllGroupIDFlag.Value))
    '                        strSQL.AppendLine(", " & Bsp.Utility.Quote(beVIPParameter.AllOrganIDFlag.Value) & ", " & Bsp.Utility.Quote(beVIPParameter.AllRankIDFlag.Value))
    '                    End If

    '                    strSQL.AppendLine(", " & Bsp.Utility.Quote(beVIP.BeginDate.Value) & ", " & Bsp.Utility.Quote(beVIP.EndDate.Value))
    '                    strSQL.AppendLine(", " & Bsp.Utility.Quote(beVIP.CreateComp.Value) & ", " & Bsp.Utility.Quote(beVIP.CreateID.Value) & ", " & Bsp.Utility.Quote(Now.ToString("yyyy-MM-dd HH:mm:ss")))
    '                    strSQL.AppendLine(", " & Bsp.Utility.Quote(beVIP.ReleaseComp.Value) & ", " & Bsp.Utility.Quote(beVIP.ReleaseID.Value) & ", " & Bsp.Utility.Quote(Now.ToString("yyyy-MM-dd HH:mm:ss")))
    '                    strSQL.AppendLine(", " & Bsp.Utility.Quote(beVIP.LastChgComp.Value) & ", " & Bsp.Utility.Quote(beVIP.LastChgID.Value) & ", " & Bsp.Utility.Quote(Now.ToString("yyyy-MM-dd HH:mm:ss")))
    '                    strSQL.AppendLine("From Organization")
    '                    strSQL.AppendLine("Where DeptID = OrganID And VirtualFlag = '0'")

    '                    Select Case strDoType
    '                        Case "1A" '(111)

    '                        Case "1B" '(101)
    '                            strSQL.AppendLine("And InValidFlag = '0'")
    '                            strSQL.AppendLine("And GroupID = " & Bsp.Utility.Quote(beVIP.UseGroupID.Value))

    '                        Case "1C" '(011)
    '                            strSQL.AppendLine("And CompID = " & Bsp.Utility.Quote(beVIP.UseCompID.Value))

    '                        Case "1D" '(010)
    '                            strSQL.AppendLine("And InValidFlag = '0'")
    '                            strSQL.AppendLine("And CompID = " & Bsp.Utility.Quote(beVIP.UseCompID.Value))
    '                            strSQL.AppendLine("And OrganID = " & Bsp.Utility.Quote(beVIP.UseOrganID.Value))

    '                        Case "1E" '(001)
    '                            strSQL.AppendLine("And InValidFlag = '0'")
    '                            strSQL.AppendLine("And CompID = " & Bsp.Utility.Quote(beVIP.UseCompID.Value))
    '                            strSQL.AppendLine("And GroupID = " & Bsp.Utility.Quote(beVIP.UseGroupID.Value))

    '                    End Select

    '                    Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran, "eHRMSDB")
    '                Next

    '                tran.Commit()
    '            Catch ex As Exception
    '                tran.Rollback()
    '                Throw
    '            Finally
    '                If tran IsNot Nothing Then tran.Dispose()
    '            End Try
    '        End Using

    '        Return True
    '    End Function
    '#End Region

    '#Region "修改Web人員資料-VIP"
    '    Public Function VIPUpdate(ByVal beVIP As beVIP.Row) As Boolean
    '        Dim bsVIP As New beVIP.Service()
    '        Dim strSQL As New StringBuilder()

    '        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
    '            cn.Open()
    '            Dim tran As DbTransaction = cn.BeginTransaction

    '            Try
    '                strSQL.AppendLine("UPDATE VIP")
    '                strSQL.AppendLine("SET EmpID = @EmpID")
    '                strSQL.AppendLine(", UseCompID = @UseCompID")
    '                strSQL.AppendLine(", UseGroupID = @UseGroupID")
    '                strSQL.AppendLine(", UseOrganID = @UseOrganID")
    '                strSQL.AppendLine(", UseRankID = @UseRankID")
    '                strSQL.AppendLine(", BeginDate = @BeginDate")
    '                strSQL.AppendLine(", EndDate = @EndDate")
    '                strSQL.AppendLine(", ReleaseComp = @ReleaseComp")
    '                strSQL.AppendLine(", ReleaseID = @ReleaseID")
    '                strSQL.AppendLine(", ReleaseDate = @ReleaseDate")
    '                strSQL.AppendLine(", LastChgComp = @LastChgComp")
    '                strSQL.AppendLine(", LastChgID = @LastChgID")
    '                strSQL.AppendLine(", LastChgDate = @LastChgDate")
    '                strSQL.AppendLine("WHERE CompID = @KeyCompID")
    '                strSQL.AppendLine("AND EmpID = @KeyEmpID")
    '                strSQL.AppendLine("AND UseCompID = @KeyUseCompID")
    '                strSQL.AppendLine("AND UseGroupID = @KeyUseGroupID")
    '                strSQL.AppendLine("AND UseOrganID = @KeyUseOrganID")

    '                Dim DbParam() As DbParameter = { _
    '                    Bsp.DB.getDbParameter("@EmpID", beVIP.EmpID.Value), _
    '                    Bsp.DB.getDbParameter("@UseCompID", beVIP.UseCompID.Value), _
    '                    Bsp.DB.getDbParameter("@UseGroupID", beVIP.UseGroupID.Value), _
    '                    Bsp.DB.getDbParameter("@UseOrganID", beVIP.UseOrganID.Value), _
    '                    Bsp.DB.getDbParameter("@UseRankID", beVIP.UseRankID.Value), _
    '                    Bsp.DB.getDbParameter("@BeginDate", beVIP.BeginDate.Value), _
    '                    Bsp.DB.getDbParameter("@EndDate", beVIP.EndDate.Value), _
    '                    Bsp.DB.getDbParameter("@ReleaseComp", beVIP.ReleaseComp.Value), _
    '                    Bsp.DB.getDbParameter("@ReleaseID", beVIP.ReleaseID.Value), _
    '                    Bsp.DB.getDbParameter("@ReleaseDate", Now), _
    '                    Bsp.DB.getDbParameter("@LastChgComp", beVIP.LastChgComp.Value), _
    '                    Bsp.DB.getDbParameter("@LastChgID", beVIP.LastChgID.Value), _
    '                    Bsp.DB.getDbParameter("@LastChgDate", Now), _
    '                    Bsp.DB.getDbParameter("@KeyCompID", beVIP.CompID.Value), _
    '                    Bsp.DB.getDbParameter("@KeyEmpID", beVIP.EmpID.OldValue), _
    '                    Bsp.DB.getDbParameter("@KeyUseCompID", beVIP.UseCompID.OldValue), _
    '                    Bsp.DB.getDbParameter("@KeyUseGroupID", beVIP.UseGroupID.OldValue), _
    '                    Bsp.DB.getDbParameter("@KeyUseOrganID", beVIP.UseOrganID.OldValue)}

    '                If Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), DbParam, tran, "eHRMSDB") = 0 Then Return False
    '                tran.Commit()
    '            Catch ex As Exception
    '                tran.Rollback()
    '                Throw
    '            Finally
    '                If tran IsNot Nothing Then tran.Dispose()
    '            End Try
    '        End Using

    '        Return True
    '    End Function
    '#End Region

    '#Region "刪除Web人員資料-VIP"
    '    Public Function VIPDelete(ByVal strDoType As String, ByVal beVIP As beVIP.Row) As Boolean
    '        Dim strTable() As String = {"VIP", "VIPParameter"}

    '        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
    '            cn.Open()
    '            Dim tran As DbTransaction = cn.BeginTransaction

    '            Try
    '                For Each item As String In strTable
    '                    Dim strSQL As New StringBuilder()

    '                    strSQL.AppendLine("Delete " & item)
    '                    strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(beVIP.CompID.Value))
    '                    strSQL.AppendLine("And EmpID = " & Bsp.Utility.Quote(beVIP.EmpID.Value))

    '                    Select Case strDoType
    '                        Case "Delete"           '刪除此筆權限
    '                            strSQL.AppendLine("And UseCompID = " & Bsp.Utility.Quote(beVIP.UseCompID.Value))
    '                            strSQL.AppendLine("And UseGroupID = " & Bsp.Utility.Quote(beVIP.UseGroupID.Value))
    '                            strSQL.AppendLine("And UseOrganID = " & Bsp.Utility.Quote(beVIP.UseOrganID.Value))

    '                        Case "DeleteAll"        '刪除全部權限

    '                        Case "DeleteAllGroup"   '該權限公司下，刪除全部查詢權限事業群
    '                            strSQL.AppendLine("And UseCompID = " & Bsp.Utility.Quote(beVIP.UseCompID.Value))

    '                        Case "DeleteAllComp"    '該權限事業群下，刪除全部查詢權限公司
    '                            strSQL.AppendLine("And UseGroupID = " & Bsp.Utility.Quote(beVIP.UseGroupID.Value))

    '                    End Select

    '                    Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran, "eHRMSDB")
    '                Next
    '                tran.Commit()
    '            Catch ex As Exception
    '                tran.Rollback()
    '                Throw
    '            Finally
    '                If tran IsNot Nothing Then tran.Dispose()
    '            End Try
    '        End Using

    '        Return True
    '    End Function
    '#End Region
#End Region
#End Region

#Region "PA4200 Web我們的同仁組織圖列印"
#Region "查詢Web我們的同仁組織圖列印"
    Public Function ExOrganPrintQuery(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select S.CompID ")
        strSQL.AppendLine(", IsNull(C.CompName, '') As CompName")
        strSQL.AppendLine(", IsNull(P.NameN, '') As Name")
        strSQL.AppendLine(", S.EmpID")
        strSQL.AppendLine(", IsNull(P.NameN, '') As Name")
        strSQL.AppendLine(", OrganPrintFlag = Case S.OrganPrintFlag When '0' Then '否' When '1' Then '是' Else '' End")
        strSQL.AppendLine(", PrintType = Case S.PrintType When '1' Then '部門' When '2' Then '全行' Else '' End")
        strSQL.AppendLine(", S.CreateID")
        strSQL.AppendLine(", IsNull(P1.NameN, '') As CreateName")
        strSQL.AppendLine(", CreateDate = Case When Convert(Char(10), S.CreateTime, 111) = '1900/01/01' Then '' Else Convert(Varchar, S.CreateTime, 120) End")
        strSQL.AppendLine(", IsNull(C1.CompName, '') As LastChgComp")
        strSQL.AppendLine(", IsNull(P2.NameN, '') As LastChgName")
        strSQL.AppendLine(", LastChgDate = Case When Convert(Char(10), S.LastChgDate, 111) = '1900/01/01' Then '' Else Convert(Varchar, S.LastChgDate, 120) End")
        strSQL.AppendLine("from ExOrganPrint S")
        strSQL.AppendLine("Left Join Company C on S.CompID = C.CompID")
        strSQL.AppendLine("Left Join Company C1 on S.LastChgComp = C1.CompID")
        strSQL.AppendLine("Left Join Personal P On S.EmpID = P.EmpID and S.CompID = P.CompID")
        strSQL.AppendLine("Left Join Personal P1 On S.CreateID = P1.EmpID and S.CreateComp = P1.CompID")
        strSQL.AppendLine("Left Join Personal P2 On S.LastChgID = P2.EmpID and S.LastChgComp = P2.CompID")
        strSQL.AppendLine("Where 1 = 1")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        If ht(strKey).ToString() <> "0" Then
                            strSQL.AppendLine("And S.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        End If
                    Case "EmpID"
                        strSQL.AppendLine("And S.EmpID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "Name"
                        strSQL.AppendLine("And P.NameN = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "PrintType"
                        strSQL.AppendLine("And S.PrintType = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "OrganPrintFlag"
                        strSQL.AppendLine("And S.OrganPrintFlag = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next

        strSQL.AppendLine("Order by S.OrganPrintFlag Desc, S.PrintType, S.EmpID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "新增Web我們的同仁組織圖列印"
    Public Function ExOrganPrintAdd(ByVal beExOrganPrint As beExOrganPrint.Row) As Boolean
        Dim bsExOrganPrint As New beExOrganPrint.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsExOrganPrint.Insert(beExOrganPrint, tran) = 0 Then Return False
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

#Region "修改Web我們的同仁組織圖列印"
    Public Function ExOrganPrintUpdate(ByVal beExOrganPrint As beExOrganPrint.Row) As Boolean
        Dim bsExOrganPrint As New beExOrganPrint.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsExOrganPrint.Update(beExOrganPrint, tran) = 0 Then Return False
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

#Region "刪除Web我們的同仁組織圖列印"
    Public Function ExOrganPrintDelete(ByVal beExOrganPrint As beExOrganPrint.Row) As Boolean
        Dim bsExOrganPrint As New beExOrganPrint.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsExOrganPrint.DeleteRowByPrimaryKey(beExOrganPrint, tran) = 0 Then Return False
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

#Region "SelectEmp"
    '2015/07/31 Add By Micky
    Public Function GetEmpName(ByVal txtCompID As String, ByVal txtEmpID As String) As DataTable
        Dim strSQL As New StringBuilder
        strSQL.AppendLine(" Select NameN from Personal ")
        strSQL.AppendLine(" Where 1 = 1 ")
        strSQL.AppendLine(" AND CompID = " & Bsp.Utility.Quote(txtCompID))
        strSQL.AppendLine(" AND EmpID = " & Bsp.Utility.Quote(txtEmpID))
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#End Region

#Region "PA4300 Web功能權限維護"
#Region "查詢Web功能權限維護"
    Public Function UserFormWebQuery(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select S.CompID ")
        strSQL.AppendLine(", IsNull(C.CompName, '') As CompName")
        strSQL.AppendLine(", S.WebID")
        strSQL.AppendLine(", F.WebName")
        strSQL.AppendLine(", F.ParentID")
        strSQL.AppendLine(", F1.WebName ParentName")
        strSQL.AppendLine(", F.FunType")
        strSQL.AppendLine(", F.TargetURL")
        strSQL.AppendLine(", F.OrderSeq")
        strSQL.AppendLine(", IsNull(C1.CompName, '') As LastChgComp")
        strSQL.AppendLine(", IsNull(P.NameN, '') As LastChgName")
        strSQL.AppendLine(", LastChgDate = Case When Convert(Char(10), S.LastChgDate, 111) = '1900/01/01' Then '' Else Convert(Varchar, S.LastChgDate, 120) End")
        strSQL.AppendLine("from UserFormWeb S")
        strSQL.AppendLine("Left Join FormWeb F on S.WebID = F.WebID")
        strSQL.AppendLine("Left Join FormWeb F1 on F.ParentID = F1.WebID")
        strSQL.AppendLine("Left Join Company C on S.CompID = C.CompID")
        strSQL.AppendLine("Left Join Company C1 on S.LastChgComp = C1.CompID")
        strSQL.AppendLine("Left Join Personal P On S.LastChgID = P.EmpID and S.LastChgComp = P.CompID")
        strSQL.AppendLine("Where 1 = 1")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        If ht(strKey).ToString() <> "0" Then
                            strSQL.AppendLine("And S.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        End If
                    Case "WebID"
                        strSQL.AppendLine("And S.WebID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next

        strSQL.AppendLine("Order by F.FunType, F.OrderSeq")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "新增Web功能權限維護"
    Public Function UserFormWebAdd(ByVal beUserFormWeb As beUserFormWeb.Row) As Boolean
        Dim bsUserFormWeb As New beUserFormWeb.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsUserFormWeb.Insert(beUserFormWeb, tran) = 0 Then Return False
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

#Region "修改Web功能權限維護"
    Public Function UserFormWebUpdate(ByVal beUserFormWeb As beUserFormWeb.Row) As Boolean
        Dim bsUserFormWeb As New beUserFormWeb.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsUserFormWeb.Update(beUserFormWeb, tran) = 0 Then Return False
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

#Region "刪除Web功能權限維護"
    Public Function UserFormWebDelete(ByVal beUserFormWeb As beUserFormWeb.Row) As Boolean
        Dim bsUserFormWeb As New beUserFormWeb.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsUserFormWeb.DeleteRowByPrimaryKey(beUserFormWeb, tran) = 0 Then Return False
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
#End Region

#Region "PA4400 Web我們的同仁組織色塊設定"
#Region "查詢Web我們的同仁組織色塊設定"
    Public Function OrganColor_WebQuery(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select S.CompID ")
        strSQL.AppendLine(", IsNull(C.CompName, '') As CompName")
        strSQL.AppendLine(", S.SortOrder")
        strSQL.AppendLine(", S.Color")
        strSQL.AppendLine(", IsNull(C1.CompName, '') As LastChgComp")
        strSQL.AppendLine(", IsNull(P.NameN, '') As LastChgName")
        strSQL.AppendLine(", LastChgDate = Case When Convert(Char(10), S.LastChgDate, 111) = '1900/01/01' Then '' Else Convert(Varchar, S.LastChgDate, 120) End")
        strSQL.AppendLine("from OrganColor_Web S")
        strSQL.AppendLine("Left Join Company C on S.CompID = C.CompID")
        strSQL.AppendLine("Left Join Company C1 on S.LastChgComp = C1.CompID")
        strSQL.AppendLine("Left Join Personal P On S.LastChgID = P.EmpID and S.LastChgComp = P.CompID")
        strSQL.AppendLine("Where 1 = 1")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        If ht(strKey).ToString() <> "0" Then
                            strSQL.AppendLine("And S.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        End If
                    Case "SortOrder"
                        strSQL.AppendLine("And S.SortOrder = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next

        strSQL.AppendLine("Order by S.CompID, S.SortOrder")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "新增Web我們的同仁組織色塊設定"
    Public Function OrganColor_WebAdd(ByVal beOrganColor_Web As beOrganColor_Web.Row) As Boolean
        Dim bsOrganColor_Web As New beOrganColor_Web.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsOrganColor_Web.Insert(beOrganColor_Web, tran) = 0 Then Return False
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

#Region "修改Web我們的同仁組織色塊設定"
    Public Function OrganColor_WebUpdate(ByVal beOrganColor_Web As beOrganColor_Web.Row) As Boolean
        Dim bsOrganColor_Web As New beOrganColor_Web.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                strSQL.AppendLine("UPDATE OrganColor_Web")
                strSQL.AppendLine("SET SortOrder = @SortOrder")
                strSQL.AppendLine(", Color = @Color")
                strSQL.AppendLine(", LastChgComp = @LastChgComp")
                strSQL.AppendLine(", LastChgID = @LastChgID")
                strSQL.AppendLine(", LastChgDate = @LastChgDate")
                strSQL.AppendLine("WHERE CompID = @KeyCompID")
                strSQL.AppendLine("AND SortOrder = @KeySortOrder")

                Dim DbParam() As DbParameter = { _
                    Bsp.DB.getDbParameter("@SortOrder", beOrganColor_Web.SortOrder.Value), _
                    Bsp.DB.getDbParameter("@Color", beOrganColor_Web.Color.Value), _
                    Bsp.DB.getDbParameter("@LastChgComp", beOrganColor_Web.LastChgComp.Value), _
                    Bsp.DB.getDbParameter("@LastChgID", beOrganColor_Web.LastChgID.Value), _
                    Bsp.DB.getDbParameter("@LastChgDate", Now), _
                    Bsp.DB.getDbParameter("@KeyCompID", beOrganColor_Web.CompID.Value), _
                    Bsp.DB.getDbParameter("@KeySortOrder", beOrganColor_Web.SortOrder.OldValue)}

                If Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), DbParam, tran, "eHRMSDB") = 0 Then Return False

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

#Region "多筆修改Web我們的同仁組織色塊設定"
    Public Function OrganColor_WebMutiUpdate(ByVal beOrganColor_Web As beOrganColor_Web.Row) As Boolean
        Dim bsOrganColor_Web As New beOrganColor_Web.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                strSQL.AppendLine("UPDATE OrganColor_Web")
                strSQL.AppendLine("SET Color = @NewColor")
                strSQL.AppendLine(", LastChgComp = @LastChgComp")
                strSQL.AppendLine(", LastChgID = @LastChgID")
                strSQL.AppendLine(", LastChgDate = @LastChgDate")
                strSQL.AppendLine("WHERE CompID = @CompID")
                strSQL.AppendLine("AND Color = @OldColor")

                Dim DbParam() As DbParameter = { _
                    Bsp.DB.getDbParameter("@NewColor", beOrganColor_Web.Color.Value), _
                    Bsp.DB.getDbParameter("@LastChgComp", beOrganColor_Web.LastChgComp.Value), _
                    Bsp.DB.getDbParameter("@LastChgID", beOrganColor_Web.LastChgID.Value), _
                    Bsp.DB.getDbParameter("@LastChgDate", Now), _
                    Bsp.DB.getDbParameter("@CompID", beOrganColor_Web.CompID.Value), _
                    Bsp.DB.getDbParameter("@OldColor", beOrganColor_Web.Color.OldValue)}

                If Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), DbParam, tran, "eHRMSDB") = 0 Then Return False

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

#Region "刪除Web我們的同仁組織色塊設定"
    Public Function OrganColor_WebDelete(ByVal beOrganColor_Web As beOrganColor_Web.Row) As Boolean
        Dim bsOrganColor_Web As New beOrganColor_Web.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsOrganColor_Web.DeleteRowByPrimaryKey(beOrganColor_Web, tran) = 0 Then Return False
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
#End Region

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
#End Region

End Class
