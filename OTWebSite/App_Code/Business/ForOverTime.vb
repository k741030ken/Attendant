Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.SqlClient

Public Class ForOverTime
    Public Shared Function ExecQuitPayrollBatch()
        Try
            Dim strSQL As New StringBuilder()
            strSQL.AppendLine(" EXEC TossingOverTime ")
            Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), "AattendantDB") '執行刪除，成功筆數回傳，並做Transaction機制
        Catch ex As Exception

        End Try

    End Function

    Public Shared Function ExecQuitDelete()
        Dim strSQL As New StringBuilder()
        Dim db As Database = DatabaseFactory.CreateDatabase("AattendantDB")
        Dim dbCommand As DbCommand = db.GetStoredProcCommand("TossingOverTime")
        db.ExecuteNonQuery(dbCommand)

    End Function

    Public Shared Function InsertToOverTimeRunningStatus(ByVal compID As String, ByVal empID As String) As Boolean
        Dim result As Boolean = False
        Dim seccessCount As Integer = 0
        Try
            Dim strSQL As New StringBuilder()
            strSQL.AppendLine(" INSERT INTO ToOverTimeRunningStatus (ToOverTimeDate,ToOverTimeType,OTCompID,OTEmpID,ToOverTimeFlag,LastChgComp,LastChgID,LastChgDate) ")
            strSQL.AppendLine(" VALUES (getDate(), ")
            strSQL.AppendLine(Bsp.Utility.Quote("0") & ",")
            strSQL.AppendLine(Bsp.Utility.Quote(compID) & ",")
            strSQL.AppendLine(Bsp.Utility.Quote(empID) & ",")
            strSQL.AppendLine(Bsp.Utility.Quote("0") & ",")
            strSQL.AppendLine(Bsp.Utility.Quote(compID) & ",")
            strSQL.AppendLine(Bsp.Utility.Quote(empID) & ",")
            strSQL.AppendLine("getDate())")

            Try
                seccessCount = Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), "AattendantDB") '執行刪除，成功筆數回傳，並做Transaction機制
                result = True
            Catch
                result = False
                Throw
            End Try
        Catch ex As Exception

        End Try

        Return result

    End Function

    Public Shared Function selectToOverTimeFlag() As String
        Dim result As String = ""
        Dim seccessCount As Integer = 0
        Try
            Dim strSQL As New StringBuilder()
            strSQL.AppendLine(" SELECT ToOverTimeFlag ")
            strSQL.AppendLine(" FROM ToOverTimeRunningStatus ")
            strSQL.AppendLine(" WHERE Id = (SELECT MAX(Id) FROM ToOverTimeRunningStatus)")

            result = Bsp.DB.ExecuteScalar(strSQL.ToString(), "AattendantDB") '執行刪除，成功筆數回傳，並做Transaction機制
        Catch ex As Exception

        End Try
        Return result
    End Function

End Class
