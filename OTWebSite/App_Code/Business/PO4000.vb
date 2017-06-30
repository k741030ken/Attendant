Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Reflection
Imports Dapper
Imports System.Data

Public Class PO4000 'POxxx

    Private Shared _attendantDB As String = "AattendantDB"
    Private Shared _eHRMSDB_ITRD As String = "eHRMSDB"

    ''' <summary>
    ''' 取得DB資料
    ''' </summary>
    ''' <param name="model">畫面model</param>
    ''' <param name="datas">回傳資料</param>
    ''' <param name="msg">回傳訊息</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Shared Function SelectPunchConfirm(ByVal model As PO4000Model, ByVal searchOrgType As String, ByRef datas As List(Of PunchConfirmBean), ByRef msg As String) As Boolean
        Dim result As Boolean = False
        msg = ""
        datas = New List(Of PunchConfirmBean)()
        Try
            Using conn = New SqlConnection() With {.ConnectionString = ConfigurationManager.ConnectionStrings(_attendantDB).ConnectionString}
                Dim dataBean As PunchConfirmBean = New PunchConfirmBean()
                dataBean.CompID = model.CompID
                dataBean.PunchFlag = model.PunchFlag
                dataBean.OrganID = model.OrganID
                dataBean.FlowOrganID = model.FlowOrganID
                dataBean.PunchSDate = model.PunchSDate
                dataBean.PunchEDate = model.PunchEDate
                dataBean.PunchSTime = model.PunchSTime
                dataBean.PunchETime = model.PunchETime
                dataBean.ConfirmPunchFlag = model.ConfirmPunchFlag
                dataBean.EmpID = model.EmpID
                dataBean.EmpName = model.EmpName

                Dim sb As StringBuilder = New StringBuilder()
                POSqlCommand.SelectPunchConfirmSql(dataBean, searchOrgType, "", sb)
                Try
                    datas = conn.Query(Of PunchConfirmBean)(sb.ToString(), dataBean).ToList()
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
    ''' 修改DB資料
    ''' </summary>
    ''' <param name="model">畫面model</param>
    ''' <param name="seccessCount">變更筆數</param>
    ''' <param name="msg">回傳訊息</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Shared Function UpdatePunchConfirm(ByVal models As List(Of PO4000Model), ByRef seccessCount As Long, ByRef msg As String) As Boolean
        Dim result As Boolean = False
        Dim dataBeans As List(Of PunchConfirmBean) = New List(Of PunchConfirmBean)()
        msg = ""
        seccessCount = 0
        Try
            For Each model In models
                Dim dataBean As PunchConfirmBean = New PunchConfirmBean() With _
                                             {
                                                 .CompID = model.CompID, _
                                                 .EmpID = model.EmpID, _
                                                 .DutyDate = model.DutyDate, _
                                                 .PunchConfirmSeq = model.PunchConfirmSeq, _
                                                 .LastChgComp = model.LastChgComp, _
                                                 .LastChgID = model.LastChgID
                                             }
                dataBeans.Add(dataBean)
            Next

            Using conn = New SqlConnection() With {.ConnectionString = ConfigurationManager.ConnectionStrings(_attendantDB).ConnectionString}
                conn.Open()
                Dim sb As StringBuilder = New StringBuilder()
                If dataBeans.Count > 0 Then
                    POSqlCommand.UpdatePunchConfirmSql(sb, True) '建立修改SqlCommand
                    Using trans = conn.BeginTransaction()
                        Try
                            seccessCount = conn.Execute(sb.ToString(), dataBeans, trans) '執行新增，成功筆數回傳，並做Transaction機制
                            trans.Commit() '成功Transaction直接Commit
                        Catch
                            trans.Rollback() '失敗Transaction Rollback
                            Throw
                        End Try
                    End Using
                End If
            End Using
            result = True
        Catch ex As Exception
            msg = ex.Message
        End Try
        Return result
    End Function

    ''' <summary>
    ''' 將DB資料轉成作格式化
    ''' </summary>
    ''' <param name="dbDataList">DB資料</param>
    ''' <returns>格式化後資料</returns>
    ''' <remarks></remarks>
    Public Shared Function GridDataFormat(ByVal dbDataList As List(Of PunchConfirmBean)) As List(Of PO4000GridData)
        Dim result = New List(Of PO4000GridData)()
        For index As Integer = 0 To dbDataList.Count - 1
            Dim item As PunchConfirmBean = dbDataList(index)
            Dim data As PO4000GridData = New PO4000GridData()
            data.CompID = item.CompID
            data.AbnormalType = "異常" & item.AbnormalType
            data.DutyDate = item.DutyDate
            data.PunchDate = item.PunchSDate.Replace("-", "/")
            data.PunchTime = item.PunchSTime
            data.PunchConfirmSeq = item.PunchConfirmSeq
            Select Case item.ConfirmPunchFlag
                Case "1"
                    data.ConfirmPunchFlag = "出勤打卡"
                Case "2"
                    data.ConfirmPunchFlag = "退勤打卡"
                Case "3"
                    data.ConfirmPunchFlag = "午休開始"
                Case "4"
                    data.ConfirmPunchFlag = "午休結束"
            End Select
            data.OrganID = item.DeptName & "/" & item.OrganName
            data.EmpID = item.EmpID
            data.EmpName = item.EmpName
            Select Case item.Remedy_AbnormalFlag
                Case "0"
                    data.Remedy_AbnormalReasonCN = "無異常"
                Case "1"
                    data.Remedy_AbnormalReasonCN = "公務"
                Case "2"
                    data.Remedy_AbnormalReasonCN = item.Remedy_AbnormalReasonCN
            End Select
            data.ConfirmStatus = item.ConfirmStatus
            Select Case item.ConfirmStatus
                Case "0"
                    data.ConfirmStatusCN = "正常"
                Case "1"
                    data.ConfirmStatusCN = "異常"
                Case "2"
                    data.ConfirmStatusCN = "補登作業中"
                Case "3"
                    data.ConfirmStatusCN = "異常不控管"
            End Select
            result.Add(data)
        Next
        Return result
    End Function

    ''' <summary>
    ''' List轉DataTable
    ''' </summary>
    ''' <typeparam name="PO4000GridData"></typeparam>
    ''' <param name="list"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ConvertToDataTable(Of PO4000GridData)(ByVal list As IList(Of PO4000GridData)) As DataTable
        Dim table As New DataTable()
        Dim fieldType() As FieldInfo = GetType(PO4000GridData).GetFields(BindingFlags.NonPublic Or BindingFlags.Instance)
        Dim fieldName() As PropertyInfo = GetType(PO4000GridData).GetProperties()
        For i As Integer = 0 To fieldType.Length - 1 Step 1
            table.Columns.Add(fieldName(i).Name, fieldType(i).FieldType)
        Next

        For Each item As PO4000GridData In list
            Dim row As DataRow = table.NewRow()
            Dim data As String = item.ToString
            For i As Integer = 0 To fieldType.Length - 1 Step 1
                row(fieldName(i).Name) = fieldType(i).GetValue(item)
            Next
            table.Rows.Add(row)
        Next
        Return table
    End Function

End Class
