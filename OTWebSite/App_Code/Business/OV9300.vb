'****************************************************
'功能說明：流程種類參數設定
'建立人員：John
'建立日期：2017/05/03
'****************************************************
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.Common

Public Class OV9300

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

#Region "OV9300 流程代碼下拉選單"
    Public Shared Sub FillFlowCode(ByVal objDDL As DropDownList, ByVal SystemID As String, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objOV9300 As New OV9300

        Try
            Using dt As DataTable = objOV9300.GetFlowCode(SystemID)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "CodeCName"
                    .DataValueField = "FlowCode"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetFlowCode(ByVal SystemID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT DISTINCT HR.FlowCode,HR.FlowCode + '-'+ ISNULL(AT.CodeCName,'') AS CodeCName FROM HRFlowTypeDefine HR ")
        strSQL.AppendLine(" LEFT JOIN AT_CodeMap AT ON HR.FlowCode = AT.Code AND AT.TabName = 'HRFlowEngine' AND AT.FldName = 'FlowCode' ")
        strSQL.AppendLine(" WHERE 1 = 1 ")

        If SystemID <> "" Then
            strSQL.AppendLine(" AND HR.SystemID = " & Bsp.Utility.Quote(SystemID))
        End If


        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "AattendantDB").Tables(0)
    End Function
#End Region

#Region "OV9300 流程種類下拉選單"
    Public Shared Sub FillFlowType(ByVal objDDL As DropDownList, ByVal SystemID As String, ByVal FlowCode As String, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objOV9300 As New OV9300

        Try
            Using dt As DataTable = objOV9300.GetFlowType(SystemID, FlowCode)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "FlowTypeName"
                    .DataValueField = "FlowType"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetFlowType(ByVal SystemID As String, ByVal FlowCode As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT FlowType,FlowType + '-' + FlowTypeName AS FlowTypeName FROM HRFlowTypeDefine WHERE 1 = 1 ")

        If SystemID <> "" Then
            strSQL.AppendLine(" AND SystemID = " & Bsp.Utility.Quote(SystemID))
        End If

        If FlowCode <> "" Then
            strSQL.AppendLine(" AND FlowCode = " & Bsp.Utility.Quote(FlowCode))
        End If


        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "AattendantDB").Tables(0)
    End Function
#End Region
#Region "OV9302 流程識別碼下拉選單"
    Public Shared Sub FillFlowSN(ByVal objDDL As DropDownList, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objOV9300 As New OV9300

        Try
            Using dt As DataTable = objOV9300.GetFlowSN()
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "FlowName"
                    .DataValueField = "FlowSN"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetFlowSN() As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT DISTINCT ED.FlowSN,ED.FlowSN + '-' + Eng.FlowName AS FlowName ")
        strSQL.AppendLine(" FROM HRFlowEmpDefine ED ")
        strSQL.AppendLine(" LEFT JOIN HRFlowEngine Eng ON ED.SystemID = Eng.SystemID AND ED.FlowCode = Eng.FlowCode AND ED.FlowSN = Eng.FlowSN ")


        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "AattendantDB").Tables(0)
    End Function
#End Region

#Region "OV9300 流程種類參數設定-查詢"
    Public Function HRFlowTypeDefineQuery(ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT HRFTD.SystemID, ")
        strSQL.AppendLine(" CASE WHEN AT.Code IS NULL THEN HRFTD.SystemID ELSE HRFTD.SystemID + '-' + AT.CodeCName END AS SystemID_Name, ")
        strSQL.AppendLine(" HRFTD.FlowCode,CASE WHEN ATC.Code IS NULL THEN HRFTD.FlowCode ELSE HRFTD.FlowCode + '-' + ATC.CodeCName END AS FlowCode_Name, ")
        strSQL.AppendLine(" HRFTD.FlowType,HRFTD.FlowType + '-' + HRFTD.FlowTypeName AS FlowTypeName,HRFTD.FlowTypeDescription,HRFTD.FlowTypeFlag,HRFTD.FlowSN, ")
        strSQL.AppendLine(" HRFTD.LastChgComp + '-' + Comp.CompName AS LastChgComp, ")
        strSQL.AppendLine(" HRFTD.LastChgID + '-' + Pers.NameN AS LastChgID, ")
        strSQL.AppendLine(" CASE WHEN CONVERT(Char(10), HRFTD.LastChgDate, 111) = '1900/01/01' THEN '1900/01/01' ElSE CONVERT(Varchar, HRFTD.LastChgDate, 120) END AS LastChgDate ")
        strSQL.AppendLine(" FROM HRFlowTypeDefine HRFTD ")
        strSQL.AppendLine(" LEFT JOIN AT_CodeMap AT ON HRFTD.SystemID = AT.Code AND AT.TabName = 'HRFlowEngine' AND AT.FldName = 'SystemID' ")
        strSQL.AppendLine(" LEFT JOIN AT_CodeMap ATC ON HRFTD.FlowCode = ATC.Code AND ATC.TabName = 'HRFlowEngine' AND ATC.FldName = 'FlowCode' ")
        strSQL.AppendLine(" LEFT JOIN " + eHRMSDB_ITRD + ".[dbo].Company Comp ON HRFTD.LastChgComp = Comp.CompID ")
        strSQL.AppendLine(" LEFT JOIN " + eHRMSDB_ITRD + ".[dbo].Personal Pers ON HRFTD.LastChgComp = Pers.CompID AND HRFTD.LastChgID = Pers.EmpID ")
        strSQL.AppendLine(" WHERE 1 = 1  ")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "SystemID"
                        strSQL.AppendLine(" AND HRFTD.SystemID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "FlowCode"
                        strSQL.AppendLine(" AND HRFTD.FlowCode = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "FlowType"
                        strSQL.AppendLine(" AND HRFTD.FlowType = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "AattendantDB").Tables(0)
    End Function
#End Region

#Region "OV9300 流程種類參數設定-修改"
    Public Function UpdateHRFlowTypeDefine(ByVal beHRFlowTypeDefine As beHRFlowTypeDefine.Row) As Boolean
        Dim bsHRFlowTypeDefine As New beHRFlowTypeDefine.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("AattendantDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsHRFlowTypeDefine.Update(beHRFlowTypeDefine, tran) = 0 Then Return False
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
End Class
