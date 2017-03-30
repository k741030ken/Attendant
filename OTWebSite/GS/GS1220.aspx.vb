'****************************************************
'功能說明：統計表畫面
'建立人員：Micky Sung
'建立日期：2015.11.04
'****************************************************
Imports System.Data

Partial Class GS_GS1220
    Inherits PageBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            pcMain.Visible = False
        End If
    End Sub

    Public Overrides Sub DoAction(ByVal Param As String)
        Select Case Param
            Case "btnQuery"     '查詢
                GetData()
            Case "btnActionX"   '返回
                Bsp.Utility.RunClientScript(Me, "window.top.close();")
        End Select
    End Sub

    Protected Overrides Sub BaseOnPageCall(ByVal ti As TransferInfo)
        If Not IsPostBack() Then
            Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(ti.Args)

            If ht.ContainsKey("CompID") Then
                hidCompID.Value = ht("CompID").ToString()
                hidGradeYear.Value = ht("GradeYear").ToString()
                hidGradeSeq.Value = ht("GradeSeq").ToString()
                hidApplyID.Value = ht("ApplyID").ToString()
                hidApplyTime.Value = ht("ApplyTime").ToString()
                hidSeq.Value = ht("Seq").ToString()
                hidMainFlag.Value = ht("MainFlag").ToString()
                hidDeptEX.Value = ht("DeptEX").ToString()
                ViewState.Item("Status") = ht("Status").ToString()
                Select Case ViewState.Item("Status")
                    Case "4"
                        ddltype.Items.Add(New ListItem("職系統計", "1"))
                        ddltype.Items.Add(New ListItem("依初核考績統計", "2"))
                        ddltype.Items.Add(New ListItem("新員統計(依單位)", "3"))
                        ddltype.Items.Add(New ListItem("新員統計(依初核考績)", "4"))
                    Case "5"
                        ddltype.Items.Add(New ListItem("職系統計", "1"))
                        ddltype.Items.Add(New ListItem("依核定考績統計", "2"))
                        ddltype.Items.Add(New ListItem("新員統計(依單位)", "3"))
                        ddltype.Items.Add(New ListItem("新員統計(依核定考績)", "4"))
                    Case Else
                        ddltype.Items.Add(New ListItem("職系統計", "1"))
                        ddltype.Items.Add(New ListItem("依區處統計", "2"))
                        ddltype.Items.Add(New ListItem("新員統計(依單位)", "3"))
                        ddltype.Items.Add(New ListItem("新員統計(依區/處)", "4"))
                End Select
                
                GetData()
            Else
                Return
            End If
        End If
    End Sub

    Private Sub GetData()
        pcMain.Visible = False
        gvMain2.Visible = False

        Dim GradeField As String = ""
        Select Case ViewState.Item("Status")
            Case "4"
                GradeField = "GradeHR"
            Case "5"
                GradeField = "Grade2"
            Case Else
                GradeField = "Grade"    '20160722 wei modify
        End Select
        Dim strSQL As New StringBuilder()
        If ddltype.SelectedValue = "1" Then '職系
            '測試用
            'strSQL.AppendLine("Select 'XX分行' AS Dept, '12' AS Grade1, '2' AS Grade2, '25' AS Grade3, '9' AS Grade4, '0' AS Grade5 ")
            strSQL.AppendLine(ConfigurationManager.AppSettings("eHRMSDBDES").ToString)
            strSQL.AppendLine(" select * From (")
            strSQL.AppendLine(" Select G.GradeYear,G.GradeSeq,G.CompID,isnull(GP.PositionTypeID,5) as Type,isnull(GP.PositionType,'專業人員') as TypeName")
            '特優
            strSQL.AppendLine(" ,Grade9=cast(isnull((select count(*) From GradeBase G1 Left Join GradePosition GP1 On G1.CompID=GP1.CompID And G1.PositionID=GP1.PositionID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and G1.UpOrganID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(PositionTypeID,5)=isnull(GP.PositionTypeID,5) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='9'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase G1 Left Join GradePosition GP1 On G1.CompID=GP1.CompID And G1.PositionID=GP1.PositionID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID and G1.UpOrganID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(PositionTypeID,5)=isnull(GP.PositionTypeID,5) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='9'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            Else
                strSQL.AppendLine(" and G1.GradeDeptID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(PositionTypeID,5)=isnull(GP.PositionTypeID,5) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='9'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase G1 Left Join GradePosition GP1 On G1.CompID=GP1.CompID And G1.PositionID=GP1.PositionID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID and G1.GradeDeptID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(PositionTypeID,5)=isnull(GP.PositionTypeID,5) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='9'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            End If
            '優
            strSQL.AppendLine(" ,Grade1=cast(isnull((select count(*) From GradeBase G1 Left Join GradePosition GP1 On G1.CompID=GP1.CompID And G1.PositionID=GP1.PositionID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and G1.UpOrganID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(PositionTypeID,5)=isnull(GP.PositionTypeID,5) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='1'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase G1 Left Join GradePosition GP1 On G1.CompID=GP1.CompID And G1.PositionID=GP1.PositionID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID and G1.UpOrganID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(PositionTypeID,5)=isnull(GP.PositionTypeID,5) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='1'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            Else
                strSQL.AppendLine(" and G1.GradeDeptID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(PositionTypeID,5)=isnull(GP.PositionTypeID,5) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='1'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase G1 Left Join GradePosition GP1 On G1.CompID=GP1.CompID And G1.PositionID=GP1.PositionID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID and G1.GradeDeptID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(PositionTypeID,5)=isnull(GP.PositionTypeID,5) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='1'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            End If
            '甲上
            strSQL.AppendLine(" ,Grade6=cast(isnull((select count(*) From GradeBase G1 Left Join GradePosition GP1 On G1.CompID=GP1.CompID And G1.PositionID=GP1.PositionID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and G1.UpOrganID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(PositionTypeID,5)=isnull(GP.PositionTypeID,5) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='6'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase G1 Left Join GradePosition GP1 On G1.CompID=GP1.CompID And G1.PositionID=GP1.PositionID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID and G1.UpOrganID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(PositionTypeID,5)=isnull(GP.PositionTypeID,5) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='6'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            Else
                strSQL.AppendLine(" and G1.GradeDeptID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(PositionTypeID,5)=isnull(GP.PositionTypeID,5) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='6'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase G1 Left Join GradePosition GP1 On G1.CompID=GP1.CompID And G1.PositionID=GP1.PositionID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID and G1.GradeDeptID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(PositionTypeID,5)=isnull(GP.PositionTypeID,5) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='6'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            End If
            '甲
            strSQL.AppendLine(" ,Grade2=cast(isnull((select count(*) From GradeBase G1 Left Join GradePosition GP1 On G1.CompID=GP1.CompID And G1.PositionID=GP1.PositionID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and G1.UpOrganID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(PositionTypeID,5)=isnull(GP.PositionTypeID,5) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='2'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase G1 Left Join GradePosition GP1 On G1.CompID=GP1.CompID And G1.PositionID=GP1.PositionID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID and G1.UpOrganID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(PositionTypeID,5)=isnull(GP.PositionTypeID,5) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='2'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            Else
                strSQL.AppendLine(" and G1.GradeDeptID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(PositionTypeID,5)=isnull(GP.PositionTypeID,5) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='2'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase G1 Left Join GradePosition GP1 On G1.CompID=GP1.CompID And G1.PositionID=GP1.PositionID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID and G1.GradeDeptID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(PositionTypeID,5)=isnull(GP.PositionTypeID,5) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='2'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            End If
            '甲下
            strSQL.AppendLine(" ,Grade7=cast(isnull((select count(*) From GradeBase G1 Left Join GradePosition GP1 On G1.CompID=GP1.CompID And G1.PositionID=GP1.PositionID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and G1.UpOrganID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(PositionTypeID,5)=isnull(GP.PositionTypeID,5) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='7'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase G1 Left Join GradePosition GP1 On G1.CompID=GP1.CompID And G1.PositionID=GP1.PositionID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID and G1.UpOrganID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(PositionTypeID,5)=isnull(GP.PositionTypeID,5) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='7'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            Else
                strSQL.AppendLine(" and G1.GradeDeptID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(PositionTypeID,5)=isnull(GP.PositionTypeID,5) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='7'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase G1 Left Join GradePosition GP1 On G1.CompID=GP1.CompID And G1.PositionID=GP1.PositionID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID and G1.GradeDeptID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(PositionTypeID,5)=isnull(GP.PositionTypeID,5) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='7'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            End If
            '乙
            strSQL.AppendLine(" ,Grade3=cast(isnull((select count(*) From GradeBase G1 Left Join GradePosition GP1 On G1.CompID=GP1.CompID And G1.PositionID=GP1.PositionID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and G1.UpOrganID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(PositionTypeID,5)=isnull(GP.PositionTypeID,5) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='3'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase G1 Left Join GradePosition GP1 On G1.CompID=GP1.CompID And G1.PositionID=GP1.PositionID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID and G1.UpOrganID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(PositionTypeID,5)=isnull(GP.PositionTypeID,5) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='3'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            Else
                strSQL.AppendLine(" and G1.GradeDeptID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(PositionTypeID,5)=isnull(GP.PositionTypeID,5) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='3'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase G1 Left Join GradePosition GP1 On G1.CompID=GP1.CompID And G1.PositionID=GP1.PositionID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID and G1.GradeDeptID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(PositionTypeID,5)=isnull(GP.PositionTypeID,5) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='3'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            End If
            '丙
            strSQL.AppendLine(" ,Grade4=cast(isnull((select count(*) From GradeBase G1 Left Join GradePosition GP1 On G1.CompID=GP1.CompID And G1.PositionID=GP1.PositionID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and G1.UpOrganID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(PositionTypeID,5)=isnull(GP.PositionTypeID,5) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='4'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase G1 Left Join GradePosition GP1 On G1.CompID=GP1.CompID And G1.PositionID=GP1.PositionID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID and G1.UpOrganID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(PositionTypeID,5)=isnull(GP.PositionTypeID,5) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='4'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            Else
                strSQL.AppendLine(" and G1.GradeDeptID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(PositionTypeID,5)=isnull(GP.PositionTypeID,5) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='4'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase G1 Left Join GradePosition GP1 On G1.CompID=GP1.CompID And G1.PositionID=GP1.PositionID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID and G1.GradeDeptID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(PositionTypeID,5)=isnull(GP.PositionTypeID,5) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='4'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            End If
            '合計
            strSQL.AppendLine(" ,Count(*) as TotalCnt")
            strSQL.AppendLine(" from GradeBase G Left Join GradePosition GP On G.CompID=GP.CompID And G.PositionID=GP.PositionID")
            strSQL.AppendLine(" Where G.CompID =" & Bsp.Utility.Quote(hidCompID.Value))
            strSQL.AppendLine(" And G.GradeYear =" & hidGradeYear.Value)
            strSQL.AppendLine(" And G.GradeSeq =" & hidGradeSeq.Value)
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and G.UpOrganID =" & Bsp.Utility.Quote(hidApplyID.Value))
            Else
                strSQL.AppendLine(" and G.GradeDeptID =" & Bsp.Utility.Quote(hidApplyID.Value))
            End If
            strSQL.AppendLine(" And isnull(CONVERT(char(1), DecryptByKey(G." & GradeField & ")) ,'') <>''")
            strSQL.AppendLine(" group by G.GradeYear,G.GradeSeq,G.CompID,isnull(GP.PositionTypeID,5),isnull(GP.PositionType,'專業人員')")
            strSQL.AppendLine(" union")
            strSQL.AppendLine(" Select G.GradeYear,G.GradeSeq,G.CompID,9999 as 'Type','合計 人數(比例)' as 'TypeName'")
            '特優合計
            strSQL.AppendLine(" ,Grade9=cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='9'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='9'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            Else
                strSQL.AppendLine(" and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='9'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='9'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            End If
            '優合計
            strSQL.AppendLine(" ,Grade1=cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='1'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='1'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            Else
                strSQL.AppendLine(" and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='1'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='1'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            End If
            '甲上合計
            strSQL.AppendLine(" ,Grade6=cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='6'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='6'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            Else
                strSQL.AppendLine(" and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='6'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='6'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            End If
            '甲合計
            strSQL.AppendLine(" ,Grade2=cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='2'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='2'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            Else
                strSQL.AppendLine(" and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='2'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='2'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            End If
            '甲下合計
            strSQL.AppendLine(" ,Grade7=cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='7'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='7'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            Else
                strSQL.AppendLine(" and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='7'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='7'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            End If
            '乙合計
            strSQL.AppendLine(" ,Grade3=cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='3'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='3'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            Else
                strSQL.AppendLine(" and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='3'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='3'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            End If
            '丙合計
            strSQL.AppendLine(" ,Grade4=cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='4'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='4'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            Else
                strSQL.AppendLine(" and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='4'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='4'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            End If
            '合計
            strSQL.AppendLine(" ,Count(*) as TotalCnt")
            strSQL.AppendLine(" from GradeBase G Left Join GradePosition GP On G.CompID=GP.CompID And G.PositionID=GP.PositionID ")
            strSQL.AppendLine(" Where G.CompID =" & Bsp.Utility.Quote(hidCompID.Value))
            strSQL.AppendLine(" And G.GradeYear =" & hidGradeYear.Value)
            strSQL.AppendLine(" And G.GradeSeq =" & hidGradeSeq.Value)
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and G.UpOrganID =" & Bsp.Utility.Quote(hidApplyID.Value))
            Else
                strSQL.AppendLine(" and G.GradeDeptID =" & Bsp.Utility.Quote(hidApplyID.Value))
            End If
            strSQL.AppendLine(" And isnull(CONVERT(char(1), DecryptByKey(G." & GradeField & ")) ,'') <>''")
            strSQL.AppendLine(" group by G.GradeYear,G.GradeSeq,G.CompID ")
            strSQL.AppendLine(" ) S")
            strSQL.AppendLine(" Order By Type")
            lblTitle.Text = "職系統計"
            Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
                gvMain1.DataSource = dt
                gvMain1.DataBind()
            End Using
        ElseIf ddltype.SelectedValue = "2" Then '依區處
            '測試用
            'strSQL.AppendLine("Select 'XX分行' AS Dept, '12' AS Grade1, '2' AS Grade2, '25' AS Grade3, '9' AS Grade4, '0' AS Grade5 ")
            strSQL.AppendLine(ConfigurationManager.AppSettings("eHRMSDBDES").ToString)
            strSQL.AppendLine(" select * From (")
            strSQL.AppendLine(" Select G.GradeYear,G.GradeSeq,G.CompID,G.GradeDeptID as Type,O.OrganName as TypeName")
            '特優
            strSQL.AppendLine(" ,Grade9=cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID and GradeDeptID=G.GradeDeptID and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='9'),0) as varchar(3))")
            strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID and GradeDeptID = G.GradeDeptID and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='9'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            '優
            strSQL.AppendLine(" ,Grade1=cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID=G.CompID and GradeDeptID=G.GradeDeptID and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='1'),0) as varchar(3))")
            strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID and GradeDeptID = G.GradeDeptID and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='1'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            '甲上
            strSQL.AppendLine(" ,Grade6=cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID=G.CompID and GradeDeptID=G.GradeDeptID and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='6'),0) as varchar(3))")
            strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID and GradeDeptID = G.GradeDeptID and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='6'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            '甲
            strSQL.AppendLine(" ,Grade2=cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID=G.CompID and GradeDeptID=G.GradeDeptID and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='2'),0) as varchar(3))")
            strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID and GradeDeptID = G.GradeDeptID and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='2'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            '甲下
            strSQL.AppendLine(" ,Grade7=cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID=G.CompID and GradeDeptID=G.GradeDeptID and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='7'),0) as varchar(3))")
            strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID and GradeDeptID = G.GradeDeptID and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='7'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            '乙
            strSQL.AppendLine(" ,Grade3=cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID=G.CompID and GradeDeptID=G.GradeDeptID and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='3'),0) as varchar(3))")
            strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID and GradeDeptID = G.GradeDeptID and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='3'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            '丙
            strSQL.AppendLine(" ,Grade4=cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID=G.CompID and GradeDeptID=G.GradeDeptID and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='4'),0) as varchar(3))")
            strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID and GradeDeptID = G.GradeDeptID and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='4'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            '合計
            strSQL.AppendLine(" ,Count(*) as TotalCnt")
            strSQL.AppendLine(" from GradeBase G Left Join Organization O On G.CompID=O.CompID And G.GradeDeptID=O.OrganID")
            strSQL.AppendLine(" Where G.CompID =" & Bsp.Utility.Quote(hidCompID.Value))
            strSQL.AppendLine(" And G.GradeYear =" & hidGradeYear.Value)
            strSQL.AppendLine(" And G.GradeSeq =" & hidGradeSeq.Value)
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and G.UpOrganID =" & Bsp.Utility.Quote(hidApplyID.Value))
            Else
                strSQL.AppendLine(" and G.GradeDeptID =" & Bsp.Utility.Quote(hidApplyID.Value))
            End If
            strSQL.AppendLine(" And isnull(CONVERT(char(1), DecryptByKey(G." & GradeField & ")) ,'') <>''")
            strSQL.AppendLine(" group by G.GradeYear,G.GradeSeq,G.CompID,G.GradeDeptID,O.OrganName")
            strSQL.AppendLine(" union")
            strSQL.AppendLine(" Select G.GradeYear,G.GradeSeq,G.CompID,'ZZZZZZ' as 'Type','處/區合計(人)' as 'TypeName'")
            '特優合計
            'strSQL.AppendLine(" ,Grade9=isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID")
            strSQL.AppendLine(" ,Grade9=cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                'strSQL.AppendLine(" and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='9'),0)")
                strSQL.AppendLine(" and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='9'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='9'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            Else
                'strSQL.AppendLine(" and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='9'),0)")
                strSQL.AppendLine(" and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='9'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='9'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            End If
            '優合計
            'strSQL.AppendLine(" ,Grade1=isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID")
            strSQL.AppendLine(" ,Grade1=cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                'strSQL.AppendLine(" and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='1'),0)")
                strSQL.AppendLine(" and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='1'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='1'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            Else
                'strSQL.AppendLine(" and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='1'),0)")
                strSQL.AppendLine(" and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='1'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='1'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            End If
            '甲上合計
            'strSQL.AppendLine(" ,Grade6=isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID")
            strSQL.AppendLine(" ,Grade6=cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                'strSQL.AppendLine(" and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='6'),0)")
                strSQL.AppendLine(" and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='6'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='6'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            Else
                'strSQL.AppendLine(" and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='6'),0)")
                strSQL.AppendLine(" and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='6'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='6'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            End If
            '甲合計
            'strSQL.AppendLine(" ,Grade2=isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID")
            strSQL.AppendLine(" ,Grade2=cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                'strSQL.AppendLine(" and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='2'),0)")
                strSQL.AppendLine(" and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='2'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='2'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            Else
                'strSQL.AppendLine(" and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='2'),0)")
                strSQL.AppendLine(" and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='2'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='2'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            End If
            '甲下合計
            'strSQL.AppendLine(" ,Grade7=isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID")
            strSQL.AppendLine(" ,Grade7=cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                'strSQL.AppendLine(" and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='7'),0)")
                strSQL.AppendLine(" and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='7'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='7'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            Else
                'strSQL.AppendLine(" and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='7'),0)")
                strSQL.AppendLine(" and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='7'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='7'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            End If
            '乙合計
            'strSQL.AppendLine(" ,Grade3=isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID")
            strSQL.AppendLine(" ,Grade3=cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                'strSQL.AppendLine(" and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='3'),0)")
                strSQL.AppendLine(" and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='3'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='3'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            Else
                'strSQL.AppendLine(" and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='3'),0)")
                strSQL.AppendLine(" and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='3'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='3'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            End If
            '丙合計
            'strSQL.AppendLine(" ,Grade4=isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID")
            strSQL.AppendLine(" ,Grade4=cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                'strSQL.AppendLine(" and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='4'),0)")
                strSQL.AppendLine(" and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='4'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='4'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            Else
                'strSQL.AppendLine(" and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='4'),0)")
                strSQL.AppendLine(" and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='4'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and CompID = G.CompID and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='4'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            End If
            '合計
            strSQL.AppendLine(" ,Count(*) as TotalCnt")
            strSQL.AppendLine(" from GradeBase G Left Join Organization O On G.CompID=O.CompID And G.GradeDeptID=O.OrganID ")
            strSQL.AppendLine(" Where G.CompID =" & Bsp.Utility.Quote(hidCompID.Value))
            strSQL.AppendLine(" And G.GradeYear =" & hidGradeYear.Value)
            strSQL.AppendLine(" And G.GradeSeq =" & hidGradeSeq.Value)
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and G.UpOrganID =" & Bsp.Utility.Quote(hidApplyID.Value))
            Else
                strSQL.AppendLine(" and G.GradeDeptID =" & Bsp.Utility.Quote(hidApplyID.Value))
            End If
            strSQL.AppendLine(" And isnull(CONVERT(char(1), DecryptByKey(G." & GradeField & ")) ,'') <>''")
            strSQL.AppendLine(" group by G.GradeYear,G.GradeSeq,G.CompID ")
            strSQL.AppendLine(" ) S")
            strSQL.AppendLine(" Order By Type")
            lblTitle.Text = "依區處統計"
            Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
                gvMain1.DataSource = dt
                gvMain1.DataBind()
            End Using
        ElseIf ddltype.SelectedValue = "3" Then '新員依單位
            '測試用
            'strSQL.AppendLine("Select '20414新員' AS Dept, '12(10%)' AS Grade1, '2(2%)' AS Grade2, '25(40%)' AS Grade3, '95(%)' AS Grade4, '0(0%)' AS Grade5 ")
            strSQL.AppendLine(ConfigurationManager.AppSettings("eHRMSDBDES").ToString)
            strSQL.AppendLine(" select * From (")
            strSQL.AppendLine(" Select G.GradeYear,G.GradeSeq,G.CompID,Year(P.EmpDate) as Type,cast(Year(P.EmpDate) as varchar(4)) + '新員' as TypeName")
            '特優
            strSQL.AppendLine(" ,Grade9=cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and G1.UpOrganID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(GradeDept)) ,'')='9'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID and G1.UpOrganID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(GradeDept)) ,'')='9'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            Else
                strSQL.AppendLine(" and G1.GradeDeptID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(GradeDept)) ,'')='9'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID and G1.GradeDeptID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(GradeDept)) ,'')='9'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            End If
            '優
            strSQL.AppendLine(" ,Grade1=cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and G1.UpOrganID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(GradeDept)) ,'')='1'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID and G1.UpOrganID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(GradeDept)) ,'')='1'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            Else
                strSQL.AppendLine(" and G1.GradeDeptID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(GradeDept)) ,'')='1'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID and G1.GradeDeptID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(GradeDept)) ,'')='1'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            End If
            '甲上
            strSQL.AppendLine(" ,Grade6=cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and G1.UpOrganID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(GradeDept)) ,'')='6'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID and G1.UpOrganID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(GradeDept)) ,'')='6'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            Else
                strSQL.AppendLine(" and G1.GradeDeptID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(GradeDept)) ,'')='6'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID and G1.GradeDeptID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(GradeDept)) ,'')='6'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            End If
            '甲
            strSQL.AppendLine(" ,Grade2=cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and G1.UpOrganID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(GradeDept)) ,'')='2'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID and G1.UpOrganID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(GradeDept)) ,'')='2'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            Else
                strSQL.AppendLine(" and G1.GradeDeptID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(GradeDept)) ,'')='2'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID and G1.GradeDeptID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(GradeDept)) ,'')='2'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            End If
            '甲下
            strSQL.AppendLine(" ,Grade7=cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and G1.UpOrganID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(GradeDept)) ,'')='7'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID and G1.UpOrganID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(GradeDept)) ,'')='7'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            Else
                strSQL.AppendLine(" and G1.GradeDeptID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(GradeDept)) ,'')='7'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID and G1.GradeDeptID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(GradeDept)) ,'')='7'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            End If
            '乙
            strSQL.AppendLine(" ,Grade3=cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and G1.UpOrganID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(GradeDept)) ,'')='3'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID and G1.UpOrganID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(GradeDept)) ,'')='3'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            Else
                strSQL.AppendLine(" and G1.GradeDeptID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(GradeDept)) ,'')='3'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID and G1.GradeDeptID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(GradeDept)) ,'')='3'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            End If
            '丙
            strSQL.AppendLine(" ,Grade4=cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and G1.UpOrganID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(GradeDept)) ,'')='4'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID and G1.UpOrganID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(GradeDept)) ,'')='4'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            Else
                strSQL.AppendLine(" and G1.GradeDeptID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(GradeDept)) ,'')='4'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID and G1.GradeDeptID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(GradeDept)) ,'')='4'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            End If
            '合計
            strSQL.AppendLine(" ,Count(*) as TotalCnt")
            strSQL.AppendLine(" from GradeBase G Left Join Personal P On G.CompID=P.CompID And G.EmpID=P.EmpID")
            strSQL.AppendLine(" Where G.CompID =" & Bsp.Utility.Quote(hidCompID.Value))
            strSQL.AppendLine(" And G.GradeYear =" & hidGradeYear.Value)
            strSQL.AppendLine(" And G.GradeSeq =" & hidGradeSeq.Value)
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and G.UpOrganID =" & Bsp.Utility.Quote(hidApplyID.Value))
            Else
                strSQL.AppendLine(" and G.GradeDeptID =" & Bsp.Utility.Quote(hidApplyID.Value))
            End If
            strSQL.AppendLine(" And isnull(CONVERT(char(1), DecryptByKey(G.GradeDept)) ,'') <>''")
            strSQL.AppendLine(" and Year(P.EmpDate) in (G.GradeYear,GradeYear-1) ")
            strSQL.AppendLine(" group by G.GradeYear,G.GradeSeq,G.CompID,Year(P.EmpDate)")
            strSQL.AppendLine(" union")
            strSQL.AppendLine(" Select G.GradeYear,G.GradeSeq,G.CompID,'9999' as 'Type','合計(人)' as 'TypeName'")
            '特優合計
            strSQL.AppendLine(" ,Grade9=cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate) in (G.GradeYear,GradeYear-1) and isnull(CONVERT(char(1), DecryptByKey(GradeDept)) ,'')='9'),0) as varchar(3))")
            Else
                strSQL.AppendLine(" and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate) in (G.GradeYear,GradeYear-1) and isnull(CONVERT(char(1), DecryptByKey(GradeDept)) ,'')='9'),0) as varchar(3))")
            End If
            '優合計
            strSQL.AppendLine(" ,Grade1=cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate) in (G.GradeYear,GradeYear-1) and isnull(CONVERT(char(1), DecryptByKey(GradeDept)) ,'')='1'),0) as varchar(3))")
            Else
                strSQL.AppendLine(" and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate) in (G.GradeYear,GradeYear-1) and isnull(CONVERT(char(1), DecryptByKey(GradeDept)) ,'')='1'),0) as varchar(3))")
            End If
            '甲上合計
            strSQL.AppendLine(" ,Grade6=cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate) in (G.GradeYear,GradeYear-1) and isnull(CONVERT(char(1), DecryptByKey(GradeDept)) ,'')='6'),0) as varchar(3))")
            Else
                strSQL.AppendLine(" and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate) in (G.GradeYear,GradeYear-1) and isnull(CONVERT(char(1), DecryptByKey(GradeDept)) ,'')='6'),0) as varchar(3))")
            End If
            '甲合計
            strSQL.AppendLine(" ,Grade2=cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate) in (G.GradeYear,GradeYear-1) and isnull(CONVERT(char(1), DecryptByKey(GradeDept)) ,'')='2'),0) as varchar(3))")
            Else
                strSQL.AppendLine(" and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate) in (G.GradeYear,GradeYear-1) and isnull(CONVERT(char(1), DecryptByKey(GradeDept)) ,'')='2'),0) as varchar(3))")
            End If
            '甲下合計
            strSQL.AppendLine(" ,Grade7=cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate) in (G.GradeYear,GradeYear-1) and isnull(CONVERT(char(1), DecryptByKey(GradeDept)) ,'')='7'),0) as varchar(3))")
            Else
                strSQL.AppendLine(" and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate) in (G.GradeYear,GradeYear-1) and isnull(CONVERT(char(1), DecryptByKey(GradeDept)) ,'')='7'),0) as varchar(3))")
            End If
            '乙合計
            strSQL.AppendLine(" ,Grade3=cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate) in (G.GradeYear,GradeYear-1) and isnull(CONVERT(char(1), DecryptByKey(GradeDept)) ,'')='3'),0) as varchar(3))")
            Else
                strSQL.AppendLine(" and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate) in (G.GradeYear,GradeYear-1) and isnull(CONVERT(char(1), DecryptByKey(GradeDept)) ,'')='3'),0) as varchar(3))")
            End If
            '丙合計
            strSQL.AppendLine(" ,Grade4=cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate) in (G.GradeYear,GradeYear-1) and isnull(CONVERT(char(1), DecryptByKey(GradeDept)) ,'')='4'),0) as varchar(3))")
            Else
                strSQL.AppendLine(" and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate) in (G.GradeYear,GradeYear-1) and isnull(CONVERT(char(1), DecryptByKey(GradeDept)) ,'')='4'),0) as varchar(3))")
            End If
            '合計
            strSQL.AppendLine(" ,Count(*) as TotalCnt")
            strSQL.AppendLine(" from GradeBase G Left Join Personal P On G.CompID=P.CompID And G.EmpID=P.EmpID ")
            strSQL.AppendLine(" Where G.CompID =" & Bsp.Utility.Quote(hidCompID.Value))
            strSQL.AppendLine(" And G.GradeYear =" & hidGradeYear.Value)
            strSQL.AppendLine(" And G.GradeSeq =" & hidGradeSeq.Value)
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and G.UpOrganID =" & Bsp.Utility.Quote(hidApplyID.Value))
            Else
                strSQL.AppendLine(" and G.GradeDeptID =" & Bsp.Utility.Quote(hidApplyID.Value))
            End If
            strSQL.AppendLine(" And isnull(CONVERT(char(1), DecryptByKey(G.GradeDept)) ,'') <>''")
            strSQL.AppendLine(" and Year(P.EmpDate) in (G.GradeYear,GradeYear-1) ")
            strSQL.AppendLine(" group by G.GradeYear,G.GradeSeq,G.CompID ")
            strSQL.AppendLine(" ) S")
            strSQL.AppendLine(" Order By Type")

            lblTitle.Text = "新員統計(依單位)"
            Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
                gvMain1.DataSource = dt
                gvMain1.DataBind()
            End Using
        ElseIf ddltype.SelectedValue = "4" Then '新員依區處
            '測試用
            'strSQL.AppendLine("Select '20414新員' AS Dept, '12(10%)' AS Grade1, '2(2%)' AS Grade2, '25(40%)' AS Grade3, '95(%)' AS Grade4, '0(0%)' AS Grade5 ")
            strSQL.AppendLine(ConfigurationManager.AppSettings("eHRMSDBDES").ToString)
            strSQL.AppendLine(" select * From (")
            strSQL.AppendLine(" Select G.GradeYear,G.GradeSeq,G.CompID,Year(P.EmpDate) as Type,cast(Year(P.EmpDate) as varchar(4)) + '新員' as TypeName")
            '特優
            strSQL.AppendLine(" ,Grade9=cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and G1.UpOrganID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='9'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID and G1.UpOrganID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='9'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            Else
                strSQL.AppendLine(" and G1.GradeDeptID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='9'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID and G1.GradeDeptID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='9'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            End If
            '優
            strSQL.AppendLine(" ,Grade1=cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and G1.UpOrganID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='1'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID and G1.UpOrganID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='1'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            Else
                strSQL.AppendLine(" and G1.GradeDeptID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='1'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID and G1.GradeDeptID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='1'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            End If
            '甲上
            strSQL.AppendLine(" ,Grade6=cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and G1.UpOrganID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='6'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID and G1.UpOrganID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='6'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            Else
                strSQL.AppendLine(" and G1.GradeDeptID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='6'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID and G1.GradeDeptID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='6'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            End If
            '甲
            strSQL.AppendLine(" ,Grade2=cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and G1.UpOrganID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='2'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID and G1.UpOrganID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='2'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            Else
                strSQL.AppendLine(" and G1.GradeDeptID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='2'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID and G1.GradeDeptID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='2'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            End If
            '甲下
            strSQL.AppendLine(" ,Grade7=cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and G1.UpOrganID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='7'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID and G1.UpOrganID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='7'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            Else
                strSQL.AppendLine(" and G1.GradeDeptID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='7'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID and G1.GradeDeptID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='7'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            End If
            '乙
            strSQL.AppendLine(" ,Grade3=cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and G1.UpOrganID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='3'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID and G1.UpOrganID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='3'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            Else
                strSQL.AppendLine(" and G1.GradeDeptID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='3'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID and G1.GradeDeptID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='3'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            End If
            '丙
            strSQL.AppendLine(" ,Grade4=cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and G1.UpOrganID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='4'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID and G1.UpOrganID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='4'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            Else
                strSQL.AppendLine(" and G1.GradeDeptID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='4'),0) as varchar(3))")
                strSQL.AppendLine("	+ '(' + cast(round((cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID and G1.GradeDeptID=" & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate)=Year(P.EmpDate) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='4'),0) as float)/cast(Count(*) as float)) *100,0) as varchar(3)) + '%)' ")
            End If
            '合計
            strSQL.AppendLine(" ,Count(*) as TotalCnt")
            strSQL.AppendLine(" from GradeBase G Left Join Personal P On G.CompID=P.CompID And G.EmpID=P.EmpID")
            strSQL.AppendLine(" Where G.CompID =" & Bsp.Utility.Quote(hidCompID.Value))
            strSQL.AppendLine(" And G.GradeYear =" & hidGradeYear.Value)
            strSQL.AppendLine(" And G.GradeSeq =" & hidGradeSeq.Value)
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and G.UpOrganID =" & Bsp.Utility.Quote(hidApplyID.Value))
            Else
                strSQL.AppendLine(" and G.GradeDeptID =" & Bsp.Utility.Quote(hidApplyID.Value))
            End If
            strSQL.AppendLine(" And isnull(CONVERT(char(1), DecryptByKey(G." & GradeField & ")) ,'') <>''")
            strSQL.AppendLine(" and Year(P.EmpDate) in (G.GradeYear,GradeYear-1) ")
            strSQL.AppendLine(" group by G.GradeYear,G.GradeSeq,G.CompID,Year(P.EmpDate)")
            strSQL.AppendLine(" union")
            strSQL.AppendLine(" Select G.GradeYear,G.GradeSeq,G.CompID,'9999' as 'Type','合計(人)' as 'TypeName'")
            '特優合計
            strSQL.AppendLine(" ,Grade9=cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate) in (G.GradeYear,GradeYear-1) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='9'),0) as varchar(3))")
            Else
                strSQL.AppendLine(" and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate) in (G.GradeYear,GradeYear-1) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='9'),0) as varchar(3))")
            End If
            '優合計
            strSQL.AppendLine(" ,Grade1=cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate) in (G.GradeYear,GradeYear-1) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='1'),0) as varchar(3))")
            Else
                strSQL.AppendLine(" and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate) in (G.GradeYear,GradeYear-1) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='1'),0) as varchar(3))")
            End If
            '甲上合計
            strSQL.AppendLine(" ,Grade6=cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate) in (G.GradeYear,GradeYear-1) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='6'),0) as varchar(3))")
            Else
                strSQL.AppendLine(" and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate) in (G.GradeYear,GradeYear-1) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='6'),0) as varchar(3))")
            End If
            '甲合計
            strSQL.AppendLine(" ,Grade2=cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate) in (G.GradeYear,GradeYear-1) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='2'),0) as varchar(3))")
            Else
                strSQL.AppendLine(" and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate) in (G.GradeYear,GradeYear-1) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='2'),0) as varchar(3))")
            End If
            '甲下合計
            strSQL.AppendLine(" ,Grade7=cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate) in (G.GradeYear,GradeYear-1) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='7'),0) as varchar(3))")
            Else
                strSQL.AppendLine(" and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate) in (G.GradeYear,GradeYear-1) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='7'),0) as varchar(3))")
            End If
            '乙合計
            strSQL.AppendLine(" ,Grade3=cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate) in (G.GradeYear,GradeYear-1) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='3'),0) as varchar(3))")
            Else
                strSQL.AppendLine(" and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate) in (G.GradeYear,GradeYear-1) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='3'),0) as varchar(3))")
            End If
            '丙合計
            strSQL.AppendLine(" ,Grade4=cast(isnull((select count(*) From GradeBase G1 Left Join Personal P1 On G1.CompID=P1.CompID And G1.EmpID=P1.EmpID Where GradeYear=G.GradeYear and GradeSeq=G.GradeSeq and G1.CompID=G.CompID")
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and UpOrganID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate) in (G.GradeYear,GradeYear-1) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='4'),0) as varchar(3))")
            Else
                strSQL.AppendLine(" and GradeDeptID = " & Bsp.Utility.Quote(hidApplyID.Value) & " and Year(P1.EmpDate) in (G.GradeYear,GradeYear-1) and isnull(CONVERT(char(1), DecryptByKey(" & GradeField & ")) ,'')='4'),0) as varchar(3))")
            End If
            '合計
            strSQL.AppendLine(" ,Count(*) as TotalCnt")
            strSQL.AppendLine(" from GradeBase G Left Join Personal P On G.CompID=P.CompID And G.EmpID=P.EmpID ")
            strSQL.AppendLine(" Where G.CompID =" & Bsp.Utility.Quote(hidCompID.Value))
            strSQL.AppendLine(" And G.GradeYear =" & hidGradeYear.Value)
            strSQL.AppendLine(" And G.GradeSeq =" & hidGradeSeq.Value)
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and G.UpOrganID =" & Bsp.Utility.Quote(hidApplyID.Value))
            Else
                strSQL.AppendLine(" and G.GradeDeptID =" & Bsp.Utility.Quote(hidApplyID.Value))
            End If
            strSQL.AppendLine(" And isnull(CONVERT(char(1), DecryptByKey(G." & GradeField & ")) ,'') <>''")
            strSQL.AppendLine(" and Year(P.EmpDate) in (G.GradeYear,GradeYear-1) ")
            strSQL.AppendLine(" group by G.GradeYear,G.GradeSeq,G.CompID ")
            strSQL.AppendLine(" ) S")
            strSQL.AppendLine(" Order By Type")

            lblTitle.Text = "新員統計(依區/處)"
            Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
                gvMain1.DataSource = dt
                gvMain1.DataBind()
            End Using
        End If
    End Sub
    Protected Sub gvMain_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvMain1.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            Select Case ddltype.SelectedValue
                Case "1"
                    e.Row.Cells(1).Text = "職系"
                    e.Row.Cells(2).Text = "特優<br>人數(比例)"
                    e.Row.Cells(3).Text = "優<br>人數(比例)"
                    e.Row.Cells(4).Text = "甲上<br>人數(比例)"
                    e.Row.Cells(5).Text = "甲<br>人數(比例)"
                    e.Row.Cells(6).Text = "甲下<br>人數(比例)"
                    e.Row.Cells(7).Text = "乙<br>人數(比例)"
                    e.Row.Cells(8).Text = "丙<br>人數(比例)"
                Case "2"
                    e.Row.Cells(2).Text = "特優<br>人數(比例)"
                    e.Row.Cells(3).Text = "優<br>人數(比例)"
                    e.Row.Cells(4).Text = "甲上<br>人數(比例)"
                    e.Row.Cells(5).Text = "甲<br>人數(比例)"
                    e.Row.Cells(6).Text = "甲下<br>人數(比例)"
                    e.Row.Cells(7).Text = "乙<br>人數(比例)"
                    e.Row.Cells(8).Text = "丙<br>人數(比例)"
                Case "3"
                    e.Row.Cells(1).Text = "新員"
                    e.Row.Cells(2).Text = "特優<br>人數(比例)"
                    e.Row.Cells(3).Text = "優<br>人數(比例)"
                    e.Row.Cells(4).Text = "甲上<br>人數(比例)"
                    e.Row.Cells(5).Text = "甲<br>人數(比例)"
                    e.Row.Cells(6).Text = "甲下<br>人數(比例)"
                    e.Row.Cells(7).Text = "乙<br>人數(比例)"
                    e.Row.Cells(8).Text = "丙<br>人數(比例)"
                Case "4"
                    e.Row.Cells(1).Text = "新員"
                    e.Row.Cells(2).Text = "特優<br>人數(比例)"
                    e.Row.Cells(3).Text = "優<br>人數(比例)"
                    e.Row.Cells(4).Text = "甲上<br>人數(比例)"
                    e.Row.Cells(5).Text = "甲<br>人數(比例)"
                    e.Row.Cells(6).Text = "甲下<br>人數(比例)"
                    e.Row.Cells(7).Text = "乙<br>人數(比例)"
                    e.Row.Cells(8).Text = "丙<br>人數(比例)"
            End Select
        ElseIf e.Row.RowType = DataControlRowType.DataRow Then
            Dim tmpLbtn As New LinkButton
            tmpLbtn = e.Row.Cells(0).FindControl("lbtnDetail")
            If ddltype.SelectedValue = "2" Then
                tmpLbtn.Visible = False
            End If
            Select Case DirectCast(gvMain1.DataSource, DataTable).Rows(e.Row.RowIndex)("Type").ToString()
                Case "ZZZZZZ"
                    tmpLbtn.Visible = False
                Case "9999"
                    tmpLbtn.Visible = False
            End Select

        End If
    End Sub

    '2015/11/11 Add gvMain1明細
    Protected Sub gvMain_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMain1.RowCommand
        Dim strSQL As New StringBuilder()
        Dim GradeField As String = ""
        Select Case ViewState.Item("Status")
            Case "4"
                GradeField = "GradeHR"
            Case "5"
                GradeField = "Grade2"
            Case Else
                GradeField = "Grade"    '20160722 wei modify
        End Select
        If e.CommandName = "Detail" Then
            pcMain.Visible = True
            gvMain2.Visible = True
            '單位名稱/員工編號/姓名/職等/職位/排序/考績
            '測試用
            'strSQL.AppendLine("Select 'OO分行' AS OrganName, '666666' AS EmpID, '皮卡丘' AS NameN, 'A02' AS RankID, 'lalala' AS Position, '2' AS Sort, 'B' AS Grade ")
            strSQL.AppendLine(ConfigurationManager.AppSettings("eHRMSDBDES").ToString)
            strSQL.AppendLine(" Select G.CompID,O.OrganName,G.EmpID,P.NameN,P.RankID,PO.Remark as Position")
            If ddltype.SelectedValue = "3" Then
                strSQL.AppendLine(" ,G.GradeOrderDept as GradeOrder")
                strSQL.AppendLine(" ,Case When isnull(CONVERT(char(1), DecryptByKey(G.GradeDept)) ,'')='1' Then '優' ")
                strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeDept)) ,'')='2' Then '甲' ")
                strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeDept)) ,'')='3' Then '乙' ")
                strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeDept)) ,'')='4' Then '丙' ")
                strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeDept)) ,'')='6' Then '甲上' ")
                strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeDept)) ,'')='7' Then '甲下' ")
                strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G.GradeDept)) ,'')='9' Then '特優' ")
                strSQL.AppendLine(" Else '' End as 'Grade' ")
            Else
                strSQL.AppendLine(" ,G.GradeOrder as GradeOrder")
                strSQL.AppendLine(" ,Case When isnull(CONVERT(char(1), DecryptByKey(G." & GradeField & ")) ,'')='1' Then '優' ")
                strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G." & GradeField & ")) ,'')='2' Then '甲' ")
                strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G." & GradeField & ")) ,'')='3' Then '乙' ")
                strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G." & GradeField & ")) ,'')='4' Then '丙' ")
                strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G." & GradeField & ")) ,'')='6' Then '甲上' ")
                strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G." & GradeField & ")) ,'')='7' Then '甲下' ")
                strSQL.AppendLine(" When isnull(CONVERT(char(1), DecryptByKey(G." & GradeField & ")) ,'')='9' Then '特優' ")
                strSQL.AppendLine(" Else '' End as 'Grade' ")
            End If
            strSQL.AppendLine(" from GradeBase G Left Join Personal P On G.CompID=P.CompID And G.EmpID=P.EmpID ")
            strSQL.AppendLine(" Left Join Organization O On G.CompID=O.CompID And G.GradeDeptID=O.OrganID")
            If ddltype.SelectedValue = "3" Or ddltype.SelectedValue = "4" Then
                strSQL.AppendLine(" Left Join EmpPosition EP On G.CompID=EP.CompID And G.EmpID=EP.EmpID and EP.PrincipalFlag='1'")
                strSQL.AppendLine(" Left Join Position PO On EP.CompID=PO.CompID And EP.PositionID=PO.PositionID")
            Else
                strSQL.AppendLine(" Left Join Position PO On G.CompID=PO.CompID And G.PositionID=PO.PositionID")
                strSQL.AppendLine(" Left Join GradePosition GP On G.CompID=GP.CompID And G.PositionID=GP.PositionID")
            End If
            strSQL.AppendLine(" Where G.CompID =" & Bsp.Utility.Quote(hidCompID.Value))
            strSQL.AppendLine(" And G.GradeYear =" & hidGradeYear.Value)
            strSQL.AppendLine(" And G.GradeSeq =" & hidGradeSeq.Value)
            If hidMainFlag.Value = "2" And hidDeptEX.Value = "N" Then
                strSQL.AppendLine(" and G.UpOrganID =" & Bsp.Utility.Quote(hidApplyID.Value))
            Else
                strSQL.AppendLine(" and G.GradeDeptID =" & Bsp.Utility.Quote(hidApplyID.Value))
            End If

            If ddltype.SelectedValue = "3" Or ddltype.SelectedValue = "4" Then
                strSQL.AppendLine(" and Year(P.EmpDate) = " & gvMain1.DataKeys(selectedRow(gvMain1))("Type").ToString())
                If ddltype.SelectedValue = "3" Then
                    strSQL.AppendLine(" And isnull(CONVERT(char(1), DecryptByKey(G.GradeDept)) ,'') <>''")
                End If
            Else
                strSQL.AppendLine(" and Isnull(GP.PositionTypeID,5) = " & gvMain1.DataKeys(selectedRow(gvMain1))("Type").ToString())
                strSQL.AppendLine(" And isnull(CONVERT(char(1), DecryptByKey(G." & GradeField & ")) ,'') <>''")
            End If
            If ddltype.SelectedValue = "3" Then
                strSQL.AppendLine(" Order By G.GradeOrderDept,G.EmpID,EP.PositionID ")
            ElseIf ddltype.SelectedValue = "4" Then
                strSQL.AppendLine(" Order By G.GradeOrderDept,G.EmpID,EP.PositionID ")
            Else
                strSQL.AppendLine(" Order By GP.PositionTypeID,G.GradeOrder,G.EmpID,G.PositionID ")
            End If

            Using dt As DataTable = Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
                gvMain2.DataSource = dt
                gvMain2.DataBind()
            End Using
        End If


    End Sub
    
End Class
