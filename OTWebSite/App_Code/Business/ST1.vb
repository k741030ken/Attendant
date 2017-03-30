'****************************************************
'功能說明：員工基本資料設定
'建立人員：MickySung
'建立日期：2015.05.29
'****************************************************
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.IO
Imports System

Public Class ST1

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

#Region "ST1000 員工資料維護作業"
#Region "ST1000 員工資料維護作業-查詢"
    Public Function QueryPersonalSetting(ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT ")
        strSQL.AppendLine(" P.CompID, P.CompID + '-' + C.CompName AS CompName, P.EmpID, P.NameN ")
        strSQL.AppendLine(" , P.DeptID, P.DeptID + '-' + OD.OrganName AS DeptName, P.OrganID, P.OrganID + '-' + OG.OrganName AS OrganName ")
        strSQL.AppendLine(" , P.IDNo, P.EmpIDOld, P.WorkStatus, P.WorkStatus + '-' + W.Remark AS WorkStatusName ")
        strSQL.AppendLine(" , CL.CompName AS LastChgComp, PL.NameN AS LastChgID ")
        strSQL.AppendLine(" , P.LastChgDate, Convert(varchar(10),P.EmpDate,111) AS EmpDate ")
        strSQL.AppendLine(" FROM Personal P ")
        strSQL.AppendLine(" LEFT JOIN Company C ON P.CompID = C.CompID ")
        strSQL.AppendLine(" LEFT JOIN Organization OD ON P.CompID = OD.CompID AND P.DeptID = OD.OrganID ")
        strSQL.AppendLine(" LEFT JOIN Organization OG ON P.CompID = OG.CompID AND P.OrganID = OG.OrganID  ")
        strSQL.AppendLine(" LEFT JOIN WorkStatus W ON P.WorkStatus = W.WorkCode ")
        strSQL.AppendLine(" LEFT JOIN Company CL ON P.LastChgComp = CL.CompID ")
        strSQL.AppendLine(" LEFT JOIN Personal PL ON P.LastChgID = PL.EmpID AND P.LastChgComp = PL.CompID ")
        strSQL.AppendLine(" WHERE 1 = 1 ")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine(" AND P.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "EmpID"
                        strSQL.AppendLine(" AND P.EmpID LIKE '" & ht(strKey).ToString() + "%' ")
                    Case "EmpIDOld"
                        strSQL.AppendLine(" AND P.EmpIDOld LIKE '" & ht(strKey).ToString() + "%' ")
                    Case "EmpName"
                        strSQL.AppendLine(" AND (P.NameN LIKE N'%" & ht(strKey).ToString() + "%' ")
                        strSQL.AppendLine(" OR P.Name LIKE '%" & ht(strKey).ToString() + "%') ")
                    Case "IDNo"
                        strSQL.AppendLine(" AND P.IDNo LIKE '" & ht(strKey).ToString() + "%' ")
                    Case "OrganID"
                        strSQL.AppendLine(" AND P.OrganID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        strSQL.AppendLine(" AND P.WorkStatus = '1' And P.EmpType = '1'")
                End Select
            End If
        Next
        strSQL.AppendLine(" ORDER BY P.CompID, P.EmpID ")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "ST1000 自行訂義條件Table之資料數"
    Public Function IsDataExists(ByVal strTable As String, ByVal strWhere As String) As Boolean
        Dim strSQL As String
        strSQL = "Select Count(*) Cnt From " & strTable
        strSQL += " Where 1 = 1 " & strWhere
        Return IIf(Bsp.DB.ExecuteScalar(strSQL, "eHRMSDB") > 0, True, False)
    End Function
#End Region

#Region "ST1000 查詢單一欄位值"
    Public Function QueryData(ByVal strTable As String, ByVal strWhere As String, ByVal strcolum As String) As String
        Dim strSQL As String
        strSQL = "Select " & strcolum & " From " & strTable
        strSQL = strSQL & " Where 1 = 1 " & strWhere
        If Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB") = Nothing Then
            Return ""
        Else
            Return Bsp.DB.ExecuteScalar(strSQL, "eHRMSDB")
        End If
    End Function
#End Region
#End Region

#Region "ST1100 員工基本資料修改"

#Region "ST1100 員工基本資料修改subGetData"
    Public Function subGetData_ST1100(ByVal CompID As String, ByVal EmpID As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine("SELECT ISNULL(PO.RecID, '') RecID")
        strSQL.AppendLine(", ISNULL(PO.CheckInDate, '') CheckInDate ")
        strSQL.AppendLine(", P.CompID")
        strSQL.AppendLine(", P.EmpID")
        strSQL.AppendLine(", P.Name")
        strSQL.AppendLine(", P.NameN")
        strSQL.AppendLine(", P.NameB")
        strSQL.AppendLine(", P.IDNo")
        strSQL.AppendLine(", P.IDType")
        strSQL.AppendLine(", P.EngName")
        strSQL.AppendLine(", P.PassportName")
        strSQL.AppendLine(", CONVERT(Varchar(20), P.BirthDate, 111) as BirthDate")
        strSQL.AppendLine(", P.Sex")
        strSQL.AppendLine(", P.NationID")
        strSQL.AppendLine(", CONVERT(Varchar(20), P.IDExpireDate, 111) as IDExpireDate")
        strSQL.AppendLine(", P.EduID")
        strSQL.AppendLine(", P.Marriage")
        strSQL.AppendLine(", P.WorkStatus")
        strSQL.AppendLine(", P.EmpType")
        strSQL.AppendLine(", CONVERT(Varchar(20), P.SinopacEmpDate, 111) SinopacEmpDate")
        strSQL.AppendLine(", CONVERT(Varchar(20), P.SinopacQuitDate, 111) SinopacQuitDate")
        strSQL.AppendLine(", CONVERT(Varchar(20), P.EmpDate, 111) EmpDate")
        strSQL.AppendLine(", CONVERT(Varchar(20), P.QuitDate, 111) QuitDate")
        strSQL.AppendLine(", CONVERT(Varchar(20), P.ProbDate, 111) ProbDate")
        strSQL.AppendLine(", P.ProbMonth")
        strSQL.AppendLine(", P.RankID")
        strSQL.AppendLine(", P.TitleID")
        strSQL.AppendLine(", P.HoldingRankID")
        strSQL.AppendLine(", ISNULL(T.TitleName, '') HoldingTitle")
        strSQL.AppendLine(", P.PublicTitleID")
        strSQL.AppendLine(", CONVERT(Varchar(20), P.RankBeginDate, 111) RankBeginDate")
        strSQL.AppendLine(", P.GroupID")
        strSQL.AppendLine(", ISNULL(F.OrganName, '') AS GroupName ")
        strSQL.AppendLine(", ES.TotSen")
        strSQL.AppendLine(", ES.TotSen_SPHOLD")
        strSQL.AppendLine(", P.DeptID")
        strSQL.AppendLine(", P.OrganID")
        strSQL.AppendLine(", P.WorkSiteID")
        strSQL.AppendLine(", W.WTIDTypeFlag")
        strSQL.AppendLine(", ISNULL(PO.WTID, '') WTID")
        strSQL.AppendLine(", P.LocalHireFlag")
        strSQL.AppendLine(", P.PassExamFlag")
        strSQL.AppendLine(", ISNULL(PO.OfficeLoginFlag, '0') OfficeLoginFlag ")
        strSQL.AppendLine(", ISNULL(PO.AboriginalFlag, '0') AboriginalFlag ")
        strSQL.AppendLine(", ISNULL(PO.AboriginalTribe, '0') AboriginalTribe ")
        strSQL.AppendLine(", ISNULL(EF.OrganID, '') AS FlowOrganID")
        strSQL.AppendLine(", P.LastChgComp")
        strSQL.AppendLine(", P.LastChgID")
        strSQL.AppendLine(", CONVERT(Varchar(20), P.LastChgDate, 120) LastChgDate")
        strSQL.AppendLine("FROM Personal P ")
        strSQL.AppendLine("LEFT JOIN PersonalOther PO ON P.CompID = PO.CompID AND P.EmpID = PO.EmpID")
        strSQL.AppendLine("LEFT JOIN TitleByHolding T ON P.HoldingRankID = T.HoldingRankID")
        strSQL.AppendLine("LEFT JOIN OrganizationFlow F ON P.GroupID = F.OrganID")
        strSQL.AppendLine("LEFT JOIN EmpSenComp ES ON P.CompID = ES.CompID And P.EmpID = ES.EmpID")
        strSQL.AppendLine("LEFT JOIN EmpFlow EF ON P.CompID = EF.CompID AND P.EmpID = EF.EmpID AND EF.ActionID = '01'")
        strSQL.AppendLine("LEFT JOIN WorkTime W on PO.WTID = W.WTID AND PO.CompID = W.CompID")
        strSQL.AppendLine("WHERE P.CompID = " & Bsp.Utility.Quote(CompID.ToString()))
        strSQL.AppendLine("AND P.EmpID = " & Bsp.Utility.Quote(EmpID.ToString()))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "ST1100 員工最小簽核單位維護-新增"
    Public Function UpdatePersonalSetting(ByVal bePersonal As bePersonal.Row, ByVal hidIDNo As String, ByVal UserName As String, ByVal IsUpdPersonalOther As Boolean, ByVal bePersonalOther As bePersonalOther.Row, ByVal beEmpWorkType() As beEmpWorkType.Row, ByVal beEmpPosition() As beEmpPosition.Row, ByVal beEmpFlow As beEmpFlow.Row, ByVal beCommunication As beCommunication.Row, ByVal beEmpOtherIDNo() As beEmpOtherIDNo.Row) As Boolean
        Dim strSQL As New StringBuilder()

        Dim bsPersonal As New bePersonal.Service()
        Dim bsPersonalOther As New bePersonalOther.Service()
        Dim bsEmpWorkType As New beEmpWorkType.Service()
        Dim bsEmpPosition As New beEmpPosition.Service()
        Dim bsEmpFlow As New beEmpFlow.Service()
        Dim bsCommunication As New beCommunication.Service()
        Dim bsEmpOtherIDNo As New beEmpOtherIDNo.Service()  '2015/12/15 Add 新增Table

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                '1. 新增員工基本資料[Personal]員工檔
                bsPersonal.Update(bePersonal, tran)

                '2015/12/15 Add EmpOtherIDNo 員工次要證號檔
                If Not beEmpOtherIDNo Is Nothing Then
                    strSQL.Clear()
                    strSQL.AppendLine("DELETE EmpOtherIDNo WHERE IDNo = " & Bsp.Utility.Quote(bePersonal.IDNo.Value))
                    Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)

                    bsEmpOtherIDNo.Insert(beEmpOtherIDNo, tran)
                End If

                '2. 如果有異動身分證IDNo(IDNo <> IDNo_Old)，需同步修改跟IDNo相關的所有Table的IDNo欄位值
                If bePersonal.IDNo.Value <> hidIDNo Then
                    Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
                    Dim dbCommand_Education As DbCommand = db.GetStoredProcCommand("SP_ChgIDNo")
                    db.AddInParameter(dbCommand_Education, "@InEmpFlag", DbType.String, "1")
                    db.AddInParameter(dbCommand_Education, "@InFormID", DbType.String, "ST1100")
                    db.AddInParameter(dbCommand_Education, "@InCompID", DbType.String, bePersonal.CompID.Value)
                    db.AddInParameter(dbCommand_Education, "@InEmpID", DbType.String, bePersonal.EmpID.Value)
                    db.AddInParameter(dbCommand_Education, "@InNewIDNo", DbType.String, bePersonal.IDNo.Value)
                    db.AddInParameter(dbCommand_Education, "@InOldIDNo", DbType.String, hidIDNo)
                    db.AddInParameter(dbCommand_Education, "@InLoginCompID", DbType.String, bePersonal.LastChgComp.Value)
                    db.AddInParameter(dbCommand_Education, "@InLoginUserID", DbType.String, bePersonal.LastChgID.Value)
                    db.AddInParameter(dbCommand_Education, "@InLoginUserName", DbType.String, UserName)
                    db.ExecuteNonQuery(dbCommand_Education, tran)
                End If

                '3. 新增/修改員工特殊設定資料檔[PersonalOther]
                If IsUpdPersonalOther Then
                    If bsPersonalOther.IsDataExists(bePersonalOther) Then
                        bsPersonalOther.Update(bePersonalOther, tran)
                    Else
                        bsPersonalOther.Insert(bePersonalOther, tran)
                    End If
                End If

                '4. 新增員工工作性質檔[EmpWorkType]
                If Not beEmpWorkType Is Nothing Then
                    strSQL.Clear()
                    strSQL.AppendLine("DELETE EmpWorkType WHERE CompID = " & Bsp.Utility.Quote(bePersonal.CompID.Value) & " AND EmpID = " & Bsp.Utility.Quote(bePersonal.EmpID.Value))
                    Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)

                    bsEmpWorkType.Insert(beEmpWorkType, tran)
                End If

                '5. 新增員工職位檔[EmpPosition]
                If Not beEmpPosition Is Nothing Then
                    strSQL.Clear()
                    strSQL.AppendLine("DELETE EmpPosition WHERE CompID = " & Bsp.Utility.Quote(bePersonal.CompID.Value) & " AND EmpID = " & Bsp.Utility.Quote(bePersonal.EmpID.Value))
                    Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)

                    bsEmpPosition.Insert(beEmpPosition, tran)
                End If

                '6. 新增/修改員工最小簽核單位檔[EmpFlow]
                If beEmpFlow.CompID.Value <> "" And beEmpFlow.EmpID.Value <> "" And beEmpFlow.ActionID.Value <> "" Then
                    If bsEmpFlow.IsDataExists(beEmpFlow) Then
                        bsEmpFlow.Update(beEmpFlow, tran)
                    Else
                        bsEmpFlow.Insert(beEmpFlow, tran)
                    End If
                End If

                '12. 新增通訊資料檔[寫入Communication]
                If beCommunication.IDNo.Value <> "" Then
                    If bsCommunication.IsDataExists(beCommunication, tran) Then
                        bsCommunication.Update(beCommunication, tran)
                    Else
                        bsCommunication.Insert(beCommunication, tran)
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

#Region "ST1100 檢查欲異動的身分證字號,居留證字號是否已經存在"
    Public Function funCheckIDNo(ByVal NewIDNo As String, ByVal OldIDNo As String) As String
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" SELECT Cnt1 + Cnt2 AS Cnt FROM ")
        strSQL.AppendLine(" (SELECT Count(*) AS Cnt1 FROM Personal WHERE IDNo = " & Bsp.Utility.Quote(NewIDNo) & ") T1, ")
        strSQL.AppendLine(" (SELECT Count(*) AS Cnt2 FROM Family WHERE IDNo = " & Bsp.Utility.Quote(OldIDNo) & " AND RelativeIDNo = " & Bsp.Utility.Quote(NewIDNo) & ") T2 ")
        Return IIf(Bsp.DB.ExecuteScalar(strSQL.ToString, "eHRMSDB") > 0, True, False)
    End Function
#End Region


#Region "ST1100 查詢使用者按鈕權限"
    Public Function funCheckRight() As Boolean
        Dim strSQL As New StringBuilder()
        Dim CheckRight As Object = Bsp.DB.ExecuteScalar("Select CheckRight From SC_Fun Where FunID = 'ST1100'")

        If String.IsNullOrEmpty(CheckRight) Then
            Return False
        Else
            If CheckRight.ToString = "0" Then
                Return True
            Else
                strSQL.AppendLine("Select distinct c.RightID")
                strSQL.AppendLine("From SC_UserGroup a")
                strSQL.AppendLine("inner join SC_GroupFun b on a.GroupID = b.GroupID and a.SysID = b.SysID and a.CompRoleID = b.CompRoleID")
                strSQL.AppendLine("inner join SC_FunRight c on b.FunID = c.FunID And b.RightID = c.RightID and b.SysID = c.SysID")
                strSQL.AppendLine("inner join SC_Right d on c.RightID = d.RightID")
                strSQL.AppendLine("Where a.UserID = " & Bsp.Utility.Quote(UserProfile.UserID))
                strSQL.AppendLine("and b.FunID = 'ST1100'")
                strSQL.AppendLine("and a.SysID = " & Bsp.Utility.Quote(UserProfile.LoginSysID))
                strSQL.AppendLine("and a.CompRoleID = " & Bsp.Utility.Quote(UserProfile.SelectCompRoleID))
                If UserProfile.SelectCompRoleID = "ALL" Then
                    strSQL.AppendLine("and d.IsCompAll = '1'")
                End If

                Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
                    If dt.Rows.Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            If dr.Item(0).ToString = "U" Then
                                Return True
                            End If
                        Next
                    Else
                        Return False
                    End If
                End Using
            End If
        End If
    End Function
#End Region

#End Region

#Region "ST1200 員工最小簽核單位維護"

#Region "ST1200 員工最小簽核單位維護-查詢"
    Public Function QueryEmpFlowSetting(ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT ")
        strSQL.AppendLine(" E.CompID, E.EmpID, E.ActionID, isnull(d.CodeCName,'') AS ActionName ")
        strSQL.AppendLine(" , isnull(d.Code,'') + ' ' + isnull(d.CodeCName,'') AS ActionCN, E.OrganID + ' ' + isnull(b.OrganName,'') AS OrganCN ")
        strSQL.AppendLine(" , isnull(e.Code,'') + ' ' + isnull(e.CodeCName,'') AS GroupTypeCN, E.GroupID + ' ' + isnull(c.OrganName,'') AS GroupIDCN ")
        strSQL.AppendLine(" , CL.CompName AS LastChgComp, PL.NameN AS LastChgID ")
        strSQL.AppendLine(" , LastChgDate = Case When Convert(Char(10), E.LastChgDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, E.LastChgDate, 120) END ")
        strSQL.AppendLine(" FROM EmpFlow E ")
        strSQL.AppendLine(" LEFT JOIN OrganizationFlow b ON E.OrganID = b.OrganID ")
        strSQL.AppendLine(" LEFT JOIN OrganizationFlow c ON E.GroupID = c.OrganID ")
        strSQL.AppendLine(" LEFT JOIN HRCodeMap d ON d.TabName = 'EmpFlow' AND d.FldName = 'ActionID' AND d.Code = E.ActionID and d.NotShowFlag = '0' ")
        strSQL.AppendLine(" LEFT JOIN HRCodeMap e ON e.TabName = 'Organization' AND e.FldName = 'GroupType' AND e.Code = E.GroupType and e.NotShowFlag = '0' ")
        strSQL.AppendLine(" LEFT JOIN Company CL ON E.LastChgComp = CL.CompID ")
        strSQL.AppendLine(" LEFT JOIN Personal PL ON E.LastChgID = PL.EmpID AND E.LastChgComp = PL.CompID ")
        strSQL.AppendLine(" WHERE 1 = 1 ")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine(" AND E.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "EmpID"
                        strSQL.AppendLine(" AND E.EmpID LIKE '" & ht(strKey).ToString() + "%' ")
                End Select
            End If
        Next
        strSQL.AppendLine(" ORDER BY E.ActionID ")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "ST1201 員工最小簽核單位維護-新增"
    Public Function AddEmpFlowSetting(ByVal beEmpFlow As beEmpFlow.Row) As Boolean
        Dim bsEmpFlow As New beEmpFlow.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsEmpFlow.Insert(beEmpFlow, tran) = 0 Then Return False
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

#Region "ST1202 員工最小簽核單位維護-修改"
    Public Function UpdateEmpFlowSetting(ByVal beEmpFlow As beEmpFlow.Row) As Boolean
        Dim bsEmpFlow As New beEmpFlow.Service()
        Dim strSQL_Personal As New StringBuilder()
        Dim strFamily As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                If bsEmpFlow.Update(beEmpFlow, tran) = 0 Then Return False
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

#Region "ST1200 員工最小簽核單位維護-刪除"
    Public Function DeleteEmpFlowSetting(ByVal beEmpFlow As beEmpFlow.Row) As Boolean
        Dim bsEmpFlow As New beEmpFlow.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As Data.Common.DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                bsEmpFlow.DeleteRowByPrimaryKey(beEmpFlow, tran)

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

#Region "ST1200 查詢OrganID,EmpDate"
    Public Function QueryEmpData(ByVal CompID As String, ByVal EmpID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" Select OrganID, EmpDate From Personal ")
        strSQL.AppendLine(" Where CompID = " + Bsp.Utility.Quote(CompID) + " AND EmpID = " + Bsp.Utility.Quote(EmpID))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#End Region

#Region "ST1300 員工學歷資料維護"

#Region "ST1300 員工學歷資料維護-查詢"
    Public Function QueryEducationSetting(ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT ")
        strSQL.AppendLine(" case when E.GraduateYear <> 0 then E.GraduateYear end AS GraduateYear ")
        strSQL.AppendLine(" , isnull(S.Remark, E.School) AS School, isnull(R1.Remark, E.Depart) AS Depart ")
        strSQL.AppendLine(" , isnull(R2.Remark, E.SecDepart) AS SecDepart, isnull(H.CodeCName, '') AS SchoolTypeName ")
        strSQL.AppendLine(" , P.NameN, (SELECT EduName FROM EduDegree WHERE EduID = P.EduID) AS TopName ")
        strSQL.AppendLine(" , P.IDNo, P.NameN, E.Seq, E.EduID, E.EduID + '-' + D.EduName AS EduName ")
        strSQL.AppendLine(" , case E.EduStatus when '1' then '1-畢業' when '2' then '2-就學中' when '9' then '9-肆業' else '' end as EduStatus ")
        strSQL.AppendLine(" , CL.CompName AS LastChgComp, PL.NameN AS LastChgID ")
        strSQL.AppendLine(" , LastChgDate = Case When Convert(Char(10), E.LastChgDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, E.LastChgDate, 120) End ")
        strSQL.AppendLine(" FROM Education E ")
        strSQL.AppendLine(" LEFT JOIN EduDegree D ON D.EduID = E.EduID ")
        strSQL.AppendLine(" LEFT JOIN School S ON S.SchoolID = E.SchoolID ")
        strSQL.AppendLine(" LEFT JOIN Depart R1 ON R1.DepartID = E.DepartID ")
        strSQL.AppendLine(" LEFT JOIN Depart R2 ON R2.DepartID = E.SecDepartID ")
        strSQL.AppendLine(" LEFT JOIN HRCodeMap H ON H.TabName = 'Education' AND H.FldName = 'SchoolType' AND H.Code = E.SchoolType AND H.NotShowFlag = '0' ")
        strSQL.AppendLine(" JOIN Personal P ON E.IDNo = P.IDNo ")
        strSQL.AppendLine(" LEFT JOIN Company CL ON E.LastChgComp = CL.CompID ")
        strSQL.AppendLine(" LEFT JOIN Personal PL ON E.LastChgID = PL.EmpID AND E.LastChgComp = PL.CompID ")
        strSQL.AppendLine(" WHERE 1 = 1 ")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine(" AND P.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "EmpID"
                        strSQL.AppendLine(" AND P.EmpID LIKE '" & ht(strKey).ToString() + "%' ")
                End Select
            End If
        Next
        strSQL.AppendLine(" ORDER BY E.EduID, Seq ")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "ST1301 員工學歷資料維護-新增"
    Public Function AddEducationSetting(ByVal beEducation As beEducation.Row, ByVal CompID As String, ByVal EmpID As String) As Boolean
        Dim bsEducation As New beEducation.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsEducation.Insert(beEducation, tran) = 0 Then
                    Return False
                Else
                    '同步更新Personal
                    strSQL.AppendLine(" UPDATE Personal SET ")
                    strSQL.AppendLine(" EduID = ISNULL(E.EduID, '010') ")
                    strSQL.AppendLine(" , LastChgComp = " & Bsp.Utility.Quote(beEducation.LastChgComp.Value))
                    strSQL.AppendLine(" , LastChgID = " & Bsp.Utility.Quote(beEducation.LastChgID.Value))
                    strSQL.AppendLine(" , LastChgDate = GETDATE() ")
                    strSQL.AppendLine(" FROM Personal P ")
                    strSQL.AppendLine(" LEFT JOIN (SELECT TOP 1 EduID, IDNo FROM Education WHERE IDNo = " & Bsp.Utility.Quote(beEducation.IDNo.Value) & " AND EduID <> '080' ORDER BY EduID DESC) E ON P.IDNo = E.IDNo ")
                    strSQL.AppendLine(" WHERE P.EmpID = " & Bsp.Utility.Quote(EmpID) & "AND P.CompID = " & Bsp.Utility.Quote(CompID))

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

#Region "ST1302 員工學歷資料維護-修改"
    Public Function UpdateEducationSetting(ByVal beEducation As beEducation.Row, ByVal CompID As String, ByVal EmpID As String) As Boolean
        Dim bsEducation As New beEducation.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                strSQL.AppendLine("UPDATE Education")
                strSQL.AppendLine("SET EduID = @EduID")
                strSQL.AppendLine(", GraduateYear = @GraduateYear")
                strSQL.AppendLine(", SchoolType = @SchoolType")
                strSQL.AppendLine(", SchoolID = @SchoolID")
                strSQL.AppendLine(", School = @School")
                strSQL.AppendLine(", DepartID = @DepartID")
                strSQL.AppendLine(", Depart = @Depart")
                strSQL.AppendLine(", SecDepartID = @SecDepartID")
                strSQL.AppendLine(", SecDepart = @SecDepart")
                strSQL.AppendLine(", EduStatus = @EduStatus")
                strSQL.AppendLine(", LastChgComp = @LastChgComp")
                strSQL.AppendLine(", LastChgID = @LastChgID")
                strSQL.AppendLine(", LastChgDate = @LastChgDate")
                strSQL.AppendLine("WHERE IDNo = @KeyIDNo")
                strSQL.AppendLine("AND EduID = @KeyEduID")
                strSQL.AppendLine("AND Seq = @KeySeq")

                Dim DbParam() As DbParameter = { _
                    Bsp.DB.getDbParameter("@EduID", beEducation.EduID.Value), _
                    Bsp.DB.getDbParameter("@GraduateYear", beEducation.GraduateYear.Value), _
                    Bsp.DB.getDbParameter("@SchoolType", beEducation.SchoolType.Value), _
                    Bsp.DB.getDbParameter("@SchoolID", beEducation.SchoolID.Value), _
                    Bsp.DB.getDbParameter("@School", beEducation.School.Value), _
                    Bsp.DB.getDbParameter("@DepartID", beEducation.DepartID.Value), _
                    Bsp.DB.getDbParameter("@Depart", beEducation.Depart.Value), _
                    Bsp.DB.getDbParameter("@SecDepartID", beEducation.SecDepartID.Value), _
                    Bsp.DB.getDbParameter("@SecDepart", beEducation.SecDepart.Value), _
                    Bsp.DB.getDbParameter("@EduStatus", beEducation.EduStatus.Value), _
                    Bsp.DB.getDbParameter("@LastChgComp", beEducation.LastChgComp.Value), _
                    Bsp.DB.getDbParameter("@LastChgID", beEducation.LastChgID.Value), _
                    Bsp.DB.getDbParameter("@LastChgDate", Now), _
                    Bsp.DB.getDbParameter("@KeyIDNo", beEducation.IDNo.Value), _
                    Bsp.DB.getDbParameter("@KeyEduID", beEducation.EduID.OldValue), _
                    Bsp.DB.getDbParameter("@KeySeq", beEducation.Seq.Value)}

                If Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), DbParam, tran, "eHRMSDB") = 0 Then
                    Return False
                Else
                    '同步更新Personal
                    strSQL.Clear()
                    strSQL.AppendLine(" UPDATE Personal SET ")
                    strSQL.AppendLine(" EduID = ISNULL(E.EduID, '010') ")
                    strSQL.AppendLine(" , LastChgComp = " & Bsp.Utility.Quote(beEducation.LastChgComp.Value))
                    strSQL.AppendLine(" , LastChgID = " & Bsp.Utility.Quote(beEducation.LastChgID.Value))
                    strSQL.AppendLine(" , LastChgDate = GETDATE() ")
                    strSQL.AppendLine(" FROM Personal P ")
                    strSQL.AppendLine(" LEFT JOIN (SELECT TOP 1 EduID, IDNo FROM Education WHERE IDNo = " & Bsp.Utility.Quote(beEducation.IDNo.Value) & " AND EduID <> '080' ORDER BY EduID DESC) E ON P.IDNo = E.IDNo ")
                    strSQL.AppendLine(" WHERE P.EmpID = " & Bsp.Utility.Quote(EmpID) & "AND P.CompID = " & Bsp.Utility.Quote(CompID))

                    Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)
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

#Region "ST1300 員工學歷資料維護-刪除"
    Public Function DeleteEducationSetting(ByVal beEducation As beEducation.Row, ByVal CompID As String, ByVal EmpID As String) As Boolean
        Dim bsEducation As New beEducation.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As Data.Common.DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                bsEducation.DeleteRowByPrimaryKey(beEducation, tran)

                '同步更新Personal
                strSQL.AppendLine(" UPDATE Personal SET ")
                strSQL.AppendLine(" EduID = ISNULL(E.EduID, '010') ")
                strSQL.AppendLine(" , LastChgComp = " & Bsp.Utility.Quote(beEducation.LastChgComp.Value))
                strSQL.AppendLine(" , LastChgID = " & Bsp.Utility.Quote(beEducation.LastChgID.Value))
                strSQL.AppendLine(" , LastChgDate = GETDATE() ")
                strSQL.AppendLine(" FROM Personal P ")
                strSQL.AppendLine(" LEFT JOIN (SELECT TOP 1 EduID, IDNo FROM Education WHERE IDNo = " & Bsp.Utility.Quote(beEducation.IDNo.Value) & " AND EduID <> '080' ORDER BY EduID DESC) E ON P.IDNo = E.IDNo ")
                strSQL.AppendLine(" WHERE P.EmpID = " & Bsp.Utility.Quote(EmpID) & "AND P.CompID = " & Bsp.Utility.Quote(CompID))

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

#Region "ST1300 員工學歷資料維護-取得最高學歷"
    Public Function QueryTopName(ByVal CompID As String, ByVal EmpID As String) As String
        Dim bsEducation As New beEducation.Service()
        Dim strSQL As New StringBuilder()


        strSQL.AppendLine(" Select ISNULL(E.EduName,'') ")
        strSQL.AppendLine(" From EduDegree E ")
        strSQL.AppendLine(" Left join Personal P ")
        strSQL.AppendLine(" On E.EduID = P.EduID ")
        strSQL.AppendLine(" Where 1 = 1 ")
        strSQL.AppendLine(" And P.CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine(" And P.EmpID = " & Bsp.Utility.Quote(EmpID))


        Return Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")
    End Function
#End Region

#Region "ST1301 查詢MaxSeq"
    Public Function QueryEducationMaxSeq(ByVal IDNo As String) As Integer
        Dim bsEducation As New beEducation.Service()
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" Select ISNULL(Max(Seq),0)+1 ")
        strSQL.AppendLine(" From Education ")
        strSQL.AppendLine(" Where IDNo = " & Bsp.Utility.Quote(IDNo))

        Return CInt(Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")) 'ExecuteScalar回傳Object

    End Function
#End Region

#End Region

#Region "ST1400 員工家庭狀況維護"

#Region "ST1400 員工家庭狀況維護-查詢"
    Public Function QueryFamilySetting(ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT ")
        strSQL.AppendLine(" F.RelativeID, F.RelativeID + '-' + R.Remark AS RelativeName, F.NameN, F.Name, F.RelativeIDNo ")
        strSQL.AppendLine(" , BirthDate = Case When Convert(Char(10), F.BirthDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, F.BirthDate, 111) END ")   '2015/12/23 Modify 生日只顯示日期
        strSQL.AppendLine(" , F.Occupation, F.Company, F.HeaStatus, F.GrpStatus, F.IsBSPEmp ")
        strSQL.AppendLine(" , F.IndustryType, isnull(H.CodeCName,'') AS IndustryTypeName ") '2015/12/29 Modify IndustryTypeName只顯示名稱就好
        strSQL.AppendLine(" , CASE F.DeleteMark WHEN '0' THEN '0-正常' WHEN '1' THEN '1-死亡' WHEN '2' THEN '2-離婚' WHEN '9' THEN '9-其他' END AS DeleteMark ")
        strSQL.AppendLine(" , P.IDNo, P.NameN as PNameN ")
        strSQL.AppendLine(" , CL.CompName AS LastChgComp, PL.NameN AS LastChgID ")
        strSQL.AppendLine(" , LastChgDate = Case When Convert(Char(10), F.LastChgDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, F.LastChgDate, 120) END ")
        strSQL.AppendLine(" FROM Family F ")
        strSQL.AppendLine(" INNER JOIN Relationship R  on F.RelativeID = R.RelativeID ")
        strSQL.AppendLine(" LEFT JOIN HRCodeMap H on H.TabName = 'Experience' AND H.FldName = 'IndustryType' AND H.Code = F.IndustryType AND H.NotShowFlag = '0' ")
        strSQL.AppendLine(" JOIN Personal P ON F.IDNo = P.IDNo ")
        strSQL.AppendLine(" LEFT JOIN Company CL ON F.LastChgComp = CL.CompID ")
        strSQL.AppendLine(" LEFT JOIN Personal PL ON F.LastChgID = PL.EmpID AND F.LastChgComp = PL.CompID ")
        strSQL.AppendLine(" WHERE 1 = 1 ")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine(" AND P.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "EmpID"
                        strSQL.AppendLine(" AND P.EmpID LIKE '" & ht(strKey).ToString() + "%' ")
                End Select
            End If
        Next
        'strSQL.AppendLine(" AND DeleteMark = '0' ")
        strSQL.AppendLine(" ORDER BY F.BirthDate ")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "ST1401 員工家庭狀況維護-新增"
    Public Function AddFamilySetting(ByVal beFamily As beFamily.Row, ByVal CompID As String, ByVal EmpID As String) As Boolean
        Dim bsFamily As New beFamily.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsFamily.Insert(beFamily, tran) = 0 Then
                    Return False
                Else
                    If beFamily.RelativeID.Value = "01" And beFamily.DeleteMark.Value = "0" Then
                        '同步更新Personal
                        strSQL.AppendLine(" Update Personal Set Marriage = 2 ")
                        strSQL.AppendLine(" , LastChgComp = " & Bsp.Utility.Quote(beFamily.LastChgComp.Value))
                        strSQL.AppendLine(" , LastChgID = " & Bsp.Utility.Quote(beFamily.LastChgID.Value))
                        strSQL.AppendLine(" , LastChgDate = GETDATE() ")
                        strSQL.AppendLine(" FROM Personal P ")
                        strSQL.AppendLine(" WHERE P.EmpID = " & Bsp.Utility.Quote(EmpID) & " AND P.CompID = " & Bsp.Utility.Quote(CompID))

                        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)
                    End If

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

#Region "ST1302 員工家庭狀況維護-修改"
    Public Function UpdateFamilySetting(ByVal beFamily As beFamily.Row, ByVal CompID As String, ByVal EmpID As String, ByVal hidRelativeIDNo As String, ByVal LastChgUserName As String, ByVal isMarriage As String, ByVal isIDNoChange As Boolean, ByVal isFlagChange As Boolean) As Boolean
        Dim bsFamily As New beFamily.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                '修改Family'
                strSQL.AppendLine(" Update Family Set RelativeID = " & Bsp.Utility.Quote(beFamily.RelativeID.Value))
                strSQL.AppendLine(" , Name = " & Bsp.Utility.Quote(beFamily.Name.Value))
                strSQL.AppendLine(" , NameN = N" & Bsp.Utility.Quote(beFamily.NameN.Value))
                strSQL.AppendLine(" , RelativeIDNo = " & Bsp.Utility.Quote(beFamily.RelativeIDNo.Value))
                strSQL.AppendLine(" , BirthDate = " & Bsp.Utility.Quote(beFamily.BirthDate.Value))
                strSQL.AppendLine(" , Occupation = " & Bsp.Utility.Quote(beFamily.Occupation.Value))
                strSQL.AppendLine(" , IndustryType = " & Bsp.Utility.Quote(beFamily.IndustryType.Value))
                strSQL.AppendLine(" , Company = " & Bsp.Utility.Quote(beFamily.Company.Value))
                strSQL.AppendLine(" , IsBSPEmp = " & Bsp.Utility.Quote(beFamily.IsBSPEmp.Value))
                strSQL.AppendLine(" , DeleteMark = " & Bsp.Utility.Quote(beFamily.DeleteMark.Value))
                strSQL.AppendLine(" , LastChgComp = " & Bsp.Utility.Quote(beFamily.LastChgComp.Value))
                strSQL.AppendLine(" , LastChgID = " & Bsp.Utility.Quote(beFamily.LastChgID.Value))
                strSQL.AppendLine(" , LastChgDate = GETDATE() ")
                strSQL.AppendLine(" WHERE IDNo = " & Bsp.Utility.Quote(beFamily.IDNo.Value) & " AND RelativeIDNo = " & Bsp.Utility.Quote(hidRelativeIDNo))
                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)

                '修改Personal的員工婚姻狀態
                If isMarriage <> "" Then
                    strSQL = New StringBuilder()
                    strSQL.AppendLine(" Update Personal Set Marriage = '" + isMarriage + "' ")
                    strSQL.AppendLine(" , LastChgComp = " & Bsp.Utility.Quote(beFamily.LastChgComp.Value))
                    strSQL.AppendLine(" , LastChgID = " & Bsp.Utility.Quote(beFamily.LastChgID.Value))
                    strSQL.AppendLine(" , LastChgDate = GETDATE() ")
                    strSQL.AppendLine(" FROM Personal P ")
                    strSQL.AppendLine(" WHERE P.EmpID = " & Bsp.Utility.Quote(EmpID) & " AND P.CompID = " & Bsp.Utility.Quote(CompID))
                    Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)
                End If

                '親屬身份證RelativeIDNo有異動時,同步修改跟親屬相關的檔案
                If isIDNoChange Then
                    Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
                    Dim dbCommand As DbCommand = db.GetStoredProcCommand("SP_ChgIDNo")

                    db.AddInParameter(dbCommand, "@InEmpFlag", DbType.String, "0")
                    db.AddInParameter(dbCommand, "@InFormID", DbType.String, "ST1400")
                    db.AddInParameter(dbCommand, "@InCompID", DbType.String, CompID)
                    db.AddInParameter(dbCommand, "@InEmpID", DbType.String, EmpID)
                    db.AddInParameter(dbCommand, "@InNewIDNo", DbType.String, beFamily.RelativeIDNo.Value.ToString)
                    db.AddInParameter(dbCommand, "@InOldIDNo", DbType.String, hidRelativeIDNo)
                    db.AddInParameter(dbCommand, "@InLoginCompID", DbType.String, beFamily.LastChgComp.Value.ToString)
                    db.AddInParameter(dbCommand, "@InLoginUserID", DbType.String, beFamily.LastChgID.Value.ToString)
                    db.AddInParameter(dbCommand, "@InLoginUserName", DbType.String, LastChgUserName)
                    db.ExecuteNonQuery(dbCommand, tran)
                End If

                '如果DeleteMark從0正常改為1死亡/2離婚/9其他，需新增PersonalLog基本資料異動記錄檔
                If isFlagChange Then
                    strSQL = New StringBuilder()
                    strSQL.AppendLine(" Insert into PersonalLog (")
                    strSQL.AppendLine(" CompID, EmpID, RelativeIDNo, Name, BirthDate, RelativeID, ModifyDate, Reason, OldData, NewData, EffDate)")
                    strSQL.AppendLine(" Values (" & Bsp.Utility.Quote(CompID) & ", " & Bsp.Utility.Quote(EmpID) & ", " & Bsp.Utility.Quote(beFamily.RelativeIDNo.Value) & ", N" & Bsp.Utility.Quote(beFamily.NameN.Value))
                    strSQL.AppendLine(" , " & Bsp.Utility.Quote(beFamily.BirthDate.Value) & ", " & Bsp.Utility.Quote(beFamily.RelativeID.Value) & ", GETDATE(), '31', '0', " & Bsp.Utility.Quote(beFamily.DeleteMark.Value) & ", " & Bsp.Utility.Quote(Now.Date.ToString("yyyy-MM-dd")))
                    strSQL.AppendLine(" )")
                    Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)
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

#Region "ST1400 員工家庭狀況維護-刪除"
    Public Function DeleteFamilySetting(ByVal beFamily As beFamily.Row, ByVal CompID As String, ByVal EmpID As String) As Boolean
        Dim bsFamily As New beFamily.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As Data.Common.DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                bsFamily.DeleteRowByPrimaryKey(beFamily, tran)

                '如果刪除的稱謂為1則連動修改Personal的員工婚姻狀態
                If beFamily.RelativeID.Value = "01" Then
                    strSQL.AppendLine(" Update Personal Set Marriage = '1' ")
                    strSQL.AppendLine(" , LastChgComp = " & Bsp.Utility.Quote(beFamily.LastChgComp.Value))
                    strSQL.AppendLine(" , LastChgID = " & Bsp.Utility.Quote(beFamily.LastChgID.Value))
                    strSQL.AppendLine(" , LastChgDate = GETDATE() ")
                    strSQL.AppendLine(" FROM Personal P ")
                    strSQL.AppendLine(" WHERE P.EmpID = " & Bsp.Utility.Quote(EmpID) & " AND P.CompID = " & Bsp.Utility.Quote(CompID))
                    Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)
                End If

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

#Region "ST1400 眷屬身分證是否重複檢核"
    Public Function checkRelativeIDNo(ByVal CompID As String, ByVal EmpID As String, ByVal IDNo As String, ByVal RelativeIDNo As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT Cnt1 + Cnt2 as Cnt from")
        strSQL.AppendLine(" (select Count(*) as Cnt1 from Personal where CompID = " & Bsp.Utility.Quote(CompID.ToString()) & "and  EmpID=" & Bsp.Utility.Quote(EmpID.ToString()) & " and IDNo = " & Bsp.Utility.Quote(RelativeIDNo.ToString()) & " and WorkStatus = '1' ) T1, ")
        strSQL.AppendLine(" (select count(*) as Cnt2 from Family where IDNo = " & Bsp.Utility.Quote(IDNo.ToString()) & " and RelativeIDNo = " & Bsp.Utility.Quote(RelativeIDNo.ToString()) & " ) T2 ")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "ST1402 是否有保險資料檢核"
    Public Function checkStatus(ByVal CompID As String, ByVal EmpID As String, ByVal IDNo As String, ByVal hidRelativeIDNo As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" select Cnt1+Cnt2+Cnt3 as Cnt from ")
        strSQL.AppendLine(" ( select count(*) as Cnt1 from Family where IDNo = " & Bsp.Utility.Quote(IDNo.ToString()) & " and RelativeIDNo = " & Bsp.Utility.Quote(hidRelativeIDNo.ToString()) & " and (HeaStatus = '1' or GrpStatus = '1')) T1, ")
        strSQL.AppendLine(" ( select count(*) as Cnt2 from InsureWait where CompID = " & Bsp.Utility.Quote(CompID.ToString()) & " and EmpID = " & Bsp.Utility.Quote(EmpID.ToString()) & " and RelativeIDNo = " & Bsp.Utility.Quote(hidRelativeIDNo.ToString()) & ") T2, ")
        strSQL.AppendLine(" ( select count(*) as Cnt3 from GroupWait where CompID = " & Bsp.Utility.Quote(CompID.ToString()) & " and  EmpID = " & Bsp.Utility.Quote(EmpID.ToString()) & " and RelativeIDNo = " & Bsp.Utility.Quote(hidRelativeIDNo.ToString()) & ") T3 ")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "ST1400 判斷 InsureWait保險待加退保檔 是否有資料"
    Public Function CountInsureWait(ByVal CompID As String, ByVal EmpID As String, ByVal RelativeIDNo As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" Select Count(*) As TotRec From InsureWait where WaitType = '1' ")
        strSQL.AppendLine(" and CompID = " & Bsp.Utility.Quote(CompID.ToString()))
        strSQL.AppendLine(" and EmpID = " & Bsp.Utility.Quote(EmpID.ToString()))
        strSQL.AppendLine(" and RelativeIDNo = " & Bsp.Utility.Quote(RelativeIDNo.ToString()))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "ST1400 判斷 GroupWait團保待加退保檔 是否有資料"
    Public Function CountGroupWait(ByVal CompID As String, ByVal EmpID As String, ByVal RelativeIDNo As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" Select Count(*) As TotRec From GroupWait where WaitType = '1' ")
        strSQL.AppendLine(" and CompID = " & Bsp.Utility.Quote(CompID.ToString()))
        strSQL.AppendLine(" and EmpID = " & Bsp.Utility.Quote(EmpID.ToString()))
        strSQL.AppendLine(" and RelativeIDNo = " & Bsp.Utility.Quote(RelativeIDNo.ToString()))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "ST1400 判斷 GroupDown團保下傳檔 是否有資料"
    Public Function CountGroupDown(ByVal CompID As String, ByVal EmpID As String, ByVal RelativeIDNo As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" Select Count(*) As TotRec From GroupDown where Confirm = '0' ")
        strSQL.AppendLine(" and CompID = " & Bsp.Utility.Quote(CompID.ToString()))
        strSQL.AppendLine(" and EmpID = " & Bsp.Utility.Quote(EmpID.ToString()))
        strSQL.AppendLine(" and IDNo = " & Bsp.Utility.Quote(RelativeIDNo.ToString()))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#End Region

#Region "ST1500 員工前職經歷維護"

#Region "ST1500 員工前職經歷維護-查詢"
    Public Function QueryExperienceSetting(ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT ")
        strSQL.AppendLine(" E.IDNo, E.Seq, Convert(Char(10), E.BeginDate, 111) AS BeginDate, Convert(Char(10), E.EndDate, 111) AS EndDate, E.Company, E.Department, E.Title, E.WorkType, E.WorkYear, E.Profession ")    '2015/12/01 Modify 起迄日只顯示年月日
        strSQL.AppendLine(" , E.IndustryType, E.IndustryType + isnull('-' + H.CodeCName, '') AS IndustryTypeName ")
        strSQL.AppendLine(" , CL.CompName AS LastChgComp, PL.NameN AS LastChgID ")
        strSQL.AppendLine(" , LastChgDate = Case When Convert(Char(10), E.LastChgDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, E.LastChgDate, 120) END ")
        strSQL.AppendLine(" FROM Experience E ")
        strSQL.AppendLine(" LEFT JOIN HRCodeMap H ON H.TabName = 'Experience' AND H.FldName = 'IndustryType' AND H.Code = E.IndustryType AND H.NotShowFlag = '0' ")
        strSQL.AppendLine(" JOIN Personal P ON E.IDNo = P.IDNo ")
        strSQL.AppendLine(" LEFT JOIN Company CL ON E.LastChgComp = CL.CompID ")
        strSQL.AppendLine(" LEFT JOIN Personal PL ON E.LastChgID = PL.EmpID AND E.LastChgComp = PL.CompID ")
        strSQL.AppendLine(" WHERE 1 = 1 ")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine(" AND P.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "EmpID"
                        strSQL.AppendLine(" AND P.EmpID LIKE '" & ht(strKey).ToString() + "%' ")
                End Select
            End If
        Next
        strSQL.AppendLine(" ORDER BY E.BeginDate, E.Seq ")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "ST1501 員工前職經歷維護-新增"
    Public Function AddExperienceSetting(ByVal beExperience As beExperience.Row, ByVal CompID As String, ByVal EmpID As String) As Boolean
        Dim bsExperience As New beExperience.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsExperience.Insert(beExperience, tran) = 0 Then Return False
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

#Region "ST1502 員工前職經歷維護-修改"
    Public Function UpdateExperienceSetting(ByVal beExperience As beExperience.Row) As Boolean
        Dim bsExperience As New beExperience.Service()
        Dim strSQL_Personal As New StringBuilder()
        Dim strFamily As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                If bsExperience.Update(beExperience, tran) = 0 Then Return False
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

#Region "ST1500 員工前職經歷維護-刪除"
    Public Function DeleteExperienceSetting(ByVal beExperience As beExperience.Row, ByVal CompID As String, ByVal EmpID As String) As Boolean
        Dim bsExperience As New beExperience.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As Data.Common.DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                bsExperience.DeleteRowByPrimaryKey(beExperience, tran)

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

    '2015/12/01 Add
#Region "ST1500 查詢Seq"
    Public Function QuerySeq(ByVal IDNo As String) As String
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select IsNull(Max(Seq), 0) + 1 From Experience")
        strSQL.AppendLine("Where 1 = 1")
        strSQL.AppendLine("And IDNo = " & Bsp.Utility.Quote(IDNo))
        If Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB") = Nothing Then
            Return ""
        Else
            Return Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")
        End If
    End Function
#End Region

#End Region

#Region "ST1600 員工通訊資料維護"

#Region "ST1600 員工通訊資料維護-查詢"
    Public Function QueryCommunicationSetting(ByVal CompID As String, ByVal EmpID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("SELECT P.NameN")
        strSQL.AppendLine(", P.CompID + '-' + IsNull(C1.CompName, '') As CompID")
        strSQL.AppendLine(", C.IDNo")
        strSQL.AppendLine(", C.RegCityCode")
        strSQL.AppendLine(", C.RegAddrCode")
        strSQL.AppendLine(", C.RegAddr")
        strSQL.AppendLine(", C.CommCityCode")
        strSQL.AppendLine(", C.CommAddrCode")
        strSQL.AppendLine(", C.CommAddr")
        strSQL.AppendLine(", C.CommTelCode1")
        strSQL.AppendLine(", C.CommTel1")
        strSQL.AppendLine(", C.CommTelCode2")
        strSQL.AppendLine(", C.CommTel2")
        strSQL.AppendLine(", C.CommTelCode3")
        strSQL.AppendLine(", C.CommTel3")
        strSQL.AppendLine(", C.CommTelCode4")
        strSQL.AppendLine(", C.CommTel4")
        strSQL.AppendLine(", C.RelName")
        strSQL.AppendLine(", C.RelTel")
        strSQL.AppendLine(", C.RelRelation")
        strSQL.AppendLine(", C.EMail")
        strSQL.AppendLine(", C.Email2")
        strSQL.AppendLine(", C.CompTelCode")
        strSQL.AppendLine(", C.AreaCode")
        strSQL.AppendLine(", C.CompTel")
        strSQL.AppendLine(", C.ExtNo")
        strSQL.AppendLine(", C.LastChgComp + '-' + IsNull(C2.CompName, '') As LastChgComp")
        strSQL.AppendLine(", C.LastChgID + '-' + ISNULL(P1.NameN, '') As LastChgID")
        strSQL.AppendLine(", LastChgDate = Case When Convert(Char(10), C.LastChgDate, 111) = '1900/01/01' Then '' Else Convert(Varchar, C.LastChgDate, 120) End")
        strSQL.AppendLine("FROM Communication C")
        strSQL.AppendLine("LEFT JOIN Personal P ON P.IDNo = C.IDNo")
        strSQL.AppendLine("Left Join Company C1 on P.CompID = C1.CompID")
        strSQL.AppendLine("LEFT JOIN Company C2 ON C.LastChgComp = C2.CompID")
        strSQL.AppendLine("LEFT JOIN Personal P1 ON C.LastChgID = P1.EmpID AND C.LastChgComp = P1.CompID")
        strSQL.AppendLine("WHERE 1 = 1")
        strSQL.AppendLine("AND P.CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine("AND P.EmpID = " & Bsp.Utility.Quote(EmpID))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "ST1600 員工通訊資料維護-修改"
    Public Function UpdateCommunicationSetting(ByVal beCommunication As beCommunication.Row) As Boolean
        Dim bsCommunication As New beCommunication.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsCommunication.Update(beCommunication, tran) = 0 Then Return False
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

#Region "ST1600 查詢此國別的區碼是否為空值"
    Public Function IsAreaCodeEmpty(ByVal CountryCode As String) As Boolean

        Dim Result As Boolean = False
        Dim strSQL As String = "SELECT COUNT(*) FROM TelAreaCode WHERE MTelFlag = '0' AND AreaCode = '' AND CountryCode = " & Bsp.Utility.Quote(CountryCode)
        Dim strRst As String = Bsp.Utility.IsStringNull(Bsp.DB.ExecuteScalar(strSQL, "eHRMSDB"), "0")
        Dim Count As Integer = Bsp.Utility.ToInteger(strRst)

        If Count > 0 Then
            Result = True
        End If

        Return Result
    End Function
#End Region

#Region "ST1600 查詢國碼是否存在於TelAreaCode"
    Public Function IsCountryCodeExist(ByVal CountryCode As String) As String

        Dim Result As Boolean = False
        Dim strSQL As String = "SELECT COUNT(*) FROM TelAreaCode WHERE MTelFlag = '0' AND CountryCode = " & Bsp.Utility.Quote(CountryCode)
        Dim strRst As String = Bsp.Utility.IsStringNull(Bsp.DB.ExecuteScalar(strSQL, "eHRMSDB"), "0")
        Dim Count As Integer = Bsp.Utility.ToInteger(strRst)

        If Count > 0 Then
            Result = True
        End If

        Return Result
    End Function
#End Region

#Region "ST1600 查詢區碼是否存在於TelAreaCode"
    Public Function IsAreaCodeExist(ByVal CountryCode As String, ByVal AreaCode As String) As String

        Dim Result As Boolean = False
        Dim strSQL As String = "SELECT COUNT(*) FROM TelAreaCode WHERE MTelFlag = '0' AND AreaCode = " & Bsp.Utility.Quote(AreaCode) & " AND CountryCode = " & Bsp.Utility.Quote(CountryCode)
        Dim strRst As String = Bsp.Utility.IsStringNull(Bsp.DB.ExecuteScalar(strSQL, "eHRMSDB"), "0")
        Dim Count As Integer = Bsp.Utility.ToInteger(strRst)

        If Count > 0 Then
            Result = True
        End If

        Return Result
    End Function
#End Region

#Region "ST1600 查詢此區碼可輸入的電話長度"
    Public Function GetTelLength(ByVal CountryCode As String, ByVal AreaCode As String) As String

        Dim Result As String = ""
        Dim strSQL As String = "SELECT TelLen FROM TelAreaCode WHERE MTelFlag = '0' AND AreaCode = " & Bsp.Utility.Quote(AreaCode) & " AND CountryCode = " & Bsp.Utility.Quote(CountryCode) & " ORDER BY TelLen"
        Dim RstTable As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL, "eHRMSDB").Tables(0)

        If RstTable.Rows.Count > 0 Then
            For i As Integer = 0 To RstTable.Rows.Count - 1
                If RstTable.Rows(i).Item("TelLen") <> "0" Then
                    Result &= RstTable.Rows(i).Item("TelLen")
                End If

                If i <> RstTable.Rows.Count - 1 Then
                    Result &= "/"
                End If
            Next
        End If

        Return Result
    End Function
#End Region

#End Region

#Region "ST1700 員工基本資料異動紀錄查詢"

#Region "ST1700 員工基本資料異動紀錄查詢-查詢"
    Public Function QueryPersonalLogSetting(ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT ")
        strSQL.AppendLine(" ROW_NUMBER() OVER (ORDER BY EffDate DESC) AS NO, EffDate, OldData, NewData, Name ")
        strSQL.AppendLine(" FROM PersonalLog ")
        strSQL.AppendLine(" WHERE 1 = 1 ")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine(" AND CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "EmpID"
                        strSQL.AppendLine(" AND EmpID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next

        strSQL.AppendLine(" AND Reason IN ('01','11') ")
        strSQL.AppendLine(" AND RelativeIDNo = '' ")
        strSQL.AppendLine(" ORDER BY EffDate DESC ")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

    '2015/12/01 Add 更改圖片顯示方式
#Region "ST1700 照片"
    Public Function EmpPhotoQuery(ParamArray Params() As String) As String
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim FileUrl As String = Bsp.Utility.getAppSetting("EmpPhoto")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        FileUrl &= "/" & ht(strKey).ToString()
                    Case "EmpID"
                        FileUrl &= "-" & ht(strKey).ToString() & ".jpg"
                End Select
            End If
        Next

        Try
            Dim wc As New System.Net.WebClient
            Dim data As Byte() = wc.DownloadData(FileUrl)
        Catch ex As Exception
            Throw New Exception("檔案路徑不存在！")
        End Try

        Return FileUrl
    End Function
#End Region

#End Region

#Region "ST1800 員工企業團經歷維護"

#Region "ST1800 員工企業團經歷維護-查詢"
    Public Function QueryEmployeeLogSetting(ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT ")
        strSQL.AppendLine(" E.IDNo, E.ModifyDate, Convert(Varchar, E.ModifyDate, 111) AS ModifyDateFormat, E.Reason, E.CompID, E.EmpID, E.Seq ")
        strSQL.AppendLine(" , Rtrim(Ltrim(E.Reason)) + '-' + ISNULL((SELECT Remark FROM EmployeeReason WHERE Reason = E.Reason), '') AS ReasonRemark ")
        strSQL.AppendLine(" , E.CompID, E.EmpID, E.Company, E.DeptID, E.DeptID + '-' + E.DeptName AS DeptName ")
        strSQL.AppendLine(" , E.OrganID, E.OrganID + '-' + E.OrganName AS OrganName ")
        strSQL.AppendLine(" , E.GroupID, E.GroupID + '-' + E.GroupName AS GroupName, E.FlowOrganID, E.FlowOrganName, E.RankID ")
        strSQL.AppendLine(" , E.TitleID, E.TitleID + '-' +  E.TitleName AS TitleName, E.PositionID, E.Position, E.WorkTypeID ")
        strSQL.AppendLine(" , E.WorkType, E.WorkStatus, E.WorkStatusName, E.Remark, E.DueDate ")
        strSQL.AppendLine(" , ISNULL(CL.CompName, '') AS LastChgComp, ISNULL(PL.NameN, '') AS LastChgID ")
        strSQL.AppendLine(" , LastChgDate = Case When Convert(Char(10), E.LastChgDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, E.LastChgDate, 120) END  ")
        '2015/08/10 Add
        strSQL.AppendLine(" , CompanyOld, E.DeptIDOld, E.DeptIDOld + '-' + E.DeptNameOld AS DeptNameOld ")
        strSQL.AppendLine(" , E.OrganIDOld, E.OrganIDOld + '-' + E.OrganNameOld AS OrganNameOld ")
        strSQL.AppendLine(" , RankIDOld, E.TitleIDOld ")
        strSQL.AppendLine(" , CASE E.TitleIDOld WHEN '' THEN E.TitleNameOld ELSE E.TitleIDOld + '-' +  E.TitleNameOld END AS TitleNameOld ") '2015/08/14 Modify
        strSQL.AppendLine(" , PositionOld, WorkTypeOld ")
        strSQL.AppendLine(" FROM EmployeeLog E ")
        strSQL.AppendLine(" JOIN Personal P ON E.IDNo = P.IDNo ")
        strSQL.AppendLine(" LEFT JOIN Company CL ON E.LastChgComp = CL.CompID ")
        strSQL.AppendLine(" LEFT JOIN Personal PL ON E.LastChgID = PL.EmpID AND E.LastChgComp = PL.CompID ")
        strSQL.AppendLine(" WHERE 1 = 1 ")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine(" AND P.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "EmpID"
                        strSQL.AppendLine(" AND P.EmpID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next
        strSQL.AppendLine(" ORDER BY E.ModifyDate DESC, E.Seq DESC")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "ST1801 員工企業團經歷維護-新增"
    Public Function AddEmployeeLogSetting(ByVal beEmployeeLog As beEmployeeLog.Row) As Boolean
        Dim bsEmployeeLog As New beEmployeeLog.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsEmployeeLog.Insert(beEmployeeLog, tran) = 0 Then Return False
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

#Region "ST1802 員工企業團經歷維護-修改"
    Public Function UpdateEmployeeLogSetting(ByVal beEmployeeLog As beEmployeeLog.Row, ByVal saveIDNo As String, ByVal saveModifyDate As String, ByVal saveReason As String, ByVal ModifyDate As String, ByVal DueDate As String) As Boolean
        Dim bsEmployeeLog As New beEmployeeLog.Service()
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" UPDATE EmployeeLog SET ")
        strSQL.AppendLine(" IDNo = " & Bsp.Utility.Quote(beEmployeeLog.IDNo.Value.ToString))
        'strSQL.AppendLine(" , ModifyDate = " & Bsp.Utility.Quote("Convert(Varchar, " + beEmployeeLog.ModifyDate.Value.ToString + ", 111)"))
        strSQL.AppendLine(" , ModifyDate = " & Bsp.Utility.Quote(ModifyDate))
        strSQL.AppendLine(" , Seq = " & Bsp.Utility.Quote(beEmployeeLog.Seq.Value.ToString))
        strSQL.AppendLine(" , Reason = " & Bsp.Utility.Quote(beEmployeeLog.Reason.Value.ToString))
        strSQL.AppendLine(" , Remark = N" & Bsp.Utility.Quote(beEmployeeLog.Remark.Value.ToString))
        'strSQL.AppendLine(" , DueDate = " & Bsp.Utility.Quote(beEmployeeLog.DueDate.Value.ToString))
        strSQL.AppendLine(" , DueDate = " & Bsp.Utility.Quote(DueDate))
        strSQL.AppendLine(" , PWID = " & Bsp.Utility.Quote(beEmployeeLog.PWID.Value.ToString))
        strSQL.AppendLine(" , PW = " & Bsp.Utility.Quote(beEmployeeLog.PW.Value.ToString))
        strSQL.AppendLine(" , IsBoss = " & Bsp.Utility.Quote(beEmployeeLog.IsBoss.Value.ToString))
        strSQL.AppendLine(" , IsSecBoss = " & Bsp.Utility.Quote(beEmployeeLog.IsSecBoss.Value.ToString))
        strSQL.AppendLine(" , IsGroupBoss = " & Bsp.Utility.Quote(beEmployeeLog.IsGroupBoss.Value.ToString))
        strSQL.AppendLine(" , IsSecGroupBoss = " & Bsp.Utility.Quote(beEmployeeLog.IsSecGroupBoss.Value.ToString))
        strSQL.AppendLine(" , BossType = " & Bsp.Utility.Quote(beEmployeeLog.BossType.Value.ToString))
        strSQL.AppendLine(" , CompID = " & Bsp.Utility.Quote(beEmployeeLog.CompID.Value.ToString))
        strSQL.AppendLine(" , EmpID = " & Bsp.Utility.Quote(beEmployeeLog.EmpID.Value.ToString))
        strSQL.AppendLine(" , Company = N" & Bsp.Utility.Quote(beEmployeeLog.Company.Value.ToString))
        strSQL.AppendLine(" , DeptID = " & Bsp.Utility.Quote(beEmployeeLog.DeptID.Value.ToString))
        strSQL.AppendLine(" , DeptName = N" & Bsp.Utility.Quote(beEmployeeLog.DeptName.Value.ToString))
        strSQL.AppendLine(" , OrganID = " & Bsp.Utility.Quote(beEmployeeLog.OrganID.Value.ToString))
        strSQL.AppendLine(" , OrganName = N" & Bsp.Utility.Quote(beEmployeeLog.OrganName.Value.ToString))
        strSQL.AppendLine(" , GroupID = " & Bsp.Utility.Quote(beEmployeeLog.GroupID.Value.ToString))
        strSQL.AppendLine(" , GroupName = N" & Bsp.Utility.Quote(beEmployeeLog.GroupName.Value.ToString))
        strSQL.AppendLine(" , FlowOrganID = " & Bsp.Utility.Quote(beEmployeeLog.FlowOrganID.Value.ToString))
        strSQL.AppendLine(" , FlowOrganName = N" & Bsp.Utility.Quote(beEmployeeLog.FlowOrganName.Value.ToString))
        strSQL.AppendLine(" , RankID = " & Bsp.Utility.Quote(beEmployeeLog.RankID.Value.ToString))
        strSQL.AppendLine(" , TitleID = " & Bsp.Utility.Quote(beEmployeeLog.TitleID.Value.ToString))
        strSQL.AppendLine(" , TitleName = N" & Bsp.Utility.Quote(beEmployeeLog.TitleName.Value.ToString))
        strSQL.AppendLine(" , PositionID = " & Bsp.Utility.Quote(beEmployeeLog.PositionID.Value.ToString))
        strSQL.AppendLine(" , Position = N" & Bsp.Utility.Quote(beEmployeeLog.Position.Value.ToString))
        strSQL.AppendLine(" , WorkTypeID = " & Bsp.Utility.Quote(beEmployeeLog.WorkTypeID.Value.ToString))
        strSQL.AppendLine(" , WorkType = N" & Bsp.Utility.Quote(beEmployeeLog.WorkType.Value.ToString))
        strSQL.AppendLine(" , WorkStatus = " & Bsp.Utility.Quote(beEmployeeLog.WorkStatus.Value.ToString))
        strSQL.AppendLine(" , WorkStatusName = N" & Bsp.Utility.Quote(beEmployeeLog.WorkStatusName.Value.ToString))
        strSQL.AppendLine(" , CompIDOld = " & Bsp.Utility.Quote(beEmployeeLog.CompIDOld.Value.ToString))
        strSQL.AppendLine(" , CompanyOld = N" & Bsp.Utility.Quote(beEmployeeLog.CompanyOld.Value.ToString))
        strSQL.AppendLine(" , DeptIDOld = " & Bsp.Utility.Quote(beEmployeeLog.DeptIDOld.Value.ToString))
        strSQL.AppendLine(" , DeptNameOld = N" & Bsp.Utility.Quote(beEmployeeLog.DeptNameOld.Value.ToString))
        strSQL.AppendLine(" , OrganIDOld = " & Bsp.Utility.Quote(beEmployeeLog.OrganIDOld.Value.ToString))
        strSQL.AppendLine(" , OrganNameOld = N" & Bsp.Utility.Quote(beEmployeeLog.OrganNameOld.Value.ToString))
        strSQL.AppendLine(" , GroupIDOld = " & Bsp.Utility.Quote(beEmployeeLog.GroupIDOld.Value.ToString))
        strSQL.AppendLine(" , GroupNameOld = N" & Bsp.Utility.Quote(beEmployeeLog.GroupNameOld.Value.ToString))
        strSQL.AppendLine(" , FlowOrganIDOld = " & Bsp.Utility.Quote(beEmployeeLog.FlowOrganIDOld.Value.ToString))
        strSQL.AppendLine(" , FlowOrganNameOld = N" & Bsp.Utility.Quote(beEmployeeLog.FlowOrganNameOld.Value.ToString))
        strSQL.AppendLine(" , RankIDOld = " & Bsp.Utility.Quote(beEmployeeLog.RankIDOld.Value.ToString))
        strSQL.AppendLine(" , TitleIDOld = " & Bsp.Utility.Quote(beEmployeeLog.TitleIDOld.Value.ToString))
        strSQL.AppendLine(" , TitleNameOld = N" & Bsp.Utility.Quote(beEmployeeLog.TitleNameOld.Value.ToString))
        strSQL.AppendLine(" , PositionIDOld = " & Bsp.Utility.Quote(beEmployeeLog.PositionIDOld.Value.ToString))
        strSQL.AppendLine(" , PositionOld = N" & Bsp.Utility.Quote(beEmployeeLog.PositionOld.Value.ToString))
        strSQL.AppendLine(" , WorkTypeIDOld = " & Bsp.Utility.Quote(beEmployeeLog.WorkTypeIDOld.Value.ToString))
        strSQL.AppendLine(" , WorkTypeOld = N" & Bsp.Utility.Quote(beEmployeeLog.WorkTypeOld.Value.ToString))
        strSQL.AppendLine(" , WorkStatusOld = " & Bsp.Utility.Quote(beEmployeeLog.WorkStatusOld.Value.ToString))
        strSQL.AppendLine(" , WorkStatusNameOld = N" & Bsp.Utility.Quote(beEmployeeLog.WorkStatusNameOld.Value.ToString))
        strSQL.AppendLine(" , LastChgComp = " & Bsp.Utility.Quote(beEmployeeLog.LastChgComp.Value.ToString))
        strSQL.AppendLine(" , LastChgID = " & Bsp.Utility.Quote(beEmployeeLog.LastChgID.Value.ToString))
        strSQL.AppendLine(" , LastChgDate = GETDATE() ")
        strSQL.AppendLine(" WHERE IDNo = " & Bsp.Utility.Quote(saveIDNo) & " AND ModifyDate = " & Bsp.Utility.Quote(saveModifyDate) & " AND Reason = " & Bsp.Utility.Quote(saveReason))
        Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB")

        Return True
    End Function
#End Region

#Region "ST1800 員工企業團經歷維護-刪除"
    Public Function DeleteEmployeeLogSetting(ByVal beEmployeeLog As beEmployeeLog.Row) As Boolean
        Dim bsEmployeeLog As New beEmployeeLog.Service()
        Dim strSQL As New StringBuilder()
        Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
        Dim dbCommand As DbCommand = db.GetStoredProcCommand("SP_EmployeeLogReSort")

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As Data.Common.DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                bsEmployeeLog.DeleteRowByPrimaryKey(beEmployeeLog, tran)

                '2015/08/05 Add SP_EmployeeLogReSort
                db.AddInParameter(dbCommand, "@InIDNo", DbType.String, beEmployeeLog.IDNo.Value.ToString)
                db.AddInParameter(dbCommand, "@InModityDate", DbType.String, Format(beEmployeeLog.ModifyDate.Value, "yyyy/MM/dd"))
                db.ExecuteNonQuery(dbCommand, tran)

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

#Region "ST1800 查詢WorkStatus"
    Public Function selectWorkStatus(ByVal Reason As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" SELECT AfterWorkStatusType, W.Remark, AfterWorkStatusType + '-' + W.Remark FROM WorkStatus_EmployeeReason WE ")
        strSQL.AppendLine(" JOIN WorkStatus W ON WE.AfterWorkStatusType = W.WorkCode ")
        strSQL.AppendLine(" WHERE Reason = " & Bsp.Utility.Quote(Reason.ToString()))
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "ST1800 查詢到職日"
    Public Function selectEmpDate(ByVal CompID As String, ByVal EmpID As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" SELECT EmpDate FROM Personal P ")
        strSQL.AppendLine(" WHERE CompID = " & Bsp.Utility.Quote(CompID.ToString()) & " AND EmpID = " & Bsp.Utility.Quote(EmpID.ToString()))
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "ST1800 異動前資料"
    Public Function QueryEmployeeLogOld(ByVal IDNo As String, ByVal ModifyDate As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" SELECT Top 1 ")
        strSQL.AppendLine(" CompID, PositionID, Position, DeptID, OrganID ")
        strSQL.AppendLine(" , WorkTypeID, WorkType, RankID, TitleID ")
        strSQL.AppendLine(" , FlowOrganID, WorkStatus ")
        strSQL.AppendLine(" , ModifyDate AS BeforeDueDate ") '2015/11/27 Add 增加生效日期
        strSQL.AppendLine(" FROM EmployeeLog ")
        strSQL.AppendLine(" WHERE IDNo = " & Bsp.Utility.Quote(IDNo) & "AND Convert(Varchar, ModifyDate, 111) <= " & Bsp.Utility.Quote(ModifyDate))
        strSQL.AppendLine(" ORDER BY ModifyDate DESC, Seq DESC ")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "ST1800 異動後資料"
    Public Function QueryEmployeeLogNew(ByVal IDNo As String, ByVal ModifyDate As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" SELECT Top 1 ")
        strSQL.AppendLine(" CompIDOld, PositionIDOld, PositionOld, DeptIDOld, OrganIDOld ")
        strSQL.AppendLine(" , WorkTypeIDOld, WorkTypeOld, RankIDOld, TitleIDOld ")
        strSQL.AppendLine(" , FlowOrganIDOld, WorkStatusOld, WorkStatusNameOld ")
        strSQL.AppendLine(" , ModifyDate AS BeforeDueDate ") '2015/11/27 Add 增加生效日期
        strSQL.AppendLine(" FROM EmployeeLog ")
        strSQL.AppendLine(" WHERE IDNo = " & Bsp.Utility.Quote(IDNo) & "AND Convert(Varchar, ModifyDate, 111) > " & Bsp.Utility.Quote(ModifyDate))
        strSQL.AppendLine(" ORDER BY ModifyDate, Seq ")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "ST1800 PositionID"
    Public Function GetPositionID(ByVal strWhere As String) As DataSet
        Dim strSQL As String

        strSQL = "Select Rtrim(PositionID) as PositionID,Remark, PositionID + '-' + Remark as FullPositionName From Position "
        If strWhere.Trim() <> "" Then
            strSQL &= " " & strWhere
        End If
        strSQL &= " order by PositionID"
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL, "eHRMSDB")
    End Function
#End Region

#Region "ST1800 WorkTypeID"
    Public Function GetWorkTypeID(ByVal strWhere As String) As DataSet
        Dim strSQL As String
        strSQL = "Select Rtrim(WorkTypeID) as WorkTypeID,Remark, WorkTypeID + '-' + Remark as FullWorkTypeName From WorkType "
        If strWhere.Trim() <> "" Then
            strSQL &= " " & strWhere
        End If
        strSQL &= " order by WorkTypeID"
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL, "eHRMSDB")
    End Function
#End Region

#Region "ST1801 新增時查詢序號"
    Public Function UpdselectSeq(ByVal IDNo As String, ByVal ModifyDate As String, ByVal Seq As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" SELECT Count(*) ")
        strSQL.AppendLine(" FROM EmployeeLog ")
        strSQL.AppendLine(" WHERE IDNo = " & Bsp.Utility.Quote(IDNo.ToString()))
        strSQL.AppendLine(" AND Convert(Char(10), ModifyDate, 111) = " & Bsp.Utility.Quote(ModifyDate.ToString()))
        strSQL.AppendLine(" AND Seq = " & Bsp.Utility.Quote(Seq.ToString()))
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

    '2015/08/03 Add 查詢序號
#Region "ST1801 新增時查詢序號"
    Public Function selectSeq(ByVal IDNo As String, ByVal ModifyDate As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" SELECT Top 1 Seq ")
        strSQL.AppendLine(" FROM EmployeeLog ")
        strSQL.AppendLine(" WHERE IDNo = " & Bsp.Utility.Quote(IDNo.ToString()))
        strSQL.AppendLine(" AND Convert(Char(10), ModifyDate, 111) = " & Bsp.Utility.Quote(ModifyDate.ToString()))
        strSQL.AppendLine(" ORDER BY ModifyDate DESC ")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#End Region

#Region "ST1900 員工中斷年資維護"

#Region "ST1900 員工中斷年資維護-上方欄位"
    Public Function QueryNotEmpLogInfo(ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select Distinct")
        strSQL.AppendLine("ISNULL(E.TotSen, 0) TotSen")
        strSQL.AppendLine(", ISNULL(FLOOR(E.Days_SPHOLD), 0) Days_SPHOLD")
        strSQL.AppendLine(", TotDays = ISNULL((Select FLOOR(SUM(Days)) From EmpSenComp Where CompID = P.CompID And EmpID = P.EmpID), 0)")
        strSQL.AppendLine(", TotDays_SPHOLD1 = ISNULL((Select SUM(CONVERT(INT, PlusOrMinus + CONVERT(VARCHAR(10), DATEDIFF(DAY, ValidDateB, ValidDateE) + 1))) From NotEmpLog Where CompID = P.CompID And EmpID = P.EmpID And CompID_ALL = 'ALL' And AdjustReason = '1'), 0)")
        strSQL.AppendLine(", TotDays_SPHOLD2 = ISNULL((Select SUM(CONVERT(INT, PlusOrMinus + CONVERT(VARCHAR(10), DATEDIFF(DAY, ValidDateB, ValidDateE) + 1))) From NotEmpLog Where CompID = P.CompID And EmpID = P.EmpID And CompID_ALL = 'ALL' And AdjustReason = '2'), 0)")
        strSQL.AppendLine(", ISNULL(E.TotSen_SPHOLD, 0) TotSen_SPHOLD")
        strSQL.AppendLine(", ISNULL(E.TotSen_SPHOLD1, 0) TotSen_SPHOLD1")
        strSQL.AppendLine(", ISNULL(E.TotSen_SPHOLD2, 0) TotSen_SPHOLD2")
        strSQL.AppendLine(", P.NotEmpDay")
        strSQL.AppendLine(", P.SinopacNotEmpDay")
        strSQL.AppendLine("From Personal P")
        strSQL.AppendLine("Left Join EmpSenComp E On E.CompID = P.CompID AND E.EmpID = P.EmpID")
        strSQL.AppendLine("WHERE 1 = 1")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine("AND P.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "EmpID"
                        strSQL.AppendLine("AND P.EmpID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "IDNo"
                        strSQL.AppendLine("AND P.IDNo = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

    Public Function QueryEmpDate(ByVal CompID As String, ByVal EmpID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("SELECT EmpDate = Case Convert(Varchar(10), EmpDate, 111) When '1900/01/01' Then '' Else Convert(Varchar, EmpDate, 111) End")
        strSQL.AppendLine(", QuitDate = Case Convert(Varchar(10), QuitDate, 111) When '1900/01/01' Then '' Else Convert(Varchar, QuitDate, 111) End")
        strSQL.AppendLine(", SinopacEmpDate = Case Convert(Varchar(10), SinopacEmpDate, 111) When '1900/01/01' Then '' Else Convert(Varchar, SinopacEmpDate, 111) End")
        strSQL.AppendLine(", SinopacQuitDate = Case Convert(Varchar(10), SinopacQuitDate, 111) When '1900/01/01' Then '' Else Convert(Varchar, SinopacQuitDate, 111) End")
        strSQL.AppendLine("From Personal")
        strSQL.AppendLine("WHERE CompID = " & Bsp.Utility.Quote(CompID) & " AND EmpID = " & Bsp.Utility.Quote(EmpID))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

    Public Function QueryNotEmpLogInfo(ByVal IDNo As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("SELECT Distinct FLOOR(Days_SPHOLD) Days_SPHOLD, TotSen_SPHOLD")
        strSQL.AppendLine("FROM EmpSenComp WHERE IDNo = " & Bsp.Utility.Quote(IDNo))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

    Public Function QueryNotEmpSen(ByVal IDNo As String, Optional ByVal CompID As String = "", Optional ByVal IsCompIDALL As Boolean = False, Optional ByVal AdjustReason As String = "") As String
        Dim strSQL As New StringBuilder()

        Dim Sen As Decimal = 0
        Dim TotSen As Decimal = 0

        strSQL.AppendLine("Select ValidDateB, ValidDateE, PlusOrMinus From NotEmpLog")
        strSQL.AppendLine("Where IDNo = " & Bsp.Utility.Quote(IDNo))
        If CompID <> "" Then
            strSQL.AppendLine("And CompID = " & Bsp.Utility.Quote(CompID))
        End If
        If IsCompIDALL Then
            strSQL.AppendLine("And CompID_ALL = 'ALL'")
            strSQL.AppendLine("And AdjustReason = " & Bsp.Utility.Quote(AdjustReason))
        Else
            strSQL.AppendLine("And CompID_ALL <> 'ALL'")
        End If

        Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
            If dt.Rows.Count > 0 Then
                Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
                Dim dbCommand As DbCommand = db.GetStoredProcCommand("SP_EmpSenCalculate")

                For Each dr As DataRow In dt.Rows
                    dbCommand.Parameters.Clear()
                    db.AddInParameter(dbCommand, "@BeginDate", DbType.String, Format(dr.Item("ValidDateB"), "yyyy/MM/dd"))
                    db.AddInParameter(dbCommand, "@EndDate", DbType.String, Format(dr.Item("ValidDateE"), "yyyy/MM/dd"))

                    Dim SqlParam As SqlParameter = New SqlParameter("@EmpSen", SqlDbType.Decimal)
                    SqlParam.Precision = 15
                    SqlParam.Scale = 9
                    SqlParam.Direction = ParameterDirection.Output
                    dbCommand.Parameters.Add(SqlParam)

                    db.ExecuteNonQuery(dbCommand)

                    Sen = db.GetParameterValue(dbCommand, "@EmpSen")

                    If IsCompIDALL Then
                        If dr.Item("PlusOrMinus") = "+" Then
                            TotSen = TotSen + Sen
                        ElseIf dr.Item("PlusOrMinus") = "-" Then
                            TotSen = TotSen - Sen
                        End If
                    Else
                        TotSen = TotSen + Sen
                    End If
                Next
            End If
        End Using

        Return TotSen.ToString("f2")
    End Function
#End Region

#Region "ST1900 員工中斷年資維護-下方表格"
    Public Function QueryNotEmpLog(ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("SELECT Distinct N.Seq")
        strSQL.AppendLine(", N.CompID")
        strSQL.AppendLine(", C.CompName")
        strSQL.AppendLine(", N.EmpID")
        strSQL.AppendLine(", N.IDNo")
        strSQL.AppendLine(", N.ValidDateB BeginDay")
        strSQL.AppendLine(", ValidDateB = Case When Convert(Char(10), N.ValidDateB, 111) = '1900/01/01' Then '' ELSE Convert(Varchar, N.ValidDateB, 111) End")
        strSQL.AppendLine(", ValidDateE = Case When Convert(Char(10), N.ValidDateE, 111) = '1900/01/01' Then '' ELSE Convert(Varchar, N.ValidDateE, 111) End")
        strSQL.AppendLine(", N.NotEmpReasonID + '-' + ISNULL(E.Remark, '') NotEmpReasonID")
        strSQL.AppendLine(", NotEmpDay = CASE WHEN N.CompID_ALL = 'ALL' THEN 0 ELSE DATEDIFF(DAY, N.ValidDateB, N.ValidDateE) + 1 END")
        strSQL.AppendLine(", DATEDIFF(DAY, N.ValidDateB, N.ValidDateE) + 1 SinopacNotEmpDay")
        strSQL.AppendLine(", N.Remark")
        strSQL.AppendLine(", ISNULL(CL.CompName, N.LastChgComp) LastChgComp")
        strSQL.AppendLine(", ISNULL(PL.NameN, N.LastChgID) LastChgID")
        strSQL.AppendLine(", LastChgDate = Case When Convert(Char(19), N.LastChgDate, 20) = '1900/01/01' Then '' ELSE Convert(Char(19), N.LastChgDate, 20) End")
        strSQL.AppendLine("From NotEmpLog N")
        strSQL.AppendLine("Left Join EmployeeReason E On N.NotEmpReasonID = E.Reason")
        strSQL.AppendLine("Left Join Company C On N.CompID = C.CompID")
        strSQL.AppendLine("Left Join Company CL On N.LastChgComp = CL.CompID")
        strSQL.AppendLine("Left Join Personal PL On N.LastChgID = PL.EmpID And N.LastChgComp = PL.CompID ")
        strSQL.AppendLine("WHERE CompID_ALL <> 'ALL'")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine("AND N.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "EmpID"
                        strSQL.AppendLine("AND N.EmpID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "IDNo"
                        strSQL.AppendLine("AND N.IDNo = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next
        strSQL.AppendLine("ORDER BY  N.CompID, N.ValidDateB DESC")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "ST1900 員工中斷年資維護-公司別年資"
    Public Function QueryCompEmpSen(ByVal IDNo As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("SELECT C.CompName, TotSen, FLOOR(SUM(Days)) TotDays")
        strSQL.AppendLine("FROM EmpSenComp E")
        strSQL.AppendLine("LEFT JOIN Company C ON E.CompID = C.CompID")
        strSQL.AppendLine("WHERE IDNo = " & Bsp.Utility.Quote(IDNo))
        strSQL.AppendLine("GROUP BY C.CompName, TotSen")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "ST1901 員工中斷年資維護-新增"
    Public Function AddNotEmpLogSetting(ByVal beNotEmpLog As beNotEmpLog.Row) As Boolean
        Dim bsNotEmpLog As New beNotEmpLog.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsNotEmpLog.Insert(beNotEmpLog, tran) = 0 Then Return False

                Bsp.DB.ExecuteNonQuery(CommandType.StoredProcedure, "init_EmpSenComp", _
                   New DbParameter() { _
                   Bsp.DB.getDbParameter("@IDNo", beNotEmpLog.IDNo.Value)}, tran)

                Bsp.DB.ExecuteNonQuery(CommandType.StoredProcedure, "sHR_EmpSenComp", _
                   New DbParameter() { _
                   Bsp.DB.getDbParameter("@IDNo", beNotEmpLog.IDNo.Value)}, tran)

                strSQL.Clear()
                strSQL.AppendLine("UPDATE Personal SET")
                strSQL.AppendLine("NotEmpDay = ISNULL((")
                strSQL.AppendLine("Select SUM(DATEDIFF(DAY, N.ValidDateB, N.ValidDateE) + 1) From NotEmpLog N")
                strSQL.AppendLine("Group By N.CompID, N.EmpID, CompID_ALL")
                strSQL.AppendLine("Having N.EmpID = " & Bsp.Utility.Quote(beNotEmpLog.EmpID.Value))
                strSQL.AppendLine("And N.CompID = " & Bsp.Utility.Quote(beNotEmpLog.CompID.Value))
                strSQL.AppendLine("And N.CompID_ALL <> 'ALL'")
                strSQL.AppendLine("), 0)")
                strSQL.AppendLine("Where EmpID = " & Bsp.Utility.Quote(beNotEmpLog.EmpID.Value))
                strSQL.AppendLine("And CompID = " & Bsp.Utility.Quote(beNotEmpLog.CompID.Value))

                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)

                strSQL.Clear()
                strSQL.AppendLine("UPDATE Personal SET")
                strSQL.AppendLine("SinopacNotEmpDay = ISNULL((")
                strSQL.AppendLine("Select SUM(DATEDIFF(DAY, N.ValidDateB, N.ValidDateE) + 1) From NotEmpLog N")
                strSQL.AppendLine("Group By N.IDNo, CompID_ALL")
                strSQL.AppendLine("Having N.IDNo = " & Bsp.Utility.Quote(beNotEmpLog.IDNo.Value))
                strSQL.AppendLine("And N.CompID_ALL <> 'ALL'")
                strSQL.AppendLine("), 0)")
                strSQL.AppendLine("Where IDNo = " & Bsp.Utility.Quote(beNotEmpLog.IDNo.Value))

                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)

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

#Region "ST1902 員工中斷年資維護-修改"
    Public Function UpdateNotEmpLogSetting(ByVal beNotEmpLog As beNotEmpLog.Row) As Boolean
        Dim bsNotEmpLog As New beNotEmpLog.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                If bsNotEmpLog.Update(beNotEmpLog, tran) = 0 Then Return False

                Bsp.DB.ExecuteNonQuery(CommandType.StoredProcedure, "init_EmpSenComp", _
                   New DbParameter() { _
                   Bsp.DB.getDbParameter("@IDNo", beNotEmpLog.IDNo.Value)}, tran)

                Bsp.DB.ExecuteNonQuery(CommandType.StoredProcedure, "sHR_EmpSenComp", _
                   New DbParameter() { _
                   Bsp.DB.getDbParameter("@IDNo", beNotEmpLog.IDNo.Value)}, tran)

                strSQL.Clear()
                strSQL.AppendLine("UPDATE Personal SET")
                strSQL.AppendLine("NotEmpDay = ISNULL((")
                strSQL.AppendLine("Select SUM(DATEDIFF(DAY, N.ValidDateB, N.ValidDateE) + 1) From NotEmpLog N")
                strSQL.AppendLine("Group By N.CompID, N.EmpID, CompID_ALL")
                strSQL.AppendLine("Having N.EmpID = " & Bsp.Utility.Quote(beNotEmpLog.EmpID.Value))
                strSQL.AppendLine("And N.CompID = " & Bsp.Utility.Quote(beNotEmpLog.CompID.Value))
                strSQL.AppendLine("And N.CompID_ALL <> 'ALL'")
                strSQL.AppendLine("), 0)")
                strSQL.AppendLine("Where EmpID = " & Bsp.Utility.Quote(beNotEmpLog.EmpID.Value))
                strSQL.AppendLine("And CompID = " & Bsp.Utility.Quote(beNotEmpLog.CompID.Value))

                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)

                strSQL.Clear()
                strSQL.AppendLine("UPDATE Personal SET")
                strSQL.AppendLine("SinopacNotEmpDay = ISNULL((")
                strSQL.AppendLine("Select SUM(DATEDIFF(DAY, N.ValidDateB, N.ValidDateE) + 1) From NotEmpLog N")
                strSQL.AppendLine("Group By N.IDNo, CompID_ALL")
                strSQL.AppendLine("Having N.IDNo = " & Bsp.Utility.Quote(beNotEmpLog.IDNo.Value))
                strSQL.AppendLine("And N.CompID_ALL <> 'ALL'")
                strSQL.AppendLine("), 0)")
                strSQL.AppendLine("Where IDNo = " & Bsp.Utility.Quote(beNotEmpLog.IDNo.Value))

                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)


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

#Region "ST1900 員工中斷年資維護-刪除"
    Public Function DeleteNotEmpLogSetting(ByVal beNotEmpLog As beNotEmpLog.Row) As Boolean
        Dim bsNotEmpLog As New beNotEmpLog.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As Data.Common.DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                bsNotEmpLog.DeleteRowByPrimaryKey(beNotEmpLog, tran)

                Bsp.DB.ExecuteNonQuery(CommandType.StoredProcedure, "init_EmpSenComp", _
                   New DbParameter() { _
                   Bsp.DB.getDbParameter("@IDNo", beNotEmpLog.IDNo.Value)}, tran)

                Bsp.DB.ExecuteNonQuery(CommandType.StoredProcedure, "sHR_EmpSenComp", _
                   New DbParameter() { _
                   Bsp.DB.getDbParameter("@IDNo", beNotEmpLog.IDNo.Value)}, tran)

                strSQL.Clear()
                strSQL.AppendLine("UPDATE Personal SET")
                strSQL.AppendLine("NotEmpDay = ISNULL((")
                strSQL.AppendLine("Select SUM(DATEDIFF(DAY, N.ValidDateB, N.ValidDateE) + 1) From NotEmpLog N")
                strSQL.AppendLine("Group By N.CompID, N.EmpID, CompID_ALL")
                strSQL.AppendLine("Having N.EmpID = " & Bsp.Utility.Quote(beNotEmpLog.EmpID.Value))
                strSQL.AppendLine("And N.CompID = " & Bsp.Utility.Quote(beNotEmpLog.CompID.Value))
                strSQL.AppendLine("And N.CompID_ALL <> 'ALL'")
                strSQL.AppendLine("), 0)")
                strSQL.AppendLine("Where EmpID = " & Bsp.Utility.Quote(beNotEmpLog.EmpID.Value))
                strSQL.AppendLine("And CompID = " & Bsp.Utility.Quote(beNotEmpLog.CompID.Value))

                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran)

                strSQL.Clear()
                strSQL.AppendLine("UPDATE Personal SET")
                strSQL.AppendLine("SinopacNotEmpDay = ISNULL((")
                strSQL.AppendLine("Select SUM(DATEDIFF(DAY, N.ValidDateB, N.ValidDateE) + 1) From NotEmpLog N")
                strSQL.AppendLine("Group By N.IDNo, CompID_ALL")
                strSQL.AppendLine("Having N.IDNo = " & Bsp.Utility.Quote(beNotEmpLog.IDNo.Value))
                strSQL.AppendLine("And N.CompID_ALL <> 'ALL'")
                strSQL.AppendLine("), 0)")
                strSQL.AppendLine("Where IDNo = " & Bsp.Utility.Quote(beNotEmpLog.IDNo.Value))

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

#Region "ST1901 查詢MaxSeq + 1"
    Public Function QueryMaxSeq(ByVal CompID As String, ByVal EmpID As String) As String
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select IsNull(Max(Seq), 0) + 1 From NotEmpLog")
        strSQL.AppendLine("Where 1 = 1")
        strSQL.AppendLine("And CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine("And EmpID = " & Bsp.Utility.Quote(EmpID))
        If Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB") = Nothing Then
            Return ""
        Else
            Return Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")
        End If
    End Function
#End Region

#End Region

#Region "ST1910 員工企業團中斷年資維護"

#Region "員工企業團中斷年資維護-下方表格"
    Public Function QueryNotEmpLog_SPHOLD(ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("SELECT Distinct N.Seq")
        strSQL.AppendLine(", N.CompID")
        strSQL.AppendLine(", C.CompName")
        strSQL.AppendLine(", N.EmpID")
        strSQL.AppendLine(", N.IDNo")
        strSQL.AppendLine(", N.AdjustReason AR")
        strSQL.AppendLine(", AdjustReason = CASE N.AdjustReason WHEN '1' THEN '1-退休金計算使用(同步請假系統)' ELSE '2-請假系統使用' END")
        strSQL.AppendLine(", N.PlusOrMinus")
        strSQL.AppendLine(", ValidDateBB = Case When Convert(Char(10), N.ValidDateB, 111) = '1900/01/01' Then '' ELSE Convert(Varchar, N.ValidDateB, 111) End")
        strSQL.AppendLine(", ValidDateEE = Case When Convert(Char(10), N.ValidDateE, 111) = '1900/01/01' Then '' ELSE Convert(Varchar, N.ValidDateE, 111) End")
        strSQL.AppendLine(", N.ValidDateB")
        strSQL.AppendLine(", NotEmpDay = DATEDIFF(DAY, N.ValidDateB, N.ValidDateE) + 1")
        strSQL.AppendLine(", SinopacNotEmpDay = DATEDIFF(DAY, N.ValidDateB, N.ValidDateE) + 1")
        strSQL.AppendLine(", N.Remark")
        strSQL.AppendLine(", ISNULL(CL.CompName, N.LastChgComp) LastChgComp")
        strSQL.AppendLine(", ISNULL(PL.NameN, N.LastChgID) LastChgID")
        strSQL.AppendLine(", LastChgDate = Case When Convert(Char(19), N.LastChgDate, 20) = '1900/01/01' Then '' ELSE Convert(Char(19), N.LastChgDate, 20) End")
        strSQL.AppendLine("From NotEmpLog N")
        strSQL.AppendLine("Left Join Company C On N.CompID = C.CompID")
        strSQL.AppendLine("Left Join Company CL On N.LastChgComp = CL.CompID")
        strSQL.AppendLine("Left Join Personal PL On N.LastChgID = PL.EmpID And N.LastChgComp = PL.CompID")
        strSQL.AppendLine("WHERE CompID_ALL = 'ALL'")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine("AND N.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "EmpID"
                        strSQL.AppendLine("AND N.EmpID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "IDNo"
                        strSQL.AppendLine("AND N.IDNo = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next
        strSQL.AppendLine("ORDER BY  N.CompID, N.AdjustReason, N.ValidDateB DESC")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "員工企業團中斷年資維護-新增"
    Public Function AddNotEmpLog_SPHOLD(ByVal beNotEmpLog() As beNotEmpLog.Row) As Boolean
        Dim bsNotEmpLog As New beNotEmpLog.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsNotEmpLog.Insert(beNotEmpLog, tran) = 0 Then Return False

                Bsp.DB.ExecuteNonQuery(CommandType.StoredProcedure, "init_EmpSenComp", _
                   New DbParameter() { _
                   Bsp.DB.getDbParameter("@IDNo", beNotEmpLog(0).IDNo.Value)}, tran)

                Bsp.DB.ExecuteNonQuery(CommandType.StoredProcedure, "sHR_EmpSenComp", _
                   New DbParameter() { _
                   Bsp.DB.getDbParameter("@IDNo", beNotEmpLog(0).IDNo.Value)}, tran)

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

#Region "員工企業團中斷年資維護-修改"
    Public Function UpdateNotEmpLog_SPHOLD(ByVal beNotEmpLog As beNotEmpLog.Row) As Boolean
        Dim bsNotEmpLog As New beNotEmpLog.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                If bsNotEmpLog.Update(beNotEmpLog, tran) = 0 Then Return False

                Bsp.DB.ExecuteNonQuery(CommandType.StoredProcedure, "init_EmpSenComp", _
                   New DbParameter() { _
                   Bsp.DB.getDbParameter("@IDNo", beNotEmpLog.IDNo.Value)}, tran)

                Bsp.DB.ExecuteNonQuery(CommandType.StoredProcedure, "sHR_EmpSenComp", _
                   New DbParameter() { _
                   Bsp.DB.getDbParameter("@IDNo", beNotEmpLog.IDNo.Value)}, tran)

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

#Region "員工企業團中斷年資維護-刪除"
    Public Function DeleteNotEmpLog_SPHOLD(ByVal beNotEmpLog As beNotEmpLog.Row) As Boolean
        Dim bsNotEmpLog As New beNotEmpLog.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As Data.Common.DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                strSQL.AppendLine("DELETE NotEmpLog")
                strSQL.AppendLine("WHERE CompID = @CompID")
                strSQL.AppendLine("AND EmpID = @EmpID")
                strSQL.AppendLine("AND IDNo = @IDNo")
                strSQL.AppendLine("AND ValidDateB = @ValidDateB")
                strSQL.AppendLine("AND ValidDateE = @ValidDateE")
                strSQL.AppendLine("AND CompID_ALL = 'ALL'")

                Dim DbParam() As DbParameter = { _
                    Bsp.DB.getDbParameter("@CompID", beNotEmpLog.CompID.Value), _
                    Bsp.DB.getDbParameter("@EmpID", beNotEmpLog.EmpID.Value), _
                    Bsp.DB.getDbParameter("@IDNo", beNotEmpLog.IDNo.Value), _
                    Bsp.DB.getDbParameter("@ValidDateB", beNotEmpLog.ValidDateB.Value), _
                    Bsp.DB.getDbParameter("@ValidDateE", beNotEmpLog.ValidDateE.Value)}

                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), DbParam, tran, "eHRMSDB")

                Bsp.DB.ExecuteNonQuery(CommandType.StoredProcedure, "init_EmpSenComp", _
                   New DbParameter() { _
                   Bsp.DB.getDbParameter("@IDNo", beNotEmpLog.IDNo.Value)}, tran)

                Bsp.DB.ExecuteNonQuery(CommandType.StoredProcedure, "sHR_EmpSenComp", _
                   New DbParameter() { _
                   Bsp.DB.getDbParameter("@IDNo", beNotEmpLog.IDNo.Value)}, tran)

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

#Region "ST1A00 員工年資紀錄查詢"

#Region "ST1A00 員工年資紀錄查詢-職等年資EmpSenRank"
    Public Function QueryEmpSenRank(ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("SELECT E.RankID")
        'strSQL.AppendLine(", T.TitleName")
        strSQL.AppendLine(", ValidDateB = Case When Convert(Char(10), ValidDateB, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, ValidDateB, 111) End")
        strSQL.AppendLine(", ValidDateE = Case When Convert(Char(10), ValidDateE, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, ValidDateE, 111) End")
        strSQL.AppendLine(", E.CurrentSen, E.TotSen")
        strSQL.AppendLine(", E.LastChgComp,E.LastChgID")
        strSQL.AppendLine(", LastChgDate = Case When Convert(Char(10), E.LastChgDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, E.LastChgDate, 120) End")
        strSQL.AppendLine("FROM EmpSenRank E")
        'strSQL.AppendLine("LEFT JOIN Title T")
        'strSQL.AppendLine("ON E.CompID =T.CompID and  E.RankID =T.RankID")
        strSQL.AppendLine("WHERE 1 = 1")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine("AND E.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "EmpID"
                        strSQL.AppendLine("AND E.EmpID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next
        strSQL.AppendLine("ORDER BY E.ValidDateB DESC, E.RankID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "ST1A00 員工年資紀錄查詢-單位年資EmpSenOrgType"
    Public Function QueryEmpSenOrgType(ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("SELECT")
        strSQL.AppendLine("E.OrgType, ISNULL(H.CodeCName, '') AS OrgTypeName")
        strSQL.AppendLine(", ValidDateB = Case When Convert(Char(10), ValidDateB, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, ValidDateB, 111) End")
        strSQL.AppendLine(", ValidDateE = Case When Convert(Char(10), ValidDateE, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, ValidDateE, 111) End")
        strSQL.AppendLine(", E.CurrentSen, E.TotSen")
        strSQL.AppendLine(", E.LastChgComp,E.LastChgID")
        strSQL.AppendLine(", LastChgDate = Case When Convert(Char(10), E.LastChgDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, E.LastChgDate, 120) End")
        strSQL.AppendLine("FROM EmpSenOrgType E")
        strSQL.AppendLine("LEFT JOIN HRCodeMap H ON H.TabName = 'Organization_OrgType' AND H.FldName = E.PreCompID AND H.Code = E.OrgType AND H.NotShowFlag = '0'") ' H.FldName = E.CompID => H.FldName ="& Bsp.Utility.Quote(ht("CompID").ToString()) ※中間須加條件時
        'strSQL.AppendLine("LEFT JOIN Company CL ON E.LastChgComp = CL.CompID")
        'strSQL.AppendLine("LEFT JOIN Personal PL ON E.LastChgID = PL.EmpID AND E.LastChgComp = PL.CompID")
        strSQL.AppendLine("WHERE 1 = 1")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine("AND E.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "EmpID"
                        strSQL.AppendLine("AND E.EmpID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next
        strSQL.AppendLine("ORDER BY E.ValidDateB DESC, E.OrgType")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "ST1A00 員工年資紀錄查詢-簽核單位年資EmpSenOrgTypeFlow"
    Public Function QueryEmpSenOrgTypeFlow(ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("SELECT")
        strSQL.AppendLine("E.OrgType,ISNULL(H.CodeCName, '') AS OrgTypeFlowName")
        strSQL.AppendLine(", ValidDateB = Case When Convert(Char(10), ValidDateB, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, ValidDateB, 111) End")
        strSQL.AppendLine(", ValidDateE = Case When Convert(Char(10), ValidDateE, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, ValidDateE, 111) End")
        strSQL.AppendLine(", E.CurrentSen, E.TotSen")
        strSQL.AppendLine(", E.LastChgComp,E.LastChgID")
        strSQL.AppendLine(", LastChgDate = Case When Convert(Char(10), E.LastChgDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, E.LastChgDate, 120) End")
        strSQL.AppendLine("FROM EmpSenOrgTypeFlow E") '2015/12/02 Modify
        strSQL.AppendLine("LEFT JOIN HRCodeMap H ON H.TabName = 'OrganizationFlow_OrgType' AND H.Code = E.OrgType AND H.NotShowFlag = '0'")
        'strSQL.AppendLine("LEFT JOIN Company CL ON E.LastChgComp = CL.CompID")
        'strSQL.AppendLine("LEFT JOIN Personal PL ON E.LastChgID = PL.EmpID AND E.LastChgComp = PL.CompID")
        strSQL.AppendLine("WHERE 1 = 1")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine("AND E.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "EmpID"
                        strSQL.AppendLine("AND E.EmpID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next
        strSQL.AppendLine("ORDER BY E.ValidDateB DESC, E.OrgType")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "ST1A00 員工年資紀錄查詢-工作性質年資EmpSenWorkType"
    Public Function QueryEmpSenWorkType(ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("SELECT")
        strSQL.AppendLine("E.WorkTypeID,ISNULL(W.Remark, '') AS WorkTypeName")
        strSQL.AppendLine(", ValidDateB = Case When Convert(Char(10), ValidDateB, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, ValidDateB, 111) End")
        strSQL.AppendLine(", ValidDateE = Case When Convert(Char(10), ValidDateE, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, ValidDateE, 111) End")
        strSQL.AppendLine(", E.CurrentSen, E.TotSen")
        strSQL.AppendLine(", ISNULL(WI.CategoryIName, '') AS CategoryIName, E.TotCategoryI")
        strSQL.AppendLine(", ISNULL(WII.CategoryIIName, '') AS CategoryIIName, E.TotCategoryII")
        strSQL.AppendLine(", ISNULL(WIII.CategoryIIIName, '') AS CategoryIIIName, E.TotCategoryIII")
        strSQL.AppendLine(", E.LastChgComp,E.LastChgID")
        strSQL.AppendLine(", LastChgDate = Case When Convert(Char(10), E.LastChgDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, E.LastChgDate, 120) End")
        strSQL.AppendLine("FROM EmpSenWorkType AS E")
        strSQL.AppendLine("LEFT JOIN WorkType AS W on E.PreCompID = W.CompID AND E.WorkTypeID = W.WorkTypeID")
        strSQL.AppendLine("LEFT JOIN WorkType_CategoryI AS WI ON E.CategoryI = WI.CategoryI")
        strSQL.AppendLine("LEFT JOIN WorkType_CategoryII AS WII ON E.CategoryI = WII.CategoryI AND E.CategoryII = WII.CategoryII")
        strSQL.AppendLine("LEFT JOIN WorkType_CategoryIII AS WIII ON E.CategoryI = WIII.CategoryI AND E.CategoryII = WIII.CategoryII AND E.CategoryIII = WIII.CategoryIII")
        'strSQL.AppendLine("LEFT JOIN Company CL ON E.LastChgComp = CL.CompID")
        'strSQL.AppendLine("LEFT JOIN Personal PL ON E.LastChgID = PL.EmpID AND E.LastChgComp = PL.CompID")
        strSQL.AppendLine("WHERE 1 = 1")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine("AND E.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "EmpID"
                        strSQL.AppendLine("AND E.EmpID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next
        strSQL.AppendLine("ORDER BY E.ValidDateB DESC, E.WorkTypeID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "ST1A00 員工年資紀錄查詢-職位年資EmpSenPosition"
    Public Function QueryEmpSenPosition(ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("SELECT")
        strSQL.AppendLine("E.PositionID, ISNULL(W.Remark, '') AS PositionName")
        strSQL.AppendLine(", ValidDateB = Case When Convert(Char(10), ValidDateB, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, ValidDateB, 111) End")
        strSQL.AppendLine(", ValidDateE = Case When Convert(Char(10), ValidDateE, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, ValidDateE, 111) End")
        strSQL.AppendLine(", E.CurrentSen, E.TotSen")
        strSQL.AppendLine(", ISNULL(PI.CategoryIName, '') AS CategoryIName, E.TotCategoryI")
        strSQL.AppendLine(", ISNULL(PII.CategoryIIName, '') AS CategoryIIName, E.TotCategoryII")
        strSQL.AppendLine(", ISNULL(PIII.CategoryIIIName, '') AS CategoryIIIName, E.TotCategoryIII")
        strSQL.AppendLine(", E.LastChgComp,E.LastChgID")
        strSQL.AppendLine(", LastChgDate = Case When Convert(Char(10), E.LastChgDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, E.LastChgDate, 120) End")
        strSQL.AppendLine("FROM EmpSenPosition AS E")
        strSQL.AppendLine("LEFT JOIN Position AS W on E.CompID = W.CompID AND E.PositionID = W.PositionID")
        strSQL.AppendLine("LEFT JOIN Position_CategoryI AS PI ON E.CategoryI = PI.CategoryI")
        strSQL.AppendLine("LEFT JOIN Position_CategoryII AS PII ON E.CategoryI = PII.CategoryI AND E.CategoryII = PII.CategoryII")
        strSQL.AppendLine("LEFT JOIN Position_CategoryIII AS PIII ON E.CategoryI = PIII.CategoryI AND E.CategoryII = PIII.CategoryII AND E.CategoryIII = PIII.CategoryIII")
        'strSQL.AppendLine("LEFT JOIN Company CL ON E.LastChgComp = CL.CompID")
        'strSQL.AppendLine("LEFT JOIN Personal PL ON E.LastChgID = PL.EmpID AND E.LastChgComp = PL.CompID")
        strSQL.AppendLine("WHERE 1 = 1")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine("AND E.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "EmpID"
                        strSQL.AppendLine("AND E.EmpID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next
        strSQL.AppendLine("ORDER BY E.ValidDateB DESC, E.PositionID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "ST1A00 員工年資紀錄查詢-公司年資EmpSenComp"
    Public Function QueryEmpSenComp(ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("SELECT E.CompID")
        strSQL.AppendLine(", C.CompName")
        strSQL.AppendLine(", P.WorkStatus")
        strSQL.AppendLine(", W.Remark")
        strSQL.AppendLine(", ValidDateB = Case When Convert(Char(10), E.ValidDateB, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, E.ValidDateB, 111) End")
        strSQL.AppendLine(", ValidDateE = Case When Convert(Char(10), E.ValidDateE, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, E.ValidDateE, 111) End")
        strSQL.AppendLine(", E.ConSen")
        strSQL.AppendLine(", E.TotSen_SPHOLD")
        strSQL.AppendLine(", E.TotSen")
        strSQL.AppendLine(", E.LastChgComp")
        strSQL.AppendLine(", E.LastChgID")
        strSQL.AppendLine(", LastChgDate = Case When Convert(Char(19), E.LastChgDate, 20) = '1900/01/01' Then '' ElSE Convert(Char(19), E.LastChgDate, 20) End")
        strSQL.AppendLine("FROM EmpSenComp AS E")
        strSQL.AppendLine("LEFT JOIN Company C ON E.CompID = C.CompID")
        strSQL.AppendLine("LEFT JOIN Personal P ON P.CompID = E.CompID AND P.EmpID = E.EmpID")
        strSQL.AppendLine("LEFT JOIN WorkStatus W ON P.WorkStatus = W.WorkCode")
        strSQL.AppendLine("WHERE 1 = 1")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine("AND E.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "EmpID"
                        strSQL.AppendLine("AND E.EmpID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next
        strSQL.AppendLine("Order by E.ValidDateB DESC")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#End Region


    Public Function QueryOrganID(a As String, b As String)
        Return ""
    End Function

End Class
