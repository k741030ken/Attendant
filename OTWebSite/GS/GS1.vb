'****************************************************
'功能說明：
'建立人員：Micky Sung
'建立日期：2015.11.02
'****************************************************
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.IO
Imports System

Public Class GS1

#Region "讀取下一簽核主管EMail"
    Public Function GetSignEMail(ByVal CompID As String, ByVal GradeYear As String, ByVal GradeSeq As String, ByVal ApplyTime As String, ByVal ApplyID As String, ByVal Seq As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" Select S.SignIDComp,S.SignID,S.ApplyID,O.OrganName,isnull(EMail,'') as EMail,Par.OrderEndDate ")
        strSQL.AppendLine(" From SignLog S join Organization O on S.CompID=O.CompID and S.ApplyID=O.OrganID ")
        strSQL.AppendLine(" join Personal P on S.SignIDComp=P.CompID and S.SignID=P.EmpID")
        strSQL.AppendLine(" left join Communication C on P.IDNo = C.IDNo ")
        strSQL.AppendLine(" join GradeParameter Par on S.CompID=Par.CompID And Par.GradeSeq=" & Bsp.Utility.Quote(GradeSeq) & " And Par.GradeYear=" & Bsp.Utility.Quote(GradeYear))
        strSQL.AppendLine(" Where S.CompID=" & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine(" and S.ApplyTime=" & Bsp.Utility.Quote(ApplyTime))
        strSQL.AppendLine(" and S.ApplyID=" & Bsp.Utility.Quote(ApplyID))
        strSQL.AppendLine(" and S.Seq=" & Bsp.Utility.Quote(Seq + 1))
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)

    End Function
#End Region
#Region "取區處名稱"
    Public Function GetSignOrganName(ByVal CompID As String, ByVal ApplyID As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" Select OrganID,OrganName From Organization O ")
        strSQL.AppendLine(" Inner Join (Select CompID, UpOrganID From Organization ")
        strSQL.AppendLine(" Where CompID=" & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine(" and OrganID=" & Bsp.Utility.Quote(ApplyID) & ") O1")
        strSQL.AppendLine(" On O.CompID=O1.CompID and O.OrganID = O1.UpOrganID ")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

#End Region
    
#Region "ScoreValue：整體評量"
    Public Function GetScoreValueParameter(ByVal strCompID As String, ByVal strYear As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" Select isnull(FinalScore,'') as FinalScore,isnull(EvaluationSeq,0) as EvaluationSeq From EvaluationParameterH E ")
        strSQL.AppendLine(" Where CompID=" & Bsp.Utility.Quote(strCompID))
        strSQL.AppendLine(" and EvaluationYear=" & Bsp.Utility.Quote(strYear))
        strSQL.AppendLine(" and EvaluationSeq=(Select Max(EvaluationSeq) From EvaluationExec where CompID=E.CompID and EvaluationYear=E.EvaluationYear and ApplyType='30')")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)

    End Function
#End Region
#Region "檢查是否有整體評量調整人員"
    Public Function IsEmpComment_ScoreAdjust(ByVal CompID As String, ByVal ApplyID As String, ByVal ApplyTime As String, ByVal Seq As String, ByVal GradeYear As String, ByVal GradeSeq As String _
                                           , ByVal EvaluationSeq As String, ByVal MainFlag As String, ByVal DeptEx As String) As Boolean
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" Select Count(*) From  GradeBase G left join GradeSignLog GS on G.CompID=GS.CompID and G.GradeDeptID=GS.ApplyID and G.EmpID=GS.EmpID and GS.Seq=" & Seq & " And G.Online='1'")
        If MainFlag = "2" Then
            strSQL.AppendLine(" Left join GradeSignLog GS1 on G.CompID=GS1.CompID and G.GradeDeptID=GS1.ApplyID and G.EmpID=GS1.EmpID and GS1.Seq=" & Seq - 1 & " and GS1.ApplyTime=GS.ApplyTime")
        End If
        strSQL.AppendLine(" Where G.CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine(" And G.GradeYear = " & Bsp.Utility.Quote(GradeYear))
        strSQL.AppendLine(" And G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
        strSQL.AppendLine(" And GS.ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
        If MainFlag = "2" And DeptEx = "N" Then
            strSQL.AppendLine(" And G.UpOrganID =" & Bsp.Utility.Quote(ApplyID))
        Else
            strSQL.AppendLine(" And G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
        End If
        strSQL.AppendLine(" And G.GradeDeptID = G.OrderDeptID ")
        If MainFlag = "2" Then
            strSQL.AppendLine(" And (isnull(GS1.Score,'')<>'' and GS.Score<> isnull(GS1.Score,''))")
        Else
            strSQL.AppendLine(" And (GS.Score<>'' and GS.Score<> isnull((Select top 1 FinalScore From EvaluationSignLog Where CompID=G.CompID And EvaluationYear = " & Bsp.Utility.Quote(GradeYear) & " And EvaluationSeq = " & Bsp.Utility.Quote(EvaluationSeq) & " And ApplyID = G.EmpID And Result='1' And StepID='3' And IdentityID='4' order by Seq desc),'')")
            strSQL.AppendLine(" And isnull((Select top 1 FinalScore From EvaluationSignLog Where CompID=G.CompID And EvaluationYear = " & Bsp.Utility.Quote(GradeYear) & " And EvaluationSeq = " & Bsp.Utility.Quote(EvaluationSeq) & " And ApplyID = G.EmpID And Result='1' And StepID='3' And IdentityID='4' order by Seq desc),'')<>'')")
        End If
        
        Return IIf(Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB") > 0, True, False)
    End Function
#End Region
#Region "檢查是否可填寫考核補充說明"
    Public Function IsEmpComment(ByVal CompID As String, ByVal ApplyID As String, ByVal ApplyTime As String, ByVal Seq As String, ByVal GradeYear As String, ByVal GradeSeq As String _
                                           , ByVal EvaluationSeq As String, ByVal MainFlag As String, ByVal DeptEx As String) As Boolean
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(ConfigurationManager.AppSettings("eHRMSDBDES").ToString)
        strSQL.AppendLine(" Select Count(*) From  GradeBase G left join GradeSignLog GS on G.CompID=GS.CompID and G.GradeDeptID=GS.ApplyID and G.EmpID=GS.EmpID and GS.Seq=" & Seq & " And G.Online='1'")
        If MainFlag = "2" Then
            strSQL.AppendLine(" Left join GradeSignLog GS1 on G.CompID=GS1.CompID and G.GradeDeptID=GS1.ApplyID and G.EmpID=GS1.EmpID and GS1.Seq=" & Seq - 1)
        End If
        strSQL.AppendLine(" Where G.CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine(" And G.GradeYear = " & Bsp.Utility.Quote(GradeYear))
        strSQL.AppendLine(" And G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
        strSQL.AppendLine(" And GS.ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
        strSQL.AppendLine(" And GS.Seq = " & Bsp.Utility.Quote(Seq))
        If MainFlag = "2" And DeptEx = "N" Then
            strSQL.AppendLine(" And G.UpOrganID =" & Bsp.Utility.Quote(ApplyID))
        Else
            strSQL.AppendLine(" And G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
        End If
        strSQL.AppendLine(" And G.GradeDeptID = G.OrderDeptID ")
        strSQL.AppendLine(" And GS.Grade is not null and G.GradeAdjust is not null ")
        strSQL.AppendLine(" And (CONVERT(char(10), DecryptByKey(GS.Grade)) in ('1','3','4','9') or CONVERT(char(10), DecryptByKey(GS.Grade))<>CONVERT(char(10), DecryptByKey(G.GradeAdjust)))")
        Return IIf(Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB") > 0, True, False)
    End Function
#End Region
#Region "取整體評量調整說明及考核補充說明"
    Public Function GetEmpComment(ByVal CompID As String, ByVal ApplyID As String, ByVal ApplyTime As String, ByVal Seq As String, ByVal GradeYear As String, ByVal GradeSeq As String _
                                           , ByVal DeptEx As String, ByVal EmpID As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" Select GE.* From GradeBase G Left join (")
        strSQL.AppendLine(" Select GradeYear,ApplyTime,ApplyID,CompID,EmpID,Seq,GradeSeq,MainFlag,Comment,'1' as CommentType From GradeEmpComment")
        strSQL.AppendLine(" union")
        strSQL.AppendLine(" Select GradeYear,ApplyTime,ApplyID,CompID,EmpID,Seq,GradeSeq,MainFlag,Comment,'2' as CommentType From GradeEmpComment_ScoreAdjust")
        strSQL.AppendLine(" ) GE on GE.CompID= " & Bsp.Utility.Quote(CompID) & " and GE.ApplyTime= " & Bsp.Utility.Quote(ApplyTime) & " and GE.ApplyID= G.GradeDeptID and GE.Seq= " & Bsp.Utility.Quote(Seq - 1) & " and G.CompID =GE.CompID and G.EmpID = GE.EmpID and G.GradeSeq = GE.GradeSeq")
        strSQL.AppendLine(" where G.CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine(" and G.GradeYear = " & Bsp.Utility.Quote(GradeYear))
        strSQL.AppendLine(" and G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
        If DeptEx = "Y" Then
            strSQL.AppendLine(" and G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
        Else
            strSQL.AppendLine(" And G.UpOrganID =" & Bsp.Utility.Quote(ApplyID))
        End If
        strSQL.AppendLine(" And G.Online='1' and G.GradeDeptID = G.OrderDeptID and G.EmpID = " & Bsp.Utility.Quote(EmpID))
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)

    End Function
    Public Function GetEmpCommentByQuery(ByVal CompID As String, ByVal ApplyID As String, ByVal ApplyTime As String, ByVal Seq As String, ByVal GradeYear As String, ByVal GradeSeq As String _
                                           , ByVal DeptEx As String, ByVal EmpID As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" Select GE.* From GradeBase G Left join (")
        strSQL.AppendLine(" Select GE.GradeYear,GE.ApplyTime,GE.ApplyID,GE.CompID,GE.EmpID,GE.Seq,GE.GradeSeq,GE.MainFlag,GE.Comment,'1' as CommentType,S.SignID,SignIDComp From GradeEmpComment GE inner join SignLog S On GE.CompID=S.CompID and GE.ApplyID=S.ApplyID and GE.ApplyTime=S.ApplyTime and GE.Seq=S.Seq")
        strSQL.AppendLine(" union")
        strSQL.AppendLine(" Select GE.GradeYear,GE.ApplyTime,GE.ApplyID,GE.CompID,GE.EmpID,GE.Seq,GE.GradeSeq,GE.MainFlag,GE.Comment,'2' as CommentType,S.SignID,SignIDComp From GradeEmpComment_ScoreAdjust GE inner join SignLog S On GE.CompID=S.CompID and GE.ApplyID=S.ApplyID and GE.ApplyTime=S.ApplyTime and GE.Seq=S.Seq")
        strSQL.AppendLine(" ) GE on GE.CompID= " & Bsp.Utility.Quote(CompID) & " and GE.ApplyTime= " & Bsp.Utility.Quote(ApplyTime) & " and GE.ApplyID= G.GradeDeptID and G.CompID =GE.CompID and G.EmpID = GE.EmpID and G.GradeSeq = GE.GradeSeq")
        strSQL.AppendLine(" where G.CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine(" and G.GradeYear = " & Bsp.Utility.Quote(GradeYear))
        strSQL.AppendLine(" and G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))

        strSQL.AppendLine(" And G.Online='1' and G.GradeDeptID = G.OrderDeptID and G.EmpID = " & Bsp.Utility.Quote(EmpID))
        strSQL.AppendLine(" Order by GE.Seq")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)

    End Function
#Region "檢查考核補充說明是否已填寫完成"
    Public Function CheckEmpComment(ByVal CompID As String, ByVal ApplyID As String, ByVal ApplyTime As String, ByVal Seq As String, ByVal GradeYear As String, ByVal GradeSeq As String _
                                           , ByVal MainFlag As String, ByVal DeptEx As String, ByVal topDataCount As String, ByVal LastDataCount As String, ByVal IsSignNext As String, Optional EvaluationSeq As String = "") As Boolean
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(ConfigurationManager.AppSettings("eHRMSDBDES").ToString)
        strSQL.AppendLine("Select Count(*) From ( ")

        strSQL.AppendLine(" Select G.CompID,G.EmpID,P.NameN,GS.ApplyID ")
        strSQL.AppendLine(" ,GS.GradeOrder ")
        strSQL.AppendLine(" ,isnull(GE.Comment,'') as Comment ")
        strSQL.AppendLine(" ,Case When isnull(GE.Comment,'N')='N' Then 'N' Else 'Y' End as IsExit ")
        If MainFlag = "2" Then
            strSQL.AppendLine(" ,ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) as Grade1 ")
            strSQL.AppendLine(" ,ltrim(isnull(CONVERT(char(1), DecryptByKey(GS1.Grade)) ,'')) as Grade2 ")
        Else
            strSQL.AppendLine(" ,isnull((Select top 1 FinalScoreValue From EvaluationSignLog Where CompID=G.CompID And EvaluationYear = " & Bsp.Utility.Quote(GradeYear) & " And EvaluationSeq = " & Bsp.Utility.Quote(EvaluationSeq) & " And ApplyID = G.EmpID And Result='1' And StepID='3' And IdentityID='4' order by Seq desc),'') As Grade1")
            strSQL.AppendLine(" ,ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) as Grade2 ")
        End If
        strSQL.AppendLine(" From  GradeBase G left join GradeSignLog GS on G.CompID=GS.CompID and G.GradeDeptID=GS.ApplyID and G.EmpID=GS.EmpID and GS.Seq=" & Seq & " And G.Online='1'")
        strSQL.AppendLine(" Left Join Personal P on G.CompID = P.CompID and G.EmpID = P.EmpID ")
        If MainFlag = "2" Then
            strSQL.AppendLine(" Left join GradeSignLog GS1 on G.CompID=GS1.CompID and G.GradeDeptID=GS1.ApplyID and G.EmpID=GS1.EmpID and GS1.Seq=" & Seq - 1)
        End If
        strSQL.AppendLine(" Left join GradeEmpComment GE on GS.CompID=GE.CompID and GS.ApplyTime=GE.ApplyTime and GS.ApplyID=GE.ApplyID and GS.EmpID=GE.EmpID and GS.Seq=GE.Seq and GS.GradeSeq=GE.GradeSeq ")
        strSQL.AppendLine(" Where G.CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine(" And G.GradeYear = " & Bsp.Utility.Quote(GradeYear))
        strSQL.AppendLine(" And G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
        strSQL.AppendLine(" And GS.ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
        strSQL.AppendLine(" And GS.Seq = " & Bsp.Utility.Quote(Seq))
        If MainFlag = "2" And DeptEx = "N" Then
            strSQL.AppendLine(" And G.UpOrganID =" & Bsp.Utility.Quote(ApplyID))
        Else
            strSQL.AppendLine(" And G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
        End If
        strSQL.AppendLine(" And G.GradeDeptID = G.OrderDeptID ")
        strSQL.AppendLine(" and (ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) in ('9','1','7','3','4') ")
        If MainFlag = "2" Then
            strSQL.AppendLine(" Or (ltrim(isnull(CONVERT(char(1), DecryptByKey(GS1.Grade)) ,'')))<>'' and ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,''))<> ltrim(isnull(CONVERT(char(1), DecryptByKey(GS1.Grade)) ,'')))")
        Else
            strSQL.AppendLine(" Or (ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,''))<>'' and ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,''))<> isnull((Select top 1 FinalScoreValue From EvaluationSignLog Where CompID=G.CompID And EvaluationYear = " & Bsp.Utility.Quote(GradeYear) & " And EvaluationSeq = " & Bsp.Utility.Quote(EvaluationSeq) & " And ApplyID = G.EmpID And Result='1' And StepID='3' And IdentityID='4' order by Seq desc),'')")
            strSQL.AppendLine(" And isnull((Select top 1 FinalScoreValue From EvaluationSignLog Where CompID=G.CompID And EvaluationYear = " & Bsp.Utility.Quote(GradeYear) & " And EvaluationSeq = " & Bsp.Utility.Quote(EvaluationSeq) & " And ApplyID = G.EmpID And Result='1' And StepID='3' And IdentityID='4' order by Seq desc),'')<>''))")
        End If


        ''前20%
        'strSQL.AppendLine(" Select Top " & topDataCount & " G.CompID,G.EmpID,P.NameN,GS.ApplyID ")
        'strSQL.AppendLine(" ,GS.GradeOrder ")
        'strSQL.AppendLine(" ,isnull(GE.Comment,'') as Comment ")
        'strSQL.AppendLine(" From  GradeBase G left join GradeSignLog GS on G.CompID=GS.CompID and G.GradeDeptID=GS.ApplyID and G.EmpID=GS.EmpID and GS.Seq=" & Seq & " And G.Online='1'")
        'strSQL.AppendLine(" Left Join Personal P on G.CompID = P.CompID and G.EmpID = P.EmpID ")
        'strSQL.AppendLine(" Left join GradeEmpComment GE on GS.CompID=GE.CompID and GS.ApplyTime=GE.ApplyTime and GS.ApplyID=GE.ApplyID and GS.EmpID=GE.EmpID and GS.Seq=GE.Seq and GS.GradeSeq=GE.GradeSeq ")
        'strSQL.AppendLine(" Where G.CompID = " & Bsp.Utility.Quote(CompID))
        'strSQL.AppendLine(" And G.GradeYear = " & Bsp.Utility.Quote(GradeYear))
        'strSQL.AppendLine(" And G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
        'strSQL.AppendLine(" And GS.ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
        'strSQL.AppendLine(" And GS.Seq = " & Bsp.Utility.Quote(Seq))
        'If MainFlag = "2" And DeptEx = "N" Then
        '    strSQL.AppendLine(" And G.UpOrganID =" & Bsp.Utility.Quote(ApplyID))
        'Else
        '    strSQL.AppendLine(" And G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
        'End If
        'strSQL.AppendLine(" And G.GradeDeptID = G.OrderDeptID ")
        'strSQL.AppendLine(" order By GS.GradeOrder ")
        'strSQL.AppendLine(" Union")
        ''後15%
        'strSQL.AppendLine(" Select Top " & LastDataCount & " G.CompID,G.EmpID,P.NameN,GS.ApplyID ")
        'strSQL.AppendLine(" ,GS.GradeOrder ")
        'strSQL.AppendLine(" ,isnull(GE.Comment,'') as Comment ")
        'strSQL.AppendLine(" From  GradeBase G left join GradeSignLog GS on G.CompID=GS.CompID and G.GradeDeptID=GS.ApplyID and G.EmpID=GS.EmpID and GS.Seq=" & Seq & " And G.Online='1'")
        'strSQL.AppendLine(" Left Join Personal P on G.CompID = P.CompID and G.EmpID = P.EmpID ")
        'strSQL.AppendLine(" Left join GradeEmpComment GE on GS.CompID=GE.CompID and GS.ApplyTime=GE.ApplyTime and GS.ApplyID=GE.ApplyID and GS.EmpID=GE.EmpID and GS.Seq=GE.Seq and GS.GradeSeq=GE.GradeSeq ")
        'strSQL.AppendLine(" Where G.CompID = " & Bsp.Utility.Quote(CompID))
        'strSQL.AppendLine(" And G.GradeYear = " & Bsp.Utility.Quote(GradeYear))
        'strSQL.AppendLine(" And G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
        'strSQL.AppendLine(" And GS.ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
        'strSQL.AppendLine(" And GS.Seq = " & Bsp.Utility.Quote(Seq))
        'If MainFlag = "2" And DeptEx = "N" Then
        '    strSQL.AppendLine(" And G.UpOrganID =" & Bsp.Utility.Quote(ApplyID))
        'Else
        '    strSQL.AppendLine(" And G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
        'End If
        'strSQL.AppendLine(" And G.GradeDeptID = G.OrderDeptID ")
        'strSQL.AppendLine(" order By GS.GradeOrder Desc")
        'If IsSignNext = "N" Then
        '    strSQL.AppendLine(" Union")
        '    '建議考績為特優/優/乙/丙及配置考績與建議考績不一致
        '    strSQL.AppendLine(" Select G.CompID,G.EmpID,P.NameN,GS.ApplyID ")
        '    strSQL.AppendLine(" ,GS.GradeOrder ")
        '    strSQL.AppendLine(" ,isnull(GE.Comment,'') as Comment ")
        '    strSQL.AppendLine(" From  GradeBase G left join GradeSignLog GS on G.CompID=GS.CompID and G.GradeDeptID=GS.ApplyID and G.EmpID=GS.EmpID and GS.Seq=" & Seq & " And G.Online='1'")
        '    strSQL.AppendLine(" Left Join Personal P on G.CompID = P.CompID and G.EmpID = P.EmpID ")
        '    strSQL.AppendLine(" Left join GradeEmpComment GE on GS.CompID=GE.CompID and GS.ApplyTime=GE.ApplyTime and GS.ApplyID=GE.ApplyID and GS.EmpID=GE.EmpID and GS.Seq=GE.Seq and GS.GradeSeq=GE.GradeSeq ")
        '    strSQL.AppendLine(" Where G.CompID = " & Bsp.Utility.Quote(CompID))
        '    strSQL.AppendLine(" And G.GradeYear = " & Bsp.Utility.Quote(GradeYear))
        '    strSQL.AppendLine(" And G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
        '    strSQL.AppendLine(" And GS.ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
        '    strSQL.AppendLine(" And GS.Seq = " & Bsp.Utility.Quote(Seq))
        '    If MainFlag = "2" And DeptEx = "N" Then
        '        strSQL.AppendLine(" And G.UpOrganID =" & Bsp.Utility.Quote(ApplyID))
        '    Else
        '        strSQL.AppendLine(" And G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
        '    End If
        '    strSQL.AppendLine(" And G.GradeDeptID = G.OrderDeptID ")
        '    strSQL.AppendLine(" And (isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'')='9' or isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'') = '1' or  isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'') = '3' or  isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'') = '4' or isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'') <> isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'') ) ")
        'End If
        strSQL.AppendLine(" ) S ")
        strSQL.AppendLine(" Where Comment=''")
        Return IIf(Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB") > 0, True, False)
    End Function

#End Region
#Region "檢查整體評量調整說明是否已填寫完成"
    Public Function CheckEmpComment_ScoreAdjust(ByVal CompID As String, ByVal ApplyID As String, ByVal ApplyTime As String, ByVal Seq As String, ByVal GradeYear As String, ByVal GradeSeq As String _
                                           , ByVal EvaluationSeq As String, ByVal MainFlag As String, ByVal DeptEx As String) As Boolean
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" Select Count(*) ")
        strSQL.AppendLine(" From  GradeBase G left join GradeSignLog GS on G.CompID=GS.CompID and G.GradeDeptID=GS.ApplyID and G.EmpID=GS.EmpID and GS.Seq=" & Seq & " And G.Online='1'")
        strSQL.AppendLine(" Left Join Personal P on G.CompID = P.CompID and G.EmpID = P.EmpID ")
        strSQL.AppendLine(" Left join GradeEmpComment_ScoreAdjust GE on GS.CompID=GE.CompID and GS.ApplyTime=GE.ApplyTime and GS.ApplyID=GE.ApplyID and GS.EmpID=GE.EmpID and GS.Seq=GE.Seq and GS.GradeSeq=GE.GradeSeq ")
        If MainFlag = "2" Then
            strSQL.AppendLine(" Left join GradeSignLog GS1 on G.CompID=GS1.CompID and G.GradeDeptID=GS1.ApplyID and G.EmpID=GS1.EmpID and GS1.Seq=" & Seq - 1 & " and GS.ApplyTime=GS1.ApplyTime and G.Online='1'")
        End If
        strSQL.AppendLine(" Where G.CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine(" And G.GradeYear = " & Bsp.Utility.Quote(GradeYear))
        strSQL.AppendLine(" And G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
        strSQL.AppendLine(" And GS.ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
        strSQL.AppendLine(" And GS.Seq = " & Bsp.Utility.Quote(Seq))
        If MainFlag = "2" And DeptEx = "N" Then
            strSQL.AppendLine(" And G.UpOrganID =" & Bsp.Utility.Quote(ApplyID))
        Else
            strSQL.AppendLine(" And G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
        End If
        strSQL.AppendLine(" And G.GradeDeptID = G.OrderDeptID ")
        If MainFlag = "1" Then
            strSQL.AppendLine(" And (GS.Score<>'' and GS.Score<> isnull((Select top 1 FinalScore From EvaluationSignLog Where CompID=G.CompID And EvaluationYear = " & Bsp.Utility.Quote(GradeYear) & " And EvaluationSeq = " & Bsp.Utility.Quote(EvaluationSeq) & " And ApplyID = G.EmpID And Result='1' And StepID='3' And IdentityID='4' order by Seq desc),'')")
            strSQL.AppendLine(" And isnull((Select top 1 FinalScore From EvaluationSignLog Where CompID=G.CompID And EvaluationYear = " & Bsp.Utility.Quote(GradeYear) & " And EvaluationSeq = " & Bsp.Utility.Quote(EvaluationSeq) & " And ApplyID = G.EmpID And Result='1' And StepID='3' And IdentityID='4' order by Seq desc),'')<>'')")
        Else
            strSQL.AppendLine(" And (isnull(GS1.Score,'')<>'' and GS.Score<> isnull(GS1.Score,''))")
        End If

        strSQL.AppendLine(" And GE.Comment is Null ")
        Return IIf(Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB") > 0, True, False)
    End Function
#End Region
#Region "檢查單位主管是否已送出"
    Public Function CheckSignLog(ByVal CompID As String, ByVal ApplyID As String, ByVal ApplyTime As String, ByVal Seq As String, ByVal GradeYear As String, ByVal GradeSeq As String _
                                           , ByVal DeptEx As String) As Boolean
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" Select Count(*) ")
        strSQL.AppendLine(" From  GradeBase G left join SignLog S on G.CompID=S.CompID and G.GradeDeptID=S.ApplyID And G.Online='1'")
        strSQL.AppendLine(" Where G.CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine(" And G.GradeYear = " & Bsp.Utility.Quote(GradeYear))
        strSQL.AppendLine(" And G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
        strSQL.AppendLine(" And S.ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
        strSQL.AppendLine(" And S.Seq = " & Bsp.Utility.Quote(Seq - 1))
        If DeptEx = "N" Then
            strSQL.AppendLine(" And G.UpOrganID =" & Bsp.Utility.Quote(ApplyID))
        Else
            strSQL.AppendLine(" And G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
        End If
        strSQL.AppendLine(" And G.GradeDeptID = G.OrderDeptID ")
        strSQL.AppendLine(" And S.Result='0' ")
        Return IIf(Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB") > 0, True, False)
    End Function
#End Region
#Region "檢查考績是否已配置"
    Public Function CheckGrade(ByVal CompID As String, ByVal ApplyID As String, ByVal ApplyTime As String, ByVal Seq As String, ByVal GradeYear As String, ByVal GradeSeq As String _
                                           , ByVal MainFlag As String, ByVal DeptEx As String) As Boolean
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" Select Count(*) ")
        strSQL.AppendLine(" From  GradeBase G left join GradeSignLog GS on G.CompID=GS.CompID and G.GradeDeptID=GS.ApplyID and G.EmpID=GS.EmpID and GS.Seq=" & Seq & " And G.Online='1'")
        strSQL.AppendLine(" Left join GradeEmpComment_ScoreAdjust GE on GS.CompID=GE.CompID and GS.ApplyTime=GE.ApplyTime and GS.ApplyID=GE.ApplyID and GS.EmpID=GE.EmpID and GS.Seq=GE.Seq and GS.GradeSeq=GE.GradeSeq ")
        strSQL.AppendLine(" Where G.CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine(" And G.GradeYear = " & Bsp.Utility.Quote(GradeYear))
        strSQL.AppendLine(" And G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
        strSQL.AppendLine(" And GS.ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
        strSQL.AppendLine(" And GS.Seq = " & Bsp.Utility.Quote(Seq))
        If MainFlag = "2" And DeptEx = "N" Then
            strSQL.AppendLine(" And G.UpOrganID =" & Bsp.Utility.Quote(ApplyID))
        Else
            strSQL.AppendLine(" And G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
        End If
        strSQL.AppendLine(" And G.GradeDeptID = G.OrderDeptID ")
        strSQL.AppendLine(" And GS.Grade is null ")
        Return IIf(Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB") > 0, True, False)
    End Function
#End Region
#Region "取單位考績"
    Public Function GetDeptGrade(ByVal CompID As String, ByVal GradeYear As String, ByVal GradeSeq As String, ByVal MainFlag As String, ByVal ApplyID As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(ConfigurationManager.AppSettings("eHRMSDBDES").ToString)
        strSQL.AppendLine(" Select isnull(CONVERT(char(1), DecryptByKey(Grade)) ,'') as DeptGrade from GradeDept ")
        strSQL.AppendLine(" where CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine(" And GradeYear = " & Bsp.Utility.Quote(GradeYear))
        strSQL.AppendLine(" And GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
        strSQL.AppendLine(" and MainFlag= " & Bsp.Utility.Quote(MainFlag))
        strSQL.AppendLine(" And DeptID = " & Bsp.Utility.Quote(ApplyID))
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)

    End Function
#End Region
#Region "取優甲乙比例"
    Public Function GetGradeRation(ByVal CompID As String, ByVal GradeYear As String, ByVal GradeSeq As String, ByVal GradeDept As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(ConfigurationManager.AppSettings("eHRMSDBDES").ToString)
        strSQL.AppendLine(" Select GradeID,Ratio from GradeDisposeParameter ")
        strSQL.AppendLine(" where CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine(" And GradeYear = " & Bsp.Utility.Quote(GradeYear))
        strSQL.AppendLine(" And GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
        strSQL.AppendLine(" and DeptGradeID= " & Bsp.Utility.Quote(GradeDept))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)

    End Function
#End Region
#End Region

#Region "GS1000 年度考核待辦清單"
    '20151104 wei add Load考核參數
    Public Function GetGradeParameter(ByVal strCompID As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" Select GradeYear,GradeSeq from GradeParameter where CompID = " & Bsp.Utility.Quote(strCompID))
        strSQL.AppendLine(" Order By GradeYear Desc,GradeSeq Desc ")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

#Region "GS1000 年度考核待辦清單-查詢"
    Public Function QueryGrade(ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" Select S.ApplyTime , S.ApplyID , S.CompID, S.Seq, S.HRFlag , S.ArriveTime , S.SignID , S.SignIDComp ,S.SignTime, S.DeleteMark, S.MainFlag , C.CompName ")
        strSQL.AppendLine(" , Case C.Payroll when 'SPHBK1' then '1' when 'SPHSC1' then '2' when 'SPHCR1' then '3' end as CompIdentity , isnull(Cast(O.OrganName as Nvarchar(60)),isnull(P.NameN,'')) ApplyName , A.ApplyType ")
        'strSQL.AppendLine(" ,Case When A.ApplyType='74' and S.MainFlag='2' and S.Seq>='2' and A.Status<='2' and S.SignTime=S.ApplyTime Then cast(A.Status as int) +'1' When A.ApplyType='74' and S.MainFlag='1' and A.Status='4' and A.ApplyID not in ('000OB0','BR0121','BR121D','000160','000K00','000R00','000X00','000180','00017A','O10000','M10000','P10000','T70000','T72000') Then '3' Else A.Status End As Status")
        strSQL.AppendLine(" ,A.Status")
        strSQL.AppendLine(" , Case when A.ApplyType = '74' then Case when A.Status in ('1','3') and S.SignTime <> '1900/1/1' then '單位-區/處主管審核' when A.Status in ('1','3') and S.SignTime = '1900/1/1' and S.MainFlag='2' then '區/處主管審核' when A.Status in ('4','5') and S.MainFlag = '1' then '單位' + isnull(H.CodeCName,'') when A.Status in ('4','5') and S.MainFlag='2' then '區/處' + isnull(H.CodeCName,'') else isnull(H.CodeCName,'') end else '' end as StatusName ")
        strSQL.AppendLine(" ,Case When A.ApplyType='74' Then '年度考核' else '' End as ApplyTypeName")
        strSQL.AppendLine(" ,convert(char(10),S.ApplyTime,111) as ApplyTimeShow")
        strSQL.AppendLine(" ,datediff(DAY,S.ArriveTime,getdate()) + 1 as ArrivalDay")
        strSQL.AppendLine(" ,IsSignNext=isnull((select 'Y' From SignLog Where CompID=S.CompID and ApplyTime=S.ApplyTime and ApplyID=S.ApplyID and Seq>S.Seq),'N')")
        strSQL.AppendLine(" ,Result ")
        strSQL.AppendLine(" FROM (")
        '單位主管提報
        strSQL.AppendLine(" SELECT ApplyTime, ApplyID ,S1.CompID, Seq, HRFlag , ArriveTime, SignID , SignIDComp ,SignTime, DeleteMark , MainFlag, Result ")
        strSQL.AppendLine(" FROM SignLog S1 Left Join Organization O1 On S1.ApplyID=O1.OrganID And O1.RoleCode=20")
        strSQL.AppendLine(" WHERE SignID = " & Bsp.Utility.Quote(UserProfile.UserID) & " and ArriveTime > '1900/1/1' and DeleteMark <> '1' and HRFlag <> '2' and SignIDComp = " & Bsp.Utility.Quote(UserProfile.CompID))
        strSQL.AppendLine(" and (MainFlag='' or MainFlag='1' or O1.UpOrganID In (Select DeptID From GradeDeptException Where CompID = " & Bsp.Utility.Quote(UserProfile.CompID) & "))")
        strSQL.AppendLine(" UNION ")
        '區處主管
        strSQL.AppendLine(" SELECT Distinct ApplyTime, O1.UpOrganID as ApplyID")
        strSQL.AppendLine(" , S1.CompID, Seq, HRFlag , ApplyTime As ArriveTime, SignID, SignIDComp , SignTime , DeleteMark ,  MainFlag, Result")
        strSQL.AppendLine(" FROM SignLog S1 Inner Join Organization O1 On S1.ApplyID=O1.OrganID And O1.RoleCode=20")
        strSQL.AppendLine(" WHERE SignID = " & Bsp.Utility.Quote(UserProfile.UserID) & " and DeleteMark <> '1' and HRFlag <> '2' and SignIDComp = " & Bsp.Utility.Quote(UserProfile.CompID) & " and MainFlag='2' And (Year(ApplyTime)=year(getdate()) or Year(ApplyTime)=year(getdate())-1) ")
        strSQL.AppendLine(" and O1.UpOrganID Not In (Select DeptID From GradeDeptException Where CompID= " & Bsp.Utility.Quote(UserProfile.CompID) & ")")
        strSQL.AppendLine(" ) S left join Application A on S.ApplyTime = A.ApplyTime and S.ApplyID = A.ApplyID and S.CompID = A.CompID")
        strSQL.AppendLine(" left join Personal P on S.ApplyID = P.EmpID and S.CompID = P.CompID")
        strSQL.AppendLine(" left join Organization O on S.ApplyID = O.OrganID and S.CompID = O.CompID")
        strSQL.AppendLine(" left join HRCodeMap as H on A.Status = H.Code and H.TabName = 'Application' and FldName = 'Status_Grade' and NotShowFlag ='0'")
        strSQL.AppendLine(" left join Company C on S.CompID = C.CompID")
        strSQL.AppendLine(" where A.ApplyType='74'")
        strSQL.AppendLine(" and (S.SignID = " & Bsp.Utility.Quote(UserProfile.UserID) & ")")
        strSQL.AppendLine(" and ((A.ApplyType <> '74' and S.SignTime = '1900/1/1') or (A.ApplyType = '74' and S.Result in ('0','1')))")
        strSQL.AppendLine(" and (S.DeleteMark <> '1')")
        strSQL.AppendLine(" and (A.Status <> '9')")
        strSQL.AppendLine(" and ((A.ApplyType <> '74' and A.Status <> '4') or A.ApplyType = '74')")
        strSQL.AppendLine(" and (S.HRFlag <> '2')")
        strSQL.AppendLine(" and (S.SignIDComp = " & Bsp.Utility.Quote(UserProfile.CompID) & ")")
        strSQL.AppendLine(" order by A.ApplyType desc,S.ArriveTime desc ")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#End Region

#Region "GS1100 年度考核(單位主管排序)"
#Region "GS1100 年度考核(單位主管排序)-查詢"
    Public Function GS1100Query(ByVal CompID As String, ByVal ApplyID As String, ByVal ApplyTime As String, ByVal Seq As String, ByVal Status As String, ByVal MainFlag As String _
                           , ByVal GradeYear As String, ByVal GradeSeq As String, ByVal EvaluationSeq As String, Optional GroupID As String = "") As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(ConfigurationManager.AppSettings("eHRMSDBDES").ToString)
        strSQL.AppendLine(" Select P.CompID,P.EmpID ,P.NameN ,Convert(char(19), P.EmpDate, 111) as EmpDate ,Po.PositionID,Po.Remark as Position ,W.Remark as WorkType ,P.RankID ,P.RankIDMap,isnull(T.TitleName,'') TitleName")
        strSQL.AppendLine(" ,isnull(CONVERT(char(1), DecryptByKey(EG3.Y3Grade)) ,'') as Y3Grade ,isnull(CONVERT(char(1), DecryptByKey(EG2.Y2Grade)),'') as Y2Grade ,isnull(CONVERT(char(1), DecryptByKey(EG1.Y1Grade)),'') as Y1Grade ")
        strSQL.AppendLine(" ,G.Memo ")
        strSQL.AppendLine(" ,ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) as Grade ")
        strSQL.AppendLine(" ,cast(Case when isnull(GS.GradeOrder,0)=0 then '' else Cast(GS.GradeOrder as Varchar(3)) end as int) as GradeOrder")
        'strSQL.AppendLine(" ,GS.ScoreValue ,GS.Score") 20160604 wei del
        strSQL.AppendLine(" ,isnull(A.Cnt,0) RewardCnt ,isnull(B.Cnt,0) PerformanceCnt ,O.OrganName")
        strSQL.AppendLine(" ,FinalScore=isnull((Select top 1 FinalScore From EvaluationSignLog Where CompID=G.CompID And EvaluationYear = " & Bsp.Utility.Quote(GradeYear) & " And EvaluationSeq = " & Bsp.Utility.Quote(EvaluationSeq) & " And ApplyID = P.EmpID And Result='1' And StepID='3' And IdentityID='4' order by Seq desc),'') ")
        strSQL.AppendLine(" ,FinalScoreValue=isnull(cast((Select top 1 FinalScoreValue From EvaluationSignLog Where CompID=G.CompID And EvaluationYear = " & Bsp.Utility.Quote(GradeYear) & " And EvaluationSeq = " & Bsp.Utility.Quote(EvaluationSeq) & " And ApplyID = P.EmpID And Result='1' And StepID='3' And IdentityID='4' order by Seq desc) as varchar(1)),'') ")
        strSQL.AppendLine(" ,isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'') as GradeAdjust ")
        strSQL.AppendLine(" ,G.GroupID ")   '20160625 wei add
        strSQL.AppendLine(" from Personal P inner join GradeBase G on G.GradeYear = " & Bsp.Utility.Quote(GradeYear) & " and P.CompID =G.CompID and P.EmpID = G.EmpID and G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq) & " And G.Online='1'")
        strSQL.AppendLine(" left join Organization O on O.CompID = G.CompID and O.OrganID = G.OrderDeptID")
        strSQL.AppendLine(" left join GradeSignLog GS on GS.CompID= " & Bsp.Utility.Quote(CompID) & " and GS.ApplyTime= " & Bsp.Utility.Quote(ApplyTime) & " and GS.ApplyID= " & Bsp.Utility.Quote(ApplyID) & " and GS.Seq= " & Bsp.Utility.Quote(Seq) & " and P.CompID =GS.CompID and P.EmpID = GS.EmpID and G.GradeSeq = GS.GradeSeq")
        strSQL.AppendLine(" left join Title T on P.CompID = T.CompID and P.RankID = T.RankID and P.TitleID = T.TitleID")
        strSQL.AppendLine(" left join EmpPosition EP1 on P.CompID = EP1.CompID and P.EmpID = EP1.EmpID and EP1.PrincipalFlag = '1'")
        strSQL.AppendLine(" left join Position Po on EP1.CompID = Po.CompID and EP1.PositionID = Po.PositionID ")
        strSQL.AppendLine(" left join EmpWorkType EW on P.CompID = EW.CompID and P.EmpID = EW.EmpID and EW.PrincipalFlag = '1'")
        strSQL.AppendLine(" left join WorkType W on EW.CompID = W.CompID and EW.WorkTypeID = W.WorkTypeID")
        strSQL.AppendLine(" left join ( select CompID,EmpID,Grade Y3Grade from EmpGrade where CompID ='SPHBK1' and GradeYear = " & GradeYear - 3 & ") EG3 on P.CompID = EG3.CompID and P.EmpID = EG3.EmpID")
        strSQL.AppendLine(" left join ( select CompID,EmpID,Grade Y2Grade from EmpGrade where CompID ='SPHBK1' and GradeYear = " & GradeYear - 2 & ") EG2 on P.CompID = EG2.CompID and P.EmpID = EG2.EmpID")
        strSQL.AppendLine(" left join ( select CompID,EmpID,Grade Y1Grade from EmpGrade where CompID ='SPHBK1' and GradeYear = " & GradeYear - 1 & ") EG1 on P.CompID = EG1.CompID and P.EmpID = EG1.EmpID")
        strSQL.AppendLine(" left join ( select distinct CompID,EmpID, 1 Cnt from EmpReward where CompID ='SPHBK1' and year(ValidDate) = " & Bsp.Utility.Quote(GradeYear) & " and EmpID <> '' ) A on P.CompID = A.CompID and P.EmpID = A.EmpID ")
        strSQL.AppendLine(" left join ( select CompID,EmpID, 1 Cnt from EmpPerformance where CompID ='SPHBK1' and EPYear= " & Bsp.Utility.Quote(GradeYear) & ") B on P.CompID = B.CompID and P.EmpID = B.EmpID")
        strSQL.AppendLine(" where G.CompID = " & Bsp.Utility.Quote(CompID) & " and G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID) & " and G.GradeDeptID = G.OrderDeptID and P.EmpID <> " & Bsp.Utility.Quote(UserProfile.UserID))
        If GroupID <> "" And GroupID <> "0" Then
            strSQL.AppendLine(" And G.GroupID=" & Bsp.Utility.Quote(GroupID))
        End If
        strSQL.Append("Order by Case when ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) = '9' then '0' when ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) = '6' then '1.9' when ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) = '7' then '2.1' when ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) = '' then '99' else ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) End")
        strSQL.AppendLine(",Case when GS.GradeOrder = 0 then 1 else 0 end ,Cast(GS.GradeOrder as int) ,Po.PositionID ,P.RankIDMap desc ,P.EmpID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region
#Region "GS1100 年度考核(單位主管排序)-查詢筆數"
    Public Function GS1100QueryCount(ByVal CompID As String, ByVal ApplyID As String, ByVal ApplyTime As String, ByVal Seq As String, ByVal Status As String, ByVal MainFlag As String _
                           , ByVal GradeYear As String, ByVal GradeSeq As String, ByVal EvaluationSeq As String, Optional GroupID As String = "") As Integer
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" Select Count(P.CompID) as Cnt")
        strSQL.AppendLine(" from Personal P inner join GradeBase G on G.GradeYear = " & Bsp.Utility.Quote(GradeYear) & " and P.CompID =G.CompID and P.EmpID = G.EmpID and G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq) & " And G.Online='1'")
        strSQL.AppendLine(" where G.CompID = " & Bsp.Utility.Quote(CompID) & " and G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID) & " and G.GradeDeptID = G.OrderDeptID and P.EmpID <> " & Bsp.Utility.Quote(UserProfile.UserID))
        If GroupID <> "" And GroupID <> "0" Then
            strSQL.AppendLine(" And G.GroupID=" & Bsp.Utility.Quote(GroupID))
        End If

        Return Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")

    End Function

#End Region
#Region "GS1100 年度考核(單位主管排序)-修改"
    Public Function GS1100Update(ByVal dt As DataTable, ByVal ApplyID As String, ByVal ApplyTime As String, ByVal Seq As String, ByVal GradeSeq As String, ByVal GradeYear As String, ByVal IsSignNext As String) As Boolean
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                strSQL.AppendLine(ConfigurationManager.AppSettings("eHRMSDBDES").ToString)
                For i As Integer = 0 To dt.Rows.Count - 1
                    strSQL.AppendLine(" Update GradeSignLog ")
                    strSQL.AppendLine(" Set Grade = EncryptByKey(Key_GUID('eHRMSDBDES'),'" & dt.Rows(i)("Grade").ToString().Trim & "')")    '20160606 wei add
                    'strSQL.AppendLine(" Set GradeOrder = " & dt.Rows(i)("GradeOrder")) '20160606 wei del
                    'strSQL.AppendLine(" ,ScoreValue = " & dt.Rows(i)("ScoreValue"))    '20160606 wei del
                    'strSQL.AppendLine(" ,Score = " & Bsp.Utility.Quote(dt.Rows(i)("Score")))   '20160606 wei del
                    strSQL.AppendLine(" ,LastChgComp = " & Bsp.Utility.Quote(UserProfile.CompID))
                    strSQL.AppendLine(" ,LastChgID = " & Bsp.Utility.Quote(UserProfile.UserID))
                    strSQL.AppendLine(" ,LastChgDate = getdate()")
                    strSQL.AppendLine(" Where CompID = " & Bsp.Utility.Quote(dt.Rows(i)("CompID")))
                    strSQL.AppendLine(" and ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
                    strSQL.AppendLine(" and ApplyID = " & Bsp.Utility.Quote(ApplyID))
                    strSQL.AppendLine(" and EmpID = " & Bsp.Utility.Quote(dt.Rows(i)("EmpID")))
                    strSQL.AppendLine(" and Seq = " & Bsp.Utility.Quote(Seq))
                    strSQL.AppendLine(" and GradeSeq = " & Bsp.Utility.Quote(GradeSeq) & ";")
                    '20160606 wei del
                    'If IsSignNext = "N" Then
                    '    strSQL.AppendLine(" Update GradeBase ")
                    '    strSQL.AppendLine(" Set GradeAdjust = EncryptByKey(Key_GUID('eHRMSDBDES'),'" & dt.Rows(i)("GradeAdjust").ToString().Trim & "')")
                    '    strSQL.AppendLine(" ,LastChgComp = " & Bsp.Utility.Quote(UserProfile.CompID))
                    '    strSQL.AppendLine(" ,LastChgID = " & Bsp.Utility.Quote(UserProfile.UserID))
                    '    strSQL.AppendLine(" ,LastChgDate = getdate()")
                    '    strSQL.AppendLine(" Where CompID = " & Bsp.Utility.Quote(dt.Rows(i)("CompID")))
                    '    strSQL.AppendLine(" and GradeYear = " & Bsp.Utility.Quote(GradeYear))
                    '    strSQL.AppendLine(" and GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
                    '    strSQL.AppendLine(" and EmpID = " & Bsp.Utility.Quote(dt.Rows(i)("EmpID")))
                    '    strSQL.AppendLine(" and GradeSeq = " & Bsp.Utility.Quote(GradeSeq) & ";")
                    'End If

                Next

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
#Region "GS1100 年度考核(單位主管排序)-產生單位考績"
    Public Function GS1100GradeData(ByVal CompID As String, ByVal ApplyID As String, ByVal ApplyTime As String, ByVal Seq As String, ByVal GradeYear As String, ByVal GradeSeq As String _
                                           , ByVal MainFlag As String, ByVal DeptGrade As String, ByVal intDeptCount() As Integer, ByVal strGrade() As String, ByVal intTotalCount As Integer, ByVal IsSignNext As String) As Boolean
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                '刪除GradeDispose
                strSQL.AppendLine(ConfigurationManager.AppSettings("eHRMSDBDES").ToString)
                strSQL.AppendLine("　Delete from GradeDispose ")
                strSQL.AppendLine(" where CompID = " & Bsp.Utility.Quote(CompID))
                strSQL.AppendLine(" and DeptID =" & Bsp.Utility.Quote(ApplyID))
                strSQL.AppendLine(" and GradeYear =" & Bsp.Utility.Quote(GradeYear))
                strSQL.AppendLine(" and GradeSeq =" & Bsp.Utility.Quote(GradeSeq))
                strSQL.AppendLine(" and MainFlag= " & Bsp.Utility.Quote(MainFlag) & "; ")
                'Insert GradeDispose
                For intGradeID = 0 To UBound(intDeptCount)
                    If intDeptCount(intGradeID) > 0 Then
                        strSQL.AppendLine(" Insert into GradeDispose (GradeYear, GradeSeq, CompID, DeptID, GradeID, DeptCount, MainFlag,LastChgComp, LastChgID, LastChgDate)  ")
                        strSQL.AppendLine(" values(" & Bsp.Utility.Quote(GradeYear))
                        strSQL.AppendLine(" ," & Bsp.Utility.Quote(GradeSeq))
                        strSQL.AppendLine(" ," & Bsp.Utility.Quote(CompID))
                        strSQL.AppendLine(" ," & Bsp.Utility.Quote(ApplyID))
                        strSQL.AppendLine(" ," & Bsp.Utility.Quote(strGrade(intGradeID)))
                        strSQL.AppendLine(" ," & intDeptCount(intGradeID))
                        strSQL.AppendLine(" ," & Bsp.Utility.Quote(MainFlag))
                        strSQL.AppendLine(" ," & Bsp.Utility.Quote(UserProfile.CompID))
                        strSQL.AppendLine(" ," & Bsp.Utility.Quote(UserProfile.UserID))
                        strSQL.AppendLine(" ,getdate());")
                    End If
                Next
                'Update GradeDept
                strSQL.AppendLine(" Update GradeDept set TotalCount =" & intTotalCount)
                strSQL.AppendLine(" where CompID = " & Bsp.Utility.Quote(CompID))
                strSQL.AppendLine(" and GradeYear =" & Bsp.Utility.Quote(GradeYear))
                strSQL.AppendLine(" and GradeSeq =" & Bsp.Utility.Quote(GradeSeq))
                strSQL.AppendLine(" and MainFlag= " & Bsp.Utility.Quote(MainFlag))
                strSQL.AppendLine(" and DeptID =" & Bsp.Utility.Quote(ApplyID) & ";")
                'Update GradeSignLog.Grade
                '9-特優
                strSQL.AppendLine(" Update GradeSignLog Set Grade=EncryptByKey(Key_GUID('eHRMSDBDES'),'9')")
                strSQL.AppendLine(" ,LastChgComp = " & Bsp.Utility.Quote(UserProfile.CompID))
                strSQL.AppendLine(" ,LastChgID = " & Bsp.Utility.Quote(UserProfile.UserID))
                strSQL.AppendLine(" ,LastChgDate = getdate()")
                strSQL.AppendLine(" Where CompID = " & Bsp.Utility.Quote(CompID))
                strSQL.AppendLine(" and ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
                strSQL.AppendLine(" and ApplyID = " & Bsp.Utility.Quote(ApplyID))
                strSQL.AppendLine(" and Seq = " & Bsp.Utility.Quote(Seq))
                strSQL.AppendLine(" and GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
                strSQL.AppendLine(" and GradeOrder <= " & intDeptCount(6) & ";")
                '1-優
                strSQL.AppendLine(" Update GradeSignLog Set Grade=EncryptByKey(Key_GUID('eHRMSDBDES'),'1')")
                strSQL.AppendLine(" ,LastChgComp = " & Bsp.Utility.Quote(UserProfile.CompID))
                strSQL.AppendLine(" ,LastChgID = " & Bsp.Utility.Quote(UserProfile.UserID))
                strSQL.AppendLine(" ,LastChgDate = getdate()")
                strSQL.AppendLine(" Where CompID = " & Bsp.Utility.Quote(CompID))
                strSQL.AppendLine(" and ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
                strSQL.AppendLine(" and ApplyID = " & Bsp.Utility.Quote(ApplyID))
                strSQL.AppendLine(" and Seq = " & Bsp.Utility.Quote(Seq))
                strSQL.AppendLine(" and GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
                strSQL.AppendLine(" and GradeOrder > " & intDeptCount(6) & ";")
                '6-甲上
                strSQL.AppendLine(" Update GradeSignLog Set Grade=EncryptByKey(Key_GUID('eHRMSDBDES'),'6')")
                strSQL.AppendLine(" ,LastChgComp = " & Bsp.Utility.Quote(UserProfile.CompID))
                strSQL.AppendLine(" ,LastChgID = " & Bsp.Utility.Quote(UserProfile.UserID))
                strSQL.AppendLine(" ,LastChgDate = getdate()")
                strSQL.AppendLine(" Where CompID = " & Bsp.Utility.Quote(CompID))
                strSQL.AppendLine(" and ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
                strSQL.AppendLine(" and ApplyID = " & Bsp.Utility.Quote(ApplyID))
                strSQL.AppendLine(" and Seq = " & Bsp.Utility.Quote(Seq))
                strSQL.AppendLine(" and GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
                strSQL.AppendLine(" and GradeOrder > " & intDeptCount(6) + intDeptCount(0) & ";")
                '2-甲
                strSQL.AppendLine(" Update GradeSignLog Set Grade=EncryptByKey(Key_GUID('eHRMSDBDES'),'2')")
                strSQL.AppendLine(" ,LastChgComp = " & Bsp.Utility.Quote(UserProfile.CompID))
                strSQL.AppendLine(" ,LastChgID = " & Bsp.Utility.Quote(UserProfile.UserID))
                strSQL.AppendLine(" ,LastChgDate = getdate()")
                strSQL.AppendLine(" Where CompID = " & Bsp.Utility.Quote(CompID))
                strSQL.AppendLine(" and ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
                strSQL.AppendLine(" and ApplyID = " & Bsp.Utility.Quote(ApplyID))
                strSQL.AppendLine(" and Seq = " & Bsp.Utility.Quote(Seq))
                strSQL.AppendLine(" and GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
                strSQL.AppendLine(" and GradeOrder > " & intDeptCount(6) + intDeptCount(0) + +intDeptCount(4) & ";")
                '7-甲下
                strSQL.AppendLine(" Update GradeSignLog Set Grade=EncryptByKey(Key_GUID('eHRMSDBDES'),'7')")
                strSQL.AppendLine(" ,LastChgComp = " & Bsp.Utility.Quote(UserProfile.CompID))
                strSQL.AppendLine(" ,LastChgID = " & Bsp.Utility.Quote(UserProfile.UserID))
                strSQL.AppendLine(" ,LastChgDate = getdate()")
                strSQL.AppendLine(" Where CompID = " & Bsp.Utility.Quote(CompID))
                strSQL.AppendLine(" and ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
                strSQL.AppendLine(" and ApplyID = " & Bsp.Utility.Quote(ApplyID))
                strSQL.AppendLine(" and Seq = " & Bsp.Utility.Quote(Seq))
                strSQL.AppendLine(" and GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
                strSQL.AppendLine(" and GradeOrder > " & intDeptCount(6) + intDeptCount(0) + intDeptCount(4) + intDeptCount(1) & ";")
                '3-乙
                strSQL.AppendLine(" Update GradeSignLog Set Grade=EncryptByKey(Key_GUID('eHRMSDBDES'),'3')")
                strSQL.AppendLine(" ,LastChgComp = " & Bsp.Utility.Quote(UserProfile.CompID))
                strSQL.AppendLine(" ,LastChgID = " & Bsp.Utility.Quote(UserProfile.UserID))
                strSQL.AppendLine(" ,LastChgDate = getdate()")
                strSQL.AppendLine(" Where CompID = " & Bsp.Utility.Quote(CompID))
                strSQL.AppendLine(" and ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
                strSQL.AppendLine(" and ApplyID = " & Bsp.Utility.Quote(ApplyID))
                strSQL.AppendLine(" and Seq = " & Bsp.Utility.Quote(Seq))
                strSQL.AppendLine(" and GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
                strSQL.AppendLine(" and GradeOrder > " & intDeptCount(6) + intDeptCount(0) + intDeptCount(4) + intDeptCount(1) + intDeptCount(5) & ";")
                '4-丙
                strSQL.AppendLine(" Update GradeSignLog Set Grade=EncryptByKey(Key_GUID('eHRMSDBDES'),'4')")
                strSQL.AppendLine(" ,LastChgComp = " & Bsp.Utility.Quote(UserProfile.CompID))
                strSQL.AppendLine(" ,LastChgID = " & Bsp.Utility.Quote(UserProfile.UserID))
                strSQL.AppendLine(" ,LastChgDate = getdate()")
                strSQL.AppendLine(" Where CompID = " & Bsp.Utility.Quote(CompID))
                strSQL.AppendLine(" and ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
                strSQL.AppendLine(" and ApplyID = " & Bsp.Utility.Quote(ApplyID))
                strSQL.AppendLine(" and Seq = " & Bsp.Utility.Quote(Seq))
                strSQL.AppendLine(" and GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
                strSQL.AppendLine(" and GradeOrder > " & intDeptCount(6) + intDeptCount(0) + intDeptCount(4) + intDeptCount(1) + intDeptCount(5) + intDeptCount(2) & ";")

                strSQL.AppendLine(" Update G Set G.GradeDept=GS.Grade,G.GradeOrderDept=GS.GradeOrder ")
                strSQL.AppendLine(" From GradeBase G  left join GradeSignLog GS on GS.CompID=G.CompID and GS.ApplyID=G.GradeDeptID and GS.GradeSeq=G.GradeSeq and GS.EmpID=G.EmpID and G.Online='1' ")
                strSQL.AppendLine(" Where G.CompID = " & Bsp.Utility.Quote(CompID))
                strSQL.AppendLine(" And G.GradeYear = " & Bsp.Utility.Quote(GradeYear))
                strSQL.AppendLine(" And G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
                strSQL.AppendLine(" And GS.ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
                strSQL.AppendLine(" And G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
                strSQL.AppendLine(" And G.GradeDeptID = G.OrderDeptID; ")

                If IsSignNext = "N" Then
                    strSQL.AppendLine(" Update G Set G.Grade=GS.Grade,G.GradeOrder=GS.GradeOrder ")
                    strSQL.AppendLine(" From GradeBase G  left join GradeSignLog GS on GS.CompID=G.CompID and GS.ApplyID=G.GradeDeptID and GS.GradeSeq=G.GradeSeq and GS.EmpID=G.EmpID and G.Online='1' ")
                    strSQL.AppendLine(" Where G.CompID = " & Bsp.Utility.Quote(CompID))
                    strSQL.AppendLine(" And G.GradeYear = " & Bsp.Utility.Quote(GradeYear))
                    strSQL.AppendLine(" And G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
                    strSQL.AppendLine(" And GS.ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
                    strSQL.AppendLine(" And G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
                    strSQL.AppendLine(" And G.GradeDeptID = G.OrderDeptID; ")

                    strSQL.AppendLine(" Update G Set G.GradeAdjust=GS.Grade")
                    strSQL.AppendLine(" ,LastChgComp = " & Bsp.Utility.Quote(UserProfile.CompID))
                    strSQL.AppendLine(" ,LastChgID = " & Bsp.Utility.Quote(UserProfile.UserID))
                    strSQL.AppendLine(" ,LastChgDate = getdate()")
                    strSQL.AppendLine(" From GradeBase G left join GradeSignLog GS on G.CompID=GS.CompID and G.GradeDeptID=GS.ApplyID and G.EmpID=GS.EmpID and G.GradeSeq=GS.GradeSeq and G.Online='1' and G.GradeYear=" & Bsp.Utility.Quote(GradeYear))
                    strSQL.AppendLine(" Where G.CompID = " & Bsp.Utility.Quote(CompID))
                    strSQL.AppendLine(" and GS.ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
                    strSQL.AppendLine(" and GS.ApplyID = " & Bsp.Utility.Quote(ApplyID))
                    strSQL.AppendLine(" and GS.Seq = " & Bsp.Utility.Quote(Seq))
                    strSQL.AppendLine(" and GS.GradeSeq = " & Bsp.Utility.Quote(GradeSeq) & ";")
                End If

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
#Region "GS1100 年度考核(單位主管排序)-送出"
    Public Function GS1100SendData(ByVal CompID As String, ByVal ApplyID As String, ByVal ApplyTime As String, ByVal Seq As String, ByVal GradeYear As String, ByVal GradeSeq As String _
                                           , ByVal EvaluationSeq As String, ByVal MainFlag As String, ByVal DeptEx As String, ByVal topDataCount As String, ByVal LastDataCount As String, ByVal IsSignNext As String) As Boolean
        Dim strSQL As New StringBuilder()
        Dim objHR As New HR()
        Dim strWhere As String = ""
        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                strSQL.AppendLine(ConfigurationManager.AppSettings("eHRMSDBDES").ToString)
                '刪除暫存的考核補充說明 20160718 wei del 取消刪除
                'strSQL.AppendLine(" Delete From GradeEmpComment ")
                'strSQL.AppendLine(" Where GradeYear = " & Bsp.Utility.Quote(GradeYear))
                'strSQL.AppendLine(" And ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
                'strSQL.AppendLine(" And ApplyID = " & Bsp.Utility.Quote(ApplyID))
                'strSQL.AppendLine(" And CompID = " & Bsp.Utility.Quote(CompID))
                'strSQL.AppendLine(" And Seq = " & Bsp.Utility.Quote(Seq))
                'strSQL.AppendLine(" And GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
                'strSQL.AppendLine(" And EmpID not In (")
                'strSQL.AppendLine("Select EmpID From ( ")
                'strSQL.AppendLine(" Select G.CompID,G.EmpID,P.NameN,GS.ApplyID ")
                'strSQL.AppendLine(" ,GS.GradeOrder ")
                'strSQL.AppendLine(" ,isnull(GE.Comment,'') as Comment ")
                'strSQL.AppendLine(" ,Case When isnull(GE.Comment,'N')='N' Then 'N' Else 'Y' End as IsExit ")
                'If MainFlag = "2" Then
                '    strSQL.AppendLine(" ,ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) as Grade1 ")
                '    strSQL.AppendLine(" ,ltrim(isnull(CONVERT(char(1), DecryptByKey(GS1.Grade)) ,'')) as Grade2 ")
                'Else
                '    strSQL.AppendLine(" ,isnull((Select top 1 FinalScoreValue From EvaluationSignLog Where CompID=G.CompID And EvaluationYear = " & Bsp.Utility.Quote(GradeYear) & " And EvaluationSeq = " & Bsp.Utility.Quote(EvaluationSeq) & " And ApplyID = G.EmpID And Result='1' And StepID='3' And IdentityID='4' order by Seq desc),'') As Grade1")
                '    strSQL.AppendLine(" ,ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) as Grade2 ")
                'End If
                'strSQL.AppendLine(" From  GradeBase G left join GradeSignLog GS on G.CompID=GS.CompID and G.GradeDeptID=GS.ApplyID and G.EmpID=GS.EmpID and GS.Seq=" & Seq & " And G.Online='1'")
                'strSQL.AppendLine(" Left Join Personal P on G.CompID = P.CompID and G.EmpID = P.EmpID ")
                'If MainFlag = "2" Then
                '    strSQL.AppendLine(" Left join GradeSignLog GS1 on G.CompID=GS1.CompID and G.GradeDeptID=GS1.ApplyID and G.EmpID=GS1.EmpID and GS1.Seq=" & Seq - 1)
                'End If
                'strSQL.AppendLine(" Left join GradeEmpComment GE on GS.CompID=GE.CompID and GS.ApplyTime=GE.ApplyTime and GS.ApplyID=GE.ApplyID and GS.EmpID=GE.EmpID and GS.Seq=GE.Seq and GS.GradeSeq=GE.GradeSeq ")
                'strSQL.AppendLine(" Where G.CompID = " & Bsp.Utility.Quote(CompID))
                'strSQL.AppendLine(" And G.GradeYear = " & Bsp.Utility.Quote(GradeYear))
                'strSQL.AppendLine(" And G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
                'strSQL.AppendLine(" And GS.ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
                'strSQL.AppendLine(" And GS.Seq = " & Bsp.Utility.Quote(Seq))
                'If MainFlag = "2" And DeptEx = "N" Then
                '    strSQL.AppendLine(" And G.UpOrganID =" & Bsp.Utility.Quote(ApplyID))
                'Else
                '    strSQL.AppendLine(" And G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
                'End If
                'strSQL.AppendLine(" And G.GradeDeptID = G.OrderDeptID ")
                'strSQL.AppendLine(" and ( ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) in ('9','1','7','3','4') ")
                'strSQL.AppendLine(" Or ( ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,''))<>'' and ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,''))<> isnull((Select top 1 FinalScoreValue From EvaluationSignLog Where CompID=G.CompID And EvaluationYear = " & Bsp.Utility.Quote(GradeYear) & " And EvaluationSeq = " & Bsp.Utility.Quote(EvaluationSeq) & " And ApplyID = G.EmpID And Result='1' And StepID='3' And IdentityID='4' order by Seq desc),'')")
                'strSQL.AppendLine(" And isnull((Select top 1 FinalScoreValue From EvaluationSignLog Where CompID=G.CompID And EvaluationYear = " & Bsp.Utility.Quote(GradeYear) & " And EvaluationSeq = " & Bsp.Utility.Quote(EvaluationSeq) & " And ApplyID = G.EmpID And Result='1' And StepID='3' And IdentityID='4' order by Seq desc),'')<>''")
                'strSQL.AppendLine(" ))")
                ' ''前20%
                ''strSQL.AppendLine(" Select Top " & topDataCount & " G.CompID,G.EmpID,P.NameN,GS.ApplyID ")
                ''strSQL.AppendLine(" ,GS.GradeOrder ")
                ''strSQL.AppendLine(" ,isnull(GE.Comment,'') as Comment ")
                ''strSQL.AppendLine(" From  GradeBase G left join GradeSignLog GS on G.CompID=GS.CompID and G.GradeDeptID=GS.ApplyID and G.EmpID=GS.EmpID and GS.Seq=" & Seq & " And G.Online='1'")
                ''strSQL.AppendLine(" Left Join Personal P on G.CompID = P.CompID and G.EmpID = P.EmpID ")
                ''strSQL.AppendLine(" Left join GradeEmpComment GE on GS.CompID=GE.CompID and GS.ApplyTime=GE.ApplyTime and GS.ApplyID=GE.ApplyID and GS.EmpID=GE.EmpID and GS.Seq=GE.Seq and GS.GradeSeq=GE.GradeSeq ")
                ''strSQL.AppendLine(" Where G.CompID = " & Bsp.Utility.Quote(CompID))
                ''strSQL.AppendLine(" And G.GradeYear = " & Bsp.Utility.Quote(GradeYear))
                ''strSQL.AppendLine(" And G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
                ''strSQL.AppendLine(" And GS.ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
                ''strSQL.AppendLine(" And GS.Seq = " & Bsp.Utility.Quote(Seq))
                ''strSQL.AppendLine(" And G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
                ''strSQL.AppendLine(" And G.GradeDeptID = G.OrderDeptID ")
                ''strSQL.AppendLine(" order By GS.GradeOrder ")
                ''strSQL.AppendLine(" Union")
                ' ''後15%
                ''strSQL.AppendLine(" Select Top " & LastDataCount & " G.CompID,G.EmpID,P.NameN,GS.ApplyID ")
                ''strSQL.AppendLine(" ,GS.GradeOrder ")
                ''strSQL.AppendLine(" ,isnull(GE.Comment,'') as Comment ")
                ''strSQL.AppendLine(" From  GradeBase G left join GradeSignLog GS on G.CompID=GS.CompID and G.GradeDeptID=GS.ApplyID and G.EmpID=GS.EmpID and GS.Seq=" & Seq & " And G.Online='1'")
                ''strSQL.AppendLine(" Left Join Personal P on G.CompID = P.CompID and G.EmpID = P.EmpID ")
                ''strSQL.AppendLine(" Left join GradeEmpComment GE on GS.CompID=GE.CompID and GS.ApplyTime=GE.ApplyTime and GS.ApplyID=GE.ApplyID and GS.EmpID=GE.EmpID and GS.Seq=GE.Seq and GS.GradeSeq=GE.GradeSeq ")
                ''strSQL.AppendLine(" Where G.CompID = " & Bsp.Utility.Quote(CompID))
                ''strSQL.AppendLine(" And G.GradeYear = " & Bsp.Utility.Quote(GradeYear))
                ''strSQL.AppendLine(" And G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
                ''strSQL.AppendLine(" And GS.ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
                ''strSQL.AppendLine(" And GS.Seq = " & Bsp.Utility.Quote(Seq))
                ''strSQL.AppendLine(" And G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
                ''strSQL.AppendLine(" And G.GradeDeptID = G.OrderDeptID ")
                ''strSQL.AppendLine(" order By GS.GradeOrder Desc")
                ''If IsSignNext = "N" Then
                ''    strSQL.AppendLine(" Union")
                ''    '配置考績為優/乙及配置考績與建議考績不一致
                ''    strSQL.AppendLine(" Select G.CompID,G.EmpID,P.NameN,GS.ApplyID ")
                ''    strSQL.AppendLine(" ,GS.GradeOrder ")
                ''    strSQL.AppendLine(" ,isnull(GE.Comment,'') as Comment ")
                ''    strSQL.AppendLine(" From  GradeBase G left join GradeSignLog GS on G.CompID=GS.CompID and G.GradeDeptID=GS.ApplyID and G.EmpID=GS.EmpID and GS.Seq=" & Seq & " And G.Online='1'")
                ''    strSQL.AppendLine(" Left Join Personal P on G.CompID = P.CompID and G.EmpID = P.EmpID ")
                ''    strSQL.AppendLine(" Left join GradeEmpComment GE on GS.CompID=GE.CompID and GS.ApplyTime=GE.ApplyTime and GS.ApplyID=GE.ApplyID and GS.EmpID=GE.EmpID and GS.Seq=GE.Seq and GS.GradeSeq=GE.GradeSeq ")
                ''    strSQL.AppendLine(" Where G.CompID = " & Bsp.Utility.Quote(CompID))
                ''    strSQL.AppendLine(" And G.GradeYear = " & Bsp.Utility.Quote(GradeYear))
                ''    strSQL.AppendLine(" And G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
                ''    strSQL.AppendLine(" And GS.ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
                ''    strSQL.AppendLine(" And GS.Seq = " & Bsp.Utility.Quote(Seq))
                ''    strSQL.AppendLine(" And G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
                ''    strSQL.AppendLine(" And G.GradeDeptID = G.OrderDeptID ")
                ''    strSQL.AppendLine(" And (isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'')='9' or isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'') = '1' or  isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'') = '3' or  isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'') = '4' or isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'') <> isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'') ) ")
                ''End If
                'strSQL.AppendLine(" ) S ")

                'strSQL.AppendLine(" );")
                ''刪除暫存的整體評量說明
                'strSQL.AppendLine("Delete From GradeEmpComment_ScoreAdjust ")
                'strSQL.AppendLine(" Where GradeYear = " & Bsp.Utility.Quote(GradeYear))
                'strSQL.AppendLine(" And ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
                'strSQL.AppendLine(" And ApplyID = " & Bsp.Utility.Quote(ApplyID))
                'strSQL.AppendLine(" And CompID = " & Bsp.Utility.Quote(CompID))
                'strSQL.AppendLine(" And Seq = " & Bsp.Utility.Quote(Seq))
                'strSQL.AppendLine(" And GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
                'strSQL.AppendLine(" And EmpID not In (")
                'strSQL.AppendLine(" Select G.EmpID")
                'strSQL.AppendLine(" From  GradeBase G left join GradeSignLog GS on G.CompID=GS.CompID and G.GradeDeptID=GS.ApplyID and G.EmpID=GS.EmpID and GS.Seq=" & Seq & " And G.Online='1'")
                'strSQL.AppendLine(" Left Join Personal P on G.CompID = P.CompID and G.EmpID = P.EmpID ")
                'strSQL.AppendLine(" Left join GradeEmpComment_ScoreAdjust GE on GS.CompID=GE.CompID and GS.ApplyTime=GE.ApplyTime and GS.ApplyID=GE.ApplyID and GS.EmpID=GE.EmpID and GS.Seq=GE.Seq and GS.GradeSeq=GE.GradeSeq ")
                'strSQL.AppendLine(" Where G.CompID = " & Bsp.Utility.Quote(CompID))
                'strSQL.AppendLine(" And G.GradeYear = " & Bsp.Utility.Quote(GradeYear))
                'strSQL.AppendLine(" And G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
                'strSQL.AppendLine(" And GS.ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
                'strSQL.AppendLine(" And GS.Seq = " & Bsp.Utility.Quote(Seq))
                'strSQL.AppendLine(" And G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
                'strSQL.AppendLine(" And G.GradeDeptID = G.OrderDeptID ")
                'strSQL.AppendLine(" And (GS.Score<>'' and GS.Score<> isnull((Select top 1 FinalScore From EvaluationSignLog Where CompID=G.CompID And EvaluationYear = " & Bsp.Utility.Quote(GradeYear) & " And EvaluationSeq = " & Bsp.Utility.Quote(EvaluationSeq) & " And ApplyID = G.EmpID And Result='1' And StepID='3' And IdentityID='4' order by Seq desc),'')")
                'strSQL.AppendLine(" And isnull((Select top 1 FinalScore From EvaluationSignLog Where CompID=G.CompID And EvaluationYear = " & Bsp.Utility.Quote(GradeYear) & " And EvaluationSeq = " & Bsp.Utility.Quote(EvaluationSeq) & " And ApplyID = G.EmpID And Result='1' And StepID='3' And IdentityID='4' order by Seq desc),'')<>'')")
                'strSQL.AppendLine(" );")

                'Update SingLog
                strSQL.AppendLine("Update SignLog Set SignTime=Getdate() ")
                strSQL.AppendLine(" ,Result='1' ")
                strSQL.AppendLine(" where CompID = " & Bsp.Utility.Quote(CompID))
                strSQL.AppendLine(" and ApplyTime= " & Bsp.Utility.Quote(ApplyTime))
                strSQL.AppendLine(" and ApplyID = " & Bsp.Utility.Quote(ApplyID))
                strSQL.AppendLine(" and Seq = " & Bsp.Utility.Quote(Seq) & ";")
                'Update GradeBase.GradeDept,GradeOrderDept
                strSQL.AppendLine(" Update G Set G.GradeDept=GS.Grade,G.GradeOrderDept=GS.GradeOrder ")
                strSQL.AppendLine(" From GradeBase G  left join GradeSignLog GS on GS.CompID=G.CompID and GS.ApplyID=G.GradeDeptID and GS.GradeSeq=G.GradeSeq and GS.EmpID=G.EmpID and G.Online='1' ")
                strSQL.AppendLine(" Where G.CompID = " & Bsp.Utility.Quote(CompID))
                strSQL.AppendLine(" And G.GradeYear = " & Bsp.Utility.Quote(GradeYear))
                strSQL.AppendLine(" And G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
                strSQL.AppendLine(" And GS.ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
                strSQL.AppendLine(" And G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
                strSQL.AppendLine(" and GS.Seq = " & Bsp.Utility.Quote(Seq))
                strSQL.AppendLine(" And G.GradeDeptID = G.OrderDeptID; ")
                '若是呈區處主管時
                If IsSignNext = "Y" Then
                    '寫入下一筆GradeSignLog
                    strSQL.AppendLine("Insert into GradeSignLog (CompID,ApplyTime,ApplyID,Seq,EmpID,GradeSeq,GradeOrder,Grade,Score,ScoreValue) ")    ',,Grade,GradeOrder2,Grade2
                    If DeptEx = "Y" Then
                        strSQL.AppendLine(" Select GS.CompID,GS.ApplyTime,GS.ApplyID," & Seq + 1 & ",GS.EmpID,GS.GradeSeq,GS.GradeOrder,GS.Grade,GS.Score,GS.ScoreValue")    ',Grade,GradeOrder2,Grade2
                    Else
                        strSQL.AppendLine(" Select GS.CompID,GS.ApplyTime,GS.ApplyID," & Seq + 1 & ",GS.EmpID,GS.GradeSeq,Case When G.GroupID='3' Then GS.GradeOrder Else '0' End,GS.Grade,GS.Score,GS.ScoreValue")    ',Grade,GradeOrder2,Grade2
                    End If
                    strSQL.AppendLine(" from GradeSignLog GS")
                    strSQL.AppendLine(" left join GradeBase G on GS.CompID=G.CompID and GS.ApplyID=G.GradeDeptID and GS.GradeSeq=G.GradeSeq and GS.EmpID=G.EmpID and G.Online='1' And G.GradeDeptID = G.OrderDeptID And G.GradeYear = " & Bsp.Utility.Quote(GradeYear))
                    strSQL.AppendLine(" where GS.CompID = " & Bsp.Utility.Quote(CompID))
                    strSQL.AppendLine(" and GS.ApplyTime= " & Bsp.Utility.Quote(ApplyTime))
                    strSQL.AppendLine(" and GS.ApplyID = " & Bsp.Utility.Quote(ApplyID))
                    strSQL.AppendLine(" and GS.Seq = " & Bsp.Utility.Quote(Seq))
                    strSQL.AppendLine(" and GS.GradeSeq = " & Bsp.Utility.Quote(GradeSeq) & ";")
                    'GradeEmpComment
                    strSQL.AppendLine("Insert into GradeEmpComment (GradeYear,ApplyTime,ApplyID,CompID,EmpID,Seq,GradeSeq,MainFlag,Comment,LastChgComp,LastChgID,LastChgDate) ")
                    strSQL.AppendLine(" Select " & GradeYear & ",ApplyTime,ApplyID,CompID,EmpID," & Seq + 1 & ",GradeSeq,'2',Comment,LastChgComp,LastChgID,LastChgDate")
                    strSQL.AppendLine(" from GradeEmpComment")
                    strSQL.AppendLine(" where CompID = " & Bsp.Utility.Quote(CompID))
                    strSQL.AppendLine(" And GradeYear = " & Bsp.Utility.Quote(GradeYear))
                    strSQL.AppendLine(" and ApplyTime= " & Bsp.Utility.Quote(ApplyTime))
                    strSQL.AppendLine(" and ApplyID = " & Bsp.Utility.Quote(ApplyID))
                    strSQL.AppendLine(" and Seq = " & Bsp.Utility.Quote(Seq))
                    strSQL.AppendLine(" and GradeSeq = " & Bsp.Utility.Quote(GradeSeq) & ";")
                    'Update下一筆SignLog
                    strSQL.AppendLine("Update SignLog Set ArriveTime = getdate() ")
                    strSQL.AppendLine(" ,SignTime='1900/01/01' ")
                    strSQL.AppendLine(" where CompID = " & Bsp.Utility.Quote(CompID))
                    strSQL.AppendLine(" and ApplyTime= " & Bsp.Utility.Quote(ApplyTime))
                    strSQL.AppendLine(" and ApplyID = " & Bsp.Utility.Quote(ApplyID))
                    strSQL.AppendLine(" and Seq = " & Bsp.Utility.Quote(Seq + 1) & ";")

                    'Update下一筆SignLog
                    strSQL.AppendLine("Update SignLog Set ArriveTime = getdate() ")
                    strSQL.AppendLine(" ,SignTime='1900/01/01' ")
                    strSQL.AppendLine(" where CompID = " & Bsp.Utility.Quote(CompID))
                    strSQL.AppendLine(" and ApplyTime= " & Bsp.Utility.Quote(ApplyTime))
                    strSQL.AppendLine(" and ApplyID = " & Bsp.Utility.Quote(ApplyID))
                    strSQL.AppendLine(" and Seq = " & Bsp.Utility.Quote(Seq + 1) & ";")



                    'Insert into MailLog
                    Dim strNextSignIDComp As String = ""
                    Dim strNextSignID As String = ""
                    Dim strNextSignEMail As String = ""
                    Dim strNextOrganName As String = ""
                    Dim strOrderEndDate As String = ""
                    Dim strEMailDateTime As String = Format(Now(), "yyyy/MM/dd HH:mm:ss")
                    Dim strMailSeq As String = objHR.GetMailSeq(strEMailDateTime)

                    '.讀取下一簽核主管EMail
                    Using dt As DataTable = GetSignEMail(CompID, GradeYear, GradeSeq, ApplyTime, ApplyID, Seq)
                        If dt.Rows.Count > 0 Then
                            strNextSignIDComp = dt.Rows(0)("SignIDComp").ToString()
                            strNextSignID = dt.Rows(0)("SignID").ToString()
                            strNextSignEMail = dt.Rows(0)("EMail").ToString()
                            strNextOrganName = dt.Rows(0)("OrganName").ToString()
                            strOrderEndDate = Format(CDate(dt.Rows(0)("OrderEndDate").ToString()), "yyyy/MM/dd")
                        Else
                            tran.Rollback()
                            Return False
                        End If
                    End Using
                    Dim strEMailSubject As String = GradeYear & "年度" & strNextOrganName & "績效考核排序簽核通知"
                    Dim strEMailContent As String = "GradeOrderAssess"
                    strEMailContent = strEMailContent & "||BM@GradeYear||" & GradeYear & "||BM@DeptName||" & strNextOrganName & "||BM@OrderEndDate||" & strOrderEndDate
                    strSQL.AppendLine(" Insert into MailLog (CreateTime, Seq, Sender, AcceptorCompID, Acceptor, EMail, Subject, Content, Attachment, SuccessFlag) ")
                    strSQL.AppendLine(" values (" & Bsp.Utility.Quote(strEMailDateTime))
                    strSQL.AppendLine(" ," & Bsp.Utility.Quote(strMailSeq))
                    strSQL.AppendLine(" ,'人力資源處'")
                    strSQL.AppendLine(" ," & Bsp.Utility.Quote(strNextSignIDComp))
                    strSQL.AppendLine(" ," & Bsp.Utility.Quote(strNextSignID))
                    strSQL.AppendLine(" ," & Bsp.Utility.Quote(strNextSignEMail))
                    strSQL.AppendLine(" ," & Bsp.Utility.Quote(strEMailSubject))
                    strSQL.AppendLine(" ," & Bsp.Utility.Quote(strEMailContent))
                    strSQL.AppendLine(" ,''")
                    strSQL.AppendLine(" ,'0');")
                    '判斷是否全排序且為最後一個送出若是則加送一封MAIL通知區/處主管
                    If DeptEx = "N" Then
                        strWhere = strWhere & " And O.CompID = " & Bsp.Utility.Quote(CompID)
                        strWhere = strWhere & " And S.ApplyTime= " & Bsp.Utility.Quote(ApplyTime)
                        strWhere = strWhere & " And S.ApplyID <>" & Bsp.Utility.Quote(ApplyID)
                        strWhere = strWhere & " And O.UpOrganID=(Select UpOrganID From Organization Where CompID =" & Bsp.Utility.Quote(CompID) & " And OrganID=" & Bsp.Utility.Quote(ApplyID) & ")"
                        strWhere = strWhere & " And Seq =" & Bsp.Utility.Quote(Seq)
                        strWhere = strWhere & " And Result <> '1'"
                        If Not objHR.IsDataExists("SignLog S Inner Join Organization O On S.CompID=O.CompID And S.ApplyID=O.OrganID", strWhere) Then
                            strMailSeq = CStr(CInt(strMailSeq) + 1)
                            '取區/處名稱
                            Using dt As DataTable = GetSignOrganName(CompID, ApplyID)
                                If dt.Rows.Count > 0 Then
                                    strNextOrganName = dt.Rows(0)("OrganName").ToString()
                                End If
                            End Using
                            strEMailSubject = GradeYear & "年度" & strNextOrganName & "績效考核排序簽核通知"
                            strEMailContent = "GradeAssess"
                            strEMailContent = strEMailContent & "||BM@GradeYear||" & GradeYear & "||BM@DeptName||" & strNextOrganName & "||BM@OrderEndDate||" & strOrderEndDate
                            strSQL.AppendLine(" Insert into MailLog (CreateTime, Seq, Sender, AcceptorCompID, Acceptor, EMail, Subject, Content, Attachment, SuccessFlag) ")
                            strSQL.AppendLine(" values (" & Bsp.Utility.Quote(strEMailDateTime))
                            strSQL.AppendLine(" ," & Bsp.Utility.Quote(strMailSeq))
                            strSQL.AppendLine(" ,'人力資源處'")
                            strSQL.AppendLine(" ," & Bsp.Utility.Quote(strNextSignIDComp))
                            strSQL.AppendLine(" ," & Bsp.Utility.Quote(strNextSignID))
                            strSQL.AppendLine(" ," & Bsp.Utility.Quote(strNextSignEMail))
                            strSQL.AppendLine(" ," & Bsp.Utility.Quote(strEMailSubject))
                            strSQL.AppendLine(" ," & Bsp.Utility.Quote(strEMailContent))
                            strSQL.AppendLine(" ,''")
                            strSQL.AppendLine(" ,'0');")
                        End If
                    End If
                Else
                    'Update Application
                    strSQL.AppendLine(" Update Application Set Status = '2' ")
                    strSQL.AppendLine(" Where CompID = " & Bsp.Utility.Quote(CompID))
                    strSQL.AppendLine(" And ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
                    strSQL.AppendLine(" And ApplyID = " & Bsp.Utility.Quote(ApplyID) & ";")

                    'Update GradeBase.Grade,GradeOrder
                    strSQL.AppendLine(" Update G Set G.Grade=GS.Grade,G.GradeOrder=GS.GradeOrder,G.GradeOrderHR=GS.GradeOrder,G.GradeHR=G.GradeAdjust,G.GradeOrder2=GS.GradeOrder,G.Grade2=G.GradeAdjust,G.ScoreValue=GS.ScoreValue,G.Score=GS.Score ")
                    strSQL.AppendLine(" From GradeBase G  left join GradeSignLog GS on GS.CompID=G.CompID and GS.ApplyID=G.GradeDeptID and GS.GradeSeq=G.GradeSeq and GS.EmpID=G.EmpID and G.Online='1' ")
                    strSQL.AppendLine(" Where G.CompID = " & Bsp.Utility.Quote(CompID))
                    strSQL.AppendLine(" And G.GradeYear = " & Bsp.Utility.Quote(GradeYear))
                    strSQL.AppendLine(" And G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
                    strSQL.AppendLine(" And GS.ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
                    strSQL.AppendLine(" and GS.Seq = " & Bsp.Utility.Quote(Seq))
                    strSQL.AppendLine(" And G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
                    strSQL.AppendLine(" And G.GradeDeptID = G.OrderDeptID; ")
                End If


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
#Region "GS1100 年度考核(單位主管排序)-下載"
    Public Function GS1100Download(ByVal CompID As String, ByVal ApplyID As String, ByVal ApplyTime As String, ByVal Seq As String, ByVal Status As String, ByVal MainFlag As String _
                           , ByVal GradeYear As String, ByVal GradeSeq As String, ByVal EvaluationSeq As String, ByVal IsPerformance As String, ByVal IsSignNext As String, Optional GroupID As String = "") As DataTable
        Dim strSQL As New StringBuilder()
        '員編、姓名、到職日、職位、職等、年度整體評量結果、年度排序、(配置考績、考績檢視建議)考核補充說明、整體評量調整說明、業績資料    20160620 wei del
        '員編、姓名、到職日、職位、職等、考績、半年度排序、考核補充說明、業績資料   20160620 wei modify
        strSQL.AppendLine(ConfigurationManager.AppSettings("eHRMSDBDES").ToString)
        strSQL.AppendLine(" Select P.EmpID as '員編',P.NameN as '姓名',Convert(char(19), P.EmpDate, 111) as '到職日' ,Po.Remark as 職位 ,P.RankID as '職等' ")
        '20160719 wei add
        strSQL.AppendLine(" ,'單位主管綜合評量'=(isnull((")
        strSQL.AppendLine(" SELECT DISTINCT Top 1 ")
        strSQL.AppendLine(" ISNULL(EA.Content, '') AS Content ")
        strSQL.AppendLine(" FROM EvaluationSignLog ES ")
        strSQL.AppendLine(" INNER JOIN EvaluationCommentAssess EA ON ES.CompID = EA.CompID AND ES.EvaluationYear = EA.EvaluationYear AND ES.EvaluationSeq = EA.EvaluationSeq AND ES.Seq = EA.Seq AND ES.ApplyID = EA.ApplyID ")
        strSQL.AppendLine(" INNER JOIN EvaluationCommentH EC ON EC.CompID = EA.CompID AND EC.EvaluationYear = EA.EvaluationYear AND EC.EvaluationSeq = EA.EvaluationSeq AND EC.Seq = EA.CommentSeq ")
        strSQL.AppendLine(" WHERE ")
        strSQL.AppendLine(" ES.CompID = P.CompID")
        strSQL.AppendLine(" AND ES.EvaluationYear = " & Bsp.Utility.Quote(GradeYear))
        strSQL.AppendLine(" AND ES.EvaluationSeq = " & Bsp.Utility.Quote(EvaluationSeq))
        strSQL.AppendLine(" AND ES.Result = '1' ")
        strSQL.AppendLine(" AND StepID = '3' ")
        strSQL.AppendLine(" AND IdentityID = '4' ")
        strSQL.AppendLine(" AND EC.Type LIKE '%0%' ")
        strSQL.AppendLine(" AND ES.ApplyID = P.EmpID")
        strSQL.AppendLine(" AND ES.Seq = (SELECT Max(Seq) FROM EvaluationSignLog WHERE CompID = ES.CompID AND EvaluationYear = ES.EvaluationYear AND EvaluationSeq = ES.EvaluationSeq AND ApplyID = ES.ApplyID AND Result = '1' AND StepID = '3' AND IdentityID = '4') ")
        strSQL.AppendLine(" ORDER BY Content DESC ")
        strSQL.AppendLine(" ),''))")
        '20160719 wei add End
        'strSQL.AppendLine(" ,Case When GS.ScoreValue<>0 then Cast(GS.ScoreValue as Varchar(2)) + '-' + GS.Score Else ' ' End as '年度整體評量結果'")   '20160620 wei del
        '20160620 wei del
        'If IsSignNext = "N" Then
        '    strSQL.AppendLine(" ,Case When isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')='1' Then '優' ")
        '    strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')='2' Then '甲' ")
        '    strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')='3' Then '乙' ")
        '    strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')='4' Then '丙' ")
        '    strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')='6' Then '甲上' ")
        '    strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')='7' Then '甲下' ")
        '    strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')='9' Then '特優' ")
        '    strSQL.AppendLine(" Else '' End as '考績' ")
        '    strSQL.AppendLine(" ,Case When isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'')='1' Then '優' ")
        '    strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'')='2' Then '甲' ")
        '    strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'')='3' Then '乙' ")
        '    strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'')='4' Then '丙' ")
        '    strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'')='6' Then '甲上' ")
        '    strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'')='7' Then '甲下' ")
        '    strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'')='9' Then '特優' ")
        '    strSQL.AppendLine(" Else '' End as '考績檢視建議' ")
        'End If
        strSQL.AppendLine(" ,Case When isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')='1' Then '優' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')='2' Then '甲' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')='3' Then '乙' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')='4' Then '丙' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')='6' Then '甲上' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')='7' Then '甲下' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')='9' Then '特優' ")
        strSQL.AppendLine(" Else '' End as '考績' ")
        strSQL.AppendLine(" ,Case when isnull(GS.GradeOrder,0)=0 then '' else Cast(GS.GradeOrder as Varchar(3)) end as '排序'")
        strSQL.AppendLine(" ,isnull(GE.Comment,'') as '考績補充說明'")
        'strSQL.AppendLine(" ,isnull(GEA.Comment,'') as '整體評量調整說明'")    20160620 wei del

        If IsPerformance = "Y" Then
            strSQL.AppendLine(" ,isnull(EP.Fld1,'') as 'Q1\個金業務\房貸撥款量(仟元)',isnull(EP.Fld2,'') as 'Q1\個金業務\信貸撥款量(仟元)',isnull(EP.Fld3,'') as 'Q1\個金業務\總AP(仟點)',isnull(EP.Fld4,'') as 'Q1\個金業務\理財Score Card',isnull(EP.Fld5,'') as 'Q1\理財業務\總AP(仟點)',isnull(EP.Fld6,'') as 'Q1\理財業務\Score Card',isnull(EP.Fld7,'') as 'Q1\法金業務(單位)\總AP(仟點)',isnull(EP.Fld8,'') as 'Q1\法金業務(單位)\單位職系達成率' ")
            strSQL.AppendLine(" ,isnull(EP.Fld9,'') as 'Q2\個金業務\房貸撥款量(仟元)',isnull(EP.Fld10,'') as 'Q2\個金業務\信貸撥款量(仟元)',isnull(EP.Fld11,'') as 'Q2\個金業務\總AP(仟點)',isnull(EP.Fld12,'') as 'Q2\個金業務\理財Score Card',isnull(EP.Fld13,'') as 'Q2\理財業務\總AP(仟點)',isnull(EP.Fld14,'') as 'Q2\理財業務\Score Card',isnull(EP.Fld15,'') as 'Q2\法金業務(單位)\總AP(仟點)',isnull(EP.Fld16,'') as 'Q2\法金業務(單位)\單位職系達成率' ")
            strSQL.AppendLine(" ,isnull(EP.Fld17,'') as 'Q3\個金業務\房貸撥款量(仟元)',isnull(EP.Fld18,'') as 'Q3\個金業務\信貸撥款量(仟元)',isnull(EP.Fld19,'') as 'Q3\個金業務\總AP(仟點)',isnull(EP.Fld20,'') as 'Q3\個金業務\理財Score Card',isnull(EP.Fld21,'') as 'Q3\理財業務\總AP(仟點)',isnull(EP.Fld22,'') as 'Q3\理財業務\Score Card',isnull(EP.Fld23,'') as 'Q3\法金業務(單位)\總AP(仟點)',isnull(EP.Fld24,'') as 'Q3\法金業務(單位)\單位職系達成率' ")
            strSQL.AppendLine(" ,isnull(EP.Fld25,'') as '" & GradeYear & "年度平均\個金業務\房貸撥款量(仟元)',isnull(EP.Fld26,'') as '" & GradeYear & "年度平均\個金業務\信貸撥款量(仟元)',isnull(EP.Fld27,'') as '" & GradeYear & "年度平均\個金業務\總AP(仟點)',isnull(EP.Fld28,'') as '" & GradeYear & "年度平均\個金業務\理財Score Card',isnull(EP.Fld29,'') as '" & GradeYear & "年度平均\理財業務\總AP(仟點)',isnull(EP.Fld30,'') as '" & GradeYear & "年度平均\理財業務\Score Card',isnull(EP.Fld31,'') as '" & GradeYear & "年度平均\法金業務(單位)\總AP(仟點)',isnull(EP.Fld32,'') as '" & GradeYear & "年度平均\法金業務(單位)\單位職系達成率' ")
        End If
        strSQL.AppendLine(" from Personal P inner join GradeBase G on G.GradeYear = " & Bsp.Utility.Quote(GradeYear) & " and P.CompID =G.CompID and P.EmpID = G.EmpID and G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq) & " And G.Online='1'")
        strSQL.AppendLine(" left join Organization O on O.CompID = G.CompID and O.OrganID = G.OrderDeptID")
        strSQL.AppendLine(" left join GradeSignLog GS on GS.CompID= " & Bsp.Utility.Quote(CompID) & " and GS.ApplyTime= " & Bsp.Utility.Quote(ApplyTime) & " and GS.ApplyID= G.GradeDeptID and GS.Seq= " & Bsp.Utility.Quote(Seq) & " and P.CompID =GS.CompID and P.EmpID = GS.EmpID and G.GradeSeq = GS.GradeSeq")
        strSQL.AppendLine(" left join GradeEmpComment GE on GE.CompID= " & Bsp.Utility.Quote(CompID) & " and GE.ApplyTime= " & Bsp.Utility.Quote(ApplyTime) & " and GE.ApplyID= G.GradeDeptID and GE.Seq= " & Bsp.Utility.Quote(Seq) & " and P.CompID =GE.CompID and P.EmpID = GE.EmpID and G.GradeSeq = GE.GradeSeq")
        strSQL.AppendLine(" left join GradeEmpComment_ScoreAdjust GEA on GEA.CompID= " & Bsp.Utility.Quote(CompID) & " and GEA.ApplyTime= " & Bsp.Utility.Quote(ApplyTime) & " and GEA.ApplyID= G.GradeDeptID and GEA.Seq= " & Bsp.Utility.Quote(Seq) & " and P.CompID =GEA.CompID and P.EmpID = GEA.EmpID and G.GradeSeq = GEA.GradeSeq")
        strSQL.AppendLine(" left join Title T on P.CompID = T.CompID and P.RankID = T.RankID and P.TitleID = T.TitleID")
        strSQL.AppendLine(" left join EmpPosition EP1 on P.CompID = EP1.CompID and P.EmpID = EP1.EmpID and EP1.PrincipalFlag = '1'")
        strSQL.AppendLine(" left join Position Po on EP1.CompID = Po.CompID and EP1.PositionID = Po.PositionID ")
        strSQL.AppendLine(" left join EmpWorkType EW on P.CompID = EW.CompID and P.EmpID = EW.EmpID and EW.PrincipalFlag = '1'")
        strSQL.AppendLine(" left join WorkType W on EW.CompID = W.CompID and EW.WorkTypeID = W.WorkTypeID")
        strSQL.AppendLine(" left join EmpPerformance EP on P.CompID = EP.CompID and P.EmpID = EP.EmpID and EP.EPYear = " & Bsp.Utility.Quote(GradeYear))
        strSQL.AppendLine(" where G.CompID = " & Bsp.Utility.Quote(CompID) & " and G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID) & " and G.GradeDeptID = G.OrderDeptID and P.EmpID <> " & Bsp.Utility.Quote(UserProfile.UserID))
        If GroupID <> "" And GroupID <> "0" Then
            strSQL.AppendLine(" And G.GroupID=" & Bsp.Utility.Quote(GroupID))
        End If
        strSQL.Append("Order by Case when ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) = '9' then '0' when ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) = '6' then '1.9' when ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) = '7' then '2.1' when ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) = '' then '99' else ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) End")
        strSQL.AppendLine(", Case when GS.GradeOrder = 0 then 1 else 0 end ,Cast(GS.GradeOrder as int) ,Po.PositionID ,P.RankIDMap desc ,P.EmpID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region
#Region "GS1100 年度考核(單位主管排序)-專用格式下載"
    Public Function GS1100DownloadSample(ByVal CompID As String, ByVal ApplyID As String, ByVal ApplyTime As String, ByVal Seq As String, ByVal Status As String, ByVal MainFlag As String _
                           , ByVal GradeYear As String, ByVal GradeSeq As String, ByVal IsSignNext As String, ByVal EvaluationSeq As String, Optional GroupID As String = "") As DataTable
        Dim strSQL As New StringBuilder()
        '項次,員編,姓名,單位,到職日,職位,職等,年度整體評量結果,年度排序
        strSQL.AppendLine(ConfigurationManager.AppSettings("eHRMSDBDES").ToString)
        strSQL.AppendLine(" Select P.EmpID as '員編',P.NameN as '姓名',O.OrganName as '單位',Convert(char(19), P.EmpDate, 111) as '到職日' ,Po.Remark as 職位 ,P.RankID as '職等' ") 'Row_Number() Over(Order By P.EmpID) As '項次',20160713 wei modify
        '20160719 wei add
        strSQL.AppendLine(" ,'單位主管綜合評量'=(isnull((")
        strSQL.AppendLine(" SELECT DISTINCT Top 1 ")
        strSQL.AppendLine(" ISNULL(EA.Content, '') AS Content ")
        strSQL.AppendLine(" FROM EvaluationSignLog ES ")
        strSQL.AppendLine(" INNER JOIN EvaluationCommentAssess EA ON ES.CompID = EA.CompID AND ES.EvaluationYear = EA.EvaluationYear AND ES.EvaluationSeq = EA.EvaluationSeq AND ES.Seq = EA.Seq AND ES.ApplyID = EA.ApplyID ")
        strSQL.AppendLine(" INNER JOIN EvaluationCommentH EC ON EC.CompID = EA.CompID AND EC.EvaluationYear = EA.EvaluationYear AND EC.EvaluationSeq = EA.EvaluationSeq AND EC.Seq = EA.CommentSeq ")
        strSQL.AppendLine(" WHERE ")
        strSQL.AppendLine(" ES.CompID = P.CompID")
        strSQL.AppendLine(" AND ES.EvaluationYear = " & Bsp.Utility.Quote(GradeYear))
        strSQL.AppendLine(" AND ES.EvaluationSeq = " & Bsp.Utility.Quote(EvaluationSeq))
        strSQL.AppendLine(" AND ES.Result = '1' ")
        strSQL.AppendLine(" AND StepID = '3' ")
        strSQL.AppendLine(" AND IdentityID = '4' ")
        strSQL.AppendLine(" AND EC.Type LIKE '%0%' ")
        strSQL.AppendLine(" AND ES.ApplyID = P.EmpID")
        strSQL.AppendLine(" AND ES.Seq = (SELECT Max(Seq) FROM EvaluationSignLog WHERE CompID = ES.CompID AND EvaluationYear = ES.EvaluationYear AND EvaluationSeq = ES.EvaluationSeq AND ApplyID = ES.ApplyID AND Result = '1' AND StepID = '3' AND IdentityID = '4') ")
        strSQL.AppendLine(" ORDER BY Content DESC ")
        strSQL.AppendLine(" ),''))")
        '20160719 wei add End
        strSQL.AppendLine(" ,Case When isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')='1' Then '優' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')='2' Then '甲' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')='3' Then '乙' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')='4' Then '丙' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')='6' Then '甲上' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')='7' Then '甲下' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')='9' Then '特優' ")
        strSQL.AppendLine(" Else '' End as '考績' ")
        strSQL.AppendLine(" ,Case when isnull(GS.GradeOrder,0)=0 then '' else Cast(GS.GradeOrder as Varchar(3)) end as '排序'")
        strSQL.AppendLine(" ,isnull(GE.Comment,'') as '考績補充說明'")
        strSQL.AppendLine(" from Personal P inner join GradeBase G on G.GradeYear = " & Bsp.Utility.Quote(GradeYear) & " and P.CompID =G.CompID and P.EmpID = G.EmpID and G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq) & " And G.Online='1'")
        strSQL.AppendLine(" left join Organization O on O.CompID = G.CompID and O.OrganID = G.OrderDeptID")
        strSQL.AppendLine(" left join GradeSignLog GS on GS.CompID= " & Bsp.Utility.Quote(CompID) & " and GS.ApplyTime= " & Bsp.Utility.Quote(ApplyTime) & " and GS.ApplyID= G.GradeDeptID and GS.Seq= " & Bsp.Utility.Quote(Seq) & " and P.CompID =GS.CompID and P.EmpID = GS.EmpID and G.GradeSeq = GS.GradeSeq")
        strSQL.AppendLine(" left join GradeEmpComment GE on GE.CompID= " & Bsp.Utility.Quote(CompID) & " and GE.ApplyTime= " & Bsp.Utility.Quote(ApplyTime) & " and GE.ApplyID= G.GradeDeptID and GE.Seq= " & Bsp.Utility.Quote(Seq) & " and P.CompID =GE.CompID and P.EmpID = GE.EmpID and G.GradeSeq = GE.GradeSeq")
        strSQL.AppendLine(" left join Title T on P.CompID = T.CompID and P.RankID = T.RankID and P.TitleID = T.TitleID")
        strSQL.AppendLine(" left join EmpPosition EP1 on P.CompID = EP1.CompID and P.EmpID = EP1.EmpID and EP1.PrincipalFlag = '1'")
        strSQL.AppendLine(" left join Position Po on EP1.CompID = Po.CompID and EP1.PositionID = Po.PositionID ")
        strSQL.AppendLine(" left join EmpWorkType EW on P.CompID = EW.CompID and P.EmpID = EW.EmpID and EW.PrincipalFlag = '1'")
        strSQL.AppendLine(" left join WorkType W on EW.CompID = W.CompID and EW.WorkTypeID = W.WorkTypeID")
        strSQL.AppendLine(" left join EmpPerformance EP on P.CompID = EP.CompID and P.EmpID = EP.EmpID and EP.EPYear = " & Bsp.Utility.Quote(GradeYear))
        strSQL.AppendLine(" where G.CompID = " & Bsp.Utility.Quote(CompID) & " and G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID) & " and G.GradeDeptID = G.OrderDeptID and P.EmpID <> " & Bsp.Utility.Quote(UserProfile.UserID))
        If GroupID <> "" And GroupID <> "0" Then
            strSQL.AppendLine(" And G.GroupID=" & Bsp.Utility.Quote(GroupID))
        End If
        strSQL.Append("Order by Case when ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) = '9' then '0' when ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) = '6' then '1.9' when ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) = '7' then '2.1' when ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) = '' then '99' else ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) End")
        strSQL.AppendLine(", Case when GS.GradeOrder = 0 then 1 else 0 end ,Cast(GS.GradeOrder as int) ,Po.PositionID ,P.RankIDMap desc ,P.EmpID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region
#End Region

#Region "GS1140 資料查詢"
    Public Function QueryEmpPerformance(ByVal CompID As String, ByVal EmpID As String, ByVal EPYear As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" SELECT ")
        strSQL.AppendLine(" Fld1, Fld2, Fld3, Fld4, Fld5, Fld6, Fld7, Fld8, Fld9, Fld10 ")
        strSQL.AppendLine(" , Fld11, Fld12, Fld13, Fld14, Fld15, Fld16, Fld17, Fld18, Fld19, Fld20 ")
        strSQL.AppendLine(" , Fld21, Fld22, Fld23, Fld24, Fld25, Fld26, Fld27, Fld28, Fld29, Fld30, Fld31, Fld32 ")
        strSQL.AppendLine(" FROM EmpPerformance ")
        strSQL.AppendLine(" WHERE CompID = " & Bsp.Utility.Quote(CompID) & " AND EmpID = " & Bsp.Utility.Quote(EmpID) & " AND EPYear = " & Bsp.Utility.Quote(EPYear))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "GS1150 整體評量調整說明畫面"
#Region "GS1150 整體評量調整說明畫面-查詢"
    Public Function GS1150Query(ByVal CompID As String, ByVal ApplyID As String, ByVal ApplyTime As String, ByVal Seq As String, ByVal GradeYear As String, ByVal GradeSeq As String _
                                           , ByVal EvaluationSeq As String, ByVal MainFlag As String, ByVal DeptEx As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" Select G.CompID,G.EmpID,P.NameN,GS.ApplyID ")
        strSQL.AppendLine(" ,isnull(GS.GradeOrder,0) as GradeOrder ")
        If MainFlag = "2" Then
            strSQL.AppendLine(" ,Cast(GS1.ScoreValue as Varchar(1)) + '-' + GS1.Score as ScoreValue1 ")
            strSQL.AppendLine(" ,Cast(GS.ScoreValue as Varchar(1)) + '-' + GS.Score as ScoreValue2 ")
        Else
            strSQL.AppendLine(" ,Cast(isnull((Select top 1 FinalScoreValue From EvaluationSignLog Where CompID=G.CompID And EvaluationYear = " & Bsp.Utility.Quote(GradeYear) & " And EvaluationSeq = " & Bsp.Utility.Quote(EvaluationSeq) & " And ApplyID = G.EmpID And Result='1' And StepID='3' And IdentityID='4' order by Seq desc),'') as Varchar(1))")
            strSQL.AppendLine(" + '-' + ")
            strSQL.AppendLine(" isnull((Select top 1 FinalScore From EvaluationSignLog Where CompID=G.CompID And EvaluationYear = " & Bsp.Utility.Quote(GradeYear) & " And EvaluationSeq = " & Bsp.Utility.Quote(EvaluationSeq) & " And ApplyID = G.EmpID And Result='1' And StepID='3' And IdentityID='4' order by Seq desc),'') As ScoreValue1")
            strSQL.AppendLine(" ,Cast(GS.ScoreValue as Varchar(1)) + '-' + GS.Score as ScoreValue2 ")
        End If
        strSQL.AppendLine(" ,isnull(GE.Comment,'') as Comment ")
        strSQL.AppendLine(" ,Case When isnull(GE.Comment,'N')='N' Then 'N' Else 'Y' End as IsExit ")
        strSQL.AppendLine(" From  GradeBase G left join GradeSignLog GS on G.CompID=GS.CompID and G.GradeDeptID=GS.ApplyID and G.EmpID=GS.EmpID and GS.Seq=" & Seq & " And G.Online='1'")
        strSQL.AppendLine(" Left Join Personal P on G.CompID = P.CompID and G.EmpID = P.EmpID ")
        If MainFlag = "2" Then
            strSQL.AppendLine(" Left join GradeSignLog GS1 on G.CompID=GS1.CompID and G.GradeDeptID=GS1.ApplyID and G.EmpID=GS1.EmpID and GS1.Seq=" & Seq - 1 & " and GS.ApplyTime=GS1.ApplyTime")
        End If
        strSQL.AppendLine(" Left join GradeEmpComment_ScoreAdjust GE on GS.CompID=GE.CompID and GS.ApplyTime=GE.ApplyTime and GS.ApplyID=GE.ApplyID and GS.EmpID=GE.EmpID and GS.Seq=GE.Seq and GS.GradeSeq=GE.GradeSeq ")
        strSQL.AppendLine(" Where G.CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine(" And G.GradeYear = " & Bsp.Utility.Quote(GradeYear))
        strSQL.AppendLine(" And G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
        strSQL.AppendLine(" And GS.ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
        strSQL.AppendLine(" And GS.Seq = " & Bsp.Utility.Quote(Seq))
        If MainFlag = "2" And DeptEx = "N" Then
            strSQL.AppendLine(" And G.UpOrganID =" & Bsp.Utility.Quote(ApplyID))
        Else
            strSQL.AppendLine(" And G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
        End If
        strSQL.AppendLine(" And G.GradeDeptID = G.OrderDeptID ")
        If MainFlag = "2" Then
            strSQL.AppendLine(" And (isnull(GS1.Score,'')<>'' and GS.Score<> isnull(GS1.Score,''))")
        Else
            strSQL.AppendLine(" And (GS.Score<>'' and GS.Score<> isnull((Select top 1 FinalScore From EvaluationSignLog Where CompID=G.CompID And EvaluationYear = " & Bsp.Utility.Quote(GradeYear) & " And EvaluationSeq = " & Bsp.Utility.Quote(EvaluationSeq) & " And ApplyID = G.EmpID And Result='1' And StepID='3' And IdentityID='4' order by Seq desc),'')")
            strSQL.AppendLine(" And isnull((Select top 1 FinalScore From EvaluationSignLog Where CompID=G.CompID And EvaluationYear = " & Bsp.Utility.Quote(GradeYear) & " And EvaluationSeq = " & Bsp.Utility.Quote(EvaluationSeq) & " And ApplyID = G.EmpID And Result='1' And StepID='3' And IdentityID='4' order by Seq desc),'')<>'')")
        End If

        strSQL.AppendLine(" order by Case when GS.GradeOrder = 0 then 1 else 0 end ,Cast(GS.GradeOrder as int) ")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "GS1150 整體評量調整說明畫面-新增"
    Public Function GS1150Insert(ByVal GradeYear As String, ByVal ApplyTime As String, ByVal ApplyID As String, ByVal CompID As String, ByVal EmpID As String, ByVal Seq As String _
                                 , ByVal GradeSeq As String, ByVal MainFlag As String, ByVal Comment As String) As Boolean
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                strSQL.AppendLine(" Insert into GradeEmpComment_ScoreAdjust (GradeYear,ApplyTime,ApplyID,CompID,EmpID,Seq,GradeSeq,MainFlag,Comment,LastChgComp,LastChgID,LastChgDate) ")
                strSQL.AppendLine(" Values(" & Bsp.Utility.Quote(GradeYear))
                strSQL.AppendLine(" ," & Bsp.Utility.Quote(ApplyTime))
                strSQL.AppendLine(" ," & Bsp.Utility.Quote(ApplyID))
                strSQL.AppendLine(" ," & Bsp.Utility.Quote(CompID))
                strSQL.AppendLine(" ," & Bsp.Utility.Quote(EmpID))
                strSQL.AppendLine(" ," & Bsp.Utility.Quote(Seq))
                strSQL.AppendLine(" ," & Bsp.Utility.Quote(GradeSeq))
                strSQL.AppendLine(" ," & Bsp.Utility.Quote(MainFlag))
                strSQL.AppendLine(" ," & Bsp.Utility.Quote(Comment))
                strSQL.AppendLine(" ," & Bsp.Utility.Quote(UserProfile.CompID))
                strSQL.AppendLine(" ," & Bsp.Utility.Quote(UserProfile.UserID))
                strSQL.AppendLine(" ,getdate())")
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
#Region "GS1150 整體評量調整說明畫面-修改"
    Public Function GS1150Update(ByVal GradeYear As String, ByVal ApplyTime As String, ByVal ApplyID As String, ByVal CompID As String, ByVal EmpID As String, ByVal Seq As String _
                                 , ByVal GradeSeq As String, ByVal MainFlag As String, ByVal Comment As String) As Boolean
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                strSQL.AppendLine(" Update GradeEmpComment_ScoreAdjust Set Comment = " & Bsp.Utility.Quote(Comment))
                strSQL.AppendLine(" ,LastChgComp = " & Bsp.Utility.Quote(UserProfile.CompID))
                strSQL.AppendLine(" ,LastChgID = " & Bsp.Utility.Quote(UserProfile.UserID))
                strSQL.AppendLine(" ,LastChgDate = getdate()")
                strSQL.AppendLine(" Where GradeYear = " & Bsp.Utility.Quote(GradeYear))
                strSQL.AppendLine(" And ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
                strSQL.AppendLine(" And ApplyID = " & Bsp.Utility.Quote(ApplyID))
                strSQL.AppendLine(" And CompID = " & Bsp.Utility.Quote(CompID))
                strSQL.AppendLine(" And EmpID = " & Bsp.Utility.Quote(EmpID))
                strSQL.AppendLine(" And Seq = " & Bsp.Utility.Quote(Seq))
                strSQL.AppendLine(" And GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
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

#Region "GS1160 考核補充說明畫面"
#Region "GS1160 考核補充說明畫面-查詢"
    Public Function GS1160Query(ByVal CompID As String, ByVal ApplyID As String, ByVal ApplyTime As String, ByVal Seq As String, ByVal GradeYear As String, ByVal GradeSeq As String _
                                           , ByVal EvaluationSeq As String, ByVal MainFlag As String, ByVal DeptEx As String, ByVal topDataCount As String, ByVal LastDataCount As String, Optional GroupID As String = "", Optional ShowComment As String = "1") As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(ConfigurationManager.AppSettings("eHRMSDBDES").ToString)
        'strSQL.AppendLine("Select * From ( ")
        '前20%
        'strSQL.AppendLine(" Select Top " & topDataCount & " G.CompID,G.EmpID,P.NameN,GS.ApplyID ")
        strSQL.AppendLine(" Select G.CompID,G.EmpID,P.NameN,GS.ApplyID ")
        strSQL.AppendLine(" ,GS.GradeOrder ")
        strSQL.AppendLine(" ,isnull(GE.Comment,'') as Comment ")
        strSQL.AppendLine(" ,Case When isnull(GE.Comment,'N')='N' Then 'N' Else 'Y' End as IsExit ")
        If MainFlag = "2" Then
            strSQL.AppendLine(" ,ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) as Grade1 ")
            strSQL.AppendLine(" ,ltrim(isnull(CONVERT(char(1), DecryptByKey(GS1.Grade)) ,'')) as Grade2 ")
        Else
            strSQL.AppendLine(" ,isnull((Select top 1 FinalScoreValue From EvaluationSignLog Where CompID=G.CompID And EvaluationYear = " & Bsp.Utility.Quote(GradeYear) & " And EvaluationSeq = " & Bsp.Utility.Quote(EvaluationSeq) & " And ApplyID = G.EmpID And Result='1' And StepID='3' And IdentityID='4' order by Seq desc),'') As Grade1")
            strSQL.AppendLine(" ,ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) as Grade2 ")
        End If
        strSQL.AppendLine(" From  GradeBase G left join GradeSignLog GS on G.CompID=GS.CompID and G.GradeDeptID=GS.ApplyID and G.EmpID=GS.EmpID and GS.Seq=" & Seq & " And G.Online='1'")
        strSQL.AppendLine(" Left Join Personal P on G.CompID = P.CompID and G.EmpID = P.EmpID ")
        If MainFlag = "2" Then
            strSQL.AppendLine(" Left join GradeSignLog GS1 on G.CompID=GS1.CompID and G.GradeDeptID=GS1.ApplyID and GS.ApplyTime=GS1.ApplyTime and G.EmpID=GS1.EmpID and GS1.Seq=" & Seq - 1)
        End If
        strSQL.AppendLine(" Left join GradeEmpComment GE on GS.CompID=GE.CompID and GS.ApplyTime=GE.ApplyTime and GS.ApplyID=GE.ApplyID and GS.EmpID=GE.EmpID and GS.Seq=GE.Seq and GS.GradeSeq=GE.GradeSeq ")
        strSQL.AppendLine(" Where G.CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine(" And G.GradeYear = " & Bsp.Utility.Quote(GradeYear))
        strSQL.AppendLine(" And G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
        strSQL.AppendLine(" And GS.ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
        strSQL.AppendLine(" And GS.Seq = " & Bsp.Utility.Quote(Seq))
        If MainFlag = "2" And DeptEx = "N" Then
            strSQL.AppendLine(" And G.UpOrganID =" & Bsp.Utility.Quote(ApplyID))
        Else
            strSQL.AppendLine(" And G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
        End If
        strSQL.AppendLine(" And G.GradeDeptID = G.OrderDeptID ")
        If ShowComment = "1" Then
            strSQL.AppendLine(" and (ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) in ('9','1','7','3','4') ")
            If MainFlag = "2" Then
                strSQL.AppendLine(" Or (ltrim(isnull(CONVERT(char(1), DecryptByKey(GS1.Grade)) ,'')))<>'' and ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,''))<> ltrim(isnull(CONVERT(char(1), DecryptByKey(GS1.Grade)) ,'')))")
                'strSQL.AppendLine(" or GE.Comment<>'')")
            Else
                strSQL.AppendLine(" Or (ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,''))<>'' and ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,''))<> isnull((Select top 1 FinalScoreValue From EvaluationSignLog Where CompID=G.CompID And EvaluationYear = " & Bsp.Utility.Quote(GradeYear) & " And EvaluationSeq = " & Bsp.Utility.Quote(EvaluationSeq) & " And ApplyID = G.EmpID And Result='1' And StepID='3' And IdentityID='4' order by Seq desc),'')")
                strSQL.AppendLine(" And isnull((Select top 1 FinalScoreValue From EvaluationSignLog Where CompID=G.CompID And EvaluationYear = " & Bsp.Utility.Quote(GradeYear) & " And EvaluationSeq = " & Bsp.Utility.Quote(EvaluationSeq) & " And ApplyID = G.EmpID And Result='1' And StepID='3' And IdentityID='4' order by Seq desc),'')<>''))")
                'strSQL.AppendLine(" or GE.Comment<>'')")
            End If
        End If
       
        If GroupID <> "" And GroupID <> "0" Then
            strSQL.AppendLine(" And G.GroupID = " & Bsp.Utility.Quote(GroupID))
        End If
        If MainFlag = "2" Then
            strSQL.AppendLine(" order By Case when ltrim(isnull(CONVERT(char(1), DecryptByKey(GS1.Grade)) ,'')) = '9' then '0' when ltrim(isnull(CONVERT(char(1), DecryptByKey(GS1.Grade)) ,'')) = '6' then '1.9' when ltrim(isnull(CONVERT(char(1), DecryptByKey(GS1.Grade)) ,'')) = '7' then '2.1' when ltrim(isnull(CONVERT(char(1), DecryptByKey(GS1.Grade)) ,'')) = '' then '99' else ltrim(isnull(CONVERT(char(1), DecryptByKey(GS1.Grade)) ,'')) End,GS1.GradeOrder ")
        Else
            strSQL.AppendLine(" order By Case when ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) = '9' then '0' when ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) = '6' then '1.9' when ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) = '7' then '2.1' when ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) = '' then '99' else ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) End,GS.GradeOrder ")
        End If


        'strSQL.AppendLine(" Union")
        ''後15%
        'strSQL.AppendLine(" Select Top " & LastDataCount & " G.CompID,G.EmpID,P.NameN,GS.ApplyID ")
        'strSQL.AppendLine(" ,GS.GradeOrder ")
        'strSQL.AppendLine(" ,isnull(GE.Comment,'') as Comment ")
        'strSQL.AppendLine(" ,Case When isnull(GE.Comment,'N')='N' Then 'N' Else 'Y' End as IsExit ")
        'strSQL.AppendLine(" From  GradeBase G left join GradeSignLog GS on G.CompID=GS.CompID and G.GradeDeptID=GS.ApplyID and G.EmpID=GS.EmpID and GS.Seq=" & Seq & " And G.Online='1'")
        'strSQL.AppendLine(" Left Join Personal P on G.CompID = P.CompID and G.EmpID = P.EmpID ")
        'If MainFlag = "2" Then
        '    strSQL.AppendLine(" Left join GradeSignLog GS1 on G.CompID=GS1.CompID and G.GradeDeptID=GS1.ApplyID and G.EmpID=GS1.EmpID and GS1.Seq=" & Seq - 1)
        'End If
        'strSQL.AppendLine(" Left join GradeEmpComment GE on GS.CompID=GE.CompID and GS.ApplyTime=GE.ApplyTime and GS.ApplyID=GE.ApplyID and GS.EmpID=GE.EmpID and GS.Seq=GE.Seq and GS.GradeSeq=GE.GradeSeq ")
        'strSQL.AppendLine(" Where G.CompID = " & Bsp.Utility.Quote(CompID))
        'strSQL.AppendLine(" And G.GradeYear = " & Bsp.Utility.Quote(GradeYear))
        'strSQL.AppendLine(" And G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
        'strSQL.AppendLine(" And GS.ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
        'strSQL.AppendLine(" And GS.Seq = " & Bsp.Utility.Quote(Seq))
        'If MainFlag = "2" And DeptEx = "N" Then
        '    strSQL.AppendLine(" And G.UpOrganID =" & Bsp.Utility.Quote(ApplyID))
        'Else
        '    strSQL.AppendLine(" And G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
        'End If
        'strSQL.AppendLine(" And G.GradeDeptID = G.OrderDeptID ")
        'strSQL.AppendLine(" order By GS.GradeOrder Desc")

        'strSQL.AppendLine(" ) S ")
        'strSQL.AppendLine(" order by Case when GradeOrder = 0 then 1 else 0 end ,Cast(GradeOrder as int) ")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "GS1160/GS1210 考核補充說明畫面-新增"
    Public Function GS1160Insert(ByVal GradeYear As String, ByVal ApplyTime As String, ByVal ApplyID As String, ByVal CompID As String, ByVal EmpID As String, ByVal Seq As String _
                                 , ByVal GradeSeq As String, ByVal MainFlag As String, ByVal Comment As String) As Boolean
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                strSQL.AppendLine(" Insert into GradeEmpComment (GradeYear,ApplyTime,ApplyID,CompID,EmpID,Seq,GradeSeq,MainFlag,Comment,LastChgComp,LastChgID,LastChgDate) ")
                strSQL.AppendLine(" Values(" & Bsp.Utility.Quote(GradeYear))
                strSQL.AppendLine(" ," & Bsp.Utility.Quote(ApplyTime))
                strSQL.AppendLine(" ," & Bsp.Utility.Quote(ApplyID))
                strSQL.AppendLine(" ," & Bsp.Utility.Quote(CompID))
                strSQL.AppendLine(" ," & Bsp.Utility.Quote(EmpID))
                strSQL.AppendLine(" ," & Bsp.Utility.Quote(Seq))
                strSQL.AppendLine(" ," & Bsp.Utility.Quote(GradeSeq))
                strSQL.AppendLine(" ," & Bsp.Utility.Quote(MainFlag))
                strSQL.AppendLine(" ," & Bsp.Utility.Quote(Comment))
                strSQL.AppendLine(" ," & Bsp.Utility.Quote(UserProfile.CompID))
                strSQL.AppendLine(" ," & Bsp.Utility.Quote(UserProfile.UserID))
                strSQL.AppendLine(" ,getdate())")
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

#Region "GS1160/GS1210 考核補充說明畫面-修改"
    Public Function GS1160Update(ByVal GradeYear As String, ByVal ApplyTime As String, ByVal ApplyID As String, ByVal CompID As String, ByVal EmpID As String, ByVal Seq As String _
                                 , ByVal GradeSeq As String, ByVal MainFlag As String, ByVal Comment As String) As Boolean
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                If Comment = "" Then
                    strSQL.AppendLine(" Delete GradeEmpComment ")
                Else
                    strSQL.AppendLine(" Update GradeEmpComment Set Comment = " & Bsp.Utility.Quote(Comment))
                    strSQL.AppendLine(" ,LastChgComp = " & Bsp.Utility.Quote(UserProfile.CompID))
                    strSQL.AppendLine(" ,LastChgID = " & Bsp.Utility.Quote(UserProfile.UserID))
                    strSQL.AppendLine(" ,LastChgDate = getdate()")
                End If

                strSQL.AppendLine(" Where GradeYear = " & Bsp.Utility.Quote(GradeYear))
                strSQL.AppendLine(" And ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
                strSQL.AppendLine(" And ApplyID = " & Bsp.Utility.Quote(ApplyID))
                strSQL.AppendLine(" And CompID = " & Bsp.Utility.Quote(CompID))
                strSQL.AppendLine(" And EmpID = " & Bsp.Utility.Quote(EmpID))
                strSQL.AppendLine(" And Seq = " & Bsp.Utility.Quote(Seq))
                strSQL.AppendLine(" And GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
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
#Region "GS1180 調整排序-查詢"
    Public Function GS1180Query(ByVal CompID As String, ByVal ApplyID As String, ByVal ApplyTime As String, ByVal Seq As String, ByVal MainFlag As String _
                           , ByVal GradeYear As String, ByVal GradeSeq As String, ByVal EvaluationSeq As String, ByVal Grade As String, ByVal DeptEx As String, Optional Group As String = "") As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(ConfigurationManager.AppSettings("eHRMSDBDES").ToString)
        strSQL.AppendLine(" Select P.EmpID , P.NameN + '/' + P.RankID + '/' + Po.Remark as ShowData")
        strSQL.AppendLine(" from Personal P inner join GradeBase G on G.GradeYear = " & Bsp.Utility.Quote(GradeYear) & " and P.CompID =G.CompID and P.EmpID = G.EmpID and G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq) & " And G.Online='1'")
        strSQL.AppendLine(" left join Organization O on O.CompID = G.CompID and O.OrganID = G.OrderDeptID")
        strSQL.AppendLine(" left join GradeSignLog GS on GS.CompID= " & Bsp.Utility.Quote(CompID) & " and GS.ApplyTime= " & Bsp.Utility.Quote(ApplyTime) & " and GS.ApplyID= G.GradeDeptID and GS.Seq= " & Bsp.Utility.Quote(Seq) & " and P.CompID =GS.CompID and P.EmpID = GS.EmpID and G.GradeSeq = GS.GradeSeq")
        strSQL.AppendLine(" left join Title T on P.CompID = T.CompID and P.RankID = T.RankID and P.TitleID = T.TitleID")
        strSQL.AppendLine(" left join EmpPosition EP1 on P.CompID = EP1.CompID and P.EmpID = EP1.EmpID and EP1.PrincipalFlag = '1'")
        strSQL.AppendLine(" left join Position Po on EP1.CompID = Po.CompID and EP1.PositionID = Po.PositionID ")
        strSQL.AppendLine(" left join EmpWorkType EW on P.CompID = EW.CompID and P.EmpID = EW.EmpID and EW.PrincipalFlag = '1'")
        strSQL.AppendLine(" left join WorkType W on EW.CompID = W.CompID and EW.WorkTypeID = W.WorkTypeID")
        strSQL.AppendLine(" where G.CompID = " & Bsp.Utility.Quote(CompID) & " and G.GradeDeptID = G.OrderDeptID and P.EmpID <> " & Bsp.Utility.Quote(UserProfile.UserID))
        If MainFlag = "2" And DeptEx = "N" Then
            strSQL.AppendLine(" And G.UpOrganID =" & Bsp.Utility.Quote(ApplyID))
        Else
            strSQL.AppendLine(" And G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
        End If
        strSQL.AppendLine(" and ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,''))=" & Bsp.Utility.Quote(Grade))
        If Group <> "" And Group <> "0" Then
            strSQL.AppendLine(" and G.GroupID=" & Bsp.Utility.Quote(Group))
        End If
        strSQL.AppendLine("order by Case when GS.GradeOrder = 0 then 1 else 0 end ,Cast(GS.GradeOrder as int) ,Po.PositionID ,P.RankIDMap desc ,P.EmpID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region
#Region "GS1180 調整排序-修改"
    Public Function GS1180Update(ByVal aryEmpID() As String, ByVal CompID As String, ByVal ApplyID As String, ByVal ApplyTime As String, ByVal Seq As String, ByVal GradeSeq As String, ByVal GradeYear As String, ByVal IsSignNext As String) As Boolean
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True
            Dim intOrder As Integer = 1
            Try
                strSQL.AppendLine(ConfigurationManager.AppSettings("eHRMSDBDES").ToString)
                For intLoop As Integer = 0 To aryEmpID.GetUpperBound(0)
                    If aryEmpID(intLoop) <> "" Then
                        strSQL.AppendLine(" Update GradeSignLog ")
                        strSQL.AppendLine(" Set GradeOrder = " & intOrder)
                        strSQL.AppendLine(" ,LastChgComp = " & Bsp.Utility.Quote(UserProfile.CompID))
                        strSQL.AppendLine(" ,LastChgID = " & Bsp.Utility.Quote(UserProfile.UserID))
                        strSQL.AppendLine(" ,LastChgDate = getdate()")
                        strSQL.AppendLine(" Where CompID = " & Bsp.Utility.Quote(CompID))
                        strSQL.AppendLine(" and ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
                        'strSQL.AppendLine(" and ApplyID = " & Bsp.Utility.Quote(ApplyID))
                        strSQL.AppendLine(" and EmpID = " & Bsp.Utility.Quote(aryEmpID(intLoop)))
                        strSQL.AppendLine(" and Seq = " & Bsp.Utility.Quote(Seq))
                        strSQL.AppendLine(" and GradeSeq = " & Bsp.Utility.Quote(GradeSeq) & ";")
                        intOrder = intOrder + 1
                    End If
                Next

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

#Region "GS1200 年度考核(區處主管排序)"
#Region "GS1200 年度考核(區處主管排序)-查詢"
    Public Function GS1200Query(ByVal CompID As String, ByVal ApplyID As String, ByVal ApplyTime As String, ByVal Seq As String, ByVal Status As String, ByVal MainFlag As String _
                           , ByVal GradeYear As String, ByVal GradeSeq As String, ByVal EvaluationSeq As String, ByVal DeptEx As String, Optional GroupID As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(ConfigurationManager.AppSettings("eHRMSDBDES").ToString)
        strSQL.AppendLine(" Select P.CompID,P.EmpID ,P.NameN ,Convert(char(19), P.EmpDate, 111) as EmpDate ,Po.PositionID,Po.Remark as Position ,W.Remark as WorkType ,P.RankID ,P.RankIDMap,isnull(T.TitleName,'') TitleName")
        strSQL.AppendLine(" ,isnull(CONVERT(char(1), DecryptByKey(EG3.Y3Grade)) ,'') as Y3Grade ,isnull(CONVERT(char(1), DecryptByKey(EG2.Y2Grade)),'') as Y2Grade ,isnull(CONVERT(char(1), DecryptByKey(EG1.Y1Grade)),'') as Y1Grade ")
        strSQL.AppendLine(" ,G.GradeDeptID as ApplyID, G.Memo ")
        strSQL.AppendLine(" ,isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'') as Grade ")
        strSQL.AppendLine(" ,cast(Case when isnull(GS.GradeOrder,0)=0 then '' else Cast(GS.GradeOrder as Varchar(3)) end as int) as GradeOrder")
        strSQL.AppendLine(" ,isnull(GS.ScoreValue,'0') as ScoreValue  ,isnull(GS.Score,'') as Score")
        strSQL.AppendLine(" ,isnull(A.Cnt,0) RewardCnt ,isnull(B.Cnt,0) PerformanceCnt ,O.OrganName")
        strSQL.AppendLine(" ,isnull(GS1.ScoreValue,'0') as DeptScoreValue, isnull(GS1.Score,'') as DeptScore ")
        strSQL.AppendLine(" ,isnull(CONVERT(char(1), DecryptByKey(G.GradeDept)) ,'') as GradeDept, G.GradeOrderDept ")
        strSQL.AppendLine(" ,isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'') as GradeAdjust ")
        strSQL.AppendLine(" ,DeptCnt=isnull((Select Count(*) From GradeBase Where CompID=G.CompID And GradeDeptID=G.GradeDeptID And GradeYear=G.GradeYear And GradeSeq=" & Bsp.Utility.Quote(GradeSeq) & " and Online='1' and GroupID=G.GroupID),'') ") '20160720 wei modify
        strSQL.AppendLine(" ,Case When GS.GradeOrder is null then 0 When GE.Comment is null Then 0 Else 1 End as CommentCnt ")
        strSQL.AppendLine(" ,G.GroupID ")   '20160625 wei add
        strSQL.AppendLine(" from Personal P inner join GradeBase G on G.GradeYear = " & Bsp.Utility.Quote(GradeYear) & " and P.CompID =G.CompID and P.EmpID = G.EmpID and G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq) & " And G.Online='1'")
        strSQL.AppendLine(" left join Organization O on O.CompID = G.CompID and O.OrganID = G.OrderDeptID")
        strSQL.AppendLine(" left join GradeSignLog GS on GS.CompID= " & Bsp.Utility.Quote(CompID) & " and GS.ApplyTime= " & Bsp.Utility.Quote(ApplyTime) & " and GS.ApplyID = G.GradeDeptID and GS.Seq= " & Bsp.Utility.Quote(Seq) & " and G.CompID =GS.CompID and G.EmpID = GS.EmpID and G.GradeSeq = GS.GradeSeq")
        strSQL.AppendLine(" left join GradeSignLog GS1 on GS1.CompID= " & Bsp.Utility.Quote(CompID) & " and GS1.ApplyTime= " & Bsp.Utility.Quote(ApplyTime) & " and GS1.ApplyID= G.GradeDeptID and GS1.Seq= " & Bsp.Utility.Quote(Seq - 1) & " and G.CompID =GS1.CompID and G.EmpID = GS1.EmpID and G.GradeSeq = GS1.GradeSeq")
        strSQL.AppendLine(" left join (")
        strSQL.AppendLine(" Select GradeYear,ApplyTime,ApplyID,CompID,EmpID,Seq,GradeSeq,MainFlag,'1' as Comment From GradeEmpComment")
        strSQL.AppendLine(" union")
        strSQL.AppendLine(" Select GradeYear,ApplyTime,ApplyID,CompID,EmpID,Seq,GradeSeq,MainFlag,'1' as Comment From GradeEmpComment_ScoreAdjust")
        strSQL.AppendLine(" ) GE on GE.CompID= " & Bsp.Utility.Quote(CompID) & " and GE.ApplyTime= " & Bsp.Utility.Quote(ApplyTime) & " and GE.ApplyID= G.GradeDeptID and GE.Seq= " & Bsp.Utility.Quote(Seq - 1) & " and P.CompID =GE.CompID and P.EmpID = GE.EmpID and G.GradeSeq = GE.GradeSeq")
        strSQL.AppendLine(" left join Title T on P.CompID = T.CompID and P.RankID = T.RankID and P.TitleID = T.TitleID")
        strSQL.AppendLine(" left join EmpPosition EP1 on P.CompID = EP1.CompID and P.EmpID = EP1.EmpID and EP1.PrincipalFlag = '1'")
        strSQL.AppendLine(" left join Position Po on EP1.CompID = Po.CompID and EP1.PositionID = Po.PositionID ")
        strSQL.AppendLine(" left join EmpWorkType EW on P.CompID = EW.CompID and P.EmpID = EW.EmpID and EW.PrincipalFlag = '1'")
        strSQL.AppendLine(" left join WorkType W on EW.CompID = W.CompID and EW.WorkTypeID = W.WorkTypeID")
        strSQL.AppendLine(" left join ( select CompID,EmpID,Grade Y3Grade from EmpGrade where CompID ='SPHBK1' and GradeYear = " & GradeYear - 3 & ") EG3 on P.CompID = EG3.CompID and P.EmpID = EG3.EmpID")
        strSQL.AppendLine(" left join ( select CompID,EmpID,Grade Y2Grade from EmpGrade where CompID ='SPHBK1' and GradeYear = " & GradeYear - 2 & ") EG2 on P.CompID = EG2.CompID and P.EmpID = EG2.EmpID")
        strSQL.AppendLine(" left join ( select CompID,EmpID,Grade Y1Grade from EmpGrade where CompID ='SPHBK1' and GradeYear = " & GradeYear - 1 & ") EG1 on P.CompID = EG1.CompID and P.EmpID = EG1.EmpID")
        strSQL.AppendLine(" left join ( select distinct CompID,EmpID, 1 Cnt from EmpReward where CompID ='SPHBK1' and year(ValidDate) = " & Bsp.Utility.Quote(GradeYear) & " and EmpID <> '' ) A on P.CompID = A.CompID and P.EmpID = A.EmpID ")
        strSQL.AppendLine(" left join ( select CompID,EmpID, 1 Cnt from EmpPerformance where CompID ='SPHBK1' and EPYear= " & Bsp.Utility.Quote(GradeYear) & ") B on P.CompID = B.CompID and P.EmpID = B.EmpID")
        strSQL.AppendLine(" where G.CompID = " & Bsp.Utility.Quote(CompID))
        If DeptEx = "Y" Then
            strSQL.AppendLine(" and G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
        Else
            strSQL.AppendLine(" And G.UpOrganID =" & Bsp.Utility.Quote(ApplyID))
        End If
        strSQL.AppendLine(" and G.GradeDeptID = G.OrderDeptID and P.EmpID <> " & Bsp.Utility.Quote(UserProfile.UserID))
        If GroupID <> "" And GroupID <> "0" Then
            strSQL.AppendLine(" And G.GroupID=" & Bsp.Utility.Quote(GroupID))
        End If
        strSQL.Append("Order by Case when ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) = '9' then '0' when ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) = '6' then '1.9' when ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) = '7' then '2.1' when ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) = '' then '99' else ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) End")
        strSQL.AppendLine(",Case when isnull(GS.GradeOrder,0) = 0 then 1 else 0 end ,Cast(isnull(GS.GradeOrder,0) as int) ,Cast(isnull(G.GradeOrderDept,0) as int),Po.PositionID ,P.RankIDMap desc ,P.EmpID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region
#Region "GS1200 年度考核(區處主管排序)-查詢筆數"
    Public Function GS1200QueryCount(ByVal CompID As String, ByVal ApplyID As String, ByVal ApplyTime As String, ByVal Seq As String, ByVal Status As String, ByVal MainFlag As String _
                           , ByVal GradeYear As String, ByVal GradeSeq As String, ByVal EvaluationSeq As String, ByVal DeptEx As String, Optional GroupID As String = "") As Integer
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" Select Count(P.CompID) as Cnt")
        strSQL.AppendLine(" from Personal P inner join GradeBase G on G.GradeYear = " & Bsp.Utility.Quote(GradeYear) & " and P.CompID =G.CompID and P.EmpID = G.EmpID and G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq) & " And G.Online='1'")
        strSQL.AppendLine(" where G.CompID = " & Bsp.Utility.Quote(CompID))
        If DeptEx = "Y" Then
            strSQL.AppendLine(" and G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
        Else
            strSQL.AppendLine(" And G.UpOrganID =" & Bsp.Utility.Quote(ApplyID))
        End If
        strSQL.AppendLine(" and G.GradeDeptID = G.OrderDeptID and P.EmpID <> " & Bsp.Utility.Quote(UserProfile.UserID))
        If GroupID <> "" And GroupID <> "0" Then
            strSQL.AppendLine(" And G.GroupID=" & Bsp.Utility.Quote(GroupID))
        End If

        Return Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")

    End Function
#End Region
#Region "GS1200 年度考核(區處主管排序)-修改"
    Public Function GS1200Update(ByVal dt As DataTable, ByVal ApplyID As String, ByVal ApplyTime As String, ByVal Seq As String, ByVal GradeSeq As String, ByVal GradeYear As String) As Boolean
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                strSQL.AppendLine(ConfigurationManager.AppSettings("eHRMSDBDES").ToString)
                For i As Integer = 0 To dt.Rows.Count - 1
                    strSQL.AppendLine(" Update GradeSignLog ")
                    strSQL.AppendLine(" Set Grade = EncryptByKey(Key_GUID('eHRMSDBDES'),'" & dt.Rows(i)("Grade").ToString().Trim & "')")    '20160607 wei add
                    '20160607 wei del
                    'strSQL.AppendLine(" Set GradeOrder = " & dt.Rows(i)("GradeOrder"))
                    'strSQL.AppendLine(" ,ScoreValue = " & dt.Rows(i)("ScoreValue"))
                    'strSQL.AppendLine(" ,Score = " & Bsp.Utility.Quote(dt.Rows(i)("Score")))
                    strSQL.AppendLine(" ,LastChgComp = " & Bsp.Utility.Quote(UserProfile.CompID))
                    strSQL.AppendLine(" ,LastChgID = " & Bsp.Utility.Quote(UserProfile.UserID))
                    strSQL.AppendLine(" ,LastChgDate = getdate()")
                    strSQL.AppendLine(" Where CompID = " & Bsp.Utility.Quote(dt.Rows(i)("CompID")))
                    strSQL.AppendLine(" and ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
                    strSQL.AppendLine(" and ApplyID = " & Bsp.Utility.Quote(dt.Rows(i)("ApplyID")))
                    strSQL.AppendLine(" and EmpID = " & Bsp.Utility.Quote(dt.Rows(i)("EmpID")))
                    strSQL.AppendLine(" and Seq = " & Bsp.Utility.Quote(Seq))
                    strSQL.AppendLine(" and GradeSeq = " & Bsp.Utility.Quote(GradeSeq) & ";")
                    '2016067 wei del
                    ''建議考績
                    'strSQL.AppendLine(" Update GradeBase ")
                    'strSQL.AppendLine(" Set GradeAdjust = EncryptByKey(Key_GUID('eHRMSDBDES'),'" & dt.Rows(i)("GradeAdjust").ToString().Trim & "')")
                    'strSQL.AppendLine(" ,LastChgComp = " & Bsp.Utility.Quote(UserProfile.CompID))
                    'strSQL.AppendLine(" ,LastChgID = " & Bsp.Utility.Quote(UserProfile.UserID))
                    'strSQL.AppendLine(" ,LastChgDate = getdate()")
                    'strSQL.AppendLine(" Where CompID = " & Bsp.Utility.Quote(dt.Rows(i)("CompID")))
                    'strSQL.AppendLine(" and GradeYear = " & Bsp.Utility.Quote(GradeYear))
                    'strSQL.AppendLine(" and GradeDeptID = " & Bsp.Utility.Quote(dt.Rows(i)("ApplyID")))
                    'strSQL.AppendLine(" and EmpID = " & Bsp.Utility.Quote(dt.Rows(i)("EmpID")))
                    'strSQL.AppendLine(" and GradeSeq = " & Bsp.Utility.Quote(GradeSeq) & ";")
                Next

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
#Region "GS1200 年度考核(區處主管排序)-產生區處考績"
    Public Function GS1200GradeData(ByVal CompID As String, ByVal ApplyID As String, ByVal ApplyTime As String, ByVal Seq As String, ByVal GradeYear As String, ByVal GradeSeq As String _
                                           , ByVal MainFlag As String, ByVal DeptGrade As String, ByVal intDeptCount() As Integer, ByVal strGrade() As String, ByVal intTotalCount As Integer, ByVal IsSignNext As String, ByVal DeptEX As String) As Boolean
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                '刪除GradeDispose
                strSQL.AppendLine(ConfigurationManager.AppSettings("eHRMSDBDES").ToString)
                strSQL.AppendLine("　Delete from GradeDispose ")
                strSQL.AppendLine(" where CompID = " & Bsp.Utility.Quote(CompID))
                strSQL.AppendLine(" and DeptID =" & Bsp.Utility.Quote(ApplyID))
                strSQL.AppendLine(" and GradeYear =" & Bsp.Utility.Quote(GradeYear))
                strSQL.AppendLine(" and GradeSeq =" & Bsp.Utility.Quote(GradeSeq))
                strSQL.AppendLine(" and MainFlag= " & Bsp.Utility.Quote(MainFlag) & "; ")
                'Insert GradeDispose
                For intGradeID = 0 To UBound(intDeptCount)
                    If intDeptCount(intGradeID) > 0 Then
                        strSQL.AppendLine(" Insert into GradeDispose (GradeYear, GradeSeq, CompID, DeptID, GradeID, DeptCount, MainFlag,LastChgComp, LastChgID, LastChgDate)  ")
                        strSQL.AppendLine(" values(" & Bsp.Utility.Quote(GradeYear))
                        strSQL.AppendLine(" ," & Bsp.Utility.Quote(GradeSeq))
                        strSQL.AppendLine(" ," & Bsp.Utility.Quote(CompID))
                        strSQL.AppendLine(" ," & Bsp.Utility.Quote(ApplyID))
                        strSQL.AppendLine(" ," & Bsp.Utility.Quote(strGrade(intGradeID)))
                        strSQL.AppendLine(" ," & intDeptCount(intGradeID))
                        strSQL.AppendLine(" ," & Bsp.Utility.Quote(MainFlag))
                        strSQL.AppendLine(" ," & Bsp.Utility.Quote(UserProfile.CompID))
                        strSQL.AppendLine(" ," & Bsp.Utility.Quote(UserProfile.UserID))
                        strSQL.AppendLine(" ,getdate());")
                    End If
                Next
                'Update GradeDept
                strSQL.AppendLine(" Update GradeDept set TotalCount =" & intTotalCount)
                strSQL.AppendLine(" where CompID = " & Bsp.Utility.Quote(CompID))
                strSQL.AppendLine(" and GradeYear =" & Bsp.Utility.Quote(GradeYear))
                strSQL.AppendLine(" and GradeSeq =" & Bsp.Utility.Quote(GradeSeq))
                strSQL.AppendLine(" and MainFlag= " & Bsp.Utility.Quote(MainFlag))
                strSQL.AppendLine(" and DeptID =" & Bsp.Utility.Quote(ApplyID) & ";")
                'Update GradeSignLog.Grade
                '9-特優
                strSQL.AppendLine(" Update GS Set Grade=EncryptByKey(Key_GUID('eHRMSDBDES'),'9')")
                strSQL.AppendLine(" ,LastChgComp = " & Bsp.Utility.Quote(UserProfile.CompID))
                strSQL.AppendLine(" ,LastChgID = " & Bsp.Utility.Quote(UserProfile.UserID))
                strSQL.AppendLine(" ,LastChgDate = getdate()")
                strSQL.AppendLine(" From  GradeBase G left join GradeSignLog GS on G.CompID=GS.CompID and G.GradeDeptID=GS.ApplyID and G.EmpID=GS.EmpID and GS.Seq=" & Seq & " And G.Online='1'")
                strSQL.AppendLine(" Where G.CompID = " & Bsp.Utility.Quote(CompID))
                strSQL.AppendLine(" and GS.ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
                If DeptEX = "N" Then
                    strSQL.AppendLine(" And G.UpOrganID =" & Bsp.Utility.Quote(ApplyID))
                Else
                    strSQL.AppendLine(" And G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
                End If
                strSQL.AppendLine(" and G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
                strSQL.AppendLine(" and GS.GradeOrder <= " & intDeptCount(6) & ";")
                '1-優
                strSQL.AppendLine(" Update GS Set Grade=EncryptByKey(Key_GUID('eHRMSDBDES'),'1')")
                strSQL.AppendLine(" ,LastChgComp = " & Bsp.Utility.Quote(UserProfile.CompID))
                strSQL.AppendLine(" ,LastChgID = " & Bsp.Utility.Quote(UserProfile.UserID))
                strSQL.AppendLine(" ,LastChgDate = getdate()")
                strSQL.AppendLine(" From  GradeBase G left join GradeSignLog GS on G.CompID=GS.CompID and G.GradeDeptID=GS.ApplyID and G.EmpID=GS.EmpID and GS.Seq=" & Seq & " And G.Online='1'")
                strSQL.AppendLine(" Where G.CompID = " & Bsp.Utility.Quote(CompID))
                strSQL.AppendLine(" and GS.ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
                If DeptEX = "N" Then
                    strSQL.AppendLine(" And G.UpOrganID =" & Bsp.Utility.Quote(ApplyID))
                Else
                    strSQL.AppendLine(" And G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
                End If
                strSQL.AppendLine(" and G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
                strSQL.AppendLine(" and GS.GradeOrder > " & intDeptCount(6) & ";")
                '6-甲上
                strSQL.AppendLine(" Update GS Set Grade=EncryptByKey(Key_GUID('eHRMSDBDES'),'6')")
                strSQL.AppendLine(" ,LastChgComp = " & Bsp.Utility.Quote(UserProfile.CompID))
                strSQL.AppendLine(" ,LastChgID = " & Bsp.Utility.Quote(UserProfile.UserID))
                strSQL.AppendLine(" ,LastChgDate = getdate()")
                strSQL.AppendLine(" From  GradeBase G left join GradeSignLog GS on G.CompID=GS.CompID and G.GradeDeptID=GS.ApplyID and G.EmpID=GS.EmpID and GS.Seq=" & Seq & " And G.Online='1'")
                strSQL.AppendLine(" Where G.CompID = " & Bsp.Utility.Quote(CompID))
                strSQL.AppendLine(" and GS.ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
                If DeptEX = "N" Then
                    strSQL.AppendLine(" And G.UpOrganID =" & Bsp.Utility.Quote(ApplyID))
                Else
                    strSQL.AppendLine(" And G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
                End If
                strSQL.AppendLine(" and G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
                strSQL.AppendLine(" and GS.GradeOrder > " & intDeptCount(6) + intDeptCount(0) & ";")
                '2-甲
                strSQL.AppendLine(" Update GS Set Grade=EncryptByKey(Key_GUID('eHRMSDBDES'),'2')")
                strSQL.AppendLine(" ,LastChgComp = " & Bsp.Utility.Quote(UserProfile.CompID))
                strSQL.AppendLine(" ,LastChgID = " & Bsp.Utility.Quote(UserProfile.UserID))
                strSQL.AppendLine(" ,LastChgDate = getdate()")
                strSQL.AppendLine(" From  GradeBase G left join GradeSignLog GS on G.CompID=GS.CompID and G.GradeDeptID=GS.ApplyID and G.EmpID=GS.EmpID and GS.Seq=" & Seq & " And G.Online='1'")
                strSQL.AppendLine(" Where G.CompID = " & Bsp.Utility.Quote(CompID))
                strSQL.AppendLine(" and GS.ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
                If DeptEX = "N" Then
                    strSQL.AppendLine(" And G.UpOrganID =" & Bsp.Utility.Quote(ApplyID))
                Else
                    strSQL.AppendLine(" And G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
                End If
                strSQL.AppendLine(" and G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
                strSQL.AppendLine(" and GS.GradeOrder > " & intDeptCount(6) + intDeptCount(0) + +intDeptCount(4) & ";")
                '7-甲下
                strSQL.AppendLine(" Update GS Set Grade=EncryptByKey(Key_GUID('eHRMSDBDES'),'7')")
                strSQL.AppendLine(" ,LastChgComp = " & Bsp.Utility.Quote(UserProfile.CompID))
                strSQL.AppendLine(" ,LastChgID = " & Bsp.Utility.Quote(UserProfile.UserID))
                strSQL.AppendLine(" ,LastChgDate = getdate()")
                strSQL.AppendLine(" From  GradeBase G left join GradeSignLog GS on G.CompID=GS.CompID and G.GradeDeptID=GS.ApplyID and G.EmpID=GS.EmpID and GS.Seq=" & Seq & " And G.Online='1'")
                strSQL.AppendLine(" Where G.CompID = " & Bsp.Utility.Quote(CompID))
                strSQL.AppendLine(" and GS.ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
                If DeptEX = "N" Then
                    strSQL.AppendLine(" And G.UpOrganID =" & Bsp.Utility.Quote(ApplyID))
                Else
                    strSQL.AppendLine(" And G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
                End If
                strSQL.AppendLine(" and G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
                strSQL.AppendLine(" and GS.GradeOrder > " & intDeptCount(6) + intDeptCount(0) + intDeptCount(4) + intDeptCount(1) & ";")
                '3-乙
                strSQL.AppendLine(" Update GS Set Grade=EncryptByKey(Key_GUID('eHRMSDBDES'),'3')")
                strSQL.AppendLine(" ,LastChgComp = " & Bsp.Utility.Quote(UserProfile.CompID))
                strSQL.AppendLine(" ,LastChgID = " & Bsp.Utility.Quote(UserProfile.UserID))
                strSQL.AppendLine(" ,LastChgDate = getdate()")
                strSQL.AppendLine(" From  GradeBase G left join GradeSignLog GS on G.CompID=GS.CompID and G.GradeDeptID=GS.ApplyID and G.EmpID=GS.EmpID and GS.Seq=" & Seq & " And G.Online='1'")
                strSQL.AppendLine(" Where G.CompID = " & Bsp.Utility.Quote(CompID))
                strSQL.AppendLine(" and GS.ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
                If DeptEX = "N" Then
                    strSQL.AppendLine(" And G.UpOrganID =" & Bsp.Utility.Quote(ApplyID))
                Else
                    strSQL.AppendLine(" And G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
                End If
                strSQL.AppendLine(" and G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
                strSQL.AppendLine(" and GS.GradeOrder > " & intDeptCount(6) + intDeptCount(0) + intDeptCount(4) + intDeptCount(1) + intDeptCount(5) & ";")
                '4-丙
                strSQL.AppendLine(" Update GS Set Grade=EncryptByKey(Key_GUID('eHRMSDBDES'),'4')")
                strSQL.AppendLine(" ,LastChgComp = " & Bsp.Utility.Quote(UserProfile.CompID))
                strSQL.AppendLine(" ,LastChgID = " & Bsp.Utility.Quote(UserProfile.UserID))
                strSQL.AppendLine(" ,LastChgDate = getdate()")
                strSQL.AppendLine(" From  GradeBase G left join GradeSignLog GS on G.CompID=GS.CompID and G.GradeDeptID=GS.ApplyID and G.EmpID=GS.EmpID and GS.Seq=" & Seq & " And G.Online='1'")
                strSQL.AppendLine(" Where G.CompID = " & Bsp.Utility.Quote(CompID))
                strSQL.AppendLine(" and GS.ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
                If DeptEX = "N" Then
                    strSQL.AppendLine(" And G.UpOrganID =" & Bsp.Utility.Quote(ApplyID))
                Else
                    strSQL.AppendLine(" And G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
                End If
                strSQL.AppendLine(" and G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
                strSQL.AppendLine(" and GS.GradeOrder > " & intDeptCount(6) + intDeptCount(0) + intDeptCount(4) + intDeptCount(1) + intDeptCount(5) + intDeptCount(2) & ";")

                strSQL.AppendLine(" Update G Set G.Grade=GS.Grade,G.GradeOrder=GS.GradeOrder ")
                strSQL.AppendLine(" From GradeBase G  left join GradeSignLog GS on GS.CompID=G.CompID and GS.ApplyID=G.GradeDeptID and GS.GradeSeq=G.GradeSeq and GS.EmpID=G.EmpID and G.Online='1' ")
                strSQL.AppendLine(" Where G.CompID = " & Bsp.Utility.Quote(CompID))
                strSQL.AppendLine(" And G.GradeYear = " & Bsp.Utility.Quote(GradeYear))
                strSQL.AppendLine(" And G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
                strSQL.AppendLine(" And GS.ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
                If DeptEX = "N" Then
                    strSQL.AppendLine(" And G.UpOrganID =" & Bsp.Utility.Quote(ApplyID))
                Else
                    strSQL.AppendLine(" And G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
                End If
                strSQL.AppendLine(" And G.GradeDeptID = G.OrderDeptID; ")

                strSQL.AppendLine(" Update G Set G.GradeAdjust=GS.Grade")
                strSQL.AppendLine(" ,LastChgComp = " & Bsp.Utility.Quote(UserProfile.CompID))
                strSQL.AppendLine(" ,LastChgID = " & Bsp.Utility.Quote(UserProfile.UserID))
                strSQL.AppendLine(" ,LastChgDate = getdate()")
                strSQL.AppendLine(" From GradeBase G left join GradeSignLog GS on G.CompID=GS.CompID and G.GradeDeptID=GS.ApplyID and G.EmpID=GS.EmpID and G.GradeSeq=GS.GradeSeq and G.Online='1' and G.GradeYear=" & Bsp.Utility.Quote(GradeYear))
                strSQL.AppendLine(" Where G.CompID = " & Bsp.Utility.Quote(CompID))
                strSQL.AppendLine(" and GS.ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
                If DeptEX = "N" Then
                    strSQL.AppendLine(" And G.UpOrganID =" & Bsp.Utility.Quote(ApplyID))
                Else
                    strSQL.AppendLine(" And G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
                End If
                strSQL.AppendLine(" and GS.Seq = " & Bsp.Utility.Quote(Seq))
                strSQL.AppendLine(" and GS.GradeSeq = " & Bsp.Utility.Quote(GradeSeq) & ";")

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
#Region "GS1200 年度考核(區處主管排序)-送出"
    Public Function GS1200SendData(ByVal CompID As String, ByVal ApplyID As String, ByVal ApplyTime As String, ByVal Seq As String, ByVal GradeYear As String, ByVal GradeSeq As String _
                                           , ByVal EvaluationSeq As String, ByVal MainFlag As String, ByVal DeptEx As String, ByVal topDataCount As String, ByVal LastDataCount As String, ByVal IsSignNext As String) As Boolean
        Dim strSQL As New StringBuilder()
        Dim objHR As New HR()
        Dim strWhere As String = ""
        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                strSQL.AppendLine(ConfigurationManager.AppSettings("eHRMSDBDES").ToString)
                '刪除暫存的考核補充說明    20160718 wei del 取消刪除
                'strSQL.AppendLine(" Delete From GradeEmpComment ")
                'strSQL.AppendLine(" Where GradeYear = " & Bsp.Utility.Quote(GradeYear))
                'strSQL.AppendLine(" And ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
                'If DeptEx = "N" Then
                '    strSQL.AppendLine(" And ApplyID in (Select GradeDeptID From GradeBase Where CompID= " & Bsp.Utility.Quote(CompID) & " And GradeYear=" & Bsp.Utility.Quote(GradeYear) & " And GradeSeq=" & Bsp.Utility.Quote(GradeSeq) & " And UpOrganID=" & Bsp.Utility.Quote(ApplyID) & ")")
                'Else
                '    strSQL.AppendLine(" And ApplyID = " & Bsp.Utility.Quote(ApplyID))
                'End If
                'strSQL.AppendLine(" And CompID = " & Bsp.Utility.Quote(CompID))
                'strSQL.AppendLine(" And Seq = " & Bsp.Utility.Quote(Seq))
                'strSQL.AppendLine(" And GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
                'strSQL.AppendLine(" And EmpID not In (")
                'strSQL.AppendLine("Select EmpID From ( ")
                'strSQL.AppendLine(" Select G.CompID,G.EmpID,P.NameN,GS.ApplyID ")
                'strSQL.AppendLine(" ,GS.GradeOrder ")
                'strSQL.AppendLine(" ,isnull(GE.Comment,'') as Comment ")
                'strSQL.AppendLine(" ,Case When isnull(GE.Comment,'N')='N' Then 'N' Else 'Y' End as IsExit ")
                'If MainFlag = "2" Then
                '    strSQL.AppendLine(" ,ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) as Grade1 ")
                '    strSQL.AppendLine(" ,ltrim(isnull(CONVERT(char(1), DecryptByKey(GS1.Grade)) ,'')) as Grade2 ")
                'Else
                '    strSQL.AppendLine(" ,isnull((Select top 1 FinalScoreValue From EvaluationSignLog Where CompID=G.CompID And EvaluationYear = " & Bsp.Utility.Quote(GradeYear) & " And EvaluationSeq = " & Bsp.Utility.Quote(EvaluationSeq) & " And ApplyID = G.EmpID And Result='1' And StepID='3' And IdentityID='4' order by Seq desc),'') As Grade1")
                '    strSQL.AppendLine(" ,ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) as Grade2 ")
                'End If
                'strSQL.AppendLine(" From  GradeBase G left join GradeSignLog GS on G.CompID=GS.CompID and G.GradeDeptID=GS.ApplyID and G.EmpID=GS.EmpID and GS.Seq=" & Seq & " And G.Online='1'")
                'strSQL.AppendLine(" Left Join Personal P on G.CompID = P.CompID and G.EmpID = P.EmpID ")
                'If MainFlag = "2" Then
                '    strSQL.AppendLine(" Left join GradeSignLog GS1 on G.CompID=GS1.CompID and G.GradeDeptID=GS1.ApplyID and G.EmpID=GS1.EmpID and GS1.Seq=" & Seq - 1)
                'End If
                'strSQL.AppendLine(" Left join GradeEmpComment GE on GS.CompID=GE.CompID and GS.ApplyTime=GE.ApplyTime and GS.ApplyID=GE.ApplyID and GS.EmpID=GE.EmpID and GS.Seq=GE.Seq and GS.GradeSeq=GE.GradeSeq ")
                'strSQL.AppendLine(" Where G.CompID = " & Bsp.Utility.Quote(CompID))
                'strSQL.AppendLine(" And G.GradeYear = " & Bsp.Utility.Quote(GradeYear))
                'strSQL.AppendLine(" And G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
                'strSQL.AppendLine(" And GS.ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
                'strSQL.AppendLine(" And GS.Seq = " & Bsp.Utility.Quote(Seq))
                'If MainFlag = "2" And DeptEx = "N" Then
                '    strSQL.AppendLine(" And G.UpOrganID =" & Bsp.Utility.Quote(ApplyID))
                'Else
                '    strSQL.AppendLine(" And G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
                'End If
                'strSQL.AppendLine(" And G.GradeDeptID = G.OrderDeptID ")
                'strSQL.AppendLine(" and (ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) in ('9','1','7','3','4') ")
                'strSQL.AppendLine(" Or (ltrim(isnull(CONVERT(char(1), DecryptByKey(GS1.Grade)) ,'')))<>'' and ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,''))<> ltrim(isnull(CONVERT(char(1), DecryptByKey(GS1.Grade)) ,''))")

                ' ''前20%
                ''strSQL.AppendLine(" Select Top " & topDataCount & " G.CompID,G.EmpID,P.NameN,GS.ApplyID ")
                ''strSQL.AppendLine(" ,GS.GradeOrder ")
                ''strSQL.AppendLine(" ,isnull(GE.Comment,'') as Comment ")
                ''strSQL.AppendLine(" From  GradeBase G left join GradeSignLog GS on G.CompID=GS.CompID and G.GradeDeptID=GS.ApplyID and G.EmpID=GS.EmpID and GS.Seq=" & Seq & " And G.Online='1'")
                ''strSQL.AppendLine(" Left Join Personal P on G.CompID = P.CompID and G.EmpID = P.EmpID ")
                ''strSQL.AppendLine(" Left join GradeEmpComment GE on GS.CompID=GE.CompID and GS.ApplyTime=GE.ApplyTime and GS.ApplyID=GE.ApplyID and GS.EmpID=GE.EmpID and GS.Seq=GE.Seq and GS.GradeSeq=GE.GradeSeq ")
                ''strSQL.AppendLine(" Where G.CompID = " & Bsp.Utility.Quote(CompID))
                ''strSQL.AppendLine(" And G.GradeYear = " & Bsp.Utility.Quote(GradeYear))
                ''strSQL.AppendLine(" And G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
                ''strSQL.AppendLine(" And GS.ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
                ''strSQL.AppendLine(" And GS.Seq = " & Bsp.Utility.Quote(Seq))
                ''If DeptEx = "N" Then
                ''    strSQL.AppendLine(" And G.UpOrganID =" & Bsp.Utility.Quote(ApplyID))
                ''Else
                ''    strSQL.AppendLine(" And G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
                ''End If
                ''strSQL.AppendLine(" And G.GradeDeptID = G.OrderDeptID ")
                ''strSQL.AppendLine(" order By GS.GradeOrder ")
                ''strSQL.AppendLine(" Union")
                ' ''後15%
                ''strSQL.AppendLine(" Select Top " & LastDataCount & " G.CompID,G.EmpID,P.NameN,GS.ApplyID ")
                ''strSQL.AppendLine(" ,GS.GradeOrder ")
                ''strSQL.AppendLine(" ,isnull(GE.Comment,'') as Comment ")
                ''strSQL.AppendLine(" From  GradeBase G left join GradeSignLog GS on G.CompID=GS.CompID and G.GradeDeptID=GS.ApplyID and G.EmpID=GS.EmpID and GS.Seq=" & Seq & " And G.Online='1'")
                ''strSQL.AppendLine(" Left Join Personal P on G.CompID = P.CompID and G.EmpID = P.EmpID ")
                ''strSQL.AppendLine(" Left join GradeEmpComment GE on GS.CompID=GE.CompID and GS.ApplyTime=GE.ApplyTime and GS.ApplyID=GE.ApplyID and GS.EmpID=GE.EmpID and GS.Seq=GE.Seq and GS.GradeSeq=GE.GradeSeq ")
                ''strSQL.AppendLine(" Where G.CompID = " & Bsp.Utility.Quote(CompID))
                ''strSQL.AppendLine(" And G.GradeYear = " & Bsp.Utility.Quote(GradeYear))
                ''strSQL.AppendLine(" And G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
                ''strSQL.AppendLine(" And GS.ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
                ''strSQL.AppendLine(" And GS.Seq = " & Bsp.Utility.Quote(Seq))
                ''If DeptEx = "N" Then
                ''    strSQL.AppendLine(" And G.UpOrganID =" & Bsp.Utility.Quote(ApplyID))
                ''Else
                ''    strSQL.AppendLine(" And G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
                ''End If
                ''strSQL.AppendLine(" And G.GradeDeptID = G.OrderDeptID ")
                ''strSQL.AppendLine(" order By GS.GradeOrder Desc")
                ''strSQL.AppendLine(" Union")
                ' ''配置考績為優/乙及配置考績與建議考績不一致
                ''strSQL.AppendLine(" Select G.CompID,G.EmpID,P.NameN,GS.ApplyID ")
                ''strSQL.AppendLine(" ,GS.GradeOrder ")
                ''strSQL.AppendLine(" ,isnull(GE.Comment,'') as Comment ")
                ''strSQL.AppendLine(" From  GradeBase G left join GradeSignLog GS on G.CompID=GS.CompID and G.GradeDeptID=GS.ApplyID and G.EmpID=GS.EmpID and GS.Seq=" & Seq & " And G.Online='1'")
                ''strSQL.AppendLine(" Left Join Personal P on G.CompID = P.CompID and G.EmpID = P.EmpID ")
                ''strSQL.AppendLine(" Left join GradeEmpComment GE on GS.CompID=GE.CompID and GS.ApplyTime=GE.ApplyTime and GS.ApplyID=GE.ApplyID and GS.EmpID=GE.EmpID and GS.Seq=GE.Seq and GS.GradeSeq=GE.GradeSeq ")
                ''strSQL.AppendLine(" Where G.CompID = " & Bsp.Utility.Quote(CompID))
                ''strSQL.AppendLine(" And G.GradeYear = " & Bsp.Utility.Quote(GradeYear))
                ''strSQL.AppendLine(" And G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
                ''strSQL.AppendLine(" And GS.ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
                ''strSQL.AppendLine(" And GS.Seq = " & Bsp.Utility.Quote(Seq))
                ''If DeptEx = "N" Then
                ''    strSQL.AppendLine(" And G.UpOrganID =" & Bsp.Utility.Quote(ApplyID))
                ''Else
                ''    strSQL.AppendLine(" And G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
                ''End If
                ''strSQL.AppendLine(" And G.GradeDeptID = G.OrderDeptID ")
                ''strSQL.AppendLine(" And (isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'')='9' or isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'') = '1' or  isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'') = '3' or  isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'') = '4' or isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'') <> isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'') ) ")
                'strSQL.AppendLine(" ) S ")

                'strSQL.AppendLine(" );")
                '刪除暫存的整體評量說明
                'strSQL.AppendLine("Delete From GradeEmpComment_ScoreAdjust ")
                'strSQL.AppendLine(" Where GradeYear = " & Bsp.Utility.Quote(GradeYear))
                'strSQL.AppendLine(" And ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
                'If DeptEx = "N" Then
                '    strSQL.AppendLine(" And ApplyID in (Select GradeDeptID From GradeBase Where CompID= " & Bsp.Utility.Quote(CompID) & " And GradeYear=" & Bsp.Utility.Quote(GradeYear) & " And GradeSeq=" & Bsp.Utility.Quote(GradeSeq) & " And UpOrganID=" & Bsp.Utility.Quote(ApplyID) & ")")
                'Else
                '    strSQL.AppendLine(" And ApplyID = " & Bsp.Utility.Quote(ApplyID))
                'End If
                'strSQL.AppendLine(" And CompID = " & Bsp.Utility.Quote(CompID))
                'strSQL.AppendLine(" And Seq = " & Bsp.Utility.Quote(Seq))
                'strSQL.AppendLine(" And GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
                'strSQL.AppendLine(" And EmpID not In (")
                'strSQL.AppendLine(" Select G.EmpID")
                'strSQL.AppendLine(" From  GradeBase G left join GradeSignLog GS on G.CompID=GS.CompID and G.GradeDeptID=GS.ApplyID and G.EmpID=GS.EmpID and GS.Seq=" & Seq & " And G.Online='1'")
                'strSQL.AppendLine(" Left Join Personal P on G.CompID = P.CompID and G.EmpID = P.EmpID ")
                'strSQL.AppendLine(" Left join GradeEmpComment_ScoreAdjust GE on GS.CompID=GE.CompID and GS.ApplyTime=GE.ApplyTime and GS.ApplyID=GE.ApplyID and GS.EmpID=GE.EmpID and GS.Seq=GE.Seq and GS.GradeSeq=GE.GradeSeq ")
                'strSQL.AppendLine(" Where G.CompID = " & Bsp.Utility.Quote(CompID))
                'strSQL.AppendLine(" And G.GradeYear = " & Bsp.Utility.Quote(GradeYear))
                'strSQL.AppendLine(" And G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
                'strSQL.AppendLine(" And GS.ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
                'strSQL.AppendLine(" And GS.Seq = " & Bsp.Utility.Quote(Seq))
                'If DeptEx = "N" Then
                '    strSQL.AppendLine(" And G.UpOrganID =" & Bsp.Utility.Quote(ApplyID))
                'Else
                '    strSQL.AppendLine(" And G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
                'End If
                'strSQL.AppendLine(" And G.GradeDeptID = G.OrderDeptID ")
                'strSQL.AppendLine(" And (GS.Score<>'' and GS.Score<> isnull((Select top 1 FinalScore From EvaluationSignLog Where CompID=G.CompID And EvaluationYear = " & Bsp.Utility.Quote(GradeYear) & " And EvaluationSeq = " & Bsp.Utility.Quote(EvaluationSeq) & " And ApplyID = G.EmpID And Result='1' And StepID='3' And IdentityID='4' order by Seq desc),'')")
                'strSQL.AppendLine(" And isnull((Select top 1 FinalScore From EvaluationSignLog Where CompID=G.CompID And EvaluationYear = " & Bsp.Utility.Quote(GradeYear) & " And EvaluationSeq = " & Bsp.Utility.Quote(EvaluationSeq) & " And ApplyID = G.EmpID And Result='1' And StepID='3' And IdentityID='4' order by Seq desc),'')<>'')")
                'strSQL.AppendLine(" );")

                'Update SingLog
                strSQL.AppendLine("Update SignLog Set SignTime=Getdate() ")
                strSQL.AppendLine(" ,Result='1' ")
                strSQL.AppendLine(" where CompID = " & Bsp.Utility.Quote(CompID))
                strSQL.AppendLine(" and ApplyTime= " & Bsp.Utility.Quote(ApplyTime))
                If DeptEx = "N" Then
                    strSQL.AppendLine(" And ApplyID in (Select GradeDeptID From GradeBase Where CompID= " & Bsp.Utility.Quote(CompID) & " And GradeYear=" & Bsp.Utility.Quote(GradeYear) & " And GradeSeq=" & Bsp.Utility.Quote(GradeSeq) & " And UpOrganID=" & Bsp.Utility.Quote(ApplyID) & ")")
                Else
                    strSQL.AppendLine(" And ApplyID = " & Bsp.Utility.Quote(ApplyID))
                End If
                strSQL.AppendLine(" and Seq = " & Bsp.Utility.Quote(Seq) & ";")

                'Update Application
                strSQL.AppendLine(" Update Application Set Status = '2' ")
                strSQL.AppendLine(" Where CompID = " & Bsp.Utility.Quote(CompID))
                strSQL.AppendLine(" And ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
                If DeptEx = "N" Then
                    strSQL.AppendLine(" And (ApplyID in (Select GradeDeptID From GradeBase Where CompID= " & Bsp.Utility.Quote(CompID) & " And GradeYear=" & Bsp.Utility.Quote(GradeYear) & " And GradeSeq=" & Bsp.Utility.Quote(GradeSeq) & " And UpOrganID=" & Bsp.Utility.Quote(ApplyID) & ")")
                    strSQL.AppendLine(" or ApplyID =" & Bsp.Utility.Quote(ApplyID) & ");")
                Else
                    strSQL.AppendLine(" And ApplyID = " & Bsp.Utility.Quote(ApplyID) & ";")
                End If

                'Update GradeBase.Grade,GradeOrder
                strSQL.AppendLine(" Update G Set G.Grade=GS.Grade,G.GradeOrder=GS.GradeOrder,G.GradeOrderHR=GS.GradeOrder,G.GradeHR=G.GradeAdjust,G.GradeOrder2=GS.GradeOrder,G.Grade2=G.GradeAdjust,G.ScoreValue=GS.ScoreValue,G.Score=GS.Score ")
                strSQL.AppendLine(" From GradeBase G  left join GradeSignLog GS on GS.CompID=G.CompID and GS.ApplyID=G.GradeDeptID and GS.GradeSeq=G.GradeSeq and GS.EmpID=G.EmpID and G.Online='1' ")
                strSQL.AppendLine(" Where G.CompID = " & Bsp.Utility.Quote(CompID))
                strSQL.AppendLine(" And G.GradeYear = " & Bsp.Utility.Quote(GradeYear))
                strSQL.AppendLine(" And G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
                strSQL.AppendLine(" And GS.ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
                strSQL.AppendLine(" and GS.Seq = " & Bsp.Utility.Quote(Seq))
                If DeptEx = "N" Then
                    strSQL.AppendLine(" And G.UpOrganID =" & Bsp.Utility.Quote(ApplyID))
                Else
                    strSQL.AppendLine(" And G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
                End If
                strSQL.AppendLine(" And G.GradeDeptID = G.OrderDeptID; ")


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
#Region "GS1200 年度考核(區處主管排序)-下載"
    Public Function GS1200Download(ByVal CompID As String, ByVal ApplyID As String, ByVal ApplyTime As String, ByVal Seq As String, ByVal Status As String, ByVal MainFlag As String _
                           , ByVal GradeYear As String, ByVal GradeSeq As String, ByVal EvaluationSeq As String, ByVal IsPerformance As String, ByVal IsSignNext As String, ByVal DeptEX As String, Optional GroupID As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        '單位、員編、姓名、到職日、職位、職等、年度整體評量結果、【單位主管】初排序、配置考績(=2015年考績檢核用的值)、【區處主管】排序、配置考績、考績檢視建議，【單位主管】整體評量調整說明、考核補充說明、【區處主管】整體評量調整說明、考績檢視補充說明、業績資料
        '(標題拆兩列，單位主管 跟 區處主管 標題，要用跨欄置中顯示)
        '單位、員編、姓名、到職日、職位、職等、【單位主管】考績、初排序、【區處主管】考績、排序，【單位主管】考核補充說明、【區處主管】考核補充說明、業績資料 20160627 wei add
        strSQL.AppendLine(ConfigurationManager.AppSettings("eHRMSDBDES").ToString)
        strSQL.AppendLine(" Select O.OrganName as '單位', P.EmpID as '員編',P.NameN as '姓名',Convert(char(19), P.EmpDate, 111) as '到職日' ,Po.Remark as '職位' ,P.RankID as '職等'")
        'strSQL.AppendLine(" ,Case When GS.ScoreValue<>0 then Cast(GS.ScoreValue as Varchar(2)) + '-' + GS.Score Else ' ' End as '年度整體評量結果'")   20160627 wei del
        '單位主管
        strSQL.AppendLine(" ,Case When isnull(CONVERT(char(1), DecryptByKey(G.GradeDept)) ,'')='1' Then '優' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeDept)) ,'')='2' Then '甲' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeDept)) ,'')='3' Then '乙' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeDept)) ,'')='4' Then '丙' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeDept)) ,'')='6' Then '甲上' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeDept)) ,'')='7' Then '甲下' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeDept)) ,'')='9' Then '特優' ")
        strSQL.AppendLine(" Else '' End as '考績' ")
        strSQL.AppendLine(" ,G.GradeOrderDept as '初排序'")
        '區處主管
        strSQL.AppendLine(" ,Case When isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')='1' Then '優' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')='2' Then '甲' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')='3' Then '乙' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')='4' Then '丙' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')='6' Then '甲上' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')='7' Then '甲下' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')='9' Then '特優' ")
        strSQL.AppendLine(" Else '' End as '考績' ")
        strSQL.AppendLine(" ,Case when isnull(GS.GradeOrder,0)=0 then '' else Cast(GS.GradeOrder as Varchar(3)) end as 排序")

        'strSQL.AppendLine(" ,Case When isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'')='1' Then '優' ")
        'strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'')='2' Then '甲' ")
        'strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'')='3' Then '乙' ")
        'strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'')='4' Then '丙' ")
        'strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'')='6' Then '甲上' ")
        'strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'')='7' Then '甲下' ")
        'strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'')='9' Then '特優' ")
        'strSQL.AppendLine(" Else '' End as '考績檢視建議' ")
        '單位主管
        'strSQL.AppendLine(" ,isnull(GEA1.Comment,'') as '整體評量調整說明',isnull(GE1.Comment,'') as '考核補充說明' ")
        strSQL.AppendLine(" ,isnull(GE1.Comment,'') as '單位考績補充說明' ")
        '區處主管
        'strSQL.AppendLine(" ,isnull(GEA.Comment,'') as '整體評量調整說明',isnull(GE.Comment,'') as '考績檢視補充說明' ")
        strSQL.AppendLine(" ,isnull(GE.Comment,'') as '區處考績補充說明' ")
        If IsPerformance = "Y" Then
            strSQL.AppendLine(" ,isnull(EP.Fld1,'') as 'Q1\個金業務\房貸撥款量(仟元)',isnull(EP.Fld2,'') as 'Q1\個金業務\信貸撥款量(仟元)',isnull(EP.Fld3,'') as 'Q1\個金業務\總AP(仟點)',isnull(EP.Fld4,'') as 'Q1\個金業務\理財Score Card',isnull(EP.Fld5,'') as 'Q1\理財業務\總AP(仟點)',isnull(EP.Fld6,'') as 'Q1\理財業務\Score Card',isnull(EP.Fld7,'') as 'Q1\法金業務(單位)\總AP(仟點)',isnull(EP.Fld8,'') as 'Q1\法金業務(單位)\單位職系達成率' ")
            strSQL.AppendLine(" ,isnull(EP.Fld9,'') as 'Q2\個金業務\房貸撥款量(仟元)',isnull(EP.Fld10,'') as 'Q2\個金業務\信貸撥款量(仟元)',isnull(EP.Fld11,'') as 'Q2\個金業務\總AP(仟點)',isnull(EP.Fld12,'') as 'Q2\個金業務\理財Score Card',isnull(EP.Fld13,'') as 'Q2\理財業務\總AP(仟點)',isnull(EP.Fld14,'') as 'Q2\理財業務\Score Card',isnull(EP.Fld15,'') as 'Q2\法金業務(單位)\總AP(仟點)',isnull(EP.Fld16,'') as 'Q2\法金業務(單位)\單位職系達成率' ")
            strSQL.AppendLine(" ,isnull(EP.Fld17,'') as 'Q3\個金業務\房貸撥款量(仟元)',isnull(EP.Fld18,'') as 'Q3\個金業務\信貸撥款量(仟元)',isnull(EP.Fld19,'') as 'Q3\個金業務\總AP(仟點)',isnull(EP.Fld20,'') as 'Q3\個金業務\理財Score Card',isnull(EP.Fld21,'') as 'Q3\理財業務\總AP(仟點)',isnull(EP.Fld22,'') as 'Q3\理財業務\Score Card',isnull(EP.Fld23,'') as 'Q3\法金業務(單位)\總AP(仟點)',isnull(EP.Fld24,'') as 'Q3\法金業務(單位)\單位職系達成率' ")
            strSQL.AppendLine(" ,isnull(EP.Fld25,'') as '" & GradeYear & "年度平均\個金業務\房貸撥款量(仟元)',isnull(EP.Fld26,'') as '" & GradeYear & "年度平均\個金業務\信貸撥款量(仟元)',isnull(EP.Fld27,'') as '" & GradeYear & "年度平均\個金業務\總AP(仟點)',isnull(EP.Fld28,'') as '" & GradeYear & "年度平均\個金業務\理財Score Card',isnull(EP.Fld29,'') as '" & GradeYear & "年度平均\理財業務\總AP(仟點)',isnull(EP.Fld30,'') as '" & GradeYear & "年度平均\理財業務\Score Card',isnull(EP.Fld31,'') as '" & GradeYear & "年度平均\法金業務(單位)\總AP(仟點)',isnull(EP.Fld32,'') as '" & GradeYear & "年度平均\法金業務(單位)\單位職系達成率' ")
        End If
        strSQL.AppendLine(" from Personal P inner join GradeBase G on G.GradeYear = " & Bsp.Utility.Quote(GradeYear) & " and P.CompID =G.CompID and P.EmpID = G.EmpID and G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq) & " And G.Online='1'")
        strSQL.AppendLine(" left join Organization O on O.CompID = G.CompID and O.OrganID = G.OrderDeptID")
        strSQL.AppendLine(" left join GradeSignLog GS on GS.CompID= " & Bsp.Utility.Quote(CompID) & " and GS.ApplyTime= " & Bsp.Utility.Quote(ApplyTime) & " and GS.ApplyID = G.GradeDeptID and GS.Seq= " & Bsp.Utility.Quote(Seq) & " and G.CompID =GS.CompID and G.EmpID = GS.EmpID and G.GradeSeq = GS.GradeSeq")
        strSQL.AppendLine(" left join GradeSignLog GS1 on GS.CompID= " & Bsp.Utility.Quote(CompID) & " and GS.ApplyTime= " & Bsp.Utility.Quote(ApplyTime) & " and GS.ApplyID= G.GradeDeptID and GS.Seq= " & Bsp.Utility.Quote(Seq - 1) & " and G.CompID =GS.CompID and G.EmpID = GS.EmpID and G.GradeSeq = GS.GradeSeq")
        strSQL.AppendLine(" left join GradeEmpComment GE on GE.CompID= " & Bsp.Utility.Quote(CompID) & " and GE.ApplyTime= " & Bsp.Utility.Quote(ApplyTime) & " and GE.ApplyID= G.GradeDeptID and GE.Seq= " & Bsp.Utility.Quote(Seq) & " and P.CompID =GE.CompID and P.EmpID = GE.EmpID and G.GradeSeq = GE.GradeSeq")
        strSQL.AppendLine(" left join GradeEmpComment_ScoreAdjust GEA on GEA.CompID= " & Bsp.Utility.Quote(CompID) & " and GEA.ApplyTime= " & Bsp.Utility.Quote(ApplyTime) & " and GEA.ApplyID= G.GradeDeptID and GEA.Seq= " & Bsp.Utility.Quote(Seq) & " and P.CompID =GEA.CompID and P.EmpID = GEA.EmpID and G.GradeSeq = GEA.GradeSeq")
        strSQL.AppendLine(" left join GradeEmpComment GE1 on GE1.CompID= " & Bsp.Utility.Quote(CompID) & " and GE1.ApplyTime= " & Bsp.Utility.Quote(ApplyTime) & " and GE1.ApplyID= G.GradeDeptID and GE1.Seq= " & Bsp.Utility.Quote(Seq - 1) & " and P.CompID =GE1.CompID and P.EmpID = GE1.EmpID and G.GradeSeq = GE1.GradeSeq")
        strSQL.AppendLine(" left join GradeEmpComment_ScoreAdjust GEA1 on GEA1.CompID= " & Bsp.Utility.Quote(CompID) & " and GEA1.ApplyTime= " & Bsp.Utility.Quote(ApplyTime) & " and GEA1.ApplyID= G.GradeDeptID and GEA1.Seq= " & Bsp.Utility.Quote(Seq - 1) & " and P.CompID =GEA1.CompID and P.EmpID = GEA1.EmpID and G.GradeSeq = GEA1.GradeSeq")
        strSQL.AppendLine(" left join Title T on P.CompID = T.CompID and P.RankID = T.RankID and P.TitleID = T.TitleID")
        strSQL.AppendLine(" left join EmpPosition EP1 on P.CompID = EP1.CompID and P.EmpID = EP1.EmpID and EP1.PrincipalFlag = '1'")
        strSQL.AppendLine(" left join Position Po on EP1.CompID = Po.CompID and EP1.PositionID = Po.PositionID ")
        strSQL.AppendLine(" left join EmpWorkType EW on P.CompID = EW.CompID and P.EmpID = EW.EmpID and EW.PrincipalFlag = '1'")
        strSQL.AppendLine(" left join WorkType W on EW.CompID = W.CompID and EW.WorkTypeID = W.WorkTypeID")
        strSQL.AppendLine(" left join EmpPerformance EP on P.CompID = EP.CompID and P.EmpID = EP.EmpID and EP.EPYear = " & Bsp.Utility.Quote(GradeYear))
        strSQL.AppendLine(" where G.CompID = " & Bsp.Utility.Quote(CompID))
        If DeptEX = "Y" Then
            strSQL.AppendLine(" and G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
        Else
            strSQL.AppendLine(" And G.UpOrganID =" & Bsp.Utility.Quote(ApplyID))
        End If
        strSQL.AppendLine(" and G.GradeDeptID = G.OrderDeptID and P.EmpID <> " & Bsp.Utility.Quote(UserProfile.UserID))
        If GroupID <> "" And GroupID <> "0" Then
            strSQL.AppendLine(" And G.GroupID=" & Bsp.Utility.Quote(GroupID))
        End If
        strSQL.Append("Order by Case when ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) = '9' then '0' when ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) = '6' then '1.9' when ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) = '7' then '2.1' when ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) = '' then '99' else ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) End")
        strSQL.AppendLine(",Case  when isnull(GS.GradeOrder,0) = 0 then 0 else Cast(isnull(GS.GradeOrder,0) as int) end ,Cast(isnull(GS.GradeOrder,0) as int),Cast(isnull(G.GradeOrderDept,0) as int) ,Po.PositionID ,P.RankIDMap desc ,P.EmpID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region
#Region "GS1200 年度考核(區處主管排序)-專用格式下載"
    Public Function GS1200DownloadSample(ByVal CompID As String, ByVal ApplyID As String, ByVal ApplyTime As String, ByVal Seq As String, ByVal Status As String, ByVal MainFlag As String _
                           , ByVal GradeYear As String, ByVal GradeSeq As String, ByVal IsSignNext As String, ByVal DeptEX As String, Optional GroupID As String = "") As DataTable
        Dim strSQL As New StringBuilder()
        '項次,員編,姓名,單位,到職日,職位,職等,年度整體評量結果,年度排序
        strSQL.AppendLine(ConfigurationManager.AppSettings("eHRMSDBDES").ToString)
        strSQL.AppendLine(" Select P.EmpID as '員編',P.NameN as '姓名',O.OrganName as '單位',Convert(char(19), P.EmpDate, 111) as '到職日' ,Po.Remark as 職位 ,P.RankID as '職等' ") 'Row_Number() Over(Order By P.EmpID) As '項次',20160713 wei modify
        'strSQL.AppendLine(" ,Case When GS.ScoreValue<>0 then Cast(GS.ScoreValue as Varchar(2)) + '-' + GS.Score Else ' ' End as '年度整體評量結果'")
        '單位主管   20160722 wei add
        strSQL.AppendLine(" ,Case When isnull(CONVERT(char(1), DecryptByKey(G.GradeDept)) ,'')='1' Then '優' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeDept)) ,'')='2' Then '甲' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeDept)) ,'')='3' Then '乙' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeDept)) ,'')='4' Then '丙' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeDept)) ,'')='6' Then '甲上' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeDept)) ,'')='7' Then '甲下' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeDept)) ,'')='9' Then '特優' ")
        strSQL.AppendLine(" Else '' End as '單位主管初核考績' ")
        strSQL.AppendLine(" ,G.GradeOrderDept as '單位主管初核排序'")
        strSQL.AppendLine(" ,Case When isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')='1' Then '優' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')='2' Then '甲' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')='3' Then '乙' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')='4' Then '丙' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')='6' Then '甲上' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')='7' Then '甲下' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')='9' Then '特優' ")
        strSQL.AppendLine(" Else '' End as '考績' ")

        strSQL.AppendLine(" ,Case when isnull(GS.GradeOrder,0)=0 then '' else Cast(GS.GradeOrder as Varchar(3)) end as '排序'")
        strSQL.AppendLine(" ,isnull(GE.Comment,'') as '考績補充說明'")
        'strSQL.AppendLine(" ,Case when isnull(GS.GradeOrder,0)=0 then '' else Cast(GS.GradeOrder as Varchar(3)) end as '年度排序'")
        strSQL.AppendLine(" from Personal P inner join GradeBase G on G.GradeYear = " & Bsp.Utility.Quote(GradeYear) & " and P.CompID =G.CompID and P.EmpID = G.EmpID and G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq) & " And G.Online='1'")
        strSQL.AppendLine(" left join Organization O on O.CompID = G.CompID and O.OrganID = G.OrderDeptID")
        strSQL.AppendLine(" left join GradeSignLog GS on GS.CompID= " & Bsp.Utility.Quote(CompID) & " and GS.ApplyTime= " & Bsp.Utility.Quote(ApplyTime) & " and GS.ApplyID= G.GradeDeptID and GS.Seq= " & Bsp.Utility.Quote(Seq) & " and P.CompID =GS.CompID and P.EmpID = GS.EmpID and G.GradeSeq = GS.GradeSeq")
        strSQL.AppendLine(" left join GradeEmpComment GE on GE.CompID= " & Bsp.Utility.Quote(CompID) & " and GE.ApplyTime= " & Bsp.Utility.Quote(ApplyTime) & " and GE.ApplyID= G.GradeDeptID and GE.Seq= " & Bsp.Utility.Quote(Seq) & " and P.CompID =GE.CompID and P.EmpID = GE.EmpID and G.GradeSeq = GE.GradeSeq")
        strSQL.AppendLine(" left join Title T on P.CompID = T.CompID and P.RankID = T.RankID and P.TitleID = T.TitleID")
        strSQL.AppendLine(" left join EmpPosition EP1 on P.CompID = EP1.CompID and P.EmpID = EP1.EmpID and EP1.PrincipalFlag = '1'")
        strSQL.AppendLine(" left join Position Po on EP1.CompID = Po.CompID and EP1.PositionID = Po.PositionID ")
        strSQL.AppendLine(" left join EmpWorkType EW on P.CompID = EW.CompID and P.EmpID = EW.EmpID and EW.PrincipalFlag = '1'")
        strSQL.AppendLine(" left join WorkType W on EW.CompID = W.CompID and EW.WorkTypeID = W.WorkTypeID")
        strSQL.AppendLine(" left join EmpPerformance EP on P.CompID = EP.CompID and P.EmpID = EP.EmpID and EP.EPYear = " & Bsp.Utility.Quote(GradeYear))
        strSQL.AppendLine(" where G.CompID = " & Bsp.Utility.Quote(CompID))
        If DeptEX = "Y" Then
            strSQL.AppendLine(" and G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
        Else
            strSQL.AppendLine(" and G.UpOrganID = " & Bsp.Utility.Quote(ApplyID))
        End If
        strSQL.AppendLine(" and G.GradeDeptID = G.OrderDeptID and P.EmpID <> " & Bsp.Utility.Quote(UserProfile.UserID))
        If GroupID <> "" And GroupID <> "0" Then
            strSQL.AppendLine(" And G.GroupID=" & Bsp.Utility.Quote(GroupID))
        End If
        strSQL.Append("Order by Case when ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) = '9' then '0' when ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) = '6' then '1.9' when ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) = '7' then '2.1' when ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) = '' then '99' else ltrim(isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')) End")
        strSQL.AppendLine(", Case when GS.GradeOrder = 0 then 1 else 0 end ,Cast(GS.GradeOrder as int) ,Cast(G.GradeOrderDept as int),Po.PositionID ,P.RankIDMap desc ,P.EmpID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region
#End Region

#Region "GS1210 考績檢視建議說明畫面"
#Region "GS1210 考績檢視建議說明畫面-查詢"
    Public Function GS1210Query(ByVal CompID As String, ByVal ApplyID As String, ByVal ApplyTime As String, ByVal Seq As String, ByVal GradeYear As String, ByVal GradeSeq As String _
                                           , ByVal EvaluationSeq As String, ByVal MainFlag As String, ByVal DeptEx As String, ByVal topDataCount As String, ByVal LastDataCount As String) As DataTable

        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(ConfigurationManager.AppSettings("eHRMSDBDES").ToString)
        strSQL.AppendLine("Select * From ( ")
        '前20%
        strSQL.AppendLine(" Select Top " & topDataCount & " G.CompID,G.EmpID,P.NameN,GS.ApplyID ")
        strSQL.AppendLine(" ,GS.GradeOrder ")
        strSQL.AppendLine(" ,isnull(GE.Comment,'') as Comment ")
        strSQL.AppendLine(" ,Case When isnull(GE.Comment,'N')='N' Then 'N' Else 'Y' End as IsExit ")
        strSQL.AppendLine(" ,isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'') as Grade ")
        strSQL.AppendLine(" ,isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'') as GradeAdjust ")
        strSQL.AppendLine(" From  GradeBase G left join GradeSignLog GS on G.CompID=GS.CompID and G.GradeDeptID=GS.ApplyID and G.EmpID=GS.EmpID and GS.Seq=" & Seq & " And G.Online='1'")
        strSQL.AppendLine(" Left Join Personal P on G.CompID = P.CompID and G.EmpID = P.EmpID ")
        If MainFlag = "2" Then
            strSQL.AppendLine(" Left join GradeSignLog GS1 on G.CompID=GS1.CompID and G.GradeDeptID=GS1.ApplyID and G.EmpID=GS1.EmpID and GS1.Seq=" & Seq - 1 & " And GS.ApplyTime=GS1.ApplyTime and G.Online='1' ")
        End If
        strSQL.AppendLine(" Left join GradeEmpComment GE on GS.CompID=GE.CompID and GS.ApplyTime=GE.ApplyTime and GS.ApplyID=GE.ApplyID and GS.EmpID=GE.EmpID and GS.Seq=GE.Seq and GS.GradeSeq=GE.GradeSeq ")
        strSQL.AppendLine(" Where G.CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine(" And G.GradeYear = " & Bsp.Utility.Quote(GradeYear))
        strSQL.AppendLine(" And G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
        strSQL.AppendLine(" And GS.ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
        strSQL.AppendLine(" And GS.Seq = " & Bsp.Utility.Quote(Seq))
        If MainFlag = "2" And DeptEx = "N" Then
            strSQL.AppendLine(" And G.UpOrganID =" & Bsp.Utility.Quote(ApplyID))
        Else
            strSQL.AppendLine(" And G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
        End If
        strSQL.AppendLine(" And G.GradeDeptID = G.OrderDeptID ")
        strSQL.AppendLine(" order By GS.GradeOrder ")

        strSQL.AppendLine(" Union")

        '後15%
        strSQL.AppendLine(" Select Top " & LastDataCount & " G.CompID,G.EmpID,P.NameN,GS.ApplyID ")
        strSQL.AppendLine(" ,GS.GradeOrder ")
        strSQL.AppendLine(" ,isnull(GE.Comment,'') as Comment ")
        strSQL.AppendLine(" ,Case When isnull(GE.Comment,'N')='N' Then 'N' Else 'Y' End as IsExit ")
        strSQL.AppendLine(" ,isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'') as GradeA ")
        strSQL.AppendLine(" ,isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'') as GradeAdjust ")
        strSQL.AppendLine(" From  GradeBase G left join GradeSignLog GS on G.CompID=GS.CompID and G.GradeDeptID=GS.ApplyID and G.EmpID=GS.EmpID and GS.Seq=" & Seq & " And G.Online='1'")
        strSQL.AppendLine(" Left Join Personal P on G.CompID = P.CompID and G.EmpID = P.EmpID ")
        If MainFlag = "2" Then
            strSQL.AppendLine(" Left join GradeSignLog GS1 on G.CompID=GS1.CompID and G.GradeDeptID=GS1.ApplyID and G.EmpID=GS1.EmpID and GS1.Seq=" & Seq - 1 & " And GS.ApplyTime=GS1.ApplyTime and G.Online='1' ")
        End If
        strSQL.AppendLine(" Left join GradeEmpComment GE on GS.CompID=GE.CompID and GS.ApplyTime=GE.ApplyTime and GS.ApplyID=GE.ApplyID and GS.EmpID=GE.EmpID and GS.Seq=GE.Seq and GS.GradeSeq=GE.GradeSeq ")
        strSQL.AppendLine(" Where G.CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine(" And G.GradeYear = " & Bsp.Utility.Quote(GradeYear))
        strSQL.AppendLine(" And G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
        strSQL.AppendLine(" And GS.ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
        strSQL.AppendLine(" And GS.Seq = " & Bsp.Utility.Quote(Seq))
        If MainFlag = "2" And DeptEx = "N" Then
            strSQL.AppendLine(" And G.UpOrganID =" & Bsp.Utility.Quote(ApplyID))
        Else
            strSQL.AppendLine(" And G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
        End If
        strSQL.AppendLine(" And G.GradeDeptID = G.OrderDeptID ")
        strSQL.AppendLine(" order By GS.GradeOrder Desc")

        strSQL.AppendLine(" Union")

        '配置考績為優/乙及配置考績與建議考績不一致
        strSQL.AppendLine(" Select G.CompID,G.EmpID,P.NameN,GS.ApplyID ")
        strSQL.AppendLine(" ,GS.GradeOrder ")
        strSQL.AppendLine(" ,isnull(GE.Comment,'') as Comment ")
        strSQL.AppendLine(" ,Case When isnull(GE.Comment,'N')='N' Then 'N' Else 'Y' End as IsExit ")
        strSQL.AppendLine(" ,isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'') as GradeA ")
        strSQL.AppendLine(" ,isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'') as GradeAdjust ")
        strSQL.AppendLine(" From  GradeBase G left join GradeSignLog GS on G.CompID=GS.CompID and G.GradeDeptID=GS.ApplyID and G.EmpID=GS.EmpID and GS.Seq=" & Seq & " And G.Online='1'")
        strSQL.AppendLine(" Left Join Personal P on G.CompID = P.CompID and G.EmpID = P.EmpID ")
        If MainFlag = "2" Then
            strSQL.AppendLine(" Left join GradeSignLog GS1 on G.CompID=GS1.CompID and G.GradeDeptID=GS1.ApplyID and G.EmpID=GS1.EmpID and GS1.Seq=" & Seq - 1 & " And GS.ApplyTime=GS1.ApplyTime and G.Online='1' ")
        End If
        strSQL.AppendLine(" Left join GradeEmpComment GE on GS.CompID=GE.CompID and GS.ApplyTime=GE.ApplyTime and GS.ApplyID=GE.ApplyID and GS.EmpID=GE.EmpID and GS.Seq=GE.Seq and GS.GradeSeq=GE.GradeSeq ")
        strSQL.AppendLine(" Where G.CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine(" And G.GradeYear = " & Bsp.Utility.Quote(GradeYear))
        strSQL.AppendLine(" And G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
        strSQL.AppendLine(" And GS.ApplyTime = " & Bsp.Utility.Quote(ApplyTime))
        strSQL.AppendLine(" And GS.Seq = " & Bsp.Utility.Quote(Seq))
        If MainFlag = "2" And DeptEx = "N" Then
            strSQL.AppendLine(" And G.UpOrganID =" & Bsp.Utility.Quote(ApplyID))
        Else
            strSQL.AppendLine(" And G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
        End If
        strSQL.AppendLine(" And G.GradeDeptID = G.OrderDeptID ")
        strSQL.AppendLine(" And (isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'')='9' or isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'') = '1' or  isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'') = '3' or  isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'') = '4' or isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'') <> isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'') ) ")

        strSQL.AppendLine(" ) S ")
        strSQL.AppendLine(" order by Case when GradeOrder = 0 then 1 else 0 end ,Cast(GradeOrder as int) ")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)

    End Function
#End Region

#End Region
#Region "GS1300 年度考核(單位主管查詢)"
#Region "GS1300 -查詢"
    Public Function GS1300Query(ByVal CompID As String, ByVal ApplyID As String, ByVal ApplyTime As String, ByVal Seq As String, ByVal Status As String, ByVal MainFlag As String _
                           , ByVal GradeYear As String, ByVal GradeSeq As String, ByVal EvaluationSeq As String, Optional GroupID As String = "") As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(ConfigurationManager.AppSettings("eHRMSDBDES").ToString)
        strSQL.AppendLine(" Select P.CompID,P.EmpID ,P.NameN ,Convert(char(19), P.EmpDate, 111) as EmpDate ,Po.PositionID,Po.Remark as Position ,W.Remark as WorkType ,P.RankID ,P.RankIDMap,isnull(T.TitleName,'') TitleName")
        strSQL.AppendLine(" ,isnull(CONVERT(char(1), DecryptByKey(EG3.Y3Grade)) ,'') as Y3Grade ,isnull(CONVERT(char(1), DecryptByKey(EG2.Y2Grade)),'') as Y2Grade ,isnull(CONVERT(char(1), DecryptByKey(EG1.Y1Grade)),'') as Y1Grade ")
        strSQL.AppendLine(" ,G.Memo ")
        strSQL.AppendLine(" ,isnull(CONVERT(char(1), DecryptByKey(G.GradeDept)) ,'') as GradeDept ")
        strSQL.AppendLine(" ,cast(Case when isnull(G.GradeOrderDept,0)=0 then '' else Cast(G.GradeOrderDept as Varchar(3)) end as int) as GradeOrderDept")
        strSQL.AppendLine(" ,GS.ScoreValue as  ScoreValueDept,Case When GS.ScoreValue<>0 then Cast(GS.ScoreValue as Varchar(2)) + '-' + GS.Score Else ' ' End as ScoreDept")
        strSQL.AppendLine(" ,G.ScoreValue as  ScoreValue,Case When G.ScoreValue<>0 then Cast(G.ScoreValue as Varchar(2)) + '-' + G.Score Else ' ' End as Score")
        strSQL.AppendLine(" ,isnull(A.Cnt,0) RewardCnt ,isnull(B.Cnt,0) PerformanceCnt ,O.OrganName")
        strSQL.AppendLine(" ,isnull(CONVERT(char(1), DecryptByKey(G.Grade)) ,'') as Grade ")
        strSQL.AppendLine(" ,isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'') as GradeAdjust ")
        strSQL.AppendLine(" ,isnull(CONVERT(char(1), DecryptByKey(G.GradeHR)) ,'') as GradeHR ")
        strSQL.AppendLine(" ,isnull(CONVERT(char(1), DecryptByKey(G.Grade2)) ,'') as Grade2 ")
        strSQL.AppendLine(" ,Case When GS.GradeOrder is null then 0 When GE.Comment is null Then 0 Else 1 End as CommentCnt ")
        strSQL.AppendLine(" from Personal P inner join GradeBase G on G.GradeYear = " & Bsp.Utility.Quote(GradeYear) & " and P.CompID =G.CompID and P.EmpID = G.EmpID and G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
        strSQL.AppendLine(" left join Organization O on O.CompID = G.CompID and O.OrganID = G.OrderDeptID")
        strSQL.AppendLine(" left join GradeSignLog GS on GS.CompID= " & Bsp.Utility.Quote(CompID) & " and GS.ApplyTime= " & Bsp.Utility.Quote(ApplyTime) & " and GS.ApplyID= " & Bsp.Utility.Quote(ApplyID) & " and GS.Seq= " & Bsp.Utility.Quote(Seq) & " and P.CompID =GS.CompID and P.EmpID = GS.EmpID and G.GradeSeq = GS.GradeSeq")
        strSQL.AppendLine(" left join (")
        strSQL.AppendLine(" Select GradeYear,ApplyTime,ApplyID,CompID,EmpID,GradeSeq,'1' as Comment From GradeEmpComment")
        strSQL.AppendLine(" union")
        strSQL.AppendLine(" Select GradeYear,ApplyTime,ApplyID,CompID,EmpID,GradeSeq,'1' as Comment From GradeEmpComment_ScoreAdjust")
        strSQL.AppendLine(" ) GE on GE.CompID= " & Bsp.Utility.Quote(CompID) & " and GE.ApplyTime= " & Bsp.Utility.Quote(ApplyTime) & " and GE.ApplyID= G.GradeDeptID and P.CompID =GE.CompID and P.EmpID = GE.EmpID and G.GradeSeq = GE.GradeSeq")
        strSQL.AppendLine(" left join Title T on P.CompID = T.CompID and P.RankID = T.RankID and P.TitleID = T.TitleID")
        strSQL.AppendLine(" left join EmpPosition EP1 on P.CompID = EP1.CompID and P.EmpID = EP1.EmpID and EP1.PrincipalFlag = '1'")
        strSQL.AppendLine(" left join Position Po on EP1.CompID = Po.CompID and EP1.PositionID = Po.PositionID ")
        strSQL.AppendLine(" left join EmpWorkType EW on P.CompID = EW.CompID and P.EmpID = EW.EmpID and EW.PrincipalFlag = '1'")
        strSQL.AppendLine(" left join WorkType W on EW.CompID = W.CompID and EW.WorkTypeID = W.WorkTypeID")
        strSQL.AppendLine(" left join ( select CompID,EmpID,Grade Y3Grade from EmpGrade where CompID ='SPHBK1' and GradeYear = " & GradeYear - 3 & ") EG3 on P.CompID = EG3.CompID and P.EmpID = EG3.EmpID")
        strSQL.AppendLine(" left join ( select CompID,EmpID,Grade Y2Grade from EmpGrade where CompID ='SPHBK1' and GradeYear = " & GradeYear - 2 & ") EG2 on P.CompID = EG2.CompID and P.EmpID = EG2.EmpID")
        strSQL.AppendLine(" left join ( select CompID,EmpID,Grade Y1Grade from EmpGrade where CompID ='SPHBK1' and GradeYear = " & GradeYear - 1 & ") EG1 on P.CompID = EG1.CompID and P.EmpID = EG1.EmpID")
        strSQL.AppendLine(" left join ( select distinct CompID,EmpID, 1 Cnt from EmpReward where CompID ='SPHBK1' and year(ValidDate) = " & Bsp.Utility.Quote(GradeYear) & " and EmpID <> '' ) A on P.CompID = A.CompID and P.EmpID = A.EmpID ")
        strSQL.AppendLine(" left join ( select CompID,EmpID, 1 Cnt from EmpPerformance where CompID ='SPHBK1' and EPYear= " & Bsp.Utility.Quote(GradeYear) & ") B on P.CompID = B.CompID and P.EmpID = B.EmpID")
        strSQL.AppendLine(" where G.CompID = " & Bsp.Utility.Quote(CompID) & " and G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID) & " and G.GradeDeptID = G.OrderDeptID and P.EmpID <> " & Bsp.Utility.Quote(UserProfile.UserID))
        If GroupID <> "" And GroupID <> "0" Then
            strSQL.AppendLine(" And G.GroupID=" & Bsp.Utility.Quote(GroupID))
        End If
        If MainFlag = "1" Then
            strSQL.Append("Order by Case when ltrim(isnull(CONVERT(char(1), DecryptByKey(G.GradeDept)) ,'')) = '9' then '0' when ltrim(isnull(CONVERT(char(1), DecryptByKey(G.GradeDept)) ,'')) = '6' then '1.9' when ltrim(isnull(CONVERT(char(1), DecryptByKey(G.GradeDept)) ,'')) = '7' then '2.1' when ltrim(isnull(CONVERT(char(1), DecryptByKey(G.GradeDept)) ,'')) = '' then '99' else ltrim(isnull(CONVERT(char(1), DecryptByKey(G.GradeDept)) ,'')) End")
            strSQL.AppendLine(",Case when G.GradeOrderDept = 0 then 1 else 0 end ,Cast(G.GradeOrderDept as int) ,Po.PositionID ,P.RankIDMap desc ,P.EmpID")
        Else
            strSQL.Append("Order by Case when ltrim(isnull(CONVERT(char(1), DecryptByKey(G.Grade2)) ,'')) = '9' then '0' when ltrim(isnull(CONVERT(char(1), DecryptByKey(G.Grade2)) ,'')) = '6' then '1.9' when ltrim(isnull(CONVERT(char(1), DecryptByKey(G.Grade2)) ,'')) = '7' then '2.1' when ltrim(isnull(CONVERT(char(1), DecryptByKey(G.Grade2)) ,'')) = '' then '99' else ltrim(isnull(CONVERT(char(1), DecryptByKey(G.Grade2)) ,'')) End")
            strSQL.AppendLine(", Case when G.GradeOrder = 0 then 1 else 0 end ,Cast(G.GradeOrder as int) ,Po.PositionID ,P.RankIDMap desc ,P.EmpID")
        End If


        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region
#Region "GS1300 -查詢筆數"
    Public Function GS1300QueryCount(ByVal CompID As String, ByVal ApplyID As String, ByVal ApplyTime As String, ByVal Seq As String, ByVal Status As String, ByVal MainFlag As String _
                           , ByVal GradeYear As String, ByVal GradeSeq As String, ByVal EvaluationSeq As String, Optional GroupID As String = "") As Integer
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" Select Count(P.CompID) as Cnt")
        strSQL.AppendLine(" from Personal P inner join GradeBase G on G.GradeYear = " & Bsp.Utility.Quote(GradeYear) & " and P.CompID =G.CompID and P.EmpID = G.EmpID and G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
        strSQL.AppendLine(" where G.CompID = " & Bsp.Utility.Quote(CompID) & " and G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID) & " and G.GradeDeptID = G.OrderDeptID and P.EmpID <> " & Bsp.Utility.Quote(UserProfile.UserID))
        If GroupID <> "" And GroupID <> "0" Then
            strSQL.AppendLine(" And G.GroupID=" & Bsp.Utility.Quote(GroupID))
        End If
        Return Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")
    End Function
#End Region
#Region "GS1300 年度考核(單位主管查詢)-下載"
    Public Function GS1300Download(ByVal CompID As String, ByVal ApplyID As String, ByVal ApplyTime As String, ByVal Seq As String, ByVal Status As String, ByVal MainFlag As String _
                           , ByVal GradeYear As String, ByVal GradeSeq As String, ByVal EvaluationSeq As String, ByVal IsPerformance As String, ByVal IsSignNext As String, Optional GroupID As String = "") As DataTable
        Dim strSQL As New StringBuilder()
        '員編、姓名、到職日、職位、職等、年度整體評量結果、年度排序、(配置考績、考績檢視建議)考核補充說明、整體評量調整說明、業績資料
        strSQL.AppendLine(ConfigurationManager.AppSettings("eHRMSDBDES").ToString)
        strSQL.AppendLine(" Select P.EmpID as '員編',P.NameN as '姓名',Convert(char(19), P.EmpDate, 111) as '到職日' ,Po.Remark as 職位 ,P.RankID as '職等' ")
        strSQL.AppendLine(" ,Case When GS.ScoreValue<>0 then Cast(GS.ScoreValue as Varchar(2)) + '-' + GS.Score Else ' ' End as '年度整體評量結果'")
        'strSQL.AppendLine(" ,Case when isnull(G.GradeOrderDept,0)=0 then '' else Cast(G.GradeOrderDept as Varchar(3)) end as '年度排序'")  '20160114 wei add
        If IsSignNext = "N" Then
            strSQL.AppendLine(" ,Case when isnull(G.GradeOrderDept,0)=0 then '' else Cast(G.GradeOrderDept as Varchar(3)) end as '排序'")   '20160114 wei add
            strSQL.AppendLine(" ,Case When isnull(CONVERT(char(1), DecryptByKey(GS.Grade)) ,'')='1' Then '優' ")
            strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.Grade)) ,'')='2' Then '甲' ")
            strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.Grade)) ,'')='3' Then '乙' ")
            strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.Grade)) ,'')='4' Then '丙' ")
            strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.Grade)) ,'')='6' Then '甲上' ")
            strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.Grade)) ,'')='7' Then '甲下' ")
            strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.Grade)) ,'')='9' Then '特優' ")
            strSQL.AppendLine(" Else '' End as '考績' ")
            'strSQL.AppendLine(" ,Case When isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'')='1' Then '優' ")
            'strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'')='2' Then '甲' ")
            'strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'')='3' Then '乙' ")
            'strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'')='4' Then '丙' ")
            'strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'')='6' Then '甲上' ")
            'strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'')='7' Then '甲下' ")
            'strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'')='9' Then '特優' ")
            'strSQL.AppendLine(" Else '' End as '考績檢視建議' ")
            '20160113 wei del
            'strSQL.AppendLine(" ,Case When isnull(CONVERT(char(1), DecryptByKey(G.GradeHR)) ,'')='1' Then '優' ")
            'strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeHR)) ,'')='2' Then '甲' ")
            'strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeHR)) ,'')='3' Then '乙' ")
            'strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeHR)) ,'')='4' Then '丙' ")
            'strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeHR)) ,'')='6' Then '甲上' ")
            'strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeHR)) ,'')='7' Then '甲下' ")
            'strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeHR)) ,'')='9' Then '特優' ")
            'strSQL.AppendLine(" Else '' End as '會議初核考績' ")
        End If
        '20160113 wei add
        strSQL.AppendLine(" ,Case When isnull(CONVERT(char(1), DecryptByKey(G.GradeHR)) ,'')='1' Then '優' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeHR)) ,'')='2' Then '甲' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeHR)) ,'')='3' Then '乙' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeHR)) ,'')='4' Then '丙' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeHR)) ,'')='6' Then '甲上' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeHR)) ,'')='7' Then '甲下' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeHR)) ,'')='9' Then '特優' ")
        strSQL.AppendLine(" Else '' End as '會議初核考績' ")
        If Status = "5" Then
            '20160113 wei del
            'If IsSignNext = "Y" Then
            '    strSQL.AppendLine(" ,Case When isnull(CONVERT(char(1), DecryptByKey(G.GradeHR)) ,'')='1' Then '優' ")
            '    strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeHR)) ,'')='2' Then '甲' ")
            '    strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeHR)) ,'')='3' Then '乙' ")
            '    strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeHR)) ,'')='4' Then '丙' ")
            '    strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeHR)) ,'')='6' Then '甲上' ")
            '    strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeHR)) ,'')='7' Then '甲下' ")
            '    strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeHR)) ,'')='9' Then '特優' ")
            '    strSQL.AppendLine(" Else '' End as '會議初核考績' ")
            'End If
            strSQL.AppendLine(" ,Case When isnull(CONVERT(char(1), DecryptByKey(G.Grade2)) ,'')='1' Then '優' ")
            strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.Grade2)) ,'')='2' Then '甲' ")
            strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.Grade2)) ,'')='3' Then '乙' ")
            strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.Grade2)) ,'')='4' Then '丙' ")
            strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.Grade2)) ,'')='6' Then '甲上' ")
            strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.Grade2)) ,'')='7' Then '甲下' ")
            strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.Grade2)) ,'')='9' Then '特優' ")
            strSQL.AppendLine(" Else '' End as '核定考績' ")
        End If
        '單位主管
        strSQL.AppendLine(" ,isnull(GE.Comment,'') as '考績補充說明'")
        strSQL.AppendLine(" ,isnull(GEA.Comment,'') as '整體評量調整說明'")
        If IsSignNext = "Y" Then
            '區處主管主管
            strSQL.AppendLine(" ,isnull(GE1.Comment,'') as '區處考績補充說明'")
            strSQL.AppendLine(" ,isnull(GEA1.Comment,'') as '區處整體評量調整說明'")
        End If

        If IsPerformance = "Y" Then
            strSQL.AppendLine(" ,isnull(EP.Fld1,'') as 'Q1\個金業務\房貸撥款量(仟元)',isnull(EP.Fld2,'') as 'Q1\個金業務\信貸撥款量(仟元)',isnull(EP.Fld3,'') as 'Q1\個金業務\總AP(仟點)',isnull(EP.Fld4,'') as 'Q1\個金業務\理財Score Card',isnull(EP.Fld5,'') as 'Q1\理財業務\總AP(仟點)',isnull(EP.Fld6,'') as 'Q1\理財業務\Score Card',isnull(EP.Fld7,'') as 'Q1\法金業務(單位)\總AP(仟點)',isnull(EP.Fld8,'') as 'Q1\法金業務(單位)\單位職系達成率' ")
            strSQL.AppendLine(" ,isnull(EP.Fld9,'') as 'Q2\個金業務\房貸撥款量(仟元)',isnull(EP.Fld10,'') as 'Q2\個金業務\信貸撥款量(仟元)',isnull(EP.Fld11,'') as 'Q2\個金業務\總AP(仟點)',isnull(EP.Fld12,'') as 'Q2\個金業務\理財Score Card',isnull(EP.Fld13,'') as 'Q2\理財業務\總AP(仟點)',isnull(EP.Fld14,'') as 'Q2\理財業務\Score Card',isnull(EP.Fld15,'') as 'Q2\法金業務(單位)\總AP(仟點)',isnull(EP.Fld16,'') as 'Q2\法金業務(單位)\單位職系達成率' ")
            strSQL.AppendLine(" ,isnull(EP.Fld17,'') as 'Q3\個金業務\房貸撥款量(仟元)',isnull(EP.Fld18,'') as 'Q3\個金業務\信貸撥款量(仟元)',isnull(EP.Fld19,'') as 'Q3\個金業務\總AP(仟點)',isnull(EP.Fld20,'') as 'Q3\個金業務\理財Score Card',isnull(EP.Fld21,'') as 'Q3\理財業務\總AP(仟點)',isnull(EP.Fld22,'') as 'Q3\理財業務\Score Card',isnull(EP.Fld23,'') as 'Q3\法金業務(單位)\總AP(仟點)',isnull(EP.Fld24,'') as 'Q3\法金業務(單位)\單位職系達成率' ")
            strSQL.AppendLine(" ,isnull(EP.Fld25,'') as '" & GradeYear & "年度平均\個金業務\房貸撥款量(仟元)',isnull(EP.Fld26,'') as '" & GradeYear & "年度平均\個金業務\信貸撥款量(仟元)',isnull(EP.Fld27,'') as '" & GradeYear & "年度平均\個金業務\總AP(仟點)',isnull(EP.Fld28,'') as '" & GradeYear & "年度平均\個金業務\理財Score Card',isnull(EP.Fld29,'') as '" & GradeYear & "年度平均\理財業務\總AP(仟點)',isnull(EP.Fld30,'') as '" & GradeYear & "年度平均\理財業務\Score Card',isnull(EP.Fld31,'') as '" & GradeYear & "年度平均\法金業務(單位)\總AP(仟點)',isnull(EP.Fld32,'') as '" & GradeYear & "年度平均\法金業務(單位)\單位職系達成率' ")
        End If
        strSQL.AppendLine(" from Personal P inner join GradeBase G on G.GradeYear = " & Bsp.Utility.Quote(GradeYear) & " and P.CompID =G.CompID and P.EmpID = G.EmpID and G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
        strSQL.AppendLine(" left join Organization O on O.CompID = G.CompID and O.OrganID = G.OrderDeptID")
        strSQL.AppendLine(" left join GradeSignLog GS on GS.CompID= " & Bsp.Utility.Quote(CompID) & " and GS.ApplyTime= " & Bsp.Utility.Quote(ApplyTime) & " and GS.ApplyID= G.GradeDeptID and GS.Seq= " & Bsp.Utility.Quote(Seq) & " and P.CompID =GS.CompID and P.EmpID = GS.EmpID and G.GradeSeq = GS.GradeSeq")
        strSQL.AppendLine(" left join GradeEmpComment GE on GE.CompID= " & Bsp.Utility.Quote(CompID) & " and GE.ApplyTime= " & Bsp.Utility.Quote(ApplyTime) & " and GE.ApplyID= G.GradeDeptID and GE.Seq= " & Bsp.Utility.Quote(Seq) & " and P.CompID =GE.CompID and P.EmpID = GE.EmpID and G.GradeSeq = GE.GradeSeq")
        strSQL.AppendLine(" left join GradeEmpComment_ScoreAdjust GEA on GEA.CompID= " & Bsp.Utility.Quote(CompID) & " and GEA.ApplyTime= " & Bsp.Utility.Quote(ApplyTime) & " and GEA.ApplyID= G.GradeDeptID and GEA.Seq= " & Bsp.Utility.Quote(Seq) & " and P.CompID =GEA.CompID and P.EmpID = GEA.EmpID and G.GradeSeq = GEA.GradeSeq")
        strSQL.AppendLine(" left join GradeEmpComment GE1 on GE1.CompID= " & Bsp.Utility.Quote(CompID) & " and GE1.ApplyTime= " & Bsp.Utility.Quote(ApplyTime) & " and GE1.ApplyID= G.GradeDeptID and GE1.Seq= " & Bsp.Utility.Quote(Seq + 1) & " and P.CompID =GE1.CompID and P.EmpID = GE1.EmpID and G.GradeSeq = GE1.GradeSeq")
        strSQL.AppendLine(" left join GradeEmpComment_ScoreAdjust GEA1 on GEA1.CompID= " & Bsp.Utility.Quote(CompID) & " and GEA1.ApplyTime= " & Bsp.Utility.Quote(ApplyTime) & " and GEA1.ApplyID= G.GradeDeptID and GEA1.Seq= " & Bsp.Utility.Quote(Seq + 1) & " and P.CompID =GEA1.CompID and P.EmpID = GEA1.EmpID and G.GradeSeq = GEA1.GradeSeq")
        strSQL.AppendLine(" left join Title T on P.CompID = T.CompID and P.RankID = T.RankID and P.TitleID = T.TitleID")
        strSQL.AppendLine(" left join EmpPosition EP1 on P.CompID = EP1.CompID and P.EmpID = EP1.EmpID and EP1.PrincipalFlag = '1'")
        strSQL.AppendLine(" left join Position Po on EP1.CompID = Po.CompID and EP1.PositionID = Po.PositionID ")
        strSQL.AppendLine(" left join EmpWorkType EW on P.CompID = EW.CompID and P.EmpID = EW.EmpID and EW.PrincipalFlag = '1'")
        strSQL.AppendLine(" left join WorkType W on EW.CompID = W.CompID and EW.WorkTypeID = W.WorkTypeID")
        strSQL.AppendLine(" left join EmpPerformance EP on P.CompID = EP.CompID and P.EmpID = EP.EmpID and EP.EPYear = " & Bsp.Utility.Quote(GradeYear))
        strSQL.AppendLine(" where G.CompID = " & Bsp.Utility.Quote(CompID) & " and G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID) & " and G.GradeDeptID = G.OrderDeptID and P.EmpID <> " & Bsp.Utility.Quote(UserProfile.UserID))
        If GroupID <> "" And GroupID <> "0" Then
            strSQL.AppendLine(" And G.GroupID=" & Bsp.Utility.Quote(GroupID))
        End If

        If MainFlag = "1" Then
            strSQL.Append("Order by Case when ltrim(isnull(CONVERT(char(1), DecryptByKey(G.GradeDept)) ,'')) = '9' then '0' when ltrim(isnull(CONVERT(char(1), DecryptByKey(G.GradeDept)) ,'')) = '6' then '1.9' when ltrim(isnull(CONVERT(char(1), DecryptByKey(G.GradeDept)) ,'')) = '7' then '2.1' when ltrim(isnull(CONVERT(char(1), DecryptByKey(G.GradeDept)) ,'')) = '' then '99' else ltrim(isnull(CONVERT(char(1), DecryptByKey(G.GradeDept)) ,'')) End")
            strSQL.AppendLine(",Case when G.GradeOrderDept = 0 then 1 else 0 end ,Cast(G.GradeOrderDept as int) ,Po.PositionID ,P.RankIDMap desc ,P.EmpID")
        Else
            strSQL.Append("Order by Case when ltrim(isnull(CONVERT(char(1), DecryptByKey(G.Grade2)) ,'')) = '9' then '0' when ltrim(isnull(CONVERT(char(1), DecryptByKey(G.Grade2)) ,'')) = '6' then '1.9' when ltrim(isnull(CONVERT(char(1), DecryptByKey(G.Grade2)) ,'')) = '7' then '2.1' when ltrim(isnull(CONVERT(char(1), DecryptByKey(G.Grade2)) ,'')) = '' then '99' else ltrim(isnull(CONVERT(char(1), DecryptByKey(G.Grade2)) ,'')) End")
            strSQL.AppendLine(", Case when G.GradeOrder = 0 then 1 else 0 end ,Cast(G.GradeOrder as int) ,Po.PositionID ,P.RankIDMap desc ,P.EmpID")
        End If

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region
#End Region
#Region "GS1400 年度考核(區處主管查詢)"
    Public Function GS1400Query(ByVal CompID As String, ByVal ApplyID As String, ByVal ApplyTime As String, ByVal Seq As String, ByVal Status As String, ByVal MainFlag As String _
                           , ByVal GradeYear As String, ByVal GradeSeq As String, ByVal EvaluationSeq As String, ByVal DeptEx As String, Optional GroupID As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(ConfigurationManager.AppSettings("eHRMSDBDES").ToString)
        strSQL.AppendLine(" Select P.CompID,P.EmpID ,P.NameN ,Convert(char(19), P.EmpDate, 111) as EmpDate ,Po.PositionID,Po.Remark as Position ,W.Remark as WorkType ,P.RankID ,P.RankIDMap,isnull(T.TitleName,'') TitleName")
        strSQL.AppendLine(" ,isnull(CONVERT(char(1), DecryptByKey(EG3.Y3Grade)) ,'') as Y3Grade ,isnull(CONVERT(char(1), DecryptByKey(EG2.Y2Grade)),'') as Y2Grade ,isnull(CONVERT(char(1), DecryptByKey(EG1.Y1Grade)),'') as Y1Grade ")
        strSQL.AppendLine(" ,G.GradeDeptID as ApplyID, G.Memo ")
        strSQL.AppendLine(" ,isnull(CONVERT(char(1), DecryptByKey(G.Grade)) ,'') as Grade ")
        strSQL.AppendLine(" ,cast(Case when isnull(G.GradeOrder,0)=0 then '' else Cast(G.GradeOrder as Varchar(3)) end as int) as GradeOrder")
        strSQL.AppendLine(" ,isnull(G.ScoreValue,'0') as ScoreValue  ,isnull(G.Score,'') as Score")
        strSQL.AppendLine(" ,isnull(A.Cnt,0) RewardCnt ,isnull(B.Cnt,0) PerformanceCnt ,O.OrganName")
        strSQL.AppendLine(" ,isnull(GS1.ScoreValue,'0') as DeptScoreValue, isnull(GS1.Score,'') as DeptScore ")
        strSQL.AppendLine(" ,isnull(CONVERT(char(1), DecryptByKey(G.GradeDept)) ,'') as GradeDept, G.GradeOrderDept ")
        strSQL.AppendLine(" ,isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'') as GradeAdjust ")
        strSQL.AppendLine(" ,DeptCnt=isnull((Select Count(*) From GradeBase Where CompID=G.CompID And GradeDeptID=G.GradeDeptID And GradeYear=G.GradeYear and GroupID=G.GroupID And GradeSeq=" & Bsp.Utility.Quote(GradeSeq) & "),'') ")    '20160720 wei modify
        strSQL.AppendLine(" ,isnull(CONVERT(char(1), DecryptByKey(G.GradeHR)) ,'') as GradeHR ")
        strSQL.AppendLine(" ,isnull(CONVERT(char(1), DecryptByKey(G.Grade2)) ,'') as Grade2 ")
        strSQL.AppendLine(" ,Case When GS.GradeOrder is null then 0 When GE.Comment is null Then 0 Else 1 End as CommentCnt ")
        strSQL.AppendLine(" from Personal P inner join GradeBase G on G.GradeYear = " & Bsp.Utility.Quote(GradeYear) & " and P.CompID =G.CompID and P.EmpID = G.EmpID and G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
        strSQL.AppendLine(" left join Organization O on O.CompID = G.CompID and O.OrganID = G.OrderDeptID")
        strSQL.AppendLine(" left join GradeSignLog GS on GS.CompID= " & Bsp.Utility.Quote(CompID) & " and GS.ApplyTime= " & Bsp.Utility.Quote(ApplyTime) & " and GS.ApplyID = G.GradeDeptID and GS.Seq= " & Bsp.Utility.Quote(Seq) & " and G.CompID =GS.CompID and G.EmpID = GS.EmpID and G.GradeSeq = GS.GradeSeq")
        strSQL.AppendLine(" left join GradeSignLog GS1 on GS1.CompID= " & Bsp.Utility.Quote(CompID) & " and GS1.ApplyTime= " & Bsp.Utility.Quote(ApplyTime) & " and GS1.ApplyID= G.GradeDeptID and GS1.Seq= " & Bsp.Utility.Quote(Seq - 1) & " and G.CompID =GS1.CompID and G.EmpID = GS1.EmpID and G.GradeSeq = GS1.GradeSeq")
        strSQL.AppendLine(" left join (")
        strSQL.AppendLine(" Select GradeYear,ApplyTime,ApplyID,CompID,EmpID,GradeSeq,'1' as Comment From GradeEmpComment")
        strSQL.AppendLine(" union")
        strSQL.AppendLine(" Select GradeYear,ApplyTime,ApplyID,CompID,EmpID,GradeSeq,'1' as Comment From GradeEmpComment_ScoreAdjust")
        strSQL.AppendLine(" ) GE on GE.CompID= " & Bsp.Utility.Quote(CompID) & " and GE.ApplyTime= " & Bsp.Utility.Quote(ApplyTime) & " and GE.ApplyID= G.GradeDeptID and P.CompID =GE.CompID and P.EmpID = GE.EmpID and G.GradeSeq = GE.GradeSeq")
        strSQL.AppendLine(" left join Title T on P.CompID = T.CompID and P.RankID = T.RankID and P.TitleID = T.TitleID")
        strSQL.AppendLine(" left join EmpPosition EP1 on P.CompID = EP1.CompID and P.EmpID = EP1.EmpID and EP1.PrincipalFlag = '1'")
        strSQL.AppendLine(" left join Position Po on EP1.CompID = Po.CompID and EP1.PositionID = Po.PositionID ")
        strSQL.AppendLine(" left join EmpWorkType EW on P.CompID = EW.CompID and P.EmpID = EW.EmpID and EW.PrincipalFlag = '1'")
        strSQL.AppendLine(" left join WorkType W on EW.CompID = W.CompID and EW.WorkTypeID = W.WorkTypeID")
        strSQL.AppendLine(" left join ( select CompID,EmpID,Grade Y3Grade from EmpGrade where CompID ='SPHBK1' and GradeYear = " & GradeYear - 3 & ") EG3 on P.CompID = EG3.CompID and P.EmpID = EG3.EmpID")
        strSQL.AppendLine(" left join ( select CompID,EmpID,Grade Y2Grade from EmpGrade where CompID ='SPHBK1' and GradeYear = " & GradeYear - 2 & ") EG2 on P.CompID = EG2.CompID and P.EmpID = EG2.EmpID")
        strSQL.AppendLine(" left join ( select CompID,EmpID,Grade Y1Grade from EmpGrade where CompID ='SPHBK1' and GradeYear = " & GradeYear - 1 & ") EG1 on P.CompID = EG1.CompID and P.EmpID = EG1.EmpID")
        strSQL.AppendLine(" left join ( select distinct CompID,EmpID, 1 Cnt from EmpReward where CompID ='SPHBK1' and year(ValidDate) = " & Bsp.Utility.Quote(GradeYear) & " and EmpID <> '' ) A on P.CompID = A.CompID and P.EmpID = A.EmpID ")
        strSQL.AppendLine(" left join ( select CompID,EmpID, 1 Cnt from EmpPerformance where CompID ='SPHBK1' and EPYear= " & Bsp.Utility.Quote(GradeYear) & ") B on P.CompID = B.CompID and P.EmpID = B.EmpID")
        strSQL.AppendLine(" where G.CompID = " & Bsp.Utility.Quote(CompID))
        If DeptEx = "Y" Then
            strSQL.AppendLine(" and G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
        Else
            strSQL.AppendLine(" And G.UpOrganID =" & Bsp.Utility.Quote(ApplyID))
        End If
        strSQL.AppendLine(" and G.GradeDeptID = G.OrderDeptID and P.EmpID <> " & Bsp.Utility.Quote(UserProfile.UserID))
        If GroupID <> "" And GroupID <> "0" Then
            strSQL.AppendLine(" And G.GroupID=" & Bsp.Utility.Quote(GroupID))
        End If
        strSQL.Append("Order by Case when ltrim(isnull(CONVERT(char(1), DecryptByKey(G.Grade2)) ,'')) = '9' then '0' when ltrim(isnull(CONVERT(char(1), DecryptByKey(G.Grade2)) ,'')) = '6' then '1.9' when ltrim(isnull(CONVERT(char(1), DecryptByKey(G.Grade2)) ,'')) = '7' then '2.1' when ltrim(isnull(CONVERT(char(1), DecryptByKey(G.Grade2)) ,'')) = '' then '99' else ltrim(isnull(CONVERT(char(1), DecryptByKey(G.Grade2)) ,'')) End")
        strSQL.AppendLine("order by Case when isnull(GS.GradeOrder,0) = 0 then 1 else 0 end ,Cast(isnull(GS.GradeOrder,0) as int) ,Po.PositionID ,P.RankIDMap desc ,P.EmpID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#Region "GS1400 年度考核(區處主管查詢)筆數"
    Public Function GS1400QueryCount(ByVal CompID As String, ByVal ApplyID As String, ByVal ApplyTime As String, ByVal Seq As String, ByVal Status As String, ByVal MainFlag As String _
                           , ByVal GradeYear As String, ByVal GradeSeq As String, ByVal EvaluationSeq As String, ByVal DeptEx As String, Optional GroupID As String = "") As Integer
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" Select Count(P.CompID) as Cnt")
        strSQL.AppendLine(" from Personal P inner join GradeBase G on G.GradeYear = " & Bsp.Utility.Quote(GradeYear) & " and P.CompID =G.CompID and P.EmpID = G.EmpID and G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
        strSQL.AppendLine(" where G.CompID = " & Bsp.Utility.Quote(CompID))
        If DeptEx = "Y" Then
            strSQL.AppendLine(" and G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
        Else
            strSQL.AppendLine(" And G.UpOrganID =" & Bsp.Utility.Quote(ApplyID))
        End If
        strSQL.AppendLine(" and G.GradeDeptID = G.OrderDeptID and P.EmpID <> " & Bsp.Utility.Quote(UserProfile.UserID))
        If GroupID <> "" And GroupID <> "0" Then
            strSQL.AppendLine(" And G.GroupID=" & Bsp.Utility.Quote(GroupID))
        End If
        Return Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")
    End Function
#End Region
#Region "GS1400 年度考核(區處主管查詢)-下載"
    Public Function GS1400Download(ByVal CompID As String, ByVal ApplyID As String, ByVal ApplyTime As String, ByVal Seq As String, ByVal Status As String, ByVal MainFlag As String _
                           , ByVal GradeYear As String, ByVal GradeSeq As String, ByVal EvaluationSeq As String, ByVal IsPerformance As String, ByVal IsSignNext As String, ByVal DeptEX As String, Optional GroupID As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        '單位、員編、姓名、到職日、職位、職等、年度整體評量結果、【單位主管】初排序、配置考績(=2015年考績檢核用的值)、【區處主管】排序、配置考績、考績檢視建議，【單位主管】整體評量調整說明、考核補充說明、【區處主管】整體評量調整說明、考績檢視補充說明、業績資料
        '(標題拆兩列，單位主管 跟 區處主管 標題，要用跨欄置中顯示)
        strSQL.AppendLine(ConfigurationManager.AppSettings("eHRMSDBDES").ToString)
        strSQL.AppendLine(" Select O.OrganName as '單位', P.EmpID as '員編',P.NameN as '姓名',Convert(char(19), P.EmpDate, 111) as '到職日' ,Po.Remark as '職位' ,P.RankID as '職等'")
        strSQL.AppendLine(" ,Case When G.ScoreValue<>0 then Cast(G.ScoreValue as Varchar(2)) + '-' + G.Score Else ' ' End as '年度整體評量結果'")
        '單位主管
        strSQL.AppendLine(" ,G.GradeOrderDept as '初排序'")
        strSQL.AppendLine(" ,Case When isnull(CONVERT(char(1), DecryptByKey(G.GradeDept)) ,'')='1' Then '優' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeDept)) ,'')='2' Then '甲' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeDept)) ,'')='3' Then '乙' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeDept)) ,'')='4' Then '丙' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeDept)) ,'')='6' Then '甲上' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeDept)) ,'')='7' Then '甲下' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeDept)) ,'')='9' Then '特優' ")
        strSQL.AppendLine(" Else '' End as '配置考績' ")
        '區處主管
        strSQL.AppendLine(" ,Case when isnull(G.GradeOrder,0)=0 then '' else Cast(G.GradeOrder as Varchar(3)) end as 排序")   '20160113 wei modify
        strSQL.AppendLine(" ,Case When isnull(CONVERT(char(1), DecryptByKey(G.Grade)) ,'')='1' Then '優' ")  '20160114 wei add
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.Grade)) ,'')='2' Then '甲' ")    '20160114 wei add
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.Grade)) ,'')='3' Then '乙' ")    '20160114 wei add
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.Grade)) ,'')='4' Then '丙' ")    '20160114 wei add
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.Grade)) ,'')='6' Then '甲上' ")   '20160114 wei add
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.Grade)) ,'')='7' Then '甲下' ")   '20160114 wei add
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.Grade)) ,'')='9' Then '特優' ")   '20160114 wei add
        strSQL.AppendLine(" Else '' End as '考績' ")
        'strSQL.AppendLine(" ,Case When isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'')='1' Then '優' ")
        'strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'')='2' Then '甲' ")
        'strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'')='3' Then '乙' ")
        'strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'')='4' Then '丙' ")
        'strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'')='6' Then '甲上' ")
        'strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'')='7' Then '甲下' ")
        'strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeAdjust)) ,'')='9' Then '特優' ")
        'strSQL.AppendLine(" Else '' End as '考績檢視建議' ")
        strSQL.AppendLine(" ,Case When isnull(CONVERT(char(1), DecryptByKey(G.GradeHR)) ,'')='1' Then '優' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeHR)) ,'')='2' Then '甲' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeHR)) ,'')='3' Then '乙' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeHR)) ,'')='4' Then '丙' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeHR)) ,'')='6' Then '甲上' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeHR)) ,'')='7' Then '甲下' ")
        strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeHR)) ,'')='9' Then '特優' ")
        strSQL.AppendLine(" Else '' End as '會議初核考績' ")
        If Status = "5" Then
            strSQL.AppendLine(" ,Case When isnull(CONVERT(char(1), DecryptByKey(G.Grade2)) ,'')='1' Then '優' ")
            strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.Grade2)) ,'')='2' Then '甲' ")
            strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.Grade2)) ,'')='3' Then '乙' ")
            strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.Grade2)) ,'')='4' Then '丙' ")
            strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.Grade2)) ,'')='6' Then '甲上' ")
            strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.Grade2)) ,'')='7' Then '甲下' ")
            strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.Grade2)) ,'')='9' Then '特優' ")
            strSQL.AppendLine(" Else '' End as '核定考績' ")
        End If


        '單位主管
        strSQL.AppendLine(" ,isnull(GEA1.Comment,'') as '整體評量調整說明',isnull(GE1.Comment,'') as '考績補充說明' ")
        '區處主管
        strSQL.AppendLine(" ,isnull(GEA.Comment,'') as '整體評量調整說明',isnull(GE.Comment,'') as '考績補充說明' ")
        If IsPerformance = "Y" Then
            strSQL.AppendLine(" ,isnull(EP.Fld1,'') as 'Q1\個金業務\房貸撥款量(仟元)',isnull(EP.Fld2,'') as 'Q1\個金業務\信貸撥款量(仟元)',isnull(EP.Fld3,'') as 'Q1\個金業務\總AP(仟點)',isnull(EP.Fld4,'') as 'Q1\個金業務\理財Score Card',isnull(EP.Fld5,'') as 'Q1\理財業務\總AP(仟點)',isnull(EP.Fld6,'') as 'Q1\理財業務\Score Card',isnull(EP.Fld7,'') as 'Q1\法金業務(單位)\總AP(仟點)',isnull(EP.Fld8,'') as 'Q1\法金業務(單位)\單位職系達成率' ")
            strSQL.AppendLine(" ,isnull(EP.Fld9,'') as 'Q2\個金業務\房貸撥款量(仟元)',isnull(EP.Fld10,'') as 'Q2\個金業務\信貸撥款量(仟元)',isnull(EP.Fld11,'') as 'Q2\個金業務\總AP(仟點)',isnull(EP.Fld12,'') as 'Q2\個金業務\理財Score Card',isnull(EP.Fld13,'') as 'Q2\理財業務\總AP(仟點)',isnull(EP.Fld14,'') as 'Q2\理財業務\Score Card',isnull(EP.Fld15,'') as 'Q2\法金業務(單位)\總AP(仟點)',isnull(EP.Fld16,'') as 'Q2\法金業務(單位)\單位職系達成率' ")
            strSQL.AppendLine(" ,isnull(EP.Fld17,'') as 'Q3\個金業務\房貸撥款量(仟元)',isnull(EP.Fld18,'') as 'Q3\個金業務\信貸撥款量(仟元)',isnull(EP.Fld19,'') as 'Q3\個金業務\總AP(仟點)',isnull(EP.Fld20,'') as 'Q3\個金業務\理財Score Card',isnull(EP.Fld21,'') as 'Q3\理財業務\總AP(仟點)',isnull(EP.Fld22,'') as 'Q3\理財業務\Score Card',isnull(EP.Fld23,'') as 'Q3\法金業務(單位)\總AP(仟點)',isnull(EP.Fld24,'') as 'Q3\法金業務(單位)\單位職系達成率' ")
            strSQL.AppendLine(" ,isnull(EP.Fld25,'') as '" & GradeYear & "年度平均\個金業務\房貸撥款量(仟元)',isnull(EP.Fld26,'') as '" & GradeYear & "年度平均\個金業務\信貸撥款量(仟元)',isnull(EP.Fld27,'') as '" & GradeYear & "年度平均\個金業務\總AP(仟點)',isnull(EP.Fld28,'') as '" & GradeYear & "年度平均\個金業務\理財Score Card',isnull(EP.Fld29,'') as '" & GradeYear & "年度平均\理財業務\總AP(仟點)',isnull(EP.Fld30,'') as '" & GradeYear & "年度平均\理財業務\Score Card',isnull(EP.Fld31,'') as '" & GradeYear & "年度平均\法金業務(單位)\總AP(仟點)',isnull(EP.Fld32,'') as '" & GradeYear & "年度平均\法金業務(單位)\單位職系達成率' ")
        End If
        strSQL.AppendLine(" from Personal P inner join GradeBase G on G.GradeYear = " & Bsp.Utility.Quote(GradeYear) & " and P.CompID =G.CompID and P.EmpID = G.EmpID and G.GradeSeq = " & Bsp.Utility.Quote(GradeSeq))
        strSQL.AppendLine(" left join Organization O on O.CompID = G.CompID and O.OrganID = G.OrderDeptID")
        strSQL.AppendLine(" left join GradeSignLog GS on GS.CompID= " & Bsp.Utility.Quote(CompID) & " and GS.ApplyTime= " & Bsp.Utility.Quote(ApplyTime) & " and GS.ApplyID = G.GradeDeptID and GS.Seq= " & Bsp.Utility.Quote(Seq) & " and G.CompID =GS.CompID and G.EmpID = GS.EmpID and G.GradeSeq = GS.GradeSeq")
        strSQL.AppendLine(" left join GradeSignLog GS1 on GS.CompID= " & Bsp.Utility.Quote(CompID) & " and GS.ApplyTime= " & Bsp.Utility.Quote(ApplyTime) & " and GS.ApplyID= G.GradeDeptID and GS.Seq= " & Bsp.Utility.Quote(Seq - 1) & " and G.CompID =GS.CompID and G.EmpID = GS.EmpID and G.GradeSeq = GS.GradeSeq")
        strSQL.AppendLine(" left join GradeEmpComment GE on GE.CompID= " & Bsp.Utility.Quote(CompID) & " and GE.ApplyTime= " & Bsp.Utility.Quote(ApplyTime) & " and GE.ApplyID= G.GradeDeptID and GE.Seq= " & Bsp.Utility.Quote(Seq) & " and P.CompID =GE.CompID and P.EmpID = GE.EmpID and G.GradeSeq = GE.GradeSeq")
        strSQL.AppendLine(" left join GradeEmpComment_ScoreAdjust GEA on GEA.CompID= " & Bsp.Utility.Quote(CompID) & " and GEA.ApplyTime= " & Bsp.Utility.Quote(ApplyTime) & " and GEA.ApplyID= G.GradeDeptID and GEA.Seq= " & Bsp.Utility.Quote(Seq) & " and P.CompID =GEA.CompID and P.EmpID = GEA.EmpID and G.GradeSeq = GEA.GradeSeq")
        strSQL.AppendLine(" left join GradeEmpComment GE1 on GE1.CompID= " & Bsp.Utility.Quote(CompID) & " and GE1.ApplyTime= " & Bsp.Utility.Quote(ApplyTime) & " and GE1.ApplyID= G.GradeDeptID and GE1.Seq= " & Bsp.Utility.Quote(Seq - 1) & " and P.CompID =GE1.CompID and P.EmpID = GE1.EmpID and G.GradeSeq = GE1.GradeSeq")
        strSQL.AppendLine(" left join GradeEmpComment_ScoreAdjust GEA1 on GEA1.CompID= " & Bsp.Utility.Quote(CompID) & " and GEA1.ApplyTime= " & Bsp.Utility.Quote(ApplyTime) & " and GEA1.ApplyID= G.GradeDeptID and GEA1.Seq= " & Bsp.Utility.Quote(Seq - 1) & " and P.CompID =GEA1.CompID and P.EmpID = GEA1.EmpID and G.GradeSeq = GEA1.GradeSeq")
        strSQL.AppendLine(" left join Title T on P.CompID = T.CompID and P.RankID = T.RankID and P.TitleID = T.TitleID")
        strSQL.AppendLine(" left join EmpPosition EP1 on P.CompID = EP1.CompID and P.EmpID = EP1.EmpID and EP1.PrincipalFlag = '1'")
        strSQL.AppendLine(" left join Position Po on EP1.CompID = Po.CompID and EP1.PositionID = Po.PositionID ")
        strSQL.AppendLine(" left join EmpWorkType EW on P.CompID = EW.CompID and P.EmpID = EW.EmpID and EW.PrincipalFlag = '1'")
        strSQL.AppendLine(" left join WorkType W on EW.CompID = W.CompID and EW.WorkTypeID = W.WorkTypeID")
        strSQL.AppendLine(" left join EmpPerformance EP on P.CompID = EP.CompID and P.EmpID = EP.EmpID and EP.EPYear = " & Bsp.Utility.Quote(GradeYear))
        strSQL.AppendLine(" where G.CompID = " & Bsp.Utility.Quote(CompID))
        If DeptEX = "Y" Then
            strSQL.AppendLine(" and G.GradeDeptID = " & Bsp.Utility.Quote(ApplyID))
        Else
            strSQL.AppendLine(" And G.UpOrganID =" & Bsp.Utility.Quote(ApplyID))
        End If
        strSQL.AppendLine(" and G.GradeDeptID = G.OrderDeptID and P.EmpID <> " & Bsp.Utility.Quote(UserProfile.UserID))
        If GroupID <> "" And GroupID <> "0" Then
            strSQL.AppendLine(" And G.GroupID=" & Bsp.Utility.Quote(GroupID))
        End If
        strSQL.Append("Order by Case when ltrim(isnull(CONVERT(char(1), DecryptByKey(G.Grade2)) ,'')) = '9' then '0' when ltrim(isnull(CONVERT(char(1), DecryptByKey(G.Grade2)) ,'')) = '6' then '1.9' when ltrim(isnull(CONVERT(char(1), DecryptByKey(G.Grade2)) ,'')) = '7' then '2.1' when ltrim(isnull(CONVERT(char(1), DecryptByKey(G.Grade2)) ,'')) = '' then '99' else ltrim(isnull(CONVERT(char(1), DecryptByKey(G.Grade2)) ,'')) End")
        strSQL.AppendLine(",Case  when isnull(G.GradeOrder,0) = 0 then 0 else Cast(isnull(G.GradeOrder,0) as int) end ,Cast(isnull(G.GradeOrder,0) as int) ,Po.PositionID ,P.RankIDMap desc ,P.EmpID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region
#End Region
#Region "查詢單一欄位值"
    Public Function QueryData(ByVal strTable As String, ByVal strWhere As String, ByVal strcolum As String, ByVal strJoin As String) As String
        Dim strSQL As String
        strSQL = "SELECT " & strcolum & " FROM " & strTable
        strSQL = strSQL & strJoin
        strSQL = strSQL & " WHERE 1 = 1 " & strWhere
        If Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB") = Nothing Then
            Return ""
        Else
            Return Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")
        End If
    End Function
#End Region

End Class
