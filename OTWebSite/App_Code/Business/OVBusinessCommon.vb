'****************************************************
'功能說明：OV4200的的查詢Funct
'建立日期：2017/02/02
'修改日期：2017/03/16
'****************************************************

Imports System.Diagnostics      'For Debug.Print()
Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.DataTable
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Newtonsoft.Json
Imports SinoPac.WebExpress.Common
Imports SinoPac.WebExpress.DAO
Imports System.Data.SqlClient

Partial Public Class OVBusinessCommon
    Implements System.Web.SessionState.IRequiresSessionState
    Public ReadOnly _flag As String = "false"
    Public Shared ReadOnly _AattendantDBName As String = "AattendantDB"
    Public Shared ReadOnly _AattendantFlowID As String = "AattendantDB"
    Public Shared ReadOnly _eHRMSDB_ITRD As String = "eHRMSDB"

    Public Shared Property AattendantDBName As String
        Get
            Dim result = ""
            Try
                Dim builder As DbConnectionStringBuilder = New System.Data.Common.DbConnectionStringBuilder()
                builder.ConnectionString = ConfigurationManager.ConnectionStrings("AattendantDB").ConnectionString
                Dim database As String = builder("database").ToString()
                result = database
            Catch ex As Exception
                result = "AattendantDB"
            End Try
            Return result
        End Get
        Set(ByVal value As String)
        End Set
    End Property

    Public Shared Property AattendantDBFlowCase As String
        Get
            Dim result = "[AattendantDB].[dbo].[AattendantDBFlowCase]"
            Try
                Dim builder As DbConnectionStringBuilder = New System.Data.Common.DbConnectionStringBuilder()
                builder.ConnectionString = ConfigurationManager.ConnectionStrings("AattendantDB").ConnectionString
                Dim database As String = builder("database").ToString()
                If Not String.IsNullOrEmpty(database) Then
                    result = String.Format("[{0}].[{1}].[{2}]", database, "dbo", database + "FlowCase")
                End If
            Catch ex As Exception
                result = "[AattendantDB].[dbo].[AattendantDBFlowCase]"
            End Try
            Return result
        End Get
        Set(ByVal value As String)
        End Set
    End Property

    Public Shared Property AattendantDBFlowFullLog As String
        Get
            Dim result = "[AattendantDB].[dbo].[AattendantDBFlowFullLog]"
            Try
                Dim builder As DbConnectionStringBuilder = New System.Data.Common.DbConnectionStringBuilder()
                builder.ConnectionString = ConfigurationManager.ConnectionStrings("AattendantDB").ConnectionString
                Dim database As String = builder("database").ToString()
                If Not String.IsNullOrEmpty(database) Then
                    result = String.Format("[{0}].[{1}].[{2}]", database, "dbo", database + "FlowFullLog")
                End If
            Catch ex As Exception
                result = "[AattendantDB].[dbo].[AattendantDBFlowFullLog]"
            End Try
            Return result
        End Get
        Set(ByVal value As String)
        End Set
    End Property

    Public Shared Property AattendantDBFlowOpenLog As String
        Get
            Dim result = "[AattendantDB].[dbo].[AattendantDBFlowOpenLog]"
            Try
                Dim builder As DbConnectionStringBuilder = New System.Data.Common.DbConnectionStringBuilder()
                builder.ConnectionString = ConfigurationManager.ConnectionStrings("AattendantDB").ConnectionString
                Dim database As String = builder("database").ToString()
                If Not String.IsNullOrEmpty(database) Then
                    result = String.Format("[{0}].[{1}].[{2}]", database, "dbo", database + "FlowOpenLog")
                End If
            Catch ex As Exception
                result = "[AattendantDB].[dbo].[AattendantDBFlowOpenLog]"
            End Try
            Return result
        End Get
        Set(ByVal value As String)
        End Set
    End Property

    ''' <summary>
    ''' GetRankIDFormMapping
    ''' </summary>
    ''' <param name="strDate"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks>從職等表查詢該公司值等資料</remarks>
    Public Shared Function GetRankID(ByVal compID As String, ByVal rankID As String) As String
        Dim result As String = rankID
        Dim errorMsg = ""
        Try
            Dim dataTable As New DataTable
            If HttpContext.Current.Session("Common_RankIDMappingDatas") Is Nothing Then
                dataTable = SelectRankID()
            Else
                dataTable = HttpContext.Current.Session("Common_RankIDMappingDatas")
            End If

            If dataTable.Rows.Count > 0 Then
                HttpContext.Current.Session("Common_RankIDMappingDatas") = dataTable
                Dim newRowData As DataRow = (From column In dataTable.Rows _
                                       Where column("CompID") = compID _
                                       And column("RankID") = rankID _
                                       Select column).FirstOrDefault()
                If newRowData.Item("RankIDMap") <> "" Then
                    result = newRowData.Item("RankIDMap").ToString
                End If
            End If
        Catch ex As Exception
            errorMsg = ex.Message
        End Try
        Return result
    End Function

    ''' <summary>
    ''' 篩選RankMapping資料
    ''' </summary>
    ''' <param name="strDate"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks>從職等表查詢該公司值等資料</remarks>
    Public Shared Function SelectRankID() As DataTable
        Dim strSQL As New StringBuilder()
        Dim dataTable As New DataTable
        strSQL.Append(" SELECT CompID, RankID, RankIDMap FROM RankMapping ")
        Try
            dataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, _eHRMSDB_ITRD).Tables(0)
            If dataTable.Rows.Count = 0 Then
                Throw New Exception("查無資料!")
            End If
        Catch ex As Exception
            Throw
        End Try
        Return dataTable
    End Function

    ''' <summary>
    ''' 檢查日期是否為假日
    ''' </summary>
    ''' <param name="strDate"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks>從假日檔查詢日期是否為假日</remarks>
    Public Shared Function CheckHolidayOrNot(ByVal strDate As String) As Boolean
        Dim strSQL As New StringBuilder()
        strSQL.Append(" SELECT HolidayOrNot FROM Calendar ")
        strSQL.Append(" Where 1=1 AND CompID = " & Bsp.Utility.Quote(UserProfile.SelectCompRoleID.Trim))
        strSQL.Append(" AND CONVERT(CHAR(10),SysDate, 111) = " & Bsp.Utility.Quote(strDate))
        Try
            Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, _eHRMSDB_ITRD).Tables(0)
                If dt.Rows.Count > 0 And dt.Rows(0).Item("HolidayOrNot").ToString() IsNot Nothing Then If dt.Rows(0).Item("HolidayOrNot").ToString() = "1" Then Return True
            End Using
            Return False
        Catch ex As Exception
            Debug.Print("CheckHolidayOrNot()==>" + ex.Message)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 判斷是否為國定假日
    ''' </summary>
    ''' <param name="checkDay">判斷的日期(DateTime)</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Shared Function IsNationalHoliday(ByVal checkDay As DateTime) As Boolean
        Try
            Return IsNationalHoliday(checkDay.ToString("yyyy/MM/dd"))
        Catch ex As Exception
            Debug.Print("CheckHolidayOrNot()==>" + ex.Message)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 判斷是否為國定假日
    ''' </summary>
    ''' <param name="checkDay">判斷的日期(yyyy/MM/dd)</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Shared Function IsNationalHoliday(ByVal checkDay As String) As Boolean
        Dim result As Boolean = False
        Dim message = ""
        Try
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            strSQL.AppendLine(" SELECT ")
            strSQL.AppendLine(" TabName, FldName, Code, CodeCName, SortFld, NotShowFlag  ")
            strSQL.AppendLine(" FROM AT_CodeMap ")
            strSQL.AppendLine(" WHERE 0 = 0 ")
            strSQL.AppendLine(" AND TabName = @TabName ")
            strSQL.AppendLine(" AND FldName = @FldName ")
            strSQL.AppendLine(" AND NotShowFlag = @NotShowFlag ")
            strSQL.AppendLine(" AND Code = @Code ")
            strSQL.AppendLine(" ; ")
            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@TabName", DbType.String, "NationalHolidayDefine")
            db.AddInParameter(dbcmd, "@FldName", DbType.String, "HolidayDate")
            db.AddInParameter(dbcmd, "@NotShowFlag", DbType.String, "0")
            db.AddInParameter(dbcmd, "@Code", DbType.String, checkDay)
            Dim ds As DataSet = db.ExecuteDataSet(dbcmd)
            If ds.Tables.Count > 0 Then
                Dim dt = ds.Tables(0)
                If dt.Rows.Count > 0 Then
                    result = True
                Else
                    message = "查無HROverTimeMain資料[001]"
                End If
            Else
                message = "查無HROverTimeMain資料[002]"
            End If
        Catch ex As Exception
            Debug.Print("IsNationalHoliday()==>" + ex.Message)
        End Try
        Return result
    End Function

    Public Shared Function getMonthTB(ByVal tMonth As String) As String()
        Dim result(2) As String
        Dim SQL As StringBuilder = New StringBuilder
        Dim dataTable As DataTable
        Try
            SQL.AppendLine("SELECT RIGHT ('0'+CAST(MONTH (" & Bsp.Utility.Quote(tMonth) & ") AS NVARCHAR(2)),2) As ThisMonth,CONVERT(NVARCHAR (10),DATEADD (MONTH, DATEDIFF (MONTH ,0," & Bsp.Utility.Quote(tMonth) & "), 0),120) As TopMonth,CONVERT(NVARCHAR (10),DATEADD (MONTH, DATEDIFF (MONTH ,0," & Bsp.Utility.Quote(tMonth) & ")+1, 0)-1,120) As BottomMonth")
            dataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, SQL.ToString, "AattendantDB").Tables(0)
            If dataTable.Rows.Count > 0 Then
                result(0) = dataTable.Rows(0).Item("ThisMonth").ToString
                result(1) = dataTable.Rows(0).Item("TopMonth").ToString
                result(2) = dataTable.Rows(0).Item("BottomMonth").ToString
            End If
        Catch ex As Exception
            Debug.Print("IsMonth" + ex.Message)
        End Try

        Return result
    End Function

    Public Function getTotalHR(ByVal CompID As String, ByVal EmpID As String, ByVal TDate As String) As String()
        Dim result(3) As String
        Dim tMonth() As String = getMonthTB(TDate)
        Dim SQL As StringBuilder = New StringBuilder
        Dim dataTable As DataTable
        Try
            SQL.AppendLine("Select OTStatus,OTTxnID,OTSeqNo,OTTotalTime,MealFlag,MealTime FROM OverTimeDeclaration")
            SQL.AppendLine("Where OTCompID = " & Bsp.Utility.Quote(CompID))
            SQL.AppendLine(" And OTStatus IN ('2','3','4')")
            SQL.AppendLine(" And OTEmpID = " & Bsp.Utility.Quote(EmpID))
            SQL.AppendLine(" And OTStartDate >= " & Bsp.Utility.Quote(tMonth(1).Replace("-", "/")))
            SQL.AppendLine(" And OTEndDate <= " & Bsp.Utility.Quote(tMonth(2).Replace("-", "/")))
            SQL.AppendLine("Order By OTStartDate")
            dataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, SQL.ToString, "AattendantDB").Tables(0)

            If dataTable.Rows.Count > 0 Then
                Dim rejectHR As Double
                Dim approveHR As Double
                Dim sendingHR As Double

                For i As Integer = 0 To dataTable.Rows.Count - 1 Step 1
                    Dim OTStatus As String = dataTable.Rows(i).Item("OTStatus").ToString
                    Dim OTTxnID As String = dataTable.Rows(i).Item("OTTxnID").ToString
                    Dim OTSeqNo As String = dataTable.Rows(i).Item("OTSeqNo").ToString
                    Dim OTTotalTime As Double = CDbl(dataTable.Rows(i).Item("OTTotalTime").ToString)
                    Dim MealFlag As String = dataTable.Rows(i).Item("MealFlag").ToString
                    Dim MealTime As String = CDbl(dataTable.Rows(i).Item("MealTime").ToString)
                    Dim newRowData As DataTable = dataTable.Clone

                    If MealFlag.Equals("1") And MealTime <> 0 Then
                        OTTotalTime = OTTotalTime - MealTime
                    End If

                    If OTSeqNo.Equals("1") Then
                        For j As Integer = 0 To dataTable.Rows.Count - 1 Step 1
                            Dim ChkTxnID As String = dataTable.Rows(j).Item("OTTxnID").ToString
                            Dim ChkSeqNo As String = dataTable.Rows(j).Item("OTSeqNo").ToString

                            If OTTxnID = ChkTxnID And "2" = ChkSeqNo Then
                                newRowData.ImportRow(dataTable.Rows(j))
                            End If
                        Next

                        If newRowData.Rows.Count = 0 Then
                            If OTStatus.Equals("2") Then
                                rejectHR += CDbl(FormatNumber((OTTotalTime / 60), 1))
                            ElseIf OTStatus.Equals("3") Then
                                approveHR += CDbl(FormatNumber((OTTotalTime / 60), 1))
                            ElseIf OTStatus.Equals("4") Then
                                sendingHR += CDbl(FormatNumber((OTTotalTime / 60), 1))
                            End If
                        Else
                            Dim OTTotalTime2 As Double = CDbl(newRowData.Rows(0).Item("OTTotalTime").ToString)
                            Dim MealFlag2 As String = newRowData.Rows(0).Item("MealFlag").ToString
                            Dim MealTime2 As String = CDbl(newRowData.Rows(0).Item("MealTime").ToString)

                            If MealFlag2.Equals("1") And MealTime <> 0 Then
                                OTTotalTime2 = OTTotalTime2 - MealTime2
                            End If

                            OTTotalTime = OTTotalTime + OTTotalTime2
                            If OTStatus.Equals("2") Then
                                rejectHR += CDbl(FormatNumber((OTTotalTime / 60), 1))
                            ElseIf OTStatus.Equals("3") Then
                                approveHR += CDbl(FormatNumber((OTTotalTime / 60), 1))
                            ElseIf OTStatus.Equals("4") Then
                                sendingHR += CDbl(FormatNumber((OTTotalTime / 60), 1))
                            End If
                        End If
                    End If
                    If newRowData.Rows.Count > 0 Then
                        newRowData = New DataTable
                    End If

                Next
                result(0) = IIf(tMonth(0).StartsWith("0"), tMonth(0).Substring(1), tMonth(0))
                result(1) = rejectHR
                result(2) = approveHR
                result(3) = sendingHR
            End If
        Catch ex As Exception
            Debug.Print("IsNothing" + ex.Message)
        End Try

        Return result
    End Function
End Class
