Imports System.Data
Imports System.Data.Common

Partial Class OV_OV1400

    Inherits PageBase
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then

        End If
    End Sub
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
    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnQuery"     '查詢
                If (checkdate()) Then
                    DoQuery()
                End If
            Case "btnActionX"   '清除
                DoClear()
        End Select
    End Sub
    Private Sub DoQuery()
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" SELECT T.OTCompID+' '+C.CompName AS CompID_Name,T.OTEmpID+' '+P.NameN AS EmpID_Name,T.OTStartDate+'~'+T.OTEndDate AS OTDate,LEFT(T.OTStartTime,2)+':'+RIGHT(T.OTStartTime,2)+'~'+LEFT(T.OTEndTime,2)+':'+RIGHT(T.OTEndTime,2) AS OTTime, ")
        strSQL.AppendLine(" CASE WHEN T.ToOverTimeStatus = '00' THEN '成功' WHEN T.ToOverTimeStatus = '01' THEN '失敗' END AS ToOverTimeStatus, ")
        strSQL.AppendLine(" T.Message,T.LastChgComp+' '+CL.CompName AS LastChgComp,T.LastChgID+' '+PL.NameN AS LastChgID,T.LastChgDate, ")
        strSQL.AppendLine(" P.NameN,PL.NameN,C.CompName,CL.CompName ")
        strSQL.AppendLine("  FROM ToOverTimeLog T ")
        strSQL.AppendLine("  LEFT JOIN " & eHRMSDB_ITRD & ".dbo.Personal P ON T.OTCompID = P.CompID AND T.OTEmpID = P.EmpID ")
        strSQL.AppendLine("  LEFT JOIN " & eHRMSDB_ITRD & ".dbo.Personal PL ON T.LastChgComp = PL.CompID AND T.LastChgID = PL.EmpID ")
        strSQL.AppendLine("  LEFT JOIN " & eHRMSDB_ITRD & ".dbo.Company C ON T.OTCompID = C.CompID ")
        strSQL.AppendLine("  LEFT JOIN " & eHRMSDB_ITRD & ".dbo.Company CL ON T.LastChgComp = CL.CompID ")
        strSQL.AppendLine("  WHERE 0 = 0 ")
        If runTimeBeginDate.DateText <> "" And runTimeEndDate.DateText <> "" Then
            strSQL.AppendLine(" AND CONVERT(NVARCHAR (10),T.RunDT,111) >= " & Bsp.Utility.Quote(runTimeBeginDate.DateText))
            strSQL.AppendLine(" AND CONVERT(NVARCHAR (10),T.RunDT,111) <= " & Bsp.Utility.Quote(runTimeEndDate.DateText))
        End If
        If toOverTimeDate.DateText <> "" Then
            strSQL.AppendLine(" AND T.ToOverTimeDate = " & Bsp.Utility.Quote(toOverTimeDate.DateText))
        End If
        If ddlStatus.SelectedValue <> "" Then
            strSQL.AppendLine(" AND T.ToOverTimeStatus = " & Bsp.Utility.Quote(ddlStatus.SelectedValue))
        End If

        Dim dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "AattendantDB").Tables(0)
        ShowTable.Visible = True
        pcMain.DataTable = dt
        gvMain.DataBind()
    End Sub

    Private Sub DoClear()
        runTimeBeginDate.DateText = ""
        runTimeEndDate.DateText = ""
        toOverTimeDate.DateText = ""
        ddlStatus.SelectedIndex = 0
        ShowTable.Visible = False
    End Sub

    Public Function checkdate() As Boolean
        If (DateStrF(runTimeBeginDate.DateText) = "" Or DateStrF(runTimeEndDate.DateText) = "") Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00070", "拋轉執行日期-起迄日")
            runTimeBeginDate.Focus()
            Return False
        End If
        If (DateStrF(runTimeBeginDate.DateText) <> "" And DateStrF(runTimeEndDate.DateText) <> "") Then
            If Not IsDate(runTimeBeginDate.DateText) Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00070", "拋轉執行日期-起日")
                runTimeBeginDate.Focus()
                Return False
            ElseIf Not IsDate(runTimeEndDate.DateText) Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00070", "拋轉執行日期-迄日")
                runTimeEndDate.Focus()
                Return False
            ElseIf CDate(runTimeEndDate.DateText) < CDate(runTimeBeginDate.DateText) Then
                Bsp.Utility.ShowFormatMessage(Me, "W_20720", "拋轉執行日期-起日", "拋轉執行日期-迄日")
                runTimeBeginDate.Focus()
                Return False
            End If
        ElseIf (DateStrF(runTimeBeginDate.DateText) <> "" And DateStrF(runTimeEndDate.DateText) = "") Then
            If Not IsDate(runTimeBeginDate.DateText) Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00070", "拋轉執行日期-起日")
                runTimeBeginDate.Focus()
                Return False
            End If
        ElseIf (DateStrF(runTimeBeginDate.DateText) = "" And DateStrF(runTimeEndDate.DateText) <> "") Then
            If Not IsDate(runTimeEndDate.DateText) Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00070", "拋轉執行日期-迄日")
                runTimeEndDate.Focus()
                Return False
            End If
        End If

        Return True
    End Function

    Private Function DateStrF(ByVal dateStr As String) As String
        Dim result = ""
        If Not dateStr Is Nothing Then
            If dateStr.Replace("/", "").Replace("_", "").Trim = "" Then
                result = ""
            Else
                result = dateStr.ToString
            End If
        End If
        Return result
    End Function

End Class
