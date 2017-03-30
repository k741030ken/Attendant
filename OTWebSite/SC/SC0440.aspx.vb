'****************************************************
'功能說明：信用風險額度維護
'建立人員：Tsao
'建立日期：2014/03/10
'****************************************************
Imports System.Data

Partial Class SC_SC0440
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            BindData()
            If StateTransfer Is Nothing Then DoQuery()
            Page.SetFocus(ddlYear)
        End If
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

        For Each strKey As String In ht.Keys
            Dim ctl As Control = Page.Form.FindControl(strKey)

            If ctl IsNot Nothing Then
                If TypeOf ctl Is DropDownList Then
                    Bsp.Utility.SetSelectedIndex(CType(ctl, DropDownList), ht(strKey))
                ElseIf TypeOf ctl Is TextBox Then
                    CType(ctl, TextBox).Text = ht(strKey)
                End If
            End If
        Next
        If Not IsPostBack() Then
            BindData()
        End If
        DoQuery()
        If ht.ContainsKey("PageIndex") Then
            pcMain.PageNo = ht("PageIndex")
            pcMain.BindGridView()
        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnAdd"
                DoAdd()
            Case "btnQuery"
                DoQuery()
            Case "btnUpdate"    '修改
                DoUpdate()
            Case "btnDelete"    '刪除
                DoDelete()
            Case "btnActionX"
                DoUpload()
        End Select
    End Sub

    Private Sub BindData()
        Dim strSQL As New StringBuilder
        strSQL.AppendLine("SELECT DISTINCT Year FROM SC_RankLimit")
        strSQL.AppendLine("ORDER BY Year DESC")
        Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
            With ddlYear
                .DataTextField = "Year"
                .DataValueField = "Year"
                .DataSource = dt
                .DataBind()
            End With
        End Using

        ddlYear.Items.Insert(0, New ListItem("---所有年份---", ""))

        Bsp.Utility.FillCommon(Me.ddlType, "743", Bsp.Enums.SelectCommonType.Valid, Bsp.Enums.FullNameType.OnlyDefine)
        ddlType.Items.Insert(0, New ListItem("---所有類別---", ""))

    End Sub


    Private Sub DoAdd()
        Dim btnA As New ButtonState(ButtonState.emButtonType.Add)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)

        btnA.Caption = "存檔返回"
        btnX.Caption = "返回"
        Me.CallModalPage("~/SC/SC0441.aspx", "500px", "300px", New ButtonState() {btnA, btnX})

    End Sub

    Private Sub DoUpdate()
        Dim btnU As New ButtonState(ButtonState.emButtonType.Update)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)

        btnU.Caption = "存檔返回"
        btnX.Caption = "返回"

        Me.CallModalPage("~/SC/SC0442.aspx", "500px", "300px", New ButtonState() {btnU, btnX}, _
                    "Year=" & gvMain.DataKeys(selectedRow(gvMain))("Year").ToString(), _
                    "Type=" & gvMain.DataKeys(selectedRow(gvMain))("Type").ToString(), _
                    "Rank=" & gvMain.DataKeys(selectedRow(gvMain))("Rank").ToString())
    End Sub

    Private Sub DoQuery()
        Dim strSQL As New StringBuilder
        strSQL.AppendLine("SELECT S.*, (SELECT Define FROM SC_Common WHERE Type = '743' AND Code = S.Type) TypeShow, CONVERT( varchar, CAST(S.RankLimit AS money),1) RankLimitShow FROM SC_RankLimit S")
        strSQL.AppendLine("WHERE 1 = 1")
        If ddlYear.SelectedValue <> "" Then
            strSQL.AppendLine("  AND S.Year = " & Bsp.Utility.Quote(ddlYear.SelectedValue))
        End If
        If ddlType.SelectedValue <> "" Then
            strSQL.AppendLine("  AND S.Type = " & Bsp.Utility.Quote(ddlType.SelectedValue))
        End If
        strSQL.AppendLine("ORDER BY S.Year DESC, S.Type, S.Rank ")

        Try
            pcMain.DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables(0)
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
        End Try
    End Sub

    Private Sub DoDelete()
        Dim strSQL As New StringBuilder

        strSQL.AppendLine("DELETE SC_RankLimit ")
        strSQL.AppendLine("WHERE Year = " & Bsp.Utility.Quote(gvMain.DataKeys(selectedRow(gvMain))("Year").ToString()))
        strSQL.AppendLine("  AND Type = " & Bsp.Utility.Quote(gvMain.DataKeys(selectedRow(gvMain))("Type").ToString()))
        strSQL.AppendLine("  AND Rank = " & Bsp.Utility.Quote(gvMain.DataKeys(selectedRow(gvMain))("Rank").ToString()))

        Try
            Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString())

            DoQuery()
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoDelete", ex)
        End Try

    End Sub

    Private Sub DoUpload()
        Dim btnA As New ButtonState(ButtonState.emButtonType.Add)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)

        btnA.Caption = "上傳"
        btnX.Caption = "返回"

        Me.CallModalPage("~/SC/SC0443.aspx", "500px", "300px", New ButtonState() {btnA, btnX})
    End Sub

    Protected Overrides Sub BaseOnPageReturn(ByVal ti As TransferInfo)
        'gvMain.DataBind()
    End Sub

    Public Overrides Sub DoModalReturn(ByVal returnValue As String)
        DoQuery()
    End Sub


End Class
