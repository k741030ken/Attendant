'****************************************************
'功能說明：待異動維護作業
'建立人員：leo
'建立日期：2016.10
'****************************************************
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.IO
Imports System

Public Class OM1
#Region "共用[原OM2]"
#Region "日期手賤防治法"
    Private Errors As String
    Public Function Check_SP(ByVal DateText As String) As Boolean
        If DateText = "" Then
            Errors = "日期未輸入"
            Return True
        End If

        If Replace(Replace(DateText, "_", ""), "/", "").Length < 8 Then
            Errors = "日期輸入格式錯誤"
            Return True
        End If

        Dim strValue() As String = Split(DateText, "/")
        Dim Year As Integer = strValue(0)
        Dim Month As Integer = strValue(1)
        Dim DateD As Integer = strValue(2)


        If Year < 1900 Or Month > 12 Or Month < 0 Or DateD > 31 Or DateD < 0 Then
            Errors = "日期須介於1900/01/02~9999/12/31"
            Return True
        End If
        Return False
    End Function
    Public Function Check(ByVal DateText As String) As Boolean
        If DateText <> "" Then
            If Replace(Replace(DateText, "_", ""), "/", "").Length < 8 Then
                Errors = "日期輸入格式錯誤"
                Return True
            End If
            Dim strValue() As String = Split(DateText, "/")
            Dim Year As Integer = strValue(0)
            Dim Month As Integer = strValue(1)
            Dim DateD As Integer = strValue(2)


            If Year < 1900 Or Month > 12 Or Month < 0 Or DateD > 31 Or DateD < 0 Then
                Errors = "日期須介於1900/01/02~9999/12/31"
                Return True
            End If
        End If
        Return False
    End Function
    Public Function rtError() As String
        Return Errors
    End Function
    Public Function DateCheck(ByVal DateText As String) As String
        If DateText = "" Then
            Return DateText
        End If
        If Replace(Replace(DateText, "_", ""), "/", "").Length < 8 Then
            Return DateText
        End If
        Dim DateTemp() As String = Split(DateText, "/")
        If DateText = "1900/01/01" Or DateTemp(1) > 12 Or DateTemp(2) > 31 Then
            DateText = ""
            Return DateText
        End If
        If Convert.ToDateTime(DateText) < "1900/01/01" Then
            DateText = ""
            Return DateText
        End If
        Return DateText
    End Function
#End Region
#Region "萬用單一欄位單筆資料查詢"
    Public Function GetValue(ByVal strFrom As String, ByVal strSelect As String, ByVal strWhere As String, ByVal strOrder As String) As String
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine("Select TOP 1" & strSelect)
        strSQL.AppendLine(" From " & strFrom)
        If strWhere <> "" Then strSQL.AppendLine(" Where 1=1  " & strWhere)
        If strOrder <> "" Then strSQL.AppendLine(" Order by  " & strOrder)
        Return IIf(IsNothing(Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")), "", Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB").ToString)
    End Function
#End Region
#Region "非SQL-OrganTypeOrganReasonToNo"
    Public Function OrganTypeOrganReasonToNo(ByVal OrganType As String, ByVal OrganReason As String) As String
        Select Case OrganType
            Case "行政組織"
                OrganType = "1"
            Case "功能組織"
                OrganType = "2"
            Case "行政與功能組織"
                OrganType = "3"
        End Select
        Select Case OrganReason
            Case "組織新增"
                OrganReason = "1"
            Case "組織無效"
                OrganReason = "2"
            Case "組織異動"
                OrganReason = "3"
            Case "組織更名"
                OrganReason = "4"
        End Select
        Return OrganType & OrganReason
    End Function
#End Region

#Region "IsDataExists"
    Public Function IsDataExists(ByVal strTable As String, ByVal strWhere As String) As Boolean
        Dim strSQL As String
        strSQL = "Select Count(*) Cnt From " & strTable
        strSQL += " Where 1 = 1 " & strWhere
        Return IIf(Bsp.DB.ExecuteScalar(strSQL, "eHRMSDB") > 0, True, False)
    End Function
#End Region
#End Region

#Region "OM2201&2202 [續OM2]"
    Public Function GetSeq2(ByVal strFrom As String, ByVal strWhere As String) As String
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine("Select ISNULL(MAX(Seq), 0)+1 as Seq From " & strFrom)
        strSQL.AppendLine(" Where 1=1  " & strWhere)
        Return IIf(IsNothing(Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")), "", Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB").ToString)
    End Function
    'OM2202 Update用
    Public Function UpdateOLAddition(ByVal beOrganizationLog As beOrganizationLog.Row, ByVal CompID As String, ByVal OrganReason As String, ByVal OrganType As String, ByVal ValidDateB As String, ByVal OrganID As String, ByVal Seq As String) As Boolean

        Dim bsOrganizationLog As New beOrganizationLog.Service()
        Dim beOrganizationLog2 As New beOrganizationLog.Row()
        Dim bsOrganizationLog2 As New beOrganizationLog.Service()

        Dim strSQL As New StringBuilder()
        With beOrganizationLog2
            .CompID.Value = CompID
            .OrganReason.Value = OrganReason
            .OrganType.Value = OrganType
            .ValidDateB.Value = ValidDateB
            .Seq.Value = Seq
            .OrganID.Value = OrganID
        End With

        '/*-----------------------------*/

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Try

                If bsOrganizationLog2.DeleteRowByPrimaryKey(beOrganizationLog2, tran) = 0 Then
                    Return False
                End If
                If bsOrganizationLog.Insert(beOrganizationLog, tran) = 0 Then
                    Return False
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
    'OM2201 Insert用
    Public Function InsertOLAddition(ByVal beOrganizationLog As beOrganizationLog.Row) As Boolean
        Dim bsOrganizationLog As New beOrganizationLog.Service()
        Dim strSQL As New StringBuilder()
        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
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
    'ValidDateWhere= "" 找之前，"Old"找之後   else相等
    '參考OM2201的GetSelectData註解
    Public Function OM2200OrganizationLog(ByVal CompID As String, ByVal OrganReason As String, ByVal OrganType As String, ByVal ValidDateB As String, ByVal ValidDateWhere As String, ByVal OrganID As String, ByVal Seq As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("SELECT  E.* " & _
        ", P.NameN as PNameN, P2.NameN as P2NameN, P3.NameN as P3NameN, P4.NameN as P4NameN, W.WorkSiteID, W.Remark, P5.NameN as P5NameN" & _
        ", OP.NameN as PNameNOld, OP2.NameN as P2NameNOld, OP3.NameN as P3NameNOld, OP4.NameN as P4NameNOld, OW.WorkSiteID as WorkSiteIDOld, OW.Remark as RemarkOld, OP5.NameN as P5NameNOld ")
        strSQL.AppendLine(" FROM OrganizationLog E")
        '部門主管
        strSQL.AppendLine(" Left join Personal P on E.BossCompID = P.CompID and E.Boss = P.EmpID ")
        '副主管
        strSQL.AppendLine(" Left join Personal P2 on E.SecBossCompID = P2.CompID and E.SecBoss = P2.EmpID ")
        '人事管理員
        strSQL.AppendLine(" Left join Personal P3 on E.CompID = P3.CompID and E.PersonPart = P3.EmpID ")
        '第二人事管理員
        strSQL.AppendLine(" Left join Personal P4 on E.CompID = P4.CompID and E.SecPersonPart = P4.EmpID ")
        '工作地點
        strSQL.AppendLine(" Left join WorkSite W on E.CompID = W.CompID and E.WorkSiteID=W.WorkSiteID ")
        '自行查核主管姓名
        strSQL.AppendLine(" Left join Personal P5 on E.CompID = P5.CompID and E.CheckPart = P5.EmpID ")
        'old
        '部門主管
        strSQL.AppendLine(" Left join Personal OP on E.BossCompIDOld = OP.CompID and E.BossOld = OP.EmpID ")
        '副主管
        strSQL.AppendLine(" Left join Personal OP2 on E.SecBossCompIDOld = OP2.CompID and E.SecBossOld = OP2.EmpID ")
        '人事管理員
        strSQL.AppendLine(" Left join Personal OP3 on E.CompID = OP3.CompID and E.PersonPartOld = OP3.EmpID ")
        '第二人事管理員
        strSQL.AppendLine(" Left join Personal OP4 on E.CompID = OP4.CompID and E.SecPersonPartOld = OP4.EmpID ")
        '工作地點
        strSQL.AppendLine(" Left join WorkSite OW on E.CompID = OW.CompID and E.WorkSiteIDOld=OW.WorkSiteID ")
        '自行查核主管姓名
        strSQL.AppendLine(" Left join Personal OP5 on E.CompID = OP5.CompID and E.CheckPartOld = OP5.EmpID ")

        strSQL.AppendLine(" Where E.CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine(" And E.OrganID = " & Bsp.Utility.Quote(OrganID))
        If OrganReason <> "" Then
            strSQL.AppendLine(" And E.OrganReason = " & Bsp.Utility.Quote(OrganReason))
        End If
        If OrganType <> "" Then
            strSQL.AppendLine(" And E.OrganType = " & Bsp.Utility.Quote(OrganType))
        End If
        'Old參考OM2201 savedata的註解，這個的Old引數要顛倒，有Old代表要取後一筆資料，沒Old取前一筆
        Select Case ValidDateWhere
            Case ""
                strSQL.AppendLine(" And E.ValidDateB < " & Bsp.Utility.Quote(ValidDateB))
            Case "Old"
                strSQL.AppendLine(" And E.ValidDateB > " & Bsp.Utility.Quote(ValidDateB))
            Case Else
                strSQL.AppendLine(" And E.ValidDateB = " & Bsp.Utility.Quote(ValidDateB))
        End Select
        If Seq <> "" Then
            strSQL.AppendLine(" And E.Seq = " & Bsp.Utility.Quote(Seq))
        End If
        strSQL.AppendLine(" order by  E.Seq desc, E.ValidDateB ")
        If ValidDateWhere = "" Then
            strSQL.AppendLine(" desc ")
        End If
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
    'OM2200查詢
    Public Function OM2000OrganizationLog(ByVal CompID As String, ByVal OrganReason As String, ByVal OrganType As String, ByVal ValidDateB As String, ByVal OrganID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("SELECT L.CompID ")
        strSQL.AppendLine(",ValidDateE =Case When Convert(Char(10), L.ValidDateE, 111) = '1900/01/01' Then '' ELSE Convert(Char(10), L.ValidDateE, 111) End")
        strSQL.AppendLine(",OrganReason  = Case L.OrganReason When '1' Then '1-組織新增' When '2' Then '2-組織無效' When '3' Then '3-組織異動' End")
        strSQL.AppendLine(",L.Seq ")
        strSQL.AppendLine(" , ValidDateB = Case When Convert(Char(10), L.ValidDateB, 111) = '1900/01/01' Then '' ELSE Convert(Char(10), L.ValidDateB, 111) End")
        strSQL.AppendLine(", OrganType = Case L.OrganType When '1' Then '1-行政組織' When '2' Then '2-功能組織' When '3' Then '3-行政與功能組織' End")
        strSQL.AppendLine(",L.OrganID ")
        strSQL.AppendLine(",L.OrganNameOld as OrganNameOld")
        strSQL.AppendLine(",OP.NameN as BossOld")
        strSQL.AppendLine(",UpOrganIDOld = Case L.OrganType When '1' Then O2.OrganName  When '2' Then OF2.OrganName End ")
        strSQL.AppendLine(",L.OrganName as OrganName")
        strSQL.AppendLine(",P.NameN as Boss")
        strSQL.AppendLine(",UpOrganID  = Case L.OrganType When '1' Then O1.OrganName  When '2' Then OF1.OrganName End")

        strSQL.AppendLine(" FROM OrganizationLog L ")

        strSQL.AppendLine(" Left join Personal P on L.BossCompID = P.CompID and L.Boss = P.EmpID ")
        strSQL.AppendLine(" Left join Personal OP on L.BossCompIDOld = OP.CompID and L.BossOld = OP.EmpID ")
        strSQL.AppendLine(" Left join Organization O1 on L.CompID = O1.CompID and L.UpOrganID = O1.OrganID ")
        strSQL.AppendLine(" Left join OrganizationFlow OF1 on  L.UpOrganID = OF1.OrganID ")
        strSQL.AppendLine(" Left join Organization O2 on L.CompID = O2.CompID and L.UpOrganIDOld = O2.OrganID ")
        strSQL.AppendLine(" Left join OrganizationFlow OF2 on  L.UpOrganIDOld = OF2.OrganID ")

        strSQL.AppendLine(" Where L.CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine(" And L.OrganID = " & Bsp.Utility.Quote(OrganID))
        If OrganReason <> "" Then
            strSQL.AppendLine(" And L.OrganReason = " & Bsp.Utility.Quote(OrganReason))
        End If
        If OrganType <> "" Then
            strSQL.AppendLine(" And L.OrganType = " & Bsp.Utility.Quote(OrganType))
        End If
        If ValidDateB <> "" Then
            strSQL.AppendLine(" And L.ValidDateB = " & Bsp.Utility.Quote(ValidDateB))
        End If
        strSQL.AppendLine(" Order by ValidDateB desc ")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "OM1000[原OM2]"
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
#End Region

#Region "OM1001&1002[原OM2]"
#Region "照片Title"

    'OM1001&1002照片資訊，用於取得Rank、Title、OrganID
    Public Function GetOnePersonal(ByVal strSelect As String, ByVal CompID As String, ByVal EmpID As String) As String
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" Select isnull(" & strSelect & ",'') from Personal where  CompID=" & Bsp.Utility.Quote(CompID) & " and EmpID = " & Bsp.Utility.Quote(EmpID) & "")
        Return Bsp.DB.ExecuteScalar(strSQL.ToString, "eHRMSDB")
    End Function
    Public Function GetPersonal(ByVal strSelect As String, ByVal CompID As String, ByVal EmpID As String) As String
        Dim strSQL As New StringBuilder()
        Dim strSelect2 As String = strSelect
        strSelect2 = Replace(strSelect2, " ", "")
        If strSelect2 = "TitleID" Or strSelect2 = "RankID" Then
            strSelect2 = " P." & strSelect & " "
        Else
            strSelect2 = " " & strSelect2 & " "
        End If
        strSQL.AppendLine(" Select isnull(Case (select count (EmpID) from Personal  where  CompID=" & Bsp.Utility.Quote(CompID) & " and EmpID = " & Bsp.Utility.Quote(EmpID) & ") When 0 Then (select  " & strSelect2 & " from Personal  P inner join Title T on T.TitleID=P.TitleID and P.RankID=T.RankID and P.CompID = T.CompID where  P.EmpID = " & Bsp.Utility.Quote(EmpID) & ")   else (select  " & strSelect2 & " from Personal P inner join Title T on T.TitleID=P.TitleID and P.RankID=T.RankID and P.CompID = T.CompID where  P.CompID=" & Bsp.Utility.Quote(CompID) & " and P.EmpID = " & Bsp.Utility.Quote(EmpID) & ") End,'') ")

        Return Bsp.DB.ExecuteScalar(strSQL.ToString, "eHRMSDB")
    End Function

    '用於OM1001&1002照片取得上階部門 主管Name
    Public Function GetEmpName(ByVal CompID As String, ByVal EmpID As String) As String
        Dim strSQL As String
        strSQL = "Select isnull(NameN,'') From Personal"
        strSQL += " Where 1=1 "
        If CompID <> "" Then
            strSQL += " And CompID =  " & Bsp.Utility.Quote(CompID)
        End If
        strSQL += " And EmpID =  " & Bsp.Utility.Quote(EmpID)

        Return Bsp.DB.ExecuteScalar(strSQL, "eHRMSDB")
    End Function
    '用於 OM1001&1002 照片取得 主管部門&上階部門 Name
    Public Function GetOrganName(ByVal OrganType As String, ByVal CompID As String, ByVal OrganID As String) As String
        Dim strSQL As New StringBuilder()
        If OrganType = "2" Then
            strSQL.AppendLine("select isnull(OL.OrganName,'') From OrganizationFlow OL  Where 1=1")
            strSQL.AppendLine("  And OL.OrganID = " & Bsp.Utility.Quote(OrganID) & "")
        Else
            strSQL.AppendLine("select isnull(O.OrganName,'') From Organization O  Where 1=1")
            If CompID <> "" Then
                strSQL.AppendLine("  And O.CompID = " & Bsp.Utility.Quote(CompID))
            End If
            strSQL.AppendLine("  And O.OrganID = " & Bsp.Utility.Quote(OrganID) & " ")
        End If
        Return Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")
    End Function
    'Public Function GetOrganName(ByVal CompID As String, ByVal OrganID As String) As String
    '    Dim strSQL As New StringBuilder()
    '    strSQL.AppendLine("Select distinct isnull(")
    '    strSQL.AppendLine("(select O.OrganName From Organization O  Where 1=1")
    '    If CompID <> "" Then
    '        strSQL.AppendLine("  And O.CompID = " & Bsp.Utility.Quote(CompID))
    '    End If
    '    strSQL.AppendLine("  And O.OrganID = " & Bsp.Utility.Quote(OrganID) & " ),")
    '    strSQL.AppendLine("(select OrganName from OrganizationFlow OL where OL.OrganID = " & Bsp.Utility.Quote(OrganID) & " ))")

    '    '/*==================
    '    'strSQL.AppendLine("Select distinct isnull(O.OrganName,OL.OrganName) as OrganName From Organization O inner join OrganizationFlow OL on O.OrganID=OL.OrganID")
    '    'strSQL.AppendLine("Where 1=1 ")
    '    'If CompID <> "" Then
    '    '    strSQL.AppendLine("And O.CompID = " & Bsp.Utility.Quote(CompID))
    '    'End If
    '    'strSQL.AppendLine(" And O.OrganID = " & Bsp.Utility.Quote(OrganID))
    '    '=====================*/
    '    Return Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")
    'End Function
    '取得上階部門主管ID
    Public Function UpOrganEmpID(ByVal OrganType As String, ByVal CompID As String, ByVal UpOrganID As String) As String
        Dim strSQL As String
        If OrganType = "2" Then
            strSQL = "select Boss from OrganizationFlow where OrganID=" & Bsp.Utility.Quote(UpOrganID)
        Else
            strSQL = "select Boss From Organization Where OrganID=" & Bsp.Utility.Quote(UpOrganID) & " and CompID = " & Bsp.Utility.Quote(CompID)
        End If
        Return Bsp.DB.ExecuteScalar(strSQL, "eHRMSDB")
    End Function
    'Public Function UpOrganEmpID(ByVal UpOrganID As String) As String
    '    Dim strSQL As String
    '    strSQL = "select isnull((select Boss From Organization Where OrganID=" & Bsp.Utility.Quote(UpOrganID) & "),(select Boss from OrganizationFlow where OrganID=" & Bsp.Utility.Quote(UpOrganID) & "))"
    '    Return Bsp.DB.ExecuteScalar(strSQL, "eHRMSDB")
    'End Function

    'Public Function GetPersonalWaste(ByVal strSelect As String, ByVal CompID As String, ByVal EmpID As String) As String
    '    Dim strSQL As New StringBuilder()
    '    strSQL.AppendLine("  Select " & strSelect & "  From Personal ")
    '    strSQL.AppendLine(" Where 1=1 ")
    '    If CompID <> "" Then
    '        strSQL.AppendLine(" And CompID =  " & Bsp.Utility.Quote(CompID))
    '    End If
    '    strSQL.AppendLine(" And EmpID =  " & Bsp.Utility.Quote(EmpID))
    '    Return Bsp.DB.ExecuteScalar(strSQL.ToString, "eHRMSDB")
    'End Function

    'Public Function GetTitle(ByVal CompID As String, ByVal EmpID As String) As String
    '    Dim strSQL As New StringBuilder()
    '    strSQL.AppendLine("Select TitleName  From Personal P ")
    '    strSQL.AppendLine("inner join Title T on T.TitleID=P.TitleID and P.RankID=T.RankID ")
    '    strSQL.AppendLine(" Where P.CompID =  " & Bsp.Utility.Quote(CompID))
    '    strSQL.AppendLine(" And EmpID =  " & Bsp.Utility.Quote(EmpID))
    '    Return Bsp.DB.ExecuteScalar(strSQL.ToString, "eHRMSDB")
    'End Function

    'Public Function GetRank(ByVal CompID As String, ByVal EmpID As String) As String
    '    Dim strSQL As New StringBuilder()
    '    strSQL.AppendLine("Select P.RankID From Personal ")
    '    strSQL.AppendLine(" Where CompID =  " & Bsp.Utility.Quote(CompID))
    '    strSQL.AppendLine(" And EmpID =  " & Bsp.Utility.Quote(EmpID))
    '    Return Bsp.DB.ExecuteScalar(strSQL.ToString, "eHRMSDB")
    'End Function
#End Region

#Region "人員待異動檢查"
    Public Function EmployeeWaitCheck(ByVal strTable As String, ByVal strWhere As String, ByVal strJoin As String) As Boolean
        Dim strSQL As String, strSQL2 As String
        strSQL = "Select Count(*) Cnt From " & strTable
        strSQL2 = strSQL
        strSQL += " " & strJoin
        strSQL += " Where 1 = 1 " & strWhere
        strSQL2 += " Where 1 = 1 " & strWhere
        Return IIf(Bsp.DB.ExecuteScalar(strSQL, "eHRMSDB") = Bsp.DB.ExecuteScalar(strSQL2, "eHRMSDB"), True, False)
    End Function
#End Region

#Region "OM1001、OM1002 資料載入"
    Public Function QueryOrganizationByDetail(ByVal strFrom As String, ByVal CompID As String, ByVal OrganReason As String, ByVal OrganType As String, ByVal ValidDate As String, ByVal OrganID As String) As DataTable
        Dim strSQL As New StringBuilder()
        Dim strFieldNames As New StringBuilder()
        strFieldNames.AppendLine(" E.*, P.NameN as PNameN, P2.NameN as P2NameN")
        If strFrom <> "OrganizationFlow" Then
            strFieldNames.AppendLine(" , P3.NameN as P3NameN, P4.NameN as P4NameN, W.WorkSiteID, W.Remark, P5.NameN as P5NameN  ")
        End If

        strSQL.AppendLine("Select" & strFieldNames.ToString)
        strSQL.AppendLine("From " & strFrom & " E")
        '部門主管
        strSQL.AppendLine(" Left join Personal P on E.BossCompID = P.CompID and E.Boss = P.EmpID ")
        '副主管
        strSQL.AppendLine(" Left join Personal P2 on E.SecBossCompID = P2.CompID and E.SecBoss = P2.EmpID ")
        If strFrom <> "OrganizationFlow" Then
            '人事管理員
            strSQL.AppendLine(" Left join Personal P3 on E.CompID = P3.CompID and E.PersonPart = P3.EmpID ")
            '第二人事管理員
            strSQL.AppendLine(" Left join Personal P4 on E.CompID = P4.CompID and E.SecPersonPart = P4.EmpID ")
            '工作地點
            strSQL.AppendLine(" Left join WorkSite W on E.CompID = W.CompID and E.WorkSiteID=W.WorkSiteID ")
            '自行查核主管姓名
            strSQL.AppendLine(" Left join Personal P5 on E.CompID = P5.CompID and E.CheckPart = P5.EmpID ")
        End If
        strSQL.AppendLine("Where 1 = 1")
        If CompID <> "" Then
            strSQL.AppendLine("And E.CompID = " & Bsp.Utility.Quote(CompID))
        End If
        'If OrganReason <> "" And strFrom = "OrganizationWait" Then
        '    strSQL.AppendLine("And E.OrganReason = " & Bsp.Utility.Quote(OrganReason))
        'End If
        If OrganType <> "" Then
            strSQL.AppendLine("And E.OrganType = " & Bsp.Utility.Quote(OrganType))
        End If
        If ValidDate <> "" And strFrom = "OrganizationWait" Then
            strSQL.AppendLine("And E.ValidDate < " & Bsp.Utility.Quote(ValidDate))
        End If
        strSQL.AppendLine("And E.OrganID = " & Bsp.Utility.Quote(OrganID))

        If strFrom = "OrganizationWait" Then
            strSQL.AppendLine(" order by E.ValidDate desc ")
        End If

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
    'OM1002查詢用GetSelectData
    Public Function QueryOrganizationWaitByDetail(ByVal CompID As String, ByVal OrganReason As String, ByVal OrganType As String, ByVal ValidDate As String, ByVal OrganID As String, ByVal Seq As String) As DataTable
        Dim strSQL As New StringBuilder()
        Dim strFieldNames As New StringBuilder()
        strFieldNames.AppendLine(" E.*, P.NameN as PNameN, P2.NameN as P2NameN, P3.NameN as P3NameN, P4.NameN as P4NameN, W.WorkSiteID, W.Remark, P5.NameN as P5NameN  ")

        strSQL.AppendLine("Select" & strFieldNames.ToString)
        strSQL.AppendLine("From OrganizationWait E")
        '部門主管
        strSQL.AppendLine(" Left join Personal P on E.BossCompID = P.CompID and E.Boss = P.EmpID ")
        '副主管
        strSQL.AppendLine(" Left join Personal P2 on E.SecBossCompID = P2.CompID and E.SecBoss = P2.EmpID ")
        '人事管理員
        strSQL.AppendLine(" Left join Personal P3 on E.CompID = P3.CompID and E.PersonPart = P3.EmpID ")
        '第二人事管理員
        strSQL.AppendLine(" Left join Personal P4 on E.CompID = P4.CompID and E.SecPersonPart = P4.EmpID ")
        '工作地點
        strSQL.AppendLine(" Left join WorkSite W on E.CompID = W.CompID and E.WorkSiteID=W.WorkSiteID ")
        '自行查核主管姓名
        strSQL.AppendLine(" Left join Personal P5 on E.CompID = P5.CompID and E.CheckPart = P5.EmpID ")

        strSQL.AppendLine("Where 1 = 1")
        strSQL.AppendLine("And E.CompID = " & Bsp.Utility.Quote(CompID))

        If OrganReason <> "" Then
            strSQL.AppendLine("And E.OrganReason = " & Bsp.Utility.Quote(OrganReason))
        End If
        If OrganType <> "" Then
            strSQL.AppendLine("And E.OrganType = " & Bsp.Utility.Quote(OrganType))
        End If
        If ValidDate <> "" Then
            strSQL.AppendLine("And E.ValidDate = " & Bsp.Utility.Quote(ValidDate))
        End If

        strSQL.AppendLine("And E.OrganID = " & Bsp.Utility.Quote(OrganID))
        If Seq <> "" Then
            strSQL.AppendLine("And E.Seq = " & Bsp.Utility.Quote(Seq))
        End If

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

    '/*=======================================*/
#Region "增加Orderby與Join下拉選單"
    Enum DisplayType
        OnlyName    '只顯示名字
        OnlyID      '顯示ID  
        Full        '顯示ID + 名字
    End Enum
    Public Shared Sub FillDDLOM1000(ByVal objDDL As DropDownList, ByVal strTabName As String, ByVal strValue As String, ByVal strText As String, ByVal str3rdOrder As String, Optional ByVal Type As DisplayType = DisplayType.Full, Optional ByVal JoinStr As String = "", Optional ByVal WhereStr As String = "", Optional ByVal OrderByStr As String = "")
        Dim objOM1 As New OM1()
        Try
            Using dt As DataTable = objOM1.GetDDLInfoOM1000(strTabName, strValue, strText, str3rdOrder, JoinStr, WhereStr, OrderByStr)
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
    Public Function GetDDLInfoOM1000(ByVal strTabName As String, ByVal strValue As String, ByVal strText As String, ByVal str3rdOrder As String, Optional ByVal JoinStr As String = "", Optional ByVal WhereStr As String = "", Optional ByVal OrderByStr As String = "") As DataTable
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

#Region "OM1001 OM1002 單獨getName、getSeq"
    Public Function GetSeq(ByVal strFrom As String, ByVal CompID As String, ByVal OrganReason As String, ByVal OrganType As String, ByVal ValidDate As String, ByVal OrganID As String) As String
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine("Select  ISNULL(MAX(Seq), 0)+1 From " & strFrom)
        strSQL.AppendLine(" Where CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine(" and OrganReason = " & Bsp.Utility.Quote(OrganReason))
        strSQL.AppendLine(" and OrganType = " & Bsp.Utility.Quote(OrganType))
        strSQL.AppendLine(" and ValidDate = " & Bsp.Utility.Quote(ValidDate))
        strSQL.AppendLine(" and OrganID = " & Bsp.Utility.Quote(OrganID))
        Return IIf(IsNothing(Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")), "", Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB").ToString)
    End Function

    Public Function GetCompName(ByVal CompName As String) As String
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine("Select distinct isnull(CompName,'') From Company ")
        strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(CompName))
        Return Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")
    End Function
#End Region


#Region "OM1001insert OM1002 Update"
#Region "⑨insert" 'gencode 與 6Key都是Insert用
    Public Function InsertOWAddition(ByVal beOrganizationWait As beOrganizationWait.Row, ByVal CompID As String, ByVal OrganReason As String, ByVal OrganType As String, ByVal ValidDate As String, ByVal OrganID As String, ByVal OrganName As String, ByVal Seq As String, ByVal PositionID() As String, ByVal WorkTypeID() As String) As Boolean

        Dim bsOrganizationWait As New beOrganizationWait.Service()

        Dim beHRCodeMap As New beHRCodeMap.Row()
        Dim bsHRCodeMap As New beHRCodeMap.Service()

        Dim beOrgWorkTypeWait As New beOrgWorkTypeWait.Row()
        Dim bsOrgWorkTypeWait As New beOrgWorkTypeWait.Service()
        Dim beOrgPositionWait As New beOrgPositionWait.Row()
        Dim bsOrgPositionWait As New beOrgPositionWait.Service()

        Dim strSQL As New StringBuilder()
        With beHRCodeMap
            .TabName.Value = ""
            .FldName.Value = CompID
            .Code.Value = OrganID
            .CodeCName.Value = OrganName
            .LastChgComp.Value = UserProfile.ActCompID
            .LastChgID.Value = UserProfile.ActUserID
            .LastChgDate.Value = Now
        End With
        'beOrgWorkTypeWait的gencode取得值
        With beOrgWorkTypeWait
            .CompID.Value = CompID
            .OrganReason.Value = OrganReason
            .OrganType.Value = OrganType
            .ValidDate.Value = ValidDate
            .Seq.Value = Seq
            .OrganID.Value = OrganID
            .WorkTypeID.Value = WorkTypeID(0)
            .PrincipalFlag.Value = "1"
            .WaitStatus.Value = "0"
            .LastChgComp.Value = UserProfile.ActCompID
            .LastChgID.Value = UserProfile.ActUserID
            .LastChgDate.Value = Now
        End With
        'beOrgPositionWait的gencode取得值
        With beOrgPositionWait
            .CompID.Value = CompID
            .OrganReason.Value = OrganReason
            .OrganType.Value = OrganType
            .ValidDate.Value = ValidDate
            .Seq.Value = Seq
            .OrganID.Value = OrganID
            .PositionID.Value = PositionID(0)
            .PrincipalFlag.Value = "1"
            .WaitStatus.Value = "0"
            .LastChgComp.Value = UserProfile.ActCompID
            .LastChgID.Value = UserProfile.ActUserID
            .LastChgDate.Value = Now
        End With


        '/*-----------------------------*/

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Try
                If bsOrganizationWait.Insert(beOrganizationWait, tran) = 0 Then Return False
                'HRCodeMap
                Select Case OrganReason
                    Case "1"
                        If OrganType = "1" Or OrganType = "3" Then
                            beHRCodeMap.TabName.Value = "Organization_OrgType"
                            If bsHRCodeMap.Insert(beHRCodeMap, tran) = 0 Then Return False
                        End If
                        If OrganType = "2" Or OrganType = "3" Then
                            beHRCodeMap.TabName.Value = "OrganizationFlow_OrgType"
                            If bsHRCodeMap.Insert(beHRCodeMap, tran) = 0 Then Return False
                        End If
                    Case "2"
                        If OrganType = "1" Or OrganType = "3" Then
                            beHRCodeMap.TabName.Value = "Organization_OrgType"
                            If bsHRCodeMap.DeleteRowByPrimaryKey(beHRCodeMap, tran) = 0 Then Return False
                        End If
                        If OrganType = "2" Or OrganType = "3" Then
                            beHRCodeMap.TabName.Value = "OrganizationFlow_OrgType"
                            If bsHRCodeMap.DeleteRowByPrimaryKey(beHRCodeMap, tran) = 0 Then Return False
                        End If
                    Case "3"
                        If OrganType = "1" Or OrganType = "3" Then
                            beHRCodeMap.TabName.Value = "Organization_OrgType"
                            If bsHRCodeMap.Update(beHRCodeMap, tran) = 0 Then
                                If bsHRCodeMap.Insert(beHRCodeMap, tran) = 0 Then Return False
                            End If
                        End If
                        If OrganType = "2" Or OrganType = "3" Then
                            beHRCodeMap.TabName.Value = "OrganizationFlow_OrgType"
                            If bsHRCodeMap.Update(beHRCodeMap, tran) = 0 Then
                                If bsHRCodeMap.Insert(beHRCodeMap, tran) = 0 Then Return False
                            End If
                        End If
                End Select
                'HRCodeMap End

                If OrganType <> "2" Then
                    If bsOrgWorkTypeWait.IsDataExists(beOrgWorkTypeWait, tran) Then  ''''''<===========
                        Bsp.Utility.ShowFormatMessage(Me, "W_00010", "WorkTypeID重複")
                        Return False
                    End If
                    If bsOrgPositionWait.IsDataExists(beOrgPositionWait, tran) Then
                        Bsp.Utility.ShowFormatMessage(Me, "W_00010", "PositionID重複")
                        Return False
                    End If
                    If OrganType <> "2" Then
                        beOrgWorkTypeWait.WorkTypeID.Value = WorkTypeID(0)
                        beOrgPositionWait.PositionID.Value = PositionID(0)
                        bsOrgWorkTypeWait.Insert(beOrgWorkTypeWait, tran)
                        bsOrgPositionWait.Insert(beOrgPositionWait, tran)

                        beOrgWorkTypeWait.PrincipalFlag.Value = "0"
                        beOrgPositionWait.PrincipalFlag.Value = "0"

                        For ii = 1 To PositionID.Length - 1
                            beOrgPositionWait.PositionID.Value = PositionID(ii)
                            bsOrgPositionWait.Insert(beOrgPositionWait, tran)
                        Next

                        For ii = 1 To WorkTypeID.Length - 1
                            beOrgWorkTypeWait.WorkTypeID.Value = WorkTypeID(ii)
                            bsOrgWorkTypeWait.Insert(beOrgWorkTypeWait, tran)
                        Next
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

#Region "⑨Update" 'gencode是insert
    'CompID OrganID PositionID() WorkTypeID()不分Old，剩下引數Old用於修改前資料做刪除or回復(gencode加2)，沒Old引數做新資料insert
    Public Function UpdateOWAddition(ByVal beOrganizationWait As beOrganizationWait.Row, ByVal CompID As String, ByVal OrganID As String, ByVal PositionID() As String, ByVal WorkTypeID() As String, ByVal Seq As String, ByVal SeqOld As String, ByVal ValidDate As String, ByVal ValidDateOld As String, ByVal OrganType As String, ByVal OrganTypeOld As String, ByVal OrganReason As String, ByVal OrganReasonOld As String, ByVal OrganName As String, ByVal OrganNameOld As String) As Boolean

        'insert
        Dim bsOrganizationWait As New beOrganizationWait.Service()
        Dim beHRCodeMap As New beHRCodeMap.Row()
        Dim bsHRCodeMap As New beHRCodeMap.Service()
        'delete
        Dim beOrganizationWait2 As New beOrganizationWait.Row()
        Dim bsOrganizationWait2 As New beOrganizationWait.Service()
        Dim beHRCodeMap2 As New beHRCodeMap.Row()
        Dim bsHRCodeMap2 As New beHRCodeMap.Service()

        Dim beOrgWorkTypeWait As New beOrgWorkTypeWait.Row()
        Dim bsOrgWorkTypeWait As New beOrgWorkTypeWait.Service()
        Dim beOrgPositionWait As New beOrgPositionWait.Row()
        Dim bsOrgPositionWait As New beOrgPositionWait.Service()

        Dim strSQL As New StringBuilder()
        'beOrganizationWait2
        With beOrganizationWait2
            .CompID.Value = CompID
            .OrganReason.Value = OrganReasonOld
            .OrganType.Value = OrganTypeOld
            .ValidDate.Value = ValidDateOld
            .Seq.Value = SeqOld
            .OrganID.Value = OrganID
        End With

        'beHRCodeMap2
        With beHRCodeMap2
            .TabName.Value = ""
            .FldName.Value = CompID
            .Code.Value = OrganID
            .CodeCName.Value = OrganNameOld
            .LastChgComp.Value = UserProfile.ActCompID
            .LastChgID.Value = UserProfile.ActUserID
            .LastChgDate.Value = Now
        End With
        'beHRCodeMap
        With beHRCodeMap
            .TabName.Value = ""
            .FldName.Value = CompID
            .Code.Value = OrganID
            .CodeCName.Value = OrganName
            .LastChgComp.Value = UserProfile.ActCompID
            .LastChgID.Value = UserProfile.ActUserID
            .LastChgDate.Value = Now
        End With

        'beOrgWorkTypeWait的gencode取得值
        With beOrgWorkTypeWait
            .CompID.Value = CompID
            .OrganReason.Value = OrganReason
            .OrganType.Value = OrganType
            .ValidDate.Value = ValidDate
            .Seq.Value = Seq
            .OrganID.Value = OrganID
            .WorkTypeID.Value = WorkTypeID(0)
            .PrincipalFlag.Value = "1"
            .WaitStatus.Value = "0"
            .LastChgComp.Value = UserProfile.ActCompID
            .LastChgID.Value = UserProfile.ActUserID
            .LastChgDate.Value = Now
        End With
        'beOrgPositionWait的gencode取得值
        With beOrgPositionWait
            .CompID.Value = CompID
            .OrganReason.Value = OrganReason
            .OrganType.Value = OrganType
            .ValidDate.Value = ValidDate
            .Seq.Value = Seq
            .OrganID.Value = OrganID
            .PositionID.Value = PositionID(0)
            .PrincipalFlag.Value = "1"
            .WaitStatus.Value = "0"
            .LastChgComp.Value = UserProfile.ActCompID
            .LastChgID.Value = UserProfile.ActUserID
            .LastChgDate.Value = Now
        End With


        '/*-----------------------------*/

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Try
                If bsOrganizationWait2.DeleteRowByPrimaryKey(beOrganizationWait2, tran) = 0 Then Return False
                If bsOrganizationWait.Insert(beOrganizationWait, tran) = 0 Then Return False
                'HRCodeMap修改前
                Select Case OrganReasonOld
                    Case "1"
                        '新增->刪除
                        If OrganTypeOld = "1" Or OrganTypeOld = "3" Then
                            beHRCodeMap2.TabName.Value = "Organization_OrgType"
                            bsHRCodeMap2.DeleteRowByPrimaryKey(beHRCodeMap2, tran)
                        End If
                        If OrganTypeOld = "2" Or OrganTypeOld = "3" Then
                            beHRCodeMap2.TabName.Value = "OrganizationFlow_OrgType"
                            bsHRCodeMap2.DeleteRowByPrimaryKey(beHRCodeMap2, tran)
                        End If
                    Case "2"
                        '刪除->新增
                        If OrganTypeOld = "1" Or OrganTypeOld = "3" Then
                            beHRCodeMap.TabName.Value = "Organization_OrgType"
                            If bsHRCodeMap.Insert(beHRCodeMap, tran) = 0 Then Return False
                        End If
                        If OrganTypeOld = "2" Or OrganTypeOld = "3" Then
                            beHRCodeMap.TabName.Value = "OrganizationFlow_OrgType"
                            If bsHRCodeMap.Insert(beHRCodeMap, tran) = 0 Then Return False
                        End If
                    Case "3"
                        If OrganTypeOld = "1" Or OrganTypeOld = "3" Then
                            beHRCodeMap.TabName.Value = "Organization_OrgType"
                            If bsHRCodeMap.Update(beHRCodeMap, tran) = 0 Then Return False
                        End If
                        If OrganTypeOld = "2" Or OrganTypeOld = "3" Then
                            beHRCodeMap.TabName.Value = "OrganizationFlow_OrgType"
                            If bsHRCodeMap.Update(beHRCodeMap, tran) = 0 Then Return False
                        End If
                End Select
                '/*====================================================*/
                'HRCodeMap修改後
                Select Case OrganReason
                    Case "1"
                        If OrganType = "1" Or OrganType = "3" Then
                            beHRCodeMap.TabName.Value = "Organization_OrgType"
                            If bsHRCodeMap.Insert(beHRCodeMap, tran) = 0 Then Return False
                        End If
                        If OrganType = "2" Or OrganType = "3" Then
                            beHRCodeMap.TabName.Value = "OrganizationFlow_OrgType"
                            If bsHRCodeMap.Insert(beHRCodeMap, tran) = 0 Then Return False
                        End If
                    Case "2"
                        If OrganType = "1" Or OrganType = "3" Then
                            beHRCodeMap.TabName.Value = "Organization_OrgType"
                            If bsHRCodeMap.DeleteRowByPrimaryKey(beHRCodeMap, tran) = 0 Then Return False
                        End If
                        If OrganType = "2" Or OrganType = "3" Then
                            beHRCodeMap.TabName.Value = "OrganizationFlow_OrgType"
                            If bsHRCodeMap.DeleteRowByPrimaryKey(beHRCodeMap, tran) = 0 Then Return False
                        End If
                    Case "3"
                        If OrganType = "1" Or OrganType = "3" Then
                            beHRCodeMap.TabName.Value = "Organization_OrgType"
                            If bsHRCodeMap.Update(beHRCodeMap, tran) = 0 Then Return False
                        End If
                        If OrganType = "2" Or OrganType = "3" Then
                            beHRCodeMap.TabName.Value = "OrganizationFlow_OrgType"
                            If bsHRCodeMap.Update(beHRCodeMap, tran) = 0 Then Return False
                        End If
                End Select

                'WorkType&Position修改前
                strSQL.AppendLine(" Delete from OrgWorkTypeWait ")
                strSQL.AppendLine(" where CompID = " & Bsp.Utility.Quote(CompID))
                strSQL.AppendLine(" and OrganReason = " & Bsp.Utility.Quote(OrganReasonOld))
                strSQL.AppendLine(" and OrganType = " & Bsp.Utility.Quote(OrganTypeOld))
                strSQL.AppendLine(" and ValidDate = " & Bsp.Utility.Quote(ValidDateOld))
                strSQL.AppendLine(" and Seq = " & Bsp.Utility.Quote(SeqOld))
                strSQL.AppendLine(" and OrganID =" & Bsp.Utility.Quote(OrganID) & "; ")
                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran, "eHRMSDB")
                strSQL.Clear()
                strSQL.AppendLine(" Delete from OrgPositionWait ")
                strSQL.AppendLine(" where CompID = " & Bsp.Utility.Quote(CompID))
                strSQL.AppendLine(" and OrganReason = " & Bsp.Utility.Quote(OrganReasonOld))
                strSQL.AppendLine(" and OrganType = " & Bsp.Utility.Quote(OrganTypeOld))
                strSQL.AppendLine(" and ValidDate = " & Bsp.Utility.Quote(ValidDateOld))
                strSQL.AppendLine(" and Seq = " & Bsp.Utility.Quote(SeqOld))
                strSQL.AppendLine(" and OrganID =" & Bsp.Utility.Quote(OrganID) & "; ")
                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran, "eHRMSDB")

                'If OrganReason = "1" And OrganType <> "2" Then '組織新增 且是 行政or行政與功能
                If OrganReason <> "2" And OrganType <> "2" Then '組織新增 且是 行政or行政與功能
                    'WorkType&Position修改前
                    If PositionID.Length <> 0 Then
                        beOrgPositionWait.PositionID.Value = PositionID(0)
                        bsOrgPositionWait.Insert(beOrgPositionWait, tran)

                        If PositionID.Length > 1 Then
                            beOrgPositionWait.PrincipalFlag.Value = "0"
                            For ii = 1 To PositionID.Length - 1
                                beOrgPositionWait.PositionID.Value = PositionID(ii)
                                bsOrgPositionWait.Insert(beOrgPositionWait, tran)
                            Next
                        End If
                    End If

                    If WorkTypeID.Length <> 0 Then
                        beOrgWorkTypeWait.WorkTypeID.Value = WorkTypeID(0)
                        bsOrgWorkTypeWait.Insert(beOrgWorkTypeWait, tran)

                        If WorkTypeID.Length > 1 Then
                            beOrgWorkTypeWait.PrincipalFlag.Value = "0"
                            For ii = 1 To WorkTypeID.Length - 1
                                beOrgWorkTypeWait.WorkTypeID.Value = WorkTypeID(ii)
                                bsOrgWorkTypeWait.Insert(beOrgWorkTypeWait, tran)
                            Next
                        End If
                    End If

                End If ' '組織新增 且是 行政or行政與功能
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

#Region "普通的FillDDL"
    Public Shared Sub FillDDL(ByVal objDDL As DropDownList, ByVal strTabName As String, ByVal strValue As String, ByVal strText As String, Optional ByVal Type As DisplayType = DisplayType.Full, Optional ByVal JoinStr As String = "", Optional ByVal WhereStr As String = "", Optional ByVal OrderByStr As String = "")
        Dim objOM1 As New OM1()
        Try
            Using dt As DataTable = objOM1.GetDDLInfo(strTabName, strValue, strText, JoinStr, WhereStr, OrderByStr)
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
        If strText <> "" Then strSQL.AppendLine(", " & strText & " AS CodeName, " & strValue.Replace("distinct", "") & " + '-' + " & strText & " AS FullName ")
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
    '/*====================================================*/
#Region "OM1000 組織待異動資料維護[原OM1]"
#Region "執行Execute SP"
    Public Function Execute_OM1000(ByVal CompID As String, ByVal OrganID As String, ByVal OrganReason As String, ByVal OrganType As String, ByVal ValidDate As String, ByVal Seq As String, ByVal UserComp As String, ByVal UserID As String) As String

        Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
        Dim dbCommand As DbCommand = db.GetStoredProcCommand("SP_OrganizationWait")
        Dim ExecResult As Integer = 0

        db.AddInParameter(dbCommand, "@ExecType", DbType.String, "O")
        db.AddInParameter(dbCommand, "@CompID", DbType.String, CompID)
        db.AddInParameter(dbCommand, "@OrganID", DbType.String, OrganID)
        db.AddInParameter(dbCommand, "@OrganReason", DbType.String, OrganReason)
        db.AddInParameter(dbCommand, "@OrganType", DbType.String, OrganType)
        db.AddInParameter(dbCommand, "@ValidDate", DbType.DateTime, ValidDate)
        db.AddInParameter(dbCommand, "@Seq", DbType.String, Seq)
        db.AddInParameter(dbCommand, "@UserComp", DbType.String, UserComp)
        db.AddInParameter(dbCommand, "@UserID", DbType.String, UserID)
        db.AddInParameter(dbCommand, "@ExecResult", DbType.String, "0")
        db.ExecuteNonQuery(dbCommand)

        ExecResult = db.GetParameterValue(dbCommand, "@ExecResult")
        Return ExecResult
    End Function
#End Region

#Region "查詢"
    Public Function OM1000Query(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()
        'strSQL.AppendLine("Select distinct CompName From Company ")
        'strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(CompID))

        strSQL.AppendLine("Select  S.CompID ")
        strSQL.AppendLine(", CompName=Case CC.CompName When '' Then S.CompID  ELSE CC.CompName End ")
        '/*註<font color=''red''>2-功能組織</font> 這裡的 兩個單引號『''』前一個是為了給SQL顯示後一個單引號增加的，讀到VB裏頭會消失一個
        '若要用作比對資料須注意*/
        strSQL.AppendLine(", OrganType = Case S.OrganType When '1' Then '1-行政組織' When '2' Then '<font color=''red''>2-功能組織</font>' When '3' Then '<font color=''blue''>3-行政與功能組織</font>' End")
        strSQL.AppendLine(", WaitStatus = Case S.WaitStatus When'0' Then  '未生效' When '1'Then '已生效' End")
        strSQL.AppendLine(", S.OrganID")
        strSQL.AppendLine(", S.OrganName")
        strSQL.AppendLine(", OrganReason = Case S.OrganReason When '1' Then '1-組織新增' When '2' Then '2-組織無效' When '3' Then '3-組織異動' When '4' Then '4-組織更名' End")
        strSQL.AppendLine(", S.Seq")
        strSQL.AppendLine(", ValidDate = Case When Convert(Char(10), S.ValidDate, 111) = '1900/01/01' Then '' ELSE Convert(Char(10), S.ValidDate, 111) End")
        strSQL.AppendLine(", case when C.CompID = S.LastChgComp then C.CompID+'-'+C.CompName else S.LastChgComp end As LastChgComp ")
        strSQL.AppendLine(", case when P.CompID = S.LastChgComp then P.EmpID+'-'+P.NameN else S.LastChgID end As LastChgID ")
        strSQL.AppendLine(", LastChgDate = Case When Convert(Char(10), S.LastChgDate, 111) = '1900/01/01' Then '' ELSE Convert(Varchar, S.LastChgDate, 120) End")
        strSQL.AppendLine(", S.InValidFlag")
        strSQL.AppendLine(", S.VirtualFlag")
        strSQL.AppendLine(", S.OrganNameOld")
        strSQL.AppendLine(", S.UpOrganID")
        strSQL.AppendLine("From OrganizationWait S")
        strSQL.AppendLine("Left Join Company C on C.CompID = S.LastChgComp")
        strSQL.AppendLine("Left Join Company CC on CC.CompID = S.CompID")
        strSQL.AppendLine("Left Join Personal P on P.CompID = S.LastChgComp And P.EmpID = S.LastChgID")

        strSQL.AppendLine("Where 1 = 1")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine("And S.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "OrganType"
                        strSQL.AppendLine("And S.OrganType in (" & Bsp.Utility.Quote(ht(strKey).ToString()) & ",'3')")
                    Case "OrganID"
                        strSQL.AppendLine("And UPPER(S.OrganID) like UPPER('%" + ht(strKey).ToString() + "%') ")
                    Case "OrganName"
                        strSQL.AppendLine("And S.OrganName like '%" + ht(strKey).ToString() + "%' ")
                    Case "WaitStatus"
                        strSQL.AppendLine("And S.WaitStatus = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "OrganReason"
                        If ht(strKey).ToString() <> "4" Then
                            strSQL.AppendLine("And S.OrganReason = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        Else
                            strSQL.AppendLine("And S.OrganName != S.OrganNameOld ")
                        End If
                    Case "ValidDateB"
                        If Bsp.Utility.Quote(ht(strKey).ToString()) <> "" Then strSQL.AppendLine("And S.ValidDate >= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "ValidDateE"
                        If Bsp.Utility.Quote(ht(strKey).ToString()) <> "" Then strSQL.AppendLine("And S.ValidDate <= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next
        strSQL.AppendLine(" Order by S.InValidFlag,S.VirtualFlag,S.OrganID")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "新增"
    Public Function OM1000Add(ByVal OrganizationWait As beOrganizationWait.Row) As Boolean
        Dim bsOrganizationWait As New beOrganizationWait.Service

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsOrganizationWait.Insert(OrganizationWait, tran) = 0 Then Return False
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

#Region "修改"
    Public Function OM1000Update(ByVal OrganizationWait As beOrganizationWait.Row) As Boolean
        Dim bsOrganizationWait As New beOrganizationWait.Service

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsOrganizationWait.Update(OrganizationWait, tran) = 0 Then Return False
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

#Region "OM1000 OW+HRCodeMap刪除"
    Public Function OM1000Delete(ByVal beOrganizationWait As beOrganizationWait.Row, ByVal beHRCodeMap As beHRCodeMap.Row, ByVal OrganType As String, ByVal OrganReason As String) As Boolean
        Dim bsOrganizationWait As New beOrganizationWait.Service()
        Dim bsHRCodeMap As New beHRCodeMap.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsOrganizationWait.DeleteRowByPrimaryKey(beOrganizationWait, tran) = 0 Then Return False
                '還原HRCodeMap
                Select Case OrganReason
                    Case "1"  '新增->刪除
                        If OrganType = "1" Or OrganType = "3" Then
                            beHRCodeMap.TabName.Value = "Organization_OrgType"
                            If bsHRCodeMap.DeleteRowByPrimaryKey(beHRCodeMap, tran) = 0 Then Return False
                        End If
                        If OrganType = "2" Or OrganType = "3" Then
                            beHRCodeMap.TabName.Value = "OrganizationFlow_OrgType"
                            If bsHRCodeMap.DeleteRowByPrimaryKey(beHRCodeMap, tran) = 0 Then Return False
                        End If
                    Case "2"   '刪除(無效)->新增
                        If OrganType = "1" Or OrganType = "3" Then
                            beHRCodeMap.TabName.Value = "Organization_OrgType"
                            If bsHRCodeMap.Insert(beHRCodeMap, tran) = 0 Then Return False
                        End If
                        If OrganType = "2" Or OrganType = "3" Then
                            beHRCodeMap.TabName.Value = "OrganizationFlow_OrgType"
                            If bsHRCodeMap.Insert(beHRCodeMap, tran) = 0 Then Return False
                        End If
                    Case "3" '修改->修改(恢復成OrganNameOld並且最後異動時間&人員補上當下修改時間與使用者)

                        If OrganType = "1" Or OrganType = "3" Then
                            'If IsDataExists(" Organization ", " and InValidFlag='1' and OrganID =" & strOrganIDSQL) Then
                            If IsDataExists(" Organization ", " and InValidFlag='1' and OrganID ='" & beOrganizationWait.OrganID.Value & "'") Then
                                beHRCodeMap.TabName.Value = "Organization_OrgType"
                                If bsHRCodeMap.DeleteRowByPrimaryKey(beHRCodeMap, tran) = 0 Then Return False
                            Else
                                beHRCodeMap.TabName.Value = "Organization_OrgType"
                                If bsHRCodeMap.Update(beHRCodeMap, tran) = 0 Then Return False
                            End If

                        End If
                        If OrganType = "2" Or OrganType = "3" Then
                            If IsDataExists(" OrganizationFlow ", " and InValidFlag='1' and OrganID ='" & beOrganizationWait.OrganID.Value & "'") Then
                                beHRCodeMap.TabName.Value = "OrganizationFlow_OrgType"
                                If bsHRCodeMap.DeleteRowByPrimaryKey(beHRCodeMap, tran) = 0 Then Return False
                            Else
                                beHRCodeMap.TabName.Value = "OrganizationFlow_OrgType"
                                If bsHRCodeMap.Update(beHRCodeMap, tran) = 0 Then Return False
                            End If
                        End If
                End Select

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

#Region "下傳"
    '讀取FlowOrgan方便在VB裡做ID轉Name
    Public Function GetFlowOrganName(ByVal FlowOrgan As String) As String
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine("SELECT STUFF((SELECT  ','+OrganName FROM OrganizationFlow ")
        strSQL.AppendLine("Where 1=1 ")
        strSQL.AppendLine(" And OrganID in ( " & FlowOrgan & ")")
        strSQL.AppendLine(" for xml path('')),1,1,'')")
        Return IIf(IsDBNull(Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")), "", Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB"))
    End Function
    '下傳專用查詢
    Public Function QueryOrganizationWaitByDownload(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()
        Dim strFieldNames As New StringBuilder()
        Dim OrganType As String = ""
        Dim aryStr() As String

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine(" And E.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "OrganType"
                        strSQL.AppendLine(" And E.OrganType = " & Bsp.Utility.Quote(ht(strKey).ToString.Trim()))
                        OrganType = ht(strKey).ToString.Trim()
                    Case "OrganID"
                        strSQL.AppendLine(" And UPPER(E.OrganID) like UPPER('%" + ht(strKey).ToString() + "%') ")
                    Case "OrganName"
                        strSQL.AppendLine(" And E.OrganName = " & Bsp.Utility.Quote(ht(strKey).ToString.Trim()))

                    Case "WaitStatus"
                        strSQL.AppendLine(" And E.WaitStatus = " & Bsp.Utility.Quote(ht(strKey).ToString.Trim()))
                    Case "OrganReason"
                        strSQL.AppendLine(" And E.OrganReason = " & Bsp.Utility.Quote(ht(strKey).ToString.Trim()))
                    Case "ValidDateB"
                        If Bsp.Utility.Quote(ht(strKey).ToString()) <> "" Then strSQL.AppendLine(" And E.ValidDate >= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "ValidDateE"
                        If Bsp.Utility.Quote(ht(strKey).ToString()) <> "" Then strSQL.AppendLine(" And E.ValidDate <= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next

        '/*excel
        strFieldNames.AppendLine("isnull(C.CompName,'') as '公司名稱',")
        strFieldNames.AppendLine("Case E.OrganType When '1' Then '行政組織' When '2' Then '功能組織' When '3' Then '行政與功能組織' End as '待異動組織類型',")
        strFieldNames.AppendLine("Case E.OrganReason When '1' Then '組織新增' When '2' Then '組織無效' When '3' Then '組織異動' When '4' Then '組織更名' End as '異動原因',")
        strFieldNames.AppendLine("Convert(char(10),E.ValidDate,111) as '生效日期',")
        strFieldNames.AppendLine("E.OrganID as '部門代碼',")
        strFieldNames.AppendLine("E.OrganName as '部門名稱',")
        strFieldNames.AppendLine("E.OrganEngName as '部門英文名稱',")
        strFieldNames.AppendLine("Case E.InValidFlag When '1' Then '是' When '0' Then '否' End as '無效註記',")
        strFieldNames.AppendLine("Case E.VirtualFlag When '1' Then '是' When '0' Then '否' End as '虛擬部門',")
        strFieldNames.AppendLine("Case E.BranchFlag When '1' Then '是' When '0' Then '否' End as '分行註記',")
        strFieldNames.AppendLine("CM1.CodeCName as '單位類別',")
        strFieldNames.AppendLine("O1.OrganName as '所屬事業群',")
        strFieldNames.AppendLine("Case when E.OrganID=E.DeptID then E.OrganName else (Case E.OrganType When '2' Then O22.OrganName else O2.OrganName End)End as '上階部門',")
        strFieldNames.AppendLine("CM2.CodeCName as'事業群類別',")
        strFieldNames.AppendLine("Case when E.OrganID=E.DeptID then E.OrganName else (Case E.OrganType When '2' Then O32.OrganName else O3.OrganName End)End as'所屬一級部門',")
        strFieldNames.AppendLine("CM3.CodeCName as'部門主管角色',")
        strFieldNames.AppendLine("P.NameN as'部門主管',")
        strFieldNames.AppendLine("Case E.BossType When '1' Then '主要' When '2' Then '兼任' End as'主管任用方式',")
        strFieldNames.AppendLine("P2.NameN as'副主管',")
        strFieldNames.AppendLine("Case E.BossTemporary When '1' Then '是' When '0' Then '否' End as'主管暫代',")
        'If OrganType <> "2" Then
        strFieldNames.AppendLine("P3.NameN as'第一人事管理員',")
        strFieldNames.AppendLine("P4.NameN as'第二人事管理員',")
        strFieldNames.AppendLine("WS.Remark as'工作地點',")
        strFieldNames.AppendLine("P5.NameN as'自行查核主管',")
        strFieldNames.AppendLine("(select p.Remark  from OrgPositionWait as a2 left join Position p on a2.PositionID=p.PositionID and a2.CompID=p.CompID where E.CompID=a2.CompID and E.OrganReason=a2.OrganReason and E.OrganType=a2.OrganType and E.ValidDate=a2.ValidDate and E.Seq=a2.Seq and E.OrganID=a2.OrganID and PrincipalFlag='1') as '主要職位',")
        strFieldNames.AppendLine("STUFF((select ','+p.Remark  from OrgPositionWait as a3 left join Position p on a3.PositionID=p.PositionID and a3.CompID=p.CompID where E.CompID=a3.CompID and E.OrganReason=a3.OrganReason and E.OrganType=a3.OrganType and E.ValidDate=a3.ValidDate and E.Seq=a3.Seq and E.OrganID=a3.OrganID and PrincipalFlag='0' for xml path('')),1,1,'') as '兼任職位',")

        strFieldNames.AppendLine("Case when E.OrganID=E.DeptID then E.OrganName else (case O4.OrganName when '' then O42.CompName else O4.OrganName End)End as'費用分攤部門',")

        strFieldNames.AppendLine("(select w.Remark  from OrgWorkTypeWait as a4 left join WorkType w on a4.WorkTypeID=w.WorkTypeID and a4.CompID=w.CompID where E.CompID=a4.CompID and E.OrganReason=a4.OrganReason and E.OrganType=a4.OrganType and E.ValidDate=a4.ValidDate and E.Seq=a4.Seq and E.OrganID=a4.OrganID and PrincipalFlag='1') as '主要工作性質',")
        strFieldNames.AppendLine("STUFF((select ','+w.Remark    from OrgWorkTypeWait as a5 left join WorkType w on a5.WorkTypeID=w.WorkTypeID and a5.CompID=w.CompID where E.CompID=a5.CompID and E.OrganReason=a5.OrganReason and E.OrganType=a5.OrganType and E.ValidDate=a5.ValidDate and E.Seq=a5.Seq and E.OrganID=a5.OrganID and PrincipalFlag='0' for xml path('')),1,1,'') as '兼任工作性質',")
        strFieldNames.AppendLine("E.AccountBranch as'會計分行別',")
        strFieldNames.AppendLine("Case E.CostType When 'A' Then '管理-5821' When 'B' Then '業務-5811' End as'費用分攤科目別',")
        'End If
        'If OrganType <> "1" Then
        strFieldNames.AppendLine("E.FlowOrganID as'比對簽核單位',")
        strFieldNames.AppendLine("Case E.CompareFlag When '1' Then '是' When '0' Then '否' End as'內部比對單位註記',")
        strFieldNames.AppendLine("Case E.DelegateFlag When '1' Then '是' When '0' Then '否' End as'授權註記',")
        strFieldNames.AppendLine("Case E.OrganNo When '1' Then '是' When '0' Then '否' End as'處級單位註記',")
        strFieldNames.AppendLine("CM4.CodeCName as'業務類別',")
        'End If
        strFieldNames.AppendLine("E.LastChgComp as'最後異動公司',")
        strFieldNames.AppendLine("E.LastChgID as'最後異動人員',")
        strFieldNames.AppendLine("E.LastChgDate as'最後異動時間' ")

        '/*⑨*/
        Return GetOrganizationWaitInfoByDownload(strFieldNames.ToString(), strSQL.ToString())
        'Return GetOrganizationWaitInfoByDownload(, strSQL.ToString())
    End Function
    Public Function GetOrganizationWaitInfoByDownload(Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames = "" Then FieldNames = "*"

        strSQL.AppendLine("Select  " & FieldNames)
        strSQL.AppendLine(" From OrganizationWait E ")
        '公司名稱
        strSQL.AppendLine(" Left join Company C on E.CompID = C.CompID ")
        '單位類別
        strSQL.AppendLine(" Left join HRCodeMap CM1 on E.OrgType = CM1.Code and E.CompID=CM1.FldName and CM1.TabName=(case E.OrganType  when '2' then  'OrganizationFlow_OrgType'else 'Organization_OrgType' end )")
        '所屬事業群
        strSQL.AppendLine(" Left join OrganizationFlow O1 on E.GroupID = O1.OrganID ")
        '上階部門
        strSQL.AppendLine(" Left join Organization O2 on E.UpOrganID = O2.OrganID and  E.CompID = O2.CompID")
        '上階部門2
        strSQL.AppendLine(" Left join OrganizationFlow O22 on E.UpOrganID = O22.OrganID")
        '事業群類別
        strSQL.AppendLine(" Left join HRCodeMap CM2 on E.GroupType = CM2.Code and CM2.TabName='Organization'  and CM2.FldName= 'GroupType' and CM2.NotShowFlag='0'")
        '所屬一級部門
        strSQL.AppendLine(" Left join Organization O3 on E.DeptID = O3.OrganID and  E.CompID = O3.CompID")
        '所屬一級部門2
        strSQL.AppendLine(" Left join OrganizationFlow O32 on E.DeptID = O32.OrganID ")
        '部門主管角色
        strSQL.AppendLine(" Left join HRCodeMap CM3 on E.RoleCode = CM3.Code and CM3.TabName='Organization'  and CM3.FldName= 'RoleCode' and CM3.NotShowFlag = '0'")
        '工作地點
        strSQL.AppendLine(" Left join WorkSite WS on E.WorkSiteID = WS.WorkSiteID and  E.CompID = WS.CompID")
        '費用分攤部門
        strSQL.AppendLine(" Left join Organization O4 on E.CostDeptID = O4.OrganID and  E.CompID = O4.CompID")
        '費用分攤部門2
        strSQL.AppendLine(" Left join Company O42 on E.CostDeptID = O42.CompID and O42.FeeShareFlag ='1'")
        '業務類別
        strSQL.AppendLine(" Left join HRCodeMap CM4 on E.BusinessType =  RTrim(CM4.Code) and CM4.TabName='Business'  and CM4.FldName= 'BusinessType'")

        '部門主管
        strSQL.AppendLine(" Left join Personal P on E.BossCompID = P.CompID and E.Boss = P.EmpID ")
        '副主管
        strSQL.AppendLine(" Left join Personal P2 on E.SecBossCompID = P2.CompID and E.SecBoss = P2.EmpID ")
        '人事管理員
        strSQL.AppendLine(" Left join Personal P3 on E.CompID = P3.CompID and E.PersonPart = P3.EmpID ")
        '第二人事管理員
        strSQL.AppendLine(" Left join Personal P4 on E.CompID = P4.CompID and E.SecPersonPart = P4.EmpID ")
        '自行查核主管姓名
        strSQL.AppendLine(" Left join Personal P5 on E.CompID = P5.CompID and E.CheckPart = P5.EmpID ")

        strSQL.AppendLine("Where 1 = 1")
        If CondStr <> "" Then strSQL.AppendLine(CondStr)
        strSQL.AppendLine(" Order by  公司名稱")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region
#End Region
End Class
