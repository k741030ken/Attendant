'****************************************************************
' Table:EmpAccount
' Created Date: 2014.09.25
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beEmpAccount
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "EmpID", "BankID", "Account", "PrincipalFlag", "AccountRatio", "CreateComp", "CreateID", "CreateDate", "LastChgComp" _
                                    , "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Integer), GetType(String), GetType(String), GetType(Date), GetType(String) _
                                    , GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "CompID", "EmpID", "BankID" }

        Public ReadOnly Property Rows() As beEmpAccount.Rows 
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
        Public Sub Transfer2Row(EmpAccountTable As DataTable)
            For Each dr As DataRow In EmpAccountTable.Rows
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
                dr(m_Rows(i).BankID.FieldName) = m_Rows(i).BankID.Value
                dr(m_Rows(i).Account.FieldName) = m_Rows(i).Account.Value
                dr(m_Rows(i).PrincipalFlag.FieldName) = m_Rows(i).PrincipalFlag.Value
                dr(m_Rows(i).AccountRatio.FieldName) = m_Rows(i).AccountRatio.Value
                dr(m_Rows(i).CreateComp.FieldName) = m_Rows(i).CreateComp.Value
                dr(m_Rows(i).CreateID.FieldName) = m_Rows(i).CreateID.Value
                dr(m_Rows(i).CreateDate.FieldName) = m_Rows(i).CreateDate.Value
                dr(m_Rows(i).LastChgComp.FieldName) = m_Rows(i).LastChgComp.Value
                dr(m_Rows(i).LastChgID.FieldName) = m_Rows(i).LastChgID.Value
                dr(m_Rows(i).LastChgDate.FieldName) = m_Rows(i).LastChgDate.Value

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

        Public Sub Add(EmpAccountRow As Row)
            m_Rows.Add(EmpAccountRow)
        End Sub

        Public Sub Remove(EmpAccountRow As Row)
            If m_Rows.IndexOf(EmpAccountRow) >= 0 Then
                m_Rows.Remove(EmpAccountRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_EmpID As Field(Of String) = new Field(Of String)("EmpID", true)
        Private FI_BankID As Field(Of String) = new Field(Of String)("BankID", true)
        Private FI_Account As Field(Of String) = new Field(Of String)("Account", true)
        Private FI_PrincipalFlag As Field(Of String) = new Field(Of String)("PrincipalFlag", true)
        Private FI_AccountRatio As Field(Of Integer) = new Field(Of Integer)("AccountRatio", true)
        Private FI_CreateComp As Field(Of String) = new Field(Of String)("CreateComp", true)
        Private FI_CreateID As Field(Of String) = new Field(Of String)("CreateID", true)
        Private FI_CreateDate As Field(Of Date) = new Field(Of Date)("CreateDate", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "CompID", "EmpID", "BankID", "Account", "PrincipalFlag", "AccountRatio", "CreateComp", "CreateID", "CreateDate", "LastChgComp" _
                                    , "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "CompID", "EmpID", "BankID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CompID"
                    Return FI_CompID.Value
                Case "EmpID"
                    Return FI_EmpID.Value
                Case "BankID"
                    Return FI_BankID.Value
                Case "Account"
                    Return FI_Account.Value
                Case "PrincipalFlag"
                    Return FI_PrincipalFlag.Value
                Case "AccountRatio"
                    Return FI_AccountRatio.Value
                Case "CreateComp"
                    Return FI_CreateComp.Value
                Case "CreateID"
                    Return FI_CreateID.Value
                Case "CreateDate"
                    Return FI_CreateDate.Value
                Case "LastChgComp"
                    Return FI_LastChgComp.Value
                Case "LastChgID"
                    Return FI_LastChgID.Value
                Case "LastChgDate"
                    Return FI_LastChgDate.Value
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
                Case "BankID"
                    FI_BankID.SetValue(value)
                Case "Account"
                    FI_Account.SetValue(value)
                Case "PrincipalFlag"
                    FI_PrincipalFlag.SetValue(value)
                Case "AccountRatio"
                    FI_AccountRatio.SetValue(value)
                Case "CreateComp"
                    FI_CreateComp.SetValue(value)
                Case "CreateID"
                    FI_CreateID.SetValue(value)
                Case "CreateDate"
                    FI_CreateDate.SetValue(value)
                Case "LastChgComp"
                    FI_LastChgComp.SetValue(value)
                Case "LastChgID"
                    FI_LastChgID.SetValue(value)
                Case "LastChgDate"
                    FI_LastChgDate.SetValue(value)
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
                Case "BankID"
                    return FI_BankID.Updated
                Case "Account"
                    return FI_Account.Updated
                Case "PrincipalFlag"
                    return FI_PrincipalFlag.Updated
                Case "AccountRatio"
                    return FI_AccountRatio.Updated
                Case "CreateComp"
                    return FI_CreateComp.Updated
                Case "CreateID"
                    return FI_CreateID.Updated
                Case "CreateDate"
                    return FI_CreateDate.Updated
                Case "LastChgComp"
                    return FI_LastChgComp.Updated
                Case "LastChgID"
                    return FI_LastChgID.Updated
                Case "LastChgDate"
                    return FI_LastChgDate.Updated
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
                Case "BankID"
                    return FI_BankID.CreateUpdateSQL
                Case "Account"
                    return FI_Account.CreateUpdateSQL
                Case "PrincipalFlag"
                    return FI_PrincipalFlag.CreateUpdateSQL
                Case "AccountRatio"
                    return FI_AccountRatio.CreateUpdateSQL
                Case "CreateComp"
                    return FI_CreateComp.CreateUpdateSQL
                Case "CreateID"
                    return FI_CreateID.CreateUpdateSQL
                Case "CreateDate"
                    return FI_CreateDate.CreateUpdateSQL
                Case "LastChgComp"
                    return FI_LastChgComp.CreateUpdateSQL
                Case "LastChgID"
                    return FI_LastChgID.CreateUpdateSQL
                Case "LastChgDate"
                    return FI_LastChgDate.CreateUpdateSQL
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
            FI_BankID.SetInitValue("")
            FI_Account.SetInitValue("")
            FI_PrincipalFlag.SetInitValue("0")
            FI_AccountRatio.SetInitValue(0)
            FI_CreateComp.SetInitValue("")
            FI_CreateID.SetInitValue("")
            FI_CreateDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CompID.SetInitValue(dr("CompID"))
            FI_EmpID.SetInitValue(dr("EmpID"))
            FI_BankID.SetInitValue(dr("BankID"))
            FI_Account.SetInitValue(dr("Account"))
            FI_PrincipalFlag.SetInitValue(dr("PrincipalFlag"))
            FI_AccountRatio.SetInitValue(dr("AccountRatio"))
            FI_CreateComp.SetInitValue(dr("CreateComp"))
            FI_CreateID.SetInitValue(dr("CreateID"))
            FI_CreateDate.SetInitValue(dr("CreateDate"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CompID.Updated = False
            FI_EmpID.Updated = False
            FI_BankID.Updated = False
            FI_Account.Updated = False
            FI_PrincipalFlag.Updated = False
            FI_AccountRatio.Updated = False
            FI_CreateComp.Updated = False
            FI_CreateID.Updated = False
            FI_CreateDate.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
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

        Public ReadOnly Property BankID As Field(Of String) 
            Get
                Return FI_BankID
            End Get
        End Property

        Public ReadOnly Property Account As Field(Of String) 
            Get
                Return FI_Account
            End Get
        End Property

        Public ReadOnly Property PrincipalFlag As Field(Of String) 
            Get
                Return FI_PrincipalFlag
            End Get
        End Property

        Public ReadOnly Property AccountRatio As Field(Of Integer) 
            Get
                Return FI_AccountRatio
            End Get
        End Property

        Public ReadOnly Property CreateComp As Field(Of String) 
            Get
                Return FI_CreateComp
            End Get
        End Property

        Public ReadOnly Property CreateID As Field(Of String) 
            Get
                Return FI_CreateID
            End Get
        End Property

        Public ReadOnly Property CreateDate As Field(Of Date) 
            Get
                Return FI_CreateDate
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

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal EmpAccountRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From EmpAccount")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And BankID = @BankID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpAccountRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpAccountRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@BankID", DbType.String, EmpAccountRow.BankID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal EmpAccountRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From EmpAccount")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And BankID = @BankID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpAccountRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpAccountRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@BankID", DbType.String, EmpAccountRow.BankID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal EmpAccountRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From EmpAccount")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And BankID = @BankID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In EmpAccountRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@BankID", DbType.String, r.BankID.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal EmpAccountRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From EmpAccount")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And BankID = @BankID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In EmpAccountRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@BankID", DbType.String, r.BankID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal EmpAccountRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmpAccount")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And BankID = @BankID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpAccountRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpAccountRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@BankID", DbType.String, EmpAccountRow.BankID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(EmpAccountRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmpAccount")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And BankID = @BankID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpAccountRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpAccountRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@BankID", DbType.String, EmpAccountRow.BankID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal EmpAccountRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update EmpAccount Set")
            For i As Integer = 0 To EmpAccountRow.FieldNames.Length - 1
                If Not EmpAccountRow.IsIdentityField(EmpAccountRow.FieldNames(i)) AndAlso EmpAccountRow.IsUpdated(EmpAccountRow.FieldNames(i)) AndAlso EmpAccountRow.CreateUpdateSQL(EmpAccountRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, EmpAccountRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And EmpID = @PKEmpID")
            strSQL.AppendLine("And BankID = @PKBankID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If EmpAccountRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpAccountRow.CompID.Value)
            If EmpAccountRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpAccountRow.EmpID.Value)
            If EmpAccountRow.BankID.Updated Then db.AddInParameter(dbcmd, "@BankID", DbType.String, EmpAccountRow.BankID.Value)
            If EmpAccountRow.Account.Updated Then db.AddInParameter(dbcmd, "@Account", DbType.String, EmpAccountRow.Account.Value)
            If EmpAccountRow.PrincipalFlag.Updated Then db.AddInParameter(dbcmd, "@PrincipalFlag", DbType.String, EmpAccountRow.PrincipalFlag.Value)
            If EmpAccountRow.AccountRatio.Updated Then db.AddInParameter(dbcmd, "@AccountRatio", DbType.Int32, EmpAccountRow.AccountRatio.Value)
            If EmpAccountRow.CreateComp.Updated Then db.AddInParameter(dbcmd, "@CreateComp", DbType.String, EmpAccountRow.CreateComp.Value)
            If EmpAccountRow.CreateID.Updated Then db.AddInParameter(dbcmd, "@CreateID", DbType.String, EmpAccountRow.CreateID.Value)
            If EmpAccountRow.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(EmpAccountRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), EmpAccountRow.CreateDate.Value))
            If EmpAccountRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpAccountRow.LastChgComp.Value)
            If EmpAccountRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpAccountRow.LastChgID.Value)
            If EmpAccountRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpAccountRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpAccountRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(EmpAccountRow.LoadFromDataRow, EmpAccountRow.CompID.OldValue, EmpAccountRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(EmpAccountRow.LoadFromDataRow, EmpAccountRow.EmpID.OldValue, EmpAccountRow.EmpID.Value))
            db.AddInParameter(dbcmd, "@PKBankID", DbType.String, IIf(EmpAccountRow.LoadFromDataRow, EmpAccountRow.BankID.OldValue, EmpAccountRow.BankID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal EmpAccountRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update EmpAccount Set")
            For i As Integer = 0 To EmpAccountRow.FieldNames.Length - 1
                If Not EmpAccountRow.IsIdentityField(EmpAccountRow.FieldNames(i)) AndAlso EmpAccountRow.IsUpdated(EmpAccountRow.FieldNames(i)) AndAlso EmpAccountRow.CreateUpdateSQL(EmpAccountRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, EmpAccountRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And EmpID = @PKEmpID")
            strSQL.AppendLine("And BankID = @PKBankID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If EmpAccountRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpAccountRow.CompID.Value)
            If EmpAccountRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpAccountRow.EmpID.Value)
            If EmpAccountRow.BankID.Updated Then db.AddInParameter(dbcmd, "@BankID", DbType.String, EmpAccountRow.BankID.Value)
            If EmpAccountRow.Account.Updated Then db.AddInParameter(dbcmd, "@Account", DbType.String, EmpAccountRow.Account.Value)
            If EmpAccountRow.PrincipalFlag.Updated Then db.AddInParameter(dbcmd, "@PrincipalFlag", DbType.String, EmpAccountRow.PrincipalFlag.Value)
            If EmpAccountRow.AccountRatio.Updated Then db.AddInParameter(dbcmd, "@AccountRatio", DbType.Int32, EmpAccountRow.AccountRatio.Value)
            If EmpAccountRow.CreateComp.Updated Then db.AddInParameter(dbcmd, "@CreateComp", DbType.String, EmpAccountRow.CreateComp.Value)
            If EmpAccountRow.CreateID.Updated Then db.AddInParameter(dbcmd, "@CreateID", DbType.String, EmpAccountRow.CreateID.Value)
            If EmpAccountRow.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(EmpAccountRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), EmpAccountRow.CreateDate.Value))
            If EmpAccountRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpAccountRow.LastChgComp.Value)
            If EmpAccountRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpAccountRow.LastChgID.Value)
            If EmpAccountRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpAccountRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpAccountRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(EmpAccountRow.LoadFromDataRow, EmpAccountRow.CompID.OldValue, EmpAccountRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(EmpAccountRow.LoadFromDataRow, EmpAccountRow.EmpID.OldValue, EmpAccountRow.EmpID.Value))
            db.AddInParameter(dbcmd, "@PKBankID", DbType.String, IIf(EmpAccountRow.LoadFromDataRow, EmpAccountRow.BankID.OldValue, EmpAccountRow.BankID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal EmpAccountRow As Row()) As Integer
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
                    For Each r As Row In EmpAccountRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update EmpAccount Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where CompID = @PKCompID")
                        strSQL.AppendLine("And EmpID = @PKEmpID")
                        strSQL.AppendLine("And BankID = @PKBankID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        If r.BankID.Updated Then db.AddInParameter(dbcmd, "@BankID", DbType.String, r.BankID.Value)
                        If r.Account.Updated Then db.AddInParameter(dbcmd, "@Account", DbType.String, r.Account.Value)
                        If r.PrincipalFlag.Updated Then db.AddInParameter(dbcmd, "@PrincipalFlag", DbType.String, r.PrincipalFlag.Value)
                        If r.AccountRatio.Updated Then db.AddInParameter(dbcmd, "@AccountRatio", DbType.Int32, r.AccountRatio.Value)
                        If r.CreateComp.Updated Then db.AddInParameter(dbcmd, "@CreateComp", DbType.String, r.CreateComp.Value)
                        If r.CreateID.Updated Then db.AddInParameter(dbcmd, "@CreateID", DbType.String, r.CreateID.Value)
                        If r.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                        db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))
                        db.AddInParameter(dbcmd, "@PKBankID", DbType.String, IIf(r.LoadFromDataRow, r.BankID.OldValue, r.BankID.Value))

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

        Public Function Update(ByVal EmpAccountRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In EmpAccountRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update EmpAccount Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where CompID = @PKCompID")
                strSQL.AppendLine("And EmpID = @PKEmpID")
                strSQL.AppendLine("And BankID = @PKBankID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                If r.BankID.Updated Then db.AddInParameter(dbcmd, "@BankID", DbType.String, r.BankID.Value)
                If r.Account.Updated Then db.AddInParameter(dbcmd, "@Account", DbType.String, r.Account.Value)
                If r.PrincipalFlag.Updated Then db.AddInParameter(dbcmd, "@PrincipalFlag", DbType.String, r.PrincipalFlag.Value)
                If r.AccountRatio.Updated Then db.AddInParameter(dbcmd, "@AccountRatio", DbType.Int32, r.AccountRatio.Value)
                If r.CreateComp.Updated Then db.AddInParameter(dbcmd, "@CreateComp", DbType.String, r.CreateComp.Value)
                If r.CreateID.Updated Then db.AddInParameter(dbcmd, "@CreateID", DbType.String, r.CreateID.Value)
                If r.CreateDate.Updated Then db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))
                db.AddInParameter(dbcmd, "@PKBankID", DbType.String, IIf(r.LoadFromDataRow, r.BankID.OldValue, r.BankID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal EmpAccountRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From EmpAccount")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And BankID = @BankID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpAccountRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpAccountRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@BankID", DbType.String, EmpAccountRow.BankID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal EmpAccountRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From EmpAccount")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And BankID = @BankID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpAccountRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpAccountRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@BankID", DbType.String, EmpAccountRow.BankID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmpAccount")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal EmpAccountRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into EmpAccount")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, BankID, Account, PrincipalFlag, AccountRatio, CreateComp, CreateID,")
            strSQL.AppendLine("    CreateDate, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @BankID, @Account, @PrincipalFlag, @AccountRatio, @CreateComp, @CreateID,")
            strSQL.AppendLine("    @CreateDate, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpAccountRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpAccountRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@BankID", DbType.String, EmpAccountRow.BankID.Value)
            db.AddInParameter(dbcmd, "@Account", DbType.String, EmpAccountRow.Account.Value)
            db.AddInParameter(dbcmd, "@PrincipalFlag", DbType.String, EmpAccountRow.PrincipalFlag.Value)
            db.AddInParameter(dbcmd, "@AccountRatio", DbType.Int32, EmpAccountRow.AccountRatio.Value)
            db.AddInParameter(dbcmd, "@CreateComp", DbType.String, EmpAccountRow.CreateComp.Value)
            db.AddInParameter(dbcmd, "@CreateID", DbType.String, EmpAccountRow.CreateID.Value)
            db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(EmpAccountRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), EmpAccountRow.CreateDate.Value))
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpAccountRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpAccountRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpAccountRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpAccountRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal EmpAccountRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into EmpAccount")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, BankID, Account, PrincipalFlag, AccountRatio, CreateComp, CreateID,")
            strSQL.AppendLine("    CreateDate, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @BankID, @Account, @PrincipalFlag, @AccountRatio, @CreateComp, @CreateID,")
            strSQL.AppendLine("    @CreateDate, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpAccountRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpAccountRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@BankID", DbType.String, EmpAccountRow.BankID.Value)
            db.AddInParameter(dbcmd, "@Account", DbType.String, EmpAccountRow.Account.Value)
            db.AddInParameter(dbcmd, "@PrincipalFlag", DbType.String, EmpAccountRow.PrincipalFlag.Value)
            db.AddInParameter(dbcmd, "@AccountRatio", DbType.Int32, EmpAccountRow.AccountRatio.Value)
            db.AddInParameter(dbcmd, "@CreateComp", DbType.String, EmpAccountRow.CreateComp.Value)
            db.AddInParameter(dbcmd, "@CreateID", DbType.String, EmpAccountRow.CreateID.Value)
            db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(EmpAccountRow.CreateDate.Value), Convert.ToDateTime("1900/1/1"), EmpAccountRow.CreateDate.Value))
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpAccountRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpAccountRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpAccountRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpAccountRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal EmpAccountRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into EmpAccount")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, BankID, Account, PrincipalFlag, AccountRatio, CreateComp, CreateID,")
            strSQL.AppendLine("    CreateDate, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @BankID, @Account, @PrincipalFlag, @AccountRatio, @CreateComp, @CreateID,")
            strSQL.AppendLine("    @CreateDate, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In EmpAccountRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@BankID", DbType.String, r.BankID.Value)
                        db.AddInParameter(dbcmd, "@Account", DbType.String, r.Account.Value)
                        db.AddInParameter(dbcmd, "@PrincipalFlag", DbType.String, r.PrincipalFlag.Value)
                        db.AddInParameter(dbcmd, "@AccountRatio", DbType.Int32, r.AccountRatio.Value)
                        db.AddInParameter(dbcmd, "@CreateComp", DbType.String, r.CreateComp.Value)
                        db.AddInParameter(dbcmd, "@CreateID", DbType.String, r.CreateID.Value)
                        db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                        db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))

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

        Public Function Insert(ByVal EmpAccountRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into EmpAccount")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, EmpID, BankID, Account, PrincipalFlag, AccountRatio, CreateComp, CreateID,")
            strSQL.AppendLine("    CreateDate, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @EmpID, @BankID, @Account, @PrincipalFlag, @AccountRatio, @CreateComp, @CreateID,")
            strSQL.AppendLine("    @CreateDate, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In EmpAccountRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@BankID", DbType.String, r.BankID.Value)
                db.AddInParameter(dbcmd, "@Account", DbType.String, r.Account.Value)
                db.AddInParameter(dbcmd, "@PrincipalFlag", DbType.String, r.PrincipalFlag.Value)
                db.AddInParameter(dbcmd, "@AccountRatio", DbType.Int32, r.AccountRatio.Value)
                db.AddInParameter(dbcmd, "@CreateComp", DbType.String, r.CreateComp.Value)
                db.AddInParameter(dbcmd, "@CreateID", DbType.String, r.CreateID.Value)
                db.AddInParameter(dbcmd, "@CreateDate", DbType.Date, IIf(IsDateTimeNull(r.CreateDate.Value), Convert.ToDateTime("1900/1/1"), r.CreateDate.Value))
                db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))

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

