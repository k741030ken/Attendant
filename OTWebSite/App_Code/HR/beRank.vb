'****************************************************************
' Table:Rank
' Created Date: 2015.08.06
'****************************************************************/
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.Data

Namespace beRank
    Public Class Table
        Private m_Rows As Rows = New Rows()
        Private m_Fields As String() = { "CompID", "RankID", "FixAmount", "YearHolidays", "GrpLvl", "LastChgComp", "LastChgID", "LastChgDate", "InValidFlag" }
        Private m_Types As System.Type() = { GetType(String), GetType(String), GetType(Decimal), GetType(Decimal), GetType(String), GetType(String), GetType(String), GetType(Date), GetType(String) }
        Private m_PrimaryFields As String() = { "CompID", "RankID" }

        Public ReadOnly Property Rows() As beRank.Rows 
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
        Public Sub Transfer2Row(RankTable As DataTable)
            For Each dr As DataRow In RankTable.Rows
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
                dr(m_Rows(i).RankID.FieldName) = m_Rows(i).RankID.Value
                dr(m_Rows(i).FixAmount.FieldName) = m_Rows(i).FixAmount.Value
                dr(m_Rows(i).YearHolidays.FieldName) = m_Rows(i).YearHolidays.Value
                dr(m_Rows(i).GrpLvl.FieldName) = m_Rows(i).GrpLvl.Value
                dr(m_Rows(i).LastChgComp.FieldName) = m_Rows(i).LastChgComp.Value
                dr(m_Rows(i).LastChgID.FieldName) = m_Rows(i).LastChgID.Value
                dr(m_Rows(i).LastChgDate.FieldName) = m_Rows(i).LastChgDate.Value
                dr(m_Rows(i).InValidFlag.FieldName) = m_Rows(i).InValidFlag.Value

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

        Public Sub Add(RankRow As Row)
            m_Rows.Add(RankRow)
        End Sub

        Public Sub Remove(RankRow As Row)
            If m_Rows.IndexOf(RankRow) >= 0 Then
                m_Rows.Remove(RankRow)
            End If
        End Sub

        Public Sub Dispose()
            m_Rows.Clear()
        End Sub

    End Class

    Public Class Row

        Private FI_CompID As Field(Of String) = new Field(Of String)("CompID", true)
        Private FI_RankID As Field(Of String) = new Field(Of String)("RankID", true)
        Private FI_FixAmount As Field(Of Decimal) = new Field(Of Decimal)("FixAmount", true)
        Private FI_YearHolidays As Field(Of Decimal) = new Field(Of Decimal)("YearHolidays", true)
        Private FI_GrpLvl As Field(Of String) = new Field(Of String)("GrpLvl", true)
        Private FI_LastChgComp As Field(Of String) = new Field(Of String)("LastChgComp", true)
        Private FI_LastChgID As Field(Of String) = new Field(Of String)("LastChgID", true)
        Private FI_LastChgDate As Field(Of Date) = new Field(Of Date)("LastChgDate", true)
        Private FI_InValidFlag As Field(Of String) = new Field(Of String)("InValidFlag", true)
        Private m_FieldNames As String() = { "CompID", "RankID", "FixAmount", "YearHolidays", "GrpLvl", "LastChgComp", "LastChgID", "LastChgDate", "InValidFlag" }
        Private m_PrimaryFields As String() = { "CompID", "RankID" }
        Private m_IdentityFields As String() = {  }
        Private m_LoadFromDataRow As Boolean = False

        Private Function GetFieldValue(ByVal fieldName As String) As Object
            Select Case fieldName
                Case "CompID"
                    Return FI_CompID.Value
                Case "RankID"
                    Return FI_RankID.Value
                Case "FixAmount"
                    Return FI_FixAmount.Value
                Case "YearHolidays"
                    Return FI_YearHolidays.Value
                Case "GrpLvl"
                    Return FI_GrpLvl.Value
                Case "LastChgComp"
                    Return FI_LastChgComp.Value
                Case "LastChgID"
                    Return FI_LastChgID.Value
                Case "LastChgDate"
                    Return FI_LastChgDate.Value
                Case "InValidFlag"
                    Return FI_InValidFlag.Value
                Case Else
                    Return Nothing
            End Select
        End Function

        Private Sub SetFieldValue(ByVal fieldName As String, ByVal value As Object)
            Select Case fieldName
                Case "CompID"
                    FI_CompID.SetValue(value)
                Case "RankID"
                    FI_RankID.SetValue(value)
                Case "FixAmount"
                    FI_FixAmount.SetValue(value)
                Case "YearHolidays"
                    FI_YearHolidays.SetValue(value)
                Case "GrpLvl"
                    FI_GrpLvl.SetValue(value)
                Case "LastChgComp"
                    FI_LastChgComp.SetValue(value)
                Case "LastChgID"
                    FI_LastChgID.SetValue(value)
                Case "LastChgDate"
                    FI_LastChgDate.SetValue(value)
                Case "InValidFlag"
                    FI_InValidFlag.SetValue(value)
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
                Case "RankID"
                    return FI_RankID.Updated
                Case "FixAmount"
                    return FI_FixAmount.Updated
                Case "YearHolidays"
                    return FI_YearHolidays.Updated
                Case "GrpLvl"
                    return FI_GrpLvl.Updated
                Case "LastChgComp"
                    return FI_LastChgComp.Updated
                Case "LastChgID"
                    return FI_LastChgID.Updated
                Case "LastChgDate"
                    return FI_LastChgDate.Updated
                Case "InValidFlag"
                    return FI_InValidFlag.Updated
                Case Else
                    Throw New Exception("無此欄位！")
            End Select
        End Function

        Public Function CreateUpdateSQL(ByVal fieldName As String) As Boolean
            Select Case fieldName
                Case "CompID"
                    return FI_CompID.CreateUpdateSQL
                Case "RankID"
                    return FI_RankID.CreateUpdateSQL
                Case "FixAmount"
                    return FI_FixAmount.CreateUpdateSQL
                Case "YearHolidays"
                    return FI_YearHolidays.CreateUpdateSQL
                Case "GrpLvl"
                    return FI_GrpLvl.CreateUpdateSQL
                Case "LastChgComp"
                    return FI_LastChgComp.CreateUpdateSQL
                Case "LastChgID"
                    return FI_LastChgID.CreateUpdateSQL
                Case "LastChgDate"
                    return FI_LastChgDate.CreateUpdateSQL
                Case "InValidFlag"
                    return FI_InValidFlag.CreateUpdateSQL
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
            FI_RankID.SetInitValue("")
            FI_FixAmount.SetInitValue(0)
            FI_YearHolidays.SetInitValue(0)
            FI_GrpLvl.SetInitValue("")
            FI_LastChgComp.SetInitValue("")
            FI_LastChgID.SetInitValue("")
            FI_LastChgDate.SetInitValue(Convert.ToDateTime("1900/01/01"))
            FI_InValidFlag.SetInitValue("0")
        End Sub

        Public Sub New(ByVal dr As System.Data.DataRow)
            FI_CompID.SetInitValue(dr("CompID"))
            FI_RankID.SetInitValue(dr("RankID"))
            FI_FixAmount.SetInitValue(dr("FixAmount"))
            FI_YearHolidays.SetInitValue(dr("YearHolidays"))
            FI_GrpLvl.SetInitValue(dr("GrpLvl"))
            FI_LastChgComp.SetInitValue(dr("LastChgComp"))
            FI_LastChgID.SetInitValue(dr("LastChgID"))
            FI_LastChgDate.SetInitValue(dr("LastChgDate"))
            FI_InValidFlag.SetInitValue(dr("InValidFlag"))

            m_LoadFromDataRow = True
        End Sub

        Private Sub ClearUpdated()
            FI_CompID.Updated = False
            FI_RankID.Updated = False
            FI_FixAmount.Updated = False
            FI_YearHolidays.Updated = False
            FI_GrpLvl.Updated = False
            FI_LastChgComp.Updated = False
            FI_LastChgID.Updated = False
            FI_LastChgDate.Updated = False
            FI_InValidFlag.Updated = False
        End Sub

        Public ReadOnly Property CompID As Field(Of String) 
            Get
                Return FI_CompID
            End Get
        End Property

        Public ReadOnly Property RankID As Field(Of String) 
            Get
                Return FI_RankID
            End Get
        End Property

        Public ReadOnly Property FixAmount As Field(Of Decimal) 
            Get
                Return FI_FixAmount
            End Get
        End Property

        Public ReadOnly Property YearHolidays As Field(Of Decimal) 
            Get
                Return FI_YearHolidays
            End Get
        End Property

        Public ReadOnly Property GrpLvl As Field(Of String) 
            Get
                Return FI_GrpLvl
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

        Public ReadOnly Property InValidFlag As Field(Of String) 
            Get
                Return FI_InValidFlag
            End Get
        End Property

    End Class

    Public Class Service
        Public Function DeleteRowByPrimaryKey(ByVal RankRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From Rank")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And RankID = @RankID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, RankRow.CompID.Value)
            db.AddInParameter(dbcmd, "@RankID", DbType.String, RankRow.RankID.Value)

            return db.ExecuteNonQuery(dbcmd)
        End Function

        public Function DeleteRowByPrimaryKey(ByVal RankRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Delete From Rank")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And RankID = @RankID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, RankRow.CompID.Value)
            db.AddInParameter(dbcmd, "@RankID", DbType.String, RankRow.RankID.Value)

            return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function DeleteRowByPrimaryKey(ByVal RankRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From Rank")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And RankID = @RankID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In RankRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)

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

        Public Function DeleteRowByPrimaryKey(ByVal RankRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Delete From Rank")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And RankID = @RankID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In RankRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function QueryByKey(ByVal RankRow As Row) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Rank")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And RankID = @RankID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, RankRow.CompID.Value)
            db.AddInParameter(dbcmd, "@RankID", DbType.String, RankRow.RankID.Value)

            Return db.ExecuteDataSet(dbcmd)
        End Function

        Public Function QueryByKey(RankRow As Row, tran As DbTransaction) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Rank")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And RankID = @RankID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, RankRow.CompID.Value)
            db.AddInParameter(dbcmd, "@RankID", DbType.String, RankRow.RankID.Value)

            Return db.ExecuteDataSet(dbcmd, tran)
        End Function

        Public Function Update(ByVal RankRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update Rank Set")
            For i As Integer = 0 To RankRow.FieldNames.Length - 1
                If Not RankRow.IsIdentityField(RankRow.FieldNames(i)) AndAlso RankRow.IsUpdated(RankRow.FieldNames(i)) AndAlso RankRow.CreateUpdateSQL(RankRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, RankRow.FieldNames(i)))
                    strDot = ","
                End If
            Next
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And RankID = @PKRankID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If RankRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, RankRow.CompID.Value)
            If RankRow.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, RankRow.RankID.Value)
            If RankRow.FixAmount.Updated Then db.AddInParameter(dbcmd, "@FixAmount", DbType.Decimal, RankRow.FixAmount.Value)
            If RankRow.YearHolidays.Updated Then db.AddInParameter(dbcmd, "@YearHolidays", DbType.Decimal, RankRow.YearHolidays.Value)
            If RankRow.GrpLvl.Updated Then db.AddInParameter(dbcmd, "@GrpLvl", DbType.String, RankRow.GrpLvl.Value)
            If RankRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, RankRow.LastChgComp.Value)
            If RankRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, RankRow.LastChgID.Value)
            If RankRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(RankRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), RankRow.LastChgDate.Value))
            If RankRow.InValidFlag.Updated Then db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, RankRow.InValidFlag.Value)
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(RankRow.LoadFromDataRow, RankRow.CompID.OldValue, RankRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKRankID", DbType.String, IIf(RankRow.LoadFromDataRow, RankRow.RankID.OldValue, RankRow.RankID.Value))

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Update(ByVal RankRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim strDot As String = String.Empty

            strSQL.AppendLine("Update Rank Set")
            For i As Integer = 0 To RankRow.FieldNames.Length - 1
                If Not RankRow.IsIdentityField(RankRow.FieldNames(i)) AndAlso RankRow.IsUpdated(RankRow.FieldNames(i)) AndAlso RankRow.CreateUpdateSQL(RankRow.FieldNames(i)) Then
                    strSQL.AppendLine(string.Format("{0}{1} = @{1}", strDot, RankRow.FieldNames(i)))
                    strDot = ","
                End If
            Next 
            If strDot = String.Empty Then Throw New Exception("未異動資料欄位！")
            strSQL.AppendLine("Where CompID = @PKCompID")
            strSQL.AppendLine("And RankID = @PKRankID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            If RankRow.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, RankRow.CompID.Value)
            If RankRow.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, RankRow.RankID.Value)
            If RankRow.FixAmount.Updated Then db.AddInParameter(dbcmd, "@FixAmount", DbType.Decimal, RankRow.FixAmount.Value)
            If RankRow.YearHolidays.Updated Then db.AddInParameter(dbcmd, "@YearHolidays", DbType.Decimal, RankRow.YearHolidays.Value)
            If RankRow.GrpLvl.Updated Then db.AddInParameter(dbcmd, "@GrpLvl", DbType.String, RankRow.GrpLvl.Value)
            If RankRow.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, RankRow.LastChgComp.Value)
            If RankRow.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, RankRow.LastChgID.Value)
            If RankRow.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(RankRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), RankRow.LastChgDate.Value))
            If RankRow.InValidFlag.Updated Then db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, RankRow.InValidFlag.Value)
            db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(RankRow.LoadFromDataRow, RankRow.CompID.OldValue, RankRow.CompID.Value))
            db.AddInParameter(dbcmd, "@PKRankID", DbType.String, IIf(RankRow.LoadFromDataRow, RankRow.RankID.OldValue, RankRow.RankID.Value))

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Update(ByVal RankRow As Row()) As Integer
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
                    For Each r As Row In RankRow
                        strDot = String.Empty
                        If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                        strSQL.AppendLine("Update Rank Set")
                        For i As Integer = 0 To r.FieldNames.Length - 1
                            If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i)) Then
                                strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                                strDot = ","
                            End If
                        Next
                        If strDot = String.Empty Then Continue For
                        strSQL.AppendLine("Where CompID = @PKCompID")
                        strSQL.AppendLine("And RankID = @PKRankID")

                        dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                        If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        If r.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                        If r.FixAmount.Updated Then db.AddInParameter(dbcmd, "@FixAmount", DbType.Decimal, r.FixAmount.Value)
                        If r.YearHolidays.Updated Then db.AddInParameter(dbcmd, "@YearHolidays", DbType.Decimal, r.YearHolidays.Value)
                        If r.GrpLvl.Updated Then db.AddInParameter(dbcmd, "@GrpLvl", DbType.String, r.GrpLvl.Value)
                        If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        If r.InValidFlag.Updated Then db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, r.InValidFlag.Value)
                        db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                        db.AddInParameter(dbcmd, "@PKRankID", DbType.String, IIf(r.LoadFromDataRow, r.RankID.OldValue, r.RankID.Value))

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

        Public Function Update(ByVal RankRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0
            Dim strDot As String

            For Each r As Row In RankRow
                strDot = String.Empty
                If strSQL.Length > 0 Then strSQL.Remove(0, strSQL.Length)
                strSQL.AppendLine("Update Rank Set")
                For i As Integer = 0 To r.FieldNames.Length - 1
                    If Not r.IsIdentityField(r.FieldNames(i)) AndAlso r.IsUpdated(r.FieldNames(i)) AndAlso r.CreateUpdateSQL(r.FieldNames(i))
                        strSQL.AppendLine(String.Format("{0}{1} = @{1}", strDot, r.FieldNames(i)))
                        strDot = ","
                    End If
                Next
                If strDot = String.Empty Then Continue For
                strSQL.AppendLine("Where CompID = @PKCompID")
                strSQL.AppendLine("And RankID = @PKRankID")

                dbcmd = db.GetSqlStringCommand(strSQL.ToString())
                If r.CompID.Updated Then db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                If r.RankID.Updated Then db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                If r.FixAmount.Updated Then db.AddInParameter(dbcmd, "@FixAmount", DbType.Decimal, r.FixAmount.Value)
                If r.YearHolidays.Updated Then db.AddInParameter(dbcmd, "@YearHolidays", DbType.Decimal, r.YearHolidays.Value)
                If r.GrpLvl.Updated Then db.AddInParameter(dbcmd, "@GrpLvl", DbType.String, r.GrpLvl.Value)
                If r.LastChgComp.Updated Then db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                If r.LastChgID.Updated Then db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                If r.LastChgDate.Updated Then db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                If r.InValidFlag.Updated Then db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, r.InValidFlag.Value)
                db.AddInParameter(dbcmd, "@PKCompID", DbType.String, IIf(r.LoadFromDataRow, r.CompID.OldValue, r.CompID.Value))
                db.AddInParameter(dbcmd, "@PKRankID", DbType.String, IIf(r.LoadFromDataRow, r.RankID.OldValue, r.RankID.Value))

                intRowsAffected += db.ExecuteNonQuery(dbcmd, tran)
            Next
            Return intRowsAffected
        End Function

        Public Function IsDataExists(ByVal RankRow As Row) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From Rank")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And RankID = @RankID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, RankRow.CompID.Value)
            db.AddInParameter(dbcmd, "@RankID", DbType.String, RankRow.RankID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function IsDataExists(ByVal RankRow As Row, ByVal tran As DbTransaction) As Boolean
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intCount As Integer = 0

            strSQL.AppendLine("Select Count(*) Cnt From Rank")
            strSQL.AppendLine("Where CompID = @CompID")
            strSQL.AppendLine("And RankID = @RankID")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, RankRow.CompID.Value)
            db.AddInParameter(dbcmd, "@RankID", DbType.String, RankRow.RankID.Value)

            intCount = Convert.ToInt32(db.ExecuteScalar(dbcmd, tran))
            Return IIf(intCount > 0, True, False)
        End Function

        Public Function QuerybyWhere(WhereCondition As String) As DataSet
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Select * From Rank")
            If WhereCondition <> String.Empty Then strSQL.AppendLine(WhereCondition)

            Return db.ExecuteDataSet(CommandType.Text, strSQL.ToString())
        End Function

        Public Function Insert(ByVal RankRow As Row) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into Rank")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, RankID, FixAmount, YearHolidays, GrpLvl, LastChgComp, LastChgID, LastChgDate,")
            strSQL.AppendLine("    InValidFlag")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @RankID, @FixAmount, @YearHolidays, @GrpLvl, @LastChgComp, @LastChgID, @LastChgDate,")
            strSQL.AppendLine("    @InValidFlag")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, RankRow.CompID.Value)
            db.AddInParameter(dbcmd, "@RankID", DbType.String, RankRow.RankID.Value)
            db.AddInParameter(dbcmd, "@FixAmount", DbType.Decimal, RankRow.FixAmount.Value)
            db.AddInParameter(dbcmd, "@YearHolidays", DbType.Decimal, RankRow.YearHolidays.Value)
            db.AddInParameter(dbcmd, "@GrpLvl", DbType.String, RankRow.GrpLvl.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, RankRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, RankRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(RankRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), RankRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, RankRow.InValidFlag.Value)

            Return db.ExecuteNonQuery(dbcmd)
        End Function

        Public Function Insert(ByVal RankRow As Row, ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()

            strSQL.AppendLine("Insert into Rank")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, RankID, FixAmount, YearHolidays, GrpLvl, LastChgComp, LastChgID, LastChgDate,")
            strSQL.AppendLine("    InValidFlag")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @RankID, @FixAmount, @YearHolidays, @GrpLvl, @LastChgComp, @LastChgID, @LastChgDate,")
            strSQL.AppendLine("    @InValidFlag")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            db.AddInParameter(dbcmd, "@CompID", DbType.String, RankRow.CompID.Value)
            db.AddInParameter(dbcmd, "@RankID", DbType.String, RankRow.RankID.Value)
            db.AddInParameter(dbcmd, "@FixAmount", DbType.Decimal, RankRow.FixAmount.Value)
            db.AddInParameter(dbcmd, "@YearHolidays", DbType.Decimal, RankRow.YearHolidays.Value)
            db.AddInParameter(dbcmd, "@GrpLvl", DbType.String, RankRow.GrpLvl.Value)
            db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, RankRow.LastChgComp.Value)
            db.AddInParameter(dbcmd, "@LastChgID", DbType.String, RankRow.LastChgID.Value)
            db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(RankRow.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), RankRow.LastChgDate.Value))
            db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, RankRow.InValidFlag.Value)

            Return db.ExecuteNonQuery(dbcmd, tran)
        End Function

        Public Function Insert(ByVal RankRow As Row()) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into Rank")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, RankID, FixAmount, YearHolidays, GrpLvl, LastChgComp, LastChgID, LastChgDate,")
            strSQL.AppendLine("    InValidFlag")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @RankID, @FixAmount, @YearHolidays, @GrpLvl, @LastChgComp, @LastChgID, @LastChgDate,")
            strSQL.AppendLine("    @InValidFlag")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())
            Using cn As DbConnection = db.CreateConnection()
                cn.Open()
                Dim tran As DbTransaction = cn.BeginTransaction()
                Dim inTrans As Boolean = True

                Try
                    For Each r As Row In RankRow
                        dbcmd.Parameters.Clear()
                        db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                        db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                        db.AddInParameter(dbcmd, "@FixAmount", DbType.Decimal, r.FixAmount.Value)
                        db.AddInParameter(dbcmd, "@YearHolidays", DbType.Decimal, r.YearHolidays.Value)
                        db.AddInParameter(dbcmd, "@GrpLvl", DbType.String, r.GrpLvl.Value)
                        db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                        db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                        db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                        db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, r.InValidFlag.Value)

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

        Public Function Insert(ByVal RankRow As Row(), ByVal tran As DbTransaction) As Integer
            Dim db As Database = DatabaseFactory.CreateDatabase("eHRMSDB")
            Dim dbcmd As DbCommand
            Dim strSQL As StringBuilder = New StringBuilder()
            Dim intRowsAffected As Integer = 0

            strSQL.AppendLine("Insert into Rank")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    CompID, RankID, FixAmount, YearHolidays, GrpLvl, LastChgComp, LastChgID, LastChgDate,")
            strSQL.AppendLine("    InValidFlag")
            strSQL.AppendLine(")")
            strSQL.AppendLine("Values")
            strSQL.AppendLine("(")
            strSQL.AppendLine("    @CompID, @RankID, @FixAmount, @YearHolidays, @GrpLvl, @LastChgComp, @LastChgID, @LastChgDate,")
            strSQL.AppendLine("    @InValidFlag")
            strSQL.AppendLine(")")

            dbcmd = db.GetSqlStringCommand(strSQL.ToString())

            For Each r As Row In RankRow
                dbcmd.Parameters.Clear()
                db.AddInParameter(dbcmd, "@CompID", DbType.String, r.CompID.Value)
                db.AddInParameter(dbcmd, "@RankID", DbType.String, r.RankID.Value)
                db.AddInParameter(dbcmd, "@FixAmount", DbType.Decimal, r.FixAmount.Value)
                db.AddInParameter(dbcmd, "@YearHolidays", DbType.Decimal, r.YearHolidays.Value)
                db.AddInParameter(dbcmd, "@GrpLvl", DbType.String, r.GrpLvl.Value)
                db.AddInParameter(dbcmd, "@LastChgComp", DbType.String, r.LastChgComp.Value)
                db.AddInParameter(dbcmd, "@LastChgID", DbType.String, r.LastChgID.Value)
                db.AddInParameter(dbcmd, "@LastChgDate", DbType.Date, IIf(IsDateTimeNull(r.LastChgDate.Value), Convert.ToDateTime("1900/1/1"), r.LastChgDate.Value))
                db.AddInParameter(dbcmd, "@InValidFlag", DbType.String, r.InValidFlag.Value)

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

