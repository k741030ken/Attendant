Imports System.Data
Imports System.Data.Common
Partial Class OV_OV8003
    Inherits PageBase
    Private Property eHRMSDB As String
        Get
            Dim result As String = ConfigurationManager.AppSettings("eHRMSDB")
            If String.IsNullOrEmpty(result) Then
                result = "eHRMSDB_ITRD"
            End If
            Return result
        End Get
        Set(ByVal value As String)

        End Set
    End Property
    Private _BaseSQL As String = "SELECT FE.CompID,FE.SystemID,FE.FlowCode, FE.FlowName, FE.FlowSN, FE.FlowSeq, FE.FlowSeqName, FE.FlowStartFlag, FE.FlowEndFlag, FE.InValidFlag, FE.VisableFlag," &
                                 "CASE FE.FlowAct WHEN '1' THEN '正常' WHEN '2' THEN '跳過' END AS FlowAct," &
                                 "CASE FE.SignLineDefine WHEN '1' THEN '行政組織' WHEN '2' THEN '功能組織' WHEN '3' THEN '特定人員' ELSE '' END AS SignLineDefine," &
                                 "CASE FE.SingIDDefine WHEN '1' THEN '組織主管' WHEN '2' THEN  '特定人員' ELSE '' END AS SingIDDefine," &
                                 "FE.SpeComp, ISNULL(C.CompName,'') AS SpeCompName,FE.SpeEmpID, FE.SpeEmpID + + CASE SingIDDefine WHEN 1 THEN '' WHEN 2 THEN '-' END + ISNULL(P.NameN,'') AS SpeEmpName," &
                                 "FE.LastChgComp + '-' + ISNULL(C2.CompName,'') AS LastChgComp, FE.LastChgID + '-' + ISNULL(P2.NameN,'') AS LastChgID, LastChgDate = Case When Convert(Char(10), FE.LastChgDate, 111) = '1900/01/01' Then '' ELSE Convert(Char(10), FE.LastChgDate, 111) End" &
                                 " FROM HRFlowEngine FE " &
                                 " LEFT JOIN " + eHRMSDB + ".[dbo].[Company] C ON C.CompID=FE.SpeComp " &
                                 " LEFT JOIN " + eHRMSDB + ".[dbo].[Personal] P ON P.EmpID=FE.SpeEmpID" &
                                 " LEFT JOIN " + eHRMSDB + ".[dbo].[Company] C2 ON C2.CompID=FE.LastChgComp " &
                                 " LEFT JOIN " + eHRMSDB + ".[dbo].[Personal] P2 ON P2.EmpID=FE.LastChgID"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
        End If
    End Sub
    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)
        If ht.ContainsKey("SelectedCompID") Then
            ViewState.Item("SystemID") = ht("SelectedSystemID").ToString()
            ViewState.Item("FlowCode") = ht("SelectedFlowCode").ToString()
            ViewState.Item("FlowName") = ht("SelectedFlowName").ToString()
            ViewState.Item("FlowSN") = ht("SelectedFlowSN").ToString()
            ViewState.Item("FlowSeq") = ht("SelectedFlowSeq").ToString()
            ViewState.Item("FlowSeqName") = ht("SelectedFlowSeqName").ToString()
            ViewState.Item("FlowStartFlag") = ht("SelectedFlowStartFlag").ToString()
            ViewState.Item("FlowEndFlag") = ht("SelectedFlowEndFlag").ToString()
            ViewState.Item("InValidFlag") = ht("SelectedInValidFlag").ToString()
            ViewState.Item("VisableFlag") = ht("SelectedVisableFlag").ToString()
            ViewState.Item("FlowAct") = ht("SelectedFlowAct").ToString()
            ViewState.Item("SignLineDefine") = ht("SelectedSignLineDefine").ToString()
            ViewState.Item("SingIDDefine") = ht("SelectedSingIDDefine").ToString()
            ViewState.Item("SpeComp") = ht("SelectedSpeComp").ToString()
            ViewState.Item("SpeEmpID") = ht("SelectedSpeEmpID").ToString()
            ViewState.Item("lblLastChgComp") = ht("SelectedLastChgComp").ToString()
            ViewState.Item("lblLastChgID") = ht("SelectedLastChgID").ToString()
            ViewState.Item("lblLastChgDate") = ht("SelectedLastChgDate").ToString()
            '公司代碼
            lblCompID.Text = UserProfile.SelectCompRoleName
            lblOTSystemID.Text = "OT"
            lblFlowCode.Text = ViewState.Item("FlowCode")
            lblFlowName.Text = ViewState.Item("FlowName")
            lblFlowSN.Text = ViewState.Item("FlowSN")
            lblFlowSeq.Text = ViewState.Item("FlowSeq") + ViewState.Item("FlowSeqName")
            If ViewState.Item("FlowStartFlag") = "1" Then
                lblFlowStartFlag.Text = "Y"
            End If
            If ViewState.Item("FlowEndFlag") = "1" Then
                lblFlowEndFlag.Text = "Y"
            End If
            If ViewState.Item("InValidFlag") = "1" Then
                lblInValidFlag.Text = "Y"
            End If
            If ViewState.Item("VisableFlag") = "1" Then
                lblVisableFlag.Text = "Y"
            End If
            If ViewState.Item("FlowAct").ToString() = "正常" Then
                lblFlowAct.Text = "1-" + ViewState.Item("FlowAct")
            ElseIf ViewState.Item("FlowAct").ToString() = "跳過" Then
                lblFlowAct.Text = "2-" + ViewState.Item("FlowAct")
            End If
            If ViewState.Item("SignLineDefine") = "行政組織" Then
                lblSignLineDefine.Text = "1-依" + ViewState.Item("SignLineDefine")
            ElseIf ViewState.Item("SignLineDefine") = "功能組織" Then
                lblSignLineDefine.Text = "2-依" + ViewState.Item("SignLineDefine")
            ElseIf ViewState.Item("SignLineDefine") = "特定人員" Then
                lblSignLineDefine.Text = "3-依" + ViewState.Item("SignLineDefine")
            End If
            If ViewState.Item("SingIDDefine").ToString() = "組織主管" Then
                lblSingIDDefine.Text = "1-" + ViewState.Item("SingIDDefine")
            ElseIf ViewState.Item("SingIDDefine").ToString() = "特定人員" Then
                lblSingIDDefine.Text = "2-" + ViewState.Item("SingIDDefine")
            End If
            If ViewState.Item("SingIDDefine") = "特定人員" Then
                lblSpeCompID.Text = ViewState.Item("SpeComp")
                Dim rtnCompName As DataTable = GetCompName(ViewState.Item("SpeComp"))
                lblSpeCompName.Text = rtnCompName.Rows(0).Item(0)
                txtSpeEmpID.Text = ViewState.Item("SpeEmpID")
                Dim objHR As New HR
                Dim rtnTable As DataTable = objHR.GetHREmpName(lblSpeCompID.Text, txtSpeEmpID.Text)
                lblSpeEmpID.Text = rtnTable.Rows(0).Item(0)
                trSpec.Visible = True
            End If
            lblLastChgComp.Text = ViewState.Item("lblLastChgComp")
            lblLastChgID.Text = ViewState.Item("lblLastChgID")
            lblLastChgDate.Text = ViewState.Item("lblLastChgDate")
        Else
            Return
        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnCancel"    '返回
                GoBack()
        End Select
    End Sub

    Private Sub GoBack()
        Dim ti As TransferInfo = Me.StateTransfer
        Me.TransferFramePage(ti.CallerUrl, Nothing, ti.Args)
    End Sub

    Public Shared Function GetCompName(ByVal CompID As String) As DataTable
        Dim strSQL As String
        strSQL = "Select CompName From Company"
        strSQL += " Where CompID =  " & Bsp.Utility.Quote(CompID)

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
End Class
