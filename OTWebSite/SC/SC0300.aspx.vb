'****************************************************
'功能說明：功能維護
'建立人員：Ann
'建立日期：2014.08.26
'****************************************************
Imports System.Data

Partial Class SC_SC0300
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objSC As New SC()

        ddlFunID.Attributes.Add("onkeypress", "EntertoSubmit();")
        txtFunName.Attributes.Add("onkeypress", "EntertoSubmit();")

        Dim strSysID As String
        strSysID = Bsp.Utility.subGetSysID(UserProfile.LoginSysID)
        Dim arySysID() As String = Split(strSysID, "-")
        lblSysName.Text = strSysID

        If Not IsPostBack Then
            'Bsp.Utility.FillFun(ddlParentFun, Bsp.Enums.SelectFunType.ParentFun, "")
            Bsp.Utility.FillFun(ddlParentFun, Bsp.Enums.SelectFunType.ParentFun, "", "")
            'ddlParentFun.Items.Insert(0, New ListItem("---不指定---", "$$"))
            ddlParentFun.Items.Insert(0, New ListItem("---請選擇---", "$$")) '20150225 Beatrice modify 下拉式查詢：預設「---請選擇---」
            ddlParentFun.Items.Insert(1, New ListItem("---父功能---", ""))

            '20150225 Beatrice modify 功能代碼改下拉式選單(代碼+名稱，EX：SC0300 – 功能維護)
            Bsp.Utility.FillFun(ddlFunID, Bsp.Enums.SelectFunType.AllFun, "", "")
            ddlFunID.Items.Insert(0, New ListItem("---請選擇---", ""))

            Page.SetFocus(ddlFunID)
        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnAdd"       '新增
                DoAdd()
            Case "btnUpdate"    '修改
                DoUpdate()
            Case "btnQuery"     '查詢
                ViewState.Item("DoQuery") = "Y"
                DoQuery()
            Case "btnDelete"    '刪除
                DoDelete()
            Case "btnActionC"   '確認
            Case "btnActionP"   '列印
        End Select
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

            For Each strKey As String In ht.Keys
                Dim ctl As Control = Me.FindControl(strKey)

                If TypeOf ctl Is TextBox Then
                    CType(ctl, TextBox).Text = ht(strKey).ToString()
                ElseIf TypeOf ctl Is DropDownList Then
                    'Bsp.Utility.SetSelectedIndex(ddlParentFun, ht(strKey).ToString())
                    '20150225 Beatrice modify
                    If ht(strKey).ToString <> "" Then
                        CType(ctl, DropDownList).SelectedValue = ht(strKey).ToString()
                    End If
                    'If ctl.ID.Equals("ddlParentFun") Then
                    '    If ht(strKey).ToString() <> "$$" Then
                    '        If ht(strKey).ToString() = "" Then
                    '            Bsp.Utility.FillFun(ddlFunID, Bsp.Enums.SelectFunType.ParentFun, "", "")
                    '            ddlFunID.Items.Insert(0, New ListItem("---請選擇---", ""))
                    '        Else
                    '            subGetFunIDbyParentFormID(ht(strKey).ToString())
                    '        End If
                    '    End If
                    'End If
                    '20150225 Beatrice modify End
                End If
            Next

            If ht("ddlParentFun").ToString() <> "$$" Then
                ddlParentFun.SelectedValue = ht("ddlParentFun").ToString()
                ddlParentFun_SelectedIndexChanged(Nothing, Nothing)
            End If

            If ht.ContainsKey("PageNo") Then pcMain.PageNo = Convert.ToInt32(ht("PageNo"))
            If ht.ContainsKey("DoQuery") Then
                If ht("DoQuery").ToString() = "Y" Then
                    ViewState.Item("DoQuery") = "Y"
                    DoQuery()
                End If
            End If
        End If
    End Sub

    Private Sub DoAdd()
        Dim btnA As New ButtonState(ButtonState.emButtonType.Add)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)

        btnA.Caption = "存檔返回"
        btnX.Caption = "返回"

        Me.TransferFramePage("~/SC/SC0301.aspx", New ButtonState() {btnA, btnX}, _
            Bsp.Utility.FormatToParam(ddlParentFun), _
            Bsp.Utility.FormatToParam(ddlFunID), _
            Bsp.Utility.FormatToParam(txtFunName), _
            "PageNo=" & pcMain.PageNo.ToString(), _
            "DoQuery=" & Bsp.Utility.IsStringNull(ViewState.Item("DoQuery")))
    End Sub

    Private Sub DoUpdate()
        Dim btnU As New ButtonState(ButtonState.emButtonType.Update)
        Dim btnX As New ButtonState(ButtonState.emButtonType.Exit)

        btnU.Caption = "存檔返回"
        btnX.Caption = "返回"

        Dim strSysID As String
        strSysID = Bsp.Utility.subGetSysID(UserProfile.LoginSysID)
        Dim arySysID() As String = Split(strSysID, "-")

        Me.TransferFramePage("~/SC/SC0302.aspx", New ButtonState() {btnU, btnX}, _
            Bsp.Utility.FormatToParam(ddlParentFun), _                             
            Bsp.Utility.FormatToParam(ddlFunID), _
            Bsp.Utility.FormatToParam(txtFunName), _
            "PageNo=" & pcMain.PageNo.ToString(), _
            "SysID=" & arySysID(0), _
            "SelectedFunID=" & gvMain.DataKeys(selectedRow(gvMain))("FunID").ToString(), _
            "DoQuery=Y")
    End Sub

    Private Sub DoQuery()
        Dim objSC As New SC()

        Try
            'pcMain.DataTable = objSC.QueryFun("FunID=" & txtFunID.Text.Trim().ToUpper(), "FunName=" & txtFunName.Text, "ParentFunID=" & ddlParentFun.SelectedValue)
            pcMain.DataTable = objSC.QueryFun("FunID=" & ddlFunID.SelectedValue, "FunName=" & txtFunName.Text, "ParentFunID=" & ddlParentFun.SelectedValue) '20150225 Beatrice modify
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
        End Try
    End Sub

    Private Sub DoDelete()
        If selectedRow(gvMain) < 0 Then
            '尚未選取資料列！
            Bsp.Utility.ShowFormatMessage(Me, "W_00000")
        Else
            Dim beFun As New beSC_Fun.Row()
            Dim objSC As New SC
            Dim strSysID As String
            strSysID = Bsp.Utility.subGetSysID(UserProfile.LoginSysID)
            Dim arySysID() As String = Split(strSysID, "-")
            beFun.SysID.Value = arySysID(0)
            beFun.FunID.Value = gvMain.DataKeys(Me.selectedRow(gvMain))("FunID").ToString()

            Try
                objSC.DeleteFun(beFun)
                objSC.DeleteGroupFun("", beFun.FunID.Value, beFun.SysID.Value, "")

            Catch ex As Exception
                Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoDelete", ex)
            End Try
            gvMain.DataBind()
        End If
    End Sub

    '20150225 Beatrice Add
    Protected Sub ddlParentFun_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlParentFun.SelectedIndexChanged
        If ddlParentFun.SelectedValue <> "" Then
            If ddlParentFun.SelectedValue <> "$$" Then
                subGetFunIDbyParentFormID(ddlParentFun.SelectedValue)
            Else
                Bsp.Utility.FillFun(ddlFunID, Bsp.Enums.SelectFunType.AllFun, "", "")
                ddlFunID.Items.Insert(0, New ListItem("---請選擇---", ""))
            End If
        Else
            Bsp.Utility.FillFun(ddlFunID, Bsp.Enums.SelectFunType.ParentFun, "", "")
            ddlFunID.Items.Insert(0, New ListItem("---請選擇---", ""))
        End If
    End Sub

    Private Sub subGetFunIDbyParentFormID(ByVal strParentFun As String)
        Dim objSC As New SC

        Try
            Using dt As DataTable = objSC.GetFunInfo("", "FunID, FunID + char(9) + FunName as FunName", "And ParentFormID = '" + strParentFun + "' Order by FunID")
                With ddlFunID
                    .DataTextField = "FunName"
                    .DataValueField = "FunID"
                    .DataSource = dt
                    .DataBind()
                    .Items.Insert(0, New ListItem("---請選擇---", ""))
                End With
            End Using
        Catch ex As Exception
            ddlFunID.Items.Insert(0, New ListItem("---請選擇---", ""))
            'Bsp.Utility.ShowMessage(Me, Me.FunID & ".ddlParentFun_SelectedIndexChanged", ex)
        End Try
    End Sub
    '20150225 Beatrice Add End
End Class
