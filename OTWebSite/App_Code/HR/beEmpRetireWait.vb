'****************************************************************
' Table:EmpRetireWait
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

Namespace beEmpRetireWait
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "ReleaseMark", "CompID", "EmpID", "ApplyDate", "Kind", "EmpRatio", "CompRatio", "ManagerFlag", "BossFlag", "Source" _
                                    , "Amount", "ReleaseDate", "ReleaseComp", "ReleaseEmpID", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(String), GetType(Date), GetType(String), GetType(Decimal), GetType(Decimal), GetType(String), GetType(String), GetType(String) _
                                    , GetType(String), GetType(Date), GetType(String), GetType(String), GetType(String), GetType(String), GetType(Date) }
        Private m_PrimaryFields As String() = { "CompID", "EmpID", "ApplyDate" }

        Public ReadOnly Property Rows() As beEmpRetireWait.Rows 
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
        Public Sub Transfer2Row(EmpRetireWaitTable As DataTable)
            For Each dr As DataRow In EmpRetireWaitTable.Rows
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

                dr(m_Rows(i).ReleaseMark.FieldName) = m_Rows(i).ReleaseMark.Value
                dr(m_Rows(i).CompID.FieldName) = m_Rows(i).CompID.Value
                dr(m_Rows(i).EmpID.FieldName) = m_Rows(i).EmpID.Value
                dr(m_Rows(i).ApplyDate.FieldName) = m_Rows(i).ApplyDate.Value
                dr(m_Rows(i).Kind.FieldName) = m_Rows(i).Kind.Value
                dr(m_Rows(i).EmpRatio.FieldName) = m_Rows(i).EmpRatio.Value
                dr(m_Rows(i).CompRatio.FieldName) = m_Rows(i).CompRatio.Value
                dr(m_Rows(i).ManagerFlag.FieldName) = m_Rows(i).ManagerFlag.Value
                dr(m_Rows(i).BossFlag.FieldName) = m_Rows(i).BossFlag.Value
                dr(m_Rows(i).Source.FieldName) = m_Rows(i).Source.Value
                dr(m_Rows(i).Amount.FieldName) = m_Rows(i).Amount.Value
                dr(m_Rows(i).ReleaseDate.FieldName) = m_Rows(i).ReleaseDate.Value
                dr(m_Rows(i).ReleaseComp.FieldName) = m_Rows(i).ReleaseComp.Value
                dr(m_Rows(i).ReleaseEmpID.FieldName) = m_Rows(i).ReleaseEmpID.Value
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

        Public Sub Add(EmpRetireWaitRow As Row)
            m_Rows.Add(EmpRetireWaitRow)
        End Sub

        Public Sub Remove(EmpRetireWaitRow As Row)
            If m_Rows.IndexOf(EmpRetireWaitRow) >= 0 Then
                m_Rows.Remove(EmpRetireWaitRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_ReleaseMark As Field(Of String) = new Field(Of String)("ReleaseMark", true)
        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_EmpID As Field(Of String) = new Field(Of String)("EmpID", true)
        Private FI_ApplyDate As Field(Of Date) = new Field(Of Date)("ApplyDate", true)
        Private FI_Kind As Field(Of String) = new Field(Of String)("Kind", true)
        Private FI_EmpRatio As Field(Of Decimal) = new Field(Of Decimal)("EmpRatio", true)
        Private FI_CompRatio As Field(Of Decimal) = new Field(Of Decimal)("CompRatio", true)
        Private FI_ManagerFlag As Field(Of String) = new Field(Of String)("ManagerFlag", true)
        Private FI_BossFlag As Field(Of String) = new Field(Of String)("BossFlag", true)
        Private FI_Source As Field(Of String) = new Field(Of String)("Source", true)
        Private FI_Amount As Field(Of String) = new Field(Of String)("Amount", true)
        Private FI_ReleaseDate As Field(Of Date) = new Field(Of Date)("ReleaseDate", true)
        Private FI_ReleaseComp As Field(Of String) = new Field(Of String)("ReleaseComp", true)
        Private FI_ReleaseEmpID As Field(Of String) = new Field(Of String)("ReleaseEmpID", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private m_FieldNames As String() = { "ReleaseMark", "CompID", "EmpID", "ApplyDate", "Kind", "EmpRatio", "CompRatio", "ManagerFlag", "BossFlag", "Source" _
                                    , "Amount", "ReleaseDate", "ReleaseComp", "ReleaseEmpID", "LastChgComp", "LastChgID", "LastChgDate" }
        Private m_PrimaryFields As String() = { "CompID", "EmpID", "ApplyDate" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "ReleaseMark"
                    Return FI_ReleaseMark.Value
                Case "CompID"
                    Return FI_CompID.Value
                Case "EmpID"
                    Return FI_EmpID.Value
                Case "ApplyDate"
                    Return FI_ApplyDate.Value
                Case "Kind"
                    Return FI_Kind.Value
                Case "EmpRatio"
                    Return FI_EmpRatio.Value
                Case "CompRatio"
                    Return FI_CompRatio.Value
                Case "ManagerFlag"
                    Return FI_ManagerFlag.Value
                Case "BossFlag"
                    Return FI_BossFlag.Value
                Case "Source"
                    Return FI_Source.Value
                Case "Amount"
                    Return FI_Amount.Value
                Case "ReleaseDate"
                    Return FI_ReleaseDate.Value
                Case "ReleaseComp"
                    Return FI_ReleaseComp.Value
                Case "ReleaseEmpID"
                    Return FI_ReleaseEmpID.Value
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
                Case "ReleaseMark"
                    FI_ReleaseMark.SetValue(value)
                Case "CompID"
                    FI_CompID.SetValue(value)
                Case "EmpID"
                    FI_EmpID.SetValue(value)
                Case "ApplyDate"
                    FI_ApplyDate.SetValue(value)
                Case "Kind"
                    FI_Kind.SetValue(value)
                Case "EmpRatio"
                    FI_EmpRatio.SetValue(value)
                Case "CompRatio"
                    FI_CompRatio.SetValue(value)
                Case "ManagerFlag"
                    FI_ManagerFlag.SetValue(value)
                Case "BossFlag"
                    FI_BossFlag.SetValue(value)
                Case "Source"
                    FI_Source.SetValue(value)
                Case "Amount"
                    FI_Amount.SetValue(value)
                Case "ReleaseDate"
                    FI_ReleaseDate.SetValue(value)
                Case "ReleaseComp"
                    FI_ReleaseComp.SetValue(value)
                Case "ReleaseEmpID"
                    FI_ReleaseEmpID.SetValue(value)
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
                Case "ReleaseMark"
                    return FI_ReleaseMark.Updated
                Case "CompID"
                    return FI_CompID.Updated
                Case "EmpID"
                    return FI_EmpID.Updated
                Case "ApplyDate"
                    return FI_ApplyDate.Updated
                Case "Kind"
                    return FI_Kind.Updated
                Case "EmpRatio"
                    return FI_EmpRatio.Updated
                Case "CompRatio"
                    return FI_CompRatio.Updated
                Case "ManagerFlag"
                    return FI_ManagerFlag.Updated
                Case "BossFlag"
                    return FI_BossFlag.Updated
                Case "Source"
                    return FI_Source.Updated
                Case "Amount"
                    return FI_Amount.Updated
                Case "ReleaseDate"
                    return FI_ReleaseDate.Updated
                Case "ReleaseComp"
                    return FI_ReleaseComp.Updated
                Case "ReleaseEmpID"
                    return FI_ReleaseEmpID.Updated
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
                Case "ReleaseMark"
                    return FI_ReleaseMark.CreateUpdateSQL
                Case "CompID"
                    return FI_CompID.CreateUpdateSQL
                Case "EmpID"
                    return FI_EmpID.CreateUpdateSQL
                Case "ApplyDate"
                    return FI_ApplyDate.CreateUpdateSQL
                Case "Kind"
                    return FI_Kind.CreateUpdateSQL
                Case "EmpRatio"
                    return FI_EmpRatio.CreateUpdateSQL
                Case "CompRatio"
                    return FI_CompRatio.CreateUpdateSQL
                Case "ManagerFlag"
                    return FI_ManagerFlag.CreateUpdateSQL
                Case "BossFlag"
                    return FI_BossFlag.CreateUpdateSQL
                Case "Source"
                    return FI_Source.CreateUpdateSQL
                Case "Amount"
                    return FI_Amount.CreateUpdateSQL
                Case "ReleaseDate"
                    return FI_ReleaseDate.CreateUpdateSQL
                Case "ReleaseComp"
                    return FI_ReleaseComp.CreateUpdateSQL
                Case "ReleaseEmpID"
                    return FI_ReleaseEmpID.CreateUpdateSQL
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
            FI_ReleaseMark.SetInitValue("0")
            FI_CompID.SetInitValue("")
            FI_EmpID.SetInitValue("")
            FI_ApplyDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_Kind.SetInitValue("")
            FI_EmpRatio.SetInitValue(0)
            FI_CompRatio.SetInitValue(6)
            FI_ManagerFlag.SetInitValue("0")
            FI_BossFlag.SetInitValue("0")
            FI_Source.SetInitValue("")
            FI_Amount.SetInitValue("")
            FI_ReleaseDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_ReleaseComp.SetInitValue("")
            FI_ReleaseEmpID.SetInitValue("")
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_ReleaseMark.SetInitValue(dr("ReleaseMark"))
            FI_CompID.SetInitValue(dr("CompID"))
            FI_EmpID.SetInitValue(dr("EmpID"))
            FI_ApplyDate.SetInitValue(dr("ApplyDate"))
            FI_Kind.SetInitValue(dr("Kind"))
            FI_EmpRatio.SetInitValue(dr("EmpRatio"))
            FI_CompRatio.SetInitValue(dr("CompRatio"))
            FI_ManagerFlag.SetInitValue(dr("ManagerFlag"))
            FI_BossFlag.SetInitValue(dr("BossFlag"))
            FI_Source.SetInitValue(dr("Source"))
            FI_Amount.SetInitValue(dr("Amount"))
            FI_ReleaseDate.SetInitValue(dr("ReleaseDate"))
            FI_ReleaseComp.SetInitValue(dr("ReleaseComp"))
            FI_ReleaseEmpID.SetInitValue(dr("ReleaseEmpID"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_ReleaseMark.Updated = False
            FI_CompID.Updated = False
            FI_EmpID.Updated = False
            FI_ApplyDate.Updated = False
            FI_Kind.Updated = False
            FI_EmpRatio.Updated = False
            FI_CompRatio.Updated = False
            FI_ManagerFlag.Updated = False
            FI_BossFlag.Updated = False
            FI_Source.Updated = False
            FI_Amount.Updated = False
            FI_ReleaseDate.Updated = False
            FI_ReleaseComp.Updated = False
            FI_ReleaseEmpID.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
        End Sub

        Public ReadOnly Property ReleaseMark As Field(Of String) 
            Get
                Return FI_ReleaseMark
            End Get
        End Property

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

        Public ReadOnly Property ApplyDate As Field(Of Date) 
            Get
                Return FI_ApplyDate
            End Get
        End Property

        Public ReadOnly Property Kind As Field(Of String) 
            Get
                Return FI_Kind
            End Get
        End Property

        Public ReadOnly Property EmpRatio As Field(Of Decimal) 
            Get
                Return FI_EmpRatio
            End Get
        End Property

        Public ReadOnly Property CompRatio As Field(Of Decimal) 
            Get
                Return FI_CompRatio
            End Get
        End Property

        Public ReadOnly Property ManagerFlag As Field(Of String) 
            Get
                Return FI_ManagerFlag
            End Get
        End Property

        Public ReadOnly Property BossFlag As Field(Of String) 
            Get
                Return FI_BossFlag
            End Get
        End Property

        Public ReadOnly Property Source As Field(Of String) 
            Get
                Return FI_Source
            End Get
        End Property

        Public ReadOnly Property Amount As Field(Of String) 
            Get
                Return FI_Amount
            End Get
        End Property

        Public ReadOnly Property ReleaseDate As Field(Of Date) 
            Get
                Return FI_ReleaseDate
            End Get
        End Property

        Public ReadOnly Property ReleaseComp As Field(Of String) 
            Get
                Return FI_ReleaseComp
            End Get
        End Property

        Public ReadOnly Property ReleaseEmpID As Field(Of String) 
            Get
                Return FI_ReleaseEmpID
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
        Public Function DeleteRowByPrimaryKey(ByVal EmpRetireWaitRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From EmpRetireWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And ApplyDate = @ApplyDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpRetireWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpRetireWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, EmpRetireWaitRow.ApplyDate.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal EmpRetireWaitRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From EmpRetireWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And ApplyDate = @ApplyDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpRetireWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpRetireWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, EmpRetireWaitRow.ApplyDate.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal EmpRetireWaitRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From EmpRetireWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And ApplyDate = @ApplyDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In EmpRetireWaitRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, r.ApplyDate.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal EmpRetireWaitRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From EmpRetireWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And ApplyDate = @ApplyDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In EmpRetireWaitRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, r.ApplyDate.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal EmpRetireWaitRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmpRetireWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And ApplyDate = @ApplyDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpRetireWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpRetireWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, EmpRetireWaitRow.ApplyDate.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(EmpRetireWaitRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmpRetireWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And ApplyDate = @ApplyDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpRetireWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpRetireWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, EmpRetireWaitRow.ApplyDate.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal EmpRetireWaitRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update EmpRetireWait Set")
            For i As Integer = 0 To EmpRetireWaitRow.FieldNames.Length - 1
                If Not EmpRetireWaitRow.IsIdentityField(EmpRetireWaitRow.FieldNames(i)) AndAlso EmpRetireWaitRow.IsUpdated(EmpRetireWaitRow.FieldNames(i)) AndAlso EmpRetireWaitRow.CreateUpdateSQL(EmpRetireWaitRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, EmpRetireWaitRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And EmpID = @PKEmpID")
            strSQL.AppendLine("And ApplyDate = @PKApplyDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If EmpRetireWaitRow.ReleaseMark.Updated Then db.AddInParameter(dbcmd, "@ReleaseMark", DbType.String, EmpRetireWaitRow.ReleaseMark.Value)
            If EmpRetireWaitRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpRetireWaitRow.CompID.Value)
            If EmpRetireWaitRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpRetireWaitRow.EmpID.Value)
            If EmpRetireWaitRow.ApplyDate.Updated Then db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, IIf(IsDateTimeNull(EmpRetireWaitRow.ApplyDate.Value), Convert.ToDateTime("1900/1/1"), EmpRetireWaitRow.ApplyDate.Value))
            If EmpRetireWaitRow.Kind.Updated Then db.AddInParameter(dbcmd, "@Kind", DbType.String, EmpRetireWaitRow.Kind.Value)
            If EmpRetireWaitRow.EmpRatio.Updated Then db.AddInParameter(dbcmd, "@EmpRatio", DbType.Decimal, EmpRetireWaitRow.EmpRatio.Value)
            If EmpRetireWaitRow.CompRatio.Updated Then db.AddInParameter(dbcmd, "@CompRatio", DbType.Decimal, EmpRetireWaitRow.CompRatio.Value)
            If EmpRetireWaitRow.ManagerFlag.Updated Then db.AddInParameter(dbcmd, "@ManagerFlag", DbType.String, EmpRetireWaitRow.ManagerFlag.Value)
            If EmpRetireWaitRow.BossFlag.Updated Then db.AddInParameter(dbcmd, "@BossFlag", DbType.String, EmpRetireWaitRow.BossFlag.Value)
            If EmpRetireWaitRow.Source.Updated Then db.AddInParameter(dbcmd, "@Source", DbType.String, EmpRetireWaitRow.Source.Value)
            If EmpRetireWaitRow.Amount.Updated Then db.AddInParameter(dbcmd, "@Amount", DbType.String, EmpRetireWaitRow.Amount.Value)
            If EmpRetireWaitRow.ReleaseDate.Updated Then db.AddInParameter(dbcmd, "@ReleaseDate", DbType.Date, IIf(IsDateTimeNull(EmpRetireWaitRow.ReleaseDate.Value), Convert.ToDateTime("1900/1/1"), EmpRetireWaitRow.ReleaseDate.Value))
            If EmpRetireWaitRow.ReleaseComp.Updated Then db.AddInParameter(dbcmd, "@ReleaseComp", DbType.String, EmpRetireWaitRow.ReleaseComp.Value)
            If EmpRetireWaitRow.ReleaseEmpID.Updated Then db.AddInParameter(dbcmd, "@ReleaseEmpID", DbType.String, EmpRetireWaitRow.ReleaseEmpID.Value)
            If EmpRetireWaitRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpRetireWaitRow.LastChgComp.Value)
            If EmpRetireWaitRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpRetireWaitRow.LastChgID.Value)
            If EmpRetireWaitRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpRetireWaitRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpRetireWaitRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(EmpRetireWaitRow.LoadFromDataRow, EmpRetireWaitRow.CompID.OldValue, EmpRetireWaitRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(EmpRetireWaitRow.LoadFromDataRow, EmpRetireWaitRow.EmpID.OldValue, EmpRetireWaitRow.EmpID.Value))
            db.AddInParameter(dbcmd, "@PKApplyDate", DbType.Date, IIf(EmpRetireWaitRow.LoadFromDataRow, EmpRetireWaitRow.ApplyDate.OldValue, EmpRetireWaitRow.ApplyDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal EmpRetireWaitRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update EmpRetireWait Set")
            For i As Integer = 0 To EmpRetireWaitRow.FieldNames.Length - 1
                If Not EmpRetireWaitRow.IsIdentityField(EmpRetireWaitRow.FieldNames(i)) AndAlso EmpRetireWaitRow.IsUpdated(EmpRetireWaitRow.FieldNames(i)) AndAlso EmpRetireWaitRow.CreateUpdateSQL(EmpRetireWaitRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, EmpRetireWaitRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And EmpID = @PKEmpID")
            strSQL.AppendLine("And ApplyDate = @PKApplyDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If EmpRetireWaitRow.ReleaseMark.Updated Then db.AddInParameter(dbcmd, "@ReleaseMark", DbType.String, EmpRetireWaitRow.ReleaseMark.Value)
            If EmpRetireWaitRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpRetireWaitRow.CompID.Value)
            If EmpRetireWaitRow.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpRetireWaitRow.EmpID.Value)
            If EmpRetireWaitRow.ApplyDate.Updated Then db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, IIf(IsDateTimeNull(EmpRetireWaitRow.ApplyDate.Value), Convert.ToDateTime("1900/1/1"), EmpRetireWaitRow.ApplyDate.Value))
            If EmpRetireWaitRow.Kind.Updated Then db.AddInParameter(dbcmd, "@Kind", DbType.String, EmpRetireWaitRow.Kind.Value)
            If EmpRetireWaitRow.EmpRatio.Updated Then db.AddInParameter(dbcmd, "@EmpRatio", DbType.Decimal, EmpRetireWaitRow.EmpRatio.Value)
            If EmpRetireWaitRow.CompRatio.Updated Then db.AddInParameter(dbcmd, "@CompRatio", DbType.Decimal, EmpRetireWaitRow.CompRatio.Value)
            If EmpRetireWaitRow.ManagerFlag.Updated Then db.AddInParameter(dbcmd, "@ManagerFlag", DbType.String, EmpRetireWaitRow.ManagerFlag.Value)
            If EmpRetireWaitRow.BossFlag.Updated Then db.AddInParameter(dbcmd, "@BossFlag", DbType.String, EmpRetireWaitRow.BossFlag.Value)
            If EmpRetireWaitRow.Source.Updated Then db.AddInParameter(dbcmd, "@Source", DbType.String, EmpRetireWaitRow.Source.Value)
            If EmpRetireWaitRow.Amount.Updated Then db.AddInParameter(dbcmd, "@Amount", DbType.String, EmpRetireWaitRow.Amount.Value)
            If EmpRetireWaitRow.ReleaseDate.Updated Then db.AddInParameter(dbcmd, "@ReleaseDate", DbType.Date, IIf(IsDateTimeNull(EmpRetireWaitRow.ReleaseDate.Value), Convert.ToDateTime("1900/1/1"), EmpRetireWaitRow.ReleaseDate.Value))
            If EmpRetireWaitRow.ReleaseComp.Updated Then db.AddInParameter(dbcmd, "@ReleaseComp", DbType.String, EmpRetireWaitRow.ReleaseComp.Value)
            If EmpRetireWaitRow.ReleaseEmpID.Updated Then db.AddInParameter(dbcmd, "@ReleaseEmpID", DbType.String, EmpRetireWaitRow.ReleaseEmpID.Value)
            If EmpRetireWaitRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpRetireWaitRow.LastChgComp.Value)
            If EmpRetireWaitRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpRetireWaitRow.LastChgID.Value)
            If EmpRetireWaitRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpRetireWaitRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpRetireWaitRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(EmpRetireWaitRow.LoadFromDataRow, EmpRetireWaitRow.CompID.OldValue, EmpRetireWaitRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(EmpRetireWaitRow.LoadFromDataRow, EmpRetireWaitRow.EmpID.OldValue, EmpRetireWaitRow.EmpID.Value))
            db.AddInParameter(dbcmd, "@PKApplyDate", DbType.Date, IIf(EmpRetireWaitRow.LoadFromDataRow, EmpRetireWaitRow.ApplyDate.OldValue, EmpRetireWaitRow.ApplyDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal EmpRetireWaitRow As Row()) As Integer
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
                    For Each r As Row In EmpRetireWaitRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update EmpRetireWait Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where CompID = @PKCompID")
                        strSQL.AppendLine("And EmpID = @PKEmpID")
                        strSQL.AppendLine("And ApplyDate = @PKApplyDate")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.ReleaseMark.Updated Then db.AddInParameter(dbcmd, "@ReleaseMark", DbType.String, r.ReleaseMark.Value)
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        If r.ApplyDate.Updated Then db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, IIf(IsDateTimeNull(r.ApplyDate.Value), Convert.ToDateTime("1900/1/1"), r.ApplyDate.Value))
                        If r.Kind.Updated Then db.AddInParameter(dbcmd, "@Kind", DbType.String, r.Kind.Value)
                        If r.EmpRatio.Updated Then db.AddInParameter(dbcmd, "@EmpRatio", DbType.Decimal, r.EmpRatio.Value)
                        If r.CompRatio.Updated Then db.AddInParameter(dbcmd, "@CompRatio", DbType.Decimal, r.CompRatio.Value)
                        If r.ManagerFlag.Updated Then db.AddInParameter(dbcmd, "@ManagerFlag", DbType.String, r.ManagerFlag.Value)
                        If r.BossFlag.Updated Then db.AddInParameter(dbcmd, "@BossFlag", DbType.String, r.BossFlag.Value)
                        If r.Source.Updated Then db.AddInParameter(dbcmd, "@Source", DbType.String, r.Source.Value)
                        If r.Amount.Updated Then db.AddInParameter(dbcmd, "@Amount", DbType.String, r.Amount.Value)
                        If r.ReleaseDate.Updated Then db.AddInParameter(dbcmd, "@ReleaseDate", DbType.Date, IIf(IsDateTimeNull(r.ReleaseDate.Value), Convert.ToDateTime("1900/1/1"), r.ReleaseDate.Value))
                        If r.ReleaseComp.Updated Then db.AddInParameter(dbcmd, "@ReleaseComp", DbType.String, r.ReleaseComp.Value)
                        If r.ReleaseEmpID.Updated Then db.AddInParameter(dbcmd, "@ReleaseEmpID", DbType.String, r.ReleaseEmpID.Value)
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                        db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))
                        db.AddInParameter(dbcmd, "@PKApplyDate", DbType.Date, IIf(r.LoadFromDataRow, r.ApplyDate.OldValue, r.ApplyDate.Value))

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

        Public Function Update(ByVal EmpRetireWaitRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In EmpRetireWaitRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update EmpRetireWait Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where CompID = @PKCompID")
                strSQL.AppendLine("And EmpID = @PKEmpID")
                strSQL.AppendLine("And ApplyDate = @PKApplyDate")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.ReleaseMark.Updated Then db.AddInParameter(dbcmd, "@ReleaseMark", DbType.String, r.ReleaseMark.Value)
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.EmpID.Updated Then db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                If r.ApplyDate.Updated Then db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, IIf(IsDateTimeNull(r.ApplyDate.Value), Convert.ToDateTime("1900/1/1"), r.ApplyDate.Value))
                If r.Kind.Updated Then db.AddInParameter(dbcmd, "@Kind", DbType.String, r.Kind.Value)
                If r.EmpRatio.Updated Then db.AddInParameter(dbcmd, "@EmpRatio", DbType.Decimal, r.EmpRatio.Value)
                If r.CompRatio.Updated Then db.AddInParameter(dbcmd, "@CompRatio", DbType.Decimal, r.CompRatio.Value)
                If r.ManagerFlag.Updated Then db.AddInParameter(dbcmd, "@ManagerFlag", DbType.String, r.ManagerFlag.Value)
                If r.BossFlag.Updated Then db.AddInParameter(dbcmd, "@BossFlag", DbType.String, r.BossFlag.Value)
                If r.Source.Updated Then db.AddInParameter(dbcmd, "@Source", DbType.String, r.Source.Value)
                If r.Amount.Updated Then db.AddInParameter(dbcmd, "@Amount", DbType.String, r.Amount.Value)
                If r.ReleaseDate.Updated Then db.AddInParameter(dbcmd, "@ReleaseDate", DbType.Date, IIf(IsDateTimeNull(r.ReleaseDate.Value), Convert.ToDateTime("1900/1/1"), r.ReleaseDate.Value))
                If r.ReleaseComp.Updated Then db.AddInParameter(dbcmd, "@ReleaseComp", DbType.String, r.ReleaseComp.Value)
                If r.ReleaseEmpID.Updated Then db.AddInParameter(dbcmd, "@ReleaseEmpID", DbType.String, r.ReleaseEmpID.Value)
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                db.AddInParameter(dbcmd, "@PKEmpID", DbType.String, IIf(r.LoadFromDataRow, r.EmpID.OldValue, r.EmpID.Value))
                db.AddInParameter(dbcmd, "@PKApplyDate", DbType.Date, IIf(r.LoadFromDataRow, r.ApplyDate.OldValue, r.ApplyDate.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal EmpRetireWaitRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From EmpRetireWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And ApplyDate = @ApplyDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpRetireWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpRetireWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, EmpRetireWaitRow.ApplyDate.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal EmpRetireWaitRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From EmpRetireWait")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And EmpID = @EmpID")
            strSQL.AppendLine("And ApplyDate = @ApplyDate")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpRetireWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpRetireWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, EmpRetireWaitRow.ApplyDate.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From EmpRetireWait")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal EmpRetireWaitRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into EmpRetireWait")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    ReleaseMark, CompID, EmpID, ApplyDate, Kind, EmpRatio, CompRatio, ManagerFlag, BossFlag,")
            strSQL.AppendLine("    Source, Amount, ReleaseDate, ReleaseComp, ReleaseEmpID, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @ReleaseMark, @CompID, @EmpID, @ApplyDate, @Kind, @EmpRatio, @CompRatio, @ManagerFlag, @BossFlag,")
            strSQL.AppendLine("    @Source, @Amount, @ReleaseDate, @ReleaseComp, @ReleaseEmpID, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@ReleaseMark", DbType.String, EmpRetireWaitRow.ReleaseMark.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpRetireWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpRetireWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, IIf(IsDateTimeNull(EmpRetireWaitRow.ApplyDate.Value), Convert.ToDateTime("1900/1/1"), EmpRetireWaitRow.ApplyDate.Value))
            db.AddInParameter(dbcmd, "@Kind", DbType.String, EmpRetireWaitRow.Kind.Value)
            db.AddInParameter(dbcmd, "@EmpRatio", DbType.Decimal, EmpRetireWaitRow.EmpRatio.Value)
            db.AddInParameter(dbcmd, "@CompRatio", DbType.Decimal, EmpRetireWaitRow.CompRatio.Value)
            db.AddInParameter(dbcmd, "@ManagerFlag", DbType.String, EmpRetireWaitRow.ManagerFlag.Value)
            db.AddInParameter(dbcmd, "@BossFlag", DbType.String, EmpRetireWaitRow.BossFlag.Value)
            db.AddInParameter(dbcmd, "@Source", DbType.String, EmpRetireWaitRow.Source.Value)
            db.AddInParameter(dbcmd, "@Amount", DbType.String, EmpRetireWaitRow.Amount.Value)
            db.AddInParameter(dbcmd, "@ReleaseDate", DbType.Date, IIf(IsDateTimeNull(EmpRetireWaitRow.ReleaseDate.Value), Convert.ToDateTime("1900/1/1"), EmpRetireWaitRow.ReleaseDate.Value))
            db.AddInParameter(dbcmd, "@ReleaseComp", DbType.String, EmpRetireWaitRow.ReleaseComp.Value)
            db.AddInParameter(dbcmd, "@ReleaseEmpID", DbType.String, EmpRetireWaitRow.ReleaseEmpID.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpRetireWaitRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpRetireWaitRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpRetireWaitRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpRetireWaitRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal EmpRetireWaitRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into EmpRetireWait")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    ReleaseMark, CompID, EmpID, ApplyDate, Kind, EmpRatio, CompRatio, ManagerFlag, BossFlag,")
            strSQL.AppendLine("    Source, Amount, ReleaseDate, ReleaseComp, ReleaseEmpID, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @ReleaseMark, @CompID, @EmpID, @ApplyDate, @Kind, @EmpRatio, @CompRatio, @ManagerFlag, @BossFlag,")
            strSQL.AppendLine("    @Source, @Amount, @ReleaseDate, @ReleaseComp, @ReleaseEmpID, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@ReleaseMark", DbType.String, EmpRetireWaitRow.ReleaseMark.Value)
            db.AddInParameter(dbcmd, "@CompID", DbType.String, EmpRetireWaitRow.CompID.Value)
            db.AddInParameter(dbcmd, "@EmpID", DbType.String, EmpRetireWaitRow.EmpID.Value)
            db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, IIf(IsDateTimeNull(EmpRetireWaitRow.ApplyDate.Value), Convert.ToDateTime("1900/1/1"), EmpRetireWaitRow.ApplyDate.Value))
            db.AddInParameter(dbcmd, "@Kind", DbType.String, EmpRetireWaitRow.Kind.Value)
            db.AddInParameter(dbcmd, "@EmpRatio", DbType.Decimal, EmpRetireWaitRow.EmpRatio.Value)
            db.AddInParameter(dbcmd, "@CompRatio", DbType.Decimal, EmpRetireWaitRow.CompRatio.Value)
            db.AddInParameter(dbcmd, "@ManagerFlag", DbType.String, EmpRetireWaitRow.ManagerFlag.Value)
            db.AddInParameter(dbcmd, "@BossFlag", DbType.String, EmpRetireWaitRow.BossFlag.Value)
            db.AddInParameter(dbcmd, "@Source", DbType.String, EmpRetireWaitRow.Source.Value)
            db.AddInParameter(dbcmd, "@Amount", DbType.String, EmpRetireWaitRow.Amount.Value)
            db.AddInParameter(dbcmd, "@ReleaseDate", DbType.Date, IIf(IsDateTimeNull(EmpRetireWaitRow.ReleaseDate.Value), Convert.ToDateTime("1900/1/1"), EmpRetireWaitRow.ReleaseDate.Value))
            db.AddInParameter(dbcmd, "@ReleaseComp", DbType.String, EmpRetireWaitRow.ReleaseComp.Value)
            db.AddInParameter(dbcmd, "@ReleaseEmpID", DbType.String, EmpRetireWaitRow.ReleaseEmpID.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, EmpRetireWaitRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, EmpRetireWaitRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(EmpRetireWaitRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), EmpRetireWaitRow.LastChgDate.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal EmpRetireWaitRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into EmpRetireWait")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    ReleaseMark, CompID, EmpID, ApplyDate, Kind, EmpRatio, CompRatio, ManagerFlag, BossFlag,")
            strSQL.AppendLine("    Source, Amount, ReleaseDate, ReleaseComp, ReleaseEmpID, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @ReleaseMark, @CompID, @EmpID, @ApplyDate, @Kind, @EmpRatio, @CompRatio, @ManagerFlag, @BossFlag,")
            strSQL.AppendLine("    @Source, @Amount, @ReleaseDate, @ReleaseComp, @ReleaseEmpID, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In EmpRetireWaitRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@ReleaseMark", DbType.String, r.ReleaseMark.Value)
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                        db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, IIf(IsDateTimeNull(r.ApplyDate.Value), Convert.ToDateTime("1900/1/1"), r.ApplyDate.Value))
                        db.AddInParameter(dbcmd, "@Kind", DbType.String, r.Kind.Value)
                        db.AddInParameter(dbcmd, "@EmpRatio", DbType.Decimal, r.EmpRatio.Value)
                        db.AddInParameter(dbcmd, "@CompRatio", DbType.Decimal, r.CompRatio.Value)
                        db.AddInParameter(dbcmd, "@ManagerFlag", DbType.String, r.ManagerFlag.Value)
                        db.AddInParameter(dbcmd, "@BossFlag", DbType.String, r.BossFlag.Value)
                        db.AddInParameter(dbcmd, "@Source", DbType.String, r.Source.Value)
                        db.AddInParameter(dbcmd, "@Amount", DbType.String, r.Amount.Value)
                        db.AddInParameter(dbcmd, "@ReleaseDate", DbType.Date, IIf(IsDateTimeNull(r.ReleaseDate.Value), Convert.ToDateTime("1900/1/1"), r.ReleaseDate.Value))
                        db.AddInParameter(dbcmd, "@ReleaseComp", DbType.String, r.ReleaseComp.Value)
                        db.AddInParameter(dbcmd, "@ReleaseEmpID", DbType.String, r.ReleaseEmpID.Value)
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

        Public Function Insert(ByVal EmpRetireWaitRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into EmpRetireWait")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    ReleaseMark, CompID, EmpID, ApplyDate, Kind, EmpRatio, CompRatio, ManagerFlag, BossFlag,")
            strSQL.AppendLine("    Source, Amount, ReleaseDate, ReleaseComp, ReleaseEmpID, LastChgComp, LastChgID, LastChgDate")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @ReleaseMark, @CompID, @EmpID, @ApplyDate, @Kind, @EmpRatio, @CompRatio, @ManagerFlag, @BossFlag,")
            strSQL.AppendLine("    @Source, @Amount, @ReleaseDate, @ReleaseComp, @ReleaseEmpID, @LastChgComp, @LastChgID, @LastChgDate")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In EmpRetireWaitRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@ReleaseMark", DbType.String, r.ReleaseMark.Value)
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@EmpID", DbType.String, r.EmpID.Value)
                db.AddInParameter(dbcmd, "@ApplyDate", DbType.Date, IIf(IsDateTimeNull(r.ApplyDate.Value), Convert.ToDateTime("1900/1/1"), r.ApplyDate.Value))
                db.AddInParameter(dbcmd, "@Kind", DbType.String, r.Kind.Value)
                db.AddInParameter(dbcmd, "@EmpRatio", DbType.Decimal, r.EmpRatio.Value)
                db.AddInParameter(dbcmd, "@CompRatio", DbType.Decimal, r.CompRatio.Value)
                db.AddInParameter(dbcmd, "@ManagerFlag", DbType.String, r.ManagerFlag.Value)
                db.AddInParameter(dbcmd, "@BossFlag", DbType.String, r.BossFlag.Value)
                db.AddInParameter(dbcmd, "@Source", DbType.String, r.Source.Value)
                db.AddInParameter(dbcmd, "@Amount", DbType.String, r.Amount.Value)
                db.AddInParameter(dbcmd, "@ReleaseDate", DbType.Date, IIf(IsDateTimeNull(r.ReleaseDate.Value), Convert.ToDateTime("1900/1/1"), r.ReleaseDate.Value))
                db.AddInParameter(dbcmd, "@ReleaseComp", DbType.String, r.ReleaseComp.Value)
                db.AddInParameter(dbcmd, "@ReleaseEmpID", DbType.String, r.ReleaseEmpID.Value)
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

