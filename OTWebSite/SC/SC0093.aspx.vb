Imports System.Data

Partial Class SC_SC0093
    Inherits PageBase
    Private dtData As DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            If Session.Item("ExportObject") Is Nothing Then
                Bsp.Utility.ShowMessage(Me, "無法找到物件！")
                Return
            End If
            dtData = CType(Session("ExportObject"), DataTable)
            Session.Remove("ExportObject")
            Randomize()
            Dim strFileName As String = Bsp.Utility.GetNewFileName("")
            Response.Clear()
            Response.ClearHeaders()
            Response.ClearContent()

            Response.Expires = 0
            Response.Buffer = True
            'Response.Charset = "big5"
            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("content-disposition", "attachment; filename=" & strFileName & ".xls")

            Dim dgData As New DataGrid
            AddHandler dgData.ItemDataBound, AddressOf ItemDataBound

            Dim oStringWriter As System.IO.StringWriter = New System.IO.StringWriter

            Dim oHtmlTextWriter As System.Web.UI.HtmlTextWriter = New System.Web.UI.HtmlTextWriter(oStringWriter)

            With dgData
                .ID = "dgData"
                .HeaderStyle.BackColor = System.Drawing.Color.FromArgb(128, 169, 210)
                .HeaderStyle.ForeColor = System.Drawing.Color.White
                .Font.Name = "細明體"
                .ItemStyle.Font.Name = "細明體"
                .DataSource = dtData
                .DataBind()
                .RenderControl(oHtmlTextWriter)
            End With

            'Dim strHtmlHeader As String = "<Head></Head><body>"
            'Dim strHtmlHeader As String = "<Head><meta http-equiv=Content-Type content=""text/html; charset=Big5""></Head><body>"
            Dim strHtmlHeader As String = "<Head><meta http-equiv=Content-Type content=""text/html; charset=UTF-8""></Head><body>"
            Dim strHtmlFooter As String = "</body>"

            Response.Write(strHtmlHeader & oStringWriter.ToString() & strHtmlFooter)
            Response.End()
        End If
    End Sub

    Private Sub ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim intLoop As Integer

            For intLoop = 0 To dtData.Columns.Count - 1
                If dtData.Columns(intLoop).DataType Is System.Type.GetType("System.String") Then
                    If IsNumeric(e.Item.Cells(intLoop).Text) Then
                        e.Item.Cells(intLoop).Style.Add("mso-style-parent", "style0")
                        e.Item.Cells(intLoop).Style.Add("mso-number-format", "'\@'")
                        'e.Item.Cells(intLoop).Text = e.Item.Cells(intLoop).Text & "&nbsp;"
                    End If
                End If
            Next
        End If
    End Sub
End Class
