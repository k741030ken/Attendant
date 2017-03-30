'******************************************************************************
'功能說明：HR相關Function
'建立人員：Weicheng
'建立日期：2014.08.28
'******************************************************************************
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.IO
Imports System
Imports System.Data.OleDb

Public Class HR
#Region "共用Function"
    '取得Server現在日期/時間
    Public Function Get_DB_NowDateTime(ByVal DateTime_Type As Integer) As String
        'DateTime_Type 0-日期(yyyy/mm/dd) 1-時間(HH:mm:ss) 2-日期時間(yyyy/MM/dd HH:mm:ss)
        Dim strSQL As String
        Dim strDateTime As String

        strSQL = "Select getdate() "
        strDateTime = Bsp.DB.ExecuteScalar(strSQL, "eHRMSDB").ToString
        Select Case DateTime_Type
            Case 0
                Return Format(CDate(strDateTime), "yyyy/MM/dd")
            Case 1
                Return Format(CDate(strDateTime), "HH:mm:ss")
            Case 2
                Return Format(CDate(strDateTime), "yyyy/MM/dd HH:mm:ss")
            Case Else
                Return Format(CDate(strDateTime), "yyyy/MM/dd")
        End Select
        
    End Function
    '自行訂義條件Table之資料數
    Public Function IsDataExists(ByVal strTable As String, ByVal strWhere As String) As Boolean
        Dim strSQL As String
        strSQL = "Select Count(*) Cnt From " & strTable
        strSQL += " Where 1 = 1 " & strWhere
        Return IIf(Bsp.DB.ExecuteScalar(strSQL, "eHRMSDB") > 0, True, False)
    End Function
    '取Mail
    Public Function GetEMailAddress(ByVal strCompID As String, ByVal strEmpID As String) As String
        Dim strSQL As String
        If strEmpID = "Billboard" Then
            Return "input@sinopac.com"
        Else
            strSQL = "Select c.EMail "
            strSQL = strSQL & " from Personal as p "
            strSQL = strSQL & " join Communication as c on p.IDNo = c.IDNo "
            strSQL = strSQL & "where p.CompID = " & Bsp.Utility.Quote(strCompID)
            strSQL = strSQL & "and p.EmpID = " & Bsp.Utility.Quote(strEmpID)
            If Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB") = Nothing Then
                Return ""
            Else
                Return Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")
            End If

        End If

    End Function

    '取IDNo
    Public Function GetIDNo(ByVal strCompID As String, ByVal strEmpID As String) As String
        Dim strSQL As String

        strSQL = "Select IDNo "
        strSQL = strSQL & " from Personal "
        strSQL = strSQL & "where CompID = " & Bsp.Utility.Quote(strCompID)
        strSQL = strSQL & "and EmpID = " & Bsp.Utility.Quote(strEmpID)
        If Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB") = Nothing Then
            Return ""
        Else
            Return Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")
        End If


    End Function

    '20150528 wei add 身份證字號檢核
    Public Function funCheckIDNO(ByVal strIDNo As String) As Boolean
        Dim strLegalChar As String = ""
        Dim strFirstChar As String = ""
        Dim strSecondChar As String = ""
        Dim strSelectChar As String = ""
        Dim X As Integer = 0
        Dim X1 As Integer = 0
        Dim X2 As Integer = 0
        Dim Y As Integer = 0
        Dim intIdx As Integer = 0
        Dim B As Integer = 0

        Try
            If Len(strIDNo) <> 10 Then
                Return False
            End If

            'check the first character should be legal
            strLegalChar = "ABCDEFGHJKLMNPQRSTUVXYWZIO"
            strFirstChar = Mid(strIDNo, 1, 1)
            strLegalChar.IndexOf(strFirstChar)
            X = InStr(strLegalChar, strFirstChar)
            If X = 0 Then
                Return False
            End If

            'check the second character should be legal
            strLegalChar = "12"
            strSecondChar = Mid(strIDNo, 2, 1)
            B = InStr(strLegalChar, strSecondChar)
            If B = 0 Then
                Return False
            End If

            X = X + 9
            X1 = X \ 10    '取商
            X2 = X Mod 10  '取餘數

            If IsNumeric(Mid(strIDNo, 10, 1)) Then
                Y = X1 + 9 * X2 + CInt(Mid(strIDNo, 10, 1))
            Else
                Return False
            End If


            For intIdx = 2 To Len(strIDNo) - 1
                strSelectChar = Mid(strIDNo, intIdx, 1)
                If IsNumeric(strSelectChar) Then
                    Y = Y + (10 - intIdx) * CInt(strSelectChar)
                Else
                    Return False
                End If
            Next intIdx

            If (Y Mod 10) <> 0 Then
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
            'Bsp.Utility.ShowMessage(Me, "funCheckIDNO", ex)
            Return False
        End Try
    End Function
    '20150528 wei add 居留證號檢核
    Public Function funCheckResidentIDNo(ByVal strResidentIDNo As String) As Boolean
        Dim strLegalChar As String
        Dim strFirstChar As String
        Dim strSecondChar As String
        Dim strSelectChar As String
        Dim mstrF As String
        Dim mstrE As String
        Dim X As Integer, X1 As Integer, X2 As Integer
        Dim Y As Integer, intIdx As Integer
        Dim B As Integer

        mstrF = ""
        mstrE = ""

        Try
            If Len(strResidentIDNo) <> 10 Then
                Return False
            End If

            '一、統一證號編列規則：
            '共計十碼，第一碼為區域碼(同國民身分證)、第二碼為性別碼(入出境管理局使用ＡＢ；警察局外事科/課使用ＣＤ)、第三至九碼為流水號、
            '第十碼為檢查號碼。

            '檢查前兩碼是否為英文字母
            'check the first character should be legal
            strLegalChar = "ABCDEFGHJKLMNPQRSTUVXYWZIO"
            strFirstChar = Mid(strResidentIDNo, 1, 1)
            X = InStr(strLegalChar, strFirstChar)
            If X = 0 Then
                Return False
            End If

            'check the second character should be legal
            strLegalChar = "ABCDEFGHJKLMNPQRSTUVXYWZIO"
            strSecondChar = Mid(strResidentIDNo, 2, 1)
            B = InStr(strLegalChar, strSecondChar)
            If B = 0 Then
                Return False
            End If

            '二、檢查號碼計算規則：
            '第一碼英文字母轉換為二位數字碼(轉換之數字與國民身分證同)，分別乘以特定數；第二碼英文字母轉換成二位數字後，只取尾數乘以特定數；餘第三～九碼，亦分別乘以特定數。檢查號碼＝10－相乘後個位數相加總和之尾數。惟若相乘後個位數相加總和尾數為0，則逕以「0」為檢查號碼。
            '舉例: FA12345689
            '(Ｆ：轉換為15，Ａ轉換為10─＞取尾數「0」)
            '【第一碼區域及第二碼性別之英文碼，先依據下列數字表換算，惟性別轉換後之二位數字碼，只取尾數。】
            'A->10 ...... Z->33
            'I->34 O->35
            '1501234568(統 號)
            '×1987654321(特定數)
            '1507256528(不進位)
            '
            '1＋5＋0＋7＋2＋5＋6＋5＋2＋8
            '＝41(將相乘後個位數相加)

            X = X + 9
            X1 = X \ 10    '取商
            X2 = X Mod 10  '取餘數
            Y = (X1 + (9 * X2)) Mod 10

            B = B + 9
            X2 = B Mod 10  '取餘數

            mstrF = Y & X2 & Mid(strResidentIDNo, 3, 7)
            mstrE = Mid(strResidentIDNo, 10, 1)

            For intIdx = 2 To Len(mstrF)
                strSelectChar = Mid(mstrF, intIdx, 1)
                If IsNumeric(strSelectChar) Then
                    Y = Y + (10 - intIdx) * CInt(strSelectChar)
                Else
                    Return False
                End If
            Next intIdx

            '若尾數為0，則逕以「0」為檢查號碼
            '若尾數不為0，ex尾數1，檢查號碼 = 10 - 1 = 9
            If (Y Mod 10) = 0 Then
                If (Y Mod 10) <> CInt(mstrE) Then
                    Return False
                End If
            Else
                If (10 - (Y Mod 10)) <> CInt(mstrE) Then
                    Return False
                End If
            End If

            Return True

        Catch ex As Exception
            Throw ex
            'Bsp.Utility.ShowMessage(Me, "funCheckResidentIDNo", ex)
            Return False
        End Try
    End Function

    '20150624 wei add 檢核是否為PB類工作性質員工需繳交刑事紀錄證明
    '輸入工作性質參數-for HR2300,HR2320新進行員功能，員工都只有一個工作性質時使用
    Public Function funIsCredit(ByVal strCompID As String, ByVal strEmpID As String, Optional ByVal strWorkTypeID As String = "") As Boolean
        Dim strWhere As String
        If strWorkTypeID <> "" Then
            strWhere = " AND WorkTypeID = " & Bsp.Utility.Quote(strWorkTypeID) & " and PBFlag = '1' " '2015/07/07 Micky Modify
            If IsDataExists("WorkType", strWhere) Then
                Return True
            End If
        Else
            strWhere = " AND CompID = " & Bsp.Utility.Quote(strCompID) & " and EmpID = " & Bsp.Utility.Quote(strEmpID) '2015/07/07 Micky Modify
            strWhere = strWhere & " and WorkTypeID in (select WorkTypeID from WorkType where CompID = " & Bsp.Utility.Quote(strCompID) & " and PBFlag = '1') "
            If IsDataExists("EmpWorkType", strWhere) Then
                Return True
            End If
        End If

        Return False
    End Function
    '20150629 wei add 檢查輸入字串是否有存在非數字
    Function funCheckStr(iStr As String) As Boolean
        Dim iLen As Integer
        Dim ii As Integer
        iLen = Len(iStr)
        For ii = 1 To iLen
            If Asc(Mid(iStr, ii, 1)) < 48 Or Asc(Mid(iStr, ii, 1)) > 57 Then
                Return False
            End If
        Next ii

        Return True
    End Function
    '傳入開始試用日與試用月份
    '傳出試用期到期日
    '2015/08/27啟義：依月計算期滿日,有相當日採相當日之前一日為期滿日,無相當日者採相當月之末日為期滿日
    Public Function funGetProbationEndDate(strBeginDate As String, intMonth As Integer) As String
        Dim strTemp As String
        'strTemp = Format(DateAdd("m", intMonth, strBeginDate), "yyyy/mm/dd")
        'If Not strTemp = DateSerial(Year(strTemp), Month(strTemp) + 1, 0) Then
        '    Return Format(DateAdd("d", -1, strTemp), "yyyy/mm/dd")
        'Else
        '    Return strTemp
        'End If

        strTemp = DateAdd("m", intMonth, strBeginDate)
        If Day(strTemp) = Day(strBeginDate) Then
            Return DateAdd("d", -1, strTemp)
        Else
            Return strTemp
        End If
    End Function
    '20150702 wei add
    '*********************************************************************************************************
    '* 功能說明：取得員工的健保/勞保等級，可以選取等級或投保薪資/取得員工的勞退新制等級，可以選取等級或月提繳工資   *
    '* 傳入參數１：lngAmount ==> 單月薪資                                                                     *
    '* 傳入參數２：ResultField ==> "Levle" or "Amount"                                                       *
    '* 傳入參數３： ==> "Hea" or "Lab" or  "Retire"                                                            *
    '* 回傳值：健保投保薪資或等級/勞保投保薪資或等級/勞保新制月提繳工資或等級                                    *
    '********************************************************************************************************
    Public Function funGetLabHeaRetireLevel(ByVal lngAmount As Long, Optional ByVal ResultField As String = "Level", Optional ByVal LevelType As String = "") As String
        Dim strSql1 As String = ""
        Select Case LevelType
            Case "Hea"
                strSql1 = "Select Top 1 HeaLvl as Lvl, Amount From HealthLevel Where Amount >= " & CStr(lngAmount) & " Order by Amount"
            Case "Lab"
                If lngAmount <= 0 Then
                    Return "0"
                End If
                strSql1 = "Select Top 1 LabLvl as Lvl, Amount From LaborLevel Where Amount >= " & CStr(lngAmount) & " Order by Amount"
            Case "Retire"
                If lngAmount = 0 And ResultField <> "Level" Then
                    Return "0"
                End If
                strSql1 = "Select Top 1 RetireLvl as Lvl,Ubound, Amount From RetireLevel Where Ubound >= " & lngAmount & " Order by Ubound "
            Case Else
                Return "0"
        End Select

        'If IsDBNull(Bsp.DB.ExecuteDataSet(CommandType.Text, strSql1.ToString(), "eHRMSDB").Tables(0)) Then
        '2016/07/11 modify by Beatrice
        If Bsp.DB.ExecuteDataSet(CommandType.Text, strSql1.ToString(), "eHRMSDB").Tables(0).Rows.Count = 0 Then
            Select Case LevelType
                Case "Hea"
                    strSql1 = "Select Top 1 HeaLvl as Lvl, Amount From HealthLevel order by Amount Desc"
                Case "Lab"
                    strSql1 = "Select Top 1 LabLvl as Lvl, Amount From LaborLevel Order by Amount Desc"
                Case "Retire"
                    strSql1 = "Select Top 1 RetireLvl as Lvl,Ubound, Amount From RetireLevel Order by Ubound Desc"
                Case Else
                    Return "0"
            End Select

            'If IsDBNull(Bsp.DB.ExecuteDataSet(CommandType.Text, strSql1.ToString(), "eHRMSDB").Tables(0)) Then
            '2016/07/11 modify by Beatrice
            If Bsp.DB.ExecuteDataSet(CommandType.Text, strSql1.ToString(), "eHRMSDB").Tables(0).Rows.Count = 0 Then
                Return "0"
            Else
                If ResultField = "Level" Then
                    Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSql1.ToString(), "eHRMSDB").Tables(0).Rows(0)("Lvl").ToString()
                Else
                    Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSql1.ToString(), "eHRMSDB").Tables(0).Rows(0)("Amount").ToString()
                End If
            End If
        Else
            If ResultField = "Level" Then
                Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSql1.ToString(), "eHRMSDB").Tables(0).Rows(0)("Lvl").ToString()
            Else
                Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSql1.ToString(), "eHRMSDB").Tables(0).Rows(0)("Amount").ToString()
            End If
        End If
    End Function
#End Region
#Region "共用Function-Upload相關"
    Public Function funCheck_UploadTableName(ByVal FileName As String) As String
        Dim strExcelConn As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & FileName & ";Extended Properties='EXCEL 8.0;HDR=Yes;IMEX=1;'"
        Dim myExcelConn As OleDbConnection = Nothing
        myExcelConn = New OleDbConnection(strExcelConn)

        myExcelConn.Open()
        Dim SchemaTable As DataTable = myExcelConn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, Nothing)
        myExcelConn.Close()

        Return SchemaTable.Rows(0)("TABLE_NAME").ToString

    End Function
    '20160628 wei add
    Public Function funCheck_UploadTableName_Xlsx(ByVal FileName As String) As String
        Dim strExcelConn As String = "Provider=Microsoft.ACE.OLEDB.12.0;;Data Source=" & FileName & ";Extended Properties='EXCEL 8.0;HDR=Yes;IMEX=1;'"    '20160513 wei modify
        'Dim strExcelConn As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & FileName & ";Extended Properties='EXCEL 8.0;HDR=Yes;IMEX=1;'"
        Dim myExcelConn As OleDbConnection = Nothing
        myExcelConn = New OleDbConnection(strExcelConn)

        myExcelConn.Open()
        Dim SchemaTable As DataTable = myExcelConn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, Nothing)
        myExcelConn.Close()

        Return SchemaTable.Rows(0)("TABLE_NAME").ToString

    End Function
    Public Function funCheck_UploadCount(ByVal FileName As String, ByVal strTableName As String) As Integer

        Dim intReadCount As Integer = 0

        Dim strExcelConn As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & FileName & ";Extended Properties='EXCEL 8.0;HDR=Yes;IMEX=1;'"

        Dim strExcelSelect As String = "SELECT Count(*) FROM " & strTableName

        Dim myExcelConn As OleDbConnection = Nothing
        myExcelConn = New OleDbConnection(strExcelConn)



        Dim myExcelCommand As OleDbCommand = New OleDbCommand(strExcelSelect, myExcelConn)

        Dim ds As DataSet = New DataSet
        Dim myDataAdapter As OleDbDataAdapter = New OleDbDataAdapter(myExcelCommand)
        myExcelConn.Open()
        myDataAdapter.Fill(ds, "ReadData")
        myExcelConn.Close()
        Dim MyTableCount As DataTable
        MyTableCount = ds.Tables("ReadData")

        intReadCount = CInt(MyTableCount.Rows(0).Item(0).ToString())

        Return intReadCount

    End Function
    '20160628 wei add
    Public Function funCheck_UploadCount_Xlsx(ByVal FileName As String, ByVal strTableName As String) As Integer

        Dim intReadCount As Integer = 0
        Dim strExcelConn As String = "Provider=Microsoft.ACE.OLEDB.12.0;;Data Source=" & FileName & ";Extended Properties='EXCEL 8.0;HDR=Yes;IMEX=1;'"    '20160513 wei modify
        'Dim strExcelConn As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & FileName & ";Extended Properties='EXCEL 8.0;HDR=Yes;IMEX=1;'"

        Dim strExcelSelect As String = "SELECT Count(*) FROM " & strTableName

        Dim myExcelConn As OleDbConnection = Nothing
        myExcelConn = New OleDbConnection(strExcelConn)



        Dim myExcelCommand As OleDbCommand = New OleDbCommand(strExcelSelect, myExcelConn)

        Dim ds As DataSet = New DataSet
        Dim myDataAdapter As OleDbDataAdapter = New OleDbDataAdapter(myExcelCommand)
        myExcelConn.Open()
        myDataAdapter.Fill(ds, "ReadData")
        myExcelConn.Close()
        Dim MyTableCount As DataTable
        MyTableCount = ds.Tables("ReadData")

        intReadCount = CInt(MyTableCount.Rows(0).Item(0).ToString())

        Return intReadCount

    End Function
#End Region

#Region "Company相關"
    Public Function GetHRCompName(ByVal CompID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select CompName From Company")
        strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(CompID))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

    Public Function GetHRCompanyInfo(ByVal CompID As String, Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder

        If FieldNames = "" Then FieldNames = "*"

        strSQL.AppendLine("Select " & FieldNames)
        strSQL.AppendLine("From Company")
        strSQL.AppendLine("Where 1=1")
        If CompID <> "" Then strSQL.AppendLine("And CompID = " & Bsp.Utility.Quote(CompID))
        If CondStr <> "" Then strSQL.AppendLine(CondStr)

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

    '取試用考核歸屬公司體系代碼
    Public Function GetProbation_Comp(ByVal CompID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select Probation From Company")
        strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(CompID))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

    '20150629
    '*****************************************************************************************
    '* 功能說明：取得報到文件歸屬公司代碼
    '* 傳入參數：strCompID公司代碼
    '* 回傳值：報到文件歸屬公司代碼
    '*****************************************************************************************
    Public Function funGetCheckInFileCompID(strCompID As String) As String
        Dim strSql As String

        strSql = "Select CheckInFile from Company where CompID=" & Bsp.Utility.Quote(strCompID)

        If Bsp.DB.ExecuteScalar(strSql.ToString(), "eHRMSDB") = Nothing Then
            Return ""
        Else
            Return Bsp.DB.ExecuteScalar(strSql.ToString(), "eHRMSDB")
        End If
    End Function
    '*****************************************************************************************
    '* 功能說明：取得計薪作業歸屬公司代碼
    '* 傳入參數：strCompID公司代碼
    '* 回傳值：計薪作業歸屬公司代碼
    '*****************************************************************************************
    Public Function funGetPayrollCompID(ByVal strCompID As String) As String
        Dim strSqlTemp As String
        strSqlTemp = "select Payroll from Company where CompID = " & Bsp.Utility.Quote(strCompID)

        If Bsp.DB.ExecuteScalar(strSqlTemp.ToString(), "eHRMSDB") = Nothing Then
            Return ""
        Else
            Return Bsp.DB.ExecuteScalar(strSqlTemp.ToString(), "eHRMSDB")
        End If

    End Function
    '20150630 wei add
    '*****************************************************************************************
    '* 功能說明：取得公司是否使用證券的三家團保公司
    '* 傳入參數：strCompID公司代碼
    '* 回傳值：是：true 否：False
    '*****************************************************************************************
    Public Function funGetSPHSC1Grp(ByVal strCompID As String) As Boolean
        Dim strSqlTemp As String
        strSqlTemp = "select SPHSC1GrpFlag from Company where CompID = " & Bsp.Utility.Quote(strCompID)

        If Bsp.DB.ExecuteScalar(strSqlTemp.ToString(), "eHRMSDB") = Nothing Then
            Return False
        Else
            If Bsp.DB.ExecuteScalar(strSqlTemp.ToString(), "eHRMSDB") = "1" Then
                Return True
            Else
                Return False
            End If
        End If
    End Function
    '20150702 wei add
    '*****************************************************************************************************************
    '* 功能說明：取公司伙食津貼值
    '* 傳入參數：CompID
    '* 傳出參數：MealPay 公司伙食津貼
    '*****************************************************************************************************************
    Public Function FunGetMealPay(strCompID As String) As Integer
        Dim strSql As String
        strSql = " Select isnull(MealPay,0) as MealPay From Parameter "
        strSql = strSql & " Where CompID= " & Bsp.Utility.Quote(strCompID)

        If Bsp.DB.ExecuteScalar(strSql.ToString(), "eHRMSDB") = Nothing Then
            Return 0
        Else
            Return Bsp.DB.ExecuteScalar(strSql.ToString(), "eHRMSDB")
        End If
    End Function
#End Region
#Region "Organization相關"
    Public Function ChkOrganIsVlaid(ByVal strCompID As String, ByVal strOrganID As String) As Boolean
        Dim bolResult As Boolean = False

        Dim strSQL As String
        Dim strReturn As String = ""

        Try
            '2016/04/29 SPHBKC資料已併入Organization中
            'If strCompID = "SPHBKC" Then
            '    strSQL = "Select InValidFlag from COrganization Where CompID = " & Bsp.Utility.Quote(strCompID) & " and OrganID = " & Bsp.Utility.Quote(strOrganID)
            'Else
            strSQL = "Select InValidFlag from Organization Where CompID = " & Bsp.Utility.Quote(strCompID) & " and OrganID = " & Bsp.Utility.Quote(strOrganID)
            'End If

            strReturn = Bsp.DB.ExecuteScalar(strSQL, "eHRMSDB")
            If strReturn = "0" Then
                bolResult = True
            End If
        Catch ex As Exception
            Throw
        End Try
        Return bolResult
    End Function
    Public Function GetHROrganName(ByVal CompID As String, ByVal OrganID As String) As DataTable
        Dim strSQL As New StringBuilder()

        '2016/04/29 SPHBKC資料已併入Organization中
        'If CompID = "SPHBKC" Then
        '    strSQL.AppendLine("Select OrganName From COrganization")
        'Else
        strSQL.AppendLine("Select OrganName From Organization")
        'End If

        strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine("And OrganID = " & Bsp.Utility.Quote(OrganID))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
    Public Function GetHROrganInfo(ByVal CompID As String, ByVal OrganID As String, Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder

        If FieldNames = "" Then FieldNames = "*"

        strSQL.AppendLine("Select " & FieldNames)

        '2016/04/29 SPHBKC資料已併入Organization中
        'If CompID = "SPHBKC" Then
        '    strSQL.AppendLine("From COrganization")
        'Else
        strSQL.AppendLine("From Organization")
        'End If

        strSQL.AppendLine("Where 1=1")
        If CompID <> "" Then strSQL.AppendLine("And CompID = " & Bsp.Utility.Quote(CompID))
        If OrganID <> "" Then strSQL.AppendLine("And OrganID = " & Bsp.Utility.Quote(OrganID))
        If CondStr <> "" Then strSQL.AppendLine(CondStr)

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region
#Region "OrganizationFlow相關"
    Public Function ChkOrganFlowIsVlaid(ByVal strCompID As String, ByVal strOrganID As String) As Boolean
        Dim bolResult As Boolean = False

        Dim strSQL As String
        Dim strReturn As String = ""

        Try
            '2016/04/29 SPHBKC資料已併入OrganizationFlow中
            'If strCompID = "SPHBKC" Then
            '    strSQL = "Select InValidFlag from COrganizationFlow Where OrganID = " & Bsp.Utility.Quote(strOrganID)
            'Else
            strSQL = "Select InValidFlag from OrganizationFlow Where OrganID = " & Bsp.Utility.Quote(strOrganID)
            'End If

            strReturn = Bsp.DB.ExecuteScalar(strSQL, "eHRMSDB")
            If strReturn = "0" Then
                bolResult = True
            End If
        Catch ex As Exception
            Throw
        End Try
        Return bolResult
    End Function
    '20161020 wei add 取簽核組組織名稱
    Public Function GetHROrganFlowName(ByVal CompID As String, ByVal OrganID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select OrganName From OrganizationFlow")
        strSQL.AppendLine("Where OrganID = " & Bsp.Utility.Quote(OrganID))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region
#Region "Personal相關"
    Public Function GetHREmpName(ByVal CompID As String, ByVal EmpID As String) As DataTable
        Dim strSQL As New StringBuilder()

        '2016/04/29 SPHBKC資料已併入Personal中
        'If CompID = "SPHBKC" Then
        '    strSQL.AppendLine("Select UserName as NameN From CPersonal")
        'Else
        strSQL.AppendLine("Select NameN From Personal")
        'End If

        strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine("And EmpID = " & Bsp.Utility.Quote(EmpID))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
    '20150529 wei add 取人員現職部門
    Public Function GetHREmpDeptID(ByVal CompID As String, ByVal EmpID As String) As DataTable
        Dim strSQL As New StringBuilder()

        '2016/04/29 SPHBKC資料已併入Personal中
        'If CompID = "SPHBKC" Then
        '    strSQL.AppendLine("Select DeptID From CPersonal")
        'Else
        strSQL.AppendLine("Select DeptID From Personal")
        'End If

        strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine("And EmpID = " & Bsp.Utility.Quote(EmpID))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region
#Region "Rank相關"
    Public Function GetRankIDInfo(ByVal CompID As String) As DataTable
        Dim strSQL As String

        '2016/04/29 SPHBKC資料已併入Rank中
        'If CompID = "SPHBKC" Then
        '    strSQL = " select Code as RankID from CHRCodeMap where TabName='Personal' and	FldName='RankID' "
        '    strSQL = strSQL & " order by Code "
        'Else
        strSQL = " select RankID from Rank where CompID = " & Bsp.Utility.Quote(CompID)
        strSQL = strSQL & " order by RankID "
        'End If

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

    Public Function GetRankID(ByVal CompID As String, ByVal RankID As String) As String
        Dim strSQL As String

        '2016/04/29 SPHBKC資料已併入Rank中
        'If CompID = "SPHBKC" Then
        '    strSQL = " select Code from RankID where TabName='Personal' and	FldName='RankID' "
        '    strSQL = strSQL & " and Code = " & Bsp.Utility.Quote(RankID)
        '    strSQL = strSQL & " order by Code "
        'Else
        strSQL = " select RankID from Rank where CompID = " & Bsp.Utility.Quote(CompID)
        strSQL = strSQL & " and RankID = " & Bsp.Utility.Quote(RankID)
        strSQL = strSQL & " order by RankID "
        'End If

        If Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB") = Nothing Then
            Return ""
        Else
            Return Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")
        End If

    End Function
    '20150624 wei add
    Public Function FunGetRankIDMap(strCompID As String, strRankID As String) As String
        Dim strSQL As String

        strSQL = " Select RankIDMap From RankMapping "
        strSQL = strSQL & " Where CompID= " & Bsp.Utility.Quote(strCompID)
        strSQL = strSQL & " And RankID = " & Bsp.Utility.Quote(strRankID)

        If Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB") = Nothing Then
            Return ""
        Else
            Return Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")
        End If

    End Function

    '2015/09/02 Micky Add
    Public Function funGetRankID(strCompID As String, strEmpID As String) As String
        Dim strSQL As String
        strSQL = " select isnull(RankID,'') RankID from Personal "
        strSQL = strSQL & " Where CompID = " & Bsp.Utility.Quote(strCompID) & " AND EmpID = " & Bsp.Utility.Quote(strEmpID)

        If Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB") = Nothing Then
            Return ""
        Else
            Return Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")
        End If
    End Function

#End Region
#Region "Title相關"
    Public Function GetTitleID(ByVal CompID As String, ByVal RankID As String) As DataTable
        Dim strSQL As String

        strSQL = " select TitleID + '-' + TitleName as TitleID from Title where CompID = " & Bsp.Utility.Quote(CompID) & "and RankID = " & Bsp.Utility.Quote(RankID)

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
    Public Function GetTitleName(ByVal CompID As String, ByVal RankID As String, ByVal TitleID As String) As DataTable
        Dim strSQL As String

        strSQL = " select TitleName from Title where CompID = " & Bsp.Utility.Quote(CompID) & "and RankID = " & Bsp.Utility.Quote(RankID) & "and TitleID = " & Bsp.Utility.Quote(TitleID)

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
    Public Function GetCTitleName(ByVal CompID As String, ByVal RankID As String, ByVal TitleID As String) As DataTable
        Dim strSQL As String

        strSQL = " select TitleName from CTitle where CompID = " & Bsp.Utility.Quote(CompID) & "and RankID = " & Bsp.Utility.Quote(RankID) & "and TitleID = " & Bsp.Utility.Quote(TitleID)

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
    Public Function GetTitleInfo(ByVal RankID As String, ByVal FieldNames As String, Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames = "" Then FieldNames = "*"
        strSQL.AppendLine("Select " & FieldNames)
        strSQL.AppendLine("From Title ")
        'strSQL.AppendLine("inner join RankMapping M on T.CompID = M.CompID and T.RankID = M.RankID")
        strSQL.AppendLine("Where 1 = 1")
        If RankID <> "" Then strSQL.AppendLine("And RankID = " & Bsp.Utility.Quote(RankID))
        If CondStr <> "" Then strSQL.AppendLine(CondStr)

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
    Public Function GetCTitleInfo(ByVal RankID As String, ByVal FieldNames As String, Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames = "" Then FieldNames = "*"
        strSQL.AppendLine("Select " & FieldNames)
        strSQL.AppendLine("From CTitle ")
        'strSQL.AppendLine("inner join RankMapping M on T.CompID = M.CompID and T.RankID = M.RankID")
        strSQL.AppendLine("Where 1 = 1")
        If RankID <> "" Then strSQL.AppendLine("And RankID = " & Bsp.Utility.Quote(RankID))
        If CondStr <> "" Then strSQL.AppendLine(CondStr)

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function

    Public Function GetRETitleIDName(ByVal CompID As String, ByVal RankID As String, ByVal TitleID As String) As String
        Dim strSQL As String

        strSQL = " select TitleID + '-' + TitleName as TitleID "
        strSQL = strSQL & "from Title "
        strSQL = strSQL & " where CompID = " & Bsp.Utility.Quote(CompID)
        strSQL = strSQL & " and RankID = " & Bsp.Utility.Quote(RankID)
        strSQL = strSQL & " and TitleID = " & Bsp.Utility.Quote(TitleID)

        If Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB") = Nothing Then
            Return ""
        Else
            Return Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")
        End If
    End Function
#End Region
#Region "WorkType相關"
    '20150529 wei add 判斷工作性質是否有效
    Public Function ChkWorkTypeIsVlaid(ByVal strCompID As String, ByVal strWorkTypeID As String) As Boolean
        Dim bolResult As Boolean = False

        Dim strSQL As String
        Dim strReturn As String = ""

        Try
            '2016/04/29 SPHBKC資料已併入WorkType中
            'If strCompID = "SPHBKC" Then
            '    strSQL = "Select NotShowFlag from CHRCodeMap Where TabName='EmpWorkType' and FldName='WorkTypeID' and Code = " & Bsp.Utility.Quote(strWorkTypeID)
            'Else
            strSQL = "Select InValidFlag from WorkType Where CompID = " & Bsp.Utility.Quote(strCompID) & " and WorkTypeID = " & Bsp.Utility.Quote(strWorkTypeID)
            'End If

            strReturn = Bsp.DB.ExecuteScalar(strSQL, "eHRMSDB")
            If strReturn = "0" Then
                bolResult = True
            End If
        Catch ex As Exception
            Throw
        End Try
        Return bolResult
    End Function
#End Region
#Region "Position相關"
    '20150529 wei add 判斷職位是否有效
    Public Function ChkPositionIsVlaid(ByVal strCompID As String, ByVal strPositionID As String) As Boolean
        Dim bolResult As Boolean = False

        Dim strSQL As String
        Dim strReturn As String = ""

        Try
            strSQL = "Select InValidFlag from Position Where CompID = " & Bsp.Utility.Quote(strCompID) & " and PositionID = " & Bsp.Utility.Quote(strPositionID)

            strReturn = Bsp.DB.ExecuteScalar(strSQL, "eHRMSDB")
            If strReturn = "0" Then
                bolResult = True
            End If
        Catch ex As Exception
            Throw
        End Try
        Return bolResult
    End Function
#End Region
#Region "WorkSite相關"
    Public Function GetWorkSite(ByVal CompID As String, ByVal OrganID As String) As DataTable
        Dim strSQL As String

        '2016/04/29 SPHBKC資料已併入Organization中
        'If CompID = "SPHBKC" Then
        '    strSQL = " select distinct Code as WorkSiteID,Code + '-' + CodeName as FullName from CHRCodeMap where TabName='Personal' and	FldName='WorkSiteID' order by Code"
        'Else
        If OrganID = "" Then
            strSQL = " select distinct W.WorkSiteID,W.WorkSiteID + '-' + W.Remark as FullName from WorkSite W left join Organization O on W.CompID = O.CompID and W.WorkSiteID = O.WorkSiteID where W.CompID = " & Bsp.Utility.Quote(CompID) & " order by WorkSiteID"
        Else
            strSQL = " select distinct W.WorkSiteID,W.WorkSiteID + '-' + W.Remark as FullName from WorkSite W left join Organization O on W.CompID = O.CompID and W.WorkSiteID = O.WorkSiteID where W.CompID = " & Bsp.Utility.Quote(CompID) & " and O.OrganID = " & Bsp.Utility.Quote(OrganID) & " order by WorkSiteID"
        End If
        'End If


        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region
#Region "導入惠悅註記"
    Public Function IsRankIDMapFlag(ByVal CompID As String) As Boolean
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine("Select Count(*) From Company")
        strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine("And RankIDMapFlag = '1'")

        If CInt(Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")) = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region
#Region "Employee相關"
    Public Function GetReasonName(ByVal Reason As String) As DataTable
        Dim strSQL As String

        strSQL = " select Reason + '-' + Remark as ReasonName from EmployeeReason where Reason = " & Bsp.Utility.Quote(Reason)

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region
#Region "FillWorkStatus：填入任職狀況"
    Public Shared Sub FillWorkStatus(ByVal objDDL As DropDownList)
        Dim objHR As New HR
        Try
            Using dt As DataTable = objHR.GetWorkStatusInfo("", "Rtrim(WorkCode) as WorkCode,Remark,Rtrim(WorkCode) + ' ' + Remark as FullName", "")
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "FullName"
                    .DataValueField = "WorkCode"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetWorkStatusInfo(ByVal WorkCode As String, Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames = "" Then FieldNames = "*"

        strSQL.AppendLine("Select " & FieldNames)
        strSQL.AppendLine("From WorkStatus")
        strSQL.AppendLine("Where 1 = 1")
        If WorkCode <> "" Then strSQL.AppendLine("And Reason = " & Bsp.Utility.Quote(WorkCode))
        If CondStr <> "" Then strSQL.AppendLine(CondStr)
        strSQL.AppendLine("Order by WorkCode")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region
#Region "MailLog"
    Public Function GetMailSeq(strCreateTime As String) As String
        Dim strSQL As New StringBuilder
        Dim intSeq As Integer = 0

        strSQL.AppendLine("Select IsNull(Max(Seq), 0) + 1 as MaxSeq from MailLog where CreateTime = " & Bsp.Utility.Quote(strCreateTime))

        If IsDBNull(Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")) Then
            intSeq = 1
        Else
            intSeq = Bsp.DB.ExecuteScalar(strSQL.ToString(), "eHRMSDB")
        End If

        Return intSeq
    End Function
    Public Function AddMailLog(ByVal beMailLog As beMailLog.Row, Optional bolInsertMailControl As Boolean = False) As Boolean
        Dim bsMailLog As New beMailLog.Service()

        beMailLog.EMail.Value = GetEMailAddress(beMailLog.AcceptorCompID.Value, beMailLog.Acceptor.Value)

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try

                bsMailLog.Insert(beMailLog, tran)

                If bolInsertMailControl Then
                    Dim strSQL As New StringBuilder()

                    strSQL.AppendLine("Insert into MailControl (")
                    strSQL.AppendLine("CreateTime,")
                    strSQL.AppendLine("Seq,")
                    strSQL.AppendLine("AcceptorCompID,")
                    strSQL.AppendLine("Acceptor,")
                    strSQL.AppendLine("OutOfDate")
                    strSQL.AppendLine(") values (")
                    strSQL.AppendLine(Bsp.Utility.Quote(Format(CDate(beMailLog.CreateTime.Value), "yyyy/MM/dd HH:hh:ss")))
                    strSQL.AppendLine("," & Bsp.Utility.Quote(beMailLog.Seq.Value))
                    strSQL.AppendLine("," & Bsp.Utility.Quote(beMailLog.AcceptorCompID.Value))
                    strSQL.AppendLine("," & Bsp.Utility.Quote(beMailLog.Acceptor.Value))
                    strSQL.AppendLine("," & Bsp.Utility.Quote(Format(CDate(DateAdd("d", 15, beMailLog.CreateTime.Value)), "yyyy/MM/dd HH:hh:ss")) & ")")

                    Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), tran, "eHRMSDB")
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
#Region "SignCount:計算待辦數字"
    Public Function SignCount(ByVal CompID As String, ByVal UserID As String) As Integer
        Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
        Dim dbCommand As DbCommand = db.GetStoredProcCommand("SP_SignCount")
        Dim intSignCount As Integer

        db.AddInParameter(dbCommand, "@CompID", DbType.String, CompID)
        db.AddInParameter(dbCommand, "@EmpID", DbType.String, UserID)

        db.AddOutParameter(dbCommand, "@intSignCount", DbType.Int32, 4)

        db.ExecuteNonQuery(dbCommand)

        intSignCount = db.GetParameterValue(dbCommand, "@intSignCount")

        Return intSignCount
    End Function
    Public Function SignCountPromote(ByVal CompID As String, ByVal UserID As String) As Integer
        Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
        Dim dbCommand As DbCommand = db.GetStoredProcCommand("SP_SignCountPromote")
        Dim intSignCount As Integer

        db.AddInParameter(dbCommand, "@CompID", DbType.String, CompID)
        db.AddInParameter(dbCommand, "@EmpID", DbType.String, UserID)

        db.AddOutParameter(dbCommand, "@intSignCount", DbType.Int32, 4)

        db.ExecuteNonQuery(dbCommand)

        intSignCount = db.GetParameterValue(dbCommand, "@intSignCount")

        Return intSignCount
    End Function
#End Region

#Region "銀行帳號檢核"
#Region "檢核入帳帳號"
    Public Function funCheckAccount(ByVal strBank As String, ByVal strAccount As String) As Integer
        Dim intRtnCode As Integer

        Select Case strBank
            Case "807"
                '            strBankName = "永豐銀行"
                intRtnCode = gfunCheckAccount_BSP(strAccount)
            Case "008"
                '            strBankName = "華南銀行"
                intRtnCode = gfunCheckAccount_008(strAccount)
            Case "009"
                '            strBankName = "彰化銀行"
                intRtnCode = gfunCheckAccount_CHB(strAccount)
            Case "010"
                '            strBankName = "花旗台灣（華僑銀行）"
                intRtnCode = gfunCheckAccount_010(strAccount)
            Case "011"
                '            strBankName = "上海銀行"
                intRtnCode = gfunCheckAccount_011(strAccount)
            Case "013"
                '            strBankName = "國泰世華商業銀行"
                intRtnCode = gfunCheckAccount_UWCCB(strAccount)
            Case "017"
                '            strBankName = "兆豐銀行"
                intRtnCode = gfunCheckAccount_017(strAccount)
            Case "700"
                '            strBankName = "郵局"
                intRtnCode = gfunCheckAccount_PO(strAccount)
            Case "822"
                '            strBankName = "中國信託商業銀行"
                intRtnCode = gfunCheckAccount_CTCB(strAccount)
                If intRtnCode > 0 Then
                    intRtnCode = 1
                End If
            Case "012"
                '            strBankName = "台北富邦銀行"
                intRtnCode = gfunCheckAccount_FB(strAccount)
            Case "928"
                '            strBankName = "板橋農會"
                intRtnCode = gfunCheckAccount_928(strAccount)
            Case "816"
                '            strBankName = "安泰商業銀行"
                intRtnCode = gfunCheckAccount_816(strAccount)
            Case "050"
                '            strBankName = "台灣中小企銀"
                intRtnCode = gfunCheckAccount_050(strAccount)
            Case "007"
                '            strBankName = "第一銀行"
                intRtnCode = gfunCheckAccount_007(strAccount)
            Case Else
                intRtnCode = -2
        End Select

        Return intRtnCode


    End Function

#End Region
#Region "永豐銀行"
    Public Function gfunCheckAccount_BSP(strAccount As String) As Integer
        Dim arrA(14) As Integer
        Dim arrB(7) As Integer
        Dim A1, A2 As Integer
        Dim A3 As Long
        Dim B As Long
        Dim c, D, E As Integer
        Dim i As Integer
        Dim lengthB As Integer
        Dim Account_BSP As Integer

        Try
            If Len(strAccount) <> 14 Then
                Account_BSP = 14
                Return Account_BSP
            End If

            For i = 1 To 14
                arrA(i) = Mid(strAccount, i, 1)
            Next i

            For i = -1 To 1
                A1 = A1 + arrA(2 + i) * 10 ^ (1 - i)
                A2 = A2 + arrA(5 + i) * 10 ^ (1 - i)
            Next i

            For i = -3 To 3
                A3 = A3 + arrA(10 + i) * 10 ^ (3 - i)
            Next i

            B = A1 + A2 + A3
            If B > 9999999 Then
                B = B Mod 10000000
            End If

            lengthB = Len(Str(B)) - 1

            For i = 1 To 7
                Select Case i
                    Case 1 To (lengthB)
                        arrB(7 - lengthB + i) = Mid(B, i, 1)
                    Case Is > lengthB
                        arrB(8 - i) = 0
                End Select
            Next i

            For i = 1 To 6
                c = c + (i + 1) * arrB(i)
            Next i
            c = c + 2 * arrB(7)

            D = c Mod 9

            E = 9 - D
            If E = arrA(14) Then
                Account_BSP = 0
            Else
                Account_BSP = -1
            End If

            Return Account_BSP
        Catch ex As Exception
            gfunCheckAccount_BSP = -1
            Return Account_BSP
        End Try
    End Function
#End Region

#Region "華南銀行"
    Public Function gfunCheckAccount_008(ByVal strAccount As String) As Integer
        Dim arrA(11) As Integer
        Dim arrB() As Integer = {7, 9, 3, 7, 9, 3, 7, 9, 3, 7, 9}
        Dim A1 As Integer
        Dim B1 As Integer
        Dim i As Integer
        Dim Account_008 As Integer

        Try
            If Len(strAccount) <> 12 Then
                Account_008 = 12
                Return Account_008
            End If

            For i = 0 To 11
                arrA(i) = CInt(Mid(strAccount, i + 1, 1))
            Next i

            For i = 0 To 10
                A1 = A1 + CInt(arrA(i) * arrB(i))
            Next i

            B1 = 11 - (A1 Mod 11)

            If B1 = arrA(11) Then
                Account_008 = 0
            Else
                Account_008 = -1
            End If

            Return Account_008
        Catch ex As Exception
            Account_008 = -1
            Return Account_008
        End Try
    End Function
#End Region

#Region "彰化銀行"
    Function gfunCheckAccount_CHB(strAccount As String) As Integer
        Dim aryWeight() As Integer = {2, 1, 9, 0, 7, 6, 5, 4, 3, 2, 1}
        Dim iLoop As Integer
        Dim intTotal As Integer
        Dim intChkCode As Integer
        Dim strAct As String
        Dim Account_CHB As Integer

        Try
            strAct = Trim(strAccount)
            If Len(strAct) <> 14 Then
                Account_CHB = 14
                Return Account_CHB
            End If

            intTotal = 0
            For iLoop = LBound(aryWeight) To UBound(aryWeight)
                intTotal = intTotal + (aryWeight(iLoop) * Val(Mid(strAct, iLoop + 1, 1)) Mod 10)
            Next iLoop

            intChkCode = 10 - (intTotal Mod 10)
            If intChkCode = 10 Then intChkCode = 0
            If intChkCode <> Val(Mid(strAct, 12, 1)) Then
                Account_CHB = -1
            Else
                Account_CHB = 0
            End If

            Return Account_CHB
        Catch ex As Exception
            Account_CHB = -1
            Return Account_CHB
        End Try
    End Function
#End Region

#Region "花旗台灣(華僑銀行)"
    Public Function gfunCheckAccount_010(ByVal strAccount As String) As Integer
        Dim arrA(13) As Integer
        Dim arrB() As Integer = {5, 5, 4, 3, 2, 7, 7, 7, 6, 5, 4, 3, 2}
        Dim A1 As Integer
        Dim B1 As Integer
        Dim C1 As Integer
        Dim i As Integer
        Dim Account_010 As Integer

        Try
            If Len(strAccount) <> 14 Then
                Account_010 = 14
                Return Account_010
            End If

            For i = 0 To 13
                arrA(i) = CInt(Mid(strAccount, i + 1, 1))
            Next i

            For i = 0 To 12
                A1 = A1 + CInt(arrA(i) * arrB(i))
            Next i

            '合計除以11之餘數，若餘數為0或1則檢查碼為0
            C1 = (A1 Mod 11)
            If C1 = 0 Or C1 = 1 Then
                B1 = 0
            Else
                '檢查碼 = 11 - 合計除以11之餘數
                B1 = 11 - C1
            End If

            If B1 = arrA(13) Then
                Account_010 = 0
            Else
                Account_010 = -1
            End If

            Return Account_010
        Catch ex As Exception
            Account_010 = -1
            Return Account_010
        End Try
    End Function
#End Region

#Region "上海銀行"
    Public Function gfunCheckAccount_011(ByVal strAccount As String) As Integer
        Dim arrA(13) As Integer
        Dim arrB() As Integer = {7, 6, 5, 4, 3, 2, 1, 7, 6, 5, 4, 3, 2}
        Dim A1 As Integer
        Dim i As Integer
        Dim intCheckSum As Integer
        Dim Account_011 As Integer

        Try
            If Len(strAccount) <> 14 Then
                Account_011 = 14
                Return Account_011
            End If

            For i = 0 To 13
                arrA(i) = CInt(Mid(strAccount, i + 1, 1))
            Next i

            For i = 0 To 12
                A1 = A1 + CInt(Right(CStr(CInt(arrA(i) * arrB(i))), 1))
            Next i

            intCheckSum = CInt(Right(CStr(A1), 1))
            If intCheckSum = 0 Then
                If intCheckSum = arrA(13) Then
                    Account_011 = 0
                Else
                    Account_011 = -1
                End If
            Else
                If 10 - intCheckSum = arrA(13) Then
                    Account_011 = 0
                Else
                    Account_011 = -1
                End If
            End If
            Return Account_011
        Catch ex As Exception
            Account_011 = -1
            Return Account_011
        End Try
    End Function
#End Region

#Region "國泰世華商業銀行"
    Function gfunCheckAccount_UWCCB(strAccount As String) As Integer
        Dim strAct As String
        Dim aryWeight() As Integer = {6, 5, 4, 3, 2, 7, 6, 5, 4, 3, 2}
        Dim iLoop As Integer
        Dim intTotal As Integer
        Dim intRemainder As Integer
        Dim intChkCode As Integer
        Dim Account_UWCCB As Integer

        Try
            strAct = Trim(strAccount)
            If Len(strAct) <> 12 Then
                Account_UWCCB = 12
                Return Account_UWCCB
            End If

            intTotal = 0
            For iLoop = LBound(aryWeight) To UBound(aryWeight)
                intTotal = intTotal + (aryWeight(iLoop) * Val(Mid(strAct, iLoop + 1, 1)))
            Next iLoop

            intRemainder = intTotal Mod 11
            intChkCode = 11 - intRemainder
            If Right(CStr(intChkCode), 1) <> Mid(strAct, 12, 1) Then
                Account_UWCCB = -1
            Else
                Account_UWCCB = 0
            End If
            Return Account_UWCCB
        Catch ex As Exception
            Account_UWCCB = -1
            Return Account_UWCCB
        End Try
    End Function
#End Region

#Region "兆豐銀行"
    Public Function gfunCheckAccount_017(ByVal strAccount As String) As Integer
        Dim arrA(10) As Integer
        Dim arrB() As Integer = {4, 3, 2, 8, 7, 6, 5, 4, 3, 2}
        Dim A1 As Integer
        Dim C1 As Integer
        Dim i As Integer
        Dim Account_017 As Integer

        Try
            If Len(strAccount) <> 14 Then
                Account_017 = 14
                Return Account_017
            End If

            If Left(strAccount, 3) <> "000" Then
                Account_017 = -1
                Return Account_017
            End If

            strAccount = Right(Trim(strAccount), 11)

            For i = 0 To 10
                arrA(i) = CInt(Mid(strAccount, i + 1, 1))
            Next i

            For i = 0 To 9
                A1 = A1 + CInt(arrA(i) * arrB(i))
            Next i

            '檢查碼 = 合計除以11之餘數
            C1 = (A1 Mod 11)

            If Val(C1) > 9 Then
                C1 = Right(C1, 1)
            End If

            If C1 = arrA(10) Then
                Account_017 = 0
            Else
                Account_017 = -1
            End If

            Return Account_017
        Catch ex As Exception
            Account_017 = -1
            Return Account_017
        End Try
    End Function
#End Region

#Region "郵局"
    Public Function gfunCheckAccount_PO(strAccount As String) As Integer
        Dim intCheckSum As Integer
        Dim strCheckCode As String
        Dim iLoop As Integer
        Dim Account_PO As Integer

        Try
            If Len(strAccount) <> 14 Then
                Account_PO = 14
                Return Account_PO
            End If

            intCheckSum = 0
            strCheckCode = ""

            '檢查局號
            For iLoop = 1 To 6
                intCheckSum = intCheckSum + Val(Mid(strAccount, iLoop, 1)) * ((iLoop Mod 7) + 1)
            Next iLoop

            strCheckCode = Trim(Str(11 - (intCheckSum Mod 11)))

            If Val(strCheckCode) > 9 Then
                strCheckCode = Right(strCheckCode, 1)
            End If

            If Not strCheckCode = Mid(strAccount, 7, 1) Then
                Account_PO = -1
                Return Account_PO
            End If

            '檢查帳號
            intCheckSum = 0
            strCheckCode = ""
            For iLoop = 8 To 13
                intCheckSum = intCheckSum + Val(Mid(strAccount, iLoop, 1)) * ((iLoop Mod 7) + 1)
            Next iLoop

            strCheckCode = Trim(Str(11 - (intCheckSum Mod 11)))

            If Val(strCheckCode) > 9 Then
                strCheckCode = Right(strCheckCode, 1)
            End If

            If Not strCheckCode = Mid(strAccount, 14, 1) Then
                Account_PO = -1
            Else
                Account_PO = 0
            End If

            Return Account_PO
        Catch ex As Exception
            Account_PO = -1
            Return Account_PO
        End Try
    End Function
#End Region

#Region "中國信託商業銀行"
    Public Function gfunCheckAccount_CTCB(strAccount As String) As Integer
        Dim iLoop As Integer
        Dim aryWeight() As Integer = {3, 7, 1, 3, 7, 1, 3, 7, 1, 3, 7}
        Dim intCheckSum As Integer
        Dim Account_CTCB As Integer

        Try
            If Not (Len(strAccount) = 12 Or Len(strAccount) = 13) Then
                Account_CTCB = 13
                Return Account_CTCB
            End If

            intCheckSum = 0
            For iLoop = LBound(aryWeight) To UBound(aryWeight)
                intCheckSum = intCheckSum + Val(Mid(strAccount, iLoop + 1, 1)) * aryWeight(iLoop)
            Next iLoop

            intCheckSum = Right(intCheckSum, 1)
            If intCheckSum = 0 Then
                If intCheckSum = Mid(strAccount, 12, 1) Then
                    Account_CTCB = 0
                Else
                    Account_CTCB = -1
                End If
            Else
                If intCheckSum = 10 - Val(Mid(strAccount, 12, 1)) Then
                    Account_CTCB = 0
                Else
                    Account_CTCB = -1
                End If
            End If

            Return Account_CTCB
        Catch ex As Exception
            Account_CTCB = -1
            Return Account_CTCB
        End Try
    End Function
#End Region

#Region "台北富邦銀行"
    Public Function gfunCheckAccount_FB(strAccount As String) As Integer
        Dim iLoop As Integer
        Dim aryWeight() As Integer = {6, 5, 4, 3, 2, 7, 6, 5, 4, 3, 2}
        Dim intCheckSum As Integer
        Dim intCheckNo As Integer
        Dim Account_FB As Integer

        Try
            If Not Len(strAccount) = 12 Then
                Account_FB = 12
                Return Account_FB
            End If

            intCheckSum = 0
            For iLoop = LBound(aryWeight) To UBound(aryWeight)
                intCheckSum = intCheckSum + Val(Mid(strAccount, iLoop + 1, 1)) * aryWeight(iLoop)
            Next iLoop

            If intCheckSum <> 0 Then
                If intCheckSum Mod 11 = 0 Or intCheckSum Mod 11 = 1 Then
                    intCheckNo = 0
                Else
                    intCheckNo = 11 - (intCheckSum Mod 11)
                End If
                If intCheckNo = CInt(Mid(strAccount, 12, 1)) Then
                    Account_FB = 0
                Else
                    Account_FB = -1
                End If
            Else
                Account_FB = -1
            End If

            Return Account_FB
        Catch ex As Exception
            Account_FB = -1
            Return Account_FB
        End Try
    End Function
#End Region

#Region "板橋農會"
    Public Function gfunCheckAccount_928(ByVal strAccount As String) As Integer
        Dim arrA(12) As Integer
        Dim arrB() As Integer = {6, 5, 4, 4, 3, 2, 6, 1, 6, 5, 4, 3}
        Dim A1 As Integer
        Dim B1 As Integer
        Dim C1 As Integer
        Dim i As Integer
        Dim bolExpression1 As Boolean = False
        Dim Account_928 As Integer

        Try
            If Len(strAccount) <> 13 Then
                Account_928 = 13
                Return Account_928
            End If

            For i = 0 To 12
                arrA(i) = CInt(Mid(strAccount, i + 1, 1))
            Next i
            Dim bolOR As Boolean
            For i = 0 To 11
                Select Case i
                    Case 2
                        If Not arrA(i) = 0 Then
                            A1 = A1 + CInt(arrA(i) * arrB(i))
                            bolExpression1 = True
                        End If
                    Case 3
                        If bolExpression1 = False Then
                            A1 = A1 + CInt(arrA(i) * arrB(i))
                        End If
                    Case Else
                        A1 = A1 + CInt(arrA(i) * arrB(i))
                End Select
            Next i

            '餘數
            C1 = A1 Mod 11

            If C1 = 0 Then
                B1 = 1
            ElseIf C1 = 1 Then
                B1 = 0
            Else
                B1 = 11 - (A1 Mod 11)
            End If

            If B1 = arrA(12) Then
                Account_928 = 0
            Else
                Account_928 = -1
            End If

            Return Account_928
        Catch ex As Exception
            Account_928 = -1
            Return Account_928
        End Try

    End Function

#End Region

#Region "安泰商業銀行"
    Function gfunCheckAccount_816(strAccount As String) As Integer
        Dim aryWeight() As Integer = {5, 4, 3, 2, 8, 7, 6, 5, 4, 3, 2}
        Dim iLoop As Integer
        Dim intTotal As Integer
        Dim intChkCode As Integer
        Dim strAct As String = Trim(strAccount)
        Dim Account_816 As Integer

        Try
            If Len(strAct) <> 14 Then
                Account_816 = 14
                Return Account_816
            End If

            intTotal = 0
            For iLoop = LBound(aryWeight) To UBound(aryWeight)
                intTotal = intTotal + (aryWeight(iLoop) * Val(Mid(strAct, iLoop + 1, 1)))
            Next iLoop

            '合計值除以11後，取其餘數R
            '若該餘數R=0,1，則檢查碼N12＝R
            '若該餘數R<>0,1，則N12＝11-R；但若檢查碼N12=4，則檢查碼N12進位為5
            intChkCode = (intTotal Mod 11)
            If intChkCode <> 0 And intChkCode <> 1 Then
                intChkCode = 11 - intChkCode
                If intChkCode = 4 Then
                    intChkCode = 5
                End If
            End If

            If intChkCode <> Val(Mid(strAct, 12, 1)) Then
                Account_816 = -1
            Else
                Account_816 = 0
            End If

            Return Account_816
        Catch ex As Exception
            Account_816 = -1
            Return Account_816
        End Try
    End Function
#End Region

#Region "台灣中小企銀"
    Function gfunCheckAccount_050(strAccount As String) As Integer
        Dim aryWeight() As Integer = {5, 4, 3, 2, 7, 6, 5, 4, 3, 2}
        Dim iLoop As Integer
        Dim intTotal As Integer
        Dim intChkCode As Integer
        Dim strAct As String
        Dim Account_050 As Integer

        Try
            strAct = Trim(strAccount)
            If Len(strAct) <> 11 Then
                Account_050 = 11
                Return Account_050
            End If

            intTotal = 0
            For iLoop = LBound(aryWeight) To UBound(aryWeight)
                intTotal = intTotal + (aryWeight(iLoop) * Val(Mid(strAct, iLoop + 1, 1)))
            Next iLoop

            'Total除以11取餘數，若餘數>1，則A11=11-餘數，否則A11=餘數
            intChkCode = (intTotal Mod 11)
            If intChkCode > 1 Then
                intChkCode = 11 - intChkCode
            End If

            If intChkCode <> Val(Mid(strAct, 11, 1)) Then
                Account_050 = -1
            Else
                Account_050 = 0
            End If

            Return Account_050
        Catch ex As Exception
            Account_050 = -1
            Return Account_050
        End Try
    End Function
#End Region

#Region "第一銀行"
    Function gfunCheckAccount_007(strAccount As String) As Integer
        Dim aryWeight() As Integer = {4, 3, 2, 8, 7, 6, 5, 4, 3, 2}
        Dim iLoop As Integer
        Dim intTotal As Integer = 0
        Dim intChkCode As Integer
        Dim strAct As String
        Dim Account_007 As Integer

        Try
            strAct = Trim(strAccount)
            If Len(strAct) <> 11 Then
                Account_007 = 11
                Return Account_007
            End If

            For iLoop = LBound(aryWeight) To UBound(aryWeight)
                intTotal = intTotal + (aryWeight(iLoop) * Val(Mid(strAct, iLoop + 1, 1)))
            Next iLoop

            intChkCode = (11 - (intTotal Mod 11)) Mod 10
            If intChkCode = 10 Then intChkCode = 0
            If intChkCode <> Val(Mid(strAct, 11, 1)) Then
                Account_007 = -1
            Else
                Account_007 = 0
            End If

            Return Account_007
        Catch ex As Exception
            Account_007 = -1
            Return Account_007
        End Try
    End Function
#End Region

#End Region

End Class
