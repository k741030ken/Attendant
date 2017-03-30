'****************************************************
'功能說明：流程關卡處理主程式
'建立人員：Chung
'建立日期：2013/01/29
'****************************************************
Imports System.Data

Partial Class WF_WFA011
    Inherits PageBase

    Const FlowMenuUrl As String = "~/WF/WFA010.aspx"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then

        End If
    End Sub

    Protected Overrides Sub BaseOnPageTransfer(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim objWF As New WF
            Try
                Select Case ti.CallerPageID
                    Case "WFA000"
                        DoAction_Default(ti.Args)
                    Case Else
                        DoAction_Default(ti.Args)
                End Select


            Catch ex As Exception
                Bsp.Utility.ShowMessage(Me, Me.FunID & ".BaseOnPageTransfer", ex)
                TransferFramePage(ResolveUrl(Bsp.MySettings.ToDoListPage), Nothing, ti.Args)
            End Try
        End If
    End Sub

    Private Sub DoAction_Default(ByVal Args() As Object)
        Dim objWF As New WF()
        Dim objSC As New SC()
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Args)
        Dim htFlowKey As Hashtable = Nothing
        Dim strRedirectPage As String = ""
        Dim bolShowMode As Boolean = False
        Dim NewArgs() As Object = Nothing
        Dim HasBackInfo As Boolean = False  '是否有回傳物件

        '將原始資料複製
        For Each o As Object In Args
            If TypeOf o Is FlowBackInfo Then
                HasBackInfo = True
                Exit For
            End If
        Next

        '若FlowCaseID="",表示為無流程之Menu,將會取用WF_FlowMenu內的FlowStepID=0000的資料，
        '但FlowID必要傳入
        If Bsp.Utility.IsStringNull(ht("FlowCaseID")) = "" Then
            Using dt As DataTable = objSC.GetFlowStepM(Bsp.Utility.IsStringNull(ht("FlowID")), "1", "0000")
                If dt.Rows.Count = 0 Then
                    Throw New Exception(String.Format("[DoAction_Default]:查無關卡資料[{0}]!", Bsp.Utility.IsStringNull(ht("FlowID"))))
                End If

                strRedirectPage = Bsp.Utility.IsStringNull(dt.Rows(0).Item("DefaultPage"), "")

                If strRedirectPage = "" Then
                    Throw New Exception(String.Format("[DoAction_Default]:流程關卡[{0}]未設定預設連結網頁!", Bsp.Utility.IsStringNull(ht("FlowID"))))
                End If
            End Using
            Array.Resize(NewArgs, Args.Length + IIf(HasBackInfo, 1, 0))
        Else
            Using dt As DataTable = objWF.GetFlowToDoList(ht("FlowID").ToString(), ht("FlowCaseID").ToString(), ht("FlowLogID").ToString())
                If dt.Rows.Count > 0 Then
                    htFlowKey = Bsp.Utility.getHashTableFromParam(dt.Rows(0).Item("FlowKeyValue").ToString())
                    '加入FlowLogBatNo
                    If Not htFlowKey.ContainsKey("FlowLogBatNo") Then htFlowKey.Add("FlowLogBatNo", dt.Rows(0).Item("FlowLogBatNo").ToString())
                    If Not htFlowKey.ContainsKey("FlowStepID") Then htFlowKey.Add("FlowStepID", dt.Rows(0).Item("FlowStepID").ToString())
                    If dt.Rows(0).Item("AssignTo").ToString() <> UserProfile.UserID Then
                        bolShowMode = True
                    End If
                Else
                    Using dtFlowCase As DataTable = objWF.GetFlowCase(ht("FlowID").ToString(), ht("FlowCaseID").ToString())
                        If dtFlowCase.Rows.Count > 0 Then
                            htFlowKey = Bsp.Utility.getHashTableFromParam(dtFlowCase.Rows(0).Item("FlowKeyValue").ToString())
                            If Not ht.ContainsKey("ShowMode") Then bolShowMode = True
                        Else
                            Throw New Exception(String.Format("[DoAction_Default]:查無案號[{0}-{1}]流程紀錄!", ht("FlowID").ToString(), ht("FlowCaseID").ToString()))
                        End If
                    End Using
                End If
            End Using
            '尋找DefaultPage
            Using dt As DataTable = objWF.GetFlowStepMbyFlowCase(ht("FlowID").ToString(), ht("FlowCaseID").ToString())
                If dt.Rows.Count = 0 Then
                    Throw New Exception(String.Format("[DoAction_Default]:查無關卡資料[{0}-{1}]!", ht("FlowID").ToString(), ht("FlowCaseID").ToString()))
                End If
                If ht.ContainsKey("ShowMode") OrElse bolShowMode Then
                    strRedirectPage = Bsp.Utility.IsStringNull(dt.Rows(0).Item("ShowModePage"), "")
                    If strRedirectPage = "" Then strRedirectPage = Bsp.Utility.IsStringNull(dt.Rows(0).Item("DefaultPage"), "")
                Else
                    strRedirectPage = Bsp.Utility.IsStringNull(dt.Rows(0).Item("DefaultPage"), "")
                End If
                If strRedirectPage = "" Then
                    Throw New Exception(String.Format("[DoAction_Default]:流程關卡[{0}]未設定預設連結網頁!", dt.Rows(0).Item("FlowStepID").ToString()))
                End If
            End Using
            Array.Resize(NewArgs, Args.Length + htFlowKey.Count + IIf(bolShowMode, 1, 0) + IIf(HasBackInfo, 1, 0))
        End If


        '將所有鍵值組合
        Dim intPos As Integer = 0

        '若有返回物件，則第一個參數固定放原傳入的參數物件
        If HasBackInfo Then
            NewArgs(intPos) = Args
            intPos = 1
        End If

        For intLoop As Integer = 0 To Args.GetUpperBound(0)
            NewArgs(intPos) = Args(intLoop)
            intPos += 1
        Next

        If htFlowKey IsNot Nothing Then
            For Each strKey As String In htFlowKey.Keys
                NewArgs(intPos) = String.Format("{0}={1}", strKey, htFlowKey(strKey).ToString())
                intPos += 1
            Next
        End If

        If bolShowMode Then NewArgs(intPos) = "ShowMode=Y"

        TransferFlowPage(ResolveUrl(strRedirectPage), Nothing, NewArgs)
    End Sub

End Class
