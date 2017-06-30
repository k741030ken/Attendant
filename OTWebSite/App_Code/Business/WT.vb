'****************************************************
'功能說明：排班管理
'建立人員：BeatriceCheng
'建立日期：2017.05.26
'****************************************************
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data

Public Class WT

#Region "查詢個人班表"
    Public Function EmpWorkTimeQuery(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("SELECT P.CompID, C.CompName, P.EmpID, P.NameN")
        strSQL.AppendLine(", O2.OrgType, O3.OrganName AS OrgTypeName")
        strSQL.AppendLine(", P.DeptID, O1.OrganName AS DeptName")
        strSQL.AppendLine(", P.OrganID, O2.OrganName")
        strSQL.AppendLine(", ISNULL(W.WTID, '') WTID")
        strSQL.AppendLine(", WorkTime = LEFT(W.BeginTime, 2) + ':' + RIGHT(W.BeginTime, 2) + '~' + LEFT(W.EndTime, 2) + ':' + RIGHT(W.EndTime, 2)")
        strSQL.AppendLine(", EW.RotateFlag")
        strSQL.AppendLine(", LastChgComp = ISNULL(LC.CompName, EW.LastChgComp)")
        strSQL.AppendLine(", LastChgID = ISNULL(LP.NameN, EW.LastChgID)")
        strSQL.AppendLine(", LastChgDate = CASE WHEN CONVERT(VARCHAR, EW.LastChgDate, 111) = '1900/01/01' THEN '' ELSE CONVERT(VARCHAR, EW.LastChgDate, 120) END")
        strSQL.AppendLine("FROM EmpWorkTime EW")
        strSQL.AppendLine("LEFT JOIN " + Bsp.Utility.getAppSetting("eHRMSDB") + "..Personal P ON EW.CompID = P.CompID AND EW.EmpID = P.EmpID")
        strSQL.AppendLine("LEFT JOIN " + Bsp.Utility.getAppSetting("eHRMSDB") + "..EmpFlow E ON P.CompID = E.CompID AND P.EmpID = E.EmpID AND E.ActionID = '01'")
        strSQL.AppendLine("LEFT JOIN " + Bsp.Utility.getAppSetting("eHRMSDB") + "..Company C ON P.CompID = C.CompID")
        strSQL.AppendLine("LEFT JOIN " + Bsp.Utility.getAppSetting("eHRMSDB") + "..Organization O1 ON O1.CompID = P.CompID AND O1.OrganID = P.DeptID")
        strSQL.AppendLine("LEFT JOIN " + Bsp.Utility.getAppSetting("eHRMSDB") + "..Organization O2 ON O2.CompID = P.CompID AND O2.OrganID = P.OrganID")
        strSQL.AppendLine("LEFT JOIN " + Bsp.Utility.getAppSetting("eHRMSDB") + "..Organization O3 ON O3.CompID = P.CompID AND O3.OrganID = O2.OrgType")
        strSQL.AppendLine("LEFT JOIN " + Bsp.Utility.getAppSetting("eHRMSDB") + "..WorkTime W ON EW.WTCompID = W.CompID AND EW.WTID = W.WTID")
        strSQL.AppendLine("LEFT JOIN " + Bsp.Utility.getAppSetting("eHRMSDB") + "..Company LC ON EW.LastChgComp = LC.CompID")
        strSQL.AppendLine("LEFT JOIN " + Bsp.Utility.getAppSetting("eHRMSDB") + "..Personal LP ON EW.LastChgComp = LP.CompID AND EW.LastChgID = LP.EmpID")
        strSQL.AppendLine("WHERE 1=1")
        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine("AND P.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "DeptID"
                        strSQL.AppendLine("AND P.DeptID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "OrganID"
                        strSQL.AppendLine("AND P.OrganID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "FlowOrganID"
                        If ht(strKey).ToString() = "ALL" Then
                            strSQL.AppendLine("AND E.BusinessType IN (Select Code From " + Bsp.Utility.getAppSetting("eHRMSDB") + "..HRCodeMap Where TabName = 'Business' and FldName = 'BusinessType' and NotShowFlag = '0')")
                        Else
                            strSQL.AppendLine("AND E.OrganID IN (Select OrganID From " + Bsp.Utility.getAppSetting("eHRMSDB") + "..funGetUnderOrganFlow(" & Bsp.Utility.Quote(ht("BusinessType").ToString()) & ", " & Bsp.Utility.Quote(ht("FlowOrganID").ToString()) & ", 'D'))")
                        End If
                    Case "EmpID"
                        strSQL.AppendLine("AND P.EmpID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "Name"
                        strSQL.AppendLine("AND P.Name LIKE N'%" & ht(strKey).ToString() & "%'")
                End Select
            End If
        Next
        strSQL.AppendLine("ORDER BY P.CompID, P.DeptID, P.OrganID, P.EmpID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "AattendantDB").Tables(0)
    End Function

    Public Function EmpWorkTimeLogQuery(ByVal CompID As String, ByVal EmpID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("SELECT P.CompID, C.CompName, P.EmpID, P.NameN")
        strSQL.AppendLine(", O2.OrgType, O3.OrganName AS OrgTypeName")
        strSQL.AppendLine(", P.DeptID, O1.OrganName AS DeptName")
        strSQL.AppendLine(", P.OrganID, O2.OrganName")
        strSQL.AppendLine(", ISNULL(W.WTID, '') WTID")
        strSQL.AppendLine(", WorkTime = LEFT(W.BeginTime, 2) + ':' + RIGHT(W.BeginTime, 2) + '~' + LEFT(W.EndTime, 2) + ':' + RIGHT(W.EndTime, 2)")
        strSQL.AppendLine(", EW.RotateFlag")
        strSQL.AppendLine(", Action = CASE EW.ActionFlag WHEN 'A' THEN '新增' WHEN 'U' THEN '修改' WHEN 'D' THEN '刪除' ELSE '' END")
        strSQL.AppendLine(", LastChgComp = ISNULL(LC.CompName, EW.LastChgComp)")
        strSQL.AppendLine(", LastChgID = ISNULL(LP.NameN, EW.LastChgID)")
        strSQL.AppendLine(", LastChgDate = CASE WHEN CONVERT(VARCHAR, EW.LastChgDate, 111) = '1900/01/01' THEN '' ELSE CONVERT(VARCHAR, EW.LastChgDate, 120) END")
        strSQL.AppendLine("FROM EmpWorkTimeLog EW")
        strSQL.AppendLine("LEFT JOIN " + Bsp.Utility.getAppSetting("eHRMSDB") + "..Personal P ON EW.CompID = P.CompID AND EW.EmpID = P.EmpID")
        strSQL.AppendLine("LEFT JOIN " + Bsp.Utility.getAppSetting("eHRMSDB") + "..Company C ON P.CompID = C.CompID")
        strSQL.AppendLine("LEFT JOIN " + Bsp.Utility.getAppSetting("eHRMSDB") + "..Organization O1 ON O1.CompID = P.CompID AND O1.OrganID = P.DeptID")
        strSQL.AppendLine("LEFT JOIN " + Bsp.Utility.getAppSetting("eHRMSDB") + "..Organization O2 ON O2.CompID = P.CompID AND O2.OrganID = P.OrganID")
        strSQL.AppendLine("LEFT JOIN " + Bsp.Utility.getAppSetting("eHRMSDB") + "..Organization O3 ON O3.CompID = P.CompID AND O3.OrganID = O2.OrgType")
        strSQL.AppendLine("LEFT JOIN " + Bsp.Utility.getAppSetting("eHRMSDB") + "..WorkTime W ON EW.WTCompID = W.CompID AND EW.WTID = W.WTID")
        strSQL.AppendLine("LEFT JOIN " + Bsp.Utility.getAppSetting("eHRMSDB") + "..Company LC ON EW.LastChgComp = LC.CompID")
        strSQL.AppendLine("LEFT JOIN " + Bsp.Utility.getAppSetting("eHRMSDB") + "..Personal LP ON EW.LastChgComp = LP.CompID AND EW.LastChgID = LP.EmpID")
        strSQL.AppendLine("WHERE 1=1")
        strSQL.AppendLine("AND P.CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine("AND P.EmpID = " & Bsp.Utility.Quote(EmpID))
        strSQL.AppendLine("ORDER BY EW.LastChgDate DESC")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "AattendantDB").Tables(0)
    End Function
#End Region

#Region "查詢值勤表"
    Public Function GetOrgWorkSite(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("SELECT W.WorkSiteID, W.Remark AS WorkSiteName FROM Organization O")
        strSQL.AppendLine("JOIN WorkSite W ON W.CompID = O.CompID AND W.WorkSiteID = O.WorkSiteID")
        strSQL.AppendLine("WHERE 1=1")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine("AND O.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "OrganID"
                        strSQL.AppendLine("AND O.OrganID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

    Public Function GetHoliday(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("SELECT SysDate FROM Calendar")
        strSQL.AppendLine("WHERE 1=1")
        strSQL.AppendLine("AND HolidayOrNot = '1'")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine("AND CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "SysDate"
                        strSQL.AppendLine("AND SysDate = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

    Public Function GetOverSeaHoliday(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("SELECT HDate AS SysDate FROM OverSeaHoliday")
        strSQL.AppendLine("WHERE 1=1")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "WorkSite"
                        strSQL.AppendLine("AND WorkSite = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "SysDate"
                        strSQL.AppendLine("AND HDate = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

    Public Function GetVacInfo(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("SELECT VacEmpID FROM VacDocInfo")
        strSQL.AppendLine("WHERE 1=1")
        strSQL.AppendLine("AND Status = '999'")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "EmpID"
                        strSQL.AppendLine("AND VacEmpID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "VacDate"
                        strSQL.AppendLine("AND VacDateS >= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        strSQL.AppendLine("AND VacDateE <= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "AattendantDB").Tables(0)
    End Function

    Public Function LoadGuardCalendar(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("SELECT EW.DutyDate, P.EmpID, P.NameN")
        strSQL.AppendLine(", DutyTime = CASE WHEN EW.BranchFlag = '1' THEN LEFT(W.BeginTime, 2) + ':' + RIGHT(W.BeginTime, 2) + '~' + LEFT(W.EndTime, 2) + ':' + RIGHT(W.EndTime, 2)")
        strSQL.AppendLine("ELSE LEFT(EW.WTBeginTime, 2) + ':' + RIGHT(EW.WTBeginTime, 2) + '~' + LEFT(EW.WTEndTime, 2) + ':' + RIGHT(EW.WTEndTime, 2) END")
        strSQL.AppendLine("FROM EmpGuardWorkTime EW")
        strSQL.AppendLine("LEFT JOIN " + Bsp.Utility.getAppSetting("eHRMSDB") + "..Personal P ON EW.CompID = P.CompID AND EW.EmpID = P.EmpID")
        strSQL.AppendLine("LEFT JOIN " + Bsp.Utility.getAppSetting("eHRMSDB") + "..WorkTime W ON EW.WTCompID = W.CompID AND EW.WTID = W.WTID")
        strSQL.AppendLine("WHERE 1=1")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine("AND EW.DutyCompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "DeptID"
                        strSQL.AppendLine("AND EW.DutyDeptID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "OrganID"
                        strSQL.AppendLine("AND EW.DutyOrganID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "DutyDate"
                        strSQL.AppendLine("AND EW.DutyDate = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "EmpID"
                        strSQL.AppendLine("AND UPPER(EW.EmpID) = UPPER(" & Bsp.Utility.Quote(ht(strKey).ToString()) & ")")
                    Case "NameN"
                        strSQL.AppendLine("AND P.NameN LIKE N" & Bsp.Utility.Quote("%" & ht(strKey).ToString() & "%"))
                End Select
            End If
        Next

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "AattendantDB").Tables(0)
    End Function
#End Region

#Region "參數設定"
    Public Function GetWorkTimePara(ByVal CompID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("SELECT MonthLimit")
        strSQL.AppendLine(", LastChgComp = ISNULL(LC.CompName, PA.LastChgComp)")
        strSQL.AppendLine(", LastChgID = ISNULL(LP.NameN, PA.LastChgID)")
        strSQL.AppendLine(", LastChgDate = CASE WHEN CONVERT(VARCHAR, PA.LastChgDate, 111) = '1900/01/01' THEN '' ELSE CONVERT(VARCHAR, PA.LastChgDate, 120) END")
        strSQL.AppendLine("FROM WorkTimePara PA")
        strSQL.AppendLine("LEFT JOIN " + Bsp.Utility.getAppSetting("eHRMSDB") + "..Company LC ON PA.LastChgComp = LC.CompID")
        strSQL.AppendLine("LEFT JOIN " + Bsp.Utility.getAppSetting("eHRMSDB") + "..Personal LP ON PA.LastChgComp = LP.CompID AND PA.LastChgID = LP.EmpID")
        strSQL.AppendLine("WHERE 1=1")
        strSQL.AppendLine("AND PA.CompID = " & Bsp.Utility.Quote(CompID))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "AattendantDB").Tables(0)
    End Function

    Public Function WorkTimeParaAdd(ByVal ParamArray Params() As String) As Boolean
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("AattendantDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                strSQL.AppendLine("DELETE WorkTimePara WHERE CompID=@CompID")
                strSQL.AppendLine("INSERT INTO WorkTimePara (CompID, MonthLimit, LastChgComp, LastChgID, LastChgDate)")
                strSQL.AppendLine("VALUES (@CompID, @MonthLimit, @LastChgComp, @LastChgID, @LastChgDate)")

                Dim DbParam() As DbParameter = { _
                    Bsp.DB.getDbParameter("@CompID", ht("CompID").ToString), _
                    Bsp.DB.getDbParameter("@MonthLimit", ht("MonthLimit").ToString), _
                    Bsp.DB.getDbParameter("@LastChgComp", UserProfile.ActCompID), _
                    Bsp.DB.getDbParameter("@LastChgID", UserProfile.ActUserID), _
                    Bsp.DB.getDbParameter("@LastChgDate", Now)}

                If Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), DbParam, tran, "AattendantDB") = 0 Then Return False

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

    Public Function GetPOSPPara(ByVal CompID As String, ByVal Category As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("SELECT PA.HoldingRankIDFlag, PA.HoldingRankID, PA.PositionIDFlag, PA.PositionID, PA.WorkTypeIDFlag, PA.WorkTypeID")
        strSQL.AppendLine(", LastChgComp = ISNULL(LC.CompName, PA.LastChgComp)")
        strSQL.AppendLine(", LastChgID = ISNULL(LP.NameN, PA.LastChgID)")
        strSQL.AppendLine(", LastChgDate = CASE WHEN CONVERT(VARCHAR, PA.LastChgDate, 111) = '1900/01/01' THEN '' ELSE CONVERT(VARCHAR, PA.LastChgDate, 120) END")
        strSQL.AppendLine("FROM POSPPara PA")
        strSQL.AppendLine("LEFT JOIN " + Bsp.Utility.getAppSetting("eHRMSDB") + "..Company LC ON PA.LastChgComp = LC.CompID")
        strSQL.AppendLine("LEFT JOIN " + Bsp.Utility.getAppSetting("eHRMSDB") + "..Personal LP ON PA.LastChgComp = LP.CompID AND PA.LastChgID = LP.EmpID")
        strSQL.AppendLine("WHERE 1=1")
        strSQL.AppendLine("AND PA.CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine("AND PA.Category = " & Bsp.Utility.Quote(Category))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "AattendantDB").Tables(0)
    End Function

    Public Function POSPParaAdd(ByVal ParamArray Params() As String) As Boolean
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("AattendantDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                strSQL.AppendLine("DELETE POSPPara WHERE CompID=@CompID")
                strSQL.AppendLine("INSERT INTO POSPPara (CompID, Category, HoldingRankIDFlag, HoldingRankID, PositionIDFlag, PositionID, WorkTypeIDFlag, WorkTypeID, LastChgComp, LastChgID, LastChgDate)")
                strSQL.AppendLine("VALUES (@CompID, @Category, @HoldingRankIDFlag, @HoldingRankID, @PositionIDFlag, @PositionID, @WorkTypeIDFlag, @WorkTypeID, @LastChgComp, @LastChgID, @LastChgDate)")

                Dim DbParam() As DbParameter = { _
                    Bsp.DB.getDbParameter("@CompID", ht("CompID").ToString), _
                    Bsp.DB.getDbParameter("@Category", ht("Category").ToString), _
                    Bsp.DB.getDbParameter("@HoldingRankIDFlag", ht("HoldingRankIDFlag").ToString), _
                    Bsp.DB.getDbParameter("@HoldingRankID", ht("HoldingRankID").ToString), _
                    Bsp.DB.getDbParameter("@PositionIDFlag", ht("PositionIDFlag").ToString), _
                    Bsp.DB.getDbParameter("@PositionID", ht("PositionID").ToString), _
                    Bsp.DB.getDbParameter("@WorkTypeIDFlag", ht("WorkTypeIDFlag").ToString), _
                    Bsp.DB.getDbParameter("@WorkTypeID", ht("WorkTypeID").ToString), _
                    Bsp.DB.getDbParameter("@LastChgComp", UserProfile.ActCompID), _
                    Bsp.DB.getDbParameter("@LastChgID", UserProfile.ActUserID), _
                    Bsp.DB.getDbParameter("@LastChgDate", Now)}

                If Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), DbParam, tran, "AattendantDB") = 0 Then Return False

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

    Public Function GetPOSPUser(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("SELECT PU.CompID, C.CompName, PU.EmpID, P.NameN, PU.OrgList, PU.OrgFlowList")
        strSQL.AppendLine(", LastChgComp = ISNULL(LC.CompName, PU.LastChgComp)")
        strSQL.AppendLine(", LastChgID = ISNULL(LP.NameN, PU.LastChgID)")
        strSQL.AppendLine(", LastChgDate = CASE WHEN CONVERT(VARCHAR, PU.LastChgDate, 111) = '1900/01/01' THEN '' ELSE CONVERT(VARCHAR, PU.LastChgDate, 120) END")
        strSQL.AppendLine("FROM POSPUser PU")
        strSQL.AppendLine("LEFT JOIN " + Bsp.Utility.getAppSetting("eHRMSDB") + "..Company C ON PU.CompID = C.CompID")
        strSQL.AppendLine("LEFT JOIN " + Bsp.Utility.getAppSetting("eHRMSDB") + "..Personal P ON PU.CompID = P.CompID AND PU.EmpID = P.EmpID")
        strSQL.AppendLine("LEFT JOIN " + Bsp.Utility.getAppSetting("eHRMSDB") + "..Company LC ON PU.LastChgComp = LC.CompID")
        strSQL.AppendLine("LEFT JOIN " + Bsp.Utility.getAppSetting("eHRMSDB") + "..Personal LP ON PU.LastChgComp = LP.CompID AND PU.LastChgID = LP.EmpID")
        strSQL.AppendLine("WHERE 1=1")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine("AND PU.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "EmpID"
                        strSQL.AppendLine("AND UPPER(PU.EmpID) = UPPER(" & Bsp.Utility.Quote(ht(strKey).ToString()) & ")")
                    Case "NameN"
                        strSQL.AppendLine("AND P.NameN LIKE N" & Bsp.Utility.Quote("%" & ht(strKey).ToString() & "%"))
                    Case "Category"
                        strSQL.AppendLine("AND PU.Category = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "AattendantDB").Tables(0)
    End Function

    Public Function POSPUserAdd(ByVal ParamArray Params() As String) As Boolean
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("AattendantDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                strSQL.AppendLine("INSERT INTO POSPUser (CompID, EmpID, Category, OrgList, OrgFlowList, LastChgComp, LastChgID, LastChgDate)")
                strSQL.AppendLine("VALUES (@CompID, @EmpID, Category, @OrgList, @OrgFlowList, @LastChgComp, @LastChgID, @LastChgDate)")

                Dim DbParam() As DbParameter = { _
                    Bsp.DB.getDbParameter("@CompID", ht("CompID").ToString), _
                    Bsp.DB.getDbParameter("@EmpID", ht("EmpID").ToString), _
                    Bsp.DB.getDbParameter("@Category", ht("Category").ToString), _
                    Bsp.DB.getDbParameter("@OrgList", ht("OrgList").ToString), _
                    Bsp.DB.getDbParameter("@OrgFlowList", ht("OrgFlowList").ToString), _
                    Bsp.DB.getDbParameter("@LastChgComp", UserProfile.ActCompID), _
                    Bsp.DB.getDbParameter("@LastChgID", UserProfile.ActUserID), _
                    Bsp.DB.getDbParameter("@LastChgDate", Now)}

                If Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), DbParam, tran, "AattendantDB") = 0 Then Return False

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

    Public Function POSPUserUpdate(ByVal ParamArray Params() As String) As Boolean
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("AattendantDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                strSQL.AppendLine("UPDATE POSPUser")
                strSQL.AppendLine("SET OrgList = @OrgList")
                strSQL.AppendLine(", OrgFlowList = @OrgFlowList")
                strSQL.AppendLine(", LastChgComp = @LastChgComp")
                strSQL.AppendLine(", LastChgID = @LastChgID")
                strSQL.AppendLine(", LastChgDate = @LastChgDate")
                strSQL.AppendLine("WHERE CompID = @CompID")
                strSQL.AppendLine("AND EmpID = @EmpID")
                strSQL.AppendLine("AND Category = @Category")

                Dim DbParam() As DbParameter = { _
                    Bsp.DB.getDbParameter("@CompID", ht("CompID").ToString), _
                    Bsp.DB.getDbParameter("@EmpID", ht("EmpID").ToString), _
                    Bsp.DB.getDbParameter("@Category", ht("Category").ToString), _
                    Bsp.DB.getDbParameter("@OrgList", ht("OrgList").ToString), _
                    Bsp.DB.getDbParameter("@OrgFlowList", ht("OrgFlowList").ToString), _
                    Bsp.DB.getDbParameter("@LastChgComp", UserProfile.ActCompID), _
                    Bsp.DB.getDbParameter("@LastChgID", UserProfile.ActUserID), _
                    Bsp.DB.getDbParameter("@LastChgDate", Now)}

                If Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), DbParam, tran, "AattendantDB") = 0 Then Return False

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

    Public Function POSPUserDelete(ByVal ParamArray Params() As String) As Boolean
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("AattendantDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                strSQL.AppendLine("DELETE POSPUser")
                strSQL.AppendLine("WHERE CompID = @CompID")
                strSQL.AppendLine("AND EmpID = @EmpID")
                strSQL.AppendLine("AND Category = @Category")

                Dim DbParam() As DbParameter = { _
                    Bsp.DB.getDbParameter("@CompID", ht("CompID").ToString), _
                    Bsp.DB.getDbParameter("@EmpID", ht("EmpID").ToString), _
                    Bsp.DB.getDbParameter("@Category", ht("Category").ToString)}

                If Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), DbParam, tran, "AattendantDB") = 0 Then Return False

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

    Public Function GetOrgMenu(ByVal CompID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select OrgType, DeptID, OrganID")
        strSQL.AppendLine(", OrganID + '-' + OrganName As OrganName")
        strSQL.AppendLine(", SortNode = Case When OrgType = DeptID Then '1' Else '2' End")
        strSQL.AppendLine(", IsNull(W.Color, '#FFFFFF') Color")
        strSQL.AppendLine(", O.InValidFlag, O.SortOrder, Right(O.GroupType, 1), O.GroupID")
        strSQL.AppendLine("From Organization O With (NoLock)")
        strSQL.AppendLine("Left Join OrganColor_Web W On O.CompID = W.CompID and O.SortOrder = W.SortOrder")
        strSQL.AppendLine("Where O.CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine("And O.DeptID = O.OrganID")
        strSQL.AppendLine("And O.VirtualFlag = '0'")
        strSQL.AppendLine("And O.InValidFlag = '0'")
        strSQL.AppendLine("Order By O.InValidFlag, O.SortOrder, Right(O.GroupType, 1), O.GroupID, O.OrgType, O.OrganID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "eHRMSDB").Tables(0)
    End Function
#End Region

    Public Function IsDataExists(ByVal strTable As String, ByVal strWhere As String) As Boolean
        Dim strSQL As String
        strSQL = "Select Count(*) Cnt From " & strTable & " Where 1 = 1 " & strWhere
        Return IIf(Bsp.DB.ExecuteScalar(strSQL, "AattendantDB") > 0, True, False)
    End Function

End Class
