'****************************************************
'功能說明：DB Access。呼叫Enterprise library
'建立人員：Chung
'建立日期：2011.05.12
'****************************************************
Imports Microsoft.VisualBasic
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports System.Data.SqlClient
Imports System.Data
Imports System.Data.Common

Namespace Bsp
    Public Class DB
        Protected Shared DBLogger As NLog.Logger = NLog.LogManager.GetLogger("LoggerForBspDB")

        Private Shared Sub WriteErrorMessage(ByVal ex As Exception, ByVal SQL As String, Optional ByVal Params() As DbParameter = Nothing)
            Dim Msg As New StringBuilder()

            'Msg.AppendLine("")
            Msg.AppendLine("錯誤訊息 :")
            Msg.AppendLine(String.Format(" {0} : {1}", ex.GetType.ToString(), ex.Message))
            Msg.AppendLine("")
            Msg.AppendLine("SQL 語法 :")
            Msg.AppendLine(SQL)
            Msg.AppendLine("")
            If Params IsNot Nothing Then
                Msg.AppendLine("SQL 參數 :")
                For Each Param As DbParameter In Params
                    Msg.AppendLine(String.Format("  {0} : {1}", Param.ParameterName, Param.Value))
                Next
                Msg.AppendLine("")
            End If
            Msg.AppendLine("堆疊 :")

            Dim st As New System.Diagnostics.StackTrace(True)

            For intLoop As Integer = 0 To st.FrameCount - 1
                If st.GetFrame(intLoop).GetFileLineNumber = 0 Then Exit For
                Msg.AppendLine(String.Format(" 於 {0}.{1} 檔案 {2} : 行 {3} ", _
                                st.GetFrame(intLoop).GetMethod().DeclaringType.Name, _
                                st.GetFrame(intLoop).GetMethod().Name, _
                                st.GetFrame(intLoop).GetFileName, _
                                st.GetFrame(intLoop).GetFileLineNumber))
            Next
            Msg.AppendLine("")

            Bsp.Utility.WriteLog(Msg.ToString(), Bsp.Utility.LogType.Error, DBLogger)
        End Sub

#Region "Create Connection"
        Public Shared Function getConnection() As DbConnection
            Return DatabaseFactory.CreateDatabase.CreateConnection
        End Function

        Public Shared Function getConnection(ByVal DBName As String) As DbConnection
            Return DatabaseFactory.CreateDatabase(DBName).CreateConnection
        End Function
#End Region

#Region "Create getDbParameter"
        Public Shared Function getDbParameter(ByVal sName As String, ByVal sValue As Object) As DbParameter
            Return getDbParameter(sName, sValue, ParameterDirection.Input)
        End Function

        Public Shared Function getDbParameter(ByVal sName As String, ByVal sValue As Object, ByVal PDirection As ParameterDirection) As DbParameter
            Return getDbParameter(sName, sValue, PDirection, "")
        End Function

        Public Shared Function getDbParameter(ByVal sName As String, ByVal sValue As Object, ByVal PDirection As ParameterDirection, ByVal ConnectionStringName As String) As DbParameter
            Dim db As Database
            If ConnectionStringName = "" Then
                db = DatabaseFactory.CreateDatabase
            Else
                db = DatabaseFactory.CreateDatabase(ConnectionStringName)
            End If
            Dim DbParam As DbParameter = db.DbProviderFactory.CreateParameter()

            DbParam.ParameterName = sName
            DbParam.Value = sValue
            DbParam.Direction = PDirection

            Return DbParam
        End Function

        Public Shared Function getDbParameter(ByVal sName As String, ByVal sValue As Object, ByVal dType As DbType, ByVal iSize As Integer, ByVal PDirection As ParameterDirection) As DbParameter
            Return getDbParameter(sName, sValue, dType, PDirection, "")
        End Function

        Public Shared Function getDbParameter(ByVal sName As String, ByVal sValue As Object, ByVal dType As DbType, ByVal iSize As Integer, ByVal PDirection As ParameterDirection, ByVal ConnectionStringName As String) As DbParameter
            Dim db As Database
            If ConnectionStringName = "" Then
                db = DatabaseFactory.CreateDatabase
            Else
                db = DatabaseFactory.CreateDatabase(ConnectionStringName)
            End If
            Dim DbParam As DbParameter = db.DbProviderFactory.CreateParameter()

            DbParam.ParameterName = sName
            DbParam.Value = sValue
            DbParam.DbType = dType
            DbParam.Size = iSize
            DbParam.Direction = PDirection

            Return DbParam
        End Function

#End Region

#Region "ExecuteScalar"
        Public Shared Function ExecuteScalar(ByVal strSQL As String) As Object
            Dim db As Database = DatabaseFactory.CreateDatabase()

            Try
                Return db.ExecuteScalar(CommandType.Text, strSQL)
            Catch ex As Exception
                WriteErrorMessage(ex, strSQL)
                Throw
            End Try
        End Function

        Public Shared Function ExecuteScalar(ByVal strSQL As String, ByVal ConnectionStringName As String) As Object
            Dim db As Database = DatabaseFactory.CreateDatabase(ConnectionStringName)

            Try
                Return db.ExecuteScalar(CommandType.Text, strSQL)
            Catch ex As Exception
                WriteErrorMessage(ex, strSQL)
                Throw
            End Try
        End Function

        Public Shared Function ExecuteScalar(ByVal strSQL As String, ByVal DbParam As DbParameter) As Object
            Return ExecuteScalar(strSQL, New DbParameter() {DbParam})
        End Function

        Public Shared Function ExecuteScalar(ByVal strSQL As String, ByVal DbParam() As DbParameter) As Object
            Return ExecuteScalar(strSQL, DbParam, "")
        End Function

        Public Shared Function ExecuteScalar(ByVal strSQL As String, ByVal DbParam() As DbParameter, ByVal ConnectionStringName As String) As Object
            Dim db As Database
            If ConnectionStringName = "" Then
                db = DatabaseFactory.CreateDatabase()
            Else
                db = DatabaseFactory.CreateDatabase(ConnectionStringName)
            End If
            Dim DbCmd As DbCommand = db.GetSqlStringCommand(strSQL)

            For intloop As Integer = 0 To DbParam.GetUpperBound(0)
                DbCmd.Parameters.Add(DbParam(intloop))
            Next

            Try
                Return db.ExecuteScalar(DbCmd)
            Catch ex As Exception
                WriteErrorMessage(ex, strSQL, DbParam)
                Throw
            End Try
        End Function

        Public Shared Function ExecuteScalar(ByVal strSQL As String, ByVal tran As DbTransaction) As Object
            Dim db As Database = DatabaseFactory.CreateDatabase
            Dim DbCmd As DbCommand = db.GetSqlStringCommand(strSQL)

            Try
                Return db.ExecuteScalar(DbCmd, tran)
            Catch ex As Exception
                WriteErrorMessage(ex, strSQL)
                Throw
            End Try
        End Function

        Public Shared Function ExecuteScalar(ByVal strSQL As String, ByVal tran As DbTransaction, ByVal ConnectionStringName As String) As Object
            Dim db As Database = DatabaseFactory.CreateDatabase(ConnectionStringName)
            Dim DbCmd As DbCommand = db.GetSqlStringCommand(strSQL)

            Try
                Return db.ExecuteScalar(DbCmd, tran)
            Catch ex As Exception
                WriteErrorMessage(ex, strSQL)
                Throw
            End Try
        End Function

        Public Shared Function ExecuteScalar(ByVal strSQL As String, ByVal DbParam As DbParameter, ByVal tran As DbTransaction) As Object
            Return ExecuteScalar(strSQL, New DbParameter() {DbParam}, tran)
        End Function

        Public Shared Function ExecuteScalar(ByVal strSQL As String, ByVal DbParam() As DbParameter, ByVal tran As DbTransaction) As Object
            Return ExecuteScalar(strSQL, DbParam, tran, "")
        End Function

        Public Shared Function ExecuteScalar(ByVal strSQL As String, ByVal DbParam() As DbParameter, ByVal tran As DbTransaction, ByVal ConnectionStringName As String) As Object
            Dim db As Database
            If ConnectionStringName = "" Then
                db = DatabaseFactory.CreateDatabase
            Else
                db = DatabaseFactory.CreateDatabase(ConnectionStringName)
            End If
            Dim DbCmd As DbCommand = db.GetSqlStringCommand(strSQL)

            For intloop As Integer = 0 To DbParam.GetUpperBound(0)
                DbCmd.Parameters.Add(DbParam(intloop))
            Next

            Try
                Return db.ExecuteScalar(DbCmd, tran)
            Catch ex As Exception
                WriteErrorMessage(ex, strSQL, DbParam)
                Throw
            End Try
        End Function
#End Region

#Region "ExecuteDataSet"
        Public Shared Function ExecuteDataSet(ByVal CmdType As CommandType, ByVal strSQL As String) As DataSet
            Return ExecuteDataSet(CmdType, strSQL, "")
        End Function

        Public Shared Function ExecuteDataSet(ByVal CmdType As CommandType, ByVal strSQL As String, ByVal WithTransaction As Boolean) As DataSet
            Return ExecuteDataSet(CmdType, strSQL, WithTransaction, "")
        End Function

        Public Shared Function ExecuteDataSet(ByVal CmdType As CommandType, ByVal strSQL As String, ByVal ConnectionStringName As String) As DataSet
            Return ExecuteDataSet(CmdType, strSQL, False, ConnectionStringName)
        End Function

        Public Shared Function ExecuteDataSet(ByVal CmdType As CommandType, ByVal strSQL As String, ByVal WithTransaction As Boolean, ByVal ConnectionStringName As String) As DataSet
            Dim db As Database
            If ConnectionStringName = "" Then
                db = DatabaseFactory.CreateDatabase()
            Else
                db = DatabaseFactory.CreateDatabase(ConnectionStringName)
            End If
            Dim DbCmd As DbCommand

            Select Case CmdType
                Case CommandType.StoredProcedure
                    DbCmd = db.GetStoredProcCommand(strSQL)
                Case CommandType.Text
                    DbCmd = db.GetSqlStringCommand(strSQL)
                Case Else
                    DbCmd = db.GetSqlStringCommand(strSQL)
            End Select
            DbCmd.CommandTimeout = 900

            If WithTransaction Then
                Using cn As DbConnection = db.CreateConnection()
                    cn.Open()
                    Dim tran As DbTransaction = cn.BeginTransaction

                    Try
                        Dim ds As DataSet = db.ExecuteDataSet(DbCmd, tran)
                        tran.Commit()
                        Return ds
                    Catch ex As Exception
                        tran.Rollback()
                        WriteErrorMessage(ex, strSQL)
                        Throw
                    Finally
                        tran.Dispose()
                        cn.Close()
                    End Try
                End Using
            Else
                Try
                    Return db.ExecuteDataSet(DbCmd)
                Catch ex As Exception
                    WriteErrorMessage(ex, strSQL)
                    Throw
                End Try
            End If
        End Function

        Public Shared Function ExecuteDataSet(ByVal CmdType As CommandType, ByVal strSQL As String, ByVal DbParam As DbParameter) As DataSet
            Return ExecuteDataSet(CmdType, strSQL, New DbParameter() {DbParam})
        End Function

        Public Shared Function ExecuteDataSet(ByVal CmdType As CommandType, ByVal strSQL As String, ByVal DbParam() As DbParameter) As DataSet
            Return ExecuteDataSet(CmdType, strSQL, DbParam, "")
        End Function

        Public Shared Function ExecuteDataSet(ByVal CmdType As CommandType, ByVal strSQL As String, ByVal DbParam As DbParameter, ByVal WithTransaction As Boolean) As DataSet
            Return ExecuteDataSet(CmdType, strSQL, New DbParameter() {DbParam}, WithTransaction)
        End Function

        Public Shared Function ExecuteDataSet(ByVal CmdType As CommandType, ByVal strSQL As String, ByVal DbParam() As DbParameter, ByVal WithTransaction As Boolean) As DataSet
            Return ExecuteDataSet(CmdType, strSQL, DbParam, WithTransaction, "")
        End Function

        Public Shared Function ExecuteDataSet(ByVal CmdType As CommandType, ByVal strSQL As String, ByVal DbParam() As DbParameter, ByVal ConnectionStringName As String) As DataSet
            Return ExecuteDataSet(CmdType, strSQL, DbParam, False, ConnectionStringName)
        End Function

        Public Shared Function ExecuteDataSet(ByVal CmdType As CommandType, ByVal strSQL As String, ByVal DbParam() As DbParameter, ByVal WithTransaction As Boolean, ByVal ConnectionStringName As String) As DataSet
            Dim db As Database
            If ConnectionStringName = "" Then
                db = DatabaseFactory.CreateDatabase
            Else
                db = DatabaseFactory.CreateDatabase(ConnectionStringName)
            End If
            Dim DbCmd As DbCommand

            If CmdType = CommandType.StoredProcedure Then
                DbCmd = db.GetStoredProcCommand(strSQL)
            Else
                DbCmd = db.GetSqlStringCommand(strSQL)
            End If

            For Each Param As DbParameter In DbParam
                DbCmd.Parameters.Add(Param)
            Next
            DbCmd.CommandTimeout = 900

            If WithTransaction Then
                Using cn As DbConnection = db.CreateConnection
                    cn.Open()
                    Dim tran As DbTransaction = cn.BeginTransaction
                    Try
                        Dim ds As DataSet = db.ExecuteDataSet(DbCmd, tran)
                        tran.Commit()
                        Return ds
                    Catch ex As Exception
                        tran.Rollback()
                        WriteErrorMessage(ex, strSQL, DbParam)
                        Throw
                    Finally
                        tran.Dispose()
                        cn.Close()
                    End Try
                End Using
            Else
                Try
                    Return db.ExecuteDataSet(DbCmd)
                Catch ex As Exception
                    WriteErrorMessage(ex, strSQL, DbParam)
                    Throw
                End Try
            End If
        End Function

        Public Shared Function ExecuteDataSet(ByVal CmdType As CommandType, ByVal strSQL As String, ByVal DbParam As DbParameter, ByVal ConnectionStringName As String) As DataSet
            Return ExecuteDataSet(CmdType, strSQL, New DbParameter() {DbParam}, ConnectionStringName)
        End Function

        Public Shared Function ExecuteDataSet(ByVal CmdType As CommandType, ByVal strSQL As String, ByVal DbParam As DbParameter, ByVal WithTransaction As Boolean, ByVal ConnectionStringName As String) As DataSet
            Return ExecuteDataSet(CmdType, strSQL, New DbParameter() {DbParam}, WithTransaction, ConnectionStringName)
        End Function

        Public Shared Function ExecuteDataSet(ByVal CmdType As CommandType, ByVal strSQL As String, ByVal tran As DbTransaction) As DataSet
            Return ExecuteDataSet(CmdType, strSQL, tran, "")
        End Function

        Public Shared Function ExecuteDataSet(ByVal CmdType As CommandType, ByVal strSQL As String, ByVal tran As DbTransaction, ByVal ConnectionStringName As String) As DataSet
            Dim db As Database
            If ConnectionStringName = "" Then
                db = DatabaseFactory.CreateDatabase
            Else
                db = DatabaseFactory.CreateDatabase(ConnectionStringName)
            End If
            Dim DbCmd As DbCommand

            If CmdType = CommandType.StoredProcedure Then
                DbCmd = db.GetStoredProcCommand(strSQL)
            Else
                DbCmd = db.GetSqlStringCommand(strSQL)
            End If
            DbCmd.CommandTimeout = 900

            Try
                Return db.ExecuteDataSet(DbCmd, tran)
            Catch ex As Exception
                WriteErrorMessage(ex, strSQL)
                Throw
            End Try
        End Function

        Public Shared Function ExecuteDataSet(ByVal CmdType As CommandType, ByVal strSQL As String, ByVal DbParam As DbParameter, ByVal tran As DbTransaction) As DataSet
            Return ExecuteDataSet(CmdType, strSQL, New DbParameter() {DbParam}, tran)
        End Function

        Public Shared Function ExecuteDataSet(ByVal CmdType As CommandType, ByVal strSQL As String, ByVal DbParam() As DbParameter, ByVal tran As DbTransaction) As DataSet
            Return ExecuteDataSet(CmdType, strSQL, DbParam, tran, "")
        End Function

        Public Shared Function ExecuteDataSet(ByVal CmdType As CommandType, ByVal strSQL As String, ByVal DbParam() As DbParameter, ByVal tran As DbTransaction, ByVal ConnectionstringName As String) As DataSet
            Dim db As Database
            If ConnectionstringName = "" Then
                db = DatabaseFactory.CreateDatabase
            Else
                db = DatabaseFactory.CreateDatabase(ConnectionstringName)
            End If
            Dim DbCmd As DbCommand

            If CmdType = CommandType.StoredProcedure Then
                DbCmd = db.GetStoredProcCommand(strSQL)
            Else
                DbCmd = db.GetSqlStringCommand(strSQL)
            End If

            For Each Param As DbParameter In DbParam
                DbCmd.Parameters.Add(Param)
            Next
            DbCmd.CommandTimeout = 900

            Try
                Return db.ExecuteDataSet(DbCmd, tran)
            Catch ex As Exception
                WriteErrorMessage(ex, strSQL, DbParam)
                Throw
            End Try
        End Function
#End Region

#Region "ExecuteNonQuery"
        Public Shared Function ExecuteNonQuery(ByVal CmdType As CommandType, ByVal strSQL As String, ByVal WithTransaction As Boolean) As Integer
            Return ExecuteNonQuery(CmdType, strSQL, WithTransaction, "")
        End Function

        Public Shared Function ExecuteNonQuery(ByVal CmdType As CommandType, ByVal strSQL As String, ByVal WithTransaction As Boolean, ByVal ConnectionStringName As String) As Integer
            If WithTransaction Then
                Return ExecuteNonQuery(CmdType, strSQL, ConnectionStringName)
            Else
                Dim db As Database
                If ConnectionStringName = "" Then
                    db = DatabaseFactory.CreateDatabase()
                Else
                    db = DatabaseFactory.CreateDatabase(ConnectionStringName)
                End If
                Dim DbCmd As DbCommand

                If CmdType = CommandType.StoredProcedure Then
                    DbCmd = db.GetStoredProcCommand(strSQL)
                Else
                    DbCmd = db.GetSqlStringCommand(strSQL)
                End If
                DbCmd.CommandTimeout = 900

                Try
                    Return db.ExecuteNonQuery(DbCmd)
                Catch ex As Exception
                    WriteErrorMessage(ex, strSQL)
                    Throw
                End Try
            End If
        End Function

        Public Shared Function ExecuteNonQuery(ByVal CmdType As CommandType, ByVal strSQL As String) As Integer
            Return ExecuteNonQuery(CmdType, strSQL, "")
        End Function

        Public Shared Function ExecuteNonQuery(ByVal CmdType As CommandType, ByVal strSQL As String, ByVal ConnectionStringName As String) As Integer
            Dim db As Database
            If ConnectionStringName = "" Then
                db = DatabaseFactory.CreateDatabase()
            Else
                db = DatabaseFactory.CreateDatabase(ConnectionStringName)
            End If

            Dim intAffectedRows As Integer = 0
            Using cn As DbConnection = db.CreateConnection
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction
                Try
                    intAffectedRows = ExecuteNonQuery(CmdType, strSQL, tran)
                    tran.Commit()
                Catch ex As Exception
                    tran.Rollback()
                    WriteErrorMessage(ex, strSQL)
                    Throw
                End Try
            End Using
            Return intAffectedRows
        End Function

        Public Shared Function ExecuteNonQuery(ByVal CmdType As CommandType, ByVal strSQL As String, ByVal tran As DbTransaction) As Integer
            Return ExecuteNonQuery(CmdType, strSQL, tran, "")
        End Function

        Public Shared Function ExecuteNonQuery(ByVal CmdType As CommandType, ByVal strSQL As String, ByVal tran As DbTransaction, ByVal ConnectionStringName As String) As Integer
            Dim db As Database
            If ConnectionStringName = "" Then
                db = DatabaseFactory.CreateDatabase()
            Else
                db = DatabaseFactory.CreateDatabase(ConnectionStringName)
            End If
            Dim DbCmd As DbCommand

            If CmdType = CommandType.StoredProcedure Then
                DbCmd = db.GetStoredProcCommand(strSQL)
            Else
                DbCmd = db.GetSqlStringCommand(strSQL)
            End If
            DbCmd.CommandTimeout = 900

            Try
                Return db.ExecuteNonQuery(DbCmd, tran)
            Catch ex As Exception
                WriteErrorMessage(ex, strSQL)
                Throw
            End Try
        End Function

        Public Shared Function ExecuteNonQuery(ByVal CmdType As CommandType, ByVal strSQL As String, ByVal DbParam As DbParameter) As Integer
            Return ExecuteNonQuery(CmdType, strSQL, New DbParameter() {DbParam})
        End Function

        Public Shared Function ExecuteNonQuery(ByVal CmdType As CommandType, ByVal strSQL As String, ByVal DbParam() As DbParameter) As Integer
            Return ExecuteNonQuery(CmdType, strSQL, DbParam, "")
        End Function

        Public Shared Function ExecuteNonQuery(ByVal CmdType As CommandType, ByVal strSQL As String, ByVal DbParam() As DbParameter, ByVal ConnectionStringName As String) As Integer
            Dim db As Database
            If ConnectionStringName = "" Then
                db = DatabaseFactory.CreateDatabase()
            Else
                db = DatabaseFactory.CreateDatabase(ConnectionStringName)
            End If
            Dim intAffectedRows As Integer = 0

            Using cn As DbConnection = db.CreateConnection
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction
                Try
                    intAffectedRows = ExecuteNonQuery(CmdType, strSQL, DbParam, tran)
                    tran.Commit()
                Catch ex As Exception
                    tran.Rollback()
                    WriteErrorMessage(ex, strSQL, DbParam)
                    Throw
                End Try
            End Using
            Return intAffectedRows
        End Function

        Public Shared Function ExecuteNonQuery(ByVal CmdType As CommandType, ByVal strSQL As String, ByVal DbParam As DbParameter, ByVal tran As DbTransaction) As Integer
            Return ExecuteNonQuery(CmdType, strSQL, New DbParameter() {DbParam}, tran)
        End Function

        Public Shared Function ExecuteNonQuery(ByVal CmdType As CommandType, ByVal strSQL As String, ByVal DbParam() As DbParameter, ByVal tran As DbTransaction) As Integer
            Return ExecuteNonQuery(CmdType, strSQL, DbParam, tran, "")
        End Function

        Public Shared Function ExecuteNonQuery(ByVal CmdType As CommandType, ByVal strSQL As String, ByVal DbParam() As DbParameter, ByVal tran As DbTransaction, ByVal ConnectionStringName As String) As Integer
            Dim db As Database
            If ConnectionStringName = "" Then
                db = DatabaseFactory.CreateDatabase
            Else
                db = DatabaseFactory.CreateDatabase(ConnectionStringName)
            End If
            Dim DbCmd As DbCommand

            If CmdType = CommandType.StoredProcedure Then
                DbCmd = db.GetStoredProcCommand(strSQL)
            Else
                DbCmd = db.GetSqlStringCommand(strSQL)
            End If

            For Each Param As DbParameter In DbParam
                DbCmd.Parameters.Add(Param)
            Next
            DbCmd.CommandTimeout = 900

            Try
                Return db.ExecuteNonQuery(DbCmd, tran)
            Catch ex As Exception
                WriteErrorMessage(ex, strSQL, DbParam)
                Throw
            End Try
        End Function
#End Region

    End Class
End Namespace
