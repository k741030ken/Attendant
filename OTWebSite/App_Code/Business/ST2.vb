'****************************************************
'功能說明：員工基本資料設定
'建立人員：BeatriceCheng
'建立日期：2015.06.03
'****************************************************
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.IO
Imports System

Public Class ST2

#Region "ST2100 員工照片檔上傳"
#Region "ST2100 員工照片檔上傳-查詢"
    Public Function EmpPhotoQuery(ByVal CompID As String, ByVal EmpID As String) As String
        Dim FileUrl As String = Bsp.Utility.getAppSetting("EmpPhoto")

        FileUrl &= "/" & CompID & "-" & EmpID & ".jpg?date=" & DateTime.Now.Ticks

        Try
            Dim wc As New System.Net.WebClient
            Dim data As Byte() = wc.DownloadData(FileUrl)
        Catch ex As Exception
            Throw New Exception("查無照片！")
        End Try

        Return FileUrl
    End Function
#End Region
#End Region

#Region "ST2300 員工訓練資料查詢"
#Region "查詢員工訓練資料"
    Public Function TrainingQuery(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select S.IDNo ")
        strSQL.AppendLine(", P.EmpID ")
        strSQL.AppendLine(", IsNull(P.NameN, '') As Name")
        strSQL.AppendLine(", Convert(char(10), S.BeginDate, 111) As BeginDate")
        strSQL.AppendLine(", Convert(char(10), S.EndDate, 111) As EndDate")
        strSQL.AppendLine(", S.LessonUnit")
        strSQL.AppendLine(", S.LessonID")
        strSQL.AppendLine(", S.LessonName")
        strSQL.AppendLine(", S.ActivityID")
        strSQL.AppendLine(", S.Hours")
        strSQL.AppendLine(", S.Fee")
        strSQL.AppendLine(", S.KindName")
        strSQL.AppendLine(", S.DeptName")
        strSQL.AppendLine("From Training S")
        strSQL.AppendLine("Left Join Personal P On S.IDNo = P.IDNo")
        strSQL.AppendLine("Where 1 = 1")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        If ht(strKey).ToString() <> "0" Then
                            strSQL.AppendLine("And P.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        End If
                    Case "EmpIDB"
                        'If Bsp.Utility.IsStringNull(ht("EmpIDE")) <> "" Then
                        strSQL.AppendLine("And P.EmpID >= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        'Else
                        '    strSQL.AppendLine("And P.EmpID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        'End If
                    Case "EmpIDE"
                        strSQL.AppendLine("And P.EmpID <= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "BeginDateB"
                        'If Bsp.Utility.IsStringNull(ht("BeginDateE")) <> "" Then
                        strSQL.AppendLine("And Convert(char(10), S.BeginDate, 111) >= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        'Else
                        '    strSQL.AppendLine("And Convert(char(10), S.BeginDate, 111) = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        'End If
                    Case "BeginDateE"
                        strSQL.AppendLine("And Convert(char(10), S.BeginDate, 111) <= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "Name"
                        strSQL.AppendLine("And (P.Name = " & Bsp.Utility.Quote(ht(strKey).ToString()) & " Or P.NameN = N" & Bsp.Utility.Quote(ht(strKey).ToString()) & ")")
                End Select
            End If
        Next

        strSQL.AppendLine("Order By P.EmpID, S.BeginDate, S.LessonID, S.LessonName")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "查詢員工資料"
    Public Function GetEmpData(ByVal IDNo As String, ByVal CompID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select CompID, EmpID, NameN ")
        strSQL.AppendLine("From Personal")
        strSQL.AppendLine("Where IDNo = " & Bsp.Utility.Quote(IDNo) & " And CompID = " & Bsp.Utility.Quote(CompID))
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#End Region

#Region "ST2400 員工證照資料查詢"
#Region "查詢員工證照資料"
    Public Function CertificationQuery(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select S.IDNo ")
        strSQL.AppendLine(", P.EmpID ")
        strSQL.AppendLine(", IsNull(P.NameN, '') As Name")
        strSQL.AppendLine(", S.CategoryID")
        strSQL.AppendLine(", Convert(char(10), S.CertiDate, 111) As CertiDate")
        strSQL.AppendLine(", CertiTo = Case When Convert(Char(10), S.CertiTo, 111) = '1900/01/01' Then '永久' ElSE Convert(Varchar, S.CertiTo, 111) End")
        strSQL.AppendLine(", S.LicenseName")
        strSQL.AppendLine(", S.Institution")
        strSQL.AppendLine(", S.SerialNum")
        strSQL.AppendLine(", S.Remark")
        strSQL.AppendLine("From Certification S")
        strSQL.AppendLine("Left Join Personal P On S.IDNo = P.IDNo")
        strSQL.AppendLine("Left Join License L On S.CategoryID = L.CategoryID")
        strSQL.AppendLine("Where 1 = 1")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        If ht(strKey).ToString() <> "0" Then
                            strSQL.AppendLine("And P.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        End If
                    Case "EmpID"
                        strSQL.AppendLine("And P.EmpID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "Name"
                        strSQL.AppendLine("And (P.Name = " & Bsp.Utility.Quote(ht(strKey).ToString()) & " Or P.NameN = N" & Bsp.Utility.Quote(ht(strKey).ToString()) & ")")
                    Case "DeptID"
                        strSQL.AppendLine("And P.DeptID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "OrganID"
                        strSQL.AppendLine("And P.OrganID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "CategoryID"
                        strSQL.AppendLine("And S.CategoryID  = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next

        strSQL.AppendLine("Order By P.EmpID, S.CategoryID, S.CertiDate")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region
#End Region

End Class
