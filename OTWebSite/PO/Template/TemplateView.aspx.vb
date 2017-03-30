Imports Newtonsoft.Json

Partial Class PO_Template_TemplateView
    Inherits PageBase

#Region "1. 全域變數"
    ''' <summary>
    ''' _templateModel
    ''' </summary>
    Private Property _templateModel As TemplateModel '全域private變數要為('_'+'小駝峰')
        Get
            Try
                If ViewState("Template_TemplateModel") IsNot Nothing Then 'ViewState當頁暫存使用
                    Return JsonConvert.DeserializeObject(Of TemplateModel)(ViewState("Template_TemplateModel").ToString())
                Else
                    Return Nothing
                End If
            Catch
                Return Nothing
            End Try
        End Get
        Set(value As TemplateModel)
            ViewState("Template_TemplateModel") = JsonConvert.SerializeObject(value)
        End Set
    End Property
    ' ''' <summary>
    ' ''' _templateModel
    ' ''' </summary>
    'Private Property _templateModel As TemplateModel '全域private變數要為('_'+'小駝峰')
    '    Get
    '        If Session("Template_TemplateModel") Is Nothing Then 'Session當下瀏覽器暫存使用(用於跨頁但需要做好用完就clear的處理)\
    '            Session("Template_TemplateModel") = New TemplateModel()
    '        End If
    '        Return Session("Template_TemplateModel")
    '    End Get
    '    Set(value As TemplateModel)
    '        Session("Template_TemplateModel") = value
    '    End Set
    'End Property
#End Region

#Region "2. 功能鍵處理邏輯"
    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnAdd"       '新增
                DoAdd()
            Case "btnUpdate"    '修改
                DoUpdate()
            Case "btnQuery"     '查詢
                DoQuery()
            Case "btnDelete"    '刪除
                DoDelete()
        End Select
    End Sub
#End Region

#Region "3. Override Method"
    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then

        End If
    End Sub
#End Region

#Region "4. Page_Load"

    ''' <summary>
    ''' 起始
    ''' </summary>
    ''' <param name="sender">object</param>
    ''' <param name="e">EventArgs</param>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            LoadData() '載入資料
        End If
    End Sub
#End Region

#Region "5. 畫面事件"
    ''' <summary>
    ''' gvMain_RowDataBound
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">GridViewRowEventArgs</param>
    Protected Sub gvMain_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvMain.RowDataBound

    End Sub
    ''' <summary>
    ''' gvMain_RowCommand
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">GridViewCommandEventArgs</param>
    Protected Sub gvMain_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvMain.RowCommand

    End Sub
    ''' <summary>
    ''' ddlSexChanged
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">EventArgs</param>
    Protected Sub ddlSexChanged(sender As Object, e As EventArgs) Handles ddlSex.SelectedIndexChanged
        LoadData()
    End Sub
#End Region

#Region "6. 畫面檢核與確認"
    ''' <summary>
    ''' 畫面檢核
    ''' </summary>
    ''' <param name="msg">檢核失敗訊息</param>
    ''' <returns>bool</returns>
    Private Function viewValidation(ByRef msg As String) As Boolean
        Dim result As Boolean = True
        msg = ""
        Dim sb As List(Of String) = New List(Of String)()
        If Not String.IsNullOrEmpty(txtOTCompID.Text) Then
            If POValidation.IsAnyOneChineseWord(txtOTCompID.Text) Or POValidation.IsAnyOneFullWidthWord(txtOTCompID.Text) Then
                sb.Add("公司欄位請勿輸入全形字或中文!!")
                result = False
            End If
        End If
        If Not String.IsNullOrEmpty(txtOTEmpID.Text) Then
            If Not POValidation.IsAllNumber(txtOTEmpID.Text) Then
                sb.Add("員編欄位只能輸入數字!!")
                result = False
            End If
        End If
        If sb.Count > 0 Then
            msg = String.Join(vbCrLf, sb)
        End If
        Return result
    End Function
    ''' <summary>
    ''' 邏輯檢核
    ''' </summary>
    ''' <param name="msg">檢核失敗訊息</param>
    ''' <returns>Boolean</returns>
    Private Function logicValidation(ByRef msg As String) As Boolean
        Dim result As Boolean = True
        msg = ""
        Return result
    End Function
#End Region

#Region "7. private Method"
    ''' <summary>
    ''' 載入資料
    ''' </summary>
    Private Sub LoadData()
        Dim isSuccess As Boolean = False
        Dim msg As String = ""
        Dim datas As List(Of TemplateBean) = New List(Of TemplateBean)()
        Dim viewData As TemplateModel = New TemplateModel() With _
        { _
            .OTCompID = txtOTCompID.Text,
            .OTEmpID = txtOTEmpID.Text,
            .NameN = txtNameN.Text,
            .Sex = ddlSex.SelectedValue
        }
        isSuccess = Template.GetEmpFlowSNEmpAndSexUseDapper(viewData, datas, msg)
        If isSuccess And datas.Count > 0 Then
            viewData.TemplateGridDataList = Template.DataFormat(datas) 'Format Data         
        End If
        gvMain.DataSource = viewData.TemplateGridDataList
        gvMain.DataBind()
        _templateModel = viewData
    End Sub
    ''' <summary>
    ''' 新增邏輯
    ''' </summary>
    Private Sub DoAdd()
        Dim result As Boolean = False
        Dim seccessCount As Long = 0
        Dim msg As String = ""
        Try
            Dim model As TemplateModel = _templateModel
            result = Template.InsertEmpFlowSNEmp(model, seccessCount, msg)
            If Not result Then
                Throw New Exception(msg)
            End If
            If seccessCount = 0 Then
                Throw New Exception("無資料被新增!!")
            End If
            Bsp.Utility.ShowMessage(Me, "新增筆數 : " + seccessCount.ToString())
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 修改邏輯
    ''' </summary>
    Private Sub DoUpdate()
        Dim result As Boolean = False
        Dim seccessCount As Long = 0
        Dim msg As String = ""
        Try
            Dim model As TemplateModel = _templateModel
            result = Template.UpdateEmpFlowSNEmp(model, seccessCount, msg)
            If Not result Then
                Throw New Exception(msg)
            End If
            If seccessCount = 0 Then
                Throw New Exception("無資料被修改!!")
            End If
            Bsp.Utility.ShowMessage(Me, "修改筆數 : " + seccessCount.ToString())
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 查詢邏輯
    ''' </summary>
    Private Sub DoQuery()
        Dim msg As String = ""
        Try
            If Not viewValidation(msg) Then '畫面檢核
                Throw New Exception(msg)
            End If
            If Not logicValidation(msg) Then '邏輯檢核
                Throw New Exception(msg)
            End If
            LoadData()
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 刪除邏輯
    ''' </summary>
    Private Sub DoDelete()
        Dim result As Boolean = False
        Dim seccessCount As Long = 0
        Dim msg As String = ""
        Try
            Dim model As TemplateModel = _templateModel
            result = Template.DeleteEmpFlowSNEmp(model, seccessCount, msg)
            If Not result Then
                Throw New Exception(msg)
            End If
            If seccessCount = 0 Then
                Throw New Exception("無資料被刪除!!")
            End If
            Bsp.Utility.ShowMessage(Me, "刪除筆數 : " + seccessCount.ToString())
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, ex.Message)
        End Try
    End Sub
#End Region

End Class
