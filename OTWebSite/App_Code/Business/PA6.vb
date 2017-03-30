'****************************************************
'功能說明：薪資參數設定
'建立人員：BeatriceCheng
'建立日期：2016.05.11
'****************************************************
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.Common

Public Class PA6

#Region "PA6100 薪資所得扣繳稅額繳款書參數維護"
#Region "查詢"
    Public Function TaxParameterOrganQuery(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select S.CompID")
        strSQL.AppendLine(", S.InvoiceNo")
        strSQL.AppendLine(", InvoiceOrganID")
        strSQL.AppendLine(", InvoiceName")
        strSQL.AppendLine(", HeadOfficeFlag")
        strSQL.AppendLine(", ISNULL(W.Address, '') Address")
        strSQL.AppendLine(", ISNULL(PS.NameN, '') Obligor")
        strSQL.AppendLine(", ISNULL(H.CodeCName, '') TaxCityCode")
        strSQL.AppendLine(", ISNULL(T.TaxUnitNo, '') + '-' + ISNULL(T.TaxUnitName, '') TaxUnitNo")
        strSQL.AppendLine(", ISNULL(C.CompName, '') LastChgComp")
        strSQL.AppendLine(", ISNULL(P.NameN, '') LastChgID")
        strSQL.AppendLine(", LastChgDate = Case When Convert(Char(10), S.LastChgDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, S.LastChgDate, 120) End")
        strSQL.AppendLine("From TaxParameterOrgan S")
        strSQL.AppendLine("Left Join Company C on C.CompID = S.LastChgComp")
        strSQL.AppendLine("Left Join HRCodeMap H on H.TabName = 'TaxParameterOrgan' And H.FldName = 'TaxCityCode' and H.Code = S.TaxCityCode")
        strSQL.AppendLine("Left Join Organization O on O.CompID = S.CompID And O.OrganID = S.InvoiceOrganID")
        strSQL.AppendLine("Left Join Personal P on P.CompID = S.LastChgComp And P.EmpID = S.LastChgID")
        strSQL.AppendLine("Left Join Personal PS on PS.CompID = O.BossCompID And PS.EmpID = O.Boss")
        strSQL.AppendLine("Left Join TaxUnit T on T.TaxCityCode = S.TaxCityCode And T.TaxUnitNo = S.TaxUnitNo")
        strSQL.AppendLine("Left Join WorkSite W on W.CompID = O.CompID And W.WorkSiteID = O.WorkSiteID")
        strSQL.AppendLine("Where 1 = 1")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine("And S.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "InvoiceNo"
                        strSQL.AppendLine("And S.InvoiceNo = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "新增"
    Public Function TaxParameterOrganAdd(ByVal beTaxParameterOrgan As beTaxParameterOrgan.Row) As Boolean
        Dim bsTaxParameterOrgan As New beTaxParameterOrgan.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsTaxParameterOrgan.Insert(beTaxParameterOrgan, tran) = 0 Then Return False
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

#Region "修改"
    Public Function TaxParameterOrganUpdate(ByVal beTaxParameterOrgan As beTaxParameterOrgan.Row) As Boolean
        Dim bsTaxParameterOrgan As New beTaxParameterOrgan.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                strSQL.AppendLine("UPDATE TaxParameterOrgan")
                strSQL.AppendLine("SET InvoiceNo = @InvoiceNo")
                strSQL.AppendLine(", InvoiceOrganID = @InvoiceOrganID")
                strSQL.AppendLine(", InvoiceName = @InvoiceName")
                strSQL.AppendLine(", HeadOfficeFlag = @HeadOfficeFlag")
                strSQL.AppendLine(", TaxCityCode = @TaxCityCode")
                strSQL.AppendLine(", TaxUnitNo = @TaxUnitNo")
                strSQL.AppendLine(", LastChgComp = @LastChgComp")
                strSQL.AppendLine(", LastChgID = @LastChgID")
                strSQL.AppendLine(", LastChgDate = @LastChgDate")
                strSQL.AppendLine("WHERE CompID = @KeyCompID")
                strSQL.AppendLine("AND InvoiceNo = @KeyInvoiceNo")

                Dim DbParam() As DbParameter = { _
                    Bsp.DB.getDbParameter("@InvoiceNo", beTaxParameterOrgan.InvoiceNo.Value), _
                    Bsp.DB.getDbParameter("@InvoiceOrganID", beTaxParameterOrgan.InvoiceOrganID.Value), _
                    Bsp.DB.getDbParameter("@InvoiceName", beTaxParameterOrgan.InvoiceName.Value), _
                    Bsp.DB.getDbParameter("@HeadOfficeFlag", beTaxParameterOrgan.HeadOfficeFlag.Value), _
                    Bsp.DB.getDbParameter("@TaxCityCode", beTaxParameterOrgan.TaxCityCode.Value), _
                    Bsp.DB.getDbParameter("@TaxUnitNo", beTaxParameterOrgan.TaxUnitNo.Value), _
                    Bsp.DB.getDbParameter("@LastChgComp", beTaxParameterOrgan.LastChgComp.Value), _
                    Bsp.DB.getDbParameter("@LastChgID", beTaxParameterOrgan.LastChgID.Value), _
                    Bsp.DB.getDbParameter("@LastChgDate", Now), _
                    Bsp.DB.getDbParameter("@KeyCompID", beTaxParameterOrgan.CompID.Value), _
                    Bsp.DB.getDbParameter("@KeyInvoiceNo", beTaxParameterOrgan.InvoiceNo.OldValue)}

                If Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), DbParam, tran, "eHRMSDB") = 0 Then Return False

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

#Region "刪除"
    Public Function TaxParameterOrganDelete(ByVal beTaxParameterOrgan As beTaxParameterOrgan.Row) As Boolean
        Dim bsTaxParameterOrgan As New beTaxParameterOrgan.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsTaxParameterOrgan.DeleteRowByPrimaryKey(beTaxParameterOrgan, tran) = 0 Then Return False
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

#Region "查詢部門地址和扣繳義務人"
    Public Function ChangeOrgan(ByVal CompID As String, ByVal OrganID As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine("Select ISNULL(W.Address, '') Address, ISNULL(P.NameN, '') Obligor")
        strSQL.AppendLine("From Organization O")
        strSQL.AppendLine("Left Join Personal P on P.CompID = O.BossCompID And P.EmpID = O.Boss")
        strSQL.AppendLine("Left Join WorkSite W on W.CompID = O.CompID And W.WorkSiteID = O.WorkSiteID")
        strSQL.AppendLine("Where O.CompID = " & Bsp.Utility.Quote(CompID.ToString()) & " AND O.OrganID = " & Bsp.Utility.Quote(OrganID.ToString()))
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region
#End Region

End Class
