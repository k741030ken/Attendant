'****************************************************
'功能說明：打卡參數設定
'建立人員：John Lin
'建立日期：2017.04.07
'****************************************************
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.Common

Public Class PO1
#Region "PO1000 打卡參數設定-新增"

    ''' <summary>
    ''' 新增
    ''' </summary>
    ''' <param name="bePunchPara">畫面資料</param>
    ''' <returns>True or False</returns>
    ''' <remarks></remarks>
    Public Function AddPunchParaSetting(ByVal bePunchPara As bePunchPara.Row) As Boolean
        Dim bsPunchPara As New bePunchPara.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("AattendantDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsPunchPara.Insert(bePunchPara, tran) = 0 Then Return False
                tran.Commit()
            Catch ex As Exception
                tran.Rollback()
                Throw
            Finally
                If tran IsNot Nothing Then tran.Dispose()
            End Try
        End Using
        Return True
    End Function
#End Region

#Region "PO1000 打卡參數設定-修改"

    ''' <summary>
    ''' 修改
    ''' </summary>
    ''' <param name="bePunchPara">畫面資料</param>
    ''' <returns>True or False</returns>
    ''' <remarks></remarks>
    Public Function UpdateWorkSiteSetting(ByVal bePunchPara As bePunchPara.Row) As Boolean
        Dim bsPunchPara As New bePunchPara.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("AattendantDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsPunchPara.Update(bePunchPara, tran) = 0 Then Return False
                tran.Commit()
            Catch ex As Exception
                tran.Rollback()
                Throw
            Finally
                If tran IsNot Nothing Then tran.Dispose()
            End Try
        End Using
        Return True
    End Function
#End Region
End Class
