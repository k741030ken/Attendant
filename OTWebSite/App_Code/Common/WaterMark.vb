Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Drawing

Public Class WaterMark

    Public Shared Function ImageTable(ByVal TempPath As String, ByVal MarkText As String) As DataTable
        Dim objDT As New DataTable
        Dim objRow As DataRow
        objDT.TableName = "Images"
        objDT.Columns.Add("img", System.Type.GetType("System.Byte[]"))
        'Dim imgFileName As String = PubFun.getNewFileName("RPT") & ".jpg"
        'imgFileName = TempPath & "/" & imgFileName
        'GenericWatermark(imgFileName)
        'Dim fs As New FileStream(imgFileName, FileMode.Open)
        'Dim br As New BinaryReader(fs)
        objRow = objDT.NewRow()
        'objRow(0) = br.ReadBytes(br.BaseStream.Length)
        objRow(0) = GenericWatermark(MarkText)
        objDT.Rows.Add(objRow)
        'br = Nothing
        'fs.Close()
        'File.Delete(imgFileName)
        'fs.Dispose()

        'Dim ds As New DataSet
        'ds.Tables.Add(objDT)
        'ds.WriteXmlSchema(XmlFolder + "/Image.xsd")

        Return objDT
    End Function

    Public Shared Function GenericWatermark(ByVal MarkText As String) As Byte()
        Dim image As System.Drawing.Bitmap = New System.Drawing.Bitmap(1900, 2400)
        Dim g As Graphics = Graphics.FromImage(image)
        Dim ms As System.IO.MemoryStream = New System.IO.MemoryStream

        Try
            g.Clear(Color.White)
            Dim font As Font = New System.Drawing.Font("細明體", 72, (FontStyle.Italic Or FontStyle.Bold), GraphicsUnit.Pixel)

            Dim brush As System.Drawing.Drawing2D.LinearGradientBrush = New System.Drawing.Drawing2D.LinearGradientBrush(New Rectangle(0, 0, image.Width, image.Height), Color.LightGray, Color.LightGray, 45.0F, True)
            'g.RotateTransform(angle:=-10)
            g.DrawString(MarkText, font, brush, 70, 100)
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
            Return ms.ToArray()
        Catch ex As Exception
            Return Nothing
        Finally
            g.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' Add and image as the watermark on each page of the source pdf to create a new pdf with watermark
    ''' </summary>
    ''' <param name="sourceFile">the full path to the source pdf</param>
    ''' <param name="outputFile">the full path where the watermarked pdf will be saved to</param>
    ''' <param name="watermarkImage">the full path to the image file to use as the watermark</param>
    ''' <remarks>The watermark image will be align in the center of each page</remarks>
    Public Shared Sub AddWatermarkImage(ByVal sourceFile As String, ByVal outputFile As String, ByVal watermarkImage As String)
        Dim reader As iTextSharp.text.pdf.PdfReader = Nothing
        Dim stamper As iTextSharp.text.pdf.PdfStamper = Nothing
        Dim img As iTextSharp.text.Image = Nothing
        Dim underContent As iTextSharp.text.pdf.PdfContentByte = Nothing
        Dim rect As iTextSharp.text.Rectangle = Nothing
        Dim X, Y As Single
        Dim pageCount As Integer = 0

        Try
            reader = New iTextSharp.text.pdf.PdfReader(sourceFile)
            rect = reader.GetPageSizeWithRotation(1)
            stamper = New iTextSharp.text.pdf.PdfStamper(reader, New System.IO.FileStream(outputFile, IO.FileMode.Create))
            img = iTextSharp.text.Image.GetInstance(watermarkImage)

            If img.Width > rect.Width OrElse img.Height > rect.Height Then
                img.ScaleToFit(rect.Width, rect.Height)
                X = (rect.Width - img.ScaledWidth) / 2
                Y = (rect.Height - img.ScaledHeight) / 2
            Else
                X = (rect.Width - img.Width) / 2
                Y = (rect.Height - img.Height) / 2
            End If

            img.SetAbsolutePosition(X, Y)
            pageCount = reader.NumberOfPages()

            For i As Integer = 1 To pageCount
                underContent = stamper.GetUnderContent(i)
                underContent.AddImage(img)
            Next

            stamper.Close()
            reader.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    ''' <summary>
    ''' Add text as the watermark to each page of the source pdf to create a new pdf with text watermark
    ''' </summary>
    ''' <param name="sourceFile">the full path to the source pdf file</param>
    ''' <param name="outputFile">the full path where the watermarked pdf file will be saved to</param>
    ''' <param name="watermarkText">the string array conntaining the text to use as the watermark. Each element is treated as a line in the watermark</param>
    ''' <param name="watermarkFont">the font to use for the watermark. The default font is HELVETICA</param>
    ''' <param name="watermarkFontSize">the size of the font. The default size is 48</param>
    ''' <param name="watermarkFontColor">the color of the watermark. The default color is blue</param>
    ''' <param name="watermarkFontOpacity">the opacity of the watermark. The default opacity is 0.3</param>
    ''' <param name="watermarkRotation">the rotation in degree of the watermark. The default rotation is 45 degree</param>
    ''' <remarks></remarks>
    Public Shared Sub AddWatermarkText(ByVal sourceFile As String, ByVal outputFile As String, ByVal watermarkText() As String, _
                                       Optional ByVal watermarkFont As iTextSharp.text.pdf.BaseFont = Nothing, _
                                       Optional ByVal watermarkFontSize As Single = 48, _
                                       Optional ByVal watermarkFontColor As iTextSharp.text.BaseColor = Nothing, _
                                       Optional ByVal watermarkFontOpacity As Single = 0.15F, _
                                       Optional ByVal watermarkRotation As Single = 45.0F)

        Dim reader As iTextSharp.text.pdf.PdfReader = Nothing
        Dim stamper As iTextSharp.text.pdf.PdfStamper = Nothing
        Dim gstate As iTextSharp.text.pdf.PdfGState = Nothing
        Dim underContent As iTextSharp.text.pdf.PdfContentByte = Nothing
        Dim rect As iTextSharp.text.Rectangle = Nothing
        Dim currentY As Single = 0.0F
        Dim offset As Single = 0.0F
        Dim pageCount As Integer = 0
        'Dim FontPath As String = Environment.GetFolderPath(Environment.SpecialFolder.System) & "\..\Fonts\mingliu.ttc,0"
        Dim FontPath As String = Environment.GetFolderPath(Environment.SpecialFolder.System) & "\..\Fonts\simhei.ttf"

        'iTextSharp.text.pdf.BaseFont.AddToResourceSearch("iTextAsian.dll")

        Try
            reader = New iTextSharp.text.pdf.PdfReader(sourceFile)
            rect = reader.GetPageSizeWithRotation(1)
            stamper = New iTextSharp.text.pdf.PdfStamper(reader, New System.IO.FileStream(outputFile, IO.FileMode.Create))

            If watermarkFont Is Nothing Then
                watermarkFont = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, _
                                             iTextSharp.text.pdf.BaseFont.CP1252, _
                                             iTextSharp.text.pdf.BaseFont.EMBEDDED)

                'watermarkFont = iTextSharp.text.pdf.BaseFont.CreateFont(FontPath, _
                '                                             iTextSharp.text.pdf.BaseFont.IDENTITY_H, _
                '                                             iTextSharp.text.pdf.BaseFont.EMBEDDED)
      
            End If

            If watermarkFontColor Is Nothing Then
                watermarkFontColor = iTextSharp.text.BaseColor.BLUE
            End If

            gstate = New iTextSharp.text.pdf.PdfGState()
            gstate.FillOpacity = watermarkFontOpacity
            gstate.StrokeOpacity = watermarkFontOpacity
            pageCount = reader.NumberOfPages()
            Dim not_printed As iTextSharp.text.pdf.PdfLayer = New iTextSharp.text.pdf.PdfLayer("not printed", stamper.Writer)

            For i As Integer = 1 To pageCount
                underContent = stamper.GetOverContent(i)
                With underContent
                    .SaveState()
                    .SetGState(gstate)
                    .SetColorFill(watermarkFontColor)
                    .BeginText()
                    .BeginLayer(not_printed)
                    .SetFontAndSize(watermarkFont, watermarkFontSize)
                    .SetTextMatrix(30, 30)

                    If watermarkText.Length > 1 Then
                        currentY = (rect.Height / 2) + ((watermarkFontSize * watermarkText.Length) / 2)
                    Else
                        currentY = (rect.Height / 2)
                    End If

                    For j As Integer = 0 To watermarkText.Length - 1
                        If j > 0 Then
                            offset = (j * watermarkFontSize) + (watermarkFontSize / 4) * j
                        Else
                            offset = 0.0F
                        End If
                        .ShowTextAligned(iTextSharp.text.Element.ALIGN_CENTER, watermarkText(j), rect.Width / 2, currentY - offset, watermarkRotation)
                    Next
                    .EndText()
                    .RestoreState()
                End With
            Next
            stamper.Close()
            reader.Close()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' Add text as the watermark to each page of the source pdf to create a new pdf with text watermark
    ''' </summary>
    ''' <param name="sourceFile">the full path to the source pdf file</param>
    ''' <param name="watermarkText">the string array conntaining the text to use as the watermark. Each element is treated as a line in the watermark</param>
    ''' <param name="watermarkFont">the font to use for the watermark. The default font is HELVETICA</param>
    ''' <param name="watermarkFontSize">the size of the font. The default size is 48</param>
    ''' <param name="watermarkFontColor">the color of the watermark. The default color is blue</param>
    ''' <param name="watermarkFontOpacity">the opacity of the watermark. The default opacity is 0.3</param>
    ''' <param name="watermarkRotation">the rotation in degree of the watermark. The default rotation is 45 degree</param>
    ''' <remarks></remarks>
    Public Shared Sub AddWatermarkText(ByVal sourceFile As String, ByVal watermarkText() As String, _
                                       Optional ByVal watermarkFont As iTextSharp.text.pdf.BaseFont = Nothing, _
                                       Optional ByVal watermarkFontSize As Single = 48, _
                                       Optional ByVal watermarkFontColor As iTextSharp.text.BaseColor = Nothing, _
                                       Optional ByVal watermarkFontOpacity As Single = 0.15F, _
                                       Optional ByVal watermarkRotation As Single = 45.0F)

        Dim outputFile As String = System.IO.Path.Combine(HttpContext.Current.Server.MapPath(Bsp.MySettings.TempPath), String.Format("{0}.pdf", Bsp.Utility.GetNewFileName("RPT")))
        Dim reader As iTextSharp.text.pdf.PdfReader = Nothing
        Dim stamper As iTextSharp.text.pdf.PdfStamper = Nothing
        Dim gstate As iTextSharp.text.pdf.PdfGState = Nothing
        Dim underContent As iTextSharp.text.pdf.PdfContentByte = Nothing
        Dim rect As iTextSharp.text.Rectangle = Nothing
        Dim currentY As Single = 0.0F
        Dim offset As Single = 0.0F
        Dim pageCount As Integer = 0
        'Dim FontPath As String = Environment.GetFolderPath(Environment.SpecialFolder.System) & "\..\Fonts\mingliu.ttc,0"
        Dim FontPath As String = Environment.GetFolderPath(Environment.SpecialFolder.System) & "\..\Fonts\simhei.ttf"

        'iTextSharp.text.pdf.BaseFont.AddToResourceSearch("iTextAsian.dll")

        Try
            reader = New iTextSharp.text.pdf.PdfReader(sourceFile)
            rect = reader.GetPageSizeWithRotation(1)
            stamper = New iTextSharp.text.pdf.PdfStamper(reader, New System.IO.FileStream(outputFile, IO.FileMode.Create))

            If watermarkFont Is Nothing Then
                watermarkFont = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, _
                                                             iTextSharp.text.pdf.BaseFont.CP1252, _
                                                             iTextSharp.text.pdf.BaseFont.EMBEDDED)

                'watermarkFont = iTextSharp.text.pdf.BaseFont.CreateFont(FontPath, _
                '                                             iTextSharp.text.pdf.BaseFont.IDENTITY_H, _
                '                                             iTextSharp.text.pdf.BaseFont.EMBEDDED)
            End If

            If watermarkFontColor Is Nothing Then
                watermarkFontColor = iTextSharp.text.BaseColor.BLUE
            End If

            gstate = New iTextSharp.text.pdf.PdfGState()
            gstate.FillOpacity = watermarkFontOpacity
            gstate.StrokeOpacity = watermarkFontOpacity
            pageCount = reader.NumberOfPages()
            Dim not_printed As iTextSharp.text.pdf.PdfLayer = New iTextSharp.text.pdf.PdfLayer("not printed", stamper.Writer)

            For i As Integer = 1 To pageCount
                underContent = stamper.GetOverContent(i)
                With underContent
                    .SaveState()
                    .SetGState(gstate)
                    .SetColorFill(watermarkFontColor)
                    .BeginText()
                    .BeginLayer(not_printed)
                    .SetFontAndSize(watermarkFont, watermarkFontSize)
                    .SetTextMatrix(30, 30)

                    If watermarkText.Length > 1 Then
                        currentY = (rect.Height / 2) + ((watermarkFontSize * watermarkText.Length) / 2)
                    Else
                        currentY = (rect.Height / 2)
                    End If

                    For j As Integer = 0 To watermarkText.Length - 1
                        If j > 0 Then
                            offset = (j * watermarkFontSize) + (watermarkFontSize / 4) * j
                        Else
                            offset = 0.0F
                        End If
                        .ShowTextAligned(iTextSharp.text.Element.ALIGN_CENTER, watermarkText(j), rect.Width / 2, currentY - offset, watermarkRotation)
                    Next
                    .EndText()
                    .RestoreState()
                End With
            Next
            stamper.Close()
            reader.Close()

            '回復名稱
            Try
                System.IO.File.Delete(sourceFile)
                System.IO.File.Copy(outputFile, sourceFile)
                System.IO.File.Delete(outputFile)
            Catch ex As Exception

            End Try
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Sub AddWatermarkTextForALStatusName(ByVal sourceFile As String, ByVal outputFile As String, ByVal watermarkText() As String, _
                               Optional ByVal watermarkFont As iTextSharp.text.pdf.BaseFont = Nothing, _
                               Optional ByVal watermarkFontSize As Single = 16, _
                               Optional ByVal watermarkFontColor As iTextSharp.text.BaseColor = Nothing, _
                               Optional ByVal watermarkFontOpacity As Single = 1.0F, _
                               Optional ByVal watermarkRotation As Single = 0.0F)

        Dim reader As iTextSharp.text.pdf.PdfReader = Nothing
        Dim stamper As iTextSharp.text.pdf.PdfStamper = Nothing
        Dim gstate As iTextSharp.text.pdf.PdfGState = Nothing
        Dim underContent As iTextSharp.text.pdf.PdfContentByte = Nothing
        Dim rect As iTextSharp.text.Rectangle = Nothing
        Dim currentY As Single = 0.0F
        Dim offset As Single = 0.0F
        Dim pageCount As Integer = 0
        'Dim FontPath As String = Environment.GetFolderPath(Environment.SpecialFolder.System) & "\..\Fonts\mingliu.ttc,0"
        Dim FontPath As String = Environment.GetFolderPath(Environment.SpecialFolder.System) & "\..\Fonts\simhei.ttf"

        Try
            reader = New iTextSharp.text.pdf.PdfReader(sourceFile)
            rect = reader.GetPageSizeWithRotation(1)
            stamper = New iTextSharp.text.pdf.PdfStamper(reader, New System.IO.FileStream(outputFile, IO.FileMode.Create))

            If watermarkFont Is Nothing Then
                watermarkFont = iTextSharp.text.pdf.BaseFont.CreateFont(iTextSharp.text.pdf.BaseFont.HELVETICA_BOLD, _
                                             iTextSharp.text.pdf.BaseFont.CP1252, _
                                             iTextSharp.text.pdf.BaseFont.EMBEDDED)

                'watermarkFont = iTextSharp.text.pdf.BaseFont.CreateFont(FontPath, _
                '                                             iTextSharp.text.pdf.BaseFont.IDENTITY_H, _
                '                                             iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED)
            End If

            If watermarkFontColor Is Nothing Then
                watermarkFontColor = iTextSharp.text.BaseColor.BLACK
            End If

            gstate = New iTextSharp.text.pdf.PdfGState()
            gstate.FillOpacity = watermarkFontOpacity
            gstate.StrokeOpacity = watermarkFontOpacity
            pageCount = reader.NumberOfPages()

            For i As Integer = 1 To pageCount
                underContent = stamper.GetOverContent(i)
                With underContent
                    .SaveState()
                    .SetGState(gstate)
                    .SetColorFill(watermarkFontColor)
                    .BeginText()
                    .SetFontAndSize(watermarkFont, watermarkFontSize)
                    .SetTextMatrix(30, 30)

                    If watermarkText.Length > 1 Then
                        currentY = (rect.Height / 2) + ((watermarkFontSize * watermarkText.Length) / 2)
                    Else
                        currentY = rect.Height ''(rect.Height / 2)
                    End If

                    For j As Integer = 0 To watermarkText.Length - 1
                        If j > 0 Then
                            offset = (j * watermarkFontSize) + (watermarkFontSize / 4) * j
                        Else
                            offset = 0.0F
                        End If
                        ''.ShowTextAligned(iTextSharp.text.Element.ALIGN_CENTER, watermarkText(j), rect.Width / 2, currentY - offset, watermarkRotation)
                        .ShowTextAligned(iTextSharp.text.Element.ALIGN_RIGHT, watermarkText(j), rect.Width - 23, currentY - 36, watermarkRotation)
                    Next
                    .EndText()
                    .RestoreState()
                End With
            Next
            stamper.Close()
            reader.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class
