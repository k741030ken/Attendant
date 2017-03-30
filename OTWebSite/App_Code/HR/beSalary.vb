'****************************************************************
' Table:Salary
' Created Date: 2015.08.28
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beSalary
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "EmpID", "SalaryID", "PayMethod", "MethodRatio", "MethodAmt", "Currency", "Amount", "PeriodFlag", "ValidFrom" _
                                    , "ValidTo", "LastChgComp", "LastChgID", "LastChgDate", "Ex_Amount", "Z_Amount" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(Decimal), GetType(Integer), GetType(Integer), GetType(String), GetType(String), GetType(Date) _
                                    , GetType(Date), GetType(String), GetType(String), GetType(Date), GetType(Integer), GetType(String) }
        Private m_PrimaryFields As String() = { "CompID", "EmpID", "SalaryID" }

        Public ReadOnly Property Rows() As beSalary.Rows 
            Get
                Return m_Rows
            End Get
        End Property

        Public ReadOnly Property FieldNames() As String()
            Get
                Return m_Fields
            End Get
        End Property

        Public ReadOnly Property PrimaryFieldNames() As String()
            Get
                Return m_PrimaryFields
            End Get
        End Property

        Public Function IsPrimaryKey(ByVal fieldName As String) As Boolean
            Dim iKeys As IEnumerable(Of String) = From s In m_PrimaryFields Where s.ToString().Equals(fieldName) Select s
            Return IIf(iKeys.Count() > 0, True, False)
        End Function

        Public Sub Dispose()
            m_Rows.Dispose()
        End Sub

        ''' <summary>
        ''' 將DataTable資料轉成entity
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Transfer2Row(SalaryTable As DataTable)
            For Each dr As DataRow In SalaryTable.Rows
                m_Rows.Add(New Row(dr))
            Next
        End Sub

        ''' <summary>
        ''' 將Entity的資料轉成DataTable
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Transfer2DataTable() As DataTable
            Dim dt As DataTable = New DataTable()
            Dim dcPrimary As DataColumn() = New DataColumn() {}

            For i As Integer = 0 To m_Fields.Length - 1
                Dim dc As DataColumn = New DataColumn(m_Fields(i), m_Types(i))
                If IsPrimaryKey(m_Fields(i)) Then
                    Array.Resize(Of DataColumn)(dcPrimary, dcPrimary.Length + 1)
                    dcPrimary(dcPrimary.Length - 1) = dc
                End If
            Next

            For i As Integer = 0 To m_Rows.Count - 1
                Dim dr As DataRow = dt.NewRow()

                dr(m_Rows(i).CompID.FieldName) = m_Rows(i).CompID.Value
                dr(m_Rows(i).EmpID.FieldName) = m_Rows(i).EmpID.Value
                dr(m_Rows(i).SalaryID.FieldName) = m_Rows(i).SalaryID.Value
                dr(m_Rows(i).PayMethod.FieldName) = m_Rows(i).PayMethod.Value
                dr(m_Rows(i).MethodRatio.FieldName) = m_Rows(i).MethodRatio.Value
                dr(m_Rows(i).MethodAmt.FieldName) = m_Rows(i).MethodAmt.Value
                dr(m_Rows(i).Currency.FieldName) = m_Rows(i).Currency.Value
                dr(m_Rows(i).Amount.FieldName) = m_Rows(i).Amount.Value
                dr(m_Rows(i).PeriodFlag.FieldName) = m_Rows(i).PeriodFlag.Value
                dr(m_Rows(i).ValidFrom.FieldName) = m_Rows(i).ValidFrom.Value
                dr(m_Rows(i).ValidTo.FieldName) = m_Rows(i).ValidTo.Value
                dr(m_Rows(i).LastChgComp.FieldName) = m_Rows(i).LastChgComp.Value
                dr(m_Rows(i).LastChgID.FieldName) = m_Rows(i).LastChgID.Value
                dr(m_Rows(i).LastChgDate.FieldName) = m_Rows(i).LastChgDate.Value
                dr(m_Rows(i).Ex_Amount.FieldName) = m_Rows(i).Ex_Amount.Value
                dr(m_Rows(i).Z_Amount.FieldName) = m_Rows(i).Z_Amount.Value

                dt.Rows.Add(dr)
            Next

            Return dt
        End Function

    End Class

    Public Class Rows
        Private m_Rows As List(Of Row) = New List(Of Row)()

        Default Public ReadOnly Property Rows(ByVal i As Integer) As Row
            Get
                Return m_Rows(i)
            End Get
        End Property

        Public ReadOnly Property Count() As Integer
            Get
                Return m_Rows.Count
            End Get
        End Property

        Public Sub Add(SalaryRow As Row)
            m_Rows.Add(SalaryRow)
        End Sub

        Public Sub Remove(SalaryRow As Row)
            If m_Rows.IndexOf(SalaryRow) >= 0 Then
                m_Rows.Remove(SalaryRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_EmpID As Field(Of String) = new Field(Of String)("EmpID", true)
        Private FI_SalaryID As Field(Of String) = new Field(Of String)("SalaryID", true)
        Private FI_PayMethod As Field(Of String) = new Field(Of String)("PayMethod", true)
        Private FI_MethodRatio As Field(Of Decimal) = new Field(Of Decimal)("MethodRatio", true)
        Private FI_MethodAmt As Field(Of Integer) = new Field(Of Integer)("MethodAmt", true)
        Private FI_Currency As Field(Of Integer) = new Field(Of Integer)("Currency", true)
        Private FI_Amount As Field(Of String) = new Field(Of String)("Amount", true)
        Private FI_PeriodFlag As Field(Of String) = new Field(Of String)("PeriodFlag", true)
        Private FI_ValidFrom As Field(Of Date) = new Field(Of Date)("ValidFrom", true)
        Private FI_ValidTo As Field(Of Date) = new Field(Of Date)("ValidTo", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private FI_Ex_Amount As Field(Of Integer) = new Field(Of Integer)("Ex_Amount", true)
        Private FI_Z_Amount As Field(Of String) = new Field(Of String)("Z_Amount", true)
        Private m_FieldNames As String() = { "CompID", "EmpID", "SalaryID", "PayMethod", "MethodRatio", "MethodAmt", "Currency", "Amount", "PeriodFlag", "ValidFrom" _
                                    , "ValidTo", "LastChgComp", "LastChgID", "LastChgDate", "Ex_Amount", "Z_Amount" }
        Private m_PrimaryFields As String() = { "CompID", "EmpID", "SalaryID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CompID"
                    Return FI_CompID.Value
                Case "EmpID"
                    Return FI_EmpID.Value
                Case "SalaryID"
                    Return FI_SalaryID.Value
                Case "PayMethod"
                    Return FI_PayMethod.Value
                Case "MethodRatio"
                    Return FI_MethodRatio.Value
                Case "MethodAmt"
                    Return FI_MethodAmt.Value
                Case "Currency"
                    Return FI_Currency.Value
                Case "Amount"
                    Return FI_Amount.Value
                Case "PeriodFlag"
                    Return FI_PeriodFlag.Value
                Case "ValidFrom"
                    Return FI_ValidFrom.Value
                Case "ValidTo"
                    Return FI_ValidTo.Value
                Case "LastChgComp"
                    Return FI_LastChgComp.Value
                Case "LastChgID"
                    Return FI_LastChgID.Value
                Case "LastChgDate"
                    Return FI_LastChgDate.Value
                Case "Ex_Amount"
                    Return FI_Ex_Amount.Value
                Case "Z_Amount"
                    Return FI_Z_Amount.Value
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Sub SetFieldValue(ByVal fieldName As String, ByVal value As Object)
            Select Case fieldName
                Case "CompID"
                    FI_CompID.SetValue(value)
                Case "EmpID"
                    FI_EmpID.SetValue(value)
                Case "SalaryID"
                    FI_SalaryID.SetValue(value)
                Case "PayMethod"
                    FI_PayMethod.SetValue(value)
                Case "MethodRatio"
                    FI_MethodRatio.SetValue(value)
                Case "MethodAmt"
                    FI_MethodAmt.SetValue(value)
                Case "Currency"
                    FI_Currency.SetValue(value)
                Case "Amount"
                    FI_Amount.SetValue(value)
                Case "PeriodFlag"
                    FI_PeriodFlag.SetValue(value)
                Case "ValidFrom"
                    FI_ValidFrom.SetValue(value)
                Case "ValidTo"
                    FI_ValidTo.SetValue(value)
                Case "LastChgComp"
                    FI_LastChgComp.SetValue(value)
                Case "LastChgID"
                    FI_LastChgID.SetValue(value)
                Case "LastChgDate"
                    FI_LastChgDate.SetValue(value)
                Case "Ex_Amount"
                    FI_Ex_Amount.SetValue(value)
                Case "Z_Amount"
                    FI_Z_Amount.SetValue(value)
            End Select
        End Sub

        Default Public Property Row(ByVal fieldName As String) As Object
            Get
                Return GetFieldValue(fieldName)
            End Get
            Set(ByVal value As Object)
                SetFieldValue(fieldName, value)
            End Set
        End Property

        Default Public Property Row(ByVal idx As Integer) As Object
            Get
                Return GetFieldValue(m_FieldNames(idx))
            End Get
            Set(ByVal value As Object)
                SetFieldValue(m_FieldNames(idx), value)
            End Set
        End Property

        Public ReadOnly Property FieldNames() As String()
            Get
                Return m_FieldNames
            End Get
        End Property

        Public ReadOnly Property FieldCount() As Integer
            Get
                Return m_FieldNames.Length
            End Get
        End Property

        Public Function IsUpdated(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "CompID"
                    return FI_CompID.Updated
                Case "EmpID"
                    return FI_EmpID.Updated
                Case "SalaryID"
                    return FI_SalaryID.Updated
                Case "PayMethod"
                    return FI_PayMethod.Updated
                Case "MethodRatio"
                    return FI_MethodRatio.Updated
                Case "MethodAmt"
                    return FI_MethodAmt.Updated
                Case "Currency"
                    return FI_Currency.Updated
                Case "Amount"
                    return FI_Amount.Updated
                Case "PeriodFlag"
                    return FI_PeriodFlag.Updated
                Case "ValidFrom"
                    return FI_ValidFrom.Updated
                Case "ValidTo"
                    return FI_ValidTo.Updated
                Case "LastChgComp"
                    return FI_LastChgComp.Updated
                Case "LastChgID"
                    return FI_LastChgID.Updated
                Case "LastChgDate"
                    return FI_LastChgDate.Updated
                Case "Ex_Amount"
                    return FI_Ex_Amount.Updated
                Case "Z_Amount"
                    return FI_Z_Amount.Updated
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public Function CreateUpdateSQL(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "CompID"
                    return FI_CompID.CreateUpdateSQL
                Case "EmpID"
                    return FI_EmpID.CreateUpdateSQL
                Case "SalaryID"
                    return FI_SalaryID.CreateUpdateSQL
                Case "PayMethod"
                    return FI_PayMethod.CreateUpdateSQL
                Case "MethodRatio"
                    return FI_MethodRatio.CreateUpdateSQL
                Case "MethodAmt"
                    return FI_MethodAmt.CreateUpdateSQL
                Case "Currency"
                    return FI_Currency.CreateUpdateSQL
                Case "Amount"
                    return FI_Amount.CreateUpdateSQL
                Case "PeriodFlag"
                    return FI_PeriodFlag.CreateUpdateSQL
                Case "ValidFrom"
                    return FI_ValidFrom.CreateUpdateSQL
                Case "ValidTo"
                    return FI_ValidTo.CreateUpdateSQL
                Case "LastChgComp"
                    return FI_LastChgComp.CreateUpdateSQL
                Case "LastChgID"
                    return FI_LastChgID.CreateUpdateSQL
                Case "LastChgDate"
                    return FI_LastChgDate.CreateUpdateSQL
                Case "Ex_Amount"
                    return FI_Ex_Amount.CreateUpdateSQL
                Case "Z_Amount"
                    return FI_Z_Amount.CreateUpdateSQL
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public ReadOnly Property PrimaryFieldNames() As String()
            Get
                Return m_PrimaryFields
            End Get
        End Property

        Public Function IsPrimaryKey(ByVal fieldName As String) As Boolean
            Dim iKeys As IEnumerable(Of String) = From s In m_PrimaryFields Where s.ToString().Equals(fieldName) Select s
            Return IIf(iKeys.Count() > 0, True, False)
        End Function

        Public ReadOnly Property IdentityFields()
            Get
                Return m_IdentityFields
            End Get
        End Property

        Public Function IsIdentityField(ByVal fieldName As String) As Boolean
            Dim iKeys As IEnumerable(Of String) = From s In m_IdentityFields Where s.ToString().Equals(fieldName) Select s
            Return IIf(iKeys.Count() > 0, True, False)
        End Function

        Public ReadOnly Property LoadFromDataRow() As Boolean
            Get
                Return m_LoadFromDataRow
            End Get
        End Property

        Public Sub New()
            FI_CompID.SetInitValue("")
            FI_EmpID.SetInitValue("")
            FI_SalaryID.SetInitValue("")
            FI_PayMethod.SetInitValue("")
            FI_MethodRatio.SetInitValue(0)
            FI_MethodAmt.SetInitValue(0)
            FI_Currency.SetInitValue(0)
            FI_Amount.SetInitValue("")
            FI_PeriodFlag.SetInitValue("")
            FI_ValidFrom.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_ValidTo.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_Ex_Amount.SetInitValue(0)
            FI_Z_Amount.SetInitValue("")
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CompID.SetInitValue(dr("CompID"))
            FI_EmpID.SetInitValue(dr("EmpID"))
            FI_SalaryID.SetInitValue(dr("SalaryID"))
            FI_PayMethod.SetInitValue(dr("PayMethod"))
            FI_MethodRatio.SetInitValue(dr("MethodRatio"))
            FI_MethodAmt.SetInitValue(dr("MethodAmt"))
            FI_Currency.SetInitValue(dr("Currency"))
            FI_Amount.SetInitValue(dr("Amount"))
            FI_PeriodFlag.SetInitValue(dr("PeriodFlag"))
            FI_ValidFrom.SetInitValue(dr("ValidFrom"))
            FI_ValidTo.SetInitValue(dr("ValidTo"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))
            FI_Ex_Amount.SetInitValue(dr("Ex_Amount"))
            FI_Z_Amount.SetInitValue(dr("Z_Amount"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CompID.Updated = False
            FI_EmpID.Updated = False
            FI_SalaryID.Updated = False
            FI_PayMethod.Updated = False
            FI_MethodRatio.Updated = False
            FI_MethodAmt.Updated = False
            FI_Currency.Updated = False
            FI_Amount.Updated = False
            FI_PeriodFlag.Updated = False
            FI_ValidFrom.Updated = False
            FI_ValidTo.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
            FI_Ex_Amount.Updated = False
            FI_Z_Amount.Updated = False
        End Sub

        Public ReadOnly Property CompID As Field(Of String) 
            Get
                Return FI_CompID
            End Get
        End Property

        Public ReadOnly Property EmpID As Field(Of String) 
            Get
                Return FI_EmpID
            End Get
        End Property

        Public ReadOnly Property SalaryID As Field(Of String) 
            Get
                Return FI_SalaryID
            End Get
        End Property

        Public ReadOnly Property PayMethod As Field(Of String) 
            Get
                Return FI_PayMethod
            End Get
        End Property

        Public ReadOnly Property MethodRatio As Field(Of Decimal) 
            Get
                Return FI_MethodRatio
            End Get
        End Property

        Public ReadOnly Property MethodAmt As Field(Of Integer) 
            Get
                Return FI_MethodAmt
            End Get
        End Property

        Public ReadOnly Property Currency As Field(Of Integer) 
            Get
                Return FI_Currency
            End Get
        End Property

        Public ReadOnly Property Amount As Field(Of String) 
            Get
                Return FI_Amount
            End Get
        End Property

        Public ReadOnly Property PeriodFlag As Field(Of String) 
            Get
                Return FI_PeriodFlag
            End Get
        End Property

        Public ReadOnly Property ValidFrom As Field(Of Date) 
            Get
                Return FI_ValidFrom
            End Get
        End Property

        Public ReadOnly Property ValidTo As Field(Of Date) 
            Get
                Return FI_ValidTo
            End Get
        End Property

        Public ReadOnly Property LastChgComp As Field(Of String) 
            Get
                Return FI_LastChgComp
            End Get
        End Property

        Public ReadOnly Property LastChgID As Field(Of String) 
            Get
                Return FI_LastChgID
            End Get
        End Property

        Public ReadOnly Property LastChgDate As Field(Of Date) 
            Get
                Return FI_LastChgDate
            End Get
        End Property

        Public ReadOnly Property Ex_Amount As Field(Of Integer) 
            Get
                Return FI_Ex_Amount
            End Get
        End Property

        Public ReadOnly Property Z_Amount As Field(Of String) 
            Get
                Return FI_Z_Amount
            End Get
        End Property

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal SalaryRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From Salary")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And SalaryID = @SalaryID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SalaryRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, SalaryRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@SalaryID", DbType.String, SalaryRow.SalaryID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal SalaryRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From Salary")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And SalaryID = @SalaryID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SalaryRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, SalaryRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@SalaryID", DbType.String, SalaryRow.SalaryID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal SalaryRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From Salary")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And SalaryID = @SalaryID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SalaryRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@SalaryID", DbType.String, r.SalaryID.Value)

                        intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
                    Next
                    tran.Commit()
                    inTrans = false
                Catch ex As Exception
                    If inTrans Then tran.Rollback()
                    Throw
                Finally
                    tran.Dispose()
                    If cn.State = ConnectionState.Open Then cn.Close()
                End Try
            End Using
            Return intRowsAffected
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal SalaryRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From Salary")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And SalaryID = @SalaryID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SalaryRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@SalaryID", DbType.String, r.SalaryID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal SalaryRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Salary")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And SalaryID = @SalaryID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SalaryRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, SalaryRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@SalaryID", DbType.String, SalaryRow.SalaryID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(SalaryRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Salary")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And SalaryID = @SalaryID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SalaryRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, SalaryRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@SalaryID", DbType.String, SalaryRow.SalaryID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal SalaryRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update Salary Set")
            For i As Integer = 0 To SalaryRow.FieldNames.Length - 1
                If Not SalaryRow.IsIdentityField(SalaryRow.FieldNames(i)) AndAlso SalaryRow.IsUpdated(SalaryRow.FieldNames(i)) AndAlso SalaryRow.CreateUpdateSQL(SalaryRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SalaryRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And EmpID = @PKEmpID")
            strSQL.AppendLine("And SalaryID = @PKSalaryID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SalaryRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, SalaryRow.CompID.Value)
            If SalaryRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, SalaryRow.EmpID.Value)
            If SalaryRow.SalaryID.Updated Then db.AddInParameter(dbcmd, "@SalaryID", DbType.String, SalaryRow.SalaryID.Value)
            If SalaryRow.PayMethod.Updated Then db.AddInParameter(dbcmd, "@PayMethod", DbType.String, SalaryRow.PayMethod.Value)
            If SalaryRow.MethodRatio.Updated Then db.AddInParameter(dbcmd, "@MethodRatio", DbType.Decimal, SalaryRow.MethodRatio.Value)
            If SalaryRow.MethodAmt.Updated Then db.AddInParameter(dbcmd, "@MethodAmt", DbType.Int32, SalaryRow.MethodAmt.Value)
            If SalaryRow.Currency.Updated Then db.AddInParameter(dbcmd, "@Currency", DbType.Int32, SalaryRow.Currency.Value)
            If SalaryRow.Amount.Updated Then db.AddInParameter(dbcmd, "@Amount", DbType.String, SalaryRow.Amount.Value)
            If SalaryRow.PeriodFlag.Updated Then db.AddInParameter(dbcmd, "@PeriodFlag", DbType.String, SalaryRow.PeriodFlag.Value)
            If SalaryRow.ValidFrom.Updated Then db.AddInParameter(dbcmd, "@ValidFrom", DbType.Date, IIf(IsDateTimeNull(SalaryRow.ValidFrom.Value), Convert.ToDateTime("1900/1/1"), SalaryRow.ValidFrom.Value))
            If SalaryRow.ValidTo.Updated Then db.AddInParameter(dbcmd, "@ValidTo", DbType.Date, IIf(IsDateTimeNull(SalaryRow.ValidTo.Value), Convert.ToDateTime("1900/1/1"), SalaryRow.ValidTo.Value))
            If SalaryRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, SalaryRow.LastChgComp.Value)
            If SalaryRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SalaryRow.LastChgID.Value)
            If SalaryRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SalaryRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SalaryRow.LastChgDate.Value))
            If SalaryRow.Ex_Amount.Updated Then db.AddInParameter(dbcmd, "@Ex_Amount", DbType.Int32, SalaryRow.Ex_Amount.Value)
            If SalaryRow.Z_Amount.Updated Then db.AddInParameter(dbcmd, "@Z_Amount", DbType.String, SalaryRow.Z_Amount.Value)
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(SalaryRow.LoadFromDataRow, SalaryRow.CompID.OldValue, SalaryRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(SalaryRow.LoadFromDataRow, SalaryRow.EmpID.OldValue, SalaryRow.EmpID.Value))
            db.AddInParameter(dbcmd, "@PKSalaryID", DbType.String, IIf(SalaryRow.LoadFromDataRow, SalaryRow.SalaryID.OldValue, SalaryRow.SalaryID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal SalaryRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update Salary Set")
            For i As Integer = 0 To SalaryRow.FieldNames.Length - 1
                If Not SalaryRow.IsIdentityField(SalaryRow.FieldNames(i)) AndAlso SalaryRow.IsUpdated(SalaryRow.FieldNames(i)) AndAlso SalaryRow.CreateUpdateSQL(SalaryRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, SalaryRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And EmpID = @PKEmpID")
            strSQL.AppendLine("And SalaryID = @PKSalaryID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If SalaryRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, SalaryRow.CompID.Value)
            If SalaryRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, SalaryRow.EmpID.Value)
            If SalaryRow.SalaryID.Updated Then db.AddInParameter(dbcmd, "@SalaryID", DbType.String, SalaryRow.SalaryID.Value)
            If SalaryRow.PayMethod.Updated Then db.AddInParameter(dbcmd, "@PayMethod", DbType.String, SalaryRow.PayMethod.Value)
            If SalaryRow.MethodRatio.Updated Then db.AddInParameter(dbcmd, "@MethodRatio", DbType.Decimal, SalaryRow.MethodRatio.Value)
            If SalaryRow.MethodAmt.Updated Then db.AddInParameter(dbcmd, "@MethodAmt", DbType.Int32, SalaryRow.MethodAmt.Value)
            If SalaryRow.Currency.Updated Then db.AddInParameter(dbcmd, "@Currency", DbType.Int32, SalaryRow.Currency.Value)
            If SalaryRow.Amount.Updated Then db.AddInParameter(dbcmd, "@Amount", DbType.String, SalaryRow.Amount.Value)
            If SalaryRow.PeriodFlag.Updated Then db.AddInParameter(dbcmd, "@PeriodFlag", DbType.String, SalaryRow.PeriodFlag.Value)
            If SalaryRow.ValidFrom.Updated Then db.AddInParameter(dbcmd, "@ValidFrom", DbType.Date, IIf(IsDateTimeNull(SalaryRow.ValidFrom.Value), Convert.ToDateTime("1900/1/1"), SalaryRow.ValidFrom.Value))
            If SalaryRow.ValidTo.Updated Then db.AddInParameter(dbcmd, "@ValidTo", DbType.Date, IIf(IsDateTimeNull(SalaryRow.ValidTo.Value), Convert.ToDateTime("1900/1/1"), SalaryRow.ValidTo.Value))
            If SalaryRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, SalaryRow.LastChgComp.Value)
            If SalaryRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SalaryRow.LastChgID.Value)
            If SalaryRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SalaryRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SalaryRow.LastChgDate.Value))
            If SalaryRow.Ex_Amount.Updated Then db.AddInParameter(dbcmd, "@Ex_Amount", DbType.Int32, SalaryRow.Ex_Amount.Value)
            If SalaryRow.Z_Amount.Updated Then db.AddInParameter(dbcmd, "@Z_Amount", DbType.String, SalaryRow.Z_Amount.Value)
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(SalaryRow.LoadFromDataRow, SalaryRow.CompID.OldValue, SalaryRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(SalaryRow.LoadFromDataRow, SalaryRow.EmpID.OldValue, SalaryRow.EmpID.Value))
            db.AddInParameter(dbcmd, "@PKSalaryID", DbType.String, IIf(SalaryRow.LoadFromDataRow, SalaryRow.SalaryID.OldValue, SalaryRow.SalaryID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal SalaryRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SalaryRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update Salary Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where CompID = @PKCompID")
                        strSQL.AppendLine("And EmpID = @PKEmpID")
                        strSQL.AppendLine("And SalaryID = @PKSalaryID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        If r.SalaryID.Updated Then db.AddInParameter(dbcmd, "@SalaryID", DbType.String, r.SalaryID.Value)
                        If r.PayMethod.Updated Then db.AddInParameter(dbcmd, "@PayMethod", DbType.String, r.PayMethod.Value)
                        If r.MethodRatio.Updated Then db.AddInParameter(dbcmd, "@MethodRatio", DbType.Decimal, r.MethodRatio.Value)
                        If r.MethodAmt.Updated Then db.AddInParameter(dbcmd, "@MethodAmt", DbType.Int32, r.MethodAmt.Value)
                        If r.Currency.Updated Then db.AddInParameter(dbcmd, "@Currency", DbType.Int32, r.Currency.Value)
                        If r.Amount.Updated Then db.AddInParameter(dbcmd, "@Amount", DbType.String, r.Amount.Value)
                        If r.PeriodFlag.Updated Then db.AddInParameter(dbcmd, "@PeriodFlag", DbType.String, r.PeriodFlag.Value)
                        If r.ValidFrom.Updated Then db.AddInParameter(dbcmd, "@ValidFrom", DbType.Date, IIf(IsDateTimeNull(r.ValidFrom.Value), Convert.ToDateTime("1900/1/1"), r.ValidFrom.Value))
                        If r.ValidTo.Updated Then db.AddInParameter(dbcmd, "@ValidTo", DbType.Date, IIf(IsDateTimeNull(r.ValidTo.Value), Convert.ToDateTime("1900/1/1"), r.ValidTo.Value))
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        If r.Ex_Amount.Updated Then db.AddInParameter(dbcmd, "@Ex_Amount", DbType.Int32, r.Ex_Amount.Value)
                        If r.Z_Amount.Updated Then db.AddInParameter(dbcmd, "@Z_Amount", DbType.String, r.Z_Amount.Value)
                        db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                        db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))
                        db.AddInParameter(dbcmd, "@PKSalaryID", DbType.String, IIf(r.LoadFromDataRow, r.SalaryID.OldValue, r.SalaryID.Value))

                        intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
                    Next
                    tran.Commit()
                Catch ex As Exception
                    If inTrans Then tran.Rollback()
                    Throw
                Finally
                    tran.Dispose()
                    If cn.State = ConnectionState.Open Then cn.Close()
                End Try
            End Using
            Return intRowsAffected
        End Function

        Public Function Update(ByVal SalaryRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In SalaryRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update Salary Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where CompID = @PKCompID")
                strSQL.AppendLine("And EmpID = @PKEmpID")
                strSQL.AppendLine("And SalaryID = @PKSalaryID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                If r.SalaryID.Updated Then db.AddInParameter(dbcmd, "@SalaryID", DbType.String, r.SalaryID.Value)
                If r.PayMethod.Updated Then db.AddInParameter(dbcmd, "@PayMethod", DbType.String, r.PayMethod.Value)
                If r.MethodRatio.Updated Then db.AddInParameter(dbcmd, "@MethodRatio", DbType.Decimal, r.MethodRatio.Value)
                If r.MethodAmt.Updated Then db.AddInParameter(dbcmd, "@MethodAmt", DbType.Int32, r.MethodAmt.Value)
                If r.Currency.Updated Then db.AddInParameter(dbcmd, "@Currency", DbType.Int32, r.Currency.Value)
                If r.Amount.Updated Then db.AddInParameter(dbcmd, "@Amount", DbType.String, r.Amount.Value)
                If r.PeriodFlag.Updated Then db.AddInParameter(dbcmd, "@PeriodFlag", DbType.String, r.PeriodFlag.Value)
                If r.ValidFrom.Updated Then db.AddInParameter(dbcmd, "@ValidFrom", DbType.Date, IIf(IsDateTimeNull(r.ValidFrom.Value), Convert.ToDateTime("1900/1/1"), r.ValidFrom.Value))
                If r.ValidTo.Updated Then db.AddInParameter(dbcmd, "@ValidTo", DbType.Date, IIf(IsDateTimeNull(r.ValidTo.Value), Convert.ToDateTime("1900/1/1"), r.ValidTo.Value))
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                If r.Ex_Amount.Updated Then db.AddInParameter(dbcmd, "@Ex_Amount", DbType.Int32, r.Ex_Amount.Value)
                If r.Z_Amount.Updated Then db.AddInParameter(dbcmd, "@Z_Amount", DbType.String, r.Z_Amount.Value)
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))
                db.AddInParameter(dbcmd, "@PKSalaryID", DbType.String, IIf(r.LoadFromDataRow, r.SalaryID.OldValue, r.SalaryID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal SalaryRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From Salary")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And SalaryID = @SalaryID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SalaryRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, SalaryRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@SalaryID", DbType.String, SalaryRow.SalaryID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal SalaryRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From Salary")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And SalaryID = @SalaryID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SalaryRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, SalaryRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@SalaryID", DbType.String, SalaryRow.SalaryID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Salary")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal SalaryRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into Salary")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, SalaryID, PayMethod, MethodRatio, MethodAmt, Currency, Amount, PeriodFlag,")
            strSQL.AppendLine("    ValidFrom, ValidTo, LastChgComp, LastChgID, LastChgDate, Ex_Amount, Z_Amount")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @SalaryID, @PayMethod, @MethodRatio, @MethodAmt, @Currency, @Amount, @PeriodFlag,")
            strSQL.AppendLine("    @ValidFrom, @ValidTo, @LastChgComp, @LastChgID, @LastChgDate, @Ex_Amount, @Z_Amount")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SalaryRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, SalaryRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@SalaryID", DbType.String, SalaryRow.SalaryID.Value)
            db.AddInParameter(dbcmd, "@PayMethod", DbType.String, SalaryRow.PayMethod.Value)
            db.AddInParameter(dbcmd, "@MethodRatio", DbType.Decimal, SalaryRow.MethodRatio.Value)
            db.AddInParameter(dbcmd, "@MethodAmt", DbType.Int32, SalaryRow.MethodAmt.Value)
            db.AddInParameter(dbcmd, "@Currency", DbType.Int32, SalaryRow.Currency.Value)
            db.AddInParameter(dbcmd, "@Amount", DbType.String, SalaryRow.Amount.Value)
            db.AddInParameter(dbcmd, "@PeriodFlag", DbType.String, SalaryRow.PeriodFlag.Value)
            db.AddInParameter(dbcmd, "@ValidFrom", DbType.Date, IIf(IsDateTimeNull(SalaryRow.ValidFrom.Value), Convert.ToDateTime("1900/1/1"), SalaryRow.ValidFrom.Value))
            db.AddInParameter(dbcmd, "@ValidTo", DbType.Date, IIf(IsDateTimeNull(SalaryRow.ValidTo.Value), Convert.ToDateTime("1900/1/1"), SalaryRow.ValidTo.Value))
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, SalaryRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SalaryRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SalaryRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SalaryRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@Ex_Amount", DbType.Int32, SalaryRow.Ex_Amount.Value)
            db.AddInParameter(dbcmd, "@Z_Amount", DbType.String, SalaryRow.Z_Amount.Value)

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal SalaryRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into Salary")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, SalaryID, PayMethod, MethodRatio, MethodAmt, Currency, Amount, PeriodFlag,")
            strSQL.AppendLine("    ValidFrom, ValidTo, LastChgComp, LastChgID, LastChgDate, Ex_Amount, Z_Amount")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @SalaryID, @PayMethod, @MethodRatio, @MethodAmt, @Currency, @Amount, @PeriodFlag,")
            strSQL.AppendLine("    @ValidFrom, @ValidTo, @LastChgComp, @LastChgID, @LastChgDate, @Ex_Amount, @Z_Amount")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, SalaryRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, SalaryRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@SalaryID", DbType.String, SalaryRow.SalaryID.Value)
            db.AddInParameter(dbcmd, "@PayMethod", DbType.String, SalaryRow.PayMethod.Value)
            db.AddInParameter(dbcmd, "@MethodRatio", DbType.Decimal, SalaryRow.MethodRatio.Value)
            db.AddInParameter(dbcmd, "@MethodAmt", DbType.Int32, SalaryRow.MethodAmt.Value)
            db.AddInParameter(dbcmd, "@Currency", DbType.Int32, SalaryRow.Currency.Value)
            db.AddInParameter(dbcmd, "@Amount", DbType.String, SalaryRow.Amount.Value)
            db.AddInParameter(dbcmd, "@PeriodFlag", DbType.String, SalaryRow.PeriodFlag.Value)
            db.AddInParameter(dbcmd, "@ValidFrom", DbType.Date, IIf(IsDateTimeNull(SalaryRow.ValidFrom.Value), Convert.ToDateTime("1900/1/1"), SalaryRow.ValidFrom.Value))
            db.AddInParameter(dbcmd, "@ValidTo", DbType.Date, IIf(IsDateTimeNull(SalaryRow.ValidTo.Value), Convert.ToDateTime("1900/1/1"), SalaryRow.ValidTo.Value))
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, SalaryRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, SalaryRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(SalaryRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), SalaryRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@Ex_Amount", DbType.Int32, SalaryRow.Ex_Amount.Value)
            db.AddInParameter(dbcmd, "@Z_Amount", DbType.String, SalaryRow.Z_Amount.Value)

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal SalaryRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into Salary")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, SalaryID, PayMethod, MethodRatio, MethodAmt, Currency, Amount, PeriodFlag,")
            strSQL.AppendLine("    ValidFrom, ValidTo, LastChgComp, LastChgID, LastChgDate, Ex_Amount, Z_Amount")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @SalaryID, @PayMethod, @MethodRatio, @MethodAmt, @Currency, @Amount, @PeriodFlag,")
            strSQL.AppendLine("    @ValidFrom, @ValidTo, @LastChgComp, @LastChgID, @LastChgDate, @Ex_Amount, @Z_Amount")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SalaryRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@SalaryID", DbType.String, r.SalaryID.Value)
                        db.AddInParameter(dbcmd, "@PayMethod", DbType.String, r.PayMethod.Value)
                        db.AddInParameter(dbcmd, "@MethodRatio", DbType.Decimal, r.MethodRatio.Value)
                        db.AddInParameter(dbcmd, "@MethodAmt", DbType.Int32, r.MethodAmt.Value)
                        db.AddInParameter(dbcmd, "@Currency", DbType.Int32, r.Currency.Value)
                        db.AddInParameter(dbcmd, "@Amount", DbType.String, r.Amount.Value)
                        db.AddInParameter(dbcmd, "@PeriodFlag", DbType.String, r.PeriodFlag.Value)
                        db.AddInParameter(dbcmd, "@ValidFrom", DbType.Date, IIf(IsDateTimeNull(r.ValidFrom.Value), Convert.ToDateTime("1900/1/1"), r.ValidFrom.Value))
                        db.AddInParameter(dbcmd, "@ValidTo", DbType.Date, IIf(IsDateTimeNull(r.ValidTo.Value), Convert.ToDateTime("1900/1/1"), r.ValidTo.Value))
                        db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@Ex_Amount", DbType.Int32, r.Ex_Amount.Value)
                        db.AddInParameter(dbcmd, "@Z_Amount", DbType.String, r.Z_Amount.Value)

                        intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
                    Next
                    tran.Commit()
                Catch ex As Exception
                    If inTrans Then tran.Rollback()
                    Throw
                Finally
                    tran.Dispose()
                    If cn.State = ConnectionState.Open Then cn.Close()
                End Try
            End Using
            Return intRowsAffected
        End Function

        Public Function Insert(ByVal SalaryRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into Salary")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, SalaryID, PayMethod, MethodRatio, MethodAmt, Currency, Amount, PeriodFlag,")
            strSQL.AppendLine("    ValidFrom, ValidTo, LastChgComp, LastChgID, LastChgDate, Ex_Amount, Z_Amount")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @SalaryID, @PayMethod, @MethodRatio, @MethodAmt, @Currency, @Amount, @PeriodFlag,")
            strSQL.AppendLine("    @ValidFrom, @ValidTo, @LastChgComp, @LastChgID, @LastChgDate, @Ex_Amount, @Z_Amount")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SalaryRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@SalaryID", DbType.String, r.SalaryID.Value)
                db.AddInParameter(dbcmd, "@PayMethod", DbType.String, r.PayMethod.Value)
                db.AddInParameter(dbcmd, "@MethodRatio", DbType.Decimal, r.MethodRatio.Value)
                db.AddInParameter(dbcmd, "@MethodAmt", DbType.Int32, r.MethodAmt.Value)
                db.AddInParameter(dbcmd, "@Currency", DbType.Int32, r.Currency.Value)
                db.AddInParameter(dbcmd, "@Amount", DbType.String, r.Amount.Value)
                db.AddInParameter(dbcmd, "@PeriodFlag", DbType.String, r.PeriodFlag.Value)
                db.AddInParameter(dbcmd, "@ValidFrom", DbType.Date, IIf(IsDateTimeNull(r.ValidFrom.Value), Convert.ToDateTime("1900/1/1"), r.ValidFrom.Value))
                db.AddInParameter(dbcmd, "@ValidTo", DbType.Date, IIf(IsDateTimeNull(r.ValidTo.Value), Convert.ToDateTime("1900/1/1"), r.ValidTo.Value))
                db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@Ex_Amount", DbType.Int32, r.Ex_Amount.Value)
                db.AddInParameter(dbcmd, "@Z_Amount", DbType.String, r.Z_Amount.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Private Function IsDateTimeNull(ByVal Src As DateTime) As Boolean
            If Src = Convert.ToDateTime("1900/1/1") OrElse _
               Src = Convert.ToDateTime("0001/1/1") Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class
End Namespace

