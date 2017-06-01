Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Reflection
Imports System.Globalization
Imports System.Data.Common
Imports Newtonsoft.Json
Imports System.Diagnostics      'For Debug.Print()

Public Class OV_3
#Region "Property"
    Public Property Type As String 'after bef
    Public Property CompID As String
    Public Property OrgType As String
    Public Property DeptID As String
    Public Property OTStartTime As String
    Public Property OTEndTime As String
    Public Property OrganID As String
    Public Property RankIDMIN As String
    Public Property RankIDMAX As String
    Public Property TitleID As String

    Public Property TitleIDMIN As String '20170219 Beatrice mod
    Public Property TitleIDMAX As String '20170219 Beatrice mod
    Public Property Sex As String
    Public Property PositionID As String
    Public Property WorkType As String
    Public Property OTTypeID As String
    Public Property HolidayOrNot As String
    Public Property OTStatus As String
    Public Property OTPayDate As String
    Public Property OTSalaryPaid As String
    Public Property OTEmpID As String
    Public Property OTEmpName As String
    Public Property OTFormNO As String
    Public Property OvertimeDateB As String
    Public Property OvertimeDateE As String
    Public Property TakeOfficeDateB As String
    Public Property TakeOfficeDateE As String
    Public Property LeaveOfficeDateB As String
    Public Property LeaveOfficeDateE As String
    Public Property DateOfApprovalB As String
    Public Property DateOfApprovalE As String
    Public Property DateOfApplicationB As String
    Public Property DateOfApplicationE As String
    Public Property WorkStatus As String
    Public Property TitleName As String
    Public Property Time As String
    Public Property ProcessDate As String
    Public Property mealTime As String
    Public Property mealFlag As String
    Public Property SalaryOrAdjust As String
    Public Property AdjustInvalidDate As String
    Public Property OTReasonMemo As String
    Public Property LastChgComp As String
    Public Property LastChgID As String
    Public Property LastChgDate As String
    Public Property OTTxnID As String
    Public Property Seq As String
    Public Property OTAttachment As String
    Public Property OTRegisterComp As String
    Public Property OTSalaryOrAdjust As String
    Public Property isSalaryPaid As Boolean = False


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
#End Region

#Region "SqlStr"
    Public Function getStrSqlForDownLoan() As String
        Dim strSQL As New StringBuilder
        If Type.Equals("bef") Then
            strSQL.AppendLine("select ORT2.OrganName AS '單位',ORT1.OrganName AS '部門',ORT.OrganName AS '科別',P.EmpID AS '員工編號'")
            strSQL.AppendLine(",P.Name AS '員工姓名', P.Sex AS '性別',WS.Remark AS '在職狀態',PO.PositionID AS '職位代碼',PO.Remark AS '職位'")
            strSQL.AppendLine(",P.RankID AS '職等',T.TitleID AS '職稱代碼',T.TitleName AS '職稱',W.WorkTypeID AS '工作性質代碼',W.Remark AS '工作性質'")
            strSQL.AppendLine(",OVA.OTStartDate as '加班開始日期',+OVA.OTEndDate AS '加班結束日期',OVA.OTStartTime AS '加班開始時間',OVA.OTEndTime  as '加班結束時間',AT.CodeCName AS '加班類型',OVA.HolidayOrNot AS 'HolidayOrNot'")
            strSQL.AppendLine(",OVA.OTStatus AS '表單狀態',OVA.OTFormNO AS '表單號碼',P.EmpDate AS '到職日期1' ,P.QuitDate AS '離職日期1',OVA.OTValidDate AS '核准日期1',OVA.OTTotalTime AS '加班時數1',OVA.OTSeqNo AS 'OTSeqNo'")
            strSQL.AppendLine(",OVA.OTRegisterDate AS '申請日期1',OVA.OTReasonMemo AS '加班原因',OVA.SalaryOrAdjust AS '轉薪資/補休',OVA.AdjustInvalidDate AS '補休失效日1',OVA.OTAttachment AS '上傳附件',OVA.MealTime AS 'MealTime',OVA.OTTxnID AS OTTxnID,OVA.OTValidID AS 'OTValidID'")
            strSQL.AppendLine("from OverTimeAdvance OVA")
            strSQL.AppendLine("LEFT JOIN " + eHRMSDB_ITRD + ".[dbo].Personal  P on OVA.OTCompID= P.CompID and OVA.OTEmpID = P.EmpID")
            strSQL.AppendLine("left join AT_CodeMap AT on OVA.OTTypeID=AT.Code and AT.TabName='OverTime' and AT.FldName='OverTimeType'") '
            strSQL.AppendLine("left join " + eHRMSDB_ITRD + ".[dbo].WorkStatus  WS ON P.WorkStatus= WS.WorkCode")
            strSQL.AppendLine("left join " + eHRMSDB_ITRD + ".[dbo].Title  T ON P.CompID=T.CompID and P.RankID=T.RankID and P.TitleID=T.TitleID")
            strSQL.AppendLine("left join " + eHRMSDB_ITRD + ".[dbo].Company C ON P.CompID=C.CompID")
            strSQL.AppendLine("left join " + eHRMSDB_ITRD + ".[dbo].EmpPosition EP ON P.CompID=EP.CompID AND P.EmpID=EP.EmpID AND EP.PrincipalFlag='1'")
            strSQL.AppendLine("left join " + eHRMSDB_ITRD + ".[dbo].Position PO ON EP.PositionID=PO.PositionID")
            strSQL.AppendLine("left join " + eHRMSDB_ITRD + ".[dbo].EmpWorkType EWT ON P.CompID=EWT.CompID AND P.EmpID=EWT.EmpID and EWT.PrincipalFlag='1'")
            strSQL.AppendLine("left join " + eHRMSDB_ITRD + ".[dbo].WorkType W ON EWT.WorkTypeID= W.WorkTypeID  AND P.CompID=W.CompID")
            strSQL.AppendLine("left join " + eHRMSDB_ITRD + ".[dbo].Organization ORT ON  P.OrganID=ORT.OrganID AND P.CompID =ORT.CompID")
            strSQL.AppendLine("left join " + eHRMSDB_ITRD + ".[dbo].Organization ORT1 ON P.DeptID=ORT1.OrganID")
            strSQL.AppendLine("left join " + eHRMSDB_ITRD + ".[dbo].Organization ORT2 ON ORT.OrgType=ORT2.OrganID")
            ''20170304kevin 
            strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].RankMapping RM on P.RankID=RM.RankID  AND RM.CompID=OVA.OTCompID")
            'strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].RankMapping RM on P.RankID=RM.RankID")
            'strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].RankMapping TRM on T.RankID=RM.RankID")
            strSQL.AppendLine("Where 1=1")
            strSQL.AppendLine(" And OVA.OTCompID = '" + UserProfile.SelectCompRoleID + "'")
            If isNull("OTEmpName") Then
                strSQL.AppendLine(" and P.Name=" + Bsp.Utility.Quote(OTEmpName))
            End If


            If isNull("OvertimeDateB") Then
                strSQL.AppendLine(" and OVA.OTStartDate>='" + OvertimeDateB + "'")
            End If
            If isNull("OvertimeDateE") Then
                strSQL.AppendLine(" and OVA.OTEndDate<='" + OvertimeDateE + "'")
            End If

            If isNull("OTStartTime") Then
                strSQL.AppendLine(" and (Cast(OVA.OTStartTime as int)>=Cast('" + OTStartTime + "' as int))")
            End If
            If isNull("OTEndTime") Then
                strSQL.AppendLine(" and (Cast(OVA.OTEndTime as int)<=Cast('" + OTEndTime + "' as int))")
            End If

            If isNull("OTStatus") Then
                strSQL.AppendLine("and OVA.OTStatus='" + OTStatus + "'")
            End If

            If isNull("DateOfApprovalB") Then
                strSQL.AppendLine(" and not REPLACE (OVA.OTValidID,' ','') =''")
                strSQL.AppendLine(" and OVA.OTValidDate>='" + DateOfApprovalB + "'")

            End If

            If isNull("DateOfApprovalE") Then
                strSQL.AppendLine("and OVA.OTValidDate<='" + DateOfApprovalE + " 23:59:59'")
                strSQL.AppendLine(" and not REPLACE (OVA.OTValidID,' ','') =''")
            End If

            If isNull("DateOfApplicationB") Then
                strSQL.AppendLine(" and OVA.OTRegisterDate>='" + DateOfApplicationB + "'")
            End If

            If isNull("DateOfApplicationE") Then
                strSQL.AppendLine("and OVA.OTRegisterDate<='" + DateOfApplicationE + " 23:59:59'")
            End If
            If isNull("HolidayOrNot") Then
                strSQL.AppendLine(" and OVA.HolidayOrNot='" + HolidayOrNot + "'")
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

            If isNull("TakeOfficeDateB") Then
                strSQL.AppendLine(" and P.EmpDate>='" + TakeOfficeDateB + "'")
            End If
            If isNull("TakeOfficeDateE") Then
                strSQL.AppendLine(" and P.EmpDate<='" + TakeOfficeDateE + " 23:59:59'")
            End If
            If isNull("LeaveOfficeDateB") Then
                strSQL.AppendLine(" and P.QuitDate>='" + LeaveOfficeDateB + "'")
            End If
            If isNull("LeaveOfficeDateE") Then
                strSQL.AppendLine(" and P.QuitDate<='" + LeaveOfficeDateE + " 23:59:59'")
                strSQL.AppendLine("and P.WorkStatus='3'")
            End If
            If isNull("Sex") Then
                strSQL.AppendLine(" and P.Sex='" + Sex + "'")
            End If


            If isNull("OTTypeID") Then
                strSQL.AppendLine(" and OVA.OTTypeID='" + OTTypeID + "'")
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
            ''20170219 Beatrice mod
            'If isNull("RankIDMIN") Then
            '    If isNull("TitleIDMIN") Then
            '        strSQL.AppendLine(" and T.RankID + T.TitleID>='" + RankIDMIN + TitleIDMIN + "'")
            '    Else
            '        strSQL.AppendLine(" and T.RankID>='" + RankIDMIN + "'")
            '    End If
            'End If
            'If isNull("RankIDMAX") Then
            '    If isNull("TitleIDMAX") Then
            '        strSQL.AppendLine(" and T.RankID + T.TitleID<='" + RankIDMAX + TitleIDMAX + "'")
            '    Else
            '        strSQL.AppendLine(" and T.RankID<='" + RankIDMAX + "'")
            '    End If
            'End If
            ''未確認
            'If isNull("TitleID") Then
            '    strSQL.AppendLine(" and T.TitleID='" + TitleID + "'")
            '    strSQL.AppendLine(" and T.TitleName='" + TitleName + "'")
            'End If


            If isNull("PositionID") Then
                strSQL.AppendLine(" and PO.PositionID='" + PositionID + "'")
            End If

            If isNull("WorkType") Then
                strSQL.AppendLine(" and W.WorkTypeID='" + WorkType + "'")
            End If

            If isNull("OrganID") Then
                strSQL.AppendLine(" and ORT.OrganID='" + OrganID + "'")
            End If

            If isNull("DeptID") Then
                strSQL.AppendLine(" and ORT1.OrganID='" + DeptID + "'")
            End If

            If isNull("OrgType") Then
                strSQL.AppendLine(" and ORT2.OrganID='" + OrgType + "'")
            End If
            strSQL.Append(" order by OTEmpID,OTStartDate,OTStartTime")
        Else

            strSQL.AppendLine("select ORT2.OrganName AS '單位',ORT1.OrganName AS '部門',ORT.OrganName AS '科別',P.EmpID AS '員工編號'")
            strSQL.AppendLine(",P.Name AS '員工姓名', P.Sex AS '性別',WS.Remark AS '在職狀態',PO.PositionID AS '職位代碼',PO.Remark AS '職位'")
            strSQL.AppendLine(",P.RankID AS '職等',T.TitleID AS '職稱代碼',T.TitleName AS '職稱',W.WorkTypeID AS '工作性質代碼',W.Remark AS '工作性質'")
            strSQL.AppendLine(",OVD.OTStartDate as '加班開始日期',+OVD.OTEndDate AS '加班結束日期',OVD.OTStartTime AS '加班開始時間',OVD.OTEndTime  as '加班結束時間',AT.CodeCName AS '加班類型',OVD.HolidayOrNot AS 'HolidayOrNot'")


            strSQL.AppendLine(",OVD.OTStatus AS '表單狀態',OVD.OTFormNO AS '表單號碼',P.EmpDate AS '到職日期1' ,P.QuitDate AS '離職日期1',OVD.OTValidDate AS '核准日期1'")
            strSQL.AppendLine(",OVD.OTRegisterDate AS '申請日期1',OVD.OTReasonMemo AS '加班原因',OVD.SalaryOrAdjust AS '轉薪資/補休',OVD.AdjustInvalidDate AS '補休失效日1',OVD.OTAttachment AS '上傳附件',OVD.ToOverTimeDate AS 'ToOverTimeDate'")
            strSQL.AppendLine(",OVD.OTCompID AS OTCompID,OVD.OTStartDate AS OTStartDate,OVD.OTEndDate AS OTEndDate ")
            strSQL.AppendLine(",OVD.OTStartTime AS OTStartTime,OVD.OTEndTime AS OTEndTime,OVD.OTTxnID AS OTTxnID,OVD.OTSeqNo AS OTSeqNo")
            strSQL.AppendLine(",OVD.MealFlag AS MealFlag,OVD.MealTime AS MealTime,OVD.OTStatus AS OTStatus,OVD.OTSalaryPaid AS OTSalaryPaid,OVD.OTValidID AS 'OTValidID',OVD.OTTotalTime AS '加班時數1'")
            strSQL.AppendLine(", OVD.HolidayOrNot AS 'HolidayOrNot',OVD.ToOverTimeFlag as 'ToOverTimeFlag'")
            strSQL.AppendLine("from OverTimeDeclaration OVD")
            strSQL.AppendLine("LEFT JOIN " + eHRMSDB_ITRD + ".[dbo].Personal  P on OVD.OTCompID= P.CompID and OVD.OTEmpID = P.EmpID")
            strSQL.AppendLine("left join AT_CodeMap AT on OVD.OTTypeID=AT.Code and AT.TabName='OverTime' and AT.FldName='OverTimeType'")
            strSQL.AppendLine("left join " + eHRMSDB_ITRD + ".[dbo].WorkStatus  WS ON P.WorkStatus= WS.WorkCode")
            strSQL.AppendLine("left join " + eHRMSDB_ITRD + ".[dbo].Title  T ON P.CompID=T.CompID and P.RankID=T.RankID and P.TitleID=T.TitleID")
            strSQL.AppendLine("left join " + eHRMSDB_ITRD + ".[dbo].Company C ON P.CompID=C.CompID")
            strSQL.AppendLine("left join " + eHRMSDB_ITRD + ".[dbo].EmpPosition EP ON P.CompID=EP.CompID AND P.EmpID=EP.EmpID AND EP.PrincipalFlag='1'")
            strSQL.AppendLine("left join " + eHRMSDB_ITRD + ".[dbo].Position PO ON EP.PositionID=PO.PositionID")
            strSQL.AppendLine("left join " + eHRMSDB_ITRD + ".[dbo].EmpWorkType EWT ON P.CompID=EWT.CompID AND P.EmpID=EWT.EmpID and EWT.PrincipalFlag='1'")
            strSQL.AppendLine("left join " + eHRMSDB_ITRD + ".[dbo].WorkType W ON EWT.WorkTypeID= W.WorkTypeID  AND P.CompID=W.CompID")
            strSQL.AppendLine("left join " + eHRMSDB_ITRD + ".[dbo].Organization ORT ON  P.OrganID=ORT.OrganID AND P.CompID =ORT.CompID")
            strSQL.AppendLine("left join " + eHRMSDB_ITRD + ".[dbo].Organization ORT1 ON P.DeptID=ORT1.OrganID")
            strSQL.AppendLine("left join " + eHRMSDB_ITRD + ".[dbo].Organization ORT2 ON ORT.OrgType=ORT2.OrganID")
            '20170304kevin 
            strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].RankMapping RM on P.RankID=RM.RankID  AND RM.CompID=OVD.OTCompID")
            'strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].RankMapping RM on P.RankID=RM.RankID")
            'strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].RankMapping TRM on T.RankID=RM.RankID")
            strSQL.AppendLine("Where 1=1")
            strSQL.AppendLine(" And OVD.OTCompID = '" + UserProfile.SelectCompRoleID + "'")
            If isNull("OTEmpName") Then
                strSQL.AppendLine(" and P.Name=" + Bsp.Utility.Quote(OTEmpName))
            End If

            If isNull("OTPayDate") Then
                strSQL.AppendLine("and CONVERT(varchar, OVD.OTPayDate)='" + OTPayDate + "'")
            End If

            If isNull("OvertimeDateB") Then
                strSQL.AppendLine(" and OVD.OTStartDate>='" + OvertimeDateB + "'")
            End If
            If isNull("OvertimeDateE") Then
                strSQL.AppendLine(" and OVD.OTEndDate<='" + OvertimeDateE + "'")
            End If

            If isNull("OTStartTime") Then
                strSQL.AppendLine(" and (Cast(OVD.OTStartTime as int)>=Cast('" + OTStartTime + "' as int))")
            End If
            If isNull("OTEndTime") Then
                strSQL.AppendLine(" and (Cast(OVD.OTEndTime as int)<=Cast('" + OTEndTime + "' as int))")
            End If

            If isNull("OTStatus") Then
                strSQL.AppendLine("and OVD.OTStatus='" + OTStatus + "'")
            End If

            If isNull("DateOfApprovalB") Then
                strSQL.AppendLine(" and not REPLACE (OVD.OTValidID,' ','') =''")
                strSQL.AppendLine(" and OVD.OTValidDate>='" + DateOfApprovalB + "'")
            End If

            If isNull("DateOfApprovalE") Then
                strSQL.AppendLine("and OVD.OTValidDate<='" + DateOfApprovalE + " 23:59:59'")
                strSQL.AppendLine(" and not REPLACE (OVD.OTValidID,' ','') =''")
            End If

            If isNull("DateOfApplicationB") Then
                strSQL.AppendLine(" and OVD.OTRegisterDate>='" + DateOfApplicationB + "'")
            End If

            If isNull("DateOfApplicationE") Then
                strSQL.AppendLine("and OVD.OTRegisterDate<='" + DateOfApplicationE + " 23:59:59'")
            End If
            If isNull("HolidayOrNot") Then
                strSQL.AppendLine(" and OVD.HolidayOrNot='" + HolidayOrNot + "'")
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

            If isNull("TakeOfficeDateB") Then
                strSQL.AppendLine(" and P.EmpDate>='" + TakeOfficeDateB + "'")
            End If
            If isNull("TakeOfficeDateE") Then
                strSQL.AppendLine(" and P.EmpDate<='" + TakeOfficeDateE + " 23:59:59'")
            End If
            If isNull("LeaveOfficeDateB") Then
                strSQL.AppendLine(" and P.QuitDate>='" + LeaveOfficeDateB + "'")
            End If
            If isNull("LeaveOfficeDateE") Then
                strSQL.AppendLine(" and P.QuitDate<='" + LeaveOfficeDateE + " 23:59:59'")
                strSQL.AppendLine("and P.WorkStatus='3'")
            End If
            If isNull("Sex") Then
                strSQL.AppendLine(" and P.Sex='" + Sex + "'")
            End If


            If isNull("OTTypeID") Then
                strSQL.AppendLine(" and OVD.OTTypeID='" + OTTypeID + "'")
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

            ''20170219 Beatrice mod
            'If isNull("RankIDMIN") Then
            '    If isNull("TitleIDMIN") Then
            '        strSQL.AppendLine(" and T.RankID + T.TitleID>='" + RankIDMIN + TitleIDMIN + "'")
            '    Else
            '        strSQL.AppendLine(" and T.RankID>='" + RankIDMIN + "'")
            '    End If
            'End If
            'If isNull("RankIDMAX") Then
            '    If isNull("TitleIDMAX") Then
            '        strSQL.AppendLine(" and T.RankID + T.TitleID<='" + RankIDMAX + TitleIDMAX + "'")
            '    Else
            '        strSQL.AppendLine(" and T.RankID<='" + RankIDMAX + "'")
            '    End If
            'End If
            ''未確認
            'If isNull("TitleID") Then
            '    strSQL.AppendLine(" and T.TitleID='" + TitleID + "'")
            '    strSQL.AppendLine(" and T.TitleName='" + TitleName + "'")
            'End If


            If isNull("PositionID") Then
                strSQL.AppendLine(" and PO.PositionID='" + PositionID + "'")
            End If

            If isNull("WorkType") Then
                strSQL.AppendLine(" and W.WorkTypeID='" + WorkType + "'")
            End If

            If isNull("OrganID") Then
                strSQL.AppendLine(" and ORT.OrganID='" + OrganID + "'")
            End If

            If isNull("DeptID") Then
                strSQL.AppendLine(" and ORT1.OrganID='" + DeptID + "'")
            End If

            If isNull("OrgType") Then
                strSQL.AppendLine(" and ORT2.OrganID='" + OrgType + "'")
            End If
            If isNull("ProcessDate") Then
                If "1".Equals(ProcessDate.Trim) Then
                    strSQL.AppendLine(" and OVD.ToOverTimeFlag='1'")
                ElseIf "2".Equals(ProcessDate.Trim) Then
                    strSQL.AppendLine(" and OVD.ToOverTimeFlag='0'")
                Else

                End If
            End If
            strSQL.Append(" order by OTEmpID,OTStartDate,OTStartTime")
        End If
        Return strSQL.ToString

    End Function

    Public Function getStrSql() As String
        Dim strSQL As New StringBuilder
        If Type.Equals("bef") Then
            strSQL.AppendLine(" select PO.PositionID,PO.Remark,ORT2.OrganName AS DeptID,ORT1.OrganName AS DeptID1, ORT.OrganName AS OrganID,P.EmpID AS EmpID,P.Name AS Name,OVA.OTStartDate+'~'+OVA.OTEndDate AS OTDate,OVA.OTStartTime+'~'+OVA.OTEndTime AS OTTime, AT.CodeCName AS OTType,OVA.OTStatus  AS OTStatus,OVA.OTCompID AS OTCompID,OVA.OTEmpID AS OTEmpID,OVA.OTStartDate AS OTStartDate,OVA.OTEndDate AS OTEndDate,OVA.OTSeq AS OTSeq,OVA.OTTxnID AS 'OTTxnID'")
            strSQL.AppendLine(" from OverTimeAdvance OVA")
            strSQL.AppendLine(" LEFT JOIN " + eHRMSDB_ITRD + ".[dbo].Personal  P on OVA.OTCompID= P.CompID and OVA.OTEmpID = P.EmpID")
            strSQL.AppendLine(" left join AT_CodeMap AT on OVA.OTTypeID=AT.Code and AT.TabName='OverTime' and AT.FldName='OverTimeType' ")
            strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].WorkStatus  WS ON P.WorkStatus= WS.WorkCode")
            strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Title  T ON P.CompID=T.CompID and P.RankID=T.RankID and P.TitleID=T.TitleID")
            strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Company C ON P.CompID=C.CompID")
            strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].EmpPosition EP ON P.CompID=EP.CompID AND P.EmpID=EP.EmpID AND EP.PrincipalFlag='1'")
            strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Position PO ON EP.PositionID=PO.PositionID")
            strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].EmpWorkType EWT ON P.CompID=EWT.CompID AND P.EmpID=EWT.EmpID and EWT.PrincipalFlag='1'")
            strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].WorkType W ON EWT.WorkTypeID= W.WorkTypeID  AND P.CompID=W.CompID")
            strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Organization ORT ON  P.OrganID=ORT.OrganID AND P.CompID =ORT.CompID")
            strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Organization ORT1 ON P.DeptID=ORT1.OrganID")
            strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Organization ORT2 ON ORT.OrgType=ORT2.OrganID")
            '20170304kevin 
            strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].RankMapping RM on P.RankID=RM.RankID  AND RM.CompID=OVA.OTCompID")
            'strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].RankMapping TRM on T.RankID=TRM.RankID AND T.CompID=TRM.CompID")
            'strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Rank R on P.RankID=R.RankID")


            strSQL.AppendLine(" Where 1=1")
            strSQL.AppendLine(" And OVA.OTCompID = '" + UserProfile.SelectCompRoleID + "'") '20170219 Beatrice Add

            If isNull("OTSalaryOrAdjust") Then
                strSQL.AppendLine(" and OVA.SalaryOrAdjust=" + Bsp.Utility.Quote(OTSalaryOrAdjust))
            End If

            If isNull("OTEmpName") Then
                strSQL.AppendLine(" and P.Name=" + Bsp.Utility.Quote(OTEmpName))
            End If

            If isNull("OvertimeDateB") Then
                strSQL.AppendLine(" and OVA.OTStartDate>='" + OvertimeDateB + "'")
            End If
            If isNull("OvertimeDateE") Then
                strSQL.AppendLine(" and OVA.OTEndDate<='" + OvertimeDateE + "'")
            End If

            If isNull("OTStartTime") Then
                strSQL.AppendLine(" and (Cast(OVA.OTStartTime as int)>=Cast('" + OTStartTime + "' as int))")
            End If
            If isNull("OTEndTime") Then
                strSQL.AppendLine(" and (Cast(OVA.OTEndTime as int)<=Cast('" + OTEndTime + "' as int))")
            End If

            If isNull("OTStatus") Then
                strSQL.AppendLine("and OVA.OTStatus='" + OTStatus + "'")
            End If

            If isNull("DateOfApprovalB") Then
                strSQL.AppendLine(" and not REPLACE (OVA.OTValidID,' ','') =''")
                strSQL.AppendLine(" and OVA.OTValidDate>='" + DateOfApprovalB + "'")
            End If

            If isNull("DateOfApprovalE") Then
                strSQL.AppendLine(" and OVA.OTValidDate<='" + DateOfApprovalE + " 23:59:59'")
                strSQL.AppendLine(" and not REPLACE (OVA.OTValidID,' ','') =''")
            End If

            If isNull("DateOfApplicationB") Then
                strSQL.AppendLine(" and OVA.OTRegisterDate>='" + DateOfApplicationB + "'")
            End If

            If isNull("DateOfApplicationE") Then
                strSQL.AppendLine("and OVA.OTRegisterDate<='" + DateOfApplicationE + " 23:59:59'")
            End If
            If isNull("HolidayOrNot") Then
                strSQL.AppendLine(" and OVA.HolidayOrNot='" + HolidayOrNot + "'")
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

            If isNull("TakeOfficeDateB") Then
                strSQL.AppendLine(" and P.EmpDate>='" + TakeOfficeDateB + "'")
            End If
            If isNull("TakeOfficeDateE") Then
                strSQL.AppendLine(" and P.EmpDate<='" + TakeOfficeDateE + " 23:59:59'")
            End If
            If isNull("LeaveOfficeDateB") Then
                strSQL.AppendLine(" and P.QuitDate>='" + LeaveOfficeDateB + "'")
            End If
            If isNull("LeaveOfficeDateE") Then
                strSQL.AppendLine(" and P.QuitDate<='" + LeaveOfficeDateE + " 23:59:59'")
                strSQL.AppendLine("and (P.WorkStatus='3' or P.WorkStatus='6')")
            End If
            If isNull("Sex") Then
                strSQL.AppendLine(" and P.Sex='" + Sex + "'")
            End If


            If isNull("OTTypeID") Then
                strSQL.AppendLine(" and OVA.OTTypeID='" + OTTypeID + "'")
            End If





            '20170219 Beatrice mod
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

            If isNull("WorkType") Then
                strSQL.AppendLine(" and W.WorkTypeID='" + WorkType + "'")
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


        Else

            strSQL.AppendLine(" select PO.PositionID,PO.Remark,ORT2.OrganName AS DeptID,PO.Remark,ORT1.OrganName AS DeptID1, ORT.OrganName AS OrganID,P.EmpID AS EmpID,P.Name AS Name,OVD.OTStartDate+'~'+OVD.OTEndDate AS OTDate,OVD.OTStartTime+'~'+OVD.OTEndTime AS OTTime, AT.CodeCName AS OTType,OVD.OTStatus  AS OTStatus,OVD.OTCompID AS OTCompID,OVD.OTEmpID AS OTEmpID,OVD.OTStartDate AS OTStartDate,OVD.OTEndDate AS OTEndDate,OVD.OTSeq AS OTSeq,OVD.ToOverTimeFlag AS 'ToOverTimeFlag'")
            strSQL.AppendLine(" ,OVD.OTStartTime as OTStartTime,OVD.OTEndTime as OTEndTime,OVD.OTTxnID as 'OTTxnID',OVD.OTSeqNo as OTSeqNo,OVD.MealFlag as MealFlag,OVD.MealTime as MealTime,OVD.OTSalaryPaid as OTSalaryPaid,OVD.HolidayOrNot as 'HolidayOrNot',OVD.FlowCaseID AS 'FlowCaseID',OVD.OTFromAdvanceTxnId AS 'OTFromAdvanceTxnId'")
            strSQL.AppendLine(" from OverTimeDeclaration OVD")
            strSQL.AppendLine(" LEFT JOIN " + eHRMSDB_ITRD + ".[dbo].Personal  P on OVD.OTCompID= P.CompID and OVD.OTEmpID = P.EmpID")
            strSQL.AppendLine(" left join AT_CodeMap AT on OVD.OTTypeID=AT.Code and AT.TabName='OverTime' and AT.FldName='OverTimeType' ")
            strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].WorkStatus  WS ON P.WorkStatus= WS.WorkCode")
            strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Title  T ON P.CompID=T.CompID and P.RankID=T.RankID and P.TitleID=T.TitleID")
            strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Company C ON P.CompID=C.CompID")
            strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].EmpPosition EP ON P.CompID=EP.CompID AND P.EmpID=EP.EmpID AND EP.PrincipalFlag='1'")
            strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Position PO ON EP.PositionID=PO.PositionID")
            strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].EmpWorkType EWT ON P.CompID=EWT.CompID AND P.EmpID=EWT.EmpID and EWT.PrincipalFlag='1'")
            strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].WorkType W ON EWT.WorkTypeID= W.WorkTypeID  AND P.CompID=W.CompID")
            strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Organization ORT ON  P.OrganID=ORT.OrganID AND P.CompID =ORT.CompID")
            strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Organization ORT1 ON P.DeptID=ORT1.OrganID")
            strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Organization ORT2 ON ORT.OrgType=ORT2.OrganID")
            strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].RankMapping RM on P.RankID=RM.RankID  AND RM.CompID=OVD.OTCompID")
            'strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].RankMapping RM on P.RankID=RM.RankID")
            'strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].RankMapping TRM on T.RankID=RM.RankID")
            strSQL.AppendLine(" Where 1=1")
            strSQL.AppendLine(" And OVD.OTCompID = '" + UserProfile.SelectCompRoleID + "'") '20170219 Beatrice Add

            If isNull("OTSalaryOrAdjust") Then
                strSQL.AppendLine(" and OVD.SalaryOrAdjust=" + Bsp.Utility.Quote(OTSalaryOrAdjust))
            End If

            If isNull("OTEmpName") Then
                strSQL.AppendLine(" and P.Name=" + Bsp.Utility.Quote(OTEmpName))
            End If

            If isNull("OvertimeDateB") Then
                strSQL.AppendLine(" and OVD.OTStartDate>='" + OvertimeDateB + "'")
            End If
            If isNull("OvertimeDateE") Then
                strSQL.AppendLine(" and OVD.OTEndDate<='" + OvertimeDateE + "'")
            End If

            If isNull("OTStartTime") Then
                strSQL.AppendLine(" and (Cast(OVD.OTStartTime as int)>=Cast('" + OTStartTime + "' as int))")
            End If
            If isNull("OTEndTime") Then
                strSQL.AppendLine(" and (Cast(OVD.OTEndTime as int)<=Cast('" + OTEndTime + "' as int))")
            End If

            If isNull("OTPayDate") Then
                strSQL.AppendLine("and CONVERT(varchar, OVD.OTPayDate)='" + OTPayDate + "'")
            End If

            If isNull("OTStatus") Then
                strSQL.AppendLine("and OVD.OTStatus='" + OTStatus + "'")
            End If

            If isNull("DateOfApprovalB") Then
                strSQL.AppendLine(" and not REPLACE (OVD.OTValidID,' ','') =''")
                strSQL.AppendLine(" and OVD.OTValidDate>='" + DateOfApprovalB + "'")
            End If

            If isNull("DateOfApprovalE") Then
                strSQL.AppendLine("and OVD.OTValidDate<='" + DateOfApprovalE + " 23:59:59'")
                strSQL.AppendLine(" and not REPLACE (OVD.OTValidID,' ','') =''")
            End If

            If isNull("DateOfApplicationB") Then
                strSQL.AppendLine(" and OVD.OTRegisterDate>='" + DateOfApplicationB + "'")
            End If

            If isNull("DateOfApplicationE") Then
                strSQL.AppendLine("and OVD.OTRegisterDate<='" + DateOfApplicationE + "  23:59:59'")
            End If
            If isNull("HolidayOrNot") Then
                strSQL.AppendLine(" and OVD.HolidayOrNot='" + HolidayOrNot + "'")
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

            If isNull("TakeOfficeDateB") Then
                strSQL.AppendLine(" and P.EmpDate>='" + TakeOfficeDateB + "'")
            End If
            If isNull("TakeOfficeDateE") Then
                strSQL.AppendLine(" and P.EmpDate<='" + TakeOfficeDateE + " 23:59:59'")
            End If
            If isNull("LeaveOfficeDateB") Then
                strSQL.AppendLine(" and P.QuitDate>='" + LeaveOfficeDateB + "'")
            End If
            If isNull("LeaveOfficeDateE") Then
                strSQL.AppendLine(" and P.QuitDate<='" + LeaveOfficeDateE + " 23:59:59'")
                strSQL.AppendLine("and (P.WorkStatus='3' or P.WorkStatus='6')")
            End If
            If isNull("Sex") Then
                strSQL.AppendLine(" and P.Sex='" + Sex + "'")
            End If


            If isNull("OTTypeID") Then
                strSQL.AppendLine(" and OVD.OTTypeID='" + OTTypeID + "'")
            End If




            '20170304 kevin mod
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

            ''20170219 Beatrice mod
            'If isNull("RankIDMIN") Then
            '    If isNull("TitleIDMIN") Then
            '        strSQL.AppendLine(" and T.RankID + T.TitleID>='" + RankIDMIN + TitleIDMIN + "'")
            '    Else
            '        strSQL.AppendLine(" and T.RankID>='" + RankIDMIN + "'")
            '    End If
            'End If
            'If isNull("RankIDMAX") Then
            '    If isNull("TitleIDMAX") Then
            '        strSQL.AppendLine(" and T.RankID + T.TitleID<='" + RankIDMAX + TitleIDMAX + "'")
            '    Else
            '        strSQL.AppendLine(" and T.RankID<='" + RankIDMAX + "'")
            '    End If
            'End If
            ''未確認
            'If isNull("TitleID") Then
            '    strSQL.AppendLine(" and T.TitleID='" + TitleID + "'")
            '    strSQL.AppendLine(" and T.TitleName='" + TitleName + "'")
            'End If


            If isNull("PositionID") Then
                strSQL.AppendLine(" and PO.PositionID='" + PositionID + "'")
            End If

            If isNull("WorkType") Then
                strSQL.AppendLine(" and W.WorkTypeID='" + WorkType + "'")
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
            If isNull("OTSalaryPaid") Then
                strSQL.AppendLine(" and OVD.OTSalaryPaid='" + OTSalaryPaid + "'")
            End If


            If isNull("ProcessDate") Then
                If "1".Equals(ProcessDate.Trim) Then
                    strSQL.AppendLine(" and OVD.ToOverTimeFlag='1'")
                ElseIf "2".Equals(ProcessDate.Trim) Then
                    strSQL.AppendLine(" and OVD.ToOverTimeFlag='0'")
                Else

                End If
            End If
            strSQL.Append(" order by OTEmpID,OTStartDate,OTStartTime")
        End If
        Return strSQL.ToString

    End Function
    Private Function getOV4001DataTableSql(ByVal OTCompID As String, ByVal OTEmpID As String, ByVal OTStartDate As String, ByVal OTEndDate As String, ByVal OTSeq As String, ByVal OTTxnID As String) As String
        Dim strSQL As New StringBuilder

        If Type.Equals("bef") Then
            strSQL.AppendLine("select P.CompID+'-'+C.CompName AS 'labCompID'")
            strSQL.AppendLine(",ORT2.OrganName +'/'+ORT1.OrganName AS 'labDeptID',ORT.OrganName AS 'labOrganID',P.EmpID+'-'+P.Name AS 'labOTEmpName'")
            strSQL.AppendLine(" ,OVA.OTCompID AS OTCompID,OVA.OTEmpID AS OTEmpID,OVA.OTStartDate AS OTStartDate,OVA.OTEndDate AS OTEndDate,OVA.OTTxnID as OTTxnID,OVA.OTSeqNo as OTSeqNo,OVA.OTStatus AS OTStatus,OVA.OTStatus as OTSalaryPaid,OVA.OTValidID AS 'OTValidID'")
            strSQL.AppendLine(",OVA.SalaryOrAdjust AS 'labSalaryOrAdjust'")
            strSQL.AppendLine(",OVA.AdjustInvalidDate AS 'labAdjustInvalidDate'")
            strSQL.AppendLine(",OVA.OTStartDate+'~'+OVA.OTEndDate AS 'labOverTimeDate'")
            strSQL.AppendLine(",OVA.OTStartTime AS 'labOTStartDate'")
            strSQL.AppendLine(",OVA.OTEndTime AS 'labOTEndDate'")
            strSQL.AppendLine(",OVA.MealFlag AS 'cbMealFlag'")
            strSQL.AppendLine(",OVA.MealTime AS 'labMealTime'")
            strSQL.AppendLine(",AT.CodeCName AS 'labOTTypeID'")
            strSQL.AppendLine(",OVA.OTReasonMemo AS 'labOTReasonID'")
            strSQL.AppendLine(",OVA.OTRegisterID+'-'+P1.Name  AS 'labOTRegisterID'")
            strSQL.AppendLine(",OVA.LastChgID+'-'+P2.Name  AS 'LastChgIDandName'")
            strSQL.AppendLine(",OVA.OTAttachment AS 'labOTAttachment'")
            strSQL.AppendLine(",WS.Remark AS 'labWorkStatus'")
            strSQL.AppendLine(",W.Remark AS 'labWorkType'")
            strSQL.AppendLine(",P.Sex AS 'labSex'")
            strSQL.AppendLine(",P.RankID AS 'labRankID'")
            strSQL.AppendLine(",T.TitleName AS 'labTitleName'")
            strSQL.AppendLine(",PO.Remark AS 'labPositionID'")
            strSQL.AppendLine(",OVA.HolidayOrNot AS 'HolidayOrNot'")
            strSQL.AppendLine(",OVA.OTStatus AS 'labOTStatus'")
            strSQL.AppendLine(",OVA.OTFormNO AS 'labOTFormNO'")
            strSQL.AppendLine(",P.EmpDate AS 'labTakeOfficeDate'")
            strSQL.AppendLine(",P.QuitDate AS 'labLeaveOfficeDate'")
            strSQL.AppendLine(",OVA.OTValidDate AS 'labDateOfApproval'")
            strSQL.AppendLine(",OVA.OTRegisterDate AS 'labDateOfApplication'")
            strSQL.AppendLine(",OVA.LastChgComp AS 'LastChgComp'")
            strSQL.AppendLine(",OVA.LastChgID AS 'labLastChgID'")
            strSQL.AppendLine(",OVA.LastChgDate AS 'labLastChgDate'")
            strSQL.AppendLine("from OverTimeAdvance OVA")

            strSQL.AppendLine("LEFT JOIN " + eHRMSDB_ITRD + ".[dbo].Personal  P on OVA.OTCompID= P.CompID and OVA.OTEmpID = P.EmpID")
            strSQL.AppendLine("LEFT JOIN " + eHRMSDB_ITRD + ".[dbo].Personal  P1 on  OVA.OTRegisterID = P1.EmpID   AND OVA.OTRegisterComp= P1.CompID ")
            strSQL.AppendLine("LEFT JOIN " + eHRMSDB_ITRD + ".[dbo].Personal  P2 on  OVA.LastChgID = P2.EmpID  AND OVA.LastChgComp= P2.CompID ")
            strSQL.AppendLine("left join AT_CodeMap AT on OVA.OTTypeID=AT.Code and AT.TabName='OverTime' and AT.FldName='OverTimeType'") '

            strSQL.AppendLine("left join " + eHRMSDB_ITRD + ".[dbo].WorkStatus  WS ON P.WorkStatus= WS.WorkCode")
            strSQL.AppendLine("left join " + eHRMSDB_ITRD + ".[dbo].Title  T ON P.CompID=T.CompID and P.RankID=T.RankID and P.TitleID=T.TitleID")
            strSQL.AppendLine("left join " + eHRMSDB_ITRD + ".[dbo].Company C ON P.CompID=C.CompID")
            strSQL.AppendLine("left join " + eHRMSDB_ITRD + ".[dbo].EmpPosition EP ON P.CompID=EP.CompID AND P.EmpID=EP.EmpID AND EP.PrincipalFlag='1'")
            strSQL.AppendLine("left join " + eHRMSDB_ITRD + ".[dbo].Position PO ON EP.PositionID=PO.PositionID")
            strSQL.AppendLine("left join " + eHRMSDB_ITRD + ".[dbo].EmpWorkType EWT ON P.CompID=EWT.CompID AND P.EmpID=EWT.EmpID and EWT.PrincipalFlag='1'")
            strSQL.AppendLine("left join " + eHRMSDB_ITRD + ".[dbo].WorkType W ON EWT.WorkTypeID= W.WorkTypeID  AND P.CompID=W.CompID")
            strSQL.AppendLine("left join " + eHRMSDB_ITRD + ".[dbo].Organization ORT ON  P.OrganID=ORT.OrganID AND P.CompID =ORT.CompID")
            strSQL.AppendLine("left join " + eHRMSDB_ITRD + ".[dbo].Organization ORT1 ON P.DeptID=ORT1.OrganID")
            strSQL.AppendLine("left join " + eHRMSDB_ITRD + ".[dbo].Organization ORT2 ON ORT.OrgType=ORT2.OrganID")
            strSQL.AppendLine("Where 1=1")
            strSQL.AppendLine("and OVA.OTCompID='" + OTCompID + "' and OVA.OTEmpID='" + OTEmpID + "' and OVA.OTTxnID='" + OTTxnID + "'")
            strSQL.Append(" order by OTEmpID,OTStartDate,OTStartTime")
        Else
            strSQL.AppendLine(" select P.CompID+'-'+C.CompName AS 'labCompID',OVD.OTValidID AS 'OTValidID',OVD.ToOverTimeDate AS 'ToOverTimeDate',OVD.ToOverTimeFlag as 'ToOverTimeFlag'")
            strSQL.AppendLine(" ,ORT2.OrganName +'/'+ORT1.OrganName AS 'labDeptID',ORT.OrganName AS 'labOrganID',P.EmpID+'-'+P.Name AS 'labOTEmpName'")
            strSQL.AppendLine(" ,OVD.SalaryOrAdjust AS 'labSalaryOrAdjust'")
            strSQL.AppendLine(" ,OVD.AdjustInvalidDate AS 'labAdjustInvalidDate'")
            strSQL.AppendLine(" ,OVD.OTStartDate+'~'+OVD.OTEndDate AS 'labOverTimeDate'")
            strSQL.AppendLine(" ,OVD.OTStartTime AS 'labOTStartDate'")
            strSQL.AppendLine(" ,OVD.OTEndTime AS 'labOTEndDate'")
            strSQL.AppendLine(" ,OVD.MealFlag AS 'cbMealFlag'")
            strSQL.AppendLine(" ,OVD.MealTime AS 'labMealTime'")
            strSQL.AppendLine(" ,AT.CodeCName AS 'labOTTypeID'")
            strSQL.AppendLine(" ,OVD.OTReasonMemo AS 'labOTReasonID'")
            strSQL.AppendLine(" ,OVD.OTCompID AS OTCompID,OVD.OTEmpID AS OTEmpID,OVD.OTStartDate AS OTStartDate,OVD.OTEndDate AS OTEndDate,OVD.OTTxnID as OTTxnID,OVD.OTSeqNo as OTSeqNo,OVD.OTStatus AS OTStatus,OVD.OTSalaryPaid as OTSalaryPaid")
            strSQL.AppendLine(" ,OVD.OTRegisterID+'-'+P1.Name AS 'labOTRegisterID'")
            'strSQL.AppendLine(" ,P2.Name  AS 'labLastChgID'")
            strSQL.AppendLine(" ,OVD.OTAttachment AS 'labOTAttachment'")
            strSQL.AppendLine(" ,WS.Remark AS 'labWorkStatus'")
            strSQL.AppendLine(" ,W.Remark AS 'labWorkType'")
            strSQL.AppendLine(" ,P.Sex AS 'labSex'")
            strSQL.AppendLine(" ,P.RankID AS 'labRankID'")
            strSQL.AppendLine(" ,T.TitleName AS 'labTitleName'")
            strSQL.AppendLine(" ,PO.Remark AS 'labPositionID'")
            strSQL.AppendLine(" ,OVD.HolidayOrNot AS 'HolidayOrNot'")
            strSQL.AppendLine(" ,OVD.OTPayDate AS 'labOTPayDate'")
            strSQL.AppendLine(" ,OVD.OTStatus AS 'labOTStatus'")
            strSQL.AppendLine(" ,OVD.OTFormNO AS 'labOTFormNO'")
            strSQL.AppendLine(" ,P.EmpDate AS 'labTakeOfficeDate'")
            strSQL.AppendLine(" ,P.QuitDate AS 'labLeaveOfficeDate'")
            strSQL.AppendLine(" ,OVD.OTValidDate AS 'labDateOfApproval'")
            strSQL.AppendLine(" ,OVD.OTRegisterDate AS 'labDateOfApplication'")
            strSQL.AppendLine(" ,OVD.LastChgComp AS 'LastChgComp'")
            strSQL.AppendLine(" ,OVD.LastChgID AS 'labLastChgID'")
            strSQL.AppendLine(" ,OVD.LastChgDate AS 'labLastChgDate'")
            strSQL.AppendLine("from OverTimeDeclaration OVD")
            strSQL.AppendLine(" LEFT JOIN " + eHRMSDB_ITRD + ".[dbo].Personal  P on OVD.OTCompID= P.CompID and OVD.OTEmpID = P.EmpID")
            strSQL.AppendLine(" LEFT JOIN " + eHRMSDB_ITRD + ".[dbo].Personal  P1 on  OVD.OTRegisterID = P1.EmpID   AND OVD.OTRegisterComp= P1.CompID ")
            strSQL.AppendLine(" LEFT JOIN " + eHRMSDB_ITRD + ".[dbo].Personal  P2 on  OVD.LastChgID = P2.EmpID   AND OVD.OTCompID= P2.CompID ")
            strSQL.AppendLine("left join AT_CodeMap AT on OVD.OTTypeID=AT.Code and AT.TabName='OverTime' and AT.FldName='OverTimeType'") '
            strSQL.AppendLine("left join " + eHRMSDB_ITRD + ".[dbo].WorkStatus  WS ON P.WorkStatus= WS.WorkCode")
            strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Title  T ON P.CompID=T.CompID and P.RankID=T.RankID and P.TitleID=T.TitleID")
            strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Company C ON P.CompID=C.CompID")
            strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].EmpPosition EP ON P.CompID=EP.CompID AND P.EmpID=EP.EmpID AND EP.PrincipalFlag='1'")
            strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Position PO ON EP.PositionID=PO.PositionID")
            strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].EmpWorkType EWT ON P.CompID=EWT.CompID AND P.EmpID=EWT.EmpID and EWT.PrincipalFlag='1'")
            strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].WorkType W ON EWT.WorkTypeID= W.WorkTypeID  AND P.CompID=W.CompID")
            strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Organization ORT ON  P.OrganID=ORT.OrganID AND P.CompID =ORT.CompID")
            strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Organization ORT1 ON P.DeptID=ORT1.OrganID")
            strSQL.AppendLine(" left join " + eHRMSDB_ITRD + ".[dbo].Organization ORT2 ON ORT.OrgType=ORT2.OrganID")
            strSQL.AppendLine("Where 1=1")
            strSQL.AppendLine("and OVD.OTCompID='" + OTCompID + "' and OVD.OTEmpID='" + OTEmpID + "' and OVD.OTTxnID='" + OTTxnID + "'")
            strSQL.Append(" order by OTEmpID,OTStartDate,OTStartTime")


        End If


        Return strSQL.ToString
    End Function
    Public Function getSumDataTableSql(ByVal OTCompID As String, ByVal OTEmpID As String, ByVal lastDate As String, ByVal firstDate As String) As String
        Dim strSQL As New StringBuilder

        If Type.Equals("bef") Then
            strSQL.AppendLine("select  OVA.OTStartDate as 'OTStartDate',OVA.OTEndDate as 'OTEndDate',OVA.OTStartTime as 'OTStartTime',OVA.OTEndTime as 'OTEndTime',OVA.OTStatus as 'OTStatus',OVA.OTTotalTime as 'OTTotalTime' ,OVA.MealTime AS 'MealTime' ,OVA.MealFlag AS 'MealFlag',OVA.OTTxnID AS 'OTTxnID',OVA.OTSeqNo AS 'OTSeqNo'")
            strSQL.AppendLine("from OverTimeAdvance OVA")
            strSQL.AppendLine(" Where 1=1 ")
            strSQL.AppendLine("and OVA.OTCompID='" + OTCompID + "' and OVA.OTEmpID='" + OTEmpID + "' and OVA.OTStartDate<='" + lastDate + "' and OVA.OTStartDate>='" + firstDate + "'")

        Else
            strSQL.AppendLine("select  OVD.OTStartDate as 'OTStartDate',OVD.OTEndDate as 'OTEndDate',OVD.OTStartTime as 'OTStartTime',OVD.OTEndTime as 'OTEndTime',OVD.OTStatus as 'OTStatus',OVD.OTTotalTime as 'OTTotalTime',OVD.MealTime AS 'MealTime' ,OVD.MealFlag AS 'MealFlag',OVD.OTTxnID AS 'OTTxnID',OVD.OTSeqNo AS 'OTSeqNo'")
            strSQL.AppendLine("from OverTimeDeclaration OVD")
            strSQL.AppendLine(" Where 1=1 ")
            strSQL.AppendLine("and OVD.OTCompID='" + OTCompID + "' and OVD.OTEmpID='" + OTEmpID + "' and OVD.OTStartDate<='" + lastDate + "' and OVD.OTStartDate>='" + firstDate + "'")
        End If
        Return strSQL.ToString


    End Function
#End Region

#Region "日期計算"
    Public Function getLastDay(ByVal yyyy As String, ByVal MM As String) As String
        Dim lastDay As String = (DateTime.DaysInMonth(CInt(yyyy), CInt(MM))).ToString
        Return lastDay
    End Function
    Public Function getLastDate(ByVal yyyy As String, ByVal MM As String) As String
        Dim lastDay As String = (DateTime.DaysInMonth(CInt(yyyy), CInt(MM))).ToString
        Dim lastDate As String = yyyy + "/" + MM + "/" + lastDay
        Return lastDate
    End Function
    Public Function getFirstDate(ByVal yyyy As String, ByVal MM As String) As String
        Dim firstDate As String = yyyy + "/" + MM + "/01"
        Return firstDate
    End Function
#End Region

#Region "取DataTable function"

    Public Function getOV4001SumDataTable(ByVal OTCompID As String, ByVal OTEmpID As String, ByVal OTStartDate As String) As DataTable
        Dim yyyy As String = OTStartDate.Split("/")(0)
        Dim MM As String = OTStartDate.Split("/")(1)
        Dim dateTime As DateTime = New DateTime
        Dim lastDate As String = getLastDate(yyyy, MM)
        Dim firstDate As String = getFirstDate(yyyy, MM)
        Dim strSQL = getSumDataTableSql(OTCompID, OTEmpID, lastDate, firstDate)
        Dim dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL, "AattendantDB").Tables(0)
        dt = SingleDataTable(dt)
        Return dt
    End Function

    Public Function getTable() As DataTable
        Dim strSQL = getStrSql()
        Dim dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL, "AattendantDB").Tables(0)
        Dim dt3 As DataTable = New DataTable()
        If Type.Equals("after") Then
            dt = getTimeIntervalAdapter(dt, getBefColumnNameList())
        End If
        dt = changeTableNumToName(dt)
        'sort
        dt3 = dt.Clone()
        For Each dr As DataRow In dt.Select("'1'='1' ", "OTStartDate DESC")
            dt3.ImportRow(dr)
        Next
        dt.Clear()


        'dt.DefaultView.Sort = "OTStartDate DESC"
        Return dt3
    End Function

    Public Function getOV4001DataTable(ByVal OTCompID As String, ByVal OTEmpID As String, ByVal OTStartDate As String, ByVal OTEndDate As String, ByVal OTSeq As String, ByVal OTTxnID As String) As DataTable
        Dim strSQL = getOV4001DataTableSql(OTCompID, OTEmpID, OTStartDate, OTEndDate, OTSeq, OTTxnID)
        Dim dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL, "AattendantDB").Tables(0)
        dt = getTimeIntervalAdapterFor4001(dt, getBefColumnName4001List())
        dt = changeTableNimToNameForDetial(dt)
        '目前得到時段
        Return dt
    End Function


    Public Function getSameDataForOTTxnID(ByVal dt As DataTable, ByVal OTTxnID As String) As DataRow()
        '移除表單一樣的
        Dim rows As DataRow() = dt.Select(" OTTxnID='" + OTTxnID + "'")

        Return rows
    End Function


    Function getSumOTTime(ByVal OverTimeSumObjectList As ArrayList) As String
        Dim sumOTTime As Double = 0

        If OverTimeSumObjectList.Count > 0 Then
            For Each OverTimeSumObject As OV_3 In OverTimeSumObjectList
                Dim MealTime As String = OverTimeSumObject.mealTime
                Dim OTTotalTime As String = OverTimeSumObject.Time
                Dim MealFlag As String = OverTimeSumObject.mealFlag
                If "1".Equals(MealFlag) Then
                    If IsNumeric(MealTime) Then
                        sumOTTime = sumOTTime + CDbl(FormatNumber((Convert.ToDouble(OTTotalTime) - Convert.ToDouble(MealTime)) / 60, 1))
                    End If
                Else
                    sumOTTime = sumOTTime + CDbl(FormatNumber(Convert.ToDouble(OTTotalTime) / 60, 1))
                End If

            Next
        End If
        'sumOTTime = CDbl(FormatNumber((Convert.ToDouble(sumOTTime) / 60), 1))
        Return sumOTTime
    End Function


    Public Function getFileName(ByVal pk As String) As String
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine("Select * From [dbo].[AttachInfo] Where AttachID = '" + pk + "'")
        Dim dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "AattendantDB").Tables(0)
        Dim FILEBODY As Object = Bsp.DB.ExecuteScalar(strSQL.ToString, "AattendantDB")
        Dim dtCloned As DataTable = dt.Clone()
        Dim FileName As String = ""
        Dim fileSize As Integer

        Dim buffer As Byte()
        If dt.Rows.Count > 0 Then
            For Each item As DataRow In dt.Rows
                fileSize = item("FileSize")
                If fileSize > 0 Then
                    FileName = item("FileName")
                End If
            Next
        End If
        Return FileName
    End Function

    Public Function getOV4002ForTimeDataTable(ByVal OTCompID As String, ByVal OTEmpID As String, ByVal OTStartDate As String, ByVal OTEndDate As String, ByVal OTStartTime As String, ByVal OTEndTime As String, ByVal OTSeq As String, ByVal OTTxnID As String, ByVal mealFlag As String, ByVal mealTime As String) As DataTable
        Dim strSQL = getOV4001DataTableSql(OTCompID, OTEmpID, OTStartDate, OTEndDate, OTSeq, OTTxnID)
        Dim dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL, "AattendantDB").Tables(0)
        Dim rows As DataRow() = getSameDataForOTTxnID(dt, OTTxnID)
        Dim row1 As DataRow = dt.NewRow()

        row1.ItemArray = TryCast(rows(0).ItemArray.Clone(), Object())

        Dim row2 = dt.NewRow()
        row2.ItemArray = TryCast(rows(0).ItemArray.Clone(), Object())

        For i = 0 To rows.Count - 1
            dt.Rows.Remove(rows(i))
        Next

        '檢查使用者所填的資料
        '如果有跨日要拆單
        If OTStartDate.Equals(OTEndDate) Then
            Dim StartDate1 As String = OTStartDate
            Dim EndDate1 As String = OTEndDate
            Dim AAA = Me.CheckHolidayOrNot(StartDate1)


            Dim HolidayOrNot1 As String = If(Me.CheckHolidayOrNot(StartDate1), "1", "0")
            Dim StartTime1 As String = OTStartTime
            Dim EndTime1 As String = OTEndTime



            '更改row 須注意 
            '一. OTSeq 序號。加班人於同一加班日之第N張申請單，從1開始 因為只是計算時段且不放入資料庫 所以不管OTSeq 統一塞98 99
            row1.Item("OTStartDate") = StartDate1
            row1.Item("OTEndDate") = EndDate1
            row1.Item("labOTStartDate") = StartTime1
            row1.Item("labOTEndDate") = EndTime1
            row1.Item("OTSeqNo") = "1"
            row1.Item("HolidayOrNot") = HolidayOrNot1


            dt.Rows.Add(row1)
            dt = (From cust In dt.AsEnumerable() _
       Order By cust.Field(Of String)("OTEmpID") Descending, cust.Field(Of String)("OTStartDate"), cust.Field(Of String)("labOTStartDate")).CopyToDataTable()


            dt = getTimeIntervalAdapterFor4002Time(dt, getBefColumnName4001List(), OTCompID, OTEmpID, OTStartDate, OTEndDate, OTStartTime, OTEndTime, OTSeq, OTTxnID, mealFlag, mealTime)
            'dt = changeTableNimToNameForDetial(dt)

        Else
            Dim StartDate1 As String = OTStartDate
            Dim EndDate1 As String = OTStartDate
            Dim StartTime1 As String = OTStartTime
            Dim EndTime1 As String = "2359"

            Dim StartDate2 As String = OTEndDate
            Dim EndDate2 As String = OTEndDate
            Dim StartTime2 As String = "0000"
            Dim EndTime2 As String = OTEndTime

            Dim HolidayOrNot1 As String = If(Me.CheckHolidayOrNot(StartDate1), "1", "0")
            Dim HolidayOrNot2 As String = If(Me.CheckHolidayOrNot(StartDate2), "1", "0")

            '更改row 須注意 
            '一. OTSeq 序號。加班人於同一加班日之第N張申請單，從1開始 因為只是計算時段且不放入資料庫 所以不管OTSeq 統一塞98 99
            row1.Item("OTStartDate") = StartDate1
            row1.Item("OTEndDate") = EndDate1
            row1.Item("labOTStartDate") = StartTime1
            row1.Item("labOTEndDate") = EndTime1
            row1.Item("OTSeqNo") = "1"
            row1.Item("HolidayOrNot") = HolidayOrNot1


            row2.Item("OTStartDate") = StartDate2
            row2.Item("OTEndDate") = EndDate2
            row2.Item("labOTStartDate") = StartTime2
            row2.Item("labOTEndDate") = EndTime2
            row2.Item("OTSeqNo") = "2"
            row2.Item("HolidayOrNot") = HolidayOrNot2
            dt.Rows.Add(row1)
            dt.Rows.Add(row2)

            If dt.Rows.Count > 0 Then
                dt = (From cust In dt.AsEnumerable() _
Order By cust.Field(Of String)("OTEmpID") Descending, cust.Field(Of String)("OTStartDate"), cust.Field(Of String)("labOTStartDate")).CopyToDataTable()

            End If

            dt = getTimeIntervalAdapterFor4002Time(dt, getBefColumnName4001List(), OTCompID, OTEmpID, OTStartDate, OTEndDate, OTStartTime, OTEndTime, OTSeq, OTTxnID, mealFlag, mealTime)
            'dt = changeTableNimToNameForDetial(dt)

        End If

        Return dt

    End Function



    Public Function getOV4002labAdjustInvalidDate(ByVal OTCompID As String) As String

        Dim strSQL = "  Select Para FROM [dbo].[OverTimePara] where CompID='" + OTCompID + "'"
        Dim dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL, "AattendantDB").Tables(0)

        Dim jsonStr As String = dt.Rows(0).Item(0)

        Dim jsontb As DataTable = Json2DataTable(jsonStr)

        'Dim strSQL = getOV4001DataTableSql(OTCompID, OTEmpID, OTStartDate, OTEndDate, OTSeq, OTTxnID)
        'Dim dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL, "AattendantDB").Tables(0)
        'dt = getTimeIntervalAdapterFor4001(dt, getBefColumnName4001List())
        'dt = changeTableNimToNameForDetial(dt)
        '目前得到時段

        Return jsontb.Rows(0).Item("AdjustInvalidDate")
    End Function

#Region "Json"

    ''' <summary>
    ''' 暫存Json的Model
    ''' </summary>
    ''' <remarks></remarks>
    Public Class JsonOutputModel
        Public Property name As String
        Public Property results As String()
    End Class

    ''' <summary>
    ''' 接收Json格式資料，並轉為資料表
    ''' </summary>
    ''' <param name="jsonStr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Json2DataTable(ByVal jsonStr As String)
        Dim result As New DataTable()
        result = JsonConvert.DeserializeObject(Of DataTable)(jsonStr)
        Return result
    End Function
#End Region



    Public Function getDownLoadTable() As DataTable
        Dim strSQL = getStrSqlForDownLoan()
        Dim dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL, "AattendantDB").Tables(0)
        If "after".Equals(Type) Then
            dt = getTimeIntervalAdapter(dt, getStrSqlForDownLoanList())
            dt.Columns().Remove("OTCompID")
            dt.Columns().Remove("OTStartDate")
            dt.Columns().Remove("OTEndDate")
            dt.Columns().Remove("OTStartTime")
            dt.Columns().Remove("OTEndTime")
            'dt.Columns().Remove("OTTxnID")

            dt.Columns().Remove("MealFlag")
            'dt.Columns().Remove("MealTime")
            dt.Columns().Remove("OTStatus")
            dt.Columns().Remove("OTSalaryPaid")
            dt.Columns().Remove("HolidayOrNot1")
        End If

        dt = changeTableNumToNameForDownLoan(dt)


        Return dt
    End Function

#End Region

#Region "取轉換ColumnName List"

    Public Function getStrSqlForDownLoanList() As ArrayList

        '公司代碼-OTCompID、加班申請人-OTEmpID   、加班開始日期-OTStartDate、加班結束日期-OTEndDate、加班開始時間-OTStartTime、加班結束時間-OTEndTime、加班單號-OTxnID
        '單號序列-OTSeqNo 、用餐扣除註記-MealFlag、用餐時間-MealTime       、申報單狀態-OTStatus   、計薪註記-OTSalaryPaid   、假日註記-HolidayOrNot

        Dim BefColumnNameList As ArrayList = New ArrayList()
        'OVD.OTCompID AS OTCompID,OVD.OTEmpID AS OTEmpID,OVD.OTStartDate AS OTStartDate,OVD.OTEndDate AS OTEndDate,OVD.OTTxnID as OTTxnID,OVD.OTSeqNo as OTSeqNo,OVD.OTStatus AS OTStatus,OVD.OTSalaryPaid as OTSalaryPaid


        BefColumnNameList.Add("OTCompID") 'NO  OTCompID  OVD.OTCompID AS OTCompID,
        BefColumnNameList.Add("員工編號") 'NO OTEmpID      OVD.OTEmpID AS OTEmpID,
        BefColumnNameList.Add("OTStartDate") 'NO OTStartDate OVD.OTStartDate AS OTStartDate,
        BefColumnNameList.Add("OTEndDate") 'NO OTEndDate    OVD.OTEndDate AS OTEndDate,
        BefColumnNameList.Add("OTStartTime") '  
        BefColumnNameList.Add("OTEndTime") '  
        BefColumnNameList.Add("OTTxnID") ' no       OVD.OTTxnID as OTTxnID
        BefColumnNameList.Add("OTSeqNo") 'no        OVD.OTSeqNo as OTSeqNo
        BefColumnNameList.Add("MealFlag")
        BefColumnNameList.Add("MealTime")
        BefColumnNameList.Add("OTStatus") 'NO OTStatus OVD.OTStatus AS OTStatus
        BefColumnNameList.Add("OTSalaryPaid") ' no  OVD.OTSalaryPaid as OTSalaryPaid
        BefColumnNameList.Add("HolidayOrNot")
        Return BefColumnNameList

    End Function



    Public Function getBefColumnName4001List() As ArrayList

        '    strSQL.AppendLine(",ORT2.OrganName +'/'+ORT1.OrganName AS 'labDeptID',ORT.OrganName AS 'labOrganID',P.Name AS 'labOTEmpName'")
        'strSQL.AppendLine(",OVD.SalaryOrAdjust AS 'labSalaryOrAdjust'")
        'strSQL.AppendLine(",OVD.AdjustInvalidDate AS 'labAdjustInvalidDate'")
        'strSQL.AppendLine(",OVD.OTStartDate+'~'+OVD.OTEndDate AS 'labOverTimeDate'")
        'strSQL.AppendLine(",OVD.OTStartTime AS 'labOTStartDate'")
        'strSQL.AppendLine(",OVD.OTEndTime AS 'labOTEndDate'")
        'strSQL.AppendLine(",OVD.MealFlag AS 'cbMealFlag'")
        'strSQL.AppendLine(",OVD.MealTime AS 'labMealTime'")
        'strSQL.AppendLine(",OVT.OTTypeName AS 'labOTTypeID'")
        'strSQL.AppendLine(",OVD.OTReasonMemo AS 'labOTReasonID'")

        '公司代碼-OTCompID、加班申請人-OTEmpID   、加班開始日期-OTStartDate、加班結束日期-OTEndDate、加班開始時間-OTStartTime、加班結束時間-OTEndTime、加班單號-OTxnID
        '單號序列-OTSeqNo 、用餐扣除註記-MealFlag、用餐時間-MealTime       、申報單狀態-OTStatus   、計薪註記-OTSalaryPaid   、假日註記-HolidayOrNot

        Dim BefColumnNameList As ArrayList = New ArrayList()
        'OVD.OTCompID AS OTCompID,OVD.OTEmpID AS OTEmpID,OVD.OTStartDate AS OTStartDate,OVD.OTEndDate AS OTEndDate,OVD.OTTxnID as OTTxnID,OVD.OTSeqNo as OTSeqNo,OVD.OTStatus AS OTStatus,OVD.OTSalaryPaid as OTSalaryPaid

        BefColumnNameList.Add("OTCompID") 'NO  OTCompID  OVD.OTCompID AS OTCompID,
        BefColumnNameList.Add("OTEmpID") 'NO OTEmpID      OVD.OTEmpID AS OTEmpID,
        BefColumnNameList.Add("OTStartDate") 'NO OTStartDate OVD.OTStartDate AS OTStartDate,
        BefColumnNameList.Add("OTEndDate") 'NO OTEndDate    OVD.OTEndDate AS OTEndDate,
        BefColumnNameList.Add("labOTStartDate") '  
        BefColumnNameList.Add("labOTEndDate") '  
        BefColumnNameList.Add("OTTxnID") ' no       OVD.OTTxnID as OTTxnID
        BefColumnNameList.Add("OTSeqNo") 'no        OVD.OTSeqNo as OTSeqNo
        BefColumnNameList.Add("cbMealFlag")
        BefColumnNameList.Add("labMealTime")
        BefColumnNameList.Add("OTStatus") 'NO OTStatus OVD.OTStatus AS OTStatus
        BefColumnNameList.Add("OTSalaryPaid") ' no  OVD.OTSalaryPaid as OTSalaryPaid
        BefColumnNameList.Add("HolidayOrNot")
        Return BefColumnNameList

    End Function



    Public Function getBefColumnNameList() As ArrayList

        '公司代碼-OTCompID、加班申請人-OTEmpID   、加班開始日期-OTStartDate、加班結束日期-OTEndDate、加班開始時間-OTStartTime、加班結束時間-OTEndTime、加班單號-OTxnID
        '單號序列-OTSeqNo 、用餐扣除註記-MealFlag、用餐時間-MealTime       、申報單狀態-OTStatus   、計薪註記-OTSalaryPaid   、假日註記-HolidayOrNot

        Dim BefColumnNameList As ArrayList = New ArrayList()
        BefColumnNameList.Add("OTCompID") 'OTCompID 
        BefColumnNameList.Add("OTEmpID") 'OTCompID
        BefColumnNameList.Add("OTStartDate") 'OTStartDate
        BefColumnNameList.Add("OTEndDate") 'OTEndDate
        BefColumnNameList.Add("OTStartTime") 'no   OVD.OTStartTime as OTStartTime
        BefColumnNameList.Add("OTEndTime") 'no     OVD.OTEndTime as OTEndTime
        BefColumnNameList.Add("OTTxnID") ' no       OVD.OTTxnID as OTTxnID
        BefColumnNameList.Add("OTSeqNo") 'no        OVD.OTSeqNo as OTSeqNo
        BefColumnNameList.Add("MealFlag") 'no       OVD.MealFlag as MealFlag
        BefColumnNameList.Add("MealTime") 'no       OVD.MealTime as MealTime
        BefColumnNameList.Add("OTStatus") 'OTStatus
        BefColumnNameList.Add("OTSalaryPaid") ' no  OVD.OTSalaryPaid as OTSalaryPaid
        BefColumnNameList.Add("HolidayOrNot") ' no  OVD.HolidayOrNot as HolidayOrNot
        Return BefColumnNameList

    End Function

#End Region

#Region "更換DataTable 內容"

    Public Function changeTableNumToName(ByVal dt As DataTable) As DataTable
        If dt.Rows.Count > 0 Then
            For Each item As DataRow In dt.Rows
                If "bef".Equals(Type) Then
                    If (item("OTStatus").ToString()).Equals("1") Then
                        item("OTStatus") = "暫存"
                    ElseIf (item("OTStatus").ToString()).Equals("2") Then
                        item("OTStatus") = "送簽"
                    ElseIf (item("OTStatus").ToString()).Equals("3") Then
                        item("OTStatus") = "核准"
                    ElseIf (item("OTStatus").ToString()).Equals("4") Then
                        item("OTStatus") = "駁回"
                    ElseIf (item("OTStatus").ToString()).Equals("5") Then
                        item("OTStatus") = "刪除"
                    ElseIf (item("OTStatus").ToString()).Equals("9") Then
                        item("OTStatus") = "取消"
                    End If
                Else
                    If (item("OTStatus").ToString()).Equals("1") Then
                        item("OTStatus") = "暫存"
                    ElseIf (item("OTStatus").ToString()).Equals("2") Then
                        item("OTStatus") = "送簽"
                    ElseIf (item("OTStatus").ToString()).Equals("3") Then
                        item("OTStatus") = "核准"
                    ElseIf (item("OTStatus").ToString()).Equals("4") Then
                        item("OTStatus") = "駁回"
                    ElseIf (item("OTStatus").ToString()).Equals("5") Then
                        item("OTStatus") = "刪除"
                    ElseIf (item("OTStatus").ToString()).Equals("6") Then
                        item("OTStatus") = "取消"
                    ElseIf (item("OTStatus").ToString()).Equals("7") Then
                        item("OTStatus") = "作廢"
                    ElseIf (item("OTStatus").ToString()).Equals("9") Then
                        item("OTStatus") = "計薪後收回"
                    End If
                End If

                If ((Not (String.IsNullOrEmpty(item("OTTime")))) And (item("OTTime").ToString().Split("~").Count > 0)) Then
                    item("OTTime") = item("OTTime").ToString().Split("~")(0).Substring(0, 2) + ":" + item("OTTime").ToString().Split("~")(0).Substring(2, 2) _
                    + "~" + item("OTTime").ToString().Split("~")(1).Substring(0, 2) + ":" + item("OTTime").ToString().Split("~")(1).Substring(2, 2)
                End If

            Next

        End If
        Return dt
    End Function
    Private Function changeTableNimToNameForDetial(ByVal dt As DataTable) As DataTable
        Dim dtCloned As DataTable = dt.Clone()
        dtCloned.Columns.Add("labIsProcessDate", GetType(String))
        dtCloned.Columns.Add("labHolidayOrNot", GetType(String))


        If dt.Rows.Count > 0 Then
            For Each item As DataRow In dt.Rows

                '表單狀態
                If "bef".Equals(Type) Then
                    If (item("labOTStatus").ToString()).Equals("1") Then
                        item("labOTStatus") = "暫存"
                    ElseIf (item("labOTStatus").ToString()).Equals("2") Then
                        item("labOTStatus") = "送簽"
                    ElseIf (item("labOTStatus").ToString()).Equals("3") Then
                        item("labOTStatus") = "核准"
                    ElseIf (item("labOTStatus").ToString()).Equals("4") Then
                        item("labOTStatus") = "駁回"
                    ElseIf (item("labOTStatus").ToString()).Equals("5") Then
                        item("labOTStatus") = "刪除"
                    ElseIf (item("labOTStatus").ToString()).Equals("9") Then
                        item("labOTStatus") = "取消"
                    End If
                Else
                    If (item("labOTStatus").ToString()).Equals("1") Then
                        item("labOTStatus") = "暫存"
                    ElseIf (item("labOTStatus").ToString()).Equals("2") Then
                        item("labOTStatus") = "送簽"
                    ElseIf (item("labOTStatus").ToString()).Equals("3") Then
                        item("labOTStatus") = "核准"
                    ElseIf (item("labOTStatus").ToString()).Equals("4") Then
                        item("labOTStatus") = "駁回"
                    ElseIf (item("labOTStatus").ToString()).Equals("5") Then
                        item("labOTStatus") = "刪除"
                    ElseIf (item("labOTStatus").ToString()).Equals("6") Then
                        item("labOTStatus") = "取消"
                    ElseIf (item("labOTStatus").ToString()).Equals("7") Then
                        item("labOTStatus") = "作廢"
                    ElseIf (item("labOTStatus").ToString()).Equals("9") Then
                        item("labOTStatus") = "計薪後收回"
                    End If
                End If

                '
                'If (item("labWorkStatus").ToString()).Equals("1") Then
                '    item("labWorkStatus") = "在職"
                'ElseIf (item("labWorkStatus").ToString()).Equals("2") Then
                '    item("labWorkStatus") = "留停"
                'ElseIf (item("labWorkStatus").ToString()).Equals("3") Then
                '    item("labWorkStatus") = "離職"
                'End If

                If (item("labSex").ToString()).Equals("1") Then
                    item("labSex") = "男"
                ElseIf (item("labSex").ToString()).Equals("2") Then
                    item("labSex") = "女"
                End If


                If (item("labSalaryOrAdjust").ToString()).Equals("1") Then
                    item("labSalaryOrAdjust") = "轉薪資"
                ElseIf (item("labSalaryOrAdjust").ToString()).Equals("2") Then
                    item("labSalaryOrAdjust") = "轉補休"
                End If




                dtCloned.ImportRow(item)

            Next
        End If

        If dtCloned.Rows.Count > 0 Then
            For Each item As DataRow In dtCloned.Rows
                If (item("HolidayOrNot").ToString()).Equals("0") Then
                    item("labHolidayOrNot") = "平日"
                ElseIf (item("HolidayOrNot").ToString()).Equals("1") Then
                    item("labHolidayOrNot") = "假日"
                End If
                item("labIsProcessDate") = ""
                If "after".Equals(Type) Then
                    If ("1".Equals(item("ToOverTimeFlag"))) Then
                        item("labIsProcessDate") = "已拋轉"
                    Else
                        item("labIsProcessDate") = "未拋轉"
                    End If
                End If

            Next
        End If
        dtCloned.Columns.Remove("HolidayOrNot")

        Return dtCloned

    End Function
    Public Function changeTableNumToNameForDownLoan(ByVal dt As DataTable) As DataTable
        Dim dtCloned As DataTable = dt.Clone()
        If "after".Equals(Type) Then
            dtCloned.Columns.Add("時段一", GetType(String))
            dtCloned.Columns.Add("時段二", GetType(String))
            dtCloned.Columns.Add("時段三", GetType(String))
            dtCloned.Columns.Add("留守時段", GetType(String))
            dtCloned.Columns.Add("拋轉狀態", GetType(String))
            dtCloned.Columns.Add("拋轉日期", GetType(String))
        End If
        dtCloned.Columns.Add("加班日期類型", GetType(String))

        dtCloned.Columns.Add("用餐時數", GetType(String))
        dtCloned.Columns.Add("離職日期", GetType(String))
        dtCloned.Columns.Add("補休失效日", GetType(String))
        dtCloned.Columns.Add("核准日期", GetType(String))
        dtCloned.Columns.Add("核准時間", GetType(String))
        dtCloned.Columns.Add("申請日期", GetType(String))
        dtCloned.Columns.Add("申請時間", GetType(String))
        dtCloned.Columns.Add("加班時數", GetType(String))




        dt.Columns.Add("申請時間", GetType(String))
        dt.Columns.Add("申請日期", GetType(String))
        dt.Columns.Add("離職日期", GetType(String))
        dt.Columns.Add("補休失效日", GetType(String))
        dt.Columns.Add("核准日期", GetType(String))
        dt.Columns.Add("核准時間", GetType(String))
        dt.Columns.Add("到職日期", GetType(String))


        If dt.Rows.Count > 0 Then
            For Each item As DataRow In dt.Rows
                Dim thisDate1 As Date = item("申請日期1")
                item("申請日期") = FormatDateTime(thisDate1.ToString, DateFormat.ShortDate).ToString
                item("申請時間") = thisDate1.ToString("HH:mm")
                thisDate1 = item("核准日期1")

                item("到職日期") = FormatDateTime(item("到職日期1").ToString, DateFormat.ShortDate).ToString
                item("離職日期") = FormatDateTime(item("離職日期1").ToString, DateFormat.ShortDate).ToString
                item("核准日期") = FormatDateTime(item("核准日期1").ToString, DateFormat.ShortDate).ToString
                item("核准時間") = thisDate1.ToString("HH:mm")
                item("補休失效日") = FormatDateTime(item("補休失效日1").ToString, DateFormat.ShortDate).ToString

                If "1900/1/1".Equals(item("補休失效日")) Then
                    item("補休失效日") = ""
                End If

                If (item("性別").ToString()).Equals("1") Then
                    item("性別") = "男"
                ElseIf (item("性別").ToString()).Equals("2") Then
                    item("性別") = "女"
                End If


                If (item("上傳附件").ToString().Trim).Equals("") Or Nothing Then
                    item("上傳附件") = "無"
                Else
                    item("上傳附件") = "有"
                End If


                If (item("加班原因").ToString()).Equals("") Or Nothing Then
                    item("加班原因") = "無"
                End If
                dtCloned.ImportRow(item)

            Next
        End If

        If dtCloned.Rows.Count > 0 Then
            For Each item As DataRow In dtCloned.Rows
                Dim OTTxnID As String = item("OTTxnID")
                Dim OTSeqNo As String = item("OTSeqNo")
                Dim sameOTTxnID As DataRow() = dtCloned.[Select]("OTTxnID='" + OTTxnID + "' ")
                Dim OTTotalTime As Double
                Dim OTTotalTime1 As Double
                Dim OTTotalTime2 As Double



                If sameOTTxnID.Count = 1 Then
                    OTTotalTime1 = CDbl(FormatNumber(((Convert.ToDouble(item("加班時數1").ToString()) - Convert.ToDouble(item("MealTime").ToString())) / 60), 1))
                    item("加班時數") = OTTotalTime1
                ElseIf sameOTTxnID.Count > 1 Then
                    If OTSeqNo.Equals("1") Then
                        OTTotalTime1 = CDbl(FormatNumber(((Convert.ToDouble(item("加班時數1").ToString()) - Convert.ToDouble(item("MealTime").ToString())) / 60), 1))
                        item("加班時數") = OTTotalTime1
                    ElseIf OTSeqNo.Equals("2") Then
                        Dim sameOTTxnIDDB As DataTable = sameOTTxnID.CopyToDataTable
                        Dim sameOTTxnIDDBOTSeqNo1 As DataRow() = sameOTTxnIDDB.[Select]("OTSeqNo='1'")
                        Dim sameOTTxnIDDBOTSeqNo2 As DataRow() = sameOTTxnIDDB.[Select]("OTSeqNo='2'")

                        If sameOTTxnIDDBOTSeqNo1.Count > 0 And sameOTTxnIDDBOTSeqNo2.Count > 0 Then '先判別有抓到值 避免錯誤
                            OTTotalTime1 = CDbl(FormatNumber(((Convert.ToDouble(sameOTTxnID(0).Item("加班時數1").ToString()) - Convert.ToDouble(sameOTTxnID(0).Item("MealTime").ToString())) / 60), 1))
                            OTTotalTime = (Convert.ToDouble(sameOTTxnIDDBOTSeqNo1(0).Item("加班時數1").ToString()) + Convert.ToDouble(sameOTTxnIDDBOTSeqNo2(0).Item("加班時數1").ToString())) - Convert.ToDouble(sameOTTxnIDDBOTSeqNo1(0).Item("MealTime").ToString()) - Convert.ToDouble(sameOTTxnIDDBOTSeqNo2(0).Item("MealTime").ToString())
                            OTTotalTime2 = CDbl(FormatNumber((OTTotalTime - (OTTotalTime1 * 60)) / 60, 1))
                            item("加班時數") = OTTotalTime2
                        Else '如果抓不到值那就用算法一
                            OTTotalTime1 = CDbl(FormatNumber(((Convert.ToDouble(item("加班時數1").ToString()) - Convert.ToDouble(item("MealTime").ToString())) / 60), 1))
                            item("加班時數") = OTTotalTime1
                        End If

                    End If
                End If




                'If IsNumeric(item("加班時數").ToString()) Then
                '    item("加班時數") = CDbl(FormatNumber(((Convert.ToDouble(item("加班時數").ToString()) - Convert.ToDouble(item("MealTime").ToString())) / 60), 1))
                'End If





                '表單狀態
                If (item("表單狀態").ToString()).Equals("1") Then
                    item("表單狀態") = "暫存"
                ElseIf (item("表單狀態").ToString()).Equals("2") Then
                    item("表單狀態") = "送簽"
                ElseIf (item("表單狀態").ToString()).Equals("3") Then
                    item("表單狀態") = "核准"
                ElseIf (item("表單狀態").ToString()).Equals("4") Then
                    item("表單狀態") = "駁回"
                ElseIf (item("表單狀態").ToString()).Equals("5") Then
                    item("表單狀態") = "刪除"
                ElseIf (item("表單狀態").ToString()).Equals("9") Then
                    item("表單狀態") = "取消"
                End If
                If "bef".Equals(Type) Then
                    If (item("表單狀態").ToString()).Equals("1") Then
                        item("表單狀態") = "暫存"
                    ElseIf (item("表單狀態").ToString()).Equals("2") Then
                        item("表單狀態") = "送簽"
                    ElseIf (item("表單狀態").ToString()).Equals("3") Then
                        item("表單狀態") = "核准"
                    ElseIf (item("表單狀態").ToString()).Equals("4") Then
                        item("表單狀態") = "駁回"
                    ElseIf (item("表單狀態").ToString()).Equals("5") Then
                        item("表單狀態") = "刪除"
                    ElseIf (item("表單狀態").ToString()).Equals("9") Then
                        item("表單狀態") = "取消"
                    End If
                Else
                    If (item("表單狀態").ToString()).Equals("1") Then
                        item("表單狀態") = "暫存"
                    ElseIf (item("表單狀態").ToString()).Equals("2") Then
                        item("表單狀態") = "送簽"
                    ElseIf (item("表單狀態").ToString()).Equals("3") Then
                        item("表單狀態") = "核准"
                    ElseIf (item("表單狀態").ToString()).Equals("4") Then
                        item("表單狀態") = "駁回"
                    ElseIf (item("表單狀態").ToString()).Equals("5") Then
                        item("表單狀態") = "刪除"
                    ElseIf (item("表單狀態").ToString()).Equals("6") Then
                        item("表單狀態") = "取消"
                    ElseIf (item("表單狀態").ToString()).Equals("7") Then
                        item("表單狀態") = "作廢"
                    ElseIf (item("表單狀態").ToString()).Equals("9") Then
                        item("表單狀態") = "計薪後收回"
                    End If
                End If

                If (item("轉薪資/補休").ToString()).Equals("1") Then
                    item("轉薪資/補休") = "轉薪資"
                    item("補休失效日") = ""
                ElseIf (item("轉薪資/補休").ToString()).Equals("2") Then
                    item("轉薪資/補休") = "轉補休"
                End If

                If Not ((item("在職狀態").ToString()).Equals("離職") Or (item("在職狀態").ToString()).Equals("退休")) Then
                    item("離職日期") = ""
                End If
                If (item("HolidayOrNot").ToString()).Equals("0") Then
                    item("加班日期類型") = "平日"
                ElseIf (item("HolidayOrNot").ToString()).Equals("1") Then
                    item("加班日期類型") = "假日"
                End If

                If (item("OTValidID").ToString.Trim.Equals("")) Then
                    item("核准日期") = ""
                    item("核准時間") = ""
                End If

                item("加班結束時間") = item("加班結束時間").ToString().Substring(0, 2) + ":" + item("加班結束時間").ToString().Substring(2, 2)
                item("加班開始時間") = item("加班開始時間").ToString().Substring(0, 2) + ":" + item("加班開始時間").ToString().Substring(2, 2)

                If "after".Equals(Type) Then

                    item("時段一") = item("Time_one")
                    If item("時段一").Equals("_") Then
                        item("時段一") = ""
                    End If
                    item("時段二") = item("Time_two")
                    If item("時段二").Equals("_") Then
                        item("時段二") = ""
                    End If
                    item("時段三") = item("Time_three")
                    If item("時段三").Equals("_") Then
                        item("時段三") = ""
                    End If
                    'item("留守時段") = item("Stay_Time")
                    'If item("留守時段").Equals("_") Then
                    '    item("留守時段") = ""
                    'End If


                    If ("1".Equals(item("ToOverTimeFlag"))) Then
                        item("拋轉日期") = item("ToOverTimeDate") 'ToOverTimeDate
                        item("拋轉狀態") = "已拋轉"
                    Else
                        item("拋轉日期") = ""
                        item("拋轉狀態") = "未拋轉"
                    End If
                End If

                item("用餐時數") = item("MealTime")
            Next
        End If
        If "after".Equals(Type) Then
            dtCloned.Columns.Remove("Time_one")
            dtCloned.Columns.Remove("Time_two")
            dtCloned.Columns.Remove("Time_three")
            'dtCloned.Columns.Remove("Stay_Time")
            dtCloned.Columns().Remove("ToOverTimeDate")
            dtCloned.Columns().Remove("ToOverTimeFlag")

            '2017 02 08 留守要改.... 先拿掉
            'dtCloned.Columns.Remove("留守時段")
        End If
        dtCloned.Columns().Remove("OTSeqNo")
        dtCloned.Columns.Remove("加班時數1")
        dtCloned.Columns.Remove("離職日期1")
        dtCloned.Columns.Remove("補休失效日1")
        dtCloned.Columns.Remove("核准日期1")
        dtCloned.Columns.Remove("申請日期1")
        dtCloned.Columns.Remove("到職日期1")
        dtCloned.Columns().Remove("OTTxnID")
        dtCloned.Columns.Remove("HolidayOrNot")
        dtCloned.Columns.Remove("MealTime")
        dtCloned.Columns().Remove("OTValidID")
        Return dtCloned
    End Function

#End Region

    Public Function isNull(ByVal strPro As String) As Boolean
        Dim myType As Type = GetType(OV_3)
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

    Public Function getOverTimeThisMonth(ByVal startDate As String, ByVal startTime As String, ByVal type As String) As String
        startTime = startTime.Substring(0, 2) + ":" + startTime.Substring(2, 2)
        Dim yyyy As String = (startDate.Split("/"))(0)
        Dim MM As String = (startDate.Split("/"))(1)

        MM = String.Format("{0:00}", (Convert.ToInt16(MM) + 1))
        startDate = startDate + " " + startTime

        Dim endDate As String = yyyy + "/" + MM + "01" + +" 00:00"

        Dim sDateTime As DateTime = DateTime.ParseExact(startDate, "yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture)
        Dim eDateTime As DateTime = DateTime.ParseExact(endDate, "yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture)
        Dim timSpan = (eDateTime - sDateTime)

        If type.Equals("H") Then
            Return CDbl(FormatNumber(timSpan.TotalHours, 1)).ToString
        ElseIf type.Equals("M") Then
            Return timSpan.TotalMinutes.ToString()
        ElseIf type.Equals("S") Then
            Return timSpan.TotalSeconds.ToString()
        End If

    End Function


    Public Function getOverTime(ByVal startDate As String, ByVal endDate As String, ByVal startTime As String, ByVal endTime As String, ByVal type As String) As String
        startTime = startTime.Substring(0, 2) + ":" + startTime.Substring(2, 2)
        endTime = endTime.Substring(0, 2) + ":" + endTime.Substring(2, 2)
        startDate = startDate + " " + startTime
        endDate = endDate + " " + endTime

        Dim sDateTime As DateTime = DateTime.ParseExact(startDate, "yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture)
        Dim eDateTime As DateTime = DateTime.ParseExact(endDate, "yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture)
        Dim timSpan = (eDateTime - sDateTime)

        If type.Equals("H") Then
            Return CDbl(FormatNumber(timSpan.TotalHours, 1)).ToString
        ElseIf type.Equals("M") Then
            Return timSpan.TotalMinutes
        ElseIf type.Equals("S") Then
            Return timSpan.TotalSeconds
        End If

    End Function

    Public Function clearArray() As Array
        Dim timeArray(4) As Double
        timeArray(0) = 0
        timeArray(1) = 0
        timeArray(2) = 0
        timeArray(3) = 0
        Return timeArray
    End Function


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="type"></param>
    ''' <param name="OTCompID"></param>
    ''' <param name="OTEmpID"></param>
    ''' <param name="OTStartDate"></param>
    ''' <param name="OTEndDate"></param>
    ''' <param name="OTStartTime"></param>
    ''' <param name="OTEndTime"></param>
    ''' <param name="OTSeq"></param>
    ''' <param name="OTTxnID"></param>
    ''' <param name="mmealFlag"></param>
    ''' <param name="mmealTime"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' 
    ''' 傳入使用者所填寫的時間用餐
    ''' 然後查出要查的資料 
    ''' 
    ''' </remarks>
    Public Function doThrowFor4002Time(ByVal type As String, ByVal OTCompID As String, ByVal OTEmpID As String, ByVal OTStartDate As String, ByVal OTEndDate As String, ByVal OTStartTime As String, ByVal OTEndTime As String, ByVal OTSeq As String, ByVal OTTxnID As String, ByVal mmealFlag As String, ByVal mmealTime As String) As DataTable
        '變數宣告
        Dim strSQL_Declaration As New StringBuilder()
        Dim data_Declaration As DataTable                '原生資料
        Dim data_Declaration_Count As Integer            '原生資料筆數
        Dim data_Period As DataTable = getTable_Period() '存放時段的Table
        Dim disasterDataTable() As DataTable = getDisasterDataTable()
        Dim data_DeclarationClone As DataTable

        '資料要排序 注意!
        '公司代碼-OTCompID、加班申請人-OTEmpID   、加班開始日期-OTStartDate、加班結束日期-OTEndDate、加班開始時間-OTStartTime、加班結束時間-OTEndTime、加班單號-OTxnID
        '單號序列-OTSeqNo 、用餐扣除註記-MealFlag、用餐時間-MealTime       、申報單狀態-OTStatus   、計薪註記-OTSalaryPaid   、假日註記-HolidayOrNot
        If type.Equals("bef") Then
            strSQL_Declaration.Append("select OTCompID,OTEmpID,OTStartDate,OTEndDate,OTStartTime,OTEndTime,OTTxnID,OTSeqNo,MealFlag,MealTime,OTStatus,HolidayOrNot from OverTimeAdvance ")
        ElseIf type.Equals("after") Then
            strSQL_Declaration.Append("select OTCompID,OTEmpID,OTStartDate,OTEndDate,OTStartTime,OTEndTime,OTTxnID,OTSeqNo,MealFlag,MealTime,OTStatus,OTSalaryPaid,HolidayOrNot from OverTimeDeclaration ")
        Else
            Return Nothing
        End If
        strSQL_Declaration.Append("Where OTStatus = '3' OR OTStatus = '2' and OTCompID='" + OTCompID + "' and OTEmpID='" + OTEmpID + "'")
        strSQL_Declaration.Append(" order by OTStartDate,OTStartTime")
        data_Declaration = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL_Declaration.ToString(), "AattendantDB").Tables(0)
        Dim rows As DataRow() = getSameDataForOTTxnID(data_Declaration, OTTxnID)
        'If rows.Count > 0 Then

        'End If
        Dim row1 As DataRow = data_Declaration.NewRow()
        row1.ItemArray = TryCast(rows(0).ItemArray.Clone(), Object())
        Dim row2 = data_Declaration.NewRow()
        row2.ItemArray = TryCast(rows(0).ItemArray.Clone(), Object())


        '檢查使用者所填的資料
        '如果有跨日要拆單
        If OTStartDate.Equals(OTEndDate) Then
            Dim StartDate1 As String = OTStartDate
            Dim EndDate1 As String = OTEndDate
            Dim StartTime1 As String = OTStartTime
            Dim EndTime1 As String = OTEndTime
            Dim MealFlag1 As String = mmealFlag
            Dim MealTime1 As String = mmealTime

            Dim HolidayOrNot1 As String = If(Me.CheckHolidayOrNot(StartDate1), "1", "0")



            '更改row 須注意 
            '一. OTSeq 序號。加班人於同一加班日之第N張申請單，從1開始 因為只是計算時段且不放入資料庫 所以不管OTSeq 統一塞98 99
            row1.Item("OTStartDate") = StartDate1
            row1.Item("OTEndDate") = EndDate1
            row1.Item("OTStartTime") = StartTime1
            row1.Item("OTEndTime") = EndTime1
            row1.Item("HolidayOrNot") = HolidayOrNot1
            row1.Item("OTSeqNo") = "1"
            row1.Item("MealFlag") = MealFlag1
            row1.Item("MealTime") = MealTime1

            For i = 0 To rows.Count - 1
                data_Declaration.Rows.Remove(rows(i))
            Next

            data_Declaration.Rows.Add(row1)
            'sort
            data_DeclarationClone = data_Declaration.Clone()
            For Each dr As DataRow In data_Declaration.Select("'1'='1' ", "OTEmpID,OTStartDate,OTStartTime")
                data_DeclarationClone.ImportRow(dr)
            Next


            'dt = changeTableNimToNameForDetial(dt)

        Else
            Dim StartDate1 As String = OTStartDate
            Dim EndDate1 As String = OTStartDate
            Dim StartTime1 As String = OTStartTime
            Dim EndTime1 As String = "2359"
            Dim StartDate2 As String = OTEndDate
            Dim EndDate2 As String = OTEndDate
            Dim StartTime2 As String = "0000"
            Dim EndTime2 As String = OTEndTime
            Dim MealFlag1 As String = mmealFlag
            Dim MealTime1 As String = mmealTime
            Dim MealFlag2 As String = "0"
            Dim MealTime2 As String = "0"

            Dim HolidayOrNot1 As String = If(Me.CheckHolidayOrNot(StartDate1), "1", "0")
            Dim HolidayOrNot2 As String = If(Me.CheckHolidayOrNot(StartDate2), "1", "0")



            If "1".Equals(MealFlag1) Then
                Dim row1OverTime As String = (Convert.ToInt32(Me.getOverTime(StartDate1, EndDate1, StartTime1, EndTime1, "M")) + 1).ToString
                If Convert.ToDouble(row1OverTime) < Convert.ToDouble(MealTime1) Then
                    MealTime1 = (Convert.ToDouble(row1OverTime)).ToString
                    MealTime2 = (Convert.ToDouble(mmealTime) - Convert.ToDouble(row1OverTime)).ToString
                    MealFlag2 = "1"
                Else
                    MealFlag2 = "0"
                    MealTime2 = "0"
                End If

            End If
            'Dim MealFlag1 As String = mealFlag
            'Dim MealTime1 As String = mealTime
            'Dim MealFlag2 As String = mealFlag
            'Dim MealTime2 As String = mealTime

            For i = 0 To rows.Count - 1
                data_Declaration.Rows.Remove(rows(i))
            Next
            '更改row 須注意 
            '一. OTSeq 序號。加班人於同一加班日之第N張申請單，從1開始 因為只是計算時段且不放入資料庫 所以不管OTSeq 統一塞98 99
            row1.Item("OTStartDate") = StartDate1
            row1.Item("OTEndDate") = EndDate1
            row1.Item("OTStartTime") = StartTime1
            row1.Item("OTEndTime") = EndTime1

            row1.Item("MealFlag") = MealFlag1
            row1.Item("MealTime") = MealTime1
            row1.Item("OTSeqNo") = "1"
            row1.Item("HolidayOrNot") = HolidayOrNot1

            row2.Item("HolidayOrNot") = HolidayOrNot2

            row2.Item("OTStartDate") = StartDate2
            row2.Item("OTEndDate") = EndDate2
            row2.Item("OTStartTime") = StartTime2
            row2.Item("OTEndTime") = EndTime2

            row2.Item("MealFlag") = MealFlag2
            row2.Item("MealTime") = MealTime2

            row2.Item("OTSeqNo") = "2"

            'sort
            data_Declaration.Rows.Add(row1)
            data_Declaration.Rows.Add(row2)

            data_DeclarationClone = data_Declaration.Clone()
            For Each dr As DataRow In data_Declaration.Select("'1'='1' ", "OTEmpID,OTStartDate,OTStartTime")
                data_DeclarationClone.ImportRow(dr)
            Next
        End If


        data_Declaration_Count = data_DeclarationClone.Rows.Count
        '取出每一筆，計算時段
        For i As Integer = 0 To data_Declaration_Count - 1 Step 1
            Dim compID As String = data_DeclarationClone.Rows(i).Item("OTCompID").ToString
            Dim empID As String = data_DeclarationClone.Rows(i).Item("OTEmpID").ToString
            Dim startDate As String = data_DeclarationClone.Rows(i).Item("OTStartDate").ToString
            Dim endDate As String = data_DeclarationClone.Rows(i).Item("OTEndDate").ToString
            Dim startTime As String = data_DeclarationClone.Rows(i).Item("OTStartTime").ToString.Insert(2, ":")
            Dim endTime As String = data_DeclarationClone.Rows(i).Item("OTEndTime").ToString.Insert(2, ":")
            Dim txnID As String = data_DeclarationClone.Rows(i).Item("OTTxnID").ToString
            Dim seqNo As String = data_DeclarationClone.Rows(i).Item("OTSeqNo").ToString
            Dim mealFlag As String = data_DeclarationClone.Rows(i).Item("MealFlag").ToString

            Dim mealTime As Double = Convert.ToDouble(data_DeclarationClone.Rows(i).Item("MealTime").ToString)
            Dim holiDay As String = IIf(data_DeclarationClone.Rows(i).Item("HolidayOrNot").ToString = "0", "T", "F")
            getTime_Interval(data_Period, compID, empID, startDate, endDate, startTime, endTime, "", txnID, seqNo, DeptID, OrganID, "", "", mealFlag, mealTime, holiDay, disasterDataTable)
        Next
        Return data_Period
    End Function

    Public Function doThrow(ByVal type As String) As DataTable


        '變數宣告
        Dim strSQL_Declaration As New StringBuilder()
        Dim data_Declaration As DataTable                '原生資料
        Dim data_Declaration_Count As Integer            '原生資料筆數
        Dim data_Period As DataTable = getTable_Period() '存放時段的Table
        Dim ProfileCompID As String = UserProfile.SelectCompRoleID
        Dim disasterDataTable() As DataTable = getDisasterDataTable()

        '資料要排序 注意!
        '公司代碼-OTCompID、加班申請人-OTEmpID   、加班開始日期-OTStartDate、加班結束日期-OTEndDate、加班開始時間-OTStartTime、加班結束時間-OTEndTime、加班單號-OTxnID
        '單號序列-OTSeqNo 、用餐扣除註記-MealFlag、用餐時間-MealTime       、申報單狀態-OTStatus   、計薪註記-OTSalaryPaid   、假日註記-HolidayOrNot
        If type.Equals("bef") Then
            strSQL_Declaration.Append("select OTCompID,OTEmpID,OTStartDate,OTEndDate,OTStartTime,OTEndTime,OTTxnID,OTSeqNo,MealFlag,MealTime,OTStatus,HolidayOrNot from OverTimeAdvance ")
        ElseIf type.Equals("after") Then
            strSQL_Declaration.Append("select OTCompID,OTEmpID,OTStartDate,OTEndDate,OTStartTime,OTEndTime,OTTxnID,OTSeqNo,MealFlag,MealTime,OTStatus,OTSalaryPaid,HolidayOrNot from OverTimeDeclaration ")
        Else
            Return Nothing
        End If

        strSQL_Declaration.Append("Where OTStatus = '3' OR OTStatus = '2' and OTCompID='" + ProfileCompID + "' ")
        strSQL_Declaration.Append(" order by OTEmpID,OTStartDate,OTStartTime")
        data_Declaration = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL_Declaration.ToString(), "AattendantDB").Tables(0)
        data_Declaration_Count = data_Declaration.Rows.Count
        '取出每一筆，計算時段
        For i As Integer = 0 To data_Declaration_Count - 1 Step 1
            Dim compID As String = data_Declaration.Rows(i).Item("OTCompID").ToString
            Dim empID As String = data_Declaration.Rows(i).Item("OTEmpID").ToString
            Dim startDate As String = data_Declaration.Rows(i).Item("OTStartDate").ToString
            Dim endDate As String = data_Declaration.Rows(i).Item("OTEndDate").ToString
            Dim startTime As String = data_Declaration.Rows(i).Item("OTStartTime").ToString.Insert(2, ":")
            Dim endTime As String = data_Declaration.Rows(i).Item("OTEndTime").ToString.Insert(2, ":")
            Dim txnID As String = data_Declaration.Rows(i).Item("OTTxnID").ToString
            Dim seqNo As String = data_Declaration.Rows(i).Item("OTSeqNo").ToString
            Dim mealFlag As String = data_Declaration.Rows(i).Item("MealFlag").ToString
            Dim mealTime As Double = Convert.ToDouble(data_Declaration.Rows(i).Item("MealTime").ToString)
            Dim holiDay As String = IIf(data_Declaration.Rows(i).Item("HolidayOrNot").ToString = "0", "T", "F")
            getTime_Interval(data_Period, compID, empID, startDate, endDate, startTime, endTime, "", txnID, seqNo, DeptID, OrganID, "", "", mealFlag, mealTime, holiDay, disasterDataTable)
        Next
        Return data_Period
    End Function

    ''' <summary>
    ''' get disasterByCityDataTable and disasterByEmpDataTable and
    ''' disasterByCityDataTable remove same dateTime for disasterByEmpDataTable 0->disasterByEmpDataTable 1->       disasterByCityDataTable

    ''' </summary>
    ''' <returns> DataTable(0)=disasterByEmpDataTable DataTable(1)=disasterByCityDataTable</returns>
    ''' <remarks></remarks>
    Private Function getDisasterDataTable() As DataTable()
        Dim disasterByEmpDataTable As DataTable = getDisasterByEmpDataTable()
        Dim disasterByCityDataTable As DataTable = getDisasterByCityDataTable()
        Dim removeArraylist As ArrayList = New ArrayList()
        Dim disasterDataTable(2) As DataTable


        '先排除同個人 同日期 同開始時間 同結束時間
        For i = 0 To disasterByCityDataTable.Rows.Count - 1
            Dim ctCompID As String = disasterByCityDataTable.Rows(i).Item("CompID")
            Dim ctEmpID As String = disasterByCityDataTable.Rows(i).Item("EmpID")
            Dim ctDisasterStartDate As String = disasterByCityDataTable.Rows(i).Item("DisasterStartDate")
            Dim ctDisasterEndDate As String = disasterByCityDataTable.Rows(i).Item("DisasterEndDate")
            Dim ctBeginTime As String = disasterByCityDataTable.Rows(i).Item("BeginTime")
            Dim ctEndTime As String = disasterByCityDataTable.Rows(i).Item("EndTime")
            For j = 0 To disasterByEmpDataTable.Rows.Count - 1
                Dim empCompID As String = disasterByEmpDataTable.Rows(j).Item("CompID")
                Dim empEmpID As String = disasterByEmpDataTable.Rows(j).Item("EmpID")
                Dim empDisasterStartDate As String = disasterByEmpDataTable.Rows(j).Item("DisasterStartDate")
                Dim empDisasterEndDate As String = disasterByEmpDataTable.Rows(j).Item("DisasterEndDate")
                Dim empBeginTime As String = disasterByEmpDataTable.Rows(j).Item("BeginTime")
                Dim empEndTime As String = disasterByEmpDataTable.Rows(j).Item("EndTime")

                If (ctDisasterStartDate.Equals(empDisasterStartDate) And ctDisasterEndDate.Equals(empDisasterEndDate) And ctBeginTime.Equals(empBeginTime) And ctEndTime.Equals(ctEndTime) And ctCompID.Equals(empCompID) And ctEmpID.Equals(empEmpID)) Then
                    removeArraylist.Add(disasterByCityDataTable.Rows(i))
                End If

            Next
        Next
        For i = 0 To removeArraylist.Count - 1
            disasterByCityDataTable.Rows.Remove(removeArraylist(i))
        Next


        disasterDataTable(0) = disasterByEmpDataTable
        disasterDataTable(1) = disasterByCityDataTable
        Return disasterDataTable
    End Function

    Private Function getDisasterByEmpDataTable() As DataTable
        Dim strSQL_Declaration As StringBuilder = New StringBuilder()
        strSQL_Declaration.Append("Select [CompID],[DisasterStartDate],[DisasterEndDate],[BeginTime],[EndTime],[EmpID],[remark],[DisasterType],[LastChgCompID],[LastChgEmpID],[LastChgDate]")
        strSQL_Declaration.Append(" FROM [NaturalDisasterByEmp]")
        strSQL_Declaration.Append(" order by DisasterStartDate,BeginTime")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL_Declaration.ToString(), "AattendantDB").Tables(0)
    End Function

    Private Function getDisasterByCityDataTable() As DataTable
        Dim strSQL_Declaration As StringBuilder = New StringBuilder()
        strSQL_Declaration.Append("SELECT NDC.[CompID] as 'CompID',NDC.[WorkSiteID] as 'WorkSiteID',NDC.[WorkSiteName] as 'WorkSiteName',NDC.DisasterStartDate as 'DisasterStartDate',NDC.DisasterEndDate as 'DisasterEndDate',NDC.BeginTime as 'BeginTime',NDC.EndTime as 'EndTime',P.EmpID as 'EmpID',P.Name as 'Name'")
        strSQL_Declaration.Append(" FROM [NaturalDisasterByCity] NDC")
        strSQL_Declaration.Append(" LEFT JOIN " + eHRMSDB_ITRD + ".[dbo].Personal  P ON NDC.WorkSiteID = P.WorkSiteID AND NDC.CompID=P.CompID")
        strSQL_Declaration.Append(" order by DisasterStartDate,BeginTime")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL_Declaration.ToString(), "AattendantDB").Tables(0)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="nowAddTime"></param>
    ''' <param name="OTEmpID"></param>
    ''' <param name="OTCompID"></param>
    ''' <param name="OTStartDate"></param>
    ''' <param name="OTEndDate"></param>
    ''' <param name="OTStartTime"></param>
    ''' <param name="OTEndTime"></param>
    ''' <returns>stayTimeAndNowAddTime stayTime0 NowAddTime1</returns>
    ''' <remarks></remarks>
    Private Function getStayTimeAndNowAddTime(ByVal nowAddTime As Double, ByVal OTEmpID As String, ByVal OTCompID As String, ByVal OTStartDate As String, ByVal OTEndDate As String, ByVal OTStartTime As String, ByVal OTEndTime As String, ByVal disasterDataTable() As DataTable) As Double()
        Dim stayTimeAndNowAddTime(2) As Double
        Dim stayTime As Double = 0
        ' sql注意 呼叫太多次了 可能要移到外面

        Dim disasterByEmpDataTable As DataTable = disasterDataTable(0)
        Dim disasterByCityDataTable As DataTable = disasterDataTable(1)
        '加班 ovStartDateTime ovEndDateTime
        Dim ovStartDateTime As DateTime = DateTime.ParseExact(OTStartDate + " " + OTStartTime, "yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture)
        Dim ovEndDateTime As DateTime = DateTime.ParseExact(OTEndDate + " " + OTEndTime, "yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture)
        '留守 stStartDateTime stEndDateTime
        'StayTime
        '(0) StayTime
        '(1) NowAddTime

        'TESTIf (OTEmpID.Equals("101261")) Then
        'EmpID 一樣 且 CompID一樣
        For i = 0 To disasterByCityDataTable.Rows.Count - 1
            Dim stEmpID As String = disasterByCityDataTable.Rows(i).Item("EmpID")
            Dim stCompID As String = disasterByCityDataTable.Rows(i).Item("CompID")
            Dim stStartDate As Date = disasterByCityDataTable.Rows(i).Item("DisasterStartDate")
            Dim stStartTime As String = disasterByCityDataTable.Rows(i).Item("BeginTime")
            Dim stEndDate As Date = disasterByCityDataTable.Rows(i).Item("DisasterEndDate")
            Dim stEndTime As String = disasterByCityDataTable.Rows(i).Item("EndTime")

            Dim stStartDateTime As DateTime = DateTime.ParseExact(stStartDate.ToString("yyyy/MM/dd") + " " + stStartTime, "yyyy/MM/dd HHmm", CultureInfo.InvariantCulture)
            Dim stEndDateTime As DateTime = DateTime.ParseExact(stEndDate.ToString("yyyy/MM/dd") + " " + stEndTime, "yyyy/MM/dd HHmm", CultureInfo.InvariantCulture)
            '當員工id一樣且公司一樣
            If (stEmpID.Equals(OTEmpID) And stCompID.Equals(OTCompID)) Then
                '留守完全覆蓋加班
                If stStartDateTime <= ovStartDateTime And stEndDateTime >= ovEndDateTime Then
                    stayTime = stayTime + nowAddTime
                    nowAddTime = 0
                    '如果完全覆蓋中止比對 因為加班已清空
                    stayTimeAndNowAddTime(0) = stayTime
                    stayTimeAndNowAddTime(1) = nowAddTime
                    Return stayTimeAndNowAddTime
                    '留守覆蓋加班後段 OVS~STE=STE-OVS
                ElseIf stStartDateTime <= ovStartDateTime And stEndDateTime < ovEndDateTime And ovStartDateTime < stEndDateTime Then
                    stayTime = stayTime + (New TimeSpan(stEndDateTime.Ticks) - New TimeSpan(ovStartDateTime.Ticks)).TotalMinutes
                    Dim spanTime = (New TimeSpan(stEndDateTime.Ticks) - New TimeSpan(ovStartDateTime.Ticks)).TotalMinutes
                    nowAddTime = nowAddTime - spanTime
                    '留守覆蓋加班前段 STS~OVE=OVE-STS
                ElseIf stStartDateTime > ovStartDateTime And stEndDateTime >= ovEndDateTime And stStartDateTime < ovEndDateTime Then
                    stayTime = stayTime + (New TimeSpan(ovEndDateTime.Ticks) - New TimeSpan(stStartDateTime.Ticks)).TotalMinutes
                    Dim spanTime = (New TimeSpan(ovEndDateTime.Ticks) - New TimeSpan(stStartDateTime.Ticks)).TotalMinutes
                    nowAddTime = nowAddTime - spanTime
                    '加班完全覆蓋留守
                ElseIf ovStartDateTime < stStartDateTime And ovEndDateTime > stEndDateTime Then
                    stayTime = stayTime + (New TimeSpan(stEndDateTime.Ticks) - New TimeSpan(stStartDateTime.Ticks)).TotalMinutes
                    Dim spanTime = (New TimeSpan(stEndDateTime.Ticks) - New TimeSpan(stStartDateTime.Ticks)).TotalMinutes
                    nowAddTime = nowAddTime - spanTime
                End If
            End If
        Next


        For i = 0 To disasterByEmpDataTable.Rows.Count - 1
            Dim stEmpID As String = disasterByEmpDataTable.Rows(i).Item("EmpID")
            Dim stCompID As String = disasterByEmpDataTable.Rows(i).Item("CompID")
            Dim stStartDate As Date = disasterByEmpDataTable.Rows(i).Item("DisasterStartDate")
            Dim stStartTime As String = disasterByEmpDataTable.Rows(i).Item("BeginTime")
            Dim stEndDate As Date = disasterByEmpDataTable.Rows(i).Item("DisasterEndDate")
            Dim stEndTime As String = disasterByEmpDataTable.Rows(i).Item("EndTime")

            Dim stStartDateTime As DateTime = DateTime.ParseExact(stStartDate.ToString("yyyy/MM/dd") + " " + stStartTime, "yyyy/MM/dd HHmm", CultureInfo.InvariantCulture)
            Dim stEndDateTime As DateTime = DateTime.ParseExact(stEndDate.ToString("yyyy/MM/dd") + " " + stEndTime, "yyyy/MM/dd HHmm", CultureInfo.InvariantCulture)
            '當員工id一樣且公司一樣
            If (stEmpID.Equals(OTEmpID) And stCompID.Equals(OTCompID)) Then
                '留守完全覆蓋加班
                If stStartDateTime <= ovStartDateTime And stEndDateTime >= ovEndDateTime Then
                    stayTime = stayTime + nowAddTime
                    nowAddTime = 0
                    '如果完全覆蓋中止比對 因為加班已清空
                    stayTimeAndNowAddTime(0) = stayTime
                    stayTimeAndNowAddTime(1) = nowAddTime
                    Return stayTimeAndNowAddTime
                    '留守覆蓋加班後段 OVS~STE=STE-OVS
                ElseIf stStartDateTime <= ovStartDateTime And stEndDateTime < ovEndDateTime And ovStartDateTime < stEndDateTime Then
                    stayTime = stayTime + (New TimeSpan(stEndDateTime.Ticks) - New TimeSpan(ovStartDateTime.Ticks)).TotalMinutes
                    Dim spanTime = (New TimeSpan(stEndDateTime.Ticks) - New TimeSpan(ovStartDateTime.Ticks)).TotalMinutes
                    nowAddTime = nowAddTime - spanTime
                    '留守覆蓋加班前段 STS~OVE=OVE-STS
                ElseIf stStartDateTime > ovStartDateTime And stEndDateTime >= ovEndDateTime And stStartDateTime < ovEndDateTime Then
                    stayTime = stayTime + (New TimeSpan(ovEndDateTime.Ticks) - New TimeSpan(stStartDateTime.Ticks)).TotalMinutes
                    Dim spanTime = (New TimeSpan(ovEndDateTime.Ticks) - New TimeSpan(stStartDateTime.Ticks)).TotalMinutes
                    nowAddTime = nowAddTime - spanTime
                    '加班完全覆蓋留守
                ElseIf ovStartDateTime < stStartDateTime And ovEndDateTime > stEndDateTime Then
                    stayTime = stayTime + (New TimeSpan(stEndDateTime.Ticks) - New TimeSpan(stStartDateTime.Ticks)).TotalMinutes
                    Dim spanTime = (New TimeSpan(stEndDateTime.Ticks) - New TimeSpan(stStartDateTime.Ticks)).TotalMinutes
                    nowAddTime = nowAddTime - spanTime
                End If
            End If
        Next


        '留守 stStartDateTime stEndDateTime
        '加班 ovStartDateTime ovEndDateTime


        '需考慮留守跨日 '需跑兩個table



        '現有六種情形 兩個重複
        ' 留守完全覆蓋加班 if stStartDateTime<ovStartDateTime and stEndDateTime>ovEndDateTime   
        '                       -->     StayTime=nowAddTime   nowAddTime=0
        '
        '
        '
        ' 留守覆蓋加班後段 if stStartDateTime<=ovStartDateTime and stEndDateTime<ovEndDateTime
        '                       -->   StayTime=ovStartDateTime~stEndDateTime 
        '                       -->   NowAddTime=NowAddTime-StayTime
        '
        ' 留守覆蓋加班前段 if stStartDateTime>ovStartDateTime and stEndDateTime>=ovEndDateTime
        '                       -->   StayTime=stStartDateTime~ovEndDateTime
        '                       -->   NowAddTime=NowAddTime-StayTime
        ' 加班完全覆蓋留守 if ovStartDateTime<stStartDateTime and ovEndDateTime>stEndDateTime
        '                       -->    StayTime =stStartDateTime~stEndDateTime
        '                       -->    NowAddTime=NowAddTime-StayTime       
        '

        stayTimeAndNowAddTime(0) = stayTime
        stayTimeAndNowAddTime(1) = nowAddTime
        Return stayTimeAndNowAddTime
        'TESTEnd If
    End Function


    ''' <summary>
    ''' 整合時段重複
    ''' </summary>
    ''' <param name="dt"></param>
    ''' 要檢核的dataTable
    ''' <param name="StartDate"></param>
    ''' StartDate is yyyy/MM/dd
    ''' <param name="EndDate"></param>
    ''' StartDate is yyyy/MM/dd
    ''' <param name="StartTime"></param>
    ''' HHmm
    ''' <param name="EndTime"></param>
    ''' HHmm
    ''' <param name="dtStartDateName"></param>
    ''' Data
    ''' <param name="dtEndDateName"></param>
    ''' <param name="dtStartTimeName"></param>
    ''' <param name="dtEndTimeName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function isTimeCover(ByVal dt As DataTable, ByVal CompID As String, ByVal EmpID As String, ByVal StartDate As String, ByVal EndDate As String, ByVal StartTime As String, ByVal EndTime As String, ByVal dtCompIDName As String, ByVal dtEmpIDName As String, ByVal dtStartDateName As String, ByVal dtEndDateName As String, ByVal dtStartTimeName As String, ByVal dtEndTimeName As String)
        Dim StartDateTime As DateTime = DateTime.ParseExact(StartDate + " " + StartTime, "yyyy/MM/dd HHmm", CultureInfo.InvariantCulture)
        Dim EndDateTime As DateTime = DateTime.ParseExact(EndDate + " " + EndTime, "yyyy/MM/dd HHmm", CultureInfo.InvariantCulture)

        For i = 0 To dt.Rows.Count - 1
            Dim dtEmpID As String = dt.Rows(i).Item(dtEmpIDName)
            Dim dtCompID As String = dt.Rows(i).Item(dtCompIDName)
            Dim dtStartDate As Date = dt.Rows(i).Item(dtStartDateName)
            Dim dtStartTime As String = dt.Rows(i).Item(dtStartTimeName)
            Dim dtEndDate As Date = dt.Rows(i).Item(dtEndDateName)
            Dim dtEndTime As String = dt.Rows(i).Item(dtEndTimeName)

            Dim dtStartDateTime As DateTime = DateTime.ParseExact(dtStartDate.ToString("yyyy/MM/dd") + " " + dtStartTime, "yyyy/MM/dd HHmm", CultureInfo.InvariantCulture)
            Dim dtEndDateTime As DateTime = DateTime.ParseExact(dtEndDate.ToString("yyyy/MM/dd") + " " + dtEndTime, "yyyy/MM/dd HHmm", CultureInfo.InvariantCulture)
            '當員工id一樣且公司一樣
            If (dtEmpID.Equals(EmpID) And dtCompID.Equals(CompID)) Then
                'DataTable裡的DateTime完全覆蓋傳入的DateTime
                If dtStartDateTime <= StartDateTime And dtEndDateTime >= EndDateTime Then
                    Return True
                ElseIf dtStartDateTime <= StartDateTime And dtEndDateTime < EndDateTime And StartDateTime < dtEndDateTime Then
                    Return True
                ElseIf dtStartDateTime > StartDateTime And dtEndDateTime >= EndDateTime And dtStartDateTime < EndDateTime Then
                    Return True
                ElseIf StartDateTime < dtStartDateTime And EndDateTime > dtEndDateTime Then
                    Return True
                End If
            End If
        Next
        Return False
        'TESTEnd If
    End Function

    Public Function getAftColumnNameList() As ArrayList
        Dim AftColumnNameList As ArrayList = New ArrayList()
        AftColumnNameList.Add("OTCompID")
        AftColumnNameList.Add("OTEmpID")
        AftColumnNameList.Add("OTStartDate")
        AftColumnNameList.Add("OTEndDate")
        AftColumnNameList.Add("OTStartTime")
        AftColumnNameList.Add("OTEndTime")
        AftColumnNameList.Add("OTTxnID")
        AftColumnNameList.Add("OTSeqNo")
        AftColumnNameList.Add("MealFlag")
        AftColumnNameList.Add("MealTime")
        AftColumnNameList.Add("OTStatus")
        AftColumnNameList.Add("OTSalaryPaid")
        AftColumnNameList.Add("HolidayOrNot")
        Return AftColumnNameList

    End Function

    '注意 dt要先排序
    ''' <summary>
    ''' 計算累加的時數
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <param name="BefColumnNameList"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' 時段Adapter 有三種
    ''' 一.給予一般的以單來算 getTimeIntervalAdapter
    ''' 
    ''' 二.以日來算累加時段 getTimeIntervalAdapterFor4001,getTimeIntervalAdapterFor2001
    ''' 
    ''' 三.即時傳入時間並以日來算  getTimeIntervalAdapterFor4002
    ''' </remarks>
    Public Function getTimeIntervalAdapter(ByVal dt As DataTable, ByVal BefColumnNameList As ArrayList) As DataTable
        Dim checkEmpID As String = ""
        Dim data_Period As DataTable = doThrow(Type) '存放時段的Table
        Dim AftColumnNameList As ArrayList = getAftColumnNameList()
        For i = 0 To BefColumnNameList.Count - 1
            If Not (dt.Columns((BefColumnNameList.Item(i)).ToString).ColumnName.Equals(AftColumnNameList.Item(i).ToString)) Then
                dt.Columns((BefColumnNameList.Item(i)).ToString).ColumnName = (AftColumnNameList.Item(i)).ToString
            End If
        Next

        'dt產新的Columns Time_one,Time_two,Time_three
        dt.Columns.Add("Time_one", System.Type.GetType("System.String"))
        dt.Columns.Add("Time_two", System.Type.GetType("System.String"))
        dt.Columns.Add("Time_three", System.Type.GetType("System.String"))
        'dt.Columns.Add("Stay_Time", System.Type.GetType("System.String"))
        For j = 0 To dt.Rows.Count - 1
            dt.Rows(j).Item("Time_one") = "_"
            dt.Rows(j).Item("Time_two") = "_"
            dt.Rows(j).Item("Time_three") = "_"
            'dt.Rows(j).Item("Stay_Time") = "_"
        Next

        '跑table loop 然後 把主鍵一樣的放入時段
        For i = 0 To data_Period.Rows.Count - 1

            Dim daOTEmpID As String = data_Period.Rows(i).Item("EmpID").ToString
            Dim daOTTxnID As String = data_Period.Rows(i).Item("TxnID").ToString
            Dim daOTSeqNo As String = data_Period.Rows(i).Item("SeqNo").ToString

            For j = 0 To dt.Rows.Count - 1
                Dim dtOTEmpID As String = dt.Rows(j).Item("OTEmpID").ToString
                Dim dtOTTxnID As String = dt.Rows(j).Item("OTTxnID").ToString
                Dim dtOTSeqNo As String = dt.Rows(j).Item("OTSeqNo").ToString
                If (dtOTEmpID.Equals(daOTEmpID) And dtOTTxnID.Equals(daOTTxnID) And dtOTSeqNo.Equals(daOTSeqNo)) Then
                    'IF
                    dt.Rows(j).Item("Time_one") = data_Period.Rows(i).Item("Time_one")
                    dt.Rows(j).Item("Time_two") = data_Period.Rows(i).Item("Time_two")
                    dt.Rows(j).Item("Time_three") = data_Period.Rows(i).Item("Time_three")
                    'dt.Rows(j).Item("Stay_Time") = data_Period.Rows(i).Item("Stay_Time")

                    'ElseIf
                    'End If
                End If
            Next
        Next

        '改回來
        For i = 0 To BefColumnNameList.Count - 1
            dt.Columns((AftColumnNameList.Item(i)).ToString).ColumnName = (BefColumnNameList.Item(i)).ToString
        Next
        Return dt
    End Function

    '注意 dt要先排序
    ''' <summary>
    ''' 計算累加的時數
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <param name="BefColumnNameList"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' 時段Adapter 有三種
    ''' 一.給予一般的以單來算 getTimeIntervalAdapter
    ''' 
    ''' 二.以日來算累加時段 getTimeIntervalAdapterFor4001,getTimeIntervalAdapterFor2001
    ''' 
    ''' 三.即時傳入時間並以日來算  getTimeIntervalAdapterFor4002
    ''' </remarks>
    Public Function getTimeIntervalAdapterFor4001(ByVal dt As DataTable, ByVal BefColumnNameList As ArrayList) As DataTable
        Dim checkEmpID As String = ""
        Dim data_Period As DataTable = doThrow(Type) '存放時段的Table
        Dim AftColumnNameList As ArrayList = getAftColumnNameList()
        For i = 0 To BefColumnNameList.Count - 1
            If Not (dt.Columns((BefColumnNameList.Item(i)).ToString).ColumnName.Equals(AftColumnNameList.Item(i).ToString)) Then
                dt.Columns((BefColumnNameList.Item(i)).ToString).ColumnName = (AftColumnNameList.Item(i)).ToString
            End If
        Next

        'dt產新的Columns Time_one,Time_two,Time_three
        dt.Columns.Add("Time_one", System.Type.GetType("System.String"))
        dt.Columns.Add("Time_two", System.Type.GetType("System.String"))
        dt.Columns.Add("Time_three", System.Type.GetType("System.String"))
        ' dt.Columns.Add("Stay_Time", System.Type.GetType("System.String"))
        For j = 0 To dt.Rows.Count - 1
            dt.Rows(j).Item("Time_one") = "_"
            dt.Rows(j).Item("Time_two") = "_"
            dt.Rows(j).Item("Time_three") = "_"
            'dt.Rows(j).Item("Stay_Time") = "_"
        Next

        '跑table loop 然後 把員工編號 加班起始日 一樣的累加起來
        For i = 0 To data_Period.Rows.Count - 1

            Dim daOTEmpID As String = data_Period.Rows(i).Item("EmpID").ToString
            Dim daDate As String = data_Period.Rows(i).Item("Date").ToString
            ' Dim daOTSeqNo As String = data_Period.Rows(i).Item("SeqNo").ToString

            For j = 0 To dt.Rows.Count - 1
                Dim dtOTEmpID As String = dt.Rows(j).Item("OTEmpID").ToString
                Dim dtStartTime As String = dt.Rows(j).Item("OTStartDate").ToString
                ' Dim dtOTSeqNo As String = dt.Rows(j).Item("OTSeqNo").ToString
                If (dtOTEmpID.Equals(daOTEmpID) And dtStartTime.Equals(daDate)) Then
                    If "_".Equals(dt.Rows(j).Item("Time_one")) Then
                        dt.Rows(j).Item("Time_one") = "0"
                    End If
                    If "_".Equals(dt.Rows(j).Item("Time_two")) Then
                        dt.Rows(j).Item("Time_two") = "0"
                    End If
                    If "_".Equals(dt.Rows(j).Item("Time_three")) Then
                        dt.Rows(j).Item("Time_three") = "0"
                    End If
                    'If "_".Equals(dt.Rows(j).Item("Stay_Time")) Then
                    '    dt.Rows(j).Item("Stay_Time") = "0"
                    'End If
                    dt.Rows(j).Item("Time_one") = (Convert.ToDouble(dt.Rows(j).Item("Time_one")) + Convert.ToDouble(data_Period.Rows(i).Item("Time_one"))).ToString
                    dt.Rows(j).Item("Time_two") = (Convert.ToDouble(dt.Rows(j).Item("Time_two")) + Convert.ToDouble(data_Period.Rows(i).Item("Time_two"))).ToString
                    dt.Rows(j).Item("Time_three") = (Convert.ToDouble(dt.Rows(j).Item("Time_three")) + Convert.ToDouble(data_Period.Rows(i).Item("Time_three"))).ToString
                    ' dt.Rows(j).Item("Stay_Time") = (Convert.ToDouble(dt.Rows(j).Item("Stay_Time")) + Convert.ToDouble(data_Period.Rows(i).Item("Stay_Time"))).ToString
                End If
            Next
        Next

        '改回來
        For i = 0 To BefColumnNameList.Count - 1
            dt.Columns((AftColumnNameList.Item(i)).ToString).ColumnName = (BefColumnNameList.Item(i)).ToString
        Next
        Return dt
    End Function

    '注意 dt要先排序
    ''' <summary>
    ''' 計算累加的時數並即時修改
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <param name="BefColumnNameList"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' 時段Adapter 有三種
    ''' 一.給予一般的以單來算 getTimeIntervalAdapter
    ''' 
    ''' 二.以日來算累加時段 getTimeIntervalAdapterFor4001,getTimeIntervalAdapterFor2001
    ''' 
    ''' 三.即時傳入時間並以日來算  getTimeIntervalAdapterFor4002
    ''' </remarks>
    Public Function getTimeIntervalAdapterFor4002Time(ByVal dt As DataTable, ByVal BefColumnNameList As ArrayList, ByVal OTCompID As String, ByVal OTEmpID As String, ByVal OTStartDate As String, ByVal OTEndDate As String, ByVal OTStartTime As String, ByVal OTEndTime As String, ByVal OTSeq As String, ByVal OTTxnID As String, ByVal mealFlag As String, ByVal mealTime As String) As DataTable
        Dim checkEmpID As String = ""
        Dim data_Period As DataTable = doThrowFor4002Time(Type, OTCompID, OTEmpID, OTStartDate, OTEndDate, OTStartTime, OTEndTime, OTSeq, OTTxnID, mealFlag, mealTime) '存放時段的Table
        Dim AftColumnNameList As ArrayList = getAftColumnNameList()
        For i = 0 To BefColumnNameList.Count - 1
            If Not (dt.Columns((BefColumnNameList.Item(i)).ToString).ColumnName.Equals(AftColumnNameList.Item(i).ToString)) Then
                dt.Columns((BefColumnNameList.Item(i)).ToString).ColumnName = (AftColumnNameList.Item(i)).ToString
            End If
        Next

        'dt產新的Columns Time_one,Time_two,Time_three
        dt.Columns.Add("Time_one", System.Type.GetType("System.String"))
        dt.Columns.Add("Time_two", System.Type.GetType("System.String"))
        dt.Columns.Add("Time_three", System.Type.GetType("System.String"))

        For j = 0 To dt.Rows.Count - 1
            dt.Rows(j).Item("Time_one") = "_"
            dt.Rows(j).Item("Time_two") = "_"
            dt.Rows(j).Item("Time_three") = "_"

        Next

        '跑table loop 然後 把員工編號 加班起始日 一樣的累加起來
        For i = 0 To data_Period.Rows.Count - 1

            Dim daOTEmpID As String = data_Period.Rows(i).Item("EmpID").ToString
            Dim daDate As String = data_Period.Rows(i).Item("Date").ToString
            ' Dim daOTSeqNo As String = data_Period.Rows(i).Item("SeqNo").ToString

            For j = 0 To dt.Rows.Count - 1
                Dim dtOTEmpID As String = dt.Rows(j).Item("OTEmpID").ToString
                Dim dtStartTime As String = dt.Rows(j).Item("OTStartDate").ToString
                ' Dim dtOTSeqNo As String = dt.Rows(j).Item("OTSeqNo").ToString
                If (dtOTEmpID.Equals(daOTEmpID) And dtStartTime.Equals(daDate)) Then
                    If "_".Equals(dt.Rows(j).Item("Time_one")) Then
                        dt.Rows(j).Item("Time_one") = "0"
                    End If
                    If "_".Equals(dt.Rows(j).Item("Time_two")) Then
                        dt.Rows(j).Item("Time_two") = "0"
                    End If
                    If "_".Equals(dt.Rows(j).Item("Time_three")) Then
                        dt.Rows(j).Item("Time_three") = "0"
                    End If

                    dt.Rows(j).Item("Time_one") = (Convert.ToDouble(dt.Rows(j).Item("Time_one")) + Convert.ToDouble(data_Period.Rows(i).Item("Time_one"))).ToString
                    dt.Rows(j).Item("Time_two") = (Convert.ToDouble(dt.Rows(j).Item("Time_two")) + Convert.ToDouble(data_Period.Rows(i).Item("Time_two"))).ToString
                    dt.Rows(j).Item("Time_three") = (Convert.ToDouble(dt.Rows(j).Item("Time_three")) + Convert.ToDouble(data_Period.Rows(i).Item("Time_three"))).ToString

                End If
            Next
        Next

        '改回來
        For i = 0 To BefColumnNameList.Count - 1
            dt.Columns((AftColumnNameList.Item(i)).ToString).ColumnName = (BefColumnNameList.Item(i)).ToString
        Next
        Return dt
    End Function

#Region "時段計算"
    Public Sub getTime_Interval(ByRef Table_Time As DataTable, ByVal compID As String, ByVal empID As String, ByVal SDate As String, ByVal EDate As String, ByVal STime As String, ByVal ETime As String, ByVal seq As String, ByVal txnID As String, ByVal seqNo As String, ByVal deptID As String, ByVal organID As String, ByVal deptName As String, ByVal organName As String, ByVal mealFlag As String, ByVal mealTime As Double, ByVal dayType As String, ByVal disasterDataTable() As DataTable)
        Dim beforeDate_Acc As Double = 0        '前一天的累積時間，計算跨日使用
        Dim beforeDateType As String = ""       '前一天時否為營業日或假日
        Dim beforeDate As String = ""           '前一天的日期
        Dim beforeDateAcross As String = ""     '前一天的加班是否有跨日過來的單
        Dim nowDateAcross As String = ""        '當天是否是有跨日過來
        Dim nowAddTime As Double = 0            '當天加班時間
        Dim nowDate_Acc As Double = 0           '當天的累積時間
        Dim time_Period(3) As Double            '時段<一>、<二>、<三>
        Dim laveMealTime As Integer = 0         '剩餘的吃飯時間
        '跨日時段誤差
        Dim timeM_acc As Double = 0             '加班分鐘數(跨日需要用到)
        Dim beforeTimeM_Acc As Double = 0           '計算加班總分鐘(跨日需要用到)

        '先清空時段
        time_Period = clearArray()

        Dim isTodayAcross As Boolean = False     '判斷今天是否有跨日過來的單
        Dim isYsdayAcross As Boolean = False     '判斷昨天是否有跨日過來的單

        Try
            If Table_Time.Rows.Count > 0 Then

                '判斷現在計算的單號是不是跨日過來的單號
                If seqNo = "2" Then
                    '當天跨日且前一天沒有跨日的狀況
                    Dim tempTime1 As Double = 0
                    Dim tempTime2 As Double = 0
                    Dim tempTime3 As Double = 0
                    '當天跨日且前一天有跨日的狀況
                    Dim tempTimeAcc1 As Double = 0
                    Dim tempTimeAcc2 As Double = 0
                    Dim tempTimeAcc3 As Double = 0

                    For i As Integer = 0 To Table_Time.Rows.Count - 1 Step 1
                        If txnID = Table_Time.Rows(i).Item("TxnID").ToString And empID = Table_Time.Rows(i).Item("EmpID").ToString Then '一定只有一筆
                            beforeDate = Table_Time.Rows(i).Item("Date")
                            tempTime1 = Table_Time.Rows(i).Item("Time_one")
                            tempTime2 = Table_Time.Rows(i).Item("Time_two")
                            tempTime3 = Table_Time.Rows(i).Item("Time_three")
                            tempTimeAcc1 = Table_Time.Rows(i).Item("TimeAcc_one")
                            tempTimeAcc2 = Table_Time.Rows(i).Item("TimeAcc_two")
                            tempTimeAcc3 = Table_Time.Rows(i).Item("TimeAcc_three")
                            'laveMealTime = CInt(Table_Time.Rows(i).Item("LaveMealTime")) '剩餘吃飯時間
                            beforeDate_Acc = Table_Time.Rows(i).Item("Time_acc").ToString
                            beforeTimeM_Acc = Table_Time.Rows(i).Item("TimeM_acc").ToString
                            beforeDateType = Table_Time.Rows(i).Item("HolidayOrNot").ToString
                            beforeDateAcross = Table_Time.Rows(i).Item("DayAcross").ToString
                        End If
                    Next
                    If tempTime1 = 0 And tempTime2 = 0 And tempTime3 = 0 Then
                        tempTimeAcc1 = 0
                        tempTimeAcc2 = 0
                        tempTimeAcc3 = 0
                        beforeDate_Acc = 0
                    End If
                    '有跨日的單號
                    isTodayAcross = True
                    If beforeDateAcross = "T" Then
                        isYsdayAcross = True
                        time_Period(0) = tempTimeAcc1
                        time_Period(1) = tempTimeAcc2
                        time_Period(2) = tempTimeAcc3
                    Else
                        time_Period(0) = tempTime1
                        time_Period(1) = tempTime2
                        time_Period(2) = tempTime3
                    End If
                    'If laveMealTime <> 0 Then
                    '    mealFlag = "1"
                    '    mealTime = laveMealTime
                    'End If
                End If

                '判斷當天，現在加班時間之前有沒有其他加班單        '1.可能跑多次的問題 2.可能要累加時段
                If isTodayAcross <> True Then                      '3.跨日過來的一定是當天最早的單，最開始的一筆最開始的判斷就不會進來了
                    For i As Integer = 0 To Table_Time.Rows.Count - 1 Step 1
                        '可能有當天的加班單或是當天有的加班單但以計薪
                        If (SDate = Table_Time.Rows(i).Item("Date").ToString And STime >= Table_Time.Rows(i).Item("StartTime").ToString And empID = Table_Time.Rows(i).Item("EmpID") And "0" = Table_Time.Rows(i).Item("SalaryPaid")) Or (SDate = Table_Time.Rows(i).Item("Date").ToString And empID = Table_Time.Rows(i).Item("EmpID") And "1" = Table_Time.Rows(i).Item("SalaryPaid")) Then
                            If Table_Time.Rows(i).Item("DayAcross").ToString = "T" Then '有跨日
                                time_Period(0) = Table_Time.Rows(i).Item("TimeAcc_one")
                                time_Period(1) = Table_Time.Rows(i).Item("TimeAcc_two")
                                time_Period(2) = Table_Time.Rows(i).Item("TimeAcc_three")
                                nowDate_Acc = Table_Time.Rows(i).Item("Time_acc")
                                nowDateAcross = Table_Time.Rows(i).Item("DayAcross")
                                '有跨日的單號
                                isTodayAcross = True
                            Else    '條件判斷的最後一筆數據
                                time_Period(0) = Table_Time.Rows(i).Item("Time_one")
                                time_Period(1) = Table_Time.Rows(i).Item("Time_two")
                                time_Period(2) = Table_Time.Rows(i).Item("Time_three")
                                nowDate_Acc = Table_Time.Rows(i).Item("Time_acc")
                                nowDateAcross = Table_Time.Rows(i).Item("DayAcross")
                            End If
                        End If
                    Next
                End If
            End If
            '時間轉換時段 
            nowAddTime = DateDiff("n", STime, ETime)                  '時間差/分鐘
            If ETime = "23:59" Then                                   '假如是跨日單，需要加一分鐘回去
                nowAddTime = nowAddTime + 1
            End If
            If mealFlag = "1" And mealTime <> 0 Then                  'mealFlag為1代表要扣除用餐時間

                nowAddTime = nowAddTime - mealTime
                'ElseIf nowAddTime < mealTime Then                     '當吃飯時間超過第一張單的時間(跨日會有的狀況)
                '    laveMealTime = mealTime - nowAddTime
                '    nowAddTime = 0
            End If

            timeM_acc = nowAddTime                                    '加班單分鐘
            '最後換算成小時
            nowAddTime = CDbl(FormatNumber((nowAddTime / 60), 1))            '加班幾分鐘/60分 = 小時

            '判斷************************************************
            If seqNo = "2" Then
                Dim result_Count As Double = 0
                Dim totalTime As Double = timeM_acc + beforeTimeM_Acc
                Dim beforeTimeM As Double = 0
                totalTime = CDbl(FormatNumber((totalTime / 60), 1))
                beforeTimeM = CDbl(FormatNumber((beforeTimeM_Acc / 60), 1))
                result_Count = totalTime - beforeTimeM
                result_Count = CDbl(FormatNumber(result_Count, 1))
                If result_Count <> nowAddTime Then
                    nowAddTime = result_Count
                End If
            End If

            If dayType = "T" Then '營業日
                If nowDate_Acc = 0 And isYsdayAcross <> True Then
                    If nowAddTime < 2 And seqNo = "1" And isTodayAcross <> True Then
                        time_Period = clearArray()
                        time_Period(0) = nowAddTime
                    ElseIf nowAddTime >= 2 And seqNo = "1" And isTodayAcross <> True Then
                        time_Period = clearArray()
                        time_Period(0) = 2
                        time_Period(1) = nowAddTime - 2
                    ElseIf seqNo = "2" And beforeDateType = "T" And isTodayAcross Then '平日跨日且前一天沒有跨日過來的的加班單的情況
                        If (beforeDate_Acc + nowAddTime) < 2 Then
                            time_Period = clearArray()
                            time_Period(0) = nowAddTime


                            '當之前時間加上現在時間大於2且小於4那應該從時段二開始算
                            '時段二等於之前時間加上現在時間-2
                            '時段一? 應該為0吧? 例如beforeDate_Acc=2.8  nowAddTime=1 那會變成 2-2.8=-0.8  3.8-2=1.8
                        ElseIf (beforeDate_Acc + nowAddTime) >= 2 And (beforeDate_Acc + nowAddTime) <= 4 Then
                            If beforeDate_Acc <= 2 Then
                                time_Period = clearArray()
                                time_Period(0) = 2 - beforeDate_Acc
                                time_Period(1) = (beforeDate_Acc + nowAddTime) - 2
                            ElseIf beforeDate_Acc > 2 Then
                                time_Period = clearArray()
                                time_Period(1) = nowAddTime
                            End If
                        ElseIf beforeDate_Acc + nowAddTime > 4 Then
                            If (beforeDate_Acc - time_Period(1)) < 2 Then
                                time_Period = clearArray()
                                time_Period(0) = nowAddTime - 2
                                time_Period(1) = 2
                            ElseIf (beforeDate_Acc - time_Period(1)) >= 2 Then
                                If nowAddTime < 2 Then
                                    time_Period = clearArray()
                                    time_Period(1) = nowAddTime
                                ElseIf nowAddTime >= 2 Then
                                    time_Period = clearArray()
                                    time_Period(0) = nowAddTime - 2
                                    time_Period(1) = 2
                                End If
                            End If
                        End If
                    ElseIf seqNo = "2" And beforeDateType = "F" And isTodayAcross Then '假日跨日且前一天沒有跨日過來的的加班單的情況
                        If beforeDate_Acc < 2 Then
                            If beforeDate_Acc + nowAddTime < 2 Then
                                time_Period = clearArray()
                                time_Period(0) = nowAddTime
                            ElseIf beforeDate_Acc + nowAddTime >= 2 And beforeDate_Acc + nowAddTime <= 4 Then
                                If beforeDate_Acc <= 2 Then
                                    time_Period = clearArray()
                                    time_Period(0) = 2 - beforeDate_Acc
                                    time_Period(1) = (beforeDate_Acc + nowAddTime) - 2
                                ElseIf beforeDate_Acc > 2 Then
                                    time_Period = clearArray()
                                    time_Period(1) = nowAddTime
                                End If
                            ElseIf beforeDate_Acc + nowAddTime > 4 Then
                                time_Period = clearArray()
                                time_Period(0) = nowAddTime - 2
                                time_Period(1) = 2
                            End If
                        ElseIf beforeDate_Acc >= 2 Then
                            If nowAddTime < 2 Then
                                time_Period = clearArray()
                                time_Period(1) = nowAddTime
                            ElseIf nowAddTime >= 2 Then
                                time_Period = clearArray()
                                time_Period(0) = nowAddTime - 2
                                time_Period(1) = 2
                            End If
                        End If
                    End If
                ElseIf nowDate_Acc <> 0 And nowDate_Acc <= 2 And isTodayAcross <> True Then
                    If nowDate_Acc + nowAddTime < 2 Then
                        time_Period = clearArray()
                        time_Period(0) = nowAddTime
                    ElseIf nowDate_Acc + nowAddTime >= 2 And nowDate_Acc + nowAddTime <= 4 Then
                        time_Period = clearArray()
                        time_Period(0) = 2 - nowDate_Acc
                        time_Period(1) = (nowDate_Acc + nowAddTime) - 2
                    End If
                ElseIf nowDate_Acc > 2 And isTodayAcross <> True Then
                    time_Period = clearArray()
                    time_Period(1) = nowAddTime
                End If

                '有跨日過來的計算
                If (isTodayAcross And seqNo = "1") Or (isYsdayAcross And seqNo = "2") Then
                    If isYsdayAcross And seqNo = "2" Then
                        nowDate_Acc = beforeDate_Acc
                    End If

                    If (time_Period(0) <> 0 And time_Period(1) = 0) Or (time_Period(0) <> 0 And time_Period(1) <> 0) Then
                        Dim time1_Acc As Double = nowDate_Acc - time_Period(1)
                        If time1_Acc + nowAddTime < 2 Then
                            time_Period = clearArray()
                            time_Period(0) = nowAddTime
                        ElseIf time1_Acc + nowAddTime >= 2 And time1_Acc + nowAddTime <= 4 Then
                            time_Period = clearArray()
                            time_Period(0) = 2 - time1_Acc
                            time_Period(1) = (nowAddTime + time1_Acc) - 2
                        ElseIf time1_Acc + nowAddTime > 4 Then
                            time_Period = clearArray()
                            time_Period(0) = nowAddTime - 2
                            time_Period(1) = 2
                        End If
                    ElseIf time_Period(0) = 0 And time_Period(1) <> 0 Then
                        If nowAddTime < 2 Then
                            time_Period = clearArray()
                            time_Period(0) = nowAddTime
                        ElseIf nowAddTime >= 2 Then
                            time_Period = clearArray()
                            time_Period(0) = 2
                            time_Period(1) = nowAddTime - 2
                        End If
                    ElseIf time_Period(0) = 0 And time_Period(1) = 0 And time_Period(2) <> 0 Then
                        If nowDate_Acc < 2 Then
                            If nowDate_Acc + nowAddTime < 2 Then
                                time_Period = clearArray()
                                time_Period(0) = nowAddTime
                            ElseIf nowDate_Acc + nowAddTime >= 2 And nowDate_Acc + nowAddTime <= 4 Then
                                time_Period = clearArray()
                                time_Period(0) = 2 - nowDate_Acc
                                time_Period(1) = (nowDate_Acc + nowAddTime) - 2
                            ElseIf nowDate_Acc + nowAddTime > 4 Then
                                time_Period = clearArray()
                                time_Period(0) = nowAddTime - 2
                                time_Period(1) = 2
                            End If
                        ElseIf nowDate_Acc >= 2 Then
                            If nowAddTime < 2 Then
                                time_Period = clearArray()
                                time_Period(1) = nowAddTime
                            ElseIf nowAddTime >= 2 Then
                                time_Period = clearArray()
                                time_Period(0) = nowAddTime - 2
                                time_Period(1) = 2
                            End If
                        End If
                    ElseIf time_Period(0) = 0 And time_Period(1) = 0 And time_Period(2) = 0 Then
                        If nowDate_Acc = 0 And isYsdayAcross <> True Then
                            If nowAddTime < 2 Then
                                time_Period = clearArray()
                                time_Period(0) = nowAddTime
                            ElseIf nowAddTime >= 2 Then
                                time_Period = clearArray()
                                time_Period(0) = 2
                                time_Period(1) = nowAddTime - 2
                            End If
                        End If
                    End If
                    '要把跨日第一筆的累積清空
                    If isYsdayAcross And seqNo = "2" Then
                        nowDate_Acc = 0
                    End If

                End If

            ElseIf dayType = "F" Then '休假日
                time_Period = clearArray()
                time_Period(2) = nowAddTime
            End If

            '最後寫回Table
            Dim dataRows As DataRow
            dataRows = Table_Time.NewRow
            dataRows("CompID") = compID
            dataRows("TxnID") = txnID
            dataRows("Seq") = seq
            dataRows("SeqNo") = seqNo
            dataRows("EmpID") = empID
            dataRows("StartTime") = STime
            dataRows("EndTime") = ETime
            'dataRows("SalaryOrAdjust") = SalaryOrAdjust
            dataRows("MealFlag") = IIf(mealFlag = "1", "Y", "N")
            dataRows("MealTime") = mealTime
            'dataRows("LaveMealTime") = laveMealTime.ToString
            dataRows("Date") = SDate
            dataRows("Time_one") = time_Period(0)
            dataRows("Time_two") = time_Period(1)
            dataRows("Time_three") = time_Period(2)
            If Table_Time.Rows.Count > 0 Then     ' 第二筆開始累加當日每一筆時段
                Dim tempTime1 As Double = 0
                Dim tempTime2 As Double = 0
                Dim tempTime3 As Double = 0
                For i As Integer = 0 To Table_Time.Rows.Count - 1
                    If SDate = Table_Time.Rows(i).Item("Date").ToString And empID = Table_Time.Rows(i).Item("EmpID") Then    '累加同一天的時段
                        tempTime1 = Table_Time.Rows(i).Item("TimeAcc_one")
                        tempTime2 = Table_Time.Rows(i).Item("TimeAcc_two")
                        tempTime3 = Table_Time.Rows(i).Item("TimeAcc_three")
                    End If
                Next
                If tempTime1 <> 0 Or tempTime2 <> 0 Or tempTime3 <> 0 Then
                    dataRows("TimeAcc_one") = time_Period(0) + tempTime1
                    dataRows("TimeAcc_two") = time_Period(1) + tempTime2
                    dataRows("TimeAcc_three") = time_Period(2) + tempTime3
                Else
                    dataRows("TimeAcc_one") = time_Period(0)
                    dataRows("TimeAcc_two") = time_Period(1)
                    dataRows("TimeAcc_three") = time_Period(2)
                End If
            ElseIf Table_Time.Rows.Count = 0 Then ' 第一筆累加就是第一筆時段的數據
                dataRows("TimeAcc_one") = time_Period(0)
                dataRows("TimeAcc_two") = time_Period(1)
                dataRows("TimeAcc_three") = time_Period(2)
            End If
            dataRows("Time_acc") = nowDate_Acc + time_Period(0) + time_Period(1) + time_Period(2)
            dataRows("TimeM_acc") = timeM_acc
            dataRows("SalaryPaid") = "0"
            dataRows("HolidayOrNot") = dayType
            dataRows("DayAcross") = IIf(isTodayAcross, "T", "F")
            dataRows("DeptID") = deptID
            dataRows("OrganID") = organID
            dataRows("DeptName") = deptName
            dataRows("OrganName") = organName
            Table_Time.Rows.Add(dataRows)

        Catch ex As Exception

            MsgBox("錯誤:" + ex.Message)
        End Try
    End Sub
#End Region
#Region "虛擬Table"
    Public Function getTable_Period() As DataTable
        Dim myTable As New DataTable
        Dim col As DataColumn
        '公司代碼
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "CompID"
        myTable.Columns.Add(col)
        '單號
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "TxnID"
        myTable.Columns.Add(col)
        '序號
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "Seq"
        myTable.Columns.Add(col)
        '單號序列
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "SeqNo"
        myTable.Columns.Add(col)
        '員工編號
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "EmpID"
        myTable.Columns.Add(col)
        '加班開始時間
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "StartTime"
        myTable.Columns.Add(col)
        '加班結束時間
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "EndTime"
        myTable.Columns.Add(col)
        '用餐註記
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "MealFlag"
        myTable.Columns.Add(col)
        '用餐時間
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "MealTime"
        myTable.Columns.Add(col)
        '剩餘用餐時間
        'col = New DataColumn
        'col.DataType = System.Type.GetType("System.String")
        'col.ColumnName = "LaveMealTime"
        'myTable.Columns.Add(col)
        '日期=>開始跟結束現在都一樣
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "Date"
        myTable.Columns.Add(col)
        '時段<一>
        col = New DataColumn
        col.DataType = System.Type.GetType("System.Double")
        col.ColumnName = "Time_one"
        myTable.Columns.Add(col)
        '時段<一>累加
        col = New DataColumn
        col.DataType = System.Type.GetType("System.Double")
        col.ColumnName = "TimeAcc_one"
        myTable.Columns.Add(col)
        '時段<二>
        col = New DataColumn
        col.DataType = System.Type.GetType("System.Double")
        col.ColumnName = "Time_two"
        myTable.Columns.Add(col)
        '時段<二>累加
        col = New DataColumn
        col.DataType = System.Type.GetType("System.Double")
        col.ColumnName = "TimeAcc_two"
        myTable.Columns.Add(col)
        '時段<三>
        col = New DataColumn
        col.DataType = System.Type.GetType("System.Double")
        col.ColumnName = "Time_three"
        myTable.Columns.Add(col)
        '時段<三>累加
        col = New DataColumn
        col.DataType = System.Type.GetType("System.Double")
        col.ColumnName = "TimeAcc_three"
        myTable.Columns.Add(col)
        ''留守時段
        'col = New DataColumn
        'col.DataType = System.Type.GetType("System.Double")
        'col.ColumnName = "Stay_Time"
        'myTable.Columns.Add(col)
        ''留守時段累加
        'col = New DataColumn
        'col.DataType = System.Type.GetType("System.Double")
        'col.ColumnName = "Stay_TimeAcc"
        'myTable.Columns.Add(col)
        '累積時間
        col = New DataColumn
        col.DataType = System.Type.GetType("System.Double")
        col.ColumnName = "Time_acc"
        myTable.Columns.Add(col)
        '累積時間(分鐘)
        col = New DataColumn
        col.DataType = System.Type.GetType("System.Double")
        col.ColumnName = "TimeM_acc"
        myTable.Columns.Add(col)
        '記薪註記
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "SalaryPaid"
        myTable.Columns.Add(col)
        '假日註記
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "HolidayOrNot"
        myTable.Columns.Add(col)
        '跨日註記
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "DayAcross"
        myTable.Columns.Add(col)
        '部門代號
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "DeptID"
        myTable.Columns.Add(col)
        '科組課代號
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "OrganID"
        myTable.Columns.Add(col)
        '部門名稱
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "DeptName"
        myTable.Columns.Add(col)
        '科組課名稱
        col = New DataColumn
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "OrganName"
        myTable.Columns.Add(col)

        Return myTable
    End Function
#End Region

#Region "檢查OverTime有無跨月資料"
    Public Sub checkOldData(ByRef Table_Time As DataTable, ByVal compID As String, ByVal empID As String, ByVal sDate As String)
        Dim checkData As DataTable
        Dim checkData_Count As Integer
        Dim strSQL_Check As New StringBuilder()

        strSQL_Check.Append("select Seq,BeginTime,EndTime,OTNormal,OTOver,OTHoliday,HolidayOrNot,DeptID,OrganID,DeptName,OrganName from OverTime")
        strSQL_Check.Append(" where 1=1")
        strSQL_Check.Append(" And CompID =" & Bsp.Utility.Quote(compID))
        strSQL_Check.Append(" And EmpID =" & Bsp.Utility.Quote(empID))
        strSQL_Check.Append(" And OTDate =" & Bsp.Utility.Quote(sDate))
        strSQL_Check.Append(" order by BeginTime")
        checkData = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL_Check.ToString(), "eHRMSDB").Tables(0)

        checkData_Count = checkData.Rows.Count
        If checkData_Count <> 0 Then
            Dim timeAcc As Double = 0
            Dim time1 As Double = 0
            Dim time2 As Double = 0
            Dim time3 As Double = 0
            Dim time1Acc As Double = 0
            Dim time2Acc As Double = 0
            Dim time3Acc As Double = 0
            Dim dayAcross As Boolean = False
            Dim SeqNo As String = "1"

            For i As Integer = 0 To checkData_Count - 1 Step 1
                Dim Seq As String = checkData.Rows(i).Item("Seq").ToString
                Dim STime As String = checkData.Rows(i).Item("BeginTime").ToString
                Dim ETime As String = checkData.Rows(i).Item("EndTime").ToString
                Dim dayType As String = checkData.Rows(i).Item("HolidayOrNot").ToString
                Dim deptID As String = checkData.Rows(i).Item("DeptID").ToString
                Dim organID As String = checkData.Rows(i).Item("OrganID").ToString
                Dim deptName As String = checkData.Rows(i).Item("DeptName").ToString
                Dim organName As String = checkData.Rows(i).Item("OrganName").ToString
                time1 = CDbl(checkData.Rows(i).Item("OTNormal"))
                time2 = CDbl(checkData.Rows(i).Item("OTOver"))
                time3 = CDbl(checkData.Rows(i).Item("OTHoliday"))
                time1Acc += CDbl(checkData.Rows(i).Item("OTNormal"))
                time2Acc += CDbl(checkData.Rows(i).Item("OTOver"))
                time3Acc += CDbl(checkData.Rows(i).Item("OTHoliday"))
                timeAcc = timeAcc + time1 + time2 + time3
                If STime = "0000" Then
                    dayAcross = True
                    SeqNo = "2"
                End If

                Dim dataRows As DataRow
                dataRows = Table_Time.NewRow
                dataRows("CompID") = compID
                dataRows("TxnID") = compID + empID + sDate + Seq
                'dataRows("Seq") = Seq
                dataRows("SeqNo") = SeqNo
                dataRows("EmpID") = empID
                dataRows("StartTime") = STime
                dataRows("EndTime") = ETime
                dataRows("MealFlag") = "N"
                dataRows("MealTime") = "0"
                dataRows("Date") = sDate
                dataRows("Time_one") = time1
                dataRows("Time_two") = time2
                dataRows("Time_three") = time3
                dataRows("TimeAcc_one") = time1Acc
                dataRows("TimeAcc_two") = time2Acc
                dataRows("TimeAcc_three") = time3Acc
                dataRows("Time_acc") = timeAcc
                dataRows("SalaryPaid") = "1"
                dataRows("HolidayOrNot") = IIf(dayType = "0", "T", "F")
                dataRows("DayAcross") = IIf(dayAcross, "T", "F")

                Table_Time.Rows.Add(dataRows)
            Next
        End If
    End Sub
#End Region
    Public Function isOTSalaryPaid() As Boolean
        'Dim dt As DataTable = getDataTable()
        'If dt.Rows.Count > 0 Then
        '    If ("1".Equals(dt.Rows(0).Item("OTSalaryPaid"))) Then
        '        Return True
        '    End If
        'End If
        'Return False
    End Function

    Public Function isToOverTime() As Boolean
        Dim dt As DataTable = getDataTable()
        If dt.Rows.Count > 0 Then
            '如果狀態為已拋轉且已核准才做以下動作
            If ("1".Equals(dt.Rows(0).Item("ToOverTimeFlag"))) And ("3".Equals(dt.Rows(0).Item("OTStatus"))) Then
                Return True
            End If
        End If
        Return False
    End Function
    Public Function getDataTable() As DataTable
        Dim StrSpl As StringBuilder = New StringBuilder()
        If Type.Equals("bef") Then
            StrSpl.Append("SELECT [OTCompID],[OTEmpID],[OTStartDate],[OTEndDate],[OTSeq],[OTTxnID],[OTSeqNo],[DeptID],[OrganID],[DeptName],[OrganName],[FlowCaseID],[OTStartTime],[OTEndTime],[OTTotalTime],[SalaryOrAdjust],[AdjustInvalidDate],[MealFlag],[MealTime],[OTTypeID],[OTReasonID],[OTReasonMemo],[OTAttachment],[OTFormNO],[OTRegisterID],[OTRegisterDate],[OTStatus],[HolidayOrNot],[OTValidDate],[OTValidID],[OTRejectDate],[OTRejectID],[OTGovernmentNo],[LastChgComp],[LastChgID],[LastChgDate]FROM [dbo].[OverTimeAdvance]")
            StrSpl.Append(" where '1'='1' and OTCompID=" + Bsp.Utility.Quote(CompID) + "and OTEmpID=" + Bsp.Utility.Quote(OTEmpID) + "and [OTTxnID]=" + Bsp.Utility.Quote(OTTxnID))
        ElseIf Type.Equals("after") Then
            StrSpl.Append("SELECT [OTCompID],[OTEmpID],[OTStartDate],[OTEndDate],[OTSeq],[OTTxnID],[OTSeqNo],[OTFromAdvanceTxnId],[DeptID],[OrganID],[DeptName],[OrganName],[FlowCaseID],[OTStartTime],[OTEndTime],[OTTotalTime],[SalaryOrAdjust],[AdjustInvalidDate],[AdjustStatus],[AdjustDate],[MealFlag],[MealTime],[OTTypeID],[OTReasonID],[OTReasonMemo],[OTAttachment],[OTFormNO],[OTRegisterID],[OTRegisterDate],[OTStatus],[OTValidDate],[OTValidID],[OverTimeFlag],[ToOverTimeDate],[ToOverTimeFlag],[OTRejectDate],[OTRejectID],[OTGovernmentNo],[OTSalaryPaid],[HolidayOrNot],[ProcessDate],[OTPayDate],[OTModifyDate],[OTRemark],[KeyInComp],[KeyInID],[HRKeyInFlag],[LastChgComp],[LastChgID],[LastChgDate]FROM [dbo].[OverTimeDeclaration]")
            StrSpl.Append(" where '1'='1' and OTCompID=" + Bsp.Utility.Quote(CompID) + "and OTEmpID=" + Bsp.Utility.Quote(OTEmpID) + "and [OTTxnID]=" + Bsp.Utility.Quote(OTTxnID))
        Else
            Return Nothing
        End If
        Dim dt = Bsp.DB.ExecuteDataSet(CommandType.Text, StrSpl.ToString(), "AattendantDB").Tables(0)
        Return dt
    End Function

    ''' <summary>
    ''' 修改寫法 20170301
    ''' 倒敘
    ''' 回傳 true 就不做薪資拋轉 
    ''' 回傳 false 就做薪資拋轉 
    ''' 當沒抓到資料就不做薪資拋轉
    ''' </summary>
    ''' <param name="compID"></param>
    ''' <param name="empID"></param>
    ''' <param name="OTDate"></param>
    ''' <param name="BeginTime"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function checkSalaryPaid(ByVal compID As String, ByVal empID As String, ByVal OTDate As String, ByVal BeginTime As String) As Boolean
        Dim strSql As New StringBuilder()
        Dim Flag As Boolean = False

        strSql.AppendLine("SELECT [SalaryPaid] FROM OverTime where [CompID]='" + compID + "' and EmpID='" + empID + "' and OTDate='" + OTDate + "' and BeginTime='" + BeginTime + "'")
        Dim dbs As DataSet = Bsp.DB.ExecuteDataSet(CommandType.Text, strSql.ToString, "eHRMSDB")
        '如果沒拿到資料 不拋轉
        If Not dbs.Tables.Count > 0 Then
            Return True
        End If
        '如果拿到的資料數量為0 不拋轉
        If Not dbs.Tables(0).Rows.Count > 0 Then
            Return True
        End If
        '如果拿到的資料欄位SalaryPaid為1不拋轉 ,不然就需要拋轉
        If "1".Equals(dbs.Tables(0).Rows(0).Item("SalaryPaid")) Then
            Return True
        Else
            Return False
        End If



        '如果有到資料 而且 這筆資料是計薪 回傳true
        'If dbs.Tables.Count > 0 AndAlso dbs.Tables(0).Rows.Count > 0 AndAlso "1".Equals(dbs.Tables(0).Rows(0).Item("SalaryPaid")) Then
        '    Return True
        'Else    '不然 回傳false
        '    Return False
        'End If
    End Function

    'Public Function deleteOverTime(ByVal compID As String, ByVal empID As String, ByVal OTDate As String) As Boolean
    '    Dim strSQL As New StringBuilder() 'ASD
    '    Dim Flag As Boolean = False
    '    strSQL.AppendLine("DELETE FROM [dbo].[OverTime] ")
    '    strSQL.AppendLine(" where '1'='1'")
    '    strSQL.AppendLine(" and CompID='" + compID + "'")
    '    strSQL.AppendLine(" and EmpID='" + empID + "'")
    '    strSQL.AppendLine(" and OTDate='" + OTDate + "'")


    '    Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
    '        cn.Open()
    '        Dim tran As DbTransaction = cn.BeginTransaction
    '        Try
    '            Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran, "eHRMSDB")
    '            tran.Commit()
    '            Flag = True
    '        Catch ex As Exception
    '            tran.Rollback()
    '            Flag = False
    '            Throw
    '        Finally
    '            If tran IsNot Nothing Then tran.Dispose()
    '        End Try
    '    End Using
    '    Return Flag


    'End Function

    Public Function changeOTStatusTo7(ByVal compID As String, ByVal empID As String, ByVal TxnID As String, ByVal userCompID As String, ByVal userID As String) As Boolean
        Dim strSQL As New StringBuilder()
        Dim Flag As Boolean = False

        strSQL.AppendLine("UPDATE [dbo].[OverTimeDeclaration] SET [OTStatus] = '7' ")
        strSQL.AppendLine(" ,LastChgComp = " & Bsp.Utility.Quote(UserProfile.CompID))
        strSQL.AppendLine(" ,LastChgID = " & Bsp.Utility.Quote(UserProfile.UserID))
        strSQL.AppendLine(" ,LastChgDate = getdate()")
        strSQL.AppendLine(" where '1'='1'")
        strSQL.AppendLine(" and OTCompID='" + compID + "'")
        strSQL.AppendLine(" and OTEmpID='" + empID + "'")
        strSQL.AppendLine(" and OTTxnID='" + TxnID + "'")

        '20170219 Beatrice Add 修改事前Table狀態=9
        Dim objField As Object = Bsp.DB.ExecuteScalar("Select Top 1 OTFromAdvanceTxnId From OverTimeDeclaration Where OTCompID='" + compID + "' And OTEmpID='" + empID + "' And OTTxnID='" + TxnID + "'", "AattendantDB")
        Dim OTFromAdvanceTxnId As String = IIf(objField Is Nothing, "", objField.ToString)
        If OTFromAdvanceTxnId <> "" Then
            strSQL.AppendLine(";")
            strSQL.AppendLine(" UPDATE [dbo].[OverTimeAdvance] SET [OTStatus] = '9' ")
            strSQL.AppendLine(" ,LastChgComp = " & Bsp.Utility.Quote(UserProfile.CompID))
            strSQL.AppendLine(" ,LastChgID = " & Bsp.Utility.Quote(UserProfile.UserID))
            strSQL.AppendLine(" ,LastChgDate = getdate()")
            strSQL.AppendLine(" where '1'='1'")
            strSQL.AppendLine(" and OTCompID='" + compID + "'")
            strSQL.AppendLine(" and OTEmpID='" + empID + "'")
            strSQL.AppendLine(" and OTTxnID='" + OTFromAdvanceTxnId + "'")
        End If

        Using cn As DbConnection = Bsp.DB.getConnection("AattendantDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Try
                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran, "AattendantDB")
                tran.Commit()
                Flag = True
            Catch ex As Exception
                tran.Rollback()
                Flag = False
                Throw
            Finally
                If tran IsNot Nothing Then tran.Dispose()
            End Try
        End Using
        Return Flag
    End Function

    Public Function changeOTStatusTo9(ByVal compID As String, ByVal empID As String, ByVal TxnID As String, ByVal userCompID As String, ByVal userID As String) As Boolean
        Dim strSQL As New StringBuilder()
        Dim Flag As Boolean = False
        strSQL.AppendLine("UPDATE [dbo].[OverTimeDeclaration] SET [OTStatus] = '9' ")
        strSQL.AppendLine(" ,LastChgComp = " & Bsp.Utility.Quote(UserProfile.CompID))
        strSQL.AppendLine(" ,LastChgID = " & Bsp.Utility.Quote(UserProfile.UserID))
        strSQL.AppendLine(" ,LastChgDate = getdate()")
        strSQL.AppendLine(" ,OTModifyDate = getdate()")
        strSQL.AppendLine(" where '1'='1'")
        strSQL.AppendLine(" and OTCompID='" + compID + "'")
        strSQL.AppendLine(" and OTEmpID='" + empID + "'")
        strSQL.AppendLine(" and OTTxnID='" + TxnID + "'")
        Using cn As DbConnection = Bsp.DB.getConnection("AattendantDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Try
                Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran, "AattendantDB")
                tran.Commit()
                Flag = True
            Catch ex As Exception
                tran.Rollback()
                Flag = False
                Throw
            Finally
                If tran IsNot Nothing Then tran.Dispose()
            End Try
        End Using
        Return Flag
    End Function
#Region "4002資料更新"
    Public Function DeleteFor4002(ByVal OV_3 As OV_3, ByVal OTSeqNo As String) As Boolean
        Dim SQLString As StringBuilder = New StringBuilder
        Dim DataTable As String
        If "bef".Equals(Type) Then
            DataTable = "OverTimeAdvance"
        Else
            DataTable = "OverTimeDeclaration"
        End If

        Dim Flag As Boolean = False

        SQLString.AppendLine(" delete [dbo]." + DataTable)
        SQLString.AppendLine(" WHERE OTTxnID=" + Bsp.Utility.Quote(OV_3.OTTxnID))
        SQLString.AppendLine(" and OTCompID=" + Bsp.Utility.Quote(OV_3.CompID))
        SQLString.AppendLine(" and OTEmpID=" + Bsp.Utility.Quote(OV_3.OTEmpID))
        If Not "".Equals(OTSeqNo) Then
            SQLString.AppendLine(" and OTSeqNo=" + Bsp.Utility.Quote(OTSeqNo))
        End If


        Using cn As DbConnection = Bsp.DB.getConnection("AattendantDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Try
                Bsp.DB.ExecuteNonQuery(CommandType.Text, SQLString.ToString(), tran, "AattendantDB")
                tran.Commit()
                Flag = True
            Catch ex As Exception
                tran.Rollback()

                Flag = False
                Throw
            Finally
                If tran IsNot Nothing Then tran.Dispose()
            End Try
        End Using
        Return Flag
    End Function
    Public Function InsertFor4002OVA(ByVal OV_3 As OV_3, ByVal oldOVAData As OV4_OVA) As Boolean
        Dim SQLString As StringBuilder = New StringBuilder
        '拆單
        Dim StartDate1 As String = OV_3.OvertimeDateB
        Dim EndDate1 As String = OV_3.OvertimeDateB
        Dim StartTime1 As String = OV_3.OTStartTime
        Dim EndTime1 As String = "2359"

        Dim StartDate2 As String = OV_3.OvertimeDateE
        Dim EndDate2 As String = OV_3.OvertimeDateE
        Dim StartTime2 As String = "0000"
        Dim EndTime2 As String = OV_3.OTEndTime
        '20170302 注意因為跨日所以無條件進位
        Dim OTTotalTime1 As String = (Convert.ToInt32(Me.getOverTime(StartDate1, EndDate1, StartTime1, EndTime1, "M")) + 1).ToString
        Dim OTTotalTime2 As String = (Convert.ToInt32(Me.getOverTime(StartDate2, EndDate2, StartTime2, EndTime2, "M"))).ToString
        Dim Flag As Boolean = False
        Dim mealFlag1 As String = OV_3.mealFlag
        Dim mealFlag2 As String = OV_3.mealFlag
        Dim mealTime1 As String
        Dim mealTime2 As String
        Dim HolidayOrNot1 As String = If(Me.CheckHolidayOrNot(OV_3.OvertimeDateB), "1", "0")
        Dim HolidayOrNot2 As String = If(Me.CheckHolidayOrNot(OV_3.OvertimeDateE), "1", "0")
        Dim DataTable As String = "OverTimeAdvance"

        'SQLString.AppendLine(",[HolidayOrNot] = " + Bsp.Utility.Quote(HolidayOrNot1))

        '如果我的用餐時數大於第一天的加班時數
        '那我就用餐時數-第一天的加班時數=第二天的用餐時數
        If Convert.ToInt32(OTTotalTime1) < Convert.ToInt32(OV_3.mealTime) Then
            mealTime1 = (Convert.ToInt32(OTTotalTime1)).ToString
            mealTime2 = (Convert.ToInt32(OV_3.mealTime) - Convert.ToInt32(OTTotalTime1)).ToString
        Else
            mealTime1 = (Convert.ToInt32(OV_3.mealTime)).ToString
            mealTime2 = "0"
        End If

        SQLString.AppendLine("INSERT INTO [dbo].[OverTimeAdvance]([OTCompID],[OTEmpID],[OTStartDate],[OTEndDate],[OTSeq],[OTTxnID],[OTSeqNo],[DeptID],[OrganID],[DeptName] ")
        SQLString.AppendLine(",[OrganName],[FlowCaseID],[OTStartTime],[OTEndTime],[OTTotalTime],[SalaryOrAdjust],[AdjustInvalidDate],[MealFlag],[MealTime] ")
        SQLString.AppendLine(",[OTTypeID],[OTReasonID],[OTReasonMemo],[OTAttachment],[OTFormNO],[OTRegisterID],[OTRegisterDate],[OTStatus],[HolidayOrNot] ")
        SQLString.AppendLine(",[OTValidDate],[OTValidID],[OTRejectDate],[OTRejectID],[OTGovernmentNo],[LastChgComp],[LastChgID],[LastChgDate],[OTRegisterComp]) VALUES( ")
        SQLString.AppendLine("" + Bsp.Utility.Quote(oldOVAData.OTCompID) + "," + Bsp.Utility.Quote(oldOVAData.OTEmpID) + "," + Bsp.Utility.Quote(StartDate1))
        SQLString.AppendLine("," + Bsp.Utility.Quote(EndDate1) + "," + Bsp.Utility.Quote(QuerySeq(DataTable, Me.OTEmpID, StartDate1)) + "," + Bsp.Utility.Quote(oldOVAData.OTTxnID))
        SQLString.AppendLine("," + Bsp.Utility.Quote("1") + "," + Bsp.Utility.Quote(oldOVAData.DeptID) + "," + Bsp.Utility.Quote(oldOVAData.OrganID))
        SQLString.AppendLine("," + Bsp.Utility.Quote(oldOVAData.DeptName) + "," + Bsp.Utility.Quote(oldOVAData.OrganName) + "," + Bsp.Utility.Quote(oldOVAData.FlowCaseID))
        SQLString.AppendLine("," + Bsp.Utility.Quote(StartTime1) + "," + Bsp.Utility.Quote(EndTime1) + "," + Bsp.Utility.Quote(OTTotalTime1))
        SQLString.AppendLine("," + Bsp.Utility.Quote(OV_3.SalaryOrAdjust) + "," + Bsp.Utility.Quote(OV_3.AdjustInvalidDate) + "," + Bsp.Utility.Quote(mealFlag1))
        SQLString.AppendLine("," + Bsp.Utility.Quote(mealTime1) + "," + Bsp.Utility.Quote(OV_3.OTTypeID) + "," + Bsp.Utility.Quote(oldOVAData.OTReasonID))
        SQLString.AppendLine("," + Bsp.Utility.Quote(OV_3.OTReasonMemo) + "," + Bsp.Utility.Quote(OV_3.OTAttachment) + "," + Bsp.Utility.Quote(oldOVAData.OTFormNO))
        SQLString.AppendLine("," + Bsp.Utility.Quote(oldOVAData.OTRegisterID) + "," + Bsp.Utility.Quote(oldOVAData.OTRegisterDate) + "," + Bsp.Utility.Quote(oldOVAData.OTStatus))
        SQLString.AppendLine("," + Bsp.Utility.Quote(HolidayOrNot1) + "," + Bsp.Utility.Quote(oldOVAData.OTValidDate) + "," + Bsp.Utility.Quote(oldOVAData.OTValidID))
        SQLString.AppendLine("," + Bsp.Utility.Quote(oldOVAData.OTRejectDate) + "," + Bsp.Utility.Quote(oldOVAData.OTRejectID) + "," + Bsp.Utility.Quote(oldOVAData.OTGovernmentNo))
        SQLString.AppendLine("," + Bsp.Utility.Quote(OV_3.LastChgComp) + "," + Bsp.Utility.Quote(OV_3.LastChgID) + ", getdate()," + Bsp.Utility.Quote(oldOVAData.OTRegisterComp) + ")")

        SQLString.AppendLine("INSERT INTO [dbo].[OverTimeAdvance]([OTCompID],[OTEmpID],[OTStartDate],[OTEndDate],[OTSeq],[OTTxnID],[OTSeqNo],[DeptID],[OrganID],[DeptName] ")
        SQLString.AppendLine(",[OrganName],[FlowCaseID],[OTStartTime],[OTEndTime],[OTTotalTime],[SalaryOrAdjust],[AdjustInvalidDate],[MealFlag],[MealTime] ")
        SQLString.AppendLine(",[OTTypeID],[OTReasonID],[OTReasonMemo],[OTAttachment],[OTFormNO],[OTRegisterID],[OTRegisterDate],[OTStatus],[HolidayOrNot] ")
        SQLString.AppendLine(",[OTValidDate],[OTValidID],[OTRejectDate],[OTRejectID],[OTGovernmentNo],[LastChgComp],[LastChgID],[LastChgDate],[OTRegisterComp]) VALUES( ")
        SQLString.AppendLine("" + Bsp.Utility.Quote(oldOVAData.OTCompID) + "," + Bsp.Utility.Quote(oldOVAData.OTEmpID) + "," + Bsp.Utility.Quote(StartDate2))
        SQLString.AppendLine("," + Bsp.Utility.Quote(EndDate2) + "," + Bsp.Utility.Quote(QuerySeq(DataTable, Me.OTEmpID, StartDate2)) + "," + Bsp.Utility.Quote(oldOVAData.OTTxnID))
        SQLString.AppendLine("," + Bsp.Utility.Quote("2") + "," + Bsp.Utility.Quote(oldOVAData.DeptID) + "," + Bsp.Utility.Quote(oldOVAData.OrganID))
        SQLString.AppendLine("," + Bsp.Utility.Quote(oldOVAData.DeptName) + "," + Bsp.Utility.Quote(oldOVAData.OrganName) + "," + Bsp.Utility.Quote(oldOVAData.FlowCaseID))
        SQLString.AppendLine("," + Bsp.Utility.Quote(StartTime2) + "," + Bsp.Utility.Quote(EndTime2) + "," + Bsp.Utility.Quote(OTTotalTime2))
        SQLString.AppendLine("," + Bsp.Utility.Quote(OV_3.SalaryOrAdjust) + "," + Bsp.Utility.Quote(OV_3.AdjustInvalidDate) + "," + Bsp.Utility.Quote(mealFlag2))
        SQLString.AppendLine("," + Bsp.Utility.Quote(mealTime2) + "," + Bsp.Utility.Quote(OV_3.OTTypeID) + "," + Bsp.Utility.Quote(oldOVAData.OTReasonID))
        SQLString.AppendLine("," + Bsp.Utility.Quote(OV_3.OTReasonMemo) + "," + Bsp.Utility.Quote(OV_3.OTAttachment) + "," + Bsp.Utility.Quote(oldOVAData.OTFormNO))
        SQLString.AppendLine("," + Bsp.Utility.Quote(oldOVAData.OTRegisterID) + "," + Bsp.Utility.Quote(oldOVAData.OTRegisterDate) + "," + Bsp.Utility.Quote(oldOVAData.OTStatus))
        SQLString.AppendLine("," + Bsp.Utility.Quote(HolidayOrNot2) + "," + Bsp.Utility.Quote(oldOVAData.OTValidDate) + "," + Bsp.Utility.Quote(oldOVAData.OTValidID))
        SQLString.AppendLine("," + Bsp.Utility.Quote(oldOVAData.OTRejectDate) + "," + Bsp.Utility.Quote(oldOVAData.OTRejectID) + "," + Bsp.Utility.Quote(oldOVAData.OTGovernmentNo))
        SQLString.AppendLine("," + Bsp.Utility.Quote(OV_3.LastChgComp) + "," + Bsp.Utility.Quote(OV_3.LastChgID) + ", getdate()," + Bsp.Utility.Quote(oldOVAData.OTRegisterComp) + ")")


        Using cn As DbConnection = Bsp.DB.getConnection("AattendantDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Try
                Bsp.DB.ExecuteNonQuery(CommandType.Text, SQLString.ToString(), tran, "AattendantDB")
                tran.Commit()
                Flag = True
            Catch ex As Exception
                tran.Rollback()
                Flag = False
                Throw
            Finally
                If tran IsNot Nothing Then tran.Dispose()
            End Try
        End Using
        Return Flag
    End Function


    Public Function InsertFor4002OVD(ByVal OV_3 As OV_3, ByVal oldOVDData As OV4_OVD) As Boolean
        Dim SQLString As StringBuilder = New StringBuilder
        '拆單
        Dim StartDate1 As String = OV_3.OvertimeDateB
        Dim EndDate1 As String = OV_3.OvertimeDateB
        Dim StartTime1 As String = OV_3.OTStartTime
        Dim EndTime1 As String = "2359"

        Dim StartDate2 As String = OV_3.OvertimeDateE
        Dim EndDate2 As String = OV_3.OvertimeDateE
        Dim StartTime2 As String = "0000"
        Dim EndTime2 As String = OV_3.OTEndTime
        '注意因為是跨日所以無條件進位 就算是59 也是60...
        Dim OTTotalTime1 As String = (Convert.ToInt32(Me.getOverTime(StartDate1, EndDate1, StartTime1, EndTime1, "M")) + 1).ToString
        Dim OTTotalTime2 As String = (Convert.ToInt32(Me.getOverTime(StartDate2, EndDate2, StartTime2, EndTime2, "M"))).ToString
        Dim Flag As Boolean = False
        Dim mealFlag1 As String = OV_3.mealFlag
        Dim mealFlag2 As String = OV_3.mealFlag
        Dim mealTime1 As String
        Dim mealTime2 As String
        Dim HolidayOrNot1 As String = If(Me.CheckHolidayOrNot(OV_3.OvertimeDateB), "1", "0")
        Dim HolidayOrNot2 As String = If(Me.CheckHolidayOrNot(OV_3.OvertimeDateE), "1", "0")
        Dim DataTable As String = "OverTimeDeclaration"




        If Convert.ToInt32(OTTotalTime1) < Convert.ToInt32(OV_3.mealTime) Then
            mealTime1 = (Convert.ToInt32(OTTotalTime1)).ToString
            mealTime2 = (Convert.ToInt32(OV_3.mealTime) - Convert.ToInt32(OTTotalTime1)).ToString
        Else
            mealTime1 = (Convert.ToInt32(OV_3.mealTime)).ToString
            mealTime2 = "0"
        End If


        '20170307 新增轉補修轉薪資拋轉修改邏輯 如果已拋轉 未計薪 那就刪除 並改toOverTimeFlag 為0
        If "2".Equals(OV_3.SalaryOrAdjust) And "after".Equals(Type) And OV_3.isSalaryPaid Then
            oldOVDData.ToOverTimeFlag = "0"
        End If


        SQLString.AppendLine("INSERT INTO [dbo].[OverTimeDeclaration]([OTCompID],[OTEmpID],[OTStartDate],[OTEndDate],[OTSeq],[OTTxnID],[OTSeqNo],[OTFromAdvanceTxnId],[DeptID],[OrganID],[DeptName],[OrganName]")
        SQLString.AppendLine(",[FlowCaseID],[OTStartTime],[OTEndTime],[OTTotalTime],[SalaryOrAdjust],[AdjustInvalidDate],[AdjustStatus],[AdjustDate],[MealFlag],[MealTime],[OTTypeID],[OTReasonID],[OTReasonMemo]")
        SQLString.AppendLine(",[OTAttachment],[OTFormNO],[OTRegisterID],[OTRegisterDate],[OTStatus],[OTValidDate],[OTValidID],[OverTimeFlag],[ToOverTimeDate],[ToOverTimeFlag],[OTRejectDate],[OTRejectID]")
        SQLString.AppendLine(",[OTGovernmentNo],[OTSalaryPaid],[HolidayOrNot],[ProcessDate],[OTPayDate],[OTModifyDate],[OTRemark],[KeyInComp],[KeyInID],[HRKeyInFlag],[LastChgComp],[LastChgID],[LastChgDate],[OTRegisterComp])VALUES(")
        SQLString.AppendLine(Bsp.Utility.Quote(oldOVDData.OTCompID) + "," + Bsp.Utility.Quote(oldOVDData.OTEmpID) + "," + Bsp.Utility.Quote(StartDate1))
        SQLString.AppendLine("," + Bsp.Utility.Quote(EndDate1) + "," + Bsp.Utility.Quote(QuerySeq(DataTable, Me.OTEmpID, StartDate1)) + "," + Bsp.Utility.Quote(oldOVDData.OTTxnID))
        SQLString.AppendLine("," + Bsp.Utility.Quote("1") + "," + Bsp.Utility.Quote(oldOVDData.OTFromAdvanceTxnId) + "," + Bsp.Utility.Quote(oldOVDData.DeptID) + "," + Bsp.Utility.Quote(oldOVDData.OrganID))
        SQLString.AppendLine("," + Bsp.Utility.Quote(oldOVDData.DeptName) + "," + Bsp.Utility.Quote(oldOVDData.OrganName) + "," + Bsp.Utility.Quote(oldOVDData.FlowCaseID))
        SQLString.AppendLine("," + Bsp.Utility.Quote(StartTime1) + "," + Bsp.Utility.Quote(EndTime1) + "," + Bsp.Utility.Quote(OTTotalTime1))
        SQLString.AppendLine("," + Bsp.Utility.Quote(OV_3.SalaryOrAdjust) + "," + Bsp.Utility.Quote(OV_3.AdjustInvalidDate))
        SQLString.AppendLine("," + Bsp.Utility.Quote(oldOVDData.AdjustStatus) + "," + Bsp.Utility.Quote(oldOVDData.AdjustDate) + "," + Bsp.Utility.Quote(mealFlag1))
        SQLString.AppendLine("," + Bsp.Utility.Quote(mealTime1) + "," + Bsp.Utility.Quote(OV_3.OTTypeID) + "," + Bsp.Utility.Quote(oldOVDData.OTReasonID))
        SQLString.AppendLine("," + Bsp.Utility.Quote(OV_3.OTReasonMemo) + "," + Bsp.Utility.Quote(OV_3.OTAttachment) + "," + Bsp.Utility.Quote(oldOVDData.OTFormNO))
        SQLString.AppendLine("," + Bsp.Utility.Quote(oldOVDData.OTRegisterID) + "," + Bsp.Utility.Quote(oldOVDData.OTRegisterDate) + "," + Bsp.Utility.Quote(oldOVDData.OTStatus))
        SQLString.AppendLine("," + Bsp.Utility.Quote(oldOVDData.OTValidDate) + "," + Bsp.Utility.Quote(oldOVDData.OTValidID) + "," + Bsp.Utility.Quote(oldOVDData.OverTimeFlag))
        SQLString.AppendLine("," + Bsp.Utility.Quote(oldOVDData.ToOverTimeDate) + "," + Bsp.Utility.Quote(oldOVDData.ToOverTimeFlag) + "," + Bsp.Utility.Quote(oldOVDData.OTRejectDate))
        SQLString.AppendLine("," + Bsp.Utility.Quote(oldOVDData.OTRejectID) + "," + Bsp.Utility.Quote(oldOVDData.OTGovernmentNo) + "," + Bsp.Utility.Quote(oldOVDData.OTSalaryPaid))
        SQLString.AppendLine("," + Bsp.Utility.Quote(HolidayOrNot1) + "," + Bsp.Utility.Quote(oldOVDData.ProcessDate) + "," + Bsp.Utility.Quote(oldOVDData.OTPayDate))
        SQLString.AppendLine("," + Bsp.Utility.Quote(oldOVDData.OTModifyDate) + "," + Bsp.Utility.Quote(oldOVDData.OTRemark) + "," + Bsp.Utility.Quote(oldOVDData.KeyInComp))
        SQLString.AppendLine("," + Bsp.Utility.Quote(oldOVDData.KeyInID) + "," + Bsp.Utility.Quote(oldOVDData.HRKeyInFlag))
        SQLString.AppendLine("," + Bsp.Utility.Quote(OV_3.LastChgComp) + "," + Bsp.Utility.Quote(OV_3.LastChgID) + ", getdate()," + Bsp.Utility.Quote(oldOVDData.OTRegisterComp) + ")")


        SQLString.AppendLine("INSERT INTO [dbo].[OverTimeDeclaration]([OTCompID],[OTEmpID],[OTStartDate],[OTEndDate],[OTSeq],[OTTxnID],[OTSeqNo],[OTFromAdvanceTxnId],[DeptID],[OrganID],[DeptName],[OrganName]")
        SQLString.AppendLine(",[FlowCaseID],[OTStartTime],[OTEndTime],[OTTotalTime],[SalaryOrAdjust],[AdjustInvalidDate],[AdjustStatus],[AdjustDate],[MealFlag],[MealTime],[OTTypeID],[OTReasonID],[OTReasonMemo]")
        SQLString.AppendLine(",[OTAttachment],[OTFormNO],[OTRegisterID],[OTRegisterDate],[OTStatus],[OTValidDate],[OTValidID],[OverTimeFlag],[ToOverTimeDate],[ToOverTimeFlag],[OTRejectDate],[OTRejectID]")
        SQLString.AppendLine(",[OTGovernmentNo],[OTSalaryPaid],[HolidayOrNot],[ProcessDate],[OTPayDate],[OTModifyDate],[OTRemark],[KeyInComp],[KeyInID],[HRKeyInFlag],[LastChgComp],[LastChgID],[LastChgDate],[OTRegisterComp])VALUES(")
        SQLString.AppendLine(Bsp.Utility.Quote(oldOVDData.OTCompID) + "," + Bsp.Utility.Quote(oldOVDData.OTEmpID) + "," + Bsp.Utility.Quote(StartDate2))
        SQLString.AppendLine("," + Bsp.Utility.Quote(EndDate2) + "," + Bsp.Utility.Quote(QuerySeq(DataTable, Me.OTEmpID, StartDate2)) + "," + Bsp.Utility.Quote(oldOVDData.OTTxnID))
        SQLString.AppendLine("," + Bsp.Utility.Quote("2") + "," + Bsp.Utility.Quote(oldOVDData.OTFromAdvanceTxnId) + "," + Bsp.Utility.Quote(oldOVDData.DeptID) + "," + Bsp.Utility.Quote(oldOVDData.OrganID))
        SQLString.AppendLine("," + Bsp.Utility.Quote(oldOVDData.DeptName) + "," + Bsp.Utility.Quote(oldOVDData.OrganName) + "," + Bsp.Utility.Quote(oldOVDData.FlowCaseID))
        SQLString.AppendLine("," + Bsp.Utility.Quote(StartTime2) + "," + Bsp.Utility.Quote(EndTime2) + "," + Bsp.Utility.Quote(OTTotalTime2))
        SQLString.AppendLine("," + Bsp.Utility.Quote(OV_3.SalaryOrAdjust) + "," + Bsp.Utility.Quote(OV_3.AdjustInvalidDate))
        SQLString.AppendLine("," + Bsp.Utility.Quote(oldOVDData.AdjustStatus) + "," + Bsp.Utility.Quote(oldOVDData.AdjustDate) + "," + Bsp.Utility.Quote(mealFlag2))
        SQLString.AppendLine("," + Bsp.Utility.Quote(mealTime2) + "," + Bsp.Utility.Quote(OV_3.OTTypeID) + "," + Bsp.Utility.Quote(oldOVDData.OTReasonID))
        SQLString.AppendLine("," + Bsp.Utility.Quote(OV_3.OTReasonMemo) + "," + Bsp.Utility.Quote(OV_3.OTAttachment) + "," + Bsp.Utility.Quote(oldOVDData.OTFormNO))
        SQLString.AppendLine("," + Bsp.Utility.Quote(oldOVDData.OTRegisterID) + "," + Bsp.Utility.Quote(oldOVDData.OTRegisterDate) + "," + Bsp.Utility.Quote(oldOVDData.OTStatus))
        SQLString.AppendLine("," + Bsp.Utility.Quote(oldOVDData.OTValidDate) + "," + Bsp.Utility.Quote(oldOVDData.OTValidID) + "," + Bsp.Utility.Quote(oldOVDData.OverTimeFlag))
        SQLString.AppendLine("," + Bsp.Utility.Quote(oldOVDData.ToOverTimeDate) + "," + Bsp.Utility.Quote(oldOVDData.ToOverTimeFlag) + "," + Bsp.Utility.Quote(oldOVDData.OTRejectDate))
        SQLString.AppendLine("," + Bsp.Utility.Quote(oldOVDData.OTRejectID) + "," + Bsp.Utility.Quote(oldOVDData.OTGovernmentNo) + "," + Bsp.Utility.Quote(oldOVDData.OTSalaryPaid))
        SQLString.AppendLine("," + Bsp.Utility.Quote(HolidayOrNot2) + "," + Bsp.Utility.Quote(oldOVDData.ProcessDate) + "," + Bsp.Utility.Quote(oldOVDData.OTPayDate))
        SQLString.AppendLine("," + Bsp.Utility.Quote(oldOVDData.OTModifyDate) + "," + Bsp.Utility.Quote(oldOVDData.OTRemark) + "," + Bsp.Utility.Quote(oldOVDData.KeyInComp))
        SQLString.AppendLine("," + Bsp.Utility.Quote(oldOVDData.KeyInID) + "," + Bsp.Utility.Quote(oldOVDData.HRKeyInFlag))
        SQLString.AppendLine("," + Bsp.Utility.Quote(OV_3.LastChgComp) + "," + Bsp.Utility.Quote(OV_3.LastChgID) + ", getdate()," + Bsp.Utility.Quote(oldOVDData.OTRegisterComp) + ")")
        Using cn As DbConnection = Bsp.DB.getConnection("AattendantDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Try
                Bsp.DB.ExecuteNonQuery(CommandType.Text, SQLString.ToString(), tran, "AattendantDB")
                tran.Commit()
                Flag = True
            Catch ex As Exception
                tran.Rollback()
                Flag = False
                Throw
            Finally
                If tran IsNot Nothing Then tran.Dispose()
            End Try
        End Using
        Return Flag
    End Function


    Public Function UpDateFor4002Single(ByVal OV_3 As OV_3) As Boolean
        Dim SQLString As StringBuilder = New StringBuilder
        Dim Flag As Boolean = False
        Dim DataTable As String
        If "bef".Equals(Type) Then
            DataTable = "OverTimeAdvance"
        Else
            DataTable = "OverTimeDeclaration"
        End If
        Dim HolidayOrNot1 As String = If(Me.CheckHolidayOrNot(OV_3.OvertimeDateB), "1", "0")
        SQLString.AppendLine(" Update [dbo]." + DataTable)
        SQLString.AppendLine("SET [OTStartDate] = " + Bsp.Utility.Quote(OV_3.OvertimeDateB))
        SQLString.AppendLine(",[OTEndDate] = " + Bsp.Utility.Quote(OV_3.OvertimeDateE))
        SQLString.AppendLine(",[OTStartTime] = " + Bsp.Utility.Quote(OV_3.OTStartTime))
        SQLString.AppendLine(",[OTEndTime] = " + Bsp.Utility.Quote(OV_3.OTEndTime))
        SQLString.AppendLine(",[OTTotalTime] =" + Bsp.Utility.Quote(OV_3.Time))
        SQLString.AppendLine(",[SalaryOrAdjust] = " + Bsp.Utility.Quote(OV_3.SalaryOrAdjust))
        SQLString.AppendLine(",[AdjustInvalidDate] = " + Bsp.Utility.Quote(OV_3.AdjustInvalidDate))
        SQLString.AppendLine(",[HolidayOrNot] = " + Bsp.Utility.Quote(HolidayOrNot1))

        If isNull("mealFlag") Then
            SQLString.AppendLine(",[MealFlag] = " + Bsp.Utility.Quote(OV_3.mealFlag))
        End If
        If isNull("mealTime") Then
            SQLString.AppendLine(",[MealTime] =" + Bsp.Utility.Quote(OV_3.mealTime))
        End If
        SQLString.AppendLine(",[OTTypeID] =" + Bsp.Utility.Quote(OV_3.OTTypeID))
        If isNull("OTReasonMemo") Then
            SQLString.AppendLine(",[OTReasonMemo] =" + Bsp.Utility.Quote(OV_3.OTReasonMemo))
        End If

        '20170307 新增轉補修轉薪資拋轉修改邏輯
        If "2".Equals(OV_3.SalaryOrAdjust) And "after".Equals(Type) And OV_3.isSalaryPaid Then
            SQLString.AppendLine(",ToOverTimeFlag = '0'")
        End If

        SQLString.AppendLine(",[OTAttachment] =" + Bsp.Utility.Quote(OV_3.OTAttachment))


        SQLString.AppendLine(",[OTSeq] =" + Bsp.Utility.Quote(QuerySeq(DataTable, Me.OTEmpID, Me.OvertimeDateB)))
        SQLString.AppendLine(",[LastChgComp] =" + Bsp.Utility.Quote(OV_3.LastChgComp))
        SQLString.AppendLine(",[LastChgID] =" + Bsp.Utility.Quote(OV_3.LastChgID))
        SQLString.AppendLine(",[LastChgDate] =  getdate()")
        SQLString.AppendLine(" WHERE OTTxnID=" + Bsp.Utility.Quote(OV_3.OTTxnID))
        SQLString.AppendLine(" and OTCompID=" + Bsp.Utility.Quote(OV_3.CompID))
        SQLString.AppendLine(" and OTEmpID=" + Bsp.Utility.Quote(OV_3.OTEmpID))
        SQLString.AppendLine(" and OTSeqNo=" + Bsp.Utility.Quote("1"))

        Using cn As DbConnection = Bsp.DB.getConnection("AattendantDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Try
                Bsp.DB.ExecuteNonQuery(CommandType.Text, SQLString.ToString(), tran, "AattendantDB")
                tran.Commit()
                Flag = True
            Catch ex As Exception
                tran.Rollback()

                Flag = False
                Throw
            Finally
                If tran IsNot Nothing Then tran.Dispose()
            End Try
        End Using
        Return Flag
    End Function
    Public Function QuiryOldDataTableArrFormOTFromAdvanceTxnId(ByVal OTFromAdvanceTxnId As String) As DataTable
        Dim SqlStr As StringBuilder = New StringBuilder()
        Dim dt As DataTable = New DataTable
        SqlStr.Append(" Select OTCompID, ")
        SqlStr.Append(" OTEmpID,")
        SqlStr.Append(" OTStartDate,")
        SqlStr.Append(" OTEndDate,")
        SqlStr.Append(" OTSeq,")
        SqlStr.Append(" OTTxnID,")
        SqlStr.Append(" OTSeqNo,")
        SqlStr.Append(" OTFromAdvanceTxnId,")
        SqlStr.Append(" DeptID,")
        SqlStr.Append(" OrganID,")
        SqlStr.Append(" DeptName,")
        SqlStr.Append(" OrganName,")
        SqlStr.Append(" FlowCaseID,")
        SqlStr.Append(" OTStartTime,")
        SqlStr.Append(" OTEndTime,")
        SqlStr.Append(" OTTotalTime,")
        SqlStr.Append(" SalaryOrAdjust,")
        SqlStr.Append(" AdjustInvalidDate,")
        SqlStr.Append(" AdjustStatus,")
        SqlStr.Append(" AdjustDate,")
        SqlStr.Append(" MealFlag,")
        SqlStr.Append(" MealTime,")
        SqlStr.Append(" OTTypeID,")
        SqlStr.Append(" OTReasonID,")
        SqlStr.Append(" OTReasonMemo,")
        SqlStr.Append(" OTAttachment,")
        SqlStr.Append(" OTFormNO,")
        SqlStr.Append(" OTRegisterID,")
        SqlStr.Append(" OTRegisterDate,")
        SqlStr.Append(" OTStatus,")
        SqlStr.Append(" OTValidDate,")
        SqlStr.Append(" OTValidID,")
        SqlStr.Append(" OverTimeFlag,")
        SqlStr.Append(" ToOverTimeDate,")
        SqlStr.Append(" ToOverTimeFlag,")
        SqlStr.Append(" OTRejectDate,")
        SqlStr.Append(" OTRejectID,")
        SqlStr.Append(" OTGovernmentNo,")
        SqlStr.Append(" OTSalaryPaid,")
        SqlStr.Append(" HolidayOrNot,")
        SqlStr.Append(" ProcessDate,")
        SqlStr.Append(" OTPayDate,")
        SqlStr.Append(" OTModifyDate,")
        SqlStr.Append(" OTRemark,")
        SqlStr.Append(" KeyInComp,")
        SqlStr.Append(" KeyInID,")
        SqlStr.Append(" HRKeyInFlag,")
        SqlStr.Append(" LastChgComp,")
        SqlStr.Append(" LastChgID,")
        SqlStr.Append(" LastChgDate, ")
        SqlStr.Append("OTRegisterComp ")
        SqlStr.Append(" FROM OverTimeDeclaration")
        SqlStr.Append(" where OTFromAdvanceTxnId=" + Bsp.Utility.Quote(OTFromAdvanceTxnId))
        SqlStr.Append("  ORDER BY OTSeqNo")
        dt = Bsp.DB.ExecuteDataSet(CommandType.Text, SqlStr.ToString, "AattendantDB").Tables(0)
        Return dt
    End Function
    Public Function QuiryOldDataTableArrForOTTxnID(ByVal OTTxnID As String, ByVal Type As String) As DataTable
        Dim SqlStr As StringBuilder = New StringBuilder()
        Dim dt As DataTable = New DataTable
        If "bef".Equals(Type) Then
            SqlStr.Append(" SELECT OTCompID, ")
            SqlStr.Append("OTEmpID, ")
            SqlStr.Append("OTStartDate, ")
            SqlStr.Append("OTEndDate, ")
            SqlStr.Append("OTSeq, ")
            SqlStr.Append("OTTxnID, ")
            SqlStr.Append("OTSeqNo, ")
            SqlStr.Append("DeptID, ")
            SqlStr.Append("OrganID, ")
            SqlStr.Append("DeptName, ")
            SqlStr.Append("OrganName, ")
            SqlStr.Append("FlowCaseID, ")
            SqlStr.Append("OTStartTime, ")
            SqlStr.Append("OTEndTime, ")
            SqlStr.Append("OTTotalTime, ")
            SqlStr.Append("SalaryOrAdjust, ")
            SqlStr.Append("AdjustInvalidDate, ")
            SqlStr.Append("MealFlag, ")
            SqlStr.Append("MealTime, ")
            SqlStr.Append("OTTypeID, ")
            SqlStr.Append("OTReasonID, ")
            SqlStr.Append("OTReasonMemo, ")
            SqlStr.Append("OTAttachment, ")
            SqlStr.Append("OTFormNO, ")
            SqlStr.Append("OTRegisterID, ")
            SqlStr.Append("OTRegisterDate, ")
            SqlStr.Append("OTStatus, ")
            SqlStr.Append("HolidayOrNot, ")
            SqlStr.Append("OTValidDate, ")
            SqlStr.Append("OTValidID, ")
            SqlStr.Append("OTRejectDate, ")
            SqlStr.Append("OTRejectID, ")
            SqlStr.Append("OTGovernmentNo, ")
            SqlStr.Append("LastChgComp, ")
            SqlStr.Append("LastChgID, ")
            SqlStr.Append("LastChgDate, ")
            SqlStr.Append("LastChgDate, ")
            SqlStr.Append("OTRegisterComp ")
            SqlStr.Append("FROM OverTimeAdvance")
            SqlStr.Append(" where OTTxnID=" + Bsp.Utility.Quote(OTTxnID))
            SqlStr.Append("  ORDER BY OTSeqNo")
            dt = Bsp.DB.ExecuteDataSet(CommandType.Text, SqlStr.ToString, "AattendantDB").Tables(0)
            Return dt
        Else
            SqlStr.Append(" Select OTCompID, ")
            SqlStr.Append(" OTEmpID,")
            SqlStr.Append(" OTStartDate,")
            SqlStr.Append(" OTEndDate,")
            SqlStr.Append(" OTSeq,")
            SqlStr.Append(" OTTxnID,")
            SqlStr.Append(" OTSeqNo,")
            SqlStr.Append(" OTFromAdvanceTxnId,")
            SqlStr.Append(" DeptID,")
            SqlStr.Append(" OrganID,")
            SqlStr.Append(" DeptName,")
            SqlStr.Append(" OrganName,")
            SqlStr.Append(" FlowCaseID,")
            SqlStr.Append(" OTStartTime,")
            SqlStr.Append(" OTEndTime,")
            SqlStr.Append(" OTTotalTime,")
            SqlStr.Append(" SalaryOrAdjust,")
            SqlStr.Append(" AdjustInvalidDate,")
            SqlStr.Append(" AdjustStatus,")
            SqlStr.Append(" AdjustDate,")
            SqlStr.Append(" MealFlag,")
            SqlStr.Append(" MealTime,")
            SqlStr.Append(" OTTypeID,")
            SqlStr.Append(" OTReasonID,")
            SqlStr.Append(" OTReasonMemo,")
            SqlStr.Append(" OTAttachment,")
            SqlStr.Append(" OTFormNO,")
            SqlStr.Append(" OTRegisterID,")
            SqlStr.Append(" OTRegisterDate,")
            SqlStr.Append(" OTStatus,")
            SqlStr.Append(" OTValidDate,")
            SqlStr.Append(" OTValidID,")
            SqlStr.Append(" OverTimeFlag,")
            SqlStr.Append(" ToOverTimeDate,")
            SqlStr.Append(" ToOverTimeFlag,")
            SqlStr.Append(" OTRejectDate,")
            SqlStr.Append(" OTRejectID,")
            SqlStr.Append(" OTGovernmentNo,")
            SqlStr.Append(" OTSalaryPaid,")
            SqlStr.Append(" HolidayOrNot,")
            SqlStr.Append(" ProcessDate,")
            SqlStr.Append(" OTPayDate,")
            SqlStr.Append(" OTModifyDate,")
            SqlStr.Append(" OTRemark,")
            SqlStr.Append(" KeyInComp,")
            SqlStr.Append(" KeyInID,")
            SqlStr.Append(" HRKeyInFlag,")
            SqlStr.Append(" LastChgComp,")
            SqlStr.Append(" LastChgID,")
            SqlStr.Append(" LastChgDate, ")
            SqlStr.Append(" OTRegisterComp ")
            SqlStr.Append(" FROM OverTimeDeclaration")
            SqlStr.Append(" where OTTxnID=" + Bsp.Utility.Quote(OTTxnID))
            SqlStr.Append("  ORDER BY OTSeqNo")
            dt = Bsp.DB.ExecuteDataSet(CommandType.Text, SqlStr.ToString, "AattendantDB").Tables(0)
            Return dt
        End If
        Return dt
    End Function

    Public Function QuerySeq(strTable As String, strEmpID As String, [date] As String) As Integer
        'Seq
        Dim OTSeq As Integer = 0

        Dim dt As DataTable = QueryData("MAX(OTSeq) AS MAXOTSeq", strTable, (Convert.ToString((Convert.ToString("AND OTCompID='" + UserProfile.SelectCompRoleID.Trim + "' AND OTEmpID='") & strEmpID) + "' AND OTStartDate='") & [date]) + "'")
        If dt.Rows.Count = 0 Then
            OTSeq = 1
        Else
            OTSeq = Convert.ToInt32(If(dt.Rows(0)("MAXOTSeq").ToString() = "", "0", dt.Rows(0)("MAXOTSeq").ToString())) + 1
        End If
        Return OTSeq
    End Function

    Public Function QueryData(strColumn As String, strTable As String, strWhere As String) As DataTable
        '查詢datatable
        Dim strSQL As StringBuilder = New StringBuilder()
        strSQL.Append(Convert.ToString((Convert.ToString("SELECT ") & strColumn) + " FROM ") & strTable)
        strSQL.Append(" WHERE 1=1 ")
        strSQL.Append(strWhere)
        Dim dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "AattendantDB").Tables(0)
        Return dt
    End Function

    ''' <summary>
    ''' 檢查日期是否為假日
    ''' </summary>
    ''' <param name="strDate"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks>從假日檔查詢日期是否為假日</remarks>
    Public Function CheckHolidayOrNot(ByVal strDate As String) As Boolean
        Dim strSQL As New StringBuilder()
        strSQL.Append(" SELECT HolidayOrNot FROM Calendar")
        strSQL.Append(" Where 1=1 AND CompID = " & Bsp.Utility.Quote(UserProfile.SelectCompRoleID.Trim))
        strSQL.Append(" AND CONVERT(CHAR(10),SysDate, 111) = " & Bsp.Utility.Quote(strDate))
        Try
            Dim dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "eHRMSDB").Tables(0)
            If dt.Rows.Count > 0 AndAlso dt.Rows(0).Item("HolidayOrNot").ToString() IsNot Nothing Then
                If dt.Rows(0).Item("HolidayOrNot").ToString() = "1" Then
                    Return True
                End If
            End If

            Return False
        Catch ex As Exception
            Debug.Print("CheckHolidayOrNot()==>" + ex.Message)
            Return False
        End Try
    End Function


#End Region
#Region "4002資料更新"


#End Region
#Region "檢查時間重複 同OV4_2 但須排除自己 且屬性全抓"
    ''' <summary>
    ''' 檢核時間重疊(事後申報)
    ''' </summary>
    ''' <param name="StartDate"></param>
    ''' <param name="EndDate"></param>
    ''' <param name="OTEmpID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckOverTimeDeclaration(ByVal StartDate As String, ByVal EndDate As String, ByVal OTEmpID As String, ByVal OTTxnID As String) As DataTable
        Dim strSQL As New StringBuilder()

        If StartDate <> "" Then
            If EndDate <> "" Then
                If StartDate = EndDate Then
                    strSQL.AppendLine("SELECT * FROM OverTimeDeclaration  WHERE OTStatus in ('1','2','3') AND OTEmpID = " + Bsp.Utility.Quote(OTEmpID) + " AND OTStartDate = " + Bsp.Utility.Quote(StartDate) + " AND OTEndDate = " + Bsp.Utility.Quote(EndDate) + "and  NOT OTTxnID=" + Bsp.Utility.Quote(OTTxnID))
                Else
                    strSQL.AppendLine(" SELECT * FROM OverTimeDeclaration WHERE OTEmpID = " + Bsp.Utility.Quote(OTEmpID) + " AND OTStatus IN ('1','2','3') AND OTStartDate IN ( " + Bsp.Utility.Quote(StartDate) + "," + Bsp.Utility.Quote(EndDate) + ")" + "and  NOT OTTxnID=" + Bsp.Utility.Quote(OTTxnID))
                End If
            End If
        End If

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "AattendantDB").Tables(0)
    End Function

    ''' <summary>
    ''' 檢核時間重疊(預先申請)
    ''' </summary>
    ''' <param name="StartDate"></param>
    ''' <param name="EndDate"></param>
    ''' <param name="OTEmpID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckOverTimeAdvance(ByVal StartDate As String, ByVal EndDate As String, ByVal OTEmpID As String, ByVal OTTxnID As String) As DataTable
        Dim strSQL As New StringBuilder()

        If StartDate <> "" Then
            If EndDate <> "" Then
                If StartDate = EndDate Then
                    strSQL.AppendLine("SELECT * FROM OverTimeAdvance  WHERE OTStatus in ('1','2','3') AND OTEmpID = " + Bsp.Utility.Quote(OTEmpID) + " AND OTStartDate = " + Bsp.Utility.Quote(StartDate) + " AND OTEndDate = " + Bsp.Utility.Quote(EndDate) + "and  NOT OTTxnID=" + Bsp.Utility.Quote(OTTxnID))
                Else
                    strSQL.AppendLine(" SELECT * FROM OverTimeAdvance WHERE OTEmpID = " + Bsp.Utility.Quote(OTEmpID) + " AND OTStatus IN ('1','2','3') AND OTStartDate IN ( " + Bsp.Utility.Quote(StartDate) + "," + Bsp.Utility.Quote(EndDate) + ")" + "and  NOT OTTxnID=" + Bsp.Utility.Quote(OTTxnID))
                End If
            End If
        End If

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "AattendantDB").Tables(0)
    End Function



#End Region
    Public Function UPdateAattendantDBFlowCase(ByVal UserDeptID As String, ByVal UserDeptName As String, ByVal UserUserID As String, ByVal UserName As String, ByVal OVDFlowCaseID As String, ByVal OTFromAdvanceTxnId As String)
        Dim OVA As DataTable = QueryData(" * ", "OverTimeAdvance", " AND OTTxnID='" + OTFromAdvanceTxnId + "'")
        Dim OVAFlowCaseID As String = ""
        If OVA.Rows.Count > 0 Then
            OVAFlowCaseID = OVA.Rows(0).Item("FlowCaseID").ToString.Trim
        End If
        If OVDFlowCaseID.ToString.Trim.Count > 0 Then
            UPdateAattendantDBFlowCaseForOVD(UserDeptID, UserDeptName, UserUserID, UserName, OVDFlowCaseID)
        End If
        If OVAFlowCaseID.ToString.Trim.Count > 0 Then
            UPdateAattendantDBFlowCaseForOVA(UserDeptID, UserDeptName, UserUserID, UserName, OVAFlowCaseID)
        End If
    End Function

    ''' <summary>
    ''' 刪除OverTimeTable裡的資料
    ''' 與OV4002不同這邊不對OverTime 另外再做時間的更新
    ''' </summary>
    ''' <param name="DataTable"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function deleteOverTimeTable(ByVal DataTable As DataTable) As Boolean
        Dim Flag As Boolean = False
        Dim SQLString As StringBuilder = New StringBuilder
        Dim OTStartDateList As ArrayList = New ArrayList

        '跨日單會跑兩次
        For i = 0 To DataTable.Rows.Count - 1
            SQLString.AppendLine(" DELETE FROM [OverTime] ")
            SQLString.AppendLine("  WHERE  OTDate=" + Bsp.Utility.Quote(DataTable.Rows(i).Item("OTStartDate")))
            SQLString.AppendLine(" and CompID=" + Bsp.Utility.Quote(DataTable.Rows(i).Item("OTCompID")))
            SQLString.AppendLine(" and EmpID=" + Bsp.Utility.Quote(DataTable.Rows(i).Item("OTEmpID")))
            SQLString.AppendLine(" and BeginTime=" + Bsp.Utility.Quote(DataTable.Rows(i).Item("OTStartTime")))

            OTStartDateList.Add(DataTable.Rows(i).Item("OTStartDate"))
        Next


        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Try
                Bsp.DB.ExecuteNonQuery(CommandType.Text, SQLString.ToString(), tran, "eHRMSDB")
                tran.Commit()
                Flag = True
            Catch ex As Exception
                tran.Rollback()
                Flag = False
                Throw
            Finally
                If tran IsNot Nothing Then tran.Dispose()
            End Try
        End Using


        Return Flag
    End Function


    Public Function UPdateAattendantDBFlowCaseForOVD(ByVal UserDeptID As String, ByVal UserDeptName As String, ByVal UserUserID As String, ByVal UserName As String, ByVal FlowCaseID As String)
        Dim sb As StringBuilder = New StringBuilder()
        Dim dt2 As DataTable = QueryData("TOP 1 *", "" + Config_AattendantDBFlowFullLog + "", " AND FlowCaseID='" + FlowCaseID + "' ORDER BY FlowLogBatNo DESC")
        Dim strLastLogBatNo As String = "3"
        Dim strLastLogSeqNo As String = "3"

        If dt2.Rows.Count > 0 Then
            Dim strIsProxy As String = "N"
            If dt2.Rows(0)("FlowStepID").ToString().Trim() = "Z00" Then
                strLastLogBatNo = "4"
                strLastLogSeqNo = "4"
                strIsProxy = "N"
            End If

            Using cn As DbConnection = Bsp.DB.getConnection("AattendantDB")
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction

                Try
                    sb.Append("UPDATE " + Config_AattendantDBFlowCase + " SET FlowCaseStatus='Close',FlowCurrStepID='Z03',FlowCurrStepName='作廢結案',")
                    sb.Append(" LastLogBatNo=(SELECT MAX(LastLogBatNo) + 1 FROM " + Config_AattendantDBFlowCase + " WHERE FlowCaseID='" + FlowCaseID + "'), ")
                    sb.Append(" LastLogSeqNo=(SELECT MAX(LastLogSeqNo) + 1 FROM " + Config_AattendantDBFlowCase + " WHERE FlowCaseID='" + FlowCaseID + "'), ")
                    sb.Append(" UpdDateTime=GETDATE() ")
                    sb.Append(" WHERE FlowCaseID='" + FlowCaseID + "'")
                    sb.Append(" ;")

                    sb.Append("UPDATE " + Config_AattendantDBFlowFullLog + " SET FlowLogIsClose='Y',FlowStepBtnID='btnInvalid',FlowStepBtnCaption='作廢結案'")
                    sb.Append(" ,LogUpdDateTime= GETDATE()")
                    sb.Append(" ,ToDept='" + UserDeptID + "'")
                    sb.Append(" ,ToDeptName='" + UserDeptName + "'")
                    sb.Append(" ,ToUser='" + UserUserID + "'")
                    sb.Append(" ,ToUserName='" + UserName + "'")
                    sb.Append(" ,IsProxy='" + strIsProxy + "'")
                    sb.Append(" WHERE FlowCaseID='" + FlowCaseID + "'")
                    sb.Append(" AND FlowLogBatNo=(SELECT MAX(FlowLogBatNo) FROM " + Config_AattendantDBFlowFullLog + " WHERE FlowCaseID='" + FlowCaseID + "') ")
                    sb.Append(" ;")

                    Dim FlowLogBatNo As String = Convert.ToString(Convert.ToInt32(dt2.Rows(0)("FlowLogBatNo")) + 1)
                    Dim FlowLogID As String = Convert.ToString(FlowCaseID.ToString & ".0000" & (Convert.ToInt32(dt2.Rows(0)("FlowLogBatNo")) + 1))
                    sb.Append(" INSERT INTO " + Config_AattendantDBFlowFullLog + "(FlowCaseID,FlowLogBatNo,FlowLogID,FlowStepID,FlowStepName,FlowStepBtnID,FlowStepBtnCaption,FlowStepOpinion,FlowLogIsClose,IsProxy,AttachID,FromDept,FromDeptName,FromUser,FromUserName,AssignTo,AssignToName,ToDept,ToDeptName,ToUser,ToUserName,LogCrDateTime,LogUpdDateTime,LogRemark) ")
                    sb.Append((Convert.ToString((Convert.ToString(" VALUES('" + FlowCaseID + "', '") & FlowLogBatNo) + "', '") & FlowLogID) + "',")
                    sb.Append(" 'Z03','作廢結案','','','','Y','','',")
                    sb.Append(" '" + UserDeptID + "','" + UserDeptName + "','" + UserUserID + "','" + UserName + "','" + UserUserID + "','" + UserName + "','" + UserDeptID + "','" + UserDeptName + "','" + UserUserID + "','" + UserName + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',")
                    sb.Append(" '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','')")

                    ''主動刪除AattendantDBFlowOpenLog 
                    'sb.Append(" DELETE FROM " + Config_AattendantDBFlowOpenLog + " WHERE FlowCaseID='" + FlowCaseID + "'")

                    Bsp.DB.ExecuteNonQuery(CommandType.Text, sb.ToString(), "AattendantDB")
                Catch ex As Exception
                    tran.Rollback()
                    Throw
                Finally
                    If tran IsNot Nothing Then tran.Dispose()
                End Try
            End Using
        End If
    End Function



    Private Function UPdateAattendantDBFlowCaseForOVA(ByVal UserDeptID As String, ByVal UserDeptName As String, ByVal UserUserID As String, ByVal UserName As String, ByVal FlowCaseID As String)
        Dim sb As StringBuilder = New StringBuilder()
        Dim dt2 As DataTable = QueryData("TOP 1 *", "" + Config_AattendantDBFlowFullLog + "", " AND FlowCaseID='" + FlowCaseID + "' ORDER BY FlowLogBatNo DESC")
        If dt2.Rows.Count > 0 Then
            Dim strLastLogBatNo As String = "3"
            Dim strLastLogSeqNo As String = "3"
            Dim strIsProxy As String = ""
            If dt2.Rows(0)("FlowStepID").ToString().Trim() = "Z00" Then
                strLastLogBatNo = "4"
                strLastLogSeqNo = "4"
                strIsProxy = "N"
            End If

            Using cn As DbConnection = Bsp.DB.getConnection("AattendantDB")
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction

                Try
                    sb.Append(" UPDATE " + Config_AattendantDBFlowCase + " SET FlowCaseStatus='Close',FlowCurrStepID='Z03',FlowCurrStepName='取消結案',")
                    sb.Append(" LastLogBatNo=(SELECT MAX(LastLogBatNo) + 1 FROM " + Config_AattendantDBFlowCase + " WHERE FlowCaseID='" + FlowCaseID + "'), ")
                    sb.Append(" LastLogSeqNo=(SELECT MAX(LastLogSeqNo) + 1 FROM " + Config_AattendantDBFlowCase + " WHERE FlowCaseID='" + FlowCaseID + "'), ")
                    sb.Append(" UpdDateTime=GETDATE() ")
                    sb.Append(" WHERE FlowCaseID='" + FlowCaseID + "'")
                    sb.Append(" ;")


                    sb.Append(" UPDATE " + Config_AattendantDBFlowFullLog + " SET FlowLogIsClose='Y',FlowStepBtnID='btnCancel',FlowStepBtnCaption='取消結案'")
                    sb.Append(" ,LogUpdDateTime= GETDATE()")
                    sb.Append(" ,ToDept='" + UserDeptID + "'")
                    sb.Append(" ,ToDeptName='" + UserDeptName + "'")
                    sb.Append(" ,ToUser='" + UserUserID + "'")
                    sb.Append(" ,ToUserName='" + UserName + "'")
                    sb.Append(" ,IsProxy='" + strIsProxy + "'")
                    sb.Append(" WHERE FlowCaseID='" + FlowCaseID + "'")
                    sb.Append(" AND FlowLogBatNo=(SELECT MAX(FlowLogBatNo) FROM " + Config_AattendantDBFlowFullLog + " WHERE FlowCaseID='" + FlowCaseID + "') ")
                    sb.Append(" ;")

                    Dim FlowLogBatNo As String = Convert.ToString(Convert.ToInt32(dt2.Rows(0)("FlowLogBatNo")) + 1)
                    Dim FlowLogID As String = Convert.ToString(FlowCaseID.ToString & ".0000" & (Convert.ToInt32(dt2.Rows(0)("FlowLogBatNo")) + 1))
                    sb.Append(" INSERT INTO " + Config_AattendantDBFlowFullLog + "(FlowCaseID,FlowLogBatNo,FlowLogID,FlowStepID,FlowStepName,FlowStepBtnID,FlowStepBtnCaption,FlowStepOpinion,FlowLogIsClose,IsProxy,AttachID,FromDept,FromDeptName,FromUser,FromUserName,AssignTo,AssignToName,ToDept,ToDeptName,ToUser,ToUserName,LogCrDateTime,LogUpdDateTime,LogRemark) ")
                    sb.Append((Convert.ToString((Convert.ToString(" VALUES('" + FlowCaseID + "', '") & FlowLogBatNo) + "', '") & FlowLogID) + "',")
                    sb.Append(" 'Z03','取消結案','','','','Y','','',")
                    sb.Append(" '" + UserDeptID + "','" + UserDeptName + "','" + UserUserID + "','" + UserName + "','" + UserUserID + "','" + UserName + "','" + UserDeptID + "','" + UserDeptName + "','" + UserUserID + "','" + UserName + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',")
                    sb.Append(" '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','')")

                    ''主動刪除AattendantDBFlowOpenLog 
                    'sb.Append(" DELETE FROM " + Config_AattendantDBFlowOpenLog + " WHERE FlowCaseID='" + FlowCaseID + "'")
                    Bsp.DB.ExecuteNonQuery(CommandType.Text, sb.ToString(), "AattendantDB")

                    tran.Commit()
                Catch ex As Exception
                    tran.Rollback()
                    Throw
                Finally
                    If tran IsNot Nothing Then tran.Dispose()
                End Try
            End Using
        End If


    End Function
    ''' <summary>
    ''' 取得字串(去除null)
    ''' </summary>
    ''' <param name="ob">Object</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Function StringIIF(ByVal ob As Object) As String
        Dim result = ""
        If Not ob Is Nothing Then
            If Not String.IsNullOrEmpty(ob.ToString()) Then
                result = ob.ToString()
            End If
        End If
        Return result
    End Function

#Region "取PersonDB"
    Public Function GetPersonalDBFromCompIDandUserID(ByVal CompID As String, ByVal UserID As String) As DataTable
        Dim strSQL As New StringBuilder()
        Dim strUserName As String
        strSQL.AppendLine("Select * From Personal")
        strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine("And EmpID = " & Bsp.Utility.Quote(UserID))
        Dim dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "eHRMSDB").Tables(0)
        Return dt
    End Function

    Public Function GetPersonName(ByVal CompID As String, ByVal UserID As String) As String
        Dim personal As DataTable = Me.GetPersonalDBFromCompIDandUserID(CompID, UserID)
        If personal.Rows.Count > 0 Then
            Return personal.Rows(0).Item("Name")
        Else
            Return ""
        End If

    End Function
#End Region


    '合單for sumDataTable
    ''' <summary>
    ''' 先走訪把db的資料有跨日的OTTxnID放入arrayList
    ''' 然後移除有兩張單的ROW
    ''' 放入合單後的dataTable
    ''' 
    ''' 注意萬一db資料是跨日但是資料只抓到一張單 那會用單日來記載
    ''' </summary>
    ''' <param name="db"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SingleDataTable(ByVal db As DataTable) As DataTable
        ' Dim detialTable As DataTable = getTableForDetial()
        Dim arrayList As ArrayList = New ArrayList()

        '先走訪如果有兩張單的單號先記起來
        For i = 0 To db.Rows.Count - 1
            If "2".Equals(db.Rows(i).Item("OTSeqNo")) Then
                arrayList.Add(db.Rows(i).Item("OTTxnID"))
            End If
        Next
        Dim cloneDB As DataTable = db.Copy

        '回傳的TABLE移除有兩張單的ROW
        For i = 0 To arrayList.Count - 1
            cloneDB.[Select]("OTTxnID='" + arrayList.Item(i) + "'")
            Dim row As DataRow() = cloneDB.[Select]("OTTxnID='" + arrayList.Item(i) + "'")
            For j = 0 To row.Count - 1
                cloneDB.Rows.Remove(row(j))
            Next
        Next
        'OTStartDate, OTEndDate, OTStartTime, OTEndTime, OTStatus, OTTotalTime, mealTime, mealFlag, OTTxnID, OTSeqNo
        '合單
        For i = 0 To arrayList.Count - 1
            Dim rows As DataRow() = db.[Select]("OTTxnID= '" + arrayList.Item(i) + "'", "OTSeqNo")
            Dim dataRows As DataRow

            Dim OTTotalTime1 As Int32 = 0
            Dim OTTotalTime2 As Int32 = 0
            Dim OTTotalTime As Int32 = 0
            Dim mealTime1 As Int32 = 0
            Dim mealTime2 As Int32 = 0
            Dim mealTime As Int32 = 0

            If rows.Count = 1 Then
                OTTotalTime1 = IIf(IsNumeric(rows(0).Item("OTTotalTime")), Convert.ToInt32(rows(0).Item("OTTotalTime")), 0)
                OTTotalTime2 = 0
                OTTotalTime = OTTotalTime1 + OTTotalTime2
                mealTime1 = IIf(IsNumeric(rows(0).Item("mealTime")), Convert.ToInt32(rows(0).Item("mealTime")), 0)
                mealTime2 = 0
                mealTime = mealTime1 + mealTime2

                dataRows = cloneDB.NewRow
                dataRows("OTStartDate") = rows(0).Item("OTStartDate")
                dataRows("OTEndDate") = rows(0).Item("OTEndDate")
                dataRows("OTStartTime") = rows(0).Item("OTStartTime")
                dataRows("OTEndTime") = rows(0).Item("OTEndTime")
                dataRows("OTStatus") = rows(0).Item("OTStatus")
                dataRows("OTTotalTime") = OTTotalTime
                dataRows("mealTime") = mealTime
                dataRows("mealFlag") = rows(0).Item("mealFlag")
                dataRows("OTTxnID") = rows(0).Item("OTTxnID")
                dataRows("OTSeqNo") = rows(0).Item("OTSeqNo")
                cloneDB.Rows.Add(dataRows)

            ElseIf rows.Count > 1 Then

                OTTotalTime1 = IIf(IsNumeric(rows(0).Item("OTTotalTime")), Convert.ToInt32(rows(0).Item("OTTotalTime")), 0)
                OTTotalTime2 = IIf(IsNumeric(rows(1).Item("OTTotalTime")), Convert.ToInt32(rows(1).Item("OTTotalTime")), 0)
                OTTotalTime = OTTotalTime1 + OTTotalTime2
                mealTime1 = IIf(IsNumeric(rows(0).Item("mealTime")), Convert.ToInt32(rows(0).Item("mealTime")), 0)
                mealTime2 = IIf(IsNumeric(rows(1).Item("mealTime")), Convert.ToInt32(rows(1).Item("mealTime")), 0)
                mealTime = mealTime1 + mealTime2

                dataRows = cloneDB.NewRow
                dataRows("OTStartDate") = rows(0).Item("OTStartDate")
                dataRows("OTEndDate") = rows(1).Item("OTEndDate")
                dataRows("OTStartTime") = rows(0).Item("OTStartTime")
                dataRows("OTEndTime") = rows(1).Item("OTEndTime")
                dataRows("OTStatus") = rows(0).Item("OTStatus")
                dataRows("OTTotalTime") = OTTotalTime
                dataRows("mealTime") = mealTime
                dataRows("mealFlag") = rows(0).Item("mealFlag")
                dataRows("OTTxnID") = rows(0).Item("OTTxnID")
                dataRows("OTSeqNo") = rows(0).Item("OTSeqNo")
                cloneDB.Rows.Add(dataRows)


            End If



        Next
        Return cloneDB
    End Function

End Class



