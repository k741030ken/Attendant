Imports System.Data
Imports System.Globalization
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common

Partial Class OV_OV4401
    Inherits PageBase

#Region "功能鍵設定"
    ''' <summary>
    ''' 功能鍵設定
    ''' </summary>
    ''' <param name="Param">String</param>
    ''' <remarks></remarks>
    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnUpdate"     '修改補休失效日
                DoUpdate()
            Case "btnCancel"     '清除
                DoCancel()
            Case "btnDelete"     '返回
                GoBack()
        End Select
    End Sub
#End Region

#Region "全域變數"
    '存取顯示資料
    Private Property _showDatas As DataTable
        Get
            Return ViewState.Item("ShowDatas")
        End Get
        Set(ByVal value As DataTable)
            ViewState.Item("ShowDatas") = value
        End Set
    End Property

    '存取傳到修改頁資料
    Private Property _goUpdateDatas As DataTable
        Get
            Return Session.Item("GoUpdateDatas")
        End Get
        Set(ByVal value As DataTable)
            Session.Item("GoUpdateDatas") = value
        End Set
    End Property
#End Region

#Region "Page_Load"
    ''' <summary>
    ''' 起始頁邏輯處理
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            _showDatas = _goUpdateDatas '接收查詢頁暫存的資料
            _goUpdateDatas = Nothing '清掉暫存

            If _showDatas.Rows.Count > 0 Then
                pcMain.DataTable = _showDatas
                gvMain.DataBind()
            End If

        End If
    End Sub
#End Region

#Region "畫面事件"
    ''' <summary>
    ''' grvMergeHeader_RowCreated
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e"></param>
    ''' <remarks>GridViewRowEventArgs</remarks>
    Protected Sub grvMergeHeader_RowCreated(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.Header Then
            Dim HeaderGrid As GridView = DirectCast(sender, GridView)
            Dim HeaderGridRow As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)
            'Dim HeaderCell As New TableCell()
            'HeaderCell.Text = ""
            'HeaderCell.ColumnSpan = 3
            'HeaderCell.CssClass = "td_header"
            'HeaderGridRow.Cells.Add(HeaderCell)
            'HeaderCell = New TableCell()
            'HeaderCell.Text = "加班單預先申請"
            'HeaderCell.ColumnSpan = 6
            'HeaderCell.CssClass = "td_header"
            'HeaderGridRow.Cells.Add(HeaderCell)
            'HeaderCell = New TableCell()
            'HeaderCell.Text = "加班單事後申報"
            'HeaderCell.ColumnSpan = 6
            'HeaderCell.CssClass = "td_header"
            'HeaderGridRow.Cells.Add(HeaderCell)
            gvMain.Controls(0).Controls.AddAt(0, HeaderGridRow)
        End If
    End Sub
#End Region

#Region "功能鍵邏輯處理"
    ''' <summary>
    ''' 返回
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GoBack()
        Dim ti As TransferInfo = Me.StateTransfer
        Me.TransferFramePage(ti.CallerUrl, Nothing, ti.Args)
    End Sub

    ''' <summary>
    ''' 修改補休失效日
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DoUpdate()
        Dim isSuccess As Boolean = False
        Dim message = ""
        Dim sFailDate = DateStringIIF(ucFailDate.DateText) '補休失效日
        Dim bFailDate = Not String.IsNullOrEmpty(sFailDate)
        If Not bFailDate Then
            Bsp.Utility.ShowMessage(Me, "請輸入補休失效日!")
        ElseIf selectedRows(gvMain) = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00000")
        Else
            Dim dt As DataTable = _showDatas.Clone()
            Dim updateKeys As New List(Of Dictionary(Of String, String))()

            Dim count As Integer = 0
            For intRow As Integer = 0 To gvMain.Rows.Count - 1
                Dim objChk As CheckBox = gvMain.Rows(intRow).FindControl("chk_gvMain")
                Dim gvCompID As String = StringIIF(gvMain.DataKeys(intRow)("OTCompID"))
                Dim gvEmpID As String = StringIIF(gvMain.DataKeys(intRow)("OTEmpID"))
                Dim gvStartDate As String = StringIIF(gvMain.DataKeys(intRow)("OTStartDate"))
                Dim gvEndDate As String = StringIIF(gvMain.DataKeys(intRow)("OTEndDate"))
                Dim gvSeq As String = StringIIF(gvMain.DataKeys(intRow)("OTSeq"))
                Dim newRowData As DataRow = (From column In _showDatas.Rows _
                               Where column("OTCompID") = gvCompID _
                               And column("OTEmpID") = gvEmpID _
                               And column("OTStartDate") = gvStartDate _
                               And column("OTEndDate") = gvEndDate _
                               And column("OTSeq") = gvSeq _
                               ).FirstOrDefault()
                If objChk.Checked And gvCompID <> "" And gvEmpID <> "" And gvStartDate <> "" And gvEndDate <> "" And gvSeq <> "" Then
                    Dim updateKey As Dictionary(Of String, String) = New Dictionary(Of String, String)
                    updateKey.Add("OTCompID", gvCompID)
                    updateKey.Add("OTEmpID", gvEmpID)
                    updateKey.Add("OTStartDate", gvStartDate)
                    updateKey.Add("OTEndDate", gvEndDate)
                    updateKey.Add("OTSeq", gvSeq)
                    updateKeys.Add(updateKey)

                    newRowData.Item("AdjustInvalidDate") = sFailDate
                    newRowData.Item("AdjustInvalidDateShow") = sFailDate
                    dt.ImportRow(newRowData)
                Else
                    dt.ImportRow(newRowData)
                End If
            Next
            If dt.Rows.Count > 0 And updateKeys.Count > 0 Then
                isSuccess = UpdateFailDates(updateKeys, sFailDate, message) '修改補休失效日(DB)

                If Not isSuccess Then
                    dt = Nothing
                    updateKeys = Nothing
                    Bsp.Utility.ShowMessage(Me, message)
                Else
                    pcMain.DataTable = dt
                    _showDatas = dt
                    gvMain.DataBind()
                End If
            Else
                Bsp.Utility.ShowFormatMessage(Me, "W_00000")
            End If
        End If
    End Sub

    ''' <summary>
    ''' 清除
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DoCancel()
        ucFailDate.DateText = ""
        If selectedRows(gvMain) <> "" Then
            For intRow As Integer = 0 To gvMain.Rows.Count - 1
                Dim objChk As CheckBox = gvMain.Rows(intRow).FindControl("chk_gvMain")
                If objChk.Checked Then
                    objChk.Checked = False
                End If
            Next
        End If
    End Sub
#End Region

#Region "Private Methods"

    ''' <summary>
    ''' 修改補休失效日(DB)
    ''' </summary>
    ''' <param name="dataKeys">List(Of Dictionary(Of String, String))</param>
    ''' <param name="newFailDate">String</param>
    ''' <param name="message">ByRef String</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Private Function UpdateFailDates(ByVal dataKeys As List(Of Dictionary(Of String, String)), ByVal newFailDate As String, ByRef message As String) As Boolean
        Dim isSuccess As Boolean = False
        Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")

        Dim strSQL As StringBuilder = New StringBuilder()
        For index As Integer = 0 To dataKeys.Count - 1
            Dim dataKey = dataKeys(index)
            Dim sOTCompID = dataKey("OTCompID")
            Dim sOTEmpID = dataKey("OTEmpID")
            Dim sOTStartDate = dataKey("OTStartDate")
            Dim sOTEndDate = dataKey("OTEndDate")
            Dim sOTSeq = dataKey("OTSeq")

            strSQL.AppendLine("UPDATE ")
            strSQL.AppendLine("OverTimeDeclaration ")
            strSQL.AppendLine("SET ")
            strSQL.AppendLine("AdjustInvalidDate = @AdjustInvalidDate ")
            strSQL.AppendLine(String.Format("WHERE OTCompID = @OTCompID{0} ", index))
            strSQL.AppendLine(String.Format("AND OTEmpID = @OTEmpID{0} ", index))
            strSQL.AppendLine(String.Format("AND OTStartDate = @OTStartDate{0} ", index))
            strSQL.AppendLine(String.Format("AND OTEndDate = @OTEndDate{0} ", index))
            strSQL.AppendLine(String.Format("AND OTSeq = @OTSeq{0} ", index))
            strSQL.AppendLine("; ")
        Next

        Dim dbcmd As DbCommand = db.GetSqlStringCommand(strSQL.ToString())
        db.AddInParameter(dbcmd, "@AdjustInvalidDate", DbType.DateTime, newFailDate)
        For index As Integer = 0 To dataKeys.Count - 1
            Dim dataKey = dataKeys(index)
            Dim sOTCompID = dataKey("OTCompID")
            Dim sOTEmpID = dataKey("OTEmpID")
            Dim sOTStartDate = dataKey("OTStartDate")
            Dim sOTEndDate = dataKey("OTEndDate")
            Dim sOTSeq = dataKey("OTSeq")
            db.AddInParameter(dbcmd, String.Format("@OTCompID{0}", index), DbType.String, sOTCompID)
            db.AddInParameter(dbcmd, String.Format("@OTEmpID{0}", index), DbType.String, sOTEmpID)
            db.AddInParameter(dbcmd, String.Format("@OTStartDate{0}", index), DbType.String, sOTStartDate)
            db.AddInParameter(dbcmd, String.Format("@OTEndDate{0}", index), DbType.String, sOTEndDate)
            db.AddInParameter(dbcmd, String.Format("@OTSeq{0}", index), DbType.String, sOTSeq)
        Next

        Try
            Dim updateCount As Integer = db.ExecuteNonQuery(dbcmd)
            If updateCount > 0 Then
                isSuccess = True
            Else
                message = "修改補休失效日筆數:" + updateCount + "筆"
            End If
        Catch ex As Exception
            message = ex.Message
        End Try
        Return isSuccess
    End Function

    ''' <summary>
    ''' 取得字串(去除null)
    ''' </summary>
    ''' <param name="ob">Object</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Private Function StringIIF(ByVal ob As Object) As String
        Dim result = ""
        If Not ob Is Nothing Then
            If Not String.IsNullOrEmpty(ob.ToString()) Then
                result = ob.ToString()
            End If
        End If
        Return result
    End Function
    Private Function DateStringIIF(ByVal dateStr As String) As String
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

    ''' <summary>
    ''' 取得格式化後的DataTime
    ''' </summary>
    ''' <param name="dateStr">String</param>
    ''' <param name="format">String</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Private Function getDataTimeStr(ByVal dateStr As String, ByVal format As String) As String
        Dim result = ""
        Dim newDate As Date = New Date()
        If dateStr <> "" And dateStr <> "1900-01-01 00:00:00.000" And dateStr <> "1900/1/1 上午 12:00:00" And Date.TryParse(dateStr, newDate) Then
            result = newDate.ToString(format)
        End If
        Return result
    End Function
#End Region

End Class

