'****************************************************
'功能說明：快速查詢網頁(QFind)
'建立人員：Ann
'建立日期：2013.11.27
'****************************************************
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports System.Data.Common
Imports System.Data

Partial Class Component_PagePosition
    Inherits PageBase

    '左選項Title說明
    Public Property LeftCaption() As String
        Get
            Return lblLeftCaption.Text
        End Get
        Set(ByVal Value As String)
            lblLeftCaption.Text = Value
        End Set
    End Property

    '右選項Title說明
    Public Property RightCaption() As String
        Get
            Return lblRightCaption.Text
        End Get
        Set(ByVal Value As String)
            lblRightCaption.Text = Value
        End Set
    End Property

    Public Property ListRows() As Integer
        Get
            Return lstLeft.Rows
        End Get
        Set(ByVal value As Integer)
            lstLeft.Rows = value
            lstRight.Rows = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then

            Dim aryParam() As Object = Session(Request("ControlSessionID"))

            If Not Session(Request("ControlSessionID")) Is Nothing Then
                ViewState.Item("QueryCompID") = aryParam(0)
                ViewState.Item("QueryRecID") = aryParam(1)
                ViewState.Item("DefaultPosition") = aryParam(2)
                ViewState.Item("Fields") = aryParam(3)
                ViewState.Item("QueryOrganID") = aryParam(4)
                ViewState.Item("IsWait") = aryParam(5)
                ViewState.Item("ValidDate") = aryParam(6)

                If ViewState.Item("DefaultPosition") <> "" Then
                    '自訂內容(已選)
                    LoadRightData()
                End If

                '自訂內容(未選)
                LoadLeftData()
            End If

            LoadField()

            RegistScript()
        End If

        '原來版本-----
        If txtLeftResult.Text.Length > 0 Or txtRightResult.Text.Length > 0 Then
            RebuildList()
        End If

        If Not Page.ClientScript.IsClientScriptBlockRegistered("UserControl") Then
            Dim cstype As Type = Me.Page.GetType()
            Dim script As String = "<script  type='text/javascript' src='" & Me.ResolveUrl("~/ClientFun/UserControl.js") & "'></script>" & vbNewLine
            Me.Page.ClientScript.RegisterClientScriptBlock(cstype, "PagePosition", script)
        End If

    End Sub

    'List Load自訂內容(未選)
    Public Sub LoadLeftData()
        Dim objUC As New UC
        Dim strWhere As String = ""

        Try
            '帶入公司別
            If Bsp.Utility.Quote(ViewState.Item("QueryCompID").ToString) <> "" Then
                strWhere = "and CompID = " & Bsp.Utility.Quote(ViewState.Item("QueryCompID").ToString)
            End If

            If txtQueryString.Text.Trim().Length > 0 Then
                strWhere += " And Upper([" & ddlField.SelectedValue & "]) like Upper('%" & txtQueryString.Text.Trim() & "%')"
                'strWhere += " And [" & ddlField.SelectedValue & "] like " & Bsp.Utility.Quote(txtQueryString.Text.Trim() & "%")
            End If

            If ViewState.Item("DefaultPosition").ToString() <> "" Then
                strWhere += "And PositionID not in ( " & ViewState.Item("DefaultPosition").ToString() & ")"
            End If

            '20161108 增加待異動組織(OrganizationWait)判斷
            If ViewState.Item("IsWait") = True Then
                If ViewState.Item("QueryOrganID").ToString() <> "" Then
                    strWhere += "And PositionID in (Select PositionID From OrgPositionWait Where OrganReason = '1' And WaitStatus = '0' And OrganType In ('1', '3')"
                    If ViewState.Item("ValidDate") <> "" Then
                        strWhere += " And ValidDate <= " & Bsp.Utility.Quote(ViewState.Item("ValidDate"))
                    End If
                    strWhere += " And CompID = " & Bsp.Utility.Quote(ViewState.Item("QueryCompID").ToString) & " And OrganID = " & Bsp.Utility.Quote(ViewState.Item("QueryOrganID").ToString) & ")"
                End If
            Else
                If ViewState.Item("QueryOrganID").ToString() <> "" Then
                    strWhere += "And PositionID in (Select PositionID From OrgPosition Where CompID = " & Bsp.Utility.Quote(ViewState.Item("QueryCompID").ToString) & " And OrganID = " & Bsp.Utility.Quote(ViewState.Item("QueryOrganID").ToString) & ")"
                End If
            End If

            Using dt As DataTable = objUC.GetPositionByUC("", "PositionID, PositionID + '-' + Remark ", strWhere)
                'Using dt As DataTable = objRE.GetPosition("PositionID", " PositionID + '-' + Remark ", strWhere)
                With lstLeft
                    .DataSource = dt
                    .DataValueField = dt.Columns(0).ColumnName
                    .DataTextField = dt.Columns(1).ColumnName
                    '.DataTextField = dt.Columns(2).ColumnName
                    .DataBind()

                    '扣除已選取的資料
                    If txtRightResult.Text.Trim() <> "" Then
                        Dim strLists() As String = txtRightResult.Text.Split(CChar(vbTab))
                        For intLoop As Integer = 0 To strLists.GetUpperBound(0)
                            Dim intIndex As Integer = .Items.IndexOf(.Items.FindByValue(strLists(intLoop).Split("|")(1)))

                            If intIndex >= 0 Then
                                .Items.RemoveAt(intIndex)
                            End If
                        Next
                    End If
                End With
            End Using

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, "PagePosition.LoadLeftData", ex)
            Return
        End Try

    End Sub

    'List Load自訂內容(已選)
    Public Sub LoadRightData()
        Dim objUC As New UC
        Dim strWhere As String = ""
        Dim strValue As String = ""
        Dim strText As String = ""

        Try
            lstRight.Items.Clear()
            '輸入多筆的職位，因第一筆為主要職位，為了要lstRight按照此順序顯示，只能BY每一筆GetPosition再寫入lstRight
            Dim aryPosition() As String = ViewState.Item("DefaultPosition").ToString().Replace("'", "").Split(",")
            Dim strCompID As String = Bsp.Utility.Quote(ViewState.Item("QueryCompID").ToString)

            For intLoop As Integer = 0 To aryPosition.GetUpperBound(0)
                If aryPosition(intLoop) <> "" Then
                    strWhere = "And PositionID ='" & aryPosition(intLoop) & "' "
                    strWhere = "And CompID =" & strCompID & ""

                    Using dt As DataTable = objUC.GetPositionByUC(aryPosition(intLoop), "PositionID, PositionID + '-' + Remark ", strWhere)
                        If dt.Rows.Count > 0 Then
                            '使用控制項ListBox 加入 ListItem
                            '一般來說我們使用控制項Listbox 需要加入值時，就直接打 Listbbox.items.add("whohow") 但這樣加入時，該筆item的text就是"whohow"，value也是"whohow" 
                            '如果我們需要將text跟value指定不同的值時，就需要用到ListItem這個控制項了 注意的是~每次加入ListItem到ListBox時，再加入下一筆，就需要再 "new" 一個ListItem 
                            'lstRight.Items.Add(dt.Rows(0).Item(1).ToString())
                            Dim myitem As New ListItem
                            myitem.Value = Trim(dt.Rows(0).Item(0).ToString())
                            myitem.Text = Trim(dt.Rows(0).Item(1).ToString())
                            lstRight.Items.Add(myitem)

                            If strValue <> "" Then strValue &= ","
                            If strText <> "" Then strText &= ","
                            strValue &= dt.Rows(0).Item(0).ToString()
                            strText &= dt.Rows(0).Item(1).ToString()
                        Else
                            Dim myitem As New ListItem
                            myitem.Value = aryPosition(intLoop)
                            myitem.Text = aryPosition(intLoop) + "-"
                            lstRight.Items.Add(myitem)
                            If strValue <> "" Then strValue &= ","
                            If strText <> "" Then strText &= ","
                            strValue &= aryPosition(intLoop)
                            strText &= ""
                        End If
                        
                    End Using
                End If
            Next

            txtValueResult.Text = strValue
            txtTextResult.Text = strText

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me.Page, "PagePosition.LoadRightData", ex)
            Return
        End Try

    End Sub

    '清除List內容
    Public Sub ClearList()
        lstLeft.Items.Clear()
        lstRight.Items.Clear()
        txtLeftResult.Text = ""
        txtRightResult.Text = ""
        txtValueResult.Text = ""
        txtTextResult.Text = ""
    End Sub

    Private Sub RebuildList()
        Try
            lstRight.Items.Clear()
            lstLeft.Items.Clear()
            Dim strLists() As String
            If txtRightResult.Text.Length > 0 Then
                strLists = txtRightResult.Text.Split(CChar(vbTab))
                For Each strKey As String In strLists
                    lstRight.Items.Add(New ListItem(strKey.Split("|")(0), strKey.Split("|")(1)))
                Next
            End If

            If txtLeftResult.Text.Length > 0 Then
                strLists = txtLeftResult.Text.Split(CChar(vbTab))
                For Each strKey As String In strLists
                    lstLeft.Items.Add(New ListItem(strKey.Split("|")(0), strKey.Split("|")(1)))
                Next
            End If
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me.Page, Bsp.Utility.getInnerException("PagePosition.RebuildList", ex))
            Return
        End Try
    End Sub

    Private Sub RegistScript()
        Const CS_FUN As String = "javascript:{0}(this.form.{1},this.form.{2},this.form.{3},this.form.{4},this.form.{5},this.form.{6});return false;"
        btnMoveLeft.Attributes("OnClick") = String.Format(CS_FUN, "MoveLeft", lstLeft.ClientID, txtLeftResult.ClientID, lstRight.ClientID, txtRightResult.ClientID, txtTextResult.ClientID, txtValueResult.ClientID)
        btnMoveRight.Attributes("OnClick") = String.Format(CS_FUN, "MoveRight", lstLeft.ClientID, txtLeftResult.ClientID, lstRight.ClientID, txtRightResult.ClientID, txtTextResult.ClientID, txtValueResult.ClientID)
        btnMoveUp.Attributes("OnClick") = String.Format(CS_FUN, "MoveUp", lstLeft.ClientID, txtLeftResult.ClientID, lstRight.ClientID, txtRightResult.ClientID, txtTextResult.ClientID, txtValueResult.ClientID)
        btnMoveDown.Attributes("OnClick") = String.Format(CS_FUN, "MoveDown", lstLeft.ClientID, txtLeftResult.ClientID, lstRight.ClientID, txtRightResult.ClientID, txtTextResult.ClientID, txtValueResult.ClientID)
    End Sub

    '回傳結果
    Public Overrides Sub DoAction(ByVal Param As String)
        Dim strValueSql As String = ""

        Select Case Param
            Case "btnActionC"
                'If txtValueResult.Text.Length = 0 Then
                '    Bsp.Utility.ShowFormatMessage(Me, "W_00030", "職位")
                '    Return
                'End If

                Dim aryValue() As String = Split(txtValueResult.Text, ",")
                For intLoop As Integer = 0 To aryValue.GetUpperBound(0)
                    If strValueSql <> "" Then strValueSql += ","
                    strValueSql += "'" & aryValue(intLoop) & "'"
                Next

                Bsp.Utility.RunClientScript(Me, "window.top.returnValue='" & Replace(strValueSql, "'", "\'") & "';window.top.close();")
                'Bsp.Utility.RunClientScript(Me, "window.top.returnValue='" & Replace(txtValueResult.Text, "'", "\'") & "';window.top.close();")
        End Select
    End Sub

    '載入查詢的下拉式選單值
    Private Sub LoadField()
        Dim Fields() As FieldState = ViewState.Item("Fields")
        For intX As Integer = 0 To Fields.Length - 1
            ddlField.Items.Add(New ListItem(Fields(intX).HeaderName, Fields(intX).FieldName))
        Next
    End Sub

    '查詢
    Protected Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        LoadLeftData()
    End Sub

End Class
