'****************************************************
'功能說明：員工通訊資料維護
'建立人員：BeatriceCheng
'建立日期：2015.06.24
'****************************************************
Imports System.Data
Imports System.Text.RegularExpressions

Partial Class ST_ST2402
    Inherits PageBase


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then

            Bsp.Utility.FillDDL(ddlRCountry, "eHRMSDB", "CountryCode", "CodeNo", "CountryName", Bsp.Utility.DisplayType.OnlyName)
            ddlRCountry.Items.Insert(0, New ListItem("---請選擇---", ""))
            ddlRCountry.Items.Add(New ListItem("其他", "其他"))

            Bsp.Utility.FillDDL(ddlCCountry, "eHRMSDB", "CountryCode", "CodeNo", "CountryName", Bsp.Utility.DisplayType.OnlyName)
            ddlCCountry.Items.Insert(0, New ListItem("---請選擇---", ""))
            ddlCCountry.Items.Add(New ListItem("其他", "其他"))

            Bsp.Utility.FillDDL(ddlMCountry, "eHRMSDB", "CountryCode", "CodeNo", "CountryName", Bsp.Utility.DisplayType.OnlyName)
            ddlMCountry.Items.Insert(0, New ListItem("---請選擇---", ""))
            ddlMCountry.Items.Add(New ListItem("其他", "其他"))

            Bsp.Utility.FillDDL(ddlMCountry2, "eHRMSDB", "CountryCode", "CodeNo", "CountryName", Bsp.Utility.DisplayType.OnlyName)
            ddlMCountry2.Items.Insert(0, New ListItem("---請選擇---", ""))
            ddlMCountry2.Items.Add(New ListItem("其他", "其他"))

            Bsp.Utility.FillDDL(ddlRegCityCode, "eHRMSDB", "PostalCode", "distinct CityCode", "LEFT(AddrCode, 1)", Bsp.Utility.DisplayType.OnlyID, "", "AND AreaCode <> ''", "ORDER BY LEFT(AddrCode, 1)")
            ddlRegCityCode.Items.Insert(0, New ListItem("縣市別", ""))

            Bsp.Utility.FillDDL(ddlCommCityCode, "eHRMSDB", "PostalCode", "distinct CityCode", "LEFT(AddrCode, 1)", Bsp.Utility.DisplayType.OnlyID, "", "AND AreaCode <> ''", "ORDER BY LEFT(AddrCode, 1)")
            ddlCommCityCode.Items.Insert(0, New ListItem("縣市別", ""))

            Bsp.Utility.FillDDL(ddlCompCountry, "eHRMSDB", "CountryCode", "CodeNo", "CountryName", Bsp.Utility.DisplayType.OnlyName)
            ddlCompCountry.Items.Insert(0, New ListItem("---請選擇---", ""))
            ddlCompCountry.Items.Add(New ListItem("其他", "其他"))

            Bsp.Utility.FillRelativeID(ddlRelRelation)
            ddlRelRelation.Items.Insert(0, New ListItem("---請選擇---", ""))
        End If
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

            If ht.ContainsKey("SelectedIDNo") Then
                ViewState.Item("CompID") = ht("SelectedCompID").ToString()
                ViewState.Item("EmpID") = ht("SelectedEmpID").ToString()
                subGetData(
                    ht("SelectedCompID").ToString(), _
                    ht("SelectedEmpID").ToString())
            Else
                Return
            End If
        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnUpdate"       '存檔
                If funCheckData() Then
                    If SaveData() Then
                        Bsp.Utility.ShowFormatMessage(Me, "I_00000")
                        subGetData(ViewState.Item("CompID"), ViewState.Item("EmpID"))
                    End If
                End If
            Case "btnActionX"   '清除
                DoClear()
        End Select
    End Sub

    Private Sub subGetData(ByVal CompID As String, ByVal EmpID As String)
        Dim objST1 As New ST1
        Dim strValue As String = ""
        Try
            Using dt As DataTable = objST1.QueryCommunicationSetting(CompID, EmpID)
                If dt.Rows.Count <= 0 Then Exit Sub

                txtCompID.Text = dt.Rows(0).Item("CompID")
                txtEmpID.Text = EmpID
                txtName.Text = dt.Rows(0).Item("NameN")
                ViewState.Item("IDNo") = dt.Rows(0).Item("IDNo")
                ViewState.Item("RegAddrEmpty") = False
                ViewState.Item("CommAddrEmpty") = False

                '戶籍電話
                Bsp.Utility.SetSelectedIndex(ddlRCountry, dt.Rows(0).Item("CommTelCode1"))
                txtRCountryCode.Text = dt.Rows(0).Item("CommTelCode1")

                strValue = dt.Rows(0).Item("CommTel1")
                If strValue.IndexOf("-") >= 0 Then
                    txtRAreaCode.Text = strValue.Substring(0, strValue.IndexOf("-"))
                    txtRPhone.Text = strValue.Substring(strValue.IndexOf("-") + 1)
                Else
                    txtRPhone.Text = strValue
                End If

                '通訊電話
                Bsp.Utility.SetSelectedIndex(ddlCCountry, dt.Rows(0).Item("CommTelCode2"))
                txtCCountryCode.Text = dt.Rows(0).Item("CommTelCode2")

                strValue = dt.Rows(0).Item("CommTel2")
                If strValue.IndexOf("-") >= 0 Then
                    txtCAreaCode.Text = strValue.Substring(0, strValue.IndexOf("-"))
                    txtCPhone.Text = strValue.Substring(strValue.IndexOf("-") + 1)
                Else
                    txtCPhone.Text = strValue
                End If

                '行動電話1
                If dt.Rows(0).Item("CommTelCode3") = "" Then
                    Bsp.Utility.SetSelectedIndex(ddlMCountry, "886")
                    txtMCountryCode.Text = "886"
                Else
                    Bsp.Utility.SetSelectedIndex(ddlMCountry, dt.Rows(0).Item("CommTelCode3"))
                    txtMCountryCode.Text = dt.Rows(0).Item("CommTelCode3")
                End If

                If dt.Rows(0).Item("CommTelCode3") <> "" And ddlMCountry.SelectedValue = "" Then
                    ddlMCountry.SelectedValue = "其他"
                End If

                txtMPhone.Text = dt.Rows(0).Item("CommTel3")

                '行動電話2
                Bsp.Utility.SetSelectedIndex(ddlMCountry2, dt.Rows(0).Item("CommTelCode4"))
                txtMCountry2Code.Text = dt.Rows(0).Item("CommTelCode4")

                If dt.Rows(0).Item("CommTelCode4") <> "" And ddlMCountry2.SelectedValue = "" Then
                    ddlMCountry2.SelectedValue = "其他"
                End If

                txtMPhone2.Text = dt.Rows(0).Item("CommTel4")

                '戶籍地址
                Bsp.Utility.SetSelectedIndex(ddlRegCityCode, dt.Rows(0).Item("RegCityCode").ToString.Trim)
                Bsp.Utility.FillDDL(ddlRegAddrCode, "eHRMSDB", "PostalCode", "AddrCode", "AreaCode", Bsp.Utility.DisplayType.Full, "", "AND CityCode = " & Bsp.Utility.Quote(dt.Rows(0).Item("RegCityCode")))
                ddlRegAddrCode.Items.Insert(0, New ListItem("區別", ""))
                Bsp.Utility.SetSelectedIndex(ddlRegAddrCode, dt.Rows(0).Item("RegAddrCode").ToString.Trim)
                txtRegAddr.Text = dt.Rows(0).Item("RegAddr")
                If txtRegAddr.Text.Trim = "" Then
                    ViewState.Item("RegAddrEmpty") = True
                End If

                '通訊地址
                Bsp.Utility.SetSelectedIndex(ddlCommCityCode, dt.Rows(0).Item("CommCityCode").ToString.Trim)
                Bsp.Utility.FillDDL(ddlCommAddrCode, "eHRMSDB", "PostalCode", "AddrCode", "AreaCode", Bsp.Utility.DisplayType.Full, "", "AND CityCode = " & Bsp.Utility.Quote(dt.Rows(0).Item("CommCityCode")))
                ddlCommAddrCode.Items.Insert(0, New ListItem("區別", ""))
                Bsp.Utility.SetSelectedIndex(ddlCommAddrCode, dt.Rows(0).Item("CommAddrCode").ToString.Trim)
                txtCommAddr.Text = dt.Rows(0).Item("CommAddr")
                If txtCommAddr.Text.Trim = "" Then
                    ViewState.Item("CommAddrEmpty") = True
                End If

                '緊急連絡人
                txtRelName.Text = dt.Rows(0).Item("RelName")
                Bsp.Utility.SetSelectedIndex(ddlRelRelation, dt.Rows(0).Item("RelRelation").ToString.Trim)
                txtRelTel.Text = dt.Rows(0).Item("RelTel")
                txtEmail.Text = dt.Rows(0).Item("EMail")
                txtEmail2.Text = dt.Rows(0).Item("Email2")

                '公司電話
                If dt.Rows(0).Item("CompTelCode") = "" Then
                    Bsp.Utility.SetSelectedIndex(ddlCompCountry, "886")
                    txtCompCountryCode.Text = "886"
                Else
                    Bsp.Utility.SetSelectedIndex(ddlCompCountry, dt.Rows(0).Item("CompTelCode"))
                    txtCompCountryCode.Text = dt.Rows(0).Item("CompTelCode")
                End If

                txtCompAreaCode.Text = dt.Rows(0).Item("AreaCode")
                txtCompPhone.Text = dt.Rows(0).Item("CompTel")
                txtCompExt.Text = dt.Rows(0).Item("ExtNo")

                txtLastChgComp.Text = dt.Rows(0).Item("LastChgComp")
                txtLastChgID.Text = dt.Rows(0).Item("LastChgID")
                txtLastChgDate.Text = dt.Rows(0).Item("LastChgDate")
            End Using

            EnableField()

        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & "subGetData", ex)
        End Try

    End Sub

    Private Sub EnableField()
        '戶籍電話
        If ddlRCountry.SelectedValue <> "" And ddlRCountry.SelectedValue <> "其他" Then
            txtRCountryCode.Enabled = False
        End If

        '通訊電話
        If ddlCCountry.SelectedValue <> "" And ddlCCountry.SelectedValue <> "其他" Then
            txtCCountryCode.Enabled = False
        End If


        '行動電話1
        If ddlMCountry.SelectedValue <> "" And ddlMCountry.SelectedValue <> "其他" Then
            txtMCountryCode.Enabled = False
        End If

        '行動電話2
        If ddlMCountry2.SelectedValue <> "" And ddlMCountry2.SelectedValue <> "其他" Then
            txtMCountry2Code.Enabled = False
        End If

        '公司電話
        If ddlCompCountry.SelectedValue <> "" And ddlCompCountry.SelectedValue <> "其他" Then
            txtCompCountryCode.Enabled = False
        End If
    End Sub

    Private Function funCheckData() As Boolean
        Dim objST1 As New ST1
        Dim regInteger As String = "^[0-9]+$"
        Dim strValue As String = ""

        '戶籍電話
        If Not (txtRCountryCode.Text.Trim = "" And txtRPhone.Text.Trim = "") And Not (txtRCountryCode.Text.Trim <> "" And txtRPhone.Text.Trim <> "") Then
            Bsp.Utility.ShowFormatMessage(Me, "H_00000", "戶籍電話-國碼、電話需同時輸入或同時不輸入")
            txtRCountryCode.Focus()
            Return False
        End If

        '戶籍電話-國碼
        If txtRCountryCode.Text.Trim <> "" And Not Regex.IsMatch(txtRCountryCode.Text, regInteger) Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00050", "戶籍電話-國碼")  '2015/12/02 Micky Modify 增加錯誤訊息
            txtRCountryCode.Focus()
            Return False
        End If

        '戶籍電話-區碼
        If txtRAreaCode.Text.Trim <> "" And Not Regex.IsMatch(txtRAreaCode.Text, regInteger) Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00050", "戶籍電話-區碼")  '2015/12/02 Micky Modify 增加錯誤訊息
            txtRAreaCode.Focus()
            Return False
        End If

        '戶籍電話-電話
        If txtRPhone.Text.Trim <> "" And Not Regex.IsMatch(txtRPhone.Text, regInteger) Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00050", "戶籍電話-電話")  '2015/12/02 Micky Modify 增加錯誤訊息
            txtRPhone.Focus()
            Return False
        End If

        If ddlRCountry.SelectedValue <> "" And ddlRCountry.SelectedValue <> "其他" Then
            If objST1.IsCountryCodeExist(ddlRCountry.SelectedValue) Then
                If objST1.IsAreaCodeExist(ddlRCountry.SelectedValue, txtRAreaCode.Text) Then
                    Dim strLength As String = objST1.GetTelLength(ddlRCountry.SelectedValue, txtRAreaCode.Text)
                    If strLength <> "" Then
                        Dim arrLength() As String = strLength.Split("/")
                        Dim ChkLength As Boolean = False

                        For Each Length In arrLength
                            If txtRPhone.Text.Length = Length Then
                                ChkLength = True
                                Exit For
                            End If
                        Next

                        If Not ChkLength Then
                            Bsp.Utility.ShowFormatMessage(Me, "H_00000", "戶籍電話-國碼：" & ddlRCountry.SelectedValue & " 區碼：" & txtRAreaCode.Text & "，電話長度應為：" & strLength & "， 請重新輸入")
                            txtRAreaCode.Focus()
                            Return False
                        End If
                    End If
                Else
                    If txtRAreaCode.Text.Trim = "" Then
                        Bsp.Utility.ShowFormatMessage(Me, "H_00000", "戶籍電話-國碼：" & ddlRCountry.SelectedValue & " 區碼不得空白")
                        txtRAreaCode.Focus()
                        Return False
                    End If
                    Bsp.Utility.ShowFormatMessage(Me, "H_00000", "戶籍電話-國碼：" & ddlRCountry.SelectedValue & " 查無區碼：" & txtRAreaCode.Text & "，請檢查區碼有無輸入錯誤")
                    txtRAreaCode.Focus()
                    Return False
                End If
            End If
        End If

        strValue = txtRAreaCode.Text + IIf(txtRAreaCode.Text <> "", "-", "") + txtRPhone.Text
        If strValue.Length > 20 Then
            Bsp.Utility.ShowFormatMessage(Me, "H_00000", "戶籍電話-區碼+電話長度不可超過20")
            txtRPhone.Focus()
            Return False
        End If

        '通訊電話
        If Not (txtCCountryCode.Text.Trim = "" And txtCPhone.Text.Trim = "") And Not (txtCCountryCode.Text.Trim <> "" And txtCPhone.Text.Trim <> "") Then
            Bsp.Utility.ShowFormatMessage(Me, "H_00000", "通訊電話-國碼、電話需同時輸入或同時不輸入")
            txtCCountryCode.Focus()
            Return False
        End If

        '通訊電話-國碼
        If txtCCountryCode.Text.Trim <> "" And Not Regex.IsMatch(txtCCountryCode.Text, regInteger) Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00050", "通訊電話-國碼")  '2015/12/02 Micky Modify 增加錯誤訊息
            txtCCountryCode.Focus()
            Return False
        End If

        '通訊電話-區碼
        If txtCAreaCode.Text.Trim <> "" And Not Regex.IsMatch(txtCAreaCode.Text, regInteger) Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00050", "通訊電話-區碼")  '2015/12/02 Micky Modify 增加錯誤訊息
            txtCAreaCode.Focus()
            Return False
        End If

        '通訊電話-電話
        If txtCPhone.Text.Trim <> "" And Not Regex.IsMatch(txtCPhone.Text, regInteger) Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00050", "通訊電話-電話")  '2015/12/02 Micky Modify 增加錯誤訊息
            txtCPhone.Focus()
            Return False
        End If

        If ddlCCountry.SelectedValue <> "" And ddlCCountry.SelectedValue <> "其他" Then
            If objST1.IsCountryCodeExist(ddlCCountry.SelectedValue) Then
                If objST1.IsAreaCodeExist(ddlCCountry.SelectedValue, txtCAreaCode.Text) Then
                    Dim strLength As String = objST1.GetTelLength(ddlCCountry.SelectedValue, txtCAreaCode.Text)
                    If strLength <> "" Then
                        Dim arrLength() As String = strLength.Split("/")
                        Dim ChkLength As Boolean = False

                        For Each Length In arrLength
                            If txtCPhone.Text.Length = Length Then
                                ChkLength = True
                                Exit For
                            End If
                        Next

                        If Not ChkLength Then
                            Bsp.Utility.ShowFormatMessage(Me, "H_00000", "通訊電話-國碼：" & ddlCCountry.SelectedValue & " 區碼：" & txtCAreaCode.Text & "，電話長度應為：" & strLength & "， 請重新輸入")
                            txtCAreaCode.Focus()
                            Return False
                        End If
                    End If
                Else
                    If txtCAreaCode.Text.Trim = "" Then
                        Bsp.Utility.ShowFormatMessage(Me, "H_00000", "通訊電話-國碼：" & ddlCCountry.SelectedValue & " 區碼不得空白")
                        txtCAreaCode.Focus()
                        Return False
                    End If
                    Bsp.Utility.ShowFormatMessage(Me, "H_00000", "通訊電話-國碼：" & ddlCCountry.SelectedValue & " 查無區碼：" & txtCAreaCode.Text & "，請檢查區碼有無輸入錯誤")
                    txtCAreaCode.Focus()
                    Return False
                End If
            End If
        End If

        strValue = txtCAreaCode.Text + IIf(txtCAreaCode.Text <> "", "-", "") + txtCPhone.Text
        If strValue.Length > 20 Then
            Bsp.Utility.ShowFormatMessage(Me, "H_00000", "通訊電話-區碼+電話長度不可超過20")
            txtCPhone.Focus()
            Return False
        End If

        '行動電話1-國別
        If txtMCountryCode.Text.Trim = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "行動電話1-國別")  '2015/12/02 Micky Modify 增加錯誤訊息
            txtMCountryCode.Focus()
            Return False
        End If

        If Not Regex.IsMatch(txtMCountryCode.Text, regInteger) Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00050", "行動電話1-國別")  '2015/12/02 Micky Modify 增加錯誤訊息
            txtMCountryCode.Focus()
            Return False
        End If

        '行動電話1-電話
        If txtMPhone.Text.Trim = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "行動電話1-電話")  '2015/12/02 Micky Modify 增加錯誤訊息
            txtMPhone.Focus()
            Return False
        End If

        If Not Regex.IsMatch(txtMPhone.Text, regInteger) Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00050", "行動電話1-電話")  '2015/12/02 Micky Modify 增加錯誤訊息
            txtMPhone.Focus()
            Return False
        End If

        If txtMCountryCode.Text = "886" And Not txtMPhone.Text.StartsWith("0") Then
            Bsp.Utility.ShowFormatMessage(Me, "H_00000", "行動電話1-電話" & "第一碼需為0")  '2015/12/02 Micky Modify 增加錯誤訊息
            txtMPhone.Focus()
            Return False
        End If

        If txtMCountryCode.Text = "886" And txtMPhone.Text.Length <> 10 Then
            Bsp.Utility.ShowFormatMessage(Me, "H_00000", "行動電話1-電話" & "長度須為10碼")  '2015/12/02 Micky Modify 增加錯誤訊息
            txtMPhone.Focus()
            Return False
        End If

        '行動電話2
        If Not (txtMCountry2Code.Text.Trim = "" And txtMPhone2.Text.Trim = "") And Not (txtMCountry2Code.Text.Trim <> "" And txtMPhone2.Text.Trim <> "") Then
            Bsp.Utility.ShowFormatMessage(Me, "H_00000", "行動電話2-國碼、電話需同時輸入或同時不輸入")
            txtMCountry2Code.Focus()
            Return False

        ElseIf txtMCountry2Code.Text.Trim <> "" And txtMPhone2.Text.Trim <> "" Then
            '行動電話2-國別
            If Not Regex.IsMatch(txtMCountry2Code.Text, regInteger) Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00050", "行動電話2-國別")  '2015/12/02 Micky Modify 增加錯誤訊息
                txtMCountry2Code.Focus()
                Return False
            End If

            '行動電話2-電話
            If Not Regex.IsMatch(txtMPhone2.Text, regInteger) Then
                Bsp.Utility.ShowFormatMessage(Me, "W_00050", "行動電話2-國別")  '2015/12/02 Micky Modify 增加錯誤訊息
                txtMPhone2.Focus()
                Return False
            End If

            If txtMCountry2Code.Text = "886" And Not txtMPhone2.Text.StartsWith("0") Then
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", "行動電話2-國別" & "第一碼需為0")  '2015/12/02 Micky Modify 增加錯誤訊息
                txtMPhone2.Focus()
                Return False
            End If

            If txtMCountry2Code.Text = "886" And txtMPhone2.Text.Length <> 10 Then
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", "行動電話2-國別" & "長度須為10碼")  '2015/12/02 Micky Modify 增加錯誤訊息
                txtMPhone2.Focus()
                Return False
            End If
        End If

        '戶籍地址
        If ddlRegCityCode.SelectedValue.Trim <> "" And ddlRegAddrCode.SelectedValue.Trim = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "H_00000", "戶籍地址-如果縣市別有挑選，區別也需挑選")
            ddlRegAddrCode.Focus()
            Return False
        End If

        'If Not (ddlRegCityCode.SelectedValue.Trim = "" And txtRegAddr.Text.Trim = "") And Not (ddlRegCityCode.SelectedValue.Trim <> "" And txtRegAddr.Text.Trim <> "") Then
        If ddlRegCityCode.SelectedValue.Trim <> "" Or ddlRegAddrCode.SelectedValue.Trim <> "" Then
            If txtRegAddr.Text.Trim = "" Then
                'Bsp.Utility.ShowFormatMessage(Me, "H_00000", "戶籍地址-縣市別、地址需同時輸入或同時不輸入")
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", "戶籍地址-如果縣市別或區別有資料，戶籍地址不可空白")
                ddlRegCityCode.Focus()
                Return False
            End If
        End If

        '通訊地址
        If ddlCommCityCode.SelectedValue.Trim <> "" And ddlCommAddrCode.SelectedValue.Trim = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "H_00000", "通訊地址-如果縣市別有挑選，區別也需挑選")
            ddlCommAddrCode.Focus()
            Return False
        End If

        'If Not (ddlCommCityCode.SelectedValue.Trim = "" And txtCommAddr.Text.Trim = "") And Not (ddlCommCityCode.SelectedValue.Trim <> "" And txtCommAddr.Text.Trim <> "") Then
        If ddlCommCityCode.SelectedValue.Trim <> "" Or ddlCommAddrCode.SelectedValue.Trim <> "" Then
            If txtCommAddr.Text.Trim = "" Then
                'Bsp.Utility.ShowFormatMessage(Me, "H_00000", "通訊地址-縣市別、地址需同時輸入或同時不輸入")
                Bsp.Utility.ShowFormatMessage(Me, "H_00000", "通訊地址-如果縣市別或區別有資料，通訊地址不可空白")
                ddlCommCityCode.Focus()
                Return False
            End If
        End If

        '緊急聯絡人電話
        If txtRelTel.Text.Trim <> "" And Not Regex.IsMatch(txtRelTel.Text, regInteger) Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00050", lblRegTel.Text)  '2015/12/02 Micky Modify 增加錯誤訊息
            txtRelTel.Focus()
            Return False
        End If

        '公司電話-國碼
        If txtCompCountryCode.Text.Trim = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "公司電話-國碼")  '2015/12/02 Micky Modify 增加錯誤訊息
            txtCompCountryCode.Focus()
            Return False
        End If

        If Not Regex.IsMatch(txtCompCountryCode.Text, regInteger) Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00050", "公司電話-國碼")  '2015/12/02 Micky Modify 增加錯誤訊息
            txtCompCountryCode.Focus()
            Return False
        End If

        '公司電話-區碼
        If txtCompAreaCode.Text.Trim <> "" And Not Regex.IsMatch(txtCompAreaCode.Text, regInteger) Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00050", "公司電話-區碼")  '2015/12/02 Micky Modify 增加錯誤訊息
            txtCompAreaCode.Focus()
            Return False
        End If

        '公司電話-電話
        If txtCompPhone.Text.Trim = "" Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00030", "公司電話-電話")  '2015/12/02 Micky Modify 增加錯誤訊息
            txtCompPhone.Focus()
            Return False
        End If

        If Not Regex.IsMatch(txtCompPhone.Text, regInteger) Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00050", "公司電話-電話")  '2015/12/02 Micky Modify 增加錯誤訊息
            txtCompPhone.Focus()
            Return False
        End If

        If ddlCompCountry.SelectedValue <> "" And ddlCompCountry.SelectedValue <> "其他" Then
            If objST1.IsCountryCodeExist(ddlCompCountry.SelectedValue) Then
                If objST1.IsAreaCodeExist(ddlCompCountry.SelectedValue, txtCompAreaCode.Text) Then
                    Dim strLength As String = objST1.GetTelLength(ddlCompCountry.SelectedValue, txtCompAreaCode.Text)
                    If strLength <> "" Then
                        Dim arrLength() As String = strLength.Split("/")
                        Dim ChkLength As Boolean = False

                        For Each Length In arrLength
                            If txtCompPhone.Text.Length = Length Then
                                ChkLength = True
                                Exit For
                            End If
                        Next

                        If Not ChkLength Then
                            Bsp.Utility.ShowFormatMessage(Me, "H_00000", "公司電話-國碼：" & ddlCompCountry.SelectedValue & " 區碼：" & txtCompAreaCode.Text & "，電話長度應為：" & strLength & "， 請重新輸入")
                            txtCompAreaCode.Focus()
                            Return False
                        End If
                    End If
                Else
                    If txtCompAreaCode.Text.Trim = "" Then
                        Bsp.Utility.ShowFormatMessage(Me, "H_00000", "公司電話-國碼：" & ddlCompCountry.SelectedValue & " 區碼不得空白")
                        txtCompAreaCode.Focus()
                        Return False
                    End If
                    Bsp.Utility.ShowFormatMessage(Me, "H_00000", "公司電話-國碼：" & ddlCompCountry.SelectedValue & " 查無區碼：" & txtCompAreaCode.Text & "，請檢查區碼有無輸入錯誤")
                    txtCompAreaCode.Focus()
                    Return False
                End If
            End If
        End If

        '公司電話-分機
        If txtCompExt.Text.Trim <> "" And Not Regex.IsMatch(txtCompExt.Text, regInteger) Then
            Bsp.Utility.ShowFormatMessage(Me, "W_00050", "公司電話-分機")  '2015/12/02 Micky Modify 增加錯誤訊息
            txtCompExt.Focus()
            Return False
        End If

        Return True
    End Function

    Private Function SaveData() As Boolean
        Dim beCommunication As New beCommunication.Row()
        Dim bsCommunication As New beCommunication.Service()
        Dim objST1 As New ST1

        '取得輸入資料
        beCommunication.IDNo.Value = ViewState.Item("IDNo")

        If txtRCountryCode.Text <> "" Then
            beCommunication.CommTelCode1.Value = txtRCountryCode.Text
            beCommunication.CommTel1.Value = txtRAreaCode.Text + IIf(txtRAreaCode.Text <> "", "-", "") + txtRPhone.Text
        ElseIf txtRCountryCode.Text = "" And txtRPhone.Text = "" Then
            beCommunication.CommTelCode1.Value = ""
            beCommunication.CommTel1.Value = ""
        End If

        If txtCCountryCode.Text <> "" Then
            beCommunication.CommTelCode2.Value = txtCCountryCode.Text
            beCommunication.CommTel2.Value = txtCAreaCode.Text + IIf(txtCAreaCode.Text <> "", "-", "") + txtCPhone.Text
        ElseIf txtCCountryCode.Text = "" And txtCPhone.Text = "" Then
            beCommunication.CommTelCode2.Value = ""
            beCommunication.CommTel2.Value = ""
        End If

        beCommunication.CommTelCode3.Value = txtMCountryCode.Text
        beCommunication.CommTel3.Value = txtMPhone.Text
        beCommunication.CommTelCode4.Value = txtMCountry2Code.Text
        beCommunication.CommTel4.Value = txtMPhone2.Text

        beCommunication.RegCityCode.Value = ddlRegCityCode.SelectedValue
        beCommunication.RegAddrCode.Value = ddlRegAddrCode.SelectedValue
        beCommunication.RegAddr.Value = txtRegAddr.Text

        If cbSameReg.Checked = True Then
            beCommunication.CommCityCode.Value = ddlRegCityCode.SelectedValue
            beCommunication.CommAddrCode.Value = ddlRegAddrCode.SelectedValue
            beCommunication.CommAddr.Value = txtRegAddr.Text
        Else
            beCommunication.CommCityCode.Value = ddlCommCityCode.SelectedValue
            beCommunication.CommAddrCode.Value = ddlCommAddrCode.SelectedValue
            beCommunication.CommAddr.Value = txtCommAddr.Text
        End If

        beCommunication.RelName.Value = txtRelName.Text
        beCommunication.RelRelation.Value = ddlRelRelation.SelectedValue
        beCommunication.RelTel.Value = txtRelTel.Text

        beCommunication.EMail.Value = txtEmail.Text
        beCommunication.Email2.Value = txtEmail2.Text

        beCommunication.CompTelCode.Value = txtCompCountryCode.Text
        beCommunication.AreaCode.Value = txtCompAreaCode.Text
        beCommunication.CompTel.Value = txtCompPhone.Text
        beCommunication.ExtNo.Value = txtCompExt.Text

        beCommunication.LastChgComp.Value = UserProfile.ActCompID
        beCommunication.LastChgID.Value = UserProfile.ActUserID
        beCommunication.LastChgDate.Value = Now

        '儲存資料
        Try
            Return objST1.UpdateCommunicationSetting(beCommunication)
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, "[CC0201_99]" & Bsp.Utility.getMessage("E_00000") & Bsp.Utility.getInnerException(Me.FunID, ex))
            Return False
        End Try
    End Function

    Private Sub DoClear()
        subGetData(ViewState.Item("CompID"), ViewState.Item("EmpID"))
    End Sub

    Protected Sub ddlCountry_SelectedChanged(sender As Object, e As System.EventArgs)
        Dim objST1 As New ST1
        Dim ddlCountry As DropDownList = CType(sender, DropDownList)
        Dim txtCountryCode As TextBox = CType(Me.FindControl(ddlCountry.ID.Replace("ddl", "txt") & "Code"), TextBox)

        If ddlCountry.SelectedValue <> "" And ddlCountry.SelectedValue <> "其他" Then
            txtCountryCode.Text = ddlCountry.SelectedValue
            txtCountryCode.Enabled = False

            If ddlCountry.ID = "ddlRCountry" Or ddlCountry.ID = "ddlCCountry" Or ddlCountry.ID = "ddlCompCountry" Then
                Dim txtAreaCode As TextBox = CType(Me.FindControl(ddlCountry.ID.Replace("ddl", "txt").Replace("Country", "AreaCode")), TextBox)

                If objST1.IsAreaCodeEmpty(ddlCountry.SelectedValue) Then
                    txtAreaCode.Text = ""
                    txtAreaCode.Enabled = False
                Else
                    txtAreaCode.Enabled = True
                End If
            End If
        Else
            txtCountryCode.Text = ""
            txtCountryCode.Enabled = True
        End If
    End Sub

    Protected Sub ddlCityCode_SelectedChanged(sender As Object, e As System.EventArgs)
        Dim objST1 As New ST1
        Dim ddlCityCode As DropDownList = CType(sender, DropDownList)
        Dim ddlAddrCode As DropDownList = CType(Me.FindControl(ddlCityCode.ID.Replace("City", "Addr")), DropDownList)

        Bsp.Utility.FillDDL(ddlAddrCode, "eHRMSDB", "PostalCode", "AddrCode", "AreaCode", Bsp.Utility.DisplayType.Full, "", "AND CityCode = " & Bsp.Utility.Quote(ddlCityCode.SelectedValue))
        ddlAddrCode.Items.Insert(0, New ListItem("區別", ""))

        If ddlCityCode.ID = "ddlRegCityCode" And ViewState.Item("RegAddrEmpty") = True Then
            txtRegAddr.Text += ddlCityCode.SelectedItem.Text
        End If

        If ddlCityCode.ID = "ddlCommCityCode" And ViewState.Item("CommAddrEmpty") = True Then
            txtCommAddr.Text += ddlCityCode.SelectedItem.Text
        End If
    End Sub

    Protected Sub ddlAddrCode_SelectedChanged(sender As Object, e As System.EventArgs)
        Dim objST1 As New ST1
        Dim ddlAddrCode As DropDownList = CType(sender, DropDownList)

        If ddlAddrCode.ID = "ddlRegAddrCode" And ViewState.Item("RegAddrEmpty") = True Then
            txtRegAddr.Text += objST1.QueryData("PostalCode", "AND CityCode = " & Bsp.Utility.Quote(ddlRegCityCode.SelectedValue) & "AND AddrCode = " & Bsp.Utility.Quote(ddlRegAddrCode.SelectedValue), "AreaCode").Replace(" ", "")
        End If

        If ddlAddrCode.ID = "ddlCommAddrCode" And ViewState.Item("CommAddrEmpty") = True Then
            txtCommAddr.Text += objST1.QueryData("PostalCode", "AND CityCode = " & Bsp.Utility.Quote(ddlCommCityCode.SelectedValue) & "AND AddrCode = " & Bsp.Utility.Quote(ddlCommAddrCode.SelectedValue), "AreaCode").Replace(" ", "")
        End If
    End Sub

    Protected Sub cbSameReg_CheckedChanged(sender As Object, e As System.EventArgs)
        If cbSameReg.Checked = True Then
            ddlCommCityCode.SelectedIndex = ddlRegCityCode.SelectedIndex

            Bsp.Utility.FillDDL(ddlCommAddrCode, "eHRMSDB", "PostalCode", "AddrCode", "AreaCode", Bsp.Utility.DisplayType.Full, "", "AND CityCode = " & Bsp.Utility.Quote(ddlCommCityCode.SelectedValue))
            ddlCommAddrCode.Items.Insert(0, New ListItem("區別", ""))

            ddlCommAddrCode.SelectedIndex = ddlRegAddrCode.SelectedIndex
            txtCommAddr.Text = txtRegAddr.Text

            ddlCommCityCode.Enabled = False
            ddlCommAddrCode.Enabled = False
            txtCommAddr.Enabled = False
        Else
            ddlCommCityCode.Enabled = True
            ddlCommAddrCode.Enabled = True
            txtCommAddr.Enabled = True
        End If
    End Sub
End Class
