'****************************************************
'功能說明：公司參數設定
'建立人員：MickySung
'建立日期：2015/04/09
'****************************************************
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.Common

Public Class PA1

    Enum DisplayType
        OnlyName    '只顯示名字
        OnlyID      '顯示ID  
        Full        '顯示ID + 名字
    End Enum

#Region "PA1100 新公司成立設定查詢功能"
    Public Function GetCompanySetting(ByVal CompRoleID As String, ByVal ConnStr As String) As DataTable
        Dim strSQL As New StringBuilder

        If ConnStr = "HRDB" Then
            strSQL.AppendLine(" SELECT Count(*) FROM SC_UserGroup A ")
            strSQL.AppendLine(" LEFT JOIN SC_Admin B ON A.UserID = B.AdminID ")
            strSQL.AppendLine(" WHERE A.CompID = " + Bsp.Utility.Quote(CompRoleID))
            strSQL.AppendLine(" AND B.AdminID is NULL ")

        ElseIf ConnStr = "eHRMSDB" Then
            strSQL.AppendLine(" Select ")
            strSQL.AppendLine(" cb_Rank = (SELECT COUNT(*) FROM Rank WHERE CompID = " + Bsp.Utility.Quote(CompRoleID) + ") ")
            strSQL.AppendLine(", cb_Title = (SELECT COUNT(*) FROM Title WHERE CompID = " + Bsp.Utility.Quote(CompRoleID) + ") ")
            strSQL.AppendLine(", cb_CompareRank = (SELECT COUNT(*) FROM CompareRank WHERE CompID = " + Bsp.Utility.Quote(CompRoleID) + ") ")
            strSQL.AppendLine(", cb_RankMapping = (SELECT COUNT(*) FROM RankMapping WHERE CompID = " + Bsp.Utility.Quote(CompRoleID) + ") ")
            strSQL.AppendLine(", cb_WorkType = (SELECT COUNT(*) FROM WorkType WHERE CompID = " + Bsp.Utility.Quote(CompRoleID) + ") ")
            strSQL.AppendLine(", cb_Position = (SELECT COUNT(*) FROM Position WHERE CompID = " + Bsp.Utility.Quote(CompRoleID) + ") ")
            strSQL.AppendLine(", cb_WorkSite = (SELECT COUNT(*) FROM WorkSite WHERE CompID = " + Bsp.Utility.Quote(CompRoleID) + ") ")
            strSQL.AppendLine(", cb_Calendar = (SELECT COUNT(*) FROM Calendar WHERE CompID = " + Bsp.Utility.Quote(CompRoleID) + ") ")
            strSQL.AppendLine(", cb_Organization = (SELECT COUNT(*) FROM Organization WHERE CompID = " + Bsp.Utility.Quote(CompRoleID) + ") ")
            strSQL.AppendLine(", cb_OrganizationFlow = (SELECT COUNT(*) FROM OrganizationFlow WHERE CompID = " + Bsp.Utility.Quote(CompRoleID) + ") ")
            strSQL.AppendLine(", cb_WorkTime = (SELECT COUNT(*) FROM WorkTime WHERE CompID = " + Bsp.Utility.Quote(CompRoleID) + ") ")
        End If

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), ConnStr).Tables(0)
    End Function

#End Region

#Region "PA1200 公司名稱設定"
#Region "PA1200 公司名稱設定-查詢"
    Public Function GetCompanyNameSetting(ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" Select ")
        strSQL.AppendLine(" C.CompID, C.CompName, C.FeeShareFlag, C.CheckInFile, C.Calendar ")
        strSQL.AppendLine(" , C.Payroll, C.SPHSC1GrpFlag, C.InValidFlag, C.NotShowFlag, C.HRISFlag ")
        strSQL.AppendLine(" , RankIDMapValidDate = Case When Convert(Char(10), C.RankIDMapValidDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, C.RankIDMapValidDate, 111) End ") '20151203 Beatrice Add
        strSQL.AppendLine(" , C.RankIDMapFlag, C.NotShowRankID, C.NotShowWorkType, HR.CodeCName, C.CNFlag ")
        strSQL.AppendLine(" , Comp.CompName AS LastChgComp, Pers.NameN AS LastChgID ")
        strSQL.AppendLine(" , LastChgDate = Case When Convert(Char(10), C.LastChgDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, C.LastChgDate, 120) End ")
        strSQL.AppendLine(" , EmpSource = Case C.EmpSource When '' Then '' When '1' Then 'wHRMS' When '2' Then 'IPL' When '3' Then '海外人事系統' When '4' Then '南京子行' End ")
        strSQL.AppendLine(" FROM Company C ")
        strSQL.AppendLine(" LEFT JOIN HRCodeMap HR ON HR.TabName = 'Company' AND HR.FldName = 'EmpSource' AND C.EmpSource = HR.Code ")
        strSQL.AppendLine(" LEFT JOIN Company Comp ON C.LastChgComp = Comp.CompID ")
        strSQL.AppendLine(" LEFT JOIN Personal Pers ON C.LastChgComp = Pers.CompID AND C.LastChgID = Pers.EmpID ")
        strSQL.AppendLine(" WHERE 1=1 ")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine(" AND C.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "CompName"
                        strSQL.AppendLine(" AND C.CompName LIKE N'%" & ht(strKey).ToString() + "%' ")
                    Case "CompEngID"
                        strSQL.AppendLine(" AND C.CompEngName LIKE '%" & ht(strKey).ToString() + "%' ")
                    Case "CompChID"
                        strSQL.AppendLine(" AND C.CompChnName LIKE N'%" & ht(strKey).ToString() + "%' ")
                    Case "InValidFlag"
                        strSQL.AppendLine(" AND C.InValidFlag = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "NotShowFlag"
                        strSQL.AppendLine(" AND C.NotShowFlag = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "HRISFlag"
                        strSQL.AppendLine(" AND C.HRISFlag = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "RankIDMapFlag"
                        strSQL.AppendLine(" AND C.RankIDMapFlag = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "NotShowRankID"
                        strSQL.AppendLine(" AND C.NotShowRankID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "NotShowWorkType"
                        strSQL.AppendLine(" AND C.NotShowWorkType = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "FeeShareFlag"
                        strSQL.AppendLine(" AND C.FeeShareFlag = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "SPHSC1GrpFlag"
                        strSQL.AppendLine(" AND C.SPHSC1GrpFlag = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "EmpSource"
                        strSQL.AppendLine(" AND C.EmpSource = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "CNFlag"
                        strSQL.AppendLine(" AND C.CNFlag = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next
        strSQL.AppendLine(" ORDER BY C.CompID ")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1201 公司名稱設定-新增"
    Public Function AddCompanySetting(ByVal beCompany As beCompany.Row, ByVal beSC_Company As beSC_Company.Row) As Boolean
        Dim bsCompany As New beCompany.Service()
        Dim bsSC_Company As New beSC_Company.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                'Company
                If bsCompany.Insert(beCompany, tran) = 0 Then
                    Return False
                Else
                    '2015/05/25 新增Parameter
                    strSQL.AppendLine(" INSERT INTO Parameter ")
                    strSQL.AppendLine(" (CompID    ,PayDate   ,PayDateSeq    ,AccountDate  ,BusinessDate  ,BatchDate   ,HRSignID   ,OTHourM   ,OTHourF   ,OTZoneHour ")
                    strSQL.AppendLine(" ,OTMixHour   ,OTMaxHour   ,SPHSC1HolidayH1  ,SPHSC1HolidayH2 ,OTNormal   ,OTNormalDiv  ,OTOver    ,OTOverDiv   ,OTHoliday   ,OTHolidayDiv ")
                    strSQL.AppendLine(" ,SPHSC1Holiday1  ,SPHSC1Holiday1Div ,SPHSC1Holiday2   ,SPHSC1Holiday2Div ,SPHSC1Holiday3  ,SPHSC1Holiday3Div ,RetireStaff  ,RetireHirer  ,StandardDeduct  ,SalaryDeduct ")
                    strSQL.AppendLine(" ,RearDeduct   ,TaxFreeBase  ,TaxGrossPay2_TaxBase ,FesFlag   ,FesYear   ,FesMths   ,FesDate   ,FesAmountBase  ,FesPayDate1  ,FesPayDate2 ")
                    strSQL.AppendLine(" ,AchMths   ,SalaryMths   ,HLBSalaryFlag   ,DESKey    ,DESKeyNew   ,DESChgID   ,DESChgDate   ,PayrollDate1  ,PayrollDate2  ,PayrollMonth ")
                    strSQL.AppendLine(" ,PayrollRankID  ,PayrollRankIDMap ,OTReleaseDate   ,OTRestDate   ,OTRestDateRun  ,ChgAPPassFlag  ,RetireDate1  ,RetireDate2  ,RetireNewDate1  ,RetireNewDate2 ")
                    strSQL.AppendLine(" ,TaxDate1   ,TaxDate2   ,PayDay     ,SiteAmountN  ,SiteAmountS  ,CalculateStockTrust,StockTrustSignComp ,StockTrustSignID ,StockTrustDate1 ,StockTrustDate2 ")
                    strSQL.AppendLine(" ,GroupStartDate  ,GroupEndDate  ,GroupRange    ,TaxOptionDate1  ,TaxOptionDate2  ,GradeFlag   ,GradeYear   ,OnlyBankFlag  ,MealPay   ,LastChgComp ")
                    strSQL.AppendLine(" ,LastChgID ")
                    strSQL.AppendLine(" ,LastChgDate) ")
                    strSQL.AppendLine(" values ")
                    strSQL.AppendLine(" ('" + beCompany.CompID.Value + "'  ,'0'    ,'1'     ,'1900/01/01'  ,''     ,'1900/01/01'  ,''     ,'46'    ,'32'    ,'2' ")
                    strSQL.AppendLine(" ,'4'    ,'8'    ,'8'     ,'2'    ,'1'    ,'1'    ,'1'    ,'1'    ,'2'    ,'1' ")
                    strSQL.AppendLine(" ,'1'    ,'1'    ,'1'     ,'1'    ,'1'    ,'1'    ,'4'    ,'4'    ,'0'    ,'0' ")
                    strSQL.AppendLine(" ,'0'    ,'0'    ,'0'     ,'0'    ,'1900'    ,0.00    ,'1900/01/01'  ,'0'    ,'0'    ,'0' ")
                    strSQL.AppendLine(" ,0.00    ,0     ,'0'     ,''     ,''     ,''     ,'1900/01/01'  ,'1900/01/01'  ,'1900/01/01'  ,0 ")
                    strSQL.AppendLine(" ,'99'    ,'99'    ,'1900/01/01'   ,'1900/01/01'  ,'1900/01/01'  ,'0'    ,'1900/01/01'  ,'1900/01/01'  ,'1900/01/01'  ,'1900/01/01' ")
                    strSQL.AppendLine(" ,'1900/01/01'  ,'1900/01/01'  ,'15'     ,10000    ,8000    ,'0'    ,''     ,''     ,'1900/01/01'  ,'1900/01/01' ")
                    strSQL.AppendLine(" ,'1900/01/01'  ,'1900/01/01'  ,0      ,'1900/01/01'  ,'1900/01/01'  ,'0'    ,0     ,'0'    ,0     ,'SPHBK1' ")
                    strSQL.AppendLine(" ,'BATCH'   ,getdate()) ")

                    '2015/05/25 新增HRCodeMap
                    strSQL.AppendLine(" INSERT INTO HRCodeMap ")
                    strSQL.AppendLine(" (TabName, FldName, Code, CodeCName, SortFld, NotShowFlag, LastChgComp, LastChgID, LastChgDate) ")
                    strSQL.AppendLine(" VALUES ('BankID','" + beCompany.CompID.Value + "','807','','1','0','','','1900/01/01') ")
                    Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB")
                    strSQL.Clear()

                    'SC_UserGroup
                    strSQL.AppendLine(" INSERT INTO SC_UserGroup ")
                    strSQL.AppendLine(" SELECT S.AdminComp, S.AdminID, S.SysID, '" + beCompany.CompID.Value + "', 'Admin' ")
                    strSQL.AppendLine(" , getdate(), '" + UserProfile.ActCompID + "','" + UserProfile.ActUserID + "',getdate() ")
                    strSQL.AppendLine(" FROM SC_Admin S ")
                    strSQL.AppendLine(" WHERE SysID = 'wHRMS' ")

                    'SC_GroupFun
                    strSQL.AppendLine(" INSERT INTO SC_GroupFun ")
                    strSQL.AppendLine(" SELECT distinct SysID, '" + beCompany.CompID.Value + "', 'Admin', FunID, RightID, getdate() ")
                    strSQL.AppendLine(" , '" + UserProfile.ActCompID + "','" + UserProfile.ActUserID + "',getdate() ")
                    strSQL.AppendLine(" FROM SC_GroupFun ")
                    strSQL.AppendLine(" WHERE GroupID = 'Admin' ")
                    Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "HRDB")
                End If

                tran.Commit()
            Catch ex As Exception
                tran.Rollback()
                Throw
            Finally
                If tran IsNot Nothing Then tran.Dispose()
            End Try
        End Using

        '2015/05/25 新增SC_Company
        Using cn As DbConnection = Bsp.DB.getConnection("HRDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Try
                If bsSC_Company.Insert(beSC_Company, tran) = 0 Then Return False
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

#Region "PA1202 公司名稱設定-修改"
    Public Function UpdateCompanySetting(ByVal beCompany As beCompany.Row, ByVal beSC_Company As beSC_Company.Row) As Boolean
        Dim bsCompany As New beCompany.Service()
        Dim bsSC_Company As New beSC_Company.Service()

        'Company
        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsCompany.Update(beCompany, tran) = 0 Then Return False
                tran.Commit()
            Catch ex As Exception
                tran.Rollback()
                Throw
            Finally
                If tran IsNot Nothing Then tran.Dispose()
            End Try
        End Using

        '2015/05/25 新增SC_Company
        Using cn As DbConnection = Bsp.DB.getConnection("HRDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsSC_Company.Update(beSC_Company, tran) = 0 Then Return False
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

#Region "PA1200 公司名稱設定-刪除"
    Public Function DeleteCompany(ByVal beCompany As beCompany.Row, ByVal beSC_Company As beSC_Company.Row) As Boolean
        Dim bsCompany As New beCompany.Service()
        Dim bsHRCodeMap As New beHRCodeMap.Service()
        Dim bsSC_Company As New beSC_Company.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As Data.Common.DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                'Company
                bsCompany.DeleteRowByPrimaryKey(beCompany, tran)

                '2015/05/29 刪除Parameter
                strSQL.AppendLine(" DELETE FROM Parameter WHERE CompID='" + beCompany.CompID.Value + "' ")

                '2015/05/29 刪除HRCodeMap
                strSQL.AppendLine(" DELETE FROM HRCodeMap WHERE TabName = 'BankID' AND FldName='" + beCompany.CompID.Value + "' AND  Code = '807' ")

                Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), tran)

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

        strSQL.Clear()

        Using cn As DbConnection = Bsp.DB.getConnection("HRDB")
            cn.Open()
            Dim tran As Data.Common.DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                'SC_UserGroup
                strSQL.AppendLine(" DELETE FROM SC_UserGroup WHERE CompRoleID='" + beCompany.CompID.Value + "' ")
                'SC_GroupFun
                strSQL.AppendLine(" DELETE FROM SC_GroupFun WHERE CompRoleID='" + beCompany.CompID.Value + "' ")
                Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), tran)

                'SC_Company
                bsSC_Company.DeleteRowByPrimaryKey(beSC_Company, tran)

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

#Region "PA1200 查詢Organization是否存在"
    Public Function checkOrganization(ByVal CompID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select Count(*) Cnt From Organization")
        strSQL.AppendLine("Where CompID = " + Bsp.Utility.Quote(CompID))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1200 查詢OrganizationFlow是否存在"
    Public Function checkOrganizationFlow(ByVal CompID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select Count(*) Cnt From OrganizationFlow")
        strSQL.AppendLine("Where CompID = " + Bsp.Utility.Quote(CompID))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1200 查詢SC_UserGroup是否存在"
    Public Function checkSC_UserGroup(ByVal CompID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" Select Count(*) Cnt From SC_UserGroup ")
        strSQL.AppendLine(" WHERE CompRoleID = " + Bsp.Utility.Quote(CompID) + " AND GroupID <> 'Admin'")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "HRDB").Tables(0)
    End Function
#End Region

#Region "PA1200 查詢SC_GroupFun是否存在"
    Public Function checkSC_GroupFun(ByVal CompID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" Select Count(*) Cnt From SC_GroupFun ")
        strSQL.AppendLine(" WHERE CompRoleID = " + Bsp.Utility.Quote(CompID) + " AND GroupID <> 'Admin'")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "HRDB").Tables(0)
    End Function
#End Region

#Region "PA1202 員工資料來源下拉選單"
    Public Shared Sub FillEmpSource(ByVal objDDL As DropDownList, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objPA As New PA1

        Try
            Using dt As DataTable = objPA.GetEmpSource()
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "FullName"
                    .DataValueField = "Code"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetEmpSource() As DataTable
        Dim strSQL As String

        strSQL = " SELECT DISTINCT Code, Code + '-' + CodeCName AS FullName FROM HRCodeMap WHERE TabName = 'Company' AND FldName = 'EmpSource'"

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region
#End Region

#Region "PA1300 職等代碼設定"
#Region "PA1300 職等代碼設定-查詢"
    Public Function RankSettingQuery(ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" Select ")
        strSQL.AppendLine(" R.CompID, C.CompName, R.RankID, R.FixAmount, R.YearHolidays, R.GrpLvl ")
        strSQL.AppendLine(" , Comp.CompName AS LastChgComp, Pers.NameN AS LastChgID ")
        strSQL.AppendLine(" , LastChgDate = Case When Convert(Char(10), R.LastChgDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, R.LastChgDate, 120) End ")
        strSQL.AppendLine(" FROM Rank R ")
        strSQL.AppendLine(" LEFT JOIN Company C ON C.CompID = R.CompID ")
        strSQL.AppendLine(" LEFT JOIN Company Comp ON R.LastChgComp = Comp.CompID ")
        strSQL.AppendLine(" LEFT JOIN Personal Pers ON R.LastChgComp = Pers.CompID AND R.LastChgID = Pers.EmpID ")
        strSQL.AppendLine(" WHERE 1=1 ")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine(" AND R.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "RankID"
                        strSQL.AppendLine(" AND R.RankID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next
        strSQL.AppendLine(" ORDER BY R.RankID ")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1301 職等代碼設定-新增"
    Public Function AddRankSetting(ByVal beRank As beRank.Row, ByVal chkHoldingRankID As Boolean, ByVal chkRankIDMap As Boolean) As Boolean
        Dim bsRank As New beRank.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsRank.Insert(beRank, tran) = 0 Then
                    Return False
                Else
                    If chkHoldingRankID = True Then
                        strSQL.AppendLine(" Insert into CompareRank ")
                        strSQL.AppendLine(" Select RankID, CompID, RankID, '" + UserProfile.ActCompID + "', '" + UserProfile.ActUserID + "', getdate() ")
                        strSQL.AppendLine(" FROM Rank ")
                        strSQL.AppendLine(" WHERE RankID = '" + beRank.RankID.Value + "'")
                        strSQL.AppendLine(" AND CompID = '" + beRank.CompID.Value + "'")
                        Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), tran, "eHRMSDB")
                    End If
                    strSQL.Clear()

                    If chkRankIDMap = True Then
                        strSQL.AppendLine(" Insert into RankMapping ")
                        strSQL.AppendLine(" Select CompID, RankID, RankID, '" + UserProfile.ActCompID + "', '" + UserProfile.ActUserID + "', getdate() ")
                        strSQL.AppendLine(" FROM Rank ")
                        strSQL.AppendLine(" WHERE CompID = '" + beRank.CompID.Value + "'")
                        strSQL.AppendLine(" AND RankID = '" + beRank.RankID.Value + "'")
                        Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), tran, "eHRMSDB")
                    End If
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

#Region "PA1302 職等代碼設定-修改"
    Public Function UpdateRankSetting(ByVal beRank As beRank.Row) As Boolean
        Dim bsRank As New beRank.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsRank.Update(beRank, tran) = 0 Then Return False
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

#Region "PA1300 職等代碼設定-刪除"
    Public Function DeleteRankSetting(ByVal beRank As beRank.Row, ByVal beCompareRank As beCompareRank.Row, ByVal beRankMapping As beRankMapping.Row) As Boolean
        Dim bsRank As New beRank.Service()
        Dim bsCompareRank As New beCompareRank.Service()
        Dim bsRankMapping As New beRankMapping.Service()

        Dim strSQL As New StringBuilder()
        Dim strSQL2 As New StringBuilder()
        Dim strSQL3 As New StringBuilder()

        strSQL.AppendLine(" Delete From Rank ")
        strSQL.AppendLine(" WHERE CompID = " & Bsp.Utility.Quote(beRank.CompID.Value))
        strSQL.AppendLine(" AND RankID = " & Bsp.Utility.Quote(beRank.RankID.Value))

        strSQL2.AppendLine(" Delete From CompareRank ")
        strSQL2.AppendLine(" WHERE HoldingRankID = " & Bsp.Utility.Quote(beCompareRank.HoldingRankID.Value))
        strSQL2.AppendLine(" AND CompID = " & Bsp.Utility.Quote(beCompareRank.CompID.Value))
        strSQL2.AppendLine(" AND RankID = " & Bsp.Utility.Quote(beCompareRank.RankID.Value))

        strSQL3.AppendLine(" Delete From RankMapping ")
        strSQL3.AppendLine(" WHERE CompID = " & Bsp.Utility.Quote(beRankMapping.CompID.Value))
        strSQL3.AppendLine(" AND RankID = " & Bsp.Utility.Quote(beRankMapping.RankID.Value))
        strSQL3.AppendLine(" AND RankIDMap = " & Bsp.Utility.Quote(beRankMapping.RankIDMap.Value))

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As Data.Common.DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                bsRank.DeleteRowByPrimaryKey(beRank, tran)
                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)

                bsCompareRank.DeleteRowByPrimaryKey(beCompareRank, tran)
                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL2.ToString(), tran)

                bsRankMapping.DeleteRowByPrimaryKey(beRankMapping, tran)
                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL3.ToString(), tran)

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

#Region "職等代碼下拉選單"
    Public Shared Sub FillRank(ByVal objDDL As DropDownList, ByVal CompID As String)
        Dim objPA As New PA1
        Try
            Using dt As DataTable = objPA.GetRankInfo(CompID)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "RankID"
                    .DataValueField = "RankID"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Function GetRankInfo(ByVal CompID As String, Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder
        strSQL.AppendLine("Select DISTINCT RankID From Rank Where 1=1 AND CompID='" + CompID + "' ")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1300 查詢Title是否有使用該RankID"
    Public Function checkTitle(ByVal CompID As String, ByVal RankID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select Count(*) Cnt From Title")
        strSQL.AppendLine("Where CompID = '" + CompID + "' AND RankID='" + RankID + "' ")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1300 查詢Personal是否有使用該RankID"
    Public Function checkPersonal_PA1300(ByVal CompID As String, ByVal RankID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select Count(*) Cnt From Personal")
        strSQL.AppendLine("Where CompID = '" + CompID + "' AND RankID='" + RankID + "' ")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region
#End Region

#Region "PA1400 職稱代碼設定"
#Region "PA1400 職等代碼下拉選單"
    Public Shared Sub FillRankID(ByVal objDDL As DropDownList, ByVal CompID As String, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objPA As New PA1

        Try
            Using dt As DataTable = objPA.GetRankIDInfo(CompID)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "RankID"
                    .DataValueField = "RankID"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetRankIDInfo(ByVal CompID As String) As DataTable
        Dim strSQL As String

        '2015/07/27 Modify 規格變更 讀取Table改為Rank
        'strSQL = " select Distinct RankID from Title where CompID = " & Bsp.Utility.Quote(CompID)
        strSQL = " select Distinct RankID from Rank where CompID = " & Bsp.Utility.Quote(CompID)
        strSQL = strSQL & " order by RankID "

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1400 職稱代碼下拉選單"
    Public Shared Sub FillTitleID(ByVal objDDL As DropDownList, ByVal CompID As String, ByVal RankID As String, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objPA As New PA1

        Try
            Using dt As DataTable = objPA.GetTitleID(CompID, RankID)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "TitleFullName"
                    .DataValueField = "TitleID"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetTitleID(ByVal CompID As String, ByVal RankID As String) As DataTable
        Dim strSQL As String

        strSQL = " select Distinct TitleID, TitleID + '-' + TitleName As TitleFullName from Title where CompID = " & Bsp.Utility.Quote(CompID) + "AND RankID = " & Bsp.Utility.Quote(RankID)

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1400 職稱代碼設定-查詢"
    Public Function TitleSettingQuery(ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" Select ")
        strSQL.AppendLine(" T.CompID, C.CompName, T.RankID, T.TitleID, T.TitleName, T.TitleEngName ")
        strSQL.AppendLine(" , Comp.CompName AS LastChgComp, Pers.NameN AS LastChgID ")
        strSQL.AppendLine(" , LastChgDate = Case When Convert(Char(10), T.LastChgDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, T.LastChgDate, 120) End ")
        strSQL.AppendLine(" FROM Title T ")
        strSQL.AppendLine(" LEFT JOIN Company C ON C.CompID = T.CompID ")
        strSQL.AppendLine(" LEFT JOIN Company Comp ON T.LastChgComp = Comp.CompID ")
        strSQL.AppendLine(" LEFT JOIN Personal Pers ON T.LastChgComp = Pers.CompID AND T.LastChgID = Pers.EmpID ")
        strSQL.AppendLine(" WHERE 1=1 ")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine(" AND T.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "RankID"
                        strSQL.AppendLine(" AND T.RankID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "TitleID"
                        strSQL.AppendLine(" AND T.TitleID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next
        strSQL.AppendLine(" ORDER BY T.RankID, T.TitleID ")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1401 職稱代碼設定-新增"
    Public Function AddTitleSetting(ByVal beTitle As beTitle.Row) As Boolean
        Dim bsTitle As New beTitle.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsTitle.Insert(beTitle, tran) = 0 Then Return False
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

#Region "PA1402 職稱代碼設定-修改"
    Public Function UpdateTitleSetting(ByVal beTitle As beTitle.Row) As Boolean
        Dim bsTitle As New beTitle.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsTitle.Update(beTitle, tran) = 0 Then Return False
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

#Region "PA1400 職稱代碼設定-刪除"
    Public Function DeleteTitleSetting(ByVal beTitle As beTitle.Row) As Boolean
        Dim bsTitle As New beTitle.Service()
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" Delete From Title ")
        strSQL.AppendLine(" WHERE CompID = " & Bsp.Utility.Quote(beTitle.CompID.Value))
        strSQL.AppendLine(" AND RankID = " & Bsp.Utility.Quote(beTitle.RankID.Value))
        strSQL.AppendLine(" AND TitleID = " & Bsp.Utility.Quote(beTitle.TitleID.Value))

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As Data.Common.DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                bsTitle.DeleteRowByPrimaryKey(beTitle, tran)
                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)

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

#Region "PA1400 查詢Personal是否有使用該RankID,TitleID"
    Public Function checkPersonal_PA1400(ByVal CompID As String, ByVal RankID As String, ByVal TitleID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select Count(*) Cnt From Personal")
        strSQL.AppendLine("Where CompID = '" + CompID + "' AND RankID='" + RankID + "' AND TitleID='" + TitleID + "' ")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region
#End Region

#Region "PA1500 金控職等對照設定"
#Region "PA1500 金控職等下拉選單"
    Public Shared Sub FillTitleByHolding(ByVal objDDL As DropDownList, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objPA As New PA1

        Try
            Using dt As DataTable = objPA.GetTitleByHolding()
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "FullName"
                    .DataValueField = "HoldingRankID"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetTitleByHolding() As DataTable
        Dim strSQL As String

        strSQL = " SELECT Distinct HoldingRankID, HoldingRankID + '-' + TitleName AS FullName from TitleByHolding "
        strSQL = strSQL & " ORDER BY HoldingRankID "

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

    Public Shared Sub FillTitleByHolding2(ByVal objDDL As DropDownList, ByVal CompID As String, ByVal RankID As String, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objPA As New PA1

        Try
            Using dt As DataTable = objPA.GetTitleByHolding2(CompID, RankID)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "HoldingRankID"
                    .DataValueField = "HoldingRankID"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetTitleByHolding2(ByVal CompID As String, ByVal RankID As String) As DataTable
        Dim strSQL As String

        strSQL = " SELECT HoldingRankID from CompareRank "
        strSQL = strSQL & " WHERE CompID='" + CompID + "' AND RankID='" + RankID + "' "
        strSQL = strSQL & " ORDER BY HoldingRankID "

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1500 公司職等下拉選單"
    Public Shared Sub FillRankID_PA1500(ByVal objDDL As DropDownList, ByVal CompID As String, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objPA As New PA1

        Try
            Using dt As DataTable = objPA.GetRankID_PA1500(CompID)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "RankID"
                    .DataValueField = "RankID"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetRankID_PA1500(ByVal CompID As String) As DataTable
        Dim strSQL As String

        strSQL = " SELECT Distinct RankID FROM Rank WHERE CompID='" + CompID + "' "
        strSQL = strSQL & " ORDER BY RankID "

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1500 金控職等對照設定-查詢"
    Public Function CompareRankSettingQuery(ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT ")
        strSQL.AppendLine(" T.CompID, C.CompName, T.RankID, T.HoldingRankID ")
        strSQL.AppendLine(" , Comp.CompName AS LastChgComp, Pers.NameN AS LastChgID ")
        strSQL.AppendLine(" , LastChgDate = Case When Convert(Char(10), T.LastChgDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, T.LastChgDate, 120) End ")
        strSQL.AppendLine(" FROM CompareRank T ")
        strSQL.AppendLine(" LEFT JOIN Company C ON C.CompID = T.CompID ")
        strSQL.AppendLine(" LEFT JOIN Company Comp ON T.LastChgComp = Comp.CompID ")
        strSQL.AppendLine(" LEFT JOIN Personal Pers ON T.LastChgComp = Pers.CompID AND T.LastChgID = Pers.EmpID ")
        strSQL.AppendLine(" WHERE 1=1 ")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine(" AND T.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "RankID"
                        strSQL.AppendLine(" AND T.RankID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "HoldingRankID"
                        strSQL.AppendLine(" AND T.HoldingRankID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next
        strSQL.AppendLine(" ORDER BY T.CompID, T.RankID, T.HoldingRankID ")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1501 金控職等對照設定-新增"
    Public Function AddCompareRankSetting(ByVal beCompareRank As beCompareRank.Row) As Boolean
        Dim bsCompareRank As New beCompareRank.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsCompareRank.Insert(beCompareRank, tran) = 0 Then Return False
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

#Region "PA1502 金控職等對照設定-修改"
    Public Function UpdateCompareRankSetting(ByVal beCompareRank As beCompareRank.Row, ByVal saveCompID As String, ByVal saveRankID As String, ByVal saveHoldingRankID As String) As Boolean
        Dim bsCompareRank As New beCompareRank.Service()
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" UPDATE CompareRank SET RankID='" + beCompareRank.RankID.Value.ToString + "',HoldingRankID='" + beCompareRank.HoldingRankID.Value.ToString + "' ")
        strSQL.AppendLine(" ,LastChgComp='" + beCompareRank.LastChgComp.Value.ToString + "',LastChgID='" + beCompareRank.LastChgID.Value.ToString + "',LastChgDate= GETDATE() ")
        strSQL.AppendLine(" WHERE CompID='" + saveCompID + "' AND RankID='" + saveRankID + "' AND HoldingRankID='" + saveHoldingRankID + "' ")
        Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB")

        Return True
    End Function
#End Region

#Region "PA1500 公司職等是否已有對照金控職等"
    '2015/07/21新增防呆，【一個公司職等】只能對照【一個金控職等】
    Public Function SelectHoldingRankID(ByVal CompID As String, ByVal RankID As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" SELECT Count(*) AS Count FROM CompareRank WHERE CompID = " & Bsp.Utility.Quote(CompID.ToString()) & " AND RankID = " & Bsp.Utility.Quote(RankID.ToString()))
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1502 公司職等下拉選單"
    Public Shared Sub FillRankID_PA1502(ByVal objDDL As DropDownList, ByVal CompID As String, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objPA As New PA1

        Try
            Using dt As DataTable = objPA.GetRankID_PA1502(CompID)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "RankID"
                    .DataValueField = "RankID"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetRankID_PA1502(ByVal CompID As String) As DataTable
        Dim strSQL As String

        strSQL = " SELECT Distinct RankID FROM CompareRank WHERE CompID='" + CompID + "' "
        strSQL = strSQL & " ORDER BY RankID "

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#End Region

#Region "PA1600 職等對照設定"
#Region "PA1600 公司職等下拉選單"
    Public Shared Sub FillRankID_PA1600(ByVal objDDL As DropDownList, ByVal CompID As String, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objPA As New PA1

        Try
            Using dt As DataTable = objPA.GetRankID_PA1600(CompID)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "RankID"
                    .DataValueField = "RankID"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetRankID_PA1600(ByVal CompID As String) As DataTable
        Dim strSQL As String

        strSQL = "SELECT DISTINCT RankID FROM RankMapping WHERE CompID='" + CompID + "'"

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1600 對照職等下拉選單"
    Public Shared Sub FillRankIDMap_PA1600(ByVal objDDL As DropDownList, ByVal CompID As String, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objPA As New PA1

        Try
            Using dt As DataTable = objPA.GetRankIDMap_PA1600(CompID)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "RankIDMap"
                    .DataValueField = "RankIDMap"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetRankIDMap_PA1600(ByVal CompID As String) As DataTable
        Dim strSQL As String

        strSQL = " SELECT DISTINCT RankIDMap FROM RankMapping WHERE CompID='" + CompID + "' "

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1600 職等對照設定-查詢"
    Public Function RankMappingSettingQuery(ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT ")
        strSQL.AppendLine(" R.CompID, C.CompName, R.RankID, R.RankIDMap ")
        strSQL.AppendLine(" , Comp.CompName AS LastChgComp, Pers.NameN AS LastChgID ")
        strSQL.AppendLine(" , LastChgDate = Case When Convert(Char(10), R.LastChgDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, R.LastChgDate, 120) End ")
        strSQL.AppendLine(" FROM RankMapping R ")
        strSQL.AppendLine(" LEFT JOIN Company C ON C.CompID = R.CompID ")
        strSQL.AppendLine(" LEFT JOIN Company Comp ON R.LastChgComp = Comp.CompID ")
        strSQL.AppendLine(" LEFT JOIN Personal Pers ON R.LastChgComp = Pers.CompID AND R.LastChgID = Pers.EmpID ")
        strSQL.AppendLine(" WHERE 1=1 ")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine(" AND R.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "RankID"
                        strSQL.AppendLine(" AND R.RankID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "RankIDMap"
                        strSQL.AppendLine(" AND R.RankIDMap = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next
        strSQL.AppendLine(" ORDER BY R.CompID, R.RankID, R.RankIDMap ")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1601 職等對照設定-新增"
    Public Function AddRankMappingSetting(ByVal beRankMapping As beRankMapping.Row) As Boolean
        Dim bsRankMapping As New beRankMapping.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsRankMapping.Insert(beRankMapping, tran) = 0 Then Return False
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

#Region "PA1602 職等對照設定-修改"
    Public Function UpdateRankMappingSetting(ByVal beRankMapping As beRankMapping.Row, ByVal saveCompID As String, ByVal saveRankID As String, ByVal saveRankIDMap As String) As Boolean
        Dim bsRankMapping As New beRankMapping.Service()
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" UPDATE RankMapping SET RankID='" + beRankMapping.RankID.Value.ToString + "',RankIDMap='" + beRankMapping.RankIDMap.Value.ToString + "' ")
        strSQL.AppendLine(" ,LastChgComp='" + beRankMapping.LastChgComp.Value.ToString + "',LastChgID='" + beRankMapping.LastChgID.Value.ToString + "',LastChgDate= GETDATE() ")
        strSQL.AppendLine(" WHERE CompID='" + saveCompID + "' AND RankID='" + saveRankID + "' AND RankIDMap='" + saveRankIDMap + "' ")
        Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB")

        Return True
    End Function
#End Region

#Region "PA1600 公司職等下拉選單from Rank"
    '2015/07/21新增
    Public Shared Sub FillRankIDFromRank_PA1600(ByVal objDDL As DropDownList, ByVal CompID As String, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objPA As New PA1

        Try
            Using dt As DataTable = objPA.GetRankIDFromRank_PA1600(CompID)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "RankID"
                    .DataValueField = "RankID"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetRankIDFromRank_PA1600(ByVal CompID As String) As DataTable
        Dim strSQL As String

        strSQL = " SELECT RankID FROM Rank WHERE CompID='" + CompID + "' "
        strSQL = strSQL & " ORDER BY CompID, RankID "

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1600 公司職等是否已有對照金控職等"
    '2015/07/21新增防呆，【一個RankID】只能對應【一個RankIDMap】
    Public Function SelectRankID(ByVal CompID As String, ByVal RankID As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" SELECT Count(*) AS Count FROM RankMapping WHERE CompID = " & Bsp.Utility.Quote(CompID.ToString()) & " AND RankID = " & Bsp.Utility.Quote(RankID.ToString()))
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#End Region

#Region "PA1700 工作性質設定"
#Region "PA1700 工作性質代碼下拉選單"
    Public Shared Sub FillWorkTypeID_PA1700(ByVal objDDL As DropDownList, ByVal CompID As String, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objPA As New PA1

        Try
            Using dt As DataTable = objPA.GetWorkTypeID_PA1700(CompID)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "FullName"
                    .DataValueField = "WorkTypeID"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetWorkTypeID_PA1700(ByVal CompID As String) As DataTable
        Dim strSQL As String

        strSQL = " SELECT DISTINCT WorkTypeID, WorkTypeID + '-' + Remark As FullName FROM WorkType WHERE CompID='" + CompID + "' "
        '2015/10/12 增加order by 條件
        strSQL += " ORDER BY WorkTypeID "


        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1700,PA1710,PA1720,PA1730 大類下拉選單"
    Public Shared Sub FillCategoryI_PA1700(ByVal objDDL As DropDownList, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objPA As New PA1

        Try
            Using dt As DataTable = objPA.GetCategoryI_PA1700()
                With(objDDL)
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "FullName"
                    .DataValueField = "CategoryI"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetCategoryI_PA1700() As DataTable
        Dim strSQL As String

        strSQL = " SELECT DISTINCT CategoryI, CategoryI + '-' + CategoryIName AS FullName FROM WorkType_CategoryI "
        '2015/10/12 增加order by 條件
        strSQL += " ORDER BY CategoryI "

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1700 中類下拉選單"
    Public Shared Sub FillCategoryII_PA1700(ByVal objDDL As DropDownList, ByVal CategoryI As String, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objPA As New PA1

        Try
            Using dt As DataTable = objPA.GetCategoryII_PA1700(CategoryI)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "FullName"
                    .DataValueField = "CategoryII"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetCategoryII_PA1700(ByVal CategoryI As String) As DataTable
        Dim strSQL As String

        strSQL = " SELECT DISTINCT CategoryII, CategoryII + '-' + CategoryIIName AS FullName FROM WorkType_CategoryII WHERE CategoryI='" + CategoryI + "'"
        '2015/10/12 增加order by 條件
        strSQL += " ORDER BY CategoryII "

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1700 細類下拉選單"
    Public Shared Sub FillCategoryIII_PA1700(ByVal objDDL As DropDownList, ByVal CategoryI As String, ByVal CategoryII As String, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objPA As New PA1

        Try
            Using dt As DataTable = objPA.GetCategoryIII_PA1700(CategoryI, CategoryII)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "FullName"
                    .DataValueField = "CategoryIII"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetCategoryIII_PA1700(ByVal CategoryI As String, ByVal CategoryII As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" SELECT DISTINCT CategoryIII, CategoryIII + '-' + CategoryIIIName AS FullName FROM WorkType_CategoryIII WHERE ")

        If CategoryI.Trim.Length <> 0 Then
            strSQL.AppendLine(" CategoryI='" + CategoryI + "' AND ")
        End If

        strSQL.AppendLine(" CategoryII='" + CategoryII + "' ")
        '2015/10/12 增加order by 條件
        strSQL.AppendLine(" ORDER BY CategoryIII ")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1700 工作性質設定-查詢"
    Public Function WorkTypeSettingQuery(ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT ")
        strSQL.AppendLine(" W.CompID, W.WorkTypeID, W.Remark, W.InValidFlag ")
        strSQL.AppendLine(" , AOFlag = Case W.AOFlag When '' Then '' When '0' Then 'OP' When '1' Then 'AO' End ")
        strSQL.AppendLine(" , W.PBFlag, W.SortOrder, W.OrganPrintFlag ")
        strSQL.AppendLine(" , Class = Case W.Class When '' Then '' When '1' Then '業務' When '2' Then '作業' When '3' Then '規劃管理' End ")
        strSQL.AppendLine(" , WI.CategoryIName AS CategoryI, WII.CategoryIIName AS CategoryII, WIII.CategoryIIIName AS CategoryIII ")
        strSQL.AppendLine(" , Comp.CompName AS LastChgComp, Pers.NameN AS LastChgID ")
        strSQL.AppendLine(" , LastChgDate = Case When Convert(Char(10), W.LastChgDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, W.LastChgDate, 120) End ")
        strSQL.AppendLine(" FROM WorkType W ")
        strSQL.AppendLine(" LEFT JOIN WorkType_CategoryI WI ON W.CategoryI = WI.CategoryI ")
        strSQL.AppendLine(" LEFT JOIN WorkType_CategoryII WII ON W.CategoryI = WII.CategoryI AND W.CategoryII = WII.CategoryII ")
        strSQL.AppendLine(" LEFT JOIN WorkType_CategoryIII WIII ON W.CategoryI = WIII.CategoryI AND W.CategoryII = WIII.CategoryII AND W.CategoryIII = WIII.CategoryIII ")
        strSQL.AppendLine(" LEFT JOIN Company Comp ON W.LastChgComp = Comp.CompID ")
        strSQL.AppendLine(" LEFT JOIN Personal Pers ON W.LastChgComp = Pers.CompID AND W.LastChgID = Pers.EmpID ")
        strSQL.AppendLine(" WHERE 1=1 ")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine(" AND W.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "WorkTypeID"
                        strSQL.AppendLine(" AND W.WorkTypeID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "Remark"
                        strSQL.AppendLine(" AND W.Remark LIKE N'%" & ht(strKey).ToString() + "%' ")
                    Case "InValidFlag"
                        strSQL.AppendLine(" AND W.InValidFlag = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "PBFlag"
                        strSQL.AppendLine(" AND W.PBFlag = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "AOFlag"
                        strSQL.AppendLine(" AND W.AOFlag = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "SortOrder"
                        strSQL.AppendLine(" AND W.SortOrder LIKE '%" & ht(strKey).ToString() + "%' ")
                    Case "OrganPrintFlag"
                        strSQL.AppendLine(" AND W.OrganPrintFlag = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "Class"
                        strSQL.AppendLine(" AND W.Class = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "CategoryI"
                        strSQL.AppendLine(" AND WI.CategoryI = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "CategoryII"
                        strSQL.AppendLine(" AND WII.CategoryII = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "CategoryIII"
                        strSQL.AppendLine(" AND WIII.CategoryIII = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next

        'Dim QueryTable As DataTable
        'QueryTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)

        'If QueryTable.Rows.Count > 0 Then
        '    For Each row As DataRow In QueryTable.Rows
        '        '工作性質類別
        '        Dim ColumnName_Class As String = QueryTable.Columns(8).ColumnName
        '        Dim Row_Class As String = row.Item(ColumnName_Class).ToString()
        '        Select Case Row_Class
        '            Case "1"
        '                row.Item(ColumnName_Class) = "業務"
        '            Case "2"
        '                row.Item(ColumnName_Class) = "作業"
        '            Case "3"
        '                row.Item(ColumnName_Class) = "規劃管理"
        '        End Select

        '    Next
        'End If

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1701 工作性質設定-新增"
    Public Function AddWorkTypeSetting(ByVal beWorkType As beWorkType.Row) As Boolean
        Dim bsWorkType As New beWorkType.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsWorkType.Insert(beWorkType, tran) = 0 Then Return False
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

#Region "PA1702 工作性質設定-修改"
    Public Function UpdateWorkTypeSetting(ByVal beWorkType As beWorkType.Row) As Boolean
        Dim bsWorkType As New beWorkType.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsWorkType.Update(beWorkType, tran) = 0 Then Return False
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

#Region "PA1700 工作性質設定-刪除"
    Public Function DeleteWorkTypeSetting(ByVal beWorkType As beWorkType.Row) As Boolean
        Dim bsWorkType As New beWorkType.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As Data.Common.DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                bsWorkType.DeleteRowByPrimaryKey(beWorkType, tran)

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

#Region "PA1700 無效註記下拉選單"
    Public Shared Sub FillInValidFlag_PA1700(ByVal objDDL As DropDownList, ByVal CompID As String, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objPA As New PA1
        Try
            Using dt As DataTable = objPA.GetInValidFlag_PA1700(CompID)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "FullName"
                    .DataValueField = "InValidFlag"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetInValidFlag_PA1700(ByVal CompID As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" SELECT DISTINCT InValidFlag ")
        strSQL.AppendLine(" , CASE InValidFlag WHEN '' THEN '' WHEN '0' THEN '0-有效' WHEN '1' THEN '1-無效' END AS FullName ")
        '2015/05/21 規格變更:不綁定公司
        'strSQL.AppendLine(" FROM WorkType WHERE CompID='" + CompID + "' AND InValidFlag <> '' ")
        strSQL.AppendLine(" FROM WorkType WHERE InValidFlag <> '' ")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1700 PB類下拉選單"
    Public Shared Sub FillPBFlag_PA1700(ByVal objDDL As DropDownList, ByVal CompID As String, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objPA As New PA1
        Try
            Using dt As DataTable = objPA.GetPBFlag_PA1700(CompID)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "FullName"
                    .DataValueField = "PBFlag"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetPBFlag_PA1700(ByVal CompID As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" SELECT DISTINCT PBFlag ")
        strSQL.AppendLine(" , CASE PBFlag WHEN '' THEN '' WHEN '0' THEN '0-否' WHEN '1' THEN '1-是' END AS FullName ")
        '2015/05/21 規格變更:不綁定公司
        'strSQL.AppendLine(" FROM WorkType WHERE CompID='" + CompID + "' AND PBFlag <> '' ")
        strSQL.AppendLine(" FROM WorkType WHERE PBFlag <> '' ")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1700 AO/OP下拉選單"
    Public Shared Sub FillAOFlag_PA1700(ByVal objDDL As DropDownList, ByVal CompID As String, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objPA As New PA1
        Try
            Using dt As DataTable = objPA.GetAOFlag_PA1700(CompID)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "FullName"
                    .DataValueField = "AOFlag"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetAOFlag_PA1700(ByVal CompID As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" SELECT DISTINCT AOFlag ")
        '2015/05/21 規格變更:改下拉選單顯示的值
        'strSQL.AppendLine(" , CASE AOFlag WHEN '' THEN '' WHEN '0' THEN '0-OP' WHEN '1' THEN '1-AO' END AS FullName ")
        strSQL.AppendLine(" , CASE AOFlag WHEN '' THEN '' WHEN '0' THEN 'OP-作業' WHEN '1' THEN 'AO-業務' END AS FullName ")
        '2015/05/21 規格變更:不綁定公司
        'strSQL.AppendLine(" FROM WorkType WHERE CompID='" + CompID + "' AND AOFlag <> '' ")
        strSQL.AppendLine(" FROM WorkType WHERE AOFlag <> '' ")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1700 部門列印註記下拉選單"
    Public Shared Sub FillOrganPrintFlag_PA1700(ByVal objDDL As DropDownList, ByVal CompID As String, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objPA As New PA1
        Try
            Using dt As DataTable = objPA.GetOrganPrintFlag_PA1700(CompID)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "FullName"
                    .DataValueField = "OrganPrintFlag"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetOrganPrintFlag_PA1700(ByVal CompID As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" SELECT DISTINCT OrganPrintFlag ")
        strSQL.AppendLine(" , CASE OrganPrintFlag WHEN '' THEN '' WHEN '0' THEN '0-否' WHEN '1' THEN '1-是' END AS FullName ")
        '2015/05/21 規格變更:不綁定公司
        'strSQL.AppendLine(" FROM WorkType WHERE CompID='" + CompID + "' AND OrganPrintFlag <> '' ")
        strSQL.AppendLine(" FROM WorkType WHERE OrganPrintFlag <> '' ")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1700 工作性質類別下拉選單"
    Public Shared Sub FillClass_PA1700(ByVal objDDL As DropDownList, ByVal CompID As String, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objPA As New PA1
        Try
            Using dt As DataTable = objPA.GetClass_PA1700(CompID)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "FullName"
                    .DataValueField = "Code"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetClass_PA1700(ByVal CompID As String) As DataTable
        Dim strSQL As New StringBuilder()
        '2015/05/21 規格變更
        'strSQL.AppendLine(" SELECT DISTINCT Class ")
        'strSQL.AppendLine(" , CASE Class WHEN '' THEN '' WHEN '1' THEN '1-業務' WHEN '2' THEN '2-作業' WHEN '3' THEN '3-規劃管理' END AS FullName ")
        'strSQL.AppendLine(" FROM WorkType WHERE CompID='" + CompID + "' AND Class <> '' ")
        strSQL.AppendLine(" Select Code,CodeCName,Code + '-' + CodeCName As FullName from HRCodeMap where TabName = 'WorkType' and FldName = 'Class' ")
        '2015/10/12 增加order by 條件
        strSQL.AppendLine(" ORDER BY Code ")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#End Region

#Region "PA1710 工作性質設定_大類"
#Region "PA1710 工作性質設定_大類-查詢"
    Public Function WorkType_CategoryIQuery(ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT ")
        strSQL.AppendLine(" WI.CategoryI, WI.CategoryIName ")
        strSQL.AppendLine(" , Comp.CompName AS LastChgComp, Pers.NameN AS LastChgID ")
        strSQL.AppendLine(" , LastChgDate = Case When Convert(Char(10), WI.LastChgDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, WI.LastChgDate, 120) End ")
        strSQL.AppendLine(" FROM WorkType_CategoryI WI ")
        strSQL.AppendLine(" LEFT JOIN Company Comp ON WI.LastChgComp = Comp.CompID ")
        strSQL.AppendLine(" LEFT JOIN Personal Pers ON WI.LastChgComp = Pers.CompID AND WI.LastChgID = Pers.EmpID ")
        strSQL.AppendLine(" WHERE 1=1 ")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CategoryI"
                        strSQL.AppendLine(" AND WI.CategoryI = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "CategoryIName"
                        strSQL.AppendLine(" AND WI.CategoryIName LIKE N'%" & ht(strKey).ToString() + "%' ")
                End Select
            End If
        Next

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1711 工作性質設定_大類-新增"
    Public Function AddWorkType_CategoryI(ByVal beWorkType_CategoryI As beWorkType_CategoryI.Row) As Boolean
        Dim bsWorkType_CategoryI As New beWorkType_CategoryI.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsWorkType_CategoryI.Insert(beWorkType_CategoryI, tran) = 0 Then Return False
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

#Region "PA1712 工作性質設定_大類-修改"
    Public Function UpdateWorkType_CategoryI(ByVal beWorkType_CategoryI As beWorkType_CategoryI.Row) As Boolean
        Dim bsWorkType_CategoryI As New beWorkType_CategoryI.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsWorkType_CategoryI.Update(beWorkType_CategoryI, tran) = 0 Then Return False
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

#Region "PA1710 工作性質設定_大類-刪除"
    Public Function DeleteWorkType_CategoryI(ByVal beWorkType_CategoryI As beWorkType_CategoryI.Row) As Boolean
        Dim bsWorkType_CategoryI As New beWorkType_CategoryI.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As Data.Common.DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                bsWorkType_CategoryI.DeleteRowByPrimaryKey(beWorkType_CategoryI, tran)

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

#Region "PA1710 工作性質設定_大類-查詢資料是否存在WorkType_CategoryII"
    Public Function IsDataExists_WorkType_CategoryII(ByVal CategoryI As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" SELECT Count(*) FROM WorkType_CategoryII WHERE CategoryI='" + CategoryI + "' ")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region
#End Region

#Region "PA1720 工作性質設定_中類"
#Region "PA1720 中類下拉選單"
    Public Shared Sub FillCategoryII_PA1720(ByVal objDDL As DropDownList, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objPA As New PA1

        Try
            Using dt As DataTable = objPA.GetCategoryII_PA1720()
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "FullName"
                    .DataValueField = "CategoryII"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetCategoryII_PA1720() As DataTable
        Dim strSQL As String

        strSQL = " SELECT DISTINCT CategoryII, CategoryII + '-' + CategoryIIName AS FullName FROM WorkType_CategoryII "
        '2015/10/12 增加order by 條件
        strSQL += " ORDER BY CategoryII "

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1720 工作性質設定_中類-查詢"
    Public Function WorkType_CategoryIIQuery(ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT ")
        strSQL.AppendLine(" WII.CategoryI, WI.CategoryIName, WII.CategoryII, WII.CategoryIIName ")
        strSQL.AppendLine(" , Comp.CompName AS LastChgComp, Pers.NameN AS LastChgID, WII.LastChgDate ")
        strSQL.AppendLine(" , LastChgDate = Case When Convert(Char(10), WII.LastChgDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, WII.LastChgDate, 120) End ")
        strSQL.AppendLine(" FROM WorkType_CategoryII WII ")
        strSQL.AppendLine(" LEFT JOIN WorkType_CategoryI WI ON WII.CategoryI = WI.CategoryI ")
        strSQL.AppendLine(" LEFT JOIN Company Comp ON WII.LastChgComp = Comp.CompID ")
        strSQL.AppendLine(" LEFT JOIN Personal Pers ON WII.LastChgComp = Pers.CompID AND WII.LastChgID = Pers.EmpID ")
		strSQL.AppendLine(" WHERE 1=1 ")
		
        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CategoryII"
                        strSQL.AppendLine(" AND WII.CategoryII = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "CategoryIIName"
                        strSQL.AppendLine(" AND WII.CategoryIIName LIKE '%" & ht(strKey).ToString() + "%' ")
                End Select
            End If
        Next

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1721 工作性質設定_中類-新增"
    Public Function AddWorkType_CategoryII(ByVal beWorkType_CategoryII As beWorkType_CategoryII.Row) As Boolean
        Dim bsWorkType_CategoryII As New beWorkType_CategoryII.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsWorkType_CategoryII.Insert(beWorkType_CategoryII, tran) = 0 Then Return False
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

#Region "PA1722 工作性質設定_中類-修改"
    Public Function UpdateWorkType_CategoryII(ByVal beWorkType_CategoryII As beWorkType_CategoryII.Row) As Boolean
        Dim bsWorkType_CategoryII As New beWorkType_CategoryII.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsWorkType_CategoryII.Update(beWorkType_CategoryII, tran) = 0 Then Return False
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

#Region "PA1720 工作性質設定_中類-刪除"
    Public Function DeleteWorkType_CategoryII(ByVal beWorkType_CategoryII As beWorkType_CategoryII.Row) As Boolean
        Dim bsWorkType_CategoryII As New beWorkType_CategoryII.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As Data.Common.DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                bsWorkType_CategoryII.DeleteRowByPrimaryKey(beWorkType_CategoryII, tran)

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

#Region "PA1720 工作性質設定_中類-查詢資料是否存在WorkType_CategoryIII"
    Public Function IsDataExists_WorkType_CategoryIII(ByVal CategoryI As String, ByVal CategoryII As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" SELECT Count(*) FROM WorkType_CategoryIII WHERE CategoryI='" + CategoryI + "' AND CategoryII='" + CategoryII + "' ") '2015/07/27 Modify 增加大類條件
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region
#End Region

#Region "PA1730 工作性質設定_細類"
#Region "PA1730 細類下拉選單"
    Public Shared Sub FillCategoryIII_PA1730(ByVal objDDL As DropDownList, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objPA As New PA1

        Try
            Using dt As DataTable = objPA.GetCategoryIII_PA1730()
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "FullName"
                    .DataValueField = "CategoryIII"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetCategoryIII_PA1730() As DataTable
        Dim strSQL As String

        strSQL = " SELECT DISTINCT CategoryIII, CategoryIII + '-' + CategoryIIIName AS FullName FROM WorkType_CategoryIII "
        '2015/10/12 增加order by 條件
        strSQL += " ORDER BY CategoryIII "

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1730 細類名稱下拉選單"
    Public Shared Sub FillCategoryIIIName_PA1730(ByVal objDDL As DropDownList, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objPA As New PA1

        Try
            Using dt As DataTable = objPA.GetCategoryIIIName_PA1730()
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "CategoryIIIName"
                    .DataValueField = "CategoryIIIName"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetCategoryIIIName_PA1730() As DataTable
        Dim strSQL As String

        strSQL = " SELECT DISTINCT CategoryIIIName FROM WorkType_CategoryIII "

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1730 工作性質設定_細類-查詢"
    Public Function WorkType_CategoryIIIQuery(ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT ")
        strSQL.AppendLine(" WIII.CategoryI, WI.CategoryIName, WIII.CategoryII, WII.CategoryIIName, WIII.CategoryIII, WIII.CategoryIIIName ")
        strSQL.AppendLine(" , Comp.CompName AS LastChgComp, Pers.NameN AS LastChgID ")
        strSQL.AppendLine(" , LastChgDate = Case When Convert(Char(10), WIII.LastChgDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, WIII.LastChgDate, 120) End ")
        strSQL.AppendLine(" FROM WorkType_CategoryIII WIII ")
        strSQL.AppendLine(" LEFT JOIN WorkType_CategoryI WI ON WIII.CategoryI = WI.CategoryI ")
        strSQL.AppendLine(" LEFT JOIN WorkType_CategoryII WII ON WIII.CategoryI = WII.CategoryI AND WIII.CategoryII = WII.CategoryII ")
        strSQL.AppendLine(" LEFT JOIN Company Comp ON WIII.LastChgComp = Comp.CompID ")
        strSQL.AppendLine(" LEFT JOIN Personal Pers ON WIII.LastChgComp = Pers.CompID AND WIII.LastChgID = Pers.EmpID ")
        strSQL.AppendLine(" WHERE 1=1 ")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CategoryIII"
                        strSQL.AppendLine(" AND WIII.CategoryIII = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "CategoryIIIName"
                        strSQL.AppendLine(" AND WIII.CategoryIIIName LIKE N'%" & ht(strKey).ToString() + "%' ")
                End Select
            End If
        Next

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1731 工作性質設定_細類-新增"
    Public Function AddWorkType_CategoryIII(ByVal beWorkType_CategoryIII As beWorkType_CategoryIII.Row) As Boolean
        Dim bsWorkType_CategoryIII As New beWorkType_CategoryIII.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsWorkType_CategoryIII.Insert(beWorkType_CategoryIII, tran) = 0 Then Return False
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

#Region "PA1732 工作性質設定_細類-修改"
    Public Function UpdateWorkType_CategoryII(ByVal beWorkType_CategoryIII As beWorkType_CategoryIII.Row) As Boolean
        Dim bsWorkType_CategoryIII As New beWorkType_CategoryIII.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsWorkType_CategoryIII.Update(beWorkType_CategoryIII, tran) = 0 Then Return False
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

#Region "PA1730 工作性質設定_細類-刪除"
    Public Function DeleteWorkType_CategoryIII(ByVal beWorkType_CategoryIII As beWorkType_CategoryIII.Row) As Boolean
        Dim bsWorkType_CategoryIII As New beWorkType_CategoryIII.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As Data.Common.DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                bsWorkType_CategoryIII.DeleteRowByPrimaryKey(beWorkType_CategoryIII, tran)

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
#End Region

#Region "PA1800 職位設定"
#Region "PA1800,PA1810,PA1820,PA1830 大類下拉選單"
    Public Shared Sub FillCategoryI_PA1800(ByVal objDDL As DropDownList, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objPA As New PA1

        Try
            Using dt As DataTable = objPA.GetCategoryI_PA1800()
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "FullName"
                    .DataValueField = "CategoryI"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetCategoryI_PA1800() As DataTable
        Dim strSQL As String

        strSQL = " SELECT DISTINCT CategoryI, CategoryI + '-' + CategoryIName AS FullName FROM Position_CategoryI "
        '2015/10/12 增加order by 條件
        strSQL += " ORDER BY CategoryI "

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1800 中類下拉選單"
    Public Shared Sub FillCategoryII_PA1800(ByVal objDDL As DropDownList, ByVal CategoryI As String, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objPA As New PA1

        Try
            Using dt As DataTable = objPA.GetCategoryII_PA1800(CategoryI)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "FullName"
                    .DataValueField = "CategoryII"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetCategoryII_PA1800(ByVal CategoryI As String) As DataTable
        Dim strSQL As String

        strSQL = " SELECT DISTINCT CategoryII, CategoryII + '-' + CategoryIIName AS FullName FROM Position_CategoryII WHERE CategoryI='" + CategoryI + "'"
        '2015/10/12 增加order by 條件
        strSQL += " ORDER BY CategoryII "

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1800 細類下拉選單"
    Public Shared Sub FillCategoryIII_PA1800(ByVal objDDL As DropDownList, ByVal CategoryI As String, ByVal CategoryII As String, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objPA As New PA1

        Try
            Using dt As DataTable = objPA.GetCategoryIII_PA1800(CategoryI, CategoryII)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "FullName"
                    .DataValueField = "CategoryIII"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetCategoryIII_PA1800(ByVal CategoryI As String, ByVal CategoryII As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" SELECT DISTINCT CategoryIII, CategoryIII + '-' + CategoryIIIName AS FullName FROM Position_CategoryIII WHERE ")

        If CategoryI.Trim.Length <> 0 Then
            strSQL.AppendLine(" CategoryI='" + CategoryI + "' AND ")
        End If

        strSQL.AppendLine(" CategoryII='" + CategoryII + "' ")
        '2015/10/12 增加order by 條件
        strSQL.AppendLine(" ORDER BY CategoryIII ")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1800 職位代碼下拉選單"
    Public Shared Sub FillPositionID_PA1800(ByVal objDDL As DropDownList, ByVal CompID As String, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objPA As New PA1

        Try
            Using dt As DataTable = objPA.GetPositionID_PA1800(CompID)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "FullName"
                    .DataValueField = "PositionID"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetPositionID_PA1800(ByVal CompID As String) As DataTable
        Dim strSQL As String

        strSQL = " SELECT DISTINCT PositionID, PositionID + '-' + Remark AS FullName FROM Position WHERE CompID = '" + CompID + "' "
        '2015/10/12 增加order by 條件
        strSQL += " ORDER BY PositionID "

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1800 職位設定-查詢"
    Public Function PositionSettingQuery(ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT ")
        strSQL.AppendLine(" P.CompID, P.PositionID, P.Remark, P.InValidFlag, P.SortOrder, P.OrganPrintFlag, P.IsEVManager ")
        strSQL.AppendLine(" , PI.CategoryIName AS CategoryI, PII.CategoryIIName AS CategoryII, PIII.CategoryIIIName AS CategoryIII ")
        strSQL.AppendLine(" , Comp.CompName AS LastChgComp, Pers.NameN AS LastChgID ")
        strSQL.AppendLine(" , LastChgDate = Case When Convert(Char(10), P.LastChgDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, P.LastChgDate, 120) End ")
        strSQL.AppendLine(" FROM Position P ")
        strSQL.AppendLine(" LEFT JOIN Position_CategoryI PI ON P.CategoryI = PI.CategoryI ")
        strSQL.AppendLine(" LEFT JOIN Position_CategoryII PII ON P.CategoryI = PII.CategoryI AND P.CategoryII = PII.CategoryII ")
        strSQL.AppendLine(" LEFT JOIN Position_CategoryIII PIII ON P.CategoryI = PIII.CategoryI AND P.CategoryII = PIII.CategoryII AND P.CategoryIII = PIII.CategoryIII ")
        strSQL.AppendLine(" LEFT JOIN Company Comp ON P.LastChgComp = Comp.CompID ")
        strSQL.AppendLine(" LEFT JOIN Personal Pers ON P.LastChgComp = Pers.CompID AND P.LastChgID = Pers.EmpID ")
        strSQL.AppendLine(" WHERE 1=1 ")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID" 
                        strSQL.AppendLine(" AND P.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "PositionID" 
                        strSQL.AppendLine(" AND P.PositionID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "Remark" 
                        strSQL.AppendLine(" AND P.Remark LIKE N'%" & ht(strKey).ToString() + "%' ")
                    Case "InValidFlag" 
                        strSQL.AppendLine(" AND P.InValidFlag = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "SortOrder" 
                        strSQL.AppendLine(" AND P.SortOrder LIKE '%" & ht(strKey).ToString() + "%' ")
                    Case "OrganPrintFlag" 
                        strSQL.AppendLine(" AND P.OrganPrintFlag = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "IsEVManager" 
                        strSQL.AppendLine(" AND P.IsEVManager = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "CategoryI" 
                        strSQL.AppendLine(" AND PI.CategoryI = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "CategoryII" 
                        strSQL.AppendLine(" AND PII.CategoryII = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "CategoryIII" 
                        strSQL.AppendLine(" AND PIII.CategoryIII = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1801 職位設定-新增"
    Public Function AddPositionSetting(ByVal bePosition As bePosition.Row) As Boolean
        Dim bsPosition As New bePosition.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsPosition.Insert(bePosition, tran) = 0 Then Return False
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

#Region "PA1802 職位設定-修改"
    Public Function UpdatePositionSetting(ByVal bePosition As bePosition.Row) As Boolean
        Dim bsPosition As New bePosition.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsPosition.Update(bePosition, tran) = 0 Then Return False
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

#Region "PA1800 職位設定-刪除"
    Public Function DeletePositionSetting(ByVal bePosition As bePosition.Row) As Boolean
        Dim bsPosition As New bePosition.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As Data.Common.DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                bsPosition.DeleteRowByPrimaryKey(bePosition, tran)

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

#Region "PA1800 無效註記下拉選單"
    Public Shared Sub FillInValidFlag_PA1800(ByVal objDDL As DropDownList, ByVal CompID As String, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objPA As New PA1
        Try
            Using dt As DataTable = objPA.GetInValidFlag_PA1800(CompID)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "FullName"
                    .DataValueField = "InValidFlag"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetInValidFlag_PA1800(ByVal CompID As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" SELECT DISTINCT InValidFlag ")
        strSQL.AppendLine(" , CASE InValidFlag WHEN '' THEN '' WHEN '0' THEN '0-有效' WHEN '1' THEN '1-無效' END AS FullName ")
        '2015/05/21 規格變更:不綁定公司
        'strSQL.AppendLine(" FROM Position WHERE CompID='" + CompID + "' AND InValidFlag <> '' ")
        strSQL.AppendLine(" FROM Position WHERE InValidFlag <> '' ")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1800 部門列印註記下拉選單"
    Public Shared Sub FillOrganPrintFlag_PA1800(ByVal objDDL As DropDownList, ByVal CompID As String, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objPA As New PA1
        Try
            Using dt As DataTable = objPA.GetOrganPrintFlag_PA1800(CompID)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "FullName"
                    .DataValueField = "OrganPrintFlag"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetOrganPrintFlag_PA1800(ByVal CompID As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" SELECT DISTINCT OrganPrintFlag ")
        strSQL.AppendLine(" , CASE OrganPrintFlag WHEN '' THEN '' WHEN '0' THEN '0-不可列印' WHEN '1' THEN '1-可列印' END AS FullName ")
        '2015/05/22 規格變更:不綁定公司
        'strSQL.AppendLine(" FROM Position WHERE CompID='" + CompID + "' AND OrganPrintFlag <> '' ")
        strSQL.AppendLine(" FROM Position WHERE OrganPrintFlag <> '' ")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1800 績效考核表主管註記下拉選單"
    Public Shared Sub FillIsEVManager_PA1800(ByVal objDDL As DropDownList, ByVal CompID As String, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objPA As New PA1
        Try
            Using dt As DataTable = objPA.GetIsEVManager_PA1800(CompID)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "FullName"
                    .DataValueField = "IsEVManager"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetIsEVManager_PA1800(ByVal CompID As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" SELECT DISTINCT IsEVManager ")
        strSQL.AppendLine(" , CASE IsEVManager WHEN '' THEN '' WHEN '0' THEN '0-非主管' WHEN '1' THEN '1-主管' END AS FullName ")
        '2015/05/21 規格變更:不綁定公司
        'strSQL.AppendLine(" FROM Position WHERE CompID='" + CompID + "' AND IsEVManager <> '' ")
        strSQL.AppendLine(" FROM Position WHERE IsEVManager <> '' ")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#End Region

#Region "PA1810 職位設定_大類"
#Region "PA1810 職位設定_大類-查詢"
    Public Function Position_CategoryIQuery(ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT ")
        strSQL.AppendLine(" PI.CategoryI, PI.CategoryIName ")
        strSQL.AppendLine(" , Comp.CompName AS LastChgComp, Pers.NameN AS LastChgID ")
        strSQL.AppendLine(" , LastChgDate = Case When Convert(Char(10), PI.LastChgDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, PI.LastChgDate, 120) End ")
        strSQL.AppendLine(" FROM Position_CategoryI PI")
        strSQL.AppendLine(" LEFT JOIN Company Comp ON PI.LastChgComp = Comp.CompID ")
        strSQL.AppendLine(" LEFT JOIN Personal Pers ON PI.LastChgComp = Pers.CompID AND PI.LastChgID = Pers.EmpID ")
        strSQL.AppendLine(" WHERE 1=1 ")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CategoryI" 
                        strSQL.AppendLine(" AND PI.CategoryI = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "CategoryIName" 
                        strSQL.AppendLine(" AND PI.CategoryIName LIKE N'%" & ht(strKey).ToString() + "%' ")
                End Select
            End If
        Next

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1811 職位設定_大類-新增"
    Public Function AddPosition_CategoryI(ByVal bePosition_CategoryI As bePosition_CategoryI.Row) As Boolean
        Dim bsPosition_CategoryI As New bePosition_CategoryI.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsPosition_CategoryI.Insert(bePosition_CategoryI, tran) = 0 Then Return False
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

#Region "PA1812 職位設定_大類-修改"
    Public Function UpdatePosition_CategoryI(ByVal bePosition_CategoryI As bePosition_CategoryI.Row) As Boolean
        Dim bsPosition_CategoryI As New bePosition_CategoryI.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsPosition_CategoryI.Update(bePosition_CategoryI, tran) = 0 Then Return False
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

#Region "PA1810 職位設定_大類-刪除"
    Public Function DeletePosition_CategoryI(ByVal bePosition_CategoryI As bePosition_CategoryI.Row) As Boolean
        Dim bsPosition_CategoryI As New bePosition_CategoryI.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As Data.Common.DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                bsPosition_CategoryI.DeleteRowByPrimaryKey(bePosition_CategoryI, tran)

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

#Region "PA1810 職位設定_大類-查詢資料是否存在Position_CategoryII"
    Public Function IsDataExists_Position_CategoryII(ByVal CategoryI As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" SELECT Count(*) FROM Position_CategoryII WHERE CategoryI='" + CategoryI + "' ")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region
#End Region

#Region "PA1820 職位設定_中類"
#Region "PA1820 中類下拉選單"
    Public Shared Sub FillCategoryII_PA1820(ByVal objDDL As DropDownList, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objPA As New PA1

        Try
            Using dt As DataTable = objPA.GetCategoryII_PA1820()
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "FullName"
                    .DataValueField = "CategoryII"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetCategoryII_PA1820() As DataTable
        Dim strSQL As String

        strSQL = " SELECT DISTINCT CategoryII, CategoryII + '-' + CategoryIIName AS FullName FROM Position_CategoryII "
        '2015/10/12 增加order by 條件
        strSQL += " ORDER BY CategoryII "

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1820 職位設定_中類-查詢"
    Public Function Position_CategoryIIQuery(ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT ")
        strSQL.AppendLine(" PII.CategoryI, PI.CategoryIName, PII.CategoryII, PII.CategoryIIName ")
        strSQL.AppendLine(" , Comp.CompName AS LastChgComp, Pers.NameN AS LastChgID ")
        strSQL.AppendLine(" , LastChgDate = Case When Convert(Char(10), PII.LastChgDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, PII.LastChgDate, 120) End ")
        strSQL.AppendLine(" FROM Position_CategoryII PII ")
        strSQL.AppendLine(" LEFT JOIN Position_CategoryI PI ON PII.CategoryI = PI.CategoryI ")
        strSQL.AppendLine(" LEFT JOIN Company Comp ON PII.LastChgComp = Comp.CompID ")
        strSQL.AppendLine(" LEFT JOIN Personal Pers ON PII.LastChgComp = Pers.CompID AND PII.LastChgID = Pers.EmpID ")
        strSQL.AppendLine(" WHERE 1=1 ")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CategoryII" 
                        strSQL.AppendLine(" AND PII.CategoryII = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "CategoryIIName" 
                        strSQL.AppendLine(" AND PII.CategoryIIName LIKE N'%" & ht(strKey).ToString() + "%' ")
                End Select
            End If
        Next

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1821 職位設定_中類-新增"
    Public Function AddPosition_CategoryII(ByVal bePosition_CategoryII As bePosition_CategoryII.Row) As Boolean
        Dim bsPosition_CategoryII As New bePosition_CategoryII.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsPosition_CategoryII.Insert(bePosition_CategoryII, tran) = 0 Then Return False
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

#Region "PA1822 職位設定_中類-修改"
    Public Function UpdatePosition_CategoryII(ByVal bePosition_CategoryII As bePosition_CategoryII.Row) As Boolean
        Dim bsPosition_CategoryII As New bePosition_CategoryII.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsPosition_CategoryII.Update(bePosition_CategoryII, tran) = 0 Then Return False
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

#Region "PA1820 職位設定_中類-刪除"
    Public Function DeletePosition_CategoryII(ByVal bePosition_CategoryII As bePosition_CategoryII.Row) As Boolean
        Dim bsPosition_CategoryII As New bePosition_CategoryII.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As Data.Common.DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                bsPosition_CategoryII.DeleteRowByPrimaryKey(bePosition_CategoryII, tran)

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

#Region "PA1820 職位設定_中類-查詢資料是否存在Position_CategoryIII"
    Public Function IsDataExists_Position_CategoryIII(ByVal CategoryI As String, ByVal CategoryII As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" SELECT Count(*) FROM Position_CategoryIII WHERE CategoryI='" + CategoryI + "' AND CategoryII='" + CategoryII + "' ") '2015/07/27 Modify 增加大類條件
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region
#End Region

#Region "PA1830 職位設定_細類"
#Region "PA1830 細類下拉選單"
    Public Shared Sub FillCategoryIII_PA1830(ByVal objDDL As DropDownList, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objPA As New PA1

        Try
            Using dt As DataTable = objPA.GetCategoryIII_PA1830()
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "FullName"
                    .DataValueField = "CategoryIII"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetCategoryIII_PA1830() As DataTable
        Dim strSQL As String

        strSQL = " SELECT DISTINCT CategoryIII, CategoryIII + '-' + CategoryIIIName AS FullName FROM Position_CategoryIII "
        '2015/10/12 增加order by 條件
        strSQL += " ORDER BY CategoryIII "

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1830 職位設定_細類-查詢"
    Public Function Position_CategoryIIIQuery(ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT ")
        strSQL.AppendLine(" PIII.CategoryI, PI.CategoryIName, PIII.CategoryII, PII.CategoryIIName, PIII.CategoryIII, PIII.CategoryIIIName ")
        strSQL.AppendLine(" , Comp.CompName AS LastChgComp, Pers.NameN AS LastChgID ")
        strSQL.AppendLine(" , LastChgDate = Case When Convert(Char(10), PIII.LastChgDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, PIII.LastChgDate, 120) End ")
        strSQL.AppendLine(" FROM Position_CategoryIII PIII ")
        strSQL.AppendLine(" LEFT JOIN Position_CategoryI PI ON PIII.CategoryI = PI.CategoryI ")
        strSQL.AppendLine(" LEFT JOIN Position_CategoryII PII ON PIII.CategoryI = PII.CategoryI AND PIII.CategoryII = PII.CategoryII ")
        strSQL.AppendLine(" LEFT JOIN Company Comp ON PIII.LastChgComp = Comp.CompID ")
        strSQL.AppendLine(" LEFT JOIN Personal Pers ON PIII.LastChgComp = Pers.CompID AND PIII.LastChgID = Pers.EmpID ")
        strSQL.AppendLine(" WHERE 1=1 ")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CategoryIII" 
                        strSQL.AppendLine(" AND PIII.CategoryIII = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "CategoryIIIName" 
                        strSQL.AppendLine(" AND PIII.CategoryIIIName LIKE N'%" & ht(strKey).ToString() + "%' ")
                End Select
            End If
        Next

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1831 職位設定_細類-新增"
    Public Function AddPosition_CategoryIII(ByVal bePosition_CategoryIII As bePosition_CategoryIII.Row) As Boolean
        Dim bsPosition_CategoryIII As New bePosition_CategoryIII.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsPosition_CategoryIII.Insert(bePosition_CategoryIII, tran) = 0 Then Return False
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

#Region "PA1832 職位設定_細類-修改"
    Public Function UpdatePosition_CategoryII(ByVal bePosition_CategoryIII As bePosition_CategoryIII.Row) As Boolean
        Dim bsPosition_CategoryIII As New bePosition_CategoryIII.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsPosition_CategoryIII.Update(bePosition_CategoryIII, tran) = 0 Then Return False
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

#Region "PA1830 職位設定_細類-刪除"
    Public Function DeletePosition_CategoryIII(ByVal bePosition_CategoryIII As bePosition_CategoryIII.Row) As Boolean
        Dim bsPosition_CategoryIII As New bePosition_CategoryIII.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As Data.Common.DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                bsPosition_CategoryIII.DeleteRowByPrimaryKey(bePosition_CategoryIII, tran)

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
#End Region

#Region "PA1900 工作地點設定"
#Region "PA1900 工作地點代碼下拉選單"
    Public Shared Sub FillWorkSiteID_PA1900(ByVal objDDL As DropDownList, ByVal CompID As String, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objPA As New PA1

        Try
            Using dt As DataTable = objPA.GetWorkSiteID_PA1900(CompID)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "WorkSiteName"
                    .DataValueField = "WorkSiteID"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetWorkSiteID_PA1900(ByVal CompID As String) As DataTable
        Dim strSQL As String

        '2015/07/21改成代碼+名稱
        'strSQL = " SELECT DISTINCT WorkSiteID FROM WorkSite WHERE CompID='" + CompID + "' "
        strSQL = " SELECT DISTINCT WorkSiteID, WorkSiteID + '-' + Remark AS WorkSiteName FROM WorkSite WHERE CompID='" + CompID + "' "
        '2015/10/13 增加Order by 條件
        strSQL += " ORDER BY WorkSiteID"

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1901 國別下拉選單"
    Public Shared Sub FillCodeNo_PA1901(ByVal objDDL As DropDownList, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objPA As New PA1

        Try
            Using dt As DataTable = objPA.GetCodeNo_PA1901()
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "CountryName"
                    .DataValueField = "CodeNo"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetCodeNo_PA1901() As DataTable
        Dim strSQL As String

        strSQL = " SELECT CodeNo, CountryName FROM CountryCode ORDER BY CodeNo "

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1901 縣市代碼下拉選單"
    Public Shared Sub FillCityCode_PA1901(ByVal objDDL As DropDownList, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objPA As New PA1

        Try
            Using dt As DataTable = objPA.GetCityCode_PA1901()
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "CodeCName"
                    .DataValueField = "Code"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetCityCode_PA1901() As DataTable
        Dim strSQL As String

        strSQL = " SELECT Code, CodeCName FROM HRCodeMap WHERE TabName = 'WorkSite' AND FldName ='CityCode' AND NotShowFlag = '0' ORDER BY Code "

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1901 撥入類別下拉選單"	'20151218 wei add
    Public Shared Sub FillDialIn_PA1901(ByVal objDDL As DropDownList, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objPA As New PA1

        Try
            Using dt As DataTable = objPA.GetDialIn_PA1901()
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "CodeCName"
                    .DataValueField = "Code"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetDialIn_PA1901() As DataTable
        Dim strSQL As String

        strSQL = " SELECT Code, CodeCName FROM HRCodeMap Where TabName ='WorkSite' and FldName ='DialIn' ORDER BY Code "

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region
#Region "PA1901 撥出類別下拉選單"	'20151218 wei add
    Public Shared Sub FillDialOut_PA1901(ByVal objDDL As DropDownList, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objPA As New PA1

        Try
            Using dt As DataTable = objPA.GetDialOut_PA1901()
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "CodeCName"
                    .DataValueField = "Code"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetDialOut_PA1901() As DataTable
        Dim strSQL As String

        strSQL = " SELECT Code, CodeCName FROM HRCodeMap Where TabName ='WorkSite' and FldName ='DialOut' ORDER BY Code "

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region
#Region "PA1900 國別"
    Public Function SelectCountryCode(ByVal CodeNo As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" SELECT Count(*) AS Count FROM CountryCode WHERE CodeNo = " & Bsp.Utility.Quote(CodeNo.ToString()))
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1900 工作地點設定-查詢"
    Public Function WorkSiteSettingQuery(ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT ")
        strSQL.AppendLine(" W.WorkSiteID, W.Remark, W.EmpCount, W.BranchFlag, W.BuildingFlag, W.CityCode, W.Address ")
        strSQL.AppendLine(" ,CASE WHEN W.Telephone <> '' AND W.ExtNo <> '' THEN W.Telephone + '-' + W.ExtNo ")
        strSQL.AppendLine(" WHEN W.Telephone <> '' AND W.ExtNo = '' THEN W.Telephone ")
        strSQL.AppendLine(" WHEN W.Telephone = '' AND W.ExtNo <> '' THEN W.ExtNo ")
        strSQL.AppendLine(" WHEN W.Telephone = '' AND W.ExtNo = '' THEN '' END AS Telephone ")
        strSQL.AppendLine("  ,W.InvoiceNo ")
        strSQL.AppendLine(" ,Comp.CompName AS LastChgComp, Pers.NameN AS LastChgID ")
        strSQL.AppendLine(" , LastChgDate = Case When Convert(Char(10), W.LastChgDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, W.LastChgDate, 120) End ")
        strSQL.AppendLine(" ,W.DialIn,W.DialOut")   '20151218 wei add   撥入撥出類別
        strSQL.AppendLine(" ,W.ExtYards")   '20160419 wei add 分機長度
        strSQL.AppendLine(" FROM WorkSite W ")
        strSQL.AppendLine(" LEFT JOIN Company Comp ON W.LastChgComp = Comp.CompID ")
        strSQL.AppendLine(" LEFT JOIN Personal Pers ON W.LastChgComp = Pers.CompID AND W.LastChgID = Pers.EmpID ")
        'strSQL.AppendLine(" LEFT JOIN HRCodeMap HRC ON W.CityCode = HRC.Code ")
        strSQL.AppendLine(" WHERE 1=1 ")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID" 
                        strSQL.AppendLine(" AND W.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "WorkSiteID" 
                        strSQL.AppendLine(" AND W.WorkSiteID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "Remark" 
                        strSQL.AppendLine(" AND W.Remark LIKE N'%" & ht(strKey).ToString() + "%' ")
                End Select
            End If
        Next

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1901 工作地點設定-新增"
    Public Function AddWorkSiteSetting(ByVal beWorkSite As beWorkSite.Row) As Boolean
        Dim bsWorkSite As New beWorkSite.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsWorkSite.Insert(beWorkSite, tran) = 0 Then Return False
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

#Region "PA1902 工作地點設定-修改"
    Public Function UpdateWorkSiteSetting(ByVal beWorkSite As beWorkSite.Row) As Boolean
        Dim bsWorkSite As New beWorkSite.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsWorkSite.Update(beWorkSite, tran) = 0 Then Return False
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

#Region "PA1903 工作地點設定-刪除"
    Public Function DeleteWorkSiteSetting(ByVal beWorkSite As beWorkSite.Row) As Boolean
        Dim bsWorkSite As New beWorkSite.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As Data.Common.DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                bsWorkSite.DeleteRowByPrimaryKey(beWorkSite, tran)

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

#Region "PA1900 查詢人事主檔是否有使用工作地點資料"
    Public Function checkPersonal_PA1900(ByVal CompID As String, ByVal WorkSiteID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select Count(*) Cnt From Personal")
        strSQL.AppendLine("Where CompID = '" + CompID + "' AND WorkSiteID = '" + WorkSiteID + "' ")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#End Region

#Region "PA1A00 年曆檔維護"
#Region "PA1A00 查詢月份下拉選單"
    Public Shared Sub FillMonth_PA1A00(ByVal objDDL As DropDownList, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objPA As New PA1

        Try
            Using dt As DataTable = objPA.GetMonth_PA1A00()
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "Month"
                    .DataValueField = "Month"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetMonth_PA1A00() As DataTable
        Dim strSQL As String

        strSQL = " SELECT DISTINCT Month(SysDate) AS Month FROM Calendar ORDER BY Month(SysDate) "

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1A00 年曆檔維護-查詢"
    Public Function CalendarSettingQuery(ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT ")
        strSQL.AppendLine(" Convert(Char(10), C.SysDate, 111) As SysDate, C.HolidayOrNot, C.Week, Convert(Char(10), C.NextBusDate, 111) As NextBusDate, Convert(Char(10), C.LastBusDate, 111) As LastBusDate ")
        strSQL.AppendLine(" ,Comp.CompName AS LastChgComp, Pers.NameN AS LastChgID ")
        strSQL.AppendLine(" , LastChgDate = Case When Convert(Char(10), C.LastChgDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, C.LastChgDate, 120) End ")
        strSQL.AppendLine(" FROM Calendar C ")
        strSQL.AppendLine(" LEFT JOIN Company Comp ON C.LastChgComp = Comp.CompID ")
        strSQL.AppendLine(" LEFT JOIN Personal Pers ON C.LastChgComp = Pers.CompID AND C.LastChgID = Pers.EmpID ")
        strSQL.AppendLine(" WHERE 1=1 ")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine(" AND C.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "Year"
                        strSQL.AppendLine(" AND Year(C.SysDate) = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "Month"
                        strSQL.AppendLine(" AND Month(C.SysDate) = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next

        strSQL.AppendLine(" ORDER BY SysDate ")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1A01 年曆檔維護-新增"
    Public Function AddCalendarSetting(ByVal beCalendar As beCalendar.Row) As Boolean
        Dim bsCalendar As New beCalendar.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsCalendar.Insert(beCalendar, tran) = 0 Then Return False
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

#Region "PA1A02 年曆檔維護-修改"
    Public Function UpdateCalendarSetting(ByVal beCalendar As beCalendar.Row) As Boolean
        Dim bsCalendar As New beCalendar.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsCalendar.Update(beCalendar, tran) = 0 Then Return False
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

#Region "PA1A03 年曆檔維護-刪除"
    Public Function DeleteCalendarSetting(ByVal beCalendar As beCalendar.Row) As Boolean
        Dim bsCalendar As New beCalendar.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As Data.Common.DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                bsCalendar.DeleteRowByPrimaryKey(beCalendar, tran)

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

#Region "PA1A04 年曆檔複製來源公司下拉選單"
    Public Shared Sub FillCompName_PA1A04(ByVal objDDL As DropDownList, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objPA As New PA1

        Try
            Using dt As DataTable = objPA.GetCompName_PA1A04()
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "FullName"
                    .DataValueField = "CompID"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetCompName_PA1A04() As DataTable
        Dim strSQL As String

        strSQL = " SELECT CompID, CompName, CompID + '-' + CompName AS FullName FROM Company WHERE InValidFlag='0' ORDER BY CompID "

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA1A04 複製至授權公司下拉選單"
    Public Shared Sub subLoadCompRoleID(ByVal objDDL As DropDownList, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objPA As New PA1()

        Try
            Using dt As Data.DataTable = objPA.GetCompRoleID(UserProfile.UserID, UserProfile.LoginSysID)
                With objDDL
                    .DataSource = dt
                    .DataTextField = "CompName"
                    .DataValueField = "CompRoleID"
                    .DataBind()

                End With
            End Using

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetCompRoleID(ByVal UserID As String, ByVal SysID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select distinct a.CompRoleID, Case When a.CompRoleID='ALL' Then 'ALL-全金控' Else a.CompRoleID + '-' + b.CompName End as CompName")
        strSQL.AppendLine(",Case when a.CompRoleID='ALL' Then '9' + a.CompRoleID  Else '0' + a.CompRoleID End as SortOrder")
        strSQL.AppendLine("From SC_UserGroup a with (nolock) ")
        strSQL.AppendLine("	Left join SC_Company b with (nolock) on a.CompRoleID = b.CompID")
        strSQL.AppendLine("Where a.UserID = " & Bsp.Utility.Quote(UserID))
        strSQL.AppendLine("And a.SysID = " & Bsp.Utility.Quote(SysID))
        strSQL.AppendLine("Order by SortOrder")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
    End Function
#End Region

#Region "PA1A04 年曆檔維護-年曆檔複製"
    Public Function CopyCalendarSetting(ByVal CopyYear As String, ByVal Source As String, ByVal CopyTo As String, ByVal LastChgComp As String, ByVal LastChgID As String, ByVal LastChgDate As String) As Boolean
        Dim strSQL_OldData As New StringBuilder()
        Dim strSQL_checkData As New StringBuilder()
        Dim strSQL_NewData As New StringBuilder()
        Dim OldData As DataTable
        Dim CheckData As DataTable
        Dim type As String = "WHERE"

        strSQL_OldData.AppendLine(" SELECT SysDate FROM Calendar WHERE CompID='" + Source + "' AND Year(SysDate)='" + CopyYear + "'")
        OldData = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL_OldData.ToString(), "eHRMSDB").Tables(0)

        If OldData.Rows.Count > 0 Then
            For Each row As DataRow In OldData.Rows
                strSQL_checkData.Clear()
                strSQL_checkData.AppendLine(" SELECT CompID FROM Calendar WHERE CompID='" + CopyTo + "' AND SysDate='" + row.Item(0) + "' ")
                CheckData = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL_checkData.ToString(), "eHRMSDB").Tables(0)
                'MsgBox(row.Item(0))

                If CheckData.Rows.Count = 0 Then
                    strSQL_NewData.Clear()
                    strSQL_NewData.AppendLine(" INSERT INTO Calendar ")
                    strSQL_NewData.AppendLine(" (CompID, SysDate, HolidayOrNot, Week, NextBusDate, LastBusDate, NeNeBusDate, LastEndDate, ThisEndDate ")
                    strSQL_NewData.AppendLine(" , MonEndDate, NextDateDiff, NeNeDateDiff, LastDateDiff, JulianDate, LastChgComp, LastChgID, LastChgDate) ")
                    strSQL_NewData.AppendLine(" SELECT ")
                    strSQL_NewData.AppendLine(" '" + CopyTo + "' AS CompID, SysDate, HolidayOrNot, Week, NextBusDate, LastBusDate, NeNeBusDate, LastEndDate, ThisEndDate ")
                    strSQL_NewData.AppendLine(" ,MonEndDate, NextDateDiff, NeNeDateDiff, LastDateDiff, JulianDate, '" + LastChgComp + "', '" + LastChgID + "', GetDate() ")
                    strSQL_NewData.AppendLine(" FROM Calendar ")
                    strSQL_NewData.AppendLine(" WHERE CompID = '" + Source + "' AND SysDate = '" + row.Item(0) + "' ")
                    Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL_NewData.ToString(), "eHRMSDB")
                End If
            Next
        End If

        Return True
    End Function
#End Region

#Region "PA1A00 查詢來源公司是否有年歷檔資料"
    Public Function checkData(ByVal CopyYear As String, ByVal Source As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT SysDate FROM Calendar WHERE CompID='" + Source + "' AND Year(SysDate)='" + CopyYear + "'")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#End Region

End Class
