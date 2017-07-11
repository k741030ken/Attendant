Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.Common
Imports System.Reflection
Imports System.Globalization

Public Class OV2
    Public Property DetailType As String 'detail statistics
    Public Property Type As String
    Public Property CompID As String
    Public Property OrgType As String
    Public Property DeptID As String
    Public Property OrganID As String
    Public Property OTEmpID As String
    Public Property OTEmpName As String
    Public Property WorkStatus As String
    Public Property RankIDMIN As String
    Public Property RankIDMAX As String
    Public Property TitleIDMIN As String '20170304 kevin

    Public Property TitleIDMAX As String '20170304 kevin
    Public Property TitleID As String
    Public Property TitleName As String
    Public Property PositionID As String
    Public Property OTStatus As String
    Public Property OTStatus1 As String
    Public Property OTFormNO As String
    Public Property OvertimeDateB As String
    Public Property OvertimeDateE As String
    Public Property OTPayDate As String
    Public Property OTSalaryOrAdjust As String
    'Public Property OTSalaryPaid As String

    Private Property eHRMSDB_ITRD As String
        Get
            Dim result As String = ConfigurationManager.AppSettings("eHRMSDB")
            If String.IsNullOrEmpty(result) Then
                result = "eHRMSDB_ITRD"
            End If
            Return result
        End Get
        Set(ByVal value As String)

        End Set
    End Property

    Private Property AattendantDB As String
        Get
            Dim result As String = ConfigurationManager.AppSettings("AattendantDB")
            If String.IsNullOrEmpty(result) Then
                result = "AattendantDB"
            End If
            Return result
        End Get
        Set(ByVal value As String)

        End Set
    End Property

    Private Property Config_AattendantDBFlowCase As String
        Get
            Dim result As String = OVBusinessCommon.AattendantDBFlowCase
            Return result
        End Get
        Set(ByVal value As String)

        End Set
    End Property

    Private Property Config_AattendantDBFlowFullLog As String
        Get
            Dim result As String = OVBusinessCommon.AattendantDBFlowFullLog
            Return result
        End Get
        Set(ByVal value As String)

        End Set
    End Property

    Private Property Config_AattendantDBFlowOpenLog As String
        Get
            Dim result As String = OVBusinessCommon.AattendantDBFlowOpenLog
            Return result
        End Get
        Set(ByVal value As String)

        End Set
    End Property

#Region "取得SqlStr"

    Private Function getStrSqlForOV2000_OVA_DB() As String
        Dim strSQL As New StringBuilder
        strSQL.AppendLine(" select P.EmpID AS 'OVAEmpID',P.Name AS 'OVAName',OVA.OTCompID AS 'OVAOTCompID',OVA.OTStartDate AS 'OVAOTStartDate',OVA.OTEndDate AS 'OVAOTEndDate',OVA.OTSeq AS 'OVAOTSeq',OVA.OTStartTime AS 'OVAOTStartTime',OVA.OTEndTime AS 'OVAOTEndTime',AT.CodeCName AS 'OVAOTTypeName' ,OVA.OTTxnID AS 'OVAOTTxnID' ,OVA.OTSeqNo AS 'OVAOTSeqNo' ,OVA.OTReasonMemo AS 'OVAOTReasonMemo' ,OVA.OTStatus AS 'OVAOTStatus'")
        strSQL.AppendLine(" from OverTimeAdvance OVA   ")
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Personal  P on OVA.OTCompID= P.CompID and OVA.OTEmpID = P.EmpID   ")
        strSQL.AppendLine(" left join AT_CodeMap AT on OVA.OTTypeID=AT.Code and AT.TabName='OverTime' and AT.FldName='OverTimeType' ")
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].WorkStatus  WS ON P.WorkStatus= WS.WorkCode   ")
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Title  T ON P.CompID=T.CompID and P.RankID=T.RankID and P.TitleID=T.TitleID   ")
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Company C ON P.CompID=C.CompID   ")
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].EmpPosition EP ON P.CompID=EP.CompID AND P.EmpID=EP.EmpID AND EP.PrincipalFlag='1'   ")
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Position PO ON EP.PositionID=PO.PositionID    ")
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].EmpWorkType EWT ON P.CompID=EWT.CompID AND P.EmpID=EWT.EmpID and EWT.PrincipalFlag='1'   ")
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].WorkType W ON EWT.WorkTypeID= W.WorkTypeID  AND P.CompID=W.CompID   ")
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Organization ORT ON  P.OrganID=ORT.OrganID AND P.CompID =ORT.CompID   ")
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Organization ORT1 ON P.DeptID=ORT1.OrganID   ")
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Organization ORT2 ON ORT.OrgType=ORT2.OrganID   ")
        '20170304kevin
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].RankMapping RM on P.RankID=RM.RankID  AND RM.CompID=OVA.OTCompID")
        'strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].RankMapping RM on P.RankID=RM.RankID")
        'strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].RankMapping TRM on T.RankID=RM.RankID")
        strSQL.AppendLine(" Where 1=1  ")
        strSQL.AppendLine(" and OVA.OTCompID='" + UserProfile.SelectCompRoleID + "'  ")

        If isNull("OTSalaryOrAdjust") Then
            strSQL.AppendLine(" and OVA.SalaryOrAdjust='" + OTSalaryOrAdjust + "'")
        End If

        If isNull("OTEmpName") Then
            strSQL.AppendLine(" and P.Name='" + OTEmpName + "'")
        End If

        If isNull("OvertimeDateB") Then
            strSQL.AppendLine(" and OVA.OTStartDate>='" + OvertimeDateB + "'")
        End If
        If isNull("OvertimeDateE") Then
            strSQL.AppendLine(" and OVA.OTEndDate<='" + OvertimeDateE + "'")
        End If

        If isNull("OTStatus") Then
            strSQL.AppendLine("and OVA.OTStatus='" + OTStatus + "'")
        End If


        If isNull("OTFormNO") Then
            strSQL.AppendLine(" and OVA.OTFormNO='" + OTFormNO + "'")
        End If
        If isNull("OTEmpID") Then
            strSQL.AppendLine(" and P.EmpID='" + OTEmpID + "'")
        End If

        If isNull("WorkStatus") Then
            strSQL.AppendLine(" and P.WorkStatus='" + WorkStatus + "'")
        End If


        '20170304kevin
        If isNull("RankIDMIN") Then
            If isNull("TitleIDMIN") Then
                Dim sRankID_S = OVBusinessCommon.GetRankID(CompID, RankIDMIN)
                strSQL.AppendLine(" and RM.RankIDMap + T.TitleID>='" + sRankID_S + TitleIDMIN + "'")
            Else
                Dim sRankID_S = OVBusinessCommon.GetRankID(CompID, RankIDMIN)
                strSQL.AppendLine(" and RM.RankIDMap>='" + sRankID_S + "'")
            End If
        End If
        If isNull("RankIDMAX") Then
            If isNull("TitleIDMAX") Then
                Dim sRankID_E = OVBusinessCommon.GetRankID(CompID, RankIDMAX)
                strSQL.AppendLine(" and RM.RankIDMap + T.TitleID<='" + sRankID_E + TitleIDMAX + "'")
            Else
                Dim sRankID_E = OVBusinessCommon.GetRankID(CompID, RankIDMAX)
                strSQL.AppendLine(" and RM.RankIDMap<='" + sRankID_E + "'")
            End If
        End If



        ''未確認
        'If isNull("TitleID") Then
        '    strSQL.AppendLine(" and T.TitleID='" + TitleID + "'")
        '    strSQL.AppendLine(" and T.TitleName='" + TitleName + "'")
        'End If


        If isNull("PositionID") Then
            strSQL.AppendLine(" and PO.PositionID='" + PositionID + "'")
        End If

        'If isNull("WorkType") Then
        '    strSQL.AppendLine(" and W.WorkTypeID='" + WorkType + "'")
        'End If

        If isNull("OrganID") Then
            strSQL.AppendLine(" and ORT.OrganID='" + OrganID + "'")
        End If

        If isNull("DeptID") Then
            strSQL.AppendLine(" and ORT1.OrganID='" + DeptID + "'")
        End If

        If isNull("OrgType") Then
            strSQL.AppendLine(" and ORT.OrgType='" + OrgType + "'")
        End If
        strSQL.AppendLine(" Order by P.EmpID,OVA.OTStartDate,OVA.OTStartTime ")

        Return strSQL.ToString
    End Function

    Private Function getStrSqlForOV2000_OVD_DB() As String
        Dim strSQL As New StringBuilder
        strSQL.AppendLine(" select P.EmpID AS 'OVDEmpID',P.Name AS 'OVDName',OVD.OTCompID AS 'OVDOTCompID',OVD.OTStartDate AS 'OVDOTStartDate',OVD.OTEndDate AS 'OVDOTEndDate',OVD.OTSeq AS 'OVDOTSeq',OVD.OTStartTime AS 'OVDOTStartTime',OVD.OTEndTime AS 'OVDOTEndTime',AT.CodeCName AS 'OVDOTTypeName' ,OVD.OTTxnID AS 'OVDOTTxnID' ,OVD.OTSeqNo AS 'OVDOTSeqNo' ,OVD.OTFromAdvanceTxnId AS 'OVDOTFromAdvanceTxnId' ,OVD.OTReasonMemo AS 'OVDOTReasonMemo',OVD.OTStatus AS 'OVDOTStatus',OVD.OTPayDate AS 'OVDOTPayDate'")
        strSQL.AppendLine(" from OverTimeDeclaration OVD   ")
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Personal  P on OVD.OTCompID= P.CompID and OVD.OTEmpID = P.EmpID   ")
        strSQL.AppendLine(" left join AT_CodeMap AT on OVD.OTTypeID=AT.Code and AT.TabName='OverTime' and AT.FldName='OverTimeType' ")
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].WorkStatus  WS ON P.WorkStatus= WS.WorkCode   ")
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Title  T ON P.CompID=T.CompID and P.RankID=T.RankID and P.TitleID=T.TitleID   ")
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Company C ON P.CompID=C.CompID   ")
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].EmpPosition EP ON P.CompID=EP.CompID AND P.EmpID=EP.EmpID AND EP.PrincipalFlag='1'   ")
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Position PO ON EP.PositionID=PO.PositionID    ")
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].EmpWorkType EWT ON P.CompID=EWT.CompID AND P.EmpID=EWT.EmpID and EWT.PrincipalFlag='1'   ")
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].WorkType W ON EWT.WorkTypeID= W.WorkTypeID  AND P.CompID=W.CompID   ")
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Organization ORT ON  P.OrganID=ORT.OrganID AND P.CompID =ORT.CompID   ")
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Organization ORT1 ON P.DeptID=ORT1.OrganID   ")
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Organization ORT2 ON ORT.OrgType=ORT2.OrganID   ")
        '20170304kevin
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].RankMapping RM on P.RankID=RM.RankID  AND RM.CompID=OVD.OTCompID")
        'strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].RankMapping RM on P.RankID=RM.RankID")
        'strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].RankMapping TRM on T.RankID=RM.RankID")
        strSQL.AppendLine(" Where 1=1  ")
        strSQL.AppendLine(" and OVD.OTCompID='" + UserProfile.SelectCompRoleID + "'  ")

        If isNull("OTSalaryOrAdjust") Then
            strSQL.AppendLine(" and OVD.SalaryOrAdjust='" + OTSalaryOrAdjust + "'")
        End If

        If isNull("OTEmpName") Then
            strSQL.AppendLine(" and P.Name='" + OTEmpName + "'")
        End If

        If isNull("OvertimeDateB") Then
            strSQL.AppendLine(" and OVD.OTStartDate>='" + OvertimeDateB + "'")
        End If
        If isNull("OvertimeDateE") Then
            strSQL.AppendLine(" and OVD.OTEndDate<='" + OvertimeDateE + "'")
        End If

        If isNull("OTStatus1") Then
            strSQL.AppendLine("and OVD.OTStatus='" + OTStatus1 + "'")
        End If


        If isNull("OTFormNO") Then
            strSQL.AppendLine(" and OVD.OTFormNO='" + OTFormNO + "'")
        End If
        If isNull("OTEmpID") Then
            strSQL.AppendLine(" and P.EmpID='" + OTEmpID + "'")
        End If

        If isNull("WorkStatus") Then
            strSQL.AppendLine(" and P.WorkStatus='" + WorkStatus + "'")
        End If





        'If isNull("RankIDMIN") Then
        '    strSQL.AppendLine(" and T.RankID>='" + RankIDMIN + "'")
        'End If
        'If isNull("RankIDMAX") Then
        '    strSQL.AppendLine(" and T.RankID<='" + RankIDMAX + "'")
        'End If
        ''未確認
        'If isNull("TitleID") Then
        '    strSQL.AppendLine(" and T.TitleID='" + TitleID + "'")
        '    strSQL.AppendLine(" and T.TitleName='" + TitleName + "'")
        'End If

        '20170304kevin
        If isNull("RankIDMIN") Then
            If isNull("TitleIDMIN") Then
                Dim sRankID_S = OVBusinessCommon.GetRankID(CompID, RankIDMIN)
                strSQL.AppendLine(" and RM.RankIDMap + T.TitleID>='" + sRankID_S + TitleIDMIN + "'")
            Else
                Dim sRankID_S = OVBusinessCommon.GetRankID(CompID, RankIDMIN)
                strSQL.AppendLine(" and RM.RankIDMap>='" + sRankID_S + "'")
            End If
        End If
        If isNull("RankIDMAX") Then
            If isNull("TitleIDMAX") Then
                Dim sRankID_E = OVBusinessCommon.GetRankID(CompID, RankIDMAX)
                strSQL.AppendLine(" and RM.RankIDMap + T.TitleID<='" + sRankID_E + TitleIDMAX + "'")
            Else
                Dim sRankID_E = OVBusinessCommon.GetRankID(CompID, RankIDMAX)
                strSQL.AppendLine(" and RM.RankIDMap<='" + sRankID_E + "'")
            End If
        End If





        If isNull("PositionID") Then
            strSQL.AppendLine(" and PO.PositionID='" + PositionID + "'")
        End If



        If isNull("OrganID") Then
            strSQL.AppendLine(" and ORT.OrganID='" + OrganID + "'")
        End If

        If isNull("DeptID") Then
            strSQL.AppendLine(" and ORT1.OrganID='" + DeptID + "'")
        End If

        If isNull("OrgType") Then
            strSQL.AppendLine(" and ORT.OrgType='" + OrgType + "'")
        End If

        If isNull("OTPayDate") Then
            strSQL.AppendLine(" and OVD.OTPayDate='" + OTPayDate + "'")
        End If

        'If isNull("OTSalaryPaid") Then
        '    strSQL.AppendLine(" and OVD.OTSalaryPaid='" + OTSalaryPaid + "'")
        'End If
        strSQL.AppendLine(" Order by P.EmpID,OVD.OTStartDate,OVD.OTStartTime ")


        Return strSQL.ToString
    End Function


    Private Function getStrSqlForOV2000Statistics_OVA_DB() As String
        Dim strSQL As New StringBuilder
        strSQL.AppendLine(" select P.EmpID AS 'OVAEmpID',P.Name AS 'OVAName',OVA.OTCompID AS 'OVAOTCompID',OVA.OTStatus AS 'OVAOTStatus',OVA.OTTotalTime AS 'OVAOTTotalTime',OVA.MealFlag as 'MealFlag',OVA.MealTime as 'MealTime',OVA.OTSeqNo AS 'OVAOTSeqNo',OVA.OTTxnID AS 'OVAOTTxnID'")
        strSQL.AppendLine(" from OverTimeAdvance OVA   ")
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Personal  P on OVA.OTCompID= P.CompID and OVA.OTEmpID = P.EmpID   ")
        'strSQL.AppendLine(" left join OverTimeType OVT ON OVA.OTTypeID= OVT.OTTypeId  ")
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].WorkStatus  WS ON P.WorkStatus= WS.WorkCode   ")
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Title  T ON P.CompID=T.CompID and P.RankID=T.RankID and P.TitleID=T.TitleID   ")
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Company C ON P.CompID=C.CompID   ")
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].EmpPosition EP ON P.CompID=EP.CompID AND P.EmpID=EP.EmpID AND EP.PrincipalFlag='1'   ")
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Position PO ON EP.PositionID=PO.PositionID    ")
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].EmpWorkType EWT ON P.CompID=EWT.CompID AND P.EmpID=EWT.EmpID and EWT.PrincipalFlag='1'   ")
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].WorkType W ON EWT.WorkTypeID= W.WorkTypeID  AND P.CompID=W.CompID   ")
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Organization ORT ON  P.OrganID=ORT.OrganID AND P.CompID =ORT.CompID   ")
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Organization ORT1 ON P.DeptID=ORT1.OrganID   ")
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Organization ORT2 ON ORT.OrgType=ORT2.OrganID   ")
        '20170304
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].RankMapping RM on P.RankID=RM.RankID  AND RM.CompID=OVA.OTCompID")
        strSQL.AppendLine(" Where 1=1  ")
        strSQL.AppendLine(" and OVA.OTCompID='" + UserProfile.SelectCompRoleID + "'")

        If isNull("OTSalaryOrAdjust") Then
            strSQL.AppendLine(" and OVA.SalaryOrAdjust='" + OTSalaryOrAdjust + "'")
        End If

        If isNull("OTEmpName") Then
            strSQL.AppendLine(" and P.Name='" + OTEmpName + "'")
        End If

        If isNull("OvertimeDateB") Then
            strSQL.AppendLine(" and OVA.OTStartDate>='" + OvertimeDateB + "'")
        End If
        If isNull("OvertimeDateE") Then
            strSQL.AppendLine(" and OVA.OTEndDate<='" + OvertimeDateE + "'")
        End If

        If isNull("OTStatus") Then
            strSQL.AppendLine("and OVA.OTStatus='" + OTStatus + "'")
        End If


        If isNull("OTFormNO") Then
            strSQL.AppendLine(" and OVA.OTFormNO='" + OTFormNO + "'")
        End If
        If isNull("OTEmpID") Then
            strSQL.AppendLine(" and P.EmpID='" + OTEmpID + "'")
        End If

        If isNull("WorkStatus") Then
            strSQL.AppendLine(" and P.WorkStatus='" + WorkStatus + "'")
        End If

        'If isNull("RankIDMIN") Then
        '    strSQL.AppendLine(" and T.RankID>='" + RankIDMIN + "'")
        'End If
        'If isNull("RankIDMAX") Then
        '    strSQL.AppendLine(" and T.RankID<='" + RankIDMAX + "'")
        'End If
        ''未確認
        'If isNull("TitleID") Then
        '    strSQL.AppendLine(" and T.TitleID='" + TitleID + "'")
        '    strSQL.AppendLine(" and T.TitleName='" + TitleName + "'")
        'End If

        '20170304kevin
        If isNull("RankIDMIN") Then
            If isNull("TitleIDMIN") Then
                Dim sRankID_S = OVBusinessCommon.GetRankID(CompID, RankIDMIN)
                strSQL.AppendLine(" and RM.RankIDMap + T.TitleID>='" + sRankID_S + TitleIDMIN + "'")
            Else
                Dim sRankID_S = OVBusinessCommon.GetRankID(CompID, RankIDMIN)
                strSQL.AppendLine(" and RM.RankIDMap>='" + sRankID_S + "'")
            End If
        End If
        If isNull("RankIDMAX") Then
            If isNull("TitleIDMAX") Then
                Dim sRankID_E = OVBusinessCommon.GetRankID(CompID, RankIDMAX)
                strSQL.AppendLine(" and RM.RankIDMap + T.TitleID<='" + sRankID_E + TitleIDMAX + "'")
            Else
                Dim sRankID_E = OVBusinessCommon.GetRankID(CompID, RankIDMAX)
                strSQL.AppendLine(" and RM.RankIDMap<='" + sRankID_E + "'")
            End If
        End If


        If isNull("PositionID") Then
            strSQL.AppendLine(" and PO.PositionID='" + PositionID + "'")
        End If

        'If isNull("WorkType") Then
        '    strSQL.AppendLine(" and W.WorkTypeID='" + WorkType + "'")
        'End If

        If isNull("OrganID") Then
            strSQL.AppendLine(" and ORT.OrganID='" + OrganID + "'")
        End If

        If isNull("DeptID") Then
            strSQL.AppendLine(" and ORT1.OrganID='" + DeptID + "'")
        End If

        If isNull("OrgType") Then
            strSQL.AppendLine(" and ORT.OrgType='" + OrgType + "'")
        End If
        strSQL.AppendLine(" Order by P.EmpID ")

        Return strSQL.ToString
    End Function

    Private Function getStrSqlForOV2000Statistics_OVD_DB() As String
        Dim strSQL As New StringBuilder
        strSQL.AppendLine(" select P.EmpID AS 'OVDEmpID',P.Name AS 'OVDName',OVD.OTCompID AS 'OVDOTCompID',OVD.OTStatus AS 'OVDOTStatus',OVD.OTTotalTime AS 'OVDOTTotalTime',OVD.MealFlag as 'MealFlag',OVD.MealTime as 'MealTime',OVD.OTSeqNo AS 'OVDOTSeqNo',OVD.OTTxnID AS 'OVDOTTxnID'")
        strSQL.AppendLine(" from OverTimeDeclaration OVD   ")
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Personal  P on OVD.OTCompID= P.CompID and OVD.OTEmpID = P.EmpID   ")
        'strSQL.AppendLine(" left join OverTimeType OVT ON OVD.OTTypeID= OVT.OTTypeId  ")
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].WorkStatus  WS ON P.WorkStatus= WS.WorkCode   ")
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Title  T ON P.CompID=T.CompID and P.RankID=T.RankID and P.TitleID=T.TitleID   ")
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Company C ON P.CompID=C.CompID   ")
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].EmpPosition EP ON P.CompID=EP.CompID AND P.EmpID=EP.EmpID AND EP.PrincipalFlag='1'   ")
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Position PO ON EP.PositionID=PO.PositionID    ")
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].EmpWorkType EWT ON P.CompID=EWT.CompID AND P.EmpID=EWT.EmpID and EWT.PrincipalFlag='1'   ")
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].WorkType W ON EWT.WorkTypeID= W.WorkTypeID  AND P.CompID=W.CompID   ")
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Organization ORT ON  P.OrganID=ORT.OrganID AND P.CompID =ORT.CompID   ")
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Organization ORT1 ON P.DeptID=ORT1.OrganID   ")
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Organization ORT2 ON ORT.OrgType=ORT2.OrganID   ")
        '20170304kevin
        strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].RankMapping RM on P.RankID=RM.RankID  AND RM.CompID=OVD.OTCompID")
        strSQL.AppendLine(" Where 1=1  ")
        strSQL.AppendLine(" And OVD.OTCompID='" + UserProfile.SelectCompRoleID + "'")

        If isNull("OTSalaryOrAdjust") Then
            strSQL.AppendLine(" and OVD.SalaryOrAdjust='" + OTSalaryOrAdjust + "'")
        End If

        If isNull("OTEmpName") Then
            strSQL.AppendLine(" and P.Name='" + OTEmpName + "'")
        End If

        If isNull("OvertimeDateB") Then
            strSQL.AppendLine(" and OVD.OTStartDate>='" + OvertimeDateB + "'")
        End If
        If isNull("OvertimeDateE") Then
            strSQL.AppendLine(" and OVD.OTEndDate<='" + OvertimeDateE + "'")
        End If

        If isNull("OTStatus1") Then
            strSQL.AppendLine("and OVD.OTStatus='" + OTStatus1 + "'")
        End If


        If isNull("OTFormNO") Then
            strSQL.AppendLine(" and OVD.OTFormNO='" + OTFormNO + "'")
        End If
        If isNull("OTEmpID") Then
            strSQL.AppendLine(" and P.EmpID='" + OTEmpID + "'")
        End If

        If isNull("WorkStatus") Then
            strSQL.AppendLine(" and P.WorkStatus='" + WorkStatus + "'")
        End If

        'If isNull("RankIDMIN") Then
        '    strSQL.AppendLine(" and T.RankID>='" + RankIDMIN + "'")
        'End If
        'If isNull("RankIDMAX") Then
        '    strSQL.AppendLine(" and T.RankID<='" + RankIDMAX + "'")
        'End If
        ''未確認
        'If isNull("TitleID") Then
        '    strSQL.AppendLine(" and T.TitleID='" + TitleID + "'")
        '    strSQL.AppendLine(" and T.TitleName='" + TitleName + "'")
        'End If
        '20170304kevin
        If isNull("RankIDMIN") Then
            If isNull("TitleIDMIN") Then
                Dim sRankID_S = OVBusinessCommon.GetRankID(CompID, RankIDMIN)
                strSQL.AppendLine(" and RM.RankIDMap + T.TitleID>='" + sRankID_S + TitleIDMIN + "'")
            Else
                Dim sRankID_S = OVBusinessCommon.GetRankID(CompID, RankIDMIN)
                strSQL.AppendLine(" and RM.RankIDMap>='" + sRankID_S + "'")
            End If
        End If
        If isNull("RankIDMAX") Then
            If isNull("TitleIDMAX") Then
                Dim sRankID_E = OVBusinessCommon.GetRankID(CompID, RankIDMAX)
                strSQL.AppendLine(" and RM.RankIDMap + T.TitleID<='" + sRankID_E + TitleIDMAX + "'")
            Else
                Dim sRankID_E = OVBusinessCommon.GetRankID(CompID, RankIDMAX)
                strSQL.AppendLine(" and RM.RankIDMap<='" + sRankID_E + "'")
            End If
        End If







        If isNull("PositionID") Then
            strSQL.AppendLine(" and PO.PositionID='" + PositionID + "'")
        End If



        If isNull("OrganID") Then
            strSQL.AppendLine(" and ORT.OrganID='" + OrganID + "'")
        End If

        If isNull("DeptID") Then
            strSQL.AppendLine(" and ORT1.OrganID='" + DeptID + "'")
        End If

        If isNull("OrgType") Then
            strSQL.AppendLine(" and ORT.OrgType='" + OrgType + "'")
        End If

        strSQL.AppendLine(" Order by P.EmpID ")


        Return strSQL.ToString
    End Function

#End Region

#Region "取得DataTable"
    Public Function IsEmpIDRun(ByVal empID As String, ByVal list As ArrayList) As Boolean
        For i = 0 To list.Count - 1
            If list.Item(i).Equals(empID) Then
                Return True
            End If
        Next
        Return False
    End Function

    ''' <summary>
    ''' 取得統計時數
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' 邏輯:
    ''' 先得查詢的到使用者所輸入條件的DataTable 然後合單 
    ''' 事先申請:OVADB
    ''' 事後申報:OVDDB
    ''' 
    ''' 創出所需要的欄位的虛擬Table :statisticsTable
    ''' 將OVADB,OVDDB 的員工編號,公司,名稱放入statisticsTable 並且不重複(用EmpID來判別 因為查詢出來的資料公司都同一家所以不會重複)
    ''' 將OVADB,OVDDB 的員工編號,公司,名稱放入statisticsTable 並且與事先申請不重複(用EmpID來判別 因為查詢出來的資料公司都同一家所以不會重複)
    ''' 跑巢狀迴圈 外:statisticsTable ROWS的數量
    '''            內:事先 OVADB 依欄位的需求去做加總 EX:如果狀態是2 送簽 那就把OVA裡的事先並且是送簽的扣掉用餐時數並加總
    '''            內:事後 OVDDB 依欄位的需求去做加總
    ''' 
    ''' </remarks>
    Public Function getOV2000_Statistics_DB() As DataTable
        Dim OVADB As DataTable = SingleDataTableForStatistics(Bsp.DB.ExecuteDataSet(CommandType.Text, getStrSqlForOV2000Statistics_OVA_DB(), "AattendantDB").Tables(0), "bef")
        Dim OVDDB As DataTable = SingleDataTableForStatistics(Bsp.DB.ExecuteDataSet(CommandType.Text, getStrSqlForOV2000Statistics_OVD_DB(), "AattendantDB").Tables(0), "after")


        Dim arrayEmpIDList As ArrayList = New ArrayList()
        Dim statisticsTable As DataTable = getTableForStatistics()

        '放入OVA的EMPID
        For i = 0 To OVADB.Rows.Count - 1

            'IsEmpIDRun 排除掉有在虛擬Table 的EmpID
            If Not IsEmpIDRun(OVADB.Rows(i).Item("OVAEmpID"), arrayEmpIDList) Then
                Dim dataRows As DataRow
                dataRows = statisticsTable.NewRow
                dataRows("OTCompID") = OVADB.Rows(i).Item("OVAOTCompID")
                dataRows("EmpID") = OVADB.Rows(i).Item("OVAEmpID")
                dataRows("Name") = OVADB.Rows(i).Item("OVAName")

                dataRows("OVAStatusTimeFor2") = 0
                dataRows("OVAStatusTimeFor3") = 0
                dataRows("OVAStatusTimeFor4") = 0
                dataRows("OVDStatusTimeFor2") = 0
                dataRows("OVDStatusTimeFor3") = 0
                dataRows("OVDStatusTimeFor4") = 0
                arrayEmpIDList.Add(OVADB.Rows(i).Item("OVAEmpID"))
                statisticsTable.Rows.Add(dataRows)
            End If
        Next

        '放入OVD的EMPID
        For i = 0 To OVDDB.Rows.Count - 1
            'IsEmpIDRun 排除掉有在裡面的EmpID
            If Not IsEmpIDRun(OVDDB.Rows(i).Item("OVDEmpID"), arrayEmpIDList) Then
                Dim dataRows As DataRow
                dataRows = statisticsTable.NewRow
                dataRows("OTCompID") = OVDDB.Rows(i).Item("OVDOTCompID")
                dataRows("EmpID") = OVDDB.Rows(i).Item("OVDEmpID")
                dataRows("Name") = OVDDB.Rows(i).Item("OVDName")
                dataRows("OVAStatusTimeFor2") = 0
                dataRows("OVAStatusTimeFor3") = 0
                dataRows("OVAStatusTimeFor4") = 0
                dataRows("OVDStatusTimeFor2") = 0
                dataRows("OVDStatusTimeFor3") = 0
                dataRows("OVDStatusTimeFor4") = 0
                arrayEmpIDList.Add(OVDDB.Rows(i).Item("OVDEmpID"))
                statisticsTable.Rows.Add(dataRows)
            End If
        Next


        '計算時間
        For i = 0 To statisticsTable.Rows.Count - 1
            Dim statisticsRows As DataRow = statisticsTable.Rows(i)
            '計算ova 相同EmpID 的時間 
            For j = 0 To OVADB.Rows.Count - 1
                Dim OVADBRows As DataRow = OVADB.Rows(j)
                If statisticsRows.Item("EmpID").Equals(OVADBRows.Item("OVAEmpID")) And statisticsRows.Item("OTCompID").Equals(OVADBRows.Item("OVAOTCompID")) Then
                    '2: 送簽
                    If OVADBRows.Item("OVAOTStatus").Equals("2") Then
                        Dim MealTime As String = OVADBRows.Item("MealTime")
                        Dim MealFlag As String = OVADBRows.Item("MealFlag")
                        If "1".Equals(MealFlag) Then
                            If IsNumeric(MealTime) Then
                                statisticsRows("OVAStatusTimeFor2") = Convert.ToString((Convert.ToDouble(statisticsRows("OVAStatusTimeFor2")) + CDbl(FormatNumber((Convert.ToDouble(OVADBRows.Item("OVAOTTotalTime")) - Convert.ToDouble(MealTime)) / 60, 1))))
                            End If
                        Else
                            statisticsRows("OVAStatusTimeFor2") = Convert.ToString((Convert.ToDouble(statisticsRows("OVAStatusTimeFor2")) + CDbl(FormatNumber((Convert.ToDouble(OVADBRows.Item("OVAOTTotalTime"))) / 60, 1))))
                        End If
                    End If
                    '3: 核准
                    If OVADBRows.Item("OVAOTStatus").Equals("3") Then
                        Dim MealTime As String = OVADBRows.Item("MealTime")
                        Dim MealFlag As String = OVADBRows.Item("MealFlag")
                        If "1".Equals(MealFlag) Then
                            If IsNumeric(MealTime) Then
                                statisticsRows("OVAStatusTimeFor3") = Convert.ToString((Convert.ToDouble(statisticsRows("OVAStatusTimeFor3")) + CDbl(FormatNumber((Convert.ToDouble(OVADBRows.Item("OVAOTTotalTime")) - Convert.ToDouble(MealTime)) / 60, 1))))
                            End If
                        Else
                            statisticsRows("OVAStatusTimeFor3") = Convert.ToString((Convert.ToDouble(statisticsRows("OVAStatusTimeFor3")) + CDbl(FormatNumber((Convert.ToDouble(OVADBRows.Item("OVAOTTotalTime"))) / 60, 1))))
                        End If
                    End If

                    '4: 駁回
                    If OVADBRows.Item("OVAOTStatus").Equals("4") Then
                        Dim MealTime As String = OVADBRows.Item("MealTime")
                        Dim MealFlag As String = OVADBRows.Item("MealFlag")
                        If "1".Equals(MealFlag) Then
                            If IsNumeric(MealTime) Then
                                statisticsRows("OVAStatusTimeFor4") = Convert.ToString((Convert.ToDouble(statisticsRows("OVAStatusTimeFor4")) + CDbl(FormatNumber((Convert.ToDouble(OVADBRows.Item("OVAOTTotalTime")) - Convert.ToDouble(MealTime)) / 60, 1))))
                            End If
                        Else
                            statisticsRows("OVAStatusTimeFor4") = Convert.ToString((Convert.ToDouble(statisticsRows("OVAStatusTimeFor4")) + CDbl(FormatNumber(Convert.ToDouble(OVADBRows.Item("OVAOTTotalTime")) / 60, 1))))
                        End If
                        '("OVAStatusTimeFor4") = Convert.ToString((Convert.ToDouble(statisticsRows("OVAStatusTimeFor4")) + Math.Round(Convert.ToDouble(OVADBRows.Item("OVAOTTotalTime")) / 60, 1)))
                    End If
                End If
            Next

            '計算ovd 相同EmpID 的時間 
            For j = 0 To OVDDB.Rows.Count - 1
                Dim OVDDBRows As DataRow = OVDDB.Rows(j)
                If statisticsRows.Item("EmpID").Equals(OVDDBRows.Item("OVDEmpID")) And statisticsRows.Item("OTCompID").Equals(OVDDBRows.Item("OVDOTCompID")) Then
                    '2: 送簽
                    If OVDDBRows.Item("OVDOTStatus").Equals("2") Then
                        Dim MealTime As String = OVDDBRows.Item("MealTime")
                        Dim MealFlag As String = OVDDBRows.Item("MealFlag")
                        If "1".Equals(MealFlag) Then
                            If IsNumeric(MealTime) Then
                                statisticsRows("OVDStatusTimeFor2") = Convert.ToString((Convert.ToDouble(statisticsRows("OVDStatusTimeFor2")) + CDbl(FormatNumber((Convert.ToDouble(OVDDBRows.Item("OVDOTTotalTime")) - Convert.ToDouble(MealTime)) / 60, 1))))
                            End If
                        Else
                            statisticsRows("OVDStatusTimeFor2") = Convert.ToString((Convert.ToDouble(statisticsRows("OVDStatusTimeFor2")) + CDbl(FormatNumber(Convert.ToDouble(OVDDBRows.Item("OVDOTTotalTime")) / 60, 1))))
                        End If

                        'statisticsRows("OVDStatusTimeFor2") = Convert.ToString((Convert.ToDouble(statisticsRows("OVDStatusTimeFor2")) + Math.Round(Convert.ToDouble(OVDDBRows.Item("OVDOTTotalTime")) / 60, 2)))
                    End If
                    '3: 核准
                    If OVDDBRows.Item("OVDOTStatus").Equals("3") Then
                        Dim MealTime As String = OVDDBRows.Item("MealTime")
                        Dim MealFlag As String = OVDDBRows.Item("MealFlag")
                        If "1".Equals(MealFlag) Then
                            If IsNumeric(MealTime) Then
                                statisticsRows("OVDStatusTimeFor3") = Convert.ToString((Convert.ToDouble(statisticsRows("OVDStatusTimeFor3")) + CDbl(FormatNumber((Convert.ToDouble(OVDDBRows.Item("OVDOTTotalTime")) - Convert.ToDouble(MealTime)) / 60, 1))))
                            End If
                        Else
                            statisticsRows("OVDStatusTimeFor3") = Convert.ToString((Convert.ToDouble(statisticsRows("OVDStatusTimeFor3")) + CDbl(FormatNumber(Convert.ToDouble(OVDDBRows.Item("OVDOTTotalTime")) / 60, 1))))
                        End If




                        ' statisticsRows("OVDStatusTimeFor3") = Convert.ToString((Convert.ToDouble(statisticsRows("OVDStatusTimeFor3")) + Math.Round(Convert.ToDouble(OVDDBRows.Item("OVDOTTotalTime")) / 60, 1)))
                    End If

                    '4: 駁回
                    If OVDDBRows.Item("OVDOTStatus").Equals("4") Then

                        Dim MealTime As String = OVDDBRows.Item("MealTime")
                        Dim MealFlag As String = OVDDBRows.Item("MealFlag")
                        If "1".Equals(MealFlag) Then
                            If IsNumeric(MealTime) Then
                                statisticsRows("OVDStatusTimeFor4") = Convert.ToString((Convert.ToDouble(statisticsRows("OVDStatusTimeFor4")) + CDbl(FormatNumber((Convert.ToDouble(OVDDBRows.Item("OVDOTTotalTime")) - Convert.ToDouble(MealTime)) / 60, 1))))
                            End If
                        Else
                            statisticsRows("OVDStatusTimeFor4") = Convert.ToString((Convert.ToDouble(statisticsRows("OVDStatusTimeFor4")) + CDbl(FormatNumber(Convert.ToDouble(OVDDBRows.Item("OVDOTTotalTime")) / 60, 1))))
                        End If

                        'statisticsRows("OVDStatusTimeFor4") = Convert.ToString((Convert.ToDouble(statisticsRows("OVDStatusTimeFor4")) + Math.Round(Convert.ToDouble(OVDDBRows.Item("OVDOTTotalTime")) / 60, 1)))
                    End If
                End If
            Next
        Next

        Return statisticsTable
    End Function


    ''' <summary>
    ''' 我的邏輯 
    ''' OVADBTemp 得使用者條件的事先申請
    ''' OVDDBTemp 得使用者條件的事後申報
    ''' OVADB 合單後的
    ''' OVDDB 合單後的
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getOV2000_Detial_DB() As DataTable

        Dim OVADBTemp As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, getStrSqlForOV2000_OVA_DB(), "AattendantDB").Tables(0)
        Dim OVDDBTemp As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, getStrSqlForOV2000_OVD_DB(), "AattendantDB").Tables(0)

        Dim OVADB As DataTable = SingleDataTable(OVADBTemp, "bef")
        Dim OVDDB As DataTable = SingleDataTable(OVDDBTemp, "after")
        Dim detialTable As DataTable = getTableForDetial()

        Dim reMoveRowForOVADBList As ArrayList = New ArrayList()

        Dim reMoveRowForOVDDBList As ArrayList = New ArrayList()




        '移除已結合的OVA 為防DB髒資料 導致錯誤 所以先查資料然後再移除
        For i = 0 To reMoveRowForOVADBList.Count - 1
            Dim OVAPK As String = reMoveRowForOVADBList.Item(i)
            Dim OVAOTSeqNo As String = OVAPK.Split("~")(0)
            Dim OVAOTTxnID As String = OVAPK.Split("~")(1)
            Dim reMoveRow As DataRow() = OVADB.[Select]("OVAOTSeqNo='" + OVAOTSeqNo + "' and OVAOTTxnID='" + OVAOTTxnID + "' ")

            For j = 0 To reMoveRow.Count - 1
                OVADB.Rows.Remove(reMoveRow(j))
            Next

            '20170306kevin
            'OVADB.Rows.Remove(reMoveRowForOVADBList.Item(i))
        Next
        '移除已結合的OVD 為防DB髒資料 導致錯誤  所以先查資料然後再移除
        For i = 0 To reMoveRowForOVDDBList.Count - 1
            Dim OVDPK As String = reMoveRowForOVDDBList.Item(i)
            Dim OVDOTSeqNo As String = OVDPK.Split("~")(0)
            Dim OVDOTTxnID As String = OVDPK.Split("~")(1)
            Dim reMoveRow As DataRow() = OVDDB.[Select]("OVDOTSeqNo='" + OVDOTSeqNo + "' and OVDOTTxnID='" + OVDOTTxnID + "' ")

            For j = 0 To reMoveRow.Count - 1
                OVDDB.Rows.Remove(reMoveRow(j))
            Next

            '20170306kevin
            'OVDDB.Rows.Remove(reMoveRowForOVDDBList.Item(i))
        Next


        '加入剩下的OVA
        For j = 0 To OVADB.Rows.Count - 1
            '20170706 - Jason 事先申請核准後轉至事後申報暫存這類單都需排掉，避免重複紀錄
            '尋找事後申報的temp table是否有OVDOTFromAdvanceTxnId等於OVAOTTxnID的row，有則排除
            Dim foundRow As DataRow = OVDDB.Select("OVDOTFromAdvanceTxnId = " + Bsp.Utility.Quote(OVADB.Rows(j).Item("OVAOTTxnID"))).FirstOrDefault()
            If Not foundRow Is Nothing Then
                Continue For
            End If

            Dim dataRows As DataRow
            dataRows = detialTable.NewRow
            '20170711 - Jason 新增GroupType來排序, 1=申請,2=申報,3=合單
            dataRows("GroupType") = "1"
            dataRows("OTCompID") = OVADB.Rows(j).Item("OVAOTCompID")
            dataRows("EmpID") = OVADB.Rows(j).Item("OVAEmpID")
            dataRows("Name") = OVADB.Rows(j).Item("OVAName")
            dataRows("OVADate") = OVADB.Rows(j).Item("OVAOTStartDate") + "~" + OVADB.Rows(j).Item("OVAOTEndDate")
            dataRows("OVATime") = OVADB.Rows(j).Item("OVAOTStartTime").ToString().Substring(0, 2) + ":" + OVADB.Rows(j).Item("OVAOTStartTime").ToString().Substring(2, 2) + "~" + OVADB.Rows(j).Item("OVAOTEndTime").ToString().Substring(0, 2) + ":" + OVADB.Rows(j).Item("OVAOTEndTime").ToString().Substring(2, 2)
            dataRows("OVAOTTypeName") = OVADB.Rows(j).Item("OVAOTTypeName")
            dataRows("OVAOTReasonMemo") = OVADB.Rows(j).Item("OVAOTReasonMemo")
            dataRows("OVAOTStatus") = OVADB.Rows(j).Item("OVAOTStatus")
            dataRows("OVAOTTxnID") = OVADB.Rows(j).Item("OVAOTTxnID")
            detialTable.Rows.Add(dataRows)
        Next

        '加入剩下的OVD
        For i = 0 To OVDDB.Rows.Count - 1
            '20170706 - Jason 事先申請核准後轉至事後申報暫存這類單都需排掉，避免重複紀錄
            If Not String.IsNullOrEmpty(OVDDB.Rows(i).Item("OVDOTFromAdvanceTxnId")) Then
                '尋找事先申請是否有相符的資料，有則排除
                Dim foundRow As DataRow = OVADB.Select("OVAOTTxnID = " + Bsp.Utility.Quote(OVDDB.Rows(i).Item("OVDOTFromAdvanceTxnId"))).FirstOrDefault()
                If Not foundRow Is Nothing Then
                    Continue For
                End If
            End If

            Dim dataRows As DataRow
            dataRows = detialTable.NewRow
            '20170711 - Jason 新增GroupType來排序, 1=申請,2=申報,3=合單
            dataRows("GroupType") = "2"
            dataRows("OTCompID") = OVDDB.Rows(i).Item("OVDOTCompID")
            dataRows("EmpID") = OVDDB.Rows(i).Item("OVDEmpID")
            dataRows("Name") = OVDDB.Rows(i).Item("OVDName")
            dataRows("OVDDate") = OVDDB.Rows(i).Item("OVDOTStartDate") + "~" + OVDDB.Rows(i).Item("OVDOTEndDate")
            dataRows("OVDTime") = OVDDB.Rows(i).Item("OVDOTStartTime").ToString().Substring(0, 2) + ":" + OVDDB.Rows(i).Item("OVDOTStartTime").ToString().Substring(2, 2) + "~" + OVDDB.Rows(i).Item("OVDOTEndTime").ToString().Substring(0, 2) + ":" + OVDDB.Rows(i).Item("OVDOTEndTime").ToString().Substring(2, 2)
            dataRows("OVDOTTypeName") = OVDDB.Rows(i).Item("OVDOTTypeName")
            dataRows("OVDOTReasonMemo") = OVDDB.Rows(i).Item("OVDOTReasonMemo")
            dataRows("OVDOTStatus") = OVDDB.Rows(i).Item("OVDOTStatus")
            dataRows("OVDOTTxnID") = OVDDB.Rows(i).Item("OVDOTTxnID")
            dataRows("OVDOTPayDate") = OVDDB.Rows(i).Item("OVDOTPayDate")
            detialTable.Rows.Add(dataRows)
        Next

        Dim arrayForRemove As ArrayList = New ArrayList()
        If isNull("OTPayDate") Then
            For i = 0 To detialTable.Rows.Count - 1
                Dim OVDOTPayDate = detialTable.Rows(i).Item("OVDOTPayDate")
                If ((Not OVDOTPayDate.Equals(Me.OTPayDate))) Then
                    arrayForRemove.Add(detialTable.Rows(i))
                End If
            Next
        End If
        For i = 0 To arrayForRemove.Count - 1
            detialTable.Rows.Remove(arrayForRemove.Item(i))
        Next

        arrayForRemove = New ArrayList()
        If isNull("OTStatus") And isNull("OTStatus1") Then
            For i = 0 To detialTable.Rows.Count - 1
                Dim OVAOTStatus = detialTable.Rows(i).Item("OVAOTStatus")
                Dim OVDOTStatus = detialTable.Rows(i).Item("OVDOTStatus")

                If ((Not OVAOTStatus.Equals(Me.OTStatus)) Or (Not OVDOTStatus.Equals(Me.OTStatus1))) Then
                    arrayForRemove.Add(detialTable.Rows(i))
                End If
            Next
        ElseIf isNull("OTStatus") And Not isNull("OTStatus1") Then
            For i = 0 To detialTable.Rows.Count - 1
                Dim OVAOTStatus = detialTable.Rows(i).Item("OVAOTStatus")

                If (Not OVAOTStatus.Equals(Me.OTStatus)) Then
                    arrayForRemove.Add(detialTable.Rows(i))
                End If
            Next
        ElseIf Not isNull("OTStatus") And isNull("OTStatus1") Then
            For i = 0 To detialTable.Rows.Count - 1

                Dim OVDOTStatus = detialTable.Rows(i).Item("OVDOTStatus")

                If (Not OVDOTStatus.Equals(Me.OTStatus1)) Then
                    arrayForRemove.Add(detialTable.Rows(i))
                End If
            Next
        End If

        For i = 0 To arrayForRemove.Count - 1
            detialTable.Rows.Remove(arrayForRemove.Item(i))
        Next

        '此邏輯為放入有事先申請也有事後申報的資料 並且放入把放入的資料分別記錄起來
        For i = 0 To OVDDB.Rows.Count - 1
            For j = 0 To OVADB.Rows.Count - 1
                If OVDDB.Rows(i).Item("OVDOTFromAdvanceTxnId").Equals(OVADB.Rows(j).Item("OVAOTTxnID")) Then
                    ' OTCompID, EmpID, Name, OVADate, OVATime, OVAOTTypeName, OVAOTReasonMemo, OVAOTStatus, OVDDate, OVDTime, OVDOTTypeName, OVDOTReasonMemo, OVDOTStatus
                    Dim dataRows As DataRow
                    dataRows = detialTable.NewRow
                    '20170711 - Jason 新增GroupType來排序, 1=申請,2=申報,3=合單
                    dataRows("GroupType") = "3"
                    dataRows("OTCompID") = OVADB.Rows(j).Item("OVAOTCompID")
                    dataRows("EmpID") = OVADB.Rows(j).Item("OVAEmpID")
                    dataRows("Name") = OVADB.Rows(j).Item("OVAName")
                    dataRows("OVADate") = OVADB.Rows(j).Item("OVAOTStartDate") + "~" + OVADB.Rows(j).Item("OVAOTEndDate")
                    dataRows("OVATime") = OVADB.Rows(j).Item("OVAOTStartTime").ToString().Substring(0, 2) + ":" + OVADB.Rows(j).Item("OVAOTStartTime").ToString().Substring(2, 2) + "~" + OVADB.Rows(j).Item("OVAOTEndTime").ToString().Substring(0, 2) + ":" + OVADB.Rows(j).Item("OVAOTEndTime").ToString().Substring(2, 2)
                    dataRows("OVAOTReasonMemo") = OVADB.Rows(j).Item("OVAOTReasonMemo")
                    dataRows("OVAOTTypeName") = OVADB.Rows(j).Item("OVAOTTypeName")
                    dataRows("OVAOTStatus") = OVADB.Rows(j).Item("OVAOTStatus")
                    dataRows("OVAOTTxnID") = OVADB.Rows(j).Item("OVAOTTxnID")

                    dataRows("OTCompID") = OVDDB.Rows(i).Item("OVDOTCompID")
                    dataRows("EmpID") = OVDDB.Rows(i).Item("OVDEmpID")
                    dataRows("Name") = OVDDB.Rows(i).Item("OVDName")
                    dataRows("OVDDate") = OVDDB.Rows(i).Item("OVDOTStartDate") + "~" + OVDDB.Rows(i).Item("OVDOTEndDate")
                    dataRows("OVDTime") = OVDDB.Rows(i).Item("OVDOTStartTime").ToString().Substring(0, 2) + ":" + OVDDB.Rows(i).Item("OVDOTStartTime").ToString().Substring(2, 2) + "~" + OVDDB.Rows(i).Item("OVDOTEndTime").ToString().Substring(0, 2) + ":" + OVDDB.Rows(i).Item("OVDOTEndTime").ToString().Substring(2, 2)
                    dataRows("OVDOTReasonMemo") = OVDDB.Rows(i).Item("OVDOTReasonMemo")
                    dataRows("OVDOTTypeName") = OVDDB.Rows(i).Item("OVDOTTypeName")
                    dataRows("OVDOTStatus") = OVDDB.Rows(i).Item("OVDOTStatus")
                    dataRows("OVDOTTxnID") = OVDDB.Rows(i).Item("OVDOTTxnID")
                    dataRows("OVDOTPayDate") = OVDDB.Rows(i).Item("OVDOTPayDate")

                    detialTable.Rows.Add(dataRows)
                    Dim OVAOTSeqNo As String = OVADB.Rows(j).Item("OVAOTSeqNo")
                    Dim OVAOTTxnID As String = OVADB.Rows(j).Item("OVAOTTxnID")

                    Dim OVDOTSeqNo As String = OVDDB.Rows(i).Item("OVDOTSeqNo")
                    Dim OVDOTTxnID As String = OVDDB.Rows(i).Item("OVDOTTxnID")

                    reMoveRowForOVADBList.Add(OVAOTSeqNo + "~" + OVAOTTxnID)
                    reMoveRowForOVDDBList.Add(OVDOTSeqNo + "~" + OVDOTTxnID)
                End If
            Next
        Next


        For i = 0 To detialTable.Rows.Count - 1
            If (detialTable.Rows(i).Item("OVAOTStatus").ToString()).Equals("1") Then
                detialTable.Rows(i).Item("OVAOTStatus") = "暫存"
            ElseIf (detialTable.Rows(i).Item("OVAOTStatus").ToString()).Equals("2") Then
                detialTable.Rows(i).Item("OVAOTStatus") = "送簽"
            ElseIf (detialTable.Rows(i).Item("OVAOTStatus").ToString()).Equals("3") Then
                detialTable.Rows(i).Item("OVAOTStatus") = "核准"
            ElseIf (detialTable.Rows(i).Item("OVAOTStatus").ToString()).Equals("4") Then
                detialTable.Rows(i).Item("OVAOTStatus") = "駁回"
            ElseIf (detialTable.Rows(i).Item("OVAOTStatus").ToString()).Equals("5") Then
                detialTable.Rows(i).Item("OVAOTStatus") = "刪除"
            ElseIf (detialTable.Rows(i).Item("OVAOTStatus").ToString()).Equals("9") Then
                detialTable.Rows(i).Item("OVAOTStatus") = "取消"
            End If

            If (detialTable.Rows(i).Item("OVDOTStatus").ToString()).Equals("1") Then
                detialTable.Rows(i).Item("OVDOTStatus") = "暫存"
            ElseIf (detialTable.Rows(i).Item("OVDOTStatus").ToString()).Equals("2") Then
                detialTable.Rows(i).Item("OVDOTStatus") = "送簽"
            ElseIf (detialTable.Rows(i).Item("OVDOTStatus").ToString()).Equals("3") Then
                detialTable.Rows(i).Item("OVDOTStatus") = "核准"
            ElseIf (detialTable.Rows(i).Item("OVDOTStatus").ToString()).Equals("4") Then
                detialTable.Rows(i).Item("OVDOTStatus") = "駁回"
            ElseIf (detialTable.Rows(i).Item("OVDOTStatus").ToString()).Equals("5") Then
                detialTable.Rows(i).Item("OVDOTStatus") = "刪除"
            ElseIf (detialTable.Rows(i).Item("OVDOTStatus").ToString()).Equals("6") Then
                detialTable.Rows(i).Item("OVDOTStatus") = "取消"
            ElseIf (detialTable.Rows(i).Item("OVDOTStatus").ToString()).Equals("7") Then
                detialTable.Rows(i).Item("OVDOTStatus") = "作廢"
            ElseIf (detialTable.Rows(i).Item("OVDOTStatus").ToString()).Equals("9") Then
                detialTable.Rows(i).Item("OVDOTStatus") = "計薪後收回"
            End If
        Next

        '20170711 - Jason 新增GroupType來排序, 1=申請,2=申報,3=合單
        'Table排序方式 
        detialTable.DefaultView.Sort = "GroupType, OTCompID, EmpID, OVADate, OVDDate, OVATime, OVDTime"
        detialTable = detialTable.DefaultView.ToTable()

        Return (detialTable)
    End Function
    '合單

#Region "虛擬Table"
    Public Function getTableForDetial() As DataTable
        Dim myTable As New DataTable
        Dim col As DataColumn

        '20170711 - Jason 新增GroupType來排序, 1=申請,2=申報,3=合單
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "GroupType"
        myTable.Columns.Add(col)

        '公司編號
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "OTCompID"
        myTable.Columns.Add(col)


        '員工編號
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "EmpID"
        myTable.Columns.Add(col)
        '加班人
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "Name"
        myTable.Columns.Add(col)

        'OVA加班日期
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "OVADate"
        myTable.Columns.Add(col)
        'OVA加班起訖時間
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "OVATime"
        myTable.Columns.Add(col)
        'OVA加班類型
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "OVAOTTypeName"
        myTable.Columns.Add(col)
        'OVA加班原因
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "OVAOTReasonMemo"
        myTable.Columns.Add(col)
        'OVA狀態
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "OVAOTStatus"
        myTable.Columns.Add(col)
        'OVA單號
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "OVAOTTxnID"
        myTable.Columns.Add(col)



        'OVD加班日期
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "OVDOTPayDate"
        myTable.Columns.Add(col)
        'OVD加班日期
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "OVDDate"
        myTable.Columns.Add(col)
        'OVD加班起訖時間
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "OVDTime"
        myTable.Columns.Add(col)
        'OVD加班類型
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "OVDOTTypeName"
        myTable.Columns.Add(col)
        'OVD加班原因
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "OVDOTReasonMemo"
        myTable.Columns.Add(col)
        'OVD狀態
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "OVDOTStatus"
        myTable.Columns.Add(col)
        'OVD單號
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "OVDOTTxnID"
        myTable.Columns.Add(col)

        Return myTable
    End Function





    Public Function getTableForStatistics() As DataTable
        Dim myTable As New DataTable
        Dim col As DataColumn
        '公司編號
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "OTCompID"
        myTable.Columns.Add(col)


        '員工編號
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "EmpID"
        myTable.Columns.Add(col)
        '加班人
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "Name"
        myTable.Columns.Add(col)



        '2  送簽
        '3: 核准
        '4: 駁回

        'OVA送簽
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "OVAStatusTimeFor2"
        myTable.Columns.Add(col)

        'OVA核准
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "OVAStatusTimeFor3"
        myTable.Columns.Add(col)

        'OVA駁回
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "OVAStatusTimeFor4"
        myTable.Columns.Add(col)

        'OVD送簽
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "OVDStatusTimeFor2"
        myTable.Columns.Add(col)

        'OVD核准
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "OVDStatusTimeFor3"
        myTable.Columns.Add(col)

        'OVD駁回
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "OVDStatusTimeFor4"
        myTable.Columns.Add(col)

        Return myTable
    End Function
#End Region
    '合單
    ''' <summary>
    ''' 先走訪把db的資料有跨日的OTTxnID放入arrayList
    ''' 然後移除有兩張單的ROW
    ''' 放入合單後的dataTable
    ''' </summary>
    ''' <param name="db"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SingleDataTable(ByVal db As DataTable, ByVal type As String) As DataTable
        ' Dim detialTable As DataTable = getTableForDetial()
        Dim arrayList As ArrayList = New ArrayList()
        If "bef".Equals(type) Then
            '先走訪如果有兩張單的單號先記起來
            For i = 0 To db.Rows.Count - 1
                If "2".Equals(db.Rows(i).Item("OVAOTSeqNo")) Then
                    arrayList.Add(db.Rows(i).Item("OVAOTTxnID"))
                End If
            Next
            Dim cloneDB As DataTable = db.Copy

            '回傳的TABLE移除有兩張單的ROW
            For i = 0 To arrayList.Count - 1
                cloneDB.[Select]("OVAOTTxnID='" + arrayList.Item(i) + "'")
                Dim row As DataRow() = cloneDB.[Select]("OVAOTTxnID='" + arrayList.Item(i) + "'")
                For j = 0 To row.Count - 1
                    cloneDB.Rows.Remove(row(j))
                Next
            Next


            '合單
            For i = 0 To arrayList.Count - 1
                Dim rows As DataRow() = db.[Select]("OVAOTTxnID= '" + arrayList.Item(i) + "'", "OVAOTSeqNo")
                Dim dataRows As DataRow

                If rows.Count > 1 Then
                    dataRows = cloneDB.NewRow
                    dataRows("OVAEmpID") = rows(0).Item("OVAEmpID")
                    dataRows("OVAName") = rows(0).Item("OVAName")
                    dataRows("OVAOTCompID") = rows(0).Item("OVAOTCompID")
                    dataRows("OVAOTStartDate") = rows(0).Item("OVAOTStartDate")
                    dataRows("OVAOTEndDate") = rows(1).Item("OVAOTEndDate")
                    dataRows("OVAOTSeq") = rows(0).Item("OVAOTSeq")
                    dataRows("OVAOTStartTime") = rows(0).Item("OVAOTStartTime")
                    dataRows("OVAOTEndTime") = rows(1).Item("OVAOTEndTime")
                    dataRows("OVAOTTypeName") = rows(0).Item("OVAOTTypeName")
                    dataRows("OVAOTTxnID") = rows(0).Item("OVAOTTxnID")
                    dataRows("OVAOTSeqNo") = rows(0).Item("OVAOTSeqNo")
                    dataRows("OVAOTReasonMemo") = rows(0).Item("OVAOTReasonMemo")
                    dataRows("OVAOTStatus") = rows(0).Item("OVAOTStatus")
                    cloneDB.Rows.Add(dataRows)
                Else

                    dataRows = cloneDB.NewRow
                    dataRows("OVAEmpID") = rows(0).Item("OVAEmpID")
                    dataRows("OVAName") = rows(0).Item("OVAName")
                    dataRows("OVAOTCompID") = rows(0).Item("OVAOTCompID")
                    dataRows("OVAOTStartDate") = rows(0).Item("OVAOTStartDate")
                    dataRows("OVAOTEndDate") = rows(0).Item("OVAOTEndDate")
                    dataRows("OVAOTSeq") = rows(0).Item("OVAOTSeq")
                    dataRows("OVAOTStartTime") = rows(0).Item("OVAOTStartTime")
                    dataRows("OVAOTEndTime") = rows(0).Item("OVAOTEndTime")
                    dataRows("OVAOTTypeName") = rows(0).Item("OVAOTTypeName")
                    dataRows("OVAOTTxnID") = rows(0).Item("OVAOTTxnID")
                    dataRows("OVAOTSeqNo") = rows(0).Item("OVAOTSeqNo")
                    dataRows("OVAOTReasonMemo") = rows(0).Item("OVAOTReasonMemo")
                    dataRows("OVAOTStatus") = rows(0).Item("OVAOTStatus")
                    cloneDB.Rows.Add(dataRows)
                End If

            Next
            Return cloneDB

        Else
            '先走訪如果有兩張單的單號先記起來
            For i = 0 To db.Rows.Count - 1
                If "2".Equals(db.Rows(i).Item("OVDOTSeqNo")) Then
                    arrayList.Add(db.Rows(i).Item("OVDOTTxnID"))
                End If
            Next
            Dim cloneDB As DataTable = db.Copy

            '回傳的TABLE移除有兩張單的ROW
            For i = 0 To arrayList.Count - 1
                Dim row As DataRow() = cloneDB.[Select]("OVDOTTxnID='" + arrayList.Item(i) + "'")
                For j = 0 To row.Count - 1
                    cloneDB.Rows.Remove(row(j))
                Next
            Next

            '合單
            For i = 0 To arrayList.Count - 1
                Dim rows As DataRow() = db.[Select]("OVDOTTxnID='" + arrayList.Item(i) + "'", "OVDOTSeqNo")
                Dim dataRows As DataRow
                If rows.Count > 1 Then
                    dataRows = cloneDB.NewRow
                    dataRows("OVDEmpID") = rows(0).Item("OVDEmpID")
                    dataRows("OVDName") = rows(0).Item("OVDName")
                    dataRows("OVDOTCompID") = rows(0).Item("OVDOTCompID")
                    dataRows("OVDOTStartDate") = rows(0).Item("OVDOTStartDate")
                    dataRows("OVDOTEndDate") = rows(1).Item("OVDOTEndDate")
                    dataRows("OVDOTSeq") = rows(0).Item("OVDOTSeq")
                    dataRows("OVDOTStartTime") = rows(0).Item("OVDOTStartTime")
                    dataRows("OVDOTEndTime") = rows(1).Item("OVDOTEndTime")
                    dataRows("OVDOTTypeName") = rows(0).Item("OVDOTTypeName")
                    dataRows("OVDOTTxnID") = rows(0).Item("OVDOTTxnID")
                    dataRows("OVDOTSeqNo") = rows(0).Item("OVDOTSeqNo")
                    dataRows("OVDOTReasonMemo") = rows(0).Item("OVDOTReasonMemo")
                    dataRows("OVDOTStatus") = rows(0).Item("OVDOTStatus")
                    dataRows("OVDOTFromAdvanceTxnId") = rows(0).Item("OVDOTFromAdvanceTxnId")
                    cloneDB.Rows.Add(dataRows)
                Else
                    dataRows = cloneDB.NewRow
                    dataRows("OVDEmpID") = rows(0).Item("OVDEmpID")
                    dataRows("OVDName") = rows(0).Item("OVDName")
                    dataRows("OVDOTCompID") = rows(0).Item("OVDOTCompID")
                    dataRows("OVDOTStartDate") = rows(0).Item("OVDOTStartDate")
                    dataRows("OVDOTEndDate") = rows(0).Item("OVDOTEndDate")
                    dataRows("OVDOTSeq") = rows(0).Item("OVDOTSeq")
                    dataRows("OVDOTStartTime") = rows(0).Item("OVDOTStartTime")
                    dataRows("OVDOTEndTime") = rows(0).Item("OVDOTEndTime")
                    dataRows("OVDOTTypeName") = rows(0).Item("OVDOTTypeName")
                    dataRows("OVDOTTxnID") = rows(0).Item("OVDOTTxnID")
                    dataRows("OVDOTSeqNo") = rows(0).Item("OVDOTSeqNo")
                    dataRows("OVDOTReasonMemo") = rows(0).Item("OVDOTReasonMemo")
                    dataRows("OVDOTStatus") = rows(0).Item("OVDOTStatus")
                    dataRows("OVDOTFromAdvanceTxnId") = rows(0).Item("OVDOTFromAdvanceTxnId")
                    cloneDB.Rows.Add(dataRows)

                End If

            Next
            Return cloneDB
        End If
    End Function


    '合單
    ''' <summary> 'OVAEmpID,OVAName,OVAOTCompID,OVAOTStatus,OVAOTTotalTime,MealFlag,MealTime,OVAOTSeqNo,OVAOTTxnID

    ''' 先走訪把db的資料有跨日的OTTxnID放入arrayList
    ''' 然後移除有兩張單的ROW
    ''' 放入合單後的dataTable
    ''' </summary>
    ''' <param name="db"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SingleDataTableForStatistics(ByVal db As DataTable, ByVal type As String) As DataTable

        ' Dim detialTable As DataTable = getTableForDetial()
        Dim arrayList As ArrayList = New ArrayList()
        If "bef".Equals(type) Then
            '先走訪如果有兩張單的單號先記起來
            For i = 0 To db.Rows.Count - 1
                If "2".Equals(db.Rows(i).Item("OVAOTSeqNo")) Then
                    arrayList.Add(db.Rows(i).Item("OVAOTTxnID"))
                End If
            Next
            Dim cloneDB As DataTable = db.Copy

            '回傳的TABLE移除有兩張單的ROW
            For i = 0 To arrayList.Count - 1
                cloneDB.[Select]("OVAOTTxnID='" + arrayList.Item(i) + "'")
                Dim row As DataRow() = cloneDB.[Select]("OVAOTTxnID='" + arrayList.Item(i) + "'")
                For j = 0 To row.Count - 1
                    cloneDB.Rows.Remove(row(j))
                Next
            Next

            '合單
            For i = 0 To arrayList.Count - 1
                Dim rows As DataRow() = db.[Select]("OVAOTTxnID= '" + arrayList.Item(i) + "'", "OVAOTSeqNo")
                '防止跨日卻只抓到其中一個
                If rows.Count > 1 Then
                    Dim dataRows As DataRow
                    dataRows = cloneDB.NewRow
                    dataRows("OVAEmpID") = rows(0).Item("OVAEmpID")
                    dataRows("OVAName") = rows(0).Item("OVAName")
                    dataRows("OVAOTCompID") = rows(0).Item("OVAOTCompID")
                    dataRows("OVAOTStatus") = rows(0).Item("OVAOTStatus")
                    dataRows("OVAOTTotalTime") = Convert.ToDouble(rows(0).Item("OVAOTTotalTime")) + Convert.ToDouble(rows(1).Item("OVAOTTotalTime"))
                    dataRows("MealFlag") = rows(0).Item("MealFlag")
                    dataRows("MealTime") = Convert.ToDouble(rows(0).Item("MealTime")) + Convert.ToDouble(rows(1).Item("MealTime"))
                    dataRows("OVAOTSeqNo") = "1"
                    dataRows("OVAOTTxnID") = rows(0).Item("OVAOTTxnID")
                    cloneDB.Rows.Add(dataRows)
                Else
                    Dim dataRows As DataRow
                    dataRows = cloneDB.NewRow
                    dataRows("OVAEmpID") = rows(0).Item("OVAEmpID")
                    dataRows("OVAName") = rows(0).Item("OVAName")
                    dataRows("OVAOTCompID") = rows(0).Item("OVAOTCompID")
                    dataRows("OVAOTStatus") = rows(0).Item("OVAOTStatus")
                    dataRows("OVAOTTotalTime") = Convert.ToDouble(rows(0).Item("OVAOTTotalTime")) + Convert.ToDouble(rows(0).Item("OVAOTTotalTime"))
                    dataRows("MealFlag") = rows(0).Item("MealFlag")
                    dataRows("MealTime") = Convert.ToDouble(rows(0).Item("MealTime")) + Convert.ToDouble(rows(0).Item("MealTime"))
                    dataRows("OVAOTSeqNo") = "1"
                    dataRows("OVAOTTxnID") = rows(0).Item("OVAOTTxnID")
                    cloneDB.Rows.Add(dataRows)
                End If

            Next
            Return cloneDB

        Else

            '先走訪如果有兩張單的單號先記起來
            For i = 0 To db.Rows.Count - 1
                If "2".Equals(db.Rows(i).Item("OVDOTSeqNo")) Then
                    arrayList.Add(db.Rows(i).Item("OVDOTTxnID"))
                End If
            Next
            Dim cloneDB As DataTable = db.Copy

            '回傳的TABLE移除有兩張單的ROW
            For i = 0 To arrayList.Count - 1
                cloneDB.[Select]("OVDOTTxnID='" + arrayList.Item(i) + "'")
                Dim row As DataRow() = cloneDB.[Select]("OVDOTTxnID='" + arrayList.Item(i) + "'")
                For j = 0 To row.Count - 1
                    cloneDB.Rows.Remove(row(j))
                Next
            Next

            '合單
            For i = 0 To arrayList.Count - 1
                Dim rows As DataRow() = db.[Select]("OVDOTTxnID= '" + arrayList.Item(i) + "'", "OVDOTSeqNo")
                '防止跨日卻只抓到其中一個
                If rows.Count > 1 Then
                    Dim dataRows As DataRow
                    dataRows = cloneDB.NewRow
                    dataRows("OVDEmpID") = rows(0).Item("OVDEmpID")
                    dataRows("OVDName") = rows(0).Item("OVDName")
                    dataRows("OVDOTCompID") = rows(0).Item("OVDOTCompID")
                    dataRows("OVDOTStatus") = rows(0).Item("OVDOTStatus")
                    dataRows("OVDOTTotalTime") = Convert.ToDouble(rows(0).Item("OVDOTTotalTime")) + Convert.ToDouble(rows(1).Item("OVDOTTotalTime"))
                    dataRows("MealFlag") = rows(0).Item("MealFlag")
                    dataRows("MealTime") = Convert.ToDouble(rows(0).Item("MealTime")) + Convert.ToDouble(rows(1).Item("MealTime"))
                    dataRows("OVDOTSeqNo") = "1"
                    dataRows("OVDOTTxnID") = rows(0).Item("OVDOTTxnID")
                    cloneDB.Rows.Add(dataRows)
                Else
                    Dim dataRows As DataRow
                    dataRows = cloneDB.NewRow
                    dataRows("OVDEmpID") = rows(0).Item("OVDEmpID")
                    dataRows("OVDName") = rows(0).Item("OVDName")
                    dataRows("OVDOTCompID") = rows(0).Item("OVDOTCompID")
                    dataRows("OVDOTStatus") = rows(0).Item("OVDOTStatus")
                    dataRows("OVDOTTotalTime") = Convert.ToDouble(rows(0).Item("OVDOTTotalTime")) + Convert.ToDouble(rows(0).Item("OVDOTTotalTime"))
                    dataRows("MealFlag") = rows(0).Item("MealFlag")
                    dataRows("MealTime") = Convert.ToDouble(rows(0).Item("MealTime")) + Convert.ToDouble(rows(0).Item("MealTime"))
                    dataRows("OVDOTSeqNo") = "1"
                    dataRows("OVDOTTxnID") = rows(0).Item("OVDOTTxnID")
                    cloneDB.Rows.Add(dataRows)
                End If
            Next
            Return cloneDB

        End If
    End Function

#End Region

    Public Function isNull(ByVal strPro As String) As Boolean
        Dim myType As Type = GetType(OV2)
        Dim myPropInfo As PropertyInfo
        Dim myProVal
        myPropInfo = myType.GetProperty(strPro)
        myProVal = myPropInfo.GetValue(Me, Nothing)

        If myProVal.Equals("") Or myProVal Is Nothing Then
            Return False
        Else
            Return True
        End If
    End Function


End Class

