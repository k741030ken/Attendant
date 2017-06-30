Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Reflection
Imports Dapper
Imports System.Data

Public Class PO1100 'POxxx

    Private Shared _attendantDB As String = "AattendantDB"

    ''' <summary>
    ''' 取得DB資料
    ''' </summary>
    ''' <param name="model">畫面model</param>
    ''' <param name="datas">回傳資料</param>
    ''' <param name="msg">回傳訊息</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Shared Function SelectPunchSpecialUnitDefine(ByVal model As PO1100Model, ByRef datas As List(Of PunchSpecialUnitDefineBean), ByRef msg As String) As Boolean
        Dim result As Boolean = False
        msg = ""
        datas = New List(Of PunchSpecialUnitDefineBean)()
        Try
            Using conn = New SqlConnection() With {.ConnectionString = ConfigurationManager.ConnectionStrings(_attendantDB).ConnectionString}
                Dim dataBean As PunchSpecialUnitDefineBean = New PunchSpecialUnitDefineBean() _
                                                  With { _
                                                      .CompID = model.CompID, _
                                                      .DeptID = model.DeptID, _
                                                      .OrganID = model.OrganID}
                Dim sb As StringBuilder = New StringBuilder()
                POSqlCommand.SelectPunchSpecialUnitDefineSql(dataBean, sb)
                Try
                    datas = conn.Query(Of PunchSpecialUnitDefineBean)(sb.ToString(), dataBean).ToList()
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
    Public Shared Function InsertPunchSpecialUnitDefine(ByVal model As PO1100Model, ByRef seccessCount As Long, ByRef msg As String) As Boolean
        Dim result As Boolean = False
        msg = ""
        seccessCount = 0
        Try
            Dim databean As PunchSpecialUnitDefineBean = New PunchSpecialUnitDefineBean() With _
                                         {.CompID = model.CompID, _
                                          .CompName = model.CompName, _
                                          .DeptID = model.DeptID, _
                                          .DeptName = model.DeptName, _
                                          .OrganID = model.OrganID, _
                                          .OrganName = model.OrganName, _
                                          .SpecialFlag = model.SpecialFlag, _
                                          .LastChgComp = model.LastChgComp, _
                                          .LastChgID = model.LastChgID}

            Using conn = New SqlConnection() With {.ConnectionString = ConfigurationManager.ConnectionStrings(_attendantDB).ConnectionString}
                conn.Open()
                Dim sb As StringBuilder = New StringBuilder()
                POSqlCommand.SelectSameKeySql(sb) '建立查詢SqlCommand
                Dim newDataBean As List(Of PunchSpecialUnitDefineBean) = New List(Of PunchSpecialUnitDefineBean)()
                '新增前檢查資料庫是否有重複PK，沒有放進待新增容器
                Try
                    Dim count = conn.Query(Of PunchSpecialUnitDefineBean)(sb.ToString(), databean).Count() '執行查詢，結果回傳至TemplateBean物件
                    If count = 0 Then
                        newDataBean.Add(databean)
                    Else
                        Throw New Exception("你欲新增的資料已經存在於資料庫!")
                    End If
                Catch
                    Throw
                End Try
                If newDataBean.Count > 0 Then
                    POSqlCommand.InsertPunchSpecialUnitDefineSql(sb, True) '建立新增SqlCommand
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
    Public Shared Function DeletePunchSpecialUnitDefine(ByVal model As PO1100Model, ByRef seccessCount As Long, ByRef msg As String) As Boolean
        Dim result As Boolean = False
        msg = ""
        seccessCount = 0
        Try
            Dim databean As PunchSpecialUnitDefineBean = New PunchSpecialUnitDefineBean() With _
                                         {.CompID = model.CompID, _
                                          .DeptID = model.DeptID, _
                                          .OrganID = model.OrganID}
            Using conn = New SqlConnection() With {.ConnectionString = ConfigurationManager.ConnectionStrings(_attendantDB).ConnectionString}
                conn.Open()
                Dim sb As StringBuilder = New StringBuilder()
                POSqlCommand.DeletePunchSpecialUnitDefineSql(sb) '建立刪除SqlCommand
                Using trans = conn.BeginTransaction()
                    Try
                        seccessCount = conn.Execute(sb.ToString(), databean, trans) '執行刪除，成功筆數回傳，並做Transaction機制
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
    Public Shared Function UpdatePunchSpecialUnitDefine(ByVal model As PO1100Model,ByVal checkFlag As Boolean, ByRef seccessCount As Long, ByRef msg As String) As Boolean
        Dim result As Boolean = False
        msg = ""
        seccessCount = 0
        Try
            Dim databean As PunchSpecialUnitDefineBean = New PunchSpecialUnitDefineBean() With _
                                         {.CompID = model.CompID, _
                                          .CompName = model.CompName, _
                                          .CompID_Old = model.CompID_Old, _
                                          .DeptID = model.DeptID, _
                                          .DeptName = model.DeptName, _
                                          .DeptID_Old = model.DeptID_Old, _
                                          .OrganID = model.OrganID, _
                                          .OrganName = model.OrganName, _
                                          .OrganID_Old = model.OrganID_Old, _
                                          .SpecialFlag = model.SpecialFlag, _
                                          .LastChgComp = model.LastChgComp, _
                                          .LastChgID = model.LastChgID}
            Using conn = New SqlConnection() With {.ConnectionString = ConfigurationManager.ConnectionStrings(_attendantDB).ConnectionString}
                conn.Open()
                Dim sb As StringBuilder = New StringBuilder()
                Dim newDataBean As List(Of PunchSpecialUnitDefineBean) = New List(Of PunchSpecialUnitDefineBean)()
                If checkFlag Then
                    POSqlCommand.SelectSameKeySql(sb) '建立查詢SqlCommand
                    '新增前檢查資料庫是否有重複PK，沒有放進待新增容器
                    Try
                        Dim count = conn.Query(Of PunchSpecialUnitDefineBean)(sb.ToString(), databean).Count() '執行查詢，結果回傳至TemplateBean物件
                        If count = 0 Then
                            newDataBean.Add(databean)
                        Else
                            Throw New Exception("你欲修改的資料已經存在於資料庫!")
                        End If
                    Catch
                        Throw
                    End Try
                Else
                    newDataBean.Add(databean)
                End If
                
                If newDataBean.Count > 0 Then
                    POSqlCommand.UpdatePunchSpecialUnitDefineSql(sb, True) '建立修改SqlCommand
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
    ''' 將DB資料轉成作格式化
    ''' </summary>
    ''' <param name="dbDataList">DB資料</param>
    ''' <returns>格式化後資料</returns>
    ''' <remarks></remarks>
    Public Shared Function SelectDataFormat(ByVal dbDataList As List(Of PunchSpecialUnitDefineBean)) As List(Of PO1100GridData)
        Dim result = New List(Of PO1100GridData)()
        For index As Integer = 0 To dbDataList.Count - 1
            Dim item As PunchSpecialUnitDefineBean = dbDataList(index)
            Dim data As PO1100GridData = New PO1100GridData()
            data.CompID = item.CompID
            data.CompName = item.CompName
            data.CompID_Name = item.CompID & item.CompName
            data.DeptID = item.DeptID
            data.DeptName = item.DeptName
            data.DeptID_Name = item.DeptID & item.DeptName
            data.OrganID = item.OrganID
            data.OrganName = item.OrganName
            data.OrganID_Name = item.OrganID & item.OrganName
            data.SpecialFlag = item.SpecialFlag
            Select Case item.SpecialFlag
                Case "1"
                    data.SpecialFlagName = "是"
                Case "2"
                    data.SpecialFlagName = "否"
            End Select
            data.LastChgComp = item.LastChgComp
            data.LastChgCompName = item.LastChgCompName
            data.LastChgCompID_Name = item.LastChgComp & item.LastChgCompName
            data.LastChgID = item.LastChgID
            data.LastChgName = item.LastChgName
            data.LastChgID_Name = item.LastChgID & item.LastChgName
            data.LastChgDate = item.LastChgDate
            result.Add(data)
        Next
        Return result
    End Function

    ''' <summary>
    ''' List轉DataTable
    ''' </summary>
    ''' <typeparam name="PO1100GridData"></typeparam>
    ''' <param name="list"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ConvertToDataTable(Of PO1100GridData)(ByVal list As IList(Of PO1100GridData)) As DataTable
        Dim table As New DataTable()
        Dim fieldType() As FieldInfo = GetType(PO1100GridData).GetFields(BindingFlags.NonPublic Or BindingFlags.Instance)
        Dim fieldName() As PropertyInfo = GetType(PO1100GridData).GetProperties()
        For i As Integer = 0 To fieldType.Length - 1 Step 1
            table.Columns.Add(fieldName(i).Name, fieldType(i).FieldType)
        Next

        For Each item As PO1100GridData In list
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
