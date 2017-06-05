'****************************************************
'功能說明：OV1200的的查詢Funct
'建立日期：2017/02/02
'修改日期：2017/03/20
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

Public Class OV1_2

    Private Property _eHRMSDB_ITRD As String
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

    Private Property _AattendantDBName As String
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

    Public Sub New()

    End Sub


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

#Region "Query"

    ''' <summary>
    ''' 查詢DataTable
    ''' </summary>
    ''' <param name="strColumn"></param>
    ''' <param name="strTable"></param>
    ''' <param name="strWhere"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function QueryData(ByVal strColumn As String, ByVal strTable As String, ByVal strWhere As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" SELECT " + strColumn + " FROM " + strTable)
        strSQL.AppendLine(" WHERE 1=1 ")
        strSQL.AppendLine(strWhere)
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "AattendantDB").Tables(0)
    End Function

    ''' <summary>
    ''' 查詢Data
    ''' </summary>
    ''' <param name="strColumn"></param>
    ''' <param name="strTable"></param>
    ''' <param name="strWhere"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function QueryColumn(ByVal strColumn As String, ByVal strTable As String, ByVal strWhere As String) As String
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" SELECT " + strColumn + " FROM " + strTable)
        strSQL.AppendLine(" WHERE 1=1 ")
        strSQL.AppendLine(strWhere)
        Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "AattendantDB").Tables(0)
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item(strColumn).ToString()
            Else
                Return String.Empty
            End If
        End Using
    End Function

#End Region

#Region "附件相關"

    ''' <summary>
    ''' 清除附件
    ''' </summary>
    ''' <param name="AttachID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteAttach(ByVal AttachID As String) As Integer
        Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
        Dim dbcmd As DbCommand
        Dim strSQL As StringBuilder = New StringBuilder()
        Dim intRowsAffected As Integer = 0

        'string strDelSQL = string.Format("Update AttachInfo Set FileSize = -1 ,FileBody = null Where AttachID = '{0}' ;", _AttachID);

        strSQL.AppendLine(" UPDATE AttachInfo SET FileSize = -1,  FileBody = null WHERE AttachID = " + Bsp.Utility.Quote(AttachID))

        dbcmd = db.GetSqlStringCommand(strSQL.ToString())

        Using cn As DbConnection = db.CreateConnection()
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)

                tran.Commit()
                inTrans = False
            Catch ex As Exception
                If inTrans Then tran.Rollback()
                Debug.Print("DeleteAttach()==>" + ex.Message)
                Throw
            Finally
                tran.Dispose()
                If cn.State = ConnectionState.Open Then cn.Close()
            End Try
        End Using

        If intRowsAffected >= 0 Then
            Try
                Util.IsAttachInfoLog("AattendantDB", AttachID, 1, "Delete")
            Catch ex As Exception
                Debug.Print("DeleteAttach()==>" + ex.Message)
            End Try
        End If
        Return intRowsAffected
    End Function

    ''' <summary>
    ''' 新增附件
    ''' </summary>
    ''' <param name="AttachID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertAttach(ByVal AttachID As String, ByVal attach As String) As Integer
        Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
        Dim dbcmd As DbCommand
        Dim strSQL As StringBuilder = New StringBuilder()
        Dim intRowsAffected As Integer = 0

        'string strDelSQL = string.Format("Update AttachInfo Set FileSize = -1 ,FileBody = null Where AttachID = '{0}' ;", _AttachID);

        strSQL.AppendLine(" INSERT INTO AttachInfo (AttachID, SeqNo, FileName, FileExtName, FileSize, AnonymousAccess, UpdUser, UpdDate, UpdTime, FileBody, MD5Check) ")
        strSQL.AppendLine(" SELECT " + Bsp.Utility.Quote(AttachID))
        strSQL.AppendLine(" , SeqNo, FileName, FileExtName, FileSize, AnonymousAccess, UpdUser, UpdDate, UpdTime, FileBody, MD5Check ")
        strSQL.AppendLine(" FROM AttachInfo ")
        strSQL.AppendLine(" WHERE AttachID = " + Bsp.Utility.Quote(attach))

        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
        Using cn As DbConnection = db.CreateConnection()
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction()
            Dim inTrans As Boolean = True

            Try
                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)

                tran.Commit()
                inTrans = False
            Catch ex As Exception
                If inTrans Then tran.Rollback()
                Debug.Print("InsertAttach()==>" + ex.Message)
                Throw
            Finally
                tran.Dispose()
                If cn.State = ConnectionState.Open Then cn.Close()
            End Try
        End Using
        Return intRowsAffected
    End Function

    ''' <summary>
    ''' 附件編號(加班人)
    ''' </summary>
    ''' <param name="AttachID"></param>
    ''' <param name="EmpID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function QueryAttach(ByVal AttachID As String, ByVal CompID As String, ByVal EmpID As String) As String
        Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
        Dim dbcmd As DbCommand
        Dim strSQL As StringBuilder = New StringBuilder()
        Dim intRowsAffected As Integer = 0
        Dim attach As String = String.Empty
        Try
            Using dt As DataTable = QueryData("AttachID", "AttachInfo", "AND AttachID = " + Bsp.Utility.Quote(AttachID) + " AND FileSize > 0")
                If dt.Rows.Count > 0 Then
                    Using dtOTSeq As DataTable = QueryData("FileSeq, CONVERT(CHAR(10), LastChgDate, 111) AS LastChgDate", "OverTimeSeq", "AND CompID = " + Bsp.Utility.Quote(CompID) + " AND EmpID = " + Bsp.Utility.Quote(EmpID))
                        If dtOTSeq.Rows.Count > 0 Then
                            If dtOTSeq.Rows(0).Item("LastChgDate").ToString() <> Date.Now.ToString("yyyy/MM/dd") Then       '不是今天的資料
                                strSQL.AppendLine(" UPDATE OverTimeSeq SET FileSeq = '1', LastChgDate = GETDATE() ")
                                strSQL.AppendLine(" WHERE CompID = " + Bsp.Utility.Quote(CompID))
                                strSQL.AppendLine(" AND EmpID = " + Bsp.Utility.Quote(EmpID))

                                attach = Date.Now.ToString("yyyyMMdd") + "01" + EmpID
                            Else
                                Dim AttSeq As Integer = (Convert.ToInt32(dtOTSeq.Rows(0).Item("FileSeq").ToString()) + 1)
                                strSQL.AppendLine(" UPDATE OverTimeSeq SET FileSeq = " + Bsp.Utility.Quote(AttSeq) + ", LastChgDate = GETDATE() ")
                                strSQL.AppendLine(" WHERE CompID = " + Bsp.Utility.Quote(CompID))
                                strSQL.AppendLine(" AND EmpID = " + Bsp.Utility.Quote(EmpID))

                                attach = Date.Now.ToString("yyyyMMdd") + AttSeq.ToString("00") + EmpID
                            End If
                        Else
                            strSQL.AppendLine(" INSERT INTO OverTimeSeq (CompID, EmpID, AdvanceFormSeq, DeclarationFormSeq, FileSeq, LastChgDate) ")
                            strSQL.AppendLine(" VALUES (" + Bsp.Utility.Quote(CompID) + ", " + Bsp.Utility.Quote(EmpID) + ", '1' , '1', '1', GETDATE())")

                            attach = Date.Now.ToString("yyyyMMdd") + "01" + EmpID
                        End If

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        Using cn As DbConnection = db.CreateConnection()
                            cn.Open()
                            db.ExecuteNonQuery(dbcmd)
                        End Using

                        '清除
                        strSQL.Clear()
                        dbcmd.Dispose()

                        If Not String.IsNullOrEmpty(attach) Then
                            strSQL.AppendLine(" UPDATE AttachInfo SET AttachID = " + Bsp.Utility.Quote(attach))
                            strSQL.AppendLine(" WHERE AttachID = " + Bsp.Utility.Quote(AttachID))

                            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                            Using cn As DbConnection = db.CreateConnection()
                                cn.Open()
                                db.ExecuteNonQuery(dbcmd)
                            End Using
                            'attach = Date.Now.ToString("yyyyMMdd") + "01" + EmpID
                        Else
                            'strSQL.AppendLine(" DELETE FROM AttachInfo ")
                            'strSQL.AppendLine(" WHERE AttachID = " + Bsp.Utility.Quote(AttachID))
                        End If
                    End Using
                Else
                    '清除
                    attach = String.Empty
                    strSQL.Clear()

                    strSQL.AppendLine(" DELETE FROM AttachInfo ")
                    strSQL.AppendLine(" WHERE AttachID = " + Bsp.Utility.Quote(AttachID))

                    dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                    Using cn As DbConnection = db.CreateConnection()
                        cn.Open()
                        db.ExecuteNonQuery(dbcmd)
                    End Using
                End If
            End Using
            Return attach
        Catch ex As Exception
            Debug.Print("QueryAttach()==>" + ex.Message)
            Throw
        End Try
    End Function

#End Region

    ''' <summary>
    ''' 查詢序號
    ''' </summary>
    ''' <param name="strTable"></param>
    ''' <param name="CompID"></param>
    ''' <param name="EmpID"></param>
    ''' <param name="StartDate"></param>
    ''' <returns></returns>
    ''' <remarks>加班人於同一加班日之第N張申報單</remarks>
    Public Function QuerySeq(ByVal strTable As String, ByVal CompID As String, ByVal EmpID As String, ByVal StartDate As String) As Integer
        Try
            Using dt As DataTable = QueryData("MAX(OTSeq) AS MAXOTSeq", strTable, "AND OTCompID= " + Bsp.Utility.Quote(CompID) + " AND OTEmpID= " + Bsp.Utility.Quote(EmpID) + " AND OTStartDate = " + Bsp.Utility.Quote(StartDate))
                If dt.Rows.Count = 0 Then
                    Return 1
                Else
                    Return Convert.ToInt32(If(dt.Rows(0).Item("MAXOTSeq").ToString() = String.Empty, "0", dt.Rows(0).Item("MAXOTSeq").ToString())) + 1
                End If
            End Using
        Catch ex As Exception
            Debug.Print("QueryAttach()==>" + ex.Message)
            Throw
        End Try
    End Function

    ''' <summary>
    ''' 取得表單編號(登錄者)
    ''' </summary>
    ''' <param name="strColumn"></param>
    ''' <param name="strCompID"></param>
    ''' <param name="strEmpID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function QueryFormNO(ByVal strColumn As String, ByVal strCompID As String, ByVal strEmpID As String) As String
        Dim strFormNo As String = ""
        Dim strSQL As New StringBuilder()
        Try
            Using dt As DataTable = QueryData(strColumn + ", FileSeq,CONVERT(CHAR(10), LastChgDate, 111) AS LastChgDate", "OverTimeSeq", "AND CompID = " + Bsp.Utility.Quote(strCompID) + " AND EmpID = " + Bsp.Utility.Quote(strEmpID))
                If dt.Rows.Count > 0 Then
                    If dt.Rows(0).Item("LastChgDate").ToString() <> Date.Now.ToString("yyyy/MM/dd") Then        '不是今天的資料
                        strSQL.Clear()
                        strSQL.Append(" UPDATE OverTimeSeq SET AdvanceFormSeq = '01', DeclarationFormSeq = '01', FileSeq = '01', LastChgDate = GETDATE() ")
                        strSQL.Append(" WHERE CompID = " + Bsp.Utility.Quote(strCompID))
                        strSQL.Append(" AND EmpID = " + Bsp.Utility.Quote(strEmpID))
                        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString, "AattendantDB")
                        strFormNo = Date.Now.ToString("yyyyMMdd") + "01" + strEmpID
                    Else
                        Dim FormSeq As Integer = (Convert.ToInt32(dt.Rows(0).Item("" + strColumn + "").ToString()) + 1)
                        strSQL.Clear()
                        strSQL.Append(" UPDATE OverTimeSeq SET " + strColumn + "=" + Bsp.Utility.Quote(FormSeq) + ", LastChgDate=GETDATE() ")
                        strSQL.Append(" WHERE CompID = " + Bsp.Utility.Quote(strCompID))
                        strSQL.Append(" AND EmpID = " + Bsp.Utility.Quote(strEmpID))
                        Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString, "AattendantDB")
                        strFormNo = Date.Now.ToString("yyyyMMdd") + FormSeq.ToString("00") + strEmpID
                    End If
                Else
                    strSQL.Clear()
                    strSQL.Append(" INSERT INTO OverTimeSeq (CompID, EmpID, AdvanceFormSeq, DeclarationFormSeq, FileSeq, LastChgDate) ")
                    strSQL.Append(" VALUES (" + Bsp.Utility.Quote(strCompID) + ", " + Bsp.Utility.Quote(strEmpID) + ", '1', '1', '', GETDATE())")
                    Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString, "AattendantDB")
                    strFormNo = Date.Now.ToString("yyyyMMdd") + "01" + strEmpID
                End If
            End Using
        Catch ex As Exception
            Debug.Print("QueryFormNO()==>" + ex.Message)
        End Try
        Return strFormNo
    End Function

    ''' <summary>
    ''' 查詢員工基本資料
    ''' </summary>
    ''' <param name="CompID"></param>
    ''' <param name="OrganID"></param>
    ''' <returns>System.Data.DataSet</returns>
    ''' <remarks></remarks>
    Public Function GetEmpData(ByVal CompID As String, ByVal EmpID As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.Append(" SELECT P.EmpID, P.Sex, P.RankID, P.EmpDate, P.WorkSiteID, P.DeptID, OD.OrganName AS DeptName, P.OrganID, O.OrganName ")
        strSQL.Append(" FROM Personal P ")
        strSQL.Append(" LEFT JOIN Organization OD ON P.CompID = OD.CompID AND P.DeptID = OD.OrganID AND OD.VirtualFlag='0' AND OD.InValidFlag = '0' ")
        strSQL.Append(" LEFT JOIN Organization O  ON P.CompID = O.CompID AND P.OrganID = O.OrganID AND O.VirtualFlag='0' AND O.InValidFlag = '0' ")
        strSQL.Append(" Where P.CompID =" & Bsp.Utility.Quote(CompID))
        strSQL.Append(" And P.EmpID = " & Bsp.Utility.Quote(EmpID))
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "eHRMSDB").Tables(0)
    End Function

    ''' <summary>
    ''' 用CompID、員工編號查詢員工姓名
    ''' </summary>
    ''' <param name="CompID"></param>
    ''' <param name="EmpName"></param>
    ''' <returns>System.Data.DataSet</returns>
    ''' <remarks></remarks>
    Public Function GetEmpID(ByVal CompID As String, ByVal EmpName As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.Append("Select EmpID")
        strSQL.Append(" From Personal With(NoLock)")
        strSQL.Append(" Where CompID =" & Bsp.Utility.Quote(CompID))
        strSQL.Append(" And NameN = " & Bsp.Utility.Quote(EmpName))
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "eHRMSDB").Tables(0)
    End Function

    ''' <summary>
    ''' 用CompID、員工編號查詢員工姓名
    ''' </summary>
    ''' <param name="CompID"></param>
    ''' <param name="EmpID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEmpName(ByVal CompID As String, ByVal EmpID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select NameN From Personal")
        strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine("And EmpID = " & Bsp.Utility.Quote(EmpID))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

#Region "檢核時間重疊-OV1201"

    ''' <summary>
    ''' 檢核時間重疊(OverTime_BK)
    ''' </summary>
    ''' <param name="StartDate"></param>
    ''' <param name="EndDate"></param>
    ''' <param name="OTEmpID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckOverTimeBK(ByVal StartDate As String, ByVal EndDate As String, ByVal OTEmpID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT BeginTime, EndTime FROM OverTime_BK WHERE EmpID = " + Bsp.Utility.Quote(OTEmpID))

        If StartDate <> "" Then
            If EndDate <> "" Then
                If StartDate = EndDate Then
                    strSQL.AppendLine(" AND Convert(varchar,OTDate,111) = " + Bsp.Utility.Quote(StartDate))
                Else
                    strSQL.AppendLine(" AND Convert(varchar,OTDate,111) IN ( " + Bsp.Utility.Quote(StartDate) + "," + Bsp.Utility.Quote(EndDate) + ")")
                End If
            End If
        End If

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "AattendantDB").Tables(0)
    End Function

    ''' <summary>
    ''' 檢核時間重疊(NaturalDisasterByCity)
    ''' </summary>
    ''' <param name="StartDate"></param>
    ''' <param name="EndDate"></param>
    ''' <param name="WorkSiteID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckNaturalDisasterByCity(ByVal StartDate As String, ByVal EndDate As String, ByVal WorkSiteID As String, ByVal CompID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT BeginTime, EndTime FROM NaturalDisasterByCity WHERE WorkSiteID = " + Bsp.Utility.Quote(WorkSiteID) + " AND CompID=" + Bsp.Utility.Quote(CompID))

        If StartDate <> "" Then
            If EndDate <> "" Then
                If StartDate = EndDate Then
                    strSQL.AppendLine(" AND DisasterStartDate = " + Bsp.Utility.Quote(StartDate))
                Else
                    strSQL.AppendLine(" AND DisasterStartDate IN ( " + Bsp.Utility.Quote(StartDate) + "," + Bsp.Utility.Quote(EndDate) + ")")
                End If
            End If
        End If

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "AattendantDB").Tables(0)
    End Function

    ''' <summary>
    ''' 檢核時間重疊(CheckNaturalDisasterByEmp)
    ''' </summary>
    ''' <param name="StartDate"></param>
    ''' <param name="EndDate"></param>
    ''' <param name="EmpID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckNaturalDisasterByEmp(ByVal StartDate As String, ByVal EndDate As String, ByVal EmpID As String, ByVal CompID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("SELECT BeginTime,EndTime,LEFT(BeginTime,2) AS StartTimeHr,RIGHT(BeginTime,2) AS StartTimeM,LEFT(EndTime,2) AS EndTimeHr,RIGHT(EndTime,2) AS EndTimeM FROM NaturalDisasterByEmp  WHERE EmpID= " + Bsp.Utility.Quote(EmpID) + " AND CompID=" + Bsp.Utility.Quote(CompID))

        If StartDate <> "" Then
            If EndDate <> "" Then
                If StartDate = EndDate Then
                    strSQL.AppendLine(" AND DisasterStartDate = " + Bsp.Utility.Quote(StartDate))
                Else
                    strSQL.AppendLine(" AND DisasterStartDate IN ( " + Bsp.Utility.Quote(StartDate) + "," + Bsp.Utility.Quote(EndDate) + ")")
                End If
            End If
        End If

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "AattendantDB").Tables(0)
    End Function

    ''' <summary>
    ''' 檢核時間重疊(事後申報)
    ''' </summary>
    ''' <param name="StartDate"></param>
    ''' <param name="EndDate"></param>
    ''' <param name="OTEmpID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckOverTimeDeclaration(ByVal StartDate As String, ByVal EndDate As String, ByVal OTEmpID As String, ByVal OTCompID As String) As DataTable
        Dim strSQL As New StringBuilder()

        If StartDate <> "" Then
            If EndDate <> "" Then
                If StartDate = EndDate Then
                    strSQL.AppendLine("SELECT OTStartTime,OTEndTime FROM OverTimeDeclaration  WHERE OTStatus in ('1','2','3') AND OTCompID=" + Bsp.Utility.Quote(OTCompID) + " AND OTEmpID = " + Bsp.Utility.Quote(OTEmpID) + " AND OTStartDate = " + Bsp.Utility.Quote(StartDate) + " AND OTEndDate = " + Bsp.Utility.Quote(EndDate))
                Else
                    strSQL.AppendLine(" SELECT OTTxnID,OTStartDate,OTStartTime,OTEndTime FROM OverTimeDeclaration WHERE OTCompID = " + Bsp.Utility.Quote(OTCompID) + "AND  OTEmpID = " + Bsp.Utility.Quote(OTEmpID) + " AND OTStatus IN ('1','2','3') AND OTStartDate IN ( " + Bsp.Utility.Quote(StartDate) + "," + Bsp.Utility.Quote(EndDate) + ")")
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
    Public Function CheckOverTimeAdvance(ByVal StartDate As String, ByVal EndDate As String, ByVal OTEmpID As String, ByVal OTCompID As String) As DataTable
        Dim strSQL As New StringBuilder()

        If StartDate <> "" Then
            If EndDate <> "" Then
                If StartDate = EndDate Then
                    strSQL.AppendLine("SELECT OTStartTime,OTEndTime FROM OverTimeAdvance  WHERE OTStatus in ('1','2','3') AND OTCompID = " + Bsp.Utility.Quote(OTCompID) + " AND OTEmpID = " + Bsp.Utility.Quote(OTEmpID) + " AND OTStartDate = " + Bsp.Utility.Quote(StartDate) + " AND OTEndDate = " + Bsp.Utility.Quote(EndDate))
                Else
                    strSQL.AppendLine(" SELECT OTTxnID,OTStartDate,OTStartTime,OTEndTime FROM OverTimeAdvance WHERE OTCompID = " + Bsp.Utility.Quote(OTCompID) + "AND OTEmpID = " + Bsp.Utility.Quote(OTEmpID) + " AND OTStatus IN ('1','2','3') AND OTStartDate IN ( " + Bsp.Utility.Quote(StartDate) + "," + Bsp.Utility.Quote(EndDate) + ")")
                End If
            End If
        End If

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "AattendantDB").Tables(0)
    End Function

#End Region

#Region "多筆送簽檢核"

    ''' <summary>
    ''' 檢查多筆加班人是否超過每個月的加班時數上限
    ''' </summary>
    ''' <param name="dt">加班單dataTable</param>
    ''' <param name="MonthLimitHour">每月上限</param>
    ''' <param name="Table">OverTimeDeclaration</param>
    ''' <returns>結果字串</returns>
    ''' <remarks></remarks>
    Public Function GetMulitTotal(dt As DataTable, MonthLimitHour As Double, Table As String) As String
        'Dim db As New DbHelper(_AattendantDBName)
        'Dim sb As CommandHelper = db.CreateCommandHelper()
        Dim sb As New StringBuilder
        Dim dbTotal As Double = 0
        Dim strEmp As String = ""
        Dim strMonth As String = ""
        Dim strYear As String = ""
        Dim msg As String = ""
        Dim dtTotal As DataTable = Nothing
        dt.DefaultView.Sort = "EmpID,OTStartDate asc"
        Dim strOTTxnID As String = ""
        Dim b As String = ""
        Dim strOTFromAdvanceTxnId As String = ""
        Dim strOTFromAdvanceTxnId_1 As String = ""
        For i As Integer = 0 To dt.Rows.Count - 1
            strOTTxnID += dt.Rows(i)("OTTxnID").ToString() + ";"
            If Table = "OverTimeDeclaration" Then
                strOTFromAdvanceTxnId += dt.Rows(i)("OTFromAdvanceTxnId").ToString() + ";"
            End If
        Next
        If Table = "OverTimeDeclaration" Then
            strOTFromAdvanceTxnId = "'" + (strOTFromAdvanceTxnId.Replace(";", "','")) + "'"
            strOTFromAdvanceTxnId_1 = strOTFromAdvanceTxnId.Substring(0, strOTFromAdvanceTxnId.Length - 3)
        End If
        strOTTxnID = "'" + (strOTTxnID.Replace(";", "','")) + "'"
        Dim strOTTxnID_1 As String = strOTTxnID.Substring(0, strOTTxnID.Length - 3)

        For j As Integer = 0 To dt.Rows.Count - 1
            If strEmp <> dt.Rows(j)("EmpID").ToString() OrElse strMonth <> dt.Rows(j)("OTStartDate").ToString().Substring(5, 2) OrElse strYear <> dt.Rows(j)("OTStartDate").ToString().Substring(0, 4) Then    '不同員編

                strEmp = dt.Rows(j)("EmpID").ToString()
                strMonth = dt.Rows(j)("OTStartDate").ToString().Substring(5, 2)
                strYear = dt.Rows(j)("OTStartDate").ToString().Substring(0, 4)
                '本月加班時數
                sb.Clear()
                sb.Append(" SELECT ISNULL(SUM(ISNULL(E.TotalTime,0)),0) AS TotalTime FROM(")
                '平日計算
                sb.Append(" SELECT ISNULL(SUM(ISNULL(A.TotalTime,0)),0) AS TotalTime FROM(")
                '事前平日計算(如事後有此筆，以事後為主)
                sb.Append(" SELECT ISNULL(SUM(ISNULL(OTTotalTime,0))-SUM(ISNULL(MealTime,0)),0) AS TotalTime  FROM OverTimeAdvance")
                sb.Append(" WHERE OTCompID='" + dt.Rows(j)("OTCompID").ToString() + "' AND OTEmpID='" + dt.Rows(j)("EmpID").ToString() + "' AND OTStatus IN ('2','3') AND YEAR(OTStartDate)=YEAR('" + dt.Rows(j)("OTStartDate").ToString() + "') AND MONTH(OTStartDate)=MONTH('" + dt.Rows(j)("OTStartDate").ToString() + "') AND HolidayOrNot='0'")
                sb.Append(" AND OTTxnID NOT IN")
                sb.Append(" (SELECT OTFromAdvanceTxnId FROM OverTimeDeclaration WHERE OTCompID='" + dt.Rows(j)("OTCompID").ToString() + "' AND OTEmpID='" + dt.Rows(j)("EmpID").ToString() + "' AND YEAR(OTStartDate)=YEAR('" + dt.Rows(j)("OTStartDate").ToString() + "') AND MONTH(OTStartDate)=MONTH('" + dt.Rows(j)("OTStartDate").ToString() + "') AND HolidayOrNot='0' AND OTStatus in ('2','3') AND OTFromAdvanceTxnId<>'')")

                If Table = "OverTimeDeclaration" Then
                    sb.Append(" AND OTTxnID NOT IN(" + strOTFromAdvanceTxnId_1 + ")")
                End If

                sb.Append(" UNION ALL")
                '事後平日計算
                sb.Append(" SELECT ISNULL(SUM(OTTotalTime)-SUM(MealTime),0) AS TotalTime  FROM OverTimeDeclaration")
                sb.Append(" WHERE OTCompID='" + dt.Rows(j)("OTCompID").ToString() + "' AND OTEmpID='" + dt.Rows(j)("EmpID").ToString() + "' AND OTStatus IN ('2','3') AND YEAR(OTStartDate)=YEAR('" + dt.Rows(j)("OTStartDate").ToString() + "') AND MONTH(OTStartDate)=MONTH('" + dt.Rows(j)("OTStartDate").ToString() + "') AND HolidayOrNot='0'")
                sb.Append(" UNION ALL ")
                '本單平日計算
                sb.Append(" SELECT ISNULL(SUM(ISNULL(OTTotalTime,0))-SUM(ISNULL(MealTime,0)),0) AS TotalTime FROM " + Table)
                sb.Append(" WHERE OTCompID='" + dt.Rows(j)("OTCompID").ToString() + "' AND OTEmpID='" + dt.Rows(j)("EmpID").ToString() + "' AND YEAR(OTStartDate)=YEAR('" + dt.Rows(j)("OTStartDate").ToString() + "') AND MONTH(OTStartDate)=MONTH('" + dt.Rows(j)("OTStartDate").ToString() + "') AND HolidayOrNot='0' AND OTTxnID IN(" + strOTTxnID_1 + ")")
                sb.Append(" ) A")
                sb.Append(" UNION ALL")
                '假日計算會有成長時數，以及排除國定假日(如事後有此筆，以事後為主)
                sb.Append(" SELECT ISNULL(SUM(ISNULL(D.TotalTime,0)),0) AS TotalTime FROM (")
                sb.Append(" SELECT CASE WHEN C.TotalTime >0 AND C.TotalTime <= 240 THEN 240")
                sb.Append(" WHEN C.TotalTime > 240 AND C.TotalTime <= 480 THEN 480")
                sb.Append(" WHEN C.TotalTime > 480 AND C.TotalTime <= 720 THEN 720 END AS TotalTime")
                sb.Append(" FROM (")
                sb.Append(" SELECT ISNULL(SUM(ISNULL(B.OTTotalTime,0))-SUM(ISNULL(B.MealTime,0)),0) AS TotalTime FROM (")
                '事前假日(如事後有此筆，以事後為主)
                sb.Append(" SELECT OTStartDate, ISNULL(OTTotalTime,0) OTTotalTime, ISNULL(MealTime,0) MealTime FROM OverTimeAdvance")
                sb.Append(" WHERE OTCompID='" + dt.Rows(j)("OTCompID").ToString() + "' AND OTEmpID='" + dt.Rows(j)("EmpID").ToString() + "' AND OTStatus IN ('2','3') AND YEAR(OTStartDate)=YEAR('" + dt.Rows(j)("OTStartDate").ToString() + "') AND MONTH(OTStartDate)=MONTH('" + dt.Rows(j)("OTStartDate").ToString() + "') AND HolidayOrNot='1'")
                sb.Append(" AND OTStartDate NOT IN (SELECT Code FROM AT_CodeMap WHERE TabName = 'NationalHolidayDefine' AND FldName='HolidayDate' AND NotShowFlag='0')")
                sb.Append(" AND OTTxnID NOT IN")
                sb.Append(" (SELECT OTFromAdvanceTxnId FROM OverTimeDeclaration WHERE OTCompID='" + dt.Rows(j)("OTCompID").ToString() + "' AND OTEmpID='" + dt.Rows(j)("EmpID").ToString() + "' AND YEAR(OTStartDate)=YEAR('" + dt.Rows(j)("OTStartDate").ToString() + "') AND MONTH(OTStartDate)=MONTH('" + dt.Rows(j)("OTStartDate").ToString() + "') AND HolidayOrNot='1' AND OTStatus in ('2','3') AND OTFromAdvanceTxnId<>'')")

                If Table = "OverTimeDeclaration" Then
                    sb.Append(" AND OTTxnID NOT IN(" + strOTFromAdvanceTxnId_1 + ")")
                End If
                sb.Append(" UNION ALL")
                '事後假日
                sb.Append(" SELECT OTStartDate, ISNULL(OTTotalTime,0) OTTotalTime, ISNULL(MealTime,0) MealTime FROM OverTimeDeclaration")
                sb.Append(" WHERE OTCompID='" + dt.Rows(j)("OTCompID").ToString() + "' AND OTEmpID='" + dt.Rows(j)("EmpID").ToString() + "' AND OTStatus IN ('2','3') AND YEAR(OTStartDate)=YEAR('" + dt.Rows(j)("OTStartDate").ToString() + "') AND MONTH(OTStartDate)=MONTH('" + dt.Rows(j)("OTStartDate").ToString() + "') AND HolidayOrNot='1'")
                sb.Append(" AND OTStartDate NOT IN (SELECT Code FROM AT_CodeMap WHERE TabName = 'NationalHolidayDefine' AND FldName='HolidayDate' AND NotShowFlag='0')")
                sb.Append(" UNION ALL")
                '本單假日
                sb.Append(" SELECT OTStartDate, ISNULL(OTTotalTime,0) OTTotalTime, ISNULL(MealTime,0) MealTime FROM " + Table)
                sb.Append(" WHERE OTCompID='" + dt.Rows(j)("OTCompID").ToString() + "' AND OTEmpID='" + dt.Rows(j)("EmpID").ToString() + "' AND YEAR(OTStartDate)=YEAR('" + dt.Rows(j)("OTStartDate").ToString() + "') AND MONTH(OTStartDate)=MONTH('" + dt.Rows(j)("OTStartDate").ToString() + "') AND HolidayOrNot='1'")
                sb.Append(" AND OTStartDate NOT IN (SELECT Code FROM AT_CodeMap WHERE TabName = 'NationalHolidayDefine' AND FldName='HolidayDate' AND NotShowFlag='0')")
                sb.Append(" AND OTTxnID IN(" + strOTTxnID_1 + ") ")
                sb.Append(" ) B ")
                sb.Append(" GROUP BY B.OTStartDate")
                sb.Append(" ) C ) D ) E")

                dtTotal = Bsp.DB.ExecuteDataSet(CommandType.Text, sb.ToString, "AattendantDB").Tables(0)
                dbTotal = Convert.ToDouble(dtTotal.Rows(0)("TotalTime").ToString())
                If dbTotal > (MonthLimitHour * 60) Then
                    msg = "False" + ";" + dt.Rows(j)("EmpID").ToString() + ";" + dt.Rows(j)("OTStartDate").ToString()
                    Return msg
                End If
            End If
        Next
        Return "True" + ";" + ""
    End Function

    'Public Function GetMulitTotal(dt As DataTable, MonthLimitHour As Double, Table As String) As String
    '    Dim db As New DbHelper(_AattendantDBName)
    '    Dim sb As CommandHelper = db.CreateCommandHelper()
    '    Dim dbTotal As Double = 0
    '    Dim strEmp As String = ""
    '    Dim strMonth As String = ""
    '    Dim msg As String = ""
    '    Dim dtTotal As DataTable = Nothing
    '    dt.DefaultView.Sort = "EmpID,OTStartDate ASC"
    '    Dim strOTTxnID As String = ""
    '    Dim b As String = ""
    '    Dim strOTFromAdvanceTxnId As String = ""
    '    For i As Integer = 0 To dt.Rows.Count - 1
    '        strOTTxnID += dt.Rows(i)("OTTxnID").ToString() + ";"
    '        If Table = "OverTimeDeclaration" Then
    '            strOTFromAdvanceTxnId += dt.Rows(i)("OTFromAdvanceTxnId").ToString() + ";"
    '        End If
    '    Next
    '    If Table = "OverTimeDeclaration" Then
    '        Dim a As String = "'" + (strOTFromAdvanceTxnId.Replace(";", ",")) + "'"
    '        b = a.Substring(0, a.Length - 3)
    '    End If
    '    dt = GroupTable(dt, strOTTxnID, strOTFromAdvanceTxnId, Table)
    '    For j As Integer = 0 To dt.Rows.Count - 1
    '        If strEmp <> dt.Rows(j)("OTEmpID").ToString() OrElse strMonth <> dt.Rows(j)("OTStartDate").ToString().Substring(5, 2) Then      '不同員編
    '            If dateTotal + dbTotal - dbHoTotal_SUM > MonthLimitHour Then
    '                msg = "False" + ";" + dt.Rows(j)("OTEmpID").ToString() + ";" + dt.Rows(j)("OTStartDate").ToString()
    '                Return msg
    '            End If
    '            strEmp = dt.Rows(j)("OTEmpID").ToString()
    '            strMonth = dt.Rows(j)("OTStartDate").ToString().Substring(5, 2)
    '            sb.Reset()
    '            sb.Append(" SELECT ISNULL(SUM(TotalTime),0) AS TotalTime FROM(")
    '            sb.Append(" SELECT SUM(TotalTime) AS TotalTime FROM(")
    '            sb.Append(" SELECT Convert(Decimal(10,1),ROUND(Convert(Decimal(10,2),ISNULL(OTTotalTime,0)-ISNULL(MealTime,0))/60,1)) AS TotalTime FROM OverTimeAdvance")
    '            sb.Append(" WHERE OTCompID='" + dt.Rows(j)("OTCompID").ToString() + "' AND OTEmpID='" + dt.Rows(j)("OTEmpID").ToString() + "' AND OTStatus IN ('2','3') AND MONTH(OTStartDate)=MONTH('" + dt.Rows(j)("OTStartDate").ToString() + "') AND HolidayOrNot='0'")
    '            sb.Append(" AND OTTxnID NOT IN")
    '            sb.Append(" (SELECT OTFromAdvanceTxnId FROM OverTimeDeclaration WHERE OTCompID='" + dt.Rows(j)("OTCompID").ToString() + "' AND OTEmpID='" + dt.Rows(j)("OTEmpID").ToString() + "' AND MONTH(OTStartDate)=MONTH('" + dt.Rows(j)("OTStartDate").ToString() + "') AND HolidayOrNot='0' AND OTStatus in ('2','3') AND OTFromAdvanceTxnId<>'')")
    '            If Table = "OverTimeDeclaration" Then
    '                sb.Append(" AND OTTxnID NOT IN(" + b + ")")
    '            End If
    '            sb.Append(" ) OTAN")
    '            sb.Append(" UNION ALL")
    '            sb.Append(" SELECT SUM(TotalTime) AS TotalTime FROM(")
    '            sb.Append(" SELECT Convert(Decimal(10,1),ROUND(Convert(Decimal(10,2),ISNULL(OTTotalTime,0)-ISNULL(MealTime,0))/60,1)) AS TotalTime FROM OverTimeDeclaration")
    '            sb.Append(" WHERE OTCompID='" + dt.Rows(j)("OTCompID").ToString() + "' AND OTEmpID='" + dt.Rows(j)("OTEmpID").ToString() + "' AND OTStatus IN ('2','3') AND MONTH(OTStartDate)=MONTH('" + dt.Rows(j)("OTStartDate").ToString() + "') AND HolidayOrNot='0'")
    '            sb.Append(" ) OTDN")
    '            sb.Append(" UNION ALL")
    '            sb.Append(" SELECT CASE WHEN  TotalTime>0 AND TotalTime <= 4 THEN 4")
    '            sb.Append(" WHEN TotalTime > 4 AND TotalTime <= 8 THEN 8")
    '            sb.Append(" WHEN TotalTime > 8 AND TotalTime <= 12 THEN 12 END AS TotalTime")
    '            sb.Append(" FROM (")
    '            sb.Append(" SELECT Convert(Decimal(10,1),ROUND(Convert(Decimal(10,2),A.TotalTime)/60,1)) AS TotalTime FROM (")
    '            sb.Append(" SELECT SUM(ISNULL(OTTotalTime,0)-ISNULL(MealTime,0)) AS TotalTime  FROM OverTimeAdvance")
    '            sb.Append(" WHERE OTCompID='" + dt.Rows(j)("OTCompID").ToString() + "' AND OTEmpID='" + dt.Rows(j)("OTEmpID").ToString() + "' AND OTStatus IN ('2','3') AND MONTH(OTStartDate)=MONTH('" + dt.Rows(j)("OTStartDate").ToString() + "') AND HolidayOrNot='1'")
    '            sb.Append(" AND OTTxnID NOT IN")
    '            sb.Append(" (SELECT OTFromAdvanceTxnId FROM OverTimeDeclaration WHERE OTCompID='" + dt.Rows(j)("OTCompID").ToString() + "' AND OTEmpID='" + dt.Rows(j)("OTEmpID").ToString() + "' AND MONTH(OTStartDate)=MONTH('" + dt.Rows(j)("OTStartDate").ToString() + "') AND HolidayOrNot='1' AND OTStatus in ('2','3') AND OTFromAdvanceTxnId<>'')")
    '            sb.Append(" AND OTStartDate NOT IN (SELECT Code FROM AT_CodeMap WHERE TabName = 'NationalHolidayDefine' AND FldName='HolidayDate' AND NotShowFlag='0')")
    '            sb.Append(" GROUP BY OTStartDate) A")
    '            sb.Append(" ) OTAH")
    '            sb.Append(" UNION ALL")
    '            sb.Append(" SELECT CASE WHEN TotalTime>0 AND TotalTime <= 4 THEN 4")
    '            sb.Append(" WHEN TotalTime > 4 AND TotalTime <= 8 THEN 8")
    '            sb.Append(" WHEN TotalTime > 8 AND TotalTime <= 12 THEN 12 ")
    '            sb.Append(" ELSE 0 END AS TotalTime")
    '            sb.Append(" FROM (")
    '            sb.Append(" SELECT Convert(Decimal(10,1),ROUND(Convert(Decimal(10,2),A.TotalTime)/60,1)) AS TotalTime FROM (")
    '            sb.Append(" SELECT SUM(ISNULL(OTTotalTime,0)-ISNULL(MealTime,0)) AS TotalTime  FROM OverTimeDeclaration")
    '            sb.Append(" WHERE OTCompID='" + dt.Rows(j)("OTCompID").ToString() + "' AND OTEmpID='" + dt.Rows(j)("OTEmpID").ToString() + "' AND OTStatus IN ('2','3') AND MONTH(OTStartDate)=MONTH('" + dt.Rows(j)("OTStartDate").ToString() + "') AND HolidayOrNot='1'")
    '            sb.Append(" AND OTStartDate NOT IN (SELECT Code FROM AT_CodeMap WHERE TabName = 'NationalHolidayDefine' AND FldName='HolidayDate' AND NotShowFlag='0')")
    '            sb.Append(" GROUP BY OTStartDate) A")
    '            sb.Append(" ) OTDH) TotalHr")
    '            dtTotal = db.ExecuteDataSet(sb.BuildCommand()).Tables(0)
    '            dbTotal = Convert.ToDouble(dtTotal.Rows(0)("TotalTime").ToString())
    '            dateTotal = 0
    '            dbHoTotal_M = 0
    '            dbHoTotal_SUM = 0
    '        End If

    '        Dim blstart As Boolean = CheckHolidayOrNot(dt.Rows(j)("OTStartDate").ToString())
    '        If blstart Then
    '            dbHoTotal = GetNTotal(dt.Rows(j)("OTCompID").ToString(), dt.Rows(j)("OTEmpID").ToString(), dt.Rows(j)("OTStartDate").ToString(), "OverTimeDeclaration", "")
    '            '假日時數
    '            If dbHoTotal > 0 AndAlso dbHoTotal <= 4 Then
    '                '多扣假日時數
    '                dbHoTotal_M = 4
    '            ElseIf dbHoTotal > 4 AndAlso dbHoTotal <= 8 Then
    '                dbHoTotal_M = 8
    '            ElseIf dbHoTotal > 8 AndAlso dbHoTotal <= 12 Then
    '                dbHoTotal_M = 12
    '            End If
    '            dbHoTotal_SUM += dbHoTotal_M
    '            datecheck = Convert.ToDouble(dt.Rows(j)("TotalTime").ToString())
    '            '正要送簽
    '            If dbHoTotal + datecheck > 0 AndAlso dbHoTotal + datecheck <= 4 Then
    '                datecheck = 4
    '            ElseIf dbHoTotal + datecheck > 4 AndAlso dbHoTotal + datecheck <= 8 Then
    '                datecheck = 8
    '            ElseIf dbHoTotal + datecheck > 8 AndAlso dbHoTotal + datecheck <= 12 Then
    '                datecheck = 12
    '            End If
    '            dateTotal += datecheck
    '        Else
    '            datecheck = Convert.ToDouble(dt.Rows(j)("TotalTime").ToString())
    '            dateTotal += datecheck
    '        End If
    '        If dt.Rows.Count - 1 = j Then
    '            '最後一筆的判斷
    '            If dateTotal + dbTotal - dbHoTotal_SUM > MonthLimitHour Then
    '                msg = "False" + ";" + dt.Rows(j)("OTEmpID").ToString() + ";" + dt.Rows(j)("OTStartDate").ToString()
    '                Return msg
    '            End If
    '        End If
    '    Next
    '    Return "True" + ";" + ""
    'End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <param name="strOTTxnID"></param>
    ''' <param name="strOTFromAdvanceTxnId"></param>
    ''' <param name="Table"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GroupTable(dt As DataTable, strOTTxnID As String, strOTFromAdvanceTxnId As String, Table As String) As DataTable
        'Dim db As New DbHelper(_AattendantDBName)
        'Dim sb As CommandHelper = db.CreateCommandHelper()
        Dim sb As New StringBuilder
        Dim a As String = Bsp.Utility.Quote((strOTTxnID.Replace(";", "','")))
        Dim b As String = a.Substring(0, a.Length - 3)
        sb.Append("SELECT OTCompID,OTEmpID,OTStartDate,Convert(Decimal(10,1),ROUND(Convert(Decimal(10,2),(ISNULL(SUM(ISNULL(OTTotalTime,0))-SUM(ISNULL(MealTime,0)),0)))/60,1)) AS TotalTime  FROM " + Table + " WHERE")
        sb.Append(" OTTxnID in(" + b + ") AND OTStatus='1' AND HolidayOrNot='0' GROUP BY OTStartDate,OTEmpID,OTCompID")
        sb.Append(" UNION ALL")
        sb.Append(" SELECT OTCompID,OTEmpID,OTStartDate,ISNULL(SUM(TotalTime),0) AS TotalTime  FROM (")
        sb.Append(" SELECT OTStartDate,OTTxnID,OTEmpID,OTCompID,Convert(Decimal(10,1),ROUND(Convert(Decimal(10,2),(ISNULL(ISNULL(OTTotalTime,0)-ISNULL(MealTime,0),0)))/60,1)) AS TotalTime FROM " + Table + " WHERE ")
        sb.Append(" OTTxnID in(" + b + ") AND OTStatus='1' AND HolidayOrNot='1' AND OTStartDate NOT IN (SELECT Code FROM AT_CodeMap WHERE TabName = 'NationalHolidayDefine' AND FldName='HolidayDate' AND NotShowFlag='0')) A GROUP BY OTCompID,OTEmpID,OTStartDate")
        dt = Bsp.DB.ExecuteDataSet(CommandType.Text, sb.ToString, "AattendantDB").Tables(0)
        Return dt
    End Function

    ''' <summary>
    ''' 檢查多筆加班人是否超過每天的加班時數上限
    ''' </summary>
    ''' <param name="dt">dataTable</param>
    ''' <param name="dayNLimit">平日加班上限</param>
    ''' <param name="dayHLimit">假日加班上限</param>
    ''' <param name="Flag">A:事先申請,D:事後申報</param>
    ''' <returns>結果字串</returns>
    ''' <remarks></remarks>
    Public Function GetCheckOverTimeIsOver(dt As DataTable, dayNLimit As Double, dayHLimit As Double, Table As String) As String
        'Dim db As New DbHelper(_AattendantDBName)
        'Dim sb As CommandHelper = db.CreateCommandHelper()
        'Dim sb1 As CommandHelper = db.CreateCommandHelper()
        Dim sb As New StringBuilder
        Dim msg As String = ""
        Dim strOTTxnID As String = ""
        Dim strOTFromAdvanceTxnId As String = ""
        Dim strOTFromAdvanceTxnId_1 As String = ""

        For i As Integer = 0 To dt.Rows.Count - 1
            strOTTxnID += dt.Rows(i)("OTTxnID").ToString() + ";"
            If Table = "OverTimeDeclaration" Then
                strOTFromAdvanceTxnId += dt.Rows(i)("OTFromAdvanceTxnId").ToString() + ";"
            End If
        Next

        If Table = "OverTimeDeclaration" Then
            strOTFromAdvanceTxnId = "'" + (strOTFromAdvanceTxnId.Replace(";", "','")) + "'"
            strOTFromAdvanceTxnId_1 = strOTFromAdvanceTxnId.Substring(0, strOTFromAdvanceTxnId.Length - 3)
        End If
        strOTTxnID = "'" + (strOTTxnID.Replace(";", "','")) + "'"
        Dim strOTTxnID_1 As String = strOTTxnID.Substring(0, strOTTxnID.Length - 3)

        For j As Integer = 0 To dt.Rows.Count - 1
            sb.Clear()
            sb.Append("SELECT A.OTStartDate,SUM(OTTotalTime)-SUM(MealTime) AS TotalTime FROM (")
            sb.Append(" SELECT OTStartDate,OTTotalTime,MealTime,OTTxnID FROM OverTimeAdvance WHERE OTCompID=" + Bsp.Utility.Quote(dt.Rows(j)("OTCompID")) + " AND OTEmpID=" + Bsp.Utility.Quote(dt.Rows(j)("EmpID")) + " AND OTStatus IN ('2','3') ")

            If dt.Rows(j)("OTStartDate").ToString() = dt.Rows(j)("OTEndDate").ToString() Then
                sb.Append(" AND OTStartDate=" + Bsp.Utility.Quote(dt.Rows(j)("OTStartDate").ToString()))
            Else
                sb.Append(" AND (OTStartDate=" + Bsp.Utility.Quote(dt.Rows(j)("OTStartDate").ToString()) + " OR OTStartDate=" + Bsp.Utility.Quote(dt.Rows(j)("OTEndDate").ToString()) + ")")
            End If

            sb.Append(" AND OTTxnID NOT IN")
            sb.Append(" (SELECT OTFromAdvanceTxnId FROM OverTimeDeclaration WHERE OTCompID=" + Bsp.Utility.Quote(dt.Rows(j)("OTCompID")) + " AND OTEmpID=" + Bsp.Utility.Quote(dt.Rows(j)("EmpID")) + " AND OTStatus in ('2','3') AND OTFromAdvanceTxnId<>'')")

            If Table = "OverTimeDeclaration" Then
                sb.Append(" AND OTTxnID NOT IN(" + strOTFromAdvanceTxnId_1 + ")")
            End If

            sb.Append(" UNION ALL")
            sb.Append(" SELECT OTStartDate,OTTotalTime,MealTime,OTTxnID FROM OverTimeDeclaration WHERE OTCompID=" + Bsp.Utility.Quote(dt.Rows(j)("OTCompID")) + " AND OTEmpID=" + Bsp.Utility.Quote(dt.Rows(j)("EmpID")) + " AND OTStatus IN ('2','3') ")

            If dt.Rows(j)("OTStartDate").ToString() = dt.Rows(j)("OTEndDate").ToString() Then
                sb.Append(" AND OTStartDate=" + Bsp.Utility.Quote(dt.Rows(j)("OTStartDate").ToString()))
            Else
                sb.Append(" AND (OTStartDate=" + Bsp.Utility.Quote(dt.Rows(j)("OTStartDate").ToString()) + " OR OTStartDate=" + Bsp.Utility.Quote(dt.Rows(j)("OTEndDate").ToString()) + ")")
            End If

            sb.Append(" UNION ALL")
            sb.Append(" SELECT OTStartDate,OTTotalTime,MealTime,OTTxnID FROM " + Table + " WHERE OTTxnID in(" + strOTTxnID_1 + ") AND OTCompID=" + Bsp.Utility.Quote(dt.Rows(j)("OTCompID")) + " AND OTEmpID=" + Bsp.Utility.Quote(dt.Rows(j)("EmpID")) + ") A")

            If dt.Rows(j)("OTStartDate").ToString() = dt.Rows(j)("OTEndDate").ToString() Then
                sb.Append(" WHERE A.OTStartDate=" + Bsp.Utility.Quote(dt.Rows(j)("OTStartDate").ToString()))
            Else
                sb.Append(" WHERE A.OTStartDate=" + Bsp.Utility.Quote(dt.Rows(j)("OTStartDate").ToString()))
                sb.Append(" OR A.OTStartDate=" + Bsp.Utility.Quote(dt.Rows(j)("OTEndDate").ToString()))
            End If

            sb.Append(" GROUP BY A.OTStartDate")

            Dim dt1 As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, sb.ToString, "AattendantDB").Tables(0)
            For i As Integer = 0 To dt1.Rows.Count - 1
                Dim blstart As Boolean = CheckHolidayOrNot(dt1.Rows(i)("OTStartDate").ToString())
                If blstart Then     '假日
                    If Convert.ToDouble(dt1.Rows(i)("TotalTime")) > (dayHLimit * 60) Then
                        msg = "False" + ";" + dt.Rows(j)("EmpID").ToString() + ";" + dt1.Rows(i)("OTStartDate").ToString() + ";" + Convert.ToString(dayHLimit)
                        Return msg
                    End If
                Else
                    If Convert.ToDouble(dt1.Rows(i)("TotalTime")) > (dayNLimit * 60) Then
                        msg = "False" + ";" + dt.Rows(j)("EmpID").ToString() + ";" + dt1.Rows(i)("OTStartDate").ToString() + ";" + Convert.ToString(dayNLimit)
                        Return msg
                    End If
                End If
            Next
        Next
        Return "True" + ";" + ""
    End Function

    ''' <summary>
    ''' 連續加班天數檢核
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <param name="strOTLimitDay"></param>
    ''' <param name="Table"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCheckOTLimitDay(dt As DataTable, strOTLimitDay As String, Table As String) As String
        'Dim db As New DbHelper(_AattendantDBName)
        'Dim sb As CommandHelper = db.CreateCommandHelper()
        Dim sb As New StringBuilder
        dt.DefaultView.Sort = "EmpID ASC"
        Dim OTLimitDay As Integer = Convert.ToInt32(strOTLimitDay)
        Dim strOTTxnID As String = ""
        Dim msg As String = ""
        Dim strEmp As String = ""
        Dim strOTFromAdvanceTxnId As String = ""
        Dim strOTFromAdvanceTxnId_1 As String = ""
        For i As Integer = 0 To (dt.Rows.Count - 1)
            strOTTxnID += dt.Rows(i)("OTTxnID").ToString() + ";"
            If Table = "OverTimeDeclaration" Then
                strOTFromAdvanceTxnId += dt.Rows(i)("OTFromAdvanceTxnId").ToString() + ";"
            End If
        Next
        If Table = "OverTimeDeclaration" Then
            strOTFromAdvanceTxnId = "'" + (strOTFromAdvanceTxnId.Replace(";", "','")) + "'"
            strOTFromAdvanceTxnId_1 = strOTFromAdvanceTxnId.Substring(0, strOTFromAdvanceTxnId.Length - 3)
        End If
        strOTTxnID = "'" + (strOTTxnID.Replace(";", "','")) + "'"
        Dim strOTTxnID_1 As String = strOTTxnID.Substring(0, strOTTxnID.Length - 3)
        For i As Integer = 0 To (dt.Rows.Count - 1)
            Dim cnt As Integer = 0
            strEmp = dt.Rows(i)("EmpID").ToString()
            sb.Clear()
            sb.Append("SELECT C.SysDate,ISNULL(O.OTStartDate,'') AS OTStartDate,C.Week,C.HolidayOrNot FROM (")
            sb.Append(" SELECT DISTINCT OTStartDate FROM " + Table + " WHERE OTTxnID in(" + strOTTxnID_1 + ") AND OTEmpID=" + Bsp.Utility.Quote(dt.Rows(i)("EmpID")) + " AND OTStartDate>= DATEADD(DAY,-" + (OTLimitDay - 1).ToString() + "," + Bsp.Utility.Quote(dt.Rows(i)("OTStartDate")) + ") AND  OTStartDate<= DATEADD(DAY," + (OTLimitDay - 1).ToString() + "," + Bsp.Utility.Quote(dt.Rows(i)("OTStartDate")) + ") ")
            sb.Append(" UNION")
            sb.Append(" SELECT DISTINCT OTStartDate FROM OverTimeAdvance WHERE  OTCompID=" + Bsp.Utility.Quote(dt.Rows(i)("OTCompID")) + " AND OTEmpID=" + Bsp.Utility.Quote(dt.Rows(i)("EmpID")) + " AND OTStatus in ('2','3') AND OTStartDate>= DATEADD(DAY,-" + (OTLimitDay - 1).ToString() + "," + Bsp.Utility.Quote(dt.Rows(i)("OTStartDate")) + ") AND  OTStartDate<= DATEADD(DAY," + (OTLimitDay - 1).ToString() + "," + Bsp.Utility.Quote(dt.Rows(i)("OTStartDate")) + ")")
            If Table = "OverTimeDeclaration" Then
                sb.Append(" AND OTTxnID NOT IN(" + strOTFromAdvanceTxnId_1 + ")")
            End If
            sb.Append(" UNION")
            sb.Append(" SELECT DISTINCT OTStartDate FROM OverTimeDeclaration WHERE  OTCompID=" + Bsp.Utility.Quote(dt.Rows(i)("OTCompID")) + " AND OTEmpID=" + Bsp.Utility.Quote(dt.Rows(i)("EmpID")) + " AND OTStatus in ('2','3') AND OTStartDate>= DATEADD(DAY,-" + (OTLimitDay - 1).ToString() + "," + Bsp.Utility.Quote(dt.Rows(i)("OTStartDate")) + ") AND  OTStartDate<= DATEADD(DAY," + (OTLimitDay - 1).ToString() + "," + Bsp.Utility.Quote(dt.Rows(i)("OTStartDate")) + ")) O")
            sb.Append(" FULL OUTER JOIN(")
            sb.Append(" SELECT * FROM " + _eHRMSDB_ITRD + ".[dbo].[Calendar] WHERE  CompID=" + Bsp.Utility.Quote(dt.Rows(i)("OTCompID")) + " AND SysDate>= DATEADD(DAY,-" + (OTLimitDay - 1).ToString() + "," + Bsp.Utility.Quote(dt.Rows(i)("OTStartDate")) + ") AND  SysDate<= DATEADD(DAY," + (OTLimitDay - 1).ToString() + "," + Bsp.Utility.Quote(dt.Rows(i)("OTStartDate")) + ")) C ON O.OTStartDate=C.SysDate")
            sb.Append(" ORDER BY C.SysDate ASC")
            Dim dt1 As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, sb.ToString, "AattendantDB").Tables(0)
            For j As Integer = 0 To dt1.Rows.Count - 1
                If dt1.Rows(j)("HolidayOrNot").ToString() = "0" Then
                    cnt += 1
                Else
                    If Not String.IsNullOrEmpty(dt1.Rows(j)("OTStartDate").ToString()) Then
                        cnt += 1
                    Else
                        cnt = 0
                    End If
                End If
                If cnt >= OTLimitDay Then
                    msg = "False" + ";" + strEmp
                    Return msg
                End If
            Next
        Next
        Return "True" + ";" + ""
    End Function


    'Public Function GetCheckOTLimitDay(dt As DataTable, strOTLimitDay As String, Table As String) As String
    '    Dim db As New DbHelper(_AattendantDBName)
    '    Dim sb As CommandHelper = db.CreateCommandHelper()
    '    dt.DefaultView.Sort = "EmpID ASC"
    '    Dim OTLimitDay As Integer = Convert.ToInt32(strOTLimitDay)
    '    Dim strOTTxnID As String = ""
    '    Dim msg As String = ""
    '    Dim strEmp As String = ""
    '    Dim strOTFromAdvanceTxnId As String = ""
    '    Dim strOTFromAdvanceTxnId_1 As String = ""
    '    For i As Integer = 0 To dt.Rows.Count - 1
    '        strOTTxnID += dt.Rows(i)("OTTxnID").ToString() + ";"
    '        If Table = "OverTimeDeclaration" Then
    '            strOTFromAdvanceTxnId += dt.Rows(i)("OTFromAdvanceTxnId").ToString() + ";"
    '        End If
    '    Next
    '    If Table = "OverTimeDeclaration" Then
    '        strOTFromAdvanceTxnId = "'" + (strOTFromAdvanceTxnId.Replace(";", "','")) + "'"
    '        strOTFromAdvanceTxnId_1 = strOTFromAdvanceTxnId.Substring(0, strOTFromAdvanceTxnId.Length - 3)
    '    End If
    '    strOTTxnID = "'" + (strOTTxnID.Replace(";", "','")) + "'"
    '    Dim strOTTxnID_1 As String = strOTTxnID.Substring(0, strOTTxnID.Length - 3)
    '    For i As Integer = 0 To dt.Rows.Count - 1
    '        Dim cnt As Integer = 0      '累計連續幾天上班
    '        If strEmp <> dt.Rows(i)("EmpID").ToString() Then
    '            '不同員編
    '            strEmp = dt.Rows(i)("EmpID").ToString()
    '            sb.Reset()
    '            sb.Append("SELECT C.SysDate,ISNULL(O.OTStartDate,'') AS OTStartDate,C.Week,C.HolidayOrNot FROM (")
    '            sb.Append(" SELECT DISTINCT OTStartDate FROM " + Table + " WHERE OTTxnID in(" + strOTTxnID_1 + ") AND OTEmpID='" + dt.Rows(i)("EmpID") + "'")
    '            sb.Append(" UNION")
    '            sb.Append(" SELECT DISTINCT OTStartDate FROM OverTimeAdvance WHERE  OTCompID='" + dt.Rows(i)("OTCompID") + "' AND OTEmpID='" + dt.Rows(i)("EmpID") + "' AND OTStatus in ('2','3') AND OTStartDate>= DATEADD(DAY,-" + (OTLimitDay - 1).ToString() + ",'" + dt.Rows(i)("OTStartDate") + "') AND  OTStartDate<= DATEADD(DAY," + (OTLimitDay - 1).ToString() + ",'" + dt.Rows(i)("OTStartDate") + "')")
    '            If Table = "OverTimeDeclaration" Then
    '                sb.Append(" AND OTTxnID NOT IN(" + strOTFromAdvanceTxnId_1 + ")")
    '            End If
    '            sb.Append(" UNION")
    '            sb.Append(" SELECT DISTINCT OTStartDate FROM OverTimeDeclaration WHERE  OTCompID='" + dt.Rows(i)("OTCompID") + "' AND OTEmpID='" + dt.Rows(i)("EmpID") + "' AND OTStatus in ('2','3') AND OTStartDate>= DATEADD(DAY,-" + (OTLimitDay - 1).ToString() + ",'" + dt.Rows(i)("OTStartDate") + "') AND  OTStartDate<= DATEADD(DAY," + (OTLimitDay - 1).ToString() + ",'" + dt.Rows(i)("OTStartDate") + "')) O")
    '            sb.Append(" FULL OUTER JOIN(")
    '            sb.Append(" SELECT * FROM " + _eHRMSDB_ITRD + ".[dbo].[Calendar] WHERE  CompID='SPHBK1' AND SysDate>= DATEADD(DAY,-" + (OTLimitDay - 1).ToString() + ",'" + dt.Rows(i)("OTStartDate") + "') AND  SysDate<= DATEADD(DAY," + (OTLimitDay - 1).ToString() + ",'" + dt.Rows(i)("OTStartDate") + "')) C ON O.OTStartDate=C.SysDate")
    '            Dim dt1 As DataTable = db.ExecuteDataSet(sb.BuildCommand()).Tables(0)
    '            For j As Integer = 0 To dt1.Rows.Count - 1
    '                If dt1.Rows(j)("HolidayOrNot").ToString() = "0" Then
    '                    cnt += 1
    '                Else
    '                    If Not String.IsNullOrEmpty(dt1.Rows(j)("OTStartDate").ToString()) Then
    '                        cnt += 1
    '                    Else
    '                        cnt = 0
    '                    End If
    '                End If
    '                If cnt >= OTLimitDay Then
    '                    msg = Convert.ToString("False" + ";") & strEmp
    '                    Return msg
    '                End If
    '            Next
    '        End If
    '    Next
    '    Return "True" + ";" + ""
    'End Function

#End Region

#Region "時段相關"

    ''' <summary>
    ''' 檢查日期為星期幾
    ''' </summary>
    ''' <param name="strDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckWeekDay(ByVal strDate As String) As String
        Dim strSQL As New StringBuilder
        strSQL.Append(" SELECT [Week] FROM Calendar ")
        strSQL.Append(" WHERE 1=1 AND CompID = 'SPHBK1' ")
        strSQL.Append(" AND CONVERT(CHAR(10), SysDate, 111) = " + Bsp.Utility.Quote(strDate))

        Try
            Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "eHRMSDB").Tables(0)
                If dt.Rows.Count > 0 AndAlso dt.Rows(0).Item("Week").ToString() IsNot Nothing Then Return dt.Rows(0).Item("Week").ToString()
            End Using
            Return ""
        Catch ex As Exception
            Debug.Print("CheckWeekDay()==>" + ex.Message)
            Return ""
        End Try
    End Function


    ''' <summary>
    ''' 檢查日期是否為假日
    ''' </summary>
    ''' <param name="strDate"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks>從假日檔查詢日期是否為假日</remarks>
    Public Function CheckHolidayOrNot(ByVal strDate As String) As Boolean
        Dim strSQL As New StringBuilder()
        strSQL.Append(" SELECT HolidayOrNot FROM Calendar ")
        strSQL.Append(" Where 1=1 AND CompID = " & Bsp.Utility.Quote(UserProfile.SelectCompRoleID.Trim))
        strSQL.Append(" AND CONVERT(CHAR(10),SysDate, 111) = " & Bsp.Utility.Quote(strDate))
        Try
            Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "eHRMSDB").Tables(0)
                If dt.Rows.Count > 0 And dt.Rows(0).Item("HolidayOrNot").ToString() IsNot Nothing Then If dt.Rows(0).Item("HolidayOrNot").ToString() = "1" Then Return True
            End Using
            Return False
        Catch ex As Exception
            Debug.Print("CheckHolidayOrNot()==>" + ex.Message)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 檢核是否有一般假日連續加班
    ''' </summary>
    ''' <param name="CompID">加班人公司</param>
    ''' <param name="EmpID">加班人員工編號</param>
    ''' <param name="OTStartDate">加班起日</param>
    ''' <param name="OTEndDate">加班迄日</param>
    ''' <param name="flag">新增頁呼叫="Add",修改頁呼叫="Upd"</param>
    ''' <param name="Msg">檢核不通過回傳的訊息</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function CheckNHolidayOTOrNot(ByVal CompID As String, ByVal EmpID As String, ByVal OTStartDate As String, ByVal OTEndDate As String, ByVal OTTxnID As String, ByVal flag As String, ByRef Msg As String) As Boolean
        Dim strSQL As New StringBuilder
        Dim ErrMsg As String = ""
        Try
            If OTStartDate = OTEndDate Then '無跨日
                '檢查本單前後一天是否在事後申報與事先申請有一般假日之加班單
                strSQL.Append(" SELECT OTStartDate, 'A' AS OTMode FROM OverTimeAdvance WHERE 1=1 AND OTCompID = " + Bsp.Utility.Quote(CompID) + " AND OTEmpID = " + Bsp.Utility.Quote(EmpID))
                strSQL.Append(" AND OTStatus IN ('2','3') ")
                strSQL.Append(" AND (OTStartDate = CONVERT(CHAR(10), DATEADD(DAY, -1, " + Bsp.Utility.Quote(OTStartDate) + "), 111) OR OTStartDate = CONVERT(CHAR(10), DATEADD(DAY, 1, " + Bsp.Utility.Quote(OTStartDate) + "), 111)) ")
                If flag = "Upd" Then
                    strSQL.Append(" AND OTTxnID NOT IN (" + Bsp.Utility.Quote(OTTxnID) + ") ")
                End If
                strSQL.Append(" AND OTStartDate IN ( ")
                strSQL.Append(" SELECT SysDate FROM " + _eHRMSDB_ITRD + ".[dbo].[Calendar] WHERE HolidayOrNot = '1' AND CompID = " + Bsp.Utility.Quote(CompID) + " AND SysDate NOT IN( ")
                strSQL.Append(" SELECT Code FROM AT_CodeMap WHERE NotShowFlag = '0' AND TabName = 'NationalHolidayDefine' AND FldName ='HolidayDate' AND Code NOT IN ( ")
                strSQL.Append(" SELECT SysDate FROM " + _eHRMSDB_ITRD + ".[dbo].[Calendar] WHERE CompID = " + Bsp.Utility.Quote(CompID) + " AND [Week] in ('6','7')))) ")
                strSQL.Append(" UNION ALL ")
                strSQL.Append(" SELECT OTStartDate, 'D' AS OTMode FROM OverTimeDeclaration WHERE 1=1 AND OTCompID = " + Bsp.Utility.Quote(CompID) + " AND OTEmpID = " + Bsp.Utility.Quote(EmpID))
                strSQL.Append(" AND OTStatus IN ('2','3') ")
                strSQL.Append(" AND (OTStartDate = CONVERT(CHAR(10), DATEADD(DAY, -1, " + Bsp.Utility.Quote(OTStartDate) + "), 111) OR OTStartDate = CONVERT(CHAR(10), DATEADD(DAY, 1, " + Bsp.Utility.Quote(OTStartDate) + "), 111)) ")
                If flag = "Upd" Then
                    strSQL.Append(" AND OTTxnID NOT IN (" + Bsp.Utility.Quote(OTTxnID) + ") ")
                End If
                strSQL.Append(" AND OTStartDate IN ( ")
                strSQL.Append(" SELECT SysDate FROM " + _eHRMSDB_ITRD + ".[dbo].[Calendar] WHERE HolidayOrNot = '1' AND CompID = " + Bsp.Utility.Quote(CompID) + " AND SysDate NOT IN( ")
                strSQL.Append(" SELECT Code FROM AT_CodeMap WHERE NotShowFlag = '0' AND TabName = 'NationalHolidayDefine' AND FldName ='HolidayDate' AND Code NOT IN ( ")
                strSQL.Append(" SELECT SysDate FROM " + _eHRMSDB_ITRD + ".[dbo].[Calendar] WHERE CompID = " + Bsp.Utility.Quote(CompID) + " AND [Week] in ('6','7')))) ")
                Using SameDatedt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "AattendantDB").Tables(0)
                    If SameDatedt.Rows.Count > 0 Then
                        If CheckHolidayOrNot(OTStartDate) Then  '如果加班日是假日
                            If Weekday(Convert.ToDateTime(OTStartDate)) = 0 OrElse Weekday(Convert.ToDateTime(OTStartDate)) = 7 Then    '如果加班日是周末
                                ErrMsg += (OTStartDate + "不能假日連續加班" & vbNewLine)
                                Msg = ErrMsg
                                Return False
                            Else    '如果加班日是非周末之一般假日
                                If Not OVBusinessCommon.IsNationalHoliday(OTStartDate) Then
                                    ErrMsg += (OTStartDate + "不能假日連續加班" & vbNewLine)
                                    Msg = ErrMsg
                                    Return False
                                End If
                            End If
                        Else
                            Return True
                        End If
                    Else
                        Return True
                    End If
                End Using
            Else '有跨日
                '先檢查加班起日是否前後一天是否在事後申報與事先申請有一般假日之加班單
                strSQL.Append(" SELECT OTStartDate, 'A' AS OTMode FROM OverTimeAdvance WHERE 1=1 AND OTCompID = " + Bsp.Utility.Quote(CompID) + " AND OTEmpID = " + Bsp.Utility.Quote(EmpID))
                strSQL.Append(" AND OTStatus IN ('2','3') ")
                strSQL.Append(" AND (OTStartDate = CONVERT(CHAR(10), DATEADD(DAY, -1, " + Bsp.Utility.Quote(OTStartDate) + "), 111) OR OTStartDate = CONVERT(CHAR(10), DATEADD(DAY, 1, " + Bsp.Utility.Quote(OTStartDate) + "), 111)) ")
                If flag = "Upd" Then
                    strSQL.Append(" AND OTTxnID NOT IN (" + Bsp.Utility.Quote(OTTxnID) + ") ")
                End If
                strSQL.Append(" AND OTStartDate IN ( ")
                strSQL.Append(" SELECT SysDate FROM " + _eHRMSDB_ITRD + ".[dbo].[Calendar] WHERE HolidayOrNot = '1' AND CompID = " + Bsp.Utility.Quote(CompID) + " AND SysDate NOT IN( ")
                strSQL.Append(" SELECT Code FROM AT_CodeMap WHERE NotShowFlag = '0' AND TabName = 'NationalHolidayDefine' AND FldName ='HolidayDate' AND Code NOT IN ( ")
                strSQL.Append(" SELECT SysDate FROM " + _eHRMSDB_ITRD + ".[dbo].[Calendar] WHERE CompID = " + Bsp.Utility.Quote(CompID) + " AND [Week] in ('6','7')))) ")
                strSQL.Append(" UNION ALL ")
                strSQL.Append(" SELECT OTStartDate, 'D' AS OTMode FROM OverTimeDeclaration WHERE 1=1 AND OTCompID = " + Bsp.Utility.Quote(CompID) + " AND OTEmpID = " + Bsp.Utility.Quote(EmpID))
                strSQL.Append(" AND OTStatus IN ('2','3') ")
                strSQL.Append(" AND (OTStartDate = CONVERT(CHAR(10), DATEADD(DAY, -1, " + Bsp.Utility.Quote(OTStartDate) + "), 111) OR OTStartDate = CONVERT(CHAR(10), DATEADD(DAY, 1, " + Bsp.Utility.Quote(OTStartDate) + "), 111)) ")
                If flag = "Upd" Then
                    strSQL.Append(" AND OTTxnID NOT IN (" + Bsp.Utility.Quote(OTTxnID) + ") ")
                End If
                strSQL.Append(" AND OTStartDate IN ( ")
                strSQL.Append(" SELECT SysDate FROM " + _eHRMSDB_ITRD + ".[dbo].[Calendar] WHERE HolidayOrNot = '1' AND CompID = " + Bsp.Utility.Quote(CompID) + " AND SysDate NOT IN( ")
                strSQL.Append(" SELECT Code FROM AT_CodeMap WHERE NotShowFlag = '0' AND TabName = 'NationalHolidayDefine' AND FldName ='HolidayDate' AND Code NOT IN ( ")
                strSQL.Append(" SELECT SysDate FROM " + _eHRMSDB_ITRD + ".[dbo].[Calendar] WHERE CompID = " + Bsp.Utility.Quote(CompID) + " AND [Week] in ('6','7')))) ")
                Using startDatedt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "AattendantDB").Tables(0)
                    If startDatedt.Rows.Count > 0 Then
                        If CheckHolidayOrNot(OTStartDate) Then  '如果加班起日是假日
                            If Weekday(Convert.ToDateTime(OTStartDate)) = 0 OrElse Weekday(Convert.ToDateTime(OTStartDate)) = 7 Then    '如果加班起日是周末
                                ErrMsg += (OTStartDate + "不能假日連續加班" & vbNewLine)
                                Msg = ErrMsg
                            Else    '如果加班起日是非周末之一般假日
                                If Not OVBusinessCommon.IsNationalHoliday(OTStartDate) Then
                                    ErrMsg += (OTStartDate + "不能假日連續加班" & vbNewLine)
                                    Msg = ErrMsg
                                End If
                            End If
                        End If
                    End If
                End Using

                '再檢查加班迄日是否前後一天是否在事後申報與事先申請有一般假日之加班單
                strSQL.Append(" SELECT OTEndDate, 'A' AS OTMode FROM OverTimeAdvance WHERE 1=1 AND OTCompID = " + Bsp.Utility.Quote(CompID) + " AND OTEmpID = " + Bsp.Utility.Quote(EmpID))
                strSQL.Append(" AND (OTEndDate = CONVERT(CHAR(10), DATEADD(DAY, -1, " + Bsp.Utility.Quote(OTEndDate) + "), 111) OR OTEndDate = CONVERT(CHAR(10), DATEADD(DAY, 1, " + Bsp.Utility.Quote(OTEndDate) + "), 111)) ")
                If flag = "Upd" Then
                    strSQL.Append(" AND OTTxnID NOT IN (" + Bsp.Utility.Quote(OTTxnID) + ") ")
                End If
                strSQL.Append(" AND OTStartDate IN ( ")
                strSQL.Append(" SELECT SysDate FROM " + _eHRMSDB_ITRD + ".[dbo].[Calendar] WHERE HolidayOrNot = '1' AND CompID = " + Bsp.Utility.Quote(CompID) + " AND SysDate NOT IN( ")
                strSQL.Append(" SELECT Code FROM AT_CodeMap WHERE NotShowFlag = '0' AND TabName = 'NationalHolidayDefine' AND FldName ='HolidayDate' AND Code NOT IN ( ")
                strSQL.Append(" SELECT SysDate FROM " + _eHRMSDB_ITRD + ".[dbo].[Calendar] WHERE CompID = " + Bsp.Utility.Quote(CompID) + " AND [Week] in ('6','7')))) ")
                strSQL.Append(" UNION ALL ")
                strSQL.Append(" SELECT OTEndDate, 'D' AS OTMode FROM OverTimeDeclaration WHERE 1=1 AND OTCompID = " + Bsp.Utility.Quote(CompID) + " AND OTEmpID = " + Bsp.Utility.Quote(EmpID))
                strSQL.Append(" AND (OTEndDate = CONVERT(CHAR(10), DATEADD(DAY, -1, " + Bsp.Utility.Quote(OTEndDate) + "), 111) OR OTEndDate = CONVERT(CHAR(10), DATEADD(DAY, 1, " + Bsp.Utility.Quote(OTEndDate) + "), 111)) ")
                If flag = "Upd" Then
                    strSQL.Append(" AND OTTxnID NOT IN (" + Bsp.Utility.Quote(OTTxnID) + ") ")
                End If
                strSQL.Append(" AND OTStartDate IN ( ")
                strSQL.Append(" SELECT SysDate FROM " + _eHRMSDB_ITRD + ".[dbo].[Calendar] WHERE HolidayOrNot = '1' AND CompID = " + Bsp.Utility.Quote(CompID) + " AND SysDate NOT IN( ")
                strSQL.Append(" SELECT Code FROM AT_CodeMap WHERE NotShowFlag = '0' AND TabName = 'NationalHolidayDefine' AND FldName ='HolidayDate' AND Code NOT IN ( ")
                strSQL.Append(" SELECT SysDate FROM " + _eHRMSDB_ITRD + ".[dbo].[Calendar] WHERE CompID = " + Bsp.Utility.Quote(CompID) + " AND [Week] in ('6','7')))) ")
                Using endDatedt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "AattendantDB").Tables(0)
                    If endDatedt.Rows.Count > 0 Then
                        If CheckHolidayOrNot(OTEndDate) Then  '如果加班迄日是假日
                            If Weekday(Convert.ToDateTime(OTEndDate)) = 0 OrElse Weekday(Convert.ToDateTime(OTEndDate)) = 7 Then    '如果加班迄日是周末
                                ErrMsg += (OTEndDate + "不能假日連續加班" & vbNewLine)
                                Msg = ErrMsg
                            Else    '如果加班迄日是非周末之一般假日
                                If Not OVBusinessCommon.IsNationalHoliday(OTEndDate) Then
                                    ErrMsg += (OTEndDate + "不能假日連續加班" & vbNewLine)
                                    Msg = ErrMsg
                                End If
                            End If
                        End If
                    End If
                End Using

                If ErrMsg <> "" Then
                    Msg = ErrMsg
                    Return False
                Else
                    Return True
                End If
            End If
        Catch ex As Exception
            Debug.Print("CheckWeekendOTOrNot()==>" & ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' 檢查是否超過每個月可以加的時數(狀態為送簽、核准)
    ''' </summary>
    ''' <param name="Table">事先還是事後申報Table</param>
    ''' <param name="strComp">加班人公司</param>
    ''' <param name="strEmp">加班人員編</param>
    ''' <param name="dateStart">加班開始日期</param>
    ''' <param name="dateEnd">加班結束日期</param>
    ''' <param name="MonthLimitHour">每月加班上限時數</param>
    ''' <param name="totalTime">本次的加班時數</param>
    ''' <param name="mealtime">用餐時間</param>
    ''' <param name="cntStart">加班起日TotalTime</param>
    ''' <param name="cntEnd">迄日TotalTime</param>
    ''' <param name="strOTFromAdvanceTxnId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function checkMonthTime(Table As String, strComp As String, strEmp As String, dateStart As String, dateEnd As String, MonthLimitHour As Double, _
     totalTime As Double, mealtime As Double, cntStart As Double, cntEnd As Double, strOTFromAdvanceTxnId As String) As Boolean
        'Dim db As New DbHelper(_AattendantDBName)
        'Dim sb As CommandHelper = db.CreateCommandHelper()
        Dim sb As New StringBuilder
        Dim blstart As Boolean = CheckHolidayOrNot(dateStart)
        Dim blend As Boolean = CheckHolidayOrNot(dateEnd)
        Dim dt As DataTable = Nothing
        Dim dbTotal As Double = 0
        Dim dbHoTotal As Double = 0
        Dim mealOver As String = ""
        Dim datecheck As Double = 0
        '本月加班時數
        sb.Clear()
        sb.Append("SELECT ISNULL(SUM(ISNULL(E.TotalTime,0)),0) AS TotalTime FROM(")
        '平日計算
        sb.Append(" SELECT ISNULL(SUM(ISNULL(A.TotalTime,0)),0) AS TotalTime FROM(")
        '事前平日計算(如事後有此筆，以事後為主)
        sb.Append(" SELECT ISNULL(SUM(ISNULL(OTTotalTime,0))-SUM(ISNULL(MealTime,0)),0) AS TotalTime  FROM OverTimeAdvance")
        sb.Append(" WHERE OTCompID=" + Bsp.Utility.Quote(strComp) + " AND OTEmpID=" + Bsp.Utility.Quote(strEmp) + " AND OTStatus IN ('2','3') AND YEAR(OTStartDate)=YEAR(" + Bsp.Utility.Quote(dateStart) + ") AND MONTH(OTStartDate)=MONTH(" + Bsp.Utility.Quote(dateStart) + ") AND HolidayOrNot='0'")
        sb.Append(" AND OTTxnID NOT IN")
        sb.Append(" (SELECT OTFromAdvanceTxnId FROM OverTimeDeclaration WHERE OTCompID=" + Bsp.Utility.Quote(strComp) + " AND OTEmpID=" + Bsp.Utility.Quote(strEmp) + " AND YEAR(OTStartDate)=YEAR(" + Bsp.Utility.Quote(dateStart) + ") AND MONTH(OTStartDate)=MONTH(" + Bsp.Utility.Quote(dateStart) + ") AND HolidayOrNot='0' AND OTStatus in ('2','3') AND OTFromAdvanceTxnId<>'')")
        If Table = "OverTimeDeclaration" Then
            sb.Append(" AND OTTxnID NOT IN('" + strOTFromAdvanceTxnId + "')")
        End If
        sb.Append(" UNION ALL")
        '事後平日計算
        sb.Append(" SELECT SUM(OTTotalTime)-SUM(MealTime) AS TotalTime  FROM OverTimeDeclaration")
        sb.Append(" WHERE OTCompID=" + Bsp.Utility.Quote(strComp) + " AND OTEmpID=" + Bsp.Utility.Quote(strEmp) + " AND OTStatus IN ('2','3') AND YEAR(OTStartDate)=YEAR(" + Bsp.Utility.Quote(dateStart) + ") AND MONTH(OTStartDate)=MONTH(" + Bsp.Utility.Quote(dateStart) + ") AND HolidayOrNot='0'")
        sb.Append(" ) A")
        sb.Append(" UNION ALL")
        '假日計算會有成長時數，以及排除國定假日(如事後有此筆，以事後為主)
        sb.Append(" SELECT ISNULL(SUM(ISNULL(D.TotalTime,0)),0) AS TotalTime FROM (")
        sb.Append(" SELECT CASE WHEN C.TotalTime >0 AND C.TotalTime <= 240 THEN 240")
        sb.Append(" WHEN C.TotalTime > 240 AND C.TotalTime <= 480 THEN 480")
        sb.Append(" WHEN C.TotalTime > 480 AND C.TotalTime <= 720 THEN 720 END AS TotalTime")
        sb.Append(" FROM (")
        sb.Append(" SELECT SUM(ISNULL(B.OTTotalTime,0))-SUM(ISNULL(B.MealTime,0)) AS TotalTime FROM (")
        '事前假日(如事後有此筆，以事後為主)
        sb.Append(" SELECT OTStartDate, ISNULL(OTTotalTime,0) OTTotalTime, ISNULL(MealTime,0) MealTime FROM OverTimeAdvance")
        sb.Append(" WHERE OTCompID=" + Bsp.Utility.Quote(strComp) + " AND OTEmpID=" + Bsp.Utility.Quote(strEmp) + " AND OTStatus IN ('2','3') AND YEAR(OTStartDate)=YEAR(" + Bsp.Utility.Quote(dateStart) + ") AND MONTH(OTStartDate)=MONTH(" + Bsp.Utility.Quote(dateStart) + ") AND HolidayOrNot='1'")
        sb.Append(" AND OTStartDate NOT IN (SELECT Code FROM AT_CodeMap WHERE TabName = 'NationalHolidayDefine' AND FldName='HolidayDate' AND NotShowFlag='0')")
        sb.Append(" AND OTTxnID NOT IN")
        sb.Append(" (SELECT OTFromAdvanceTxnId FROM OverTimeDeclaration WHERE OTCompID=" + Bsp.Utility.Quote(strComp) + " AND OTEmpID=" + Bsp.Utility.Quote(strEmp) + " AND YEAR(OTStartDate)=YEAR(" + Bsp.Utility.Quote(dateStart) + ") AND MONTH(OTStartDate)=MONTH(" + Bsp.Utility.Quote(dateStart) + ") AND HolidayOrNot='1' AND OTStatus in ('2','3') AND OTFromAdvanceTxnId<>'')")
        If Table = "OverTimeDeclaration" Then
            sb.Append(" AND OTTxnID NOT IN('" + strOTFromAdvanceTxnId + "')")
        End If
        sb.Append(" UNION ALL")
        '事後假日
        sb.Append(" SELECT OTStartDate, ISNULL(OTTotalTime,0) OTTotalTime, ISNULL(MealTime,0) MealTime FROM OverTimeDeclaration")
        sb.Append(" WHERE OTCompID=" + Bsp.Utility.Quote(strComp) + " AND OTEmpID=" + Bsp.Utility.Quote(strEmp) + " AND OTStatus IN ('2','3') AND YEAR(OTStartDate)=YEAR(" + Bsp.Utility.Quote(dateStart) + ") AND MONTH(OTStartDate)=MONTH(" + Bsp.Utility.Quote(dateStart) + ") AND HolidayOrNot='1' ")
        sb.Append(" AND OTStartDate NOT IN (SELECT Code FROM AT_CodeMap WHERE TabName = 'NationalHolidayDefine' AND FldName='HolidayDate' AND NotShowFlag='0')")
        sb.Append(" ) B WHERE 1=1")
        If blstart AndAlso Not blend Then   '假日跨平日
            sb.Append(" AND B.OTStartDate<> " + Bsp.Utility.Quote(dateStart))
        ElseIf Not blstart AndAlso blend Then   '平日跨假日
            sb.Append(" AND B.OTStartDate<> " + Bsp.Utility.Quote(dateEnd))
        ElseIf blstart AndAlso blend Then    '假日跨假日(國定假日不納入檢核)
            If OVBusinessCommon.IsNationalHoliday(dateEnd) Then '迄日假日為國定假日
                sb.Append(" AND B.OTStartDate<> " + Bsp.Utility.Quote(dateStart))
            Else       '起日假日為國定假日
                sb.Append(" AND B.OTStartDate<> " + Bsp.Utility.Quote(dateEnd))
            End If
        End If
        sb.Append(" GROUP BY B.OTStartDate")
        sb.Append(" ) C ) D ) E")

        dt = Bsp.DB.ExecuteDataSet(CommandType.Text, sb.ToString, "AattendantDB").Tables(0)
        dbTotal = Convert.ToDouble(dt.Rows(0)("TotalTime").ToString())

        If dateStart = dateEnd Then '不跨日
            datecheck = totalTime - mealtime
            If Not blstart Then '平日
                If dbTotal + datecheck > (MonthLimitHour * 60) Then
                    Return False
                End If
            Else
                If OVBusinessCommon.IsNationalHoliday(dateStart) Then   '國定假日不納入檢核
                    If dbTotal > (MonthLimitHour * 60) Then
                        Return False
                    End If
                Else
                    dbHoTotal = GetNTotal(strComp, strEmp, dateStart, Table, strOTFromAdvanceTxnId)  '假日時數
                    If dbHoTotal + datecheck > 0 AndAlso dbHoTotal + datecheck <= 240 Then
                        dbHoTotal = 240
                    ElseIf dbHoTotal + datecheck > 240 AndAlso dbHoTotal + datecheck <= 480 Then
                        dbHoTotal = 480
                    ElseIf dbHoTotal + datecheck > 480 AndAlso dbHoTotal + datecheck <= 720 Then
                        dbHoTotal = 720
                    End If
                    If dbHoTotal + dbTotal > (MonthLimitHour * 60) Then
                        Return False
                    End If
                End If
            End If
        Else    '跨日
            If Not blstart AndAlso Not blend Then   '平日
                datecheck = cntStart + cntEnd - mealtime
                If dbTotal + datecheck > (MonthLimitHour * 60) Then
                    Return False
                End If
            ElseIf blstart AndAlso Not blend Then    '假日跨平日
                mealOver = MealJudge(cntStart, mealtime)
                If OVBusinessCommon.IsNationalHoliday(dateStart) Then   '國定假日不納入檢核
                    datecheck = cntEnd - Convert.ToDouble(mealOver.Split(","c)(3))
                    If dbTotal + datecheck > (MonthLimitHour * 60) Then
                        Return False
                    End If
                Else
                    dbHoTotal = GetNTotal(strComp, strEmp, dateStart, Table, strOTFromAdvanceTxnId)  '假日時數
                    datecheck = cntStart - Convert.ToDouble(mealOver.Split(","c)(1))    '本單假日
                    If dbHoTotal + datecheck > 0 AndAlso dbHoTotal + datecheck <= 240 Then
                        dbHoTotal = 240
                    ElseIf dbHoTotal + datecheck > 240 AndAlso dbHoTotal + datecheck <= 480 Then
                        dbHoTotal = 480
                    ElseIf dbHoTotal + datecheck > 480 AndAlso dbHoTotal + datecheck <= 720 Then
                        dbHoTotal = 720
                    End If
                    datecheck = cntEnd - Convert.ToDouble(mealOver.Split(","c)(3))   '本單平日
                    If dbHoTotal + dbTotal + datecheck > (MonthLimitHour * 60) Then
                        Return False
                    End If
                End If
            ElseIf Not blstart AndAlso blend Then    '平日跨假日
                mealOver = MealJudge(cntStart, mealtime)
                If OVBusinessCommon.IsNationalHoliday(dateEnd) Then '國定假日不納入檢核
                    datecheck = cntStart - Convert.ToDouble(mealOver.Split(","c)(1))
                    '本單平日
                    If dbTotal + datecheck > (MonthLimitHour * 60) Then
                        Return False
                    End If
                Else
                    dbHoTotal = GetNTotal(strComp, strEmp, dateEnd, Table, strOTFromAdvanceTxnId)   '假日時數
                    datecheck = cntEnd - Convert.ToDouble(mealOver.Split(","c)(3))  '本單假日
                    If dbHoTotal + datecheck > 0 AndAlso dbHoTotal + datecheck <= 240 Then
                        dbHoTotal = 240
                    ElseIf dbHoTotal + datecheck > 240 AndAlso dbHoTotal + datecheck <= 480 Then
                        dbHoTotal = 480
                    ElseIf dbHoTotal + datecheck > 480 AndAlso dbHoTotal + datecheck <= 720 Then
                        dbHoTotal = 720
                    End If
                    datecheck = cntStart - Convert.ToDouble(mealOver.Split(","c)(1))     '本單平日
                    If dbHoTotal + dbTotal + datecheck > (MonthLimitHour * 60) Then
                        Return False
                    End If
                End If
            ElseIf blstart AndAlso blend Then   '假日跨假日
                '迄日假日為國定假日
                If OVBusinessCommon.IsNationalHoliday(dateEnd) Then '國定假日不納入檢核
                    dbHoTotal = GetNTotal(strComp, strEmp, dateStart, Table, strOTFromAdvanceTxnId)     '假日時數
                    mealOver = MealJudge(cntStart, mealtime)
                    datecheck = cntStart - Convert.ToDouble(mealOver.Split(","c)(1))     '本單假日(起日)
                    If dbHoTotal + datecheck > 0 AndAlso dbHoTotal + datecheck <= 240 Then
                        dbHoTotal = 240
                    ElseIf dbHoTotal + datecheck > 240 AndAlso dbHoTotal + datecheck <= 480 Then
                        dbHoTotal = 480
                    ElseIf dbHoTotal + datecheck > 480 AndAlso dbHoTotal + datecheck <= 720 Then
                        dbHoTotal = 720
                    End If
                    If dbTotal + dbHoTotal > (MonthLimitHour * 60) Then
                        Return False
                    End If
                    '起日假日為國定假日
                ElseIf OVBusinessCommon.IsNationalHoliday(dateStart) Then   '國定假日不納入檢核
                    dbHoTotal = GetNTotal(strComp, strEmp, dateEnd, Table, strOTFromAdvanceTxnId)   '假日時數
                    datecheck = cntEnd - Convert.ToDouble(mealOver.Split(","c)(3))  '本單假日(迄日)
                    If dbHoTotal + datecheck > 0 AndAlso dbHoTotal + datecheck <= 240 Then
                        dbHoTotal = 240
                    ElseIf dbHoTotal + datecheck > 240 AndAlso dbHoTotal + datecheck <= 480 Then
                        dbHoTotal = 480
                    ElseIf dbHoTotal + datecheck > 480 AndAlso dbHoTotal + datecheck <= 720 Then
                        dbHoTotal = 720
                    End If
                    If dbTotal + dbHoTotal > (MonthLimitHour * 60) Then
                        Return False
                    End If
                Else
                    mealOver = MealJudge(cntStart, mealtime)    '查詢須扣除多少用餐時數
                    datecheck = cntStart + cntEnd - mealtime
                    Dim dbStartHoToal As Double = GetNTotal(strComp, strEmp, dateStart, Table, strOTFromAdvanceTxnId)   '加班起日假日時數
                    Dim dbEndHoToal As Double = GetNTotal(strComp, strEmp, dateEnd, Table, strOTFromAdvanceTxnId)   '加班迄日假日時數
                    Dim dateStartCheck As Double = cntStart - Convert.ToDouble(mealOver.Split(","c)(1)) '本單平日(起日)
                    Dim dateEndCheck As Double = cntEnd - Convert.ToDouble(mealOver.Split(","c)(3))  '本單假日(迄日)
                    Dim dbStartHoTotal As Double = 0.0
                    Dim dbEndHoTotal As Double = 0.0

                    If (dbStartHoToal + dateStartCheck) > 0 AndAlso (dbStartHoToal + dateStartCheck) <= 240 Then
                        dbStartHoTotal = 240
                    ElseIf (dbStartHoToal + dateStartCheck) > 240 AndAlso (dbStartHoToal + dateStartCheck) <= 480 Then
                        dbStartHoTotal = 480
                    ElseIf (dbStartHoToal + dateStartCheck) > 480 AndAlso (dbStartHoToal + dateStartCheck) <= 720 Then
                        dbStartHoTotal = 720
                    End If

                    If (dbEndHoToal + dateEndCheck) > 0 AndAlso (dbEndHoToal + dateEndCheck) <= 240 Then
                        dbEndHoTotal = 240
                    ElseIf (dbEndHoToal + dateEndCheck) > 240 AndAlso (dbEndHoToal + dateEndCheck) <= 480 Then
                        dbEndHoTotal = 480
                    ElseIf (dbEndHoToal + dateEndCheck) > 480 AndAlso (dbEndHoToal + dateEndCheck) <= 720 Then
                        dbEndHoTotal = 720
                    End If


                    If (dbStartHoTotal + dbEndHoTotal + dbTotal) > (MonthLimitHour * 60) Then
                        Return False
                    End If
                End If
            End If
        End If
        Return True
    End Function

    '   Public Function checkMonthTime(strComp As String, strEmp As String, dateStart As String, dateEnd As String, MonthLimitHour As Double, totalTime As Double, mealtime As Double, _
    'cntStart As Double, cntEnd As Double) As Boolean
    '       'Dim db As New DbHelper("AattendantDB")
    '       'Dim sb As CommandHelper = db.CreateCommandHelper()
    '       'Dim sb1 As CommandHelper = db.CreateCommandHelper()
    '       Dim ovBusinessCommon As New OVBusinessCommon
    '       Dim blstart As Boolean = CheckHolidayOrNot(dateStart)
    '       Dim blend As Boolean = CheckHolidayOrNot(dateEnd)
    '       Dim dt As DataTable = Nothing
    '       Dim datecheck As Double = 0
    '       Dim dbTotal As Double = 0
    '       Dim dbHoTotal As Double = 0
    '       Dim dbNTotal As Double = 0
    '       Dim mealOver As String = ""

    '       Dim sb As New StringBuilder()
    '       Dim sb1 As New StringBuilder()

    '       sb.Clear()
    '       sb.Append("SELECT SUM(ISNULL(E.TotalTime,0)) AS TotalTime FROM(")
    '       '平日計算(HolidayOrNot='0')：事前加事後
    '       sb.Append(" SELECT ISNULL(Convert(Decimal(10,1),SUM(ISNULL(A.TotalTime,0))),0) AS TotalTime FROM(")
    '       sb.Append(" SELECT ROUND(Convert(Decimal(10,2),(ISNULL(SUM(ISNULL(OTTotalTime,0))-SUM(ISNULL(MealTime,0)),0)))/60,1) AS TotalTime  FROM OverTimeAdvance")
    '       sb.Append(" WHERE OTCompID='" + strComp + "' AND OTEmpID='" + strEmp + "' AND OTStatus IN ('2','3') AND MONTH(OTStartDate)=MONTH('" + dateStart + "') AND HolidayOrNot='0'")
    '       sb.Append(" AND OTTxnID NOT IN (SELECT OTFromAdvanceTxnId FROM OverTimeDeclaration WHERE OTCompID='" + strComp + "' AND OTEmpID='" + strEmp + "' AND MONTH(OTStartDate)=MONTH('" + dateStart + "') AND HolidayOrNot='0'")
    '       sb.Append(" AND OTStatus in ('1','2','3')) GROUP BY OTTxnID")
    '       sb.Append(" UNION ALL")
    '       sb.Append(" SELECT ISNULL(Convert(Decimal(10,1),SUM(ISNULL(D.TotalTime,0))),0) AS TotalTime FROM(")
    '       sb.Append(" SELECT ROUND(Convert(Decimal(10,2),(ISNULL(SUM(ISNULL(OTTotalTime,0))-SUM(ISNULL(MealTime,0)),0)))/60,1) AS TotalTime  FROM OverTimeDeclaration")
    '       sb.Append(" WHERE OTCompID='" + strComp + "' AND OTEmpID='" + strEmp + "' AND OTStatus IN ('2','3') AND MONTH(OTStartDate)=MONTH('" + dateStart + "') AND HolidayOrNot='0' GROUP BY OTTxnID ) D ) A")
    '       sb.Append(" UNION")
    '       '假日計算會有成長時數，以及排除國定假日(HolidayOrNot='1')：事前加事後
    '       sb.Append(" SELECT ISNULL(SUM(ISNULL(B.TotalTime,0)),0) AS TotalTime FROM (")
    '       sb.Append(" SELECT CASE WHEN TotalTime <= 4 THEN 4")
    '       sb.Append(" WHEN TotalTime > 4 AND TotalTime <= 8 THEN 8")
    '       sb.Append(" WHEN TotalTime > 8 AND TotalTime <= 12 THEN 12 END AS TotalTime")
    '       sb.Append(" FROM (")
    '       sb.Append(" SELECT C.OTStartDate,ISNULL(Convert(Decimal(10,1),SUM(C.TotalTime)),0) AS TotalTime FROM(")
    '       sb.Append(" SELECT OTStartDate,OTTxnID,ROUND(Convert(Decimal(10,2),(ISNULL(SUM(ISNULL(OTTotalTime,0))-SUM(ISNULL(MealTime,0)),0)))/60,1) AS TotalTime FROM OverTimeAdvance")
    '       sb.Append(" WHERE OTCompID='" + strComp + "' AND OTEmpID='" + strEmp + "' AND OTStatus IN ('2','3') AND MONTH(OTStartDate)=MONTH('" + dateStart + "') AND HolidayOrNot='1'")
    '       sb.Append(" AND OTTxnID NOT IN (SELECT OTFromAdvanceTxnId FROM OverTimeDeclaration WHERE OTCompID='" + strComp + "' AND OTEmpID='" + strEmp + "' AND MONTH(OTStartDate)=MONTH('" + dateStart + "') AND HolidayOrNot='1'")
    '       sb.Append(" AND OTStatus in ('1','2','3'))")
    '       sb.Append(" AND OTStartDate NOT IN (SELECT Code FROM AT_CodeMap WHERE TabName = 'NationalHolidayDefine' AND FldName='HolidayDate' AND NotShowFlag='0')")
    '       sb.Append(" AND OTStartDate <> '" + dateStart + "' GROUP BY OTStartDate,OTTxnID")
    '       sb.Append(" UNION ALL")
    '       sb.Append(" SELECT OTStartDate,OTTxnID,ROUND(Convert(Decimal(10,2),(ISNULL(SUM(ISNULL(OTTotalTime,0))-SUM(ISNULL(MealTime,0)),0)))/60,1) AS TotalTime  FROM OverTimeDeclaration")
    '       sb.Append(" WHERE OTCompID='" + strComp + "' AND OTEmpID='" + strEmp + "' AND OTStatus IN ('2','3') AND MONTH(OTStartDate)=MONTH('" + dateStart + "') AND HolidayOrNot='1'")
    '       sb.Append(" AND OTStartDate NOT IN (SELECT Code FROM AT_CodeMap WHERE TabName = 'NationalHolidayDefine' AND FldName='HolidayDate' AND NotShowFlag='0')")
    '       sb.Append(" AND OTStartDate <> '" + dateStart + "' GROUP BY OTStartDate,OTTxnID) C GROUP BY OTStartDate")
    '       sb.Append(" ) F ) B ) E")

    '       dt = Bsp.DB.ExecuteDataSet(CommandType.Text, sb.ToString, "AattendantDB").Tables(0)
    '       dbTotal = Convert.ToDouble(dt.Rows(0)("TotalTime").ToString())
    '       If dateStart = dateEnd Then     '是同一天
    '           datecheck = (Math.Round((totalTime - mealtime) / 6, MidpointRounding.AwayFromZero)) / 10        'datecheck = Math.Round((totalTime - mealtime) / 60, 1)
    '           If Not blstart Then     '起日是平日
    '               If (dbTotal + datecheck) > MonthLimitHour Then
    '                   Return False
    '               End If
    '           Else    '起日是假日
    '               If ovBusinessCommon.IsNationalHoliday(dateStart) Then   '國定假日不納入檢核
    '                   If (dbTotal + datecheck) > MonthLimitHour Then
    '                       Return False
    '                   End If
    '               Else
    '                   dbHoTotal = GetNTotal(strComp, strEmp, dateStart)     '假日時數
    '                   If (dbHoTotal + datecheck > 0 AndAlso dbHoTotal + datecheck <= 4) Then
    '                       dbHoTotal = 4
    '                   ElseIf (dbHoTotal + datecheck > 4 AndAlso dbHoTotal + datecheck <= 8) Then
    '                       dbHoTotal = 8
    '                   ElseIf (dbHoTotal + datecheck > 8 AndAlso dbHoTotal + datecheck <= 12) Then
    '                       dbHoTotal = 12
    '                   End If
    '                   If (dbHoTotal + dbTotal > MonthLimitHour) Then
    '                       Return False
    '                   End If
    '               End If
    '           End If
    '       Else    '是跨日
    '           If Not blstart AndAlso Not blend Then
    '               '平日
    '               mealOver = MealJudge(cntStart, mealtime)
    '               datecheck = (Math.Round(((cntStart + cntEnd) - mealtime) / 6, MidpointRounding.AwayFromZero)) / 10        'datecheck = Math.Round((totalTime - mealtime) / 60, 1)
    '               If dbTotal + datecheck > MonthLimitHour Then
    '                   Return False
    '               End If
    '           ElseIf blstart AndAlso Not blend Then   '假日跨平日
    '               dbHoTotal = GetNTotal(strComp, strEmp, dateStart)       '假日時數
    '               mealOver = MealJudge(cntStart, mealtime)

    '               If ovBusinessCommon.IsNationalHoliday(dateStart) Then   '國定假日不納入檢核
    '                   If (dbTotal + datecheck) > MonthLimitHour Then
    '                       Return False
    '                   End If
    '               Else
    '                   datecheck = (Math.Round((cntStart - Convert.ToDouble(mealOver.Split(",")(1))) / 6, MidpointRounding.AwayFromZero)) / 10
    '                   If dbHoTotal + datecheck > 0 AndAlso dbHoTotal + datecheck <= 4 Then
    '                       dbHoTotal = 4
    '                   ElseIf dbHoTotal + datecheck > 4 AndAlso dbHoTotal + datecheck <= 8 Then
    '                       dbHoTotal = 8
    '                   ElseIf dbHoTotal + datecheck > 8 AndAlso dbHoTotal + datecheck <= 12 Then
    '                       dbHoTotal = 12
    '                   End If
    '                   datecheck = (Math.Round((cntEnd - Convert.ToDouble(mealOver.Split(",")(3))) / 6, MidpointRounding.AwayFromZero)) / 10
    '                   If dbHoTotal + dbTotal + datecheck > MonthLimitHour Then
    '                       Return False
    '                   End If
    '               End If
    '           ElseIf Not blstart AndAlso blend Then   '平日跨假日
    '               dbHoTotal = GetNTotal(strComp, strEmp, dateEnd)       '假日時數
    '               mealOver = MealJudge(cntStart, mealtime)

    '               If ovBusinessCommon.IsNationalHoliday(dateEnd) Then   '國定假日不納入檢核
    '                   datecheck = (Math.Round((cntStart - Convert.ToDouble(mealOver.Split(",")(1))) / 6, MidpointRounding.AwayFromZero)) / 10
    '                   If (dbTotal + datecheck) > MonthLimitHour Then
    '                       Return False
    '                   End If
    '               Else
    '                   datecheck = (Math.Round((cntEnd - Convert.ToDouble(mealOver.Split(",")(3))) / 6, MidpointRounding.AwayFromZero)) / 10
    '                   If dbHoTotal + datecheck > 0 AndAlso dbHoTotal + datecheck <= 4 Then
    '                       dbHoTotal = 4
    '                   ElseIf dbHoTotal + datecheck > 4 AndAlso dbHoTotal + datecheck <= 8 Then
    '                       dbHoTotal = 8
    '                   ElseIf dbHoTotal + datecheck > 8 AndAlso dbHoTotal + datecheck <= 12 Then
    '                       dbHoTotal = 12
    '                   End If
    '                   datecheck = (Math.Round((cntStart - Convert.ToDouble(mealOver.Split(",")(1))) / 6, MidpointRounding.AwayFromZero)) / 10
    '                   If dbHoTotal + dbTotal + datecheck > MonthLimitHour Then
    '                       Return False
    '                   End If
    '               End If
    '           ElseIf Not blstart AndAlso blend Then   '假日跨假日
    '               If ovBusinessCommon.IsNationalHoliday(dateEnd) Then   '迄日假日為國定假日,國定假日不納入檢核
    '                   dbHoTotal = GetNTotal(strComp, strEmp, dateStart)     '假日時數
    '                   mealOver = MealJudge(cntStart, mealtime)

    '                   datecheck = (Math.Round((cntStart - Convert.ToDouble(mealOver.Split(",")(1))) / 6, MidpointRounding.AwayFromZero)) / 10
    '                   If dbHoTotal + datecheck > 0 AndAlso dbHoTotal + datecheck <= 4 Then
    '                       dbHoTotal = 4
    '                   ElseIf dbHoTotal + datecheck > 4 AndAlso dbHoTotal + datecheck <= 8 Then
    '                       dbHoTotal = 8
    '                   ElseIf dbHoTotal + datecheck > 8 AndAlso dbHoTotal + datecheck <= 12 Then
    '                       dbHoTotal = 12
    '                   End If
    '                   If dbHoTotal + dbTotal + datecheck > MonthLimitHour Then
    '                       Return False
    '                   End If
    '               ElseIf ovBusinessCommon.IsNationalHoliday(dateEnd) Then '起日假日為國定假日,國定假日不納入檢核
    '                   dbHoTotal = GetNTotal(strComp, strEmp, dateEnd)     '假日時數
    '                   datecheck = (Math.Round((cntEnd - Convert.ToDouble(mealOver.Split(",")(3))) / 6, MidpointRounding.AwayFromZero)) / 10
    '                   If dbHoTotal + datecheck > 0 AndAlso dbHoTotal + datecheck <= 4 Then
    '                       dbHoTotal = 4
    '                   ElseIf dbHoTotal + datecheck > 4 AndAlso dbHoTotal + datecheck <= 8 Then
    '                       dbHoTotal = 8
    '                   ElseIf dbHoTotal + datecheck > 8 AndAlso dbHoTotal + datecheck <= 12 Then
    '                       dbHoTotal = 12
    '                   End If
    '                   If dbHoTotal + dbTotal + datecheck > MonthLimitHour Then
    '                       Return False
    '                   End If
    '               End If
    '           End If
    '       End If
    '       Return True
    '   End Function

    ''' <summary>
    ''' 算假日的成長時數
    ''' </summary>
    ''' <param name="strComp">加班人公司</param>
    ''' <param name="strEmp">加班人</param>
    ''' <param name="OTdate">加班日期</param>
    ''' <returns>假日時數</returns>
    ''' <remarks></remarks>
    Public Function GetNTotal(ByVal strComp As String, ByVal strEmp As String, ByVal OTdate As String, ByVal Table As String, ByVal strOTFromAdvanceTxnId As String)
        Dim sb As New StringBuilder()
        Dim dbHoTotal As Double = 0

        '以小時計算
        sb.Append("SELECT ISNULL(SUM(A.TotalTime),0) AS TotalTime FROM")
        sb.Append(" (SELECT SUM(OTTotalTime)-SUM(MealTime) AS TotalTime  FROM OverTimeAdvance WHERE OTCompID=" + Bsp.Utility.Quote(strComp) + "  AND OTEmpID=" + Bsp.Utility.Quote(strEmp) + " AND OTStartDate=" + Bsp.Utility.Quote(OTdate) + " AND OTStatus in ('2','3')")
        If Table = "OverTimeDeclaration" Then
            sb.Append(" AND OTTxnID NOT IN('" + strOTFromAdvanceTxnId + "')")
        End If
        sb.Append(" UNION ALL")
        sb.Append(" SELECT SUM(OTTotalTime)-SUM(MealTime) AS TotalTime FROM OverTimeDeclaration WHERE OTCompID=" + Bsp.Utility.Quote(strComp) + " AND OTEmpID=" + Bsp.Utility.Quote(strEmp) + " AND OTStartDate=" + Bsp.Utility.Quote(OTdate) + " AND OTStatus in ('2','3')) A")
        Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, sb.ToString, "AattendantDB").Tables(0)
            '假日時數
            dbHoTotal = Convert.ToDouble(dt.Rows(0)("TotalTime").ToString())
        End Using

        Return dbHoTotal
    End Function

    ''' <summary>
    ''' 判斷迄日是否需要扣除不足的用餐時間
    ''' </summary>
    ''' <param name="cntStart">起日TotalTime(分鐘)</param>
    ''' <param name="MealTime">用餐時間(分鐘)</param>
    ''' <returns>ResultArr</returns>
    ''' <remarks>
    ''' ResultArr(0) -> StartDayMealFlag
    ''' ResultArr(1) -> StartDayMealTime
    ''' ResultArr(2) -> EndDayMealFlag
    ''' ResultArr(3) -> EndDayMealTime
    ''' </remarks>
    Public Function MealJudge(ByVal cntStart As Double, ByVal MealTime As Double) As String
        Dim Result As String = ""
        Dim StartDayMealFlag As String = String.Empty
        Dim StartDayMealTime As String = String.Empty
        Dim EndDayMealFlag As String = String.Empty
        Dim EndDayMealTime As String = String.Empty
        If MealTime = 0 Then
            StartDayMealFlag = "0"
            StartDayMealTime = "0"
            EndDayMealFlag = "0"
            EndDayMealTime = "0"
        ElseIf (cntStart - MealTime) < 0 Then
            StartDayMealFlag = "1"
            StartDayMealTime = cntStart.ToString()
            EndDayMealFlag = "1"
            EndDayMealTime = (MealTime - cntStart).ToString()
        Else
            StartDayMealFlag = "1"
            StartDayMealTime = MealTime.ToString()
            EndDayMealFlag = "1"
            EndDayMealTime = "0"
        End If

        Result = StartDayMealFlag + "," + StartDayMealTime + "," + EndDayMealFlag + "," + EndDayMealTime

        Return Result
    End Function

    ''' <summary>
    ''' 計算已申報時數合計
    ''' </summary>
    ''' <param name="CompID"></param>
    ''' <param name="EmpID"></param>
    ''' <param name="OTStartDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetOverTimeSum(ByVal CompID As String, ByVal EmpID As String, ByVal OTStartDate As String) As ArrayList
        Dim OverTimeSumArr As New ArrayList
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT A.Submit,A.Approval,(A.Submit+A.Approval) AS Total,A.Reject FROM (")
        '2017/02/15-資料庫改用分鐘計算~
        strSQL.AppendLine(" SELECT SUM(CASE OTStatus WHEN '2' THEN (CASE MealFlag WHEN 1 THEN Convert(Decimal(10,2),(CAST(OTTotalTime AS FLOAT)-CAST(MealTime AS FLOAT))/CAST(60 AS FLOAT)) ELSE Convert(Decimal(10,2),(CAST(OTTotalTime AS FLOAT)/CAST(60 AS FLOAT))) END) ELSE '0.0' END) AS Submit, ")
        strSQL.AppendLine(" SUM(CASE OTStatus WHEN '3' THEN (CASE MealFlag WHEN 1 THEN Convert(Decimal(10,2),(CAST(OTTotalTime AS FLOAT)-CAST(MealTime AS FLOAT))/CAST(60 AS FLOAT)) ELSE Convert(Decimal(10,2),(CAST(OTTotalTime AS FLOAT)/CAST(60 AS FLOAT))) END) ELSE '0.0' END)  AS Approval, ")
        strSQL.AppendLine(" SUM(CASE OTStatus WHEN '4' THEN (CASE MealFlag WHEN 1 THEN Convert(Decimal(10,2),(CAST(OTTotalTime AS FLOAT)-CAST(MealTime AS FLOAT))/CAST(60 AS FLOAT)) ELSE Convert(Decimal(10,2),(CAST(OTTotalTime AS FLOAT)/CAST(60 AS FLOAT))) END) ELSE '0.0' END) AS Reject ")
        strSQL.AppendLine(" FROM OverTimeDeclaration ")
        strSQL.AppendLine(" WHERE OTCompID = '" + CompID + "'")
        strSQL.AppendLine(" AND MONTH(OTStartDate) = MONTH('" + OTStartDate + "') ")
        strSQL.AppendLine(" AND YEAR(OTStartDate) = YEAR('" + OTStartDate + "') ")
        strSQL.AppendLine(" AND OTEmpID = '" + EmpID + "') A")

        Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "AattendantDB").Tables(0)
            If dt.Rows.Count > 0 Then
                Dim submitTime, approvalTime, rejectTime As String

                submitTime = If((dt.Rows(0)("Submit").ToString() = "0.00" OrElse dt.Rows(0)("Submit").ToString() = ""), "0.0", (Math.Round(Convert.ToDouble(dt.Rows(0)("Submit")), 1, MidpointRounding.AwayFromZero)).ToString("0.0"))
                '本月送簽總時數
                approvalTime = If((dt.Rows(0)("Approval").ToString() = "0.00" OrElse dt.Rows(0)("Approval").ToString() = ""), "0.0", (Math.Round(Convert.ToDouble(dt.Rows(0)("Approval")), 1, MidpointRounding.AwayFromZero)).ToString("0.0"))
                '本月核准總時數
                rejectTime = If((dt.Rows(0)("Reject").ToString() = "0.00" OrElse dt.Rows(0)("Reject").ToString() = ""), "0.0", (Math.Round(Convert.ToDouble(dt.Rows(0)("Reject")), 1, MidpointRounding.AwayFromZero)).ToString("0.0"))
                '本月駁回總時數

                '送簽
                OverTimeSumArr.Add(submitTime)

                '核准
                OverTimeSumArr.Add(approvalTime)

                '駁回
                OverTimeSumArr.Add(rejectTime)
            End If
        End Using
        Return OverTimeSumArr
    End Function

#Region "2017/03/20-新時段計算"
    ''' <summary>
    ''' 時段計算(單日、跨日共用)
    ''' </summary>
    ''' <param name="table">資料表名稱</param>
    ''' <param name="strOTEmpID">加班人員工編號</param>
    ''' <param name="cntStart">起日TotalTime</param>
    ''' <param name="cntEnd">迄日TotalTime</param>
    ''' <param name="startDate">起日Arr</param>
    ''' <param name="endDate">迄日Arr</param>
    ''' <param name="MealTime">用餐時間</param>
    ''' <param name="MealFlag">用餐註記</param>
    ''' <param name="ottxnid">單號</param>
    ''' <param name="reMsg">回傳訊息</param>
    ''' <returns>若回傳為true，則顯示加班時段，否則顯示錯誤訊息</returns>
    ''' <remarks>
    ''' startDate(0)=StartDate
    ''' startDate(1)=StartbeginTime
    ''' startDate(2)=StartendTime
    ''' EndDate(0)=EndDate
    ''' EndDate(1)=EndbeginTime
    ''' EndDate(2)=EndendTime
    ''' </remarks>
    Public Function PeriodCount(ByVal table As String, ByVal strOTEmpID As String, ByVal cntStart As Double, ByVal cntEnd As Double, ByVal startDate As ArrayList, ByVal endDate As ArrayList, ByVal MealTime As Double, ByVal MealFlag As String, ByVal ottxnid As String, ByRef reMsg As String) As Boolean
        Dim strSQL As New StringBuilder()

        '跨日的時段計算
        Dim execResult As Boolean = True
        Dim Result As String = ""
        Dim outMsg As String = ""

        'TimePeriodBegin
        Dim StartDatePeriod1 As Double = 0.0
        Dim StartDatePeriod2 As Double = 0.0
        Dim StartDatePeriod3 As Double = 0.0
        Dim DateB As String = startDate(0)

        'TimePeriodEnd
        Dim EndDatePeriod1 As Double = 0.0
        Dim EndDatePeriod2 As Double = 0.0
        Dim EndDatePeriod3 As Double = 0.0
        Dim DateE As String = endDate(0)

        If "1900/01/01".Equals(DateE) Then
            'TimePeriodEnd
            EndDatePeriod1 = 0.0
            EndDatePeriod2 = 0.0
            EndDatePeriod3 = 0.0
            DateE = startDate(0)
            cntEnd = cntStart
        Else
            'TimePeriodBegin
            StartDatePeriod1 = 0.0
            StartDatePeriod2 = 0.0
            StartDatePeriod3 = 0.0
            DateB = startDate(0)

            'TimePeriodEnd
            EndDatePeriod1 = 0.0
            EndDatePeriod2 = 0.0
            EndDatePeriod3 = 0.0
            DateE = endDate(0)
        End If

        Dim LastPeriod1 As Double = 0.0
        Dim LastPeriod2 As Double = 0.0
        Dim LastPeriod3 As Double = 0.0
        Dim LastDate As String = ""
        Dim LastTxnID As String = ""

        Dim StartDateHo As String = ""
        Dim EndDateHo As String = ""

        Dim FlagPeriod1 As Boolean = False
        Dim FlagPeriod2 As Boolean = False
        Dim FlagPeriod3 As Boolean = False

        '畫面是否為假日
        If "1900/01/01".Equals(endDate(0)) Then
            Using dtStartHo As DataTable = QueryData("HolidayOrNot", _eHRMSDB_ITRD + ".[dbo].[Calendar]", " AND CompID =" + Bsp.Utility.Quote(UserProfile.SelectCompRoleID.ToString().Trim) + " AND CONVERT(CHAR(10),SysDate, 111) =" + Bsp.Utility.Quote(startDate(0)))
                If dtStartHo.Rows(0).Item("HolidayOrNot").ToString() = "0" Then
                    StartDateHo = "0"
                Else
                    StartDateHo = "1"
                End If
            End Using
        Else
            Using dtStartHo As DataTable = QueryData("HolidayOrNot", _eHRMSDB_ITRD + ".[dbo].[Calendar]", " AND CompID =" + Bsp.Utility.Quote(UserProfile.SelectCompRoleID.ToString().Trim) + " AND CONVERT(CHAR(10),SysDate, 111) =" + Bsp.Utility.Quote(startDate(0)))
                If dtStartHo.Rows(0).Item("HolidayOrNot").ToString() = "0" Then
                    StartDateHo = "0"
                Else
                    StartDateHo = "1"
                End If
            End Using

            Using dtEndHo As DataTable = QueryData("HolidayOrNot", _eHRMSDB_ITRD + ".[dbo].[Calendar]", " AND CompID =" + Bsp.Utility.Quote(UserProfile.SelectCompRoleID.ToString().Trim) + " AND CONVERT(CHAR(10),SysDate, 111) =" + Bsp.Utility.Quote(endDate(0)))
                If dtEndHo.Rows(0).Item("HolidayOrNot").ToString() = "0" Then
                    EndDateHo = "0"
                Else
                    EndDateHo = "1"
                End If
            End Using
        End If
        '計算吃飯時間
        If "1900/01/01".Equals(endDate(0)) Then
            If MealFlag = "1" Then
                '如果有要扣吃飯時間
                cntStart = cntStart - MealTime
            End If
            'cntStart = (Math.Round(cntStart / 6, 1, MidpointRounding.AwayFromZero)) / 10     '轉小時
        Else
            If MealFlag = "1" Then
                '如果有要扣吃飯時間
                cntStart = cntStart - MealTime
                If cntStart < 0.0 Then
                    cntEnd = cntEnd + cntStart
                    cntStart = 0.0
                End If
            End If
            'cntStart = (Math.Round(cntStart / 6, 1, MidpointRounding.AwayFromZero)) / 10     '轉小時
            'cntEnd = (Math.Round(cntStart / 6, 1, MidpointRounding.AwayFromZero)) / 10     '轉小時
        End If

        Dim OverTimeDatas As DataTable
        '撈出正確的資料
        If "1900/01/01".Equals(endDate(0)) Then
            OverTimeDatas = QueryData("OTStartDate,OTEndDate,OTTxnID,OTStartTime,OTEndTime,OTTotalTime,MealFlag,MealTime,HolidayOrNot", table, "AND MONTH(CONVERT(DATETIME, OTStartDate))=MONTH(CONVERT(DATETIME, " + Bsp.Utility.Quote(startDate(0)) + ")) AND OTStartDate <= " + Bsp.Utility.Quote(startDate(0)) + " AND OTCompID= " + Bsp.Utility.Quote(UserProfile.SelectCompRoleID.ToString().Trim) + " AND OTEmpID=" + Bsp.Utility.Quote(strOTEmpID) + " AND OTStatus IN ('2', '3') AND NOT(OTTxnID= " + Bsp.Utility.Quote(ottxnid) + ") ORDER BY OTStartDate, OTStartTime ")
        Else
            'OverTimeDatas = QueryData("OTStartDate,OTEndDate,OTTxnID,OTStartTime,OTEndTime,OTTotalTime,MealFlag,MealTime,HolidayOrNot", table, "AND MONTH(CONVERT(DATETIME, OTStartDate))=MONTH(CONVERT(DATETIME, " + Bsp.Utility.Quote(endDate(0)) + ")) AND OTStartDate <= " + Bsp.Utility.Quote(endDate(0)) + " AND OTCompID= " + Bsp.Utility.Quote(UserProfile.SelectCompRoleID.ToString().Trim) + " AND OTEmpID=" + Bsp.Utility.Quote(strOTEmpID) + " AND OTStatus IN ('2', '3') AND NOT(OTTxnID= " + Bsp.Utility.Quote(ottxnid) + ") ORDER BY OTStartDate, OTStartTime ")
            OverTimeDatas = QueryData("OTStartDate,OTEndDate,OTTxnID,OTStartTime,OTEndTime,OTTotalTime,MealFlag,MealTime,HolidayOrNot", table, "AND MONTH(CONVERT(DATETIME, OTStartDate))=MONTH(CONVERT(DATETIME, " + Bsp.Utility.Quote(startDate(0)) + ")) AND OTStartDate <= " + Bsp.Utility.Quote(startDate(0)) + " AND OTCompID=" + Bsp.Utility.Quote(UserProfile.SelectCompRoleID.ToString().Trim) + " AND OTEmpID=" + Bsp.Utility.Quote(strOTEmpID) + " AND OTStatus IN ('2', '3') AND NOT(OTTxnID=" + Bsp.Utility.Quote(ottxnid) + ") ORDER BY OTStartDate, OTStartTime ")
        End If

        If "1900/01/01".Equals(endDate(0)) Then
            Dim EndDateViewData As DataRow = OverTimeDatas.NewRow()
            EndDateViewData.Item("OTStartDate") = startDate(0)
            EndDateViewData.Item("OTEndDate") = startDate(0)
            EndDateViewData.Item("OTTxnID") = "ViewData"
            EndDateViewData.Item("OTStartTime") = startDate(1).ToString()
            EndDateViewData.Item("OTEndTime") = startDate(2).ToString()
            EndDateViewData.Item("OTTotalTime") = cntStart
            EndDateViewData.Item("MealFlag") = "0"  'MealFlag
            EndDateViewData.Item("MealTime") = 0     'MealTime
            EndDateViewData.Item("HolidayOrNot") = StartDateHo
            OverTimeDatas.Rows.Add(EndDateViewData)
        Else
            Dim StartDateViewData As DataRow = OverTimeDatas.NewRow()
            StartDateViewData.Item("OTStartDate") = startDate(0)
            StartDateViewData.Item("OTEndDate") = startDate(0)
            StartDateViewData.Item("OTTxnID") = "ViewData"
            StartDateViewData.Item("OTStartTime") = startDate(1).ToString()
            StartDateViewData.Item("OTEndTime") = startDate(2).ToString()
            StartDateViewData.Item("OTTotalTime") = cntStart
            StartDateViewData.Item("MealFlag") = "0"    'MealFlag
            StartDateViewData.Item("MealTime") = 0    'MealTime
            StartDateViewData.Item("HolidayOrNot") = StartDateHo
            OverTimeDatas.Rows.Add(StartDateViewData)

            Dim EndDateViewData As DataRow = OverTimeDatas.NewRow()
            EndDateViewData.Item("OTStartDate") = endDate(0)
            EndDateViewData.Item("OTEndDate") = endDate(0)
            EndDateViewData.Item("OTTxnID") = "ViewData"
            EndDateViewData.Item("OTStartTime") = endDate(1).ToString()
            EndDateViewData.Item("OTEndTime") = endDate(2).ToString()
            EndDateViewData.Item("OTTotalTime") = cntEnd
            EndDateViewData.Item("MealFlag") = "0"
            EndDateViewData.Item("MealTime") = 0
            EndDateViewData.Item("HolidayOrNot") = EndDateHo
            OverTimeDatas.Rows.Add(EndDateViewData)
        End If

        OverTimeDatas.DefaultView.Sort = "OTStartDate ASC, OTStartTime ASC"
        OverTimeDatas = OverTimeDatas.DefaultView.ToTable()
        Dim LastRowTotalTime As Double = 0
        Dim LastRowMinTime As Double = 0
        Dim SameDayTotalMinTime As Double = 0
        For i = 0 To (OverTimeDatas.Rows.Count - 1)
            Dim Period1 As Double = 0
            Dim Period2 As Double = 0
            Dim Period3 As Double = 0
            Dim RowTotalMinTime = Convert.ToDouble(OverTimeDatas.Rows(i).Item("OTTotalTime").ToString())       '轉分鐘
            Dim RowTotalTime As Double = 0

            Dim RowTxnID As String = OverTimeDatas.Rows(i).Item("OTTxnID").ToString()  '單號
            Dim RowDate As String = OverTimeDatas.Rows(i).Item("OTStartDate").ToString()
            Dim HolidayOrNot As String = OverTimeDatas.Rows(i).Item("HolidayOrNot").ToString()
            Dim RowMealFlag As String = OverTimeDatas.Rows(i).Item("MealFlag").ToString()
            Dim RowMealTime = Convert.ToDouble(OverTimeDatas.Rows(i).Item("MealTime").ToString())
            If RowMealFlag = "1" Then    '如果有要扣吃飯時間
                RowTotalMinTime = RowTotalMinTime - RowMealTime
            End If

            RowTotalTime = (Math.Round(RowTotalMinTime / 6, MidpointRounding.AwayFromZero)) / 10  '轉小時
            'RowTotalTime = CDbl(FormatNumber(RowTotalMinTime / 60, 1))
            '轉小時
            If LastTxnID <> RowTxnID Then      '單號不同
                If RowDate = DateB Then
                    If HolidayOrNot = "0" Then   '起日是平日
                        If LastDate <> DateB Then   '從頭算(重新來過)
                            SameDayTotalMinTime = RowTotalMinTime
                            If RowTotalTime > 4 Then
                                Period1 = 2
                                Period2 = RowTotalTime - 2
                                FlagPeriod1 = False
                                FlagPeriod2 = True
                            ElseIf RowTotalTime <= 4 AndAlso RowTotalTime > 2 Then
                                Period1 = 2
                                Period2 = RowTotalTime - 2
                                FlagPeriod1 = False
                                FlagPeriod2 = True
                            ElseIf RowTotalTime <= 2 Then
                                Period1 = RowTotalTime
                                If Period1 < 2 Then
                                    FlagPeriod1 = True
                                    FlagPeriod2 = False
                                Else
                                    FlagPeriod1 = False
                                    FlagPeriod2 = True
                                End If
                            End If
                        ElseIf LastDate = DateB Then    '繼續算(繼續算下一筆)
                            'If (LastPeriod1 + LastPeriod2 + RowTotalTime) > 4 Then
                            '    outMsg = "累積時數超過4小時!"
                            '    execResult = False
                            'ElseIf (LastPeriod1 + LastPeriod2 + RowTotalTime) < 4 AndAlso (LastPeriod1 + LastPeriod2 + RowTotalTime) > 2 Then
                            '    Period1 = 2
                            '    Period2 = (LastPeriod1 + LastPeriod2 + RowTotalTime) - 2
                            '    FlagPeriod1 = False
                            '    FlagPeriod2 = True
                            'ElseIf (LastPeriod1 + LastPeriod2 + RowTotalTime) <= 2 Then
                            '    Period1 = (LastPeriod1 + RowTotalTime)
                            '    Period2 = LastPeriod2
                            '    If Period1 < 2 Then
                            '        FlagPeriod1 = True
                            '        FlagPeriod2 = False
                            '    Else
                            '        FlagPeriod1 = False
                            '        FlagPeriod2 = True
                            '    End If
                            If (SameDayTotalMinTime + RowTotalMinTime) > 240 Then
                                Period1 = 2
                                Period2 = (LastPeriod1 + LastPeriod2 + RowTotalTime) - 2
                                SameDayTotalMinTime = SameDayTotalMinTime + RowTotalMinTime
                                FlagPeriod1 = False
                                FlagPeriod2 = True
                            ElseIf (SameDayTotalMinTime + RowTotalMinTime) <= 240 AndAlso (SameDayTotalMinTime + RowTotalMinTime) > 120 Then
                                If LastPeriod1 + RowTotalTime < 2 Then
                                    Period1 = LastPeriod1 + RowTotalTime
                                    Period2 = LastPeriod2
                                    SameDayTotalMinTime = SameDayTotalMinTime + RowTotalMinTime
                                    FlagPeriod1 = True
                                    FlagPeriod2 = False
                                Else
                                    Period1 = 2
                                    Period2 = (LastPeriod1 + LastPeriod2 + RowTotalTime) - 2
                                    SameDayTotalMinTime = SameDayTotalMinTime + RowTotalMinTime
                                    FlagPeriod1 = False
                                    FlagPeriod2 = True
                                End If
                            ElseIf (SameDayTotalMinTime + RowTotalMinTime) <= 120 Then
                                Period1 = (LastPeriod1 + RowTotalTime)
                                If Period1 < 2 Then
                                    Period2 = LastPeriod2
                                    SameDayTotalMinTime = SameDayTotalMinTime + RowTotalMinTime
                                    FlagPeriod1 = True
                                    FlagPeriod2 = False
                                Else
                                    SameDayTotalMinTime = SameDayTotalMinTime + RowTotalMinTime
                                    FlagPeriod1 = False
                                    FlagPeriod2 = True
                                End If
                            End If
                        End If
                        LastTxnID = RowTxnID
                        LastDate = RowDate
                        LastRowTotalTime = RowTotalTime
                        LastRowMinTime = RowTotalMinTime
                        LastPeriod1 = Period1
                        LastPeriod2 = Period2
                        LastPeriod3 = Period3
                        StartDatePeriod1 = LastPeriod1
                        StartDatePeriod2 = LastPeriod2
                        StartDatePeriod3 = LastPeriod3
                    ElseIf HolidayOrNot = "1" Then '起日是假日
                        If LastDate <> DateB Then    '從頭算(重新來過)
                            'If RowTotalTime <= 12 Then
                            Period3 = RowTotalTime
                            FlagPeriod3 = True
                            'End If
                        ElseIf LastDate = DateB Then  '繼續算(繼續算下一筆)
                            'If LastPeriod3 + RowTotalTime > 12 Then
                            '    outMsg = "累積時數超過12小時!"
                            '    execResult = False
                            'If LastPeriod3 + RowTotalTime <= 12 Then
                            Period3 = LastPeriod3 + RowTotalTime
                            FlagPeriod3 = True
                            'End If
                        End If
                        LastTxnID = RowTxnID
                        LastDate = RowDate
                        LastRowTotalTime = RowTotalTime
                        LastRowMinTime = RowTotalMinTime
                        LastPeriod1 = Period1
                        LastPeriod2 = Period2
                        LastPeriod3 = Period3
                        StartDatePeriod1 = LastPeriod1
                        StartDatePeriod2 = LastPeriod2
                        StartDatePeriod3 = LastPeriod3
                    End If
                ElseIf LastDate = DateE Then
                    SameDayTotalMinTime = LastRowMinTime + RowTotalMinTime
                    If HolidayOrNot = "0" Then      '迄日是平日
                        If LastDate <> DateE Then        '從頭算(重新來過)
                            'If RowTotalTime > 4 Then
                            '    outMsg = "累積時數超過4小時!"
                            '    execResult = False
                            If RowTotalTime <= 4 AndAlso RowTotalTime > 2 Then
                                Period1 = 2
                                Period2 = RowTotalTime - 2
                                FlagPeriod1 = False
                                FlagPeriod2 = True
                            ElseIf RowTotalTime <= 2 Then
                                Period1 = RowTotalTime
                                If Period1 < 2 Then
                                    FlagPeriod1 = True
                                    FlagPeriod2 = False
                                Else
                                    FlagPeriod1 = False
                                    FlagPeriod2 = True
                                End If
                            End If
                        ElseIf LastDate = DateE Then   '繼續算(繼續算下一筆)
                            'If (LastPeriod1 + LastPeriod2 + RowTotalTime) > 4 Then
                            '    outMsg = "累積時數超過4小時!"
                            '    execResult = False
                            If (SameDayTotalMinTime + RowTotalMinTime) > 240 Then
                                Period1 = 2
                                Period2 = (LastPeriod1 + LastPeriod2 + RowTotalTime) - 2
                                SameDayTotalMinTime = LastRowMinTime + RowTotalMinTime
                                FlagPeriod1 = False
                                FlagPeriod2 = True
                            ElseIf (SameDayTotalMinTime + RowTotalMinTime) <= 240 AndAlso (SameDayTotalMinTime + RowTotalMinTime) > 120 Then
                                If LastPeriod1 + RowTotalTime < 2 Then
                                    Period1 = LastPeriod1 + RowTotalTime
                                    Period2 = LastPeriod2
                                    SameDayTotalMinTime = LastRowMinTime + RowTotalMinTime
                                    FlagPeriod1 = True
                                    FlagPeriod2 = False
                                Else
                                    Period1 = 2
                                    Period2 = (LastPeriod1 + LastPeriod2 + RowTotalTime) - 2
                                    SameDayTotalMinTime = LastRowMinTime + RowTotalMinTime
                                    FlagPeriod1 = False
                                    FlagPeriod2 = True
                                End If
                            ElseIf (SameDayTotalMinTime + RowTotalMinTime) <= 120 Then
                                Period1 = (LastPeriod1 + RowTotalTime)
                                If Period1 < 2 Then
                                    Period2 = LastPeriod2
                                    SameDayTotalMinTime = LastRowMinTime + RowTotalMinTime
                                    FlagPeriod1 = True
                                    FlagPeriod2 = False
                                Else
                                    SameDayTotalMinTime = LastRowMinTime + RowTotalMinTime
                                    FlagPeriod1 = False
                                    FlagPeriod2 = True
                                End If
                            End If
                        End If
                        LastTxnID = RowTxnID
                        LastDate = RowDate
                        LastRowTotalTime = RowTotalTime
                        LastRowMinTime = RowTotalMinTime
                        LastPeriod1 = Period1
                        LastPeriod2 = Period2
                        LastPeriod3 = Period3
                        EndDatePeriod1 = LastPeriod1
                        EndDatePeriod2 = LastPeriod2
                        EndDatePeriod3 = LastPeriod3
                    ElseIf HolidayOrNot = "1" Then        '迄日是假日
                        If LastDate <> DateE Then    '從頭算(重新來過)
                            'If RowTotalTime <= 12 Then
                            Period3 = RowTotalTime
                            FlagPeriod3 = True
                            'End If
                        ElseIf LastDate = DateE Then       '繼續算(繼續算下一筆)
                            'If LastPeriod3 + RowTotalTime > 12 Then
                            '    outMsg = "累積時數超過12小時!"
                            '    execResult = False
                            'If LastPeriod3 + RowTotalTime <= 12 Then
                            Period3 = LastPeriod3 + RowTotalTime
                            FlagPeriod3 = True
                            'End If
                        End If
                        LastTxnID = RowTxnID
                        LastDate = RowDate
                        LastRowTotalTime = RowTotalTime
                        LastRowMinTime = RowTotalMinTime
                        LastPeriod1 = Period1
                        LastPeriod2 = Period2
                        LastPeriod3 = Period3
                        EndDatePeriod1 = LastPeriod1
                        EndDatePeriod2 = LastPeriod2
                        EndDatePeriod3 = LastPeriod3
                    End If
                ElseIf RowDate <> DateB AndAlso RowDate <> DateE Then    '一般情況
                    If HolidayOrNot = "0" Then        '平日
                        If LastDate <> RowDate Then     '從頭算(重新來過)
                            SameDayTotalMinTime = RowTotalMinTime
                            'If RowTotalTime > 4 Then
                            '    outMsg = "累積時數超過4小時!"
                            '    execResult = False
                            If RowTotalTime <= 4 AndAlso RowTotalTime > 2 Then
                                Period1 = 2
                                Period2 = RowTotalTime - 2
                                FlagPeriod1 = False
                                FlagPeriod2 = True
                            ElseIf RowTotalTime <= 2 Then
                                Period1 = RowTotalTime
                                If Period1 < 2 Then
                                    FlagPeriod1 = True
                                    FlagPeriod2 = False
                                Else
                                    FlagPeriod1 = False
                                    FlagPeriod2 = True
                                End If
                            End If
                        ElseIf LastDate = RowDate Then  '繼續算(繼續算下一筆)
                            'If (LastPeriod1 + LastPeriod2 + RowTotalTime) > 4 Then
                            '    outMsg = "累積時數超過4小時!"
                            '    execResult = False
                            If (SameDayTotalMinTime + RowTotalMinTime) > 240 Then
                                Period1 = 2
                                Period2 = (LastPeriod1 + LastPeriod2 + RowTotalTime) - 2
                                SameDayTotalMinTime = SameDayTotalMinTime + RowTotalMinTime
                                FlagPeriod1 = False
                                FlagPeriod2 = True
                            ElseIf (SameDayTotalMinTime + RowTotalMinTime) <= 240 AndAlso (LastRowMinTime + RowTotalMinTime) > 120 Then
                                Period1 = 2
                                Period2 = (LastPeriod1 + LastPeriod2 + RowTotalTime) - 2
                                SameDayTotalMinTime = SameDayTotalMinTime + RowTotalMinTime
                                FlagPeriod1 = False
                                FlagPeriod2 = True
                                'ElseIf (LastPeriod1 + LastPeriod2 + RowTotalTime) <= 4 AndAlso (LastPeriod1 + LastPeriod2 + RowTotalTime) > 2 Then
                                '    Period1 = 2
                                '    Period2 = (LastPeriod1 + LastPeriod2 + RowTotalTime) - 2
                                '    FlagPeriod1 = False
                                '    FlagPeriod2 = True
                                'ElseIf (LastPeriod1 + LastPeriod2 + RowTotalTime) <= 2 Then
                                '    Period1 = (LastPeriod1 + LastPeriod2 + RowTotalTime)
                                '    If Period1 < 2 Then
                                '        FlagPeriod1 = True
                                '        FlagPeriod2 = False
                                '    Else
                                '        FlagPeriod1 = False
                                '        FlagPeriod2 = True
                                '    End If
                            ElseIf (LastRowMinTime + RowTotalMinTime) <= 120 Then
                                Period1 = (LastPeriod1 + RowTotalTime)
                                Period2 = LastPeriod2
                                SameDayTotalMinTime = SameDayTotalMinTime + RowTotalMinTime
                                If Period1 < 2 Then
                                    FlagPeriod1 = True
                                    FlagPeriod2 = False
                                Else
                                    FlagPeriod1 = False
                                    FlagPeriod2 = True
                                End If
                            End If
                        End If
                        LastTxnID = RowTxnID
                        LastDate = RowDate
                        LastRowTotalTime = RowTotalTime
                        LastRowMinTime = RowTotalMinTime
                        LastPeriod1 = Period1
                        LastPeriod2 = Period2
                        LastPeriod3 = Period3
                    ElseIf HolidayOrNot = "1" Then    '假日
                        If LastDate <> RowDate Then    '從頭算(重新來過)
                            'If RowTotalTime <= 12 Then
                            Period3 = RowTotalTime
                            FlagPeriod3 = True
                            'End If
                        ElseIf LastDate = RowDate Then      '繼續算(繼續算下一筆)
                            'If LastPeriod3 + RowTotalTime > 12 Then
                            '    outMsg = "累積時數超過12小時!"
                            '    execResult = False
                            'If LastPeriod3 + RowTotalTime <= 12 Then
                            Period3 = LastPeriod3 + RowTotalTime
                            FlagPeriod3 = True
                            'End If
                        End If
                        LastTxnID = RowTxnID
                        LastDate = RowDate
                        LastRowTotalTime = RowTotalTime
                        LastRowMinTime = RowTotalMinTime
                        LastPeriod1 = Period1
                        LastPeriod2 = Period2
                        LastPeriod3 = Period3
                    End If
                End If
            ElseIf LastTxnID = RowTxnID Then     '跨日
                Dim SumMinTime As Double = LastRowMinTime + RowTotalMinTime   '計算當筆加班單總"分鐘"數
                Dim SumHrTime As Double = (Math.Round(SumMinTime / 6, MidpointRounding.AwayFromZero)) / 10   '計算當筆加班單總"小時"數
                RowTotalTime = SumHrTime - LastRowTotalTime     '第二天的時數 = 總時數 - 第一天的時數
                If LastPeriod1 >= 2 AndAlso LastPeriod2 >= 2 Then
                    FlagPeriod1 = False
                    FlagPeriod2 = True
                End If
                If RowDate = DateB Then
                    If HolidayOrNot = "0" Then       '起日是平日
                        'If FlagPeriod1 = True Then       '從時段一開始計算
                        '    If RowTotalTime > 4 * 60 Then
                        '        outMsg = "累積時數超過4小時!"
                        '        execResult = False
                        '    ElseIf RowTotalTime <= 4 * 60 AndAlso RowTotalTime > 2 * 60 Then
                        '        Period1 = 2 * 60
                        '        Period2 = RowTotalTime - 2 * 60
                        '    ElseIf RowTotalTime <= 2 * 60 Then
                        '        Period1 = RowTotalTime
                        '    End If
                        'ElseIf FlagPeriod2 = True Then       '從時段二開始計算
                        '    If RowTotalTime > 4 * 60 Then
                        '        outMsg = "累積時數超過4小時!"
                        '        execResult = False
                        '    ElseIf RowTotalTime <= 4 * 60 AndAlso RowTotalTime > 2 * 60 Then
                        '        Period2 = 2 * 60
                        '        Period1 = RowTotalTime - 2 * 60
                        '    ElseIf RowTotalTime <= 2 * 60 Then
                        '        Period2 = RowTotalTime
                        '    End If
                        'ElseIf FlagPeriod3 = True Then       '從時段三開始計算
                        '    If LastPeriod3 >= 2 * 60 Then     '前筆的時段三大於2從時段二補
                        '        If RowTotalTime <= 4 * 60 AndAlso RowTotalTime > 2 * 60 Then
                        '            Period2 = 2 * 60
                        '            Period1 = RowTotalTime - 2 * 60
                        '        ElseIf RowTotalTime <= 2 * 60 Then
                        '            Period2 = RowTotalTime
                        '        End If
                        '    ElseIf LastPeriod3 < 2 * 60 Then     '前筆的時段三小於2從時段一補        
                        '        If (RowTotalTime <= 4 * 60 AndAlso RowTotalTime > 2 * 60) Then
                        '            Period1 = 2 * 60 - LastPeriod3
                        '            Period2 = (LastPeriod3 + RowTotalTime) - 2 * 60
                        '        ElseIf RowTotalTime <= 2 * 60 Then
                        '            If (RowTotalTime > (2 * 60 - LastPeriod3)) Then
                        '                Period1 = 2 * 60 - LastPeriod3
                        '                Period2 = (LastPeriod3 + RowTotalTime) - 2 * 60
                        '            Else
                        '                Period1 = RowTotalTime
                        '            End If
                        '        End If
                        '    End If
                        'End If
                        If LastRowTotalTime = 0 Then       '上一筆加班時數等於0
                            'If RowTotalTime > 4 Then
                            '    outMsg = "累積時數超過4小時!"
                            '    execResult = False
                            If RowTotalTime > 4 Then
                                Period1 = 2
                                Period2 = RowTotalTime - 2
                                FlagPeriod1 = False
                                FlagPeriod2 = True
                            ElseIf RowTotalTime <= 4 AndAlso RowTotalTime > 2 Then
                                Period1 = 2
                                Period2 = RowTotalTime - 2
                                FlagPeriod1 = False
                                FlagPeriod2 = True
                            ElseIf RowTotalTime <= 2 Then
                                Period1 = RowTotalTime
                                If Period1 < 2 Then
                                    FlagPeriod1 = True
                                    FlagPeriod2 = False
                                Else
                                    FlagPeriod1 = False
                                    FlagPeriod2 = True
                                End If
                            End If
                        Else
                            If FlagPeriod1 = True Then       '從時段一開始計算
                                If RowTotalTime > 4 Then
                                    If RowTotalTime + LastPeriod1 > 6 Then
                                        Period1 = 2
                                        Period2 = RowTotalTime - 2
                                    End If
                                ElseIf RowTotalTime <= 2 Then
                                    If RowTotalTime + LastPeriod1 <= 4 AndAlso RowTotalTime + LastPeriod1 > 2 Then
                                        Period1 = 2 - LastPeriod1
                                        Period2 = RowTotalTime - Period1
                                    ElseIf RowTotalTime + LastPeriod1 <= 2 Then
                                        Period1 = RowTotalTime
                                    End If
                                ElseIf RowTotalTime <= 4 AndAlso RowTotalTime > 2 Then
                                    If RowTotalTime + LastPeriod1 <= 6 AndAlso RowTotalTime + LastPeriod1 > 4 Then
                                        Period1 = 2 - LastPeriod1
                                        Period2 = RowTotalTime - Period1
                                        If Period2 <= 4 AndAlso Period2 > 2 Then
                                            Period2 = 2
                                            Period1 = RowTotalTime - Period2
                                        End If
                                    ElseIf RowTotalTime + LastPeriod1 <= 4 AndAlso RowTotalTime + LastPeriod1 > 2 Then
                                        Period1 = 2 - LastPeriod1
                                        Period2 = RowTotalTime - Period1
                                    ElseIf RowTotalTime + LastPeriod1 <= 2 Then
                                        Period1 = 2 - LastPeriod1
                                        Period2 = RowTotalTime - Period1
                                    End If
                                ElseIf RowTotalTime <= 2 Then
                                    If RowTotalTime + LastPeriod1 <= 4 AndAlso RowTotalTime + LastPeriod1 > 2 Then
                                        Period1 = 2 - LastPeriod1
                                        Period2 = RowTotalTime - Period1
                                    ElseIf RowTotalTime + LastPeriod1 <= 2 Then
                                        Period1 = RowTotalTime
                                    End If
                                End If
                            ElseIf FlagPeriod2 = True Then '從時段二開始計算
                                If RowTotalTime > 4 Then
                                    Period1 = 2
                                    Period2 = RowTotalTime - 2
                                    FlagPeriod1 = False
                                    FlagPeriod2 = True
                                ElseIf (RowTotalTime <= 4 AndAlso RowTotalTime > 2) Then
                                    If RowTotalTime + LastPeriod2 <= 6 AndAlso RowTotalTime + LastPeriod2 > 4 Then
                                        Period2 = 2 '- LastPeriod2
                                        Period1 = RowTotalTime - Period2
                                        If Period1 <= 4 AndAlso Period1 > 2 Then
                                            Period1 = 2
                                            Period2 = RowTotalTime - Period2
                                        End If
                                    ElseIf RowTotalTime + LastPeriod2 <= 4 AndAlso RowTotalTime + LastPeriod2 > 2 Then
                                        Period2 = 2 ' -LastPeriod2;
                                        Period1 = RowTotalTime - Period2
                                    ElseIf RowTotalTime + LastPeriod2 <= 2 Then
                                        Period2 = RowTotalTime
                                        'Period1 = RowTotalTime - Period2
                                    End If
                                ElseIf RowTotalTime <= 2 Then
                                    Period2 = RowTotalTime
                                End If
                            ElseIf FlagPeriod3 = True Then '從時段三開始計算
                                If LastPeriod3 >= 2 Then    '前筆的時段三大於2從時段二補
                                    If RowTotalTime <= 4 AndAlso RowTotalTime > 2 Then
                                        Period2 = 2
                                        Period1 = RowTotalTime - 2
                                    ElseIf RowTotalTime <= 2 Then
                                        Period2 = RowTotalTime
                                    End If
                                ElseIf LastPeriod3 < 2 Then '前筆的時段三小於2從時段一補
                                    If RowTotalTime <= 4 AndAlso RowTotalTime > 2 Then
                                        Period1 = 2 - LastPeriod3
                                        Period2 = (LastPeriod3 + RowTotalTime) - 2
                                    ElseIf RowTotalTime <= 2 Then
                                        If (RowTotalTime > (2 - LastPeriod3)) Then
                                            Period1 = 2 - LastPeriod3
                                            Period2 = (LastPeriod3 + RowTotalTime) - 2
                                        Else
                                            Period1 = RowTotalTime
                                        End If
                                    End If
                                End If
                            End If
                        End If
                        LastTxnID = RowTxnID
                        LastDate = RowDate
                        LastRowTotalTime = RowTotalTime
                        LastPeriod1 = Period1
                        LastPeriod2 = Period2
                        LastPeriod3 = Period3
                        StartDatePeriod1 = LastPeriod1
                        StartDatePeriod2 = LastPeriod2
                        StartDatePeriod3 = LastPeriod3
                    ElseIf HolidayOrNot = "1" Then      '起日是假日
                        Period3 = RowTotalTime
                        LastTxnID = RowTxnID
                        LastDate = RowDate
                        LastRowTotalTime = RowTotalTime
                        LastPeriod1 = Period1
                        LastPeriod2 = Period2
                        LastPeriod3 = Period3
                        StartDatePeriod1 = LastPeriod1
                        StartDatePeriod2 = LastPeriod2
                        StartDatePeriod3 = LastPeriod3
                    End If
                ElseIf RowDate = DateE Then      '迄日
                    If HolidayOrNot = "0" Then   '迄日是平日
                        If LastRowTotalTime = 0 Then    '上一筆加班時數等於0
                            If RowTotalTime > 4 Then
                                Period1 = 2
                                Period2 = RowTotalTime - 2
                                FlagPeriod1 = False
                                FlagPeriod2 = True
                            ElseIf (RowTotalTime <= 4 AndAlso RowTotalTime > 2) Then
                                Period1 = 2
                                Period2 = RowTotalTime - 2
                                FlagPeriod1 = False
                                FlagPeriod2 = True
                            ElseIf RowTotalTime <= 2 Then
                                Period1 = RowTotalTime
                                If Period1 < 2 Then
                                    FlagPeriod1 = True
                                    FlagPeriod2 = False
                                Else
                                    FlagPeriod1 = False
                                    FlagPeriod2 = True
                                End If
                            End If
                        Else
                            If FlagPeriod1 = True Then       '從時段一開始計算
                                If RowTotalTime > 4 Then
                                    Period1 = 2
                                    Period2 = RowTotalTime - 2
                                    FlagPeriod1 = False
                                    FlagPeriod2 = True
                                ElseIf (RowTotalTime <= 4 AndAlso RowTotalTime > 2) Then
                                    If RowTotalTime + LastPeriod1 > 6 Then
                                        Period1 = 2
                                        Period2 = RowTotalTime - 2
                                        FlagPeriod1 = False
                                        FlagPeriod2 = True
                                    ElseIf RowTotalTime + LastPeriod1 > 6 AndAlso RowTotalTime + LastPeriod1 > 4 Then
                                        Period1 = 2 - LastPeriod1
                                        Period2 = RowTotalTime - Period1
                                        If Period2 <= 4 AndAlso Period2 > 2 Then
                                            Period2 = 2
                                            Period1 = RowTotalTime - Period2
                                        End If
                                    ElseIf RowTotalTime + LastPeriod1 <= 4 AndAlso RowTotalTime + LastPeriod1 > 2 Then
                                        Period1 = 2 - LastPeriod1
                                        Period2 = RowTotalTime - Period1
                                    ElseIf RowTotalTime + LastPeriod1 <= 2 Then
                                        Period1 = 2 - LastPeriod1
                                        Period2 = RowTotalTime - Period1
                                    End If
                                ElseIf RowTotalTime <= 2 Then
                                    If RowTotalTime + LastPeriod1 <= 4 AndAlso RowTotalTime + LastPeriod1 > 2 Then
                                        Period1 = 2 - LastPeriod1
                                        Period2 = RowTotalTime - Period1
                                    ElseIf RowTotalTime + LastPeriod1 <= 2 Then
                                        Period1 = RowTotalTime
                                    End If
                                End If
                            ElseIf FlagPeriod2 = True Then       '從時段二開始計算
                                If RowTotalTime > 4 Then
                                    Period1 = 2
                                    Period2 = RowTotalTime - 2
                                    FlagPeriod1 = False
                                    FlagPeriod2 = True
                                ElseIf RowTotalTime <= 4 AndAlso RowTotalTime > 2 Then
                                    If RowTotalTime + LastPeriod2 > 6 Then
                                        Period1 = RowTotalTime - 2
                                        Period2 = 2
                                        FlagPeriod1 = False
                                        FlagPeriod2 = True
                                    ElseIf RowTotalTime + LastPeriod2 <= 6 AndAlso RowTotalTime + LastPeriod2 > 4 Then
                                        Period2 = 2 '-LastPeriod2
                                        Period1 = RowTotalTime - Period2
                                        If Period1 <= 4 AndAlso Period1 > 2 Then
                                            Period1 = 2
                                            Period2 = RowTotalTime - Period2
                                        End If
                                    ElseIf RowTotalTime + LastPeriod2 <= 4 AndAlso RowTotalTime + LastPeriod2 > 2 Then
                                        Period2 = 2    '-LastPeriod2
                                        Period1 = RowTotalTime - Period2
                                    ElseIf RowTotalTime + LastPeriod2 <= 2 Then
                                        Period2 = RowTotalTime
                                        'Period1 = RowTotalTime - Period2
                                    End If
                                ElseIf RowTotalTime <= 2 Then
                                    Period2 = RowTotalTime
                                    'Period1 = RowTotalTime - Period2
                                End If
                            ElseIf FlagPeriod3 = True Then       '從時段三開始計算
                                If LastPeriod3 >= 2 Then       '前筆的時段三大於2從時段二補
                                    If RowTotalTime > 4 Then
                                        Period1 = 2
                                        Period2 = RowTotalTime - 2
                                        FlagPeriod1 = False
                                        FlagPeriod2 = True
                                    ElseIf RowTotalTime <= 4 AndAlso RowTotalTime > 2 Then
                                        Period2 = 2
                                        Period1 = RowTotalTime - 2
                                    ElseIf RowTotalTime <= 2 Then
                                        Period2 = RowTotalTime
                                    End If
                                ElseIf LastPeriod3 < 2 Then     '前筆的時段三小於2從時段一補
                                    If RowTotalTime > 4 Then
                                        Period1 = 2
                                        Period2 = RowTotalTime - 2
                                        FlagPeriod1 = False
                                        FlagPeriod2 = True
                                    ElseIf (RowTotalTime <= 4 AndAlso RowTotalTime > 2) Then
                                        Period1 = 2 - LastPeriod3
                                        Period2 = (LastPeriod3 + RowTotalTime) - 2
                                    ElseIf RowTotalTime <= 2 Then
                                        If (RowTotalTime > (2 - LastPeriod3)) Then
                                            Period1 = 2 - LastPeriod3
                                            Period2 = (LastPeriod3 + RowTotalTime) - 2
                                        Else
                                            Period1 = RowTotalTime
                                        End If
                                    End If
                                End If
                            End If
                        End If
                        LastTxnID = RowTxnID
                        LastDate = RowDate
                        LastRowTotalTime = RowTotalTime
                        LastPeriod1 = Period1
                        LastPeriod2 = Period2
                        LastPeriod3 = Period3
                        EndDatePeriod1 = LastPeriod1
                        EndDatePeriod2 = LastPeriod2
                        EndDatePeriod3 = LastPeriod3
                    ElseIf HolidayOrNot = "1" Then     '迄日是假日
                        Period3 = RowTotalTime
                        LastTxnID = RowTxnID
                        LastDate = RowDate
                        LastRowTotalTime = RowTotalTime
                        LastPeriod1 = Period1
                        LastPeriod2 = Period2
                        LastPeriod3 = Period3
                        EndDatePeriod1 = LastPeriod1
                        EndDatePeriod2 = LastPeriod2
                        EndDatePeriod3 = LastPeriod3
                    End If
                ElseIf (RowDate <> DateB) AndAlso (RowDate <> DateE) Then      '一般情況
                    If HolidayOrNot = "0" Then    '平日
                        'If FlagPeriod1 = True Then        '從時段一開始計算
                        '    If (RowTotalTime > 4 * 60) Then
                        '        outMsg = "累積時數超過4小時!"
                        '        execResult = False
                        '    ElseIf (RowTotalTime <= 4 * 60 AndAlso RowTotalTime > 2 * 60) Then
                        '        Period1 = 2 * 60
                        '        Period2 = RowTotalTime - 2 * 60
                        '    ElseIf RowTotalTime <= 2 * 60 Then
                        '        Period1 = RowTotalTime
                        '    End If
                        'ElseIf FlagPeriod2 = True Then     '從時段二開始計算
                        '    If RowTotalTime > 4 * 60 Then
                        '        outMsg = "累積時數超過4小時!"
                        '        execResult = False
                        '    ElseIf (RowTotalTime <= 4 * 60 AndAlso RowTotalTime > 2 * 60) Then
                        '        Period2 = 2 * 60
                        '        Period1 = RowTotalTime - 2 * 60
                        '    ElseIf RowTotalTime <= 2 * 60 Then
                        '        Period2 = RowTotalTime
                        '    End If
                        'ElseIf FlagPeriod3 = True Then        '從時段三開始計算
                        '    If LastPeriod3 >= 2 * 60 Then       '前筆的時段三大於2從時段二補
                        '        If (RowTotalTime <= 4 * 60 AndAlso RowTotalTime > 2 * 60) Then
                        '            Period2 = 2 * 60
                        '            Period1 = RowTotalTime - 2 * 60
                        '        ElseIf RowTotalTime <= 2 * 60 Then
                        '            Period2 = RowTotalTime
                        '        End If
                        '    ElseIf LastPeriod3 < 2 * 60 Then    '前筆的時段三小於2從時段一補
                        '        If (RowTotalTime <= 4 * 60 AndAlso RowTotalTime > 2 * 60) Then
                        '            Period1 = 2 * 60 - LastPeriod3
                        '            Period2 = (LastPeriod3 + RowTotalTime) - 2 * 60
                        '        ElseIf RowTotalTime <= 2 * 60 Then
                        '            If (RowTotalTime > (2 * 60 - LastPeriod3)) Then
                        '                Period1 = 2 * 60 - LastPeriod3
                        '                Period2 = (LastPeriod3 + RowTotalTime) - 2 * 60
                        '            Else
                        '                Period1 = RowTotalTime
                        '            End If
                        '        End If
                        '    End If
                        'End If
                        If LastRowTotalTime = 0 Then    '上一筆加班時數等於0
                            If RowTotalTime > 4 Then
                                Period1 = 2
                                Period2 = RowTotalTime - 2
                                FlagPeriod1 = False
                                FlagPeriod2 = True
                            ElseIf RowTotalTime <= 4 AndAlso RowTotalTime > 2 Then
                                Period1 = 2
                                Period2 = RowTotalTime - 2
                                FlagPeriod1 = False
                                FlagPeriod2 = True
                            ElseIf RowTotalTime <= 2 Then
                                Period1 = RowTotalTime
                                If Period1 < 2 Then
                                    FlagPeriod1 = True
                                    FlagPeriod2 = False
                                Else
                                    FlagPeriod1 = False
                                    FlagPeriod2 = True
                                End If
                            End If
                        Else
                            If FlagPeriod1 = True Then        '從時段一開始計算
                                If (RowTotalTime > 4) Then
                                    Period1 = 2
                                    Period2 = RowTotalTime - 2
                                    FlagPeriod1 = False
                                    FlagPeriod2 = True
                                ElseIf (RowTotalTime <= 4 AndAlso RowTotalTime > 2) Then
                                    If RowTotalTime + LastPeriod1 <= 6 AndAlso RowTotalTime + LastPeriod1 > 4 Then
                                        Period1 = 2 - LastPeriod1
                                        Period2 = RowTotalTime - Period1
                                        If Period2 <= 4 AndAlso Period2 > 2 Then
                                            Period2 = 2
                                            Period1 = RowTotalTime - Period2
                                        End If
                                    ElseIf RowTotalTime + LastPeriod1 <= 4 AndAlso RowTotalTime + LastPeriod1 > 2 Then
                                        Period1 = 2 - LastPeriod1
                                        Period2 = RowTotalTime - Period1
                                    ElseIf RowTotalTime + LastPeriod1 <= 2 Then
                                        Period1 = 2 - LastPeriod1
                                        Period2 = RowTotalTime - Period1
                                    End If
                                ElseIf RowTotalTime <= 2 Then
                                    If (RowTotalTime + LastPeriod1 <= 4 AndAlso RowTotalTime + LastPeriod1 > 2) Then
                                        Period1 = 2 - LastPeriod1
                                        Period2 = RowTotalTime - Period1
                                    ElseIf (RowTotalTime + LastPeriod1 <= 2) Then
                                        Period1 = RowTotalTime
                                    End If
                                End If
                            ElseIf FlagPeriod2 = True Then     '從時段二開始計算
                                If RowTotalTime > 4 Then
                                    Period1 = 2
                                    Period2 = RowTotalTime - 2
                                    FlagPeriod1 = False
                                    FlagPeriod2 = True
                                ElseIf (RowTotalTime <= 4 AndAlso RowTotalTime > 2) Then
                                    If RowTotalTime + LastPeriod2 <= 6 AndAlso RowTotalTime + LastPeriod2 > 4 Then
                                        Period2 = 2     '- LastPeriod2
                                        Period1 = RowTotalTime - Period2
                                        If Period1 <= 4 AndAlso Period1 > 2 Then
                                            Period1 = 2
                                            Period2 = RowTotalTime - Period2
                                        End If
                                    ElseIf RowTotalTime + LastPeriod2 <= 4 AndAlso RowTotalTime + LastPeriod2 > 2 Then
                                        Period2 = 2     '- LastPeriod2
                                        Period1 = RowTotalTime - Period2
                                    ElseIf RowTotalTime + LastPeriod2 <= 2 Then
                                        Period2 = RowTotalTime
                                        'Period1 = RowTotalTime - Period2
                                    End If
                                ElseIf RowTotalTime <= 2 Then
                                    Period2 = RowTotalTime
                                End If
                            ElseIf FlagPeriod3 = True Then        '從時段三開始計算
                                If LastPeriod3 >= 2 Then       '前筆的時段三大於2從時段二補
                                    If (RowTotalTime <= 4 AndAlso RowTotalTime > 2) Then
                                        Period2 = 2
                                        Period1 = RowTotalTime - 2
                                    ElseIf RowTotalTime <= 2 Then
                                        Period2 = RowTotalTime
                                    End If
                                ElseIf LastPeriod3 < 2 Then    '前筆的時段三小於2從時段一補
                                    If (RowTotalTime <= 4 AndAlso RowTotalTime > 2) Then
                                        Period1 = 2 - LastPeriod3
                                        Period2 = (LastPeriod3 + RowTotalTime) - 2
                                    ElseIf RowTotalTime <= 2 Then
                                        If (RowTotalTime > (2 - LastPeriod3)) Then
                                            Period1 = 2 - LastPeriod3
                                            Period2 = (LastPeriod3 + RowTotalTime) - 2
                                        Else
                                            Period1 = RowTotalTime
                                        End If
                                    End If
                                End If
                            End If
                        End If
                        LastTxnID = RowTxnID
                        LastDate = RowDate
                        LastRowTotalTime = RowTotalTime
                        LastPeriod1 = Period1
                        LastPeriod2 = Period2
                        LastPeriod3 = Period3
                    ElseIf HolidayOrNot = "1" Then      '假日
                        Period3 = RowTotalTime
                        LastTxnID = RowTxnID
                        LastDate = RowDate
                        LastRowTotalTime = RowTotalTime
                        LastPeriod1 = Period1
                        LastPeriod2 = Period2
                        LastPeriod3 = Period3
                    End If
                End If
            End If
        Next
        Dim strOTDateStart = startDate(0) + "," + Convert.ToDouble(StartDatePeriod1).ToString("0.0") + "," + Convert.ToDouble(StartDatePeriod2).ToString("0.0") + "," + Convert.ToDouble(StartDatePeriod3).ToString("0.0")
        Dim strOTDateEnd = endDate(0) + "," + Convert.ToDouble(EndDatePeriod1).ToString("0.0") + "," + Convert.ToDouble(EndDatePeriod2).ToString("0.0") + "," + Convert.ToDouble(EndDatePeriod3).ToString("0.0")
        If execResult = True Then
            Result = strOTDateStart + ";" + strOTDateEnd
            reMsg = Result
        Else
            reMsg = outMsg
        End If
        Return execResult
    End Function

#End Region

#End Region

#Region "舊的時段計算"
    ''' <summary>
    ''' 時段計算(單日、跨日共用)
    ''' </summary>
    ''' <param name="table">資料表名稱</param>
    ''' <param name="strOTEmpID">加班人員工編號</param>
    ''' <param name="cntStart">起日TotalTime</param>
    ''' <param name="cntEnd">迄日TotalTime</param>
    ''' <param name="startDate">起日Arr</param>
    ''' <param name="endDate">迄日Arr</param>
    ''' <param name="MealTime">用餐時間</param>
    ''' <param name="MealFlag">用餐註記</param>
    ''' <param name="ottxnid">單號</param>
    ''' <param name="reMsg">回傳訊息</param>
    ''' <returns>若回傳為true，則顯示加班時段，否則顯示錯誤訊息</returns>
    ''' <remarks>
    ''' startDate(0)=StartDate
    ''' startDate(1)=StartbeginTime
    ''' startDate(2)=StartendTime
    ''' EndDate(0)=EndDate
    ''' EndDate(1)=EndbeginTime
    ''' EndDate(2)=EndendTime
    ''' </remarks>
    'Public Function PeriodCount(ByVal table As String, ByVal strOTEmpID As String, ByVal cntStart As Double, ByVal cntEnd As Double, ByVal startDate As ArrayList, ByVal endDate As ArrayList, ByVal MealTime As Double, ByVal MealFlag As String, ByVal ottxnid As String, ByRef reMsg As String) As Boolean
    '    Dim strSQL As New StringBuilder()

    '    '跨日的時段計算
    '    Dim execResult As Boolean = True
    '    Dim Result As String = ""
    '    Dim outMsg As String = ""

    '    'TimePeriodBegin
    '    Dim StartDatePeriod1 As Double = 0.0
    '    Dim StartDatePeriod2 As Double = 0.0
    '    Dim StartDatePeriod3 As Double = 0.0
    '    Dim DateB As String = startDate(0)

    '    'TimePeriodEnd
    '    Dim EndDatePeriod1 As Double = 0.0
    '    Dim EndDatePeriod2 As Double = 0.0
    '    Dim EndDatePeriod3 As Double = 0.0
    '    Dim DateE As String = endDate(0)

    '    If "1900/01/01".Equals(DateE) Then
    '        'TimePeriodEnd
    '        EndDatePeriod1 = 0.0
    '        EndDatePeriod2 = 0.0
    '        EndDatePeriod3 = 0.0
    '        DateE = startDate(0)
    '        cntEnd = cntStart
    '    Else
    '        'TimePeriodBegin
    '        StartDatePeriod1 = 0.0
    '        StartDatePeriod2 = 0.0
    '        StartDatePeriod3 = 0.0
    '        DateB = startDate(0)

    '        'TimePeriodEnd
    '        EndDatePeriod1 = 0.0
    '        EndDatePeriod2 = 0.0
    '        EndDatePeriod3 = 0.0
    '        DateE = endDate(0)
    '    End If

    '    Dim LastPeriod1 As Double = 0.0
    '    Dim LastPeriod2 As Double = 0.0
    '    Dim LastPeriod3 As Double = 0.0
    '    Dim LastDate As String = ""
    '    Dim LastTxnID As String = ""

    '    Dim StartDateHo As String = ""
    '    Dim EndDateHo As String = ""

    '    Dim FlagPeriod1 As Boolean = False
    '    Dim FlagPeriod2 As Boolean = False
    '    Dim FlagPeriod3 As Boolean = False

    '    '畫面是否為假日
    '    If "1900/01/01".Equals(endDate(0)) Then
    '        Using dtStartHo As DataTable = QueryData("HolidayOrNot", _eHRMSDB_ITRD + ".[dbo].[Calendar]", " AND CompID =" + Bsp.Utility.Quote(UserProfile.SelectCompRoleID.ToString().Trim) + " AND CONVERT(CHAR(10),SysDate, 111) =" + Bsp.Utility.Quote(startDate(0)))
    '            If dtStartHo.Rows(0).Item("HolidayOrNot").ToString() = "0" Then
    '                StartDateHo = "0"
    '            Else
    '                StartDateHo = "1"
    '            End If
    '        End Using
    '    Else
    '        Using dtStartHo As DataTable = QueryData("HolidayOrNot", _eHRMSDB_ITRD + ".[dbo].[Calendar]", " AND CompID =" + Bsp.Utility.Quote(UserProfile.SelectCompRoleID.ToString().Trim) + " AND CONVERT(CHAR(10),SysDate, 111) =" + Bsp.Utility.Quote(startDate(0)))
    '            If dtStartHo.Rows(0).Item("HolidayOrNot").ToString() = "0" Then
    '                StartDateHo = "0"
    '            Else
    '                StartDateHo = "1"
    '            End If
    '        End Using

    '        Using dtEndHo As DataTable = QueryData("HolidayOrNot", _eHRMSDB_ITRD + ".[dbo].[Calendar]", " AND CompID =" + Bsp.Utility.Quote(UserProfile.SelectCompRoleID.ToString().Trim) + " AND CONVERT(CHAR(10),SysDate, 111) =" + Bsp.Utility.Quote(endDate(0)))
    '            If dtEndHo.Rows(0).Item("HolidayOrNot").ToString() = "0" Then
    '                EndDateHo = "0"
    '            Else
    '                EndDateHo = "1"
    '            End If
    '        End Using
    '    End If
    '    '計算吃飯時間
    '    If "1900/01/01".Equals(endDate(0)) Then
    '        If MealFlag = "1" Then
    '            '如果有要扣吃飯時間
    '            cntStart = cntStart - MealTime
    '        End If
    '        'cntStart = (Math.Round(cntStart / 6, 1, MidpointRounding.AwayFromZero)) / 10     '轉小時
    '    Else
    '        If MealFlag = "1" Then
    '            '如果有要扣吃飯時間
    '            cntStart = cntStart - MealTime
    '            If cntStart < 0.0 Then
    '                cntEnd = cntEnd + cntStart
    '                cntStart = 0.0
    '            End If
    '        End If
    '        'cntStart = (Math.Round(cntStart / 6, 1, MidpointRounding.AwayFromZero)) / 10     '轉小時
    '        'cntEnd = (Math.Round(cntStart / 6, 1, MidpointRounding.AwayFromZero)) / 10     '轉小時
    '    End If

    '    Dim OverTimeDatas As DataTable
    '    '撈出正確的資料
    '    If "1900/01/01".Equals(endDate(0)) Then
    '        OverTimeDatas = QueryData("OTStartDate,OTEndDate,OTTxnID,OTStartTime,OTEndTime,OTTotalTime,MealFlag,MealTime,HolidayOrNot", table, "AND MONTH(CONVERT(DATETIME, OTStartDate))=MONTH(CONVERT(DATETIME, " + Bsp.Utility.Quote(startDate(0)) + ")) AND OTStartDate <= " + Bsp.Utility.Quote(startDate(0)) + " AND OTCompID= " + Bsp.Utility.Quote(UserProfile.SelectCompRoleID.ToString().Trim) + " AND OTEmpID=" + Bsp.Utility.Quote(strOTEmpID) + " AND OTStatus IN ('2', '3') AND NOT(OTTxnID= " + Bsp.Utility.Quote(ottxnid) + ") ORDER BY OTStartDate, OTStartTime ")
    '    Else
    '        OverTimeDatas = QueryData("OTStartDate,OTEndDate,OTTxnID,OTStartTime,OTEndTime,OTTotalTime,MealFlag,MealTime,HolidayOrNot", table, "AND MONTH(CONVERT(DATETIME, OTStartDate))=MONTH(CONVERT(DATETIME, " + Bsp.Utility.Quote(endDate(0)) + ")) AND OTStartDate <= " + Bsp.Utility.Quote(endDate(0)) + " AND OTCompID= " + Bsp.Utility.Quote(UserProfile.SelectCompRoleID.ToString().Trim) + " AND OTEmpID=" + Bsp.Utility.Quote(strOTEmpID) + " AND OTStatus IN ('2', '3') AND NOT(OTTxnID= " + Bsp.Utility.Quote(ottxnid) + ") ORDER BY OTStartDate, OTStartTime ")
    '    End If

    '    If "1900/01/01".Equals(endDate(0)) Then
    '        Dim EndDateViewData As DataRow = OverTimeDatas.NewRow()
    '        EndDateViewData.Item("OTStartDate") = startDate(0)
    '        EndDateViewData.Item("OTEndDate") = startDate(0)
    '        EndDateViewData.Item("OTTxnID") = "ViewData"
    '        EndDateViewData.Item("OTStartTime") = startDate(1).ToString()
    '        EndDateViewData.Item("OTEndTime") = startDate(2).ToString()
    '        EndDateViewData.Item("OTTotalTime") = cntStart
    '        EndDateViewData.Item("MealFlag") = "0"  'MealFlag
    '        EndDateViewData.Item("MealTime") = 0     'MealTime
    '        EndDateViewData.Item("HolidayOrNot") = StartDateHo
    '        OverTimeDatas.Rows.Add(EndDateViewData)
    '    Else
    '        Dim StartDateViewData As DataRow = OverTimeDatas.NewRow()
    '        StartDateViewData.Item("OTStartDate") = startDate(0)
    '        StartDateViewData.Item("OTEndDate") = startDate(0)
    '        StartDateViewData.Item("OTTxnID") = "ViewData"
    '        StartDateViewData.Item("OTStartTime") = startDate(1).ToString()
    '        StartDateViewData.Item("OTEndTime") = startDate(2).ToString()
    '        StartDateViewData.Item("OTTotalTime") = cntStart
    '        StartDateViewData.Item("MealFlag") = "0"    'MealFlag
    '        StartDateViewData.Item("MealTime") = 0    'MealTime
    '        StartDateViewData.Item("HolidayOrNot") = StartDateHo
    '        OverTimeDatas.Rows.Add(StartDateViewData)

    '        Dim EndDateViewData As DataRow = OverTimeDatas.NewRow()
    '        EndDateViewData.Item("OTStartDate") = endDate(0)
    '        EndDateViewData.Item("OTEndDate") = endDate(0)
    '        EndDateViewData.Item("OTTxnID") = "ViewData"
    '        EndDateViewData.Item("OTStartTime") = endDate(1).ToString()
    '        EndDateViewData.Item("OTEndTime") = endDate(2).ToString()
    '        EndDateViewData.Item("OTTotalTime") = cntEnd
    '        EndDateViewData.Item("MealFlag") = "0"
    '        EndDateViewData.Item("MealTime") = 0
    '        EndDateViewData.Item("HolidayOrNot") = EndDateHo
    '        OverTimeDatas.Rows.Add(EndDateViewData)
    '    End If

    '    OverTimeDatas.DefaultView.Sort = "OTStartDate ASC, OTStartTime ASC"
    '    OverTimeDatas = OverTimeDatas.DefaultView.ToTable()
    '    Dim LastRowTotalTime As Double = 0
    '    Dim LastRowMinTime As Double = 0
    '    For i = 0 To (OverTimeDatas.Rows.Count - 1)
    '        Dim Period1 As Double = 0
    '        Dim Period2 As Double = 0
    '        Dim Period3 As Double = 0
    '        Dim RowTotalMinTime = Convert.ToDouble(OverTimeDatas.Rows(i).Item("OTTotalTime").ToString())       '轉分鐘
    '        Dim RowTotalTime As Double = 0

    '        Dim RowTxnID As String = OverTimeDatas.Rows(i).Item("OTTxnID").ToString()  '單號
    '        Dim RowDate As String = OverTimeDatas.Rows(i).Item("OTStartDate").ToString()
    '        Dim HolidayOrNot As String = OverTimeDatas.Rows(i).Item("HolidayOrNot").ToString()
    '        Dim RowMealFlag As String = OverTimeDatas.Rows(i).Item("MealFlag").ToString()
    '        Dim RowMealTime = Convert.ToDouble(OverTimeDatas.Rows(i).Item("MealTime").ToString())
    '        If RowMealFlag = "1" Then    '如果有要扣吃飯時間
    '            RowTotalMinTime = RowTotalMinTime - RowMealTime
    '        End If

    '        RowTotalTime = (Math.Round(RowTotalMinTime / 6, MidpointRounding.AwayFromZero)) / 10  '轉小時
    '        'RowTotalTime = CDbl(FormatNumber(RowTotalMinTime / 60, 1))
    '        '轉小時
    '        If LastTxnID <> RowTxnID Then      '單號不同
    '            If RowDate = DateB Then
    '                If HolidayOrNot = "0" Then   '起日是平日
    '                    If LastDate <> DateB Then   '從頭算(重新來過)
    '                        'If RowTotalTime > 4 Then
    '                        '    outMsg = "累積時數超過4小時!"
    '                        '    execResult = False
    '                        If RowTotalTime <= 4 AndAlso RowTotalTime > 2 Then
    '                            Period1 = 2
    '                            Period2 = RowTotalTime - 2
    '                            FlagPeriod1 = False
    '                            FlagPeriod2 = True
    '                        ElseIf RowTotalTime <= 2 Then
    '                            Period1 = RowTotalTime
    '                            If Period1 < 2 Then
    '                                FlagPeriod1 = True
    '                                FlagPeriod2 = False
    '                            Else
    '                                FlagPeriod1 = False
    '                                FlagPeriod2 = True
    '                            End If
    '                        End If
    '                    ElseIf LastDate = DateB Then    '繼續算(繼續算下一筆)
    '                        'If (LastPeriod1 + LastPeriod2 + RowTotalTime) > 4 Then
    '                        '    outMsg = "累積時數超過4小時!"
    '                        '    execResult = False
    '                        'ElseIf (LastPeriod1 + LastPeriod2 + RowTotalTime) < 4 AndAlso (LastPeriod1 + LastPeriod2 + RowTotalTime) > 2 Then
    '                        '    Period1 = 2
    '                        '    Period2 = (LastPeriod1 + LastPeriod2 + RowTotalTime) - 2
    '                        '    FlagPeriod1 = False
    '                        '    FlagPeriod2 = True
    '                        'ElseIf (LastPeriod1 + LastPeriod2 + RowTotalTime) <= 2 Then
    '                        '    Period1 = (LastPeriod1 + RowTotalTime)
    '                        '    Period2 = LastPeriod2
    '                        '    If Period1 < 2 Then
    '                        '        FlagPeriod1 = True
    '                        '        FlagPeriod2 = False
    '                        '    Else
    '                        '        FlagPeriod1 = False
    '                        '        FlagPeriod2 = True
    '                        '    End If
    '                        If (LastPeriod1 + LastPeriod2 + RowTotalTime) <= 4 AndAlso (LastPeriod1 + LastPeriod2 + RowTotalTime) > 2 Then
    '                            If LastPeriod1 + RowTotalTime < 2 Then
    '                                Period1 = LastPeriod1 + RowTotalTime
    '                                Period2 = LastPeriod2
    '                                FlagPeriod1 = True
    '                                FlagPeriod2 = False
    '                            Else
    '                                Period1 = 2
    '                                Period2 = (LastPeriod1 + LastPeriod2 + RowTotalTime) - 2
    '                                FlagPeriod1 = False
    '                                FlagPeriod2 = True
    '                            End If
    '                        ElseIf (LastPeriod1 + LastPeriod2 + RowTotalTime) <= 2 Then
    '                            Period1 = (LastPeriod1 + RowTotalTime)
    '                            If Period1 < 2 Then
    '                                Period2 = LastPeriod2
    '                                FlagPeriod1 = True
    '                                FlagPeriod2 = False
    '                            Else
    '                                FlagPeriod1 = False
    '                                FlagPeriod2 = True
    '                            End If
    '                        End If
    '                    End If
    '                    LastTxnID = RowTxnID
    '                    LastDate = RowDate
    '                    LastRowTotalTime = RowTotalTime
    '                    LastRowMinTime = RowTotalMinTime
    '                    LastPeriod1 = Period1
    '                    LastPeriod2 = Period2
    '                    LastPeriod3 = Period3
    '                    StartDatePeriod1 = LastPeriod1
    '                    StartDatePeriod2 = LastPeriod2
    '                    StartDatePeriod3 = LastPeriod3
    '                ElseIf HolidayOrNot = "1" Then '起日是假日
    '                    If LastDate <> DateB Then    '從頭算(重新來過)
    '                        If RowTotalTime <= 12 Then
    '                            Period3 = RowTotalTime
    '                            FlagPeriod3 = True
    '                        End If
    '                    ElseIf LastDate = DateB Then  '繼續算(繼續算下一筆)
    '                        'If LastPeriod3 + RowTotalTime > 12 Then
    '                        '    outMsg = "累積時數超過12小時!"
    '                        '    execResult = False
    '                        If LastPeriod3 + RowTotalTime <= 12 Then
    '                            Period3 = LastPeriod3 + RowTotalTime
    '                            FlagPeriod3 = True
    '                        End If
    '                    End If
    '                    LastTxnID = RowTxnID
    '                    LastDate = RowDate
    '                    LastRowTotalTime = RowTotalTime
    '                    LastRowMinTime = RowTotalMinTime
    '                    LastPeriod1 = Period1
    '                    LastPeriod2 = Period2
    '                    LastPeriod3 = Period3
    '                    StartDatePeriod1 = LastPeriod1
    '                    StartDatePeriod2 = LastPeriod2
    '                    StartDatePeriod3 = LastPeriod3
    '                End If
    '            ElseIf LastDate = DateE Then
    '                If HolidayOrNot = "0" Then      '迄日是平日
    '                    If LastDate <> DateE Then        '從頭算(重新來過)
    '                        'If RowTotalTime > 4 Then
    '                        '    outMsg = "累積時數超過4小時!"
    '                        '    execResult = False
    '                        If RowTotalTime <= 4 AndAlso RowTotalTime > 2 Then
    '                            Period1 = 2
    '                            Period2 = RowTotalTime - 2
    '                            FlagPeriod1 = False
    '                            FlagPeriod2 = True
    '                        ElseIf RowTotalTime <= 2 Then
    '                            Period1 = RowTotalTime
    '                            If Period1 < 2 Then
    '                                FlagPeriod1 = True
    '                                FlagPeriod2 = False
    '                            Else
    '                                FlagPeriod1 = False
    '                                FlagPeriod2 = True
    '                            End If
    '                        End If
    '                    ElseIf LastDate = DateE Then   '繼續算(繼續算下一筆)
    '                        'If (LastPeriod1 + LastPeriod2 + RowTotalTime) > 4 Then
    '                        '    outMsg = "累積時數超過4小時!"
    '                        '    execResult = False
    '                        If (LastPeriod1 + LastPeriod2 + RowTotalTime) <= 4 AndAlso (LastPeriod1 + LastPeriod2 + RowTotalTime) > 2 Then
    '                            If LastPeriod1 + RowTotalTime < 2 Then
    '                                Period1 = LastPeriod1 + RowTotalTime
    '                                Period2 = LastPeriod2
    '                                FlagPeriod1 = True
    '                                FlagPeriod2 = False
    '                            Else
    '                                Period1 = 2
    '                                Period2 = (LastPeriod1 + LastPeriod2 + RowTotalTime) - 2
    '                                FlagPeriod1 = False
    '                                FlagPeriod2 = True
    '                            End If
    '                        ElseIf (LastPeriod1 + LastPeriod2 + RowTotalTime) <= 2 Then
    '                            Period1 = (LastPeriod1 + RowTotalTime)
    '                            If Period1 < 2 Then
    '                                Period2 = LastPeriod2
    '                                FlagPeriod1 = True
    '                                FlagPeriod2 = False
    '                            Else
    '                                FlagPeriod1 = False
    '                                FlagPeriod2 = True
    '                            End If
    '                        End If
    '                    End If
    '                    LastTxnID = RowTxnID
    '                    LastDate = RowDate
    '                    LastRowTotalTime = RowTotalTime
    '                    LastRowMinTime = RowTotalMinTime
    '                    LastPeriod1 = Period1
    '                    LastPeriod2 = Period2
    '                    LastPeriod3 = Period3
    '                    EndDatePeriod1 = LastPeriod1
    '                    EndDatePeriod2 = LastPeriod2
    '                    EndDatePeriod3 = LastPeriod3
    '                ElseIf HolidayOrNot = "1" Then        '迄日是假日
    '                    If LastDate <> DateE Then    '從頭算(重新來過)
    '                        If RowTotalTime <= 12 Then
    '                            Period3 = RowTotalTime
    '                            FlagPeriod3 = True
    '                        End If
    '                    ElseIf LastDate = DateE Then       '繼續算(繼續算下一筆)
    '                        'If LastPeriod3 + RowTotalTime > 12 Then
    '                        '    outMsg = "累積時數超過12小時!"
    '                        '    execResult = False
    '                        If LastPeriod3 + RowTotalTime <= 12 Then
    '                            Period3 = LastPeriod3 + RowTotalTime
    '                            FlagPeriod3 = True
    '                        End If
    '                    End If
    '                    LastTxnID = RowTxnID
    '                    LastDate = RowDate
    '                    LastRowTotalTime = RowTotalTime
    '                    LastRowMinTime = RowTotalMinTime
    '                    LastPeriod1 = Period1
    '                    LastPeriod2 = Period2
    '                    LastPeriod3 = Period3
    '                    EndDatePeriod1 = LastPeriod1
    '                    EndDatePeriod2 = LastPeriod2
    '                    EndDatePeriod3 = LastPeriod3
    '                End If
    '            ElseIf RowDate <> DateB AndAlso RowDate <> DateE Then    '一般情況
    '                If HolidayOrNot = "0" Then        '平日
    '                    If LastDate <> RowDate Then     '從頭算(重新來過)
    '                        'If RowTotalTime > 4 Then
    '                        '    outMsg = "累積時數超過4小時!"
    '                        '    execResult = False
    '                        If RowTotalTime <= 4 AndAlso RowTotalTime > 2 Then
    '                            Period1 = 2
    '                            Period2 = RowTotalTime - 2
    '                            FlagPeriod1 = False
    '                            FlagPeriod2 = True
    '                        ElseIf RowTotalTime <= 2 Then
    '                            Period1 = RowTotalTime
    '                            If Period1 < 2 Then
    '                                FlagPeriod1 = True
    '                                FlagPeriod2 = False
    '                            Else
    '                                FlagPeriod1 = False
    '                                FlagPeriod2 = True
    '                            End If
    '                        End If
    '                    ElseIf LastDate = RowDate Then  '繼續算(繼續算下一筆)
    '                        'If (LastPeriod1 + LastPeriod2 + RowTotalTime) > 4 Then
    '                        '    outMsg = "累積時數超過4小時!"
    '                        '    execResult = False
    '                        If (LastPeriod1 + LastPeriod2 + RowTotalTime) <= 4 AndAlso (LastPeriod1 + LastPeriod2 + RowTotalTime) > 2 Then
    '                            Period1 = 2
    '                            Period2 = (LastPeriod1 + LastPeriod2 + RowTotalTime) - 2
    '                            FlagPeriod1 = False
    '                            FlagPeriod2 = True
    '                            'ElseIf (LastPeriod1 + LastPeriod2 + RowTotalTime) <= 4 AndAlso (LastPeriod1 + LastPeriod2 + RowTotalTime) > 2 Then
    '                            '    Period1 = 2
    '                            '    Period2 = (LastPeriod1 + LastPeriod2 + RowTotalTime) - 2
    '                            '    FlagPeriod1 = False
    '                            '    FlagPeriod2 = True
    '                            'ElseIf (LastPeriod1 + LastPeriod2 + RowTotalTime) <= 2 Then
    '                            '    Period1 = (LastPeriod1 + LastPeriod2 + RowTotalTime)
    '                            '    If Period1 < 2 Then
    '                            '        FlagPeriod1 = True
    '                            '        FlagPeriod2 = False
    '                            '    Else
    '                            '        FlagPeriod1 = False
    '                            '        FlagPeriod2 = True
    '                            '    End If
    '                        ElseIf (LastPeriod1 + LastPeriod2 + RowTotalTime) <= 2 Then
    '                            Period1 = (LastPeriod1 + RowTotalTime)
    '                            Period2 = LastPeriod2
    '                            If Period1 < 2 Then
    '                                FlagPeriod1 = True
    '                                FlagPeriod2 = False
    '                            Else
    '                                FlagPeriod1 = False
    '                                FlagPeriod2 = True
    '                            End If
    '                        End If
    '                    End If
    '                    LastTxnID = RowTxnID
    '                    LastDate = RowDate
    '                    LastRowTotalTime = RowTotalTime
    '                    LastRowMinTime = RowTotalMinTime
    '                    LastPeriod1 = Period1
    '                    LastPeriod2 = Period2
    '                    LastPeriod3 = Period3
    '                ElseIf HolidayOrNot = "1" Then    '假日
    '                    If LastDate <> RowDate Then    '從頭算(重新來過)
    '                        If RowTotalTime <= 12 Then
    '                            Period3 = RowTotalTime
    '                            FlagPeriod3 = True
    '                        End If
    '                    ElseIf LastDate = RowDate Then      '繼續算(繼續算下一筆)
    '                        'If LastPeriod3 + RowTotalTime > 12 Then
    '                        '    outMsg = "累積時數超過12小時!"
    '                        '    execResult = False
    '                        If LastPeriod3 + RowTotalTime <= 12 Then
    '                            Period3 = LastPeriod3 + RowTotalTime
    '                            FlagPeriod3 = True
    '                        End If
    '                    End If
    '                    LastTxnID = RowTxnID
    '                    LastDate = RowDate
    '                    LastRowTotalTime = RowTotalTime
    '                    LastRowMinTime = RowTotalMinTime
    '                    LastPeriod1 = Period1
    '                    LastPeriod2 = Period2
    '                    LastPeriod3 = Period3
    '                End If
    '            End If
    '        ElseIf LastTxnID = RowTxnID Then     '跨日
    '            Dim SumMinTime As Double = LastRowMinTime + RowTotalMinTime   '計算當筆加班單總"分鐘"數
    '            Dim SumHrTime As Double = (Math.Round(SumMinTime / 6, MidpointRounding.AwayFromZero)) / 10   '計算當筆加班單總"小時"數
    '            RowTotalTime = SumHrTime - LastRowTotalTime     '第二天的時數 = 總時數 - 第一天的時數

    '            If RowDate = DateB Then
    '                If HolidayOrNot = "0" Then       '起日是平日
    '                    'If FlagPeriod1 = True Then       '從時段一開始計算
    '                    '    If RowTotalTime > 4 * 60 Then
    '                    '        outMsg = "累積時數超過4小時!"
    '                    '        execResult = False
    '                    '    ElseIf RowTotalTime <= 4 * 60 AndAlso RowTotalTime > 2 * 60 Then
    '                    '        Period1 = 2 * 60
    '                    '        Period2 = RowTotalTime - 2 * 60
    '                    '    ElseIf RowTotalTime <= 2 * 60 Then
    '                    '        Period1 = RowTotalTime
    '                    '    End If
    '                    'ElseIf FlagPeriod2 = True Then       '從時段二開始計算
    '                    '    If RowTotalTime > 4 * 60 Then
    '                    '        outMsg = "累積時數超過4小時!"
    '                    '        execResult = False
    '                    '    ElseIf RowTotalTime <= 4 * 60 AndAlso RowTotalTime > 2 * 60 Then
    '                    '        Period2 = 2 * 60
    '                    '        Period1 = RowTotalTime - 2 * 60
    '                    '    ElseIf RowTotalTime <= 2 * 60 Then
    '                    '        Period2 = RowTotalTime
    '                    '    End If
    '                    'ElseIf FlagPeriod3 = True Then       '從時段三開始計算
    '                    '    If LastPeriod3 >= 2 * 60 Then     '前筆的時段三大於2從時段二補
    '                    '        If RowTotalTime <= 4 * 60 AndAlso RowTotalTime > 2 * 60 Then
    '                    '            Period2 = 2 * 60
    '                    '            Period1 = RowTotalTime - 2 * 60
    '                    '        ElseIf RowTotalTime <= 2 * 60 Then
    '                    '            Period2 = RowTotalTime
    '                    '        End If
    '                    '    ElseIf LastPeriod3 < 2 * 60 Then     '前筆的時段三小於2從時段一補        
    '                    '        If (RowTotalTime <= 4 * 60 AndAlso RowTotalTime > 2 * 60) Then
    '                    '            Period1 = 2 * 60 - LastPeriod3
    '                    '            Period2 = (LastPeriod3 + RowTotalTime) - 2 * 60
    '                    '        ElseIf RowTotalTime <= 2 * 60 Then
    '                    '            If (RowTotalTime > (2 * 60 - LastPeriod3)) Then
    '                    '                Period1 = 2 * 60 - LastPeriod3
    '                    '                Period2 = (LastPeriod3 + RowTotalTime) - 2 * 60
    '                    '            Else
    '                    '                Period1 = RowTotalTime
    '                    '            End If
    '                    '        End If
    '                    '    End If
    '                    'End If
    '                    If LastRowTotalTime = 0 Then       '上一筆加班時數等於0
    '                        'If RowTotalTime > 4 Then
    '                        '    outMsg = "累積時數超過4小時!"
    '                        '    execResult = False
    '                        If RowTotalTime <= 4 AndAlso RowTotalTime > 2 Then
    '                            Period1 = 2
    '                            Period2 = RowTotalTime - 2
    '                            FlagPeriod1 = False
    '                            FlagPeriod2 = True
    '                        ElseIf RowTotalTime <= 2 Then
    '                            Period1 = RowTotalTime
    '                            If Period1 < 2 Then
    '                                FlagPeriod1 = True
    '                                FlagPeriod2 = False
    '                            Else
    '                                FlagPeriod1 = False
    '                                FlagPeriod2 = True
    '                            End If
    '                        End If
    '                    Else
    '                        If FlagPeriod1 = True Then       '從時段一開始計算
    '                            'If RowTotalTime > 4 Then
    '                            '    outMsg = "累積時數超過4小時!"
    '                            '    execResult = False
    '                            If RowTotalTime <= 4 AndAlso RowTotalTime > 2 Then
    '                                If RowTotalTime + LastPeriod1 <= 6 AndAlso RowTotalTime + LastPeriod1 > 4 Then
    '                                    Period1 = 2 - LastPeriod1
    '                                    Period2 = RowTotalTime - Period1
    '                                    If Period2 <= 4 AndAlso Period2 > 2 Then
    '                                        Period2 = 2
    '                                        Period1 = RowTotalTime - Period2
    '                                    End If
    '                                ElseIf RowTotalTime + LastPeriod1 <= 4 AndAlso RowTotalTime + LastPeriod1 > 2 Then
    '                                    Period1 = 2 - LastPeriod1
    '                                    Period2 = RowTotalTime - Period1
    '                                ElseIf RowTotalTime + LastPeriod1 <= 2 Then
    '                                    Period1 = 2 - LastPeriod1
    '                                    Period2 = RowTotalTime - Period1
    '                                End If
    '                            ElseIf RowTotalTime <= 2 Then
    '                                If RowTotalTime + LastPeriod1 <= 4 AndAlso RowTotalTime + LastPeriod1 > 2 Then
    '                                    Period1 = 2 - LastPeriod1
    '                                    Period2 = RowTotalTime - Period1
    '                                ElseIf RowTotalTime <= 2 Then
    '                                    Period1 = RowTotalTime
    '                                End If
    '                            End If
    '                        ElseIf FlagPeriod2 = True Then '從時段二開始計算
    '                            'If RowTotalTime > 4 Then
    '                            '    outMsg = "累積時數超過4小時!"
    '                            '    execResult = False
    '                            If (RowTotalTime <= 4 AndAlso RowTotalTime > 2) Then
    '                                If RowTotalTime + LastPeriod2 <= 6 AndAlso RowTotalTime + LastPeriod2 > 4 Then
    '                                    Period2 = 2 '- LastPeriod2
    '                                    Period1 = RowTotalTime - Period2
    '                                    If Period1 <= 4 AndAlso Period1 > 2 Then
    '                                        Period1 = 2
    '                                        Period2 = RowTotalTime - Period2
    '                                    End If
    '                                ElseIf RowTotalTime + LastPeriod2 <= 4 AndAlso RowTotalTime + LastPeriod2 > 2 Then
    '                                    Period2 = 2 ' -LastPeriod2;
    '                                    Period1 = RowTotalTime - Period2
    '                                ElseIf RowTotalTime + LastPeriod2 <= 2 Then
    '                                    Period2 = RowTotalTime
    '                                    'Period1 = RowTotalTime - Period2
    '                                End If
    '                            ElseIf RowTotalTime <= 2 Then
    '                                Period2 = RowTotalTime
    '                            End If
    '                        ElseIf FlagPeriod3 = True Then '從時段三開始計算
    '                            If LastPeriod3 >= 2 Then    '前筆的時段三大於2從時段二補
    '                                If RowTotalTime <= 4 AndAlso RowTotalTime > 2 Then
    '                                    Period2 = 2
    '                                    Period1 = RowTotalTime - 2
    '                                ElseIf RowTotalTime <= 2 Then
    '                                    Period2 = RowTotalTime
    '                                End If
    '                            ElseIf LastPeriod3 < 2 Then '前筆的時段三小於2從時段一補
    '                                If RowTotalTime <= 4 AndAlso RowTotalTime > 2 Then
    '                                    Period1 = 2 - LastPeriod3
    '                                    Period2 = (LastPeriod3 + RowTotalTime) - 2
    '                                ElseIf RowTotalTime <= 2 Then
    '                                    If (RowTotalTime > (2 - LastPeriod3)) Then
    '                                        Period1 = 2 - LastPeriod3
    '                                        Period2 = (LastPeriod3 + RowTotalTime) - 2
    '                                    Else
    '                                        Period1 = RowTotalTime
    '                                    End If
    '                                End If
    '                            End If
    '                        End If
    '                    End If
    '                    LastTxnID = RowTxnID
    '                    LastDate = RowDate
    '                    LastRowTotalTime = RowTotalTime
    '                    LastPeriod1 = Period1
    '                    LastPeriod2 = Period2
    '                    LastPeriod3 = Period3
    '                    StartDatePeriod1 = LastPeriod1
    '                    StartDatePeriod2 = LastPeriod2
    '                    StartDatePeriod3 = LastPeriod3
    '                ElseIf HolidayOrNot = "1" Then      '起日是假日
    '                    Period3 = RowTotalTime
    '                    LastTxnID = RowTxnID
    '                    LastDate = RowDate
    '                    LastRowTotalTime = RowTotalTime
    '                    LastPeriod1 = Period1
    '                    LastPeriod2 = Period2
    '                    LastPeriod3 = Period3
    '                    StartDatePeriod1 = LastPeriod1
    '                    StartDatePeriod2 = LastPeriod2
    '                    StartDatePeriod3 = LastPeriod3
    '                End If
    '            ElseIf RowDate = DateE Then      '迄日
    '                If HolidayOrNot = "0" Then   '迄日是平日
    '                    If LastRowTotalTime = 0 Then    '上一筆加班時數等於0
    '                        'If RowTotalTime > 4 Then
    '                        '    outMsg = "累積時數超過4小時!"
    '                        '    execResult = False
    '                        If (RowTotalTime <= 4 AndAlso RowTotalTime > 2) Then
    '                            Period1 = 2
    '                            Period2 = RowTotalTime - 2
    '                            FlagPeriod1 = False
    '                            FlagPeriod2 = True
    '                        ElseIf RowTotalTime <= 2 Then
    '                            Period1 = RowTotalTime
    '                            If Period1 < 2 Then
    '                                FlagPeriod1 = True
    '                                FlagPeriod2 = False
    '                            Else
    '                                FlagPeriod1 = False
    '                                FlagPeriod2 = True
    '                            End If
    '                        End If
    '                    Else
    '                        If FlagPeriod1 = True Then       '從時段一開始計算
    '                            'If RowTotalTime > 4 Then
    '                            '    outMsg = "累積時數超過4小時!"
    '                            '    execResult = False
    '                            If (RowTotalTime <= 4 AndAlso RowTotalTime > 2) Then
    '                                If RowTotalTime + LastPeriod1 <= 6 AndAlso RowTotalTime + LastPeriod1 > 4 Then
    '                                    Period1 = 2 - LastPeriod1
    '                                    Period2 = RowTotalTime - Period1
    '                                    If Period2 <= 4 AndAlso Period2 > 2 Then
    '                                        Period2 = 2
    '                                        Period1 = RowTotalTime - Period2
    '                                    End If
    '                                ElseIf RowTotalTime + LastPeriod1 <= 4 AndAlso RowTotalTime + LastPeriod1 > 2 Then
    '                                    Period1 = 2 - LastPeriod1
    '                                    Period2 = RowTotalTime - Period1
    '                                ElseIf RowTotalTime + LastPeriod1 <= 2 Then
    '                                    Period1 = 2 - LastPeriod1
    '                                    Period2 = RowTotalTime - Period1
    '                                End If
    '                            ElseIf RowTotalTime <= 2 Then
    '                                If RowTotalTime + LastPeriod1 <= 4 AndAlso RowTotalTime + LastPeriod1 > 2 Then
    '                                    Period1 = 2 - LastPeriod1
    '                                    Period2 = RowTotalTime - Period1
    '                                ElseIf RowTotalTime + LastPeriod1 <= 2 Then
    '                                    Period1 = RowTotalTime
    '                                End If
    '                            End If
    '                        ElseIf FlagPeriod2 = True Then       '從時段二開始計算
    '                            'If RowTotalTime > 4 Then
    '                            '    outMsg = "累積時數超過4小時!"
    '                            '    execResult = False
    '                            If RowTotalTime <= 4 AndAlso RowTotalTime > 2 Then
    '                                If RowTotalTime + LastPeriod2 <= 6 AndAlso RowTotalTime + LastPeriod2 > 4 Then
    '                                    Period2 = 2 '-LastPeriod2
    '                                    Period1 = RowTotalTime - Period2
    '                                    If Period1 <= 4 AndAlso Period1 > 2 Then
    '                                        Period1 = 2
    '                                        Period2 = RowTotalTime - Period2
    '                                    End If
    '                                ElseIf RowTotalTime + LastPeriod2 <= 4 AndAlso RowTotalTime + LastPeriod2 > 2 Then
    '                                    Period2 = 2    '-LastPeriod2
    '                                    Period1 = RowTotalTime - Period2
    '                                ElseIf RowTotalTime + LastPeriod2 <= 2 Then
    '                                    Period2 = RowTotalTime
    '                                    'Period1 = RowTotalTime - Period2
    '                                End If
    '                            ElseIf RowTotalTime <= 2 Then
    '                                Period2 = RowTotalTime
    '                                'Period1 = RowTotalTime - Period2
    '                            End If
    '                        ElseIf FlagPeriod3 = True Then       '從時段三開始計算
    '                            If LastPeriod3 >= 2 Then       '前筆的時段三大於2從時段二補
    '                                If RowTotalTime <= 4 AndAlso RowTotalTime > 2 Then
    '                                    Period2 = 2
    '                                    Period1 = RowTotalTime - 2
    '                                ElseIf RowTotalTime <= 2 Then
    '                                    Period2 = RowTotalTime
    '                                End If
    '                            ElseIf LastPeriod3 < 2 Then     '前筆的時段三小於2從時段一補
    '                                If (RowTotalTime <= 4 AndAlso RowTotalTime > 2) Then
    '                                    Period1 = 2 - LastPeriod3
    '                                    Period2 = (LastPeriod3 + RowTotalTime) - 2
    '                                ElseIf RowTotalTime <= 2 Then
    '                                    If (RowTotalTime > (2 - LastPeriod3)) Then
    '                                        Period1 = 2 - LastPeriod3
    '                                        Period2 = (LastPeriod3 + RowTotalTime) - 2
    '                                    Else
    '                                        Period1 = RowTotalTime
    '                                    End If
    '                                End If
    '                            End If
    '                        End If
    '                    End If
    '                    LastTxnID = RowTxnID
    '                    LastDate = RowDate
    '                    LastRowTotalTime = RowTotalTime
    '                    LastPeriod1 = Period1
    '                    LastPeriod2 = Period2
    '                    LastPeriod3 = Period3
    '                    EndDatePeriod1 = LastPeriod1
    '                    EndDatePeriod2 = LastPeriod2
    '                    EndDatePeriod3 = LastPeriod3
    '                ElseIf HolidayOrNot = "1" Then     '迄日是假日
    '                    Period3 = RowTotalTime
    '                    LastTxnID = RowTxnID
    '                    LastDate = RowDate
    '                    LastRowTotalTime = RowTotalTime
    '                    LastPeriod1 = Period1
    '                    LastPeriod2 = Period2
    '                    LastPeriod3 = Period3
    '                    EndDatePeriod1 = LastPeriod1
    '                    EndDatePeriod2 = LastPeriod2
    '                    EndDatePeriod3 = LastPeriod3
    '                End If
    '            ElseIf (RowDate <> DateB) AndAlso (RowDate <> DateE) Then      '一般情況
    '                If HolidayOrNot = "0" Then    '平日
    '                    'If FlagPeriod1 = True Then        '從時段一開始計算
    '                    '    If (RowTotalTime > 4 * 60) Then
    '                    '        outMsg = "累積時數超過4小時!"
    '                    '        execResult = False
    '                    '    ElseIf (RowTotalTime <= 4 * 60 AndAlso RowTotalTime > 2 * 60) Then
    '                    '        Period1 = 2 * 60
    '                    '        Period2 = RowTotalTime - 2 * 60
    '                    '    ElseIf RowTotalTime <= 2 * 60 Then
    '                    '        Period1 = RowTotalTime
    '                    '    End If
    '                    'ElseIf FlagPeriod2 = True Then     '從時段二開始計算
    '                    '    If RowTotalTime > 4 * 60 Then
    '                    '        outMsg = "累積時數超過4小時!"
    '                    '        execResult = False
    '                    '    ElseIf (RowTotalTime <= 4 * 60 AndAlso RowTotalTime > 2 * 60) Then
    '                    '        Period2 = 2 * 60
    '                    '        Period1 = RowTotalTime - 2 * 60
    '                    '    ElseIf RowTotalTime <= 2 * 60 Then
    '                    '        Period2 = RowTotalTime
    '                    '    End If
    '                    'ElseIf FlagPeriod3 = True Then        '從時段三開始計算
    '                    '    If LastPeriod3 >= 2 * 60 Then       '前筆的時段三大於2從時段二補
    '                    '        If (RowTotalTime <= 4 * 60 AndAlso RowTotalTime > 2 * 60) Then
    '                    '            Period2 = 2 * 60
    '                    '            Period1 = RowTotalTime - 2 * 60
    '                    '        ElseIf RowTotalTime <= 2 * 60 Then
    '                    '            Period2 = RowTotalTime
    '                    '        End If
    '                    '    ElseIf LastPeriod3 < 2 * 60 Then    '前筆的時段三小於2從時段一補
    '                    '        If (RowTotalTime <= 4 * 60 AndAlso RowTotalTime > 2 * 60) Then
    '                    '            Period1 = 2 * 60 - LastPeriod3
    '                    '            Period2 = (LastPeriod3 + RowTotalTime) - 2 * 60
    '                    '        ElseIf RowTotalTime <= 2 * 60 Then
    '                    '            If (RowTotalTime > (2 * 60 - LastPeriod3)) Then
    '                    '                Period1 = 2 * 60 - LastPeriod3
    '                    '                Period2 = (LastPeriod3 + RowTotalTime) - 2 * 60
    '                    '            Else
    '                    '                Period1 = RowTotalTime
    '                    '            End If
    '                    '        End If
    '                    '    End If
    '                    'End If
    '                    If LastRowTotalTime = 0 Then   '上一筆加班時數等於0
    '                        'If LastRowTotalTime > 4 Then
    '                        '    outMsg = "累積時數超過4小時!"
    '                        '    execResult = False
    '                        If RowTotalTime <= 4 AndAlso RowTotalTime > 2 Then
    '                            Period1 = 2
    '                            Period2 = RowTotalTime - 2
    '                            FlagPeriod1 = False
    '                            FlagPeriod2 = True
    '                        ElseIf RowTotalTime <= 2 Then
    '                            Period1 = RowTotalTime
    '                            If Period1 < 2 Then
    '                                FlagPeriod1 = True
    '                                FlagPeriod2 = False
    '                            Else
    '                                FlagPeriod1 = False
    '                                FlagPeriod2 = True
    '                            End If
    '                        End If
    '                    Else
    '                        If FlagPeriod1 = True Then        '從時段一開始計算
    '                            'If (RowTotalTime > 4) Then
    '                            '    outMsg = "累積時數超過4小時!"
    '                            '    execResult = False
    '                            If (RowTotalTime <= 4 AndAlso RowTotalTime > 2) Then
    '                                If RowTotalTime + LastPeriod1 <= 6 AndAlso RowTotalTime + LastPeriod1 > 4 Then
    '                                    Period1 = 2 - LastPeriod1
    '                                    Period2 = RowTotalTime - Period1
    '                                    If Period2 <= 4 AndAlso Period2 > 2 Then
    '                                        Period2 = 2
    '                                        Period1 = RowTotalTime - Period2
    '                                    End If
    '                                ElseIf RowTotalTime + LastPeriod1 <= 4 AndAlso RowTotalTime + LastPeriod1 > 2 Then
    '                                    Period1 = 2 - LastPeriod1
    '                                    Period2 = RowTotalTime - Period1
    '                                ElseIf RowTotalTime + LastPeriod1 <= 2 Then
    '                                    Period1 = 2 - LastPeriod1
    '                                    Period2 = RowTotalTime - Period1
    '                                End If
    '                            ElseIf RowTotalTime <= 2 Then
    '                                If (RowTotalTime + LastPeriod1 <= 4 AndAlso RowTotalTime + LastPeriod1 > 2) Then
    '                                    Period1 = 2 - LastPeriod1
    '                                    Period2 = RowTotalTime - Period1
    '                                ElseIf (RowTotalTime + LastPeriod1 <= 2) Then
    '                                    Period1 = RowTotalTime
    '                                End If
    '                            End If
    '                        ElseIf FlagPeriod2 = True Then     '從時段二開始計算
    '                            'If RowTotalTime > 4 Then
    '                            '    outMsg = "累積時數超過4小時!"
    '                            '    execResult = False
    '                            If (RowTotalTime <= 4 AndAlso RowTotalTime > 2) Then
    '                                If RowTotalTime + LastPeriod2 <= 6 AndAlso RowTotalTime + LastPeriod2 > 4 Then
    '                                    Period2 = 2     '- LastPeriod2
    '                                    Period1 = RowTotalTime - Period2
    '                                    If Period1 <= 4 AndAlso Period1 > 2 Then
    '                                        Period1 = 2
    '                                        Period2 = RowTotalTime - Period2
    '                                    End If
    '                                ElseIf RowTotalTime + LastPeriod2 <= 4 AndAlso RowTotalTime + LastPeriod2 > 2 Then
    '                                    Period1 = 2     '- LastPeriod2
    '                                    Period2 = RowTotalTime - Period2
    '                                ElseIf RowTotalTime + LastPeriod2 <= 2 Then
    '                                    Period2 = RowTotalTime
    '                                    'Period1 = RowTotalTime - Period2
    '                                End If
    '                            ElseIf RowTotalTime <= 2 Then
    '                                Period2 = RowTotalTime
    '                            End If
    '                        ElseIf FlagPeriod3 = True Then        '從時段三開始計算
    '                            If LastPeriod3 >= 2 Then       '前筆的時段三大於2從時段二補
    '                                If (RowTotalTime <= 4 AndAlso RowTotalTime > 2) Then
    '                                    Period2 = 2
    '                                    Period1 = RowTotalTime - 2
    '                                ElseIf RowTotalTime <= 2 Then
    '                                    Period2 = RowTotalTime
    '                                End If
    '                            ElseIf LastPeriod3 < 2 Then    '前筆的時段三小於2從時段一補
    '                                If (RowTotalTime <= 4 AndAlso RowTotalTime > 2) Then
    '                                    Period1 = 2 - LastPeriod3
    '                                    Period2 = (LastPeriod3 + RowTotalTime) - 2
    '                                ElseIf RowTotalTime <= 2 Then
    '                                    If (RowTotalTime > (2 - LastPeriod3)) Then
    '                                        Period1 = 2 - LastPeriod3
    '                                        Period2 = (LastPeriod3 + RowTotalTime) - 2
    '                                    Else
    '                                        Period1 = RowTotalTime
    '                                    End If
    '                                End If
    '                            End If
    '                        End If
    '                    End If
    '                    LastTxnID = RowTxnID
    '                    LastDate = RowDate
    '                    LastRowTotalTime = RowTotalTime
    '                    LastPeriod1 = Period1
    '                    LastPeriod2 = Period2
    '                    LastPeriod3 = Period3
    '                ElseIf HolidayOrNot = "1" Then      '假日
    '                    Period3 = RowTotalTime
    '                    LastTxnID = RowTxnID
    '                    LastDate = RowDate
    '                    LastRowTotalTime = RowTotalTime
    '                    LastPeriod1 = Period1
    '                    LastPeriod2 = Period2
    '                    LastPeriod3 = Period3
    '                End If
    '            End If
    '        End If
    '    Next
    '    Dim strOTDateStart = startDate(0) + "," + Convert.ToDouble(StartDatePeriod1).ToString("0.0") + "," + Convert.ToDouble(StartDatePeriod2).ToString("0.0") + "," + Convert.ToDouble(StartDatePeriod3).ToString("0.0")
    '    Dim strOTDateEnd = endDate(0) + "," + Convert.ToDouble(EndDatePeriod1).ToString("0.0") + "," + Convert.ToDouble(EndDatePeriod2).ToString("0.0") + "," + Convert.ToDouble(EndDatePeriod3).ToString("0.0")
    '    If execResult = True Then
    '        Result = strOTDateStart + ";" + strOTDateEnd
    '        reMsg = Result
    '    Else
    '        reMsg = outMsg
    '    End If
    '    Return execResult
    'End Function

#End Region

#Region "舊的checkMonthTime-檢查是否超過每個月可以加的時數"
    ''' <summary>
    ''' 檢查是否超過每個月可以加的時數
    ''' </summary>
    ''' <param name="strEmp">員工編號</param>
    ''' <param name="dateStart">開始日期</param>
    ''' <param name="dateEnd">結束日期</param>
    ''' <param name="MonthLimitHour">參數設定</param>
    ''' <param name="totalTime">本次的加班時數</param>
    ''' <param name="mealtime">用餐時間</param>
    ''' <param name="cntStart">起日TotalTime</param>
    ''' <param name="cntEnd">迄日TotalTime</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    'Public Function checkMonthTime(strComp As String, strEmp As String, dateStart As String, dateEnd As String, MonthLimitHour As Double, totalTime As Double, mealtime As Double, _
    ' cntStart As Double, cntEnd As Double) As Boolean
    '    'Dim db As New DbHelper("AattendantDB")
    '    'Dim sb As CommandHelper = db.CreateCommandHelper()
    '    'Dim sb1 As CommandHelper = db.CreateCommandHelper()
    '    Dim ovBusinessCommon As New OVBusinessCommon
    '    Dim blstart As Boolean = CheckHolidayOrNot(dateStart)
    '    Dim blend As Boolean = CheckHolidayOrNot(dateEnd)
    '    Dim dt As DataTable = Nothing
    '    Dim dt1 As DataTable = Nothing
    '    Dim dbTotal As Double = 0
    '    Dim dbHoTotal As Double = 0
    '    Dim dbNTotal As Double = 0
    '    Dim mealOver As String = ""

    '    Dim sb As New StringBuilder()
    '    Dim sb1 As New StringBuilder()

    '    '本月加班時數
    '    sb.Clear()
    '    sb.Append("SELECT ISNULL(SUM(A.TotalTime),0) AS TotalTime FROM ")
    '    sb.Append(" (SELECT SUM(OTTotalTime)-SUM(MealTime) AS TotalTime  FROM OverTimeAdvance WHERE OTEmpID='" + strEmp + "' AND OTStatus IN ('2','3') AND MONTH(OTStartDate)=MONTH('" + dateStart + "')")
    '    sb.Append(" UNION")
    '    sb.Append(" SELECT SUM(OTTotalTime)-SUM(MealTime) AS TotalTime FROM OverTimeDeclaration WHERE OTEmpID='" + strEmp + "' AND OTStatus IN ('2','3') AND MONTH(OTStartDate)=MONTH('" + dateStart + "')) A")
    '    dt = Bsp.DB.ExecuteDataSet(CommandType.Text, sb.ToString, "AattendantDB").Tables(0)
    '    dbTotal = Convert.ToDouble(dt.Rows(0)("TotalTime").ToString())
    '    If dateStart = dateEnd Then     '是同一天
    '        If Not blstart Then     '起日是平日
    '            ' If (dbTotal + (totalTime - mealtime)) / 60 > MonthLimitHour Then
    '            If (dbTotal + (totalTime - mealtime) - dbNTotal) / 60 > MonthLimitHour Then
    '                Return False
    '            End If
    '        Else    '起日是假日
    '            sb1.Append("SELECT ISNULL(SUM(A.TotalTime),0) AS TotalTime FROM ")
    '            sb1.Append(" (SELECT SUM(OTTotalTime)-SUM(MealTime) AS TotalTime  FROM OverTimeAdvance WHERE OTEmpID='" + strEmp + "' AND OTStatus IN ('2','3') AND OTStartDate='" + dateStart + "'")
    '            sb1.Append(" UNION")
    '            sb1.Append(" SELECT SUM(OTTotalTime)-SUM(MealTime) AS TotalTime FROM OverTimeDeclaration WHERE OTEmpID='" + strEmp + "' AND OTStatus IN ('2','3') AND OTStartDate='" + dateStart + "' ) A")
    '            dt1 = Bsp.DB.ExecuteDataSet(CommandType.Text, sb.ToString, "AattendantDB").Tables(0)
    '            '假日時數
    '            dbHoTotal = If(dt1.Rows(0)("TotalTime") <> Nothing, Convert.ToDouble(dt1.Rows(0)("TotalTime").ToString()), "0.0")

    '            If ovBusinessCommon.IsNationalHoliday(dateStart) Then   '國定假日不納入檢核
    '                'If ((dbTotal - ((totalTime - mealtime) + dbHoTotal)) / 60 > MonthLimitHour) Then

    '                '    Return False
    '                'End If
    '            Else
    '                If (dbHoTotal + (totalTime - mealtime)) / 60 > 0 AndAlso (dbHoTotal + (totalTime - mealtime)) / 60 <= 4 Then
    '                    dbHoTotal = 4
    '                ElseIf (dbHoTotal + (totalTime - mealtime)) / 60 > 4 AndAlso (dbHoTotal + (totalTime - mealtime)) / 60 <= 8 Then
    '                    dbHoTotal = 8
    '                ElseIf (dbHoTotal + (totalTime - mealtime)) / 60 > 8 AndAlso (dbHoTotal + (totalTime - mealtime)) / 60 <= 12 Then
    '                    dbHoTotal = 12
    '                End If
    '            End If
    '            If dbHoTotal + (dbTotal / 60) > MonthLimitHour Then
    '                Return False
    '            End If
    '        End If
    '    Else
    '        mealOver = MealJudge(cntStart, mealtime)
    '        If Not blstart AndAlso Not blend Then
    '            '平日
    '            If (dbTotal + (totalTime - mealtime)) / 60 > MonthLimitHour Then
    '                Return False
    '            End If
    '        ElseIf blstart AndAlso Not blend Then   '假日跨平日
    '            '假日跨平日
    '            sb1.Clear()
    '            sb1.Append("SELECT ISNULL(SUM(A.TotalTime),0) AS TotalTime FROM ")
    '            sb1.Append(" (SELECT SUM(OTTotalTime)-SUM(MealTime) AS TotalTime  FROM OverTimeAdvance WHERE OTEmpID='" + strEmp + "' AND OTStatus IN ('2','3') AND OTStartDate='" + dateStart + "'")
    '            sb1.Append(" UNION")
    '            sb1.Append(" SELECT SUM(OTTotalTime)-SUM(MealTime) AS TotalTime FROM OverTimeDeclaration WHERE OTEmpID='" + strEmp + "' AND OTStatus IN ('2','3') AND OTStartDate='" + dateStart + "' ) A")
    '            dt1 = Bsp.DB.ExecuteDataSet(CommandType.Text, sb1.ToString, "AattendantDB").Tables(0)
    '            '假日時數
    '            dbHoTotal = If(dt1.Rows(0)("TotalTime") <> Nothing, Convert.ToDouble(dt1.Rows(0)("TotalTime").ToString()), "0.0")
    '            mealOver = MealJudge(cntStart, mealtime)

    '            If ovBusinessCommon.IsNationalHoliday(dateStart) Then   '國定假日不納入檢核
    '                'If (dbTotal + (cntEnd - Convert.ToDouble(mealOver.Split(",")(3))) - (cntStart - Convert.ToDouble(mealOver.Split(",")(1)) + dbNTotal) / 60 > MonthLimitHour) Then
    '                '    Return False
    '                'End If
    '            Else
    '                If (dbHoTotal + (cntStart - Convert.ToDouble(mealOver.Split(","c)(1)))) / 60 > 0 AndAlso (dbHoTotal + (cntStart - Convert.ToDouble(mealOver.Split(","c)(1)))) / 60 <= 4 Then
    '                    dbHoTotal = 4
    '                ElseIf (dbHoTotal + (cntStart - Convert.ToDouble(mealOver.Split(","c)(1)))) / 60 > 4 AndAlso (dbHoTotal + (cntStart - Convert.ToDouble(mealOver.Split(","c)(1)))) / 60 <= 8 Then
    '                    dbHoTotal = 8
    '                ElseIf (dbHoTotal + (cntStart - Convert.ToDouble(mealOver.Split(","c)(1)))) / 60 > 8 AndAlso (dbHoTotal + (cntStart - Convert.ToDouble(mealOver.Split(","c)(1)))) / 60 <= 12 Then
    '                    dbHoTotal = 12
    '                End If
    '                If dbHoTotal + (dbTotal + (cntEnd - Convert.ToDouble(mealOver.Split(","c)(3)))) / 60 > MonthLimitHour Then
    '                    Return False
    '                End If
    '            End If

    '        ElseIf Not blstart AndAlso blend Then
    '            '平日跨假日
    '            sb1.Clear()
    '            sb1.Append("SELECT ISNULL(SUM(A.TotalTime),0) AS TotalTime FROM ")
    '            sb1.Append(" (SELECT SUM(OTTotalTime)-SUM(MealTime) AS TotalTime  FROM OverTimeAdvance WHERE OTEmpID='" + strEmp + "' AND OTStatus IN ('2','3') AND OTStartDate='" + dateEnd + "'")
    '            sb1.Append(" UNION")
    '            sb1.Append(" SELECT SUM(OTTotalTime)-SUM(MealTime) AS TotalTime FROM OverTimeDeclaration WHERE OTEmpID='" + strEmp + "' AND OTStatus IN ('2','3') AND OTStartDate='" + dateEnd + "' ) A")
    '            dt1 = Bsp.DB.ExecuteDataSet(CommandType.Text, sb1.ToString, "AattendantDB").Tables(0)
    '            '假日時數
    '            dbHoTotal = If(dt1.Rows(0)("TotalTime") <> Nothing, Convert.ToDouble(dt1.Rows(0)("TotalTime").ToString()), "0.0")
    '            mealOver = MealJudge(cntStart, mealtime)
    '            If ovBusinessCommon.IsNationalHoliday(dateStart) Then   '國定假日不納入檢核
    '                'If (dbTotal + (cntEnd - Convert.ToDouble(mealOver.Split(",")(3))) - (cntStart - Convert.ToDouble(mealOver.Split(",")(1)) + dbNTotal) / 60 > MonthLimitHour) Then
    '                '    Return False
    '                'End If
    '            Else
    '                If (dbHoTotal + (cntEnd - Convert.ToDouble(mealOver.Split(","c)(3)))) / 60 > 0 AndAlso (dbHoTotal + (cntEnd - Convert.ToDouble(mealOver.Split(","c)(3)))) / 60 <= 4 Then
    '                    dbHoTotal = 4
    '                ElseIf (dbHoTotal + (cntEnd - Convert.ToDouble(mealOver.Split(","c)(3)))) / 60 > 4 AndAlso (dbHoTotal + (cntEnd - Convert.ToDouble(mealOver.Split(","c)(3)))) / 60 <= 8 Then
    '                    dbHoTotal = 8
    '                ElseIf (dbHoTotal + (cntEnd - Convert.ToDouble(mealOver.Split(","c)(3)))) / 60 > 8 AndAlso (dbHoTotal + (cntEnd - Convert.ToDouble(mealOver.Split(","c)(3)))) / 60 <= 12 Then
    '                    dbHoTotal = 12
    '                End If
    '                If dbHoTotal + (dbTotal + (cntStart - Convert.ToDouble(mealOver.Split(","c)(1)))) / 60 > MonthLimitHour Then
    '                    Return False
    '                End If
    '            End If
    '        End If
    '    End If
    '    Return True
    'End Function
#End Region
End Class
