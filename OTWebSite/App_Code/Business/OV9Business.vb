'****************************************************
'功能說明：OV9000的Funct
'建立人員：Anson
'建立日期：2017/2/16
'****************************************************

Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Threading

Public Class OV9Business
    Implements IRequiresSessionState

#Region "Session暫存資料的全域變數"
    ''' <summary>
    ''' 暫存新增用的人員名單
    ''' </summary>
    ''' <value>_addESNDatas</value>
    ''' <returns>List(Of Dictionary(Of String, String))</returns>
    ''' <remarks></remarks>
    Private Shared Property _addESNDatas As List(Of Dictionary(Of String, String))
        Get
            Return HttpContext.Current.Session.Item(UserProfile.ActUserID + "OV9Business" + "AddESNDatas")
        End Get
        Set(ByVal value As List(Of Dictionary(Of String, String)))
            HttpContext.Current.Session.Item(UserProfile.ActUserID + "OV9Business" + "AddESNDatas") = value
        End Set
    End Property
    ''' <summary>
    ''' 暫存新增用的人員流程設定
    ''' </summary>
    ''' <value>_insertESNFDatas</value>
    ''' <returns>List(Of Dictionary(Of String, String))</returns>
    ''' <remarks></remarks>
    Private Shared Property _insertESNFDatas As List(Of Dictionary(Of String, String))
        Get
            Return HttpContext.Current.Session.Item(UserProfile.ActUserID + "OV9Business" + "InsertESNFDatas")
        End Get
        Set(ByVal value As List(Of Dictionary(Of String, String)))
            HttpContext.Current.Session.Item(UserProfile.ActUserID + "OV9Business" + "InsertESNFDatas") = value
        End Set
    End Property
    ''' <summary>
    ''' 暫存修改用的人員流程設定
    ''' </summary>
    ''' <value>_updateESNFDatas</value>
    ''' <returns>List(Of Dictionary(Of String, String))</returns>
    ''' <remarks></remarks>
    Private Shared Property _updateESNFDatas As List(Of Dictionary(Of String, String))
        Get
            Return HttpContext.Current.Session.Item(UserProfile.ActUserID + "OV9Business" + "UpdateESNFDatas")
        End Get
        Set(ByVal value As List(Of Dictionary(Of String, String)))
            HttpContext.Current.Session.Item(UserProfile.ActUserID + "OV9Business" + "UpdateESNFDatas") = value
        End Set
    End Property
#End Region

#Region "新增與修改 EmpFlowSN EmpFlowSNDefine"
    ''' <summary>
    ''' 新增與修改 EmpFlowSN EmpFlowSNDefine
    ''' </summary>
    ''' <param name="returnCount">回傳:完成新增修改筆數</param>
    ''' <param name="message">回傳:訊息</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Shared Function AddOrEditEmpFlowSNData(ByRef returnCount As Long, ByRef message As String) As Boolean
        Dim result As Boolean = False
        message = ""
        returnCount = 0
        Dim msg As String = ""
        Dim count As Long = 0
        Dim addESNDatas As List(Of Dictionary(Of String, String)) = _addESNDatas
        Dim insertESNFDatas As List(Of Dictionary(Of String, String)) = _insertESNFDatas
        Dim updateESNFDatas As List(Of Dictionary(Of String, String)) = _updateESNFDatas
        Dim compID As String = UserProfile.SelectCompRoleID
        Dim userID As String = UserProfile.ActUserID
        Dim thread As New Thread(
                           Sub()
                               Try
                                   If addESNDatas IsNot Nothing Then
                                       If addESNDatas.Count > 0 Then
                                           AddEmpFlowSNData(compID, userID, addESNDatas, count, msg)
                                       End If
                                   End If

                                   result = InsertOrUpdateEmpFlowSNFData(compID, userID, insertESNFDatas, updateESNFDatas, count, msg)

                               Catch ex As Exception
                                   msg = ex.Message
                               End Try
                           End Sub)
        thread.Start()
        result = True
        _addESNDatas = New List(Of Dictionary(Of String, String))()
        _insertESNFDatas = New List(Of Dictionary(Of String, String))()
        _updateESNFDatas = New List(Of Dictionary(Of String, String))()
        Return result
    End Function
#End Region

#Region "新增與修改SQL"
    ''' <summary>
    ''' 新增 EmpFlowSN
    ''' </summary>
    ''' <param name="datas">新增的暫存處理資料</param>
    ''' <param name="returnCount">回傳:完成新增筆數</param>
    ''' <param name="message">回傳:訊息</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Private Shared Function AddEmpFlowSNData(ByVal compID As String, ByVal userID As String, ByVal datas As List(Of Dictionary(Of String, String)), ByRef returnCount As Long, ByRef message As String) As Boolean
        Dim isSuccess As Boolean = False
        message = ""
        Dim sqlKeyName = "AddEmpFlowSNData"
        Dim maxCount As Long = 200
        Try
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Try
                    Dim allCount As Long = 0
                    Do While allCount <= datas.Count - 1
                        Dim strSQL As StringBuilder = New StringBuilder()
                        Dim sqlCount As Long = allCount
                        Dim paraCount As Long = allCount
                        Dim index01 As Long = 0
                        Dim index02 As Long = 0
                        Do While sqlCount <= datas.Count - 1 And index01 <= maxCount
                            strSQL.AppendLine(" INSERT INTO EmpFlowSN ")
                            strSQL.AppendLine(" ( ")
                            strSQL.AppendLine(" CompID, EmpID, LastChgComp, LastChgID, LastChgDate ")
                            strSQL.AppendLine(" ) ")
                            strSQL.AppendLine(" VALUES ")
                            strSQL.AppendLine(" ( ")
                            strSQL.AppendLine(String.Format(" @CompID{0}{1} ", sqlKeyName, sqlCount))
                            strSQL.AppendLine(String.Format(" , @EmpID{0}{1} ", sqlKeyName, sqlCount))
                            strSQL.AppendLine(String.Format(" , @LastChgComp{0}{1} ", sqlKeyName, sqlCount))
                            strSQL.AppendLine(String.Format(" , @LastChgID{0}{1} ", sqlKeyName, sqlCount))
                            strSQL.AppendLine(String.Format(" , @LastChgDate{0}{1} ", sqlKeyName, sqlCount))
                            strSQL.AppendLine(" ) ")
                            strSQL.AppendLine(" ; ")
                            sqlCount += 1
                            index01 += 1
                        Loop
                        Dim dbcmd As DbCommand = db.GetSqlStringCommand(strSQL.ToString())
                        Do While paraCount <= datas.Count - 1 And index02 <= maxCount
                            Dim data As Dictionary(Of String, String) = datas(paraCount)
                            db.AddInParameter(dbcmd, String.Format("@CompID{0}{1}", sqlKeyName, paraCount), DbType.String, data("CompID").ToString())
                            db.AddInParameter(dbcmd, String.Format("@EmpID{0}{1}", sqlKeyName, paraCount), DbType.String, data("EmpID").ToString())
                            db.AddInParameter(dbcmd, String.Format("@LastChgComp{0}{1}", sqlKeyName, paraCount), DbType.String, compID)
                            db.AddInParameter(dbcmd, String.Format("@LastChgID{0}{1}", sqlKeyName, paraCount), DbType.String, userID)
                            db.AddInParameter(dbcmd, String.Format("@LastChgDate{0}{1}", sqlKeyName, paraCount), DbType.String, Date.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"))
                            paraCount += 1
                            index02 += 1
                        Loop

                        Dim updateCount As Long = db.ExecuteNonQuery(dbcmd)
                        If updateCount > 0 Then
                            returnCount += updateCount
                            isSuccess = True
                        Else
                            'Throw New Exception("新增筆數為0")
                        End If
                        allCount = sqlCount
                    Loop
                    tran.Commit()
                Catch ex As Exception
                    tran.Rollback()
                    Throw New Exception(ex.Message)
                Finally
                    tran.Dispose()
                    If cn.State = ConnectionState.Open Then cn.Close()
                End Try
            End Using
            isSuccess = True
        Catch ex As Exception
            message = ex.Message
        End Try
        Return isSuccess
    End Function

    ''' <summary>
    ''' 新增與修改 EmpFlowSNDefine
    ''' </summary>
    ''' <param name="insertDatas">新增的暫存處理資料</param>
    ''' <param name="updateDatas">修改的暫存處理資料</param>
    ''' <param name="returnCount">回傳:完成新增修改筆數</param>
    ''' <param name="message">回傳:訊息</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Private Shared Function InsertOrUpdateEmpFlowSNFData(ByVal compID As String, ByVal userID As String, ByVal insertDatas As List(Of Dictionary(Of String, String)), ByVal updateDatas As List(Of Dictionary(Of String, String)), ByRef returnCount As Long, ByRef message As String) As Boolean
        Dim isSuccess As Boolean = False
        message = ""
        Dim insertSqlKeyName = "InsertEmpFlowSNFData"
        Dim updateSqlKeyName = "UpdateEmpFlowSNFData"
        Dim maxCount As Long = 200
        Try
            Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Try
                    Dim allCount As Long = 0
                    Do While allCount <= updateDatas.Count - 1
                        Dim strSQL As StringBuilder = New StringBuilder()
                        Dim sqlCount As Long = allCount
                        Dim paraCount As Long = allCount
                        Dim index01 As Long = 0
                        Dim index02 As Long = 0
                        Do While sqlCount <= updateDatas.Count - 1 And index01 <= maxCount
                            strSQL.AppendLine(" Update EmpFlowSNDefine ")
                            strSQL.AppendLine(" SET ")
                            strSQL.AppendLine(String.Format(" PrincipalFlag = @PrincipalFlag{0}{1} ", insertSqlKeyName, sqlCount))
                            strSQL.AppendLine(String.Format(" , LastChgComp = @LastChgComp{0}{1} ", insertSqlKeyName, sqlCount))
                            strSQL.AppendLine(String.Format(" , LastChgID = @LastChgID{0}{1} ", insertSqlKeyName, sqlCount))
                            strSQL.AppendLine(String.Format(" , LastChgDate = @LastChgDate{0}{1} ", insertSqlKeyName, sqlCount))
                            strSQL.AppendLine(" WHERE 0 = 0 ")
                            strSQL.AppendLine(String.Format(" AND CompID = @CompID{0}{1} ", insertSqlKeyName, sqlCount))
                            strSQL.AppendLine(String.Format(" AND EmpID = @EmpID{0}{1} ", insertSqlKeyName, sqlCount))
                            strSQL.AppendLine(String.Format(" AND SystemID = @SystemID{0}{1} ", insertSqlKeyName, sqlCount))
                            strSQL.AppendLine(String.Format(" AND FlowCode = @FlowCode{0}{1} ", insertSqlKeyName, sqlCount))
                            strSQL.AppendLine(String.Format(" AND FlowSN = @FlowSN{0}{1} ", insertSqlKeyName, sqlCount))
                            strSQL.AppendLine(" ; ")
                            sqlCount += 1
                            index01 += 1
                        Loop
                        Dim dbcmd As DbCommand = db.GetSqlStringCommand(strSQL.ToString())
                        Do While paraCount <= updateDatas.Count - 1 And index02 <= maxCount
                            Dim data As Dictionary(Of String, String) = updateDatas(paraCount)
                            db.AddInParameter(dbcmd, String.Format("@CompID{0}{1}", insertSqlKeyName, paraCount), DbType.String, data("CompID").ToString())
                            db.AddInParameter(dbcmd, String.Format("@EmpID{0}{1}", insertSqlKeyName, paraCount), DbType.String, data("EmpID").ToString())
                            db.AddInParameter(dbcmd, String.Format("@SystemID{0}{1}", insertSqlKeyName, paraCount), DbType.String, data("SystemID").ToString())
                            db.AddInParameter(dbcmd, String.Format("@FlowCode{0}{1}", insertSqlKeyName, paraCount), DbType.String, data("FlowCode").ToString())
                            db.AddInParameter(dbcmd, String.Format("@FlowSN{0}{1}", insertSqlKeyName, paraCount), DbType.String, data("FlowSN").ToString())
                            db.AddInParameter(dbcmd, String.Format("@PrincipalFlag{0}{1}", insertSqlKeyName, paraCount), DbType.String, data("PrincipalFlag").ToString())
                            db.AddInParameter(dbcmd, String.Format("@LastChgComp{0}{1}", insertSqlKeyName, paraCount), DbType.String, compID)
                            db.AddInParameter(dbcmd, String.Format("@LastChgID{0}{1}", insertSqlKeyName, paraCount), DbType.String, userID)
                            db.AddInParameter(dbcmd, String.Format("@LastChgDate{0}{1}", insertSqlKeyName, paraCount), DbType.String, Date.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"))
                            paraCount += 1
                            index02 += 1
                        Loop
                        Dim updateCount As Long = db.ExecuteNonQuery(dbcmd)
                        If updateCount > 0 Then
                            returnCount += updateCount
                            isSuccess = True
                        Else
                            'Throw New Exception("修改筆數為0")
                        End If
                        allCount = sqlCount
                    Loop

                    Dim allCount2 As Long = 0
                    Do While allCount2 <= insertDatas.Count - 1
                        Dim strSQL As StringBuilder = New StringBuilder()
                        Dim sqlCount As Long = allCount2
                        Dim paraCount As Long = allCount2
                        Dim index01 As Long = 0
                        Dim index02 As Long = 0
                        Do While sqlCount <= insertDatas.Count - 1 And index01 <= maxCount
                            strSQL.AppendLine(" INSERT INTO EmpFlowSNDefine ")
                            strSQL.AppendLine(" ( ")
                            strSQL.AppendLine(" CompID, EmpID, SystemID, FlowCode, FlowSN, PrincipalFlag, LastChgComp, LastChgID, LastChgDate ")
                            strSQL.AppendLine(" ) ")
                            strSQL.AppendLine(" VALUES ")
                            strSQL.AppendLine(" ( ")
                            strSQL.AppendLine(String.Format(" @CompID{0}{1} ", insertSqlKeyName, sqlCount))
                            strSQL.AppendLine(String.Format(" , @EmpID{0}{1} ", insertSqlKeyName, sqlCount))
                            strSQL.AppendLine(String.Format(" , @SystemID{0}{1} ", insertSqlKeyName, sqlCount))
                            strSQL.AppendLine(String.Format(" , @FlowCode{0}{1} ", insertSqlKeyName, sqlCount))
                            strSQL.AppendLine(String.Format(" , @FlowSN{0}{1} ", insertSqlKeyName, sqlCount))
                            strSQL.AppendLine(String.Format(" , @PrincipalFlag{0}{1} ", insertSqlKeyName, sqlCount))
                            strSQL.AppendLine(String.Format(" , @LastChgComp{0}{1} ", insertSqlKeyName, sqlCount))
                            strSQL.AppendLine(String.Format(" , @LastChgID{0}{1} ", insertSqlKeyName, sqlCount))
                            strSQL.AppendLine(String.Format(" , @LastChgDate{0}{1} ", insertSqlKeyName, sqlCount))
                            strSQL.AppendLine(" ) ")
                            strSQL.AppendLine(" ; ")
                            sqlCount += 1
                            index01 += 1
                        Loop
                        Dim dbcmd As DbCommand = db.GetSqlStringCommand(strSQL.ToString())
                        Do While paraCount <= insertDatas.Count - 1 And index02 <= maxCount
                            Dim data As Dictionary(Of String, String) = insertDatas(paraCount)
                            db.AddInParameter(dbcmd, String.Format("@CompID{0}{1}", insertSqlKeyName, paraCount), DbType.String, data("CompID").ToString())
                            db.AddInParameter(dbcmd, String.Format("@EmpID{0}{1}", insertSqlKeyName, paraCount), DbType.String, data("EmpID").ToString())
                            db.AddInParameter(dbcmd, String.Format("@SystemID{0}{1}", insertSqlKeyName, paraCount), DbType.String, data("SystemID").ToString())
                            db.AddInParameter(dbcmd, String.Format("@FlowCode{0}{1}", insertSqlKeyName, paraCount), DbType.String, data("FlowCode").ToString())
                            db.AddInParameter(dbcmd, String.Format("@FlowSN{0}{1}", insertSqlKeyName, paraCount), DbType.String, data("FlowSN").ToString())
                            db.AddInParameter(dbcmd, String.Format("@PrincipalFlag{0}{1}", insertSqlKeyName, paraCount), DbType.String, data("PrincipalFlag").ToString())
                            db.AddInParameter(dbcmd, String.Format("@LastChgComp{0}{1}", insertSqlKeyName, paraCount), DbType.String, compID)
                            db.AddInParameter(dbcmd, String.Format("@LastChgID{0}{1}", insertSqlKeyName, paraCount), DbType.String, userID)
                            db.AddInParameter(dbcmd, String.Format("@LastChgDate{0}{1}", insertSqlKeyName, paraCount), DbType.String, Date.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"))
                            paraCount += 1
                            index02 += 1
                        Loop
                        Dim updateCount As Long = db.ExecuteNonQuery(dbcmd)
                        If updateCount > 0 Then
                            returnCount += updateCount
                            isSuccess = True
                        Else
                            'Throw New Exception("新增筆數為0")
                        End If
                        allCount2 = sqlCount
                    Loop

                    tran.Commit()
                Catch ex As Exception
                    tran.Rollback()
                    Throw New Exception(ex.Message)
                Finally
                    tran.Dispose()
                    If cn.State = ConnectionState.Open Then cn.Close()
                End Try
            End Using
            isSuccess = True
        Catch ex As Exception
            message = ex.Message
        End Try
        Return isSuccess
    End Function
#End Region

#Region "人員流程設定檔資料比對"
    ''' <summary>
    ''' 人員流程設定檔資料比對
    ''' </summary>
    ''' <param name="sCompID">公司</param>
    ''' <param name="sEmpID">員編</param>
    ''' <param name="sOrganID">單位</param>
    ''' <param name="sDeptID">部門</param>
    ''' <param name="sSystemID">系統代碼</param>
    ''' <param name="sFlowCode">流程代碼</param>
    ''' <param name="sFlowSN">流程識別碼</param>
    ''' <param name="sRankIDTop">職等(起)</param>
    ''' <param name="sRankIDBottom">職等(迄)</param>
    ''' <param name="sTitleIDTop">職稱(起)</param>
    ''' <param name="sTitleIDBottom">職稱(迄)</param>
    ''' <param name="sPositionID">職位</param>
    ''' <param name="sWorkTypeID">工作性質</param>
    ''' <param name="sEmpFlowRemark">功能備註</param>
    ''' <param name="sBusinessType">業務類別</param>
    ''' <param name="addOrEditCount">回傳:新增與修改的總筆數</param>
    ''' <param name="showMsg">回傳:主流程有變動的所有員編</param>
    ''' <param name="message">回傳:訊息</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Shared Function ComparEmpFlowSNData(ByVal sCompID As String, ByVal sEmpID As String, ByVal sOrganID As String, _
                  ByVal sDeptID As String, ByVal sSystemID As String, ByVal sFlowCode As String, ByVal sFlowSN As String, _
                  ByVal sRankIDTop As String, ByVal sRankIDBottom As String, ByVal sTitleIDTop As String, ByVal sTitleIDBottom As String, _
                  ByVal sPositionID As String, ByVal sWorkTypeID As String, ByVal sEmpFlowRemark As String, ByVal sBusinessType As String, _
                  ByRef addOrEditCount As Long, ByRef showMsg As String, ByRef message As String) As Boolean
        Dim result As Boolean = False
        Dim insertESNDatas As List(Of Dictionary(Of String, String)) = New List(Of Dictionary(Of String, String))()
        Dim updateESNFDatas As List(Of Dictionary(Of String, String)) = New List(Of Dictionary(Of String, String))()
        Dim insertESNFDatas As List(Of Dictionary(Of String, String)) = New List(Of Dictionary(Of String, String))()
        Dim showMsgDatas As List(Of String) = New List(Of String)()
        _addESNDatas = insertESNDatas
        _insertESNFDatas = insertESNFDatas
        _updateESNFDatas = updateESNFDatas
        message = ""
        showMsg = ""
        addOrEditCount = 0
        Try
            If String.IsNullOrEmpty(sCompID) Or String.IsNullOrEmpty(sSystemID) Or String.IsNullOrEmpty(sFlowCode) Or String.IsNullOrEmpty(sFlowSN) Then
                Throw New Exception("查詢人員流程設定所需的資料不齊全!!")
            End If
            Dim esnBL = False
            Dim esnfBL = False
            Dim pdtBL = False
            Dim esnMsg As String = ""
            Dim esnfMsg As String = ""
            Dim pdtMsg As String = ""
            Dim empFlowSNDataTable As DataTable = New DataTable("EmpFlowSNDataTable")
            Dim empFlowSNDefineDataTable As DataTable = New DataTable("EmpFlowSNDefineDataTable")
            Dim personalDataTable As DataTable = New DataTable("PersonalDataTable")
            '查詢EmpFlowSN資料
            esnBL = QueryEmpFlowSNData(sCompID, sEmpID, empFlowSNDataTable, esnMsg)
            '查詢EmpFlowSNDefine資料
            esnfBL = QueryEmpFlowSNDefineData(sCompID, sEmpID, sSystemID, sFlowCode, "", empFlowSNDefineDataTable, esnfMsg)
            '查詢Personal資料
            pdtBL = QueryPersonalData(sCompID, sEmpID, sOrganID, sDeptID, _
                                      sRankIDTop, sRankIDBottom, sTitleIDTop, sTitleIDBottom, _
                                      sPositionID, sWorkTypeID, sEmpFlowRemark, sBusinessType, _
                                      personalDataTable, pdtMsg)
            '查無Personal資料直接拋錯誤
            If Not pdtBL Then
                Throw New Exception(pdtMsg)
            End If
            '符合名單loop逐一檢查
            For index As Integer = 0 To personalDataTable.Rows.Count - 1
                Dim itemData As DataRow = personalDataTable.Rows(index)
                Dim itemCompID = itemData("CompID").ToString()
                Dim itemEmpID = itemData("EmpID").ToString()
                Dim isNewEmpFlowSNDefineData As Boolean = False
                Dim isAddMessage As Boolean = False
                If Not String.IsNullOrEmpty(itemCompID) And Not String.IsNullOrEmpty(itemEmpID) Then
                    '收集要需要新增到EmpFlowSN的資料
                    Dim dataCount As Integer = (From row In empFlowSNDataTable.AsEnumerable() Where (row.Field(Of String)("CompID") = itemCompID And row.Field(Of String)("EmpID") = itemEmpID)).Count()
                    If dataCount = 0 Then
                        Dim checkCount As Integer = (From row In insertESNDatas Where (row("CompID") = itemCompID And row("EmpID") = itemEmpID)).Count()
                        If checkCount = 0 Then
                            Dim newData As Dictionary(Of String, String) = New Dictionary(Of String, String)() From {{"CompID", itemCompID}, {"EmpID", itemEmpID}}
                            insertESNDatas.Add(newData)
                        End If
                    End If
                    'Dim empFlowSNDefineSelectDatas As DataTable
                    'Dim selectDatas = (From row In empFlowSNDefineDataTable.AsEnumerable() _
                    '          Where (row.Field(Of String)("CompID") = itemCompID _
                    '                 And row.Field(Of String)("EmpID") = itemEmpID _
                    '                 And row.Field(Of String)("SystemID") = sSystemID _
                    '                 And row.Field(Of String)("FlowCode") = sFlowCode _
                    '                 ))
                    'If selectDatas.Count > 0 Then
                    '    empFlowSNDefineSelectDatas = selectDatas.CopyToDataTable()
                    'Else
                    '    empFlowSNDefineSelectDatas = New DataTable()
                    'End If

                    If empFlowSNDefineDataTable Is Nothing Then
                        isNewEmpFlowSNDefineData = True '需新增至EmpFlowSNDefineData
                    Else
                        If empFlowSNDefineDataTable.Rows.Count = 0 Then
                            isNewEmpFlowSNDefineData = True '需新增至EmpFlowSNDefineData
                        Else
                            Dim hadData = False
                            For i As Integer = 0 To empFlowSNDefineDataTable.Rows.Count - 1
                                Dim empFlowSNDefineSelectData = empFlowSNDefineDataTable.Rows(i)
                                Dim i_EmpID = empFlowSNDefineSelectData("EmpID").ToString()
                                Dim i_FlowSN = empFlowSNDefineSelectData("FlowSN").ToString()
                                Dim i_PrincipalFlag = empFlowSNDefineSelectData("PrincipalFlag").ToString()
                                Dim newData As Dictionary(Of String, String) = New Dictionary(Of String, String)()
                                If i_FlowSN = sFlowSN Then
                                    If i_PrincipalFlag <> "1" Then
                                        newData.Add("CompID", itemCompID)
                                        newData.Add("EmpID", itemEmpID)
                                        newData.Add("SystemID", sSystemID)
                                        newData.Add("FlowCode", sFlowCode)
                                        newData.Add("FlowSN", i_FlowSN)
                                        newData.Add("PrincipalFlag", "1")
                                        updateESNFDatas.Add(newData)
                                    End If
                                    If itemEmpID = i_EmpID Then
                                        hadData = True
                                    End If
                                Else
                                    If i_PrincipalFlag = "1" Then
                                        newData.Add("CompID", itemCompID)
                                        newData.Add("EmpID", itemEmpID)
                                        newData.Add("SystemID", sSystemID)
                                        newData.Add("FlowCode", sFlowCode)
                                        newData.Add("FlowSN", i_FlowSN)
                                        newData.Add("PrincipalFlag", "0")
                                        updateESNFDatas.Add(newData)
                                        isAddMessage = True '需顯示在主流程切換訊息
                                    End If
                                End If
                            Next
                            If Not hadData Then
                                isNewEmpFlowSNDefineData = True
                            End If
                        End If
                    End If
                    If isNewEmpFlowSNDefineData Then '需新增至EmpFlowSNDefineData
                        Dim checkCount As Integer = (From row In insertESNFDatas Where (row("CompID") = itemCompID And row("EmpID") = itemEmpID And row("SystemID") = sSystemID And row("FlowCode") = sFlowCode And row("FlowSN") = sFlowSN)).Count()
                        If checkCount = 0 Then
                            Dim newData As Dictionary(Of String, String) = New Dictionary(Of String, String)()
                            newData.Add("CompID", itemCompID)
                            newData.Add("EmpID", itemEmpID)
                            newData.Add("SystemID", sSystemID)
                            newData.Add("FlowCode", sFlowCode)
                            newData.Add("FlowSN", sFlowSN)
                            newData.Add("PrincipalFlag", "1")
                            insertESNFDatas.Add(newData)
                        End If

                    End If
                    If isAddMessage Then '需顯示在主流程切換訊息
                        showMsgDatas.Add(itemEmpID)
                    End If

                End If
            Next
            If showMsgDatas.Count > 0 Then
                showMsg = String.Join(", ", showMsgDatas)
            End If
            result = True
        Catch ex As Exception
            message = ex.Message
        End Try
        addOrEditCount = insertESNDatas.Count + insertESNFDatas.Count + updateESNFDatas.Count
        _addESNDatas = insertESNDatas
        _insertESNFDatas = insertESNFDatas
        _updateESNFDatas = updateESNFDatas
        Return result
    End Function
#End Region

#Region "查詢EmpFlowSN資料"
    ''' <summary>
    ''' 查詢EmpFlowSN資料
    ''' </summary>
    ''' <param name="sCompID">公司</param>
    ''' <param name="sEmpID">員編</param>
    ''' <param name="returnDataTable">回傳:資料</param>
    ''' <param name="message">回傳:訊息</param>
    ''' <returns>是否有資料或是否程式語法有誤</returns>
    ''' <remarks></remarks>
    Public Shared Function QueryEmpFlowSNData(ByVal sCompID As String, ByVal sEmpID As String, ByRef returnDataTable As DataTable, ByRef message As String) As Boolean
        Dim result As Boolean = False
        returnDataTable = New DataTable()
        message = ""
        Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
        Dim dbcmd As DbCommand
        Dim strSQL As StringBuilder = New StringBuilder()
        strSQL.AppendLine(" SELECT ")
        strSQL.AppendLine(" CompID, EmpID ")
        strSQL.AppendLine(" FROM EmpFlowSN ")
        strSQL.AppendLine(" WHERE 0 = 0 ")
        If Not String.IsNullOrEmpty(sCompID) Then
            strSQL.AppendLine(" AND CompID = @CompID ")
        End If
        If Not String.IsNullOrEmpty(sEmpID) Then
            strSQL.AppendLine(" AND EmpID = @EmpID ")
        End If
        strSQL.AppendLine(" ORDER BY CompID, EmpID ")
        strSQL.AppendLine(" ; ")
        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
        db.AddInParameter(dbcmd, "@CompID", DbType.String, sCompID)
        db.AddInParameter(dbcmd, "@EmpID", DbType.String, sEmpID)
        Dim ds As DataSet = db.ExecuteDataSet(dbcmd)
        If ds.Tables.Count > 0 Then

            Dim dt = ds.Tables(0)
            If dt.Rows.Count > 0 Then
                returnDataTable = dt
                result = True
            Else
                message = "查無EmpFlowSN資料[001]"
            End If
        Else
            message = "查無EmpFlowSN資料[002]"
        End If
        Return result
    End Function
#End Region

#Region "查詢EmpFlowSNDefine資料"
    ''' <summary>
    ''' 查詢EmpFlowSNDefine資料
    ''' </summary>
    ''' <param name="sCompID">公司</param>
    ''' <param name="sEmpID">員編</param>
    ''' <param name="sSystemID">系統代碼</param>
    ''' <param name="sFlowCode">流程代碼</param>
    ''' <param name="sFlowSN">流程識別碼</param>
    ''' <param name="returnDataTable">回傳:資料</param>
    ''' <param name="message">回傳:訊息</param>
    ''' <returns>是否有資料或是否程式語法有誤</returns>
    ''' <remarks></remarks>
    Public Shared Function QueryEmpFlowSNDefineData(ByVal sCompID As String, ByVal sEmpID As String, ByVal sSystemID As String, ByVal sFlowCode As String, ByVal sFlowSN As String, ByRef returnDataTable As DataTable, ByRef message As String) As Boolean
        Dim result As Boolean = False
        returnDataTable = New DataTable()
        message = ""
        Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
        Dim dbcmd As DbCommand
        Dim strSQL As StringBuilder = New StringBuilder()
        strSQL.AppendLine(" SELECT ")
        strSQL.AppendLine(" CompID, EmpID, SystemID, FlowCode, FlowSN, PrincipalFlag ")
        strSQL.AppendLine(" FROM EmpFlowSNDefine ")
        strSQL.AppendLine(" WHERE 0 = 0 ")
        If Not String.IsNullOrEmpty(sCompID) Then
            strSQL.AppendLine(" AND CompID = @CompID ")
        End If
        If Not String.IsNullOrEmpty(sEmpID) Then
            strSQL.AppendLine(" AND EmpID = @EmpID ")
        End If
        If Not String.IsNullOrEmpty(sSystemID) Then
            strSQL.AppendLine(" AND SystemID = @SystemID ")
        End If
        If Not String.IsNullOrEmpty(sFlowCode) Then
            strSQL.AppendLine(" AND FlowCode = @FlowCode ")
        End If
        If Not String.IsNullOrEmpty(sFlowSN) Then
            strSQL.AppendLine(" AND FlowSN = @FlowSN ")
        End If
        strSQL.AppendLine(" ORDER BY CompID, EmpID, SystemID, FlowCode, FlowSN ")
        strSQL.AppendLine(" ; ")
        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
        db.AddInParameter(dbcmd, "@CompID", DbType.String, sCompID)
        db.AddInParameter(dbcmd, "@EmpID", DbType.String, sEmpID)
        db.AddInParameter(dbcmd, "@SystemID", DbType.String, sSystemID)
        db.AddInParameter(dbcmd, "@FlowCode", DbType.String, sFlowCode)
        db.AddInParameter(dbcmd, "@FlowSN", DbType.String, sFlowSN)
        Dim ds As DataSet = db.ExecuteDataSet(dbcmd)
        If ds.Tables.Count > 0 Then

            Dim dt = ds.Tables(0)
            If dt.Rows.Count > 0 Then
                returnDataTable = dt
                result = True
            Else
                message = "查無EmpFlowSNDefine資料[001]"
            End If
        Else
            message = "查無EmpFlowSNDefine資料[002]"
        End If
        Return result
    End Function
#End Region

#Region "查詢Personal資料"
    ''' <summary>
    ''' 查詢Personal資料
    ''' </summary>
    ''' <param name="sCompID">公司</param>
    ''' <param name="sEmpID">員編</param>
    ''' <param name="sOrganID">單位</param>
    ''' <param name="sDeptID">部門</param>
    ''' <param name="sRankIDTop">職等(起)</param>
    ''' <param name="sRankIDBottom">職等(迄)</param>
    ''' <param name="sTitleIDTop">職稱(起)</param>
    ''' <param name="sTitleIDBottom">職稱(迄)</param>
    ''' <param name="sPositionID">職位</param>
    ''' <param name="sWorkTypeID">工作性質</param>
    ''' <param name="sEmpFlowRemark">功能備註</param>
    ''' <param name="sBusinessType">業務類別</param>
    ''' <param name="returnDataTable">回傳:資料</param>
    ''' <param name="message">回傳:訊息</param>
    ''' <returns>是否有資料或是否程式語法有誤</returns>
    ''' <remarks>上千筆資料</remarks>
    Public Shared Function QueryPersonalData(ByVal sCompID As String, ByVal sEmpID As String, ByVal sOrganID As String, ByVal sDeptID As String, _
                                             ByVal sRankIDTop As String, ByVal sRankIDBottom As String, ByVal sTitleIDTop As String, ByVal sTitleIDBottom As String, _
                                             ByVal sPositionID As String, ByVal sWorkTypeID As String, ByVal sEmpFlowRemark As String, ByVal sBusinessType As String, _
                                             ByRef returnDataTable As DataTable, ByRef message As String) As Boolean
        Dim result As Boolean = False
        returnDataTable = New DataTable()
        message = ""
        Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
        Dim dbcmd As DbCommand
        Dim strSQL As StringBuilder = New StringBuilder()
        strSQL.AppendLine(" SELECT ")
        strSQL.AppendLine(" P.CompID, P.EmpID, P.RankID, P.OrganID, P.DeptID, P.TitleID ")
        strSQL.AppendLine(" , EP.PositionID, EW.WorkTypeID, EF.EmpFlowRemarkID, OrF.BusinessType ")
        strSQL.AppendLine(" FROM Personal AS P ")
        strSQL.AppendLine(" LEFT JOIN EmpPosition AS EP ON EP.EmpID = P.EmpID AND EP.CompID = P.CompID ")
        strSQL.AppendLine(" LEFT JOIN EmpWorkType AS EW ON EW.EmpID = P.EmpID AND EW.CompID = P.CompID ")
        strSQL.AppendLine(" LEFT JOIN EmpFlow AS EF ON EF.CompID = P.CompID AND EF.EmpID = P.EmpID ")
        strSQL.AppendLine(" LEFT JOIN OrganizationFlow AS OrF ON  EF.OrganID = OrF.OrganID ")
        strSQL.AppendLine(" WHERE 0 = 0 ")
        strSQL.AppendLine(" AND P.WorkStatus = '1' ")
        If Not String.IsNullOrEmpty(sCompID) Then
            strSQL.AppendLine(" AND P.CompID = @CompID ")
        End If
        If Not String.IsNullOrEmpty(sEmpID) Then
            strSQL.AppendLine(" AND P.EmpID = @EmpID ")
        End If
        If Not String.IsNullOrEmpty(sOrganID) Then
            strSQL.AppendLine(" AND P.OrganID = @OrganID ")
        End If
        If Not String.IsNullOrEmpty(sDeptID) Then
            strSQL.AppendLine(" AND P.DeptID = @DeptID ")
        End If
        If Not String.IsNullOrEmpty(sRankIDTop) Then
            strSQL.AppendLine(" AND P.RankID >= @RankIDTop ")
        End If
        If Not String.IsNullOrEmpty(sRankIDBottom) Then
            strSQL.AppendLine(" AND P.RankID <= @RankIDBottom ")
        End If
        If Not String.IsNullOrEmpty(sTitleIDTop) Then
            strSQL.AppendLine(" AND P.TitleID >= @TitleIDTop ")
        End If
        If Not String.IsNullOrEmpty(sTitleIDBottom) Then
            strSQL.AppendLine(" AND P.TitleID <= @TitleIDBottom ")
        End If
        If Not String.IsNullOrEmpty(sPositionID) Then
            strSQL.AppendLine(" AND EP.PositionID = @PositionID ")
        End If
        If Not String.IsNullOrEmpty(sWorkTypeID) Then
            strSQL.AppendLine(" AND EW.WorkTypeID = @WorkTypeID ")
        End If
        If Not String.IsNullOrEmpty(sEmpFlowRemark) Then
            strSQL.AppendLine(" AND EF.EmpFlowRemarkID = @EmpFlowRemark ")
        End If
        If Not String.IsNullOrEmpty(sBusinessType) Then
            strSQL.AppendLine(" AND OrF.BusinessType = @BusinessType ")
        End If
        strSQL.AppendLine(" ORDER BY P.CompID, P.EmpID ")
        strSQL.AppendLine(" ; ")
        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
        db.AddInParameter(dbcmd, "@CompID", DbType.String, sCompID)
        db.AddInParameter(dbcmd, "@EmpID", DbType.String, sEmpID)
        db.AddInParameter(dbcmd, "@OrganID", DbType.String, sOrganID)
        db.AddInParameter(dbcmd, "@DeptID", DbType.String, sDeptID)
        db.AddInParameter(dbcmd, "@RankIDTop", DbType.String, sRankIDTop)
        db.AddInParameter(dbcmd, "@RankIDBottom", DbType.String, sRankIDBottom)
        db.AddInParameter(dbcmd, "@TitleIDTop", DbType.String, sTitleIDTop)
        db.AddInParameter(dbcmd, "@TitleIDBottom", DbType.String, sTitleIDBottom)
        db.AddInParameter(dbcmd, "@PositionID", DbType.String, sPositionID)
        db.AddInParameter(dbcmd, "@WorkTypeID", DbType.String, sWorkTypeID)
        db.AddInParameter(dbcmd, "@EmpFlowRemark", DbType.String, sEmpFlowRemark)
        db.AddInParameter(dbcmd, "@BusinessType", DbType.String, sBusinessType)
        Dim ds As DataSet = db.ExecuteDataSet(dbcmd)
        If ds.Tables.Count > 0 Then

            Dim dt = ds.Tables(0)
            If dt.Rows.Count > 0 Then
                returnDataTable = dt
                result = True
            Else
                message = "查無Personal資料[001]"
            End If
        Else
            message = "查無Personal資料[002]"
        End If
        Return result
    End Function
#End Region

#Region "OV8 OV9 刪除時的檢核"
    ''' <summary>
    ''' OV8 OV9 刪除時的檢核
    ''' </summary>
    ''' <param name="gvMain">GridView</param>
    ''' <returns>有錯帶訊息</returns>
    ''' <remarks></remarks>
    Public Shared Function OV8And9DeleteCheck(ByVal gvMain As GridView) As String
        Dim result As String = ""
        Dim checkCount = GvCheckCount(gvMain)
        If checkCount > 1 Then
            result = "刪除請選擇一筆資料刪除"
        ElseIf checkCount = 1 Then
            Dim strSQL As New StringBuilder
            Dim dataTable As New DataTable
            Dim compID As String = ""
            Dim flowCode As String = ""
            Dim flowSN As String = ""
            For intRow As Integer = 0 To gvMain.Rows.Count - 1
                If gvMain.Rows.Count >= 1 Then
                    Dim objChk As CheckBox = gvMain.Rows(intRow).FindControl("chk_gvMain")
                    If objChk.Checked Then
                        compID = UserProfile.SelectCompRoleID
                        flowCode = gvMain.DataKeys(intRow).Item("FlowCode").ToString
                        flowSN = gvMain.DataKeys(intRow).Item("FlowSN").ToString
                    End If
                End If
            Next
            Dim message As String = ""
            If HadHROverTimeMainData(compID, flowCode, flowSN, message) Then
                result = "簽核表單目前還在使用中，無法刪除此筆"
            End If
        End If
        Return result
    End Function
#End Region

#Region "OV8 OV9 修改時的檢核"
    ''' <summary>
    ''' OV8 OV9 修改時的檢核
    ''' </summary>
    ''' <param name="gvMain">GridView</param>
    ''' <returns>有錯帶訊息</returns>
    ''' <remarks></remarks>
    Public Shared Function OV8And9EditCheck(ByVal compID As String, ByVal flowCode As String, ByVal flowSN As String) As String
        Dim result As String = ""
        Dim message As String = ""
        If HadHROverTimeMainData(compID, flowCode, flowSN, message) Then
            result = "簽核表單目前還在使用中，請確認是否需要修改"
        End If
        Return result
    End Function
#End Region

#Region "OV8 OV9 新增時的檢核"
    ''' <summary>
    ''' OV8 OV9 新增時的檢核
    ''' </summary>
    ''' <param name="gvMain">GridView</param>
    ''' <returns>有錯帶訊息</returns>
    ''' <remarks></remarks>
    Public Shared Function OV8And9InsertCheck(ByVal compID As String, ByVal flowCode As String, ByVal flowSN As String) As String
        Dim result As String = ""
        Dim message As String = ""
        If HadHROverTimeMainData(compID, flowCode, flowSN, message) Then
            result = "簽核表單目前還在使用中，請確認是否需要新增"
        End If
        Return result
    End Function
#End Region

#Region "GridView中有勾選的筆數"
    ''' <summary>
    ''' GridView中有勾選的筆數
    ''' </summary>
    ''' <param name="gvMain">GridView</param>
    ''' <returns>勾選的筆數</returns>
    ''' <remarks></remarks>
    Public Shared Function GvCheckCount(ByVal gvMain As GridView) As Integer
        Dim checkCount As Integer = 0
        For c As Integer = 0 To gvMain.Rows.Count - 1 Step 1
            If gvMain.Rows.Count >= 1 Then
                Dim objChk As CheckBox = gvMain.Rows(c).FindControl("chk_gvMain")
                If objChk.Checked Then
                    checkCount = checkCount + 1
                End If
            End If
        Next
        Return checkCount
    End Function
#End Region

#Region "是否有使用中的流程"
    ''' <summary>
    ''' 是否有使用中的流程
    ''' </summary>
    ''' <param name="compID">compID</param>
    ''' <param name="flowCode">flowCode</param>
    ''' <param name="flowSN">flowSN</param>
    ''' <param name="message">回傳:訊息</param>
    ''' <returns>是否有使用中的流程</returns>
    ''' <remarks></remarks>
    Public Shared Function HadHROverTimeMainData(ByVal compID As String, ByVal flowCode As String, ByVal flowSN As String, ByRef message As String) As Boolean
        Dim result As Boolean = False
        message = ""
        Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
        Dim dbcmd As DbCommand
        Dim strSQL As StringBuilder = New StringBuilder()
        strSQL.AppendLine(" SELECT ")
        strSQL.AppendLine(" FlowFlag  ")
        strSQL.AppendLine(" FROM HROverTimeMain ")
        strSQL.AppendLine(" WHERE 0 = 0 ")
        strSQL.AppendLine(" AND CompID = @CompID ")
        strSQL.AppendLine(" AND FlowCode = @FlowCode ")
        strSQL.AppendLine(" AND FlowSN = @FlowSN ")
        strSQL.AppendLine(" AND FlowFlag = @FlowFlag ")
        strSQL.AppendLine(" ; ")
        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
        db.AddInParameter(dbcmd, "@CompID", DbType.String, compID)
        db.AddInParameter(dbcmd, "@FlowCode", DbType.String, flowCode)
        db.AddInParameter(dbcmd, "@FlowSN", DbType.String, flowSN)
        db.AddInParameter(dbcmd, "@FlowFlag", DbType.String, "1")
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
        Return result
    End Function
#End Region

End Class
