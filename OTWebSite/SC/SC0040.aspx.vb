'****************************************************
'功能說明：佈告欄顯示
'建立人員：Chung
'建立日期：2011.05.13
'****************************************************
Imports System.Data

Partial Class SC_SC0040
    Inherits CommonBase

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            MyBase.ClearSession()
            subLoadSCBillboardData()
        End If
    End Sub

    Private Sub subLoadSCBillboardData()
        Dim objSC As New SC()
        Dim Cell As HtmlTableCell
        Dim Row As HtmlTableRow

        Using dt As DataTable = objSC.GetBillboardInfo("", "", "Kind, Seq, ImportantFlag, Title, ValidFrom, Content, DetailFlag, FileName", _
                                                       "And ValidTo > Getdate() And ValidFrom < Getdate() Order by ValidFrom desc")
            For Each dr As DataRow In dt.Rows
                'Title寫入
                Row = New HtmlTableRow()
                Cell = New HtmlTableCell()
                Cell.Align = "left"
                Cell.ColSpan = 2
                Cell.InnerHtml = "<img border='0' src='../images/line.gif'>"
                Row.Cells.Add(Cell)
                tblBillboard.Rows.Add(Row)

                Row = New HtmlTableRow()
                'Row.Style.Item("background-color") = "#eaf4f9"

                Cell = New HtmlTableCell()
                Cell.Align = "left"
                Cell.Width = "4%"
                Cell.InnerHtml = "<img border='0' src='../images/dot_red.gif'>"
                Row.Cells.Add(Cell)

                Cell = New HtmlTableCell()
                Cell.Align = "left"
                Cell.Width = "96%"
                If dr.Item("ImportantFlag").ToString() = "1" Then
                    Cell.InnerHtml = "<font style='font-size:11pt;color:red;font-weight:bolder;border-bottom:1px solid silver'>" & dr.Item("Title").ToString & "</font><font class=""f9"" style='color:red'>" & " (" & CType(dr.Item("ValidFrom"), DateTime).ToString("yyyy/MM/dd") & ")</font>"
                Else
                    Cell.InnerHtml = "<font style='font-size:11pt;color:black;font-weight:bolder;border-bottom:1px solid silver'>" & dr.Item("Title").ToString & "</font><font class=""f9"" style='color:black'>" & " (" & CType(dr.Item("ValidFrom"), DateTime).ToString("yyyy/MM/dd") & ")</font>"
                End If
                Row.Cells.Add(Cell)

                tblBillboard.Rows.Add(Row)

                'Content
                Row = New HtmlTableRow()
                Cell = New HtmlTableCell()
                Cell.Width = "4%"
                Row.Cells.Add(Cell)

                Cell = New HtmlTableCell()
                Cell.ColSpan = "2"
                Cell.Align = "left"
                Cell.Width = "96%"
                Cell.InnerHtml = "<font style='color:dimgray;font-size:12px;line-Height:16px'>" & dr.Item("Content").ToString & "</font>"
                Row.Cells.Add(Cell)
                tblBillboard.Rows.Add(Row)

                '深入說明
                If dr.Item("DetailFlag") = "1" Then
                    Row = New HtmlTableRow()

                    Cell = New HtmlTableCell()
                    Cell.ColSpan = "2"
                    Cell.Align = "right"
                    Cell.Width = "100%"
                    Dim strHrefString As String = Bsp.MySettings.BillboardPath & "/" & dr.Item("FileName").ToString()
                    Cell.InnerHtml = "<font style='color:blue;font-size:12px'><a href='" & strHrefString & "' target='new'> >> 深入說明...</a></font>"
                    Row.Cells.Add(Cell)
                    tblBillboard.Rows.Add(Row)
                End If

                '空白行
                Row = New HtmlTableRow()

                Cell = New HtmlTableCell()
                Cell.ColSpan = "2"
                Cell.Align = "right"
                Cell.Width = "100%"
                Cell.InnerHtml = "&nbsp;"
                Row.Cells.Add(Cell)

                tblBillboard.Rows.Add(Row)
            Next
            If dt.Rows.Count > 0 Then
                Row = New HtmlTableRow()
                Cell = New HtmlTableCell()
                Cell.Align = "left"
                Cell.ColSpan = 2
                Cell.InnerHtml = "<img border='0' src='../images/line.gif'>"
                Row.Cells.Add(Cell)
                tblBillboard.Rows.Add(Row)
            End If
        End Using
    End Sub

End Class
