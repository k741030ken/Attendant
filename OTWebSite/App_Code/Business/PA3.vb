'****************************************************
'功能說明：管理分析-人員參數設定
'建立人員：BeatriceCheng
'建立日期：2015/05/03
'****************************************************
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.Common

Public Class PA3

#Region "PA3100 學歷代碼設定"
#Region "查詢學歷代碼檔"
    Public Function EduDegreeQuery(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select S.EduID, EduName, EduNameCN, C.CompName As LastChgComp, P.NameN As LastChgID, ")
        strSQL.AppendLine("LastChgDate = Case When Convert(Char(10), S.LastChgDate, 111) = '1900/01/01' Then '' ")
        strSQL.AppendLine("ElSE Convert(Varchar, S.LastChgDate, 120) End")
        strSQL.AppendLine("From EduDegree S")
        strSQL.AppendLine("Left Join Company C on C.CompID = S.LastChgComp")
        strSQL.AppendLine("Left Join Personal P on P.CompID = S.LastChgComp And P.EmpID = S.LastChgID")
        strSQL.AppendLine("Where 1 = 1")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "EduID"
                        strSQL.AppendLine("And S.EduID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "新增學歷代碼檔"
    Public Function EduDegreeAdd(ByVal beEduDegree As beEduDegree.Row) As Boolean
        Dim bsEduDegree As New beEduDegree.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsEduDegree.Insert(beEduDegree, tran) = 0 Then Return False
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

#Region "修改學歷代碼檔"
    Public Function EduDegreeUpdate(ByVal beEduDegree As beEduDegree.Row) As Boolean
        Dim bsEduDegree As New beEduDegree.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsEduDegree.Update(beEduDegree, tran) = 0 Then Return False
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

#Region "刪除學歷代碼檔"
    Public Function EduDegreeDelete(ByVal beEduDegree As beEduDegree.Row) As Boolean
        Dim bsEduDegree As New beEduDegree.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsEduDegree.DeleteRowByPrimaryKey(beEduDegree, tran) = 0 Then Return False
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

#Region "PA3200 任職狀況設定"
#Region "查詢任職狀況檔"
    Public Function WorkStatusQuery(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select WorkCode, Remark, RemarkCN, C.CompName As LastChgComp, P.NameN As LastChgID, ")
        strSQL.AppendLine("LastChgDate = Case When Convert(Char(10), S.LastChgDate, 111) = '1900/01/01' Then '' ")
        strSQL.AppendLine("ElSE Convert(Varchar, S.LastChgDate, 120) End")
        strSQL.AppendLine("From WorkStatus S")
        strSQL.AppendLine("Left Join Company C on C.CompID = S.LastChgComp")
        strSQL.AppendLine("Left Join Personal P on P.CompID = S.LastChgComp And P.EmpID = S.LastChgID")
        strSQL.AppendLine("Where 1 = 1")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "WorkCode"
                        strSQL.AppendLine("And WorkCode = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "新增任職狀況檔"
    Public Function WorkStatusAdd(ByVal beWorkStatus As beWorkStatus.Row) As Boolean
        Dim bsWorkStatus As New beWorkStatus.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsWorkStatus.Insert(beWorkStatus, tran) = 0 Then Return False
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

#Region "修改任職狀況檔"
    Public Function WorkStatusUpdate(ByVal beWorkStatus As beWorkStatus.Row) As Boolean
        Dim bsWorkStatus As New beWorkStatus.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsWorkStatus.Update(beWorkStatus, tran) = 0 Then Return False
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

#Region "刪除任職狀況檔"
    Public Function WorkStatusDelete(ByVal beWorkStatus As beWorkStatus.Row) As Boolean
        Dim bsWorkStatus As New beWorkStatus.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsWorkStatus.DeleteRowByPrimaryKey(beWorkStatus, tran) = 0 Then Return False
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

#Region "PA3300 家屬稱謂設定"
#Region "查詢家屬稱謂檔"
    Public Function RelationshipQuery(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select RelativeID, Remark, RemarkCN, HeaRelID, CompRelID, DeathPayID")
        strSQL.AppendLine(", TaxFamilyID = Case TaxFamilyID When '0' Then '配偶' When '1' Then '尊親' When '2' Then '子女' When '3' Then '兄弟姐妹' When '4' Then '親戚' Else '' End")
        strSQL.AppendLine(", RelTypeID = Case RelTypeID When 'B' Then '血親' When 'M' Then '姻親' Else '' End")
        strSQL.AppendLine(", RelClassID")
        strSQL.AppendLine(", C.CompName As LastChgComp, P.NameN As LastChgID, ")
        strSQL.AppendLine("LastChgDate = Case When Convert(Char(10), S.LastChgDate, 111) = '1900/01/01' Then '' ")
        strSQL.AppendLine("ElSE Convert(Varchar, S.LastChgDate, 120) End")
        strSQL.AppendLine("From Relationship S")
        strSQL.AppendLine("Left Join Company C on C.CompID = S.LastChgComp")
        strSQL.AppendLine("Left Join Personal P on P.CompID = S.LastChgComp And P.EmpID = S.LastChgID")
        strSQL.AppendLine("Where 1 = 1")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "RelativeID"
                        strSQL.AppendLine("And RelativeID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "Remark"
                        strSQL.AppendLine("And Remark like N'%" & ht(strKey).ToString() & "%'")
                End Select
            End If
        Next

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "新增家屬稱謂檔"
    Public Function RelationshipAdd(ByVal beRelationship As beRelationship.Row) As Boolean
        Dim bsRelationship As New beRelationship.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsRelationship.Insert(beRelationship, tran) = 0 Then Return False
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

#Region "修改家屬稱謂檔"
    Public Function RelationshipUpdate(ByVal beRelationship As beRelationship.Row) As Boolean
        Dim bsRelationship As New beRelationship.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsRelationship.Update(beRelationship, tran) = 0 Then Return False
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

#Region "刪除家屬稱謂檔"
    Public Function RelationshipDelete(ByVal beRelationship As beRelationship.Row) As Boolean
        Dim bsRelationship As New beRelationship.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsRelationship.DeleteRowByPrimaryKey(beRelationship, tran) = 0 Then Return False
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

#Region "PA3400 學校設定"
#Region "查詢學校檔"
    Public Function SchoolQuery(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select SchoolID, Remark, PrimaryFlag, C.CompName As LastChgComp, P.NameN As LastChgID, ")
        strSQL.AppendLine("LastChgDate = Case When Convert(Char(10), S.LastChgDate, 111) = '1900/01/01' Then '' ")
        strSQL.AppendLine("ElSE Convert(Varchar, S.LastChgDate, 120) End")
        strSQL.AppendLine("From School S")
        strSQL.AppendLine("Left Join Company C on C.CompID = S.LastChgComp")
        strSQL.AppendLine("Left Join Personal P on P.CompID = S.LastChgComp And P.EmpID = S.LastChgID")
        strSQL.AppendLine("Where 1 = 1")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "SchoolID"
                        strSQL.AppendLine("And SchoolID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "Remark"
                        strSQL.AppendLine("And Upper(Remark) like Upper(N'%" & ht(strKey).ToString() & "%')")
                    Case "PrimaryFlag"
                        strSQL.AppendLine("And PrimaryFlag = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "新增學校檔"
    Public Function SchoolAdd(ByVal beSchool As beSchool.Row) As Boolean
        Dim bsSchool As New beSchool.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsSchool.Insert(beSchool, tran) = 0 Then Return False
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

#Region "修改學校檔"
    Public Function SchoolUpdate(ByVal beSchool As beSchool.Row) As Boolean
        Dim bsSchool As New beSchool.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsSchool.Update(beSchool, tran) = 0 Then Return False
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

#Region "刪除學校檔"
    Public Function SchoolDelete(ByVal beSchool As beSchool.Row) As Boolean
        Dim bsSchool As New beSchool.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsSchool.DeleteRowByPrimaryKey(beSchool, tran) = 0 Then Return False
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

#Region "PA3500 科系設定"
#Region "查詢科系檔"
    Public Function DepartQuery(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select DepartID, Remark, C.CompName As LastChgComp, P.NameN As LastChgID, ")
        strSQL.AppendLine("LastChgDate = Case When Convert(Char(10), S.LastChgDate, 111) = '1900/01/01' Then '' ")
        strSQL.AppendLine("ElSE Convert(Varchar, S.LastChgDate, 120) End")
        strSQL.AppendLine("From Depart S")
        strSQL.AppendLine("Left Join Company C on C.CompID = S.LastChgComp")
        strSQL.AppendLine("Left Join Personal P on P.CompID = S.LastChgComp And P.EmpID = S.LastChgID")
        strSQL.AppendLine("Where 1 = 1")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "DepartID"
                        strSQL.AppendLine("And DepartID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "Remark"
                        strSQL.AppendLine("And Remark like N'%" & ht(strKey).ToString() & "%'")
                End Select
            End If
        Next

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "新增科系檔"
    Public Function DepartAdd(ByVal beDepart As beDepart.Row) As Boolean
        Dim bsDepart As New beDepart.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsDepart.Insert(beDepart, tran) = 0 Then Return False
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

#Region "修改科系檔"
    Public Function DepartUpdate(ByVal beDepart As beDepart.Row) As Boolean
        Dim bsDepart As New beDepart.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsDepart.Update(beDepart, tran) = 0 Then Return False
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

#Region "刪除科系檔"
    Public Function DepartDelete(ByVal beDepart As beDepart.Row) As Boolean
        Dim bsDepart As New beDepart.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsDepart.DeleteRowByPrimaryKey(beDepart, tran) = 0 Then Return False
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

#Region "PA3500 簽證國家設定"
#Region "查詢簽證國家檔"
    Public Function VisaCountryQuery(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select Country, CountryName, OfficeName, S.Address, C.CompName As LastChgComp, P.NameN As LastChgID, ")
        strSQL.AppendLine("LastChgDate = Case When Convert(Char(10), S.LastChgDate, 111) = '1900/01/01' Then '' ")
        strSQL.AppendLine("ElSE Convert(Varchar, S.LastChgDate, 120) End")
        strSQL.AppendLine("From VisaCountry S")
        strSQL.AppendLine("Left Join Company C on C.CompID = S.LastChgComp")
        strSQL.AppendLine("Left Join Personal P on P.CompID = S.LastChgComp And P.EmpID = S.LastChgID")
        strSQL.AppendLine("Where 1 = 1")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "Country"
                        strSQL.AppendLine("And Country = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "新增簽證國家檔"
    Public Function VisaCountryAdd(ByVal beVisaCountry As beVisaCountry.Row) As Boolean
        Dim bsVisaCountry As New beVisaCountry.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsVisaCountry.Insert(beVisaCountry, tran) = 0 Then Return False
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

#Region "修改簽證國家檔"
    Public Function VisaCountryUpdate(ByVal beVisaCountry As beVisaCountry.Row) As Boolean
        Dim bsVisaCountry As New beVisaCountry.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsVisaCountry.Update(beVisaCountry, tran) = 0 Then Return False
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

#Region "刪除簽證國家檔"
    Public Function VisaCountryDelete(ByVal beVisaCountry As beVisaCountry.Row) As Boolean
        Dim bsVisaCountry As New beVisaCountry.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsVisaCountry.DeleteRowByPrimaryKey(beVisaCountry, tran) = 0 Then Return False
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

    '2015/07/30 Add by Micky
#Region "PA3700 員工任職狀態與異動原因對應表"
#Region "查詢員工任職狀態與異動原因對應表"
    Public Function WorkStatus_EmployeeReasonQuery(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" select ")
        strSQL.AppendLine(" WE.BeforeWorkStatusType, case when WE.BeforeWorkStatusType = '1' then '在職' when WE.BeforeWorkStatusType = '0' then '非在職' End As BeforeWorkStatusTypeName ")
        strSQL.AppendLine(" , WE.BeforeWorkStatus, W.Remark AS BeforeWorkStatusName, WE.Reason ,E.Remark AS ReasonName ")
        strSQL.AppendLine(" , WE.AfterWorkStatusType, W1.Remark AS AfterWorkStatusTypeName ")
        strSQL.AppendLine(" , CL.CompName AS LastChgComp, PL.NameN AS LastChgID ")
        strSQL.AppendLine(" , LastChgDate = Case When Convert(Char(10), WE.LastChgDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, WE.LastChgDate, 120) END ")
        strSQL.AppendLine(" FROM WorkStatus_EmployeeReason WE ")
        strSQL.AppendLine(" left join WorkStatus W on WE.BeforeWorkStatus = W.WorkCode ")
        strSQL.AppendLine(" left join WorkStatus W1 on WE.AfterWorkStatusType = W1.WorkCode  ")
        strSQL.AppendLine(" left join EmployeeReason E on WE.Reason = E.Reason ")
        strSQL.AppendLine(" LEFT JOIN Company CL ON WE.LastChgComp = CL.CompID ")
        strSQL.AppendLine(" LEFT JOIN Personal PL ON WE.LastChgID = PL.EmpID AND WE.LastChgComp = PL.CompID ")
        strSQL.AppendLine(" Where 1 = 1 ")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "BeforeWorkStatusType"
                        strSQL.AppendLine(" And WE.BeforeWorkStatusType = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "BeforeWorkStatus"
                        strSQL.AppendLine(" And WE.BeforeWorkStatus = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "Reason"
                        strSQL.AppendLine(" And WE.Reason = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "AfterWorkStatusType"
                        strSQL.AppendLine(" And WE.AfterWorkStatusType = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next

        strSQL.AppendLine(" order by 3 ")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#End Region

#Region "下拉選單"
    Enum DisplayType
        OnlyName    '只顯示名字
        OnlyID      '顯示ID  
        Full        '顯示ID + 名字
    End Enum

    Public Shared Sub FillDDL(ByVal objDDL As DropDownList, ByVal strTabName As String, ByVal strValue As String, ByVal strText As String, Optional ByVal Type As DisplayType = DisplayType.Full, Optional ByVal CondStr As String = "")
        Dim objPA3 As New PA3()
        Try
            Using dt As DataTable = objPA3.GetDDLInfo(strTabName, strValue, strText, CondStr)
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

    Public Function GetDDLInfo(ByVal strTabName As String, ByVal strValue As String, ByVal strText As String, Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder
        strSQL.AppendLine("Select " & strValue & " AS Code, " & strText & " AS CodeName, " & strValue & " + '-' + " & strText & " AS FullName " & " FROM " & strTabName)
        strSQL.AppendLine("Where 1=1")
        If CondStr <> "" Then strSQL.AppendLine(CondStr)
        strSQL.AppendLine("Order By " & strValue & ", " & strText)
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "eHRMSDB").Tables(0)
    End Function
#End Region

End Class
