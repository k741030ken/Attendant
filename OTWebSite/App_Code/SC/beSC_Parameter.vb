'****************************************************************
' Table:SC_Parameter
' Created Date: 2014.08.06
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beSC_Parameter
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CloseFlag", "ProductionFlag", "MailDefaultSubject" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String) }
        Private m_PrimaryFields As String() = {  }

        Public ReadOnly Property Rows() As beSC_Parameter.Rows 
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
        Public Sub Transfer2Row(SC_ParameterTable As DataTable)
            For Each dr As DataRow In SC_ParameterTable.Rows
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

                dr(m_Rows(i).CloseFlag.FieldName) = m_Rows(i).CloseFlag.Value
                dr(m_Rows(i).ProductionFlag.FieldName) = m_Rows(i).ProductionFlag.Value
                dr(m_Rows(i).MailDefaultSubject.FieldName) = m_Rows(i).MailDefaultSubject.Value

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

        Public Sub Add(SC_ParameterRow As Row)
            m_Rows.Add(SC_ParameterRow)
        End Sub

        Public Sub Remove(SC_ParameterRow As Row)
            If m_Rows.IndexOf(SC_ParameterRow) >= 0 Then
                m_Rows.Remove(SC_ParameterRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CloseFlag As Field(Of String) = new Field(Of String)("CloseFlag", true)
        Private FI_ProductionFlag As Field(Of String) = new Field(Of String)("ProductionFlag", true)
        Private FI_MailDefaultSubject As Field(Of String) = new Field(Of String)("MailDefaultSubject", true)
        Private m_FieldNames As String() = { "CloseFlag", "ProductionFlag", "MailDefaultSubject" }
        Private m_PrimaryFields As String() = {  }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CloseFlag"
                    Return FI_CloseFlag.Value
                Case "ProductionFlag"
                    Return FI_ProductionFlag.Value
                Case "MailDefaultSubject"
                    Return FI_MailDefaultSubject.Value
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Sub SetFieldValue(ByVal fieldName As String, ByVal value As Object)
            Select Case fieldName
                Case "CloseFlag"
                    FI_CloseFlag.SetValue(value)
                Case "ProductionFlag"
                    FI_ProductionFlag.SetValue(value)
                Case "MailDefaultSubject"
                    FI_MailDefaultSubject.SetValue(value)
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
                Case "CloseFlag"
                    return FI_CloseFlag.Updated
                Case "ProductionFlag"
                    return FI_ProductionFlag.Updated
                Case "MailDefaultSubject"
                    return FI_MailDefaultSubject.Updated
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public Function CreateUpdateSQL(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "CloseFlag"
                    return FI_CloseFlag.CreateUpdateSQL
                Case "ProductionFlag"
                    return FI_ProductionFlag.CreateUpdateSQL
                Case "MailDefaultSubject"
                    return FI_MailDefaultSubject.CreateUpdateSQL
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
            FI_CloseFlag.SetInitValue("")
            FI_ProductionFlag.SetInitValue("")
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CloseFlag.SetInitValue(dr("CloseFlag"))
            FI_ProductionFlag.SetInitValue(dr("ProductionFlag"))
            FI_MailDefaultSubject.SetInitValue(dr("MailDefaultSubject"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CloseFlag.Updated = False
            FI_ProductionFlag.Updated = False
            FI_MailDefaultSubject.Updated = False
        End Sub

        Public ReadOnly Property CloseFlag As Field(Of String) 
            Get
                Return FI_CloseFlag
            End Get
        End Property

        Public ReadOnly Property ProductionFlag As Field(Of String) 
            Get
                Return FI_ProductionFlag
            End Get
        End Property

        Public ReadOnly Property MailDefaultSubject As Field(Of String) 
            Get
                Return FI_MailDefaultSubject
            End Get
        End Property

    End Class

    Public Class Service
        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From SC_Parameter")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal SC_ParameterRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_Parameter")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CloseFlag, ProductionFlag, MailDefaultSubject")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CloseFlag, @ProductionFlag, @MailDefaultSubject")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CloseFlag", DbType.String, SC_ParameterRow.CloseFlag.Value)
            db.AddInParameter(dbcmd, "@ProductionFlag", DbType.String, SC_ParameterRow.ProductionFlag.Value)
            db.AddInParameter(dbcmd, "@MailDefaultSubject", DbType.String, SC_ParameterRow.MailDefaultSubject.Value)

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal SC_ParameterRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into SC_Parameter")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CloseFlag, ProductionFlag, MailDefaultSubject")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CloseFlag, @ProductionFlag, @MailDefaultSubject")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CloseFlag", DbType.String, SC_ParameterRow.CloseFlag.Value)
            db.AddInParameter(dbcmd, "@ProductionFlag", DbType.String, SC_ParameterRow.ProductionFlag.Value)
            db.AddInParameter(dbcmd, "@MailDefaultSubject", DbType.String, SC_ParameterRow.MailDefaultSubject.Value)

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal SC_ParameterRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_Parameter")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CloseFlag, ProductionFlag, MailDefaultSubject")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CloseFlag, @ProductionFlag, @MailDefaultSubject")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In SC_ParameterRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CloseFlag", DbType.String, r.CloseFlag.Value)
                        db.AddInParameter(dbcmd, "@ProductionFlag", DbType.String, r.ProductionFlag.Value)
                        db.AddInParameter(dbcmd, "@MailDefaultSubject", DbType.String, r.MailDefaultSubject.Value)

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

        Public Function Insert(ByVal SC_ParameterRow As Row(), ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into SC_Parameter")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CloseFlag, ProductionFlag, MailDefaultSubject")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CloseFlag, @ProductionFlag, @MailDefaultSubject")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In SC_ParameterRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CloseFlag", DbType.String, r.CloseFlag.Value)
                db.AddInParameter(dbcmd, "@ProductionFlag", DbType.String, r.ProductionFlag.Value)
                db.AddInParameter(dbcmd, "@MailDefaultSubject", DbType.String, r.MailDefaultSubject.Value)

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

