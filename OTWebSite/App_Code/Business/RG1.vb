'****************************************************
'功能說明：
'建立人員：MickySung
'建立日期：2015.06.29
'****************************************************
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.IO
Imports System

Public Class RG1

#Region "下拉選單"
    Enum DisplayType
        OnlyName    '只顯示名字
        OnlyID      '顯示ID
        Full        '顯示ID + 名字
    End Enum

    Public Shared Sub FillDDL(ByVal objDDL As DropDownList, ByVal strTabName As String, ByVal strValue As String, ByVal strText As String, Optional ByVal Type As DisplayType = DisplayType.Full, Optional ByVal CondStr As String = "", Optional ByVal Distinct As String = "", Optional ByVal JoinStr As String = "")
        Dim objPA As New PA5()
        Try
            Using dt As DataTable = objPA.GetDDLInfo(strTabName, strValue, strText, CondStr, Distinct, JoinStr)
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

    Public Function GetDDLInfo(ByVal strTabName As String, ByVal strValue As String, ByVal strText As String, Optional ByVal CondStr As String = "", Optional ByVal Distinct As String = "", Optional ByVal JoinStr As String = "") As DataTable
        Dim strSQL As New StringBuilder
        strSQL.AppendLine(" Select ")
        If Distinct = "Y" Then strSQL.AppendLine(" Distinct ")
        strSQL.AppendLine(" " & strValue & " AS Code, " & strText & " AS CodeName, " & strValue & " + '-' + " & strText & " AS FullName " & " FROM " & strTabName)
        If JoinStr <> "" Then strSQL.AppendLine(" " + JoinStr + " ")
        strSQL.AppendLine(" Where 1=1 ")
        If CondStr <> "" Then strSQL.AppendLine(CondStr)
        strSQL.AppendLine("Order By " & strValue & " ")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "RG1000 新進員工資料輸入"
#Region "RG1001 新進員工資料輸入-新增"
    '2016/10/26 增加參數intEmpSenComp[201610270001]
    Public Function AddPersonalSetting(ByVal bePersonal As bePersonal.Row, ByVal bePersonalOther As bePersonalOther.Row, ByVal beEmployeeLog As beEmployeeLog.Row, _
                                       ByVal IsRankIDMapFlag As Boolean, ByVal beEmpWorkType() As beEmpWorkType.Row, ByVal beEmpPosition() As beEmpPosition.Row, _
                                       ByVal strCheckInFileCompID As String, ByVal beCheckInFile_SPHBK1 As beCheckInFile_SPHBK1.Row, ByVal beCheckInFile_SPHSC1 As beCheckInFile_SPHSC1.Row, _
                                       ByVal IsTypeA As Boolean, ByVal strPayrollCompID As String, ByVal beProbation As beProbation.Row, ByVal beProbationSPHSC1 As beProbationSPHSC1.Row, _
                                       ByVal beSalaryData As beSalaryData.Row, ByVal IsSalary As Boolean, ByVal beSalary() As beSalary.Row, ByVal beEmpRetireWait As beEmpRetireWait.Row, ByVal beEmpFlow As beEmpFlow.Row, ByVal beCommunication As beCommunication.Row, _
                                       ByVal beEmpSenRank As beEmpSenRank.Row, ByVal beEmpSenOrgType As beEmpSenOrgType.Row, ByVal beEmpSenOrgTypeFlow As beEmpSenOrgTypeFlow.Row, ByVal beEmpSenWorkType As beEmpSenWorkType.Row, ByVal beEmpSenPosition As beEmpSenPosition.Row, ByVal beEmpSenComp As beEmpSenComp.Row, _
                                       ByVal beInsureWait As beInsureWait.Row, ByVal beGroupWait As beGroupWait.Row _
                                       , ByVal intEmpSenComp As Integer _
                                       ) As Boolean

        Dim strSQL As New StringBuilder()

        Dim bsPersonal As New bePersonal.Service()
        Dim bsPersonalOther As New bePersonalOther.Service()
        Dim bsEmpWorkType As New beEmpWorkType.Service()
        Dim bsEmpPosition As New beEmpPosition.Service()
        Dim bsCheckInFile_SPHBK1 As New beCheckInFile_SPHBK1.Service()
        Dim bsCheckInFile_SPHSC1 As New beCheckInFile_SPHSC1.Service()
        Dim bsEmployeeLog As New beEmployeeLog.Service()
        Dim bsProbation As New beProbation.Service()
        Dim bsProbationSPHSC1 As New beProbationSPHSC1.Service()
        Dim bsSalaryData As New beSalaryData.Service()
        Dim bsSalary As New beSalary.Service()
        Dim bsEmpRetireWait As New beEmpRetireWait.Service()
        Dim bsEmpFlow As New beEmpFlow.Service()
        Dim bsCommunication As New beCommunication.Service()
        Dim bsEmpSenRank As New beEmpSenRank.Service()
        Dim bsEmpSenOrgType As New beEmpSenOrgType.Service()
        Dim bsEmpSenOrgTypeFlow As New beEmpSenOrgTypeFlow.Service()
        Dim bsEmpSenWorkType As New beEmpSenWorkType.Service()
        Dim bsEmpSenPosition As New beEmpSenPosition.Service()
        Dim bsEmpSenComp As New beEmpSenComp.Service()
        Dim bsInsureWait As New beInsureWait.Service()
        Dim bsGroupWait As New beGroupWait.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                '1. 新增員工基本資料[Personal]員工檔
                bsPersonal.Insert(bePersonal, tran)

                '2. 新增員工特殊設定資料檔[PersonalOther]
                If bePersonalOther.WTID.Value <> "" Or bePersonalOther.RecID.Value <> "" Or bePersonalOther.AboriginalTribe.Value <> "" Then
                    bsPersonalOther.Insert(bePersonalOther, tran)
                End If

                '3. 新增員工工作性質檔[EmpWorkType]
                bsEmpWorkType.Insert(beEmpWorkType, tran)

                '4. 新增員工職位檔[EmpPosition]
                If IsRankIDMapFlag Then
                    bsEmpPosition.Insert(beEmpPosition, tran)
                End If

                '5. 新增員工報到文件檔[CheckInFile]
                If strCheckInFileCompID = "SPHBK1" Then
                    bsCheckInFile_SPHBK1.Insert(beCheckInFile_SPHBK1, tran)
                ElseIf strCheckInFileCompID = "SPHSC1" Then
                    bsCheckInFile_SPHSC1.Insert(beCheckInFile_SPHSC1, tran)
                End If

                '6. 新增員工異動記錄檔[EmployeeLog]
                bsEmployeeLog.Insert(beEmployeeLog, tran)

                '7. 新增試用考核檔[Probation](A類人員)
                If IsTypeA Then
                    If strPayrollCompID <> "SPHSC1" Then
                        bsProbation.Insert(beProbation, tran)
                    Else
                        bsProbationSPHSC1.Insert(beProbationSPHSC1, tran)
                    End If
                End If

                '8. 新增員工薪資資料檔[SalaryData]
                bsSalaryData.Insert(beSalaryData, tran)

                '9. 新增薪資主檔[Salary]
                If IsSalary Then
                    bsSalary.Insert(beSalary, tran)
                End If

                '10. 新增新進員工勞退新制放行作業檔[EmpRetireWait]
                bsEmpRetireWait.Insert(beEmpRetireWait, tran)

                '11. 新增員工最小簽核單位檔[EmpFlow]
                bsEmpFlow.Insert(beEmpFlow, tran)

                '13.1 新增職等年資EmpSenRank
                bsEmpSenRank.Insert(beEmpSenRank, tran)

                '13.2 新增單位年資EmpSenOrgType
                bsEmpSenOrgType.Insert(beEmpSenOrgType, tran)

                '13.3 新增簽核單位年資EmpSenOrgTypeFlow
                bsEmpSenOrgTypeFlow.Insert(beEmpSenOrgTypeFlow, tran)

                '13.4 新增工作性質年資檔EmpSenWorkType
                bsEmpSenWorkType.Insert(beEmpSenWorkType, tran)

                '13.5 新增職位年資檔EmpSenPosition
                bsEmpSenPosition.Insert(beEmpSenPosition, tran)

                '13.6 新增公司年資檔EmpSenComp
                If intEmpSenComp = 0 Then   '2016/10/26 增加判斷[201610270001]
                    bsEmpSenComp.Insert(beEmpSenComp, tran)
                End If

                '14.
                If bePersonalOther.RecID.Value <> "" Then
                    '14.2 新增勞健InsureWait保險待加退保檔
                    bsInsureWait.Insert(beInsureWait, tran)

                    '14.4 新增團保GroupWait
                    bsGroupWait.Insert(beGroupWait, tran)

                    '14.5.a 學歷資料 Education
                    Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
                    Dim dbCommand_Education As DbCommand = db.GetStoredProcCommand("sHR2300_Education")
                    db.AddInParameter(dbCommand_Education, "@IDNo", DbType.String, bePersonal.IDNo.Value)
                    db.AddInParameter(dbCommand_Education, "@LastChgComp", DbType.String, bePersonal.LastChgComp.Value)
                    db.AddInParameter(dbCommand_Education, "@LastChgID", DbType.String, bePersonal.LastChgID.Value)
                    db.AddInParameter(dbCommand_Education, "@LastChgDate", DbType.DateTime, Now)

                    db.AddOutParameter(dbCommand_Education, "@sp_Complete1", DbType.Int32, 1)
                    db.AddOutParameter(dbCommand_Education, "@sp_ErrorSave", DbType.Int32, 10)
                    db.ExecuteNonQuery(dbCommand_Education, tran)

                    '14.5.b 前職資料 Experience
                    Dim dbCommand_Experience As DbCommand = db.GetStoredProcCommand("sHR2300_Experience")
                    db.AddInParameter(dbCommand_Experience, "@IDNo", DbType.String, bePersonal.IDNo.Value)
                    db.AddInParameter(dbCommand_Experience, "@LastChgComp", DbType.String, bePersonal.LastChgComp.Value)
                    db.AddInParameter(dbCommand_Experience, "@LastChgID", DbType.String, bePersonal.LastChgID.Value)
                    db.AddInParameter(dbCommand_Experience, "@LastChgDate", DbType.DateTime, Now)

                    db.AddOutParameter(dbCommand_Experience, "@sp_Complete1", DbType.Int32, 1)
                    db.AddOutParameter(dbCommand_Experience, "@sp_ErrorSave", DbType.Int32, 10)
                    db.ExecuteNonQuery(dbCommand_Experience, tran)

                    '14.5.c 家庭資料 Family
                    Dim dbCommand_Family As DbCommand = db.GetStoredProcCommand("sHR2300_Family")
                    db.AddInParameter(dbCommand_Family, "@IDNo", DbType.String, bePersonal.IDNo.Value)
                    db.AddInParameter(dbCommand_Family, "@LastChgComp", DbType.String, bePersonal.LastChgComp.Value)
                    db.AddInParameter(dbCommand_Family, "@LastChgID", DbType.String, bePersonal.LastChgID.Value)
                    db.AddInParameter(dbCommand_Family, "@LastChgDate", DbType.DateTime, Now)

                    db.AddOutParameter(dbCommand_Family, "@sp_Complete1", DbType.Int32, 1)
                    db.AddOutParameter(dbCommand_Family, "@sp_ErrorSave", DbType.Int32, 10)
                    db.ExecuteNonQuery(dbCommand_Family, tran)

                    '14.5.d 通訊資料 Communication
                    Dim dbCommand_Communication As DbCommand = db.GetStoredProcCommand("sHR2300_Communication")
                    db.AddInParameter(dbCommand_Communication, "@IDNo", DbType.String, bePersonal.IDNo.Value)
                    db.AddInParameter(dbCommand_Communication, "@LastChgComp", DbType.String, bePersonal.LastChgComp.Value)
                    db.AddInParameter(dbCommand_Communication, "@LastChgID", DbType.String, bePersonal.LastChgID.Value)
                    db.AddInParameter(dbCommand_Communication, "@LastChgDate", DbType.DateTime, Now)

                    db.AddOutParameter(dbCommand_Communication, "@sp_Complete1", DbType.Int32, 1)
                    db.AddOutParameter(dbCommand_Communication, "@sp_ErrorSave", DbType.Int32, 10)
                    db.ExecuteNonQuery(dbCommand_Communication, tran)

                    '14.6 回寫招募系統
                    strSQL.Clear()
                    strSQL.AppendLine(" UPDATE " + Bsp.Utility.getAppSetting("RecruitDB") + ".dbo.RE_ContractData SET ")
                    strSQL.AppendLine(" EmpDate = " & Bsp.Utility.Quote(bePersonal.EmpDate.Value))
                    strSQL.AppendLine(" , CheckInFlag = 'Y'")
                    strSQL.AppendLine(" , EmpID = " & Bsp.Utility.Quote(bePersonal.EmpID.Value))
                    strSQL.AppendLine(" , FinalFlag = '4'")
                    strSQL.AppendLine(" , LastChgComp = " & Bsp.Utility.Quote(bePersonal.LastChgComp.Value))
                    strSQL.AppendLine(" , LastChgID = " & Bsp.Utility.Quote(bePersonal.LastChgID.Value))
                    strSQL.AppendLine(" , LastChgDate  = GetDate() ")
                    strSQL.AppendLine(" WHERE CompID  = " & Bsp.Utility.Quote(bePersonal.CompID.Value))
                    strSQL.AppendLine(" AND RecID  = " & Bsp.Utility.Quote(bePersonalOther.RecID.Value))
                    strSQL.AppendLine(" AND CheckInDate = " & Bsp.Utility.Quote(bePersonalOther.CheckInDate.Value))
                    Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)
                End If

                '12. 新增通訊資料檔[寫入Communication]
                If bsCommunication.IsDataExists(beCommunication, tran) Then
                    bsCommunication.Update(beCommunication, tran)
                Else
                    bsCommunication.Insert(beCommunication, tran)
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

#Region "RG1001 查詢多個欄位值"
    Public Function QueryTable(ByVal strTable As String, ByVal strWhere As String, ByVal strcolum As String) As DataTable
        Dim strSQL As String
        strSQL = "Select " & strcolum & " From " & strTable
        strSQL = strSQL & " Where 1 = 1 " & strWhere
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL, "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "RG1001 金額解密"
    Public Function Decryption(ByVal strTable As String, ByVal strWhere As String, ByVal strcolum As String) As String
        Dim strSQL As String
        strSQL = Bsp.Utility.getAppSetting("RecruitDES")
        strSQL = strSQL & " Select " & strcolum & " From " & strTable
        strSQL = strSQL & " Where 1 = 1 " & strWhere
        If Bsp.DB.ExecuteScalar(strSQL.ToString(), "Recruit") = Nothing Then
            Return ""
        Else
            Return Bsp.DB.ExecuteScalar(strSQL.ToString(), "Recruit")
        End If
    End Function
#End Region

#End Region

#Region "RG1003 新進員工資料輸入-刪除"
    Public Function DeletePersonalSetting(ByVal bePersonal As bePersonal.Row) As Boolean
        Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
        Dim dbCommand As DbCommand = db.GetStoredProcCommand("SP_DeletePersonal")

        db.AddInParameter(dbCommand, "@InCompID", DbType.String, bePersonal.CompID.Value.ToString)
        db.AddInParameter(dbCommand, "@InEmpID", DbType.String, bePersonal.EmpID.Value.ToString)
        db.ExecuteNonQuery(dbCommand)

        Return True
    End Function
#End Region
#End Region

#Region "RG1100 新進臨時人員資料輸入"

#Region "RG1100 新進臨時人員資料輸入-查詢"
    Public Function QueryPersonalOutsourcingSetting(ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT ")
        strSQL.AppendLine(" P.CompID, P.EmpID, P.NameN AS Name, P.Sex, CASE P.Sex WHEN '1' THEN '男' WHEN '2' THEN '女' END AS SexN ")
        strSQL.AppendLine(" , BirthDate = Case When Convert(Char(10), P.BirthDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, P.BirthDate, 111) End  ")
        strSQL.AppendLine(" , P.IDNo, P.EduID, ISNULL(Edu.EduName, '') AS EduName ")
        strSQL.AppendLine(" , P.HighSchool, P.HighDepart, P.SchoolStatus, ISNULL(hrCode.CodeCName, '') AS SchoolStatusName ")
        strSQL.AppendLine(" , P.EmpAttrib, ISNULL(hrCode2.CodeCName, '') AS EmpAttribName ")
        strSQL.AppendLine(" , ContractStartDate = Case When Convert(Char(10), P.ContractStartDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, P.ContractStartDate, 111) End ")
        strSQL.AppendLine(" , ContractQuitDate = Case When Convert(Char(10), P.ContractQuitDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, P.ContractQuitDate, 111) End ")
        strSQL.AppendLine(" , EmpDate = Case When Convert(Char(10), P.EmpDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, P.EmpDate, 111) End ")
        strSQL.AppendLine(" , CL.CompName AS LastChgComp, PL.NameN AS LastChgID ")
        strSQL.AppendLine(" , LastChgDate = Case When Convert(Char(10), P.LastChgDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, P.LastChgDate, 120) End ")
        strSQL.AppendLine(" FROM PersonalOutsourcing P ")
        strSQL.AppendLine(" LEFT JOIN EduDegree Edu ON P.EduID = Edu.EduID ")
        strSQL.AppendLine(" LEFT JOIN HRCodeMap hrCode ON hrCode.TabName = 'PersonalOutsourcing' AND hrCode.FldName = 'SchoolStatus' AND hrCode.NotShowFlag = '0' AND P.SchoolStatus = hrCode.Code ")
        strSQL.AppendLine(" LEFT JOIN HRCodeMap hrCode2 ON hrCode2.TabName = 'PersonalOutsourcing' AND hrCode2.FldName = 'EmpAttrib' AND hrCode2.NotShowFlag = '0' AND P.EmpAttrib = hrCode2.Code ")
        strSQL.AppendLine(" LEFT JOIN Company CL ON P.LastChgComp = CL.CompID ")
        strSQL.AppendLine(" LEFT JOIN Personal PL ON P.LastChgID = PL.EmpID AND P.LastChgComp = PL.CompID ")
        strSQL.AppendLine(" WHERE 1 = 1 ")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine(" AND P.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "IDNo"
                        strSQL.AppendLine(" AND P.IDNo LIKE '" & ht(strKey).ToString() + "%' ")
                    Case "EmpID"
                        strSQL.AppendLine(" AND P.EmpID LIKE '" & ht(strKey).ToString() + "%' ")
                    Case "Name"
                        strSQL.AppendLine(" AND P.NameN LIKE N'" & ht(strKey).ToString() + "%' ")
                End Select
            End If
        Next

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "RG1101 新進臨時人員資料輸入-新增"
    Public Function AddPersonalOutsourcingSetting(ByVal bePersonalOutsourcing As bePersonalOutsourcing.Row) As Boolean
        Dim bsPersonalOutsourcing As New bePersonalOutsourcing.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsPersonalOutsourcing.Insert(bePersonalOutsourcing, tran) = 0 Then Return False
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

#Region "RG1102 新進臨時人員資料輸入-修改"
    Public Function UpdatePersonalOutsourcingSetting(ByVal bePersonalOutsourcing As bePersonalOutsourcing.Row) As Boolean
        Dim bsPersonalOutsourcing As New bePersonalOutsourcing.Service()
        Dim strSQL_Personal As New StringBuilder()
        Dim strFamily As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                If bsPersonalOutsourcing.Update(bePersonalOutsourcing, tran) = 0 Then Return False
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

#Region "RG1100 新進臨時人員資料輸入-刪除"
    Public Function DeletePersonalOutsourcingSetting(ByVal bePersonalOutsourcing As bePersonalOutsourcing.Row) As Boolean
        Dim bsPersonalOutsourcing As New bePersonalOutsourcing.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As Data.Common.DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                bsPersonalOutsourcing.DeleteRowByPrimaryKey(bePersonalOutsourcing, tran)

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

#Region "RG1100 身分證字號是否存在PersonalOutsourcing"
    Public Function checkIDNoFromPersonalOutsourcing(ByVal IDNo As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" SELECT Count(*) AS Count FROM PersonalOutsourcing WHERE IDNo = " & Bsp.Utility.Quote(IDNo.ToString()))
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "RG1100 身分證字號是否存在Personal"
    Public Function checkIDNoFromPersonal(ByVal IDNo As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" SELECT Count(*) AS Count FROM Personal WHERE WorkStatus = '1' AND IDNo = " & Bsp.Utility.Quote(IDNo.ToString()))
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "RG1100 用科/組/課查詢WorkSite和GroupID"
    Public Function selectWorkSite(ByVal CompID As String, ByVal DeptID As String, ByVal OrganID As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" SELECT WorkSiteID, GroupID FROM Organization WHERE CompID = " & Bsp.Utility.Quote(CompID) & " AND DeptID = " & Bsp.Utility.Quote(DeptID) & " AND OrganID = " & Bsp.Utility.Quote(OrganID))
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

    Public Function GetOrgWaitData(ByVal CompID As String, ByVal OrganID As String, ByVal ValidDate As String, ByVal OrganType As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine("SELECT WorkSiteID, GroupID FROM OrganizationWait WHERE CompID = " & Bsp.Utility.Quote(CompID) & " AND OrganID = " & Bsp.Utility.Quote(OrganID) & " AND ValidDate <= " & Bsp.Utility.Quote(ValidDate))
        strSQL.AppendLine("And OrganReason='1' And WaitStatus='0' And OrganType IN ('" & OrganType & "', '3')")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#End Region

#Region "RG1200 整批新員資料上傳作業"

#Region "RG1200 自行訂義條件Table之資料數"
    Public Function IsDataExists(ByVal strTable As String, ByVal strWhere As String) As Boolean
        Dim strSQL As String
        strSQL = "Select Count(*) Cnt From " & strTable
        strSQL += " Where 1 = 1 " & strWhere
        Return IIf(Bsp.DB.ExecuteScalar(strSQL, "eHRMSDB") > 0, True, False)
    End Function
#End Region

#Region "RG1200 自行訂義條件Table之資料數-招募"
    Public Function IsDataExistsRecruit(ByVal strTable As String, ByVal strWhere As String) As String
        Dim strSQL As String
        strSQL = "Select Count(*) Cnt From " & strTable
        strSQL += " Where 1 = 1 " & strWhere
        Return Bsp.DB.ExecuteScalar(strSQL, "Recruit")
    End Function
#End Region

#Region "RG1200 取Seq"
    Public Function GetSeq(ByVal table As String, ByVal col As String) As String '2015/12/08 Modify
        Dim strSQL As String
        strSQL = "Select ISNULL(Max(Seq)+1,'1') "   '2015/12/11 Modify 預設改為1
        strSQL = strSQL & " from " & table
        strSQL = strSQL & " WHERE 1=1 " & col
        If Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB") = Nothing Then
            Return ""
        Else
            Return Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")
        End If
    End Function
#End Region

#Region "RG1200 取欄位值"
    Public Function GetName(ByVal table As String, ByVal where As String) As String
        Dim strSQL As String
        strSQL = "Select isnull(Remark, '') AS Remark "
        strSQL = strSQL & " from " & table
        strSQL = strSQL & " WHERE " & where

        If Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB") = Nothing Then
            Return ""
        Else
            Return Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")
        End If
    End Function
#End Region

#Region "RG1200 取單位名稱"
    Public Function funGetEmpLogName(ByVal OrganID As String) As String
        Dim strSQL As String
        strSQL = "Select isnull(OrganName, '') as Name from OrganizationFlow where OrganID=" & Bsp.Utility.Quote(OrganID)

        If Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB") = Nothing Then
            Return ""
        Else
            Return Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")
        End If
    End Function
#End Region

#Region "RG1200 員工到職日"
    Public Function GetEmpDate(ByVal CompID As String, ByVal EmpID As String) As String
        Dim strSQL As String
        strSQL = "Select convert(char(10), EmpDate, 111) FROM Personal "
        strSQL = strSQL & "where CompID = " & Bsp.Utility.Quote(CompID)
        strSQL = strSQL & "and EmpID = " & Bsp.Utility.Quote(EmpID)

        If Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB") = Nothing Then
            Return ""
        Else
            Return Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")
        End If
    End Function
#End Region

#Region "RG1200 查詢單一欄位值"
    Public Function QueryData(ByVal strTable As String, ByVal strWhere As String, ByVal strcolum As String) As String
        Dim strSQL As String
        strSQL = "Select " & strcolum & " From " & strTable
        strSQL = strSQL & " Where 1 = 1 " & strWhere
        If Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB") = Nothing Then
            Return ""
        Else
            Return Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")
        End If
    End Function
#End Region

#Region "RG1200 查詢單一欄位值-招募"
    Public Function QueryDataRecruit(ByVal strTable As String, ByVal strWhere As String, ByVal strcolum As String) As String
        Dim strSQL As String
        strSQL = "Select " & strcolum & " From " & strTable
        strSQL = strSQL & " Where 1 = 1 " & strWhere
        If Bsp.DB.ExecuteScalar(strSQL.ToString(), "Recruit") = Nothing Then
            Return ""
        Else
            Return Bsp.DB.ExecuteScalar(strSQL.ToString(), "Recruit")
        End If
    End Function
#End Region

#Region "RG1200 回寫招募系統-H00017功能-報到註記-Y報到及實際報到日欄位 DB欄位"
    Public Function UpdateRE_ContractData(ByVal strEmpDate As String, ByVal strEmpID As String, ByVal strCompID As String, ByVal strRecID As String, ByVal strCheckInDate As String) As Boolean
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("Recruit")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            strSQL.AppendLine(" Update RE_ContractData Set ")
            strSQL.AppendLine(" EmpDate = " & Bsp.Utility.Quote(strEmpDate))
            strSQL.AppendLine(" , CheckInFlag = 'Y' ")
            strSQL.AppendLine(" , EmpID = " & Bsp.Utility.Quote(strEmpID))
            strSQL.AppendLine(" , FinalFlag = '4' ")
            strSQL.AppendLine(" , ChkLastChgComp = 'RG1200' ")
            strSQL.AppendLine(" , ChkLastChgID = 'RG1200' ")
            strSQL.AppendLine(" , ChkLastChgDate = GETDATE() ")
            strSQL.AppendLine(" WHERE RecID = " & Bsp.Utility.Quote(strRecID) & " AND CompID = " & Bsp.Utility.Quote(strCompID) & " AND CheckInDate = " & Bsp.Utility.Quote(strCheckInDate))

            Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)
            tran.Commit()
        End Using
        Return True
    End Function
#End Region

#Region "RG1300 新進員工文件繳交作業_銀行-查詢"
    Public Function QueryOrganization(ByVal strOrganID As String, ByVal strDeptID As String, ByVal strCompID As String) As DataTable
        Dim strSQL As New StringBuilder()

        '2015/11/23 Modify SQL語法修改
        'strSQL.AppendLine(" select o2.OrgType as OrgType, o2.OrganName as OrgTypeName, o1.OrganID as OrganID, o1.OrganName as OrganName ")
        'strSQL.AppendLine(" from Personal p ")
        'strSQL.AppendLine(" left join Organization o1 on o1.CompID=p.CompID and o1.OrganID=p.DeptID ")
        'strSQL.AppendLine(" left join Organization o2 on o2.CompID=p.CompID and o2.OrganID=o1.OrgType  ")
        'strSQL.AppendLine(" where(1 = 1) ")
        'strSQL.AppendLine(" and o1.VirtualFlag = '0' and o1.InValidFlag = '0' ")
        'strSQL.AppendLine(" and o2.VirtualFlag = '0' and o2.InValidFlag = '0' ")
        'strSQL.AppendLine(" and p.OrganID = " & Bsp.Utility.Quote(strOrganID))
        'strSQL.AppendLine(" and p.DeptID = " & Bsp.Utility.Quote(strDeptID))
        'strSQL.AppendLine(" and p.CompID = " & Bsp.Utility.Quote(strCompID))
        'strSQL.AppendLine(" and p.EmpID = " & Bsp.Utility.Quote(strEmpID))

        strSQL.AppendLine(" select o2.OrgType as OrgType, o2.OrganName as OrgTypeName, o1.OrganID as OrganID, o1.OrganName as OrganName ")
        strSQL.AppendLine(" from Organization o ")
        strSQL.AppendLine(" left join Organization o1 on o1.CompID = o.CompID and o1.OrganID = o.DeptID--部 ")
        strSQL.AppendLine(" left join Organization o2 on o2.CompID = o.CompID and o2.OrganID = o1.OrgType--處 ")
        strSQL.AppendLine(" where 1=1 ")
        strSQL.AppendLine(" and o1.VirtualFlag = '0' and o1.InValidFlag = '0' ")
        strSQL.AppendLine(" and o2.VirtualFlag = '0' and o2.InValidFlag = '0' ")
        strSQL.AppendLine(" and o.OrganID = " & Bsp.Utility.Quote(strOrganID))
        strSQL.AppendLine(" and o.DeptID = " & Bsp.Utility.Quote(strDeptID))
        strSQL.AppendLine(" and o.CompID = " & Bsp.Utility.Quote(strCompID))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "RG1200 更新勞退主檔EmpRetire"
    Public Function UpdateEmpRetire(ByVal strCompID As String, ByVal strEmpID As String, ByVal strAmount As String) As Boolean
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            strSQL.AppendLine(" Update EmpRetire Set ")
            strSQL.AppendLine(" Amount = " & Bsp.Utility.Quote(strAmount))
            strSQL.AppendLine(" , LastChgComp = 'RG1200' ")
            strSQL.AppendLine(" , LastChgID = 'RG1200' ")
            strSQL.AppendLine(" , LastChgDate = GETDATE() ")
            strSQL.AppendLine(" WHERE CompID = " & Bsp.Utility.Quote(strCompID) & " AND EmpID = " & Bsp.Utility.Quote(strEmpID) & " AND Kind = '1' ")

            Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)
            tran.Commit()
        End Using
        Return True
    End Function
#End Region

#End Region

#Region "RG1300 新進員工文件繳交作業_銀行"

#Region "計算報到日迄今"
    Public Function CalEmpDay(ByVal CompID As String, ByVal Direction As String, ByVal BaseDate As String, ByVal Days As Integer) As String
        Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
        Dim dbCommand As DbCommand = db.GetStoredProcCommand("sHR0100")
        Dim strOutDate As String

        db.AddInParameter(dbCommand, "@CompID", DbType.String, CompID)
        db.AddInParameter(dbCommand, "@Direction", DbType.String, Direction)
        db.AddInParameter(dbCommand, "@BaseDate", DbType.DateTime, BaseDate)
        db.AddInParameter(dbCommand, "@Days", DbType.Int32, Days)


        db.AddOutParameter(dbCommand, "@OutDate", DbType.DateTime, 10)
        db.AddOutParameter(dbCommand, "@sp_Complete", DbType.Int32, 1)
        db.ExecuteNonQuery(dbCommand)

        strOutDate = db.GetParameterValue(dbCommand, "@OutDate")

        Return strOutDate
    End Function
#End Region

#Region "RG1300 新進員工文件繳交作業_銀行-查詢"
    Public Function QueryCheckInFile_SPHBK1(ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        Dim objRG As New RG1
        Dim day_start As String
        Dim day_end As String
        Dim CompID As String

        strSQL.AppendLine(" SELECT ")
        strSQL.AppendLine(" C.CompID, ISNULL(Cmp.CompName, '') AS CompName, C.EmpID, P.NameN AS Name, Case P.CheckInFlag When '3' Then '已繳齊' Else '未繳齊' End AS CheckInFlag ")
        strSQL.AppendLine(" , ISNULL(O.OrganName, '') AS OrganName, EmpDate = Case When Convert(Char(10), P.EmpDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, P.EmpDate, 111) End, DATEDIFF(DD, P.EmpDate, GETDATE()) AS EmpDay ")
        strSQL.AppendLine(" , 19 - (CAST(File1 AS Int) + CAST(File2 AS Int) + CAST(File3 AS Int) + CAST(File4 AS Int) + CAST(File5 AS Int) ")
        strSQL.AppendLine(" + CAST(File6 AS Int) + CAST(File7 AS Int) + CAST(File8 AS Int) + CAST(File9 AS Int) + CAST(File10 AS Int) ")
        strSQL.AppendLine(" + CAST(File11 AS Int) + CAST(File12 AS Int) + CAST(File13 AS Int) + CAST(File14 AS Int) + CAST(File15 AS Int) ")
        strSQL.AppendLine(" + CAST(File16 AS Int) + CAST(File17 AS Int) + CAST(File18 AS Int) + CAST(File19 AS Int)) AS LackOfParts ")
        strSQL.AppendLine(" , File1, File2, File3, File4, File5, File6, File7, File8, File9, File10 ")
        strSQL.AppendLine(" , File11, File12, File13, File14, File15, File16, File17, File18, File19 ")
        strSQL.AppendLine(" FROM CheckInFile_SPHBK1 C ")
        strSQL.AppendLine(" INNER JOIN Personal P ON C.CompID = P.CompID AND C.EmpID = P.EmpID ")
        strSQL.AppendLine(" LEFT JOIN Organization O ON P.CompID = O.CompID AND P.DeptID = O.OrganID ")
        strSQL.AppendLine(" LEFT JOIN Company AS Cmp ON C.CompID = Cmp.CompID ")
        strSQL.AppendLine(" WHERE 1 = 1 ")
        strSQL.AppendLine(" AND P.WorkStatus IN('1','4','5','7') ")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                CompID = ht("CompID")
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine(" AND C.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "EmpDay"
                        Select Case ht(strKey).ToString()
                            Case "1"
                                day_start = objRG.CalEmpDay(CompID, "1", Now, 3)
                                day_end = objRG.CalEmpDay(CompID, "1", Now, 7)
                                strSQL.AppendLine(" AND P.EmpDate <= Convert(Char(10), " & Bsp.Utility.Quote(day_start) & ", 111) ")
                                strSQL.AppendLine(" AND P.EmpDate >= Convert(Char(10), " & Bsp.Utility.Quote(day_end) & ", 111) ")
                            Case "2"
                                day_start = objRG.CalEmpDay(CompID, "1", Now, 7)
                                day_end = objRG.CalEmpDay(CompID, "1", Now, 13)
                                strSQL.AppendLine(" AND P.EmpDate <= Convert(Char(10), " & Bsp.Utility.Quote(day_start) & ", 111) ")
                                strSQL.AppendLine(" AND P.EmpDate >= Convert(Char(10), " & Bsp.Utility.Quote(day_end) & ", 111) ")
                            Case "3"
                                day_start = objRG.CalEmpDay(CompID, "1", Now, 14)
                                strSQL.AppendLine(" AND P.EmpDate <= Convert(Char(10), " & Bsp.Utility.Quote(day_start) & ", 111) ")
                        End Select
                    Case "EmpID"
                        strSQL.AppendLine(" AND P.EmpID LIKE '" & ht(strKey).ToString() + "%' ")
                    Case "Name"
                        strSQL.AppendLine(" AND P.NameN LIKE N'" & ht(strKey).ToString() + "%' ")
                    Case "Status"
                        Select Case ht(strKey).ToString()
                            Case "1"
                                strSQL.AppendLine(" AND P.CheckInFlag <> '3' ")
                            Case "2"
                                strSQL.AppendLine(" AND P.CheckInFlag = '3' ")
                        End Select
                End Select
            End If
        Next
        strSQL.AppendLine(" ORDER BY C.EmpID ")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "RG1301 新進員工文件繳交作業_銀行-新增"
    Public Function AddCheckInFile_SPHBK1(ByVal beCheckInFile_SPHBK1 As beCheckInFile_SPHBK1.Row, ByVal LastChgCompID As String, ByVal LastChgID As String) As Boolean
        Dim bsCheckInFile_SPHBK1 As New beCheckInFile_SPHBK1.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            If bsCheckInFile_SPHBK1.Insert(beCheckInFile_SPHBK1, tran) = 0 Then
                Return False
            Else
                '同步更新Personal
                strSQL.AppendLine(" UPDATE Personal SET ")
                strSQL.AppendLine(" CheckInFlag = '1' ")
                strSQL.AppendLine(" , LastChgComp = " & Bsp.Utility.Quote(LastChgCompID))
                strSQL.AppendLine(" , LastChgID = " & Bsp.Utility.Quote(LastChgID))
                strSQL.AppendLine(" , LastChgDate = GETDATE() ")
                strSQL.AppendLine(" WHERE EmpID = " & Bsp.Utility.Quote(beCheckInFile_SPHBK1.EmpID.Value) & " AND CompID = " & Bsp.Utility.Quote(beCheckInFile_SPHBK1.CompID.Value))

                '同步更新SalaryData
                strSQL.AppendLine(" UPDATE SalaryData SET ")
                strSQL.AppendLine(" SalaryFlag = '0' ")
                strSQL.AppendLine(" , LastChgComp = " & Bsp.Utility.Quote(LastChgCompID))
                strSQL.AppendLine(" , LastChgID = " & Bsp.Utility.Quote(LastChgID))
                strSQL.AppendLine(" , LastChgDate = GETDATE() ")
                strSQL.AppendLine(" WHERE EmpID = " & Bsp.Utility.Quote(beCheckInFile_SPHBK1.EmpID.Value) & " AND CompID = " & Bsp.Utility.Quote(beCheckInFile_SPHBK1.CompID.Value))

                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)
                tran.Commit()
            End If
        End Using
        Return True
    End Function
#End Region

#Region "RG1302 新進員工文件繳交作業_銀行-修改"
    Public Function UpdateCheckInFile_SPHBK1(ByVal beCheckInFile_SPHBK1 As beCheckInFile_SPHBK1.Row, ByVal LastChgCompID As String, ByVal LastChgID As String, ByVal strCheckInFlag As String, ByVal strSalaryFlag As String) As Boolean
        Dim bsCheckInFile_SPHBK1 As New beCheckInFile_SPHBK1.Service()
        Dim strSQL As New StringBuilder()
        Dim strFamily As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                If bsCheckInFile_SPHBK1.Update(beCheckInFile_SPHBK1, tran) = 0 Then
                    Return False
                Else
                    '同步更新Personal
                    strSQL.AppendLine(" UPDATE Personal SET ")
                    strSQL.AppendLine(" CheckInFlag = " & Bsp.Utility.Quote(strCheckInFlag))
                    strSQL.AppendLine(" , LastChgComp = " & Bsp.Utility.Quote(LastChgCompID))
                    strSQL.AppendLine(" , LastChgID = " & Bsp.Utility.Quote(LastChgID))
                    strSQL.AppendLine(" , LastChgDate = GETDATE() ")
                    strSQL.AppendLine(" WHERE EmpID = " & Bsp.Utility.Quote(beCheckInFile_SPHBK1.EmpID.Value) & " AND CompID = " & Bsp.Utility.Quote(beCheckInFile_SPHBK1.CompID.Value))

                    '同步更新SalaryData
                    strSQL.AppendLine(" UPDATE SalaryData SET ")
                    strSQL.AppendLine(" SalaryFlag = " & Bsp.Utility.Quote(strSalaryFlag))
                    strSQL.AppendLine(" , LastChgComp = " & Bsp.Utility.Quote(LastChgCompID))
                    strSQL.AppendLine(" , LastChgID = " & Bsp.Utility.Quote(LastChgID))
                    strSQL.AppendLine(" , LastChgDate = GETDATE() ")
                    strSQL.AppendLine(" WHERE EmpID = " & Bsp.Utility.Quote(beCheckInFile_SPHBK1.EmpID.Value) & " AND CompID = " & Bsp.Utility.Quote(beCheckInFile_SPHBK1.CompID.Value))

                    Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)
                    tran.Commit()
                End If

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

#Region "RG1300 新進員工文件繳交作業_銀行-刪除"
    Public Function DeleteCheckInFile_SPHBK1(ByVal beCheckInFile_SPHBK1 As beCheckInFile_SPHBK1.Row) As Boolean
        Dim bsCheckInFile_SPHBK1 As New beCheckInFile_SPHBK1.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As Data.Common.DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                bsCheckInFile_SPHBK1.DeleteRowByPrimaryKey(beCheckInFile_SPHBK1, tran)

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

#Region "RG1300 新進員工文件繳交作業_銀行-下傳"
    Public Function CheckInFile_SPHBK1Download(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()
        Dim strFieldNames As New StringBuilder()
        Dim aryStr() As String
        Dim objRG As New RG1
        Dim day_start As String
        Dim day_end As String
        Dim CompID As String

        For Each strKey As String In ht.Keys
            CompID = ht("CompID")
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine(" AND C.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "EmpDay"
                        Select Case ht(strKey).ToString()
                            Case "1"
                                day_start = objRG.CalEmpDay(CompID, "1", Now, 3)
                                day_end = objRG.CalEmpDay(CompID, "1", Now, 7)
                                strSQL.AppendLine(" AND P.EmpDate <= Convert(Char(10), " & Bsp.Utility.Quote(day_start) & ", 111) ")
                                strSQL.AppendLine(" AND P.EmpDate >= Convert(Char(10), " & Bsp.Utility.Quote(day_end) & ", 111) ")
                            Case "2"
                                day_start = objRG.CalEmpDay(CompID, "1", Now, 7)
                                day_end = objRG.CalEmpDay(CompID, "1", Now, 13)
                                strSQL.AppendLine(" AND P.EmpDate <= Convert(Char(10), " & Bsp.Utility.Quote(day_start) & ", 111) ")
                                strSQL.AppendLine(" AND P.EmpDate >= Convert(Char(10), " & Bsp.Utility.Quote(day_end) & ", 111) ")
                            Case "3"
                                day_start = objRG.CalEmpDay(CompID, "1", Now, 14)
                                strSQL.AppendLine(" AND P.EmpDate <= Convert(Char(10), " & Bsp.Utility.Quote(day_start) & ", 111) ")
                        End Select
                    Case "EmpID"
                        strSQL.AppendLine("And P.EmpID LIKE '" & ht(strKey).ToString() + "%' ")
                    Case "Name"
                        strSQL.AppendLine(" AND P.NameN LIKE N'" & ht(strKey).ToString() + "%' ")
                End Select
            End If
        Next

        strSQL.AppendLine(" AND P.CheckInFlag <> '3' ")
        strSQL.AppendLine(" AND P.WorkStatus IN('1','4','5','7') ")

        strFieldNames.AppendLine(" C.EmpID AS '員工編號', ")
        strFieldNames.AppendLine(" isnull(P.NameN, '') AS '姓名', ")
        strFieldNames.AppendLine(" isnull(O.OrganName, '') AS '部門', ")
        strFieldNames.AppendLine(" Convert(Char(10), P.EmpDate, 111) AS '到職日', ")
        strFieldNames.AppendLine(" DATEDIFF(DD, P.EmpDate, GETDATE()) AS '報到天數', ")
        strFieldNames.AppendLine(" 19 - (CAST(File1 AS Int) + CAST(File2 AS Int) + CAST(File3 AS Int) + CAST(File4 AS Int) + CAST(File5 AS Int) + CAST(File6 AS Int) + CAST(File7 AS Int) + CAST(File8 AS Int) + CAST(File9 AS Int) + CAST(File10 AS Int) + CAST(File11 AS Int) + CAST(File12 AS Int) + CAST(File13 AS Int) + CAST(File14 AS Int) + CAST(File15 AS Int) + CAST(File16 AS Int) + CAST(File17 AS Int) + CAST(File18 AS Int) + CAST(File19 AS Int)) AS '缺件數', ")
        strFieldNames.AppendLine(" CASE File1 WHEN '0' THEN '○' WHEN '1' THEN '●' END AS '基本資料', ")
        strFieldNames.AppendLine(" CASE File2 WHEN '0' THEN '○' WHEN '1' THEN '●' END AS '同意書', ")
        strFieldNames.AppendLine(" CASE File3 WHEN '0' THEN '○' WHEN '1' THEN '●' END AS '扶養親屬表', ")
        strFieldNames.AppendLine(" CASE File4 WHEN '0' THEN '○' WHEN '1' THEN '●' END AS '紀律表', ")
        strFieldNames.AppendLine(" CASE File5 WHEN '0' THEN '○' WHEN '1' THEN '●' END AS '身分證影本', ")
        strFieldNames.AppendLine(" CASE File6 WHEN '0' THEN '○' WHEN '1' THEN '●' END AS '學歷影本', ")
        strFieldNames.AppendLine(" CASE File7 WHEN '0' THEN '○' WHEN '1' THEN '●' END AS '離職證明', ")
        strFieldNames.AppendLine(" CASE File8 WHEN '0' THEN '○' WHEN '1' THEN '●' END AS '轉出申請表', ")
        strFieldNames.AppendLine(" CASE File9 WHEN '0' THEN '○' WHEN '1' THEN '●' END AS '退伍令', ")
        strFieldNames.AppendLine(" CASE File10 WHEN '0' THEN '○' WHEN '1' THEN '●' END AS '永豐帳戶', ")
        strFieldNames.AppendLine(" CASE File11 WHEN '0' THEN '○' WHEN '1' THEN '●' END AS '彰銀帳戶', ")
        strFieldNames.AppendLine(" CASE File12 WHEN '0' THEN '○' WHEN '1' THEN '●' END AS '健檢報告', ")
        strFieldNames.AppendLine(" CASE File13 WHEN '0' THEN '○' WHEN '1' THEN '●' END AS '2吋照', ")
        strFieldNames.AppendLine(" CASE File14 WHEN '0' THEN '○' WHEN '1' THEN '●' END AS '契約書', ")
        strFieldNames.AppendLine(" CASE File15 WHEN '0' THEN '○' WHEN '1' THEN '●' END AS '學生證/在學證明', ")
        strFieldNames.AppendLine(" CASE File16 WHEN '0' THEN '○' WHEN '1' THEN '●' END AS '健康聲明書', ")
        strFieldNames.AppendLine(" CASE File17 WHEN '0' THEN '○' WHEN '1' THEN '●' END AS '勞退徵詢表', ")
        strFieldNames.AppendLine(" CASE File18 WHEN '0' THEN '○' WHEN '1' THEN '●' END AS '刑事紀錄證明', ")
        strFieldNames.AppendLine(" CASE File19 WHEN '0' THEN '○' WHEN '1' THEN '●' END AS '保密切結書' ")
        Return GetCheckInFile_SPHBK1Download(strFieldNames.ToString(), strSQL.ToString())
    End Function

    Public Function GetCheckInFile_SPHBK1Download(Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames = "" Then FieldNames = "*"

        strSQL.AppendLine(" Select " & FieldNames)
        strSQL.AppendLine(" From CheckInFile_SPHBK1 C")
        strSQL.AppendLine(" INNER JOIN Personal P ON C.CompID = P.CompID AND C.EmpID = P.EmpID ")
        strSQL.AppendLine(" LEFT JOIN Organization O ON P.CompID = O.CompID AND P.DeptID = O.OrganID ")
        strSQL.AppendLine(" LEFT JOIN Company AS Cmp ON C.CompID = Cmp.CompID ")
        strSQL.AppendLine(" Where 1 = 1")
        If CondStr <> "" Then strSQL.AppendLine(CondStr)
        strSQL.AppendLine(" Order by C.CompID, C.EmpID ")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

#End Region

#Region "提供刑事紀錄證明" 'Return True:需提供刑事紀錄證明 / Return False:不需提供刑事紀錄證明
    Public Function IsCredit(ByVal strCompID As String, ByVal strEmpID As String, Optional ByVal strWorkTypeID As String = "") As Boolean
        Dim objHR As New HR()
        Dim arrCompID As String() = {"SPHBK1", "SPHOLD", "SPHPIA", "SPHPLA", "SPHSPC", "SPHVCL"}
        Dim rtValue As Boolean = True

        For Each item In arrCompID
            If strCompID = item Then
                rtValue = False
                Exit For
            End If
        Next

        rtValue = objHR.funIsCredit(strCompID, strEmpID, strWorkTypeID)

        If IsSWorkType(strCompID, strEmpID) Then
            rtValue = True
        End If

        Return rtValue
    End Function
#End Region

#Region "消金業務推廣BSNM1R、信貸業務推廣BSNM1Z、個金業務開發BSNM03、法人金融業務BSL003、個金業務專員BSNPB1、理財規劃專員BSNPB3"
    Public Function IsSWorkType(ByVal strCompID As String, ByVal strEmpID As String) As Boolean
        If IsDataExists("EmpWorkType", "AND CompID = " & Bsp.Utility.Quote(strCompID) & " AND EmpID = " & Bsp.Utility.Quote(strEmpID) & " AND WorkTypeID IN ('BSNM1R','BSNM1Z','BSNM03','BSL003','BSNPB1','BSNPB3')") Then
            Return True
        End If

        Return False
    End Function
#End Region

#End Region

#Region "RG1400 新進員工文件繳交作業_證券"
#Region "取得員工資料"
    Public Function GetEmpData(ByVal CompID As String, ByVal EmpID As String) As DataTable
        Dim strSQL As New StringBuilder()
        Dim PersonalTable As DataTable

        strSQL.AppendLine("SELECT P.EmpID")
        strSQL.AppendLine(", P.NameN")
        strSQL.AppendLine(", P.Sex")
        strSQL.AppendLine(", P.NationID")
        strSQL.AppendLine(", P.EmpType")
        strSQL.AppendLine(", P.RankID")
        strSQL.AppendLine(", P.RankIDMap")
        strSQL.AppendLine(", EmpDate = Case When Convert(Char(10), P.EmpDate, 111) = '1900/01/01' Then '' Else Convert(Varchar, P.EmpDate, 111) End")
        strSQL.AppendLine(", P.OrganID")
        strSQL.AppendLine(", O.OrganID + '-' + O.OrganName AS OrganName")
        strSQL.AppendLine("FROM Personal P")
        strSQL.AppendLine("LEFT JOIN Organization O ON P.CompID = O.CompID AND P.OrganID = O.OrganID ")
        strSQL.AppendLine("WHERE 1=1")
        strSQL.AppendLine("AND P.WorkStatus IN ('1', '4', '5', '7')")
        strSQL.AppendLine("AND P.CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine("AND P.EmpID = " & Bsp.Utility.Quote(EmpID))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "查詢新進員工文件繳交作業_證券"
    Public Function QueryCheckInFile_SPHSC1(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        Dim StartDate As String = ""
        Dim EndDate As String = ""

        strSQL.AppendLine("SELECT C.CompID")
        strSQL.AppendLine(", ISNULL(Cmp.CompName, '') AS CompName")
        strSQL.AppendLine(", C.EmpID")
        strSQL.AppendLine(", P.NameN AS Name ")
        strSQL.AppendLine(", Case P.CheckInFlag When '3' Then '已繳齊' Else '未繳齊' End AS CheckInFlag ")
        strSQL.AppendLine(", ISNULL(O.OrganName, '') AS OrganName")
        strSQL.AppendLine(", EmpDate = Case When Convert(Char(10), P.EmpDate, 111) = '1900/01/01' Then '' Else Convert(Varchar, P.EmpDate, 111) End")
        strSQL.AppendLine(", DATEDIFF(DD, P.EmpDate, GETDATE()) AS EmpDay")
        strSQL.AppendLine(", 18 - (CAST(File1 AS Int) + CAST(File2 AS Int) + CAST(File3 AS Int) + CAST(File4 AS Int) + CAST(File5 AS Int)")
        strSQL.AppendLine("+ CAST(File6 AS Int) + CAST(File7 AS Int) + CAST(File8 AS Int) + CAST(File9 AS Int) + CAST(File10 AS Int)")
        strSQL.AppendLine("+ CAST(File11 AS Int) + CAST(File12 AS Int) + CAST(File13 AS Int) + CAST(File14 AS Int) + CAST(File15 AS Int)")
        strSQL.AppendLine("+ CAST(File16 AS Int) + CAST(File17 AS Int) + CAST(File18 AS Int)) AS LackOfParts")
        strSQL.AppendLine(", File1, File2, File3, File4, File5, File6, File7, File8, File9, File10")
        strSQL.AppendLine(", File11, File12, File13, File14, File15, File16, File17, File18")
        strSQL.AppendLine("FROM CheckInFile_SPHSC1 C")
        strSQL.AppendLine("INNER JOIN Personal P ON C.CompID = P.CompID AND C.EmpID = P.EmpID")
        strSQL.AppendLine("LEFT JOIN Organization O ON P.CompID = O.CompID AND P.DeptID = O.OrganID")
        strSQL.AppendLine("LEFT JOIN Company AS Cmp ON C.CompID = Cmp.CompID")
        strSQL.AppendLine("WHERE 1 = 1")
        strSQL.AppendLine("AND P.WorkStatus IN('1','4','5','7')")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        If ht(strKey).ToString() <> "0" Then
                            strSQL.AppendLine("And C.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        End If
                    Case "EmpID"
                        strSQL.AppendLine("And C.EmpID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "Name"
                        strSQL.AppendLine("And P.NameN Like N'%" & ht(strKey).ToString() + "%' ")
                    Case "EmpDay"
                        Select Case ht(strKey).ToString()
                            Case "1"
                                StartDate = CalEmpDay(ht("CompID").ToString(), "1", Now, 3)
                                EndDate = CalEmpDay(ht("CompID").ToString(), "1", Now, 7)
                                strSQL.AppendLine(" AND P.EmpDate <= Convert(Char(10), " & Bsp.Utility.Quote(StartDate) & ", 111) ")
                                strSQL.AppendLine(" AND P.EmpDate >= Convert(Char(10), " & Bsp.Utility.Quote(EndDate) & ", 111) ")
                            Case "2"
                                StartDate = CalEmpDay(ht("CompID").ToString(), "1", Now, 7)
                                EndDate = CalEmpDay(ht("CompID").ToString(), "1", Now, 13)
                                strSQL.AppendLine(" AND P.EmpDate <= Convert(Char(10), " & Bsp.Utility.Quote(StartDate) & ", 111) ")
                                strSQL.AppendLine(" AND P.EmpDate >= Convert(Char(10), " & Bsp.Utility.Quote(EndDate) & ", 111) ")
                            Case "3"
                                StartDate = CalEmpDay(ht("CompID").ToString(), "1", Now, 14)
                                strSQL.AppendLine(" AND P.EmpDate <= Convert(Char(10), " & Bsp.Utility.Quote(StartDate) & ", 111) ")
                        End Select
                    Case "Status"
                        Select Case ht(strKey).ToString()
                            Case "1"
                                strSQL.AppendLine(" AND P.CheckInFlag <> '3' ")
                            Case "2"
                                strSQL.AppendLine(" AND P.CheckInFlag = '3' ")
                        End Select
                End Select
            End If
        Next

        strSQL.AppendLine("Order by C.EmpID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "新增新進員工文件繳交作業_證券"
    Public Function AddCheckInFile_SPHSC1(ByVal beCheckInFile_SPHSC1 As beCheckInFile_SPHSC1.Row, ByVal bePersonal As bePersonal.Row) As Boolean
        Dim bsCheckInFile_SPHSC1 As New beCheckInFile_SPHSC1.Service()
        Dim bsPersonal As New bePersonal.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsCheckInFile_SPHSC1.Insert(beCheckInFile_SPHSC1, tran) = 0 Then Return False
                If bsPersonal.Update(bePersonal, tran) = 0 Then Return False
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

#Region "修改新進員工文件繳交作業_證券"
    Public Function UpdateCheckInFile_SPHSC1(ByVal beCheckInFile_SPHSC1 As beCheckInFile_SPHSC1.Row, ByVal bePersonal As bePersonal.Row) As Boolean
        Dim bsCheckInFile_SPHSC1 As New beCheckInFile_SPHSC1.Service()
        Dim bsPersonal As New bePersonal.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsCheckInFile_SPHSC1.Update(beCheckInFile_SPHSC1, tran) = 0 Then Return False
                If bsPersonal.Update(bePersonal, tran) = 0 Then Return False
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

#Region "刪除新進員工文件繳交作業_證券"
    Public Function DeleteCheckInFile_SPHSC1(ByVal beCheckInFile_SPHSC1 As beCheckInFile_SPHSC1.Row) As Boolean
        Dim bsCheckInFile_SPHSC1 As New beCheckInFile_SPHSC1.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsCheckInFile_SPHSC1.DeleteRowByPrimaryKey(beCheckInFile_SPHSC1, tran) = 0 Then Return False
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

#Region "RG1500 不報到員工資料輸入"
#Region "查詢不報到員工資料"
    Public Function QueryRE_ContractData(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select C.CompID")
        strSQL.AppendLine(", IsNull(C1.CompName, '') As CompName")
        strSQL.AppendLine(", Convert(Varchar, C.CheckInDate, 111) As CheckInDate")
        strSQL.AppendLine(", C.RecID")
        strSQL.AppendLine(", R.NameN AS Name")
        strSQL.AppendLine(", Case C.CheckInFlag When 'N' Then '1' Else '0' End As CheckInFlag")
        strSQL.AppendLine(", ContractDate = Case When Convert(Char(10), C.ContractDate, 111) = '1900/01/01' Then '' Else Convert(Varchar, C.ContractDate, 111) End")
        strSQL.AppendLine(", C.NotCheckInRemark")
        strSQL.AppendLine(", C.ChkLastChgComp")
        strSQL.AppendLine(", IsNull(C2.CompName, '') As LastChgComp")
        strSQL.AppendLine(", C.ChkLastChgID")
        strSQL.AppendLine(", IsNull(U.NameN, '') As LastChgID")
        strSQL.AppendLine(", LastChgDate = Case When Convert(Char(10), C.ChkLastChgDate, 111) = '1900/01/01' Then '' Else Convert(Varchar, C.ChkLastChgDate, 120) End")
        strSQL.AppendLine("FROM RE_ContractData C")
        strSQL.AppendLine("INNER JOIN RE_Recruit AS R ON C.RecID = R.RecID")
        strSQL.AppendLine("LEFT JOIN eHRMSDB..Company AS C1 ON C.CompID = C1.CompID")
        strSQL.AppendLine("LEFT JOIN eHRMSDB..Company AS C2 ON C.ChkLastChgComp = C2.CompID")
        strSQL.AppendLine("LEFT JOIN eHRMSDB..Personal AS U ON C.ChkLastChgComp = U.CompID AND C.ChkLastChgID = U.EmpID")
        strSQL.AppendLine("WHERE 1 = 1")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        If ht(strKey).ToString() <> "0" Then
                            strSQL.AppendLine("And C.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        End If
                    Case "ContractDateB"
                        If ht("ContractDateE").ToString() <> "" Then
                            strSQL.AppendLine("And C.ContractDate >= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        Else
                            strSQL.AppendLine("And C.ContractDate = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        End If
                    Case "ContractDateE"
                        strSQL.AppendLine("And C.ContractDate <= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "RecID"
                        strSQL.AppendLine("And C.RecID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "CheckInDate"
                        strSQL.AppendLine("And C.CheckInDate = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next

        strSQL.AppendLine("Order by C.EmpID")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "Recruit").Tables(0)
    End Function
#End Region

#Region "修改不報到員工資料"
    Public Function UpdateRE_ContractData(ByVal ParamArray Params() As String) As Boolean
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("Recruit")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                strSQL.AppendLine("UPDATE RE_ContractData")
                strSQL.AppendLine("SET CheckInFlag = @CheckInFlag")
                strSQL.AppendLine(", NotCheckInRemark = @NotCheckInRemark")
                strSQL.AppendLine(", ChkLastChgComp = @ChkLastChgComp")
                strSQL.AppendLine(", ChkLastChgID = @ChkLastChgID")
                strSQL.AppendLine(", ChkLastChgDate = @ChkLastChgDate")
                strSQL.AppendLine("WHERE CompID = @KeyCompID")
                strSQL.AppendLine("AND RecID = @KeyRecID")
                strSQL.AppendLine("AND CheckInDate = @KeyCheckInDate")

                Dim DbParam() As DbParameter = { _
                    Bsp.DB.getDbParameter("@CheckInFlag", ht("CheckInFlag").ToString()), _
                    Bsp.DB.getDbParameter("@NotCheckInRemark", ht("NotCheckInRemark").ToString()), _
                    Bsp.DB.getDbParameter("@ChkLastChgComp", ht("ChkLastChgComp").ToString()), _
                    Bsp.DB.getDbParameter("@ChkLastChgID", ht("ChkLastChgID").ToString()), _
                    Bsp.DB.getDbParameter("@ChkLastChgDate", Now), _
                    Bsp.DB.getDbParameter("@KeyCompID", ht("CompID").ToString()), _
                    Bsp.DB.getDbParameter("@KeyRecID", ht("RecID").ToString()), _
                    Bsp.DB.getDbParameter("@KeyCheckInDate", ht("CheckInDate").ToString())}

                If Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), DbParam, tran, "Recruit") = 0 Then Return False
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

End Class
