Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Reflection
Imports Dapper
Imports System.Data

Public Class POCommon'POxxx

    Private Shared _attendantDB As String = "AattendantDB"
    Private Shared _eHRMSDB_ITRD As String = "eHRMSDB"

    ''' <summary>
    ''' SelectOrgan
    ''' 取得行政組織下拉選單
    ''' </summary>
    ''' <param name="model">畫面model</param>
    ''' <param name="datas">回傳資料</param>
    ''' <param name="msg">回傳訊息</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Shared Function SelectOrgan(ByVal model As PO4000Model, ByRef datas As List(Of OrganListModel), ByRef msg As String) As Boolean
        Dim result As Boolean = False
        msg = ""
        datas = New List(Of OrganListModel)()
        Try
            Using conn = New SqlConnection() With {.ConnectionString = ConfigurationManager.ConnectionStrings(_eHRMSDB_ITRD).ConnectionString}
                Dim dataBean As PunchConfirmBean = New PunchConfirmBean() _
                                                  With { _
                                                      .CompID = model.CompID
                                                  }
                Dim sb As StringBuilder = New StringBuilder()
                POSqlCommand.SelectOrganSql(sb)
                Try
                    datas = conn.Query(Of OrganListModel)(sb.ToString(), dataBean).ToList()
                Catch
                    Throw
                End Try
                If datas Is Nothing Or datas.Count = 0 Then
                    Throw New Exception("查無資料!")
                End If
            End Using
            result = True
        Catch ex As Exception
            msg = ex.Message
        End Try
        Return result
    End Function

    ''' <summary>
    ''' SelectFlowOrgan
    ''' 取得功能組織下拉選單
    ''' </summary>
    ''' <param name="model">畫面model</param>
    ''' <param name="datas">回傳資料</param>
    ''' <param name="msg">回傳訊息</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Shared Function SelectFlowOrgan(ByVal model As PO4000Model, ByRef datas As List(Of FlowOrganListModel), ByRef msg As String) As Boolean
        Dim result As Boolean = False
        msg = ""
        datas = New List(Of FlowOrganListModel)()
        Try
            Using conn = New SqlConnection() With {.ConnectionString = ConfigurationManager.ConnectionStrings(_eHRMSDB_ITRD).ConnectionString}
                Dim dataBean As PunchConfirmBean = New PunchConfirmBean() _
                                                  With { _
                                                      .CompID = model.CompID
                                                  }
                Dim sb As StringBuilder = New StringBuilder()
                POSqlCommand.SelectFlowOrganSql(sb)
                Try
                    datas = conn.Query(Of FlowOrganListModel)(sb.ToString(), dataBean).ToList()
                Catch
                    Throw
                End Try
                If datas Is Nothing Or datas.Count = 0 Then
                    Throw New Exception("查無資料!")
                End If
            End Using
            result = True
        Catch ex As Exception
            msg = ex.Message
        End Try
        Return result
    End Function

    ''' <summary>
    ''' 日期算相差月數與日數
    ''' </summary>
    ''' <param name="strFrom"></param>
    ''' <param name="strTo"></param>
    ''' <param name="strType">D-天,M-月</param>
    ''' <returns></returns>
    Public Shared Function GetTimeDiff(SDateStr As String, EDateStr As String, DateType As String) As Integer
        Dim dtStart As DateTime = DateTime.Parse(SDateStr)
        Dim dtEnd As DateTime = DateTime.Parse(EDateStr)

        If DateType = "Day" Then
            '使用TimeSpan提供的Days屬性
            Dim ts As TimeSpan = (dtEnd - dtStart)
            Dim iDays As Integer = ts.Days + 1
            Return iDays
        ElseIf DateType = "Month" Then
            Dim iMonths As Integer = dtEnd.Year * 12 + dtEnd.Month - (dtStart.Year * 12 + dtStart.Month) + 1
            Return iMonths
        ElseIf DateType = "Minute" Then
            Dim ts As TimeSpan = (dtEnd - dtStart)
            Return CInt(ts.TotalMinutes)
        Else
            Return 0
        End If
    End Function

End Class
