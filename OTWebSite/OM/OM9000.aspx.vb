Imports System.Data
Imports System.Data.Common
Imports Newtonsoft.Json

Partial Class OM_OM9000
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txtCompID.Text = UserProfile.SelectCompRoleName

            ViewState.Item("OrgTypeColors") = New List(Of ArrayList)()
            ViewState.Item("DeptColors") = New List(Of ArrayList)()

            subLoadOrganColor(ddlOrgType, UserProfile.SelectCompRoleID)
            subLoadOrganColor(ddlDeptID, UserProfile.SelectCompRoleID)

            '科組課
            ddlDept_Changed(Nothing, Nothing)

            DoQuery()
        Else
            subReLoadColor(ddlOrgType)
            subReLoadColor(ddlDeptID)
        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)

        Select Case Param
            Case "btnQuery"
                DoQuery()
        End Select
    End Sub

    Private Sub DoQuery()
        Try
            Bsp.Utility.RunClientScript(Me, "LoadData(" & Bsp.Utility.Quote(ddlOrgType.SelectedValue) & ", " & Bsp.Utility.Quote(ddlDeptID.SelectedValue) & ", " & Bsp.Utility.Quote(ddlOrganID.SelectedValue) & ")")
        Catch ex As Exception
            Bsp.Utility.ShowMessage(Me, Me.FunID & ".DoQuery", ex)
        End Try
    End Sub

    Protected Sub btnDetail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDetail.Click
        Me.TransferFramePage("~/ST/ST1000.aspx", New ButtonState() {}, "OrganID=" & hldOrganID.Value)
    End Sub

#Region "下拉選單"
    Private Sub subLoadOrganColor(ByVal objDDL As DropDownList, ByVal strCompID As String)
        Dim strSQL As New StringBuilder
        strSQL.AppendLine("Select O.OrganID")
        'strSQL.AppendLine(", Case When OrgType = DeptID Then OrganID + '-' + OrganName + '-' + RoleCode Else '　　' + OrganID + '-' + OrganName + '-' + RoleCode End OrganName")
        strSQL.AppendLine(", Case When OrgType = DeptID Then OrganID + '-' + OrganName Else '　　' + OrganID + '-' + OrganName End OrganName")
        strSQL.AppendLine(", IsNull(W.Color, '#FFFFFF') Color")
        strSQL.AppendLine("From Organization O")
        strSQL.AppendLine("Left Join OrganColor_Web W On O.CompID = W.CompID and O.SortOrder = W.SortOrder")
        If objDDL.ID = "ddlOrgType" Then
            strSQL.AppendLine("Where OrganID = OrgType")
        Else
            strSQL.AppendLine("Where OrganID = DeptID")
        End If
        strSQL.AppendLine("And O.CompID = " & Bsp.Utility.Quote(strCompID))
        strSQL.AppendLine("And VirtualFlag = '0'")
        strSQL.AppendLine("And InValidFlag = '0'")
        strSQL.AppendLine("Order By O.InValidFlag, O.SortOrder, Right(O.GroupType, 1), O.GroupID, O.OrganID")

        Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
            objDDL.Items.Clear()
            Dim ArrColors As New List(Of ArrayList)()

            If dt.Rows.Count > 0 Then
                For Each item As DataRow In dt.Rows
                    Dim ListOpt As ListItem = New ListItem()
                    ListOpt.Value = item("OrganID").ToString()
                    ListOpt.Text = item("OrganName").ToString()

                    If item("Color").ToString().Trim() <> "#FFFFFF" Then
                        ListOpt.Attributes.Add("style", "background-color:" + item("Color").ToString().Trim())

                        Dim ArrColor As New ArrayList()
                        ArrColor.Add(item("OrganID").ToString())
                        ArrColor.Add(item("Color").ToString().Trim())
                        ArrColors.Add(ArrColor)
                    End If

                    objDDL.Items.Add(ListOpt)
                Next
            End If

            objDDL.Items.Insert(0, New ListItem("---請選擇---", ""))

            Select Case objDDL.ID
                Case "ddlOrgType"
                    ViewState.Item("OrgTypeColors") = ArrColors
                Case "ddlDeptID"
                    ViewState.Item("DeptColors") = ArrColors
            End Select
        End Using
    End Sub

    Private Sub subReLoadColor(ByVal objDDL As DropDownList)
        If objDDL.Items.Count > 0 Then
            Dim ArrColors As New List(Of ArrayList)()

            Select Case objDDL.ID
                Case "ddlOrgType"
                    ArrColors = ViewState.Item("OrgTypeColors")
                Case "ddlDeptID"
                    ArrColors = ViewState.Item("DeptColors")
            End Select

            For Each item As ArrayList In ArrColors
                Dim list As ListItem = objDDL.Items.FindByValue(item(0))
                If Not list Is Nothing Then
                    list.Attributes.Add("style", "background-color:" + item(1))
                End If
            Next
        End If
    End Sub

    Protected Sub ddlDept_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDeptID.SelectedIndexChanged
        If ddlDeptID.SelectedValue = "" Then
            Bsp.Utility.FillDDL(ddlOrganID, "eHRMSDB", "Organization", "RTRIM(OrganID)", "OrganName + '-' + RoleCode", Bsp.Utility.DisplayType.Full, "", "And InValidFlag = '0' And VirtualFlag = '0' And CompID = " & Bsp.Utility.Quote(UserProfile.SelectCompRoleID) & " And OrganID <> DeptID", "Order By OrganID")
            ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
        Else
            Bsp.Utility.FillDDL(ddlOrganID, "eHRMSDB", "Organization", "RTRIM(OrganID)", "OrganName + '-' + RoleCode", Bsp.Utility.DisplayType.Full, "", "And InValidFlag = '0' And VirtualFlag = '0' And CompID = " & Bsp.Utility.Quote(UserProfile.SelectCompRoleID) & " And OrganID <> DeptID And DeptID = " & Bsp.Utility.Quote(ddlDeptID.SelectedValue), "Order By OrganID")
            ddlOrganID.Items.Insert(0, New ListItem("---請選擇---", ""))
        End If
    End Sub
#End Region

#Region "查詢"
    <System.Web.Services.WebMethod()> _
    Public Shared Function SetOrganGraph(ByVal OrgType As String, ByVal DeptID As String, ByVal OrganID As String) As String
        Dim objOM As New OM9
        Dim returnValue As New List(Of Dictionary(Of String, String))()
        Dim TotalCnt As Integer = 0
        Dim strWhere As String = ""

        If OrgType <> "" Or DeptID <> "" Or OrganID <> "" Then
            strWhere = GetOrgan(OrgType, DeptID, OrganID)
        End If

        Using dt = objOM.GetOrganData(UserProfile.SelectCompRoleID, Now.Date, strWhere)
            For i As Integer = 0 To dt.Rows.Count - 1
                'TotalCnt = GetTotalCnt(dt.Rows(i).Item("OrganID").ToString.Trim, dt.Rows(i).Item("EmpCnt"), returnValue)
                Dim Dictionary As New Dictionary(Of String, String)
                Dictionary.Add("id", dt.Rows(i).Item("OrganID").ToString.Trim)
                Dictionary.Add("parent", dt.Rows(i).Item("UpOrganID").ToString.Trim)
                Dictionary.Add("itemTitleColor", dt.Rows(i).Item("Color").ToString.Trim)
                Dictionary.Add("title", dt.Rows(i).Item("OrganID").ToString.Trim + dt.Rows(i).Item("OrganName").ToString.Trim)
                Dictionary.Add("boss", dt.Rows(i).Item("Boss").ToString.Trim + " " + dt.Rows(i).Item("NameN").ToString.Trim)
                Dictionary.Add("bTitle", dt.Rows(i).Item("TitleName").ToString.Trim)
                Dictionary.Add("badge", dt.Rows(i).Item("OrgCnt").ToString.Trim)
                'Dictionary.Add("TotCnt", TotalCnt.ToString)
                Dictionary.Add("EmpCnt", dt.Rows(i).Item("EmpCnt").ToString)
                Dictionary.Add("href", "#" + dt.Rows(i).Item("OrganID").ToString.Trim)
                Try
                    Dictionary.Add("image", New ST2().EmpPhotoQuery(dt.Rows(i).Item("BossCompID").ToString.Trim, dt.Rows(i).Item("Boss").ToString.Trim))
                Catch ex As Exception
                    Dictionary.Add("image", "../css/primitives/demo/images/photos/n.png")
                End Try
                returnValue.Add(Dictionary)
            Next

            For i As Integer = 0 To dt.Rows.Count - 1
                Dim OrgID As String = dt.Rows(i).Item("OrganID").ToString.Trim
                Dim TotCnt As Integer = 0
                Dim UnderOrgans As List(Of String) = GetUnderOrgan(OrgID)
                For Each UnderOrganID As String In UnderOrgans
                    Dim dic As Dictionary(Of String, String) = returnValue.Find(Function(p) p.Item("id") = UnderOrganID)
                    TotCnt = TotCnt + Integer.Parse(dic.Item("EmpCnt"))
                Next
                returnValue.Find(Function(p) p.Item("id") = OrgID).Add("TotCnt", TotCnt.ToString)
            Next
        End Using

        Return JsonConvert.SerializeObject(returnValue)
    End Function

    Private Shared Function GetUnderOrgan(ByVal OrganID As String) As List(Of String)
        Dim ArrValue = New List(Of String)

        Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.StoredProcedure, "SP_GetUnderOrgan", _
            New DbParameter() {Bsp.DB.getDbParameter("@CompID", UserProfile.SelectCompRoleID), Bsp.DB.getDbParameter("@OrganID", OrganID)}, "eHRMSDB").Tables(0)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    ArrValue.Add(dr.Item(0).ToString)
                Next
            End If
        End Using

        Return ArrValue
    End Function

    Private Shared Function GetTotalCnt(ByVal OrganID As String, ByVal EmpCnt As Integer, ByVal List As List(Of Dictionary(Of String, String))) As Integer

        Dim ListDic As List(Of Dictionary(Of String, String)) = List.FindAll(Function(p) p.Item("parent") = OrganID)
        For Each dic As Dictionary(Of String, String) In ListDic
            EmpCnt = EmpCnt + Integer.Parse(dic.Item("TotCnt"))
        Next

        Return EmpCnt
    End Function

    Private Shared Function GetOrgan(ByVal OrgType As String, ByVal DeptID As String, ByVal OrganID As String) As String
        Dim objRG As New RG1
        Dim ArrOrgan As New ArrayList()

        If OrgType <> "" And DeptID <> "" Then
            Dim strOrgType As String = objRG.QueryData("Organization", "And CompID = " & Bsp.Utility.Quote(UserProfile.SelectCompRoleID) & " And OrganID = " & Bsp.Utility.Quote(DeptID), "OrgType")
            If OrgType <> strOrgType Then
                Return "And 1=0"
            End If
        End If

        If OrganID <> "" Then
            Return "And O.OrganID = " & Bsp.Utility.Quote(OrganID) & " Or O.DeptID = " & Bsp.Utility.Quote(OrganID) & " Or O.UpOrganID = " & Bsp.Utility.Quote(OrganID) & " Or O.OrgType = " & Bsp.Utility.Quote(OrganID) & ")"
        End If

        If OrgType <> "" And DeptID = "" Then
            Dim strUpOrganID As String = objRG.QueryData("Organization", "And CompID = " & Bsp.Utility.Quote(UserProfile.SelectCompRoleID) & " And OrganID = " & Bsp.Utility.Quote(OrgType), "UpOrganID")
            If OrgType = strUpOrganID Then
                Return ""
            End If
        End If

        If OrgType = "" And DeptID <> "" Then
            Dim UpOrganID As String = objRG.QueryData("Organization", "And CompID = " & Bsp.Utility.Quote(UserProfile.SelectCompRoleID) & " And OrganID = " & Bsp.Utility.Quote(DeptID), "UpOrganID")
            If DeptID = UpOrganID Then
                Return ""
            End If
        End If

        If (OrgType <> "" And DeptID = "") Or (OrgType = "" And DeptID <> "") Or (OrgType <> "" And DeptID <> "") Then
            Dim strOrgan As String = IIf(OrgType <> "" And DeptID <> "", DeptID, IIf(OrgType <> "", OrgType, DeptID))
            Dim returnValue = ""

            Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.StoredProcedure, "SP_GetUnderOrgan", _
                New DbParameter() {Bsp.DB.getDbParameter("@CompID", UserProfile.SelectCompRoleID), Bsp.DB.getDbParameter("@OrganID", strOrgan)}, "eHRMSDB").Tables(0)
                If dt.Rows.Count > 0 Then
                    returnValue = "And O.OrganID In ('"
                    For Each dr As DataRow In dt.Rows
                        returnValue = returnValue & dr.Item(0).ToString & "', '"
                    Next
                    returnValue = returnValue.Substring(0, returnValue.Length - 3) & ")"
                End If
            End Using
            Return returnValue
        End If

        Return ""
    End Function
#End Region

    Protected Sub ddlOrgType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlOrgType.SelectedIndexChanged

    End Sub
End Class
