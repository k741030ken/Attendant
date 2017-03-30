'****************************************************
'功能說明：
'建立人員：Micky Sung
'建立日期：2015.08.13
'****************************************************
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.IO
Imports System

Public Class OT1

#Region "OT1140 員工持股信託特例人員處理"

#Region "OT1140 員工持股信託特例人員處理-查詢"
    Public Function QueryEmpStockTrustExLevelSetting(ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT ")
        strSQL.AppendLine(" E.CompID, E.EmpID, P.NameN, E.UAmount ")
        strSQL.AppendLine(" , CL.CompName AS LastChgComp, PL.NameN AS LastChgID ")
        strSQL.AppendLine(" , LastChgDate = Case When Convert(Char(10), E.LastChgDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, E.LastChgDate, 120) END ")
        strSQL.AppendLine(" FROM EmpStockTrustExLevel E ")
        strSQL.AppendLine(" LEFT JOIN Personal P ON E.EmpID = P.EmpID AND E.CompID = P.CompID ")
        strSQL.AppendLine(" LEFT JOIN Company CL ON E.LastChgComp = CL.CompID ")
        strSQL.AppendLine(" LEFT JOIN Personal PL ON E.LastChgID = PL.EmpID AND E.LastChgComp = PL.CompID ")
        strSQL.AppendLine(" WHERE 1 = 1 ")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine(" AND E.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "EmpID"
                        strSQL.AppendLine(" AND E.EmpID LIKE '" & ht(strKey).ToString() + "%' ")
                    Case "Name"
                        strSQL.AppendLine(" AND P.NameN LIKE N'" & ht(strKey).ToString() + "%' ")
                End Select
            End If
        Next

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "OT1141 員工持股信託特例人員處理-新增"
    Public Function AddEmpStockTrustExLevelSetting(ByVal beEmpStockTrustExLevel As beEmpStockTrustExLevel.Row) As Boolean
        Dim bsEmpStockTrustExLevel As New beEmpStockTrustExLevel.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsEmpStockTrustExLevel.Insert(beEmpStockTrustExLevel, tran) = 0 Then Return False
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

#Region "OT1142 員工持股信託特例人員處理-修改"
    Public Function UpdateEmpStockTrustExLevelSetting(ByVal beEmpStockTrustExLevel As beEmpStockTrustExLevel.Row) As Boolean
        Dim bsEmpStockTrustExLevel As New beEmpStockTrustExLevel.Service()
        Dim strSQL_Personal As New StringBuilder()
        Dim strFamily As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                If bsEmpStockTrustExLevel.Update(beEmpStockTrustExLevel, tran) = 0 Then Return False
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

#Region "OT1140 員工持股信託特例人員處理-刪除"
    Public Function DeleteEmpStockTrustExLevelSetting(ByVal beEmpStockTrustExLevel As beEmpStockTrustExLevel.Row) As Boolean
        Dim bsEmpStockTrustExLevel As New beEmpStockTrustExLevel.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As Data.Common.DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                bsEmpStockTrustExLevel.DeleteRowByPrimaryKey(beEmpStockTrustExLevel, tran)

                tran.Commit()
                inTrans = False
            Catch ex As Exception
                If inTrans Then tran.Rollback()
                Throw
            Finally
                tran.Dispose()
                If cn.State = ConnectionState.Open Then cn.Close()
            End Try
        End Using

        Return True
    End Function
#End Region

#Region "OT1140 自行訂義條件Table之資料數"
    Public Function IsDataExists(ByVal strTable As String, ByVal strWhere As String) As Boolean
        Dim strSQL As String
        strSQL = "Select Count(*) Cnt From " & strTable
        strSQL += " Where 1 = 1 " & strWhere
        Return IIf(Bsp.DB.ExecuteScalar(strSQL, "eHRMSDB") > 0, True, False)
    End Function
#End Region

#End Region

End Class
