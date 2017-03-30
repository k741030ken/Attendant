Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class MyDB
    Public Shared Function gfunGetECConnection(ByVal strConnectionString As String) As SqlConnection
        If strConnectionString = "PD" Then
            'strConnectionString = Bsp.Utility.getAppSetting("DB_CC")
            strConnectionString = ConfigurationManager.ConnectionStrings("DB_EL").ConnectionString
        End If
        Return New SqlConnection(strConnectionString)
    End Function

    Public Shared Function gfunExecuteQuery(ByVal strSQL As String, ByVal cn As SqlClient.SqlConnection) As DataSet
        Dim adoCmd As New SqlCommand(strSQL, cn)
        Dim dsData As New DataSet

        adoCmd.CommandType = CommandType.Text
        adoCmd.CommandTimeout = 90000

        Try
            Dim daData As New SqlClient.SqlDataAdapter(adoCmd)
            daData.Fill(dsData)
        Catch sqlex As SqlClient.SqlException
            Dim strErrMsg As String = ""
            Dim objErr As Object

            For Each objErr In sqlex.Errors
                strErrMsg = String.Format("{0};{1}", strErrMsg, objErr.Message.ToString())
            Next
            If Left(strErrMsg, 1) = ";" Then
                strErrMsg = Mid(strErrMsg, 2)
            End If
            Throw New Exception(strErrMsg)
        Catch ex As Exception
            Throw ex
        End Try

        Return dsData
    End Function

    Public Shared Function gfunExecAccessSQL(ByVal strSQL As String, ByRef Paras() As SqlClient.SqlParameter, ByVal cn As SqlClient.SqlConnection) As Integer
        Dim adoCmd As New SqlClient.SqlCommand(strSQL, cn)
        Dim intAffects As Integer = 0

        If cn.State = ConnectionState.Closed Then cn.Open()
        With adoCmd
            .CommandType = CommandType.Text
            .CommandTimeout = 90000

            For Each obj As SqlParameter In Paras
                .Parameters.Add(obj)
            Next

            Try
                intAffects = .ExecuteNonQuery()
            Catch sqlex As SqlClient.SqlException
                Dim strErrMsg As String = ""

                For Each objErr As SqlError In sqlex.Errors
                    strErrMsg = String.Format("{0};{1}", strErrMsg, objErr.Message.ToString())
                Next
                If Left(strErrMsg, 1) = ";" Then
                    strErrMsg = Mid(strErrMsg, 2)
                End If
                Throw New Exception(strErrMsg)
            Catch ex As Exception
                Throw ex
            End Try

        End With

        Return intAffects
    End Function

    Public Shared Function gfunExecuteNonQuery(ByVal strSQL As String, ByRef Txn As SqlClient.SqlTransaction) As Integer
        Dim adoCmd As New SqlClient.SqlCommand(strSQL, Txn.Connection)
        Dim intAffects As Integer = 0

        With adoCmd
            .Transaction = Txn
            .CommandType = CommandType.Text
            .CommandTimeout = 90000

            Try
                intAffects = .ExecuteNonQuery()
            Catch sqlex As SqlClient.SqlException
                Dim strErrMsg As String = ""
                For Each objErr As SqlError In sqlex.Errors
                    strErrMsg = String.Format("{0};{1}", strErrMsg, objErr.Message.ToString())
                Next
                If Left(strErrMsg, 1) = ";" Then
                    strErrMsg = Mid(strErrMsg, 2)
                End If
                Throw New Exception(strErrMsg)
            Catch ex As Exception
                Throw ex
            End Try

        End With

        Return intAffects
    End Function

End Class
