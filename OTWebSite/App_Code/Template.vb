Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports Dapper

Public Class Template 'POxxx

    Private Shared _attendantDB As String = "AattendantDB"

    ''' <summary>
    ''' 取得DB資料
    ''' </summary>
    ''' <param name="model">畫面model</param>
    ''' <param name="datas">回傳資料</param>
    ''' <param name="msg">回傳訊息</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Shared Function GetEmpFlowSNEmpAndSexUseDapper(ByVal model As TemplateModel, ByRef datas As List(Of TemplateBean), ByRef msg As String) As Boolean
        Dim result As Boolean = False
        msg = ""
        datas = New List(Of TemplateBean)()
        Try
            Using conn = New SqlConnection() With {.ConnectionString = ConfigurationManager.ConnectionStrings(_attendantDB).ConnectionString}
                Dim dataBean As TemplateBean = New TemplateBean() _
                                                  With { _
                                                      .CompID = model.OTCompID, _
                                                      .EmpID = model.OTEmpID, _
                                                      .NameN = model.NameN, _
                                                      .Sex = model.Sex}
                Dim sb As StringBuilder = New StringBuilder()
                POSqlCommand.GetEmpFlowSNEmpAndSexSql(dataBean, sb)
                Try
                    datas = conn.Query(Of TemplateBean)(sb.ToString(), dataBean).ToList()
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
    ''' 新增DB資料
    ''' </summary>
    ''' <param name="model">畫面model</param>
    ''' <param name="seccessCount">新增筆數</param>
    ''' <param name="msg">回傳訊息</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Shared Function InsertEmpFlowSNEmp(ByVal model As TemplateModel, ByRef seccessCount As Long, ByRef msg As String) As Boolean
        Dim result As Boolean = False
        msg = ""
        seccessCount = 0
        Try
            '測試資料
            Dim dataBean As List(Of TemplateBean) = New List(Of TemplateBean)() From _
            { _
                New TemplateBean() With {.CompID = "ZZZZZZ", .EmpID = "111111", .LastChgComp = "ZZZZZZ", .LastChgID = "111111", .LastChgDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}, _
                New TemplateBean() With {.CompID = "ZZZZZZ", .EmpID = "222222", .LastChgComp = "ZZZZZZ", .LastChgID = "222222", .LastChgDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} _
            }
            Using conn = New SqlConnection() With {.ConnectionString = ConfigurationManager.ConnectionStrings(_attendantDB).ConnectionString}
                conn.Open()
                Dim sb As StringBuilder = New StringBuilder()
                POSqlCommand.SeleteTemplateSql(sb) '建立查詢SqlCommand
                Dim newDataBean As List(Of TemplateBean) = New List(Of TemplateBean)()
                '新增前檢查資料庫是否有重複PK，沒有放進待新增容器
                For index As Integer = 0 To dataBean.Count - 1
                    Dim item As TemplateBean = dataBean(index)
                    Try
                        Dim count = conn.Query(Of TemplateBean)(sb.ToString(), item).Count() '執行查詢，結果回傳至TemplateBean物件
                        If count = 0 Then
                            newDataBean.Add(item)
                        End If
                    Catch
                        Throw
                    End Try
                Next
                If newDataBean.Count > 0 Then
                    POSqlCommand.InsertTemplateSql(sb, True) '建立新增SqlCommand
                    Using trans = conn.BeginTransaction()
                        Try
                            seccessCount = conn.Execute(sb.ToString(), newDataBean, trans) '執行新增，成功筆數回傳，並做Transaction機制
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
    ''' 刪除DB資料
    ''' </summary>
    ''' <param name="model">畫面model</param>
    ''' <param name="seccessCount">刪除筆數</param>
    ''' <param name="msg">回傳訊息</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Shared Function DeleteEmpFlowSNEmp(ByVal model As TemplateModel, ByRef seccessCount As Long, ByRef msg As String) As Boolean
        Dim result As Boolean = False
        msg = ""
        seccessCount = 0
        Try
            '測試資料
            Dim dataBean As List(Of TemplateBean) = New List(Of TemplateBean)() From _
            { _
                New TemplateBean() With {.CompID = "ZZZZZZ", .EmpID = "111111"}, _
                New TemplateBean() With {.CompID = "ZZZZZZ", .EmpID = "222222"} _
            }
            Using conn = New SqlConnection() With {.ConnectionString = ConfigurationManager.ConnectionStrings(_attendantDB).ConnectionString}
                conn.Open()
                Dim sb As StringBuilder = New StringBuilder()
                POSqlCommand.DeleteTemplateSql(sb) '建立刪除SqlCommand
                Using trans = conn.BeginTransaction()
                    Try
                        seccessCount = conn.Execute(sb.ToString(), dataBean, trans) '執行刪除，成功筆數回傳，並做Transaction機制
                        trans.Commit() '成功Transaction直接Commit
                    Catch
                        trans.Rollback() '失敗Transaction Rollback
                        Throw
                    End Try
                End Using
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
    Public Shared Function UpdateEmpFlowSNEmp(ByVal model As TemplateModel, ByRef seccessCount As Long, ByRef msg As String) As Boolean
        Dim result As Boolean = False
        msg = ""
        seccessCount = 0
        Try
            '測試資料
            Dim dataBean As List(Of TemplateBean) = New List(Of TemplateBean)() From _
           { _
               New TemplateBean() With {.CompID = "ZZZZZZ", .EmpID = "111111", .LastChgComp = "ZZZZZZ", .LastChgID = "111111", .LastChgDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}, _
               New TemplateBean() With {.CompID = "ZZZZZZ", .EmpID = "222222", .LastChgComp = "ZZZZZZ", .LastChgID = "222222", .LastChgDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} _
           }
            Using conn = New SqlConnection() With {.ConnectionString = ConfigurationManager.ConnectionStrings(_attendantDB).ConnectionString}
                conn.Open()
                Dim sb As StringBuilder = New StringBuilder()
                POSqlCommand.UpdateTemplateSql(sb) '建立修改SqlCommand
                Using trans = conn.BeginTransaction()
                    Try
                        seccessCount = conn.Execute(sb.ToString(), dataBean, trans) '執行修改，成功筆數回傳，並做Transaction機制
                        trans.Commit() '成功Transaction直接Commit
                    Catch
                        trans.Rollback() '失敗Transaction Rollback
                        Throw
                    End Try
                End Using
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
    Public Shared Function DataFormat(ByVal dbDataList As List(Of TemplateBean)) As List(Of TemplateGridData)
        Dim result = New List(Of TemplateGridData)()
        For index As Integer = 0 To dbDataList.Count - 1
            Dim item As TemplateBean = dbDataList(index)
            Dim data As TemplateGridData = New TemplateGridData()
            data.OTCompID = item.CompID
            data.OTEmpID = item.EmpID
            data.Sex = item.Sex
            data.NameN = item.NameN
            data.ShowOTEmp = item.EmpID + item.NameN
            Select Case item.Sex
                Case "1"
                    data.ShowSex = "男"
                Case "2"
                    data.ShowSex = "女"
            End Select
            result.Add(data)
        Next
        Return result
    End Function

End Class
