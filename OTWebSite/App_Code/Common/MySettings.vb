'*********************************************************************************
'功能說明：web.config內的參數,或系統用到的參數
'建立人員：Chung
'建立日期：2011.05.12
'*********************************************************************************
Imports Microsoft.VisualBasic

Namespace Bsp
    Public Class MySettings
        '提供Client 端的IP
        Public Shared ReadOnly Property ClientIP() As String
            Get
                Return HttpContext.Current.Request.UserHostAddress
            End Get
        End Property

        '提供佈告欄存放路徑
        Public Shared ReadOnly Property BillboardPath() As String
            Get
                Return Web.VirtualPathUtility.ToAbsolute(Bsp.Utility.getAppSetting("BillboardPath").ToString())
            End Get
        End Property

        ''' <summary>
        ''' 檢查目前系統狀態是否關閉中
        ''' </summary>
        ''' <value></value>
        ''' <returns>Y:關閉中 N:開放中</returns>
        ''' <remarks></remarks>
        Public Shared ReadOnly Property IsSystemClose() As String
            Get
                Dim strStatus As String = Bsp.Utility.IsStringNull(Bsp.DB.ExecuteScalar("Select CloseFlag From SC_Parameter"), "Y")

                Return strStatus
            End Get
        End Property

        '登入頁面
        Public Shared ReadOnly Property LoginPage() As String
            Get
                Return Bsp.Utility.getAppSetting("LoginPage").ToString()
            End Get
        End Property

        '訊息頁
        Public Shared ReadOnly Property MessagePage() As String
            Get
                Return Web.VirtualPathUtility.ToAbsolute(Bsp.Utility.getAppSetting("MessagePage").ToString())
            End Get
        End Property

        '郵件通知標頭
        Public Shared Function FlowMailHeader() As String
            Return Bsp.Utility.getAppSetting("FlowMailHeader").ToString()
        End Function

        '主頁面
        Public Shared ReadOnly Property StartPage() As String
            Get
                Return Web.VirtualPathUtility.ToAbsolute(Bsp.Utility.getAppSetting("StartPage").ToString())
            End Get
        End Property

        '暫存檔路徑
        Public Shared ReadOnly Property TempPath() As String
            Get
                Return Web.VirtualPathUtility.ToAbsolute(Bsp.Utility.getAppSetting("TempPath").ToString())
            End Get
        End Property

        'JCIC產檔路徑
        Public Shared ReadOnly Property JCIC_FilePath() As String
            Get
                Return System.Configuration.ConfigurationManager.AppSettings("JCIC_FilePath").ToString()
            End Get
        End Property

        '上傳檔案最大size, 單位KB
        Public Shared ReadOnly Property UploadMaxFileSize() As Integer
            Get
                Try
                    Return CInt(Bsp.Utility.getAppSetting("UploadMaxFileSize"))
                Catch ex As Exception
                    Return 512
                End Try
            End Get
        End Property

        '不限制上傳size的檔案類型清單
        Public Shared ReadOnly Property UploadUnlimitSizeFileType() As String
            Get
                Return Bsp.Utility.getAppSetting("UploadUnlimitSizeFileType").ToString()
            End Get
        End Property

        '允許上傳檔案類型
        Public Shared ReadOnly Property UploadAllowFileType() As String
            Get
                Return Bsp.Utility.getAppSetting("UploadAllowFileType").ToString()
            End Get
        End Property

        '附件上傳路徑
        Public Shared ReadOnly Property UploadPath() As String
            Get
                Return Web.VirtualPathUtility.ToAbsolute(Bsp.Utility.getAppSetting("UploadPath").ToString())
            End Get
        End Property

        Public Shared Function ProductionFlag() As String
            Dim strSQL As New StringBuilder()

            strSQL.AppendLine("Select ProductionFlag From SC_Parameter")
            Return Bsp.Utility.IsStringNull(Bsp.DB.ExecuteScalar(strSQL.ToString()), "0")
        End Function

        '待辦事項頁
        Public Shared Function ToDoListPage() As String
            Return Bsp.Utility.getAppSetting("ToDoListPage").ToString()
        End Function

        ''' <summary>
        ''' UserProfile Session名稱
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function UserProfileSessionName() As String
            Return Bsp.Utility.getAppSetting("UserProfileSessionName").ToString()
        End Function

        ''' <summary>
        ''' 流程處理程式網頁
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function FlowRedirectPage() As String
            Return Bsp.Utility.getAppSetting("FlowRedirectPage").ToString()
        End Function

        ''' <summary>
        ''' 每次查詢最大顯示筆數
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks>避免一次性撈取資料過多，網頁速度受限</remarks>
        Public Shared ReadOnly Property ShowMaxRecords() As Integer
            Get
                Return 200
            End Get
        End Property

        'eCredit核貸書PDF網址
        Public Shared ReadOnly Property ALWebURL() As String
            Get
                Return Utility.getAppSetting("ALWebURL").ToString()
            End Get
        End Property

        '核貸書備份路徑
        Public Shared ReadOnly Property ALBackupPath() As String
            Get
                Return Utility.getAppSetting("ALBackupPath").ToString()
            End Get
        End Property

        ''' <summary>
        ''' 取得最大捞取笔数上限
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function QueryLimit() As String
            If HttpContext.Current.Application.Item("QueryLimit") Is Nothing OrElse _
                HttpContext.Current.Application.Item("QueryLimit").ToString() = "" Then
                'Dim objQR As New QR()
                Dim strLimit As String = "200"

                'strLimit = objQR.GetQryLimit()

                If strLimit Is Nothing OrElse strLimit = "" Then
                    strLimit = "200"
                End If
                HttpContext.Current.Application.Item("QueryLimit") = String.Format("{0},{1}", Date.Now.ToString("yyyy/MM/dd HH:mm:ss"), strLimit)
                Return strLimit
            Else
                Dim aryLimit() As String = HttpContext.Current.Application.Item("QueryLimit").ToString().Split(",")

                Try
                    If DateDiff(DateInterval.Second, Convert.ToDateTime(aryLimit(0)), Date.Now) > 3600 Then
                        HttpContext.Current.Application.Remove("QueryLimit")
                        Return QueryLimit()
                    Else
                        Return aryLimit(1)
                    End If
                Catch ex As Exception
                    HttpContext.Current.Application.Remove("QueryLimit")
                    Return QueryLimit()
                End Try
            End If
        End Function

    End Class

End Namespace
